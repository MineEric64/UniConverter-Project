Imports System.IO
Imports System.Text

Imports NAudio.Wave
Imports NAudio.CoreAudioApi
Imports NAudio.Wave.SampleProviders

''' <summary>
''' Invoke가 필요 없는 mciFunctions 클래스보다 향상된 사운드 관리자 입니다. mciFunctions랑 기능은 같지만 더 효율적인 코드를 제공합니다.
''' 또한 오디오 32비트를 지원합니다.
''' </summary>
Public Class SoundManager

    '이 코드는 ProjectUnitor v3.1.2.2 (Unstable-v4.0.3)을 기준으로 한 소리 관리자 코드입니다.
    '소리를 재생할 때 필요한 클래스이며 원본 소스 코드는 ProjectUnitor 내에서 확인하실 수 있습니다.

    ''' <summary>
    ''' 사운드의 볼륨을 설정합니다. (기본 1.0)
    ''' </summary>
    Public Shared Property SoundVolume As Single = 1.0

    Private Shared enumerator As MMDeviceEnumerator = New MMDeviceEnumerator()
    Private Shared PlaybackDevice As String = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia).ID

    Private Shared WaveOut As WasapiOut = New WasapiOut(enumerator.GetDevice(PlaybackDevice), AudioClientShareMode.Shared, True, 10)
    Private Shared Mixer As New MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2)) With {
        .ReadFully = True}
    Private Shared ctrl As New Dictionary(Of SoundKey, SoundLibrary)

    ''' <summary>
    ''' 소리 파일을 추가합니다. NAudio.Wave 네임스페이스를 사용하며 RAM (Random Access Memory)에 접근해서 소리를 빠르게 재생합니다.
    ''' 캐시 파일이기 때문에 속도가 굉장히 빠릅니다.
    ''' </summary>
    ''' <param name="chain"></param>
    ''' <param name="x">-1은 mc입니다.</param>
    ''' <param name="y"></param>
    ''' <param name="path"></param>
    Public Shared Sub AddSound(chain As Integer, x As Integer, y As Integer, index As Integer, path As String)
        If Not File.Exists(path) Then
            Throw New FileNotFoundException()
        End If

        Dim newSound = New CachedSound(path)
        Dim key = New SoundKey(chain, x, y, index)

        If ctrl.ContainsKey(key) Then
            CloseSound(chain, x, y, index)
        End If

        Dim soundOnDisk = New WaveFileReader(path)
        ctrl.Add(key, New SoundLibrary(soundOnDisk.TotalTime, newSound))

        soundOnDisk.Dispose()
        soundOnDisk = Nothing
    End Sub

    ''' <summary>
    ''' 사운드 플레이시 첫 초기화를 해주므로서 첫 SoundManager.PlaySound() 지연 시간을 단축해줍니다.
    ''' </summary>
    Public Shared Sub InitializeSound()
        WaveOut.Init(Mixer)
        WaveOut.Play()
    End Sub

    ''' <summary>
    ''' WasApi에서 사용하는 오디오 디바이스를 변경해줍니다.
    ''' </summary>
    Private Shared Sub ChangePlaybackDevice()
        PlaybackDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia).ID
        WaveOut = New WasapiOut(enumerator.GetDevice(PlaybackDevice), AudioClientShareMode.Shared, True, 10)
        Mixer = New MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2)) With {
        .ReadFully = True}

        InitializeSound()
    End Sub

    ''' <summary>
    ''' 소리 파일을 재생합니다.
    ''' </summary>
    ''' <param name="sound">소리 재생 요청용 특정한 변수를 데이터로 보냅니다.</param>
    Public Shared Sub PlaySound(sound As SoundKey)
        If enumerator.GetDevice(PlaybackDevice).ID = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia).ID Then
            If ctrl.ContainsKey(sound) Then

                'Sound Data
                Dim cacheSoundSampleProvider As New CachedSoundSampleProvider(ctrl(sound).soundOnMemory)

                'Extension
                Dim VolumeSampleProvider As New VolumeSampleProvider(cacheSoundSampleProvider) With {
                    .Volume = SoundVolume}

                'All mixer inputs must have the same WaveFormat
                Dim resampler As New WdlResamplingSampleProvider(VolumeSampleProvider, 44100)
                Dim ChannelResampler As ISampleProvider = resampler.ToStereo()

                Mixer.AddMixerInput(ChannelResampler)

                If Not WaveOut.PlaybackState = PlaybackState.Playing Then
                    WaveOut.Init(Mixer)
                    WaveOut.Play()
                End If
            End If
        Else
            ChangePlaybackDevice()
            PlaySound(sound)
        End If
    End Sub

    Public Shared Sub CloseSound(chain As Integer, x As Integer, y As Integer, index As Integer)
        Dim sound = New SoundKey(chain, x, y, index)

        If ctrl.ContainsKey(sound) Then
            ctrl(sound) = Nothing
            ctrl.Remove(sound)
        End If
    End Sub

    Public Shared Sub CloseAllSounds()
        Dim keys As SoundKey() = ctrl.Keys.ToArray()

        For i = 0 To keys.Length - 1
            ctrl.Remove(keys(i))
        Next
    End Sub

    Public Shared Sub Close()
        CloseAllSounds()
        enumerator.Dispose()
        WaveOut.Stop()
        WaveOut.Dispose()
    End Sub

    ''' <summary>
    ''' 추가한 사운드의 key로 통해 사운드의 길이를 구합니다.
    ''' </summary>
    Public Shared Function GetSoundLength(chain As Integer, x As Integer, y As Integer, index As Integer) As TimeSpan
        Dim sound = New SoundKey(chain, x, y, index)

        If Not ctrl.ContainsKey(sound) Then
            Return TimeSpan.FromMilliseconds(0)
        End If

        Return ctrl(sound).soundTime
    End Function

    ''' <summary>
    ''' 로컬 파일로 접근하여 사운드의 길이를 구합니다.
    ''' </summary>
    ''' <param name="path">로컬 파일의 위치</param>
    ''' <returns></returns>
    Public Shared Function GetSoundLength(path As String) As TimeSpan
        If Not File.Exists(path) Then
            Return TimeSpan.FromMilliseconds(0)
        End If

        Dim sound As AudioFileReader = New AudioFileReader(path)
        Dim soundLength As TimeSpan = sound.TotalTime

        sound.Dispose()
        sound = Nothing
        Return soundLength
    End Function

    ''' <summary>
    ''' 현재 사운드 플레이어가 플레이 중인지 물어봅니다. 읽기 전용입니다.
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property IsPlaying As Boolean
        Get
            Return WaveOut.PlaybackState = PlaybackState.Playing
        End Get
    End Property

    Public Class SoundKey
        Public Property Chain As Integer
        Public Property X As Integer
        Public Property Y As Integer
        Public Property Index As Integer

        Sub New(chain_ As Integer, x_ As Integer, y_ As Integer, index_ As Integer)
            Chain = chain_
            X = x_
            Y = y_
            Index = index_
        End Sub
    End Class

    Private Structure SoundLibrary
        Public soundTime As TimeSpan
        Public soundOnMemory As CachedSound

        Sub New(soundTime As TimeSpan, soundOnMemory As CachedSound)
            Me.soundTime = soundTime
            Me.soundOnMemory = soundOnMemory
        End Sub
    End Structure
End Class

#Region "CachedSound Class"
Friend Class CachedSound
    Private _AudioData As Single(), _WaveFormat As WaveFormat

    Public Property AudioData As Single()
        Get
            Return _AudioData
        End Get
        Private Set(ByVal value As Single())
            _AudioData = value
        End Set
    End Property

    Public Property WaveFormat As WaveFormat
        Get
            Return _WaveFormat
        End Get
        Private Set(ByVal value As WaveFormat)
            _WaveFormat = value
        End Set
    End Property

    Public Sub New(ByVal audioFileName As String)
        Using audioFileReader = New AudioFileReader(audioFileName)
            WaveFormat = audioFileReader.WaveFormat
            Dim wholeFile = New List(Of Single)(CInt(audioFileReader.Length / 4))
            Dim readBuffer = New Single(audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels - 1) {}
            Dim samplesRead As Integer = audioFileReader.Read(readBuffer, 0, readBuffer.Length)

            While samplesRead > 0
                wholeFile.AddRange(readBuffer.Take(samplesRead))
                samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)
            End While

            AudioData = wholeFile.ToArray()
        End Using
    End Sub
End Class

Friend Class CachedSoundSampleProvider
    Implements ISampleProvider

    Private ReadOnly cachedSound As CachedSound
    Private position As Long

    Public Sub New(ByVal cachedSound As CachedSound)
        Me.cachedSound = cachedSound
    End Sub

    Public Function Read(buffer() As Single, offset As Integer, count As Integer) As Integer Implements ISampleProvider.Read
        Dim availableSamples = cachedSound.AudioData.Length - position
        Dim samplesToCopy = Math.Min(availableSamples, count)
        Array.Copy(cachedSound.AudioData, position, buffer, offset, samplesToCopy)
        position += samplesToCopy
        Return CInt(samplesToCopy)
    End Function

    Public ReadOnly Property WaveFormat As WaveFormat Implements ISampleProvider.WaveFormat
        Get
            Return cachedSound.WaveFormat
        End Get
    End Property
End Class

#End Region
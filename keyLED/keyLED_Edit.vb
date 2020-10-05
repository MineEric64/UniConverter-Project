Imports System.IO
Imports System.Threading
Imports System.Text

Imports NAudio.Midi

Imports A2UP.A2U.keyLED_MIDEX

Public Class keyLED_Edit
    Public CanEnable As Boolean = False
    Public WaitForSpeed As Boolean = False
    Private _uniText As String

    Public ExtensionList As New Dictionary(Of String, LEDExtensions)()
    Public Const EXTENSION_DEFAULT_KEY As String = "DIRECT-KEY"

    Private Sub KeyLED_Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'FileName 표시.
        For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\ableproj\CoLED", FileIO.SearchOption.SearchTopLevelOnly, "*.mid") 'FileName의 파일 찾기
            Invoke(Sub()
                       Dim itm As New ListViewItem(New String() {Path.GetFileName(foundFile), foundFile})
                       LED_ListView.Items.Add(itm)  '파일 이름 추가
                   End Sub)
        Next

        Select Case MainProject.lang
            Case Translator.tL.Korean
                Text = "keyLED 편집 (미디 익스텐션, 베타)"
                AblLED.Text = "에이블톤 LED 파일"
                UniLED.Text = "유니팩 LED 텍스트"
                UniLED1.Text = "파일 이름: N/A"
                spTipLb.Text = "빠르기:"
                TimeLabel.Text = "LED Delay 시간:"

                LED_ListView.Columns(0).Text = "파일 이름"
                ALGModeBox.Items.Clear()
                ALGModeBox.Items.AddRange({"에이블톤 라이브 미디 1", "원본 (제작자 모드)"})
                ALGModeBox.Text = "에이블톤 라이브 미디 1"

                TestButton.Text = "테스트"
                CopyButton.Text = "복사"
                LEDExButton.Text = "LED 확장 플러그인"
        End Select
    End Sub

    Private Sub CopyButton_Click(sender As Object, e As EventArgs) Handles CopyButton.Click
        '복사 코드.
        If UniLED_Edit.Enabled = False Then
            Select Case MainProject.lang
                Case Translator.tL.English
                    MessageBox.Show("First, You have to convert LED!", "UniConverter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Case Translator.tL.Korean
                    MessageBox.Show("먼저, LED를 변환해야 합니다!", "유니컨버터", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Select
        Else
            Clipboard.SetText(UniLED_Edit.Text)
            Select Case MainProject.lang
                Case Translator.tL.English
                    MessageBox.Show("UniPack LED Copied!", "UniConverter", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Case Translator.tL.Korean
                    MessageBox.Show("유니팩 LED를 복사 했습니다!", "유니컨버터", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Select
        End If
    End Sub

    Private Async Sub GazuaButton_Click(sender As Object, e As EventArgs) Handles GazuaButton.Click
        Try
            CanEnable = False
            TestButton.Enabled = False
            keyLED_Test.Enabled = False
            CopyButton.Enabled = False
            LEDExButton.Enabled = False
            keyLED_Edit_Ex.Enabled = False

            Dim extension As New LEDExtensions()

            If ExtensionList.ContainsKey(EXTENSION_DEFAULT_KEY) Then
                extension = ExtensionList(EXTENSION_DEFAULT_KEY)
            End If

            UniLED_Edit.Text = Await keyLED_MidiToKeyLEDAsync(Application.StartupPath & "\Workspace\ableproj\CoLED\" & LED_ListView.FocusedItem.Text, False, SpeedTrackBar.Value, 0, extension)
        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.ToString(), Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub KeyLED_Edit_Closing() Handles MyBase.FormClosing, MyBase.Disposed
        keyLED_Test.Enabled = True
        keyLED_Test.Dispose()
    End Sub

    ''' <summary>
    ''' keyLED 기능: MIDI 파일을 keyLED 내용으로 변환해줍니다.
    ''' </summary>
    ''' <param name="autoConvert">자동 변환인지의 여부입니다.</param>
    Public Function keyLED_MidiToKeyLED(filePath As String, autoConvert As Boolean, Optional speed As Integer = 100, Optional tempo As Integer = 120, Optional extension As LEDExtensions = Nothing) As String
        Try
            Dim stopw As New Stopwatch
            stopw.Start()

            Try
                Dim ledMidiFileTest As New MidiFile(filePath, False)
            Catch
                If autoConvert = False Then
                    Invoke(Sub()
                               UniLED_Edit.Enabled = True
                               UniLED_Edit.Clear()
                           End Sub)
                End If
                Return "Error:" & vbNewLine & "This Midi File is something wrong. please select another midi file. We're so sorry about that."
            End Try

            Dim keyLED As New MidiFile(filePath, False)
            Dim ledFileName As String = Path.GetFileName(filePath)

            If autoConvert = False Then
                Invoke(Sub()
                           UniLED_Edit.Enabled = True
                           UniLED_Edit.Clear()
                           UniLED_Edit.Text = "Please Wait..." 'FastColoredTextBox supports only English :(
                           Select Case MainProject.lang
                               Case Translator.tL.English
                                   UniLED1.Text = "File Name: " & Path.GetFileName(filePath)
                               Case Translator.tL.Korean
                                   UniLED1.Text = "파일 이름: " & Path.GetFileName(filePath)
                           End Select
                       End Sub)
            End If

            Dim algItemIndex As Integer = -1
            If autoConvert = False Then
                Invoke(Sub() algItemIndex = ALGModeBox.SelectedIndex)
            Else
                algItemIndex = 0 '자동변환에서는 기본 알고리즘이 Drum Rack Layout이다.
            End If

            If IsNothing(extension) Then
                extension = New LEDExtensions()
            End If

            '이 코드는 Follow_JB님의 midi2keyLED를 참고하여 만든 코드. (Thanks to Follow_JB. :D)

            Dim str As New StringBuilder(100)
            Dim delayCount As Integer = 0
            Dim uniNoteNumberX As Integer 'X
            Dim uniNoteNumberY As Integer 'Y

            For i = 0 To keyLED.Events.Count - 1
                For j = 0 To keyLED.Events(i).Count - 1
                    Dim mdEvent As MidiEvent = keyLED.Events(i)(j)

                    If algItemIndex = 0 Then 'Drum Rack Layout

                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a = DirectCast(mdEvent, NoteOnEvent)
                            Dim bpm As New TempoEvent(500000, a.AbsoluteTime)

                            If Not delayCount = a.AbsoluteTime OrElse Not a.DeltaTime = 0 Then
                                Dim bpmTempo = 0

                                If tempo = 0 Then
                                    bpmTempo = bpm.Tempo
                                Else
                                    bpmTempo = tempo
                                End If

                                str.Append("d ")
                                str.Append(Math.Round(GetNoteDelay(keyLED_NoteEvents.NoteLength_2, bpm.Tempo, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delayCount) * (speed / 100)))
                                str.Append(vbNewLine)
                            End If

                            '기본값
                            uniNoteNumberX = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber)
                            uniNoteNumberY = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber)
                            delayCount = a.AbsoluteTime

                            If uniNoteNumberX = 0 AndAlso uniNoteNumberY = 0 Then
                                Debug.WriteLine("Unknown Note Number. [ Note: " & a.NoteNumber & " ]")
                                Continue For
                            End If

                            'LED 익스텐션
                            If Not extension.Flip.IsEmpty Then
                                extension.Flip.SyncFlip("on", uniNoteNumberX, uniNoteNumberY, a.Velocity, str)
                            End If

                            If uniNoteNumberX <> -8192 AndAlso uniNoteNumberX <> -8193 Then
                                str.Append("o ")
                                str.Append(uniNoteNumberX)
                                str.Append(" ")
                                str.Append(uniNoteNumberY)
                                str.Append(" a ")
                                str.Append(a.Velocity)
                                str.Append(vbNewLine)

                            ElseIf uniNoteNumberX = -8193 Then '로고라이트 및 모드라이트
                                str.Append("o l a ")
                                str.Append(a.Velocity)
                                str.Append(vbNewLine)
                            Else
                                str.Append("o mc ")
                                str.Append(uniNoteNumberY)
                                str.Append(" a ")
                                str.Append(a.Velocity)
                                str.Append(vbNewLine)
                            End If

                        ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                            Dim a = DirectCast(mdEvent, NoteEvent)
                            Dim bpm As New TempoEvent(500000, a.AbsoluteTime)

                            If Not delayCount = a.AbsoluteTime OrElse Not a.DeltaTime = 0 Then
                                Dim bpmTempo = 0

                                If tempo = 0 Then
                                    bpmTempo = bpm.Tempo
                                Else
                                    bpmTempo = tempo
                                End If

                                str.Append("d ")
                                str.Append(Math.Round(GetNoteDelay(keyLED_NoteEvents.NoteLength_2, bpmTempo, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delayCount) * (speed / 100)))
                                str.Append(vbNewLine)
                            End If

                            uniNoteNumberX = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber)
                            uniNoteNumberY = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber)
                            delayCount = a.AbsoluteTime

                            If uniNoteNumberX = 0 AndAlso uniNoteNumberY = 0 Then
                                Debug.WriteLine("Unknown Note Number. [ Note: " & a.NoteNumber & " ]")
                                Continue For
                            End If

                            Dim isFlickering = False '플리커링 문제 (유니컨버터 v1.2.0.6 이전 버전에서 발생하는 문제)

                            If Not j + 1 > keyLED.Events(i).Count - 1 Then
                                Dim nextEvent As MidiEvent = keyLED.Events(i)(j + 1)

                                If nextEvent.CommandCode = MidiCommandCode.NoteOn AndAlso DirectCast(nextEvent, NoteOnEvent).NoteNumber = a.NoteNumber Then
                                    isFlickering = True
                                End If
                            End If

                            If isFlickering = False Then
                                If Not extension.Flip.IsEmpty Then
                                    extension.Flip.SyncFlip("off", uniNoteNumberX, uniNoteNumberY, a.Velocity, str)
                                End If

                                If uniNoteNumberX <> -8192 AndAlso uniNoteNumberX <> -8193 Then
                                    str.Append("f ")
                                    str.Append(uniNoteNumberX)
                                    str.Append(" ")
                                    str.Append(uniNoteNumberY)
                                    str.Append(vbNewLine)

                                ElseIf uniNoteNumberX = -8193 Then '로고라이트 및 모드라이트
                                    str.Append("f l")
                                    str.Append(vbNewLine)
                                Else
                                    str.Append("f mc ")
                                    str.Append(uniNoteNumberY)
                                    str.Append(vbNewLine)
                                End If
                            End If
                        End If

                    ElseIf algItemIndex = 1 Then 'Developer Mode
#Region "Non-Convert (Developer Mode)"
                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a = DirectCast(mdEvent, NoteOnEvent)

                            If Not delayCount = a.AbsoluteTime Then
                                str.Append("delay ")
                                str.Append(a.AbsoluteTime - delayCount)
                                str.Append(vbNewLine)
                            End If

                            delayCount = a.AbsoluteTime
                            str.Append("on ")
                            str.Append(a.NoteNumber)
                            str.Append(" auto ")
                            str.Append(a.Velocity)
                            str.Append(vbNewLine)

                        ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                            Dim a = DirectCast(mdEvent, NoteEvent)

                            If Not delayCount = a.AbsoluteTime Then
                                str.Append("delay ")
                                str.Append(a.AbsoluteTime - delayCount)
                                str.Append(vbNewLine)
                            End If

                            str.Append("off ")
                            str.Append(a.NoteNumber)
                            str.Append(vbNewLine)
                        End If
#End Region

                    Else
                        Select Case MainProject.lang
                            Case Translator.tL.English
                                MessageBox.Show("You have to select the 'Ableton Live MIDI ALG1' or 'Non-Convert (Developer Mode)'.'", Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Case Translator.tL.Korean
                                MessageBox.Show("알고리즘은 에이블톤 라이브 미디 1'이나 '원본 (제작자 모드)'를 선택해야 합니다.", Me.Text & ": 오류", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Select
                        Return String.Empty
                    End If
                Next
            Next

            If str.Length > 0 Then
                str.Length -= 1 'New Line 제거
            End If

            If autoConvert = False Then
                Invoke(Sub()
                           UniLED_Edit.Text = str.ToString()
                           TestButton.Enabled = True
                           keyLED_Test.Enabled = True
                           CopyButton.Enabled = True
                           LEDExButton.Enabled = True
                           keyLED_Edit_Ex.Enabled = True
                       End Sub)

                _uniText = str.ToString()
                CanEnable = True 'Enabled to Test the LED.
                keyLED_Test.LoadkeyLEDText(str.ToString())
                Dim LEDDelay As Integer = GetLEDDelay(str.ToString())

                Invoke(Sub()
                           Select Case MainProject.lang
                               Case Translator.tL.English
                                   TimeLabel.Text = "LED Running Time: " & LEDDelay & "ms"
                               Case Translator.tL.Korean
                                   TimeLabel.Text = "LED Delay 시간: " & LEDDelay & "ms"
                           End Select
                       End Sub)

                stopw.Stop()
                Debug.WriteLine(String.Format("'{0}' Elapsed Time: {1}ms", Path.GetFileName(filePath), stopw.ElapsedMilliseconds))
            End If

            If WaitForSpeed Then
                WaitForSpeed = False
                Return keyLED_SpeedChanged(speed)
            End If

            Return str.ToString()

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

        Return String.Empty
    End Function

    Public Async Function keyLED_MidiToKeyLEDAsync(filePath As String, autoConvert As Boolean, speed As Integer, tempo As Integer, extension As LEDExtensions) As Task(Of String)
        Return Await Task.Run(Function() keyLED_MidiToKeyLED(filePath, autoConvert, speed, tempo, extension))
    End Function

    Private Sub TestButton_Click(sender As Object, e As EventArgs) Handles TestButton.Click
        If CanEnable Then
            keyLED_Test.Show()
            keyLED_Test.LoadkeyLEDText(UniLED_Edit.Text)
        Else
            Select Case MainProject.lang
                Case Translator.tL.English
                    MessageBox.Show("You have to convert the LED first!" & vbNewLine & "Please wait...", Me.Text & ": Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Case Translator.tL.Korean
                    MessageBox.Show("먼저 LED를 변환해야 합니다!" & vbNewLine & "잠시만 기다려주세요...", Me.Text & ": 경고", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Select
        End If
    End Sub

    Public Function GetLEDDelay(LEDText As String) As Integer
        Dim delayMilliseconds As Integer = 0
        Dim textSp As String() = MainProject.SplitbyLine(LEDText)

        For i As Integer = 0 To textSp.Length - 1
            Dim sp As String() = textSp(i).Split(" ")
            If String.IsNullOrWhiteSpace(textSp(i)) AndAlso sp.Count < 2 Then
                Continue For
            End If
            If sp(0) = "d" AndAlso IsNumeric(sp(1)) Then
                delayMilliseconds += Integer.Parse(sp(1))
            Else
                Continue For
            End If
        Next

        Return delayMilliseconds
    End Function

    Private Sub SpeedTrackBar_Scroll(sender As Object, e As EventArgs) Handles SpeedTrackBar.Scroll
        spLb.Text = String.Format("{0}%", SpeedTrackBar.Value)
    End Sub

    Private Sub SpeedTrackBar_ValueChanged(sender As Object, e As EventArgs) Handles SpeedTrackBar.MouseUp
        If CanEnable Then
            ThreadPool.QueueUserWorkItem(AddressOf keyLED_SpeedChanged, SpeedTrackBar.Value)
        Else
            WaitForSpeed = True
        End If
    End Sub

    Public Function keyLED_SpeedChanged(speed As Integer) As String
        Dim orText As String = _uniText
        Dim rText As StringBuilder = New StringBuilder(100)
        Dim NotWhSp As Boolean = True

        If String.IsNullOrWhiteSpace(orText) Then
            Select Case MainProject.lang
                Case Translator.tL.English
                    MessageBox.Show("Please Convert the LED First!", Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Case Translator.tL.Korean
                    MessageBox.Show("먼저 LED를 변환 해주세요!", Me.Text & ": 오류", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Select
        End If

        CanEnable = False
        Invoke(Sub()
                   TestButton.Enabled = False
                   keyLED_Test.Enabled = False
                   CopyButton.Enabled = False

                   With Loading
                       .Show()
                       Select Case MainProject.lang
                           Case Translator.tL.English
                               .Text = "LED Speed Changing..."
                               .DLb.Text = "Please Wait..."
                           Case Translator.tL.Korean
                               .Text = "LED 빠르기 바꾸는 중..."
                               .DLb.Text = "잠시만 기다려주세요..."
                       End Select
                   End With
               End Sub)

        Dim textSp As String() = MainProject.SplitbyLine(orText)
        For i As Integer = 0 To textSp.Length - 1
            Dim sp As String() = textSp(i).Split(" ")
            If sp(0) = "d" Then
                Dim d As Integer = Integer.Parse(sp(1))
                sp(1) = Math.Round(d * (speed / 100))

#Region "NewLine"
                If NotWhSp = True Then
                    NotWhSp = False
                Else
                    rText.Append(vbNewLine)
                End If
#End Region
                rText.Append(sp(0))
                rText.Append(" ")
                rText.Append(sp(1))
            Else
#Region "NewLine"
                If NotWhSp = True Then
                    NotWhSp = False
                Else
                    rText.Append(vbNewLine)
                End If
#End Region
                rText.Append(textSp(i))
            End If
        Next

        Invoke(Sub()
                   UniLED_Edit.Text = rText.ToString()
                   TestButton.Enabled = True
                   keyLED_Test.Enabled = True
                   CopyButton.Enabled = True
               End Sub)
        CanEnable = True 'Enabled to Test the LED.
        keyLED_Test.LoadkeyLEDText(UniLED_Edit.Text)
        Dim LEDDelay As Integer = GetLEDDelay(UniLED_Edit.Text)
        Invoke(Sub()
                   Select Case MainProject.lang
                       Case Translator.tL.English
                           TimeLabel.Text = "LED Running Time: " & LEDDelay & "ms"
                       Case Translator.tL.Korean
                           TimeLabel.Text = "LED를 실행한 시간: " & LEDDelay & "ms"
                   End Select
               End Sub)

        Invoke(Sub() Loading.Dispose())
        Return rText.ToString()
    End Function

    Private Sub LEDExButton_Click(sender As Object, e As EventArgs) Handles LEDExButton.Click
        keyLED_Edit_Ex.Show()
    End Sub
End Class

Public Enum Plugins
    MidiExtension '1.0
    MidiExt '2.0 이상
    MidiFire
    Lightweight
    Flip
    None
End Enum
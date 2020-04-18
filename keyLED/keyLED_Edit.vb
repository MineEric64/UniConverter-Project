Imports System.IO
Imports NAudio.Midi
Imports A2UP.A2U.keyLED_MIDEX
Imports UniConverter.keyLED_Edit_Ex
Imports System.Threading
Imports System.Text

Public Class keyLED_Edit
    Public CanEnable As Boolean = False
    Public WaitForSpeed As Boolean = False
    Private UniText As String

    Public Ex_Default As New List(Of Nullable)
    Public Ex_Flip As New List(Of FlipStructure)
    Public Ex_Color As New List(Of Nullable)

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

            UniLED_Edit.Text = Await keyLED_MidiToKeyLEDAsync(Application.StartupPath & "\Workspace\ableproj\CoLED\" & LED_ListView.FocusedItem.Text, False, SpeedTrackBar.Value, 0)
        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    ''' keyLED 기능: MIDI 파일을 keyLED 내용으로 변환해줍니다. 미디 익스텐션만 가능
    ''' </summary>
    ''' <param name="AutoConvert">자동 변환인지의 여부입니다.</param>
    Public Function keyLED_MidiToKeyLED(FilePath As String, AutoConvert As Boolean, speed As Integer, tempo As Integer) As String
        Try
            Dim notWhSp As Boolean = True

            'Beta Code!
            '이 Beta Convert Code는 오류가 발생할 수 있습니다.
            '주의사항을 다 보셨다면, 당신은 Editor 권한을 가질 수 있습니다.

            '변환 코드...            
            'V1.0.0.1 ~ V1.0.0.2 - String.Replace로 이용한 ConLED 파일 표시: ConLEDFile '[ConLEDFile String 계산: ":FileName.Ext"]
            'V1.1.0.3 - Item.ToString을 Item.Text로 코드 최적화.

            Dim stopw As New Stopwatch
            stopw.Start()

            Dim keyLED As New MidiFile(FilePath, False)

            If AutoConvert = False Then
                Invoke(Sub()
                           UniLED_Edit.Enabled = True
                           UniLED_Edit.Clear()
                           UniLED_Edit.Text = "Please Wait..." 'FastColoredTextBox supports only English :(
                           Select Case MainProject.lang
                               Case Translator.tL.English
                                   UniLED1.Text = "File Name: " & Path.GetFileName(FilePath)
                               Case Translator.tL.Korean
                                   UniLED1.Text = "파일 이름: " & Path.GetFileName(FilePath)
                           End Select
                       End Sub)
            End If

            Dim ALGItemIndex As Integer = -1
            If AutoConvert = False Then
                Invoke(Sub() ALGItemIndex = ALGModeBox.SelectedIndex)
            Else
                ALGItemIndex = 0 '자동변환에서는 기본 알고리즘이 Drum Rack Layout이다.
            End If

            '이 코드는 Follow_JB님의 midi2keyLED를 참고하여 만든 코드. (Thanks to Follow_JB. :D)

            Dim str As StringBuilder = New StringBuilder(100)
            Dim delaycount As Integer = 0
            Dim UniNoteNumberX As Integer 'X
            Dim UniNoteNumberY As Integer 'Y

            For i As Integer = 0 To keyLED.Events.Count - 1
                For j As Integer = 0 To keyLED.Events(i).Count - 1
                    Dim mdEvent As MidiEvent = keyLED.Events(i)(j)

                    If ALGItemIndex = 0 Then 'Drum Rack Layout

                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a As NoteOnEvent = DirectCast(mdEvent, NoteOnEvent)
                            Dim bpm As New TempoEvent(500000, a.AbsoluteTime)

                            If Not delaycount = a.AbsoluteTime OrElse Not a.DeltaTime = 0 Then
                                Dim bpmTempo As Integer = 0

                                If tempo = 0 Then
                                    bpmTempo = bpm.Tempo
                                Else
                                    bpmTempo = tempo
                                End If
#Region "NewLine"
                                If notWhSp = True Then
                                    notWhSp = False
                                Else
                                    str.Append(vbNewLine)
                                End If
#End Region
                                str.Append("d ")
                                str.Append(Math.Round(GetNoteDelay(keyLED_NoteEvents.NoteLength_2, bpm.Tempo, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delaycount) * (speed / 100)))
                            End If

                            '기본값
                            UniNoteNumberX = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber)
                            UniNoteNumberY = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber)
                            delaycount = a.AbsoluteTime

                            If UniNoteNumberX = 0 AndAlso UniNoteNumberY = 0 Then
                                Debug.WriteLine("Unknown Note Number. [ Note: " & a.NoteNumber & " ]")
                                Continue For
                            End If

                            'LED 익스텐션
                            If Not Ex_Flip.Count = 0 Then
#Region "Flip 익스텐션"
                                If IsAutoLoaded = False Then
                                    Dim FlipStructure_ As FlipStructure = Ex_Flip(0)

                                    If FlipStructure_.Mirror = Mirror.Horizontal Then 'Mirror
                                        If UniNoteNumberX = -8192 Then
                                            UniNoteNumberY = keyLED_Edit_Ex.Flip_Mirror_Horizontal_MC(UniNoteNumberY)
                                        ElseIf Not UniNoteNumberX = -8193 Then
                                            UniNoteNumberY = 9 - UniNoteNumberY
                                        End If
                                    ElseIf FlipStructure_.Mirror = Mirror.Vertical Then
                                        If UniNoteNumberX = -8192 Then
                                            UniNoteNumberY = keyLED_Edit_Ex.Flip_Mirror_Vertical_MC(UniNoteNumberY)
                                        ElseIf Not UniNoteNumberX = -8193 Then
                                            UniNoteNumberX = 9 - UniNoteNumberX
                                        End If
                                    End If

                                    If FlipStructure_.Rotate = 90 Then 'Rotate
                                        If UniNoteNumberX = -8192 Then
                                            UniNoteNumberY = keyLED_Edit_Ex.Flip_Rotate_90_MC(UniNoteNumberY)
                                        ElseIf Not UniNoteNumberX = -8193 Then
                                            Dim xValue As Flip_Rotate_XYReturn = keyLED_Edit_Ex.Flip_Rotate_90(UniNoteNumberX & UniNoteNumberY)
                                            UniNoteNumberX = xValue.x
                                            UniNoteNumberY = xValue.y
                                        End If
                                    ElseIf FlipStructure_.Rotate = 180 Then '좀 난감한 스파게티 코드...
                                        If UniNoteNumberX = -8192 Then
                                            UniNoteNumberY = keyLED_Edit_Ex.Flip_Rotate_90_MC(keyLED_Edit_Ex.Flip_Rotate_90_MC(UniNoteNumberY))
                                        ElseIf Not UniNoteNumberX = -8193 Then
                                            Dim xValue As Flip_Rotate_XYReturn = keyLED_Edit_Ex.Flip_Rotate_90(keyLED_Edit_Ex.Flip_Rotate_90(UniNoteNumberX & UniNoteNumberY).x & keyLED_Edit_Ex.Flip_Rotate_90(UniNoteNumberX & UniNoteNumberY).y)
                                            UniNoteNumberX = xValue.x
                                            UniNoteNumberY = xValue.y
                                        End If
                                    ElseIf FlipStructure_.Rotate = 270 Then
                                        If UniNoteNumberX = -8192 Then
                                            UniNoteNumberY = keyLED_Edit_Ex.Flip_Rotate_270_MC(UniNoteNumberY)
                                        ElseIf Not UniNoteNumberX = -8193 Then
                                            Dim xValue As Flip_Rotate_XYReturn = keyLED_Edit_Ex.Flip_Rotate_270(UniNoteNumberX & UniNoteNumberY)
                                            UniNoteNumberX = xValue.x
                                            UniNoteNumberY = xValue.y
                                        End If
                                    End If

                                    If FlipStructure_.Duplicate = True Then 'Duplicate
                                        If FlipStructure_.Mirror <> Mirror.None OrElse FlipStructure_.Rotate <> 0 Then
                                            If Not UniNoteNumberX = -8192 Then
#Region "NewLine"
                                                If notWhSp = True Then
                                                    notWhSp = False
                                                Else
                                                    str.Append(vbNewLine)
                                                End If
#End Region
                                                str.Append("o ")
                                                str.Append(GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber))
                                                str.Append(" ")
                                                str.Append(GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber))
                                                str.Append(" a ")
                                                str.Append(a.Velocity)
                                            ElseIf Not UniNoteNumberX = -8193 Then
#Region "NewLine"
                                                If notWhSp = True Then
                                                    notWhSp = False
                                                Else
                                                    str.Append(vbNewLine)
                                                End If
#End Region
                                                str.Append("o mc ")
                                                str.Append(GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber))
                                                str.Append(" a ")
                                                str.Append(a.Velocity)
                                            End If
                                        End If
                                    End If
                                Else
                                    Dim LEDListView_SelectedIndex As Integer = -1
                                    Invoke(Sub() LEDListView_SelectedIndex = LED_ListView.SelectedItems(0).Index)
                                    Dim FlipStructure_ As FlipStructure = Ex_Flip(LEDListView_SelectedIndex)

                                    If FlipStructure_.Mirror = Mirror.Horizontal Then 'Mirror
                                        If UniNoteNumberX = -8192 Then
                                            UniNoteNumberY = keyLED_Edit_Ex.Flip_Mirror_Horizontal_MC(UniNoteNumberY)
                                        ElseIf Not UniNoteNumberX = -8193 Then
                                            UniNoteNumberY = 9 - UniNoteNumberY
                                        End If
                                    ElseIf FlipStructure_.Mirror = Mirror.Vertical Then
                                        If UniNoteNumberX = -8192 Then
                                            UniNoteNumberY = keyLED_Edit_Ex.Flip_Mirror_Vertical_MC(UniNoteNumberY)
                                        ElseIf Not UniNoteNumberX = -8193 Then
                                            UniNoteNumberX = 9 - UniNoteNumberX
                                        End If
                                    End If

                                    If FlipStructure_.Rotate = 90 Then 'Rotate
                                        If UniNoteNumberX = -8192 Then
                                            UniNoteNumberY = keyLED_Edit_Ex.Flip_Rotate_90_MC(UniNoteNumberY)
                                        ElseIf Not UniNoteNumberX = -8193 Then
                                            Dim xValue As Flip_Rotate_XYReturn = keyLED_Edit_Ex.Flip_Rotate_90(UniNoteNumberX & UniNoteNumberY)
                                            UniNoteNumberX = xValue.x
                                            UniNoteNumberY = xValue.y
                                        End If
                                    ElseIf FlipStructure_.Rotate = 180 Then '좀 난감한 스파게티 코드...
                                        If UniNoteNumberX = -8192 Then
                                            UniNoteNumberY = keyLED_Edit_Ex.Flip_Rotate_90_MC(keyLED_Edit_Ex.Flip_Rotate_90_MC(UniNoteNumberY))
                                        ElseIf Not UniNoteNumberX = -8193 Then
                                            Dim xValue As Flip_Rotate_XYReturn = keyLED_Edit_Ex.Flip_Rotate_90(keyLED_Edit_Ex.Flip_Rotate_90(UniNoteNumberX & UniNoteNumberY).x & keyLED_Edit_Ex.Flip_Rotate_90(UniNoteNumberX & UniNoteNumberY).y)
                                            UniNoteNumberX = xValue.x
                                            UniNoteNumberY = xValue.y
                                        End If
                                    ElseIf FlipStructure_.Rotate = 270 Then
                                        If UniNoteNumberX = -8192 Then
                                            UniNoteNumberY = keyLED_Edit_Ex.Flip_Rotate_270_MC(UniNoteNumberY)
                                        ElseIf Not UniNoteNumberX = -8193 Then
                                            Dim xValue As Flip_Rotate_XYReturn = keyLED_Edit_Ex.Flip_Rotate_270(UniNoteNumberX & UniNoteNumberY)
                                            UniNoteNumberX = xValue.x
                                            UniNoteNumberY = xValue.y
                                        End If
                                    End If

                                    If FlipStructure_.Duplicate = True Then 'Duplicate
                                        If FlipStructure_.Mirror <> Mirror.None OrElse FlipStructure_.Rotate <> 0 Then
                                            If Not UniNoteNumberX = -8192 Then
#Region "NewLine"
                                                If notWhSp = True Then
                                                    notWhSp = False
                                                Else
                                                    str.Append(vbNewLine)
                                                End If
#End Region
                                                str.Append("o ")
                                                str.Append(GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber))
                                                str.Append(" ")
                                                str.Append(GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber))
                                                str.Append(" a ")
                                                str.Append(a.Velocity)
                                            ElseIf Not UniNoteNumberX = -8193 Then
#Region "NewLine"
                                                If notWhSp = True Then
                                                    notWhSp = False
                                                Else
                                                    str.Append(vbNewLine)
                                                End If
#End Region
                                                str.Append("o mc ")
                                                str.Append(GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber))
                                                str.Append(" a ")
                                                str.Append(a.Velocity)
                                            End If
                                        End If
                                    End If
#End Region
                                End If
                            End If

                            If Not UniNoteNumberX = -8192 AndAlso Not UniNoteNumberX = -8193 Then
#Region "set str var"
#Region "NewLine"
                                If notWhSp = True Then
                                    notWhSp = False
                                Else
                                    str.Append(vbNewLine)
                                End If
#End Region
                                str.Append("o ")
                                str.Append(UniNoteNumberX)
                                str.Append(" ")
                                str.Append(UniNoteNumberY)
                                str.Append(" a ")
                                str.Append(a.Velocity)
#End Region
                            ElseIf UniNoteNumberX = -8193 Then '로고라이트 및 모드라이트
#Region "set str var"
#Region "NewLine"
                                If notWhSp = True Then
                                    notWhSp = False
                                Else
                                    str.Append(vbNewLine)
                                End If
#End Region
                                str.Append("o l a ")
                                str.Append(a.Velocity)
#End Region
                            Else
#Region "set str var"
#Region "NewLine"
                                If notWhSp = True Then
                                    notWhSp = False
                                Else
                                    str.Append(vbNewLine)
                                End If
#End Region
                                str.Append("o mc ")
                                str.Append(UniNoteNumberY)
                                str.Append(" a ")
                                str.Append(a.Velocity)
#End Region
                            End If

                        ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                            Dim a As NoteEvent = DirectCast(mdEvent, NoteEvent)
                            Dim bpm As New TempoEvent(500000, a.AbsoluteTime)

                            If Not delaycount = a.AbsoluteTime OrElse Not a.DeltaTime = 0 Then
                                Dim bpmTempo As Integer = 0
                                If tempo = 0 Then
                                    bpmTempo = bpm.Tempo
                                Else
                                    bpmTempo = tempo
                                End If
#Region "NewLine"
                                If notWhSp = True Then
                                    notWhSp = False
                                Else
                                    str.Append(vbNewLine)
                                End If
#End Region
                                str.Append("d ")
                                str.Append(Math.Round(GetNoteDelay(keyLED_NoteEvents.NoteLength_2, bpmTempo, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delaycount) * (speed / 100)))
                            End If

                            UniNoteNumberX = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber)
                            UniNoteNumberY = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber)
                            delaycount = a.AbsoluteTime

                            If UniNoteNumberX = 0 AndAlso UniNoteNumberY = 0 Then
                                Debug.WriteLine("Unknown Note Number. [ Note: " & a.NoteNumber & " ]")
                                Continue For
                            End If

                            Dim IsFlickering As Boolean = False '플리커링 문제 (유니컨버터 v1.2.0.6 이전 버전에서 발생하는 문제)
                            If Not j + 1 > keyLED.Events(i).Count - 1 Then
                                Dim nextEvent As MidiEvent = keyLED.Events(i)(j + 1)
                                If nextEvent.CommandCode = MidiCommandCode.NoteOn AndAlso DirectCast(nextEvent, NoteOnEvent).NoteNumber = a.NoteNumber Then
                                    IsFlickering = True
                                End If
                            End If

                            If IsFlickering = False Then
                                'LED 익스텐션
                                If Not Ex_Flip.Count = 0 Then
#Region "Flip 익스텐션"
                                    If IsAutoLoaded = False Then
                                        Dim FlipStructure_ As FlipStructure = Ex_Flip(0)

                                        If FlipStructure_.Mirror = Mirror.Horizontal Then 'Mirror
                                            If UniNoteNumberX = -8192 Then
                                                UniNoteNumberY = keyLED_Edit_Ex.Flip_Mirror_Horizontal_MC(UniNoteNumberY)
                                            ElseIf Not UniNoteNumberX = -8193 Then
                                                UniNoteNumberY = 9 - UniNoteNumberY
                                            End If
                                        ElseIf FlipStructure_.Mirror = Mirror.Vertical Then
                                            If UniNoteNumberX = -8192 Then
                                                UniNoteNumberY = keyLED_Edit_Ex.Flip_Mirror_Vertical_MC(UniNoteNumberY)
                                            ElseIf Not UniNoteNumberX = -8193 Then
                                                UniNoteNumberX = 9 - UniNoteNumberX
                                            End If
                                        End If

                                        If FlipStructure_.Rotate = 90 Then 'Rotate
                                            If UniNoteNumberX = -8192 Then
                                                UniNoteNumberY = keyLED_Edit_Ex.Flip_Rotate_90_MC(UniNoteNumberY)
                                            ElseIf Not UniNoteNumberX = -8193 Then
                                                Dim xValue As Flip_Rotate_XYReturn = keyLED_Edit_Ex.Flip_Rotate_90(UniNoteNumberX & UniNoteNumberY)
                                                UniNoteNumberX = xValue.x
                                                UniNoteNumberY = xValue.y
                                            End If
                                        ElseIf FlipStructure_.Rotate = 180 Then '좀 난감한 스파게티 코드...
                                            If UniNoteNumberX = -8192 Then
                                                UniNoteNumberY = keyLED_Edit_Ex.Flip_Rotate_90_MC(keyLED_Edit_Ex.Flip_Rotate_90_MC(UniNoteNumberY))
                                            ElseIf Not UniNoteNumberX = -8193 Then
                                                Dim xValue As Flip_Rotate_XYReturn = keyLED_Edit_Ex.Flip_Rotate_90(keyLED_Edit_Ex.Flip_Rotate_90(UniNoteNumberX & UniNoteNumberY).x & keyLED_Edit_Ex.Flip_Rotate_90(UniNoteNumberX & UniNoteNumberY).y)
                                                UniNoteNumberX = xValue.x
                                                UniNoteNumberY = xValue.y
                                            End If
                                        ElseIf FlipStructure_.Rotate = 270 Then
                                            If UniNoteNumberX = -8192 Then
                                                UniNoteNumberY = keyLED_Edit_Ex.Flip_Rotate_270_MC(UniNoteNumberY)
                                            ElseIf Not UniNoteNumberX = -8193 Then
                                                Dim xValue As Flip_Rotate_XYReturn = keyLED_Edit_Ex.Flip_Rotate_270(UniNoteNumberX & UniNoteNumberY)
                                                UniNoteNumberX = xValue.x
                                                UniNoteNumberY = xValue.y
                                            End If
                                        End If

                                        If FlipStructure_.Duplicate = True Then 'Duplicate
                                            If FlipStructure_.Mirror <> Mirror.None OrElse FlipStructure_.Rotate <> 0 Then
                                                If Not UniNoteNumberX = -8192 Then
#Region "NewLine"
                                                    If notWhSp = True Then
                                                        notWhSp = False
                                                    Else
                                                        str.Append(vbNewLine)
                                                    End If
#End Region
                                                    str.Append("f ")
                                                    str.Append(GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber))
                                                    str.Append(" ")
                                                    str.Append(GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber))
                                                ElseIf Not UniNoteNumberX = -8193 Then
#Region "NewLine"
                                                    If notWhSp = True Then
                                                        notWhSp = False
                                                    Else
                                                        str.Append(vbNewLine)
                                                    End If
#End Region
                                                    str.Append("f mc ")
                                                    str.Append(GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber))
                                                End If
                                            End If
                                        End If
                                    Else
                                        Dim LEDListView_SelectedIndex As Integer = -1
                                        Invoke(Sub() LEDListView_SelectedIndex = LED_ListView.SelectedItems(0).Index)
                                        Dim FlipStructure_ As FlipStructure = Ex_Flip(LEDListView_SelectedIndex)

                                        If FlipStructure_.Mirror = Mirror.Horizontal Then 'Mirror
                                            If UniNoteNumberX = -8192 Then
                                                UniNoteNumberY = keyLED_Edit_Ex.Flip_Mirror_Horizontal_MC(UniNoteNumberY)
                                            ElseIf Not UniNoteNumberX = -8193 Then
                                                UniNoteNumberY = 9 - UniNoteNumberY
                                            End If
                                        ElseIf FlipStructure_.Mirror = Mirror.Vertical Then
                                            If UniNoteNumberX = -8192 Then
                                                UniNoteNumberY = keyLED_Edit_Ex.Flip_Mirror_Vertical_MC(UniNoteNumberY)
                                            ElseIf Not UniNoteNumberX = -8193 Then
                                                UniNoteNumberX = 9 - UniNoteNumberX
                                            End If
                                        End If

                                        If FlipStructure_.Rotate = 90 Then 'Rotate
                                            If UniNoteNumberX = -8192 Then
                                                UniNoteNumberY = keyLED_Edit_Ex.Flip_Rotate_90_MC(UniNoteNumberY)
                                            ElseIf Not UniNoteNumberX = -8193 Then
                                                Dim xValue As Flip_Rotate_XYReturn = keyLED_Edit_Ex.Flip_Rotate_90(UniNoteNumberX & UniNoteNumberY)
                                                UniNoteNumberX = xValue.x
                                                UniNoteNumberY = xValue.y
                                            End If
                                        ElseIf FlipStructure_.Rotate = 180 Then '좀 난감한 스파게티 코드...
                                            If UniNoteNumberX = -8192 Then
                                                UniNoteNumberY = keyLED_Edit_Ex.Flip_Rotate_90_MC(keyLED_Edit_Ex.Flip_Rotate_90_MC(UniNoteNumberY))
                                            ElseIf Not UniNoteNumberX = -8193 Then
                                                Dim xValue As Flip_Rotate_XYReturn = keyLED_Edit_Ex.Flip_Rotate_90(keyLED_Edit_Ex.Flip_Rotate_90(UniNoteNumberX & UniNoteNumberY).x & keyLED_Edit_Ex.Flip_Rotate_90(UniNoteNumberX & UniNoteNumberY).y)
                                                UniNoteNumberX = xValue.x
                                                UniNoteNumberY = xValue.y
                                            End If
                                        ElseIf FlipStructure_.Rotate = 270 Then
                                            If UniNoteNumberX = -8192 Then
                                                UniNoteNumberY = keyLED_Edit_Ex.Flip_Rotate_270_MC(UniNoteNumberY)
                                            ElseIf Not UniNoteNumberX = -8193 Then
                                                Dim xValue As Flip_Rotate_XYReturn = keyLED_Edit_Ex.Flip_Rotate_270(UniNoteNumberX & UniNoteNumberY)
                                                UniNoteNumberX = xValue.x
                                                UniNoteNumberY = xValue.y
                                            End If
                                        End If

                                        If FlipStructure_.Duplicate = True Then 'Duplicate
                                            If FlipStructure_.Mirror <> Mirror.None OrElse FlipStructure_.Rotate <> 0 Then
                                                If Not UniNoteNumberX = -8192 Then
#Region "NewLine"
                                                    If notWhSp = True Then
                                                        notWhSp = False
                                                    Else
                                                        str.Append(vbNewLine)
                                                    End If
#End Region
                                                    str.Append("f ")
                                                    str.Append(GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber))
                                                    str.Append(" ")
                                                    str.Append(GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber))
                                                ElseIf Not UniNoteNumberX = -8193 Then
#Region "NewLine"
                                                    If notWhSp = True Then
                                                        notWhSp = False
                                                    Else
                                                        str.Append(vbNewLine)
                                                    End If
#End Region
                                                    str.Append("f mc ")
                                                    str.Append(GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber))
                                                End If
                                            End If
                                        End If
#End Region
                                    End If
                                End If

                                If Not UniNoteNumberX = -8192 AndAlso Not UniNoteNumberX = -8193 Then
#Region "set str var"
#Region "NewLine"
                                    If notWhSp = True Then
                                        notWhSp = False
                                    Else
                                        str.Append(vbNewLine)
                                    End If
#End Region
                                    str.Append("f ")
                                    str.Append(UniNoteNumberX)
                                    str.Append(" ")
                                    str.Append(UniNoteNumberY)
#End Region
                                ElseIf UniNoteNumberX = -8193 Then '로고라이트 및 모드라이트
#Region "set str var"
#Region "NewLine"
                                    If notWhSp = True Then
                                        notWhSp = False
                                    Else
                                        str.Append(vbNewLine)
                                    End If
#End Region
                                    str.Append("f l")
#End Region
                                Else
#Region "set str var"
#Region "NewLine"
                                    If notWhSp = True Then
                                        notWhSp = False
                                    Else
                                        str.Append(vbNewLine)
                                    End If
#End Region
                                    str.Append("f mc ")
                                    str.Append(UniNoteNumberY)
#End Region
                                End If
                            End If
                        End If

                    ElseIf ALGItemIndex = 1 Then 'Developer Mode
#Region "Non-Convert (Developer Mode)"
                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a As NoteOnEvent = DirectCast(mdEvent, NoteOnEvent)

                            If Not delaycount = a.AbsoluteTime Then
                                str.Append(vbNewLine)
                                str.Append("d ")
                                str.Append(a.AbsoluteTime - delaycount)
                            End If

                            delaycount = a.AbsoluteTime
                            str.Append(vbNewLine)
                            str.Append("o ")
                            str.Append(a.NoteNumber)
                            str.Append(" a ")
                            str.Append(a.Velocity)

                        ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                            Dim a As NoteEvent = DirectCast(mdEvent, NoteEvent)

                            If Not delaycount = a.AbsoluteTime Then
                                str.Append(vbNewLine)
                                str.Append("d ")
                                str.Append(a.AbsoluteTime - delaycount)
                            End If

                            str.Append("f ")
                            str.Append(a.NoteNumber)

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

            If AutoConvert = False Then
                Invoke(Sub()
                           UniLED_Edit.Text = str.ToString()
                           TestButton.Enabled = True
                           keyLED_Test.Enabled = True
                           CopyButton.Enabled = True
                           LEDExButton.Enabled = True
                           keyLED_Edit_Ex.Enabled = True
                       End Sub)
                UniText = str.ToString()
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
                Debug.WriteLine(String.Format("'{0}' Elapsed Time: {1}ms", Path.GetFileName(FilePath), stopw.ElapsedMilliseconds))
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

    Public Async Function keyLED_MidiToKeyLEDAsync(FilePath As String, AutoConvert As Boolean, speed As Integer, tempo As Integer) As Task(Of String)
        Return Await Task.Run(Function() keyLED_MidiToKeyLED(FilePath, AutoConvert, speed, tempo))
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
        Dim orText As String = UniText
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
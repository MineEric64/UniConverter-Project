Imports System.IO
Imports NAudio.Midi
Imports A2UP.A2U.keyLED_MIDEX
Imports System.Threading

Public Class keyLED_Edit
    Public CanEnable As Boolean = False
    Private UniText As String

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
                Text = "keyLED (미디 익스텐션) 편집 (베타)"
                AblLED.Text = "에이블톤 LED 파일"
                UniLED.Text = "유니팩 LED 텍스트"
                UniLED1.Text = "파일 이름: N/A"
                spTipLb.Text = "빠르기:"

                LED_ListView.Columns(0).Text = "파일 이름"
                ALGModeBox.Items.Clear()
                ALGModeBox.Items.AddRange({"에이블톤 라이브 미디 1", "원본 (제작자 모드)"})
                ALGModeBox.Text = "에이블톤 라이브 미디 1"

                TestButton.Text = "테스트"
                CopyButton.Text = "복사"
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

    Private Sub GazuaButton_Click(sender As Object, e As EventArgs) Handles GazuaButton.Click
        Try
            If GAZUA_.IsBusy = False Then
                GAZUA_.RunWorkerAsync()
            ElseIf GAZUA_.IsBusy = True Then
                MessageBox.Show("Please Wait...", Me.Text & ": Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
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

    Private Sub GAZUA__DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles GAZUA_.DoWork
        Try
            Dim ConLEDFile As String = String.Empty
            Dim notWhSp As Boolean = True
            Invoke(Sub() ConLEDFile = LED_ListView.FocusedItem.SubItems.Item(0).Text) '선택한 아이템.

            Invoke(Sub()
                       CanEnable = False
                       TestButton.Enabled = False
                       keyLED_Test.Enabled = False
                       CopyButton.Enabled = False
                   End Sub)

            'Beta Code!
            '이 Beta Convert Code는 오류가 발생할 수 있습니다.
            '주의사항을 다 보셨다면, 당신은 Editor 권한을 가질 수 있습니다.

            '변환 코드...            
            'V1.0.0.1 ~ V1.0.0.2 - String.Replace로 이용한 ConLED 파일 표시: ConLEDFile '[ConLEDFile String 계산: ":FileName.Ext"]
            'V1.1.0.3 - Item.ToString을 Item.Text로 코드 최적화.

            Dim stopw As New Stopwatch
            stopw.Start()

            Dim LEDFileName As String = "Workspace\ableproj\CoLED\" & ConLEDFile
            Dim keyLED As New MidiFile(LEDFileName, False)

            Invoke(Sub()
                       UniLED_Edit.Enabled = True
                       UniLED_Edit.Clear()
                       UniLED_Edit.Text = "Please Wait..." 'FastColoredTextBox supports only English :(
                       Select Case MainProject.lang
                           Case Translator.tL.English
                               UniLED1.Text = "File Name: " & ConLEDFile
                           Case Translator.tL.Korean
                               UniLED1.Text = "파일 이름: " & ConLEDFile
                       End Select
                   End Sub)

            '이 코드는 Follow_JB님의 midi2keyLED를 참고하여 만든 코드. (Thanks to Follow_JB. :D)

            Dim str As String = String.Empty
            Dim delaycount As Integer = 0
            Dim UniNoteNumberX As Integer 'X
            Dim UniNoteNumberY As Integer 'Y

            For Each mdEvent_list In keyLED.Events
                For Each mdEvent In mdEvent_list

                    Dim ALGItem As String = String.Empty
                    Invoke(Sub() ALGItem = ALGModeBox.SelectedItem.ToString())
                    If ALGItem = "Ableton Live MIDI ALG1" OrElse ALGItem = "에이블톤 라이브 미디 1" Then

                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a As NoteOnEvent = DirectCast(mdEvent, NoteOnEvent)
                            Dim bpm As New TempoEvent(500000, a.AbsoluteTime)

                            If Not delaycount = a.AbsoluteTime OrElse Not a.DeltaTime = 0 Then
                                Dim speed As Integer = 100
                                Invoke(Sub() speed = 201 - SpeedTrackBar.Value)
                                str = str & vbNewLine & "d " & Math.Round(GetNoteDelay(keyLED_NoteEvents.NoteLength_2, bpm.Tempo, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delaycount) * (speed / 100))
                            End If

                            UniNoteNumberX = GX_keyLED(keyLED_NoteEvents.NoteNumber_1, a.NoteNumber)
                            UniNoteNumberY = GY_keyLED(keyLED_NoteEvents.NoteNumber_1, a.NoteNumber)
                            delaycount = a.AbsoluteTime

                            If UniNoteNumberX = 0 AndAlso UniNoteNumberY = 0 Then
                                Debug.WriteLine("Unknown Note Number. [ Note: " & a.NoteNumber & " ]")
                                Continue For
                            End If

                            If Not UniNoteNumberX = -8192 Then
                                If notWhSp Then
                                    notWhSp = False
                                    str = "o " & UniNoteNumberX & " " & UniNoteNumberY & " a " & a.Velocity
                                Else
                                    str = str & vbNewLine & "o " & UniNoteNumberX & " " & UniNoteNumberY & " a " & a.Velocity
                                End If
                            Else
                                If notWhSp Then
                                    notWhSp = False
                                    str = "o mc " & UniNoteNumberY & " a " & a.Velocity
                                Else
                                    str = str & vbNewLine & "o mc " & UniNoteNumberY & " a " & a.Velocity
                                End If
                            End If

                        ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                            Dim a As NoteEvent = DirectCast(mdEvent, NoteEvent)
                            Dim bpm As New TempoEvent(500000, a.AbsoluteTime)

                            If Not delaycount = a.AbsoluteTime OrElse Not a.DeltaTime = 0 Then
                                Dim speed As Integer = 100
                                Invoke(Sub() speed = 201 - SpeedTrackBar.Value)
                                str = str & vbNewLine & "d " & Math.Round(GetNoteDelay(keyLED_NoteEvents.NoteLength_2, bpm.Tempo, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delaycount) * (speed / 100))
                            End If

                            UniNoteNumberX = GX_keyLED(keyLED_NoteEvents.NoteNumber_1, a.NoteNumber)
                            UniNoteNumberY = GY_keyLED(keyLED_NoteEvents.NoteNumber_1, a.NoteNumber)
                            delaycount = a.AbsoluteTime

                            If UniNoteNumberX = 0 AndAlso UniNoteNumberY = 0 Then
                                Debug.WriteLine("Unknown Note Number. [ Note: " & a.NoteNumber & " ]")
                                Continue For
                            End If

                            If Not UniNoteNumberX = -8192 Then
                                If notWhSp Then
                                    notWhSp = False
                                    str = "f " & UniNoteNumberX & " " & UniNoteNumberY
                                Else
                                    str = str & vbNewLine & "f " & UniNoteNumberX & " " & UniNoteNumberY
                                End If
                            Else
                                If notWhSp Then
                                    notWhSp = False
                                    str = "f mc " & UniNoteNumberY
                                Else
                                    str = str & vbNewLine & "f mc " & UniNoteNumberY
                                End If
                            End If

                        End If

                    ElseIf ALGItem = "Non-Convert (Developer Mode)" OrElse ALGItem = "원본 (제작자 모드)" Then
#Region "Non-Convert (Developer Mode)"
                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a As NoteOnEvent = DirectCast(mdEvent, NoteOnEvent)

                            If Not delaycount = a.AbsoluteTime Then
                                str = str & vbNewLine & "d " & a.AbsoluteTime - delaycount
                            End If

                            delaycount = a.AbsoluteTime
                            str = str & vbNewLine & "o " & a.NoteNumber & " a " & a.Velocity

                        ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                            Dim a As NoteEvent = DirectCast(mdEvent, NoteEvent)

                            If Not delaycount = a.AbsoluteTime Then
                                str = str & vbNewLine & "d " & a.AbsoluteTime - delaycount
                            End If

                            str = str & vbNewLine & "f " & a.NoteNumber

                        End If
#End Region

                    Else
                        Select Case MainProject.lang
                            Case Translator.tL.English
                                MessageBox.Show("You have to select the 'Ableton Live MIDI ALG1' or 'Non-Convert (Developer Mode)'.'", Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Case Translator.tL.Korean
                                MessageBox.Show("알고리즘은 에이블톤 라이브 미디 1'이나 '원본 (제작자 모드)'를 선택해야 합니다.", Me.Text & ": 오류", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Select
                        e.Cancel = True
                        Exit Sub
                    End If
                Next
            Next

            Invoke(Sub()
                       UniLED_Edit.Text = str
                       TestButton.Enabled = True
                       keyLED_Test.Enabled = True
                       CopyButton.Enabled = True
                   End Sub)
            UniText = str
            CanEnable = True 'Enabled to Test the LED.
            keyLED_Test.LoadkeyLEDText(UniLED_Edit.Text)

            stopw.Stop()
            Debug.WriteLine(String.Format("'{0}' Elapsed Time: {1}ms", ConLEDFile, stopw.ElapsedMilliseconds))

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

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

    Private Sub SpeedTrackBar_Scroll(sender As Object, e As EventArgs) Handles SpeedTrackBar.Scroll
        spLb.Text = String.Format("{0}%", SpeedTrackBar.Value)
    End Sub

    Private Sub SpeedTrackBar_ValueChanged(sender As Object, e As EventArgs) Handles SpeedTrackBar.MouseUp
        ThreadPool.QueueUserWorkItem(AddressOf keyLED_SpeedChanged, 201 - SpeedTrackBar.Value)
    End Sub

    Public Sub keyLED_SpeedChanged(speed As Integer)
        Dim orText As String = UniText
        Dim rText As String = String.Empty
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

        For Each x As String In MainProject.SplitbyLine(orText)
            Dim sp As String() = x.Split(" ")
            If sp(0) = "d" Then
                Dim d As Integer = Integer.Parse(sp(1))
                sp(1) = Math.Round(d * (speed / 100))

                If NotWhSp Then
                    NotWhSp = False
                    rText = sp(0) & " " & sp(1)
                Else
                    rText &= vbNewLine & sp(0) & " " & sp(1)
                End If

            Else
                If NotWhSp Then
                    NotWhSp = False
                    rText = x
                Else
                    rText &= vbNewLine & x
                End If
            End If
        Next

        Invoke(Sub()
                   UniLED_Edit.Text = rText
                   TestButton.Enabled = True
                   keyLED_Test.Enabled = True
                   CopyButton.Enabled = True
               End Sub)
        CanEnable = True 'Enabled to Test the LED.
        keyLED_Test.LoadkeyLEDText(UniLED_Edit.Text)

        Invoke(Sub() Loading.Dispose())
    End Sub
End Class
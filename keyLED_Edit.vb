Imports System.IO
Imports System.Text.RegularExpressions
Imports NAudio.Midi
Imports A2UP.A2U
Imports System.Xml

Public Class keyLED_Edit
    Public CanEnable As Boolean = False
    Public SoGood As String() = New String(1) {}

    Private Sub KeyLED_Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'FileName 표시.
        For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\ableproj\CoLED", FileIO.SearchOption.SearchTopLevelOnly, "*.mid") 'FileName의 파일 찾기
            Invoke(Sub()
                       Dim itm As New ListViewItem(New String() {Path.GetFileName(foundFile), foundFile})
                       LED_ListView.Items.Add(itm)  '파일 이름 추가
                   End Sub)
        Next

        Dim file_ex = Application.StartupPath + "\settings.xml"
        Dim setNode As New XmlDocument
        setNode.Load(file_ex)
        Dim setaNode As XmlNode = setNode.SelectSingleNode("/Settings-XML/keyLED-Adv")

        If setaNode IsNot Nothing Then
            Select Case setaNode.ChildNodes(0).InnerText
                Case "NoteLength"
                    SoGood(0) = "Note Length"
                    SoGood(1) = setaNode.ChildNodes(1).InnerText

                Case "DeltaTime"
                    SoGood(0) = "Delta Time"
                    SoGood(1) = setaNode.ChildNodes(2).InnerText

                Case "AbsoluteTime"
                    SoGood(0) = "Absolute Time"
                    SoGood(1) = setaNode.ChildNodes(3).InnerText
            End Select

            setaNode = setNode.SelectSingleNode("/Settings-XML/UCV-PATH")
            CleaningButton.Enabled = Boolean.Parse(setaNode.ChildNodes(2).InnerText)
        End If

    End Sub

    Private Sub CopyButton_Click(sender As Object, e As EventArgs) Handles CopyButton.Click
        '복사 코드.
        If UniLED_Edit.Enabled = False Then
            MessageBox.Show("First, You should convert LED!", "UniConverter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Clipboard.SetText(UniLED_Edit.Text)
            MessageBox.Show("UniPack LED Copied!", "UniConverter", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub AdvChk_CheckedChanged(sender As Object, e As EventArgs) Handles AdvChk.CheckedChanged
        If AdvChk.Checked = True Then
            If keyLED_Edit_Advanced.Enabled = True Then
                If MessageBox.Show("This mode changes the LED algorithm in detail." & vbNewLine &
                        "Developers don't recommend this mode. Would you like to continue?",
                        Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    keyLED_Edit_Advanced.Show()
                Else
                    AdvChk.Checked = False
                End If
            End If
        End If
    End Sub

    Private Sub KeyLED_Edit_Closed(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        keyLED_Edit_Advanced.Enabled = True
        keyLED_Edit_Advanced.Dispose()
        keyLED_Test.Enabled = True
        keyLED_Test.Dispose()
    End Sub

    Private Sub GAZUA__DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles GAZUA_.DoWork
        Try
            Dim ConLEDFile As String = String.Empty
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
                       UniLED_Edit.Text = "Please Wait..."
                       UniLED1.Text = "File Name: " & ConLEDFile
                   End Sub)

            '이 코드는 Follow_JB님의 midi2keyLED를 참고하여 만든 코드. (Thanks to Follow_JB. :D)

            Dim str As String = String.Empty
            Dim delaycount As Integer = 0
            Dim UniNoteNumberX As Integer 'X
            Dim UniNoteNumberY As Integer 'Y

            For Each mdEvent_list In keyLED.Events
                For Each mdEvent In mdEvent_list

                    Dim Item_a As String = String.Empty
                    Invoke(Sub() Item_a = ALGModeBox.SelectedItem.ToString())
                    If Item_a = "Ableton 9 ALG1" Then

                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a As NoteOnEvent = DirectCast(mdEvent, NoteOnEvent)
                            Dim bpm As New TempoEvent(500000, a.AbsoluteTime)
#Region "keyLED - Delays 1"
                            '최적화. 이 코드들을 Load 코드 부분에 이동 하고 변수를 선언해 최대한 변환 시간을 줄임.
                            If AdvChk.Checked Then

                                Select Case SoGood(0)
                                    Case "Note Length"

                                        Select Case SoGood(1)
                                            Case "Non-Convert"
                                                str = str & vbNewLine & "d " & a.NoteLength
                                            Case "NL4Ticks/NL2M"
                                                str = str & vbNewLine & "d " & GetNoteDelay(keyLED_MIDEX.NoteLength_1, 120, 192, a.NoteLength)
                                        End Select

                                    Case "Delta Time"

                                        Select Case SoGood(1)
                                            Case "Non-Convert"
                                                str = str & vbNewLine & "d " & a.DeltaTime
                                        End Select

                                    Case "Absolute Time"

                                        Select Case SoGood(1)
                                            Case "Non-Convert"
                                                str = str & vbNewLine & "d " & a.AbsoluteTime
                                            Case "AbTofMIDI"
                                                If Not delaycount = a.AbsoluteTime Then
                                                    str = str & vbNewLine & "d " & GetNoteDelay(keyLED_MIDEX.NoteLength_1, 120, 192, delaycount - a.AbsoluteTime + Math.Round(a.DeltaTime * 2.604) + Math.Round(a.NoteLength * 2.604))
                                                End If
                                            Case "TimeLine/NL2M"
                                                'Default Settings. [UniConverter v1.1.0.3]
                                                If Not delaycount = a.AbsoluteTime Then
                                                    str = str & vbNewLine & "d " & GetNoteDelay(keyLED_MIDEX.NoteLength_2, bpm.Tempo, keyLED.DeltaTicksPerQuarterNote, a.NoteLength)
                                                End If
                                        End Select
                                End Select

                            Else
#End Region

                                '기본 알고리즘: Absolute Time, TimeLine / NL2M
                                '이 delay 변환 코드는 변환 코드 알고리즘이 수정될 때마다 수정 해야 합니다!
                                If Not delaycount = a.AbsoluteTime OrElse Not a.DeltaTime = 0 Then
                                    str = str & vbNewLine & "d " & GetNoteDelay(keyLED_MIDEX.NoteLength_2, bpm.Tempo, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delaycount)
                                End If
                            End If

                            UniNoteNumberX = GX_keyLED(keyLED_MIDEX.NoteNumber_1, a.NoteNumber)
                            UniNoteNumberY = GY_keyLED(keyLED_MIDEX.NoteNumber_1, a.NoteNumber)
                            delaycount = a.AbsoluteTime
                            str = str & vbNewLine & "o " & UniNoteNumberX & " " & UniNoteNumberY & " a " & a.Velocity

                        ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                            Dim a As NoteEvent = DirectCast(mdEvent, NoteEvent)
                            UniNoteNumberX = GX_keyLED(keyLED_MIDEX.NoteNumber_1, a.NoteNumber)
                            UniNoteNumberY = GY_keyLED(keyLED_MIDEX.NoteNumber_1, a.NoteNumber)
                            str = str & vbNewLine & "f " & UniNoteNumberX & " " & UniNoteNumberY

                        End If

                    ElseIf Item_a = "Non-Convert (Developer Mode)" Then
#Region "Non-Convert (Developer Mode)"
                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a = DirectCast(mdEvent, NoteOnEvent)

                            If Not delaycount = a.AbsoluteTime Then
                                str = str & vbNewLine & "d " & a.NoteLength
                            End If

                            delaycount = a.AbsoluteTime
                            str = str & vbNewLine & "o " & a.NoteNumber & " a " & a.Velocity

                        ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                            Dim a As NoteEvent = DirectCast(mdEvent, NoteEvent)
                            str = str & vbNewLine & "f " & a.NoteNumber

                        End If
#End Region

                    Else
                        MessageBox.Show("You have to select the 'Ableton ALG1' or 'Non-Convert (Developer Mode)'.'", Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        e.Cancel = True
                        Exit Sub
                    End If
                Next
            Next

            '8192는 MC LED 번호.
            If Regex.IsMatch(str, "-8192") Then '-8192 = Non-UniNoteNumber
                str = str.Replace("o -8192 ", "o mc ").Trim() 'ON MC LED Convert.
                str = str.Replace("f -8192 ", "f mc ").Trim() 'OFF MC LED Convert.
            End If

            Invoke(Sub()
                       UniLED_Edit.Text = str.Remove(0, 0)
                       TestButton.Enabled = True
                       keyLED_Test.Enabled = True
                       CopyButton.Enabled = True
                   End Sub)
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

    Private Sub CleaningButton_Click(sender As Object, e As EventArgs) Handles CleaningButton.Click
        Try
            CanEnable = False
            TestButton.Enabled = False
            keyLED_Test.Enabled = False
            CopyButton.Enabled = False

            UniLED_Edit.Text = UniLED_Edit.Text.Replace("d 0", "")

            Dim i As Integer = 0
            For Each x As String In UniLED_Edit.Text.Split(Environment.NewLine)
                i += 1
            Next

            Dim CleanedText As String() = New String(i) {}
            Dim q As Integer = 0
            Dim loi As Boolean = True
            For Each x As String In UniLED_Edit.Text.Split(Environment.NewLine)
                If loi Then

                    If Not x = "" Then
                        CleanedText(q) = x
                    End If
                    q += 1
                    loi = False

                Else

                    If Not x = "" Then
                        CleanedText(q) = x.Remove(0, 1)
                    End If
                    q += 1

                End If
            Next

            Dim CleanedTextA As String = String.Empty
            For Each xi As String In CleanedText
                If Not xi = "" Then
                    CleanedTextA = CleanedTextA & xi & vbNewLine
                End If
            Next

            UniLED_Edit.Text = CleanedTextA

            CanEnable = True
            TestButton.Enabled = True
            keyLED_Test.Enabled = True
            CopyButton.Enabled = True

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
            MessageBox.Show("You have to convert the LED first!" & vbNewLine & "Please wait...", Me.Text & ": Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub
End Class
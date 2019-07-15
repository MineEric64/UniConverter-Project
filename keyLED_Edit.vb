Imports System.IO
Imports System.Text.RegularExpressions
Imports NAudio.Midi
Imports A2UP
Imports System.Xml
Imports System.Threading

Public Class keyLED_Edit
    Public CanEnable As Boolean = False

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
        Dim setaNode As XmlNode = setNode.SelectSingleNode("/Settings-XML/UCV-PATH")

        If setaNode IsNot Nothing Then
            CleaningButton.Enabled = Boolean.Parse(setaNode.ChildNodes(2).InnerText)
        End If
    End Sub

    Private Sub CopyButton_Click(sender As Object, e As EventArgs) Handles CopyButton.Click
        '복사 코드.
        If UniLED_Edit.Enabled = False Then
            MessageBox.Show("First, You should convert LED!", "UniConverter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Clipboard.SetText(UniLED_Edit.Text)
            MessageBox.Show("Unipack LED Copied!", "UniConverter", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub GazuaButton_Click(sender As Object, e As EventArgs) Handles GazuaButton.Click
        Try
            If GAZUA_.IsBusy = False Then
                GAZUA_.RunWorkerAsync()
            ElseIf GAZUA_.IsBusy = True Then
                MessageBox.Show("We are converting MIDI File now!" & vbNewLine & "Please Wait...", Me.Text & ": Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
            Dim LEDFileC As New MidiFile(LEDFileName, False)

            Invoke(Sub()
                       UniLED_Edit.Enabled = True
                       UniLED_Edit.Clear()
                       UniLED_Edit.Text = "Please Wait..."
                       UniLED1.Text = "FileName: " & ConLEDFile
                   End Sub)

            '이 코드는 Follow_JB님의 midi2keyLED를 참고하여 만든 코드. (Thanks to Follow_JB. :D)
            Dim li As Integer = 0
            For Each mdEvent_list In LEDFileC.Events
                For Each mdEvent In mdEvent_list
                    li += 1
                Next
            Next
            Dim str As String() = New String(li + 100000) {}

            Dim rt As Integer = 0
            Select Case str.Count
                'MIDI 파일 데이터를 수집하여 시간 복잡도의 정확도 향상. 
                Case 100000 To 105000 - 1
                    rt = 1
                Case 105000 To 110000 - 1
                    rt = 2
                Case 110000 To 113000 - 1
                    rt = 3
                Case 113000 To 117000 - 1
                    rt = 4
                Case 117000 To 150000 - 1
                    rt = Math.Round((str.Count - 93000) / 1000 * 167 / 1000)
                Case 150000 To 200000 - 1
                    rt = Math.Round((str.Count + 20000) / 1000 * 167 / 1000)
                Case > 200000
                    rt = Math.Round((str.Count + 50000) / 1000 * 167 / 1000)
            End Select

            Invoke(Sub() UniLED_Edit.Text = "Please Wait..." & vbNewLine & "Time complexity: " & rt & "s")
            Dim i As Integer = 0
            Dim delaycount As Integer = 0
            Dim file_ex = Application.StartupPath + "\settings.xml"
            Dim setNode As New XmlDocument
            setNode.Load(file_ex)
            Dim setaNode As XmlNode = setNode.SelectSingleNode("/Settings-XML/keyLED-Adv")

            Dim UniNoteNumberX As Integer 'X
            Dim UniNoteNumberY As Integer 'Y
            For Each mdEvent_list In LEDFileC.Events
                For Each mdEvent In mdEvent_list

                    Dim SelconItem As String = String.Empty
                    Invoke(Sub() SelconItem = SelCon1.SelectedItem.ToString())
                    If SelconItem = "Ableton 9 ALG1" Then

                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a = DirectCast(mdEvent, NoteOnEvent)
                            Dim b As New A2U

#Region "keyLED - Delays 1"
                            If setaNode IsNot Nothing Then

                                If setaNode.ChildNodes(0).InnerText = "NoteLength" Then

                                    If setaNode.ChildNodes(1).InnerText = "Non-Convert" Then
                                        str(i) = "d " & a.NoteLength
                                    ElseIf setaNode.ChildNodes(1).InnerText = "NL4Ticks/NL2M" Then
                                        If Not a.DeltaTime = 0 Then
                                            str(i) = "d " & b.GetNoteDelay(b.keyLED_AC.T_NoteLength1, 120, 192, a.NoteLength)
                                        End If
                                    End If

                                ElseIf setaNode.ChildNodes(0).InnerText = "DeltaTime" Then

                                    If setaNode.ChildNodes(2).InnerText = "Non-Convert" Then
                                        str(i) = "d " & a.DeltaTime
                                    End If

                                ElseIf setaNode.ChildNodes(0).InnerText = "AbsoluteTime" Then

                                    If setaNode.ChildNodes(3).InnerText = "Non-Convert" Then
                                        str(i) = "d " & a.AbsoluteTime
                                    ElseIf setaNode.ChildNodes(3).InnerText = "AbTofMIDI" Then

                                        Dim bpm As Integer = 120
                                        Dim ppq = LEDFileC.DeltaTicksPerQuarterNote
                                        Dim r As Integer = ppq * bpm
                                        str(i) = "d " & Math.Truncate(a.AbsoluteTime * 60000 / r)
                                        'str(i) = "d " & Math.Truncate(a.AbsoluteTime / LEDFileC.DeltaTicksPerQuarterNote * 120)
                                        'str(i) = "d " & Math.Truncate(((a.AbsoluteTime - LastTempoEvent.AbsoluteTime) / LEDFileC.DeltaTicksPerQuarterNote) * 120 + LastTempoEvent.RealTime)

                                    ElseIf setaNode.ChildNodes(3).InnerText = "TimeLine/NL2M" Then
                                        If Not delaycount = a.AbsoluteTime Then
                                            str(i) = "d " & b.GetNoteDelay(b.keyLED_AC.T_NoteLength1, 120, 192, delaycount - a.AbsoluteTime + Math.Round(a.DeltaTime * 2.604) + Math.Round(a.NoteLength * 2.604))
                                        End If
                                    End If
                                End If

                                i += 1
                            End If
#End Region

                            UniNoteNumberX = b.GX_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                            UniNoteNumberY = b.GY_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                            delaycount = a.AbsoluteTime
                            str(i) = "o " & UniNoteNumberX & " " & UniNoteNumberY & " a " & a.Velocity

                        ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                            Dim a = DirectCast(mdEvent, NoteEvent)
                            Dim b As New A2U
                            UniNoteNumberX = b.GX_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                            UniNoteNumberY = b.GY_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                            str(i) = "f " & UniNoteNumberX & " " & UniNoteNumberY

                        End If

                    ElseIf SelconItem = "Non-Convert (Developer Mode)" Then

                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a = DirectCast(mdEvent, NoteOnEvent)
                            Dim b As New A2U

#Region "keyLED - Delays 1"
                            If setaNode IsNot Nothing Then

                                If setaNode.ChildNodes(0).InnerText = "NoteLength" Then

                                    If setaNode.ChildNodes(1).InnerText = "Non-Convert" Then
                                        str(i) = "d " & a.NoteLength
                                    ElseIf setaNode.ChildNodes(1).InnerText = "NL4Ticks/NL2M" Then
                                        str(i) = "d " & b.GetNoteDelay(b.keyLED_AC.T_NoteLength1, 120, 192, a.NoteLength)
                                    End If

                                ElseIf setaNode.ChildNodes(0).InnerText = "DeltaTime" Then

                                    If setaNode.ChildNodes(2).InnerText = "Non-Convert" Then
                                        str(i) = "d " & a.DeltaTime
                                    End If

                                ElseIf setaNode.ChildNodes(0).InnerText = "AbsoluteTime" Then

                                    If setaNode.ChildNodes(3).InnerText = "Non-Convert" Then
                                        str(i) = "d " & a.AbsoluteTime
                                    ElseIf setaNode.ChildNodes(3).InnerText = "AbTofMIDI" Then

                                        Dim bpm As Integer = 120
                                        Dim ppq = LEDFileC.DeltaTicksPerQuarterNote
                                        Dim r As Integer = ppq * bpm
                                        str(i) = "d " & Math.Truncate(a.AbsoluteTime * 60000 / r)
                                        'str(i) = "d " & Math.Truncate(a.AbsoluteTime / LEDFileC.DeltaTicksPerQuarterNote * 120)
                                        'str(i) = "d " & Math.Truncate(((a.AbsoluteTime - LastTempoEvent.AbsoluteTime) / LEDFileC.DeltaTicksPerQuarterNote) * 120 + LastTempoEvent.RealTime)

                                    ElseIf setaNode.ChildNodes(3).InnerText = "TimeLine/NL2M" Then
                                        If Not delaycount = a.AbsoluteTime Then
                                            str(i) = "d " & b.GetNoteDelay(b.keyLED_AC.T_NoteLength1, 120, 192, delaycount - a.AbsoluteTime + Math.Round(a.DeltaTime * 2.604) + Math.Round(a.NoteLength * 2.604))
                                        End If
                                    End If
                                End If
                                i += 1
                            End If
#End Region

                            delaycount = a.AbsoluteTime
                            str(i) = "o " & a.NoteNumber & " a " & a.Velocity

                        ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                            Dim a = DirectCast(mdEvent, NoteEvent)
                            str(i) = "f " & a.NoteNumber

                        End If

                    Else
                        MessageBox.Show("You have to select the 'Ableton ALG1' or 'Non-Convert (Developer Mode)'.'", Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    i += 1
                Next
            Next

            Dim strn As String = String.Empty
            For Each stnr As String In str
                If Not stnr = "" Then
                    strn = strn & stnr & vbNewLine
                End If
            Next

            Invoke(Sub() UniLED_Edit.Text = strn)

            '8192는 MC LED 번호.
            If Regex.IsMatch(strn, "-8192") Then '-8192 = Non-UniNoteNumber
                Invoke(Sub()
                           UniLED_Edit.Text = strn.Replace("o -8192 ", "o mc ").Trim() 'ON MC LED Convert.
                           UniLED_Edit.Text = UniLED_Edit.Text.Replace("f -8192 ", "f mc ").Trim() 'OFF MC LED Convert.
                       End Sub)
            End If

            Invoke(Sub()
                       CanEnable = True 'Enabled to Test the LED.
                       TestButton.Enabled = True
                       keyLED_Test.Enabled = True
                       CopyButton.Enabled = True
                   End Sub)
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
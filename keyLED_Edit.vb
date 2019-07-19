Imports System.IO
Imports System.Text.RegularExpressions
Imports NAudio.Midi
Imports A2UP
Imports System.Xml

Public Class keyLED_Edit
    Public CanEnable As Boolean = False
    Public SoGood As String() = New String(1) {}

#Region "pfTest-Debug"
    Public pfTest As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\pfTest.mid"
    Public milus As Integer = 0
    Public ILoveYa As MidiFile
#End Region

    Private Sub KeyLED_Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'FileName 표시.
        For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\ableproj\CoLED", FileIO.SearchOption.SearchTopLevelOnly, "*.mid") 'FileName의 파일 찾기
            Invoke(Sub()
                       Dim itm As New ListViewItem(New String() {Path.GetFileName(foundFile), foundFile})
                       LED_ListView.Items.Add(itm)  '파일 이름 추가
                   End Sub)
        Next

        '성능 테스트. (Remaining Time ++)
        pfTest_bgw.RunWorkerAsync()

        Dim file_ex = Application.StartupPath + "\settings.xml"
        Dim setNode As New XmlDocument
        setNode.Load(file_ex)
        Dim setaNode As XmlNode = setNode.SelectSingleNode("/Settings-XML/UCV-PATH")

        If setaNode IsNot Nothing Then
            CleaningButton.Enabled = Boolean.Parse(setaNode.ChildNodes(2).InnerText)
        End If

        If setaNode IsNot Nothing Then
            If setaNode.ChildNodes(0).InnerText = "NoteLength" Then
                SoGood(0) = "Note Length"
                SoGood(1) = setaNode.ChildNodes(1).InnerText

            ElseIf setaNode.ChildNodes(0).InnerText = "DeltaTime" Then
                SoGood(0) = "Delta Time"
                SoGood(1) = setaNode.ChildNodes(2).InnerText

            ElseIf setaNode.ChildNodes(0).InnerText = "AbsoluteTime" Then
                SoGood(0) = "Absolute Time"
                SoGood(1) = setaNode.ChildNodes(3).InnerText

            End If
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
            Dim i As Integer = 0
            Dim delaycount As Integer = 0

            Dim UniNoteNumberX As Integer 'X
            Dim UniNoteNumberY As Integer 'Y
            For Each mdEvent_list In LEDFileC.Events
                For Each mdEvent In mdEvent_list

                    Dim SelconItem As String = String.Empty
                    Invoke(Sub() SelconItem = SelCon1.SelectedItem.ToString())
                    If SelconItem = "Ableton 9 ALG1" Then

                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a As NoteOnEvent = DirectCast(mdEvent, NoteOnEvent)
                            Dim b As New A2U

#Region "keyLED - Delays 1"
                            '최적화. 이 코드들을 Load 코드 부분에 이동 하고 변수를 선언해 최대한 변환 시간을 줄임.
                            If AdvChk.Checked Then

                                If SoGood(0) = "Note Length" Then

                                    If SoGood(1) = "Non-Convert" Then
                                        str(i) = "d " & a.NoteLength
                                    ElseIf SoGood(1) = "NL4Ticks/NL2M" Then
                                        str(i) = "d " & b.GetNoteDelay(A2U.keyLED_AC.T_NoteLength1, 120, 192, a.NoteLength)
                                    End If

                                ElseIf SoGood(0) = "Delta Time" Then

                                    If SoGood(1) = "Non-Convert" Then
                                        str(i) = "d " & a.DeltaTime
                                    End If

                                ElseIf SoGood(0) = "Absolute Time" Then

                                    If SoGood(1) = "Non-Convert" Then
                                        str(i) = "d " & a.AbsoluteTime

                                    ElseIf SoGood(1) = "AbTofMIDI" Then
                                        Dim bpm As Integer = 120
                                        Dim ppq = LEDFileC.DeltaTicksPerQuarterNote
                                        Dim r As Integer = ppq * bpm
                                        str(i) = "d " & Math.Truncate(a.AbsoluteTime * 60000 / r)
                                        'str(i) = "d " & Math.Truncate(a.AbsoluteTime / LEDFileC.DeltaTicksPerQuarterNote * 120)
                                        'str(i) = "d " & Math.Truncate(((a.AbsoluteTime - LastTempoEvent.AbsoluteTime) / LEDFileC.DeltaTicksPerQuarterNote) * 120 + LastTempoEvent.RealTime)

                                    ElseIf SoGood(1) = "TimeLine/NL2M" Then
                                        If Not delaycount = a.AbsoluteTime Then
                                            str(i) = "d " & b.GetNoteDelay(A2U.keyLED_AC.T_NoteLength1, 120, 192, delaycount - a.AbsoluteTime + Math.Round(a.DeltaTime * 2.604) + Math.Round(a.NoteLength * 2.604))
                                        End If
                                    End If
                                    i += 1
                                End If

                            Else

                                '기본 알고리즘: Absolute Time, TimeLine / NL2M
                                '이 delay 변환 코드는 변환 코드 알고리즘이 수정될 때마다 수정 해야 합니다!
                                If Not delaycount = a.AbsoluteTime Then
                                    str(i) = "d " & b.GetNoteDelay(A2U.keyLED_AC.T_NoteLength1, 120, 192, delaycount - a.AbsoluteTime + Math.Round(a.DeltaTime * 2.604) + Math.Round(a.NoteLength * 2.604))
                                End If

                            End If
#End Region

                            UniNoteNumberX = b.GX_keyLED(A2U.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                            UniNoteNumberY = b.GY_keyLED(A2U.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                            delaycount = a.AbsoluteTime
                                str(i) = "o " & UniNoteNumberX & " " & UniNoteNumberY & " a " & a.Velocity

                            ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                                Dim a = DirectCast(mdEvent, NoteEvent)
                            Dim b As New A2U
                            UniNoteNumberX = b.GX_keyLED(A2U.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                            UniNoteNumberY = b.GY_keyLED(A2U.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                            str(i) = "f " & UniNoteNumberX & " " & UniNoteNumberY

                        End If

                    ElseIf SelconItem = "Non-Convert (Developer Mode)" Then

                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a = DirectCast(mdEvent, NoteOnEvent)
                            Dim b As New A2U

#Region "keyLED - Delays 1"
                            If AdvChk.Checked Then

                                If SoGood(0) = "Note Length" Then

                                    If SoGood(1) = "Non-Convert" Then
                                        str(i) = "d " & a.NoteLength
                                    ElseIf SoGood(1) = "NL4Ticks/NL2M" Then
                                        str(i) = "d " & b.GetNoteDelay(A2U.keyLED_AC.T_NoteLength1, 120, 192, a.NoteLength)
                                    End If

                                ElseIf SoGood(0) = "Delta Time" Then

                                    If SoGood(1) = "Non-Convert" Then
                                        str(i) = "d " & a.DeltaTime
                                    End If

                                ElseIf SoGood(0) = "Absolute Time" Then

                                    If SoGood(1) = "Non-Convert" Then
                                        str(i) = "d " & a.AbsoluteTime

                                    ElseIf SoGood(1) = "AbTofMIDI" Then
                                        Dim bpm As Integer = 120
                                        Dim ppq = LEDFileC.DeltaTicksPerQuarterNote
                                        Dim r As Integer = ppq * bpm
                                        str(i) = "d " & Math.Truncate(a.AbsoluteTime * 60000 / r)
                                        'str(i) = "d " & Math.Truncate(a.AbsoluteTime / LEDFileC.DeltaTicksPerQuarterNote * 120)
                                        'str(i) = "d " & Math.Truncate(((a.AbsoluteTime - LastTempoEvent.AbsoluteTime) / LEDFileC.DeltaTicksPerQuarterNote) * 120 + LastTempoEvent.RealTime)

                                    ElseIf SoGood(1) = "TimeLine/NL2M" Then
                                        If Not delaycount = a.AbsoluteTime Then
                                            str(i) = "d " & b.GetNoteDelay(A2U.keyLED_AC.T_NoteLength1, 120, 192, delaycount - a.AbsoluteTime + Math.Round(a.DeltaTime * 2.604) + Math.Round(a.NoteLength * 2.604))
                                        End If
                                    End If
                                    i += 1
                                End If

                            Else

                                '이 delay 변환 코드는 변환 코드 알고리즘이 수정될 때마다 수정 해야 합니다!
                                '기본 알고리즘: Absolute Time, TimeLine / NL2M
                                If Not delaycount = a.AbsoluteTime Then
                                    str(i) = "d " & b.GetNoteDelay(A2U.keyLED_AC.T_NoteLength1, 120, 192, delaycount - a.AbsoluteTime + Math.Round(a.DeltaTime * 2.604) + Math.Round(a.NoteLength * 2.604))
                                End If

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
                        e.Cancel = True
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

            '8192는 MC LED 번호.
            If Regex.IsMatch(strn, "-8192") Then '-8192 = Non-UniNoteNumber
                Invoke(Sub()
                           strn = strn.Replace("o -8192 ", "o mc ").Trim() 'ON MC LED Convert.
                           strn = strn.Replace("f -8192 ", "f mc ").Trim() 'OFF MC LED Convert.
                       End Sub)
            End If

            Invoke(Sub()
                       UniLED_Edit.Text = strn
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

    Private Sub PfTest_bgw_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles pfTest_bgw.DoWork
        File.WriteAllBytes(pfTest, My.Resources.pfTest)
        ILoveYa = New MidiFile(pfTest, False)
        Tests_bgw.RunWorkerAsync()
    End Sub

    Public Sub GetkeyLED_MIDEX2(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Tests_bgw.DoWork
        'A2UP keyLED Code.
        '- Test Code.

        Dim sto As New Stopwatch
        sto.Start()
        Dim li As Integer = 0
        For Each mdEvent_list In ILoveYa.Events
            For Each mdEvent In mdEvent_list
                li += 1
            Next
        Next
        Dim str As String() = New String(li + 100000) {}

        Dim i As Integer = 0
        Dim delaycount As Integer = 0
        Dim UniNoteNumberX As Integer 'X
        Dim UniNoteNumberY As Integer 'Y
        For Each mdEvent_list In ILoveYa.Events
            For Each mdEvent In mdEvent_list
                If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                    Dim a = DirectCast(mdEvent, NoteOnEvent)
                    Dim b As New A2U

                    If Not delaycount = a.AbsoluteTime Then
                        str(i) = "d " & b.GetNoteDelay(A2U.keyLED_AC.T_NoteLength1, 120, 192, delaycount - a.AbsoluteTime + Math.Round(a.DeltaTime * 2.604) + Math.Round(a.NoteLength * 2.604))
                        i += 1
                    End If

                    UniNoteNumberX = b.GX_keyLED(A2U.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                    UniNoteNumberY = b.GY_keyLED(A2U.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                    delaycount = a.AbsoluteTime
                    str(i) = "o " & UniNoteNumberX & " " & UniNoteNumberY & " a " & a.Velocity

                ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                    Dim a = DirectCast(mdEvent, NoteEvent)
                    Dim b As New A2U
                    UniNoteNumberX = b.GX_keyLED(A2U.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                    UniNoteNumberY = b.GY_keyLED(A2U.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                    str(i) = "f " & UniNoteNumberX & " " & UniNoteNumberY

                End If
                i += 1
            Next
        Next

        sto.Stop()
        Debug.WriteLine(sto.Elapsed.ToString)
        milus = sto.Elapsed.Seconds
    End Sub
End Class
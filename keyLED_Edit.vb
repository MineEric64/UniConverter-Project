Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading
Imports NAudio.Midi
Imports A2UP
Public Class keyLED_Edit
    Inherits Form
    Dim midFile As FileInfo
    Private trd As Thread

    Private Sub KeyLED_Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'FileName 표시.
        Invoke(Sub()
                   For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\ableproj\CoLED", FileIO.SearchOption.SearchTopLevelOnly, "*.mid") 'FileName의 파일 찾기
                       Dim itm As New ListViewItem(New String() {Path.GetFileName(foundFile), foundFile})
                       LED_ListView.Items.Add(itm)  '파일 이름 추가
                   Next
               End Sub)
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
            trd = New Thread(AddressOf keyLED_ConvertingGazua)
            trd.SetApartmentState(ApartmentState.MTA)
            trd.IsBackground = True
            trd.Start()
        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub keyLED_ConvertingGazua()
        Try
            Invoke(Sub()
                       Dim ConLEDFile = LED_ListView.FocusedItem.SubItems.Item(0).Text '선택한 아이템.

                       'Beta Code!
                       '이 Beta Convert Code는 오류가 발생할 수 있습니다.
                       '주의사항을 다 보셨다면, 당신은 Editor 권한을 가질 수 있습니다.


                       '변환 코드...
                       UniLED_Edit.Enabled = True
                       'V1.0.0.1 ~ V1.0.0.2 - String.Replace로 이용한 ConLED 파일 표시: ConLEDFile '[ConLEDFile String 계산: ":FileName.Ext"]
                       'V1.1.0.3 - Item.ToString을 Item.Text로 코드 최적화.

                       Dim LEDFileName = "Workspace\ableproj\CoLED\" & ConLEDFile
                       Dim LEDFileC As New MidiFile(LEDFileName, False)
                       Dim str As String

                       UniLED_Edit.Clear() 'UniPack LED Text Reset.
                       UniLED1.Text = "FileName: " & ConLEDFile 'UniLED1 File Show.

                       '이 코드는 Follow_JB님의 midi2keyLED를 참고하여 만든 코드. (Thanks to Follow_JB. :D)
                       Dim UniNoteNumberX As Integer 'X
                       Dim UniNoteNumberY As Integer 'Y
                       For Each mdEvent_list In LEDFileC.Events
                           For Each mdEvent In mdEvent_list
                               If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                                   Dim a = DirectCast(mdEvent, NoteOnEvent)
                                   If Not SelCon1.SelectedItem.ToString = "Non-Convert (Developer Mode)" Then
                                       Dim b As New A2U
                                       UniNoteNumberX = b.GX_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                                       UniNoteNumberY = b.GY_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                                       str = str & vbNewLine & "o " & UniNoteNumberX & " " & UniNoteNumberY & " a " & a.Velocity
                                   Else
                                       str = str & vbNewLine & "o " & a.NoteNumber & " a " & a.Velocity
                                   End If
#Region "Advanced Delay Mode"
                                   If AdvChk.Checked = True Then
                                       If keyLED_Edit_Advanced.DelayMode1.Text = "Note Length" Then
                                           If keyLED_Edit_Advanced.DelayConvert1_1.Checked = True Then
                                               str = str & vbNewLine & "d " & a.NoteLength
                                           ElseIf keyLED_Edit_Advanced.DelayConvert1_2.Checked = True Then
                                               If Not a.DeltaTime = 0 Then
                                                   Dim b As New A2U
                                                   str = str & vbNewLine & "d " & b.GetNoteDelay(b.keyLED_AC.T_NoteLength1, 120, 192, a.NoteLength)
                                               End If
                                           End If
                                       End If
                                       If keyLED_Edit_Advanced.DelayMode1.Text = "Delta Time" Then
                                           If keyLED_Edit_Advanced.DelayConvert2_1.Checked = True Then
                                               str = str & vbNewLine & "d " & a.DeltaTime
                                           End If
                                       End If
                                       If keyLED_Edit_Advanced.DelayMode1.Text = "Absolute Time" Then
                                           If keyLED_Edit_Advanced.DelayConvert3_1.Checked = True Then
                                               str = str & vbNewLine & "d " & a.AbsoluteTime
                                           ElseIf keyLED_Edit_Advanced.DelayConvert3_2.Checked = True Then
                                               Dim bpm As Integer = 120
                                               Dim ppq = LEDFileC.DeltaTicksPerQuarterNote
                                               Dim r As Integer = ppq * bpm
                                               str = str & vbNewLine & "d " & Math.Truncate(a.AbsoluteTime * 60000 / r)
                                               'str = str & vbNewLine & "d " & Math.Truncate(a.AbsoluteTime / LEDFileC.DeltaTicksPerQuarterNote * 120)
                                               'str = str & vbNewLine & "d " & Math.Truncate(((a.AbsoluteTime - LastTempoEvent.AbsoluteTime) / LEDFileC.DeltaTicksPerQuarterNote) * 120 + LastTempoEvent.RealTime)
                                           End If
                                       End If
                                   Else
                                       If Not a.DeltaTime = 0 Then
                                           Dim b As New A2U
                                           str = str & vbNewLine & "d " & b.GetNoteDelay(b.keyLED_AC.T_NoteLength1, 120, 48, a.NoteLength) 'Default Option.
                                       End If
                                   End If
#End Region 'LEDEdit_AdvancedMode_DelayOption
                                   '수정 해야할 사항: NOTE ON의 DELAY도 필요.
                                   '수정 사항: 일단 DELAY는 Delta Time이 0이 아닌 경우에 Note Length를 계산하여 추가함.
                                   'DELAY 테스트 성공.
                               ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then
                                   Dim a = DirectCast(mdEvent, NoteEvent)
                                   If Not SelCon1.SelectedItem.ToString = "Non-Convert (Developer Mode)" Then
                                       Dim b As New A2U
                                       UniNoteNumberX = b.GX_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                                       UniNoteNumberY = b.GY_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                                       str = str & vbNewLine & "f " & UniNoteNumberX & " " & UniNoteNumberY
                                   Else
                                       str = str & vbNewLine & "f " & a.NoteNumber
                                   End If
                               End If
                           Next
                       Next
                       UniLED_Edit.Text = str

                       '8192는 MC LED 번호.
                       If Regex.IsMatch(str, "8192") Then '8192 = Non-UniNoteNumber
                           UniLED_Edit.Text = str.Replace(" 8192", "").Trim() 'MC LED Convert.
                           UniLED_Edit.Text = UniLED_Edit.Text.Replace("o ", "o mc ").Trim() 'On MC LED Convert.
                           UniLED_Edit.Text = UniLED_Edit.Text.Replace("f ", "f mc ").Trim() 'Off MC LED Convert.
                       End If
                   End Sub)
        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub KeyLED_Edit_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        keyLED_Edit_Advanced.Enabled = True
        keyLED_Edit_Advanced.Hide()
    End Sub
End Class
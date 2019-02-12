Imports System.IO
Imports System.Runtime
Imports System.Text.RegularExpressions
Imports NAudio.Midi
Public Class keyLED_Edit
    Inherits Form
    Dim midFile As FileInfo
    Private Sub KeyLED_Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'FileName 표시.
        For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\ableproj\CoLED", FileIO.SearchOption.SearchTopLevelOnly, "*.mid") 'FileName의 파일 찾기
            Dim itm As New ListViewItem(New String() {Path.GetFileName(foundFile), foundFile})
            LED_ListView.Items.Add(itm)  '파일 이름 추가
        Next

        LED_ListView.AllowDrop = True
    End Sub

    Private Sub LED_ListView_DragEnter(sender As System.Object, e As DragEventArgs) Handles LED_ListView.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub LED_ListView_DragDrop(sender As System.Object, e As DragEventArgs) Handles LED_ListView.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

        '---Beta Code: Drag and Drop & Get File Name Only---
        LED_ListView.Items.Clear()
        UniLED_Edit.Clear()
        UniLED1.Text = "File Name: None"
        If UniLED_Edit.Enabled = True Then UniLED_Edit.Enabled = False
        If Dir("Workspace\ableproj\CoLED", vbDirectory) <> "" Then My.Computer.FileSystem.DeleteDirectory("Workspace\ableproj\CoLED", FileIO.DeleteDirectoryOption.DeleteAllContents)

        My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\CoLED")
        For i = 0 To files.Length - 1
            File.Copy(files(i), "Workspace\ableproj\CoLED\" & files(i).Split("\").Last, True)
        Next

        For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\ableproj\CoLED", FileIO.SearchOption.SearchTopLevelOnly, "*.mid") 'FileName의 파일 찾기
            Dim itm As New ListViewItem(New String() {Path.GetFileName(foundFile), foundFile})
            LED_ListView.Items.Add(itm)  '파일 이름 추가
        Next
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
            Dim ConLEDFile = LED_ListView.FocusedItem.SubItems.Item(0).Text '선택한 아이템.

            'Beta Code!
            '이 Beta Convert Code는 오류가 발생할 수 있습니다.
            '주의사항을 다 보셨다면, 당신은 Editor 권한을 가질 수 있습니다.


            '변환 코드...
            UniLED_Edit.Enabled = True
            'V1.0.0.1 ~ V1.0.0.2 - String.Replace로 이용한 ConLED 파일 표시: ConLEDFile '[ConLEDFile String 계산: ":FileName.Ext"]
            'V1.1.0.3 - Item.ToString을 Item.Text로 코드 최적화.

            Dim LEDFIleName = "Workspace\ableproj\CoLED\" & ConLEDFile
            Dim LEDFileC As New MidiFile(LEDFIleName, False)
            Dim str As String

            UniLED_Edit.Clear() 'UniPack LED Text Reset.
            UniLED1.Text = "FileName: " & ConLEDFile 'UniLED1 File Show.

            '이 코드는 Follow_JB님의 midi2keyLED를 참고하여 만든 코드. (Thanks to Follow_JB. :D)
            Dim UniNoteNumber As Integer 'YButton

            For Each mdEvent_list In LEDFileC.Events
                For Each mdEvent In mdEvent_list
                    If (mdEvent.CommandCode = MidiCommandCode.MetaEvent) Then
                        Dim a = DirectCast(mdEvent, MetaEvent)
                    ElseIf (mdEvent.CommandCode = MidiCommandCode.NoteOn) Then
                        Dim a = DirectCast(mdEvent, NoteOnEvent)
#Region "#Ableton 9 #1 Settings"
                        If SelCon1.Text = "Ableton 9 #1" Then 'Ableton 9 #1은 Ableton X/Y:64를 Unipack X/Y:1로 변환 하는 것. [Ableton 9에서 작동하는 알고리즘 #1]
                            '[Ableton LED To UniPack LED Convert CODE] [BETA!!]'

                            'X1, Y1 ~ X1, Y8
                            If a.NoteNumber = 64 Then '[Ableton Button]'
                                a.NoteNumber = 1 '[UniPack X Button]'
                                UniNoteNumber = 1 '[UniPack Y Button]'
                            End If

                            If a.NoteNumber = 65 Then
                                a.NoteNumber = 1
                                UniNoteNumber = 2
                            End If

                            If a.NoteNumber = 66 Then
                                a.NoteNumber = 1
                                UniNoteNumber = 3
                            End If

                            If a.NoteNumber = 67 Then
                                a.NoteNumber = 1
                                UniNoteNumber = 4
                            End If

                            If a.NoteNumber = 96 Then
                                a.NoteNumber = 1
                                UniNoteNumber = 5
                            End If

                            If a.NoteNumber = 97 Then
                                a.NoteNumber = 1
                                UniNoteNumber = 6
                            End If

                            If a.NoteNumber = 98 Then
                                a.NoteNumber = 1
                                UniNoteNumber = 7
                            End If

                            If a.NoteNumber = 99 Then
                                a.NoteNumber = 1
                                UniNoteNumber = 8
                            End If

                            'X2, Y1 ~ X2, Y8
                            If a.NoteNumber = 60 Then
                                a.NoteNumber = 2
                                UniNoteNumber = 1
                            End If

                            If a.NoteNumber = 61 Then
                                a.NoteNumber = 2
                                UniNoteNumber = 2
                            End If

                            If a.NoteNumber = 62 Then
                                a.NoteNumber = 2
                                UniNoteNumber = 3
                            End If

                            If a.NoteNumber = 63 Then
                                a.NoteNumber = 2
                                UniNoteNumber = 4
                            End If

                            If a.NoteNumber = 92 Then
                                a.NoteNumber = 2
                                UniNoteNumber = 5
                            End If

                            If a.NoteNumber = 93 Then
                                a.NoteNumber = 2
                                UniNoteNumber = 6
                            End If

                            If a.NoteNumber = 94 Then
                                a.NoteNumber = 2
                                UniNoteNumber = 7
                            End If

                            If a.NoteNumber = 95 Then
                                a.NoteNumber = 2
                                UniNoteNumber = 8
                            End If

                            'X3, Y1 ~ X3, Y8
                            If a.NoteNumber = 56 Then
                                a.NoteNumber = 3
                                UniNoteNumber = 1
                            End If

                            If a.NoteNumber = 57 Then
                                a.NoteNumber = 3
                                UniNoteNumber = 2
                            End If

                            If a.NoteNumber = 58 Then
                                a.NoteNumber = 3
                                UniNoteNumber = 3
                            End If

                            If a.NoteNumber = 59 Then
                                a.NoteNumber = 3
                                UniNoteNumber = 4
                            End If

                            If a.NoteNumber = 88 Then
                                a.NoteNumber = 3
                                UniNoteNumber = 5
                            End If

                            If a.NoteNumber = 89 Then
                                a.NoteNumber = 3
                                UniNoteNumber = 6
                            End If

                            If a.NoteNumber = 90 Then
                                a.NoteNumber = 3
                                UniNoteNumber = 7
                            End If

                            If a.NoteNumber = 91 Then
                                a.NoteNumber = 3
                                UniNoteNumber = 8
                            End If

                            'X4, Y1 ~ X4, Y8
                            If a.NoteNumber = 52 Then
                                a.NoteNumber = 4
                                UniNoteNumber = 1
                            End If

                            If a.NoteNumber = 53 Then
                                a.NoteNumber = 4
                                UniNoteNumber = 2
                            End If

                            If a.NoteNumber = 54 Then
                                a.NoteNumber = 4
                                UniNoteNumber = 3
                            End If

                            If a.NoteNumber = 55 Then
                                a.NoteNumber = 4
                                UniNoteNumber = 4
                            End If

                            If a.NoteNumber = 84 Then
                                a.NoteNumber = 4
                                UniNoteNumber = 5
                            End If

                            If a.NoteNumber = 85 Then
                                a.NoteNumber = 4
                                UniNoteNumber = 6
                            End If

                            If a.NoteNumber = 86 Then
                                a.NoteNumber = 4
                                UniNoteNumber = 7
                            End If

                            If a.NoteNumber = 87 Then
                                a.NoteNumber = 4
                                UniNoteNumber = 8
                            End If

                            'X5, Y1 ~ X5, Y8
                            If a.NoteNumber = 48 Then
                                a.NoteNumber = 5
                                UniNoteNumber = 1
                            End If

                            If a.NoteNumber = 49 Then
                                a.NoteNumber = 5
                                UniNoteNumber = 2
                            End If

                            If a.NoteNumber = 50 Then
                                a.NoteNumber = 5
                                UniNoteNumber = 3
                            End If

                            If a.NoteNumber = 51 Then
                                a.NoteNumber = 5
                                UniNoteNumber = 4
                            End If

                            If a.NoteNumber = 80 Then
                                a.NoteNumber = 5
                                UniNoteNumber = 5
                            End If

                            If a.NoteNumber = 81 Then
                                a.NoteNumber = 5
                                UniNoteNumber = 6
                            End If

                            If a.NoteNumber = 82 Then
                                a.NoteNumber = 5
                                UniNoteNumber = 7
                            End If

                            If a.NoteNumber = 83 Then
                                a.NoteNumber = 5
                                UniNoteNumber = 8
                            End If

                            'X6, Y1 ~ X6, Y8
                            If a.NoteNumber = 44 Then
                                a.NoteNumber = 6
                                UniNoteNumber = 1
                            End If

                            If a.NoteNumber = 45 Then
                                a.NoteNumber = 6
                                UniNoteNumber = 2
                            End If

                            If a.NoteNumber = 46 Then
                                a.NoteNumber = 6
                                UniNoteNumber = 3
                            End If

                            If a.NoteNumber = 47 Then
                                a.NoteNumber = 6
                                UniNoteNumber = 4
                            End If

                            If a.NoteNumber = 76 Then
                                a.NoteNumber = 6
                                UniNoteNumber = 5
                            End If

                            If a.NoteNumber = 77 Then
                                a.NoteNumber = 6
                                UniNoteNumber = 6
                            End If

                            If a.NoteNumber = 78 Then
                                a.NoteNumber = 6
                                UniNoteNumber = 7
                            End If

                            If a.NoteNumber = 79 Then
                                a.NoteNumber = 6
                                UniNoteNumber = 8
                            End If

                            'X7, Y1 ~ X7, Y8
                            If a.NoteNumber = 40 Then
                                a.NoteNumber = 7
                                UniNoteNumber = 1
                            End If

                            If a.NoteNumber = 41 Then
                                a.NoteNumber = 7
                                UniNoteNumber = 2
                            End If

                            If a.NoteNumber = 42 Then
                                a.NoteNumber = 7
                                UniNoteNumber = 3
                            End If

                            If a.NoteNumber = 43 Then
                                a.NoteNumber = 7
                                UniNoteNumber = 4
                            End If

                            If a.NoteNumber = 72 Then
                                a.NoteNumber = 7
                                UniNoteNumber = 5
                            End If

                            If a.NoteNumber = 73 Then
                                a.NoteNumber = 7
                                UniNoteNumber = 6
                            End If

                            If a.NoteNumber = 74 Then
                                a.NoteNumber = 7
                                UniNoteNumber = 7
                            End If

                            If a.NoteNumber = 75 Then
                                a.NoteNumber = 7
                                UniNoteNumber = 8
                            End If

                            'X8, Y1 ~ X8, Y8
                            If a.NoteNumber = 36 Then
                                a.NoteNumber = 8
                                UniNoteNumber = 1
                            End If

                            If a.NoteNumber = 37 Then
                                a.NoteNumber = 8
                                UniNoteNumber = 2
                            End If

                            If a.NoteNumber = 38 Then
                                a.NoteNumber = 8
                                UniNoteNumber = 3
                            End If

                            If a.NoteNumber = 39 Then
                                a.NoteNumber = 8
                                UniNoteNumber = 4
                            End If

                            If a.NoteNumber = 68 Then
                                a.NoteNumber = 8
                                UniNoteNumber = 5
                            End If

                            If a.NoteNumber = 69 Then
                                a.NoteNumber = 8
                                UniNoteNumber = 6
                            End If

                            If a.NoteNumber = 70 Then
                                a.NoteNumber = 8
                                UniNoteNumber = 7
                            End If

                            If a.NoteNumber = 71 Then
                                a.NoteNumber = 8
                                UniNoteNumber = 8
                            End If

                            'MC9 ~ MC16
                            If a.NoteNumber = 100 Then
                                a.NoteNumber = 9
                                UniNoteNumber = 8192
                            End If

                            If a.NoteNumber = 101 Then
                                a.NoteNumber = 10
                                UniNoteNumber = 8192
                            End If

                            If a.NoteNumber = 102 Then
                                a.NoteNumber = 11
                                UniNoteNumber = 8192
                            End If

                            If a.NoteNumber = 103 Then
                                a.NoteNumber = 12
                                UniNoteNumber = 8192
                            End If

                            If a.NoteNumber = 104 Then
                                a.NoteNumber = 13
                                UniNoteNumber = 8192
                            End If

                            If a.NoteNumber = 105 Then
                                a.NoteNumber = 14
                                UniNoteNumber = 8192
                            End If

                            If a.NoteNumber = 106 Then
                                a.NoteNumber = 15
                                UniNoteNumber = 8192
                            End If

                            If a.NoteNumber = 107 Then
                                a.NoteNumber = 16
                                UniNoteNumber = 8192
                            End If

                        End If
#End Region
#Region "Ableton 9 #2 Settings"
                        If SelCon1.Text = "Ableton 9 #2" Then
                            If a.NoteNumber = 64 Then

                            End If
                        End If
#End Region
                        str = str & vbNewLine & "o " & a.NoteNumber & " " & UniNoteNumber & " a " & a.Velocity
#Region "Advanced Delay Mode"
                        If AdvChk.Checked = True Then
                            If LEDEdit_Advanced.DelayMode2.Text = "Note Length" Then
                                If LEDEdit_Advanced.DelayConvert1.Checked = True Then
                                    str = str & vbNewLine & "d " & a.NoteLength
                                End If
                            End If
                            If LEDEdit_Advanced.DelayMode2.Text = "Delta Time" Then
                                If LEDEdit_Advanced.DelayConvert1.Checked = True Then
                                    str = str & vbNewLine & "d " & a.DeltaTime
                                End If
                            End If
                            If LEDEdit_Advanced.DelayMode2.Text = "Absolute Time" Then
                                If LEDEdit_Advanced.DelayConvert1.Checked = True Then
                                    str = str & vbNewLine & "d " & a.AbsoluteTime
                                End If
                            End If
                        Else
                            str = str & vbNewLine & "d " & a.NoteLength 'Default Option.
                        End If
#End Region 'LEDEdit_AdvancedMode_DelayOption
                        'DELAY 문제...
                        'NoteLength = 노트 길이, DeltaTime = 노트와의 간격 시간, AbsoulteTime = 절대적인 시간
                        '셋 다 DELAY는 아님. NoteLength나 DeltaTime을 Delay로 바꾸는 알고리즘이 필요.
                        '그런데 문제가 그 알고리즘을 만드는 것이 매우 힘듬.
                    ElseIf (mdEvent.CommandCode = MidiCommandCode.NoteOff) Then
                        Dim a = DirectCast(mdEvent, NoteEvent)
                        str = str & vbNewLine & "f " & a.NoteNumber & " " & UniNoteNumber
                    End If
                Next
            Next
            UniLED_Edit.Text = str

            If Regex.IsMatch(str, "8192") Then '8192 = Non-UniNoteNumber
                UniLED_Edit.Text = str.Replace(" 8192", "").Trim() 'MC LED Convert.
                UniLED_Edit.Text = UniLED_Edit.Text.Replace("o ", "o mc ").Trim() 'On MC LED Convert.
                UniLED_Edit.Text = UniLED_Edit.Text.Replace("f ", "f mc ").Trim() 'Off MC LED Convert.
            End If

        Catch ex As Exception
            MessageBox.Show("Converting Failed. Error Code: Unknown" & vbNewLine & "Warning: " & ex.Message, "UniConverter: Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub RefButton1_Click(sender As Object, e As EventArgs) Handles OpenButton1.Click
        'FileName OPEN.
        Dim ofd1 As New OpenFileDialog()

        ofd1.Filter = "Sound Files|*.mid"
        ofd1.Title = "Select Ableton LED File"
        ofd1.Multiselect = True

        If ofd1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            If Dir("Workspace\ableproj\CoLED", vbDirectory) <> "" Then
                My.Computer.FileSystem.DeleteDirectory("Workspace\ableproj\CoLED", FileIO.DeleteDirectoryOption.DeleteAllContents)
                My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\CoLED")
OpenLine:
                LED_ListView.Items.Clear()
                UniLED_Edit.Clear()
                UniLED1.Text = "File Name: None"
                If UniLED_Edit.Enabled = True Then UniLED_Edit.Enabled = False

                For i = 0 To ofd1.FileNames.Length - 1
                    IO.File.Copy(ofd1.FileNames(i), "Workspace\ableproj\CoLED\" & ofd1.FileNames(i).Split("\").Last, True)
                Next
                For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\ableproj\CoLED", FileIO.SearchOption.SearchTopLevelOnly, "*.mid") 'FileName의 파일 찾기
                    Dim itm As New ListViewItem(New String() {IO.Path.GetFileName(foundFile), foundFile})
                    LED_ListView.Items.Add(itm)  '파일 이름 추가
                Next
            Else
                My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\CoLED")
                GoTo OpenLine
            End If
        End If
    End Sub

    Private Sub AdvChk_CheckedChanged(sender As Object, e As EventArgs) Handles AdvChk.CheckedChanged
        If AdvChk.Checked = True Then
            If LEDEdit_Advanced.Enabled = True Then
                If MessageBox.Show("This mode changes the LED algorithm in detail." & vbNewLine &
                        "Developers don't recommend this mode. Would you like to continue?",
                        Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    LEDEdit_Advanced.Show()
                Else
                    AdvChk.Checked = False
                End If
            End If
        End If
    End Sub

    Private Sub KeyLED_Edit_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        LEDEdit_Advanced.Enabled = True
        LEDEdit_Advanced.Hide()
    End Sub
End Class
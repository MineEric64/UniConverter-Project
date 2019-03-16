Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading
Imports NAudio.Midi
Public Class keyLED_Edit
    Inherits Form
    Dim midFile As FileInfo
    Private trd As Thread
    Private trd_DNDFiles() As String
    Private trd_FileNames() As String
    Private Sub KeyLED_Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'FileName 표시.
        Invoke(Sub()
                   For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\ableproj\CoLED", FileIO.SearchOption.SearchTopLevelOnly, "*.mid") 'FileName의 파일 찾기
                       Dim itm As New ListViewItem(New String() {Path.GetFileName(foundFile), foundFile})
                       LED_ListView.Items.Add(itm)  '파일 이름 추가
                   Next

                   LED_ListView.AllowDrop = True
               End Sub)
    End Sub

    Private Sub LED_ListView_DragEnter(sender As Object, e As DragEventArgs) Handles LED_ListView.DragEnter
        Invoke(Sub()
                   If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                       e.Effect = DragDropEffects.Copy
                   End If
               End Sub)
    End Sub

    Private Sub LED_ListView_DragDrop(sender As Object, e As DragEventArgs) Handles LED_ListView.DragDrop
        Invoke(Sub()
                   Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
                   trd_DNDFiles = files
                   trd = New Thread(AddressOf LED_OpenLEDFiles)
                   trd.SetApartmentState(ApartmentState.MTA)
                   trd.IsBackground = True
                   trd.Start()
               End Sub)
    End Sub

    Private Sub LED_OpenLEDFiles(Files() As String)
        '---Beta Code: Drag and Drop & Get File Name Only---
        Invoke(Sub()
                   Files = trd_DNDFiles
                   LED_ListView.Items.Clear()
                   UniLED_Edit.Clear()
                   UniLED1.Text = "File Name: None"
                   If UniLED_Edit.Enabled = True Then UniLED_Edit.Enabled = False
                   Invoke(Sub()
                              Loading.Show()
                              Loading.Text = Me.Text & ": Loading LED Files..."
                              Loading.DPr.Maximum = Files.Length
                              Loading.DLb.Left = 40
                              Loading.DLb.Text = "Loading LED Files..."
                              Loading.DLb.Refresh()
                          End Sub)
               End Sub)

        If Dir("Workspace\ableproj\CoLED", vbDirectory) <> "" Then My.Computer.FileSystem.DeleteDirectory("Workspace\ableproj\CoLED", FileIO.DeleteDirectoryOption.DeleteAllContents)

                   My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\CoLED")
        For i = 0 To Files.Length - 1
            File.Copy(Files(i), "Workspace\ableproj\CoLED\" & Files(i).Split("\").Last, True)
            Invoke(Sub()
                       Loading.DPr.Style = ProgressBarStyle.Continuous
                       Loading.DPr.Value += 1
                       Loading.DLb.Left = 40
                       Loading.DLb.Text = String.Format(MainProject.loading_LED_open_msg, Loading.DPr.Value, Files.Length)
                       Loading.DLb.Refresh()
                   End Sub)
        Next

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

                       Dim LEDFIleName = "Workspace\ableproj\CoLED\" & ConLEDFile
                       Dim LEDFileC As New MidiFile(LEDFIleName, False)
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
#Region "#Ableton 9 ALG1 Settings"
                                   If SelCon1.Text = "Ableton 9 ALG1" Then 'Ableton X / Y: 64를 Unipack X / Y: 1로 변환 하는 것.
                                       '[Ableton LED To UniPack LED Convert CODE] [BETA!!]'

                                       'X1, Y1 ~ X1, Y8
                                       If a.NoteNumber = 64 Then '[Ableton Button]'
                                           UniNoteNumberX = 1 '[UniPack X Button]'
                                           UniNoteNumberY = 1 '[UniPack Y Button]'
                                       End If

                                       If a.NoteNumber = 65 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 66 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 67 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 96 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 97 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 98 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 99 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 8
                                       End If

                                       'X2, Y1 ~ X2, Y8
                                       If a.NoteNumber = 60 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 61 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 62 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 63 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 92 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 93 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 94 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 95 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 8
                                       End If

                                       'X3, Y1 ~ X3, Y8
                                       If a.NoteNumber = 56 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 57 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 58 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 59 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 88 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 89 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 90 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 91 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 8
                                       End If

                                       'X4, Y1 ~ X4, Y8
                                       If a.NoteNumber = 52 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 53 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 54 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 55 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 84 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 85 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 86 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 87 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 8
                                       End If

                                       'X5, Y1 ~ X5, Y8
                                       If a.NoteNumber = 48 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 49 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 50 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 51 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 80 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 81 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 82 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 83 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 8
                                       End If

                                       'X6, Y1 ~ X6, Y8
                                       If a.NoteNumber = 44 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 45 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 46 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 47 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 76 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 77 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 78 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 79 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 8
                                       End If

                                       'X7, Y1 ~ X7, Y8
                                       If a.NoteNumber = 40 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 41 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 42 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 43 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 72 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 73 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 74 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 75 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 8
                                       End If

                                       'X8, Y1 ~ X8, Y8
                                       If a.NoteNumber = 36 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 37 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 38 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 39 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 68 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 69 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 70 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 71 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 8
                                       End If

                                       'MC9 ~ MC16
                                       If a.NoteNumber = 100 Then
                                           UniNoteNumberX = 9
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 101 Then
                                           UniNoteNumberX = 10
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 102 Then
                                           UniNoteNumberX = 11
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 103 Then
                                           UniNoteNumberX = 12
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 104 Then
                                           UniNoteNumberX = 13
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 105 Then
                                           UniNoteNumberX = 14
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 106 Then
                                           UniNoteNumberX = 15
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 107 Then
                                           UniNoteNumberX = 16
                                           UniNoteNumberY = 8192
                                       End If

                                   End If
#End Region
#Region "Ableton 9 ALG2 Settings"
                                   If SelCon1.Text = "Ableton 9 ALG2" Then

                                   End If
#End Region
                                   If Not SelCon1.SelectedItem.ToString = "Non-Convert (Developer Mode)" Then
                                       str = str & vbNewLine & "o " & UniNoteNumberX & " " & UniNoteNumberY & " a " & a.Velocity
                                   Else
                                       str = str & vbNewLine & "o " & a.NoteNumber & " a " & a.Velocity
                                   End If
#Region "Advanced Delay Mode"
                                   If AdvChk.Checked = True Then
                                       If LEDEdit_Advanced.DelayMode1.Text = "Note Length" Then
                                           If LEDEdit_Advanced.DelayConvert1_1.Checked = True Then
                                               str = str & vbNewLine & "d " & a.NoteLength
                                           ElseIf LEDEdit_Advanced.DelayConvert1_2.Checked = True Then
                                               If Not a.DeltaTime = 0 Then
                                                   Dim bpm, ppq As Integer
                                                   bpm = 120
                                                   ppq = 192
                                                   str = str & vbNewLine & "d " & Math.Truncate(a.NoteLength * 60000 / 192 / bpm)
                                               End If
                                           End If
                                       End If
                                       If LEDEdit_Advanced.DelayMode1.Text = "Delta Time" Then
                                           If LEDEdit_Advanced.DelayConvert2_1.Checked = True Then
                                               str = str & vbNewLine & "d " & a.DeltaTime
                                           End If
                                       End If
                                       If LEDEdit_Advanced.DelayMode1.Text = "Absolute Time" Then
                                           If LEDEdit_Advanced.DelayConvert3_1.Checked = True Then
                                               str = str & vbNewLine & "d " & a.AbsoluteTime
                                           ElseIf LEDEdit_Advanced.DelayConvert3_2.Checked = True Then
                                               Dim bpm As Integer = 120
                                               Dim ppq = LEDFileC.DeltaTicksPerQuarterNote
                                               Dim r As Integer = ppq * bpm
                                               str = str & vbNewLine & "d " & Math.Truncate(a.AbsoluteTime * 60000 / r)
                                               'str = str & vbNewLine & "d " & Math.Truncate(a.AbsoluteTime / LEDFileC.DeltaTicksPerQuarterNote * 120)
                                               'str = str & vbNewLine & "d " & Math.Truncate(((a.AbsoluteTime - LastTempoEvent.AbsoluteTime) / LEDFileC.DeltaTicksPerQuarterNote) * 120 + LastTempoEvent.RealTime)
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
                               ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then
                                   Dim a = DirectCast(mdEvent, NoteEvent)
#Region "#Ableton 9 ALG1 Settings"
                                   If SelCon1.Text = "Ableton 9 ALG1" Then 'Ableton X / Y: 64를 Unipack X / Y: 1로 변환 하는 것.
                                       '[Ableton LED To UniPack LED Convert CODE] [BETA!!]'

                                       'X1, Y1 ~ X1, Y8
                                       If a.NoteNumber = 64 Then '[Ableton Button]'
                                           UniNoteNumberX = 1 '[UniPack X Button]'
                                           UniNoteNumberY = 1 '[UniPack Y Button]'
                                       End If

                                       If a.NoteNumber = 65 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 66 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 67 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 96 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 97 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 98 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 99 Then
                                           UniNoteNumberX = 1
                                           UniNoteNumberY = 8
                                       End If

                                       'X2, Y1 ~ X2, Y8
                                       If a.NoteNumber = 60 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 61 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 62 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 63 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 92 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 93 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 94 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 95 Then
                                           UniNoteNumberX = 2
                                           UniNoteNumberY = 8
                                       End If

                                       'X3, Y1 ~ X3, Y8
                                       If a.NoteNumber = 56 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 57 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 58 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 59 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 88 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 89 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 90 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 91 Then
                                           UniNoteNumberX = 3
                                           UniNoteNumberY = 8
                                       End If

                                       'X4, Y1 ~ X4, Y8
                                       If a.NoteNumber = 52 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 53 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 54 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 55 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 84 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 85 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 86 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 87 Then
                                           UniNoteNumberX = 4
                                           UniNoteNumberY = 8
                                       End If

                                       'X5, Y1 ~ X5, Y8
                                       If a.NoteNumber = 48 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 49 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 50 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 51 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 80 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 81 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 82 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 83 Then
                                           UniNoteNumberX = 5
                                           UniNoteNumberY = 8
                                       End If

                                       'X6, Y1 ~ X6, Y8
                                       If a.NoteNumber = 44 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 45 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 46 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 47 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 76 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 77 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 78 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 79 Then
                                           UniNoteNumberX = 6
                                           UniNoteNumberY = 8
                                       End If

                                       'X7, Y1 ~ X7, Y8
                                       If a.NoteNumber = 40 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 41 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 42 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 43 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 72 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 73 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 74 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 75 Then
                                           UniNoteNumberX = 7
                                           UniNoteNumberY = 8
                                       End If

                                       'X8, Y1 ~ X8, Y8
                                       If a.NoteNumber = 36 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 1
                                       End If

                                       If a.NoteNumber = 37 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 2
                                       End If

                                       If a.NoteNumber = 38 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 3
                                       End If

                                       If a.NoteNumber = 39 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 4
                                       End If

                                       If a.NoteNumber = 68 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 5
                                       End If

                                       If a.NoteNumber = 69 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 6
                                       End If

                                       If a.NoteNumber = 70 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 7
                                       End If

                                       If a.NoteNumber = 71 Then
                                           UniNoteNumberX = 8
                                           UniNoteNumberY = 8
                                       End If

                                       'MC9 ~ MC16
                                       If a.NoteNumber = 100 Then
                                           UniNoteNumberX = 9
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 101 Then
                                           UniNoteNumberX = 10
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 102 Then
                                           UniNoteNumberX = 11
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 103 Then
                                           UniNoteNumberX = 12
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 104 Then
                                           UniNoteNumberX = 13
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 105 Then
                                           UniNoteNumberX = 14
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 106 Then
                                           UniNoteNumberX = 15
                                           UniNoteNumberY = 8192
                                       End If

                                       If a.NoteNumber = 107 Then
                                           UniNoteNumberX = 16
                                           UniNoteNumberY = 8192
                                       End If

                                   End If
#End Region
                                   If Not SelCon1.SelectedItem.ToString = "Non-Convert (Developer Mode)" Then
                                       str = str & vbNewLine & "f " & UniNoteNumberX & " " & UniNoteNumberY
                                   Else
                                       str = str & vbNewLine & "f " & a.NoteNumber
                                   End If
                               End If
                           Next
                       Next
                       UniLED_Edit.Text = str

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

    Private Sub RefButton1_Click(sender As Object, e As EventArgs) Handles OpenButton1.Click
        'FileName OPEN.
        Dim ofd1 As New OpenFileDialog()

        ofd1.Filter = "LED Files|*.mid"
        ofd1.Title = "Select Ableton LED File"
        ofd1.Multiselect = True

        If ofd1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                trd = New Thread(AddressOf keyLED_RefreshNOpen)
                trd_FileNames = ofd1.FileNames
                trd.SetApartmentState(ApartmentState.MTA)
                trd.IsBackground = True
                trd.Start()
            Catch ex As Exception
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub keyLED_RefreshNOpen(FileNames() As String)
        Invoke(Sub()
                   FileNames = trd_FileNames

                   Invoke(Sub()
                              Loading.Show()
                              Loading.Text = MainProject.Text & ": Loading LED Files..."
                              Loading.DPr.Maximum = FileNames.Length
                              Loading.DPr.Refresh()
                              Loading.DLb.Left = 40
                              Loading.DLb.Text = "Loading LED Files..."
                              Loading.DLb.Refresh()
                          End Sub)

                   If Dir("Workspace\ableproj\CoLED", vbDirectory) <> "" Then
                       My.Computer.FileSystem.DeleteDirectory("Workspace\ableproj\CoLED", FileIO.DeleteDirectoryOption.DeleteAllContents)
                       My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\CoLED")
OpenLine:
                       LED_ListView.Items.Clear()
                       UniLED_Edit.Clear()
                       UniLED1.Text = "File Name: None"
                       If UniLED_Edit.Enabled = True Then UniLED_Edit.Enabled = False

                       For i = 0 To FileNames.Length - 1
                           File.Copy(FileNames(i), "Workspace\ableproj\CoLED\" & FileNames(i).Split("\").Last, True)
                           Invoke(Sub()
                                      Loading.DPr.Style = ProgressBarStyle.Continuous
                                      Loading.DPr.Value += 1
                                      Loading.DLb.Left = 40
                                      Loading.DLb.Text = String.Format(MainProject.loading_LED_open_msg, Loading.DPr.Value, FileNames.Length)
                                      Loading.DLb.Refresh()
                                  End Sub)
                       Next
                       Loading.DPr.Value = 0
                       For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\ableproj\CoLED", FileIO.SearchOption.SearchTopLevelOnly, "*.mid") 'FileName의 파일 찾기
                           Dim itm As New ListViewItem(New String() {Path.GetFileName(foundFile), foundFile})
                           LED_ListView.Items.Add(itm)  '파일 이름 추가
                           Invoke(Sub()
                                      Loading.DPr.Style = ProgressBarStyle.Continuous
                                      Loading.DPr.Value += 1
                                      Loading.DLb.Left = 40
                                      Loading.DLb.Text = String.Format(MainProject.loading_LED_openList_msg, Loading.DPr.Value, FileNames.Length)
                                      Loading.DLb.Refresh()
                                  End Sub)
                       Next
                       Loading.Dispose()
                   Else
                       My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\CoLED")
                       GoTo OpenLine
                   End If
               End Sub)
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
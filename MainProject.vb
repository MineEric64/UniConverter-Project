﻿Imports System.IO
Imports System.IO.Compression
Imports NAudio.Midi
Imports NAudio.Wave
Imports ICSharpCode.SharpZipLib.GZip
Imports ICSharpCode.SharpZipLib.Core
Imports System.Text
Imports System.Net
Imports System.Threading

Module modINI
    'ini 파일 구조
    Private Declare Unicode Function WritePrivateProfileString Lib "kernel32" _
    Alias "WritePrivateProfileStringW" (ByVal lpApplicationName As String,
    ByVal lpKeyName As String, ByVal lpString As String,
    ByVal lpFileName As String) As Int32

    Private Declare Unicode Function GetPrivateProfileString Lib "kernel32" _
    Alias "GetPrivateProfileStringW" (ByVal lpApplicationName As String,
    ByVal lpKeyName As String, ByVal lpDefault As String,
    ByVal lpReturnedString As String, ByVal nSize As Int32,
    ByVal lpFileName As String) As Int32

    Public Sub writeIni(ByVal iniFileName As String, ByVal Section As String, ByVal ParamName As String, ByVal ParamVal As String)
        Dim Result As Integer = WritePrivateProfileString(Section, ParamName, ParamVal, iniFileName)
    End Sub

    Public Function ReadIni(ByVal IniFileName As String, ByVal Section As String, ByVal ParamName As String, ByVal ParamDefault As String) As String
        Dim ParamVal As String = Space$(1024)
        Dim LenParamVal As Long = GetPrivateProfileString(Section, ParamName, ParamDefault, ParamVal, Len(ParamVal), IniFileName)
        ReadIni = Left$(ParamVal, LenParamVal)
    End Function
End Module

Public Class MainProject
    ''' <summary>
    '''  Developer Mode의 라이센스 파일.
    ''' </summary>
    Public Shared LicenseFile As String = Application.StartupPath & "\DeveloperMode.uni"
    Public Shared abl_ver As String
    Public Shared abl_FileName As String
    Public Shared abl_Name As String
    Public Shared abl_openedproj As Boolean
    Public Shared abl_openedsnd As Boolean

    ''' <summary>
    ''' UniConverter 최신 버전.
    ''' </summary>
    Public FileInfo As Version
    ''' <summary>
    ''' UniConverter 최신 버전 업데이트 로그.
    ''' </summary>
    Public VerLog As String
    ''' <summary>
    ''' Version.XML 파일 분석.
    ''' </summary>
    Public vxml As XDocument
    ''' <summary>
    ''' settings.ini 중 Convert Unipack 설정.
    ''' </summary>
    Dim uni_confile As String
    ''' <summary>
    ''' 특별 기호 (")
    ''' </summary>
    Public Shared ast As String = """"
    ''' <summary>
    ''' MainProject 저장 여부.
    ''' </summary>
    Dim IsSaved As Boolean

    Public Shared loading_Sound_Open_msg As String = "Loading Sound Files... ({0} / {1})"
    Public Shared loading_LED_open_msg As String = "Loading LED Files... ({0} / {1})"
    Public Shared loading_LED_openList_msg As String = "Replacing LED Files... ({0} / {1})"
    Public Shared loading_Project_Extract_msg As String = "Extracting The Project File..."
    Public Shared loading_Project_Load_msg As String = "Loading The Project File..."
    Public Shared loading_Project_DeleteTmp_msg As String = "Deleting The Tempoary Files..."
    Public Shared loading_Project_ChangeExt_msg As String = "Applying to readable Infos..."
    Public Shared loading_Project_FileName_msg As String = "Finding File Name..."

    ''' <summary>
    ''' 사운드 검색시 원본 리스트뷰.
    ''' </summary>
    Dim LLV As New ListView
    Private trd As Thread
    Private ofd_FileName As String
    Private ofd_FileNames() As String
    Private trd_ListView As ListView
    Private trd_KeyEvent_e As KeyEventArgs

    ''' <summary>
    '''  LAME으로 소리 확장자 변환. 현재 MP3toWAV 변환 가능. FileName의 경우 반드시 Application.StartupPath로 File을 지정하기 바람.
    ''' </summary>
    ''' <param name="CMDpath"></param> ex: Application.StartupPath + "\lame\cmd.exe"
    ''' <param name="LAMEpath"></param> ex: Application.StartupPath + "\lame\lame.exe"
    ''' <param name="resFile"></param> ex: Application.StartupPath + "\Workspace\Hello_World.mp3"
    ''' <param name="desFile"></param> ex: Application.StartupPath + "\Workspace\Hello_World.wav"
    ''' <param name="LameOption"></param> ex: "--preset extreme"
    ''' <param name="HideCMD"></param> ex: ""
    ''' <param name="AppStyle"></param> ex: AppWinStyle.Hide
    Public Shared Sub Lame(CMDpath As String, LAMEpath As String, resFile As String, desFile As String, LameOption As String, HideCMD As Boolean, AppStyle As AppWinStyle)
        Dim ast As String = """" 'Special Letter (")

        If HideCMD = True Then
            Shell(CMDpath + " /k " & LAMEpath & " " & ast & resFile & ast & " " & desFile & " " & LameOption, AppStyle)
        ElseIf HideCMD = False Then
            Shell(CMDpath + " /c " & LAMEpath & " " & ast & resFile & ast & " " & desFile & " " & LameOption, AppStyle)
        End If
    End Sub

    Private Sub MainProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim file_ex = Application.StartupPath + "\settings.ini"
        vxml = XDocument.Load(Application.StartupPath & "\version.xml")
        FileInfo = Version.Parse(vxml.<Update-XML>.<Update-Info>.<Version>.Value)
        VerLog = vxml.<Update-XML>.<Update-Info>.<Update-Log>.Value.TrimStart
        Me.KeyPreview = True
        IsSaved = True
        abl_openedproj = False
        abl_openedsnd = False

        'License File of Developer Mode.
        If File.Exists(LicenseFile) Then
            Me.Text = Me.Text & " (Enabled Developer Mode)"
            Info_AdvancedButton.Visible = True
        End If

        'Text of Info TextBox
        infoTB1.Text = "My Amazing Launchpad Project!" 'Title
        infoTB2.Text = "UniConverter, MineEric64, More..." 'Producer Name
        'Chain!

        'Edit>Ableton Option.
        AnyAbletonToolStripMenuItem.Checked = False
        AbletonLive9LiteToolStripMenuItem.Checked = False
        AbletonLive9TrialToolStripMenuItem.Checked = False
        AbletonLive9SuiteToolStripMenuItem.Checked = False
        AbletonLive10ToolStripMenuItem.Checked = False
        'RESET!!!

        If ReadIni(file_ex, "UCV_PATH", "AbletonVersion", "") = "AnyAbleton" Then
            AnyAbletonToolStripMenuItem.Checked = True
        End If

        If ReadIni(file_ex, "UCV_PATH", "AbletonVersion", "") = "Ableton9_Lite" Then
            AbletonLive9LiteToolStripMenuItem.Checked = True
        End If

        If ReadIni(file_ex, "UCV_PATH", "AbletonVersion", "") = "Ableton9_Trial" Then
            AbletonLive9TrialToolStripMenuItem.Checked = True
        End If

        If ReadIni(file_ex, "UCV_PATH", "AbletonVersion", "") = "Ableton9_Suite" Then
            AbletonLive9SuiteToolStripMenuItem.Checked = True
        End If

        If ReadIni(file_ex, "UCV_PATH", "AbletonVersion", "") = "Ableton10" Then
            AbletonLive10ToolStripMenuItem.Checked = True
        End If

        'Edit>Unipack Option.
        ConvertToZipUniToolStripMenuItem.Checked = False
        'RESET!!!

        If ReadIni(file_ex, "UCV_PATH", "ConvertUnipack", "") = "zip/uni" Then
            ConvertToZipUniToolStripMenuItem.Checked = True
        End If
    End Sub

    Private Sub InfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InfoToolStripMenuItem.Click
        Info.Show()
    End Sub

    Private Sub OpenSoundsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SoundsToolStripMenuItem.Click
        Dim ofd As New OpenFileDialog With {
            .Filter = "WAV Sound Files|*.wav|MP3 Sound Files|*.mp3",
            .Title = "Select Sounds",
            .Multiselect = True
        }

        If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            trd = New Thread(AddressOf OpenSounds)
            ofd_FileNames = ofd.FileNames
            trd.SetApartmentState(ApartmentState.MTA)
            trd.IsBackground = True
            trd.Start()
        End If
    End Sub

    Private Sub TutorialsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TutorialsToolStripMenuItem.Click
        MessageBox.Show("We are developing the NEW Tutorial Function." & vbNewLine & "Coming Soon...!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'z_Tutorial.Show()
    End Sub

    Private Sub SaveProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveProjectToolStripMenuItem.Click
        Save2Project(True)
    End Sub

    Private Sub KeyLEDBetaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeyLEDBetaToolStripMenuItem.Click
        Dim LEDOpen1 As New OpenFileDialog
        LEDOpen1.Filter = "Ableton LED File|*.mid"
        LEDOpen1.Title = "Select a Ableton LED File"
        LEDOpen1.AddExtension = False
        LEDOpen1.Multiselect = True

        If LEDOpen1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                ofd_FileNames = LEDOpen1.FileNames
                BGW_keyLED.RunWorkerAsync()
            Catch ex As Exception
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub BGW_keyLED_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BGW_keyLED.DoWork
        Try
            Dim FileNames = ofd_FileNames

            Invoke(Sub()
                       Loading.Show()
                       Loading.Text = Me.Text & ": Loading LED Files..."
                       FileNames = ofd_FileNames
                       Loading.DPr.Maximum = FileNames.Length
                       Loading.DLb.Left = 40
                       Loading.DLb.Text = "Loading LED Files..."
                       Loading.DLb.Refresh()
                   End Sub)

            If Dir("Workspace\ableproj\CoLED", vbDirectory) <> "" Then
                My.Computer.FileSystem.DeleteDirectory("Workspace\ableproj\CoLED", FileIO.DeleteDirectoryOption.DeleteAllContents)
                My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\CoLED")
OpenLine:
                For i = 0 To FileNames.Length - 1
                    File.Copy(FileNames(i), "Workspace\ableproj\CoLED\" & FileNames(i).Split("\").Last, True)
                    Invoke(Sub()
                               Loading.DPr.Style = ProgressBarStyle.Continuous
                               Loading.DPr.Value += 1
                               Loading.DLb.Left = 40
                               Loading.DLb.Text = String.Format(loading_LED_open_msg, Loading.DPr.Value, FileNames.Length)
                               Loading.DLb.Refresh()
                           End Sub)
                Next
                Invoke(Sub() Loading.DPr.Value = Loading.DPr.Maximum)
            Else
                My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\CoLED")
                GoTo OpenLine
            End If
        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BGW_keyLED_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGW_keyLED.RunWorkerCompleted
        Try
            If e.Error IsNot Nothing Then
                MessageBox.Show("Error - " & e.Error.Message & vbNewLine & "Error Message: " & e.Error.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ElseIf e.Cancelled Then
            Else
                keyLED_Edit.Show()
                Loading.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ExtractGZip(gzipFileName As String, targetDir As String)

        '---Beta Code: Extract GZip to Original File---

        ' Use a 4K buffer. Any larger is a waste.  
        Dim dataBuffer As Byte() = New Byte(4095) {}

        Using fs As Stream = New FileStream(gzipFileName, FileMode.Open, FileAccess.Read)
            Using gzipStream As New GZipInputStream(fs)

                ' Change this to your needs
                Dim fnOut As String = Path.Combine(targetDir, Path.GetFileNameWithoutExtension(gzipFileName))

                Using fsOut As FileStream = File.Create(fnOut)
                    StreamUtils.Copy(gzipStream, fsOut, dataBuffer)
                End Using
            End Using
        End Using
    End Sub

    Private Sub Ableton_OpenProject(ByVal FileName As String)
        '---Beta Code: Converting Ableton Project Info To Unipack Info---
        '이 Beta Convert Code는 오류가 발생할 수 있습니다.
        '주의사항을 다 보셨다면, 당신은 Editor 권한을 가질 수 있습니다.

        'Convert Ableton Project to Unipack Informations. (BETA!!!)
        FileName = ofd_FileName
        If Dir("Workspace\ableproj", vbDirectory) <> "" Then
OpenProjectLine:
            Invoke(Sub()
                       Loading.Show()
                       Loading.DLb.Left = 40
                       Loading.Text = Me.Text & ": Loading The Ableton Project File..."
                       Loading.DLb.Text = loading_Project_Load_msg
                       Loading.DLb.Refresh()
                   End Sub)
            abl_FileName = FileName
            File.Copy(FileName, "Workspace\ableproj\abl_proj.gz", True)
            Invoke(Sub()
                       Loading.DLb.Left = 40
                       Loading.DLb.Text = loading_Project_Extract_msg
                       Loading.DLb.Refresh()
                   End Sub)
            ExtractGZip("Workspace\ableproj\abl_proj.gz", "Workspace\ableproj")
            Invoke(Sub()
                       Loading.DLb.Left = 40
                       Loading.DLb.Text = loading_Project_DeleteTmp_msg
                       Loading.DLb.Refresh()
                   End Sub)
            File.Delete("Workspace\ableproj\abl_proj.gz")
            File.Delete("Workspace\ableproj\abl_proj.xml")
            Invoke(Sub()
                       Loading.DLb.Left = 40
                       Loading.DLb.Text = loading_Project_ChangeExt_msg
                       Loading.DLb.Refresh()
                   End Sub)
            File.Move("Workspace\ableproj\abl_proj", "Workspace\ableproj\abl_proj.xml")
            Invoke(Sub()
                       Loading.DLb.Left = 40
                       Loading.DLb.Text = loading_Project_DeleteTmp_msg
                       Loading.DLb.Refresh()
                   End Sub)
            File.Delete("Workspace\ableproj\abl_proj")
            'Reading Informations of Ableton Project.
            Invoke(Sub()
                       Loading.DLb.Left = 40
                       Loading.DLb.Text = loading_Project_FileName_msg
                       Loading.DLb.Refresh()
                   End Sub)
            abl_Name = Path.GetFileNameWithoutExtension(FileName)
            Invoke(Sub()
                       Loading.DLb.Left = 40
                       Loading.DLb.Text = "Loading The Ableton Project File..."
                       Loading.DLb.Refresh()
                   End Sub)

            Invoke(Sub() Loading.Dispose())
            If Not abl_openedproj = True Then
                MessageBox.Show("Ableton Project File Loaded!" & vbNewLine &
                            "You can edit info in Information Tab.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                abl_openedproj = True
            Else
                MessageBox.Show("Ableton Project File Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            'XML File Load.
            Invoke(Sub()
                       infoTB1.Text = abl_Name
                   End Sub)
        Else
            My.Computer.FileSystem.CreateDirectory("Workspace\ableproj")
            GoTo OpenProjectLine
        End If
    End Sub

    Private Sub OpenAbletonProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenAbletonProjectToolStripMenuItem.Click
        Dim alsOpen1 As New OpenFileDialog
        alsOpen1.Filter = "Ableton Project File|*.als"
        alsOpen1.Title = "Select a Ableton Project File"
        alsOpen1.AddExtension = False
        alsOpen1.Multiselect = False

        If alsOpen1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            trd = New Thread(AddressOf Ableton_OpenProject)
            ofd_FileName = alsOpen1.FileName
            trd.SetApartmentState(ApartmentState.MTA)
            trd.IsBackground = True
            trd.Start()
        End If
    End Sub

    Private Sub ConvertALSToUnipackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertALSToUnipackToolStripMenuItem.Click

        Dim Conv2 As New SaveFileDialog()
        If ConvertToZipUniToolStripMenuItem.Checked = True Then
            Conv2.Filter = "Zip File|*.zip|Unipack FIle|*.uni"
        Else
            'Anothoer Convert File Code,
        End If
        Conv2.Title = "Select Convert ALS to Unipack"
        Conv2.AddExtension = False

        Try
            If Conv2.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                'CONVERT Ableton Project to Unipack & Save Unipack (BETA!!!)
            End If

        Catch ex As Exception
            MessageBox.Show("Save Unipack Failed. Error Code: Unknown" & vbNewLine & "Warning: " & ex.Message, "UniConverter: Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Info_SaveButton_Click(sender As Object, e As EventArgs) Handles Info_SaveButton.Click
        If abl_openedproj = True Then
            Dim fs As FileStream
            Dim info As Byte()

            If Dir("Workspace\unipack", vbDirectory) <> "" Then
SaveInfoLine:
                fs = File.Create("Workspace\unipack\info")
                info = New UTF8Encoding(True).GetBytes("title=" & infoTB1.Text & vbNewLine & "buttonX=8" & vbNewLine & "buttonY=8" & vbNewLine & "producerName=" & infoTB2.Text & vbNewLine & "chain=" & infoTB3.Text & vbNewLine & "squareButton=true")
                fs.Write(info, 0, info.Length)
                fs.Close()
                MessageBox.Show("Saved info!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                My.Computer.FileSystem.CreateDirectory("Workspace\unipack")
                GoTo SaveInfoLine
            End If
        Else
            MessageBox.Show("You didn't open Ableton Project!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub AnyAbletonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnyAbletonToolStripMenuItem.Click
        Dim file_ex = Application.StartupPath + "\settings.ini"

        AnyAbletonToolStripMenuItem.Checked = False
        AbletonLive9LiteToolStripMenuItem.Checked = False
        AbletonLive9TrialToolStripMenuItem.Checked = False
        AbletonLive9SuiteToolStripMenuItem.Checked = False
        AbletonLive10ToolStripMenuItem.Checked = False

        abl_ver = "AnyAbleton"
        writeIni(file_ex, "UCV_PATH", "AbletonVersion", abl_ver)
        AnyAbletonToolStripMenuItem.Checked = True
    End Sub

    Private Sub AbletonLive9LiteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbletonLive9LiteToolStripMenuItem.Click
        Dim file_ex = Application.StartupPath + "\settings.ini"

        AnyAbletonToolStripMenuItem.Checked = False
        AbletonLive9LiteToolStripMenuItem.Checked = False
        AbletonLive9TrialToolStripMenuItem.Checked = False
        AbletonLive9SuiteToolStripMenuItem.Checked = False
        AbletonLive10ToolStripMenuItem.Checked = False

        abl_ver = "Ableton9_Lite"
        writeIni(file_ex, "UCV_PATH", "AbletonVersion", abl_ver)
        AbletonLive9LiteToolStripMenuItem.Checked = True
    End Sub

    Private Sub AbletonLive9TrialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbletonLive9TrialToolStripMenuItem.Click
        Dim file_ex = Application.StartupPath + "\settings.ini"

        AnyAbletonToolStripMenuItem.Checked = False
        AbletonLive9LiteToolStripMenuItem.Checked = False
        AbletonLive9TrialToolStripMenuItem.Checked = False
        AbletonLive9SuiteToolStripMenuItem.Checked = False
        AbletonLive10ToolStripMenuItem.Checked = False

        abl_ver = "Ableton9_Trial"
        writeIni(file_ex, "UCV_PATH", "AbletonVersion", abl_ver)
        AbletonLive9TrialToolStripMenuItem.Checked = True
    End Sub

    Private Sub AbletonLive9SuiteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbletonLive9SuiteToolStripMenuItem.Click
        Dim file_ex = Application.StartupPath + "\settings.ini"

        AnyAbletonToolStripMenuItem.Checked = False
        AbletonLive9LiteToolStripMenuItem.Checked = False
        AbletonLive9TrialToolStripMenuItem.Checked = False
        AbletonLive9SuiteToolStripMenuItem.Checked = False
        AbletonLive10ToolStripMenuItem.Checked = False

        abl_ver = "Ableton9_Suite"
        writeIni(file_ex, "UCV_PATH", "AbletonVersion", abl_ver)
        AbletonLive9SuiteToolStripMenuItem.Checked = True
    End Sub

    Private Sub AbletonLive10ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbletonLive10ToolStripMenuItem.Click
        Dim file_ex = Application.StartupPath + "\settings.ini"

        AnyAbletonToolStripMenuItem.Checked = False
        AbletonLive9LiteToolStripMenuItem.Checked = False
        AbletonLive9TrialToolStripMenuItem.Checked = False
        AbletonLive9SuiteToolStripMenuItem.Checked = False
        AbletonLive10ToolStripMenuItem.Checked = False

        abl_ver = "Ableton10"
        writeIni(file_ex, "UCV_PATH", "AbletonVersion", abl_ver)
        AbletonLive10ToolStripMenuItem.Checked = True
    End Sub

    Private Sub ConvertToZipUniToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertToZipUniToolStripMenuItem.Click
        Dim file_ex = Application.StartupPath + "\settings.ini"

        ConvertToZipUniToolStripMenuItem.Checked = False

        uni_confile = "zip/uni"
        writeIni(file_ex, "UCV_PATH", "ConvertUnipack", uni_confile)
        ConvertToZipUniToolStripMenuItem.Checked = True
    End Sub

    Private Sub CutSndButton_Click(sender As Object, e As EventArgs) Handles CutSndButton.Click
        Try
            Dim ConkeySndFile = keySound_ListView.FocusedItem.SubItems.Item(0).Text
            Dim ConSndFile = Sound_ListView.FocusedItem.SubItems.Item(0).Text

            If abl_openedsnd = True Then
                If Not ConkeySndFile = Nothing Then
                    CuttingSound.Show()
                Else
                    MessageBox.Show("You didn't select anything!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("You didn't import sounds!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Editing keySound Failed. Error Code: Unknown" & vbNewLine & "Warning: " & ex.Message, "UniConverter: Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Try
            If abl_openedsnd = True Then
                MessageBox.Show("Sorry, You can't use this function." & vbNewLine &
                        "We are developing about Converting keySound!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'SAVING CODE!!!
            Else
                MessageBox.Show("You didn't import sounds!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Save keySound Failed. Error Code: Unknown" & vbNewLine & "Warning: " & ex.Message, "UniConverter: Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub GoButton_Click(sender As Object, e As EventArgs) Handles GoButton.Click
        Try
            trd = New Thread(AddressOf keySound_GoToSound)
            trd.SetApartmentState(ApartmentState.MTA)
            trd.IsBackground = True
            trd.Start()
        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub keySound_GoToSound()
        Try
            Invoke(Sub()
                       Dim SelectedIndex As Integer = Sound_ListView.SelectedIndices.Count
                       Dim ConSndFile As ListViewItem = Sound_ListView.SelectedItems.Item(0)
                       Dim SndInfo As New WaveFileReader(Application.StartupPath & "\Workspace\unipack\sounds\" & ConSndFile.Text)

                       If abl_openedsnd = True Then
                           IsSaved = False
                           If SelectedIndex = 1 Then
                               Dim loopNum = InputBox("Please input the loop number of the new sounds." & vbNewLine & "Default is 1.", "Setting Loop Number", 1)
                               If IsNumeric(loopNum) = False Then
                                   If loopNum.Length = 1 Then
                                       MessageBox.Show("Please input number! Not character.", "Not Numeric", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                                       Exit Sub
                                   ElseIf loopNum.Length > 1 Then
                                       MessageBox.Show("Please input number! Not characters.", "Not Numeric", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                                       Exit Sub
                                   ElseIf loopNum.Length = 0 Then
                                       MessageBox.Show("Please input number! Not Null.", "Text is Null", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                                       Exit Sub
                                   End If
                               End If

                               keySound_ListView.Items.Add(New ListViewItem({keySound_ListView.Items.Count + 1, ConSndFile.Text, SndInfo.TotalTime.Minutes & ":" & SndInfo.TotalTime.Seconds & "." & SndInfo.TotalTime.Milliseconds, loopNum}))

                           ElseIf SelectedIndex > 1 Then
                               Dim loopNum = InputBox("Please input the loop number of the new sounds." & vbNewLine & "Default is 1.", "Setting Loop Number", 1)
                               If IsNumeric(loopNum) = False Then
                                   If loopNum.Length = 1 Then
                                       MessageBox.Show("Please input number! Not character.", "Not Numeric", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                                       Exit Sub
                                   ElseIf loopNum.Length > 1 Then
                                       MessageBox.Show("Please input number! Not characters.", "Not Numeric", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                                       Exit Sub
                                   ElseIf loopNum.Length = 0 Then
                                       MessageBox.Show("Please input number! Not Null.", "Text is Null", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                                       Exit Sub
                                   End If
                               End If

                               For i As Integer = 0 To SelectedIndex - 1
                                   ConSndFile = Sound_ListView.SelectedItems.Item(i)
                                   SndInfo = New WaveFileReader(Application.StartupPath & "\Workspace\unipack\sounds\" & ConSndFile.Text)
                                   keySound_ListView.Items.Add(New ListViewItem({keySound_ListView.Items.Count + 1, ConSndFile.Text, SndInfo.TotalTime.Minutes & ":" & SndInfo.TotalTime.Seconds & "." & SndInfo.TotalTime.Milliseconds, loopNum}))
                               Next
                           End If
                       Else
                           MessageBox.Show("You didn't import sounds!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                       End If
                   End Sub)
        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click
        Try
            Dim ConkeySndFile = keySound_ListView.FocusedItem.SubItems.Item(0).Text

            If abl_openedsnd = True Then
                If Not ConkeySndFile = Nothing Then
                    MessageBox.Show("Sorry, You can't use this function." & vbNewLine &
                        "We are developing about Converting keySound!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub

                    IsSaved = False
                    'BACK SOUND CODE!!!
                Else
                    MessageBox.Show("You didn't select anything!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("You didn't import sounds!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Converting Failed. Error Code: Unknown" & vbNewLine & "Warning: " & ex.Message, "UniConverter: Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub EdKeysButton_Click(sender As Object, e As EventArgs) Handles EdKeysButton.Click
        If abl_openedsnd = True Then
            If IsSaved = False Then
                Dim result As DialogResult = MessageBox.Show("You didn't save your UniPack's Sounds. Would you like to save your UniPack's Sounds?", Me.Text & ": Not Saved", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    SaveSounds(False)
                End If
            End If
            EditkeySound.Show()
        Else
            MessageBox.Show("You didn't import sounds!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Public Sub SaveSounds(Waiting As Boolean)
        Try
            For Each unipack_sounds As ListViewItem In keySound_ListView.Items
                Dim keySoundTxt As String = File.ReadAllText(Application.StartupPath & "\Workspace\unipack\keySound")

            Next

        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub OpenSounds(ByVal FileNames() As String)
        Invoke(Sub()
                   Loading.Show()
                   Loading.Text = Me.Text & ": Loading Sound Files..."
                   FileNames = ofd_FileNames
                   Loading.DPr.Maximum = FileNames.Length
                   Loading.DLb.Left = 40
                   Loading.DLb.Text = "Loading Sound Files..."
                   Loading.DLb.Refresh()
               End Sub)

        If Path.GetExtension(FileNames(FileNames.Length - 1)) = ".wav" Then

            If My.Computer.FileSystem.DirectoryExists("Workspace\unipack\sounds") = True Then
                My.Computer.FileSystem.DeleteDirectory("Workspace\unipack\sounds", FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If

            My.Computer.FileSystem.CreateDirectory("Workspace\unipack\sounds")


            For i = 0 To FileNames.Length - 1
                File.Copy(FileNames(i), "Workspace\unipack\sounds\" & FileNames(i).Split("\").Last, True)
                Invoke(Sub()
                           Loading.DPr.Style = ProgressBarStyle.Continuous
                           Loading.DPr.Value += 1
                           Loading.DLb.Left = 40
                           Loading.DLb.Text = String.Format(loading_Sound_Open_msg, Loading.DPr.Value, FileNames.Length)
                           Loading.DLb.Refresh()
                       End Sub)
            Next

        ElseIf Path.GetExtension(FileNames(FileNames.Length - 1)) = ".mp3" Then
            If My.Computer.FileSystem.DirectoryExists("Workspace\unipack\sounds") = True Then
                My.Computer.FileSystem.DeleteDirectory("Workspace\unipack\sounds", FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
            My.Computer.FileSystem.CreateDirectory("Workspace\unipack\sounds")

            For i = 0 To FileNames.Length - 1
                File.Copy(FileNames(i), "Workspace\" & FileNames(i).Split("\").Last.Replace(" ", "").Trim(), True)
            Next

            For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\", FileIO.SearchOption.SearchTopLevelOnly, "*.mp3")
                Lame("lame\cmd.exe", "lame\lame.exe", foundFile.Replace(Application.StartupPath + "\", ""), foundFile.Replace(".mp3", ".wav").Replace(Application.StartupPath + "\", ""), "--preset extreme", False, AppWinStyle.Hide)
            Next

            Try
fexLine:
                For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\", FileIO.SearchOption.SearchTopLevelOnly, "*.mp3")
                    If File.Exists(foundFile.Replace(".mp3", ".wav")) Then
                        File.Move(foundFile.Replace(".mp3", ".wav"), "Workspace\unipack\sounds\" & Path.GetFileName(foundFile.Replace(".mp3", ".wav")))
                        File.Delete(foundFile)
                        Invoke(Sub()
                                   Loading.DPr.Style = ProgressBarStyle.Continuous
                                   Loading.DPr.Value += 1
                                   Loading.DLb.Left = 40
                                   Loading.DLb.Text = String.Format(loading_Sound_Open_msg, Loading.DPr.Value, ofd.FileNames.Length)
                                   Loading.DLb.Refresh()
                               End Sub)
                    End If
                Next
            Catch fex As IOException 'I/O 오류 해결 코드.
                Thread.Sleep(100)
                GoTo fexLine
            End Try
        End If

        '-After Loading WAV/MP3!
        Invoke(Sub()
                   Loading.DPr.Value = Loading.DPr.Maximum
                   If Loading.DPr.Value = FileNames.Length Then
                       If FileNames.Length = Directory.GetFiles(Application.StartupPath & "\Workspace\unipack\sounds\", "*.wav").Length Then
                           If Not abl_openedsnd = True Then
                               Sound_ListView.Items.Clear()
                               keySound_ListView.Items.Clear()
                               For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\unipack\sounds", FileIO.SearchOption.SearchTopLevelOnly, "*.wav")
                                   Dim SndInfo As New WaveFileReader(foundFile)
                                   Sound_ListView.Items.Add(New ListViewItem({Path.GetFileName(foundFile), SndInfo.TotalTime.Minutes & ":" & SndInfo.TotalTime.Seconds & "." & SndInfo.TotalTime.Milliseconds, ""}))
                               Next

                               Loading.Dispose()
                               MessageBox.Show("Sounds Loaded!" & vbNewLine &
                    "You can edit keySound in keySound Tab.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                               abl_openedsnd = True

                               For Each itm As ListViewItem In Sound_ListView.Items
                                   LLV.Items.Add(New ListViewItem({itm.SubItems(0).Text, itm.SubItems(1).Text, itm.SubItems(2).Text}))
                               Next
                           Else
                               Sound_ListView.Items.Clear()
                               keySound_ListView.Items.Clear()


                               For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\unipack\sounds", FileIO.SearchOption.SearchTopLevelOnly, "*.wav")
                                   Dim SndInfo As New WaveFileReader(foundFile)
                                   Sound_ListView.Items.Add(New ListViewItem({Path.GetFileName(foundFile), SndInfo.TotalTime.Minutes & ":" & SndInfo.TotalTime.Seconds & "." & SndInfo.TotalTime.Milliseconds, ""}))
                               Next

                               Loading.Dispose()
                               MessageBox.Show("Sounds Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                               abl_openedsnd = True

                               For Each itm As ListViewItem In Sound_ListView.Items
                                   LLV.Items.Add(New ListViewItem({itm.SubItems(0).Text, itm.SubItems(1).Text, itm.SubItems(2).Text}))
                               Next
                           End If
                       Else
                           MessageBox.Show("Error! - Code: MaxFileLength.Value = GetFiles.Length", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                       End If
                   Else
                       MessageBox.Show("Error! - Code: LoadedFiles.Value = MaxFileLength.Value", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                   End If
               End Sub)
    End Sub

    Private Sub OpenProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenProjectToolStripMenuItem.Click

    End Sub

    Private Sub CheckUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckUpdateToolStripMenuItem.Click
        CheckUpdate()

        If My.Computer.Network.IsAvailable = True Then
            If My.Application.Info.Version = FileInfo Then
                MessageBox.Show("You are using a Latest Version." & vbNewLine & vbNewLine &
                       "Current Version : " & My.Application.Info.Version.ToString & vbNewLine & "Latest Version : " & FileInfo.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf My.Application.Info.Version > FileInfo Then
                MessageBox.Show("You are using a Test Version!" & vbNewLine & vbNewLine & "Current Version : " & FileInfo.ToString & vbNewLine &
                       "Your Test Version : " & My.Application.Info.Version.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("Network Connect Failed! Can't Check Update.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Public Sub CheckUpdate()
        Dim Client As New WebClient

        If My.Computer.Network.IsAvailable = True Then
            Client.DownloadFile("http://dver.ucv.kro.kr", Application.StartupPath & "\version.xml")
            FileInfo = Version.Parse(vxml.<Update-XML>.<Update-Info>.<Version>.Value)
            VerLog = vxml.<Update-XML>.<Update-Info>.<Update-Log>.Value.TrimStart
            If My.Application.Info.Version < FileInfo Then
                If MessageBox.Show("New Version " & FileInfo.ToString & " is Available!" & vbNewLine & "Current Version : " & My.Application.Info.Version.ToString & vbNewLine & "Latest Version : " & FileInfo.ToString & vbNewLine &
                                 vbNewLine & "Update Log:" & vbNewLine & VerLog, Me.Text & ": Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    With Loading
                        .Show()
                        .Text = "Downloading UniConverter V" & FileInfo.ToString
                        .DPr.Refresh()
                        .DLb.Text = "Downloading UniConverter V" & FileInfo.ToString & " ..."
                        .DLb.Left = 20
                        .DLb.Refresh()
                    End With
                    Client.DownloadFile("http://dprg0.ucv.kro.kr", My.Computer.FileSystem.SpecialDirectories.Temp & "\UniConverter-Update.zip")
                    Loading.DPr.Style = ProgressBarStyle.Continuous
                    Loading.DPr.Value = 800

                    If Dir(Application.StartupPath & "\UniConverter_v" & FileInfo.ToString, vbDirectory) <> "" Then
                        If File.Exists(Application.StartupPath & "\UniConverter_v" & FileInfo.ToString & "\UniConverter.exe") Then
                            My.Computer.FileSystem.DeleteDirectory(Application.StartupPath & "\UniConverter_v" & FileInfo.ToString, FileIO.DeleteDirectoryOption.DeleteAllContents)
                            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\UniConverter_v" & FileInfo.ToString)
                            ZipFile.ExtractToDirectory(My.Computer.FileSystem.SpecialDirectories.Temp & "\UniConverter-Update.zip", "UniConverter_v" & FileInfo.ToString)
                        Else
                            ZipFile.ExtractToDirectory(My.Computer.FileSystem.SpecialDirectories.Temp & "\UniConverter-Update.zip", "UniConverter_v" & FileInfo.ToString)
                        End If
                    Else
                        My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\UniConverter_v" & FileInfo.ToString)
                        ZipFile.ExtractToDirectory(My.Computer.FileSystem.SpecialDirectories.Temp & "\UniConverter-Update.zip", "UniConverter_v" & FileInfo.ToString)
                    End If
                    With Loading
                        .DPr.Value = 1000
                        If .DPr.Value = 1000 Then
                            .DLb.Left = 120
                            .DLb.Text = "Update Complete!"
                            If MessageBox.Show("Update Complete! UniConverter " & FileInfo.ToString & " is in 'UniConverter_v" & FileInfo.ToString & "' Folder.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) = DialogResult.OK Then
                                File.Delete(My.Computer.FileSystem.SpecialDirectories.Temp & "\UniConverter-Update.zip")
                                .Dispose()
                            End If
                        End If
                    End With
                End If
            End If
        Else
            MessageBox.Show("Network Connect Failed! Can't Check Update.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ReportBugsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportBugsToolStripMenuItem.Click
        REPORTForm.Show()
    End Sub

    Private Sub MainProject_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If IsSaved = False Then
            Dim result As DialogResult = MessageBox.Show("You didn't save your UniPack. Would you like to save your UniPack?", Me.Text & ": Not Saved", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                Save2Project(False)
            ElseIf result = DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub Save2Project(Waiting As Boolean)
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "Zip File|*.zip|Unipack File|*.uni"
        sfd.Title = "Select Save Unipack"
        sfd.AddExtension = False

        Try
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\Workspace\unipack") Then
                    If Waiting = True Then
                        Invoke(Sub()
                                   Loading.Show()
                                   Loading.Text = Me.Text & ": Saving Ableton Project File to UniPack..."
                                   Loading.DLb.Left = 40
                                   Dim result As String = Path.GetExtension(sfd.FileName)
                                   If result = ".zip" Then
                                       Loading.DLb.Text = "Creating UniPack to zip File..."
                                   ElseIf result = ".uni" Then
                                       Loading.DLb.Text = "Creating UniPack to uni File..."
                                   End If
                                   Loading.DLb.Refresh()
                               End Sub)
                    End If
                    ZipFile.CreateFromDirectory(Application.StartupPath & "\Workspace\unipack", sfd.FileName)
                    If Waiting = True Then
                        IsSaved = True
                        MessageBox.Show("Saved Unipack!", "UniConverter", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Save Unipack Failed. Error Code: Unknown" & vbNewLine & "Warning: " & ex.Message, "UniConverter: Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub infoTB1_TextChanged(sender As Object, e As EventArgs) Handles infoTB1.TextChanged
        IsSaved = False
    End Sub

    Private Sub infoTB2_TextChanged(sender As Object, e As EventArgs) Handles infoTB2.TextChanged
        IsSaved = False
    End Sub

    Private Sub ks_SearchSound_TextChanged(sender As Object, e As EventArgs) Handles ks_SearchSound.TextChanged
        Dim LV As New ListView
        If Not ks_SearchSound.Text = "" Then
            Dim loi = 1
            For i As Integer = 0 To Sound_ListView.Items.Count - 1

                For Each itm As ListViewItem In LLV.Items
                    LV.Items.Add(New ListViewItem({itm.SubItems(0).Text, itm.SubItems(1).Text, itm.SubItems(2).Text}))
                Next

                Dim Find_Sounds As ListViewItem = LV.Items.Item(i)
                Dim FndSnd As String = Find_Sounds.SubItems(2).Text
                If Find_Sounds.SubItems(0).Text.Contains(ks_SearchSound.Text) Then
                    Dim SndInfo As New WaveFileReader(Application.StartupPath & "\Workspace\unipack\sounds\" & Find_Sounds.SubItems(0).Text)
                    Dim AssingedButtons As String = FndSnd
                    If loi = 1 Then
                        Sound_ListView.Items.Clear()
                        Sound_ListView.Items.Add(New ListViewItem({Find_Sounds.Text, SndInfo.TotalTime.Minutes & ":" & SndInfo.TotalTime.Seconds & "." & SndInfo.TotalTime.Milliseconds, AssingedButtons}))
                        loi = 0
                    Else
                        Sound_ListView.Items.Add(New ListViewItem({Find_Sounds.Text, SndInfo.TotalTime.Minutes & ":" & SndInfo.TotalTime.Seconds & "." & SndInfo.TotalTime.Milliseconds, AssingedButtons}))
                    End If
                End If
            Next
        Else
            Sound_ListView.Items.Clear()
            For Each recover_sounds As ListViewItem In LLV.Items
                Sound_ListView.Items.Add(New ListViewItem({recover_sounds.SubItems(0).Text, recover_sounds.SubItems(1).Text, recover_sounds.SubItems(2).Text}))
            Next
        End If
    End Sub

    Private Sub Sound_ListView_MouseDoubleClick(sender As Object, e As EventArgs) Handles Sound_ListView.MouseDoubleClick
        Dim SelectedItem As ListViewItem = Sound_ListView.SelectedItems.Item(0)

        My.Computer.Audio.Play(Application.StartupPath & "\Workspace\unipack\sounds\" & SelectedItem.SubItems(0).Text, AudioPlayMode.Background)
    End Sub

    Private Sub keySound_ListView_MouseDoubleClick(sender As Object, e As EventArgs) Handles keySound_ListView.MouseDoubleClick
        Dim SelectedItem As ListViewItem = keySound_ListView.SelectedItems.Item(0)

        My.Computer.Audio.Play(Application.StartupPath & "\Workspace\unipack\sounds\" & SelectedItem.SubItems(1).Text, AudioPlayMode.Background)
    End Sub

    Private Sub Sound_ListView_KeyDown(sender As Object, e As KeyEventArgs) Handles Sound_ListView.KeyDown
        trd = New Thread(AddressOf Sound_ListView_KeyLists)
        trd_KeyEvent_e = e
        trd.SetApartmentState(ApartmentState.MTA)
        trd.IsBackground = True
        trd.Start()
    End Sub

    Private Sub keySound_ListView_KeyDown(sender As Object, e As KeyEventArgs) Handles keySound_ListView.KeyDown
        e.Handled = True
        If e.KeyCode = Keys.A AndAlso e.Control = True Then
            trd = New Thread(AddressOf SelectAllItems)
            trd_ListView = keySound_ListView
            trd.SetApartmentState(ApartmentState.MTA)
            trd.IsBackground = True
            trd.Start()
        End If
    End Sub

    Private Sub Sound_ListView_KeyLists(e As KeyEventArgs)
        e = trd_KeyEvent_e
        e.Handled = True
        Invoke(Sub()
                   Select Case e.KeyCode
                       Case Keys.Delete
                           Dim SelectedIndex As Integer = Sound_ListView.SelectedIndices.Count
                           If SelectedIndex = 1 Then
                               Sound_ListView.Items.RemoveAt(SelectedIndex - 1)
                           ElseIf SelectedIndex > 1 Then
                               For i As Integer = 0 To SelectedIndex - 1
                                   Sound_ListView.Items.RemoveAt(0)
                               Next
                           End If
                   End Select

                   If e.KeyCode = Keys.A AndAlso e.Control = True Then
                       trd = New Thread(AddressOf SelectAllItems)
                       trd_ListView = Sound_ListView
                       trd.SetApartmentState(ApartmentState.MTA)
                       trd.IsBackground = True
                       trd.Start()
                   End If
               End Sub)
    End Sub

    Public Sub SelectAllItems(ListView As ListView)
        Try
            ListView = trd_ListView
            Invoke(Sub()
                       For i = 0 To ListView.Items.Count - 1
                           ListView.Items(i).Selected = True
                       Next i
                   End Sub)
        Catch ex As Exception
            MessageBox.Show("Error! - " & ex.Message & vbNewLine & ex.StackTrace,
        Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Info_AdvancedButton_Click(sender As Object, e As EventArgs) Handles Info_AdvancedButton.Click
        DeveloperMode_Project.Show()
    End Sub
End Class
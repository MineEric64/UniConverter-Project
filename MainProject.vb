Imports System.IO
Imports System.IO.Compression
Imports NAudio.Midi
Imports ICSharpCode.SharpZipLib.GZip
Imports ICSharpCode.SharpZipLib.Core
Imports System.Text

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
    Dim abl_ver As String
    Dim abl_FileName As String
    Dim abl_openedproj As Boolean
    Public abl_openedsnd As Boolean
    Dim uni_confile As String

    'Dim fslog As FileStream = File.Create("log.txt") '(fslog를 선언할경우 log.txt 만들어져서 일단 일시적으로 주석)
    'Dim infofs As Byte() '(fslog와 마찬가지로 선언)
    Dim ast As String = """" 'Special Letter (")
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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim curFile As String = "Sources\DeveloperMode.uni" 'License File of Developer Mode.
        Dim file_ex = Application.StartupPath + "\settings.ini"
        abl_openedproj = False
        abl_openedsnd = False

        'Devloper Mode
        If File.Exists(curFile) Then
            '개발자 툴 지원
            DeveloperToolStripMenuItem.Visible = True
            DeveloperToolStripMenuItem.Enabled = True
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
        OpenSounds()
    End Sub

    Private Sub DeveloperToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeveloperToolStripMenuItem.Click
        DeveloperMode.Show()
    End Sub

    Private Sub TutorialsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TutorialsToolStripMenuItem.Click
        Tutorial.Show()
    End Sub

    Private Sub SaveProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveProjectToolStripMenuItem.Click
        Dim SaveFileDialog1 As New SaveFileDialog()
        SaveFileDialog1.Filter = "Zip File|*.zip|Unipack File|*.uni"
        SaveFileDialog1.Title = "Select Save Unipack"
        SaveFileDialog1.AddExtension = False

        Try
            If SaveFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                If Dir("Workspace\unipack", vbDirectory) <> "" Then
                    ZipFile.CreateFromDirectory("Workspace\unipack", SaveFileDialog1.FileName)
                    MessageBox.Show("Saved Unipack!", "UniConverter", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Save Unipack Failed. Error Code: 9" & vbNewLine &
                                    "Warning: There's no file to save project.", "UniConverter: Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Save Unipack Failed. Error Code: Unknown" & vbNewLine & "Warning: " & ex.Message, "UniConverter: Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub KeyLEDBetaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeyLEDBetaToolStripMenuItem.Click
        Dim LEDOpen1 As New OpenFileDialog
        LEDOpen1.Filter = "Ableton LED File|*.mid"
        LEDOpen1.Title = "Select a Ableton LED File"
        LEDOpen1.AddExtension = False
        LEDOpen1.Multiselect = True

        If LEDOpen1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            If Dir("Workspace\ableproj\CoLED", vbDirectory) <> "" Then
                My.Computer.FileSystem.DeleteDirectory("Workspace\ableproj\CoLED", FileIO.DeleteDirectoryOption.DeleteAllContents)
                My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\CoLED")
OpenLine:
                For i = 0 To LEDOpen1.FileNames.Length - 1
                    IO.File.Copy(LEDOpen1.FileNames(i), "Workspace\ableproj\CoLED\" & LEDOpen1.FileNames(i).Split("\").Last, True)
                Next
                keyLED_Edit.Show()
            Else
                My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\CoLED")
                GoTo OpenLine
            End If
        End If
    End Sub

    Private Sub keyLED1_Button_Click(sender As Object, e As EventArgs)
        keyLED_Edit.Show()
    End Sub

    Public Sub ExtractGZip(gzipFileName As String, targetDir As String)

        '---Beta Code: Extract GZip to Original File---

        ' Use a 4K buffer. Any larger is a waste.  
        Dim dataBuffer As Byte() = New Byte(4095) {}

        Using fs As System.IO.Stream = New FileStream(gzipFileName, FileMode.Open, FileAccess.Read)
            Using gzipStream As New GZipInputStream(fs)

                ' Change this to your needs
                Dim fnOut As String = Path.Combine(targetDir, Path.GetFileNameWithoutExtension(gzipFileName))

                Using fsOut As FileStream = File.Create(fnOut)
                    StreamUtils.Copy(gzipStream, fsOut, dataBuffer)
                End Using
            End Using
        End Using
    End Sub

    Private Sub OpenAbletonProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenAbletonProjectToolStripMenuItem.Click
        Dim alsOpen1 As New OpenFileDialog
        alsOpen1.Filter = "Ableton Project File|*.als"
        alsOpen1.Title = "Select a Ableton Project File"
        alsOpen1.AddExtension = False
        alsOpen1.Multiselect = False

        If alsOpen1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            '---Beta Code: Converting mid to keyLED---
            '이 Beta Convert Code는 오류가 발생할 수 있습니다.
            '주의사항을 다 보셨다면, 당신은 Editor 권한을 가질 수 있습니다.

            'Convert Ableton Project to Unipack Informations. (BETA!!!)
            If Dir("Workspace\ableproj", vbDirectory) <> "" Then
OpenProjectLine:
                abl_FileName = alsOpen1.SafeFileName.Replace(".als", "")
                File.Copy(alsOpen1.FileName, "Workspace\ableproj\abl_proj.gz", True)
                ExtractGZip("Workspace\ableproj\abl_proj.gz", "Workspace\ableproj")
                File.Delete("Workspace\ableproj\abl_proj.gz")
                If Not abl_openedproj = True Then
                    MessageBox.Show("Ableton Project File Loaded!" & vbNewLine &
                                "You can edit info in Information Tab.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    abl_openedproj = True
                Else
                    MessageBox.Show("Ableton Project File Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                'XML File Load.
                infoTB1.Text = abl_FileName
            Else
                My.Computer.FileSystem.CreateDirectory("Workspace\ableproj")
                GoTo OpenProjectLine
            End If
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
            Dim ConSndFile = Sound_ListView.FocusedItem.SubItems.Item(0).Text

            If abl_openedsnd = True Then
                If Not ConSndFile = Nothing Then
                    MessageBox.Show("Sorry, You can't use this function." & vbNewLine &
                    "We are developing about Converting keySound!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'GO SOUND CODE!!!
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

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click
        Try
            Dim ConkeySndFile = keySound_ListView.FocusedItem.SubItems.Item(0).Text

            If abl_openedsnd = True Then
                If Not ConkeySndFile = Nothing Then
                    MessageBox.Show("Sorry, You can't use this function." & vbNewLine &
                        "We are developing about Converting keySound!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            EditkeySound.Show()
        Else
            MessageBox.Show("You didn't import sounds!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Public Sub OpenSounds()
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.Filter = "WAV Sound Files|*.wav|MP3 Sound Files|*.mp3"
        openFileDialog1.Title = "Select Sounds"
        openFileDialog1.Multiselect = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            If Path.GetExtension(openFileDialog1.FileNames(openFileDialog1.FileNames.Length - 1)) = ".wav" Then

                If (My.Computer.FileSystem.DirectoryExists("Workspace\unipack\sounds") = True) Then
                    My.Computer.FileSystem.DeleteDirectory("Workspace\unipack\sounds", FileIO.DeleteDirectoryOption.DeleteAllContents)
                End If

                My.Computer.FileSystem.CreateDirectory("Workspace\unipack\sounds")

                For i = 0 To openFileDialog1.FileNames.Length - 1
                    File.Copy(openFileDialog1.FileNames(i), "Workspace\unipack\sounds\" & openFileDialog1.FileNames(i).Split("\").Last, True)
                Next

            ElseIf Path.GetExtension(openFileDialog1.FileNames(openFileDialog1.FileNames.Length - 1)) = ".mp3" Then
                If (My.Computer.FileSystem.DirectoryExists("Workspace\unipack\sounds") = True) Then
                    My.Computer.FileSystem.DeleteDirectory("Workspace\unipack\sounds", FileIO.DeleteDirectoryOption.DeleteAllContents)
                End If
                My.Computer.FileSystem.CreateDirectory("Workspace\unipack\sounds")

                For i = 0 To openFileDialog1.FileNames.Length - 1
                    File.Copy(openFileDialog1.FileNames(i), "Workspace\" & openFileDialog1.FileNames(i).Split("\").Last.Replace(" ", "").Trim(), True)
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
                        End If
                    Next
                Catch fex As IOException 'I/O 오류 해결 코드.
                    Threading.Thread.Sleep(100)
                    GoTo fexLine
                End Try
            End If

            '-After Loading WAV/MP3!
            If Not abl_openedsnd = True Then
                MessageBox.Show("Sounds Loaded!" & vbNewLine &
                        "You can edit keySound in keySound Tab.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                abl_openedsnd = True
            Else
                MessageBox.Show("Sounds Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Sound_ListView.Items.Clear()
            keySound_ListView.Items.Clear()

            For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\unipack\sounds", FileIO.SearchOption.SearchTopLevelOnly, "*.wav")
                Dim itm As New ListViewItem(New String() {Path.GetFileName(foundFile), foundFile})
                Sound_ListView.Items.Add(itm)
            Next
        End If
    End Sub
End Class
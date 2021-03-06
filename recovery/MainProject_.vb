﻿Imports System.IO
Imports System.IO.Compression
Imports NAudio.Midi
Imports NAudio.Wave
Imports ICSharpCode.SharpZipLib.GZip
Imports ICSharpCode.SharpZipLib.Core
Imports System.Net
Imports System.Threading
Imports System.Xml
Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports A2UP.A2U.keyLED_MIDEX
Imports WMPLib

Public Class MainProject

#Region "UniConverter-MainProject(s)"
    ''' <summary>
    '''  라이센스 파일. (0: Developer Mode, 1: GreatEx Mode)
    ''' </summary>
    Public Shared LicenseFile As String() = New String(1) {Application.StartupPath & "\MDSL\DeveloperMode.uni", Application.StartupPath & "\MDSL\GreatExMode.uni"}

    ''' <summary>
    ''' Developer Mode?
    ''' </summary>
    Public Shared IsDeveloperMode As Boolean = False

    ''' <summary>
    ''' Great Exception Mode!
    ''' </summary>
    Public Shared IsGreatExMode As Boolean = False

    ''' <summary>
    ''' MainProject 전체 저장 여부. (Save As Project)
    ''' </summary>
    Public Shared IsSaved As Boolean

    ''' <summary>
    ''' UniPack info 저장 여부.
    ''' </summary>
    Public Shared infoIsSaved As Boolean

    ''' <summary>
    ''' 업데이트 여부.
    ''' </summary>
    Public Shared IsUpdated As Boolean

    ''' <summary>
    ''' 한 번에 Ableton Project를 열 것인가?
    ''' </summary>
    Public OpenProjectOnce As Boolean

    ''' <summary>
    ''' 지금 매우 중요한 작업 여부.
    ''' </summary>
    Public Shared IsWorking As Boolean

    ''' <summary>
    ''' 미디 Input과 Note On 테스트 여부.
    ''' </summary>
    Public Shared IsMIDITest As Boolean

    ''' <summary>
    ''' Waiting For Ableton Project. (LED Convert: "keyLED")
    ''' </summary>
    Public Shared w8t4abl As String

#Region "MainProject-keySound(s)"
    ''' <summary>
    ''' keySound 저장 여부.
    ''' </summary>
    Public Shared SoundIsSaved As Boolean

    ''' <summary>
    ''' keySound 버튼 저장
    ''' </summary>
    Public ks_ctrl As New Dictionary(Of String, Button)

    ''' <summary>
    ''' keySound 레이아웃 여부.
    ''' </summary>
    Dim keySoundLayout As Boolean

    ''' <summary>
    ''' X 버튼 좌표. (1~8)
    ''' </summary>
    Public ksUniPack_X As Integer

    ''' <summary>
    ''' Y 버튼 좌표. (1~8)
    ''' </summary>
    Public ksUniPack_Y As Integer

    ''' <summary>
    ''' 키사운드 매핑.
    ''' </summary>
    Public keySound_Mapping As Integer

    ''' <summary>
    ''' 최대 다중 매핑 지원. (1, 같은 사운드 다중 매핑 적용)
    ''' </summary>
    Public keySound_SameM As Integer

    ''' <summary>
    ''' 최대 다중 매핑 지원. (2, 다른 사운드 다중 매핑 적용)
    ''' </summary>
    Public keySound_DifM As Integer

    ''' <summary>
    ''' 현재 체인. (1~8)
    ''' </summary>
    Public keySound_CChain As Integer

    ''' <summary>
    ''' 선택한 체인. (1~8)
    ''' </summary>
    Public ksUniPack_SelectedChain As Integer = 1

    ''' <summary>
    ''' keySound Loop 여부.
    ''' </summary>
    Dim keySoundLoop As Boolean

    ''' <summary>
    ''' keySound Play Loop 1.
    ''' </summary>
    Dim ks_PlayLoop1 As New WindowsMediaPlayer

    ''' <summary>
    ''' keySound Play Loop 2.
    ''' </summary>
    Dim ks_PlayLoop2 As New WindowsMediaPlayer

#End Region
#Region "MainProject-keyLED(s)"
    Private stopitnow As Boolean = False

    ''' <summary>
    ''' MainProject keyLED 저장 여부. (keyLED / keyLED (MIDEX))
    ''' </summary>
    Public Shared keyLEDIsSaved As Boolean
#End Region
#Region "MainProject-Thread(s)"
    Public Shared ofd_FileName As String
    Private ofd_FileNames() As String
    Private trd_ListView As ListView
#End Region

#Region "MIDI Settings"
    Public midioutput As MidiOut

    ''' <summary>
    ''' 0: Mini/S, 1: MK2, 2: Pro
    ''' </summary>
    Public midioutput_kind As Integer = 0
    Public midioutput_avail As Boolean = False

    Public WithEvents midiinput As MidiIn
    ''' <summary>
    ''' 0: Mini/S, 1: MK2, 2: Pro (0~127 단계 소리 세기 조절 기능)
    ''' </summary>
    Public midiinput_kind As Integer = 0
    Public midiinput_avail As Boolean = False
#End Region
#End Region

#Region "MainProject-Ableton(s)"
    Public Shared abl_ver As String
    Public Shared abl_FileName As String
    Public Shared abl_Name As String
    Public Shared abl_Chain As Integer
    Public Shared abl_openedproj As Boolean
    Public Shared abl_openedsnd As Boolean
    Public Shared abl_openedled As Boolean
    Public Shared abl_openedled2 As Boolean
#End Region

#Region "About XML (Settings / Version)"
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
    Public vxml As New XmlDocument

    ''' <summary>
    ''' Settings.XML 파일 분석.
    ''' </summary>
    Public setxml As New XmlDocument

    ''' <summary>
    ''' settings.xml 중 Convert UniPack 설정.
    ''' </summary>
    Private uni_confile As String

    ''' <summary>
    ''' Launchpad Setup Light. [When you connect the Launchpad]
    ''' </summary>
    Private SetUpLight_ As Boolean
#End Region

#Region "UniPack(s)"
    ''' <summary>
    ''' LED 다중 매핑 순서. (a b c ...)
    ''' </summary>
    Public Shared LEDMapping_N As Char() = New Char(25) {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
#End Region

    ''' <summary>
    ''' 특별 기호 (")
    ''' </summary>
    Public Shared ast As String = Chr(34)

    ''' <summary>
    ''' Temp 폴더.
    ''' </summary>
    Public Shared TempDirectory As String = My.Computer.FileSystem.SpecialDirectories.Temp

    ''' <summary>
    '''  LAME으로 소리 확장자 변환. 현재 MP3toWAV 변환 가능. FileName의 경우 반드시 Application.StartupPath로 File을 지정하기 바람.
    ''' </summary>
    ''' <param name="CMDpath">CMD Path. (ex: Application.StartupPath + "\lame\cmd.exe")</param>
    ''' <param name="LAMEpath">Lame Path. (ex: Application.StartupPath + "\lame\lame.exe")</param> 
    ''' <param name="resFile">Original File Path. (ex: Application.StartupPath + "\Workspace\Hello_World.mp3")</param> 
    ''' <param name="desFile">Destination File Path. (ex: Application.StartupPath + "\Workspace\Hello_World.wav")</param> 
    ''' <param name="LameOption">Lame's Option Argument. (ex: "--preset extreme")</param> 
    ''' <param name="HideCMD">Hiding CMD. (ex: True)</param>
    ''' <param name="AppStyle">CMD App Style. (ex: AppWinStyle.Hide)</param>
    Public Shared Sub Lame(CMDpath As String, LAMEpath As String, resFile As String, desFile As String, LameOption As String, HideCMD As Boolean, AppStyle As AppWinStyle)
        If HideCMD = True Then
            Shell(CMDpath + " /k " & LAMEpath & " " & ast & resFile & ast & " " & desFile & " " & LameOption, AppStyle)
        ElseIf HideCMD = False Then
            Shell(CMDpath + " /c " & LAMEpath & " " & ast & resFile & ast & " " & desFile & " " & LameOption, AppStyle)
        End If
    End Sub

    Private Sub MainProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim file_ex As String = Application.StartupPath + "\settings.xml"

            If File.Exists(file_ex) = False Then
                Throw New FileNotFoundException("Settings File doesn't exists.")
            End If

            'License File of Developer Mode.
            If File.Exists(LicenseFile(0)) AndAlso File.ReadAllText(LicenseFile(0)) = My.Resources.License_DeveloperMode Then
                IsDeveloperMode = True
            End If

            If IsDeveloperMode Then
                Me.Text = Me.Text & " (Enabled Developer Mode)"
                DeveloperModeToolStripMenuItem.Visible = True
            End If

            If File.Exists(LicenseFile(1)) AndAlso File.ReadAllText(LicenseFile(1)) = My.Resources.License_GreatExMode Then
                IsGreatExMode = True
            End If
#Region "변수 기본값 설정"
            Me.KeyPreview = True

            abl_openedproj = False
            abl_openedsnd = False
            abl_openedled = False
            abl_openedled2 = False

            IsSaved = True
            SoundIsSaved = False
            keyLEDIsSaved = False
            infoIsSaved = False
            IsUpdated = False
            IsWorking = False

            w8t4abl = String.Empty
            OpenProjectOnce = False
            IsMIDITest = False
#End Region
#Region "Dictionary 버튼 추가"
            ks_ctrl.Add(11, uni1_1)
            ks_ctrl.Add(12, uni1_2)
            ks_ctrl.Add(13, uni1_3)
            ks_ctrl.Add(14, uni1_4)
            ks_ctrl.Add(15, uni1_5)
            ks_ctrl.Add(16, uni1_6)
            ks_ctrl.Add(17, uni1_7)
            ks_ctrl.Add(18, uni1_8)
            ks_ctrl.Add(21, uni2_1)
            ks_ctrl.Add(22, uni2_2)
            ks_ctrl.Add(23, uni2_3)
            ks_ctrl.Add(24, uni2_4)
            ks_ctrl.Add(25, uni2_5)
            ks_ctrl.Add(26, uni2_6)
            ks_ctrl.Add(27, uni2_7)
            ks_ctrl.Add(28, uni2_8)
            ks_ctrl.Add(31, uni3_1)
            ks_ctrl.Add(32, uni3_2)
            ks_ctrl.Add(33, uni3_3)
            ks_ctrl.Add(34, uni3_4)
            ks_ctrl.Add(35, uni3_5)
            ks_ctrl.Add(36, uni3_6)
            ks_ctrl.Add(37, uni3_7)
            ks_ctrl.Add(38, uni3_8)
            ks_ctrl.Add(41, uni4_1)
            ks_ctrl.Add(42, uni4_2)
            ks_ctrl.Add(43, uni4_3)
            ks_ctrl.Add(44, uni4_4)
            ks_ctrl.Add(45, uni4_5)
            ks_ctrl.Add(46, uni4_6)
            ks_ctrl.Add(47, uni4_7)
            ks_ctrl.Add(48, uni4_8)
            ks_ctrl.Add(51, uni5_1)
            ks_ctrl.Add(52, uni5_2)
            ks_ctrl.Add(53, uni5_3)
            ks_ctrl.Add(54, uni5_4)
            ks_ctrl.Add(55, uni5_5)
            ks_ctrl.Add(56, uni5_6)
            ks_ctrl.Add(57, uni5_7)
            ks_ctrl.Add(58, uni5_8)
            ks_ctrl.Add(61, uni6_1)
            ks_ctrl.Add(62, uni6_2)
            ks_ctrl.Add(63, uni6_3)
            ks_ctrl.Add(64, uni6_4)
            ks_ctrl.Add(65, uni6_5)
            ks_ctrl.Add(66, uni6_6)
            ks_ctrl.Add(67, uni6_7)
            ks_ctrl.Add(68, uni6_8)
            ks_ctrl.Add(71, uni7_1)
            ks_ctrl.Add(72, uni7_2)
            ks_ctrl.Add(73, uni7_3)
            ks_ctrl.Add(74, uni7_4)
            ks_ctrl.Add(75, uni7_5)
            ks_ctrl.Add(76, uni7_6)
            ks_ctrl.Add(77, uni7_7)
            ks_ctrl.Add(78, uni7_8)
            ks_ctrl.Add(81, uni8_1)
            ks_ctrl.Add(82, uni8_2)
            ks_ctrl.Add(83, uni8_3)
            ks_ctrl.Add(84, uni8_4)
            ks_ctrl.Add(85, uni8_5)
            ks_ctrl.Add(86, uni8_6)
            ks_ctrl.Add(87, uni8_7)
            ks_ctrl.Add(88, uni8_8)

            '키사운드 레이아웃 비활성화
            PadLayoutPanel.Enabled = False
            btnPad_chain1.Enabled = False
            btnPad_chain2.Enabled = False
            btnPad_chain3.Enabled = False
            btnPad_chain4.Enabled = False
            btnPad_chain5.Enabled = False
            btnPad_chain6.Enabled = False
            btnPad_chain7.Enabled = False
            btnPad_chain8.Enabled = False
            keySoundLayout = False
#End Region

            setxml.Load(file_ex)
            Dim setNode As XmlNode
            setNode = setxml.SelectSingleNode("/UniConverter-XML/UG-Settings")

            If setNode.ChildNodes(0).InnerText = "True" Then
                BGW_CheckUpdate.RunWorkerAsync()
            End If

            If setNode.ChildNodes(2).InnerText = "True" Then
                SetUpLight_ = True
            End If

            'Text of Info TextBox
            infoTB1.Text = "My Amazing UniPack!" 'Title
            infoTB2.Text = "UniConverter, " & My.Computer.Name 'Producer Name
            'Chain!

            'Edit>Ableton Option.
            AnyAbletonToolStripMenuItem.Checked = False
            AbletonLive9LiteToolStripMenuItem.Checked = False
            AbletonLive9TrialToolStripMenuItem.Checked = False
            AbletonLive9SuiteToolStripMenuItem.Checked = False
            AbletonLive10ToolStripMenuItem.Checked = False
            'RESET!!!

            setNode = setxml.SelectSingleNode("/UniConverter-XML/LiveSet")
            Select Case setNode.ChildNodes(0).InnerText
                Case "AnyAbleton"
                    AnyAbletonToolStripMenuItem.Checked = True
                Case "Ableton9_Lite"
                    AbletonLive9LiteToolStripMenuItem.Checked = True
                Case "Ableton9_Trial"
                    AbletonLive9TrialToolStripMenuItem.Checked = True
                Case "Ableton9_Suite"
                    AbletonLive9SuiteToolStripMenuItem.Checked = True
                Case "Ableton10"
                    AbletonLive10ToolStripMenuItem.Checked = True
            End Select

            'Edit>Unipack Option.
            ConvertToZipUniToolStripMenuItem.Checked = False
            'RESET!!!

            Select Case setNode.ChildNodes(1).InnerText
                Case "zip/uni"
                    ConvertToZipUniToolStripMenuItem.Checked = True
            End Select

            '건드리면 IsSaved가 False로 진행되기 때문에 다시 기본값 설정을 해준다!
            IsSaved = True

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
            End If
        End Try
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
            ofd_FileNames = ofd.FileNames
            BGW_sounds.RunWorkerAsync()
        End If
    End Sub
#Region "Smart Invoke Function"
    Public Sub UI(ByVal uiUpdate As Action)
        If InvokeRequired Then
            Try
                Invoke(DirectCast(Sub() uiUpdate(), MethodInvoker))
            Catch ex As Exception
                ' The try catch block here is here because the end user (you) may decide to close the form without closing the server first, 
                ' and that may cause an ObjectDesposed access exception. I think we'd all rather not see that.
            End Try
        Else
            Try
                uiUpdate()
            Catch ex As Exception
                ' The try catch block here is here because the end user (you) may decide to close the form without closing the server first, 
                ' and that may cause an ObjectDesposed access exception. I think we'd all rather not see that.
            End Try
        End If
    End Sub
#End Region

    Private Sub TutorialsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TutorialsToolStripMenuItem.Click
        Tutorials.Show()
    End Sub

    Private Sub SaveProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveProjectToolStripMenuItem.Click
        Save2Project(True)
    End Sub

    ''' <summary>
    ''' URL 내용을 문자열로 나타냅니다. (Get Text from site.)
    ''' </summary>
    ''' <param name="URL">사이트 url.</param>
    ''' <returns></returns>
    Public Function ReadURLString(URL As String) As String
        Try

            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(URL))
            Dim returnstr As String = reader.ReadToEnd()
            Return returnstr

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return String.Empty
        End Try
    End Function

    Private Sub BGW_keyLED_DoWork(sender As Object, e As DoWorkEventArgs) Handles BGW_keyLED.DoWork
        Try
            Dim FileNames = ofd_FileNames

            Loading.Show()
            Loading.Text = Me.Text & ": Loading LED Files..."
            FileNames = ofd_FileNames
            Loading.DPr.Maximum = FileNames.Length
            Loading.DLb.Left = 40
            Loading.DLb.Text = "Loading LED Files..."
            Loading.DLb.Refresh()

            If Directory.Exists("Workspace\ableproj\CoLED") Then
                My.Computer.FileSystem.DeleteDirectory("Workspace\ableproj\CoLED", FileIO.DeleteDirectoryOption.DeleteAllContents)
                Directory.CreateDirectory("Workspace\ableproj\CoLED")
            Else
                Directory.CreateDirectory("Workspace\ableproj\CoLED")
            End If

            For i = 0 To FileNames.Length - 1
                File.Copy(FileNames(i), "Workspace\ableproj\CoLED\" & FileNames(i).Split("\").Last, True)
                Loading.DPr.Style = ProgressBarStyle.Continuous
                Loading.DPr.Value += 1
                Loading.DLb.Left = 40
                Loading.DLb.Text = String.Format(Loading.loading_LED_open_msg, Loading.DPr.Value, FileNames.Length)
                Loading.DLb.Refresh()
            Next

            Loading.DPr.Value = Loading.DPr.Maximum
            Loading.DPr.Style = ProgressBarStyle.Marquee
            Loading.DPr.Refresh()
            Loading.DLb.Left = 40
            Loading.DLb.Text = "Loaded LED Files. Please Wait..."
            Loading.DLb.Refresh()

            abl_openedled = True
            Loading.Dispose()
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            e.Cancel = True
        End Try
    End Sub

    Private Sub BGW_keyLED_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BGW_keyLED.RunWorkerCompleted
        Try
            If e.Error IsNot Nothing Then
                MessageBox.Show("Error - " & e.Error.Message & vbNewLine & "Error Message: " & e.Error.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            ElseIf e.Cancelled Then
                Exit Sub
            Else

                Dim s As String = My.Computer.FileSystem.GetParentPath(ofd_FileNames(0))
                Dim wowkac As String = String.Empty
                For Each d As String In My.Computer.FileSystem.GetFiles(s, FileIO.SearchOption.SearchTopLevelOnly)
                    If d.Contains("Save") OrElse d.Contains("save") AndAlso Path.HasExtension(d) = False Then
                        wowkac = d
                        Exit For
                    End If
                Next

                If Not wowkac = String.Empty Then
                    File.Copy(wowkac, Application.StartupPath & "\Workspace\ableproj\LEDSave.uni", True)
                    abl_openedled2 = True
                    stopitnow = True
                    BGW_keyLED2.RunWorkerAsync()
                Else
                    OpenkeyLED2()
                End If

            End If
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub BGW_keyLED2_DoWork(sender As Object, e As DoWorkEventArgs) Handles BGW_keyLED2.DoWork
        Try
            Dim FileName As String = ofd_FileName

            If String.IsNullOrEmpty(FileName) OrElse stopitnow Then
                e.Cancel = True
            End If

            If e.Cancel = False Then
                Loading.Show()
                Loading.Text = Me.Text & ": Loading LED Save File..."
                FileName = ofd_FileName
                Loading.DPr.Maximum = 1
                Loading.DLb.Left = 40
                Loading.DLb.Text = "Loading LED Save File..."
                Loading.DLb.Refresh()

                File.Copy(FileName, Application.StartupPath & "\Workspace\ableproj\LEDSave.uni", True)
                Loading.DPr.Style = ProgressBarStyle.Continuous
                Loading.DPr.Value = 1
                Loading.DLb.Left = 40
                Loading.DLb.Text = String.Format(Loading.loading_LED_open_msg, Loading.DPr.Value, 1)
                Loading.DLb.Refresh()

                Loading.DPr.Value = Loading.DPr.Maximum
                Loading.DPr.Style = ProgressBarStyle.Marquee
                Loading.DPr.Refresh()
                Loading.DLb.Left = 40
                Loading.DLb.Text = "Loaded keyLED Save File. Please Wait..."
                Loading.DLb.Refresh()

                If OpenProjectOnce = False Then MessageBox.Show("LED Files Loaded! You can edit LEDs in 'keyLED (MIDI Extension)' Tab.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                abl_openedled2 = True
                Loading.Dispose()
            End If

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            e.Cancel = True
        End Try
    End Sub

    Private Sub BGW_keyLED2_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BGW_keyLED2.RunWorkerCompleted
        Try
            If e.Error IsNot Nothing Then
                MessageBox.Show("Error - " & e.Error.Message & vbNewLine & "Error Message: " & e.Error.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            ElseIf e.Cancelled Then
                If OpenProjectOnce Then
                    OpenProjectOnce = False
                    If abl_openedproj AndAlso abl_openedsnd AndAlso abl_openedled Then
                        MessageBox.Show("Ableton Project, Sounds, LEDs Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf abl_openedproj AndAlso abl_openedsnd AndAlso abl_openedled Then
                        MessageBox.Show("Ableton Project, Sounds Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf abl_openedproj Then
                        MessageBox.Show("Ableton Project Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf abl_openedsnd AndAlso abl_openedled Then
                        MessageBox.Show("Sounds, LEDs Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf abl_openedsnd Then
                        MessageBox.Show("Sounds Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf abl_openedled Then
                        MessageBox.Show("LEDs Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    '이제 변환 작업 시작.
                    'BGW_ablprojCvt.RunWorkerAsync()
                    'BGW_soundsCvt.RunWorkerAsync()
                    'BGW_keyLEDCvt.RunWorkerAsync()
                Else
                    MessageBox.Show("LED Files Loaded! You can edit LEDs in 'keyLED (MIDI Extension)' Tab.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    BGW_keyLED_.RunWorkerAsync()
                End If
                Exit Sub
            Else
                If OpenProjectOnce Then
                    OpenProjectOnce = False
                    If abl_openedproj AndAlso abl_openedsnd AndAlso abl_openedled Then
                        MessageBox.Show("Ableton Project, Sounds, LEDs Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf abl_openedproj AndAlso abl_openedsnd AndAlso abl_openedled Then
                        MessageBox.Show("Ableton Project, Sounds Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf abl_openedproj Then
                        MessageBox.Show("Ableton Project Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf abl_openedsnd AndAlso abl_openedled Then
                        MessageBox.Show("Sounds, LEDs Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf abl_openedsnd Then
                        MessageBox.Show("Sounds Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf abl_openedled Then
                        MessageBox.Show("LEDs Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    '이제 변환 작업 시작.
                    'BGW_ablprojCvt.RunWorkerAsync()
                    'BGW_soundsCvt.RunWorkerAsync()
                    'BGW_keyLEDCvt.RunWorkerAsync()
                Else
                    BGW_keyLED_.RunWorkerAsync()
                End If
            End If
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub OpenkeyLED2()
        Try
            ofd.Multiselect = False
            ofd.Title = "Open the keyLED Save File"
            ofd.Filter = "keyLED Save File|*.*"

            If ofd.ShowDialog() = DialogResult.OK Then
                ofd_FileName = ofd.FileName
                BGW_keyLED2.RunWorkerAsync()
            Else
                ofd_FileName = String.Empty
                BGW_keyLED2.RunWorkerAsync()
            End If
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
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

    Private Sub Ableton_OpenProject(sender As Object, e As DoWorkEventArgs) Handles BGW_ablproj.DoWork
        '---Beta Code: Converting Ableton Project Info To Unipack Info---
        '이 Beta Convert Code는 오류가 발생할 수 있습니다.
        '주의사항을 다 보셨다면, 당신은 Editor 권한을 가질 수 있습니다.

        'Convert Ableton Project to Unipack Informations. (BETA!!!)

        Dim FileName As String = ofd_FileName
        If Not Dir("Workspace\ableproj", vbDirectory) <> "" Then
            My.Computer.FileSystem.CreateDirectory("Workspace\ableproj")
        End If

        Loading.Show()
        Loading.DPr.Style = ProgressBarStyle.Marquee
        Loading.DPr.Refresh()
        Loading.DLb.Left = 60
        Loading.Text = Me.Text & ": Loading The Ableton Project File..."
        Loading.DLb.Text = Loading.loading_Project_Load_msg
        Loading.DLb.Refresh()

        abl_FileName = FileName
        File.Copy(FileName, "Workspace\ableproj\abl_proj.gz", True)

        Loading.DLb.Text = Loading.loading_Project_Extract_msg
        Loading.DLb.Refresh()
        ExtractGZip("Workspace\ableproj\abl_proj.gz", "Workspace\ableproj")

        Loading.DLb.Text = Loading.loading_Project_DeleteTmp_msg
        Loading.DLb.Refresh()
        File.Delete("Workspace\ableproj\abl_proj.gz")
        File.Delete("Workspace\ableproj\abl_proj.xml")

        Loading.DLb.Text = Loading.loading_Project_ChangeExt_msg
        Loading.DLb.Refresh()
        File.Move("Workspace\ableproj\abl_proj", "Workspace\ableproj\abl_proj.xml")

        Loading.DLb.Text = Loading.loading_Project_DeleteTmp_msg
        Loading.DLb.Refresh()
        File.Delete("Workspace\ableproj\abl_proj")



        'Reading Informations of Ableton Project.

        'Ableton Project's Name.
        Loading.DLb.Text = Loading.loading_Project_FileName_msg
        Loading.DLb.Refresh()

        Dim FinalName As String = Path.GetFileNameWithoutExtension(FileName)

        'Ableton Project's Chain.
        Loading.DLb.Left = 130
        Loading.DLb.Text = Loading.loading_Project_Chain_msg
        Loading.DLb.Refresh()

#Region "Loading Chain Numbers"
        Dim ablprj As String = Application.StartupPath & "\Workspace\ableproj\abl_proj.xml"
        Dim doc As New XmlDocument
        Dim setNode As XmlNodeList
        doc.Load(ablprj)
        setNode = doc.GetElementsByTagName("MidiEffectBranch")

        Dim li As Integer = setNode.Count
        Dim chan_ As Integer() = New Integer(li) {}

        Dim iy As Integer = 0
        For Each x As XmlNode In setNode
            'Chain + 1 해주는 이유는 항상 Chain의 기본값이 0이기 때문임. 유니팩에서는 Chain 1이여도 에이블톤에서는 Chain 0임.
            chan_(iy) = x.Item("BranchSelectorRange").Item("Max").GetAttribute("Value") + 1
            iy += 1
        Next

        Array.Sort(chan_)
        Array.Reverse(chan_)

        Dim FinalChain As Integer = 0
        For i As Integer = 0 To chan_.Count - 1
            If chan_(i) < 9 AndAlso chan_(i) > 0 Then
                FinalChain = chan_(i)
                Exit For
            End If
        Next
#End Region

        '정리.
        abl_Name = FinalName
        abl_Chain = FinalChain

        Loading.DLb.Left = 40
        Loading.DLb.Text = "Loading The Ableton Project File..."
        Loading.DLb.Refresh()

        Loading.Dispose()

        'XML File Load.
        Invoke(Sub()
                   infoTB1.Text = abl_Name
                   infoTB3.Text = abl_Chain
               End Sub)

        abl_openedproj = True
        UniPack_SaveInfo(False)

        If OpenProjectOnce = False Then
            MessageBox.Show("Ableton Project File Loaded!" & vbNewLine & "You can edit info in Information Tab.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub BGW_ablproj_Completed(sender As Object, e As RunWorkerCompletedEventArgs) Handles BGW_ablproj.RunWorkerCompleted
        Try
            If e.Error IsNot Nothing Then
                MessageBox.Show("Error - " & e.Error.Message & vbNewLine & "Error Message: " & e.Error.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            ElseIf e.Cancelled Then
                Exit Sub
            Else
                If OpenProjectOnce Then
                    OpenSoundsToolStripMenuItem_Click(Nothing, Nothing)
                End If

                If String.IsNullOrEmpty(w8t4abl) = False Then
                    Select Case w8t4abl

                        Case "keyLED"
                            BGW_keyLED_.RunWorkerAsync()

                    End Select
                End If

            End If
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub OpenAbletonProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenAbletonProjectToolStripMenuItem.Click
        Dim alsOpen1 As New OpenFileDialog
        alsOpen1.Filter = "Ableton Project File|*.als"
        alsOpen1.Title = "Select a Ableton Project File"
        alsOpen1.AddExtension = False
        alsOpen1.Multiselect = False

        If alsOpen1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            ofd_FileName = alsOpen1.FileName
            BGW_ablproj.RunWorkerAsync()
        End If
    End Sub

    Private Sub ConvertALSToUnipackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertALSToUnipackToolStripMenuItem.Click

        Dim sfd As New SaveFileDialog()
        If ConvertToZipUniToolStripMenuItem.Checked = True Then
            sfd.Filter = "Zip File|*.zip|UniPack File|*.uni"
        Else
            'Another Convert File Code,
        End If
        sfd.Title = "Select Convert ALS to UniPack"
        sfd.AddExtension = False

        Try
            If sfd.ShowDialog() = DialogResult.OK Then
                'Convert Ableton Project to UniPack & Save UniPack (BETA!!!)
            End If

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Failed to save UniPack." & vbNewLine & "Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub Info_SaveButton_Click(sender As Object, e As EventArgs) Handles Info_SaveButton.Click
        Try
            UniPack_SaveInfo(True)
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' UniPack의 info 파일을 저장합니다.
    ''' </summary>
    ''' <param name="Message">메시지 박스를 표시함.</param>
    Public Sub UniPack_SaveInfo(Message As Boolean)
        Try
            If abl_openedproj = True Then

                If Directory.Exists(Application.StartupPath & "\Workspace\unipack") = False Then
                    My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\Workspace\unipack")
                End If

                File.WriteAllText(Application.StartupPath & "\Workspace\unipack\info", String.Format("title={0}{1}buttonX=8{1}buttonY=8{1}producerName={2}{1}chain={3}{1}squareButton=true", infoTB1.Text, vbNewLine, infoTB2.Text, infoTB3.Text))
                infoIsSaved = True
                If Message Then
                    MessageBox.Show("Saved info!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("You didn't open Ableton Project!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub BGW_keySound_DoWork(sender As Object, e As DoWorkEventArgs) Handles BGW_keySound.DoWork
        Try
            If e.Cancel = False AndAlso IsWorking = False AndAlso abl_openedproj AndAlso abl_openedsnd Then
                IsWorking = True

                With Loading
                    .Show()
                    .Text = "Converting Ableton Sound Mapping to keySound..."
                    .Refresh()
                    .DLb.Text = "Loading Mapping Infos..."
                    .DPr.Style = ProgressBarStyle.Marquee
                    .DPr.MarqueeAnimationSpeed = 1
                End With

                'InstrumentGroupDevice
                'ChainSelector
                Dim ablprj As String = Application.StartupPath & "\Workspace\ableproj\abl_proj.xml"
                Dim doc As New XmlDocument
                Dim setNode As XmlNodeList
                doc.Load(ablprj)
                setNode = doc.GetElementsByTagName("InstrumentBranch")

                'Get Sound Name from Drum Rack.
                Dim PrChain As Integer = 0
                Dim IsRandom As Boolean = False
                Dim rnd As Integer = 0
                Dim currentRnd As Integer = 1
                For Each x As XmlNode In setNode
                    Try
                        Dim Try4sndName As String = x.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("OriginalSimpler").Item("Player").Item("MultiSampleMap").Item("SampleParts").Item("MultiSamplePart").Item("SampleRef").Item("FileRef").Item("Name").GetAttribute("Value")
                    Catch exN As NullReferenceException

                        PrChain = Integer.Parse(x.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1
                        If Cntstr(x.OuterXml, "</InstrumentBranch>") > 1 Then
                            rnd = Cntstr(x.OuterXml, "</InstrumentBranch>")
                            IsRandom = True
                        Else
                            IsRandom = False
                        End If

                        Continue For
                    End Try

                    Dim sndName As String = x.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("OriginalSimpler").Item("Player").Item("MultiSampleMap").Item("SampleParts").Item("MultiSamplePart").Item("SampleRef").Item("FileRef").Item("Name").GetAttribute("Value")
                    Dim Chain As Integer = Integer.Parse(x.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1

                    If Not PrChain = 0 AndAlso IsRandom Then
                        Chain = PrChain
                        currentRnd += 1

                        If rnd = currentRnd Then
                            PrChain = 0
                            IsRandom = False
                            rnd = 0
                            currentRnd = 0
                        End If
                    Else
                        PrChain = 0
                    End If

                    If Not Chain = Integer.Parse(x.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1 Then

                    End If

                    Debug.WriteLine(sndName & ": " & Chain)
                Next

                Invoke(Sub()

                       End Sub)
                ShowkeySoundLayout()

                IsWorking = False
                Loading.Dispose()
                MessageBox.Show("keySound Converted!" & vbNewLine & "You can show the keySound on 'keySound' Tab!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As Exception
            If IsGreatExMode Then
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        End Try
    End Sub

    Private Sub ShowkeySoundLayout()
        If keySoundLayout = False Then
            clskeySoundLayout()
        End If

        For x As Integer = 1 To 8
            For y As Integer = 1 To 8
                ks_ctrl(x & y).Text = String.Empty
                ks_ctrl(x & y).BackColor = Color.Gray
                ks_ctrl(x & y).ForeColor = Color.Black
            Next
        Next

        keySound_CChain = 1
        BGW_keySoundLayout.RunWorkerAsync()
    End Sub

    Private Sub clskeySoundLayout()
        PadLayoutPanel.Enabled = True
        btnPad_chain1.Enabled = True
        btnPad_chain2.Enabled = True
        btnPad_chain3.Enabled = True
        btnPad_chain4.Enabled = True
        btnPad_chain5.Enabled = True
        btnPad_chain6.Enabled = True
        btnPad_chain7.Enabled = True
        btnPad_chain8.Enabled = True
        keySoundLayout = True
    End Sub

    Private Sub BGW_keySoundLayout_DoWork(sender As Object, e As DoWorkEventArgs) Handles BGW_keySoundLayout.DoWork
        Try

            Dim btnText As String = ""
            Dim ksnd As String = Application.StartupPath & "\Workspace\unipack\keySound"
            Dim ksTmpTXT As String = File.ReadAllText(ksnd)
            If String.IsNullOrWhiteSpace(ksTmpTXT) = False Then
                For Each strLine As String In SplitbyLine(ksTmpTXT) 'String을 각 라인마다 자름.
                    If String.IsNullOrWhiteSpace(strLine) = False Then

                        Select Case Integer.Parse(Mid(strLine, 1, 1))
                            Case 1 To 8
                                'Continue.
                            Case Else
                                MessageBox.Show("Error! - Chain " & keySound_CChain.ToString & " doesn't exists in keySound. (Ex: Check Failed Chain " & Mid(strLine, 1, 1) & ", Full: " & strLine & ")", Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                        End Select

                        If CInt(Mid(strLine, 1, 1)) = keySound_CChain Then

                            ksUniPack_SelectedChain = keySound_CChain
                            ksUniPack_X = CInt(Mid(strLine, 3, 1))
                            ksUniPack_Y = CInt(Mid(strLine, 5, 1))

                            'ex: 1 1 1 001.wav 1 (같은 사운드 다중 매핑 적용)
                            If Strings.Right(strLine, 4) = ".mp3" Then '.mp3의 경우
                                keySound_Mapping = 1
                            ElseIf Strings.Right(strLine, 4) = ".wav" Then '.wav의 경우
                                keySound_Mapping = 1
                            Else
                                keySound_SameM = Strings.Right(strLine, 1) '반복문이 1 이상의 경우
                            End If

                            'ex: 1 1 1 001.wav, 1 1 1 002.wav (다른 사운드 다중 매핑 적용, 추천)
                            keySound_DifM = Cntstr(ksTmpTXT, ksUniPack_SelectedChain & " " & ksUniPack_X & " " & ksUniPack_Y & " ")

                            If keySound_Mapping > 0 Then '기본적인 사운드 매핑.
                                btnText = keySound_Mapping
                            End If

                            If keySound_SameM > 0 Then
                                btnText = keySound_SameM
                            End If

                            If keySound_DifM > 1 Then '사운드 다중 매핑.
                                If keySound_SameM > 0 Then
                                    btnText = keySound_DifM + keySound_SameM
                                Else
                                    btnText = keySound_DifM
                                End If
                            End If

                            Invoke(Sub()
                                       ks_ctrl(ksUniPack_X & ksUniPack_Y).BackColor = Color.Green
                                       ks_ctrl(ksUniPack_X & ksUniPack_Y).Text = btnText
                                   End Sub)

                        Else
                            Continue For
                        End If
                    Else
                        Continue For
                    End If
                Next
            Else
                MessageBox.Show("Error: keySound doesn't exists.", Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub keySound_ChainChanged(sender As Object, e As EventArgs) Handles btnPad_chain1.Click, btnPad_chain2.Click, btnPad_chain3.Click, btnPad_chain4.Click, btnPad_chain5.Click, btnPad_chain6.Click, btnPad_chain7.Click, btnPad_chain8.Click
        '선택한 체인 선언.
        Dim ClickedBtn As Button = CType(sender, Button)
        keySound_CChain = CInt(ClickedBtn.Name.Substring(12, 1))

        '선택한 체인으로 레이아웃 새로고침.
        If keySoundLayout = True Then

            For x As Integer = 1 To 8
                For y As Integer = 1 To 8
                    ks_ctrl(x & y).Text = String.Empty
                    ks_ctrl(x & y).BackColor = Color.Gray
                    ks_ctrl(x & y).ForeColor = Color.Black
                Next
            Next

            BGW_keySoundLayout.RunWorkerAsync()

        End If
    End Sub

    Private Sub UniPadButtons_Click(sender As Object, e As EventArgs) Handles uni1_1.MouseDown, uni1_2.MouseDown, uni1_3.MouseDown, uni1_4.MouseDown, uni1_5.MouseDown, uni1_6.MouseDown, uni1_7.MouseDown, uni1_8.MouseDown, uni2_1.MouseDown, uni2_2.MouseDown, uni2_3.MouseDown, uni2_4.MouseDown, uni2_5.MouseDown, uni2_6.MouseDown, uni2_7.MouseDown, uni2_8.MouseDown, uni3_1.MouseDown, uni3_2.MouseDown, uni3_3.MouseDown, uni3_4.MouseDown, uni3_5.MouseDown, uni3_6.MouseDown, uni3_7.MouseDown, uni3_8.MouseDown, uni4_1.MouseDown, uni4_2.MouseDown, uni4_3.MouseDown, uni4_4.MouseDown, uni4_5.MouseDown, uni4_6.MouseDown, uni4_7.MouseDown, uni4_8.MouseDown, uni5_1.MouseDown, uni5_2.MouseDown, uni5_3.MouseDown, uni5_4.MouseDown, uni5_5.MouseDown, uni5_6.MouseDown, uni5_7.MouseDown, uni5_8.MouseDown, uni6_1.MouseDown, uni6_2.MouseDown, uni6_3.MouseDown, uni6_4.MouseDown, uni6_5.MouseDown, uni6_6.MouseDown, uni6_7.MouseDown, uni6_8.MouseDown, uni7_1.MouseDown, uni7_2.MouseDown, uni7_3.MouseDown, uni7_4.MouseDown, uni7_5.MouseDown, uni7_6.MouseDown, uni7_7.MouseDown, uni7_8.MouseDown, uni8_1.MouseDown, uni8_2.MouseDown, uni8_3.MouseDown, uni8_4.MouseDown, uni8_5.MouseDown, uni8_6.MouseDown, uni8_7.MouseDown, uni8_8.MouseDown
        PlaykeySound(sender)
    End Sub

    Private Sub UniPadButtons_Loop0(sender As Object, e As EventArgs) Handles uni1_1.MouseUp, uni1_2.MouseUp, uni1_3.MouseUp, uni1_4.MouseUp, uni1_5.MouseUp, uni1_6.MouseUp, uni1_7.MouseUp, uni1_8.MouseUp, uni2_1.MouseUp, uni2_2.MouseUp, uni2_3.MouseUp, uni2_4.MouseUp, uni2_5.MouseUp, uni2_6.MouseUp, uni2_7.MouseUp, uni2_8.MouseUp, uni3_1.MouseUp, uni3_2.MouseUp, uni3_3.MouseUp, uni3_4.MouseUp, uni3_5.MouseUp, uni3_6.MouseUp, uni3_7.MouseUp, uni3_8.MouseUp, uni4_1.MouseUp, uni4_2.MouseUp, uni4_3.MouseUp, uni4_4.MouseUp, uni4_5.MouseUp, uni4_6.MouseUp, uni4_7.MouseUp, uni4_8.MouseUp, uni5_1.MouseUp, uni5_2.MouseUp, uni5_3.MouseUp, uni5_4.MouseUp, uni5_5.MouseUp, uni5_6.MouseUp, uni5_7.MouseUp, uni5_8.MouseUp, uni6_1.MouseUp, uni6_2.MouseUp, uni6_3.MouseUp, uni6_4.MouseUp, uni6_5.MouseUp, uni6_6.MouseUp, uni6_7.MouseUp, uni6_8.MouseUp, uni7_1.MouseUp, uni7_2.MouseUp, uni7_3.MouseUp, uni7_4.MouseUp, uni7_5.MouseUp, uni7_6.MouseUp, uni7_7.MouseUp, uni7_8.MouseUp, uni8_1.MouseUp, uni8_2.MouseUp, uni8_3.MouseUp, uni8_4.MouseUp, uni8_5.MouseUp, uni8_6.MouseUp, uni8_7.MouseUp, uni8_8.MouseUp
        If keySoundLoop = True Then
            ks_PlayLoop1.controls.stop()
            ks_PlayLoop2.controls.stop()
        End If
    End Sub

    Private Sub ks_Disposing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ks_PlayLoop1.controls.stop()
        ks_PlayLoop2.controls.stop()
    End Sub

    Private Sub PlaykeySound(sender As Object)
        Try

            'keySound 테스트 할 때는 이 변수의 경로를 에이블톤 폴더로 바꿔주세요!
            Dim sndFile As String = Application.StartupPath & "\Workspace\unipack\sounds"

            Dim ClickedBtn As Button = CType(sender, Button)
            Dim x As Integer = ClickedBtn.Name.Substring(3, 1)
            Dim y As Integer = ClickedBtn.Name.Substring(5, 1)
            Dim WavFile As String

            Dim ksnd As String = Application.StartupPath & "\Workspace\unipack\keySound"
            Dim ksTmpTXT As String = File.ReadAllText(ksnd)

            For Each strLine As String In SplitbyLine(ksTmpTXT)
                If strLine.Contains(keySound_CChain & " " & x & " " & y & " ") Then
                    For Each WavFileName As String In strLine.Split(" ")

                        If WavFileName.Contains(".wav") Then
                            WavFile = WavFileName
                            If File.Exists(sndFile & "\" & WavFile) Then
                                If Strings.Right(strLine, 4) = ".wav" Then
                                    Dim a As New WindowsMediaPlayer
                                    a.URL = sndFile & "\" & WavFile
                                    a.controls.play()

                                    If keySoundLoop = True Then keySoundLoop = False
                                ElseIf Strings.Right(strLine, 1) = "1" Then
                                    Dim a As New WindowsMediaPlayer
                                    a.URL = sndFile & "\" & WavFile
                                    a.controls.play()

                                    If keySoundLoop = True Then keySoundLoop = False
                                ElseIf Strings.Right(strLine, 1) = "0" Then
                                    ks_PlayLoop1.URL = sndFile & "\" & WavFile
                                    ks_PlayLoop1.controls.play()
                                    ks_PlayLoop1.settings.playCount = Integer.MaxValue
                                    keySoundLoop = True

                                Else
                                    ks_PlayLoop2.URL = sndFile & "\" & WavFile
                                    ks_PlayLoop2.controls.play()
                                    ks_PlayLoop2.settings.playCount = CInt(Strings.Right(strLine, 1))
                                    If keySoundLoop = True Then keySoundLoop = False
                                End If
                            End If
                        End If
                    Next
                End If
            Next

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Line by Line으로 문자열을 자릅니다.
    ''' </summary>
    ''' <param name="inputstr">문자열.</param>
    ''' <returns></returns>
    Public Shared Function SplitbyLine(inputstr As String) As String()
        Return inputstr.Replace(vbCr, "").Split(vbLf)
    End Function

    Private Sub AnyAbletonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnyAbletonToolStripMenuItem.Click
        Try
            Dim file_ex = Application.StartupPath + "\settings.xml"
            Dim setNode As New XmlDocument

            AnyAbletonToolStripMenuItem.Checked = False
            AbletonLive9LiteToolStripMenuItem.Checked = False
            AbletonLive9TrialToolStripMenuItem.Checked = False
            AbletonLive9SuiteToolStripMenuItem.Checked = False
            AbletonLive10ToolStripMenuItem.Checked = False

            abl_ver = "AnyAbleton"
            setNode.Load(file_ex)
            Dim setaNode As XmlNode = setNode.SelectSingleNode("/UniConverter-XML/LiveSet")

            If setaNode IsNot Nothing Then
                setaNode.ChildNodes(0).InnerText = abl_ver
            Else
                Throw New Exception("Settings XML File's Argument is invaild.")
            End If
            setNode.Save(file_ex)
            AnyAbletonToolStripMenuItem.Checked = True
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub AbletonLive9LiteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbletonLive9LiteToolStripMenuItem.Click
        Try
            Dim file_ex = Application.StartupPath + "\settings.xml"
            Dim setNode As New XmlDocument

            AnyAbletonToolStripMenuItem.Checked = False
            AbletonLive9LiteToolStripMenuItem.Checked = False
            AbletonLive9TrialToolStripMenuItem.Checked = False
            AbletonLive9SuiteToolStripMenuItem.Checked = False
            AbletonLive10ToolStripMenuItem.Checked = False

            abl_ver = "Ableton9_Lite"
            setNode.Load(file_ex)
            Dim setaNode As XmlNode = setNode.SelectSingleNode("/UniConverter-XML/LiveSet")

            If setaNode IsNot Nothing Then
                setaNode.ChildNodes(0).InnerText = abl_ver
            Else
                Throw New Exception("Settings XML File's Argument is invaild.")
            End If
            setNode.Save(file_ex)
            AbletonLive9LiteToolStripMenuItem.Checked = True
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub AbletonLive9TrialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbletonLive9TrialToolStripMenuItem.Click
        Try
            Dim file_ex = Application.StartupPath + "\settings.xml"
            Dim setNode As New XmlDocument

            AnyAbletonToolStripMenuItem.Checked = False
            AbletonLive9LiteToolStripMenuItem.Checked = False
            AbletonLive9TrialToolStripMenuItem.Checked = False
            AbletonLive9SuiteToolStripMenuItem.Checked = False
            AbletonLive10ToolStripMenuItem.Checked = False

            abl_ver = "Ableton9_Trial"
            setNode.Load(file_ex)
            Dim setaNode As XmlNode = setNode.SelectSingleNode("/UniConverter-XML/LiveSet")

            If setaNode IsNot Nothing Then
                setaNode.ChildNodes(0).InnerText = abl_ver
            Else
                Throw New Exception("Settings XML File's Argument is invaild.")
            End If
            setNode.Save(file_ex)
            AbletonLive9TrialToolStripMenuItem.Checked = True
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub AbletonLive9SuiteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbletonLive9SuiteToolStripMenuItem.Click
        Try
            Dim file_ex = Application.StartupPath + "\settings.xml"
            Dim setNode As New XmlDocument

            AnyAbletonToolStripMenuItem.Checked = False
            AbletonLive9LiteToolStripMenuItem.Checked = False
            AbletonLive9TrialToolStripMenuItem.Checked = False
            AbletonLive9SuiteToolStripMenuItem.Checked = False
            AbletonLive10ToolStripMenuItem.Checked = False

            abl_ver = "Ableton9_Suite"
            setNode.Load(file_ex)
            Dim setaNode As XmlNode = setNode.SelectSingleNode("/UniConverter-XML/LiveSet")

            If setaNode IsNot Nothing Then
                setaNode.ChildNodes(0).InnerText = abl_ver
            Else
                Throw New Exception("Settings XML File's Argument is invaild.")
            End If
            setNode.Save(file_ex)
            AbletonLive9SuiteToolStripMenuItem.Checked = True
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub AbletonLive10ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbletonLive10ToolStripMenuItem.Click
        Try
            Dim file_ex = Application.StartupPath + "\settings.xml"
            Dim setNode As New XmlDocument

            AnyAbletonToolStripMenuItem.Checked = False
            AbletonLive9LiteToolStripMenuItem.Checked = False
            AbletonLive9TrialToolStripMenuItem.Checked = False
            AbletonLive9SuiteToolStripMenuItem.Checked = False
            AbletonLive10ToolStripMenuItem.Checked = False

            abl_ver = "Ableton10"
            setNode.Load(file_ex)
            Dim setaNode As XmlNode = setNode.SelectSingleNode("/UniConverter-XML/LiveSet")

            If setaNode IsNot Nothing Then
                setaNode.ChildNodes(0).InnerText = abl_ver
            Else
                Throw New Exception("Settings XML File's Argument is invaild.")
            End If
            setNode.Save(file_ex)
            AbletonLive10ToolStripMenuItem.Checked = True
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub ConvertToZipUniToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertToZipUniToolStripMenuItem.Click
        Try
            Dim file_ex = Application.StartupPath + "\settings.xml"
            Dim setNode As New XmlDocument

            ConvertToZipUniToolStripMenuItem.Checked = False

            uni_confile = "zip/uni"
            setNode.Load(file_ex)
            Dim setaNode As XmlNode = setNode.SelectSingleNode("/UniConverter-XML/LiveSet")

            If setaNode IsNot Nothing Then
                setaNode.ChildNodes(1).InnerText = uni_confile
            Else
                Throw New Exception("Settings XML File's Argument is invaild.")
            End If
            setNode.Save(file_ex)
            ConvertToZipUniToolStripMenuItem.Checked = True
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub CutSndButton_Click(sender As Object, e As EventArgs) Handles CutSndButton.Click
        Try
            ofd.Filter = "MP3 File|*.mp3|WAV File|*.wav"
            If ofd.ShowDialog = DialogResult.OK Then
                ofd_FileName = ofd.FileName
                Sound_Cutting.Show()
            End If
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Failed to edit keySound." & vbNewLine & "Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Public Sub OpenSounds(sender As Object, e As DoWorkEventArgs) Handles BGW_sounds.DoWork
        Dim FileNames() As String

        Loading.Show()
        Loading.Text = Me.Text & ": Loading Sound Files..."
        FileNames = ofd_FileNames
        Loading.DPr.Maximum = FileNames.Length
        Loading.DLb.Left = 40
        Loading.DLb.Text = "Loading Sound Files..."
        Loading.DLb.Refresh()

        If Path.GetExtension(FileNames(FileNames.Length - 1)) = ".wav" Then

            If My.Computer.FileSystem.DirectoryExists("Workspace\ableproj\sounds") = True Then
                My.Computer.FileSystem.DeleteDirectory("Workspace\ableproj\sounds", FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If

            My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\sounds")


            For i = 0 To FileNames.Length - 1
                File.Copy(FileNames(i), "Workspace\ableproj\sounds\" & FileNames(i).Split("\").Last, True)
                Loading.DPr.Style = ProgressBarStyle.Continuous
                Loading.DPr.Value += 1
                Loading.DLb.Left = 40
                Loading.DLb.Text = String.Format(Loading.loading_Sound_Open_msg, Loading.DPr.Value, FileNames.Length)
                Loading.DLb.Refresh()
            Next

        ElseIf Path.GetExtension(FileNames(FileNames.Length - 1)) = ".mp3" Then
            If My.Computer.FileSystem.DirectoryExists("Workspace\ableproj\sounds") = True Then
                My.Computer.FileSystem.DeleteDirectory("Workspace\ableproj\sounds", FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
            My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\sounds")

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
                        File.Move(foundFile.Replace(".mp3", ".wav"), "Workspace\ableproj\sounds\" & Path.GetFileName(foundFile.Replace(".mp3", ".wav")))
                        File.Delete(foundFile)
                        Loading.DPr.Style = ProgressBarStyle.Continuous
                        Loading.DPr.Value += 1
                        Loading.DLb.Left = 40
                        Loading.DLb.Text = String.Format(Loading.loading_Sound_Open_msg, Loading.DPr.Value, ofd.FileNames.Length)
                        Loading.DLb.Refresh()
                    End If
                Next
            Catch fex As IOException 'I/O 오류 해결 코드.
                Thread.Sleep(100)
                GoTo fexLine
            End Try
        End If

        '-After Loading WAV/MP3!
        Loading.DPr.Value = Loading.DPr.Maximum
        If Loading.DPr.Value = FileNames.Length Then
            If FileNames.Length = Directory.GetFiles(Application.StartupPath & "\Workspace\ableproj\sounds\", "*.wav").Length Then
                Loading.DPr.Style = ProgressBarStyle.Marquee
                Loading.DPr.Refresh()
                Loading.DLb.Left = 40
                Loading.DLb.Text = "Loaded Sound Files. Please Wait..."
                Loading.DLb.Refresh()

                Loading.Dispose()
                If OpenProjectOnce = False Then MessageBox.Show("Sounds Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                abl_openedsnd = True
                SoundIsSaved = True

            Else
                MessageBox.Show("Error! - Code: MaxFileLength.Value = GetFiles.Length", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Error! - Code: LoadedFiles.Value = MaxFileLength.Value", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BGW_sounds_Completed(sender As Object, e As RunWorkerCompletedEventArgs) Handles BGW_sounds.RunWorkerCompleted
        Try
            If e.Error IsNot Nothing Then
                MessageBox.Show("Error - " & e.Error.Message & vbNewLine & "Error Message: " & e.Error.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            ElseIf e.Cancelled Then
                Exit Sub
            Else
                If abl_openedproj AndAlso abl_openedsnd Then
                    BGW_keySound.RunWorkerAsync()
                End If

                If OpenProjectOnce Then OpenKeyLEDToolStripMenuItem_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub OpenProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenProjectToolStripMenuItem.Click
        Dim alsOpen1 As New OpenFileDialog
        alsOpen1.Filter = "Ableton Project File|*.als"
        alsOpen1.Title = "Select a Ableton Project File"
        alsOpen1.AddExtension = False
        alsOpen1.Multiselect = False

        If alsOpen1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            ofd_FileName = alsOpen1.FileName
            OpenProjectOnce = True
            BGW_ablproj.RunWorkerAsync()
        End If
    End Sub

    Private Sub CheckUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckUpdateToolStripMenuItem.Click
        If BGW_CheckUpdate.IsBusy = False Then
            BGW_CheckUpdate.RunWorkerAsync()
        End If

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

    Private Sub CheckUpdate(sender As Object, e As DoWorkEventArgs) Handles BGW_CheckUpdate.DoWork
        Dim Client As New WebClient

        If My.Computer.Network.IsAvailable = True Then
            Client.DownloadFile("http://dver.ucv.kro.kr", TempDirectory & "\UniConverter-version.xml")
            vxml.Load(TempDirectory & "\UniConverter-version.xml")
            Dim setaNode As XmlNode
            setaNode = vxml.SelectSingleNode("/Update-XML/Update-Info")

            FileInfo = Version.Parse(setaNode.ChildNodes(1).InnerText)
            VerLog = setaNode.ChildNodes(2).InnerText.TrimStart

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
                    Client.DownloadFile("http://dpr.ucv.kro.kr", My.Computer.FileSystem.SpecialDirectories.Temp & "\UniConverter-Update.zip")
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
                            IsUpdated = True
                            If MessageBox.Show("Update Complete! UniConverter " & FileInfo.ToString & " is in 'UniConverter_v" & FileInfo.ToString & "' Folder.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) = DialogResult.OK Then
                                File.Delete(My.Computer.FileSystem.SpecialDirectories.Temp & "\UniConverter-Update.zip")
                                .Dispose()
                                Dim setNode As XmlNode = setxml.SelectSingleNode("/UniConverter-XML/UG-Settings")
                                If Convert.ToBoolean(setNode.ChildNodes(1).InnerText) = True Then
                                    Process.Start(String.Format("{0}\UniConverter_v{1}\UniConverter.exe", Application.StartupPath, FileInfo.ToString))
                                    Application.Exit()
                                End If
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
        Tpdby.Show()
    End Sub

    Private Sub MainProject_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If IsUpdated = True Then
            Exit Sub
        End If

        If IsSaved = False Then
            Dim result As DialogResult = MessageBox.Show("You didn't save your UniPack. Would you like to save your UniPack?", Me.Text & ": Not Saved", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                Save2Project(False)
            ElseIf result = DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' MainProject에서 프로젝트를 저장합니다.
    ''' </summary>
    ''' <param name="Waiting">기다릴까?</param>
    Public Sub Save2Project(Waiting As Boolean)
        Try
            Dim infoTitle As String = String.Empty
            infoTitle = File.ReadAllLines(Application.StartupPath & "\Workspace\unipack\info")(0).Replace("title=", "")

            Dim sfd As New SaveFileDialog()
            sfd.Filter = "Zip File|*.zip|UniPack File|*.uni"
            sfd.Title = "Save the UniPack"
            sfd.FileName = infoTitle
            sfd.AddExtension = False

            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\Workspace\unipack") Then
                    If Waiting = True Then
                        Invoke(Sub()
                                   Loading.Show()
                                   Loading.Text = Me.Text & ": Saving Ableton Project File to UniPack..."
                                   Loading.DLb.Left = 40
                                   Loading.Refresh()
                                   Dim result As String = Path.GetExtension(sfd.FileName)
                                   If result = ".zip" Then
                                       Loading.DLb.Text = "Creating UniPack to zip File..."
                                   ElseIf result = ".uni" Then
                                       Loading.DLb.Text = "Creating UniPack to uni File..."
                                   End If
                                   Loading.Refresh()
                               End Sub)
                    End If
                    If File.Exists(sfd.FileName) Then
                        File.Delete(sfd.FileName)
                        Thread.Sleep(300)
                    End If

                    ZipFile.CreateFromDirectory(Application.StartupPath & "\Workspace\unipack", sfd.FileName)
                    Loading.Dispose()
                    If Waiting = True Then
                        IsSaved = True
                        MessageBox.Show("Saved UniPack!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub infoTB1_TextChanged(sender As Object, e As EventArgs) Handles infoTB1.TextChanged
        IsSaved = False
    End Sub

    Private Sub infoTB2_TextChanged(sender As Object, e As EventArgs) Handles infoTB2.TextChanged
        IsSaved = False
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
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub DeveloperModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeveloperModeToolStripMenuItem.Click
        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
        DeveloperMode_Main.Show()
    End Sub

    Private Sub keyLEDBetaButton_Click(sender As Object, e As EventArgs) Handles keyLEDMIDEX_BetaButton.Click
        Try
            If abl_openedled = True Then
                keyLED_Edit.Show()
            Else
                Throw New FileNotFoundException("There is no LED Files! Please Try Open LED Files.")
            End If
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub OpenKeyLEDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenKeyLEDToolStripMenuItem.Click
        Try
            If keyLED_Edit.Visible Then
                keyLED_Edit.Dispose()
            End If

            ofd.Multiselect = True
            ofd.Filter = "LED Files|*.mid"
            If ofd.ShowDialog() = DialogResult.OK Then
                ofd_FileNames = ofd.FileNames
                BGW_keyLED.RunWorkerAsync()
            End If
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub SettingsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem1.Click
        UG_Settings.Show()
    End Sub

    Private Sub LoadButton_Click(sender As Object, e As EventArgs) Handles LoadButton.Click

        InListBox.Items.Clear()
        For i = 0 To MidiIn.NumberOfDevices - 1
            InListBox.Items.Add(MidiIn.DeviceInfo(i).ProductName)
        Next

        OutListBox.Items.Clear()
        For i = 0 To MidiOut.NumberOfDevices - 1
            OutListBox.Items.Add(MidiOut.DeviceInfo(i).ProductName)
        Next
    End Sub

    Private Sub ConnectButton_Click(sender As Object, e As EventArgs) Handles ConnectButton.Click
        DisconnectMidi()

#Region "MIDI 1"
        Try ' Connect the Launchpad to MIDI In
            midiinput = New MidiIn(InListBox.SelectedIndex)
            midiinput.Start()
            midiinput_avail = True

            Dim wowk As String = InListBox.Items(InListBox.SelectedIndex).ToString
            If wowk.Contains("Launchpad S") OrElse wowk.Contains("Launchpad Mini") Then
                midiinput_kind = 0 'launchpad s
            ElseIf wowk.Contains("Launchpad MK2") Then
                midiinput_kind = 1 'launchpad mk2
            ElseIf wowk.Contains("Launchpad Pro") Then
                midiinput_kind = 2 'launchpad pro
            ElseIf wowk.Contains("MidiFighter 64") OrElse wowk.Contains("MidiFighter64") Then
                midiinput_kind = 3 '미파64
            Else
                MessageBox.Show("Wrong input Launchpad! Please select other thing!" & vbNewLine & String.Format("(Selected MIDI Device: {0})", wowk), "Wrong Launchpad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                midioutput_kind = 0
                MIDIStatIn.Text = "MIDI Input: Not Connected"
            End If

            MIDIStatIn.Text = String.Format("MIDI Input: Connected ({0})", MidiIn.DeviceInfo(InListBox.SelectedIndex).ProductName)

        Catch ex As Exception
            MessageBox.Show("Failed to connect input device. Please try again or restart UniConverter." & vbNewLine & "Also, You can report this in 'Report Tab'.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            midiinput_avail = False
            If IsGreatExMode Then
                MessageBox.Show("Error: " & ex.Message & vbNewLine & "Exception StackTrace: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Try

        Try 'Connect the Launchpad to MIDI Out
            midioutput = New MidiOut(OutListBox.SelectedIndex)
            midioutput.Reset()
            midioutput_avail = True

            Dim wowc As String = OutListBox.Items(OutListBox.SelectedIndex).ToString
            If wowc.Contains("Launchpad S") OrElse wowc.Contains("Launchpad Mini") Then
                midioutput_kind = 0 'launchpad s
            ElseIf wowc.Contains("Launchpad MK2") Then
                midioutput_kind = 1 'launchpad mk2
            ElseIf wowc.Contains("Launchpad Pro") Then
                midioutput_kind = 2 'launchpad pro
            ElseIf wowc.Contains("MidiFighter 64") OrElse wowc.Contains("MidiFighter64") Then
                midioutput_kind = 3 '미파64
            Else
                MessageBox.Show("Wrong output Launchpad! Please select other thing!" & vbNewLine & String.Format("(Selected MIDI Device: {0})", wowc), "Wrong Launchpad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                midioutput_kind = 0
            End If

            MIDIStatOut.Text = String.Format("Midi Output: Connected ({0})", MidiOut.DeviceInfo(OutListBox.SelectedIndex).ProductName)
            midioutput.SendBuffer({240, 0, 32, 41, 9, 60, 85, 110, 105, 116, 111, 114, 32, 118, Asc(My.Application.Info.Version.Major), 46, Asc(My.Application.Info.Version.Minor), 46, Asc(My.Application.Info.Version.Build), 46, Asc(My.Application.Info.Version.Revision), 247})

            If SetUpLight_ Then
                'Launchpad Setup Light. [keyLED Version: v1.1.0.3]

                File.WriteAllText(Application.StartupPath & "\Workspace\TmpLED.txt", My.Resources.SuperLightText)
                With keyLED_Test
                    .lkind = midioutput_kind
                    .lmo = midioutput
                    .lmo.Reset()
                    .lmo.SendBuffer({240, 0, 32, 41, 9, 60, 85, 110, 105, 116, 111, 114, 32, 118, Asc(My.Application.Info.Version.Major), 46, Asc(My.Application.Info.Version.Minor), 46, Asc(My.Application.Info.Version.Build), 46, Asc(My.Application.Info.Version.Revision), 247})
                    .IsLaunchpaded = True

                    .LEDHandler_Launchpad()
                End With

            End If

        Catch ex As Exception
            MessageBox.Show("Failed to connect output device. Please try again or restart UniConverter." & vbNewLine & "Also, You can report this in 'Report Tab'.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            If IsGreatExMode Then
                MessageBox.Show("Error: " & ex.Message & vbNewLine & "Exception StackTrace: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            midioutput_kind = False
            MIDIStatOut.Text = "MIDI Output: Not Connected"
        End Try

#End Region
    End Sub
#Region "MIDI (Launchpad) Codes"
    Sub DisconnectMidi()
        If midiinput_avail = True Then

            Try

                midiinput.Stop()
                midiinput.Reset()
                midiinput.Dispose()


            Catch ex As Exception
            End Try
            midiinput_avail = False
        End If
        If midioutput_avail = True Then
            midioutput.Dispose()
            midioutput_avail = False
        End If


        MIDIStatIn.Text = "MIDI Input: Not Connected"
        MIDIStatOut.Text = "MIDI Output: Not Connected"
    End Sub

#Region "Pitch2XY"
    Private Function MIDIFighter64_GetKey_(ByVal 피치 As Integer) As String
        Select Case 피치
            Case 52
                Return "n;1;1"
            Case 53
                Return "n;1;2"
            Case 54
                Return "n;1;3"
            Case 55
                Return "n;1;4"
            Case 84
                Return "n;1;5"
            Case 85
                Return "n;1;6"
            Case 86
                Return "n;1;7"
            Case 87
                Return "n;1;8"

            Case 48
                Return "n;2;1"
            Case 49
                Return "n;2;2"
            Case 50
                Return "n;2;3"
            Case 51
                Return "n;2;4"
            Case 80
                Return "n;2;5"
            Case 81
                Return "n;2;6"
            Case 82
                Return "n;2;7"
            Case 83
                Return "n;2;8"

            Case 44
                Return "n;3;1"
            Case 45
                Return "n;3;2"
            Case 46
                Return "n;3;3"
            Case 47
                Return "n;3;4"
            Case 76
                Return "n;3;5"
            Case 77
                Return "n;3;6"
            Case 78
                Return "n;3;7"
            Case 79
                Return "n;3;8"

            Case 40
                Return "n;4;1"
            Case 41
                Return "n;4;2"
            Case 42
                Return "n;4;3"
            Case 43
                Return "n;4;4"
            Case 72
                Return "n;4;5"
            Case 73
                Return "n;4;6"
            Case 74
                Return "n;4;7"
            Case 75
                Return "n;4;8"

            Case 36
                Return "n;5;1"
            Case 37
                Return "n;5;2"
            Case 38
                Return "n;5;3"
            Case 39
                Return "n;5;4"
            Case 68
                Return "n;5;5"
            Case 69
                Return "n;5;6"
            Case 70
                Return "n;5;7"
            Case 71
                Return "n;5;8"

            Case 32
                Return "n;6;1"
            Case 33
                Return "n;6;2"
            Case 34
                Return "n;6;3"
            Case 35
                Return "n;6;4"
            Case 64
                Return "n;6;5"
            Case 65
                Return "n;6;6"
            Case 66
                Return "n;6;7"
            Case 67
                Return "n;6;8"

            Case 28
                Return "n;7;1"
            Case 29
                Return "n;7;2"
            Case 30
                Return "n;7;3"
            Case 31
                Return "n;7;4"
            Case 60
                Return "n;7;5"
            Case 61
                Return "n;7;6"
            Case 62
                Return "n;7;7"
            Case 63
                Return "n;7;8"

            Case 24
                Return "n;8;1"
            Case 25
                Return "n;8;2"
            Case 26
                Return "n;8;3"
            Case 27
                Return "n;8;4"
            Case 56
                Return "n;8;5"
            Case 57
                Return "n;8;6"
            Case 58
                Return "n;8;7"
            Case 59
                Return "n;8;8"

        End Select
        Return "c;1"
    End Function

    Private Function MIDIFighter64_GetKey(ByVal 피치 As Integer) As String
        Select Case 피치
            Case 64
                Return "1;1"
            Case 65
                Return "1;2"
            Case 66
                Return "1;3"
            Case 67
                Return "1;4"
            Case 96
                Return "1;5"
            Case 97
                Return "1;6"
            Case 98
                Return "1;7"
            Case 99
                Return "1;8"

            Case 60
                Return "2;1"
            Case 61
                Return "2;2"
            Case 62
                Return "2;3"
            Case 63
                Return "2;4"
            Case 64
                Return "2;5"
            Case 92
                Return "2;6"
            Case 93
                Return "2;7"
            Case 94
                Return "2;8"

            Case 56
                Return "3;1"
            Case 57
                Return "3;2"
            Case 58
                Return "3;3"
            Case 59
                Return "3;4"
            Case 88
                Return "3;5"
            Case 89
                Return "3;6"
            Case 90
                Return "3;7"
            Case 91
                Return "3;8"

            Case 52
                Return "4;1"
            Case 53
                Return "4;2"
            Case 54
                Return "4;3"
            Case 55
                Return "4;4"
            Case 84
                Return "4;5"
            Case 85
                Return "4;6"
            Case 86
                Return "4;7"
            Case 87
                Return "4;8"

            Case 48
                Return "5;1"
            Case 49
                Return "5;2"
            Case 50
                Return "5;3"
            Case 51
                Return "5;4"
            Case 84
                Return "5;5"
            Case 85
                Return "5;6"
            Case 86
                Return "5;7"
            Case 87
                Return "5;8"

            Case 44
                Return "6;1"
            Case 45
                Return "6;2"
            Case 46
                Return "6;3"
            Case 47
                Return "6;4"
            Case 80
                Return "6;5"
            Case 81
                Return "6;6"
            Case 82
                Return "6;7"
            Case 83
                Return "6;8"

            Case 40
                Return "7;1"
            Case 41
                Return "7;2"
            Case 42
                Return "7;3"
            Case 43
                Return "7;4"
            Case 72
                Return "7;5"
            Case 73
                Return "7;6"
            Case 74
                Return "7;7"
            Case 75
                Return "7;8"

            Case 36
                Return "8;1"
            Case 37
                Return "8;2"
            Case 38
                Return "8;3"
            Case 39
                Return "8;4"
            Case 68
                Return "8;5"
            Case 69
                Return "8;6"
            Case 70
                Return "8;7"
            Case 71
                Return "8;8"

        End Select
        Return "1;9"
    End Function


    Private Function LaunchPadS_MC_GetKey(ByVal 피치 As Integer) As String
        Select Case 피치
            Case 104
                Return 1
            Case 105
                Return 2
            Case 106
                Return 3
            Case 107
                Return 4
            Case 108
                Return 5
            Case 109
                Return 6
            Case 110
                Return 7
            Case 111
                Return 8
        End Select
        Return 1
    End Function

    Private Function LaunchPadMK2_MC_GetKey(ByVal 피치 As Integer) As String
        Select Case 피치
            Case 104
                Return 1
            Case 105
                Return 2
            Case 106
                Return 3
            Case 107
                Return 4
            Case 108
                Return 5
            Case 109
                Return 6
            Case 110
                Return 7
            Case 111
                Return 8
        End Select
        Return 1
    End Function

    Private Function LaunchPadPro_MC_GetKey(ByVal 피치 As Integer) As String
        Select Case 피치
            Case 91
                Return 1
            Case 92
                Return 2
            Case 93
                Return 3
            Case 94
                Return 4
            Case 95
                Return 5
            Case 96
                Return 6
            Case 97
                Return 7
            Case 98
                Return 8

                '체인 영역
            Case 89
                Return 9
            Case 79
                Return 10
            Case 69
                Return 11
            Case 59
                Return 12
            Case 49
                Return 13
            Case 39
                Return 14
            Case 29
                Return 15
            Case 19
                Return 16


            Case 8
                Return 17
            Case 7
                Return 18
            Case 6
                Return 19
            Case 5
                Return 20
            Case 4
                Return 21
            Case 3
                Return 22
            Case 2
                Return 23
            Case 1
                Return 24

            Case 10
                Return 25
            Case 20
                Return 26
            Case 30
                Return 27
            Case 40
                Return 28
            Case 50
                Return 29
            Case 60
                Return 30
            Case 70
                Return 31
            Case 80
                Return 32
        End Select
        Return 1
    End Function
#End Region
#End Region

    Private Sub keyLEDMIDEX_TestButton_Click(sender As Object, e As EventArgs) Handles keyLEDMIDEX_TestButton.Click
        keyLED_Test.Show()
        keyLED_Test.LoadkeyLEDText(keyLEDMIDEX_UniLED.Text)
    End Sub

    Private Sub KeyLEDMIDEX_ListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles keyLEDMIDEX_ListBox.SelectedIndexChanged
        Try

            If abl_openedled AndAlso abl_openedled2 Then
                If IsWorking = False Then

                    If keyLEDMIDEX_ListBox.SelectedItems.Count > 0 Then '이것이 신의 한수... SelectedItem 코드 작성 시 꼭 필요. (invaildArgument 오류)
                        Dim s As String = keyLEDMIDEX_ListBox.SelectedItem.ToString

                        keyLEDMIDEX_UniLED.Enabled = True
                        keyLEDMIDEX_UniLED.Clear()
                        keyLEDMIDEX_UniLED.Text = File.ReadAllText(String.Format("{0}\Workspace\unipack\keyLED\{1}", Application.StartupPath, s))

                        keyLEDMIDEX_TestButton.Enabled = True
                        keyLEDMIDEX_CopyButton.Enabled = True
                        keyLED_Test.LoadkeyLEDText(keyLEDMIDEX_UniLED.Text)
                    End If

                Else
                    Throw New TimeoutException("Please Wait...")
                End If
            Else
                Throw New FileNotFoundException("You must load the keyLED and save the file!")
            End If

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub BGW_keyLED__DoWork(sender As Object, e As DoWorkEventArgs) Handles BGW_keyLED_.DoWork
        Try

            If abl_openedproj AndAlso abl_openedled AndAlso Not e.Cancel Then

                With Loading
                    .Show()
                    .Text = "Converting Ableton LED to UniPack LED..."
                    .Refresh()
                    .DLb.Text = "Loading LED Infos..."
                    .DPr.Style = ProgressBarStyle.Marquee
                    .DPr.MarqueeAnimationSpeed = 1
                End With

                Dim s As String = Application.StartupPath & "\Workspace\ableproj\CoLED"
                Dim c As String = Application.StartupPath & "\Workspace\unipack\keyLED"

                IsWorking = True

                If Not Directory.Exists(c) Then
                    Directory.CreateDirectory(c)
                Else
                    My.Computer.FileSystem.DeleteDirectory(c, FileIO.DeleteDirectoryOption.DeleteAllContents)
                    Thread.Sleep(300)
                    Directory.CreateDirectory(c)
                End If

                Dim LEDSave As String = Application.StartupPath & "\Workspace\ableproj\LEDSave.uni"
                Dim LEDMapping As String = Application.StartupPath & "\Workspace\ableproj\LEDMapping.uni"
                Dim LEDs As String() = File.ReadAllLines(LEDSave)
                File.WriteAllText(LEDSave, StringsToString(LEDs))

                For i As Integer = 0 To LEDs.Count - 1
                    LEDs(i) = LEDs(i).Replace(";", "")

                    If LEDs(i).Contains("temp") Then
                        If LEDs(i).Contains("temp1") Then 'New Tempo (빠르기)

                            Dim id As Integer = Integer.Parse(LEDs(i).Split(",")(0).Replace("temp1", "").Replace("id", ""))
                            Dim speed As Integer = Integer.Parse(LEDs(i).Split(" ")(2))
                            LEDs(i) = String.Format("New Tempo/{0}/{1}", id, speed) '[1: 구문, 2: id, 3: 빠르기]

                        ElseIf LEDs(i).Contains("temp2") Then 'Clip Tempo (BPM)

                            Dim id As Integer = Integer.Parse(LEDs(i).Split(",")(0).Replace("temp2", "").Replace("id", ""))
                            Dim bpm As Integer = Integer.Parse(LEDs(i).Split(" ")(2))
                            LEDs(i) = String.Format("Clip Tempo/{0}/{1}", id, bpm) '[1: 구문, 2: id, 3: BPM]

                        End If

                    ElseIf LEDs(i).Contains("midifile") Then '미디 파일

                        Dim id As Integer = Integer.Parse(LEDs(i).Split(",")(0).Replace("midifile", "").Replace("id", ""))
                        Dim FileName As String = LEDs(i).Split(" ")(2).Split("/").Last()
                        LEDs(i) = String.Format("MIDI Extension/{0}/{1}", id, FileName) '[1: 구문, 2: id, 3: BPM]

                    End If

                Next

                File.WriteAllText(LEDMapping, StringsToString(LEDs))

                '또한 Clip Tempo는 BPM이며, [Clip Tempo = temp2]
                'New Tempo는 빠르기다. [New Tempo = temp1]

                Dim il As Integer = 0
                Dim Alrt As String = String.Empty
                For Each d As String In LEDs

                    'Beta Code!
                    '이 Beta Convert Code는 오류가 발생할 수 있습니다.
                    '주의사항: 완전 꼬인 스파게티 코드라서, 눈에 보기 좋지 않고, 코드도 꼬여서 프로그램이 뻗어버릴 확률이 높습니다.
                    '코드를 만질 때 주의해주세요!

                    '주의사항을 다 보셨다면, 당신은 Editor 권한을 가질 수 있습니다.

                    '이 코드는 Follow_JB님의 midi2keyLED를 참고하여 만든 코드. (Thanks to Follow_JB. :D)

                    'yi = XML의 수.
                    'il = XML keyLED 변수.

                    If String.IsNullOrWhiteSpace(d) Then
                        Continue For
                    End If

                    Dim d_arg As String = d.Split("/")(0)
                    Select Case d_arg

                        Case "MIDI Extension" '미디 파일.
                            Dim d_id As Integer = d.Split("/")(1)

                            Dim dFile As String = d.Split("/")(2)
                            Dim dIndex As Integer() = ReadAllIndex(LEDs, "MIDI Extension")
                            Dim dix As Integer = 0
#Region "Set the Tempo"
                            If Alrt = "adsfjkh" Then 'String.IsNullOrWhiteSpace(Alrt) = False
                                Dim skip_r As Boolean = False
                                For Each ri As String In Alrt.Split(";")
                                    If dFile = ri Then
                                        skip_r = True
                                        Exit For
                                    End If
                                Next

                                If skip_r Then
                                    Alrt = Alrt.Replace(dFile & ";", "")
                                    Continue For
                                End If
                            End If
#End Region '나중에 BPM 재조정 코드로 Alrt 변수를 재활용 할거임.

                            Loading.DLb.Left -= 70
                            Loading.DLb.Text = String.Format("Converting LED ({0}) to keyLED...", dFile)
                            Loading.Refresh()

                            Dim dPath As String = String.Format("{0}\Workspace\ableproj\CoLED\{1}", Application.StartupPath, dFile)
                            If File.Exists(dPath) = False Then
                                Throw New FileNotFoundException("MIDI File '" & dFile & "' doesn't exists. Try Again!")
                            End If

                            Dim keyLED As New MidiFile(dPath, False)
                            Dim str As String = String.Empty
                            Dim delaycount As Integer = 0
                            Dim UniNoteNumberX As Integer 'X
                            Dim UniNoteNumberY As Integer 'Y

                            For Each mdEvent_list In keyLED.Events
                                For Each mdEvent In mdEvent_list

                                    If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                                        Dim a As NoteOnEvent = DirectCast(mdEvent, NoteOnEvent)
                                        Dim bpm As New TempoEvent(500000, a.AbsoluteTime)

                                        If Not delaycount = a.AbsoluteTime OrElse Not a.DeltaTime = 0 Then
                                            str = str & vbNewLine & "d " & GetNoteDelay(keyLED_NoteEvents.NoteLength_2, bpm.Tempo, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delaycount)
                                        End If

                                        UniNoteNumberX = GX_keyLED(keyLED_NoteEvents.NoteNumber_1, a.NoteNumber)
                                        UniNoteNumberY = GY_keyLED(keyLED_NoteEvents.NoteNumber_1, a.NoteNumber)
                                        delaycount = a.AbsoluteTime

                                        If UniNoteNumberX = 0 AndAlso UniNoteNumberY = 0 Then
                                            Debug.WriteLine("Unknown Note Number. [ Note: " & a.NoteNumber & " ]")
                                            Continue For
                                        End If

                                        If Not UniNoteNumberX = -8192 Then
                                            str = str & vbNewLine & "o " & UniNoteNumberX & " " & UniNoteNumberY & " a " & a.Velocity
                                        Else
                                            str = str & vbNewLine & "o mc " & UniNoteNumberY & " a " & a.Velocity
                                        End If

                                    ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                                        Dim a As NoteEvent = DirectCast(mdEvent, NoteEvent)
                                        Dim bpm As New TempoEvent(500000, a.AbsoluteTime)

                                        If Not delaycount = a.AbsoluteTime OrElse Not a.DeltaTime = 0 Then
                                            str = str & vbNewLine & "d " & GetNoteDelay(keyLED_NoteEvents.NoteLength_2, bpm.Tempo, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delaycount)
                                        End If

                                        UniNoteNumberX = GX_keyLED(keyLED_NoteEvents.NoteNumber_1, a.NoteNumber)
                                        UniNoteNumberY = GY_keyLED(keyLED_NoteEvents.NoteNumber_1, a.NoteNumber)
                                        delaycount = a.AbsoluteTime

                                        If UniNoteNumberX = 0 AndAlso UniNoteNumberY = 0 Then
                                            Debug.WriteLine("Unknown Note Number. [ Note: " & a.NoteNumber & " ]")
                                            Continue For
                                        End If

                                        If Not UniNoteNumberX = -8192 Then
                                            str = str & vbNewLine & "f " & UniNoteNumberX & " " & UniNoteNumberY
                                        Else
                                            str = str & vbNewLine & "f mc " & UniNoteNumberY
                                        End If

                                    End If
                                Next
                            Next

                            '이제 Get Chain & X, Y from XML!!!
                            Dim ablprj As String = Application.StartupPath & "\Workspace\ableproj\abl_proj.xml"
                            Dim doc As New XmlDocument
                            Dim setNode As XmlNodeList
                            doc.Load(ablprj)
                            setNode = doc.GetElementsByTagName("MidiEffectBranch")

                            Dim UniPack_Chain As Integer = 1
                            Dim UniPack_X As Integer = 0
                            Dim UniPack_Y As Integer = 0
                            Dim UniPack_L As Integer = 0

                            Dim fileN As String = String.Empty
                            Dim x As XmlNode
                            Dim sFile As String = String.Empty
                            Loading.DLb.Left += 70
                            Loading.DLb.Text = "Extracting LED Infos..."
                            Loading.Refresh()

                            'PatchSlot > Value > MxDPatchRef > FileRef > Name > Value 'Midi Extension.amxd'
                            'LED Save 파일의 id는 MxDeviceMidiEffect의 LomId Value랑 같음.
                            Dim id_index As Integer = 0
                            Dim rnd As Integer = 0
                            Dim currentRnd As Integer = 1
                            Dim PrChain As Integer = 0
                            Dim isRandom As Boolean = False
                            For ndx As Integer = 0 To setNode.Count - 1
                                Dim fndError As Boolean = False
                                Dim currentid As Integer = 0
                                Dim MidiName As String = String.Empty
                                Try
                                    currentid = Integer.Parse(setNode(ndx).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("LomId").GetAttribute("Value"))
                                    MidiName = setNode(ndx).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("PatchSlot").Item("Value").Item("MxDPatchRef").Item("FileRef").Item("Name").GetAttribute("Value")
                                    fndError = False
                                Catch exN As NullReferenceException
                                    fndError = True
                                    If Cntstr(setNode(ndx).OuterXml, "</MidiEffectBranch>") > 1 Then
                                        rnd = Cntstr(setNode(ndx).OuterXml, "</MidiEffectBranch>")
                                        isRandom = True
                                    Else
                                        isRandom = False
                                    End If
                                End Try

                                If fndError = False Then
                                    currentid = Integer.Parse(setNode(ndx).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("LomId").GetAttribute("Value"))
                                    MidiName = setNode(ndx).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("PatchSlot").Item("Value").Item("MxDPatchRef").Item("FileRef").Item("Name").GetAttribute("Value")
                                    If d_id = currentid AndAlso MidiName = "Midi Extension.amxd" Then
                                        id_index = ndx
                                        Exit For
                                    End If
                                End If
                            Next
                            x = setNode(id_index)

                            UniPack_Chain = Integer.Parse(x.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1 'Get Chain.
                            UniPack_X = GX_keyLED(keyLED_NoteEvents.NoteNumber_1, Integer.Parse(x.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value"))) 'Get X Pos.
                            UniPack_Y = GY_keyLED(keyLED_NoteEvents.NoteNumber_1, Integer.Parse(x.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value"))) 'Get Y Pos.
                            UniPack_L = 1

                            If UniPack_Chain > 8 OrElse UniPack_Chain = 0 OrElse UniPack_X = -8192 Then
                                Continue For
                            End If

                            If Not PrChain = 0 AndAlso isRandom Then
                                UniPack_Chain = PrChain
                                currentRnd += 1

                                If rnd = currentRnd Then
                                    PrChain = 0
                                    isRandom = False
                                    rnd = 0
                                    currentRnd = 0
                                End If
                            Else
                                PrChain = 0
                            End If

                            Dim LoopNumber_1 As Integer() = New Integer(1) {}
                            Dim LoopNumber_1bool As Boolean 'Chain Value = ?
                            LoopNumber_1(0) = Integer.Parse(x.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1
                            LoopNumber_1(1) = Integer.Parse(x.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1
                            LoopNumber_1bool = LoopNumber_1(0) = LoopNumber_1(1)

                            Dim LoopNumber_2 As Integer() = New Integer(1) {}
                            Dim LoopNumber_2bool As Boolean 'Key Value = ?
                            LoopNumber_2(0) = Integer.Parse(x.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value"))
                            LoopNumber_2(1) = Integer.Parse(x.Item("ZoneSettings").Item("KeyRange").Item("Max").GetAttribute("Value"))
                            LoopNumber_2bool = LoopNumber_2(0) = LoopNumber_2(1)

                            If LoopNumber_1bool = False Then

                                '시작 길이와 끝 길이가 다른 경우 (Loop 1 활성화 시)
                                For p As Integer = LoopNumber_1(0) To LoopNumber_1(1)

                                    If LoopNumber_2bool Then

                                        UniPack_Chain = p
#Region "Save the keyLED with Overwrite Protection!"
                                        If File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) OrElse File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} a", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) Then
                                            If File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) Then
                                                My.Computer.FileSystem.RenameFile(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), String.Format("{0} {1} {2} {3} a", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L))
                                            End If
                                            For Each lpn As Char In LEDMapping_N
                                                If Not File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn)) Then
                                                    File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn), str)
                                                    Exit For
                                                End If
                                            Next

                                        Else
                                            File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), str)
                                        End If
#End Region

                                    ElseIf LoopNumber_2bool = False Then

                                        For q As Integer = LoopNumber_2(0) To LoopNumber_2(1)
                                            UniPack_Chain = p
                                            UniPack_X = GX_keyLED(keyLED_NoteEvents.NoteNumber_1, q)
                                            UniPack_Y = GY_keyLED(keyLED_NoteEvents.NoteNumber_1, q)
#Region "Save the keyLED with Overwrite Protection!"
                                            If File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) OrElse File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} a", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) Then
                                                If File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) Then
                                                    My.Computer.FileSystem.RenameFile(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), String.Format("{0} {1} {2} {3} a", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L))
                                                End If
                                                For Each lpn As Char In LEDMapping_N
                                                    If Not File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn)) Then
                                                        File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn), str)
                                                        Exit For
                                                    End If
                                                Next

                                            Else
                                                File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), str)
                                            End If
#End Region
                                        Next

                                    End If

                                Next

                            ElseIf LoopNumber_1bool Then

                                If LoopNumber_2bool Then
                                    '기본값.
#Region "Save the keyLED with Overwrite Protection!"
                                    If File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) OrElse File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} a", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) Then
                                        If File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) Then
                                            My.Computer.FileSystem.RenameFile(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), String.Format("{0} {1} {2} {3} a", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L))
                                        End If
                                        For Each lpn As Char In LEDMapping_N
                                            If Not File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn)) Then
                                                File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn), str)
                                                Exit For
                                            End If
                                        Next

                                    Else
                                        File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), str)
                                    End If
#End Region

                                ElseIf LoopNumber_2bool = False Then

                                    For q As Integer = LoopNumber_2(0) To LoopNumber_2(1)
                                        UniPack_X = GX_keyLED(keyLED_NoteEvents.NoteNumber_1, q)
                                        UniPack_Y = GY_keyLED(keyLED_NoteEvents.NoteNumber_1, q)
#Region "Save the keyLED with Overwrite Protection!"
                                        If File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) OrElse File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} a", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) Then
                                            If File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) Then
                                                My.Computer.FileSystem.RenameFile(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), String.Format("{0} {1} {2} {3} a", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L))
                                            End If
                                            For Each lpn As Char In LEDMapping_N
                                                If Not File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn)) Then
                                                    File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn), str)
                                                    Exit For
                                                End If
                                            Next

                                        Else
                                            File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), str)
                                        End If
#End Region
                                    Next

                                End If

                            End If
                            Debug.WriteLine(dFile & ", x:" & UniPack_X & " y:" & UniPack_Y)
                            il = dIndex(dix)
                            dix += 1

                        Case "Clip Tempo"
                            Dim d_id As Integer = d.Split("/")(1)

                        Case "New Tempo"
                            Dim d_id As Integer = d.Split("/")(1)

                    End Select

                Next

                Debug.WriteLine("Finish...")
                Loading.DLb.Text = "Loading UniPack LEDs..."
                Loading.DLb.Refresh()
                For Each d As String In My.Computer.FileSystem.GetFiles(c, FileIO.SearchOption.SearchTopLevelOnly)
                    Dim k As String = Path.GetFileName(d)
                    Invoke(Sub()
                               keyLEDMIDEX_ListBox.Items.Add(k)
                           End Sub)
                Next

                Loading.Dispose()

                If w8t4abl = "keyLED" Then
                    w8t4abl = String.Empty
                End If

                Invoke(Sub()
                           keyLEDMIDEX_UniLED.Enabled = True
                           keyLEDMIDEX_UniLED.Clear()
                       End Sub)

                keyLEDIsSaved = True

            Else
                w8t4abl = "keyLED"
                e.Cancel = True
            End If

        Catch ex As Exception
            Loading.Dispose()
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            e.Cancel = True
        End Try
    End Sub

    Private Sub BGW_keyLED__RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BGW_keyLED_.RunWorkerCompleted
        Try
            If e.Error IsNot Nothing Then
                MessageBox.Show("Error - " & e.Error.Message & vbNewLine & "Error Message: " & e.Error.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            ElseIf e.Cancelled Then
                Exit Sub
            Else

                IsWorking = False
                MessageBox.Show("LED File Converted!" & vbNewLine & "You can show the LEDs on 'keyLED (MIDI Extension)' Tab!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' String의 수를 세줍니다. 결과는 Integer.
    ''' </summary>
    Public Function Cntstr(ByVal inputString As String, ByVal pattern As String) As Integer
        Return Regex.Split(inputString, pattern).Length - 1
    End Function

    ''' <summary>
    ''' inputString의 pattern을 찾아 Index를 찾아 줍니다.
    ''' </summary>
    ''' <param name="inputString"></param>
    ''' <param name="pattern"></param>
    ''' <param name="Skip"></param>
    ''' <returns></returns>
    Public Function ReadTextIndex(ByVal inputString() As String, ByVal pattern As String, Optional ByVal Skip As Integer = 0) As Integer
        For i As Integer = 0 To inputString.Count - 1
            If inputString(i).Contains(pattern) Then
                If Not Skip = 0 Then
                    Skip -= 1
                    Continue For
                End If

                Return i
            End If
        Next
        Return -1
    End Function

    ''' <summary>
    ''' 모든 Index를 반환합니다.
    ''' </summary>
    ''' <param name="inputString"></param>
    ''' <param name="pattern"></param>
    ''' <returns></returns>
    Public Function ReadAllIndex(ByVal inputString() As String, ByVal pattern As String) As Integer()
        Dim ifoun As Integer = 0
        Dim lq As Integer() = New Integer(inputString.Count - 1) {}

        For i As Integer = 0 To lq.Count - 1
            If inputString(i).Contains(pattern) Then
                lq(ifoun) = i
                ifoun += 1
            End If
        Next

        If ifoun = 0 Then
            Return {-1}
        End If

        Dim finlq As Integer() = New Integer(ifoun) {}
        For q As Integer = 0 To ifoun - 1
            finlq(q) = lq(q)
        Next


        Return finlq
    End Function

    ''' <summary>
    ''' String() 형식을 String으로 변환 합니다.
    ''' </summary>
    ''' <param name="inputstr">문자열</param>
    ''' <returns></returns>
    Public Function StringsToString(inputstr() As String) As String
        Dim str As String = String.Empty
        For i As Integer = 0 To inputstr.Count - 1
            If Not i = inputstr.Count - 1 Then
                str = str & inputstr(i) & vbNewLine
            Else
                str = str & inputstr(i)
            End If
        Next
        Return str
    End Function

    Private Sub KeyLEDMIDEX_CopyButton_Click(sender As Object, e As EventArgs) Handles keyLEDMIDEX_CopyButton.Click
        Try

            If keyLEDMIDEX_UniLED.Enabled = False Then
                MessageBox.Show("First, You should convert LED!", "UniConverter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Clipboard.SetText(keyLEDMIDEX_UniLED.Text)
                MessageBox.Show("UniPack LED Copied!", "UniConverter", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub NoteOn_Test(sender As Object, e As MidiInMessageEventArgs) Handles midiinput.MessageReceived
        'NAudio랑 A2UP가 반드시 필요 합니다.

        Try

            Dim a As MidiEvent = e.MidiEvent '현재 미디 이벤트.
            If MidiEvent.IsNoteOn(a) Then '만약 미디 이벤트가 Note On 일 경우
                If IsMIDITest Then

                    Dim b As NoteOnEvent = DirectCast(a, NoteOnEvent) '미디 이벤트를 Note On 이벤트로 변환
                    Dim x As Integer = GX_keyLED(keyLED_NoteEvents.NoteNumber_2, b.NoteNumber) 'Note On 번호를 유니팩의 x로 변환.
                    Dim y As Integer = GY_keyLED(keyLED_NoteEvents.NoteNumber_2, b.NoteNumber) 'Note On 번호를 유니팩의 y로 변환.

                    If Not x = -8192 Then 'Chain이 아닌 경우
                        MessageBox.Show("Note: " & x & ", " & y, "MIDI In Test", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Note: (Chain) " & y, "MIDI In Test", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If

            End If

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub MIDIn_Test_Click(sender As Object, e As EventArgs) Handles MIDIn_Test.Click
        If IsMIDITest Then
            IsMIDITest = False
            MessageBox.Show("MIDI Input and Note On Test Disabled.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            IsMIDITest = True
            MessageBox.Show("MIDI Input and Note On Test Enabled!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class
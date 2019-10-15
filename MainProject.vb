Imports System.IO
Imports System.IO.Compression
Imports NAudio.Midi
Imports ICSharpCode.SharpZipLib.GZip
Imports ICSharpCode.SharpZipLib.Core
Imports System.Net
Imports System.Threading
Imports System.Xml
Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports A2UP
Imports A2UP.A2U.keyLED_MIDEX
Imports A2UP.A2U.keySound
Imports WMPLib
Imports System.Drawing.Drawing2D
Imports NAudio.Wave

Public Class MainProject

#Region "UniConverter-MainProject(s)"
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

    ''' <summary>
    ''' 다른 매핑할 때 소리를 재생함.
    ''' </summary>
    Dim ks_lpn As Integer = 0

    ''' <summary>
    ''' 현재 소리를 재생.
    ''' </summary>
    Dim ks_lpu As Integer = 1

    ''' <summary>
    ''' keySound 변환 여부.
    ''' </summary>
    Public ks_Converted As Boolean

#End Region
#Region "MainProject-keyLED(s)"
    Private stopitnow As Boolean = False

    ''' <summary>
    ''' MainProject keyLED 저장 여부. (keyLED / keyLED (MIDEX))
    ''' </summary>
    Public Shared keyLEDIsSaved As Boolean

    ''' <summary>
    ''' keyLED 버튼 저장
    ''' </summary>
    Public Shared kl_ctrl As New Dictionary(Of String, Button)

    ''' <summary>
    ''' 현재 선택한 체인. (1~8)
    ''' </summary>
    Public Shared klUniPack_SelectedChain As Integer = 1

    ''' <summary>
    ''' HTML to LED Velocity.
    ''' </summary>
    Public led As New ledReturn

    ''' <summary>
    ''' keyLED 변환 여부.
    ''' </summary>
    Public kl_Converted As Boolean
#End Region
#Region "MainProject-Thread(s)"
    Public Shared ofd_FileName As String
    Private ofd_FileNames() As String
    Private trd_ListView As ListView
    Private updateShow As Boolean = False
#End Region

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

    ''' <summary>
    ''' 유니컨버터 언어. (English / Korean)
    ''' </summary>
    Public Shared lang As Translator.tL

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

    Private Sub MainProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            Dim file_ex As String = Application.StartupPath + "\settings.xml"

            If File.Exists(file_ex) = False Then
                Throw New FileNotFoundException("Settings File doesn't exists.")
            End If

            'License File of Developer Mode.
            If File.Exists(LicenseFile(0)) AndAlso File.ReadAllText(LicenseFile(0)).GetHashCode = My.Resources.License_DeveloperMode.GetHashCode Then
                IsDeveloperMode = True
            End If

            'License File of Great Exception Mode.
            If File.Exists(LicenseFile(1)) AndAlso File.ReadAllText(LicenseFile(1)).GetHashCode = My.Resources.License_GreatExMode.GetHashCode Then
                IsGreatExMode = True
            End If

            If IsDeveloperMode Then
                Me.Text &= " (Enabled Developer Mode)"
                DeveloperModeToolStripMenuItem.Visible = True
            End If

            DeleteWorkspaceDir() 'Workspace의 UniPack 폴더 정리.
            Thread.Sleep(500)

#Region "Dictionary 버튼 추가"
            'keySound 8x8 유니패드 버튼
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

            'keyLED 8x8 + Top Lights 유니패드 버튼
            kl_ctrl.Add(11, u11)
            kl_ctrl.Add(12, u12)
            kl_ctrl.Add(13, u13)
            kl_ctrl.Add(14, u14)
            kl_ctrl.Add(15, u15)
            kl_ctrl.Add(16, u16)
            kl_ctrl.Add(17, u17)
            kl_ctrl.Add(18, u18)
            kl_ctrl.Add(21, u21)
            kl_ctrl.Add(22, u22)
            kl_ctrl.Add(23, u23)
            kl_ctrl.Add(24, u24)
            kl_ctrl.Add(25, u25)
            kl_ctrl.Add(26, u26)
            kl_ctrl.Add(27, u27)
            kl_ctrl.Add(28, u28)
            kl_ctrl.Add(31, u31)
            kl_ctrl.Add(32, u32)
            kl_ctrl.Add(33, u33)
            kl_ctrl.Add(34, u34)
            kl_ctrl.Add(35, u35)
            kl_ctrl.Add(36, u36)
            kl_ctrl.Add(37, u37)
            kl_ctrl.Add(38, u38)
            kl_ctrl.Add(41, u41)
            kl_ctrl.Add(42, u42)
            kl_ctrl.Add(43, u43)
            kl_ctrl.Add(44, u44)
            kl_ctrl.Add(45, u45)
            kl_ctrl.Add(46, u46)
            kl_ctrl.Add(47, u47)
            kl_ctrl.Add(48, u48)
            kl_ctrl.Add(51, u51)
            kl_ctrl.Add(52, u52)
            kl_ctrl.Add(53, u53)
            kl_ctrl.Add(54, u54)
            kl_ctrl.Add(55, u55)
            kl_ctrl.Add(56, u56)
            kl_ctrl.Add(57, u57)
            kl_ctrl.Add(58, u58)
            kl_ctrl.Add(61, u61)
            kl_ctrl.Add(62, u62)
            kl_ctrl.Add(63, u63)
            kl_ctrl.Add(64, u64)
            kl_ctrl.Add(65, u65)
            kl_ctrl.Add(66, u66)
            kl_ctrl.Add(67, u67)
            kl_ctrl.Add(68, u68)
            kl_ctrl.Add(71, u71)
            kl_ctrl.Add(72, u72)
            kl_ctrl.Add(73, u73)
            kl_ctrl.Add(74, u74)
            kl_ctrl.Add(75, u75)
            kl_ctrl.Add(76, u76)
            kl_ctrl.Add(77, u77)
            kl_ctrl.Add(78, u78)
            kl_ctrl.Add(81, u81)
            kl_ctrl.Add(82, u82)
            kl_ctrl.Add(83, u83)
            kl_ctrl.Add(84, u84)
            kl_ctrl.Add(85, u85)
            kl_ctrl.Add(86, u86)
            kl_ctrl.Add(87, u87)
            kl_ctrl.Add(88, u88)

            'UniPad MC Buttons
            kl_ctrl.Add("mc1", mc1)
            kl_ctrl.Add("mc2", mc2)
            kl_ctrl.Add("mc3", mc3)
            kl_ctrl.Add("mc4", mc4)
            kl_ctrl.Add("mc5", mc5)
            kl_ctrl.Add("mc6", mc6)
            kl_ctrl.Add("mc7", mc7)
            kl_ctrl.Add("mc8", mc8)
            kl_ctrl.Add("mc9", mc9)
            kl_ctrl.Add("mc10", mc10)
            kl_ctrl.Add("mc11", mc11)
            kl_ctrl.Add("mc12", mc12)
            kl_ctrl.Add("mc13", mc13)
            kl_ctrl.Add("mc14", mc14)
            kl_ctrl.Add("mc15", mc15)
            kl_ctrl.Add("mc16", mc16)
            kl_ctrl.Add("mc17", mc17)
            kl_ctrl.Add("mc18", mc18)
            kl_ctrl.Add("mc19", mc19)
            kl_ctrl.Add("mc20", mc20)
            kl_ctrl.Add("mc21", mc21)
            kl_ctrl.Add("mc22", mc22)
            kl_ctrl.Add("mc23", mc23)
            kl_ctrl.Add("mc24", mc24)
            kl_ctrl.Add("mc25", mc25)
            kl_ctrl.Add("mc26", mc26)
            kl_ctrl.Add("mc27", mc27)
            kl_ctrl.Add("mc28", mc28)
            kl_ctrl.Add("mc29", mc29)
            kl_ctrl.Add("mc30", mc30)
            kl_ctrl.Add("mc31", mc31)
            kl_ctrl.Add("mc32", mc32)
#End Region
#Region "패드 설정"
            'keyLED Pad64.
            For i = 1 To 32 'Width - 2, Height - 1 빼주는 이유는 빼주지 않으면 LED가 어긋나게 실행이 되기 때문이다.
                Dim circle As New GraphicsPath
                circle.AddEllipse(New Rectangle(0, 0, kl_ctrl("mc" & i).Size.Width - 2, kl_ctrl("mc" & i).Size.Height - 1))
                kl_ctrl("mc" & i).Region = New Region(circle)
                kl_ctrl("mc" & i).ForeColor = Color.Gray
            Next
#End Region
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
            ks_Converted = False
            kl_Converted = False

            w8t4abl = String.Empty
            OpenProjectOnce = False
            IsMIDITest = False

            keyLEDMIDEX_LEDViewMode.Checked = True
            keyLEDPad_Flush(False)
            keyLEDMIDEX_BetaButton.Enabled = False
#End Region


            setxml.Load(file_ex)
            Dim setNode As XmlNode
            setNode = setxml.SelectSingleNode("/UniConverter-XML/UniConverter-Settings")

            If setNode.ChildNodes(0).InnerText = "True" Then
                BGW_CheckUpdate.RunWorkerAsync()
            End If

            If setNode.ChildNodes(2).InnerText = "True" Then
                SetUpLight_ = True
            End If

            'Text of Info TextBox
            infoTB1.Text = "My Amazing UniPack!" 'Title
            infoTB2.Text = "UniConverter, " & My.Computer.Name 'Producer Name

            'Translate the Language from "Translator" Class.
            Dim Ln As String = setNode.ChildNodes(3).InnerText
            Dim tLn As Translator.tL = Translator.GetLnEnum(Ln)
            lang = tLn

            Dim trn As New Translator(tLn, IsDeveloperMode)
            trn.TranslateMain()

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

    Public Sub DeleteWorkspaceDir()
        If Directory.Exists(Application.StartupPath & "\Workspace") Then
            Directory.Delete(Application.StartupPath & "\Workspace", True)
            Thread.Sleep(500)
            Directory.CreateDirectory(Application.StartupPath & "\Workspace")
        Else
            Directory.CreateDirectory(Application.StartupPath & "\Workspace")
        End If
    End Sub

    Private Sub OpenSoundsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SoundsToolStripMenuItem.Click
        Dim ofd As New OpenFileDialog
        Select Case lang
            Case Translator.tL.English
                ofd.Filter = "wav sound files|*.wav|mp3 sounds files|*.mp3"
                ofd.Title = "Select Sounds"
            Case Translator.tL.Korean
                ofd.Filter = "wav 파일|*.wav|mp3 파일|*.mp3"
                ofd.Title = "음악 파일을 선택하세요"
        End Select
        ofd.Multiselect = True

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
        ThreadPool.QueueUserWorkItem(AddressOf Save2Project, True)
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
            Dim FileNames As String() = ofd_FileNames

            UI(Sub()
                   Loading.Show()
                   Loading.DPr.Maximum = FileNames.Length
                   Loading.DLb.Left = 40
                   Select Case lang
                       Case Translator.tL.English
                           Loading.Text = Me.Text & ": " & Loading.MsgEn.loading_LED_open_msg
                           Loading.DLb.Text = Loading.MsgEn.loading_LED_open_msg
                       Case Translator.tL.Korean
                           Loading.Text = Me.Text & ": " & Loading.MsgKr.loading_LED_open_msg
                           Loading.DLb.Text = Loading.MsgKr.loading_LED_open_msg
                   End Select
               End Sub)

            If Directory.Exists("Workspace\ableproj\CoLED") Then
                My.Computer.FileSystem.DeleteDirectory("Workspace\ableproj\CoLED", FileIO.DeleteDirectoryOption.DeleteAllContents)
                Directory.CreateDirectory("Workspace\ableproj\CoLED")
            Else
                Directory.CreateDirectory("Workspace\ableproj\CoLED")
            End If

            For i = 0 To FileNames.Length - 1
                File.Copy(FileNames(i), "Workspace\ableproj\CoLED\" & FileNames(i).Split("\").Last, True)
                UI(Sub()
                       Loading.DPr.Style = ProgressBarStyle.Continuous
                       Loading.DPr.Value += 1
                       Loading.DLb.Left = 40
                       Select Case lang
                           Case Translator.tL.English
                               Loading.DLb.Text = String.Format(Loading.MsgEn.loading_LED_open_msg, Loading.DPr.Value, FileNames.Length)
                           Case Translator.tL.Korean
                               Loading.DLb.Text = String.Format(Loading.MsgKr.loading_LED_open_msg, Loading.DPr.Value, FileNames.Length)
                       End Select
                   End Sub)
            Next

            UI(Sub()
                   Loading.DPr.Value = Loading.DPr.Maximum
                   Loading.DPr.Style = ProgressBarStyle.Marquee
                   Loading.DLb.Left = 40
                   Select Case lang
                       Case Translator.tL.English
                           Loading.DLb.Text = Loading.MsgEn.loading_Sound_Loaded_msg
                       Case Translator.tL.Korean
                           Loading.DLb.Text = Loading.MsgKr.loading_Sound_Loaded_msg
                   End Select
               End Sub)

            abl_openedled = True
            UI(Sub() Loading.Dispose())
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

                keyLEDMIDEX_BetaButton.Enabled = True

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

    'LED Save 파일 (0save 파일, 미디 익스텐션용)
    Private Sub BGW_keyLED2_DoWork(sender As Object, e As DoWorkEventArgs) Handles BGW_keyLED2.DoWork
        Try
            Dim FileName As String = ofd_FileName

            If String.IsNullOrEmpty(FileName) OrElse stopitnow Then
                e.Cancel = True
            End If

            If e.Cancel = False Then
                UI(Sub()
                       Loading.Show()
                       FileName = ofd_FileName
                       Loading.DPr.Maximum = 1
                       Loading.DLb.Left = 40
                       Select Case lang
                           Case Translator.tL.English
                               Loading.Text = Me.Text & ": " & Loading.MsgEn.loading_LEDSave_open_msg
                               Loading.DLb.Text = Loading.MsgEn.loading_LEDSave_open_msg
                           Case Translator.tL.Korean
                               Loading.Text = Me.Text & ": " & Loading.MsgKr.loading_LEDSave_open_msg
                               Loading.DLb.Text = Loading.MsgKr.loading_LEDSave_open_msg
                       End Select
                   End Sub)

                File.Copy(FileName, Application.StartupPath & "\Workspace\ableproj\LEDSave.uni", True)
                UI(Sub()
                       Loading.DPr.Style = ProgressBarStyle.Continuous
                       Loading.DPr.Value = 1
                       Loading.DLb.Left = 40
                       Select Case lang
                           Case Translator.tL.English
                               Loading.DLb.Text = String.Format(Loading.MsgEn.loading_LED_open_msg, Loading.DPr.Value, 1)
                           Case Translator.tL.Korean
                               Loading.DLb.Text = String.Format(Loading.MsgKr.loading_LED_open_msg, Loading.DPr.Value, 1)
                       End Select

                       Loading.DPr.Value = Loading.DPr.Maximum
                       Loading.DPr.Style = ProgressBarStyle.Marquee
                       Loading.DLb.Left = 40
                       Select Case lang
                           Case Translator.tL.English
                               Loading.DLb.Text = Loading.MsgEn.loading_LEDSave_Loaded_msg
                           Case Translator.tL.Korean
                               Loading.DLb.Text = Loading.MsgKr.loading_LEDSave_Loaded_msg
                       End Select
                   End Sub)

                abl_openedled2 = True
                UI(Sub() Loading.Dispose())
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
                    BGW_keySound.RunWorkerAsync()
                Else
                    Select Case lang
                        Case Translator.tL.English
                            MessageBox.Show("LED Files Loaded! You can edit LEDs in 'keyLED (MIDI Extension)' Tab.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Case Translator.tL.Korean
                            MessageBox.Show("LED 파일이 로딩되었습니다! 'keyLED (미디 익스텐션)' 탭에서 LED를 편집할 수 있습니다.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Select
                    BGW_keyLED_.RunWorkerAsync()
                End If
                Exit Sub
            Else
                If OpenProjectOnce Then
                    BGW_keySound.RunWorkerAsync()
                Else
                    Select Case lang
                        Case Translator.tL.English
                            MessageBox.Show("LED Files Loaded! You can edit LEDs in 'keyLED (MIDI Extension)' Tab.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Case Translator.tL.Korean
                            MessageBox.Show("LED 파일이 로딩되었습니다! 'keyLED (미디 익스텐션)' 탭에서 LED를 편집할 수 있습니다.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Select
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

        UI(Sub()
               Loading.Show()
               Loading.DPr.Style = ProgressBarStyle.Marquee
               Loading.DPr.MarqueeAnimationSpeed = 10
               Loading.DLb.Left = 60
               Select Case lang
                   Case Translator.tL.English
                       Loading.Text = Me.Text & ": Loading The Ableton Project File..."
                       Loading.DLb.Text = Loading.MsgEn.loading_Project_Load_msg
                   Case Translator.tL.Korean
                       Loading.Text = Me.Text & ": 에이블톤 프로젝트 파일을 불러오는 중..."
                       Loading.DLb.Text = Loading.MsgKr.loading_Project_Load_msg
               End Select
           End Sub)

        abl_FileName = FileName
        File.Copy(FileName, "Workspace\ableproj\abl_proj.gz", True)

        UI(Sub()
               Select Case lang
                   Case Translator.tL.English
                       Loading.DLb.Text = Loading.MsgEn.loading_Project_Extract_msg
                   Case Translator.tL.Korean
                       Loading.DLb.Text = Loading.MsgKr.loading_Project_Extract_msg
               End Select
           End Sub)
        ExtractGZip("Workspace\ableproj\abl_proj.gz", "Workspace\ableproj")
        Thread.Sleep(300)

        UI(Sub()
               Select Case lang
                   Case Translator.tL.English
                       Loading.DLb.Text = Loading.MsgEn.loading_Project_DeleteTmp_msg
                   Case Translator.tL.Korean
                       Loading.DLb.Text = Loading.MsgKr.loading_Project_DeleteTmp_msg
               End Select
           End Sub)
        File.Delete("Workspace\ableproj\abl_proj.gz")
        File.Delete("Workspace\ableproj\abl_proj.xml")

        UI(Sub()
               Select Case lang
                   Case Translator.tL.English
                       Loading.DLb.Text = Loading.MsgEn.loading_Project_ChangeExt_msg
                   Case Translator.tL.Korean
                       Loading.DLb.Text = Loading.MsgKr.loading_Project_ChangeExt_msg
               End Select
           End Sub)
        File.Move("Workspace\ableproj\abl_proj", "Workspace\ableproj\abl_proj.xml")

        UI(Sub()
               Select Case lang
                   Case Translator.tL.English
                       Loading.DLb.Text = Loading.MsgEn.loading_Project_DeleteTmp_msg
                   Case Translator.tL.Korean
                       Loading.DLb.Text = Loading.MsgKr.loading_Project_DeleteTmp_msg
               End Select
           End Sub)
        File.Delete("Workspace\ableproj\abl_proj")



        'Reading Informations of Ableton Project.

        'Ableton Project's Name.
        UI(Sub()
               Select Case lang
                   Case Translator.tL.English
                       Loading.DLb.Text = Loading.MsgEn.loading_Project_FileName_msg
                   Case Translator.tL.Korean
                       Loading.DLb.Text = Loading.MsgKr.loading_Project_FileName_msg
               End Select
           End Sub)

        Dim FinalName As String = Path.GetFileNameWithoutExtension(FileName)

        'Ableton Project's Chain.
        UI(Sub()
               Loading.DLb.Left = 130
               Select Case lang
                   Case Translator.tL.English
                       Loading.DLb.Text = Loading.MsgEn.loading_Project_Chain_msg
                   Case Translator.tL.Korean
                       Loading.DLb.Text = Loading.MsgKr.loading_Project_Chain_msg
               End Select
           End Sub)
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

        UI(Sub()
               Loading.DLb.Left = 40
               Select Case lang
                   Case Translator.tL.English
                       Loading.DLb.Text = Loading.MsgEn.loading_Project_Load_msg
                   Case Translator.tL.Korean
                       Loading.DLb.Text = Loading.MsgKr.loading_Project_Load_msg
               End Select
           End Sub)

        'XML File Load.
        Invoke(Sub()
                   infoTB1.Text = abl_Name
                   infoTB3.Text = abl_Chain
               End Sub)

        abl_openedproj = True
        UniPack_SaveInfo(False)
        UI(Sub() Loading.Dispose())

        If OpenProjectOnce = False Then
            Select Case lang
                Case Translator.tL.English
                    MessageBox.Show("Ableton Project File Loaded!" & vbNewLine & "You can edit info in Information Tab.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Case Translator.tL.Korean
                    MessageBox.Show("에이블톤 프로젝트 파일이 로딩되었습니다!" & vbNewLine & "'정보' 탭에서 정보를 수정할 수 있습니다.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Select
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

                If abl_openedproj AndAlso abl_openedsnd Then
                    BGW_keySound.RunWorkerAsync()
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
        Select Case lang
            Case Translator.tL.English
                alsOpen1.Filter = "Ableton Project File|*.als"
                alsOpen1.Title = "Select a Ableton Project File"
            Case Translator.tL.Korean
                alsOpen1.Filter = "에이블톤 프로젝트 파일|*.als"
                alsOpen1.Title = "에이블톤 프로젝트 파일을 선택하세요"
        End Select
        alsOpen1.AddExtension = False
        alsOpen1.Multiselect = False

        If alsOpen1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            ofd_FileName = alsOpen1.FileName
            BGW_ablproj.RunWorkerAsync()
        End If
    End Sub

    Private Sub ConvertALSToUnipackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertALSToUnipackToolStripMenuItem.Click

        Dim sfd As New SaveFileDialog()
        Select Case lang
            Case Translator.tL.English
                sfd.Filter = "Zip File|*.zip|UniPack File|*.uni"
                sfd.Title = "Select the UniPack File"
            Case Translator.tL.Korean
                sfd.Filter = "Zip 파일|*.zip|유니팩 파일|*.uni"
                sfd.Title = "유니팩 파일을 어디다 저장할지 선택 하세요"
        End Select
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
                    Select Case lang
                        Case Translator.tL.English
                            MessageBox.Show("Saved info!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Case Translator.tL.Korean
                            MessageBox.Show("info를 저장 했습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Select
                End If
            Else
                Select Case lang
                    Case Translator.tL.English
                        MessageBox.Show("You didn't open Ableton Project!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Case Translator.tL.Korean
                        MessageBox.Show("에이블톤 프로젝트를 열지 않았습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Select
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

                UI(Sub()
                       With Loading
                           .Show()
                           Select Case lang
                               Case Translator.tL.English
                                   .Text = Loading.MsgEn.loading_keySound_def_msg
                                   .DLb.Text = Loading.MsgEn.loading_keySound_open_msg
                               Case Translator.tL.Korean
                                   .Text = Loading.MsgKr.loading_keySound_def_msg
                                   .DLb.Text = Loading.MsgKr.loading_keySound_open_msg
                           End Select
                           .DLb.Left -= 20
                           .DPr.Style = ProgressBarStyle.Marquee
                           .DPr.MarqueeAnimationSpeed = 10
                       End With
                   End Sub)

                If Directory.Exists(Application.StartupPath & "\Workspace\unipack\sounds") Then
                    UI(Sub()
                           Select Case lang
                               Case Translator.tL.English
                                   Loading.DLb.Text = Loading.MsgEn.loading_Project_DeleteTmp_msg
                               Case Translator.tL.Korean
                                   Loading.DLb.Text = Loading.MsgKr.loading_Project_DeleteTmp_msg
                           End Select
                       End Sub)

                    My.Computer.FileSystem.DeleteDirectory(Application.StartupPath & "\Workspace\unipack\sounds", FileIO.DeleteDirectoryOption.DeleteAllContents)
                    Thread.Sleep(300)
                    Directory.CreateDirectory(Application.StartupPath & "\Workspace\unipack\sounds")
                Else
                    Directory.CreateDirectory(Application.StartupPath & "\Workspace\unipack\sounds")
                End If

                UI(Sub()
                       Select Case lang
                           Case Translator.tL.English
                               Loading.DLb.Text = Loading.MsgEn.loading_keySound_open_msg
                           Case Translator.tL.Korean
                               Loading.DLb.Text = Loading.MsgKr.loading_keySound_open_msg
                       End Select
                   End Sub)

                'InstrumentGroupDevice
                'ChainSelector
                Dim ablprj As String = Application.StartupPath & "\Workspace\ableproj\abl_proj.xml"
                Dim doc As New XmlDocument
                Dim setNode As XmlNodeList
                Dim setaNode As XmlNodeList

                doc.Load(ablprj)
                setNode = doc.GetElementsByTagName("InstrumentBranch")
                setaNode = doc.GetElementsByTagName("DrumBranch")

                'Get Sound Name from Drum Rack.
                Dim rNote As Integer = 0 'Receiving Note.
                Dim nx As Integer = 0 'setNode (InstrumentBranch)와 setaNode (DrumBranch)를 동기화 시켜주는 i.

                Dim PrChain As Integer = 0 '랜덤의 체인.
                Dim PrChainM As Integer = 0 '랜덤의 최대 체인.
                Dim IsRandom As Boolean = False '현재 접근하고 있는 XML Branch가 랜덤인가?
                Dim rnd As Integer = 1 '랜덤의 수
                Dim Choices As Integer = 0 '매우 정확한 랜덤의 수. (from MidiRandom)
                Dim curid As Integer = 1 '현재의 랜덤 Xml.
                Dim realCh As Integer = 0 '랜덤을 선언할 때 정말 정확한 수.

                Dim str As String = String.Empty 'keySound 그 자체.
                Dim err As String = String.Empty 'keySound 변환 할 때의 오류를 저장하는 곳.
                For Each x As XmlNode In setNode

#Region "keySound / IsRandom"
                    Try
                        Dim Try4sndName As String = x.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("OriginalSimpler").Item("Player").Item("MultiSampleMap").Item("SampleParts").Item("MultiSamplePart").Item("SampleRef").Item("FileRef").Item("Name").GetAttribute("Value")
                        If curid = 1 Then
                            rNote = Integer.Parse(setaNode(nx).Item("BranchInfo").Item("ReceivingNote").GetAttribute("Value"))

                            Dim branches As Integer = Cntstr(setaNode(nx).InnerXml, "</MidiRandom>")
                            If branches > 0 Then
                                Debug.WriteLine("brn: " & branches)
                                rnd = Cntstr(setaNode(nx).InnerXml, "</InstrumentBranch>") - branches
                            Else
                                rnd = Cntstr(setaNode(nx).InnerXml, "</InstrumentBranch>")
                            End If

                            Debug.WriteLine(rnd) '디버깅 전용.
                            nx += 1
                        End If

                    Catch exN As NullReferenceException
                        If setaNode.Count < nx + 1 Then '심각한데?
                            If IsGreatExMode Then
                                Throw New NullReferenceException("DrumRack < nx")
                            Else
                                err &= vbNewLine & String.Format("NullReferenceException: Drum Rack's Length </>")
                                Exit For
                            End If
                        End If

                        PrChain = Integer.Parse(x.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1 '최소 체인.
                        PrChainM = Integer.Parse(x.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1 '최대 체인.

                        Try
                            Choices = Integer.Parse(x.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("MidiRandom").Item("Choices").Item("Manual").GetAttribute("Value")) 'MidiRandom > Choices > Manual Value
                        Catch exNN As NullReferenceException
                            Choices = 0
                        End Try

                        If Choices > 0 Then '랜덤인 경우.
                            IsRandom = True
                            realCh = curid - 1 + Choices
                        Else
                            Choices = 0
                            IsRandom = False
                        End If

                        Continue For
                    End Try
#End Region

                    Dim sndName As String = x.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("OriginalSimpler").Item("Player").Item("MultiSampleMap").Item("SampleParts").Item("MultiSamplePart").Item("SampleRef").Item("FileRef").Item("Name").GetAttribute("Value")
                    Dim Chain As Integer = Integer.Parse(x.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1

#Region "Error Lists"
                    If Not File.Exists(Application.StartupPath & "\Workspace\ableproj\sounds\" & sndName) Then
                        Debug.WriteLine(String.Format("'{0}' File doesn't exists.", sndName))
                        err &= vbNewLine & String.Format("'{0}' File doesn't exists.", sndName)

                        If Not rnd = curid Then
                            curid += 1 'id를 + 1 안해주면 key가 하나씩 계속 밀리게 됨.
                        Else
                            curid = 1
                        End If

                        Continue For
                    End If
#End Region
#Region "IsRandom Codes"
                    Dim lpn As Boolean = False
                    Dim MaxChain As Integer = Integer.Parse(x.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1
                    If Not Chain = MaxChain Then
                        lpn = True
                    Else
                        lpn = False
                    End If

                    '랜덤이니깐 체인 동기화를 해줌.
                    If Not PrChain = 0 AndAlso IsRandom Then

                        Chain = PrChain
                        MaxChain = PrChainM
                        If Not Chain = MaxChain Then
                            lpn = True
                        Else
                            lpn = False
                        End If

                    Else
                        PrChain = 0
                    End If
#End Region

                    Dim ks As ksX = GetkeySound(ks_NoteEvents.NoteNumber_1, rNote)
#Region "keySound Debugging"
                    If lpn = False Then
                        Debug.WriteLine(String.Format("{0}: {1} {2} {3}", sndName, Chain, ks.x, ks.y))
                    Else
                        Debug.WriteLine(String.Format("{0}: {1} ~ {2} {3} {4}", sndName, Chain, MaxChain, ks.x, ks.y))
                    End If
#End Region

                    File.Copy(Application.StartupPath & "\Workspace\ableproj\sounds\" & sndName, Application.StartupPath & "\Workspace\unipack\sounds\" & sndName, True)
                    If String.IsNullOrWhiteSpace(str) Then
                        If lpn = False Then
                            str &= String.Format("{0} {1} {2} {3}", Chain, ks.x, ks.y, sndName)
                        Else
#Region "체인 ~ 최대 체인 변환"
                            For Ci As Integer = Chain To MaxChain
                                str &= String.Format("{0} {1} {2} {3}", Ci, ks.x, ks.y, sndName)
                            Next
#End Region
                        End If
                    Else
                        If lpn = False Then
                            str &= vbNewLine & String.Format("{0} {1} {2} {3}", Chain, ks.x, ks.y, sndName)
                        Else
#Region "체인 ~ 최대 체인 변환"
                            For Ci As Integer = Chain To MaxChain
                                str &= vbNewLine & String.Format("{0} {1} {2} {3}", Ci, ks.x, ks.y, sndName)
                            Next
#End Region
                        End If
                    End If

                    If IsRandom AndAlso curid = realCh Then
                        IsRandom = False
                        Choices = 0
                    End If

                    If rnd = curid Then
                        IsRandom = False
                        rnd = 1
                        curid = 0
                    End If

                    curid += 1
                Next
                File.WriteAllText(Application.StartupPath & "\Workspace\unipack\keySound", str)
                UI(Sub()
                       ShowkeySoundLayout()
                   End Sub)

                IsWorking = False
                ks_Converted = True
                UI(Sub() Loading.Dispose())

                If OpenProjectOnce Then
                    BGW_keyLED_.RunWorkerAsync()
                    Exit Sub
                End If

                Select Case lang
                    Case Translator.tL.English
                        MessageBox.Show("keySound Converted!" & vbNewLine & "You can show the keySound on 'keySound' Tab!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Case Translator.tL.Korean
                        MessageBox.Show("keySound를 변환 했습니다!" & vbNewLine & "'keySound' 탭에서 keySound를 보실 수가 있습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Select

                If Not String.IsNullOrWhiteSpace(err) Then
                    MessageBox.Show("[ Warning ]" & vbNewLine & "keySound: [] format is invaild." & vbNewLine & err, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub BGW_soundcut_DoWork(sender As Object, e As DoWorkEventArgs) Handles BGW_soundcut.DoWork
        Try
            If e.Cancel = False AndAlso IsWorking = False AndAlso abl_openedproj AndAlso abl_openedsnd Then
                IsWorking = True

                UI(Sub()
                       With Loading
                           .Show()
                           Select Case lang
                               Case Translator.tL.English
                                   .Text = Loading.MsgEn.loading_soundcut_def_msg
                                   .DLb.Text = Loading.MsgEn.loading_soundcut_open_msg
                               Case Translator.tL.Korean
                                   .Text = Loading.MsgKr.loading_soundcut_def_msg
                                   .DLb.Text = Loading.MsgKr.loading_soundcut_open_msg
                           End Select
                           .DLb.Left -= 20
                           .DPr.Style = ProgressBarStyle.Marquee
                           .DPr.MarqueeAnimationSpeed = 10
                       End With
                   End Sub)

                UI(Sub()
                       Select Case lang
                           Case Translator.tL.English
                               Loading.DLb.Text = Loading.MsgEn.loading_Project_DeleteTmp_msg
                           Case Translator.tL.Korean
                               Loading.DLb.Text = Loading.MsgKr.loading_Project_DeleteTmp_msg
                       End Select
                   End Sub)

                If Directory.Exists(Application.StartupPath & "\Workspace\unipack\sounds") Then
                    Directory.Delete(Application.StartupPath & "\Workspace\unipack\sounds", True)
                    Thread.Sleep(300)
                    Directory.CreateDirectory(Application.StartupPath & "\Workspace\unipack\sounds")
                Else
                    Directory.CreateDirectory(Application.StartupPath & "\Workspace\unipack\sounds")
                End If

                If Directory.Exists(Application.StartupPath & "\Workspace\TmpSound") Then
                    Directory.Delete(Application.StartupPath & "\Workspace\TmpSound", True)
                    Thread.Sleep(300)
                    Directory.CreateDirectory(Application.StartupPath & "\Workspace\TmpSound")
                Else
                    Directory.CreateDirectory(Application.StartupPath & "\Workspace\TmpSound")
                End If

                UI(Sub()
                       Select Case lang
                           Case Translator.tL.English
                               Loading.DLb.Text = Loading.MsgEn.loading_soundcut_open_msg
                           Case Translator.tL.Korean
                               Loading.DLb.Text = Loading.MsgKr.loading_soundcut_open_msg
                       End Select
                   End Sub)

                Dim ablprj As String = Application.StartupPath & "\Workspace\ableproj\abl_proj.xml"
                Dim doc As New XmlDocument
                Dim setNode As XmlNodeList

                doc.Load(ablprj)
                setNode = doc.GetElementsByTagName("InstrumentBranch")

                UI(Sub()
                       Loading.DPr.Style = ProgressBarStyle.Continuous
                       Loading.DPr.Value = 0
                       Loading.DPr.Maximum = setNode.Count
                       Select Case lang
                           Case Translator.tL.English
                               Loading.DLb.Text = String.Format(Loading.MsgEn.loading_soundcut_convert1_msg, 0, Loading.DPr.Maximum)
                           Case Translator.tL.Korean
                               Loading.DLb.Text = String.Format(Loading.MsgKr.loading_soundcut_convert1_msg, 0, Loading.DPr.Maximum)
                       End Select
                       Loading.DLb.Left -= 30
                   End Sub)

                '에이블톤 sounds Crop 길이는 InstrumentBranch > DeviceChain > MidiToAudioDeviceChain > 
                'Devices > OriginalSimpler > Player > MultiSampleMap > SampleParts > MultiSamplePart > 
                'SampleEnd Value - SampleStart Value에 있습니다.

                Dim il As Integer = 1 '로딩 폼 value.
                Dim trName As String = "1" 'Trim할 때 쓰는 이름.
                For Each x As XmlNode In setNode

                    Try
                        Dim t_DeviceChain As String = x.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("OriginalSimpler").Item("Player").Item("MultiSampleMap").Item("SampleParts").Item("MultiSamplePart").Item("SampleRef").Item("FileRef").Item("Name").GetAttribute("Value")
                    Catch exN As NullReferenceException
                        il += 1
                        Continue For 'Random 이거나 Page > Chain 인거임.
                    End Try

                    Dim sndName As String = x.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("OriginalSimpler").Item("Player").Item("MultiSampleMap").Item("SampleParts").Item("MultiSamplePart").Item("SampleRef").Item("FileRef").Item("Name").GetAttribute("Value")
                    Dim ssTime As Long = x.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("OriginalSimpler").Item("Player").Item("MultiSampleMap").Item("SampleParts").Item("MultiSamplePart").Item("SampleStart").GetAttribute("Value")
                    Dim seTime As Long = x.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("OriginalSimpler").Item("Player").Item("MultiSampleMap").Item("SampleParts").Item("MultiSamplePart").Item("SampleEnd").GetAttribute("Value")

                    Dim StartTime As TimeSpan = sLToTime(ssTime)
                    Dim EndTime As TimeSpan = sLToTime(seTime)

                    If sndName.Contains(".mp3") Then
                        sndName = sndName.Replace(".mp3", ".wav") '이미 파일을 불러왔을 때 변환이 되었으니 replace.
                    End If

                    If New WaveFileReader(Application.StartupPath & "\Workspace\ableproj\sounds\" & sndName).TotalTime.TotalMilliseconds - 30 <= EndTime.TotalMilliseconds AndAlso StartTime.TotalMilliseconds <= 30 Then
                        Continue For '오차 ±30ms 보정 후 넘겨!
                    End If

                    Sound_Cutting.TrimWavFile(Application.StartupPath & "\Workspace\ableproj\sounds\" & sndName, Application.StartupPath & "\Workspace\TmpSound\" & trName & ".wav", StartTime, EndTime)
                    Debug.WriteLine(sndName & " : " & trName & ".wav, " & StartTime.TotalMilliseconds & " - " & EndTime.TotalMilliseconds)
                    UI(Sub()
                           Select Case lang
                               Case Translator.tL.English
                                   Loading.DLb.Text = String.Format(Loading.MsgEn.loading_soundcut_convert1_msg, il, Loading.DPr.Maximum)
                                   Loading.DPr.Value = il
                               Case Translator.tL.Korean
                                   Loading.DLb.Text = String.Format(Loading.MsgKr.loading_soundcut_convert1_msg, il, Loading.DPr.Maximum)
                                   Loading.DPr.Value = il
                           End Select
                       End Sub)
                    il += 1
                    trName = Integer.Parse(trName) + 1
                Next

                UI(Sub()
                       il = 1
                       Loading.DPr.Value = 0
                       Loading.DPr.Maximum = Directory.GetFiles(Application.StartupPath & "\Workspace\TmpSound", "*.wav").Count
                       Select Case lang
                           Case Translator.tL.English
                               Loading.DLb.Text = String.Format(Loading.MsgEn.loading_soundcut_convert2_msg, 0, Loading.DPr.Maximum)
                           Case Translator.tL.Korean
                               Loading.DLb.Text = String.Format(Loading.MsgKr.loading_soundcut_convert2_msg, 0, Loading.DPr.Maximum)
                       End Select
                       Loading.DLb.Left -= 40
                   End Sub)

                For Each itm As String In Directory.GetFiles(Application.StartupPath & "\Workspace\TmpSound", "*.wav")
                    File.Move(itm, Application.StartupPath & "\Workspace\ableproj\sounds\" & Path.GetFileName(itm))
                    UI(Sub()
                           Select Case lang
                               Case Translator.tL.English
                                   Loading.DLb.Text = String.Format(Loading.MsgEn.loading_soundcut_convert2_msg, il, Loading.DPr.Maximum)
                                   Loading.DPr.Value = il
                               Case Translator.tL.Korean
                                   Loading.DLb.Text = String.Format(Loading.MsgKr.loading_soundcut_convert2_msg, il, Loading.DPr.Maximum)
                                   Loading.DPr.Value = il
                           End Select
                       End Sub)
                    il += 1
                Next

                UI(Sub()
                       Loading.Dispose()
                   End Sub)
            End If
        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Public Function SetFileName(ByVal FilePath As String, ByVal name As String) As String
        Dim rName As String
        Dim filename As String = FilePath.Split(Path.DirectorySeparatorChar).Last()

        rName = FilePath.Replace(filename, name)
        Return rName
    End Function

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
            If Not ks_lpn = Cntstr(ksTmpTXT, keySound_CChain & " " & x & " " & y & " ") Then
                ks_lpu = 1
            End If
            ks_lpn = Cntstr(ksTmpTXT, keySound_CChain & " " & x & " " & y & " ")

            If ks_lpn = ks_lpu - 1 Then
                ks_lpu = 1
            End If

            For Each strLine As String In SplitbyLine(ksTmpTXT)
                If strLine.Contains(keySound_CChain & " " & x & " " & y & " ") Then

                    If Not ks_lpu = 1 Then
                        For i As Integer = 1 To ks_lpu - 1
                            Continue For
                        Next
                    End If

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

                    ks_lpu += 1
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

    Private Sub CutSndButton_Click(sender As Object, e As EventArgs)
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
        FileNames = ofd_FileNames

        UI(Sub()
               Loading.Show()
               Loading.DPr.Maximum = FileNames.Length
               Loading.DLb.Left = 40
               Select Case lang
                   Case Translator.tL.English
                       Loading.Text = Me.Text & ": " & Loading.MsgEn.loading_Sound_Open_msg
                       Loading.DLb.Text = Loading.MsgEn.loading_Sound_Open_msg
                   Case Translator.tL.Korean
                       Loading.Text = Me.Text & ": " & Loading.MsgKr.loading_Sound_Open_msg
                       Loading.DLb.Text = Loading.MsgKr.loading_Sound_Open_msg
               End Select
           End Sub)

        If Path.GetExtension(FileNames(FileNames.Length - 1)) = ".wav" Then

            If My.Computer.FileSystem.DirectoryExists("Workspace\ableproj\sounds") = True Then
                My.Computer.FileSystem.DeleteDirectory("Workspace\ableproj\sounds", FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If

            My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\sounds")


            For i = 0 To FileNames.Length - 1
                File.Copy(FileNames(i), "Workspace\ableproj\sounds\" & FileNames(i).Split("\").Last, True)
                UI(Sub()
                       Loading.DPr.Style = ProgressBarStyle.Continuous
                       Loading.DPr.Value += 1
                       Loading.DLb.Left = 40
                       Select Case lang
                           Case Translator.tL.English
                               Loading.DLb.Text = String.Format(Loading.MsgEn.loading_Sound_Open_msg, Loading.DPr.Value, FileNames.Length)
                           Case Translator.tL.Korean
                               Loading.DLb.Text = String.Format(Loading.MsgKr.loading_Sound_Open_msg, Loading.DPr.Value, FileNames.Length)
                       End Select
                   End Sub)
            Next

        ElseIf Path.GetExtension(FileNames(FileNames.Length - 1)) = ".mp3" Then

            If My.Computer.FileSystem.DirectoryExists("Workspace\ableproj\sounds") = True Then
                My.Computer.FileSystem.DeleteDirectory("Workspace\ableproj\sounds", FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
            My.Computer.FileSystem.CreateDirectory("Workspace\ableproj\sounds")

            For i = 0 To FileNames.Length - 1
                File.Copy(FileNames(i), "Workspace\TmpSound\" & FileNames(i).Split("\").Last.Replace(" ", "").Trim(), True)
            Next

            Try
                For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\", FileIO.SearchOption.SearchTopLevelOnly, "*.mp3")
                    Dim wavFile As String = foundFile.Replace(".mp3", ".wav")

                    Sound_Cutting.Mp3ToWav(foundFile, wavFile)
                    Thread.Sleep(300)

                    If File.Exists(wavFile) Then

                        File.Move(wavFile, "Workspace\ableproj\sounds\" & Path.GetFileName(wavFile))
                        Thread.Sleep(300)
                        File.Delete(foundFile)

                        UI(Sub()
                               Loading.DPr.Style = ProgressBarStyle.Continuous
                               Loading.DPr.Value += 1
                               Loading.DLb.Left = 40

                               Select Case lang
                                   Case Translator.tL.English
                                       Loading.DLb.Text = String.Format(Loading.MsgEn.loading_Sound_Open_msg, Loading.DPr.Value, ofd.FileNames.Length)
                                   Case Translator.tL.Korean
                                       Loading.DLb.Text = String.Format(Loading.MsgKr.loading_Sound_Open_msg, Loading.DPr.Value, ofd.FileNames.Length)
                               End Select
                           End Sub)

                    End If
                Next
            Catch fex As IOException 'I/O 오류 해결 코드.
            End Try
        End If

        '-After Loading WAV/MP3!
        UI(Sub()
               Loading.DPr.Value = Loading.DPr.Maximum
               If Loading.DPr.Value = FileNames.Length Then
                   If FileNames.Length = Directory.GetFiles(Application.StartupPath & "\Workspace\ableproj\sounds\", "*.wav").Length Then
                       UI(Sub()
                              Loading.DPr.Style = ProgressBarStyle.Marquee
                              Loading.DLb.Left = 40
                              Select Case lang
                                  Case Translator.tL.English
                                      Loading.DLb.Text = Loading.MsgEn.loading_Sound_Loaded_msg
                                  Case Translator.tL.Korean
                                      Loading.DLb.Text = Loading.MsgKr.loading_Sound_Loaded_msg
                              End Select

                              Loading.Dispose()
                          End Sub)
                       If OpenProjectOnce = False Then
                           Select Case lang
                               Case Translator.tL.English
                                   MessageBox.Show("Sounds Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                               Case Translator.tL.Korean
                                   MessageBox.Show("사운드 파일들이 로딩되었습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                           End Select
                       End If
                       abl_openedsnd = True
                       SoundIsSaved = True

                   Else
                       MessageBox.Show("Error! - Code: MaxFileLength.Value = GetFiles.Length", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                   End If
               Else
                   MessageBox.Show("Error! - Code: LoadedFiles.Value = MaxFileLength.Value", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
               End If
           End Sub)
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
                    BGW_soundcut.RunWorkerAsync()
                    'BGW_keySound.RunWorkerAsync()
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

    Private Sub CheckUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckUpdateToolStripMenuItem.Click
        If My.Computer.Network.IsAvailable = False Then
            Select Case lang
                Case Translator.tL.English
                    MessageBox.Show("Network Connect Failed! Can't Check Update.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Case Translator.tL.Korean
                    MessageBox.Show("네트워크를 연결할 수 없습니다! 업데이트를 확인할 수 없습니다.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Select
            Exit Sub
        End If

        Try
            If My.Application.Info.Version = FileInfo Then
                Select Case lang
                    Case Translator.tL.English
                        MessageBox.Show("You are using a Latest Version." & vbNewLine & vbNewLine &
              "Current Version : " & My.Application.Info.Version.ToString & vbNewLine & "Latest Version : " & FileInfo.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Case Translator.tL.Korean
                        MessageBox.Show("최신 버전을 사용하고 있습니다." & vbNewLine & vbNewLine &
              "현재 버전 : " & My.Application.Info.Version.ToString & vbNewLine & "최신 버전 : " & FileInfo.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Select
            ElseIf My.Application.Info.Version > FileInfo Then
                Select Case lang
                    Case Translator.tL.English
                        MessageBox.Show("You are using a Test Version!" & vbNewLine & vbNewLine & "Current Version : " & FileInfo.ToString & vbNewLine &
               "Your Test Version : " & My.Application.Info.Version.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Case Translator.tL.Korean
                        MessageBox.Show("테스트 버전을 사용하고 있습니다!" & vbNewLine & vbNewLine & "현재 버전 : " & FileInfo.ToString & vbNewLine &
               "테스트 버전 : " & My.Application.Info.Version.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Select
            End If

        Catch ex As ArgumentNullException
            updateShow = True
            If BGW_CheckUpdate.IsBusy = False Then
                BGW_CheckUpdate.RunWorkerAsync()
            End If
        End Try
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
                Dim result As String = String.Empty
                Dim result2 As String = String.Empty
                Select Case lang
                    Case Translator.tL.English
                        result = "New Version " & FileInfo.ToString & " is Available!" & vbNewLine & "Current Version : " & My.Application.Info.Version.ToString & vbNewLine & "Latest Version : " & FileInfo.ToString & vbNewLine &
                                 vbNewLine & "Update Log:" & vbNewLine & VerLog
                        result2 = Me.Text & ": Update"
                    Case Translator.tL.Korean
                        result = FileInfo.ToString & " 버전을 사용할 수 있습니다!" & vbNewLine & "현재 버전 : " & My.Application.Info.Version.ToString & vbNewLine & "최신 버전 : " & FileInfo.ToString & vbNewLine &
                                 vbNewLine & "업데이트 사항:" & vbNewLine & VerLog
                        result2 = Me.Text & ": 업데이트"
                End Select

                If MessageBox.Show(result, result2, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    UI(Sub()
                           With Loading
                               .Show()
                               Select Case lang
                                   Case Translator.tL.English
                                       .Text = "Downloading UniConverter v" & FileInfo.ToString
                                       .DLb.Text = "Downloading UniConverter v" & FileInfo.ToString & " ..."
                                   Case Translator.tL.Korean
                                       .Text = "유니컨버터 v" & FileInfo.ToString
                                       .DLb.Text = "유니컨버터 v" & FileInfo.ToString & " 다운로드 중..."
                               End Select
                               .DLb.Left = 20
                           End With
                       End Sub)
                    Client.DownloadFile("http://dpr.ucv.kro.kr", My.Computer.FileSystem.SpecialDirectories.Temp & "\UniConverter-Update.zip")

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
                        UI(Sub()
                               .DLb.Left = 120
                               Select Case lang
                                   Case Translator.tL.English
                                       .DLb.Text = "Update Complete!"
                                   Case Translator.tL.Korean
                                       .DLb.Text = "업데이트를 완료 하였습니다!"
                               End Select
                               .Dispose()
                           End Sub)
                        IsUpdated = True
                        Dim result3 As String = String.Empty

                        Select Case lang
                            Case Translator.tL.English
                                result3 = "Update Complete! UniConverter " & FileInfo.ToString & " is in 'UniConverter_v" & FileInfo.ToString & "' Folder."
                            Case Translator.tL.Korean
                                result3 = "업데이트를 완료 하였습니다! 유니컨버터 v" & FileInfo.ToString & " 버전은 'UniConverter_v" & FileInfo.ToString & "' 폴더에 있습니다."
                        End Select
                        If MessageBox.Show(result3, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) = DialogResult.OK Then
                            File.Delete(My.Computer.FileSystem.SpecialDirectories.Temp & "\UniConverter-Update.zip")
                            Dim setNode As XmlNode = setxml.SelectSingleNode("/UniConverter-XML/UniConverter-Settings")
                            If Convert.ToBoolean(setNode.ChildNodes(1).InnerText) = True Then
                                Process.Start(String.Format("{0}\UniConverter_v{1}\UniConverter.exe", Application.StartupPath, FileInfo.ToString))
                                Application.Exit()
                            End If
                        End If
                    End With
                End If
            End If

            If updateShow Then
                If My.Application.Info.Version = FileInfo Then
                    Select Case lang
                        Case Translator.tL.English
                            MessageBox.Show("You are using a Latest Version." & vbNewLine & vbNewLine &
              "Current Version : " & My.Application.Info.Version.ToString & vbNewLine & "Latest Version : " & FileInfo.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Case Translator.tL.Korean
                            MessageBox.Show("최신 버전을 사용하고 있습니다." & vbNewLine & vbNewLine &
              "현재 버전 : " & My.Application.Info.Version.ToString & vbNewLine & "최신 버전 : " & FileInfo.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Select
                ElseIf My.Application.Info.Version > FileInfo Then
                    Select Case lang
                        Case Translator.tL.English
                            MessageBox.Show("You are using a Test Version!" & vbNewLine & vbNewLine & "Current Version : " & FileInfo.ToString & vbNewLine &
               "Your Test Version : " & My.Application.Info.Version.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Case Translator.tL.Korean
                            MessageBox.Show("테스트 버전을 사용하고 있습니다!" & vbNewLine & vbNewLine & "현재 버전 : " & FileInfo.ToString & vbNewLine &
               "테스트 버전 : " & My.Application.Info.Version.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Select
                End If
            End If
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
            Dim result As DialogResult
            Select Case lang
                Case Translator.tL.English
                    result = MessageBox.Show("You didn't save your UniPack. Would you like to save your UniPack?", Me.Text & ": Not Saved", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                Case Translator.tL.Korean
                    result = MessageBox.Show("유니팩을 저장하지 않으셨습니다. 유니팩을 저장 하시겠습니까?", Me.Text & ": 저장하지 않음", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            End Select

            If result = DialogResult.Yes Then
                ThreadPool.QueueUserWorkItem(AddressOf Save2Project, False)
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
            Dim IsHaveProject As Boolean = False
            Dim IsHaveSound As Boolean = False
            Dim IsHaveLED As Boolean = False
            UI(Sub()
                   IsHaveProject = abl_openedproj
                   IsHaveSound = abl_openedsnd
                   IsHaveLED = abl_openedled
               End Sub)

            If IsHaveProject AndAlso IsHaveSound Or IsHaveProject AndAlso IsHaveLED Then
                Dim infoTitle As String = String.Empty
                infoTitle = File.ReadAllLines(Application.StartupPath & "\Workspace\unipack\info")(0).Replace("title=", "")

                Dim sfd As New SaveFileDialog()
                Dim aN As DialogResult

                UI(Sub()
                       Select Case lang
                           Case Translator.tL.English
                               sfd.Filter = "Zip File|*.zip|UniPack File|*.uni"
                               sfd.Title = "Save the UniPack"
                           Case Translator.tL.Korean
                               sfd.Filter = "Zip 파일|*.zip|유니팩 파일|*.uni"
                               sfd.Title = "유니팩을 어디에 저장할지 선택하세요"
                       End Select
                   End Sub)
                sfd.FileName = infoTitle
                sfd.AddExtension = False
                UI(Sub()
                       aN = sfd.ShowDialog()
                   End Sub)

                If aN = DialogResult.OK Then
                    If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\Workspace\unipack") Then
                        If Waiting = True Then

                            UI(Sub()
                                   With Loading
                                       .Show()
                                       .DPr.Style = ProgressBarStyle.Marquee
                                       .DPr.MarqueeAnimationSpeed = 10
                                       Select Case lang
                                           Case Translator.tL.English
                                               .Text = Me.Text & ": Saving Converted UniPack..."
                                           Case Translator.tL.Korean
                                               .Text = Me.Text & ": 변환한 유니팩 저장 중..."
                                       End Select
                                       .DLb.Left = 45
                                   End With
                               End Sub)

                            Dim result As String = Path.GetExtension(sfd.FileName)
                            If result = ".zip" Then
                                UI(Sub()
                                       Select Case lang
                                           Case Translator.tL.English
                                               Loading.DLb.Text = "Creating UniPack to zip File..."
                                           Case Translator.tL.Korean
                                               Loading.DLb.Text = "유니팩을 zip 파일로 저장 중..."
                                       End Select
                                   End Sub)
                            ElseIf result = ".uni" Then
                                UI(Sub()
                                       Select Case lang
                                           Case Translator.tL.English
                                               Loading.DLb.Text = "Creating UniPack to uni File..."
                                           Case Translator.tL.Korean
                                               Loading.DLb.Text = "유니팩을 uni 파일로 저장 중..."
                                       End Select
                                   End Sub)
                            End If

                        End If
                        If File.Exists(sfd.FileName) Then
                            File.Delete(sfd.FileName)
                            Thread.Sleep(300)
                        End If

                        ZipFile.CreateFromDirectory(Application.StartupPath & "\Workspace\unipack", sfd.FileName)
                        UI(Sub() Loading.Dispose())
                        If Waiting = True Then
                            IsSaved = True
                            UI(Sub()
                                   Select Case lang
                                       Case Translator.tL.English
                                           MessageBox.Show("Saved UniPack!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                       Case Translator.tL.Korean
                                           MessageBox.Show("유니팩을 저장 했습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                   End Select
                               End Sub)
                        End If
                    End If
                End If
            Else
                UI(Sub()
                       Select Case lang
                           Case Translator.tL.English
                               MessageBox.Show("Please convert the Ableton Project to UniPack first!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                           Case Translator.tL.Korean
                               MessageBox.Show("먼저 에이블톤 프로젝트에서 유니팩으로 변환 해주세요!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                       End Select
                   End Sub)
            End If

        Catch ex As Exception
            UI(Sub()
                   If IsGreatExMode Then
                       MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                   Else
                       MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                   End If
               End Sub)
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

    Private Sub keyLEDBetaButton_Click(sender As Object, e As EventArgs)
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
            Select Case lang
                Case Translator.tL.English
                    ofd.Title = "Select LED Files"
                    ofd.Filter = "LED Files|*.mid"
                Case Translator.tL.Korean
                    ofd.Title = "LED 파일을 선택하세요"
                    ofd.Filter = "LED 파일|*.mid"
            End Select
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
                Select Case lang
                    Case Translator.tL.English
                        MessageBox.Show("Wrong input Launchpad! Please select other thing!" & vbNewLine & String.Format("(Selected MIDI Device: {0})", wowk), "Wrong Launchpad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        MIDIStatIn.Text = "MIDI Input: Not Connected"
                    Case Translator.tL.Korean
                        MessageBox.Show("미디 입력에 런치패드를 연결할 수 없습니다! 다른걸 선택해주세요." & vbNewLine & String.Format("(선택한 미디 장치: {0})", wowk), "잘못된 장치", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        MIDIStatIn.Text = "미디 입력: 연결 안됨"
                End Select
                midioutput_kind = 0
            End If

            Select Case lang
                Case Translator.tL.English
                    MIDIStatIn.Text = String.Format("MIDI Input: Connected ({0})", MidiIn.DeviceInfo(InListBox.SelectedIndex).ProductName)
                Case Translator.tL.Korean
                    MIDIStatIn.Text = String.Format("미디 입력: 연결됨 ({0})", MidiIn.DeviceInfo(InListBox.SelectedIndex).ProductName)
            End Select

        Catch ex As Exception
            Select Case lang
                Case Translator.tL.English
                    MessageBox.Show("Failed to connect input device. Please try again or restart UniConverter." & vbNewLine & "Also, You can report this in 'Report Tab'.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    MIDIStatIn.Text = "MIDI Input: Not Connected"
                Case Translator.tL.Korean
                    MessageBox.Show("미디 입력을 장치에 연결할 수 없습니다. 다시 시도 하시거나 유니컨버터를 재시작 해주시기 바랍니다." & vbNewLine & "또한, '버그 제보' 탭에서 버그 제보 하실 수 있습니다.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    MIDIStatIn.Text = "미디 입력: 연결 안됨"
            End Select
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
                Select Case lang
                    Case Translator.tL.English
                        MessageBox.Show("Wrong output Launchpad! Please select other thing." & vbNewLine & String.Format("(Selected MIDI Device: {0})", wowc), "Wrong Launchpad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Case Translator.tL.Korean
                        MessageBox.Show("미디 출력에 런치패드를 연결할 수 없습니다! 다른걸 선택해주세요." & vbNewLine & String.Format("(선택한 미디 장치: {0})", wowc), "잘못된 장치", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Select
                midioutput_kind = 0
            End If

            Select Case lang
                Case Translator.tL.English
                    MIDIStatOut.Text = String.Format("Midi Output: Connected ({0})", MidiOut.DeviceInfo(OutListBox.SelectedIndex).ProductName)
                Case Translator.tL.Korean
                    MIDIStatOut.Text = String.Format("미디 출력: 연결됨 ({0})", MidiOut.DeviceInfo(OutListBox.SelectedIndex).ProductName)
            End Select
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
            Select Case lang
                Case Translator.tL.English
                    MIDIStatOut.Text = "MIDI Output: Not Connected"
                    MessageBox.Show("Failed to connect output device. Please try again or restart UniConverter." & vbNewLine & "Also, You can report this in 'Report Tab'.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Case Translator.tL.Korean
                    MIDIStatOut.Text = "미디 출력: 연결 안됨"
                    MessageBox.Show("미디 출력을 장치에 연결할 수 없습니다. 다시 시도 하시거나 유니컨버터를 재시작 해주시기 바랍니다." & vbNewLine & "또한, '버그 제보' 탭에서 버그 제보 하실 수 있습니다.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Select

            If IsGreatExMode Then
                MessageBox.Show("Error: " & ex.Message & vbNewLine & "Exception StackTrace: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            midioutput_kind = False
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

        Select Case lang
            Case Translator.tL.English
                MIDIStatIn.Text = "MIDI Input: Not Connected"
                MIDIStatOut.Text = "MIDI Output: Not Connected"
            Case Translator.tL.Korean
                MIDIStatIn.Text = "미디 입력: 연결 안됨"
                MIDIStatOut.Text = "미디 출력: 연결 안됨"
        End Select
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

    Private Sub keyLEDMIDEX_TestButton_Click(sender As Object, e As EventArgs)
        keyLED_Test.Show()
        'keyLED_Test.LoadkeyLEDText(keyLEDMIDEX_UniLED.Text)
    End Sub

    '에이블톤 Instrument Rack을 keyLED로 바꿔주는 코드.
    Private Sub BGW_keyLED__DoWork(sender As Object, e As DoWorkEventArgs) Handles BGW_keyLED_.DoWork
        Try

            If abl_openedproj AndAlso abl_openedled AndAlso Not e.Cancel Then

                Invoke(Sub()
                           With Loading
                               .Show()
                               Select Case lang
                                   Case Translator.tL.English
                                       .Text = Loading.MsgEn.loading_keyLED_def_msg
                                       .DLb.Text = Loading.MsgEn.loading_keyLED_open_msg
                                   Case Translator.tL.Korean
                               End Select
                               .DPr.Style = ProgressBarStyle.Marquee
                               .DPr.MarqueeAnimationSpeed = 10
                           End With
                       End Sub)

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
                Dim ChN As String = String.Empty 'New Tempo [Speed]
                Dim ChN2 As String = String.Empty 'Clip Tempo [BPM]
                Dim err As String = String.Empty
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

                            Dim dSpeed As Integer = 0
                            Dim dBPM As Integer = 0
#Region "Set the Tempo"
                            If String.IsNullOrWhiteSpace(ChN) = False Then 'Set Speed.

                                For Each ri As String In ChN.Split(";")
                                    If String.IsNullOrWhiteSpace(ri) Then
                                        Continue For
                                    End If

                                    If d_id = ri.Split("/")(0) Then
                                        ChN = ChN.Replace(ri & ";", "")
                                        dSpeed = ri.Split("/")(1)
                                        Exit For
                                    End If
                                Next

                            End If

                            If String.IsNullOrWhiteSpace(ChN2) = False Then 'Set BPM.

                                For Each ri As String In ChN2.Split(";")
                                    If String.IsNullOrWhiteSpace(ri) Then
                                        Continue For
                                    End If

                                    If d_id = ri.Split("/")(0) Then
                                        ChN2 = ChN2.Replace(ri & ";", "")
                                        dBPM = ri.Split("/")(1)
                                        Exit For
                                    End If
                                Next

                            End If
#End Region

                            UI(Sub()
                                   Loading.DLb.Left -= 70
                                   Select Case lang
                                       Case Translator.tL.English
                                           Loading.DLb.Text = String.Format(Loading.MsgEn.loading_keyLED_Convert_msg, dFile)
                                       Case Translator.tL.Korean
                                           Loading.DLb.Text = String.Format(Loading.MsgKr.loading_keyLED_Convert_msg, dFile)
                                   End Select
                               End Sub)

                            Dim dPath As String = String.Format("{0}\Workspace\ableproj\CoLED\{1}", Application.StartupPath, dFile)
                            If File.Exists(dPath) = False Then
                                Debug.WriteLine(String.Format("'{0}' File doesn't exists.", dFile))
                                err &= vbNewLine & String.Format("'{0}' MIDI File doesn't exists.", dFile)
                                Continue For
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

                                        If Not delaycount = a.AbsoluteTime OrElse Not a.DeltaTime = 0 Then
                                            If dSpeed = 0 Then
                                                str = str & vbNewLine & "d " & GetNoteDelay(keyLED_NoteEvents.NoteLength_2, dBPM, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delaycount)
                                            Else
                                                str = str & vbNewLine & "d " & Math.Round(GetNoteDelay(keyLED_NoteEvents.NoteLength_2, dBPM, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delaycount) * (dSpeed / 100))
                                            End If
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

                                        If Not delaycount = a.AbsoluteTime OrElse Not a.DeltaTime = 0 Then
                                            If dSpeed = 0 Then
                                                str = str & vbNewLine & "d " & GetNoteDelay(keyLED_NoteEvents.NoteLength_2, dBPM, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delaycount)
                                            Else
                                                str = str & vbNewLine & "d " & Math.Round(GetNoteDelay(keyLED_NoteEvents.NoteLength_2, dBPM, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delaycount) * (dSpeed / 100))
                                            End If
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

                            dSpeed = 0

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

                            UI(Sub()
                                   Loading.DLb.Left += 70
                                   Select Case lang
                                       Case Translator.tL.English
                                           Loading.DLb.Text = Loading.MsgEn.loading_keyLED_Convert2_msg
                                       Case Translator.tL.Korean
                                           Loading.DLb.Text = Loading.MsgEn.loading_keyLED_Convert2_msg
                                   End Select
                               End Sub)

                            'PatchSlot > Value > MxDPatchRef > FileRef > Name > Value 'Midi Extension.amxd'
                            'LED Save 파일의 id는 MxDeviceMidiEffect의 LomId Value랑 같음.

                            '또한 keySound에서는 Random 선언이 MidiToAudioDeviceChain,
                            'keyLED에서는 MidiToMidiDeviceChain임. (ㄹㅇ 에이블톤 프로그램 제작자들은 알고리즘을 왜 이따구로 만들었냐..)
                            Dim id_index As Integer = 0 'LomId (MIDI Extension id)
                            Dim fndError As Boolean = False 'Key / Random Key
                            Dim currentid As Integer = 0 '현재 id.
                            Dim MidiName As String = String.Empty

                            Dim PrChain As Integer = 0 '랜덤의 체인.
                            Dim PrChainM As Integer = 0 '랜덤의 최대 체인.
                            Dim PrKey As Integer = 0 '랜덤의 ksX.
                            Dim PrKeyM As Integer = 0 '랜덤의 최대 ksX.

                            Dim IsRandom As Boolean = False '현재 접근하고 있는 XML Branch가 랜덤인가?
                            Dim Choices As Integer = 0 '매우 정확한 랜덤의 수. (from MidiRandom)
                            Dim curid As Integer = 1 '현재의 랜덤. (from Choices / MidiRandom)
                            For ndx As Integer = 0 To setNode.Count - 1
                                Try
                                    currentid = Integer.Parse(setNode(ndx).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("LomId").GetAttribute("Value"))
                                    MidiName = setNode(ndx).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("PatchSlot").Item("Value").Item("MxDPatchRef").Item("FileRef").Item("Name").GetAttribute("Value")

                                    If Choices >= curid AndAlso IsRandom Then '랜덤인 경우.
                                        IsRandom = True
                                        If Choices = curid Then
                                            Choices = 0
                                            curid = 0
                                        End If

                                        curid += 1
                                    Else
                                        IsRandom = False
                                    End If

                                    fndError = False

                                Catch exN As NullReferenceException

                                    PrChain = Integer.Parse(setNode(ndx).Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1 '최소 체인.
                                    PrChainM = Integer.Parse(setNode(ndx).Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1 '최대 체인.

                                    PrKey = Integer.Parse(setNode(ndx).Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value")) '최소 Key (ksX).
                                    PrKeyM = Integer.Parse(setNode(ndx).Item("ZoneSettings").Item("KeyRange").Item("Max").GetAttribute("Value")) '최대 Key (ksX).

                                    Try
                                        Choices = Integer.Parse(setNode(ndx).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiRandom").Item("Choices").Item("Manual").GetAttribute("Value")) 'MidiRandom > Choices > Manual Value
                                    Catch exNN As NullReferenceException
                                        Choices = 0
                                    End Try

                                    If Choices > 0 Then '랜덤인 경우.
                                        IsRandom = True
                                    Else
                                        Choices = 0
                                        IsRandom = False
                                    End If
                                    fndError = True
                                End Try

                                If fndError = False Then
                                    currentid = Integer.Parse(setNode(ndx).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("LomId").GetAttribute("Value"))
                                    MidiName = setNode(ndx).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("PatchSlot").Item("Value").Item("MxDPatchRef").Item("FileRef").Item("Name").GetAttribute("Value")
                                    If d_id = currentid AndAlso MidiName = "Midi Extension.amxd" Then
                                        id_index = ndx
                                        Exit For
                                    End If
                                End If

                                If ndx = setNode.Count - 1 Then
                                    err &= vbNewLine & String.Format("Can't find id {0} on '{1}'.", d_id, dFile)
                                    Choices = 8192 'Same As Continue For
                                End If
                            Next
                            x = setNode(id_index)

                            UniPack_Chain = Integer.Parse(x.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1 'Get Chain.
                            UniPack_X = GX_keyLED(keyLED_NoteEvents.NoteNumber_1, Integer.Parse(x.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value"))) 'Get X Pos.
                            UniPack_Y = GY_keyLED(keyLED_NoteEvents.NoteNumber_1, Integer.Parse(x.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value"))) 'Get Y Pos.
                            UniPack_L = 1

                            Dim MaxChain As Integer = Integer.Parse(x.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1
                            If Not PrChain = 0 AndAlso IsRandom Then 'Random Chain.
                                UniPack_Chain = PrChain
                                MaxChain = PrChainM
                            Else
                                PrChain = 0
                            End If

                            Dim MaxX As Integer = GX_keyLED(keyLED_NoteEvents.NoteNumber_1, Integer.Parse(x.Item("ZoneSettings").Item("KeyRange").Item("Max").GetAttribute("Value"))) 'Get X Pos.
                            Dim MaxY As Integer = GY_keyLED(keyLED_NoteEvents.NoteNumber_1, Integer.Parse(x.Item("ZoneSettings").Item("KeyRange").Item("Max").GetAttribute("Value"))) 'Get Y Pos.
                            If Not PrKey = 0 AndAlso IsRandom Then 'Random Key.
                                UniPack_X = GX_keyLED(keyLED_NoteEvents.NoteNumber_1, PrKey)
                                UniPack_Y = GY_keyLED(keyLED_NoteEvents.NoteNumber_1, PrKey)
                            End If

                            If UniPack_Chain > 8 OrElse UniPack_Chain = 0 OrElse UniPack_X = -8192 OrElse UniPack_X = 0 OrElse Choices = 8192 Then
                                Continue For
                            End If

                            Dim LoopNumber_1 As Integer() = New Integer(1) {}
                            Dim LoopNumber_1bool As Boolean 'Chain Value = ?
                            LoopNumber_1(0) = UniPack_Chain
                            LoopNumber_1(1) = MaxChain
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

                        Case "Clip Tempo" 'MIDI Clip's BPM.
                            Dim d_id As Integer = d.Split("/")(1)
                            Dim dBPM As Integer = d.Split("/")(2)
                            ChN2 &= String.Format("{0}/{1};", d_id, dBPM)

                        Case "New Tempo" 'MIDI Clip's Speed.
                            Dim d_id As Integer = d.Split("/")(1)
                            Dim dSpeed As Integer = d.Split("/")(2)
                            ChN &= String.Format("{0}/{1};", d_id, dSpeed)

                    End Select

                Next

                Debug.WriteLine("Finish...")
                UI(Sub()
                       Select Case lang
                           Case Translator.tL.English
                               Loading.Text = Loading.MsgEn.loading_keyLED_Convert3_msg
                           Case Translator.tL.Korean
                               Loading.Text = Loading.MsgKr.loading_keyLED_Convert3_msg
                       End Select
                   End Sub)

                UI(Sub() Loading.Dispose())

                If w8t4abl = "keyLED" Then
                    w8t4abl = String.Empty
                End If

                IsWorking = False
                keyLEDIsSaved = True
                kl_Converted = True

                UI(Sub()
                       keyLEDPad_Flush(True)
                       Thread.Sleep(300)
                       BGW_keyLEDLayout.RunWorkerAsync()
                   End Sub)

                If Not String.IsNullOrWhiteSpace(err) Then
                    MessageBox.Show("[ Warning ]" & vbNewLine & "keyLED (MIDI Extension): [] format is invaild." & vbNewLine & err, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Else
                w8t4abl = "keyLED"
                e.Cancel = True
            End If

        Catch ex As Exception
            UI(Sub() Loading.Dispose())
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
                If OpenProjectOnce Then
                    OpenProjectOnce = False
                    If abl_openedproj AndAlso abl_openedsnd AndAlso abl_openedled Then
                        Select Case lang
                            Case Translator.tL.English
                                MessageBox.Show("Ableton Project, Sounds, LEDs Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case Translator.tL.Korean
                                MessageBox.Show("에이블톤 프로젝트, 사운드, LED 파일들이 로딩되었습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Select
                    ElseIf abl_openedproj AndAlso abl_openedsnd AndAlso abl_openedled Then
                        Select Case lang
                            Case Translator.tL.English
                                MessageBox.Show("Ableton Project, Sounds Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case Translator.tL.Korean
                                MessageBox.Show("에이블톤 프로젝트, 사운드가 로딩되었습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Select
                    ElseIf abl_openedproj Then
                        Select Case lang
                            Case Translator.tL.English
                                MessageBox.Show("Ableton Project Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case Translator.tL.Korean
                                MessageBox.Show("에이블톤 프로젝트가 로딩되었습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Select
                    ElseIf abl_openedsnd AndAlso abl_openedled Then
                        Select Case lang
                            Case Translator.tL.English
                                MessageBox.Show("Sounds, LEDs Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case Translator.tL.Korean
                                MessageBox.Show("사운드, LED 파일이 로딩되었습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Select
                    ElseIf abl_openedsnd Then
                        Select Case lang
                            Case Translator.tL.English
                                MessageBox.Show("Sounds Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case Translator.tL.Korean
                                MessageBox.Show("사운드가 로딩되었습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Select
                    ElseIf abl_openedled Then
                        Select Case lang
                            Case Translator.tL.English
                                MessageBox.Show("LEDs Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case Translator.tL.Korean
                                MessageBox.Show("LED 파일이 로딩되었습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Select
                    End If
                    Exit Sub
                End If
            Else

                IsWorking = False

                If OpenProjectOnce Then
                    OpenProjectOnce = False
                    If abl_openedproj AndAlso abl_openedsnd AndAlso abl_openedled Then
                        Select Case lang
                            Case Translator.tL.English
                                MessageBox.Show("Ableton Project, Sounds, LEDs Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case Translator.tL.Korean
                                MessageBox.Show("에이블톤 프로젝트, 사운드, LED 파일들이 로딩되었습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Select
                    ElseIf abl_openedproj AndAlso abl_openedsnd AndAlso abl_openedled Then
                        Select Case lang
                            Case Translator.tL.English
                                MessageBox.Show("Ableton Project, Sounds Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case Translator.tL.Korean
                                MessageBox.Show("에이블톤 프로젝트, 사운드가 로딩되었습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Select
                    ElseIf abl_openedproj Then
                        Select Case lang
                            Case Translator.tL.English
                                MessageBox.Show("Ableton Project Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case Translator.tL.Korean
                                MessageBox.Show("에이블톤 프로젝트가 로딩되었습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Select
                    ElseIf abl_openedsnd AndAlso abl_openedled Then
                        Select Case lang
                            Case Translator.tL.English
                                MessageBox.Show("Sounds, LEDs Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case Translator.tL.Korean
                                MessageBox.Show("사운드, LED 파일이 로딩되었습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Select
                    ElseIf abl_openedsnd Then
                        Select Case lang
                            Case Translator.tL.English
                                MessageBox.Show("Sounds Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case Translator.tL.Korean
                                MessageBox.Show("사운드가 로딩되었습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Select
                    ElseIf abl_openedled Then
                        Select Case lang
                            Case Translator.tL.English
                                MessageBox.Show("LEDs Loaded!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case Translator.tL.Korean
                                MessageBox.Show("LED 파일이 로딩되었습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Select
                    End If
                    Exit Sub
                End If

                Select Case lang
                    Case Translator.tL.English
                        MessageBox.Show("LED File Converted!" & vbNewLine & "You can show the LEDs on 'keyLED (MIDI Extension)' Tab!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Case Translator.tL.Korean
                        MessageBox.Show("LED File Converted!" & vbNewLine & "''keyLED (미디 익스텐션)' 탭에서 LED 파일들을 볼 수 있습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Select

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
            Select Case lang
                Case Translator.tL.English
                    MessageBox.Show("MIDI Input and Note On Test Disabled.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Case Translator.tL.Korean
                    MessageBox.Show("비활성화: 미디 입력, 노트 테스트", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Select
        Else
            IsMIDITest = True
            Select Case lang
                Case Translator.tL.English
                    MessageBox.Show("MIDI Input and Note On Test Enabled!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Case Translator.tL.Korean
                    MessageBox.Show("활성화: 미디 입력, 노트 테스트", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Select
        End If
    End Sub

    'keyLED
    Private Sub BGW_keyLEDLayout_DoWork(sender As Object, e As DoWorkEventArgs) Handles BGW_keyLEDLayout.DoWork
        For Each LEDFile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Workspace\unipack\keyLED\", FileIO.SearchOption.SearchTopLevelOnly)

            Dim LEDName As String = Path.GetFileName(LEDFile)
            Dim LEDSyntax As String() = LEDName.Split(" ")

            Dim Chain As Integer = LEDSyntax(0)
            Dim x As Integer = LEDSyntax(1)
            Dim y As Integer = LEDSyntax(2)
            Dim l As Integer = LEDSyntax(3)

            If klUniPack_SelectedChain = Chain Then

                UI(Sub()
                       kl_ctrl(x & y).BackColor = Color.SkyBlue
                       If String.IsNullOrWhiteSpace(kl_ctrl(x & y).Text) Then
                           kl_ctrl(x & y).Text = "1"
                       Else
                           kl_ctrl(x & y).Text = Integer.Parse(kl_ctrl(x & y).Text) + 1
                       End If
                   End Sub)

            Else
                Continue For
            End If

        Next
    End Sub

    Private Sub keyLED_Chains_Click(sender As Object, e As EventArgs) Handles btn_Chain1.Click, btn_Chain2.Click, btn_Chain3.Click, btn_Chain4.Click, btn_Chain5.Click, btn_Chain6.Click, btn_Chain7.Click, btn_Chain8.Click
        Dim SelectedChain As Integer = CType(sender, Button).Name.Substring(9, 1)
        klUniPack_SelectedChain = SelectedChain

        If keyLEDMIDEX_LEDViewMode.Checked Then
            kl_LEDFlush()
            BGW_keyLEDLayout.RunWorkerAsync()
        End If
    End Sub

    Private Sub keyLED_ButtonsClick(sender As Object, e As EventArgs) Handles u11.MouseDown, u12.MouseDown, u13.MouseDown, u14.MouseDown, u15.MouseDown, u16.MouseDown, u17.MouseDown, u18.MouseDown, u21.MouseDown, u22.MouseDown, u23.MouseDown, u24.MouseDown, u25.MouseDown, u26.MouseDown, u27.MouseDown, u28.MouseDown, u31.MouseDown, u32.MouseDown, u33.MouseDown, u34.MouseDown, u35.MouseDown, u36.MouseDown, u37.MouseDown, u38.MouseDown, u41.MouseDown, u42.MouseDown, u43.MouseDown, u44.MouseDown, u45.MouseDown, u46.MouseDown, u47.MouseDown, u48.MouseDown, u51.MouseDown, u52.MouseDown, u53.MouseDown, u54.MouseDown, u55.MouseDown, u56.MouseDown, u57.MouseDown, u58.MouseDown, u61.MouseDown, u62.MouseDown, u63.MouseDown, u64.MouseDown, u65.MouseDown, u66.MouseDown, u67.MouseDown, u68.MouseDown, u71.MouseDown, u72.MouseDown, u73.MouseDown, u74.MouseDown, u75.MouseDown, u76.MouseDown, u77.MouseDown, u78.MouseDown, u81.MouseDown, u82.MouseDown, u83.MouseDown, u84.MouseDown, u85.MouseDown, u86.MouseDown, u87.MouseDown, u88.MouseDown
        Dim klX As Button = CType(sender, Button)
        Dim x As Integer = klX.Name.Substring(1, 1)
        Dim y As Integer = klX.Name.Substring(2, 1)
        Dim l As Integer = 1 '유니컨버터 v1.1.0.3 Loop 변수.

        If Not File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", klUniPack_SelectedChain, x, y, l)) Then
            Exit Sub
        End If

        File.WriteAllText(Application.StartupPath & "\Workspace\TmpLED.txt", File.ReadAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", klUniPack_SelectedChain, x, y, l)))

        If keyLEDMIDEX_prMode.Checked Then
            ThreadPool.QueueUserWorkItem(AddressOf kl_LEDHandler)
        End If
    End Sub
#Region "LED 테스트 코드"
    Private Sub kl_LEDHandler()
        Try
            Dim linesInfo As New List(Of String)(File.ReadAllLines(Application.StartupPath & "\Workspace\TmpLED.txt"))
            linesInfo.RemoveAll(Function(s) s.Trim = "")
            Dim Lines() As String = linesInfo.ToArray
            Dim linescounter As Integer = Lines.Length

            Dim sp() As String
            For i = 0 To linescounter - 1

                sp = Lines(i).Split(" ")
                If sp(0) = "o" OrElse sp(0) = "on" Then

                    'Velocity Code.
                    If sp(3) = "a" OrElse sp(3) = "auto" Then
                        sp(3) = sp(4)
                        If IsNumeric(sp(3)) = False Then
                            MessageBox.Show("Wrong UniPad button code on line " & i + 1 & "!", "Wrong on command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End If
                        sp(3) = led.returnLED(sp(3))
                    End If

                    'On Code.
                    If sp(1) = "mc" Then
                        Try
                            kl_ctrl("mc" & sp(2)).BackColor = ColorTranslator.FromHtml("#" & sp(3))
                        Catch ex As Exception
                            MessageBox.Show("Wrong UniPad button code mc line " & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong on command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End Try
                    Else

                        Try
                            kl_ctrl(sp(1) & sp(2)).BackColor = ColorTranslator.FromHtml("#" & sp(3))
                        Catch
                            MessageBox.Show("Wrong UniPad button code on line " & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong on command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End Try
                    End If

                ElseIf sp(0) = "f" OrElse sp(0) = "off" Then

                    'Off Code.
                    If sp(1) = "mc" Then
                        Try
                            kl_ctrl("mc" & sp(2)).BackColor = Color.Gray
                        Catch ex As Exception
                            MessageBox.Show("Wrong UniPad button code mc line " & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong on command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End Try
                    Else

                        Try
                            kl_ctrl(sp(1) & sp(2)).BackColor = Color.Gray
                        Catch
                            MessageBox.Show("Wrong UniPad button code on line" & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong off command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End Try
                    End If

                ElseIf sp(0) = "d" OrElse sp(0) = "delay" Then
                    'Delay Code.
                    If IsNumeric(sp(1)) = False Then
                        MessageBox.Show("Wrong millisecond code on line " & i + 1 & "!", "Wrong delay command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit For
                    End If
                    Thread.Sleep(sp(1))

                Else
                    MessageBox.Show("Wrong LED command " & Lines(i) & " on line" & i, "Wrong Data", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit For
                End If
            Next
        Catch

        End Try
    End Sub

    Public Sub kl_LEDFlush()
        For x As Integer = 1 To 8
            For y As Integer = 1 To 8
                kl_ctrl(x & y).BackColor = Color.Gray
                kl_ctrl(x & y).Text = String.Empty
            Next
        Next

        For mc As Integer = 1 To 32
            kl_ctrl("mc" & mc).BackColor = Color.Gray
        Next
    End Sub
#End Region

    Private Sub KeyLEDMIDEX_LEDViewMode_CheckedChanged(sender As Object, e As EventArgs) Handles keyLEDMIDEX_LEDViewMode.CheckedChanged
        If keyLEDMIDEX_LEDViewMode.Checked Then
            kl_LEDFlush()
            BGW_keyLEDLayout.RunWorkerAsync()
        End If
    End Sub

    Private Sub KeyLEDMIDEX_prMode_CheckedChanged(sender As Object, e As EventArgs) Handles keyLEDMIDEX_prMode.CheckedChanged
        If keyLEDMIDEX_prMode.Checked Then
            kl_LEDFlush()
        End If
    End Sub

    Public Sub keyLEDPad_Flush(ByVal Enabled As Boolean)
        keyLED_Pad64.Enabled = Enabled
        btn_Chain1.Enabled = Enabled
        btn_Chain2.Enabled = Enabled
        btn_Chain3.Enabled = Enabled
        btn_Chain4.Enabled = Enabled
        btn_Chain5.Enabled = Enabled
        btn_Chain6.Enabled = Enabled
        btn_Chain7.Enabled = Enabled
        btn_Chain8.Enabled = Enabled

        keyLEDMIDEX_Md.Enabled = Enabled
    End Sub

    Private Sub ResetTheProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetTheProjectToolStripMenuItem.Click
        '이 코드는 MainProject.Load 코드를 따와서 만들었습니다.
        '만약 MainProject.Load 코드가 업데이트를 했다면, 이 코드도 업데이트를 해주시기 바랍니다.

        If Directory.Exists(Application.StartupPath & "\Workspace") Then
            Directory.Delete(Application.StartupPath & "\Workspace", True)
            Thread.Sleep(300)
            Directory.CreateDirectory(Application.StartupPath & "\Workspace")
        Else
            Directory.CreateDirectory(Application.StartupPath & "\Workspace")
        End If

        abl_openedproj = False
        abl_openedsnd = False
        abl_openedled = False
        abl_openedled2 = False

        Select Case lang
            Case Translator.tL.English
                infoTB1.Text = "My Amazing UniPack!"
                infoTB2.Text = "UniConverter, " & My.Computer.Name
            Case Translator.tL.Korean
                infoTB1.Text = "나의 멋진 유니팩!"
                infoTB2.Text = "유니컨버터, " & My.Computer.Name
        End Select

        infoTB3.Text = "1"

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

        For x As Integer = 1 To 8
            For y As Integer = 1 To 8
                ks_ctrl(x & y).Text = String.Empty
                ks_ctrl(x & y).BackColor = Color.Gray
                ks_ctrl(x & y).ForeColor = Color.Black
            Next
        Next

        SoundIsSaved = False
        keyLEDIsSaved = False
        infoIsSaved = False
        IsWorking = False

        w8t4abl = String.Empty
        OpenProjectOnce = False

        keyLEDMIDEX_LEDViewMode.Checked = True
        keyLEDPad_Flush(False)
        keyLEDMIDEX_BetaButton.Enabled = False
        kl_LEDFlush()

        IsSaved = True
        Select Case lang
            Case Translator.tL.English
                MessageBox.Show("The Project reseted!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Case Translator.tL.Korean
                MessageBox.Show("프로젝트를 초기화 하였습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select
    End Sub

    Private Sub KeyLEDMIDEX_BetaButton_Click(sender As Object, e As EventArgs) Handles keyLEDMIDEX_BetaButton.Click
        keyLED_Edit.Show()
    End Sub
End Class
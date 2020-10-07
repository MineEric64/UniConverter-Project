Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Threading
Imports System.Xml
Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports System.Drawing.Drawing2D
Imports System.Text
Imports System.Globalization

Imports NAudio.Wave
Imports NAudio.Midi

Imports ICSharpCode.SharpZipLib.GZip
Imports ICSharpCode.SharpZipLib.Core

Imports A2UP
Imports A2UP.A2U.keyLED_MIDEX
Imports A2UP.A2U.keySound

Imports WMPLib

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
    ''' keyLED 변환 여부.
    ''' </summary>
    Public kl_Converted As Boolean
#End Region
#Region "MainProject-Thread(s)"
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
    Public OpenProjectOnce As ProjectOpenMethod

    ''' <summary>
    ''' 지금 매우 중요한 작업 여부.
    ''' </summary>
    Public Shared IsWorking As Boolean

    ''' <summary>
    ''' Waiting For Ableton Project. (LED Convert: "keyLED")
    ''' </summary>
    Public Shared w8t4abl As String

    ''' <summary>
    ''' 유니컨버터 언어. (English / Korean)
    ''' </summary>
    Public Shared lang As Translator.tL

    ''' <summary>
    ''' 유니팩 이름
    ''' </summary>
    Public Shared UniPack_Title As String

    ''' <summary>
    ''' 유니팩 제작자 이름
    ''' </summary>
    Public Shared UniPack_ProducerName As String

    ''' <summary>
    ''' 유니팩 체인
    ''' </summary>
    Public Shared UniPack_Chains As Integer

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

    Private _midiExtensionMapping As New Dictionary(Of Integer, MidiExtensionSave)

    Public Shared ReadOnly WORKSPACE_PATH As String = Application.StartupPath & "\Workspace"

    Public Shared ReadOnly ABLETON_PROJECT_PATH As String = WORKSPACE_PATH & "\ableproj"
    Public Shared ReadOnly ABLETON_PROJECT_XML_PATH As String = ABLETON_PROJECT_PATH & "\abl_proj.xml"
    Public Shared ReadOnly ABLETON_SOUNDS_PATH As String = ABLETON_PROJECT_PATH & "\sounds"
    Public Shared ReadOnly ABLETON_KEYLED_PATH As String = ABLETON_PROJECT_PATH & "\CoLED"
    
    Public Shared ReadOnly UNIPACK_PROJECT_PATH As String = WORKSPACE_PATH & "\unipack"
    Public Shared ReadOnly UNIPACK_INFO_PATH As String = UNIPACK_PROJECT_PATH & "\info"
    Public Shared ReadOnly UNIPACK_SOUNDS_PATH As String = UNIPACK_PROJECT_PATH & "\sounds"
    Public Shared ReadOnly UNIPACK_KEYSOUND_PATH As String = UNIPACK_PROJECT_PATH & "\keySound"
    Public Shared ReadOnly UNIPACK_KEYLED_PATH As String = UNIPACK_PROJECT_PATH & "\keyLED"
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
    Public Shared LEDMappings As Char() = New Char(25) {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
#End Region

#Region "DLL Import Functions"
    Declare Function GetDC Lib "user32" Alias "GetDC" (ByVal hwnd As Integer) As Integer
    'Declare Function RoundRect Lib "gdi32" Alias "RoundRect" (ByVal hdc As Integer, ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer, ByVal x3 As Integer, ByVal y3 As Integer) As Integer
#End Region

    ''' <summary>
    ''' 특별 기호 (")
    ''' </summary>
    Public Shared ast As String = Chr(34)

    ''' <summary>
    ''' Temp 폴더.
    ''' </summary>
    Public Shared TempDirectory As String = My.Computer.FileSystem.SpecialDirectories.Temp

    Private Async Sub MainProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            w8t4abl = String.Empty
            Me.KeyPreview = True
            OpenProjectOnce = ProjectOpenMethod.Smart

            Await Initialize()

            'Text of Info TextBox
            infoTB1.Text = "My Amazing UniPack!" 'Title
            infoTB2.Text = "UniConverter, " & My.Computer.Name 'Producer Name

            setxml.Load(file_ex)
            Dim setNode As XmlNode
            setNode = setxml.SelectSingleNode("/UniConverter-XML/UniConverter-Settings")

            If setNode.ChildNodes(0).InnerText = "True" Then
                BGW_CheckUpdate.RunWorkerAsync()
            End If

            If setNode.ChildNodes(2).InnerText = "True" Then
                SetUpLight_ = True
            End If

            'Translate the Language from "Translator" Class.
            Dim Ln As String = setNode.ChildNodes(3).InnerText
            Dim tLn As Translator.tL = Translator.GetLnEnum(Ln)
            lang = tLn

            Select Case tLn
                Case Translator.tL.English
                    Dim culture As CultureInfo = CultureInfo.GetCultureInfo("en-US")

                    Thread.CurrentThread.CurrentUICulture = culture
                    My.Resources.Contents.Culture = culture
                Case Translator.tL.Korean
                    Dim culture As CultureInfo = CultureInfo.GetCultureInfo("ko-KR")

                    Thread.CurrentThread.CurrentUICulture = culture
                    My.Resources.Contents.Culture = culture
            End Select

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

    Public Async Function Initialize() As Task
#Region "변수 기본값 설정"
        abl_openedproj = False
        abl_openedsnd = False
        abl_openedled = False

        IsSaved = True
        SoundIsSaved = False
        keyLEDIsSaved = False
        infoIsSaved = False
        IsUpdated = False
        IsWorking = False
        ks_Converted = False
        kl_Converted = False

        UniPack_Title = String.Empty
        UniPack_ProducerName = String.Empty
        UniPack_Chains = 1

        keyLEDMIDEX_LEDViewMode.Checked = True
        'keyLEDPad_Flush(False)
        
        btnKeySound_AutoConvert.Enabled = False
        keyLEDMIDEX_BetaButton.Enabled = False
        btnConvertKeyLEDAutomatically.Enabled = False
#End Region

        Await Task.Run(Sub() 'Workspace의 UniPack 폴더 정리.
            DeleteWorkspaceDir()
        End Sub)
    End Function

    Private Sub InfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InfoToolStripMenuItem.Click
        Info.Show()
    End Sub

    Public Sub DeleteWorkspaceDir()
        If Directory.Exists(Application.StartupPath & "\Workspace") Then
            Directory.Delete(Application.StartupPath & "\Workspace", True)
            Directory.CreateDirectory(Application.StartupPath & "\Workspace")
        Else
            Directory.CreateDirectory(Application.StartupPath & "\Workspace")
        End If
    End Sub

    Private Async Sub OpenSoundsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SoundsToolStripMenuItem.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = My.Resources.Contents.Sound_ofd_Filter
        ofd.Title = My.Resources.Contents.Sound_ofd_Title
        ofd.Multiselect = True

        If ofd.ShowDialog() = DialogResult.OK Then
            Await Task.Run(Sub()
                         OpenSounds(ofd.FileNames, True)
                     End Sub)

            Await ReadyForAutoConvertForKeySound()
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
        Task.Run(Sub()
            SaveProject(True)
                 End Sub)
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

    Public Function roundRect(ByVal Rc As Rectangle, ByVal Rnd As Integer) As GraphicsPath
        Dim path As New GraphicsPath

        path.AddArc(Rc.X, Rc.Y, Rnd, Rnd, -180, 90)
        path.AddLine(Rc.X + (Rnd \ 2), Rc.Y, Rc.X + Rc.Width - (Rnd \ 2), Rc.Y)
        path.AddArc(Rc.X + Rc.Width - Rnd, Rc.Y, Rnd, Rnd, -90, 90)

        path.AddLine(Rc.X + Rc.Width, Rc.Y + (Rnd \ 2), Rc.X + Rc.Width, Rc.Y + Rc.Height - (Rnd \ 2))
        path.AddArc(Rc.X + Rc.Width - Rnd, Rc.Y + Rc.Height - Rnd, Rnd, Rnd, 0, 90)

        path.AddLine(Rc.X + (Rnd \ 2), Rc.Y + Rc.Height, Rc.X + Rc.Width - (Rnd \ 2), Rc.Y + Rc.Height)

        path.AddArc(Rc.X, Rc.Y + Rc.Height - Rnd, Rnd, Rnd, 90, 90)

        path.AddLine(Rc.X, Rc.Y + (Rnd \ 2), Rc.X, Rc.Y + Rc.Height - (Rnd \ 2))

        Return path
    End Function

    Public Function roundRect(ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal Rnd As Integer) As GraphicsPath
        Dim path As New GraphicsPath

        path.AddArc(x, y, Rnd, Rnd, -180, 90)
        path.AddLine(x + (Rnd \ 2), y, x + width - (Rnd \ 2), y)
        path.AddArc(x + width - Rnd, y, Rnd, Rnd, -90, 90)

        path.AddLine(x + width, y + (Rnd \ 2), x + width, y + height - (Rnd \ 2))
        path.AddArc(x + width - Rnd, y + height - Rnd, Rnd, Rnd, 0, 90)

        path.AddLine(x + (Rnd \ 2), y + height, x + width - (Rnd \ 2), y + height)

        path.AddArc(x, y + height - Rnd, Rnd, Rnd, 90, 90)

        path.AddLine(x, y + (Rnd \ 2), x, y + height - (Rnd \ 2))

        Return path
    End Function

    Private Sub OpenKeyLED(fileNames As String(), showLoadingMessage As Boolean)
        If fileNames.Length > 0 Then
            If showLoadingMessage Then
                Invoke(Sub()
                    Loading.Show()
                    Loading.DPr.Maximum = fileNames.Length

                    Loading.Text = My.Resources.Contents.LED_Open_Title
                    Loading.DLb.Text = String.Format(My.Resources.Contents.LED_Open, 0, fileNames.Length)
                       End Sub)
            End If

            If Directory.Exists(ABLETON_KEYLED_PATH) Then
                Directory.Delete(ABLETON_KEYLED_PATH, True)
            End If

            Directory.CreateDirectory(ABLETON_KEYLED_PATH)

            For i = 0 To fileNames.Length - 1
                Dim fileName As String = fileNames(i)

                File.Copy(fileName, Path.Combine(ABLETON_KEYLED_PATH, Path.GetFileName(fileName)), True)
                
                If showLoadingMessage Then
                    Invoke(Sub()
                           Loading.DPr.Style = ProgressBarStyle.Continuous
                           Loading.DPr.Value += 1

                           Loading.DLb.Text = String.Format(My.Resources.Contents.LED_Open, Loading.DPr.Value, fileNames.Length)
                       End Sub)
                End If
            Next

            If showLoadingMessage Then
                Invoke(Sub()
                       Loading.DPr.Value = Loading.DPr.Maximum
                       Loading.DPr.Style = ProgressBarStyle.Marquee

                       Loading.DLb.Text = My.Resources.Contents.LED_Loaded
                   End Sub)
            End If

            abl_openedled = True
            GetMidiExtensionSaveFile(fileNames)

            If showLoadingMessage Then
                Invoke(Sub()
                    Loading.Close()
                End Sub)

                MessageBox.Show(My.Resources.Contents.LED_Loaded, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Function GetMidiExtensionSaveFile(ledFilePath As String()) As Boolean
        Try
            _midiExtensionMapping.Clear()

            Dim pathList As New List(Of String)

            Dim saveFilePath As String = String.Empty
            Dim saveFileFound As Boolean = False

            For Each led In ledFilePath
                Dim parentPath As String = Directory.GetParent(led).FullName

                If Not pathList.Contains(parentPath) Then
                    For Each x In Directory.GetFiles(parentPath, "*.*", SearchOption.AllDirectories)
                        Dim fileName As String = Path.GetFileName(x)

                        If fileName.ToLower().Contains("save") AndAlso Not Path.HasExtension(x) Then
                            saveFilePath = x
                            saveFileFound = True
                            Exit For
                        End If
                    Next

                    If saveFileFound Then
                        Exit For
                    End If

                    pathList.Add(parentPath)
                End If
            Next

            If saveFileFound AndAlso Not String.IsNullOrWhiteSpace(saveFilePath) Then
                _midiExtensionMapping = GetMappingListForMidiExtension(saveFilePath)
            End If

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

        Return _midiExtensionMapping.Count > 0
    End Function

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

    ''' <summary>
    ''' 체인 값을 가져옵니다.
    ''' </summary>
    ''' <param name="xmlPath">에이블톤 프로젝트 XML 경로</param>
    ''' <returns></returns>
    Public Shared Function GetChainFromAbletonProject(xmlPath As String) As Integer
        Dim doc As New XmlDocument
        doc.Load(xmlPath)

        Dim chain = 1

        'LED
        Dim ledNodes As XmlNodeList = doc.GetElementsByTagName("MidiEffectBranch")
        Dim ledNodeList As List(Of XmlNode) = ledNodes.Cast(Of XmlNode).ToList()

        Dim chainList As List(Of Integer) = ledNodeList.Select(Function(x) Integer.Parse(x.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1).ToList()
        Dim sortedChainList As List(Of Integer) = chainList.OrderByDescending(Function(x) x).ToList()

        If sortedChainList.Count > 0 Then
            Dim maxChain As Integer = sortedChainList.First()

            ledNodeList.Clear()
            chainList.Clear()
            sortedChainList.Clear()

            If maxChain >= 1 AndAlso maxChain <= 8 Then
                chain = maxChain
            ElseIf maxChain > 8 Then
                chain = 8
            End If '최대 체인이 1보다 작은 경우는 이미 chain이 1이므로 생략
        End If

        Return chain
    End Function

    ''' <summary>
    ''' 에이블톤 프로젝트 파일을 불러옵니다.
    ''' </summary>
    ''' <param name="fileName">에이블톤 프로젝트 파일 경로</param>
    ''' <param name="showLoadingMessage">불러오는 중 메시지의 존재 여부</param>
    Private Sub OpenAbletonProjectFile(fileName As String, showLoadingMessage As Boolean)
        If String.IsNullOrWhiteSpace(fileName) OrElse Not File.Exists(fileName) Then
            Return
        End If

        If Not Directory.Exists(ABLETON_PROJECT_PATH) Then
            Directory.CreateDirectory(ABLETON_PROJECT_PATH)
        End If

        If showLoadingMessage Then
            Invoke(Sub()
                Loading.Show()
                Loading.DPr.Style = ProgressBarStyle.Marquee
                Loading.DPr.MarqueeAnimationSpeed = 10

                Loading.Text = Me.Text & $": {My.Resources.Contents.Project_Title}"
                Loading.DLb.Text = My.Resources.Contents.Project_Loading
               End Sub)
        End If

        If showLoadingMessage Then
            Invoke(Sub()
                Loading.DLb.Text = My.Resources.Contents.Project_DeletingTempoaryFiles
                   End Sub)
        End If

        If File.Exists(ABLETON_PROJECT_XML_PATH) Then
            File.Delete(ABLETON_PROJECT_XML_PATH)
        End If

        abl_FileName = fileName
        File.Copy(fileName, ABLETON_PROJECT_PATH & "\abl_proj.gz", True)

        If showLoadingMessage Then
            Invoke(Sub()
                Loading.DLb.Text = My.Resources.Contents.Project_Extracting
               End Sub)
        End If

        ExtractGZip(ABLETON_PROJECT_PATH & "\abl_proj.gz", ABLETON_PROJECT_PATH)

        If showLoadingMessage Then
            Invoke(Sub()
                Loading.DLb.Text = My.Resources.Contents.Project_DeletingTempoaryFiles
            End Sub)
        End If

        File.Delete("Workspace\ableproj\abl_proj.gz")

        If showLoadingMessage Then
            Invoke(Sub()
                Loading.DLb.Text = My.Resources.Contents.Project_ChangeExtension
            End Sub)
        End If

        File.Move(ABLETON_PROJECT_PATH & "\abl_proj", ABLETON_PROJECT_XML_PATH)

        'Reading Informations of Ableton Project.

        'Ableton Project's Name.
        If showLoadingMessage Then
            Invoke(Sub()
                Loading.DLb.Text = My.Resources.Contents.Project_FileName
            End Sub)
        End If

        Dim finalName As String = Path.GetFileNameWithoutExtension(fileName)

        'Ableton Project's Chain.
        If showLoadingMessage Then
            Invoke(Sub()
                Loading.DLb.Text = My.Resources.Contents.Project_Chain
            End Sub)
        End If

        '정리.
        abl_Name = finalName
        abl_Chain = GetChainFromAbletonProject(ABLETON_PROJECT_XML_PATH)

        'XML File Load.
        If showLoadingMessage Then
            Invoke(Sub()
                Loading.DLb.Text = My.Resources.Contents.Project_Loading

                infoTB1.Text = abl_Name
                infoTB3.Text = abl_Chain
                   End Sub)
        End If

        abl_openedproj = True
        UniPack_SaveInfo(False)

        If showLoadingMessage Then
            Invoke(Sub()
                Loading.Close()
            End Sub)

            MessageBox.Show(My.Resources.Contents.Project_Loaded, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ''' <summary>
    ''' 자동 변환 (KeySound)
    ''' </summary>
    Public Async Function ReadyForAutoConvertForKeySound() As Task
        If abl_openedproj AndAlso abl_openedsnd Then
            btnKeySound_AutoConvert.Enabled = True

            If AutoConvert.Checked Then
                Dim errorMessage As String = Await ReadyForConvertKeySound(True)

                If Not String.IsNullOrWhiteSpace(errorMessage) Then
                    MessageBox.Show(String.Format(My.Resources.Contents.Sound_Converting_Error, errorMessage), Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                MessageBox.Show(My.Resources.Contents.KeySound_Created, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Function

    Private Async Function ReadyForConvertKeySound(showLoadingMessage As Boolean) As Task(Of String)
        Dim errorMessage As New StringBuilder(255)
        Dim conversionErrorMessage As String = String.Empty
        Dim soundConversion As Task(Of KeySoundStructure()) = Task.Run(Function() As KeySoundStructure()
                                                                           Return ConvertKeySound_v2(ABLETON_PROJECT_XML_PATH, conversionErrorMessage, True)
                                                                       End Function)

        Dim sounds As KeySoundStructure() = Await soundConversion

        If Not String.IsNullOrWhiteSpace(conversionErrorMessage) Then
            errorMessage.Append(errorMessage)
        End If

        Await Task.Run(Sub()
            Dim keySound As New StringBuilder(255)

            Dim sortedSoundList As New List(Of KeySoundStructure)(sounds)
            sortedSoundList = sortedSoundList.OrderBy(Function(x) x.Chain * 100 + x.X * 10 + x.Y).ToList()
            Dim trimmedSoundList As List(Of KeySoundStructure) = sortedSoundList.Where(Function(x) x.StartTime <> TimeSpan.Zero OrElse x.EndTime <> TimeSpan.Zero).ToList()
            trimmedSoundList = trimmedSoundList.OrderBy(Function(x) Long.Parse($"{Convert.ToInt32(Math.Truncate(x.StartTime.TotalMilliseconds))}{Convert.ToInt32(Math.Truncate(x.EndTime.TotalMilliseconds))}")).ToList()

            Dim previousChain = 0
            Dim padZeroFormat As String = New String("0"C, trimmedSoundList.Count.ToString().Length)

            If showLoadingMessage Then
                Invoke(Sub()
                    Loading.Show()
                    Loading.Text = My.Resources.Contents.KeySound_Creating_Title
                    Loading.DLb.Text = String.Format(My.Resources.Contents.Sound_Verifying, 0, trimmedSoundList.Count)
                       End Sub)
            End If

            For i = 0 To trimmedSoundList.Count - 1
                Dim sound As KeySoundStructure = trimmedSoundList(i)

                If Path.GetExtension(sound.FileName) <> ".wav" Then 'mp3 파일은 이미 변환 했으므로
                    sound.FileName = Path.ChangeExtension(sound.FileName, ".wav")
                End If
                
                Dim trimmedSoundFileName As String = $"ucv_ac_ts_{(i + 1).ToString(padZeroFormat)}.wav"
                Dim abletonSoundFilePath As String = $"{ABLETON_SOUNDS_PATH}\{sound.FileName}"
                Dim trimmedSoundFilePath As String = $"{ABLETON_SOUNDS_PATH}\{trimmedSoundFileName}"

                Sound_Cutting.TrimWavFile(abletonSoundFilePath, trimmedSoundFilePath, sound.StartTime, sound.EndTime)
                sound.FileName = trimmedSoundFileName

                If showLoadingMessage Then
                    Invoke(Sub()
                        Loading.DLb.Text = String.Format(My.Resources.Contents.Sound_Verifying, i + 1, trimmedSoundList.Count)
                           End Sub)
                End If
            Next

            If showLoadingMessage Then
                Invoke(Sub()
                    Loading.DLb.Text = String.Format(My.Resources.Contents.Sound_Verifying, 0, sortedSoundList.Count)
                       End Sub)
            End If

            For i = 0 To sortedSoundList.Count - 1
                Dim sound As KeySoundStructure = sortedSoundList(i)

                If Path.GetExtension(sound.FileName) <> ".wav" Then 'mp3 파일은 이미 변환 했으므로
                    sound.FileName = Path.ChangeExtension(sound.FileName, ".wav")
                End If

                Dim abletonSoundFilePath As String = $"{ABLETON_SOUNDS_PATH}\{sound.FileName}"
                Dim unipackSoundFilePath As String = $"{UNIPACK_SOUNDS_PATH}\{sound.FileName}"

                If Not File.Exists(abletonSoundFilePath) Then
                    errorMessage.Append($"Error occured while converting to keySound: '{sound.FileName}' file doesn't exists.")
                    Continue For
                End If
                If Not Directory.Exists(UNIPACK_SOUNDS_PATH) Then
                    Directory.CreateDirectory(UNIPACK_SOUNDS_PATH)
                End If

                Dim key As String = sound.ToString()
                Debug.WriteLine(key)

                If previousChain <> 0 AndAlso previousChain <> sound.Chain Then
                    keySound.Append(Environment.NewLine)
                End If
                
                keySound.Append(key)
                keySound.Append(Environment.NewLine)

                File.Copy(abletonSoundFilePath, unipackSoundFilePath, True)

                previousChain = sound.Chain

                If showLoadingMessage Then
                    Invoke(Sub()
                        Loading.DLb.Text = String.Format(My.Resources.Contents.KeySound_Creating, i + 1, sortedSoundList.Count)
                           End Sub)
                End If
             Next
            
            keySound.Length -= 1
            Dim content As String = keySound.ToString()

            File.WriteAllText(UNIPACK_KEYSOUND_PATH, content)
                       End Sub)

        If showLoadingMessage Then
            Invoke(Sub()
                Loading.Close()
                   End Sub)
        End If

        Return errorMessage.ToString()
    End Function

    ''' <summary>
    ''' 자동 변환 (KeyLED MIDEX)
    ''' </summary>
    Public Async Function ReadyForAutoConvertForKeyLED() As Task
        If abl_openedproj AndAlso abl_openedled Then
            btnConvertKeyLEDAutomatically.Enabled = True

            If AutoConvert.Checked Then
                Dim errorMessage As String = Await ReadyForConvertKeyLEDForMIDEX(True)

                If Not String.IsNullOrWhiteSpace(errorMessage) Then
                    MessageBox.Show(String.Format(My.Resources.Contents.LED_Converting_Error, errorMessage), Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                MessageBox.Show(My.Resources.Contents.KeyLED_Created, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Function

    Private Async Function ReadyForConvertKeyLEDForMIDEX(showLoadingMessage As Boolean) As Task(Of String)
        Dim errorMessage As String = String.Empty
        Dim ledConversion As Task(Of KeyLEDStructure()) = Task.Run(Function() As KeyLEDStructure()
            Return ConvertKeyLEDForMIDEX_v2(ABLETON_PROJECT_XML_PATH, errorMessage, True)
        End Function)

        Dim keyLEDs As KeyLEDStructure() = Await ledConversion

        If showLoadingMessage Then
            Invoke(Sub()
                Loading.Show()
                Loading.Text = My.Resources.Contents.KeyLED_Creating_Title
                Loading.DLb.Text = String.Format(My.Resources.Contents.KeyLED_Creating, 0, keyLEDs.Length)
                   End Sub)
        End If

        Await Task.Run(Sub()
            For i = 0 To keyLEDs.Length - 1
                Dim led As KeyLEDStructure = keyLEDs(i)
                SaveKeyLED(led)

                If showLoadingMessage Then
                    Invoke(Sub()
                        Loading.DLb.Text = String.Format(My.Resources.Contents.KeyLED_Creating, i + 1, keyLEDs.Length)
                           End Sub)
                End If
            Next

            If showLoadingMessage Then
                Invoke(Sub()
                    Loading.Close()
                       End Sub)
            End If
        End Sub)

        Return errorMessage
    End Function

    Private Async Sub OpenAbletonProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenAbletonProjectToolStripMenuItem.Click
        Dim alsOpen1 As New OpenFileDialog
        alsOpen1.Filter = My.Resources.Contents.Project_ofd_Filter
        alsOpen1.Title = My.Resources.Contents.Project_ofd_Title
        alsOpen1.AddExtension = False
        alsOpen1.Multiselect = False

        If alsOpen1.ShowDialog() = DialogResult.OK Then
            Await Task.Run(Sub()
                OpenAbletonProjectFile(alsOpen1.FileName, True)
                     End Sub)

            Await ReadyForAutoConvertForKeySound()
            Await ReadyForAutoConvertForKeyLED()
        End If
    End Sub

    Private Sub ConvertALSToUnipackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertALSToUnipackToolStripMenuItem.Click

        Dim sfd As New SaveFileDialog()
        sfd.Filter = My.Resources.Contents.Project_sfd_Filter
        sfd.Title = My.Resources.Contents.Project_sfd_Title
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
    ''' <param name="showMessage">메시지 박스를 표시할지의 여부</param>
    Public Sub UniPack_SaveInfo(showMessage As Boolean)
        Try
            If abl_openedproj Then
                If Not Directory.Exists(UNIPACK_PROJECT_PATH) Then
                    Directory.CreateDirectory(UNIPACK_PROJECT_PATH)
                End If

                UniPack_Title = infoTB1.Text
                UniPack_ProducerName = infoTB2.Text
                UniPack_Chains = Integer.Parse(infoTB3.Text)

                File.WriteAllText(UNIPACK_INFO_PATH, String.Format("title={0}{1}buttonX=8{1}buttonY=8{1}producerName={2}{1}chain={3}{1}squareButton=true", infoTB1.Text, vbNewLine, infoTB2.Text, infoTB3.Text))
                infoIsSaved = True

                If showMessage Then
                    MessageBox.Show(My.Resources.Contents.UniPack_Info_Saved, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show(My.Resources.Contents.Project_Not_Opened, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        Dim strs As String() = {}

        If inputstr.Contains(vbCr) AndAlso Not inputstr.Contains(vbLf) Then 'Macintosh (Cr)
            strs = inputstr.Split(vbCr)
        ElseIf Not inputstr.Contains(vbCr) AndAlso inputstr.Contains(vbLf) Then 'Linux (Lf)
            strs = inputstr.Split(vbLf)
        Else 'Windows (CrLf)
            strs = inputstr.Split(new String() {Environment.NewLine}, StringSplitOptions.None)
        End If

        Return strs
    End Function

    Private Sub OpenSounds(fileNames As String(), showLoadingMessage As Boolean)
        If fileNames.Length > 0 Then
            If showLoadingMessage Then
                Invoke(Sub()
                       Loading.Show()
                       Loading.DPr.Maximum = FileNames.Length

                       Loading.Text = My.Resources.Contents.Sound_Open_Title
                       Loading.DLb.Text = String.Format(My.Resources.Contents.Sound_Open, 0, fileNames.Length)
                   End Sub)
            End If

            If Directory.Exists(ABLETON_SOUNDS_PATH) Then
                Directory.Delete(ABLETON_SOUNDS_PATH, True)
            End If

            Directory.CreateDirectory(ABLETON_SOUNDS_PATH)

            Dim toConvertSoundList As New List(Of String)

            For i = 0 To fileNames.Length - 1
                Dim fileName As String = fileNames(i)
                Dim fileExtension As String = Path.GetExtension(fileName)

                If fileExtension = ".wav" Then
                    File.Copy(fileName, $"{ABLETON_SOUNDS_PATH}\{Path.GetFileName(fileName)}", True)
                    
                    If showLoadingMessage Then
                        Invoke(Sub()
                               Loading.DPr.Style = ProgressBarStyle.Continuous
                               Loading.DPr.Value += 1
                               
                               Loading.DLb.Text = String.Format(My.Resources.Contents.Sound_Open, Loading.DPr.Value, fileNames.Length)
                           End Sub)
                    End If
                ElseIf fileExtension = ".mp3" Then
                    toConvertSoundList.Add(fileName)
                End If
            Next

            For i = 0 To toConvertSoundList.Count - 1
                Dim filePath As String = toConvertSoundList(i)
                Dim fileName As String = Path.GetFileName(filePath)
                Dim destPath As String = $"{ABLETON_SOUNDS_PATH}\{Path.GetFileName(Path.ChangeExtension(filePath, ".wav"))}"

                Sound_Cutting.Mp3ToWav(filePath, destPath)

                If showLoadingMessage Then
                    Invoke(Sub()
                        Loading.DPr.Value += 1
                        Loading.DLb.Text = String.Format(My.Resources.Contents.Sound_Open, Loading.DPr.Value, fileNames.Length)
                    End Sub)
                End If
            Next

            abl_openedsnd = True
            SoundIsSaved = True

            If showLoadingMessage Then
                Invoke(Sub()
                    Loading.Close()
                End Sub)

                MessageBox.Show(My.Resources.Contents.Sound_Loaded, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
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

    Private Async Sub MainProject_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If IsUpdated Then
            Exit Sub
        End If

        If Not IsSaved Then
            Dim result As DialogResult = MessageBox.Show(My.Resources.Contents.UniPack_Not_Saved, My.Resources.Contents.UniPack_Not_Saved_Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                Await Task.Run(Sub()
                    SaveProject(False)
                               End Sub)

            ElseIf result = DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' 프로젝트를 저장 합니다.
    ''' </summary>
    ''' <param name="wait">기다릴지의 여부</param>
    Public Sub SaveProject(wait As Boolean)
        If abl_openedproj AndAlso (abl_openedsnd OrElse abl_openedled) Then
            Dim sfd As New SaveFileDialog()
            Invoke(Sub()
                sfd.Title = My.Resources.Contents.Project_sfd_Title
                sfd.Filter = My.Resources.Contents.Project_sfd_Filter
                sfd.FileName = UniPack_Title
                   End Sub)

            Dim sfdResult As DialogResult = Nothing

            Invoke(Sub()
                       sfdResult = sfd.ShowDialog()
                   End Sub)

            If sfdResult = DialogResult.OK Then
                If Directory.Exists(UNIPACK_PROJECT_PATH) Then
                    If wait Then
                        Invoke(Sub()
                            Loading.Show()
                            Loading.Text = My.Resources.Contents.Project_Saving_Title
                            Loading.DLb.Text = My.Resources.Contents.Project_Saving
                               End Sub)
                    End If

                    If File.Exists(sfd.FileName) Then
                        File.Delete(sfd.FileName)
                    End If

                    ZipFile.CreateFromDirectory(UNIPACK_PROJECT_PATH, sfd.FileName)
                    IsSaved = True

                    If wait Then
                        Invoke(Sub()
                            Loading.Close()
                               End Sub)

                        MessageBox.Show(My.Resources.Contents.Project_Saved, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    MessageBox.Show(My.Resources.Contents.Project_Not_Converted, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End If
    End Sub
#Region "Save Project Method (Deprecated)"
    ''' <summary>
    ''' MainProject에서 프로젝트를 저장합니다.
    ''' </summary>
    ''' <param name="Waiting">기다릴까?</param>
    <Obsolete("This method is deprecated. Please use 'SaveProject' method.")>
    Public Sub Save2Project(Waiting As Boolean)
        Try
            Dim IsHaveProject As Boolean = False
            Dim IsHaveSound As Boolean = False
            Dim IsHaveLED As Boolean = False
            Invoke(Sub()
                   IsHaveProject = abl_openedproj
                   IsHaveSound = abl_openedsnd
                   IsHaveLED = abl_openedled
               End Sub)

            If IsHaveProject AndAlso IsHaveSound Or IsHaveProject AndAlso IsHaveLED Then
                Dim infoTitle As String = String.Empty
                infoTitle = File.ReadAllLines(Application.StartupPath & "\Workspace\unipack\info")(0).Replace("title=", "")

                Dim sfd As New SaveFileDialog()
                Dim aN As DialogResult

                Invoke(Sub()
                    sfd.Title = My.Resources.Contents.Project_sfd_Title
                    sfd.Filter = My.Resources.Contents.Project_sfd_Filter
                       End Sub)
                sfd.FileName = infoTitle
                sfd.AddExtension = False

                Invoke(Sub()
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
    #End Region

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

    Private Async Sub OpenKeyLEDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenKeyLEDToolStripMenuItem.Click
        Try
            ofd.Multiselect = True
            ofd.Title = My.Resources.Contents.LED_ofd_Title
            ofd.Filter = My.Resources.Contents.LED_ofd_Filter

            If ofd.ShowDialog() = DialogResult.OK Then
                If keyLED_Edit.Visible Then
                    keyLED_Edit.Close()
                End If
                Await Task.Run(Sub()
                    OpenKeyLED(ofd.FileNames, True)
                               End Sub)
                Await ReadyForAutoConvertForKeyLED()
                keyLEDMIDEX_BetaButton.Enabled = True
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

#Region "Pitch2XY (Deprecated On Unitor v3.1.2.1)"
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

    ''' <summary>
    ''' 종합 KeySound 변환 함수 (Version 2)
    ''' </summary>
    ''' <param name="apfPath">에이블톤 프로젝트 파일 경로</param>
    ''' <param name="err">오류 메시지</param>
    ''' <param name="showLoadingMessage">로딩 메시지 존재의 여부</param>
    Public Function ConvertKeySound_v2(apfPath As String, ByRef err As String, showLoadingMessage As Boolean) As KeySoundStructure()
        Dim errSb As New StringBuilder(255)

        If showLoadingMessage Then
            Invoke(Sub()
                Loading.Show()
                Loading.Text = My.Resources.Contents.Sound_Converting
                Loading.DLb.Text = My.Resources.Contents.Sound_Converting
                   End Sub)
        End If
        
        Dim doc As New XmlDocument()
        doc.Load(apfPath)

        Dim setNode As XmlNodeList = doc.GetElementsByTagName("InstrumentBranch")
        Dim keySoundList As New List(Of KeySoundStructure) '최종 배열 반환 리스트

        Dim soundList As New List(Of SoundNodeList) '최종 노드 배열
        Dim nodeListInNode As List(Of SoundNodeList) = Nothing 'For문을 돌면서 LEDList에 Node를 넣을 배열

        Dim instrumentBranchList As New List(Of List(Of InstrumentBranch))(GetInstrumentBranches(setNode))

        If showLoadingMessage Then
            Invoke(Sub()
                Loading.DLb.Text = String.Format(My.Resources.Contents.Sound_Verifying, 0, instrumentBranchList.Count)
                   End Sub)
        End If

        For i = 0 To instrumentBranchList.Count - 1
            Dim branches As List(Of InstrumentBranch) = instrumentBranchList(i)
            nodeListInNode = soundList

            For j = 0 To branches.Count - 1
                Dim branch As InstrumentBranch = branches(j)

                Dim branchesInNodeList As List(Of SoundNodeList)
                branchesInNodeList = nodeListInNode.Where(Function(x) x.Name = "InstrumentBranch" AndAlso x.Id = branch.Id).ToList()

                If branchesInNodeList.Count = 0 Then '추가
                    Dim nodeList As New SoundNodeList("InstrumentBranch", branch.Id, branch.Node)

                    Dim drumRack As XmlNode = branch.Node.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("DrumGroupDevice")

                    If Not IsNothing(drumRack) Then
                        Dim drumBranches As XmlNodeList = drumRack.Item("Branches")?.SelectNodes("DrumBranch")

                        If Not IsNothing(drumBranches) Then
                            For k = 0 To drumBranches.Count - 1 'Drum Branches
                                Dim drumBranch As XmlNode = drumBranches(k)
                                Dim drumBranchId As Integer = Integer.Parse(drumBranch.Attributes("Id").Value)

                                Dim drumNode As New SoundNodeList("DrumBranch", drumBranchId, drumBranch)
                                nodeList.NodeList.Add(drumNode)
                            Next
                        End If
                    End If

                    If Not IsNothing(branch.DrumBranch) Then 'Drum Branch
                        Dim drumBranchId As Integer = Integer.Parse(branch.DrumBranch.Attributes("Id").Value)
                        Dim drumBranchList As List(Of SoundNodeList) = nodeListInNode.Where(Function(x) x.Name = "DrumBranch" AndAlso x.Id = drumBranchId).ToList()

                        If drumBranchList.Count = 0 Then '추가
                            Dim drumBranchNode As New SoundNodeList("DrumBranch", drumBranchId, branch.DrumBranch)

                            nodeListInNode.Add(drumBranchNode)
                            nodeListInNode = drumBranchNode.NodeList
                        Else '존재 하는 경우
                            nodeListInNode = drumBranchList(0).NodeList
                        End If
                    End If

                    If Not IsNothing(branch.InstrumentRack) Then 'Instrument Rack 추가
                        Dim instrumentRackId As Integer = Integer.Parse(branch.InstrumentRack.Attributes("Id").Value)
                        Dim instrumentRackList As List(Of SoundNodeList) = nodeListInNode.Where(Function(x) x.Name = "InstrumentRack" AndAlso x.Id = instrumentRackId).ToList()

                        If instrumentRackList.Count = 0 Then
                            Dim nodeInstrumentRack As New SoundNodeList("InstrumentRack", instrumentRackId, branch.InstrumentRack)
                            nodeListInNode.Add(nodeInstrumentRack)
                        End If
                    End If

                    nodeListInNode.Add(nodeList)
                    nodeListInNode = nodeList.NodeList
                Else '존재 하는 경우
                    Dim firstBranch As SoundNodeList = branchesInNodeList(0)
                    nodeListInNode = firstBranch.NodeList
                End If
            Next

            If showLoadingMessage Then
                Invoke(Sub()
                    Loading.DLb.Text = String.Format(My.Resources.Contents.Sound_Verifying, i + 1, instrumentBranchList.Count)
                       End Sub)
            End If
        Next

        If showLoadingMessage Then
            Invoke(Sub()
                Loading.Text = My.Resources.Contents.Sound_Converting
                   End Sub)
        End If

        If soundList.Count > 0 Then
            'Chain 유효성 검사 (with InstrumentRack)
            Dim chain = 1
            Dim drumBranch As XmlNode = Nothing
            Dim mm As MultiMapping = MultiMapping.Empty

            Dim needToExit = False
            Dim checkChainAction As Action(Of SoundNodeList, List(Of SoundNodeList)) = Sub(node As SoundNodeList, parentNode As List(Of SoundNodeList))
               If needToExit Then
                   Return
               End If

               If node.Name <> "InstrumentRack" Then
                   Dim isRealChain = False '현재 체인을 바꿀 수 있는 체인인가?
                   Dim instrumentRack As List(Of SoundNodeList) = parentNode.Where(Function(x) x.Name = "InstrumentRack").ToList()

                    If instrumentRack.Count > 0 Then
                        Dim macroControl As XmlNode = instrumentRack.First().Node.Item("MacroControls.0")
                        Dim keyMidi As XmlNode = macroControl.Item("KeyMidi")

                        If Not IsNothing(keyMidi) Then
                            Dim lowerRangeNote As Integer = Integer.Parse(keyMidi.Item("LowerRangeNote").GetAttribute("Value"))
                            Dim upperRangeNote As Integer = Integer.Parse(keyMidi.Item("UpperRangeNote").GetAttribute("Value"))

                            If upperRangeNote - lowerRangeNote = 7 Then 'Chain Selector 부분
                                isRealChain = True
                            ElseIf upperRangeNote = -1 AndAlso lowerRangeNote = -1 Then 'Chain Selector Map (Beta)
                                isRealChain = True
                            End If
                        End If
                    End If

                   Dim sounds As KeySoundStructure() = GetKeySoundsFromInstrumentBranch(node, drumBranch, mm)
                    
                    For Each sound In sounds
                        If isRealChain Then
                            chain = sound.Chain
                        End If

                       sound.Chain = chain
                       keySoundList.Add(sound)
                    Next

                   'NextOfNext InstrumentRack
                   If sounds.Length = 0 AndAlso node.NodeList.Count > 0 AndAlso isRealChain Then
                       chain = Integer.Parse(node.Node.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1
                   End If
               End If
                                                               End Sub

            GetSoundNodeInLoop(soundList, checkChainAction)

            If showLoadingMessage Then
                Invoke(Sub()
                    Loading.Close()
                       End Sub)

                MessageBox.Show(My.Resources.Contents.Sound_Converted, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Else
            If showLoadingMessage Then
                Invoke(Sub()
                    Loading.Close()
                       End Sub)

                MessageBox.Show(My.Resources.Contents.Sound_Not_Found, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

        err = errSb.ToString()
        Return keySoundList.ToArray()
    End Function

    Public Shared Function GetKeySoundsFromInstrumentBranch(node As SoundNodeList, ByRef drumBranch As XmlNode, ByRef mm As MultiMapping) As KeySoundStructure()
        Dim soundList As New List(Of KeySoundStructure)
        
        Try
            Dim isRandom = False '멀티매핑 인가?

            If node.Name = "DrumBranch" Then
                drumBranch = node.Node
            End If

            If mm.CurrentCount > 0 Then
                isRandom = True
                mm.CurrentCount -= 1
            Else
                isRandom = False
                mm = MultiMapping.Empty
            End If

            If Not isRandom Then 'MidiRandom 테스트
                Try
                    Dim midiRandomNode As XmlNode = node.Node.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("MidiRandom")

                    If Not IsNothing(midiRandomNode) Then
                        Dim isActive As Boolean = Boolean.Parse(midiRandomNode.Item("On").Item("Manual").GetAttribute("Value"))
                            
                        If isActive Then
                            Dim choices As Integer = Integer.Parse(midiRandomNode.Item("Choices").Item("Manual").GetAttribute("Value"))
                            Dim randomMinNoteNumber = 0
                            Dim randomMaxNoteNumber = 0

                            If node.Name = "DrumBranch" Then
                                Dim noteNumber As Integer = GetNoteNumberFromDrumBranch(node.Node)
                                randomMinNoteNumber = noteNumber
                                randomMaxNoteNumber = noteNumber

                            ElseIf node.Name = "InstrumentBranch" Then
                                Dim p As Footprint(Of Integer) = GetNoteNumberFromInstrumentBranch(node.Node)
                                randomMinNoteNumber = p.Start
                                randomMaxNoteNumber = p.End

                            End If

                            mm.CurrentCount = choices
                            mm.NoteNumber = New Footprint(Of Integer)(randomMinNoteNumber, randomMaxNoteNumber)

                            Return {}
                        End If
                    End If

                Catch ex As NullReferenceException
                    '정상적인 DrumBranch임.
                End Try
            End If

            '사운드 가져오기 (with OriginalSimpler)
            Dim soundName As String = String.Empty
            Dim minChain = 1
            Dim maxChain = 1
            Dim minNoteNumber As Integer = -1
            Dim maxNoteNumber As Integer = -1
            Dim noteNumberMethod As String = "keySound" 'keySound / keyLED
            Dim loopNumber = 1

            Dim startTime As TimeSpan = TimeSpan.Zero
            Dim endTime As TimeSpan = TimeSpan.Zero

            Dim originalSimpler As XmlNode = node.Node.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("OriginalSimpler")

            If Not IsNothing(originalSimpler) Then
                Dim multiSamplePart As XmlNode = originalSimpler.Item("Player").Item("MultiSampleMap").Item("SampleParts").Item("MultiSamplePart")
                soundName = multiSamplePart.Item("SampleRef").Item("FileRef").Item("Name").GetAttribute("Value")

                'Trimming Sound
                Dim fp As Footprint(Of TimeSpan) = GetTimeForTrimmingSound(multiSamplePart)
                Dim filePath As String = $"{ABLETON_SOUNDS_PATH}\{Path.ChangeExtension(soundName, ".wav")}"

                If (fp.Start <> TimeSpan.Zero OrElse fp.End <> TimeSpan.Zero) AndAlso File.Exists(filePath) Then
                    Dim waveFile As New WaveFileReader(filePath)
                    
                    If fp.Start <> TimeSpan.Zero OrElse Math.Abs(Convert.ToInt32(Math.Truncate(waveFile.TotalTime.TotalSeconds)) - Convert.ToInt32(Math.Truncate(fp.End.TotalSeconds))) >= 1 Then
                        startTime = fp.Start
                        endTime = fp.End
                    End If

                    waveFile.Dispose()
                End If
            End If

            'Get Chain (Instrument Branch Only)
            If node.Name = "InstrumentBranch" Then
                minChain = Integer.Parse(node.Node.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1
                maxChain = Integer.Parse(node.Node.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1
            End If

            Dim branchInfo As XmlNode = drumBranch?.Item("BranchInfo")

            'Get Position
            If Not IsNothing(branchInfo) Then
                Dim noteNumber As Integer = Integer.Parse(branchInfo.Item("ReceivingNote").GetAttribute("Value"))
                
                minNoteNumber = noteNumber
                maxNoteNumber = noteNumber

            ElseIf node.Name = "InstrumentBranch" Then
                Dim p As Footprint(Of Integer) = GetNoteNumberFromInstrumentBranch(node.Node)

                minNoteNumber = p.Start
                maxNoteNumber = p.End

            End If

            '성공
            If Not String.IsNullOrWhiteSpace(soundName) AndAlso minNoteNumber <> -1 AndAlso maxNoteNumber <> -1 Then
                For chain = minChain To maxChain
                    For noteNumber = minNoteNumber To maxNoteNumber
                        Dim x As Integer = -1
                        Dim y As Integer = -1

                        Select Case noteNumberMethod 'NoteNumber To X / Y
                            Case "keySound"
                                Dim ksPoint As ksX = A2U.keySound.GetkeySound(ks_NoteEvents.NoteNumber_1, noteNumber)
                                x = ksPoint.x
                                y = ksPoint.y

                            Case "keyLED"
                                x = A2U.keyLED_MIDEX.GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, noteNumber)
                                y = A2U.keyLED_MIDEX.GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, noteNumber)
                        
                        End Select

                        If x <> - 1 AndAlso y <> - 1 Then
                            Dim sound As New KeySoundStructure(chain, x, y, soundName, loopNumber)
                            
                            'Trimming Sound
                            If startTime <> TimeSpan.Zero OrElse endTime <> TimeSpan.Zero Then
                                sound.StartTime = startTime
                                sound.EndTime = endTime
                            End If

                            soundList.Add(sound)
                        End if
                    Next
                Next
            End If

        Catch ex As NullReferenceException
            '?
        End Try

        Return soundList.ToArray()
    End Function

    Public Shared Function GetInstrumentBranches(branchNode As XmlNodeList) As List(Of List(Of InstrumentBranch))
        Dim instrumentBranchList As New List(Of List(Of InstrumentBranch))

        For i  = 0 To branchNode.Count - 1
            Dim soundNode As XmlNode = branchNode(i)
            Dim xpaths As String() = GetXpathsForKeySound(GetXpathForXml(soundNode))

            Dim branches As New List(Of InstrumentBranch)
            Dim nodeInNode As XmlNode = soundNode

            For j = 0 To xpaths.Length - 1
                If nodeInNode.Name = "InstrumentBranch" Then
                    Dim id As Integer = Integer.Parse(nodeInNode.Attributes("Id").Value)
                    Dim branch As New InstrumentBranch(id, nodeInNode)

                    If nodeInNode.ParentNode.ParentNode.Name = "InstrumentGroupDevice" Then 'Instrument Rack
                        branch.InstrumentRack = nodeInNode.ParentNode.ParentNode
                    End If
                    If nodeInNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.Name = "DrumBranch" Then 'Drum Branch
                        branch.DrumBranch = nodeInNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode
                    End If

                    branches.Add(branch)
                End If

                nodeInNode = nodeInNode.ParentNode
            Next

            branches.Reverse()
            instrumentBranchList.Add(branches)
        Next

        Return instrumentBranchList
    End Function

    ''' <summary>
    ''' Get Time (Trimming Sound)
    ''' </summary>
    ''' <param name="node">MultiSamplePart (OriginalSimpler)</param>
    ''' <returns></returns>
    Public Shared Function GetTimeForTrimmingSound(node As XmlNode) As Footprint(Of TimeSpan)
        Dim fp As New Footprint(Of TimeSpan)(TimeSpan.Zero, TimeSpan.Zero)

        'Node (MultiSamplePart)
        Dim sampleStart As Long = Long.Parse(node.Item("SampleStart").GetAttribute("Value"))
        Dim sampleEnd As Long = Long.Parse(node.Item("SampleEnd").GetAttribute("Value"))

        fp.Start = A2U.keySound.SampleTimeToTime(sampleStart)
        fp.End = A2U.keySound.SampleTimeToTime(sampleEnd)

        Return fp
    End Function

    Public Shared Function GetXpathsForKeySound(xpath As String) As String()
        Dim xpaths As String() = xpath.TrimStart("/").Split("/")
        Dim xpathList As New List(Of String)

        Dim needToAdd As Boolean = False

        For i = 0 To xpaths.Count() - 1
            If Not needToAdd AndAlso xpaths(i) = "InstrumentBranch" Then
                needToAdd = True
            End If
            If Not needToAdd Then
                Continue For
            End If

            xpathList.Add(xpaths(i))
        Next
        
        Return xpathList.ToArray()
    End Function

    Public Shared Function GetNoteNumberFromDrumBranch(node As XmlNode) As Integer
        Return Integer.Parse(node.Item("BranchInfo").Item("ReceivingNote").GetAttribute("Value"))
    End Function

    Public Shared Function GetNoteNumberFromInstrumentBranch(node As XmlNode) As Footprint(Of Integer)
        Return New Footprint(Of Integer)(Integer.Parse(node.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value")), Integer.Parse(node.Item("ZoneSettings").Item("KeyRange").Item("Max").GetAttribute("Value")))
    End Function

    Public Shared Sub GetSoundNodeInLoop(nodes As IEnumerable(Of SoundNodeList), doAction As Action(Of SoundNodeList, List(Of SoundNodeList)), Optional indent As Integer = 0)
        For Each root In nodes
            doAction(root, nodes)
            GetSoundNodeInLoop(root.NodeList, doAction, indent + 1)
        Next
    End Sub
#Region "KeySound Conversion (Deprecated, v1)"
    <Obsolete("This method is deprecated, use ConvertKeySound_v2() instead.")>
    Private Sub ConvertKeySound_DeprecatedVersion()
        '이 함수의 코드들은 스파게티 코드여서,
        '제작자인 저도 알아볼 수가 없습니다.
        '또한 이 함수에는 더 이상 새롭거나 수정된 코드가 없을 것입니다.

        '그대신 새롭게 짠 ConvertKeySound_v2() 함수를 이용해주시기 바랍니다.

        Try
            If IsWorking = False AndAlso abl_openedproj AndAlso abl_openedsnd Then
                IsWorking = True

                If Directory.Exists(Application.StartupPath & "\Workspace\unipack\sounds") Then
                    Invoke(Sub()
                        Loading.DLb.Text = My.Resources.Contents.Project_DeletingTempoaryFiles
                       End Sub)

                    Directory.Delete(UNIPACK_SOUNDS_PATH, True)
                    Directory.CreateDirectory(UNIPACK_SOUNDS_PATH)
                Else
                    Directory.CreateDirectory(UNIPACK_SOUNDS_PATH)
                End If

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
                Dim rnd As Integer = 1 'MidiRandom (랜덤)이 몇 개가 있는가?
                Dim Choices As Integer = 0 '매우 정확한 랜덤의 수. (from MidiRandom, 최종 랜덤)
                Dim curid As Integer = 1 '지금 현재 무슨 랜덤을 선언 하고 있나? (index)
                Dim realCh As Integer = 0 'Choices랑 같음 (다중매핑 index, 그런데 현재 Choices랑 다른 점을 못찾겠음. 그 때는 진짜 IQ 200 넘었었나;)

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
                    Dim FinalSoundName As String = x.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("OriginalSimpler").Item("Player").Item("MultiSampleMap").Item("SampleParts").Item("MultiSamplePart").Item("SampleRef").Item("FileRef").Item("Name").GetAttribute("Value")
                    Dim Chain As Integer = Integer.Parse(x.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1

                    If False Then 'Not splitSounds.Count = 0 Then
                        Try
                            Dim StartTime As TimeSpan = TimeSpan.Zero 'sLToTime(Convert.ToInt64(x.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("OriginalSimpler").Item("Player").Item("MultiSampleMap").Item("SampleParts").Item("MultiSamplePart").Item("SampleStart").GetAttribute("Value")))
                            Dim EndTime As TimeSpan = TimeSpan.Zero 'sLToTime(Convert.ToInt64(x.Item("DeviceChain").Item("MidiToAudioDeviceChain").Item("Devices").Item("OriginalSimpler").Item("Player").Item("MultiSampleMap").Item("SampleParts").Item("MultiSamplePart").Item("SampleEnd").GetAttribute("Value")))

                            Dim key As String = StartTime.TotalMilliseconds & "-" & EndTime.TotalMilliseconds & ".wav"
                            If False Then 'splitSounds.Contains(key) Then
                                FinalSoundName = key
                            End If

                        Catch exN As NullReferenceException '없는걸로...
                        End Try
                    End If

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
                        Debug.WriteLine(String.Format("{0}: {1} {2} {3}", FinalSoundName, Chain, ks.x, ks.y))
                    Else
                        Debug.WriteLine(String.Format("{0}: {1} ~ {2} {3} {4}", FinalSoundName, Chain, MaxChain, ks.x, ks.y))
                    End If
#End Region

                    File.Copy(Application.StartupPath & "\Workspace\ableproj\sounds\" & FinalSoundName, Application.StartupPath & "\Workspace\unipack\sounds\" & FinalSoundName, True)
                    If String.IsNullOrWhiteSpace(str) Then
                        If lpn = False Then
                            str &= String.Format("{0} {1} {2} {3}", Chain, ks.x, ks.y, FinalSoundName)
                        Else
#Region "체인 ~ 최대 체인 변환"
                            For Ci As Integer = Chain To MaxChain
                                str &= String.Format("{0} {1} {2} {3}", Ci, ks.x, ks.y, FinalSoundName)
                            Next
#End Region
                        End If
                    Else
                        If lpn = False Then
                            str &= vbNewLine & String.Format("{0} {1} {2} {3}", Chain, ks.x, ks.y, FinalSoundName)
                        Else
#Region "체인 ~ 최대 체인 변환"
                            For Ci As Integer = Chain To MaxChain
                                str &= vbNewLine & String.Format("{0} {1} {2} {3}", Ci, ks.x, ks.y, FinalSoundName)
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
#End Region
#Region "Trimming Sounds (Deprecated, v1)"
    <Obsolete("This method is deprecated, use ConvertKeySound_v2() instead.")>
    Private Sub TrimSounds_DeprecatedVersion() 
        Try
            If IsWorking = False AndAlso abl_openedproj AndAlso abl_openedsnd Then
                IsWorking = True

                Invoke(Sub()
                    Loading.DLb.Text = My.Resources.Contents.Project_DeletingTempoaryFiles
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

                Dim ablprj As String = Application.StartupPath & "\Workspace\ableproj\abl_proj.xml"
                Dim doc As New XmlDocument
                Dim setNode As XmlNodeList
                Dim err As New StringBuilder(255)

                doc.Load(ablprj)
                setNode = doc.GetElementsByTagName("InstrumentBranch")

                '에이블톤 sounds Crop 길이는 InstrumentBranch > DeviceChain > MidiToAudioDeviceChain > 
                'Devices > OriginalSimpler > Player > MultiSampleMap > SampleParts > MultiSamplePart > 
                'SampleEnd Value - SampleStart Value에 있습니다.

                Dim il As Integer = 1 '로딩 폼 value.
                Dim trName As String = String.Empty 'Trim할 때 쓰는 이름.
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

                    Dim StartTime As TimeSpan = TimeSpan.Zero 'sLToTime(ssTime)
                    Dim EndTime As TimeSpan = TimeSpan.Zero 'sLToTime(seTime)
                    trName = Convert.ToInt32(StartTime.TotalMilliseconds) & "-" & Convert.ToInt32(EndTime.TotalMilliseconds)

                    If File.Exists(Application.StartupPath & "\Workspace\ableproj\sounds\" & sndName) = False Then
                        err.Append(vbNewLine)
                        err.Append("File '")
                        err.Append(sndName)
                        err.Append("' doesn't exists.")

                        Continue For
                    End If

                    If sndName.Contains(".mp3") Then
                        sndName = sndName.Replace(".mp3", ".wav") '이미 파일을 불러왔을 때 변환이 되었으니 replace.
                    End If

                    If New WaveFileReader(Application.StartupPath & "\Workspace\ableproj\sounds\" & sndName).TotalTime.TotalMilliseconds - 30 <= EndTime.TotalMilliseconds AndAlso StartTime.TotalMilliseconds <= 30 Then
                        Continue For '오차 ±30ms 보정 후 넘겨!
                    End If

                    Sound_Cutting.TrimWavFile(Application.StartupPath & "\Workspace\ableproj\sounds\" & sndName, Application.StartupPath & "\Workspace\TmpSound\" & trName & ".wav", StartTime, EndTime)
                    'splitSounds.Add(trName & ".wav")
                    Debug.WriteLine(sndName & " : " & trName & ".wav, " & StartTime.TotalMilliseconds & " - " & EndTime.TotalMilliseconds)
                    il += 1
                Next

                Loading.DLb.Left -= 40
                Dim files As String() = Directory.GetFiles(Application.StartupPath & "\Workspace\TmpSound", "*.wav")
                For i As Integer = 0 To files.Count - 1
                    File.Move(files(i), Application.StartupPath & "\Workspace\ableproj\sounds\" & Path.GetFileName(files(i)))
                Next

                Invoke(Sub()
                           Loading.Dispose()
                       End Sub)

                If Not err.Length = 0 Then
                    MessageBox.Show("Error occured when it splits the sounds automatically." & vbNewLine & err.ToString(), Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

                Dim AutoConvertBoolean As Boolean = False
                Invoke(Sub() AutoConvertBoolean = AutoConvert.Checked)

                IsWorking = False
                If abl_openedproj AndAlso abl_openedsnd AndAlso AutoConvertBoolean Then
                    'BGW_keySound.RunWorkerAsync()
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
#End Region

    ''' <summary>
    ''' 종합 keyLED (MIDEX) 변환 함수 (Midi Extension, MIDIext, Midi Fire, Lightweight 지원)
    ''' </summary>
    ''' <param name="AbletonProjectFilePath">에이블톤 프로젝트 파일 경로</param>
    ''' <param name="err">오류 메시지</param>
    ''' <param name="showLoadingMessage">로딩 메시지</param>
    Public Function ConvertKeyLEDForMIDEX_v2(abletonProjectFilePath As String, ByRef err As String, showLoadingMessage As Boolean) As KeyLEDStructure()
        '코드 종합 및 최적화 버전 (v2)

        'NextOfNext 문제점 완전히 해결

        Dim doc As New XmlDocument()
        Dim setNode As XmlNodeList

        Invoke(Sub()
            Loading.Show()

            Loading.Text = My.Resources.Contents.LED_Converting_Title
            Loading.DLb.Text = My.Resources.Contents.LED_Converting
        End Sub)

        doc.Load(AbletonProjectFilePath)
        setNode = doc.GetElementsByTagName("MidiEffectBranch")

        Dim errSb As New StringBuilder(255)
        Dim needToExit As Boolean = False

        'List In List 알고리즘으로 배열 정렬
        Dim LEDList As New List(Of LEDNodeList) '최종 노드 배열

        Dim midiEffectBranchList As New List(Of MidiEffectBranches) '최종으로 MidiEffectBranch만 갖고 올 배열
        Dim nodeListInNode As List(Of LEDNodeList) = Nothing 'For문을 돌면서 LEDList에 Node를 넣을 배열

        midiEffectBranchList.AddRange(GetMidiEffectBranches(setNode))

        Invoke(Sub()
                   Loading.Show()

                   Loading.Text = My.Resources.Contents.LED_Converting_Title
                   Loading.DLb.Text = String.Format(My.Resources.Contents.LED_Verifying, 0, midiEffectBranchList.Count)
               End Sub)

        For i = 0 To midiEffectBranchList.Count - 1
            Dim branches As MidiEffectBranches = midiEffectBranchList(i)

            nodeListInNode = LEDList

            For j = 0 To branches.MidiEffectBranchList.Count - 1
                Dim branch As MidiEffectBranch = branches.MidiEffectBranchList(j)

                Dim branchesInNodeList As List(Of LEDNodeList)
                branchesInNodeList = nodeListInNode.Where(Function(x) x.Name = "MidiEffectBranch" AndAlso x.Id = branch.Id).ToList()
                
                If branchesInNodeList.Count = 0 Then '추가
                    Dim nodeList As New LEDNodeList("MidiEffectBranch", branch.Id, branch.Node)

                    If Not IsNothing(branch.MidiEffectRack) Then 'Midi Effect Rack 추가
                        Dim midiEffectRackId As Integer = Integer.Parse(branch.MidiEffectRack.Attributes("Id").Value)
                        Dim midiEffectRackList As List(Of LEDNodeList) = nodeListInNode.Where(Function(x) x.Name = "MidiEffectRack" AndAlso x.Id = midiEffectRackId).ToList()

                        If midiEffectRackList.Count = 0 Then
                            Dim nodeMidiEffectRack As New LEDNodeList("MidiEffectRack", midiEffectRackId, branch.MidiEffectRack)
                            nodeListInNode.Add(nodeMidiEffectRack)
                        End If
                    End If

                    nodeListInNode.Add(nodeList)
                    nodeListInNode = nodeList.NodeList

                Else '존재 하는 경우
                    Dim firstBranch As LEDNodeList = branchesInNodeList(0)
                    nodeListInNode = firstBranch.NodeList
                End If
            Next

            Invoke(Sub()
                Loading.DLb.Text = String.Format(My.Resources.Contents.LED_Verifying, i + 1, midiEffectBranchList.Count)
                   End Sub)
        Next

        Invoke(Sub()
            Loading.DLb.Text = My.Resources.Contents.LED_Converting
               End Sub)

        If LEDList.Count > 0 Then
            'Chain 유효성 검사 (with MidiEffectRack)
            Dim convertedLEDList As New List(Of KeyLEDStructure)
            Dim chain = 1

            Dim pluginName As Plugins = Nothing
            Dim isFoundPlugin As Boolean = False
            Dim mm As MultiMapping = MultiMapping.Empty

            Dim checkChainAction As Action(Of LEDNodeList, List(Of LEDNodeList), Integer) = Sub(node As LEDNodeList, parentNode As List(Of LEDNodeList), indent As Integer)
                If needToExit Then
                    Return
                End If
                
                If node.Name = "MidiEffectBranch" Then
                    Dim isRealChain = False '현재 체인을 바꿀 수 있는 체인인가?

                    Dim midiEffectRack As List(Of LEDNodeList) = parentNode.Where(Function(x) x.Name = "MidiEffectRack").ToList()

                    If midiEffectRack.Count > 0 Then
                        Dim macroControl As XmlNode = midiEffectRack.First().Node.Item("MacroControls.0")
                        Dim keyMidi As XmlNode = macroControl.Item("KeyMidi")

                        If Not IsNothing(keyMidi) Then
                            Dim lowerRangeNote As Integer = Integer.Parse(keyMidi.Item("LowerRangeNote").GetAttribute("Value"))
                            Dim upperRangeNote As Integer = Integer.Parse(keyMidi.Item("UpperRangeNote").GetAttribute("Value"))

                            If upperRangeNote - lowerRangeNote = 7 Then 'Chain Selector 부분
                                isRealChain = True
                            End If
                        End If
                    End If

                    '플러그인 자동 인식
                    If Not isFoundPlugin Then
                        Try
                            Dim pluginNameInXml As String = node.Node.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect")?.Item("SourceContext")?.Item("Value")?.Item("BranchSourceContext")?.Item("OriginalFileRef")?.Item("FileRef")?.Item("Name")?.GetAttribute("Value")
                            pluginName = GetPluginForKeyLED(pluginNameInXml)

                            If pluginName = Plugins.None Then
                                isFoundPlugin = False
                            Else
                                isFoundPlugin = True
                            End If

                        Catch ex As NullReferenceException
                            isFoundPlugin = False
                            pluginName = Plugins.None
                        End Try
                    End If

                    Dim toSaveLEDList As KeyLEDStructure() = {}

                    Select Case pluginName
                        Case Plugins.MidiExtension
                            If _midiExtensionMapping.Count = 0 Then
                                GetMappingFromOfd()

                                If _midiExtensionMapping.Count = 0 Then
                                    needToExit = True
                                    Return
                                End If
                            End If

                            toSaveLEDList = ConvertKeyLEDForMidiExtension_v2(node.Node, mm, indent, _midiExtensionMapping)

                        Case Plugins.MidiExt, Plugins.MidiFire, Plugins.Lightweight
                            toSaveLEDList = ConvertKeyLEDForMidiFire_v2(node.Node, mm, indent)

                    End Select
                    
                    Dim midiEffectBranchChildNodeList As List(Of LEDNodeList) = node.NodeList.Where(Function(x) x.Name = "MidiEffectBranch").ToList()

                    If midiEffectBranchChildNodeList.Count > 0 AndAlso mm.CurrentCount > 0 AndAlso mm.Count <> midiEffectBranchChildNodeList.Count Then
                        mm.IsStrange = True 'Insufficient / Emptyspace 문제점
                        toSaveLEDList = SupportMMKeyLED(node.NodeList, mm, pluginName)
                    ElseIf midiEffectBranchChildNodeList.Count > 0 AndAlso mm.Count > 0 AndAlso mm.Count = midiEffectBranchChildNodeList.Count Then
                        toSaveLEDList = SupportMMKeyLED(node.NodeList, mm, pluginName) 'Swapping / Normal
                    Else
                        mm.IsStrange = False
                    End If

                    For Each led In toSaveLEDList
                        If isRealChain Then
                            chain = led.Chain
                        End If

                        led.Chain = chain
                        convertedLEDList.Add(led)
                    Next

                    'NextOfNext MidiEffectRack
                    If toSaveLEDList.Length = 0 AndAlso node.NodeList.Count > 0 AndAlso isRealChain Then
                        chain = Integer.Parse(node.Node.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1
                    End If
                End If
                                                                      End Sub

            GetLEDNodeInLoop(LEDList, checkChainAction)

            If showLoadingMessage Then
                Invoke(Sub()
                    Loading.Close()
                       End Sub)

                MessageBox.Show(My.Resources.Contents.LED_Converted, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            
            err = errSb.ToString()
            Return convertedLEDList.ToArray()
        Else
            Invoke(Sub()
                Loading.Close()
                   End Sub)

            MessageBox.Show(My.Resources.Contents.LED_Not_Found, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        Return {}
    End Function

    Public Shared Function GetXpathForXml(ByVal node As XmlNode) As String
        If node.Name = "#document" Then Return String.Empty
        Return GetXpathForXml(node.SelectSingleNode("..")) & "/" + If(node.NodeType = XmlNodeType.Attribute, "@", String.Empty) + node.Name
    End Function

    Public Shared Function GetXpathsForKeyLED(xpath As String) As String()
        Dim xpaths As String() = xpath.TrimStart("/").Split("/")
        Dim xpathList As New List(Of String)

        Dim needToAdd As Boolean = False

        For i = 0 To xpaths.Count() - 1
            If Not needToAdd AndAlso xpaths(i) = "MidiEffectBranch" Then
                needToAdd = True
            End If
            If Not needToAdd Then
                Continue For
            End If

            xpathList.Add(xpaths(i))
        Next
        
        Return xpathList.ToArray()
    End Function

    Public Shared Function GetMidiEffectBranches(setNode As XmlNodeList) As MidiEffectBranches()
        Dim midiEffectBranchList As New List(Of MidiEffectBranches)

        For i = 0 To setNode.Count - 1
            Dim LEDNode As XmlNode = setNode(i)
            Dim nodeXpath As String() = GetXpathsForKeyLED(GetXpathForXml(LEDNode))

            Dim branches As New MidiEffectBranches()

            Dim nodeInNode As XmlNode = LEDNode

            For j = 0 To nodeXpath.Length - 1
                If nodeInNode.Name = "MidiEffectBranch" Then
                    Dim id As Integer = Integer.Parse(nodeInNode.Attributes("Id").Value)
                    Dim branch As New MidiEffectBranch(id, nodeInNode)

                    If nodeInNode.ParentNode.ParentNode.Name = "MidiEffectGroupDevice" Then 'Midi Effect Rack
                        branch.MidiEffectRack = nodeInNode.ParentNode.ParentNode
                    End If

                    branches.MidiEffectBranchList.Add(branch)
                End If

                nodeInNode = nodeInNode.ParentNode
            Next

            branches.MidiEffectBranchList.Reverse()
            midiEffectBranchList.Add(branches)
        Next

        Return midiEffectBranchList.ToArray()
    End Function

    Public Shared Function GetPluginForKeyLED(name As String) As Plugins
        Dim detectName As String = name.ToLower().Replace(" ", "")

        If String.IsNullOrWhiteSpace(name) Then
            Return Plugins.None
        End If

        If detectName.Contains(".amxd") Then
            If detectName.Contains("midiext") OrElse detectName.Contains("midext") Then
                If name.Contains("Midi Extension") Then
                    Return Plugins.MidiExtension
                Else
                    Return Plugins.MidiExt
                End If

            ElseIf detectName.Contains("midifire") OrElse detectName.Contains("midfire") Then
                Return Plugins.MidiFire
            ElseIf detectName.Contains("lightweight") Then
                Return Plugins.Lightweight
            End If
        End If

        Return Plugins.None
    End Function

    Public Function ConvertKeyLEDForMidiExtension_v2(node As XmlNode, ByRef mm As MultiMapping, indent As Integer, saveContent As Dictionary(Of Integer, MidiExtensionSave)) As KeyLEDStructure()
        Dim ledList As New List(Of KeyLEDStructure)

        Try
            Dim save As MidiExtensionSave = GetMidiExtensionSave(node, saveContent)
            
            If Not String.IsNullOrWhiteSpace(save.MidiName) Then
                Dim filePath As String = $"{ABLETON_KEYLED_PATH}\{save.MidiName}"

                ledList.AddRange(ConvertKeyLEDForAnyMIDEX(node, mm, filePath, save.Speed, save.BPM))
            Else
                ConvertKeyLEDForAnyMIDEX(node, mm, indent, String.Empty) 'MultiMapping
            End If

        Catch ex As NullReferenceException
            '?
        End Try

        Return ledList.ToArray()
    End Function

    Public Shared Function GetMidiExtensionSave(node As XmlNode, saveContent As Dictionary(Of Integer, MidiExtensionSave)) As MidiExtensionSave
        Try
        Dim device As XmlNode = node?.Item("DeviceChain")?.Item("MidiToMidiDeviceChain")?.Item("Devices")?.Item("MxDeviceMidiEffect")
        Dim id As Integer = If(Not IsNothing(device), Integer.Parse(device.Item("LomId").GetAttribute("Value")), -1)

        If id <> -1 AndAlso saveContent.ContainsKey(id) Then
            Dim save As MidiExtensionSave = saveContent(id)
            Return save
        End If

        Catch ex As NullReferenceException
            '?
        End Try

        Return New MidiExtensionSave()
    End Function

    Private Sub GetMappingFromOfd()
        If MessageBox.Show(My.Resources.Contents.LED_Save_File_Not_Found, Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim fileName As String = String.Empty

            Invoke(Sub()
                Dim _ofd As New OpenFileDialog()
                _ofd.Multiselect = False
                _ofd.Title = "Please"

                If _ofd.ShowDialog() = DialogResult.OK Then
                    fileName = _ofd.FileName
                End If
            End Sub)

            If Not String.IsNullOrWhiteSpace(fileName) Then
                _midiExtensionMapping = GetMappingListForMidiExtension(fileName)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 미디 익스텐션 Save 파일을 통해 매핑 데이터들을 불러옵니다.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetMappingListForMidiExtension(path As String) As Dictionary(Of Integer, MidiExtensionSave)
        Dim mappingList As New Dictionary(Of Integer, MidiExtensionSave)

        Dim texts As String() = SplitbyLine(File.ReadAllText(path))

        For Each s In texts
            If String.IsNullOrWhiteSpace(s) Then
                Continue For
            End If

            Dim datas As String() = s.Split(" ")

            If datas(1).Contains("temp1") Then '빠르기
                Dim id As Integer = Integer.Parse(datas(1).Replace("id", "").Replace("temp1", ""))
                Dim speed As Integer = Integer.Parse(datas(2).TrimEnd(";"))

                If Not mappingList.ContainsKey(id) Then
                    Dim save As New MidiExtensionSave()
                    mappingList.Add(id, save)
                End If

                mappingList(id).Speed = speed

            ElseIf datas(1).Contains("temp2") Then 'BPM
                Dim id As Integer = Integer.Parse(datas(1).Replace("id", "").Replace("temp2", ""))
                Dim bpm As Integer = Integer.Parse(datas(2).TrimEnd(";"))

                If Not mappingList.ContainsKey(id) Then
                    Dim save As New MidiExtensionSave()
                    mappingList.Add(id, save)
                End If

                mappingList(id).BPM = bpm

            ElseIf datas(1).Contains("midifile") Then 'LED 파일
                Dim id As Integer = Integer.Parse(datas(1).Replace("midifile", "").Replace("id", ""))
                Dim file As String = datas(2).Split("/").Last().TrimEnd(";")

                If Not mappingList.ContainsKey(id) Then
                    Dim save As New MidiExtensionSave()
                    mappingList.Add(id, save)
                End If

                mappingList(id).MidiName = file
            End If
        Next

        Return mappingList
    End Function

    ''' <summary>
    ''' Midi Fire 플러그인을 위한 LED 자동 변환 함수
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="mm"></param>
    ''' <returns></returns>
    Public Function ConvertKeyLEDForMidiFire_v2(node As XmlNode, ByRef mm As MultiMapping, indent As Integer) As KeyLEDStructure()
        Dim ledList As New List(Of KeyLEDStructure)

        Try
            Dim save As MidiExtensionSave = GetMidiFireSave(node)

            If Not String.IsNullOrWhiteSpace(save.MidiName) Then
                Dim midiPathList As List(Of String) = Directory.GetFiles(ABLETON_KEYLED_PATH, "*.mid").ToList().Where(Function(filePath) Path.GetFileName(filePath) = save.MidiName).ToList()

                If midiPathList.Count > 0 Then
                    Dim midiPath As String = midiPathList.First()
                    ledList.AddRange(ConvertKeyLEDForAnyMIDEX(node, mm, indent, midiPath, save.Speed, save.BPM))
                Else
                    ConvertKeyLEDForAnyMIDEX(node, mm, indent, String.Empty) 'MultiMapping
                End If
            Else
                ConvertKeyLEDForAnyMIDEX(node, mm, indent, String.Empty) 'MultiMapping
            End If
        Catch ex As NullReferenceException
            '?
        End Try

        Return ledList.ToArray()
    End Function

    Public Shared Function GetMidiFireSave(node As XmlNode) As MidiExtensionSave
        Dim save As New MidiExtensionSave()
        
        Dim midiName As String = node.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices")?.Item("MxDeviceMidiEffect")?.Item("FileDropList")?.Item("FileDropList")?.Item("MxDFullFileDrop")?.Item("FileRef")?.Item("FileRef")?.Item("Name")?.GetAttribute("Value")
        Dim speed As String = node.Item("")
        Dim bpm As String = String.Empty

        If Not String.IsNullOrWhiteSpace(midiName) Then
            save.MidiName = midiName
        End If
        If Not IsNothing(speed) Then
            save.Speed = Integer.Parse(speed)
        End If
        If Not IsNothing(bpm) Then
            save.BPM = Integer.Parse(bpm)
        End If

        Return save
    End Function

    Public Shared Function GetLEDExtensionsForMidiFire(node As XmlNode) As LEDExtensions
        Return New LEDExtensions()
    End Function

    ''' <summary>
    ''' 통합 이후 Midi Extension, MIDIext, Midi Fire, Lightweight를 통해 LED 파일만 변환할 때 통합적으로 불러오는 함수
    ''' </summary>
    ''' <param name="node">Xml 노드</param>
    ''' <param name="mm">멀티 매핑</param>
    ''' <param name="indent">깊이</param>
    ''' <param name="midiFilePath">미디 (LED) 파일 경로</param>
    ''' <returns></returns>
    Public Shared Function ConvertKeyLEDForAnyMIDEX(node As XmlNode, ByRef mm As MultiMapping, indent As Integer, midiFilePath As String, Optional speed As Integer = 100, Optional bpm As Integer = 120) As KeyLEDStructure()
        Dim ledList As New List(Of KeyLEDStructure)()

        Dim isRandom = False '멀티매핑 인가?
        Dim isMyChair = True '여기가 내 자리인가? (멀티매핑 Key 정확도, Insufficient 및 Emptyspace 문제점 해결 변수)

        If mm.CurrentCount > 0 AndAlso indent = mm.Indent + 1 AndAlso Not mm.IsStrange Then
            isRandom = True
        Else
            If mm.IsStrange Then
                isMyChair = False
            Else
                mm = MultiMapping.Empty
            End If

            isRandom = False
        End If

        If Not isRandom Then
            'MidiRandom 테스트
            Try
                Dim midiRandomNode As XmlNode = node.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiRandom")
                
                If Not IsNothing(midiRandomNode) Then
                    Dim isActive As Boolean = Boolean.Parse(midiRandomNode.Item("On").Item("Manual").GetAttribute("Value"))

                    If isActive Then '활성화 상태인 경우
                        Dim choices As Integer = Integer.Parse(midiRandomNode.Item("Choices").Item("Manual").GetAttribute("Value"))
                        
                        Dim chainMin As Integer = Integer.Parse(node.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1
                        Dim chainMax As Integer = Integer.Parse(node.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1
                        
                        Dim noteNumberMin As Integer = Integer.Parse(node.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value"))
                        Dim noteNumberMax As Integer = Integer.Parse(node.Item("ZoneSettings").Item("KeyRange").Item("Max").GetAttribute("Value"))

                        mm.Count = choices
                        mm.CurrentCount = choices

                        mm.Chain.Start = chainMin
                        mm.Chain.End = chainMax

                        mm.NoteNumber.Start = noteNumberMin
                        mm.NoteNumber.End = noteNumberMax
                        mm.Indent = indent

                        Return {}
                    End If
                End If

            Catch ex As NullReferenceException
                '정상적인 MidiEffectBranch임.
            End Try
        End If

        Try
            If File.Exists(midiFilePath) Then
                Dim script As String = keyLED_Edit.keyLED_MidiToKeyLED(midiFilePath, True, speed, bpm)

                Dim chainMin As Integer = Integer.Parse(node.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1
                Dim chainMax As Integer = Integer.Parse(node.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1

                Dim noteNumberMin As Integer = Integer.Parse(node.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value"))
                Dim noteNumberMax As Integer = Integer.Parse(node.Item("ZoneSettings").Item("KeyRange").Item("Max").GetAttribute("Value"))

                If isRandom Then
                    isMyChair = False 'SupportStrangeMMKeyLED 함수
                End If

                If isMyChair Then
                    For chain = chainMin To chainMax
                        For noteNumber = noteNumberMin To noteNumberMax
                            Dim x As Integer = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, noteNumber)
                            Dim y As Integer = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, noteNumber)
                            Dim loopNumber = 1

                            Dim led As New KeyLEDStructure(chain, x, y, loopNumber, script)
                            ledList.Add(led)
                        Next
                    Next
                End If
            End If

            If isRandom Then
                mm.CurrentCount -= 1
            End If

        Catch ex As NullReferenceException
            '?
        End Try

        Return ledList.ToArray()
    End Function

    ''' <summary>
    ''' KeyLED 멀티 매핑 키 구하는 함수 (Insufficient, Emptyspace, Swapping 문제점 해결)
    ''' </summary>
    ''' <param name="nodeList">LED 노드 배열</param>
    ''' <param name="mm">멀티 매핑 정보</param>
    ''' <param name="plugin">플러그인 이름</param>
    ''' <returns>LED 배열</returns>
    Public Function SupportMMKeyLED(nodeList As List(Of LEDNodeList), ByRef mm As MultiMapping, plugin As Plugins) As KeyLEDStructure()
        Dim ledList As New List(Of KeyLEDStructure)()
        
        For i = 0 To mm.Count - 1
            Dim node As XmlNode = Nothing
            Dim isFound = False
            
            For j = 0 To nodeList.Count - 1
                Dim ledNode As LEDNodeList = nodeList(j)

                If ledNode.Name = "MidiEffectBranch" Then
                    Dim noteNumberMin As Integer = Integer.Parse(ledNode.Node.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value"))
                    Dim usedCount As Integer = noteNumberMin - mm.NoteNumber.Start

                    If usedCount = i Then
                        isFound = True
                        node = ledNode.Node
                        Exit For
                    End If
                End If
            Next

            Dim chainFootprint As Footprint(Of Integer) = mm.Chain
            Dim noteNumberFootprint As Footprint(Of Integer) = mm.NoteNumber
            Dim script = ""

            If isFound Then
                Dim chainMin As Integer = Integer.Parse(node.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1
                Dim chainMax As Integer = Integer.Parse(node.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1
            
                chainFootprint = New Footprint(Of Integer)(chainMin, chainMax)
                
                Dim midexSave As New MidiExtensionSave()

                Select Case plugin
                    Case Plugins.MidiExtension
                        midexSave = GetMidiExtensionSave(node, _midiExtensionMapping)

                    Case Plugins.MidiExt, Plugins.MidiFire, Plugins.Lightweight
                        midexSave = GetMidiFireSave(node)

                End Select

                If Not String.IsNullOrWhiteSpace(midexSave.MidiName) Then
                    Dim midiFilePath As String = $"{ABLETON_KEYLED_PATH}\{midexSave.MidiName}"
                    script = keyLED_Edit.keyLED_MidiToKeyLED(midiFilePath, True, midexSave.Speed, midexSave.BPM)
                End If
            End If

            For chain = chainFootprint.Start To chainFootprint.End
                For noteNumber = noteNumberFootprint.Start To noteNumberFootprint.End
                    Dim x As Integer = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, noteNumber)
                    Dim y As Integer = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, noteNumber)
                    Dim loopNumber = 1

                    Dim led As New KeyLEDStructure(chain, x, y, loopNumber, script)
                    ledList.Add(led)
                Next
            Next
        Next

        If mm.CurrentCount > 0 Then
            mm.CurrentCount = 0
        End If

        Return ledList.ToArray()
    End Function

    ''' <summary>
    ''' keyLED를 Workspace에 저장
    ''' </summary>
    ''' <param name="keyLED"></param>
    Public Shared Sub SaveKeyLED(keyLED As KeyLEDStructure)
        SaveKeyLED(keyLED.Chain, keyLED.X, keyLED.Y, keyLED.LoopNumber, keyLED.Script)
    End Sub

    Public Shared Sub SaveKeyLED(chain As Integer, x As Integer, y As Integer, loopNumber As Integer, content As String)
        If Not Directory.Exists(UNIPACK_KEYLED_PATH) Then
            Directory.CreateDirectory(UNIPACK_KEYLED_PATH)
        End If

        Dim name As String = $"{chain} {x} {y} {loopNumber}"
        Dim path As String = $"{UNIPACK_KEYLED_PATH}\{name}"

        If File.Exists(path) Then
            File.Move(path, RenameKeyLED(path, "a"C))
        End If

        For Each c In LEDMappings
            Dim ledPath As String = RenameKeyLED(path, c)

            If Not File.Exists(ledPath) Then
                File.WriteAllText(ledPath, content)
                Exit For
            End If
        Next
    End Sub

    Private Shared Function RenameKeyLED(name As String, alphabet As Char) As String
        If Not IsNothing(alphabet) AndAlso Asc(alphabet) >= 97 AndAlso Asc(alphabet) <= 122 Then
            name = $"{name} {alphabet}"
        End If

        Return name
    End Function

    'https://stackoverflow.com/questions/21472941/create-a-nested-list-of-items-from-objects-with-a-parent-reference
    Public Shared Sub GetLEDNodeInLoop(nodes As IEnumerable(Of LEDNodeList), doAction As Action(Of LEDNodeList, List(Of LEDNodeList), Integer), Optional indent As Integer = 0)
        For Each root In nodes
            doAction(root, nodes, indent)
            GetLEDNodeInLoop(root.NodeList, doAction, indent + 1)
        Next
    End Sub

    'https://stackoverflow.com/questions/9555864/variable-nested-for-loops for Visual Basic .NET
    Public Shared Sub DoLoop(depth As Integer, ByRef numbers As List(Of Integer), ByRef maxes As List(Of Integer), doAction As Action(Of List(Of Integer)))
        If depth > 0 Then
            For i = 0 To maxes(depth - 1) - 1
                numbers(depth - 1) = i
                DoLoop(depth - 1, numbers, maxes, doAction)
            Next

        Else
            doAction(numbers)

        End If
    End Sub
#Region "KeyLED (MIDEX) Conversion (Deprecated, v1)"
    '에이블톤 Instrument Rack을 keyLED로 바꿔주는 코드. (Deprecated)
    <Obsolete("This method is deprecated, use ConvertKeyLEDForMIDEX_v2 instead.")>
    Private Sub KeyLED_MidiToKeyLED_AutoConvert()
        Try
            If abl_openedproj AndAlso abl_openedled Then

                Invoke(Sub()
                           With Loading
                               .Show()
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

                Dim MidiKind As String = "MidiExtension" 'MidiExtension / MidiFire

                Dim MIDEX_LEDSave As String = Application.StartupPath & "\Workspace\ableproj\LEDSave.uni"
                Dim MIDEX_LEDMapping As String = Application.StartupPath & "\Workspace\ableproj\LEDMapping.uni"



                If File.Exists(MIDEX_LEDSave) AndAlso File.ReadAllText(Application.StartupPath & "\Workspace\ableproj\abl_proj.xml").Contains("Midi Extension") Then
                    Dim LEDs As String() = File.ReadAllLines(MIDEX_LEDSave)
                    File.WriteAllText(MIDEX_LEDSave, StringsToString(LEDs))

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

                    File.WriteAllText(MIDEX_LEDMapping, StringsToString(LEDs))
                Else
                    MidiKind = "MidiFire"
                End If

                '이제 버그와의 전쟁 (악몽)이 시작될 코드.
                Dim ablprj As String = Application.StartupPath & "\Workspace\ableproj\abl_proj.xml"
                Dim doc As New XmlDocument
                Dim setNode As XmlNodeList
                doc.Load(ablprj)
                setNode = doc.GetElementsByTagName("MidiEffectBranch")

                Dim err As String = String.Empty

                If MidiKind = "MidiExtension" Then '구식 스파게티 코드
                    ConvertKeyLEDForMidiExtension(ablprj, err, True)

                ElseIf MidiKind = "MidiFire" Then '너무 스파게티 코드여서 그대로 진행하기에는 너무 아까워서 처음부터 코드를 다시 짜기로 함.
                    Dim items As New List(Of Integer)
                    Dim errStr As New StringBuilder(255)

                    Dim IsNextOfNext As New List(Of Boolean) 'MidiEffectRack 안에 MidiEffectRack이 있는가? ( https://blog.naver.com/ericseyoun/221673229156 참고 )

                    For i As Integer = 0 To setNode.Count - 1
                        '유니컨버터 3074줄부터 제대로 다시 코딩.
                        Try
                            Dim _Test1 As Integer = Integer.Parse(setNode(i).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("LomId").GetAttribute("Value"))
                            Try
                                Dim _Test2 As Integer = Integer.Parse(setNode(i).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiRandom").Item("Choices").Item("Manual").GetAttribute("Value")) '랜덤 MidiEffectRack
                            Catch exNN As NullReferenceException
                                Dim _Test3 As String = setNode(i).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("PatchSlot").Item("Value").Item("MxDPatchRef").Item("FileRef").Item("Name").GetAttribute("Value") '일반 MidiEffectRack
                            End Try
                            IsNextOfNext.Add(False)
                            items.Add(i) '옳소.
                        Catch exN As NullReferenceException
                            Dim NextOfNextFound As Boolean = False
                            Try
                                Dim _Test4 As Integer = Integer.Parse(setNode(i).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiEffectGroupDevice").Item("LomId").GetAttribute("Value"))
                                Try
                                    Integer.Parse(setNode(i).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiRandom").Item("Choices").Item("Manual").GetAttribute("Value")) '랜덤 코드만 입장 가능.
                                Catch
                                    '이것도 Exception 처리 나오면 다른거고, 성공하면 Page임.
                                    Dim NextOfNextTest As String = setNode(i).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiEffectGroupDevice").Item("Branches").Item("MidiEffectBranch").GetAttribute("Id")
                                    NextOfNextFound = True
                                End Try
                                IsNextOfNext.Add(NextOfNextFound)
                                items.Add(i)
                                Catch exNN As Exception
                                Continue For '다른 무언가이였던것임!
                            End Try
                        End Try
                    Next

                    Dim UniPack_Chain As Integer = 1
                    Dim UniPack_X As Integer = 0
                    Dim UniPack_Y As Integer = 0
                    Dim UniPack_L As Integer = 0

                    Dim fndError As Boolean = False 'Key / Random Key
                    Dim MidiName As String = String.Empty
                    Dim MidiFileName As String = String.Empty

                    Dim PrChain As Integer = 0 '랜덤의 체인.
                    Dim PrChainM As Integer = 0 '랜덤의 최대 체인.
                    Dim PrKey As Integer = 0 '랜덤의 ksX.
                    Dim PrKeyM As Integer = 0 '랜덤의 최대 ksX.

                    Dim NextOfNextChain As Integer = 0 'Parent의 체인.
                    Dim NextOfNextMaxChain As Integer = 0 'Parent의 최대 체인.
                    Dim IsRandom As Boolean = False '현재 접근하고 있는 XML Branch가 랜덤인가?
                    Dim Choices As Integer = 0 '매우 정확한 랜덤의 수. (from MidiRandom)
                    Dim curid As Integer = 1 '현재의 랜덤. (from Choices / MidiRandom)

                    For i As Integer = 0 To items.Count - 1
                        Dim root As XmlNode = setNode(items(i))

                        Try
                            If IsNextOfNext(i) Then
                                NextOfNextChain = Integer.Parse(root.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1 '최소 체인.
                                NextOfNextMaxChain = Integer.Parse(root.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1 '최대 체인.
                                Continue For
                            End If

                            Integer.Parse(root.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("LomId").GetAttribute("Value"))
                            MidiName = root.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("PatchSlot").Item("Value").Item("MxDPatchRef").Item("FileRef").Item("Name").GetAttribute("Value")

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
                            If root.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiRandom").Item("On").Item("Manual").GetAttribute("Value") = "false" Then
                                Continue For '비활성화
                            End If

                            PrChain = Integer.Parse(root.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1 '최소 체인.
                            PrChainM = Integer.Parse(root.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1 '최대 체인.

                            PrKey = Integer.Parse(root.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value")) '최소 Key (ksX).
                            PrKeyM = Integer.Parse(root.Item("ZoneSettings").Item("KeyRange").Item("Max").GetAttribute("Value")) '최대 Key (ksX).

                            Try
                                Choices = Integer.Parse(root.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiRandom").Item("Choices").Item("Manual").GetAttribute("Value")) 'MidiRandom > Choices > Manual Value
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

                        If fndError = False AndAlso Not MidiName.Contains(".amxd") Then
                            errStr.Append(vbNewLine & "Can't find index " & i.ToString() & ".")
                            Continue For
                        End If

                        UniPack_Chain = Integer.Parse(root.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1 'Get Chain.
                        UniPack_X = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, Integer.Parse(root.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value"))) 'Get X Pos.
                        UniPack_Y = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, Integer.Parse(root.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value"))) 'Get Y Pos.
                        UniPack_L = 1

                        Dim MaxChain As Integer = Integer.Parse(root.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1
                        If Not PrChain = 0 AndAlso IsRandom Then 'Random Chain.
                            UniPack_Chain = PrChain
                            MaxChain = PrChainM
                        Else
                            PrChain = 0
                        End If

                        'NextOfNext Chain 반영
                        If Not NextOfNextChain = 0 AndAlso Not NextOfNextMaxChain = 0 AndAlso IsRandom = False Then
                            UniPack_Chain = NextOfNextChain
                            MaxChain = NextOfNextMaxChain
                        End If

                        Dim MaxX As Integer = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, Integer.Parse(root.Item("ZoneSettings").Item("KeyRange").Item("Max").GetAttribute("Value"))) 'Get X Pos.
                        Dim MaxY As Integer = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, Integer.Parse(root.Item("ZoneSettings").Item("KeyRange").Item("Max").GetAttribute("Value"))) 'Get Y Pos.
                        If Not PrKey = 0 AndAlso IsRandom Then 'Random Key.
                            UniPack_X = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, PrKey)
                            UniPack_Y = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, PrKey)
                        End If

                        Try
                            MidiFileName = root.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("FileDropList").Item("FileDropList").Item("MxDFullFileDrop").Item("FileRef").Item("FileRef").Item("Name").GetAttribute("Value")
                        Catch ex As NullReferenceException
                            Continue For
                        End Try

                        If UniPack_Chain > 8 OrElse UniPack_Chain = 0 OrElse UniPack_X = -8192 OrElse UniPack_X = 0 Then
                            Continue For
                        End If

                        Dim LoopNumber_1 As Integer() = New Integer(1) {}
                        Dim LoopNumber_1bool As Boolean 'Chain Value = ?
                        LoopNumber_1(0) = UniPack_Chain
                        LoopNumber_1(1) = MaxChain
                        LoopNumber_1bool = LoopNumber_1(0) = LoopNumber_1(1)

                        Dim LoopNumber_2 As Integer() = New Integer(1) {}
                        Dim LoopNumber_2bool As Boolean 'Key Value = ?
                        LoopNumber_2(0) = Integer.Parse(root.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value"))
                        LoopNumber_2(1) = Integer.Parse(root.Item("ZoneSettings").Item("KeyRange").Item("Max").GetAttribute("Value"))
                        LoopNumber_2bool = LoopNumber_2(0) = LoopNumber_2(1)

                        Dim str As String = keyLED_Edit.keyLED_MidiToKeyLED(String.Format("{0}\Workspace\ableproj\CoLED\{1}", Application.StartupPath, MidiFileName), True, 100, 120)

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
                                        For Each lpn As Char In LEDMappings
                                            If Not File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn)) Then
                                                File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn), Str)
                                                Exit For
                                            End If
                                        Next

                                    Else
                                        File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), Str)
                                    End If
#End Region

                                ElseIf LoopNumber_2bool = False Then

                                    For q As Integer = LoopNumber_2(0) To LoopNumber_2(1)
                                        UniPack_Chain = p
                                        UniPack_X = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, q)
                                        UniPack_Y = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, q)
#Region "Save the keyLED with Overwrite Protection!"
                                        If File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) OrElse File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} a", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) Then
                                            If File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) Then
                                                My.Computer.FileSystem.RenameFile(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), String.Format("{0} {1} {2} {3} a", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L))
                                            End If
                                            For Each lpn As Char In LEDMappings
                                                If Not File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn)) Then
                                                    File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn), Str)
                                                    Exit For
                                                End If
                                            Next

                                        Else
                                            File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), Str)
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
                                    For Each lpn As Char In LEDMappings
                                        If Not File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn)) Then
                                            File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn), Str)
                                            Exit For
                                        End If
                                    Next

                                Else
                                    File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), Str)
                                End If
#End Region

                            ElseIf LoopNumber_2bool = False Then

                                For q As Integer = LoopNumber_2(0) To LoopNumber_2(1)
                                    UniPack_X = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, q)
                                    UniPack_Y = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, q)
#Region "Save the keyLED with Overwrite Protection!"
                                    If File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) OrElse File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} a", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) Then
                                        If File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L)) Then
                                            My.Computer.FileSystem.RenameFile(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), String.Format("{0} {1} {2} {3} a", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L))
                                        End If
                                        For Each lpn As Char In LEDMappings
                                            If Not File.Exists(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn)) Then
                                                File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3} {4}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, lpn), Str)
                                                Exit For
                                            End If
                                        Next

                                    Else
                                        File.WriteAllText(Application.StartupPath & String.Format("\Workspace\unipack\keyLED\{0} {1} {2} {3}", UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L), Str)
                                    End If
#End Region
                                Next

                            End If

                        End If
                        Debug.WriteLine(MidiFileName & ", chain:" & UniPack_Chain & " x:" & UniPack_X & " y:" & UniPack_Y)
                    Next

                    err = errStr.ToString()
                End If

                Debug.WriteLine("Finish...")

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
                    MessageBox.Show("[ Warning ]" & vbNewLine & "keyLED (MIDI Extension): [] format is invaild." & vbNewLine & err.ToString(), Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Else
                w8t4abl = "keyLED"
            End If

            IsWorking = False

            Select Case lang
                Case Translator.tL.English
                    MessageBox.Show("LED File Converted!" & vbNewLine & "You can show the LEDs on 'keyLED (MIDI Extension)' Tab!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Case Translator.tL.Korean
                    MessageBox.Show("LED 파일이 변환 되었습니다!" & vbNewLine & "''keyLED (미디 익스텐션)' 탭에서 LED 파일들을 볼 수 있습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Select

        Catch ex As Exception
            UI(Sub() Loading.Dispose())
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    <Obsolete("This method is deprecated, use ConvertKeyLEDForMidiExtension_v2 instead.")>
    Public Sub ConvertKeyLEDForMidiExtension(AbletonProjectFilePath As String, ByRef err As String, ShowLoadingMessage As Boolean)
        Dim doc As New XmlDocument
        Dim setNode As XmlNodeList
        doc.Load(AbletonProjectFilePath)
        setNode = doc.GetElementsByTagName("MidiEffectBranch")

        Dim MIDEX_LEDMapping As String = Application.StartupPath & "\Workspace\ableproj\LEDMapping.uni"
        
        Dim InsBrnN As String = String.Empty 'Midi Effect Rack의 index를 통과 시켜주는 lst
        Dim li As Integer = 1 '람다 식 경고 실화냐...

        If ShowLoadingMessage Then
            UI(Sub() Loading.DLb.Left -= 70)
        End If

        For i As Integer = 0 To setNode.Count - 1
            li += 1

            Try
                Dim _Test1 As Integer = Integer.Parse(setNode(i).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("LomId").GetAttribute("Value"))
                Try
                    Dim _Test2 As Integer = Integer.Parse(setNode(i).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiRandom").Item("Choices").Item("Manual").GetAttribute("Value")) '랜덤 MidiEffectRack
                Catch exNN As NullReferenceException
                    Dim _Test3 As String = setNode(i).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("PatchSlot").Item("Value").Item("MxDPatchRef").Item("FileRef").Item("Name").GetAttribute("Value") '일반 MidiEffectRack
                End Try
                InsBrnN &= i & ";"
            Catch exN As NullReferenceException
                Try
                    Dim _Test4 As Integer = Integer.Parse(setNode(i).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiEffectGroupDevice").Item("LomId").GetAttribute("Value"))
                    Integer.Parse(setNode(i).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiRandom").Item("Choices").Item("Manual").GetAttribute("Value")) '랜덤 코드만 입장 가능.
                    InsBrnN &= i & ";"
                Catch exNN As Exception
                    Continue For 'Page나 다른 무언가이였던것임!
                End Try
            End Try
        Next
        
        If ShowLoadingMessage Then
            UI(Sub()
                   Loading.DLb.Left += 70
               End Sub)
        End If

        '또한 Clip Tempo는 BPM이며, [Clip Tempo = temp2]
        'New Tempo는 빠르기다. [New Tempo = temp1]

        Dim il As Integer = 0
        Dim ChN As String = String.Empty 'New Tempo [Speed]
        Dim ChN2 As String = String.Empty 'Clip Tempo [BPM]

        Dim LEDsText As String() = File.ReadAllLines(MIDEX_LEDMapping)
        For di As Integer = 0 To LEDsText.Length - 1
            Dim d As String = LEDsText(di)

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
                    Dim dIndex As Integer() = ReadAllIndex(LEDsText, "MIDI Extension")
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

                    Dim dPath As String = String.Format("{0}\Workspace\ableproj\CoLED\{1}", Application.StartupPath, dFile)
                    If File.Exists(dPath) = False Then
                        Debug.WriteLine(String.Format("'{0}' File doesn't exists.", dFile))
                        err &= vbNewLine & String.Format("'{0}' MIDI File doesn't exists.", dFile)
                        Continue For
                    End If

                    Dim str As String = keyLED_Edit.keyLED_MidiToKeyLED(dPath, True, dSpeed, dBPM)
                    dSpeed = 0

                    '이제 Get Chain & X, Y from XML!!!
                    Dim UniPack_Chain As Integer = 1
                    Dim UniPack_X As Integer = 0
                    Dim UniPack_Y As Integer = 0
                    Dim UniPack_L As Integer = 0

                    Dim fileN As String = String.Empty
                    Dim x As XmlNode
                    Dim sFile As String = String.Empty

                    'PatchSlot > Value > MxDPatchRef > FileRef > Name > Value 'Midi Extension.amxd'
                    'LED Save 파일의 id는 MxDeviceMidiEffect의 LomId Value랑 같음.

                    '또한 keySound에서는 Random 선언이 MidiToAudioDeviceChain,
                    'keyLED에서는 MidiToMidiDeviceChain임. (ㄹㅇ 에이블톤 프로그램 제작자들은 알고리즘을 왜 이따구로 만들었냐..)
                    Dim id_index As Integer = 0 'LomId (MIDI Extension id)
                    Dim fndError As Boolean = False 'Key / Random Key
                    Dim NotFound As Boolean = False '아직도 못찾았냐? 넘겨

                    Dim currentid As Integer = 0 '현재 id.
                    Dim MidiName As String = String.Empty

                    Dim PrChain As Integer = 0 '랜덤의 체인.
                    Dim PrChainM As Integer = 0 '랜덤의 최대 체인.
                    Dim PrKey As Integer = 0 '랜덤의 ksX.
                    Dim PrKeyM As Integer = 0 '랜덤의 최대 ksX.

                    Dim IsRandom As Boolean = False '현재 접근하고 있는 XML Branch가 랜덤인가?
                    Dim Choices As Integer = 0 '매우 정확한 랜덤의 수. (from MidiRandom)
                    Dim curid As Integer = 1 '현재의 랜덤. (from Choices / MidiRandom)

                    Dim InsX As String = InsBrnN.TrimEnd(";")
                    For Each ndx As Integer In InsX.Split(";")
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
                            NotFound = True
                            currentid = Integer.Parse(setNode(ndx).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("LomId").GetAttribute("Value"))
                            MidiName = setNode(ndx).Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("PatchSlot").Item("Value").Item("MxDPatchRef").Item("FileRef").Item("Name").GetAttribute("Value")
                            If d_id = currentid AndAlso MidiName.Contains(".amxd") Then
                                id_index = ndx
                                InsBrnN.Replace(ndx & ";", "")
                                NotFound = False
                                Exit For
                            End If
                        End If
                    Next

                    If NotFound Then
                        err &= vbNewLine & String.Format("Can't find id {0} on '{1}'.", d_id, dFile)
                        Choices = 8192 'Same As Continue For
                    End If

                    x = setNode(id_index)

                    UniPack_Chain = Integer.Parse(x.Item("BranchSelectorRange").Item("Min").GetAttribute("Value")) + 1 'Get Chain.
                    UniPack_X = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, Integer.Parse(x.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value"))) 'Get X Pos.
                    UniPack_Y = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, Integer.Parse(x.Item("ZoneSettings").Item("KeyRange").Item("Min").GetAttribute("Value"))) 'Get Y Pos.
                    UniPack_L = 1

                    Dim MaxChain As Integer = Integer.Parse(x.Item("BranchSelectorRange").Item("Max").GetAttribute("Value")) + 1
                    If Not PrChain = 0 AndAlso IsRandom Then 'Random Chain.
                        UniPack_Chain = PrChain
                        MaxChain = PrChainM
                    Else
                        PrChain = 0
                    End If

                    Dim MaxX As Integer = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, Integer.Parse(x.Item("ZoneSettings").Item("KeyRange").Item("Max").GetAttribute("Value"))) 'Get X Pos.
                    Dim MaxY As Integer = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, Integer.Parse(x.Item("ZoneSettings").Item("KeyRange").Item("Max").GetAttribute("Value"))) 'Get Y Pos.
                    If Not PrKey = 0 AndAlso IsRandom Then 'Random Key.
                        UniPack_X = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, PrKey)
                        UniPack_Y = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, PrKey)
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
                                SaveKeyLEDWithOverwriteProtectionForMIDEX(UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, str)

                            ElseIf LoopNumber_2bool = False Then

                                For q As Integer = LoopNumber_2(0) To LoopNumber_2(1)
                                    UniPack_Chain = p
                                    UniPack_X = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, q)
                                    UniPack_Y = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, q)
                                    SaveKeyLEDWithOverwriteProtectionForMIDEX(UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, str)
                                Next

                            End If

                        Next

                    ElseIf LoopNumber_1bool Then

                        If LoopNumber_2bool Then
                            '기본값.
                            SaveKeyLEDWithOverwriteProtectionForMIDEX(UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, str)

                        ElseIf LoopNumber_2bool = False Then

                            For q As Integer = LoopNumber_2(0) To LoopNumber_2(1)
                                UniPack_X = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, q)
                                UniPack_Y = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, q)
                                SaveKeyLEDWithOverwriteProtectionForMIDEX(UniPack_Chain, UniPack_X, UniPack_Y, UniPack_L, str)
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
    End Sub

    <Obsolete("This method is deprecated, use SaveKeyLED instead.")>
    Public Shared Sub SaveKeyLEDWithOverwriteProtectionForMIDEX(chain As Integer, x As Integer, y As Integer, loopNumber As Integer, keyLEDContent As String)
        If Not Directory.Exists(UNIPACK_KEYLED_PATH) Then
            Directory.CreateDirectory(UNIPACK_KEYLED_PATH)
        End If
        
        If File.Exists(UNIPACK_KEYLED_PATH & String.Format("\{0} {1} {2} {3}", chain, x, y, loopNumber)) OrElse File.Exists(UNIPACK_KEYLED_PATH & String.Format("\{0} {1} {2} {3} a", chain, x, y, loopNumber)) Then
            If File.Exists(UNIPACK_KEYLED_PATH & String.Format("\{0} {1} {2} {3}", chain, x, y, loopNumber)) Then
                My.Computer.FileSystem.RenameFile(UNIPACK_KEYLED_PATH & String.Format("\{0} {1} {2} {3}", chain, x, y, loopNumber), String.Format("{0} {1} {2} {3} a", chain, x, y, loopNumber))
            End If

            For Each lpn In LEDMappings
                If Not File.Exists(UNIPACK_KEYLED_PATH & String.Format("\{0} {1} {2} {3} {4}", chain, x, y, loopNumber, lpn)) Then
                    File.WriteAllText(UNIPACK_KEYLED_PATH & String.Format("\{0} {1} {2} {3} {4}", chain, x, y, loopNumber, lpn), keyLEDContent)
                    Exit For
                End If
            Next

        Else
            File.WriteAllText(UNIPACK_KEYLED_PATH & String.Format("\{0} {1} {2} {3}", chain, x, y, loopNumber), keyLEDContent)
        End If
    End Sub
#End Region

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
                '나중에 keyLED (MIDEX) 탭에서 런치패드 지원할 예정.
            End If

        Catch ex As Exception
            If IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
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

            Dim led As New ledReturn

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

    Private Async Sub ResetTheProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetTheProjectToolStripMenuItem.Click
        Await InitializeProject(True)
    End Sub

    Public Async Function InitializeProject(showLoadingMessage As Boolean) As Task
        If showLoadingMessage Then
            Invoke(Sub()
                With Loading
                    .Show()
                    .Text = My.Resources.Contents.Project_Reset
                    .DLb.Text = My.Resources.Contents.Project_Initializing
                End With
            End Sub)
        End If

        Await Initialize()

        If showLoadingMessage Then
            Invoke(Sub()
                Loading.Close()
                   End Sub)

            MessageBox.Show(My.Resources.Contents.Project_Initialized)
        End If
    End Function

    Private Sub KeyLEDMIDEX_BetaButton_Click(sender As Object, e As EventArgs) Handles keyLEDMIDEX_BetaButton.Click
        keyLED_Edit.Show()
        keyLED_Edit.Focus()
    End Sub

    Private Sub LEDtoAutoPlayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LEDtoAutoPlayToolStripMenuItem.Click
        keyLED_AutoPlay.Show()
    End Sub

    Public Shared Function ArrayToString(Of T)(ByVal array As IEnumerable(Of T)) As String
        Dim text As StringBuilder = New StringBuilder()
        text.Append("[")

        For i As Integer = 0 To array.Count - 1
            text.Append(array(i).ToString())

            If Not i = array.Count - 1 Then
                text.Append(",")
            End If
        Next
        text.Append("]")

        Return text.ToString()
    End Function

    Private Async Sub btnConvertKeyLEDAutomatically_Click(sender As Object, e As EventArgs) Handles btnConvertKeyLEDAutomatically.Click
        Dim errorMessage As String = Await ReadyForConvertKeyLEDForMIDEX(True)

        If Not String.IsNullOrWhiteSpace(errorMessage) Then
            MessageBox.Show(errorMessage, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        MessageBox.Show(My.Resources.Contents.KeyLED_Created, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub UseNewFeatureAboutOpenProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UseNewFeatureAboutOpenProjectToolStripMenuItem.Click
        If UseNewFeatureAboutOpenProjectToolStripMenuItem.Checked Then
            OpenProjectOnce = ProjectOpenMethod.Smart
            OpenAPFToolStripMenuItem.Text = My.Resources.Contents.Main_OpenAbletonProject_Beta
        Else
            OpenProjectOnce = ProjectOpenMethod.Nogada
            OpenAPFToolStripMenuItem.Text = My.Resources.Contents.Main_OpenAbletonProject
        End If
    End Sub

    Private Async Sub OpenAPFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenAPFToolStripMenuItem.Click
        Select Case OpenProjectOnce
            Case ProjectOpenMethod.Nogada
                Await OpenAbletonProjectOnce()
            
            Case ProjectOpenMethod.Smart
                Await OpenAbletonProjectOnce_v2()

        End Select

        Await ReadyForAutoConvertForKeySound()
        Await ReadyForAutoConvertForKeyLED()
    End Sub

    ''' <summary>
    ''' Nogada Version (v1.0)
    ''' </summary>
    ''' <returns></returns>
    Private Async Function OpenAbletonProjectOnce() As Task
        Dim apfPath As String = String.Empty
        Dim soundPaths As String() = {}
        Dim ledPaths As String() = {}

        'Ableton Project File
        Dim apfOfd As New OpenFileDialog() With {
                .Filter = My.Resources.Contents.Project_ofd_Filter,
                .Title = My.Resources.Contents.Project_ofd_Title,
                .AddExtension = False,
                .Multiselect = False
        }

        If apfOfd.ShowDialog() = DialogResult.OK Then
            apfPath = apfOfd.FileName
        End If

        'Sounds
        Dim soundsOfd As New OpenFileDialog() With {
                .Filter = My.Resources.Contents.Sound_ofd_Filter,
                .Title = My.Resources.Contents.Sound_ofd_Title,
                .Multiselect = True
        }

        If soundsOfd.ShowDialog() = DialogResult.OK Then
            soundPaths = soundsOfd.FileNames
        End If

        'LEDs
        Dim ledsOfd As New OpenFileDialog() With {
                .Multiselect = True,
                .Title = My.Resources.Contents.LED_ofd_Title,
                .Filter = My.Resources.Contents.LED_ofd_Filter            
        }

        If ledsOfd.ShowDialog() = DialogResult.OK Then
            ledPaths = ledsOfd.FileNames
        End If

        Await Task.Run(Sub()
            OpenAbletonProjectFile(apfPath, True)
            OpenSounds(soundPaths, True)
            OpenKeyLED(ledPaths, True)
                       End Sub)
    End Function

    ''' <summary>
    ''' Smart Version (v2.0)
    ''' </summary>
    ''' <returns></returns>
    Private Async Function OpenAbletonProjectOnce_v2() As Task
        Dim apfPath As String = String.Empty
        Dim soundList As New List(Of String)
        Dim ledPaths As String() = {}

        'Load Ableton Project File Only
        Dim apfOfd As New OpenFileDialog() With {
                .Filter = My.Resources.Contents.Project_ofd_Filter,
                .Title = My.Resources.Contents.Project_ofd_Title,
                .AddExtension = False,
                .Multiselect = False
        }

        If apfOfd.ShowDialog() = DialogResult.OK Then
            apfPath = apfOfd.FileName

            'Get Sound Files And LED Files Automatically
            Dim apfDirectory As String = Directory.GetParent(apfPath).FullName

            soundList.AddRange(Directory.GetFiles(apfDirectory, "*.wav", SearchOption.AllDirectories))
            soundList.AddRange(Directory.GetFiles(apfDirectory, "*.mp3", SearchOption.AllDirectories))

            ledPaths = Directory.GetFiles(apfDirectory, "*.mid", SearchOption.AllDirectories)
        End If

        Await Task.Run(Sub()
            OpenAbletonProjectFile(apfPath, True)
            OpenSounds(soundList.ToArray(), True)
            OpenKeyLED(ledPaths, True)
        End Sub)

        If abl_openedled Then
            keyLEDMIDEX_BetaButton.Enabled = True
        End If
    End Function

    Private Async Sub btnKeySound_AutoConvert_Click(sender As Object, e As EventArgs) Handles btnKeySound_AutoConvert.Click
        Dim errorMessage As String = Await ReadyForConvertKeySound(True)

        If Not String.IsNullOrWhiteSpace(errorMessage) Then
            MessageBox.Show(errorMessage, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        MessageBox.Show(My.Resources.Contents.KeySound_Created, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
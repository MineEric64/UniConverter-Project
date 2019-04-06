<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainProject
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainProject))
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenAbletonProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SoundsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenKeyLEDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConvertALSToUnipackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnipackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnyAbletonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbletonLive9LiteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbletonLive9TrialToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbletonLive9SuiteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbletonLive10ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnipackToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConvertToZipUniToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TutorialsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckUpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportBugsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeveloperModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.InfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ofd = New System.Windows.Forms.OpenFileDialog()
        Me.sfd = New System.Windows.Forms.SaveFileDialog()
        Me.LEDOpen1 = New System.Windows.Forms.OpenFileDialog()
        Me.HomeEdit = New System.Windows.Forms.TabControl()
        Me.Info1 = New System.Windows.Forms.TabPage()
        Me.infoTB3 = New System.Windows.Forms.TextBox()
        Me.Tip1 = New System.Windows.Forms.Label()
        Me.Info_SaveButton = New System.Windows.Forms.Button()
        Me.infoT3 = New System.Windows.Forms.Label()
        Me.infoT2 = New System.Windows.Forms.Label()
        Me.infoTB2 = New System.Windows.Forms.TextBox()
        Me.infoTB1 = New System.Windows.Forms.TextBox()
        Me.infoT1 = New System.Windows.Forms.Label()
        Me.KeySoundTab = New System.Windows.Forms.TabPage()
        Me.ks_SearchLabel = New System.Windows.Forms.Label()
        Me.ks_SearchSound = New System.Windows.Forms.TextBox()
        Me.ks_Sound2 = New System.Windows.Forms.Label()
        Me.ks_Sound1 = New System.Windows.Forms.Label()
        Me.ks_SelY = New System.Windows.Forms.ComboBox()
        Me.ks_SelX = New System.Windows.Forms.ComboBox()
        Me.ks_SelChain = New System.Windows.Forms.ComboBox()
        Me.EdKeysButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.CutSndButton = New System.Windows.Forms.Button()
        Me.Tip3 = New System.Windows.Forms.Label()
        Me.DevelopingLabel1 = New System.Windows.Forms.Label()
        Me.BackButton = New System.Windows.Forms.Button()
        Me.GoButton = New System.Windows.Forms.Button()
        Me.keySound_ListView = New System.Windows.Forms.ListView()
        Me.SortingNumber = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FileName2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Length2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Loop2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Sound_ListView = New System.Windows.Forms.ListView()
        Me.FileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Length = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.AssingedButtons = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.keyLED1 = New System.Windows.Forms.TabPage()
        Me.DevelopingLabel2 = New System.Windows.Forms.Label()
        Me.keyLED2 = New System.Windows.Forms.TabPage()
        Me.DevelopingLabel3 = New System.Windows.Forms.Label()
        Me.keyLEDBetaButton = New System.Windows.Forms.Button()
        Me.BGW_keyLED = New System.ComponentModel.BackgroundWorker()
        Me.BGW_ablproj = New System.ComponentModel.BackgroundWorker()
        Me.BGW_sounds = New System.ComponentModel.BackgroundWorker()
        Me.MenuStrip.SuspendLayout()
        Me.HomeEdit.SuspendLayout()
        Me.Info1.SuspendLayout()
        Me.KeySoundTab.SuspendLayout()
        Me.keyLED1.SuspendLayout()
        Me.keyLED2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.TutorialsToolStripMenuItem, Me.SettingsToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(796, 24)
        Me.MenuStrip.TabIndex = 0
        Me.MenuStrip.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenProjectToolStripMenuItem, Me.SaveProjectToolStripMenuItem, Me.ConvertALSToUnipackToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.ShowShortcutKeys = False
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenProjectToolStripMenuItem
        '
        Me.OpenProjectToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenAbletonProjectToolStripMenuItem, Me.SoundsToolStripMenuItem, Me.OpenKeyLEDToolStripMenuItem})
        Me.OpenProjectToolStripMenuItem.Name = "OpenProjectToolStripMenuItem"
        Me.OpenProjectToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.OpenProjectToolStripMenuItem.Text = "Open Project"
        '
        'OpenAbletonProjectToolStripMenuItem
        '
        Me.OpenAbletonProjectToolStripMenuItem.Name = "OpenAbletonProjectToolStripMenuItem"
        Me.OpenAbletonProjectToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
        Me.OpenAbletonProjectToolStripMenuItem.Text = "Open Ableton Project (Beta)"
        '
        'SoundsToolStripMenuItem
        '
        Me.SoundsToolStripMenuItem.Name = "SoundsToolStripMenuItem"
        Me.SoundsToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
        Me.SoundsToolStripMenuItem.Text = "Open Sound Files"
        '
        'OpenKeyLEDToolStripMenuItem
        '
        Me.OpenKeyLEDToolStripMenuItem.Name = "OpenKeyLEDToolStripMenuItem"
        Me.OpenKeyLEDToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
        Me.OpenKeyLEDToolStripMenuItem.Text = "Open LED Files"
        '
        'SaveProjectToolStripMenuItem
        '
        Me.SaveProjectToolStripMenuItem.Name = "SaveProjectToolStripMenuItem"
        Me.SaveProjectToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.SaveProjectToolStripMenuItem.Text = "Save Project (Loaded Project Only)"
        '
        'ConvertALSToUnipackToolStripMenuItem
        '
        Me.ConvertALSToUnipackToolStripMenuItem.Enabled = False
        Me.ConvertALSToUnipackToolStripMenuItem.Name = "ConvertALSToUnipackToolStripMenuItem"
        Me.ConvertALSToUnipackToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.ConvertALSToUnipackToolStripMenuItem.Text = "Convert Ableton Project to Unipack!"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UnipackToolStripMenuItem, Me.UnipackToolStripMenuItem1})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'UnipackToolStripMenuItem
        '
        Me.UnipackToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnyAbletonToolStripMenuItem, Me.AbletonLive9LiteToolStripMenuItem, Me.AbletonLive9TrialToolStripMenuItem, Me.AbletonLive9SuiteToolStripMenuItem, Me.AbletonLive10ToolStripMenuItem})
        Me.UnipackToolStripMenuItem.Name = "UnipackToolStripMenuItem"
        Me.UnipackToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.UnipackToolStripMenuItem.Text = "Ableton"
        '
        'AnyAbletonToolStripMenuItem
        '
        Me.AnyAbletonToolStripMenuItem.Checked = True
        Me.AnyAbletonToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AnyAbletonToolStripMenuItem.Name = "AnyAbletonToolStripMenuItem"
        Me.AnyAbletonToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.AnyAbletonToolStripMenuItem.Text = "Any Ableton Version"
        '
        'AbletonLive9LiteToolStripMenuItem
        '
        Me.AbletonLive9LiteToolStripMenuItem.Name = "AbletonLive9LiteToolStripMenuItem"
        Me.AbletonLive9LiteToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.AbletonLive9LiteToolStripMenuItem.Text = "Ableton Live 9 Lite"
        '
        'AbletonLive9TrialToolStripMenuItem
        '
        Me.AbletonLive9TrialToolStripMenuItem.Enabled = False
        Me.AbletonLive9TrialToolStripMenuItem.Name = "AbletonLive9TrialToolStripMenuItem"
        Me.AbletonLive9TrialToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.AbletonLive9TrialToolStripMenuItem.Text = "Ableton Live 9 Trial"
        '
        'AbletonLive9SuiteToolStripMenuItem
        '
        Me.AbletonLive9SuiteToolStripMenuItem.Enabled = False
        Me.AbletonLive9SuiteToolStripMenuItem.Name = "AbletonLive9SuiteToolStripMenuItem"
        Me.AbletonLive9SuiteToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.AbletonLive9SuiteToolStripMenuItem.Text = "Ableton Live 9 Suite"
        '
        'AbletonLive10ToolStripMenuItem
        '
        Me.AbletonLive10ToolStripMenuItem.Enabled = False
        Me.AbletonLive10ToolStripMenuItem.Name = "AbletonLive10ToolStripMenuItem"
        Me.AbletonLive10ToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.AbletonLive10ToolStripMenuItem.Text = "Ableton Live 10"
        '
        'UnipackToolStripMenuItem1
        '
        Me.UnipackToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConvertToZipUniToolStripMenuItem})
        Me.UnipackToolStripMenuItem1.Name = "UnipackToolStripMenuItem1"
        Me.UnipackToolStripMenuItem1.Size = New System.Drawing.Size(117, 22)
        Me.UnipackToolStripMenuItem1.Text = "UniPack"
        '
        'ConvertToZipUniToolStripMenuItem
        '
        Me.ConvertToZipUniToolStripMenuItem.Checked = True
        Me.ConvertToZipUniToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ConvertToZipUniToolStripMenuItem.Name = "ConvertToZipUniToolStripMenuItem"
        Me.ConvertToZipUniToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.ConvertToZipUniToolStripMenuItem.Text = "Convert to zip, uni"
        '
        'TutorialsToolStripMenuItem
        '
        Me.TutorialsToolStripMenuItem.Name = "TutorialsToolStripMenuItem"
        Me.TutorialsToolStripMenuItem.Size = New System.Drawing.Size(64, 20)
        Me.TutorialsToolStripMenuItem.Text = "Tutorials"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CheckUpdateToolStripMenuItem, Me.ReportBugsToolStripMenuItem, Me.DeveloperModeToolStripMenuItem, Me.SettingsToolStripMenuItem1, Me.InfoToolStripMenuItem})
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.SettingsToolStripMenuItem.Text = "About"
        '
        'CheckUpdateToolStripMenuItem
        '
        Me.CheckUpdateToolStripMenuItem.Name = "CheckUpdateToolStripMenuItem"
        Me.CheckUpdateToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.CheckUpdateToolStripMenuItem.Text = "Check Update"
        '
        'ReportBugsToolStripMenuItem
        '
        Me.ReportBugsToolStripMenuItem.Name = "ReportBugsToolStripMenuItem"
        Me.ReportBugsToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.ReportBugsToolStripMenuItem.Text = "Report Bugs"
        '
        'DeveloperModeToolStripMenuItem
        '
        Me.DeveloperModeToolStripMenuItem.Name = "DeveloperModeToolStripMenuItem"
        Me.DeveloperModeToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.DeveloperModeToolStripMenuItem.Text = "Developer Mode"
        Me.DeveloperModeToolStripMenuItem.Visible = False
        '
        'SettingsToolStripMenuItem1
        '
        Me.SettingsToolStripMenuItem1.Name = "SettingsToolStripMenuItem1"
        Me.SettingsToolStripMenuItem1.Size = New System.Drawing.Size(163, 22)
        Me.SettingsToolStripMenuItem1.Text = "Settings"
        '
        'InfoToolStripMenuItem
        '
        Me.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem"
        Me.InfoToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.InfoToolStripMenuItem.Text = "Info"
        '
        'sfd
        '
        Me.sfd.AddExtension = False
        '
        'LEDOpen1
        '
        Me.LEDOpen1.FileName = "LEDOpen1"
        '
        'HomeEdit
        '
        Me.HomeEdit.Controls.Add(Me.Info1)
        Me.HomeEdit.Controls.Add(Me.KeySoundTab)
        Me.HomeEdit.Controls.Add(Me.keyLED1)
        Me.HomeEdit.Controls.Add(Me.keyLED2)
        Me.HomeEdit.HotTrack = True
        Me.HomeEdit.Location = New System.Drawing.Point(12, 27)
        Me.HomeEdit.Name = "HomeEdit"
        Me.HomeEdit.SelectedIndex = 0
        Me.HomeEdit.Size = New System.Drawing.Size(784, 522)
        Me.HomeEdit.TabIndex = 1
        '
        'Info1
        '
        Me.Info1.Controls.Add(Me.infoTB3)
        Me.Info1.Controls.Add(Me.Tip1)
        Me.Info1.Controls.Add(Me.Info_SaveButton)
        Me.Info1.Controls.Add(Me.infoT3)
        Me.Info1.Controls.Add(Me.infoT2)
        Me.Info1.Controls.Add(Me.infoTB2)
        Me.Info1.Controls.Add(Me.infoTB1)
        Me.Info1.Controls.Add(Me.infoT1)
        Me.Info1.Location = New System.Drawing.Point(4, 22)
        Me.Info1.Name = "Info1"
        Me.Info1.Padding = New System.Windows.Forms.Padding(3)
        Me.Info1.Size = New System.Drawing.Size(776, 496)
        Me.Info1.TabIndex = 0
        Me.Info1.Text = "Information"
        Me.Info1.UseVisualStyleBackColor = True
        '
        'infoTB3
        '
        Me.infoTB3.Location = New System.Drawing.Point(295, 246)
        Me.infoTB3.Name = "infoTB3"
        Me.infoTB3.ReadOnly = True
        Me.infoTB3.Size = New System.Drawing.Size(200, 21)
        Me.infoTB3.TabIndex = 30
        Me.infoTB3.Text = "1"
        '
        'Tip1
        '
        Me.Tip1.AutoSize = True
        Me.Tip1.Font = New System.Drawing.Font("맑은 고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Tip1.ForeColor = System.Drawing.Color.LightCoral
        Me.Tip1.Location = New System.Drawing.Point(6, 472)
        Me.Tip1.Name = "Tip1"
        Me.Tip1.Size = New System.Drawing.Size(307, 17)
        Me.Tip1.TabIndex = 29
        Me.Tip1.Text = "Tip: You can edit Info after open Ableton Project."
        '
        'Info_SaveButton
        '
        Me.Info_SaveButton.Location = New System.Drawing.Point(586, 391)
        Me.Info_SaveButton.Name = "Info_SaveButton"
        Me.Info_SaveButton.Size = New System.Drawing.Size(169, 95)
        Me.Info_SaveButton.TabIndex = 27
        Me.Info_SaveButton.Text = "Save"
        Me.Info_SaveButton.UseVisualStyleBackColor = True
        '
        'infoT3
        '
        Me.infoT3.AutoSize = True
        Me.infoT3.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.infoT3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.infoT3.Location = New System.Drawing.Point(249, 247)
        Me.infoT3.Name = "infoT3"
        Me.infoT3.Size = New System.Drawing.Size(39, 15)
        Me.infoT3.TabIndex = 26
        Me.infoT3.Text = "Chain"
        '
        'infoT2
        '
        Me.infoT2.AutoSize = True
        Me.infoT2.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.infoT2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.infoT2.Location = New System.Drawing.Point(190, 195)
        Me.infoT2.Name = "infoT2"
        Me.infoT2.Size = New System.Drawing.Size(98, 15)
        Me.infoT2.TabIndex = 24
        Me.infoT2.Text = "Producer Name"
        '
        'infoTB2
        '
        Me.infoTB2.Location = New System.Drawing.Point(295, 194)
        Me.infoTB2.Name = "infoTB2"
        Me.infoTB2.Size = New System.Drawing.Size(200, 21)
        Me.infoTB2.TabIndex = 23
        Me.infoTB2.Text = "UniConverter V1.1.0.3"
        '
        'infoTB1
        '
        Me.infoTB1.Location = New System.Drawing.Point(295, 146)
        Me.infoTB1.Name = "infoTB1"
        Me.infoTB1.Size = New System.Drawing.Size(200, 21)
        Me.infoTB1.TabIndex = 21
        Me.infoTB1.Text = "UniPack Project"
        '
        'infoT1
        '
        Me.infoT1.AutoSize = True
        Me.infoT1.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.infoT1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.infoT1.Location = New System.Drawing.Point(257, 147)
        Me.infoT1.Name = "infoT1"
        Me.infoT1.Size = New System.Drawing.Size(32, 15)
        Me.infoT1.TabIndex = 22
        Me.infoT1.Text = "Title"
        '
        'KeySoundTab
        '
        Me.KeySoundTab.Controls.Add(Me.ks_SearchLabel)
        Me.KeySoundTab.Controls.Add(Me.ks_SearchSound)
        Me.KeySoundTab.Controls.Add(Me.ks_Sound2)
        Me.KeySoundTab.Controls.Add(Me.ks_Sound1)
        Me.KeySoundTab.Controls.Add(Me.ks_SelY)
        Me.KeySoundTab.Controls.Add(Me.ks_SelX)
        Me.KeySoundTab.Controls.Add(Me.ks_SelChain)
        Me.KeySoundTab.Controls.Add(Me.EdKeysButton)
        Me.KeySoundTab.Controls.Add(Me.SaveButton)
        Me.KeySoundTab.Controls.Add(Me.CutSndButton)
        Me.KeySoundTab.Controls.Add(Me.Tip3)
        Me.KeySoundTab.Controls.Add(Me.DevelopingLabel1)
        Me.KeySoundTab.Controls.Add(Me.BackButton)
        Me.KeySoundTab.Controls.Add(Me.GoButton)
        Me.KeySoundTab.Controls.Add(Me.keySound_ListView)
        Me.KeySoundTab.Controls.Add(Me.Sound_ListView)
        Me.KeySoundTab.Location = New System.Drawing.Point(4, 22)
        Me.KeySoundTab.Name = "KeySoundTab"
        Me.KeySoundTab.Padding = New System.Windows.Forms.Padding(3)
        Me.KeySoundTab.Size = New System.Drawing.Size(776, 496)
        Me.KeySoundTab.TabIndex = 1
        Me.KeySoundTab.Text = "keySound"
        Me.KeySoundTab.UseVisualStyleBackColor = True
        '
        'ks_SearchLabel
        '
        Me.ks_SearchLabel.AutoSize = True
        Me.ks_SearchLabel.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.ks_SearchLabel.Location = New System.Drawing.Point(14, 14)
        Me.ks_SearchLabel.Name = "ks_SearchLabel"
        Me.ks_SearchLabel.Size = New System.Drawing.Size(85, 15)
        Me.ks_SearchLabel.TabIndex = 50
        Me.ks_SearchLabel.Text = "Search Sound:"
        '
        'ks_SearchSound
        '
        Me.ks_SearchSound.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.ks_SearchSound.Location = New System.Drawing.Point(103, 13)
        Me.ks_SearchSound.Name = "ks_SearchSound"
        Me.ks_SearchSound.Size = New System.Drawing.Size(141, 23)
        Me.ks_SearchSound.TabIndex = 49
        '
        'ks_Sound2
        '
        Me.ks_Sound2.AutoSize = True
        Me.ks_Sound2.Font = New System.Drawing.Font("Ubuntu", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ks_Sound2.Location = New System.Drawing.Point(469, 10)
        Me.ks_Sound2.Name = "ks_Sound2"
        Me.ks_Sound2.Size = New System.Drawing.Size(127, 19)
        Me.ks_Sound2.TabIndex = 48
        Me.ks_Sound2.Text = "Sounds In Button"
        '
        'ks_Sound1
        '
        Me.ks_Sound1.AutoSize = True
        Me.ks_Sound1.Font = New System.Drawing.Font("Ubuntu", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ks_Sound1.Location = New System.Drawing.Point(13, 39)
        Me.ks_Sound1.Name = "ks_Sound1"
        Me.ks_Sound1.Size = New System.Drawing.Size(103, 19)
        Me.ks_Sound1.TabIndex = 47
        Me.ks_Sound1.Text = "Sound Library"
        '
        'ks_SelY
        '
        Me.ks_SelY.FormattingEnabled = True
        Me.ks_SelY.Location = New System.Drawing.Point(431, 9)
        Me.ks_SelY.Name = "ks_SelY"
        Me.ks_SelY.Size = New System.Drawing.Size(29, 20)
        Me.ks_SelY.TabIndex = 46
        Me.ks_SelY.Text = "y"
        '
        'ks_SelX
        '
        Me.ks_SelX.FormattingEnabled = True
        Me.ks_SelX.Location = New System.Drawing.Point(398, 9)
        Me.ks_SelX.Name = "ks_SelX"
        Me.ks_SelX.Size = New System.Drawing.Size(29, 20)
        Me.ks_SelX.TabIndex = 45
        Me.ks_SelX.Text = "x"
        '
        'ks_SelChain
        '
        Me.ks_SelChain.FormattingEnabled = True
        Me.ks_SelChain.Location = New System.Drawing.Point(363, 9)
        Me.ks_SelChain.Name = "ks_SelChain"
        Me.ks_SelChain.Size = New System.Drawing.Size(29, 20)
        Me.ks_SelChain.TabIndex = 44
        Me.ks_SelChain.Text = "c"
        '
        'EdKeysButton
        '
        Me.EdKeysButton.Location = New System.Drawing.Point(473, 408)
        Me.EdKeysButton.Name = "EdKeysButton"
        Me.EdKeysButton.Size = New System.Drawing.Size(152, 38)
        Me.EdKeysButton.TabIndex = 43
        Me.EdKeysButton.Text = "Edit keySound!"
        Me.EdKeysButton.UseVisualStyleBackColor = True
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(631, 408)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(125, 78)
        Me.SaveButton.TabIndex = 42
        Me.SaveButton.Text = "Save"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'CutSndButton
        '
        Me.CutSndButton.Location = New System.Drawing.Point(473, 452)
        Me.CutSndButton.Name = "CutSndButton"
        Me.CutSndButton.Size = New System.Drawing.Size(152, 34)
        Me.CutSndButton.TabIndex = 41
        Me.CutSndButton.Text = "Cut Sound "
        Me.CutSndButton.UseVisualStyleBackColor = True
        '
        'Tip3
        '
        Me.Tip3.AutoSize = True
        Me.Tip3.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Tip3.ForeColor = System.Drawing.Color.LightCoral
        Me.Tip3.Location = New System.Drawing.Point(6, 463)
        Me.Tip3.Name = "Tip3"
        Me.Tip3.Size = New System.Drawing.Size(268, 30)
        Me.Tip3.TabIndex = 36
        Me.Tip3.Text = "Tip: If sound file didn't split, don't put it here." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Instead, you can cut sound o" &
    "n Cut Sound Form."
        '
        'DevelopingLabel1
        '
        Me.DevelopingLabel1.AutoSize = True
        Me.DevelopingLabel1.Font = New System.Drawing.Font("Adobe Heiti Std R", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DevelopingLabel1.Location = New System.Drawing.Point(115, 190)
        Me.DevelopingLabel1.Name = "DevelopingLabel1"
        Me.DevelopingLabel1.Size = New System.Drawing.Size(544, 68)
        Me.DevelopingLabel1.TabIndex = 35
        Me.DevelopingLabel1.Text = "We are developing ""Converting keySound""!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "                           Coming Soon." &
    ".."
        '
        'BackButton
        '
        Me.BackButton.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.BackButton.Location = New System.Drawing.Point(335, 237)
        Me.BackButton.Name = "BackButton"
        Me.BackButton.Size = New System.Drawing.Size(106, 66)
        Me.BackButton.TabIndex = 34
        Me.BackButton.Text = "<--"
        Me.BackButton.UseVisualStyleBackColor = True
        '
        'GoButton
        '
        Me.GoButton.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GoButton.Location = New System.Drawing.Point(335, 141)
        Me.GoButton.Name = "GoButton"
        Me.GoButton.Size = New System.Drawing.Size(106, 69)
        Me.GoButton.TabIndex = 33
        Me.GoButton.Text = "-->"
        Me.GoButton.UseVisualStyleBackColor = True
        '
        'keySound_ListView
        '
        Me.keySound_ListView.AllowDrop = True
        Me.keySound_ListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.SortingNumber, Me.FileName2, Me.Length2, Me.Loop2})
        Me.keySound_ListView.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.keySound_ListView.FullRowSelect = True
        Me.keySound_ListView.Location = New System.Drawing.Point(473, 32)
        Me.keySound_ListView.Name = "keySound_ListView"
        Me.keySound_ListView.Size = New System.Drawing.Size(283, 370)
        Me.keySound_ListView.TabIndex = 31
        Me.keySound_ListView.UseCompatibleStateImageBehavior = False
        Me.keySound_ListView.View = System.Windows.Forms.View.Details
        '
        'SortingNumber
        '
        Me.SortingNumber.Text = "#"
        Me.SortingNumber.Width = 26
        '
        'FileName2
        '
        Me.FileName2.Text = "Loaded Sound Files"
        Me.FileName2.Width = 120
        '
        'Length2
        '
        Me.Length2.Text = "Length"
        Me.Length2.Width = 86
        '
        'Loop2
        '
        Me.Loop2.Text = "Loop"
        Me.Loop2.Width = 46
        '
        'Sound_ListView
        '
        Me.Sound_ListView.AllowDrop = True
        Me.Sound_ListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FileName, Me.Length, Me.AssingedButtons})
        Me.Sound_ListView.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Sound_ListView.FullRowSelect = True
        Me.Sound_ListView.Location = New System.Drawing.Point(17, 61)
        Me.Sound_ListView.Name = "Sound_ListView"
        Me.Sound_ListView.Size = New System.Drawing.Size(294, 390)
        Me.Sound_ListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.Sound_ListView.TabIndex = 24
        Me.Sound_ListView.UseCompatibleStateImageBehavior = False
        Me.Sound_ListView.View = System.Windows.Forms.View.Details
        '
        'FileName
        '
        Me.FileName.Text = "File Name"
        Me.FileName.Width = 82
        '
        'Length
        '
        Me.Length.Text = "Length"
        Me.Length.Width = 91
        '
        'AssingedButtons
        '
        Me.AssingedButtons.Text = "Assinged Buttons"
        Me.AssingedButtons.Width = 116
        '
        'keyLED1
        '
        Me.keyLED1.Controls.Add(Me.DevelopingLabel2)
        Me.keyLED1.Location = New System.Drawing.Point(4, 22)
        Me.keyLED1.Name = "keyLED1"
        Me.keyLED1.Size = New System.Drawing.Size(776, 496)
        Me.keyLED1.TabIndex = 2
        Me.keyLED1.Text = "keyLED"
        Me.keyLED1.UseVisualStyleBackColor = True
        '
        'DevelopingLabel2
        '
        Me.DevelopingLabel2.AutoSize = True
        Me.DevelopingLabel2.Font = New System.Drawing.Font("Adobe Heiti Std R", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DevelopingLabel2.Location = New System.Drawing.Point(83, 191)
        Me.DevelopingLabel2.Name = "DevelopingLabel2"
        Me.DevelopingLabel2.Size = New System.Drawing.Size(605, 80)
        Me.DevelopingLabel2.TabIndex = 36
        Me.DevelopingLabel2.Text = "We are developing ""Converting keyLED""!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "                           Coming Soon..." &
    ""
        '
        'keyLED2
        '
        Me.keyLED2.Controls.Add(Me.DevelopingLabel3)
        Me.keyLED2.Controls.Add(Me.keyLEDBetaButton)
        Me.keyLED2.Location = New System.Drawing.Point(4, 22)
        Me.keyLED2.Name = "keyLED2"
        Me.keyLED2.Size = New System.Drawing.Size(776, 496)
        Me.keyLED2.TabIndex = 3
        Me.keyLED2.Text = "keyLED (MIDI Extension)"
        Me.keyLED2.UseVisualStyleBackColor = True
        '
        'DevelopingLabel3
        '
        Me.DevelopingLabel3.AutoSize = True
        Me.DevelopingLabel3.Font = New System.Drawing.Font("Adobe Heiti Std R", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DevelopingLabel3.Location = New System.Drawing.Point(28, 303)
        Me.DevelopingLabel3.Name = "DevelopingLabel3"
        Me.DevelopingLabel3.Size = New System.Drawing.Size(719, 68)
        Me.DevelopingLabel3.TabIndex = 39
        Me.DevelopingLabel3.Text = "We are developing ""Converting keyLED (MIDI Extension)""!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "                        " &
    "                Coming Soon..."
        '
        'keyLEDBetaButton
        '
        Me.keyLEDBetaButton.Font = New System.Drawing.Font("Ubuntu", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.keyLEDBetaButton.Location = New System.Drawing.Point(199, 117)
        Me.keyLEDBetaButton.Name = "keyLEDBetaButton"
        Me.keyLEDBetaButton.Size = New System.Drawing.Size(347, 149)
        Me.keyLEDBetaButton.TabIndex = 38
        Me.keyLEDBetaButton.Text = "Edit keyLED! (MIDI Extension) (Advanced)"
        Me.keyLEDBetaButton.UseVisualStyleBackColor = True
        '
        'BGW_keyLED
        '
        '
        'BGW_sounds
        '
        '
        'MainProject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(796, 547)
        Me.Controls.Add(Me.HomeEdit)
        Me.Controls.Add(Me.MenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "MainProject"
        Me.Text = "UniConverter Beta 4"
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.HomeEdit.ResumeLayout(False)
        Me.Info1.ResumeLayout(False)
        Me.Info1.PerformLayout()
        Me.KeySoundTab.ResumeLayout(False)
        Me.KeySoundTab.PerformLayout()
        Me.keyLED1.ResumeLayout(False)
        Me.keyLED1.PerformLayout()
        Me.keyLED2.ResumeLayout(False)
        Me.keyLED2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenProjectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveProjectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ofd As OpenFileDialog
    Friend WithEvents TutorialsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents UnipackToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents sfd As SaveFileDialog
    Friend WithEvents SoundsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LEDOpen1 As OpenFileDialog
    Friend WithEvents ReportBugsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HomeEdit As TabControl
    Friend WithEvents Info1 As TabPage
    Friend WithEvents KeySoundTab As TabPage
    Friend WithEvents AbletonLive9LiteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AbletonLive9TrialToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AbletonLive9SuiteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AbletonLive10ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UnipackToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents OpenAbletonProjectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConvertToZipUniToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConvertALSToUnipackToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents infoT3 As Label
    Friend WithEvents infoT2 As Label
    Friend WithEvents infoTB2 As TextBox
    Friend WithEvents infoTB1 As TextBox
    Friend WithEvents infoT1 As Label
    Friend WithEvents Info_SaveButton As Button
    Friend WithEvents Sound_ListView As ListView
    Friend WithEvents FileName As ColumnHeader
    Friend WithEvents Tip1 As Label
    Friend WithEvents AnyAbletonToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BackButton As Button
    Friend WithEvents GoButton As Button
    Friend WithEvents Tip3 As Label
    Friend WithEvents EdKeysButton As Button
    Friend WithEvents SaveButton As Button
    Friend WithEvents CutSndButton As Button
    Friend WithEvents DevelopingLabel1 As Label
    Friend WithEvents keySound_ListView As ListView
    Friend WithEvents FileName2 As ColumnHeader
    Friend WithEvents keyLED1 As TabPage
    Friend WithEvents keyLED2 As TabPage
    Friend WithEvents CheckUpdateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ks_SelY As ComboBox
    Friend WithEvents ks_SelX As ComboBox
    Friend WithEvents ks_SelChain As ComboBox
    Friend WithEvents DevelopingLabel2 As Label
    Friend WithEvents infoTB3 As TextBox
    Friend WithEvents Length As ColumnHeader
    Friend WithEvents AssingedButtons As ColumnHeader
    Friend WithEvents ks_Sound1 As Label
    Friend WithEvents SortingNumber As ColumnHeader
    Friend WithEvents Length2 As ColumnHeader
    Friend WithEvents Loop2 As ColumnHeader
    Friend WithEvents ks_Sound2 As Label
    Friend WithEvents ks_SearchLabel As Label
    Friend WithEvents ks_SearchSound As TextBox
    Friend WithEvents BGW_keyLED As System.ComponentModel.BackgroundWorker
    Friend WithEvents DeveloperModeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenKeyLEDToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents keyLEDBetaButton As Button
    Friend WithEvents DevelopingLabel3 As Label
    Friend WithEvents BGW_ablproj As System.ComponentModel.BackgroundWorker
    Friend WithEvents BGW_sounds As System.ComponentModel.BackgroundWorker
End Class

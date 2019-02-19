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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenAbletonProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SoundsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KeyLEDBetaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.ReportBugsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.InfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeveloperToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.LEDOpen1 = New System.Windows.Forms.OpenFileDialog()
        Me.HomeEdit1 = New System.Windows.Forms.TabControl()
        Me.Info1 = New System.Windows.Forms.TabPage()
        Me.Tip2 = New System.Windows.Forms.Label()
        Me.infoTB3 = New System.Windows.Forms.DomainUpDown()
        Me.Tip1 = New System.Windows.Forms.Label()
        Me.Info_SaveButton = New System.Windows.Forms.Button()
        Me.infoT3 = New System.Windows.Forms.Label()
        Me.infoT2 = New System.Windows.Forms.Label()
        Me.infoTB2 = New System.Windows.Forms.TextBox()
        Me.infoTB1 = New System.Windows.Forms.TextBox()
        Me.infoT1 = New System.Windows.Forms.Label()
        Me.KeyS1 = New System.Windows.Forms.TabPage()
        Me.EdKeysButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.CutSndButton = New System.Windows.Forms.Button()
        Me.Tip3 = New System.Windows.Forms.Label()
        Me.DevelopingLabel1 = New System.Windows.Forms.Label()
        Me.BackButton = New System.Windows.Forms.Button()
        Me.GoButton = New System.Windows.Forms.Button()
        Me.keySound_ListView = New System.Windows.Forms.ListView()
        Me.keySound1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Sound_ListView = New System.Windows.Forms.ListView()
        Me.FileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.keyLED1 = New System.Windows.Forms.TabPage()
        Me.keyLED2 = New System.Windows.Forms.TabPage()
        Me.MenuStrip1.SuspendLayout()
        Me.HomeEdit1.SuspendLayout()
        Me.Info1.SuspendLayout()
        Me.KeyS1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.TutorialsToolStripMenuItem, Me.SettingsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(699, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenProjectToolStripMenuItem, Me.SaveProjectToolStripMenuItem, Me.ConvertALSToUnipackToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenProjectToolStripMenuItem
        '
        Me.OpenProjectToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenAbletonProjectToolStripMenuItem, Me.SoundsToolStripMenuItem, Me.KeyLEDBetaToolStripMenuItem})
        Me.OpenProjectToolStripMenuItem.Name = "OpenProjectToolStripMenuItem"
        Me.OpenProjectToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
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
        Me.SoundsToolStripMenuItem.Text = "Open Sounds (Advanced)"
        '
        'KeyLEDBetaToolStripMenuItem
        '
        Me.KeyLEDBetaToolStripMenuItem.Name = "KeyLEDBetaToolStripMenuItem"
        Me.KeyLEDBetaToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
        Me.KeyLEDBetaToolStripMenuItem.Text = "keyLED (Beta, Advanced)"
        '
        'SaveProjectToolStripMenuItem
        '
        Me.SaveProjectToolStripMenuItem.Name = "SaveProjectToolStripMenuItem"
        Me.SaveProjectToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.SaveProjectToolStripMenuItem.Text = "Save Project (Loaded Project Only)"
        '
        'ConvertALSToUnipackToolStripMenuItem
        '
        Me.ConvertALSToUnipackToolStripMenuItem.Enabled = False
        Me.ConvertALSToUnipackToolStripMenuItem.Name = "ConvertALSToUnipackToolStripMenuItem"
        Me.ConvertALSToUnipackToolStripMenuItem.Size = New System.Drawing.Size(261, 22)
        Me.ConvertALSToUnipackToolStripMenuItem.Text = "Convert ALS to Unipack!"
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
        Me.UnipackToolStripMenuItem1.Text = "Unipack"
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
        Me.SettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReportBugsToolStripMenuItem, Me.SettingsToolStripMenuItem1, Me.InfoToolStripMenuItem, Me.DeveloperToolStripMenuItem})
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.SettingsToolStripMenuItem.Text = "About"
        '
        'ReportBugsToolStripMenuItem
        '
        Me.ReportBugsToolStripMenuItem.Name = "ReportBugsToolStripMenuItem"
        Me.ReportBugsToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.ReportBugsToolStripMenuItem.Text = "Report Bugs"
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
        'DeveloperToolStripMenuItem
        '
        Me.DeveloperToolStripMenuItem.Enabled = False
        Me.DeveloperToolStripMenuItem.Name = "DeveloperToolStripMenuItem"
        Me.DeveloperToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.DeveloperToolStripMenuItem.Text = "Developer Mode"
        Me.DeveloperToolStripMenuItem.Visible = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.AddExtension = False
        '
        'LEDOpen1
        '
        Me.LEDOpen1.FileName = "LEDOpen1"
        '
        'HomeEdit1
        '
        Me.HomeEdit1.Controls.Add(Me.Info1)
        Me.HomeEdit1.Controls.Add(Me.KeyS1)
        Me.HomeEdit1.Controls.Add(Me.keyLED1)
        Me.HomeEdit1.Controls.Add(Me.keyLED2)
        Me.HomeEdit1.HotTrack = True
        Me.HomeEdit1.Location = New System.Drawing.Point(12, 27)
        Me.HomeEdit1.Name = "HomeEdit1"
        Me.HomeEdit1.SelectedIndex = 0
        Me.HomeEdit1.Size = New System.Drawing.Size(677, 449)
        Me.HomeEdit1.TabIndex = 1
        '
        'Info1
        '
        Me.Info1.Controls.Add(Me.Tip2)
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
        Me.Info1.Size = New System.Drawing.Size(669, 423)
        Me.Info1.TabIndex = 0
        Me.Info1.Text = "Information"
        Me.Info1.UseVisualStyleBackColor = True
        '
        'Tip2
        '
        Me.Tip2.AutoSize = True
        Me.Tip2.Location = New System.Drawing.Point(191, 184)
        Me.Tip2.Name = "Tip2"
        Me.Tip2.Size = New System.Drawing.Size(219, 12)
        Me.Tip2.TabIndex = 31
        Me.Tip2.Text = "Don't edit this number! (Recommend)"
        '
        'infoTB3
        '
        Me.infoTB3.Items.Add("8")
        Me.infoTB3.Items.Add("7")
        Me.infoTB3.Items.Add("6")
        Me.infoTB3.Items.Add("5")
        Me.infoTB3.Items.Add("4")
        Me.infoTB3.Items.Add("3")
        Me.infoTB3.Items.Add("2")
        Me.infoTB3.Items.Add("1")
        Me.infoTB3.Location = New System.Drawing.Point(225, 154)
        Me.infoTB3.Name = "infoTB3"
        Me.infoTB3.Size = New System.Drawing.Size(201, 21)
        Me.infoTB3.TabIndex = 30
        Me.infoTB3.Text = "1"
        '
        'Tip1
        '
        Me.Tip1.AutoSize = True
        Me.Tip1.Font = New System.Drawing.Font("맑은 고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Tip1.ForeColor = System.Drawing.Color.LightCoral
        Me.Tip1.Location = New System.Drawing.Point(6, 400)
        Me.Tip1.Name = "Tip1"
        Me.Tip1.Size = New System.Drawing.Size(307, 17)
        Me.Tip1.TabIndex = 29
        Me.Tip1.Text = "Tip: You can edit Info after open Ableton Project."
        '
        'Info_SaveButton
        '
        Me.Info_SaveButton.Location = New System.Drawing.Point(536, 355)
        Me.Info_SaveButton.Name = "Info_SaveButton"
        Me.Info_SaveButton.Size = New System.Drawing.Size(95, 44)
        Me.Info_SaveButton.TabIndex = 27
        Me.Info_SaveButton.Text = "Save"
        Me.Info_SaveButton.UseVisualStyleBackColor = True
        '
        'infoT3
        '
        Me.infoT3.AutoSize = True
        Me.infoT3.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.infoT3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.infoT3.Location = New System.Drawing.Point(180, 154)
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
        Me.infoT2.Location = New System.Drawing.Point(121, 102)
        Me.infoT2.Name = "infoT2"
        Me.infoT2.Size = New System.Drawing.Size(98, 15)
        Me.infoT2.TabIndex = 24
        Me.infoT2.Text = "Producer Name"
        '
        'infoTB2
        '
        Me.infoTB2.Location = New System.Drawing.Point(226, 101)
        Me.infoTB2.Name = "infoTB2"
        Me.infoTB2.Size = New System.Drawing.Size(200, 21)
        Me.infoTB2.TabIndex = 23
        Me.infoTB2.Text = "UniConverter V1.1.0.3"
        '
        'infoTB1
        '
        Me.infoTB1.Location = New System.Drawing.Point(226, 53)
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
        Me.infoT1.Location = New System.Drawing.Point(188, 54)
        Me.infoT1.Name = "infoT1"
        Me.infoT1.Size = New System.Drawing.Size(32, 15)
        Me.infoT1.TabIndex = 22
        Me.infoT1.Text = "Title"
        '
        'KeyS1
        '
        Me.KeyS1.Controls.Add(Me.EdKeysButton)
        Me.KeyS1.Controls.Add(Me.SaveButton)
        Me.KeyS1.Controls.Add(Me.CutSndButton)
        Me.KeyS1.Controls.Add(Me.Tip3)
        Me.KeyS1.Controls.Add(Me.DevelopingLabel1)
        Me.KeyS1.Controls.Add(Me.BackButton)
        Me.KeyS1.Controls.Add(Me.GoButton)
        Me.KeyS1.Controls.Add(Me.keySound_ListView)
        Me.KeyS1.Controls.Add(Me.Sound_ListView)
        Me.KeyS1.Location = New System.Drawing.Point(4, 22)
        Me.KeyS1.Name = "KeyS1"
        Me.KeyS1.Padding = New System.Windows.Forms.Padding(3)
        Me.KeyS1.Size = New System.Drawing.Size(669, 423)
        Me.KeyS1.TabIndex = 1
        Me.KeyS1.Text = "keySound"
        Me.KeyS1.UseVisualStyleBackColor = True
        '
        'EdKeysButton
        '
        Me.EdKeysButton.Location = New System.Drawing.Point(407, 347)
        Me.EdKeysButton.Name = "EdKeysButton"
        Me.EdKeysButton.Size = New System.Drawing.Size(103, 29)
        Me.EdKeysButton.TabIndex = 43
        Me.EdKeysButton.Text = "Edit keySound!"
        Me.EdKeysButton.UseVisualStyleBackColor = True
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(516, 347)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(125, 64)
        Me.SaveButton.TabIndex = 42
        Me.SaveButton.Text = "Save"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'CutSndButton
        '
        Me.CutSndButton.Location = New System.Drawing.Point(407, 382)
        Me.CutSndButton.Name = "CutSndButton"
        Me.CutSndButton.Size = New System.Drawing.Size(103, 29)
        Me.CutSndButton.TabIndex = 41
        Me.CutSndButton.Text = "Cut Sound "
        Me.CutSndButton.UseVisualStyleBackColor = True
        '
        'Tip3
        '
        Me.Tip3.AutoSize = True
        Me.Tip3.ForeColor = System.Drawing.Color.LightCoral
        Me.Tip3.Location = New System.Drawing.Point(6, 390)
        Me.Tip3.Name = "Tip3"
        Me.Tip3.Size = New System.Drawing.Size(278, 24)
        Me.Tip3.TabIndex = 36
        Me.Tip3.Text = "Tip: If sound file didn't split, don't put it here." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Instead, you can cut sound o" &
    "n Cut Sound Form."
        '
        'DevelopingLabel1
        '
        Me.DevelopingLabel1.AutoSize = True
        Me.DevelopingLabel1.Font = New System.Drawing.Font("Adobe Heiti Std R", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DevelopingLabel1.Location = New System.Drawing.Point(59, 154)
        Me.DevelopingLabel1.Name = "DevelopingLabel1"
        Me.DevelopingLabel1.Size = New System.Drawing.Size(544, 68)
        Me.DevelopingLabel1.TabIndex = 35
        Me.DevelopingLabel1.Text = "We are developing ""Converting keySound""!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "                           Coming Soon." &
    ".."
        '
        'BackButton
        '
        Me.BackButton.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.BackButton.Location = New System.Drawing.Point(289, 196)
        Me.BackButton.Name = "BackButton"
        Me.BackButton.Size = New System.Drawing.Size(79, 44)
        Me.BackButton.TabIndex = 34
        Me.BackButton.Text = "<--"
        Me.BackButton.UseVisualStyleBackColor = True
        '
        'GoButton
        '
        Me.GoButton.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GoButton.Location = New System.Drawing.Point(289, 134)
        Me.GoButton.Name = "GoButton"
        Me.GoButton.Size = New System.Drawing.Size(79, 44)
        Me.GoButton.TabIndex = 33
        Me.GoButton.Text = "-->"
        Me.GoButton.UseVisualStyleBackColor = True
        '
        'keySound_ListView
        '
        Me.keySound_ListView.AllowDrop = True
        Me.keySound_ListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.keySound1})
        Me.keySound_ListView.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.keySound_ListView.FullRowSelect = True
        Me.keySound_ListView.Location = New System.Drawing.Point(407, 6)
        Me.keySound_ListView.MultiSelect = False
        Me.keySound_ListView.Name = "keySound_ListView"
        Me.keySound_ListView.Size = New System.Drawing.Size(234, 335)
        Me.keySound_ListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.keySound_ListView.TabIndex = 31
        Me.keySound_ListView.UseCompatibleStateImageBehavior = False
        Me.keySound_ListView.View = System.Windows.Forms.View.Details
        '
        'keySound1
        '
        Me.keySound1.Text = "Loaded Sound Files"
        Me.keySound1.Width = 229
        '
        'Sound_ListView
        '
        Me.Sound_ListView.AllowDrop = True
        Me.Sound_ListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FileName})
        Me.Sound_ListView.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Sound_ListView.FullRowSelect = True
        Me.Sound_ListView.Location = New System.Drawing.Point(23, 6)
        Me.Sound_ListView.MultiSelect = False
        Me.Sound_ListView.Name = "Sound_ListView"
        Me.Sound_ListView.Size = New System.Drawing.Size(234, 361)
        Me.Sound_ListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.Sound_ListView.TabIndex = 24
        Me.Sound_ListView.UseCompatibleStateImageBehavior = False
        Me.Sound_ListView.View = System.Windows.Forms.View.Details
        '
        'FileName
        '
        Me.FileName.Text = "Sound File Name"
        Me.FileName.Width = 229
        '
        'keyLED1
        '
        Me.keyLED1.Location = New System.Drawing.Point(4, 22)
        Me.keyLED1.Name = "keyLED1"
        Me.keyLED1.Size = New System.Drawing.Size(669, 423)
        Me.keyLED1.TabIndex = 2
        Me.keyLED1.Text = "keyLED"
        Me.keyLED1.UseVisualStyleBackColor = True
        '
        'keyLED2
        '
        Me.keyLED2.Location = New System.Drawing.Point(4, 22)
        Me.keyLED2.Name = "keyLED2"
        Me.keyLED2.Size = New System.Drawing.Size(669, 423)
        Me.keyLED2.TabIndex = 3
        Me.keyLED2.Text = "keyLED (mid)"
        Me.keyLED2.UseVisualStyleBackColor = True
        '
        'MainProject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(699, 485)
        Me.Controls.Add(Me.HomeEdit1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainProject"
        Me.Text = "UniConverter Beta 4"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.HomeEdit1.ResumeLayout(False)
        Me.Info1.ResumeLayout(False)
        Me.Info1.PerformLayout()
        Me.KeyS1.ResumeLayout(False)
        Me.KeyS1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenProjectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveProjectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents TutorialsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents DeveloperToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UnipackToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents SoundsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents KeyLEDBetaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LEDOpen1 As OpenFileDialog
    Friend WithEvents ReportBugsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HomeEdit1 As TabControl
    Friend WithEvents Info1 As TabPage
    Friend WithEvents KeyS1 As TabPage
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
    Friend WithEvents Tip2 As Label
    Friend WithEvents infoTB3 As DomainUpDown
    Friend WithEvents BackButton As Button
    Friend WithEvents GoButton As Button
    Friend WithEvents Tip3 As Label
    Friend WithEvents EdKeysButton As Button
    Friend WithEvents SaveButton As Button
    Friend WithEvents CutSndButton As Button
    Friend WithEvents DevelopingLabel1 As Label
    Friend WithEvents keySound_ListView As ListView
    Friend WithEvents keySound1 As ColumnHeader
    Friend WithEvents keyLED1 As TabPage
    Friend WithEvents keyLED2 As TabPage
End Class

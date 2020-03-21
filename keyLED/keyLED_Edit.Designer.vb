<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class keyLED_Edit
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
        Me.components = New System.ComponentModel.Container()
        Me.UniLED_Edit = New FastColoredTextBoxNS.FastColoredTextBox()
        Me.CopyButton = New System.Windows.Forms.Button()
        Me.AblLED = New System.Windows.Forms.Label()
        Me.UniLED = New System.Windows.Forms.Label()
        Me.LED_ListView = New System.Windows.Forms.ListView()
        Me.FileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GazuaButton = New System.Windows.Forms.Button()
        Me.ALGModeBox = New System.Windows.Forms.ComboBox()
        Me.UniLED1 = New System.Windows.Forms.Label()
        Me.GAZUA_ = New System.ComponentModel.BackgroundWorker()
        Me.TestButton = New System.Windows.Forms.Button()
        Me.SpeedTrackBar = New System.Windows.Forms.TrackBar()
        Me.spTipLb = New System.Windows.Forms.Label()
        Me.spLb = New System.Windows.Forms.Label()
        Me.TimeLabel = New System.Windows.Forms.Label()
        Me.LEDExButton = New System.Windows.Forms.Button()
        CType(Me.UniLED_Edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpeedTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UniLED_Edit
        '
        Me.UniLED_Edit.AutoCompleteBracketsList = New Char() {Global.Microsoft.VisualBasic.ChrW(40), Global.Microsoft.VisualBasic.ChrW(41), Global.Microsoft.VisualBasic.ChrW(123), Global.Microsoft.VisualBasic.ChrW(125), Global.Microsoft.VisualBasic.ChrW(91), Global.Microsoft.VisualBasic.ChrW(93), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(39), Global.Microsoft.VisualBasic.ChrW(39)}
        Me.UniLED_Edit.AutoScrollMinSize = New System.Drawing.Size(27, 14)
        Me.UniLED_Edit.BackBrush = Nothing
        Me.UniLED_Edit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UniLED_Edit.CharHeight = 14
        Me.UniLED_Edit.CharWidth = 8
        Me.UniLED_Edit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.UniLED_Edit.DisabledColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.UniLED_Edit.Enabled = False
        Me.UniLED_Edit.Font = New System.Drawing.Font("Courier New", 9.75!)
        Me.UniLED_Edit.IsReplaceMode = False
        Me.UniLED_Edit.Location = New System.Drawing.Point(360, 63)
        Me.UniLED_Edit.Name = "UniLED_Edit"
        Me.UniLED_Edit.Paddings = New System.Windows.Forms.Padding(0)
        Me.UniLED_Edit.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.UniLED_Edit.Size = New System.Drawing.Size(205, 355)
        Me.UniLED_Edit.TabIndex = 17
        Me.UniLED_Edit.Zoom = 100
        '
        'CopyButton
        '
        Me.CopyButton.Enabled = False
        Me.CopyButton.Font = New System.Drawing.Font("NanumBarunGothicOTF", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.CopyButton.Location = New System.Drawing.Point(463, 427)
        Me.CopyButton.Name = "CopyButton"
        Me.CopyButton.Size = New System.Drawing.Size(102, 41)
        Me.CopyButton.TabIndex = 19
        Me.CopyButton.Text = "Copy"
        Me.CopyButton.UseVisualStyleBackColor = True
        '
        'AblLED
        '
        Me.AblLED.AutoSize = True
        Me.AblLED.Font = New System.Drawing.Font("NanumBarunGothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.AblLED.Location = New System.Drawing.Point(64, 41)
        Me.AblLED.Name = "AblLED"
        Me.AblLED.Size = New System.Drawing.Size(120, 17)
        Me.AblLED.TabIndex = 21
        Me.AblLED.Text = "Ableton LED File"
        '
        'UniLED
        '
        Me.UniLED.AutoSize = True
        Me.UniLED.Font = New System.Drawing.Font("NanumBarunGothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UniLED.Location = New System.Drawing.Point(403, 41)
        Me.UniLED.Name = "UniLED"
        Me.UniLED.Size = New System.Drawing.Size(126, 17)
        Me.UniLED.TabIndex = 22
        Me.UniLED.Text = "UniPack LED Text"
        '
        'LED_ListView
        '
        Me.LED_ListView.AllowDrop = True
        Me.LED_ListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FileName})
        Me.LED_ListView.Font = New System.Drawing.Font("NanumGothic", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.LED_ListView.FullRowSelect = True
        Me.LED_ListView.HideSelection = False
        Me.LED_ListView.Location = New System.Drawing.Point(10, 63)
        Me.LED_ListView.MultiSelect = False
        Me.LED_ListView.Name = "LED_ListView"
        Me.LED_ListView.Size = New System.Drawing.Size(234, 355)
        Me.LED_ListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.LED_ListView.TabIndex = 23
        Me.LED_ListView.UseCompatibleStateImageBehavior = False
        Me.LED_ListView.View = System.Windows.Forms.View.Details
        '
        'FileName
        '
        Me.FileName.Text = "File Name"
        Me.FileName.Width = 229
        '
        'GazuaButton
        '
        Me.GazuaButton.Font = New System.Drawing.Font("Ubuntu", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GazuaButton.Location = New System.Drawing.Point(263, 210)
        Me.GazuaButton.Name = "GazuaButton"
        Me.GazuaButton.Size = New System.Drawing.Size(75, 68)
        Me.GazuaButton.TabIndex = 24
        Me.GazuaButton.Text = "-->"
        Me.GazuaButton.UseVisualStyleBackColor = True
        '
        'ALGModeBox
        '
        Me.ALGModeBox.Font = New System.Drawing.Font("NanumGothicExtraBold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ALGModeBox.FormattingEnabled = True
        Me.ALGModeBox.Items.AddRange(New Object() {"Ableton Live MIDI (Drum Rack Layout)", "Non-Convert (Developer Mode)"})
        Me.ALGModeBox.Location = New System.Drawing.Point(12, 508)
        Me.ALGModeBox.Name = "ALGModeBox"
        Me.ALGModeBox.Size = New System.Drawing.Size(232, 23)
        Me.ALGModeBox.TabIndex = 26
        Me.ALGModeBox.Text = "Ableton Live MIDI (Drum Rack Layout)"
        '
        'UniLED1
        '
        Me.UniLED1.AutoSize = True
        Me.UniLED1.Font = New System.Drawing.Font("NanumBarunGothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UniLED1.Location = New System.Drawing.Point(275, 13)
        Me.UniLED1.Name = "UniLED1"
        Me.UniLED1.Size = New System.Drawing.Size(94, 15)
        Me.UniLED1.TabIndex = 27
        Me.UniLED1.Text = "File Name: N/A"
        '
        'GAZUA_
        '
        Me.GAZUA_.WorkerSupportsCancellation = True
        '
        'TestButton
        '
        Me.TestButton.Enabled = False
        Me.TestButton.Font = New System.Drawing.Font("NanumBarunGothicOTF", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TestButton.Location = New System.Drawing.Point(360, 427)
        Me.TestButton.Name = "TestButton"
        Me.TestButton.Size = New System.Drawing.Size(97, 41)
        Me.TestButton.TabIndex = 31
        Me.TestButton.Text = "Test"
        Me.TestButton.UseVisualStyleBackColor = True
        '
        'SpeedTrackBar
        '
        Me.SpeedTrackBar.Location = New System.Drawing.Point(10, 442)
        Me.SpeedTrackBar.Maximum = 200
        Me.SpeedTrackBar.Minimum = 1
        Me.SpeedTrackBar.Name = "SpeedTrackBar"
        Me.SpeedTrackBar.Size = New System.Drawing.Size(234, 45)
        Me.SpeedTrackBar.TabIndex = 32
        Me.SpeedTrackBar.Value = 100
        '
        'spTipLb
        '
        Me.spTipLb.AutoSize = True
        Me.spTipLb.Font = New System.Drawing.Font("NanumBarunGothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.spTipLb.Location = New System.Drawing.Point(250, 437)
        Me.spTipLb.Name = "spTipLb"
        Me.spTipLb.Size = New System.Drawing.Size(49, 15)
        Me.spTipLb.TabIndex = 33
        Me.spTipLb.Text = "Speed:"
        '
        'spLb
        '
        Me.spLb.AutoSize = True
        Me.spLb.Font = New System.Drawing.Font("NanumBarunGothic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.spLb.Location = New System.Drawing.Point(250, 460)
        Me.spLb.Name = "spLb"
        Me.spLb.Size = New System.Drawing.Size(60, 22)
        Me.spLb.TabIndex = 34
        Me.spLb.Text = "100%"
        '
        'TimeLabel
        '
        Me.TimeLabel.AutoSize = True
        Me.TimeLabel.Font = New System.Drawing.Font("NanumBarunGothicOTF", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TimeLabel.Location = New System.Drawing.Point(376, 522)
        Me.TimeLabel.Name = "TimeLabel"
        Me.TimeLabel.Size = New System.Drawing.Size(115, 14)
        Me.TimeLabel.TabIndex = 35
        Me.TimeLabel.Text = "LED Running Time: "
        '
        'LEDExButton
        '
        Me.LEDExButton.Font = New System.Drawing.Font("NanumBarunGothicOTF", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.LEDExButton.Location = New System.Drawing.Point(360, 474)
        Me.LEDExButton.Name = "LEDExButton"
        Me.LEDExButton.Size = New System.Drawing.Size(205, 45)
        Me.LEDExButton.TabIndex = 36
        Me.LEDExButton.Text = "LED Extensions"
        Me.LEDExButton.UseVisualStyleBackColor = True
        '
        'keyLED_Edit
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(579, 543)
        Me.Controls.Add(Me.LEDExButton)
        Me.Controls.Add(Me.TimeLabel)
        Me.Controls.Add(Me.spLb)
        Me.Controls.Add(Me.spTipLb)
        Me.Controls.Add(Me.SpeedTrackBar)
        Me.Controls.Add(Me.TestButton)
        Me.Controls.Add(Me.UniLED1)
        Me.Controls.Add(Me.ALGModeBox)
        Me.Controls.Add(Me.GazuaButton)
        Me.Controls.Add(Me.LED_ListView)
        Me.Controls.Add(Me.UniLED)
        Me.Controls.Add(Me.AblLED)
        Me.Controls.Add(Me.CopyButton)
        Me.Controls.Add(Me.UniLED_Edit)
        Me.MaximizeBox = False
        Me.Name = "keyLED_Edit"
        Me.Text = "keyLED Edit (MIDI Extension, Beta)"
        CType(Me.UniLED_Edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpeedTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UniLED_Edit As FastColoredTextBoxNS.FastColoredTextBox
    Friend WithEvents CopyButton As Button
    Friend WithEvents AblLED As Label
    Friend WithEvents UniLED As Label
    Friend WithEvents LED_ListView As ListView
    Friend WithEvents FileName As ColumnHeader
    Friend WithEvents GazuaButton As Button
    Friend WithEvents ALGModeBox As ComboBox
    Friend WithEvents UniLED1 As Label
    Friend WithEvents GAZUA_ As System.ComponentModel.BackgroundWorker
    Friend WithEvents TestButton As Button
    Friend WithEvents SpeedTrackBar As TrackBar
    Friend WithEvents spTipLb As Label
    Friend WithEvents spLb As Label
    Friend WithEvents TimeLabel As Label
    Friend WithEvents LEDExButton As Button
End Class

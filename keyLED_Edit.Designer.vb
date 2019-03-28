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
        Me.OpenButton1 = New System.Windows.Forms.Button()
        Me.SelCon1 = New System.Windows.Forms.ComboBox()
        Me.UniLED1 = New System.Windows.Forms.Label()
        Me.Tip1 = New System.Windows.Forms.Label()
        Me.AdvChk = New System.Windows.Forms.CheckBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        CType(Me.UniLED_Edit, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.UniLED_Edit.Location = New System.Drawing.Point(362, 43)
        Me.UniLED_Edit.Name = "UniLED_Edit"
        Me.UniLED_Edit.Paddings = New System.Windows.Forms.Padding(0)
        Me.UniLED_Edit.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.UniLED_Edit.Size = New System.Drawing.Size(205, 355)
        Me.UniLED_Edit.TabIndex = 17
        Me.UniLED_Edit.Zoom = 100
        '
        'CopyButton
        '
        Me.CopyButton.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CopyButton.Location = New System.Drawing.Point(436, 404)
        Me.CopyButton.Name = "CopyButton"
        Me.CopyButton.Size = New System.Drawing.Size(75, 23)
        Me.CopyButton.TabIndex = 19
        Me.CopyButton.Text = "Copy"
        Me.CopyButton.UseVisualStyleBackColor = True
        '
        'AblLED
        '
        Me.AblLED.AutoSize = True
        Me.AblLED.Font = New System.Drawing.Font("맑은 고딕 Semilight", 10.0!)
        Me.AblLED.Location = New System.Drawing.Point(71, 21)
        Me.AblLED.Name = "AblLED"
        Me.AblLED.Size = New System.Drawing.Size(108, 19)
        Me.AblLED.TabIndex = 21
        Me.AblLED.Text = "Ableton LED File"
        '
        'UniLED
        '
        Me.UniLED.AutoSize = True
        Me.UniLED.Font = New System.Drawing.Font("맑은 고딕 Semilight", 10.0!)
        Me.UniLED.Location = New System.Drawing.Point(405, 21)
        Me.UniLED.Name = "UniLED"
        Me.UniLED.Size = New System.Drawing.Size(114, 19)
        Me.UniLED.TabIndex = 22
        Me.UniLED.Text = "Unipack LED Text"
        '
        'LED_ListView
        '
        Me.LED_ListView.AllowDrop = True
        Me.LED_ListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FileName})
        Me.LED_ListView.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.LED_ListView.FullRowSelect = True
        Me.LED_ListView.Location = New System.Drawing.Point(12, 43)
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
        Me.GazuaButton.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GazuaButton.Location = New System.Drawing.Point(265, 178)
        Me.GazuaButton.Name = "GazuaButton"
        Me.GazuaButton.Size = New System.Drawing.Size(75, 66)
        Me.GazuaButton.TabIndex = 24
        Me.GazuaButton.Text = "-->"
        Me.GazuaButton.UseVisualStyleBackColor = True
        '
        'OpenButton1
        '
        Me.OpenButton1.Cursor = System.Windows.Forms.Cursors.Default
        Me.OpenButton1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OpenButton1.Location = New System.Drawing.Point(12, 411)
        Me.OpenButton1.Name = "OpenButton1"
        Me.OpenButton1.Size = New System.Drawing.Size(75, 23)
        Me.OpenButton1.TabIndex = 25
        Me.OpenButton1.Text = "Open"
        Me.OpenButton1.UseVisualStyleBackColor = True
        '
        'SelCon1
        '
        Me.SelCon1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SelCon1.FormattingEnabled = True
        Me.SelCon1.Items.AddRange(New Object() {"Ableton 9 ALG1", "Non-Convert (Developer Mode)"})
        Me.SelCon1.Location = New System.Drawing.Point(93, 413)
        Me.SelCon1.Name = "SelCon1"
        Me.SelCon1.Size = New System.Drawing.Size(144, 22)
        Me.SelCon1.TabIndex = 26
        Me.SelCon1.Text = "Ableton 9 ALG1"
        '
        'UniLED1
        '
        Me.UniLED1.AutoSize = True
        Me.UniLED1.Font = New System.Drawing.Font("맑은 고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.UniLED1.Location = New System.Drawing.Point(293, 9)
        Me.UniLED1.Name = "UniLED1"
        Me.UniLED1.Size = New System.Drawing.Size(108, 17)
        Me.UniLED1.TabIndex = 27
        Me.UniLED1.Text = "File Name: None"
        '
        'Tip1
        '
        Me.Tip1.AutoSize = True
        Me.Tip1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tip1.ForeColor = System.Drawing.Color.LightCoral
        Me.Tip1.Location = New System.Drawing.Point(262, 441)
        Me.Tip1.Name = "Tip1"
        Me.Tip1.Size = New System.Drawing.Size(317, 28)
        Me.Tip1.TabIndex = 28
        Me.Tip1.Text = "Tip: Launchpad MK2 Project File can only convert." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Otherwise, UniConverter will s" &
    "how you Error."
        '
        'AdvChk
        '
        Me.AdvChk.AutoSize = True
        Me.AdvChk.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdvChk.Location = New System.Drawing.Point(13, 451)
        Me.AdvChk.Name = "AdvChk"
        Me.AdvChk.Size = New System.Drawing.Size(125, 18)
        Me.AdvChk.TabIndex = 29
        Me.AdvChk.Text = "Advanced Mode"
        Me.AdvChk.UseVisualStyleBackColor = True
        '
        'keyLED_Edit
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(579, 473)
        Me.Controls.Add(Me.AdvChk)
        Me.Controls.Add(Me.Tip1)
        Me.Controls.Add(Me.UniLED1)
        Me.Controls.Add(Me.SelCon1)
        Me.Controls.Add(Me.OpenButton1)
        Me.Controls.Add(Me.GazuaButton)
        Me.Controls.Add(Me.LED_ListView)
        Me.Controls.Add(Me.UniLED)
        Me.Controls.Add(Me.AblLED)
        Me.Controls.Add(Me.CopyButton)
        Me.Controls.Add(Me.UniLED_Edit)
        Me.Name = "keyLED_Edit"
        Me.Text = "keyLED Edit (Beta)"
        CType(Me.UniLED_Edit, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents OpenButton1 As Button
    Friend WithEvents SelCon1 As ComboBox
    Friend WithEvents UniLED1 As Label
    Friend WithEvents Tip1 As Label
    Friend WithEvents AdvChk As CheckBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
End Class

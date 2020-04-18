<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class keyLED_AutoPlay
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LEDText = New FastColoredTextBoxNS.FastColoredTextBox()
        Me.TipLabel1 = New System.Windows.Forms.Label()
        Me.GazuaButton = New System.Windows.Forms.Button()
        Me.TipLabel2 = New System.Windows.Forms.Label()
        Me.AutoPlayText = New FastColoredTextBoxNS.FastColoredTextBox()
        Me.CopyButton = New System.Windows.Forms.Button()
        CType(Me.LEDText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AutoPlayText, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LEDText
        '
        Me.LEDText.AutoCompleteBracketsList = New Char() {Global.Microsoft.VisualBasic.ChrW(40), Global.Microsoft.VisualBasic.ChrW(41), Global.Microsoft.VisualBasic.ChrW(123), Global.Microsoft.VisualBasic.ChrW(125), Global.Microsoft.VisualBasic.ChrW(91), Global.Microsoft.VisualBasic.ChrW(93), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(39), Global.Microsoft.VisualBasic.ChrW(39)}
        Me.LEDText.AutoScrollMinSize = New System.Drawing.Size(27, 14)
        Me.LEDText.BackBrush = Nothing
        Me.LEDText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LEDText.CharHeight = 14
        Me.LEDText.CharWidth = 8
        Me.LEDText.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.LEDText.DisabledColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.LEDText.Font = New System.Drawing.Font("Courier New", 9.75!)
        Me.LEDText.IsReplaceMode = False
        Me.LEDText.Location = New System.Drawing.Point(12, 45)
        Me.LEDText.Name = "LEDText"
        Me.LEDText.Paddings = New System.Windows.Forms.Padding(0)
        Me.LEDText.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LEDText.Size = New System.Drawing.Size(205, 355)
        Me.LEDText.TabIndex = 18
        Me.LEDText.Zoom = 100
        '
        'TipLabel1
        '
        Me.TipLabel1.AutoSize = True
        Me.TipLabel1.Font = New System.Drawing.Font("NanumBarunGothicOTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TipLabel1.Location = New System.Drawing.Point(52, 20)
        Me.TipLabel1.Name = "TipLabel1"
        Me.TipLabel1.Size = New System.Drawing.Size(118, 22)
        Me.TipLabel1.TabIndex = 19
        Me.TipLabel1.Text = "keyLED Text"
        '
        'GazuaButton
        '
        Me.GazuaButton.Font = New System.Drawing.Font("Ubuntu", 14.25!)
        Me.GazuaButton.Location = New System.Drawing.Point(233, 180)
        Me.GazuaButton.Name = "GazuaButton"
        Me.GazuaButton.Size = New System.Drawing.Size(78, 58)
        Me.GazuaButton.TabIndex = 20
        Me.GazuaButton.Text = "-->"
        Me.GazuaButton.UseVisualStyleBackColor = True
        '
        'TipLabel2
        '
        Me.TipLabel2.AutoSize = True
        Me.TipLabel2.Font = New System.Drawing.Font("NanumBarunGothicOTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TipLabel2.Location = New System.Drawing.Point(373, 20)
        Me.TipLabel2.Name = "TipLabel2"
        Me.TipLabel2.Size = New System.Drawing.Size(130, 22)
        Me.TipLabel2.TabIndex = 22
        Me.TipLabel2.Text = "AutoPlay Text"
        '
        'AutoPlayText
        '
        Me.AutoPlayText.AutoCompleteBracketsList = New Char() {Global.Microsoft.VisualBasic.ChrW(40), Global.Microsoft.VisualBasic.ChrW(41), Global.Microsoft.VisualBasic.ChrW(123), Global.Microsoft.VisualBasic.ChrW(125), Global.Microsoft.VisualBasic.ChrW(91), Global.Microsoft.VisualBasic.ChrW(93), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(39), Global.Microsoft.VisualBasic.ChrW(39)}
        Me.AutoPlayText.AutoScrollMinSize = New System.Drawing.Size(27, 14)
        Me.AutoPlayText.BackBrush = Nothing
        Me.AutoPlayText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AutoPlayText.CharHeight = 14
        Me.AutoPlayText.CharWidth = 8
        Me.AutoPlayText.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.AutoPlayText.DisabledColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.AutoPlayText.Enabled = False
        Me.AutoPlayText.Font = New System.Drawing.Font("Courier New", 9.75!)
        Me.AutoPlayText.IsReplaceMode = False
        Me.AutoPlayText.Location = New System.Drawing.Point(333, 45)
        Me.AutoPlayText.Name = "AutoPlayText"
        Me.AutoPlayText.Paddings = New System.Windows.Forms.Padding(0)
        Me.AutoPlayText.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AutoPlayText.Size = New System.Drawing.Size(205, 282)
        Me.AutoPlayText.TabIndex = 21
        Me.AutoPlayText.Zoom = 100
        '
        'CopyButton
        '
        Me.CopyButton.Font = New System.Drawing.Font("Ubuntu", 14.25!)
        Me.CopyButton.Location = New System.Drawing.Point(333, 342)
        Me.CopyButton.Name = "CopyButton"
        Me.CopyButton.Size = New System.Drawing.Size(205, 58)
        Me.CopyButton.TabIndex = 23
        Me.CopyButton.Text = "Copy to Clipboard"
        Me.CopyButton.UseVisualStyleBackColor = True
        '
        'keyLED_AutoPlay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(558, 429)
        Me.Controls.Add(Me.CopyButton)
        Me.Controls.Add(Me.TipLabel2)
        Me.Controls.Add(Me.AutoPlayText)
        Me.Controls.Add(Me.GazuaButton)
        Me.Controls.Add(Me.TipLabel1)
        Me.Controls.Add(Me.LEDText)
        Me.Name = "keyLED_AutoPlay"
        Me.Text = "keyLED to AutoPlay Converter"
        CType(Me.LEDText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AutoPlayText, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LEDText As FastColoredTextBoxNS.FastColoredTextBox
    Friend WithEvents TipLabel1 As Label
    Friend WithEvents GazuaButton As Button
    Friend WithEvents TipLabel2 As Label
    Friend WithEvents AutoPlayText As FastColoredTextBoxNS.FastColoredTextBox
    Friend WithEvents CopyButton As Button
End Class

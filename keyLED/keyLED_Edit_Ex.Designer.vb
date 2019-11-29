<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class keyLED_Edit_Ex
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.DefEx = New System.Windows.Forms.TabPage()
        Me.OpenAblPrjBtn = New System.Windows.Forms.Button()
        Me.Flip = New System.Windows.Forms.TabPage()
        Me.DuplicateCheckBox = New System.Windows.Forms.CheckBox()
        Me.FLIPGroupBox = New System.Windows.Forms.GroupBox()
        Me.MirrorCheckBox = New System.Windows.Forms.CheckBox()
        Me.RotateComboBox = New System.Windows.Forms.ComboBox()
        Me.MirrorComboBox = New System.Windows.Forms.ComboBox()
        Me.RotateCheckBox = New System.Windows.Forms.CheckBox()
        Me.FLIPPictureBox = New System.Windows.Forms.PictureBox()
        Me.ColorEx = New System.Windows.Forms.TabPage()
        Me.TabControl.SuspendLayout()
        Me.DefEx.SuspendLayout()
        Me.Flip.SuspendLayout()
        Me.FLIPGroupBox.SuspendLayout()
        CType(Me.FLIPPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.DefEx)
        Me.TabControl.Controls.Add(Me.Flip)
        Me.TabControl.Controls.Add(Me.ColorEx)
        Me.TabControl.Location = New System.Drawing.Point(12, 12)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(776, 426)
        Me.TabControl.TabIndex = 0
        '
        'DefEx
        '
        Me.DefEx.Controls.Add(Me.OpenAblPrjBtn)
        Me.DefEx.Location = New System.Drawing.Point(4, 22)
        Me.DefEx.Name = "DefEx"
        Me.DefEx.Padding = New System.Windows.Forms.Padding(3)
        Me.DefEx.Size = New System.Drawing.Size(768, 400)
        Me.DefEx.TabIndex = 0
        Me.DefEx.Text = "[Default Extension]"
        Me.DefEx.UseVisualStyleBackColor = True
        '
        'OpenAblPrjBtn
        '
        Me.OpenAblPrjBtn.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.OpenAblPrjBtn.Location = New System.Drawing.Point(23, 20)
        Me.OpenAblPrjBtn.Name = "OpenAblPrjBtn"
        Me.OpenAblPrjBtn.Size = New System.Drawing.Size(146, 62)
        Me.OpenAblPrjBtn.TabIndex = 0
        Me.OpenAblPrjBtn.Text = "Open Ableton Project"
        Me.OpenAblPrjBtn.UseVisualStyleBackColor = True
        '
        'Flip
        '
        Me.Flip.Controls.Add(Me.DuplicateCheckBox)
        Me.Flip.Controls.Add(Me.FLIPGroupBox)
        Me.Flip.Controls.Add(Me.FLIPPictureBox)
        Me.Flip.Location = New System.Drawing.Point(4, 22)
        Me.Flip.Name = "Flip"
        Me.Flip.Padding = New System.Windows.Forms.Padding(3)
        Me.Flip.Size = New System.Drawing.Size(768, 400)
        Me.Flip.TabIndex = 1
        Me.Flip.Text = "Flip"
        Me.Flip.UseVisualStyleBackColor = True
        '
        'DuplicateCheckBox
        '
        Me.DuplicateCheckBox.AutoSize = True
        Me.DuplicateCheckBox.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.DuplicateCheckBox.Location = New System.Drawing.Point(301, 348)
        Me.DuplicateCheckBox.Name = "DuplicateCheckBox"
        Me.DuplicateCheckBox.Size = New System.Drawing.Size(128, 26)
        Me.DuplicateCheckBox.TabIndex = 5
        Me.DuplicateCheckBox.Text = "DUPLICATE"
        Me.DuplicateCheckBox.UseVisualStyleBackColor = True
        '
        'FLIPGroupBox
        '
        Me.FLIPGroupBox.Controls.Add(Me.MirrorCheckBox)
        Me.FLIPGroupBox.Controls.Add(Me.RotateComboBox)
        Me.FLIPGroupBox.Controls.Add(Me.MirrorComboBox)
        Me.FLIPGroupBox.Controls.Add(Me.RotateCheckBox)
        Me.FLIPGroupBox.Font = New System.Drawing.Font("나눔바른고딕OTF", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FLIPGroupBox.Location = New System.Drawing.Point(218, 223)
        Me.FLIPGroupBox.Name = "FLIPGroupBox"
        Me.FLIPGroupBox.Size = New System.Drawing.Size(285, 109)
        Me.FLIPGroupBox.TabIndex = 5
        Me.FLIPGroupBox.TabStop = False
        Me.FLIPGroupBox.Text = "LED Extensions"
        '
        'MirrorCheckBox
        '
        Me.MirrorCheckBox.AutoSize = True
        Me.MirrorCheckBox.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.MirrorCheckBox.Location = New System.Drawing.Point(25, 21)
        Me.MirrorCheckBox.Name = "MirrorCheckBox"
        Me.MirrorCheckBox.Size = New System.Drawing.Size(101, 26)
        Me.MirrorCheckBox.TabIndex = 1
        Me.MirrorCheckBox.Text = "MIRROR"
        Me.MirrorCheckBox.UseVisualStyleBackColor = True
        '
        'RotateComboBox
        '
        Me.RotateComboBox.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.RotateComboBox.FormattingEnabled = True
        Me.RotateComboBox.Items.AddRange(New Object() {"90°", "180°", "270°"})
        Me.RotateComboBox.Location = New System.Drawing.Point(132, 66)
        Me.RotateComboBox.Name = "RotateComboBox"
        Me.RotateComboBox.Size = New System.Drawing.Size(121, 30)
        Me.RotateComboBox.TabIndex = 4
        Me.RotateComboBox.Text = "90°"
        '
        'MirrorComboBox
        '
        Me.MirrorComboBox.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.MirrorComboBox.FormattingEnabled = True
        Me.MirrorComboBox.Items.AddRange(New Object() {"Horizontal", "Vertical"})
        Me.MirrorComboBox.Location = New System.Drawing.Point(132, 19)
        Me.MirrorComboBox.Name = "MirrorComboBox"
        Me.MirrorComboBox.Size = New System.Drawing.Size(121, 30)
        Me.MirrorComboBox.TabIndex = 2
        Me.MirrorComboBox.Text = "Horizontal"
        '
        'RotateCheckBox
        '
        Me.RotateCheckBox.AutoSize = True
        Me.RotateCheckBox.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.RotateCheckBox.Location = New System.Drawing.Point(25, 68)
        Me.RotateCheckBox.Name = "RotateCheckBox"
        Me.RotateCheckBox.Size = New System.Drawing.Size(99, 26)
        Me.RotateCheckBox.TabIndex = 3
        Me.RotateCheckBox.Text = "ROTATE"
        Me.RotateCheckBox.UseVisualStyleBackColor = True
        '
        'FLIPPictureBox
        '
        Me.FLIPPictureBox.Image = Global.UniConverter_Project.My.Resources.Resources.FLIP
        Me.FLIPPictureBox.Location = New System.Drawing.Point(6, 6)
        Me.FLIPPictureBox.Name = "FLIPPictureBox"
        Me.FLIPPictureBox.Size = New System.Drawing.Size(756, 194)
        Me.FLIPPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.FLIPPictureBox.TabIndex = 0
        Me.FLIPPictureBox.TabStop = False
        '
        'ColorEx
        '
        Me.ColorEx.Location = New System.Drawing.Point(4, 22)
        Me.ColorEx.Name = "ColorEx"
        Me.ColorEx.Size = New System.Drawing.Size(768, 400)
        Me.ColorEx.TabIndex = 2
        Me.ColorEx.Text = "Color Extension"
        Me.ColorEx.UseVisualStyleBackColor = True
        '
        'keyLED_Edit_Ex
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TabControl)
        Me.Name = "keyLED_Edit_Ex"
        Me.Text = "keyLED Edit - LED Extensions"
        Me.TabControl.ResumeLayout(False)
        Me.DefEx.ResumeLayout(False)
        Me.Flip.ResumeLayout(False)
        Me.Flip.PerformLayout()
        Me.FLIPGroupBox.ResumeLayout(False)
        Me.FLIPGroupBox.PerformLayout()
        CType(Me.FLIPPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl As TabControl
    Friend WithEvents DefEx As TabPage
    Friend WithEvents Flip As TabPage
    Friend WithEvents ColorEx As TabPage
    Friend WithEvents OpenAblPrjBtn As Button
    Friend WithEvents DuplicateCheckBox As CheckBox
    Friend WithEvents FLIPGroupBox As GroupBox
    Friend WithEvents MirrorCheckBox As CheckBox
    Friend WithEvents RotateComboBox As ComboBox
    Friend WithEvents MirrorComboBox As ComboBox
    Friend WithEvents RotateCheckBox As CheckBox
    Friend WithEvents FLIPPictureBox As PictureBox
End Class

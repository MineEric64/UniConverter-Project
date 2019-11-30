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
        Me.Flip_AutoLoadButton = New System.Windows.Forms.Button()
        Me.Flip_ResetButton = New System.Windows.Forms.Button()
        Me.Flip_DuplicateCheckBox = New System.Windows.Forms.CheckBox()
        Me.FlipGroupBox = New System.Windows.Forms.GroupBox()
        Me.Flip_MirrorCheckBox = New System.Windows.Forms.CheckBox()
        Me.Flip_RotateComboBox = New System.Windows.Forms.ComboBox()
        Me.Flip_MirrorComboBox = New System.Windows.Forms.ComboBox()
        Me.Flip_RotateCheckBox = New System.Windows.Forms.CheckBox()
        Me.FlipPictureBox = New System.Windows.Forms.PictureBox()
        Me.ColorEx = New System.Windows.Forms.TabPage()
        Me.TabControl.SuspendLayout()
        Me.DefEx.SuspendLayout()
        Me.Flip.SuspendLayout()
        Me.FlipGroupBox.SuspendLayout()
        CType(Me.FlipPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Flip.Controls.Add(Me.Flip_AutoLoadButton)
        Me.Flip.Controls.Add(Me.Flip_ResetButton)
        Me.Flip.Controls.Add(Me.Flip_DuplicateCheckBox)
        Me.Flip.Controls.Add(Me.FlipGroupBox)
        Me.Flip.Controls.Add(Me.FlipPictureBox)
        Me.Flip.Location = New System.Drawing.Point(4, 22)
        Me.Flip.Name = "Flip"
        Me.Flip.Padding = New System.Windows.Forms.Padding(3)
        Me.Flip.Size = New System.Drawing.Size(768, 400)
        Me.Flip.TabIndex = 1
        Me.Flip.Text = "Flip"
        Me.Flip.UseVisualStyleBackColor = True
        '
        'Flip_AutoLoadButton
        '
        Me.Flip_AutoLoadButton.Enabled = False
        Me.Flip_AutoLoadButton.Font = New System.Drawing.Font("나눔바른고딕OTF", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Flip_AutoLoadButton.Location = New System.Drawing.Point(583, 311)
        Me.Flip_AutoLoadButton.Name = "Flip_AutoLoadButton"
        Me.Flip_AutoLoadButton.Size = New System.Drawing.Size(160, 74)
        Me.Flip_AutoLoadButton.TabIndex = 7
        Me.Flip_AutoLoadButton.Text = "Auto Load from Ableton Project (Beta)"
        Me.Flip_AutoLoadButton.UseVisualStyleBackColor = True
        '
        'Flip_ResetButton
        '
        Me.Flip_ResetButton.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Flip_ResetButton.Location = New System.Drawing.Point(583, 244)
        Me.Flip_ResetButton.Name = "Flip_ResetButton"
        Me.Flip_ResetButton.Size = New System.Drawing.Size(160, 53)
        Me.Flip_ResetButton.TabIndex = 6
        Me.Flip_ResetButton.Text = "Reset"
        Me.Flip_ResetButton.UseVisualStyleBackColor = True
        '
        'Flip_DuplicateCheckBox
        '
        Me.Flip_DuplicateCheckBox.AutoSize = True
        Me.Flip_DuplicateCheckBox.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Flip_DuplicateCheckBox.Location = New System.Drawing.Point(301, 348)
        Me.Flip_DuplicateCheckBox.Name = "Flip_DuplicateCheckBox"
        Me.Flip_DuplicateCheckBox.Size = New System.Drawing.Size(128, 26)
        Me.Flip_DuplicateCheckBox.TabIndex = 5
        Me.Flip_DuplicateCheckBox.Text = "DUPLICATE"
        Me.Flip_DuplicateCheckBox.UseVisualStyleBackColor = True
        '
        'FlipGroupBox
        '
        Me.FlipGroupBox.Controls.Add(Me.Flip_MirrorCheckBox)
        Me.FlipGroupBox.Controls.Add(Me.Flip_RotateComboBox)
        Me.FlipGroupBox.Controls.Add(Me.Flip_MirrorComboBox)
        Me.FlipGroupBox.Controls.Add(Me.Flip_RotateCheckBox)
        Me.FlipGroupBox.Font = New System.Drawing.Font("나눔바른고딕OTF", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FlipGroupBox.Location = New System.Drawing.Point(218, 223)
        Me.FlipGroupBox.Name = "FlipGroupBox"
        Me.FlipGroupBox.Size = New System.Drawing.Size(285, 109)
        Me.FlipGroupBox.TabIndex = 5
        Me.FlipGroupBox.TabStop = False
        Me.FlipGroupBox.Text = "LED Extensions"
        '
        'Flip_MirrorCheckBox
        '
        Me.Flip_MirrorCheckBox.AutoSize = True
        Me.Flip_MirrorCheckBox.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Flip_MirrorCheckBox.Location = New System.Drawing.Point(25, 21)
        Me.Flip_MirrorCheckBox.Name = "Flip_MirrorCheckBox"
        Me.Flip_MirrorCheckBox.Size = New System.Drawing.Size(101, 26)
        Me.Flip_MirrorCheckBox.TabIndex = 1
        Me.Flip_MirrorCheckBox.Text = "MIRROR"
        Me.Flip_MirrorCheckBox.UseVisualStyleBackColor = True
        '
        'Flip_RotateComboBox
        '
        Me.Flip_RotateComboBox.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Flip_RotateComboBox.FormattingEnabled = True
        Me.Flip_RotateComboBox.Items.AddRange(New Object() {"90°", "180°", "270°"})
        Me.Flip_RotateComboBox.Location = New System.Drawing.Point(132, 66)
        Me.Flip_RotateComboBox.Name = "Flip_RotateComboBox"
        Me.Flip_RotateComboBox.Size = New System.Drawing.Size(121, 30)
        Me.Flip_RotateComboBox.TabIndex = 4
        Me.Flip_RotateComboBox.Text = "90°"
        '
        'Flip_MirrorComboBox
        '
        Me.Flip_MirrorComboBox.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Flip_MirrorComboBox.FormattingEnabled = True
        Me.Flip_MirrorComboBox.Items.AddRange(New Object() {"Horizontal", "Vertical"})
        Me.Flip_MirrorComboBox.Location = New System.Drawing.Point(132, 19)
        Me.Flip_MirrorComboBox.Name = "Flip_MirrorComboBox"
        Me.Flip_MirrorComboBox.Size = New System.Drawing.Size(121, 30)
        Me.Flip_MirrorComboBox.TabIndex = 2
        Me.Flip_MirrorComboBox.Text = "Horizontal"
        '
        'Flip_RotateCheckBox
        '
        Me.Flip_RotateCheckBox.AutoSize = True
        Me.Flip_RotateCheckBox.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Flip_RotateCheckBox.Location = New System.Drawing.Point(25, 68)
        Me.Flip_RotateCheckBox.Name = "Flip_RotateCheckBox"
        Me.Flip_RotateCheckBox.Size = New System.Drawing.Size(99, 26)
        Me.Flip_RotateCheckBox.TabIndex = 3
        Me.Flip_RotateCheckBox.Text = "ROTATE"
        Me.Flip_RotateCheckBox.UseVisualStyleBackColor = True
        '
        'FlipPictureBox
        '
        Me.FlipPictureBox.Image = Global.UniConverter_Project.My.Resources.Resources.FLIP
        Me.FlipPictureBox.Location = New System.Drawing.Point(6, 6)
        Me.FlipPictureBox.Name = "FlipPictureBox"
        Me.FlipPictureBox.Size = New System.Drawing.Size(756, 194)
        Me.FlipPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.FlipPictureBox.TabIndex = 0
        Me.FlipPictureBox.TabStop = False
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
        Me.FlipGroupBox.ResumeLayout(False)
        Me.FlipGroupBox.PerformLayout()
        CType(Me.FlipPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl As TabControl
    Friend WithEvents DefEx As TabPage
    Friend WithEvents Flip As TabPage
    Friend WithEvents ColorEx As TabPage
    Friend WithEvents OpenAblPrjBtn As Button
    Friend WithEvents Flip_DuplicateCheckBox As CheckBox
    Friend WithEvents FlipGroupBox As GroupBox
    Friend WithEvents Flip_MirrorCheckBox As CheckBox
    Friend WithEvents Flip_RotateComboBox As ComboBox
    Friend WithEvents Flip_MirrorComboBox As ComboBox
    Friend WithEvents Flip_RotateCheckBox As CheckBox
    Friend WithEvents FlipPictureBox As PictureBox
    Friend WithEvents Flip_ResetButton As Button
    Friend WithEvents Flip_AutoLoadButton As Button
End Class

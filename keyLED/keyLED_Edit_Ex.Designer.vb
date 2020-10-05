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
        Me.tabPageDefaultEx = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tabPageFlip = New System.Windows.Forms.TabPage()
        Me.Flip_ResetButton = New System.Windows.Forms.Button()
        Me.Flip_DuplicateCheckBox = New System.Windows.Forms.CheckBox()
        Me.FlipGroupBox = New System.Windows.Forms.GroupBox()
        Me.Flip_MirrorCheckBox = New System.Windows.Forms.CheckBox()
        Me.Flip_RotateComboBox = New System.Windows.Forms.ComboBox()
        Me.Flip_MirrorComboBox = New System.Windows.Forms.ComboBox()
        Me.Flip_RotateCheckBox = New System.Windows.Forms.CheckBox()
        Me.FlipPictureBox = New System.Windows.Forms.PictureBox()
        Me.tabPageColorEx = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabControl.SuspendLayout
        Me.tabPageDefaultEx.SuspendLayout
        Me.tabPageFlip.SuspendLayout
        Me.FlipGroupBox.SuspendLayout
        CType(Me.FlipPictureBox,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tabPageColorEx.SuspendLayout
        Me.SuspendLayout
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.tabPageDefaultEx)
        Me.TabControl.Controls.Add(Me.tabPageFlip)
        Me.TabControl.Controls.Add(Me.tabPageColorEx)
        Me.TabControl.Font = New System.Drawing.Font("NanumBarunGothic", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.TabControl.Location = New System.Drawing.Point(12, 12)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(776, 426)
        Me.TabControl.TabIndex = 0
        '
        'tabPageDefaultEx
        '
        Me.tabPageDefaultEx.Controls.Add(Me.Label1)
        Me.tabPageDefaultEx.Font = New System.Drawing.Font("NanumBarunGothic", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.tabPageDefaultEx.Location = New System.Drawing.Point(4, 23)
        Me.tabPageDefaultEx.Name = "tabPageDefaultEx"
        Me.tabPageDefaultEx.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageDefaultEx.Size = New System.Drawing.Size(768, 399)
        Me.tabPageDefaultEx.TabIndex = 0
        Me.tabPageDefaultEx.Text = "[Default Extension]"
        Me.tabPageDefaultEx.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("NanumBarunGothicOTF", 36!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.Label1.Location = New System.Drawing.Point(66, 133)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(658, 110)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Not yet :("&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"They will add in next version."
        '
        'tabPageFlip
        '
        Me.tabPageFlip.Controls.Add(Me.Flip_ResetButton)
        Me.tabPageFlip.Controls.Add(Me.Flip_DuplicateCheckBox)
        Me.tabPageFlip.Controls.Add(Me.FlipGroupBox)
        Me.tabPageFlip.Controls.Add(Me.FlipPictureBox)
        Me.tabPageFlip.Font = New System.Drawing.Font("NanumBarunGothic", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.tabPageFlip.Location = New System.Drawing.Point(4, 23)
        Me.tabPageFlip.Name = "tabPageFlip"
        Me.tabPageFlip.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageFlip.Size = New System.Drawing.Size(768, 399)
        Me.tabPageFlip.TabIndex = 1
        Me.tabPageFlip.Text = "Flip"
        Me.tabPageFlip.UseVisualStyleBackColor = true
        '
        'Flip_ResetButton
        '
        Me.Flip_ResetButton.Font = New System.Drawing.Font("NanumBarunGothicOTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.Flip_ResetButton.Location = New System.Drawing.Point(590, 334)
        Me.Flip_ResetButton.Name = "Flip_ResetButton"
        Me.Flip_ResetButton.Size = New System.Drawing.Size(160, 53)
        Me.Flip_ResetButton.TabIndex = 6
        Me.Flip_ResetButton.Text = "Reset"
        Me.Flip_ResetButton.UseVisualStyleBackColor = true
        '
        'Flip_DuplicateCheckBox
        '
        Me.Flip_DuplicateCheckBox.AutoSize = true
        Me.Flip_DuplicateCheckBox.Font = New System.Drawing.Font("NanumBarunGothicOTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.Flip_DuplicateCheckBox.Location = New System.Drawing.Point(301, 348)
        Me.Flip_DuplicateCheckBox.Name = "Flip_DuplicateCheckBox"
        Me.Flip_DuplicateCheckBox.Size = New System.Drawing.Size(128, 26)
        Me.Flip_DuplicateCheckBox.TabIndex = 5
        Me.Flip_DuplicateCheckBox.Text = "DUPLICATE"
        Me.Flip_DuplicateCheckBox.UseVisualStyleBackColor = true
        '
        'FlipGroupBox
        '
        Me.FlipGroupBox.Controls.Add(Me.Flip_MirrorCheckBox)
        Me.FlipGroupBox.Controls.Add(Me.Flip_RotateComboBox)
        Me.FlipGroupBox.Controls.Add(Me.Flip_MirrorComboBox)
        Me.FlipGroupBox.Controls.Add(Me.Flip_RotateCheckBox)
        Me.FlipGroupBox.Font = New System.Drawing.Font("NanumBarunGothicOTF", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.FlipGroupBox.Location = New System.Drawing.Point(218, 223)
        Me.FlipGroupBox.Name = "FlipGroupBox"
        Me.FlipGroupBox.Size = New System.Drawing.Size(285, 109)
        Me.FlipGroupBox.TabIndex = 5
        Me.FlipGroupBox.TabStop = false
        Me.FlipGroupBox.Text = "LED Extensions"
        '
        'Flip_MirrorCheckBox
        '
        Me.Flip_MirrorCheckBox.AutoSize = true
        Me.Flip_MirrorCheckBox.Font = New System.Drawing.Font("NanumBarunGothicOTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.Flip_MirrorCheckBox.Location = New System.Drawing.Point(25, 21)
        Me.Flip_MirrorCheckBox.Name = "Flip_MirrorCheckBox"
        Me.Flip_MirrorCheckBox.Size = New System.Drawing.Size(101, 26)
        Me.Flip_MirrorCheckBox.TabIndex = 1
        Me.Flip_MirrorCheckBox.Text = "MIRROR"
        Me.Flip_MirrorCheckBox.UseVisualStyleBackColor = true
        '
        'Flip_RotateComboBox
        '
        Me.Flip_RotateComboBox.Font = New System.Drawing.Font("NanumBarunGothicOTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.Flip_RotateComboBox.FormattingEnabled = true
        Me.Flip_RotateComboBox.Items.AddRange(New Object() {"90°", "180°", "270°"})
        Me.Flip_RotateComboBox.Location = New System.Drawing.Point(132, 66)
        Me.Flip_RotateComboBox.Name = "Flip_RotateComboBox"
        Me.Flip_RotateComboBox.Size = New System.Drawing.Size(121, 30)
        Me.Flip_RotateComboBox.TabIndex = 4
        Me.Flip_RotateComboBox.Text = "90°"
        '
        'Flip_MirrorComboBox
        '
        Me.Flip_MirrorComboBox.Font = New System.Drawing.Font("NanumBarunGothicOTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.Flip_MirrorComboBox.FormattingEnabled = true
        Me.Flip_MirrorComboBox.Items.AddRange(New Object() {"Horizontal", "Vertical"})
        Me.Flip_MirrorComboBox.Location = New System.Drawing.Point(132, 19)
        Me.Flip_MirrorComboBox.Name = "Flip_MirrorComboBox"
        Me.Flip_MirrorComboBox.Size = New System.Drawing.Size(121, 30)
        Me.Flip_MirrorComboBox.TabIndex = 2
        Me.Flip_MirrorComboBox.Text = "Horizontal"
        '
        'Flip_RotateCheckBox
        '
        Me.Flip_RotateCheckBox.AutoSize = true
        Me.Flip_RotateCheckBox.Font = New System.Drawing.Font("NanumBarunGothicOTF", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.Flip_RotateCheckBox.Location = New System.Drawing.Point(25, 68)
        Me.Flip_RotateCheckBox.Name = "Flip_RotateCheckBox"
        Me.Flip_RotateCheckBox.Size = New System.Drawing.Size(99, 26)
        Me.Flip_RotateCheckBox.TabIndex = 3
        Me.Flip_RotateCheckBox.Text = "ROTATE"
        Me.Flip_RotateCheckBox.UseVisualStyleBackColor = true
        '
        'FlipPictureBox
        '
        Me.FlipPictureBox.Image = Global.UniConverter.My.Resources.Resources.FLIP
        Me.FlipPictureBox.Location = New System.Drawing.Point(6, 6)
        Me.FlipPictureBox.Name = "FlipPictureBox"
        Me.FlipPictureBox.Size = New System.Drawing.Size(756, 194)
        Me.FlipPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.FlipPictureBox.TabIndex = 0
        Me.FlipPictureBox.TabStop = false
        '
        'tabPageColorEx
        '
        Me.tabPageColorEx.Controls.Add(Me.Label2)
        Me.tabPageColorEx.Font = New System.Drawing.Font("NanumBarunGothic", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.tabPageColorEx.Location = New System.Drawing.Point(4, 23)
        Me.tabPageColorEx.Name = "tabPageColorEx"
        Me.tabPageColorEx.Size = New System.Drawing.Size(768, 399)
        Me.tabPageColorEx.TabIndex = 2
        Me.tabPageColorEx.Text = "Color Extension"
        Me.tabPageColorEx.UseVisualStyleBackColor = true
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Font = New System.Drawing.Font("NanumBarunGothicOTF", 36!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.Label2.Location = New System.Drawing.Point(55, 144)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(658, 110)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Not yet :("&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"They will add in next version."
        '
        'keyLED_Edit_Ex
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 12!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TabControl)
        Me.MaximizeBox = false
        Me.Name = "keyLED_Edit_Ex"
        Me.Text = "keyLED Edit - LED Extensions"
        Me.TabControl.ResumeLayout(false)
        Me.tabPageDefaultEx.ResumeLayout(false)
        Me.tabPageDefaultEx.PerformLayout
        Me.tabPageFlip.ResumeLayout(false)
        Me.tabPageFlip.PerformLayout
        Me.FlipGroupBox.ResumeLayout(false)
        Me.FlipGroupBox.PerformLayout
        CType(Me.FlipPictureBox,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabPageColorEx.ResumeLayout(false)
        Me.tabPageColorEx.PerformLayout
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents TabControl As TabControl
    Friend WithEvents tabPageDefaultEx As TabPage
    Friend WithEvents tabPageFlip As TabPage
    Friend WithEvents tabPageColorEx As TabPage
    Friend WithEvents Flip_DuplicateCheckBox As CheckBox
    Friend WithEvents FlipGroupBox As GroupBox
    Friend WithEvents Flip_MirrorCheckBox As CheckBox
    Friend WithEvents Flip_RotateComboBox As ComboBox
    Friend WithEvents Flip_MirrorComboBox As ComboBox
    Friend WithEvents Flip_RotateCheckBox As CheckBox
    Friend WithEvents FlipPictureBox As PictureBox
    Friend WithEvents Flip_ResetButton As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class

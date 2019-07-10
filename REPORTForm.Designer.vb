<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class REPORTForm
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
        Me.UplButton = New System.Windows.Forms.Button()
        Me.attLab = New System.Windows.Forms.Label()
        Me.MAIL_ATTACHMENT = New System.Windows.Forms.TextBox()
        Me.DelButton = New System.Windows.Forms.Button()
        Me.SenButton = New System.Windows.Forms.Button()
        Me.DescBox = New System.Windows.Forms.TextBox()
        Me.FileDlg = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'UplButton
        '
        Me.UplButton.Font = New System.Drawing.Font("Ubuntu", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UplButton.Location = New System.Drawing.Point(383, 222)
        Me.UplButton.Name = "UplButton"
        Me.UplButton.Size = New System.Drawing.Size(75, 25)
        Me.UplButton.TabIndex = 15
        Me.UplButton.Text = "Upload"
        Me.UplButton.UseVisualStyleBackColor = True
        '
        'attLab
        '
        Me.attLab.AutoSize = True
        Me.attLab.Font = New System.Drawing.Font("Ubuntu", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.attLab.Location = New System.Drawing.Point(37, 224)
        Me.attLab.Name = "attLab"
        Me.attLab.Size = New System.Drawing.Size(89, 17)
        Me.attLab.TabIndex = 14
        Me.attLab.Text = "Attachment: "
        '
        'MAIL_ATTACHMENT
        '
        Me.MAIL_ATTACHMENT.Font = New System.Drawing.Font("나눔고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MAIL_ATTACHMENT.Location = New System.Drawing.Point(126, 223)
        Me.MAIL_ATTACHMENT.Name = "MAIL_ATTACHMENT"
        Me.MAIL_ATTACHMENT.ReadOnly = True
        Me.MAIL_ATTACHMENT.Size = New System.Drawing.Size(251, 22)
        Me.MAIL_ATTACHMENT.TabIndex = 13
        '
        'DelButton
        '
        Me.DelButton.Font = New System.Drawing.Font("Ubuntu", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DelButton.Location = New System.Drawing.Point(303, 258)
        Me.DelButton.Name = "DelButton"
        Me.DelButton.Size = New System.Drawing.Size(74, 38)
        Me.DelButton.TabIndex = 12
        Me.DelButton.Text = "Delete"
        Me.DelButton.UseVisualStyleBackColor = True
        '
        'SenButton
        '
        Me.SenButton.Font = New System.Drawing.Font("Ubuntu", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SenButton.Location = New System.Drawing.Point(394, 258)
        Me.SenButton.Name = "SenButton"
        Me.SenButton.Size = New System.Drawing.Size(75, 38)
        Me.SenButton.TabIndex = 11
        Me.SenButton.Text = "Send!"
        Me.SenButton.UseVisualStyleBackColor = True
        '
        'DescBox
        '
        Me.DescBox.Font = New System.Drawing.Font("나눔고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.DescBox.Location = New System.Drawing.Point(23, 21)
        Me.DescBox.Multiline = True
        Me.DescBox.Name = "DescBox"
        Me.DescBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.DescBox.Size = New System.Drawing.Size(446, 190)
        Me.DescBox.TabIndex = 10
        Me.DescBox.Text = "Please write your amazing opinion! :D"
        '
        'REPORTForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(489, 322)
        Me.Controls.Add(Me.UplButton)
        Me.Controls.Add(Me.attLab)
        Me.Controls.Add(Me.MAIL_ATTACHMENT)
        Me.Controls.Add(Me.DelButton)
        Me.Controls.Add(Me.SenButton)
        Me.Controls.Add(Me.DescBox)
        Me.MaximizeBox = False
        Me.Name = "REPORTForm"
        Me.Text = "UniConverter: Report a bug"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UplButton As Button
    Friend WithEvents attLab As Label
    Friend WithEvents MAIL_ATTACHMENT As TextBox
    Friend WithEvents DelButton As Button
    Friend WithEvents SenButton As Button
    Friend WithEvents DescBox As TextBox
    Friend WithEvents FileDlg As OpenFileDialog
End Class

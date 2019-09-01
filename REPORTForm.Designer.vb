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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(REPORTForm))
        Me.p = New System.Windows.Forms.Button()
        Me.attLab = New System.Windows.Forms.Label()
        Me.o = New System.Windows.Forms.TextBox()
        Me.s = New System.Windows.Forms.Button()
        Me.r = New System.Windows.Forms.Button()
        Me.q = New System.Windows.Forms.TextBox()
        Me.v = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'p
        '
        Me.p.Font = New System.Drawing.Font("Ubuntu", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.p.Location = New System.Drawing.Point(383, 222)
        Me.p.Name = "p"
        Me.p.Size = New System.Drawing.Size(75, 25)
        Me.p.TabIndex = 15
        Me.p.Text = "Upload"
        Me.p.UseVisualStyleBackColor = True
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
        'o
        '
        Me.o.Font = New System.Drawing.Font("나눔고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.o.Location = New System.Drawing.Point(126, 223)
        Me.o.Name = "o"
        Me.o.ReadOnly = True
        Me.o.Size = New System.Drawing.Size(251, 22)
        Me.o.TabIndex = 13
        '
        's
        '
        Me.s.Font = New System.Drawing.Font("Ubuntu", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.s.Location = New System.Drawing.Point(303, 258)
        Me.s.Name = "s"
        Me.s.Size = New System.Drawing.Size(74, 38)
        Me.s.TabIndex = 12
        Me.s.Text = "Delete"
        Me.s.UseVisualStyleBackColor = True
        '
        'r
        '
        Me.r.Font = New System.Drawing.Font("Ubuntu", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.r.Location = New System.Drawing.Point(394, 258)
        Me.r.Name = "r"
        Me.r.Size = New System.Drawing.Size(75, 38)
        Me.r.TabIndex = 11
        Me.r.Text = "Send!"
        Me.r.UseVisualStyleBackColor = True
        '
        'q
        '
        Me.q.Font = New System.Drawing.Font("나눔고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.q.Location = New System.Drawing.Point(23, 21)
        Me.q.Multiline = True
        Me.q.Name = "q"
        Me.q.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.q.Size = New System.Drawing.Size(446, 190)
        Me.q.TabIndex = 10
        Me.q.Text = "Please write your amazing opinion! :D"
        '
        'REPORTForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(489, 322)
        Me.Controls.Add(Me.p)
        Me.Controls.Add(Me.attLab)
        Me.Controls.Add(Me.o)
        Me.Controls.Add(Me.s)
        Me.Controls.Add(Me.r)
        Me.Controls.Add(Me.q)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "REPORTForm"
        Me.Text = "UniConverter: Report a bug"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents p As Button
    Friend WithEvents attLab As Label
    Friend WithEvents o As TextBox
    Friend WithEvents s As Button
    Friend WithEvents r As Button
    Friend WithEvents q As TextBox
    Friend WithEvents v As OpenFileDialog
End Class

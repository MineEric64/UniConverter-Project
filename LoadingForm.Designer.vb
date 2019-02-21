<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LOADINGForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LOADINGForm))
        Me.DProgress1 = New System.Windows.Forms.ProgressBar()
        Me.DLabel1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'DProgress1
        '
        Me.DProgress1.Location = New System.Drawing.Point(34, 62)
        Me.DProgress1.MarqueeAnimationSpeed = 1000
        Me.DProgress1.Maximum = 1000
        Me.DProgress1.Name = "DProgress1"
        Me.DProgress1.Size = New System.Drawing.Size(367, 23)
        Me.DProgress1.TabIndex = 0
        '
        'DLabel1
        '
        Me.DLabel1.AutoSize = True
        Me.DLabel1.Font = New System.Drawing.Font("맑은 고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.DLabel1.Location = New System.Drawing.Point(101, 108)
        Me.DLabel1.Name = "DLabel1"
        Me.DLabel1.Size = New System.Drawing.Size(128, 17)
        Me.DLabel1.TabIndex = 1
        Me.DLabel1.Text = "Loading Contents ..."
        '
        'LOADINGForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 218)
        Me.Controls.Add(Me.DLabel1)
        Me.Controls.Add(Me.DProgress1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "LOADINGForm"
        Me.Text = "LOADING ..."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DProgress1 As ProgressBar
    Friend WithEvents DLabel1 As Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Loading
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Loading))
        Me.DPr = New System.Windows.Forms.ProgressBar()
        Me.DLb = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'DPr
        '
        Me.DPr.Location = New System.Drawing.Point(34, 28)
        Me.DPr.MarqueeAnimationSpeed = 10
        Me.DPr.Maximum = 1000
        Me.DPr.Name = "DPr"
        Me.DPr.Size = New System.Drawing.Size(367, 23)
        Me.DPr.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.DPr.TabIndex = 0
        '
        'DLb
        '
        Me.DLb.AutoSize = True
        Me.DLb.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DLb.Location = New System.Drawing.Point(121, 82)
        Me.DLb.Name = "DLb"
        Me.DLb.Size = New System.Drawing.Size(198, 23)
        Me.DLb.TabIndex = 1
        Me.DLb.Text = "Loading Contents..."
        '
        'Loading
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(440, 140)
        Me.Controls.Add(Me.DLb)
        Me.Controls.Add(Me.DPr)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "Loading"
        Me.Text = "Loading Contents..."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DPr As ProgressBar
    Friend WithEvents DLb As Label
End Class

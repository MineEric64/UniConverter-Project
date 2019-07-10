<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainScreen
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainScreen))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.VerText = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("나눔바른고딕OTF", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(268, 259)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 24)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Loading..."
        '
        'VerText
        '
        Me.VerText.AutoSize = True
        Me.VerText.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.VerText.Font = New System.Drawing.Font("나눔바른고딕OTF", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.VerText.ForeColor = System.Drawing.Color.White
        Me.VerText.Location = New System.Drawing.Point(560, 337)
        Me.VerText.Name = "VerText"
        Me.VerText.Size = New System.Drawing.Size(81, 24)
        Me.VerText.TabIndex = 6
        Me.VerText.Text = "Version"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(199, 289)
        Me.ProgressBar1.MarqueeAnimationSpeed = 500
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(230, 23)
        Me.ProgressBar1.TabIndex = 5
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("나눔바른고딕OTF", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(4, 342)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(452, 18)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "ⓒ 2018 ~ 2019 UniLab, 최에릭, Follow_JB All Rights Reserved."
        '
        'MainScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(646, 367)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.VerText)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainScreen"
        Me.Text = "UniConverter"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents VerText As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label3 As Label
End Class

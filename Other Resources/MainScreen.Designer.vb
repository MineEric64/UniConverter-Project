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
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(22,Byte),Integer), CType(CType(30,Byte),Integer), CType(CType(43,Byte),Integer))
        Me.Label2.Font = New System.Drawing.Font("NanumBarunGothicOTF", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(268, 275)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 24)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Loading..."
        '
        'VerText
        '
        Me.VerText.AutoSize = true
        Me.VerText.BackColor = System.Drawing.Color.FromArgb(CType(CType(22,Byte),Integer), CType(CType(30,Byte),Integer), CType(CType(43,Byte),Integer))
        Me.VerText.Font = New System.Drawing.Font("NanumBarunGothicOTF", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.VerText.ForeColor = System.Drawing.Color.White
        Me.VerText.Location = New System.Drawing.Point(560, 337)
        Me.VerText.Name = "VerText"
        Me.VerText.Size = New System.Drawing.Size(81, 24)
        Me.VerText.TabIndex = 6
        Me.VerText.Text = "Version"
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(22,Byte),Integer), CType(CType(30,Byte),Integer), CType(CType(43,Byte),Integer))
        Me.Label3.Font = New System.Drawing.Font("NanumBarunGothicOTF", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129,Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(8, 342)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(334, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "ⓒ 2018 ~ 2020 Team Unitor All Rights Reserved."
        '
        'MainScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 12!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackgroundImage = Global.UniConverter.My.Resources.Resources.UniConverter_New_Intro
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(646, 367)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.VerText)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.DoubleBuffered = true
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.Name = "MainScreen"
        Me.Text = "UniConverter"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents VerText As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label3 As Label
End Class

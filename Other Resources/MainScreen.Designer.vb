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
        Me.VerText = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.videoPanel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.videoPanel.SuspendLayout
        Me.SuspendLayout
        '
        'VerText
        '
        Me.VerText.AutoSize = true
        Me.VerText.BackColor = System.Drawing.Color.FromArgb(CType(CType(22,Byte),Integer), CType(CType(30,Byte),Integer), CType(CType(43,Byte),Integer))
        Me.VerText.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.VerText.ForeColor = System.Drawing.Color.White
        Me.VerText.Location = New System.Drawing.Point(560, 370)
        Me.VerText.Name = "VerText"
        Me.VerText.Size = New System.Drawing.Size(85, 25)
        Me.VerText.TabIndex = 6
        Me.VerText.Text = "Version"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(2, 373)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(330, 18)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "ⓒ 2018 ~ 2021 Team Unitor All Rights Reserved."
        '
        'videoPanel
        '
        Me.videoPanel.Controls.Add(Me.Panel1)
        Me.videoPanel.Location = New System.Drawing.Point(0, 0)
        Me.videoPanel.Name = "videoPanel"
        Me.videoPanel.Size = New System.Drawing.Size(646, 367)
        Me.videoPanel.TabIndex = 9
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(20,Byte),Integer), CType(CType(20,Byte),Integer), CType(CType(20,Byte),Integer))
        Me.Panel1.Location = New System.Drawing.Point(0, 365)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(646, 5)
        Me.Panel1.TabIndex = 0
        '
        'MainScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 12!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(22,Byte),Integer), CType(CType(30,Byte),Integer), CType(CType(43,Byte),Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(646, 397)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.VerText)
        Me.Controls.Add(Me.videoPanel)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.DoubleBuffered = true
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.Name = "MainScreen"
        Me.Text = "UniConverter"
        Me.videoPanel.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents VerText As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label3 As Label
    Friend WithEvents videoPanel As Panel
    Friend WithEvents Panel1 As Panel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeveloperMode_Workspace
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
        Me.DebugText = New System.Windows.Forms.TextBox()
        Me.cmdText = New System.Windows.Forms.TextBox()
        Me.DOE = New System.Windows.Forms.Button()
        Me.RebootTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'DebugText
        '
        Me.DebugText.Location = New System.Drawing.Point(12, 44)
        Me.DebugText.Multiline = True
        Me.DebugText.Name = "DebugText"
        Me.DebugText.ReadOnly = True
        Me.DebugText.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.DebugText.Size = New System.Drawing.Size(420, 211)
        Me.DebugText.TabIndex = 0
        '
        'cmdText
        '
        Me.cmdText.Location = New System.Drawing.Point(12, 12)
        Me.cmdText.Name = "cmdText"
        Me.cmdText.Size = New System.Drawing.Size(339, 21)
        Me.cmdText.TabIndex = 1
        '
        'DOE
        '
        Me.DOE.Location = New System.Drawing.Point(357, 10)
        Me.DOE.Name = "DOE"
        Me.DOE.Size = New System.Drawing.Size(75, 23)
        Me.DOE.TabIndex = 2
        Me.DOE.Text = "OK"
        Me.DOE.UseVisualStyleBackColor = True
        '
        'RebootTimer
        '
        Me.RebootTimer.Interval = 3000
        '
        'DeveloperMode_Workspace
        '
        Me.AcceptButton = Me.DOE
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(447, 268)
        Me.Controls.Add(Me.DOE)
        Me.Controls.Add(Me.cmdText)
        Me.Controls.Add(Me.DebugText)
        Me.Name = "DeveloperMode_Workspace"
        Me.Text = "Developer Mode: Workspace"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DebugText As TextBox
    Friend WithEvents cmdText As TextBox
    Friend WithEvents DOE As Button
    Friend WithEvents RebootTimer As Timer
End Class

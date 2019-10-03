<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeveloperMode_Main
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
        Me.DM_CW = New System.Windows.Forms.Button()
        Me.DM_CF = New System.Windows.Forms.Button()
        Me.SLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'DM_CW
        '
        Me.DM_CW.Font = New System.Drawing.Font("-윤디자인웹돋움", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.DM_CW.Location = New System.Drawing.Point(255, 15)
        Me.DM_CW.Name = "DM_CW"
        Me.DM_CW.Size = New System.Drawing.Size(212, 174)
        Me.DM_CW.TabIndex = 0
        Me.DM_CW.Text = "Clean Workspace"
        Me.DM_CW.UseVisualStyleBackColor = True
        '
        'DM_CF
        '
        Me.DM_CF.Font = New System.Drawing.Font("나눔바른고딕OTF", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DM_CF.Location = New System.Drawing.Point(12, 15)
        Me.DM_CF.Name = "DM_CF"
        Me.DM_CF.Size = New System.Drawing.Size(227, 174)
        Me.DM_CF.TabIndex = 1
        Me.DM_CF.Text = "2CNVR"
        Me.DM_CF.UseVisualStyleBackColor = True
        '
        'SLabel
        '
        Me.SLabel.AutoSize = True
        Me.SLabel.Font = New System.Drawing.Font("-윤디자인웹돋움", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.SLabel.Location = New System.Drawing.Point(12, 202)
        Me.SLabel.Name = "SLabel"
        Me.SLabel.Size = New System.Drawing.Size(463, 57)
        Me.SLabel.TabIndex = 2
        Me.SLabel.Text = "This Mode is Developer Mode." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "It causes a lot of errors..." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Also, it doesn't supp" &
    "ort translating the Language."
        '
        'DeveloperMode_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(479, 274)
        Me.Controls.Add(Me.SLabel)
        Me.Controls.Add(Me.DM_CF)
        Me.Controls.Add(Me.DM_CW)
        Me.Name = "DeveloperMode_Main"
        Me.Text = "Developer Mode - Main"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DM_CW As Button
    Friend WithEvents DM_CF As Button
    Friend WithEvents SLabel As Label
End Class

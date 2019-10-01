<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UG_Settings
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
        Me.ChkUpdate = New System.Windows.Forms.CheckBox()
        Me.LatestVer = New System.Windows.Forms.CheckBox()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.ResetButton = New System.Windows.Forms.Button()
        Me.SetUpLight = New System.Windows.Forms.CheckBox()
        Me.langs = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ChkUpdate
        '
        Me.ChkUpdate.AutoSize = True
        Me.ChkUpdate.Checked = True
        Me.ChkUpdate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkUpdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkUpdate.Location = New System.Drawing.Point(17, 15)
        Me.ChkUpdate.Name = "ChkUpdate"
        Me.ChkUpdate.Size = New System.Drawing.Size(147, 18)
        Me.ChkUpdate.TabIndex = 0
        Me.ChkUpdate.Text = "Auto Check Update"
        Me.ChkUpdate.UseVisualStyleBackColor = True
        '
        'LatestVer
        '
        Me.LatestVer.AutoSize = True
        Me.LatestVer.Checked = True
        Me.LatestVer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.LatestVer.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LatestVer.Location = New System.Drawing.Point(17, 37)
        Me.LatestVer.Name = "LatestVer"
        Me.LatestVer.Size = New System.Drawing.Size(254, 18)
        Me.LatestVer.TabIndex = 1
        Me.LatestVer.Text = "Execute Latest Version After Update"
        Me.LatestVer.UseVisualStyleBackColor = True
        '
        'SaveButton
        '
        Me.SaveButton.Font = New System.Drawing.Font("나눔스퀘어라운드 Bold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveButton.Location = New System.Drawing.Point(187, 140)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(82, 47)
        Me.SaveButton.TabIndex = 4
        Me.SaveButton.Text = "Save"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'ResetButton
        '
        Me.ResetButton.Font = New System.Drawing.Font("나눔스퀘어라운드 Bold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ResetButton.Location = New System.Drawing.Point(106, 140)
        Me.ResetButton.Name = "ResetButton"
        Me.ResetButton.Size = New System.Drawing.Size(75, 47)
        Me.ResetButton.TabIndex = 5
        Me.ResetButton.Text = "Reset"
        Me.ResetButton.UseVisualStyleBackColor = True
        '
        'SetUpLight
        '
        Me.SetUpLight.AutoSize = True
        Me.SetUpLight.Checked = True
        Me.SetUpLight.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SetUpLight.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SetUpLight.Location = New System.Drawing.Point(17, 61)
        Me.SetUpLight.Name = "SetUpLight"
        Me.SetUpLight.Size = New System.Drawing.Size(171, 18)
        Me.SetUpLight.TabIndex = 7
        Me.SetUpLight.Text = "Launchpad Setup Light"
        Me.SetUpLight.UseVisualStyleBackColor = True
        '
        'langs
        '
        Me.langs.Font = New System.Drawing.Font("나눔바른고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.langs.FormattingEnabled = True
        Me.langs.Items.AddRange(New Object() {"English", "Korean"})
        Me.langs.Location = New System.Drawing.Point(98, 91)
        Me.langs.Name = "langs"
        Me.langs.Size = New System.Drawing.Size(171, 23)
        Me.langs.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("나눔바른고딕", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 15)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Languages:"
        '
        'UG_Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(284, 199)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.langs)
        Me.Controls.Add(Me.SetUpLight)
        Me.Controls.Add(Me.ResetButton)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.LatestVer)
        Me.Controls.Add(Me.ChkUpdate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "UG_Settings"
        Me.Text = "Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ChkUpdate As CheckBox
    Friend WithEvents LatestVer As CheckBox
    Friend WithEvents SaveButton As Button
    Friend WithEvents ResetButton As Button
    Friend WithEvents SetUpLight As CheckBox
    Friend WithEvents langs As ComboBox
    Friend WithEvents Label1 As Label
End Class

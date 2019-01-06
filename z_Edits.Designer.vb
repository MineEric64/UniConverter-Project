<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class z_Edits
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(z_Edits))
        Me.EControl = New System.Windows.Forms.TabControl()
        Me.Sounds = New System.Windows.Forms.TabPage()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.info = New System.Windows.Forms.TabPage()
        Me.infoT5 = New System.Windows.Forms.Label()
        Me.infoTB5 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.infoT4 = New System.Windows.Forms.Label()
        Me.infoTB4 = New System.Windows.Forms.TextBox()
        Me.infoTB3 = New System.Windows.Forms.TextBox()
        Me.infoTB2 = New System.Windows.Forms.TextBox()
        Me.infoTB1 = New System.Windows.Forms.TextBox()
        Me.infoT3 = New System.Windows.Forms.Label()
        Me.infoT2 = New System.Windows.Forms.Label()
        Me.infoCSave = New System.Windows.Forms.Button()
        Me.infoT1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.WarningLabel1 = New System.Windows.Forms.Label()
        Me.EControl.SuspendLayout()
        Me.Sounds.SuspendLayout()
        Me.info.SuspendLayout()
        Me.SuspendLayout()
        '
        'EControl
        '
        Me.EControl.Controls.Add(Me.Sounds)
        Me.EControl.Controls.Add(Me.info)
        Me.EControl.Location = New System.Drawing.Point(12, 12)
        Me.EControl.Name = "EControl"
        Me.EControl.SelectedIndex = 0
        Me.EControl.Size = New System.Drawing.Size(718, 478)
        Me.EControl.TabIndex = 0
        '
        'Sounds
        '
        Me.Sounds.Controls.Add(Me.WarningLabel1)
        Me.Sounds.Controls.Add(Me.Button5)
        Me.Sounds.Controls.Add(Me.Button4)
        Me.Sounds.Location = New System.Drawing.Point(4, 22)
        Me.Sounds.Name = "Sounds"
        Me.Sounds.Padding = New System.Windows.Forms.Padding(3)
        Me.Sounds.Size = New System.Drawing.Size(710, 452)
        Me.Sounds.TabIndex = 0
        Me.Sounds.Text = "Sounds"
        Me.Sounds.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(193, 54)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(115, 52)
        Me.Button5.TabIndex = 1
        Me.Button5.Text = "Put Sounds in WorkSpace"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(49, 54)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(115, 52)
        Me.Button4.TabIndex = 0
        Me.Button4.Text = "Sound Play" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " (Example)"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'info
        '
        Me.info.Controls.Add(Me.infoT5)
        Me.info.Controls.Add(Me.infoTB5)
        Me.info.Controls.Add(Me.Button3)
        Me.info.Controls.Add(Me.infoT4)
        Me.info.Controls.Add(Me.infoTB4)
        Me.info.Controls.Add(Me.infoTB3)
        Me.info.Controls.Add(Me.infoTB2)
        Me.info.Controls.Add(Me.infoTB1)
        Me.info.Controls.Add(Me.infoT3)
        Me.info.Controls.Add(Me.infoT2)
        Me.info.Controls.Add(Me.infoCSave)
        Me.info.Controls.Add(Me.infoT1)
        Me.info.Location = New System.Drawing.Point(4, 22)
        Me.info.Name = "info"
        Me.info.Size = New System.Drawing.Size(710, 452)
        Me.info.TabIndex = 1
        Me.info.Text = "info"
        Me.info.UseVisualStyleBackColor = True
        '
        'infoT5
        '
        Me.infoT5.AutoSize = True
        Me.infoT5.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.infoT5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.infoT5.Location = New System.Drawing.Point(219, 297)
        Me.infoT5.Name = "infoT5"
        Me.infoT5.Size = New System.Drawing.Size(39, 15)
        Me.infoT5.TabIndex = 20
        Me.infoT5.Text = "Chain"
        '
        'infoTB5
        '
        Me.infoTB5.Location = New System.Drawing.Point(265, 296)
        Me.infoTB5.Name = "infoTB5"
        Me.infoTB5.Size = New System.Drawing.Size(200, 21)
        Me.infoTB5.TabIndex = 19
        Me.infoTB5.Text = "3"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(497, 389)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(86, 46)
        Me.Button3.TabIndex = 18
        Me.Button3.Text = "Delete"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'infoT4
        '
        Me.infoT4.AutoSize = True
        Me.infoT4.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.infoT4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.infoT4.Location = New System.Drawing.Point(160, 245)
        Me.infoT4.Name = "infoT4"
        Me.infoT4.Size = New System.Drawing.Size(98, 15)
        Me.infoT4.TabIndex = 17
        Me.infoT4.Text = "Producer Name"
        '
        'infoTB4
        '
        Me.infoTB4.Location = New System.Drawing.Point(265, 244)
        Me.infoTB4.Name = "infoTB4"
        Me.infoTB4.Size = New System.Drawing.Size(200, 21)
        Me.infoTB4.TabIndex = 16
        Me.infoTB4.Text = "최에릭, UniConverter"
        '
        'infoTB3
        '
        Me.infoTB3.Location = New System.Drawing.Point(265, 196)
        Me.infoTB3.Name = "infoTB3"
        Me.infoTB3.Size = New System.Drawing.Size(200, 21)
        Me.infoTB3.TabIndex = 14
        Me.infoTB3.Text = "8"
        '
        'infoTB2
        '
        Me.infoTB2.Location = New System.Drawing.Point(265, 145)
        Me.infoTB2.Name = "infoTB2"
        Me.infoTB2.Size = New System.Drawing.Size(200, 21)
        Me.infoTB2.TabIndex = 12
        Me.infoTB2.Text = "8"
        '
        'infoTB1
        '
        Me.infoTB1.Location = New System.Drawing.Point(265, 94)
        Me.infoTB1.Name = "infoTB1"
        Me.infoTB1.Size = New System.Drawing.Size(200, 21)
        Me.infoTB1.TabIndex = 9
        Me.infoTB1.Text = "UniConverter"
        '
        'infoT3
        '
        Me.infoT3.AutoSize = True
        Me.infoT3.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.infoT3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.infoT3.Location = New System.Drawing.Point(205, 197)
        Me.infoT3.Name = "infoT3"
        Me.infoT3.Size = New System.Drawing.Size(53, 15)
        Me.infoT3.TabIndex = 15
        Me.infoT3.Text = "buttonY"
        '
        'infoT2
        '
        Me.infoT2.AutoSize = True
        Me.infoT2.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.infoT2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.infoT2.Location = New System.Drawing.Point(205, 146)
        Me.infoT2.Name = "infoT2"
        Me.infoT2.Size = New System.Drawing.Size(54, 15)
        Me.infoT2.TabIndex = 13
        Me.infoT2.Text = "buttonX"
        '
        'infoCSave
        '
        Me.infoCSave.Location = New System.Drawing.Point(606, 389)
        Me.infoCSave.Name = "infoCSave"
        Me.infoCSave.Size = New System.Drawing.Size(91, 46)
        Me.infoCSave.TabIndex = 11
        Me.infoCSave.Text = "Save"
        Me.infoCSave.UseVisualStyleBackColor = True
        '
        'infoT1
        '
        Me.infoT1.AutoSize = True
        Me.infoT1.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.infoT1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.infoT1.Location = New System.Drawing.Point(227, 95)
        Me.infoT1.Name = "infoT1"
        Me.infoT1.Size = New System.Drawing.Size(32, 15)
        Me.infoT1.TabIndex = 10
        Me.infoT1.Text = "Title"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(539, 496)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 39)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(638, 496)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 39)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "OK"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'WarningLabel1
        '
        Me.WarningLabel1.AutoSize = True
        Me.WarningLabel1.Font = New System.Drawing.Font("Adobe Fan Heiti Std B", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.WarningLabel1.Location = New System.Drawing.Point(72, 207)
        Me.WarningLabel1.Name = "WarningLabel1"
        Me.WarningLabel1.Size = New System.Drawing.Size(549, 68)
        Me.WarningLabel1.TabIndex = 2
        Me.WarningLabel1.Text = "The data folder deleted in 1.0.0.2 version." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "This form causes errors. Don't use t" &
    "his form!"
        '
        'z_Edits
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(742, 544)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.EControl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "z_Edits"
        Me.Text = "Edit Unipack"
        Me.EControl.ResumeLayout(False)
        Me.Sounds.ResumeLayout(False)
        Me.Sounds.PerformLayout()
        Me.info.ResumeLayout(False)
        Me.info.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents EControl As TabControl
    Friend WithEvents Sounds As TabPage
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents info As TabPage
    Friend WithEvents Button3 As Button
    Friend WithEvents infoT4 As Label
    Friend WithEvents infoTB4 As TextBox
    Friend WithEvents infoTB3 As TextBox
    Friend WithEvents infoTB1 As TextBox
    Friend WithEvents infoT3 As Label
    Friend WithEvents infoT2 As Label
    Friend WithEvents infoCSave As Button
    Friend WithEvents infoT1 As Label
    Friend WithEvents infoT5 As Label
    Friend WithEvents infoTB5 As TextBox
    Friend WithEvents Button4 As Button
    Friend WithEvents infoTB2 As TextBox
    Friend WithEvents Button5 As Button
    Friend WithEvents WarningLabel1 As Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LEDEdit_Advanced
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
        Me.Load_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.DelayMode1 = New System.Windows.Forms.GroupBox()
        Me.DelayConvert1_3 = New System.Windows.Forms.RadioButton()
        Me.DelayConvert1_1 = New System.Windows.Forms.RadioButton()
        Me.DelayConvert1_2 = New System.Windows.Forms.RadioButton()
        Me.DelayMode2 = New System.Windows.Forms.ComboBox()
        Me.LED_SaveButton = New System.Windows.Forms.Button()
        Me.LED_ResetButton = New System.Windows.Forms.Button()
        Me.DelayMode5 = New System.Windows.Forms.GroupBox()
        Me.DelayConvert4_1 = New System.Windows.Forms.RadioButton()
        Me.DelayConvert4_2 = New System.Windows.Forms.RadioButton()
        Me.DelayMode3 = New System.Windows.Forms.GroupBox()
        Me.DelayConvert2_1 = New System.Windows.Forms.RadioButton()
        Me.DelayConvert2_2 = New System.Windows.Forms.RadioButton()
        Me.DelayMode4 = New System.Windows.Forms.GroupBox()
        Me.DelayConvert3_1 = New System.Windows.Forms.RadioButton()
        Me.DelayConvert3_2 = New System.Windows.Forms.RadioButton()
        Me.DelayMode1.SuspendLayout()
        Me.DelayMode5.SuspendLayout()
        Me.DelayMode3.SuspendLayout()
        Me.DelayMode4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Load_Timer
        '
        Me.Load_Timer.Enabled = True
        '
        'DelayMode1
        '
        Me.DelayMode1.Controls.Add(Me.DelayConvert1_3)
        Me.DelayMode1.Controls.Add(Me.DelayConvert1_1)
        Me.DelayMode1.Controls.Add(Me.DelayConvert1_2)
        Me.DelayMode1.Controls.Add(Me.DelayMode2)
        Me.DelayMode1.Location = New System.Drawing.Point(21, 12)
        Me.DelayMode1.Name = "DelayMode1"
        Me.DelayMode1.Size = New System.Drawing.Size(165, 131)
        Me.DelayMode1.TabIndex = 0
        Me.DelayMode1.TabStop = False
        Me.DelayMode1.Text = "Delay"
        '
        'DelayConvert1_3
        '
        Me.DelayConvert1_3.AutoSize = True
        Me.DelayConvert1_3.Enabled = False
        Me.DelayConvert1_3.Location = New System.Drawing.Point(17, 101)
        Me.DelayConvert1_3.Name = "DelayConvert1_3"
        Me.DelayConvert1_3.Size = New System.Drawing.Size(145, 16)
        Me.DelayConvert1_3.TabIndex = 3
        Me.DelayConvert1_3.Text = "Convert #2 (x + y + z)"
        Me.DelayConvert1_3.UseVisualStyleBackColor = True
        '
        'DelayConvert1_1
        '
        Me.DelayConvert1_1.AutoSize = True
        Me.DelayConvert1_1.Checked = True
        Me.DelayConvert1_1.Location = New System.Drawing.Point(17, 57)
        Me.DelayConvert1_1.Name = "DelayConvert1_1"
        Me.DelayConvert1_1.Size = New System.Drawing.Size(95, 16)
        Me.DelayConvert1_1.TabIndex = 2
        Me.DelayConvert1_1.TabStop = True
        Me.DelayConvert1_1.Text = "Non-Convert"
        Me.DelayConvert1_1.UseVisualStyleBackColor = True
        '
        'DelayConvert1_2
        '
        Me.DelayConvert1_2.AutoSize = True
        Me.DelayConvert1_2.Enabled = False
        Me.DelayConvert1_2.Location = New System.Drawing.Point(17, 79)
        Me.DelayConvert1_2.Name = "DelayConvert1_2"
        Me.DelayConvert1_2.Size = New System.Drawing.Size(124, 16)
        Me.DelayConvert1_2.TabIndex = 1
        Me.DelayConvert1_2.Text = "Convert #1 (x + y)"
        Me.DelayConvert1_2.UseVisualStyleBackColor = True
        '
        'DelayMode2
        '
        Me.DelayMode2.FormattingEnabled = True
        Me.DelayMode2.Items.AddRange(New Object() {"Note Length", "Delta Time", "Absolute Time"})
        Me.DelayMode2.Location = New System.Drawing.Point(17, 20)
        Me.DelayMode2.Name = "DelayMode2"
        Me.DelayMode2.Size = New System.Drawing.Size(121, 20)
        Me.DelayMode2.TabIndex = 0
        Me.DelayMode2.Text = "Note Length"
        '
        'LED_SaveButton
        '
        Me.LED_SaveButton.Location = New System.Drawing.Point(192, 314)
        Me.LED_SaveButton.Name = "LED_SaveButton"
        Me.LED_SaveButton.Size = New System.Drawing.Size(165, 61)
        Me.LED_SaveButton.TabIndex = 1
        Me.LED_SaveButton.Text = "Save"
        Me.LED_SaveButton.UseVisualStyleBackColor = True
        '
        'LED_ResetButton
        '
        Me.LED_ResetButton.Location = New System.Drawing.Point(21, 314)
        Me.LED_ResetButton.Name = "LED_ResetButton"
        Me.LED_ResetButton.Size = New System.Drawing.Size(165, 61)
        Me.LED_ResetButton.TabIndex = 2
        Me.LED_ResetButton.Text = "Reset"
        Me.LED_ResetButton.UseVisualStyleBackColor = True
        '
        'DelayMode5
        '
        Me.DelayMode5.Controls.Add(Me.DelayConvert4_1)
        Me.DelayMode5.Controls.Add(Me.DelayConvert4_2)
        Me.DelayMode5.Enabled = False
        Me.DelayMode5.Location = New System.Drawing.Point(192, 171)
        Me.DelayMode5.Name = "DelayMode5"
        Me.DelayMode5.Size = New System.Drawing.Size(165, 137)
        Me.DelayMode5.TabIndex = 4
        Me.DelayMode5.TabStop = False
        Me.DelayMode5.Text = "Absolute Time"
        '
        'DelayConvert4_1
        '
        Me.DelayConvert4_1.AutoSize = True
        Me.DelayConvert4_1.Checked = True
        Me.DelayConvert4_1.Location = New System.Drawing.Point(6, 20)
        Me.DelayConvert4_1.Name = "DelayConvert4_1"
        Me.DelayConvert4_1.Size = New System.Drawing.Size(95, 16)
        Me.DelayConvert4_1.TabIndex = 2
        Me.DelayConvert4_1.TabStop = True
        Me.DelayConvert4_1.Text = "Non-Convert"
        Me.DelayConvert4_1.UseVisualStyleBackColor = True
        '
        'DelayConvert4_2
        '
        Me.DelayConvert4_2.AutoSize = True
        Me.DelayConvert4_2.Location = New System.Drawing.Point(6, 57)
        Me.DelayConvert4_2.Name = "DelayConvert4_2"
        Me.DelayConvert4_2.Size = New System.Drawing.Size(153, 16)
        Me.DelayConvert4_2.TabIndex = 1
        Me.DelayConvert4_2.Text = "[1] Exact Time of MIDI"
        Me.DelayConvert4_2.UseVisualStyleBackColor = True
        '
        'DelayMode3
        '
        Me.DelayMode3.Controls.Add(Me.DelayConvert2_1)
        Me.DelayMode3.Controls.Add(Me.DelayConvert2_2)
        Me.DelayMode3.Enabled = False
        Me.DelayMode3.Location = New System.Drawing.Point(192, 17)
        Me.DelayMode3.Name = "DelayMode3"
        Me.DelayMode3.Size = New System.Drawing.Size(165, 126)
        Me.DelayMode3.TabIndex = 5
        Me.DelayMode3.TabStop = False
        Me.DelayMode3.Text = "Note Length"
        '
        'DelayConvert2_1
        '
        Me.DelayConvert2_1.AutoSize = True
        Me.DelayConvert2_1.Checked = True
        Me.DelayConvert2_1.Location = New System.Drawing.Point(15, 20)
        Me.DelayConvert2_1.Name = "DelayConvert2_1"
        Me.DelayConvert2_1.Size = New System.Drawing.Size(95, 16)
        Me.DelayConvert2_1.TabIndex = 2
        Me.DelayConvert2_1.TabStop = True
        Me.DelayConvert2_1.Text = "Non-Convert"
        Me.DelayConvert2_1.UseVisualStyleBackColor = True
        '
        'DelayConvert2_2
        '
        Me.DelayConvert2_2.AutoSize = True
        Me.DelayConvert2_2.Enabled = False
        Me.DelayConvert2_2.Location = New System.Drawing.Point(15, 57)
        Me.DelayConvert2_2.Name = "DelayConvert2_2"
        Me.DelayConvert2_2.Size = New System.Drawing.Size(75, 16)
        Me.DelayConvert2_2.TabIndex = 1
        Me.DelayConvert2_2.Text = "[1] X + Y"
        Me.DelayConvert2_2.UseVisualStyleBackColor = True
        '
        'DelayMode4
        '
        Me.DelayMode4.Controls.Add(Me.DelayConvert3_1)
        Me.DelayMode4.Controls.Add(Me.DelayConvert3_2)
        Me.DelayMode4.Enabled = False
        Me.DelayMode4.Location = New System.Drawing.Point(21, 171)
        Me.DelayMode4.Name = "DelayMode4"
        Me.DelayMode4.Size = New System.Drawing.Size(165, 137)
        Me.DelayMode4.TabIndex = 6
        Me.DelayMode4.TabStop = False
        Me.DelayMode4.Text = "Delta Time"
        '
        'DelayConvert3_1
        '
        Me.DelayConvert3_1.AutoSize = True
        Me.DelayConvert3_1.Checked = True
        Me.DelayConvert3_1.Location = New System.Drawing.Point(15, 20)
        Me.DelayConvert3_1.Name = "DelayConvert3_1"
        Me.DelayConvert3_1.Size = New System.Drawing.Size(95, 16)
        Me.DelayConvert3_1.TabIndex = 2
        Me.DelayConvert3_1.TabStop = True
        Me.DelayConvert3_1.Text = "Non-Convert"
        Me.DelayConvert3_1.UseVisualStyleBackColor = True
        '
        'DelayConvert3_2
        '
        Me.DelayConvert3_2.AutoSize = True
        Me.DelayConvert3_2.Enabled = False
        Me.DelayConvert3_2.Location = New System.Drawing.Point(15, 57)
        Me.DelayConvert3_2.Name = "DelayConvert3_2"
        Me.DelayConvert3_2.Size = New System.Drawing.Size(75, 16)
        Me.DelayConvert3_2.TabIndex = 1
        Me.DelayConvert3_2.Text = "[1] X + Y"
        Me.DelayConvert3_2.UseVisualStyleBackColor = True
        '
        'LEDEdit_Advanced
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(369, 387)
        Me.Controls.Add(Me.DelayMode4)
        Me.Controls.Add(Me.DelayMode3)
        Me.Controls.Add(Me.DelayMode5)
        Me.Controls.Add(Me.LED_ResetButton)
        Me.Controls.Add(Me.LED_SaveButton)
        Me.Controls.Add(Me.DelayMode1)
        Me.Name = "LEDEdit_Advanced"
        Me.Text = "keyLED Edit: Advanced Mode"
        Me.DelayMode1.ResumeLayout(False)
        Me.DelayMode1.PerformLayout()
        Me.DelayMode5.ResumeLayout(False)
        Me.DelayMode5.PerformLayout()
        Me.DelayMode3.ResumeLayout(False)
        Me.DelayMode3.PerformLayout()
        Me.DelayMode4.ResumeLayout(False)
        Me.DelayMode4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Load_Timer As Timer
    Friend WithEvents DelayMode1 As GroupBox
    Friend WithEvents DelayMode2 As ComboBox
    Friend WithEvents DelayConvert1_1 As RadioButton
    Friend WithEvents DelayConvert1_2 As RadioButton
    Friend WithEvents DelayConvert1_3 As RadioButton
    Friend WithEvents LED_SaveButton As Button
    Friend WithEvents LED_ResetButton As Button
    Friend WithEvents DelayMode5 As GroupBox
    Friend WithEvents DelayConvert4_1 As RadioButton
    Friend WithEvents DelayConvert4_2 As RadioButton
    Friend WithEvents DelayMode3 As GroupBox
    Friend WithEvents DelayConvert2_1 As RadioButton
    Friend WithEvents DelayConvert2_2 As RadioButton
    Friend WithEvents DelayMode4 As GroupBox
    Friend WithEvents DelayConvert3_1 As RadioButton
    Friend WithEvents DelayConvert3_2 As RadioButton
End Class

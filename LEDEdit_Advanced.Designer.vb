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
        Me.DelayConvert3 = New System.Windows.Forms.RadioButton()
        Me.DelayConvert1 = New System.Windows.Forms.RadioButton()
        Me.DelayConvert2 = New System.Windows.Forms.RadioButton()
        Me.DelayMode2 = New System.Windows.Forms.ComboBox()
        Me.LED_SaveButton = New System.Windows.Forms.Button()
        Me.LED_ResetButton = New System.Windows.Forms.Button()
        Me.DelayMode1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Load_Timer
        '
        Me.Load_Timer.Enabled = True
        '
        'DelayMode1
        '
        Me.DelayMode1.Controls.Add(Me.DelayConvert3)
        Me.DelayMode1.Controls.Add(Me.DelayConvert1)
        Me.DelayMode1.Controls.Add(Me.DelayConvert2)
        Me.DelayMode1.Controls.Add(Me.DelayMode2)
        Me.DelayMode1.Location = New System.Drawing.Point(21, 12)
        Me.DelayMode1.Name = "DelayMode1"
        Me.DelayMode1.Size = New System.Drawing.Size(165, 120)
        Me.DelayMode1.TabIndex = 0
        Me.DelayMode1.TabStop = False
        Me.DelayMode1.Text = "Delay"
        '
        'DelayConvert3
        '
        Me.DelayConvert3.AutoSize = True
        Me.DelayConvert3.Enabled = False
        Me.DelayConvert3.Location = New System.Drawing.Point(17, 90)
        Me.DelayConvert3.Name = "DelayConvert3"
        Me.DelayConvert3.Size = New System.Drawing.Size(145, 16)
        Me.DelayConvert3.TabIndex = 3
        Me.DelayConvert3.Text = "Convert #2 (x + y + z)"
        Me.DelayConvert3.UseVisualStyleBackColor = True
        '
        'DelayConvert1
        '
        Me.DelayConvert1.AutoSize = True
        Me.DelayConvert1.Checked = True
        Me.DelayConvert1.Location = New System.Drawing.Point(17, 46)
        Me.DelayConvert1.Name = "DelayConvert1"
        Me.DelayConvert1.Size = New System.Drawing.Size(95, 16)
        Me.DelayConvert1.TabIndex = 2
        Me.DelayConvert1.TabStop = True
        Me.DelayConvert1.Text = "Non-Convert"
        Me.DelayConvert1.UseVisualStyleBackColor = True
        '
        'DelayConvert2
        '
        Me.DelayConvert2.AutoSize = True
        Me.DelayConvert2.Enabled = False
        Me.DelayConvert2.Location = New System.Drawing.Point(17, 68)
        Me.DelayConvert2.Name = "DelayConvert2"
        Me.DelayConvert2.Size = New System.Drawing.Size(124, 16)
        Me.DelayConvert2.TabIndex = 1
        Me.DelayConvert2.Text = "Convert #1 (x + y)"
        Me.DelayConvert2.UseVisualStyleBackColor = True
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
        Me.LED_SaveButton.Location = New System.Drawing.Point(258, 342)
        Me.LED_SaveButton.Name = "LED_SaveButton"
        Me.LED_SaveButton.Size = New System.Drawing.Size(91, 33)
        Me.LED_SaveButton.TabIndex = 1
        Me.LED_SaveButton.Text = "Save"
        Me.LED_SaveButton.UseVisualStyleBackColor = True
        '
        'LED_ResetButton
        '
        Me.LED_ResetButton.Location = New System.Drawing.Point(152, 342)
        Me.LED_ResetButton.Name = "LED_ResetButton"
        Me.LED_ResetButton.Size = New System.Drawing.Size(91, 33)
        Me.LED_ResetButton.TabIndex = 2
        Me.LED_ResetButton.Text = "Reset"
        Me.LED_ResetButton.UseVisualStyleBackColor = True
        '
        'LEDEdit_Advanced
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(361, 387)
        Me.Controls.Add(Me.LED_ResetButton)
        Me.Controls.Add(Me.LED_SaveButton)
        Me.Controls.Add(Me.DelayMode1)
        Me.Name = "LEDEdit_Advanced"
        Me.Text = "keyLED Edit: Advanced Mode"
        Me.DelayMode1.ResumeLayout(False)
        Me.DelayMode1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Load_Timer As Timer
    Friend WithEvents DelayMode1 As GroupBox
    Friend WithEvents DelayMode2 As ComboBox
    Friend WithEvents DelayConvert1 As RadioButton
    Friend WithEvents DelayConvert2 As RadioButton
    Friend WithEvents DelayConvert3 As RadioButton
    Friend WithEvents LED_SaveButton As Button
    Friend WithEvents LED_ResetButton As Button
End Class

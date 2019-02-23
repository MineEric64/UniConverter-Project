<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class z_Tutorial
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(z_Tutorial))
        Me.FixBox1 = New System.Windows.Forms.RichTextBox()
        Me.TrialBox1 = New System.Windows.Forms.ComboBox()
        Me.help1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'FixBox1
        '
        Me.FixBox1.Location = New System.Drawing.Point(27, 61)
        Me.FixBox1.Name = "FixBox1"
        Me.FixBox1.ReadOnly = True
        Me.FixBox1.Size = New System.Drawing.Size(382, 214)
        Me.FixBox1.TabIndex = 0
        Me.FixBox1.Text = "" & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(10) & "                               " & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(10) & "                            I'll Fix Up Your " &
    "Questions :)" & Global.Microsoft.VisualBasic.ChrW(10) & "                                           GG"
        '
        'TrialBox1
        '
        Me.TrialBox1.FormattingEnabled = True
        Me.TrialBox1.Items.AddRange(New Object() {"[Error Code: 9]", "[What is Put Sounds in Folder?]", "[How to Check for Updates?]", "[What is General, and Special?]"})
        Me.TrialBox1.Location = New System.Drawing.Point(39, 26)
        Me.TrialBox1.Name = "TrialBox1"
        Me.TrialBox1.Size = New System.Drawing.Size(244, 20)
        Me.TrialBox1.TabIndex = 1
        '
        'help1
        '
        Me.help1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.help1.Location = New System.Drawing.Point(289, 26)
        Me.help1.Name = "help1"
        Me.help1.Size = New System.Drawing.Size(101, 20)
        Me.help1.TabIndex = 2
        Me.help1.Text = "I need Help."
        Me.help1.UseVisualStyleBackColor = True
        '
        'Tutorial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(438, 314)
        Me.Controls.Add(Me.help1)
        Me.Controls.Add(Me.TrialBox1)
        Me.Controls.Add(Me.FixBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Tutorial"
        Me.Text = "Tutorial"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FixBox1 As RichTextBox
    Friend WithEvents TrialBox1 As ComboBox
    Friend WithEvents help1 As Button
End Class

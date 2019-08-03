<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tutorials
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Tutorials))
        Me.Q_ListView = New System.Windows.Forms.ListView()
        Me.Qs = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.A_RichTextBox = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'Q_ListView
        '
        Me.Q_ListView.AllowDrop = True
        Me.Q_ListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Qs})
        Me.Q_ListView.Font = New System.Drawing.Font("나눔고딕", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Q_ListView.FullRowSelect = True
        Me.Q_ListView.Location = New System.Drawing.Point(12, 12)
        Me.Q_ListView.MultiSelect = False
        Me.Q_ListView.Name = "Q_ListView"
        Me.Q_ListView.Size = New System.Drawing.Size(502, 150)
        Me.Q_ListView.TabIndex = 24
        Me.Q_ListView.UseCompatibleStateImageBehavior = False
        Me.Q_ListView.View = System.Windows.Forms.View.Details
        '
        'Qs
        '
        Me.Qs.Text = "Questions"
        Me.Qs.Width = 498
        '
        'A_RichTextBox
        '
        Me.A_RichTextBox.Font = New System.Drawing.Font("나눔고딕", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.A_RichTextBox.Location = New System.Drawing.Point(12, 168)
        Me.A_RichTextBox.Name = "A_RichTextBox"
        Me.A_RichTextBox.ReadOnly = True
        Me.A_RichTextBox.Size = New System.Drawing.Size(502, 270)
        Me.A_RichTextBox.TabIndex = 25
        Me.A_RichTextBox.Text = ""
        '
        'Tutorials
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 450)
        Me.Controls.Add(Me.A_RichTextBox)
        Me.Controls.Add(Me.Q_ListView)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Tutorials"
        Me.Text = "Tutorials"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Q_ListView As ListView
    Friend WithEvents Qs As ColumnHeader
    Friend WithEvents A_RichTextBox As RichTextBox
End Class

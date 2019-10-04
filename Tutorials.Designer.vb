<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Tutorials
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
        resources.ApplyResources(Me.Q_ListView, "Q_ListView")
        Me.Q_ListView.FullRowSelect = True
        Me.Q_ListView.HideSelection = False
        Me.Q_ListView.MultiSelect = False
        Me.Q_ListView.Name = "Q_ListView"
        Me.Q_ListView.UseCompatibleStateImageBehavior = False
        Me.Q_ListView.View = System.Windows.Forms.View.Details
        '
        'Qs
        '
        resources.ApplyResources(Me.Qs, "Qs")
        '
        'A_RichTextBox
        '
        resources.ApplyResources(Me.A_RichTextBox, "A_RichTextBox")
        Me.A_RichTextBox.Name = "A_RichTextBox"
        Me.A_RichTextBox.ReadOnly = True
        '
        'Tutorials
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.A_RichTextBox)
        Me.Controls.Add(Me.Q_ListView)
        Me.MaximizeBox = False
        Me.Name = "Tutorials"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Q_ListView As ListView
    Friend WithEvents Qs As ColumnHeader
    Friend WithEvents A_RichTextBox As RichTextBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DeveloperMode_Project
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
        Me.components = New System.ComponentModel.Container()
        Me.Info_ListView = New System.Windows.Forms.ListView()
        Me.Info = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Project_FNTextBox = New System.Windows.Forms.TextBox()
        Me.Project_OpenButton = New System.Windows.Forms.Button()
        Me.Info_TextBox = New FastColoredTextBoxNS.FastColoredTextBox()
        Me.ofd_Project = New System.Windows.Forms.OpenFileDialog()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        CType(Me.Info_TextBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Info_ListView
        '
        Me.Info_ListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Info})
        Me.Info_ListView.Font = New System.Drawing.Font("맑은 고딕", 9.0!)
        Me.Info_ListView.FullRowSelect = True
        Me.Info_ListView.Location = New System.Drawing.Point(12, 51)
        Me.Info_ListView.MultiSelect = False
        Me.Info_ListView.Name = "Info_ListView"
        Me.Info_ListView.Size = New System.Drawing.Size(254, 387)
        Me.Info_ListView.TabIndex = 25
        Me.Info_ListView.UseCompatibleStateImageBehavior = False
        Me.Info_ListView.View = System.Windows.Forms.View.Details
        '
        'Info
        '
        Me.Info.Text = "Ableton Project Informations"
        Me.Info.Width = 250
        '
        'Project_FNTextBox
        '
        Me.Project_FNTextBox.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Project_FNTextBox.Location = New System.Drawing.Point(12, 12)
        Me.Project_FNTextBox.Name = "Project_FNTextBox"
        Me.Project_FNTextBox.Size = New System.Drawing.Size(412, 29)
        Me.Project_FNTextBox.TabIndex = 27
        '
        'Project_OpenButton
        '
        Me.Project_OpenButton.Font = New System.Drawing.Font("맑은 고딕", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Project_OpenButton.Location = New System.Drawing.Point(442, 12)
        Me.Project_OpenButton.Name = "Project_OpenButton"
        Me.Project_OpenButton.Size = New System.Drawing.Size(90, 29)
        Me.Project_OpenButton.TabIndex = 28
        Me.Project_OpenButton.Text = "Open"
        Me.Project_OpenButton.UseVisualStyleBackColor = True
        '
        'Info_TextBox
        '
        Me.Info_TextBox.AutoCompleteBracketsList = New Char() {Global.Microsoft.VisualBasic.ChrW(40), Global.Microsoft.VisualBasic.ChrW(41), Global.Microsoft.VisualBasic.ChrW(123), Global.Microsoft.VisualBasic.ChrW(125), Global.Microsoft.VisualBasic.ChrW(91), Global.Microsoft.VisualBasic.ChrW(93), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(39), Global.Microsoft.VisualBasic.ChrW(39)}
        Me.Info_TextBox.AutoScrollMinSize = New System.Drawing.Size(27, 14)
        Me.Info_TextBox.BackBrush = Nothing
        Me.Info_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Info_TextBox.CharHeight = 14
        Me.Info_TextBox.CharWidth = 8
        Me.Info_TextBox.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Info_TextBox.DisabledColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.Info_TextBox.Font = New System.Drawing.Font("Courier New", 9.75!)
        Me.Info_TextBox.IsReplaceMode = False
        Me.Info_TextBox.Location = New System.Drawing.Point(288, 51)
        Me.Info_TextBox.Name = "Info_TextBox"
        Me.Info_TextBox.Paddings = New System.Windows.Forms.Padding(0)
        Me.Info_TextBox.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Info_TextBox.Size = New System.Drawing.Size(259, 387)
        Me.Info_TextBox.TabIndex = 29
        Me.Info_TextBox.Zoom = 100
        '
        'ofd_Project
        '
        Me.ofd_Project.Filter = "Ableton Project File|*.als"
        Me.ofd_Project.Title = "Please Select Ableton Project File"
        '
        'BackgroundWorker1
        '
        '
        'DeveloperMode_Project
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(565, 450)
        Me.Controls.Add(Me.Info_TextBox)
        Me.Controls.Add(Me.Project_OpenButton)
        Me.Controls.Add(Me.Project_FNTextBox)
        Me.Controls.Add(Me.Info_ListView)
        Me.Name = "DeveloperMode_Project"
        Me.Text = "Developer Mode - Ableton Project Info"
        CType(Me.Info_TextBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Info_ListView As ListView
    Friend WithEvents Info As ColumnHeader
    Friend WithEvents Project_FNTextBox As TextBox
    Friend WithEvents Project_OpenButton As Button
    Friend WithEvents Info_TextBox As FastColoredTextBoxNS.FastColoredTextBox
    Friend WithEvents ofd_Project As OpenFileDialog
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
End Class

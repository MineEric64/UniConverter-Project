<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CuttingSound
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
        Me.colTest = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.colisAuto = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colFileName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEnd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnAddRow = New System.Windows.Forms.Button()
        Me.SoundCutControl = New System.Windows.Forms.DataGridView()
        Me.lblSoundLength = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblstat = New System.Windows.Forms.Label()
        Me.btnSelectdir = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ofdFile = New System.Windows.Forms.OpenFileDialog()
        Me.btnSelectSource = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.txtTargetDir = New System.Windows.Forms.TextBox()
        Me.txtSource = New System.Windows.Forms.TextBox()
        Me.CutButton = New System.Windows.Forms.Button()
        Me.fbdFolder = New System.Windows.Forms.FolderBrowserDialog()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.AfterTabCheckBox = New System.Windows.Forms.CheckBox()
        Me.asdFileButton = New System.Windows.Forms.Button()
        CType(Me.SoundCutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'colTest
        '
        Me.colTest.HeaderText = "Test"
        Me.colTest.Name = "colTest"
        Me.colTest.Width = 50
        '
        'colisAuto
        '
        Me.colisAuto.HeaderText = "Auto Naming"
        Me.colisAuto.Name = "colisAuto"
        '
        'colFileName
        '
        Me.colFileName.HeaderText = "FileName"
        Me.colFileName.MaxInputLength = 4000
        Me.colFileName.Name = "colFileName"
        '
        'colEnd
        '
        Me.colEnd.HeaderText = "End Pos (or Length)"
        Me.colEnd.MaxInputLength = 17
        Me.colEnd.Name = "colEnd"
        '
        'colStart
        '
        Me.colStart.HeaderText = "Start Pos"
        Me.colStart.MaxInputLength = 17
        Me.colStart.Name = "colStart"
        Me.colStart.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'btnAddRow
        '
        Me.btnAddRow.Location = New System.Drawing.Point(414, 107)
        Me.btnAddRow.Name = "btnAddRow"
        Me.btnAddRow.Size = New System.Drawing.Size(110, 51)
        Me.btnAddRow.TabIndex = 32
        Me.btnAddRow.Text = "Add Row"
        Me.btnAddRow.UseVisualStyleBackColor = True
        '
        'SoundCutControl
        '
        Me.SoundCutControl.AllowUserToAddRows = False
        Me.SoundCutControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.SoundCutControl.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colStart, Me.colEnd, Me.colFileName, Me.colisAuto, Me.colTest})
        Me.SoundCutControl.Location = New System.Drawing.Point(25, 196)
        Me.SoundCutControl.Name = "SoundCutControl"
        Me.SoundCutControl.RowTemplate.Height = 23
        Me.SoundCutControl.Size = New System.Drawing.Size(499, 248)
        Me.SoundCutControl.TabIndex = 31
        '
        'lblSoundLength
        '
        Me.lblSoundLength.AutoSize = True
        Me.lblSoundLength.Location = New System.Drawing.Point(347, 181)
        Me.lblSoundLength.Name = "lblSoundLength"
        Me.lblSoundLength.Size = New System.Drawing.Size(47, 12)
        Me.lblSoundLength.TabIndex = 30
        Me.lblSoundLength.Text = "Length:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 181)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 12)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Script"
        '
        'lblstat
        '
        Me.lblstat.AutoSize = True
        Me.lblstat.Location = New System.Drawing.Point(22, 47)
        Me.lblstat.Name = "lblstat"
        Me.lblstat.Size = New System.Drawing.Size(41, 12)
        Me.lblstat.TabIndex = 28
        Me.lblstat.Text = "Ready"
        '
        'btnSelectdir
        '
        Me.btnSelectdir.Location = New System.Drawing.Point(349, 104)
        Me.btnSelectdir.Name = "btnSelectdir"
        Me.btnSelectdir.Size = New System.Drawing.Size(27, 23)
        Me.btnSelectdir.TabIndex = 27
        Me.btnSelectdir.Text = "..."
        Me.btnSelectdir.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 12)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Target Dir"
        '
        'ofdFile
        '
        Me.ofdFile.Filter = "WAV File|*.wav"
        '
        'btnSelectSource
        '
        Me.btnSelectSource.Location = New System.Drawing.Point(349, 75)
        Me.btnSelectSource.Name = "btnSelectSource"
        Me.btnSelectSource.Size = New System.Drawing.Size(27, 23)
        Me.btnSelectSource.TabIndex = 26
        Me.btnSelectSource.Text = "..."
        Me.btnSelectSource.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 12)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Source File"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(25, 196)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(354, 248)
        Me.RichTextBox1.TabIndex = 20
        Me.RichTextBox1.Text = "[Start of Sound (format ex: 0:0:0.0)] [Start of Sound (format ex: 0:0:1.1000) OR " &
    "(second)s] [file name OR auto (if you select auto, soundname will be saved to tr" &
    "_(LINENUMBER).wav)]"
        '
        'txtTargetDir
        '
        Me.txtTargetDir.Location = New System.Drawing.Point(97, 104)
        Me.txtTargetDir.Name = "txtTargetDir"
        Me.txtTargetDir.Size = New System.Drawing.Size(246, 21)
        Me.txtTargetDir.TabIndex = 19
        '
        'txtSource
        '
        Me.txtSource.Location = New System.Drawing.Point(97, 77)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.Size = New System.Drawing.Size(246, 21)
        Me.txtSource.TabIndex = 18
        '
        'CutButton
        '
        Me.CutButton.Location = New System.Drawing.Point(301, 13)
        Me.CutButton.Name = "CutButton"
        Me.CutButton.Size = New System.Drawing.Size(75, 56)
        Me.CutButton.TabIndex = 17
        Me.CutButton.Text = "Cut!"
        Me.CutButton.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(22, 21)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(271, 23)
        Me.ProgressBar1.TabIndex = 23
        '
        'AfterTabCheckBox
        '
        Me.AfterTabCheckBox.AutoSize = True
        Me.AfterTabCheckBox.Checked = True
        Me.AfterTabCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AfterTabCheckBox.Location = New System.Drawing.Point(24, 140)
        Me.AfterTabCheckBox.Name = "AfterTabCheckBox"
        Me.AfterTabCheckBox.Size = New System.Drawing.Size(267, 16)
        Me.AfterTabCheckBox.TabIndex = 33
        Me.AfterTabCheckBox.Text = "Put sounds in keySound Tab automatically"
        Me.AfterTabCheckBox.UseVisualStyleBackColor = True
        '
        'asdFileButton
        '
        Me.asdFileButton.Location = New System.Drawing.Point(430, 13)
        Me.asdFileButton.Name = "asdFileButton"
        Me.asdFileButton.Size = New System.Drawing.Size(94, 49)
        Me.asdFileButton.TabIndex = 34
        Me.asdFileButton.Text = "Hoxy using .asd file?"
        Me.asdFileButton.UseVisualStyleBackColor = True
        '
        'CuttingSound
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(546, 456)
        Me.Controls.Add(Me.asdFileButton)
        Me.Controls.Add(Me.AfterTabCheckBox)
        Me.Controls.Add(Me.btnAddRow)
        Me.Controls.Add(Me.SoundCutControl)
        Me.Controls.Add(Me.lblSoundLength)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblstat)
        Me.Controls.Add(Me.btnSelectdir)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnSelectSource)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.txtTargetDir)
        Me.Controls.Add(Me.txtSource)
        Me.Controls.Add(Me.CutButton)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Name = "CuttingSound"
        Me.Text = "Sound Cutter"
        CType(Me.SoundCutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents colTest As DataGridViewButtonColumn
    Friend WithEvents colisAuto As DataGridViewCheckBoxColumn
    Friend WithEvents colFileName As DataGridViewTextBoxColumn
    Friend WithEvents colEnd As DataGridViewTextBoxColumn
    Friend WithEvents colStart As DataGridViewTextBoxColumn
    Friend WithEvents btnAddRow As Button
    Friend WithEvents SoundCutControl As DataGridView
    Friend WithEvents lblSoundLength As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblstat As Label
    Friend WithEvents btnSelectdir As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents ofdFile As OpenFileDialog
    Friend WithEvents btnSelectSource As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents txtTargetDir As TextBox
    Friend WithEvents txtSource As TextBox
    Friend WithEvents CutButton As Button
    Friend WithEvents fbdFolder As FolderBrowserDialog
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents AfterTabCheckBox As CheckBox
    Friend WithEvents asdFileButton As Button
End Class

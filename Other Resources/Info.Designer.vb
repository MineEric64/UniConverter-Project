<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Info
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Info))
        Me.InfoText = New System.Windows.Forms.Label()
        Me.TipText1 = New System.Windows.Forms.Label()
        Me.ModeE = New System.Windows.Forms.TextBox()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.ucv_link = New System.Windows.Forms.LinkLabel()
        Me.UCV_Alpha = New System.Windows.Forms.PictureBox()
        Me.unitor_link = New System.Windows.Forms.LinkLabel()
        Me.ucvg_link = New System.Windows.Forms.LinkLabel()
        Me.unipad_link = New System.Windows.Forms.LinkLabel()
        Me.unipadc_link = New System.Windows.Forms.LinkLabel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.panelVideoPlayer = New System.Windows.Forms.Panel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.UCV_Alpha,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel1.SuspendLayout
        Me.SuspendLayout
        '
        'InfoText
        '
        Me.InfoText.AutoSize = True
        Me.InfoText.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.InfoText.ForeColor = System.Drawing.Color.White
        Me.InfoText.Location = New System.Drawing.Point(300, 305)
        Me.InfoText.Name = "InfoText"
        Me.InfoText.Size = New System.Drawing.Size(49, 24)
        Me.InfoText.TabIndex = 2
        Me.InfoText.Text = "Ver."
        '
        'TipText1
        '
        Me.TipText1.AutoSize = True
        Me.TipText1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TipText1.ForeColor = System.Drawing.Color.White
        Me.TipText1.Location = New System.Drawing.Point(42, 205)
        Me.TipText1.Name = "TipText1"
        Me.TipText1.Size = New System.Drawing.Size(316, 15)
        Me.TipText1.TabIndex = 3
        Me.TipText1.Text = "Copyright 2018 ~ 2022. Team Unitor All Rights Reserved."
        '
        'ModeE
        '
        Me.ModeE.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.ModeE.Font = New System.Drawing.Font("NanumBarunGothic", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ModeE.ForeColor = System.Drawing.Color.White
        Me.ModeE.Location = New System.Drawing.Point(77, 222)
        Me.ModeE.Name = "ModeE"
        Me.ModeE.Size = New System.Drawing.Size(169, 21)
        Me.ModeE.TabIndex = 4
        '
        'OKButton
        '
        Me.OKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OKButton.Font = New System.Drawing.Font("NanumGothic", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.OKButton.ForeColor = System.Drawing.Color.White
        Me.OKButton.Location = New System.Drawing.Point(252, 222)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(75, 23)
        Me.OKButton.TabIndex = 5
        Me.OKButton.Text = "OK"
        Me.OKButton.UseVisualStyleBackColor = True
        '
        'ucv_link
        '
        Me.ucv_link.AutoSize = True
        Me.ucv_link.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ucv_link.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ucv_link.LinkColor = System.Drawing.Color.White
        Me.ucv_link.Location = New System.Drawing.Point(12, 290)
        Me.ucv_link.Name = "ucv_link"
        Me.ucv_link.Size = New System.Drawing.Size(102, 15)
        Me.ucv_link.TabIndex = 6
        Me.ucv_link.TabStop = True
        Me.ucv_link.Text = "UniConverter Site"
        '
        'UCV_Alpha
        '
        Me.UCV_Alpha.Image = Global.UniConverter.My.Resources.Resources.UniConverter_New_Title
        Me.UCV_Alpha.Location = New System.Drawing.Point(121, 160)
        Me.UCV_Alpha.Name = "UCV_Alpha"
        Me.UCV_Alpha.Size = New System.Drawing.Size(163, 42)
        Me.UCV_Alpha.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.UCV_Alpha.TabIndex = 7
        Me.UCV_Alpha.TabStop = False
        '
        'unitor_link
        '
        Me.unitor_link.AutoSize = True
        Me.unitor_link.Cursor = System.Windows.Forms.Cursors.Hand
        Me.unitor_link.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.unitor_link.LinkColor = System.Drawing.Color.White
        Me.unitor_link.Location = New System.Drawing.Point(12, 313)
        Me.unitor_link.Name = "unitor_link"
        Me.unitor_link.Size = New System.Drawing.Size(111, 15)
        Me.unitor_link.TabIndex = 8
        Me.unitor_link.TabStop = True
        Me.unitor_link.Text = "MineEric64 GitHub"
        '
        'ucvg_link
        '
        Me.ucvg_link.AutoSize = True
        Me.ucvg_link.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ucvg_link.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ucvg_link.LinkColor = System.Drawing.Color.White
        Me.ucvg_link.Location = New System.Drawing.Point(123, 290)
        Me.ucvg_link.Name = "ucvg_link"
        Me.ucvg_link.Size = New System.Drawing.Size(119, 15)
        Me.ucvg_link.TabIndex = 9
        Me.ucvg_link.TabStop = True
        Me.ucvg_link.Text = "UniConverter GitHub"
        '
        'unipad_link
        '
        Me.unipad_link.AutoSize = True
        Me.unipad_link.Cursor = System.Windows.Forms.Cursors.Hand
        Me.unipad_link.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.unipad_link.LinkColor = System.Drawing.Color.White
        Me.unipad_link.Location = New System.Drawing.Point(206, 313)
        Me.unipad_link.Name = "unipad_link"
        Me.unipad_link.Size = New System.Drawing.Size(76, 15)
        Me.unipad_link.TabIndex = 10
        Me.unipad_link.TabStop = True
        Me.unipad_link.Text = "UniPad Cafe"
        '
        'unipadc_link
        '
        Me.unipadc_link.AutoSize = True
        Me.unipadc_link.Cursor = System.Windows.Forms.Cursors.Hand
        Me.unipadc_link.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.unipadc_link.LinkColor = System.Drawing.Color.White
        Me.unipadc_link.Location = New System.Drawing.Point(130, 313)
        Me.unipadc_link.Name = "unipadc_link"
        Me.unipadc_link.Size = New System.Drawing.Size(72, 15)
        Me.unipadc_link.TabIndex = 11
        Me.unipadc_link.TabStop = true
        Me.unipadc_link.Text = "UniPad Site"
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip1.ToolTipTitle = "Information"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(10,Byte),Integer), CType(CType(10,Byte),Integer), CType(CType(10,Byte),Integer))
        Me.Panel1.Controls.Add(Me.panelVideoPlayer)
        Me.Panel1.Location = New System.Drawing.Point(133, 14)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(140, 140)
        Me.Panel1.TabIndex = 12
        '
        'panelVideoPlayer
        '
        Me.panelVideoPlayer.BackColor = System.Drawing.Color.FromArgb(CType(CType(22,Byte),Integer), CType(CType(30,Byte),Integer), CType(CType(43,Byte),Integer))
        Me.panelVideoPlayer.Location = New System.Drawing.Point(5, 4)
        Me.panelVideoPlayer.Name = "panelVideoPlayer"
        Me.panelVideoPlayer.Size = New System.Drawing.Size(130, 130)
        Me.panelVideoPlayer.TabIndex = 13
        '
        'Timer1
        '
        Me.Timer1.Enabled = true
        '
        'Info
        '
        Me.AcceptButton = Me.OKButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 12!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(22,Byte),Integer), CType(CType(30,Byte),Integer), CType(CType(43,Byte),Integer))
        Me.ClientSize = New System.Drawing.Size(412, 331)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.unipadc_link)
        Me.Controls.Add(Me.unipad_link)
        Me.Controls.Add(Me.ucvg_link)
        Me.Controls.Add(Me.unitor_link)
        Me.Controls.Add(Me.UCV_Alpha)
        Me.Controls.Add(Me.ucv_link)
        Me.Controls.Add(Me.OKButton)
        Me.Controls.Add(Me.ModeE)
        Me.Controls.Add(Me.TipText1)
        Me.Controls.Add(Me.InfoText)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.Name = "Info"
        Me.Text = "Info"
        CType(Me.UCV_Alpha,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel1.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents InfoText As Label
    Friend WithEvents TipText1 As Label
    Friend WithEvents ModeE As TextBox
    Friend WithEvents OKButton As Button
    Friend WithEvents ucv_link As LinkLabel
    Friend WithEvents UCV_Alpha As PictureBox
    Friend WithEvents unitor_link As LinkLabel
    Friend WithEvents ucvg_link As LinkLabel
    Friend WithEvents unipad_link As LinkLabel
    Friend WithEvents unipadc_link As LinkLabel
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Panel1 As Panel
    Friend WithEvents panelVideoPlayer As Panel
    Friend WithEvents Timer1 As Timer
End Class

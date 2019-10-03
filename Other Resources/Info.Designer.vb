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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Info))
        Me.InfoText = New System.Windows.Forms.Label()
        Me.TipText1 = New System.Windows.Forms.Label()
        Me.ModeE = New System.Windows.Forms.TextBox()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.ucv_link = New System.Windows.Forms.LinkLabel()
        Me.UCV_Alpha = New System.Windows.Forms.PictureBox()
        Me.UCV_Icon = New System.Windows.Forms.PictureBox()
        Me.unitor_link = New System.Windows.Forms.LinkLabel()
        Me.ucvg_link = New System.Windows.Forms.LinkLabel()
        Me.unipad_link = New System.Windows.Forms.LinkLabel()
        Me.unipadc_link = New System.Windows.Forms.LinkLabel()
        CType(Me.UCV_Alpha, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UCV_Icon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'InfoText
        '
        Me.InfoText.AutoSize = True
        Me.InfoText.Font = New System.Drawing.Font("나눔바른고딕OTF", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.InfoText.Location = New System.Drawing.Point(300, 305)
        Me.InfoText.Name = "InfoText"
        Me.InfoText.Size = New System.Drawing.Size(45, 22)
        Me.InfoText.TabIndex = 2
        Me.InfoText.Text = "Ver."
        '
        'TipText1
        '
        Me.TipText1.AutoSize = True
        Me.TipText1.Font = New System.Drawing.Font("나눔바른고딕OTF", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TipText1.Location = New System.Drawing.Point(19, 205)
        Me.TipText1.Name = "TipText1"
        Me.TipText1.Size = New System.Drawing.Size(377, 14)
        Me.TipText1.TabIndex = 3
        Me.TipText1.Text = "ⓒ 2018 ~ 2019 MineEric64 (최에릭), Follow_JB All Rights Reserved."
        '
        'ModeE
        '
        Me.ModeE.Font = New System.Drawing.Font("나눔바른고딕", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ModeE.Location = New System.Drawing.Point(77, 222)
        Me.ModeE.Name = "ModeE"
        Me.ModeE.Size = New System.Drawing.Size(169, 21)
        Me.ModeE.TabIndex = 4
        '
        'OKButton
        '
        Me.OKButton.Font = New System.Drawing.Font("나눔고딕", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
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
        Me.ucv_link.Font = New System.Drawing.Font("나눔바른고딕OTF", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ucv_link.Location = New System.Drawing.Point(12, 290)
        Me.ucv_link.Name = "ucv_link"
        Me.ucv_link.Size = New System.Drawing.Size(105, 14)
        Me.ucv_link.TabIndex = 6
        Me.ucv_link.TabStop = True
        Me.ucv_link.Text = "UniConverter Site"
        '
        'UCV_Alpha
        '
        Me.UCV_Alpha.Image = CType(resources.GetObject("UCV_Alpha.Image"), System.Drawing.Image)
        Me.UCV_Alpha.Location = New System.Drawing.Point(121, 160)
        Me.UCV_Alpha.Name = "UCV_Alpha"
        Me.UCV_Alpha.Size = New System.Drawing.Size(163, 42)
        Me.UCV_Alpha.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.UCV_Alpha.TabIndex = 7
        Me.UCV_Alpha.TabStop = False
        '
        'UCV_Icon
        '
        Me.UCV_Icon.Image = Global.UniConverter_Project.My.Resources.Resources.UniConverter_icon
        Me.UCV_Icon.Location = New System.Drawing.Point(134, 26)
        Me.UCV_Icon.Name = "UCV_Icon"
        Me.UCV_Icon.Size = New System.Drawing.Size(131, 130)
        Me.UCV_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.UCV_Icon.TabIndex = 0
        Me.UCV_Icon.TabStop = False
        '
        'unitor_link
        '
        Me.unitor_link.AutoSize = True
        Me.unitor_link.Cursor = System.Windows.Forms.Cursors.Hand
        Me.unitor_link.Font = New System.Drawing.Font("나눔바른고딕OTF", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.unitor_link.Location = New System.Drawing.Point(12, 313)
        Me.unitor_link.Name = "unitor_link"
        Me.unitor_link.Size = New System.Drawing.Size(112, 14)
        Me.unitor_link.TabIndex = 8
        Me.unitor_link.TabStop = True
        Me.unitor_link.Text = "MineEric64 GitHub"
        '
        'ucvg_link
        '
        Me.ucvg_link.AutoSize = True
        Me.ucvg_link.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ucvg_link.Font = New System.Drawing.Font("나눔바른고딕OTF", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.ucvg_link.Location = New System.Drawing.Point(123, 290)
        Me.ucvg_link.Name = "ucvg_link"
        Me.ucvg_link.Size = New System.Drawing.Size(123, 14)
        Me.ucvg_link.TabIndex = 9
        Me.ucvg_link.TabStop = True
        Me.ucvg_link.Text = "UniConverter GitHub"
        '
        'unipad_link
        '
        Me.unipad_link.AutoSize = True
        Me.unipad_link.Cursor = System.Windows.Forms.Cursors.Hand
        Me.unipad_link.Font = New System.Drawing.Font("나눔바른고딕OTF", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.unipad_link.Location = New System.Drawing.Point(206, 313)
        Me.unipad_link.Name = "unipad_link"
        Me.unipad_link.Size = New System.Drawing.Size(75, 14)
        Me.unipad_link.TabIndex = 10
        Me.unipad_link.TabStop = True
        Me.unipad_link.Text = "UniPad Cafe"
        '
        'unipadc_link
        '
        Me.unipadc_link.AutoSize = True
        Me.unipadc_link.Cursor = System.Windows.Forms.Cursors.Hand
        Me.unipadc_link.Font = New System.Drawing.Font("나눔바른고딕OTF", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.unipadc_link.Location = New System.Drawing.Point(130, 313)
        Me.unipadc_link.Name = "unipadc_link"
        Me.unipadc_link.Size = New System.Drawing.Size(70, 14)
        Me.unipadc_link.TabIndex = 11
        Me.unipadc_link.TabStop = True
        Me.unipadc_link.Text = "UniPad Site"
        '
        'Info
        '
        Me.AcceptButton = Me.OKButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(412, 331)
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
        Me.Controls.Add(Me.UCV_Icon)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Info"
        Me.Text = "Info"
        CType(Me.UCV_Alpha, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UCV_Icon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UCV_Icon As PictureBox
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
End Class

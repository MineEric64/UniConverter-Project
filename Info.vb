Imports System.IO
Imports System.Text

Public Class Info

    Private Sub Info_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = MainProject.Text & ": Information"
        InfoText.Text = "Ver " & My.Application.Info.Version.ToString
    End Sub

    Private Sub UCV_Icon_Click(sender As Object, e As EventArgs) Handles UCV_Icon.DoubleClick
        '이스터에그 1
        MessageBox.Show("Unfortunatly, UniConverter Project will be terminated without notice... :(", MainProject.Text & ": Easter Egg", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click
        Dim fs As FileStream
        Dim info As Byte()

        '텍스트박스1 (ModeE) 텍스트가 UniConverterProject_DeveloperMode 일 때
        'BASE64 | UTF-8 | CRLF
        If ModeE.Text = "UniConverterProject_DeveloperMode" Then
            If Dir(MainProject.LicenseFile, vbDirectory) <> "" Then
                MessageBox.Show("Enable Devleoper Mode Failed! Error Code: 9 (I/O Error)", MainProject.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                fs = File.Create(MainProject.LicenseFile)
                info = New UTF8Encoding(True).GetBytes(My.Resources.LicenseText)
                fs.Write(info, 0, info.Length)
                fs.Close()
                If MessageBox.Show("Developer Mode Enabled! Would you like to reboot the UniConverter?", MainProject.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Application.Restart()
                Else
                    Exit Sub
                End If
            End If
                Else
            MessageBox.Show("Incorrect Character.", MainProject.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub ucv_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ucv_link.LinkClicked
        Shell("explorer.exe http://ucv.kro.kr")
    End Sub

    Private Sub unitor_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles unitor_link.LinkClicked
        Shell("explorer.exe http://unitor.ga")
    End Sub
End Class
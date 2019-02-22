Imports System.Net.Mail

Public Class REPORTForm
    Private Sub SenButton_Click(sender As Object, e As EventArgs) Handles SenButton.Click
        Try
            Dim Smtp As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp.UseDefaultCredentials = False
            Smtp.Credentials = New Net.NetworkCredential("unirep1204@gmail.com", "Q$=rS')4*d<Y / Drrn8Zx9Tx9x")
            Smtp.Port = 587
            Smtp.EnableSsl = True
            Smtp.Host = "smtp.gmail.com"
            e_mail = New MailMessage()
            e_mail.From = New MailAddress("unirep1204@gmail.com")
            e_mail.To.Add("besteric40@gmail.com")
            e_mail.Subject = "Report a Bug From UniConverter V" & MainProject.FileInfo.ToString & "."
            e_mail.IsBodyHtml = False 'HTML USING.
            e_mail.Body = DescBox.Text
            If Replace(MAIL_ATTACHMENT.Text, " ", "") <> "" Then
                Dim attachment As Attachment
                'ATTACHMENT SEND
                attachment = New Attachment(MAIL_ATTACHMENT.Text)
                e_mail.Attachments.Add(attachment)
            End If
            Smtp.Send(e_mail)
            MessageBox.Show("Sent! Thank you For reporting bug! :D", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch email_error As Exception
            MessageBox.Show(email_error.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DelButton_Click(sender As Object, e As EventArgs) Handles DelButton.Click
        DescBox.Text = Nothing
    End Sub

    Private Sub UplButton_Click(sender As Object, e As EventArgs) Handles UplButton.Click
        If FileDlg.ShowDialog() = DialogResult.OK Then
            MAIL_ATTACHMENT.Text = FileDlg.FileName
        Else
            MAIL_ATTACHMENT.Text = ""
            Exit Sub
        End If
    End Sub
End Class
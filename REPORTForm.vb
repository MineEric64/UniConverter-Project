Imports System.Net
Imports System.Net.Mail

Public Class REPORTForm
    Private Sub q_() Handles r.Click
        Try

            Dim t As New SmtpClient
            Dim u As New MailMessage()
            t.UseDefaultCredentials = False
            t.Credentials = New NetworkCredential("unirep1204@gmail.com", "Q$=rS')4*d<Y / Drrn8Zx9Tx9x")
            t.Port = 587
            t.EnableSsl = True
            t.Host = "t.gmail.com"
            u = New MailMessage()
            u.From = New MailAddress("unirep1204@gmail.com")
            u.To.Add("besteric40@gmail.com")
            u.Subject = "유니컨버터 v" & MainProject.FileInfo.ToString & " 버전에서 버그 제보가 도착 했습니다."
            u.IsBodyHtml = False
            u.Body = q.Text
            If o.Text.Replace(" ", "") <> "" Then
                Dim m As New Attachment(o.Text)
                u.Attachments.Add(m)
            End If
            t.Send(u)
            MessageBox.Show("Sent! Thx for using it! :D", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch x As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & x.Message & vbNewLine & "Error Message: " & x.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & x.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub s_() Handles s.Click
        q.Text = Nothing
    End Sub

    Private Sub p_() Handles p.Click
        If v.ShowDialog() = DialogResult.OK Then
            o.Text = v.FileName
        Else
            o.Text = ""
            Exit Sub
        End If
    End Sub
End Class
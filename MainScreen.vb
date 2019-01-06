Public Class MainScreen
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ProgressBar1.Value = "100" Then
            Timer1.Enabled = False
        Else
            ProgressBar1.Value = ProgressBar1.Value + 1
        End If
    End Sub

    Private Sub MainScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        VerText.Text = My.Application.Info.Version.ToString
    End Sub
End Class
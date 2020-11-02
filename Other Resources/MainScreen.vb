Public Class MainScreen
    Private Sub MainScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        VerText.Text = My.Application.Info.Version.ToString()
    End Sub
End Class
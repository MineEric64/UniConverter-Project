Public Class DeveloperMode_Main
    Private Sub DM_CW_Click(sender As Object, e As EventArgs) Handles DM_CW.Click
        DeveloperMode_Workspace.Show()
    End Sub

    Private Sub DM_CF_Click(sender As Object, e As EventArgs) Handles DM_CF.Click
        DeveloperMode_Project.Show()
    End Sub

    Private Sub DeveloperMode_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("{0}: Main", MainProject.Text)
    End Sub
End Class
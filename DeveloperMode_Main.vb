Public Class DeveloperMode_Main
    'Developer Mode에서는 Exception 예외 처리 때 GreatEx가 필요 없습니다.
    '어처피 Developer Mode는 불안정한 모드들을 Beta 기능으로 지원해주기 때문에 GreatEx가 필요 없습니다.

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
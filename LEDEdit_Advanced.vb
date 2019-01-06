Public Class LEDEdit_Advanced
    Private Sub Load_Timer_Tick(sender As Object, e As EventArgs) Handles Load_Timer.Tick
        Dim AdvChk_Checked As Boolean

        If keyLED_Edit.AdvChk.Checked = False Then
            Me.Enabled = False
            AdvChk_Checked = False
        End If

        If keyLED_Edit.AdvChk.Checked = True And AdvChk_Checked = False Then
            Me.Enabled = True
            AdvChk_Checked = True
        End If
    End Sub
End Class
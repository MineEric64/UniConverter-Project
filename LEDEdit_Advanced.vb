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

    Private Sub LED_ResetButton_Click(sender As Object, e As EventArgs) Handles LED_ResetButton.Click
        Dim itm As New List(Of String) _
    From {"Note Length", "Delta Time", "Absolute Time"}

        DelayMode2.Items.Clear() 'Clear the Items.

        For Each va In itm
            DelayMode2.Items.Add(va) 'Add the Delay Mode Items.
        Next
        DelayMode2.SelectedIndex = 0 'Reset the Selected Item.

        'Delay Convert #1
        DelayConvert1_1.Checked = True
        DelayConvert1_2.Checked = False
        DelayConvert1_3.Checked = False


    End Sub

    Private Sub DelayMode2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DelayMode2.SelectedIndexChanged
        If DelayMode2.SelectedText = "Absolute Time" Then
            DelayMode5.Enabled = True
        Else
            If DelayMode5.Enabled = True Then DelayMode5.Enabled = False
        End If
    End Sub
End Class
Imports NAudio.Wave

Public Class keyLED_Edit_Advanced
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

        DelayMode1.Items.Clear() 'Clear the Items.

        '---Beta Code---
        '이 리셋 버튼 코드들은 추가 및 변경 및 삭제가 될 때마다 이 코드들을 편집을 해야 합니다.
        '이 주의사항을 꼭 지켜주시고 다 읽어주셨다면 편집할 수 있습니다.

        'Delay Mode #1 / #2
        For Each va In itm
            DelayMode1.Items.Add(va) 'Add the Delay Mode Items.
        Next
        DelayMode1.SelectedIndex = 0 'Reset the Selected Item.

        'Delay Convert #1
        DelayConvert1_1.Checked = True
        DelayConvert1_2.Checked = False

        'Delay Convert #2
        DelayConvert2_1.Checked = True
        DelayConvert2_2.Checked = -False

        'Delay Convert #3
        DelayConvert3_1.Checked = True
        DelayConvert3_2.Checked = False

    End Sub

    Private Sub DelayMode1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DelayMode1.SelectedIndexChanged
        If DelayMode1.SelectedItem.ToString = "Note Length" Then
            DelayMode2.Enabled = True
            DelayMode3.Enabled = False
            DelayMode4.Enabled = False
        ElseIf DelayMode1.SelectedItem.ToString = "Delta Time" Then
            DelayMode2.Enabled = False
            DelayMode3.Enabled = True
            DelayMode4.Enabled = False
        ElseIf DelayMode1.SelectedItem.ToString = "Absolute Time" Then
            DelayMode2.Enabled = False
            DelayMode3.Enabled = False
            DelayMode4.Enabled = True
        End If
    End Sub

    Private Sub LED_SaveButton_Click(sender As Object, e As EventArgs) Handles LED_SaveButton.Click

    End Sub
End Class
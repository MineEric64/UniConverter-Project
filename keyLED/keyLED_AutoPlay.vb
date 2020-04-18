Imports System.Threading
Imports System.Text

Public Class keyLED_AutoPlay
    Private Async Sub GazuaButton_Click(sender As Object, e As EventArgs) Handles GazuaButton.Click
        If AutoPlayText.Enabled = False Then
            AutoPlayText.Enabled = True
        End If

        AutoPlayText.Text = "Please Wait..."
        AutoPlayText.Text = Await keyLED_ToAutoPlayAsync(LEDText.Text)
    End Sub

    Public Shared Function keyLED_ToAutoPlay(text As String) As String
        Dim str As New StringBuilder(255)

        Dim cmpStr As String() = MainProject.SplitbyLine(text)
        Dim newline As Boolean = True

        For i As Integer = 0 To cmpStr.Length - 1
            Dim sp As String() = cmpStr(i).Split(" ")
            Dim x As Integer = 0
            Dim y As Integer = 0

            If sp.Length = 5 AndAlso (sp(0) = "o" OrElse sp(0) = "on") AndAlso Integer.TryParse(sp(1), x) AndAlso Integer.TryParse(sp(2), y) Then 'on 명령어
                str.Append(vbNewLine)
                str.Append("t ")
                str.Append(x)
                str.Append(" ")
                str.Append(y)

                If newline Then
                    str.Replace(vbNewLine, "")
                    newline = False
                End If
            ElseIf sp.Length = 2 AndAlso (sp(0) = "d" OrElse sp(0) = "delay") AndAlso Integer.TryParse(sp(1), y) Then 'delay 명령어
                str.Append(vbNewLine)
                str.Append("d ")
                str.Append(y)

                If newline Then
                    str.Replace(vbNewLine, "")
                    newline = False
                End If
            Else
                Continue For
            End If
        Next

        Return str.ToString()
    End Function

    Public Shared Async Function keyLED_ToAutoPlayAsync(text As String) As Task(Of String)
        Return Await Task.Run(Function() keyLED_ToAutoPlay(text))
    End Function

    Private Sub CopyButton_Click(sender As Object, e As EventArgs) Handles CopyButton.Click
        Clipboard.SetText(AutoPlayText.Text)
        MessageBox.Show("Copied to Clipboard!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
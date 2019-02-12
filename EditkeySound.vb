Imports System.IO
Imports System.Text.RegularExpressions

Public Class EditkeySound
    Private Sub EditkeySound_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If File.Exists(Application.StartupPath + "\Workspace\unipack\keySound") Then
            keySoundTextBox.Text = File.ReadAllText(Application.StartupPath + "\Workspace\unipack\keySound")
        Else
            If Not File.Exists(Application.StartupPath + "Sources\DeveloperMode.uni") Then
                MessageBox.Show("keySound File doesn't exists! (File Path: " & Application.StartupPath + "Workspace\unipack\keySound",
                                Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        Me.Dispose()
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        File.WriteAllText(Application.StartupPath + "\Workspace\unipack\keySound", keySoundTextBox.Text)
        MessageBox.Show("Saved keySound!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' Counting String's Patterns! Showing As Integer.
    ''' </summary>
    Public Function Cntstr(ByVal inputString As String, ByVal pattern As String) As Integer
        Return Regex.Split(inputString, pattern).Length - 1
    End Function
End Class
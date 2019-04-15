Imports System.IO
Imports System.Text

Public Class Info

    Private Sub Info_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = MainProject.Text & ": Information"
        InfoText.Text = "Ver " & My.Application.Info.Version.ToString
    End Sub

    Private Sub UCV_Icon_Click(sender As Object, e As EventArgs) Handles UCV_Icon.DoubleClick
        '이스터에그 1
        MessageBox.Show("Unfortunatly, UniConverter Project will be terminated without notice... :(", MainProject.Text & ": Easter Egg", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click
        Try
            Dim A2UP_File As String = Application.StartupPath & "\A2UP.dll"
            Dim fs As FileStream
            Dim info As Byte()

            '텍스트박스1 (ModeE) 텍스트가 UniConverterProject_DeveloperMode 일 때
            'BASE64 | UTF-8 | CRLF
            If ModeE.Text = "UniConverter-DeveloperMode" Then

                If File.Exists(MainProject.LicenseFile) Then
                    MessageBox.Show("Enabling Devleoper Mode Failed! Error Code: I/O Error (Developer Mode already exists.)", MainProject.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    fs = File.Create(MainProject.LicenseFile)
                    info = New UTF8Encoding(True).GetBytes(My.Resources.LicenseText)
                    fs.Write(info, 0, info.Length)
                    fs.Close()
                    If MessageBox.Show("Developer Mode Enabled! Would you like to reboot the UniConverter?", MainProject.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Application.Restart()
                    Else
                        Exit Sub
                    End If
                End If

            ElseIf ModeE.Text = "UniConverter-Project" Then
                MessageBox.Show(String.Format("UniConverter V{0} - MineEric64 (최에릭)" & vbNewLine & "A2UP V{1} - MineEric64 (최에릭)" & vbNewLine & vbNewLine & "Help By Follow_JB, EX867, Ph-r", My.Application.Info.Version.ToString, GetFileVerInfo(A2UP_File)), Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Not Good :(", MainProject.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Function GetFileVerInfo(ByVal FileName As String) As Version
        Return Version.Parse(FileVersionInfo.GetVersionInfo(FileName).FileVersion)
    End Function

    Private Sub ucv_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ucv_link.LinkClicked
        Try
            Shell("explorer.exe http://ucv.kro.kr")
        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub unitor_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles unitor_link.LinkClicked
        Try
            Shell("explorer.exe http://unitor.ga")
        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Unipad_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles unipad_link.LinkClicked
        Shell("explorer.exe http://cafe.naver.com/unipad")
    End Sub

    Private Sub Unipadc_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles unipadc_link.LinkClicked
        Shell("explorer.exe http://test.unipad.kr")
    End Sub

    Private Sub Ucvg_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ucvg_link.LinkClicked
        Shell("explorer.exe http://github.com/MineEric64/UniConverter-Project")
    End Sub
End Class
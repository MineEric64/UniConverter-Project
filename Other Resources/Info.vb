Imports System.IO
Imports System.Text

Public Class Info

    Private Sub Info_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = MainProject.Text & ": Information"
        InfoText.Text = "Ver " & My.Application.Info.Version.ToString

        Select Case MainProject.lang
            Case Translator.tL.English
                Exit Sub
            Case Translator.tL.Korean
                Text = MainProject.Text & ": 유니컨버터 정보"
                ucv_link.Text = "유니컨버터 사이트"
                ucvg_link.Text = "유니컨버터 오픈소스"
                unitor_link.Text = "최에릭 깃헙 프로필"
                unipadc_link.Left -= 15
                unipadc_link.Text = "유니패드 사이트"
                unipad_link.Text = "유니패드 카페"
                InfoText.Text = My.Application.Info.Version.ToString & " 버전"
        End Select
    End Sub

    Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click
        Try
            Dim A2UPFile As String = Application.StartupPath & "\A2UP.dll"
            Dim fs As FileStream
            Dim info As Byte()

            'BASE64 | UTF-8 | CRLF
            Select Case ModeE.Text

                Case "UniConverter-DeveloperMode"

                    If File.Exists(MainProject.LicenseFile(0)) Then
                        MessageBox.Show("Failed to enable Developer Mode!" & vbNewLine & "I/O Error (Developer Mode already exists.)", MainProject.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        fs = File.Create(MainProject.LicenseFile(0))
                        info = New UTF8Encoding(True).GetBytes(My.Resources.License_DeveloperMode)
                        fs.Write(info, 0, info.Length)
                        fs.Close()
                        If MessageBox.Show("Developer Mode Enabled! Would you like to reboot the UniConverter?", MainProject.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Application.Restart()
                        Else
                            Exit Sub
                        End If
                    End If

                Case "UniConverter-GreatExMode"

                    If File.Exists(MainProject.LicenseFile(1)) Then
                        MessageBox.Show("Failed to enable GreatEx Mode!" & vbNewLine & "I/O Error (GreatEx Mode already exists.)", MainProject.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        fs = File.Create(MainProject.LicenseFile(1))
                        info = New UTF8Encoding(True).GetBytes(My.Resources.License_GreatExMode)
                        fs.Write(info, 0, info.Length)
                        fs.Close()
                        If MessageBox.Show("GreatEx Enabled! Would you like to reboot the UniConverter?", MainProject.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Application.Restart()
                        Else
                            Exit Sub
                        End If
                    End If

                Case "UniConverter-MDSL"
                    Dim DeveloperModebool As String = "(Disabled)"
                    Dim GreatExModebool As String = "(Disabled)"

                    If MainProject.IsDeveloperMode Then
                        DeveloperModebool = "(Enabled)"
                    End If

                    If MainProject.IsGreatExMode Then
                        GreatExModebool = "(Enabled)"
                    End If

                    MessageBox.Show(String.Format("Developer Mode {0}, GreatEx Mode {1}", DeveloperModebool, GreatExModebool), Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Case "UniConverter-Project"
                    MessageBox.Show(String.Format("UniConverter v{0} - MineEric64 (최에릭)" & vbNewLine & "A2UP V{1} - MineEric64 (최에릭)" & vbNewLine & vbNewLine & "Help By Follow_JB, EX867, Ph-r", My.Application.Info.Version.ToString, GetFileVerInfo(A2UPFile)), Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Case Else
                    Select Case MainProject.lang
                        Case Translator.tL.English
                            MessageBox.Show("UniPack Converter for UniPad.", MainProject.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Case Translator.tL.Korean
                            MessageBox.Show("유니패드를 위한 유니팩 컨버터.", MainProject.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Select
            End Select

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Public Shared Function GetFileVerInfo(ByVal FileName As String) As Version
        Return Version.Parse(FileVersionInfo.GetVersionInfo(FileName).FileVersion)
    End Function

    Private Sub ucv_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ucv_link.LinkClicked
        Try

            Shell("explorer.exe http://ucv.kro.kr")

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub unitor_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles unitor_link.LinkClicked
        Try

            Shell("explorer.exe https://github.com/MineEric64")

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub Unipad_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles unipad_link.LinkClicked
        Try

            Shell("explorer.exe http://cafe.naver.com/unipad")

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub Unipadc_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles unipadc_link.LinkClicked
        Try

            Shell("explorer.exe http://test.unipad.kr")

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub Ucvg_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ucvg_link.LinkClicked
        Try

            Shell("explorer.exe http://github.com/MineEric64/UniConverter-Project")

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub
End Class
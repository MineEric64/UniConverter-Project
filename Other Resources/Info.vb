﻿Imports System.IO
Imports System.Text
Imports PVS.MediaPlayer

Public Class Info
    Private ReadOnly _videoPlayer As Player = New Player()
    Private ReadOnly _loadingVideoFile As String = $"{My.Computer.FileSystem.SpecialDirectories.Temp}\UniConverter_New_Loading.mp4"

    Private Sub Info_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = MainProject.Text & ": Information"
        InfoText.Text = "Ver " & My.Application.Info.Version.ToString()

        ToolTip1.SetToolTip(TipText1, "Q. Who made it?" & vbNewLine & "A. This program made by MineEric64 (최에릭), Helped by FollowJB, EX867.")

        
        File.WriteAllBytes(_loadingVideoFile, My.Resources.UniConverter_New_Loading)

        panelVideoPlayer.BackgroundImage = My.Resources.UniConverter_New_Real_Icon
        panelVideoPlayer.BackgroundImageLayout = ImageLayout.Zoom
        _videoPlayer.Display.Window = panelVideoPlayer
        _videoPlayer.Display.Mode = DisplayMode.SizeToFitCenter
        _videoPlayer.Play(_loadingVideoFile)

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
                    MessageBox.Show(String.Format("UniConverter v{0} - MineEric64 (최에릭)" & vbNewLine & "A2UP V{1} - MineEric64 (최에릭)" & vbNewLine & vbNewLine & "Help By Follow_JB, EX867", My.Application.Info.Version.ToString, A2UP.A2U.GetVersion()), Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Case Else
                    Select Case MainProject.lang
                        Case Translator.tL.English
                            MessageBox.Show("UniPack Converter for UniPad." & vbNewLine & My.Application.Info.Copyright, MainProject.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Case Translator.tL.Korean
                            MessageBox.Show("유니패드를 위한 유니팩 컨버터." & vbNewLine & My.Application.Info.Copyright, MainProject.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub panelVideoPlayer_Click(sender As Object, e As EventArgs) Handles panelVideoPlayer.MouseClick
        _videoPlayer.Play(_loadingVideoFile)
    End Sub

    Private Sub Info_FormClosing(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        _videoPlayer.Dispose()
        File.Delete(_loadingVideoFile)
    End Sub
End Class
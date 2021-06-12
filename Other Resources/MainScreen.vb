Imports System.IO
Imports System.Threading
Imports PVS.MediaPlayer

Public Class MainScreen
    Private _videoPlayer As Player = New Player()

    Private Sub MainScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        VerText.Text = My.Application.Info.Version.ToString()

        If Not Player.MFPresent Then
            ChangeIntroToImage()
        Else 'using mediaplayer
            Dim videoPath As String = $"{My.Computer.FileSystem.SpecialDirectories.Temp}\UniConverter_New_Intro.mp4"
            File.WriteAllBytes(videoPath, My.Resources.UniConverter_New_Intro_Video)

            _videoPlayer.Display.Window = videoPanel
            _videoPlayer.Display.Mode = DisplayMode.SizeToFitCenter
            _videoPlayer.Play(videoPath)

            If _videoPlayer.LastError Then
                Debug.WriteLine(_videoPlayer.LastErrorString)
                ChangeIntroToImage()
            End If

            File.Delete(videoPath)
        End If
    End Sub

    Private Sub ChangeIntroToImage()
        videoPanel.BackgroundImage = My.Resources.UniConverter_New_Intro
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If _videoPlayer.Position.Current.TotalSeconds >= 5.0R Then
            _videoPlayer.Pause()
            Timer1.Stop()
        End If
    End Sub

    Public Sub ShowFormSmoothly()
        Me.Opacity = 0.00
        Me.Show()

        For i = 0 To 100
            Me.Opacity = i / 100.0
            Thread.Sleep(3)
        Next
    End Sub

    Public Sub CloseFormSmoothly()
        For i = 100 To 0 Step -1
            Me.Opacity = i / 100.0
            Thread.Sleep(3)
        Next

        Me.Close()
    End Sub
End Class
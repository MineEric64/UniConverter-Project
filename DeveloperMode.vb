Imports System.IO
Imports System.Text

Public Class DeveloperMode
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim curFile As String = "WorkSpace"
        '파일이 존재하는지 검사
        If Dir(curFile, vbDirectory) <> "" Then
            MsgBox("Create WorkSpace Failed! Eror Code: 9")
        Else
            My.Computer.FileSystem.CreateDirectory("WorkSpace")
            MsgBox("Created WorkSpace!", vbOKOnly, "UniConverter")
        End If
        '이래야지 오류가 나지 않음

    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '---같은 형식---
        Dim curFile As String = "WorkSpace\sounds"
        If Dir(curFile, vbDirectory) <> "" Then
            MsgBox("Create sounds Failed! Eror Code: 9", vbOKOnly, "UniConverter")
        Else
            My.Computer.FileSystem.CreateDirectory("WorkSpace\sounds")
            MsgBox("Created sounds!", vbOKOnly, "UniConverter")
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim curFile As String = "WorkSpace\keyLED"
        If Dir(curFile, vbDirectory) <> "" Then
            MsgBox("Create keyLED Failed! Eror Code: 9", vbOKOnly, "UniConverter")
        Else
            My.Computer.FileSystem.CreateDirectory("WorkSpace\keyLED")
            MsgBox("Created keyLED!", vbOKOnly, "UniConverter")
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim curFile As String = "WorkSpace"
        If Dir(curFile, vbDirectory) <> "" Then
            My.Computer.FileSystem.DeleteDirectory("WorkSpace", FileIO.DeleteDirectoryOption.DeleteAllContents)
            MsgBox("Deleted WorkSpace!", vbOKOnly, "UniConverter")
        Else
            MsgBox("Delete WorkSpace Failed! Eror Code: 9", vbOKOnly, "UniConverter")
        End If
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim curFile As String = "WorkSpace\sounds"
        If Dir(curFile, vbDirectory) <> "" Then
            My.Computer.FileSystem.DeleteDirectory("WorkSpace\sounds", FileIO.DeleteDirectoryOption.DeleteAllContents)
            MsgBox("Deleted sounds!", vbOKOnly, "UniConverter")
        Else
            MsgBox("Delete sounds Failed! Eror Code: 9", vbOKOnly, "UniConverter")
        End If
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim curFile As String = "WorkSpace\keyLED"
        If Dir(curFile, vbDirectory) <> "" Then
            My.Computer.FileSystem.DeleteDirectory("WorkSpace\keyLED", FileIO.DeleteDirectoryOption.DeleteAllContents)
            MsgBox("Deleted keyLED !", vbOKOnly, "UniConverter")
        Else
            MsgBox("Delete keyLED Failed! Eror Code: 9", vbOKOnly, "UniConverter")
        End If
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim curFile As String = "Sources"
        If Dir(curFile, vbDirectory) <> "" Then
            My.Computer.FileSystem.DeleteDirectory("Sources", FileIO.DeleteDirectoryOption.DeleteAllContents)
            If MsgBox("Reset All Modes! Do you rebbot the UniConverter?", vbYesNo, "UniConverter") = vbYes Then
                MsgBox("After Reboot, Execute the UniConverter.", vbOKOnly, "UniConverter")
                End
            End If
        Else
            MsgBox("Reset All Modes Failed! Eror Code: 9", vbOKOnly, "UniConverter")
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim curFile As String = "Sources\DeveloperMode.uni"
        If File.Exists(curFile) Then
            Kill("Sources\DeveloperMode.uni")
            If MsgBox("Disabled Developer Mode! Do you reboot the UniConverter?", vbYesNo, "UniConverter") = vbYes Then
                MsgBox("After Reboot, Execute the UniConverter.", vbOKOnly, "UniConverter")
                End
            End If
        Else
            MsgBox("Disable Developer Mode Failed! Eror Code: 9", vbOKOnly, "UniConverter")
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.Close()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Me.Close()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim curFile As String = "WorkSpace\info"
        Dim furFile As String = "WorkSpace"

        If Dir(furFile, vbDirectory) <> "" Then
            If Dir(curFile, vbDirectory) <> "" Then
                MsgBox("Create SInfo Failed! Eror Code: 9", vbOKOnly, "UniConverter")
            Else
                Dim fs As FileStream
                Dim info As Byte()

                '파일 만듬
                fs = File.Create("WorkSpace\info")
                '파일 내용 UTF 8로 인코딩함
                info = New UTF8Encoding(True).GetBytes("title=UniConverter Test" & vbNewLine & "buttonX=8" & vbNewLine & "buttonY=8" & vbNewLine & "producerName=UniConverter, 최에릭" & vbNewLine & "chain=1" & vbNewLine & "squareButton=true")
                fs.Write(info, 0, info.Length)
                fs.Close()
                MsgBox("Created SInfo!", vbOKOnly, "UniConverter")
            End If
        Else

        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim curFile As String = "WorkSpace\info"
        If Dir(curFile, vbDirectory) <> "" Then
            File.Delete("WorkSpace\info")
            MsgBox("Deleted SInfo!", vbOKOnly, "UniConverter")
        Else
            MsgBox("Delete SInfo Failed! Eror Code: 9", vbOKOnly, "UniConverter")
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim curFile As String = "WorkSpace\keySound"
        If Dir(curFile, vbDirectory) <> "" Then
            MsgBox("Create keySound Failed! Eror Code: 9", vbOKOnly, "UniConverter")
        Else
            Dim fs As FileStream
            Dim info As Byte()

            fs = File.Create("WorkSpace\keySound")
            info = New UTF8Encoding(True).GetBytes("")
            fs.Write(info, 0, info.Length)
            fs.Close()
            MsgBox("Created keySound!", vbOKOnly, "UniConverter")
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim curFile As String = "WorkSpace\keySound"
        If Dir(curFile, vbDirectory) <> "" Then
            File.Delete("WorkSpace\keySound")
            MsgBox("Deleted keySound!", vbOKOnly, "UniConverter")
        Else
            MsgBox("Delete keySound Failed! Eror Code: 9", vbOKOnly, "UniConverter")
        End If
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim curFile As String = "Sources\DeveloperMode.uni"
        Dim durFile As String = "Sources"
        Dim fs As FileStream
        Dim info As Byte()

        If Dir(curFile, vbDirectory) <> "" Then
            MsgBox("Enable Developer Mode Failed! Eror Code: 9", vbOKOnly, "UniConverter")
        Else
            If Dir(durFile, vbDirectory) <> "" Then
                fs = File.Create("Sources\DeveloperMode.uni")
                info = New UTF8Encoding(True).GetBytes("//This UniConverter's Developer Mode license." & vbNewLine & vbNewLine & "- UniConverter ⓒ 2018 최에릭 All Rights Reserved.")
                fs.Write(info, 0, info.Length)
                fs.Close()
                If MsgBox("Enabled Developer Mode! Do you reboot the UniConverter?", vbYesNo, "UniConverter") = vbYes Then
                    MsgBox("After Reboot, Execute the UniConverter.")
                Else
                    My.Computer.FileSystem.CreateDirectory("Sources")
                    fs = File.Create("Sources\DeveloperMode.uni")
                    Info = New UTF8Encoding(True).GetBytes("//This UniConverter's Developer Mode license." & vbNewLine & vbNewLine & "- UniConverter ⓒ 2018 최에릭 All Rights Reserved.")
                    fs.Write(Info, 0, Info.Length)
                    fs.Close()
                    If MsgBox("Enabled Developer Mode! Do you reboot the UniConverter?", vbYesNo, "UniConverter") = vbYes Then
                        MsgBox("After Reboot, Execute the UniConverter.")
                        End
                    Else
                    End If
                End If
            End If
        End If
    End Sub
End Class
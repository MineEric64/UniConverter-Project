Imports System.IO
Imports System.Text

Public Class z_Edits
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        File.Delete("WorkSpace\unipack\info")
        MsgBox("info Deleted!", vbInformation, "UniConverter")
    End Sub

    Private Sub infoCSave_Click(sender As Object, e As EventArgs) Handles infoCSave.Click
        Dim curFile As String = "WorkSpace\unipack\info"
        Dim furFile As String = "WorkSpace\unipack"

        If Dir(furFile, vbDirectory) <> "" Then
            If Dir(curFile, vbDirectory) <> "" Then
                File.Delete("WorkSpace\unipack\info")
                Dim fs As FileStream
                Dim info As Byte()

                '파일 만듬
                fs = File.Create("WorkSpace\unipack\info")
                '파일 내용 UTF 8로 인코딩함
                info = New UTF8Encoding(True).GetBytes("title=" & infoTB1.Text & vbNewLine & "buttonX=" & infoTB2.Text & vbNewLine & "buttonY=" & infoTB3.Text & vbNewLine & "producerName=" & infoTB4.Text & vbNewLine & "chain=" & infoTB5.Text & vbNewLine & "squareButton=true")
                fs.Write(info, 0, info.Length)
                fs.Close()
                MsgBox("Saved info!", vbInformation, "UniConverter")
            Else
                Dim fs As FileStream
                Dim info As Byte()

                fs = File.Create("WorkSpace\unipack\info")
                info = New UTF8Encoding(True).GetBytes("title=" & infoTB1.Text & vbNewLine & "buttonX=" & infoTB2.Text & vbNewLine & "buttonY=" & infoTB3.Text & vbNewLine & "producerName=" & infoTB4.Text & vbNewLine & "chain=" & infoTB5.Text & vbNewLine & "squareButton=true")
                fs.Write(info, 0, info.Length)
                fs.Close()
                MsgBox("Saved info!", vbInformation, "UniConverter")
            End If
        Else
            Dim fs As FileStream
            Dim info As Byte()
            My.Computer.FileSystem.CreateDirectory("WorkSpace\unipack")
            fs = File.Create("WorkSpace\unipack\info")
            info = New UTF8Encoding(True).GetBytes("title=" & infoTB1.Text & vbNewLine & "buttonX=" & infoTB2.Text & vbNewLine & "buttonY=" & infoTB3.Text & vbNewLine & "producerName=" & infoTB4.Text & vbNewLine & "chain=" & infoTB5.Text & vbNewLine & "squareButton=true")
            fs.Write(info, 0, info.Length)
            fs.Close()
            MsgBox("Saved info!", vbInformation, "UniConverter")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        My.Computer.Audio.Play(My.Resources.Katalk, AudioPlayMode.Background)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim curFile As String = "WorkSpace\unipack\sounds"
        Dim furFile As String = "WorkSpace\unipack"

        If Dir(furFile, vbDirectory) <> "" Then
            If Dir(curFile, vbDirectory) <> "" Then
                File.Copy("Data\sounds\Katalk.wav", "WorkSpace\unipack\sounds\Katalk.wav", True)
                MsgBox("Put in Exam Sounds in WorkSpace!", vbInformation)
            Else
                My.Computer.FileSystem.CreateDirectory("WorkSpace\sounds")
                File.Copy("Data\sounds\Katalk.wav", "WorkSpace\unipack\sounds\Katalk.wav", True)
                MsgBox("Put in Exam Sounds in WorkSpace!", vbInformation)
            End If
        Else
            My.Computer.FileSystem.CreateDirectory("WorkSpace")
            My.Computer.FileSystem.CreateDirectory("WorkSpace\sounds")
            File.Copy("Data\sounds\Katalk.wav", "WorkSpace\unipack\sounds\Katalk.wav", True)
            MsgBox("Put in Exam Sounds in WorkSpace!", vbInformation)
        End If
    End Sub
End Class
Imports System.IO
Imports System.Text

Public Class Info

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InfoText.Text = "Ver " & My.Application.Info.Version.ToString
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        '이스터에그 1
        MsgBox("Unfortunatly, This Project (UniConverter) will be terminated without notice. 어쩌면, 이 프로젝트 (UniConverter)는 예고 없이 서비스가 종료될 수 있습니다.", , "By EasterEgg :(")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim fs As FileStream
        Dim info As Byte()
        Dim curFile As String = "Sources\DeveloperMode.uni"

        '텍스트박스1 (ModeE) 텍스트가 UniConverterProject_DeveloperMode 일 때
        'BASE64 | UTF-8 | CRLF
        If ModeE.Text = "UniConverterProject_DeveloperMode" Then
            If Dir(curFile, vbDirectory) <> "" Then
                MsgBox("Enable Devleoper Mode Failed! Error Code: 9")
            Else
                'Sources 파일 생성
                My.Computer.FileSystem.CreateDirectory("Sources")
                'Sources 파일 안에 DeveloperMode.uni 생성
                fs = File.Create("Sources\DeveloperMode.uni")
                'DeveloperMode.uni 내용을 적고 UTF-8로 인코딩
                info = New UTF8Encoding(True).GetBytes("RGltIGN1ckZpbGUgQXMgU3RyaW5nID0gIlNvdXJjZXNcRGV2ZWxvcGVyTW9kZS51bmkiDQoNCiAgICAgICAgSWYgRmlsZS5FeGlzdHMoY3VyRmlsZSkgVGhlbg0KICAgICAgICAgICAgRGV2ZWxvcGVyVG9vbFN0cmlwTWVudUl0ZW0uVmlzaWJsZSA9IFRydWUNCiAgICAgICAgICAgIERldmVsb3BlclRvb2xTdHJpcE1lbnVJdGVtLkVuYWJsZWQgPSBUcnVlDQogICAgICAgIEVuZCBJZg0KCQkNCgkJJ0RldmVsb3BlclRvb2xTdHJpcE1lbnVJdGVtDQoJCURldmVsb3Blck1vZGUuU2hvdygpDQoJCQ0KCQknSW5mby52YiAtIE1vZEUNCgkJSWYgTW9kZUUuVGV4dCA9ICJVbmlDb252ZXJ0ZXJQcm9qZWN0X0RldmVsb3Blck1vZGUiIFRoZW4NCiAgICAgICAgICAgIElmIERpcihjdXJGaWxlLCB2YkRpcmVjdG9yeSkgPD4gIiIgVGhlbg0KICAgICAgICAgICAgICAgIE1zZ0JveCgiRW5hYmxlIERldmxlb3BlciBNb2RlIEZhaWxlZCEgRXJyb3IgQ29kZTogOSIpDQogICAgICAgICAgICBFbHNlDQogICAgICAgICAgICAgICAgTXkuQ29tcHV0ZXIuRmlsZVN5c3RlbS5DcmVhdGVEaXJlY3RvcnkoIlNvdXJjZXMiKQ0KICAgICAgICAgICAgICAgIGZzID0gRmlsZS5DcmVhdGUoIlNvdXJjZXNcRGV2ZWxvcGVyTW9kZS51bmkiKQ0KICAgICAgICAgICAgICAgIGluZm8gPSBOZXcgVVRGOEVuY29kaW5nKFRydWUpLkdldEJ5dGVzKCIvL1RoaXMgVW5pQ29udmVydGVyJ3MgRGV2ZWxvcGVyIE1vZGUgbGljZW5zZS4iICYgdmJOZXdMaW5lICYgdmJOZXdMaW5lICYgIi0gVW5pQ29udmVydGVyIOKTkiAyMDE4IOy1nOyXkOumrSBBbGwgUmlnaHRzIFJlc2VydmVkLiIpDQogICAgICAgICAgICAgICAgZnMuV3JpdGUoaW5mbywgMCwgaW5mby5MZW5ndGgpDQogICAgICAgICAgICAgICAgZnMuQ2xvc2UoKQ0KICAgICAgICAgICAgICAgIElmIE1zZ0JveCgiRGV2ZWxvcGVyIE1vZGUgRW5hYmxlZCEgRG8geW91IHJlYm9vdCB0aGUgVW5pQ29udmVydGVyPyIsIHZiWWVzTm8sICJVbmlDb252ZXJ0ZXIiKSA9IHZiWWVzIFRoZW4NCiAgICAgICAgICAgICAgICAgICAgTXNnQm94KCJBZnRlciBSZWJvb3QsIEV4ZWN1dGUgdGhlIFVuaUNvbnZlcnRlci4iKQ0KICAgICAgICAgICAgICAgICAgICBBcHBsaWNhdGlvbi5SZXN0YXJ0KCkNCiAgICAgICAgICAgICAgICBFbHNlDQogICAgICAgICAgICAgICAgRW5kIElmDQogICAgICAgICAgICBFbmQgSWYNCgkJDQoJCQ==
		" & vbNewLine & "//This UniConverter's Developer Mode license." & vbNewLine & "- UniConverter ⓒ 2018 최에릭 All Rights Reserved.")
                fs.Write(info, 0, info.Length)
                fs.Close()
                If MsgBox("Developer Mode Enabled! Do you reboot the UniConverter?", vbYesNo, "UniConverter") = vbYes Then
                    MsgBox("After Reboot, Execute the UniConverter.")
                    Application.Restart()
                Else
                End If
            End If
        Else
            '만약 이스터에그를 넣을꺼라면 여기다가 코드를 짜시오.
            If ModeE.Text = "Follow_JB" Then
                '이스터에그 2
                MsgBox("He's a God of Unitor." & vbNewLine & "He can do Everything. He's a Super Ultra Ultimate Fantastic God :D")
            End If
            If ModeE.Text = "Unitor" Then
                '이스터에그 2
                MsgBox("Godnitor Appeared!!!" & vbNewLine & "Godnitor = Unitor")
            End If
            MsgBox("Incorrect Character :(", vbOKOnly, "UniConverter")
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        '인터넷으로 ucv.kro.kr 을 접속
        Shell("explorer.exe http://ucv.kro.kr")
    End Sub
End Class
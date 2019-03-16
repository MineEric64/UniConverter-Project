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
        Dim fs As FileStream
        Dim info As Byte()

        '텍스트박스1 (ModeE) 텍스트가 UniConverterProject_DeveloperMode 일 때
        'BASE64 | UTF-8 | CRLF
        If ModeE.Text = "UniConverterProject_DeveloperMode" Then
            If Dir(MainProject.LicenseFile, vbDirectory) <> "" Then
                MessageBox.Show("Enable Devleoper Mode Failed! Error Code: 9 (I/O Error)", MainProject.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                fs = File.Create(MainProject.LicenseFile)
                info = New UTF8Encoding(True).GetBytes("RGltIGN1ckZpbGUgQXMgU3RyaW5nID0gIlNvdXJjZXNcRGV2ZWxvcGVyTW9kZS51bmkiDQoNCiAgICAgICAgSWYgRmlsZS5FeGlzdHMoY3VyRmlsZSkgVGhlbg0KICAgICAgICAgICAgRGV2ZWxvcGVyVG9vbFN0cmlwTWVudUl0ZW0uVmlzaWJsZSA9IFRydWUNCiAgICAgICAgICAgIERldmVsb3BlclRvb2xTdHJpcE1lbnVJdGVtLkVuYWJsZWQgPSBUcnVlDQogICAgICAgIEVuZCBJZg0KCQkNCgkJJ0RldmVsb3BlclRvb2xTdHJpcE1lbnVJdGVtDQoJCURldmVsb3Blck1vZGUuU2hvdygpDQoJCQ0KCQknSW5mby52YiAtIE1vZEUNCgkJSWYgTW9kZUUuVGV4dCA9ICJVbmlDb252ZXJ0ZXJQcm9qZWN0X0RldmVsb3Blck1vZGUiIFRoZW4NCiAgICAgICAgICAgIElmIERpcihjdXJGaWxlLCB2YkRpcmVjdG9yeSkgPD4gIiIgVGhlbg0KICAgICAgICAgICAgICAgIE1zZ0JveCgiRW5hYmxlIERldmxlb3BlciBNb2RlIEZhaWxlZCEgRXJyb3IgQ29kZTogOSIpDQogICAgICAgICAgICBFbHNlDQogICAgICAgICAgICAgICAgTXkuQ29tcHV0ZXIuRmlsZVN5c3RlbS5DcmVhdGVEaXJlY3RvcnkoIlNvdXJjZXMiKQ0KICAgICAgICAgICAgICAgIGZzID0gRmlsZS5DcmVhdGUoIlNvdXJjZXNcRGV2ZWxvcGVyTW9kZS51bmkiKQ0KICAgICAgICAgICAgICAgIGluZm8gPSBOZXcgVVRGOEVuY29kaW5nKFRydWUpLkdldEJ5dGVzKCIvL1RoaXMgVW5pQ29udmVydGVyJ3MgRGV2ZWxvcGVyIE1vZGUgbGljZW5zZS4iICYgdmJOZXdMaW5lICYgdmJOZXdMaW5lICYgIi0gVW5pQ29udmVydGVyIOKTkiAyMDE4IOy1nOyXkOumrSBBbGwgUmlnaHRzIFJlc2VydmVkLiIpDQogICAgICAgICAgICAgICAgZnMuV3JpdGUoaW5mbywgMCwgaW5mby5MZW5ndGgpDQogICAgICAgICAgICAgICAgZnMuQ2xvc2UoKQ0KICAgICAgICAgICAgICAgIElmIE1zZ0JveCgiRGV2ZWxvcGVyIE1vZGUgRW5hYmxlZCEgRG8geW91IHJlYm9vdCB0aGUgVW5pQ29udmVydGVyPyIsIHZiWWVzTm8sICJVbmlDb252ZXJ0ZXIiKSA9IHZiWWVzIFRoZW4NCiAgICAgICAgICAgICAgICAgICAgTXNnQm94KCJBZnRlciBSZWJvb3QsIEV4ZWN1dGUgdGhlIFVuaUNvbnZlcnRlci4iKQ0KICAgICAgICAgICAgICAgICAgICBBcHBsaWNhdGlvbi5SZXN0YXJ0KCkNCiAgICAgICAgICAgICAgICBFbHNlDQogICAgICAgICAgICAgICAgRW5kIElmDQogICAgICAgICAgICBFbmQgSWYNCgkJDQoJCQ==
		" & vbNewLine & "//This UniConverter's Developer Mode license." & vbNewLine & "- UniConverter ⓒ 2018 최에릭 All Rights Reserved.")
                fs.Write(info, 0, info.Length)
                fs.Close()
                If MessageBox.Show("Developer Mode Enabled! Would you like to reboot the UniConverter?", MainProject.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) = DialogResult.OK Then
                    Application.Restart()
                End If
            End If
        Else
            MessageBox.Show("Incorrect Character.", MainProject.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub ucv_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ucv_link.LinkClicked
        Shell("explorer.exe http://ucv.kro.kr")
    End Sub

    Private Sub unitor_link_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles unitor_link.LinkClicked
        Shell("explorer.exe http://unitor.ga")
    End Sub
End Class
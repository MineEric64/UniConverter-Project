Public Class z_Tutorial
    Private Sub TrialBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TrialBox1.SelectedIndexChanged
        '에러 코드 9
        If TrialBox1.Text = "[Error Code: 9]" Then
            FixBox1.Text = "Error Code: 9 is The File or Folder already exists or does not exist. So, You should a Create, or Delete The Folder/ File Directly." & vbNewLine & vbNewLine &
                "에러 코드: 9는 그 파일이 이미 존재하거나 없는 오류입니다. 해결방법은, 직접 폴더를 만드시거나, 폴더를 삭제 해야 합니다."
        End If
        '폴더에 사운드를 넣는 기능
        If TrialBox1.Text = "[What is Put Sounds in Folder?]" Then
            FixBox1.Text = "First, You can see (PutSounds) Folder. Then, Put Sounds in (PutSounds) Folder. Then Press File>Open Project>Put Sounds in Folder, UniConverter will Complete Sound Converting." & vbNewLine & vbNewLine &
                "먼저, UniConverter 폴더에 보시면 Put Sounds 라는 폴더가 보이실껍니다. File>Open Project>Put Sounds in Folder 를 누르시면, UniConverter는 사운드를 추가 해줄껍니다."
        End If
        '업데이트 체크 확인 방법
        If TrialBox1.Text = "[How to Check for Updates?]" Then
            FixBox1.Text = "first, Visit ( ucv.kro.kr ). Then, Press the Download Button. Then, You can Download UniConverter for Latest Version." & vbNewLine & vbNewLine &
                "먼저, ( ucv.kro.kr ) 에 들어가셔서 다운로드 버튼을 누르세요. 그러면, UniConverter 최신 버전을 다운로드 하실 수 있습니다."
        End If

        If TrialBox1.Text = "[What is General, and Special?]" Then
            FixBox1.Text = "General is Ableton's Launchpad Setting #1." & vbNewLine & "And, Special is Ableton's Launchpad Setting #2!" & vbNewLine & vbNewLine &
                "General은 에이블톤의 런치패드 설정 #1 입니다." & vbNewLine & "그리고, Special은 에이블톤의 런치패드 설정 #2 입니다!" & vbNewLine & vbNewLine &
                "(사실은 에이블톤 고유 런치패드 버튼 노트숫자가 2개 있는걸로 추정됩니다만, 일반 노트숫자가 General 이고요, 다른 한 개의 노트숫자가 Special입니다.)"
        End If
    End Sub

    Private Sub Help1_Click(sender As Object, e As EventArgs) Handles help1.Click
        Shell("explorer.exe https://goo.gl/forms/5qT6AZ5x4M1JzA9m1")
    End Sub
End Class
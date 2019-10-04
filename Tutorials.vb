Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Threading

Public Class Tutorials
    ''' <summary>
    ''' 답변 문자열().
    ''' </summary>
    Public Answers_ As New List(Of String)

    Private Sub Tutorials_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Select Case MainProject.lang
                Case Translator.tL.Korean
                    Text = "튜토리얼"
            End Select

            If My.Computer.Network.IsAvailable Then

                ThreadPool.QueueUserWorkItem(AddressOf Tutorial_DownloadFile)

            Else
                MessageBox.Show("Connecting Network Failed!" & vbNewLine & "Please check the computer's network.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Public Sub Tutorial_DownloadFile()
        Invoke(Sub()
                   With Loading
                       .Show()
                       .DPr.Style = ProgressBarStyle.Marquee
                       .DPr.MarqueeAnimationSpeed = 10
                       Select Case MainProject.lang
                           Case Translator.tL.English
                               .Text = "Loading Tutorials..."
                               .DLb.Text = "Downloading Tutorials File..."
                           Case Translator.tL.Korean
                               .Text = "튜토리얼 불러오는 중..."
                               .DLb.Text = "튜토리얼 파일 다운로드 하는 중..."
                       End Select
                       .DLb.Left -= 50
                   End With
               End Sub)

        Invoke(Sub() Q_ListView.Enabled = False)
        Try
            Dim a As New WebClient
            a.DownloadFile("http://dtr.ucv.kro.kr", MainProject.TempDirectory & "\UniConverter-Tutorials.uni")
        Catch exN As WebException
            Invoke(Sub()
                       Loading.Dispose()
                       Select Case MainProject.lang
                           Case Translator.tL.English
                               MessageBox.Show("Connecting Network Failed!" & vbNewLine & "Please check the computer's network.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                           Case Translator.tL.Korean
                               MessageBox.Show("네트워크에 연결을 할 수 없습니다!" & vbNewLine & "컴퓨터의 네트워크를 확인해 주세요.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                       End Select
                   End Sub)
            Exit Sub
        End Try

        Invoke(Sub()
                   Loading.Dispose()
                   Q_ListView.Enabled = True
                   Tutorial_ReadMark()
               End Sub)
    End Sub

    Public Sub Tutorial_ReadMark()
        Dim TutorialsDir As String = MainProject.TempDirectory & "\UniConverter-Tutorials.uni"
        Dim tstr As String() = File.ReadAllLines(TutorialsDir, Encoding.Default)
        Dim skipInt As Integer = 0

        For i As Integer = 0 To tstr.Count - 1

            '빈 문자열 이거나 주석인 경우.
            If String.IsNullOrWhiteSpace(tstr(i)) OrElse Mid(tstr(i), 1, 2) = "//" Then
                Continue For
            End If

            Dim a As String() = tstr(i).Split("=")
            Select Case a(0)

                Case "`"
                    If Not MainProject.lang = Translator.GetLnEnum(a(1).Trim) Then
                        skipInt = MainProject.Cntstr(File.ReadAllText(TutorialsDir), ">>")
                    Else
                        skipInt = 0
                    End If

                Case ">"
                    If Not skipInt = 0 Then
                        skipInt -= 1
                        Continue For
                    End If
                    Q_ListView.Items.Add(a(1).Trim())

                Case ">>"
                    If Not skipInt = 0 Then
                        skipInt -= 1
                        Continue For
                    End If
                    Answers_.Add(a(1))

            End Select
        Next
    End Sub

    Private Sub Q_ListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Q_ListView.SelectedIndexChanged
        If Q_ListView.SelectedItems.Count > 0 Then
            Dim a As Integer = Q_ListView.SelectedItems.Item(0).Index
            A_RichTextBox.Text = Answers_.Item(a)
        End If
    End Sub

    Private Sub A_RichTextBox_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles A_RichTextBox.LinkClicked
        Shell("explorer.exe " & e.LinkText)
    End Sub
End Class
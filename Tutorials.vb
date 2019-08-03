Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Threading

Public Class Tutorials
    ''' <summary>
    ''' 언어 정보.
    ''' </summary>
    Public Shared lang As CultureInfo

    ''' <summary>
    ''' 답변 문자열().
    ''' </summary>
    Public Answers_ As String()

    Private Sub Tutorials_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            If My.Computer.Network.IsAvailable Then

                ThreadPool.QueueUserWorkItem(AddressOf Tutorial_DownloadFile)
                Tutorial_ReadMark()

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
        With Loading
            .Show()
            .Text = "Loading Tutorials..."
            .DPr.Style = ProgressBarStyle.Marquee
            .DLb.Text = "Downloading Tutorials File..."
            .DLb.Left -= 50
            .DPr.Refresh()
            .DLb.Refresh()
        End With

        Invoke(Sub() Me.Enabled = False)
        Dim a As New WebClient
        a.DownloadFile("http://dtr.ucv.kro.kr", MainProject.TempDirectory & "\UniConverter-Tutorials.uni")

        Loading.Dispose()
        Invoke(Sub() Me.Enabled = True)
    End Sub

    Public Sub Tutorial_ReadMark()
        Dim TutorialsDir As String = MainProject.TempDirectory & "\UniConverter-Tutorials.uni"
        Dim tstr As String() = File.ReadAllLines(TutorialsDir)
        Dim tr As String = File.ReadAllText(TutorialsDir)
        Dim b As Integer = MainProject.Cntstr(tr, ">>") - 1
        Dim c As Integer = 0

        Answers_ = New String(b) {}


        For i As Integer = 0 To tstr.Count - 1

            '빈 문자열 이거나 주석인 경우.
            If String.IsNullOrWhiteSpace(tstr(i)) OrElse Mid(tstr(i), 1, 2) = "//" Then
                Continue For
            End If

            Dim a As String() = tstr(i).Split("=")

            If a(0) & a(1) = "` Korean" Then
                '한국어 지원은 나중에 언어 기능이 지원될 때...
                Exit Sub
            End If

            Select Case a(0)

                Case "`"
                    '한국어 지원은 나중에 언어 기능이 지원될 때...
                    If a(1) = " English" Then
                        lang = New CultureInfo("en-US")
                    End If

                Case ">"
                    Q_ListView.Items.Add(a(1).Trim())

                Case ">>"
                    Answers_(c) = a(1)
                    c += 1

            End Select

        Next
    End Sub

    Private Sub Q_ListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Q_ListView.SelectedIndexChanged
        If Q_ListView.SelectedItems.Count > 0 Then
            Dim a As Integer = Q_ListView.SelectedItems.Item(0).Index
            A_RichTextBox.Text = Answers_(a)
        End If
    End Sub

    Private Sub A_RichTextBox_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles A_RichTextBox.LinkClicked
        Shell("explorer.exe " & e.LinkText)
    End Sub
End Class
Imports System.IO
Imports System.Text.RegularExpressions

Public Class EditkeySound
    ''' <summary>
    ''' 선택한 체인. (1~8)
    ''' </summary>
    Public UniPack_SelectedChain As Integer = 1
    ''' <summary>
    ''' X 버튼 좌표. (1~8)
    ''' </summary>
    Public UniPack_X As Integer
    ''' <summary>
    ''' Y 버튼 좌표. (1~8)
    ''' </summary>
    Public UniPack_Y As Integer
    ''' <summary>
    ''' 최대 다중 매핑 지원. (1, 같은 사운드 다중 매핑 적용)
    ''' </summary>
    Public keySound_SMax As Integer
    ''' <summary>
    ''' 최대 다중 매핑 지원. (2, 다른 사운드 다중 매핑 적용)
    ''' </summary>
    Public keySound_DifM As Integer

#Region "#Unitor Variables"
    ''' <summary>
    ''' 버튼 저장
    ''' </summary>
    Public Shared ctrl As New Dictionary(Of String, Button)
#End Region

    Private Sub EditkeySound_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            With Me
                .ctrl.Add(11, .uni1_1)
                .ctrl.Add(12, .uni1_2)
                .ctrl.Add(13, .uni1_3)
                .ctrl.Add(14, .uni1_4)
                .ctrl.Add(15, .uni1_5)
                .ctrl.Add(16, .uni1_6)
                .ctrl.Add(17, .uni1_7)
                .ctrl.Add(18, .uni1_8)
                .ctrl.Add(21, .uni2_1)
                .ctrl.Add(22, .uni2_2)
                .ctrl.Add(23, .uni2_3)
                .ctrl.Add(24, .uni2_4)
                .ctrl.Add(25, .uni2_5)
                .ctrl.Add(26, .uni2_6)
                .ctrl.Add(27, .uni2_7)
                .ctrl.Add(28, .uni2_8)
                .ctrl.Add(31, .uni3_1)
                .ctrl.Add(32, .uni3_2)
                .ctrl.Add(33, .uni3_3)
                .ctrl.Add(34, .uni3_4)
                .ctrl.Add(35, .uni3_5)
                .ctrl.Add(36, .uni3_6)
                .ctrl.Add(37, .uni3_7)
                .ctrl.Add(38, .uni3_8)
                .ctrl.Add(41, .uni4_1)
                .ctrl.Add(42, .uni4_2)
                .ctrl.Add(43, .uni4_3)
                .ctrl.Add(44, .uni4_4)
                .ctrl.Add(45, .uni4_5)
                .ctrl.Add(46, .uni4_6)
                .ctrl.Add(47, .uni4_7)
                .ctrl.Add(48, .uni4_8)
                .ctrl.Add(51, .uni5_1)
                .ctrl.Add(52, .uni5_2)
                .ctrl.Add(53, .uni5_3)
                .ctrl.Add(54, .uni5_4)
                .ctrl.Add(55, .uni5_5)
                .ctrl.Add(56, .uni5_6)
                .ctrl.Add(57, .uni5_7)
                .ctrl.Add(58, .uni5_8)
                .ctrl.Add(61, .uni6_1)
                .ctrl.Add(62, .uni6_2)
                .ctrl.Add(63, .uni6_3)
                .ctrl.Add(64, .uni6_4)
                .ctrl.Add(65, .uni6_5)
                .ctrl.Add(66, .uni6_6)
                .ctrl.Add(67, .uni6_7)
                .ctrl.Add(68, .uni6_8)
                .ctrl.Add(71, .uni7_1)
                .ctrl.Add(72, .uni7_2)
                .ctrl.Add(73, .uni7_3)
                .ctrl.Add(74, .uni7_4)
                .ctrl.Add(75, .uni7_5)
                .ctrl.Add(76, .uni7_6)
                .ctrl.Add(77, .uni7_7)
                .ctrl.Add(78, .uni7_8)
                .ctrl.Add(81, .uni8_1)
                .ctrl.Add(82, .uni8_2)
                .ctrl.Add(83, .uni8_3)
                .ctrl.Add(84, .uni8_4)
                .ctrl.Add(85, .uni8_5)
                .ctrl.Add(86, .uni8_6)
                .ctrl.Add(87, .uni8_7)
                .ctrl.Add(88, .uni8_8)
            End With

            If File.Exists(Application.StartupPath + "\Workspace\unipack\keySound") Then
                keySoundTextBox.Text = File.ReadAllText(Application.StartupPath + "\Workspace\unipack\keySound")
            Else
                If Not File.Exists(Application.StartupPath + "Sources\DeveloperMode.uni") Then
                    MessageBox.Show("keySound File doesn't exists! (File Path: " & Application.StartupPath + "Workspace\unipack\keySound",
                                    Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Error! - " & ex.Message & vbNewLine & ex.StackTrace,
        Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        Me.Dispose()
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        File.WriteAllText(Application.StartupPath + "\Workspace\unipack\keySound", keySoundTextBox.Text)
        MessageBox.Show("Saved keySound!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' String의 수를 세줍니다. 결과는 Integer.
    ''' </summary>
    Public Function Cntstr(ByVal inputString As String, ByVal pattern As String) As Integer
        Return Regex.Split(inputString, pattern).Length - 1
    End Function

    Private Sub keySoundLayoutButton_Click(sender As Object, e As EventArgs) Handles keySoundLayoutButton.Click
        Try
            For x As Integer = 1 To 8
                For y As Integer = 1 To 8
                    ctrl(x & y).Text = Nothing
                    ctrl(x & y).BackColor = Color.Gray
                    ctrl(x & y).ForeColor = Color.Black
                Next
            Next

            Dim loi As Integer = 1
            Dim btnText As String = ""
            For Each strLine As String In keySoundTextBox.Text.Split(vbNewLine) 'String을 각 라인마다 자름.
                If loi = 1 Then
                    UniPack_SelectedChain = Mid(strLine, 1, 1)
                    UniPack_X = Mid(strLine, 3, 1)
                    UniPack_Y = Mid(strLine, 5, 1)

                    'ex: 1 1 1 001.wav 1 (같은 사운드 다중 매핑 적용)
                    If Microsoft.VisualBasic.Right(strLine, 1) = "" Then 'String.Empty의 경우
                        keySound_SMax = 1
                    ElseIf Microsoft.VisualBasic.Right(strLine, 1) = "v" Then '001.wav의 경우 (String.Empty의 경우랑 일치)
                        keySound_SMax = 1
                    Else
                        keySound_SMax = Microsoft.VisualBasic.Right(strLine, 1) '반복문이 1 이상의 경우
                    End If

                    'ex: 1 1 1 001.wav, 1 1 1 002.wav (다른 사운드 다중 매핑 적용, 추천)
                    keySound_DifM = Cntstr(keySoundTextBox.Text, UniPack_SelectedChain & " " & UniPack_X & " " & UniPack_Y)

                    loi = 0
                Else
                    UniPack_SelectedChain = Mid(strLine.Remove(0, 1), 1, 1)
                    UniPack_X = Mid(strLine.Remove(0, 1), 3, 1)
                    UniPack_Y = Mid(strLine.Remove(0, 1), 5, 1)
                    If Microsoft.VisualBasic.Right(strLine.Remove(0, 1), 1) = "" Then
                        keySound_SMax = 1
                    ElseIf Microsoft.VisualBasic.Right(strLine.Remove(0, 1), 1) = "v" Then
                        keySound_SMax = 1
                    Else
                        keySound_SMax = Microsoft.VisualBasic.Right(strLine.Remove(0, 1), 1)
                    End If
                    keySound_DifM = Cntstr(keySoundTextBox.Text, UniPack_SelectedChain & " " & UniPack_X & " " & UniPack_Y)
                End If

                If keySound_SMax > 0 Then '기본적인 사운드 매핑 & 다중 매핑.
                    btnText = keySound_SMax
                End If

                If keySound_DifM > 1 Then '사운드 다중 매핑.
                    btnText = CInt(btnText) + keySound_DifM
                End If

                ctrl(UniPack_X & UniPack_Y).BackColor = Color.Green
                ctrl(UniPack_X & UniPack_Y).Text = btnText
            Next

        Catch ex As Exception
            MessageBox.Show("Error! - " & ex.Message & vbNewLine & ex.StackTrace,
        Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MainProject_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Dispose()
    End Sub
End Class
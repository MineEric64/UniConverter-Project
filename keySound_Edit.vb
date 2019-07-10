﻿Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Media

Public Class keySound_Edit
#Region "keySound Variables"
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
    ''' 키사운드 매핑.
    ''' </summary>
    Public keySound_Mapping As Integer
    ''' <summary>
    ''' 최대 다중 매핑 지원. (1, 같은 사운드 다중 매핑 적용)
    ''' </summary>
    Public keySound_SameM As Integer
    ''' <summary>
    ''' 최대 다중 매핑 지원. (2, 다른 사운드 다중 매핑 적용)
    ''' </summary>
    Public keySound_DifM As Integer
    ''' <summary>
    ''' 현재 체인. (1~8)
    ''' </summary>
    Public keySound_CChain As Integer
    ''' <summary>
    ''' 최대 체인. (1~8)
    ''' </summary>
    Public keySound_MChain As Integer 'Old Algorithm #1
    ''' <summary>
    ''' 키사운드 레이아웃 Boolean 여부.
    ''' </summary>
    Dim keySoundLayoutTrue As Boolean
    ''' <summary>
    ''' 키사운드 텍스트 라인. Index: 0부터 시작
    ''' </summary>
    Public keySound_Line As Integer 'Old Algorithm #1
    ''' <summary>
    ''' 이전 체인.
    ''' </summary>
    Public PrChain As Integer
    ''' <summary>
    ''' 체인을 리셋. (keySound Layout 버튼)
    ''' </summary>
    Public ResetChain As Boolean 'Old Algorithm #1
    ''' <summary>
    ''' keySound Loop 여부.
    ''' </summary>
    Dim keySoundLoop As Boolean
#Region "#Unitor Variables"
    ''' <summary>
    ''' 버튼 저장
    ''' </summary>
    Public ctrl As New Dictionary(Of String, Button)
#End Region
#End Region

    Private Sub EditkeySound_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
#Region "Dictionary 버튼 추가"
            With Me
                '버튼 사전 추가
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

                '키사운드 레이아웃 비활성화
                .PadLayoutPanel.Enabled = False
                .btnPad_chain1.Enabled = False
                .btnPad_chain2.Enabled = False
                .btnPad_chain3.Enabled = False
                .btnPad_chain4.Enabled = False
                .btnPad_chain5.Enabled = False
                .btnPad_chain6.Enabled = False
                .btnPad_chain7.Enabled = False
                .btnPad_chain8.Enabled = False
                .keySoundLayoutTrue = False

                .keySound_CChain = 1 '기본으로 선택한 체인.
                .keySound_Line = 0
            End With
#End Region

            Me.Text = MainProject.Text & ": Edit keySound!"

            If File.Exists(Application.StartupPath + "\Workspace\unipack\keySound") Then
                keySoundTextBox.Text = File.ReadAllText(Application.StartupPath + "\Workspace\unipack\keySound")
                If keySoundTextBox.Text.Contains(".mp3") Then
                    If MessageBox.Show("keySound use the mp3 Files! Would you like to replace File Extensions?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.OK Then
                        keySoundTextBox.Text = keySoundTextBox.Text.Replace(".mp3", ".wav")
                    End If
                End If
                Else
                If Not File.Exists(MainProject.LicenseFile(0)) Then
                    MessageBox.Show("keySound File doesn't exists! (File Path: " & Application.StartupPath + "Workspace\unipack\keySound",
                                    Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                keySoundLoop = False
            End If

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton2.Click, CancelButton3.Click
        Me.Dispose()
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Try

            File.WriteAllText(Application.StartupPath + "\Workspace\unipack\keySound", keySoundTextBox.Text)
            MessageBox.Show("Saved keySound!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' String의 수를 세줍니다. 결과는 Integer.
    ''' </summary>
    Public Function Cntstr(ByVal inputString As String, ByVal pattern As String) As Integer
        Return Regex.Split(inputString, pattern).Length - 1
    End Function

    Private Sub keySoundLayoutButton_Click(sender As Object, e As EventArgs) Handles keySoundLayoutButton.Click
        Try
            If keySoundLayoutTrue = False Then SREkeySoundLayout()

            For x As Integer = 1 To 8
                For y As Integer = 1 To 8
                    ctrl(x & y).Text = Nothing
                    ctrl(x & y).BackColor = Color.Gray
                    ctrl(x & y).ForeColor = Color.Black
                Next
            Next

#Region "keySound Opened Project 1"
            '---BETA CODE (Show keySound & Chain)---
            Dim loi As Integer = 1
            Dim btnText As String = ""
            Dim ksTmpTXT As String = Application.StartupPath & "\Workspace\ksTmp.txt"
            If String.IsNullOrWhiteSpace(keySoundTextBox.Text) = False AndAlso String.IsNullOrEmpty(keySoundTextBox.Text) = False Then
                If Not keySoundTextBox.Text = Environment.NewLine Then

                    Dim keySoundText As String
                    keySoundText = keySoundTextBox.Text.TrimStart
                    keySoundText = keySoundTextBox.Text.TrimEnd

                    If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\Workspace") Then
                        File.WriteAllText(ksTmpTXT, keySoundText)
                    Else
                        My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\Workspace")
                        File.WriteAllText(ksTmpTXT, keySoundText)
                    End If

                    For Each strLine As String In File.ReadAllText(ksTmpTXT).Split(Environment.NewLine) 'String을 각 라인마다 자름.
                        If loi = 1 Then
                            If Not String.IsNullOrWhiteSpace(strLine) = False AndAlso String.IsNullOrEmpty(strLine) = False Then
                                If strLine.Contains(Environment.NewLine) = False OrElse strLine = Environment.NewLine Then
                                    Select Case CInt(Mid(strLine, 1, 1))
                                        Case 1 To 8
                                            'Continue.
                                        Case Else
                                            MessageBox.Show("Error! - Chain " & keySound_CChain.ToString & " doesn't exists in keySound. (Ex: Check Failed Chain " & Mid(strLine, 1, 1) & ", Full: " & strLine & ")", Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                    End Select

                                    If CInt(Mid(strLine, 1, 1)) = keySound_CChain Then

                                        UniPack_SelectedChain = keySound_CChain
                                        UniPack_X = CInt(Mid(strLine, 3, 1))
                                        UniPack_Y = CInt(Mid(strLine, 5, 1))

                                        'ex: 1 1 1 001.wav 1 (같은 사운드 다중 매핑 적용)
                                        If Strings.Right(strLine, 4) = ".mp3" Then '.mp3의 경우
                                            keySound_Mapping = 1
                                        ElseIf Strings.Right(strLine, 4) = ".wav" Then '.wav의 경우
                                            keySound_Mapping = 1
                                        Else
                                            keySound_SameM = Strings.Right(strLine, 1) '반복문이 1 이상의 경우
                                        End If

                                        'ex: 1 1 1 001.wav, 1 1 1 002.wav (다른 사운드 다중 매핑 적용, 추천)
                                        keySound_DifM = Cntstr(File.ReadAllText(ksTmpTXT), UniPack_SelectedChain & " " & UniPack_X & " " & UniPack_Y & " ")

                                        loi = 0

                                    Else
                                        loi = 0
                                        Continue For
                                    End If
                                Else
                                    loi = 0
                                    Continue For
                                End If
                            Else
                                loi = 0
                                Continue For
                            End If
                        Else
                            If Not strLine.Remove(0, 1) = "" Then
                                If Not strLine.Remove(0, 1).Contains(vbNewLine) Then
                                    Select Case CInt(Mid(strLine.Remove(0, 1), 1, 1))
                                        Case 1 To 8
                                            'Continue.
                                        Case Else
                                            MessageBox.Show("Error! - Chain " & keySound_CChain.ToString & " doesn't exists in keySound. (Ex: Check Failed Chain " & Mid(strLine.Remove(0, 1), 1, 1) & ", Full: " & strLine.Remove(0, 1) & ")", Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                    End Select

                                    If CInt(Mid(strLine.Remove(0, 1), 1, 1)) = keySound_CChain Then

                                        UniPack_SelectedChain = keySound_CChain
                                        UniPack_X = CInt(Mid(strLine.Remove(0, 1), 3, 1))
                                        UniPack_Y = CInt(Mid(strLine.Remove(0, 1), 5, 1))

                                        'ex: 1 1 1 001.wav 1 (같은 사운드 다중 매핑 적용)
                                        If Strings.Right(strLine.Remove(0, 1), 4) = ".mp3" Then '.mp3의 경우
                                            keySound_Mapping = 1
                                        ElseIf Strings.Right(strLine.Remove(0, 1), 4) = ".wav" Then '.wav의 경우
                                            keySound_Mapping = 1
                                        Else
                                            keySound_SameM = Strings.Right(strLine.Remove(0, 1), 1) '반복문이 1 이상의 경우
                                        End If

                                        'ex: 1 1 1 001.wav, 1 1 1 002.wav (다른 사운드 다중 매핑 적용, 추천)
                                        keySound_DifM = Cntstr(File.ReadAllText(ksTmpTXT), UniPack_SelectedChain & " " & UniPack_X & " " & UniPack_Y & " ")
                                    End If
                                End If
                            End If
                        End If

                        If keySound_Mapping > 0 Then '기본적인 사운드 매핑.
                            btnText = keySound_Mapping
                        End If

                        If keySound_SameM > 0 Then
                            btnText = keySound_SameM
                        End If

                        If keySound_DifM > 1 Then '사운드 다중 매핑.
                            If keySound_SameM > 0 Then
                                btnText = keySound_DifM + keySound_SameM
                            Else
                                btnText = keySound_DifM
                            End If
                        End If

                        ctrl(UniPack_X & UniPack_Y).BackColor = Color.Green
                        ctrl(UniPack_X & UniPack_Y).Text = btnText
                    Next
                End If
            Else
                MessageBox.Show("Error: keySound doesn't exists.", Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
#End Region

#Region "z_keySound Closed Project 1"
            '개발 하다가 Project 취소된 코드 작업.
#Enable Warning BC42356
        Catch OldAlgorithm1 As DivideByZeroException
            keySound_Line = 0

            Dim loi As Integer = 1
            Dim lou As Integer = 0
            Dim btnText As String = ""
            If Not keySoundTextBox.Text = "" Then
                If Not keySoundTextBox.Text = Environment.NewLine Then
                    keySoundTextBox.Text = keySoundTextBox.Text.TrimStart()
                    keySoundTextBox.Text = keySoundTextBox.Text.TrimEnd()

                    If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\Workspace") Then
                        File.WriteAllText(Application.StartupPath & "\Workspace\ksTmp.txt", keySoundTextBox.Text)
                    Else
                        My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\Workspace")
                        File.WriteAllText(Application.StartupPath & "\Workspace\ksTmp.txt", keySoundTextBox.Text)
                    End If

                    If ResetChain = False Then
                        For Each strLine As String In keySoundTextBox.Text.Split(vbNewLine) 'String을 각 라인마다 자름.
                            If loi = 1 Then
                                lou = lou + 1

                                UniPack_SelectedChain = Mid(strLine, 1, 1)
                                UniPack_X = Mid(strLine, 3, 1)
                                UniPack_Y = Mid(strLine, 5, 1)

                                'ex: 1 1 1 001.wav 1 (같은 사운드 다중 매핑 적용)
                                If Strings.Right(strLine, 1) = "" Then 'String.Empty의 경우
                                    keySound_Mapping = 1
                                ElseIf Strings.Right(strLine, 1) = "v" Then '001.wav의 경우 (String.Empty의 경우랑 일치)
                                    keySound_Mapping = 1
                                Else
                                    keySound_SameM = Strings.Right(strLine, 1) '반복문이 1 이상의 경우
                                End If

                                'ex: 1 1 1 001.wav, 1 1 1 002.wav (다른 사운드 다중 매핑 적용, 추천)
                                keySound_DifM = Cntstr(keySoundTextBox.Text, UniPack_SelectedChain & " " & UniPack_X & " " & UniPack_Y & " ")

                                PrChain = Mid(strLine, 1, 1)
                                loi = 0
                            Else
                                If Not strLine.Remove(0, 1) = "" Then
                                    If Not strLine.Remove(0, 1) = vbNewLine Then
                                        If Not Mid(strLine.Remove(0, 1), 1, 1) = PrChain Then '선택한 체인과 이전에 선택한 체인과 다른 경우
                                            keySound_Line = lou
                                            ResetChain = True
                                            Exit For
                                        Else
                                            UniPack_SelectedChain = Mid(strLine.Remove(0, 1), 1, 1)
                                            UniPack_X = Mid(strLine.Remove(0, 1), 3, 1)
                                            UniPack_Y = Mid(strLine.Remove(0, 1), 5, 1)

                                            If Strings.Right(strLine.Remove(0, 1), 1) = "" Then
                                                keySound_Mapping = 1
                                            ElseIf Strings.Right(strLine.Remove(0, 1), 1) = "v" Then
                                                keySound_Mapping = 1
                                            Else
                                                keySound_SameM = Microsoft.VisualBasic.Right(strLine.Remove(0, 1), 1)
                                            End If
                                            keySound_DifM = Cntstr(keySoundTextBox.Text, UniPack_SelectedChain & " " & UniPack_X & " " & UniPack_Y & " ")

                                            PrChain = Mid(strLine.Remove(0, 1), 1, 1)
                                            lou = lou + 1
                                        End If
                                    Else
                                        Continue For
                                    End If
                                End If

                                If keySound_Mapping > 0 Then '기본적인 사운드 매핑.
                                    btnText = keySound_Mapping
                                End If

                                If keySound_SameM > 0 Then
                                    btnText = keySound_SameM
                                End If

                                If keySound_DifM > 1 Then '사운드 다중 매핑.
                                    If keySound_SameM > 0 Then
                                        btnText = keySound_DifM + keySound_SameM
                                    Else
                                        btnText = keySound_DifM
                                    End If
                                End If

                                ctrl(UniPack_X & UniPack_Y).BackColor = Color.Green
                                ctrl(UniPack_X & UniPack_Y).Text = btnText
                            End If
                        Next
                    Else
                        '2체인 이상 keySound 매핑 작업
                        'Index로 불필요한 문자열 제거.

                        If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\Workspace") Then
                            File.WriteAllText(Application.StartupPath & "\Workspace\ksTmp.txt", keySoundTextBox.Text)
                        Else
                            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\Workspace")
                            File.WriteAllText(Application.StartupPath & "\Workspace\ksTmp.txt", keySoundTextBox.Text)
                        End If

                        Dim lop As Integer = 0
                        lou = 0
                        loi = 1
                        For Each Indexstr As String In File.ReadAllText(Application.StartupPath & "\Workspace\ksTmp.txt").Split(Environment.NewLine)
                            If loi = 1 Then
                                If Mid(Indexstr, 1, 1) = keySound_CChain.ToString Then
                                    keySound_Line = lou
                                    lop = 1
                                    Exit For
                                Else
                                    lou = lou + 1
                                    Continue For
                                End If

                                loi = 0
                            Else
                                If Mid(Indexstr.Remove(0, 1), 1, 1) = keySound_CChain.ToString Then
                                    keySound_Line = lou
                                    lop = 1
                                    Exit For
                                Else
                                    lou = lou + 1
                                    Continue For
                                End If
                            End If
                        Next

                        'keySound에 해당 Chain이 없음
                        If lop = 0 Then
                            MessageBox.Show("There is no Chain " & keySound_CChain.ToString & " in keySound!", Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            Dim TheFileLines As New List(Of String)
                            For i As Integer = 0 To keySound_Line - 1
                                TheFileLines.AddRange(File.ReadAllLines(Application.StartupPath & "\Workspace\ksTmp.txt"))
                                If i >= TheFileLines.Count Then Exit Sub
                                TheFileLines.RemoveAt(0)
                            Next
                            File.WriteAllLines(Application.StartupPath & "\Workspace\ksTmp.txt", TheFileLines.ToArray)
                        End If

                        keySound_Line = 0 'keySound 라인 초기화.

                        loi = 1
                        For Each Linestr As String In File.ReadAllText(Application.StartupPath & "\Workspace\ksTmp.txt").Split(Environment.NewLine)
                            'Index 찾기를 이용하여 체인을 찾는 방법.

                            If loi = 1 Then
                                If Not Linestr = "" Then
                                    If Not Linestr = vbNewLine Then
                                        lou = lou + 1

                                        If Mid(Linestr, 1, 1) Then
                                            UniPack_SelectedChain = Mid(Linestr, 1, 1)
                                            UniPack_X = Mid(Linestr, 3, 1)
                                            UniPack_Y = Mid(Linestr, 5, 1)

                                            If Strings.Right(Linestr, 1) = "" Then
                                                keySound_Mapping = 1
                                            ElseIf Strings.Right(Linestr, 1) = "v" Then
                                                keySound_Mapping = 1
                                            Else
                                                keySound_SameM = CInt(Strings.Right(Linestr, 1))
                                            End If

                                            keySound_DifM = Cntstr(File.ReadAllText(Application.StartupPath & "\Workspace\ksTmp.txt"), UniPack_SelectedChain & " " & UniPack_X & " " & UniPack_Y & " ")
                                        Else
                                            Continue For
                                        End If

                                        If Not Mid(Linestr, 1, 1) = PrChain.ToString Then '선택한 체인과 이전에 선택한 체인과 다른 경우
                                            keySound_Line = lou
                                            Exit For
                                        Else
                                            loi = 0
                                            PrChain = Mid(Linestr, 1, 1)
                                        End If
                                    Else
                                        Continue For
                                    End If
                                Else
                                    Continue For
                                End If
                            Else
                                If Not Linestr.Remove(0, 1) = "" Then
                                    If Not Linestr.Remove(0, 1) = vbNewLine Then
                                        UniPack_SelectedChain = CInt(Mid(Linestr.Remove(0, 1), 1, 1))
                                        UniPack_X = CInt(Mid(Linestr.Remove(0, 1), 3, 1))
                                        UniPack_Y = CInt(Mid(Linestr.Remove(0, 1), 5, 1))

                                        If Strings.Right(Linestr.Remove(0, 1), 1) = "" Then
                                            keySound_Mapping = 1
                                        ElseIf Strings.Right(Linestr.Remove(0, 1), 1) = "v" Then
                                            keySound_Mapping = 1
                                        Else
                                            keySound_SameM = CInt(Strings.Right(Linestr.Remove(0, 1), 1))
                                        End If

                                        keySound_DifM = Cntstr(File.ReadAllText(Application.StartupPath & "\Workspace\ksTmp.txt"), UniPack_SelectedChain & " " & UniPack_X & " " & UniPack_Y & " ")
                                        PrChain = CInt(Mid(Linestr.Remove(0, 1), 1, 1))
                                        lou = lou + 1
                                    Else
                                        Continue For
                                    End If

                                    If Not Mid(Linestr.Remove(0, 1), 1, 1) = PrChain Then '선택한 체인과 이전에 선택한 체인과 다른 경우
                                        keySound_Line = lou
                                        Exit For
                                    Else
                                        If keySound_Mapping > 0 Then '기본적인 사운드 매핑.
                                            btnText = keySound_Mapping
                                        End If

                                        If keySound_SameM > 0 Then
                                            btnText = keySound_SameM
                                        End If

                                        If keySound_DifM > 1 Then '사운드 다중 매핑.
                                            If keySound_SameM > 0 Then
                                                btnText = keySound_DifM + keySound_SameM
                                            Else
                                                btnText = keySound_DifM
                                            End If
                                        End If

                                        ctrl(UniPack_X & UniPack_Y).BackColor = Color.Green
                                        ctrl(UniPack_X & UniPack_Y).Text = btnText
                                    End If
                                End If
                            End If
                        Next
                    End If
                Else
                    MessageBox.Show("Error! - String is Empty!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Error! - String is Empty!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
#Disable Warning
#End Region

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub keySound_ChainChanged(sender As Object, e As EventArgs) Handles btnPad_chain1.Click, btnPad_chain2.Click, btnPad_chain3.Click, btnPad_chain4.Click, btnPad_chain5.Click, btnPad_chain6.Click, btnPad_chain7.Click, btnPad_chain8.Click
        '선택한 체인 선언.
        Dim ClickedBtn As Button = CType(sender, Button)
        keySound_CChain = CInt(ClickedBtn.Name.Substring(12, 1))

        '선택한 체인으로 레이아웃 새로고침.
        If keySoundLayoutTrue = True Then
            keySoundLayoutButton_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub SREkeySoundLayout()
        PadLayoutPanel.Enabled = True
        btnPad_chain1.Enabled = True
        btnPad_chain2.Enabled = True
        btnPad_chain3.Enabled = True
        btnPad_chain4.Enabled = True
        btnPad_chain5.Enabled = True
        btnPad_chain6.Enabled = True
        btnPad_chain7.Enabled = True
        btnPad_chain8.Enabled = True
        keySoundLayoutTrue = True
    End Sub

    Private Sub UniPadButtons_Click(sender As Object, e As EventArgs) Handles uni1_1.MouseDown, uni1_2.MouseDown, uni1_3.MouseDown, uni1_4.MouseDown, uni1_5.MouseDown, uni1_6.MouseDown, uni1_7.MouseDown, uni1_8.MouseDown, uni2_1.MouseDown, uni2_2.MouseDown, uni2_3.MouseDown, uni2_4.MouseDown, uni2_5.MouseDown, uni2_6.MouseDown, uni2_7.MouseDown, uni2_8.MouseDown, uni3_1.MouseDown, uni3_2.MouseDown, uni3_3.MouseDown, uni3_4.MouseDown, uni3_5.MouseDown, uni3_6.MouseDown, uni3_7.MouseDown, uni3_8.MouseDown, uni4_1.MouseDown, uni4_2.MouseDown, uni4_3.MouseDown, uni4_4.MouseDown, uni4_5.MouseDown, uni4_6.MouseDown, uni4_7.MouseDown, uni4_8.MouseDown, uni5_1.MouseDown, uni5_2.MouseDown, uni5_3.MouseDown, uni5_4.MouseDown, uni5_5.MouseDown, uni5_6.MouseDown, uni5_7.MouseDown, uni5_8.MouseDown, uni6_1.MouseDown, uni6_2.MouseDown, uni6_3.MouseDown, uni6_4.MouseDown, uni6_5.MouseDown, uni6_6.MouseDown, uni6_7.MouseDown, uni6_8.MouseDown, uni7_1.MouseDown, uni7_2.MouseDown, uni7_3.MouseDown, uni7_4.MouseDown, uni7_5.MouseDown, uni7_6.MouseDown, uni7_7.MouseDown, uni7_8.MouseDown, uni8_1.MouseDown, uni8_2.MouseDown, uni8_3.MouseDown, uni8_4.MouseDown, uni8_5.MouseDown, uni8_6.MouseDown, uni8_7.MouseDown, uni8_8.MouseDown

        Dim ClickedBtn As Button = CType(sender, Button)
        Dim x As Integer = ClickedBtn.Name.Substring(3, 1)
        Dim y As Integer = ClickedBtn.Name.Substring(5, 1)
        Dim WavFile As String
        Dim ksTmpTXT As String = Application.StartupPath & "\Workspace\ksTmp.txt"
        Dim keySoundText As String
        keySoundText = keySoundTextBox.Text.TrimStart
        keySoundText = keySoundTextBox.Text.TrimEnd
        File.WriteAllText(ksTmpTXT, keySoundText)

        Dim loi As Integer = 1
        For Each strLine As String In File.ReadAllText(Application.StartupPath & "\Workspace\ksTmp.txt").Split(Environment.NewLine)
            If loi = 1 Then
                If strLine.Contains(keySound_CChain & " " & x & " " & y & " ") Then
                    For Each WavFileName As String In strLine.Split(" ") 'Split(" ")의 경우에는 loi가 필요 없음.
                        If WavFileName.Contains(".wav") Then
                            WavFile = WavFileName
                            Dim WavFie As New SoundPlayer(Application.StartupPath & "\Workspace\unipack\sounds\" & WavFile)
                            WavFie.Load()
                            WavFie.Play()
                            If File.Exists(Application.StartupPath & "\Workspace\unipack\sounds\" & WavFile) Then
                                If Strings.Right(strLine, 4) = ".wav" Then
                                    'My.Computer.Audio.Play(Application.StartupPath & "\Workspace\unipack\sounds\" & WavFile, AudioPlayMode.Background)

                                    If keySoundLoop = True Then keySoundLoop = False
                                ElseIf Strings.Right(strLine, 1) = "1" Then
                                    'My.Computer.Audio.Play(Application.StartupPath & "\Workspace\unipack\sounds\" & WavFile, AudioPlayMode.Background)

                                    If keySoundLoop = True Then keySoundLoop = False
                                ElseIf Strings.Right(strLine, 1) = "0" Then
                                    'My.Computer.Audio.Play(Application.StartupPath & "\Workspace\unipack\sounds\" & WavFile, AudioPlayMode.BackgroundLoop)

                                    keySoundLoop = True
                                Else
                                    For i As Integer = 1 To CInt(Strings.Right(strLine, 1))
                                        My.Computer.Audio.Play(Application.StartupPath & "\Workspace\unipack\sounds\" & WavFile, AudioPlayMode.WaitToComplete)
                                        If keySoundLoop = True Then keySoundLoop = False
                                    Next
                                End If
                            End If
                        End If
                    Next
                End If

                loi = 0
            Else
                If strLine.Remove(0, 1).Contains(keySound_CChain & " " & x & " " & y & " ") Then
                    For Each WavFileName As String In strLine.Remove(0, 1).Split(" ")
                        If WavFileName.Contains(".wav") Then
                            WavFile = WavFileName
                            If File.Exists(Application.StartupPath & "\Workspace\unipack\sounds\" & WavFile) Then
                                If Strings.Right(strLine.Remove(0, 1), 4) = ".wav" Then
                                    My.Computer.Audio.Play(Application.StartupPath & "\Workspace\unipack\sounds\" & WavFile, AudioPlayMode.Background)
                                    If keySoundLoop = True Then keySoundLoop = False
                                ElseIf Strings.Right(strLine.Remove(0, 1), 1) = "1" Then
                                    My.Computer.Audio.Play(Application.StartupPath & "\Workspace\unipack\sounds\" & WavFile, AudioPlayMode.Background)
                                    If keySoundLoop = True Then keySoundLoop = False
                                ElseIf Strings.Right(strLine.Remove(0, 1), 1) = "0" Then
                                    My.Computer.Audio.Play(Application.StartupPath & "\Workspace\unipack\sounds\" & WavFile, AudioPlayMode.BackgroundLoop)
                                    keySoundLoop = True
                                Else
                                    For i As Integer = 1 To CInt(Strings.Right(strLine, 1))
                                        My.Computer.Audio.Play(Application.StartupPath & "\Workspace\unipack\sounds\" & WavFile, AudioPlayMode.WaitToComplete)
                                        If keySoundLoop = True Then keySoundLoop = False
                                    Next
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Next

    End Sub

    Private Sub UniPadButtons_Loop0(sender As Object, e As EventArgs) Handles uni1_1.MouseUp, uni1_2.MouseUp, uni1_3.MouseUp, uni1_4.MouseUp, uni1_5.MouseUp, uni1_6.MouseUp, uni1_7.MouseUp, uni1_8.MouseUp, uni2_1.MouseUp, uni2_2.MouseUp, uni2_3.MouseUp, uni2_4.MouseUp, uni2_5.MouseUp, uni2_6.MouseUp, uni2_7.MouseUp, uni2_8.MouseUp, uni3_1.MouseUp, uni3_2.MouseUp, uni3_3.MouseUp, uni3_4.MouseUp, uni3_5.MouseUp, uni3_6.MouseUp, uni3_7.MouseUp, uni3_8.MouseUp, uni4_1.MouseUp, uni4_2.MouseUp, uni4_3.MouseUp, uni4_4.MouseUp, uni4_5.MouseUp, uni4_6.MouseUp, uni4_7.MouseUp, uni4_8.MouseUp, uni5_1.MouseUp, uni5_2.MouseUp, uni5_3.MouseUp, uni5_4.MouseUp, uni5_5.MouseUp, uni5_6.MouseUp, uni5_7.MouseUp, uni5_8.MouseUp, uni6_1.MouseUp, uni6_2.MouseUp, uni6_3.MouseUp, uni6_4.MouseUp, uni6_5.MouseUp, uni6_6.MouseUp, uni6_7.MouseUp, uni6_8.MouseUp, uni7_1.MouseUp, uni7_2.MouseUp, uni7_3.MouseUp, uni7_4.MouseUp, uni7_5.MouseUp, uni7_6.MouseUp, uni7_7.MouseUp, uni7_8.MouseUp, uni8_1.MouseUp, uni8_2.MouseUp, uni8_3.MouseUp, uni8_4.MouseUp, uni8_5.MouseUp, uni8_6.MouseUp, uni8_7.MouseUp, uni8_8.MouseUp
        If keySoundLoop = True Then
            My.Computer.Audio.Stop()
        End If
    End Sub
End Class
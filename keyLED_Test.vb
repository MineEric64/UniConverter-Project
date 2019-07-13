﻿Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Threading
Imports A2UP

Public Class keyLED_Test

    Public LEDTexts As String = String.Empty
    Public IsLoaded As Boolean = False
    ''' <summary>
    ''' 버튼 저장
    ''' </summary>
    Public ctrl As New Dictionary(Of String, Button)
    Public led As New ledReturn

    Private Sub keyLED_Test_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If keyLED_Edit.Enabled = False Then
            Me.Enabled = False
        End If
#Region "Dictionary 버튼 추가"
        '버튼 사전 추가

        'UniPad Buttons
        ctrl.Add(11, u11)
        ctrl.Add(12, u12)
        ctrl.Add(13, u13)
        ctrl.Add(14, u14)
        ctrl.Add(15, u15)
        ctrl.Add(16, u16)
        ctrl.Add(17, u17)
        ctrl.Add(18, u18)
        ctrl.Add(21, u21)
        ctrl.Add(22, u22)
        ctrl.Add(23, u23)
        ctrl.Add(24, u24)
        ctrl.Add(25, u25)
        ctrl.Add(26, u26)
        ctrl.Add(27, u27)
        ctrl.Add(28, u28)
        ctrl.Add(31, u31)
        ctrl.Add(32, u32)
        ctrl.Add(33, u33)
        ctrl.Add(34, u34)
        ctrl.Add(35, u35)
        ctrl.Add(36, u36)
        ctrl.Add(37, u37)
        ctrl.Add(38, u38)
        ctrl.Add(41, u41)
        ctrl.Add(42, u42)
        ctrl.Add(43, u43)
        ctrl.Add(44, u44)
        ctrl.Add(45, u45)
        ctrl.Add(46, u46)
        ctrl.Add(47, u47)
        ctrl.Add(48, u48)
        ctrl.Add(51, u51)
        ctrl.Add(52, u52)
        ctrl.Add(53, u53)
        ctrl.Add(54, u54)
        ctrl.Add(55, u55)
        ctrl.Add(56, u56)
        ctrl.Add(57, u57)
        ctrl.Add(58, u58)
        ctrl.Add(61, u61)
        ctrl.Add(62, u62)
        ctrl.Add(63, u63)
        ctrl.Add(64, u64)
        ctrl.Add(65, u65)
        ctrl.Add(66, u66)
        ctrl.Add(67, u67)
        ctrl.Add(68, u68)
        ctrl.Add(71, u71)
        ctrl.Add(72, u72)
        ctrl.Add(73, u73)
        ctrl.Add(74, u74)
        ctrl.Add(75, u75)
        ctrl.Add(76, u76)
        ctrl.Add(77, u77)
        ctrl.Add(78, u78)
        ctrl.Add(81, u81)
        ctrl.Add(82, u82)
        ctrl.Add(83, u83)
        ctrl.Add(84, u84)
        ctrl.Add(85, u85)
        ctrl.Add(86, u86)
        ctrl.Add(87, u87)
        ctrl.Add(88, u88)

        'UniPad MC Buttons
        ctrl.Add("mc1", mc1)
        ctrl.Add("mc2", mc2)
        ctrl.Add("mc3", mc3)
        ctrl.Add("mc4", mc4)
        ctrl.Add("mc5", mc5)
        ctrl.Add("mc6", mc6)
        ctrl.Add("mc7", mc7)
        ctrl.Add("mc8", mc8)
        ctrl.Add("mc9", mc9)
        ctrl.Add("mc10", mc10)
        ctrl.Add("mc11", mc11)
        ctrl.Add("mc12", mc12)
        ctrl.Add("mc13", mc13)
        ctrl.Add("mc14", mc14)
        ctrl.Add("mc15", mc15)
        ctrl.Add("mc16", mc16)
        ctrl.Add("mc17", mc17)
        ctrl.Add("mc18", mc18)
        ctrl.Add("mc19", mc19)
        ctrl.Add("mc20", mc20)
        ctrl.Add("mc21", mc21)
        ctrl.Add("mc22", mc22)
        ctrl.Add("mc23", mc23)
        ctrl.Add("mc24", mc24)
        ctrl.Add("mc25", mc25)
        ctrl.Add("mc26", mc26)
        ctrl.Add("mc27", mc27)
        ctrl.Add("mc28", mc28)
        ctrl.Add("mc29", mc29)
        ctrl.Add("mc30", mc30)
        ctrl.Add("mc31", mc31)
        ctrl.Add("mc32", mc32)
#End Region

        For i = 1 To 32
            Dim circle As New GraphicsPath
            circle.AddEllipse(New Rectangle(0, 0, ctrl("mc" & i).Size.Width, ctrl("mc" & i).Size.Height))
            ctrl("mc" & i).Region = New Region(circle)
            ctrl("mc" & i).ForeColor = Color.Gray
        Next
    End Sub

    Public Sub LoadkeyLEDText(LEDText As String)
        LEDTexts = LEDText
        IsLoaded = True
    End Sub

    Structure Pad_VTouch_Parm
        Public chain As Integer
        Public x As String
        Public y As Integer
        Public touch_mode As Integer
    End Structure

    Private Function CreateNew_Pad_VTouch_Parm(chain As Integer, x As String, y As Integer, touch_mode As Integer) As Pad_VTouch_Parm
        Dim return_v As New Pad_VTouch_Parm
        return_v.chain = chain
        return_v.x = x
        return_v.y = y
        return_v.touch_mode = touch_mode
        Return return_v
    End Function

    Private Sub TestByt_Click(sender As Object, e As EventArgs) Handles TestByt.Click
        For x As Integer = 1 To 8
            For y As Integer = 1 To 8
                ctrl(x & y).BackColor = Color.Gray
            Next
        Next

        File.WriteAllText(Application.StartupPath & "\Workspace\TmpLED.txt", LEDTexts)
        ThreadPool.QueueUserWorkItem(AddressOf LEDHandler)
    End Sub

    Private Sub LEDHandler()
        Try
            Dim linesInfo As New List(Of String)(File.ReadAllLines(Application.StartupPath & "\Workspace\TmpLED.txt"))
            linesInfo.RemoveAll(Function(s) s.Trim = "")
            Dim Lines() As String = linesInfo.ToArray
            Dim linescounter As Integer = Lines.Length
            Dim stopwatch__1 As Stopwatch = Stopwatch.StartNew()

            Dim sp() As String
            For i = 0 To linescounter - 1

                sp = Lines(i).Split(" ")
                If sp(0) = "o" OrElse sp(0) = "on" Then

                    'Velocity Code.
                    If sp(3) = "a" OrElse sp(3) = "auto" Then
                        sp(3) = sp(4)
                        If IsNumeric(sp(3)) = False Then
                            MessageBox.Show("Wrong UniPad button code on line " & i + 1 & "!", "Wrong on command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End If
                        sp(3) = led.returnLED(sp(3))
                    End If

                    'On Code.
                    If sp(1) = "mc" Then
                        Try
                            ctrl("mc" & sp(2)).BackColor = ColorTranslator.FromHtml("#" & sp(3))
                        Catch ex As Exception
                            MessageBox.Show("Wrong UniPad button code mc line " & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong on command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End Try
                    End If

                    Try
                        ctrl(sp(1) & sp(2)).BackColor = ColorTranslator.FromHtml("#" & sp(3))
                    Catch
                        MessageBox.Show("Wrong UniPad button code on line " & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong on command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit For
                    End Try

                ElseIf sp(0) = "f" OrElse sp(0) = "off" Then

                    'Off Code.
                    If sp(1) = "mc" Then
                        Try
                            ctrl("mc" & sp(2)).BackColor = Color.Gray
                        Catch ex As Exception
                            MessageBox.Show("Wrong UniPad button code mc line " & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong on command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End Try
                    End If

                    Try
                        ctrl(sp(1) & sp(2)).BackColor = Color.Gray
                    Catch
                        MessageBox.Show("Wrong UniPad button code on line" & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong off command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit For
                    End Try

                ElseIf sp(0) = "d" OrElse sp(0) = "delay" Then
                    'Delay Code.
                    If IsNumeric(sp(1)) = False Then
                        MessageBox.Show("Wrong millisecond code on line " & i + 1 & "!", "Wrong delay command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit For
                    End If
                    Thread.Sleep(sp(1))

                Else
                    MessageBox.Show("Wrong LED command " & Lines(i) & " on line" & i, "Wrong Data", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit For
                End If
            Next
            'ledfiles_now(chain, xcode, ycode) += 1
            stopwatch__1.[Stop]()
            Invoke(Sub()
                       TimeLabel.Text = "Test Time: " & stopwatch__1.ElapsedMilliseconds & "ms"
                   End Sub)
        Catch

        End Try
    End Sub
End Class
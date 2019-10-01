Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Threading
Imports A2UP
Imports NAudio.Midi

Public Class keyLED_Test

    Public Shared LEDTexts As String = String.Empty
    Public IsLoaded As Boolean = False
    Public IsLaunchpaded As Boolean = False
    Public lkind As Integer = 0
    Public lmo As MidiOut

    ''' <summary>
    ''' 버튼 저장
    ''' </summary>
    Public ctrl As New Dictionary(Of String, Button)
    Public led As New ledReturn

    Public Sub New()

        ' 디자이너에서 이 호출이 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하세요.

    End Sub

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

        For i = 1 To 32 'Width - 2, Height - 1 빼주는 이유는 빼주지 않으면 LED가 어긋나게 실행이 되기 때문이다.
            Dim circle As New GraphicsPath
            circle.AddEllipse(New Rectangle(0, 0, ctrl("mc" & i).Size.Width - 2, ctrl("mc" & i).Size.Height - 1))
            ctrl("mc" & i).Region = New Region(circle)
            ctrl("mc" & i).ForeColor = Color.Gray
        Next

        If MainProject.midioutput_avail Then
            lkind = MainProject.midioutput_kind
            lmo = MainProject.midioutput
            lmo.Reset()
            lmo.SendBuffer({240, 0, 32, 41, 9, 60, 85, 110, 105, 116, 111, 114, 32, 118, Asc(My.Application.Info.Version.Major), 46, Asc(My.Application.Info.Version.Minor), 46, Asc(My.Application.Info.Version.Build), 46, Asc(My.Application.Info.Version.Revision), 247})
            IsLaunchpaded = True
        End If
    End Sub

    Public Sub LoadkeyLEDText(LEDText As String)
        LEDTexts = LEDText
        IsLoaded = True
    End Sub

    Private Sub Me_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.T Then 'Test Mode [Ctrl + T]
            TestByt.PerformClick()
        End If
    End Sub

    Private Sub TestByt_Click(sender As Object, e As EventArgs) Handles TestByt.Click
        For x As Integer = 1 To 8
            For y As Integer = 1 To 8
                ctrl(x & y).BackColor = Color.Gray
            Next
        Next

        Try
            File.WriteAllText(Application.StartupPath & "\Workspace\TmpLED.txt", LEDTexts)
        Catch exN As IOException
            Thread.Sleep(300)
        End Try
        ThreadPool.QueueUserWorkItem(AddressOf LEDHandler)

        If MainProject.midioutput_avail Then
            If IsLaunchpaded = False Then
                lkind = MainProject.midioutput_kind
                lmo = MainProject.midioutput
                lmo.Reset()
                lmo.SendBuffer({240, 0, 32, 41, 9, 60, 85, 110, 105, 116, 111, 114, 32, 118, Asc(My.Application.Info.Version.Major), 46, Asc(My.Application.Info.Version.Minor), 46, Asc(My.Application.Info.Version.Build), 46, Asc(My.Application.Info.Version.Revision), 247})
                IsLaunchpaded = True
            End If

            LEDHandler_Launchpad() 'Handle the LED to Launchpad.
            End If
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
                    Else

                        Try
                            ctrl(sp(1) & sp(2)).BackColor = ColorTranslator.FromHtml("#" & sp(3))
                        Catch
                            MessageBox.Show("Wrong UniPad button code on line " & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong on command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End Try
                    End If

                ElseIf sp(0) = "f" OrElse sp(0) = "off" Then

                    'Off Code.
                    If sp(1) = "mc" Then
                        Try
                            ctrl("mc" & sp(2)).BackColor = Color.Gray
                        Catch ex As Exception
                            MessageBox.Show("Wrong UniPad button code mc line " & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong on command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End Try
                    Else

                        Try
                            ctrl(sp(1) & sp(2)).BackColor = Color.Gray
                        Catch
                            MessageBox.Show("Wrong UniPad button code on line" & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong off command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End Try
                    End If

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

    Public Sub LEDHandler_Launchpad()
        If MainProject.midioutput_avail Then
            '패드를 밀고 시작
            For x = 1 To 8
                For y = 1 To 8
                    Try
                        Select Case MainProject.midioutput_kind
                            Case 0
                                MainProject.midioutput.SendBuffer({144, ((x - 1) * 16) + y - 1, 0})
                            Case 1
                                MainProject.midioutput.SendBuffer({144, 10 * (9 - x) + y, 0})
                            Case Else
                                MainProject.midioutput.SendBuffer({144, 10 * (9 - x) + y, 0})
                        End Select
                    Catch
                        MainProject.DisconnectMidi()
                    End Try
                Next
            Next
            For i = 1 To 32
                mcSendNote(i, 0, Nothing, True)
            Next

            ThreadPool.QueueUserWorkItem(AddressOf LEDEdit_PrepareToQueue)
        End If
    End Sub

    Private Sub LEDEdit_PrepareToQueue()
        Try
            Dim nsto As New Stopwatch
            nsto.Start()

            Dim linesInfo As New List(Of String)(File.ReadAllLines(Application.StartupPath & "\Workspace\TmpLED.txt"))
            linesInfo.RemoveAll(Function(s) s.Trim = "")
            Dim Lines() As String = linesInfo.ToArray
            Dim linescounter As Integer = Lines.Length

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
                    End If

                    'On Code.
                    If sp(1) = "mc" Then
                        Try
                            mcSendNote(sp(2), sp(3), Nothing, lkind, True)
                        Catch ex As Exception
                            MessageBox.Show("Wrong UniPad button code mc line " & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong on command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End Try
                    Else

                        Try
                            Handle_Launchpad(sp(1), sp(2), sp(3), lkind)
                        Catch
                            MessageBox.Show("Wrong UniPad button code on line " & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong on command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End Try

                    End If

                ElseIf sp(0) = "f" OrElse sp(0) = "off" Then

                    'Off Code.
                    If sp(1) = "mc" Then
                        Try
                            mcSendNote(sp(2), 0, Nothing, lkind, True)
                        Catch ex As Exception
                            MessageBox.Show("Wrong UniPad button code mc line " & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong on command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End Try
                    Else

                        Try
                            Handle_Launchpad(sp(1), sp(2), 0, lkind)
                        Catch
                            MessageBox.Show("Wrong UniPad button code on line" & i + 1 & "! Or maybe you didn't pointed the button code!", "Wrong off command", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit For
                        End Try
                    End If

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

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 런치패드를 조작합니다. (LED ON!)
    ''' </summary>
    ''' <param name="x">LED X 좌표.</param>
    ''' <param name="y">LED Y 좌표.</param>
    ''' <param name="Velocity">LED 색깔.</param>
    Public Sub Handle_Launchpad(x As Integer, y As Integer, Velocity As Integer)
        Try

            If MainProject.midioutput_avail Then
                '예외 잡아주기.
                If x < 1 OrElse x > 8 Then Throw New ArgumentException("x는 항상 1~8 사이여야 합니다.")
                If y < 1 OrElse y > 8 Then Throw New ArgumentException("y는 항상 1~8 사이여야 합니다.")
                If Velocity < 0 OrElse Velocity > 127 Then Throw New ArgumentException("Velocity는 항상 0~127 사이여야 합니다. (데이터 형식: Byte)")

                If MainProject.midioutput_kind = 0 Then
                    MainProject.midioutput.SendBuffer({144, ((x - 1) * 16) + y - 1, Velocity})
                ElseIf MainProject.midioutput_kind = 1 Then
                    MainProject.midioutput.SendBuffer({144, 10 * (9 - x) + y, Velocity})
                Else
                    MainProject.midioutput.SendBuffer({144, 10 * (9 - x) + y, Velocity})
                End If

            Else
                Throw New ArgumentNullException("MIDI Devices 연결을 확인 해주세요.")
            End If

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub
#Region "More Handles!"
    ''' <summary>
    ''' 런치패드를 조작합니다. (LED ON!)
    ''' </summary>
    ''' <param name="x">LED X 좌표.</param>
    ''' <param name="y">LED Y 좌표.</param>
    ''' <param name="Velocity">LED 색깔.</param>
    ''' <param name="Launchpad_Kind">런치패드 종류. (0: Mini/S, 1: MK2, 2: Pro)</param>
    Public Sub Handle_Launchpad(x As Integer, y As Integer, Velocity As Integer, Launchpad_Kind As Integer)
        Try

            '예외 잡아주기.
            If x < 1 OrElse x > 8 Then Throw New ArgumentException("x는 항상 1~8 사이여야 합니다.")
            If y < 1 OrElse y > 8 Then Throw New ArgumentException("y는 항상 1~8 사이여야 합니다.")
            If Velocity < 0 OrElse Velocity > 127 Then Throw New ArgumentException("Velocity는 항상 0~127 사이여야 합니다. (데이터 형식: Byte)")

            If Launchpad_Kind = 0 Then
                lmo.SendBuffer({144, ((x - 1) * 16) + y - 1, Velocity})
            ElseIf Launchpad_Kind = 1 Then
                lmo.SendBuffer({144, 10 * (9 - x) + y, Velocity})
            Else
                lmo.SendBuffer({144, 10 * (9 - x) + y, Velocity})
            End If

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub
#End Region

    ''' <summary>
    ''' MC 버튼을 켭니다.
    ''' </summary>
    ''' <param name="num">MC 버튼의 코드를 뜻합니다. 1~32로 있습니다.</param>
    ''' <param name="velo">벨로시티를 말합니다.</param>
    ''' <param name="col">색을 말합니다.</param>
    Public Sub mcSendNote(ByVal num As Integer, ByVal velo As Integer, ByVal col As Color, ByVal ModeL As Boolean)
        If ModeL = False Then
            If Not num = 0 Then
                MainProject.UI(Sub()
                                   ctrl("mc" & num).ForeColor = col
                                   ctrl("mc" & num).BackColor = col
                               End Sub)
            End If
        End If

        If MainProject.midioutput_avail = True Then

            Try
                If MainProject.midioutput_kind = 0 Then
                    Select Case num

                        Case 1
                            MainProject.midioutput.SendBuffer({176, 104, velo})
                        Case 2
                            MainProject.midioutput.SendBuffer({176, 105, velo})
                        Case 3
                            MainProject.midioutput.SendBuffer({176, 106, velo})
                        Case 4
                            MainProject.midioutput.SendBuffer({176, 107, velo})
                        Case 5
                            MainProject.midioutput.SendBuffer({176, 108, velo})
                        Case 6
                            MainProject.midioutput.SendBuffer({176, 109, velo})
                        Case 7
                            MainProject.midioutput.SendBuffer({176, 110, velo})
                        Case 8
                            MainProject.midioutput.SendBuffer({176, 111, velo})
                        Case 9
                            MainProject.midioutput.SendBuffer({144, 8, velo})
                        Case 10
                            MainProject.midioutput.SendBuffer({144, 24, velo})
                        Case 11
                            MainProject.midioutput.SendBuffer({144, 40, velo})
                        Case 12
                            MainProject.midioutput.SendBuffer({144, 56, velo})
                        Case 13
                            MainProject.midioutput.SendBuffer({144, 72, velo})
                        Case 14
                            MainProject.midioutput.SendBuffer({144, 88, velo})
                        Case 15
                            MainProject.midioutput.SendBuffer({144, 104, velo})
                        Case 16
                            MainProject.midioutput.SendBuffer({144, 120, velo})
                    End Select
                ElseIf MainProject.midioutput_kind = 1 Then
                    Select Case num
                        Case 1
                            MainProject.midioutput.SendBuffer({176, 104, velo})
                        Case 2
                            MainProject.midioutput.SendBuffer({176, 105, velo})
                        Case 3
                            MainProject.midioutput.SendBuffer({176, 106, velo})
                        Case 4
                            MainProject.midioutput.SendBuffer({176, 107, velo})
                        Case 5
                            MainProject.midioutput.SendBuffer({176, 108, velo})
                        Case 6
                            MainProject.midioutput.SendBuffer({176, 109, velo})
                        Case 7
                            MainProject.midioutput.SendBuffer({176, 110, velo})
                        Case 8
                            MainProject.midioutput.SendBuffer({176, 111, velo})
                        Case 9
                            MainProject.midioutput.SendBuffer({144, 89, velo})
                        Case 10
                            MainProject.midioutput.SendBuffer({144, 79, velo})
                        Case 11
                            MainProject.midioutput.SendBuffer({144, 69, velo})
                        Case 12
                            MainProject.midioutput.SendBuffer({144, 59, velo})
                        Case 13
                            MainProject.midioutput.SendBuffer({144, 49, velo})
                        Case 14
                            MainProject.midioutput.SendBuffer({144, 39, velo})
                        Case 15
                            MainProject.midioutput.SendBuffer({144, 29, velo})
                        Case 16
                            MainProject.midioutput.SendBuffer({144, 19, velo})
                    End Select
                Else
                    Select Case num
                        Case 0
                            MainProject.midioutput.SendBuffer({240, 0, 32, 41, 2, 16, 10, 99, velo, 247})
                        Case 1
                            MainProject.midioutput.SendBuffer({176, 91, velo})
                        Case 2
                            MainProject.midioutput.SendBuffer({176, 92, velo})
                        Case 3
                            MainProject.midioutput.SendBuffer({176, 93, velo})
                        Case 4
                            MainProject.midioutput.SendBuffer({176, 94, velo})
                        Case 5
                            MainProject.midioutput.SendBuffer({176, 95, velo})
                        Case 6
                            MainProject.midioutput.SendBuffer({176, 96, velo})
                        Case 7
                            MainProject.midioutput.SendBuffer({176, 97, velo})
                        Case 8
                            MainProject.midioutput.SendBuffer({176, 98, velo})

                            '오류 발생시 여기를 176 (cc)로 바꿔야함.
                        Case 9
                            MainProject.midioutput.SendBuffer({144, 89, velo})
                        Case 10
                            MainProject.midioutput.SendBuffer({144, 79, velo})
                        Case 11
                            MainProject.midioutput.SendBuffer({144, 69, velo})
                        Case 12
                            MainProject.midioutput.SendBuffer({144, 59, velo})
                        Case 13
                            MainProject.midioutput.SendBuffer({144, 49, velo})
                        Case 14
                            MainProject.midioutput.SendBuffer({144, 39, velo})
                        Case 15
                            MainProject.midioutput.SendBuffer({144, 29, velo})
                        Case 16
                            MainProject.midioutput.SendBuffer({144, 19, velo})
                        Case 17
                            MainProject.midioutput.SendBuffer({176, 8, velo})
                        Case 18
                            MainProject.midioutput.SendBuffer({176, 7, velo})
                        Case 19
                            MainProject.midioutput.SendBuffer({176, 6, velo})
                        Case 20
                            MainProject.midioutput.SendBuffer({176, 5, velo})
                        Case 21
                            MainProject.midioutput.SendBuffer({176, 4, velo})
                        Case 22
                            MainProject.midioutput.SendBuffer({176, 3, velo})
                        Case 23
                            MainProject.midioutput.SendBuffer({176, 2, velo})
                        Case 24
                            MainProject.midioutput.SendBuffer({176, 1, velo})
                        Case 25
                            MainProject.midioutput.SendBuffer({176, 10, velo})
                        Case 26
                            MainProject.midioutput.SendBuffer({176, 20, velo})
                        Case 27
                            MainProject.midioutput.SendBuffer({176, 30, velo})
                        Case 28
                            MainProject.midioutput.SendBuffer({176, 40, velo})
                        Case 29
                            MainProject.midioutput.SendBuffer({176, 50, velo})
                        Case 30
                            MainProject.midioutput.SendBuffer({176, 60, velo})
                        Case 31
                            MainProject.midioutput.SendBuffer({176, 70, velo})
                        Case 32
                            MainProject.midioutput.SendBuffer({176, 80, velo})
                    End Select
                End If
            Catch
                MainProject.midioutput_avail = False
            End Try
        End If
    End Sub
#Region "More Handles!"
    ''' <summary>
    ''' MC 버튼을 켭니다.
    ''' </summary>
    ''' <param name="num">MC 버튼의 코드를 뜻합니다. 1~32로 있습니다.</param>
    ''' <param name="velo">벨로시티를 말합니다.</param>
    ''' <param name="col">색을 말합니다.</param>
    ''' <param name="Launchpad_Kind">런치패드 종류. (0: Mini/S, 1: MK2, 2: Pro)</param>
    Public Sub mcSendNote(ByVal num As Integer, ByVal velo As Integer, ByVal col As Color, Launchpad_Kind As Integer, ByVal ModeL As Boolean)
        If ModeL = False Then
            If Not num = 0 Then
                MainProject.UI(Sub()
                                   ctrl("mc" & num).ForeColor = col
                                   ctrl("mc" & num).BackColor = col
                               End Sub)
            End If
        End If

        Try
            If Launchpad_Kind = 0 Then
                Select Case num

                    Case 1
                        lmo.SendBuffer({176, 104, velo})
                    Case 2
                        lmo.SendBuffer({176, 105, velo})
                    Case 3
                        lmo.SendBuffer({176, 106, velo})
                    Case 4
                        lmo.SendBuffer({176, 107, velo})
                    Case 5
                        lmo.SendBuffer({176, 108, velo})
                    Case 6
                        lmo.SendBuffer({176, 109, velo})
                    Case 7
                        lmo.SendBuffer({176, 110, velo})
                    Case 8
                        lmo.SendBuffer({176, 111, velo})
                    Case 9
                        lmo.SendBuffer({144, 8, velo})
                    Case 10
                        lmo.SendBuffer({144, 24, velo})
                    Case 11
                        lmo.SendBuffer({144, 40, velo})
                    Case 12
                        lmo.SendBuffer({144, 56, velo})
                    Case 13
                        lmo.SendBuffer({144, 72, velo})
                    Case 14
                        lmo.SendBuffer({144, 88, velo})
                    Case 15
                        lmo.SendBuffer({144, 104, velo})
                    Case 16
                        lmo.SendBuffer({144, 120, velo})
                End Select
            ElseIf Launchpad_Kind = 1 Then
                Select Case num
                    Case 1
                        lmo.SendBuffer({176, 104, velo})
                    Case 2
                        lmo.SendBuffer({176, 105, velo})
                    Case 3
                        lmo.SendBuffer({176, 106, velo})
                    Case 4
                        lmo.SendBuffer({176, 107, velo})
                    Case 5
                        lmo.SendBuffer({176, 108, velo})
                    Case 6
                        lmo.SendBuffer({176, 109, velo})
                    Case 7
                        lmo.SendBuffer({176, 110, velo})
                    Case 8
                        lmo.SendBuffer({176, 111, velo})
                    Case 9
                        lmo.SendBuffer({144, 89, velo})
                    Case 10
                        lmo.SendBuffer({144, 79, velo})
                    Case 11
                        lmo.SendBuffer({144, 69, velo})
                    Case 12
                        lmo.SendBuffer({144, 59, velo})
                    Case 13
                        lmo.SendBuffer({144, 49, velo})
                    Case 14
                        lmo.SendBuffer({144, 39, velo})
                    Case 15
                        lmo.SendBuffer({144, 29, velo})
                    Case 16
                        lmo.SendBuffer({144, 19, velo})
                End Select
            Else
                Select Case num
                    Case 0
                        lmo.SendBuffer({240, 0, 32, 41, 2, 16, 10, 99, velo, 247})
                    Case 1
                        lmo.SendBuffer({176, 91, velo})
                    Case 2
                        lmo.SendBuffer({176, 92, velo})
                    Case 3
                        lmo.SendBuffer({176, 93, velo})
                    Case 4
                        lmo.SendBuffer({176, 94, velo})
                    Case 5
                        lmo.SendBuffer({176, 95, velo})
                    Case 6
                        lmo.SendBuffer({176, 96, velo})
                    Case 7
                        lmo.SendBuffer({176, 97, velo})
                    Case 8
                        lmo.SendBuffer({176, 98, velo})

                            '오류 발생시 여기를 176 (cc)로 바꿔야함.
                    Case 9
                        lmo.SendBuffer({144, 89, velo})
                    Case 10
                        lmo.SendBuffer({144, 79, velo})
                    Case 11
                        lmo.SendBuffer({144, 69, velo})
                    Case 12
                        lmo.SendBuffer({144, 59, velo})
                    Case 13
                        lmo.SendBuffer({144, 49, velo})
                    Case 14
                        lmo.SendBuffer({144, 39, velo})
                    Case 15
                        lmo.SendBuffer({144, 29, velo})
                    Case 16
                        lmo.SendBuffer({144, 19, velo})
                    Case 17
                        lmo.SendBuffer({176, 8, velo})
                    Case 18
                        lmo.SendBuffer({176, 7, velo})
                    Case 19
                        lmo.SendBuffer({176, 6, velo})
                    Case 20
                        lmo.SendBuffer({176, 5, velo})
                    Case 21
                        lmo.SendBuffer({176, 4, velo})
                    Case 22
                        lmo.SendBuffer({176, 3, velo})
                    Case 23
                        lmo.SendBuffer({176, 2, velo})
                    Case 24
                        lmo.SendBuffer({176, 1, velo})
                    Case 25
                        lmo.SendBuffer({176, 10, velo})
                    Case 26
                        lmo.SendBuffer({176, 20, velo})
                    Case 27
                        lmo.SendBuffer({176, 30, velo})
                    Case 28
                        lmo.SendBuffer({176, 40, velo})
                    Case 29
                        lmo.SendBuffer({176, 50, velo})
                    Case 30
                        lmo.SendBuffer({176, 60, velo})
                    Case 31
                        lmo.SendBuffer({176, 70, velo})
                    Case 32
                        lmo.SendBuffer({176, 80, velo})
                End Select
            End If
        Catch
            MainProject.midioutput_avail = False
        End Try
    End Sub
#End Region
End Class
Public Class keyLED_Edit_Ex
    Dim openedproj As Boolean = False
    Dim projectPath As String = String.Empty

    Public Shared IsAutoLoaded As Boolean = False
    Public FlipIndex As Integer

    Private Sub KeyLED_Edit_Ex_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainProject.abl_openedproj Then
            openedproj = True
            projectPath = MainProject.abl_Name
        End If

        Select Case MainProject.lang
            Case Translator.tL.Korean
                Me.Text = "keyLED 편집 - LED 확장 기능"
                OpenAblPrjBtn.Text = "에이블톤 프로젝트 열기"
                TabControl.TabPages(0).Text = "[기본 플러그인]"
                TabControl.TabPages(1).Text = "플립"
                TabControl.TabPages(2).Text = "색깔 플러그인"

                FlipGroupBox.Text = "LED 플러그인"
                Flip_ResetButton.Text = "초기화"
                Flip_AutoLoadButton.Text = "에이블톤 프로젝트에서 자동 불러오기 (베타)"
                Flip_MirrorCheckBox.Text = "거울 모드"
                Flip_RotateCheckBox.Text = "회전"
                Flip_DuplicateCheckBox.Text = "복제"
        End Select
    End Sub

    Public Function GetFlipStructure() As FlipStructure
        Dim FlipStructure_ As New FlipStructure(Mirror.None, 0, False)

        If Flip_MirrorCheckBox.Checked Then
            If Flip_MirrorComboBox.Text = "Horizontal" Then
                FlipStructure_.Mirror = Mirror.Horizontal
            ElseIf Flip_MirrorComboBox.Text = "Vertical" Then
                FlipStructure_.Mirror = Mirror.Vertical
            End If
        End If

        If Flip_RotateCheckBox.Checked Then
            FlipStructure_.Rotate = Integer.Parse(Flip_RotateComboBox.Text.Replace("°", ""))
        End If

        FlipStructure_.Duplicate = Flip_DuplicateCheckBox.Checked

        Return FlipStructure_
    End Function

    Public Structure FlipStructure
        Public Mirror As Mirror
        Public Rotate As Integer
        Public Duplicate As Boolean

        Public Sub New(ByVal MirrorMode As Mirror, ByVal RotateAngle As Integer, ByVal Duplicate_ As Boolean)
            Mirror = MirrorMode
            Rotate = RotateAngle
            Duplicate = Duplicate_
        End Sub
    End Structure

    Public Enum Mirror
        Horizontal
        Vertical
        None
    End Enum

    Private Sub FlipExtensions_Changed(sender As Object, e As EventArgs) Handles Flip_MirrorCheckBox.CheckedChanged, Flip_MirrorComboBox.SelectedIndexChanged, Flip_RotateCheckBox.CheckedChanged, Flip_RotateComboBox.SelectedIndexChanged, Flip_DuplicateCheckBox.CheckedChanged
        If keyLED_Edit.GAZUA_.IsBusy Then
            Exit Sub '오류!
        End If

        If IsAutoLoaded = False Then
            keyLED_Edit.Ex_Flip.Clear()
            keyLED_Edit.Ex_Flip.Add(GetFlipStructure()) '나중에 FlipIndex로 바꿔야 함.
        End If
    End Sub

    Private Sub Flip_ResetButton_Click(sender As Object, e As EventArgs) Handles Flip_ResetButton.Click
        Flip_MirrorCheckBox.Checked = False
        Flip_MirrorComboBox.SelectedIndex = 0
        Flip_RotateCheckBox.Checked = False
        Flip_RotateComboBox.SelectedIndex = 0
        Flip_DuplicateCheckBox.Checked = False
    End Sub

    Private Sub Flip_AutoLoadButton_Click(sender As Object, e As EventArgs) Handles Flip_AutoLoadButton.Click
        'Do Something.
        IsAutoLoaded = True
    End Sub

#Region "Flip Functions" '유니터 / OrientationHelper에서 코드를 가져옴.
    Public Structure Flip_Rotate_XYReturn
        Dim x As Integer
        Dim y As Integer
    End Structure

    Public Function Flip_Mirror_Horizontal_MC(ByVal source As Integer) As Integer
        Select Case source
            Case 1
                Return 8
            Case 2
                Return 7
            Case 3
                Return 6
            Case 4
                Return 5
            Case 5
                Return 4
            Case 6
                Return 3
            Case 7
                Return 2
            Case 8
                Return 1
            Case 9
                Return 32
            Case 10
                Return 31
            Case 11
                Return 30
            Case 12
                Return 29
            Case 13
                Return 28
            Case 14
                Return 27
            Case 15
                Return 26
            Case 16
                Return 25
            Case 17
                Return 24
            Case 18
                Return 23
            Case 19
                Return 22
            Case 20
                Return 21
            Case 21
                Return 20
            Case 22
                Return 19
            Case 23
                Return 18
            Case 24
                Return 17
            Case 25
                Return 16
            Case 26
                Return 15
            Case 27
                Return 14
            Case 28
                Return 13
            Case 29
                Return 12
            Case 30
                Return 11
            Case 31
                Return 10
            Case 32
                Return 9
            Case Else
                Return 0
        End Select
    End Function

    Public Function Flip_Mirror_Vertical_MC(ByVal source As Integer) As Integer
        Select Case source
            Case 1
                Return 24
            Case 2
                Return 23
            Case 3
                Return 22
            Case 4
                Return 21
            Case 5
                Return 20
            Case 6
                Return 19
            Case 7
                Return 18
            Case 8
                Return 17
            Case 9
                Return 16
            Case 10
                Return 15
            Case 11
                Return 14
            Case 12
                Return 13
            Case 13
                Return 12
            Case 14
                Return 11
            Case 15
                Return 10
            Case 16
                Return 9
            Case 17
                Return 8
            Case 18
                Return 7
            Case 19
                Return 6
            Case 20
                Return 5
            Case 21
                Return 4
            Case 22
                Return 3
            Case 23
                Return 2
            Case 24
                Return 1
            Case 25
                Return 32
            Case 26
                Return 31
            Case 27
                Return 30
            Case 28
                Return 29
            Case 29
                Return 28
            Case 30
                Return 27
            Case 31
                Return 26
            Case 32
                Return 25
            Case Else
                Return 0
        End Select
    End Function

    ''' <summary>
    ''' 90도 회전해줍니다. 이 함수에서는 시계방향 90도를 해줍니다.
    ''' </summary>
    ''' <param name="source">X, Y 좌표입니다. XY형식으로 붙여서 쓰세요. 예: 88, 11, 35</param>
    ''' <returns></returns>
    Public Function Flip_Rotate_90(ByVal source As Integer) As Flip_Rotate_XYReturn
        Dim returnValue As New Flip_Rotate_XYReturn
        Select Case source
            Case 11
                returnValue.x = 1
                returnValue.y = 8
            Case 12
                returnValue.x = 2
                returnValue.y = 8
            Case 13
                returnValue.x = 3
                returnValue.y = 8
            Case 14
                returnValue.x = 4
                returnValue.y = 8
            Case 15
                returnValue.x = 5
                returnValue.y = 8
            Case 16
                returnValue.x = 6
                returnValue.y = 8
            Case 17
                returnValue.x = 7
                returnValue.y = 8
            Case 18
                returnValue.x = 8
                returnValue.y = 8
            Case 21
                returnValue.x = 1
                returnValue.y = 7
            Case 22
                returnValue.x = 2
                returnValue.y = 7
            Case 23
                returnValue.x = 3
                returnValue.y = 7
            Case 24
                returnValue.x = 4
                returnValue.y = 7
            Case 25
                returnValue.x = 5
                returnValue.y = 7
            Case 26
                returnValue.x = 6
                returnValue.y = 7
            Case 27
                returnValue.x = 7
                returnValue.y = 7
            Case 28
                returnValue.x = 8
                returnValue.y = 7
            Case 31
                returnValue.x = 1
                returnValue.y = 6
            Case 32
                returnValue.x = 2
                returnValue.y = 6
            Case 33
                returnValue.x = 3
                returnValue.y = 6
            Case 34
                returnValue.x = 4
                returnValue.y = 6
            Case 35
                returnValue.x = 5
                returnValue.y = 6
            Case 36
                returnValue.x = 6
                returnValue.y = 6
            Case 37
                returnValue.x = 7
                returnValue.y = 6
            Case 38
                returnValue.x = 8
                returnValue.y = 6
            Case 41
                returnValue.x = 1
                returnValue.y = 5
            Case 42
                returnValue.x = 2
                returnValue.y = 5
            Case 43
                returnValue.x = 3
                returnValue.y = 5
            Case 44
                returnValue.x = 4
                returnValue.y = 5
            Case 45
                returnValue.x = 5
                returnValue.y = 5
            Case 46
                returnValue.x = 6
                returnValue.y = 5
            Case 47
                returnValue.x = 7
                returnValue.y = 5
            Case 48
                returnValue.x = 8
                returnValue.y = 5
            Case 51
                returnValue.x = 1
                returnValue.y = 4
            Case 52
                returnValue.x = 2
                returnValue.y = 4
            Case 53
                returnValue.x = 3
                returnValue.y = 4
            Case 54
                returnValue.x = 4
                returnValue.y = 4
            Case 55
                returnValue.x = 5
                returnValue.y = 4
            Case 56
                returnValue.x = 6
                returnValue.y = 4
            Case 57
                returnValue.x = 7
                returnValue.y = 4
            Case 58
                returnValue.x = 8
                returnValue.y = 4
            Case 61
                returnValue.x = 1
                returnValue.y = 3
            Case 62
                returnValue.x = 2
                returnValue.y = 3
            Case 63
                returnValue.x = 3
                returnValue.y = 3
            Case 64
                returnValue.x = 4
                returnValue.y = 3
            Case 65
                returnValue.x = 5
                returnValue.y = 3
            Case 66
                returnValue.x = 6
                returnValue.y = 3
            Case 67
                returnValue.x = 7
                returnValue.y = 3
            Case 68
                returnValue.x = 8
                returnValue.y = 3
            Case 71
                returnValue.x = 1
                returnValue.y = 2
            Case 72
                returnValue.x = 2
                returnValue.y = 2
            Case 73
                returnValue.x = 3
                returnValue.y = 2
            Case 74
                returnValue.x = 4
                returnValue.y = 2
            Case 75
                returnValue.x = 5
                returnValue.y = 2
            Case 76
                returnValue.x = 6
                returnValue.y = 2
            Case 77
                returnValue.x = 7
                returnValue.y = 2
            Case 78
                returnValue.x = 8
                returnValue.y = 2
            Case 81
                returnValue.x = 1
                returnValue.y = 1
            Case 82
                returnValue.x = 2
                returnValue.y = 1
            Case 83
                returnValue.x = 3
                returnValue.y = 1
            Case 84
                returnValue.x = 4
                returnValue.y = 1
            Case 85
                returnValue.x = 5
                returnValue.y = 1
            Case 86
                returnValue.x = 6
                returnValue.y = 1
            Case 87
                returnValue.x = 7
                returnValue.y = 1
            Case 88
                returnValue.x = 8
                returnValue.y = 1


        End Select
        Return returnValue
    End Function

    Public Function Flip_Rotate_90_MC(ByVal source As Integer) As Integer
        Select Case source
            Case 1
                Return 9
            Case 2
                Return 10
            Case 3
                Return 11
            Case 4
                Return 12
            Case 5
                Return 13
            Case 6
                Return 14
            Case 7
                Return 15
            Case 8
                Return 16
            Case 9
                Return 17
            Case 10
                Return 18
            Case 11
                Return 19
            Case 12
                Return 20
            Case 13
                Return 21
            Case 14
                Return 22
            Case 15
                Return 23
            Case 16
                Return 24
            Case 17
                Return 25
            Case 18
                Return 26
            Case 19
                Return 27
            Case 20
                Return 28
            Case 21
                Return 29
            Case 22
                Return 30
            Case 23
                Return 31
            Case 24
                Return 32
            Case 25
                Return 1
            Case 26
                Return 2
            Case 27
                Return 3
            Case 28
                Return 4
            Case 29
                Return 5
            Case 30
                Return 6
            Case 31
                Return 7
            Case 32
                Return 8
            Case Else
                Return 0
        End Select
    End Function

    ''' <summary>
    ''' 90도 회전해줍니다. 이 함수에서는 반시계방향 90도를 해줍니다.
    ''' </summary>
    ''' <param name="source">X, Y 좌표입니다. XY형식으로 붙여서 쓰세요. 예: 88, 11, 35</param>
    ''' <returns></returns>
    Public Function Flip_Rotate_270(ByVal source As Integer) As Flip_Rotate_XYReturn
        Dim returnValue As New Flip_Rotate_XYReturn
        Select Case source
            Case 11
                returnValue.x = 8
                returnValue.y = 1
            Case 12
                returnValue.x = 7
                returnValue.y = 1
            Case 13
                returnValue.x = 6
                returnValue.y = 1
            Case 14
                returnValue.x = 5
                returnValue.y = 1
            Case 15
                returnValue.x = 4
                returnValue.y = 1
            Case 16
                returnValue.x = 3
                returnValue.y = 1
            Case 17
                returnValue.x = 2
                returnValue.y = 1
            Case 18
                returnValue.x = 1
                returnValue.y = 1
            Case 21
                returnValue.x = 8
                returnValue.y = 2
            Case 22
                returnValue.x = 7
                returnValue.y = 2
            Case 23
                returnValue.x = 6
                returnValue.y = 2
            Case 24
                returnValue.x = 5
                returnValue.y = 2
            Case 25
                returnValue.x = 4
                returnValue.y = 2
            Case 26
                returnValue.x = 3
                returnValue.y = 2
            Case 27
                returnValue.x = 2
                returnValue.y = 2
            Case 28
                returnValue.x = 1
                returnValue.y = 2
            Case 31
                returnValue.x = 8
                returnValue.y = 3
            Case 32
                returnValue.x = 7
                returnValue.y = 3
            Case 33
                returnValue.x = 6
                returnValue.y = 3
            Case 34
                returnValue.x = 5
                returnValue.y = 3
            Case 35
                returnValue.x = 4
                returnValue.y = 3
            Case 36
                returnValue.x = 3
                returnValue.y = 3
            Case 37
                returnValue.x = 2
                returnValue.y = 3
            Case 38
                returnValue.x = 1
                returnValue.y = 3
            Case 41
                returnValue.x = 8
                returnValue.y = 4
            Case 42
                returnValue.x = 7
                returnValue.y = 4
            Case 43
                returnValue.x = 6
                returnValue.y = 4
            Case 44
                returnValue.x = 5
                returnValue.y = 4
            Case 45
                returnValue.x = 4
                returnValue.y = 4
            Case 46
                returnValue.x = 3
                returnValue.y = 4
            Case 47
                returnValue.x = 2
                returnValue.y = 4
            Case 48
                returnValue.x = 1
                returnValue.y = 4
            Case 51
                returnValue.x = 8
                returnValue.y = 5
            Case 52
                returnValue.x = 7
                returnValue.y = 5
            Case 53
                returnValue.x = 6
                returnValue.y = 5
            Case 54
                returnValue.x = 5
                returnValue.y = 5
            Case 55
                returnValue.x = 4
                returnValue.y = 5
            Case 56
                returnValue.x = 3
                returnValue.y = 5
            Case 57
                returnValue.x = 2
                returnValue.y = 5
            Case 58
                returnValue.x = 1
                returnValue.y = 5
            Case 61
                returnValue.x = 8
                returnValue.y = 6
            Case 62
                returnValue.x = 7
                returnValue.y = 6
            Case 63
                returnValue.x = 6
                returnValue.y = 6
            Case 64
                returnValue.x = 5
                returnValue.y = 6
            Case 65
                returnValue.x = 4
                returnValue.y = 6
            Case 66
                returnValue.x = 3
                returnValue.y = 6
            Case 67
                returnValue.x = 2
                returnValue.y = 6
            Case 68
                returnValue.x = 1
                returnValue.y = 6
            Case 71
                returnValue.x = 8
                returnValue.y = 7
            Case 72
                returnValue.x = 7
                returnValue.y = 7
            Case 73
                returnValue.x = 6
                returnValue.y = 7
            Case 74
                returnValue.x = 5
                returnValue.y = 7
            Case 75
                returnValue.x = 4
                returnValue.y = 7
            Case 76
                returnValue.x = 3
                returnValue.y = 7
            Case 77
                returnValue.x = 2
                returnValue.y = 7
            Case 78
                returnValue.x = 1
                returnValue.y = 7
            Case 81
                returnValue.x = 8
                returnValue.y = 8
            Case 82
                returnValue.x = 7
                returnValue.y = 8
            Case 83
                returnValue.x = 6
                returnValue.y = 8
            Case 84
                returnValue.x = 5
                returnValue.y = 8
            Case 85
                returnValue.x = 4
                returnValue.y = 8
            Case 86
                returnValue.x = 3
                returnValue.y = 8
            Case 87
                returnValue.x = 2
                returnValue.y = 8
            Case 88
                returnValue.x = 1
                returnValue.y = 8
        End Select
        Return returnValue
    End Function

    Public Function Flip_Rotate_270_MC(ByVal source As Integer) As Integer
        Select Case source
            Case 1
                Return 25
            Case 2
                Return 26
            Case 3
                Return 27
            Case 4
                Return 28
            Case 5
                Return 29
            Case 6
                Return 30
            Case 7
                Return 31
            Case 8
                Return 32
            Case 9
                Return 1
            Case 10
                Return 2
            Case 11
                Return 3
            Case 12
                Return 4
            Case 13
                Return 5
            Case 14
                Return 6
            Case 15
                Return 7
            Case 16
                Return 8
            Case 17
                Return 9
            Case 18
                Return 10
            Case 19
                Return 11
            Case 20
                Return 12
            Case 21
                Return 13
            Case 22
                Return 14
            Case 23
                Return 15
            Case 24
                Return 16
            Case 25
                Return 17
            Case 26
                Return 18
            Case 27
                Return 19
            Case 28
                Return 20
            Case 29
                Return 21
            Case 30
                Return 22
            Case 31
                Return 23
            Case 32
                Return 24
            Case Else
                Return 0
        End Select
    End Function
#End Region
End Class
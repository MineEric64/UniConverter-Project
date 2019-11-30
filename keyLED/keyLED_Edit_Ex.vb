Public Class keyLED_Edit_Ex
    Dim openedproj As Boolean = False
    Dim projectPath As String = String.Empty

    Public IsAutoLoaded As Boolean = False
    Public FlipIndex As Integer

    Private Sub KeyLED_Edit_Ex_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainProject.abl_openedproj Then
            openedproj = True
            projectPath = MainProject.abl_Name
        End If
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
        keyLED_Edit.Ex_Flip.Insert(0, GetFlipStructure()) '나중에 FlipIndex로 바꿔야 함.
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

#Region "Flip Functions"
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
#End Region
End Class
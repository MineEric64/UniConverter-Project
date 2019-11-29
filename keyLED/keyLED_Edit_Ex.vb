Public Class keyLED_Edit_Ex
    Dim openedproj As Boolean = False
    Dim projectPath As String = String.Empty

    Dim FlipIndex As Integer

    Private Sub KeyLED_Edit_Ex_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainProject.abl_openedproj Then
            openedproj = True
            projectPath = MainProject.abl_Name
        End If
    End Sub

    Public Function GetFlipStructure()
        Dim FlipStructure_ As New FlipStructure(Mirror.None, 0, False)

        If MirrorCheckBox.Checked Then
            If MirrorComboBox.Text = "Horizontal" Then
                FlipStructure_.Mirror = Mirror.Horizontal
            ElseIf MirrorComboBox.Text = "Vertical" Then
                FlipStructure_.Mirror = Mirror.Vertical
            End If
        End If

        If RotateCheckBox.Checked Then
            FlipStructure_.Rotate = Integer.Parse(RotateComboBox.Text)
        End If

        FlipStructure_.Duplicate = DuplicateCheckBox.Checked

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

    Private Sub FlipExtensions_Changed(sender As Object, e As EventArgs) Handles MirrorCheckBox.CheckedChanged, MirrorComboBox.SelectedIndexChanged, RotateCheckBox.CheckedChanged, RotateComboBox.SelectedIndexChanged, DuplicateCheckBox.CheckedChanged
        keyLED_Edit.Ex_Flip(0) = GetFlipStructure() '나중에 FlipIndex로 바꿔야 함.
    End Sub
End Class
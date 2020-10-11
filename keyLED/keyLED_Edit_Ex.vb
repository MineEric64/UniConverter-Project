Imports UniConverter.LEDExtensions
Imports UniConverter.LEDExtensions.FlipClass

Public Class keyLED_Edit_Ex
    Private Sub KeyLED_Edit_Ex_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case MainProject.lang
            Case Translator.tL.Korean
                Me.Text = "keyLED 편집 - LED 확장 기능"
                TabControl.TabPages(0).Text = "[기본 플러그인]"
                TabControl.TabPages(1).Text = "플립"
                TabControl.TabPages(2).Text = "색상 플러그인"

                FlipGroupBox.Text = "LED 플러그인"
                Flip_ResetButton.Text = "초기화"
                Flip_MirrorCheckBox.Text = "거울 모드"
                Flip_MirrorComboBox.Items(0) = My.Resources.Contents.Flip_Horizontal 'ComboBox에서 Items(i)는 string (Text)임.
                Flip_MirrorComboBox.Items(1) = My.Resources.Contents.Flip_Vertical
                Flip_RotateCheckBox.Text = "회전"
                Flip_DuplicateCheckBox.Text = "복제"
        End Select
    End Sub

    Public Function GetFlipStructure() As FlipClass
        Dim flip As New FlipClass(Mirror.None, 0, False)

        If Flip_MirrorCheckBox.Checked Then
            If Flip_MirrorComboBox.Text = My.Resources.Contents.Flip_Horizontal Then
                flip.MirrorMode = Mirror.Horizontal
            ElseIf Flip_MirrorComboBox.Text = My.Resources.Contents.Flip_Vertical Then
                flip.MirrorMode = Mirror.Vertical
            End If
        End If

        If Flip_RotateCheckBox.Checked Then
            flip.Rotate = Integer.Parse(Flip_RotateComboBox.Text.Replace("°", ""))
        End If

        flip.Duplicate = Flip_DuplicateCheckBox.Checked

        Return flip
    End Function

    

    Private Sub FlipExtensions_Changed(sender As Object, e As EventArgs) Handles Flip_MirrorCheckBox.CheckedChanged, Flip_MirrorComboBox.SelectedIndexChanged, Flip_RotateCheckBox.CheckedChanged, Flip_RotateComboBox.SelectedIndexChanged, Flip_DuplicateCheckBox.CheckedChanged
        If keyLED_Edit.GAZUA_.IsBusy Then
            Exit Sub '오류!
        End If

        If Not keyLED_Edit.ExtensionList.ContainsKey(keyLED_Edit.EXTENSION_DEFAULT_KEY) Then
            keyLED_Edit.ExtensionList.Add(keyLED_Edit.EXTENSION_DEFAULT_KEY, New LEDExtensions())
        End If
        keyLED_Edit.ExtensionList(keyLED_Edit.EXTENSION_DEFAULT_KEY).Flip = GetFlipStructure()
    End Sub

    Private Sub Flip_ResetButton_Click(sender As Object, e As EventArgs) Handles Flip_ResetButton.Click
        Flip_MirrorCheckBox.Checked = False
        Flip_MirrorComboBox.SelectedIndex = 0
        Flip_RotateCheckBox.Checked = False
        Flip_RotateComboBox.SelectedIndex = 0
        Flip_DuplicateCheckBox.Checked = False
    End Sub
End Class
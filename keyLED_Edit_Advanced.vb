Imports System.Xml
Imports NAudio.Wave

Public Class keyLED_Edit_Advanced
    Public Shared IsSaved = True

    Private Sub KeyLED_Edit_Advanced_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSettings()
    End Sub

    Private Sub Load_Timer_Tick(sender As Object, e As EventArgs) Handles Load_Timer.Tick
        Dim AdvChk_Checked As Boolean

        If keyLED_Edit.AdvChk.Checked = False Then
            Me.Enabled = False
            AdvChk_Checked = False
        End If

        If keyLED_Edit.AdvChk.Checked = True AndAlso AdvChk_Checked = False Then
            Me.Enabled = True
            AdvChk_Checked = True
        End If
    End Sub

    Private Sub LED_ResetButton_Click(sender As Object, e As EventArgs) Handles LED_ResetButton.Click
        Dim itm As New List(Of String) _
    From {"Note Length", "Delta Time", "Absolute Time"}

        DelayMode1.Items.Clear() 'Clear the Items.

        '---Beta Code---
        '이 리셋 버튼 코드들은 추가 및 변경 및 삭제가 될 때마다 이 코드들을 편집을 해야 합니다.
        '이 주의사항을 꼭 지켜주시고 다 읽어주셨다면 편집할 수 있습니다.

        'Delay Mode #1 / #2
        For Each va In itm
            DelayMode1.Items.Add(va) 'Add the Delay Mode Items.
        Next
        DelayMode1.SelectedIndex = 2 'Reset the Selected Item.

        'Delay Convert #1
        DelayConvert1_1.Checked = False
        DelayConvert1_2.Checked = True

        'Delay Convert #2
        DelayConvert2_1.Checked = True

        'Delay Convert #3
        DelayConvert3_1.Checked = False
        DelayConvert3_2.Checked = False
        DelayConvert3_3.Checked = True

        Save2Settings(False)
    End Sub

    Private Sub DelayMode1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DelayMode1.SelectedIndexChanged
        If DelayMode1.SelectedItem.ToString = "Note Length" Then
            DelayMode2.Enabled = True
            DelayMode3.Enabled = False
            DelayMode4.Enabled = False
        ElseIf DelayMode1.SelectedItem.ToString = "Delta Time" Then
            DelayMode2.Enabled = False
            DelayMode3.Enabled = True
            DelayMode4.Enabled = False
        ElseIf DelayMode1.SelectedItem.ToString = "Absolute Time" Then
            DelayMode2.Enabled = False
            DelayMode3.Enabled = False
            DelayMode4.Enabled = True
        End If
        IsSaved = False
    End Sub

    Private Sub LED_SaveButton_Click(sender As Object, e As EventArgs) Handles LED_SaveButton.Click
        Save2Settings(True)
    End Sub

    ''' <summary>
    ''' Save Settings To XML.
    ''' </summary>
    ''' <param name="ShowMessage">Showing Message.</param>
    Public Sub Save2Settings(ShowMessage As Boolean)
        '이 설정 저장 코드는 설정 코드가 수정될 때 마다 코드를 수정 해야 합니다.
        Try
            Dim file_ex = Application.StartupPath + "\settings.xml"
            Dim setNode As New XmlDocument
            Dim setaNode As XmlNode
            setNode.Load(file_ex)

            setaNode = setNode.SelectSingleNode("/Settings-XML/keyLED-Adv")
            If setaNode IsNot Nothing Then

                If DelayMode1.SelectedItem.ToString = "Note Length" Then
                    setaNode.ChildNodes(0).InnerText = "NoteLength"
                ElseIf DelayMode1.SelectedItem.ToString = "Delta Time" Then
                    setaNode.ChildNodes(0).InnerText = "DeltaTime"
                ElseIf DelayMode1.SelectedItem.ToString = "Absolute Time" Then
                    setaNode.ChildNodes(0).InnerText = "AbsoluteTime"
                End If

                If DelayConvert1_1.Checked Then
                    setaNode.ChildNodes(1).InnerText = "Non-Convert"
                ElseIf DelayConvert1_2.Checked Then
                    setaNode.ChildNodes(1).InnerText = "NL4Ticks/NL2M"
                End If

                If DelayConvert2_1.Checked Then
                    setaNode.ChildNodes(2).InnerText = "Non-Convert"
                End If

                If DelayConvert3_1.Checked Then
                    setaNode.ChildNodes(3).InnerText = "Non-Convert"
                ElseIf DelayConvert3_2.Checked Then
                    setaNode.ChildNodes(3).InnerText = "AbTofMIDI"
                ElseIf DelayConvert3_3.Checked Then
                    setaNode.ChildNodes(3).InnerText = "TimeLine/NL2M"
                End If

            Else
                Throw New FormatException("Settings XML's Argument is invaild. <keyLED-Adv>")
            End If

            setNode.Save(file_ex)
            IsSaved = True
            If ShowMessage = True Then MessageBox.Show("Saved Settings!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            IsSaved = False
        End Try
    End Sub

    ''' <summary>
    ''' Load Settings To XML.
    ''' </summary>
    Public Sub LoadSettings()
        '이 설정 불러오기 코드는 설정 코드가 수정될 때 마다 코드를 수정 해야 합니다.
        Try
            Dim file_ex = Application.StartupPath + "\settings.xml"
            Dim setNode As New XmlDocument
            Dim setaNode As XmlNode
            setNode.Load(file_ex)

            setaNode = setNode.SelectSingleNode("/Settings-XML/keyLED-Adv")
            If setaNode IsNot Nothing Then

                If setaNode.ChildNodes(0).InnerText = "NoteLength" Then
                    DelayMode1.Text = "Note Length"
                ElseIf setaNode.ChildNodes(0).InnerText = "DeltaTime" Then
                    DelayMode1.Text = "Delta Time"
                ElseIf setaNode.ChildNodes(0).InnerText = "AbsoluteTime" Then
                    DelayMode1.Text = "Absolute Time"
                End If

                If setaNode.ChildNodes(1).InnerText = "Non-Convert" Then
                    DelayConvert1_1.Checked = True
                    DelayConvert1_2.Checked = False
                ElseIf setaNode.ChildNodes(1).InnerText = "NL4Ticks/NL2M" Then
                    DelayConvert1_1.Checked = False
                    DelayConvert1_2.Checked = True
                End If

                If setaNode.ChildNodes(2).InnerText = "Non-Convert" Then
                    DelayConvert2_1.Checked = True
                End If

                If setaNode.ChildNodes(3).InnerText = "Non-Convert" Then
                    DelayConvert3_1.Checked = True
                    DelayConvert3_2.Checked = False
                    DelayConvert3_3.Checked = False
                ElseIf setaNode.ChildNodes(3).InnerText = "AbTofMIDI" Then
                    DelayConvert3_1.Checked = False
                    DelayConvert3_2.Checked = True
                    DelayConvert3_3.Checked = False
                ElseIf setaNode.ChildNodes(3).InnerText = "TimeLine/NL2M" Then
                    DelayConvert3_1.Checked = False
                    DelayConvert3_2.Checked = False
                    DelayConvert3_3.Checked = True
                End If

            Else
                Throw New FormatException("Settings XML's Argument is invaild. <keyLED-Adv>")
            End If

            setNode.Save(file_ex)
            IsSaved = True

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            IsSaved = False
        End Try
    End Sub
End Class
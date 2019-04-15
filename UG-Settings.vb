Imports System.Xml

Public Class UG_Settings
    Private IsSaved As Boolean
    Public setxml As XDocument
    Private Sub UG_Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '이 설정 로딩 코드는 설정 코드가 수정될 때 마다 코드를 수정 해야 합니다.
        Try
            Dim file_ex = Application.StartupPath + "\settings.xml"
            setxml = XDocument.Load(file_ex)
            Me.Text = String.Format("{0}: Settings", MainProject.Text)
            AbletonSet.Enabled = False
            UniPackSet.Enabled = False

            Dim lst As New List(Of String) _
            From {"Any Ableton", "Ableton 9 Lite", "Ableton 9 Trial", "Ableton 9 Suite", "Ableton 10"}
            For Each item As String In lst
                AbletonSet.Items.Add(item)
            Next

            Dim lst2 As New List(Of String) _
            From {"Zip / Uni File"}
            For Each item As String In lst2
                UniPackSet.Items.Add(item)
            Next

            'Reading Value of Settings.
            If Convert.ToBoolean(setxml.<Settings-XML>.<UCV-Settings>.<CheckUpdate>.Value) = True Then
                ChkUpdate.Checked = True
            ElseIf Convert.ToBoolean(setxml.<Settings-XML>.<UCV-Settings>.<LatestVer>.Value) = False Then
                ChkUpdate.Checked = False
            Else
                Throw New FormatException("<CheckUpdate>'s Value must has True/False.")
            End If

            If Convert.ToBoolean(setxml.<Settings-XML>.<UCV-Settings>.<LatestVer>.Value) = True Then
                LatestVer.Checked = True
            ElseIf Convert.ToBoolean(setxml.<Settings-XML>.<UCV-Settings>.<LatestVer>.Value) = False Then
                LatestVer.Checked = False
            Else
                Throw New FormatException("<LatestVer>'s Value must has True/False.")
            End If

            Select Case setxml.<Settings-XML>.<UCV-PATH>.<AbletonVersion>.Value
                Case "AnyAbleton"
                    AbletonSet.Text = "Any Ableton"
                Case "Ableton9_Lite"
                    AbletonSet.Text = "Ableton 9 Lite"
                Case "Ableton9_Trial"
                    AbletonSet.Text = "Ableton 9 Trial"
                Case "Ableton9_Suite"
                    AbletonSet.Text = "Ableton 9 Suite"
                Case "Ableton10"
                    AbletonSet.Text = "Ableton 10"
                Case Else
                    Throw New FormatException("<AbletonVersion>'s Value is invaild.")
            End Select

            Select Case setxml.<Settings-XML>.<UCV-PATH>.<ConvertUniPack>.Value
                Case "zip/uni"
                    UniPackSet.Text = "Zip / Uni File"
                Case Else
                    Throw New FormatException("<ConvertUniPack>'s Value is invaild.")
            End Select

            IsSaved = True

        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        '이 설정 초기화 코드는 설정 코드가 수정될 때 마다 코드를 수정 해야 합니다.
        Try
            Dim file_ex = Application.StartupPath + "\settings.xml"
            Dim setNode As New XmlDocument
            Dim setaNode As XmlNode
            setNode.Load(file_ex)

            setaNode = setNode.SelectSingleNode("/Settings-XML/UCV-Settings")
            If setaNode IsNot Nothing Then

                ChkUpdate.Checked = True
                setaNode.ChildNodes(0).InnerText = "True"

                LatestVer.Checked = False
                setaNode.ChildNodes(1).InnerText = "False"

            Else
                Throw New FormatException("Settings XML's Argument is invaild. <UCV-Settings>")
            End If

            setaNode = setNode.SelectSingleNode("/Settings-XML/UCV-PATH")
            If setaNode IsNot Nothing Then

                AbletonSet.Text = "Any Ableton"
                setaNode.ChildNodes(0).InnerText = "AnyAbleton"

                UniPackSet.Text = "Zip / Uni File"
                setaNode.ChildNodes(1).InnerText = "zip/uni"

            Else
                Throw New FormatException("Settings XML File's Argument is invaild. <UCV-PATH>")
            End If

            setNode.Save(file_ex)
            IsSaved = True
            MessageBox.Show("Reseted Settings!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UG_Setting_Values_CheckedChanged(sender As Object, e As EventArgs) Handles ChkUpdate.CheckedChanged, LatestVer.CheckedChanged, AbletonSet.SelectedIndexChanged, UniPackSet.SelectedIndexChanged
        IsSaved = False
    End Sub

    Private Sub UG_Setting_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If IsSaved = False Then
                Dim result As DialogResult = MessageBox.Show("You didn't save UniConverter's Settings. Would you like to save your Settings?", Me.Text & ": Not Saved", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    Save2Settings(False)
                ElseIf result = DialogResult.Cancel Then
                    e.Cancel = True
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
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

            setaNode = setNode.SelectSingleNode("/Settings-XML/UCV-Settings")
            If setaNode IsNot Nothing Then

                If ChkUpdate.Checked = True Then
                    setaNode.ChildNodes(0).InnerText = "True"
                Else
                    setaNode.ChildNodes(0).InnerText = "False"
                End If

                If LatestVer.Checked = True Then
                    setaNode.ChildNodes(1).InnerText = "True"
                Else
                    setaNode.ChildNodes(1).InnerText = "False"
                End If

            Else
                Throw New FormatException("Settings XML's Argument is invaild. <UCV-Settings>")
            End If

            setaNode = setNode.SelectSingleNode("/Settings-XML/UCV-PATH")
            If setaNode IsNot Nothing Then

                Select Case AbletonSet.Text
                    Case "Any Ableton"
                        setaNode.ChildNodes(0).InnerText = "AnyAbleton"
                    Case "Ableton 9 Lite"
                        setaNode.ChildNodes(0).InnerText = "Ableton9_Lite"
                    Case "Ableton 9 Trial"
                        setaNode.ChildNodes(0).InnerText = "Ableton9_Trial"
                    Case "Ableton 9 Suite"
                        setaNode.ChildNodes(0).InnerText = "Ableton9_Suite"
                    Case "Ableton 10"
                        setaNode.ChildNodes(0).InnerText = "Ableton10"
                    Case Else
                        Throw New FormatException("<AbletonVersion>'s Value is invaild.")
                End Select

                Select Case UniPackSet.Text
                    Case "Zip / Uni File"
                        setaNode.ChildNodes(1).InnerText = "zip/uni"
                    Case Else
                        Throw New FormatException("<ConvertUniPack>'s Value is invaild.")
                End Select

                setNode.Save(file_ex)
                IsSaved = True
                If ShowMessage = True Then MessageBox.Show("Saved Settings!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Throw New FormatException("Settings XML's Argument is invaild. <UCV-PATH>")
            End If

        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
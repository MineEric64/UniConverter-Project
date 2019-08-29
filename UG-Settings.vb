Imports System.Xml

Public Class UG_Settings
    Private IsSaved As Boolean
    Public setxml As New XmlDocument
    Private Sub UG_Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '이 설정 로딩 코드는 설정 코드가 수정될 때 마다 코드를 수정 해야 합니다.
        Try
            Dim setaNode As XmlNode
            Dim file_ex As String = Application.StartupPath + "\settings.xml"
            setxml.Load(file_ex)

            Me.Text = String.Format("{0}: Settings", MainProject.Text)

            setaNode = setxml.SelectSingleNode("/Settings-XML/UCV-Settings")
            If setaNode IsNot Nothing Then

                'Reading Value of Settings.
                Select Case setaNode.ChildNodes(0).InnerText
                    Case "True"
                        ChkUpdate.Checked = True
                    Case "False"
                        ChkUpdate.Checked = False
                    Case Else
                        Throw New FormatException("<CheckUpdate>'s Value is invaild.")
                End Select

                Select Case setaNode.ChildNodes(1).InnerText
                    Case "True"
                        LatestVer.Checked = True
                    Case "False"
                        LatestVer.Checked = False
                    Case Else
                        Throw New FormatException("<LatestVer>'s Value is invaild.")
                End Select

                Select Case setaNode.ChildNodes(2).InnerText
                    Case "True"
                        SetUpLight.Checked = True
                    Case "False"
                        SetUpLight.Checked = False
                    Case Else
                        Throw New FormatException("<SetupLights>'s Value is invaild.")
                End Select

                setaNode = setxml.SelectSingleNode("/Settings-XML/UCV-PATH")

                Select Case setaNode.ChildNodes(2).InnerText
                    Case "True"
                        CleanTheTexts.Checked = True
                    Case "False"
                        CleanTheTexts.Checked = False
                    Case Else
                        Throw New FormatException("<CleanTheText>'s Value is invaild.")
                End Select

                IsSaved = True
            End If

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
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

                LatestVer.Checked = True
                setaNode.ChildNodes(1).InnerText = "True"

                SetUpLight.Checked = True
                setaNode.ChildNodes(2).InnerText = "True"

            Else
                Throw New FormatException("Settings XML's Argument is invaild. <UCV-Settings>")
            End If

            setaNode = setNode.SelectSingleNode("/Settings-XML/UCV-PATH")
            If setaNode IsNot Nothing Then

                CleanTheTexts.Checked = False
                setaNode.ChildNodes(2).InnerText = "False"

            Else
                Throw New FormatException("Settings XML File's Argument is invaild. <UCV-PATH>")
            End If

            setNode.Save(file_ex)
            IsSaved = True
            MessageBox.Show("Reseted Settings!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub UG_Setting_Values_CheckedChanged(sender As Object, e As EventArgs) Handles ChkUpdate.CheckedChanged, LatestVer.CheckedChanged, CleanTheTexts.CheckedChanged, SetUpLight.CheckedChanged
        '이 코드는 기능이 추가되면 Handles 코드를 수정해야 합니다.
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
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
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

                Select Case ChkUpdate.Checked
                    Case True
                        setaNode.ChildNodes(0).InnerText = "True"
                    Case False
                        setaNode.ChildNodes(0).InnerText = "False"
                    Case Else
                        Throw New FormatException("<ChkUpdate>'s Value is invaild.")
                End Select

                Select Case LatestVer.Checked
                    Case True
                        setaNode.ChildNodes(1).InnerText = "True"
                    Case False
                        setaNode.ChildNodes(1).InnerText = "False"
                    Case Else
                        Throw New FormatException("<LatestVer>'s Value is invaild.")
                End Select

                Select Case SetUpLight.Checked
                    Case True
                        setaNode.ChildNodes(2).InnerText = "True"
                    Case False
                        setaNode.ChildNodes(2).InnerText = "False"
                    Case Else
                        Throw New FormatException("<SetUpLights>'s Value is invaild.")
                End Select

            Else
                    Throw New FormatException("Settings XML's Argument is invaild. <UCV-Settings>")
            End If

            setaNode = setNode.SelectSingleNode("/Settings-XML/UCV-PATH")
            If setaNode IsNot Nothing Then

                Select Case CleanTheTexts.Checked
                    Case True
                        setaNode.ChildNodes(2).InnerText = "True"
                    Case False
                        setaNode.ChildNodes(2).InnerText = "False"
                    Case Else
                        Throw New FormatException("<CleanTheText>'s Value is invaild.")
                End Select

                setNode.Save(file_ex)
                IsSaved = True
                If ShowMessage = True Then MessageBox.Show("Saved Settings!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Throw New FormatException("Settings XML's Argument is invaild. <UCV-PATH>")
            End If

        Catch ex As Exception
            If MainProject.IsGreatExMode Then
                MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error: " & ex.Message, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub
End Class
Imports System.Xml

Public Class UG_Settings
    Private IsSaved As Boolean
    Private IsLn As Boolean
    Public setxml As New XmlDocument

    Private Sub UG_Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '이 설정 로딩 코드는 설정 코드가 수정될 때 마다 코드를 수정 해야 합니다.
        Try
            Dim setaNode As XmlNode
            Dim file_ex As String = Application.StartupPath + "\settings.xml"
            setxml.Load(file_ex)

            Me.Text = String.Format("{0}: Settings", MainProject.Text)

            setaNode = setxml.SelectSingleNode("/UniConverter-XML/UniConverter-Settings")
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

                Select Case setaNode.ChildNodes(3).InnerText
                    Case "English", "Korean"
                        Select Case MainProject.lang
                            Case Translator.tL.English
                                LnComboBox.Text = setaNode.ChildNodes(3).InnerText
                            Case Translator.tL.Korean
                                Dim ln As String = setaNode.ChildNodes(3).InnerText
                                Select Case ln
                                    Case "English"
                                        LnComboBox.Text = "영어"
                                    Case "Korean"
                                        LnComboBox.Text = "한국어"
                                End Select
                        End Select

                    Case Else
                        Throw New FormatException("<Language>'s Value is invaild.")
                End Select

                IsSaved = True
            End If

            '언어 설정
            Select Case MainProject.lang
                Case Translator.tL.English
                    Exit Sub
                Case Translator.tL.Korean
                    Text = MainProject.Text & ": 설정"
                    ChkUpdate.Text = "자동 업데이트 확인"
                    LatestVer.Text = "업데이트 후 최신 버전 실행"
                    SetUpLight.Text = "런치패드 셋업 LED"

                    LnLb.Text = "언어: "
                    LnComboBox.Left -= 30
                    LnComboBox.Items.Clear()
                    LnComboBox.Items.AddRange({"영어", "한국어"})

                    ResetButton.Text = "초기화"
                    SaveButton.Text = "저장"
            End Select

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

            setaNode = setNode.SelectSingleNode("/UniConverter-XML/UniConverter-Settings")
            If setaNode IsNot Nothing Then

                ChkUpdate.Checked = True
                setaNode.ChildNodes(0).InnerText = "True"

                LatestVer.Checked = True
                setaNode.ChildNodes(1).InnerText = "True"

                SetUpLight.Checked = True
                setaNode.ChildNodes(2).InnerText = "True"

                Select Case MainProject.lang
                    Case Translator.tL.English
                        LnComboBox.Text = "English"
                    Case Translator.tL.Korean
                        LnComboBox.Text = "영어"
                End Select
                setaNode.ChildNodes(3).InnerText = "English"

            Else
                Throw New FormatException("Settings XML's Argument is invaild. <UniConverter-Settings>")
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

    Private Sub UG_Setting_Values_CheckedChanged(sender As Object, e As EventArgs) Handles ChkUpdate.CheckedChanged, LatestVer.CheckedChanged, SetUpLight.CheckedChanged
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

            setaNode = setNode.SelectSingleNode("/UniConverter-XML/UniConverter-Settings")
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

                Select Case LnComboBox.Text
                    Case "English", "Korean"
                        setaNode.ChildNodes(3).InnerText = LnComboBox.Text
                    Case "영어"
                        setaNode.ChildNodes(3).InnerText = "English"
                    Case "한국어"
                        setaNode.ChildNodes(3).InnerText = "Korean"
                    Case Else
                        Throw New FormatException("<Language>'s Value is invaild.")
                End Select

            Else
                Throw New FormatException("Settings XML's Argument is invaild. <UniConverter-Settings>")
            End If

            setNode.Save(file_ex)
            IsSaved = True
            If ShowMessage = True Then
                Select Case MainProject.lang
                    Case Translator.tL.English
                        MessageBox.Show("Saved Settings!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Case Translator.tL.Korean
                        MessageBox.Show("설정을 저장했습니다!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Select

                If IsLn Then
                    Dim result As String = String.Empty
                    Select Case MainProject.lang
                        Case Translator.tL.English
                            result = "It needs to restart the UniConverter." & vbNewLine & "Continue?"
                        Case Translator.tL.Korean
                            result = "유니컨버터를 재시작해야 합니다." & vbNewLine & "계속 하시겠습니까?"
                    End Select
                    If MessageBox.Show(result, Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        MainProject.IsUpdated = True
                        Application.Restart()
                    End If
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

    Private Sub LnComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LnComboBox.SelectedIndexChanged
        IsLn = True
    End Sub
End Class
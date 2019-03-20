Imports System
Imports System.IO
Imports System.Xml

Public Class DeveloperMode_Project
    Dim DeveloperMode_abl_openedproj As Boolean
    Dim DeveloperMode_abl_FileName As String
    Dim DeveloperMode_abl_TmpFileName As String
    Dim DeveloperMode_abl_FileVersion As String

    Private Sub DeveloperMode_Project_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("{0}: Developer Mode - Ableton Project Info", MainProject.Text)
        DeveloperMode_abl_openedproj = False

        If MainProject.abl_openedproj = True Then
            Project_FNTextBox.Text = MainProject.abl_FileName
            DeveloperMode_abl_openedproj = True
            OpenProject()
        End If
    End Sub

    Private Sub Project_OpenButton_Click(sender As Object, e As EventArgs) Handles Project_OpenButton.Click
        If ofd_Project.ShowDialog() = DialogResult.OK Then
            Project_FNTextBox.Text = ofd_Project.FileName
            OpenProject()
        End If
    End Sub

    Private Sub OpenProject()
        DeveloperMode_abl_openedproj = True
        DeveloperMode_abl_TmpFileName = Application.StartupPath & "\Workspace\ableproj\abl_proj.xml"

        'Developer Mode Project Infos
        DeveloperMode_abl_FileName = Project_FNTextBox.Text
        DeveloperMode_abl_FileVersion = File.ReadAllLines(DeveloperMode_abl_TmpFileName)(1)

        'Reading Project Infos
        For Each strLine As String In DeveloperMode_abl_FileVersion.Split("=")
            If strLine.Contains("Ableton Live") Then
                DeveloperMode_abl_FileVersion = strLine.Replace("Revision", "")
                DeveloperMode_abl_FileVersion = DeveloperMode_abl_FileVersion.Replace(MainProject.ast, "")
            End If
        Next

        Dim itm As New List(Of String) _
    From {"File Name", "Chains", "File Version", "KeyTracks"}
        Info_ListView.Items.Clear()
        For Each items As String In itm
            Info_ListView.Items.Add(items)
        Next
    End Sub

    Private Sub Info_ListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Info_ListView.SelectedIndexChanged
        Try
            If Info_ListView.SelectedItems.Count > 0 Then '이것이 신의 한수... SelectedItem 코드 작성 시 꼭 필요. (invaildArgument 오류)
                Dim SelectedItem As ListViewItem = Info_ListView.SelectedItems(0)
                If SelectedItem.Text = "File Name" Then
                    Info_TextBox.Text = Path.GetFileNameWithoutExtension(DeveloperMode_abl_FileName)
                ElseIf SelectedItem.Text = "Chains" Then

                ElseIf SelectedItem.Text = "File Version" Then
                    Info_TextBox.Text = DeveloperMode_abl_FileVersion
                ElseIf SelectedItem.Text = "KeyTracks" Then
                    'Dim Xpath As String = "/Ableton/LiveSet"
                    Dim doc As New XmlDocument
                    Dim NewElementList As XmlNodeList
                    doc.Load(Application.StartupPath & "\Workspace\ableproj\abl_proj.xml")
                    Dim str As String

                    '와... 진짜 LED 구현하느라 완전 힘들었다........ ㅠㅠ
                    NewElementList = doc.GetElementsByTagName("KeyTracks")
                    For i As Integer = 0 To NewElementList.Count - 1
                        For q As Integer = 0 To NewElementList(i).ChildNodes.Count - 1
                            If NewElementList(i).HasChildNodes Then str = str & NewElementList(i).ChildNodes(q).InnerXml & vbNewLine
                        Next
                    Next
                    Info_TextBox.Text = str
                    File.WriteAllText(Application.StartupPath & "\Workspace\ableproj\KeyTracks.xml", str)
                    Dim notes As XmlNodeList = doc.GetElementsByTagName("MidiNoteEvent")
                    Dim noteArr As New ListView
                    For i As Integer = 0 To doc.GetElementsByTagName("MidiKey").Count - 1
                        Dim note As Integer = CInt(Mid(doc.GetElementsByTagName("MidiKey")(i).OuterXml, doc.GetElementsByTagName("MidiKey")(i).OuterXml.Length - 5, 2))
                        noteArr.Items.Add(note)
                    Next
                    'LED 변환... (For x : notes 및 Short Message 부분)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error - " & ex.Message & vbNewLine & "Error Message: " & ex.StackTrace, Me.Text & ": Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Shared Function GetXpath(ByVal node As XmlNode) As String
        If node.Name = "#document" Then Return String.Empty
        Return GetXpath(node.SelectSingleNode("..")) & "/" + If(node.NodeType = XmlNodeType.Attribute, "@", String.Empty) + node.Name
    End Function

    Private Sub Info_TextBox_DoubleClick(sender As Object, e As EventArgs) Handles Info_TextBox.DoubleClick
        Clipboard.SetText(Info_TextBox.Text)
        Debug.WriteLine("Copied To Clipboard.")
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

    End Sub
End Class
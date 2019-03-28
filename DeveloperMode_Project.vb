Imports NAudio.Midi
Imports A2U_Project
Imports System.IO
Imports System.Xml
Imports System.Text.RegularExpressions

Public Class DeveloperMode_Project
    Dim DeveloperMode_abl_openedproj As Boolean
    Dim DeveloperMode_abl_FileName As String
    Dim DeveloperMode_abl_TmpFileName As String
    Dim DeveloperMode_abl_FileVersion As String

    Private Sub DeveloperMode_Project_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("{0}: Ableton Project Info", MainProject.Text)
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
        Dim doc As New XmlDocument
        Dim NewElementList As XmlElement
        doc.Load(Application.StartupPath & "\Workspace\ableproj\abl_proj.xml")
        NewElementList = doc.GetElementsByTagName("Ableton")(0)
        DeveloperMode_abl_FileVersion = NewElementList.GetAttribute("Creator")

        Dim itm As New List(Of String) _
    From {"File Name", "Chains", "File Version", "KeyTracks (keyLED)", "keyLED (using mid file)"}
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
                ElseIf SelectedItem.Text = "KeyTracks (keyLED)" Then

                    'Dim Xpath As String = "/Ableton/LiveSet"
                    Dim doc As New XmlDocument
                    Dim NewElementList As XmlNodeList
                    doc.Load(Application.StartupPath & "\Workspace\ableproj\abl_proj.xml")
                    Dim str As String

                    '와... 진짜 LED 구현하느라 완전 힘들었다........ ㅠㅠ
                    NewElementList = doc.GetElementsByTagName("KeyTracks") 'KeyTracks XML 트랙
                    For i As Integer = 0 To NewElementList.Count - 1
                        For q As Integer = 0 To NewElementList(i).ChildNodes.Count - 1 'XML 트랙 아이 추출
                            If NewElementList(i).HasChildNodes Then str = str & NewElementList(i).ChildNodes(q).InnerXml & vbNewLine
                        Next
                    Next

                    If String.IsNullOrWhiteSpace(str) Then 'KeyTracks가 없으면 예외 발생
                        Throw New Exception("There is no KeyTracks. Please use keyLED (mid).")
                    End If

                    File.WriteAllText(Application.StartupPath & "\Workspace\ableproj\KeyTracks.xml", str)
                    Debug.WriteLine("Added KeyTracks.xml")
                    Dim notes As XmlNodeList = doc.GetElementsByTagName("MidiNoteEvent") 'Note Event Value
                    Dim MidiKey As XmlNodeList = doc.GetElementsByTagName("MidiKey")
                    Dim noteArr As New ListView
                    For Each xi As XmlElement In MidiKey
                        Dim note As Integer = CInt(xi.GetAttribute("Value"))
                        noteArr.Items.Add(note)
                    Next

                    Dim evstr As String
                    For Each x As XmlElement In notes
                        Dim timeStart As Double = x.GetAttribute("Time") 'Start Time
                        Dim timeEnd As Double = timeStart + x.GetAttribute("Duration") 'Start Time + Duration (End Time)
                        Dim time As Double = timeEnd - timeStart 'Duration
                        Dim FinalTime As String = Convert.ToDouble(Mid(Convert.ToString(time), 1, 5)) * 1000 'Milliseconds

                        'keyLED Code.
                        Dim a As New MidiEvent(timeStart, 1, MidiCommandCode.NoteOn)
                        '수정 해야할 사항: Short Message 클래스 만들어 KeyTracks 코드 마무리. 
                    Next

                ElseIf SelectedItem.Text = "keyLED (using mid file)" Then
                    Dim result1 As DialogResult = MessageBox.Show("Did you import keyLED (MID) Files?", MainProject.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    If result1 = DialogResult.Yes Then
                        Dim result2 = InputBox("Please write the Mid File Name." & vbNewLine & "", Me.Text, "1.mid")

                        Dim LEDFileName = "Workspace\ableproj\CoLED\" & result2
                        Dim LEDFileC As New MidiFile(LEDFileName, False)

                        'A2UP keyLED Code.
                        Dim str As String
                        Dim UniNoteNumberX As Integer 'X
                        Dim UniNoteNumberY As Integer 'Y
                        For Each mdEvent_list In LEDFileC.Events
                            For Each mdEvent In mdEvent_list
                                If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                                    Dim a = DirectCast(mdEvent, NoteOnEvent)
                                    Dim b As New A2UP
                                    UniNoteNumberX = b.GX_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                                    UniNoteNumberY = b.GY_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                                    str = str & vbNewLine & "o " & UniNoteNumberX & " " & UniNoteNumberY & " a " & a.Velocity
                                    If Not a.DeltaTime = 0 Then
                                        str = str & vbNewLine & "d " & b.GetNoteDelay(b.keyLED_AC.T_NoteLength1, 120, 192, a.NoteLength)
                                    End If
                                ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then
                                    Dim a = DirectCast(mdEvent, NoteEvent)
                                    Dim b As New A2UP
                                    UniNoteNumberX = b.GX_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                                    UniNoteNumberY = b.GY_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                                    str = str & vbNewLine & "f " & UniNoteNumberX & " " & UniNoteNumberY
                                End If
                            Next
                        Next

                        If Regex.IsMatch(str, "8192") Then '8192 = Non-UniNoteNumber
                            str = str.Replace(" 8192", "").Trim() 'MC LED Convert.
                            str = str.Replace("o ", "o mc ").Trim() 'On MC LED Convert.
                            str = str.Replace("f ", "f mc ").Trim() 'Off MC LED Convert.
                        End If
                        Info_TextBox.Text = str

                    ElseIf result1 = DialogResult.No Then
                        Exit Sub
                    End If
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
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

    End Sub
End Class
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Imports A2UP
Imports NAudio.Midi

Public Class DeveloperMode_Project
    'Developer Mode에서는 Exception 예외 처리 때 GreatEx가 필요 없습니다.
    '어처피 Developer Mode는 불안정한 모드들을 Beta 기능으로 지원해주기 때문에 GreatEx가 필요 없습니다.

    Dim DeveloperMode_abl_openedproj As Boolean
    Dim DeveloperMode_abl_FileName As String
    Dim DeveloperMode_abl_TmpFileName As String
    Dim DeveloperMode_abl_FileVersion As String

    Public Enum EachCode
        ''' <summary>
        ''' XML2keyLED Code.
        ''' </summary>
        keyLED_1
        ''' <summary>
        ''' MID2keyLED (MIDI Extension) Code.
        ''' </summary>
        keyLED_MIDEX_1
        ''' <summary>
        ''' XML2Sounds Code.
        ''' </summary>
        SlicePoints_1
    End Enum

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
    From {"File Name", "Chains", "File Version", "Sound Cutting", "KeyTracks (keyLED)", "keyLED (MIDI Extension)"}
        Info_ListView.Items.Clear()
        For Each items As String In itm
            Info_ListView.Items.Add(items)
        Next
    End Sub

    Private Sub Info_ListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Info_ListView.SelectedIndexChanged
        If Info_ListView.SelectedItems.Count > 0 Then '이것이 신의 한수... SelectedItem 코드 작성 시 꼭 필요. (invaildArgument 오류)
            Dim SelectedItem As ListViewItem = Info_ListView.SelectedItems(0)
            Select Case SelectedItem.Text
                Case "File Name"
                    Info_TextBox.Text = Path.GetFileNameWithoutExtension(DeveloperMode_abl_FileName)
                Case "Chains"
                    Info_TextBox.Text = "No Way :("
                Case "File Version"
                    Info_TextBox.Text = DeveloperMode_abl_FileVersion
                Case "Sound Cutting"
                    GetSoundCutting(EachCode.SlicePoints_1, Application.StartupPath & "\Workspace\ableproj\abl_proj.xml", Application.StartupPath & "Workspace\unipack\", True)
                Case "KeyTracks (keyLED)"
                    Info_TextBox.Text = GetkeyLED(EachCode.keyLED_1, Application.StartupPath & "\Workspace\ableproj\abl_proj.xml", True, True)
                Case "keyLED (MIDI Extension)"
                    Info_TextBox.Text = GetkeyLED(EachCode.keyLED_MIDEX_1, Application.StartupPath & "\Workspace\ableproj\abl_proj.xml", True, True)
            End Select
        End If
    End Sub

    Private Shared Function GetXpath(ByVal node As XmlNode) As String
        If node.Name = "#document" Then Return String.Empty
        Return GetXpath(node.SelectSingleNode("..")) & "/" + If(node.NodeType = XmlNodeType.Attribute, "@", String.Empty) + node.Name
    End Function


    ''' <summary>
    ''' Converting keyLED with XML2keyLED.
    ''' </summary>
    ''' <param name="XMLPath">XML Path.</param>
    ''' <param name="keyLEDFiles">(Create/Don't Create) keyLED Files.</param>
    ''' <param name="DebugFiles">(Create/Don't Create) Debug Files.</param>
    ''' <returns></returns>
    Public Shared Function GetkeyLED(ByVal LEDArg As EachCode, ByVal XMLPath As String, ByVal keyLEDFiles As Boolean, ByVal DebugFiles As Boolean) As String

        Select Case LEDArg
            Case EachCode.keyLED_1
                Dim sto As New Stopwatch
                sto.Start()

                'Dim Xpath As String = "/Ableton/LiveSet"
                Dim doc As New XmlDocument
                Dim NewElementList As XmlNodeList
                doc.Load(XMLPath)
                Dim str As String = String.Empty

                '와... 진짜 LED 구현하느라 완전 힘들었다........ ㅠㅠ
                NewElementList = doc.GetElementsByTagName("KeyTracks") 'KeyTracks XML 트랙
                For i As Integer = 0 To NewElementList.Count - 1
                    For q As Integer = 0 To NewElementList(i).ChildNodes.Count - 1 'XML 트랙 아이 추출
                        If NewElementList(i).HasChildNodes Then str = str & NewElementList(i).ChildNodes(q).InnerXml & vbNewLine
                    Next
                Next

                If String.IsNullOrWhiteSpace(str) Then 'KeyTracks가 없으면 예외 발생
                    Throw New Exception("There is no KeyTracks. Please use keyLED (MIDI Extension).")
                End If

                If DebugFiles Then
                    File.WriteAllText(Application.StartupPath & "\Workspace\ableproj\KeyTracks.xml", str)
                    Debug.WriteLine("Added KeyTracks.xml")
                End If
                Dim notes As XmlNodeList = doc.GetElementsByTagName("MidiNoteEvent") 'Note Event Value
                Dim MidiKey As XmlNodeList = doc.GetElementsByTagName("MidiKey")

                Dim Thow As Integer = 0
                For Each xi As XmlElement In MidiKey
                    Thow += 1
                Next

                Dim noteArr As Integer() = New Integer(Thow) {}
                Dim Noo As Integer = 0
                For Each xi As XmlElement In MidiKey
                    Dim note As Integer = Integer.Parse(xi.GetAttribute("Value"))
                    noteArr(Noo) = note
                    Noo += 1
                Next

                Dim lin As Integer = 0
                For Each xi As XmlElement In notes
                    lin += 1
                Next
                Debug.WriteLine(String.Format("Array lin: {0}", lin))

                Dim TimeArray As Double() = New Double(lin) {} 'Start Time Array.
                Dim DurArray As Integer() = New Integer(lin) {} 'Duration Array.
                Dim FirstKey As Double() = New Double(lin) {} 'First Start Time Array.
                Dim FinalDurArr As Integer() = New Integer(lin) {} 'Last Duration Array after SORT.
                Dim FinNOTEArr As Integer() = New Integer(Thow) {} 'Last MIDIKEY Array after SORT.
                Dim LastNOTEArr As Integer() = New Integer(lin) {} 'keynote를 맞추기 위해 FinNOTE를 순서대로 복제하는 Array.
                Dim YKey As String() = New String(lin) {} 'REAL Duration.
                Dim FinalYKey As String() = New String(lin) {} 'REAL-Duration!
                Dim VelArr As Integer() = New Integer(lin) {} 'Wow! Velocity!!
                Dim FinalVelocity As Integer() = New Integer(lin) {} 'REAL-Velocity.

                Debug.WriteLine("Reading KeyTracks...")

                Dim lil As Integer = 0
                For Each x As XmlElement In notes
                    TimeArray(lil) = Convert.ToDouble(x.GetAttribute("Time")) 'Start Time
                    DurArray(lil) = Integer.Parse(Double.Parse(Mid(x.GetAttribute("Duration"), 1, 5)) * 1000) 'Duration (ms).
                    YKey(lil) = x.GetAttribute("Duration")
                    VelArr(lil) = x.GetAttribute("Velocity")
                    lil += 1
                Next

                Debug.WriteLine("Sorting TimeArray...")
                For i As Integer = 0 To lin - 1 '정렬 전 Key를 조합.
                    FirstKey(i) = TimeArray(i)
                Next
                Array.Sort(TimeArray)

                Debug.WriteLine("Sorting Keys...")

                Dim li As Integer = 0
                Dim RetStr As String = String.Empty '결과 반환 문자열.
                For Each x As XmlElement In notes
                    'Milliseconds, TimeArray와 같은 Array Length를 세팅하는 코드.
                    FinalDurArr(GetIndex(TimeArray, Convert.ToString(FirstKey(li)), FinalDurArr, 0)) = DurArray(li)
                    FinalYKey(GetIndex(TimeArray, Convert.ToString(FirstKey(li)), FinalYKey, 0)) = YKey(li)
                    FinalVelocity(GetIndex(TimeArray, Convert.ToString(FirstKey(li)), FinalVelocity, 0)) = VelArr(li)
                    li += 1
                Next

                Debug.WriteLine("Sorting MidiKeys..." & vbNewLine)

                Dim KeyTrstr As String() = File.ReadAllLines(Application.StartupPath & "\Workspace\ableproj\KeyTracks.xml")
                For i As Integer = 0 To KeyTrstr.Count - 1
                    If Not KeyTrstr(i) = "" Then
                        For q As Integer = 0 To lin - 1
                            If KeyTrstr(i).Contains(String.Format("Duration={0}{1}", Chr(34), FinalYKey(q))) Then
                                If LastNOTEArr(q) = 0 Then
                                    LastNOTEArr(q) = noteArr(i)
                                Else
                                    Continue For
                                End If
                            Else
                                Continue For
                            End If
                        Next
                    End If
                Next

                For i As Integer = 0 To lin - 1
                    Debug.WriteLine(String.Format("Start Time: {0}, Duration: {1}, MidiKey: {2}, Velocity: {3}", TimeArray(i), FinalDurArr(i), LastNOTEArr(i), FinalVelocity(i)))
                Next

                '수정 해야할 사항: XML에 있는 On 메시지와 딜레이 구문 추가 후 딜레이가 끝나면 Off 메시지 구문 추가.
                Dim b As New A2U

                'RetStr = RetStr & vbNewLine & String.Format("o {0} {1} a {2}")

                sto.Stop()
                Debug.WriteLine("Finish... (" & sto.Elapsed.ToString & ")")

                Return RetStr

            Case EachCode.keyLED_MIDEX_1
                Dim result1 As DialogResult = MessageBox.Show("Did you import keyLED (MIDI Extension) Files?", MainProject.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If result1 = DialogResult.Yes Then
                    Dim result2 = InputBox("Please write the LED File Name." & vbNewLine & "", DeveloperMode_Project.Text, "bFINAL.mid")

                    Dim LEDFileName = "Workspace\ableproj\CoLED\" & result2
                    Dim LEDFileC As New MidiFile(LEDFileName, False)
                    Return GetkeyLED_MIDEX2(EachCode.keyLED_MIDEX_1, LEDFileC)

                ElseIf result1 = DialogResult.No Then
                    Return String.Empty
                End If
        End Select

        Return String.Empty
    End Function

    Public Shared Function GetkeyLED_MIDEX2(ByVal LEDArg As EachCode, ByVal keyLEDFile As MidiFile) As String
        'A2UP keyLED Code.
        Select Case LEDArg
            Case EachCode.keyLED_MIDEX_1

                Dim li As Integer = 0
                For Each mdEvent_list In keyLEDFile.Events
                    For Each mdEvent In mdEvent_list
                        li += 1
                    Next
                Next
                Dim str As String() = New String(li + 100000) {}

                Dim i As Integer = 0
                Dim delaycount As Integer = 0
                Dim UniNoteNumberX As Integer 'X
                Dim UniNoteNumberY As Integer 'Y
                For Each mdEvent_list In keyLEDFile.Events
                    For Each mdEvent In mdEvent_list
                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a = DirectCast(mdEvent, NoteOnEvent)
                            Dim b As New A2U

                            If Not delaycount = a.AbsoluteTime Then
                                str(i) = "d " & b.GetNoteDelay(b.keyLED_AC.T_NoteLength1, 120, 192, delaycount - a.AbsoluteTime + Math.Round(a.DeltaTime * 2.604) + Math.Round(a.NoteLength * 2.604))
                                i += 1
                            End If

                            UniNoteNumberX = b.GX_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                            UniNoteNumberY = b.GY_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                            delaycount = a.AbsoluteTime
                            str(i) = "o " & UniNoteNumberX & " " & UniNoteNumberY & " a " & a.Velocity

                        ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                            Dim a = DirectCast(mdEvent, NoteEvent)
                            Dim b As New A2U
                            UniNoteNumberX = b.GX_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                            UniNoteNumberY = b.GY_keyLED(b.keyLED_AC.C_NoteNumber1, a.NoteNumber)
                            str(i) = "f " & UniNoteNumberX & " " & UniNoteNumberY

                        End If
                        i += 1
                    Next
                Next

                Dim strn As String = String.Empty
                For Each stnr As String In str
                    If Not stnr = "" Then
                        strn = strn & stnr & vbNewLine
                    End If
                Next

                If Regex.IsMatch(strn, "8192") Then '8192 = Non-UniNoteNumber
                    strn = strn.Replace(" 8192", "").Trim() 'MC LED Convert.
                    strn = strn.Replace("o ", "o mc ").Trim() 'On MC LED Convert.
                    strn = strn.Replace("f ", "f mc ").Trim() 'Off MC LED Convert.
                End If

                Return strn
        End Select

        Return String.Empty
    End Function

    ''' <summary>
    ''' Get Index from Object.
    ''' </summary>
    ''' <param name="BSoo">1 Object To Get Object's Index.</param>
    ''' <param name="itemName">Object's Item Name.</param>
    ''' <param name="B2Soo">Compare with 1 Object.</param>
    ''' <param name="DefaultVal">Object's Default Value.</param>
    ''' <param name="B2SooAR">1 Object's Length = 2 Object's Length</param>
    ''' <returns></returns>
    Public Shared Function GetIndex(ByVal BSoo As Object, ByVal itemName As String, Optional ByVal B2Soo As Object = Nothing, Optional ByVal DefaultVal As Object = Nothing, Optional ByVal B2SooAR As Boolean = True) As Integer
        For i As Integer = 0 To BSoo.Length - 1
            If itemName.Equals(Convert.ToString(BSoo(i))) = True Then
                If B2SooAR = True Then
                    If B2Soo(i) Is DefaultVal OrElse B2Soo(i) = DefaultVal Then
                        Return i
                    Else
                        Continue For
                    End If
                End If
            End If
        Next

        Return -1
    End Function

    Public Shared Function GetSoundCutting(ByVal CodeArg As EachCode, ByVal XMLPath As String, ByVal SoundsDir As String, ByVal DebugFiles As Boolean) As String
        Select Case CodeArg
            Case EachCode.SlicePoints_1
                Dim doc As New XmlDocument
                Dim NewElementList As XmlNodeList
                doc.Load(XMLPath)
                Dim str As String = String.Empty

                NewElementList = doc.GetElementsByTagName("SlicePoints") 'KeyTracks XML 트랙
                For i As Integer = 0 To NewElementList.Count - 1
                    For q As Integer = 0 To NewElementList(i).ChildNodes.Count - 1 'XML 트랙 아이 추출
                        If NewElementList(i).HasChildNodes Then str = str & NewElementList(i).ChildNodes(q).InnerXml & vbNewLine
                    Next
                Next

                If String.IsNullOrWhiteSpace(str) Then 'SlicePoints가 없으면 예외 발생
                    Throw New Exception("There is no SlicePoints.")
                End If

                If DebugFiles Then
                    File.WriteAllText(Application.StartupPath & "\Workspace\ableproj\SlicePoints.xml", str)
                    Debug.WriteLine("Added SlicePoints.xml")
                End If
        End Select

        Return String.Empty
    End Function

    Private Sub Info_TextBox_DoubleClick(sender As Object, e As EventArgs) Handles Info_TextBox.DoubleClick
        Clipboard.SetText(Info_TextBox.Text)
    End Sub
End Class
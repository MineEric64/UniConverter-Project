﻿Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml

Imports A2UP.A2U.keyLED_MIDEX
Imports NAudio.Midi

Imports MathNet.Numerics.Interpolation

Public Class DeveloperMode_Project
    'Developer Mode에서는 Exception 예외 처리 때 GreatEx가 필요 없습니다.
    '어처피 Developer Mode는 불안정한 모드들을 Beta 기능으로 지원해주기 때문에 GreatEx가 필요 없습니다.

    Public Shared AbletonProjectXML As String = Application.StartupPath & "\Workspace\ableproj\abl_proj.xml"

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

        Dim itm As New List(Of String)()
        itm.AddRange({"File Name", "Chains", "File Version", "Sound Cutting"})
        itm.AddRange({"KeyTracks (keyLED)", "keyLED (MIDI Extension)", "keyLED (MIDEX, MidiFire)", "keyLED (integrated version)"})
        itm.AddRange({"XML Test", "Mp3ToWav", "NewLine Test", "Extract To Xml"})
        itm.AddRange({"LED Extension - Velocity", "Draw Curve Graph", "Interpolate Test", "Velocity Curve Test"})

        Info_ListView.Items.Clear()
        For Each items As String In itm
            Info_ListView.Items.Add(items)
        Next
    End Sub

    Private Sub Project_OpenButton_Click(sender As Object, e As EventArgs) Handles Project_OpenButton.Click
        If ofd_Project.ShowDialog() = DialogResult.OK Then
            Project_FNTextBox.Text = ofd_Project.FileName
            OpenProject()
        End If
    End Sub

    Private Sub OpenProject()
        DeveloperMode_abl_openedproj = True
        DeveloperMode_abl_TmpFileName = AbletonProjectXML

        'Developer Mode Project Infos
        DeveloperMode_abl_FileName = Project_FNTextBox.Text
        DeveloperMode_abl_FileVersion = File.ReadAllLines(DeveloperMode_abl_TmpFileName)(1)

        'Reading Project Infos
        Dim doc As New XmlDocument
        Dim NewElementList As XmlElement
        doc.Load(AbletonProjectXML)
        NewElementList = doc.GetElementsByTagName("Ableton")(0)
        DeveloperMode_abl_FileVersion = NewElementList.GetAttribute("Creator")
    End Sub

    Private Async Sub Info_ListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Info_ListView.SelectedIndexChanged
        If Info_ListView.SelectedItems.Count > 0 Then '이것이 신의 한수... SelectedItem 코드 작성 시 꼭 필요. (invaildArgument 오류)

            Dim SelectedItem As ListViewItem = Info_ListView.SelectedItems(0)

            Select Case SelectedItem.Text
                Case "File Name"
                    Info_TextBox.Text = Path.GetFileNameWithoutExtension(DeveloperMode_abl_FileName)
                Case "Chains"
                    Info_TextBox.Text = GetChainN()
                Case "File Version"
                    Info_TextBox.Text = DeveloperMode_abl_FileVersion
                Case "Sound Cutting"
                    GetSoundCutting(EachCode.SlicePoints_1)
                Case "KeyTracks (keyLED)"
                    Info_TextBox.Text = GetkeyLED(EachCode.keyLED_1)
                Case "keyLED (MIDI Extension)"
                    Info_TextBox.Text = GetkeyLED(EachCode.keyLED_MIDEX_1)
                Case "keyLED (MIDEX, MidiFire)"
                    Info_TextBox.Text = GetkeyLED_MIDEX_v2()
                Case "keyLED (integrated version)"
                    With MainProject
                        Dim err As String = String.Empty

                        Await Task.Run(Sub()
                                .ConvertKeyLEDForMIDEX_v2(AbletonProjectXML, err, True)
                            End Sub)

                        Debug.WriteLineIf(Not String.IsNullOrWhiteSpace(err), err)
                    End With
                Case "XML Test"
                    XmlTest()

                Case "Mp3ToWav"
                    Const mp3File = "C:\Users\gobac\Downloads\Workspace\Hinkik - Time Leaper.mp3"
                    Const wavFile = "C:\Users\gobac\Downloads\Workspace\Hinkik - Time Leaper.wav"

                    If File.Exists(mp3File) Then
                        Sound_Cutting.Mp3ToWav(mp3File, wavFile)
                    Else
                        MessageBox.Show($"'{Path.GetFileName(mp3File)}' sound file doesn't exists on Downloads directory.")
                    End If
                Case "NewLine Test"
                    Info_TextBox.Text = NewLineTest()

                Case "Extract To Xml"
                    Dim ofd As New OpenFileDialog()
                    ofd.Filter = "Ableton Project File|*.als"
                    ofd.Title = "Select Ableton Project File"

                    Dim sfd As New SaveFileDialog()
                    sfd.Filter = "Xml File|*.xml"
                    sfd.Title = "Save Ableton Project To Xml"

                    If ofd.ShowDialog() = DialogResult.OK Then
                        sfd.FileName = $"{Path.GetFileNameWithoutExtension(ofd.FileName)}.xml"

                        If sfd.ShowDialog() = DialogResult.OK Then
                            Await Task.Run(Sub()
                                ExtractAbletonProjectToXml(ofd.FileName, sfd.FileName)
                                           End Sub)

                            MessageBox.Show("Extracted Xml successfully!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If

                Case "LED Extension - Velocity"
                    Dim picture As New Bitmap(128, 128)
                    Dim points As Point()
                    points = DrawVelocityGraph(0, 127, 1.0, 0, 127, "Clip", 0.5)
                    'points = DrawVelocityGraph(1, 51, 0.38, 1, 127)

                    Using gfx As Graphics = Graphics.FromImage(picture)
                        Using brush As New SolidBrush(Color.White)
                            gfx.FillRectangle(brush, 0, 0, picture.Width, picture.Height)
                        End Using
                    End Using

                    For Each point In points
                        picture.SetPixel(Math.Min(Math.Max(point.X, 0), 127), 127 - Math.Min(point.Y, 127), Color.OrangeRed)
                    Next

                    Clipboard.SetImage(picture)
                    MessageBox.Show("Copied Image to Clipboard!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Case "Draw Curve Graph"
                    Dim picture As New Bitmap(1000, 1000)

                    Dim startPoint As New Point(0, 0)
                    Dim endPoint As New Point(picture.Width - 1, picture.Height - 1)
                    Dim points As Point() = {}
                    Dim curvature = 0.0

                    Using gfx As Graphics = Graphics.FromImage(picture)
                        Using brush As New SolidBrush(Color.White)
                            gfx.FillRectangle(brush, 0, 0, picture.Width, picture.Height)
                        End Using
                    End Using

                    If Double.TryParse(InputBox("Please enter the curvature value. (-1.0 ~ 1.0)"), curvature) Then
                        points = DrawCurveGraph_v2(startPoint, endPoint,curvature)
                    End If

                    For Each point In points
                        picture.SetPixel(point.X, endPoint.Y - point.Y, Color.OrangeRed)
                    Next

                    Clipboard.SetImage(picture)
                    MessageBox.Show("Copied Image to Clipboard!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Case "Interpolate Test"
                    Dim p1 As New Point(0, 0)
                    Dim p2 As New Point(499, 499)
                    Dim p3 As New Point(999, 999)

                    InterpolateTest(p1, p2, p3)

                Case "Velocity Curve Test"
                    VelocityCurve.Show()

            End Select
        End If
    End Sub

    Public Shared Sub ExtractAbletonProjectToXml(apfPath As String, destFilePath As String)
        Dim gzipFile As String = Path.GetTempFileName()
        Dim gzipDir As String = $"{Path.GetTempPath()}\{Path.GetRandomFileName()}"

        Directory.CreateDirectory(gzipDir)

        File.Copy(apfPath, gzipFile, True)
        MainProject.ExtractGZip(gzipFile, gzipDir)
        File.Move($"{gzipDir}\{Path.GetFileNameWithoutExtension(gzipFile)}", destFilePath)

        File.Delete(gzipFile)
        Directory.Delete(gzipDir, True)
    End Sub

    ''' <summary>
    ''' Velocity 플러그인 그래프
    ''' </summary>
    ''' <param name="start">Out Low (Y)</param>
    ''' <param name="[end]">Out Hi (Y)</param>
    ''' <param name="drive">Drive Value</param>
    ''' <param name="mode">Clip / Gate / Fixed</param>
    ''' <returns>Point Array</returns>
    Public Shared Function DrawVelocityGraph(start As Integer, [end] As Integer, drive As Double, Optional lowest As Integer = 0, Optional range As Integer = 127, Optional mode As String = "Clip", Optional errVal As Double = 0.5) As Point()
        Dim pointList As New List(Of Point)()

        Dim startPoint As New Point(lowest, start)
        Dim endPoint As New Point(range, [end])

        pointList.AddRange(DrawCurveGraph(startPoint, endPoint, drive, errVal))

        For x = 0 To 127
            If x >= lowest AndAlso x <= range OrElse mode = "Gate" Then
                Continue For
            End If

            Dim p As New Point(x, 0)

            If mode = "Clip" Then
                If Not x >= lowest Then
                    p.Y = [start]
                ElseIf Not x <= range Then
                    p.Y = [end]
                End If

            ElseIf mode = "Fixed" Then
                p.Y = [end]
            End If

            pointList.Add(p)
        Next

        Return pointList.OrderBy(Function(p) p.X * 10 + p.Y).ToArray()
    End Function

    <Obsolete("This method is deprecated, please use 'DrawCurveGraph_v2()' instead.", False)>
    Private Shared Function DrawCurveGraph(startPoint As Point, endPoint As Point, curvature As Double, errVal As Double) As Point()
        Dim middlePoint As New Point((endPoint.X - startPoint.X) / 2, (endPoint.Y - startPoint.Y) / 2)
        middlePoint.X -= Convert.ToInt32(Math.Ceiling((endPoint.X - startPoint.X) * errVal * curvature)) '오차 보정
        middlePoint.Y += Convert.ToInt32(Math.Ceiling((endPoint.Y - startPoint.Y) * errVal * curvature))

        Dim bezierList As New List(Of Point)(ZeichneBezier(6, startPoint, middlePoint, endPoint))
        Dim pointSortFunction As Func(Of Point, Boolean) = Function(p) p.X * 10 + p.Y
        bezierList = bezierList.OrderBy(pointSortFunction).ToList()

        Dim ap As Point = Point.Empty

        For y = 0 To 127 '값 보정
            Dim pointList As List(Of Point) = bezierList.Where(Function(p) p.Y = y).ToList()

            If pointList.Count > 0 Then
                ap = pointList(0)
            Else
                bezierList.Add(New Point(ap.X, ap.Y + 1))
            End If
        Next

        Return bezierList.OrderBy(pointSortFunction).ToArray()
    End Function

    Private Shared Function DrawCurveGraph_v2(startPoint As Point, endPoint As Point, curvature As Double) As Point()
        Dim middlePoint As New Point((endPoint.X - startPoint.X) / 2, (endPoint.Y - startPoint.Y) / 2)
        middlePoint.X -= Convert.ToInt32(Math.Truncate((endPoint.X - startPoint.X) * curvature)) '오차 보정
        middlePoint.Y += Convert.ToInt32(Math.Truncate((endPoint.Y - startPoint.Y) * curvature))

        'https://stackoverflow.com/questions/52433314/extracting-points-coordinatesx-y-from-a-curve-c-sharp
        Using path = New GraphicsPath()
            path.AddCurve({startPoint, middlePoint, endPoint}, 1.0F)

            Using mx = New Matrix(1, 0, 0, 1, 0, 0)
                path.Flatten(mx, 0.00001F)
            End Using

            Dim pointfList As New List(Of PointF)(path.PathPoints)
            Return pointfList.Select(Function(p) Point.Round(p)).ToArray()
        End Using
    End Function

    Private Shared Function ZeichneBezier(n As Integer, p1 As Point, p2 As Point, p3 As Point) As Point()
        Dim pointList As New List(Of Point)()

        If n > 0 Then
            Dim p12 As New Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2)
            Dim p23 As New Point((p2.X + p3.X) / 2, (p2.Y + p3.Y) / 2)
            Dim p123 As New Point((p12.X + p23.X) / 2, (p12.Y + p23.Y) / 2)

            pointList.AddRange(ZeichneBezier(n - 1, p1, p12, p123))
            pointList.AddRange(ZeichneBezier(n - 1, p123, p23, p3))
        Else
            Dim line12 As Point() = DrawLine(p1, p2)
            Dim line23 As Point() = DrawLine(p2, p3)

            pointList.AddRange(line12)
            pointList.AddRange(line23)
        End If

        Return pointList.ToArray()
    End Function

    Private Shared Function DrawLine(p1 As Point, p2 As Point) As Point()
        Dim pointList As New List(Of Point)()
        
        Dim space As Double = (p2.Y - p1.Y) / (p2.X - p1.X)
        Dim yf As Double = Convert.ToDouble(p1.Y)

        For x = p1.X To p2.X
            Dim y As Integer = Convert.ToInt32(Math.Truncate(yf))
            Dim p As New Point(x, y)

            pointList.Add(p)
            yf += space
        Next

        Return pointList.ToArray()
    End Function

    Private Shared Sub InterpolateTest(p1 As Point, p2 As Point, p3 As Point)
        Dim x As Double() = {Convert.ToDouble(p1.X), Convert.ToDouble(p2.X), Convert.ToDouble(p3.X)}
        Dim y As Double() = {Convert.ToDouble(p1.Y), Convert.ToDouble(p2.Y), Convert.ToDouble(p3.Y)}

        Dim polynomial As NevillePolynomialInterpolation = NevillePolynomialInterpolation.Interpolate(x, y)

        For t = 0.0 To 1.0 Step 0.1
            Debug.WriteLine(polynomial.Interpolate(t))
        Next
    End Sub

    Private Shared Function NewLineTest() As String
        Dim sb As New StringBuilder()

        For i = 0 To 499
            sb.Append("Hello World!")
            sb.Append(Environment.NewLine)
        Next
        sb.Length -= 1

        Return sb.ToString()
    End Function

    Private Shared Function GetXpath(ByVal node As XmlNode) As String
        If node.Name = "#document" Then Return String.Empty
        Return GetXpath(node.SelectSingleNode("..")) & "/" + If(node.NodeType = XmlNodeType.Attribute, "@", String.Empty) + node.Name
    End Function

    Private Shared Sub XmlTest()
        Dim doc As New XmlDocument()
        doc.Load("https://docbook.org/xml/4.3b3/test.xml")

        Dim node As XmlNode = doc("article")("table")("tgroup")("tbody")("row")
        Dim entryList As XmlNodeList = node.SelectNodes("entry")

        Debug.WriteLine(entryList.Count)
        For Each x As XmlNode In entryList
            Debug.WriteLine(x.InnerText)
        Next
    End Sub

    Public Shared Function GetkeyLED_MIDEX_v2() As String
        Dim sb As New StringBuilder(255)

        Dim doc As New XmlDocument
        doc.Load(AbletonProjectXML)
        
        For Each x As XmlNode In doc.GetElementsByTagName("MidiEffectBranch")
            Try
                Dim _Test1 As Integer = Integer.Parse(x.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("LomId").GetAttribute("Value"))
                Try
                    Dim _Test2 As Integer = Integer.Parse(x.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiRandom").Item("Choices").Item("Manual").GetAttribute("Value")) '랜덤 MidiEffectRack
                Catch exNN As NullReferenceException
                    Dim _Test3 As String = x.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MxDeviceMidiEffect").Item("PatchSlot").Item("Value").Item("MxDPatchRef").Item("FileRef").Item("Name").GetAttribute("Value") '일반 MidiEffectRack
                End Try
            Catch exN As NullReferenceException
                Dim NextOfNextFound As Boolean = False
                Try
                    Dim _Test4 As Integer = Integer.Parse(x.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiEffectGroupDevice").Item("LomId").GetAttribute("Value"))
                    Try
                        Integer.Parse(x.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiRandom").Item("Choices").Item("Manual").GetAttribute("Value")) '랜덤 코드만 입장 가능.
                    Catch
                        '이것도 Exception 처리 나오면 다른거고, 성공하면 Page임.
                        Dim NextOfNextTest As String = x.Item("DeviceChain").Item("MidiToMidiDeviceChain").Item("Devices").Item("MidiEffectGroupDevice").Item("Branches").Item("MidiEffectBranch").GetAttribute("Id")
                        NextOfNextFound = True
                    End Try
                    Catch exNN As Exception
                        Continue For '다른 무언가이였던것임!
                    End Try
            End Try

            sb.Append(GetXpath(x))
            sb.Append(Environment.NewLine)
        Next

        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Get Ableton Project's Chain Number.
    ''' </summary>
    Public Shared Function GetChainN() As Integer

        Dim ablprj As String = AbletonProjectXML
        Dim doc As New XmlDocument
        Dim setNode As XmlNodeList
        doc.Load(ablprj)
        setNode = doc.GetElementsByTagName("BranchSelectorRange")

        Dim li As Integer = setNode.Count
        Dim chan_ As Integer() = New Integer(li) {}

        Dim iy As Integer = 0
        For Each x As XmlNode In setNode
            'Chain + 1 해주는 이유는 항상 Chain의 기본값이 0이기 때문임. 유니팩에서는 Chain 1이여도 에이블톤에서는 Chain 0임.
            chan_(iy) = Integer.Parse(x.Item("Max").GetAttribute("Value")) + 1
            iy += 1
        Next

        Array.Sort(chan_)
        Array.Reverse(chan_)

        Dim FinalChain As Integer = 0
        For i As Integer = 0 To chan_.Count - 1
            If chan_(i) < 9 AndAlso chan_(i) > 0 Then
                FinalChain = chan_(i)
                Exit For
            End If
        Next

        Return FinalChain
    End Function

    ''' <summary>
    ''' Converting keyLED with XML2keyLED.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetkeyLED(ByVal LEDArg As EachCode) As String

        Select Case LEDArg
            Case EachCode.keyLED_1
                Dim sto As New Stopwatch
                sto.Start()

                'Dim Xpath As String = "/Ableton/LiveSet"
                Dim doc As New XmlDocument
                Dim NewElementList As XmlNodeList
                doc.Load(AbletonProjectXML)
                Dim str As String = String.Empty

                '와... 진짜 LED 구현하느라 완전 힘들었다........ ㅠㅠ
                'KeyTrack도 있음.
                NewElementList = doc.GetElementsByTagName("KeyTracks") 'KeyTracks XML 트랙
                For i As Integer = 0 To NewElementList.Count - 1
                    For q As Integer = 0 To NewElementList(i).ChildNodes.Count - 1 'XML 트랙 아이 추출
                        If NewElementList(i).HasChildNodes Then str = str & NewElementList(i).ChildNodes(q).InnerXml & vbNewLine
                    Next
                Next

                If String.IsNullOrWhiteSpace(str) Then 'KeyTracks가 없으면 예외 발생
                    Throw New Exception("There is no KeyTracks. Please use keyLED (MIDI Extension).")
                End If

                File.WriteAllText(Application.StartupPath & "\Workspace\ableproj\KeyTracks.xml", str)
                    Debug.WriteLine("Added KeyTracks.xml")

                Dim notes As XmlNodeList = doc.GetElementsByTagName("MidiNoteEvent") 'Note Event Value
                Dim MidiKey As XmlNodeList = doc.GetElementsByTagName("MidiKey")

                Dim Thow As Integer = MidiKey.Count - 1
                Dim noteArr As Integer() = New Integer(Thow) {}
                Dim Noo As Integer = 0
                For Each xi As XmlElement In MidiKey
                    Dim note As Integer = Integer.Parse(xi.GetAttribute("Value"))
                    noteArr(Noo) = note
                    Noo += 1
                Next

                Dim lin As Integer = notes.Count - 1
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

                '수정 해야할 사항: XML에 있는 On 메시지와 딜레이 구문 추가 후 딜레이가 끝나면 Off 메시지 구문 추가.
                For i As Integer = 0 To lin - 1

                    Dim MidiKey_X As Integer = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, LastNOTEArr(i))
                    Dim MidiKey_Y As Integer = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, LastNOTEArr(i))
                    RetStr = RetStr & vbNewLine & String.Format("o {0} {1} a {2}", MidiKey_X, MidiKey_Y, FinalVelocity(i))
                    If Not FinalDurArr(i) = 0 Then
                        RetStr = RetStr & vbNewLine & String.Format("d {0}", FinalDurArr(i))
                    End If

                    Debug.WriteLine(String.Format("Start Time: {0}, Duration: {1}, MidiKey: {2}, Velocity: {3}", TimeArray(i), FinalDurArr(i), LastNOTEArr(i), FinalVelocity(i)))
                Next

                sto.Stop()
                Debug.WriteLine("Finish... (" & sto.Elapsed.ToString & ")")

                Return RetStr

            Case EachCode.keyLED_MIDEX_1
                Dim result1 As DialogResult = MessageBox.Show("Did you import keyLED (MIDI Extension) Files?", MainProject.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If result1 = DialogResult.Yes Then
                    Dim result2 = InputBox("Please write the LED File Name." & vbNewLine & "", DeveloperMode_Project.Text, "bFINAL.mid")

                    Dim LEDFileName = "Workspace\ableproj\CoLED\" & result2
                    Dim LEDFile As New MidiFile(LEDFileName, False)
                    Return GetkeyLED_MIDEX2(EachCode.keyLED_MIDEX_1, LEDFile)

                ElseIf result1 = DialogResult.No Then
                    Return String.Empty
                End If
        End Select

        Return String.Empty
    End Function

    Public Shared Function GetkeyLED_MIDEX2(ByVal LEDArg As EachCode, ByVal keyLED As MidiFile) As String
        'A2UP keyLED Code.
        Select Case LEDArg
            Case EachCode.keyLED_MIDEX_1

                Dim str As String = String.Empty
                Dim delaycount As Long = 0

                Dim UniNoteNumberX As Integer 'X
                Dim UniNoteNumberY As Integer 'Y
                For Each mdEvent_list In keyLED.Events
                    For Each mdEvent In mdEvent_list
                        If mdEvent.CommandCode = MidiCommandCode.NoteOn Then
                            Dim a As NoteOnEvent = DirectCast(mdEvent, NoteOnEvent)
                            Dim bpm As New TempoEvent(500000, a.AbsoluteTime)

                            If Not delaycount = a.AbsoluteTime OrElse Not a.DeltaTime = 0 Then
                                str = str & vbNewLine & "d " & GetNoteDelay(keyLED_NoteEvents.NoteLength_2, bpm.Tempo, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delaycount)
                            End If

                            UniNoteNumberX = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber)
                            UniNoteNumberY = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber)
                            delaycount = a.AbsoluteTime

                            If UniNoteNumberX = 0 AndAlso UniNoteNumberY = 0 Then
                                Debug.WriteLine("Unknown Note Number. [ Note: " & a.NoteNumber & " ]")
                                Continue For
                            End If

                            If Not UniNoteNumberX = -8192 Then
                                str = str & vbNewLine & "o " & UniNoteNumberX & " " & UniNoteNumberY & " a " & a.Velocity
                            Else
                                str = str & vbNewLine & "o mc " & UniNoteNumberY & " a " & a.Velocity
                            End If

                        ElseIf mdEvent.CommandCode = MidiCommandCode.NoteOff Then

                            Dim a As NoteEvent = DirectCast(mdEvent, NoteEvent)
                            Dim bpm As New TempoEvent(500000, a.AbsoluteTime)

                            If Not delaycount = a.AbsoluteTime OrElse Not a.DeltaTime = 0 Then
                                str = str & vbNewLine & "d " & GetNoteDelay(keyLED_NoteEvents.NoteLength_2, bpm.Tempo, keyLED.DeltaTicksPerQuarterNote, a.AbsoluteTime - delaycount)
                            End If

                            UniNoteNumberX = GX_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber)
                            UniNoteNumberY = GY_keyLED(keyLED_NoteEvents.NoteNumber_DrumRackLayout, a.NoteNumber)
                            delaycount = a.AbsoluteTime

                            If UniNoteNumberX = 0 AndAlso UniNoteNumberY = 0 Then
                                Debug.WriteLine("Unknown Note Number. [ Note: " & a.NoteNumber & " ]")
                                Continue For
                            End If

                            If Not UniNoteNumberX = -8192 Then
                                str = str & vbNewLine & "f " & UniNoteNumberX & " " & UniNoteNumberY
                            Else
                                str = str & vbNewLine & "f mc " & UniNoteNumberY
                            End If

                        End If
                    Next
                Next

                If Regex.IsMatch(str, "-8192") Then '-8192 = Non-UniNoteNumber
                    str = str.Replace("o -8192 ", "o mc ").Trim() 'ON MC LED Convert.
                    str = str.Replace("f -8192 ", "f mc ").Trim() 'OFF MC LED Convert.
                End If

                Return str
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

    Public Shared Function GetSoundCutting(ByVal CodeArg As EachCode) As String
        Select Case CodeArg
            Case EachCode.SlicePoints_1
                Dim doc As New XmlDocument
                Dim NewElementList As XmlNodeList
                doc.Load(AbletonProjectXML)
                Dim str As String = String.Empty

                NewElementList = doc.GetElementsByTagName("SlicePoints") 'KeyTracks XML 트랙
                For i As Integer = 0 To NewElementList.Count - 1 'XML 트랙 아이 추출
                    str = str & NewElementList(i).InnerXml & vbNewLine
                Next

                If String.IsNullOrWhiteSpace(str) Then 'SlicePoints가 없으면 예외 발생
                    Throw New Exception("There is no SlicePoints.")
                End If

                File.WriteAllText(Application.StartupPath & "\Workspace\ableproj\SlicePoints.xml", str)
                Debug.WriteLine("Added SlicePoints.xml")
        End Select

        Return String.Empty
    End Function

    Private Sub Info_TextBox_DoubleClick(sender As Object, e As EventArgs) Handles Info_TextBox.DoubleClick
        Clipboard.SetText(Info_TextBox.Text)
    End Sub
End Class
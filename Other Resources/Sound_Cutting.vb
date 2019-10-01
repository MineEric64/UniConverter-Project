Imports NAudio.Wave
Imports System.IO
Imports System.Threading
Imports System.Globalization
Public Class Sound_Cutting
    'http://www.vbforums.com/showthread.php?620394-WIP-Double-TrackBar&p=4057480#post4057480
    'Dim a As NAudio.Gui.WaveViewer


    Private Sub CutButton_Click(sender As Object, e As EventArgs) Handles CutButton.Click
        ThreadPool.QueueUserWorkItem(AddressOf CutSound)
    End Sub

    Private Sub CutSound()
        'Dim fname() As String
        Dim str() As String
        Me.Invoke(Sub()
                      'fname = Me.RichTextBox1.Lines()
                  End Sub)
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        Me.Invoke(Sub()
                      Me.lblstat.Text = "Preparing..."
                      Me.CutButton.Enabled = False
                      Me.RichTextBox1.Enabled = False
                      str = Me.RichTextBox1.Lines()
                      Me.txtSource.Enabled = False
                      Me.btnSelectSource.Enabled = False
                  End Sub)

        Dim mp3Path = Me.txtSource.Text
        Try
            Me.Invoke(Sub()
                          Me.lblstat.Text = "Cutting (0 / " & str.Count & ")"
                          Me.ProgressBar1.Value = 0
                          Me.ProgressBar1.Maximum = str.Count
                      End Sub)
            For i = 0 To str.Count - 1
                Dim isErr = False
                Me.Invoke(Sub()
                              Me.lblstat.Text = "Cutting (" & i & " / " & str.Count & ")"
                              Me.ProgressBar1.Value += 1
                          End Sub)
                Dim sp() As String
                Try
                    sp = str(i).Split(" ") '시작위치 종료위치(or[초]s) 파일명(확장자제외: 자동으로 wav로 저장하기!)(auto 일 경우 자동으로 [i].tr.wav로 저장)
                    If (sp.Count <> 3) Then
                        isErr = True
                        Me.Invoke(Sub()
                                      MessageBox.Show("Can't be more or less then 3 parameters on line " & i + 1 & "! We will just continue.", "Wrong parameter counter", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                  End Sub)
                    End If
                Catch
                    '잘리지 않음.
                    isErr = True
                End Try
                If (isErr = False) Then
                    'Dim a As New NAudio.Wave.WaveFileReader("060.wav")
                    'Me.WavView.WaveStream = a
                    Dim star As String = sp(0)


                    Dim outputPath As String
                    If (sp(2) = "auto") Then
                        '수정 해야할 사항: 사운드 파일 이름 asd파일이나 alc파일로 추출.
                        outputPath = "Workspace\unipack\sounds\tr_" & i & ".wav"
                    Else
                        '수정 해야할 사항: 사운드 파일 이름 asd파일이나 alc파일로 추출.
                        outputPath = "Workspace\unipack\sounds\" & sp(2) & ".wav"
                    End If
                    If (sp(1).Substring(sp(1).Length - 1) = "s") Then
                        sp(1) = TimeSpan.Parse(sp(0)).Add(TimeSpan.FromSeconds(sp(1).Substring(0, sp(1).Length - 1))).ToString '시간 더하기 (지정된 초만큼)
                    End If
                    TrimMp3(mp3Path, outputPath, TimeSpan.Parse(sp(0)), TimeSpan.Parse(sp(1)))
                    ' fname(i) = outputPath
                End If
            Next
            If AfterTabCheckBox.Checked = True Then
                If (My.Computer.FileSystem.DirectoryExists("Workspace\unipack\sounds") = True) Then
                    My.Computer.FileSystem.DeleteDirectory("Workspace\unipack\sounds", FileIO.DeleteDirectoryOption.DeleteAllContents)
                End If

                My.Computer.FileSystem.CreateDirectory("Workspace\unipack\sounds")
                For Each foundFile As String In My.Computer.FileSystem.GetFiles("Workspace\unipack\sounds\", FileIO.SearchOption.SearchTopLevelOnly, "*.wav")
                    File.Copy(foundFile, Application.StartupPath + "Workspace\unipack\sounds\" & foundFile.Split("\").Last, True)

                    Dim itm As New ListViewItem(New String() {Path.GetFileName(foundFile), foundFile})
                Next
                If MainProject.abl_openedsnd = False Then MainProject.abl_openedsnd = True
            End If
        Catch
            MessageBox.Show("An error occured! Message: " & Err.Description, "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Warning)


            Me.Invoke(Sub()
                          Me.lblstat.Text = "Error"
                          Me.RichTextBox1.Enabled = True
                          Me.CutButton.Enabled = True
                          Me.txtSource.Enabled = True
                          Me.btnSelectSource.Enabled = True
                      End Sub)
            Exit Sub
        End Try
        '       Current Culture: en-US
        '       6 --> 6.00:00:00
        '       6:12 --> 06:12:00
        '       6:12:14 --> 06:12:14
        '       6:12:14:45 --> 6.12:14:45
        '       6.12:14:45 --> 6.12:14:45
        '       6:12:14:45.3448 --> 6.12:14:45.3448000
        '       6:12:14:45,3448: Bad Format
        '       6:34:14:45: Overflow
        Me.Invoke(Sub()
                      Me.lblstat.Text = "Ready"
                      Me.RichTextBox1.Enabled = True
                      Me.CutButton.Enabled = True
                      Me.txtSource.Enabled = True
                      Me.btnSelectSource.Enabled = True
                  End Sub)
    End Sub

    'http://stackoverflow.com/questions/7932951/trimming-mp3-files-using-naudio
    Private Sub TrimMp3(inputPath As String, outputPath As String, begin As TimeSpan?, [end] As TimeSpan?)
        If begin.HasValue AndAlso [end].HasValue AndAlso begin > [end] Then
            Throw New Exception("Script Error. End sound path must be greater than first!")
        End If

        Using reader = New Mp3FileReader(inputPath)
            Using writer = File.Create(outputPath)
                Dim frame As Mp3Frame = Nothing
                While InlineAssignHelper(frame, reader.ReadNextFrame()) IsNot Nothing
                    If reader.CurrentTime >= begin OrElse Not begin.HasValue Then
                        If reader.CurrentTime <= [end] OrElse Not [end].HasValue Then
                            writer.Write(frame.RawData, 0, frame.RawData.Length)
                        Else
                            Exit While
                        End If
                    End If
                End While
            End Using
        End Using
        Mp3ToWav(outputPath, outputPath)

    End Sub
    Private Sub TrimMp3_TESTTRIM(inputPath As String, outputPath As String, begin As System.Nullable(Of TimeSpan), [end] As System.Nullable(Of TimeSpan))
        If begin.HasValue AndAlso [end].HasValue AndAlso begin > [end] Then
            Throw New Exception("Script Error. End sound path must be greater than first!")
        End If

        Using reader = New Mp3FileReader(inputPath)
            Using writer = File.Create(outputPath)
                Dim frame As Mp3Frame = Nothing
                While (InlineAssignHelper(frame, reader.ReadNextFrame())) IsNot Nothing
                    If reader.CurrentTime >= begin OrElse Not begin.HasValue Then
                        If reader.CurrentTime <= [end] OrElse Not [end].HasValue Then
                            writer.Write(frame.RawData, 0, frame.RawData.Length)
                        Else
                            Exit While
                        End If
                    End If
                End While
            End Using
        End Using
        Mp3ToWav(outputPath, "TmpSound\soundconverted.wav")

    End Sub
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function

    Public Shared Sub Mp3ToWav(mp3File As String, outputFile As String)
        Try
            Using reader As New Mp3FileReader(mp3File)
                Using pcmStream As WaveStream = WaveFormatConversionStream.CreatePcmStream(reader)
                    WaveFileWriter.CreateWaveFile(outputFile, pcmStream)

                End Using

            End Using
        Catch
        End Try
    End Sub

    Private Sub btnSelectSource_Click(sender As Object, e As EventArgs) Handles btnSelectSource.Click
        If (Me.ofdFile.ShowDialog = Windows.Forms.DialogResult.OK) Then
            Me.txtSource.Text = Me.ofdFile.FileName
        End If
    End Sub

    Private Sub txtSource_TextChanged(sender As Object, e As EventArgs) Handles txtSource.TextChanged
        If (My.Computer.FileSystem.FileExists(Me.txtSource.Text) = True) Then
            Dim a As New Mp3FileReader(Me.txtSource.Text)
            Me.lblSoundLength.Text = "Length: " & a.TotalTime.ToString
        End If
    End Sub


    Private Sub DataGridView1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles SoundCutControl.CellClick
        'check it the button column being clicked, and check they are not clicking the column heading

        'If e.ColumnIndex = 4 And e.RowIndex >= 0 Then
        '    'SoundCutControl.CurrentCell.RowIndex
        '    'SoundCutControl.Rows.RemoveAt(e.RowIndex)

        'End If

        If e.ColumnIndex = 3 And e.RowIndex >= 0 Then

            SoundCutControl.Rows(e.RowIndex).Cells(2).[ReadOnly] = Not SoundCutControl.Rows(e.RowIndex).Cells(3).EditedFormattedValue
            'Value 대신 EditedFormattedValue 사용해야 실시간 트랙이 가능 (ReadOnly)
            If (Not SoundCutControl.Rows(e.RowIndex).Cells(3).EditedFormattedValue = True) Then
                SoundCutControl.Item(2, e.RowIndex).Style.BackColor = Color.Gray
                SoundCutControl.Rows(e.RowIndex).Cells(2).Value = ""
            Else
                SoundCutControl.Item(2, e.RowIndex).Style.BackColor = SystemColors.Window
            End If

        ElseIf e.ColumnIndex = 4 And e.RowIndex >= 0 Then
            'TEST'
            Try
                Dim endPos As String = SoundCutControl.Rows(e.RowIndex).Cells(1).Value
                If (endPos.Substring(endPos.Length - 1) = "s") Then
                    endPos = TimeSpan.Parse(SoundCutControl.Rows(e.RowIndex).Cells(0).Value).Add(TimeSpan.FromSeconds("Workspace\unipack\sounds\" & SoundCutControl.Rows(e.RowIndex).Cells(1).Value.Substring(0, endPos.Length - 1))).ToString '시간 더하기 (지정된 초만큼)
                End If
                TrimMp3_TESTTRIM(Me.txtSource.Text, "TmpSound\testtrim.wav", TimeSpan.Parse(SoundCutControl.Rows(e.RowIndex).Cells(0).Value), TimeSpan.Parse(endPos))
            Catch ex As Exception
                MessageBox.Show("There is something wrong... During trimming sound. Message: " & Err.Description, "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Try
                My.Computer.Audio.Play("TmpSound\soundconverted.wav", AudioPlayMode.Background)

            Catch ex As Exception
                MessageBox.Show("There is something wrong... During playing sound. Message: " & Err.Description, "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnAddRow_Click(sender As Object, e As EventArgs) Handles btnAddRow.Click
        SoundCutControl.Rows.Add({"0:0:0.0000000", "0:0:0.1000000", "ex", False, "Test"})
    End Sub

    Public Sub New()

        ' 이 호출은 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Private Sub CuttingSound_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtSource.Text = MainProject.ofd_FileName
    End Sub
End Class
Imports System.IO
Imports NAudio.Wave

Public Class Sound_Cutting
    'http://stackoverflow.com/questions/7932951/trimming-mp3-files-using-naudio
    Public Shared Sub TrimMp3(inputPath As String, outputPath As String, begin As TimeSpan?, [end] As TimeSpan?)
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

    'https://markheath.net/post/trimming-wav-file-using-naudio
    Public Shared Sub TrimWavFile(ByVal inPath As String, ByVal outPath As String, ByVal cutFromStart As TimeSpan, ByVal cutFromEnd As TimeSpan)
        Using reader As WaveFileReader = New WaveFileReader(inPath)
            Using writer As WaveFileWriter = New WaveFileWriter(outPath, reader.WaveFormat)

                Dim bytesPerMillisecond As Integer = reader.WaveFormat.AverageBytesPerSecond / 1000
                Dim startPos = CInt(cutFromStart.TotalMilliseconds) * bytesPerMillisecond
                startPos = startPos - startPos Mod reader.WaveFormat.BlockAlign
                Dim endBytes = CInt(cutFromEnd.TotalMilliseconds) * bytesPerMillisecond
                endBytes = endBytes - endBytes Mod reader.WaveFormat.BlockAlign
                Dim endPos = CInt(reader.Length) - endBytes
                TrimWavFile(reader, writer, startPos, endBytes)

            End Using
        End Using
    End Sub

    Private Shared Sub TrimWavFile(ByVal reader As WaveFileReader, ByVal writer As WaveFileWriter, ByVal startPos As Integer, ByVal endPos As Integer)
        reader.Position = startPos
        Dim buffer = New Byte(1023) {}
        While reader.Position < endPos
            If reader.Position = reader.Length Then
                Exit While
            End If

            Dim bytesRequired = CInt(endPos - reader.Position)
            If bytesRequired > 0 Then
                Dim bytesToRead As Integer
                Select Case reader.WaveFormat.BitsPerSample
                    Case 16
                        bytesToRead = Math.Min(bytesRequired, buffer.Length)
                    Case 24
                        bytesToRead = Math.Min(bytesRequired, buffer.Length - (buffer.Length Mod reader.WaveFormat.BlockAlign))
                End Select
                Dim bytesRead As Integer = reader.Read(buffer, 0, bytesToRead)

                If bytesRead > 0 Then
                    writer.Write(buffer, 0, bytesRead)
                End If
            End If
        End While
    End Sub

    '완벽한 코드.
    Private Sub SoundTest(ByVal inputPath As String)
        Dim outputPath As String = SetFileName(inputPath, Path.GetFileNameWithoutExtension(inputPath) & "_Trim.wav")
        Dim startTime As TimeSpan = TimeSpan.Parse("0:0:10.0000000")
        Dim endTime As TimeSpan = TimeSpan.Parse("0:0:20.0000000")
        TrimWavFile(inputPath, outputPath, startTime, endTime)

        'Mp3 파일은 변환 과정을 거쳐야 하므로 코드가 꼬인다.
        'TrimMp3(txtSource.Text, outputPath, startTime, endTime)
        'Mp3ToWav(outputPath, outputPath.Replace(".mp3", ".wav"))
    End Sub

    Public Function SetFileName(ByVal FilePath As String, ByVal name As String) As String
        Dim rName As String
        Dim filename As String = FilePath.Split(Path.DirectorySeparatorChar).Last()

        rName = FilePath.Replace(filename, name)
        Return rName
    End Function
End Class

Public Class MidiExtensionSave
    Public Speed As Integer 'New Tempo
    Public BPM As Integer 'Clip Tempo
    Public MidiName As String

    Sub New(speed As Integer, bpm As Integer, midiName As String)
        Me.Speed = speed
        Me.BPM = bpm
        Me.MidiName = midiName
    End Sub
End Class

Public Class MidiExtensionSave
    Public Speed As Integer 'New Tempo
    Public BPM As Integer 'Clip Tempo
    Public MidiName As String

    Sub New()
        Me.Speed = 100
        Me.BPM = 120
        Me.MidiName = String.Empty
    End Sub
End Class

Public Class MultiMapping
    Public Count As Integer

    Public NoteNumberMin As Integer
    Public NoteNumberMax As Integer

    Sub New(count As Integer, noteNumberMin As Integer, noteNumberMax As Integer)
        Me.Count = count
        
        Me.NoteNumberMin = noteNumberMin
        Me.NoteNumberMax = noteNumberMax
    End Sub
End Class

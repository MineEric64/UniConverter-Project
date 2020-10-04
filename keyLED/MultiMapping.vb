Public Class MultiMapping
    Public Count As Integer
    Public CurrentCount As Integer
    Public IsStrange As Boolean

    Public NoteNumberMin As Integer
    Public NoteNumberMax As Integer

    Public Indent As Integer

    Sub New(count As Integer, currentCount As Integer, noteNumberMin As Integer, noteNumberMax As Integer, indent As Integer)
        Me.Count = count
        Me.CurrentCount = currentCount
        Me.IsStrange = False
        
        Me.NoteNumberMin = noteNumberMin
        Me.NoteNumberMax = noteNumberMax
        Me.Indent = indent
    End Sub

    Public Shared ReadOnly Property Empty As MultiMapping
        Get
            Return New MultiMapping(0, 0, 0, 0, 0)
        End Get
    End Property

    Public ReadOnly Property IsEmpty As Boolean
        Get
            Return Me.Count = 0 AndAlso Me.CurrentCount = 0 AndAlso Me.NoteNumberMin = 0 AndAlso Me.NoteNumberMax = 0 AndAlso Me.Indent = 0
        End Get
    End Property
End Class

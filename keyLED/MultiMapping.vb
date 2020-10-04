Public Class MultiMapping
    Public Count As Integer
    Public CurrentCount As Integer
    Public IsStrange As Boolean

    Public Chain As Footprint(Of Integer)
    Public NoteNumber As Footprint(Of Integer)

    Public Indent As Integer

    Sub New(count As Integer, currentCount As Integer, noteNumber As Footprint(Of Integer), indent As Integer)
        Me.Count = count
        Me.CurrentCount = currentCount
        Me.IsStrange = False
        
        Me.Chain = New Footprint(Of Integer)(0, 0)
        Me.NoteNumber = noteNumber
        Me.Indent = indent
    End Sub

    Public Shared ReadOnly Property Empty As MultiMapping
        Get
            Return New MultiMapping(0, 0, New Footprint(Of Integer)(0, 0), 0)
        End Get
    End Property

    Public ReadOnly Property IsEmpty As Boolean
        Get
            Return Me.Count = 0 AndAlso Me.CurrentCount = 0 AndAlso Me.NoteNumber.Start = 0 AndAlso Me.NoteNumber.End = 0 AndAlso Me.Indent = 0
        End Get
    End Property
End Class

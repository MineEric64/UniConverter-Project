Public Class Footprint(Of T)
    Public Property Start As T
    Public Property [End] As T

    Sub New()

    End Sub

    Sub New(start As T, [end] As T)
        Me.Start = start
        Me.End = [end]
    End Sub
End Class

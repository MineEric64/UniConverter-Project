Public Class KeyLEDStructure
    Public Chain As Integer
    Public X As Integer
    Public Y As Integer
    Public LoopNumber As Integer

    Public Script As String

    Sub New(chain As Integer, x As Integer, y As Integer, loopNumber As Integer, script As String)
        Me.Chain = chain
        Me.X = x
        Me.Y = y
        Me.LoopNumber = loopNumber

        Me.Script = script
    End Sub
End Class

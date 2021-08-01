Public Class KeySoundArgument
    Public Property Chain As Integer
    Public Property X As Integer
    Public Property Y As Integer
    Public Property Index As Integer
    Public Property LoopNumber As Integer
    Public Property SoundName As String

    Sub New(chain_ As Integer, x_ As Integer, y_ As Integer, index_ As Integer, loopNumber_ As Integer, soundName_ As String)
        Chain = chain_
        X = x_
        Y = y_
        Index = index_
        LoopNumber = loopNumber_
        SoundName = soundName_
    End Sub
End Class

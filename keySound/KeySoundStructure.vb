Imports System.Text

Public Class KeySoundStructure
    Public Chain As Integer
    Public X As Integer
    Public Y As Integer
    Public FileName As String
    Public LoopNumber As Integer
    Public Wormhole As Integer

    Public Sub Initialize(chain As Integer, x As Integer, y As Integer, fileName As String, loopNumber As Integer, wormhole As Integer)
        Me.Chain = chain
        Me.X = x
        Me.Y = y
        Me.FileName = fileName
        Me.LoopNumber = loopNumber
        Me.Wormhole = wormhole
    End Sub

    Sub New(chain As Integer, x As Integer, y As Integer, fileName As String)
        Initialize(chain, x, y, fileName, 1, -1)
    End Sub

    Sub New(chain As Integer, p As Point, fileName As String)
        Initialize(chain, p.X, p.Y, fileName, 1, -1)
    End Sub

    Sub New(chain As Integer, x As Integer, y As Integer, fileName As String, loopNumber As Integer)
        Initialize(chain, x, y, fileName, loopNumber, -1)
    End Sub

    Sub New(chain As Integer, p As Point, fileName As String, loopNumber As Integer)
        Initialize(chain, p.X, p.Y, fileName, loopNumber, -1)
    End Sub

    Sub New(chain As Integer, x As Integer, y As Integer, fileName As String, loopNumber As Integer, wormhole As Integer)
        Initialize(chain, x, y, fileName, loopNumber, wormhole)
    End Sub

    Sub New(chain As Integer, p As Point, fileName As String, loopNumber As Integer, wormhole As Integer)
        Initialize(chain, p.X, p.Y, fileName, loopNumber, wormhole)
    End Sub

    Public Overrides Function ToString() As String
        Dim key As New StringBuilder(100)
        key.Append(chain)
        key.Append(" "C)
        key.Append(X)
        key.Append(" "C)
        key.Append(Y)
        key.Append(" "C)
        key.Append(FileName)
        
        If LoopNumber <> 1 OrElse Wormhole <> -1 Then
            key.Append(" "C)
            key.Append(LoopNumber)
        End If
        If Wormhole <> -1 Then
            key.Append(" "C)
            key.Append(Wormhole)
        End If

        Return key.ToString()
    End Function
End Class

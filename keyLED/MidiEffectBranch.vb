Imports System.Xml

Public Class MidiEffectBranch
    Public Id As Integer
    Public Node As XmlNode
    Public MidiEffectRack As XmlNode 'MidiEffectGroupDevice In Xml

    Sub New(id As Integer, node As XmlNode)
        Me.Id = id
        Me.Node = node
    End Sub
End Class

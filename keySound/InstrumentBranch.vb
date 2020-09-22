Imports System.Xml

Public Class InstrumentBranch
    Public Id As Integer
    Public Node As XmlNode
    Public InstrumentRack As XmlNode 'InstrumentGroupDevice In Xml

    Sub New(id As Integer, node As XmlNode)
        Me.Id = id
        Me.Node = node
    End Sub
End Class

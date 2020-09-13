Imports System.Xml

Public Class LEDNodeList
    Public Name As String
    Public Id As Integer

    Public ReadOnly NodeList As List(Of LEDNodeList)
    Public Node As XmlNode

    Sub New(name As String)
        Me.Name = name
        Me.Id = -1
        Me.NodeList = New List(Of LEDNodeList)
    End Sub

    Sub New(name As String, id As Integer)
        Me.Name = name
        Me.Id = id
        Me.NodeList = New List(Of LEDNodeList)
    End Sub

    Sub New(name As String, id As Integer, node As XmlNode)
        Me.Name = name
        Me.Id = id
        Me.Node = node
        Me.NodeList = New List(Of LEDNodeList)
    End Sub
End Class

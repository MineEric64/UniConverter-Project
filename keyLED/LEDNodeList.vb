Imports System.Xml

Public Class LEDNodeList
    Public Xpath As String
    Public Id As Integer

    Public ReadOnly NodeList As List(Of LEDNodeList)
    Public Node As XmlNode

    Sub New(xpath As String)
        Me.Xpath = xpath
        Me.Id = -1
        Me.NodeList = New List(Of LEDNodeList)
    End Sub

    Sub New(xpath As String, id As Integer)
        Me.Xpath = xpath
        Me.Id = id
        Me.NodeList = New List(Of LEDNodeList)
    End Sub

    Sub New(xpath As String, id As Integer, node As XmlNode)
        Me.Xpath = xpath
        Me.Id = id
        Me.Node = node
        Me.NodeList = New List(Of LEDNodeList)
    End Sub
End Class

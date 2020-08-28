Imports System.Xml

Public Class LEDNodeList
    Public Xpath As String
    Public ReadOnly NodeList As List(Of LEDNodeList)
    Public ReadOnly Node As XmlNode

    Sub New(Xpath As String, Node As XmlNode)
        Me.NodeList = New List(Of LEDNodeList)
        Me.Node = Node
        Me.Xpath = Xpath
    End Sub
End Class

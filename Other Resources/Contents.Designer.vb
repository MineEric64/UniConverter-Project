﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Class Contents
        
        Private Shared resourceMan As Global.System.Resources.ResourceManager
        
        Private Shared resourceCulture As Global.System.Globalization.CultureInfo
        
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
        Friend Sub New()
            MyBase.New
        End Sub
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("UniConverter.Contents", GetType(Contents).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Converted LED sucessfully!.
        '''</summary>
        Friend Shared ReadOnly Property LED_Converted() As String
            Get
                Return ResourceManager.GetString("LED_Converted", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Converting Ableton LED to UniPack LED....
        '''</summary>
        Friend Shared ReadOnly Property LED_Converting() As String
            Get
                Return ResourceManager.GetString("LED_Converting", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Converting LED... ({0} / {1}).
        '''</summary>
        Friend Shared ReadOnly Property LED_Converting_Title() As String
            Get
                Return ResourceManager.GetString("LED_Converting_Title", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Couldn&apos;t find any LED in this project..
        '''</summary>
        Friend Shared ReadOnly Property LED_Not_Found() As String
            Get
                Return ResourceManager.GetString("LED_Not_Found", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to This project&apos;s led made using Midi Extension Plugin.
        '''But Couldn&apos;t find Midi Extension data file.
        '''
        '''Would you like to select the data file and continue?
        '''(Often data file is in LED File (.mid extension) folder, it doesn&apos;t have extension also its name contains &apos;save&apos;).
        '''</summary>
        Friend Shared ReadOnly Property LED_Save_File_Not_Found() As String
            Get
                Return ResourceManager.GetString("LED_Save_File_Not_Found", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Verifying LED... ({0} / {1}).
        '''</summary>
        Friend Shared ReadOnly Property LED_Verifying() As String
            Get
                Return ResourceManager.GetString("LED_Verifying", resourceCulture)
            End Get
        End Property
    End Class
End Namespace

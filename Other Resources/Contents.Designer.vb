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
    Public Class Contents
        
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
        Public Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
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
        Public Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Hello, World!.
        '''</summary>
        Public Shared ReadOnly Property HelloWorld() As String
            Get
                Return ResourceManager.GetString("HelloWorld", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Created KeyLED successfully!.
        '''</summary>
        Public Shared ReadOnly Property KeyLED_Created() As String
            Get
                Return ResourceManager.GetString("KeyLED_Created", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Creating KeyLED... ({0} / {1}).
        '''</summary>
        Public Shared ReadOnly Property KeyLED_Creating() As String
            Get
                Return ResourceManager.GetString("KeyLED_Creating", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Creating KeyLED.
        '''</summary>
        Public Shared ReadOnly Property KeyLED_Creating_Title() As String
            Get
                Return ResourceManager.GetString("KeyLED_Creating_Title", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Created KeySound successfully!.
        '''</summary>
        Public Shared ReadOnly Property KeySound_Created() As String
            Get
                Return ResourceManager.GetString("KeySound_Created", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Creating KeySound... ({0} / {1}).
        '''</summary>
        Public Shared ReadOnly Property KeySound_Creating() As String
            Get
                Return ResourceManager.GetString("KeySound_Creating", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Creating KeySound.
        '''</summary>
        Public Shared ReadOnly Property KeySound_Creating_Title() As String
            Get
                Return ResourceManager.GetString("KeySound_Creating_Title", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Converted LED sucessfully!.
        '''</summary>
        Public Shared ReadOnly Property LED_Converted() As String
            Get
                Return ResourceManager.GetString("LED_Converted", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Converting Ableton LED to UniPack LED....
        '''</summary>
        Public Shared ReadOnly Property LED_Converting() As String
            Get
                Return ResourceManager.GetString("LED_Converting", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Error occured while converting LED.
        '''
        '''Error Message:
        '''{0}.
        '''</summary>
        Public Shared ReadOnly Property LED_Converting_Error() As String
            Get
                Return ResourceManager.GetString("LED_Converting_Error", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Converting LED.
        '''</summary>
        Public Shared ReadOnly Property LED_Converting_Title() As String
            Get
                Return ResourceManager.GetString("LED_Converting_Title", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to LED Files Loaded! You can edit LEDs in &apos;keyLED (MIDI Extension)&apos; Tab..
        '''</summary>
        Public Shared ReadOnly Property LED_Loaded() As String
            Get
                Return ResourceManager.GetString("LED_Loaded", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Couldn&apos;t find any LED in this project..
        '''</summary>
        Public Shared ReadOnly Property LED_Not_Found() As String
            Get
                Return ResourceManager.GetString("LED_Not_Found", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to LED Files|*.mid.
        '''</summary>
        Public Shared ReadOnly Property LED_ofd_Filter() As String
            Get
                Return ResourceManager.GetString("LED_ofd_Filter", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Select LED Files.
        '''</summary>
        Public Shared ReadOnly Property LED_ofd_Title() As String
            Get
                Return ResourceManager.GetString("LED_ofd_Title", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Loading LED Files... ({0} / {1}).
        '''</summary>
        Public Shared ReadOnly Property LED_Open() As String
            Get
                Return ResourceManager.GetString("LED_Open", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Opening LED Files.
        '''</summary>
        Public Shared ReadOnly Property LED_Open_Title() As String
            Get
                Return ResourceManager.GetString("LED_Open_Title", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to This project&apos;s led made using Midi Extension Plugin.
        '''But Couldn&apos;t find Midi Extension data file.
        '''
        '''Would you like to select the data file and continue?
        '''(Often data file is in LED File (.mid extension) folder, it doesn&apos;t have extension also its name contains &apos;save&apos;).
        '''</summary>
        Public Shared ReadOnly Property LED_Save_File_Not_Found() As String
            Get
                Return ResourceManager.GetString("LED_Save_File_Not_Found", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Verifying LED... ({0} / {1}).
        '''</summary>
        Public Shared ReadOnly Property LED_Verifying() As String
            Get
                Return ResourceManager.GetString("LED_Verifying", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Open Ableton Project.
        '''</summary>
        Public Shared ReadOnly Property Main_OpenAbletonProject() As String
            Get
                Return ResourceManager.GetString("Main_OpenAbletonProject", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Open Ableton Project (Beta).
        '''</summary>
        Public Shared ReadOnly Property Main_OpenAbletonProject_Beta() As String
            Get
                Return ResourceManager.GetString("Main_OpenAbletonProject_Beta", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Finding Chains....
        '''</summary>
        Public Shared ReadOnly Property Project_Chain() As String
            Get
                Return ResourceManager.GetString("Project_Chain", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Applying to readable Infos....
        '''</summary>
        Public Shared ReadOnly Property Project_ChangeExtension() As String
            Get
                Return ResourceManager.GetString("Project_ChangeExtension", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Deleting Workspace....
        '''</summary>
        Public Shared ReadOnly Property Project_DeleteWorkspace() As String
            Get
                Return ResourceManager.GetString("Project_DeleteWorkspace", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Deleting The Tempoary Files....
        '''</summary>
        Public Shared ReadOnly Property Project_DeletingTempoaryFiles() As String
            Get
                Return ResourceManager.GetString("Project_DeletingTempoaryFiles", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Extracting The Project File....
        '''</summary>
        Public Shared ReadOnly Property Project_Extracting() As String
            Get
                Return ResourceManager.GetString("Project_Extracting", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Finding File Name....
        '''</summary>
        Public Shared ReadOnly Property Project_FileName() As String
            Get
                Return ResourceManager.GetString("Project_FileName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Project initialized!.
        '''</summary>
        Public Shared ReadOnly Property Project_Initialized() As String
            Get
                Return ResourceManager.GetString("Project_Initialized", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Initializing....
        '''</summary>
        Public Shared ReadOnly Property Project_Initializing() As String
            Get
                Return ResourceManager.GetString("Project_Initializing", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Ableton Project File Loaded!
        '''You can edit info in Information Tab..
        '''</summary>
        Public Shared ReadOnly Property Project_Loaded() As String
            Get
                Return ResourceManager.GetString("Project_Loaded", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Loading The Project File....
        '''</summary>
        Public Shared ReadOnly Property Project_Loading() As String
            Get
                Return ResourceManager.GetString("Project_Loading", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to You didn&apos;t convert the project..
        '''</summary>
        Public Shared ReadOnly Property Project_Not_Converted() As String
            Get
                Return ResourceManager.GetString("Project_Not_Converted", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to You didn&apos;t open Ableton Project!.
        '''</summary>
        Public Shared ReadOnly Property Project_Not_Opened() As String
            Get
                Return ResourceManager.GetString("Project_Not_Opened", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Ableton Project File|*.als.
        '''</summary>
        Public Shared ReadOnly Property Project_ofd_Filter() As String
            Get
                Return ResourceManager.GetString("Project_ofd_Filter", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Select a Ableton Project File.
        '''</summary>
        Public Shared ReadOnly Property Project_ofd_Title() As String
            Get
                Return ResourceManager.GetString("Project_ofd_Title", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Reseting Project....
        '''</summary>
        Public Shared ReadOnly Property Project_Reset() As String
            Get
                Return ResourceManager.GetString("Project_Reset", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Saved UniPack!.
        '''</summary>
        Public Shared ReadOnly Property Project_Saved() As String
            Get
                Return ResourceManager.GetString("Project_Saved", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Saving Project to UniPack....
        '''</summary>
        Public Shared ReadOnly Property Project_Saving() As String
            Get
                Return ResourceManager.GetString("Project_Saving", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Saving UniPack.
        '''</summary>
        Public Shared ReadOnly Property Project_Saving_Title() As String
            Get
                Return ResourceManager.GetString("Project_Saving_Title", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Zip File|*.zip|UniPack File|*.uni.
        '''</summary>
        Public Shared ReadOnly Property Project_sfd_Filter() As String
            Get
                Return ResourceManager.GetString("Project_sfd_Filter", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Select the UniPack File.
        '''</summary>
        Public Shared ReadOnly Property Project_sfd_Title() As String
            Get
                Return ResourceManager.GetString("Project_sfd_Title", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Loading The Ableton Project File....
        '''</summary>
        Public Shared ReadOnly Property Project_Title() As String
            Get
                Return ResourceManager.GetString("Project_Title", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Converted Sound sucessfully!.
        '''</summary>
        Public Shared ReadOnly Property Sound_Converted() As String
            Get
                Return ResourceManager.GetString("Sound_Converted", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Converting Sounds....
        '''</summary>
        Public Shared ReadOnly Property Sound_Converting() As String
            Get
                Return ResourceManager.GetString("Sound_Converting", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Error occured while converting Sound.
        '''
        '''Error Message:
        '''{0}.
        '''</summary>
        Public Shared ReadOnly Property Sound_Converting_Error() As String
            Get
                Return ResourceManager.GetString("Sound_Converting_Error", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Sounds Loaded!.
        '''</summary>
        Public Shared ReadOnly Property Sound_Loaded() As String
            Get
                Return ResourceManager.GetString("Sound_Loaded", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Couldn&apos;t find any sounds in this project..
        '''</summary>
        Public Shared ReadOnly Property Sound_Not_Found() As String
            Get
                Return ResourceManager.GetString("Sound_Not_Found", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to wav sound files|*.wav|mp3 sounds files|*.mp3|All Sound Files|*.*.
        '''</summary>
        Public Shared ReadOnly Property Sound_ofd_Filter() As String
            Get
                Return ResourceManager.GetString("Sound_ofd_Filter", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Select Sounds.
        '''</summary>
        Public Shared ReadOnly Property Sound_ofd_Title() As String
            Get
                Return ResourceManager.GetString("Sound_ofd_Title", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Loading Sound Files... ({0} / {1}).
        '''</summary>
        Public Shared ReadOnly Property Sound_Open() As String
            Get
                Return ResourceManager.GetString("Sound_Open", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Loading Sound Files.
        '''</summary>
        Public Shared ReadOnly Property Sound_Open_Title() As String
            Get
                Return ResourceManager.GetString("Sound_Open_Title", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Verifying Sounds... ({0} / {1}).
        '''</summary>
        Public Shared ReadOnly Property Sound_Verifying() As String
            Get
                Return ResourceManager.GetString("Sound_Verifying", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Saved UniPack Information!.
        '''</summary>
        Public Shared ReadOnly Property UniPack_Info_Saved() As String
            Get
                Return ResourceManager.GetString("UniPack_Info_Saved", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to You didn&apos;t save your UniPack. Would you like to save your UniPack?.
        '''</summary>
        Public Shared ReadOnly Property UniPack_Not_Saved() As String
            Get
                Return ResourceManager.GetString("UniPack_Not_Saved", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Not Saved.
        '''</summary>
        Public Shared ReadOnly Property UniPack_Not_Saved_Title() As String
            Get
                Return ResourceManager.GetString("UniPack_Not_Saved_Title", resourceCulture)
            End Get
        End Property
    End Class
End Namespace

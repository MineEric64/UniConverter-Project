Imports System.IO
Imports System.Threading

Public Class DeveloperMode_Workspace
    'Developer Mode에서는 Exception 예외 처리 때 GreatEx가 필요 없습니다.
    '어처피 Developer Mode는 불안정한 모드들을 Beta 기능으로 지원해주기 때문에 GreatEx가 필요 없습니다.

#Region "Workspace Paths"
    Public Shared Workspace As String = Application.StartupPath & "\Workspace"
    Public Shared abl_proj As String = Application.StartupPath & "\Workspace\ableproj\abl_proj.xml"
    Public Shared asounds As String = Application.StartupPath & "\Workspace\ableproj\sounds"
    Public Shared usounds As String = Application.StartupPath & "\Workspace\unipack\sounds"
    Public Shared keySound As String = Application.StartupPath & "\Workspace\unipack\keySound"
    Public Shared CoLED As String = Application.StartupPath & "\Workspace\ableproj\CoLED"
    Public Shared keyLED As String = Application.StartupPath & "\Workspace\unipack\keyLED"
    Public Shared info As String = Application.StartupPath & "\Workspace\unipack\info"
    Public Shared ksTmp As String = Application.StartupPath & "\Workspace\ksTmp.txt"
    Public Shared KeyTracks As String = Application.StartupPath & "\Workspace\ableproj\KeyTracks.xml"
#End Region

    Private Sub DeveloperMode_Workspace_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("{0}: Workspace", MainProject.Text)
    End Sub

    Private Sub DOE_Click(sender As Object, e As EventArgs) Handles DOE.Click
        BGW_Workspace.RunWorkerAsync()
    End Sub

    Private Sub DOEC(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BGW_Workspace.DoWork
        Try
            Dim CMDText2 As String
            CMDText2 = cmdText.Text.Trim()
            Select Case CMDText2
                Case "/?", "/help"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - {1}", Date.Now.ToString("tt hh:mm:ss"), My.Resources.CMDCode_help))

                Case "/clear"
                    DebugText.Clear()

#Region "/del <Workspace Name>"
                Case "/del"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Command Usage: '/del <WName>'", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)

                Case "/del Workspace"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleting Workspace...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    If My.Computer.FileSystem.DirectoryExists(Workspace) Then
                        My.Computer.FileSystem.DeleteDirectory(Workspace, FileIO.DeleteDirectoryOption.DeleteAllContents)
                        Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleted Workspace!", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    Else
                        Throw New Exception("The Directory 'Workspace' doesn't exists.")
                    End If

                Case "/del abl_proj"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleting abl_proj.xml...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    If File.Exists(abl_proj) Then
                        File.Delete(abl_proj)
                        Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleted abl_proj.xml!", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    Else
                        Throw New Exception("The File 'abl_proj.xml' doesn't exists.")
                    End If

                Case "/del sounds"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleting Loaded / UniPack sounds...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    If My.Computer.FileSystem.DirectoryExists(asounds) AndAlso My.Computer.FileSystem.DirectoryExists(usounds) Then
                        My.Computer.FileSystem.DeleteDirectory(asounds, FileIO.DeleteDirectoryOption.DeleteAllContents)
                        My.Computer.FileSystem.DeleteDirectory(usounds, FileIO.DeleteDirectoryOption.DeleteAllContents)
                    ElseIf My.Computer.FileSystem.DirectoryExists(asounds) Then
                        My.Computer.FileSystem.DeleteDirectory(asounds, FileIO.DeleteDirectoryOption.DeleteAllContents)
                    ElseIf My.Computer.FileSystem.DirectoryExists(usounds) Then
                        My.Computer.FileSystem.DeleteDirectory(asounds, FileIO.DeleteDirectoryOption.DeleteAllContents)
                    Else
                        Throw New Exception("The Directory 'sounds' (Ableton, UniPack sounds) doesn't exists.")
                    End If
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleted sounds!", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)

                Case "/del keySound"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleting keySound...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    If File.Exists(keySound) Then
                        File.Delete(keySound)
                        Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleted keySound!", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    Else
                        Throw New Exception("The File 'info' doesn't exists.")
                    End If

                Case "/del CoLED"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleting LED Files...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    If My.Computer.FileSystem.DirectoryExists(CoLED) Then
                        My.Computer.FileSystem.DeleteDirectory(CoLED, FileIO.DeleteDirectoryOption.DeleteAllContents)
                        Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleted LED Files!", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    Else
                        Throw New Exception("The Directory 'CoLED' doesn't exists.")
                    End If

                Case "/del keyLED"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleting keyLED...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    If My.Computer.FileSystem.DirectoryExists(keyLED) Then
                        My.Computer.FileSystem.DeleteDirectory(keyLED, FileIO.DeleteDirectoryOption.DeleteAllContents)
                        Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleted keyLED!", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    Else
                        Throw New Exception("The Directory 'keyLED' doesn't exists.")
                    End If

                Case "/del info"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleting info...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    If File.Exists(info) Then
                        File.Delete(info)
                        Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleted info!", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    Else
                        Throw New Exception("The File 'info' doesn't exists.")
                    End If

                Case "/del ksTmp"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleting ksTmp.txt...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    If File.Exists(ksTmp) Then
                        File.Delete(ksTmp)
                        Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleted ksTmp.txt!", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    Else
                        Throw New Exception("The File 'ksTmp.txt' doesn't exists.")
                    End If

                Case "/del KeyTracks"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleting KeyTracks.xml...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    If File.Exists(KeyTracks) Then
                        File.Delete(KeyTracks)
                        Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Deleted KeyTracks.xml!", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
                    Else
                        Throw New Exception("The File 'KeyTracks' doesn't exists.")
                    End If
#End Region

#Region "/exists <Workspace Name>"
                Case "/exists"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Command Usage: '/exists <WName>'", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)

                Case "/exists Workspace"
                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - 'Workspace' Directory Exists: ", Date.Now.ToString("tt hh:mm:ss"))
                               DebugText.Text = DebugText.Text & Convert.ToString(My.Computer.FileSystem.DirectoryExists(Workspace)) & vbNewLine
                           End Sub)

                Case "/exists abl_proj"
                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - 'abl_proj.xml' File Exists: ", Date.Now.ToString("tt hh:mm:ss"))
                               DebugText.Text = DebugText.Text & Convert.ToString(File.Exists(abl_proj)) & vbNewLine
                           End Sub)

                Case "/exists sounds"
                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - 'sounds' (Ableton) Directory Exists: ", Date.Now.ToString("tt hh:mm:ss"))
                               DebugText.Text = DebugText.Text & Convert.ToString(My.Computer.FileSystem.DirectoryExists(asounds)) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - 'sounds' (UniPack) Directory Exists: ", Date.Now.ToString("tt hh:mm:ss"))
                               DebugText.Text = DebugText.Text & Convert.ToString(My.Computer.FileSystem.DirectoryExists(usounds)) & vbNewLine
                           End Sub)

                Case "/exists keySound"
                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - 'keySound' File Exists: ", Date.Now.ToString("tt hh:mm:ss"))
                               DebugText.Text = DebugText.Text & Convert.ToString(File.Exists(keySound)) & vbNewLine
                           End Sub)

                Case "/exists CoLED"
                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - 'CoLED' Directory Exists: ", Date.Now.ToString("tt hh:mm:ss"))
                               DebugText.Text = DebugText.Text & Convert.ToString(My.Computer.FileSystem.DirectoryExists(CoLED)) & vbNewLine
                           End Sub)

                Case "/exists keyLED"
                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - 'keyLED' Directory Exists: ", Date.Now.ToString("tt hh:mm:ss"))
                               DebugText.Text = DebugText.Text & Convert.ToString(My.Computer.FileSystem.DirectoryExists(keyLED)) & vbNewLine
                           End Sub)

                Case "/exists info"
                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - 'info' File Exists: ", Date.Now.ToString("tt hh:mm:ss"))
                               DebugText.Text = DebugText.Text & Convert.ToString(File.Exists(info)) & vbNewLine
                           End Sub)

                Case "/exists ksTmp"
                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - 'ksTmp.txt' File Exists: ", Date.Now.ToString("tt hh:mm:ss"))
                               DebugText.Text = DebugText.Text & Convert.ToString(File.Exists(ksTmp)) & vbNewLine
                           End Sub)

                Case "/exists KeyTracks"
                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - 'KeyTracks.xml' File Exists: ", Date.Now.ToString("tt hh:mm:ss"))
                               DebugText.Text = DebugText.Text & Convert.ToString(File.Exists(KeyTracks)) & vbNewLine
                           End Sub)
#End Region

#Region "/enable <Mode Name>"
                Case "/enable"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Command Usage: '/enable <ModeName>'", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)

                Case "/enable DeveloperMode"
                    If File.Exists(MainProject.LicenseFile(0)) Then Throw New Exception("'Developer Mode License already exists.")

                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - Enabling Developer Mode...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Encoding Developer Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Writing Developer Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Encrypting Developer Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                           End Sub)
                    File.WriteAllText(MainProject.LicenseFile(0), My.Resources.License_DeveloperMode)
                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - Enabled Developer Mode!", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - UniConverter will reboot after 3 seconds to refresh codes...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               RebootTimer.Start()
                           End Sub)

                Case "/enable GreatExMode"
                    If File.Exists(MainProject.LicenseFile(1)) Then Throw New Exception("GreatEx Mode License already exists.")

                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - Enabling GreatEx Mode...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Encoding GreatEx Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Writing GreatEx Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Encrypting GreatEx Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                           End Sub)
                    File.WriteAllText(MainProject.LicenseFile(0), My.Resources.License_GreatExMode)
                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - Enabled GreatEx Mode!", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - UniConverter will reboot after 3 seconds to refresh codes...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               RebootTimer.Start()
                           End Sub)
#End Region

#Region "/disable <Mode Name>"
                Case "/disable"
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Command Usage: '/disable <ModeName>'", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)

                Case "/disable DeveloperMode"
                    If File.Exists(MainProject.LicenseFile(0)) = False Then Throw New Exception("'Developer Mode License doesn't exists.")

                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - Disabling Developer Mode...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Decrypting Developer Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Decoding Developer Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Reading Developer Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Deleting Developer Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                           End Sub)
                    File.Delete(MainProject.LicenseFile(0))
                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - Disabled Developer Mode!", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - UniConverter will reboot after 3 seconds to refresh codes...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               RebootTimer.Start()
                           End Sub)

                Case "/disable GreatExMode"
                    If File.Exists(MainProject.LicenseFile(0)) = False Then Throw New Exception("'GreatEx Mode License doesn't exists.")

                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - Disabling GreatEx Mode...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Decrypting GreatEx Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Decoding GreatEx Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Reading GreatEx Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - Deleting GreatEx Mode License...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                           End Sub)
                    File.Delete(MainProject.LicenseFile(0))
                    Invoke(Sub()
                               DebugText.Text = DebugText.Text & String.Format("{0} - Disabled GreatEx Mode!", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               DebugText.Text = DebugText.Text & String.Format("{0} - UniConverter will reboot after 3 seconds to refresh codes...", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine
                               RebootTimer.Start()
                           End Sub)
#End Region

                Case Else
                    Invoke(Sub() DebugText.Text = DebugText.Text & String.Format("{0} - Type '/?' or '/help' to help.", Date.Now.ToString("tt hh:mm:ss")) & vbNewLine)
            End Select

        Catch ex As Exception
            Invoke(Sub() DebugText.Text = DebugText.Text & "---Error Message--- " & ex.Message & vbNewLine & "---Error StackTrace---" & ex.StackTrace & vbNewLine)
        End Try
    End Sub

    Private Sub RebootTimer_Tick(sender As Object, e As EventArgs) Handles RebootTimer.Tick
        Application.Restart()
        RebootTimer.Stop()
    End Sub
End Class
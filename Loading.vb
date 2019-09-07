Public Class Loading
    Public WithEvents Timer As New Timer

    Public Shared loading_Sound_Open_msg As String = "Loading Sound Files... ({0} / {1})"
    Public Shared loading_LED_open_msg As String = "Loading LED Files... ({0} / {1})"
    Public Shared loading_LED_openList_msg As String = "Replacing LED Files... ({0} / {1})"
    Public Shared loading_Project_Extract_msg As String = "Extracting The Project File..."
    Public Shared loading_Project_Load_msg As String = "Loading The Project File..."
    Public Shared loading_Project_DeleteTmp_msg As String = "Deleting The Tempoary Files..."
    Public Shared loading_Project_ChangeExt_msg As String = "Applying to readable Infos..."
    Public Shared loading_Project_FileName_msg As String = "Finding File Name..."
    Public Shared loading_Project_Chain_msg As String = "Finding Chains..."
End Class
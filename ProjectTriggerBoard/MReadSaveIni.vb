
'********************************************************mobary+ 2013/11/20*************************************************************************
#Region "Mobary+ 2013/11/20"



Module MReadSaveIni

    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Long
    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByRef nSize As Long, ByVal lpFileName As String) As Long

    ''' <summary>
    ''' ReadIniString:字串讀取
    ''' </summary>
    ''' <param name="Section">類別</param>
    ''' <param name="KeyName">名稱</param>
    ''' <param name="FileName">檔名</param>
    ''' <param name="DefualValue">預設值</param>
    ''' <param name="BufferSize">字串長度</param>
    ''' <returns>讀取的字串</returns>
    ''' <remarks></remarks>
    Public Function ReadIniString(ByVal Section As String, ByVal KeyName As String, ByVal FileName As String, Optional ByVal DefualValue As String = "", Optional ByVal BufferSize As Long = 1024) As String

        'GetPrivateProfileString "Section", "Key", "預設值", 傳回字串, 傳回長度, "ini檔名"
        Dim strReturnedString As String
        Dim lngLen As Long

        On Error GoTo ErrExit

        strReturnedString = New String(Chr(0), BufferSize)
        lngLen = GetPrivateProfileString(Section, KeyName, DefualValue, strReturnedString, BufferSize, FileName)
        ReadIniString = Left(strReturnedString, lngLen)

        Exit Function

ErrExit:
        MsgBox(Err.GetException.ToString(), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        Err.Clear()
    End Function

    ''' <summary>
    ''' SaveIniString:儲存資料 寫至檔案內
    ''' </summary>
    ''' <param name="Section">類別</param>
    ''' <param name="KeyName">名稱</param>
    ''' <param name="KeyValue">數值</param>
    ''' <param name="FileName">檔名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveIniString(ByVal Section As String, ByVal KeyName As String, ByVal KeyValue As String, ByVal FileName As String) As Long

        'WritePrivateProfileString "Section", "Key", "值", "ini檔名"
        '若要刪除某一個名稱，值傳入vbNullString即可
        'WritePrivateProfileString "Position", "Width", vbNullString, "C:\Windows\temp\MyApp.ini"
        On Error GoTo ErrExit

        SaveIniString = WritePrivateProfileString(Section, KeyName, KeyValue, FileName)

        Exit Function
ErrExit:
        MsgBox(Err.GetException.ToString(), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        Err.Clear()
    End Function

End Module

#End Region
'**************************************************************************************************************************************************
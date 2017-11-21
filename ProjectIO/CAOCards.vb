Imports ProjectCore

Public Class CAOCards
    ''' <summary>AO卡數量</summary>
    ''' <remarks></remarks>
    Dim AOCardCount As Integer
    ''' <summary>實際使用AO接點數量</summary>
    ''' <remarks></remarks>
    Dim AOChannelCount As Integer = 4
    ''' <summary>AO卡參數</summary>
    ''' <remarks></remarks>
    Public AOCardParameter As New List(Of sAOCardParameter)

    Public Function Load(ByVal strFileName As String) As Boolean
        Dim strSection As String
        strSection = "Configuration"
        AOCardCount = CInt(ReadIniString(strSection, "AOCardCount", strFileName, 1))
        AOChannelCount = CInt(ReadIniString(strSection, "AOChannelCount", strFileName, 4))
        AOCardParameter.Clear()
        gSyslog.Save("AO-Card:" & AOCardCount & " PCS Channel: " & AOChannelCount)

        '[說明]:對每一張AO卡,讀取參數
        For mCardNo = 0 To AOCardCount - 1
            Dim mCard As New sAOCardParameter
            mCard.Load(mCardNo, strFileName)
            AOCardParameter.Add(mCard)
        Next
        Return True
    End Function

    Public Function Save(ByVal strFileName As String) As Boolean
        Dim strSection As String
        strSection = "Configuration"
        Call SaveIniString(strSection, "AOCardCount", AOCardCount, strFileName)
        Call SaveIniString(strSection, "AOChannelCount", AOChannelCount, strFileName)
        '[說明]:對每一張AO卡,儲存參數
        For mCardNo = 0 To AOCardCount - 1
            AOCardParameter(mCardNo).Save(mCardNo, strFileName)
        Next
        Return True
    End Function

    

End Class

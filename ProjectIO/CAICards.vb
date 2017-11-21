Imports ProjectCore

Public Class CAICards
    ''' <summary>AI卡數量</summary>
    ''' <remarks></remarks>
    Public AICardCount As Integer
    ''' <summary>實際使用AI接點數量</summary>
    ''' <remarks></remarks>
    Public AIChannelCount As Integer = 8
    ''' <summary>AI卡參數</summary>
    ''' <remarks></remarks>
    Public AICardParameter As New List(Of sAICardParameter)
    Public Function Load(ByVal strFileName As String) As Boolean
        Dim strSection As String
        strSection = "Configuration"
        AICardCount = CInt(ReadIniString(strSection, "AICardCount", strFileName, 1))
        AIChannelCount = CInt(ReadIniString(strSection, "AIChannelCount", strFileName, 8))
        AICardParameter.Clear()
        gSyslog.Save("AI-Card:" & AICardCount & " PCS Channel: " & AIChannelCount)

        '[說明]:對每一張AI卡,讀取參數
        For mCardNo = 0 To AICardCount - 1
            Dim mCard As New sAICardParameter
            mCard.Load(mCardNo, strFileName)
            AICardParameter.Add(mCard)
        Next
        Return True
    End Function

    Public Function Save(ByVal strFileName As String) As Boolean
        Dim strSection As String
        strSection = "Configuration"
        Call SaveIniString(strSection, "AICardCount", AICardCount, strFileName)
        Call SaveIniString(strSection, "AIChannelCount", AIChannelCount, strFileName)
        '[說明]:對每一張AI卡,儲存參數
        For mCardNo = 0 To AICardCount - 1
            AICardParameter(mCardNo).Save(mCardNo, strFileName)
        Next
        Return True
    End Function

End Class

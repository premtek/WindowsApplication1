Imports ProjectCore '機台關鍵參數

Public Structure sEPVCardParameter
    ''' <summary>卡型號</summary>
    ''' <remarks></remarks>
    Public CardType As enmEPVCardType
    ''' <summary>
    ''' AIO配接參數
    ''' </summary>
    ''' <remarks></remarks>
    Public AIOAdapter As sAIOAdapter
    ''' <summary>ITV連線參數</summary>
    ''' <remarks></remarks>
    Public ITV_Series As System.IO.Ports.SerialPort
    Public Function Load(ByVal cardNo As Integer, ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "Configuration"
        CardType = Val(ReadIniString(strSection, "EPVCard-" & cardNo & "-CardType", fileName, enmEPVCardType.None))
        AIOAdapter.AI = Val(ReadIniString(strSection, "EPVCard-" & cardNo & "-AI", fileName, 0))
        AIOAdapter.AO = Val(ReadIniString(strSection, "EPVCard-" & cardNo & "-AO", fileName, 0))
        ITV_Series = New IO.Ports.SerialPort()
        ITV_Series.PortName = ReadIniString(strSection, "EPVCard-" & cardNo & "-PortName", fileName, "COM1")
        ITV_Series.BaudRate = Val(ReadIniString(strSection, "EPVCard-" & cardNo & "-BaudRate", fileName, 9600))
        ITV_Series.DataBits = Val(ReadIniString(strSection, "EPVCard-" & cardNo & "-DataBits", fileName, 8))
        ITV_Series.Parity = Val(ReadIniString(strSection, "EPVCard-" & cardNo & "-Parity", fileName, CInt(System.IO.Ports.Parity.None)))
        ITV_Series.StopBits = Val(ReadIniString(strSection, "EPVCard-" & cardNo & "-StopBits", fileName, CInt(System.IO.Ports.StopBits.One)))
        ITV_Series.NewLine = vbCrLf
        ITV_Series.Encoding = System.Text.Encoding.ASCII
        ITV_Series.Handshake = IO.Ports.Handshake.None
        Return True
    End Function

    Public Function Save(ByVal cardNo As Integer, ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "Configuration"
        Call SaveIniString(strSection, "EPVCard-" & cardNo & "-CardType", CInt(CardType), fileName)
        Call SaveIniString(strSection, "EPVCard-" & cardNo & "-AI", AIOAdapter.AI, fileName)
        Call SaveIniString(strSection, "EPVCard-" & cardNo & "-AO", AIOAdapter.AO, fileName)

        If Not ITV_Series Is Nothing Then
            Call SaveIniString(strSection, "EPVCard-" & cardNo & "-PortName", ITV_Series.PortName, fileName)
            Call SaveIniString(strSection, "EPVCard-" & cardNo & "-BaudRate", ITV_Series.BaudRate, fileName)
            Call SaveIniString(strSection, "EPVCard-" & cardNo & "-DataBits", ITV_Series.DataBits, fileName)
            Call SaveIniString(strSection, "EPVCard-" & cardNo & "-Parity", CInt(ITV_Series.Parity), fileName)
            Call SaveIniString(strSection, "EPVCard-" & cardNo & "-StopBits", CInt(ITV_Series.StopBits), fileName)
        Else
            Call SaveIniString(strSection, "EPVCard-" & cardNo & "-PortName", "COM1", fileName)
            Call SaveIniString(strSection, "EPVCard-" & cardNo & "-BaudRate", 9600, fileName)
            Call SaveIniString(strSection, "EPVCard-" & cardNo & "-DataBits", 8, fileName)
            Call SaveIniString(strSection, "EPVCard-" & cardNo & "-Parity", CInt(System.IO.Ports.Parity.Even), fileName)
            Call SaveIniString(strSection, "EPVCard-" & cardNo & "-StopBits", CInt(System.IO.Ports.StopBits.One), fileName)
        End If
        Return True
    End Function

End Structure

''' <summary>電空閥卡片集合</summary>
''' <remarks></remarks>
Public Class CElectroPneumaticValveCards
    ''' <summary>
    ''' 電空閥控制單元參數
    ''' </summary>
    ''' <remarks></remarks>
    Public Parameters As New List(Of sEPVCardParameter)
    ''' <summary>
    ''' 實際使用電空閥控制單元數量
    ''' </summary>
    ''' <remarks></remarks>
    Public ChannelCount As Integer = 4
    ''' <summary>
    ''' 電空閥控制單元數量
    ''' </summary>
    ''' <remarks></remarks>
    Public CardCount As Integer
    ''' <summary>讀取卡片設定集</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal strFileName As String) As Boolean
        Dim strSection As String
        strSection = "Configuration"
        CardCount = CInt(ReadIniString(strSection, "EPVCardCount", strFileName, 1))
        ChannelCount = CInt(ReadIniString(strSection, "EPVChannelCount", strFileName, 4))
        gSyslog.Save("EPV-Card:" & ChannelCount & " PCS Channel: " & ChannelCount)
        Parameters.Clear()
        For mCardNo = 0 To CardCount - 1
            Dim mCard As New sEPVCardParameter
            mCard.Load(mCardNo, strFileName)
            Parameters.Add(mCard)
        Next
        Return True
    End Function
    ''' <summary>儲存卡片設定集</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal strFileName As String) As Boolean
        Dim strSection As String
        strSection = "Configuration"
        Call SaveIniString(strSection, "EPVCardCount", CardCount, strFileName)
        Call SaveIniString(strSection, "EPVChannelCount", ChannelCount, strFileName)
        For mCardNo = 0 To CardCount - 1
            Parameters(mCardNo).Save(mCardNo, strFileName)
        Next
        Return True
    End Function
End Class

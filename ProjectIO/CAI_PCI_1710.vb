Imports Automation.BDaq
Imports ProjectCore

Public Class CAI_PCI_1710
    Implements IAIInterface
    ''' <summary>AI實體物件</summary>
    ''' <remarks></remarks>
    Friend WithEvents InstantAiCtrl1 As New Automation.BDaq.InstantAiCtrl

    ''' <summary>建立卡片物件, 指定一卡幾Port</summary>
    ''' <remarks></remarks>
    Sub New()
        PortPerCard = 8
    End Sub

    ''' <summary>關閉卡片</summary>
    ''' <remarks></remarks>
    Public Sub Close() Implements IAIInterface.Close
        InstantAiCtrl1.Dispose()
    End Sub
    ''' <summary>初始化連線設定</summary>
    ''' <param name="deviceDescription"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(deviceDescription As String) As Boolean Implements IAIInterface.Initial
        Try
            InstantAiCtrl1.SelectedDevice = New DeviceInformation(deviceDescription)
            Return True
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1007000), "Error_1007000", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            Return False
        End Try
    End Function
    ''' <summary>埠/卡</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PortPerCard As Integer Implements IAIInterface.PortPerCard

    ''' <summary>讀取資料</summary>
    ''' <param name="portStart"></param>
    ''' <param name="portEnd"></param>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Read(portStart As Integer, portEnd As Integer, ByRef val() As Double) As Integer Implements IAIInterface.Read
        Dim errorCode As ErrorCode = InstantAiCtrl1.Read(portStart, portEnd, val)
        Return errorCode
    End Function
End Class

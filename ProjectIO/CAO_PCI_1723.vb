Imports Automation.BDaq
Imports ProjectCore

Public Class CAO_PCI_1723
    Implements IAOInterface
    ''' <summary>
    ''' AO卡片實體物件
    ''' </summary>
    ''' <remarks></remarks>
    Friend WithEvents InstantAoCtrl1 As New Automation.BDaq.InstantAoCtrl

    ''' <summary>
    ''' 新建卡片物件
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        PortPerCard = 4
    End Sub
    ''' <summary>
    ''' 關閉卡片連線
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Close() Implements IAOInterface.Close
        InstantAoCtrl1.Dispose()
    End Sub
    ''' <summary>
    ''' 初始化卡片連線
    ''' </summary>
    ''' <param name="deviceDescription"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initial(deviceDescription As String) As Boolean Implements IAOInterface.Initial
        Try
            InstantAoCtrl1.SelectedDevice = New DeviceInformation(deviceDescription)
            Return True
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1008000), "Error_1008000", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox("gMsgHandler.GetMessage(Error_1008000)" & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "PCI-1723")
            Return False
        End Try
    End Function
    ''' <summary>
    ''' 埠/卡
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PortPerCard As Integer Implements IAOInterface.PortPerCard
    ''' <summary>
    ''' 資料寫入卡片
    ''' </summary>
    ''' <param name="portStart"></param>
    ''' <param name="portEnd"></param>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Write(portStart As Integer, portEnd As Integer, ByRef val() As Double) As Integer Implements IAOInterface.Write
        Dim errorCode As ErrorCode = InstantAoCtrl1.Write(portStart, portEnd, val)
        Return errorCode
    End Function
End Class

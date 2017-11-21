Imports ProjectCore

Public Interface ILightInterface
    ''' <summary>
    ''' 初始化連線設定
    ''' </summary>
    ''' <param name="portName"></param>
    ''' <param name="baudRate"></param>
    ''' <param name="dataBits"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Initial(ByVal portName As String, ByVal baudRate As Integer, ByVal dataBits As Integer) As Boolean
    ''' <summary>
    ''' 關閉連線
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Close() As Boolean

    ''' <summary>
    ''' 讀取亮度值到Property
    ''' </summary>
    ''' <param name="ChNo">通道</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLightValue(ByVal ChNo As Integer, ByRef value As Integer, Optional ByVal waitReturn As Boolean = False) As String
    ''' <summary>設定亮度值</summary>
    ''' <param name="ChNo">通道</param>
    ''' <param name="lightValue">亮度值</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetLightValue(ByVal ChNo As Integer, ByVal lightValue As Integer, Optional ByVal waitReturn As Boolean = False) As String

    ''' <summary>命令逾時</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsTimeOut As Boolean
    ''' <summary>忙碌中</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsBusy As Boolean
    ''' <summary>讀取資料結構</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Result(ByVal channelNo As Integer) As sReceiveStatus

End Interface

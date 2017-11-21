Imports ProjectCore

Public Interface IElectroPneumaticValve
    ''' <summary>
    ''' 初始化 使用序列埠
    ''' </summary>
    ''' <param name="item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Initial(ByRef item As System.IO.Ports.SerialPort) As Boolean
    ''' <summary>
    ''' 關閉連線
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Close() As Boolean
    ''' <summary>
    ''' 本卡有幾個通道可用
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ChannelPerCard As Integer
    ''' <summary>
    ''' 設定值
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="stationNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetValue(ByVal value As Decimal, Optional ByVal waitReturn As Boolean = False, Optional ByVal stationNo As Integer = -1) As Boolean
    ''' <summary>
    ''' 讀取值
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="stationNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetValue(ByRef value As Decimal, Optional ByVal waitReturn As Boolean = False, Optional ByVal stationNo As Integer = -1) As Boolean
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
    ReadOnly Property Result As sReceiveStatus

    ''' <summary>
    ''' 最大氣壓值
    ''' </summary>
    ''' <remarks></remarks>
    Property Max_Mpa As Decimal
    ''' <summary>
    ''' 最小氣壓值
    ''' </summary>
    ''' <remarks></remarks>
    Property Min_Mpa As Decimal
End Interface

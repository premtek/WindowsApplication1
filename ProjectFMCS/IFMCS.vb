Imports ProjectCore

''' <summary>流量監控系統介面</summary>
''' <remarks></remarks>
Public Interface IFMCS
    Event OnRecievedData(sender As Object, ByVal e As FMCSEventArgs)
    ''' <summary>
    ''' Error Message
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ErrMsg As String
    ''' <summary>輸出平均流量</summary>
    ''' <remarks></remarks>
    Property OutputAvgFlow As Double
    ''' <summary>輸出體積</summary>
    ''' <remarks></remarks>
    Property OutputVolume As Double
    ''' <summary>索引</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValveIndex As Integer
    ''' <summary>通訊埠已開啟</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsPortOpen() As Boolean
    ''' <summary>
    ''' 忙碌中
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property IsBusy As Boolean

    ''' <summary>
    ''' TimeOut(逾時)
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property IsTimeOut As Boolean

    ''' <summary>
    ''' 設定Timeout時間
    ''' </summary>
    ''' <remarks></remarks>
    Property TimeoutTimer As Integer

    
    ''' <summary>是否初始化成功</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsInitialOK As Boolean
    ''' <summary>初始化</summary>
    ''' <param name="portName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Initial(ByVal portName As String, ByVal baudRate As Integer) As Boolean
    ''' <summary>關閉</summary>
    ''' <remarks></remarks>
    Sub Close()
    ''' <summary>開始記錄</summary>
    ''' <param name="intX"></param>
    ''' <param name="intY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RecordStart(ByVal intX As Integer, ByVal intY As Integer) As String
    ''' <summary>結束記錄</summary>
    ''' <param name="intX"></param>
    ''' <param name="intY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RecordEnd(ByVal intX As Integer, ByVal intY As Integer) As String
    
End Interface

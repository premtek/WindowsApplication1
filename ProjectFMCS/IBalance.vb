Imports ProjectCore

Public Interface IBalance
    ''' <summary>接收資料</summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Event OnDataRecieved(sender As Object, ByVal e As DataEventArgs)
    ''' <summary>
    ''' Error Message
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ErrMsg As String
  
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

    ''' <summary>
    ''' Is Port Open?
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property PortIsOpen As Boolean
    ''' <summary>是否初始化成功</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsInitialOK As Boolean
    ''' <summary>讀取資料結構</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Result As sReceiveStatus

    ''' <summary>要求讀取穩定值</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RequestStableValue(ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>要求讀取現在值</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RequestCurrentValue(ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>要求重新啟動</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RequestReStart(Optional ByVal waitReturn As Boolean = False) As Boolean


    ''' <summary>歸零</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Rezero(Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>清除結果</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Reset() As Boolean

    ''' <summary>連線</summary>
    ''' <param name="PortName"></param>
    ''' <param name="baudRate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Initial(ByVal PortName As String, ByVal baudRate As String, ByVal TimeoutTimer As Double) As Boolean
    ''' <summary>關閉</summary>
    ''' <remarks></remarks>
    Sub Close()
    ''' <summary>RS232命令</summary>
    ''' <param name="strCmd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SendCommandToSerialPort(ByVal strCmd As String, ByRef value As Double, Optional ByVal waitReturn As Boolean = False) As Boolean

End Interface

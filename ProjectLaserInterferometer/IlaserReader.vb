Imports ProjectCore

Public Interface ILaserReader
   
    ''' <summary>網路連線</summary>
    ''' <param name="IP"></param>
    ''' <param name="Port"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function EthernetOpen(ByVal IP As String, ByVal Port As Integer) As Boolean
    ''' <summary>COM埠連線</summary>
    ''' <param name="PortName"></param>
    ''' <param name="BaudRate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Initial(ByVal PortName As String, ByVal BaudRate As String) As Boolean
    ''' <summary>關閉</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Close() As Boolean
    ''' <summary>讀取測高值</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetValue(ByVal Mode As String, ByRef data As String, Optional aiIndex As Integer = 0, Optional ByVal waitReturn As Boolean = False) As Boolean
    ' ''' <summary>狀態重置</summary>
    ' ''' <remarks></remarks>
    'Sub ResetState()
    ''' <summary>取得版本</summary>
    ''' <param name="Version"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetVersion(ByRef Version As String) As Boolean
    ''' <summary>重啟控制器</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RebootController() As Boolean
    ''' <summary>選擇Recipe</summary>
    ''' <param name="ProgramID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeProgram(ByVal ProgramID As Integer) As Boolean
    ReadOnly Property IsTimeOut As Boolean
    Property TimeoutTimer As Integer
    ReadOnly Property PortIsOpen As Boolean
    ReadOnly Property IsBusy As Boolean
    ''' <summary>讀取資料結構</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Result(ByVal channelNo As Integer) As sReceiveStatus
End Interface

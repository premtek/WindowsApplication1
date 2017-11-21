Imports ProjectCore
Imports ProjectMotion
Imports Cognex.VisionPro
'Imports Cognex.VisionPro.ImageFile
Imports Cognex.VisionPro.ToolBlock


Public Class AOIEventArgs
    Inherits EventArgs
    ''' <summary>實體CCD索引</summary>
    ''' <remarks></remarks>
    Public ChannelNo As Integer
    ''' <summary>
    ''' 場景名稱
    ''' </summary>
    ''' <remarks></remarks>
    Public SceneName As String
    
    ''' <summary>
    ''' 相應票券/索引
    ''' </summary>
    ''' <remarks></remarks>
    Public Ticket As Integer
    ''' <summary>
    ''' 是否純取像
    ''' </summary>
    ''' <remarks></remarks>
    Public IsAcqImageOnly As Boolean
    ''' <summary>
    ''' 虛擬CCD索引
    ''' </summary>
    ''' <remarks></remarks>
    Public CCDNo As Integer
End Class

Public Interface IAOIInterface
    Function Copy() As IAOIInterface
    ''' <summary>計算成功事件</summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Event OnRunSuccessed(sender As Object, ByVal e As AOIEventArgs)

    ReadOnly Property IsIntialOK As Boolean

    ReadOnly Property SockStatus As SockStatusInfo
    ''' <summary>錯誤訊息</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ErrorMessage As String
    ''' <summary>
    ''' [資料接收中]
    ''' </summary>
    ''' <remarks></remarks>
    Property IsCCDReceiveBusy(ByVal realCCDNo As Integer) As Boolean
    ''' <summary>
    ''' [CCD Handshake(接收到的資料格式正確)回應成功 為true, 失敗為false]
    ''' </summary>
    ''' <remarks></remarks>
    Property IsCCDReceivedDataOK(ByVal realCCDNo As Integer) As Boolean
    ''' <summary>
    ''' CCD取像後是否設定下一組場景參數，需要為true，不需要為false
    ''' </summary>
    ''' <param name="realCCDNo"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IsCCDSetNextScene(ByVal realCCDNo As Integer) As Boolean

    ''' <summary>取得定位工具清單</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAlignmentToolNameList() As String()
    ''' <summary>[設定CCD運轉行為]</summary>
    ''' <param name="enmRun"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetCCDRunType(ByVal realCCDNo As Integer, ByVal enmRun As enmCCDRunType) As Boolean
    ''' <summary>[開啟與CCD通訊]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Initial(ByVal ccdConnectionParameter As sCCDConnectionParameter) As Boolean
    ''' <summary>讀取教導工具</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadVision(ByVal strFileName As String, Optional sceneName As String = "") As Boolean
    ''' <summary>[CCD觸發拍照]</summary>
    ''' <param name="OnOff"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetCCDTrigger(ByVal realCCDNo As Integer, ByVal OnOff As enmONOFF, ByVal isAcqImageOnly As Boolean, ByVal isSetNextScene As Boolean) As Integer
    ''' <summary>[CCD完成取像]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsCCDReady(ByVal realCCDNo As Integer) As Boolean
    ''' <summary>[CCD Busy] </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsCCDCBusy(ByVal realCCDNo As Integer) As Boolean
    Function IsCCDExist() As Boolean
    ''' <summary>
    ''' [CCD運算結果判定NG]
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsCCDResultNG(ByVal ticket As Integer, ByVal ccdNo As Integer) As Boolean
    ''' <summary>
    '''  [Send Command To CCD]
    ''' </summary>
    ''' <param name="CCDHandshakeData"></param>
    ''' <remarks></remarks>
    Function CCDSendCommand(ByVal CCDHandshakeData As String) As Boolean
    ''' <summary>
    ''' CCD取像模式
    ''' </summary>
    ''' <param name="ImageMode"></param>
    ''' <param name="ImageDisplayingNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetImageMode(ByVal ImageMode As enmCCDImageShowType, Optional ImageDisplayingNo As Integer = 0) As Boolean

    ''' <summary>
    ''' Select CCD Scence 場景切換
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetCCDScene(ByVal realCCDNo As Integer, ByVal ScenceName As String) As Boolean

    ''' <summary>[CCD TimeOut檢查][KEYENCE CCD 場景切換需要2s] </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsCCDTimeOut() As Boolean

    ''' <summary>取得定位結果</summary>
    ''' <param name="realCCDNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAlignOffset(ByVal realCCDNo As Integer, ByVal ticket As Integer, ByRef alignResult As List(Of sAlignResult), ByVal ccdNo As Integer) As Boolean

    ''' <summary>關閉</summary>
    ''' <remarks></remarks>
    Sub Close()
    ''' <summary>顯示定位結果</summary>
    ''' <param name="realCCDNo">CCD索引</param>
    ''' <param name="ticket">場景索引</param>
    ''' <param name="ucDisp">顯示介面</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ShowAlignResult(ByVal realCCDNo As Integer, ByVal ticket As Integer, ByVal ucDisp As ucDisplay, ByVal ccdNo As Integer) As Boolean
    ''' <summary>取得CCD定位教導寬,高</summary>
    ''' <param name="sceneName">場景索引</param>
    ''' <param name="sideXLength">寬</param>
    ''' <param name="sideYLength">高</param>
    ''' <param name="rotation">角度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAlignTrainSideLength(ByVal sceneName As String, ByRef sideXLength As Decimal, ByRef sideYLength As Decimal, ByRef rotation As Decimal) As Boolean
    ''' <summary>取得取像影像大小</summary>
    ''' <param name="realCCDNo">CCD索引</param>
    ''' <param name="sideXLength">寬</param>
    ''' <param name="sideYLength">高</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAcquistionSideLength(ByVal realCCDNo As Integer, ByRef sideXLength As Integer, ByRef sideYLength As Integer) As Boolean
    ''' <summary>設定定位影像</summary>
    ''' <param name="realCCDNo">CCD索引</param>
    ''' <param name="sceneName">場景索引</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetAlignImage(ByVal realCCDNo As Integer, ByVal sceneName As String) As Boolean
    Function SetExposure(ByVal realCCDNo As Integer, ByVal exposure_ms As Decimal) As Boolean
    Function GetExposure(ByVal realCCDNo As Integer) As Double
    Function SetAutoWhiteBalance(ByVal channelNo As Integer, ByVal Enable As Boolean) As Boolean
    Function SetAutoExposure(ByVal channelNo As Integer, ByVal Enable As Boolean) As Boolean
    Function CalibBoardCalibration(ByVal channelNo As Integer, ByVal InputImage As Object, ByVal Train As Boolean, ByVal TileSize As Decimal) As Object
    ''' <summary>取得場景清單</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSceneList() As String()
    ''' <summary>
    ''' 取得取像工具
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAcqTool() As Object
    ''' <summary>取得CCD影像</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAcqOutputImage() As Object
    ''' <summary>
    ''' 取得場景工具包
    ''' </summary>
    ''' <param name="sceneName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetToolBlock(ByVal sceneName As String) As Object
    ''' <summary>
    ''' 取像
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AcquireImage() As Boolean
    Function SaveSceneOutputParam(ByVal sceneName As String, ByVal fileName As String) As Boolean
    Function OutputParam(ByVal sceneName As String) As COutputParam
    Function SetSceneInputImage(ByVal SceneName As String, ByRef image As Object) As Boolean
    Function RemoveScene(ByVal sceneName As String) As Boolean
    Function CreateScene(ByVal sceneName As String, ByVal mAlignType As eAlignType, ByVal mScenePath As String) As Boolean
    Function IsSceneExist(ByVal SceneName As String) As Boolean
    Function IsCalcFinish(ByVal sceneName As String) As Boolean
    Function SetAlignImage(ByVal realCCDNo As Integer) As Boolean


    ''' <summary>[清空Ticket]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ClearTicket() As Boolean

    Function SetHardwareTrigger(ByVal HardwareTrigger As Boolean) As Boolean
    Function SetIsStop(ByVal state As Boolean) As Boolean

    Function SetIsAcqImageOnly(ByVal AcqImageOnly As Boolean) As Boolean

    Function SetLiveTriggerMode(ByVal channelNo As Integer, ByVal TriggerType As eTriggerType) As Boolean
    '飛拍
    'Function LoadOnFlyScene(ByVal CCDNo As Integer, ByVal ScenceList() As String) As Boolean
 
    'Function SetOnFlyCCDNextScene(ByVal CCDNo As Integer, ByVal SceneCount As UInteger) As Boolean
    'Eason 20170217 Ticket:100032 , Memory Free Part3
    Sub Clear()

End Interface

Imports System.Windows.Forms
''' <summary>
''' 底層共用介面
''' </summary>
''' <remarks></remarks>
Public Enum enmCommandState
    ''' <summary>執行完成</summary>
    ''' <remarks></remarks>
    Success = -1
    ''' <summary>命令執行失敗</summary>
    ''' <remarks></remarks>
    Failed = 0
    ''' <summary>等待命令</summary>
    ''' <remarks></remarks>
    WaitCommand
    ''' <summary>命令已發,等待執行</summary>
    ''' <remarks></remarks>
    WaitExecution
End Enum
Public Enum enmValveModeType
    Timed = 1
    Purge = 2
    Continuous = 3
    ReadCurrentMode = 5
End Enum
''' <summary>
''' PicoTouch命令種類
''' </summary>
''' <remarks></remarks>
Public Enum enmValveCommandType
    ValveMode
    ValveDispenseCount
    ValveOnTime
    ValveOffTime
    ValveStatus
    ValveCycleOnOff
    ValvePower
    ValvePlok
    HeaterMode
    HeaterTemperature
    HeaterStatus
    CloseProfile
    OpenProfile
    Stroke
    CloseVoltage
    CloseTime
    OpenTime
    DisplayInfo
    AlarmInfo
    ResetAlarm

End Enum

''' <summary>
''' PicoValve回應資料
''' </summary>
''' <remarks></remarks>
Public Structure sPicoValveCommandResponseData
    ''' <summary>
    ''' 回傳狀態
    ''' </summary>
    ''' <remarks></remarks>
    Dim Status As Boolean
    ''' <summary>
    ''' 資料(未拆解)
    ''' </summary>
    ''' <remarks></remarks>
    Dim STR As String
    ''' <summary>
    ''' 資料(已拆解)
    ''' </summary>
    ''' <remarks></remarks>
    Dim value As String
End Structure

''' <summary>
''' PicoValve 閥狀態資料
''' </summary>
''' <remarks></remarks>
''' 
Public Structure sPicoValveStatus
    Dim Status As String
    Dim sValvePower As String
    'Dim STR As String
    Dim sMode As String
    Dim sPulse As String
    Dim sCycle As String
    Dim sCount As String
    Dim sProfileRise As String
    Dim sProfileFall As String
    Dim sStroke As String
    Dim sUpRampTime As String
    Dim sDwnRampTime As String
    Dim sCloseVoltage As String
    Dim sNumShots As String
End Structure


''' <summary>
''' PicoValve 溫度狀態資料
''' </summary>
''' <remarks></remarks>
Public Structure sPicoValveHeaterStatus
    Dim Status As String
    Dim sMode As String
    Dim sSet As String
    Dim sACT As String
    Dim sStack As String
End Structure

Public Interface IValveControllerInterface
    ReadOnly Property ValveMode As sPicoValveCommandResponseData
    ReadOnly Property ValveDispenseCount As sPicoValveCommandResponseData
    ReadOnly Property OpenTime As sPicoValveCommandResponseData
    ReadOnly Property ValveOnTime As sPicoValveCommandResponseData
    ReadOnly Property CloseTime As sPicoValveCommandResponseData
    ReadOnly Property ValveOffTime As sPicoValveCommandResponseData
    ReadOnly Property ValveStatus As sPicoValveStatus
    ReadOnly Property ValveCycleONOFFStatus As sPicoValveCommandResponseData
    ReadOnly Property ValvePower As sPicoValveCommandResponseData
    ReadOnly Property ValvePlok As sPicoValveCommandResponseData
    ReadOnly Property HeaterMode As sPicoValveCommandResponseData
    ReadOnly Property HeaterTemperature As sPicoValveCommandResponseData
    ReadOnly Property HeaterStatus As sPicoValveHeaterStatus
    ReadOnly Property CloseProfile As sPicoValveCommandResponseData
    ReadOnly Property OpenProfile As sPicoValveCommandResponseData
    ReadOnly Property Stroke As sPicoValveCommandResponseData
    ReadOnly Property CloseVoltage As sPicoValveCommandResponseData
    ReadOnly Property DisplayInfo As sPicoValveCommandResponseData
    ReadOnly Property AlarmInfo As sPicoValveCommandResponseData
    ReadOnly Property ResetAlarm As sPicoValveCommandResponseData

    Property mCommandResponse As Windows.Forms.TextBox
    ''' <summary>[Error Message]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ErrMsg As String
    ''' <summary>[忙碌中]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsBusy As Boolean
    ''' <summary>TimeOut(逾時)</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsTimeOut As Boolean
    ''' <summary>設定Timeout時間</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TimeoutTimer As Integer
    ''' <summary>Is Port Open?</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property PortIsOpen As Boolean
    Sub Dispose()
    ''' <summary>ComPort Initial</summary>
    ''' <param name="PortName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Initial(ByVal PortName As String, ByVal BaudRate As String) As Boolean
    ''' <summary>關閉ComPort</summary>
    ''' <remarks></remarks>
    Sub Close()
    ''' <summary> Send Command</summary>
    ''' <param name="CommandBtye"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SendCommandToSerialPort(ByVal CommandBtye As String) As Boolean
    ''' <summary>取得目前電腦的序列埠代號</summary>
    ''' <param name="PortIDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortIDs(ByRef PortIDs() As String) As Boolean
    Function SetValveMode(ByVal eModeType As enmValveModeType, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetValveDispenseCount(iDispenseCount As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetOpenTime(iOpenTime As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetValveOnTime(dVavleOnTime As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetCloseTime(iCloseTime As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetValveOffTime(dVavleOffTime As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetValveCycleTime(dVavleOffTime As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function GetValveStatus(Optional ByRef sVavleStatus As sPicoValveStatus = Nothing, Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetCycleOnOff(bCycleOnOff As Boolean, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetValvePower(bValvePower As Boolean, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetValvePlok(iPlokTime As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetHeaterMode(iHeatherMode As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetHeaterTemperature(dHeaterTemperature As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function GetHeaterStatus(Optional ByRef sHeaterStatus As sPicoValveHeaterStatus = Nothing, Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetCloseProfile(iSelect As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetOpenProfile(iSelect As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetStrokeValve(dStrokeValve As Double, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function SetCloseVoltage(iCloseVoltage As Integer, Optional ByRef sResponseContent As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function GetDisplayInfo(Optional ByRef sDisplayInfo As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function GetAlarmInfo(Optional ByRef sAlarmInfo As String = "", Optional bWaitUntilResponse As Boolean = False) As enmCommandState
    Function ResetAlarmStatus() As enmCommandState


End Interface

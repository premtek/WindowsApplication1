Imports ProjectCore
Imports ProjectRecipe
Imports ProjectMotion

Public Interface ITriggerBoard

#Region "Definitions"
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
    Property TimeoutTimes As Integer
    ''' <summary>[Is Port Open?]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property PortIsOpen As Boolean
    ''' <summary>[是否初始化成功]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsInitialOK As Boolean
    ''' <summary>[f Command的續傳量(F Command資料續傳-->f Command)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property TransmissionResumingOfStepCount As Integer
    ''' <summary>[J Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property JetParamRecipe As sReceiveStatus

    ''' <summary>[G Command之回傳字串(將參數丟給Trigger Board)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property JetParameter As sReceiveStatus
    ''' <summary>[L Command之回傳字串(將Vision取像座標資料丟給Trigger Board)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property VisionRecipe As sReceiveStatus
    ''' <summary>[F Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property JetRecipe As sReceiveStatus
    ''' <summary>[f Command之回傳字串(將Jet Valve點膠資料丟給Trigger Board)(F Command資料續傳-->f Command)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property JetRecipeUseTransmissionResuming As sReceiveStatus
    ''' <summary>[T Command之回傳字串(固定頻率打點)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property CycleRecipe As sReceiveStatus
    ''' <summary>[P Command之回傳字串(固定間距打點)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property PitchRecipe As sReceiveStatus
    ''' <summary>[X Command之回傳字串(Dispensing Run)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property DispenseRun As sReceiveStatus
    ''' <summary>[D Command之回傳字串(Dummy Run)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property DummyRun As sReceiveStatus
    ''' <summary>[S Command之回傳字串(閥體溫度、膠管壓力、閥體電源開關)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Parameter As sReceiveStatus
    ''' <summary>[C Command之回傳字串(打點數)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property DispenseCounts As sReceiveStatus
    ''' <summary>[O Command之回傳字串(打點數)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property VisionCounts As sReceiveStatus
    ''' <summary>[V Command之回傳字串(韌體版本)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Version As sReceiveStatus
    ''' <summary>[B Command之回傳字串(點膠真實Cycle)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property CycleArray As sReceiveStatus
    ''' <summary>[E Command之回傳字串(異常代號)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ErrorCode As sReceiveStatus
    ''' <summary>[c Command之回傳字串(異常清除)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ResetAlarm As sReceiveStatus
    ''' <summary>[R Command之回傳字串(閥體溫度)]</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Temperature As sReceiveStatus


#End Region

#Region "Properties"
    Sub Dispose()

    ''' <summary>[ComPort Initial]</summary>
    ''' <param name="PortName"></param>
    ''' <param name="BaudRate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Initial(ByVal portName As String, ByVal baudRate As String) As Boolean

    ''' <summary>[關閉ComPort]</summary>
    ''' <remarks></remarks>
    Sub Close()

    ''' <summary>[Send Command]</summary>
    ''' <param name="CommandBtye"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SendCommandToSerialPort(ByVal commandBtye() As Byte) As Boolean

#End Region

#Region "Function"
    ''' <summary>[取得目前電腦的序列埠代號]</summary>
    ''' <param name="PortIDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortIDs(ByRef portIDs() As String) As Boolean

    ''' <summary>[J Command的資料串接]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AddJetParamRecipe(ByVal is1stStep As Boolean, ByVal zoneNo As Integer, ByVal patternStep As sTriggerJCmdStep, ByVal isLastStep As Boolean, Optional ByVal parameter As sTriggerJCmdParam = Nothing) As Boolean

    ''' <summary>[將點膠資料丟給Trigger Board(J Command)]</summary>
    ''' <param name="WaitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetJetParamRecipe(Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>[Parameter of Jet(只丟參數)(G Command)]</summary>
    ''' <param name="parameter"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetJetParameter(ByVal parameter As sTriggerGCmdParam, Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>[L Command的資料串接]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AddVisionRecipe(ByVal is1stStep As Boolean, ByVal patternStep As sTriggerVisionCmdStep, ByVal isLastStep As Boolean, Optional ByVal parameter As sTriggerVisionCmdParam = Nothing) As Boolean

    ''' <summary>[將取像座標資料丟給Trigger Board(L Command)]</summary>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetVisionRecipe(Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>[F Command的資料串接]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AddJetRecipe(ByVal is1stStep As Boolean, ByVal patternStep As sTriggerFCmdStep, ByVal isLastStep As Boolean, Optional ByVal parameter As sTriggerFCmdParam = Nothing) As Boolean

    ''' <summary>[將點膠資料丟給Trigger Board(F Command)]</summary>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetJetRecipe(Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>[f Command的資料串接(F Command資料續傳-->f Command)]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AddJetRecipeUseTransmissionResuming(ByVal is1stStep As Boolean, ByVal patternStep As sTriggerFCmdStep, ByVal isLastStep As Boolean, Optional ByVal parameter As sTriggerFCmdParam = Nothing) As Boolean

    ''' <summary>[將點膠資料丟給Trigger Board(f Command-->F Command的續傳)]</summary>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetJetRecipeByTransmissionResuming(Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>Dummy Run(D Command)
    ''' [Note]:Recipe丟完後-->Dummy Run-->Free Type Dispensing</summary>
    ''' <param name="dispType"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="zoneNo"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetDummyRun(ByVal dispType As enmTriggerDispType, ByVal valveNo As eValveWorkMode, ByVal zoneNo As Integer, Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>Free Type Dispensing (X Command)
    ''' [Note]:Recipe丟完後-->Dummy Run-->Free Type Dispensing</summary>
    ''' <param name="dispType"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="zoneNo"></param>
    ''' <param name="degree"></param>
    ''' <param name="reworkDotCounts"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetDispenseRun(ByVal dispType As enmTriggerDispType, ByVal valveNo As eValveWorkMode, ByVal zoneNo As Integer, ByVal degree As Decimal, ByVal reworkDotCounts As Integer, Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>[固定頻率打點 T Command(Cycle Time)]</summary>
    ''' <param name="parameter"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetCycleRecipe(ByVal parameter As sTriggerTPCmdParam, Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>[固定間距打點 P Command(Pitch)]</summary>
    ''' <param name="parameter"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetPitchRecipe(ByVal parameter As sTriggerTPCmdParam, Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>[設定閥體溫度]</summary>
    ''' <param name="valve"></param>
    ''' <param name="value"></param>
    ''' <param name="WaitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetTempture(ByVal valve As eValveWorkMode, ByVal value As Decimal, Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>[Purge開關]</summary>
    ''' <param name="valve"></param>
    ''' <param name="purgeOn"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetPurge(ByVal valve As eValveWorkMode, ByVal purgeOn As Boolean, Optional ByVal waitReturn As Boolean = False) As Boolean


    '20170920
    ''' <summary>[Tempture開關]</summary>
    ''' <param name="valve"></param>
    ''' <param name="TemptureOn"></param>
    ''' <param name="waitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetTemptureOnOff(ByVal valve As eValveWorkMode, ByVal TemptureOn As Boolean, Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>[設定膠管壓力]</summary>
    ''' <param name="valve"></param>
    ''' <param name="value"></param>
    ''' <param name="WaitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetPressure(ByVal valve As eValveWorkMode, ByVal value As Decimal, Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>[設定開啟閥的電源]</summary>
    ''' <param name="valve"></param>
    ''' <param name="powerOn"></param>
    ''' <param name="WaitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetValvePower(ByVal valve As eValveWorkMode, ByVal powerOn As Boolean, Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>
    ''' [回傳打點數]
    ''' [Note]:上一次Jetting ON~OFF之間打的Dot數量(Dispense Counts)</summary>
    ''' <param name="WaitReturn"></param>
    ''' <param name="dotCounts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDispenseCounts(Optional ByVal waitReturn As Boolean = False, Optional ByRef dotCounts As Long = 0) As Boolean

    ''' <summary>
    ''' [回傳打點數]
    ''' [Note]:上一次Jetting ON~OFF之間打的Dot數量(Vision Counts)</summary>
    ''' <param name="WaitReturn"></param>
    ''' <param name="dotCounts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetVisionCounts(Optional ByVal waitReturn As Boolean = False, Optional ByRef dotCounts As Long = 0) As Boolean

    ''' <summary>[傳回韌體版本]</summary>
    ''' <param name="WaitReturn"></param>
    ''' <param name="version"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetVersion(Optional ByVal waitReturn As Boolean = False, Optional ByRef version As String = "") As Boolean

    ''' <summary>[傳回點膠Cycle(真實)]</summary>
    ''' <param name="WaitReturn"></param>
    ''' <param name="cycleArray"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDispCycle(Optional ByVal waitReturn As Boolean = False, Optional ByRef cycleArray As String = "") As Boolean

    ''' <summary>[傳回異常代號]</summary>
    ''' <param name="WaitReturn"></param>
    ''' <param name="errorCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetErrorCode(Optional ByVal waitReturn As Boolean = False, Optional ByRef errorCode As String = "") As Boolean

    ''' <summary>[異常清除]</summary>
    ''' <param name="WaitReturn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetResetAlarm(Optional ByVal waitReturn As Boolean = False) As Boolean

    ''' <summary>[傳回閥體溫度]</summary>
    ''' <param name="waitReturn"></param>
    ''' <param name="tempArray"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTemperature(Optional ByVal waitReturn As Boolean = False, Optional ByRef tempArray As String = "") As Boolean

    '20171010
    ''' <summary>[傳回開關狀態]</summary>
    ''' <param name="waitReturn"></param>
    ''' <param name="tempArray"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSwitch(Optional ByVal waitReturn As Boolean = False, Optional ByRef tempArray As String = "") As Boolean
#End Region

End Interface

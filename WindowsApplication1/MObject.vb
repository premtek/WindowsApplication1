Imports ProjectCore
Imports ProjectIO
Imports ProjectFeedback
Imports ProjectRecipe
Imports ProjectAOI
Imports ProjectLaserInterferometer
Imports ProjectTriggerBoard
Imports ProjectValveController
Imports MapData

Public Module MObject '共用物件放置處
    Public resMain As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
    Public resSystemSet As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSystemSet))
    Public resDiag As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDiag))
    Public resDiagWetco As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDiagWetco))

    Public mSystem As Premtek.CSystem0Top


    ' ''' <summary>三點找圓心動作流程</summary>
    ' ''' <remarks></remarks>
    'Public Point3FindCenterAction As New CAction3PointFindCenter
    ''' <summary>三特徵點找圓心動作流程</summary>
    ''' <remarks></remarks>
    Public AlignPos3FindCenterAction As New CAction3AlignPosFindCenter
    ''' <summary>排膠清膠動作流程</summary>
    ''' <remarks></remarks>
    Public gPurgeAction As New CActionPurge
    ''' <summary>閥頭自動測高流程</summary>
    ''' <remarks></remarks>
    Public gValveHeightAutoSearchAction As New CActionValveHeightAutoSearch
    ''' <summary>CCD對閥XY自動校正流程</summary>
    ''' <remarks></remarks>
    Public gCCDValveAutoCalibXYAction As New CActionCCDValveAutoCalibXY
    ''' <summary>移動到安全位置流程</summary>
    ''' <remarks></remarks>
    Public gMoveToSafePosAction As New CActionMoveToSafePos
    ''' <summary>
    ''' 800AQ異常退料流程
    ''' </summary>
    ''' <remarks></remarks>
    Public gCActionAbnormalUnload800AQ As New CActionAbnormalUnload800AQ
    ''' <summary>換膠動作流程</summary>
    ''' <remarks></remarks>
    Public gActionChangeGlue As New CActionChangeGlue

    Public gWeight As MFunctionWeight
    '20170520
    ''' <summary>校正閥體參數</summary>
    ''' <remarks></remarks>
    Public gCalibrationValveParameter As New CCalibrationValveParameter

    ''' <summary>
    ''' 使用者權限資料庫
    ''' </summary>
    ''' <remarks></remarks>
    Public gSetUserLevel As CUserLevel

    Public Sub DisposeObject()
        gfrmMain = Nothing
        'gfrmRecipe00 = Nothing
        'gfrmRecipe01 = Nothing
        'gfrmRecipe02 = Nothing
        gfrmRecipe04 = Nothing
        'gfrmRecipe05 = Nothing
        'gfrmRecipe06 = Nothing
        'gfrmRecipe07 = Nothing
        'gfrmCalibrationCCD2FindHeight = Nothing
        gfrmCalibrationCCD2Valve1 = Nothing
        gfrmCalibrationCCD2Valve2 = Nothing
        gfrmCalibrationZHeight = Nothing
        'gfrmOpStatus = Nothing
        gfrmHelp = Nothing
        'gfrmRecipe02Area = Nothing
        'gfrmRecipe02Block = Nothing
        'gfrmRecipe02Multi = Nothing
        'gfrmRecipe02Workpiece = Nothing
        'gfrmAlarmList = Nothing

        gfrmCCDHandshake = Nothing
        'gfrmDebugTool = Nothing
        gfrmEngineMode = Nothing

        gfrmLogin = Nothing
        gfrmManual = Nothing
        'gfrmRecipe01AlignmentPR = Nothing
        'gfrmRecipe03AugerArc = Nothing
        'gfrmRecipe03AugerEndLine = Nothing
        'gfrmRecipe03AugerFirstLine = Nothing
        'gfrmRecipe03AugerLine = Nothing
        'gfrmRecipe03AugerMove = Nothing
        'gfrmRecipe03JetArc = Nothing
        'gfrmRecipe03JetLine = Nothing
        'gfrmRecipe03JetMove = Nothing
        'gfrmRecipe03JetPoint = Nothing
        'gfrmRecipe03JetCircle = Nothing
        'gfrmRecipe06CoverRate = Nothing
        'gfrmRecipe06InspectionPR = Nothing
        'gfrmSetPurview = Nothing
        gfrmSetUserLevel = Nothing
        gfrmSplashScreen = Nothing
        gfrmSystemSet = Nothing
        'gfrmUserAuth = Nothing

        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.ManualDispose()

    End Sub

    ''' <summary>DO接點設定表單</summary>
    ''' <remarks></remarks>
    Public frmDOCfg As frmDOConfig
    ''' <summary>DI接點設定表單</summary>
    ''' <remarks></remarks>
    Public frmDICfg As frmDIConfig
    ''' <summary>AI接點設定表單</summary>
    ''' <remarks></remarks>
    Public frmAICfg As frmAIConfig
    ''' <summary>AO接點設定表單</summary>
    ''' <remarks></remarks>
    Public frmAOCfg As frmAOConfig
    ' ''' <summary>IO設定介面</summary>
    ' ''' <remarks></remarks>
    'Public frmIOConfig As New frmIOSet
    ''' <summary>IO</summary>
    ''' <remarks></remarks>
    Public gfrmIOList As frmIOList
    Public gfrmIOTable As frmIOTable
    Public gfrmMain As frmMain

    'Public gfrmRecipe01 As frmRecipe01
    'Public gfrmRecipe02 As frmRecipe02
    'Public gfrmRecipe03 As frmRecipe03
    Public gfrmRecipe04 As frmRecipe04
    'Public gfrmRecipe05 As frmRecipe05
    'Public gfrmRecipe06 As frmRecipe06
    'Public gfrmRecipe07 As frmRecipe07

    '20170920
    ''' <summary>Pm</summary>
    ''' <remarks></remarks>
    Public gfrmValvePm As frmValvePm

    ''' <summary>設定自動校正參數介面</summary>
    ''' <remarks></remarks>
    Public gfrmSetAutoCalibration As frmSetAutoCalibration
    ' ''' <summary>測高校正</summary>
    ' ''' <remarks></remarks>
    'Public gfrmCalibrationCCD2FindHeight As frmCalibrationCCD2FindHeight
    ''' <summary>閥頭校正</summary>
    ''' <remarks></remarks>
    Public gfrmCalibrationCCD2Valve1 As frmCalibrationCCD2Valve1
    ''' <summary>閥頭校正</summary>
    ''' <remarks></remarks>
    Public gfrmCalibrationCCD2Valve2 As frmCalibrationCCD2Valve2
    ''' <summary>Z高校正</summary>
    ''' <remarks></remarks>
    Public gfrmCalibrationZHeight As frmCalibrationZHeight
    ''' <summary>
    ''' CCD1與測高感測器校正
    ''' </summary>
    ''' <remarks></remarks>
    Public gfrmCalibrationCCD1Laser As frmCalibrationCCD2Height
    ''' <summary>
    ''' CCD2與測高感測器校正
    ''' </summary>
    ''' <remarks></remarks>
    Public gfrmCalibrationCCD2Laser As frmCalibrationCCD2Height
    ''' <summary>
    ''' CCD3與測高感測器校正
    ''' </summary>
    ''' <remarks></remarks>
    Public gfrmCalibrationCCD3Laser As frmCalibrationCCD2Height
    ''' <summary>
    ''' CCD4與測高感測器校正
    ''' </summary>
    ''' <remarks></remarks>
    Public gfrmCalibrationCCD4Laser As frmCalibrationCCD2Height
    ''' <summary>接觸式校正</summary>
    ''' <remarks></remarks>
    Public gfrmContactCablication As frmContactCablication
    ''' <summary>
    ''' Stage1的精準度測量
    ''' </summary>
    ''' <remarks></remarks>
    Public gfrmMeasureStageVerification As frmStageVerification
    ''' <summary>
    ''' Stage1的精準度測量
    ''' </summary>
    ''' <remarks></remarks>
    Public gfrmMeasureStageVerification_new As frmStageVerificationNew
    ''' <summary>
    ''' CCD 影像校正
    ''' </summary>
    ''' <remarks></remarks>
    Public gfrmCalibrationCCDImage As frmCalibrationCCDImage
    ' ''' <summary>
    ' ''' CCD2 影像校正
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public gfrmCalibrationCCD2Image As frmCalibrationCCDImage
    ' ''' <summary>
    ' ''' CCD3 影像校正
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public gfrmCalibrationCCD3Image As frmCalibrationCCDImage
    ' ''' <summary>
    ' ''' CCD4 影像校正
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public gfrmCalibrationCCD4Image As frmCalibrationCCDImage
    ' ''' <summary>操作介面</summary>
    ' ''' <remarks></remarks>
    'Public gfrmOpStatus As frmOpStatus

    ''' <summary>Op操作介面</summary>
    ''' <remarks></remarks>
    Public gfrmUIViewer As frmOperator

    ''' <summary>DrawGraphics操作介面</summary>
    ''' <remarks></remarks>
    Public gfrmDrawGraphics As frmDrawGraphics

    ''' <summary>DrawMap操作介面</summary>
    ''' <remarks></remarks>
    Public gfrmMapData As clsMapData


    ''' <summary>NodeMap</summary>
    ''' <remarks></remarks>
    Public gfrmNodeMap As frmNode

    ' ''' <summary>Log介面</summary>
    ' ''' <remarks></remarks>
    'Public gfrmLog As frmLog
    ''' <summary>關於</summary>
    ''' <remarks></remarks>
    Public gfrmHelp As frmHelp

    'Public gfrmAlarmList As frmAlarmList
    ''' <summary>
    ''' 訊息層級設定
    ''' </summary>
    ''' <remarks></remarks>
    Public gfrmMessageConfig As frmMessageConfig
    ''' <summary>硬體互鎖層級設定</summary>
    ''' <remarks></remarks>
    Public gfrmInterlock As frmInterlockConfig

    Public gfrmCCDHandshake As frmCCDHandshake

    Public gfrmEngineMode As frmEngineMode
    Public gfrmDiagnosis As frmDiag
    Public gfrmDiagnosisWetco As frmDiagWetco
    Public gfrmDiagnosis350A As frmDiag350A
    Public gfrmLightTowerConfig As frmLightTowerConfig
    Public gfrmLogin As frmLogin
    Public gfrmManual As frmManual

    'Public gfrmRecipe06CoverRate As frmRecipe06CoverRate
    'Public gfrmRecipe06InspectionPR As frmRecipe06InspectionPR

    ''' <summary>Light連線設定</summary>
    ''' <remarks></remarks>
    Public gfrmSetLightConnection As frmSetLightConnection
    ''' <summary>CCD連線設定</summary>
    ''' <remarks></remarks>
    Public gfrmSetCCDConnection As frmSetCCDConnection
    ''' <summary>LaserReader連線設定</summary>
    ''' <remarks></remarks>
    Public gfrmSetLaserReaderConnection As frmSetLaserReaderConnection

    Public gfrmSetValveController As frmPicoValveControllerTest
    ''' <summary>觸發板連線設定</summary>
    ''' <remarks></remarks>
    Public gfrmSetTriggerConnection As frmSetTriggerConnection
    Public gfrmSetBalance As frmSetBalance
    ''' <summary>FMCS連線設定</summary>
    ''' <remarks></remarks>
    Public gfrmSetFMCS As frmSetFMCSConnection

    ''' <summary>步驟詳細參數</summary>
    ''' <remarks></remarks>
    Public gfrmRecipeStepParameter As frmRecipeStepParameter
    Public gfrmProgramControlLight As frmProgramControlLight

    Public gfrmSetUserLevel As frmSetUserLevel
    Public gfrmSplashScreen As frmSplashScreen
    Public gfrmSystemSet As frmSystemSet
    ''' <summary>回授設定</summary>
    ''' <remarks></remarks>
    Public gfrmFeedback As frmFeedback
    ''' <summary>秤重測試A</summary>
    ''' <remarks></remarks>
    Public gfrmWeight As frmWeight
    ''' <summary>秤重測試B</summary>
    ''' <remarks></remarks>
    Public gfrmWeightB As frmWeight

    '20160920
    ''' <summary>秤重測試Valve1</summary>
    ''' <remarks></remarks>
    Public gfrmWeightValve1 As frmWeight
    ''' <summary>秤重測試Valve2</summary>
    ''' <remarks></remarks>
    Public gfrmWeightValve2 As frmWeight
    ''' <summary>秤重測試Valve3</summary>
    ''' <remarks></remarks>
    Public gfrmWeightValve3 As frmWeight
    ''' <summary>秤重測試Valve4</summary>
    ''' <remarks></remarks>
    Public gfrmWeightValve4 As frmWeight

    ''' <summary>RecipeStep</summary>
    ''' <remarks></remarks>
    Public gfrmRecipe04Step As frmRecipe04Step

    ''' <summary>UIViewerPauseMotor</summary>
    ''' <remarks></remarks>
    Public gfrmUIViewerPauseMotor As frmOpMachine


    ' ''' <summary>UIViewerSignalOperation</summary>
    ' ''' <remarks></remarks>
    'Public gfrmUIViewerSignalOperation As frmUIViewerSignalOperation
    ''' <summary>Purge介面 取代為有CCD校正</summary>
    ''' <remarks></remarks>
    Public gfrmPurge As frmCalibrationPurge
    ''' <summary>天平介面 取代為有CCD校正</summary>
    ''' <remarks></remarks>
    Public gfrmWeightPosition As frmCalibrationFlowRate 'frmWeightPosition
    Public gfrmValveControler As frmPicoValveControllerTest
    Public gfrmClearGlue As frmCalibrationClearGlue
    'Public gfrmCCDCalibration As frmCCDCalibration
    ''' <summary>單一Pattern顯示</summary>
    ''' <remarks></remarks>
    Public gfrmSinglePattern As frmSinglePattern


    '20160901
    Public gfrmSetProcessTime As frmSetProcessTime

    Public gfrmSetValveAirPressure As frmSetValveAirPressure
    ''' <summary>
    ''' 系統對底層配接類別
    ''' </summary>
    ''' <remarks></remarks>
    Public gSysAdapter As New CSystemAdapter
    ''' <summary>DCS-W800AQ動作</summary>
    ''' <remarks></remarks>
    Public gConveyorW800AQ As New CActionConveyorDCSW800AQ
    ''' <summary>F230A動作</summary>
    ''' <remarks></remarks>
    Public gConveyorF230A As New CActionConveyorF230A
    ''' <summary>F350A動作</summary>
    ''' <remarks></remarks>
    Public gConveyor350A As New CActionConveyorF350A

    '''' <summary>Log存取用物件</summary>
    '''' <remarks></remarks>
    'Public gLogFile As New CLogFile()
    ' ''' <summary>Log資料記錄包 </summary>
    ' ''' <remarks></remarks>
    'Public gLogMap As New Dictionary(Of sLevelIndexCollection, sProcessLogParameter)
    ' ''' <summary>整盤參數暫存</summary>
    ' ''' <remarks></remarks>
    'Public gTrayLog As sMapLog
    ' ''' <summary>製程參數暫存</summary>
    ' ''' <remarks></remarks>
    'Public gParameter As sParameter
    ''' <summary>[程式Loop迴圈]</summary>
    ''' <remarks></remarks>
    Public ThreadRun As New CSystemThread()
    ''' <summary>IO運行用緒</summary>
    ''' <remarks></remarks>
    Public gSystemThread As New System.Threading.Thread(AddressOf ThreadRun.Action)


    'Old 用法保留
    '====================================================================================================
    ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的Initial狀態]</summary>
    ''' <remarks></remarks>
    Public gblnUpdateInitial As Boolean = False
    ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的Initial狀態 Jeffadd gblnUpdateInitialA 20160411]</summary>
    ''' <remarks></remarks>
    Public gblnUpdateInitialA As Boolean = False
    ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的Initial狀態 Jeffadd gblnUpdateInitialB 20160411]</summary>
    ''' <remarks></remarks>
    Public gblnUpdateInitialB As Boolean = False

    ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的Purge狀態]</summary>
    ''' <remarks></remarks>
    Public gblnUpdatePurge As Boolean = False
    ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的ChangeGlue狀態]</summary>
    ''' <remarks></remarks>
    Public gblnUpdateChangeGlue As Boolean = False
    ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的ClearGlue狀態]</summary>
    ''' <remarks></remarks>
    Public gblnUpdateClearGlue As Boolean = False
    ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的DispenserAutoSearch狀態]</summary>
    ''' <remarks></remarks>
    Public gblnDispenserAutoSearch As Boolean = False
    ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的DispenserCalibrationAction狀態]</summary>
    ''' <remarks></remarks>
    Public gblnDispenserCalibrationAction As Boolean = False

    ''' <summary>[判斷是否更新Main1 DispenserNo1AutoValveCalibrationAction 裡面的Calib狀態]</summary>
    ''' <remarks></remarks>
    Public gblnUpdateDispenserNo1AutoValveCalibrationAction As Boolean = False
    ''' <summary>[判斷是否更新Main1 DispenserNo2AutoValveCalibrationAction]</summary>
    ''' <remarks></remarks>
    Public gblnUpdateDispenserNo2AutoValveCalibrationAction As Boolean = False

    ''====================================================================================================
    ''UIViewer NEW用法 通通不要, 因為開關介面時間點太隨意...會不能更新
    ' ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的Initial狀態]</summary>
    ' ''' <remarks></remarks>
    'Public gblnInitial As Boolean
    ' ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的Purge狀態]</summary>
    ' ''' <remarks></remarks>
    'Public gblnPurge() As Boolean = {False, False, False, False}
    ' ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的ChangeGlue狀態]</summary>
    ' ''' <remarks></remarks>
    'Public gblnChangeGlue() As Boolean = {False, False, False, False}
    ' ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的ClearGlue狀態]</summary>
    ' ''' <remarks></remarks>
    'Public gblnClearGlue() As Boolean = {False, False, False, False}
    ' ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的ChangeGlue狀態]</summary>
    ' ''' <remarks></remarks>
    'Public gblnAutoSearch() As Boolean = {False, False, False, False}
    ' ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的ClearGlue狀態]</summary>
    ' ''' <remarks></remarks>
    'Public gblnCalibrationAction() As Boolean = {False, False, False, False}
    ' ''' <summary>[判斷是否更新Main1 TmrAutoRun裡面的Weight狀態]</summary>
    ' ''' <remarks></remarks>
    'Public gblnWeight() As Boolean = {False, False, False, False}

    ''====================================================================================================

    
    '由底層移回WindowsApplication1, 原因 機台使用哪些是由主程序決定, 與底層無關.
    ''' <summary>[讀取IO卡設定參數]</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadIOCard(ByVal strFileName As String) As Boolean
        Try
            gDOCollection.Cards.Load(strFileName)
            gDICollection.Cards.Load(strFileName)
            gAICollection.Cards.Load(strFileName)
            gEPVCollection.Cards.Load(strFileName)

            Return True
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002008), "Error_1002008", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002008) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "MIO@ReadIOCard")
            Return False
        End Try
    End Function

    '由底層移回WindowsApplication1, 原因 機台使用哪些是由主程序決定, 與底層無關.
    ''' <summary>[儲存IO卡參數]</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveIOCard(ByVal strFileName As String) As Boolean
        Try
            gDOCollection.Cards.Save(strFileName)
            gDICollection.Cards.Save(strFileName)
            gAOCollection.Cards.Save(strFileName)
            gAICollection.Cards.Save(strFileName)
            gEPVCollection.Cards.Save(strFileName)

            Return True
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002009), "Error_1002009", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002009) & ex.Message, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "MIOUse@ReadIOCard")
            Return False
        End Try
    End Function

#Region "IO卡初始化/關閉"
    '由底層移回WindowsApplication1, 原因 機台使用哪些是由主程序決定, 與底層無關.
    ''' <summary>[IO卡初始化]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InitialIOCard() As Boolean

        Try
            Dim strFileName = Application.StartupPath & "\System\" & MachineName & "\CardIO.ini"
            ReadIOCard(strFileName)
            SaveIOCard(strFileName)



            Select Case gSSystemParameter.RunMode
                Case enmRunMode.Simulation
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6000004), "INFO_6000004")
                    Return True

                Case enmRunMode.Run

                    Dim isDOCardInitialOK As Boolean
                    Dim isDICardInitialOK As Boolean
                    Dim isAOCardInitialOK As Boolean
                    Dim isAICardInitialOK As Boolean
                    Dim isEPVCardInitialOK As Boolean

                    isDOCardInitialOK = gDOCollection.Initial(gDOCollection.Cards.DOCardParameter)
                    isDICardInitialOK = gDICollection.Initial(gDICollection.Cards.DICardParameter)
                    isAOCardInitialOK = gAOCollection.Initial(gAOCollection.Cards.AOCardParameter)
                    isAICardInitialOK = gAICollection.Initial(gAICollection.Cards.AICardParameter)
                    isEPVCardInitialOK = gEPVCollection.Initial(gEPVCollection.Cards.Parameters)
                    gDICollection.Load(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigDI.ini") '路徑改向至System下
                    gDOCollection.Load(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigDO.ini") '路徑改向至System下
                    gAICollection.Load(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigAI.ini") '路徑改向至System下
                    gAOCollection.Load(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigAO.ini") '路徑改向至System下

                    gEPVCollection.Load(Application.StartupPath & "\System\" & MachineName & "\ConfigEPValve.ini")
                    gEPVCollection.Save(Application.StartupPath & "\System\" & MachineName & "\ConfigEPValve.ini")
                    gEPVCollection.LoadMap(Application.StartupPath & "\System\" & MachineName & "\ConfigEPValve.ini")
                    'The default device of project is demo device, users can choose other devices according to their needs.
                    If isDOCardInitialOK = False Then
                        gEqpMsg.AddHistoryAlarm("Error_1005000", "Advantech_OpenCard_DIO", , gMsgHandler.GetMessage(Error_1005000), eMessageLevel.Error) 'DO 初始化失敗!!
                        MsgBox(gMsgHandler.GetMessage(Error_1005000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Return False
                    End If

                    If isDICardInitialOK = False Then
                        gEqpMsg.AddHistoryAlarm("Error_1004000", "Advantech_OpenCard_DIO", , gMsgHandler.GetMessage(Error_1004000), eMessageLevel.Error) 'Di 初始化失敗!!
                        MsgBox(gMsgHandler.GetMessage(Error_1004000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Return False
                    End If

                    If isAOCardInitialOK = False Then
                        gEqpMsg.AddHistoryAlarm("Error_1008000", "Advantech_OpenCard_DIO", , gMsgHandler.GetMessage(Error_1008000), eMessageLevel.Error) 'AO 初始化失敗!!
                        MsgBox(gMsgHandler.GetMessage(Error_1008000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Return False
                    End If

                    If isAICardInitialOK = False Then
                        gEqpMsg.AddHistoryAlarm("Error_1007000", "Advantech_OpenCard_DIO", , gMsgHandler.GetMessage(Error_1007000), eMessageLevel.Error) 'Ai 初始化失敗!!
                        MsgBox(gMsgHandler.GetMessage(Error_1007000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Return False
                    End If

                    If isEPVCardInitialOK = False Then
                        Return False
                    End If
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6000005), "INFO_6000005")
                    Return True

                Case Else
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6000005), "INFO_6000005")
                    Return True

            End Select

            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1003000), "Error_1003000", eMessageLevel.Error)
            gEqpMsg.AddHistoryAlarm("Error_1003000", "InitialIOCard", 0, gMsgHandler.GetMessage(Error_1003000), eMessageLevel.Error) 'IO Card 初始化失敗!!
            Return False
        End Try

    End Function
    '由底層移回WindowsApplication1, 原因 機台使用哪些是由主程序決定, 與底層無關.
    ''' <summary>[關閉IO卡]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CloseIOCard() As Boolean
        Try
            Select Case gSSystemParameter.RunMode
                Case enmRunMode.Simulation
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6000006), "INFO_6000006")
                    Return True

                Case enmRunMode.Run
                    If gDOCollection.IsCardIntialOK Then
                        gDOCollection.Close()
                    End If
                    If gDICollection.IsCardIntialOK Then
                        gDICollection.Close()
                    End If
                    If gAOCollection.IsCardIntialOK Then
                        gAOCollection.Close()
                    End If
                    If gAICollection.IsCardIntialOK Then
                        gAICollection.Close()
                    End If
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6000006), "INFO_6000006")
                    Return True

                Case Else
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6000006), "INFO_6000006")
                    Return True
            End Select
        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1003001) & ex.Message, "Error_1003001", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1003001) & ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "CloseIOCard")
            Return False
        End Try

    End Function
#End Region

    ''' <summary>新版本Recipe資料結構</summary>
    ''' <remarks></remarks>
    Public gRecipeUse As New Premtek.CRecipe(4)

    ''' <summary>新版本Map資料結構
    ''' </summary>
    ''' <remarks>如需選取哪些線要點不點, 應於此處理</remarks>
    Public gMapUse As Premtek.CRecipe

End Module

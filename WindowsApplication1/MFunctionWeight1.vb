Imports System.Math
Imports System.IO
Imports ProjectFeedback
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectMotion
Imports ProjectCore
Imports ProjectTriggerBoard

''' <summary>微量天平</summary>
''' <remarks></remarks>
Public Class MFunctionWeight

    Private MFunctionWeight_mItemCount(enmStage.Max) As Integer
    Private MFunctionWeight_WeightCounter(enmStage.Max) As Integer

    Private MFunctionWeight_BalanceCounter(enmStage.Max) As Integer
    Private MFunctionWeight_DoTolerance(enmStage.Max) As Boolean                    '[說明]:開啟反饋
    Private MFunctionWeight_ToleranceWeighingPointNumber(enmStage.Max) As Integer   '[說明]:紀錄反饋計算之新的Dots數
    ' Private MFunctionWeight_Weight(enmStage.Max, 300) As Double                     '[儲存每筆秤重資訊]   20170630
    Private MFunctionWeight_Weight(enmStage.Max) As List(Of Double)


    '20170505
    Private MFunctionWeight_CPK(enmStage.Max) As Double

    Private MFunctionWeight_mGetRezeroValue(enmStage.Max) As Double                 '[紀錄歸零次數重量值]
    Private MFunctionWeight_mGetValue(enmStage.Max) As Double                       '[紀錄秤重次數重量值]
    Private MFunctionWeight_BalanceNum(enmStage.Max) As Double
    Private MFunctionWeight_Balance(enmStage.Max, 10) As Double

    Private MFunctionWeight_TriggerCounter(enmStage.Max) As Double
    Private MFunctionWeight_SetCycleRecipeOK(enmStage.Max) As Boolean               '[說明]:確認SetCycleRecipeOK
    Private MFunctionWeight_SetDispenseRunOK(enmStage.Max) As Boolean               '[說明]:確認SetDispenseRunOK
    Private MFunctionWeight_SetDispenseResetOK(enmStage.Max) As Boolean             '[說明]:確認SetDispenseResetOK
    Private MFunctionWeight_DotWeightCheck(enmStage.Max) As Boolean                 '[說明]:開啟Dot 重量檢測
    Private MFunctionWeight_WeightFailDo(enmStage.Max) As Boolean                   '[說明]:重量fail是否繼續

    Private MFunctionWeight_WriteIOFileLock As New Object
    Private MFunctionWeight_Upper(enmStage.Max) As Double
    Private MFunctionWeight_Lower(enmStage.Max) As Double
    Private MFunctionWeight_DotWeight(enmStage.Max) As Double
    Private MFunctionWeight_retryTime(enmStage.Max) As Integer
    Dim GetValueBuffer As Double

    '20170620
    Private mTriggerCmdFailCount(enmStage.Max) As Integer                               '[紀錄資料傳輸異常次數]
    ' Dim mCyleParam As sTriggerTPCmdParam

    '觸發板用資料
    Dim mCyleParam(enmStage.Max) As sTriggerTPCmdParam

    ''' <summary>紀錄動作用閥號</summary>
    ''' <remarks></remarks>
    Dim mValveNo(enmStage.Max) As enmValve

    Public ReadOnly Property GetMFunctionWeight_WeightCounter(eStage As enmStage)
        Get
            If (eStage <= enmStage.Max) Then
                Return MFunctionWeight_WeightCounter(eStage)
            Else
                Return 0
            End If
        End Get
    End Property
    Public Sub InitialMFunctionWeight_WeightCounter(eStage As enmStage)
        If (eStage = enmStage.Max) Then
            For index = enmStage.No1 To enmStage.Max
                MFunctionWeight_WeightCounter(index) = 0
            Next
        Else
            If (eStage <= enmStage.Max) Then
                MFunctionWeight_WeightCounter(eStage) = 0
            End If
        End If
    End Sub

    Sub New()
        For i = enmStage.No1 To enmStage.Max
            MFunctionWeight_Weight(i) = New List(Of Double)
        Next
    End Sub
    'jimmy 20170822 手動觸發秤重
    Private mbManualWeightAction As Boolean
    Public Property _bManualWeightAction() As Boolean
        Get
            Return mbManualWeightAction
        End Get
        Set(ByVal value As Boolean)
            mbManualWeightAction = value
        End Set
    End Property
    Private mManualSelectValve As eValveWorkMode
    Public Property _ManualSelectValve() As eValveWorkMode
        Get
            Return mManualSelectValve
        End Get
        Set(ByVal value As eValveWorkMode)
            mManualSelectValve = value
        End Set
    End Property
    'jimmy 20170822
    Private mSelectValveTemp As eValveWorkMode
    Public Sub SetTempValveMode(ByVal sys As sSysParam)
        mSelectValveTemp = sys.SelectValve
    End Sub
    '20170620
    Public Function SubDispStage_WeighingAction(ByRef sys As sSysParam, ByVal flowRateID As String, ByVal ValveID As String) As enmRunStatus

        Static PosX(enmStage.Max) As Double                             '[用來紀錄Purge前馬達之座標]
        Static PosY(enmStage.Max) As Double                             '[用來紀錄Purge前馬達之座標]
        Static PosZ(enmStage.Max) As Double                             '[用來紀錄Purge前馬達之座標]
        Static PosTilt(enmStage.Max) As Double                          '[用來紀錄Purge前馬達之座標]

        Static mStopWatchTimeOut(enmStage.Max) As Stopwatch
        Static mPurgeTime(enmStage.Max) As Double                       '實際出膠時間
        'Dim Upper As Double                                            '[說明]:重量上限
        'Dim Lower As Double                                            '[說明]:重量下限

        Static mSideStage(enmStage.Max) As enmStage                     '[另一側的StageNo]
        Static mSideParam(enmStage.Max) As sSysParam

        Dim mErrorMessage As String = ""

        'jimmy 20170822
        If mbManualWeightAction = True Then
            sys.SelectValve = mManualSelectValve
        End If

        '[說明]:計算秤重上下限 公式:重量 = 預設重量 + (預設重量 * (比率))
        MFunctionWeight_Upper(sys.StageNo) = Math.Round((gFlowRateDB(flowRateID).WeighingWeight + (gFlowRateDB(flowRateID).WeighingWeight * (gFlowRateDB(flowRateID).WeighingTolerance / 100))), 4) ' 原本小數點2位改成4位
        MFunctionWeight_Lower(sys.StageNo) = Math.Round((gFlowRateDB(flowRateID).WeighingWeight - (gFlowRateDB(flowRateID).WeighingWeight * (gFlowRateDB(flowRateID).WeighingTolerance / 100))), 4) ' 原本小數點2位改成4位


        Static DebugPrint As Boolean = False
        Dim mAxisBState(enmStage.Max) As CommandStatus                                      '[Tilt軸的狀態]
        'Static old_sys(enmStage.Max) As Integer
        'If old_sys(sys.StageNo) <> sys.SysNum Then
        '    Debug.Print("SubDispStage_WeighingAction:" & sys.SysNum)
        '    old_sys(sys.StageNo) = sys.SysNum
        'End If


        With sys
            Select Case .SysNum
                Case sSysParam.SysLoopStart
                    mValveNo(sys.StageNo) = sys.SelectValve

                    If (DebugPrint) Then
                        Debug.Print("StageNo:" & .StageNo.ToString() & " Run Case: " & .SysNum)
                    End If

                    '初始化值'------------------------------------
                    MFunctionWeight_retryTime(.StageNo) = 0
                    mStopWatchTimeOut(.StageNo) = New Stopwatch()
                    mStopWatchTimeOut(.StageNo).Start()
                    MFunctionWeight_WeightCounter(.StageNo) = 0

                    MFunctionWeight_DoTolerance(.StageNo) = gFlowRateDB(flowRateID).WeighingEnableDoAverageWeight
                    MFunctionWeight_DotWeightCheck(.StageNo) = gFlowRateDB(flowRateID).WeighingEnableDoAverageDot
                    MFunctionWeight_WeightFailDo(.StageNo) = gFlowRateDB(flowRateID).WeighingEnableProductionRunFail


                    '20170630

                    ' MFunctionWeight_Weight(enmStage.Max) As List(Of Double)
                    'For i = enmStage.No1 To enmStage.Max
                    '    MFunctionWeight_Weight(i) = New List(Of Double)
                    'Next

                    MFunctionWeight_Weight(sys.StageNo).Clear()


                    If gFlowRateDB.ContainsKey(flowRateID) = True Then
                        gEPVCollection.SetValue(.StageNo, .SelectValve, eEPVPressureType.Syringe, gFlowRateDB(flowRateID).WeighingPressure, False)
                        If gFlowRateDB(flowRateID).WeighingPressure <> 0 Then
                            'gSysAdapter.SetSyringeAirPressure(.StageNo, .SelectValve, gFlowRateDB(flowRateID).WeighingPressure, False)
                        Else
                            '不做預設值保護
                            'gSysAdapter.SetSyringeAirPressure(.StageNo, .SelectValve, gFlowRateDB(flowRateID).WeighingPressure, False)
                            gEqpMsg.AddHistoryAlarm("Warn_3000073", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3000073), eMessageLevel.Warning)
                            sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Running
                            'gSysAdapter.SetSyringeAirPressure(.StageNo, .SelectValve, 0.1, False)
                        End If
                    Else
                        mErrorMessage = "FlowRateDB Name does not exist!!"
                        gEqpMsg.AddHistoryAlarm("Alarm_2000021", "WeighingAction", gMsgHandler.GetMessage(Alarm_2000021), eMessageLevel.Alarm)
                        .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If

                        Return enmRunStatus.Alarm
                    End If



                    '[說明]:紀錄當下的馬達座標
                    PosX(.StageNo) = CDbl(gCMotion.GetPositionValue(.AxisX))
                    PosY(.StageNo) = CDbl(gCMotion.GetPositionValue(.AxisY))
                    PosZ(.StageNo) = CDbl(gCMotion.GetPositionValue(.AxisZ))
                    PosTilt(.StageNo) = CDbl(gCMotion.GetPositionValue(.AxisB))

                    '[說明]:關閉出膠
                    gCMotion.DOOutput(.AxisX, enmMDO.GlueOn, enmCardIOONOFF.eOFF)
                    gCMotion.DOOutput(.AxisY, enmMDO.GlueOn, enmCardIOONOFF.eOFF)
                    gCMotion.DOOutput(.AxisZ, enmMDO.GlueOn, enmCardIOONOFF.eOFF)

                    '[Note]:關閉出膠
                    SetDispensingTrigger(.StageNo, .SelectValve, enmONOFF.eOff)

                    '[MachineType]
                    Select Case gSSystemParameter.MachineType
                        Case enmMachineType.eDTS300A, enmMachineType.eDTS330A
                            gCMotion.DOOutput(enmAxis.Y2Axis, enmMDO.GlueOn, enmCardIOONOFF.eOFF) '只有DTS雙軸同動才有
                    End Select
                    '[Module]
                    Select Case gSSystemParameter.StageUseValveCount
                        Case eMechanismModule.OneValveOneStage
                        Case eMechanismModule.TwoValveOneStage

                            ValveCylinderAction(sys.StageNo, sys.SelectValve, enmUpDown.Down) 'Soni + 2017.04.27 增加氣缸動作
                            mStopWatchTimeOut(sys.StageNo).Restart()
                            ''[說明]:第二隻Valve的氣缸上昇
                            'gDOCollection.GetSetState(enmDO.ValveCylUp1) = True
                            'gDOCollection.GetSetState(enmDO.ValveCylDown1) = False
                    End Select
                    '[MachineType]
                    Select Case gSSystemParameter.MachineType
                        Case enmMachineType.eDTS_2S2V, enmMachineType.DCSW_800AQ
                            Select Case .StageNo
                                Case enmStage.No1
                                    mSideStage(.StageNo) = enmStage.No2
                                    mSideParam(.StageNo) = gSYS(eSys.DispStage2)
                                Case enmStage.No2
                                    mSideStage(.StageNo) = enmStage.No1
                                    mSideParam(.StageNo) = gSYS(eSys.DispStage1)
                                Case enmStage.No3
                                    mSideStage(.StageNo) = enmStage.No4
                                    mSideParam(.StageNo) = gSYS(eSys.DispStage4)
                                Case enmStage.No4
                                    mSideStage(.StageNo) = enmStage.No3
                                    mSideParam(.StageNo) = gSYS(eSys.DispStage3)
                            End Select
                        Case Else
                    End Select
                    sys.SysNum = 1020

                Case 1020


                    If ValveCylinderSensor(sys.StageNo, sys.SelectValve, enmUpDown.Down) = True Then 'Soni + 2017.04.27 增加氣缸動作
                        .SysNum = 1100
                    ElseIf IsTimeOut(mStopWatchTimeOut(sys.StageNo), gSSystemParameter.StableTime.CheckSensorTimeout) Then 'Soni + 2017.09.01 等待到位逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2004001", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2004001), eMessageLevel.Error)
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If

                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm
                    End If



                Case 1100

                    If (DebugPrint) Then
                        Debug.Print("StageNo:" & .StageNo.ToString() & " Run Case: " & .SysNum)
                    End If

                    '[說明]:設定移動速度
                    If SetSpeed(sys) = False Then
                        .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    .SysNum = 1150

                Case 1150
                    '開氣壓_20170601修改

                    Call gSysAdapter.SetSyringePressure(.StageNo, .SelectValve, enmONOFF.eON) '[說明]開膠管氣壓 & 給壓力
                    .SysNum = 1200



                Case 1200

                    '[說明]:移動Z軸至安全移動位置 ValveWeightSafePosZ

                    If gCMotion.AbsMove(.AxisZ, gSSystemParameter.Pos.WeightCalibration(.StageNo).SafePosZ(.SelectValve)) <> CommandStatus.Sucessed Then
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Select Case .StageNo
                            Case enmStage.No1
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm

                                Return enmRunStatus.Alarm
                            Case enmStage.No2
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No3
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No4
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case Else
                                'Not Define Stage Error
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm

                                Return enmRunStatus.Alarm
                        End Select

                    End If
                    .SysNum = 1300



                Case 1300
                    '[說明]:等待Z軸到位  
                    If gCMotion.MotionDone(.AxisZ) <> CommandStatus.Sucessed Then

                        If gCMotion.IsMoveTimeOut(.AxisZ) = True Then
                            If mbManualWeightAction = True Then
                                mbManualWeightAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Select Case .StageNo
                                Case enmStage.No1
                                    gEqpMsg.AddHistoryAlarm("Error_1032004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                                    .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm

                                    Return enmRunStatus.Alarm
                                Case enmStage.No2
                                    gEqpMsg.AddHistoryAlarm("Error_1044004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                                    .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm

                                    Return enmRunStatus.Alarm
                                Case enmStage.No3
                                    gEqpMsg.AddHistoryAlarm("Error_1062004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                                    .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm

                                    Return enmRunStatus.Alarm
                                Case enmStage.No4
                                    gEqpMsg.AddHistoryAlarm("Error_1069004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                                    .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm

                                    Return enmRunStatus.Alarm
                                Case Else

                                    Return enmRunStatus.Alarm
                            End Select
                        End If

                        Return enmRunStatus.Running
                    End If

                    '沒有AxisB
                    If .AxisB <> -1 Then
                        MFunctionWeight_TriggerCounter(.StageNo) = 0
                        .SysNum = 1301
                    Else
                        MFunctionWeight_TriggerCounter(.StageNo) = 0
                        .SysNum = 1350
                    End If

                Case 1301

                    If gCMotion.AbsMove(.AxisB, GetSysParamTilePos(sys)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case enmStage.No1
                                gEqpMsg.AddHistoryAlarm("Error_1034000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1034000), eMessageLevel.Error)
                            Case enmStage.No2
                                gEqpMsg.AddHistoryAlarm("Error_1046000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1046000), eMessageLevel.Error)
                            Case enmStage.No3
                                gEqpMsg.AddHistoryAlarm("Error_1064000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1064000), eMessageLevel.Error)
                            Case enmStage.No4
                                gEqpMsg.AddHistoryAlarm("Error_1071000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1071000), eMessageLevel.Error)
                        End Select
                        .Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        .RunStatus = enmRunStatus.Alarm
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    .SysNum = 1302

                Case 1302

                    mAxisBState(sys.StageNo) = gCMotion.MotionDone(sys.AxisB)
                    If mAxisBState(sys.StageNo) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisB) Then
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1034004", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1046004", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1064004", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1071004", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071004), eMessageLevel.Error)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            If mbManualWeightAction = True Then
                                mbManualWeightAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                    Else
                        sys.SysNum = 1350
                    End If
                Case 1350
                    MFunctionWeight_SetDispenseResetOK(.StageNo) = False

                    '   If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                    If gTriggerBoard.SetResetAlarm(sys.StageNo, False) = True Then
                        sys.SysNum = 1355
                    End If
                    'ElseIf (mStopWatchTimeOut(.StageNo).ElapsedMilliseconds > gSSystemParameter.TimeOut1) Then
                    'gSyslog.Save(mErrorMessage(sys.StageNo))
                    'sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                    'sys.RunStatus = enmRunStatus.Alarm
                    'Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    'End If
                Case 1355
                    '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                    If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                        '[Note]:還在接收傳送資料中
                        If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                            '[Note]:超過時間還沒處裡完-->在下一次
                            mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                            If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                                'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                                Select Case sys.StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016001", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016101", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016201", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016301", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                End Select
                                sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                sys.RunStatus = enmRunStatus.Alarm
                                If mbManualWeightAction = True Then
                                    mbManualWeightAction = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            Else
                                mStopWatchTimeOut(.StageNo).Restart()
                                sys.SysNum = 1350
                            End If
                        End If
                    Else
                        '[Note]:檢查接收資料
                        If gTriggerBoard.ResetAlarm(sys.StageNo).Status = True Then
                            mTriggerCmdFailCount(sys.StageNo) = 0
                            MFunctionWeight_SetDispenseResetOK(.StageNo) = True
                            sys.SysNum = 1360
                        Else
                            '[Note]:查看收到的內容是????
                            Debug.Print("ResetAlarm: " & gTriggerBoard.ResetAlarm(sys.StageNo).STR)
                            mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                            If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                                'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                                Select Case sys.StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016001", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016101", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016201", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016301", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                End Select
                                sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                sys.RunStatus = enmRunStatus.Alarm
                                If mbManualWeightAction = True Then
                                    mbManualWeightAction = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            Else
                                mStopWatchTimeOut(.StageNo).Restart()
                                sys.SysNum = 1350
                            End If
                        End If
                    End If

                Case 1360

                    '  Dim mCyleParam As sTriggerTPCmdParam
                    gVolumneControl.ControlType = enmControlType.WeightToAir
                    mPurgeTime(.StageNo) = 0

                    Select Case gVolumneControl.ControlType
                        Case enmControlType.FlowToAir, enmControlType.VolumeToAir, enmControlType.WeightToAir
                            '[說明]:判斷是否為反饋流程
                            If MFunctionWeight_DoTolerance(.StageNo) = True Then
                                If MFunctionWeight_WeightCounter(.StageNo) = 0 Then '第一個Run   用設定的顆數先打一次
                                    If gFlowRateDB.ContainsKey(flowRateID) = True Then
                                        '[說明]:設定Jetting Valve Trigger Controller 為固定頻率打點模式
                                        mPurgeTime(.StageNo) = ((gFlowRateDB(flowRateID).WeighingCycleTime * gFlowRateDB(flowRateID).WeighingPointNumber)) + 500
                                        '[Note]:參數需要補
                                        ' mCyleParam(mValveNo(sys.StageNo)).CycleTime = gFlowRateDB(flowRateID).WeighingCycleTime * 1000
                                        mCyleParam(mValveNo(sys.StageNo)).DotCounts = gFlowRateDB(flowRateID).WeighingPointNumber
                                        ' mCyleParam(mValveNo(sys.StageNo)).PulseTime = gFlowRateDB(flowRateID).WeighingPulseTimes * 1000
                                        mCyleParam(mValveNo(sys.StageNo)).JetPressure = 0
                                        mCyleParam(mValveNo(sys.StageNo)).GluePressure = 0

                                        Select Case gJetValveDB(ValveID).ValveModel
                                            Case eValveModel.Advanjet
                                                'mCyleParam(mValveNo(sys.StageNo)).OpenTime = 1000
                                                'mCyleParam(mValveNo(sys.StageNo)).CloseTime = 1000
                                                'mCyleParam(mValveNo(sys.StageNo)).Stroke = 100
                                                'mCyleParam(mValveNo(sys.StageNo)).CloseVoltage = 100
                                                mCyleParam(mValveNo(sys.StageNo)).CycleTime = CInt(gJetValveDB(ValveID).Advanjet.CycleTime * 1000)
                                                mCyleParam(mValveNo(sys.StageNo)).PulseTime = CInt(gJetValveDB(ValveID).Advanjet.PulseTime * 1000)

                                            Case eValveModel.PicoPulse
                                                mCyleParam(mValveNo(sys.StageNo)).OpenTime = CInt(gJetValveDB(ValveID).PicoTouch.OpenTime * 1000)
                                                mCyleParam(mValveNo(sys.StageNo)).CloseTime = CInt(gJetValveDB(ValveID).PicoTouch.CloseTime * 1000)
                                                mCyleParam(mValveNo(sys.StageNo)).Stroke = gJetValveDB(ValveID).PicoTouch.Stroke
                                                mCyleParam(mValveNo(sys.StageNo)).CloseVoltage = gJetValveDB(ValveID).PicoTouch.CloseVoltage
                                        End Select
                                    Else
                                        mErrorMessage = "FlowRateDB Name does not exist!!"
                                        gEqpMsg.AddHistoryAlarm("Alarm_2000021", "WeighingAction", gMsgHandler.GetMessage(Alarm_2000021), eMessageLevel.Alarm)
                                        .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                        If mbManualWeightAction = True Then
                                            mbManualWeightAction = False
                                            .SelectValve = mSelectValveTemp
                                        End If
                                        Return enmRunStatus.Alarm
                                    End If
                                Else
                                    '反饋_用前一次計算出的顆數
                                    If gFlowRateDB.ContainsKey(flowRateID) = True Then
                                        '[說明]:設定Jetting Valve Trigger Controller 為固定頻率打點模式
                                        'PurgeTime透過Dot數計算出來
                                        '[Note]:參數需要補
                                        mPurgeTime(.StageNo) = (gFlowRateDB(flowRateID).WeighingCycleTime * MFunctionWeight_ToleranceWeighingPointNumber(.StageNo)) + 500
                                        mCyleParam(mValveNo(sys.StageNo)).CycleTime = gFlowRateDB(flowRateID).WeighingCycleTime * 1000
                                        mCyleParam(mValveNo(sys.StageNo)).DotCounts = MFunctionWeight_ToleranceWeighingPointNumber(.StageNo)
                                        mCyleParam(mValveNo(sys.StageNo)).PulseTime = gFlowRateDB(flowRateID).WeighingPulseTimes * 1000
                                        Select Case gJetValveDB(ValveID).ValveModel
                                            Case eValveModel.Advanjet
                                                mCyleParam(mValveNo(sys.StageNo)).OpenTime = 1000
                                                mCyleParam(mValveNo(sys.StageNo)).CloseTime = 1000
                                                mCyleParam(mValveNo(sys.StageNo)).Stroke = 100
                                                mCyleParam(mValveNo(sys.StageNo)).CloseVoltage = 100

                                            Case eValveModel.PicoPulse
                                                mCyleParam(mValveNo(sys.StageNo)).OpenTime = CInt(gJetValveDB(ValveID).PicoTouch.OpenTime * 1000)
                                                mCyleParam(mValveNo(sys.StageNo)).CloseTime = CInt(gJetValveDB(ValveID).PicoTouch.CloseTime * 1000)
                                                mCyleParam(mValveNo(sys.StageNo)).Stroke = gJetValveDB(ValveID).PicoTouch.Stroke
                                                mCyleParam(mValveNo(sys.StageNo)).CloseVoltage = gJetValveDB(ValveID).PicoTouch.CloseVoltage
                                        End Select
                                    Else
                                        mErrorMessage = "FlowRateDB Name does not exist!!"
                                        gEqpMsg.AddHistoryAlarm("Alarm_2000021", "WeighingAction", gMsgHandler.GetMessage(Alarm_2000021), eMessageLevel.Alarm)
                                        .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                        If mbManualWeightAction = True Then
                                            mbManualWeightAction = False
                                            .SelectValve = mSelectValveTemp
                                        End If
                                        Return enmRunStatus.Alarm
                                    End If
                                End If

                            ElseIf MFunctionWeight_DoTolerance(.StageNo) = False Then
                                If gFlowRateDB.ContainsKey(flowRateID) = True Then
                                    '[說明]:設定Jetting Valve Trigger Controller 為固定頻率打點模式
                                    '[Note]:參數需要補
                                    mPurgeTime(.StageNo) = ((gFlowRateDB(flowRateID).WeighingCycleTime * gFlowRateDB(flowRateID).WeighingPointNumber)) + 500
                                    mCyleParam(mValveNo(sys.StageNo)).CycleTime = gFlowRateDB(flowRateID).WeighingCycleTime * 1000
                                    mCyleParam(mValveNo(sys.StageNo)).DotCounts = gFlowRateDB(flowRateID).WeighingPointNumber
                                    mCyleParam(mValveNo(sys.StageNo)).PulseTime = gFlowRateDB(flowRateID).WeighingPulseTimes * 1000
                                    mCyleParam(mValveNo(sys.StageNo)).JetPressure = 0
                                    mCyleParam(mValveNo(sys.StageNo)).GluePressure = 0
                                    Select Case gJetValveDB(ValveID).ValveModel
                                        Case eValveModel.Advanjet
                                            mCyleParam(mValveNo(sys.StageNo)).OpenTime = 1000
                                            mCyleParam(mValveNo(sys.StageNo)).CloseTime = 1000
                                            mCyleParam(mValveNo(sys.StageNo)).Stroke = 100
                                            mCyleParam(mValveNo(sys.StageNo)).CloseVoltage = 100

                                        Case eValveModel.PicoPulse
                                            mCyleParam(mValveNo(sys.StageNo)).OpenTime = CInt(gJetValveDB(ValveID).PicoTouch.OpenTime * 1000)
                                            mCyleParam(mValveNo(sys.StageNo)).CloseTime = CInt(gJetValveDB(ValveID).PicoTouch.CloseTime * 1000)
                                            mCyleParam(mValveNo(sys.StageNo)).Stroke = gJetValveDB(ValveID).PicoTouch.Stroke
                                            mCyleParam(mValveNo(sys.StageNo)).CloseVoltage = gJetValveDB(ValveID).PicoTouch.CloseVoltage
                                    End Select
                                Else
                                    mErrorMessage = "FlowRateDB Name does not exist!!"
                                    gEqpMsg.AddHistoryAlarm("Alarm_2000021", "WeighingAction", gMsgHandler.GetMessage(Alarm_2000021), eMessageLevel.Alarm)
                                    .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                    If mbManualWeightAction = True Then
                                        mbManualWeightAction = False
                                        .SelectValve = mSelectValveTemp
                                    End If
                                    Return enmRunStatus.Alarm
                                End If

                            End If
                        Case enmControlType.FlowToPoints, enmControlType.VolumeToPoints, enmControlType.WeightToPoints

                        Case Else

                    End Select

                    mStopWatchTimeOut(.StageNo).Restart()
                    .SysNum = 1361

                Case 1361
                    MFunctionWeight_SetCycleRecipeOK(.StageNo) = False

                    If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                        If gTriggerBoard.SetCycleRecipe(.StageNo, mCyleParam(mValveNo(sys.StageNo)), False) = True Then
                            sys.SysNum = 1365
                        End If
                    ElseIf (mStopWatchTimeOut(.StageNo).ElapsedMilliseconds > gSSystemParameter.TimeOut1) Then

                        gSyslog.Save(mErrorMessage(sys.StageNo))
                        sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If

                Case 1365

                    '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                    If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                        '[Note]:還在接收傳送資料中
                        If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                            '[Note]:超過時間還沒處裡完-->在下一次
                            mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                            If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                                'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                                Select Case sys.StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016001", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016101", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016201", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016301", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                End Select
                                sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                sys.RunStatus = enmRunStatus.Alarm
                                If mbManualWeightAction = True Then
                                    mbManualWeightAction = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            Else
                                mStopWatchTimeOut(.StageNo).Restart()
                                sys.SysNum = 1361
                            End If
                        End If
                    Else
                        ''[Note]:檢查接收資料
                        If gTriggerBoard.CycleRecipe(sys.StageNo).Status Then
                            mTriggerCmdFailCount(sys.StageNo) = 0
                            mErrorMessage = "WeighingAction Set Cycle Recipe OK!!"
                            gSyslog.Save(mErrorMessage)
                            MFunctionWeight_SetCycleRecipeOK(.StageNo) = True
                            MFunctionWeight_TriggerCounter(.StageNo) = 0
                            sys.SysNum = 1370
                        Else
                            '[Note]:查看收到的內容是????
                            Debug.Print("CycleRecipe(T Cmd): " & gTriggerBoard.CycleRecipe(sys.StageNo).STR)
                            mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                            If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                                'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                                Select Case sys.StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Error_1016003", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Error_1016103", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Error_1016203", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Error_1016303", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                End Select
                                sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                sys.RunStatus = enmRunStatus.Alarm
                                If mbManualWeightAction = True Then
                                    mbManualWeightAction = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                            Else
                                mStopWatchTimeOut(.StageNo).Restart()
                                sys.SysNum = 1361
                            End If
                        End If
                    End If

                Case 1370
                    MFunctionWeight_SetDispenseRunOK(.StageNo) = False
                    If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                        '[說明]: enmValve.No1 部分 Stage1 Valve1 = 0 ,Valve2 = 1   設定部分還需測試
                        If gTriggerBoard.SetDispenseRun(sys.StageNo, enmTriggerDispType.CycleRecipe, .SelectValve, 0, 0, False) = True Then
                            sys.SysNum = 1375
                        End If
                    ElseIf (mStopWatchTimeOut(.StageNo).ElapsedMilliseconds > gSSystemParameter.TimeOut1) Then

                        sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If

                Case 1375

                    '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                    If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                        '[Note]:還在接收傳送資料中
                        If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                            '[Note]:超過時間還沒處裡完-->在下一次
                            mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                            If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                                'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                                Select Case sys.StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016001", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016101", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016201", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016301", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                End Select
                                '20170808
                                sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                sys.RunStatus = enmRunStatus.Alarm
                                If mbManualWeightAction = True Then
                                    mbManualWeightAction = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            Else
                                sys.SysNum = 1370
                            End If
                        End If
                    Else
                        ''[Note]:檢查接收資料
                        If gTriggerBoard.DispenseRun(sys.StageNo).Status Then
                            mTriggerCmdFailCount(sys.StageNo) = 0
                            MFunctionWeight_SetDispenseRunOK(.StageNo) = True
                            sys.SysNum = 1380
                        Else
                            '[Note]:查看收到的內容是????
                            '20170808
                            Debug.Print("DispenseRun(X Cmd): " & gTriggerBoard.DispenseRun(sys.StageNo).STR)
                            mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                            If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                                'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                                Select Case sys.StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Error_1016003", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Error_1016103", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Error_1016203", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Error_1016303", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                End Select
                                '20170808
                                sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                sys.RunStatus = enmRunStatus.Alarm
                                If mbManualWeightAction = True Then
                                    mbManualWeightAction = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            Else
                                sys.SysNum = 1370
                            End If
                        End If

                    End If

                Case 1380

                    '[說明]:檢查Trigger Board 命令是否下達完成
                    If MFunctionWeight_SetCycleRecipeOK(.StageNo) = False Or MFunctionWeight_SetDispenseRunOK(.StageNo) = False Or MFunctionWeight_SetDispenseResetOK(.StageNo) = False Then

                        MFunctionWeight_SetCycleRecipeOK(.StageNo) = False
                        MFunctionWeight_SetDispenseRunOK(.StageNo) = False

                        '[說明]:紀錄TriggerBoard IsBusy 裡面數值   20161125
                        If MFunctionWeight_DoTolerance(.StageNo) = True Then
                            '[說明]:參數保護
                            If gFlowRateDB.ContainsKey(flowRateID) = True Then
                                mErrorMessage = "TriggerBoard IsBusy Time Out!!!" &
                                      "CycleTime = " &
                                      gFlowRateDB(flowRateID).WeighingCycleTime * 1000 &
                                      "DotCounts = " & MFunctionWeight_ToleranceWeighingPointNumber(.StageNo) &
                                      "PulseTime = " &
                                       gFlowRateDB(flowRateID).WeighingPulseTimes * 1000
                            Else
                                mErrorMessage = "FlowRateDB Name does not exist!!"
                                gEqpMsg.AddHistoryAlarm("Alarm_2000021", "WeighingAction", gMsgHandler.GetMessage(Alarm_2000021), eMessageLevel.Alarm)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                If mbManualWeightAction = True Then
                                    mbManualWeightAction = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            End If
                        Else
                            '[說明]:參數保護
                            If gFlowRateDB.ContainsKey(flowRateID) = True Then
                                mErrorMessage = "TriggerBoard IsBusy Time Out!!!" &
                                      "CycleTime = " &
                                      gFlowRateDB(flowRateID).WeighingCycleTime * 1000 &
                                      "DotCounts = " & gFlowRateDB(flowRateID).WeighingPointNumber &
                                      "PulseTime = " &
                                      gFlowRateDB(flowRateID).WeighingPulseTimes * 1000
                            Else
                                mErrorMessage = "FlowRateDB Name does not exist!!"
                                gEqpMsg.AddHistoryAlarm("Alarm_2000021", "WeighingAction", gMsgHandler.GetMessage(Alarm_2000021), eMessageLevel.Alarm)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                If mbManualWeightAction = True Then
                                    mbManualWeightAction = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            End If
                        End If

                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1016003", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1016103", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1016203", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1016303", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Error)
                        End Select
                        .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    Else
                        '[說明]:確認Trigger命令正確
                        MFunctionWeight_SetCycleRecipeOK(.StageNo) = False
                        MFunctionWeight_SetDispenseRunOK(.StageNo) = False
                        .SysNum = 2000
                    End If
                    ''------------------------------------------------------------------------------------------------------------------------
                    '[Case 2000] 開始移動到位置

                Case 2000 '這裡應該要特別注意是否會有同時移動撞機的問題

                    If (DebugPrint) Then
                        Debug.Print("StageNo:" & .StageNo.ToString() & " Run Case: " & .SysNum)
                    End If

                    '[說明]:移動到微量天平之位置
                    If gCMotion.AbsMove(.AxisX, gSSystemParameter.Pos.WeightCalibration(.StageNo).ValvePosX(.SelectValve)) <> CommandStatus.Sucessed Then
                        SetDispensingTrigger(.StageNo, .SelectValve, enmONOFF.eOff) '[Note]:Dispenesing Trigger Off
                        'gSysAdapter.SetSyringePressure(.StageNo, .SelectValve, enmONOFF.eOff) '[說明]:關閉出膠 & 給壓力
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Select Case .StageNo
                            Case enmStage.No1
                                gEqpMsg.AddHistoryAlarm("Error_1030000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No2
                                gEqpMsg.AddHistoryAlarm("Error_1042000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No3
                                gEqpMsg.AddHistoryAlarm("Error_1060000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No4
                                gEqpMsg.AddHistoryAlarm("Error_1067000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case Else
                                gEqpMsg.AddHistoryAlarm("Error_1009006", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                        End Select
                    End If

                    If gCMotion.AbsMove(.AxisY, gSSystemParameter.Pos.WeightCalibration(.StageNo).ValvePosY(.SelectValve)) <> CommandStatus.Sucessed Then
                        SetDispensingTrigger(.StageNo, .SelectValve, enmONOFF.eOff) '[Note]:Dispenesing Trigger Off
                        'gSysAdapter.SetSyringePressure(.StageNo, .SelectValve, enmONOFF.eOff) '[說明]:關閉出膠 & 給壓力
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Select Case .StageNo
                            Case enmStage.No1
                                gEqpMsg.AddHistoryAlarm("Error_1031000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No2
                                gEqpMsg.AddHistoryAlarm("Error_1043000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No3
                                gEqpMsg.AddHistoryAlarm("Error_1061000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No4
                                gEqpMsg.AddHistoryAlarm("Error_1068000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case Else
                                gEqpMsg.AddHistoryAlarm("Error_1009006", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                        End Select
                    End If
                    .SysNum = 2100 'XY is Moving to Weigh Pos

                Case 2100
                    '[說明]:X,Y軸檢查停止 & 開Pump
                    If gCMotion.MotionDone(.AxisX) <> CommandStatus.Sucessed Then

                        If gCMotion.IsMoveTimeOut(.AxisX) Then
                            Select Case .StageNo
                                Case enmStage.No1
                                    gEqpMsg.AddHistoryAlarm("Error_1030004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                                Case enmStage.No2
                                    gEqpMsg.AddHistoryAlarm("Error_1042004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                                Case enmStage.No3
                                    gEqpMsg.AddHistoryAlarm("Error_1060004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                                Case enmStage.No4
                                    gEqpMsg.AddHistoryAlarm("Error_1067004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
                            End Select
                            SetDispensingTrigger(.StageNo, .SelectValve, enmONOFF.eOff) '[Note]:Dispenesing Trigger Off
                            'gSysAdapter.SetSyringePressure(.StageNo, .SelectValve, enmONOFF.eOff) '[說明]:關閉出膠 & 給壓力
                            .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                            If mbManualWeightAction = True Then
                                mbManualWeightAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If
                    If gCMotion.MotionDone(.AxisY) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(.AxisY) Then
                            Select Case .StageNo
                                Case enmStage.No1
                                    gEqpMsg.AddHistoryAlarm("Error_1031004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                                Case enmStage.No2
                                    gEqpMsg.AddHistoryAlarm("Error_1043004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                                Case enmStage.No3
                                    gEqpMsg.AddHistoryAlarm("Error_1061004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                                Case enmStage.No4
                                    gEqpMsg.AddHistoryAlarm("Error_1068004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                            End Select
                            SetDispensingTrigger(.StageNo, .SelectValve, enmONOFF.eOff) '[Note]:Dispenesing Trigger Off
                            'gSysAdapter.SetSyringePressure(.StageNo, .SelectValve, enmONOFF.eOff) '[說明]:關閉出膠 & 給壓力
                            .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                            If mbManualWeightAction = True Then
                                mbManualWeightAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If
                    .SysNum = 2300

                Case 2300
                    '[說明]:Z軸移至點膠高度
                    If gCMotion.AbsMove(.AxisZ, gSSystemParameter.Pos.WeightCalibration(.StageNo).ValvePosZ(.SelectValve)) <> CommandStatus.Sucessed Then
                        SetDispensingTrigger(.StageNo, .SelectValve, enmONOFF.eOff) '[Note]:Dispenesing Trigger Off
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        'gSysAdapter.SetSyringePressure(.StageNo, .SelectValve, enmONOFF.eOff) '[說明]:關閉出膠 & 給壓力
                        Select Case .StageNo
                            Case enmStage.No1
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No2
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No3
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No4
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case Else
                                gEqpMsg.AddHistoryAlarm("Error_1009006", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                        End Select
                    End If
                    .SysNum = 2400

                Case 2400
                    '[說明]:Z軸檢查停止 & Purge計時
                    If gCMotion.MotionDone(.AxisZ) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(.AxisZ) Then
                            Select Case .StageNo
                                Case enmStage.No1
                                    gEqpMsg.AddHistoryAlarm("Error_1032004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                                Case enmStage.No2
                                    gEqpMsg.AddHistoryAlarm("Error_1044004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                                Case enmStage.No3
                                    gEqpMsg.AddHistoryAlarm("Error_1062004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                                Case enmStage.No4
                                    gEqpMsg.AddHistoryAlarm("Error_1069004", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                            End Select
                            SetDispensingTrigger(.StageNo, .SelectValve, enmONOFF.eOff) '[Note]:Dispenesing Trigger Off
                            'gSysAdapter.SetSyringePressure(.StageNo, .SelectValve, enmONOFF.eOff) '[說明]:關閉出膠 & 給壓力
                            .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                            If mbManualWeightAction = True Then
                                mbManualWeightAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If

                    '微量天平歸零()
                    gBalanceCollection.Rezero(.BalanceNo) '到位再歸零, 避免微風擾動
                    mStopWatchTimeOut(.StageNo).Restart()
                    .SysNum = 2410

                Case 2410

                    If gFlowRateDB.ContainsKey(flowRateID) = True Then
                        '[說明]: 等待取值,等待馬達安定 
                        '20170720_單位換為sec*1000
                        If mStopWatchTimeOut(.StageNo).ElapsedMilliseconds > (gFlowRateDB(flowRateID).WeighingTableTimes * 1000) Then
                            If gBalanceCollection.RequestStableValue(.BalanceNo, 0, True) = True Then
                                .SysNum = 2500
                            End If
                        End If
                    Else
                        mErrorMessage = "FlowRateDB Name does not exist!!"
                        gEqpMsg.AddHistoryAlarm("Alarm_2000021", "WeighingAction", gMsgHandler.GetMessage(Alarm_2000021), eMessageLevel.Alarm)
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm
                    End If

                Case 2420 '重新歸零
                    If Not gBalanceCollection.IsBusy(.BalanceNo) Then
                        gBalanceCollection.Rezero(.BalanceNo) '到位再歸零, 避免微風擾動
                        .SysNum = 2430
                    End If

                Case 2430

                    If gBalanceCollection.RequestStableValue(.BalanceNo, 0, True) = True Then
                        .SysNum = 2500
                    End If

                Case 2500

                    If (DebugPrint) Then
                        Debug.Print("StageNo:" & .StageNo.ToString() & " Run Case: " & .SysNum & "Count: " & MFunctionWeight_BalanceNum(.StageNo))
                    End If

                    If Val(gBalanceCollection.GetValue(.BalanceNo)) = 0 Then
                        .SysNum = 2510
                    Else
                        .SysNum = 2420
                    End If


                Case 2510

                    If (DebugPrint) Then
                        Debug.Print("StageNo:" & .StageNo.ToString() & " Run Case: " & .SysNum)
                    End If

                    '[說明]:出膠
                    SetDispensingTrigger(.StageNo, .SelectValve, enmONOFF.eON)
                    gFMCSCollection.RecordStart(.StageNo, -1, -1) 'FMCS同步紀錄
                    mStopWatchTimeOut(.StageNo).Restart()
                    .SysNum = 2600

                Case 2600
                    '[說明]:等待Purge
                    If mStopWatchTimeOut(.StageNo).ElapsedMilliseconds > mPurgeTime(.StageNo) Then
                        '[說明]:在Purge前開啟膠桶壓力，Purge完膠就關閉
                        SetDispensingTrigger(.StageNo, .SelectValve, enmONOFF.eOff)
                        gFMCSCollection.RecordEnd(.StageNo, -1, -1) 'FMCS記錄結束

                        '[說明]:設定出膠壓力
                        If gFlowRateDB.ContainsKey(flowRateID) = True Then
                            .SysNum = 9000
                        Else
                            mErrorMessage = "FlowRateDB Name does not exist!!"
                            gEqpMsg.AddHistoryAlarm("Alarm_2000021", "WeighingAction", gMsgHandler.GetMessage(Alarm_2000021), eMessageLevel.Alarm)
                            .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                            If mbManualWeightAction = True Then
                                mbManualWeightAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                    End If

                Case 9000
                    mStopWatchTimeOut(.StageNo).Restart()
                    .SysNum = 9100

                Case 9100
                    If gFlowRateDB.ContainsKey(flowRateID) = True Then
                        '[說明]:點膠後等待時間   20170720_單位換成sec *1000
                        If mStopWatchTimeOut(.StageNo).ElapsedMilliseconds > (gFlowRateDB(flowRateID).WeighingBlanceSteadyTimes * 1000) Then
                            '讀取微量天平值
                            If gBalanceCollection.RequestStableValue(.BalanceNo, 0, True) = True Then
                                MFunctionWeight_Balance(.StageNo, MFunctionWeight_BalanceNum(.StageNo)) = Val(gBalanceCollection.GetValue(.BalanceNo))
                                mStopWatchTimeOut(.StageNo).Restart()
                                .SysNum = 9150
                            End If
                        End If
                    Else
                        mErrorMessage = "FlowRateDB Name does not exist!!"
                        gEqpMsg.AddHistoryAlarm("Alarm_2000021", "WeighingAction", gMsgHandler.GetMessage(Alarm_2000021), eMessageLevel.Alarm)
                        .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                Case 9120

                    If mStopWatchTimeOut(.StageNo).ElapsedMilliseconds > 200 Then
                        If Not gBalanceCollection.IsBusy(.BalanceNo) Then
                            If gBalanceCollection.RequestStableValue(.BalanceNo, 0, True) = True Then
                                MFunctionWeight_Balance(.StageNo, MFunctionWeight_BalanceNum(.StageNo)) = Val(gBalanceCollection.GetValue(.BalanceNo))

                                Debug.Print("Run: " & MFunctionWeight_WeightCounter(.StageNo).ToString & " " & MFunctionWeight_BalanceNum(.StageNo) + 1 & " " & MFunctionWeight_Balance(.StageNo, MFunctionWeight_BalanceNum(.StageNo)))

                                MFunctionWeight_BalanceNum(.StageNo) = MFunctionWeight_BalanceNum(.StageNo) + 1
                                .SysNum = 9150
                            End If
                        End If
                    End If

                Case 9150

                    If (DebugPrint) Then
                        Debug.Print("StageNo:" & .StageNo.ToString() & " Run Case: " & .SysNum & " Count: " & MFunctionWeight_BalanceNum(.StageNo))
                    End If

                    If MFunctionWeight_BalanceNum(.StageNo) > 9 Then
                        MFunctionWeight_BalanceNum(.StageNo) = 0
                        'For i = 0 To 9
                        '    MFunctionWeight_Balance(.StageNo, i) = Val(gBalanceCollection.GetValue(.BalanceNo))
                        '    Debug.Print("Run: " & MFunctionWeight_WeightCounter(.StageNo).ToString & " " & i.ToString & " " & MFunctionWeight_Balance(.StageNo, i))
                        'Next
                        Dim numberlength As Integer
                        If gSSystemParameter.MachineType = enmMachineType.DCSW_800AQ Then
                            numberlength = 2 '顯示小數點2位
                        Else
                            numberlength = 1 '顯示小數點1位
                        End If

                        Dim sumw As Double = 0
                        For j = 0 To 9
                            sumw = sumw + MFunctionWeight_Balance(.StageNo, j)
                        Next

                        '計算平均 依據讀值，則取相同位數小數點
                        '若大於目標值，小數點後2位數無條件捨去=>Ernest 建議
                        '若小於目標值，小數點後2位數無條件進位=>Ernest 建議
                        If (sumw / 10) >= gFlowRateDB(flowRateID).WeighingWeight Then
                            GetValueBuffer = Math.Round(sumw / 10, numberlength)
                        ElseIf (sumw / 10) < gFlowRateDB(flowRateID).WeighingWeight And (sumw / 10) <> 0 Then
                            GetValueBuffer = Math.Round(sumw / 10, numberlength) + 0.1
                        Else
                            GetValueBuffer = Math.Round(sumw / 10, numberlength)
                        End If

                        .SysNum = 9200
                    Else
                        If MFunctionWeight_BalanceNum(.StageNo) <> 0 Then

                            Dim mBetValue = Math.Abs(MFunctionWeight_Balance(.StageNo, MFunctionWeight_BalanceNum(.StageNo)) - MFunctionWeight_Balance(.StageNo, MFunctionWeight_BalanceNum(.StageNo) - 1))
                            'If mBetValue <= 0.1 Then
                            mStopWatchTimeOut(.StageNo).Restart()
                            '修改_不應該加count
                            'MFunctionWeight_BalanceNum(.StageNo) = MFunctionWeight_BalanceNum(.StageNo) + 1
                            .SysNum = 9120
                            'Else
                            '    MFunctionWeight_BalanceNum(.StageNo) = 0
                            '    '清空buffer
                            '    For i = 0 To 9
                            '        MFunctionWeight_Balance(.StageNo, i) = 0
                            '        'Debug.Print("Run: " & MFunctionWeight_WeightCounter(.StageNo).ToString & " " & i.ToString & " " & MFunctionWeight_Balance(.StageNo, i))
                            '    Next
                            '    mStopWatchTimeOut(.StageNo).Restart()
                            '    .SysNum = 9120
                            'End If
                        Else
                            mStopWatchTimeOut(.StageNo).Restart()
                            .SysNum = 9120
                        End If
                    End If

                Case 9200

                    If (DebugPrint) Then
                        Debug.Print("StageNo:" & .StageNo.ToString() & " Run Case: " & .SysNum)
                    End If


                    'Dim GetValueBuffer As Double = Val(gBalanceCollection.GetValue(.BalanceNo))

                    '--------------------------------------------------------------------------------------------------
                    '[說明]:作弊模式是否開啟,塞隨機值  20170712
                    If gSSystemParameter.Correction = True And gSSystemParameter.NonCorrection = False Then
                        If GetValueBuffer > (gSSystemParameter.CorrectionUSL) Or
                           GetValueBuffer < (gSSystemParameter.CorrectionLSL) Then
                            '[說明]:儲存重量 
                            MFunctionWeight_Weight(.StageNo).Add(GetValueBuffer)
                            MFunctionWeight_mGetValue(.StageNo) = GetValueBuffer
                        Else
                            'Dim num As Double
                            Dim randomValue As Double
                            Dim WeightUpper As Double = gSSystemParameter.CorrectionWeightUpper * 100
                            Dim WeightLower As Double = gSSystemParameter.CorrectionWeightLower * 100

                            randomValue = CInt(Math.Floor((WeightUpper - WeightLower + 1) * Rnd())) + WeightLower
                            randomValue = (randomValue / 100)

                            '[說明]:儲存重量 randon塞數值 
                            MFunctionWeight_Weight(.StageNo).Add(Format(System.Math.Round(randomValue, 1), "0.000"))
                            MFunctionWeight_mGetValue(.StageNo) = Format(System.Math.Round(randomValue, 1), "0.000")
                        End If
                        '--------------------------------------------------------------------------------------------------
                    ElseIf gSSystemParameter.Correction = False And gSSystemParameter.NonCorrection = True Then
                        '[說明]:重量超出範圍，重秤一次
                        If GetValueBuffer > MFunctionWeight_Upper(sys.StageNo) Or
                          GetValueBuffer < MFunctionWeight_Lower(sys.StageNo) Then
                            '[說明]:回到1350 重新秤重
                            If MFunctionWeight_retryTime(.StageNo) >= gSSystemParameter.CorrectionNum Then
                                '跳出訊息_超過設定次數上限
                                'MsgBox("Re-try次數超過設定上限")
                                Select Case sys.StageNo
                                    Case 0, 1
                                        gEqpMsg.AddHistoryAlarm("Alarm_2015002", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2015002), eMessageLevel.Alarm)
                                    Case 2, 3
                                        gEqpMsg.AddHistoryAlarm("Alarm_2015008", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2015008), eMessageLevel.Alarm)
                                End Select
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                If mbManualWeightAction = True Then
                                    mbManualWeightAction = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            Else
                                .SysNum = 1350
                                gBalanceCollection.Rezero(.BalanceNo)
                                MFunctionWeight_retryTime(.StageNo) = MFunctionWeight_retryTime(.StageNo) + 1
                                Return enmRunStatus.Running
                            End If
                        Else
                            '[說明]:儲存重量   20170630
                            MFunctionWeight_Weight(.StageNo).Add(GetValueBuffer) 'Soni / 2016.08.17 引數錯誤修正
                            MFunctionWeight_mGetValue(.StageNo) = GetValueBuffer
                        End If
                    Else '不作任何補償(當2個功能都為true 也跑到這邊 or 兩個False)
                        MFunctionWeight_Weight(.StageNo).Add(GetValueBuffer)
                        MFunctionWeight_mGetValue(.StageNo) = GetValueBuffer
                    End If
                    '--------------------------------------------------------------------------------------------------

                    '[說明]:紀錄天平現有重量   20161116
                    If .BalanceNo = enmBalance.No1 Then
                        gSSystemParameter.WeightMeasureA = gSSystemParameter.WeightMeasureA + GetValueBuffer
                    ElseIf .BalanceNo = enmBalance.No2 Then
                        gSSystemParameter.WeightMeasureB = gSSystemParameter.WeightMeasureB + GetValueBuffer
                    End If

                    Dim strSection As String = "WorkSize"
                    SaveIniString(strSection, "WeightmeasureA", CInt(gSSystemParameter.WeightMeasureA), Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
                    SaveIniString(strSection, "WeightmeasureB", CInt(gSSystemParameter.WeightMeasureB), Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")

                    '[說明]:更新 ListBox and Chart 
                    'DisplayData(sys)

                    If gFlowRateDB.ContainsKey(flowRateID) = True Then
                        '[說明]:Repeat Mode
                        If MFunctionWeight_WeightCounter(.StageNo) <= (gFlowRateDB(flowRateID).WeighingTimes - 1) Then
                            '[說明]:是否為最後一筆資料
                            If MFunctionWeight_WeightCounter(.StageNo) = (gFlowRateDB(flowRateID).WeighingTimes - 1) Then
                                SaveWeightData(True, sys, gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)) '寫檔
                                SaveWeighingDataCounter(sys) '一次Run寫一份檔案 Counter 數By Recipe 更新
                                '[說明]:最後一個RUN不必算反饋顆數(先Mark)
                                If DisplayResult(sys, gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)) Then
                                    'MFunctionWeight_WeightCounter(.StageNo) = 0 'Re-set Counter
                                    DisplayData(sys, gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve))
                                    MFunctionWeight_WeightCounter(.StageNo) = 0 'Re-set Counter
                                    If gFlowRateDB(flowRateID).WeighingTimes > 1 Then
                                        CalCPK(sys, gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve))
                                    End If
                                    'CalCPK(sys)
                                    .SysNum = 9250
                                Else
                                    .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                    If mbManualWeightAction = True Then
                                        mbManualWeightAction = False
                                        .SelectValve = mSelectValveTemp
                                    End If
                                    Return enmRunStatus.Alarm
                                End If


                            Else
                                '[說明]:寫檔紀錄
                                SaveWeightData(False, sys, gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve))
                                '[說明]:計算反饋點數,與平均重量
                                If DisplayResult(sys, gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)) Then
                                    DisplayData(sys, gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve))
                                    '秤重數加1
                                    MFunctionWeight_WeightCounter(.StageNo) = MFunctionWeight_WeightCounter(.StageNo) + 1
                                    .SysNum = 1350
                                    gBalanceCollection.Rezero(.BalanceNo)
                                    Return enmRunStatus.Running
                                Else
                                    .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                    If mbManualWeightAction = True Then
                                        mbManualWeightAction = False
                                        .SelectValve = mSelectValveTemp
                                    End If
                                    Return enmRunStatus.Alarm
                                End If
                            End If
                        End If
                    Else
                        gEqpMsg.AddHistoryAlarm("Alarm_2000021", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Alarm_2000021), eMessageLevel.Alarm)
                        MsgBox(gMsgHandler.GetMessage(Alarm_2000021), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                Case 9250
                    '儲存DB資料
                    WriteToDB(sys, gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve))
                    .SysNum = 9300

                Case 9300
                    '[說明]:移動Z軸至Up高度 & 關閉Pump
                    If gCMotion.AbsMove(.AxisZ, gSSystemParameter.Pos.SafePosZ) <> CommandStatus.Sucessed Then
                        SetDispensingTrigger(.StageNo, .SelectValve, enmONOFF.eOff) '[Note]:Dispenesing Trigger Off
                        'gSysAdapter.SetSyringePressure(.StageNo, .SelectValve, enmONOFF.eOff) '[說明]:關閉出膠 & 給壓力
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Select Case .StageNo
                            Case enmStage.No1
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No2
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No3
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case enmStage.No4
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Case Else
                                gEqpMsg.AddHistoryAlarm("Error_1009006", "WeighingAction", .SysNum, gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                        End Select
                    End If
                    .SysNum = 9400

                Case 9400
                    '[說明]等待停止
                    If gCMotion.MotionDone(.AxisZ) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(.AxisZ) Then
                            Call SetDispensingTrigger(.StageNo, .SelectValve, enmONOFF.eOff) '[Note]:Dispenesing Trigger Off
                            'Call gSysAdapter.SetSyringePressure(.StageNo, .SelectValve, enmONOFF.eOff) '[說明]:關閉出膠 & 給壓力
                            If mbManualWeightAction = True Then
                                mbManualWeightAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        Else
                            Return enmRunStatus.Running
                        End If
                    Else
                        'CalCPK(sys)
                        '20170520
                        'Call gSysAdapter.SetSyringePressure(.StageNo, .SelectValve, enmONOFF.eOff) '[說明]:關閉出膠 & 給壓力
                        .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Finish
                        .RunStatus = enmRunStatus.Finish
                        If mbManualWeightAction = True Then
                            mbManualWeightAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Finish
                    End If
            End Select
        End With
        Return enmRunStatus.Running
    End Function

    Public Sub ComputeWeighingAverageWeightDot(ByRef sys As sSysParam, ByVal flowRateID As String)

        Dim MeanDotsWeight As Double = 0 '[說明]:單點Dot 重量 

        '[說明]:計算單點點數重量
        If gFlowRateDB(flowRateID).WeighingTimes <= 0 Then
            MeanDotsWeight = 0
        Else
            If MFunctionWeight_DoTolerance(sys.StageNo) = True Then
                '反饋流程才需要確認是否為第一次or other
                If MFunctionWeight_WeightCounter(sys.StageNo) = 0 Then
                    MeanDotsWeight = MFunctionWeight_mGetValue(sys.StageNo) / gFlowRateDB(flowRateID).WeighingPointNumber
                Else
                    MeanDotsWeight = MFunctionWeight_mGetValue(sys.StageNo) / MFunctionWeight_ToleranceWeighingPointNumber(sys.StageNo)
                End If
            Else
                MeanDotsWeight = MFunctionWeight_mGetValue(sys.StageNo) / gFlowRateDB(flowRateID).WeighingPointNumber

            End If
        End If


        If MFunctionWeight_mGetValue(sys.StageNo) < 0.1 Then
            MFunctionWeight_ToleranceWeighingPointNumber(sys.StageNo) = gFlowRateDB(flowRateID).WeighingPointNumber
            Exit Sub
        End If

        gFlowRateDB(flowRateID).WeighingAverageWeightDot = MeanDotsWeight

        '20170505
        'gCRecipeAsa.StageParts(sys.StageNo).AverageWeightPerDot(sys.SelectValve) = MeanDotsWeight

        '[說明]:Dot重量>0 才需紀錄
        'If MeanDotsWeight > 0 Then
        '    Dim folderPath As String = Application.StartupPath & "\Database\Weight\"
        '    Dim fileName As String = folderPath & gFlowRateDB(flowRateID).Name & ".wdb"
        '    gFlowRateDB(flowRateID).Save(fileName)
        'End If

        '當已經執行到最後一次，不用算反饋顆數
        If MFunctionWeight_WeightCounter(sys.StageNo) < (gFlowRateDB(flowRateID).WeighingTimes - 1) Then
            If MeanDotsWeight > 0 Then
                MFunctionWeight_ToleranceWeighingPointNumber(sys.StageNo) = gFlowRateDB(flowRateID).WeighingWeight / MeanDotsWeight
            Else
                MFunctionWeight_ToleranceWeighingPointNumber(sys.StageNo) = gFlowRateDB(flowRateID).WeighingPointNumber
            End If
        End If

        'Debug.Print("Dot:" & MFunctionWeight_ToleranceWeighingPointNumber(sys.StageNo))

    End Sub

#Region "[Set Speed]"
    Public Function SetSpeed(ByRef sys As sSysParam) As Boolean

        '---------------------------------------------------------------------------------------------------
        '[SET LOW SPEED]
        If (sys.AxisX <> -1) Then
            If gCMotion.SetVelLow(sys.AxisX, gCMotion.AxisParameter(sys.AxisX).Velocity.VelLow) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1030014", "SetSpeed", , gMsgHandler.GetMessage(Error_1030014), eMessageLevel.Error)
                Return False
            End If
        End If
        If (sys.AxisY <> -1) Then
            If gCMotion.SetVelLow(sys.AxisY, gCMotion.AxisParameter(sys.AxisY).Velocity.VelLow) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1031014", "SetSpeed", , gMsgHandler.GetMessage(Error_1031014), eMessageLevel.Error)
                Return False
            End If
        End If
        If (sys.AxisZ <> -1) Then
            If gCMotion.SetVelLow(sys.AxisZ, gCMotion.AxisParameter(sys.AxisZ).Velocity.VelLow) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1032014", "SetSpeed", , gMsgHandler.GetMessage(Error_1032014), eMessageLevel.Error)
                Return False
            End If
        End If
        If (sys.AxisB <> -1) Then
            If gCMotion.SetVelLow(sys.AxisB, gCMotion.AxisParameter(sys.AxisB).Velocity.VelLow) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1034014", "SetSpeed", , gMsgHandler.GetMessage(Error_1034014), eMessageLevel.Error)
                Return False
            End If
        End If
        '---------------------------------------------------------------------------------------------------
        '[SET HIGH SPEED]
        If (sys.AxisX <> -1) Then
            If gCMotion.SetVelHigh(sys.AxisX, gCMotion.AxisParameter(sys.AxisX).Velocity.VelHigh) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1030013", "SetSpeed", , gMsgHandler.GetMessage(Error_1030013), eMessageLevel.Error)
                Return False
            End If
        End If
        If (sys.AxisY <> -1) Then
            If gCMotion.SetVelHigh(sys.AxisY, gCMotion.AxisParameter(sys.AxisY).Velocity.VelHigh) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1031013", "SetSpeed", , gMsgHandler.GetMessage(Error_1031013), eMessageLevel.Error)
                Return False
            End If
        End If

        If (sys.AxisZ <> -1) Then
            If gCMotion.SetVelHigh(sys.AxisZ, gCMotion.AxisParameter(sys.AxisZ).Velocity.VelHigh) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1032013", "SetSpeed", , gMsgHandler.GetMessage(Error_1032013), eMessageLevel.Error)
                Return False
            End If
        End If
        If (sys.AxisB <> -1) Then
            If gCMotion.SetVelHigh(sys.AxisB, gCMotion.AxisParameter(sys.AxisB).Velocity.VelHigh) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1034014", "SetSpeed", , gMsgHandler.GetMessage(Error_1034013), eMessageLevel.Error)
                Return False
            End If
        End If
        '---------------------------------------------------------------------------------------------------
        '[SET ACC]
        If (sys.AxisX <> -1) Then
            If gCMotion.SetAcc(sys.AxisX, gCMotion.AxisParameter(sys.AxisX).Velocity.Acc * gCMotion.AxisParameter(sys.AxisX).Velocity.AccRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1030011", "SetSpeed", , gMsgHandler.GetMessage(Error_1030011), eMessageLevel.Error)
                Return False
            End If
        End If
        If (sys.AxisY <> -1) Then
            If gCMotion.SetAcc(sys.AxisY, gCMotion.AxisParameter(sys.AxisY).Velocity.Acc * gCMotion.AxisParameter(sys.AxisY).Velocity.AccRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1031011", "SetSpeed", , gMsgHandler.GetMessage(Error_1031011), eMessageLevel.Error)
                Return False
            End If
        End If
        If (sys.AxisZ <> -1) Then
            If gCMotion.SetAcc(sys.AxisZ, gCMotion.AxisParameter(sys.AxisZ).Velocity.Acc * gCMotion.AxisParameter(sys.AxisZ).Velocity.AccRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1032011", "SetSpeed", , gMsgHandler.GetMessage(Error_1032011), eMessageLevel.Error)
                Return False
            End If
        End If
        If (sys.AxisB <> -1) Then
            If gCMotion.SetAcc(sys.AxisB, gCMotion.AxisParameter(sys.AxisB).Velocity.Acc * gCMotion.AxisParameter(sys.AxisB).Velocity.AccRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1034014", "SetSpeed", , gMsgHandler.GetMessage(Error_1034011), eMessageLevel.Error)
                Return False
            End If
        End If
        '---------------------------------------------------------------------------------------------------
        '[SET DEC]
        If (sys.AxisX <> -1) Then
            If gCMotion.SetDec(sys.AxisX, gCMotion.AxisParameter(sys.AxisX).Velocity.Dec * gCMotion.AxisParameter(sys.AxisX).Velocity.DecRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1030012", "SetSpeed", , gMsgHandler.GetMessage(Error_1030012), eMessageLevel.Error)
                Return False
            End If
        End If
        If (sys.AxisY <> -1) Then
            If gCMotion.SetDec(sys.AxisY, gCMotion.AxisParameter(sys.AxisY).Velocity.Dec * gCMotion.AxisParameter(sys.AxisY).Velocity.DecRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1031012", "SetSpeed", , gMsgHandler.GetMessage(Error_1031012), eMessageLevel.Error)
                Return False
            End If
        End If
        If (sys.AxisZ <> -1) Then
            If gCMotion.SetDec(sys.AxisZ, gCMotion.AxisParameter(sys.AxisZ).Velocity.Dec * gCMotion.AxisParameter(sys.AxisZ).Velocity.DecRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1032012", "SetSpeed", , gMsgHandler.GetMessage(Error_1032012), eMessageLevel.Error)
                Return False
            End If
        End If
        If (sys.AxisB <> -1) Then
            If gCMotion.SetDec(sys.AxisB, gCMotion.AxisParameter(sys.AxisB).Velocity.Dec * gCMotion.AxisParameter(sys.AxisB).Velocity.DecRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.AddHistoryAlarm("Error_1034014", "SetSpeed", , gMsgHandler.GetMessage(Error_1034012), eMessageLevel.Error)
                Return False
            End If
        End If
        '---------------------------------------------------------------------------------------------------
        Return True
    End Function
#End Region

    Public Sub SaveWeighingDataCounter(ByRef sys As sSysParam)

        Dim fileName As String = Application.StartupPath & "\System\" & MachineName & "\SysParam.ini"
        Dim strSection As String

        If gSSystemParameter.SystemWeighingDataCounter <= 500 Then
            gSSystemParameter.SystemWeighingDataCounter += 1
        Else
            gSSystemParameter.SystemWeighingDataCounter = 1
        End If

        '[說明]:刪除下一個檔案再建立儲存才不會堆疊 20160820
        Dim DeleyFilename As String
        Dim path As String
        path = "D:\PIIData\DataLog\" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & "\"
        DeleyFilename = path & "WeightData" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & "_" & (sys.StageNo + 1) & "_" & gSSystemParameter.SystemWeighingDataCounter & ".csv"

        '[說明]:檔案存在則刪除
        If File.Exists(DeleyFilename) Then
            File.Delete(DeleyFilename)
        End If

        strSection = "WorkSize"
        SaveIniString(strSection, "SystemWeighingDataCounter", CInt(gSSystemParameter.SystemWeighingDataCounter), fileName)

    End Sub

    Public Function CheckTolerance(ByRef sys As sSysParam, ByVal flowRateID As String) As Boolean

        'Dim MeanDotsWeight As Double = 0 '[說明]:單點Dot 重量 

        MFunctionWeight_Upper(sys.StageNo) = Math.Round((gFlowRateDB(flowRateID).WeighingWeight + (gFlowRateDB(flowRateID).WeighingWeight * (gFlowRateDB(flowRateID).WeighingTolerance / 100))), 4)
        MFunctionWeight_Lower(sys.StageNo) = Math.Round((gFlowRateDB(flowRateID).WeighingWeight - (gFlowRateDB(flowRateID).WeighingWeight * (gFlowRateDB(flowRateID).WeighingTolerance / 100))), 4)

        '[說明]:計算最後一次反饋資料   20160920 還須測試
        gFlowRateDB(flowRateID).WeighingAverageWeightDot = MFunctionWeight_mGetValue(sys.StageNo) / MFunctionWeight_ToleranceWeighingPointNumber(sys.StageNo)

        '[說明]:判斷總重量或單點重量超出範圍是否顯示fail選項
        If MFunctionWeight_WeightFailDo(sys.StageNo) = True Then
            '重量驗證功能與反饋綁在一起??
            If gFlowRateDB(flowRateID).WeighingEnableDoAverageWeight = True Then
                '[說明]:計算秤重上下限 公式:重量 = 預設重量 + ((預設重量 * (比率)))
                If MFunctionWeight_mGetValue(sys.StageNo) > MFunctionWeight_Upper(sys.StageNo) Or MFunctionWeight_mGetValue(sys.StageNo) < MFunctionWeight_Lower(sys.StageNo) Then
                    Select Case sys.StageNo
                        Case 0, 1
                            gEqpMsg.AddHistoryAlarm("Alarm_2015002", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2015002), eMessageLevel.Alarm)
                        Case 2, 3
                            gEqpMsg.AddHistoryAlarm("Alarm_2015008", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2015008), eMessageLevel.Alarm)
                    End Select
                    sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                    Return False
                End If
            End If


            If gFlowRateDB(flowRateID).WeighingEnableDoAverageDot = True Then

                If MFunctionWeight_DotWeight(sys.StageNo) > gFlowRateDB(flowRateID).WeighingWeightDotMax Or
                   MFunctionWeight_DotWeight(sys.StageNo) < gFlowRateDB(flowRateID).WeighingWeightDotMin Then
                    Select Case sys.StageNo
                        Case 0, 1
                            gEqpMsg.AddHistoryAlarm("Alarm_2015002", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2015002), eMessageLevel.Warning)
                        Case 2, 3
                            gEqpMsg.AddHistoryAlarm("Alarm_2015008", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2015008), eMessageLevel.Warning)
                    End Select
                    sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm

                    Return False
                End If

            End If
        Else : MFunctionWeight_WeightFailDo(sys.StageNo) = False
            If gFlowRateDB(flowRateID).WeighingEnableDoAverageWeight = True Then
                '[說明]:計算秤重上下限 公式:重量 = 預設重量 + ((預設重量 * (比率))/2)
                If MFunctionWeight_mGetValue(sys.StageNo) > MFunctionWeight_Upper(sys.StageNo) Or MFunctionWeight_mGetValue(sys.StageNo) < MFunctionWeight_Lower(sys.StageNo) Then
                    Select Case sys.StageNo
                        Case 0, 1
                            gEqpMsg.AddHistoryAlarm("Warn_3000064", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3000064), eMessageLevel.Warning)
                        Case 2, 3
                            gEqpMsg.AddHistoryAlarm("Warn_3000065", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3000065), eMessageLevel.Warning)
                    End Select
                    sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Running
                    Return True
                End If
            End If

            If gFlowRateDB(flowRateID).WeighingEnableDoAverageDot = True Then

                If MFunctionWeight_DotWeight(sys.StageNo) > gFlowRateDB(flowRateID).WeighingWeightDotMax Or
                   MFunctionWeight_DotWeight(sys.StageNo) < gFlowRateDB(flowRateID).WeighingWeightDotMin Then
                    Select Case sys.StageNo
                        Case 0, 1
                            gEqpMsg.AddHistoryAlarm("Warn_3000064", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3000064), eMessageLevel.Warning)
                        Case 2, 3
                            gEqpMsg.AddHistoryAlarm("Warn_3000065", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3000065), eMessageLevel.Warning)
                    End Select
                    sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Running
                    Return True
                End If
            End If
        End If

        Return True

    End Function
    Public Function DisplayResult(ByRef sys As sSysParam, ByVal flowRateID As String) As Boolean
        Dim sz As String = ""

        If MFunctionWeight_DoTolerance(sys.StageNo) = True Then
            '反饋流程才需要確認是否為第一次or other
            If MFunctionWeight_WeightCounter(sys.StageNo) = 0 Then
                MFunctionWeight_DotWeight(sys.StageNo) = MFunctionWeight_mGetValue(sys.StageNo) / gFlowRateDB(flowRateID).WeighingPointNumber
            Else
                MFunctionWeight_DotWeight(sys.StageNo) = MFunctionWeight_mGetValue(sys.StageNo) / MFunctionWeight_ToleranceWeighingPointNumber(sys.StageNo)
            End If
        Else
            '非反饋流程
            MFunctionWeight_DotWeight(sys.StageNo) = MFunctionWeight_mGetValue(sys.StageNo) / gFlowRateDB(flowRateID).WeighingPointNumber
        End If

        '顯示總重量與單點重量
        '反饋__將反饋Dot數也顯示
        If CheckTolerance(sys, gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)) Then
            If MFunctionWeight_WeightFailDo(sys.StageNo) = True Then
                sz = (MFunctionWeight_WeightCounter(sys.StageNo) + 1).ToString + ".Pass "
            Else
                sz = (MFunctionWeight_WeightCounter(sys.StageNo) + 1).ToString
            End If
            ComputeWeighingAverageWeightDot(sys, gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve))

            If gSSystemParameter.MachineType = enmMachineType.DCSW_800AQ Then
                sz = sz + " W:" + Format(MFunctionWeight_mGetValue(sys.StageNo), "0.00") + " Dot:" + Format(MFunctionWeight_DotWeight(sys.StageNo), "0.00000")
            Else
                sz = sz + " W:" + Format(MFunctionWeight_mGetValue(sys.StageNo), "0.0") + " Dot:" + Format(MFunctionWeight_DotWeight(sys.StageNo), "0.00000")
            End If

            If MFunctionWeight_DoTolerance(sys.StageNo) Then
                sz = sz + " FB_Dot:" + MFunctionWeight_ToleranceWeighingPointNumber(sys.StageNo).ToString
            End If

            If gfrmWeightValve1 Is Nothing Then
            ElseIf gfrmWeightValve1.IsDisposed Then
            Else
                Invokelistbox(gfrmWeightValve1.listWeight, sz)
            End If

            If gfrmUIViewer Is Nothing Then
            ElseIf gfrmUIViewer.IsDisposed Then
            Else
                Invokelistbox(gfrmUIViewer.lstWeightInfo, sz)
            End If

            Return True
        Else
            If MFunctionWeight_WeightFailDo(sys.StageNo) = True Then
                sz = (MFunctionWeight_WeightCounter(sys.StageNo) + 1).ToString + ".NG "
            Else
                sz = (MFunctionWeight_WeightCounter(sys.StageNo) + 1).ToString
            End If
            'ComputeWeighingAverageWeightDot(sys)

            If gSSystemParameter.MachineType = enmMachineType.DCSW_800AQ Then
                sz = sz + " W:" + Format(MFunctionWeight_mGetValue(sys.StageNo), "0.00") + " Dot:" + Format(MFunctionWeight_DotWeight(sys.StageNo), "0.00000")
            Else
                sz = sz + " W:" + Format(MFunctionWeight_mGetValue(sys.StageNo), "0.0") + " Dot:" + Format(MFunctionWeight_DotWeight(sys.StageNo), "0.00000")
            End If

            If MFunctionWeight_DoTolerance(sys.StageNo) Then
                sz = sz + " FB_Dot:" + MFunctionWeight_ToleranceWeighingPointNumber(sys.StageNo).ToString
            End If

            If gfrmWeightValve1 Is Nothing Then
            ElseIf gfrmWeightValve1.IsDisposed Then
            Else
                Invokelistbox(gfrmWeightValve1.listWeight, sz)
            End If

            If gfrmUIViewer Is Nothing Then
            ElseIf gfrmUIViewer.IsDisposed Then
            Else
                Invokelistbox(gfrmUIViewer.lstWeightInfo, sz)
            End If

            Return False
        End If

    End Function


    Public Sub DisplayData(ByRef sys As sSysParam, ByVal flowRateID As String)

        If gFlowRateDB(flowRateID).WeighingTolerance > 0 Then
            If gfrmWeightValve1 Is Nothing Then
            ElseIf gfrmWeightValve1.IsDisposed Then
            Else
                InvokeChartWeight(gfrmWeightValve1.chartWeight, MFunctionWeight_WeightCounter(sys.StageNo) + 1, MFunctionWeight_Upper(sys.StageNo), MFunctionWeight_Lower(sys.StageNo), MFunctionWeight_mGetValue(sys.StageNo)) 'Soni / 2016.08.17 引數錯誤修正

                InvokeChartDotsWeight(gfrmWeightValve1.chartDotsWeight, MFunctionWeight_WeightCounter(sys.StageNo) + 1, gFlowRateDB(flowRateID).WeighingWeightDotMax, gFlowRateDB(flowRateID).WeighingWeightDotMin, MFunctionWeight_DotWeight(sys.StageNo)) 'Soni / 2016.08.17 引數錯誤修正
            End If
        End If

    End Sub

    Public Sub SaveWeightData(ByVal ifinally As Boolean, ByRef sys As sSysParam, ByVal flowRateID As String)

        SyncLock (MFunctionWeight_WriteIOFileLock)
            Dim mStartTime As String = Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & " " &
            Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00")
            Dim mEndTime As String = " "
            Dim mDuringTime As String = " "
            Dim folderName As String
            Dim fileName As String
            Dim sw As System.IO.StreamWriter

            '檔案路徑確認是否存在
            folderName = "D:\PIIData\DataLog\" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & "\"

            If Not System.IO.Directory.Exists(folderName) Then
                System.IO.Directory.CreateDirectory(folderName)
            End If
            '20161122
            'fileName = folderName & "WeightData" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & gFlowRateDB(gCRecipeAsa.FlowRateName(sys.StageNo)).WeighingDataCounter & ".csv"
            fileName = folderName & "WeightData" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & "_" & (sys.StageNo + 1) & "_" & gSSystemParameter.SystemWeighingDataCounter & ".csv"

            Dim data As String = Format(MFunctionWeight_mItemCount(sys.StageNo) + 1, "00000") &
                "," & mStartTime &
                "," & (Val(sys.SelectValve) + 1) &
                "," & Val(MFunctionWeight_mGetValue(sys.StageNo)) &
                "," & Val(MFunctionWeight_mGetValue(sys.StageNo) / gFlowRateDB(flowRateID).WeighingPointNumber) &
                "," & gFlowRateDB(flowRateID).WeighingWeight &
                "," & gFlowRateDB(flowRateID).WeighingTolerance &
                ","

            '檔案存在
            If System.IO.File.Exists(fileName) Then
                Dim fInfo As New System.IO.FileInfo(fileName)
                sw = New IO.StreamWriter(fileName, True)
                sw.WriteLineAsync(data)
                sw.Close()
                MFunctionWeight_mItemCount(sys.StageNo) += 1
            Else
                '檔案不存在,另開新檔
                sw = New IO.StreamWriter(fileName, False)
                '項次 
                '1.Item(項目) 2.Times(紀錄時間) 3.DsipenserNo(閥編號) 4.Weight(重量) 5.Dots(重量) 6.RecipeWeighingWeight(重量) 6.RecipeWeighingTolerance(重量Ratio) 
                Dim header As String = "Item" & ",WrightTime" & ",DsipenserNo" & ",Weight" & ",DotsWeight" & ",RecipeWeighingWeight" & ",RecipeWeighingTolerance"
                MFunctionWeight_mItemCount(sys.StageNo) = 0

                data = Format(MFunctionWeight_mItemCount(sys.StageNo) + 1, "00000") & "," & mStartTime & "," & (Val(sys.SelectValve) + 1) & "," & Val(MFunctionWeight_mGetValue(sys.StageNo)) & "," & Val(MFunctionWeight_mGetValue(sys.StageNo) / gFlowRateDB(flowRateID).WeighingPointNumber) & "," & gFlowRateDB(flowRateID).WeighingWeight & "," & gFlowRateDB(flowRateID).WeighingTolerance & ","
                sw.WriteLineAsync(header)
                sw.WriteLineAsync(data)
                sw.Close()
                MFunctionWeight_mItemCount(sys.StageNo) += 1
            End If

            'Soni / 2016.08.17 修正最後一筆未存檔
            '[說明]:確認為最後一筆資料,Count清為零
            If ifinally = True Then
                MFunctionWeight_mItemCount(sys.StageNo) = 0
                Exit Sub
            End If
        End SyncLock

    End Sub

    '[說明]:新增反饋點數儲存修正單點點數重量 '20161102
    Public Sub SaveWeightDataEnd(ByVal ifinally As Boolean, ByRef sys As sSysParam, ByVal flowRateID As String)

        SyncLock (MFunctionWeight_WriteIOFileLock)

            Dim mStartTime As String = Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & " " &
            Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00")
            Dim mEndTime As String = " "
            Dim mDuringTime As String = " "
            Dim folderName As String
            Dim fileName As String
            Dim sw As System.IO.StreamWriter

            '檔案路徑確認是否存在
            folderName = "D:\PIIData\DataLog\" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & "\"

            If Not System.IO.Directory.Exists(folderName) Then
                System.IO.Directory.CreateDirectory(folderName)
            End If

            '20161122
            'fileName = folderName & "WeightData" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & gFlowRateDB(gCRecipeAsa.FlowRateName(sys.StageNo)).WeighingDataCounter & ".csv"
            fileName = folderName & "WeightData" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & "_" & (sys.StageNo + 1) & "_" & gSSystemParameter.SystemWeighingDataCounter & ".csv"


            Dim data As String = Format(MFunctionWeight_mItemCount(sys.StageNo) + 1, "00000") &
                "," & mStartTime & "," & (Val(sys.SelectValve) + 1) &
                "," & Val(MFunctionWeight_mGetValue(sys.StageNo)) &
                "," & Val(MFunctionWeight_mGetValue(sys.StageNo) / MFunctionWeight_ToleranceWeighingPointNumber(sys.StageNo)) &
                "," & gFlowRateDB(flowRateID).WeighingWeight &
                "," & gFlowRateDB(flowRateID).WeighingTolerance &
                "," & MFunctionWeight_ToleranceWeighingPointNumber(sys.StageNo) &
                ","

            '檔案存在
            If System.IO.File.Exists(fileName) Then
                Dim fInfo As New System.IO.FileInfo(fileName)
                sw = New IO.StreamWriter(fileName, True)
                sw.WriteLineAsync(data)
                sw.Close()
                MFunctionWeight_mItemCount(sys.StageNo) += 1
            Else
                '檔案不存在,另開新檔
                sw = New IO.StreamWriter(fileName, False)
                '項次 
                '1.Item(項目) 2.Times(紀錄時間) 3.DsipenserNo(閥編號) 4.Weight(重量) 5.Dots(重量) 6.RecipeWeighingWeight(重量) 6.RecipeWeighingTolerance(重量Ratio) 
                Dim header As String = "Item" & ",WrightTime" & ",DsipenserNo" & ",Weight" & ",DotsWeight" & ",RecipeWeighingWeight" & ",RecipeWeighingTolerance"
                MFunctionWeight_mItemCount(sys.StageNo) = 0
                '20161010
                data = Format(MFunctionWeight_mItemCount(sys.StageNo) + 1, "00000") &
                    "," & mStartTime & "," & (Val(sys.SelectValve) + 1) &
                    "," & Val(MFunctionWeight_mGetValue(sys.StageNo)) &
                    "," & Val(MFunctionWeight_mGetValue(sys.StageNo) / gFlowRateDB(flowRateID).WeighingPointNumber) &
                    "," & gFlowRateDB(flowRateID).WeighingWeight &
                    "," & gFlowRateDB(flowRateID).WeighingTolerance &
                    ","

                sw.WriteLineAsync(header)
                sw.WriteLineAsync(data)
                sw.Close()
                MFunctionWeight_mItemCount(sys.StageNo) += 1
            End If

            'Soni / 2016.08.17 修正最後一筆未存檔
            '[說明]:確認為最後一筆資料,Count清為零
            If ifinally = True Then
                MFunctionWeight_mItemCount(sys.StageNo) = 0
                Exit Sub
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' [判斷是否更新Main1 TmrAutoRun裡面的Weighing狀態]
    ''' </summary>
    ''' <remarks></remarks>
    Public gblnUpdateWeighing As Boolean = False
    ''' <summary>
    ''' [判斷移置Weighing位置後有沒有要移回來]
    ''' </summary>
    ''' <remarks></remarks>
    Public gblnWeighingComeBack As Boolean = True
    ''' <summary>秤重顆數計數</summary>
    ''' <remarks></remarks>
    Public intWeightingDeviceIndex As Integer




    Public Sub WriteToDB(ByRef sys As sSysParam, ByVal flowRateID As String)

        Dim MeanDotsWeight As Double = 0 '[說明]:平均Dot 重量 
        Dim Total As Double '總重量

        If MFunctionWeight_DoTolerance(sys.StageNo) Then
            '反饋流程__只用前一次data算單點重量 20170630
            'MeanDotsWeight = MFunctionWeight_Weight(sys.StageNo, gFlowRateDB(flowRateID).WeighingTimes - 1) / (MFunctionWeight_ToleranceWeighingPointNumber(sys.StageNo))
            MeanDotsWeight = MFunctionWeight_Weight(sys.StageNo)(gFlowRateDB(flowRateID).WeighingTimes - 1) / (MFunctionWeight_ToleranceWeighingPointNumber(sys.StageNo))
        Else
            '無反饋流程__用全部data算單點重量
            Total = 0
            For i = 0 To (gFlowRateDB(flowRateID).WeighingTimes - 1)
                'Total = Total + MFunctionWeight_Weight(sys.StageNo, i)
                Total = Total + MFunctionWeight_Weight(sys.StageNo)(i)   '20170630
            Next
            MeanDotsWeight = Total / ((gFlowRateDB(flowRateID).WeighingPointNumber) * (gFlowRateDB(flowRateID).WeighingTimes))
        End If

        gFlowRateDB(flowRateID).WeighingAverageWeightDot = MeanDotsWeight

        If gFlowRateDB(flowRateID).WeighingAverageWeightDot > 0 Then
            '將單點重量拋到gcRecipe_20170627
            If gFlowRateDB(flowRateID).WeighingEnableDoAverageDot = True Then
                If MeanDotsWeight > gFlowRateDB(flowRateID).WeighingWeightDotMax Or MeanDotsWeight < gFlowRateDB(flowRateID).WeighingWeightDotMin Then
                    '不更新單點重
                Else
                    gCRecipe.StageParts(sys.StageNo).AverageWeightPerDot(sys.SelectValve) = MeanDotsWeight
                End If

            Else '若不開启單點重量檢查機制則不做保護，直接寫入
                gCRecipe.StageParts(sys.StageNo).AverageWeightPerDot(sys.SelectValve) = MeanDotsWeight
            End If

            Dim folderPath As String = Application.StartupPath & "\Database\Weight\"
            Dim fileName As String = folderPath & gFlowRateDB(flowRateID).Name & ".wdb"
            gFlowRateDB(flowRateID).Save(fileName)
        End If
    End Sub

    '20170505
    Private Sub CalCPK(ByRef sys As sSysParam, ByVal flowRateID As String)
        Dim Total As Double '總重量
        Dim Mean As Double '平均值
        Dim Standerdiff As Double '標準差
        Dim CPK As Double 'CPK值
        Dim mLabelNo As Integer
        Dim cp As Double
        Dim ca As Double
        Dim Tolerance As Double


        '計算總重量
        Total = 0
        For i = 0 To (gFlowRateDB(flowRateID).WeighingTimes - 1)
            ' Total = Total + MFunctionWeight_Weight(sys.StageNo, i)
            Total = Total + MFunctionWeight_Weight(sys.StageNo)(i)   '20170630
        Next

        If Total = 0 Then
            Exit Sub
        End If

        '計算平均值
        Mean = Total / (gFlowRateDB(flowRateID).WeighingTimes)

        '標準差
        '先計算總合(根號裡面)
        Dim SumofAll As Double = 0
        For j = 0 To (gFlowRateDB(flowRateID).WeighingTimes - 1)
            'SumofAll = SumofAll + (MFunctionWeight_Weight(sys.StageNo, j) - Mean) ^ 2
            SumofAll = SumofAll + (MFunctionWeight_Weight(sys.StageNo)(j) - Mean) ^ 2   '20170630
        Next
        Standerdiff = (SumofAll / (gFlowRateDB(flowRateID).WeighingTimes)) ^ 0.5

        '計算CPK Soni / 2017.05.23 補括弧
        Tolerance = MFunctionWeight_Upper(sys.StageNo) - MFunctionWeight_Lower(sys.StageNo)
        cp = Tolerance / (6 * Standerdiff)
        ca = (Mean - gFlowRateDB(flowRateID).WeighingWeight) / (Tolerance / 2)

        MFunctionWeight_CPK(sys.StageNo) = cp * (1 - Math.Abs(ca))
        'MFunctionWeight_CPK(sys.StageNo) = ((MFunctionWeight_Upper(sys.StageNo) - MFunctionWeight_Lower(sys.StageNo)) / (6 * Standerdiff)) * (1 - Abs((Mean - gFlowRateDB(flowRateID).WeighingWeight) / (MFunctionWeight_Upper(sys.StageNo) - MFunctionWeight_Lower(sys.StageNo)) / 2))

        Debug.Print("總重量:" & Total)
        Debug.Print("平均值:" & Mean)
        Debug.Print("標準差:" & Standerdiff)
        Debug.Print("CPK:" & CPK)



        '標準差為0時，計算CPK會造成正負∞ 顯示訊息告知
        If Standerdiff = 0 Then
            gEqpMsg.AddHistoryAlarm("Warn_3000072", "WeighingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3000072), eMessageLevel.Warning)
            sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.Running
            'Select Case gSSystemParameter.LanguageType
            '    Case enmLanguageType.eEnglish
            '        MsgBox("Standard deviation is zero,CPK can not be calculated")
            '    Case enmLanguageType.eSimplifiedChinese
            '        MsgBox("标准差为0，无法计算CPK")
            '    Case enmLanguageType.eTraditionalChinese
            '        MsgBox("標準差為0，無法計算CPK")
            'End Select
            Exit Sub
        End If

        'MsgBox("CPK:" + Format(CPK, "0.00000"), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        If gfrmWeightValve1 Is Nothing Then
        ElseIf gfrmWeightValve1.IsDisposed Then
        Else
            InvokeLabel(gfrmWeightValve1.lblCPKData, Format(MFunctionWeight_CPK(sys.StageNo), "0.000"))
        End If

        If gfrmUIViewer Is Nothing Then
        ElseIf gfrmUIViewer.IsDisposed Then
        Else
            '[Note]:計算要在顯示在哪個欄位
            mLabelNo = (sys.StageNo * gSSystemParameter.StageUseValveCount) + sys.SelectValve
            Select Case mLabelNo
                Case 0
                    InvokeLabel(gfrmUIViewer.UcProcessInfo1.lblCPKDisplay1, Format(MFunctionWeight_CPK(mLabelNo), "0.000"))

                Case 1
                    InvokeLabel(gfrmUIViewer.UcProcessInfo1.lblCPKDisplay2, Format(MFunctionWeight_CPK(mLabelNo), "0.000"))

                Case 2
                    InvokeLabel(gfrmUIViewer.UcProcessInfo1.lblCPKDisplay3, Format(MFunctionWeight_CPK(mLabelNo), "0.000"))

                Case 3
                    InvokeLabel(gfrmUIViewer.UcProcessInfo1.lblCPKDisplay4, Format(MFunctionWeight_CPK(mLabelNo), "0.000"))

            End Select
        End If

    End Sub

End Class


Imports ProjectCore
Imports ProjectMotion
Imports ProjectIO
Imports ProjectRecipe
Imports ProjectAOI
Imports ProjectTriggerBoard

Public Class CActionCCDValveAutoCalibXY
    ''' <summary>[紀錄檢查Sensor之起始時間]</summary>
    ''' <remarks></remarks>
    Dim mCheckSensorStart(enmStage.Max) As Decimal
    Dim mPosX(enmStage.Max) As Decimal
    Dim mPosY(enmStage.Max) As Decimal
    Dim mPosZ(enmStage.Max) As Decimal
    ''' <summary>
    ''' [座標(B Axis-->Tilt Axis)]
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPosB(enmStage.Max) As Decimal
    ''' <summary>
    ''' 出膠開始時間
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPurgeStart(enmValve.Max) As Double
    Dim mLightCmdFailCount(enmStage.Max) As Integer
    ''' <summary>
    ''' [暫存光源數值]
    ''' </summary>
    Dim mLightValue1(enmStage.Max) As Integer
    ''' <summary>
    ''' [暫存光源數值]
    ''' </summary>
    Dim mLightValue2(enmStage.Max) As Integer
    ''' <summary>
    ''' [暫存光源數值]
    ''' </summary>
    Dim mLightValue3(enmStage.Max) As Integer
    ''' <summary>
    ''' [暫存光源數值]
    ''' </summary>
    ''' <remarks></remarks>
    Dim mLightValue4(enmStage.Max) As Integer
    ''' <summary>
    ''' [Tilt軸的狀態]
    ''' </summary>
    ''' <remarks></remarks>
    Dim mAxisBState(enmStage.Max) As CommandStatus

    Dim mTimeoutStopWatch(enmStage.Max) As Stopwatch
    ''' <summary>
    ''' [計算馬達安定時間]
    ''' </summary>
    ''' <remarks></remarks>
    Dim mMotorStableStopWatch(enmStage.Max) As Stopwatch
    ''' <summary>
    ''' [紀錄馬達安定時間]
    ''' </summary>
    ''' <remarks></remarks>
    Dim mMotorStableTime(enmStage.Max) As Decimal
    ''' <summary>
    ''' 觸發Ticket
    ''' </summary>
    ''' <remarks></remarks>
    Dim Ticket(enmStage.Max) As Integer
    Dim CalibrationX(enmStage.Max) As List(Of Double)               '[紀錄次數]   
    Dim CalibrationY(enmStage.Max) As List(Of Double)


    ''' <summary>
    ''' 校正用CCD場景名稱
    ''' </summary>
    ''' <remarks></remarks>
    Dim CCDScene(enmStage.Max) As String

    Dim mPurgeStopWatch(enmStage.Max) As Stopwatch

    Dim mLightValue(enmStage.Max) As Integer
    Dim mlight(enmStage.Max) As enmLight '[光源]

    Private mTriggerCmdFailCount(enmStage.Max) As Integer                               '[紀錄資料傳輸異常次數]
    '觸發板用資料
    Dim mCyleParam(enmStage.Max) As sTriggerTPCmdParam
    ''' <summary>紀錄動作用閥號</summary>
    ''' <remarks></remarks>
    Dim mValveNo(enmStage.Max) As enmValve
    'jimmy 20170822 手動觸發AutoValveCalibXY
    Private mbManualAutoCalibXY As Boolean
    Public Property _bManualAutoCalibXY() As Boolean
        Get
            Return mbManualAutoCalibXY
        End Get
        Set(ByVal value As Boolean)
            mbManualAutoCalibXY = value
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
    Sub New()
        For mStageNo As Integer = 0 To CCDScene.Count - 1
            CCDScene(mStageNo) = "CALIBValve" & (mStageNo + 1).ToString
            mTimeoutStopWatch(mStageNo) = New Stopwatch
            mMotorStableStopWatch(mStageNo) = New Stopwatch
            mPurgeStopWatch(mStageNo) = New Stopwatch
            CalibrationX(mStageNo) = New List(Of Double)
            CalibrationY(mStageNo) = New List(Of Double)
        Next
    End Sub

    ''' <summary>
    ''' 閥自動校正XY 動作流程    20161122
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Run(ByRef sys As sSysParam) As enmRunStatus
        Static mStopWatchTimeOut(enmStage.Max) As Stopwatch
        Dim mErrorMessage As String = ""
        'jimmy 20170822
        If mbManualAutoCalibXY = True Then
            sys.SelectValve = mManualSelectValve
        End If
        With sys
            Select Case .SysNum
                Case sSysParam.SysLoopStart

                    mValveNo(sys.StageNo) = sys.SelectValve
                    mStopWatchTimeOut(.StageNo) = New Stopwatch()
                    mStopWatchTimeOut(.StageNo).Start()

                    '[說明]:紀錄當下的馬達座標 & 關閉出膠 & 氣缸上升

                    '[說明]:在Purge前開啟膠桶壓力，Purge完膠就關閉
                    'gSysAdapter.SetSyringePressure(sys.StageNo, sys.SelectValve, enmONOFF.eOff)
                    '[說明]:關閉膠閥  
                    Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eOff)

                    '[說明]:清除計數器
                    CalibrationX(sys.StageNo).Clear()
                    CalibrationY(sys.StageNo).Clear()

                    '[說明]:開啟膠桶壓力
                    gSysAdapter.SetSyringePressure(sys.StageNo, sys.SelectValve, enmONOFF.eON)
                    mPurgeStopWatch(sys.StageNo).Restart()
                    mPurgeStart(sys.StageNo) = mPurgeStopWatch(sys.StageNo).ElapsedMilliseconds

                    '[說明]:'觸發拍照關確保 會進計算流程
                    Call gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False)

                    If sys.AxisB <> -1 Then
                        Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                        '[說明]:切換場景   CCDScene 固定場景
                        Call gAOICollection.SetCCDScene(sys.CCDNo, CCDScene(sys.StageNo))
                        '[說明]:設定出膠壓力
                        gEPVCollection.SetValue(sys.StageNo, sys.SelectValve, eEPVPressureType.Syringe, gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decAirPressure(sys.SelectValve, mPosB(sys.StageNo)), False)
                    Else
                        '[說明]:切換場景   CCDScene 固定場景
                        Call gAOICollection.SetCCDScene(sys.CCDNo, CCDScene(sys.StageNo))
                        '[說明]:設定出膠壓力
                        gEPVCollection.SetValue(sys.StageNo, sys.SelectValve, eEPVPressureType.Syringe, gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decAirPressure(sys.SelectValve, 0), False)
                    End If
                    '                    mCCDRecivedErrorCount = 0
                    .SysNum = 1050

                Case 1050
                    '[說明]:設定移動速度
                    '[說明]:X軸
                    If gCMotion.SetVelAccDec(sys.AxisX) = False Then
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                    '[說明]:Y軸
                    If gCMotion.SetVelAccDec(sys.AxisY) = False Then
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                    '[說明]:Z軸
                    If gCMotion.SetVelAccDec(sys.AxisZ) = False Then
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                    'jimmy 20161130
                    '[說明]:Tilt軸
                    If sys.AxisB <> -1 Then
                        If gCMotion.SetVelAccDec(sys.AxisB) = False Then
                            sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            If mbManualAutoCalibXY = True Then
                                mbManualAutoCalibXY = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                    End If
                    .SysNum = 1100

                Case 1100
                    '[Note]:光源切換
                    '[Note]:總共四組光源(一個Stage)，光源開啟後暫無確認機制，若後續發現打光時間不夠，再補打光時間
                    '[Note]:第一組光源
                    mlight(sys.StageNo) = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
                    If mlight(sys.StageNo) = -1 Then 'Soni / 2017.05.11
                        sys.SysNum = 1130
                    Else
                        Call gSysAdapter.SetLightOnOff(mlight(sys.StageNo), gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightEnable(enmValveLight.No1))
                        If gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightEnable(enmValveLight.No1) = True Then
                            mLightValue(sys.StageNo) = gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightValue(enmValveLight.No1)
                            If mLightValue1(sys.StageNo) <> mLightValue(sys.StageNo) Then
                                mLightCmdFailCount(sys.StageNo) = 0
                                sys.SysNum = 1110
                            Else
                                sys.SysNum = 1120
                            End If
                        Else
                            sys.SysNum = 1120
                        End If
                    End If

                Case 1110
                    '[Note]:確認光源切換是否完成
                    mLightValue(sys.StageNo) = gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightValue(enmValveLight.No1)
                    If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = False Then
                        If gLightCollection.SetCCDLight(sys.CCDNo, mlight(sys.StageNo), mLightValue(sys.StageNo)) = True Then
                            sys.SysNum = 1120
                        End If
                    End If
                Case 1120
                    '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                    If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        If gLightCollection.IsTimeOut(sys.CCDNo, mlight(sys.StageNo)) = True Then
                            '[Note]:超過時間還沒處裡完-->在下一次
                            mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                            If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                                gEqpMsg.AddHistoryAlarm("Error_1022002", "SubDispStage_AutoValveCalibrationActionTest", sys.SysNum, gMsgHandler.GetMessage(Error_1022002), eMessageLevel.Error)
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                sys.RunStatus = enmRunStatus.Alarm
                                'Exit Sub
                            Else
                                sys.SysNum = 1110
                            End If
                        End If
                    Else
                        '[Note]:檢查接收資料
                        If gLightCollection.GetResultStatus(sys.CCDNo, mlight(sys.StageNo)) = True Then
                            mLightValue1(sys.StageNo) = gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightValue(enmValveLight.No1)
                            sys.SysNum = 1130
                        Else
                            '[Note]:超過時間還沒處裡完-->在下一次
                            mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                            If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                                gEqpMsg.AddHistoryAlarm("Error_1022002", "SubDispStage_AutoValveCalibrationActionTest", sys.SysNum, gMsgHandler.GetMessage(Error_1022002), eMessageLevel.Error)
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                sys.RunStatus = enmRunStatus.Alarm
                                'Exit Sub
                            Else
                                sys.SysNum = 1110
                            End If
                        End If
                    End If

                Case 1130
                    '[Note]:第二組光源
                    mlight(sys.StageNo) = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
                    If mlight(sys.StageNo) = -1 Then 'Soni / 2017.05.11
                        sys.SysNum = 1160
                    Else
                        Call gSysAdapter.SetLightOnOff(mlight(sys.StageNo), gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightEnable(enmValveLight.No2))
                        If gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightEnable(enmValveLight.No2) = True Then
                            mLightValue(sys.StageNo) = gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightValue(enmValveLight.No2)
                            If mLightValue2(sys.StageNo) <> mLightValue(sys.StageNo) Then
                                sys.SysNum = 1140
                            Else
                                sys.SysNum = 1160
                            End If
                        Else
                            sys.SysNum = 1160
                        End If
                    End If

                Case 1140
                    mLightValue(sys.StageNo) = gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightValue(enmValveLight.No2)
                    If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = False Then
                        If gLightCollection.SetCCDLight(sys.CCDNo, mlight(sys.StageNo), mLightValue(sys.StageNo)) = True Then
                            sys.SysNum = 1150
                        End If
                    End If
                Case 1150
                    '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                    If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        If gLightCollection.IsTimeOut(sys.CCDNo, mlight(sys.StageNo)) = True Then
                            '[Note]:超過時間還沒處裡完-->在下一次
                            mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                            If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                                gEqpMsg.AddHistoryAlarm("Error_1022002", "SubDispStage_AutoValveCalibrationActionTest", sys.SysNum, gMsgHandler.GetMessage(Error_1022002), eMessageLevel.Error)
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                sys.RunStatus = enmRunStatus.Alarm
                                'Exit Sub
                            Else
                                sys.SysNum = 1140
                            End If
                        End If
                    Else
                        '[Note]:檢查接收資料
                        If gLightCollection.GetResultStatus(sys.CCDNo, mlight(sys.StageNo)) = True Then
                            mLightValue1(sys.StageNo) = gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightValue(enmValveLight.No2)
                            sys.SysNum = 1160
                        Else
                            '[Note]:超過時間還沒處裡完-->在下一次
                            mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                            If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                                gEqpMsg.AddHistoryAlarm("Error_1022002", "SubDispStage_AutoValveCalibrationActionTest", sys.SysNum, gMsgHandler.GetMessage(Error_1022002), eMessageLevel.Error)
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                sys.RunStatus = enmRunStatus.Alarm
                                ' Exit Sub
                            Else
                                sys.SysNum = 1140
                            End If
                        End If
                    End If

                Case 1160
                    '[Note]:第三組光源
                    mlight(sys.StageNo) = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3)
                    If mlight(sys.StageNo) = -1 Then 'Soni / 2017.05.11
                        sys.SysNum = 1190
                    Else
                        Call gSysAdapter.SetLightOnOff(mlight(sys.StageNo), gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightEnable(enmValveLight.No3))
                        If gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightEnable(enmValveLight.No3) = True Then
                            mLightValue(sys.StageNo) = gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightValue(enmValveLight.No3)
                            If mLightValue3(sys.StageNo) <> mLightValue(sys.StageNo) Then
                                sys.SysNum = 1170
                            Else
                                sys.SysNum = 1190
                            End If
                        Else
                            sys.SysNum = 1190
                        End If
                    End If

                Case 1170
                    mLightValue(sys.StageNo) = gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightValue(enmValveLight.No3)
                    If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = False Then
                        If gLightCollection.SetCCDLight(sys.CCDNo, mlight(sys.StageNo), mLightValue(sys.StageNo)) = True Then
                            sys.SysNum = 1180
                        End If
                    End If

                Case 1180
                    '[Note]:第三組光源
                    '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                    If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        If gLightCollection.IsTimeOut(sys.CCDNo, mlight(sys.StageNo)) = True Then
                            '[Note]:超過時間還沒處裡完-->在下一次
                            mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                            If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                                gEqpMsg.AddHistoryAlarm("Error_1022002", "SubDispStage_AutoValveCalibrationActionTest", sys.SysNum, gMsgHandler.GetMessage(Error_1022002), eMessageLevel.Error)
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                sys.RunStatus = enmRunStatus.Alarm
                                'Exit Sub
                            Else
                                sys.SysNum = 1170
                            End If
                        End If
                    Else
                        '[Note]:檢查接收資料
                        If gLightCollection.GetResultStatus(sys.CCDNo, mlight(sys.StageNo)) = True Then
                            mLightValue1(sys.StageNo) = gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightValue(enmValveLight.No3)
                            sys.SysNum = 1190
                        Else
                            '[Note]:超過時間還沒處裡完-->在下一次
                            mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                            If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                                gEqpMsg.AddHistoryAlarm("Error_1022002", "SubDispStage_AutoValveCalibrationActionTest", sys.SysNum, gMsgHandler.GetMessage(Error_1022002), eMessageLevel.Error)
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                sys.RunStatus = enmRunStatus.Alarm
                                'Exit Sub
                            Else
                                sys.SysNum = 1180
                            End If
                        End If
                    End If

                Case 1190
                    '[Note]:第四組光源
                    mlight(sys.StageNo) = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4)
                    If mlight(sys.StageNo) = -1 Then 'Soni / 2017.05.11
                        sys.SysNum = 1300
                    Else
                        Call gSysAdapter.SetLightOnOff(mlight(sys.StageNo), gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightEnable(enmValveLight.No4))
                        If gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightEnable(enmValveLight.No4) = True Then
                            mLightValue(sys.StageNo) = gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightValue(enmValveLight.No4)
                            If mLightValue4(sys.StageNo) <> mLightValue(sys.StageNo) Then
                                sys.SysNum = 1200
                            Else
                                sys.SysNum = 1300
                            End If
                        Else
                            sys.SysNum = 1200
                        End If
                    End If

                Case 1200
                    mLightValue(sys.StageNo) = gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightValue(enmValveLight.No4)
                    If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = False Then
                        If gLightCollection.SetCCDLight(sys.CCDNo, mlight(sys.StageNo), mLightValue(sys.StageNo)) = True Then
                            sys.SysNum = 1210
                        End If
                    End If
                Case 1210
                    '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                    If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        If gLightCollection.IsTimeOut(sys.CCDNo, mlight(sys.StageNo)) = True Then
                            '[Note]:超過時間還沒處裡完-->在下一次
                            mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                            If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                                gEqpMsg.AddHistoryAlarm("Error_1022002", "SubDispStage_AutoValveCalibrationActionTest", sys.SysNum, gMsgHandler.GetMessage(Error_1022002), eMessageLevel.Error)
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                sys.RunStatus = enmRunStatus.Alarm
                                'Exit Sub
                            Else
                                sys.SysNum = 1200
                            End If
                        End If
                    Else
                        '[Note]:檢查接收資料
                        If gLightCollection.GetResultStatus(sys.CCDNo, mlight(sys.StageNo)) = True Then
                            mLightValue1(sys.StageNo) = gAOICollection.SceneDictionary(CCDScene(sys.StageNo)).LightValue(enmValveLight.No4)
                            sys.SysNum = 1300
                        Else
                            '[Note]:超過時間還沒處裡完-->在下一次
                            mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                            If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                                gEqpMsg.AddHistoryAlarm("Error_1022002", "SubDispStage_AutoValveCalibrationActionTest", sys.SysNum, gMsgHandler.GetMessage(Error_1022002), eMessageLevel.Error)
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                sys.RunStatus = enmRunStatus.Alarm
                                ' Exit Sub
                            Else
                                sys.SysNum = 1200
                            End If
                        End If
                    End If

                Case 1300
                    Select Case gSSystemParameter.CCDModuleType
                        Case enmCCDModule.eFix
                            '[說明]:Z軸移至Up高度，避免撞到流道與Purge平台
                            If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                                Select Case .StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Error_1032000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Error_1044000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Error_1062000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Error_1069000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                                End Select
                                'gEqpMsg.AddHistoryAlarm("Error_1032000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error) 'Z軸命令執行失敗
                                sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            End If

                            .SysNum = 1400
                        Case enmCCDModule.eFree
                            '[說明]:Z軸移至自動校時之CCD取像高度(可以避免撞到流道與Purge平台)
                            If sys.AxisB <> -1 Then
                                Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                                mPosZ(sys.StageNo) = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDZ(sys.SelectValve, mPosB(sys.StageNo))
                            Else
                                mPosZ(sys.StageNo) = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDZ(sys.SelectValve, 0)
                            End If

                            If gCMotion.AbsMove(sys.AxisZ, mPosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                                Select Case .StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Error_1032000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Error_1044000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Error_1062000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Error_1069000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                                End Select
                                'gEqpMsg.AddHistoryAlarm("Error_1032000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error) 'Z軸命令執行失敗
                                sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            End If
                            .SysNum = 1400
                    End Select

                Case 1400
                    If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1032004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error) 'Z軸命令執行失敗
                            sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            If mbManualAutoCalibXY = True Then
                                mbManualAutoCalibXY = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If
                    'mCCDRecivedErrorCount = 0
                    If sys.AxisB <> -1 Then
                        .SysNum = 1500
                    Else
                        .SysNum = 1410
                    End If                   '
                Case 1410
                    ' 20161130 Tilt轉
                    Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                    If gCMotion.AbsMove(sys.AxisB, mPosB(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1034000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1034000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1046000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1046000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1064000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1064000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1071000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1071000), eMessageLevel.Error)
                        End Select
                        '  gEqpMsg.AddHistoryAlarm("Error_1032000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    .SysNum = 1420
                Case 1420
                    'If gCMotion.MotionDone(sys.AxisB) <> CommandStatus.Sucessed Then
                    '    If gCMotion.IsMoveTimeOut(sys.AxisB) Then
                    '        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                    '        Return enmRunStatus.Alarm
                    '    Else
                    '        Return enmRunStatus.Running
                    '    End If
                    'End If
                    '.SysNum = 1500
                    mAxisBState(sys.StageNo) = gCMotion.MotionDone(sys.AxisB)
                    If mAxisBState(sys.StageNo) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisB) Then
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1034004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1046004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1064004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1071004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071004), eMessageLevel.Error)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            If mbManualAutoCalibXY = True Then
                                mbManualAutoCalibXY = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                    Else
                        sys.SysNum = 1500
                    End If
                Case 1500
                    '[說明]:Sensor檢查計數開始
                    Call mMotorStableStopWatch(.StageNo).Restart()
                    mCheckSensorStart(sys.StageNo) = mMotorStableStopWatch(.StageNo).ElapsedMilliseconds
                    .SysNum = 1700
                    '***********************************************************************************************************************
                    '******************************************************移至試點區塊*****************************************************
                    '***********************************************************************************************************************
                Case 1700
                    '[說明]:X Y 至拍照位置
                    If sys.AxisB <> -1 Then
                        Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                        mPosX(sys.StageNo) = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDX(sys.SelectValve, mPosB(sys.StageNo))
                        mPosY(sys.StageNo) = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDY(sys.SelectValve, mPosB(sys.StageNo))
                    Else
                        mPosX(sys.StageNo) = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDX(sys.SelectValve, 0)
                        mPosY(sys.StageNo) = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDY(sys.SelectValve, 0)
                    End If


                    If gCMotion.AbsMove(sys.AxisX, mPosX(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1030000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1042000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1060000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1067000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1030000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    If gCMotion.AbsMove(sys.AxisY, mPosY(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1031000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1043000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1061000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1068000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1031000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                    .SysNum = 1800


                Case 1800
                    If gCMotion.MotionDone(sys.AxisX) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1030004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1042004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1060004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1067004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1030004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                            sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            If mbManualAutoCalibXY = True Then
                                mbManualAutoCalibXY = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If
                    If gCMotion.MotionDone(sys.AxisY) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisY) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1031004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1043004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1061004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1068004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1031004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                            sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            If mbManualAutoCalibXY = True Then
                                mbManualAutoCalibXY = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If

                    .SysNum = 1900


                    '***********************************************************************************************************************
                    '****************************************************先檢查上面有膠*****************************************************
                    '***********************************************************************************************************************
                Case 1900
                    '[說明]:取像
                    If gAOICollection.IsCCDReady(sys.CCDNo) = True Then

                        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保
                        System.Threading.Thread.CurrentThread.Join(10)
                        Ticket(sys.CCDNo) = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False)
                        System.Threading.Thread.CurrentThread.Join(10)
                        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保
                        .SysNum = 2050
                    End If

                Case 2050

                    .SysNum = 2060

                Case 2060
                    '[說明]:檢查CCD是否已經完成取像
                    If gAOICollection.IsCCDCBusy(sys.CCDNo) = False Then
                        .SysNum = 2200
                    End If


                Case 2200
                    If gCCDAlignResultDict(sys.CCDNo).ContainsKey(Ticket(sys.CCDNo)) = True Then
                        If gCCDAlignResultDict(sys.CCDNo)(Ticket(sys.CCDNo)).IsRunSuccess = True Then
                            .SysNum = 2210
                        End If
                    End If

                Case 2210
                    '[說明]:資料接收完成
                    If gCCDAlignResultDict(sys.CCDNo)(Ticket(sys.StageNo)).Result.Count > 0 Then
                        '改跳 Warning 結束流程
                        Select Case .StageNo '[閥座自動校正異常，請確認!]
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Warn_3012000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012000), eMessageLevel.Warning)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Warn_3012200", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012200), eMessageLevel.Warning)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Warn_3012300", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012300), eMessageLevel.Warning)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Warn_3012500", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012500), eMessageLevel.Warning)
                        End Select
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Finish
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        sys.RunStatus = enmRunStatus.Finish
                        Return enmRunStatus.Finish
                        'gEqpMsg.AddHistoryAlarm("Alarm_2012102", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Alarm_2012102), eMessageLevel.Error)     '[閥座自動校正異常，請確認!]
                        'sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        'Return enmRunStatus.Alarm
                        '[說明]:無回傳值才是對的   '20161125
                    ElseIf gCCDAlignResultDict(sys.CCDNo)(Ticket(sys.StageNo)).Result.Count = 0 Then
                        .SysNum = 3000
                    End If

                    '*******************************************************************************************************************
                    '***********************************************先畫二條線後，在畫單點**********************************************
                    '*******************************************************************************************************************

                    '***********************************************************************************************************************
                    '*************************************************在試點平台上進行打點**************************************************
                    '***********************************************************************************************************************
                Case 3000
                    Call gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False)

                    '[說明]:移至畫膠位置，進行劃膠
                    With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
                        If sys.AxisB <> -1 Then
                            Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                            mPosX(sys.StageNo) = .CCDX(sys.SelectValve, mPosB(sys.StageNo)) - .CCDTiltValveOffsetX(sys.SelectValve, mPosB(sys.StageNo)) + (.decPitch(sys.SelectValve, mPosB(sys.StageNo)) * CalibrationX(sys.StageNo).Count)
                            mPosY(sys.StageNo) = .CCDY(sys.SelectValve, mPosB(sys.StageNo)) - .CCDTiltValveOffsetY(sys.SelectValve, mPosB(sys.StageNo))
                        Else
                            mPosX(sys.StageNo) = .CCDX(sys.SelectValve, 0) - .CCDTiltValveOffsetX(sys.SelectValve, 0) + (.decPitch(sys.SelectValve, 0) * CalibrationX(sys.StageNo).Count)
                            mPosY(sys.StageNo) = .CCDY(sys.SelectValve, 0) - .CCDTiltValveOffsetY(sys.SelectValve, 0)
                        End If
                    End With

                    If gCMotion.AbsMove(sys.AxisX, mPosX(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1030000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1042000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1060000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1067000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                        End Select

                        'gEqpMsg.AddHistoryAlarm("Error_1030000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    If gCMotion.AbsMove(sys.AxisY, mPosY(sys.StageNo)) <> CommandStatus.Sucessed Then

                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1031000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1043000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1061000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1068000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1031000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                    .SysNum = 3100

                Case 3100
                    If gCMotion.MotionDone(sys.AxisX) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1030004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1042004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1060004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1067004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1030004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                            sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            If mbManualAutoCalibXY = True Then
                                mbManualAutoCalibXY = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If
                    If gCMotion.MotionDone(sys.AxisY) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisY) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1031004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1043004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1061004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1068004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1031004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                            sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            If mbManualAutoCalibXY = True Then
                                mbManualAutoCalibXY = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If
                    .SysNum = 3200

                Case 3200

                    '[說明]:Sensor檢查計數開始
                    Call mMotorStableStopWatch(.StageNo).Restart()
                    mCheckSensorStart(sys.StageNo) = mMotorStableStopWatch(.StageNo).ElapsedMilliseconds

                    .SysNum = 3300

                Case 3300
                    '[說明]:移至畫膠高度，進行劃膠
                    If sys.AxisB <> -1 Then
                        Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                        mPosZ(sys.StageNo) = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).ValveZ(sys.SelectValve, mPosB(sys.StageNo))
                    Else
                        mPosZ(sys.StageNo) = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).ValveZ(sys.SelectValve, 0)
                    End If

                    If gCMotion.AbsMove(sys.AxisZ, mPosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error) 'Z軸命令執行失敗
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                    .SysNum = 3500

                Case 3500
                    If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1032004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            If mbManualAutoCalibXY = True Then
                                mbManualAutoCalibXY = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If


                    '[說明]:設定DispenserNo1 DispenserNo2之出膠壓力
                    With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
                        If sys.AxisB <> -1 Then
                            Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                            'gSysAdapter.SetSyringeAirPressure(sys.StageNo, sys.SelectValve, gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decAirPressure(sys.SelectValve, mPosB(sys.StageNo)))
                            gEPVCollection.SetValue(sys.StageNo, sys.SelectValve, eEPVPressureType.Syringe, gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decAirPressure(sys.SelectValve, mPosB(sys.StageNo)), False)
                        Else
                            Dim num As Decimal = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decAirPressure(sys.SelectValve, 0)
                            'gSysAdapter.SetSyringeAirPressure(sys.StageNo, sys.SelectValve, gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decAirPressure(sys.SelectValve, 0))
                            gEPVCollection.SetValue(sys.StageNo, sys.SelectValve, eEPVPressureType.Syringe, gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decAirPressure(sys.SelectValve, 0), False)
                        End If
                    End With
                    mPurgeStopWatch(sys.StageNo).Restart()
                    mPurgeStart(sys.StageNo) = mPurgeStopWatch(sys.StageNo).ElapsedMilliseconds

                    .SysNum = 3550

                Case 3550
                    '[說明]:等待馬達穩定時間
                    If Math.Abs(mPurgeStopWatch(sys.StageNo).ElapsedMilliseconds - mPurgeStart(sys.StageNo)) > 500 Then
                        .SysNum = 3560
                    End If

                Case 3560    '20170808
                    '[Note]:(Step1.)Reset Alarm Check trigger board is ready before send command
                    If gTriggerBoard.SetResetAlarm(sys.StageNo, False) = True Then
                        sys.SysNum = 3570
                    End If
                Case 3570
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
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016001", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016101", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016201", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016301", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                End Select
                                sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                                sys.RunStatus = enmRunStatus.Alarm
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            Else
                                mStopWatchTimeOut(.StageNo).Restart()
                                sys.SysNum = 3560
                            End If
                        End If
                    Else
                        '[Note]:檢查接收資料
                        If gTriggerBoard.ResetAlarm(sys.StageNo).Status = True Then
                            mTriggerCmdFailCount(sys.StageNo) = 0
                            sys.SysNum = 3600
                        Else
                            '[Note]:查看收到的內容是????
                            Debug.Print("ResetAlarm: " & gTriggerBoard.ResetAlarm(sys.StageNo).STR)
                            mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                            If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                                'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                                Select Case sys.StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016001", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016101", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016201", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016301", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                End Select
                                sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                                sys.RunStatus = enmRunStatus.Alarm
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            Else
                                mStopWatchTimeOut(.StageNo).Restart()
                                sys.SysNum = 3560
                            End If
                        End If
                    End If
                Case 3600
                    '[說明]:設定Jetting Valve Trigger Controller 為固定頻率打點模式
                    '==========================================================================================   20170630
                    mCyleParam(mValveNo(sys.StageNo)).DotCounts = 1
                    mCyleParam(mValveNo(sys.StageNo)).JetPressure = 0
                    mCyleParam(mValveNo(sys.StageNo)).GluePressure = 0


                    Select Case gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).ValveModel
                        Case eValveModel.Advanjet
                            mCyleParam(mValveNo(sys.StageNo)).CycleTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).Advanjet.CycleTime * 1000)
                            mCyleParam(mValveNo(sys.StageNo)).PulseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).Advanjet.PulseTime * 1000)

                            'mCyleParam.CycleTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).Advanjet.CycleTime * 1000)
                            'mCyleParam.PulseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).Advanjet.PulseTime * 1000)

                        Case eValveModel.PicoPulse
                            mCyleParam(mValveNo(sys.StageNo)).CycleTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CycleTime * 1000)
                            mCyleParam(mValveNo(sys.StageNo)).PulseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.ValveOnTime * 1000)
                            mCyleParam(mValveNo(sys.StageNo)).OpenTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.OpenTime * 1000)
                            mCyleParam(mValveNo(sys.StageNo)).CloseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CloseTime * 1000)
                            mCyleParam(mValveNo(sys.StageNo)).Stroke = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.Stroke
                            mCyleParam(mValveNo(sys.StageNo)).CloseVoltage = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CloseVoltage

                            'mCyleParam.CycleTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CycleTime * 1000)
                            'mCyleParam.PulseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.ValveOnTime * 1000)
                            'mCyleParam.OpenTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.OpenTime * 1000)
                            'mCyleParam.CloseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CloseTime * 1000)
                            'mCyleParam.Stroke = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.Stroke
                            'mCyleParam.CloseVoltage = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CloseVoltage
                    End Select


                    sys.SysNum = 3610
                    '==========================================================================================   20170630
                Case 3610
                    If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                        If gTriggerBoard.SetCycleRecipe(.StageNo, mCyleParam(mValveNo(sys.StageNo)), False) = True Then
                            sys.SysNum = 3620
                        End If
                    ElseIf (mStopWatchTimeOut(.StageNo).ElapsedMilliseconds > gSSystemParameter.TimeOut1) Then

                        gSyslog.Save(mErrorMessage(sys.StageNo))
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                Case 3620
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
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016001", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016101", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016201", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016301", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                End Select
                                sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                sys.RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Else
                                mStopWatchTimeOut(.StageNo).Restart()
                                sys.SysNum = 3610
                            End If
                        End If
                    Else
                        ''[Note]:檢查接收資料  CycleRecipe
                        If gTriggerBoard.CycleRecipe(sys.StageNo).Status Then
                            mTriggerCmdFailCount(sys.StageNo) = 0
                            mErrorMessage = "AutoValveCalibration Set Cycle Recipe OK!!"
                            gSyslog.Save(mErrorMessage)
                            sys.SysNum = 3630
                        Else
                            '[Note]:查看收到的內容是????
                            Debug.Print("CycleRecipe(T Cmd): " & gTriggerBoard.CycleRecipe(sys.StageNo).STR)
                            mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                            If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                                'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                                Select Case sys.StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Error_1016003", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Error_1016103", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Error_1016203", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Error_1016303", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                End Select
                                sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                sys.RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                            Else
                                mStopWatchTimeOut(.StageNo).Restart()
                                sys.SysNum = 3610
                            End If
                        End If
                    End If
                Case 3630
                    'SetDispenseRun
                    If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                        '[說明]: enmValve.No1 部分 Stage1 Valve1 = 0 ,Valve2 = 1   設定部分還需測試
                        If gTriggerBoard.SetDispenseRun(sys.StageNo, enmTriggerDispType.CycleRecipe, .SelectValve, 0, 0, False) = True Then
                            sys.SysNum = 3640
                        End If
                    ElseIf (mStopWatchTimeOut(.StageNo).ElapsedMilliseconds > gSSystemParameter.TimeOut1) Then

                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                Case 3640
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
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016001", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016101", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016201", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Alarm_2016301", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                End Select
                                sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                sys.RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Else
                                sys.SysNum = 3630
                            End If
                        End If
                    Else
                        ''[Note]:檢查接收資料
                        If gTriggerBoard.DispenseRun(sys.StageNo).Status Then
                            mTriggerCmdFailCount(sys.StageNo) = 0
                            sys.SysNum = 3650
                        Else
                            '[Note]:查看收到的內容是????
                            Debug.Print("DispenseRun(X Cmd): " & gTriggerBoard.DispenseRun(sys.StageNo).STR)
                            mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                            If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                                'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                                Select Case sys.StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Error_1016003", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Error_1016103", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Error_1016203", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Error_1016303", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                End Select
                                sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                sys.RunStatus = enmRunStatus.Alarm
                                Return enmRunStatus.Alarm
                            Else
                                sys.SysNum = 3630
                            End If
                        End If
                    End If

                Case 3650

                    '[說明]:出膠  
                    Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eON)
                    mMotorStableStopWatch(.StageNo).Restart()
                    '========================================================================================
                    .SysNum = 3700

                Case 3700
                    If mMotorStableStopWatch(.StageNo).ElapsedMilliseconds > 300 Then
                        'If IsTimeOut(mMotorStableStopWatch(.StageNo), mMotorStableTime(sys.StageNo), 100) = True Then '開閥10ms
                        '[說明]:關膠  
                        Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eOff)
                        .SysNum = 3800
                    End If

                Case 3800
                    Select Case gSSystemParameter.CCDModuleType
                        Case enmCCDModule.eFix
                            '[說明]:Z軸移至Up高度，避免撞到流道與Purge平台
                            If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                                Select Case .StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Error_1032000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Error_1044000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Error_1062000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Error_1069000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                                End Select
                                'gEqpMsg.AddHistoryAlarm("Error_1032000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error) 'Z軸命令執行失敗
                                sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            End If

                            .SysNum = 3900

                        Case enmCCDModule.eFree
                            '[說明]:Z軸移至自動校時之CCD取像高度(可以避免撞到流道與Purge平台)
                            If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDZ(sys.SelectValve, 0)) <> CommandStatus.Sucessed Then
                                Select Case .StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Error_1032000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Error_1044000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Error_1062000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Error_1069000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                                End Select
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error) 'Z軸命令執行失敗
                                sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                                If mbManualAutoCalibXY = True Then
                                    mbManualAutoCalibXY = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            End If
                            .SysNum = 3900
                    End Select

                Case 3900
                    If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                            End Select

                            'gEqpMsg.AddHistoryAlarm("Error_1032004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error) 'Z軸命令執行失敗
                            sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            If mbManualAutoCalibXY = True Then
                                mbManualAutoCalibXY = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If
                    .SysNum = 5000

                    '***********************************************************************************************************************
                    '**********************************************打完移動CCD過去看打點狀況************************************************
                    '***********************************************************************************************************************

                Case 5000
                    '[說明]:X Y 至CCD 待測位置
                    With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
                        If sys.AxisB <> -1 Then
                            Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                            mPosX(sys.StageNo) = .CCDX(sys.SelectValve, mPosB(sys.StageNo)) + (.decPitch(sys.SelectValve, mPosB(sys.StageNo)) * CalibrationX(sys.StageNo).Count)
                            mPosY(sys.StageNo) = .CCDY(sys.SelectValve, mPosB(sys.StageNo))
                        Else
                            mPosX(sys.StageNo) = .CCDX(sys.SelectValve, 0) + (.decPitch(sys.SelectValve, 0) * CalibrationX(sys.StageNo).Count)
                            mPosY(sys.StageNo) = .CCDY(sys.SelectValve, 0)
                        End If
                    End With

                    'mCCDRecivedErrorCount = 0
                    If gCMotion.AbsMove(sys.AxisX, mPosX(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1030000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1042000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1060000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1067000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1030000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    If gCMotion.AbsMove(sys.AxisY, mPosY(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1031000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1043000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1061000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1068000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1031000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    '20170520
                    .SysNum = 5090
                Case 5090

                    If sys.AxisB <> -1 Then
                        Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                        System.Threading.Thread.CurrentThread.Join(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decCCDStableTime(sys.SelectValve, mPosB(sys.StageNo)))
                    Else
                        Dim num As Decimal = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decCCDStableTime(sys.SelectValve, 0)
                        System.Threading.Thread.CurrentThread.Join(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decCCDStableTime(sys.SelectValve, 0))
                    End If

                    '[說明]:切換場景
                    'Call gAOICollection.SetCCDScene(sys.CCDNo, CCDScene)
                    mPurgeStopWatch(sys.StageNo).Restart()
                    .SysNum = 5100


                Case 5100
                    If gCMotion.MotionDone(sys.AxisX) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1030004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1042004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1060004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1067004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1030004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                            sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            If mbManualAutoCalibXY = True Then
                                mbManualAutoCalibXY = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If
                    If gCMotion.MotionDone(sys.AxisY) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisY) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1031004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1043004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1061004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1068004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1031004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                            sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            If mbManualAutoCalibXY = True Then
                                mbManualAutoCalibXY = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If

                    '[說明]:切換場景
                    'Call gAOICollection.SetCCDScene(sys.CCDNo, CCDScene)
                    mPurgeStopWatch(sys.StageNo).Restart()
                    .SysNum = 5200
                Case 5150
                    If mPurgeStopWatch(sys.StageNo).ElapsedMilliseconds > 500 Then
                        .SysNum = 5200
                    End If
                Case 5200
                    '[說明]:取像
                    If gAOICollection.IsCCDReady(sys.CCDNo) = True Then
                        Ticket(sys.CCDNo) = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False)
                        mTimeoutStopWatch(sys.StageNo).Restart()
                        .SysNum = 5350
                    End If

                Case 5350
                    gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保
                    .SysNum = 5360

                Case 5360
                    '[說明]:檢查CCD是否已經完成取像
                    If gAOICollection.IsCCDCBusy(sys.CCDNo) = False Then
                        .SysNum = 5500
                    End If

                Case 5500
                    If gCCDAlignResultDict(sys.CCDNo).ContainsKey(Ticket(sys.CCDNo)) = True Then
                        If gCCDAlignResultDict(sys.CCDNo)(Ticket(sys.CCDNo)).IsRunSuccess = True Then
                            .SysNum = 6000
                        End If
                    End If

                Case 6000
                    Dim result As List(Of sAlignResult)
                    '[說明]:取得檢測資料
                    result = gCCDAlignResultDict(sys.CCDNo)(Ticket(sys.CCDNo)).Result
                    If gCCDAlignResultDict(sys.CCDNo)(Ticket(sys.StageNo)).Result.Count > 0 Then
                        If sys.AxisB <> -1 Then
                            Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                            ''20170612
                            'If Math.Abs(Math.Round(result(0).AbsOffsetX, 3)) > gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).dCCDValveThreadshold(sys.SelectValve, mPosB(sys.StageNo)) Then '定位結果誤差過大
                            '    '改跳 Warning 結束流程
                            '    Select Case .StageNo '[閥座自動校正異常，請確認!]
                            '        Case 0
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012000), eMessageLevel.Warning)
                            '        Case 1
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012200", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012200), eMessageLevel.Warning)
                            '        Case 2
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012300", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012300), eMessageLevel.Warning)
                            '        Case 3
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012500", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012500), eMessageLevel.Warning)
                            '    End Select
                            '    sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Finish
                            '    sys.RunStatus = enmRunStatus.Finish
                            '    If mbManualAutoCalibXY = True Then
                            '        mbManualAutoCalibXY = False
                            '        .SelectValve = mSelectValveTemp
                            '    End If
                            '    Return enmRunStatus.Finish
                            '    'gEqpMsg.AddHistoryAlarm("Alarm_2012111", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Alarm_2012111), eMessageLevel.Alarm)     '[閥座自動校正異常，請確認!]
                            '    'sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            '    'Return enmRunStatus.Alarm
                            'ElseIf Math.Abs(Math.Round(result(0).AbsOffsetY, 3)) > gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).dCCDValveThreadshold(sys.SelectValve, mPosB(sys.StageNo)) Then '定位結果誤差過大
                            '    '改跳 Warning 結束流程
                            '    Select Case .StageNo '[閥座自動校正異常，請確認!]
                            '        Case 0
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012000), eMessageLevel.Warning)
                            '        Case 1
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012200", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012200), eMessageLevel.Warning)
                            '        Case 2
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012300", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012300), eMessageLevel.Warning)
                            '        Case 3
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012500", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012500), eMessageLevel.Warning)
                            '    End Select
                            '    sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Finish
                            '    sys.RunStatus = enmRunStatus.Finish
                            '    If mbManualAutoCalibXY = True Then
                            '        mbManualAutoCalibXY = False
                            '        .SelectValve = mSelectValveTemp
                            '    End If
                            '    Return enmRunStatus.Finish
                            '    'gEqpMsg.AddHistoryAlarm("Alarm_2012111", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Alarm_2012111), eMessageLevel.Alarm)     '[閥座自動校正異常，請確認!]
                            '    'sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            '    'Return enmRunStatus.Alarm
                            'End If

                            '20170520
                            CalibrationX(sys.StageNo).Add(Math.Round(result(0).AbsOffsetX, 3))
                            CalibrationY(sys.StageNo).Add(Math.Round(result(0).AbsOffsetY, 3))

                            If CalibrationX.Count < gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).iCCDValveCount(sys.SelectValve, mPosB(sys.StageNo)) Then
                                .SysNum = 3000
                            Else
                                .SysNum = 8000
                            End If
                        Else
                            ''20170612
                            'If Math.Abs(Math.Round(result(0).AbsOffsetX, 3)) > gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).dCCDValveThreadshold(sys.SelectValve, 0) Then '定位結果誤差過大
                            '    '改跳 Warning 結束流程
                            '    Select Case .StageNo '[閥座自動校正異常，請確認!]
                            '        Case 0
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012000), eMessageLevel.Warning)
                            '        Case 1
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012200", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012200), eMessageLevel.Warning)
                            '        Case 2
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012300", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012300), eMessageLevel.Warning)
                            '        Case 3
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012500", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012500), eMessageLevel.Warning)
                            '    End Select
                            '    sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Finish
                            '    sys.RunStatus = enmRunStatus.Finish
                            '    If mbManualAutoCalibXY = True Then
                            '        mbManualAutoCalibXY = False
                            '        .SelectValve = mSelectValveTemp
                            '    End If
                            '    Return enmRunStatus.Finish
                            '    'gEqpMsg.AddHistoryAlarm("Alarm_2012111", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Alarm_2012111), eMessageLevel.Alarm)     '[閥座自動校正異常，請確認!]
                            '    'sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            '    'Return enmRunStatus.Alarm
                            'ElseIf Math.Abs(Math.Round(result(0).AbsOffsetY, 3)) > gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).dCCDValveThreadshold(sys.SelectValve, 0) Then '定位結果誤差過大
                            '    '改跳 Warning 結束流程
                            '    Select Case .StageNo '[閥座自動校正異常，請確認!]
                            '        Case 0
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012000), eMessageLevel.Warning)
                            '        Case 1
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012200", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012200), eMessageLevel.Warning)
                            '        Case 2
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012300", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012300), eMessageLevel.Warning)
                            '        Case 3
                            '            gEqpMsg.AddHistoryAlarm("Warn_3012500", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012500), eMessageLevel.Warning)
                            '    End Select
                            '    sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Finish
                            '    sys.RunStatus = enmRunStatus.Finish
                            '    If mbManualAutoCalibXY = True Then
                            '        mbManualAutoCalibXY = False
                            '        .SelectValve = mSelectValveTemp
                            '    End If
                            '    Return enmRunStatus.Finish
                            '    'gEqpMsg.AddHistoryAlarm("Alarm_2012111", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Alarm_2012111), eMessageLevel.Alarm)     '[閥座自動校正異常，請確認!]
                            '    'sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                            '    'Return enmRunStatus.Alarm
                            'End If

                            '20170520
                            CalibrationX(sys.StageNo).Add(Math.Round(result(0).AbsOffsetX, 3))
                            CalibrationY(sys.StageNo).Add(Math.Round(result(0).AbsOffsetY, 3))

                            If CalibrationX(sys.StageNo).Count < gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).iCCDValveCount(sys.SelectValve, 0) Then
                                .SysNum = 3000
                            Else
                                .SysNum = 8000
                            End If
                        End If

                    Else
                        '改跳 Warning 結束流程
                        Select Case .StageNo '[閥座自動校正異常，請確認!]
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Warn_3012000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012000), eMessageLevel.Warning)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Warn_3012200", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012200), eMessageLevel.Warning)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Warn_3012300", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012300), eMessageLevel.Warning)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Warn_3012500", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Warn_3012500), eMessageLevel.Warning)
                        End Select
                        sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Finish
                        sys.RunStatus = enmRunStatus.Finish
                        If mbManualAutoCalibXY = True Then
                            mbManualAutoCalibXY = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Finish
                        'gEqpMsg.AddHistoryAlarm("Alarm_2012103", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Alarm_2012103), eMessageLevel.Alarm)     'CCD1符合特徵未找到
                        'sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Alarm
                        'Return enmRunStatus.Alarm
                    End If

                    '***********************************************************************************************************************
                    '************************************************看完進行自動Calibration************************************************
                    '***********************************************************************************************************************

                Case 8000
                    'mCCDRecivedErrorCount = 0
                    .SysNum = 9000

                Case 9000
                    mMotorStableStopWatch(.StageNo).Stop()
                    '[說明]:計算平均偏移量
                    If sys.AxisB <> -1 Then
                        Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                        gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).AutoResultX(sys.SelectValve, mPosB(sys.StageNo)) = CalibrationX(sys.StageNo).Average
                        gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).AutoResultY(sys.SelectValve, mPosB(sys.StageNo)) = CalibrationY(sys.StageNo).Average
                    Else
                        gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).AutoResultX(sys.SelectValve, 0) = CalibrationX(sys.StageNo).Average
                        gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).AutoResultY(sys.SelectValve, 0) = CalibrationY(sys.StageNo).Average
                    End If

                    '[說明]:寫入CCD偏移量
                    With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
                        If sys.AxisB <> -1 Then
                            Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                            .CCDX(sys.SelectValve, mPosB(sys.StageNo)) = .CCDX(sys.SelectValve, mPosB(sys.StageNo)) - .AutoResultX(sys.SelectValve, mPosB(sys.StageNo))
                            .CCDY(sys.SelectValve, mPosB(sys.StageNo)) = .CCDY(sys.SelectValve, mPosB(sys.StageNo)) - .AutoResultY(sys.SelectValve, mPosB(sys.StageNo))
                            MsgBox("Auto Calib OK." & vbCrLf & "OffsetX:" & .AutoResultX(sys.SelectValve, mPosB(sys.StageNo)) & vbCrLf &
                                   "OffsetY: " & .AutoResultY(sys.SelectValve, mPosB(sys.StageNo)), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            .CCDX(sys.SelectValve, 0) = .CCDX(sys.SelectValve, 0) - .AutoResultX(sys.SelectValve, 0)
                            .CCDY(sys.SelectValve, 0) = .CCDY(sys.SelectValve, 0) - .AutoResultY(sys.SelectValve, 0)
                            MsgBox("Auto Calib OK." & vbCrLf & "OffsetX:" & .AutoResultX(sys.SelectValve, 0) & vbCrLf &
                                   "OffsetY: " & .AutoResultY(sys.SelectValve, 0), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        End If
                    End With


                    'gSysAdapter.SetSyringePressure(sys.StageNo, sys.SelectValve, enmONOFF.eOff)
                    sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Finish
                    If mbManualAutoCalibXY = True Then
                        mbManualAutoCalibXY = False
                        .SelectValve = mSelectValveTemp
                    End If
                    sys.RunStatus = enmRunStatus.Finish
                    Return enmRunStatus.Finish
            End Select

            Return enmRunStatus.Running

        End With

    End Function
End Class

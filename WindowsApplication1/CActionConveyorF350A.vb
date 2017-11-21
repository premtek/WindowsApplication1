Imports ProjectCore
Imports ProjectMotion
Imports ProjectIO
Imports WetcoConveyor

Public Class CActionConveyorF350A

    Sub New()
        'CvUnit.Add(New clsF350A_Cv1Unit)
        'CvUnit.Add(New clsF350A_Cv2Unit)

        'CvRoller.Add(New clsF350A_Roller(1))
        'CvRoller.Add(New clsF350A_Roller(2))
    End Sub

    ''' <summary>取得或設定通過減速Sensor時,皮帶減速時間</summary>
    Property RollerSlowTime As UInteger = 2000

    ''' <summary>取得或設定未使用SMEMA時,皮帶Unload運轉時間</summary>
    Property RollerUnloadTime As UInteger = 5000

    ''' <summary>
    ''' 取得或設定單步作業未完成時,發生逾時之前的毫秒數
    ''' </summary>
    Property MotionTimeOut As Integer = 20000

    ''' <summary>
    ''' 取得或設定SMEMA發生逾時之前的毫秒數
    ''' </summary>
    Property SmemaTimeOut As Integer = 1800000

    ' ''' <summary>
    ' ''' 取得或設定單步作業未完成時,發生逾時之前的毫秒數
    ' ''' </summary>
    'Property SensorTimeOut As Integer = 30000

    Dim _isCv1TimeOut As Boolean
    ReadOnly Property IsCv1TimeOut As Boolean
        Get
            Return _isCv1TimeOut
        End Get
    End Property

    Dim _isCv2TimeOut As Boolean
    ReadOnly Property IsCv2TimeOut As Boolean
        Get
            Return _isCv2TimeOut
        End Get
    End Property

    Dim SwCv1TimeOut As New Stopwatch
    Dim SwCv2TimeOut As New Stopwatch

    'Conveyor各自獨立, 不使用整機動作
    ''' <summary>Conveyor整體復歸動作</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Private Sub Home(ByRef sys As sSysParam)
        With sys
            Select Case .SysNum
                Case 1000
                    If Unit IsNot Nothing Then
                        If Unit.A_Roller IsNot Nothing Then
                            Unit.A_Roller.Initial()
                        End If
                        If Unit.B_Roller IsNot Nothing Then
                            Unit.B_Roller.Initial()
                        End If
                    End If

                    .SysNum = 1100

                Case 1100 '復歸速度設定
                    If gCMotion.SetHomeVelAccDec(enmAxis.Conveyor1) = False Then 'S2 = Station 2 = Work Station
                        gEqpMsg.AddHistoryAlarm("Error_1040000", "CActionConveyorF350A_Home", sys.SysNum, gMsgHandler.GetMessage(Error_1040000), eMessageLevel.Error)
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    If gCMotion.SetHomeVelAccDec(enmAxis.Conveyor2) = False Then 'S3
                        gEqpMsg.AddHistoryAlarm("Error_1041000", "CActionConveyorF350A_Home", sys.SysNum, gMsgHandler.GetMessage(Error_1041000), eMessageLevel.Error)
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    .SysNum = 1200

                Case 1200 '復歸命令發出
                    If gCMotion.Home(enmAxis.Conveyor1) <> CommandStatus.Sucessed Then
                        gEqpMsg.AddHistoryAlarm("Error_1040016", "CActionConveyorF350A_Home", .SysNum, gMsgHandler.GetMessage(Error_1040016), eMessageLevel.Error)
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If

                    If gCMotion.Home(enmAxis.Conveyor2) <> CommandStatus.Sucessed Then
                        gEqpMsg.AddHistoryAlarm("Error_1041016", "CActionConveyorF350A_Home", .SysNum, gMsgHandler.GetMessage(Error_1041016), eMessageLevel.Error)
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum, False)  '吸真空OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, True)  '吹氣ON
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum, False)  '吸真空OFF
                    gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak1, True)  '吹氣ON

                    gDOCollection.SetState(enmDO.HeaterCylinderUp1, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown1, True)
                    gDOCollection.SetState(enmDO.HeaterCylinderUp2, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown2, True)

                    gDOCollection.SetState(enmDO.Station2StopperUp, True)
                    gDOCollection.SetState(enmDO.Station2StopperDown, False)
                    gDOCollection.SetState(enmDO.Station3StopperUp, True)
                    gDOCollection.SetState(enmDO.Station3StopperDown, False)
                    SwCv1TimeOut.Restart()
                    .SysNum = 1300

                Case 1300 '等待復歸完成
                    Dim ChuckStatus1 As CommandStatus = gCMotion.HomeFinish(enmAxis.Conveyor1)
                    Dim ChuckStatus2 As CommandStatus = gCMotion.HomeFinish(enmAxis.Conveyor2)
                    If ChuckStatus1 = CommandStatus.Sucessed AndAlso ChuckStatus2 = CommandStatus.Sucessed Then
                        sys.SysNum = 1400
                        SwCv1TimeOut.Restart()
                    ElseIf CheckCv1TimeOut(gSSystemParameter.TimeOut5) = True Then
                        SwCv1TimeOut.Stop()
                        If ChuckStatus1 <> CommandStatus.Sucessed Then
                            gEqpMsg.AddHistoryAlarm("Error_1040016", "CActionConveyorF350A_Home", sys.SysNum, gMsgHandler.GetMessage(Error_1040016))
                        End If
                        If ChuckStatus2 <> CommandStatus.Sucessed Then
                            gEqpMsg.AddHistoryAlarm("Error_1041016", "CActionConveyorF350A_Home", sys.SysNum, gMsgHandler.GetMessage(Error_1041016))
                        End If
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If

                Case 1400 '氣缸動作確認
                    If (Unit.A_IsVacuum(False) = False AndAlso Unit.B_IsVacuum = False) Then
                        gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, False)  '吹氣ON
                        gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak1, False) '吹氣ON

                        If (gDICollection.GetState(enmDI.Station2StopperUpReady) = True AndAlso gDICollection.GetState(enmDI.Station3StopperUpReady) = True AndAlso gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady) = True AndAlso gDICollection.GetState(enmDI.Station3Heater1CylinderDownReady) = True) Then
                            sys.Act(eAct.Home).RunStatus = enmRunStatus.Finish
                            sys.RunStatus = enmRunStatus.Finish
                            SwCv1TimeOut.Stop()
                        ElseIf CheckCv1TimeOut(gSSystemParameter.StableTime.CheckSensorTimeout) Then
                            If gDICollection.GetState(enmDI.Station2StopperUpReady) <> True Then
                                gEqpMsg.AddHistoryAlarm("Alarm_2037101", "CActionConveyorF350A_Home", sys.SysNum, gMsgHandler.GetMessage(Alarm_2037101))
                            End If
                            If gDICollection.GetState(enmDI.Station3StopperUpReady) <> True Then
                                gEqpMsg.AddHistoryAlarm("Alarm_2037201", "CActionConveyorF350A_Home", sys.SysNum, gMsgHandler.GetMessage(Alarm_2037201))
                            End If
                            If gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady) <> True Then
                                gEqpMsg.AddHistoryAlarm("Alarm_2037201", "CActionConveyorF350A_Home", sys.SysNum, gMsgHandler.GetMessage(Alarm_2037201))
                            End If
                            If gDICollection.GetState(enmDI.Station3Heater1CylinderDownReady) <> True Then
                                gEqpMsg.AddHistoryAlarm("Alarm_2037201", "CActionConveyorF350A_Home", sys.SysNum, gMsgHandler.GetMessage(Alarm_2037201))
                            End If
                            sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                    End If

            End Select
        End With
    End Sub

    ''' <summary>
    ''' 流道復歸
    ''' </summary>
    Public Sub Initial(ByRef sys As sSysParam)


        If (sys.ConveyorNo = eConveyor.ConveyorNo1) Then
            InitialCv1(sys)
        ElseIf (sys.ConveyorNo = eConveyor.ConveyorNo2) Then
            InitialCv2(sys)
        Else

        End If
    End Sub

    ''' <summary>
    ''' 進料
    ''' </summary>
    ''' <param name="auto">是否與SMEMA交握</param>
    Public Sub Load(ByRef sys As sSysParam, ByVal auto As Boolean)

        If (sys.ConveyorNo = eConveyor.ConveyorNo1) Then
            LoadCv1(sys, auto)
        ElseIf (sys.ConveyorNo = eConveyor.ConveyorNo2) Then
            LoadCv2(sys, auto)
        Else

        End If
    End Sub

    ''' <summary>
    ''' 退料
    ''' </summary>
    ''' <param name="auto">是否與SMEMA交握</param>
    Public Sub Unload(ByRef sys As sSysParam, ByVal auto As Boolean)
        If (sys.ConveyorNo = eConveyor.ConveyorNo1) Then
            UnloadCv1(sys, auto)
        ElseIf (sys.ConveyorNo = eConveyor.ConveyorNo2) Then
            UnloadCv2(sys, auto)
        Else

        End If
    End Sub


    ''' <summary>
    ''' A機復歸
    ''' </summary>
    Private Sub InitialCv1(ByRef sys As sSysParam)
        With sys
            Select Case .SysNum
                Case 1000   'Roller Initial
                    '[Note]:清除異常狀態
                    gCMotion.AxisResetError(sys.AxisConverter)

                    SwCv1TimeOut.Start()
                    CvSMEMA(0).IsReadyToSend = False
                    CvSMEMA(0).IsReadyToRecieve = False
                    Unit.A_Roller.Initial()

                    .SysNum = 1100

                Case 1100 '破真空
                    If (Unit.A_VacuumControl(False)) Then
                        SwCv1TimeOut.Restart()
                        .SysNum = 1200
                    ElseIf (CheckCv1TimeOut(MotionTimeOut)) Then
                        'Sue0710
                        'Conveyor1 真空逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083108", "CActionConveyorF350A_InitialCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2083108), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1200 'Stoper上升/頂升汽缸下降
                    If (Unit.A_Stoper(clsUnit.enmDirection.Up) And Unit.A_Lifter(IConveyorUnit.enmDirection.Down)) Then
                        SwCv1TimeOut.Restart()
                        .SysNum = 9000
                    ElseIf (CheckCv1TimeOut(MotionTimeOut)) Then
                        'Sue0710
                        'Conveyor1 安全狀態確認逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083122", "CActionConveyorF350A_InitialCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2083122), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 9000
                    SwCv1TimeOut.Reset()
                    sys.RunStatus = enmRunStatus.Finish

            End Select
        End With
    End Sub

    ''' <summary>
    ''' A機進料
    ''' </summary>
    Private Sub LoadCv1(ByRef sys As sSysParam, ByVal auto As Boolean)
        With sys
            Select Case .SysNum
                Case 1000 '檢查是否有料片在機台上
                    If (Unit.A_StoperSensor(True)) Then
                        'Sue0710
                        'A機上有產品
                        gEqpMsg.AddHistoryAlarm("Alarm_2083110", "CActionConveyorF350A_InitialCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2083110), eMessageLevel.Warning)
                        .SysNum = 9000
                    Else
                        SwCv1TimeOut.Start()
                        .SysNum = 1100
                    End If

                Case 1100 '頂升汽缸下降 / Stoper上升
                    If Unit.A_Lifter(IConveyorUnit.enmDirection.Down) AndAlso Unit.A_Stoper(clsUnit.enmDirection.Up) Then
                        SwCv1TimeOut.Restart()
                        .SysNum = 1200
                    ElseIf CheckCv1TimeOut(MotionTimeOut) Then
                        'Sue0710
                        If (Unit.A_Lifter(IConveyorUnit.enmDirection.Down) = False) Then
                            '氣缸下降逾時
                            gEqpMsg.AddHistoryAlarm("Alarm_2004001", "CActionConveyorF350A_LoadCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2004001), eMessageLevel.Warning)
                        ElseIf (Unit.A_Stoper(clsUnit.enmDirection.Up) = False) Then
                            'A機阻擋缸上升逾時
                            gEqpMsg.AddHistoryAlarm("Alarm_2083112", "CActionConveyorF350A_LoadCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2083112), eMessageLevel.Warning)
                        End If
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1200 'Loader是否可供料
                    If (CvSMEMA(0).IsLoaderReady Or auto = False) Then
                        SwCv1TimeOut.Restart()
                        .SysNum = 1300
                    ElseIf CheckCv1TimeOut(SmemaTimeOut) Then
                        'Sue0710
                        'Conveyor1 進料異常
                        gEqpMsg.AddHistoryAlarm("Alarm_2083007", "CActionConveyorF350A_LoadCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2083007), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1300 'Roller ON
                    If auto Then
                        CvSMEMA(0).IsReadyToRecieve = True '要料訊號 ON
                    End If

                    Unit.A_Roller.Load()

                    SwCv1TimeOut.Restart()
                    .SysNum = 1400

                Case 1400 'Roller減速
                    Dim mSec As UInteger = 0 '減速delay時間, 半自動狀態下mSec = 0
                    If auto Then
                        mSec = RollerSlowTime
                    End If
                    If CheckCv1TimeOut(mSec) Then
                        Unit.A_Roller.Load()
                        SwCv1TimeOut.Restart()
                        .SysNum = 1500
                    End If

                Case 1500 '檢查到位Sensor / 要料訊號 OFF
                    If (Unit.A_StoperSensor(True)) Then
                        CvSMEMA(0).IsReadyToRecieve = False
                        SwCv1TimeOut.Restart()
                        .SysNum = 1600
                    ElseIf CheckCv1TimeOut(MotionTimeOut) Then
                        'Sue0710
                        'Conveyor1 到位感測器觸發逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083123", "CActionConveyorF350A_LoadCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2083123), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1600 'Roller Stop
                    Unit.A_Roller.Stop()
                    SwCv1TimeOut.Restart()
                    .SysNum = 1700

                Case 1700  '頂升上升
                    If (Unit.A_Lifter(IConveyorUnit.enmDirection.Up)) Then
                        SwCv1TimeOut.Restart()
                        .SysNum = 1800
                    ElseIf CheckCv1TimeOut(MotionTimeOut) Then
                        'Sue0710
                        'A機 氣壓缸上升逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083118", "CActionConveyorF350A_LoadCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2083118), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1800 '真空建立
                    If Unit.A_VacuumControl(True) Then
                        SwCv1TimeOut.Restart()
                        .SysNum = 9000
                    ElseIf CheckCv1TimeOut(MotionTimeOut) Then
                        'Sue0710
                        'Conveyor1 真空逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083108", "CActionConveyorF350A_LoadCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2083108), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 9000
                    SwCv1TimeOut.Reset()
                    sys.RunStatus = enmRunStatus.Finish

            End Select
        End With
    End Sub

    ''' <summary>
    ''' A機退料
    ''' </summary>
    Private Sub UnloadCv1(ByRef sys As sSysParam, ByVal auto As Boolean)
        With sys
            Select Case .SysNum
                Case 1000 '檢查是否有料片在機台上
                    If (Unit.A_StoperSensor(True) = False) Then
                        'Sue0710
                        'Conveyor1 沒有料片在機台上
                        gEqpMsg.AddHistoryAlarm("Warn_3024001", "CActionConveyorF350A_UnloadCv1", .SysNum, gMsgHandler.GetMessage(Warn_3024001), eMessageLevel.Warning)
                        .SysNum = 9000
                    Else
                        SwCv1TimeOut.Restart()
                        .SysNum = 1100
                    End If

                Case 1100 'Unloader是否可收料
                    CvSMEMA(0).IsReadyToSend = True '出料訊號 ON
                    If (CvSMEMA(0).IsUnloaderReady Or auto = False) Then
                        CvSMEMA(0).IsReadyToSend = False '出料訊號 OFF
                        Unit.A_Roller.Stop()
                        SwCv1TimeOut.Restart()
                        .SysNum = 1200
                    ElseIf CheckCv1TimeOut(SmemaTimeOut) Then
                        'Sue0710
                        'Conveyor1 退料異常
                        gEqpMsg.AddHistoryAlarm("Alarm_2083008", "CActionConveyorF350A_UnloadCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2083008), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1200 '破真空
                    If (Unit.A_VacuumControl(False)) Then
                        SwCv1TimeOut.Restart()
                        .SysNum = 1300
                    ElseIf CheckCv1TimeOut(MotionTimeOut) Then
                        'Sue0710
                        'A機真空關閉逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083120", "CActionConveyorF350A_UnloadCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2083120), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1300 '頂升下降
                    If (Unit.A_Lifter(IConveyorUnit.enmDirection.Down)) Then
                        SwCv1TimeOut.Restart()
                        .SysNum = 1400
                    ElseIf CheckCv1TimeOut(MotionTimeOut) Then
                        'Sue0710
                        '汽缸下降逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2004001", "CActionConveyorF350A_UnloadCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2004001), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1400   '退料方向判斷
                    '同一邊進退料,則Stoper不下降
                    .SysNum = IIf(Unit.A_Roller.LoadDirection = Unit.A_Roller.UnloadDirection, 1500, 1600)
                    SwCv1TimeOut.Start()

                Case 1500 'Stoper 下降
                    If (Unit.A_Stoper(clsUnit.enmDirection.Down)) Then
                        SwCv1TimeOut.Restart()
                        .SysNum = 1600
                    ElseIf CheckCv1TimeOut(MotionTimeOut) Then
                        'Sue0710
                        'Conveyor1 阻擋缸下降逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083127", "CActionConveyorF350A_UnloadCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2083127), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1600 'Roller Start
                    Unit.A_Roller.Unload()
                    SwCv1TimeOut.Restart()
                    .SysNum = 1700

                Case 1700 'Unloader是否收到料
                    If (auto = False) Then
                        If (CheckCv1TimeOut(RollerUnloadTime)) Then    '無Unloader則退料5秒
                            SwCv1TimeOut.Restart()
                            .SysNum = 9000
                        End If

                    ElseIf (CvSMEMA(0).IsUnloaderReady = False) Then
                        SwCv1TimeOut.Restart()
                        .SysNum = 9000

                    ElseIf CheckCv1TimeOut(SmemaTimeOut) Then
                        'Sue0710
                        'Conveyor1 退料異常
                        gEqpMsg.AddHistoryAlarm("Alarm_2083008", "CActionConveyorF350A_UnloadCv1", .SysNum, gMsgHandler.GetMessage(Alarm_2083008), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm

                    End If

                Case 9000
                    Unit.A_Roller.Stop()
                    SwCv1TimeOut.Reset()
                    sys.RunStatus = enmRunStatus.Finish

            End Select
        End With
    End Sub

    ''' <summary>
    ''' B機復歸
    ''' </summary>
    Private Sub InitialCv2(ByRef sys As sSysParam)
        With sys
            Select Case .SysNum
                Case 1000   'Roller Initial
                    '[Note]:清除異常狀態
                    gCMotion.AxisResetError(sys.AxisConverter)
                    SwCv2TimeOut.Start()
                    CvSMEMA(1).IsReadyToSend = False
                    CvSMEMA(1).IsReadyToRecieve = False
                    Unit.B_Roller.Initial()
                    .SysNum = 1100

                Case 1100 '破真空
                    If (Unit.B_VacuumControl(False)) Then
                        SwCv2TimeOut.Restart()
                        .SysNum = 1200
                    ElseIf (CheckCv2TimeOut(MotionTimeOut)) Then
                        'Sue0710
                        'B機真空關閉逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2084120", "CActionConveyorF350A_InitialCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2084120), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1200 'Stoper上升/頂升汽缸下降
                    If (Unit.B_Stoper(clsUnit.enmDirection.Up) And Unit.B_Lifter(IConveyorUnit.enmDirection.Down)) Then
                        SwCv2TimeOut.Restart()
                        .SysNum = 9000
                    ElseIf (CheckCv2TimeOut(MotionTimeOut)) Then

                        'Sue0710
                        'Conveyor2 安全狀態確認逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2084122", "CActionConveyorF350A_InitialCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2084122), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 9000
                    SwCv2TimeOut.Reset()
                    sys.RunStatus = enmRunStatus.Finish

            End Select
        End With
    End Sub

    ''' <summary>
    ''' B機進料
    ''' </summary>
    Private Sub LoadCv2(ByRef sys As sSysParam, ByVal auto As Boolean)
        With sys
            Select Case .SysNum
                Case 1000 '起始
                    '檢查到位Sensor是否在安全狀態
                    If Unit.B_StoperSensor Then
                        'Sue0710
                        'B機上有產品
                        gEqpMsg.AddHistoryAlarm("Alarm_2084110", "CActionConveyorF350A_LoadCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2084110), eMessageLevel.Warning)
                        .SysNum = 9000
                    Else
                        SwCv2TimeOut.Start()
                        .SysNum = 1100
                    End If

                Case 1100 '頂升汽缸下降 / Stoper上升
                    If Unit.B_Lifter(IConveyorUnit.enmDirection.Down) AndAlso Unit.B_Stoper(clsUnit.enmDirection.Up) Then
                        SwCv2TimeOut.Restart()
                        .SysNum = 1200
                    ElseIf CheckCv2TimeOut(MotionTimeOut) Then
                        'Sue0710
                        If (Unit.A_Lifter(IConveyorUnit.enmDirection.Down) = False) Then
                            '氣缸下降逾時
                            gEqpMsg.AddHistoryAlarm("Alarm_2004001", "CActionConveyorF350A_LoadCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2004001), eMessageLevel.Warning)
                        ElseIf (Unit.A_Stoper(clsUnit.enmDirection.Up) = False) Then
                            'B機阻擋缸上升逾時
                            gEqpMsg.AddHistoryAlarm("Alarm_2084112", "CActionConveyorF350A_LoadCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2084112), eMessageLevel.Warning)
                        End If
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1200 'Loader是否可供料
                    If (CvSMEMA(1).IsLoaderReady Or auto = False) Then
                        SwCv2TimeOut.Restart()
                        .SysNum = 1300
                    ElseIf CheckCv2TimeOut(SmemaTimeOut) Then

                        'Sue0710
                        'Conveyor2 進料異常
                        gEqpMsg.AddHistoryAlarm("Alarm_2084007", "CActionConveyorF350A_LoadCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2084007), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1300 'Roller ON
                    If auto Then
                        CvSMEMA(1).IsReadyToRecieve = True '要料訊號 ON
                    End If

                    Unit.B_Roller.Load()

                    SwCv2TimeOut.Restart()
                    .SysNum = 1400

                Case 1400 'Roller減速
                    Dim mSec As UInteger = 0 '減速delay時間, 半自動狀態下mSec = 0
                    If auto Then
                        mSec = RollerSlowTime
                    End If
                    If CheckCv2TimeOut(mSec) Then
                        Unit.B_Roller.Load()
                        SwCv2TimeOut.Restart()
                        .SysNum = 1500
                    End If

                Case 1500 '檢查到位Sensor / 要料訊號 OFF
                    If (Unit.B_StoperSensor) Then
                        CvSMEMA(1).IsReadyToRecieve = False
                        SwCv2TimeOut.Restart()
                        .SysNum = 1600
                    ElseIf CheckCv2TimeOut(MotionTimeOut) Then

                        'Sue0710
                        'Conveyor2 到位感測器觸發逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2084123", "CActionConveyorF350A_LoadCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2084123), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1600 'Roller Stop
                    Unit.B_Roller.Stop()
                    SwCv2TimeOut.Restart()
                    .SysNum = 1700

                Case 1700  '頂升上升
                    If (Unit.B_Lifter(IConveyorUnit.enmDirection.Up)) Then
                        SwCv2TimeOut.Restart()
                        .SysNum = 1800
                    ElseIf CheckCv2TimeOut(MotionTimeOut) Then

                        'Sue0710
                        'B機 氣壓缸上升逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2084118", "CActionConveyorF350A_LoadCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2084118), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1800 '真空建立
                    If Unit.B_VacuumControl(True) Then
                        SwCv2TimeOut.Restart()
                        .SysNum = 9000
                    ElseIf CheckCv2TimeOut(MotionTimeOut) Then
                        'Sue0710
                        'Conveyor2 真空逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2084108", "CActionConveyorF350A_LoadCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2084108), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 9000
                    SwCv2TimeOut.Reset()
                    sys.RunStatus = enmRunStatus.Finish
            End Select
        End With
    End Sub

    ''' <summary>
    ''' B機退料
    ''' </summary>
    Private Sub UnloadCv2(ByRef sys As sSysParam, ByVal auto As Boolean)
        With sys
            Select Case .SysNum
                Case 1000 '檢查是否有料片在機台上
                    If (Unit.B_StoperSensor = False) Then

                        'Sue0710
                        'Conveyor2 沒有料片在機台上
                        gEqpMsg.AddHistoryAlarm("Warn_3024001", "CActionConveyorF350A_UnloadCv2", .SysNum, gMsgHandler.GetMessage(Warn_3024001), eMessageLevel.Warning)
                        .SysNum = 9000
                    Else
                        SwCv1TimeOut.Restart()
                        .SysNum = 1100
                    End If

                Case 1100 'Unloader是否可收料
                    CvSMEMA(1).IsReadyToSend = True '出料訊號 ON
                    If (CvSMEMA(1).IsUnloaderReady Or auto = False) Then
                        CvSMEMA(1).IsReadyToSend = False '出料訊號 OFF
                        Unit.B_Roller.Stop()
                        SwCv2TimeOut.Restart()
                        .SysNum = 1200
                    ElseIf CheckCv2TimeOut(SmemaTimeOut) Then

                        'Sue0710
                        'Conveyor2 退料異常
                        gEqpMsg.AddHistoryAlarm("Alarm_2084008", "CActionConveyorF350A_UnloadCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2084008), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1200 '破真空
                    If (Unit.B_VacuumControl(False)) Then
                        SwCv2TimeOut.Restart()
                        .SysNum = 1300
                    ElseIf CheckCv2TimeOut(MotionTimeOut) Then
                        'Sue0710
                        'B機真空關閉逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2084120", "CActionConveyorF350A_UnloadCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2084120), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1300 '頂升下降
                    If (Unit.B_Lifter(IConveyorUnit.enmDirection.Down)) Then
                        SwCv2TimeOut.Restart()
                        .SysNum = 1400
                    ElseIf CheckCv2TimeOut(MotionTimeOut) Then
                        'Sue0710
                        'Conveyor2 阻擋缸下降逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2084127", "CActionConveyorF350A_UnloadCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2084127), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1400
                    '同一邊進退料,則Stoper不下降
                    .SysNum = IIf(Unit.B_Roller.LoadDirection = Unit.B_Roller.UnloadDirection, 1500, 1600)
                    SwCv2TimeOut.Start()

                Case 1500 'Stoper 下降
                    If (Unit.B_Stoper(clsUnit.enmDirection.Down)) Then
                        SwCv2TimeOut.Restart()
                        .SysNum = 1600
                    ElseIf CheckCv2TimeOut(MotionTimeOut) Then
                        'Sue0710
                        'Conveyor2 阻擋缸下降逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2084127", "CActionConveyorF350A_UnloadCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2084127), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1600 'Roller Start
                    Unit.B_Roller.Unload()
                    SwCv2TimeOut.Restart()
                    .SysNum = 1700

                Case 1700 'Unloader是否收到料
                    If (auto = False) Then
                        If (CheckCv2TimeOut(RollerUnloadTime)) Then    '無Unloader則退料5秒
                            SwCv2TimeOut.Restart()
                            .SysNum = 9000
                        End If

                    ElseIf (CvSMEMA(1).IsUnloaderReady = False) Then
                        SwCv2TimeOut.Restart()
                        .SysNum = 9000

                    ElseIf CheckCv2TimeOut(SmemaTimeOut) Then
                        'Sue0710
                        'Conveyor2 退料異常
                        gEqpMsg.AddHistoryAlarm("Alarm_2084008", "CActionConveyorF350A_UnloadCv2", .SysNum, gMsgHandler.GetMessage(Alarm_2084008), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 9000
                    Unit.B_Roller.Stop()
                    SwCv2TimeOut.Reset()
                    sys.RunStatus = enmRunStatus.Finish

            End Select
        End With
    End Sub

    ''' <summary>
    ''' 一號流道動作是否逾時
    ''' </summary>
    Private Function CheckCv1TimeOut(ByVal time As UInteger) As Boolean
        If (SwCv1TimeOut.ElapsedMilliseconds > time) Then
            _isCv1TimeOut = True
            Return True
        End If
        _isCv1TimeOut = False
        Return False
    End Function

    ''' <summary>
    ''' 二號流道動作是否逾時
    ''' </summary>
    Private Function CheckCv2TimeOut(ByVal time As UInteger) As Boolean
        If (SwCv2TimeOut.ElapsedMilliseconds > time) Then
            _isCv2TimeOut = True
            Return True
        End If
        _isCv2TimeOut = False
        Return False
    End Function

End Class
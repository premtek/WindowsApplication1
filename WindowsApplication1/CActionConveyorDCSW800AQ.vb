Imports ProjectCore
Imports ProjectMotion
Imports ProjectIO
Imports WetcoConveyor
Imports WetcoConveyor.mGlobalPool

''' <summary>DCS-W800AQ傳送帶流程轉換範例</summary>
''' <remarks></remarks>
Public Class CActionConveyorDCSW800AQ
    ''' <summary>復歸逾時計時器</summary>
    ''' <remarks></remarks>
    Dim mHomeStopWatch As New Stopwatch

    Dim SwTimeOut As New Stopwatch

    Dim CvA As New clsCV_A
    Dim CvB As New clsCV_B

    Sub New()

        Dim time As UInteger
        Try
            Dim ConveyorIniPath As String = System.Windows.Forms.Application.StartupPath & "\system\" & MachineName & "\Conveyor.ini"
            time = ReadIniString("Roller Decelerate Time", "Time", ConveyorIniPath, 2000)
        Catch ex As Exception
        End Try

        CvA.RollerSlowTime = time
        CvB.RollerSlowTime = time
    End Sub

    ''' <summary>Conveyor整體復歸動作</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Public Sub Home(ByRef sys As sSysParam)
        With sys
            Select Case .SysNum
                Case 1000
                    CvA.MotionTimeOut = gSSystemParameter.TimeOut4
                    CvB.MotionTimeOut = gSSystemParameter.TimeOut4
                    CvA.SmemaTimeOut = gSSystemParameter.TimeOut6
                    CvB.SmemaTimeOut = gSSystemParameter.TimeOut6

                    Call gCMotion.Servo(enmAxis.MachineAChuck1, enmONOFF.eON) '通電確保
                    Call gCMotion.Servo(enmAxis.MachineBChuck1, enmONOFF.eON)

                    Call gCMotion.AxisResetError(enmAxis.MachineAChuck1) '清除錯誤
                    Call gCMotion.AxisResetError(enmAxis.MachineBChuck1)


                    Call gCMotion.SetCurve(enmAxis.MachineAChuck1, eCurveMode.TCurve) '命令規畫確保
                    Call gCMotion.SetCurve(enmAxis.MachineBChuck1, eCurveMode.TCurve)

                    gCMotion.DOOutput(enmAxis.MachineAChuck1, 7, enmCardIOONOFF.eOFF)
                    gCMotion.DOOutput(enmAxis.MachineBChuck1, 7, enmCardIOONOFF.eOFF)
                    gCMotion.DOOutput(enmAxis.MachineAChuck1, 7, enmCardIOONOFF.eON)
                    gCMotion.DOOutput(enmAxis.MachineBChuck1, 7, enmCardIOONOFF.eON)

                    '要求電動缸停止
                    gCMotion.EmgStop(enmAxis.MachineAChuck1) '停止確保
                    gCMotion.EmgStop(enmAxis.MachineBChuck1)

                    gDOCollection.SetState(enmDO.MoveInMotorCW, False) 'A機Roller
                    gDOCollection.SetState(enmDO.MoveInMotorSlow, False) 'A機關閉低速IO

                    gDOCollection.SetState(enmDO.MoveInMotorCW2, False) 'B機Roller
                    gDOCollection.SetState(enmDO.MoveInMotorSlow2, False) 'B機關閉低速IO

                    .SysNum = 1100

                Case 1100 '復歸速度設定
                    If gCMotion.SetHomeVelAccDec(enmAxis.MachineAChuck1) = False Then 'S2 = Station 2 = Work Station
                        gEqpMsg.AddHistoryAlarm("Error_1040000", "CActionConveyor800AQ_Home", sys.SysNum, gMsgHandler.GetMessage(Error_1040000), eMessageLevel.Error)
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    If gCMotion.SetHomeVelAccDec(enmAxis.MachineBChuck1) = False Then 'S3
                        gEqpMsg.AddHistoryAlarm("Error_1041000", "CActionConveyor800AQ_Home", sys.SysNum, gMsgHandler.GetMessage(Error_1041000), eMessageLevel.Error)
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    .SysNum = 1200

                Case 1200 '復歸命令發出
                    If gCMotion.Home(enmAxis.MachineAChuck1) <> CommandStatus.Sucessed Then
                        gEqpMsg.AddHistoryAlarm("Error_1040016", "CActionConveyor800AQ_Home", .SysNum, gMsgHandler.GetMessage(Error_1040016), eMessageLevel.Error)
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If

                    If gCMotion.Home(enmAxis.MachineBChuck1) <> CommandStatus.Sucessed Then
                        gEqpMsg.AddHistoryAlarm("Error_1041016", "CActionConveyor800AQ_Home", .SysNum, gMsgHandler.GetMessage(Error_1041016), eMessageLevel.Error)
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    gDOCollection.SetState(enmDO.Station1StopperUpDown, True)
                    gDOCollection.SetState(enmDO.Station1StopperDown, False)
                    gDOCollection.SetState(enmDO.Station2StopperUp, True) '氣缸命令
                    gDOCollection.SetState(enmDO.Station2StopperDown, False) '氣缸命令
                    gDOCollection.SetState(enmDO.Station3StopperUp, True) '氣缸命令
                    gDOCollection.SetState(enmDO.Station3StopperDown, False)
                    mHomeStopWatch.Restart()
                    .SysNum = 1300

                Case 1300 '等待復歸完成
                    Dim ChuckStatus1 As CommandStatus = gCMotion.HomeFinish(enmAxis.MachineAChuck1)
                    Dim ChuckStatus2 As CommandStatus = gCMotion.HomeFinish(enmAxis.MachineBChuck1)
                    If ChuckStatus1 = CommandStatus.Sucessed AndAlso ChuckStatus2 = CommandStatus.Sucessed Then
                        sys.SysNum = 1400
                        SwTimeOut.Restart()
                    ElseIf IsTimeOut(mHomeStopWatch, gSSystemParameter.TimeOut5) = True Then
                        mHomeStopWatch.Stop()
                        If ChuckStatus1 <> CommandStatus.Sucessed Then
                            gEqpMsg.AddHistoryAlarm("Error_1040016", "CActionConveyor800AQ_Home", sys.SysNum, gMsgHandler.GetMessage(Error_1040016))
                        End If
                        If ChuckStatus2 <> CommandStatus.Sucessed Then
                            gEqpMsg.AddHistoryAlarm("Error_1041016", "CActionConveyor800AQ_Home", sys.SysNum, gMsgHandler.GetMessage(Error_1041016))
                        End If
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If

                Case 1400 '氣缸動作確認
                    If gDICollection.GetState(enmDI.Station1StopperUpReady) = True AndAlso gDICollection.GetState(enmDI.Station2StopperUpReady) = True AndAlso gDICollection.GetState(enmDI.Station3StopperUpReady) = True Then
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Finish
                        sys.RunStatus = enmRunStatus.Finish
                        mHomeStopWatch.Stop()
                    ElseIf IsTimeOut(mHomeStopWatch, gSSystemParameter.StableTime.CheckSensorTimeout) Then
                        If gDICollection.GetState(enmDI.Station1StopperUpReady) <> True Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2037001", "CActionConveyor800AQ_Home", sys.SysNum, gMsgHandler.GetMessage(Alarm_2037001))
                        End If
                        If gDICollection.GetState(enmDI.Station2StopperUpReady) <> True Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2037101", "CActionConveyor800AQ_Home", sys.SysNum, gMsgHandler.GetMessage(Alarm_2037101))
                        End If
                        If gDICollection.GetState(enmDI.Station3StopperUpReady) <> True Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2037201", "CActionConveyor800AQ_Home", sys.SysNum, gMsgHandler.GetMessage(Alarm_2037201))
                        End If
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If

            End Select
        End With

    End Sub

    ''' <summary>
    ''' A機復歸
    ''' </summary>
    Public Sub HomePartA(ByRef sys As sSysParam)
        With sys
            Select Case .SysNum
                Case 1000
                    CvA.MotionTimeOut = gSSystemParameter.TimeOut4
                    CvA.SmemaTimeOut = gSSystemParameter.TimeOut6
                    Call gCMotion.Servo(enmAxis.MachineAChuck1, enmONOFF.eON) '通電確保
                    Call gCMotion.AxisResetError(enmAxis.MachineAChuck1) '清除錯誤
                    Call gCMotion.SetCurve(enmAxis.MachineAChuck1, eCurveMode.TCurve) '命令規畫確保
                    gCMotion.DOOutput(enmAxis.MachineAChuck1, 7, enmCardIOONOFF.eOFF) '電動缸alarm重製
                    gCMotion.DOOutput(enmAxis.MachineAChuck1, 7, enmCardIOONOFF.eON)

                    CvA.Reset()
                    .SysNum = 1100

                Case 1100 '流道動作
                    sys.RunStatus = enmRunStatus.Running
                    Dim status As enmMotionStatus = CvA.Initial() '執行動作
                    If (status = enmMotionStatus.Finish) Then
                        .SysNum = 9000
                    ElseIf (status = enmMotionStatus.Alarm) Then
                        gEqpMsg.AddHistoryAlarm(CvA.AlarmCode, "A_InitialMotion()", CvA.TaskStep.ToString(), gMsgHandler.GetMessage(CvA.AlarmID), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 9000
                    sys.RunStatus = enmRunStatus.Finish
                    .SysNum = 1000
            End Select
        End With
    End Sub

    ''' <summary>
    ''' A機進料
    ''' </summary>
    Public Sub LoadA(ByRef sys As sSysParam, ByVal auto As Boolean)
        With sys
            Select Case .SysNum
                Case 1000
                    CvA.Reset()
                    .SysNum = 1100

                Case 1100
                    If (cls800AQ_LUL.Loader.IsAlarm) Then
                        gEqpMsg.AddHistoryAlarm(CvA.AlarmCode, "A_LoadMotion()", 0, "SMEMA Loader Alarm", eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    Else
                        .SysNum = 1200
                    End If

                Case 1200 '流道動作
                    sys.RunStatus = enmRunStatus.Running
                    Dim status As enmMotionStatus = CvA.Load(auto) '執行動作
                    If (status = enmMotionStatus.Finish) Then
                        If (auto AndAlso cls800AQ_LUL.IsLoaderPass = False AndAlso cls800AQ_LUL.Loader.IsOpen) Then
                            .SysNum = 1300
                        Else
                            .SysNum = 9000
                        End If
                    ElseIf (status = enmMotionStatus.Alarm) Then
                        gEqpMsg.AddHistoryAlarm(CvA.AlarmCode, "A_LoadMotion()", CvA.TaskStep.ToString(), gMsgHandler.GetMessage(CvA.AlarmID), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1300 '發送PLC命令
                    If (cls800AQ_LUL.GetProductNum(cls800AQLul.enmDevice.Loader)) Then
                        .SysNum = 1400
                    End If

                Case 1400 '取得Product Number
                    If (cls800AQ_LUL.IsLoaderBusy = False) Then
                        cls800AQ_LUL.A_ProductNum = cls800AQ_LUL.Loader.Data.ProductNum
                        .SysNum = 1500
                    End If

                Case 1500
                    If (cls800AQ_LUL.GetCastetData(cls800AQLul.enmDevice.Loader, False)) Then
                        .SysNum = 1600
                    End If

                Case 1600
                    If (cls800AQ_LUL.IsLoaderBusy = False) Then
                        .SysNum = 9000
                    End If

                Case 9000
                    sys.RunStatus = enmRunStatus.Finish
                    .SysNum = 1000
            End Select

        End With
    End Sub

    ''' <summary>
    ''' A機退料
    ''' </summary>
    Public Sub UnloadA(ByRef sys As sSysParam, ByVal auto As Boolean)
        If (gSSystemParameter.MachineType = enmMachineType.DCS_500AD) Then
            CvA.ByPassMachineB = True
        End If

        With sys
            Select Case .SysNum
                Case 1000
                    CvA.Reset()
                    .SysNum = 1100

                Case 1100
                    If (cls800AQ_LUL.Unloader.IsAlarm) Then
                        gEqpMsg.AddHistoryAlarm(CvA.AlarmCode, "A_UnloadMotion()", 0, "SMEMA Unloader Alarm", eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    Else
                        If (auto AndAlso cls800AQ_LUL.IsUnloaderPass = False AndAlso cls800AQ_LUL.Unloader.IsOpen) Then
                            .SysNum = 1200
                        Else
                            .SysNum = 1400
                        End If
                    End If

                Case 1200 '發送PLC命令
                    If (cls800AQ_LUL.SetProductNum(cls800AQ_LUL.A_ProductNum, cls800AQLul.enmDevice.Unloader)) Then
                        .SysNum = 1300
                    End If

                Case 1300
                    If (cls800AQ_LUL.IsUnloaderBusy = False) Then
                        .SysNum = 1400
                    End If

                Case 1400 '流道動作
                    sys.RunStatus = enmRunStatus.Running
                    Dim status As enmMotionStatus = CvA.Unload(auto) '執行動作
                    If (status = enmMotionStatus.Finish) Then
                        cls800AQ_LUL.A_ProductNum = 0 '清空 Product Number
                        .SysNum = 9000
                    ElseIf (status = enmMotionStatus.Alarm) Then
                        gEqpMsg.AddHistoryAlarm(CvA.AlarmCode, "A_UnloadMotion()", CvA.TaskStep.ToString(), gMsgHandler.GetMessage(CvA.AlarmID), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 9000
                    sys.RunStatus = enmRunStatus.Finish
                    .SysNum = 1000
            End Select

        End With
    End Sub

    ''' <summary>
    ''' B機復歸
    ''' </summary>
    Public Sub HomePartB(ByRef sys As sSysParam)
        With sys
            Select Case .SysNum
                Case 1000
                    CvB.MotionTimeOut = gSSystemParameter.TimeOut4
                    CvB.SmemaTimeOut = gSSystemParameter.TimeOut6
                    Call gCMotion.Servo(enmAxis.MachineBChuck1, enmONOFF.eON)
                    Call gCMotion.AxisResetError(enmAxis.MachineBChuck1)
                    Call gCMotion.SetCurve(enmAxis.MachineBChuck1, eCurveMode.TCurve)
                    gCMotion.DOOutput(enmAxis.MachineBChuck1, 7, enmCardIOONOFF.eOFF)
                    gCMotion.DOOutput(enmAxis.MachineBChuck1, 7, enmCardIOONOFF.eON)

                    CvB.Reset()
                    .SysNum = 1100

                Case 1100 '流道動作
                    sys.RunStatus = enmRunStatus.Running
                    Dim status As enmMotionStatus = CvB.Initial() '執行動作
                    If (status = enmMotionStatus.Finish) Then
                        .SysNum = 9000
                    ElseIf (status = enmMotionStatus.Alarm) Then
                        gEqpMsg.AddHistoryAlarm(CvB.AlarmCode, "B_InitialMotion()", CvB.TaskStep.ToString(), gMsgHandler.GetMessage(CvB.AlarmCode), eMessageLevel.Alarm)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 9000
                    sys.RunStatus = enmRunStatus.Finish
                    .SysNum = 1000
            End Select
        End With
    End Sub

    ''' <summary>
    ''' B機進料
    ''' </summary>
    Public Sub LoadB(ByRef sys As sSysParam, ByVal auto As Boolean)
        With sys
            Select Case .SysNum
                Case 1000
                    CvB.Reset()
                    .SysNum = 1100

                Case 1100
                    If (cls800AQ_LUL.Loader.IsAlarm) Then
                        gEqpMsg.AddHistoryAlarm(CvB.AlarmCode, "B_LoadMotion()", 0, "SMEMA Loader Alarm", eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    Else
                        .SysNum = 1200
                    End If

                Case 1200 '流道動作
                    sys.RunStatus = enmRunStatus.Running
                    Dim status As enmMotionStatus = CvB.Load(auto) '執行動作
                    If (status = enmMotionStatus.Finish) Then
                        If (auto AndAlso cls800AQ_LUL.IsLoaderPass = False AndAlso cls800AQ_LUL.Loader.IsOpen) Then
                            .SysNum = 1300
                        Else
                            .SysNum = 9000
                        End If
                    ElseIf (status = enmMotionStatus.Alarm) Then
                        gEqpMsg.AddHistoryAlarm(CvB.AlarmCode, "B_LoadMotion()", CvB.TaskStep.ToString(), gMsgHandler.GetMessage(CvB.AlarmCode), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1300 '發送PLC命令
                    If (cls800AQ_LUL.GetProductNum(cls800AQLul.enmDevice.Loader)) Then
                        .SysNum = 1400
                    End If

                Case 1400 '取得Product Number
                    If (cls800AQ_LUL.IsLoaderBusy = False) Then
                        cls800AQ_LUL.B_ProductNum = cls800AQ_LUL.Loader.Data.ProductNum
                        .SysNum = 1500
                    End If

                Case 1500
                    If (cls800AQ_LUL.GetCastetData(cls800AQLul.enmDevice.Loader, False)) Then
                        .SysNum = 1600
                    End If

                Case 1600
                    If (cls800AQ_LUL.IsLoaderBusy = False) Then
                        .SysNum = 9000
                    End If

                Case 9000
                    sys.RunStatus = enmRunStatus.Finish
                    .SysNum = 1000
            End Select

        End With
    End Sub

    ''' <summary>
    ''' B機退料
    ''' </summary>
    Public Sub UnloadB(ByRef sys As sSysParam, ByVal auto As Boolean)
        With sys
            Select Case .SysNum
                Case 1000
                    CvB.Reset()
                    .SysNum = 1100

                Case 1100
                    If (cls800AQ_LUL.Unloader.IsAlarm) Then
                        gEqpMsg.AddHistoryAlarm(CvB.AlarmCode, "B_UnloadMotion()", 0, "SMEMA Unloader Alarm", eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    Else
                        If (auto AndAlso cls800AQ_LUL.IsUnloaderPass = False AndAlso cls800AQ_LUL.Unloader.IsOpen) Then
                            .SysNum = 1200
                        Else
                            .SysNum = 1400
                        End If
                    End If

                Case 1200 '發送PLC命令
                    If (cls800AQ_LUL.SetProductNum(cls800AQ_LUL.B_ProductNum, cls800AQLul.enmDevice.Unloader)) Then
                        .SysNum = 1300
                    End If

                Case 1300
                    If (cls800AQ_LUL.IsUnloaderBusy = False) Then
                        .SysNum = 1400
                    End If

                Case 1400 '流道動作
                    sys.RunStatus = enmRunStatus.Running
                    Dim status As enmMotionStatus = CvB.Unload(auto) '執行動作
                    If (status = enmMotionStatus.Finish) Then
                        cls800AQ_LUL.B_ProductNum = 0 '清空 Product Number
                        .SysNum = 9000
                    ElseIf (status = enmMotionStatus.Alarm) Then
                        gEqpMsg.AddHistoryAlarm(CvB.AlarmCode, "B_UnloadMotion()", CvB.TaskStep.ToString(), gMsgHandler.GetMessage(CvB.AlarmCode), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 9000
                    sys.RunStatus = enmRunStatus.Finish
                    .SysNum = 1000
            End Select

        End With
    End Sub

End Class

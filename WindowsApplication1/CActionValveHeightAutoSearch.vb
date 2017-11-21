Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO

''' <summary>閥頭自動測高, 需指定Sys,SelectValve</summary>
''' <remarks></remarks>
Public Class CActionValveHeightAutoSearch
    ''' <summary>[紀錄檢查Sensor之起始時間]</summary>
    ''' <remarks></remarks>
    Private mCheckSensorStart As Decimal
    Dim AutoSearchWatch(enmStage.Max) As Stopwatch
    ''' <summary>
    ''' [紀錄Latch時的位置]
    ''' </summary>
    ''' <remarks></remarks>
    Dim LatchPos(enmValve.Max) As Decimal
    ''' <summary>
    ''' [用來紀錄DispenserAutoSearch前馬達之座標]
    ''' </summary>
    ''' <remarks></remarks>
    Dim PosX(enmValve.Max) As Decimal
    ''' <summary>
    ''' [用來紀錄DispenserAutoSearch前馬達之座標]
    ''' </summary>
    ''' <remarks></remarks>
    Dim PosY(enmValve.Max) As Decimal
    ''' <summary>
    ''' [用來紀錄DispenserAutoSearch前馬達之座標]
    ''' </summary>
    ''' <remarks></remarks>
    Dim PosZ(enmValve.Max) As Decimal
    Dim mPosX(enmValve.Max) As Decimal
    Dim mPosY(enmValve.Max) As Decimal
    Dim mPosZ(enmValve.Max) As Decimal
    ''' <summary>
    ''' [座標(B Axis-->Tilt Axis)]
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPosB(enmStage.Max) As Decimal
    ''' <summary>
    ''' [Z軸的狀態]
    ''' </summary>
    ''' <remarks></remarks>
    Dim mAxisZState(enmStage.Max) As CommandStatus
    ''' <summary>
    ''' [Tilt軸的狀態]
    ''' </summary>
    ''' <remarks></remarks>
    Dim mAxisBState(enmStage.Max) As CommandStatus
    'jimmy 20170822 手動觸發HeightAutoSearch
    Private mbManualHeightAutoSearchAction As Boolean
    Public Property _bManualHeightAutoSearchAction() As Boolean
        Get
            Return mbManualHeightAutoSearchAction
        End Get
        Set(ByVal value As Boolean)
            mbManualHeightAutoSearchAction = value
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
        For mStageNo As Integer = 0 To AutoSearchWatch.Count - 1
            AutoSearchWatch(mStageNo) = New Stopwatch
        Next
    End Sub
    ''' <summary>
    ''' 閥頭自動測高動作流程
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Run(ByRef sys As sSysParam) As enmRunStatus
        If mbManualHeightAutoSearchAction = True Then
            sys.SelectValve = mManualSelectValve
        End If
        With sys
            Select Case .SysNum
                Case sSysParam.SysLoopStart

                    If sys.AxisB <> -1 Then
                        Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                    End If

                    '[說明]:紀錄當下的馬達座標 & 關閉出膠 & 氣缸上升

                    '[說明]:紀錄當下的馬達座標
                    PosX(sys.StageNo) = CDbl(gCMotion.GetPositionValue(sys.AxisX))
                    PosY(sys.StageNo) = CDbl(gCMotion.GetPositionValue(sys.AxisY))
                    PosZ(sys.StageNo) = CDbl(gCMotion.GetPositionValue(sys.AxisZ))

                    '[說明]:關閉出膠
                    Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eOff)
                    'mZHightSearchLimit = gSSystemParameter.Pos.dblPinNo1ZHightSearchLimit(sys.SelectValve)


                    '[說明]:第二隻Valve的氣缸上昇 
                    Call ValveCylinderAction(sys.StageNo, eValveWorkMode.Valve2, enmUpDown.Up, AutoSearchWatch(.StageNo))
                    AutoSearchWatch(.StageNo).Restart()
                    mCheckSensorStart = AutoSearchWatch(.StageNo).ElapsedMilliseconds
                    .SysNum = 1100

                Case 1100
                    '[說明]:設定移動速度
                    '[說明]:X軸

                    If gCMotion.SetVelAccDec(sys.AxisX) = False Then
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                    '[說明]:Y軸
                    If gCMotion.SetVelAccDec(sys.AxisY) = False Then
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                    '[說明]:Z軸
                    If gCMotion.SetVelAccDec(sys.AxisZ) = False Then
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                    .SysNum = 1200


                Case 1200
                    '[說明]:氣缸到位偵測
                    If ValveCylinderSensor(sys.StageNo, eValveWorkMode.Valve2, enmUpDown.Up) = True Then
                        Call AutoSearchWatch(.StageNo).Stop()
                        .SysNum = 1300
                    Else
                        If IsTimeOut(AutoSearchWatch(.StageNo), mCheckSensorStart, gSSystemParameter.StableTime.CheckSensorTimeout) = True Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2004000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Alarm_2004000), eMessageLevel.Alarm)    '[Cylinder Up Down Sensor Alarm]
                            sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                    End If

                Case 1300
                    '[說明]:Z軸移至Up高度，避免撞到流道與Purge平台
                    mPosZ(sys.StageNo) = gSSystemParameter.Pos.ZUpPos
                    If gCMotion.AbsMove(sys.AxisZ, mPosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                        End Select
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                    .SysNum = 1400

                Case 1400
                    If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1032004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm '移動逾時
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If
                    '[Note]:確認Tilt有無存在，存在則流程會增加Z軸上升-->Tilt旋轉
                    If sys.AxisB <> -1 Then
                        Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                        sys.SysNum = 1410
                    Else
                        sys.SysNum = 1500
                    End If

                Case 1410
                    '[Note]:根據Recipe給的Tilt角度決定轉至該位置
                    If gCMotion.AbsMove(sys.AxisB, mPosB(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1034000", "DispenserAutoSearchAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1046000", "DispenserAutoSearchAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1064000", "DispenserAutoSearchAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1071000", "DispenserAutoSearchAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071000), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    sys.SysNum = 1420

                Case 1420
                    '[Note]:等待Table Stop
                    mAxisBState(sys.StageNo) = gCMotion.MotionDone(sys.AxisB)
                    If mAxisBState(sys.StageNo) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisB) Then
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1034004", "DispenserAutoSearchAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1046004", "DispenserAutoSearchAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1064004", "DispenserAutoSearchAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1071004", "DispenserAutoSearchAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071004), eMessageLevel.Error)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                    Else
                        sys.SysNum = 1500
                    End If


                Case 1500
                    Call ValveCylinderAction(sys.StageNo, sys.SelectValve, enmUpDown.Down, AutoSearchWatch(.StageNo))
                    AutoSearchWatch(.StageNo).Restart()
                    mCheckSensorStart = AutoSearchWatch(.StageNo).ElapsedMilliseconds
                    .SysNum = 1600

                Case 1600
                    '[說明]:Step3:氣缸到位偵測(第二支螺桿下降)
                    If ValveCylinderSensor(sys.StageNo, sys.SelectValve, enmUpDown.Down) = True Then
                        Call AutoSearchWatch(.StageNo).Stop()
                        .SysNum = 1700
                    Else
                        If IsTimeOut(AutoSearchWatch(.StageNo), mCheckSensorStart, gSSystemParameter.StableTime.CheckSensorTimeout) = True Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2004001", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Alarm_2004001), eMessageLevel.Alarm)   '[Cylinder Up Down Sensor Alarm]
                            sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                    End If

                Case 1700
                    '[說明]:X Y 至待測位置
                    mPosX(sys.StageNo) = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).ValvePinX(.SelectValve, mPosB(sys.StageNo))
                    mPosY(sys.StageNo) = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).ValvePinY(.SelectValve, mPosB(sys.StageNo))


                    If gCMotion.AbsMove(sys.AxisX, mPosX(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1030000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1042000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1060000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1067000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1030000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    If gCMotion.AbsMove(sys.AxisY, mPosY(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1031000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1043000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1061000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1068000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1031000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
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
                                    gEqpMsg.AddHistoryAlarm("Error_1030004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1042004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1060004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1067004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1030004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm '移動逾時
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If
                    If gCMotion.MotionDone(sys.AxisY) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisY) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1031004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1043004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1061004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1068004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1031004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm '移動逾時
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If

                    mPosZ(sys.StageNo) = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).ValvePinZ(sys.SelectValve, mPosB(sys.StageNo)) '測高開始高度
                    If gCMotion.AbsMove(sys.AxisZ, mPosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error) 'Z軸命令執行失敗
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm
                    End If

                    .SysNum = 1820

                Case 1820
                    If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.Add("DispenserAutoSearchAction", Error_1032004, eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.Add("DispenserAutoSearchAction", Error_1044004, eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.Add("DispenserAutoSearchAction", Error_1062004, eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.Add("DispenserAutoSearchAction", Error_1069004, eMessageLevel.Error)
                            End Select
                            'gEqpMsg.Add("DispenserAutoSearchAction", Error_1032004, eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm '移動逾時
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If
                    .SysNum = 1900

                Case 1900
                    '[說明]:清除Latch資料 & 設定Z軸向下之速度
                    If gCMotion.SetVelLow(sys.AxisZ, 0) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032014", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032014), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044014", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044014), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062014", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062014), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069014", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069014), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032012), eMessageLevel.Error)
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    If gCMotion.SetVelHigh(sys.AxisZ, 4) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032013), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044013), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062013), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069013), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032013), eMessageLevel.Error)
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    If gCMotion.SetAcc(sys.AxisZ, 1) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032011), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044011), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062011), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069011), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032011), eMessageLevel.Error)
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    If gCMotion.SetDec(sys.AxisZ, 1) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032012), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044012), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062012), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069012), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032012), eMessageLevel.Error)
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                    gCMotion.ResetLatch(sys.AxisZ)
                    .SysNum = 2000

                Case 2000
                    '[說明]:等速度向下 直到Latch訊號ON
                    If gCMotion.VelMove(sys.AxisZ, eDirection.Negative) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1030000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                    .SysNum = 2100

                Case 2100
                    '[說明]: 若超過測高極限則停止 & 跳Alarm
                    If gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserPinLimitZ(sys.SelectValve, mPosB(sys.StageNo)) <> 0 AndAlso
                        gCMotion.GetPositionValue(sys.AxisZ) < gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserPinLimitZ(sys.SelectValve, 0) Then
                        Call gCMotion.EmgStop(sys.AxisZ)
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032006", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032006), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044006", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044006), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062006", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062006), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069006", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069006), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1030006", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1030006), eMessageLevel.Error)
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    Else
                        '[說明]:直到Latch訊號ON
                        If gCMotion.AxisParameter(sys.AxisZ).MotionIOStatus.blnLTC = False Then '低電壓才是On
                            Call gCMotion.EmgStop(sys.AxisZ)
                            AutoSearchWatch(.StageNo).Restart()
                            .SysNum = 2200
                        End If
                    End If

                Case 2200
                    '[說明]:清除Latch資料 & 設定Z軸向上之速度 & 檢查Z軸停止
                    If AutoSearchWatch(.StageNo).ElapsedMilliseconds > 50 Then
                        If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                            'IsMoveTimeOut尚未支援
                            Return enmRunStatus.Running
                            'Return enmRunStatus.Alarm '到位異常
                        End If
                        If gCMotion.SetVelLow(sys.AxisZ, 0) <> CommandStatus.Sucessed Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032014", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032014), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044014", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044014), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062014", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062014), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069014", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069014), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1032012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032012), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                        If gCMotion.SetVelHigh(sys.AxisZ, 2) <> CommandStatus.Sucessed Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032013), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044013), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062013), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069013), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1032013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032013), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                        If gCMotion.SetAcc(sys.AxisZ, 1) <> CommandStatus.Sucessed Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032011), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044011), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062011), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069011), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1032011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032011), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                        If gCMotion.SetDec(sys.AxisZ, 1) <> CommandStatus.Sucessed Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032012), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044012), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062012), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069012), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1032012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032012), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If


                        If gCMotion.ResetLatch(sys.AxisZ) <> CommandStatus.Sucessed Then
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                        .SysNum = 2300
                    Else
                        .SysNum = 2200
                    End If

                Case 2300
                    '[說明]:等速度向上 直到Latch訊號OFF
                    If gCMotion.VelMove(sys.AxisZ, eDirection.Positive) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    .SysNum = 2400

                Case 2400
                    '[說明]:若超過極限則停止 & 跳Alarm
                    If gCMotion.GetPositionValue(sys.AxisZ) > gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit Then
                        Call gCMotion.EmgStop(sys.AxisZ)
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032005", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032005), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044005", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044005), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062005", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062005), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069005", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069005), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032005", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032005), eMessageLevel.Error)
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    Else
                        '[說明]:直到Latch訊號OFF
                        If gCMotion.AxisParameter(sys.AxisZ).MotionIOStatus.blnLTC = True Then '高電壓是Off
                            Call gCMotion.EmgStop(sys.AxisZ)
                            AutoSearchWatch(.StageNo).Restart()
                            .SysNum = 2500
                        End If
                    End If


                Case 2500
                    '先判斷EmgStop 後有沒有超過50 msce
                    If AutoSearchWatch(.StageNo).ElapsedMilliseconds > 50 Then
                        '[說明]:清除Latch資料 & 設定Z軸向下之速度 & 檢查Z軸停止
                        If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                            Return enmRunStatus.Running
                        End If
                        If gCMotion.SetVelLow(sys.AxisZ, 0) <> CommandStatus.Sucessed Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032014", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032014), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044014", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044014), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062014", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062014), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069014", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069014), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1032012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032012), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                        If gCMotion.SetVelHigh(sys.AxisZ, 1) <> CommandStatus.Sucessed Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032013), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044013), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062013), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069013), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1032013", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032013), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                        If gCMotion.SetAcc(sys.AxisZ, 1) <> CommandStatus.Sucessed Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032011), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044011), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062011), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069011), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1032011", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032011), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                        If gCMotion.SetDec(sys.AxisZ, 1) <> CommandStatus.Sucessed Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032012), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044012), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062012), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069012), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1032012", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032012), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                        If gCMotion.ResetLatch(sys.AxisZ) <> CommandStatus.Sucessed Then
                            Return enmRunStatus.Alarm
                        End If
                        .SysNum = 2600
                    Else
                        '還沒超過50msec 繼續在case 2500 wait
                        .SysNum = 2500
                    End If

                Case 2600
                    '[說明]:等速度向下 直到Latch訊號ON
                    If gCMotion.VelMove(sys.AxisZ, eDirection.Negative) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    .SysNum = 2700

                Case 2700
                    '[說明]: 若超過測高極限則停止 & 跳Alarm
                    If gCMotion.GetPositionValue(sys.AxisZ) > gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserPinLimitZ(sys.SelectValve, mPosB(sys.StageNo)) Then
                        Call gCMotion.EmgStop(sys.AxisZ)
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032005", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032005), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044005", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044005), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062005", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062005), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069005", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069005), eMessageLevel.Error)
                        End Select
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    Else
                        '[說明]:直到Latch訊號ON
                        If gCMotion.AxisParameter(sys.AxisZ).MotionIOStatus.blnLTC = False Then '低電壓是On
                            Call gCMotion.EmgStop(sys.AxisZ)
                            AutoSearchWatch(.StageNo).Restart()
                            .SysNum = 2800
                        End If
                    End If

                Case 2800
                    '判斷emgStop 後超過50msec
                    If AutoSearchWatch(.StageNo).ElapsedMilliseconds > 50 Then
                        '[說明]:檢查Z軸停止 & 取出Counter Value
                        If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                            If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                                Select Case .StageNo
                                    Case 0
                                        gEqpMsg.AddHistoryAlarm("Error_1032004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                                    Case 1
                                        gEqpMsg.AddHistoryAlarm("Error_1044004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                                    Case 2
                                        gEqpMsg.AddHistoryAlarm("Error_1062004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                                    Case 3
                                        gEqpMsg.AddHistoryAlarm("Error_1069004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                                End Select
                                If mbManualHeightAutoSearchAction = True Then
                                    mbManualHeightAutoSearchAction = False
                                    .SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm '移動逾時
                            Else
                                Return enmRunStatus.Running
                            End If
                        End If
                        Call gCMotion.GetLatchPosition(sys.AxisZ, enmPositionType.ActualPosition, LatchPos(sys.SelectValve))
                        .SysNum = 2900
                    Else
                        .SysNum = 2800
                    End If

                Case 2900
                    '[說明]:Z軸移動速度
                    If gCMotion.SetHomeVelAccDec(sys.AxisZ) = False Then
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    .SysNum = 3000

                Case 3000
                    '[說明]:Z軸回到Up之高度，避免撞到流道與Purge平台
                    If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error) 'Z軸命令執行失敗
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If

                    .SysNum = 3100

                Case 3100
                    '[說明]:等到定位後移動汽缸
                    If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1032000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm '移動逾時
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If

                    '[說明]:第二隻Valve的氣缸上昇
                    Call ValveCylinderAction(sys.StageNo, enmValve.No2, enmUpDown.Up, AutoSearchWatch(.StageNo))
                    '[說明]:Sensor檢查計數開始
                    mCheckSensorStart = AutoSearchWatch(.StageNo).ElapsedMilliseconds

                    .SysNum = 3200

                Case 3200
                    If ValveCylinderSensor(sys.StageNo, enmValve.No2, enmUpDown.Up) = True Then
                        Call AutoSearchWatch(.StageNo).Stop()
                        .SysNum = 3300
                    Else
                        If IsTimeOut(AutoSearchWatch(.StageNo), mCheckSensorStart, gSSystemParameter.StableTime.CheckSensorTimeout) = True Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2004000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Alarm_2004000), eMessageLevel.Alarm)    '[Cylinder Up Down Sensor Alarm]
                            sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm
                        End If
                    End If

                Case 3300
                    '[說明]:移回測高前的位置
                    If gCMotion.AbsMove(sys.AxisX, PosX(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1030000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1042000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1060000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1067000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1030000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    If gCMotion.AbsMove(sys.AxisY, PosY(sys.StageNo)) <> CommandStatus.Sucessed Then
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    .SysNum = 3400

                Case 3400
                    If gCMotion.MotionDone(sys.AxisX) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1030004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1042004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1060004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1067004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1030004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
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
                                    gEqpMsg.AddHistoryAlarm("Error_1031004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1043004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1061004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1068004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1031004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm '移動逾時
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If
                    .SysNum = 3500

                Case 3500
                    If gCMotion.AbsMove(sys.AxisZ, PosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                        Select Case .StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032000", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm
                        If mbManualHeightAutoSearchAction = True Then
                            mbManualHeightAutoSearchAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                    .SysNum = 3600

                Case 3600
                    If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                            Select Case .StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1032004", "DispenserAutoSearchAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            If mbManualHeightAutoSearchAction = True Then
                                mbManualHeightAutoSearchAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm '移動逾時
                        Else
                            Return enmRunStatus.Running
                        End If
                    End If
                    .SysNum = 9000

                Case 9000
                    sys.Tag = LatchPos '將位置暫存到tag
                    sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Finish
                    sys.RunStatus = enmRunStatus.Finish
                    If mbManualHeightAutoSearchAction = True Then
                        mbManualHeightAutoSearchAction = False
                        .SelectValve = mSelectValveTemp
                    End If
                    Return enmRunStatus.Finish

            End Select

            Return enmRunStatus.Running
        End With
        'ErrorHandler:

        '        strShowMessage = Err.GetException.StackTrace
        '        frmMsg.TopMost = True
        '        frmMsg.Show()
        '        frmMsg.BringToFront()
        '        Err.Clear()         '清除錯誤資訊
        '        Return enmRunStatus.Alarm

    End Function

End Class

Imports ProjectCore
Imports ProjectMotion
Imports ProjectIO

Public Class CActionChangeGlue
    ''' <summary>[紀錄檢查Sensor之起始時間]</summary>
    ''' <remarks></remarks>
    Dim mCheckSensorStart As Decimal

    Dim PosX(enmStage.Max) As Decimal       '[用來紀錄Purge前馬達之座標] '分Stage暫存 避免同時使用被覆蓋
    Dim PosY(enmStage.Max) As Decimal       '[用來紀錄Purge前馬達之座標]
    Dim PosZ(enmStage.Max) As Decimal       '[用來紀錄Purge前馬達之座標]
    Dim mChangeGlueStopWatch(enmStage.Max) As Stopwatch
    Dim inpX(enmStage.Max) As CommandStatus
    Dim inpY(enmStage.Max) As CommandStatus
    Dim iStageNo(enmStage.Max) As Integer

    'jimmy 20170822 手動觸發ChangeGlue
    Private mbManualChangeGlueAction As Boolean
    Public Property _bManualChangeGlueAction() As Boolean
        Get
            Return mbManualChangeGlueAction
        End Get
        Set(ByVal value As Boolean)
            mbManualChangeGlueAction = value
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

    Sub New()
        For mStageNo As Integer = 0 To mChangeGlueStopWatch.Count - 1
            mChangeGlueStopWatch(mStageNo) = New Stopwatch
        Next

    End Sub
    'jimmy 20170822
    Private mSelectValveTemp As eValveWorkMode
    Public Sub SetTempValveMode(ByVal sys As sSysParam)
        mSelectValveTemp = sys.SelectValve
    End Sub

    ''' <summary>
    ''' ChangeGlue動作流程
    ''' </summary>
    ''' <remarks></remarks>

    Sub Run(ByRef Sys As sSysParam)

        'Static iStageStart As Integer
        ' Static iStageEnd As Integer
        Dim mAxisBState(enmStage.Max) As CommandStatus
        'On Error GoTo ErrorHandler

        If mbManualChangeGlueAction = True Then
            Sys.SelectValve = mManualSelectValve
        End If


        With Sys

            Select Case .SysNum
                Case sSysParam.SysLoopStart '設定同步作動平台索引

                    Select Case gSSystemParameter.MachineType

                        Case enmMachineType.DCSW_800AQ
                            Select Case Sys.StageNo
                                'Case enmStage.No1, enmStage.No2
                                '    iStageStart = eSys.DispStage1
                                '    iStageEnd = eSys.DispStage2

                                'Case enmStage.No3, enmStage.No4
                                '    iStageStart = eSys.DispStage3
                                '    iStageEnd = eSys.DispStage4
                                Case enmStage.No1
                                    iStageNo(Sys.StageNo) = eSys.DispStage1
                                    '    iStageEnd = enmStage.No1
                                Case enmStage.No2
                                    iStageNo(Sys.StageNo) = eSys.DispStage2
                                    '    iStageStart = enmStage.No2
                                    '    iStageEnd = enmStage.No2
                                Case enmStage.No3
                                    iStageNo(Sys.StageNo) = eSys.DispStage3
                                    '    iStageStart = enmStage.No3
                                    '    iStageEnd = enmStage.No3
                                Case enmStage.No4
                                    iStageNo(Sys.StageNo) = eSys.DispStage4
                                    '    iStageStart = enmStage.No4
                                    '    iStageEnd = enmStage.No1


                                    '    End Select

                                    'Case enmMachineType.eDTS_2S2V
                                    '    iStageStart = eSys.DispStage1
                                    '    iStageEnd = eSys.DispStage2

                                    'Case Else
                                    '    iStageStart = eSys.DispStage1
                                    '    iStageEnd = eSys.DispStage1

                            End Select
                        Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                            Select Case Sys.StageNo
                                Case enmStage.No1
                                    iStageNo(Sys.StageNo) = eSys.DispStage1
                                Case enmStage.No2
                                    iStageNo(Sys.StageNo) = eSys.DispStage2
                            End Select
                        Case Else
                            iStageNo(Sys.StageNo) = eSys.DispStage1
                    End Select
                    .SysNum = 1020

                Case 1020 'Stage到位確保
                    'For iSys As Integer = iStageStart To iStageEnd
                    inpX(Sys.StageNo) = gCMotion.MotionDone(gSYS(iStageNo(Sys.StageNo)).AxisX, False)
                    inpY(Sys.StageNo) = gCMotion.MotionDone(gSYS(iStageNo(Sys.StageNo)).AxisY, False)
                    If inpX(Sys.StageNo) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(gSYS(iStageNo(Sys.StageNo)).AxisX) Then
                            '移動逾時
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            .RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            Exit Sub
                        End If
                    End If
                    If inpY(Sys.StageNo) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(gSYS(iStageNo(Sys.StageNo)).AxisY) Then
                            '移動逾時
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            .RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            Exit Sub
                        End If
                    End If
                    'Next
                    .SysNum = 1040

                Case 1040 '設定移動速度
                    'For iSys As Integer = iStageStart To iStageEnd
                    '[說明]:設定移動速度
                    '[說明]:X軸
                    With gCMotion.AxisParameter(gSYS(iStageNo(Sys.StageNo)).AxisX).Velocity
                        If gCMotion.SetVelLow(gSYS(iStageNo(Sys.StageNo)).AxisX, .VelLow) <> CommandStatus.Sucessed Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            Sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                        If gCMotion.SetVelHigh(gSYS(iStageNo(Sys.StageNo)).AxisX, gSSystemParameter.ManualVelHigh) <> CommandStatus.Sucessed Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            Sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                        If gCMotion.SetAcc(gSYS(iStageNo(Sys.StageNo)).AxisX, .Acc * .AccRatio) <> CommandStatus.Sucessed Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            Sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                        If gCMotion.SetDec(gSYS(iStageNo(Sys.StageNo)).AxisX, .Dec * .DecRatio) <> CommandStatus.Sucessed Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            Sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                    End With

                    '[說明]:Y軸
                    With gCMotion.AxisParameter(gSYS(iStageNo(Sys.StageNo)).AxisY).Velocity

                        If gCMotion.SetVelLow(gSYS(iStageNo(Sys.StageNo)).AxisY, .VelLow) <> CommandStatus.Sucessed Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            Sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                        If gCMotion.SetVelHigh(gSYS(iStageNo(Sys.StageNo)).AxisY, gSSystemParameter.ManualVelHigh) <> CommandStatus.Sucessed Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            Sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                        If gCMotion.SetAcc(gSYS(iStageNo(Sys.StageNo)).AxisY, .Acc * .AccRatio) <> CommandStatus.Sucessed Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            Sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                        If gCMotion.SetDec(gSYS(iStageNo(Sys.StageNo)).AxisY, .Dec * .DecRatio) <> CommandStatus.Sucessed Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            Sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                    End With

                    '[說明]:Z軸
                    With gCMotion.AxisParameter(gSYS(iStageNo(Sys.StageNo)).AxisZ).Velocity

                        If gCMotion.SetVelLow(gSYS(iStageNo(Sys.StageNo)).AxisZ, .VelLow) <> CommandStatus.Sucessed Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            Sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                        If gCMotion.SetVelHigh(gSYS(iStageNo(Sys.StageNo)).AxisZ, gSSystemParameter.ManualVelHigh) <> CommandStatus.Sucessed Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            Sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                        If gCMotion.SetAcc(gSYS(iStageNo(Sys.StageNo)).AxisZ, .Acc * .AccRatio) <> CommandStatus.Sucessed Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            Sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                        If gCMotion.SetDec(gSYS(iStageNo(Sys.StageNo)).AxisZ, .Dec * .DecRatio) <> CommandStatus.Sucessed Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            Sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                    End With

                    'Next

                    .SysNum = 1100

                Case 1100 '紀錄當下的馬達座標
                    'If gblnChangeGlueComeBack = True Then
                    '[說明]:紀錄當下的馬達座標 & 關閉出膠 & 氣缸缸上升
                    '[說明]:紀錄當下的馬達座標

                    '  For iSys As Integer = iStageStart To iStageEnd
                    PosX(gSYS(iStageNo(Sys.StageNo)).StageNo) = CDbl(gCMotion.GetPositionValue(gSYS(iStageNo(Sys.StageNo)).AxisX))
                    PosY(gSYS(iStageNo(Sys.StageNo)).StageNo) = CDbl(gCMotion.GetPositionValue(gSYS(iStageNo(Sys.StageNo)).AxisY))
                    PosZ(gSYS(iStageNo(Sys.StageNo)).StageNo) = CDbl(gCMotion.GetPositionValue(gSYS(iStageNo(Sys.StageNo)).AxisZ))
                    ' Next
                    Call SetDispensingTrigger(Sys.StageNo, Sys.SelectValve, enmONOFF.eOff)
                    Call ValveCylinderAction(Sys.StageNo, eValveWorkMode.Valve2, enmUpDown.Up, mChangeGlueStopWatch(.StageNo)) 'Soni / 2017.05.05 應強制第二閥上升
                    mCheckSensorStart = mChangeGlueStopWatch(.StageNo).ElapsedMilliseconds
                    .SysNum = 1150
                Case 1150
                    '[說明]:移動Z軸至安全移動位置 ValveWeightSafePosZ
                    If gCMotion.AbsMove(.AxisZ, gSSystemParameter.Pos.WeightCalibration(.StageNo).SafePosZ(.SelectValve)) <> CommandStatus.Sucessed Then
                        If mbManualChangeGlueAction = True Then
                            mbManualChangeGlueAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Select Case .StageNo
                            Case enmStage.No1
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Sys.RunStatus = enmRunStatus.Alarm
                            Case enmStage.No2
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Sys.RunStatus = enmRunStatus.Alarm
                            Case enmStage.No3
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Sys.RunStatus = enmRunStatus.Alarm
                            Case enmStage.No4
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Sys.RunStatus = enmRunStatus.Alarm
                            Case Else
                                'Not Define Stage Error
                                .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                Sys.RunStatus = enmRunStatus.Alarm
                        End Select
                    End If
                    .SysNum = 1160

                Case 1160
                    '[說明]:等待Z軸到位  
                    If gCMotion.MotionDone(.AxisZ) <> CommandStatus.Sucessed Then

                        If gCMotion.IsMoveTimeOut(.AxisZ) = True Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Select Case .StageNo
                                Case enmStage.No1
                                    gEqpMsg.AddHistoryAlarm("Error_1032004", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                                    .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                    Sys.RunStatus = enmRunStatus.Alarm
                                Case enmStage.No2
                                    gEqpMsg.AddHistoryAlarm("Error_1044004", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                                    .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                    Sys.RunStatus = enmRunStatus.Alarm
                                Case enmStage.No3
                                    gEqpMsg.AddHistoryAlarm("Error_1062004", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                                    .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                    Sys.RunStatus = enmRunStatus.Alarm
                                Case enmStage.No4
                                    gEqpMsg.AddHistoryAlarm("Error_1069004", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                                    .Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm
                                    Sys.RunStatus = enmRunStatus.Alarm
                                Case Else
                                    Sys.RunStatus = enmRunStatus.Alarm
                            End Select
                        End If

                        Sys.RunStatus = enmRunStatus.Running
                    End If

                    '沒有AxisB
                    If .AxisB <> -1 Then

                        .SysNum = 1170
                    Else

                        .SysNum = 1200
                    End If

                Case 1170

                    If gCMotion.AbsMove(.AxisB, GetSysParamTilePos(Sys)) <> CommandStatus.Sucessed Then
                        If mbManualChangeGlueAction = True Then
                            mbManualChangeGlueAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        Select Case .StageNo
                            Case enmStage.No1
                                gEqpMsg.AddHistoryAlarm("Error_1034000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1034000), eMessageLevel.Error)
                            Case enmStage.No2
                                gEqpMsg.AddHistoryAlarm("Error_1046000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1046000), eMessageLevel.Error)
                            Case enmStage.No3
                                gEqpMsg.AddHistoryAlarm("Error_1064000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1064000), eMessageLevel.Error)
                            Case enmStage.No4
                                gEqpMsg.AddHistoryAlarm("Error_1071000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1071000), eMessageLevel.Error)
                        End Select
                        .Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        .RunStatus = enmRunStatus.Alarm

                    End If
                    .SysNum = 1180

                Case 1180

                    mAxisBState(Sys.StageNo) = gCMotion.MotionDone(Sys.AxisB)
                    If mAxisBState(Sys.StageNo) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(Sys.AxisB) Then
                            Select Case Sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1034004", "ChangeGlueAction", Sys.SysNum, gMsgHandler.GetMessage(Error_1034004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1046004", "ChangeGlueAction", Sys.SysNum, gMsgHandler.GetMessage(Error_1046004), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1064004", "ChangeGlueAction", Sys.SysNum, gMsgHandler.GetMessage(Error_1064004), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1071004", "ChangeGlueAction", Sys.SysNum, gMsgHandler.GetMessage(Error_1071004), eMessageLevel.Error)
                            End Select
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            Sys.RunStatus = enmRunStatus.Alarm

                        End If
                    Else
                        Sys.SysNum = 1200
                    End If
                Case 1200
                    '[說明]:移動Z軸至Up位置
                    ' For iSys As Integer = iStageStart To iStageEnd
                    If gCMotion.AbsMove(gSYS(iStageNo(Sys.StageNo)).AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                        Select Case iStageNo(Sys.StageNo)
                            Case eSys.DispStage1
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                            Case eSys.DispStage2
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                            Case eSys.DispStage3
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                            Case eSys.DispStage4
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                        End Select
                        Sys.Act(eAct.ChangeGlue).RunStatus = enmRunStatus.Alarm
                        If mbManualChangeGlueAction = True Then
                            mbManualChangeGlueAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        .RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    'Next
                    .SysNum = 1300

                Case 1300
                    '[說明]:Check Stop & Cylinder Up Sensor
                    ' For iSys As Integer = iStageStart To iStageEnd
                    If gCMotion.MotionDone(gSYS(iStageNo(Sys.StageNo)).AxisZ) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(gSYS(iStageNo(Sys.StageNo)).AxisZ) Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                .SelectValve = mSelectValveTemp
                            End If
                            .RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            Exit Sub
                        End If
                    End If
                    ' Next

                    If IsTimeOut(mChangeGlueStopWatch(.StageNo), mCheckSensorStart, gSSystemParameter.StableTime.CheckSensorTimeout) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2004000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Alarm_2004000), eMessageLevel.Alarm)
                        Call mChangeGlueStopWatch(.StageNo).Stop()
                        Sys.Act(eAct.ChangeGlue).RunStatus = enmRunStatus.Alarm
                        If mbManualChangeGlueAction = True Then
                            mbManualChangeGlueAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        .RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        If ValveCylinderSensor(Sys.StageNo, eValveWorkMode.Valve2, enmUpDown.Up) = True Then 'Soni / 2017.05.05 修正enm使用錯誤
                            Call mChangeGlueStopWatch(.StageNo).Stop()
                            .SysNum = 1400
                        End If
                    End If

                Case 1400
                    '[說明]:移動到Change Glue之位置
                    '  For iSys As Integer = iStageStart To iStageEnd
                    If gCMotion.AbsMove(gSYS(iStageNo(Sys.StageNo)).AxisX, gSSystemParameter.Pos.ChangePotCalibration(gSYS(iStageNo(Sys.StageNo)).StageNo).PosX(gSYS(iStageNo(Sys.StageNo)).SelectValve)) <> CommandStatus.Sucessed Then
                        Select Case iStageNo(Sys.StageNo)
                            Case eSys.DispStage1
                                gEqpMsg.AddHistoryAlarm("Error_1030000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                            Case eSys.DispStage2
                                gEqpMsg.AddHistoryAlarm("Error_1042000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                            Case eSys.DispStage3
                                gEqpMsg.AddHistoryAlarm("Error_1060000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                            Case eSys.DispStage4
                                gEqpMsg.AddHistoryAlarm("Error_1067000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1030000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error) 'X軸命令執行失敗
                        Sys.Act(eAct.ChangeGlue).RunStatus = enmRunStatus.Alarm
                        If mbManualChangeGlueAction = True Then
                            mbManualChangeGlueAction = False
                            .SelectValve = mSelectValveTemp
                        End If
                        .RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    If gCMotion.AbsMove(gSYS(iStageNo(Sys.StageNo)).AxisY, gSSystemParameter.Pos.ChangePotCalibration(gSYS(iStageNo(Sys.StageNo)).StageNo).PosY(gSYS(iStageNo(Sys.StageNo)).SelectValve)) <> CommandStatus.Sucessed Then
                        Select Case iStageNo(Sys.StageNo)
                            Case eSys.DispStage1
                                gEqpMsg.AddHistoryAlarm("Error_1031000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                            Case eSys.DispStage2
                                gEqpMsg.AddHistoryAlarm("Error_1043000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                            Case eSys.DispStage3
                                gEqpMsg.AddHistoryAlarm("Error_1061000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                            Case eSys.DispStage4
                                gEqpMsg.AddHistoryAlarm("Error_1068000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1031000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error) 'Y軸命令執行失敗
                        Sys.Act(eAct.ChangeGlue).RunStatus = enmRunStatus.Alarm
                        If mbManualChangeGlueAction = True Then
                            mbManualChangeGlueAction = False
                            .SelectValve = mSelectValveTemp
                        End If

                        .RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    'Next
                    .SysNum = 1500

                Case 1500
                    '[說明]:檢查停止 & 開Pump
                    '  For iSys As Integer = iStageStart To iStageEnd
                    inpX(Sys.StageNo) = gCMotion.MotionDone(gSYS(iStageNo(Sys.StageNo)).AxisX)
                    inpY(Sys.StageNo) = gCMotion.MotionDone(gSYS(iStageNo(Sys.StageNo)).AxisY)
                    If inpX(Sys.StageNo) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(gSYS(iStageNo(Sys.StageNo)).AxisX) Then
                            '移動逾時
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            .RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            Exit Sub
                        End If
                    End If
                    If inpY(Sys.StageNo) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(gSYS(iStageNo(Sys.StageNo)).AxisY) Then
                            '移動逾時
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            .RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            Exit Sub
                        End If
                    End If

                    '   Next
                    .SysNum = 1600


                Case 1600
                    '[說明]:Z軸移至Change Glue高度
                    '       For iSys As Integer = iStageStart To iStageEnd
                    If gCMotion.AbsMove(gSYS(iStageNo(Sys.StageNo)).AxisZ, gSSystemParameter.Pos.ChangePotCalibration(gSYS(iStageNo(Sys.StageNo)).StageNo).PosZ(gSYS(iStageNo(Sys.StageNo)).SelectValve)) <> CommandStatus.Sucessed Then
                        Select Case iStageNo(Sys.StageNo)
                            Case eSys.DispStage1
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                            Case eSys.DispStage2
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                            Case eSys.DispStage3
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                            Case eSys.DispStage4
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error) 'Z軸命令執行失敗
                        Sys.Act(eAct.ChangeGlue).RunStatus = enmRunStatus.Alarm
                        If mbManualChangeGlueAction = True Then
                            mbManualChangeGlueAction = False
                            Sys.SelectValve = mSelectValveTemp
                        End If
                        .RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    '   Next

                    .SysNum = 1700

                Case 1700
                    '[說明]:檢查停止
                    '          For iSys As Integer = iStageStart To iStageEnd
                    If gCMotion.MotionDone(gSYS(iStageNo(Sys.StageNo)).AxisZ, False) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(gSYS(iStageNo(Sys.StageNo)).AxisZ) Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            .RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            Exit Sub
                        End If
                    End If
                    '       Next

                    '[說明]:還沒回來
                    'gblnChangeGlueComeBack = False
                    .SysNum = 9100 '9000 沒到Finish接不回去

                    '*****************************************************************************************
                    '*********************************移回Change Glue 前位置**********************************
                    '*****************************************************************************************
                Case 2000
                    ' For iSys As Integer = iStageStart To iStageEnd
                    '[說明]:移回Change Glue 前位置
                    '[說明]:移動Z軸至Up位置
                    If gCMotion.AbsMove(gSYS(iStageNo(Sys.StageNo)).AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                        Select Case iStageNo(Sys.StageNo)
                            Case eSys.DispStage1
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                            Case eSys.DispStage2
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                            Case eSys.DispStage3
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                            Case eSys.DispStage4
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error) 'Z軸命令執行失敗
                        Sys.Act(eAct.ChangeGlue).RunStatus = enmRunStatus.Alarm
                        If mbManualChangeGlueAction = True Then
                            mbManualChangeGlueAction = False
                            Sys.SelectValve = mSelectValveTemp
                        End If
                        .RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    '  Next

                    .SysNum = 2100

                Case 2100
                    'For iSys As Integer = iStageStart To iStageEnd
                    If gCMotion.MotionDone(gSYS(iStageNo(Sys.StageNo)).AxisZ, False) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(gSYS(iStageNo(Sys.StageNo)).AxisZ) Then
                            '移動逾時
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            .RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            Exit Sub
                        End If
                    End If
                    ' Next

                    .SysNum = 2200

                Case 2200
                    '[說明]:移回Change Glue 前位置
                    '  For iSys As Integer = iStageStart To iStageEnd
                    If gCMotion.AbsMove(gSYS(iStageNo(Sys.StageNo)).AxisX, PosX(gSYS(iStageNo(Sys.StageNo)).StageNo)) <> CommandStatus.Sucessed Then
                        Select Case iStageNo(Sys.StageNo)
                            Case eSys.DispStage1
                                gEqpMsg.AddHistoryAlarm("Error_1030000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                            Case eSys.DispStage2
                                gEqpMsg.AddHistoryAlarm("Error_1042000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                            Case eSys.DispStage3
                                gEqpMsg.AddHistoryAlarm("Error_1060000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                            Case eSys.DispStage4
                                gEqpMsg.AddHistoryAlarm("Error_1067000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1030000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error) 'X軸命令執行失敗
                        Sys.Act(eAct.ChangeGlue).RunStatus = enmRunStatus.Alarm
                        If mbManualChangeGlueAction = True Then
                            mbManualChangeGlueAction = False
                            Sys.SelectValve = mSelectValveTemp
                        End If
                        .RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    If gCMotion.AbsMove(gSYS(iStageNo(Sys.StageNo)).AxisY, PosY(gSYS(iStageNo(Sys.StageNo)).StageNo)) <> CommandStatus.Sucessed Then
                        Select Case iStageNo(Sys.StageNo)
                            Case eSys.DispStage1
                                gEqpMsg.AddHistoryAlarm("Error_1031000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                            Case eSys.DispStage2
                                gEqpMsg.AddHistoryAlarm("Error_1043000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                            Case eSys.DispStage3
                                gEqpMsg.AddHistoryAlarm("Error_1061000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                            Case eSys.DispStage4
                                gEqpMsg.AddHistoryAlarm("Error_1068000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1031000", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error) 'Y軸命令執行失敗
                        Sys.Act(eAct.ChangeGlue).RunStatus = enmRunStatus.Alarm
                        If mbManualChangeGlueAction = True Then
                            mbManualChangeGlueAction = False
                            Sys.SelectValve = mSelectValveTemp
                        End If
                        .RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    ' Next
                    .SysNum = 2300

                Case 2300
                    '         For iSys As Integer = iStageStart To iStageEnd
                    inpX(Sys.StageNo) = gCMotion.MotionDone(gSYS(iStageNo(Sys.StageNo)).AxisX, False)
                    inpY(Sys.StageNo) = gCMotion.MotionDone(gSYS(iStageNo(Sys.StageNo)).AxisY, False)
                    If inpX(Sys.StageNo) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(gSYS(iStageNo(Sys.StageNo)).AxisX) Then
                            Select Case iStageNo(Sys.StageNo)
                                Case eSys.DispStage1
                                    gEqpMsg.AddHistoryAlarm("Error_1030004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                                Case eSys.DispStage2
                                    gEqpMsg.AddHistoryAlarm("Error_1042004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                                Case eSys.DispStage3
                                    gEqpMsg.AddHistoryAlarm("Error_1060004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                                Case eSys.DispStage4
                                    gEqpMsg.AddHistoryAlarm("Error_1067004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1030004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error) '移動逾時
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            .RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            Exit Sub
                        End If
                    End If
                    If inpY(Sys.StageNo) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(gSYS(iStageNo(Sys.StageNo)).AxisY) Then
                            Select Case iStageNo(Sys.StageNo)
                                Case eSys.DispStage1
                                    gEqpMsg.AddHistoryAlarm("Error_1031004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                                Case eSys.DispStage2
                                    gEqpMsg.AddHistoryAlarm("Error_1043004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                                Case eSys.DispStage3
                                    gEqpMsg.AddHistoryAlarm("Error_1061004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                                Case eSys.DispStage4
                                    gEqpMsg.AddHistoryAlarm("Error_1068004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1031004", "AutoValveCalibrationAction", .SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error) '移動逾時
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            .RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            Exit Sub
                        End If
                    End If
                    .SysNum = 2400

                    '    Next
                Case 2400
                    ' For iSys As Integer = iStageStart To iStageEnd
                    '[說明]:移動Z軸回Change Glue 前位置
                    If gCMotion.AbsMove(gSYS(iStageNo(Sys.StageNo)).AxisZ, PosZ(gSYS(iStageNo(Sys.StageNo)).StageNo)) <> CommandStatus.Sucessed Then
                        Select Case iStageNo(Sys.StageNo)
                            Case eSys.DispStage1
                                gEqpMsg.AddHistoryAlarm("Error_1032000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                            Case eSys.DispStage2
                                gEqpMsg.AddHistoryAlarm("Error_1044000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                            Case eSys.DispStage3
                                gEqpMsg.AddHistoryAlarm("Error_1062000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                            Case eSys.DispStage4
                                gEqpMsg.AddHistoryAlarm("Error_1069000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032000", "ChangeGlueAction", .SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error) 'Z軸命令執行失敗
                        Sys.Act(eAct.ChangeGlue).RunStatus = enmRunStatus.Alarm
                        If mbManualChangeGlueAction = True Then
                            mbManualChangeGlueAction = False
                            Sys.SelectValve = mSelectValveTemp
                        End If
                        .RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                    ' Next
                    .SysNum = 2500

                Case 2500
                    '  For iSys As Integer = iStageStart To iStageEnd
                    '[說明]等待停止
                    If gCMotion.MotionDone(gSYS(iStageNo(Sys.StageNo)).AxisZ) <> CommandStatus.Sucessed Then
                        If gCMotion.IsMoveTimeOut(gSYS(iStageNo(Sys.StageNo)).AxisZ) Then
                            If mbManualChangeGlueAction = True Then
                                mbManualChangeGlueAction = False
                                Sys.SelectValve = mSelectValveTemp
                            End If
                            .RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            Exit Sub
                        End If
                    End If
                    '  Next
                    '[說明]:已經回來了
                    'gblnChangeGlueComeBack = True
                    .SysNum = 9100

                    '20160526
                    'Case 9000
                    '    Sys.Act(eAct.ChangeGlue).RunStatus = enmRunStatus.Waiting
                    '    Sys.RunStatus = enmRunStatus.Waiting
                    '    Return enmRunStatus.Waiting

                Case 9100
                    Sys.Act(eAct.ChangeGlue).RunStatus = enmRunStatus.Finish
                    If mbManualChangeGlueAction = True Then
                        mbManualChangeGlueAction = False
                        Sys.SelectValve = mSelectValveTemp
                    End If
                    Sys.RunStatus = enmRunStatus.Finish
                    Exit Sub

            End Select

        End With

    End Sub
End Class

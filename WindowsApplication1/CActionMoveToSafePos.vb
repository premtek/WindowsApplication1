Imports ProjectMotion
Imports ProjectIO
Imports ProjectCore

''' <summary>
''' [跑至安全位置動作流程]
''' </summary>
''' <remarks></remarks>
Public Class CActionMoveToSafePos
    ''' <summary>
    ''' [X軸的狀態]
    ''' </summary>
    ''' <remarks></remarks>
    Dim mAxisXState(enmStage.Max) As CommandStatus
    ''' <summary>
    ''' [Y軸的狀態]
    ''' </summary>
    ''' <remarks></remarks>
    Dim mAxisYState(enmStage.Max) As CommandStatus
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
    ''' <summary>[跑至安全位置動作流程]</summary>
    ''' <remarks></remarks>
    Sub Run(ByRef sys As sSysParam)

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                sys.SelectValve = eValveWorkMode.Valve1

                '[說明]:速度載入
                If gCMotion.SetVelAccDec(sys.AxisX) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030010))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042010))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060010))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067010))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetVelAccDec(sys.AxisY) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031010))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043010))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061010))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068010))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetVelAccDec(sys.AxisZ) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032010))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044010))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062010))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069010))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetVelAccDec(sys.AxisB) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1034010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034010))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1046010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046010))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1064010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064010))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1071010", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071010))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 2000

            Case 2000
                '[Note]:先將Z軸升至安全位置
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.TiltSafePosZ) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 3000

            Case 3000
                '[Note]:等待Table Stop
                mAxisZState(sys.StageNo) = gCMotion.MotionDone(sys.AxisZ)
                If mAxisZState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                sys.SysNum = 4000

            Case 4000
                '[說明]:載入移動-->移動到起始座標
                If gCMotion.AbsMove(sys.AxisX, gSSystemParameter.Pos.PurgeCalibration(sys.StageNo).ValvePosX(sys.SelectValve)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030000", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042000", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060000", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067000", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.AbsMove(sys.AxisY, gSSystemParameter.Pos.PurgeCalibration(sys.StageNo).ValvePosY(sys.SelectValve)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031000", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043000", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061000", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068000", "DispNonHistoryModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 5000

            Case 5000
                If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) = CommandStatus.Sucessed Then
                    sys.SysNum = 9000
                End If

            Case 9000
                sys.RunStatus = enmRunStatus.Finish
                Exit Sub

                '***********************************************************************************************************************************************************
        End Select

    End Sub


End Class

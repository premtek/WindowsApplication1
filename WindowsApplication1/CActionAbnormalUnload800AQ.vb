Imports ProjectCore
Imports ProjectIO
Imports ProjectMotion
''' <summary>
''' 強制退料動作流程
''' </summary>
''' <remarks></remarks>
Public Class CActionAbnormalUnload800AQ

    Dim mHomeStopWatch(enmStage.Max) As Stopwatch
    Dim mI(enmStage.Max) As Integer

    Sub New()
        For mStageNo As Integer = 0 To mHomeStopWatch.Count - 1
            mHomeStopWatch(mStageNo) = New Stopwatch
        Next
    End Sub
    ''' <summary>[強制退料動作流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub Run(ByRef sys As sSysParam)


        Select Case sys.SysNum
            Case sSysParam.SysLoopStart


                '[Note]:Server On & 關閉出膠 & 氣缸缸上升
                'gblnMotorUnlock = False

                Call gCMotion.Servo(sys.AxisZ, enmONOFF.eON)

                '[Note]:復歸前先讓所有的動作停止
                Call gCMotion.EmgStop(sys.AxisZ)

                '[Note]:清除異常狀態
                Call gCMotion.AxisResetError(sys.AxisZ)

                '[Note]:清除同動內Buffer的資料
                For mI(sys.StageNo) = 0 To gCMotion.SyncParameter.Count - 1
                    Call gCMotion.GpClearMovePath(gCMotion.SyncParameter(mI(sys.StageNo)))
                Next

                '[Note]:Dispenesing Trigger Off
                Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eOff)

                '[說明]:關閉出膠 & 給壓力
                'Call gSysAdapter.SetSyringePressure(sys.StageNo, sys.SelectValve, enmONOFF.eOff)
                sys.SysNum = 2000


            Case 2000
                '[Note]:Z軸先回Home，完成後XY軸再回Home
                If gCMotion.Home(sys.AxisZ) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032016", "DispStage_AbnormalUnloadAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032016), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044016", "DispStage_AbnormalUnloadAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044016), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062016", "DispStage_AbnormalUnloadAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062016), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069016", "DispStage_AbnormalUnloadAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069016), eMessageLevel.Error)
                    End Select
                    'gEqpMsg.AddHistoryAlarm("Error_1032016", "DispStage_AbnormalUnloadAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032016), eMessageLevel.Error)
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                mHomeStopWatch(sys.StageNo).Restart()
                sys.SysNum = 3000

            Case 3000
                '[Note]:確認Z軸復歸完成
                If IsTimeOut(mHomeStopWatch(sys.StageNo), gSSystemParameter.TimeOut5) = True Then
                    mHomeStopWatch(sys.StageNo).Stop()
                    If gCMotion.HomeFinish(sys.AxisZ) <> CommandStatus.Sucessed Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032001", "DispStage_AbnormalUnloadAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032001), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044001", "DispStage_AbnormalUnloadAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044001), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062001", "DispStage_AbnormalUnloadAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062001), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069001", "DispStage_AbnormalUnloadAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069001), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1032001", "DispStage_AbnormalUnloadAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032001), eMessageLevel.Error)
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                Else
                    If gCMotion.HomeFinish(sys.AxisZ) = CommandStatus.Sucessed Then
                        mHomeStopWatch(sys.StageNo).Stop()
                        sys.SysNum = 9000
                    End If
                End If

            Case 9000
                sys.RunStatus = enmRunStatus.Finish
                Exit Sub

        End Select

    End Sub

End Class

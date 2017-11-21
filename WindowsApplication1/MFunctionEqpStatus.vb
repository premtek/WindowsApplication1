Imports ProjectIO
Imports ProjectCore
Imports WetcoConveyor
Imports ProjectMotion

''' <summary>設備狀態處理</summary>
''' <remarks></remarks>
Module MFunctionEqpStatus

    ''' <summary>設備狀態列舉</summary>
    ''' <remarks></remarks>
    Public Enum enmEqpStatus
        ''' <summary>需要復歸</summary>
        ''' <remarks></remarks>
        NeedHome = 0
        ''' <summary>復歸中</summary>
        ''' <remarks></remarks>
        Homing = 1
        ''' <summary>復歸完成</summary>
        ''' <remarks></remarks>
        HomeFinish = 2
        ''' <summary>復歸中斷</summary>
        ''' <remarks></remarks>
        HomeStop = 3
        ''' <summary>生產中</summary>
        ''' <remarks></remarks>
        Running = 4
        ''' <summary>生產完成</summary>
        ''' <remarks></remarks>
        RunFinish = 5
        ''' <summary>整機生產暫停</summary>
        ''' <remarks></remarks>
        RunPause = 6
        ''' <summary>生產中斷</summary>
        ''' <remarks></remarks>
        RunStop = 7
        ''' <summary>注意</summary>
        ''' <remarks></remarks>
        Warning = 8
        ''' <summary>異常發生</summary>
        ''' <remarks></remarks>
        Alarm = 9
        ''' <summary>A機暫停</summary>
        ''' <remarks></remarks>
        RunPauseA = 10
        ''' <summary>B機暫停</summary>
        ''' <remarks></remarks>
        RunPauseB = 11
    End Enum
    ''' <summary>設備資訊</summary>
    ''' <remarks></remarks>
    Public Structure sEqpInfo
        ''' <summary>當前狀態</summary>
        ''' <remarks></remarks>
        Public Status As enmEqpStatus
        ''' <summary>標題訊息</summary>
        ''' <remarks></remarks>
        Public Message As String

        ''' <summary>A機硬體Alarm檢查</summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function CheckW800AQHardwareAlarmA() As Boolean
            If enmHardwardAlarm.EMS >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.EMS).Status = True Then

                    '20171114
                    '[說明]:不可執行初始化,要偵測到enmDI.ResetButton 才可改變狀態
                    gSSystemParameter.EMSResetButton = False

                    For i As Integer = eSys.DispStage1 To eSys.DispStage2
                        gCMotion.EmgStop(gSYS(i).AxisX)
                        gCMotion.EmgStop(gSYS(i).AxisY)
                        gCMotion.EmgStop(gSYS(i).AxisZ)
                        gCMotion.EmgStop(gSYS(i).AxisB)
                        gCMotion.EmgStop(gSYS(i).AxisC)
                    Next
                    gCMotion.EmgStop(enmAxis.MachineAChuck1)
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.EMS).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A EMS Warning"
                            SetMachineAGantry() 'A機同動確保
                            SetMachineAStatusAlarm() '設定A機狀態為Alarm
                            Return True
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A EMS Alarm"
                            gEqpMsg.AddHistoryAlarm("Alarm_2081100", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2081100))
                            SetMachineAGantry() 'A機同動確保
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '復歸狀態清除
                            Return True
                            '^^^^^^^
                    End Select
                Else
                    '20171114
                    If gDICollection.GetState(enmDI.ResetButton) = True Then
                        gSSystemParameter.EMSResetButton = True
                    End If
                End If
            End If


            If enmHardwardAlarm.CDA >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.CDA).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.CDA).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A CDA Warning"
                            Return True
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpMsg.AddHistoryAlarm("Alarm_2081101", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2081101)) 'CDA異常!!
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A CDA Alarm"
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '復歸狀態清除
                            Return True
                            '^^^^^^^
                    End Select
                End If
            End If
            If enmHardwardAlarm.DoorClose >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.DoorClose).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.DoorClose).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A Door-Close Warning"
                            Return True
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpMsg.AddHistoryAlarm("Alarm_2081105", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2081105))
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A Door-Close Alarm"
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '復歸狀態清除
                            Return True
                            '^^^^^^^
                    End Select

                End If
            End If
            If enmHardwardAlarm.MC_Motor >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.MC_Motor).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.MC_Motor).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A MC_Motor Warning"
                            Return True
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpMsg.AddHistoryAlarm("Alarm_2081103", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2081103))
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A MC_Motor Alarm"
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '復歸狀態清除
                            Return True
                            '^^^^^^^
                    End Select

                End If
            End If
            If enmHardwardAlarm.MC_Heater >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.MC_Heater).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.MC_Heater).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A MC_Heater Warning"
                            Return True
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpMsg.AddHistoryAlarm("Alarm_2081104", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2081104))
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A MC_Heater Alarm"
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '復歸狀態清除
                            Return True
                            '^^^^^^^
                    End Select

                End If
            End If
            Return False
        End Function
        ''' <summary>B機硬體Alarm檢查</summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function CheckW800AQHardwareAlarmB() As Boolean
            If enmHardwardAlarm.EMS2 >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.EMS2).Status = True Then
                    For i As Integer = eSys.DispStage3 To eSys.DispStage4
                        gCMotion.EmgStop(gSYS(i).AxisX)
                        gCMotion.EmgStop(gSYS(i).AxisY)
                        gCMotion.EmgStop(gSYS(i).AxisZ)
                        gCMotion.EmgStop(gSYS(i).AxisB)
                        gCMotion.EmgStop(gSYS(i).AxisC)
                    Next
                    gCMotion.EmgStop(enmAxis.MachineBChuck1)
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.EMS2).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-B EMS Warning"
                            SetMachineBGantry() 'B機同動確保
                            Return True
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-B EMS Alarm"
                            gEqpMsg.AddHistoryAlarm("Alarm_2082100", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2082100))
                            SetMachineBGantry() 'B機同動確保
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '復歸狀態清除
                            Return True
                            '^^^^^^^
                    End Select
                End If
            End If
            If enmHardwardAlarm.CDA2 >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.CDA2).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.CDA2).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-B CDA Warning"
                            Return True
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpMsg.AddHistoryAlarm("Alarm_2082101", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2082101)) 'CDA異常!!
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-B CDA Alarm"
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '復歸狀態清除
                            Return True
                            '^^^^^^^
                    End Select
                End If
            End If
            If enmHardwardAlarm.DoorClose2 >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.DoorClose2).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.DoorClose2).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-B Door-Close Warning"
                            Return True
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpMsg.AddHistoryAlarm("Alarm_2082102", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2082102)) '
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-B Door-Close Alarm"
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '復歸狀態清除
                            Return True
                            '^^^^^^^
                    End Select
                End If
            End If
            If enmHardwardAlarm.MC_Motor2 >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.MC_Motor2).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.MC_Motor2).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-B MC_Motor Warning"
                            Return True
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpMsg.AddHistoryAlarm("Alarm_2082103", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2082103))
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-B MC_Motor Alarm"
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '復歸狀態清除
                            Return True
                            '^^^^^^^
                    End Select
                End If
            End If
            If enmHardwardAlarm.MC_Heater2 >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.MC_Heater2).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.MC_Heater2).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-B MC_Heater Warning"
                            Return True
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpMsg.AddHistoryAlarm("Alarm_2082104", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2082104))
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-B MC_Heater Alarm"
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '復歸狀態清除
                            Return True
                            '^^^^^^^
                    End Select
                End If
            End If
            Return False
        End Function
        ''' <summary>Conveyor硬體檢查</summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function CheckW800AQHardwareAlarmC() As Boolean

            '=== 硬體保護, 加熱器異常 ===
            If gDICollection.GetState(enmDI.HeaterAlarm1) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Heater1 Alarm"
                Return True
                '^^^^^^^
            End If

            If gDICollection.GetState(enmDI.HeaterAlarm2) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Heater2 Alarm"
                Return True
                '^^^^^^^
            End If

            If gDICollection.GetState(enmDI.HeaterAlarm3) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Heater3 Alarm"
                Return True
                '^^^^^^^
            End If

            If gDICollection.GetState(enmDI.HeaterAlarm4) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Heater4 Alarm"
                Return True
                '^^^^^^^
            End If

            If gDICollection.GetState(enmDI.HeaterAlarm5) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Heater5 Alarm"
                Return True
                '^^^^^^^
            End If

            If gDICollection.GetState(enmDI.HeaterAlarm6) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Heater6 Alarm"
                Return True
                '^^^^^^^
            End If

            If gDICollection.GetState(enmDI.HeaterAlarm7) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Heater1 Alarm"
                Return True
                '^^^^^^^
            End If

            If gDICollection.GetState(enmDI.HeaterAlarm8) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Heater2 Alarm"
                Return True
                '^^^^^^^
            End If

            If gDICollection.GetState(enmDI.HeaterAlarm9) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Heater3 Alarm"
                Return True
                '^^^^^^^
            End If

            If gDICollection.GetState(enmDI.HeaterAlarm10) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Heater4 Alarm"
                Return True
                '^^^^^^^
            End If

            If gDICollection.GetState(enmDI.HeaterAlarm11) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Heater5 Alarm"
                Return True
                '^^^^^^^
            End If

            If gDICollection.GetState(enmDI.HeaterAlarm12) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Heater6 Alarm"
                Return True
                '^^^^^^^
            End If
            '=== 硬體保護, 加熱器異常 ===

            '=== 硬體保護, 過熱保護 ===
            If gDICollection.GetState(enmDI.OverTemperature) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Heater1 Over-Heat"
                Return True
                '^^^^^^^
            End If
            If gDICollection.GetState(enmDI.OverTemperature2) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Heater2 Over-Heat"
                Return True
                '^^^^^^^
            End If
            If gDICollection.GetState(enmDI.OverTemperature3) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Heater3 Over-Heat"
                Return True
                '^^^^^^^
            End If
            If gDICollection.GetState(enmDI.OverTemperature4) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Heater4 Over-Heat"
                Return True
                '^^^^^^^
            End If
            If gDICollection.GetState(enmDI.OverTemperature5) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Heater5 Over-Heat"
                Return True
                '^^^^^^^
            End If
            If gDICollection.GetState(enmDI.OverTemperature6) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Heater6 Over-Heat"
                Return True
                '^^^^^^^
            End If

            If gDICollection.GetState(enmDI.OverTemperature7) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Heater1 Over-Heat"
                Return True
                '^^^^^^^
            End If
            If gDICollection.GetState(enmDI.OverTemperature8) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Heater2 Over-Heat"
                Return True
                '^^^^^^^
            End If
            If gDICollection.GetState(enmDI.OverTemperature9) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Heater3 Over-Heat"
                Return True
                '^^^^^^^
            End If
            If gDICollection.GetState(enmDI.OverTemperature10) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Heater4 Over-Heat"
                Return True
                '^^^^^^^
            End If
            If gDICollection.GetState(enmDI.OverTemperature11) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Heater5 Over-Heat"
                Return True
                '^^^^^^^
            End If
            If gDICollection.GetState(enmDI.OverTemperature12) = True Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Heater6 Over-Heat"
                Return True
                '^^^^^^^
            End If
            '=== 硬體保護, 過熱保護 ===

            'hot plate overheat : WetcoConveyor.mGlobalPool.Overheat = true
            If (WetcoConveyor.mGlobalPool.Overheat) Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Conveyor OvertHeat Alarm"
                Return True
                '^^^^^^^
            End If

            If Unit IsNot Nothing Then
                If Unit.A_Roller IsNot Nothing Then
                    If Unit.A_Roller.Alarm Then
                        gEqpInfo.Status = enmEqpStatus.Alarm
                        gEqpInfo.Message = "Machine-A Conveyor Roller Alarm"
                        Return True
                        '^^^^^^^
                    End If
                End If
                If Unit.B_Roller IsNot Nothing Then
                    If Unit.B_Roller.Alarm Then
                        gEqpInfo.Status = enmEqpStatus.Alarm
                        gEqpInfo.Message = "Machine-B Conveyor Roller Alarm"
                        Return True
                        '^^^^^^^
                    End If
                End If
            End If
           
            Return False
        End Function

        ''' <summary>取得DCSW-800AQ設備狀態</summary>
        ''' <remarks>判斷狀態需放在主畫面, 以確保即時攔截,避免條件混亂. 介面更新放在生產畫面, 以確保顯示正常. </remarks>
        Public Sub GetW800AQEqpStatus()
            '硬體優先檢查, 最後才是軟體

            If enmHardwardAlarm.EMO >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.EMO).Status = True Then


                    For i As Integer = 0 To enmAxis.Max
                        If gCMotion.AxisParameter(i).AxisName <> "" Then
                            gCMotion.EmgStop(i)
                        End If
                    Next
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.EMO).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "EMO Warning"
                            SetMachineAGantry() 'A機同動確保
                            SetMachineBGantry() 'B機同動確保

                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "EMO Alarm"
                            gEqpMsg.AddHistoryAlarm("Alarm_2000001", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000001))
                            SetMachineAGantry() 'A機同動確保
                            SetMachineBGantry() 'B機同動確保
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '復歸狀態清除
                            Exit Sub
                            '^^^^^^^
                    End Select
                End If
            End If

            CheckW800AQHardwareAlarmA()
            CheckW800AQHardwareAlarmB()
            CheckW800AQHardwareAlarmC()
           
            If (gDICollection.GetState(enmDI.PrevAlarm) Or gDICollection.GetState(enmDI.NextAlarm)) Then
                gEqpMsg.AddHistoryAlarm("Alarm_2083130", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2083130))
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Prev/Next Alarm"
                Exit Sub
                '^^^^^^^
            End If
            If (gDICollection.GetState(enmDI.PrevAlarm2) Or gDICollection.GetState(enmDI.NextAlarm2)) Then
                gEqpMsg.AddHistoryAlarm("Alarm_2084130", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2084130))
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Prev/Next Alarm"
                Exit Sub
                '^^^^^^^
            End If
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '很重要要說三次

            If gEqpMsg.MsgLevel = eMessageLevel.Alarm Or gEqpMsg.MsgLevel = eMessageLevel.Error Then '如果系統本次拋回含有異常, 則標題顯示為紅色Alarm Soni / 2016.09.09 搭配燈號 錯誤訊息等級擴充
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Alarm"
                Exit Sub
                '^^^^^^^
            End If
            If gEqpMsg.MsgLevel = eMessageLevel.Warning Then '如果系統本次拋回含有警告, 則標題顯是為黃色Warning Soni / 2016.09.09 搭配燈號 錯誤訊息等級擴充
                gEqpInfo.Status = enmEqpStatus.Warning
                gEqpInfo.Message = "Warning"
                Exit Sub
                '^^^^^^^
            End If

            '由最底層先判斷, 逐層往上
            For mStageNo As Integer = eSys.DispStage1 To eSys.DispStage4
                If Not gSYS(mStageNo) Is Nothing Then
                    Select Case gSYS(mStageNo).RunStatus
                        Case enmRunStatus.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Stage Alarm"
                            Exit Sub
                            '^^^^^^^
                    End Select
                End If
            Next
            '系統無異常狀況下才會顯示正常訊息
            'Select Case gEqpInfo.RunMode
            '    Case enmMachineRunMode.AutoRun '整機
            Select Case gSYS(eSys.OverAll).ExecuteCommand
                Case eSysCommand.Home '復歸命令
                    If gblnUpdateInitial = True Then '復歸動作中才會更新
                        If gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineA).RunStatus = enmRunStatus.Alarm Then '整機復歸,A機復歸異常
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine A Initial Alarm"
                            gDOCollection.SetState(enmDO.DoorLock, False) '異常可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.MachineB).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineB).RunStatus = enmRunStatus.Alarm Then '整機復歸,B機復歸異常
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine B Initial Alarm"
                            gDOCollection.SetState(enmDO.DoorLock2, False) '異常可開門
                            Exit Sub
                            '^^^^^^^
                        End If

                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Alarm Then '整機復歸異常
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "System Initial Alarm"
                            gDOCollection.SetState(enmDO.DoorLock, False) '異常可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '異常可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Stop Then '整機暫停/停止
                            gEqpInfo.Status = enmEqpStatus.HomeStop
                            gEqpInfo.Message = "System has Stopped"
                            gDOCollection.SetState(enmDO.DoorLock, False) '停止可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '停止可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Finish Then '整機復歸完成
                            gEqpInfo.Status = enmEqpStatus.HomeFinish
                            If gEqpInfo.Message <> "System Initial Finish" Then
                                gDOCollection.SetState(enmDO.StartButtonLight, True) 'Perry要求, 復歸完提示使用者可按開始
                                gDOCollection.SetState(enmDO.PauseButtonLight, False) 'Perry要求, 復歸完提示使用者不可按結束
                                gDOCollection.SetState(enmDO.StartButtonLight2, True) 'Perry要求, 復歸完提示使用者可按開始
                                gDOCollection.SetState(enmDO.PauseButtonLight2, False) 'Perry要求, 復歸完提示使用者不可按結束
                                gEqpInfo.Message = "System Initial Finish"
                            End If
                            gDOCollection.SetState(enmDO.DoorLock, False) '停止可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '停止可開門


                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish Then '整機復歸,A機復歸完成
                            gEqpInfo.Status = enmEqpStatus.Homing
                            gEqpInfo.Message = "Machine A Initial Finish"
                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.MachineB).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineB).RunStatus = enmRunStatus.Finish Then '整機復歸,B機復歸完成
                            gEqpInfo.Status = enmEqpStatus.Homing
                            gEqpInfo.Message = "Machine B Initial Finish"
                            Exit Sub
                            '^^^^^^^
                        End If
                        gEqpInfo.Status = enmEqpStatus.Homing
                        gEqpInfo.Message = "System is initialling.." 'Soni / 2016.09.13 Logan要求
                        Exit Sub
                        '^^^^^^^
                    End If
                Case eSysCommand.AutoRun '生產命令
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None Then '整機未完成復歸
                        gEqpInfo.Status = enmEqpStatus.NeedHome
                        gEqpInfo.Message = "Click Initial"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Stop Then '整機暫停/停止
                        gEqpInfo.Status = enmEqpStatus.RunStop
                        gEqpInfo.Message = "System Run Stopped"
                        gDOCollection.SetState(enmDO.DoorLock, False) '停止後可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '停止後可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Finish Then '整機生產完成
                        gEqpInfo.Status = enmEqpStatus.RunFinish
                        gEqpInfo.Message = "System Run Finish"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).ExternalPause = True And gSYS(eSys.OverAll).IsCanPause Then '整機暫停
                        gEqpInfo.Status = enmEqpStatus.RunPause
                        gEqpInfo.Message = "System is Paused."
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門

                        '=== 所有Stage備份 ===
                        For mSysNo As Integer = eSys.DispStage1 To eSys.DispStage4
                            gSysStagePause(mSysNo).StopPosX = gCMotion.GetPositionValue(gSYS(mSysNo).AxisX)
                            gSysStagePause(mSysNo).StopPosY = gCMotion.GetPositionValue(gSYS(mSysNo).AxisY)
                            gSysStagePause(mSysNo).StopPosZ = gCMotion.GetPositionValue(gSYS(mSysNo).AxisZ)
                            gSysStagePause(mSysNo).StopPosA = gCMotion.GetPositionValue(gSYS(mSysNo).AxisA)
                            gSysStagePause(mSysNo).StopPosB = gCMotion.GetPositionValue(gSYS(mSysNo).AxisB)
                            gSysStagePause(mSysNo).StopPosC = gCMotion.GetPositionValue(gSYS(mSysNo).AxisC)
                            gSysStagePause(mSysNo).ExcuteCommand = gSYS(mSysNo).ExecuteCommand
                            gSysStagePause(mSysNo).SysNum = gSYS(mSysNo).SysNum
                            'gSysStagePause(mSysNo).SyringeAirPressure = gSysAdapter.GetSyringeAirPressure(gSYS(mSysNo).ValveNo(0))
                            gSysStagePause(mSysNo).SyringeAirPressureOn = gSysAdapter.GetSyringeAirPressureState(gSYS(mSysNo).StageNo, gSYS(mSysNo).ValveNo(0))
                        Next
                        '=== 所有Stage備份 ===
                        Exit Sub
                        '^^^^^^^
                    End If

                    'A機暫停
                    If gSYS(eSys.MachineA).ExternalPause = True And gSYS(eSys.MachineA).IsCanPause = True Then
                        gEqpInfo.Status = enmEqpStatus.RunPauseA
                        gEqpInfo.Message = "Machine-A is Paused."
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        '=== 所有Stage備份 ===
                        For mSysNo As Integer = eSys.DispStage1 To eSys.DispStage2
                            gSysStagePause(mSysNo).StopPosX = gCMotion.GetPositionValue(gSYS(mSysNo).AxisX)
                            gSysStagePause(mSysNo).StopPosY = gCMotion.GetPositionValue(gSYS(mSysNo).AxisY)
                            gSysStagePause(mSysNo).StopPosZ = gCMotion.GetPositionValue(gSYS(mSysNo).AxisZ)
                            gSysStagePause(mSysNo).StopPosA = gCMotion.GetPositionValue(gSYS(mSysNo).AxisA)
                            gSysStagePause(mSysNo).StopPosB = gCMotion.GetPositionValue(gSYS(mSysNo).AxisB)
                            gSysStagePause(mSysNo).StopPosC = gCMotion.GetPositionValue(gSYS(mSysNo).AxisC)
                            gSysStagePause(mSysNo).ExcuteCommand = gSYS(mSysNo).ExecuteCommand
                            gSysStagePause(mSysNo).SysNum = gSYS(mSysNo).SysNum
                            'gSysStagePause(mSysNo).SyringeAirPressure = gSysAdapter.GetSyringeAirPressure(gSYS(mSysNo).ValveNo(0))
                            For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                                gSysStagePause(mSysNo).SyringeAirPressureOn = gSysAdapter.GetSyringeAirPressureState(gSYS(mSysNo).StageNo, mValveNo)
                            Next
                        Next
                        '=== 所有Stage備份 ===
                        Exit Sub
                        '^^^^^^^
                    End If

                    'B機暫停
                    If gSYS(eSys.MachineB).ExternalPause = True And gSYS(eSys.MachineB).IsCanPause = True Then
                        gEqpInfo.Status = enmEqpStatus.RunPauseA
                        gEqpInfo.Message = "Machine-B is Paused."
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        '=== 所有Stage備份 ===
                        For mSysNo As Integer = eSys.DispStage3 To eSys.DispStage4
                            gSysStagePause(mSysNo).StopPosX = gCMotion.GetPositionValue(gSYS(mSysNo).AxisX)
                            gSysStagePause(mSysNo).StopPosY = gCMotion.GetPositionValue(gSYS(mSysNo).AxisY)
                            gSysStagePause(mSysNo).StopPosZ = gCMotion.GetPositionValue(gSYS(mSysNo).AxisZ)
                            gSysStagePause(mSysNo).StopPosA = gCMotion.GetPositionValue(gSYS(mSysNo).AxisA)
                            gSysStagePause(mSysNo).StopPosB = gCMotion.GetPositionValue(gSYS(mSysNo).AxisB)
                            gSysStagePause(mSysNo).StopPosC = gCMotion.GetPositionValue(gSYS(mSysNo).AxisC)
                            gSysStagePause(mSysNo).ExcuteCommand = gSYS(mSysNo).ExecuteCommand
                            gSysStagePause(mSysNo).SysNum = gSYS(mSysNo).SysNum
                            'gSysStagePause(mSysNo).SyringeAirPressure = gSysAdapter.GetSyringeAirPressure(gSYS(mSysNo).ValveNo(0))
                            For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                                gSysStagePause(mSysNo).SyringeAirPressureOn = gSysAdapter.GetSyringeAirPressureState(gSYS(mSysNo).StageNo, mValveNo)
                            Next
                        Next
                        '=== 所有Stage備份 ===
                        Exit Sub
                        '^^^^^^^
                    End If

                    gEqpInfo.Status = enmEqpStatus.Running
                    gEqpInfo.Message = "System is Running.."
                    'gDOCollection.GetSetState(enmDO.DoorLock) = True 避免造成暫停後不可開門
                    'gDOCollection.GetSetState(enmDO.DoorLock2) = True
                    Exit Sub
                    '^^^^^^^

                Case Else '其他命令/無命令
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None Then '整機未完成復歸
                        gEqpInfo.Status = enmEqpStatus.NeedHome
                        gEqpInfo.Message = "Click Initial"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

            End Select
        End Sub

        ''' <summary>取得DCS-500AD設備狀態</summary>
        ''' <remarks>判斷狀態需放在主畫面, 以確保即時攔截,避免條件混亂. 介面更新放在生產畫面, 以確保顯示正常. </remarks>
        Public Sub Get500ADEqpStatus()
            '硬體優先檢查, 最後才是軟體

            If enmHardwardAlarm.EMO >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.EMO).Status = True Then

                    '20171114
                    '[說明]:不可執行初始化,要偵測到enmDI.ResetButton 才可改變狀態
                    gSSystemParameter.EMSResetButton = False

                    For i As Integer = 0 To enmAxis.Max
                        If gCMotion.AxisParameter(i).AxisName <> "" Then
                            gCMotion.EmgStop(i)
                        End If
                    Next
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.EMO).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "EMO Warning"
                            SetMachineAGantry() 'A機同動確保
                            SetMachineBGantry() 'B機同動確保

                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "EMO Alarm"
                            gEqpMsg.AddHistoryAlarm("Alarm_2000001", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000001))
                            SetMachineAGantry() 'A機同動確保
                            SetMachineBGantry() 'B機同動確保
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '復歸狀態清除
                            Exit Sub
                            '^^^^^^^
                    End Select
                Else
                    '20171114
                    If gDICollection.GetState(enmDI.ResetButton) = True Then
                        gSSystemParameter.EMSResetButton = True
                    End If
                End If
            End If

            If (CheckW800AQHardwareAlarmA()) Then
                Exit Sub
                '^^^^^^^
            End If

            If (CheckW800AQHardwareAlarmC()) Then
                Exit Sub
                '^^^^^^^
            End If

            If (gDICollection.GetState(enmDI.PrevAlarm) Or gDICollection.GetState(enmDI.NextAlarm)) Then
                gEqpMsg.AddHistoryAlarm("Alarm_2083130", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2083130))
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Prev/Next Alarm"
                Exit Sub
                '^^^^^^^
            End If
            If (gDICollection.GetState(enmDI.PrevAlarm2) Or gDICollection.GetState(enmDI.NextAlarm2)) Then
                gEqpMsg.AddHistoryAlarm("Alarm_2084130", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2084130))
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Prev/Next Alarm"
                Exit Sub
                '^^^^^^^
            End If
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '很重要要說三次

            If gEqpMsg.MsgLevel = eMessageLevel.Alarm Or gEqpMsg.MsgLevel = eMessageLevel.Error Then '如果系統本次拋回含有異常, 則標題顯示為紅色Alarm Soni / 2016.09.09 搭配燈號 錯誤訊息等級擴充
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Alarm"
                Exit Sub
                '^^^^^^^
            End If
            If gEqpMsg.MsgLevel = eMessageLevel.Warning Then '如果系統本次拋回含有警告, 則標題顯是為黃色Warning Soni / 2016.09.09 搭配燈號 錯誤訊息等級擴充
                gEqpInfo.Status = enmEqpStatus.Warning
                gEqpInfo.Message = "Warning"
                Exit Sub
                '^^^^^^^
            End If

            '由最底層先判斷, 逐層往上
            For mStageNo As Integer = eSys.DispStage1 To eSys.DispStage4
                If Not gSYS(mStageNo) Is Nothing Then
                    Select Case gSYS(mStageNo).RunStatus
                        Case enmRunStatus.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Stage Alarm"
                            Exit Sub
                            '^^^^^^^
                    End Select
                End If
            Next
            '系統無異常狀況下才會顯示正常訊息
            'Select Case gEqpInfo.RunMode
            '    Case enmMachineRunMode.AutoRun '整機
            Select Case gSYS(eSys.OverAll).ExecuteCommand
                Case eSysCommand.Home '復歸命令
                    If gblnUpdateInitial = True Then '復歸動作中才會更新
                        If gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineA).RunStatus = enmRunStatus.Alarm Then '整機復歸,A機復歸異常
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine A Initial Alarm"
                            gDOCollection.SetState(enmDO.DoorLock, False) '異常可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        
                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Alarm Then '整機復歸異常
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "System Initial Alarm"
                            gDOCollection.SetState(enmDO.DoorLock, False) '異常可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '異常可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Stop Then '整機暫停/停止
                            gEqpInfo.Status = enmEqpStatus.HomeStop
                            gEqpInfo.Message = "System has Stopped"
                            gDOCollection.SetState(enmDO.DoorLock, False) '停止可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '停止可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Finish Then '整機復歸完成
                            gEqpInfo.Status = enmEqpStatus.HomeFinish
                            If gEqpInfo.Message <> "System Initial Finish" Then
                                gDOCollection.SetState(enmDO.StartButtonLight, True) 'Perry要求, 復歸完提示使用者可按開始
                                gDOCollection.SetState(enmDO.PauseButtonLight, False) 'Perry要求, 復歸完提示使用者不可按結束
                                gDOCollection.SetState(enmDO.StartButtonLight2, True) 'Perry要求, 復歸完提示使用者可按開始
                                gDOCollection.SetState(enmDO.PauseButtonLight2, False) 'Perry要求, 復歸完提示使用者不可按結束
                                gEqpInfo.Message = "System Initial Finish"
                            End If
                            gDOCollection.SetState(enmDO.DoorLock, False) '停止可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '停止可開門


                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish Then '整機復歸,A機復歸完成
                            gEqpInfo.Status = enmEqpStatus.Homing
                            gEqpInfo.Message = "Machine A Initial Finish"
                            Exit Sub
                            '^^^^^^^
                        End If
                        
                        gEqpInfo.Status = enmEqpStatus.Homing
                        gEqpInfo.Message = "System is initialling.." 'Soni / 2016.09.13 Logan要求
                        Exit Sub
                        '^^^^^^^
                    End If
                Case eSysCommand.AutoRun '生產命令
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None Then '整機未完成復歸
                        gEqpInfo.Status = enmEqpStatus.NeedHome
                        gEqpInfo.Message = "Click Initial"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Stop Then '整機暫停/停止
                        gEqpInfo.Status = enmEqpStatus.RunStop
                        gEqpInfo.Message = "System Run Stopped"
                        gDOCollection.SetState(enmDO.DoorLock, False) '停止後可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '停止後可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Finish Then '整機生產完成
                        gEqpInfo.Status = enmEqpStatus.RunFinish
                        gEqpInfo.Message = "System Run Finish"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).ExternalPause = True And gSYS(eSys.OverAll).IsCanPause Then '整機暫停
                        gEqpInfo.Status = enmEqpStatus.RunPause
                        gEqpInfo.Message = "System is Paused."
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門

                        '=== 所有Stage備份 ===
                        For mSysNo As Integer = eSys.DispStage1 To eSys.DispStage4
                            gSysStagePause(mSysNo).StopPosX = gCMotion.GetPositionValue(gSYS(mSysNo).AxisX)
                            gSysStagePause(mSysNo).StopPosY = gCMotion.GetPositionValue(gSYS(mSysNo).AxisY)
                            gSysStagePause(mSysNo).StopPosZ = gCMotion.GetPositionValue(gSYS(mSysNo).AxisZ)
                            gSysStagePause(mSysNo).StopPosA = gCMotion.GetPositionValue(gSYS(mSysNo).AxisA)
                            gSysStagePause(mSysNo).StopPosB = gCMotion.GetPositionValue(gSYS(mSysNo).AxisB)
                            gSysStagePause(mSysNo).StopPosC = gCMotion.GetPositionValue(gSYS(mSysNo).AxisC)
                            gSysStagePause(mSysNo).ExcuteCommand = gSYS(mSysNo).ExecuteCommand
                            gSysStagePause(mSysNo).SysNum = gSYS(mSysNo).SysNum
                            'gSysStagePause(mSysNo).SyringeAirPressure = gSysAdapter.GetSyringeAirPressure(gSYS(mSysNo).ValveNo(0))
                            gSysStagePause(mSysNo).SyringeAirPressureOn = gSysAdapter.GetSyringeAirPressureState(gSYS(mSysNo).StageNo, gSYS(mSysNo).ValveNo(0))
                        Next
                        '=== 所有Stage備份 ===
                        Exit Sub
                        '^^^^^^^
                    End If

                    'A機暫停
                    If gSYS(eSys.MachineA).ExternalPause = True And gSYS(eSys.MachineA).IsCanPause = True Then
                        gEqpInfo.Status = enmEqpStatus.RunPauseA
                        gEqpInfo.Message = "Machine-A is Paused."
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        '=== 所有Stage備份 ===
                        For mSysNo As Integer = eSys.DispStage1 To eSys.DispStage2
                            gSysStagePause(mSysNo).StopPosX = gCMotion.GetPositionValue(gSYS(mSysNo).AxisX)
                            gSysStagePause(mSysNo).StopPosY = gCMotion.GetPositionValue(gSYS(mSysNo).AxisY)
                            gSysStagePause(mSysNo).StopPosZ = gCMotion.GetPositionValue(gSYS(mSysNo).AxisZ)
                            gSysStagePause(mSysNo).StopPosA = gCMotion.GetPositionValue(gSYS(mSysNo).AxisA)
                            gSysStagePause(mSysNo).StopPosB = gCMotion.GetPositionValue(gSYS(mSysNo).AxisB)
                            gSysStagePause(mSysNo).StopPosC = gCMotion.GetPositionValue(gSYS(mSysNo).AxisC)
                            gSysStagePause(mSysNo).ExcuteCommand = gSYS(mSysNo).ExecuteCommand
                            gSysStagePause(mSysNo).SysNum = gSYS(mSysNo).SysNum
                            'gSysStagePause(mSysNo).SyringeAirPressure = gSysAdapter.GetSyringeAirPressure(gSYS(mSysNo).ValveNo(0))
                            For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                                gSysStagePause(mSysNo).SyringeAirPressureOn = gSysAdapter.GetSyringeAirPressureState(gSYS(mSysNo).StageNo, mValveNo)
                            Next
                        Next
                        '=== 所有Stage備份 ===
                        Exit Sub
                        '^^^^^^^
                    End If

                   
                    gEqpInfo.Status = enmEqpStatus.Running
                    gEqpInfo.Message = "System is Running.."
                    'gDOCollection.GetSetState(enmDO.DoorLock) = True 避免造成暫停後不可開門
                    'gDOCollection.GetSetState(enmDO.DoorLock2) = True
                    Exit Sub
                    '^^^^^^^

                Case Else '其他命令/無命令
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None Then '整機未完成復歸
                        gEqpInfo.Status = enmEqpStatus.NeedHome
                        gEqpInfo.Message = "Click Initial"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

            End Select
        End Sub

        ''' <summary>取得350A設備狀態</summary>
        ''' <remarks></remarks>
        Public Sub Get350AEqpStatus()
            If enmHardwardAlarm.EMO >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.EMO).Status = True Then
                    For i As Integer = 0 To enmAxis.Max
                        If gCMotion.AxisParameter(i).AxisName <> "" Then
                            gCMotion.EmgStop(i)
                        End If
                    Next
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.EMO).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "EMO Warning"
                            SetMachineAGantry() 'A機同動確保
                            SetMachineBGantry() 'B機同動確保
                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "EMO Alarm"
                            gEqpMsg.AddHistoryAlarm("Alarm_2000001", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000001))
                            SetMachineAGantry() 'A機同動確保
                            SetMachineBGantry() 'B機同動確保
                            Exit Sub
                            '^^^^^^^
                    End Select
                End If
            End If

            If enmHardwardAlarm.EMS >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.EMS).Status = True Then
                    For i As Integer = 0 To enmAxis.Max
                        If gCMotion.AxisParameter(i).AxisName <> "" Then
                            gCMotion.EmgStop(i)
                        End If
                    Next
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.EMS).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A EMS Warning"
                            SetMachineAGantry() 'A機同動確保
                            SetMachineAStatusAlarm() '設定A機狀態為Alarm
                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A EMS Alarm"
                            gEqpMsg.AddHistoryAlarm("Alarm_2000001", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000001))
                            SetMachineAGantry() 'A機同動確保
                            Exit Sub
                            '^^^^^^^
                    End Select
                End If
            End If
            If enmHardwardAlarm.CDA >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.CDA).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.CDA).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A CDA Warning"
                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpMsg.AddHistoryAlarm("Alarm_2000000", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000000)) 'CDA異常!!
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A CDA Alarm"
                            Exit Sub
                            '^^^^^^^
                    End Select
                End If
            End If
            If enmHardwardAlarm.DoorClose >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.DoorClose).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.DoorClose).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A Door-Close Warning"
                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A Door-Close Alarm"
                            Exit Sub
                            '^^^^^^^
                    End Select

                End If
            End If
            If enmHardwardAlarm.MC_Motor >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.MC_Motor).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.MC_Motor).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A MC_Motor Warning"
                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A MC_Motor Alarm"
                            Exit Sub
                            '^^^^^^^
                    End Select

                End If
            End If
            If enmHardwardAlarm.MC_Heater >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.MC_Heater).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.MC_Heater).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A MC_Heater Warning"
                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A MC_Heater Alarm"
                            Exit Sub
                            '^^^^^^^
                    End Select

                End If
            End If

            ''hot plate overheat : WetcoConveyor.mGlobalPool.Overheat = true
            'If (WetcoConveyor.mGlobalPool.Overheat) Then
            '    gEqpInfo.Status = enmEqpStatus.Alarm
            '    gEqpInfo.Message = "Conveyor OvertHeat Alarm"
            '    Exit Sub
            '    '^^^^^^^
            'End If

            'If Unit.A_Roller.Alarm Then
            '    gEqpInfo.Status = enmEqpStatus.Alarm
            '    gEqpInfo.Message = "Machine-A Conveyor Roller Alarm"
            '    Exit Sub
            '    '^^^^^^^
            'End If
            'If Unit.B_Roller.Alarm Then
            '    gEqpInfo.Status = enmEqpStatus.Alarm
            '    gEqpInfo.Message = "Machine-B Conveyor Roller Alarm"
            '    Exit Sub
            '    '^^^^^^^
            'End If

            If (gDICollection.GetState(enmDI.PrevAlarm) Or gDICollection.GetState(enmDI.NextAlarm)) Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Prev/Next Alarm"
                Exit Sub
                '^^^^^^^
            End If
            If (gDICollection.GetState(enmDI.PrevAlarm2) Or gDICollection.GetState(enmDI.NextAlarm2)) Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Prev/Next Alarm"
                Exit Sub
                '^^^^^^^
            End If
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '很重要要說三次

            If gEqpMsg.MsgLevel = eMessageLevel.Alarm Or gEqpMsg.MsgLevel = eMessageLevel.Error Then '如果系統本次拋回含有異常, 則標題顯示為紅色Alarm  Soni / 2016.09.09 搭配燈號 錯誤訊息等級擴充
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Alarm"
                Exit Sub
                '^^^^^^^
            End If
            If gEqpMsg.MsgLevel = eMessageLevel.Warning Then '如果系統本次拋回含有警告, 則標題顯是為黃色Warning  Soni / 2016.09.09 搭配燈號 錯誤訊息等級擴充
                gEqpInfo.Status = enmEqpStatus.Warning
                gEqpInfo.Message = "Warning"
                Exit Sub
                '^^^^^^^
            End If

            '由最底層先判斷, 逐層往上
            For mStageNo As Integer = eSys.DispStage1 To eSys.DispStage1
                If Not gSYS(mStageNo) Is Nothing Then
                    Select Case gSYS(mStageNo).RunStatus
                        Case enmRunStatus.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Stage Alarm"
                            Exit Sub
                            '^^^^^^^
                    End Select
                End If
            Next
            '系統無異常狀況下才會顯示正常訊息
            'Select Case gEqpInfo.RunMode
            '    Case enmMachineRunMode.AutoRun '整機
            Select Case gSYS(eSys.OverAll).ExecuteCommand
                Case eSysCommand.Home '復歸命令
                    If gblnUpdateInitial = True Then '復歸動作中才會更新
                        If gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineA).RunStatus = enmRunStatus.Alarm Then '整機復歸,A機復歸異常
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine A Initial Alarm"
                            gDOCollection.SetState(enmDO.DoorLock, False) '異常可開門
                            Exit Sub
                            '^^^^^^^
                        End If

                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Alarm Then '整機復歸異常
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "System Initial Alarm"
                            gDOCollection.SetState(enmDO.DoorLock, False) '異常可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '異常可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Stop Then '整機暫停/停止
                            gEqpInfo.Status = enmEqpStatus.HomeStop
                            gEqpInfo.Message = "System has Stopped"
                            gDOCollection.SetState(enmDO.DoorLock, False) '停止可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '停止可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        '20170712
                        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None And gSYS(eSys.OverAll).RunStatus = enmRunStatus.Finish Then '整機未完成復歸
                            gEqpInfo.Status = enmEqpStatus.NeedHome
                            gEqpInfo.Message = "Click Initial"
                            gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                            Exit Sub
                            '^^^^^^^
                        End If

                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Finish Then '整機復歸完成
                            gEqpInfo.Status = enmEqpStatus.HomeFinish
                            gEqpInfo.Message = "System Initial Finish"
                            gDOCollection.SetState(enmDO.DoorLock, False) '停止可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '停止可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish Then '整機復歸,A機復歸完成
                            gEqpInfo.Status = enmEqpStatus.Homing
                            gEqpInfo.Message = "Machine A Initial Finish"
                            Exit Sub
                            '^^^^^^^
                        End If

                        gEqpInfo.Status = enmEqpStatus.Homing
                        gEqpInfo.Message = "System is initialling.."
                        Exit Sub
                        '^^^^^^^
                    End If
                Case eSysCommand.AutoRun '生產命令
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None Then '整機未完成復歸
                        gEqpInfo.Status = enmEqpStatus.NeedHome
                        gEqpInfo.Message = "Click Initial"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Stop Then '整機暫停/停止
                        gEqpInfo.Status = enmEqpStatus.RunStop
                        gEqpInfo.Message = "System Run Stopped"
                        gDOCollection.SetState(enmDO.DoorLock, False) '停止後可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '停止後可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Finish Then '整機生產完成
                        gEqpInfo.Status = enmEqpStatus.RunFinish
                        gEqpInfo.Message = "System Run Finish"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).ExternalPause = True And gSYS(eSys.OverAll).IsCanPause Then '整機暫停
                        gEqpInfo.Status = enmEqpStatus.RunPause
                        gEqpInfo.Message = "System is Paused."
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門

                        '=== 所有Stage備份 ===
                        For mSysNo As Integer = eSys.DispStage1 To eSys.DispStage4
                            gSysStagePause(mSysNo).StopPosX = gCMotion.GetPositionValue(gSYS(mSysNo).AxisX)
                            gSysStagePause(mSysNo).StopPosY = gCMotion.GetPositionValue(gSYS(mSysNo).AxisY)
                            gSysStagePause(mSysNo).StopPosZ = gCMotion.GetPositionValue(gSYS(mSysNo).AxisZ)
                            gSysStagePause(mSysNo).StopPosA = gCMotion.GetPositionValue(gSYS(mSysNo).AxisA)
                            gSysStagePause(mSysNo).StopPosB = gCMotion.GetPositionValue(gSYS(mSysNo).AxisB)
                            gSysStagePause(mSysNo).StopPosC = gCMotion.GetPositionValue(gSYS(mSysNo).AxisC)
                            gSysStagePause(mSysNo).ExcuteCommand = gSYS(mSysNo).ExecuteCommand
                            gSysStagePause(mSysNo).SysNum = gSYS(mSysNo).SysNum
                            'gSysStagePause(mSysNo).SyringeAirPressure = gSysAdapter.GetSyringeAirPressure(gSYS(mSysNo).ValveNo(0))
                            For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                                gSysStagePause(mSysNo).SyringeAirPressureOn = gSysAdapter.GetSyringeAirPressureState(gSYS(mSysNo).StageNo, mValveNo)
                            Next
                        Next
                        '=== 所有Stage備份 ===
                        Exit Sub
                        '^^^^^^^
                    End If

                    'A機暫停
                    If gSYS(eSys.MachineA).ExternalPause = True And gSYS(eSys.MachineA).IsCanPause = True Then
                        gEqpInfo.Status = enmEqpStatus.RunPauseA
                        gEqpInfo.Message = "Machine-A is Paused."
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        '=== 所有Stage備份 ===
                        For mSysNo As Integer = eSys.DispStage1 To eSys.DispStage2
                            gSysStagePause(mSysNo).StopPosX = gCMotion.GetPositionValue(gSYS(mSysNo).AxisX)
                            gSysStagePause(mSysNo).StopPosY = gCMotion.GetPositionValue(gSYS(mSysNo).AxisY)
                            gSysStagePause(mSysNo).StopPosZ = gCMotion.GetPositionValue(gSYS(mSysNo).AxisZ)
                            gSysStagePause(mSysNo).StopPosA = gCMotion.GetPositionValue(gSYS(mSysNo).AxisA)
                            gSysStagePause(mSysNo).StopPosB = gCMotion.GetPositionValue(gSYS(mSysNo).AxisB)
                            gSysStagePause(mSysNo).StopPosC = gCMotion.GetPositionValue(gSYS(mSysNo).AxisC)
                            gSysStagePause(mSysNo).ExcuteCommand = gSYS(mSysNo).ExecuteCommand
                            gSysStagePause(mSysNo).SysNum = gSYS(mSysNo).SysNum
                            'gSysStagePause(mSysNo).SyringeAirPressure = gSysAdapter.GetSyringeAirPressure(gSYS(mSysNo).ValveNo(0))
                            For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                                gSysStagePause(mSysNo).SyringeAirPressureOn = gSysAdapter.GetSyringeAirPressureState(gSYS(mSysNo).StageNo, mValveNo)
                            Next
                        Next
                        '=== 所有Stage備份 ===
                        Exit Sub
                        '^^^^^^^
                    End If

                    gEqpInfo.Status = enmEqpStatus.Running
                    gEqpInfo.Message = "System is Running.."
                    'gDOCollection.GetSetState(enmDO.DoorLock) = True 避免造成暫停後不可開門
                    'gDOCollection.GetSetState(enmDO.DoorLock2) = True
                    Exit Sub
                    '^^^^^^^

                Case Else '其他命令/無命令
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None Then '整機未完成復歸
                        gEqpInfo.Status = enmEqpStatus.NeedHome
                        gEqpInfo.Message = "Click Initial"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

            End Select
        End Sub

        ''' <summary>取得F230A設備狀態</summary>
        ''' <remarks></remarks>
        Public Sub GetF230AEqpStatus()
            If enmHardwardAlarm.EMO >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.EMO).Status = True Then
                    For i As Integer = 0 To enmAxis.Max
                        If gCMotion.AxisParameter(i).AxisName <> "" Then
                            gCMotion.EmgStop(i)
                        End If
                    Next
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.EMO).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "EMO Warning"
                            SetMachineAGantry() 'A機同動確保
                            SetMachineBGantry() 'B機同動確保
                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "EMO Alarm"
                            gEqpMsg.AddHistoryAlarm("Alarm_2000001", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000001))
                            SetMachineAGantry() 'A機同動確保
                            SetMachineBGantry() 'B機同動確保
                            Exit Sub
                            '^^^^^^^
                    End Select
                End If
            End If

            If enmHardwardAlarm.EMS >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.EMS).Status = True Then
                    For i As Integer = 0 To enmAxis.Max
                        If gCMotion.AxisParameter(i).AxisName <> "" Then
                            gCMotion.EmgStop(i)
                        End If
                    Next
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.EMS).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A EMS Warning"
                            SetMachineAGantry() 'A機同動確保
                            SetMachineAStatusAlarm() '設定A機狀態為Alarm
                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A EMS Alarm"
                            gEqpMsg.AddHistoryAlarm("Alarm_2000001", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000001))
                            SetMachineAGantry() 'A機同動確保
                            Exit Sub
                            '^^^^^^^
                    End Select
                End If
            End If
            If enmHardwardAlarm.CDA >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.CDA).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.CDA).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A CDA Warning"
                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpMsg.AddHistoryAlarm("Alarm_2000000", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000000)) 'CDA異常!!
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A CDA Alarm"
                            Exit Sub
                            '^^^^^^^
                    End Select
                End If
            End If
            If enmHardwardAlarm.DoorClose >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.DoorClose).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.DoorClose).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A Door-Close Warning"
                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A Door-Close Alarm"
                            Exit Sub
                            '^^^^^^^
                    End Select

                End If
            End If
            If enmHardwardAlarm.MC_Motor >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.MC_Motor).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.MC_Motor).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A MC_Motor Warning"
                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A MC_Motor Alarm"
                            Exit Sub
                            '^^^^^^^
                    End Select

                End If
            End If
            If enmHardwardAlarm.MC_Heater >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.MC_Heater).Status = True Then
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.MC_Heater).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "Machine-A MC_Heater Warning"
                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine-A MC_Heater Alarm"
                            Exit Sub
                            '^^^^^^^
                    End Select

                End If
            End If

            ''hot plate overheat : WetcoConveyor.mGlobalPool.Overheat = true
            'If (WetcoConveyor.mGlobalPool.Overheat) Then
            '    gEqpInfo.Status = enmEqpStatus.Alarm
            '    gEqpInfo.Message = "Conveyor OvertHeat Alarm"
            '    Exit Sub
            '    '^^^^^^^
            'End If

            'If Unit.A_Roller.Alarm Then
            '    gEqpInfo.Status = enmEqpStatus.Alarm
            '    gEqpInfo.Message = "Machine-A Conveyor Roller Alarm"
            '    Exit Sub
            '    '^^^^^^^
            'End If
            'If Unit.B_Roller.Alarm Then
            '    gEqpInfo.Status = enmEqpStatus.Alarm
            '    gEqpInfo.Message = "Machine-B Conveyor Roller Alarm"
            '    Exit Sub
            '    '^^^^^^^
            'End If

            If (gDICollection.GetState(enmDI.PrevAlarm) Or gDICollection.GetState(enmDI.NextAlarm)) Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Prev/Next Alarm"
                Exit Sub
                '^^^^^^^
            End If
            If (gDICollection.GetState(enmDI.PrevAlarm2) Or gDICollection.GetState(enmDI.NextAlarm2)) Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Prev/Next Alarm"
                Exit Sub
                '^^^^^^^
            End If
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '很重要要說三次

            If gEqpMsg.MsgLevel = eMessageLevel.Alarm Or gEqpMsg.MsgLevel = eMessageLevel.Error Then '如果系統本次拋回含有異常, 則標題顯示為紅色Alarm  Soni / 2016.09.09 搭配燈號 錯誤訊息等級擴充
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Alarm"
                Exit Sub
                '^^^^^^^
            End If
            If gEqpMsg.MsgLevel = eMessageLevel.Warning Then '如果系統本次拋回含有警告, 則標題顯是為黃色Warning  Soni / 2016.09.09 搭配燈號 錯誤訊息等級擴充
                gEqpInfo.Status = enmEqpStatus.Warning
                gEqpInfo.Message = "Warning"
                Exit Sub
                '^^^^^^^
            End If

            '由最底層先判斷, 逐層往上
            For mStageNo As Integer = eSys.DispStage1 To eSys.DispStage1
                If Not gSYS(mStageNo) Is Nothing Then
                    Select Case gSYS(mStageNo).RunStatus
                        Case enmRunStatus.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Stage Alarm"
                            Exit Sub
                            '^^^^^^^
                    End Select
                End If
            Next
            '系統無異常狀況下才會顯示正常訊息
            'Select Case gEqpInfo.RunMode
            '    Case enmMachineRunMode.AutoRun '整機
            Select Case gSYS(eSys.OverAll).ExecuteCommand
                Case eSysCommand.Home '復歸命令
                    If gblnUpdateInitial = True Then '復歸動作中才會更新
                        If gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineA).RunStatus = enmRunStatus.Alarm Then '整機復歸,A機復歸異常
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine A Initial Alarm"
                            gDOCollection.SetState(enmDO.DoorLock, False) '異常可開門
                            Exit Sub
                            '^^^^^^^
                        End If

                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Alarm Then '整機復歸異常
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "System Initial Alarm"
                            gDOCollection.SetState(enmDO.DoorLock, False) '異常可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '異常可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Stop Then '整機暫停/停止
                            gEqpInfo.Status = enmEqpStatus.HomeStop
                            gEqpInfo.Message = "System has Stopped"
                            gDOCollection.SetState(enmDO.DoorLock, False) '停止可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '停止可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Finish Then '整機復歸完成
                            gEqpInfo.Status = enmEqpStatus.HomeFinish
                            gEqpInfo.Message = "System Initial Finish"
                            gDOCollection.SetState(enmDO.DoorLock, False) '停止可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '停止可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish Then '整機復歸,A機復歸完成
                            gEqpInfo.Status = enmEqpStatus.Homing
                            gEqpInfo.Message = "Machine A Initial Finish"
                            Exit Sub
                            '^^^^^^^
                        End If

                        gEqpInfo.Status = enmEqpStatus.Homing
                        gEqpInfo.Message = "System is initialling.."
                        Exit Sub
                        '^^^^^^^
                    End If
                Case eSysCommand.AutoRun '生產命令
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None Then '整機未完成復歸
                        gEqpInfo.Status = enmEqpStatus.NeedHome
                        gEqpInfo.Message = "Click Initial"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Stop Then '整機暫停/停止
                        gEqpInfo.Status = enmEqpStatus.RunStop
                        gEqpInfo.Message = "System Run Stopped"
                        gDOCollection.SetState(enmDO.DoorLock, False) '停止後可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '停止後可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Finish Then '整機生產完成
                        gEqpInfo.Status = enmEqpStatus.RunFinish
                        gEqpInfo.Message = "System Run Finish"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).ExternalPause = True And gSYS(eSys.OverAll).IsCanPause Then '整機暫停
                        gEqpInfo.Status = enmEqpStatus.RunPause
                        gEqpInfo.Message = "System is Paused."
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門

                        '=== 所有Stage備份 ===
                        For mSysNo As Integer = eSys.DispStage1 To eSys.DispStage4
                            gSysStagePause(mSysNo).StopPosX = gCMotion.GetPositionValue(gSYS(mSysNo).AxisX)
                            gSysStagePause(mSysNo).StopPosY = gCMotion.GetPositionValue(gSYS(mSysNo).AxisY)
                            gSysStagePause(mSysNo).StopPosZ = gCMotion.GetPositionValue(gSYS(mSysNo).AxisZ)
                            gSysStagePause(mSysNo).StopPosA = gCMotion.GetPositionValue(gSYS(mSysNo).AxisA)
                            gSysStagePause(mSysNo).StopPosB = gCMotion.GetPositionValue(gSYS(mSysNo).AxisB)
                            gSysStagePause(mSysNo).StopPosC = gCMotion.GetPositionValue(gSYS(mSysNo).AxisC)
                            gSysStagePause(mSysNo).ExcuteCommand = gSYS(mSysNo).ExecuteCommand
                            gSysStagePause(mSysNo).SysNum = gSYS(mSysNo).SysNum
                            'gSysStagePause(mSysNo).SyringeAirPressure = gSysAdapter.GetSyringeAirPressure(gSYS(mSysNo).ValveNo(0))
                            For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                                gSysStagePause(mSysNo).SyringeAirPressureOn = gSysAdapter.GetSyringeAirPressureState(gSYS(mSysNo).StageNo, mValveNo)
                            Next
                        Next
                        '=== 所有Stage備份 ===
                        Exit Sub
                        '^^^^^^^
                    End If

                    'A機暫停
                    If gSYS(eSys.MachineA).ExternalPause = True And gSYS(eSys.MachineA).IsCanPause = True Then
                        gEqpInfo.Status = enmEqpStatus.RunPauseA
                        gEqpInfo.Message = "Machine-A is Paused."
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        '=== 所有Stage備份 ===
                        For mSysNo As Integer = eSys.DispStage1 To eSys.DispStage2
                            gSysStagePause(mSysNo).StopPosX = gCMotion.GetPositionValue(gSYS(mSysNo).AxisX)
                            gSysStagePause(mSysNo).StopPosY = gCMotion.GetPositionValue(gSYS(mSysNo).AxisY)
                            gSysStagePause(mSysNo).StopPosZ = gCMotion.GetPositionValue(gSYS(mSysNo).AxisZ)
                            gSysStagePause(mSysNo).StopPosA = gCMotion.GetPositionValue(gSYS(mSysNo).AxisA)
                            gSysStagePause(mSysNo).StopPosB = gCMotion.GetPositionValue(gSYS(mSysNo).AxisB)
                            gSysStagePause(mSysNo).StopPosC = gCMotion.GetPositionValue(gSYS(mSysNo).AxisC)
                            gSysStagePause(mSysNo).ExcuteCommand = gSYS(mSysNo).ExecuteCommand
                            gSysStagePause(mSysNo).SysNum = gSYS(mSysNo).SysNum
                            'gSysStagePause(mSysNo).SyringeAirPressure = gSysAdapter.GetSyringeAirPressure(gSYS(mSysNo).ValveNo(0))
                            For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                                gSysStagePause(mSysNo).SyringeAirPressureOn = gSysAdapter.GetSyringeAirPressureState(gSYS(mSysNo).StageNo, mValveNo)
                            Next
                        Next
                        '=== 所有Stage備份 ===
                        Exit Sub
                        '^^^^^^^
                    End If

                    gEqpInfo.Status = enmEqpStatus.Running
                    gEqpInfo.Message = "System is Running.."
                    'gDOCollection.GetSetState(enmDO.DoorLock) = True 避免造成暫停後不可開門
                    'gDOCollection.GetSetState(enmDO.DoorLock2) = True
                    Exit Sub
                    '^^^^^^^

                Case Else '其他命令/無命令
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None Then '整機未完成復歸
                        gEqpInfo.Status = enmEqpStatus.NeedHome
                        gEqpInfo.Message = "Click Initial"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

            End Select
        End Sub

        'Soni + 2016.11.23
        ''' <summary>取得單軌雙閥設備狀態</summary>
        ''' <remarks></remarks>
        Public Sub Get2S2VEqpStatus()
            '硬體優先檢查, 最後才是軟體

            If enmHardwardAlarm.EMO >= 0 Then
                If gInterlockCollection.Items(enmHardwardAlarm.EMO).Status = True Then
                    For i As Integer = 0 To enmAxis.Max
                        If gCMotion.AxisParameter(i).AxisName <> "" Then
                            gCMotion.EmgStop(i)
                        End If
                    Next
                    Select Case gInterlockCollection.Items(enmHardwardAlarm.EMO).Level
                        Case eInterlock.Warn
                            gEqpInfo.Status = enmEqpStatus.Warning
                            gEqpInfo.Message = "EMO Warning"
                            SetMachineAGantry() 'A機同動確保
                            SetMachineBGantry() 'B機同動確保

                            Exit Sub
                            '^^^^^^^
                        Case eInterlock.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "EMO Alarm"
                            gEqpMsg.AddHistoryAlarm("Alarm_2000001", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000001))
                            SetMachineAGantry() 'A機同動確保
                            SetMachineBGantry() 'B機同動確保
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '復歸狀態清除
                            Exit Sub
                            '^^^^^^^
                    End Select
                End If
            End If

            CheckW800AQHardwareAlarmA()

            If (gDICollection.GetState(enmDI.PrevAlarm) Or gDICollection.GetState(enmDI.NextAlarm)) Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-A Conveyor Prev/Next Alarm"
                Exit Sub
                '^^^^^^^
            End If
            If (gDICollection.GetState(enmDI.PrevAlarm2) Or gDICollection.GetState(enmDI.NextAlarm2)) Then
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Machine-B Conveyor Prev/Next Alarm"
                Exit Sub
                '^^^^^^^
            End If
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '=== 以上為硬體判定   分   隔   線   以下為軟體判定 ===
            '很重要要說三次

            If gEqpMsg.MsgLevel = eMessageLevel.Alarm Or gEqpMsg.MsgLevel = eMessageLevel.Error Then '如果系統本次拋回含有異常, 則標題顯示為紅色Alarm Soni / 2016.09.09 搭配燈號 錯誤訊息等級擴充
                gEqpInfo.Status = enmEqpStatus.Alarm
                gEqpInfo.Message = "Alarm"
                Exit Sub
                '^^^^^^^
            End If
            If gEqpMsg.MsgLevel = eMessageLevel.Warning Then '如果系統本次拋回含有警告, 則標題顯是為黃色Warning Soni / 2016.09.09 搭配燈號 錯誤訊息等級擴充
                gEqpInfo.Status = enmEqpStatus.Warning
                gEqpInfo.Message = "Warning"
                Exit Sub
                '^^^^^^^
            End If

            '由最底層先判斷, 逐層往上
            For mStageNo As Integer = eSys.DispStage1 To gSSystemParameter.StageMax 'eSys.DispStage4
                If Not gSYS(mStageNo) Is Nothing Then
                    Select Case gSYS(mStageNo).RunStatus
                        Case enmRunStatus.Alarm
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Stage Alarm"
                            Exit Sub
                            '^^^^^^^
                    End Select
                End If
            Next
            '系統無異常狀況下才會顯示正常訊息
            'Select Case gEqpInfo.RunMode
            '    Case enmMachineRunMode.AutoRun '整機
            Select Case gSYS(eSys.OverAll).ExecuteCommand
                Case eSysCommand.Home '復歸命令
                    If gblnUpdateInitial = True Then '復歸動作中才會更新
                        If gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineA).RunStatus = enmRunStatus.Alarm Then '整機復歸,A機復歸異常
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "Machine A Initial Alarm"
                            gDOCollection.SetState(enmDO.DoorLock, False) '異常可開門
                            Exit Sub
                            '^^^^^^^
                        End If

                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Alarm Then '整機復歸異常
                            gEqpInfo.Status = enmEqpStatus.Alarm
                            gEqpInfo.Message = "System Initial Alarm"
                            gDOCollection.SetState(enmDO.DoorLock, False) '異常可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '異常可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Stop Then '整機暫停/停止
                            gEqpInfo.Status = enmEqpStatus.HomeStop
                            gEqpInfo.Message = "System has Stopped"
                            gDOCollection.SetState(enmDO.DoorLock, False) '停止可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '停止可開門
                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Finish Then '整機復歸完成
                            gEqpInfo.Status = enmEqpStatus.HomeFinish
                            If gEqpInfo.Message <> "System Initial Finish" Then
                                gDOCollection.SetState(enmDO.StartButtonLight, True) 'Perry要求, 復歸完提示使用者可按開始
                                gDOCollection.SetState(enmDO.PauseButtonLight, False) 'Perry要求, 復歸完提示使用者不可按結束
                                gDOCollection.SetState(enmDO.StartButtonLight2, True) 'Perry要求, 復歸完提示使用者可按開始
                                gDOCollection.SetState(enmDO.PauseButtonLight2, False) 'Perry要求, 復歸完提示使用者不可按結束
                                gEqpInfo.Message = "System Initial Finish"
                            End If
                            gDOCollection.SetState(enmDO.DoorLock, False) '停止可開門
                            gDOCollection.SetState(enmDO.DoorLock2, False) '停止可開門


                            Exit Sub
                            '^^^^^^^
                        End If
                        If gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish Then '整機復歸,A機復歸完成
                            gEqpInfo.Status = enmEqpStatus.Homing
                            gEqpInfo.Message = "Machine A Initial Finish"
                            Exit Sub
                            '^^^^^^^
                        End If

                        gEqpInfo.Status = enmEqpStatus.Homing
                        gEqpInfo.Message = "System is initialling.." 'Soni / 2016.09.13 Logan要求
                        Exit Sub
                        '^^^^^^^
                    End If
                Case eSysCommand.AutoRun '生產命令
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None Then '整機未完成復歸
                        gEqpInfo.Status = enmEqpStatus.NeedHome
                        gEqpInfo.Message = "Click Initial"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Stop Then '整機暫停/停止
                        gEqpInfo.Status = enmEqpStatus.RunStop
                        gEqpInfo.Message = "System Run Stopped"
                        gDOCollection.SetState(enmDO.DoorLock, False) '停止後可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '停止後可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Finish Then '整機生產完成
                        gEqpInfo.Status = enmEqpStatus.RunFinish
                        gEqpInfo.Message = "System Run Finish"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

                    If gSYS(eSys.OverAll).ExternalPause = True And gSYS(eSys.OverAll).IsCanPause Then '整機暫停
                        gEqpInfo.Status = enmEqpStatus.RunPause
                        gEqpInfo.Message = "System is Paused."
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門

                        '=== 所有Stage備份 ===
                        For mSysNo As Integer = eSys.DispStage1 To eSys.DispStage4
                            gSysStagePause(mSysNo).StopPosX = gCMotion.GetPositionValue(gSYS(mSysNo).AxisX)
                            gSysStagePause(mSysNo).StopPosY = gCMotion.GetPositionValue(gSYS(mSysNo).AxisY)
                            gSysStagePause(mSysNo).StopPosZ = gCMotion.GetPositionValue(gSYS(mSysNo).AxisZ)
                            gSysStagePause(mSysNo).StopPosA = gCMotion.GetPositionValue(gSYS(mSysNo).AxisA)
                            gSysStagePause(mSysNo).StopPosB = gCMotion.GetPositionValue(gSYS(mSysNo).AxisB)
                            gSysStagePause(mSysNo).StopPosC = gCMotion.GetPositionValue(gSYS(mSysNo).AxisC)
                            gSysStagePause(mSysNo).ExcuteCommand = gSYS(mSysNo).ExecuteCommand
                            gSysStagePause(mSysNo).SysNum = gSYS(mSysNo).SysNum
                            'gSysStagePause(mSysNo).SyringeAirPressure = gSysAdapter.GetSyringeAirPressure(gSYS(mSysNo).ValveNo(0))
                            For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                                gSysStagePause(mSysNo).SyringeAirPressureOn = gSysAdapter.GetSyringeAirPressureState(gSYS(mSysNo).StageNo, mValveNo)
                            Next
                        Next
                        '=== 所有Stage備份 ===
                        Exit Sub
                        '^^^^^^^
                    End If

                    'A機暫停
                    If gSYS(eSys.MachineA).ExternalPause = True And gSYS(eSys.MachineA).IsCanPause = True Then
                        gEqpInfo.Status = enmEqpStatus.RunPauseA
                        gEqpInfo.Message = "Machine-A is Paused."
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        '=== 所有Stage備份 ===
                        For mSysNo As Integer = eSys.DispStage1 To eSys.DispStage2
                            gSysStagePause(mSysNo).StopPosX = gCMotion.GetPositionValue(gSYS(mSysNo).AxisX)
                            gSysStagePause(mSysNo).StopPosY = gCMotion.GetPositionValue(gSYS(mSysNo).AxisY)
                            gSysStagePause(mSysNo).StopPosZ = gCMotion.GetPositionValue(gSYS(mSysNo).AxisZ)
                            gSysStagePause(mSysNo).StopPosA = gCMotion.GetPositionValue(gSYS(mSysNo).AxisA)
                            gSysStagePause(mSysNo).StopPosB = gCMotion.GetPositionValue(gSYS(mSysNo).AxisB)
                            gSysStagePause(mSysNo).StopPosC = gCMotion.GetPositionValue(gSYS(mSysNo).AxisC)
                            gSysStagePause(mSysNo).ExcuteCommand = gSYS(mSysNo).ExecuteCommand
                            gSysStagePause(mSysNo).SysNum = gSYS(mSysNo).SysNum
                            'gSysStagePause(mSysNo).SyringeAirPressure = gSysAdapter.GetSyringeAirPressure(gSYS(mSysNo).ValveNo(0))
                            For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                                gSysStagePause(mSysNo).SyringeAirPressureOn = gSysAdapter.GetSyringeAirPressureState(gSYS(mSysNo).StageNo, mValveNo)
                            Next
                        Next
                        '=== 所有Stage備份 ===
                        Exit Sub
                        '^^^^^^^
                    End If

                    gEqpInfo.Status = enmEqpStatus.Running
                    gEqpInfo.Message = "System is Running.."
                    'gDOCollection.GetSetState(enmDO.DoorLock) = True 避免造成暫停後不可開門
                    'gDOCollection.GetSetState(enmDO.DoorLock2) = True
                    Exit Sub
                    '^^^^^^^

                Case Else '其他命令/無命令
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None Then '整機未完成復歸
                        gEqpInfo.Status = enmEqpStatus.NeedHome
                        gEqpInfo.Message = "Click Initial"
                        gDOCollection.SetState(enmDO.DoorLock, False) '可開門
                        gDOCollection.SetState(enmDO.DoorLock2, False) '可開門
                        Exit Sub
                        '^^^^^^^
                    End If

            End Select
        End Sub

        ''' <summary>W800AQ暫停是否可繼續</summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsW800AQPauseCanContinue() As Boolean
            Const INPRange As Decimal = 0.025 '容許誤差範圍
            '位置比對由於馬達定位誤差, 以位置相減進行比對
            'Select Case RunMode
            '    Case enmMachineRunMode.AutoRun
            For mSysNo As Integer = eSys.DispStage1 To eSys.DispStage4
                If Math.Abs(gSysStagePause(mSysNo).StopPosX - gCMotion.GetPositionValue(gSYS(mSysNo).AxisX)) > INPRange Then '任一不相等則不能繼續
                    Return False
                End If
                If Math.Abs(gSysStagePause(mSysNo).StopPosY - gCMotion.GetPositionValue(gSYS(mSysNo).AxisY)) > INPRange Then '任一不相等則不能繼續
                    Return False
                End If
                If Math.Abs(gSysStagePause(mSysNo).StopPosZ - gCMotion.GetPositionValue(gSYS(mSysNo).AxisZ)) > INPRange Then '任一不相等則不能繼續
                    Return False
                End If
                If Math.Abs(gSysStagePause(mSysNo).StopPosA - gCMotion.GetPositionValue(gSYS(mSysNo).AxisA)) > INPRange Then '任一不相等則不能繼續
                    Return False
                End If
                If Math.Abs(gSysStagePause(mSysNo).StopPosB - gCMotion.GetPositionValue(gSYS(mSysNo).AxisB)) > INPRange Then '任一不相等則不能繼續
                    Return False
                End If
                If Math.Abs(gSysStagePause(mSysNo).StopPosC - gCMotion.GetPositionValue(gSYS(mSysNo).AxisC)) > INPRange Then '任一不相等則不能繼續
                    Return False
                End If
                If gSysStagePause(mSysNo).SyringeAirPressureOn <> gSysAdapter.GetSyringeAirPressureState(gSYS(mSysNo).StageNo, gSYS(mSysNo).ValveNo(0)) Then
                    Return False
                End If
                If gSysStagePause(mSysNo).SyringeAirPressure <> gSysAdapter.GetAirPressure(gSYS(mSysNo).StageNo, gSYS(mSysNo).ValveNo(0), eEPVPressureType.Syringe) Then
                    Return False
                End If
                'For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                '    If gSysStagePause(mSysNo).SyringeAirPressureOn <> gSysAdapter.GetSyringeAirPressureOnOff(gSYS(mSysNo).StageNo, mValveNo) Then '任一不相等則不能繼續
                '        Return False
                '    End If
                '    If gSysStagePause(mSysNo).SyringeAirPressure <> gSysAdapter.GetSyringeAirPressure(gSYS(mSysNo).StageNo, mValveNo) Then '任一不相等則不能繼續
                '        Return False
                '    End If
                'Next
            Next
            Return True

        End Function
    End Structure

    ''' <summary>設備狀態</summary>
    ''' <remarks></remarks>
    Public gEqpInfo As sEqpInfo



    ''' <summary>
    ''' A機同動確保
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetMachineAGantry()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.eDTS300A, enmMachineType.eDTS330A
                '--- Soni + 2014.09.29 馬達Gantry重設 ---
                If gDICollection.GetState(enmDI.MC2) = True Then '馬達有動力判定才正確
                    If gDICollection.GetState(enmDI.EMS) = False Then 'EMS未按下
                        If gCMotion.GetAxisState(enmAxis.Y1Axis) = Premtek.Base.CommandStatus.Sucessed Then
                            If gCMotion.AxisParameter(enmAxis.Y1Axis).MotionStatus = AxisState.STA_AX_ERROR_STOP Then 'Y2不再同動狀態
                                gCMotion.GpClearMovePath(gCMotion.SyncParameter(0)) '清除位置
                            End If
                        End If
                    End If
                End If
                '--- Soni + 2014.09.29 馬達Gantry重設 ---

            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If gDICollection.GetState(enmDI.MC2) = True Then '馬達有動力判定才正確
                    If gDICollection.GetState(enmDI.EMS) = False Then 'EMS未按下
                        If gCMotion.GetAxisState(enmAxis.XAxis) = Premtek.Base.CommandStatus.Sucessed Then
                            If gCMotion.AxisParameter(enmAxis.X2Axis).MotionStatus = AxisState.STA_AX_ERROR_STOP Then '第一組X2不再同動狀態
                                gCMotion.GpClearMovePath(gCMotion.SyncParameter(0)) '清除位置
                            End If
                        End If
                    End If
                    If gDICollection.GetState(enmDI.EMS2) = False Then 'EMS未按下
                        If gCMotion.GetAxisState(enmAxis.UAxis) = Premtek.Base.CommandStatus.Sucessed Then
                            If gCMotion.AxisParameter(enmAxis.U2Axis).MotionStatus = AxisState.STA_AX_ERROR_STOP Then '第二組X不再同動狀態
                                gCMotion.GpClearMovePath(gCMotion.SyncParameter(1)) '清除位置
                            End If
                        End If
                    End If
                End If
        End Select
    End Sub
    ''' <summary>
    ''' B機同動確保
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetMachineBGantry()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gDICollection.GetState(enmDI.MC_Motor2) = True Then '馬達有動力判定才正確
                    If gCMotion.GetAxisState(enmAxis.RAxis) = Premtek.Base.CommandStatus.Sucessed Then
                        If gCMotion.AxisParameter(enmAxis.R2Axis).MotionStatus = AxisState.STA_AX_ERROR_STOP Then '第三組X2不再同動狀態
                            gCMotion.GpClearMovePath(gCMotion.SyncParameter(2)) '清除位置
                        End If
                    End If
                    If gCMotion.GetAxisState(enmAxis.OAxis) = Premtek.Base.CommandStatus.Sucessed Then
                        If gCMotion.AxisParameter(enmAxis.O2Axis).MotionStatus = AxisState.STA_AX_ERROR_STOP Then '第四組X2不再同動狀態
                            gCMotion.GpClearMovePath(gCMotion.SyncParameter(3)) '清除位置
                        End If
                    End If
                End If

        End Select
    End Sub
    ''' <summary>
    ''' 設定A機狀態為Alarm
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetMachineAStatusAlarm()
        gSYS(eSys.MachineA).RunStatus = enmRunStatus.Alarm
        For mStageNo As Integer = eSys.DispStage1 To eSys.DispStage2
            gSYS(mStageNo).RunStatus = enmRunStatus.Alarm
            gSYS(mStageNo).Act(eAct.ClearGlue).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.ChangeGlue).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.LaserReader).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.Dispensing).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.CCDSCanGlue).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.CCDSCanFix).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
        Next
    End Sub
    ''' <summary>
    ''' 設定B機狀態為Alarm
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetMachineBStatusAlarm()
        gSYS(eSys.MachineB).RunStatus = enmRunStatus.Alarm
        For mStageNo As Integer = eSys.DispStage3 To eSys.DispStage4
            gSYS(mStageNo).RunStatus = enmRunStatus.Alarm
            gSYS(mStageNo).Act(eAct.ClearGlue).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.ChangeGlue).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.LaserReader).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.Dispensing).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.CCDSCanGlue).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.CCDSCanFix).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
            gSYS(mStageNo).Act(eAct.WeightUnit).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
        Next
    End Sub

    ''' <summary>暫停保存狀態資料結構</summary>
    ''' <remarks></remarks>
    Public Structure sSysStageData
        ''' <summary>執行命令</summary>
        ''' <remarks></remarks>
        Public ExcuteCommand As eSysCommand
        ''' <summary>執行步驟</summary>
        ''' <remarks></remarks>
        Public SysNum As Integer
        ''' <summary>停止時的X位置</summary>
        ''' <remarks></remarks>
        Public StopPosX As Decimal
        ''' <summary>停止時的Y位置</summary>
        ''' <remarks></remarks>
        Public StopPosY As Decimal
        ''' <summary>停止時的Z位置</summary>
        ''' <remarks></remarks>
        Public StopPosZ As Decimal
        ''' <summary>停止時的A位置</summary>
        ''' <remarks></remarks>
        Public StopPosA As Decimal
        ''' <summary>停止時的B位置</summary>
        ''' <remarks></remarks>
        Public StopPosB As Decimal
        ''' <summary>停止時的C位置</summary>
        ''' <remarks></remarks>
        Public StopPosC As Decimal
        ''' <summary>膠管氣壓開關狀態</summary>
        ''' <remarks></remarks>
        Public SyringeAirPressureOn As Boolean
        ''' <summary>第二管膠管氣壓開關狀態(預留)</summary>
        ''' <remarks></remarks>
        Public SyringeAirPressureOn2 As Boolean
        ''' <summary>膠管氣壓值</summary>
        ''' <remarks></remarks>
        Public SyringeAirPressure As Decimal
        ''' <summary>膠管氣壓值2(預留)</summary>
        ''' <remarks></remarks>
        Public SyringeAirPressure2 As Decimal
    End Structure


    ''' <summary>暫停資料紀錄</summary>
    ''' <remarks></remarks>
    Public gSysStagePause(eSys.Max) As sSysStageData

End Module

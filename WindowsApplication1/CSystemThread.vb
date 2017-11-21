Imports ProjectIO
Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports WetcoConveyor

''' <summary>
''' System Thread
''' </summary>
''' <remarks></remarks>
Public Class CSystemThread


    'Const busyTime As Integer = 18

    'Dim CycleTimer As New Stopwatch
    Dim oldTimer As Decimal
    Dim nowTimer As Decimal

    Dim SwLulDelay As New Stopwatch

    ''' <summary>是否發生Gantry錯誤</summary>
    ''' <remarks></remarks>
    Dim gantryError_F As Boolean
    ''' <summary>是否發生Gantry錯誤</summary>
    ''' <remarks></remarks>
    Dim gantry1Error_F As Boolean
    ''' <summary>是否發生Gantry錯誤</summary>
    ''' <remarks></remarks>
    Dim gantry2Error_F As Boolean
    ''' <summary>是否發生Gantry錯誤</summary>
    ''' <remarks></remarks>
    Dim gantry3Error_F As Boolean
    ''' <summary>是否發生Gantry錯誤</summary>
    ''' <remarks></remarks>
    Dim gantry4Error_F As Boolean

    ''' <summary>*****主生產迴圈*****</summary>
    ''' <remarks></remarks>
    Sub Action()
        'Dim mStopWatch As New Stopwatch
        'mStopWatch.Restart()
        While 1

            ''*************************************mobary+ 2014/03/11***********************
            '[說明]:UpDate AIO、DIO、Motor Status 

            '[說明]:If the axis is in ErrorStop state, the state will be changed to Ready after calling reset function.
            'For i As Integer = 0 to gSYS(eSys.DispStage1).
            If gSSystemParameter.RunMode = enmRunMode.Run Then

                'TODO:收到System_DispStage內
                If enmAxis.Y2Axis <> -1 Then
                    If gCMotion.GetAxisState(enmAxis.Y2Axis) = Premtek.Base.CommandStatus.Sucessed Then
                        If gCMotion.AxisParameter(enmAxis.Y2Axis).MotionStatus <> AxisState.STA_AX_SYNC_MOT Then
                            gCMotion.EmgStop(enmAxis.Y2Axis)
                            If gCMotion.AxisResetError(enmAxis.Y1Axis) = Premtek.Base.CommandStatus.Sucessed Then
                                If gCMotion.AxisResetError(enmAxis.Y2Axis) = Premtek.Base.CommandStatus.Sucessed Then
                                    If gDICollection.GetState(enmDI.EMS) = False Then
                                        If gCMotion.SetGantry(enmAxis.Y1Axis, enmAxis.Y2Axis) = CommandStatus.Sucessed Then
                                            gantryError_F = False
                                            gCMotion.AxisParameter(enmAxis.Y2Axis).MotionStatus = Nothing
                                        ElseIf gantryError_F = False Then '2016.06.29 避免Loop存檔衝擊
                                            gSyslog.Save(gMsgHandler.GetMessage(Error_1000004), "Error_1000004", eMessageLevel.Error)
                                            gSYS(eSys.DispStage1).RunStatus = enmRunStatus.Alarm 'Gantry異常, 切斷流程
                                            gSYS(eSys.SubDisp1).RunStatus = enmRunStatus.Alarm 'Gantry異常, 切斷流程
                                            gantryError_F = True
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    End If
                End If
                If enmAxis.X2Axis <> -1 Then
                    If gCMotion.GetAxisState(enmAxis.X2Axis) = Premtek.Base.CommandStatus.Sucessed Then
                        If gCMotion.AxisParameter(enmAxis.X2Axis).MotionStatus <> AxisState.STA_AX_SYNC_MOT Then
                            gCMotion.EmgStop(enmAxis.X2Axis)
                            If gCMotion.AxisResetError(enmAxis.XAxis) = Premtek.Base.CommandStatus.Sucessed Then
                                If gCMotion.AxisResetError(enmAxis.X2Axis) = Premtek.Base.CommandStatus.Sucessed Then
                                    If gDICollection.GetState(enmDI.EMS) = False Then
                                        If gCMotion.SetGantry(enmAxis.XAxis, enmAxis.X2Axis) = CommandStatus.Sucessed Then
                                            gCMotion.AxisParameter(enmAxis.X2Axis).MotionStatus = Nothing
                                            gantry1Error_F = False
                                        ElseIf gantry1Error_F = False Then '2016.06.29 避免Loop存檔衝擊
                                            gSyslog.Save(gMsgHandler.GetMessage(Error_1000004), "Error_1000004", eMessageLevel.Error)
                                            gSYS(eSys.DispStage1).RunStatus = enmRunStatus.Alarm 'Gantry異常, 切斷流程
                                            gSYS(eSys.SubDisp1).RunStatus = enmRunStatus.Alarm 'Gantry異常, 切斷流程
                                            gantry1Error_F = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                If enmAxis.U2Axis <> -1 Then
                    If gCMotion.GetAxisState(enmAxis.U2Axis) = Premtek.Base.CommandStatus.Sucessed Then
                        If gCMotion.AxisParameter(enmAxis.U2Axis).MotionStatus <> AxisState.STA_AX_SYNC_MOT Then
                            gCMotion.EmgStop(enmAxis.U2Axis)
                            If gCMotion.AxisResetError(enmAxis.UAxis) = Premtek.Base.CommandStatus.Sucessed Then
                                If gCMotion.AxisResetError(enmAxis.U2Axis) = Premtek.Base.CommandStatus.Sucessed Then
                                    If gDICollection.GetState(enmDI.EMS) = False Then
                                        If gCMotion.SetGantry(enmAxis.UAxis, enmAxis.U2Axis) = CommandStatus.Sucessed Then
                                            gCMotion.AxisParameter(enmAxis.U2Axis).MotionStatus = Nothing
                                            gantry2Error_F = False
                                        ElseIf gantry2Error_F = False Then '2016.06.29 避免Loop存檔衝擊
                                            gSyslog.Save(gMsgHandler.GetMessage(Error_1000004), "Error_1000004", eMessageLevel.Error)
                                            gSYS(eSys.DispStage2).RunStatus = enmRunStatus.Alarm 'Gantry異常, 切斷流程
                                            gSYS(eSys.SubDisp2).RunStatus = enmRunStatus.Alarm 'Gantry異常, 切斷流程
                                            gantry2Error_F = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                If enmAxis.R2Axis <> -1 Then
                    If gCMotion.GetAxisState(enmAxis.R2Axis) = Premtek.Base.CommandStatus.Sucessed Then
                        If gCMotion.AxisParameter(enmAxis.R2Axis).MotionStatus <> AxisState.STA_AX_SYNC_MOT Then
                            gCMotion.EmgStop(enmAxis.R2Axis)
                            If gCMotion.AxisResetError(enmAxis.RAxis) = Premtek.Base.CommandStatus.Sucessed Then
                                If gCMotion.AxisResetError(enmAxis.R2Axis) = Premtek.Base.CommandStatus.Sucessed Then
                                    If gDICollection.GetState(enmDI.EMS2) = False Then
                                        If gCMotion.SetGantry(enmAxis.RAxis, enmAxis.R2Axis) = CommandStatus.Sucessed Then
                                            gCMotion.AxisParameter(enmAxis.R2Axis).MotionStatus = Nothing
                                            gantry3Error_F = False
                                        ElseIf gantry3Error_F = False Then '2016.06.29 避免Loop存檔衝擊
                                            gSyslog.Save(gMsgHandler.GetMessage(Error_1000004), "Error_1000004", eMessageLevel.Error)
                                            gSYS(eSys.DispStage3).RunStatus = enmRunStatus.Alarm 'Gantry異常, 切斷流程
                                            gSYS(eSys.SubDisp3).RunStatus = enmRunStatus.Alarm 'Gantry異常, 切斷流程
                                            gantry3Error_F = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                If enmAxis.O2Axis <> -1 Then
                    If gCMotion.GetAxisState(enmAxis.O2Axis) = Premtek.Base.CommandStatus.Sucessed Then
                        If gCMotion.AxisParameter(enmAxis.O2Axis).MotionStatus <> AxisState.STA_AX_SYNC_MOT Then
                            gCMotion.EmgStop(enmAxis.O2Axis)
                            If gCMotion.AxisResetError(enmAxis.OAxis) = Premtek.Base.CommandStatus.Sucessed Then
                                If gCMotion.AxisResetError(enmAxis.O2Axis) = Premtek.Base.CommandStatus.Sucessed Then
                                    If gDICollection.GetState(enmDI.EMS2) = False Then
                                        If gCMotion.SetGantry(enmAxis.OAxis, enmAxis.O2Axis) = CommandStatus.Sucessed Then
                                            gCMotion.AxisParameter(enmAxis.O2Axis).MotionStatus = Nothing
                                            gantry4Error_F = False
                                        ElseIf gantry4Error_F = False Then '2016.06.29 避免Loop存檔衝擊
                                            gSyslog.Save(gMsgHandler.GetMessage(Error_1000004), "Error_1000004", eMessageLevel.Error)
                                            gSYS(eSys.DispStage4).RunStatus = enmRunStatus.Alarm 'Gantry異常, 切斷流程
                                            gSYS(eSys.SubDisp4).RunStatus = enmRunStatus.Alarm 'Gantry異常, 切斷流程
                                            gantry4Error_F = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                Call gDOCollection.RefreshDO()  '寫入卡片 DO點
                Call gDICollection.RefreshDI()  '抓取卡片 DI點
                Call gAICollection.AI_GetVoltage(CAICollection.AIUpdateMode.Origin) ' 讀出卡片內的AI值
                Call gAOCollection.AO_SetVoltage()
            End If



            '=== 移動到System_Run ===
            'If gUserLevel = enmUserLevel.eSoftwareMaker Then
            '    mSystem.System_OverAll(gSYS(eSys.OverAll))
            'Else
            '[Note]:主控層(LevelNo1端:控制 MachineA、MachineB、 Conveyor)
            System_OverAll(gSYS(eSys.OverAll))

            '[Note]:機台控制層(LevelNo2端:控制 DispStage)
            For mMachineNo As Integer = eSys.MachineA To gSSystemParameter.MachineMax
                System_Machine(gSYS(mMachineNo), mMachineNo)
            Next

            '[Note]:Stage控制層(LevelNo3端:控制底層<Initial、定位、測高、點膠......>)
            For mStageNo As Integer = eSys.DispStage1 To gSSystemParameter.StageMax
                System_DispStage(gSYS(mStageNo))
            Next

            '[Note]:SubDisp控制層(LevelNo4端:控制點膠周邊動作<清膠、除膠、秤重>)
            For mStageNo As Integer = eSys.SubDisp1 To gSSystemParameter.SubDispMax
                System_SubDisp(gSYS(mStageNo))
            Next

            '[Note]:Monitor Stage Status
            For mStageNo As Integer = eSys.MonitorDisp1 To gSSystemParameter.MonitorDispMax
                System_MonitorDisp(gSYS(mStageNo))
            Next

            For Conveyor As Integer = eSys.Conveyor1 To gSSystemParameter.ConveyorMax
                System_Conveyor1(gSYS(Conveyor))
            Next
            'End If

            'TODO:收入System_Conveyor1內
            '檢查hot plate是否Alarm, Alarm 則 Overheat = true
            WetcoConveyor.mGlobalPool.Overheat = WetcoConveyor.mGlobalPool.CheckOverheat(WetcoConveyor.mGlobalPool.SV, WetcoConveyor.mGlobalPool.TempUpperLimit, WetcoConveyor.mGlobalPool.TempLowerLimit)

            If (gSSystemParameter.MachineType = enmMachineType.DCSW_800AQ) Then
                SwLulDelay.Start()
                If (SwLulDelay.ElapsedMilliseconds > 100) Then '減少PLC觸發頻率
                    If (mGlobalPool.cls800AQ_LUL.Loader.IsOpen) Then
                        If (mGlobalPool.cls800AQ_LUL.IsLoaderPass = False) Then
                            mGlobalPool.cls800AQ_LUL.BackgroundLoader()
                        End If
                    End If

                    If (mGlobalPool.cls800AQ_LUL.Unloader.IsOpen) Then
                        If (mGlobalPool.cls800AQ_LUL.IsUnloaderPass = False) Then
                            mGlobalPool.cls800AQ_LUL.BackgroundUnloader()
                        End If
                    End If

                    GetLoaderData()
                    GetUnloaderData()

                    SwLulDelay.Restart()
                End If
            End If

            If IsMachineASafeOpenDoor() = False Then
                If enmDO.DoorLock <> -1 Then
                    gDOCollection.SetState(enmDO.DoorLock, True)
                End If
            End If
            If IsMachineBSafeOpenDoor() = False Then
                If enmDO.DoorLock2 <> -1 Then
                    gDOCollection.SetState(enmDO.DoorLock2, True)
                End If
            End If
            'nowTimer = mStopWatch.ElapsedMilliseconds
            'Debug.Print("Cycle Time: " & nowTimer - oldTimer)
            'oldTimer = nowTimer

            '20161116
            If gSSystemParameter.EnableWeightMeasureA = True Then
                If gSSystemParameter.WeightMeasureA > gSSystemParameter.WeightSet Then
                    gEqpMsg.AddHistoryAlarm("Warn_0000001", "Check Weight A", , "Check A Balance Please", eMessageLevel.Warning)
                End If
            End If
            If gSSystemParameter.EnableWeightMeasureB = True Then
                If gSSystemParameter.WeightMeasureB > gSSystemParameter.WeightSet Then
                    gEqpMsg.AddHistoryAlarm("Warn_0000002", "Check Weight B", , "Check B Balance Please", eMessageLevel.Warning)
                End If
            End If

        End While
    End Sub

    ''' <summary>A機是否安全可開門</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsMachineASafeOpenDoor() As Boolean
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCS_350A
                If WetcoConveyor.Unit Is Nothing Then 'Conveyor物件不存在
                    Return True
                End If
                If WetcoConveyor.Unit.TempController Is Nothing Then '無溫控器
                    Return True
                End If
                If WetcoConveyor.Unit.TempController.arrPidController Is Nothing Then
                    Return True
                End If
                If WetcoConveyor.Unit.TempController.arrPidController.Count = 0 Then
                    Return True
                End If
                If gSSystemParameter.SafeTemperature <= 0 Then '溫度設定不合法
                    Return True
                End If
                If Not WetcoConveyor.Unit.TempController.arrPidController(0) Is Nothing Then
                    If WetcoConveyor.Unit.TempController.arrPidController(0).PV > gSSystemParameter.SafeTemperature Then '如果任一大於安全溫度則不可開門
                        Return False
                    End If
                End If
                Return True
            Case enmMachineType.DCS_F230A
                If WetcoConveyor.Unit Is Nothing Then 'Conveyor物件不存在
                    Return True
                End If
                If WetcoConveyor.Unit.TempController Is Nothing Then '無溫控器
                    Return True
                End If
                If WetcoConveyor.Unit.TempController.arrPidController Is Nothing Then
                    Return True
                End If
                If WetcoConveyor.Unit.TempController.arrPidController.Count = 0 Then
                    Return True
                End If
                If gSSystemParameter.SafeTemperature <= 0 Then '溫度設定不合法
                    Return True
                End If
                If Not WetcoConveyor.Unit.TempController.arrPidController(0) Is Nothing Then
                    If WetcoConveyor.Unit.TempController.arrPidController(0).PV > gSSystemParameter.SafeTemperature Then '如果任一大於安全溫度則不可開門
                        Return False
                    End If
                End If
                Return True
            Case enmMachineType.DCSW_800AQ, enmMachineType.DCS_500AD
                If WetcoConveyor.Unit Is Nothing Then 'Conveyor物件不存在
                    Return True
                End If
                If WetcoConveyor.Unit.TempController Is Nothing Then '無溫控器
                    Return True
                End If
                If WetcoConveyor.Unit.TempController.arrPidController Is Nothing Then
                    Return True
                End If
                If WetcoConveyor.Unit.TempController.arrPidController.Count = 0 Then
                    Return True
                End If
                If gSSystemParameter.SafeTemperature <= 0 Then '溫度設定不合法
                    Return True
                End If
                For i As Integer = 0 To 5 '對每一個溫控器回授值
                    If Not WetcoConveyor.Unit.TempController.arrPidController(i) Is Nothing Then
                        'If WetcoConveyor.Unit.TempController.arrPidController(i).PV > gSSystemParameter.SafeTemperature Then '如果任一大於安全溫度則不可開門
                        '    Return False
                        'End If
                        Dim num As Double = Unit.TempController.GetPidController(i)
                        '20161010
                        If Unit.TempController.GetPidController(i) > gSSystemParameter.SafeTemperature Then '如果任一大於安全溫度則不可開門
                            Return False
                        End If
                    End If
                Next
                Return True
            Case enmMachineType.eDTS_2S2V
                '尚無此機型,待補
        End Select
        Return True
    End Function

    ''' <summary>B機是否安全可開門</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsMachineBSafeOpenDoor() As Boolean
        If gSSystemParameter.MachineType <> enmMachineType.DCSW_800AQ Then '機型不符略
            Return True
        End If
        If WetcoConveyor.Unit Is Nothing Then 'Conveyor物件不存在
            Return True
        End If
        If WetcoConveyor.Unit.TempController Is Nothing Then '無溫控器
            Return True
        End If
        If WetcoConveyor.Unit.TempController.arrPidController Is Nothing Then
            Return True
        End If
        If WetcoConveyor.Unit.TempController.arrPidController.Count = 0 Then
            Return True
        End If
        If gSSystemParameter.SafeTemperature <= 0 Then '溫度設定不合法
            Return True
        End If
        For i As Integer = 6 To 11 '對每一個溫控器回授值
            If Not WetcoConveyor.Unit.TempController.arrPidController(i) Is Nothing Then
                'If WetcoConveyor.Unit.TempController.arrPidController(i).PV > gSSystemParameter.SafeTemperature Then '如果任一大於安全溫度則不可開門
                '    Return False
                'End If

                '20161010
                If Unit.TempController.GetPidController(i) > gSSystemParameter.SafeTemperature Then '如果任一大於安全溫度則不可開門
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    ''' <summary>
    ''' Loader 資料接收
    ''' </summary>
    Private Sub GetLoaderData()
        If (mGlobalPool.cls800AQ_LUL.IsLoaderPass = False) Then

            If (cls800AQ_LUL.Loader.IsMappingFinish) Then
                If Not (mGlobalPool.cls800AQ_LUL.IsLoaderBusy) Then
                    mGlobalPool.cls800AQ_LUL.GetCastetData(cls800AQLul.enmDevice.Loader)
                End If
            End If

            If (cls800AQ_LUL.Loader.IsCaseteBarCodeReady) Then
                If Not (mGlobalPool.cls800AQ_LUL.IsLoaderBusy) Then
                    mGlobalPool.cls800AQ_LUL.GetCaseteBarCode()
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' Unloader 資料接收
    ''' </summary>
    Private Sub GetUnloaderData()
        If (mGlobalPool.cls800AQ_LUL.IsUnloaderPass = False) Then

            If (cls800AQ_LUL.Unloader.IsMappingFinish) Then
                If Not (mGlobalPool.cls800AQ_LUL.IsUnloaderBusy) Then
                    mGlobalPool.cls800AQ_LUL.GetCastetData(cls800AQLul.enmDevice.Unloader)
                End If
            End If

        End If
    End Sub
  
End Class


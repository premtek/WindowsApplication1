Imports ProjectCore
Imports ProjectMotion
Imports ProjectIO
Imports WetcoConveyor

Public Class CActionConveyorF230A

    ''' <summary>復歸逾時計時器</summary>
    ''' <remarks></remarks>
    Dim mHomeStopWatch As New Stopwatch

    ''' <summary>
    ''' [A機] 是否為真空建立完成
    ''' </summary>
    Public ReadOnly Property IsChuckAVacuumReady As Boolean
        Get
            If (gDICollection.GetState(enmDI.Station2ChuckVacuumReady, Unit.A_Vacuum(0)) = Unit.A_Vacuum(0) AndAlso _
                gDICollection.GetState(enmDI.Station2ChuckVacuumReady2, Unit.A_Vacuum(1)) = Unit.A_Vacuum(1) AndAlso _
                gDICollection.GetState(enmDI.Station2ChuckVacuumReady3, Unit.A_Vacuum(2)) = Unit.A_Vacuum(2) AndAlso _
                gDICollection.GetState(enmDI.Station2ChuckVacuumReady4, Unit.A_Vacuum(3)) = Unit.A_Vacuum(3) AndAlso _
                gDICollection.GetState(enmDI.Station2ChuckVacuumReady5, Unit.A_Vacuum(4)) = Unit.A_Vacuum(4) AndAlso _
                gDICollection.GetState(enmDI.Station2ChuckVacuumReady6, Unit.A_Vacuum(5)) = Unit.A_Vacuum(5)) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Dim SwTimeOut As New Stopwatch

    ''' <summary>皮帶減速時間</summary>
    ''' <remarks></remarks>
    Public RollerSlowTime As UInteger = 2000

    ''' <summary>ConveyorA機復歸動作</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Public Sub HomePartA(ByRef sys As sSysParam)
        With sys
            Select Case .SysNum
                Case 1000
                    mHomeStopWatch.Start()

                    'Roller 停止
                    gDOCollection.SetState(enmDO.MoveInMotorCW, False)
                    gDOCollection.SetState(enmDO.MoveInMotorSlow, False)
                    .SysNum = 1100

                Case 1100 '破真空
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum, False)  '吸真空OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, True)  '吹氣ON

                    mHomeStopWatch.Restart()
                    .SysNum = 1200

                Case 1200 '破真空判斷
                    If (IsChuckAVacuumReady = False) Then
                        gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, False)  '吹氣ON

                        mHomeStopWatch.Restart()
                        .SysNum = 1300

                    ElseIf (IsTimeOut(mHomeStopWatch, gSSystemParameter.TimeOut4)) Then
                        'Sue0710
                        'A機 真空關閉逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083120", "CActionConveyorF230A_HomePartA", .SysNum, gMsgHandler.GetMessage(Alarm_2083120), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1300 '頂升汽缸下降/Stoper上升/料盤逼緊缸:放
                    gDOCollection.SetState(enmDO.HeaterCylinderUp1, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown1, True)
                    gDOCollection.SetState(enmDO.Station2StopperUp, True)
                    gDOCollection.SetState(enmDO.Station2StopperDown, False)
                    gDOCollection.SetState(enmDO.TrayClamperOn, False) '料盤逼緊缸:夾
                    gDOCollection.SetState(enmDO.TrayClamperOff, True) '料盤逼緊缸:放

                    mHomeStopWatch.Restart()
                    .SysNum = 1400

                Case 1400 '頂升汽缸是否已在下位 / Stoper是否已在上位 / 定位汽缸是否到位
                    Dim CylinderDownReady As Boolean = gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady, True)
                    Dim StopperUpReady As Boolean = gDICollection.GetState(enmDI.Station2StopperUpReady, True)
                    Dim CylinderOFF As Boolean = gDICollection.GetState(enmDI.TrayClamperOffReady, True) '料盤逼緊缸:"放"定位檢知

                    If (CylinderDownReady AndAlso StopperUpReady AndAlso CylinderOFF) Then
                        mHomeStopWatch.Restart()
                        .SysNum = 9000
                    ElseIf (IsTimeOut(mHomeStopWatch, gSSystemParameter.TimeOut4)) Then
                        'Sue0710
                        'Conveyor1 安全狀態確認逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083122", "CActionConveyorF230A_HomePartA", .SysNum, gMsgHandler.GetMessage(Alarm_2083122), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 9000
                    mHomeStopWatch.Reset()
                    sys.RunStatus = enmRunStatus.Finish

            End Select
        End With
    End Sub

    ''' <summary>A機進料</summary>
    ''' <param name="sys"></param>
    ''' <param name="auto"></param>
    ''' <remarks></remarks>
    Public Sub LoadA(ByRef sys As sSysParam, ByVal auto As Boolean)
        With sys
            Select Case .SysNum
                Case 1000 '起始 : 檢查到位Sensor是否在安全狀態
                    If gDICollection.GetState(enmDI.Station2TrayReady, True) = True Then
                        'Sue0710
                        'A機上有產品
                        gEqpMsg.AddHistoryAlarm("Alarm_2083110", "CActionConveyorF230A_LoadA", .SysNum, gMsgHandler.GetMessage(Alarm_2083110), eMessageLevel.Warning)
                        .SysNum = 9000
                    Else
                        SwTimeOut.Start()
                        .SysNum = 1040
                    End If

                Case 1040 '氣缸下降/Stoper上升/料盤逼緊缸:放
                    gDOCollection.SetState(enmDO.HeaterCylinderUp1, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown1, True)
                    gDOCollection.SetState(enmDO.Station2StopperUp, True)
                    gDOCollection.SetState(enmDO.Station2StopperDown, False)
                    gDOCollection.SetState(enmDO.TrayClamperOn, False) '料盤逼緊缸:夾
                    gDOCollection.SetState(enmDO.TrayClamperOff, True) '料盤逼緊缸:放

                    SwTimeOut.Restart()
                    .SysNum = 1100

                Case 1100 '頂升汽缸是否已在下位 / Stoper是否已在上位 / 定位汽缸是否到位
                    Dim CylinderDownReady As Boolean = gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady, True)
                    Dim StopperUpReady As Boolean = gDICollection.GetState(enmDI.Station2StopperUpReady, True)
                    Dim CylinderOFF As Boolean = gDICollection.GetState(enmDI.TrayClamperOffReady, True) '料盤逼緊缸:"放"定位檢知

                    If CylinderDownReady AndAlso StopperUpReady AndAlso CylinderOFF Then
                        SwTimeOut.Restart()
                        .SysNum = 1120
                    ElseIf IsTimeOut(SwTimeOut, gSSystemParameter.TimeOut4) Then
                        'Sue0710
                        'Conveyor1 安全狀態確認逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083122", "CActionConveyorF230A_LoadA", .SysNum, gMsgHandler.GetMessage(Alarm_2083122), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1120 'Loader是否可供料
                    If (gDICollection.GetState(enmDI.BoardAvailable, True) Or auto = False) Then
                        SwTimeOut.Restart()
                        .SysNum = 1200
                    ElseIf IsTimeOut(SwTimeOut, gSSystemParameter.TimeOut4) Then
                        'Sue0710
                        'Conveyor1 A機進料異常
                        gEqpMsg.AddHistoryAlarm("Alarm_2083003", "CActionConveyorF230A_LoadA", .SysNum, gMsgHandler.GetMessage(Alarm_2083003), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1200 'Roller ON
                    'If gDICollection.GetState(enmDI.MoveInMotorAlarm, False) <> True Then 'Conveyor皮帶傳輸動力 Alarm (B接點?)
                    If auto Then
                        gDOCollection.SetState(enmDO.MachineReadyToRecieve, True) '開啟要料訊號
                    End If
                    gDOCollection.SetState(enmDO.MoveInMotorCW, True)

                    SwTimeOut.Restart()
                    .SysNum = 1220
                    'Else
                    '    gEqpMsg.AddHistoryAlarm("", "CActionConveyorF230A_LoadA", .SysNum, "Conveyor皮帶傳輸動力 Alarm", eMessageLevel.Warning)
                    '     sys.RunStatus = enmRunStatus.Alarm
                    'End If

                Case 1220 'Roller減速
                    Dim mSec As UInteger = 0 '減速delay時間, 半自動狀態下mSec = 0
                    If auto Then
                        mSec = RollerSlowTime
                    End If
                    If IsTimeOut(SwTimeOut, mSec) Then
                        gDOCollection.SetState(enmDO.MoveInMotorSlow, True)
                        SwTimeOut.Restart()
                        .SysNum = 1300
                    End If

                Case 1300 '檢查到位Sensor / 關閉要料訊號
                    If gDICollection.GetState(enmDI.Station2TrayReady, True) Then
                        gDOCollection.SetState(enmDO.MachineReadyToRecieve, False)
                        SwTimeOut.Restart()
                        .SysNum = 1400
                        '20170620
                    ElseIf IsTimeOut(SwTimeOut, gSSystemParameter.TimeOut4) Then
                        'Sue0710
                        'Conveyor1 到位感測器觸發逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083123", "CActionConveyorF230A_LoadA", .SysNum, gMsgHandler.GetMessage(Alarm_2083123), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1400 'Roller Stop
                    gDOCollection.SetState(enmDO.MoveInMotorCW, False)
                    gDOCollection.SetState(enmDO.MoveInMotorSlow, False)
                    SwTimeOut.Restart()
                    .SysNum = 1500

                Case 1500 '頂升汽缸上升
                    gDOCollection.SetState(enmDO.HeaterCylinderDown1, False) '頂升汽缸下降
                    gDOCollection.SetState(enmDO.HeaterCylinderUp1, True) '頂升汽缸上升
                    SwTimeOut.Restart()
                    .SysNum = 1600

                Case 1600  '頂升汽缸上升到位檢查
                    If gDICollection.GetState(enmDI.Station2Heater1CylinderUpReady, True) Then
                        SwTimeOut.Restart()
                        .SysNum = 1700
                    ElseIf IsTimeOut(SwTimeOut, gSSystemParameter.TimeOut4) Then
                        'Sue0710
                        'A機 氣壓缸上升逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083118", "CActionConveyorF230A_LoadA", .SysNum, gMsgHandler.GetMessage(Alarm_2083118), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1700 '料盤逼緊缸夾
                    gDOCollection.SetState(enmDO.TrayClamperOff, False) '料盤逼緊缸:放
                    gDOCollection.SetState(enmDO.TrayClamperOn, True) '料盤逼緊缸:夾
                    SwTimeOut.Restart()
                    .SysNum = 1800

                Case 1800 '料盤逼緊缸:"夾"定位檢知
                    If gDICollection.GetState(enmDI.TrayClamperOnReady, True) Then
                        SwTimeOut.Restart()
                        .SysNum = 1900
                    ElseIf IsTimeOut(SwTimeOut, gSSystemParameter.TimeOut4) Then
                        'Sue0710
                        'Conveyor1料盤逼緊缸預備逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083131", "CActionConveyorF230A_LoadA", .SysNum, gMsgHandler.GetMessage(Alarm_2083131), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1900 '真空開啟
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, False)  '吹氣OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum, Unit.A_Vacuum(0))   '吸真空ON
                    SwTimeOut.Restart()
                    .SysNum = 2000

                Case 2000 '真空檢查
                    If IsChuckAVacuumReady Then
                        SwTimeOut.Restart()
                        .SysNum = 9000
                    ElseIf IsTimeOut(SwTimeOut, gSSystemParameter.TimeOut4) Then

                        'Sue0710
                        'Conveyor1 真空逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083108", "CActionConveyorF230A_LoadA", .SysNum, gMsgHandler.GetMessage(Alarm_2083108), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 9000
                    SwTimeOut.Reset()
                    sys.RunStatus = enmRunStatus.Finish

            End Select
        End With

    End Sub

    ''' <summary>
    ''' A機退料
    ''' </summary>
    ''' <param name="sys"></param>
    ''' <param name="auto"></param>
    ''' <remarks></remarks>
    Public Sub UnloadA(ByRef sys As sSysParam, ByVal auto As Boolean)
        With sys
            Select Case .SysNum
                Case 1000 '檢查是否有料片在機台上
                    If (gDICollection.GetState(enmDI.Station2TrayReady, False) = False) Then
                        'Sue0710
                        'Conveyor1 沒有料片在機台上
                        gEqpMsg.AddHistoryAlarm("Warn_3024001", "CActionConveyorF230A_UnloadA", .SysNum, gMsgHandler.GetMessage(EqpID.Warn_3024001), eMessageLevel.Warning)
                        .SysNum = 9000
                    Else
                        .SysNum = 1100
                    End If

                Case 1100 'Unloader是否可收料
                    gDOCollection.SetState(enmDO.BoardAvailable, True) '開啟出料訊號
                    If (gDICollection.GetState(enmDI.MachineReadyToRecieve, True) Or auto = False) Then
                        gDOCollection.SetState(enmDO.BoardAvailable, False) '出料訊號 OFF
                        gDOCollection.SetState(enmDO.MoveInMotorCW, False)
                        gDOCollection.SetState(enmDO.MoveInMotorSlow, False)
                        SwTimeOut.Restart()
                        .SysNum = 1200
                    ElseIf IsTimeOut(SwTimeOut, gSSystemParameter.TimeOut4) Then
                        'Sue0710
                        'SMEMA 載出預備逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2080013", "CActionConveyorF230A_UnloadA", .SysNum, gMsgHandler.GetMessage(Alarm_2080013), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1200 '料盤逼緊缸放
                    gDOCollection.SetState(enmDO.TrayClamperOn, False) '料盤逼緊缸:夾
                    gDOCollection.SetState(enmDO.TrayClamperOff, True) '料盤逼緊缸:放
                    SwTimeOut.Restart()
                    .SysNum = 1220

                Case 1220   '料盤逼緊缸:"放"定位檢知
                    If gDICollection.GetState(enmDI.TrayClamperOffReady, True) Then
                        SwTimeOut.Restart()
                        .SysNum = 1300
                    ElseIf IsTimeOut(SwTimeOut, gSSystemParameter.TimeOut4) Then
                        'Sue0710
                        'Conveyor1 料盤逼緊缸夾開啟逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083125", "CActionConveyorF230A_UnloadA", .SysNum, gMsgHandler.GetMessage(Alarm_2083125), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1300 '破真空
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum, False)  '吸真空OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, True)   '吹氣ON
                    SwTimeOut.Restart()
                    .SysNum = 1320

                Case 1320 '檢查是否已破真空
                    If IsTimeOut(SwTimeOut, 300) Then
                        If (gDICollection.GetState(enmDI.Station2ChuckVacuumReady, False) = False) Then '已破真空
                            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, False)   '吹氣OFF

                            SwTimeOut.Restart()
                            .SysNum = 1400
                        End If
                    ElseIf IsTimeOut(SwTimeOut, gSSystemParameter.TimeOut4) Then
                        'Sue0710
                        'A機真空關閉逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083120", "CActionConveyorF230A_UnloadA", .SysNum, gMsgHandler.GetMessage(Alarm_2083120), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1400 '頂升汽缸下降
                    gDOCollection.SetState(enmDO.HeaterCylinderUp1, False) '頂升汽缸上升
                    gDOCollection.SetState(enmDO.HeaterCylinderDown1, True) '頂升汽缸下降
                    SwTimeOut.Restart()
                    .SysNum = 1420

                Case 1420  '頂升汽缸下降到位檢查
                    If gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady, True) Then
                        SwTimeOut.Restart()
                        .SysNum = 1500
                        'If (auto) Then
                        '    .SysNum = 1500
                        'Else
                        '    .SysNum = 9000
                        'End If
                    ElseIf IsTimeOut(SwTimeOut, gSSystemParameter.TimeOut4) Then
                        'Sue0710
                        'A機 氣壓缸上升逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083118", "CActionConveyorF230A_LoadA", .SysNum, gMsgHandler.GetMessage(Alarm_2083118), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1500 '檢查是否有料片在機台上
                    If (gDICollection.GetState(enmDI.Station2TrayReady, False) = False) Then
                        'Sue0710
                        'Conveyor1 沒有料片在機台上
                        gEqpMsg.AddHistoryAlarm("Warn_3024001", "CActionConveyorF230A_UnloadA", .SysNum, gMsgHandler.GetMessage(Warn_3024001), eMessageLevel.Warning)
                        .SysNum = 9000
                    End If
                    SwTimeOut.Start()
                    .SysNum = 1600

                Case 1600 'Stoper 下降
                    gDOCollection.SetState(enmDO.Station2StopperUp, False)
                    gDOCollection.SetState(enmDO.Station2StopperDown, True)
                    SwTimeOut.Start()
                    .SysNum = 1620

                Case 1620 '檢查Stoper是否在下位
                    If (gDICollection.GetState(enmDI.Station2StopperDownReady, True)) Then
                        SwTimeOut.Restart()
                        .SysNum = 1700
                    ElseIf IsTimeOut(SwTimeOut, gSSystemParameter.TimeOut4) Then
                        'Sue0710
                        'Conveyor1 阻擋缸下降逾時
                        gEqpMsg.AddHistoryAlarm("Alarm_2083127", "CActionConveyorF230A_UnloadA", .SysNum, gMsgHandler.GetMessage(Alarm_2083127), eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If

                Case 1700 'Roller Start
                    gDOCollection.SetState(enmDO.MoveInMotorCW, True)
                    gDOCollection.SetState(enmDO.MoveInMotorSlow, False)
                    SwTimeOut.Restart()
                    .SysNum = 1800

                Case 1800 'Unloader是否收到料
                    If (auto = False) Then
                        'If (IsTimeOut(SwTimeOut, 5000)) Then
                        '20170620
                        If (IsTimeOut(SwTimeOut, gSSystemParameter.UnLoad230WaitProductOut)) Then
                            SwTimeOut.Restart()
                            .SysNum = 9000
                        End If
                    ElseIf (gDICollection.GetState(enmDI.MachineReadyToRecieve, False) = False) Then
                        gDOCollection.SetState(enmDO.MoveInMotorCW, False)
                        gDOCollection.SetState(enmDO.MoveInMotorSlow, False)
                        SwTimeOut.Restart()
                        .SysNum = 9000
                    End If

                Case 9000
                    SwTimeOut.Reset()
                    sys.RunStatus = enmRunStatus.Finish

            End Select
        End With
    End Sub


End Class

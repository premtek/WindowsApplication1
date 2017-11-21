Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectTriggerBoard

''' <summary>
''' Purge對外流程物件接口
''' </summary>
''' <remarks></remarks>
Public Class CActionPurge

#Region "內部參數"
    ''' <summary>內部配接子系統</summary>
    ''' <remarks></remarks>
    Dim mSYS(enmStage.Max) As sSysParam

    ''' <summary>
    ''' 擦膠形式
    ''' </summary>
    ''' <remarks></remarks>
    Dim mCleanType(enmStage.Max) As eCleanType

    ''' <summary>Purge杯馬達座標</summary>
    ''' <remarks></remarks>
    Dim mPurgePosX(enmStage.Max) As Decimal
    ''' <summary>Purge杯馬達座標</summary>
    ''' <remarks></remarks>
    Dim mPurgePosY(enmStage.Max) As Decimal
    ''' <summary>Purge杯馬達座標</summary>
    ''' <remarks></remarks>
    Dim mPurgePosZ(enmStage.Max) As Decimal

    ''' <summary>擦拭位置</summary>
    ''' <remarks></remarks>
    Dim mClearGluePosX(enmStage.Max) As Decimal
    ''' <summary>擦拭位置</summary>
    ''' <remarks></remarks>
    Dim mClearGluePosY(enmStage.Max) As Decimal
    ''' <summary>擦拭位置</summary>
    ''' <remarks></remarks>
    Dim mClearGluePosZ(enmStage.Max) As Decimal

    ''' <summary>計時器開始時間</summary>
    ''' <remarks></remarks>
    Dim mStopWatchStart(enmStage.Max) As Decimal

    ''' <summary>
    ''' 共用計時器
    ''' </summary>
    ''' <remarks></remarks>
    Dim mStopWatch(enmStage.Max) As Stopwatch

    ''' <summary>紀錄動作用閥號</summary>
    ''' <remarks></remarks>
    Dim mValveNo(enmStage.Max) As enmValve
    ''' <summary>紀錄動作用閥參數</summary>
    ''' <remarks></remarks>
    Dim mValveName(enmStage.Max) As String
    ''' <summary>
    ''' 觸發板用資料
    ''' </summary>
    ''' <remarks></remarks>
    Dim mCyleParam(enmStage.Max) As sTriggerTPCmdParam
    ''' <summary>
    ''' [已經做過Purge了]
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPurgeDone(enmStage.Max) As Boolean
    ''' <summary>
    ''' Purge時間紀錄
    ''' </summary>
    ''' <remarks></remarks>
    Dim mPurgeTime(enmStage.Max) As Decimal
    ''' <summary>
    ''' 錯誤訊息
    ''' </summary>
    ''' <remarks></remarks>
    Dim mErrorMessage(enmStage.Max) As String

    '20170620
    Private mTriggerCmdFailCount(enmStage.Max) As Integer                               '[紀錄資料傳輸異常次數]

#End Region

#Region "Ctor/Dtor"
    ''' <summary>
    ''' 建構子Ctor/Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        For mStageNo As Integer = enmStage.No1 To enmStage.Max
            mStopWatch(mStageNo) = New Stopwatch
            mSYS(mStageNo) = New sSysParam
        Next
    End Sub

    ''' <summary>
    ''' 解構子 Dtor/Destructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ManualDispose()
        For mStageNo As Integer = enmStage.No1 To enmStage.Max
            mStopWatch(mStageNo).Stop()
            mStopWatch(mStageNo) = Nothing
            mSYS(mStageNo) = Nothing
            mCyleParam(mStageNo) = Nothing
        Next
    End Sub

#End Region

#Region "Private Function"
    ''' <summary>簡易ALID對照</summary>
    ''' <param name="stage"></param>
    ''' <param name="ALID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetStageALID(ByVal stage As enmStage, ByVal ALID As Integer) As Integer
        Select Case stage
            Case enmStage.No1
                Return ALID
            Case enmStage.No2
                Return ALID + 12000
            Case enmStage.No3
                Return ALID + 30000
            Case enmStage.No4
                Return ALID + 37000
            Case Else
                Return ALID
        End Select
    End Function
    ''' <summary>簡易ALID對照</summary>
    ''' <param name="valve"></param>
    ''' <param name="ALID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetValveALID(ByVal valve As enmValve, ByVal ALID As Integer) As Integer
        Select Case valve
            Case enmValve.No1
                Return ALID
            Case enmValve.No2
                Return ALID + 100
            Case enmValve.No3
                Return ALID + 200
            Case enmValve.No4
                Return ALID + 300
            Case Else
                Return ALID
        End Select
    End Function

    ''' <summary>設定XYZB 四軸速度加減速</summary>
    ''' <param name="sys"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetXYZBVelocity(ByRef sys As sSysParam) As enmRunStatus
        Const mFunctionName As String = "SetXYZBVelocity"
        With gCMotion.AxisParameter(sys.AxisX).Velocity
            If gCMotion.SetVelLow(sys.AxisX, .VelLow) <> CommandStatus.Sucessed Then
                gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030014), eMessageLevel.Error, sys.SysNum)

                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                sys.RunStatus = enmRunStatus.Alarm
                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
            End If
            If gCMotion.SetVelHigh(sys.AxisX, .VelHigh) <> CommandStatus.Sucessed Then
                gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030013), eMessageLevel.Error, sys.SysNum)

                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                sys.RunStatus = enmRunStatus.Alarm
                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
            End If
            If gCMotion.SetAcc(sys.AxisX, .Acc * .AccRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030011), eMessageLevel.Error, sys.SysNum)

                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                sys.RunStatus = enmRunStatus.Alarm
                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
            End If
            If gCMotion.SetDec(sys.AxisX, .Dec * .DecRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030012), eMessageLevel.Error, sys.SysNum)

                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                sys.RunStatus = enmRunStatus.Alarm
                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
            End If
        End With

        With gCMotion.AxisParameter(sys.AxisY).Velocity
            If gCMotion.SetVelLow(sys.AxisY, .VelLow) <> CommandStatus.Sucessed Then
                gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031014), eMessageLevel.Error, sys.SysNum)

                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                sys.RunStatus = enmRunStatus.Alarm
                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
            End If
            If gCMotion.SetVelHigh(sys.AxisY, .VelHigh) <> CommandStatus.Sucessed Then
                gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031013), eMessageLevel.Error, sys.SysNum)

                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                sys.RunStatus = enmRunStatus.Alarm
                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
            End If
            If gCMotion.SetAcc(sys.AxisY, .Acc * .AccRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031011), eMessageLevel.Error, sys.SysNum)

                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                sys.RunStatus = enmRunStatus.Alarm
                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
            End If
            If gCMotion.SetDec(sys.AxisY, .Dec * .DecRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031012), eMessageLevel.Error, sys.SysNum)

                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                sys.RunStatus = enmRunStatus.Alarm
                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
            End If
        End With

        With gCMotion.AxisParameter(sys.AxisZ).Velocity
            If gCMotion.SetVelLow(sys.AxisZ, .VelLow) <> CommandStatus.Sucessed Then
                gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032014), eMessageLevel.Error, sys.SysNum)

                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                sys.RunStatus = enmRunStatus.Alarm
                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
            End If
            If gCMotion.SetVelHigh(sys.AxisZ, .VelHigh) <> CommandStatus.Sucessed Then
                gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032013), eMessageLevel.Error, sys.SysNum)

                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                sys.RunStatus = enmRunStatus.Alarm
                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
            End If
            If gCMotion.SetAcc(sys.AxisZ, .Acc * .AccRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032011), eMessageLevel.Error, sys.SysNum)

                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                sys.RunStatus = enmRunStatus.Alarm
                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
            End If
            If gCMotion.SetDec(sys.AxisZ, .Dec * .DecRatio) <> CommandStatus.Sucessed Then
                gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032012), eMessageLevel.Error, sys.SysNum)

                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                sys.RunStatus = enmRunStatus.Alarm
                Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
            End If
        End With

        '20161130
        '[說明]:Tilt軸
        If sys.AxisB <> -1 Then
            With gCMotion.AxisParameter(sys.AxisB).Velocity
                If gCMotion.SetVelLow(sys.AxisB, .VelLow) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1034014), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                If gCMotion.SetVelHigh(sys.AxisB, .VelHigh) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1034013), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                If gCMotion.SetAcc(sys.AxisB, .Acc * .AccRatio) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1034011), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                If gCMotion.SetDec(sys.AxisB, .Dec * .DecRatio) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1034012), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
            End With
        End If
        Return enmRunStatus.Finish
    End Function

    '20170620
    ''' <summary>
    ''' 純Purge子流程
    ''' </summary>
    ''' <param name="sys"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Purge(ByRef sys As sSysParam) As enmRunStatus 'Soni +2017.01.20 補傳回值格式
        Const mFunctionName As String = "PurgeVacuumClean"
        Select Case sys.SysNum
            Case sSysParam.SysLoopStart

                mValveNo(sys.StageNo) = sys.SelectValve '紀錄選取得閥 避免暫停中斷等影響
                mValveName(sys.StageNo) = gCRecipe.StageParts(sys.StageNo).ValveName(mValveNo(sys.StageNo))

                If Not gJetValveDB.ContainsKey(mValveName(sys.StageNo)) Then '閥設定檔不存在
                    gEqpMsg.Add(mFunctionName, Error_1002006, eMessageLevel.Error, sys.SysNum)
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

                '[說明]:關閉出膠
                Call SetDispensingTrigger(sys.StageNo, mValveNo(sys.StageNo), enmONOFF.eOff)
                '[說明]:設定第二次Purge不出膠,只推壓力,吸附舊膠
                mPurgeDone(sys.StageNo) = False

                '[說明]:設定DispenserNo1 DispenserNo2之出膠壓力
                gEPVCollection.SetValue(sys.StageNo, mValveNo(sys.StageNo), eEPVPressureType.Syringe, gCRecipe.StageParts(sys.StageNo).SyringePressure(mValveNo(sys.StageNo)), False)
                'gSysAdapter.SetSyringeAirPressure(sys.StageNo, mValveNo(sys.StageNo), gCRecipe.StageParts(sys.StageNo).SyringePressure(mValveNo(sys.StageNo)))
                '20170720 _改單位為SEC *1000
                mPurgeTime(sys.StageNo) = gSSystemParameter.StageParts.ValveData(sys.StageNo).PurgeTime(mValveNo(sys.StageNo)) * 1000
                sys.SysNum = 1100

            Case 1100
                '[說明]:設定移動速度
                If SetXYZBVelocity(sys) = enmRunStatus.Alarm Then
                    Return enmRunStatus.Alarm
                End If
                sys.SysNum = 1200

            Case 1200
                '[說明]:移動Z軸至Up位置
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                sys.SysNum = 1300

            Case 1300
                If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If

                If sys.AxisB <> -1 Then
                    sys.SysNum = 1310
                Else
                    sys.SysNum = 1400
                End If

            Case 1310
                '2016 1130 Tilt 轉正
                If gCMotion.AbsMove(sys.AxisB, GetSysParamTilePos(sys)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1034000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                sys.SysNum = 1320

            Case 1320
                If gCMotion.MotionDone(sys.AxisB) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisB) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1034004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If
                sys.SysNum = 1400

            Case 1400
                '[說明]:移動到Purge杯之位置
                mPurgePosX(sys.StageNo) = gSSystemParameter.Pos.PurgeCalibration(sys.StageNo).ValvePosX(mValveNo(sys.StageNo))
                mPurgePosY(sys.StageNo) = gSSystemParameter.Pos.PurgeCalibration(sys.StageNo).ValvePosY(mValveNo(sys.StageNo))
                If gCMotion.AbsMove(sys.AxisX, mPurgePosX(sys.StageNo)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

                If gCMotion.AbsMove(sys.AxisY, mPurgePosY(sys.StageNo)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                sys.SysNum = 1500

            Case 1500
                '[說明]:檢查停止
                If gCMotion.MotionDone(sys.AxisX) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030004), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If
                If gCMotion.MotionDone(sys.AxisY) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisY) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031004), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If

                Call ValveCylinderAction(sys.StageNo, mValveNo(sys.StageNo), enmUpDown.Down, mStopWatch(sys.StageNo))
                mStopWatch(sys.StageNo).Restart()
                sys.SysNum = 1550

            Case 1550
                '[說明]:Check Cylinder Sensor
                If ValveCylinderSensor(sys.StageNo, mValveNo(sys.StageNo), enmUpDown.Down) = True Then
                    mStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 1600
                ElseIf IsTimeOut(mStopWatch(sys.StageNo), gSSystemParameter.StableTime.CheckSensorTimeout) = True Then
                    gEqpMsg.Add(mFunctionName, Alarm_2004001, eMessageLevel.Error, sys.SysNum) '[Cylinder Up Down Sensor Alarm]
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

            Case 1600
                '[說明]:Z軸移至Purge高度
                mPurgePosZ(sys.StageNo) = gSSystemParameter.Pos.PurgeCalibration(sys.StageNo).ValvePosZ(mValveNo(sys.StageNo))
                If gCMotion.AbsMove(sys.AxisZ, mPurgePosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                sys.SysNum = 1700

            Case 1700
                '[說明]:檢查停止
                If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If

                sys.SysNum = 1750

            Case 1750
                '[說明]:移至Purge杯上後等待多久才開始開Pump

                '[說明]:在Purge前開啟膠桶壓力，Purge完膠就關閉
                gSysAdapter.SetSyringePressure(sys.StageNo, mValveNo(sys.StageNo), enmONOFF.eON)

                If mPurgeDone(sys.StageNo) = False Then '第一輪 要寫入參數
                    sys.SysNum = 1760
                Else
                    sys.SysNum = 1795
                End If

            Case 1760
                '[說明]:出膠  20161112

                '[說明]:設定Jetting Valve Trigger Controller 為固定頻率打點模式
                '==========================================================================================   20161112
                'Trigger卡新的交握方式-Mobary

                If gJetValveDB.ContainsKey(mValveName(sys.StageNo)) = True Then
                    Select Case gJetValveDB(mValveName(sys.StageNo)).ValveModel
                        Case eValveModel.PicoPulse
                            mCyleParam(mValveNo(sys.StageNo)).CycleTime = gJetValveDB(mValveName(sys.StageNo)).PicoTouch.CycleTime * 1000
                        Case eValveModel.Advanjet
                            mCyleParam(mValveNo(sys.StageNo)).CycleTime = gJetValveDB(mValveName(sys.StageNo)).Advanjet.CycleTime * 1000
                    End Select
                Else
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1002006), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                '20161221
                '[Note]:參數需要補
                mCyleParam(mValveNo(sys.StageNo)).DotCounts = 65534
                mCyleParam(mValveNo(sys.StageNo)).JetPressure = 0
                mCyleParam(mValveNo(sys.StageNo)).GluePressure = 0
                Select Case gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).ValveModel
                    Case eValveModel.Advanjet
                        mCyleParam(mValveNo(sys.StageNo)).CycleTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).Advanjet.CycleTime * 1000)
                        mCyleParam(mValveNo(sys.StageNo)).PulseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).Advanjet.PulseTime * 1000)

                    Case eValveModel.PicoPulse
                        mCyleParam(mValveNo(sys.StageNo)).CycleTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CycleTime * 1000)
                        mCyleParam(mValveNo(sys.StageNo)).PulseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.ValveOnTime * 1000)
                        mCyleParam(mValveNo(sys.StageNo)).OpenTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.OpenTime * 1000)
                        mCyleParam(mValveNo(sys.StageNo)).CloseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CloseTime * 1000)
                        mCyleParam(mValveNo(sys.StageNo)).Stroke = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.Stroke
                        mCyleParam(mValveNo(sys.StageNo)).CloseVoltage = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CloseVoltage
                End Select
                mStopWatch(mValveNo(sys.StageNo)).Restart() '逾時計時
                sys.SysNum = 1770

                '20160620
            Case 1770
                'If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                If gTriggerBoard.SetResetAlarm(sys.StageNo, False) = True Then
                    mStopWatch(mValveNo(sys.StageNo)).Restart() '逾時計時
                    sys.SysNum = 1775
                End If
                'ElseIf IsTimeOut(mStopWatch(mValveNo(sys.StageNo)), gSSystemParameter.TimeOut1) Then
                'gEqpMsg.Add(mFunctionName, GetValveALID(mValveNo(sys.StageNo), Error_1016002), eMessageLevel.Error, sys.SysNum)

                'mErrorMessage(sys.StageNo) = "Trigger Board is Busy TimeOut!!"
                'gSyslog.Save(mErrorMessage(sys.StageNo))
                'sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                'sys.RunStatus = enmRunStatus.Alarm
                'Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                'End If
                '20170620
            Case 1775
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
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016001", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016101", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016201", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016301", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                            End Select
                            sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Return enmRunStatus.Alarm
                        Else
                            sys.SysNum = 1770
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gTriggerBoard.ResetAlarm(sys.StageNo).Status = True Then
                        mTriggerCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 1780
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("ResetAlarm: " & gTriggerBoard.ResetAlarm(sys.StageNo).STR)
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016001", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016101", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016201", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016301", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                            End Select
                            sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Return enmRunStatus.Alarm
                        Else
                            sys.SysNum = 1770
                        End If
                    End If
                End If

            Case 1780
                If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                    If gTriggerBoard.SetCycleRecipe(sys.StageNo, mCyleParam(mValveNo(sys.StageNo)), False) = True Then
                        mStopWatch(mValveNo(sys.StageNo)).Restart() '逾時計時
                        sys.SysNum = 1785
                    End If
                ElseIf IsTimeOut(mStopWatch(mValveNo(sys.StageNo)), gSSystemParameter.TimeOut1) Then
                    gEqpMsg.Add(mFunctionName, GetValveALID(mValveNo(sys.StageNo), Error_1016002), eMessageLevel.Error, sys.SysNum)

                    mErrorMessage(sys.StageNo) = "Trigger Board is Busy TimeOut!!"
                    gSyslog.Save(mErrorMessage(sys.StageNo))
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
            Case 1785
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
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016001", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016101", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016201", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016301", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                            End Select
                            sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Return enmRunStatus.Alarm
                        Else
                            sys.SysNum = 1780
                        End If
                    End If
                Else
                    ''[Note]:檢查接收資料
                    If gTriggerBoard.CycleRecipe(sys.StageNo).Status Then
                        mTriggerCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 1790
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("CycleRecipe(T Cmd): " & gTriggerBoard.CycleRecipe(sys.StageNo).STR)
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1016003", "Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1016103", "Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1016203", "Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1016303", "Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                            End Select
                            sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                        Else
                            sys.SysNum = 1780
                        End If
                    End If
                End If

            Case 1790
                If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                    '[說明]: enmValve.No1 部分 Stage1 Valve1 = 0 ,Valve2 = 1   設定部分還需測試
                    If gTriggerBoard.SetDispenseRun(sys.StageNo, enmTriggerDispType.CycleRecipe, mValveNo(sys.StageNo), 0, 0, False) = True Then
                        sys.SysNum = 1791
                    End If
                ElseIf IsTimeOut(mStopWatch(mValveNo(sys.StageNo)), gSSystemParameter.TimeOut1) Then
                    gEqpMsg.Add(mFunctionName, GetValveALID(mValveNo(sys.StageNo), Error_1016002), eMessageLevel.Error, sys.SysNum)

                    mErrorMessage(sys.StageNo) = "Trigger Board is Busy TimeOut!!"
                    gSyslog.Save(mErrorMessage(sys.StageNo))
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
            Case 1791
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
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016001", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016101", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016201", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016301", "Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                            End Select
                            sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Return enmRunStatus.Alarm
                        Else
                            sys.SysNum = 1790
                        End If
                    End If
                Else
                    ''[Note]:檢查接收資料
                    If gTriggerBoard.DispenseRun(sys.StageNo).Status Then
                        mTriggerCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 1792
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("DispenseRun(T Cmd): " & gTriggerBoard.DispenseRun(sys.StageNo).STR)
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1016003", "Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1016103", "Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1016203", "Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1016303", "Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                            End Select
                            sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Return enmRunStatus.Alarm
                        Else
                            sys.SysNum = 1791
                        End If
                    End If

                End If
            Case 1792
                '[說明]:Trigger ON 
                Call SetDispensingTrigger(sys.StageNo, mValveNo(sys.StageNo), enmONOFF.eON)
                sys.SysNum = 1795
            Case 1795
                '[說明]:Purge杯真空吸
                gSysAdapter.SetPurgeVacuum(sys.StageNo, mValveNo(sys.StageNo), enmONOFF.eON)

                '[說明]:Sensor檢查計數開始
                Call mStopWatch(sys.StageNo).Restart()
                mStopWatchStart(sys.StageNo) = mStopWatch(sys.StageNo).ElapsedMilliseconds
                sys.SysNum = 1800

            Case 1800
                '[說明]:Vacuum Ready Sensor
                If gSysAdapter.IsPurgeVacuumReady(sys.StageNo, mValveNo(sys.StageNo)) Then
                    Call mStopWatch(sys.StageNo).Stop()
                    mStopWatch(sys.StageNo).Restart()
                    mStopWatchStart(sys.StageNo) = mStopWatch(sys.StageNo).ElapsedMilliseconds
                    sys.SysNum = 1850
                ElseIf IsTimeOut(mStopWatch(sys.StageNo), mStopWatchStart(sys.StageNo), gSSystemParameter.StableTime.CheckSensorTimeout) = True Then
                    '--- Soni + 2014.10.29 真空建立失敗時,停止出膠
                    'gSysAdapter.SetSyringePressure(sys.StageNo, mValveNo(sys.StageNo), enmONOFF.eOff)

                    '[說明]:關膠
                    '20161114
                    Call SetDispensingTrigger(sys.StageNo, mValveNo(sys.StageNo), enmONOFF.eOff)
                    'Call SetValveGlueOnOff(mValveNo(sys.StageNo), enmONOFF.eOff)

                    gDOCollection.SetState(enmDO.Pump, False) '關閉Pump 真空建立失敗時,停止出膠
                    gEqpMsg.Add(mFunctionName, GetValveALID(mValveNo(sys.StageNo), Alarm_2019001), eMessageLevel.Error, sys.SysNum)
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm
                End If
            Case 1850
                '[說明]:等待Purge
                If IsTimeOut(mStopWatch(sys.StageNo), mStopWatchStart(sys.StageNo), mPurgeTime(sys.StageNo)) = True Then
                    '[說明]:在Purge前開啟膠桶壓力，Purge完膠就關閉
                    'gSysAdapter.SetSyringePressure(sys.StageNo, mValveNo(sys.StageNo), enmONOFF.eOff)
                    '[說明]:關膠
                    Call SetDispensingTrigger(sys.StageNo, mValveNo(sys.StageNo), enmONOFF.eOff)

                    sys.SysNum = 1900
                End If

            Case 1900
                '[說明]:移動Z軸至Up高度 & 關閉Pump
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                gSysAdapter.SetPurgeVacuum(sys.StageNo, mValveNo(sys.StageNo), enmONOFF.eOff)
                sys.SysNum = 3000

            Case 3000

                '[說明]等待停止
                If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If

                'Purge為排舊膠功能
                If mPurgeDone(sys.StageNo) = True Then '第二輪 純吸不排完成
                    gSysAdapter.SetPurgeVacuum(sys.StageNo, mValveNo(sys.StageNo), enmONOFF.eOff)
                    Call SetDispensingTrigger(sys.StageNo, mValveNo(sys.StageNo), enmONOFF.eOff)
                    'gSysAdapter.SetSyringePressure(sys.StageNo, mValveNo(sys.StageNo), enmONOFF.eOff)
                    sys.SysNum = 9000
                Else '第一輪完成
                    mPurgeDone(sys.StageNo) = True
                    mStopWatch(sys.StageNo).Restart()
                    sys.SysNum = 3100
                End If

            Case 3100
                If mStopWatch(sys.StageNo).ElapsedMilliseconds > 100 Then
                    sys.SysNum = 1600
                End If

            Case 9000

                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Finish
                sys.RunStatus = enmRunStatus.Finish
                Return enmRunStatus.Finish 'Soni + 2017.01.20 傳回正常結束
        End Select

        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
    End Function

    ''' <summary>
    ''' 純滾輪擦拭子流程
    ''' </summary>
    ''' <param name="sys"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JetClear(ByRef sys As sSysParam) As enmRunStatus 'Soni +2017.01.20 補傳回值格式
        Const mFunctionName As String = "JetClear"

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart '紀錄參數, 預設狀態
                mValveNo(sys.StageNo) = sys.SelectValve '紀錄選取的閥 避免暫停中斷等影響

                '[說明]:關閉出膠
                Call SetDispensingTrigger(sys.StageNo, mValveNo(sys.StageNo), enmONOFF.eOff)
                '[說明]:Clamp OFF
                Call gSysAdapter.ClearGlueClampOnOff(mValveNo(sys.StageNo), enmONOFF.eOff)
                sys.SysNum = 4000

            Case 4000    '清膠材料更換判定!?
                '換膠暫停中, 換完按開始來解除暫停狀態. 此時CleanPasteNum=0, 可順利往下繼續
                If sys.ExternalPause = True Then
                    Return enmRunStatus.Running
                End If

                With gSSystemParameter.StageParts.ValveData(sys.StageNo)
                    If .CleanPasteNum(mValveNo(sys.StageNo)) > .CleanPasteNumLimit(mValveNo(sys.StageNo)) Then
                        .CleanPasteNum(mValveNo(sys.StageNo)) = 0
                        gEqpMsg.Add(mFunctionName, GetValveALID(mValveNo(sys.StageNo), Warn_3019009), eMessageLevel.Warning, sys.SysNum) '請更換清膠材料!
                        sys.ExternalPause = True

                        Return enmRunStatus.Running
                    Else
                        sys.SysNum = 4100
                    End If
                End With

            Case 4100 '閥氣缸上升 & 各軸速度設定
                '[說明]:氣缸上昇
                Call ValveCylinderAction(sys.StageNo, enmValve.No2, enmUpDown.Up, mStopWatch(sys.StageNo)) '注意給定參數 ClearGlueStopWatch(sys.StageNo)是沒用的
                mStopWatch(sys.StageNo).Restart()
                mStopWatchStart(sys.StageNo) = mStopWatch(sys.StageNo).ElapsedMilliseconds
                If SetXYZBVelocity(sys) = enmRunStatus.Alarm Then 'X,Y,Z,B軸速度設定
                    Return enmRunStatus.Alarm
                End If
                sys.SysNum = 4150

            Case 4150 '閥氣缸上升確保
                '[說明]:Check Cylinder Up Down Sensor
                If ValveCylinderSensor(sys.StageNo, enmValve.No2, enmUpDown.Up) = True Then
                    Call mStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 4200
                ElseIf IsTimeOut(mStopWatch(sys.StageNo), mStopWatchStart(sys.StageNo), gSSystemParameter.StableTime.CheckSensorTimeout) = True Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Alarm_2004000), eMessageLevel.Alarm, sys.SysNum)  '[Cylinder Up Down Sensor Alarm]
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

            Case 4200 '[說明]:移動Z軸至Up位置
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                sys.SysNum = 4300

            Case 4300 'Z軸移動到位&逾時判定
                If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If

                If sys.AxisB <> -1 Then
                    sys.SysNum = 4310 'B軸轉正流程
                Else
                    sys.SysNum = 4400
                End If

            Case 4310 'Tilt 轉正
                If gCMotion.AbsMove(sys.AxisB, GetSysParamTilePos(sys)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1034000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                sys.SysNum = 4320

            Case 4320 'Tilt轉正到位
                If gCMotion.MotionDone(sys.AxisB) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisB) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1034004), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If
                sys.SysNum = 4400

            Case 4400 '[說明]:移動到Clear Glue之位置  20160921
                mClearGluePosX(sys.StageNo) = gSSystemParameter.Pos.CleanValveCalibration(sys.StageNo).PosX(mValveNo(sys.StageNo))
                mClearGluePosY(sys.StageNo) = gSSystemParameter.Pos.CleanValveCalibration(sys.StageNo).PosY(mValveNo(sys.StageNo))
                mClearGluePosZ(sys.StageNo) = gSSystemParameter.Pos.CleanValveCalibration(sys.StageNo).PosZ(mValveNo(sys.StageNo))

                '[說明]:根據清膠的紀錄，移動到乾淨的海綿位置準備清膠(只有X軸)
                With gSSystemParameter.StageParts.ValveData(sys.StageNo)
                    mClearGluePosX(sys.StageNo) = mClearGluePosX(sys.StageNo) + (.CleanPastePitch(mValveNo(sys.StageNo)) * .CleanPasteNum(mValveNo(sys.StageNo)))
                End With

                If gCMotion.AbsMove(sys.AxisX, mClearGluePosX(sys.StageNo)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

                If gCMotion.AbsMove(sys.AxisY, mClearGluePosY(sys.StageNo)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

                sys.SysNum = 4500

            Case 4500
                '[說明]:檢查停止
                If gCMotion.MotionDone(sys.AxisX) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If
                If gCMotion.MotionDone(sys.AxisY) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisY) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If
                sys.SysNum = 4550
            Case 4550
                '[說明]:汽缸下降
                Call ValveCylinderAction(sys.StageNo, mValveNo(sys.StageNo), enmUpDown.Down, mStopWatch(sys.StageNo))
                mStopWatch(sys.StageNo).Restart()
                mStopWatchStart(sys.StageNo) = mStopWatch(sys.StageNo).ElapsedMilliseconds
                sys.SysNum = 4560

            Case 4560
                If ValveCylinderSensor(sys.StageNo, mValveNo(sys.StageNo), enmUpDown.Down) = True Then
                    Call mStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 4600
                Else
                    If IsTimeOut(mStopWatch(sys.StageNo), mStopWatchStart(sys.StageNo), gSSystemParameter.StableTime.CheckSensorTimeout) = True Then
                        gEqpMsg.Add(mFunctionName, Alarm_2004001, eMessageLevel.Error, sys.SysNum)  '[Cylinder Up Down Sensor Alarm]
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                End If

            Case 4600
                '[說明]:Z軸移至Clear Glue高度
                If gCMotion.AbsMove(sys.AxisZ, mClearGluePosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

                '[說明]:啟動滾輪
                If gSSystemParameter.StageParts.ValveData(sys.StageNo).CleanPasteDir(mValveNo(sys.StageNo)) Then
                    gDOCollection.SetState(enmDO.ClearGlueMotorPowerOn, True)
                Else
                    gDOCollection.SetState(enmDO.ClearGlueMotorPowerOn, True)
                    gDOCollection.SetState(enmDO.ClearGlueMotorCW, True)
                End If
                mStopWatch(sys.StageNo).Restart()
                sys.SysNum = 4700

            Case 4700
                '[說明]:檢查停止 Z
                If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If
                sys.SysNum = 4800

            Case 4800
                '[說明]:Clear Glue Motor On
                gDOCollection.SetState(enmDO.ClearGlueClampOn, True)

                '[說明]:Sensor檢查計數開始
                Call mStopWatch(sys.StageNo).Restart()
                mStopWatchStart(sys.StageNo) = mStopWatch(sys.StageNo).ElapsedMilliseconds

                mClearGluePosY(sys.StageNo) += gSSystemParameter.StageParts.ValveData(sys.StageNo).CleanPasteOffset(mValveNo(sys.StageNo)) '移動目標位置(擦拭動作)

                If gSSystemParameter.StageParts.ValveData(sys.StageNo).CleanPasteSpeed(mValveNo(sys.StageNo)) > 0 Then
                    If gCMotion.SetVelHigh(sys.AxisX, gSSystemParameter.StageParts.ValveData(sys.StageNo).CleanPasteSpeed(mValveNo(sys.StageNo))) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030013), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                    If gCMotion.SetVelHigh(sys.AxisY, gSSystemParameter.StageParts.ValveData(sys.StageNo).CleanPasteSpeed(mValveNo(sys.StageNo))) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031013), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                End If
                sys.SysNum = 4820

            Case 4820
                If gCMotion.AbsMove(sys.AxisY, mClearGluePosY(sys.StageNo)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                sys.SysNum = 4840

            Case 4840
                If gCMotion.MotionDone(sys.AxisY) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisY) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If

                '[說明]:修改為原始速度
                '[說明]:X軸
                With gCMotion.AxisParameter(sys.AxisX).Velocity
                    If gCMotion.SetVelLow(sys.AxisX, .VelLow) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030014), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                    If gCMotion.SetVelHigh(sys.AxisX, .VelHigh) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030013), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                    If gCMotion.SetAcc(sys.AxisX, .Acc * .AccRatio) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030011), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                    If gCMotion.SetDec(sys.AxisX, .Dec * .DecRatio) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030012), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                End With

                With gCMotion.AxisParameter(sys.AxisY).Velocity
                    If gCMotion.SetVelLow(sys.AxisY, .VelLow) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031014), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                    If gCMotion.SetVelHigh(sys.AxisY, .VelHigh) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031013), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                    If gCMotion.SetAcc(sys.AxisY, .Acc * .AccRatio) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031011), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                    If gCMotion.SetDec(sys.AxisY, .Dec * .DecRatio) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031012), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                End With

                With gCMotion.AxisParameter(sys.AxisZ).Velocity
                    If gCMotion.SetVelLow(sys.AxisZ, .VelLow) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032014), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                    If gCMotion.SetVelHigh(sys.AxisZ, .VelHigh) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032013), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                    If gCMotion.SetAcc(sys.AxisZ, .Acc * .AccRatio) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032011), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                    If gCMotion.SetDec(sys.AxisZ, .Dec * .DecRatio) <> CommandStatus.Sucessed Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032012), eMessageLevel.Error, sys.SysNum)

                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                End With

                sys.SysNum = 5200

            Case 5200
                '[說明]:關閉滾輪
                gDOCollection.SetState(enmDO.ClearGlueMotorPowerOn, False)
                gDOCollection.SetState(enmDO.ClearGlueMotorCW, False)

                sys.SysNum = 5300

            Case 5300
                'If gCRecipe.Pattern.Count > 0 Then
                '    '[說明]:設定DispenserNo1 DispenserNo2之出膠壓力
                '    'gSysAdapter.SetSyringeAirPressure(sys.StageNo, mValveNo(sys.StageNo), gCRecipe.StageParts(sys.StageNo).SyringePressure(mValveNo(sys.StageNo)), False)
                'Else
                '    '[說明]:設定DispenserNo1 DispenserNo2之出膠壓力
                '    'gSysAdapter.SetSyringeAirPressure(sys.StageNo, 0, False) 'TODO:???
                'End If

                gEPVCollection.SetValue(sys.StageNo, mValveNo(sys.StageNo), eEPVPressureType.Syringe, gCRecipe.StageParts(sys.StageNo).SyringePressure(mValveNo(sys.StageNo)), False)

                '[說明]:開啟膠桶壓力
                gDOCollection.SetState(enmDO.SyringePressure2, True)

                mStopWatch(sys.StageNo).Restart()
                mStopWatchStart(sys.StageNo) = mStopWatch(sys.StageNo).ElapsedMilliseconds
                sys.SysNum = 5350

            Case 5350
                '[說明]:持續給壓力但不出膠一段時間後，關閉膠桶壓力
                If IsTimeOut(mStopWatch(sys.StageNo), mStopWatchStart(sys.StageNo), gSSystemParameter.StageParts.ValveData(sys.StageNo).CleanPastePressureTime(mValveNo(sys.StageNo))) = True Then
                    '[說明]:關閉膠桶壓力
                    gDOCollection.SetState(enmDO.SyringePressure2, False)
                    sys.SysNum = 5400
                End If

            Case 5400
                '[說明]:移動Z軸至Up位置
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

                sys.SysNum = 5500
            Case 5500
                If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If
                sys.SysNum = 9000
            Case 9000
                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Finish
                sys.RunStatus = enmRunStatus.Finish
                Return enmRunStatus.Finish 'Soni + 2017.01.20 傳回正常結束

        End Select
        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
    End Function

    ''' <summary>
    ''' 純針狀閥(螺桿閥)擦拭子流程
    ''' </summary>
    ''' <param name="sys"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AugerClear(ByRef sys As sSysParam) As enmRunStatus 'Soni +2017.01.20 補傳回值格式
        Const mFunctionName As String = "AugerClear"
        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                mValveNo(sys.StageNo) = sys.SelectValve '紀錄選取得閥 避免暫停中斷等影響

                '[說明]:關閉出膠
                Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eOff)
                '[說明]:設定第二次Purge不出膠,只推壓力,吸附舊膠
                mPurgeDone(sys.StageNo) = False

                '[說明]:設定DispenserNo1 DispenserNo2之出膠壓力
                'gSysAdapter.SetSyringeAirPressure(sys.StageNo, sys.SelectValve, gCRecipe.StageParts(sys.StageNo).SyringePressure(sys.SelectValve))
                gEPVCollection.SetValue(sys.StageNo, sys.SelectValve, eEPVPressureType.Syringe, gCRecipe.StageParts(sys.StageNo).SyringePressure(sys.SelectValve), False)
                '20170720 _改單位為SEC *1000
                mPurgeTime(sys.StageNo) = gSSystemParameter.StageParts.ValveData(sys.StageNo).PurgeTime(sys.SelectValve) * 1000
                sys.SysNum = 4000

            Case 4000    'JettingClean

                '[說明]:紀錄當下的馬達座標 & 關閉出膠 & Clamp OFF

                '[說明]:Clamp OFF
                Call gSysAdapter.ClearGlueClampOnOff(sys.SelectValve, enmONOFF.eOff)

                With gSSystemParameter.StageParts.ValveData(sys.StageNo)
                    If .CleanPasteNum(sys.SelectValve) >= .CleanPasteNumLimit(sys.SelectValve) Then
                        .CleanPasteNum(sys.SelectValve) = 0
                        gEqpMsg.Add(mFunctionName, Warn_3019009, eMessageLevel.Warning, sys.SysNum) '請更換清膠材料!
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        sys.SysNum = 4060
                    End If
                End With

            Case 4060
                '[說明]:氣缸上昇
                Call ValveCylinderAction(sys.StageNo, enmValve.No2, enmUpDown.Up, mStopWatch(sys.StageNo))
                mStopWatch(sys.StageNo).Restart()
                mStopWatchStart(sys.StageNo) = mStopWatch(sys.StageNo).ElapsedMilliseconds
                sys.SysNum = 4100

            Case 4100
                '[說明]:設定移動速度
                '[說明]:X軸
                If SetXYZBVelocity(sys) = enmRunStatus.Alarm Then
                    Return enmRunStatus.Alarm
                End If

                sys.SysNum = 4150

            Case 4150
                '[說明]:Check Cylinder Up Down Sensor
                If ValveCylinderSensor(sys.StageNo, enmValve.No2, enmUpDown.Up) = True Then
                    Call mStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 4200
                Else
                    If IsTimeOut(mStopWatch(sys.StageNo), gSSystemParameter.StableTime.CheckSensorTimeout) = True Then
                        gEqpMsg.Add(mFunctionName, Alarm_2004000, eMessageLevel.Alarm, sys.SysNum)   '[Cylinder Up Down Sensor Alarm]
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                End If

            Case 4200
                '[說明]:移動Z軸至Up位置
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032000), eMessageLevel.Error, sys.SysNum)   '[Cylinder Up Down Sensor Alarm]
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

                sys.SysNum = 4300
            Case 4300
                If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        '移動逾時
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If
                '20161130
                If sys.AxisB <> -1 Then
                    sys.SysNum = 4310
                Else
                    sys.SysNum = 4400
                End If
            Case 4310
                '2016 1130 Tilt 轉正
                If gCMotion.AbsMove(sys.AxisB, GetSysParamTilePos(sys)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1034000), eMessageLevel.Error, sys.SysNum)
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                sys.SysNum = 4320
            Case 4320

                If gCMotion.MotionDone(sys.AxisB) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisB) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1034004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm
                    Else
                        Return enmRunStatus.Running
                    End If
                End If
                sys.SysNum = 4400

            Case 4400
                '[說明]:移動到Clear Glue之位置  20160921
                mClearGluePosX(sys.StageNo) = gSSystemParameter.Pos.CleanValveCalibration(sys.StageNo).PosX(sys.SelectValve)
                mClearGluePosY(sys.StageNo) = gSSystemParameter.Pos.CleanValveCalibration(sys.StageNo).PosY(sys.SelectValve)
                mClearGluePosZ(sys.StageNo) = gSSystemParameter.Pos.CleanValveCalibration(sys.StageNo).PosZ(sys.SelectValve)

                '[說明]:根據清膠的紀錄，移動到乾淨的海綿位置準備清膠(只有X軸)
                With gSSystemParameter.StageParts.ValveData(sys.StageNo)
                    mClearGluePosX(sys.StageNo) = mClearGluePosX(sys.StageNo) + (.CleanPastePitch(sys.SelectValve) * .CleanPasteNum(sys.SelectValve))
                End With

                If gCMotion.AbsMove(sys.AxisX, mClearGluePosX(sys.StageNo)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

                If gCMotion.AbsMove(sys.AxisY, mClearGluePosY(sys.StageNo)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

                With gSSystemParameter.StageParts.ValveData(sys.StageNo)
                    .CleanPasteNum(sys.SelectValve) = .CleanPasteNum(sys.SelectValve) + 1
                End With

                sys.SysNum = 4500

            Case 4500
                '[說明]:檢查停止
                If gCMotion.MotionDone(sys.AxisX) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1030004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If
                If gCMotion.MotionDone(sys.AxisY) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisY) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1031004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If
                sys.SysNum = 4550

            Case 4550
                '[說明]:汽缸下降
                Call ValveCylinderAction(sys.StageNo, sys.SelectValve, enmUpDown.Down, mStopWatch(sys.StageNo))
                mStopWatch(sys.StageNo).Restart()
                sys.SysNum = 4560

            Case 4560
                If ValveCylinderSensor(sys.StageNo, sys.SelectValve, enmUpDown.Down) = True Then
                    Call mStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 4600
                Else
                    If IsTimeOut(mStopWatch(sys.StageNo), gSSystemParameter.StableTime.CheckSensorTimeout) = True Then
                        gEqpMsg.Add(mFunctionName, Alarm_2004001, eMessageLevel.Alarm, sys.SysNum)    '[Cylinder Up Down Sensor Alarm]
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    End If
                End If

            Case 4600
                '[說明]:Z軸移至Clear Glue高度
                If gCMotion.AbsMove(sys.AxisZ, mClearGluePosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032000), eMessageLevel.Alarm, sys.SysNum)    '[Cylinder Up Down Sensor Alarm]

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

                sys.SysNum = 4700

            Case 4700
                '[說明]:檢查停止 Z
                If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032004), eMessageLevel.Alarm, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If
                sys.SysNum = 4800

            Case 4800
                '[說明]:Clear Glue Clamp On
                gDOCollection.SetState(enmDO.ClearGlueClampOn, True)
                gDOCollection.SetState(enmDO.ClearGlueClampOff, False)
                '[說明]:Sensor檢查計數開始
                Call mStopWatch(sys.StageNo).Restart()
                mStopWatchStart(sys.StageNo) = mStopWatch(sys.StageNo).ElapsedMilliseconds
                sys.SysNum = 4900

            Case 4900
                '[說明]:螺桿 夾定位到位確認
                If gDICollection.GetState(enmDI.ClearGlueClampOnSensor, True) = True And gDICollection.GetState(enmDI.ClearGlueClampOffSensor, False) = False Then
                    Call mStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 5000
                ElseIf IsTimeOut(mStopWatch(sys.StageNo), mStopWatchStart(sys.StageNo), gSSystemParameter.StableTime.CheckSensorTimeout) = True Then
                    gEqpMsg.Add(mFunctionName, Alarm_2004002, eMessageLevel.Alarm, sys.SysNum)  '[Clamp On Off Sensor Alarm]
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

            Case 5000
                '[說明]:Z軸往上移
                Select Case gSSystemParameter.CoordType
                    Case enmCoordinateRelationType.eDTS
                        mClearGluePosZ(sys.SelectValve) = mClearGluePosZ(sys.StageNo) + Math.Abs(gSSystemParameter.StageParts.ValveData(sys.StageNo).CleanPasteDistanceZ(sys.SelectValve))

                    Case enmCoordinateRelationType.eGN2
                        mClearGluePosZ(sys.SelectValve) = mClearGluePosZ(sys.StageNo) - Math.Abs(gSSystemParameter.StageParts.ValveData(sys.StageNo).CleanPasteDistanceZ(sys.SelectValve))

                End Select

                If gCMotion.AbsMove(sys.AxisZ, mClearGluePosZ(sys.SelectValve)) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If

                sys.SysNum = 5100

            Case 5100
                '[說明]等待停止
                If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If
                sys.SysNum = 5200

            Case 5200
                '[說明]:Clear Glue Clamp Off
                gDOCollection.SetState(enmDO.ClearGlueClampOn, False)
                gDOCollection.SetState(enmDO.ClearGlueClampOff, True)

                '[說明]:Sensor檢查計數開始
                Call mStopWatch(sys.StageNo).Restart()
                mStopWatchStart(sys.StageNo) = mStopWatch(sys.StageNo).ElapsedMilliseconds
                sys.SysNum = 5300

            Case 5300
                '[說明]:Clamp Off 定位
                If IsTimeOut(mStopWatch(sys.StageNo), mStopWatchStart(sys.StageNo), gSSystemParameter.StableTime.CheckSensorTimeout) = True Then
                    gEqpMsg.Add(mFunctionName, Alarm_2004003, eMessageLevel.Alarm, sys.SysNum) '[Clamp On Off Sensor Alarm]
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                Else
                    If gDICollection.GetState(enmDI.ClearGlueClampOnSensor, False) = False And gDICollection.GetState(enmDI.ClearGlueClampOffSensor, True) = True Then
                        Call mStopWatch(sys.StageNo).Stop()
                        'If gCRecipe.Pattern.Count > 0 Then
                        '    '[說明]:設定DispenserNo1 DispenserNo2之出膠壓力
                        '    gSysAdapter.SetSyringeAirPressure(sys.StageNo, sys.SelectValve, gCRecipe.StageParts(sys.StageNo).SyringePressure(sys.SelectValve), False)
                        'Else
                        '    '[說明]:設定DispenserNo1 DispenserNo2之出膠壓力
                        '    gSysAdapter.SetSyringeAirPressure(sys.StageNo, 0, False)
                        'End If
                        gEPVCollection.SetValue(sys.StageNo, sys.SelectValve, eEPVPressureType.Syringe, gCRecipe.StageParts(sys.StageNo).SyringePressure(sys.SelectValve), False)
                        '[說明]:開啟膠桶壓力
                        gDOCollection.SetState(enmDO.SyringePressure2, True)
                    End If
                End If
                mStopWatch(sys.StageNo).Restart()
                mStopWatchStart(sys.StageNo) = mStopWatch(sys.StageNo).ElapsedMilliseconds
                sys.SysNum = 5350

            Case 5350
                '[說明]:持續給壓力但不出膠一段時間後，關閉膠桶壓力
                If IsTimeOut(mStopWatch(sys.StageNo), mStopWatchStart(sys.StageNo), gSSystemParameter.StageParts.ValveData(sys.StageNo).CleanPastePressureTime(sys.SelectValve)) = True Then
                    '[說明]:關閉膠桶壓力
                    gDOCollection.SetState(enmDO.SyringePressure2, False)
                    sys.SysNum = 5400
                End If

            Case 5400
                '[說明]:移動Z軸至Up位置
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032000), eMessageLevel.Error, sys.SysNum)

                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                End If
                sys.SysNum = 5500

            Case 5500
                If gCMotion.MotionDone(sys.AxisZ) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1032004), eMessageLevel.Error, sys.SysNum)
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Return enmRunStatus.Alarm 'Soni + 2017.01.20 傳回Alarm
                    Else
                        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
                    End If
                End If
                sys.SysNum = 9000

            Case 9000
                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Finish
                sys.RunStatus = enmRunStatus.Finish
                Return enmRunStatus.Finish 'Soni + 2017.01.20 傳回正常結束
        End Select
        Return enmRunStatus.Running 'Soni + 2017.01.20 傳回運行中
    End Function
#End Region

    'jimmy 20170822 手動觸發秤重
    Private mbManualPurgeAction As Boolean
    Public Property _bManualPurgeAction() As Boolean
        Get
            Return mbManualPurgeAction
        End Get
        Set(ByVal value As Boolean)
            mbManualPurgeAction = value
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

#Region "Public Function"
    ''' <summary>
    ''' Purge, 擦螺桿, 擦閥頭混合派工流程
    ''' </summary>
    ''' <param name="sys"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Run(ByRef sys As sSysParam) As enmRunStatus
        'jimmy 20170822
        If mbManualPurgeAction = True Then
            sys.SelectValve = mManualSelectValve
        End If
        Const mFunctionName As String = "SubDispStage_PurgeFunction"
        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                If sys.StageNo >= gCRecipe.StageParts.Count Then '索引超出範圍保護
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1002002), eMessageLevel.Error, sys.SysNum) '系統參數讀取失敗!請確認參數正確!
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    If mbManualPurgeAction = True Then
                        mbManualPurgeAction = False
                        sys.SelectValve = mSelectValveTemp
                    End If
                    Return enmRunStatus.Alarm
                End If

                If sys.SelectValve >= gCRecipe.StageParts(sys.StageNo).PasteName.Count Then '索引超出範圍保護
                    gEqpMsg.Add(mFunctionName, GetStageALID(sys.StageNo, Error_1002004), eMessageLevel.Error, sys.SysNum) '膠材參數讀取失敗!請確認參數正確!
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    If mbManualPurgeAction = True Then
                        mbManualPurgeAction = False
                        sys.SelectValve = mSelectValveTemp
                    End If
                    Return enmRunStatus.Alarm
                End If

                Dim mPurgeName As String = gCRecipe.StageParts(sys.StageNo).PurgeName(sys.SelectValve)
                If mPurgeName = "" Then '無Purge設定檔
                    'Sue0504
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Alarm_2019014", "frmCalibrationCCD2Valve1 btnALign_Click", , gMsgHandler.GetMessage(Alarm_2019014), eMessageLevel.Alarm)
                            MsgBox(gMsgHandler.GetMessage(Alarm_2019014), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Alarm_2019114", "frmCalibrationCCD2Valve1 btnALign_Click", , gMsgHandler.GetMessage(Alarm_2019114), eMessageLevel.Alarm)
                            MsgBox(gMsgHandler.GetMessage(Alarm_2019114), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Alarm_2019214", "frmCalibrationCCD2Valve1 btnALign_Click", , gMsgHandler.GetMessage(Alarm_2019214), eMessageLevel.Alarm)
                            MsgBox(gMsgHandler.GetMessage(Alarm_2019214), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Alarm_2019314", "frmCalibrationCCD2Valve1 btnALign_Click", , gMsgHandler.GetMessage(Alarm_2019314), eMessageLevel.Alarm)
                            MsgBox(gMsgHandler.GetMessage(Alarm_2019314), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    If mbManualPurgeAction = True Then
                        mbManualPurgeAction = False
                        sys.SelectValve = mSelectValveTemp
                    End If
                    Return enmRunStatus.Alarm
                End If
                mSYS(sys.StageNo).AxisX = sys.AxisX
                mSYS(sys.StageNo).AxisY = sys.AxisY
                mSYS(sys.StageNo).AxisZ = sys.AxisZ
                mSYS(sys.StageNo).AxisA = sys.AxisA
                mSYS(sys.StageNo).AxisB = sys.AxisB
                mSYS(sys.StageNo).AxisC = sys.AxisC
                mSYS(sys.StageNo).StageNo = sys.StageNo
                mSYS(sys.StageNo).SelectValve = sys.SelectValve
                mSYS(sys.StageNo).ValveNo = sys.ValveNo

                mCleanType(sys.StageNo) = gPurgeDB(mPurgeName).CleanType '暫存清潔方式
                ValveCylinderAction(sys.StageNo, sys.SelectValve, enmUpDown.Down) 'Soni + 2017.04.27 增加氣缸動作
                mStopWatch(sys.StageNo).Restart()
                sys.SysNum = 1020
            Case 1020
                If ValveCylinderSensor(sys.StageNo, sys.SelectValve, enmUpDown.Down) = True Then 'Soni + 2017.04.27 增加氣缸動作
                    Select Case mCleanType(sys.StageNo)
                        Case eCleanType.VacuumClean, eCleanType.VacuumJetClean, eCleanType.VacuumAugerClean 'Purge 開頭流程
                            mSYS(sys.StageNo).SysNum = sSysParam.SysLoopStart
                            mSYS(sys.StageNo).RunStatus = enmRunStatus.Running
                            sys.SysNum = 1100

                        Case eCleanType.JetClean '擦閥頭 開頭流程
                            mSYS(sys.StageNo).SysNum = sSysParam.SysLoopStart
                            mSYS(sys.StageNo).RunStatus = enmRunStatus.Running
                            sys.SysNum = 1200

                        Case eCleanType.AugerClean '擦螺桿 開頭流程
                            mSYS(sys.StageNo).SysNum = sSysParam.SysLoopStart
                            mSYS(sys.StageNo).RunStatus = enmRunStatus.Running
                            sys.SysNum = 1300

                    End Select
                ElseIf mStopWatch(sys.StageNo).ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2004001", "frmCalibrationCCD2Valve1 btnALign_Click", , gMsgHandler.GetMessage(Alarm_2004001), eMessageLevel.Alarm)
                    MsgBox(gMsgHandler.GetMessage(Alarm_2019014), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    If mbManualPurgeAction = True Then
                        mbManualPurgeAction = False
                        sys.SelectValve = mSelectValveTemp
                    End If
                    Return enmRunStatus.Alarm
                End If


            Case 1100 '純真空
                If mSYS(sys.StageNo).RunStatus = enmRunStatus.Running Then
                    Select Case mCleanType(sys.StageNo)
                        Case eCleanType.VacuumClean, eCleanType.VacuumJetClean, eCleanType.VacuumAugerClean
                            sys.RunStatus = Purge(mSYS(sys.StageNo))
                            If sys.RunStatus = enmRunStatus.Alarm Then
                                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                                sys.RunStatus = enmRunStatus.Alarm
                                If mbManualPurgeAction = True Then
                                    mbManualPurgeAction = False
                                    sys.SelectValve = mSelectValveTemp
                                End If
                                Return enmRunStatus.Alarm
                            End If
                        Case Else

                            mSYS(sys.StageNo).RunStatus = enmRunStatus.Finish
                    End Select
                End If

                If mSYS(sys.StageNo).RunStatus = enmRunStatus.Finish Then
                    Select Case mCleanType(sys.StageNo)
                        Case eCleanType.VacuumClean '純真空
                            sys.SysNum = 9000
                        Case eCleanType.JetClean, eCleanType.AugerClean '擦閥頭 ,擦螺桿  此模式流程不合法
                            sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            If mbManualPurgeAction = True Then
                                mbManualPurgeAction = False
                                sys.SelectValve = mSelectValveTemp
                            End If
                            Return enmRunStatus.Alarm

                        Case eCleanType.VacuumJetClean 'Purge+擦閥頭
                            mSYS(sys.StageNo).SysNum = sSysParam.SysLoopStart
                            mSYS(sys.StageNo).RunStatus = enmRunStatus.Running
                            sys.SysNum = 1200
                        Case eCleanType.VacuumAugerClean 'Purge+擦螺桿
                            mSYS(sys.StageNo).SysNum = sSysParam.SysLoopStart
                            mSYS(sys.StageNo).RunStatus = enmRunStatus.Running
                            sys.SysNum = 1300
                    End Select

                End If

            Case 1200 '擦閥頭
                If mSYS(sys.StageNo).RunStatus = enmRunStatus.Running Then
                    sys.RunStatus = JetClear(mSYS(sys.StageNo))
                    If sys.RunStatus = enmRunStatus.Alarm Then
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        If mbManualPurgeAction = True Then
                            mbManualPurgeAction = False
                            sys.SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                End If
                If mSYS(sys.StageNo).RunStatus = enmRunStatus.Finish Then
                    sys.SysNum = 9000
                End If
            Case 1300 '擦螺桿
                If mSYS(sys.StageNo).RunStatus = enmRunStatus.Running Then
                    sys.RunStatus = AugerClear(mSYS(sys.StageNo))
                    If sys.RunStatus = enmRunStatus.Alarm Then
                        sys.Act(eAct.Purge).RunStatus = enmRunStatus.Alarm
                        If mbManualPurgeAction = True Then
                            mbManualPurgeAction = False
                            sys.SelectValve = mSelectValveTemp
                        End If
                        Return enmRunStatus.Alarm
                    End If
                End If
                If mSYS(sys.StageNo).RunStatus = enmRunStatus.Finish Then
                    sys.SysNum = 9000
                End If

            Case 9000
                With gSSystemParameter.StageParts.ValveData(sys.StageNo) '清膠計屬累增
                    .CleanPasteNum(mValveNo(sys.StageNo)) += 1
                End With
                sys.Act(eAct.Purge).RunStatus = enmRunStatus.Finish
                If mbManualPurgeAction = True Then
                    mbManualPurgeAction = False
                    sys.SelectValve = mSelectValveTemp
                End If
                sys.RunStatus = enmRunStatus.Finish
                Return enmRunStatus.Finish
        End Select
        Return enmRunStatus.Running
    End Function

#End Region


End Class

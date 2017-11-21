Imports ProjectIO
Imports ProjectCore

Public Class clsCV_B
    Enum enmTask
        Null
        Initial
        Load
        Unload
    End Enum

    Enum enmStep
        Start
        VacuumOFF
        VacuumON
        CheckVacuum
        ElectricCylinderUp
        ElectricCylinderDown
        'CylinderUp
        'CylinderDown
        StoperUp
        StoperDown
        CheckStoperSensor
        RollerLoad
        RollerUnload
        RollerStop
        RollerSpeedChange
        CheckDespatchSensor
        CheckRecieveSensor
        CheckTemperatrue
        ElectricCylinderHome
        [End]

        A_CheckDespatchSensor
        A_CheckRecieveSensor
        A_FrontStoperUp
        A_FrontStoperDown
    End Enum

    Enum enmDirection
        Home
        Up
        Down
    End Enum

    ''' <summary>
    ''' 是否忙碌中 (有無執行中任務)
    ''' </summary>
    Public IsBusy As Boolean
    ''' <summary>
    ''' 執行中任務
    ''' </summary>
    Public Task As enmTask = enmTask.Null
    ''' <summary>
    ''' 任務執行到的步驟
    ''' </summary>
    Public TaskStep As enmStep = enmStep.Start

    Dim SwTimeOut As New Stopwatch

    ''' <summary>
    ''' 取得或設定皮帶傳送時,發生減速之前的毫秒數
    ''' </summary>
    Property RollerSlowTime As UInteger = 2000

    ''' <summary>
    ''' 取得或設定單步作業未完成時,發生逾時之前的毫秒數
    ''' </summary>
    Property MotionTimeOut As Integer = 30000

    ''' <summary>
    ''' 取得或設定SMEMA發生逾時之前的毫秒數
    ''' </summary>
    Property SmemaTimeOut As Integer = 1800000 '30分鐘  

    Dim _alarmMsg As String
    ''' <summary>
    ''' 錯誤訊息
    ''' </summary>
    ReadOnly Property AlarmMessage As String
        Get
            Return _alarmMsg
        End Get
    End Property

    Dim _alarmCode As String
    ''' <summary>
    ''' 錯誤訊息代碼
    ''' </summary>
    ReadOnly Property AlarmCode As String
        Get
            Return _alarmCode
        End Get
    End Property


    Sub New()

    End Sub

    Public Function Initial() As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.Initial
                    IsBusy = True
                    SwTimeOut.Start()
                    'If (Unit.B_IsVacuum) Then
                    '    Return enmMotionStatus.Alarm
                    'Else
                    '    SwTimeOut.Restart()
                    '    TaskStep = enmStep.ElectricCylinderHome
                    'End If

                    CvSMEMA(0).IsReadyToRecieve = False
                    CvSMEMA(0).IsReadyToSend = False

                    TaskStep = enmStep.ElectricCylinderHome

                Case enmStep.ElectricCylinderHome
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2084109"
                        _alarmMsg = "MAchine B Stoper 'Up' Timeout Or Electric Cylinder 'Home' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        Unit.B_Roller.Stop()
                        If (Unit.B_Stoper(clsUnit.enmDirection.Up) And Unit.B_ElectricCylinder(enmDirection.Home)) Then
                            If (Unit.B_StoperSensor) Then
                                _alarmCode = "Alarm_2084110"
                                _alarmMsg = "There Are Products On Machine B."
                                Return enmMotionStatus.Alarm
                            Else
                                SwTimeOut.Restart()
                                TaskStep = enmStep.End
                            End If
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select
            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmMsg = ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    Public Function Load(ByVal auto As Boolean) As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start    '起始
                    Task = enmTask.Load
                    IsBusy = True
                    SwTimeOut.Start()
                    If (TimeOutCheck(SmemaTimeOut) Or SecurityCheck() = False) Then
                        _alarmCode = "Alarm_2080016"
                        _alarmMsg = "SMEMA Loader Ready Timeout Or Sensor Not Safe."
                        Return enmMotionStatus.Alarm
                    Else
                        If (CvSMEMA(0).IsLoaderReady) Or (auto = False) Then 'Load站是否可供料
                            SwTimeOut.Restart()
                            TaskStep = enmStep.ElectricCylinderHome
                        End If
                    End If

                    'Case enmStep.ElectricCylinderDown 'B電動缸下降,A電動缸與前後Stoper下降
                    '    If (TimeOutCheck(DeadTime)) Then
                    '        Return enmMotionStatus.Alarm
                    '    Else
                    '        If (Unit.B_ElectricCylinder(enmDirection.Down) And Unit.A_Stoper(enmDirection.Down) And Unit.A_ElectricCylinder(enmDirection.Down) AndAlso Unit.A_FrontStoper(clsUnit.enmDirection.Down)) Then
                    '            SwTimeOut.Restart()
                    '            TaskStep = enmStep.ElectricCylinderHome
                    '        End If
                    '    End If

                Case enmStep.ElectricCylinderHome 'AB電動缸回Home, A前後Stoper下降
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2080017"
                        _alarmMsg = "Machine A/B Electric Cylinder 'Home' Timeout or Machine A Stoper 'Down' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.B_ElectricCylinder(enmDirection.Home) And Unit.A_Stoper(enmDirection.Down) And Unit.A_ElectricCylinder(enmDirection.Home) AndAlso Unit.A_FrontStoper(clsUnit.enmDirection.Down)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.StoperUp
                        End If
                    End If

                Case enmStep.StoperUp 'Stoper上升
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2084112"
                        _alarmMsg = "Machine B Stoper 'Up' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.B_Stoper(enmDirection.Up)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.RollerLoad
                        End If
                    End If

                Case enmStep.RollerLoad   'Roller入料
                    If (TimeOutCheck(SmemaTimeOut)) Then
                        _alarmCode = "Alarm_2080011"
                        _alarmMsg = "SMEMA Loader Ready Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (CvSMEMA(0).IsLoaderReady) Or (auto = False) Then
                            If auto Then
                                CvSMEMA(0).IsReadyToRecieve = True '開啟要料訊號
                            End If
                            Unit.A_Roller.Load(IRoller.enmSpeed.Normal) 'A機Roller start
                            Unit.B_Roller.Load(IRoller.enmSpeed.Normal) 'B機Roller start
                            SwTimeOut.Restart()
                            TaskStep = enmStep.A_CheckRecieveSensor
                        End If
                    End If

                Case enmStep.A_CheckRecieveSensor  '檢查A機入料Sensor
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2083114"
                        _alarmMsg = "Machine A Entrance Sensor Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_EntranceSensor() Or auto = False) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.A_CheckDespatchSensor
                        End If
                    End If

                Case enmStep.A_CheckDespatchSensor '檢查A機出料Sensor
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2083121"
                        _alarmMsg = "Machine A Exit Sensor Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_ExitSensor() Or auto = False) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.CheckRecieveSensor
                        End If
                    End If

                Case enmStep.CheckRecieveSensor   '檢查入料Sensor
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2084114"
                        _alarmMsg = "Machine B Entrance Sensor Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.B_EntranceSensor() Or auto = False) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.RollerSpeedChange
                        End If
                    End If

                Case enmStep.RollerSpeedChange
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2084115"
                        _alarmMsg = "Machine B Roller Change Speed Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        Dim mSec As UInteger = 0 '減速delay時間, 半自動狀態下mSec = 0
                        If auto Then
                            mSec = RollerSlowTime
                        End If

                        If (RollerSpeedChange(mSec)) Then
                            Unit.B_Roller.Load(IRoller.enmSpeed.Slow) '減速IO
                            SwTimeOut.Restart()
                            TaskStep = enmStep.CheckStoperSensor
                        End If
                    End If

                Case enmStep.CheckStoperSensor    '檢查Stoper Sensor
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2084116"
                        _alarmMsg = "Machine B Stoper Sensor Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.B_StoperSensor) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.RollerStop
                        End If
                    End If

                Case enmStep.RollerStop   'Roller停止
                    If (TimeOutCheck(MotionTimeOut)) Then
                        Return enmMotionStatus.Alarm
                    Else
                        Unit.A_Roller.Stop()
                        Unit.B_Roller.Stop() 'B機Roller停止
                        SwTimeOut.Restart()
                        TaskStep = enmStep.A_FrontStoperUp
                    End If

                Case enmStep.A_FrontStoperUp 'A前Stoper上升
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2083117"
                        _alarmMsg = "Machine A Front Stoper 'Up' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_FrontStoper(enmDirection.Up)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.ElectricCylinderUp
                        End If
                    End If

                Case enmStep.ElectricCylinderUp   '電動缸上升
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2084118"
                        _alarmMsg = "Machine B Electric Cylinder 'Up' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.B_ElectricCylinder(enmDirection.Up)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.VacuumON
                            'TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.VacuumON '吸真空
                    gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak1, False)  '吹氣OFF
                    gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak2, False) '吹氣OFF
                    gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak3, False) '吹氣OFF
                    gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak4, False)  '吹氣OFF
                    gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak5, False) '吹氣OFF
                    gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak6, False) '吹氣OFF
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum, Unit.B_Vacuum(0))   '吸真空ON
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum2, Unit.B_Vacuum(1))   '吸真空ON
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum3, Unit.B_Vacuum(2))  '吸真空ON
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum4, Unit.B_Vacuum(3)) '吸真空ON
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum5, Unit.B_Vacuum(4))  '吸真空ON
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum6, Unit.B_Vacuum(5))  '吸真空ON

                    TaskStep = enmStep.CheckVacuum

                Case enmStep.CheckVacuum
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2084119"
                        _alarmMsg = "Machine B Vacuum ON Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.B_IsVacuum(True)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    If (TimeOutCheck(SmemaTimeOut)) Then
                        _alarmCode = "Alarm_2080012"
                        _alarmMsg = "SMEMA Loader Ready OFF Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (CvSMEMA(0).IsLoaderReady = False Or auto = False) Then
                            CvSMEMA(0).IsReadyToRecieve = False    '關閉要料訊號
                            Reset()
                            Return enmMotionStatus.Finish
                        End If
                    End If

            End Select

            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmMsg = ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    Public Function Unload(ByVal auto As Boolean) As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start 'Start
                    Task = enmTask.Unload
                    IsBusy = True
                    SwTimeOut.Start()
                    If (TimeOutCheck(SmemaTimeOut) Or SecurityCheck() = False) Then
                        _alarmCode = "Alarm_2080018"
                        _alarmMsg = "SMEMA Unloader Ready Timeout Or Sensor Not Safe."
                        Return enmMotionStatus.Alarm
                    Else
                        CvSMEMA(0).IsReadyToSend = True '出料訊號ON
                        If (CvSMEMA(0).IsUnloaderReady) Or (auto = False) Then   'Unload站是否可收料
                            SwTimeOut.Restart()
                            TaskStep = enmStep.VacuumOFF
                        End If
                    End If

                Case enmStep.VacuumOFF    '破真空
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum, False)  '吸真空OFF
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum2, False)  '吸真空OFF
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum3, False) '吸真空OFF
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum4, False) '吸真空OFF
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum5, False) '吸真空OFF
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum6, False) '吸真空OFF

                    gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak1, True)  '吹氣ON
                    gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak2, True)  '吹氣ON
                    gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak3, True)  '吹氣ON
                    gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak4, True)  '吹氣ON
                    gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak5, True)  '吹氣ON
                    gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak6, True)  '吹氣ON
                    SwTimeOut.Restart()
                    TaskStep = enmStep.CheckVacuum

                Case enmStep.CheckVacuum
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2084120"
                        _alarmMsg = "Machine B Vacuum OFF Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.B_IsVacuum(False) = False) Then
                            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak1, False)  '吹氣OFF
                            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak2, False)   '吹氣OFF
                            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak3, False) '吹氣OFF
                            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak4, False)  '吹氣OFF
                            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak5, False)  '吹氣OFF
                            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak6, False) '吹氣OFF
                            SwTimeOut.Restart()
                            TaskStep = enmStep.ElectricCylinderHome
                        End If
                    End If

                    'Case enmStep.ElectricCylinderDown '電動缸下降
                    '    If (TimeOutCheck(DeadTime)) Then
                    '        Return enmMotionStatus.Alarm
                    '    Else
                    '        If (Unit.B_ElectricCylinder(enmDirection.Down)) Then
                    '            SwTimeOut.Restart()
                    '            TaskStep = enmStep.StoperDown
                    '        End If
                    '    End If

                Case enmStep.ElectricCylinderHome '電動缸回Home
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2084111"
                        _alarmMsg = "Machine B Electric Cylinder 'Home' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.B_ElectricCylinder(enmDirection.Home)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.StoperDown
                        End If
                    End If

                Case enmStep.StoperDown   'Stoper下降
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2084113"
                        _alarmMsg = "Machine B Stoper 'Down' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.B_Stoper(enmDirection.Down)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.RollerUnload
                        End If
                    End If

                Case enmStep.RollerUnload 'Roller下料
                    If (TimeOutCheck(MotionTimeOut)) Then
                        Return enmMotionStatus.Alarm
                    Else
                        SwTimeOut.Restart()
                        If auto Then
                            'CvSMEMA(0).IsReadyToSend = True '出料訊號ON
                            Unit.B_Roller.Unload(IRoller.enmSpeed.Normal) 'B機Roller下料
                            TaskStep = enmStep.CheckDespatchSensor
                        Else
                            TaskStep = enmStep.End  '不出料至Unloader
                        End If
                    End If

                Case enmStep.CheckDespatchSensor  '檢查出料Sensor
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2084121"
                        _alarmMsg = "Machine B Exit Sensor Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.B_ExitSensor()) Then
                            SwTimeOut.Restart()
                            CvSMEMA(0).IsReadyToSend = False '出料訊號OFF
                            TaskStep = enmStep.RollerStop
                        End If
                    End If

                Case enmStep.RollerStop   'Roller停止
                    If (TimeOutCheck(SmemaTimeOut)) Then
                        _alarmCode = "Alarm_2080019"
                        _alarmMsg = "SMEMA Unloader Ready OFF Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (CvSMEMA(0).IsUnloaderReady = False) Or (auto = False) Then
                            Unit.B_Roller.Stop() 'B機Roller停止
                            'CvSMEMA(0).IsReadyToSend = False '出料訊號OFF
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    Reset()
                    Return enmMotionStatus.Finish
            End Select

            Return enmMotionStatus.Running
        Catch ex As Exception
            _alarmMsg = ex.Message
            Return enmMotionStatus.Alarm
        End Try
    End Function

    ''' <summary>
    ''' 工作重置
    ''' </summary>
    ''' <remarks>發生異常後須執行重製動作</remarks>
    Public Sub Reset()
        SwTimeOut.Reset()
        Task = enmTask.Null
        TaskStep = enmStep.Start
        Unit.A_Roller.Stop()
        Unit.B_Roller.Stop()
        IsBusy = False
    End Sub

    ''' <summary>
    ''' 判斷動作是否逾時
    ''' </summary>
    ''' <param name="time">毫秒</param>
    Private Function TimeOutCheck(ByVal time As Integer) As Boolean
        If (SwTimeOut.ElapsedMilliseconds > time) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 檢查流道安全
    ''' </summary>
    ''' <returns>true : 安全. false : 危險</returns>
    Private Function SecurityCheck() As Boolean
        'If Unit.B_EntranceSensor Or Unit.B_ExitSensor Or gDICollection.GetState(0, True) = False Then
        If Unit.B_EntranceSensor Or Unit.B_ExitSensor Then
            Return False
        End If
        Return True
    End Function

    Dim SwSpeedChange As New Stopwatch
    Private Function RollerSpeedChange(ByVal mSec As Integer) As Boolean
        SwSpeedChange.Start()
        If (SwSpeedChange.ElapsedMilliseconds > mSec) Then
            SwSpeedChange.Reset()
            Return True
        End If
        Return False
    End Function
End Class

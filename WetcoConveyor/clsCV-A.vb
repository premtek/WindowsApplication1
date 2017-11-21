Imports ProjectIO
Imports ProjectCore

Public Class clsCV_A
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
        ElectricCylinderHome
        'CylinderUp
        'CylinderDown
        StoperUp
        StoperDown
        FrontStoperUp
        FrontStoperDown
        CheckStoperSensor
        RollerLoad
        RollerUnload
        RollerStop
        RollerSpeedChange
        CheckDespatchSensor
        CheckRecieveSensor
        CheckTemperatrue
        [End]

        B_CylinderDown '頂升與Stoper下降
        B_CheckDespatchSensor
        B_CheckRecieveSensor
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
    ''' </summary>      r`
    ReadOnly Property AlarmMessage As String
        Get
            Return _alarmMsg
        End Get
    End Property

    Public ReadOnly Property AlarmID As Integer
        Get
            Return mAlarmCode
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

    Public ByPassMachineB As Boolean

    Dim mAlarmCode As Integer

    Sub New()

    End Sub

    Public Function Initial() As enmMotionStatus
        Try
            Select Case TaskStep
                Case enmStep.Start
                    Task = enmTask.Initial
                    IsBusy = True
                    SwTimeOut.Start()
                    'If (Unit.A_IsVacuum) Then
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
                        _alarmCode = "Alarm_2083109"
                        mAlarmCode = Alarm_2083109
                        _alarmMsg = "Machine A Stoper 'Up' Timeout Or Front Stoper 'Up' Timeout Or Electric Cylinder 'Home' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        Unit.A_Roller.Stop()
                        If (Unit.A_Stoper(clsUnit.enmDirection.Up) And Unit.A_ElectricCylinder(enmDirection.Home) And Unit.A_FrontStoper(clsUnit.enmDirection.Up)) Then
                            If (Unit.A_StoperSensor(False)) Then
                                _alarmCode = "Alarm_2083110"
                                mAlarmCode = Alarm_2083110
                                _alarmMsg = "There Are Products On Machine A"
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
                Case enmStep.Start '起始
                    Task = enmTask.Load
                    IsBusy = True
                    SwTimeOut.Start()
                    If (TimeOutCheck(SmemaTimeOut) Or SecurityCheck() = False) Then
                        _alarmCode = "Alarm_2080011"
                        mAlarmCode = Alarm_2080011
                        _alarmMsg = "SMEMA Loader Ready Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (CvSMEMA(0).IsLoaderReady = True Or auto = False) Then 'Load站是否可供料
                            SwTimeOut.Restart()
                            TaskStep = enmStep.ElectricCylinderHome
                        End If
                    End If

                    'Case enmStep.ElectricCylinderDown '電動缸下降
                    '    If (TimeOutCheck(DeadTime)) Then
                    '        Return enmMotionStatus.Alarm
                    '    Else
                    '        If (Unit.A_ElectricCylinder(enmDirection.Down)) Then
                    '            SwTimeOut.Restart()
                    '            TaskStep = enmStep.ElectricCylinderHome
                    '        End If
                    '    End If

                Case enmStep.ElectricCylinderHome '電動缸回Home
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2083111"
                        mAlarmCode = Alarm_2083111
                        _alarmMsg = "Machine A Electric Cylinder 'Home' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_ElectricCylinder(enmDirection.Home)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.StoperUp
                        End If
                    End If

                Case enmStep.StoperUp 'Stoper上升
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2083112"
                        mAlarmCode = Alarm_2083112
                        _alarmMsg = "Machine A Stoper 'Up' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_Stoper(enmDirection.Up)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.FrontStoperDown
                        End If
                    End If

                Case enmStep.FrontStoperDown '前Stoper下降
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2083113"
                        mAlarmCode = Alarm_2083113
                        _alarmMsg = "Machine A Front Stoper 'Down' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_FrontStoper(enmDirection.Down)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.RollerLoad
                        End If
                    End If

                Case enmStep.RollerLoad   'Roller入料
                    If (TimeOutCheck(SmemaTimeOut)) Then
                        _alarmCode = "Alarm_2080011"
                        mAlarmCode = Alarm_2080011
                        _alarmMsg = "SMEMA Loader Ready Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (CvSMEMA(0).IsLoaderReady Or auto = False) Then
                            If auto Then
                                CvSMEMA(0).IsReadyToRecieve = True '開啟要料訊號
                            End If
                            Unit.A_Roller.Load(IRoller.enmSpeed.Normal) 'A機Roller start
                            SwTimeOut.Restart()
                            TaskStep = enmStep.CheckRecieveSensor
                        End If
                    End If

                Case enmStep.CheckRecieveSensor   '檢查入料Sensor
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2083114"
                        mAlarmCode = Alarm_2083114
                        _alarmMsg = "Machine A Entrance Sensor Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_EntranceSensor(True) Or auto = False) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.RollerSpeedChange
                        End If
                    End If

                Case enmStep.RollerSpeedChange
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2083115"
                        mAlarmCode = Alarm_2083115
                        _alarmMsg = "Machine A Speed Change Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        Dim mSec As UInteger = 0 '減速delay時間, 半自動狀態下mSec = 0
                        If auto Then
                            mSec = RollerSlowTime
                        End If

                        If (RollerSpeedChange(mSec)) Then
                            Unit.A_Roller.Load(IRoller.enmSpeed.Slow) '減速
                            SwTimeOut.Restart()
                            TaskStep = enmStep.CheckStoperSensor
                        End If
                    End If

                Case enmStep.CheckStoperSensor    '檢查Stoper Sensor
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2083116"
                        mAlarmCode = Alarm_2083116
                        _alarmMsg = "Machine A Stoper Sensor Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_StoperSensor(False)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.RollerStop
                        End If
                    End If

                Case enmStep.RollerStop   'Roller停止
                    If (TimeOutCheck(MotionTimeOut)) Then
                        Return enmMotionStatus.Alarm
                    Else
                        Unit.A_Roller.Stop() 'A機Roller停止
                        SwTimeOut.Restart()
                        TaskStep = enmStep.FrontStoperUp
                    End If

                Case enmStep.FrontStoperUp '前Stoper上升
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2083117"
                        mAlarmCode = Alarm_2083117
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
                        _alarmCode = "Alarm_2083118"
                        mAlarmCode = Alarm_2083118
                        _alarmMsg = "Machine A Electric Cylinder 'Up' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_ElectricCylinder(enmDirection.Up)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.VacuumON
                            'TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.VacuumON '吸真空
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, False)  '吹氣OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak2, False) '吹氣OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak3, False) '吹氣OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak4, False) '吹氣OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak5, False) '吹氣OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak6, False) '吹氣OFF

                    gDOCollection.SetState(enmDO.Station2ChuckVacuum, Unit.A_Vacuum(0))   '吸真空ON
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum2, Unit.A_Vacuum(1))   '吸真空ON
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum3, Unit.A_Vacuum(2))  '吸真空ON
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum4, Unit.A_Vacuum(3)) '吸真空ON
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum5, Unit.A_Vacuum(4)) '吸真空ON
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum6, Unit.A_Vacuum(5)) '吸真空ON
                    SwTimeOut.Restart()
                    TaskStep = enmStep.CheckVacuum

                Case enmStep.CheckVacuum
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2083119"
                        mAlarmCode = Alarm_2083119
                        _alarmMsg = "Machine A Vacuum ON Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_IsVacuum(True)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.End
                        End If
                    End If

                Case enmStep.End
                    If (TimeOutCheck(SmemaTimeOut)) Then
                        _alarmCode = "Alarm_2080012"
                        mAlarmCode = Alarm_2080012
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
                        _alarmCode = "Alarm_2080013"
                        mAlarmCode = Alarm_2080013
                        _alarmMsg = "SMEMA Unloader Ready Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        CvSMEMA(0).IsReadyToSend = True '出料訊號ON
                        If (CvSMEMA(0).IsUnloaderReady = True) Or (auto = False) Then   'Unload站是否可收料
                            SwTimeOut.Restart()
                            TaskStep = enmStep.VacuumOFF
                        End If
                    End If

                Case enmStep.VacuumOFF '破真空
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum, False)  '吸真空OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum2, False)  '吸真空OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum3, False) '吸真空OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum4, False) '吸真空OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum5, False)  '吸真空OFF
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum6, False)  '吸真空OFF

                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, True)   '吹氣ON
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak2, True)  '吹氣ON
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak3, True) '吹氣ON
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak4, True)   '吹氣ON
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak5, True)  '吹氣ON
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak6, True)   '吹氣ON

                    SwTimeOut.Restart()
                    TaskStep = enmStep.CheckVacuum

                Case enmStep.CheckVacuum
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2083120"
                        mAlarmCode = Alarm_2083120
                        _alarmMsg = "Machine A Vacuum OFF Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_IsVacuum(False) = False) Then
                            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, False)   '吹氣OFF
                            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak2, False)  '吹氣OFF
                            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak3, False) '吹氣OFF
                            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak4, False)   '吹氣OFF
                            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak5, False)   '吹氣OFF
                            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak6, False)  '吹氣OFF
                            SwTimeOut.Restart()
                            TaskStep = enmStep.ElectricCylinderHome
                        End If
                    End If

                    'Case enmStep.ElectricCylinderDown 'A機電動缸下降, B機電動缸與Stoper下降
                    '    If (TimeOutCheck(DeadTime)) Then
                    '        Return enmMotionStatus.Alarm
                    '    Else
                    '        If (Unit.A_ElectricCylinder(enmDirection.Down) AndAlso Unit.B_ElectricCylinder(enmDirection.Down)) Then
                    '            SwTimeOut.Restart()
                    '            TaskStep = enmStep.StoperDown
                    '        End If
                    '    End If

                Case enmStep.ElectricCylinderHome '電動缸回Home
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2080014"
                        mAlarmCode = Alarm_2080014
                        _alarmMsg = "Machine A/B Electric Cylinder 'Home' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_ElectricCylinder(enmDirection.Home) AndAlso Unit.B_ElectricCylinder(enmDirection.Home, ByPassMachineB)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.StoperDown
                        End If
                    End If

                Case enmStep.StoperDown   'Stoper下降
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2080015"
                        mAlarmCode = Alarm_2080015
                        _alarmMsg = "Machine A/B Stoper 'Down' Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_Stoper(enmDirection.Down) AndAlso Unit.B_Stoper(enmDirection.Down)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.RollerUnload
                        End If
                    End If

                Case enmStep.RollerUnload 'A,B機Roller下料
                    If (TimeOutCheck(MotionTimeOut)) Then
                        Return enmMotionStatus.Alarm
                    Else
                        SwTimeOut.Restart()
                        If auto Then
                            'CvSMEMA(0).IsReadyToSend = True '出料訊號ON
                            If Unit IsNot Nothing Then
                                If Unit.A_Roller IsNot Nothing Then
                                    Unit.A_Roller.Unload(IRoller.enmSpeed.Normal) 'A機Roller下料
                                End If
                                If Unit.B_Roller IsNot Nothing Then
                                    Unit.B_Roller.Unload(IRoller.enmSpeed.Normal) 'B機Roller下料
                                End If
                            End If

                            TaskStep = enmStep.CheckDespatchSensor
                        Else
                            TaskStep = enmStep.End '不出料至Unloader
                        End If
                    End If

                Case enmStep.CheckDespatchSensor  '檢查出料Sensor
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2083121"
                        mAlarmCode = Alarm_2083121
                        _alarmMsg = "Machine A Exit Sensor Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.A_ExitSensor(True)) Then
                            SwTimeOut.Restart()
                            TaskStep = enmStep.B_CheckRecieveSensor
                        End If
                    End If

                Case enmStep.B_CheckRecieveSensor '檢查B機入料Sensor
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2084114"
                        mAlarmCode = Alarm_2084114
                        _alarmMsg = "Machine B Entrance Sensor Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.B_EntranceSensor(True)) Then
                            SwTimeOut.Restart()
                            'Unit.A_Roller.Stop() 'A機Roller停止
                            TaskStep = enmStep.B_CheckDespatchSensor
                        End If
                    End If

                Case enmStep.B_CheckDespatchSensor '檢查B機出料Sensor
                    If (TimeOutCheck(MotionTimeOut)) Then
                        _alarmCode = "Alarm_2084121"
                        mAlarmCode = Alarm_2084121
                        _alarmMsg = "Machine B Exit Sensor Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (Unit.B_ExitSensor(True)) Then
                            SwTimeOut.Restart()
                            CvSMEMA(0).IsReadyToSend = False '出料訊號OFF
                            TaskStep = enmStep.RollerStop
                        End If
                    End If

                Case enmStep.RollerStop   'Roller停止
                    If (TimeOutCheck(SmemaTimeOut)) Then
                        _alarmCode = "Alarm_2080019"
                        mAlarmCode = Alarm_2080019
                        _alarmMsg = "SMEMA Unloader Ready OFF Timeout."
                        Return enmMotionStatus.Alarm
                    Else
                        If (CvSMEMA(0).IsUnloaderReady = False) Or (auto = False) Then
                            'CvSMEMA(0).IsReadyToSend = False '出料訊號OFF
                            If Unit IsNot Nothing Then
                                If Unit.A_Roller IsNot Nothing Then
                                    Unit.A_Roller.Stop() 'A機Roller停止
                                End If
                                If Unit.B_Roller IsNot Nothing Then
                                    Unit.B_Roller.Stop() 'B機Roller停止
                                End If
                            End If

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
        If Unit IsNot Nothing Then
            If Unit.A_Roller IsNot Nothing Then
                Unit.A_Roller.Stop()
            End If
            If Unit.B_Roller IsNot Nothing Then
                Unit.B_Roller.Stop()
            End If
        End If

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
        'If Unit.A_EntranceSensor Or Unit.A_ExitSensor Or gDICollection.GetState(0, True) = False Then
        If Unit.A_EntranceSensor(False) Or Unit.A_ExitSensor(False) Then
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
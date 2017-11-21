Imports ProjectIO
Imports ProjectCore
Imports ProjectMotion

Public Structure ConveyorRegister
    Public boolNoUseStation1 As Boolean             '停用Station1設定   (true:停用 ,false啟用)
    Public boolNoUseStation2 As Boolean             '停用Station2設定   (true:停用 ,false啟用)
    Public boolNoUseStation3 As Boolean             '停用Station3設定   (true:停用 ,false啟用)
    Public intRollSpeed As Integer                  '轉輪 速度設定
    Public boolDisHeater1 As Boolean                '停用Heater1設定   (true:停用 ,false啟用)
    Public boolDisHeater2 As Boolean                '停用Heater2設定   (true:停用 ,false啟用)
    Public boolDisHeater3 As Boolean                '停用Heater3設定   (true:停用 ,false啟用)
    Public boolDisTrayClamper2 As Boolean           '停用側推氣缸2設定   (true:停用 ,false啟用)
    Public dblStation1HeaterSet As Double           'Heater1 加熱溫度設定
    Public intStation2TopLiftSpeed As Integer       'Chuck2 上下移動速度設定
    Public dblStation2TopUpPos As Double            'Chuck2 上點位設定
    Public dblStation2TopDownPos As Double          'Chuck2 下點位設定
    Public dblStation2HeaterSet As Double           'Heater2 加熱溫度設定
    Public dblStation3HeaterSet As Double           'Heater3 加熱溫度設定
End Structure

''' <summary>運行模式</summary>
''' <remarks></remarks>
Public Enum enmConveyorRunMode
    ''' <summary>手動</summary>
    ''' <remarks></remarks>
    Manual = 0
    ''' <summary>自動</summary>
    ''' <remarks></remarks>
    Auto = 1
End Enum

Public Class CConveyorFlow

#Region "Definitions"
    ''' <summary>站的總數量</summary>
    Private Const STATION_TOTALNUMBER As Integer = 3

    ''' <summary>Station 1 的ID </summary>
    Private Const STATION1 As Integer = 0
    ''' <summary>Station 2 的ID </summary>
    Private Const STATION2 As Integer = 1
    ''' <summary>Station 3 的ID </summary>
    Private Const STATION3 As Integer = 2

    ''' <summary>轉輪滾動秒數(預設為1秒) </summary>
    Public Const mRollingSec As Integer = 1
    ''' <summary>偵測Sensor 燈號未亮起秒數(預設為6秒) </summary>
    Public Const mDetectSensorSec As Integer = 6

    ' ''' <summary>前轉輪通訊編號 </summary>
    'Public FrontRoll As Integer = 4
    ' ''' <summary>後轉輪通訊編號 </summary>
    'Public BehindRoll As Integer = 5

    ' ''' <summary>Chuck 2 Motor 通訊編號</summary>
    'Public CMotorID As Integer = 6

    ''' <summary>Station1加熱器 通訊編號</summary>
    Public Heart1 As Integer = enmAxis.Heater1
    ''' <summary>Station2加熱器 通訊編號</summary>
    Public Heart2 As Integer = enmAxis.Heater2
    ''' <summary>Station3加熱器 通訊編號</summary>
    Public Heart3 As Integer = enmAxis.Heater3

    ''' <summary>通訊間隔發步秒數(預設為2秒) </summary>
    Public Const mCommunicationGapSec As Integer = 2

    ''' <summary>轉輪轉動優先權</summary>
    ''' <remarks></remarks>
    Public Enum RollPriority As UInteger
        None = 0           '轉輪不動作
        Rolling = 1        '轉輪轉動
    End Enum

    ''' <summary>[Station 1 計數]</summary>
    Private gStation1StopWatch As New Stopwatch

    ''' <summary>記憶Station 1 初使時間</summary>
    Private mCheckStation1Start As Integer

    ''' <summary>[Station 2 計數]</summary>
    Private gStation2StopWatch As New Stopwatch

    ''' <summary>記憶Station 2 初使時間</summary>
    Private mCheckStation2Start As Integer

    ''' <summary>[Station 3 計數]</summary>
    Private gStation3StopWatch As New Stopwatch

    ''' <summary>記憶Station 3 初使時間</summary>
    Private mCheckStation3Start As Integer

    ''' <summary>[Home 計數]</summary>
    Private gHomeStopWatch As New Stopwatch

    ''' <summary>記憶Home初使時間</summary>
    Private mCheckHomeStart As Integer

    ''' <summary>[Roll 計數]</summary>
    Private gRollStopWatch As New Stopwatch

    ''' <summary>通知點膠機點膠</summary>
    Public bAckDispensrTrayReady As Boolean = False

    ''' <summary>回傳點膠機點膠完成</summary>
    Public bBckDispensrFinish As Boolean = False

#End Region

#Region " Properties "
    Public ConveryorName As String                                      '[說明]ConveryorName
    'Public intHomeTask As Integer = 1                                   '[說明]回Home任務
    'Public intArrayTask(STATION_TOTALNUMBER - 1) As Integer             '[說明] Station1,2,3動作流程任務
    Public gStationID(STATION_TOTALNUMBER - 1) As StationState          '[說明] 設定Station 狀態
    Public StationRoll(STATION_TOTALNUMBER - 1) As RollPriority         '[說明]每一站轉輪轉動優先權
    ''' <summary>轉輪滾動任務 </summary>
    Public intRollingTask As Integer = 1
    ''' <summary>轉輪滾動停止任務 </summary>
    Public intStopRollTask As Integer = 1
    Public boolTrayRecive As Boolean = False                          '[說明]測試用略過Load Tray
    Public boolUnloadTray As Boolean = False                          '[說明]測試略過UnLoad Tray
    Public boolIgnoreSensor As Boolean = False                        '[說明]測試用略過Sensor Detect
    ''' <summary>運行模式</summary>
    ''' <remarks></remarks>
    Public RunMode As enmConveyorRunMode

    Public Structure StationState                         '[說明]此站狀態   
        Public Working As Boolean                         '[說明]此站別是否使用中
        Public ReceiveTray As Boolean                     '[說明]接收Tray盤
        Public ReceiveTrayFinish As Boolean               '[說明]接收Tray盤完成
        Public ChuckPar As gCParameters                   '[說明]此站Chuck不使用
    End Structure


    Public Structure gCParameters                        '[說明]此站Chuck參數 
        Public Disable As Boolean                        '[說明]停用Chuck上下
        Public DisHeater As Boolean                      '[說明]停用Heater溫度設定&偵測
        Public DisTrayClamp As Boolean                   '[說明]停用側推氣缸 (目前只有Station2 有安裝)
        Public USEMotor As Boolean                       '[說明]此站Chuck是使用馬達or氣缸
        Public dbTargetTemperature As Double             '[說明]此站Chuck的達到的目標溫度
        Public dbMotorUpPos As Double                    '[說明]馬達上升點位
        Public dbMotorDownPos As Double                  '[說明]馬達下降點位
    End Structure

#End Region

#Region " Methods "

    ' ''' <summary>
    ' ''' 取得歸零任務
    ' ''' </summary>
    ' ''' <returns>回傳現在任務值</returns>
    ' ''' <remarks></remarks>
    'Public Function GetHomeTask() As String
    '    Return intHomeTask.ToString()
    'End Function

    ' ''' <summary>
    ' ''' 取得站別 1~3 的現在任務
    ' ''' </summary>
    ' ''' <param name="intStationID">站別編號</param>
    ' ''' <returns>回傳現在任務值</returns>
    ' ''' <remarks></remarks>
    'Public Function GetStationTask(ByVal intStationID As Integer) As String

    '    Select Case intStationID
    '        Case 1
    '            Return intArrayTask(STATION1).ToString()
    '        Case 2
    '            Return intArrayTask(STATION2).ToString()
    '        Case 3
    '            Return intArrayTask(STATION3).ToString()
    '    End Select

    '    Return "0000"       '未選到站别
    'End Function

    ''' <summary>
    ''' 通知點膠機 Tray準備完成訊號
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetTrayReady As Boolean
        Get
            Return bAckDispensrTrayReady
        End Get
    End Property

    ''' <summary>
    ''' 等待點膠機點膠完成訊號
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property DispensorFinish As Boolean
        Set(value As Boolean)
            bBckDispensrFinish = value
        End Set
    End Property

    ''' <summary>
    ''' 停用Station Chuek站別
    ''' </summary>
    ''' <param name="intStationID">站別編號(1~3)</param>
    ''' <param name="valve">停用/啟用</param>
    ''' <remarks></remarks>
    Public Sub DisableStation(ByVal intStationID As Integer, ByVal valve As Boolean)
        gStationID(intStationID - 1).ChuckPar.Disable = valve
    End Sub

    ''' <summary>
    ''' 停用加熱器站別
    ''' </summary>
    ''' <param name="intStationID">站別編號(1~3)</param>
    ''' <param name="valve">停用/啟用</param>
    ''' <remarks></remarks>
    Public Sub DisableHeater(ByVal intStationID As Integer, ByVal valve As Boolean)
        gStationID(intStationID - 1).ChuckPar.DisHeater = valve
    End Sub

    ''' <summary>
    ''' 停用側推氣缸站別
    ''' </summary>
    ''' <param name="intStationID">站別編號(1~3)</param>
    ''' <param name="valve">停用/啟用</param>
    ''' <remarks></remarks>
    Public Sub DisableTrayClamper(ByVal intStationID As Integer, ByVal valve As Boolean)
        gStationID(intStationID - 1).ChuckPar.DisTrayClamp = valve
    End Sub

    ''' <summary>
    ''' 站別內Chuck為使用氣缸或馬達
    ''' </summary>
    ''' <param name="intStationID">站別編號(1~3)</param>
    ''' <param name="sChoose">選擇馬達或氣缸</param>
    ''' <remarks></remarks>
    Public Sub MotorORCylinder(ByVal intStationID As Integer, ByVal sChoose As String)

        Dim ID As Integer = intStationID - 1

        Select Case sChoose
            Case "Motor"
                gStationID(ID).ChuckPar.USEMotor = True
            Case Else
                gStationID(ID).ChuckPar.USEMotor = False
        End Select
    End Sub

    ''' <summary>
    ''' 轉輪轉動
    ''' </summary>
    ''' <param name="StationID"> 站別陣列0~2 </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Rolling(ByVal StationID As Integer) As Boolean
        Dim stopWatch As New Stopwatch
        If StationRoll(StationID) = RollPriority.Rolling Then
            Return True
        End If
        StationRoll(StationID) = RollPriority.Rolling
        Select Case intRollingTask
            Case 1
                intRollingTask = 10
            Case 10
                If gCMotion.VelMove(enmAxis.Conveyor1, eDirection.Negative) = False Then
                    'If gCMotion.JogPlus(FrontRoll, enmDirection.Negative) = False Then
                    gEqpMsg.AddHistoryAlarm("Error_1037000", "frmConveyor-Rolling", , gMsgHandler.GetMessage(Error_1037000), eMessageLevel.Error)
                Else
                    stopWatch.Restart()
                    intRollingTask = 20
                End If

            Case 20
                If gCMotion.GetCmdStatus(enmAxis.Conveyor1) = CommandStatus.Sucessed Then
                    'If gCMotion.GetCmdStatus(FrontRoll) = CommandStatus.Sucessed Then
                    intRollingTask = 30
                ElseIf stopWatch.ElapsedMilliseconds > 1000 Then
                    intRollingTask = 10
                End If

            Case 30
                If gCMotion.VelMove(enmAxis.Conveyor2, eDirection.Positive) = False Then
                    gEqpMsg.AddHistoryAlarm("Error_1037000", "frmConveyor-Rolling", , gMsgHandler.GetMessage(Error_1037000), eMessageLevel.Error)
                Else
                    stopWatch.Restart()
                    intRollingTask = 40
                End If


            Case 40
                If gCMotion.GetCmdStatus(enmAxis.Conveyor2) = CommandStatus.Sucessed Then
                    'If gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed Then
                    intRollingTask = 50
                ElseIf stopWatch.ElapsedMilliseconds > 1000 Then
                    intRollingTask = 30
                End If
            Case 50
                intRollingTask = 1
                Return True
        End Select

        Return False

        Exit Function

        'Select Case StationID
        '    Case STATION1
        '        If StationRoll(STATION2) = RollPriority.None And StationRoll(STATION3) = RollPriority.None Then
        '            'Station 1站使用
        '            StationRoll(STATION1) = RollPriority.Rolling
        '            'Station 1滾動

        '            Select Case intRollingTask
        '                Case 1
        '                    intRollingTask = 10
        '                Case 10
        '                    If gCMotion.JogPlus(FrontRoll, enmDirection.Negative) = False Then
        '                        gEqpMsg.AddHistoryAlarm("A0104", "frmConveyor-Rolling")
        '                    Else
        '                        stopWatch.Restart()
        '                        intRollingTask = 20
        '                    End If

        '                Case 20
        '                    If gCMotion.GetCmdStatus(FrontRoll) = CommandStatus.Sucessed Then
        '                        intRollingTask = 30
        '                    ElseIf stopWatch.ElapsedMilliseconds > 1000 Then
        '                        intRollingTask = 10
        '                    End If

        '                Case 30

        '                    If gCMotion.JogPlus(BehindRoll, enmDirection.Positive) = False Then
        '                        gEqpMsg.AddHistoryAlarm("A0104", "frmConveyor-Rolling")
        '                    Else
        '                        stopWatch.Restart()
        '                        intRollingTask = 40
        '                    End If


        '                Case 40
        '                    If gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed Then
        '                        intRollingTask = 50
        '                    ElseIf stopWatch.ElapsedMilliseconds > 1000 Then
        '                        intRollingTask = 30
        '                    End If
        '                Case 50
        '                    intRollingTask = 1
        '                    Return True
        '            End Select
        '        End If


        '    Case STATION2
        '        If StationRoll(STATION1) = RollPriority.None And StationRoll(STATION3) = RollPriority.None Then
        '            'Station 2站使用
        '            StationRoll(STATION2) = RollPriority.Rolling
        '            'Station 2滾動
        '             Select Case intRollingTask
        '                Case 1
        '                    intRollingTask = 10
        '                Case 10
        '                    If gCMotion.JogPlus(FrontRoll, enmDirection.Negative) = False Then
        '                        gEqpMsg.AddHistoryAlarm("A0104", "frmConveyor-Rolling")
        '                    Else
        '                        stopWatch.Restart()
        '                        intRollingTask = 20
        '                    End If

        '                Case 20
        '                    If gCMotion.GetCmdStatus(FrontRoll) = CommandStatus.Sucessed Then
        '                        intRollingTask = 30
        '                    ElseIf stopWatch.ElapsedMilliseconds > 1000 Then
        '                        intRollingTask = 10
        '                    End If

        '                Case 30

        '                    If gCMotion.JogPlus(BehindRoll, enmDirection.Positive) = False Then
        '                        gEqpMsg.AddHistoryAlarm("A0104", "frmConveyor-Rolling")
        '                    Else
        '                        stopWatch.Restart()
        '                        intRollingTask = 40
        '                    End If


        '                Case 40
        '                    If gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed Then
        '                        intRollingTask = 50
        '                    ElseIf stopWatch.ElapsedMilliseconds > 1000 Then
        '                        intRollingTask = 30
        '                    End If
        '                Case 50
        '                    intRollingTask = 1
        '                    Return True
        '            End Select
        '        End If


        '    Case STATION3
        '        If StationRoll(STATION1) = RollPriority.None And StationRoll(STATION2) = RollPriority.None Then
        '            'Station 3站使用
        '            StationRoll(STATION3) = RollPriority.Rolling
        '            'Station 3滾動

        '             Select Case intRollingTask
        '                Case 1
        '                    intRollingTask = 10
        '                Case 10
        '                    If gCMotion.JogPlus(FrontRoll, enmDirection.Negative) = False Then
        '                        gEqpMsg.AddHistoryAlarm("A0104", "frmConveyor-Rolling")
        '                    Else
        '                        stopWatch.Restart()
        '                        intRollingTask = 20
        '                    End If

        '                Case 20
        '                    If gCMotion.GetCmdStatus(FrontRoll) = CommandStatus.Sucessed Then
        '                        intRollingTask = 30
        '                    ElseIf stopWatch.ElapsedMilliseconds > 1000 Then
        '                        intRollingTask = 10
        '                    End If

        '                Case 30

        '                    If gCMotion.JogPlus(BehindRoll, enmDirection.Positive) = False Then
        '                        gEqpMsg.AddHistoryAlarm("A0104", "frmConveyor-Rolling")
        '                    Else
        '                        stopWatch.Restart()
        '                        intRollingTask = 40
        '                    End If


        '                Case 40
        '                    If gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed Then
        '                        intRollingTask = 50
        '                    ElseIf stopWatch.ElapsedMilliseconds > 1000 Then
        '                        intRollingTask = 30
        '                    End If
        '                Case 50
        '                    intRollingTask = 1
        '                    Return True
        '            End Select
        '        End If
        'End Select

        'Return False
    End Function

    ''' <summary>
    ''' 轉輪停止轉動
    ''' </summary>
    ''' <param name="StationID">站別陣列0~2</param>
    ''' <remarks></remarks>
    Private Function StopRoll(ByVal StationID As Integer) As Boolean
        Dim stopWatch As New Stopwatch
        StationRoll(StationID) = RollPriority.None
        If StationRoll(STATION1) = RollPriority.None And StationRoll(STATION2) = RollPriority.None And StationRoll(STATION3) = RollPriority.None Then
            '停止轉動
            Select Case intStopRollTask
                Case 1
                    intStopRollTask = 10
                Case 10
                    gCMotion.EmgStop(enmAxis.Conveyor1)
                    'gCMotion.EmgStop(FrontRoll)
                    stopWatch.Restart()
                    intStopRollTask = 20

                Case 20
                    If gCMotion.GetCmdStatus(enmAxis.Conveyor1) = CommandStatus.Sucessed Then
                        'If gCMotion.GetCmdStatus(FrontRoll) = CommandStatus.Sucessed Then
                        intStopRollTask = 30
                    ElseIf stopWatch.ElapsedMilliseconds > 1000 Then '逾時,重發命令
                        gEqpMsg.AddHistoryAlarm("Error_1037000", "StopRoll", , gMsgHandler.GetMessage(Error_1037000), eMessageLevel.Error)
                    End If

                Case 30
                    gCMotion.EmgStop(enmAxis.Conveyor2)
                    'gCMotion.EmgStop(BehindRoll)
                    stopWatch.Restart()
                    intStopRollTask = 40

                Case 40
                    If gCMotion.GetCmdStatus(enmAxis.Conveyor2) = CommandStatus.Sucessed Then
                        'If gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed Then
                        intStopRollTask = 50
                    ElseIf stopWatch.ElapsedMilliseconds > 1000 Then '逾時,重發命令
                        gEqpMsg.AddHistoryAlarm("Error_1037000", "BehindRoll", , gMsgHandler.GetMessage(Error_1037000), eMessageLevel.Error)
                    End If

                Case 50
                    System.Threading.Thread.CurrentThread.Join(100)
                    intStopRollTask = 60
                Case 60
                    Select Case StationID
                        Case STATION1
                            StationRoll(STATION1) = RollPriority.None
                        Case STATION2
                            StationRoll(STATION2) = RollPriority.None
                        Case STATION3
                            StationRoll(STATION3) = RollPriority.None
                    End Select

                    intStopRollTask = 1
                    Return True
            End Select
        Else
            Return True
        End If

        Return False

        Exit Function

        ''停止轉動
        'Select Case intStopRollTask
        '    Case 1
        '        intStopRollTask = 10
        '    Case 10
        '        gCMotion.EmgStop(FrontRoll)
        '        stopWatch.Restart()
        '        intStopRollTask = 20

        '    Case 20
        '        If gCMotion.GetCmdStatus(FrontRoll) = CommandStatus.Sucessed Then
        '            intStopRollTask = 30
        '        ElseIf stopWatch.ElapsedMilliseconds > 1000 Then '逾時,重發命令
        '            gEqpMsg.AddHistoryAlarm("", "無法停止FrontRoll")
        '        End If

        '    Case 30
        '        gCMotion.EmgStop(BehindRoll)
        '        stopWatch.Restart()
        '        intStopRollTask = 40

        '    Case 40
        '        If gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed Then
        '            intStopRollTask = 50
        '        ElseIf stopWatch.ElapsedMilliseconds > 1000 Then '逾時,重發命令
        '            gEqpMsg.AddHistoryAlarm("", "無法停止BehindRoll")
        '        End If

        '    Case 50
        '        System.Threading.Thread.CurrentThread.Join(100)
        '        intStopRollTask = 60
        '    Case 60
        '        Select Case StationID
        '            Case STATION1
        '                StationRoll(STATION1) = RollPriority.None
        '            Case STATION2
        '                StationRoll(STATION2) = RollPriority.None
        '            Case STATION3
        '                StationRoll(STATION3) = RollPriority.None
        '        End Select

        '        intStopRollTask = 1
        '        Return True
        'End Select


        'Return False
    End Function

    ''' <summary>[Is TimeOut]</summary>
    ''' <param name="iStopWatch">計數時間</param>
    ''' <param name="iTime">開始時間</param>
    ''' <param name="iTimeOutTimer">限制總時間</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsTimeOut(ByVal iStopWatch As Stopwatch, ByVal iTime As Double, ByVal iTimeOutTimer As Double) As Boolean
        If Math.Abs(iTime - iStopWatch.ElapsedMilliseconds) > (iTimeOutTimer * 1000) Then

            iStopWatch.Stop()
            Return True
        Else
            Return False
        End If
    End Function


    ''' <summary>
    ''' Chuck站 馬達上昇點位設定
    ''' </summary>
    ''' <param name="stationID">站別1~3</param>
    ''' <param name="pos">點位</param>
    ''' <remarks></remarks>
    Public Sub SetMotorUpPos(ByVal stationID As Integer, ByVal pos As Double)
        Dim id As Integer = stationID - 1

        Select Case id
            Case STATION1
                gStationID(STATION1).ChuckPar.dbMotorUpPos = pos
            Case STATION2
                gStationID(STATION2).ChuckPar.dbMotorUpPos = pos
            Case STATION3
                gStationID(STATION3).ChuckPar.dbMotorUpPos = pos
        End Select

    End Sub

    ''' <summary>
    ''' Chuck 馬達下降點位設定
    ''' </summary>
    ''' <param name="stationID">站別1~3</param>
    ''' <param name="pos">點位</param>
    ''' <remarks></remarks>
    Public Sub SetMotorDownPos(ByVal stationID As Integer, ByVal pos As Double)
        Dim id As Integer = stationID - 1

        Select Case id
            Case STATION1
                gStationID(STATION1).ChuckPar.dbMotorDownPos = pos
            Case STATION2
                gStationID(STATION2).ChuckPar.dbMotorDownPos = pos
            Case STATION3
                gStationID(STATION3).ChuckPar.dbMotorDownPos = pos
        End Select
    End Sub

    ''' <summary>
    ''' '設定 Chuck 加熱器目標溫度
    ''' </summary>
    ''' <param name="stationID">站別1~3</param>
    ''' <param name="valve">目標溫度</param>
    ''' <remarks></remarks>
    Public Sub SetTargetTemperature(ByVal stationID As Integer, ByVal valve As Double)
        Dim id As Integer = stationID - 1
        gStationID(id).ChuckPar.dbTargetTemperature = valve
    End Sub


#End Region

#Region " Function "

    '''' <summary>Conveyor Home Reset</summary>
    'Public Sub HomeReset()
    '    intHomeTask = 1
    '    intRollingTask = 1
    '    intStopRollTask = 1
    'End Sub

    '''' <summary>Home 動作流程</summary>
    'Public Function Home(ByRef sys As sSysParam) As Boolean
    '    Return ConveyorHome(sys)
    'End Function

    ''' <summary>Conveyor Reset</summary>
    Public Sub Reset()
        'intArrayTask(STATION1) = 1
        'intArrayTask(STATION2) = 1
        'intArrayTask(STATION3) = 1

        intRollingTask = 1
        intStopRollTask = 1

        boolTrayRecive = False
        boolUnloadTray = False

        gStationID(STATION1).Working = False
        gStationID(STATION2).Working = False
        gStationID(STATION3).Working = False

        gStationID(STATION1).ReceiveTray = False
        gStationID(STATION2).ReceiveTray = False
        gStationID(STATION3).ReceiveTray = False

        gStationID(STATION1).ReceiveTrayFinish = False
        gStationID(STATION2).ReceiveTrayFinish = False
        gStationID(STATION3).ReceiveTrayFinish = False

        StationRoll(STATION1) = RollPriority.None
        StationRoll(STATION2) = RollPriority.None
        StationRoll(STATION3) = RollPriority.None
    End Sub

    ' ''' <summary>Conveyor 動作流程</summary>
    'Public Sub Action()

    'End Sub

    ''' <summary>滾輪速度形式設定</summary>
    ''' <param name="speed">低/中/高 </param>
    ''' <remarks></remarks>
    Public Sub SetRollSpeedType(ByVal speed As SpeedType)
        Select Case speed
            Case SpeedType.Fast
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(FrontRoll) = CommandStatus.Sucessed

                gCMotion.SetVelHigh(enmAxis.Conveyor1, 1500)
                'gCMotion.SetVelHigh(FrontRoll, 1500)
                System.Threading.Thread.CurrentThread.Join(100)
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(FrontRoll) = CommandStatus.Sucessed
                'System.Threading.Thread.CurrentThread.Join(100)
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed
                System.Threading.Thread.CurrentThread.Join(100)
                gCMotion.SetVelHigh(enmAxis.Conveyor2, 1500)
                'gCMotion.SetVelHigh(BehindRoll, 1500)
                System.Threading.Thread.CurrentThread.Join(100)
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed
                'System.Threading.Thread.CurrentThread.Join(100)
            Case SpeedType.Medium
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(FrontRoll) = CommandStatus.Sucessed
                'System.Threading.Thread.CurrentThread.Join(100)
                gCMotion.SetVelHigh(enmAxis.Conveyor1, 1000)
                'gCMotion.SetVelHigh(FrontRoll, 1000)
                System.Threading.Thread.CurrentThread.Join(100)
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed
                'System.Threading.Thread.CurrentThread.Join(100)
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed
                System.Threading.Thread.CurrentThread.Join(100)
                gCMotion.SetVelHigh(enmAxis.Conveyor2, 1000)
                'gCMotion.SetVelHigh(BehindRoll, 1000)
                System.Threading.Thread.CurrentThread.Join(100)
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed
                'System.Threading.Thread.CurrentThread.Join(100)
            Case SpeedType.Slow
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(FrontRoll) = CommandStatus.Sucessed
                'System.Threading.Thread.CurrentThread.Join(100)
                gCMotion.SetVelHigh(enmAxis.Conveyor1, 500)
                'gCMotion.SetVelHigh(FrontRoll, 500)
                System.Threading.Thread.CurrentThread.Join(100)
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed
                'System.Threading.Thread.CurrentThread.Join(100)
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed
                System.Threading.Thread.CurrentThread.Join(100)
                gCMotion.SetVelHigh(enmAxis.Conveyor2, 500)
                'gCMotion.SetVelHigh(BehindRoll, 500)
                System.Threading.Thread.CurrentThread.Join(100)

                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(BehindRoll) = CommandStatus.Sucessed
                'System.Threading.Thread.CurrentThread.Join(100)
        End Select


    End Sub


    ''' <summary>Chuck2 馬達速度形式設定</summary>
    ''' <param name="speed">低/中/高 </param>
    ''' <remarks></remarks>
    Public Sub SetMotorSpeedType(ByVal speed As SpeedType)
        Select Case speed
            Case SpeedType.Fast
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(CMotorID) = CommandStatus.Sucessed

                gCMotion.SetVelHigh(enmAxis.Station2, 3000)
                'gCMotion.SetVelHigh(CMotorID, 3000)
                System.Threading.Thread.CurrentThread.Join(100)
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(CMotorID) = CommandStatus.Sucessed

            Case SpeedType.Medium
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(CMotorID) = CommandStatus.Sucessed

                gCMotion.SetVelHigh(enmAxis.Station2, 2000)
                'gCMotion.SetVelHigh(CMotorID, 2000)
                System.Threading.Thread.CurrentThread.Join(100)
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(CMotorID) = CommandStatus.Sucessed

            Case SpeedType.Slow
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(CMotorID) = CommandStatus.Sucessed

                gCMotion.SetVelHigh(enmAxis.Station2, 1000)
                'gCMotion.SetVelHigh(CMotorID, 1000)
                System.Threading.Thread.CurrentThread.Join(100)
                'Do
                '    Application.DoEvents()
                'Loop Until gCMotion.GetCmdStatus(CMotorID) = CommandStatus.Sucessed

        End Select


    End Sub

    ''' <summary>
    '''  Conveyor 回原點動作
    ''' </summary>
    ''' <returns>回傳完成/未完成</returns>
    ''' <remarks></remarks>
    Public Function ConveyorHome(ByRef sys As sSysParam) As enmRunStatus
        With sys

            Select Case .SysNum
                Case 1     '[說明] 初始化
                    gHomeStopWatch.Reset()
                    gStation1StopWatch.Reset()
                    gStation2StopWatch.Reset()
                    gStation3StopWatch.Reset()
                    gDOCollection.SetState(enmDO.Station2Unlock, True) '解除Z軸剎車
                    .SysNum = 1000

                Case 1000  '[說明]轉輪停止轉動
                    If StopRoll(STATION1) = True Then

                        Call gHomeStopWatch.Restart()
                        mCheckHomeStart = gHomeStopWatch.ElapsedMilliseconds
                        .SysNum = 4000
                    End If




                Case 4000  '[說明]Chuck(1,2,3)加熱器關閉/真空關閉/阻擋氣缸下降，Chuck 2料盤氣缸夾持開(側推氣缸)
                    '加熱器開關
                    gDOCollection.SetState(enmDO.HeaterPower, False)

                    'Chuck(1,2,3)真空關閉
                    gDOCollection.SetState(enmDO.Station1ChuckVacuum, False)
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum, False)
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum, False)

                    'Chuck(1,2,3)加熱器關閉
                    gDOCollection.SetState(enmDO.Station1Heating, False)
                    gDOCollection.SetState(enmDO.Station2Heating, False)
                    gDOCollection.SetState(enmDO.Station3Heating, False)

                    '站(1,2,3) 阻擋氣缸下降(閘門)
                    gDOCollection.SetState(enmDO.Station1StopperUpDown, False)
                    gDOCollection.SetState(enmDO.Station2StopperUp, False)
                    gDOCollection.SetState(enmDO.Station3StopperUp, False)

                    'Chuck 2 料盤氣缸夾持開(側推)
                    gDOCollection.SetState(enmDO.TrayClamper, False)

                    '站(1,2,3)阻擋氣缸下偵測
                    Dim b1 As Boolean = gDICollection.GetState(enmDI.Station1StopperDownReady, True)
                    Dim b2 As Boolean = gDICollection.GetState(enmDI.Station2StopperDownReady, True)
                    Dim b3 As Boolean = gDICollection.GetState(enmDI.Station3StopperDownReady, True)
                    '料盤氣缸夾持開偵測
                    Dim b4 As Boolean = True
                    'Dim b4 As Boolean = gDICollection.GetState(enmDI.TrayClamperOffReady, True)

                    If (b1 And b2 And b3 And b4) Then
                        .SysNum = 3000
                    End If

                    If IsTimeOut(gHomeStopWatch, mCheckHomeStart, mDetectSensorSec) = True Then
                        If b1 = False Then                        '站(1)阻擋氣缸下偵測
                            gEqpMsg.AddHistoryAlarm("Alarm_2037000", "ConveyorHome", , gMsgHandler.GetMessage(Alarm_2037000), eMessageLevel.Alarm)
                            Return enmRunStatus.Alarm
                        End If
                        If b2 = False Then                        '站(2)阻擋氣缸下偵測
                            gEqpMsg.AddHistoryAlarm("Alarm_2037100", "ConveyorHome", , gMsgHandler.GetMessage(Alarm_2037100), eMessageLevel.Alarm)
                            Return enmRunStatus.Alarm
                        End If
                        If b3 = False Then                        '站(3)阻擋氣缸下偵測
                            gEqpMsg.AddHistoryAlarm("Alarm_2037200", "ConveyorHome", , gMsgHandler.GetMessage(Alarm_2037200), eMessageLevel.Alarm)
                            Return enmRunStatus.Alarm
                        End If
                        If b4 = False Then                        '站(2)料盤氣缸夾持開(側推)
                            gEqpMsg.AddHistoryAlarm("Alarm_2037109", "ConveyorHome", , gMsgHandler.GetMessage(Alarm_2037109), eMessageLevel.Alarm)
                            Return enmRunStatus.Alarm
                        End If
                    End If

                Case 3000
                    Call gHomeStopWatch.Restart()
                    mCheckHomeStart = gHomeStopWatch.ElapsedMilliseconds
                    .SysNum = 5000

                Case 5000  '[說明]氣缸(1,3)在下定位

                    '站(1,3)頂升氣缸下降
                    gDOCollection.SetState(enmDO.Station1TopLiftUpDown, False)
                    gDOCollection.SetState(enmDO.Station3TopLiftUpDown, False)

                    '站(1,3)頂升氣缸下偵測
                    Dim b1 As Boolean = gDICollection.GetState(enmDI.Station1TopLiftDownReady, True)
                    Dim b3 As Boolean = gDICollection.GetState(enmDI.Station3TopLiftDownReady, True)

                    If b1 And b3 Then
                        .SysNum = 5200
                    End If

                    If IsTimeOut(gHomeStopWatch, mCheckHomeStart, mDetectSensorSec) = True Then
                        If b1 = False Then    '站(1)頂升氣缸下偵測
                            gEqpMsg.AddHistoryAlarm("Alarm_2037002", "ConveyorHome", .SysNum, gMsgHandler.GetMessage(Alarm_2037002), eMessageLevel.Alarm)
                            Return enmRunStatus.Alarm
                        End If

                        If b3 = False Then   '站(3)頂升氣缸下偵測
                            gEqpMsg.AddHistoryAlarm("Alarm_2037202", "ConveyorHome", .SysNum, gMsgHandler.GetMessage(Alarm_2037202), eMessageLevel.Alarm)
                            Return enmRunStatus.Alarm
                        End If
                    End If

                Case 5200 '[說明]Chuck(2)馬達回Home
                    If gCMotion.GetCmdStatus(enmAxis.Station2) = CommandStatus.Sucessed Then
                        'If gCMotion.GetCmdStatus(CMotorID) = CommandStatus.Sucessed Then
                        gCMotion.Home(enmAxis.Station2)
                        'gCMotion.Home(CMotorID)
                        .SysNum = 5600
                    End If

                    'Mobus 要下兩次命令才會正確
                Case 5600 '[說明]取得Chuck(2)馬達歸零訊號完成

                    'If gCMotion.GetCmdStatus(CMotorID) = CommandStatus.Sucessed Then

                    'End If

                    If gCMotion.HomeFinish(enmAxis.Station2) = CommandStatus.Sucessed Then
                        'If gCMotion.HomeFinish(CMotorID) = CommandStatus.Sucessed Then
                        'System.Threading.Thread.CurrentThread.Join(100)
                        gHomeStopWatch.Restart()
                        .SysNum = 5700
                    End If

                Case 5700
                    If IsTimeOut(gHomeStopWatch, 0, 200) = False Then '等待100ms
                        Return enmRunStatus.Running
                    End If
                    'If gCMotion.GetCmdStatus(CMotorID) = CommandStatus.Sucessed Then

                    'End If

                    'System.Threading.Thread.CurrentThread.Join(100)

                    If gCMotion.HomeFinish(enmAxis.Station2) = CommandStatus.Sucessed Then
                        'If gCMotion.HomeFinish(CMotorID) = CommandStatus.Sucessed Then
                        .SysNum = 5800
                    End If

                Case 5800
                    .SysNum = 7000


                Case 7000  '[說明]偵測Station(1,2,3)是否有Tray盤，Yes發警報
                    Dim b1 As Boolean = gDICollection.GetState(enmDI.Station1TrayReady, True)
                    Dim b2 As Boolean = gDICollection.GetState(enmDI.Station2TrayReady, True)
                    Dim b3 As Boolean = gDICollection.GetState(enmDI.Station3TrayReady, True)
                    If (b1 = True Or b2 = True Or b3 = True) Then
                        If b1 = True Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2037004", "ConveyorHome", .SysNum, gMsgHandler.GetMessage(Alarm_2037004), eMessageLevel.Alarm)
                            Return enmRunStatus.Alarm
                        End If

                        If b2 = True Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2037104", "ConveyorHome", .SysNum, gMsgHandler.GetMessage(Alarm_2037104), eMessageLevel.Alarm)
                            Return enmRunStatus.Alarm
                        End If

                        If b3 = True Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2037204", "ConveyorHome", .SysNum, gMsgHandler.GetMessage(Alarm_2037204), eMessageLevel.Alarm)
                            Return enmRunStatus.Alarm
                        End If
                    Else
                        .SysNum = 8000
                    End If

                Case 8000  '[說明]
                    .SysNum = 9000

                Case 9000  '[說明]動作流程任務初始化

                    Reset()   '動作流程任務初始化

                    .SysNum = 9999

                Case 9999
                    Return enmRunStatus.Finish
            End Select

            Return enmRunStatus.Running

        End With
    End Function

    ''' <summary>
    ''' Conveyor Station1 動作流程
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConveyorStation1(ByRef sys As sSysParam) As enmRunStatus
        With sys

            Select Case .SysNum
                Case 1  '[說明] 初始化
                    Call gStation1StopWatch.Restart()
                    mCheckStation1Start = gStation1StopWatch.ElapsedMilliseconds
                    .SysNum = 1000

                Case 1000 '阻擋氣缸上升(閘門關閉)

                    '站(1) 阻擋氣缸上升(閘門)
                    gDOCollection.SetState(enmDO.Station1StopperUpDown, True)

                    Dim b1 As Boolean = gDICollection.GetState(enmDI.Station1StopperUpReady, True)
                    If b1 = True Then
                        .SysNum = 1100
                    End If

                    If IsTimeOut(gStation1StopWatch, mCheckStation1Start, mDetectSensorSec) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2037000", "ConveyorStation1", .SysNum, gMsgHandler.GetMessage(Alarm_2037000), eMessageLevel.Alarm)
                        Return enmRunStatus.Alarm
                    End If

                Case 1100 '[說明] 是否啟動加熱器

                    If gStationID(STATION1).ChuckPar.DisHeater = True Then    '[說明]停用加熱器
                        gDOCollection.SetState(enmDO.HeaterPower, False)
                        gDOCollection.SetState(enmDO.Station1Heating, False)
                        .SysNum = 2000
                    Else                                                      '[說明]啟用加熱器

                        gDOCollection.SetState(enmDO.HeaterPower, True)
                        gDOCollection.SetState(enmDO.Station1Heating, True)

                        '設定加熱器溫度                    
                        .SysNum = 1200


                    End If

                Case 1200  '[說明]設定加熱器溫度

                    Dim intTotalTemp As Double = gStationID(STATION1).ChuckPar.dbTargetTemperature
                    Dim index As Integer = enmAxis.Heater1
                    If gCMotion.SetHeaterSV(index, intTotalTemp) = CommandStatus.Sending Then
                        .SysNum = 2000    '設定完成

                    Else                  '設定未完成
                        Call gStation1StopWatch.Restart()
                        mCheckStation1Start = gStation1StopWatch.ElapsedMilliseconds
                        .SysNum = 1300
                    End If

                Case 1300 '[說明]通訊間隔2秒後再發一次
                    If IsTimeOut(gStation1StopWatch, mCheckStation1Start, mCommunicationGapSec) = True Then
                        .SysNum = 1200
                    End If

                Case 2000 '[說明] Load 接收交握[傳送要板訊號/等待接收Tray盤訊號]
                    Select Case RunMode
                        Case enmConveyorRunMode.Manual '手動放片
                            gStationID(STATION1).Working = True
                            gDOCollection.SetState(enmDO.MachineReadyToRecieve, False)
                            boolTrayRecive = False
                            .SysNum = 4000

                        Case enmConveyorRunMode.Auto '自動SMT
                            '傳送要板訊號
                            gDOCollection.SetState(enmDO.MachineReadyToRecieve, True)

                            '等待接收Tray盤訊號
                            Dim bRecive As Boolean = gDICollection.GetState(enmDI.BoardAvailable, True)

                            If boolTrayRecive = True Or bRecive = True Then
                                gStationID(STATION1).Working = True
                                gDOCollection.SetState(enmDO.MachineReadyToRecieve, False)
                                boolTrayRecive = False
                                .SysNum = 4000
                            End If
                    End Select



                Case 4000 '[說明] 轉輪轉動
                    If Rolling(STATION1) = True Then
                        Call gStation1StopWatch.Restart()
                        mCheckStation1Start = gStation1StopWatch.ElapsedMilliseconds
                        .SysNum = 4200
                    End If


                Case 4200 '[說明] Sensor偵測到Tray
                    Dim b1 As Boolean = gDICollection.GetState(enmDI.Station1TrayReady, True)

                    If b1 = True Then
                        Call gStation1StopWatch.Restart()
                        mCheckStation1Start = gStation1StopWatch.ElapsedMilliseconds
                        .SysNum = 4400
                    End If

                    If IsTimeOut(gStation1StopWatch, mCheckStation1Start, mDetectSensorSec) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2037005", "ConveyorStation1", .SysNum, gMsgHandler.GetMessage(Alarm_2037005), eMessageLevel.Alarm)
                        Return enmRunStatus.Alarm
                        .SysNum = 4000
                        StationRoll(STATION1) = RollPriority.None
                    End If


                Case 4400 '[說明] 轉動1秒停止，接收到Tray盤
                    If IsTimeOut(gStation1StopWatch, mCheckStation1Start, mRollingSec) = True Then
                        .SysNum = 4500
                    End If

                Case 4500  '[說明]轉輪停止運轉,接收到Tray盤
                    If StopRoll(STATION1) = True Then   'Station 1 轉輪停止運轉
                        gStationID(STATION1).ReceiveTrayFinish = True    'Station 1 接收到Tray盤旗標
                        .SysNum = 4600
                    End If

                Case 4600  '[說明] Heater啟用
                    If gStationID(STATION1).ChuckPar.DisHeater = True Then '[說明]停用加熱器, 直接跳過
                        .SysNum = 5000
                        Exit Select
                    Else
                        .SysNum = 4700
                    End If



                Case 4700   '[說明] 加熱器達到溫度?  Yes(5100) No(Idle)


                    '目標溫度
                    Dim dbTargetTemp As Double = gStationID(STATION1).ChuckPar.dbTargetTemperature
                    Dim index As Integer = enmAxis.Heater1
                    Dim sNowTemp As String = gCMotion.ReadHeaterPV(index)
                    Dim dbNowTemp As Double = CDbl(sNowTemp)

                    If Math.Abs(dbNowTemp - dbTargetTemp) < dbTargetTemp * 0.05 Then  '允許目標溫度5%誤差 Soni / 2015.08.21 客服同仁建議,溫度應卡上下限
                        'If dbNowTemp >= CStr(dbTargetTemp - 5) Then  '允許5度誤差
                        .SysNum = 5000
                    Else
                        Call gStation1StopWatch.Restart()
                        mCheckStation1Start = gStation1StopWatch.ElapsedMilliseconds
                        .SysNum = 4800
                    End If


                Case 4800 '[說明]通訊間隔2秒後再發一次
                    If IsTimeOut(gStation1StopWatch, mCheckStation1Start, mCommunicationGapSec) = True Then
                        .SysNum = 4600
                    End If


                Case 5000 '[說明] 停用Station1 Chuck站  Yes(7000) No(5100)
                    If gStationID(STATION1).ChuckPar.Disable = True Then
                        .SysNum = 7000       '[說明]停用Station1 Chuck站
                    Else
                        .SysNum = 5100       '[說明]不停用Station1 Chuck站
                    End If


                Case 5100 '[說明] Chuck 開真空

                    '開真空 
                    gDOCollection.SetState(enmDO.Station1ChuckVacuum, True)
                    '真空偵測
                    Dim b1 As Boolean = gDICollection.GetState(enmDI.Station1ChuckVacuumReady, True)

                    If b1 = True Then
                        Call gStation1StopWatch.Restart()
                        mCheckStation1Start = gStation1StopWatch.ElapsedMilliseconds
                        .SysNum = 5200
                    End If

                    If IsTimeOut(gStation1StopWatch, mCheckStation1Start, mDetectSensorSec) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2037006", "ConveyorStation1", .SysNum, gMsgHandler.GetMessage(Alarm_2037006), eMessageLevel.Alarm)
                        Return enmRunStatus.Alarm
                    End If

                Case 5200 '[說明] Chuck氣缸上昇
                    '站(1)頂升氣缸上昇
                    gDOCollection.SetState(enmDO.Station1TopLiftUpDown, True)
                    '站(1)頂升氣缸上偵測
                    Dim b1 As Boolean = gDICollection.GetState(enmDI.Station1TopLiftUpReady, True)

                    If b1 = True Then
                        .SysNum = 5400
                    End If

                    If IsTimeOut(gStation1StopWatch, mCheckStation1Start, mDetectSensorSec) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2037003", "ConveyorStation1", .SysNum, gMsgHandler.GetMessage(Alarm_2037003), eMessageLevel.Alarm)
                        Return enmRunStatus.Alarm
                    End If

                Case 5400 '[說明] 判斷 Station2 旗標 = false，否則等待工作完畢

                    If gStationID(STATION2).Working = False Then
                        .SysNum = 5600
                    End If


                Case 5600 '[說明] Chuck 關真空

                    '關真空
                    gDOCollection.SetState(enmDO.Station1ChuckVacuum, False)

                    Call gStation1StopWatch.Restart()
                    mCheckStation1Start = gStation1StopWatch.ElapsedMilliseconds
                    .SysNum = 5800

                Case 5800 '[說明] Chuck氣缸下降

                    '站(1)頂升氣缸下降
                    gDOCollection.SetState(enmDO.Station1TopLiftUpDown, False)
                    '站(1)頂升氣缸下偵測
                    Dim b1 As Boolean = gDICollection.GetState(enmDI.Station1TopLiftDownReady, True)

                    If b1 = True Then
                        .SysNum = 7000
                    End If

                    If IsTimeOut(gStation1StopWatch, mCheckStation1Start, mDetectSensorSec) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2037002", "ConveyorStation1", .SysNum, gMsgHandler.GetMessage(Alarm_2037002), eMessageLevel.Alarm)
                        Return enmRunStatus.Alarm
                    End If

                Case 7000 '[說明] Chuck Station2旗標 =false 往下執行，否則等待工作完畢

                    If gStationID(STATION2).Working = False Then
                        Call gStation1StopWatch.Restart()
                        mCheckStation1Start = gStation1StopWatch.ElapsedMilliseconds
                        .SysNum = 9000
                    End If

                Case 9000
                    Call gStation1StopWatch.Restart()
                    mCheckStation1Start = gStation1StopWatch.ElapsedMilliseconds
                    .SysNum = 9200

                Case 9200 '[說明] 閘門放開，呼叫 Station2 接收Tray盤

                    '站(1) 阻擋氣缸下降(閘門開)
                    gDOCollection.SetState(enmDO.Station1StopperUpDown, False)
                    '站(1) 阻擋氣缸上偵測
                    Dim b1 As Boolean = gDICollection.GetState(enmDI.Station1StopperDownReady, True)

                    If b1 = True Then
                        gStationID(STATION2).ReceiveTray = True  '呼叫 Station2 接收Tray盤
                        .SysNum = 9400
                    End If

                    If IsTimeOut(gStation1StopWatch, mCheckStation1Start, mDetectSensorSec) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2037000", "ConveyorStation1", .SysNum, gMsgHandler.GetMessage(Alarm_2037000), eMessageLevel.Alarm)
                        Return enmRunStatus.Alarm
                    End If

                Case 9400 '[說明] 等待Station2接收到Tray盤後
                    If gStationID(STATION2).ReceiveTrayFinish = True Then
                        gStationID(STATION2).ReceiveTrayFinish = False
                        .SysNum = 9600
                    End If

                Case 9600 '[說明]回到原點
                    gStationID(STATION1).Working = False
                    .SysNum = 1000
            End Select

            Return enmRunStatus.Running
        End With
    End Function

    ''' <summary>
    ''' Conveyor Station2 動作流程
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConveyorStation2(ByRef sys As sSysParam) As Boolean
        With sys

            Select Case .SysNum
                Case 1  '[說明] 初始化
                    Call gStation2StopWatch.Restart()
                    mCheckStation2Start = gStation2StopWatch.ElapsedMilliseconds
                    .SysNum = 1000

                Case 1000 '閘門關閉

                    '站(2) 阻擋氣缸上升(閘門關)
                    gDOCollection.SetState(enmDO.Station2StopperUp, True)
                    '站(2) 阻擋氣缸上偵測
                    Dim b2 As Boolean = gDICollection.GetState(enmDI.Station2StopperUpReady, True)

                    If b2 = True Then
                        .SysNum = 1100
                    End If

                    If IsTimeOut(gStation2StopWatch, mCheckStation2Start, mDetectSensorSec) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2037101", "ConveyorStation2", .SysNum, gMsgHandler.GetMessage(Alarm_2037101), eMessageLevel.Alarm)
                        Return enmRunStatus.Alarm
                    End If

                Case 1100 '[說明] 加熱器開始加熱

                    If gStationID(STATION2).ChuckPar.DisHeater = True Then    '[說明]停用Station2 Chuck站
                        gDOCollection.SetState(enmDO.HeaterPower, False)
                        gDOCollection.SetState(enmDO.Station2Heating, False)
                        .SysNum = 2000
                    Else                                                      '[說明]啟用Station2 Chuck站
                        gDOCollection.SetState(enmDO.HeaterPower, True)
                        gDOCollection.SetState(enmDO.Station2Heating, True)
                        '設定加熱器溫度
                        .SysNum = 1200
                    End If

                Case 1200  '[說明]設定加熱器溫度

                    Dim intTotalTemp As Integer = gStationID(STATION2).ChuckPar.dbTargetTemperature
                    Dim index As Integer = enmAxis.Heater2
                    Dim sv As CommandStatus = gCMotion.SetHeaterSV(index, intTotalTemp)

                    If sv = CommandStatus.Sending Then
                        .SysNum = 2000    '設定完成

                    Else                  '設定未完成
                        Call gStation2StopWatch.Restart()
                        mCheckStation2Start = gStation2StopWatch.ElapsedMilliseconds
                        .SysNum = 1300
                    End If

                Case 1300 '[說明]通訊間隔2秒後再發一次
                    If IsTimeOut(gStation2StopWatch, mCheckStation2Start, mCommunicationGapSec) = True Then
                        .SysNum = 1200
                    End If

                Case 2000 '[說明] 等待接收Tray盤
                    If gStationID(STATION2).ReceiveTray = True Then
                        gStationID(STATION2).ReceiveTray = False
                        gStationID(STATION2).Working = True
                        .SysNum = 3000
                    End If

                Case 3000
                    .SysNum = 3200

                Case 3200 '[說明] 轉輪轉動
                    If Rolling(STATION2) = True Then
                        .SysNum = 3400
                    End If

                Case 3400 '[說明] Sensor偵測到Tray
                    Dim b2 As Boolean = gDICollection.GetState(enmDI.Station2TrayReady, True)
                    If b2 = True Then
                        gStationID(STATION2).ReceiveTrayFinish = True                   '接收Tray盤完成旗標
                        Call gStation2StopWatch.Restart()
                        mCheckStation2Start = gStation2StopWatch.ElapsedMilliseconds
                        .SysNum = 3600
                    End If

                Case 3600  '[說明] 轉動1秒停止，Station 2 接收完成
                    If IsTimeOut(gStation2StopWatch, mCheckStation2Start, mRollingSec) = True Then
                        .SysNum = 3700
                    End If

                Case 3700  '[說明] Station 2 轉輪停止運轉
                    If StopRoll(STATION2) = True Then 'Station 2 轉輪停止運轉
                        .SysNum = 3800
                    End If


                Case 3800 '[說明] 加熱器達到溫度?   Yes(4000) No(3900)

                    If gStationID(STATION2).ChuckPar.DisHeater = True Then    '[說明]停用Station2 加熱器
                        .SysNum = 4000
                        Exit Select
                    End If


                    '目標溫度
                    Dim dbTargetTemp As Double = gStationID(STATION2).ChuckPar.dbTargetTemperature
                    Dim index As Integer = enmAxis.Heater2
                    Dim sNowTemp As String = gCMotion.ReadHeaterPV(index)
                    Dim dbNowTemp As Double = CDbl(sNowTemp)

                    If Math.Abs(dbNowTemp - dbTargetTemp) < dbTargetTemp * 0.05 Then  '允許目標溫度5%誤差 Soni / 2015.08.21 客服同仁建議,溫度應卡上下限
                        'If dbNowTemp >= CStr(dbTargetTemp - 5) Then  '允許5度誤差
                        .SysNum = 4000
                    Else
                        Call gStation2StopWatch.Restart()
                        mCheckStation2Start = gStation2StopWatch.ElapsedMilliseconds
                        .SysNum = 3900
                    End If

                Case 3900 '[說明]通訊間隔2秒後再發一次
                    If IsTimeOut(gStation2StopWatch, mCheckStation2Start, mCommunicationGapSec) = True Then
                        .SysNum = 3800
                    End If

                Case 4000
                    If gStationID(STATION2).ChuckPar.Disable = True Then    '[說明]停用Station2 Chuck站
                        .SysNum = 4800    '停用
                    Else
                        .SysNum = 4200    '啟用
                    End If

                Case 4200 '[說明] Chuck 開真空，Chuck馬達上昇至上點位

                    '開真空
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum, True)

                    Dim dblPos As Double = gStationID(STATION2).ChuckPar.dbMotorUpPos
                    If gCMotion.AbsMove(enmAxis.Station2, dblPos) = CommandStatus.Sucessed Then
                        'If gCMotion.AbsMove(CMotorID, dblPos) = CommandStatus.Sucessed Then
                        Call gStation2StopWatch.Restart()
                        mCheckStation2Start = gStation2StopWatch.ElapsedMilliseconds
                        .SysNum = 4300
                    End If

                    'Mobus 要下兩次命令才會正確
                Case 4300 '[說明]通訊間隔0.2秒
                    If IsTimeOut(gStation2StopWatch, mCheckStation2Start, 0.2) = True Then
                        .SysNum = 4400
                    End If

                Case 4400 '[說明] 詢問第一次

                    If gCMotion.GetCmdStatus(enmAxis.Station2) = CommandStatus.Sucessed And gCMotion.MotionDone(enmAxis.Station2) = CommandStatus.Sucessed Then
                        'If gCMotion.GetCmdStatus(CMotorID) = CommandStatus.Sucessed And gCMotion.MoveFinish(CMotorID) = CommandStatus.Sucessed Then
                        Call gStation2StopWatch.Restart()
                        mCheckStation2Start = gStation2StopWatch.ElapsedMilliseconds
                        .SysNum = 4500
                    End If

                Case 4500 '[說明]通訊間隔0.2秒
                    If IsTimeOut(gStation2StopWatch, mCheckStation2Start, 0.2) = True Then
                        .SysNum = 4600
                    End If

                Case 4600  '[說明] 詢問第二次

                    If gCMotion.GetCmdStatus(enmAxis.Station2) = CommandStatus.Sucessed And gCMotion.MotionDone(enmAxis.Station2) = CommandStatus.Sucessed Then
                        'If gCMotion.GetCmdStatus(CMotorID) = CommandStatus.Sucessed And gCMotion.MoveFinish(CMotorID) = CommandStatus.Sucessed Then
                        .SysNum = 4800
                    End If


                Case 4800 '[說明] 側推氣缸合起

                    If gStationID(STATION2).ChuckPar.DisTrayClamp = True Then    '[說明]停用Station2 側推氣缸
                        .SysNum = 5000
                        Exit Select
                    End If


                    gDOCollection.SetState(enmDO.TrayClamper, True)
                    Dim b2 As Boolean = gDICollection.GetState(enmDI.TrayClamperOnReady, True)

                    If b2 = True Then
                        .SysNum = 5000
                    End If

                    If IsTimeOut(gStation2StopWatch, mCheckStation2Start, mDetectSensorSec) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2037108", "ConveyorStation2", .SysNum, gMsgHandler.GetMessage(Alarm_2037108), eMessageLevel.Alarm)
                        Return enmRunStatus.Alarm
                    End If

                Case 5000 '[說明] 通知點膠

                    bBckDispensrFinish = False     '通知完成 False
                    bAckDispensrTrayReady = True   '通知點膠 True
                    .SysNum = 5200


                Case 5200 '[說明] 點膠完成? Yes(5400)  No(Idle)

                    Select Case RunMode
                        Case enmConveyorRunMode.Manual '手動放片
                            'If bBckDispensrFinish = True Then
                            '    bAckDispensrTrayReady = False   '通知點膠 False
                            '    .SysNum = 5400
                            'End If

                            bAckDispensrTrayReady = False   '通知點膠 False
                            .SysNum = 5400

                        Case enmConveyorRunMode.Auto '自動SMT
                            If bBckDispensrFinish = True Then
                                bAckDispensrTrayReady = False   '通知點膠 False
                                .SysNum = 5400
                            End If
                    End Select


                Case 5400 '[說明] Chuck 關真空，側推氣缸打開

                    gDOCollection.SetState(enmDO.Station2ChuckVacuum, False)

                    If gStationID(STATION2).ChuckPar.DisTrayClamp = True Then    '[說明]停用Station2 側推氣缸
                        .SysNum = 5600
                        Exit Select
                    End If



                    Dim b2 As Boolean = gDICollection.GetState(enmDI.TrayClamperOffReady, True)

                    If b2 = True Then
                        .SysNum = 5600
                    End If

                    If IsTimeOut(gStation2StopWatch, mCheckStation2Start, mDetectSensorSec) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2037109", "ConveyorStation2", .SysNum, gMsgHandler.GetMessage(Alarm_2037109), eMessageLevel.Alarm)
                        Return enmRunStatus.Alarm
                    End If

                Case 5600 '[說明]判斷Station3工作是否完成
                    If gStationID(STATION3).Working = False Then
                        .SysNum = 5700
                    End If

                Case 5700
                    If gStationID(STATION2).ChuckPar.Disable = True Then    '[說明]停用Station2 Chuck站
                        .SysNum = 9000      '停用
                    Else
                        .SysNum = 5800      '啟用
                    End If

                Case 5800 '[說明] Chuck馬達下降至下點位

                    Dim dblPos As Double = gStationID(STATION2).ChuckPar.dbMotorDownPos

                    If gCMotion.AbsMove(enmAxis.Station2, dblPos) = CommandStatus.Sucessed Then
                        'If gCMotion.AbsMove(CMotorID, dblPos) = CommandStatus.Sucessed Then
                        Call gStation2StopWatch.Restart()
                        mCheckStation2Start = gStation2StopWatch.ElapsedMilliseconds
                        .SysNum = 5900
                    End If

                    'Mobus 要下兩次命令才會正確
                Case 5900 '[說明] 發第一次

                    If gCMotion.GetCmdStatus(enmAxis.Station2) = CommandStatus.Sucessed And gCMotion.MotionDone(enmAxis.Station2) = CommandStatus.Sucessed Then
                        'If gCMotion.GetCmdStatus(CMotorID) = CommandStatus.Sucessed And gCMotion.MoveFinish(CMotorID) = CommandStatus.Sucessed Then
                        Call gStation2StopWatch.Restart()
                        mCheckStation2Start = gStation2StopWatch.ElapsedMilliseconds
                        .SysNum = 6000
                    End If

                Case 6000
                    If IsTimeOut(gStation2StopWatch, mCheckStation2Start, 0.2) = True Then
                        .SysNum = 6100
                    End If


                Case 6100 '[說明] 發第二次
                    If gCMotion.GetCmdStatus(enmAxis.Station2) = CommandStatus.Sucessed And gCMotion.MotionDone(enmAxis.Station2) = CommandStatus.Sucessed Then
                        'If gCMotion.GetCmdStatus(CMotorID) = CommandStatus.Sucessed And gCMotion.MoveFinish(CMotorID) = CommandStatus.Sucessed Then
                        Call gStation2StopWatch.Restart()
                        mCheckStation2Start = gStation2StopWatch.ElapsedMilliseconds
                        .SysNum = 6200
                    End If

                Case 6200
                    If IsTimeOut(gStation2StopWatch, mCheckStation2Start, 0.2) = True Then
                        .SysNum = 6300
                    End If

                Case 6300
                    .SysNum = 9000



                Case 9000 '[說明] 
                    Call gStation2StopWatch.Restart()
                    mCheckStation2Start = gStation2StopWatch.ElapsedMilliseconds
                    .SysNum = 9200

                Case 9200 '[說明]閘門放開，Station2旗標=false，Call Station3 接收Tray盤

                    '站(2) 阻擋氣缸下降(閘門開)
                    gDOCollection.SetState(enmDO.Station2StopperUp, False)
                    '站(2) 阻擋氣缸上偵測
                    Dim b2 As Boolean = gDICollection.GetState(enmDI.Station2StopperDownReady, True)

                    If b2 = True Then
                        gStationID(STATION3).ReceiveTray = True
                        .SysNum = 9400
                    End If

                    If IsTimeOut(gStation2StopWatch, mCheckStation2Start, mDetectSensorSec) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2037100", "ConveyorStation2", .SysNum, gMsgHandler.GetMessage(Alarm_2037100), eMessageLevel.Alarm)
                        Return enmRunStatus.Alarm
                    End If

                Case 9400 '[說明] 等待Station3接收到Tray盤後
                    If gStationID(STATION3).ReceiveTrayFinish = True Then
                        gStationID(STATION3).ReceiveTrayFinish = False
                        .SysNum = 9600
                    End If

                Case 9600 '[說明] 回到原點
                    gStationID(STATION2).Working = False
                    .SysNum = 1000

            End Select

            Return enmRunStatus.Running

        End With
    End Function

    ''' <summary>
    ''' Conveyor Station3 動作流程
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConveyorStation3(ByRef sys As sSysParam) As enmRunStatus
        With sys

            Select Case .SysNum
                Case 1   '[說明] 初始化
                    Call gStation3StopWatch.Restart()
                    mCheckStation3Start = gStation3StopWatch.ElapsedMilliseconds
                    .SysNum = 1000

                Case 1000 '阻擋氣缸(閘門關閉)

                    '站(3) 阻擋氣缸(閘門關)
                    gDOCollection.SetState(enmDO.Station3StopperUp, False)
                    '站(3) 阻擋氣缸上偵測
                    Dim b3 As Boolean = gDICollection.GetState(enmDI.Station3StopperDownReady, True)

                    If b3 = True Then
                        .SysNum = 1200
                    End If

                    If IsTimeOut(gStation3StopWatch, mCheckStation3Start, mDetectSensorSec) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2037201", "ConveyorStation3", .SysNum, gMsgHandler.GetMessage(Alarm_2037201), eMessageLevel.Alarm)
                        Return enmRunStatus.Alarm
                    End If

                Case 1200 '[說明] 加熱器開始加熱

                    If gStationID(STATION3).ChuckPar.DisHeater = True Then    '[說明]停用Station3 加熱器
                        gDOCollection.SetState(enmDO.HeaterPower, False)
                        gDOCollection.SetState(enmDO.Station3Heating, False)
                        .SysNum = 2000
                    Else                                                      '[說明]啟用Station3 加熱器
                        gDOCollection.SetState(enmDO.HeaterPower, True)
                        gDOCollection.SetState(enmDO.Station3Heating, True)
                        '設定加熱器溫度
                        .SysNum = 1300
                    End If

                Case 1300  '[說明]設定加熱器溫度

                    Dim intTotalTemp As Integer = gStationID(STATION3).ChuckPar.dbTargetTemperature
                    Dim index As Integer = enmAxis.Heater3
                    Dim sv As CommandStatus = gCMotion.SetHeaterSV(index, intTotalTemp)

                    If sv = CommandStatus.Sending Then
                        .SysNum = 2000    '設定完成

                    Else                  '設定未完成
                        Call gStation3StopWatch.Restart()
                        mCheckStation3Start = gStation3StopWatch.ElapsedMilliseconds
                        .SysNum = 1400
                    End If

                Case 1400 '[說明]通訊間隔2秒後再發一次
                    If IsTimeOut(gStation3StopWatch, mCheckStation3Start, mCommunicationGapSec) = True Then
                        .SysNum = 1300
                    End If


                Case 2000 '[說明] 等待接收Tray盤
                    If gStationID(STATION3).ReceiveTray = True Then
                        gStationID(STATION3).ReceiveTray = False
                        gStationID(STATION3).Working = True
                        .SysNum = 3000
                    End If

                Case 3000 '[說明] 轉輪轉動
                    If Rolling(STATION3) = True Then
                        .SysNum = 3200
                    End If

                Case 3200 '[說明] Sensor偵測到Tray

                    Dim b3 As Boolean = gDICollection.GetState(enmDI.Station3TrayReady, True)

                    If b3 = True Then
                        gStationID(STATION3).ReceiveTrayFinish = True
                        Call gStation3StopWatch.Restart()
                        mCheckStation3Start = gStation3StopWatch.ElapsedMilliseconds
                        .SysNum = 3400
                    End If

                Case 3400 '[說明] 轉動1秒停止，
                    If IsTimeOut(gStation3StopWatch, mCheckStation3Start, mRollingSec) = True Then
                        .SysNum = 3500
                    End If

                Case 3500
                    If StopRoll(STATION3) = True Then 'Station 3 轉輪停止運轉
                        .SysNum = 3600
                    End If

                Case 3600 '[說明]停用Station3

                    If gStationID(STATION3).ChuckPar.Disable = True Then    '[說明]停用Station3 Chuck
                        .SysNum = 3700      '停用
                    Else
                        .SysNum = 3800      '啟用
                    End If

                Case 3700 '[說明] Unload可接收Tray盤? Yes(9000) ,No(Idle)
                    Select Case RunMode
                        Case enmConveyorRunMode.Manual
                            boolUnloadTray = False
                            gDOCollection.SetState(enmDO.BoardAvailable, True) '發出 Unload 機接收Tray盤(開)
                            .SysNum = 9000
                        Case enmConveyorRunMode.Auto
                            '詢問 Unload 機Tray盤接收動作中 ?
                            Dim b1 As Boolean = gDICollection.GetState(enmDI.MachineReadyToRecieve, True)

                            If boolUnloadTray = True Or b1 = True Then
                                boolUnloadTray = False
                                gDOCollection.SetState(enmDO.BoardAvailable, True) '發出 Unload 機接收Tray盤(開)
                                .SysNum = 9000
                            End If

                    End Select


                Case 3800 '[說明] 讀取加熱器達到溫度?   Yes(5000)，No(3900)
                    If gStationID(STATION3).ChuckPar.DisHeater = True Then    '[說明]停用Station3 加熱器
                        .SysNum = 5000
                        Exit Select
                    End If


                    '目標溫度
                    Dim dbTargetTemp As Double = gStationID(STATION3).ChuckPar.dbTargetTemperature
                    Dim index As Integer = enmAxis.Heater3
                    Dim sNowTemp As String = gCMotion.ReadHeaterPV(index)
                    Dim dbNowTemp As Double = CDbl(sNowTemp)

                    If Math.Abs(dbNowTemp - dbTargetTemp) < dbTargetTemp * 0.05 Then  '允許目標溫度5%誤差 Soni / 2015.08.21 客服同仁建議,溫度應卡上下限
                        'If dbNowTemp >= CStr(dbTargetTemp - 5) Then  '允許5度誤差
                        .SysNum = 5000
                    Else
                        Call gStation3StopWatch.Restart()
                        mCheckStation3Start = gStation3StopWatch.ElapsedMilliseconds
                        .SysNum = 3900
                    End If


                Case 3900 '[說明]通訊間隔2秒後再發一次(讀值)
                    If IsTimeOut(gStation3StopWatch, mCheckStation3Start, mCommunicationGapSec) = True Then
                        .SysNum = 3800
                    End If



                Case 5000 '[說明] Chuck 開真空，Chuck氣缸上昇

                    '站(3)Chuck 開真空
                    gDOCollection.SetState(enmDO.Station3ChuckVacuum, True)
                    '站(3)頂升氣缸上昇
                    gDOCollection.SetState(enmDO.Station3TopLiftUpDown, True)
                    '站(3)Chuck 真空偵測
                    Dim b1 As Boolean = gDICollection.GetState(enmDI.Station3ChuckVacuumReady, True)
                    '站(3)頂升氣缸上偵測
                    Dim b2 As Boolean = gDICollection.GetState(enmDI.Station3TopLiftUpReady, True)

                    If b1 And b2 Then
                        .SysNum = 5200
                    End If

                    If IsTimeOut(gStation3StopWatch, mCheckStation3Start, mDetectSensorSec) = True Then
                        If b1 = False Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2037206", "ConveyorStation3", .SysNum, gMsgHandler.GetMessage(Alarm_2037206), eMessageLevel.Alarm)
                            Return enmRunStatus.Alarm
                        End If

                        If b2 = False Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2037203", "ConveyorStation3", .SysNum, gMsgHandler.GetMessage(Alarm_2037203), eMessageLevel.Alarm)
                            Return enmRunStatus.Alarm
                        End If
                    End If

                Case 5200 '[說明] Unload可接收Tray盤?  Yes(5400)，No(5200)

                    '詢問 Unload 機Tray盤接收動作中 ?
                    Dim b1 As Boolean = gDICollection.GetState(enmDI.MachineReadyToRecieve, True)

                    If b1 = True Then
                        .SysNum = 5400
                    End If


                Case 5400 '[說明] Chuck 關真空

                    gDOCollection.SetState(enmDO.Station3ChuckVacuum, False)
                    Call gStation3StopWatch.Restart()
                    mCheckStation3Start = gStation3StopWatch.ElapsedMilliseconds
                    .SysNum = 5600

                Case 5600 '[說明] Chuck氣缸下降

                    '站(3)頂升氣缸下降
                    gDOCollection.SetState(enmDO.Station3TopLiftUpDown, False)

                    '站(3)頂升氣缸下偵測
                    Dim b3 As Boolean = gDICollection.GetState(enmDI.Station3TopLiftDownReady, True)

                    If b3 = True Then
                        .SysNum = 9000
                    End If

                    If IsTimeOut(gStation3StopWatch, mCheckStation3Start, mDetectSensorSec) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2037202", "ConveyorStation3", .SysNum, gMsgHandler.GetMessage(Alarm_2037202), eMessageLevel.Alarm)
                        Return enmRunStatus.Alarm
                    End If


                Case 9000
                    Call gStation3StopWatch.Restart()
                    mCheckStation3Start = gStation3StopWatch.ElapsedMilliseconds
                    gDOCollection.SetState(enmDO.BoardAvailable, True) '發出 Unload 機接收Tray盤(開)
                    .SysNum = 9200



                Case 9200 '[說明] 閘門放開

                    '站(3) 阻擋氣缸上升(閘門開)
                    gDOCollection.SetState(enmDO.Station3StopperUp, True)

                    Dim b3 As Boolean = gDICollection.GetState(enmDI.Station3StopperUpReady, True)

                    If b3 = True Then
                        .SysNum = 9400
                    End If

                    If IsTimeOut(gStation3StopWatch, mCheckStation3Start, mDetectSensorSec) = True Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2037200", "ConveyorStation3", .SysNum, gMsgHandler.GetMessage(Alarm_2037200), eMessageLevel.Alarm)
                        Return enmRunStatus.Alarm
                    End If

                Case 9400 '[說明] 轉輪轉動
                    If Rolling(STATION3) = True Then
                        .SysNum = 9600
                    End If


                Case 9600 '[說明] 詢問UnLoad收到Tray盤? Yes(9600) No()
                    Select Case RunMode
                        Case enmConveyorRunMode.Manual
                            If gDICollection.GetState(enmDI.Station3TrayReady, False) = False Then
                                boolUnloadTray = False
                                gDOCollection.SetState(enmDO.BoardAvailable, False) '發出 Unload 機接收Tray盤(關)
                                gStation3StopWatch.Restart()

                                .SysNum = 9620
                            End If

                        Case enmConveyorRunMode.Auto

                            '詢問 Unload 機Tray盤接收動作中 ?
                            Dim b1 As Boolean = gDICollection.GetState(enmDI.MachineReadyToRecieve, True)

                            If boolUnloadTray = True Or b1 = True Then
                                boolUnloadTray = False
                                gDOCollection.SetState(enmDO.BoardAvailable, False) '發出 Unload 機接收Tray盤(關)

                                .SysNum = 9700
                            End If
                    End Select

                Case 9620
                    If gStation3StopWatch.ElapsedMilliseconds > 1000 Then '離開Tray3 Ready後多轉1秒
                        .SysNum = 9700
                    End If

                Case 9700 '[說明] 停止轉動
                    If StopRoll(STATION3) = True Then
                        .SysNum = 9800
                    End If

                Case 9800 '[說明] Station3旗標=false，回到原點
                    gStationID(STATION3).Working = False
                    .SysNum = 1000
            End Select

            Return enmRunStatus.Running

        End With
    End Function
#End Region

End Class




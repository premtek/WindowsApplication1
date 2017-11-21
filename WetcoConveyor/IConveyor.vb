Imports ProjectIO
Imports ProjectCore
Imports ProjectMotion

Public Enum enmMotionStatus
    Null
    [Stop]
    Finish
    Running
    Pause
    Alarm
    Warning
End Enum

Public Interface IConveyorMotion
    Enum enmConveyor As Integer
        No1
        No2
        No3
        No4
    End Enum

    ''' <summary>
    ''' 取得值，指出是否正在執行流道作業。
    ''' </summary>
    ReadOnly Property IsBusy As Boolean

    ''' <summary>
    ''' 取得值，指出流道目前狀態。
    ''' </summary>
    ReadOnly Property Starus As enmMotionStatus

    ''' <summary>
    ''' 取得或設定退料後是否接續進料。
    ''' </summary>
    ''' <value>trre : 完成退料後自動進料</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContinueLoad As Boolean

    ''' <summary>
    ''' 取得值，流程當前步驟。
    ''' </summary>
    ReadOnly Property MotionStep As Integer

    ''' <summary>
    ''' 復歸流程。
    ''' </summary>
    Function Initial() As enmMotionStatus

    ''' <summary>
    ''' 進料流程。
    ''' </summary>
    Function Load() As enmMotionStatus

    ''' <summary>
    ''' 退料流程。
    ''' </summary>
    Function Unload() As enmMotionStatus

    ''' <summary>
    ''' 將運動狀態重設為起始狀態。
    ''' </summary>
    Sub Reset()

End Interface

Public Interface IRoller
    Enum enmStatus
        [Stop]
        Load
        Unload
        Alarm
    End Enum

    Enum enmSpeed
        Fast
        Normal
        Slow
    End Enum

    Enum enmDirection
        Forward
        Reversal
    End Enum

    ''' <summary>
    ''' 當前狀態
    ''' </summary>
    ReadOnly Property Status As enmStatus

    ''' <summary>
    ''' Load 運轉方向
    ''' </summary>
    ReadOnly Property LoadDirection As enmDirection

    ''' <summary>
    ''' Unload 運轉方向
    ''' </summary>
    ReadOnly Property UnloadDirection As enmDirection

    ''' <summary>
    ''' 設定進出料方向
    ''' </summary>
    ''' <param name="load">進貨方向</param>
    ''' <param name="unload">卸貨方向</param>
    ''' <remarks></remarks>
    Sub SetDirection(ByVal load As enmDirection, ByVal unload As enmDirection)

    ''' <summary>
    ''' 速度設定
    ''' </summary>
    Function SetSpeed(ByVal low As Decimal, ByVal high As Decimal) As Boolean

    ''' <summary>
    ''' 復歸
    ''' </summary>
    Function Initial() As Boolean

    ''' <summary>
    ''' 執行運轉
    ''' </summary>
    Sub Run(ByVal direction As enmDirection)

    ''' <summary>
    ''' 執行Load方向運動
    ''' </summary>
    Sub Load()

    ''' <summary>
    ''' 執行Load方向運動
    ''' </summary>
    Sub Load(Optional speed As enmSpeed = enmSpeed.Normal)

    ''' <summary>
    ''' 執行Unlaod方向運動
    ''' </summary>
    Sub Unload()

    ''' <summary>
    ''' 執行Unlaod方向運動
    ''' </summary>
    Sub Unload(Optional speed As enmSpeed = enmSpeed.Normal)

    ''' <summary>
    '''停止運作 
    ''' </summary>
    Sub [Stop]()

    ReadOnly Property Alarm As Boolean

End Interface

Public Interface IConveyorUnit

    Enum enmDirection
        Null
        Home
        Up
        Down
    End Enum

    ''' <summary>
    '''入料Sensor 
    ''' </summary>
    ''' <value></value>
    ''' <returns>true:遮擋, false:無遮擋</returns>
    ReadOnly Property EntranceSensor As Boolean

    ''' <summary>
    '''出料Sensor 
    ''' </summary>
    ''' <value></value>
    ''' <returns>true:遮擋, false:無遮擋</returns>
    ReadOnly Property ExitSensor As Boolean

    ''' <summary>
    '''到位Sensor 
    ''' </summary>
    ''' <value></value>
    ''' <returns>true:已到位, false:未到位</returns>
    ReadOnly Property PositionSensor As Boolean

    ''' <summary>
    ''' 阻擋器
    ''' </summary>
    ''' <param name="direction">動作</param>
    ''' <returns>命令是否發送成功</returns>
    ''' <remarks></remarks>
    Function Stoper(ByVal direction As enmDirection) As Boolean
    
    ''' <summary>
    ''' Stoper當前位置
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ReadOnly Property StoperLocation As enmDirection

    ''' <summary>
    ''' 是否為真空狀態
    ''' </summary>
    ReadOnly Property IsVacuum As Boolean

    ''' <summary>
    ''' 真空控制
    ''' </summary>
    ''' <returns>命令是否發送成功</returns>
    ''' <remarks></remarks>
    Function VacuumControl(ByVal enable As Boolean) As Boolean

    ''' <summary>
    ''' 升降器當前位置
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ReadOnly Property LifterLocation As enmDirection

    ''' <summary>
    ''' 升降器
    ''' </summary>
    ''' <param name="direction">動作</param>
    ''' <returns>命令是否發送成功</returns>
    ''' <remarks></remarks>
    Function Lifter(ByVal direction As enmDirection) As Boolean

End Interface

Public Interface ISMEMA
    ''' <summary>
    ''' 指出Loader是否可供料
    ''' </summary>
    ReadOnly Property IsLoaderReady As Boolean

    ''' <summary>
    ''' 表示點膠機是否要對Loader要料
    ''' </summary>
    Property IsReadyToRecieve As Boolean

    ''' <summary>
    ''' 指出Unload是否可收料
    ''' </summary>
    ReadOnly Property IsUnloaderReady As Boolean

    ''' <summary>
    ''' 表示點膠機是否要對Unloader供料
    ''' </summary>
    Property IsReadyToSend As Boolean
End Interface

Public Interface IConveyor
    Property Motion As IConveyorMotion
    Property SMEMA As ISMEMA
    Property Unit As IConveyorUnit
End Interface



'未使用
''' <summary>
''' F350A 一號流道控制元件
''' </summary>
''' <remarks></remarks>
Public Class clsF350A_Cv1Unit : Implements IConveyorUnit

    ''' <summary>
    ''' 真空建立/真空破除 所需時間
    ''' </summary>
    Dim VacuumDelayTime As Integer = 300

    Public ReadOnly Property EntranceSensor As Boolean Implements IConveyorUnit.EntranceSensor
        Get
            If (gDICollection.GetState(enmDI.Station2TrayInSensor)) Then
                Return True
            End If
            Return False
        End Get
    End Property

    Public ReadOnly Property IsVacuum As Boolean Implements IConveyorUnit.IsVacuum
        Get
            If (gDICollection.GetState(enmDI.Station2ChuckVacuumReady)) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Function VacuumControl(enable As Boolean) As Boolean Implements IConveyorUnit.VacuumControl
        If enable Then
            '吹氣OFF->吸真空ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, False)  '吹氣OFF
            gDOCollection.SetState(enmDO.Station2ChuckVacuum, True)  '吸真空ON
            Threading.Thread.CurrentThread.Join(VacuumDelayTime)

            If (IsVacuum) Then '真空建立判斷
                Return True '真空
            Else
                Return False
            End If
        Else
            '吸真空OFF->吹氣ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuum, False)  '吸真空OFF
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, True)   '吹氣ON
            Threading.Thread.CurrentThread.Join(VacuumDelayTime)

            If (IsVacuum = False) Then '真空建立判斷
                gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, False)   '吹氣OFF
                Return True '破真空
            Else
                Return False '真空
            End If
        End If
        Return False
    End Function

    Public Function Lifter(direction As IConveyorUnit.enmDirection) As Boolean Implements IConveyorUnit.Lifter
        If (direction = IConveyorUnit.enmDirection.Up) Then
            gDOCollection.SetState(enmDO.HeaterCylinderDown1, False)
            gDOCollection.SetState(enmDO.HeaterCylinderUp1, True)
            If (gDICollection.GetState(enmDI.Station2Heater1CylinderUpReady, True)) Then '上升完成
                Return True
            End If
        ElseIf (direction = IConveyorUnit.enmDirection.Down) Then
            gDOCollection.SetState(enmDO.HeaterCylinderUp1, False)
            gDOCollection.SetState(enmDO.HeaterCylinderDown1, True)
            If (gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady, True)) Then '下降完成
                Return True
            End If
        End If
        Return False
    End Function

    Public ReadOnly Property LifterLocation As IConveyorUnit.enmDirection Implements IConveyorUnit.LifterLocation
        Get
            If (gDICollection.GetState(enmDI.Station2Heater1CylinderUpReady)) Then
                Return IConveyorUnit.enmDirection.Up
            ElseIf (gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady)) Then
                Return IConveyorUnit.enmDirection.Down
            Else
                Return IConveyorUnit.enmDirection.Null
            End If
        End Get
    End Property

    Public Function Stoper(direction As IConveyorUnit.enmDirection) As Boolean Implements IConveyorUnit.Stoper
        If (direction = IConveyorUnit.enmDirection.Up) Then
            gDOCollection.SetState(enmDO.Station2StopperDown, False)
            gDOCollection.SetState(enmDO.Station2StopperUp, True)
            If (gDICollection.GetState(enmDI.Station2StopperUpReady, True)) Then '上升完成
                Return True
            End If
        ElseIf (direction = IConveyorUnit.enmDirection.Down) Then
            gDOCollection.SetState(enmDO.Station2StopperUp, False)
            gDOCollection.SetState(enmDO.Station2StopperDown, True)
            If (gDICollection.GetState(enmDI.Station2StopperDownReady, True)) Then '下降完成
                Return True
            End If
        End If
        Return False
    End Function

    Public ReadOnly Property PositionSensor As Boolean Implements IConveyorUnit.PositionSensor
        Get
            If (gDICollection.GetState(enmDI.Station2TrayReady)) Then
                Return True
            End If
            Return False
        End Get
    End Property

    Public ReadOnly Property StoperLocation As IConveyorUnit.enmDirection Implements IConveyorUnit.StoperLocation
        Get
            If (gDICollection.GetState(enmDI.Station2StopperUpReady)) Then
                Return IConveyorUnit.enmDirection.Up
            ElseIf (gDICollection.GetState(enmDI.Station2StopperDownReady)) Then
                Return IConveyorUnit.enmDirection.Down
            Else
                Return IConveyorUnit.enmDirection.Null
            End If
        End Get
    End Property

#Region "硬體無此元件"
    Private ReadOnly Property ExitSensor As Boolean Implements IConveyorUnit.ExitSensor
        Get
            Return False
        End Get
    End Property

#End Region
End Class

'未使用
''' <summary>
''' F350A 二號流道控制元件
''' </summary>
''' <remarks></remarks>
Public Class clsF350A_Cv2Unit : Implements IConveyorUnit

    ''' <summary>
    ''' 真空建立/真空破除 所需時間
    ''' </summary>
    Dim VacuumDelayTime As Integer = 300

    Public ReadOnly Property EntranceSensor As Boolean Implements IConveyorUnit.EntranceSensor
        Get
            If gDICollection.GetState(enmDI.Station3TrayInSensor) Then
                Return True
            End If
            Return False
        End Get
    End Property

    Public ReadOnly Property IsVacuum As Boolean Implements IConveyorUnit.IsVacuum
        Get
            If (gDICollection.GetState(enmDI.Station3ChuckVacuumReady)) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Function VacuumControl(ByVal enable As Boolean) As Boolean Implements IConveyorUnit.VacuumControl
        If enable Then
            '吹氣OFF->吸真空ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak1, False)  '吹氣OFF
            gDOCollection.SetState(enmDO.Station3ChuckVacuum, True)  '吸真空ON
            Threading.Thread.CurrentThread.Join(VacuumDelayTime)

            If (IsVacuum) Then '真空建立判斷
                Return True '真空
            Else
                Return False
            End If
        Else
            '吸真空OFF->吹氣ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuum, False)  '吸真空OFF
            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak1, True)   '吹氣ON

            Threading.Thread.CurrentThread.Join(VacuumDelayTime)

            If (IsVacuum = False) Then '真空建立判斷
                gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak1, False)   '吹氣OFF
                Return True '破真空
            Else
                Return False '真空
            End If
        End If
        Return False
    End Function

    Public Function Lifter(direction As IConveyorUnit.enmDirection) As Boolean Implements IConveyorUnit.Lifter
        If (direction = IConveyorUnit.enmDirection.Up) Then
            gDOCollection.SetState(enmDO.HeaterCylinderDown7, False)
            gDOCollection.SetState(enmDO.HeaterCylinderUp7, True)
            If (gDICollection.GetState(enmDI.Station3Heater1CylinderUpReady, True)) Then  'Stoper上升完成
                Return True
            End If
        ElseIf (direction = IConveyorUnit.enmDirection.Down) Then
            gDOCollection.SetState(enmDO.HeaterCylinderUp7, False)
            gDOCollection.SetState(enmDO.HeaterCylinderDown7, True)
            If (gDICollection.GetState(enmDI.Station3Heater1CylinderDownReady, True)) Then 'Stoper下降完成
                Return True
            End If
        End If
        Return False
    End Function

    Public ReadOnly Property LifterLocation As IConveyorUnit.enmDirection Implements IConveyorUnit.LifterLocation
        Get
            If (gDICollection.GetState(enmDI.Station3Heater1CylinderUpReady)) Then
                Return IConveyorUnit.enmDirection.Up
            ElseIf (gDICollection.GetState(enmDI.Station3Heater1CylinderDownReady)) Then
                Return IConveyorUnit.enmDirection.Down
            Else
                Return IConveyorUnit.enmDirection.Null
            End If
        End Get
    End Property

    Public Function Stoper(direction As IConveyorUnit.enmDirection) As Boolean Implements IConveyorUnit.Stoper
        If (direction = IConveyorUnit.enmDirection.Up) Then
            If (gDICollection.GetState(enmDI.Station3StopperUpReady, True)) Then  'Stoper上升完成
                Return True
            End If
            gDOCollection.SetState(enmDO.Station3StopperDown, False)
            gDOCollection.SetState(enmDO.Station3StopperUp, True)
        ElseIf (direction = IConveyorUnit.enmDirection.Down) Then
            If (gDICollection.GetState(enmDI.Station3StopperDownReady, True)) Then 'Stoper下降完成
                Return True
            End If
            gDOCollection.SetState(enmDO.Station3StopperUp, False)
            gDOCollection.SetState(enmDO.Station3StopperDown, True)
        End If
        Return False
    End Function

    Public ReadOnly Property PositionSensor As Boolean Implements IConveyorUnit.PositionSensor
        Get
            If (gDICollection.GetState(enmDI.Station3TrayReady)) Then
                Return True
            End If
            Return False
        End Get
    End Property

    Public ReadOnly Property StoperLocation As IConveyorUnit.enmDirection Implements IConveyorUnit.StoperLocation
        Get
            If (gDICollection.GetState(enmDI.Station3StopperUpReady)) Then
                Return IConveyorUnit.enmDirection.Up
            ElseIf (gDICollection.GetState(enmDI.Station3StopperDownReady)) Then
                Return IConveyorUnit.enmDirection.Down
            Else
                Return IConveyorUnit.enmDirection.Null
            End If
        End Get
    End Property

#Region "硬體無此元件"
    Private ReadOnly Property ExitSensor As Boolean Implements IConveyorUnit.ExitSensor
        Get
            Return False
        End Get
    End Property

#End Region
End Class


''' <summary>
''' F350A 一號流道SMEMA
''' </summary>
''' <remarks></remarks>
Public Class clsCv1SMEMA : Implements ISMEMA
    Public Property IsReadyToRecieve As Boolean Implements ISMEMA.IsReadyToRecieve
        Get
            Return gDOCollection.GetState(enmDO.MachineReadyToRecieve)
        End Get
        Set(value As Boolean)
            gDOCollection.SetState(enmDO.MachineReadyToRecieve, value)
        End Set
    End Property

    Public Property IsReadyToSend As Boolean Implements ISMEMA.IsReadyToSend
        Get
            Return gDOCollection.GetState(enmDO.BoardAvailable)
        End Get
        Set(value As Boolean)
            gDOCollection.SetState(enmDO.BoardAvailable, value)
        End Set
    End Property

    Public ReadOnly Property IsLoaderReady As Boolean Implements ISMEMA.IsLoaderReady
        Get
            Return gDICollection.GetState(enmDI.BoardAvailable)
        End Get
    End Property

    Public ReadOnly Property IsUnloaderReady As Boolean Implements ISMEMA.IsUnloaderReady
        Get
            Return gDICollection.GetState(enmDI.MachineReadyToRecieve)
        End Get
    End Property
End Class

''' <summary>
''' F350A 二號流道SMEMA
''' </summary>
''' <remarks></remarks>
Public Class clsCv2SMEMA : Implements ISMEMA
    Public Property IsReadyToRecieve As Boolean Implements ISMEMA.IsReadyToRecieve
        Get
            Return gDOCollection.GetState(enmDO.MachineReadyToRecieve2)
        End Get
        Set(value As Boolean)
            gDOCollection.SetState(enmDO.MachineReadyToRecieve2, value)
        End Set
    End Property

    Public Property IsReadyToSend As Boolean Implements ISMEMA.IsReadyToSend
        Get
            Return gDOCollection.GetState(enmDO.BoardAvailable2)
        End Get
        Set(value As Boolean)
            gDOCollection.SetState(enmDO.BoardAvailable2, value)
        End Set
    End Property

    Public ReadOnly Property IsLoaderReady As Boolean Implements ISMEMA.IsLoaderReady
        Get
            Return gDICollection.GetState(enmDI.BoardAvailable2)
        End Get
    End Property

    Public ReadOnly Property IsUnloaderReady As Boolean Implements ISMEMA.IsUnloaderReady
        Get
            Return gDICollection.GetState(enmDI.MachineReadyToRecieve2)
        End Get
    End Property
End Class

''' <summary>
''' F350A Roller 控制類別
''' </summary>
''' <remarks></remarks>
Public Class clsF350A_Roller : Implements IRoller

    Dim CvNum As Integer

    Dim _alarm As Boolean
    Public ReadOnly Property Alarm As Boolean Implements IRoller.Alarm
        Get
            Return _alarm
        End Get
    End Property

    Dim _status As IRoller.enmStatus
    ''' <summary>
    ''' 取得當前狀態
    ''' </summary>
    Public ReadOnly Property Status As IRoller.enmStatus Implements IRoller.Status
        Get
            Return _status
        End Get
    End Property

    Dim _loadDirection As IRoller.enmDirection
    ''' <summary>
    ''' 取得 Load 運轉方向
    ''' </summary>
    Public ReadOnly Property LoadDirection As IRoller.enmDirection Implements IRoller.LoadDirection
        Get
            Return _loadDirection
        End Get
    End Property

    Dim _unloadDirection As IRoller.enmDirection
    ''' <summary>
    ''' 取得 Unload 運轉方向
    ''' </summary>
    Public ReadOnly Property UnloadDirection As IRoller.enmDirection Implements IRoller.UnloadDirection
        Get
            Return _unloadDirection
        End Get
    End Property

    ''' <summary>
    ''' 指定流道
    ''' </summary>
    ''' <param name="index">流道編號</param>
    ''' <remarks></remarks>
    Sub New(Optional index As UInteger = 1)
        If index = 1 Then
            CvNum = enmAxis.Conveyor1
        ElseIf index = 2 Then
            CvNum = enmAxis.Conveyor2
        End If

    End Sub

    ''' <summary>
    '''初始化 
    ''' </summary>
    Public Function Initial() As Boolean Implements IRoller.Initial
        Call gCMotion.Servo(CvNum, enmONOFF.eON) '通電確保
        Call gCMotion.AxisResetError(CvNum) '清除錯誤
        Call gCMotion.SetCurve(CvNum, eCurveMode.TCurve) '命令規畫確保
        gCMotion.DOOutput(CvNum, 7, enmCardIOONOFF.eOFF) 'Alarm重製
        gCMotion.DOOutput(CvNum, 7, enmCardIOONOFF.eON)

        [Stop]()

        gCMotion.AxisParameter(CvNum).Velocity.AccRatio = 0.01
        gCMotion.AxisParameter(CvNum).Velocity.DecRatio = 0.01

        If gCMotion.SetAcc(CvNum, gCMotion.AxisParameter(CvNum).Velocity.Acc * gCMotion.AxisParameter(CvNum).Velocity.AccRatio) <> CommandStatus.Sucessed Then
            Return False
        End If

        If gCMotion.SetDec(CvNum, gCMotion.AxisParameter(CvNum).Velocity.Dec * gCMotion.AxisParameter(CvNum).Velocity.DecRatio) <> CommandStatus.Sucessed Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 設定初速度與最大速度
    ''' </summary>
    ''' <param name="low">初速度</param>
    ''' <param name="high">最大速度</param>
    Public Function SetSpeed(ByVal low As Decimal, ByVal high As Decimal) As Boolean Implements IRoller.SetSpeed
        If (high > low) Then
            'If gCMotion.SetVelLow(CvNum, low / 10) <> CommandStatus.Sucessed Then
            If gCMotion.SetVelLow(CvNum, low) <> CommandStatus.Sucessed Then 'Soni / 2017.07.04
                Return False
            End If
            'If gCMotion.SetVelHigh(CvNum, high / 10) <> CommandStatus.Sucessed Then
            If gCMotion.SetVelHigh(CvNum, high) <> CommandStatus.Sucessed Then 'Soni / 2017.07.04
                Return False
            End If

            Return True
        End If

        Return False
    End Function

    ''' <summary>
    ''' 設定進出料方向
    ''' </summary>
    ''' <param name="load">進貨方向</param>
    ''' <param name="unload">卸貨方向</param>
    ''' <remarks></remarks>
    Public Sub SetDirection(load As IRoller.enmDirection, unload As IRoller.enmDirection) Implements IRoller.SetDirection
        _loadDirection = load
        _unloadDirection = unload
    End Sub

    ''' <summary>
    ''' 執行運轉
    ''' </summary>
    Public Sub Run(direction As IRoller.enmDirection) Implements IRoller.Run
        If (direction = IRoller.enmDirection.Forward) Then
            gCMotion.VelMove(CvNum, eDirection.Positive)
        Else
            gCMotion.VelMove(CvNum, eDirection.Negative)
        End If
        _status = IRoller.enmStatus.Load
    End Sub

    ''' <summary>
    ''' 執行Load方向運動
    ''' </summary>
    Public Sub Load() Implements IRoller.Load
        If (LoadDirection = IRoller.enmDirection.Forward) Then
            gCMotion.VelMove(CvNum, eDirection.Positive)
        Else
            gCMotion.VelMove(CvNum, eDirection.Negative)
        End If
        _status = IRoller.enmStatus.Load
    End Sub

    ''' <summary>
    ''' 執行Unlaod方向運動
    ''' </summary>
    Public Sub Unload() Implements IRoller.Unload
        If (UnloadDirection = IRoller.enmDirection.Forward) Then
            gCMotion.VelMove(CvNum, eDirection.Positive)
        Else
            gCMotion.VelMove(CvNum, eDirection.Negative)
        End If
        _status = IRoller.enmStatus.Unload
    End Sub

    ''' <summary>
    '''停止運作 
    ''' </summary>
    Public Sub [Stop]() Implements IRoller.Stop
        gCMotion.SlowStop(CvNum, 100)
        gCMotion.SetPosition(CvNum, 0)
        _status = IRoller.enmStatus.Stop
    End Sub

    Public Sub Load(Optional speed As IRoller.enmSpeed = IRoller.enmSpeed.Normal) Implements IRoller.Load
        Load()
    End Sub

    Public Sub Unload(Optional speed As IRoller.enmSpeed = IRoller.enmSpeed.Normal) Implements IRoller.Unload
        Unload()
    End Sub
End Class
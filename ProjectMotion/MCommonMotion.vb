Imports ProjectCore
Imports ProjectIO
Imports ProjectRecipe

Public Module MCommonMotion


    'Public Enum enmAxisType
    '    ''' <summary>不適用,未定義</summary>
    '    ''' <remarks></remarks>
    '    Undefined = 0
    '    ''' <summary>RS-485 東方CRK系列</summary>
    '    ''' <remarks></remarks>
    '    HM60163C = 1
    '    ''' <summary>東方RKII系列</summary>
    '    ''' <remarks></remarks>
    '    RK2 = 2
    '    ''' <summary>士林電機 微電腦溫度控制器系列</summary>
    '    ''' <remarks></remarks>
    '    WT404 = 3
    'End Enum

    ''' <summary>運動控制卡型號</summary>
    ''' <remarks></remarks>
    Public Enum enmMotionCardType
        ''' <summary>虛擬</summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>Advantech PCI-1245</summary>
        ''' <remarks></remarks>
        PCI_1245 = 1
        ''' <summary>Advantech PCI-1285</summary>
        ''' <remarks></remarks>
        PCI_1285 = 2
        ''' <summary>ModBus</summary>
        ''' <remarks></remarks>
        ModBus = 3
    End Enum

    ''' <summary>[移動速度之快中慢]</summary>
    ''' <remarks></remarks>
    Public Enum SpeedType
        ''' <summary>慢速</summary>
        ''' <remarks></remarks>
        Slow = 0
        ''' <summary>中速</summary>
        ''' <remarks></remarks>
        Medium = 1
        ''' <summary>快速</summary>
        ''' <remarks></remarks>
        Fast = 2
    End Enum

    ''' <summary>命令狀態</summary>
    ''' <remarks></remarks>
    Public Enum CommandStatus
        ''' <summary>命令異常</summary>
        ''' <remarks></remarks>
        Alarm = 0
        ''' <summary>命令成功</summary>
        ''' <remarks></remarks>
        Sucessed = 1
        ''' <summary>警告</summary>
        ''' <remarks></remarks>
        Warning = 2
        ''' <summary>發送中</summary>
        ''' <remarks></remarks>
        Sending = 3
    End Enum

    ' ''' <summary>
    ' ''' Setting the signal source of Latch
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum LTCSource
    '    DI = 0
    '    EZ = 1
    '    ORG = 2
    '    MEL = 3
    'End Enum

    ' ''' <summary>
    ' ''' Setting of active logic for LTC signal
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum LTCLogic
    '    RisingEdge = 0
    '    FallingEdge = 1
    'End Enum

    ' ''' <summary>
    ' ''' INP function enable/disable
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum INPEnable
    '    Disable = 0
    '    Enable = 1
    'End Enum

    ' ''' <summary>
    ' ''' Setting of active logic for INP signal
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum INPLogic
    '    LowActive = 0
    '    HighActive = 1
    'End Enum

    Public Enum enmONOFF
        eOff = 0
        eON = 1
    End Enum

    ' ''' <summary>
    ' ''' encode max pulse in frequency
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum enmEncodePulseInFrequency
    '    e500K = 0
    '    e1M = 1
    '    e2M = 2
    '    e4M = 3
    'End Enum

    'Public Enum enmAlarmEnable
    '    Disable = 0
    '    Enable = 1
    'End Enum

    'Public Enum enmAlarmLogic
    '    LowActive = 0
    '    HighActive = 1
    'End Enum

    'Public Enum enmAlarmStopMode
    '    MotorImmediatelyStop = 0
    '    MotorDeceleratesStop = 1
    'End Enum

    'Public Enum enmBacklashEnable
    '    Disable = 0
    '    Enable = 1
    'End Enum

    'Public Enum enmErcEnableMode
    '    Disable = 0
    '    ErcOutputWhenHomeFinish = 1
    'End Enum

    'Public Enum enmErcLogic
    '    LowActive = 0
    '    HighActive = 1
    'End Enum

    'Public Enum enmExternalDrive
    '    '[說明]:In PCI-1265/PCI-1245/PCI-1245V/PCI-1245E, only support 0
    '    Axis_0 = 0
    '    Axis_1 = 1
    '    Axis_2 = 2
    '    Axis_3 = 3
    'End Enum

    'Public Enum enmExternalDriveEnable
    '    '[說明]:In PCI-1265/PCI-1245/PCI-1245V/PCI-1245E, only support 0
    '    Disabled = 0
    '    Enabled = 1
    'End Enum

    'Public Enum enmExternalDrivePulseInMode
    '    e1XAB = 0
    '    e2XAB = 1
    '    e4XAB = 2
    '    eCWCCW = 3
    'End Enum

    'Public Enum enmINPEnable
    '    Disabled = 0
    '    Enable = 1
    'End Enum

    'Public Enum enmINPLogic
    '    LowActive = 0
    '    HighActive = 1
    'End Enum

    'Public Enum enmTriggerStopEnable
    '    Disabled = 0
    '    Enable = 1
    'End Enum

    'Public Enum enmTriggerStopMode
    '    SuddenStop = 0
    '    Decelerating = 1
    'End Enum

    'Public Enum enmTriggerStopLogic
    '    LowActive = 0
    '    HighActive = 1
    'End Enum

    'Public Enum enmEmgLogic
    '    LowActive = 0
    '    HighActive = 1
    'End Enum

    'Public Enum enmOrgLogic
    '    LowActive = 0
    '    HighActive = 1
    'End Enum

    'Public Enum enmLimitStopMode
    '    MotorImmediatelyStop = 0
    '    MotorDeceleratesStop = 1
    'End Enum

    'Public Enum enmLimitLogic
    '    LowActive = 0
    '    HighActive = 1
    'End Enum

    'Public Enum enmLimitEnable
    '    Disabled = 0
    '    Enabled = 1
    'End Enum

    'Public Enum enmEZLogic
    '    LowActive = 0
    '    HighActive = 1
    'End Enum

    Public Enum enmPositionType
        CommandPosition = 0
        ActualPosition = 1
    End Enum

    'Public Enum enmLatchEnable
    '    Disabled = 0
    '    Enable = 1
    'End Enum

    'Public Enum enmLatchPLogic
    '    LowActive = 0
    '    HighActive = 1
    'End Enum

    'Public Enum enmCompareSource
    '    CommandPosition = 0
    '    ActualPosition = 1
    'End Enum

    Public Enum enmCardIOONOFF
        eOFF = 0
        eON = 1
    End Enum

    ' ''' <summary>
    ' ''' In PCI-1245/1245V/1245E/1265, the default value is 0.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum enmCompareMethod
    '    ''' <summary>[大於 Position Counter]</summary>
    '    ''' <remarks></remarks>
    '    More = 0
    '    ''' <summary>[小於 Position Counter]</summary>
    '    ''' <remarks></remarks>
    '    Small = 1
    '    ''' <summary>[等於 Counter (Directionless)(Not support)] </summary>
    '    ''' <remarks></remarks>
    '    Equal = 2
    'End Enum

    'Public Enum enmCompareEnable
    '    Disabled = 0
    '    Enable = 1
    'End Enum

    ' ''' <summary>
    ' ''' 脈衝輸入邏輯
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum enmPulseInLogic
    '    ''' <summary>
    '    ''' 非反向
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    NotInverseDirection = 0
    '    ''' <summary>
    '    ''' 反向
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    InverseDirection = 1
    'End Enum
    'Public Enum enmPulseOutReverse
    '    Revserse_Disable = 0
    '    Reverse_Enable = 1
    'End Enum

    Public Enum enmPulseOutMode
        OUT_DIR = 1
        OUT_DIR_OUT_NegativeLogic = 2
        OUT_DIR_DIR_NegativeLogic = 4
        OUT_DIR_OUTDIR_NegativeLogic = 8
        CW_CCW = 16
        CW_CCV_CWCCW_NegativeLogic = 32
        AB_Phase = 64
        BA_Phase = 128
        CW_CCV_OUT_NegativeLogic = 256
        CW_CCV_DIR_NegativeLogic = 512
    End Enum

    'Public Enum enmPulseInMode
    '    e1XAB = 0
    '    e2XAB = 1
    '    e4XAB = 2
    '    eCWCCW = 3
    'End Enum


    'Public Enum enmHomeReset
    '    Disabled = 0
    '    Enable = 1
    'End Enum

    'Public Enum enmSFEnable
    '    Disabled = 0
    '    Enable = 1
    'End Enum

    Public Enum eCurveMode
        ''' <summary>[T Curve 移動]</summary>
        ''' <remarks></remarks>
        TCurve = 0
        ''' <summary>[S Curve 移動]</summary>
        ''' <remarks></remarks>
        SCurve = 1
    End Enum


    Public PathCountLimit As Integer = 3
    '-----------------------------------------------------------
    ''' <summary>軸卡轉接層</summary>
    ''' <remarks></remarks>
    Public gCMotion As New Premtek.Base.CMotionCollection

    ' ''' <summary>給定</summary>
    ' ''' <param name="posX"></param>
    ' ''' <param name="posY"></param>
    ' ''' <param name="MaxVel"></param>
    ' ''' <param name="Acc"></param>
    ' ''' <param name="dec"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function DoLoopAbsMove(ByVal enmAxisX As Integer, ByVal enmAxisY As Integer, ByVal posX As Decimal, ByVal posY As Decimal, ByVal MaxVel As Double, ByVal Acc As Double, ByVal dec As Double) As Boolean
    '    Const MOVE_TIME_OUT = 10000
    '    If gCMotion.MotionDone(enmAxisX) <> CommandStatus.Sucessed Then
    '        Return False
    '    End If
    '    If gCMotion.MotionDone(enmAxisY) <> CommandStatus.Sucessed Then
    '        Return False
    '    End If
    '    gCMotion.SetVelHigh(enmAxisX, MaxVel)
    '    gCMotion.SetVelHigh(enmAxisY, MaxVel)
    '    gCMotion.SetAcc(enmAxisX, Acc)
    '    gCMotion.SetAcc(enmAxisY, Acc)
    '    gCMotion.SetDec(enmAxisX, dec)
    '    gCMotion.SetDec(enmAxisY, dec)

    '    If gCMotion.AbsMove(enmAxisX, posX) <> CommandStatus.Sucessed Then
    '        MsgBox("(" & enmAxisX & ")" & gCMotion.AxisParameter(enmAxisX).AxisName & " AbsMove Error")
    '        Return False
    '    End If
    '    If gCMotion.AbsMove(enmAxisY, posY) <> CommandStatus.Sucessed Then
    '        MsgBox("(" & enmAxisY & ")" & gCMotion.AxisParameter(enmAxisY).AxisName & " AbsMove Error")
    '        Return False
    '    End If

    '    System.Threading.Thread.CurrentThread.Join(100)
    '    Dim sw As New Stopwatch
    '    sw.Start()
    '    Do
    '        Application.DoEvents()
    '        If gCMotion.MotionDone(enmAxisX) = CommandStatus.Sucessed Then
    '            If gCMotion.MotionDone(enmAxisY) = CommandStatus.Sucessed Then
    '                Exit Do
    '            ElseIf sw.ElapsedMilliseconds > MOVE_TIME_OUT Then
    '                gSyslog.Save(gMsgHandler.GetMessage(Error_1031004), "Error_1031004", eMessageLevel.Error)
    '                MsgBox(gMsgHandler.GetMessage(Error_1031004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    '                Return False
    '            End If
    '        ElseIf sw.ElapsedMilliseconds > MOVE_TIME_OUT Then
    '            gSyslog.Save(gMsgHandler.GetMessage(Error_1030004), "Error_1030004", eMessageLevel.Error)
    '            MsgBox(gMsgHandler.GetMessage(Error_1030004))
    '            Return False
    '        End If
    '    Loop
    '    Return True
    'End Function


    ' ''' <summary>Sensor標籤背景色套用</summary>
    ' ''' <param name="refLabel"></param>
    ' ''' <param name="status"></param>
    ' ''' <param name="isPass"></param>
    ' ''' <remarks></remarks>
    'Public Sub SetSensorBackColor(ByRef refLabel As System.Windows.Forms.Label, ByVal status As Boolean, ByVal isPass As Boolean)
    '    If isPass Then
    '        refLabel.BackColor = System.Drawing.Color.Gray
    '        Exit Sub
    '    End If
    '    If status Then
    '        refLabel.BackColor = System.Drawing.Color.Red
    '    Else
    '        refLabel.BackColor = System.Drawing.Color.White
    '    End If
    'End Sub

End Module

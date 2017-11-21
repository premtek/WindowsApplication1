Imports ProjectCore
Imports ProjectMotion

Public Module MMotorParameter

    ''' <summary>將MotionStatus拆除</summary>
    ''' <remarks></remarks>
    Public Enum AxisState
        STA_AX_DISABLE = 0
        STA_AX_READY = 1
        STA_AX_STOPPING = 2
        STA_AX_ERROR_STOP = 3
        STA_AX_HOMING = 4
        STA_AX_PTP_MOT = 5
        STA_AX_CONTI_MOT = 6
        STA_AX_SYNC_MOT = 7
        STA_AX_EXT_JOG = 8
        STA_AX_EXT_MPG = 9
    End Enum
    ' ''20171013
    '' ''' <summary>將AxisMotionStatus拆除</summary>
    '' ''' <remarks></remarks>
    ''Public Enum AxisMotionState
    ''    Res1 = 1
    ''    WaitERC = 2
    ''    Res2 = 3
    ''    CorrectBksh = 4
    ''    Res3 = 5
    ''    InFA = 6
    ''    InFL = 7
    ''    InAcc = 8
    ''    InFH = 9
    ''    InDEC = 10
    ''    WaitINP = 11
    ''End Enum
    ' ''' <summary>馬達類型(決定Function作動方式)</summary>
    ' ''' <remarks></remarks>
    'Public Enum eMotorType
    '    ''' <summary>伺服馬達</summary>
    '    ''' <remarks></remarks>
    '    ServoMotor = 0
    '    ''' <summary>步進馬達</summary>
    '    ''' <remarks></remarks>
    '    SteppingMotor = 1
    '    ''' <summary>電動缸</summary>
    '    ''' <remarks></remarks>
    '    ElectricCylinder = 2
    '    ''' <summary>[預設類型]</summary>
    '    ''' <remarks></remarks>
    '    None = 3
    'End Enum
    ''' <summary>座標方向類型</summary>
    ''' <remarks></remarks>
    Public Enum CoordinateType
        ''' <summary>對使用者為 第一象限 X軸座標</summary>
        ''' <remarks></remarks>
        Coordinate1X = 0
        ''' <summary>對使用者為 第一象限 Y軸座標</summary>
        ''' <remarks></remarks>
        Coordinate1Y = 1
        ''' <summary>對使用者為 第一象限 Z軸座標</summary>
        ''' <remarks></remarks>
        Coordinate1Z = 2
        ''' <summary>對使用者為 第一象限 對X軸旋轉座標(逆時針為正)</summary>
        ''' <remarks></remarks>
        Coordinate1A = 3
        ''' <summary>對使用者為 第一象限 對Y軸旋轉座標(逆時針為正)</summary>
        ''' <remarks></remarks>
        Coordinate1B = 4
        ''' <summary>對使用者為 第一象限 對Z軸旋轉座標(逆時針為正)</summary>
        ''' <remarks></remarks>
        Coordinate1C = 5
    End Enum

    ' ''' <summary>[記錄軸的所有參數] </summary>
    ' ''' <remarks></remarks>
    'Public Structure SMotor
    '    ''' <summary>軸名稱</summary>
    '    ''' <remarks></remarks>
    '    Public AxisName As String
    '    ''' <summary>"顯示"的座標方向,不是實際的</summary>
    '    ''' <remarks></remarks>
    '    Public Coordinate As CoordinateType
    '    ''' <summary>復歸參數</summary>
    '    ''' <remarks></remarks>
    '    Public HomeParameter As SHomeParameter
    '    ''' <summary>極限設定</summary>
    '    ''' <remarks></remarks>
    '    Public Limit As SLimit
    '    ''' <summary>速度設定</summary>
    '    ''' <remarks></remarks>
    '    Public Velocity As SVelocity
    '    ''' <summary>驅動器參數</summary>
    '    ''' <remarks></remarks>
    '    Public Parameter As SDriverParameter
    '    ''' <summary>運動控制卡參數</summary>
    '    ''' <remarks></remarks>
    '    Public CardParameter As SCardParameter
    '    ''' <summary>IO狀態</summary>
    '    ''' <remarks></remarks>
    '    Public MotionIOStatus As IOStatus
    '    ''' <summary>運動狀態</summary>
    '    ''' <remarks></remarks>
    '    Public MotionStatus As AxisState 'Status

    '    '20171013 新增get 軸運動狀態參數
    '    ''' <summary>軸運動狀態</summary>
    '    ''' <remarks></remarks>
    '    Public AxisMotionStatus As AxisMotionState 'Status

    '    ''' <summary>到位穩定時間(ms)</summary>
    '    ''' <remarks></remarks>
    '    Public InpositionStableTime As Double
    '    ''' <summary>讀取單軸設定檔</summary>
    '    ''' <param name="strFileName"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function Load(ByVal strFileName As String) As Boolean
    '        Try

    '            Dim strSection As String
    '            strSection = "HomeParameter"
    '            With HomeParameter
    '                .HomeMode = CInt(ReadIniString(strSection, "HomeMode", strFileName, 11))
    '                .HomeDirection = CInt(ReadIniString(strSection, "HomeDirection", strFileName, 1))
    '                .HomeExSwitchMode = CInt(ReadIniString(strSection, "HomeExSwitchMode", strFileName, 2))
    '                .HomeCrossDistance = CInt(ReadIniString(strSection, "HomeCrossDistance", strFileName, 100))
    '                .HomeOffset = CDbl(ReadIniString(strSection, "HomeOffset", strFileName, 0))
    '                .IsHomeDouble = CBool(ReadIniString(strSection, "IsHomeDouble", strFileName, False))
    '                '[Note]:預設為MODE7_AbsSearch  Move (Dir) ->Search ORG ->Stop
    '                .HomeDoubleMode = CInt(ReadIniString(strSection, "HomeDoubleMode", strFileName, 6))
    '            End With

    '            strSection = "Limit"
    '            With Limit
    '                .PosivtiveLimit = CDbl(ReadIniString(strSection, "PosivtiveLimit", strFileName, 200))
    '                .NegativeLimit = CDbl(ReadIniString(strSection, "NegativeLimit", strFileName, -1))
    '                .HLimitEnable = CInt(ReadIniString(strSection, "HLimitEnable", strFileName, CInt(enmLimitEnable.Enabled)))
    '                .HLimitLogic = CInt(ReadIniString(strSection, "HLimitLogic", strFileName, CInt(enmLimitLogic.HighActive)))
    '                .HLimitStopMode = CInt(ReadIniString(strSection, "HLimitStopMode", strFileName, CInt(enmLimitStopMode.MotorImmediatelyStop)))
    '            End With

    '            strSection = "Velocity"
    '            With Velocity
    '                .HomeVelLow = CDbl(ReadIniString(strSection, "HomeVelLow", strFileName, 0.2))
    '                .HomeVelHigh = CDbl(ReadIniString(strSection, "HomeVelHigh", strFileName, 20))
    '                .HomeAcc = CDbl(ReadIniString(strSection, "HomeAcc", strFileName, 1000))
    '                .HomeDec = CDbl(ReadIniString(strSection, "HomeDec", strFileName, 1000))
    '                .VelLow = CDbl(ReadIniString(strSection, "VelLow", strFileName, 0))
    '                .VelHigh = CDbl(ReadIniString(strSection, "VelHigh", strFileName, 100))
    '                .Acc = CDbl(ReadIniString(strSection, "Acc", strFileName, 9800))
    '                .Dec = CDbl(ReadIniString(strSection, "Dec", strFileName, 9800))
    '                .MaxAcc = CDbl(ReadIniString(strSection, "MaxAcc", strFileName, 490000))
    '                .MaxDec = CDbl(ReadIniString(strSection, "MaxDec", strFileName, 490000))
    '                .MaxVel = CDbl(ReadIniString(strSection, "MaxVel", strFileName, 1200))
    '                .AccRatio = CDec(ReadIniString(strSection, "AccRatio", strFileName, 1.0))
    '                .DecRatio = CDec(ReadIniString(strSection, "DecRatio", strFileName, 1.0))
    '            End With

    '            strSection = "Parameter"
    '            With Parameter
    '                .PPU = CDbl(ReadIniString(strSection, "PPU", strFileName, 1))
    '                .Scale = CDbl(ReadIniString(strSection, "Scale", strFileName, 1000))
    '                .Direction = CInt(ReadIniString(strSection, "Direction", strFileName, 0))
    '                .PulseInDirection = CInt(ReadIniString(strSection, "PulseInDirection", strFileName, CInt(enmPulseInLogic.NotInverseDirection)))
    '                .PulseInMode = CInt(ReadIniString(strSection, "PulseInMode", strFileName, CInt(enmPulseInMode.e4XAB)))
    '                .PulseInMaxFreq = CInt(ReadIniString(strSection, "PulseInMaxFreq", strFileName, CInt(enmEncodePulseInFrequency.e1M)))
    '                .PulseOutMode = CInt(ReadIniString(strSection, "PulseOutMode", strFileName, CInt(enmPulseOutMode.CW_CCW)))
    '                .PulseOutReverse = CInt(ReadIniString(strSection, "PulseOutReverse", strFileName, CInt(enmPulseOutReverse.Revserse_Disable)))

    '                .AlarmLogic = CInt(ReadIniString(strSection, "AlarmLogic", strFileName, CInt(enmAlarmLogic.HighActive)))
    '                .AlarmStopMode = CInt(ReadIniString(strSection, "AlarmStopMode", strFileName, CInt(enmAlarmStopMode.MotorDeceleratesStop)))
    '                .AlarmEnable = CInt(ReadIniString(strSection, "AlarmEnable", strFileName, CInt(enmAlarmEnable.Disable)))

    '                .BacklashEnable = CInt(ReadIniString(strSection, "BacklashEnable", strFileName, CInt(enmBacklashEnable.Disable)))
    '                .OrgLogic = CInt(ReadIniString(strSection, "OrgLogic", strFileName, CInt(enmOrgLogic.LowActive)))
    '                .EZLogic = CInt(ReadIniString(strSection, "EZLogic", strFileName, CInt(enmEZLogic.LowActive)))
    '                .HomeReset = CInt(ReadIniString(strSection, "HomeReset", strFileName, CInt(enmHomeReset.Enable)))
    '                .INPEnable = CInt(ReadIniString(strSection, "INPEnable", strFileName, CInt(enmINPEnable.Enable)))
    '                .INPLogic = CInt(ReadIniString(strSection, "INPLogic", strFileName, CInt(enmINPLogic.LowActive)))

    '                .TriggerStopEnable1 = CInt(ReadIniString(strSection, "TriggerStopEnable1", strFileName, CInt(enmTriggerStopEnable.Disabled)))
    '                .TriggerStopEnable2 = CInt(ReadIniString(strSection, "TriggerStopEnable2", strFileName, CInt(enmTriggerStopEnable.Disabled)))
    '                .TriggerStopEnable3 = CInt(ReadIniString(strSection, "TriggerStopEnable3", strFileName, CInt(enmTriggerStopEnable.Disabled)))
    '                .TriggerStopEnable4 = CInt(ReadIniString(strSection, "TriggerStopEnable4", strFileName, CInt(enmTriggerStopEnable.Disabled)))
    '                .TriggerStopEnable5 = CInt(ReadIniString(strSection, "TriggerStopEnable5", strFileName, CInt(enmTriggerStopEnable.Disabled)))
    '                .TriggerStopLogic1 = CInt(ReadIniString(strSection, "TriggerStopLogic1", strFileName, CInt(enmTriggerStopLogic.LowActive)))
    '                .TriggerStopLogic2 = CInt(ReadIniString(strSection, "TriggerStopLogic2", strFileName, CInt(enmTriggerStopLogic.LowActive)))
    '                .TriggerStopLogic3 = CInt(ReadIniString(strSection, "TriggerStopLogic3", strFileName, CInt(enmTriggerStopLogic.LowActive)))
    '                .TriggerStopLogic4 = CInt(ReadIniString(strSection, "TriggerStopLogic4", strFileName, CInt(enmTriggerStopLogic.LowActive)))
    '                .TriggerStopLogic5 = CInt(ReadIniString(strSection, "TriggerStopLogic5", strFileName, CInt(enmTriggerStopLogic.LowActive)))
    '                .TriggerStopMode1 = CInt(ReadIniString(strSection, "TriggerStopMode1", strFileName, CInt(enmTriggerStopMode.Decelerating)))
    '                .TriggerStopMode2 = CInt(ReadIniString(strSection, "TriggerStopMode2", strFileName, CInt(enmTriggerStopMode.Decelerating)))
    '                .TriggerStopMode3 = CInt(ReadIniString(strSection, "TriggerStopMode3", strFileName, CInt(enmTriggerStopMode.Decelerating)))
    '                .TriggerStopMode4 = CInt(ReadIniString(strSection, "TriggerStopMode4", strFileName, CInt(enmTriggerStopMode.Decelerating)))
    '                .TriggerStopMode5 = CInt(ReadIniString(strSection, "TriggerStopMode5", strFileName, CInt(enmTriggerStopMode.Decelerating)))

    '                .LatchEnable = CInt(ReadIniString(strSection, "LatchEnable", strFileName, CInt(enmLatchEnable.Enable)))
    '                .LatchLogic = CInt(ReadIniString(strSection, "LatchLogic", strFileName, CInt(enmLatchPLogic.LowActive)))

    '                .ErcEnable = CInt(ReadIniString(strSection, "ErcEnable", strFileName, CInt(enmErcEnableMode.Disable)))
    '                .ErcLogic = CInt(ReadIniString(strSection, "ErcLogic", strFileName, CInt(enmErcLogic.HighActive)))

    '                .ExternalDriveAxis = CInt(ReadIniString(strSection, "ExternalDriveAxis", strFileName, CInt(enmExternalDrive.Axis_0)))
    '                .ExternalDriveEnable = CInt(ReadIniString(strSection, "ExternalDriveEnable", strFileName, CInt(enmExternalDriveEnable.Disabled)))
    '                .ExternalDrivePulseInMode = CInt(ReadIniString(strSection, "ExternalDrivePulseInMode", strFileName, CInt(enmExternalDrive.Axis_0)))

    '                .ButtonDirection = CInt(ReadIniString(strSection, "ButtonDirection", strFileName, 0))
    '                .MotorType = CInt(ReadIniString(strSection, "MotorType", strFileName, 0))
    '                .IsEncoderExist = CInt(ReadIniString(strSection, "IsEncoderExist", strFileName, 1))
    '                Me.InpositionStableTime = Val(ReadIniString(strSection, "InpositionStableTime", strFileName, 10))
    '            End With

    '            strSection = "CardParameter"
    '            With CardParameter
    '                .AxisNo = CInt(ReadIniString(strSection, "AxisNo", strFileName, 0))
    '                .CardType = CInt(ReadIniString(strSection, "CardType", strFileName, 0))
    '                .AxisType = CInt(ReadIniString(strSection, "AxisType", strFileName, 0))
    '                .ItemNo = Val(ReadIniString(strSection, "Ticket", strFileName, 0))
    '            End With
    '            Me.AxisName = ReadIniString(strSection, "AxisName", strFileName, "") 'Soni 2017.03.22 沒該軸, 不顯示
    '            Me.Coordinate = CInt(ReadIniString(strSection, "Coordinate", strFileName, 0))

    '            Return True

    '        Catch ex As Exception
    '            gSyslog.Save(gMsgHandler.GetMessage(Error_1002010), "Error_1002010", eMessageLevel.Error)
    '            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
    '            MsgBox(gMsgHandler.GetMessage(Error_1002010) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    '            Return False
    '        End Try

    '    End Function


    '    ''' <summary>儲存單軸設定檔</summary>
    '    ''' <param name="strFileName"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function Save(ByVal strFileName As String) As Boolean
    '        Try

    '            Dim strSection As String
    '            'strFileName = Application.StartupPath & "\Motor\XAxis.ini"
    '            strSection = "HomeParameter"
    '            With HomeParameter
    '                Call SaveIniString(strSection, "HomeMode", CInt(.HomeMode), strFileName)
    '                Call SaveIniString(strSection, "HomeDirection", CInt(.HomeDirection), strFileName)
    '                Call SaveIniString(strSection, "HomeExSwitchMode", CInt(.HomeExSwitchMode), strFileName)
    '                Call SaveIniString(strSection, "HomeCrossDistance", .HomeCrossDistance, strFileName)
    '                Call SaveIniString(strSection, "HomeOffset", .HomeOffset, strFileName)
    '                Call SaveIniString(strSection, "IsHomeDouble", CInt(.IsHomeDouble), strFileName)
    '                Call SaveIniString(strSection, "HomeDoubleMode", CInt(.HomeDoubleMode), strFileName)
    '            End With

    '            strSection = "Limit"
    '            With Limit
    '                Call SaveIniString(strSection, "PosivtiveLimit", .PosivtiveLimit, strFileName)
    '                Call SaveIniString(strSection, "NegativeLimit", .NegativeLimit, strFileName)
    '                Call SaveIniString(strSection, "HLimitEnable", CInt(.HLimitEnable), strFileName)
    '                Call SaveIniString(strSection, "HLimitLogic", CInt(.HLimitLogic), strFileName)
    '                Call SaveIniString(strSection, "HLimitStopMode", CInt(.HLimitStopMode), strFileName)
    '            End With

    '            strSection = "Velocity"
    '            With Velocity
    '                Call SaveIniString(strSection, "HomeVelLow", .HomeVelLow, strFileName)
    '                Call SaveIniString(strSection, "HomeVelHigh", .HomeVelHigh, strFileName)
    '                Call SaveIniString(strSection, "HomeAcc", .HomeAcc, strFileName)
    '                Call SaveIniString(strSection, "HomeDec", .HomeDec, strFileName)
    '                Call SaveIniString(strSection, "VelLow", .VelLow, strFileName)
    '                Call SaveIniString(strSection, "VelHigh", .VelHigh, strFileName)
    '                Call SaveIniString(strSection, "Acc", .Acc, strFileName)
    '                Call SaveIniString(strSection, "Dec", .Dec, strFileName)
    '                Call SaveIniString(strSection, "MaxAcc", .MaxAcc, strFileName)
    '                Call SaveIniString(strSection, "MaxDec", .MaxDec, strFileName)
    '                Call SaveIniString(strSection, "MaxVel", .MaxVel, strFileName)
    '                Call SaveIniString(strSection, "AccRatio", .AccRatio, strFileName)
    '                Call SaveIniString(strSection, "DecRatio", .DecRatio, strFileName)
    '            End With

    '            strSection = "Parameter"
    '            With Parameter
    '                Call SaveIniString(strSection, "PPU", .PPU, strFileName)
    '                Call SaveIniString(strSection, "Scale", .Scale, strFileName)
    '                Call SaveIniString(strSection, "Direction", CInt(.Direction), strFileName)
    '                Call SaveIniString(strSection, "PulseInDirection", CInt(.PulseInDirection), strFileName)
    '                Call SaveIniString(strSection, "PulseInMode", CInt(.PulseInMode), strFileName)
    '                Call SaveIniString(strSection, "PulseInMaxFreq", CInt(.PulseInMaxFreq), strFileName)
    '                Call SaveIniString(strSection, "PulseOutMode", CInt(.PulseOutMode), strFileName)
    '                Call SaveIniString(strSection, "PulseOutReverse", CInt(.PulseOutReverse), strFileName)

    '                Call SaveIniString(strSection, "AlarmLogic", CInt(.AlarmLogic), strFileName)
    '                Call SaveIniString(strSection, "AlarmStopMode", CInt(.AlarmStopMode), strFileName)
    '                Call SaveIniString(strSection, "AlarmEnable", CInt(.AlarmEnable), strFileName)

    '                Call SaveIniString(strSection, "BacklashEnable", CInt(.BacklashEnable), strFileName)
    '                Call SaveIniString(strSection, "OrgLogic", CInt(.OrgLogic), strFileName)
    '                Call SaveIniString(strSection, "EZLogic", CInt(.EZLogic), strFileName)
    '                Call SaveIniString(strSection, "HomeReset", CInt(.HomeReset), strFileName)
    '                Call SaveIniString(strSection, "INPEnable", CInt(.INPEnable), strFileName)
    '                Call SaveIniString(strSection, "INPLogic", CInt(.INPLogic), strFileName)

    '                Call SaveIniString(strSection, "TriggerStopEnable1", CInt(.TriggerStopEnable1), strFileName)
    '                Call SaveIniString(strSection, "TriggerStopEnable2", CInt(.TriggerStopEnable2), strFileName)
    '                Call SaveIniString(strSection, "TriggerStopEnable3", CInt(.TriggerStopEnable3), strFileName)
    '                Call SaveIniString(strSection, "TriggerStopEnable4", CInt(.TriggerStopEnable4), strFileName)
    '                Call SaveIniString(strSection, "TriggerStopEnable5", CInt(.TriggerStopEnable5), strFileName)

    '                Call SaveIniString(strSection, "TriggerStopLogic1", CInt(.TriggerStopLogic1), strFileName)
    '                Call SaveIniString(strSection, "TriggerStopLogic2", CInt(.TriggerStopLogic2), strFileName)
    '                Call SaveIniString(strSection, "TriggerStopLogic3", CInt(.TriggerStopLogic3), strFileName)
    '                Call SaveIniString(strSection, "TriggerStopLogic4", CInt(.TriggerStopLogic4), strFileName)
    '                Call SaveIniString(strSection, "TriggerStopLogic5", CInt(.TriggerStopLogic5), strFileName)

    '                Call SaveIniString(strSection, "TriggerStopMode1", CInt(.TriggerStopMode1), strFileName)
    '                Call SaveIniString(strSection, "TriggerStopMode2", CInt(.TriggerStopMode2), strFileName)
    '                Call SaveIniString(strSection, "TriggerStopMode3", CInt(.TriggerStopMode3), strFileName)
    '                Call SaveIniString(strSection, "TriggerStopMode4", CInt(.TriggerStopMode4), strFileName)
    '                Call SaveIniString(strSection, "TriggerStopMode5", CInt(.TriggerStopMode5), strFileName)

    '                Call SaveIniString(strSection, "LatchEnable", CInt(.LatchEnable), strFileName)
    '                Call SaveIniString(strSection, "LatchLogic", CInt(.LatchLogic), strFileName)

    '                Call SaveIniString(strSection, "ErcEnable", CInt(.ErcEnable), strFileName)
    '                Call SaveIniString(strSection, "ErcLogic", CInt(.ErcLogic), strFileName)

    '                Call SaveIniString(strSection, "ExternalDriveAxis", CInt(.ExternalDriveAxis), strFileName)
    '                Call SaveIniString(strSection, "ExternalDriveEnable", CInt(.ExternalDriveEnable), strFileName)
    '                Call SaveIniString(strSection, "ExternalDrivePulseInMode", CInt(.ExternalDrivePulseInMode), strFileName)

    '                Call SaveIniString(strSection, "ButtonDirection", CInt(.ButtonDirection), strFileName)
    '                Call SaveIniString(strSection, "MotorType", CInt(.MotorType), strFileName)
    '                Call SaveIniString(strSection, "IsEncoderExist", CInt(.IsEncoderExist), strFileName)
    '                Call SaveIniString(strSection, "InpositionStableTime", Me.InpositionStableTime, strFileName)
    '            End With

    '            strSection = "CardParameter"
    '            With CardParameter
    '                Call SaveIniString(strSection, "AxisNo", .AxisNo, strFileName)
    '                Call SaveIniString(strSection, "CardType", CInt(.CardType), strFileName)
    '                Call SaveIniString(strSection, "AxisType", CInt(.AxisType), strFileName)
    '                Call SaveIniString(strSection, "Ticket", CInt(.ItemNo), strFileName)
    '            End With

    '            Call SaveIniString(strSection, "AxisName", Me.AxisName, strFileName)
    '            Call SaveIniString(strSection, "Coordinate", CInt(Me.Coordinate), strFileName)


    '            Return True

    '        Catch ex As Exception
    '            gSyslog.Save(gMsgHandler.GetMessage(Error_1002011), "Error_1002011", eMessageLevel.Error)
    '            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
    '            MsgBox(gMsgHandler.GetMessage(Error_1002011) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    '            Return False
    '        End Try
    '    End Function

    'End Structure

    ' ''' <summary>[回Home參數] </summary>
    ' ''' <remarks></remarks>
    'Public Structure SHomeParameter
    '    ''' <summary>單步復歸偏移量(不是HomeOffset)</summary>
    '    ''' <remarks></remarks>
    '    Public HomeCrossDistance As Double
    '    ''' <summary>原點復歸模式</summary>
    '    ''' <remarks></remarks>
    '    Public HomeMode As enmHomeMode
    '    Public HomeExSwitchMode As enmHomeExSwitchMode
    '    ''' <summary>復歸方向</summary>
    '    ''' <remarks></remarks>
    '    Public HomeDirection As eDirection
    '    ''' <summary>原點復歸偏移量</summary>
    '    ''' <remarks></remarks>
    '    Public HomeOffset As Double
    '    ''' <summary>[判斷是否執行二次復歸(此為非正常流程)]</summary>
    '    ''' <remarks></remarks>
    '    Public IsHomeDouble As Boolean
    '    ''' <summary>[執行二次復歸之原點復歸模式(此為第一次復歸的方式，第二次復歸使用原本的原點復歸方式)]</summary>
    '    ''' <remarks></remarks>
    '    Public HomeDoubleMode As enmHomeMode
    'End Structure

    ' ''' <summary>
    ' ''' 回原點模式
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum enmHomeMode
    '    Abs = 0
    '    Lmt = 1
    '    Ref = 2
    '    Abs_Ref = 3
    '    Abs_NegRef = 4
    '    Lmt_Ref = 5
    '    AbsSearch = 6
    '    LmtSeaarch = 7
    '    AbsSearch_Ref = 8
    '    AbsSearch_NegRef = 9
    '    LmtSearch_Ref = 10
    '    AbsSearchReFind = 11
    '    LmtSearchReFind = 12
    '    AbsSearchReFind_Ref = 13
    '    AbsSearchReFind_NegRe = 14
    '    LmtSearchReFind_Ref = 15
    'End Enum

    ' ''' <summary>
    ' ''' Setting the stopping condition of Acm_AxHomeEx.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum enmHomeExSwitchMode
    '    ''' <summary>[When sensor is ON(Active)] </summary>
    '    ''' <remarks></remarks>
    '    LevelOn = 0
    '    ''' <summary>[When sensor is OFF(Non-active)]</summary>
    '    ''' <remarks></remarks>
    '    LevelOff = 1
    '    ''' <summary>[When OFF to ON transition in sensor]</summary>
    '    ''' <remarks></remarks>
    '    RisingEdge = 2
    '    ''' <summary>[When ON to OFF transition in sensor]</summary>
    '    ''' <remarks></remarks>
    '    FallingEdge = 3
    'End Enum

    ' ''' <summary>[Limit]</summary>
    ' ''' <remarks></remarks>
    'Public Structure SLimit
    '    ''' <summary>正極限(mm)</summary>
    '    ''' <remarks></remarks>
    '    Public PosivtiveLimit As Decimal
    '    ''' <summary>負極限(mm)</summary>
    '    ''' <remarks></remarks>
    '    Public NegativeLimit As Decimal

    '    ''' <summary>
    '    ''' 硬體極限致能
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Public HLimitEnable As enmLimitEnable
    '    ''' <summary>
    '    ''' 硬體極限邏輯
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Public HLimitLogic As enmLimitLogic
    '    ''' <summary>硬體極限停止模式</summary>
    '    ''' <remarks></remarks>
    '    Public HLimitStopMode As enmLimitStopMode
    'End Structure

    ' ''' <summary>[Velocity] </summary>
    ' ''' <remarks></remarks>
    'Public Structure SVelocity
    '    ''' <summary>[回Home初速度]</summary>
    '    ''' <remarks></remarks>
    '    Public HomeVelLow As Decimal
    '    ''' <summary>[回Home最大速]</summary>
    '    ''' <remarks></remarks>
    '    Public HomeVelHigh As Decimal
    '    ''' <summary>[回Home加速度] </summary>
    '    ''' <remarks></remarks>
    '    Public HomeAcc As Decimal
    '    ''' <summary>[回Home減速度] </summary>
    '    ''' <remarks></remarks>
    '    Public HomeDec As Decimal
    '    ''' <summary>[初速度]</summary>
    '    ''' <remarks></remarks>
    '    Public VelLow As Decimal
    '    ''' <summary>[最大速]</summary>
    '    ''' <remarks></remarks>
    '    Public VelHigh As Decimal
    '    ''' <summary>[加速度] </summary>
    '    ''' <remarks></remarks>
    '    Public Acc As Decimal
    '    ''' <summary>[減速度]</summary>
    '    ''' <remarks></remarks>
    '    Public Dec As Decimal
    '    ''' <summary>[速度上限值]</summary>
    '    ''' <remarks></remarks>
    '    Public MaxVel As Double
    '    ''' <summary>[加速度上限值]</summary>
    '    ''' <remarks></remarks>
    '    Public MaxAcc As Double
    '    ''' <summary>[減速度上限值]</summary>
    '    ''' <remarks></remarks>
    '    Public MaxDec As Double
    '    ''' <summary>[加速比例設定(0~1)]</summary>
    '    ''' <remarks></remarks>
    '    Public AccRatio As Decimal
    '    ''' <summary>[減速比例設定(0~1)]</summary>
    '    ''' <remarks></remarks>
    '    Public DecRatio As Decimal
    'End Structure

    '    ''' <summary>驅動器參數</summary>
    '    ''' <remarks></remarks>
    '    Public Structure SDriverParameter
    '        ''' <summary>轉換比例</summary>
    '        ''' <remarks></remarks>
    '        Public Scale As Decimal
    '        ''' <summary>單位脈衝</summary>
    '        ''' <remarks></remarks>
    '        Public PPU As Decimal
    '        ''' <summary>運動方向</summary>
    '        ''' <remarks></remarks>
    '        Public Direction As eDirection 'TODO: Direction要移除

    '#Region "PulseIn"
    '        ''' <summary>脈衝輸入(Encoder)方向</summary>
    '        ''' <remarks></remarks>
    '        Public PulseInDirection As enmPulseInLogic
    '        ''' <summary>脈衝輸入方式</summary>
    '        ''' <remarks></remarks>
    '        Public PulseInMode As enmPulseInMode
    '        ''' <summary>編碼器最大輸入頻率</summary>
    '        ''' <remarks></remarks>
    '        Public PulseInMaxFreq As enmEncodePulseInFrequency
    '#End Region
    '        ''' <summary>輸出方向反轉(碰極限不反轉者)</summary>
    '        ''' <remarks></remarks>
    '        Public PulseOutReverse As enmPulseOutReverse
    '        ''' <summary>脈衝輸出模式</summary>
    '        ''' <remarks></remarks>
    '        Public PulseOutMode As enmPulseOutMode
    '#Region "Alarm"
    '        ''' <summary>異警致能</summary>
    '        ''' <remarks></remarks>
    '        Public AlarmEnable As enmAlarmEnable
    '        ''' <summary>異警邏輯</summary>
    '        ''' <remarks></remarks>
    '        Public AlarmLogic As enmAlarmLogic
    '        ''' <summary>異警停止模式</summary>
    '        ''' <remarks></remarks>
    '        Public AlarmStopMode As enmAlarmStopMode
    '#End Region
    '        ''' <summary>背隙修正</summary>
    '        ''' <remarks></remarks>
    '        Public BacklashEnable As enmBacklashEnable
    '        ''' <summary>原點邏輯</summary>
    '        ''' <remarks></remarks>
    '        Public OrgLogic As enmOrgLogic
    '        ''' <summary>
    '        ''' Z相邏輯
    '        ''' </summary>
    '        ''' <remarks></remarks>
    '        Public EZLogic As enmEZLogic
    '        ''' <summary>復歸重置</summary>
    '        ''' <remarks></remarks>
    '        Public HomeReset As enmHomeReset
    '        ''' <summary>到位訊號致能</summary>
    '        ''' <remarks></remarks>
    '        Public INPEnable As enmINPEnable
    '        ''' <summary>
    '        ''' 到位訊號邏輯
    '        ''' </summary>
    '        ''' <remarks></remarks>
    '        Public INPLogic As enmINPLogic
    '        ''' <summary>
    '        ''' 栓鎖致能
    '        ''' </summary>
    '        ''' <remarks></remarks>
    '        Public LatchEnable As enmLatchEnable
    '        ''' <summary>
    '        ''' 栓鎖邏輯
    '        ''' </summary>
    '        ''' <remarks></remarks>
    '        Public LatchLogic As enmLatchPLogic

    '        Public TriggerStopEnable1 As enmTriggerStopEnable
    '        Public TriggerStopEnable2 As enmTriggerStopEnable
    '        Public TriggerStopEnable3 As enmTriggerStopEnable
    '        Public TriggerStopEnable4 As enmTriggerStopEnable
    '        Public TriggerStopEnable5 As enmTriggerStopEnable
    '        Public TriggerStopMode1 As enmTriggerStopMode
    '        Public TriggerStopMode2 As enmTriggerStopMode
    '        Public TriggerStopMode3 As enmTriggerStopMode
    '        Public TriggerStopMode4 As enmTriggerStopMode
    '        Public TriggerStopMode5 As enmTriggerStopMode
    '        Public TriggerStopLogic1 As enmTriggerStopLogic
    '        Public TriggerStopLogic2 As enmTriggerStopLogic
    '        Public TriggerStopLogic3 As enmTriggerStopLogic
    '        Public TriggerStopLogic4 As enmTriggerStopLogic
    '        Public TriggerStopLogic5 As enmTriggerStopLogic

    '        Public ErcEnable As enmErcEnableMode
    '        Public ErcLogic As enmErcLogic

    '        Public ExternalDriveAxis As enmExternalDrive
    '        Public ExternalDriveEnable As enmExternalDriveEnable
    '        Public ExternalDrivePulseInMode As enmExternalDrivePulseInMode

    '        ''' <summary>編碼器是否存在</summary>
    '        ''' <remarks>如果不存在,則GetPositionValue讀取命令位置</remarks>
    '        Public IsEncoderExist As Boolean
    '        ''' <summary>馬達類型</summary>
    '        ''' <remarks></remarks>
    '        Public MotorType As eMotorType
    '        ''' <summary>介面按鍵方向</summary>
    '        ''' <remarks></remarks>
    '        Public ButtonDirection As eDirection
    '    End Structure

    ' ''' <summary>軸對應卡片參數</summary>
    ' ''' <remarks></remarks>
    'Public Structure sCardParameter
    '    ''' <summary>卡片型號</summary>
    '    ''' <remarks></remarks>
    '    Public CardType As enmMotionCardType
    '    ''' <summary>對Motion註冊票券(卡號)</summary>
    '    ''' <remarks></remarks>
    '    Public ItemNo As Integer

    '    ''' <summary>軸型號</summary>
    '    ''' <remarks></remarks>
    '    Public AxisType As enmAxisType
    '    ''' <summary>卡片軸號</summary>
    '    ''' <remarks></remarks>
    '    Public AxisNo As Integer
    '    ''' <summary>所屬群號</summary>
    '    ''' <remarks></remarks>
    '    Public GroupNo As Integer
    'End Structure


End Module

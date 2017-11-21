Imports ProjectCore

''' <summary>觸發電路板</summary>
''' <remarks></remarks>
Public Module MCommonTriggerBoard

#Region "Enum 、Structure"

    ''' <summary>[點膠圖樣(給Trigger Board)]</summary>
    ''' <remarks></remarks>
    Public Enum eTriggerBoardPathType
        ''' <summary>[點]</summary>
        ''' <remarks></remarks>
        Dot = 0
        ''' <summary>[線]</summary>
        ''' <remarks></remarks>
        Line = 1
        ''' <summary>[弧]</summary>
        ''' <remarks></remarks>
        Arc = 2
    End Enum

    ''' <summary>[圓弧方向]</summary>
    ''' <remarks></remarks>
    Public Enum eArcDir
        ''' <summary>[順時針]</summary>
        ''' <remarks></remarks>
        CW = 0
        ''' <summary>[逆時針]</summary>
        ''' <remarks></remarks>
        CCW = 1
    End Enum

    Public Structure sReceiveStatus
        ''' <summary>[狀態]</summary>
        ''' <remarks></remarks>
        Public Status As Boolean
        ''' <summary>[字串]</summary>
        ''' <remarks></remarks>
        Public STR As String
        ''' <summary>[結果(處理完的資料內容)]</summary>
        ''' <remarks></remarks>
        Public Value As String
    End Structure

    ''' <summary>[落點估測參數(供Trigger board做落點分析用)(J Command)]</summary>
    ''' <remarks></remarks>
    Public Structure sTriggerJCmdParam
        ''' <summary>[Jet Time(us)]</summary>
        ''' <remarks></remarks>
        Public JetTime As Decimal
        ''' <summary>[Tolerance(um)]</summary>
        ''' <remarks></remarks>
        Public Tolerance As Decimal
        ''' <summary>[Measure Length(um)]</summary>
        ''' <remarks></remarks>
        Public MeasureLength As Decimal
        ''' <summary>[Measure Pitch(um)]</summary>
        ''' <remarks></remarks>
        Public MeasurePitch As Decimal
    End Structure

    ''' <summary>[路徑資料與閥體參數 (J Command)]</summary>
    ''' <remarks></remarks>
    Public Structure sTriggerJCmdStep
        ''' <summary>[Glue Pressure (*1000)]</summary>
        ''' <remarks></remarks>
        Public GluePressure As Decimal
        ''' <summary>[Jet Pressure(*1000)]</summary>
        ''' <remarks></remarks>
        Public JetPressure As Decimal
        ''' <summary>[Pulse Time (us)]</summary>
        ''' <remarks></remarks>
        Public PulseTime As Decimal
        ''' <summary>[Falling Velocity(0~100%)]</summary>
        ''' <remarks></remarks>
        Public FallingVelocity As Decimal
        ''' <summary>[Stroke(0~100%)]</summary>
        ''' <remarks></remarks>
        Public Stroke As Decimal
        ''' <summary>[圖形樣式]</summary>
        ''' <remarks></remarks>
        Public Path As eTriggerBoardPathType
        ''' <summary>[起點座標X] </summary>
        ''' <remarks></remarks>
        Public StartPosX As Decimal
        ''' <summary>[起點座標Y] </summary>
        ''' <remarks></remarks>
        Public StartPosY As Decimal
        ''' <summary>[起點座標Z] </summary>
        ''' <remarks></remarks>
        Public StartPosZ As Decimal
        ''' <summary>[終點座標X] </summary>
        ''' <remarks></remarks>
        Public EndPosX As Decimal
        ''' <summary>[終點座標Y] </summary>
        ''' <remarks></remarks>
        Public EndPosY As Decimal
        ''' <summary>[終點座標Z] </summary>
        ''' <remarks></remarks>
        Public EndPosZ As Decimal
        ''' <summary>[圓心座標X] </summary>
        ''' <remarks></remarks>
        Public CenPosX As Decimal
        ''' <summary>[圓心座標Y] </summary>
        ''' <remarks></remarks>
        Public CenPosY As Decimal
        ''' <summary>[圓心座標Z] </summary>
        ''' <remarks></remarks>
        Public CenPosZ As Decimal
        ''' <summary>[方向]</summary>
        ''' <remarks></remarks>
        Public Dir As eArcDir
        ''' <summary>[Dot數量] </summary>
        ''' <remarks></remarks>
        Public DotCounts As Decimal
        ''' <summary>[Open Time(us)]</summary>
        ''' <remarks></remarks>
        Public OpenTime As Integer
        ''' <summary>[Close Time(us)]</summary>
        ''' <remarks></remarks>
        Public CloseTime As Integer
        ''' <summary>[Close Voltage]</summary>
        ''' <remarks></remarks>
        Public CloseVoltage As Decimal
    End Structure
    ''' <summary>[落點估測參數(供Trigger board做落點分析用)(H Command)]</summary>
    ''' <remarks></remarks>
    Public Structure sTriggerLaserCmdParam
        ''' <summary>[取像總數]</summary>
        ''' <remarks></remarks>
        Public TotalPointCounts As Decimal
        ''' <summary>[助跑座標X(um)]</summary>
        ''' <remarks></remarks>
        Public ApproachPosX As Decimal
        ''' <summary>[助跑座標Y(um)]</summary>
        ''' <remarks></remarks>
        Public ApproachPosY As Decimal
        ''' <summary>[延遲時間(ms)]</summary>
        ''' <remarks></remarks>
        Public DelayTime As Decimal
    End Structure

    ''' <summary>[路徑資料與視覺參數(H Command)]</summary>
    ''' <remarks></remarks>
    Public Structure sTriggerLaserCmdStep
        ''' <summary>[圖形樣式]</summary>
        ''' <remarks></remarks>
        Public Path As eTriggerBoardPathType
        ''' <summary>[方向]</summary>
        ''' <remarks></remarks>
        Public Dir As eArcDir
        ''' <summary>[Stage移動速度]</summary>
        ''' <remarks></remarks>
        Public Velocity As Decimal
        ''' <summary>[取像數量] </summary>
        ''' <remarks></remarks>
        Public PointCounts As Decimal
        ''' <summary>[起點座標X] </summary>
        ''' <remarks></remarks>
        Public StartPosX As Decimal
        ''' <summary>[起點座標Y] </summary>
        ''' <remarks></remarks>
        Public StartPosY As Decimal
        ''' <summary>[終點座標X] </summary>
        ''' <remarks></remarks>
        Public EndPosX As Decimal
        ''' <summary>[終點座標Y] </summary>
        ''' <remarks></remarks>
        Public EndPosY As Decimal
        ''' <summary>[圓心座標X] </summary>
        ''' <remarks></remarks>
        Public CenPosX As Decimal
        ''' <summary>[圓心座標Y] </summary>
        ''' <remarks></remarks>
        Public CenPosY As Decimal
        ''' <summary>[隔多久的時間才會走到下一條線段]</summary>
        ''' <remarks></remarks>
        Public PathWaitTime As Decimal
    End Structure
    ''' <summary>[落點估測參數(供Trigger board做落點分析用)(L Command)]</summary>
    ''' <remarks></remarks>
    Public Structure sTriggerVisionCmdParam
        ''' <summary>[取像總數]</summary>
        ''' <remarks></remarks>
        Public TotalPointCounts As Decimal
        ''' <summary>[助跑座標X(um)]</summary>
        ''' <remarks></remarks>
        Public ApproachPosX As Decimal
        ''' <summary>[助跑座標Y(um)]</summary>
        ''' <remarks></remarks>
        Public ApproachPosY As Decimal
        ''' <summary>[延遲時間(ms)]</summary>
        ''' <remarks></remarks>
        Public DelayTime As Decimal
    End Structure

    ''' <summary>[路徑資料與視覺參數(L Command)]</summary>
    ''' <remarks></remarks>
    Public Structure sTriggerVisionCmdStep
        ''' <summary>[圖形樣式]</summary>
        ''' <remarks></remarks>
        Public Path As eTriggerBoardPathType
        ''' <summary>[方向]</summary>
        ''' <remarks></remarks>
        Public Dir As eArcDir
        ''' <summary>[Stage移動速度]</summary>
        ''' <remarks></remarks>
        Public Velocity As Decimal
        ''' <summary>[取像數量] </summary>
        ''' <remarks></remarks>
        Public PointCounts As Decimal
        ''' <summary>[起點座標X] </summary>
        ''' <remarks></remarks>
        Public StartPosX As Decimal
        ''' <summary>[起點座標Y] </summary>
        ''' <remarks></remarks>
        Public StartPosY As Decimal
        ''' <summary>[終點座標X] </summary>
        ''' <remarks></remarks>
        Public EndPosX As Decimal
        ''' <summary>[終點座標Y] </summary>
        ''' <remarks></remarks>
        Public EndPosY As Decimal
        ''' <summary>[圓心座標X] </summary>
        ''' <remarks></remarks>
        Public CenPosX As Decimal
        ''' <summary>[圓心座標Y] </summary>
        ''' <remarks></remarks>
        Public CenPosY As Decimal
        ''' <summary>[隔多久的時間才會走到下一條線段]</summary>
        ''' <remarks></remarks>
        Public PathWaitTime As Decimal
    End Structure

    ''' <summary>[落點估測參數(供Trigger board做落點分析用)(F Command)]</summary>
    ''' <remarks></remarks>
    Public Structure sTriggerFCmdParam
        ''' <summary>[打點總數]</summary>
        ''' <remarks></remarks>
        Public TotalDotCounts As Decimal
        ''' <summary>[助跑座標X(um)]</summary>
        ''' <remarks></remarks>
        Public ApproachPosX As Decimal
        ''' <summary>[助跑座標Y(um)]</summary>
        ''' <remarks></remarks>
        Public ApproachPosY As Decimal
        ' ''' <summary>[隔多久的時間才會走到下一條線段(us)]</summary>
        ' ''' <remarks></remarks>
        'Public WaitTime As Decimal
    End Structure

    ''' <summary>[路徑資料與閥體參數(F Command)]</summary>
    ''' <remarks></remarks>
    Public Structure sTriggerFCmdStep
        ''' <summary>[圖形樣式]</summary>
        ''' <remarks></remarks>
        Public Path As eTriggerBoardPathType
        ''' <summary>[方向]</summary>
        ''' <remarks></remarks>
        Public Dir As eArcDir
        ''' <summary>[Stage移動速度]</summary>
        ''' <remarks></remarks>
        Public Velocity As Decimal
        ''' <summary>[Dot數量] </summary>
        ''' <remarks></remarks>
        Public DotCounts As Decimal
        ''' <summary>[起點座標X] </summary>
        ''' <remarks></remarks>
        Public StartPosX As Decimal
        ''' <summary>[起點座標Y] </summary>
        ''' <remarks></remarks>
        Public StartPosY As Decimal
        ''' <summary>[終點座標X] </summary>
        ''' <remarks></remarks>
        Public EndPosX As Decimal
        ''' <summary>[終點座標Y] </summary>
        ''' <remarks></remarks>
        Public EndPosY As Decimal
        ''' <summary>[圓心座標X] </summary>
        ''' <remarks></remarks>
        Public CenPosX As Decimal
        ''' <summary>[圓心座標Y] </summary>
        ''' <remarks></remarks>
        Public CenPosY As Decimal
        ''' <summary>[隔多久的時間才會走到下一條線段]</summary>
        ''' <remarks></remarks>
        Public PathWaitTime As Decimal
    End Structure

    ''' <summary>[閥體參數(供Trigger board做落點分析用)]</summary>
    ''' <remarks></remarks>
    Public Structure sTriggerGCmdParam
        ''' <summary>[Head No]</summary>
        ''' <remarks></remarks>
        Public HeadNo As Integer
        ''' <summary>[Pulse Time (us)]</summary>
        ''' <remarks></remarks>
        Public PulseTime As Integer
        ''' <summary>[Jet Time(us)]</summary>
        ''' <remarks></remarks>
        Public JetTime As Integer
        ''' <summary>[Stroke(0~100%)]</summary>
        ''' <remarks></remarks>
        Public Stroke As Integer
        ''' <summary>[Glue Pressure (*1000)]</summary>
        ''' <remarks></remarks>
        Public GluePressure As Integer
        ''' <summary>[Tolerance(um)]</summary>
        ''' <remarks></remarks>
        Public Tolerance As Integer
        ''' <summary>[Measure Length(um)]</summary>
        ''' <remarks></remarks>
        Public MeasureLength As Integer
        ''' <summary>[Measure Pitch(um)]</summary>
        ''' <remarks></remarks>
        Public MeasurePitch As Integer
        ''' <summary>[Measure Counts (次)]</summary>
        ''' <remarks></remarks>
        Public MeasureCounts As Integer
        ''' <summary>[Jet Pressure(*1000)]</summary>
        ''' <remarks></remarks>
        Public JetPressure As Integer
        ''' <summary>[Open Time(us)]</summary>
        ''' <remarks></remarks>
        Public OpenTime As Integer
        ''' <summary>[Close Time(us)]</summary>
        ''' <remarks></remarks>
        Public CloseTime As Integer
        ''' <summary>[Close Voltage(V)]</summary>
        ''' <remarks></remarks>
        Public CloseVoltage As Integer
        ''' <summary>[Cycle Time(us)]</summary>
        ''' <remarks></remarks>
        Public CycleTime As Integer
    End Structure

    ''' <summary>[閥體參數(供Trigger board控制Valve)]</summary>
    ''' <remarks></remarks>
    Public Structure sTriggerTPCmdParam
        ''' <summary>[Cycle Time(us)]</summary>
        ''' <remarks></remarks>
        Public CycleTime As Integer
        ''' <summary>[Pitch(um)]</summary>
        ''' <remarks></remarks>
        Public Pitch As Integer
        ''' <summary>[Dot Counts(ea)]</summary>
        ''' <remarks></remarks>
        Public DotCounts As Integer
        ''' <summary>[Glue Pressure (*1000)]</summary>
        ''' <remarks></remarks>
        Public GluePressure As Integer
        ''' <summary>[Jet Pressure (*1000)]</summary>
        ''' <remarks></remarks>
        Public JetPressure As Integer
        ''' <summary>[Pulse Time (us)]</summary>
        ''' <remarks></remarks>
        Public PulseTime As Integer
        ''' <summary>[Open Time(us)]</summary>
        ''' <remarks></remarks>
        Public OpenTime As Integer
        ''' <summary>[Close Time(us)]</summary>
        ''' <remarks></remarks>
        Public CloseTime As Integer
        ''' <summary>[Close Voltage(V)]</summary>
        ''' <remarks></remarks>
        Public CloseVoltage As Integer
        ''' <summary>[Stroke(0~100%)]</summary>
        ''' <remarks></remarks>
        Public Stroke As Integer
    End Structure

    Public Enum enmTriggerComdEndType
        ''' <summary>[非最後線段]</summary>
        ''' <remarks></remarks>
        NonEndLine = 0
        ''' <summary>[此為最後線段] </summary>
        ''' <remarks></remarks>
        EedLine = 1
    End Enum

    ''' <summary>[Trigger Board Dispensing Type]</summary>
    ''' <remarks></remarks>
    Public Enum enmTriggerDispType
        ''' <summary>[J]</summary>
        ''' <remarks></remarks>
        JetParamRecipe = 0
        ''' <summary>[I]</summary>
        ''' <remarks></remarks>
        NeedleJetParamRecipe = 1
        ''' <summary>[A]</summary>
        ''' <remarks></remarks>
        AugerParamRecipe = 2
        ''' <summary>[T(Cycle Time)]</summary>
        ''' <remarks></remarks>
        CycleRecipe = 3
        ''' <summary>[P(Pitch)]</summary>
        ''' <remarks></remarks>
        PitchRecipe = 4
        ''' <summary>[F(套用在相同的Valve Parameter)]</summary>
        ''' <remarks></remarks>
        JetRecipe = 5
        ''' <summary>[Vision Recipe(L Command)]</summary>
        ''' <remarks></remarks>
        VisionRecipe = 6
    End Enum
#End Region

    ''' <summary>[觸發控制器集合]</summary>
    ''' <remarks></remarks>
    Public gTriggerBoard As New CTriggerBoardCollection

    ''' <summary>[觸發版版本]</summary>
    ''' <remarks></remarks>
    Public gTriggerBoardVersion(enmTriggerBoard.Max) As String

    ''' <summary>[通訊異常後 允取再從送幾次]</summary>
    ''' <remarks></remarks>
    Public Const gTriggerCmdMaxFailCounts As Integer = 3


    ''' <summary>[觸發板初始化]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Initialize_TriggerBoard() As Boolean
        gTriggerBoard.Load(Application.StartupPath & "\System\" & MachineName & "\CardTriggerBoard.ini")
        If gTriggerBoard.Initial(gTriggerBoard.TBConnectionParameter) = False Then
            gSyslog.Save("Initialized Trigger Board Failed!", , eMessageLevel.Error)
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>[關閉觸發板]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Close_TriggerBoard() As Boolean
        Return gTriggerBoard.Close()
    End Function


End Module

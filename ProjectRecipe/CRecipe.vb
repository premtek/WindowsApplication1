Imports ProjectCore

''' <summary>
''' 移動的軸向
''' </summary>
''' <remarks></remarks>
Public Enum enmMoveAxis
    eXAxis = 0
    eY1Axis = 1
End Enum

Public Enum enmJettingTriggerCommandEndType
    ''' <summary>[還有下一個同方向之線段] </summary>
    ''' <remarks></remarks>
    NextLineInParallel = 0
    ''' <summary>[下一線段會改變方向] </summary>
    ''' <remarks></remarks>
    NextLineChangeDirection = 1
    ''' <summary>[此為最後線段] </summary>
    ''' <remarks></remarks>
    EedLine = 2
End Enum

''' <summary>多軸同動運行模式</summary>
''' <remarks></remarks>
Public Enum eRunMode
    ''' <summary>当MoveMode为非交接模式。在这种模式中，每个路径都包括加速和减速的整个过程。这种不支持速度前瞻功能，因此应禁用CFG_GpSFEnable。</summary>
    ''' <remarks></remarks>
    BufferMode = 0
    ''' <summary>当MoveMode为交接模式，且 CFG_GpBldTime 的值大于 0。这种不支持 S 型速度曲线。当通过 CFG_SFEnable 启用速度前瞻功能时，由于路径运动中使用的所有速度参数为群组速度设置，因此无需使用参数FL 和 FH，所有路径支持相同的驱动速度。比如，CFG_SFEnable = Disable，BlendingTime>0。</summary>
    ''' <remarks></remarks>
    BlendingMode = 1
    ''' <summary>当 MoveMode 为交接模式，且 CFG_GpBldTime 的值为 0。当通过 CFG_SFEnable 启用速度前瞻功能时，由于路径运动中使用的所有速度参数为群组速度设置，因此无需使用参数 FL 和 FH，所有路径支持相同的驱动速度。</summary>
    ''' <remarks></remarks>
    FlyMode = 2
End Enum

''' <summary>
''' 產品類型
''' </summary>
''' <remarks></remarks>
Public Enum enmProductType
    ''' <summary>預設</summary>
    ''' <remarks></remarks>
    [Default] = 1
    ''' <summary>晶圓</summary>
    ''' <remarks></remarks>
    Wafer = 1
    ''' <summary>面狀封裝</summary>
    ''' <remarks></remarks>
    Panel = 2
    ''' <summary>條狀封裝</summary>
    ''' <remarks></remarks>
    Strip = 3
End Enum

''' <summary>元件定位方式</summary>
''' <remarks></remarks>
Public Enum enmAlignType
    ''' <summary>單點定位</summary>
    ''' <remarks></remarks>
    DevicePos1 = 0
    ''' <summary>兩點定位</summary>
    ''' <remarks></remarks>
    DevicePos2 = 1
    ''' <summary>三點定位</summary>
    ''' <remarks></remarks>
    DevicePos3 = 2
    ''' <summary>[Max]</summary>
    ''' <remarks></remarks>
    Max = DevicePos3
End Enum


''' <summary>[點膠模式(Trigger Board路徑估測方式)]</summary>
''' <remarks></remarks>
Public Enum eDispHistoryModel
    ''' <summary>[沒有限定]</summary>
    ''' <remarks></remarks>
    None = -1
    ''' <summary>[非 History Model]</summary>
    ''' <remarks></remarks>
    DispNonHistory = 0
    ''' <summary>[History Model]</summary>
    ''' <remarks></remarks>
    DispHistory = 1
    ''' <summary>[Max]</summary>
    ''' <remarks></remarks>
    Max = DispHistory
End Enum

''' <summary>[CCD定位方式]</summary>
''' <remarks></remarks>
Public Enum eCCDFixModel
    ''' <summary>[非飛拍模式(一次拍一顆-->拍、停)]</summary>
    ''' <remarks></remarks>
    NonOnFly = 0
    ''' <summary>[飛拍模式(一次拍多顆-->拍、拍、拍、停)]</summary>
    ''' <remarks></remarks>
    OnFly = 1
End Enum
''' <summary>[測高方式]</summary>
''' <remarks></remarks>
Public Enum eHeightModel
    ''' <summary>[接觸式]</summary>
    ''' <remarks></remarks>
    Contact = 0
    ''' <summary>[Laser]</summary>
    ''' <remarks></remarks>
    Laser_NonOnFly = 1
    ''' <summary>[Laser飛拍模式]</summary>
    ''' <remarks></remarks>
    Laser_OnFly = 2
End Enum

''' <summary>[時間控制型點膠(時間估算邏輯)]</summary>
''' <remarks></remarks>
Public Enum eDispTimeModel
    ''' <summary>[取最大值]</summary>
    ''' <remarks></remarks>
    Max = 0
    ''' <summary>[取最小值]</summary>
    ''' <remarks></remarks>
    Min = 1
End Enum

Public Structure sLeadAngle
    ''' <summary>[角度]</summary>
    ''' <remarks></remarks>
    Public Degress As Decimal
    ''' <summary>[長度]</summary>
    ''' <remarks></remarks>
    Public Distance As Decimal
End Structure
''' <summary>[Laser Run 模式]</summary>
''' <remarks></remarks>
Public Enum eLaserRunModel
    ''' <summary>[沒有限定]</summary>
    ''' <remarks></remarks>
    None = -1
    ''' <summary>[Array]</summary>
    ''' <remarks></remarks>
    Array = 0
    ''' <summary>[NonArray]</summary>
    ''' <remarks></remarks>
    NonArray = 1
End Enum

''' <summary>接續生產方式</summary>
''' <remarks></remarks>
Public Enum enmFollowMode
    ''' <summary>重新開始(整片放棄,不接續)</summary>
    ''' <remarks></remarks>
    Restart = 0
    ''' <summary>下一步驟(同一Round,接著生產)</summary>
    ''' <remarks></remarks>
    NextStep = 1
    ''' <summary>下一輪(本Round放棄,從下一Round接著生產)</summary>
    ''' <remarks></remarks>
    NextRound = 2
    ''' <summary>下一個Pattern(本Round經過的通通放棄,從下一個新的開始)</summary>
    ''' <remarks></remarks>
    NextPattern = 3
End Enum

''' <summary>產品配方設定檔Root</summary>
''' <remarks></remarks>
Public Class CRecipe

    ''' <summary>初始化給定參數 Stage數量
    ''' </summary>
    ''' <remarks></remarks>
    Dim mStageCount As Integer
    ''' <summary>初始化給定參數 一個Stage有幾個閥
    ''' </summary>
    ''' <remarks></remarks>
    Dim mStageValveCount As Integer
    Public Sub New(ByVal stageCount As Integer, ByVal stageValveCount As Integer)
        MyBase.New()
        mStageCount = stageCount
        mStageValveCount = stageValveCount
        For i = enmStage.No1 To stageCount - 1
            Me.Node(i) = New Dictionary(Of String, CRecipeNode)
            Me.DispenseTraversal(i) = New List(Of String)
            Me.ScanTraversal(i) = New List(Of String)
            Me.CCDFixTraversal(i) = New List(Of String)
            Me.LaserTraversal(i) = New List(Of String)
            Me.NodeConnectList(i) = New List(Of Dictionary(Of String, String))
            Me.NodeToMap(i) = New List(Of String)
        Next
        For stageNo As Integer = 0 To stageCount - 1
            StageParts(stageNo) = New CStageParts
        Next

        '[說明]:先給預設值,不用LoadRecipe
        For i = 0 To stageCount - 1
            For j = 0 To stageValveCount - 1
                Me.StageParts(i).ValveName(j) = "Default"
                Me.StageParts(i).PurgeName(j) = "Default"
                Me.StageParts(i).FlowRateName(j) = "Default"
                Me.StageParts(i).PasteName(j) = "Default"
            Next
        Next

    End Sub




    ''' <summary>啟用編輯(不存檔)</summary>
    ''' <remarks></remarks>
    Public Editable As Boolean
    ''' <summary>
    ''' [雷射測高取值模式]
    ''' </summary>
    ''' <remarks></remarks>
    Public LaserMode As enmLaserMode = enmLaserMode.AverageHigh
    ''' <summary>[測高取值模式]</summary>
    ''' <remarks></remarks>
    Public LaserFixMode As eHeightModel
    ''' <summary>搜尋方式</summary>
    ''' <remarks></remarks>
    Public SearchType As enmSearchType = enmSearchType.Y_Snake
    ''' <summary>[點膠模式(Trigger Board路徑估測方式)]</summary>
    ''' <remarks></remarks>
    Public DispHistory As eDispHistoryModel
    ''' <summary>[時間控制型點膠(只有在多個Round時間控制型態下才有作用)]</summary>
    ''' <remarks></remarks>
    Public DispTimeModel As eDispTimeModel
    ''' <summary>[導角資訊]</summary>
    ''' <remarks></remarks>
    Public LeadAngle As sLeadAngle
    ''' <summary>[產品類型]</summary>
    ''' <remarks></remarks>
    Public ProductType As enmProductType
    ''' <summary>[路徑的膠重控制模式(Dot、膠重)]</summary>
    ''' <remarks></remarks>
    Public RunType As eWeightControlType = eWeightControlType.Dots
    ''' <summary>[忽略CCD運算結果]</summary>
    ''' <remarks></remarks>
    Public BypassCCDResult As Boolean
    ''' <summary>[忽略Laser運算結果]</summary>
    ''' <remarks></remarks>
    Public BypassLaserResult As Boolean
    ''' <summary>[影像定位方式]</summary>
    ''' <remarks></remarks>
    Public CCDFixModel As eCCDFixModel
    ''' <summary>[飛拍速度]</summary>
    ''' <remarks></remarks>
    Public CCDOnFlySpeed As Double
    ''' <summary>[飛拍觸發延遲時間(ms)]</summary>
    ''' <remarks></remarks>
    Public CCDOnFlyDelayTime As Double
    ''' <summary>[Align定位NG是否停止(中斷後續的流程)]</summary>
    ''' <remarks></remarks>
    Public IsStopAlignNG As Boolean
    ''' <summary>[整個流程跑一次就結束(這是暫時速解的方法，後續再補手動流程改這個問題)]</summary>
    ''' <remarks></remarks>
    Public IsJustOneRun As Boolean
    ''' <summary>[Rerun流程跑一次就結束]</summary>
    ''' <remarks></remarks>
    Public IsRerunRun As Boolean
    ''' <summary>[角度補償功能關閉(忽略旋轉修正)] </summary>
    ''' <remarks></remarks>
    Public BypassRotationCorrection As Boolean
    '20170308_Toby
    ''' <summary>啟用Step編輯_是否Alignable pass</summary>
    ''' <remarks></remarks>
    Public Alignable As Boolean
    '20170215
    ''' <summary>[Conveyor speed]</summary>
    ''' <remarks></remarks>
    Public ConveyorSpeed As Double
    ''' <summary>[laser run 模式]</summary>
    ''' <remarks></remarks>
    Public LaserRunMode As eLaserRunModel


#Region "製程參數"


    ' ''' <summary>[膠管壓力]</summary>
    ' ''' <remarks></remarks>
    'Public SyringPressure(enmValve.Max) As Decimal
    ' ''' <summary>[光源亮度]</summary>
    ' ''' <remarks></remarks>
    'Public LightValue As Integer

#End Region

    ''' <summary>[Recipe Name] </summary>
    ''' <remarks></remarks>
    Public strName As String
    ''' <summary>[Recipe完整路徑檔名] </summary>
    ''' <remarks></remarks>
    Public strFileName As String
    ''' <summary>[Stage下Valve的配置]</summary>
    ''' <remarks></remarks>
    Public StageParts(enmStage.Max) As CStageParts
    ''' <summary>溫控檔名</summary>
    ''' <remarks></remarks>
    Public TempName As String
    ''' <summary>平台起始節點名稱</summary>
    ''' <remarks></remarks>
    Public StageNodeID(enmStage.Max) As String
    ''' <summary>Pattern總清單</summary>
    ''' <remarks></remarks>
    Public PatternName As New List(Of String)
    ''' <summary>Pattern總數</summary>
    ''' <remarks></remarks>
    Public PatternCount As Integer
    ''' <summary>[Recipe內的Patern清單(不同節點可能是同樣的名稱)]</summary>
    ''' <remarks></remarks>
    Public Pattern As New Dictionary(Of String, CRecipePattern)
    ''' <summary>[Recipe內的Node清單]</summary>
    ''' <remarks></remarks>
    Public Node(enmStage.Max) As Dictionary(Of String, CRecipeNode)
    ''' <summary>[Node總數]</summary>
    ''' <remarks></remarks>
    Public NodeCount(enmStage.Max) As Integer
    ''' <summary>[Node對應到Map]</summary>
    ''' <remarks></remarks>
    Public NodeToMap(enmStage.Max) As List(Of String)
    ''' <summary>[CCD和Laser工作清單]</summary>
    ''' <remarks></remarks>
    Public ScanTraversal(enmStage.Max) As List(Of String)
    ''' <summary>[CCD Fix工作清單]</summary>
    ''' <remarks></remarks>
    Public CCDFixTraversal(enmStage.Max) As List(Of String)
    ''' <summary>[Laser測高工作清單]</summary>
    ''' <remarks></remarks>
    Public LaserTraversal(enmStage.Max) As List(Of String)
    ''' <summary>[點膠工作清單]</summary>
    ''' <remarks></remarks>
    Public DispenseTraversal(enmStage.Max) As List(Of String)

    Public NodeConnectList(enmStage.Max) As List(Of Dictionary(Of String, String))
    ''' <summary>溫度參數設定</summary>
    ''' <remarks></remarks>
    Public TempParam(enmTemp.Count - 1) As sTemperatureConfig 'Soni + 2015.09.06
    ''' <summary>
    ''' [Notch方向]
    ''' </summary>
    ''' <remarks></remarks>
    Public NotchDir(1) As Integer  'TODO 待Asa Enmtype
    ''' <summary>
    ''' [Bin Map 資料填入]
    ''' </summary>
    ''' <remarks></remarks>
    Public sBinData As New Dictionary(Of String, BinMappingData)
    ''' <summary>
    ''' [Bin數量]
    ''' </summary>
    ''' <remarks></remarks>
    Public BinNumber As Integer

    ''' <summary>天平設定</summary>
    ''' <remarks></remarks>
    Public Scale As New CScaleParameter

    ''' <summary>群組運行模式</summary>
    ''' <remarks></remarks>
    Public GPSetRunMode As eRunMode = eRunMode.BufferMode

    ''' <summary>接續生產模式</summary>
    ''' <remarks></remarks>
    Public FollowMode As enmFollowMode = enmFollowMode.Restart


    '20161005
    ''' <summary>ProductType TotalNum</summary>
    ''' <remarks></remarks>
    Public dblProductTypeTotalNum As Decimal
    ''' <summary>[ProductType Name]</summary>
    ''' <remarks></remarks>
    Public ProductTypeName(20) As String

    ' ''' <summary>
    ' ''' 點膠溫度
    ' ''' </summary>
    'Public WorkTemp As UInteger

    ' ''' <summary>
    ' ''' Loader溫度
    ' ''' </summary>
    'Public LoaderTemp As UInteger

    ' ''' <summary>
    ' ''' Unloader溫度
    ' ''' </summary>
    'Public UnloaderTemp As UInteger

    ' ''' <summary>
    ' ''' A機Hot Plate
    ' ''' </summary>
    'Public A_HotPlate(5) As Boolean

    ' ''' <summary>
    ' ''' B機Hot Plate
    ' ''' </summary>
    'Public B_HotPlate(5) As Boolean

    ''' <summary>Chuck對應方式</summary>
    ''' <remarks></remarks>
    Public ChuckMapType As enmSearchType

    ''' <summary>
    ''' 讀取Recipe
    ''' </summary>
    ''' <param name="FileName">檔名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadRecipe(ByVal fileName As String) As Boolean
        Dim strSection As String

        '20170214_Toby
        Dim LASERMODE_s As String

        strFileName = fileName

        Scale.Load(strFileName)

        strSection = "RecipeStep"
        With Me
            .strFileName = strFileName
            .strName = System.IO.Path.GetFileName(strFileName)
            .LeadAngle.Degress = Val(ReadIniString(strSection, "LeadAngleDegress", strFileName, 90))        '[90度]
            .LeadAngle.Distance = Val(ReadIniString(strSection, "LeadAngleDistance", strFileName, 1))       '[1mm]
            .GPSetRunMode = CInt(ReadIniString(strSection, "GPSetRunMode", strFileName, CInt(eRunMode.BlendingMode)))
            .GPSetRunMode = eRunMode.BufferMode '模式限定
            .SearchType = CInt(ReadIniString(strSection, "SearchType", strFileName, CInt(enmSearchType.Y_Snake)))         'Fenix+ 2016/02/23
            .LaserMode = CInt(ReadIniString(strSection, "LaserMode", strFileName, CInt(enmLaserMode.AverageHigh)))         'Fenix+ 2016/03/07
            .DispHistory = CInt(ReadIniString(strSection, "DispHistory", strFileName, 0))
            .DispTimeModel = CInt(ReadIniString(strSection, "DispTimeModel", strFileName, 1)) 'Soni / 2017.08.25 修改為預設最小值
            .RunType = CInt(ReadIniString(strSection, "RunType", strFileName, CInt(eWeightControlType.Dots)))
            .BypassCCDResult = CBool(ReadIniString(strSection, "BypassCCDResult", strFileName, False))
            .BypassLaserResult = CBool(ReadIniString(strSection, "BypassLaserResult", strFileName, False))
            .BypassRotationCorrection = CBool(ReadIniString(strSection, "BypassRotationCorrection", strFileName, False))
            .FollowMode = Val(ReadIniString(strSection, "FollowMode", fileName, CInt(enmFollowMode.Restart))) '接續生產模式
            .IsStopAlignNG = CBool(ReadIniString(strSection, "IsStopAlignNG", strFileName, False))
            .IsJustOneRun = CBool(ReadIniString(strSection, "IsJustOneRun", strFileName, False))
            '20160920
            .IsJustOneRun = CBool(ReadIniString(strSection, "JustOneRun", strFileName, False))
            .IsStopAlignNG = CBool(ReadIniString(strSection, "StopAlignNG", strFileName, False))

            '20170215
            .ConveyorSpeed = CDbl(ReadIniString(strSection, "ConveyorSpeed", fileName, "1"))

            .CCDFixModel = CInt(ReadIniString(strSection, "CCDFixModel", strFileName, CInt(eCCDFixModel.NonOnFly)))
            .CCDOnFlySpeed = CDbl(ReadIniString(strSection, "CCDOnFlySpeed", fileName, "100"))
            .CCDOnFlyDelayTime = CDbl(ReadIniString(strSection, "CCDOnFlyDelayTime", fileName, "0.30"))


            'Laser NonArray
            .LaserRunMode = CDbl(ReadIniString(strSection, "LaserRunMode", fileName, CInt(eLaserRunModel.Array)))

            '[Note]因舊版時將時間單位設為ms
            If .CCDOnFlyDelayTime > 1 Then
                .CCDOnFlyDelayTime = .CCDOnFlyDelayTime * 0.001
            End If
            '20170214
            LASERMODE_s = CStr(ReadIniString(strSection, "MeasureZMode", strFileName, ""))

            Select Case LASERMODE_s
                Case "Contact"
                    .LaserFixMode = eHeightModel.Contact
                Case "Laser_NonOnFly"
                    .LaserFixMode = eHeightModel.Laser_NonOnFly
                Case "Laser_OnFly"
                    .LaserFixMode = eHeightModel.Laser_OnFly
                    'Case "" 'Soni 2017.06.26 無資料時,提供預設值
                    '    Select Case gSSystemParameter.MeasureType
                    '        Case enmMeasureType.Contact
                    '            .LaserFixMode = eHeightModel.Contact
                    '        Case enmMeasureType.Laser
                    '            .LaserFixMode = eHeightModel.Laser_NonOnFly
                    '        Case enmMeasureType.Both
                    '            .LaserFixMode = eHeightModel.Laser_NonOnFly
                    'End Select
            End Select

            'For mstageNo As enmStage = enmStage.No1 To gSSystemParameter.StageCount - 1
            '    Me.StageParts(mstageNo) = New CStageParts
            'Next

            '--- 參數引用 ---
            '.LightValue = CInt(ReadIniString(strSection, "LightValue", strFileName, 0))  '[0:全暗]
            '.dblFindHeightPosZ = CDbl(ReadIniString(strSection, "FindHeightPosZ", strFileName, 45))
            .ChuckMapType = CInt(ReadIniString(strSection, "ChuckMapType", strFileName, 0))

            For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
                Me.NodeToMap(mStageNo).Clear()
                Dim index As Integer = 1
                While True
                    Dim node As String = ReadIniString(strSection, "NodeToMap" & (mStageNo + 1).ToString() & "_" & index.ToString(), strFileName, "")
                    If (node <> "") Then
                        Me.NodeToMap(mStageNo).Add(node)
                        index += 1
                    Else
                        Exit While
                    End If
                End While
            Next

            .BinNumber = 36
            .sBinData.Clear()
            For BinCount As Integer = 0 To .BinNumber - 1
                Dim tempBin As New BinMappingData
                If BinCount <= 9 Then
                    tempBin.BinName = ReadIniString(strSection, "BinNo" & BinCount.ToString() & "Name", strFileName, "0")
                    tempBin.Disable = CBool(ReadIniString(strSection, "BinNo" & BinCount.ToString("0") & "Disable", strFileName, "False"))
                    tempBin.PatternName = ReadIniString(strSection, "BinNo" & BinCount.ToString("0") & "PatternName", strFileName, "0")
                    .sBinData.Add(BinCount.ToString(), tempBin)
                Else
                    Dim BinName As String = Chr(55 + BinCount)
                    tempBin.BinName = ReadIniString(strSection, "BinNo" & BinName & "Name", strFileName, "0")
                    tempBin.Disable = CBool(ReadIniString(strSection, "BinNo" & BinName & "Disable", strFileName, "False"))
                    tempBin.PatternName = ReadIniString(strSection, "BinNo" & BinName & "PatternName", strFileName, "0")
                    .sBinData.Add(BinName, tempBin)
                End If
            Next

            .NotchDir(0) = CInt(ReadIniString(strSection, "NotchDirect1", strFileName, "0"))
            .NotchDir(1) = CInt(ReadIniString(strSection, "NotchDirect2", strFileName, "0"))

            LoadPattern(fileName)
            LoadNode(fileName)
            LoadStageParts(fileName)

        End With

        '[說明]:Load LodProduct 20161010
        Dim LodProductFileName As String = Application.StartupPath & "\System\" & MachineName & "\Conveyor.ini"
        strSection = "LodProduct"
        With Me
            '[說明]:預設值為三組,需新增則手動修改Conveyor.ini  LodProduct 內 Tatle 數字 Num 名稱 即可
            .dblProductTypeTotalNum = Val(ReadIniString(strSection, "Tatle", LodProductFileName, 3))
            For i = 0 To .dblProductTypeTotalNum - 1
                '[說明]:前三組為預設值
                If i = 0 Then
                    .ProductTypeName(0) = "Wafer"
                ElseIf i = 1 Then
                    .ProductTypeName(1) = "Panel"
                ElseIf i = 2 Then
                    .ProductTypeName(2) = "Strip"
                Else
                    .ProductTypeName(i) = ReadIniString(strSection, "Num" & (i + 1), LodProductFileName, "LodProductName" & (i + 1))
                End If
            Next
        End With
        Return True

ErrorHandler:  ' Error-handling routine.
        MsgBox(Err.GetException.ToString(), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        Err.Clear()
        Return False

    End Function

    ''' <summary>
    ''' 儲存Recipe
    ''' </summary>
    ''' <param name="FileName">檔名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveRecipe(ByVal FileName As String) As Boolean

        Dim strSection As String

        strFileName = FileName
        Scale.Save(strFileName)
        strSection = "WorkSize"


        strSection = "RecipeStep"
        With Me
            .strName = System.IO.Path.GetFileName(strFileName)

            Call SaveIniString(strSection, "LeadAngleDegress", .LeadAngle.Degress, strFileName)
            Call SaveIniString(strSection, "LeadAngleDistance", .LeadAngle.Distance, strFileName)
            Call SaveIniString(strSection, "GPSetRunMode", GPSetRunMode, strFileName)
            Call SaveIniString(strSection, "SearchType", SearchType, strFileName)       'Fenix+ 2016/02/23
            Call SaveIniString(strSection, "LaserMode", .LaserMode, strFileName)       'Fenix+ 2016/03/07
            Call SaveIniString(strSection, "DispHistory", .DispHistory, strFileName)
            Call SaveIniString(strSection, "DispTimeModel", .DispTimeModel, strFileName)
            Call SaveIniString(strSection, "RunType", .RunType, strFileName)
            Call SaveIniString(strSection, "BypassCCDResult", .BypassCCDResult, strFileName)
            Call SaveIniString(strSection, "BypassLaserResult", .BypassLaserResult, strFileName)
            Call SaveIniString(strSection, "BypassRotationCorrection", .BypassRotationCorrection, strFileName)
            Call SaveIniString(strSection, "FollowMode", .FollowMode, strFileName) '接續生產模式
            Call SaveIniString(strSection, "IsStopAlignNG", .IsStopAlignNG, strFileName)
            Call SaveIniString(strSection, "IsJustOneRun", .IsJustOneRun, strFileName)
            '20160920
            Call SaveIniString(strSection, "JustOneRun", .IsJustOneRun, strFileName)
            Call SaveIniString(strSection, "StopAlignNG", .IsStopAlignNG, strFileName)

            '20170215
            Call SaveIniString(strSection, "ConveyorSpeed", .ConveyorSpeed, strFileName)

            Call SaveIniString(strSection, "CCDFixModel", .CCDFixModel, strFileName)
            Call SaveIniString(strSection, "CCDOnFlySpeed", .CCDOnFlySpeed, strFileName)
            Call SaveIniString(strSection, "CCDOnFlyDelayTime", .CCDOnFlyDelayTime, strFileName)
            '20170214
            Call SaveIniString(strSection, "MeasureZMode", .LaserFixMode.ToString, strFileName)
            '--- 參數引用 ---  'Jeffadd 20160726
            'Call SaveIniString(strSection, "LightValue", .LightValue, strFileName)
            'Call SaveIniString(strSection, "FindHeightPosZ", CDbl(.dblFindHeightPosZ), strFileName)
            'Laser NonArray
            Call SaveIniString(strSection, "LaserRunMode", .LaserRunMode, strFileName)

            For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
                Dim index As Integer = 1
                For Each node2Map In Me.NodeToMap(mStageNo)
                    Call SaveIniString(strSection, "NodeToMap" & (mStageNo + 1).ToString() & "_" & index.ToString(), node2Map, strFileName)
                    index += 1
                Next
            Next

            'Jeffadd BinNumber 先給預設值 20160328
            .BinNumber = 36
            Call SaveIniString(strSection, "BinNumber", 36, strFileName)
            For mBinNo As Integer = 0 To .BinNumber - 1
                If mBinNo <= 9 Then
                    If .sBinData.Count <= mBinNo Then
                        Dim tempBin As BinMappingData
                        tempBin.BinName = mBinNo.ToString()
                        tempBin.Disable = False
                        tempBin.PatternName = "0"
                        .sBinData.Add(mBinNo.ToString(), tempBin)
                    End If
                    Call SaveIniString(strSection, "BinNo" & mBinNo.ToString() & "Name", mBinNo.ToString(), strFileName)
                    Call SaveIniString(strSection, "BinNo" & mBinNo.ToString() & "Disable", .sBinData(mBinNo.ToString()).Disable.ToString(), strFileName)
                    Call SaveIniString(strSection, "BinNo" & mBinNo.ToString() & "PatternName", .sBinData(mBinNo.ToString()).PatternName.ToString(), strFileName)
                Else
                    Dim BinName As String = Chr(55 + mBinNo)
                    If .sBinData.Count <= mBinNo Then
                        Dim tempBin As BinMappingData
                        tempBin.BinName = BinName
                        tempBin.Disable = False
                        tempBin.PatternName = "0"
                        .sBinData.Add(BinName, tempBin)
                    End If
                    Call SaveIniString(strSection, "BinNo" & BinName & "Name", BinName, strFileName)
                    Call SaveIniString(strSection, "BinNo" & BinName & "Disable", .sBinData(BinName).Disable.ToString(), strFileName)
                    Call SaveIniString(strSection, "BinNo" & BinName & "PatternName", .sBinData(BinName).PatternName.ToString(), strFileName)
                End If

            Next

            Call SaveIniString(strSection, "NotchDirect1", CStr(.NotchDir(0)), strFileName)
            Call SaveIniString(strSection, "NotchDirect2", CStr(.NotchDir(1)), strFileName)

            SavePattern(FileName)
            SaveNode(FileName)
            SaveStageParts(FileName)
            Call SaveIniString(strSection, "ChuckMapSearchType", CInt(.ChuckMapType), strFileName)

        End With

        '[說明]:Load LodProduct 20161010
        Dim LodProductFileName As String = Application.StartupPath & "\System\" & MachineName & "\Conveyor.ini"
        strSection = "LodProduct"
        With Me
            Call SaveIniString(strSection, "Tatle", .dblProductTypeTotalNum, LodProductFileName)
            For i = 0 To .dblProductTypeTotalNum - 1
                Call SaveIniString(strSection, "Num" & (i + 1), .ProductTypeName(i), LodProductFileName)
            Next
        End With

        If Me.CheckPath = False Then
            MsgBox("Please Check Pattern Path. If Patten Path Step Contain Dot then need ContiEnd behind", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If

        Return True

ErrorHandler:  ' Error-handling routine.
        MsgBox(Err.GetException.ToString(), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        Err.Clear()
        Return False

    End Function

    ''' <summary>找資料長度最長(末端)的節點</summary>
    ''' <param name="stageNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEndNode(ByVal stageNo As Integer) As String
        Dim mEndNodeName As String = "0,0,1,0"
        For mNodeNo As Integer = 0 To Me.Node(stageNo).Keys.Count - 1
            If Me.Node(stageNo).Keys(mNodeNo).Length > 4 Then
                If Me.Node(stageNo).Keys(mNodeNo).Length > mEndNodeName.Length Then '找資料長度最長的
                    mEndNodeName = Me.Node(stageNo).Keys(mNodeNo)
                End If
            End If
        Next
        Return mEndNodeName
    End Function

#Region "參數提取保護"
    Function GetAvgWeightPerDot(ByVal stageNo As enmStage, valveNo As eValveWorkMode) As Decimal 'Soni + 2017.03.28
        ' If Not IsNothing(Me.StageParts(stageNo)) Then
        '20170520
        If IsNothing(Me.StageParts(stageNo)) Then
            Return 0
        End If
        Return Me.StageParts(stageNo).AverageWeightPerDot(valveNo)
    End Function
    Function GetSyringetPressure(ByVal stageNo As enmStage, valveNo As eValveWorkMode) As Decimal 'Soni + 2017.03.28
        If Me.StageParts(stageNo) Is Nothing Then
            Return 0
        End If
        Return Me.StageParts(stageNo).SyringePressure(valveNo)
    End Function

    Function GetValveName(ByVal stageNo As enmStage, valveNo As eValveWorkMode) As Decimal
        If Me.StageParts(stageNo) Is Nothing Then
            Return ""
        End If
        Return Me.StageParts(stageNo).ValveName(valveNo)
    End Function
#End Region



#Region "節點-陣列存取"

    ''' <summary>讀取Pattern陣列參數</summary>
    ''' <param name="fileName">[檔案全路徑]</param>
    ''' <param name="StageNumber">[平台編號]</param>
    ''' <param name="NodeName">[節點名稱]</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadNodeArray(ByVal fileName As String, ByVal StageNumber As Integer, ByVal NodeName As String) As Boolean
        Dim strSection As String
        strSection = NodeName
        With Me
            Dim mMultiArrayCount As Integer = Val(ReadIniString(strSection, "MultiArrayCount", fileName, 1)) '陣列層數
            .Node(StageNumber)(NodeName).Array.Clear()
            For i As Integer = 0 To mMultiArrayCount - 1

                strSection = NodeName & "_" & i
                Dim mTemp As New CArray
                Dim mLevel As New CRecipeNodeLevel

                mLevel.LevelType = CInt(ReadIniString(strSection, "LevelType", fileName, 0)) '單層型態

                '=== 陣列型 ===
                mTemp.CountX = Val(ReadIniString(strSection, "ArrayCountX", fileName, 1)) '矩陣X方向數量
                mTemp.CountY = Val(ReadIniString(strSection, "ArrayCountY", fileName, 1)) '矩陣Y方向數量
                mTemp.PitchX = Val(ReadIniString(strSection, "ArrayPitchX", fileName, 1)) '矩陣X方向間距
                mTemp.PitchY = Val(ReadIniString(strSection, "ArrayPitchY", fileName, 1)) '矩陣Y方向間距
                mTemp.Theta = Val(ReadIniString(strSection, "ArrayTheta", fileName, 0)) '矩陣旋轉角度
                If mTemp.CountX < 1 Then mTemp.CountX = 1
                If mTemp.CountY < 1 Then mTemp.CountY = 1

                mTemp.StartPosX = Val(ReadIniString(strSection, "StartPosX", fileName, 0)) '矩陣X方向數量
                mTemp.StartPosY = Val(ReadIniString(strSection, "StartPosY", fileName, 0)) '矩陣Y方向數量
                mTemp.StartPosZ = Val(ReadIniString(strSection, "StartPosZ", fileName, 0)) '矩陣Z方向數量 Soni + 2017.01.10 多層陣列
                mTemp.EndPosX = Val(ReadIniString(strSection, "EndPosX", fileName, 0)) '矩陣X方向數量
                mTemp.EndPosY = Val(ReadIniString(strSection, "EndPosY", fileName, 0)) '矩陣Y方向數量
                mLevel.Array = mTemp
                '=== 陣列型 ===

                '=== 非陣列型 ===
                mLevel.NonArray = New List(Of NonArray)
                mLevel.NonArray.Clear()
                Dim mNonArrayCount As Integer = Val(ReadIniString(strSection, "NonArrayCount", fileName, 0)) '非陣列型資料數
                For mI As Integer = 0 To mNonArrayCount - 1
                    Dim mNonArrayTemp As NonArray '非陣列型資料
                    mNonArrayTemp.RelPosX = Val(ReadIniString(strSection, "RelPosX_" & mI, fileName, 0))
                    mNonArrayTemp.RelPosY = Val(ReadIniString(strSection, "RelPosY_" & mI, fileName, 0))

                    mLevel.NonArray.Add(mNonArrayTemp)
                Next
                '=== 非陣列型 ===

                .Node(StageNumber)(NodeName).Array.Add(mLevel)
            Next

        End With

        Return True
    End Function

    ''' <summary>儲存Pattern陣列參數</summary>
    ''' <param name="fileName">[檔案全路徑]</param>
    ''' <param name="StageNumber">[平台編號]</param>
    ''' <param name="NodeName">[節點名稱]</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveNodeArray(ByVal fileName As String, ByVal StageNumber As Integer, ByVal NodeName As String) As Boolean
        Dim strSection As String
        strSection = NodeName

        With Me
            Call SaveIniString(strSection, "MultiArrayCount", .Node(StageNumber)(NodeName).Array.Count, strFileName) '陣列層數

            For i As Integer = 0 To .Node(StageNumber)(NodeName).Array.Count - 1
                strSection = NodeName & "_" & i

                Call SaveIniString(strSection, "LevelType", .Node(StageNumber)(NodeName).Array(i).LevelType, strFileName) '單層型態

                '=== 陣列型 ===
                Call SaveIniString(strSection, "ArrayCountX", .Node(StageNumber)(NodeName).Array(i).Array.CountX, strFileName) '矩陣X方向數量
                Call SaveIniString(strSection, "ArrayCountY", .Node(StageNumber)(NodeName).Array(i).Array.CountY, strFileName) '矩陣Y方向數量
                Call SaveIniString(strSection, "ArrayPitchX", .Node(StageNumber)(NodeName).Array(i).Array.PitchX, strFileName) '矩陣X方向間距
                Call SaveIniString(strSection, "ArrayPitchY", .Node(StageNumber)(NodeName).Array(i).Array.PitchY, strFileName) '矩陣Y方向間距
                Call SaveIniString(strSection, "ArrayTheta", .Node(StageNumber)(NodeName).Array(i).Array.Theta, strFileName) '矩陣旋轉角度

                Call SaveIniString(strSection, "StartPosX", .Node(StageNumber)(NodeName).Array(i).Array.StartPosX, strFileName)
                Call SaveIniString(strSection, "StartPosY", .Node(StageNumber)(NodeName).Array(i).Array.StartPosY, strFileName)
                Call SaveIniString(strSection, "StartPosZ", .Node(StageNumber)(NodeName).Array(i).Array.StartPosZ, strFileName) 'Soni + 2017.01.10 多層陣列
                Call SaveIniString(strSection, "EndPosX", .Node(StageNumber)(NodeName).Array(i).Array.EndPosX, strFileName)
                Call SaveIniString(strSection, "EndPosY", .Node(StageNumber)(NodeName).Array(i).Array.EndPosY, strFileName)
                '=== 陣列型 ===

                '=== 非陣列型 ===
                Call SaveIniString(strSection, "NonArrayCount", .Node(StageNumber)(NodeName).Array(i).NonArray.Count, strFileName) '非陣列型資料數
                For mI As Integer = 0 To .Node(StageNumber)(NodeName).Array(i).NonArray.Count - 1
                    Call SaveIniString(strSection, "RelPosX_" & mI, .Node(StageNumber)(NodeName).Array(i).NonArray(mI).RelPosX, strFileName)
                    Call SaveIniString(strSection, "RelPosY_" & mI, .Node(StageNumber)(NodeName).Array(i).NonArray(mI).RelPosY, strFileName)

                Next
                '=== 非陣列型 ===

            Next


        End With
        Return True
    End Function

#End Region


#Region "節點存取"
    Function LoadNode(ByVal fileName As String) As Boolean
        With Me
            Dim strSection As String

            Dim mStageNo As Integer
            strSection = "Node"
            For mStageNo = enmStage.No1 To gSSystemParameter.StageCount - 1
                If IsNothing(.Node(mStageNo)) = False Then
                    .NodeCount(mStageNo) = Val(ReadIniString(strSection, "NodeCount" & (mStageNo + 1).ToString(), fileName, 0)) 'Node數量
                End If
            Next
            For mStageNo = enmStage.No1 To gSSystemParameter.StageCount - 1
                .Node(mStageNo).Clear()
                If IsNothing(.Node(mStageNo)) = False Then
                    .Node(mStageNo).Clear()
                    strSection = "NodeStage" & mStageNo.ToString()
                    For mNodeNo As Integer = 0 To .NodeCount(mStageNo) - 1
                        Dim nodeName As String = ReadIniString(strSection, "NodeNo" & (mNodeNo + 1).ToString, strFileName, 0)
                        If Not .Node(mStageNo).ContainsKey(nodeName) Then
                            .Node(mStageNo).Add(nodeName, New CRecipeNode)
                        End If
                    Next

                    Dim KeyCollect As Dictionary(Of String, CRecipeNode).KeyCollection = .Node(mStageNo).Keys
                    For mNodeNo As Integer = 0 To KeyCollect.Count - 1
                        Dim mNodeKey As String = KeyCollect(mNodeNo)
                        strSection = mNodeKey
                        .Node(mStageNo)(mNodeKey).NodePath = ReadIniString(strSection, "NodePath", strFileName, 0)
                        .Node(mStageNo)(mNodeKey).PatternName = ReadIniString(strSection, "PatternName", strFileName, 0)
                        .Node(mStageNo)(mNodeKey).NodeStartingX = CInt(ReadIniString(strSection, "NodeStartingX", strFileName, 1))
                        .Node(mStageNo)(mNodeKey).NodeStartingY = CInt(ReadIniString(strSection, "NodeStartingY", strFileName, 1))
                        .Node(mStageNo)(mNodeKey).Enable = CBool(ReadIniString(strSection, "Enable", strFileName, "False"))

                        Dim NodeLevel As Integer = (mNodeKey.Split(",").Length - 1) / 2
                        Dim NodePath() As String = mNodeKey.Split(",")

                        If NodeLevel > 3 Then
                            .Node(mStageNo)(mNodeKey).ParentNode = ""
                            For LevelCount = 0 To NodeLevel - 2
                                .Node(mStageNo)(mNodeKey).ParentNode += NodePath(LevelCount * 2) & "," & NodePath(LevelCount * 2 + 1) & ","
                            Next
                        Else
                            .Node(mStageNo)(mNodeKey).ParentNode = "Nothing"
                        End If

                        LoadNodeArray(fileName, mStageNo, mNodeKey)

                        Dim mChildCount As Integer = ReadIniString(strSection, "ChildNodesCount", strFileName, 0) '子節點
                        .Node(mStageNo)(mNodeKey).ChildNodes.Clear()
                        For mChildNode As Integer = 0 To mChildCount - 1
                            Dim mChildNodeName As String
                            mChildNodeName = ReadIniString(strSection, "ChildNodes(" & mChildNode & ")", strFileName, "")
                            If .Node(mStageNo)(mNodeKey).ChildNodes.Contains(mChildNodeName) = False Then
                                .Node(mStageNo)(mNodeKey).ChildNodes.Add(mChildNodeName)
                            End If
                        Next
                        'Soni 2017.02.09 
                        For mConveyorNo As Integer = 0 To .Node(mStageNo)(mNodeKey).ConveyorPos.Count - 1
                            If mConveyorNo <> 0 Then strSection = mNodeKey & "_Conveyor" & (mConveyorNo + 1)
                            .Node(mStageNo)(mNodeKey).ConveyorPos(mConveyorNo).Load(strFileName, strSection) 'Conveyor參數讀檔
                            For mAlignNo As Integer = 0 To .Node(mStageNo)(mNodeKey).ConveyorPos(mConveyorNo).AlignmentData.Count - 1
                                AddSceneNamelist(.Node(mStageNo)(mNodeKey).ConveyorPos(mConveyorNo).AlignmentData(mAlignNo).AlignScene)
                            Next
                            AddSceneNamelist(.Node(mStageNo)(mNodeKey).ConveyorPos(mConveyorNo).SkipMarkData(0).AlignScene)
                        Next
                        .Node(mStageNo)(mNodeKey).AlignType = CInt(ReadIniString(strSection, "enmAlignType", strFileName, 0))
                        .Node(mStageNo)(mNodeKey).AlignmentEnable = CBool(ReadIniString(strSection, "AligmentEnable", strFileName, True))
                        .Node(mStageNo)(mNodeKey).SkipMarkEnable = CBool(ReadIniString(strSection, "SkipMarkEnable", strFileName, False))
                        .Node(mStageNo)(mNodeKey).LaserEnable = CBool(ReadIniString(strSection, "LaserEnable", strFileName, True))
                        .Node(mStageNo)(mNodeKey).TeachIndexX = Val(ReadIniString(strSection, "TeachIndexX", strFileName, 0))
                        .Node(mStageNo)(mNodeKey).TeachIndexY = Val(ReadIniString(strSection, "TeachIndexY", strFileName, 0))
                        .Node(mStageNo)(mNodeKey).IsNodeConnect = CBool(ReadIniString(strSection, "IsNodeConnect", strFileName, "False"))
                    Next
                End If

                For i As Integer = 0 To .Node(mStageNo).Keys.Count - 1 '對每個Node
                    Dim mNodeID As String = .Node(mStageNo).Keys(i) '取得NodeID
                    If mNodeID.Length >= 16 Then 'Soni + 2016.09.14 計算相對偏移量
                        Dim parentNodeID As String = "" 'mNodeID.Substring(0, mNodeID.Length - 4) '取父節點名稱
                        If Me.GetParentNodeID(mNodeID, parentNodeID) = True Then
                            'Soni 2017.02.09 
                            For mConveyorNo As Integer = 0 To .Node(mStageNo)(mNodeID).ConveyorPos.Count - 1 '計算定位與基準偏移量
                                If Me.Node(mStageNo)(parentNodeID).ConveyorPos(mConveyorNo).AlignmentData.Count > 0 AndAlso Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData.Count > 0 Then 'Soni 2017.02.09 雙閥資料結構
                                    '[Note]:Parent Node AlignmentEnable=False，則不能取AlignPos
                                    '       計算上層定位點與點膠基準點偏移量
                                    If Me.Node(mStageNo)(parentNodeID).AlignmentEnable = True Then
                                        Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignBasicOffsetX = Me.Node(mStageNo)(parentNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosX - Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                                        Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignBasicOffsetY = Me.Node(mStageNo)(parentNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosY - Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                                        Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignBasicOffsetZ = Me.Node(mStageNo)(parentNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosZ - Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                                    Else
                                        Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignBasicOffsetX = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                                        Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignBasicOffsetY = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                                        Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignBasicOffsetZ = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                                    End If

                                    '[Note]:Node AlignmentEnable=False，則不能取AlignPos
                                    '       計算本層定位與點膠基準點偏移量
                                    If Me.Node(mStageNo)(mNodeID).AlignmentEnable = True Then
                                        Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignBasicOffsetX = Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosX - Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                                        Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignBasicOffsetY = Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosY - Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                                        Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignBasicOffsetZ = Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosZ - Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                                    Else
                                        Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignBasicOffsetX = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                                        Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignBasicOffsetY = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                                        Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignBasicOffsetZ = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                                    End If

                                    '[Note]:Parent Node AlignmentEnable=False，則不能取AlignPos
                                    '       Node AlignmentEnable=False，則不能取AlignPos
                                    '       計算上層定位點與本層定位點偏移量
                                    If Me.Node(mStageNo)(parentNodeID).AlignmentEnable = True Then
                                        If Me.Node(mStageNo)(mNodeID).AlignmentEnable = True Then
                                            Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetX = Me.Node(mStageNo)(parentNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosX - Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosX
                                            Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetY = Me.Node(mStageNo)(parentNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosY - Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosY
                                            Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetZ = Me.Node(mStageNo)(parentNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosZ - Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosZ
                                        Else
                                            Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetX = Me.Node(mStageNo)(parentNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosX
                                            Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetY = Me.Node(mStageNo)(parentNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosY
                                            Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetZ = Me.Node(mStageNo)(parentNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosZ
                                        End If
                                    Else
                                        If Me.Node(mStageNo)(mNodeID).AlignmentEnable = True Then
                                            Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetX = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosX
                                            Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetY = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosY
                                            Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetZ = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosZ
                                        Else
                                            Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetX = 0
                                            Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetY = 0
                                            Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetZ = 0
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Else
                        For mConveyorNo As Integer = 0 To .Node(mStageNo)(mNodeID).ConveyorPos.Count - 1 '計算定位與基準偏移量
                            If Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData.Count > 0 Then 'Soni 2017.02.09 雙閥資料結構
                                '計算上層定位點與點膠基準點偏移量
                                Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignBasicOffsetX = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                                Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignBasicOffsetY = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                                Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignBasicOffsetZ = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                                '[Note]:Node AlignmentEnable=False，則不能取AlignPos
                                '       計算本層定位與點膠基準點偏移量
                                If Me.Node(mStageNo)(mNodeID).AlignmentEnable = True Then
                                    Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignBasicOffsetX = Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosX - Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                                    Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignBasicOffsetY = Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosY - Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                                    Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignBasicOffsetZ = Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosZ - Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                                Else
                                    Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignBasicOffsetX = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                                    Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignBasicOffsetY = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                                    Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignBasicOffsetZ = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                                End If
                                '[Note]:Node AlignmentEnable=False，則不能取AlignPos
                                '       計算上層定位點與本層定位點偏移量
                                If Me.Node(mStageNo)(mNodeID).AlignmentEnable = True Then
                                    Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetX = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosX
                                    Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetY = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosY
                                    Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetZ = -Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosZ
                                Else
                                    Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetX = 0
                                    Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetY = 0
                                    Me.Node(mStageNo)(mNodeID).ConveyorPos(mConveyorNo).ParentAlignAlignOffsetZ = 0
                                End If
                            End If
                        Next
                    End If
                Next
            Next
        End With
        Return True
    End Function

    Public Function SaveNode(ByVal fileName As String) As Boolean
        With Me
            Dim strSection As String
            Dim mStageNo As Integer

            strSection = "Node"
            For mStageNo = enmStage.No1 To gSSystemParameter.StageCount - 1
                If IsNothing(.Node(mStageNo)) = False Then
                    Call SaveIniString(strSection, "NodeCount" & (mStageNo + 1).ToString, .NodeCount(mStageNo).ToString(), strFileName) 'Node數量
                End If
            Next
            For mStageNo = enmStage.No1 To gSSystemParameter.StageCount - 1
                If IsNothing(.Node(mStageNo)) = False Then
                    Dim KeyCollect As Dictionary(Of String, CRecipeNode).KeyCollection = .Node(mStageNo).Keys
                    For mNodeNo As Integer = 0 To KeyCollect.Count - 1
                        strSection = "NodeStage" & mStageNo.ToString()
                        Dim mNodeKey As String = KeyCollect(mNodeNo)
                        Call SaveIniString(strSection, "NodeNo" & (mNodeNo + 1).ToString, .Node(mStageNo)(mNodeKey).NodePath, strFileName)
                    Next

                    For mNodeNo As Integer = 0 To KeyCollect.Count - 1
                        Dim mNodeKey As String = KeyCollect(mNodeNo)
                        strSection = mNodeKey
                        Call SaveIniString(strSection, "NodePath", .Node(mStageNo)(mNodeKey).NodePath, strFileName)
                        Call SaveIniString(strSection, "PatternName", .Node(mStageNo)(mNodeKey).PatternName, strFileName)
                        Call SaveIniString(strSection, "NodeStartingX", .Node(mStageNo)(mNodeKey).NodeStartingX.ToString(), strFileName)
                        Call SaveIniString(strSection, "NodeStartingY", .Node(mStageNo)(mNodeKey).NodeStartingY.ToString(), strFileName)
                        Call SaveIniString(strSection, "Enable", .Node(mStageNo)(mNodeKey).Enable.ToString(), strFileName)

                        SaveNodeArray(fileName, mStageNo, mNodeKey)

                        Call SaveIniString(strSection, "ChildNodesCount", .Node(mStageNo)(mNodeKey).ChildNodes.Count, strFileName)
                        For mChildNode As Integer = 0 To .Node(mStageNo)(mNodeKey).ChildNodes.Count - 1
                            Call SaveIniString(strSection, "ChildNodes(" & mChildNode & ")", .Node(mStageNo)(mNodeKey).ChildNodes(mChildNode), strFileName)
                        Next

                        For mConveyorNo As Integer = 0 To .Node(mStageNo)(mNodeKey).ConveyorPos.Count - 1 'Soni 2017.02.09 雙軌資料結構
                            If mConveyorNo <> 0 Then strSection = mNodeKey & "_Conveyor" & (mConveyorNo + 1)
                            .Node(mStageNo)(mNodeKey).ConveyorPos(mConveyorNo).Save(fileName, strSection)
                            For mAlignNo As Integer = 0 To .Node(mStageNo)(mNodeKey).ConveyorPos(mConveyorNo).AlignmentData.Count - 1
                                AddSceneNamelist(.Node(mStageNo)(mNodeKey).ConveyorPos(mConveyorNo).AlignmentData(mAlignNo).AlignScene)
                            Next
                            AddSceneNamelist(.Node(mStageNo)(mNodeKey).ConveyorPos(mConveyorNo).SkipMarkData(0).AlignScene)
                        Next
                        Call SaveIniString(strSection, "enmAlignType", .Node(mStageNo)(mNodeKey).AlignType, strFileName)
                        Call SaveIniString(strSection, "AligmentEnable", .Node(mStageNo)(mNodeKey).AlignmentEnable.ToString(), strFileName)
                        Call SaveIniString(strSection, "SkipMarkEnable", .Node(mStageNo)(mNodeKey).SkipMarkEnable.ToString(), strFileName)
                        Call SaveIniString(strSection, "LaserEnable", .Node(mStageNo)(mNodeKey).LaserEnable.ToString(), strFileName)
                        Call SaveIniString(strSection, "TeachIndexX", .Node(mStageNo)(mNodeKey).TeachIndexX.ToString(), strFileName)
                        Call SaveIniString(strSection, "TeachIndexY", .Node(mStageNo)(mNodeKey).TeachIndexY.ToString(), strFileName)
                        'Call SaveIniString(strSection, "BasicOffsetX", .Node(mStageNo)(mNodeKey).BasicOffsetX.ToString(), strFileName)
                        'Call SaveIniString(strSection, "BasicOffsetY", .Node(mStageNo)(mNodeKey).BasicOffsetY.ToString(), strFileName)
                        'Call SaveIniString(strSection, "BasicOffsetZ", .Node(mStageNo)(mNodeKey).BasicOffsetZ.ToString(), strFileName)
                        Call SaveIniString(strSection, "IsNodeConnect", .Node(mStageNo)(mNodeKey).IsNodeConnect.ToString(), strFileName)
                    Next
                End If
            Next
        End With
        Return True
    End Function
#End Region

#Region "Pattern存取"

    ''' <summary>[檢查Pattern正確性]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckPattern() As Boolean

        Dim mNodeName As String
        Dim mPatternName As String
        Dim mRoundNo As Integer
        Dim mStepNo As Integer
        Dim mI As Integer
        Dim mJ As Integer
        Dim mStageNo As enmStage
        Dim mPosBFail As Boolean
        'Soni / 2017.11.13 增加條件保護.
        '[Note]:檢查每個Stage下的節點對應的Pattern
        With Me
            For mStageNo = enmStage.No1 To gSSystemParameter.StageCount - 1
                If mStageNo < Me.DispenseTraversal.Count - 1 Then
                    For mI = 0 To Me.DispenseTraversal(mStageNo).Count - 1
                        '[說明]:找出對應的Node & Patterm
                        mNodeName = Me.DispenseTraversal(mStageNo)(mI)
                        If Me.Node(mStageNo).ContainsKey(mNodeName) Then
                            mPatternName = Me.Node(mStageNo)(mNodeName).PatternName
                            If .Pattern.ContainsKey(mPatternName) Then
                                If .Pattern(mPatternName).Round.Count > 0 Then
                                    For mRoundNo = 0 To .Pattern(mPatternName).Round.Count - 1
                                        For mStepNo = 0 To .Pattern(mPatternName).Round(mRoundNo).CStep.Count - 1
                                            With .Pattern(mPatternName).Round(mRoundNo).CStep(mStepNo)
                                                Select Case .StepType
                                                    Case eStepFunctionType.SelectValve
                                                        If gSSystemParameter.StageUseValveCount - 1 < .SelectValve.ValveNo Then
                                                            Return False
                                                        End If
                                                        mPosBFail = True
                                                        For mJ = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(mStageNo).DicCCDTiltValveCalib(.SelectValve.ValveNo).Keys.Count - 1
                                                            If .SelectValve.PosB = gSSystemParameter.Pos.CCDTiltVavleCalbration(mStageNo).DicCCDTiltValveCalib(.SelectValve.ValveNo).Keys(mJ) Then
                                                                mPosBFail = False
                                                                Exit For
                                                            End If
                                                        Next
                                                        If mPosBFail = True Then
                                                            Return False
                                                        End If

                                                    Case eStepFunctionType.Dots3D
                                                        If Me.Pattern(mPatternName).ProcessTimeType <> eProcessTimeType.None Then
                                                            Return False
                                                        End If

                                                End Select
                                            End With
                                        Next
                                    Next
                                End If
                            Else
                                Return False
                            End If
                        Else
                            Return False
                        End If
                    Next
                Else
                    Return False
                End If
            Next
        End With

        Return True
    End Function
    Public Function CheckPath() As Boolean '主要確認Round 若有含Dot step 必須加ContiEnd
        With Me
            Dim strSection As String
            Dim PathOK As Boolean = True
            strSection = "Pattern"
            For mPatterNo As Integer = 0 To .Pattern.Count - 1 '對每個Pattern
                Dim mPatternName As String = .Pattern.Keys(mPatterNo)
                strSection = mPatternName
                If .Pattern(mPatternName).Round.Count = 0 Then 'Round物件不存在時, 建立空物件
                    .Pattern(mPatternName).Round.Add(New CPatternRound)
                End If

                If .Pattern(mPatternName).Round.Count > 0 Then
                    For mRoundNo = 0 To .Pattern(mPatternName).Round.Count - 1 '對每個Round
                        strSection = mPatternName & "_Round" & (mRoundNo + 1).ToString
                        With .Pattern(mPatternName).Round(mRoundNo)
                            For mStepNo As Integer = 0 To .CStep.Count - 1
                                With .CStep(mStepNo)
                                    Select Case .StepType
                                        Case eStepFunctionType.Dots3D
                                            If mStepNo + 1 >= Me.Pattern(mPatternName).Round(mRoundNo).CStep.Count - 1 Then
                                                If Me.Pattern(mPatternName).Round(mRoundNo).CStep(mStepNo + 1).StepType <> eStepFunctionType.ContiEnd Then
                                                    PathOK = False
                                                End If
                                            Else
                                                PathOK = False
                                            End If

                                    End Select
                                End With
                            Next
                            If PathOK = False Then
                                Return False
                            End If
                        End With
                    Next
                End If
            Next

        End With
        Return True
    End Function

    ''' <summary>讀取Pattern設定</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadPattern(ByVal fileName As String) As Boolean
        With Me

            Dim strSection As String
            strSection = "Pattern"
            .PatternCount = Val(ReadIniString(strSection, "PatternCount", fileName, 1)) 'Pattern數量
            .PatternName.Clear() '清除Pattern名稱清單
            .Pattern.Clear() '清除Pattern清單

            For i As Integer = 0 To .PatternCount - 1
                .PatternName.Add(ReadIniString(strSection, "PatternName" & (i + 1).ToString, fileName, "Stage" & (i + 1).ToString)) '讀取Pattern名稱 Soni / 2017.04.24 i->i+1
                .Pattern.Add(.PatternName(i), New CRecipePattern)
                .Pattern(.PatternName(i)).Round.Clear()
            Next

            For i As Integer = 0 To .StageNodeID.Count - 1
                .StageNodeID(i) = ReadIniString(strSection, "StageNodeID" & (i + 1).ToString, fileName, "Stage" & (i + 1).ToString) '讀取起始Pattern名稱
            Next

            For mPatterNo As Integer = 0 To .PatternCount - 1 '對每個Pattern
                Dim mPatternName As String = .Pattern.Keys(mPatterNo)
                strSection = mPatternName
                .Pattern(mPatternName).Name = mPatternName
                .Pattern(mPatternName).RoundCount = Val(ReadIniString(strSection, "RoundCount", fileName, 1)) '讀取Round數量
                .Pattern(mPatternName).ProcessTimeType = Val(ReadIniString(strSection, "ProcessTimeType", fileName, 0))
                'Toby  新增顆數設定
                .Pattern(mPatternName).Diecount = Val(ReadIniString(strSection, "DieCount", fileName, 0))

                '--- 2016.08.09 存讀檔重新整理, 補充缺漏項目 ---
                If .Pattern(mPatternName).RoundCount > 0 Then '有數量就建立
                    For mRoundNo = 0 To .Pattern(mPatternName).RoundCount - 1 '對每個Round
                        .Pattern(mPatternName).Round.Add(New CPatternRound) '新建Round
                        strSection = mPatternName & "_Round" & (mRoundNo + 1).ToString
                        .Pattern(mPatternName).Round(mRoundNo).StepCount = Val(ReadIniString(strSection, "StepCount", strFileName, 0)) 'Soni / 2017.04.24 預設值改為0, 因為有數量沒步驟,StepType =0
                        If .Pattern(mPatternName).Round(mRoundNo).StepCount <> 0 Then '數量不為零才清除, 數量為0時提供預設值
                            .Pattern(mPatternName).Round(mRoundNo).CStep.Clear()
                        End If

                        With .Pattern(mPatternName).Round(mRoundNo)
                            .ProcessTime = Val(ReadIniString(strSection, "ProcessTime", fileName, 0)) '讀取製程返回時間
                            For mStepNo As Integer = 0 To .StepCount - 1
                                Dim mCStep As New CPatternStep
                                With mCStep
                                    .StepNo = mStepNo
                                    .StepType = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "StepType", strFileName, 0))
                                    Select Case .StepType
                                        Case eStepFunctionType.SelectValve
                                            .SelectValve.ValveNo = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "ValveNo", strFileName, 0))
                                            .SelectValve.PosB = CDec(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "PosB", strFileName, 0))
                                        Case eStepFunctionType.Wait
                                            .Wait.DwellTimeInMs = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "WaitDwellTime", strFileName, 0))

                                        Case eStepFunctionType.Circle2D
                                            .Circle2D.CenterPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DCenterPosX", strFileName, 0)) 'Soni + 2015.07.23 圓心相對座標
                                            .Circle2D.CenterPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DCenterPosY", strFileName, 0)) 'Soni + 2015.07.23 圓心相對座標
                                            .Circle2D.Direction = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DDirection", strFileName, 0))
                                            .Circle2D.Middle2PosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DMid2PosX", strFileName, 0))
                                            .Circle2D.Middle2PosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DMid2PosY", strFileName, 0))
                                            .Circle2D.MiddlePosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DMidPosX", strFileName, 0))
                                            .Circle2D.MiddlePosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DMidPosY", strFileName, 0))
                                            .Circle2D.EndPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DEndPosX", strFileName, 0))
                                            .Circle2D.EndPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DEndPosY", strFileName, 0))
                                            .Circle2D.Pitch = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DPitch", strFileName, 400))
                                            .Circle2D.RPM = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DRPM", strFileName, 0))
                                            .Circle2D.StartPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DStartPosX", strFileName, 0))
                                            .Circle2D.StartPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DStartPosY", strFileName, 0))
                                            .Circle2D.WeightControl.Type = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DWeightControlType", strFileName, 0))
                                            .Circle2D.WeightControl.DotCounts = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DWeightControlDotCounts", strFileName, 5))
                                            .Circle2D.WeightControl.Weight = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DWeightControlWeight", strFileName, 1))
                                            .Circle2D.WeightControl.Velocity = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DWeightControlVelocity", strFileName, 20))
                                            .Circle2D.ArcParameterName = ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DParameterName", strFileName, "Default")
                                            .Circle2D.Comment = ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DComment", strFileName, "")
                                            '20171016
                                            .Circle2D.StartVel = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DStartVelocity", strFileName, 20))

                                        Case eStepFunctionType.Arc2D
                                            .Arc2D.Angle = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DAngle", strFileName, 0))
                                            .Arc2D.CenterPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DCenterPosX", strFileName, 0)) 'Soni + 2015.07.23 圓心相對座標
                                            .Arc2D.CenterPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DCenterPosY", strFileName, 0)) 'Soni + 2015.07.23 圓心相對座標
                                            .Arc2D.Direction = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DDirection", strFileName, 0))
                                            .Arc2D.EndPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DEndPosX", strFileName, 0))
                                            .Arc2D.EndPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DEndPosY", strFileName, 0))
                                            .Arc2D.MiddlePosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DMidPosX", strFileName, 0))
                                            .Arc2D.MiddlePosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DMidPosY", strFileName, 0))
                                            .Arc2D.MiddlePosZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DMidPosZ", strFileName, 0))
                                            .Arc2D.Pitch = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DPitch", strFileName, 400))
                                            .Arc2D.Radius = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DRadius", strFileName, 0))
                                            .Arc2D.RPM = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DRPM", strFileName, 0))
                                            .Arc2D.StartPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DStartPosX", strFileName, 0))
                                            .Arc2D.StartPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DStartPosY", strFileName, 0))
                                            .Arc2D.WeightControl.Type = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DWeightControlType", strFileName, 0))
                                            .Arc2D.WeightControl.DotCounts = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DWeightControlDotCounts", strFileName, 5))
                                            .Arc2D.WeightControl.Weight = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DWeightControlWeight", strFileName, 1))
                                            .Arc2D.WeightControl.Velocity = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DWeightControlVelocity", strFileName, 20))
                                            .Arc2D.ArcParameterName = ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DParameterName", strFileName, "Default")
                                            .Arc2D.Comment = ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DComment", strFileName, "")
                                            '20171016
                                            .Arc2D.StartVel = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DStartVelocity", strFileName, 20))

                                        Case eStepFunctionType.Dots3D
                                            .Dots3D.PosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DEndPosX", strFileName, 0))
                                            .Dots3D.PosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DEndPosY", strFileName, 0))
                                            .Dots3D.PosZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DEndPosZ", strFileName, 0))
                                            .Dots3D.RPM = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DRPM", strFileName, 0))
                                            .Dots3D.WeightControl.Type = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DWeightControlType", strFileName, 0))
                                            .Dots3D.WeightControl.DotCounts = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DWeightControlDotCounts", strFileName, 1))
                                            .Dots3D.WeightControl.Weight = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DWeightControlWeight", strFileName, 1))
                                            .Dots3D.WeightControl.Velocity = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DWeightControlVelocity", strFileName, 20))
                                            .Dots3D.DotParameterName = ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "DotParameterName", strFileName, "Default")

                                            '20171016
                                            .Dots3D.StartVel = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dot3DStartVelocity", strFileName, 20))

                                        Case eStepFunctionType.Line3D
                                            .Line3D.EndPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DEndPosX", strFileName, 0))
                                            .Line3D.EndPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DEndPosY", strFileName, 0))
                                            .Line3D.EndPosZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DEndPosZ", strFileName, 0))
                                            .Line3D.Pitch = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DPitch", strFileName, 400))
                                            .Line3D.RPM = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DRPM", strFileName, 0))
                                            .Line3D.StartPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DStartPosX", strFileName, 0))
                                            .Line3D.StartPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DStartPosY", strFileName, 0))
                                            .Line3D.StartPosZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DStartPosZ", strFileName, 0))
                                            .Line3D.WeightControl.Type = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DWeightControlType", strFileName, 0))
                                            .Line3D.WeightControl.DotCounts = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DWeightControlDotCounts", strFileName, 5))
                                            .Line3D.WeightControl.Weight = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DWeightControlWeight", strFileName, 1))
                                            .Line3D.WeightControl.Velocity = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DWeightControlVelocity", strFileName, 20))
                                            .Line3D.LineParameterName = ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "LineParameterName", strFileName, "Default")
                                            .Line3D.Comment = ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DComment", strFileName, "")
                                            '20171016
                                            .Line3D.StartVel = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DStartVelocity", strFileName, 20))

                                        Case eStepFunctionType.Circle3D
                                            .Circle3D.CenterPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DCenterPosX", strFileName, 0)) 'Soni + 2015.07.23 圓心相對座標
                                            .Circle3D.CenterPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DCenterPosY", strFileName, 0)) 'Soni + 2015.07.23 圓心相對座標
                                            .Circle3D.CenterPosZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DCenterPosZ", strFileName, 0)) 'Soni + 2015.07.23 圓心相對座標
                                            .Circle3D.Direction = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DDirection", strFileName, 0))
                                            .Circle3D.EndPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DEndPosX", strFileName, 0))
                                            .Circle3D.EndPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DEndPosY", strFileName, 0))
                                            .Circle3D.EndPosZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DEndPosZ", strFileName, 0))
                                            .Circle3D.MiddlePosB = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DMidPosB", strFileName, 0))
                                            .Circle3D.MiddlePosC = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DMidPosC", strFileName, 0))
                                            .Circle3D.MiddlePosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DMidPosX", strFileName, 0))
                                            .Circle3D.MiddlePosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DMidPosY", strFileName, 0))
                                            .Circle3D.MiddlePosZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DMidPosZ", strFileName, 0))
                                            .Circle3D.Pitch = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DPitch", strFileName, 400))
                                            .Circle3D.RPM = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DRPM", strFileName, 0))
                                            .Circle3D.WeightControl.Type = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DWeightControlType", strFileName, 0))
                                            .Circle3D.WeightControl.DotCounts = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DWeightControlDotCounts", strFileName, 5))
                                            .Circle3D.WeightControl.Weight = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DWeightControlWeight", strFileName, 1))
                                            .Circle3D.WeightControl.Velocity = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DWeightControlVelocity", strFileName, 20))
                                            .Circle3D.ArcParameterName = ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DParameterName", strFileName, "Default")

                                        Case eStepFunctionType.Arc3D
                                            .Arc3D.Angle = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DAngle", strFileName, 0))
                                            .Arc3D.CenterPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DCenterPosX", strFileName, 0)) 'Soni + 2015.07.23 圓心相對座標
                                            .Arc3D.CenterPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DCenterPosY", strFileName, 0)) 'Soni + 2015.07.23 圓心相對座標
                                            .Arc3D.CenterPosZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DCenterPosZ", strFileName, 0)) 'Soni + 2015.07.23 圓心相對座標
                                            .Arc3D.Direction = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DDirection", strFileName, 0))
                                            .Arc3D.EndPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DEndPosX", strFileName, 0))
                                            .Arc3D.EndPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DEndPosY", strFileName, 0))
                                            .Arc3D.EndPosZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DEndPosZ", strFileName, 0))
                                            .Arc3D.MiddlePosB = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DMidPosB", strFileName, 0))
                                            .Arc3D.MiddlePosC = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DMidPosC", strFileName, 0))
                                            .Arc3D.MiddlePosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DMidPosX", strFileName, 0))
                                            .Arc3D.MiddlePosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DMidPosY", strFileName, 0))
                                            .Arc3D.MiddlePosZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DMidPosZ", strFileName, 0))
                                            .Arc3D.Pitch = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DPitch", strFileName, 400))
                                            .Arc3D.Radius = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DRadius", strFileName, 0))
                                            .Arc3D.RPM = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DDotRPM", strFileName, 0))
                                            .Arc3D.StartPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DStartPosX", strFileName, 0))
                                            .Arc3D.StartPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DStartPosY", strFileName, 0))
                                            .Arc3D.StartPosZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DStartPosZ", strFileName, 0))
                                            .Arc3D.WeightControl.Type = CInt(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DWeightControlType", strFileName, 0))
                                            .Arc3D.WeightControl.DotCounts = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DWeightControlDotCounts", strFileName, 5))
                                            .Arc3D.WeightControl.Weight = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DWeightControlWeight", strFileName, 1))
                                            .Arc3D.WeightControl.Velocity = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DWeightControlVelocity", strFileName, 20))
                                            .Arc3D.ArcParameterName = ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DParameterName", strFileName, "Default")

                                        Case eStepFunctionType.Move3D
                                            .Move3D.EndPosX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Move3DEndPosX", strFileName, 0))
                                            .Move3D.EndPosY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Move3DEndPosY", strFileName, 0))
                                            .Move3D.EndPosZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Move3DEndPosZ", strFileName, 0))

                                        Case eStepFunctionType.Array
                                            .Array.BasicOffsetX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "BasicOffsetX", strFileName, 0))
                                            .Array.BasicOffsetY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "BasicOffsetY", strFileName, 0))
                                            .Array.BasicOffsetZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "BasicOffsetZ", strFileName, 0))
                                            .Array.CountX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "CountX", strFileName, 1))
                                            .Array.CountY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "CountY", strFileName, 1))
                                            .Array.PatternID = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "PatternID", strFileName, ""))
                                            .Array.PitchX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "PitchX", strFileName, 10))
                                            .Array.PitchY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "PitchY", strFileName, 10))

                                        Case eStepFunctionType.SubPattern
                                            .SubPattern.BasicOffsetX = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "BasicOffsetX", strFileName, 0))
                                            .SubPattern.BasicOffsetY = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "BasicOffsetY", strFileName, 0))
                                            .SubPattern.BasicOffsetZ = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "BasicOffsetZ", strFileName, 0))
                                            .SubPattern.PatternID = Val(ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "PatternID", strFileName, 0))

                                    End Select
                                    .SceneID = ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "SceneID", strFileName, "")
                                    .LaserProgramID = ReadIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "LaserProgramID", strFileName, "")
                                End With
                                .CStep.Add(mCStep)
                            Next
                        End With
                    Next
                End If
            Next

        End With
        Return True
    End Function
    ''' <summary>儲存Pattern設定</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePattern(ByVal fileName As String) As Boolean
        With Me
            Dim strSection As String
            strSection = "Pattern"
            Call SaveIniString(strSection, "PatternCount", .Pattern.Count, strFileName) 'Pattern數量
            For i As Integer = 0 To .Pattern.Count - 1
                Call SaveIniString(strSection, "PatternName" & (i + 1).ToString, .Pattern.Keys(i), strFileName) '儲存Pattern名稱
            Next
            For i As Integer = 0 To .StageNodeID.Count - 1
                Call SaveIniString(strSection, "StageNodeID" & (i + 1).ToString, .StageNodeID(i), strFileName) '讀取起始Pattern名稱
            Next
            For mPatterNo As Integer = 0 To .Pattern.Count - 1 '對每個Pattern
                Dim mPatternName As String = .Pattern.Keys(mPatterNo)
                strSection = mPatternName
                If .Pattern(mPatternName).Round.Count = 0 Then 'Round物件不存在時, 建立空物件
                    .Pattern(mPatternName).Round.Add(New CPatternRound)
                End If
                'Toby  新增顆數設定
                Call SaveIniString(strSection, "DieCount", .Pattern(mPatternName).Diecount, strFileName) '儲存retrun Die 數量 
                Call SaveIniString(strSection, "RoundCount", .Pattern(mPatternName).Round.Count, strFileName) '儲存Round數量
                Call SaveIniString(strSection, "ProcessTimeType", .Pattern(mPatternName).ProcessTimeType, strFileName) '儲存Round數量

                If .Pattern(mPatternName).Round.Count > 0 Then
                    For mRoundNo = 0 To .Pattern(mPatternName).Round.Count - 1 '對每個Round
                        strSection = mPatternName & "_Round" & (mRoundNo + 1).ToString
                        With .Pattern(mPatternName).Round(mRoundNo)
                            Call SaveIniString(strSection, "StepCount", .CStep.Count, strFileName)
                            Call SaveIniString(strSection, "ProcessTime", .ProcessTime, strFileName)
                            '--- 2016.08.09 存讀檔重新整理, 補充缺漏項目 ---
                            For mStepNo As Integer = 0 To .CStep.Count - 1
                                With .CStep(mStepNo)
                                    Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "StepType", .StepType, strFileName)
                                    Select Case .StepType
                                        Case eStepFunctionType.SelectValve
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "ValveNo", .SelectValve.ValveNo, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "PosB", .SelectValve.PosB, strFileName)

                                        Case eStepFunctionType.Wait
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "WaitDwellTime", .Wait.DwellTimeInMs, strFileName)

                                        Case eStepFunctionType.Circle2D
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DCenterPosX", .Circle2D.CenterPosX, strFileName) 'Soni + 2015.07.23 圓心相對座標
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DCenterPosY", .Circle2D.CenterPosY, strFileName) 'Soni + 2015.07.23 圓心相對座標
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DDirection", .Circle2D.Direction, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DMid2PosX", .Circle2D.Middle2PosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DMid2PosY", .Circle2D.Middle2PosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DMidPosX", .Circle2D.MiddlePosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DMidPosY", .Circle2D.MiddlePosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DEndPosX", .Circle2D.EndPosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DEndPosY", .Circle2D.EndPosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DPitch", .Circle2D.Pitch, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DRPM", .Circle2D.RPM, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DStartPosX", .Circle2D.StartPosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DStartPosY", .Circle2D.StartPosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DWeightControlType", CInt(.Circle2D.WeightControl.Type), strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DWeightControlDotCounts", .Circle2D.WeightControl.DotCounts, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DWeightControlWeight", .Circle2D.WeightControl.Weight, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DWeightControlVelocity", .Circle2D.WeightControl.Velocity, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DParameterName", .Circle2D.ArcParameterName, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DComment", .Circle2D.Comment, strFileName)

                                            '20171016
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle2DStartVelocity", .Circle2D.StartVel, strFileName)

                                        Case eStepFunctionType.Arc2D
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DAngle", .Arc2D.Angle, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DCenterPosX", .Arc2D.CenterPosX, strFileName) 'Soni + 2015.07.23 圓心相對座標
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DCenterPosY", .Arc2D.CenterPosY, strFileName) 'Soni + 2015.07.23 圓心相對座標
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DDirection", .Arc2D.Direction, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DEndPosX", .Arc2D.EndPosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DEndPosY", .Arc2D.EndPosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DMidPosX", .Arc2D.MiddlePosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DMidPosY", .Arc2D.MiddlePosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DMidPosZ", .Arc2D.MiddlePosZ, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DPitch", .Arc2D.Pitch, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DRadius", .Arc2D.Radius, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DRPM", .Arc2D.RPM, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DStartPosX", .Arc2D.StartPosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DStartPosY", .Arc2D.StartPosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DWeightControlType", CInt(.Arc2D.WeightControl.Type), strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DWeightControlDotCounts", .Arc2D.WeightControl.DotCounts, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DWeightControlWeight", .Arc2D.WeightControl.Weight, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DWeightControlVelocity", .Arc2D.WeightControl.Velocity, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DParameterName", .Arc2D.ArcParameterName, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DComment", .Arc2D.Comment, strFileName)
                                            '20171016
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc2DStartVelocity", .Arc2D.StartVel, strFileName)

                                        Case eStepFunctionType.Dots3D
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DEndPosX", .Dots3D.PosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DEndPosY", .Dots3D.PosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DEndPosZ", .Dots3D.PosZ, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DDRPM", .Dots3D.RPM, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DWeightControlType", CInt(.Dots3D.WeightControl.Type), strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DWeightControlDotCounts", .Dots3D.WeightControl.DotCounts, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DWeightControlWeight", .Dots3D.WeightControl.Weight, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dots3DWeightControlVelocity", .Dots3D.WeightControl.Velocity, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "DotParameterName", .Dots3D.DotParameterName, strFileName)

                                            '20171016
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Dot3DStartVelocity", .Dots3D.StartVel, strFileName)

                                        Case eStepFunctionType.Line3D
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DEndPosX", .Line3D.EndPosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DEndPosY", .Line3D.EndPosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DEndPosZ", .Line3D.EndPosZ, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DPitch", .Line3D.Pitch, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DDRPM", .Line3D.RPM, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DStartPosX", .Line3D.StartPosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DStartPosY", .Line3D.StartPosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DStartPosZ", .Line3D.StartPosZ, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DWeightControlType", CInt(.Line3D.WeightControl.Type), strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DWeightControlDotCounts", .Line3D.WeightControl.DotCounts, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DWeightControlWeight", .Line3D.WeightControl.Weight, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DWeightControlVelocity", .Line3D.WeightControl.Velocity, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "LineParameterName", .Line3D.LineParameterName, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DComment", .Line3D.Comment, strFileName)
                                            '20171016
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Line3DStartVelocity", .Line3D.StartVel, strFileName)

                                        Case eStepFunctionType.Circle3D
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DCenterPosX", .Circle3D.CenterPosX, strFileName) 'Soni + 2015.07.23 圓心相對座標
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DCenterPosY", .Circle3D.CenterPosY, strFileName) 'Soni + 2015.07.23 圓心相對座標
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DCenterPosZ", .Circle3D.CenterPosZ, strFileName) 'Soni + 2015.07.23 圓心相對座標
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DDirection", .Circle3D.Direction, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DEndPosX", .Circle3D.EndPosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DEndPosY", .Circle3D.EndPosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DEndPosZ", .Circle3D.EndPosZ, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DMidPosB", .Circle3D.MiddlePosB, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DMidPosC", .Circle3D.MiddlePosC, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DMidPosX", .Circle3D.MiddlePosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DMidPosY", .Circle3D.MiddlePosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DMidPosZ", .Circle3D.MiddlePosZ, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DPitch", .Circle3D.Pitch, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DRPM", .Circle3D.RPM, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DWeightControlType", CInt(.Circle3D.WeightControl.Type), strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DWeightControlDotCounts", .Circle3D.WeightControl.DotCounts, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DWeightControlWeight", .Circle3D.WeightControl.Weight, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DWeightControlVelocity", .Circle3D.WeightControl.Velocity, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Circle3DParameterName", .Circle3D.ArcParameterName, strFileName)

                                        Case eStepFunctionType.Arc3D
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DAngle", .Arc3D.Angle, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DCenterPosX", .Arc3D.CenterPosX, strFileName) 'Soni + 2015.07.23 圓心相對座標
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DCenterPosY", .Arc3D.CenterPosY, strFileName) 'Soni + 2015.07.23 圓心相對座標
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DCenterPosZ", .Arc3D.CenterPosZ, strFileName) 'Soni + 2015.07.23 圓心相對座標
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DDirection", .Arc3D.Direction, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DEndPosX", .Arc3D.EndPosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DEndPosY", .Arc3D.EndPosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DEndPosZ", .Arc3D.EndPosZ, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DMidPosB", .Arc3D.MiddlePosB, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DMidPosC", .Arc3D.MiddlePosC, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DMidPosX", .Arc3D.MiddlePosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DMidPosY", .Arc3D.MiddlePosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DMidPosZ", .Arc3D.MiddlePosZ, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DPitch", .Arc3D.Pitch, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DRadius", .Arc3D.Radius, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DRPM", .Arc3D.RPM, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DStartPosX", .Arc3D.StartPosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DStartPosY", .Arc3D.StartPosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DStartPosZ", .Arc3D.StartPosZ, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DWeightControlType", CInt(.Arc3D.WeightControl.Type), strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DWeightControlDotCounts", .Arc3D.WeightControl.DotCounts, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DWeightControlWeight", .Arc3D.WeightControl.Weight, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DWeightControlVelocity", .Arc3D.WeightControl.Velocity, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Arc3DParameterName", .Arc3D.ArcParameterName, strFileName)

                                        Case eStepFunctionType.Move3D
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Move3DEndPosX", .Move3D.EndPosX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Move3DEndPosY", .Move3D.EndPosY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "Move3DEndPosZ", .Move3D.EndPosZ, strFileName)

                                        Case eStepFunctionType.Array '陣列Call Pattern
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "BasicOffsetX", .Array.BasicOffsetX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "BasicOffsetY", .Array.BasicOffsetY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "BasicOffsetZ", .Array.BasicOffsetZ, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "CountX", .Array.CountX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "CountY", .Array.CountY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "PatternID", .Array.PatternID, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "PitchX", .Array.PitchX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "PitchY", .Array.PitchY, strFileName)

                                        Case eStepFunctionType.SubPattern '單一Call Pattern
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "BasicOffsetX", .SubPattern.BasicOffsetX, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "BasicOffsetY", .SubPattern.BasicOffsetY, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "BasicOffsetZ", .SubPattern.BasicOffsetZ, strFileName)
                                            Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "PatternID", .SubPattern.PatternID, strFileName)

                                    End Select
                                    Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "SceneID", .SceneID, strFileName)
                                    Call SaveIniString(strSection, "StepNo" & (mStepNo + 1).ToString & "LaserProgramID", .LaserProgramID, strFileName)

                                End With
                            Next
                        End With
                    Next
                End If
            Next

        End With
        Return True
    End Function
#End Region


#Region "Soni 2017.03.06"
    ''' <summary>[抓取Level等級]</summary>
    ''' <param name="nodeName"></param>
    ''' <param name="levelNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNodeLevel(ByVal nodeName As String, ByRef levelNo As Integer) As Boolean

        Dim mNodeSplite() As String

        mNodeSplite = nodeName.Trim().Split(",")
        If mNodeSplite.Length >= 2 Then
            levelNo = CInt(mNodeSplite(mNodeSplite.Length - 3)) - 1
        End If

        If mNodeSplite.Length = 1 Then
            Return False
        End If

        Return True
    End Function

    Public Function GetParentNodeID(ByVal nodeName As String, ByRef parentNodeID As String) As Boolean
        If nodeName Is Nothing Then '輸入資料異常
            Return False
        End If

        Dim mNodeSplite() As String

        mNodeSplite = nodeName.Trim().Split(",")
        If mNodeSplite Is Nothing Then '資料分拆異常
            Return False
        End If
        If mNodeSplite.Length < 2 Then '資料分拆異常
            Return False
        End If
        Dim mNodeLevel As Integer = CInt((mNodeSplite.Length - 1) / 2)
        If mNodeLevel <= 1 Then '節點層數異常
            Return False
        End If
        For i As Integer = 0 To mNodeLevel - 2
            parentNodeID += mNodeSplite(i * 2) & "," & mNodeSplite(i * 2 + 1) & ","
        Next
        Return True
    End Function
#End Region


    ''' <summary>[讀取Stage下Valve的周邊參數]</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadStageParts(ByVal fileName As String) As Boolean
        Dim strSection As String
        With Me
            For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
                strSection = "StageNo" & mStageNo
                For mValveNo As Integer = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                    .StageParts(mStageNo).ValveShiftX(mValveNo) = Val(ReadIniString(strSection, "Valve" & (mValveNo + 1).ToString & "ShiftX", strFileName, 0))
                    .StageParts(mStageNo).ValveShiftY(mValveNo) = Val(ReadIniString(strSection, "Valve" & (mValveNo + 1).ToString & "ShiftY", strFileName, 0))
                    .StageParts(mStageNo).ValveShiftZ(mValveNo) = Val(ReadIniString(strSection, "Valve" & (mValveNo + 1).ToString & "ShiftZ", strFileName, 5)) 'Soni + 2016.08.17 預設拉高5mm, 避免校正做太好磨到
                    .StageParts(mStageNo).SyringePressure(mValveNo) = Val(ReadIniString(strSection, "Valve" & (mValveNo + 1).ToString & "SyringPressure", strFileName, 0))
                    .StageParts(mStageNo).ValvePressure(mValveNo) = Val(ReadIniString(strSection, "Valve" & (mValveNo + 1).ToString & "ValvePressure", strFileName, 0))
                    .StageParts(mStageNo).AverageWeightPerDot(mValveNo) = Val(ReadIniString(strSection, "Valve" & (mValveNo + 1).ToString & "AverageWeightPerDot", strFileName, 0.01))
                    .StageParts(mStageNo).PurgeName(mValveNo) = ReadIniString(strSection, "Valve" & (mValveNo + 1).ToString & "PurgeName", strFileName, "Default") 'Soni / 2017.04.26 提供預設值Default
                    .StageParts(mStageNo).PasteName(mValveNo) = ReadIniString(strSection, "Valve" & (mValveNo + 1).ToString & "PasteName", strFileName, "Default") 'Soni / 2017.04.26 提供預設值Default
                    .StageParts(mStageNo).ValveName(mValveNo) = ReadIniString(strSection, "Valve" & (mValveNo + 1).ToString & "ValveName", strFileName, "Default") 'Soni / 2017.04.26 提供預設值Default
                    .StageParts(mStageNo).FlowRateName(mValveNo) = ReadIniString(strSection, "Valve" & (mValveNo + 1).ToString & "FlowRateName", strFileName, "Default") 'Soni / 2017.04.26 提供預設值Default
                Next

            Next

            strSection = "RecipeStep"
            .TempName = ReadIniString(strSection, "TempName", strFileName, "Default") 'Soni / 2017.04.26 提供預設值Default
        End With

        Return True
    End Function

  
    ''' <summary>[儲存Stage下Valve的周邊參數]</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveStageParts(ByVal fileName As String) As Boolean
        Dim strSection As String
        With Me
            For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
                strSection = "StageNo" & mStageNo
                For mValveNo As Integer = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                    Call SaveIniString(strSection, "Valve" & (mValveNo + 1).ToString & "ShiftX", .StageParts(mStageNo).ValveShiftX(mValveNo), strFileName)
                    Call SaveIniString(strSection, "Valve" & (mValveNo + 1).ToString & "ShiftY", .StageParts(mStageNo).ValveShiftY(mValveNo), strFileName)
                    Call SaveIniString(strSection, "Valve" & (mValveNo + 1).ToString & "ShiftZ", .StageParts(mStageNo).ValveShiftZ(mValveNo), strFileName)
                    Call SaveIniString(strSection, "Valve" & (mValveNo + 1).ToString & "SyringPressure", .StageParts(mStageNo).SyringePressure(mValveNo), strFileName)
                    Call SaveIniString(strSection, "Valve" & (mValveNo + 1).ToString & "ValvePressure", .StageParts(mStageNo).ValvePressure(mValveNo), strFileName)
                    Call SaveIniString(strSection, "Valve" & (mValveNo + 1).ToString & "AverageWeightPerDot", .StageParts(mStageNo).AverageWeightPerDot(mValveNo), strFileName)
                    Call SaveIniString(strSection, "Valve" & (mValveNo + 1).ToString & "PurgeName", .StageParts(mStageNo).PurgeName(mValveNo), strFileName)
                    Call SaveIniString(strSection, "Valve" & (mValveNo + 1).ToString & "PasteName", .StageParts(mStageNo).PasteName(mValveNo), strFileName)
                    Call SaveIniString(strSection, "Valve" & (mValveNo + 1).ToString & "ValveName", .StageParts(mStageNo).ValveName(mValveNo), strFileName)
                    Call SaveIniString(strSection, "Valve" & (mValveNo + 1).ToString & "FlowRateName", .StageParts(mStageNo).FlowRateName(mValveNo), strFileName)
                Next
            Next

            strSection = "RecipeStep"
            Call SaveIniString(strSection, "TempName", .TempName, strFileName)
        End With
        Return True
    End Function

    ' ''' <summary>
    ' ''' 讀取溫度參數
    ' ''' </summary>
    'Public Function LoadTemperatureParameter(ByVal filePath As String) As Boolean
    '    Try
    '        Dim section As String = "Temperature"
    '        With Me
    '            .WorkTemp = CUInt(ReadIniString(section, "WorkTemp", filePath, "0"))
    '            .LoaderTemp = CUInt(ReadIniString(section, "LoaderTemp", filePath, "0"))
    '            .UnloaderTemp = CUInt(ReadIniString(section, "UnloaderTemp", filePath, "0"))

    '            .A_HotPlate(0) = IIf(ReadIniString(section, "A1_hotPlate", filePath, "0") = "1", True, False)
    '            .A_HotPlate(1) = IIf(ReadIniString(section, "A2_hotPlate", filePath, "0") = "1", True, False)
    '            .A_HotPlate(2) = IIf(ReadIniString(section, "A3_hotPlate", filePath, "0") = "1", True, False)
    '            .A_HotPlate(3) = IIf(ReadIniString(section, "A4_hotPlate", filePath, "0") = "1", True, False)
    '            .A_HotPlate(4) = IIf(ReadIniString(section, "A5_hotPlate", filePath, "0") = "1", True, False)
    '            .A_HotPlate(5) = IIf(ReadIniString(section, "A6_hotPlate", filePath, "0") = "1", True, False)

    '            .B_HotPlate(0) = IIf(ReadIniString(section, "B1_hotPlate", filePath, "0") = "1", True, False)
    '            .B_HotPlate(1) = IIf(ReadIniString(section, "B2_hotPlate", filePath, "0") = "1", True, False)
    '            .B_HotPlate(2) = IIf(ReadIniString(section, "B3_hotPlate", filePath, "0") = "1", True, False)
    '            .B_HotPlate(3) = IIf(ReadIniString(section, "B4_hotPlate", filePath, "0") = "1", True, False)
    '            .B_HotPlate(4) = IIf(ReadIniString(section, "B5_hotPlate", filePath, "0") = "1", True, False)
    '            .B_HotPlate(5) = IIf(ReadIniString(section, "B6_hotPlate", filePath, "0") = "1", True, False)

    '        End With

    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' 儲存溫度參數
    ' ''' </summary>
    'Public Function SaveTemperatureParameter(ByVal filePath As String) As Boolean
    '    Try
    '        Dim section As String = "Temperature"
    '        With Me
    '            Call SaveIniString(section, "WorkTemp", .WorkTemp, filePath)
    '            Call SaveIniString(section, "LoaderTemp", .LoaderTemp, filePath)
    '            Call SaveIniString(section, "UnloaderTemp", .UnloaderTemp, filePath)

    '            Call SaveIniString(section, "A1_hotPlate", IIf(.A_HotPlate(0), 1, 0), filePath)
    '            Call SaveIniString(section, "A2_hotPlate", IIf(.A_HotPlate(1), 1, 0), filePath)
    '            Call SaveIniString(section, "A3_hotPlate", IIf(.A_HotPlate(2), 1, 0), filePath)
    '            Call SaveIniString(section, "A4_hotPlate", IIf(.A_HotPlate(3), 1, 0), filePath)
    '            Call SaveIniString(section, "A5_hotPlate", IIf(.A_HotPlate(4), 1, 0), filePath)
    '            Call SaveIniString(section, "A6_hotPlate", IIf(.A_HotPlate(5), 1, 0), filePath)

    '            Call SaveIniString(section, "B1_hotPlate", IIf(.B_HotPlate(0), 1, 0), filePath)
    '            Call SaveIniString(section, "B2_hotPlate", IIf(.B_HotPlate(1), 1, 0), filePath)
    '            Call SaveIniString(section, "B3_hotPlate", IIf(.B_HotPlate(2), 1, 0), filePath)
    '            Call SaveIniString(section, "B4_hotPlate", IIf(.B_HotPlate(3), 1, 0), filePath)
    '            Call SaveIniString(section, "B5_hotPlate", IIf(.B_HotPlate(4), 1, 0), filePath)
    '            Call SaveIniString(section, "B6_hotPlate", IIf(.B_HotPlate(5), 1, 0), filePath)
    '        End With

    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

#Region "工作清單搜尋方法"

    ''' <summary>搜尋樹</summary>
    ''' <remarks></remarks>
    Public SearchTree As New List(Of TreeNode)
    ''' <summary>產生搜尋樹</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenSearchTree() As List(Of TreeNode)
        SearchTree.Clear()

        For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
            Dim mNodeName As String = "Stage" & (mStageNo + 1).ToString
            Dim mStageNode As New TreeNode '建立節點
            mStageNode.Name = mNodeName
            mStageNode.Text = mNodeName

            Dim mBodyNode As New TreeNode
            mBodyNode.Name = "MainBody"
            mBodyNode.Text = "MainBody"
            mStageNode.Nodes.Add(mBodyNode) '本文

            SearchTree.Add(mStageNode)

            Dim mNodeLevelPath() As Integer '節點分層路徑
            Dim mNodeIDCollection As Dictionary(Of String, CRecipeNode).KeyCollection = Me.Node(mStageNo).Keys
            Dim tempNode As TreeNode

            '更新TreeView 從node資料中找到全部的節點再依照相對應的位置加入
            For Each mNodeID As String In mNodeIDCollection '對清單中的每一個節點
                Dim mSplitedNodeID() As String = mNodeID.Split(",") '將資料分拆
                Dim LevelCount As Integer = (mSplitedNodeID.Length - 1) \ 2 '分層數
                ReDim mNodeLevelPath(LevelCount - 1)
                For mLevel As Integer = 0 To LevelCount - 1
                    mNodeLevelPath(mLevel) = CInt(mSplitedNodeID(mLevel * 2 + 1)) '將節點ID的資料分層拆開
                Next
                tempNode = SearchTree(mStageNo).Clone() '取得Stage節點
                If Not tempNode Is Nothing Then
                    For mLevelNo As Integer = 1 To mNodeLevelPath.Length - 2 '找到該節點父節點
                        tempNode = tempNode.Nodes(mNodeLevelPath(mLevelNo))
                    Next
                    Dim newNode As New TreeNode
                    newNode.Text = Me.Node(mStageNo)(mNodeID).PatternName '設定該節點名稱
                    newNode.Name = mNodeID
                    newNode.ToolTipText = mNodeID
                    tempNode.Nodes.Add(newNode)
                    '填入親代節點名稱，第三層視為無親代，第四層開始填入
                    If LevelCount > 3 Then
                        Me.Node(mStageNo)(mNodeID).ParentNode = ""
                        For NameCount = 0 To LevelCount - 2
                            Me.Node(mStageNo)(mNodeID).ParentNode += mSplitedNodeID(NameCount * 2) & "," & mSplitedNodeID(NameCount * 2 + 1) & ","
                        Next
                    Else
                        Me.Node(mStageNo)(mNodeID).ParentNode = "Nothing"
                    End If
                    For j As Integer = 1 To mNodeLevelPath.Length - 2 '往回找到Stage節點
                        tempNode = tempNode.Parent
                    Next
                    SearchTree(mStageNo) = tempNode.Clone
                End If

            Next

        Next

        Return SearchTree
    End Function

    ''' <summary>
    ''' [點膠工作清單搜尋方法]
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DispenseSort()

        For mStageNo = enmStage.No1 To gSSystemParameter.StageCount - 1
            DispenseTraversal(mStageNo).Clear()
            Dim treenumber As Integer = 0
            Dim usenumber As Integer = 0
            Dim tempNowTreeNode As New TreeNode
            Dim tempLastTreeNode As New List(Of TreeNode)

            'PreOder搜尋方式 Start
            tempNowTreeNode = SearchTree(mStageNo).Nodes(0)
            tempLastTreeNode.Add(tempNowTreeNode)
            '保護只有一層WorkPiece
            If IsNothing(tempNowTreeNode.LastNode) = False Then
                '處理後續資料流，會依照先層再個別索引去讀取
                Do
                    '保護讀到null值
                    If IsNothing(tempNowTreeNode.LastNode) = False Then
                        '紀錄每層的成員，採用先進後出方式
                        For j = 0 To tempNowTreeNode.Nodes.Count - 1
                            tempLastTreeNode.Add(tempNowTreeNode.Nodes(tempNowTreeNode.Nodes.Count - 1 - j))
                            treenumber += 1
                        Next
                    End If
                    '當成員掃紀錄完畢，從序列最小的先找，並從暫存器移除
                    tempNowTreeNode = tempLastTreeNode(tempLastTreeNode.Count - 1).Clone()
                    tempLastTreeNode.RemoveAt(tempLastTreeNode.Count - 1)
                    usenumber += 1
                    DispenseTraversal(mStageNo).Add(tempNowTreeNode.Name)
                Loop Until treenumber = usenumber And IsNothing(tempNowTreeNode.LastNode) = True
            End If
            ''PreOder搜尋方式 End
            'For j = 0 To DispenseTraversal(mStageNo).Count - 1
            '    Debug.Print("Stage" & (mStageNo + 1).ToString & " Disp Traversal:" & DispenseTraversal(mStageNo)(j))
            'Next
        Next
    End Sub
    'Soni 2017.02.09 雙軌資料結構
    ''' <summary>[ReDim StageMap 資料結構]</summary>
    ''' <remarks></remarks>
    Public Function Initial_StageMap(ByVal stagNo As Integer, ByVal passCCD As Boolean, Optional ByVal conveyorNo As eConveyor = eConveyor.ConveyorNo1) As Boolean

        Dim mStageNo As Integer
        Dim mNodeKey As Integer

        Dim mRoundCount As Integer
        Dim mNodeID As String
        Dim mPatternID As String
        Dim mArrayX As Integer
        Dim mArrayY As Integer

        Dim mIdxX As Integer
        Dim mIdxY As Integer
        Dim mI As Integer
        Dim mLaserNo As Integer

        mStageNo = stagNo
        'Eason 20170302 Ticket:100090 , System Update Crash

        SyncLock (gStageMap(mStageNo).mobjectLock)


            '[Note]:將Node加入
            gStageMap(mStageNo).Node.Clear()
            'For mNodeKey = 0 To Me.ScanTraversal(mStageNo).Count - 1
            '    Debug.Print("Stage" & (mStageNo + 1).ToString & " Scan Traversal:" & Me.ScanTraversal(mStageNo)(mNodeKey))
            '    If Me.ScanTraversal(mStageNo)(mNodeKey) <> "LevelEnd" Then
            '        mNodeID = Me.ScanTraversal(mStageNo)(mNodeKey)
            '        If Not gStageMap(mStageNo).Node.ContainsKey(mNodeID) Then
            '            gStageMap(mStageNo).Node.Add(mNodeID, New CPatternMap) '展開節點
            '        End If
            '    End If
            'Next

            For mNodeKey = 0 To Me.CCDFixTraversal(mStageNo).Count - 1
                'Debug.Print("Stage" & (mStageNo + 1).ToString & " CCD Fix Traversal:" & Me.CCDFixTraversal(mStageNo)(mNodeKey))
                mNodeID = Me.CCDFixTraversal(mStageNo)(mNodeKey)
                If Not gStageMap(mStageNo).Node.ContainsKey(mNodeID) Then
                    gStageMap(mStageNo).Node.Add(mNodeID, New CPatternMap) '展開節點
                End If
            Next

            '[Note]:根據各Node給定預設值
            For mNodeKey = 0 To gStageMap(mStageNo).Node.Count - 1
                mNodeID = gStageMap(mStageNo).Node.Keys(mNodeKey)
                Dim mMultiArrayAdapter = New CMultiArrayAdapter(Me.Node(mStageNo)(mNodeID).Array)
                mPatternID = Me.Node(mStageNo)(mNodeID).PatternName
                mArrayX = mMultiArrayAdapter.GetMemoryCountX() - 1
                mArrayY = mMultiArrayAdapter.GetMemoryCountY() - 1

                If Me.Pattern.ContainsKey(mPatternID) = True Then
                    mRoundCount = Me.Pattern(mPatternID).Round.Count
                Else
                    mRoundCount = 0
                End If
                gStageMap(mStageNo).Node(mNodeID).AlignType = Me.Node(mStageNo)(mNodeID).AlignType 'Soni 2017.02.09

                ReDim gStageMap(mStageNo).Node(mNodeID).SRecipePos(mArrayX, mArrayY)
                ReDim gStageMap(mStageNo).Node(mNodeID).SLaserValue(mArrayX, mArrayY)
                ReDim gStageMap(mStageNo).Node(mNodeID).ScanGlueArray(mArrayX, mArrayY)
                ReDim gStageMap(mStageNo).Node(mNodeID).ChipState(mArrayX, mArrayY)
                ReDim gStageMap(mStageNo).Node(mNodeID).SDispenseGlue(mArrayX, mArrayY)
                ReDim gStageMap(mStageNo).Node(mNodeID).SBinMapData(mArrayX, mArrayY)

                '[Note]:先將Round加進來然後再做ReDim
                For mI = 0 To mRoundCount - 1
                    gStageMap(mStageNo).Node(mNodeID).Round.Add(New CRoundMap)
                    ReDim gStageMap(mStageNo).Node(mNodeID).Round(mI).DispensingStatus(mArrayX, mArrayY)
                Next



                If Me.Node(mStageNo)(mNodeID).IsMapping Then
                    'TODO: Asa 建立BinMap Data，若不需與MappingData確認則不用建立
                End If

                For mIdxX = 0 To mArrayX
                    For mIdxY = 0 To mArrayY
                        ReDim gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).CCDFinish(CInt(gStageMap(mStageNo).Node(mNodeID).AlignType))
                        '[Note]:給SkipMark座標與狀態
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).SkipMarkPosX = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(mIdxX, mIdxY)
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).SkipMarkPosY = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(mIdxX, mIdxY)
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).SkipMarkPosZ = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosZ
                        '[Note]:給Scan座標與狀態
                        Select Case Me.Node(mStageNo)(mNodeID).AlignType 'Soni 2017.02.09
                            Case enmAlignType.DevicePos1
                                '[Note]:Scan Pos
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosX = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosY = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosZ = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
                                '[Note]:Dispensing
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).RealBasicPosX = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).BasicPositionX + mMultiArrayAdapter.GetMemoryPosX(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).RealBasicPosY = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).BasicPositionY + mMultiArrayAdapter.GetMemoryPosY(mIdxX, mIdxY)
                                If passCCD = True Then
                                    gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).CCDFinish(0) = True
                                End If
                                'Debug.Print("Pos: " & gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).RealBasicPosX & " , " & gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).RealBasicPosY)

                            Case enmAlignType.DevicePos2
                                '[Note]:Scan Pos
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosX = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosY = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosZ = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosX2 = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosY2 = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosZ2 = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosZ
                                '[Note]:Dispensing
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).RealBasicPosX = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).BasicPositionX + mMultiArrayAdapter.GetMemoryPosX(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).RealBasicPosY = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).BasicPositionY + mMultiArrayAdapter.GetMemoryPosY(mIdxX, mIdxY)

                                If passCCD = True Then
                                    gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).CCDFinish(1) = True
                                End If

                            Case enmAlignType.DevicePos3
                                '[Note]:Scan Pos
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosX = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosY = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosZ = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosX2 = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosY2 = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosZ2 = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosZ
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosX3 = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosY3 = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ScanPosZ3 = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosZ
                                '[Note]:Dispensing
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).RealBasicPosX = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).BasicPositionX + mMultiArrayAdapter.GetMemoryPosX(mIdxX, mIdxY)
                                gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).RealBasicPosY = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).BasicPositionY + mMultiArrayAdapter.GetMemoryPosY(mIdxX, mIdxY)
                                If passCCD = True Then
                                    gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).CCDFinish(2) = True
                                End If

                        End Select
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).IsCCDOffsetReady = False
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).IsCCDOffsetReady2 = False
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).IsCCDOffsetReady3 = False

                        '[Note]:給Laser座標 狀態 
                        ReDim gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).LaserPosX(Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).LaserData.Count - 1)
                        ReDim gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).LaserPosY(Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).LaserData.Count - 1)
                        ReDim gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).LaserPosZ(Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).LaserData.Count - 1)
                        ReDim gStageMap(mStageNo).Node(mNodeID).SLaserValue(mIdxX, mIdxY).ZHigh(Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).LaserData.Count - 1)
                        ReDim gStageMap(mStageNo).Node(mNodeID).SLaserValue(mIdxX, mIdxY).LaserFinish(Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).LaserData.Count - 1)

                        For mLaserNo = 0 To Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).LaserData.Count - 1
                            gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).LaserPosX(mLaserNo) = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).LaserData(mLaserNo).LaserPositionX + mMultiArrayAdapter.GetMemoryPosX(mIdxX, mIdxY)
                            gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).LaserPosY(mLaserNo) = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).LaserData(mLaserNo).LaserPositionY + mMultiArrayAdapter.GetMemoryPosY(mIdxX, mIdxY)
                            gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).LaserPosZ(mLaserNo) = Me.Node(mStageNo)(mNodeID).ConveyorPos(conveyorNo).LaserData(mLaserNo).LaserPositionZ
                        Next

                        'Soni +2017.05.05 提供盲打預設值
                        If gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).LaserPosZ.Count > 0 Then
                            gStageMap(mStageNo).Node(mNodeID).SLaserValue(mIdxX, mIdxY).RealBasicZHigh = gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).LaserPosZ(0) '0
                        Else
                            gStageMap(mStageNo).Node(mNodeID).SLaserValue(mIdxX, mIdxY).RealBasicZHigh = 0
                            Debug.Assert(False)
                        End If


                        With gStageMap(mStageNo).Node(mNodeID).ChipState(mIdxX, mIdxY)
                            .DieState = enmDieState.None
                            .NeedUpdate = False
                        End With

                        '[Note]:先暫時預設都是Bin1
                        gStageMap(mStageNo).Node(mNodeID).SBinMapData(mIdxX, mIdxY).PatternName = Me.Node(mStageNo)(mNodeID).PatternName
                        gStageMap(mStageNo).Node(mNodeID).SBinMapData(mIdxX, mIdxY).Disable = False
                        gStageMap(mStageNo).Node(mNodeID).SBinMapData(mIdxX, mIdxY).Status = eDieStatus.None
                        gStageMap(mStageNo).Node(mNodeID).SBinMapData(mIdxX, mIdxY).BinName = "1"
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).IsByPassCCDScanFixAction = False
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).IsByPassDispensingAction = False
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).IsByPassLaserAction = False

                        '[Note]:其他預設狀態與數值
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).IsParentAlignFail = False
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ParentCenterPosX = 0
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ParentCenterPosY = 0
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ParentOffsetX = 0
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ParentOffsetY = 0
                        gStageMap(mStageNo).Node(mNodeID).SRecipePos(mIdxX, mIdxY).ParentTh = 0

                        '[Note]:將點膠狀態重置
                        For mI = 0 To mRoundCount - 1
                            'gStageMap(mStageNo).Node(mNodeID).Round(mI).DispensingStatus(mIdxX, mIdxY) = eDispensingStatus.None
                            'Eason 20170302 Ticket:100090 , System Update Crash
                            gStageMap(mStageNo).Node(mNodeID).Round(mI).SetDispensingStatus(mIdxX, mIdxY, eDispensingStatus.None)
                        Next
                    Next
                Next
            Next
        End SyncLock

        If SortNodeConnect(mStageNo) = False Then
            ProjectIO.gEqpMsg.Add("Initial_StageMap", Error_1025003, eMessageLevel.Error)
        End If

        Return True
    End Function



    ''' <summary>
    ''' [CCD和Laser工作清單搜尋方法]
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ScanSort()
        Dim mStageNo As Integer

        For mStageNo = 0 To gSSystemParameter.StageCount - 1
            Dim treenumber As Integer = 0
            Dim usenumber As Integer = 0
            Dim tempNowTreeNode As New TreeNode
            Dim tempLastTreeNode As New List(Of TreeNode)
            Me.ScanTraversal(mStageNo).Clear()
            'LevelOrder搜尋方式 Start
            tempNowTreeNode = SearchTree(mStageNo).Nodes(0)
            '保護只有一層WorkPiece
            If IsNothing(tempNowTreeNode.LastNode) = False Then
                '處理後續資料流，會依照先層再個別索引去讀取
                Do
                    '保護讀到null值
                    If IsNothing(tempNowTreeNode.LastNode) = False Then
                        '層資料讀取
                        For j = 0 To tempNowTreeNode.Nodes.Count - 1
                            tempLastTreeNode.Add(tempNowTreeNode.Nodes(j))
                            treenumber += 1
                        Next
                        '加入層分割旗標
                        Dim newLevel As New TreeNode("LevelEnd")
                        newLevel.Name = "LevelEnd"
                        tempLastTreeNode.Add(newLevel)
                        treenumber += 1
                    End If
                    tempNowTreeNode = tempLastTreeNode(usenumber)
                    usenumber += 1
                    If tempNowTreeNode.Text = "LevelEnd" Then
                        If usenumber < tempLastTreeNode.Count Then
                            tempNowTreeNode = tempLastTreeNode(usenumber)
                            usenumber += 1
                        End If
                    End If
                Loop Until treenumber = usenumber And IsNothing(tempNowTreeNode.LastNode) = True
            End If

            For j = 0 To tempLastTreeNode.Count - 1
                Me.ScanTraversal(mStageNo).Add(tempLastTreeNode(j).Name)
                Debug.Print("Stage" & (mStageNo + 1).ToString & " Scan Traversal:" & ScanTraversal(mStageNo)(j))
            Next
            'LevelOrder搜尋方式 End

        Next


    End Sub

    ''' <summary>[CCD Fix 工作清單搜尋方法]</summary>
    ''' <remarks></remarks>
    Public Sub CCDFixSort()
        Dim mStageNo As Integer

        For mStageNo = 0 To gSSystemParameter.StageCount - 1
            Dim treenumber As Integer = 0
            Dim usenumber As Integer = 0
            Dim tempNowTreeNode As New TreeNode
            Dim tempLastTreeNode As New List(Of TreeNode)
            Me.CCDFixTraversal(mStageNo).Clear()
            tempNowTreeNode = SearchTree(mStageNo).Nodes(0)
            '保護只有一層WorkPiece
            If IsNothing(tempNowTreeNode.LastNode) = False Then
                '處理後續資料流，會依照先層再個別索引去讀取
                Do
                    '保護讀到null值
                    If IsNothing(tempNowTreeNode.LastNode) = False Then
                        '層資料讀取
                        For j = 0 To tempNowTreeNode.Nodes.Count - 1
                            tempLastTreeNode.Add(tempNowTreeNode.Nodes(j))
                            treenumber += 1
                        Next
                        '加入層分割旗標
                        Dim newLevel As New TreeNode("LevelEnd")
                        newLevel.Name = "LevelEnd"
                        tempLastTreeNode.Add(newLevel)
                        treenumber += 1
                    End If
                    tempNowTreeNode = tempLastTreeNode(usenumber)
                    usenumber += 1
                    If tempNowTreeNode.Text = "LevelEnd" Then
                        If usenumber < tempLastTreeNode.Count Then
                            tempNowTreeNode = tempLastTreeNode(usenumber)
                            usenumber += 1
                        End If
                    End If
                Loop Until treenumber = usenumber And IsNothing(tempNowTreeNode.LastNode) = True
            End If


            For mI = 0 To tempLastTreeNode.Count - 1
                '[Note]:把"LevelEnd"的節點去掉
                If tempLastTreeNode(mI).Name <> "LevelEnd" Then
                    Me.CCDFixTraversal(mStageNo).Add(tempLastTreeNode(mI).Name)
                    'Debug.Print("Stage" & (mStageNo + 1).ToString & " CCD Fix Traversal:" & CCDFixTraversal(mStageNo)(CCDFixTraversal(mStageNo).Count - 1))
                End If
            Next
        Next
    End Sub


    ''' <summary>[Laser 工作清單搜尋方法]</summary>
    ''' <remarks></remarks>
    Public Sub LaserSort()
        Dim mStageNo As Integer

        For mStageNo = 0 To gSSystemParameter.StageCount - 1
            Dim treenumber As Integer = 0
            Dim usenumber As Integer = 0
            Dim tempNowTreeNode As New TreeNode
            Dim tempLastTreeNode As New List(Of TreeNode)
            Me.LaserTraversal(mStageNo).Clear()
            tempNowTreeNode = SearchTree(mStageNo).Nodes(0)
            '保護只有一層WorkPiece
            If IsNothing(tempNowTreeNode.LastNode) = False Then
                '處理後續資料流，會依照先層再個別索引去讀取
                Do
                    '保護讀到null值
                    If IsNothing(tempNowTreeNode.LastNode) = False Then
                        '層資料讀取
                        For j = 0 To tempNowTreeNode.Nodes.Count - 1
                            tempLastTreeNode.Add(tempNowTreeNode.Nodes(j))
                            treenumber += 1
                        Next
                        '加入層分割旗標
                        Dim newLevel As New TreeNode("LevelEnd")
                        newLevel.Name = "LevelEnd"
                        tempLastTreeNode.Add(newLevel)
                        treenumber += 1
                    End If
                    tempNowTreeNode = tempLastTreeNode(usenumber)
                    usenumber += 1
                    If tempNowTreeNode.Text = "LevelEnd" Then
                        If usenumber < tempLastTreeNode.Count Then
                            tempNowTreeNode = tempLastTreeNode(usenumber)
                            usenumber += 1
                        End If
                    End If
                Loop Until treenumber = usenumber And IsNothing(tempNowTreeNode.LastNode) = True
            End If


            For mI = 0 To tempLastTreeNode.Count - 1
                '[Note]:把"LevelEnd"的節點去掉
                If tempLastTreeNode(mI).Name <> "LevelEnd" Then
                    Me.LaserTraversal(mStageNo).Add(tempLastTreeNode(mI).Name)
                    Debug.Print("Stage" & (mStageNo + 1).ToString & " Laser Traversal:" & LaserTraversal(mStageNo)(LaserTraversal(mStageNo).Count - 1))
                End If
            Next
        Next
    End Sub



    ''' <summary>
    ''' 節點串接方法
    ''' </summary>
    ''' <param name="StageNo"></param>
    ''' <returns>Stage編號</returns>
    ''' <remarks></remarks>
    Public Function SortNodeConnect(ByVal StageNo As Integer) As Boolean
        Dim NodeName As String = ""
        Dim ListNum As Integer
        Dim IsInList As Boolean
        Dim ParentName As String
        Me.NodeConnectList(StageNo).Clear()

        Dim NodeKeys As New Dictionary(Of String, String)
        Dim tempNode As New List(Of String)
        Dim strCount As Integer = 0
        Dim NodeAll As Integer = 0
        Dim NodeNowCount As Integer = 0
        Dim NodeCount As Integer
        'nodeKeys 先清空_20170710 Toby
        NodeKeys.Clear()



        While strCount <> Me.Node(StageNo).Count
            For Each NodeName In Me.Node(StageNo).Keys
                If NodeKeys.ContainsKey(NodeName) = True Then

                Else
                    NodeKeys.Add(NodeName, NodeName)
                    strCount += 1
                    Do
                        For NodeCount = 0 To Me.Node(StageNo)(NodeName).ChildNodes.Count - 1
                            If Me.Node(StageNo).ContainsKey(Me.Node(StageNo)(NodeName).ChildNodes(NodeCount)) Then '有子節點名稱, 但是清單內沒有!!! 不加
                                tempNode.Add(Me.Node(StageNo)(NodeName).ChildNodes(Me.Node(StageNo)(NodeName).ChildNodes.Count - 1 - NodeCount))
                                NodeAll += 1
                            End If
                        Next
                        If tempNode.Count > 0 Then
                            NodeName = tempNode(tempNode.Count - 1)
                            tempNode.Remove(NodeName)
                            NodeKeys.Add(NodeName, NodeName)
                            strCount += 1
                            NodeNowCount += 1
                        End If
                    Loop Until NodeAll = NodeNowCount
                End If
            Next
            If NodeName = "" Then
                Exit While
            End If
        End While


        Try
            For Each NodeName In Me.Node(StageNo).Keys
                If Me.Node(StageNo)(NodeName).IsNodeConnect = True Then
                    ParentName = NodeName
                    '找到親代有連結的項目，若無則會找到自己
                    While Me.Node(StageNo)(ParentName).ParentNode <> "Nothing"
                        If Me.Node(StageNo)(Me.Node(StageNo)(ParentName).ParentNode).IsNodeConnect = True Then
                            ParentName = Me.Node(StageNo)(ParentName).ParentNode
                        Else
                            Exit While
                        End If
                    End While
                    IsInList = False
                    '判斷親代是否已經在LIST中
                    For ListNum = 0 To Me.NodeConnectList(StageNo).Count - 1
                        If Me.NodeConnectList(StageNo)(ListNum).ContainsKey(ParentName) Then
                            IsInList = True
                            Exit For
                        End If
                    Next
                    '若在LIST中則加入此LIST，若無則新增一個新的LIST
                    If IsInList Then
                        Me.NodeConnectList(StageNo)(ListNum).Add(NodeName, NodeName)
                    Else
                        Dim tempDictionary As New Dictionary(Of String, String)
                        tempDictionary.Add(NodeName, NodeName)
                        Me.NodeConnectList(StageNo).Add(tempDictionary)
                    End If

                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region


#Region "AOI場景存取"
    Dim RecipeSceneNamelist As New ArrayList
    ''' <summary>
    ''' 清除場景名稱清單
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ClearSceneNamelist() As Boolean
        RecipeSceneNamelist.Clear()
        Return True
    End Function
    ''' <summary>
    ''' 加入場景名稱
    ''' </summary>
    ''' <param name="SceneName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddSceneNamelist(ByVal SceneName As String) As Boolean
        If SceneName = "0" Or SceneName = "" Then
            Return False
        End If

        For i As Integer = 0 To RecipeSceneNamelist.Count - 1
            If SceneName = RecipeSceneNamelist.Item(i).ToString Then '使用同一個Scene，不重複讀取
                Return True
            End If
        Next
        RecipeSceneNamelist.Add(SceneName)
        Return True
    End Function
    ''' <summary>
    ''' 讀取Recipe裡所有用到的場景
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSceneNamelist() As String()
        Dim Scenelist(RecipeSceneNamelist.Count - 1) As String
        For i As Integer = 0 To RecipeSceneNamelist.Count - 1
            Scenelist(i) = RecipeSceneNamelist.Item(i).ToString
        Next
        Return Scenelist
    End Function


#End Region

    Public Sub Print()
        With Me
            Debug.Print("Alignable:" & .Alignable)
            Debug.Print("BinNumber:" & .BinNumber)
            Debug.Print("BypassCCDResult:" & .BypassCCDResult)
            Debug.Print("BypassLaserResult:" & .BypassLaserResult)
            Debug.Print("BypassRotationCorrection:" & .BypassRotationCorrection)

            Debug.Print("CCDFixTraversal.Count:" & .CCDFixTraversal.Count)
            For i As Integer = 0 To .CCDFixTraversal.Count - 1
                If .CCDFixTraversal(i) Is Nothing Then
                    Debug.Print("CCDFixTraversal(" & i & "): Nothing")
                Else
                    For j As Integer = 0 To .CCDFixTraversal(i).Count - 1
                        Debug.Print("CCDFixTraversal(" & i & "," & j & "):" & .CCDFixTraversal(i)(j).ToString())
                    Next
                End If

            Next
            Debug.Print("DispenseTraversal.Count:" & .DispenseTraversal.Count)
            For i As Integer = 0 To .DispenseTraversal.Count - 1
                If .DispenseTraversal(i) Is Nothing Then
                    Debug.Print("DispenseTraversal(" & i & "): Nothing")
                Else
                    For j As Integer = 0 To .DispenseTraversal(i).Count - 1
                        Debug.Print("DispenseTraversal(" & i & "," & j & "):" & .DispenseTraversal(i)(j).ToString())
                    Next
                End If

            Next
            Debug.Print("LaserTraversal.Count:" & .LaserTraversal.Count)
            For i As Integer = 0 To .LaserTraversal.Count - 1
                If .LaserTraversal(i) Is Nothing Then
                    Debug.Print("LaserTraversal(" & i & "): Nothing")
                Else
                    For j As Integer = 0 To .LaserTraversal(i).Count - 1
                        Debug.Print("LaserTraversal(" & i & "," & j & "):" & .LaserTraversal(i)(j).ToString())
                    Next
                End If

            Next
            Debug.Print("ScanTraversal.Count:" & .ScanTraversal.Count)
            For i As Integer = 0 To .ScanTraversal.Count - 1
                If .ScanTraversal(i) Is Nothing Then
                    Debug.Print("ScanTraversal(" & i & "): Nothing")
                Else
                    For j As Integer = 0 To .ScanTraversal(i).Count - 1
                        Debug.Print("ScanTraversal(" & i & "," & j & "):" & .ScanTraversal(i)(j).ToString())
                    Next
                End If

            Next
            Debug.Print("Node.Count:" & .Node.Count)
            For i As Integer = 0 To .Node.Count - 1
                If .Node(i) Is Nothing Then
                    Debug.Print("Node(" & i & "): Nothing")
                Else
                    For j As Integer = 0 To .Node(i).Keys.Count - 1
                        Debug.Print("Node(" & i & ")(" & j & "):" & .Node(i).Keys(j))
                        With .Node(i)(.Node(i).Keys(j))
                            Debug.Print("AlignmentEnable: " & .AlignmentEnable)
                            Debug.Print("AlignType: " & .AlignType)
                            Debug.Print("Array.Count: " & .Array.Count)
                            For k As Integer = 0 To .Array.Count - 1
                                Debug.Print("Array( " & k & ").LevelType:" & .Array(k).LevelType)
                                Debug.Print("Array( " & k & ").Array.CountX:" & .Array(k).Array.CountX)
                                Debug.Print("Array( " & k & ").Array.CountY:" & .Array(k).Array.CountY)
                                Debug.Print("Array( " & k & ").Array.EndPosX:" & .Array(k).Array.EndPosX)
                                Debug.Print("Array( " & k & ").Array.EndPosY:" & .Array(k).Array.EndPosY)
                                Debug.Print("Array( " & k & ").Array.PitchX:" & .Array(k).Array.PitchX)
                                Debug.Print("Array( " & k & ").Array.PitchY:" & .Array(k).Array.PitchY)
                                Debug.Print("Array( " & k & ").Array.StartPosX:" & .Array(k).Array.StartPosX)
                                Debug.Print("Array( " & k & ").Array.StartPosY:" & .Array(k).Array.StartPosY)
                                Debug.Print("Array( " & k & ").Array.StartPosZ:" & .Array(k).Array.StartPosZ)
                                Debug.Print("Array( " & k & ").Array.Theta:" & .Array(k).Array.Theta)
                            Next

                            Debug.Print("ChildNodes.Count: " & .ChildNodes.Count)
                            For k As Integer = 0 To .ChildNodes.Count - 1
                                Debug.Print("ChildNodes(" & k & "):" & .ChildNodes(k))
                            Next
                            For k As Integer = 0 To .ConveyorPos.Count - 1
                                Debug.Print("ConveyorPos(" & k & ").AlignBasicOffsetX:" & .ConveyorPos(k).AlignBasicOffsetX)
                                Debug.Print("ConveyorPos(" & k & ").AlignBasicOffsetY:" & .ConveyorPos(k).AlignBasicOffsetY)
                                Debug.Print("ConveyorPos(" & k & ").AlignBasicOffsetZ:" & .ConveyorPos(k).AlignBasicOffsetZ)
                                Debug.Print("ConveyorPos(" & k & ").AlignmentData.Count:" & .ConveyorPos(k).AlignmentData.Count)
                                For l As Integer = 0 To .ConveyorPos(k).AlignmentData.Count - 1
                                    Debug.Print("ConveyorPos(" & k & ").AlignmentData(" & l & ").AlignOffsetX:" & .ConveyorPos(k).AlignmentData(l).AlignOffsetX)
                                    Debug.Print("ConveyorPos(" & k & ").AlignmentData(" & l & ").AlignOffsetY:" & .ConveyorPos(k).AlignmentData(l).AlignOffsetY)
                                    Debug.Print("ConveyorPos(" & k & ").AlignmentData(" & l & ").AlignPosX:" & .ConveyorPos(k).AlignmentData(l).AlignPosX)
                                    Debug.Print("ConveyorPos(" & k & ").AlignmentData(" & l & ").AlignPosY:" & .ConveyorPos(k).AlignmentData(l).AlignPosY)
                                    Debug.Print("ConveyorPos(" & k & ").AlignmentData(" & l & ").AlignPosZ:" & .ConveyorPos(k).AlignmentData(l).AlignPosZ)
                                    Debug.Print("ConveyorPos(" & k & ").AlignmentData(" & l & ").AlignRoation:" & .ConveyorPos(k).AlignmentData(l).AlignRoation)
                                    Debug.Print("ConveyorPos(" & k & ").AlignmentData(" & l & ").AlignScene:" & .ConveyorPos(k).AlignmentData(l).AlignScene)
                                    Debug.Print("ConveyorPos(" & k & ").AlignmentData(" & l & ").TeachPosX:" & .ConveyorPos(k).AlignmentData(l).TeachPosX)
                                    Debug.Print("ConveyorPos(" & k & ").AlignmentData(" & l & ").TeachPosY:" & .ConveyorPos(k).AlignmentData(l).TeachPosY)
                                    Debug.Print("ConveyorPos(" & k & ").AlignmentData(" & l & ").TeachPosZ:" & .ConveyorPos(k).AlignmentData(l).TeachPosZ)
                                Next
                                Debug.Print("ConveyorPos(" & k & ").LaserData.Count:" & .ConveyorPos(k).LaserData.Count)
                                For l As Integer = 0 To .ConveyorPos(k).LaserData.Count - 1
                                    Debug.Print("ConveyorPos(" & k & ").LaserData(" & l & ").LaserCalibrationCCDPosX:" & .ConveyorPos(k).LaserData(l).LaserCalibrationCCDPosX)
                                    Debug.Print("ConveyorPos(" & k & ").LaserData(" & l & ").LaserCalibrationCCDPosY:" & .ConveyorPos(k).LaserData(l).LaserCalibrationCCDPosY)
                                    Debug.Print("ConveyorPos(" & k & ").LaserData(" & l & ").LaserCalibrationCCDPosZ:" & .ConveyorPos(k).LaserData(l).LaserCalibrationCCDPosZ)
                                    Debug.Print("ConveyorPos(" & k & ").LaserData(" & l & ").LaserData1:" & .ConveyorPos(k).LaserData(l).LaserData1)
                                    Debug.Print("ConveyorPos(" & k & ").LaserData(" & l & ").LaserPositionX:" & .ConveyorPos(k).LaserData(l).LaserPositionX)
                                    Debug.Print("ConveyorPos(" & k & ").LaserData(" & l & ").LaserPositionY:" & .ConveyorPos(k).LaserData(l).LaserPositionY)
                                    Debug.Print("ConveyorPos(" & k & ").LaserData(" & l & ").LaserPositionZ:" & .ConveyorPos(k).LaserData(l).LaserPositionZ)
                                    Debug.Print("ConveyorPos(" & k & ").LaserData(" & l & ").TeachPosX:" & .ConveyorPos(k).LaserData(l).TeachPosX)
                                    Debug.Print("ConveyorPos(" & k & ").LaserData(" & l & ").TeachPosY:" & .ConveyorPos(k).LaserData(l).TeachPosY)
                                    Debug.Print("ConveyorPos(" & k & ").LaserData(" & l & ").TeachPosZ:" & .ConveyorPos(k).LaserData(l).TeachPosZ)
                                Next
                                Debug.Print("ConveyorPos(" & k & ").SkipMarkData.Count:" & .ConveyorPos(k).SkipMarkData.Count)
                                For l As Integer = 0 To .ConveyorPos(k).SkipMarkData.Count - 1
                                    Debug.Print("ConveyorPos(" & k & ").SkipMarkData(" & l & ").LaserCalibrationCCDPosX:" & .ConveyorPos(k).SkipMarkData(l).AlignOffsetX)
                                    Debug.Print("ConveyorPos(" & k & ").SkipMarkData(" & l & ").AlignOffsetY:" & .ConveyorPos(k).SkipMarkData(l).AlignOffsetY)
                                    Debug.Print("ConveyorPos(" & k & ").SkipMarkData(" & l & ").AlignPosX:" & .ConveyorPos(k).SkipMarkData(l).AlignPosX)
                                    Debug.Print("ConveyorPos(" & k & ").SkipMarkData(" & l & ").AlignPosY:" & .ConveyorPos(k).SkipMarkData(l).AlignPosY)
                                    Debug.Print("ConveyorPos(" & k & ").SkipMarkData(" & l & ").AlignPosZ:" & .ConveyorPos(k).SkipMarkData(l).AlignPosZ)
                                    Debug.Print("ConveyorPos(" & k & ").SkipMarkData(" & l & ").AlignRoation:" & .ConveyorPos(k).SkipMarkData(l).AlignRoation)
                                    Debug.Print("ConveyorPos(" & k & ").SkipMarkData(" & l & ").AlignScene:" & .ConveyorPos(k).SkipMarkData(l).AlignScene)
                                    Debug.Print("ConveyorPos(" & k & ").SkipMarkData(" & l & ").TeachPosX:" & .ConveyorPos(k).SkipMarkData(l).TeachPosX)
                                    Debug.Print("ConveyorPos(" & k & ").SkipMarkData(" & l & ").TeachPosY:" & .ConveyorPos(k).SkipMarkData(l).TeachPosY)
                                    Debug.Print("ConveyorPos(" & k & ").SkipMarkData(" & l & ").TeachPosZ:" & .ConveyorPos(k).SkipMarkData(l).TeachPosZ)
                                Next
                                Debug.Print("ConveyorPos(" & k & ").BasicPositionX:" & .ConveyorPos(k).BasicPositionX)
                                Debug.Print("ConveyorPos(" & k & ").BasicPositionY:" & .ConveyorPos(k).BasicPositionY)
                                Debug.Print("ConveyorPos(" & k & ").BasicPositionZ:" & .ConveyorPos(k).BasicPositionZ)
                                Debug.Print("ConveyorPos(" & k & ").ParentAlignAlignOffsetX:" & .ConveyorPos(k).ParentAlignAlignOffsetX)
                                Debug.Print("ConveyorPos(" & k & ").ParentAlignAlignOffsetY:" & .ConveyorPos(k).ParentAlignAlignOffsetY)
                                Debug.Print("ConveyorPos(" & k & ").ParentAlignAlignOffsetZ:" & .ConveyorPos(k).ParentAlignAlignOffsetZ)
                                Debug.Print("ConveyorPos(" & k & ").ParentAlignBasicOffsetX:" & .ConveyorPos(k).ParentAlignBasicOffsetX)
                                Debug.Print("ConveyorPos(" & k & ").ParentAlignBasicOffsetY:" & .ConveyorPos(k).ParentAlignBasicOffsetY)
                                Debug.Print("ConveyorPos(" & k & ").ParentAlignBasicOffsetZ:" & .ConveyorPos(k).ParentAlignBasicOffsetZ)
                                Debug.Print("ConveyorPos(" & k & ").RealBasicPosC:" & .ConveyorPos(k).RealBasicPosC)
                                Debug.Print("ConveyorPos(" & k & ").RealBasicPosX:" & .ConveyorPos(k).RealBasicPosX)
                                Debug.Print("ConveyorPos(" & k & ").RealBasicPosY:" & .ConveyorPos(k).RealBasicPosY)
                                Debug.Print("ConveyorPos(" & k & ").RealBasicPosZ:" & .ConveyorPos(k).RealBasicPosZ)
                                Debug.Print("ConveyorPos(" & k & ").TeachBasicPosX:" & .ConveyorPos(k).TeachBasicPosX)
                                Debug.Print("ConveyorPos(" & k & ").TeachBasicPosY:" & .ConveyorPos(k).TeachBasicPosY)
                                Debug.Print("ConveyorPos(" & k & ").TeachBasicPosZ:" & .ConveyorPos(k).TeachBasicPosZ)
                            Next

                            Debug.Print("AlignmentEnable: " & .AlignType)
                            Debug.Print("AlignmentEnable: " & .AlignType)
                        End With

                    Next
                End If

            Next
            For i As Integer = 0 To .NodeConnectList.Count - 1
                If .NodeConnectList(i) Is Nothing Then
                    Debug.Print("NodeConnectList(" & i & "): Nothing")
                Else
                    Debug.Print("NodeConnectList(" & i & ").Count" & .NodeConnectList(i).Count)
                    For j As Integer = 0 To .NodeConnectList(i).Count - 1
                        For k As Integer = 0 To .NodeConnectList(i)(j).Keys.Count - 1
                            Debug.Print("NodeConnectList(" & i & ")(" & j & ")(" & .NodeConnectList(i)(j).Keys(k) & ")" & .NodeConnectList(i)(j)(.NodeConnectList(i)(j).Keys(k)))
                        Next

                    Next
                End If
            Next
            For i As Integer = 0 To .NodeCount.Count - 1
                Debug.Print("NodeCount(" & i & "):" & .NodeCount(i))
            Next
            For i As Integer = 0 To .NodeToMap.Count - 1

                If .NodeToMap(i) Is Nothing Then
                    Debug.Print("NodeToMap(" & i & "): Nothing")
                Else
                    Debug.Print("NodeToMap(" & i & ").Count:" & .NodeToMap(i).Count)
                    For j As Integer = 0 To .NodeToMap(i).Count - 1
                        Debug.Print("NodeToMap(" & i & ")(:" & j & "):" & .NodeToMap(i)(j))
                    Next
                End If

            Next
            For i As Integer = 0 To .NotchDir.Count - 1
                Debug.Print("NotchDir(" & i & "):" & .NotchDir(i))
            Next
            For i As Integer = 0 To .Pattern.Count - 1
                If .Pattern(Pattern.Keys(i)) Is Nothing Then
                    Debug.Print("Pattern(" & Pattern.Keys(i) & "): Nothing")
                Else
                    Debug.Print("Pattern(" & .Pattern.Keys(i) & ").Diecount:" & .Pattern(Pattern.Keys(i)).Diecount)
                    Debug.Print("Pattern(" & .Pattern.Keys(i) & ").DispenseNode:" & .Pattern(Pattern.Keys(i)).DispenseNode)
                    Debug.Print("Pattern(" & .Pattern.Keys(i) & ").Enable:" & .Pattern(Pattern.Keys(i)).Enable)
                    Debug.Print("Pattern(" & .Pattern.Keys(i) & ").PatternCalibration.BasicCalibrationPosX:" & .Pattern(Pattern.Keys(i)).PatternCalibration.BasicCalibrationPosX)
                    Debug.Print("Pattern(" & .Pattern.Keys(i) & ").PatternCalibration.BasicCalibrationPosY:" & .Pattern(Pattern.Keys(i)).PatternCalibration.BasicCalibrationPosY)
                    Debug.Print("Pattern(" & .Pattern.Keys(i) & ").PatternCalibration.BasicCalibrationPosZ:" & .Pattern(Pattern.Keys(i)).PatternCalibration.BasicCalibrationPosZ)
                    Debug.Print("Pattern(" & .Pattern.Keys(i) & ").PatternCalibration.PatternOffsetX:" & .Pattern(Pattern.Keys(i)).PatternCalibration.PatternOffsetX)
                    Debug.Print("Pattern(" & .Pattern.Keys(i) & ").PatternCalibration.PatternOffsetY:" & .Pattern(Pattern.Keys(i)).PatternCalibration.PatternOffsetY)
                    Debug.Print("Pattern(" & .Pattern.Keys(i) & ").PatternCalibration.PatternRotation:" & .Pattern(Pattern.Keys(i)).PatternCalibration.PatternRotation)
                End If
            Next
            Debug.Print("PatternCount:" & .PatternCount)
            For i As Integer = 0 To .PatternName.Count - 1
                Debug.Print("PatternName(" & i & "):" & .PatternName(i))
            Next
            Debug.Print("ProductType:" & .ProductType)
            For i As Integer = 0 To .ProductTypeName.Count - 1
                Debug.Print("ProductTypeName:" & .ProductTypeName(i))
            Next
            For i As Integer = 0 To .RecipeSceneNamelist.Count - 1
                Debug.Print("RecipeSceneNamelist(" & i & "):" & .RecipeSceneNamelist(i))
            Next
            Debug.Print("RunType:" & .RunType)
            For i As Integer = 0 To .sBinData.Count - 1

                Debug.Print("sBinData(" & .sBinData.Keys(i) & ").BinName" & .sBinData(.sBinData.Keys(i)).BinName)
                Debug.Print("sBinData(" & .sBinData.Keys(i) & ").Disable" & .sBinData(.sBinData.Keys(i)).Disable)
                Debug.Print("sBinData(" & .sBinData.Keys(i) & ").PatternName" & .sBinData(.sBinData.Keys(i)).PatternName)
                Debug.Print("sBinData(" & .sBinData.Keys(i) & ").Status" & .sBinData(.sBinData.Keys(i)).Status)
            Next

            Debug.Print("Scale.CountInPcs:" & .Scale.CountInPcs)
            Debug.Print("Scale.TimerInSec:" & .Scale.TimerInSec)
            Debug.Print("Scale.WeightControlType:" & .Scale.WeightControlType)

            Debug.Print("SearchTree.Count:" & .SearchTree.Count)
            Debug.Print("SearchType:" & .SearchType)
            For i As Integer = 0 To .StageNodeID.Count - 1
                Debug.Print("StageNodeID(" & i & "):" & .StageNodeID(i))
            Next
            For i As Integer = 0 To .StageParts.Count - 1
                If .StageParts(i) Is Nothing Then
                    Debug.Print("StageParts(" & i & "): Nothing")
                Else
                    Debug.Print("StageParts(" & i & ").AverageWeightPerDot.Count:" & .StageParts(i).AverageWeightPerDot.Count)
                    For j As Integer = 0 To .StageParts(i).AverageWeightPerDot.Count - 1
                        Debug.Print("StageParts(" & i & ").AverageWeightPerDot(" & j & "):" & .StageParts(i).AverageWeightPerDot(j))
                    Next
                    Debug.Print("StageParts(" & i & ").FlowRateName.Count:" & .StageParts(i).FlowRateName.Count)
                    For j As Integer = 0 To .StageParts(i).FlowRateName.Count - 1
                        Debug.Print("StageParts(" & i & ").FlowRateName(" & j & "):" & .StageParts(i).FlowRateName(j))
                    Next
                    Debug.Print("StageParts(" & i & ").NozzleTemperature.Count:" & .StageParts(i).NozzleTemperature.Count)
                    For j As Integer = 0 To .StageParts(i).NozzleTemperature.Count - 1
                        Debug.Print("StageParts(" & i & ").NozzleTemperature(" & j & "):" & .StageParts(i).NozzleTemperature(j))
                    Next
                    Debug.Print("StageParts(" & i & ").PasteName.Count:" & .StageParts(i).PasteName.Count)
                    For j As Integer = 0 To .StageParts(i).PasteName.Count - 1
                        Debug.Print("StageParts(" & i & ").PasteName(" & j & "):" & .StageParts(i).PasteName(j))
                    Next
                    Debug.Print("StageParts(" & i & ").PiezoTemperature.Count:" & .StageParts(i).PiezoTemperature.Count)
                    For j As Integer = 0 To .StageParts(i).PiezoTemperature.Count - 1
                        Debug.Print("StageParts(" & i & ").PiezoTemperature(" & j & "):" & .StageParts(i).PiezoTemperature(j))
                    Next
                    Debug.Print("StageParts(" & i & ").PurgeName.Count:" & .StageParts(i).PurgeName.Count)
                    For j As Integer = 0 To .StageParts(i).PurgeName.Count - 1
                        Debug.Print("StageParts(" & i & ").PurgeName(" & j & "):" & .StageParts(i).PurgeName(j))
                    Next
                    Debug.Print("StageParts(" & i & ").SyringePressure.Count:" & .StageParts(i).SyringePressure.Count)
                    For j As Integer = 0 To .StageParts(i).SyringePressure.Count - 1
                        Debug.Print("StageParts(" & i & ").SyringePressure(" & j & "):" & .StageParts(i).SyringePressure(j))
                    Next
                    Debug.Print("StageParts(" & i & ").ValveName.Count:" & .StageParts(i).ValveName.Count)
                    For j As Integer = 0 To .StageParts(i).ValveName.Count - 1
                        Debug.Print("StageParts(" & i & ").ValveName(" & j & "):" & .StageParts(i).ValveName(j))
                    Next
                    Debug.Print("StageParts(" & i & ").ValvePressure.Count:" & .StageParts(i).ValvePressure.Count)
                    For j As Integer = 0 To .StageParts(i).ValvePressure.Count - 1
                        Debug.Print("StageParts(" & i & ").ValvePressure(" & j & "):" & .StageParts(i).ValvePressure(j))
                    Next
                    Debug.Print("StageParts(" & i & ").ValveShiftX.Count:" & .StageParts(i).ValveShiftX.Count)
                    For j As Integer = 0 To .StageParts(i).ValveShiftX.Count - 1
                        Debug.Print("StageParts(" & i & ").ValveShiftX(" & j & "):" & .StageParts(i).ValveShiftX(j))
                    Next
                    Debug.Print("StageParts(" & i & ").ValveShiftY.Count:" & .StageParts(i).ValveShiftY.Count)
                    For j As Integer = 0 To .StageParts(i).ValveShiftY.Count - 1
                        Debug.Print("StageParts(" & i & ").ValveShiftY(" & j & "):" & .StageParts(i).ValveShiftY(j))
                    Next
                    Debug.Print("StageParts(" & i & ").ValveShiftZ.Count:" & .StageParts(i).ValveShiftZ.Count)
                    For j As Integer = 0 To .StageParts(i).ValveShiftZ.Count - 1
                        Debug.Print("StageParts(" & i & ").ValveShiftZ(" & j & "):" & .StageParts(i).ValveShiftZ(j))
                    Next
                End If
            Next
            Debug.Print("strFileName:" & .strFileName)
            Debug.Print("strName:" & .strName)
            Debug.Print("TempName:" & .TempName)
            Debug.Print("TempParam.Count:" & .TempParam.Count)
            For i As Integer = 0 To .TempParam.Count - 1
                Debug.Print("TempParam(" & i & ").Enabled:" & .TempParam(i).Enabled)
                Debug.Print("TempParam(" & i & ").PVOS:" & .TempParam(i).PVOS)
                Debug.Print("TempParam(" & i & ").SetValue:" & .TempParam(i).SetValue)
            Next
        End With

    End Sub
    Public Function Clone() As CRecipe
        Dim mTemp As New CRecipe(mStageCount, mStageValveCount)
        With mTemp
            .Alignable = Me.Alignable
            .BinNumber = Me.BinNumber
            .BypassCCDResult = Me.BypassCCDResult
            .BypassLaserResult = Me.BypassLaserResult
            .BypassRotationCorrection = Me.BypassRotationCorrection
            .CCDFixModel = Me.CCDFixModel
            For i As Integer = 0 To Me.CCDFixTraversal.Count - 1
                If .CCDFixTraversal(i) Is Nothing Then
                Else
                    .CCDFixTraversal(i).Clear()
                    For j As Integer = 0 To Me.CCDFixTraversal(i).Count - 1
                        .CCDFixTraversal(i).Add(Me.CCDFixTraversal(i)(j))
                    Next
                End If
            Next
           
            .CCDOnFlyDelayTime = Me.CCDOnFlyDelayTime
            .CCDOnFlySpeed = Me.CCDOnFlySpeed
            .ChuckMapType = Me.ChuckMapType
            .ConveyorSpeed = Me.ConveyorSpeed
            .dblProductTypeTotalNum = Me.dblProductTypeTotalNum

            For i As Integer = 0 To Me.DispenseTraversal.Count - 1
                If .DispenseTraversal(i) Is Nothing Then
                Else
                    .DispenseTraversal(i).Clear()
                    For j As Integer = 0 To Me.DispenseTraversal(i).Count - 1
                        .DispenseTraversal(i).Add(Me.DispenseTraversal(i)(j))
                    Next
                End If
            Next
            .DispHistory = Me.DispHistory
            .DispTimeModel = Me.DispTimeModel
            .Editable = Me.Editable
            .FollowMode = Me.FollowMode
            .GPSetRunMode = Me.GPSetRunMode
            .IsJustOneRun = Me.IsJustOneRun
            .IsRerunRun = Me.IsRerunRun
            .IsStopAlignNG = Me.IsStopAlignNG
            .LaserFixMode = Me.LaserFixMode
            .LaserMode = Me.LaserMode
            .LaserRunMode = Me.LaserRunMode
          
            For i As Integer = 0 To Me.LaserTraversal.Count - 1
                If .LaserTraversal(i) Is Nothing Then
                Else
                    .LaserTraversal(i).Clear()
                    For j As Integer = 0 To Me.LaserTraversal(i).Count - 1
                        .LaserTraversal(i).Add(Me.LaserTraversal(i)(j))
                    Next
                End If
            Next
            .LeadAngle = Me.LeadAngle
            
            For i As Integer = 0 To Me.Node.Count - 1
                If Me.Node(i) IsNot Nothing Then
                    For j As Integer = 0 To Me.Node(i).Keys.Count - 1
                        If Me.Node(i).Keys(j) IsNot Nothing Then
                            .Node(i).Clear()
                            .Node(i).Add(Me.Node(i).Keys(j), Me.Node(i)(Me.Node(i).Keys(j)).Clone)
                        End If
                    Next
                End If
            Next

            .NodeConnectList = Me.NodeConnectList.Clone
            .NodeCount = Me.NodeCount
            .NodeToMap = Me.NodeToMap.Clone()
            .NotchDir = Me.NotchDir.Clone()
            .Pattern.Clear()
            For i As Integer = 0 To Me.Pattern.Count - 1
                .Pattern.Add(Me.Pattern.Keys(i), Me.Pattern.Values(i).Clone)
            Next
            .PatternCount = Me.PatternCount
            .PatternName.Clear()
            For i As Integer = 0 To Me.PatternName.Count - 1
                .PatternName.Add(Me.PatternName(i).Clone)
            Next
            .ProductType = Me.ProductType
            .ProductTypeName = Me.ProductTypeName.Clone
            .RecipeSceneNamelist.Clear()
            For i As Integer = 0 To Me.RecipeSceneNamelist.Count - 1
                .RecipeSceneNamelist.Add(Me.RecipeSceneNamelist(i))
            Next
            .RunType = Me.RunType
            .sBinData.Clear()
            For i As Integer = 0 To Me.sBinData.Count - 1
                .sBinData.Add(Me.sBinData.Keys(i), sBinData.Values(i))
            Next
            .Scale = Me.Scale.Clone
            
            For i As Integer = 0 To Me.ScanTraversal.Count - 1
                If .ScanTraversal(i) Is Nothing Then
                Else
                    .ScanTraversal(i).Clear()
                    For j As Integer = 0 To Me.ScanTraversal(i).Count - 1
                        .ScanTraversal(i).Add(Me.ScanTraversal(i)(j))
                    Next
                End If
            Next
            .SearchTree.Clear()
            For i As Integer = 0 To Me.SearchTree.Count - 1
                .SearchTree.Add(Me.SearchTree(i).Clone())
            Next
            .SearchType = Me.SearchType
            .StageNodeID = Me.StageNodeID.Clone()
            For i As Integer = 0 To Me.StageParts.Count - 1
                If Me.StageParts(i) IsNot Nothing Then
                    .StageParts(i) = Me.StageParts(i).Clone
                End If
            Next
            .strFileName = Me.strFileName.Clone
            .strName = Me.strName.Clone
            .TempName = Me.TempName.Clone
            .TempParam = Me.TempParam.Clone
        End With
        Return mTemp
    End Function


    Function UpdateOriginDataConveyorParentRelation(ByVal stageNo As Integer, ByVal NodeID As String, ByVal mConveyorNo As eConveyor) As Boolean
        Dim levelNo As Integer
        With Me.Node(stageNo)(NodeID).ConveyorPos(mConveyorNo)
            If GetNodeLevel(NodeID, levelNo) = True Then
                If levelNo > 1 Then 'Soni + 2016.09.14 計算相對偏移量
                    Dim parentNodeID As String = "" 'NodeID.Substring(0, NodeID.Length - 4) '取父節點名稱
                    If Me.GetParentNodeID(NodeID, parentNodeID) = True Then 'Soni + 2017.03.05 修正名稱過長錯誤
                        .ParentAlignAlignOffsetX = Me.Node(stageNo)(parentNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosX - .AlignmentData(0).AlignPosX
                        .ParentAlignAlignOffsetY = Me.Node(stageNo)(parentNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosY - .AlignmentData(0).AlignPosY
                        .ParentAlignAlignOffsetZ = Me.Node(stageNo)(parentNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignPosZ - .AlignmentData(0).AlignPosZ

                        .AlignBasicOffsetX = .AlignmentData(0).AlignPosX - .BasicPositionX
                        .AlignBasicOffsetY = .AlignmentData(0).AlignPosY - .BasicPositionY
                        .AlignBasicOffsetZ = .AlignmentData(0).AlignPosZ - .BasicPositionZ
                    End If

                Else
                    .ParentAlignAlignOffsetX = -.AlignmentData(0).AlignPosX
                    .ParentAlignAlignOffsetY = -.AlignmentData(0).AlignPosY
                    .ParentAlignAlignOffsetZ = -.AlignmentData(0).AlignPosZ

                    .AlignBasicOffsetX = .AlignmentData(0).AlignPosX - .BasicPositionX
                    .AlignBasicOffsetY = .AlignmentData(0).AlignPosY - .BasicPositionY
                    .AlignBasicOffsetZ = .AlignmentData(0).AlignPosZ - .BasicPositionZ
                End If
            End If

        End With

        Return True
    End Function

    Function UpdateOriginDataConveyorHeight(ByVal stageNo As Integer, ByVal NodeID As String, ByVal mConveyorNo As eConveyor, ByVal alignIdx As Integer, ByVal absIdxX As Integer, ByVal absIdxY As Integer, ByRef mMultiArrayAdapter As CMultiArrayAdapter) As Boolean
        With Me.Node(stageNo)(NodeID).ConveyorPos(mConveyorNo).LaserData(alignIdx)
            .LaserPositionX = .TeachPosX - mMultiArrayAdapter.GetMemoryPosX(absIdxX, absIdxY)
            .LaserPositionY = .TeachPosY - mMultiArrayAdapter.GetMemoryPosY(absIdxX, absIdxY)
            .LaserPositionZ = .TeachPosZ
        End With
        Return True
    End Function
    Function UpdateOriginDataConveyorAlign(ByVal stageNo As Integer, ByVal NodeID As String, ByVal mConveyorNo As eConveyor, ByVal alignIdx As Integer, ByVal absIdxX As Integer, ByVal absIdxY As Integer, ByRef mMultiArrayAdapter As CMultiArrayAdapter) As Boolean
        With Me.Node(stageNo)(NodeID).ConveyorPos(mConveyorNo).AlignmentData(alignIdx)
            .AlignPosX = .TeachPosX - mMultiArrayAdapter.GetMemoryPosX(absIdxX, absIdxY)
            .AlignPosY = .TeachPosY - mMultiArrayAdapter.GetMemoryPosY(absIdxX, absIdxY)
            .AlignPosZ = .TeachPosZ
            .AlignOffsetX = 0
            .AlignOffsetY = 0
            .AlignRoation = 0
        End With
        Return True
    End Function

    Function UpdateOriginDataConveyorSkip1(ByVal stageNo As Integer, ByVal NodeID As String, ByVal mConveyorNo As eConveyor, ByVal absIdxX As Integer, ByVal absIdxY As Integer, ByRef mMultiArrayAdapter As CMultiArrayAdapter) As Boolean
        With Me.Node(stageNo)(NodeID).ConveyorPos(mConveyorNo).SkipMarkData(0)
            .AlignPosX = .TeachPosX - mMultiArrayAdapter.GetMemoryPosX(absIdxX, absIdxY)
            .AlignPosY = .TeachPosY - mMultiArrayAdapter.GetMemoryPosY(absIdxX, absIdxY)
            .AlignPosZ = .TeachPosZ 'Soni + 2016.09.26 補給流程用的Z軸高度
            .AlignOffsetX = 0
            .AlignOffsetY = 0
        End With
        Return True
    End Function

    ''' <summary>更新指定Conveyor的基準點與父節點關係
    ''' </summary>
    ''' <param name="conveyorNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateOriginDataConveyorBasicParentRelation(ByVal stageNo As Integer, ByVal NodeID As String, ByVal conveyorNo As eConveyor) As Boolean
        Dim levelNo As Integer
        With Me.Node(stageNo)(NodeID).ConveyorPos(conveyorNo)
            If GetNodeLevel(NodeID, levelNo) = True Then
                If levelNo > 1 Then 'Soni + 2016.09.14 計算相對偏移量
                    Dim parentNodeID As String = "" 'NodeID.Substring(0, NodeID.Length - 4) '取父節點名稱
                    If Me.GetParentNodeID(NodeID, parentNodeID) = True Then 'Soni + 2017.03.05 修正名稱過長錯誤
                        '[Note]:Parent Node AlignmentEnable=False，則不能取AlignPos
                        If Me.Node(stageNo)(parentNodeID).AlignmentEnable = True Then
                            .ParentAlignBasicOffsetX = Me.Node(stageNo)(parentNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX - .BasicPositionX
                            .ParentAlignBasicOffsetY = Me.Node(stageNo)(parentNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY - .BasicPositionY
                            .ParentAlignBasicOffsetZ = Me.Node(stageNo)(parentNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ - .BasicPositionZ
                            .AlignBasicOffsetX = .AlignmentData(0).AlignPosX - .BasicPositionX
                            .AlignBasicOffsetY = .AlignmentData(0).AlignPosY - .BasicPositionY
                            .AlignBasicOffsetZ = .AlignmentData(0).AlignPosZ - .BasicPositionZ
                        Else
                            .ParentAlignBasicOffsetX = -.BasicPositionX
                            .ParentAlignBasicOffsetY = -.BasicPositionY
                            .ParentAlignBasicOffsetZ = -.BasicPositionZ
                            .AlignBasicOffsetX = -.BasicPositionX
                            .AlignBasicOffsetY = -.BasicPositionY
                            .AlignBasicOffsetZ = -.BasicPositionZ
                        End If
                    End If
                Else
                    '[Note]:Node AlignmentEnable=False，則不能取AlignPos
                    If Me.Node(stageNo)(NodeID).AlignmentEnable = True Then
                        .ParentAlignBasicOffsetX = -.BasicPositionX
                        .ParentAlignBasicOffsetY = -.BasicPositionY
                        .ParentAlignBasicOffsetZ = -.BasicPositionZ
                        .AlignBasicOffsetX = .AlignmentData(0).AlignPosX - .BasicPositionX
                        .AlignBasicOffsetY = .AlignmentData(0).AlignPosY - .BasicPositionY
                        .AlignBasicOffsetZ = .AlignmentData(0).AlignPosZ - .BasicPositionZ
                    Else
                        .ParentAlignBasicOffsetX = -.BasicPositionX
                        .ParentAlignBasicOffsetY = -.BasicPositionY
                        .ParentAlignBasicOffsetZ = -.BasicPositionZ
                        .AlignBasicOffsetX = -.BasicPositionX
                        .AlignBasicOffsetY = -.BasicPositionY
                        .AlignBasicOffsetZ = -.BasicPositionZ
                    End If
                End If
            End If
        End With
        Return True
    End Function

    ''' <summary>由教導基準點更新(0,0)基準點
    ''' </summary>
    ''' <param name="mConveyorNo"></param>
    ''' <param name="absIdxX"></param>
    ''' <param name="absIdxY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateOriginDataBasic(ByVal stageNo As Integer, ByVal NodeID As String, ByVal mConveyorNo As eConveyor, ByVal absIdxX As Integer, ByVal absIdxY As Integer, ByRef mMultiArrayAdapter As CMultiArrayAdapter) As Boolean
        With Me.Node(stageNo)(NodeID).ConveyorPos(mConveyorNo)
            .BasicPositionX = .TeachBasicPosX - mMultiArrayAdapter.GetMemoryPosX(absIdxX, absIdxY)
            .BasicPositionY = .TeachBasicPosY - mMultiArrayAdapter.GetMemoryPosY(absIdxX, absIdxY)
            .BasicPositionZ = .TeachBasicPosZ
        End With
        Return True
    End Function

    ''' <summary>更新陣列(0,0)的資料
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateOriginData(ByVal stageNo As Integer, ByVal NodeID As String, ByVal absIdxX As Integer, ByVal absIdxY As Integer, ByRef mMultiArrayAdapter As CMultiArrayAdapter) As Boolean
        'Conveyor1資料
        UpdateOriginDataConveyorAlign(stageNo, NodeID, eConveyor.ConveyorNo1, 0, absIdxX, absIdxY, mMultiArrayAdapter)
        UpdateOriginDataConveyorAlign(stageNo, NodeID, eConveyor.ConveyorNo1, 1, absIdxX, absIdxY, mMultiArrayAdapter)
        UpdateOriginDataConveyorAlign(stageNo, NodeID, eConveyor.ConveyorNo1, 2, absIdxX, absIdxY, mMultiArrayAdapter)
        UpdateOriginDataConveyorSkip1(stageNo, NodeID, eConveyor.ConveyorNo1, absIdxX, absIdxY, mMultiArrayAdapter)
        UpdateOriginDataBasic(stageNo, NodeID, eConveyor.ConveyorNo1, absIdxX, absIdxY, mMultiArrayAdapter)
        '先算完基本資料, 再推算父子關係
        UpdateOriginDataConveyorParentRelation(stageNo, NodeID, eConveyor.ConveyorNo1)
        UpdateOriginDataConveyorBasicParentRelation(stageNo, NodeID, eConveyor.ConveyorNo1)
        'Conveyor2資料
        UpdateOriginDataConveyorAlign(stageNo, NodeID, eConveyor.ConveyorNo2, 0, absIdxX, absIdxY, mMultiArrayAdapter)
        UpdateOriginDataConveyorAlign(stageNo, NodeID, eConveyor.ConveyorNo2, 1, absIdxX, absIdxY, mMultiArrayAdapter)
        UpdateOriginDataConveyorAlign(stageNo, NodeID, eConveyor.ConveyorNo2, 2, absIdxX, absIdxY, mMultiArrayAdapter)
        UpdateOriginDataConveyorSkip1(stageNo, NodeID, eConveyor.ConveyorNo2, absIdxX, absIdxY, mMultiArrayAdapter)
        UpdateOriginDataBasic(stageNo, NodeID, eConveyor.ConveyorNo2, absIdxX, absIdxY, mMultiArrayAdapter)
        '先算完基本資料, 再推算父子關係
        UpdateOriginDataConveyorParentRelation(stageNo, NodeID, eConveyor.ConveyorNo2)
        UpdateOriginDataConveyorBasicParentRelation(stageNo, NodeID, eConveyor.ConveyorNo2)
        Return True
    End Function

 
End Class

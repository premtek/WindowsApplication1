﻿
Imports ProjectCore
Imports ProjectIO
Imports ProjectRecipe
Imports ProjectMotion
Imports ProjectAOI
Imports ProjectTriggerBoard
Imports System.Math

Module MFunctionCCDFix


#Region "區域變數"
    Private mCCDFixProtect(enmStage.Max) As sProtectData                                  '[紀錄各自的相關座標]
    Private mTriggerCmdFailCount(enmStage.Max) As Integer                               '[紀錄資料傳輸異常次數]
    Private mTriggerType(enmStage.Max) As enmTriggerDispType
    Private mDispType(enmStage.Max) As enmTriggerDispType
    Private mMotionDispPathRegister(enmStage.Max) As List(Of sPatternPath)
    Private mTriggerDispPathRegister(enmStage.Max) As List(Of sPatternPath)
    Private mMaxExtendDistance(enmStage.Max) As Decimal                                 '[記錄最長的延伸距離]
    Private mMaxBlendTime(enmStage.Max) As Decimal                                      '[記錄最長的BlendTime 單位:s]
    Private mTriggerLCmdParameter(enmStage.Max) As sTriggerVisionCmdParam
    Private mTriggerGCmdParameter(enmStage.Max) As sTriggerGCmdParam                    '[塞空資料]
    Private m1stPath(enmStage.Max) As sDot3DPath                                        '[紀錄起始點座標]
    Private mlastPath(enmStage.Max) As sDot3DPath                                       '[紀錄終點座標]
    Private mTriggerPathMenory(enmStage.Max) As sPatternPath                             '[紀錄Trigger Path(給Dot用的)]
    Private mTriggerFixPathIndex(enmStage.Max) As sIndex                               '[紀錄執行到哪一個]

    Private mCCDTimeStopWatch(enmStage.Max) As Stopwatch
#End Region

#Region "Enum、Structure"


    ''' <summary>[定位資料接收狀態]</summary>
    ''' <remarks></remarks>
    Private Enum eCCDAlginStatus
        ''' <summary>[資料完整收取]</summary>
        ''' <remarks></remarks>
        eOK = 0
        ''' <summary>[定位失敗停止(需要配合外不需要定位異常時要停機之條件)]</summary>
        ''' <remarks></remarks>
        eNGStop = 1
        ''' <summary>[等待資料接收(忙線中)]</summary>
        ''' <remarks></remarks>
        eBusy = 2
    End Enum

    ''' <summary>[Axis對應的編號]</summary>
    ''' <remarks></remarks>
    Private Structure sAxis
        ''' <summary>[X軸]</summary>
        ''' <remarks></remarks>
        Shared AxisX As Integer = 0
        ''' <summary>[Y軸]</summary>
        ''' <remarks></remarks>
        Shared AxisY As Integer = 1
        ''' <summary>[Z軸]</summary>
        ''' <remarks></remarks>
        Shared AxisZ As Integer = 2
        ''' <summary>[A軸]</summary>
        ''' <remarks></remarks>
        Shared AxisA As Integer = 3
        ''' <summary>[B軸]</summary>
        ''' <remarks></remarks>
        Shared AxisB As Integer = 4
        ''' <summary>[C軸]</summary>
        ''' <remarks></remarks>
        Shared AxisC As Integer = 5
    End Structure

    ''' <summary>[定位點Index]</summary>
    ''' <remarks></remarks>
    Public Enum eAlignIndex
        ''' <summary>[沒有]</summary>
        ''' <remarks></remarks>
        None = -1
        ''' <summary>[第1個定位點]</summary>
        ''' <remarks></remarks>
        No1 = 0
        ''' <summary>[第2個定位點]</summary>
        ''' <remarks></remarks>
        No2 = 1
        ''' <summary>[第3個定位點]</summary>
        ''' <remarks></remarks>
        No3 = 2
        ''' <remarks></remarks>
        Max = No3
        ''' <summary>[不生產點]</summary>
        ''' <remarks></remarks>
        SkipMark = 3
    End Enum

    ''' <summary>[定位型態:不生產點 與 定位點]</summary>
    ''' <remarks></remarks>
    Public Enum eCCDAlignType
        None = -1
        ''' <summary>[不生產點]</summary>
        ''' <remarks></remarks>
        eSkipMark = 0
        ''' <summary>[定位點]</summary>
        ''' <remarks></remarks>
        eAlign = 1

    End Enum

    Public Structure SNodeAlignScene
        ''' <summary>[Node名稱]</summary>
        ''' <remarks></remarks>
        Public NodeName As String
        ''' <summary>[定位場景名稱]</summary>
        ''' <remarks></remarks>
        Public AlignScene As String
        ''' <summary>[哪一層的Node]</summary>
        ''' <remarks></remarks>
        Public NodeLevel As Integer
        ''' <summary>[該Node是否省略定位]</summary>
        ''' <remarks></remarks>
        Public IsEnableAlignScene As Boolean
        ''' <summary>[該Node是否使用不生產點]</summary>
        ''' <remarks></remarks>
        Public IsEnableSkipMarkScene As Boolean
        ''' <summary>[第幾個定位場景]</summary>
        ''' <remarks></remarks>
        Public AlignIndex As eAlignIndex
        ''' <summary> 定位型態 </summary>
        ''' <remarks></remarks>
        Public AlignType As eCCDAlignType
    End Structure

    ''' <summary>[CCD Fix相關資訊]</summary>
    ''' <remarks></remarks>
    Private Structure sCCDFixData
        ''' <summary>[陣列(X)]</summary>
        ''' <remarks></remarks>
        Friend IndexX As Integer
        ''' <summary>[陣列(Y)]</summary>
        ''' <remarks></remarks>
        Friend IndexY As Integer
        ''' <summary>[定位場景名稱]</summary>
        ''' <remarks></remarks>
        Friend AlignScene As String
        ''' <summary>[第幾個定位場景]</summary>
        ''' <remarks></remarks>
        Friend AlignIndex As eAlignIndex
        ''' <summary>[Node名稱]</summary>
        ''' <remarks></remarks>
        Friend NodeName As String
        ''' <summary>[哪一層的Node]</summary>
        ''' <remarks></remarks>
        Friend NodeLevel As Integer
    End Structure


    ''' <summary>[塞路徑規劃]</summary>
    ''' <remarks></remarks>
    Private Structure sCCDFixPath
        ''' <summary>[定位點座標(X)]</summary>
        ''' <remarks></remarks>
        Friend PosX As Decimal
        ''' <summary>[定位點座標(Y)]</summary>
        ''' <remarks></remarks>
        Friend PosY As Decimal
        ''' <summary>[定位點座標(Z)]</summary>
        ''' <remarks></remarks>
        Friend PosZ As Decimal
        ''' <summary>[定位場景名稱]</summary>
        ''' <remarks></remarks>
        Friend AlignScene As String
        ''' <summary>[陣列(X)]</summary>
        ''' <remarks></remarks>
        Friend IndexX As Integer
        ''' <summary>[陣列(Y)]</summary>
        ''' <remarks></remarks>
        Friend IndexY As Integer
        ''' <summary>[Node名稱]</summary>
        ''' <remarks></remarks>
        Friend NodeName As String
        ''' <summary>[第幾個定位場景]</summary>
        ''' <remarks></remarks>
        Friend AlignIndex As eAlignIndex
    End Structure

    ''' <summary>[助跑資訊]</summary>
    ''' <remarks></remarks>
    Private Structure sExtendParam
        ''' <summary>[速度(mm/s)]</summary>
        ''' <remarks></remarks>
        Friend Velocity As Decimal
        ''' <summary>[助跑時間(加速時間)(s)]</summary>
        ''' <remarks></remarks>
        Friend Time As Decimal
        ''' <summary>[助跑距離(加速距離)(mm)]</summary>
        ''' <remarks></remarks>
        Friend Distance As Decimal
    End Structure

    ''' <summary>[紀錄做到哪個]</summary>
    ''' <remarks></remarks>
    Private Structure sIndex
        ''' <summary>[從哪個開始]</summary>
        ''' <remarks></remarks>
        Friend Start As Integer
        ''' <summary>[做到哪個結束]</summary>
        ''' <remarks></remarks>
        Friend Ending As Integer
        ''' <summary>[已經完成到哪個]</summary>
        ''' <remarks></remarks>
        Friend Done As Integer
    End Structure

    ''' <summary>[CCD Fix相關參數]</summary>
    ''' <remarks></remarks>
    Private Structure sCCDFixParam
        ''' <summary>[群組加速度]</summary>
        ''' <remarks></remarks>
        Friend Acc As Decimal
        ''' <summary>[群組減速度]</summary>
        ''' <remarks></remarks>
        Friend Dec As Decimal
        ''' <summary>[群組最大速度(用來卡上限用的)]</summary>
        ''' <remarks></remarks>
        Friend VelHigh As Decimal
        ''' <summary>[導角資訊](飛拍連接路徑使用)</summary>
        ''' <remarks></remarks>
        Public LeadAngle As sLeadAngle
    End Structure


    ''' <summary>[前後二組路徑的樣式]</summary>
    ''' <remarks></remarks>
    Private Enum eTwoPathModel
        ''' <summary>[Dots Dots]</summary>
        ''' <remarks></remarks>
        DotsDots = 0
        ''' <summary>[Dots Line]</summary>
        ''' <remarks></remarks>
        DotsLine = 1
        ''' <summary>[DotsArc]</summary>
        ''' <remarks></remarks>
        DotsArc = 2
        ''' <summary>[Line Dots]</summary>
        ''' <remarks></remarks>
        LineDots = 3
        ''' <summary>[Line Line]</summary>
        ''' <remarks></remarks>
        LineLine = 4
        ''' <summary>[Line Arc]</summary>
        ''' <remarks></remarks>
        LineArc = 5
        ''' <summary>[Arc Dots]</summary>
        ''' <remarks></remarks>
        ArcDots = 6
        ''' <summary>[Arc Line]</summary>
        ''' <remarks></remarks>
        ArcLine = 7
        ''' <summary>[Arc Arc]</summary>
        ''' <remarks></remarks>
        ArcArc = 8
        ''' <summary>[非預期樣式]</summary>
        ''' <remarks></remarks>
        None = 9
    End Enum


    ''' <summary>[二條線的關係]</summary>
    ''' <remarks></remarks>
    Private Enum TwoPathRelationship
        ''' <summary>[非平行線關係]</summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>[同一直線且同一方向]</summary>
        ''' <remarks></remarks>
        LineAndDirectionIsTheSame = 1
        ''' <summary>[同一直線但不同方向]</summary>
        ''' <remarks></remarks>
        LineIsTheSameButDirectionIsNot = 2
        ''' <summary>[只是平行但不在同一條直上且方向相同]</summary>
        ''' <remarks></remarks>
        ParallelAndDirectionIsSame = 3
        ''' <summary>[只是平行但不在同一條直上且方向不同]</summary>
        ''' <remarks></remarks>
        ParallelAndDirectionIsNonSame = 4
    End Enum

    Public Structure sLeadAngle
        ''' <summary>[角度]</summary>
        ''' <remarks></remarks>
        Public Degress As Decimal
        ''' <summary>[長度]</summary>
        ''' <remarks></remarks>
        Public Distance As Decimal
    End Structure



#End Region




    ''' <summary>
    ''' [Node內CCD定位關係的清單]
    ''' </summary>
    ''' <remarks></remarks>
    Public CCDFixRelationshipList(enmStage.Max) As List(Of SNodeAlignScene)


    ''' <summary>[動作基本參數]</summary>
    ''' <remarks></remarks>
    Private Class sCCDFixSysParam
        ''' <summary>[系統執行步驟]</summary>
        ''' <remarks></remarks>
        Friend SysNum As Integer
        ''' <summary>[命令執行狀態]</summary>
        ''' <remarks></remarks>
        Friend RunStatus As enmRunStatus
        ''' <summary>[起始步驟]</summary>
        ''' <remarks></remarks>
        Friend Const SysLoopStart As Integer = 1000
        ''' <summary>[搭配平台索引]</summary>
        ''' <remarks></remarks>
        Friend StageNo As Integer
        ''' <summary>[搭配CCD索引]</summary>
        ''' <remarks></remarks>
        Friend CCDNo As Integer
        ''' <summary>[等效X軸]</summary>
        ''' <remarks></remarks> 
        Friend AxisX As Integer
        ''' <summary>[等效Y軸]</summary>
        ''' <remarks></remarks>
        Friend AxisY As Integer
        ''' <summary>[等效Z軸]</summary>
        ''' <remarks></remarks>
        Friend AxisZ As Integer
        ''' <summary>[等效A軸]</summary>
        ''' <remarks></remarks>
        Friend AxisA As Integer
        ''' <summary>[等效B軸]</summary>
        ''' <remarks></remarks>
        Friend AxisB As Integer
        ''' <summary>[等效C軸]</summary>
        ''' <remarks></remarks>
        Friend AxisC As Integer
        ''' <summary>[未定義暫存用標記]</summary>
        ''' <remarks></remarks>
        Friend Tag As Object
        ''' <summary>[時間紀錄]</summary>
        ''' <remarks></remarks>
        Friend Timer As New Stopwatch
        ''' <summary>[影像定位參數]</summary>
        ''' <remarks></remarks>
        Friend CCDFixParam As sCCDFixParam
        ''' <summary>[排列出拍照順序(Ccd Path)]</summary>
        ''' <remarks></remarks>
        Friend CCDFixPathRegister As List(Of sCCDFixPath)
        ''' <summary>[排列出飛拍順序(Pattern Path)]</summary>
        ''' <remarks></remarks>
        Friend CCDOnFlyPathRegister As List(Of sPatternPath)
        ''' <summary>[排列出丟給Trigger Board的順序(Path)]</summary>
        ''' <remarks></remarks>
        Friend TriggerBoardPathRegister As List(Of sPatternPath)
        ''' <summary>[由流程決定外部是否可暫停]</summary>
        ''' <remarks></remarks>
        Friend IsCanPause As Boolean
        ''' <summary>[CCD Fix方式]</summary>
        ''' <remarks></remarks>
        Friend CCDFixModel As eCCDFixModel
        ''' <summary>[紀錄最後一個Ticket]</summary>
        ''' <remarks></remarks>
        Friend LastTicket As Integer
        ''' <summary>[定位流程開始時須將光源的紀錄值歸零]</summary>
        ''' <remarks></remarks>
        Friend IsResetLightValue As Boolean
        ' ''' <summary>[硬體觸發的起始Ticket] </summary>
        ' ''' <remarks></remarks>
        Friend StartIndex As Integer
        ' ''' <summary>[硬體觸發的結束Ticket] </summary>
        ' ''' <remarks></remarks>
        Friend EndIndex As Integer
    End Class

    ''' <summary>[取出定位Index(Integer-->enum)]</summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAlignIndex(ByVal index As Integer) As eAlignIndex
        Select Case index
            Case -1
                Return eAlignIndex.None

            Case 0
                Return eAlignIndex.No1

            Case 1
                Return eAlignIndex.No2

            Case 2
                Return eAlignIndex.No3

            Case Else
                Return eAlignIndex.None

        End Select
    End Function



    ''' <summary>[將Node與Scene串接起來]</summary>
    ''' <param name="Recipe"></param>
    ''' <param name="StageNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CCDFixRecipeNodeSort(ByVal recipe As CRecipe, ByVal stageNo As enmStage, ByVal conveyorNo As eConveyor) As Boolean

        Dim mI(enmStage.Max) As Integer
        Dim mJ(enmStage.Max) As Integer
        Dim mNodeName(enmStage.Max) As String
        Dim mNodeAlignScene(enmStage.Max) As SNodeAlignScene
        Dim mAlignScene(enmStage.Max) As String                   '定位點場景名稱
        Dim mAlignIndex(enmStage.Max) As Integer
        Dim mNodeLevel(enmStage.Max) As Integer
        Dim mTmpNodeLevel(enmStage.Max) As Integer
        Dim mNowNodeLevel(enmStage.Max) As Integer

        mNodeLevel(stageNo) = 1
        mNowNodeLevel(stageNo) = 1
        mTmpNodeLevel(stageNo) = 1
        CCDFixRelationshipList(stageNo) = New List(Of SNodeAlignScene)
        CCDFixRelationshipList(stageNo).Clear()

        For mJ(stageNo) = 0 To recipe.CCDFixTraversal(stageNo).Count - 1
            '[說明]:找出對應的Node & AlignScene
            mNodeName(stageNo) = recipe.CCDFixTraversal(stageNo)(mJ(stageNo))
            Call GetNodeLevel(mNodeName(stageNo), mNodeLevel(stageNo))
            If mNowNodeLevel(stageNo) <> mNodeLevel(stageNo) Then
                '[Note]換層
                For mI(stageNo) = 0 To recipe.CCDFixTraversal(stageNo).Count - 1
                    '[說明]:找出對應的Node & AlignScene
                    mNodeName(stageNo) = recipe.CCDFixTraversal(stageNo)(mI(stageNo))
                    Call GetNodeLevel(mNodeName(stageNo), mTmpNodeLevel(stageNo))
                    If mNowNodeLevel(stageNo) = mTmpNodeLevel(stageNo) Then
                        If recipe.Node(stageNo)(mNodeName(stageNo)).SkipMarkEnable Then
                            '[Note]:使用不生產點資訊
                            mAlignScene(stageNo) = recipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyorNo).SkipMarkData(0).AlignScene
                            With mNodeAlignScene(stageNo)
                                .IsEnableSkipMarkScene = True
                                .IsEnableAlignScene = False '[需確認]
                                .NodeName = mNodeName(stageNo)
                                .NodeLevel = mNowNodeLevel(stageNo)
                                .AlignScene = mAlignScene(stageNo)
                                .AlignIndex = eAlignIndex.SkipMark
                                .AlignType = eCCDAlignType.eSkipMark
                            End With
                            CCDFixRelationshipList(stageNo).Add(mNodeAlignScene(stageNo))
                        End If
                    Else
                        'Exit For
                    End If
                Next
                For mI(stageNo) = 0 To recipe.CCDFixTraversal(stageNo).Count - 1
                    '[說明]:找出對應的Node & AlignScene
                    mNodeName(stageNo) = recipe.CCDFixTraversal(stageNo)(mI(stageNo))
                    Call GetNodeLevel(mNodeName(stageNo), mTmpNodeLevel(stageNo))
                    If mNowNodeLevel(stageNo) = mTmpNodeLevel(stageNo) Then
                        If recipe.Node(stageNo)(mNodeName(stageNo)).AlignmentEnable Then
                            '[Note]:使用定位資訊
                            Select Case recipe.Node(stageNo)(mNodeName(stageNo)).AlignType
                                Case enmAlignType.DevicePos1
                                    mAlignScene(stageNo) = recipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyorNo).AlignmentData(CInt(eAlignIndex.No1)).AlignScene
                                    With mNodeAlignScene(stageNo)
                                        .IsEnableAlignScene = True
                                        .IsEnableSkipMarkScene = False '[需確認]
                                        .NodeName = mNodeName(stageNo)
                                        .NodeLevel = mNowNodeLevel(stageNo)
                                        .AlignScene = mAlignScene(stageNo)
                                        .AlignIndex = eAlignIndex.No1
                                        .AlignType = eCCDAlignType.eAlign
                                    End With
                                    CCDFixRelationshipList(stageNo).Add(mNodeAlignScene(stageNo))

                                Case enmAlignType.DevicePos2
                                    For mAlignIndex(stageNo) = CInt(eAlignIndex.No1) To CInt(eAlignIndex.No2)
                                        mAlignScene(stageNo) = recipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyorNo).AlignmentData(mAlignIndex(stageNo)).AlignScene
                                        With mNodeAlignScene(stageNo)
                                            .IsEnableAlignScene = True
                                            .IsEnableSkipMarkScene = False '[需確認]
                                            .NodeName = mNodeName(stageNo)
                                            .NodeLevel = mNowNodeLevel(stageNo)
                                            .AlignScene = mAlignScene(stageNo)
                                            .AlignIndex = GetAlignIndex(mAlignIndex(stageNo))
                                            .AlignType = eCCDAlignType.eAlign
                                        End With
                                        CCDFixRelationshipList(stageNo).Add(mNodeAlignScene(stageNo))
                                    Next

                                Case enmAlignType.DevicePos3
                                    For mAlignIndex(stageNo) = CInt(eAlignIndex.No1) To CInt(eAlignIndex.No3)
                                        mAlignScene(stageNo) = recipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyorNo).AlignmentData(mAlignIndex(stageNo)).AlignScene
                                        With mNodeAlignScene(stageNo)
                                            .IsEnableAlignScene = True
                                            .IsEnableSkipMarkScene = False '[需確認]
                                            .NodeName = mNodeName(stageNo)
                                            .NodeLevel = mNowNodeLevel(stageNo)
                                            .AlignScene = mAlignScene(stageNo)
                                            .AlignIndex = GetAlignIndex(mAlignIndex(stageNo))
                                            .AlignType = eCCDAlignType.eAlign
                                        End With
                                        CCDFixRelationshipList(stageNo).Add(mNodeAlignScene(stageNo))
                                    Next

                            End Select
                        Else
                            '[Note]:省略定位
                            With mNodeAlignScene(stageNo)
                                .IsEnableAlignScene = False
                                .IsEnableSkipMarkScene = False '[需確認]
                                .NodeName = mNodeName(stageNo)
                                .NodeLevel = mNowNodeLevel(stageNo)
                                .AlignScene = ""
                                .AlignIndex = eAlignIndex.None
                                .AlignType = eCCDAlignType.None
                            End With
                            CCDFixRelationshipList(stageNo).Add(mNodeAlignScene(stageNo))
                        End If
                    Else
                        'Exit For
                    End If
                Next
            End If
            mNowNodeLevel(stageNo) = mNodeLevel(stageNo)

            If mJ(stageNo) = recipe.CCDFixTraversal(stageNo).Count - 1 Then
                '[Note]最後一點
                mNowNodeLevel(stageNo) = mNodeLevel(stageNo)
                For mI(stageNo) = 0 To recipe.CCDFixTraversal(stageNo).Count - 1
                    '[說明]:找出對應的Node & AlignScene
                    mNodeName(stageNo) = recipe.CCDFixTraversal(stageNo)(mI(stageNo))
                    Call GetNodeLevel(mNodeName(stageNo), mTmpNodeLevel(stageNo))
                    If mNowNodeLevel(stageNo) = mTmpNodeLevel(stageNo) Then
                        If recipe.Node(stageNo)(mNodeName(stageNo)).SkipMarkEnable Then
                            '[Note]:使用不生產點資訊
                            mAlignScene(stageNo) = recipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyorNo).SkipMarkData(0).AlignScene
                            With mNodeAlignScene(stageNo)
                                .IsEnableSkipMarkScene = True
                                .IsEnableAlignScene = False '[需確認]
                                .NodeName = mNodeName(stageNo)
                                .NodeLevel = mNowNodeLevel(stageNo)
                                .AlignScene = mAlignScene(stageNo)
                                .AlignIndex = eAlignIndex.SkipMark
                                .AlignType = eCCDAlignType.eSkipMark
                            End With
                            CCDFixRelationshipList(stageNo).Add(mNodeAlignScene(stageNo))
                        End If
                    Else
                        'Exit For
                    End If
                Next
                For mI(stageNo) = 0 To recipe.CCDFixTraversal(stageNo).Count - 1
                    '[說明]:找出對應的Node & AlignScene
                    mNodeName(stageNo) = recipe.CCDFixTraversal(stageNo)(mI(stageNo))
                    Call GetNodeLevel(mNodeName(stageNo), mTmpNodeLevel(stageNo))
                    If mNowNodeLevel(stageNo) = mTmpNodeLevel(stageNo) Then
                        If recipe.Node(stageNo)(mNodeName(stageNo)).AlignmentEnable Then
                            '[Note]:使用定位資訊
                            Select Case recipe.Node(stageNo)(mNodeName(stageNo)).AlignType
                                Case enmAlignType.DevicePos1
                                    mAlignScene(stageNo) = recipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyorNo).AlignmentData(CInt(eAlignIndex.No1)).AlignScene
                                    With mNodeAlignScene(stageNo)
                                        .IsEnableAlignScene = True
                                        .IsEnableSkipMarkScene = False '[需確認]
                                        .NodeName = mNodeName(stageNo)
                                        .NodeLevel = mNowNodeLevel(stageNo)
                                        .AlignScene = mAlignScene(stageNo)
                                        .AlignIndex = eAlignIndex.No1
                                        .AlignType = eCCDAlignType.eAlign
                                    End With
                                    CCDFixRelationshipList(stageNo).Add(mNodeAlignScene(stageNo))

                                Case enmAlignType.DevicePos2
                                    For mAlignIndex(stageNo) = CInt(eAlignIndex.No1) To CInt(eAlignIndex.No2)
                                        mAlignScene(stageNo) = recipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyorNo).AlignmentData(mAlignIndex(stageNo)).AlignScene
                                        With mNodeAlignScene(stageNo)
                                            .IsEnableAlignScene = True
                                            .IsEnableSkipMarkScene = False '[需確認]
                                            .NodeName = mNodeName(stageNo)
                                            .NodeLevel = mNowNodeLevel(stageNo)
                                            .AlignScene = mAlignScene(stageNo)
                                            .AlignIndex = GetAlignIndex(mAlignIndex(stageNo))
                                            .AlignType = eCCDAlignType.eAlign
                                        End With
                                        CCDFixRelationshipList(stageNo).Add(mNodeAlignScene(stageNo))
                                    Next

                                Case enmAlignType.DevicePos3
                                    For mAlignIndex(stageNo) = CInt(eAlignIndex.No1) To CInt(eAlignIndex.No3)
                                        mAlignScene(stageNo) = recipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyorNo).AlignmentData(mAlignIndex(stageNo)).AlignScene
                                        With mNodeAlignScene(stageNo)
                                            .IsEnableAlignScene = True
                                            .IsEnableSkipMarkScene = False '[需確認]
                                            .NodeName = mNodeName(stageNo)
                                            .NodeLevel = mNowNodeLevel(stageNo)
                                            .AlignScene = mAlignScene(stageNo)
                                            .AlignIndex = GetAlignIndex(mAlignIndex(stageNo))
                                            .AlignType = eCCDAlignType.eAlign
                                        End With
                                        CCDFixRelationshipList(stageNo).Add(mNodeAlignScene(stageNo))
                                    Next

                            End Select
                        Else
                            '[Note]:省略定位
                            With mNodeAlignScene(stageNo)
                                .IsEnableAlignScene = False
                                .IsEnableSkipMarkScene = False '[需確認]
                                .NodeName = mNodeName(stageNo)
                                .NodeLevel = mNowNodeLevel(stageNo)
                                .AlignScene = ""
                                .AlignIndex = eAlignIndex.None
                                .AlignType = eCCDAlignType.None
                            End With
                            CCDFixRelationshipList(stageNo).Add(mNodeAlignScene(stageNo))
                        End If
                    Else
                        'Exit For
                    End If
                Next
            End If
        Next

        Return True

    End Function

    ''' <summary>[判斷是否需要執行CCD定位程序]</summary>
    ''' <param name="recipe"></param>
    ''' <param name="stageNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsNeedCCDFixProcess(ByVal recipe As CRecipe, ByVal stageNo As Integer) As Boolean

        '[Note]:完全沒有節點，該側根本沒有動作需要執行
        If recipe.Node(stageNo).Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' [列出不生產點順序]
    ''' </summary>
    ''' <param name="recipe"></param>
    ''' <param name="stageNo"></param>
    ''' <param name="ccdFixRelationshipList"></param>
    ''' <param name="ccdFixList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CCDSkipMarkSortList(ByVal recipe As CRecipe, ByVal stageNo As enmStage, ByVal ccdFixRelationshipList As SNodeAlignScene, ByRef ccdFixList As List(Of sCCDFixData)) As Boolean
        Dim mI(enmStage.Max) As Integer
        Dim mJ(enmStage.Max) As Integer
        Dim mIndex(enmStage.Max) As sCCDFixData
        Dim mNodeName(enmStage.Max) As String
        Dim mAlignIndex(enmStage.Max) As eAlignIndex
        Dim mAlignScene(enmStage.Max) As String
        Dim mNodeLevel(enmStage.Max) As Integer

        mNodeName(stageNo) = ccdFixRelationshipList.NodeName
        mAlignIndex(stageNo) = ccdFixRelationshipList.AlignIndex
        mAlignScene(stageNo) = ccdFixRelationshipList.AlignScene
        mNodeLevel(stageNo) = ccdFixRelationshipList.NodeLevel
        '[Note]先排列出該層的SkipMark的位置

        If ccdFixRelationshipList.AlignIndex = eAlignIndex.SkipMark Then
            If ccdFixRelationshipList.IsEnableSkipMarkScene = True Then
                Select Case recipe.SearchType
                    Case enmSearchType.Y_ZigZag
                        For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                            '[正向]
                            For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                                Select Case recipe.CCDFixModel
                                    Case eCCDFixModel.OnFly
                                        '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                        With mIndex(stageNo)
                                            .IndexX = mI(stageNo)
                                            .IndexY = mJ(stageNo)
                                            .AlignScene = mAlignScene(stageNo)
                                            .AlignIndex = mAlignIndex(stageNo)
                                            .NodeName = mNodeName(stageNo)
                                            .NodeLevel = mNodeLevel(stageNo)
                                        End With
                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                        ccdFixList.Capacity = ccdFixList.Count + 1
                                        ccdFixList.Add(mIndex(stageNo))
                                        gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)

                                    Case eCCDFixModel.NonOnFly
                                        '[Note]:判斷是否Mapping啟用該Die
                                        If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                            gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                            Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                            If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                                With mIndex(stageNo)
                                                    .IndexX = mI(stageNo)
                                                    .IndexY = mJ(stageNo)
                                                    .AlignScene = mAlignScene(stageNo)
                                                    .AlignIndex = mAlignIndex(stageNo)
                                                    .NodeName = mNodeName(stageNo)
                                                    .NodeLevel = mNodeLevel(stageNo)
                                                End With
                                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                ccdFixList.Capacity = ccdFixList.Count + 1
                                                ccdFixList.Add(mIndex(stageNo))
                                                gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                            End If
                                        End If
                                End Select
                            Next mJ
                        Next mI
                    Case enmSearchType.Y_Snake
                        For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                            If mI(stageNo) Mod 2 = 0 Then
                                '[正向]
                                For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                                    Select Case recipe.CCDFixModel
                                        Case eCCDFixModel.OnFly
                                            '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                            With mIndex(stageNo)
                                                .IndexX = mI(stageNo)
                                                .IndexY = mJ(stageNo)
                                                .AlignScene = mAlignScene(stageNo)
                                                .AlignIndex = mAlignIndex(stageNo)
                                                .NodeName = mNodeName(stageNo)
                                                .NodeLevel = mNodeLevel(stageNo)
                                            End With
                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                            ccdFixList.Capacity = ccdFixList.Count + 1
                                            ccdFixList.Add(mIndex(stageNo))
                                            gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)

                                        Case eCCDFixModel.NonOnFly
                                            '[Note]:判斷是否Mapping啟用該Die
                                            If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                                gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                                Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                                    With mIndex(stageNo)
                                                        .IndexX = mI(stageNo)
                                                        .IndexY = mJ(stageNo)
                                                        .AlignScene = mAlignScene(stageNo)
                                                        .AlignIndex = mAlignIndex(stageNo)
                                                        .NodeName = mNodeName(stageNo)
                                                        .NodeLevel = mNodeLevel(stageNo)
                                                    End With
                                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                    ccdFixList.Capacity = ccdFixList.Count + 1
                                                    ccdFixList.Add(mIndex(stageNo))
                                                    gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                                End If
                                            End If
                                    End Select
                                Next mJ
                            Else
                                '[逆向]
                                For mJ(stageNo) = UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2) To 0 Step -1
                                    Select Case recipe.CCDFixModel
                                        Case eCCDFixModel.OnFly
                                            '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                            With mIndex(stageNo)
                                                .IndexX = mI(stageNo)
                                                .IndexY = mJ(stageNo)
                                                .AlignScene = mAlignScene(stageNo)
                                                .AlignIndex = mAlignIndex(stageNo)
                                                .NodeName = mNodeName(stageNo)
                                                .NodeLevel = mNodeLevel(stageNo)
                                            End With
                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                            ccdFixList.Capacity = ccdFixList.Count + 1
                                            ccdFixList.Add(mIndex(stageNo))
                                            gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                        Case eCCDFixModel.NonOnFly
                                            '[Note]:判斷是否Mapping啟用該Die
                                            If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                                gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                                Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                                    With mIndex(stageNo)
                                                        .IndexX = mI(stageNo)
                                                        .IndexY = mJ(stageNo)
                                                        .AlignScene = mAlignScene(stageNo)
                                                        .AlignIndex = mAlignIndex(stageNo)
                                                        .NodeName = mNodeName(stageNo)
                                                        .NodeLevel = mNodeLevel(stageNo)
                                                    End With
                                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                    ccdFixList.Capacity = ccdFixList.Count + 1
                                                    ccdFixList.Add(mIndex(stageNo))
                                                    gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                                End If
                                            End If
                                    End Select
                                Next mJ
                            End If
                        Next mI
                    Case enmSearchType.X_Snake
                        For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                            If mJ(stageNo) Mod 2 = 0 Then
                                '[正向]
                                For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                                    Select Case recipe.CCDFixModel
                                        Case eCCDFixModel.OnFly
                                            '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                            With mIndex(stageNo)
                                                .IndexX = mI(stageNo)
                                                .IndexY = mJ(stageNo)
                                                .AlignScene = mAlignScene(stageNo)
                                                .AlignIndex = mAlignIndex(stageNo)
                                                .NodeName = mNodeName(stageNo)
                                                .NodeLevel = mNodeLevel(stageNo)
                                            End With
                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                            ccdFixList.Capacity = ccdFixList.Count + 1
                                            ccdFixList.Add(mIndex(stageNo))
                                            gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                        Case eCCDFixModel.NonOnFly
                                            '[Note]:判斷是否Mapping啟用該Die
                                            If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                                gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                                Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                                    With mIndex(stageNo)
                                                        .IndexX = mI(stageNo)
                                                        .IndexY = mJ(stageNo)
                                                        .AlignScene = mAlignScene(stageNo)
                                                        .AlignIndex = mAlignIndex(stageNo)
                                                        .NodeName = mNodeName(stageNo)
                                                        .NodeLevel = mNodeLevel(stageNo)
                                                    End With
                                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                    ccdFixList.Capacity = ccdFixList.Count + 1
                                                    ccdFixList.Add(mIndex(stageNo))
                                                    gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                                End If
                                            End If
                                    End Select
                                Next
                            Else
                                '[逆向]
                                For mI(stageNo) = UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1) To 0 Step -1
                                    Select Case recipe.CCDFixModel
                                        Case eCCDFixModel.OnFly
                                            '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                            With mIndex(stageNo)
                                                .IndexX = mI(stageNo)
                                                .IndexY = mJ(stageNo)
                                                .AlignScene = mAlignScene(stageNo)
                                                .AlignIndex = mAlignIndex(stageNo)
                                                .NodeName = mNodeName(stageNo)
                                                .NodeLevel = mNodeLevel(stageNo)
                                            End With
                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                            ccdFixList.Capacity = ccdFixList.Count + 1
                                            ccdFixList.Add(mIndex(stageNo))
                                            gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                        Case eCCDFixModel.NonOnFly
                                            '[Note]:判斷是否Mapping啟用該Die
                                            If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                                gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                                Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                                    With mIndex(stageNo)
                                                        .IndexX = mI(stageNo)
                                                        .IndexY = mJ(stageNo)
                                                        .AlignScene = mAlignScene(stageNo)
                                                        .AlignIndex = mAlignIndex(stageNo)
                                                        .NodeName = mNodeName(stageNo)
                                                        .NodeLevel = mNodeLevel(stageNo)
                                                    End With
                                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                    ccdFixList.Capacity = ccdFixList.Count + 1
                                                    ccdFixList.Add(mIndex(stageNo))
                                                    gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                                End If
                                            End If
                                    End Select
                                Next
                            End If
                        Next
                    Case enmSearchType.X_ZigZag
                        For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                            '[正向]
                            For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                                Select Case recipe.CCDFixModel
                                    Case eCCDFixModel.OnFly
                                        '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                        With mIndex(stageNo)
                                            .IndexX = mI(stageNo)
                                            .IndexY = mJ(stageNo)
                                            .AlignScene = mAlignScene(stageNo)
                                            .AlignIndex = mAlignIndex(stageNo)
                                            .NodeName = mNodeName(stageNo)
                                            .NodeLevel = mNodeLevel(stageNo)
                                        End With
                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                        ccdFixList.Capacity = ccdFixList.Count + 1
                                        ccdFixList.Add(mIndex(stageNo))
                                        gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                    Case eCCDFixModel.NonOnFly
                                        '[Note]:判斷是否Mapping啟用該Die
                                        If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                            gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                            Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                            If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                                With mIndex(stageNo)
                                                    .IndexX = mI(stageNo)
                                                    .IndexY = mJ(stageNo)
                                                    .AlignScene = mAlignScene(stageNo)
                                                    .AlignIndex = mAlignIndex(stageNo)
                                                    .NodeName = mNodeName(stageNo)
                                                    .NodeLevel = mNodeLevel(stageNo)
                                                End With
                                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                ccdFixList.Capacity = ccdFixList.Count + 1
                                                ccdFixList.Add(mIndex(stageNo))
                                                gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                            End If
                                        End If
                                End Select
                            Next
                        Next
                End Select
            Else
                '[不使用不生產點]
                Return True
            End If
            Return True
        End If
        Return True
    End Function


    ''' <summary>[列出定位順序]</summary>
    ''' <param name="recipe"></param>
    ''' <param name="stageNo"></param>
    ''' <param name="ccdFixRelationshipList"></param>
    ''' <param name="ccdFixList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CCDFixSortList(ByVal recipe As CRecipe, ByVal stageNo As enmStage, ByVal ccdFixRelationshipList As List(Of SNodeAlignScene), ByRef ccdFixList As List(Of sCCDFixData)) As Boolean

        Dim mI(enmStage.Max) As Integer
        Dim mJ(enmStage.Max) As Integer
        Dim mIndex(enmStage.Max) As sCCDFixData
        Dim mNodeName(enmStage.Max) As String
        Dim mAlignIndex(enmStage.Max) As eAlignIndex
        Dim mAlignScene(enmStage.Max) As String
        Dim mNodeLevel(enmStage.Max) As Integer

        mNodeName(stageNo) = ccdFixRelationshipList(0).NodeName
        mAlignIndex(stageNo) = ccdFixRelationshipList(0).AlignIndex
        mAlignScene(stageNo) = ccdFixRelationshipList(0).AlignScene
        mNodeLevel(stageNo) = ccdFixRelationshipList(0).NodeLevel

        If mAlignIndex(stageNo) = eAlignIndex.None Then
            '[Note]:就是不需要啦
            Return True
        End If

        If ccdFixRelationshipList(0).IsEnableAlignScene = True Then
            Select Case recipe.SearchType
                Case enmSearchType.Y_ZigZag
                    For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                        '[正向]
                        For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                            If recipe.CCDFixModel = eCCDFixModel.OnFly Then
                                '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                With mIndex(stageNo)
                                    .IndexX = mI(stageNo)
                                    .IndexY = mJ(stageNo)
                                    .AlignScene = mAlignScene(stageNo)
                                    .AlignIndex = mAlignIndex(stageNo)
                                    .NodeName = mNodeName(stageNo)
                                    .NodeLevel = mNodeLevel(stageNo)
                                End With
                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                ccdFixList.Capacity = ccdFixList.Count + 1
                                ccdFixList.Add(mIndex(stageNo))
                                gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                            Else
                                '[Note]:判斷是否Mapping啟用該Die
                                'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                '    gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                '    Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                    Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(mAlignIndex(stageNo))) = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then

                                        '[Note]:把所有的List撈出來
                                        For Each SNodeAlignScene In ccdFixRelationshipList
                                            With mIndex(stageNo)
                                                .IndexX = mI(stageNo)
                                                .IndexY = mJ(stageNo)
                                                .AlignScene = SNodeAlignScene.AlignScene 'mAlignScene(stageNo)
                                                .AlignIndex = SNodeAlignScene.AlignIndex 'mAlignIndex(stageNo)
                                                .NodeName = SNodeAlignScene.NodeName 'mNodeName(stageNo)
                                                .NodeLevel = SNodeAlignScene.NodeLevel 'mNodeLevel(stageNo)
                                            End With
                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                            ccdFixList.Capacity = ccdFixList.Count + 1
                                            ccdFixList.Add(mIndex(stageNo))
                                            gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                        Next
                                    End If
                                End If
                            End If
                        Next mJ
                    Next mI

                Case enmSearchType.Y_Snake
                    For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                        If mI(stageNo) Mod 2 = 0 Then
                            '[正向]
                            For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                                If recipe.CCDFixModel = eCCDFixModel.OnFly Then
                                    '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                    With mIndex(stageNo)
                                        .IndexX = mI(stageNo)
                                        .IndexY = mJ(stageNo)
                                        .AlignScene = mAlignScene(stageNo)
                                        .AlignIndex = mAlignIndex(stageNo)
                                        .NodeName = mNodeName(stageNo)
                                        .NodeLevel = mNodeLevel(stageNo)
                                    End With
                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                    ccdFixList.Capacity = ccdFixList.Count + 1
                                    ccdFixList.Add(mIndex(stageNo))
                                    gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                Else
                                    '[Note]:判斷是否Mapping啟用該Die
                                    'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                    '    gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                    '    Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                        Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                        If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(mAlignIndex(stageNo))) = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                            '[Note]:把所有的List撈出來
                                            For Each SNodeAlignScene In ccdFixRelationshipList
                                                With mIndex(stageNo)
                                                    .IndexX = mI(stageNo)
                                                    .IndexY = mJ(stageNo)
                                                    .AlignScene = SNodeAlignScene.AlignScene 'mAlignScene(stageNo)
                                                    .AlignIndex = SNodeAlignScene.AlignIndex 'mAlignIndex(stageNo)
                                                    .NodeName = SNodeAlignScene.NodeName 'mNodeName(stageNo)
                                                    .NodeLevel = SNodeAlignScene.NodeLevel 'mNodeLevel(stageNo)
                                                End With
                                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                ccdFixList.Capacity = ccdFixList.Count + 1
                                                ccdFixList.Add(mIndex(stageNo))
                                                gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                            Next

                                        End If
                                    End If
                                End If
                            Next mJ
                        Else
                            '[逆向]
                            For mJ(stageNo) = UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2) To 0 Step -1
                                If recipe.CCDFixModel = eCCDFixModel.OnFly Then
                                    '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                    With mIndex(stageNo)
                                        .IndexX = mI(stageNo)
                                        .IndexY = mJ(stageNo)
                                        .AlignScene = mAlignScene(stageNo)
                                        .AlignIndex = mAlignIndex(stageNo)
                                        .NodeName = mNodeName(stageNo)
                                        .NodeLevel = mNodeLevel(stageNo)
                                    End With
                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                    ccdFixList.Capacity = ccdFixList.Count + 1
                                    ccdFixList.Add(mIndex(stageNo))
                                    gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                Else
                                    '[Note]:判斷是否Mapping啟用該Die
                                    'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                    '    gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                    '     Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                        Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                        If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(mAlignIndex(stageNo))) = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                            '[Note]:把所有的List撈出來
                                            For Each SNodeAlignScene In ccdFixRelationshipList
                                                With mIndex(stageNo)
                                                    .IndexX = mI(stageNo)
                                                    .IndexY = mJ(stageNo)
                                                    .AlignScene = SNodeAlignScene.AlignScene 'mAlignScene(stageNo)
                                                    .AlignIndex = SNodeAlignScene.AlignIndex 'mAlignIndex(stageNo)
                                                    .NodeName = SNodeAlignScene.NodeName 'mNodeName(stageNo)
                                                    .NodeLevel = SNodeAlignScene.NodeLevel 'mNodeLevel(stageNo)
                                                End With
                                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                ccdFixList.Capacity = ccdFixList.Count + 1
                                                ccdFixList.Add(mIndex(stageNo))
                                                gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                            Next

                                        End If
                                    End If
                                End If
                            Next mJ
                        End If
                    Next mI

                Case enmSearchType.X_Snake
                    For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                        If mJ(stageNo) Mod 2 = 0 Then
                            '[正向]
                            For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                                If recipe.CCDFixModel = eCCDFixModel.OnFly Then
                                    '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                    With mIndex(stageNo)
                                        .IndexX = mI(stageNo)
                                        .IndexY = mJ(stageNo)
                                        .AlignScene = mAlignScene(stageNo)
                                        .AlignIndex = mAlignIndex(stageNo)
                                        .NodeName = mNodeName(stageNo)
                                        .NodeLevel = mNodeLevel(stageNo)
                                    End With
                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                    ccdFixList.Capacity = ccdFixList.Count + 1
                                    ccdFixList.Add(mIndex(stageNo))
                                    gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                Else
                                    '[Note]:判斷是否Mapping啟用該Die
                                    'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                    '    gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                    '     Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                        Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                        If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(mAlignIndex(stageNo))) = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                            '[Note]:把所有的List撈出來
                                            For Each SNodeAlignScene In ccdFixRelationshipList
                                                With mIndex(stageNo)
                                                    .IndexX = mI(stageNo)
                                                    .IndexY = mJ(stageNo)
                                                    .AlignScene = SNodeAlignScene.AlignScene 'mAlignScene(stageNo)
                                                    .AlignIndex = SNodeAlignScene.AlignIndex 'mAlignIndex(stageNo)
                                                    .NodeName = SNodeAlignScene.NodeName 'mNodeName(stageNo)
                                                    .NodeLevel = SNodeAlignScene.NodeLevel 'mNodeLevel(stageNo)
                                                End With
                                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                ccdFixList.Capacity = ccdFixList.Count + 1
                                                ccdFixList.Add(mIndex(stageNo))
                                                gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                            Next

                                        End If
                                    End If
                                End If
                            Next
                        Else
                            '[逆向]
                            For mI(stageNo) = UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1) To 0 Step -1
                                If recipe.CCDFixModel = eCCDFixModel.OnFly Then
                                    '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                    With mIndex(stageNo)
                                        .IndexX = mI(stageNo)
                                        .IndexY = mJ(stageNo)
                                        .AlignScene = mAlignScene(stageNo)
                                        .AlignIndex = mAlignIndex(stageNo)
                                        .NodeName = mNodeName(stageNo)
                                        .NodeLevel = mNodeLevel(stageNo)
                                    End With
                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                    ccdFixList.Capacity = ccdFixList.Count + 1
                                    ccdFixList.Add(mIndex(stageNo))
                                    gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                Else
                                    '[Note]:判斷是否Mapping啟用該Die
                                    'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                    '    gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                    '        Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                        Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                        If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(mAlignIndex(stageNo))) = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                            '[Note]:把所有的List撈出來
                                            For Each SNodeAlignScene In ccdFixRelationshipList
                                                With mIndex(stageNo)
                                                    .IndexX = mI(stageNo)
                                                    .IndexY = mJ(stageNo)
                                                    .AlignScene = SNodeAlignScene.AlignScene 'mAlignScene(stageNo)
                                                    .AlignIndex = SNodeAlignScene.AlignIndex 'mAlignIndex(stageNo)
                                                    .NodeName = SNodeAlignScene.NodeName 'mNodeName(stageNo)
                                                    .NodeLevel = SNodeAlignScene.NodeLevel 'mNodeLevel(stageNo)
                                                End With
                                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                ccdFixList.Capacity = ccdFixList.Count + 1
                                                ccdFixList.Add(mIndex(stageNo))
                                                gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                            Next

                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Next

                Case enmSearchType.X_ZigZag
                    For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                        '[正向]
                        For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                            If recipe.CCDFixModel = eCCDFixModel.OnFly Then
                                '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                With mIndex(stageNo)
                                    .IndexX = mI(stageNo)
                                    .IndexY = mJ(stageNo)
                                    .AlignScene = mAlignScene(stageNo)
                                    .AlignIndex = mAlignIndex(stageNo)
                                    .NodeName = mNodeName(stageNo)
                                    .NodeLevel = mNodeLevel(stageNo)
                                End With
                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                ccdFixList.Capacity = ccdFixList.Count + 1
                                ccdFixList.Add(mIndex(stageNo))
                                gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                            Else
                                '[Note]:判斷是否Mapping啟用該Die
                                'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                '    gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                '        Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                    Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(mAlignIndex(stageNo))) = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                        '[Note]:把所有的List撈出來
                                        For Each SNodeAlignScene In ccdFixRelationshipList
                                            With mIndex(stageNo)
                                                .IndexX = mI(stageNo)
                                                .IndexY = mJ(stageNo)
                                                .AlignScene = SNodeAlignScene.AlignScene 'mAlignScene(stageNo)
                                                .AlignIndex = SNodeAlignScene.AlignIndex 'mAlignIndex(stageNo)
                                                .NodeName = SNodeAlignScene.NodeName 'mNodeName(stageNo)
                                                .NodeLevel = SNodeAlignScene.NodeLevel 'mNodeLevel(stageNo)
                                            End With
                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                            ccdFixList.Capacity = ccdFixList.Count + 1
                                            ccdFixList.Add(mIndex(stageNo))
                                            gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                        Next

                                    End If
                                End If
                            End If
                        Next
                    Next
            End Select
        Else
            '[Note]:就是不需要啦
            Return True
        End If

        Return True
    End Function


    ''' <summary>[列出定位順序]</summary>
    ''' <param name="recipe"></param>
    ''' <param name="stageNo"></param>
    ''' <param name="ccdFixRelationshipList"></param>
    ''' <param name="ccdFixList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CCDFixSortList(ByVal recipe As CRecipe, ByVal stageNo As enmStage, ByVal ccdFixRelationshipList As SNodeAlignScene, ByRef ccdFixList As List(Of sCCDFixData)) As Boolean

        Dim mI(enmStage.Max) As Integer
        Dim mJ(enmStage.Max) As Integer
        Dim mIndex(enmStage.Max) As sCCDFixData
        Dim mNodeName(enmStage.Max) As String
        Dim mAlignIndex(enmStage.Max) As eAlignIndex
        Dim mAlignScene(enmStage.Max) As String
        Dim mNodeLevel(enmStage.Max) As Integer

        mNodeName(stageNo) = ccdFixRelationshipList.NodeName
        mAlignIndex(stageNo) = ccdFixRelationshipList.AlignIndex
        mAlignScene(stageNo) = ccdFixRelationshipList.AlignScene
        mNodeLevel(stageNo) = ccdFixRelationshipList.NodeLevel

        If mAlignIndex(stageNo) = eAlignIndex.None Then
            '[Note]:就是不需要啦
            Return True
        End If

        If ccdFixRelationshipList.IsEnableAlignScene = True Then
            Select Case recipe.SearchType
                Case enmSearchType.Y_ZigZag
                    For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                        '[正向]
                        For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                            If recipe.CCDFixModel = eCCDFixModel.OnFly Then
                                '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                With mIndex(stageNo)
                                    .IndexX = mI(stageNo)
                                    .IndexY = mJ(stageNo)
                                    .AlignScene = mAlignScene(stageNo)
                                    .AlignIndex = mAlignIndex(stageNo)
                                    .NodeName = mNodeName(stageNo)
                                    .NodeLevel = mNodeLevel(stageNo)
                                End With
                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                ccdFixList.Capacity = ccdFixList.Count + 1
                                ccdFixList.Add(mIndex(stageNo))
                                gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                            Else
                                '[Note]:判斷是否Mapping啟用該Die
                                'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                '    gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                '    Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                    Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(mAlignIndex(stageNo))) = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                        With mIndex(stageNo)
                                            .IndexX = mI(stageNo)
                                            .IndexY = mJ(stageNo)
                                            .AlignScene = mAlignScene(stageNo)
                                            .AlignIndex = mAlignIndex(stageNo)
                                            .NodeName = mNodeName(stageNo)
                                            .NodeLevel = mNodeLevel(stageNo)
                                        End With
                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                        ccdFixList.Capacity = ccdFixList.Count + 1
                                        ccdFixList.Add(mIndex(stageNo))
                                        gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                    End If
                                End If
                            End If
                        Next mJ
                    Next mI

                Case enmSearchType.Y_Snake
                    For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                        If mI(stageNo) Mod 2 = 0 Then
                            '[正向]
                            For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                                If recipe.CCDFixModel = eCCDFixModel.OnFly Then
                                    '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                    With mIndex(stageNo)
                                        .IndexX = mI(stageNo)
                                        .IndexY = mJ(stageNo)
                                        .AlignScene = mAlignScene(stageNo)
                                        .AlignIndex = mAlignIndex(stageNo)
                                        .NodeName = mNodeName(stageNo)
                                        .NodeLevel = mNodeLevel(stageNo)
                                    End With
                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                    ccdFixList.Capacity = ccdFixList.Count + 1
                                    ccdFixList.Add(mIndex(stageNo))
                                    gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                Else
                                    '[Note]:判斷是否Mapping啟用該Die
                                    'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                    '    gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                    '    Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                        Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                        If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(mAlignIndex(stageNo))) = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                            With mIndex(stageNo)
                                                .IndexX = mI(stageNo)
                                                .IndexY = mJ(stageNo)
                                                .AlignScene = mAlignScene(stageNo)
                                                .AlignIndex = mAlignIndex(stageNo)
                                                .NodeName = mNodeName(stageNo)
                                                .NodeLevel = mNodeLevel(stageNo)
                                            End With
                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                            ccdFixList.Capacity = ccdFixList.Count + 1
                                            ccdFixList.Add(mIndex(stageNo))
                                            gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                        End If
                                    End If
                                End If
                            Next mJ
                        Else
                            '[逆向]
                            For mJ(stageNo) = UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2) To 0 Step -1
                                If recipe.CCDFixModel = eCCDFixModel.OnFly Then
                                    '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                    With mIndex(stageNo)
                                        .IndexX = mI(stageNo)
                                        .IndexY = mJ(stageNo)
                                        .AlignScene = mAlignScene(stageNo)
                                        .AlignIndex = mAlignIndex(stageNo)
                                        .NodeName = mNodeName(stageNo)
                                        .NodeLevel = mNodeLevel(stageNo)
                                    End With
                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                    ccdFixList.Capacity = ccdFixList.Count + 1
                                    ccdFixList.Add(mIndex(stageNo))
                                    gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                Else
                                    '[Note]:判斷是否Mapping啟用該Die
                                    'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                    '    gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                    '     Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                        Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                        If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(mAlignIndex(stageNo))) = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                            With mIndex(stageNo)
                                                .IndexX = mI(stageNo)
                                                .IndexY = mJ(stageNo)
                                                .AlignScene = mAlignScene(stageNo)
                                                .AlignIndex = mAlignIndex(stageNo)
                                                .NodeName = mNodeName(stageNo)
                                                .NodeLevel = mNodeLevel(stageNo)
                                            End With
                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                            ccdFixList.Capacity = ccdFixList.Count + 1
                                            ccdFixList.Add(mIndex(stageNo))
                                            gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                        End If
                                    End If
                                End If
                            Next mJ
                        End If
                    Next mI

                Case enmSearchType.X_Snake
                    For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                        If mJ(stageNo) Mod 2 = 0 Then
                            '[正向]
                            For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                                If recipe.CCDFixModel = eCCDFixModel.OnFly Then
                                    '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                    With mIndex(stageNo)
                                        .IndexX = mI(stageNo)
                                        .IndexY = mJ(stageNo)
                                        .AlignScene = mAlignScene(stageNo)
                                        .AlignIndex = mAlignIndex(stageNo)
                                        .NodeName = mNodeName(stageNo)
                                        .NodeLevel = mNodeLevel(stageNo)
                                    End With
                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                    ccdFixList.Capacity = ccdFixList.Count + 1
                                    ccdFixList.Add(mIndex(stageNo))
                                    gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                Else
                                    '[Note]:判斷是否Mapping啟用該Die
                                    'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                    '    gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                    '     Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                        Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                        If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(mAlignIndex(stageNo))) = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                            With mIndex(stageNo)
                                                .IndexX = mI(stageNo)
                                                .IndexY = mJ(stageNo)
                                                .AlignScene = mAlignScene(stageNo)
                                                .AlignIndex = mAlignIndex(stageNo)
                                                .NodeName = mNodeName(stageNo)
                                                .NodeLevel = mNodeLevel(stageNo)
                                            End With
                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                            ccdFixList.Capacity = ccdFixList.Count + 1
                                            ccdFixList.Add(mIndex(stageNo))
                                            gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                        End If
                                    End If
                                End If
                            Next
                        Else
                            '[逆向]
                            For mI(stageNo) = UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1) To 0 Step -1
                                If recipe.CCDFixModel = eCCDFixModel.OnFly Then
                                    '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                    With mIndex(stageNo)
                                        .IndexX = mI(stageNo)
                                        .IndexY = mJ(stageNo)
                                        .AlignScene = mAlignScene(stageNo)
                                        .AlignIndex = mAlignIndex(stageNo)
                                        .NodeName = mNodeName(stageNo)
                                        .NodeLevel = mNodeLevel(stageNo)
                                    End With
                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                    ccdFixList.Capacity = ccdFixList.Count + 1
                                    ccdFixList.Add(mIndex(stageNo))
                                    gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                Else
                                    '[Note]:判斷是否Mapping啟用該Die
                                    'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                    '    gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                    '        Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                        Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                        If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(mAlignIndex(stageNo))) = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                            With mIndex(stageNo)
                                                .IndexX = mI(stageNo)
                                                .IndexY = mJ(stageNo)
                                                .AlignScene = mAlignScene(stageNo)
                                                .AlignIndex = mAlignIndex(stageNo)
                                                .NodeName = mNodeName(stageNo)
                                                .NodeLevel = mNodeLevel(stageNo)
                                            End With
                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                            ccdFixList.Capacity = ccdFixList.Count + 1
                                            ccdFixList.Add(mIndex(stageNo))
                                            gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Next

                Case enmSearchType.X_ZigZag
                    For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                        '[正向]
                        For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                            If recipe.CCDFixModel = eCCDFixModel.OnFly Then
                                '[Note]飛拍路徑全串，否則點太少時動作看起來很怪
                                With mIndex(stageNo)
                                    .IndexX = mI(stageNo)
                                    .IndexY = mJ(stageNo)
                                    .AlignScene = mAlignScene(stageNo)
                                    .AlignIndex = mAlignIndex(stageNo)
                                    .NodeName = mNodeName(stageNo)
                                    .NodeLevel = mNodeLevel(stageNo)
                                End With
                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                ccdFixList.Capacity = ccdFixList.Count + 1
                                ccdFixList.Add(mIndex(stageNo))
                                gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                            Else
                                '[Note]:判斷是否Mapping啟用該Die
                                'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And
                                '    gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                '        Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanFixAction = False And
                                    Not (gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.Finish) Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(mAlignIndex(stageNo))) = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                        With mIndex(stageNo)
                                            .IndexX = mI(stageNo)
                                            .IndexY = mJ(stageNo)
                                            .AlignScene = mAlignScene(stageNo)
                                            .AlignIndex = mAlignIndex(stageNo)
                                            .NodeName = mNodeName(stageNo)
                                            .NodeLevel = mNodeLevel(stageNo)
                                        End With
                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                        ccdFixList.Capacity = ccdFixList.Count + 1
                                        ccdFixList.Add(mIndex(stageNo))
                                        gSyslog.CCDSave("CCDFixList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")", CSystemLog.eCCDMessageProcess.Add)
                                    End If
                                End If
                            End If
                        Next
                    Next
            End Select
        Else
            '[Note]:就是不需要啦
            Return True
        End If

        Return True
    End Function

    ''' <summary>[取出定位點座標]</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="recipe"></param>
    ''' <param name="ccdFixData"></param>
    ''' <param name="ccdFixPos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EstimateCCDFixPos(ByVal stageNo As Integer, ByVal recipe As CRecipe, ByVal ccdFixData As sCCDFixData, ByRef ccdFixPos As Premtek.sPos) As Boolean

        Dim mNodeName(enmStage.Max) As String
        Dim mIndexX(enmStage.Max) As Integer
        Dim mIndexY(enmStage.Max) As Integer

        mNodeName(stageNo) = ccdFixData.NodeName
        mIndexX(stageNo) = ccdFixData.IndexX
        mIndexY(stageNo) = ccdFixData.IndexY

        Select Case ccdFixData.AlignIndex
            Case eAlignIndex.No1
                ccdFixPos.PosX = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).ScanPosX
                ccdFixPos.PosY = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).ScanPosY
                ccdFixPos.PosZ = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).ScanPosZ

            Case eAlignIndex.No2
                ccdFixPos.PosX = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).ScanPosX2
                ccdFixPos.PosY = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).ScanPosY2
                ccdFixPos.PosZ = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).ScanPosZ2

            Case eAlignIndex.No3
                ccdFixPos.PosX = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).ScanPosX3
                ccdFixPos.PosY = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).ScanPosY3
                ccdFixPos.PosZ = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).ScanPosZ3

            Case eAlignIndex.SkipMark
                ccdFixPos.PosX = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).SkipMarkPosX
                ccdFixPos.PosY = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).SkipMarkPosY
                ccdFixPos.PosZ = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).SkipMarkPosZ

            Case Else
                '[Note]:不應該進入
                Return False

        End Select
        Return True

    End Function


    ''' <summary>[將所有的CCD Fix資訊先串起來，後面再做運算處理(Motion)]</summary>
    ''' <param name="recipe"></param>
    ''' <param name="stageNo"></param>
    ''' <param name="ccdFixList"></param>
    ''' <param name="ccdFixPosRegister"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateCCDFixPosList(ByVal recipe As CRecipe, ByVal stageNo As enmStage, ByVal ccdFixList As List(Of sCCDFixData), ByRef ccdFixPosRegister As List(Of sCCDFixPath)) As Boolean

        Dim mCount(enmStage.Max) As Integer
        Dim mPath(enmStage.Max) As sCCDFixPath
        Dim mCCDFixPathRegister(enmStage.Max) As List(Of sCCDFixPath)
        Dim mOnFlyPath(enmStage.Max) As sPatternPath
        Dim mCCDOnFlyPathRegister(enmStage.Max) As List(Of sPatternPath) 'OnFlyAdd
        Dim mLimitPos(enmStage.Max) As Premtek.sPos                              '[估算極限位置]
        Dim mCCDFixPosX(enmStage.Max) As Decimal
        Dim mCCDFixPosY(enmStage.Max) As Decimal
        Dim mCCDFixPos(enmStage.Max) As Premtek.sPos


        '[Note]:將所有路徑座標存至暫存器
        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
        mCCDFixPathRegister(stageNo) = New List(Of sCCDFixPath)
        mCCDFixPathRegister(stageNo).Clear()
        mCCDFixPathRegister(stageNo).Capacity = 1

        '[Note]:將所有路徑座標存至暫存器
        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
        mCCDOnFlyPathRegister(stageNo) = New List(Of sPatternPath)
        mCCDOnFlyPathRegister(stageNo).Clear()
        mCCDOnFlyPathRegister(stageNo).Capacity = 1

        For mCount(stageNo) = 0 To ccdFixList.Count - 1
            If EstimateCCDFixPos(stageNo, recipe, ccdFixList(mCount(stageNo)), mCCDFixPos(stageNo)) = True Then
                With mPath(stageNo)
                    .PosX = mCCDFixPos(stageNo).PosX
                    .PosY = mCCDFixPos(stageNo).PosY
                    .PosZ = mCCDFixPos(stageNo).PosZ
                    .AlignScene = ccdFixList(mCount(stageNo)).AlignScene
                    .IndexX = ccdFixList(mCount(stageNo)).IndexX
                    .IndexY = ccdFixList(mCount(stageNo)).IndexY
                    .NodeName = ccdFixList(mCount(stageNo)).NodeName
                    .AlignIndex = ccdFixList(mCount(stageNo)).AlignIndex
                End With
                'With mOnFlyPath(stageNo)
                '    .PathType = ePathType.Line3D
                '    .Line3D.StartPosX()
                'End With
                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                mCCDFixPathRegister(stageNo).Capacity = mCCDFixPathRegister(stageNo).Count + 1
                mCCDFixPathRegister(stageNo).Add(mPath(stageNo))
            Else
                '[Note]:若發生要查原因
                Return False
            End If
        Next
        ccdFixPosRegister = mCCDFixPathRegister(stageNo)
        Return True
    End Function

    ''' <summary>[要做哪一顆]</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="ccdFixPosRegister"></param>
    ''' <param name="startIndex"></param>
    ''' <param name="endIndex"></param>
    ''' <param name="subCCDFixPosRegister"></param>
    ''' <param name="limitPos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EstimateCCDFixPath(ByVal stageNo As enmStage, ByVal ccdFixPosRegister As List(Of sCCDFixPath), ByVal startIndex As Integer, ByRef endIndex As Integer, ByRef subCCDFixPosRegister As List(Of sCCDFixPath), ByRef limitPos As Premtek.sPos) As Boolean

        Dim mLimitPos(enmStage.Max) As Premtek.sPos
        Dim mCCDFixPathRegister(enmStage.Max) As List(Of sCCDFixPath)
        Dim mPath(enmStage.Max) As sCCDFixPath

        mCCDFixPathRegister(stageNo) = New List(Of sCCDFixPath)
        mCCDFixPathRegister(stageNo).Clear()
        mCCDFixPathRegister(stageNo).Capacity = 1


        '[Note]:只會有一個
        mPath(stageNo) = ccdFixPosRegister(startIndex)
        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
        mCCDFixPathRegister(stageNo).Capacity = mCCDFixPathRegister(stageNo).Count + 1
        mCCDFixPathRegister(stageNo).Add(mPath(stageNo))

        mLimitPos(stageNo).PosX = mPath(stageNo).PosX
        mLimitPos(stageNo).PosY = mPath(stageNo).PosY

        subCCDFixPosRegister = mCCDFixPathRegister(stageNo)
        endIndex = startIndex
        limitPos = mLimitPos(stageNo)
        Return True

    End Function

    ''' <summary>
    ''' 產生飛拍I Type路徑
    ''' </summary>
    ''' <param name="stageNo"></param>
    ''' <param name="ccdFixPosRegister"></param>
    ''' <param name="startIndex"></param>
    ''' <param name="endIndex"></param>
    ''' <param name="subCCDFixPosRegister"></param>
    ''' <param name="limitPos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EstimateCCDFixMultiDieOnFly(ByVal stageNo As enmStage, ByVal ccdFixPosRegister As List(Of sCCDFixPath), ByVal startIndex As Integer, ByRef endIndex As Integer, ByRef subCCDFixPosRegister As List(Of sCCDFixPath), ByRef limitPos As Premtek.sPos, Optional ByVal isClearCCDPathRegister As Boolean = True) As Boolean
        Dim mI(enmStage.Max) As Integer
        Dim mTempLimitPos(enmStage.Max) As Premtek.sPos
        Dim mLimitPos(enmStage.Max) As Premtek.sPos
        Dim mCCDFixPathRegister(enmStage.Max) As List(Of sCCDFixPath)
        Dim mPath(enmStage.Max) As sCCDFixPath
        Dim mCCDOnFlyPathRegister(enmStage.Max) As List(Of sPatternPath)

        Dim mVectorX(enmStage.Max) As Decimal
        Dim mVectorY(enmStage.Max) As Decimal
        Dim mStartPos(enmStage.Max) As Premtek.sPos
        Dim mEndPos(enmStage.Max) As Premtek.sPos
        Dim mLastPos(enmStage.Max) As Premtek.sPos

        Dim mdx(enmStage.Max) As Decimal
        Dim mdy(enmStage.Max) As Decimal
        Dim mr(enmStage.Max) As Decimal

        mTempLimitPos(stageNo).PosX = 0
        mTempLimitPos(stageNo).PosY = 0

        If isClearCCDPathRegister = True Then
            mCCDFixPathRegister(stageNo) = New List(Of sCCDFixPath)
            mCCDFixPathRegister(stageNo).Clear()
            mCCDFixPathRegister(stageNo).Capacity = 1
        Else
            '[Note]:保留原有的接續串接起來
            mCCDFixPathRegister(stageNo) = subCCDFixPosRegister
        End If

        '[Note]:至少要有一個
        For mI(stageNo) = startIndex To ccdFixPosRegister.Count - 1
            mPath(stageNo) = ccdFixPosRegister(mI(stageNo))
            'Step1:估算極限位置
            Select Case stageNo
                Case enmStage.No1, enmStage.No3
                    '[Note]:取X最大值
                    If mTempLimitPos(stageNo).PosX < mPath(stageNo).PosX Then
                        mTempLimitPos(stageNo).PosX = mPath(stageNo).PosX
                    End If
                Case enmStage.No2, enmStage.No4
                    '[Note]:取X最小值
                    If mTempLimitPos(stageNo).PosX > mPath(stageNo).PosX Then
                        mTempLimitPos(stageNo).PosX = mPath(stageNo).PosX
                    End If
            End Select
            If mTempLimitPos(stageNo).PosY > mPath(stageNo).PosY Then
                mTempLimitPos(stageNo).PosY = mPath(stageNo).PosY
            End If

            'Step2:比對是否超出保護框架
            Select Case stageNo
                Case enmStage.No1
                    If IsSafePos(stageNo, gProtectData(enmStage.No2), mTempLimitPos(stageNo)) = False Then
                        Exit For
                    End If

                Case enmStage.No2
                    If IsSafePos(stageNo, gProtectData(enmStage.No1), mTempLimitPos(stageNo)) = False Then
                        Exit For
                    End If

                Case enmStage.No3
                    If IsSafePos(stageNo, gProtectData(enmStage.No4), mTempLimitPos(stageNo)) = False Then
                        Exit For
                    End If

                Case enmStage.No4
                    If IsSafePos(stageNo, gProtectData(enmStage.No3), mTempLimitPos(stageNo)) = False Then
                        Exit For
                    End If
            End Select

            'Step3:判斷是否在同一條線上-->從第三點開始
            If mI(stageNo) = startIndex Then
                '[Note]:第一點-->直接加上去
                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                mCCDFixPathRegister(stageNo).Capacity = mCCDFixPathRegister(stageNo).Count + 1
                mCCDFixPathRegister(stageNo).Add(mPath(stageNo))
                endIndex = mI(stageNo)
                '[Note]:更新LimitPos
                mLimitPos(stageNo) = mTempLimitPos(stageNo)
            ElseIf mI(stageNo) = startIndex + 1 Then
                '[Note]:第二點
                mdx(stageNo) = mPath(stageNo).PosX - mLastPos(stageNo).PosX
                mdy(stageNo) = mPath(stageNo).PosY - mLastPos(stageNo).PosY
                mr(stageNo) = CDec(Sqrt(mdx(stageNo) * mdx(stageNo) + mdy(stageNo) * mdy(stageNo)))

                If mr(stageNo) = 0 Then
                    '[Note]:同一點-->停止
                    Exit For
                Else
                    mVectorX(stageNo) = mdx(stageNo) / mr(stageNo)
                    mVectorY(stageNo) = mdy(stageNo) / mr(stageNo)

                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                    mCCDFixPathRegister(stageNo).Capacity = mCCDFixPathRegister(stageNo).Count + 1
                    mCCDFixPathRegister(stageNo).Add(mPath(stageNo))
                    endIndex = mI(stageNo)
                    '[Note]:更新LimitPos
                    mLimitPos(stageNo) = mTempLimitPos(stageNo)
                End If
            Else
                '[Note]:第三點~最後一點
                mdx(stageNo) = mPath(stageNo).PosX - mLastPos(stageNo).PosX
                mdy(stageNo) = mPath(stageNo).PosY - mLastPos(stageNo).PosY
                mr(stageNo) = CDec(Sqrt(mdx(stageNo) * mdx(stageNo) + mdy(stageNo) * mdy(stageNo)))

                If mr(stageNo) = 0 Then
                    '[Note]:同一點-->停止
                    Exit For
                Else
                    '[Note]:向量相同就加了-->同一條線上且同方向
                    If Math.Abs(mVectorX(stageNo) - (mdx(stageNo) / mr(stageNo))) < gSSystemParameter.MotionTolerance And Math.Abs(mVectorY(stageNo) - (mdy(stageNo) / mr(stageNo))) < gSSystemParameter.MotionTolerance Then
                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                        mCCDFixPathRegister(stageNo).Capacity = mCCDFixPathRegister(stageNo).Count + 1
                        mCCDFixPathRegister(stageNo).Add(mPath(stageNo))
                        endIndex = mI(stageNo)
                        '[Note]:更新LimitPos
                        mLimitPos(stageNo) = mTempLimitPos(stageNo)
                    Else
                        '[Note]:不是同方向，就停止
                        Exit For
                    End If
                End If
            End If
            mLastPos(stageNo).PosX = mPath(stageNo).PosX
            mLastPos(stageNo).PosY = mPath(stageNo).PosY
        Next

        '[Note]:至少要有一個，若沒有一個過，那就放一個吧，反正後面還會卡住
        If mCCDFixPathRegister(stageNo).Count = 0 Then
            mPath(stageNo) = ccdFixPosRegister(startIndex)
            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
            mCCDFixPathRegister(stageNo).Capacity = mCCDFixPathRegister(stageNo).Count + 1
            mCCDFixPathRegister(stageNo).Add(mPath(stageNo))

            mTempLimitPos(stageNo).PosX = mPath(stageNo).PosX
            mTempLimitPos(stageNo).PosY = mPath(stageNo).PosY
            '[Note]:更新LimitPos
            mLimitPos(stageNo) = mTempLimitPos(stageNo)
            endIndex = startIndex
        End If
        subCCDFixPosRegister = mCCDFixPathRegister(stageNo)
        limitPos = mLimitPos(stageNo)
        Return True

    End Function



    ''' <summary>[旋轉運算式(只是計算出修正量)]</summary>
    ''' <param name="angle">[旋轉角]</param>
    ''' <param name="cenPos">[旋轉中心]</param>
    ''' <param name="inPos">[預修正點]</param>
    ''' <param name="outPos">[預修正點的修正量]</param>
    ''' <remarks></remarks>
    Private Function ModifyByRotation(ByVal angle As Decimal, ByVal cenPos As Premtek.sPos, ByVal inPos As Premtek.sPos, ByRef outPos As Premtek.sPos) As Boolean

        Dim mCos As Decimal
        Dim mSin As Decimal

        mCos = CosTrigonometricFunction(-1 * angle)
        mSin = SinTrigonometricFunction(-1 * angle)

        'outPos.PosX = CDec(Format((((inPos.PosX - cenPos.PosX) * mCos) - ((inPos.PosY - cenPos.PosY) * mSin)), "0.000"))
        'outPos.PosY = CDec(Format((((inPos.PosX - cenPos.PosX) * mSin) + ((inPos.PosY - cenPos.PosY) * mCos)), "0.000"))

        outPos.PosX = CDec(Format((((inPos.PosX - cenPos.PosX) * mCos) - ((inPos.PosY - cenPos.PosY) * mSin)) + cenPos.PosX, "0.000"))
        outPos.PosY = CDec(Format((((inPos.PosX - cenPos.PosX) * mSin) + ((inPos.PosY - cenPos.PosY) * mCos)) + cenPos.PosY, "0.000"))

        Return True

    End Function


    Private Function IsSamePitch(ByVal mPosX As Decimal, ByVal mPosY As Decimal, ByVal mLastPosX As Decimal, ByVal mLastPosY As Decimal, ByVal SamplePitchX As Decimal, ByVal SamplePitchY As Decimal) As Boolean
        Dim pitchX As Decimal
        Dim pitchY As Decimal

        pitchX = mPosX - mLastPosX
        pitchY = mPosY - mLastPosY

        If Math.Abs(pitchX - SamplePitchX) < gSSystemParameter.MotionTolerance And Math.Abs(pitchY - SamplePitchY) < gSSystemParameter.MotionTolerance Then
            Return True
        End If

        Return False
    End Function

    Private Function CreateDispTriggerCmd(ByVal StartPos As Premtek.sPos, ByVal EndPos As Premtek.sPos, ByVal FixCount As Integer, ByVal mIsExtendOn As Boolean, ByRef mTriggerPathRegister As sPatternPath) As Boolean
        If FixCount = 0 Then
            Return False
        End If
        Dim mTriggerPath As New sPatternPath

        If FixCount = 1 Then
            mTriggerPath.PathType = ePathType.Dot3D
            mTriggerPath.Dot3D.PosX = StartPos.PosX 'mCCDFixPathRegister(stageNo).Item(0).PosX '- mCrossExtendOffset.PosX
            mTriggerPath.Dot3D.PosY = StartPos.PosY 'mCCDFixPathRegister(stageNo).Item(0).PosY '- mCrossExtendOffset.PosY
            mTriggerPath.Dot3D.PosZ = StartPos.PosZ 'mCCDFixPathRegister(stageNo).Item(0).PosZ 'mRegisterPath.Line3D.EndPosZ
            mTriggerPath.Dot3D.Velocity = 0 'gCRecipe.CCDOnFlySpeed
            mTriggerPath.Dot3D.ParameterType = ePathParameterType.Dot
            'mTriggerPath(stageNo).Dot3D.ParameterName = mRegisterPath.Line3D.ParameterName
            mTriggerPath.Dot3D.CcdTiggerCount = FixCount ' 1
            mTriggerPath.Dot3D.IsDispense = True
            mTriggerPath.Dot3D.IsFristPathInDie = True
            mTriggerPath.Dot3D.IsLastRowColumn = True
            mTriggerPath.IsFristPathInDie = True
            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
            'mDispTriggerPathRegister(stageNo).Capacity = mDispTriggerPathRegister.Count + 1
            'mDispTriggerPathRegister(stageNo).Add(mTriggerPath(stageNo))
        Else

            mTriggerPath.PathType = ePathType.Line3D
            '[Note]:起點座標
            mTriggerPath.Line3D.StartPosX = StartPos.PosX  'mTransformPos(stageNo).PosX + mBasicPos(dispParam.StageNo).PosX + mNeedTh(dispParam.StageNo).PosX 'Soni / 2016.11.10 CCD對閥平移應在旋轉後執行
            mTriggerPath.Line3D.StartPosY = StartPos.PosY 'mTransformPos(stageNo).PosY + mBasicPos(dispParam.StageNo).PosY + mNeedTh(dispParam.StageNo).PosY 'Soni / 2016.11.10 CCD對閥平移應在旋轉後執行
            mTriggerPath.Line3D.StartPosZ = StartPos.PosZ '.StartPosZ + mBasicPos(dispParam.StageNo).PosZ
            '[Note]:終點座標
            mTriggerPath.Line3D.EndPosX = EndPos.PosX 'mTransformPos(dispParam.StageNo).PosX + mBasicPos(dispParam.StageNo).PosX + mNeedTh(dispParam.StageNo).PosX 'Soni / 2016.11.10 CCD對閥平移應在旋轉後執行
            mTriggerPath.Line3D.EndPosY = EndPos.PosY 'mTransformPos(dispParam.StageNo).PosY + mBasicPos(dispParam.StageNo).PosY + mNeedTh(dispParam.StageNo).PosY 'Soni / 2016.11.10 CCD對閥平移應在旋轉後執行
            mTriggerPath.Line3D.EndPosZ = EndPos.PosZ '.EndPosZ + mBasicPos(dispParam.StageNo).PosZ
            'mTriggerPath(stageNo).Line3D.WeightControl =
            mTriggerPath.Line3D.CcdTiggerCount = FixCount
            mTriggerPath.Line3D.ParameterType = ePathParameterType.Line
            'mTriggerPath(stageNo).Line3D.ParameterName = .LineParameterName

            mTriggerPath.Line3D.IsDispense = True
            mTriggerPath.Line3D.Velocity = gCRecipe.CCDOnFlySpeed
            mTriggerPath.Line3D.IsFristPathInDie = True 'mIsFristPathInDie(stageNo)
            mTriggerPath.Line3D.IsLastRowColumn = True 'mIsLastRowColumn(stageNo)
            mTriggerPath.Line3D.IsExtendOn = mIsExtendOn
            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
            'mDispTriggerPathRegister(stageNo).Capacity = mDispTriggerPathRegister.Count + 1
            'mDispTriggerPathRegister(stageNo).Add(mTriggerPath(stageNo))
        End If
        mTriggerPathRegister = mTriggerPath
        Return True
    End Function

    ''' <summary>[將所有的座標先串起來，後面再做運算處理(Motion)]</summary>
    Private Function CreateDispList(ByVal stageNo As enmStage, ByVal CCDFixParam As sCCDFixParam, ByVal CCDFixPath As List(Of sCCDFixPath), ByVal startIndex As Integer, ByRef dispPathRegister As List(Of sPatternPath), ByRef dispTriggerPathRegister As List(Of sPatternPath), ByRef limitPos As Premtek.sPos, Optional ByVal isClearDispPathRegister As Boolean = True, Optional ByVal isOnlyGetlimitPos As Boolean = False) As Boolean

        Dim mPath(enmStage.Max) As sPatternPath
        Dim mTriggerPath(enmStage.Max) As sPatternPath

        Dim mI(enmStage.Max) As Integer
        Dim mStartIndex(enmStage.Max) As Integer                         '[從哪一顆開始]
        Dim mEndIndex(enmStage.Max) As Integer                           '[到哪一顆結束]
        Dim mDispPathRegister(enmStage.Max) As List(Of sPatternPath)
        Dim mDispTriggerPathRegister(enmStage.Max) As List(Of sPatternPath)

        Dim mLimitPos(enmStage.Max) As Premtek.sPos                              '[估算極限位置]
        Dim mIsExtendOn(enmStage.Max) As Boolean                            '[強制路徑延伸]
        Dim mRunUp(enmStage.Max) As sExtendParam                            '[助跑資料]
        Dim mMaxExtendDistance(enmStage.Max) As Decimal
        Dim mDotWeight(enmStage.Max) As Decimal                             '[mg]
        Dim mCycleTimes(enmStage.Max) As Decimal                            '[ms]

        Dim SamplePitchX(enmStage.Max) As Decimal
        Dim SamplePitchY(enmStage.Max) As Decimal
        Dim mCCDFixPath(enmStage.Max) As sCCDFixPath
        Dim mStartPos(enmStage.Max) As Premtek.sPos
        Dim mLastPos(enmStage.Max) As Premtek.sPos
        Dim mEndPos(enmStage.Max) As Premtek.sPos
        Dim mPosCount(enmStage.Max) As Integer


        '[Note]:將所有路徑座標存至暫存器
        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
        If isClearDispPathRegister = True Then
            mDispPathRegister(stageNo) = New List(Of sPatternPath)
            mDispPathRegister(stageNo).Clear()
            mDispTriggerPathRegister(stageNo) = New List(Of sPatternPath)
            mDispTriggerPathRegister(stageNo).Clear()
        Else
            '[Note]:保留原有的接續串接起來
            mDispPathRegister(stageNo) = dispPathRegister
            mDispTriggerPathRegister(stageNo) = dispTriggerPathRegister
        End If

        mStartIndex(stageNo) = startIndex
        mEndIndex(stageNo) = CCDFixPath.Count - 1
        mPosCount(stageNo) = 0
        'mStartStepIndex(stageNo) = dispParam.SingleDieParam.StartStep
        'mEndStepIndex(stageNo) = dispParam.SingleDieParam.EndStep

        '[Note]:預設為強制延伸路徑
        mIsExtendOn(stageNo) = True

        '[Note]:處理給Trigger Board的資料(dispTriggerPathRegister)  若不同Pitch分不同線段下 
        For mI(stageNo) = mStartIndex(stageNo) To mEndIndex(stageNo)
            mCCDFixPath(stageNo) = CCDFixPath(mI(stageNo))
            mPosCount(stageNo) = mPosCount(stageNo) + 1
            If mPosCount(stageNo) = 1 Then
                '[Note]:第一點-->直接加上去
                mStartPos(stageNo).PosX = mCCDFixPath(stageNo).PosX
                mStartPos(stageNo).PosY = mCCDFixPath(stageNo).PosY
                mStartPos(stageNo).PosZ = mCCDFixPath(stageNo).PosZ
                mEndPos(stageNo).PosX = mCCDFixPath(stageNo).PosX
                mEndPos(stageNo).PosY = mCCDFixPath(stageNo).PosY
                mEndPos(stageNo).PosZ = mCCDFixPath(stageNo).PosZ
                If mI(stageNo) = mEndIndex(stageNo) Then
                    '[Note]最後一點
                    If CreateDispTriggerCmd(mStartPos(stageNo), mEndPos(stageNo), mPosCount(stageNo), mIsExtendOn(stageNo), mTriggerPath(stageNo)) Then
                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                        mDispTriggerPathRegister(stageNo).Capacity = mDispTriggerPathRegister(stageNo).Count + 1
                        mDispTriggerPathRegister(stageNo).Add(mTriggerPath(stageNo))
                    End If
                End If

            ElseIf mPosCount(stageNo) = 2 Then
                '[Note]:第二點 算Pitch
                SamplePitchX(stageNo) = mCCDFixPath(stageNo).PosX - mLastPos(stageNo).PosX
                SamplePitchY(stageNo) = mCCDFixPath(stageNo).PosY - mLastPos(stageNo).PosY
                mEndPos(stageNo).PosX = mCCDFixPath(stageNo).PosX
                mEndPos(stageNo).PosY = mCCDFixPath(stageNo).PosY
                mEndPos(stageNo).PosZ = mCCDFixPath(stageNo).PosZ
                If mI(stageNo) = mEndIndex(stageNo) Then
                    '[Note]最後一點
                    If CreateDispTriggerCmd(mStartPos(stageNo), mEndPos(stageNo), mPosCount(stageNo), mIsExtendOn(stageNo), mTriggerPath(stageNo)) Then
                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                        mDispTriggerPathRegister(stageNo).Capacity = mDispTriggerPathRegister(stageNo).Count + 1
                        mDispTriggerPathRegister(stageNo).Add(mTriggerPath(stageNo))
                    End If
                End If

            Else
                '[Note]:第三點~最後一點
                If IsSamePitch(mCCDFixPath(stageNo).PosX, mCCDFixPath(stageNo).PosY, mLastPos(stageNo).PosX, mLastPos(stageNo).PosY, SamplePitchX(stageNo), SamplePitchY(stageNo)) Then
                    '[Note]:Pitch相同-->繼續取資料
                    mEndPos(stageNo).PosX = mCCDFixPath(stageNo).PosX
                    mEndPos(stageNo).PosY = mCCDFixPath(stageNo).PosY
                    mEndPos(stageNo).PosZ = mCCDFixPath(stageNo).PosZ
                    If mI(stageNo) = mEndIndex(stageNo) Then
                        '[Note]最後一點
                        If CreateDispTriggerCmd(mStartPos(stageNo), mEndPos(stageNo), mPosCount(stageNo), mIsExtendOn(stageNo), mTriggerPath(stageNo)) Then
                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                            mDispTriggerPathRegister(stageNo).Capacity = mDispTriggerPathRegister(stageNo).Count + 1
                            mDispTriggerPathRegister(stageNo).Add(mTriggerPath(stageNo))
                        End If
                    End If
                Else
                    '[Note]:Pitch不同-->加入TriggerBoard資料
                    mEndPos(stageNo).PosX = mLastPos(stageNo).PosX
                    mEndPos(stageNo).PosY = mLastPos(stageNo).PosY
                    mEndPos(stageNo).PosZ = mLastPos(stageNo).PosZ
                    If CreateDispTriggerCmd(mStartPos(stageNo), mEndPos(stageNo), mPosCount(stageNo) - 1, mIsExtendOn(stageNo), mTriggerPath(stageNo)) Then
                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                        mDispTriggerPathRegister(stageNo).Capacity = mDispTriggerPathRegister(stageNo).Count + 1
                        mDispTriggerPathRegister(stageNo).Add(mTriggerPath(stageNo))
                        mPosCount(stageNo) = 1
                        mStartPos(stageNo).PosX = mCCDFixPath(stageNo).PosX
                        mStartPos(stageNo).PosY = mCCDFixPath(stageNo).PosY
                        mStartPos(stageNo).PosZ = mCCDFixPath(stageNo).PosZ
                        mEndPos(stageNo).PosX = mCCDFixPath(stageNo).PosX
                        mEndPos(stageNo).PosY = mCCDFixPath(stageNo).PosY
                        mEndPos(stageNo).PosZ = mCCDFixPath(stageNo).PosZ
                        If mI(stageNo) = mEndIndex(stageNo) Then
                            '[Note]最後一點
                            If CreateDispTriggerCmd(mStartPos(stageNo), mEndPos(stageNo), mPosCount(stageNo), mIsExtendOn(stageNo), mTriggerPath(stageNo)) Then
                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                mDispTriggerPathRegister(stageNo).Capacity = mDispTriggerPathRegister(stageNo).Count + 1
                                mDispTriggerPathRegister(stageNo).Add(mTriggerPath(stageNo))
                            End If
                        End If
                    End If
                    SamplePitchX(stageNo) = 0
                    SamplePitchY(stageNo) = 0
                End If
            End If
            mLastPos(stageNo).PosX = mCCDFixPath(stageNo).PosX
            mLastPos(stageNo).PosY = mCCDFixPath(stageNo).PosY
        Next





        '[Note]:處理給軸卡的資料(dispPathRegister)
        If CCDFixPath.Count = 1 Then
            mPath(stageNo).PathType = ePathType.Dot3D
        Else
            mPath(stageNo).PathType = ePathType.Line3D
        End If

        Select Case mPath(stageNo).PathType
            Case ePathType.Line3D
                With CCDFixPath
                    '[Note]:起點座標
                    mPath(stageNo).Line3D.StartPosX = .Item(mStartIndex(stageNo)).PosX  'mTransformPos(stageNo).PosX + mBasicPos(dispParam.StageNo).PosX + mNeedTh(dispParam.StageNo).PosX 'Soni / 2016.11.10 CCD對閥平移應在旋轉後執行
                    mPath(stageNo).Line3D.StartPosY = .Item(mStartIndex(stageNo)).PosY 'mTransformPos(stageNo).PosY + mBasicPos(dispParam.StageNo).PosY + mNeedTh(dispParam.StageNo).PosY 'Soni / 2016.11.10 CCD對閥平移應在旋轉後執行
                    mPath(stageNo).Line3D.StartPosZ = .Item(mStartIndex(stageNo)).PosZ '.StartPosZ + mBasicPos(dispParam.StageNo).PosZ
                    '[Note]:終點座標
                    mPath(stageNo).Line3D.EndPosX = .Item(mEndIndex(stageNo)).PosX 'mTransformPos(dispParam.StageNo).PosX + mBasicPos(dispParam.StageNo).PosX + mNeedTh(dispParam.StageNo).PosX 'Soni / 2016.11.10 CCD對閥平移應在旋轉後執行
                    mPath(stageNo).Line3D.EndPosY = .Item(mEndIndex(stageNo)).PosY 'mTransformPos(dispParam.StageNo).PosY + mBasicPos(dispParam.StageNo).PosY + mNeedTh(dispParam.StageNo).PosY 'Soni / 2016.11.10 CCD對閥平移應在旋轉後執行
                    mPath(stageNo).Line3D.EndPosZ = .Item(mEndIndex(stageNo)).PosZ '.EndPosZ + mBasicPos(dispParam.StageNo).PosZ
                    'mPath(stageNo).Line3D.WeightControl =
                    mPath(stageNo).Line3D.CcdTiggerCount = CCDFixPath.Count
                    'mPath(stageNo).Line3D.ParameterType = ePathParameterType.Line
                    'mPath(stageNo).Line3D.ParameterName = .LineParameterName
                End With
                mPath(stageNo).Line3D.IsDispense = True
                mPath(stageNo).Line3D.Velocity = gCRecipe.CCDOnFlySpeed
                mPath(stageNo).Line3D.IsFristPathInDie = isClearDispPathRegister 'True 'mIsFristPathInDie(stageNo)
                mPath(stageNo).Line3D.IsLastRowColumn = True 'mIsLastRowColumn(stageNo)
                mPath(stageNo).Line3D.IsExtendOn = mIsExtendOn(stageNo)

            Case ePathType.Dot3D
                With CCDFixPath
                    mPath(stageNo).Dot3D.PosX = .Item(mStartIndex(stageNo)).PosX 'mCCDFixPathRegister(stageNo).Item(0).PosX '- mCrossExtendOffset.PosX
                    mPath(stageNo).Dot3D.PosY = .Item(mStartIndex(stageNo)).PosY 'mCCDFixPathRegister(stageNo).Item(0).PosY '- mCrossExtendOffset.PosY
                    mPath(stageNo).Dot3D.PosZ = .Item(mStartIndex(stageNo)).PosZ 'mCCDFixPathRegister(stageNo).Item(0).PosZ 'mRegisterPath.Line3D.EndPosZ
                End With
                mPath(stageNo).Dot3D.Velocity = 0 'gCRecipe.CCDOnFlySpeed
                'mPath(stageNo).Dot3D.ParameterType = ePathParameterType.Dot
                'mPath(stageNo).Dot3D.ParameterName = mRegisterPath.Line3D.ParameterName
                mPath(stageNo).Dot3D.CcdTiggerCount = CCDFixPath.Count ' 1
                mPath(stageNo).Dot3D.IsDispense = True
                mPath(stageNo).Dot3D.IsFristPathInDie = isClearDispPathRegister
                mPath(stageNo).IsFristPathInDie = isClearDispPathRegister 'True

        End Select

        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
        mDispPathRegister(stageNo).Capacity = mDispPathRegister(stageNo).Count + 1
        mDispPathRegister(stageNo).Add(mPath(stageNo))
        'If mIsFristPathInDie(stageNo) = True Then
        '    mIsFristPathInDie(stageNo) = False
        'End If

        Call DetermineRunUpTimeDistance(CCDFixParam.Acc, mPath(stageNo), mRunUp(stageNo))
        If mMaxExtendDistance(stageNo) < mRunUp(stageNo).Distance Then
            mMaxExtendDistance(stageNo) = mRunUp(stageNo).Distance
        End If

        '[Note]:估算極限位置
        Select Case stageNo
            Case enmStage.No1, enmStage.No3
                '[Note]:取X最大值
                If mLimitPos(stageNo).PosX < mPath(stageNo).Line3D.StartPosX Then
                    mLimitPos(stageNo).PosX = mPath(stageNo).Line3D.StartPosX
                End If
                If mLimitPos(stageNo).PosX < mPath(stageNo).Line3D.EndPosX Then
                    mLimitPos(stageNo).PosX = mPath(stageNo).Line3D.EndPosX
                End If
            Case enmStage.No2, enmStage.No4
                '[Note]:取X最小值
                If mLimitPos(stageNo).PosX > mPath(stageNo).Line3D.StartPosX Then
                    mLimitPos(stageNo).PosX = mPath(stageNo).Line3D.StartPosX
                End If
                If mLimitPos(stageNo).PosX > mPath(stageNo).Line3D.EndPosX Then
                    mLimitPos(stageNo).PosX = mPath(stageNo).Line3D.EndPosX
                End If
        End Select
        If mLimitPos(stageNo).PosY > mPath(stageNo).Line3D.StartPosY Then
            mLimitPos(stageNo).PosY = mPath(stageNo).Line3D.StartPosY
        End If
        If mLimitPos(stageNo).PosY > mPath(stageNo).Line3D.EndPosY Then
            mLimitPos(stageNo).PosY = mPath(stageNo).Line3D.EndPosY
        End If


        'Step2:從哪一顆做到哪一顆
        'For mI(stageNo) = mDieStartIndex(stageNo) To mDieEndIndex(stageNo)
        '    'Step3:先取出該顆BasicPos，再做處理
        '    With dispParam
        '        mArrayIndexX(stageNo) = .DispList.Item(mI(stageNo)).IndexX
        '        mArrayIndexY(stageNo) = .DispList.Item(mI(stageNo)).IndexY
        '        mIsLastRowColumn(stageNo) = .DispList.Item(mI(stageNo)).IsLastRowColumn
        '    End With

        'mDispensingDie(stageNo).IndexX = mArrayIndexX(stageNo)
        'mDispensingDie(stageNo).IndexY = mArrayIndexY(stageNo)
        'mDispensingDie(stageNo).NodeName = FixParam

        ''[Note]:若是走節點串接，則只有第一個點視為起始的第一點，後面節點串街都視為同一顆。
        'If isClearDispPathRegister = True Then
        '    mIsFristPathInDie(stageNo) = True
        'End If


        '[Note]:更新極限位置(==須加上助跑所使用的延伸路徑長)
        Select Case stageNo
            Case enmStage.No1, enmStage.No3
                '[Note]:取X往正方向延伸
                mLimitPos(stageNo).PosX = mLimitPos(stageNo).PosX + mMaxExtendDistance(stageNo)

            Case enmStage.No2, enmStage.No4
                '[Note]:取X往負方向延伸
                mLimitPos(stageNo).PosX = mLimitPos(stageNo).PosX - mMaxExtendDistance(stageNo)

        End Select
        mLimitPos(stageNo).PosY = mLimitPos(stageNo).PosY + mMaxExtendDistance(stageNo)

        limitPos = mLimitPos(stageNo)
        If isOnlyGetlimitPos = False Then
            dispPathRegister = mDispPathRegister(stageNo)
            dispTriggerPathRegister = mDispTriggerPathRegister(stageNo)
        End If

        Return True

    End Function

    ''' <summary>[計算旋轉角度(透過二個定位點資訊)]</summary>
    ''' <param name="GoldenNo1">[Golden第一點定位點]</param>
    ''' <param name="GoldenNo2">[Golden第二點定位點]</param>
    ''' <param name="RealNo1">[Real第一點定位點]</param>
    ''' <param name="RealNo2">[Real第二點定位點]</param>
    ''' <param name="Angle">[計算後的旋轉量]</param>
    ''' <remarks></remarks>
    Public Sub EstimateRotationByTwoAlign(ByVal GoldenNo1 As Premtek.sPos, ByVal GoldenNo2 As Premtek.sPos, ByVal RealNo1 As Premtek.sPos, ByVal RealNo2 As Premtek.sPos, ByRef angle As Decimal)
        Dim OldVector As Premtek.sPos
        Dim NewVector As Premtek.sPos
        Dim OldVectorDistance As Decimal
        Dim NewVectorDistance As Decimal
        Dim cosTh As Decimal
        Dim tempAngle As Decimal
        Dim Cross As Decimal        '[U x V]-->判定方向-->大於0，表示V在U的正方向
        Dim DotValve As Decimal         '[U。V]
        OldVector.PosX = GoldenNo2.PosX - GoldenNo1.PosX 'a1
        OldVector.PosY = GoldenNo2.PosY - GoldenNo1.PosY 'a2
        NewVector.PosX = RealNo2.PosX - RealNo1.PosX 'b1
        NewVector.PosY = RealNo2.PosY - RealNo1.PosY 'b2

        OldVectorDistance = Math.Sqrt((OldVector.PosX ^ 2) + (OldVector.PosY ^ 2))
        NewVectorDistance = Math.Sqrt((NewVector.PosX ^ 2) + (NewVector.PosY ^ 2))
        '[Note] cosTh=[U。V]/[||U|| ||V||] = (a1b1+a2b2)/( (a1^2+a2^2)^1/2 * (b1^2+b2^2)^1/2 )
        Cross = OldVectorDistance * NewVectorDistance
        DotValve = (OldVector.PosX * NewVector.PosX) + (OldVector.PosY * NewVector.PosY)
        'Debug.Print("EstimateRotationByTwoAlign DotValve: " & DotValve & "  Cross: " & Cross)
        If Cross = 0 Then
            cosTh = 0
        Else
            cosTh = DotValve / Cross
        End If

        '[Note]:保護機制-->cosTh最大為1、最小為-1
        If cosTh > 1 Then cosTh = 1
        If cosTh < -1 Then cosTh = -1

        tempAngle = Math.Acos(cosTh)
        If Cross >= 0 Then
            angle = -tempAngle * 180 / Math.PI
        Else
            angle = tempAngle * 180 / Math.PI
        End If
        'Debug.Print("Acos angle: " & angle)

    End Sub

    ' ''' <summary>[計算旋轉角度(透過二個定位點資訊)]</summary>
    ' ''' <param name="AlignNo1">[第一點定位點]</param>
    ' ''' <param name="AlignNo2">[第二點定位點]</param>
    ' ''' <param name="OffsetNo1">[第一點定位點偏移量]</param>
    ' ''' <param name="OffsetNo2">[第二點定位點偏移量]</param>
    ' ''' <param name="Angle">[計算後的旋轉量]</param>
    ' ''' <remarks></remarks>
    'Public Sub EstimateRotationDegreeByTwoAlign(ByVal alignNo1 As Premtek.sPos, ByVal alignNo2 As Premtek.sPos, ByVal offsetNo1 As Premtek.sPos, ByVal offsetNo2 As Premtek.sPos, ByRef angle As Decimal)

    '    Dim OldVector As Premtek.sPos
    '    Dim NewVector As Premtek.sPos
    '    Dim crossData As Decimal        '[U x V]-->判定方向-->大於0，表示V在U的正方向
    '    Dim dotValve As Decimal         '[U。V]
    '    Dim OldVectorDistance As Decimal
    '    Dim NewVectorDistance As Decimal
    '    'Dim value As Decimal
    '    Dim cosTh As Decimal
    '    Dim tempAngle As Decimal
    '    Dim str As String

    '    OldVector.PosX = alignNo2.PosX - alignNo1.PosX
    '    OldVector.PosY = alignNo2.PosY - alignNo1.PosY
    '    NewVector.PosX = (alignNo2.PosX - offsetNo2.PosX) - (alignNo1.PosX - offsetNo1.PosX)
    '    NewVector.PosY = (alignNo2.PosY - offsetNo2.PosY) - (alignNo1.PosY - offsetNo1.PosY)

    '    '[Note]:'cosTh=[U。V]/[||U|| ||V||]
    '    OldVectorDistance = Math.Sqrt((OldVector.PosX ^ 2) + (OldVector.PosY ^ 2))
    '    NewVectorDistance = Math.Sqrt((NewVector.PosX ^ 2) + (NewVector.PosY ^ 2))
    '    dotValve = (OldVector.PosX * NewVector.PosX) + (OldVector.PosY * NewVector.PosY)
    '    cosTh = dotValve / (OldVectorDistance * NewVectorDistance)

    '    '[Note]:[U x V]>=0，表示V在U的正方向
    '    crossData = (OldVector.PosX * NewVector.PosY) - (OldVector.PosY * NewVector.PosX)

    '    '[Note]:保護機制-->cosTh最大為1、最小為-1
    '    If cosTh > 1 Then cosTh = 1
    '    If cosTh < -1 Then cosTh = -1
    '    tempAngle = Math.Acos(cosTh)

    '    If crossData >= 0 Then
    '        angle = -tempAngle * 180 / Math.PI
    '    Else
    '        angle = tempAngle * 180 / Math.PI
    '    End If
    '    'angle = 0
    '    Debug.Print("Degree: " & angle)

    '    str = "1st X:" & alignNo1.PosX.ToString("0.0000") & " Y:" & alignNo1.PosY.ToString("0.0000") & " Offset X:" & offsetNo1.PosX.ToString("0.0000") & " Y:" & offsetNo1.PosY.ToString("0.0000") &
    '        " 2rd X:" & alignNo2.PosX.ToString("0.0000") & " Y:" & alignNo2.PosY.ToString("0.0000") & " Offset X:" & offsetNo2.PosX.ToString("0.0000") & " Y:" & offsetNo2.PosY.ToString("0.0000") &
    '        " angle:" & angle.ToString("0.0000")

    '    gSyslog.Save("EstimateRotationDegreeByTwoAlign:" & str)

    'End Sub

    ''' <summary>
    ''' 根據不生產點
    ''' </summary>
    ''' <param name="machineNo"></param>
    ''' <param name="stageNo"></param>
    ''' <param name="recipe"></param>
    ''' <param name="nodeName"></param>
    ''' <param name="conveyorNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdatePosBySkipMark(ByVal machineNo As enmMachineStation, ByVal stageNo As enmStage, ByVal recipe As CRecipe, ByVal nodeName As String, Optional ByVal conveyorNo As eConveyor = eConveyor.ConveyorNo1) As Boolean

        Dim mI(enmStage.Max) As Integer
        Dim mJ(enmStage.Max) As Integer
        Dim mK(enmStage.Max) As Integer
        Dim mParentNodeName(enmStage.Max) As String             '[上一層節點名稱]
        Dim mChildNodeName(enmStage.Max) As String
        Dim mChildArrayX(enmStage.Max) As Integer               '[子節點的陣列大小]
        Dim mChildArrayY(enmStage.Max) As Integer
        If recipe.Node(stageNo)(nodeName).SkipMarkEnable = True Then
            For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(nodeName).SRecipePos, 1)
                For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(nodeName).SRecipePos, 2)

                    If gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = True Then
                        '[不生產點成功]此層不定位
                        Select Case recipe.Node(stageNo)(nodeName).AlignType
                            Case enmAlignType.DevicePos1
                                gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No1)) = True
                            Case enmAlignType.DevicePos2
                                gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No2)) = True
                            Case enmAlignType.DevicePos3
                                gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No3)) = True
                        End Select

                        '下一層就直接不用再做了
                        gStageMap(stageNo).Node(nodeName).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = True '[不生產點]
                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = True
                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassDispensingAction = True
                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanGlueAction = True
                        '[Note]:更新目前處理的狀態
                        'gStageMap(stageNo).Node(nodeName).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.AlignFail
                        'gStageMap(stageNo).Node(nodeName).SetCCDLaserStatus(mI(stageNo), mJ(stageNo), eDieStatus.AlignFail)
                        'Call WriteDieStatusForMappingData("GG", eDataType.eNonDispensing, stageNo, nodeName, mI(stageNo), mJ(stageNo), eDieStatus.AlignFail)

                        '[Note]:直接宣告下一層不用做了
                        For mK(stageNo) = 0 To recipe.Node(stageNo)(nodeName).ChildNodes.Count - 1
                            mChildNodeName(stageNo) = recipe.Node(stageNo)(nodeName).ChildNodes(mK(stageNo))
                            For mChildArrayX(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 1)
                                For mChildArrayY(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 2)
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SBinMapData(mChildArrayX(stageNo), mChildArrayY(stageNo)).Disable = True '[不生產點]
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassLaserAction = True
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassDispensingAction = True
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassCCDScanGlueAction = True
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsParentAlignFail = True
                                    '[Note]:更新目前處理的狀態
                                    'gStageMap(stageNo).Node(mChildNodeName(stageNo)).SBinMapData(mChildArrayX(stageNo), mChildArrayY(stageNo)).Status = eDieStatus.AlignFail
                                    'Call WriteDieStatusForMappingData("GG", eDataType.eNonDispensing, stageNo, mChildNodeName(stageNo), mChildArrayX(stageNo), mChildArrayY(stageNo), eDieStatus.AlignFail)
                                Next
                            Next
                        Next

                    End If
                Next
            Next

            Return True
        Else
            Return True
        End If


    End Function

    ''' <summary>[根據定位點資訊修正本層的RealBasicPos、LaserPos、下一層的RealBasicPos、下一層的LaserPos、下一層的ScanPos + 判斷定位結果與後續的動作是否要接續下去]</summary>
    ''' <param name="machineNo"></param>
    ''' <param name="stageNo"></param>
    ''' <param name="recipe"></param>
    ''' <param name="nodeName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdatePosByAlign(ByVal machineNo As enmMachineStation, ByVal stageNo As enmStage, ByVal recipe As CRecipe, ByVal nodeName As String, Optional ByVal conveyorNo As eConveyor = eConveyor.ConveyorNo1) As Boolean

        Dim mI(enmStage.Max) As Integer
        Dim mJ(enmStage.Max) As Integer
        Dim mK(enmStage.Max) As Integer
        Dim mParentNodeName(enmStage.Max) As String             '[上一層節點名稱]
        Dim mCalPos(enmStage.Max) As Premtek.sPos                    '[根據旋轉中心、預修正點、角度求出偏移後的位置]
        Dim mCenter(enmStage.Max) As Premtek.sPos                    '[旋轉中心]
        Dim mPos(enmStage.Max) As Premtek.sPos                       '[預修正點]
        Dim mTh(enmStage.Max) As Decimal                        '[角度]
        Dim mChildNodeName(enmStage.Max) As String
        Dim mChildArrayX(enmStage.Max) As Integer               '[子節點的陣列大小]
        Dim mChildArrayY(enmStage.Max) As Integer
        Dim mAlignNo1(enmStage.Max) As Premtek.sPos
        Dim mAlignNo2(enmStage.Max) As Premtek.sPos
        Dim mRealNo1(enmStage.Max) As Premtek.sPos
        Dim mRealNo2(enmStage.Max) As Premtek.sPos
        Dim mAlignFinish(enmStage.Max) As Boolean               '[用來判斷定位的資料是否正確]
        Dim mL(enmStage.Max) As Integer

        '[Note]:判斷此Node是否使用Align定位
        If recipe.Node(stageNo)(nodeName).AlignmentEnable = True Then
            Select Case recipe.Node(stageNo)(nodeName).AlignType
                Case enmAlignType.DevicePos1
                    For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(nodeName).SRecipePos, 1)
                        For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(nodeName).SRecipePos, 2)
                            If gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).IsParentAlignFail = True Then
                                '[Note]:父層定位異常，子層當然就是為定位失敗並且不做任何修正，只需跟下一層說，父層定位失敗
                                For mK(stageNo) = 0 To recipe.Node(stageNo)(nodeName).ChildNodes.Count - 1
                                    mChildNodeName(stageNo) = recipe.Node(stageNo)(nodeName).ChildNodes(mK(stageNo))
                                    For mChildArrayX(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 1)
                                        For mChildArrayY(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 2)
                                            '[Note]:紀錄這一層CCD水平修正量與修正時所用的旋轉中心，給下一層使用(預留)
                                            gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsParentAlignFail = True
                                            gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassLaserAction = True
                                            gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassDispensingAction = True
                                            gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassCCDScanGlueAction = True
                                        Next
                                    Next
                                Next
                            Else
                                gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosTh = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdRotationAngle
                                If gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No1)) = True Then
                                    '[Note]:若角度大於上限也是判定此顆產品有問題
                                    If Math.Abs(gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosTh) >= gSSystemParameter.CCDAngelLimit Then
                                        mAlignFinish(stageNo) = False
                                        '[Note]:定位的資料若不正確，則清掉CCDFinish，須重做
                                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No1)) = False
                                    Else
                                        mAlignFinish(stageNo) = True
                                    End If
                                Else
                                    mAlignFinish(stageNo) = False
                                    '[Note]:定位的資料若不正確，則清掉CCDFinish，須重做
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No1)) = False
                                End If

                                '[Note]:判斷此顆產品是否定位有問題-->有問題就不需Update
                                If mAlignFinish(stageNo) = True And gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).SKFinish = False Then
                                    '[沒判斷出不生產點且定位成功]
                                    'Step1:修正本層的RealBasicPos、LaserPos
                                    '[Note]:以該層的Golden Pattern作為旋轉中心，根據旋轉角度、RealBasicPos求出RealBasicPos通過旋轉後的數值
                                    mTh(stageNo) = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosTh
                                    mCenter(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosX - gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignmentData(eAlignIndex.No1).AlignOffsetX
                                    mCenter(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosY - gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignmentData(eAlignIndex.No1).AlignOffsetY
                                    mPos(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosX - recipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignBasicOffsetX
                                    mPos(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosY - recipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignBasicOffsetY
                                    Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                    '[Note]:RealBasicPos=RealBasicPosX-修正量  
                                    '       修正量=水平偏移+旋轉偏移
                                    '[Note]:旋轉修正
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosX = mCalPos(stageNo).PosX
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosY = mCalPos(stageNo).PosY
                                    '[Note]:水平修正
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosX + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosY + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                    'gSyslog.CCDSave("BasicPos: " & mCalPos(stageNo).PosX & " , " & mCalPos(stageNo).PosY, CSystemLog.eCCDMessageProcess.Add)

                                    '[Note]:以該層的Golden Pattern作為旋轉中心，根據旋轉角度、LaserPos求出LaserPos通過旋轉後的數值
                                    For mL(stageNo) = 0 To gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).LaserData.Count - 1
                                        mPos(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosX(mL(stageNo))
                                        mPos(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosY(mL(stageNo))
                                        Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                        '[Note]:LaserPos=LaserPos-修正量  
                                        '       修正量=水平偏移+旋轉偏移
                                        '[Note]:旋轉修正
                                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosX(mL(stageNo)) = mCalPos(stageNo).PosX
                                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosY(mL(stageNo)) = mCalPos(stageNo).PosY
                                        '[Note]:水平修正
                                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosX(mL(stageNo)) = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosX(mL(stageNo)) + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosY(mL(stageNo)) = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosY(mL(stageNo)) + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY
                                    Next

                                    'Step2:修正下一層的ScanPos、RealBasicPos
                                    '[Note]:以該層的Golden Pattern作為旋轉中心，根據旋轉角度、下一層RealBasicPos求出下一層RealBasicPos的旋轉修正量
                                    '[Note]:以該層的Golden Pattern作為旋轉中心，根據旋轉角度、下一層ScanPos求出下一層ScanPos的旋轉修正量
                                    mTh(stageNo) = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosTh
                                    mCenter(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosX - gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignmentData(eAlignIndex.No1).AlignOffsetX
                                    mCenter(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosY - gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignmentData(eAlignIndex.No1).AlignOffsetY
                                    '[Note]:對下一個Node只須修正一次即可(針對(0,0)進來做修正)
                                    If mI(stageNo) = 0 And mJ(stageNo) = 0 Then
                                        For mK(stageNo) = 0 To recipe.Node(stageNo)(nodeName).ChildNodes.Count - 1
                                            mChildNodeName(stageNo) = recipe.Node(stageNo)(nodeName).ChildNodes(mK(stageNo))
                                            Dim mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(stageNo)(mChildNodeName(stageNo)).Array)
                                            For mChildArrayX(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 1)
                                                For mChildArrayY(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 2)
                                                    '[Note]:紀錄這一層CCD水平修正量與修正時所用的旋轉中心，給下一層使用(預留)
                                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentCenterPosX = mCenter(stageNo).PosX
                                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentCenterPosY = mCenter(stageNo).PosY
                                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentOffsetX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentOffsetY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY
                                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentTh = mTh(stageNo)
                                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsParentAlignFail = False

                                                    '[Note]:若下一層沒有定位，才修正RealBasicPos位置
                                                    If recipe.Node(stageNo)(mChildNodeName(stageNo)).AlignmentEnable = False Then
                                                        '[Note]::以該層的Golden Pattern作為旋轉中心，根據旋轉角度、下一層RealBasicPos求出下一層RealBasicPos的旋轉修正量
                                                        mPos(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosX - recipe.Node(stageNo)(mChildNodeName(stageNo)).ConveyorPos(conveyorNo).ParentAlignBasicOffsetX + mMultiArrayAdapter.GetMemoryPosX(mChildArrayX(stageNo), mChildArrayY(stageNo))
                                                        mPos(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosY - recipe.Node(stageNo)(mChildNodeName(stageNo)).ConveyorPos(conveyorNo).ParentAlignBasicOffsetY + mMultiArrayAdapter.GetMemoryPosY(mChildArrayX(stageNo), mChildArrayY(stageNo))
                                                        Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                        '[Note]:RealBasicPos=RealBasicPosX-修正量  
                                                        '       修正量=水平偏移+旋轉偏移
                                                        '[Note]:旋轉修正
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosX = mCalPos(stageNo).PosX
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosY = mCalPos(stageNo).PosY
                                                        '[Note]:水平修正
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosX + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosY + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY
                                                    End If


                                                    '[Note]:以該層取像位置作為旋轉中心，根據旋轉角度、下一層LaserPos求出下一層LaserPos通過旋轉後的數值
                                                    For mL(stageNo) = 0 To gCRecipe.Node(stageNo)(mChildNodeName(stageNo)).ConveyorPos(conveyorNo).LaserData.Count - 1
                                                        mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosX(mL(stageNo))
                                                        mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosY(mL(stageNo))
                                                        Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                        '[Note]:LaserPos=LaserPos-修正量  
                                                        '       修正量=水平偏移+旋轉偏移
                                                        '[Note]:旋轉修正
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosX(mL(stageNo)) = mCalPos(stageNo).PosX
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosY(mL(stageNo)) = mCalPos(stageNo).PosY
                                                        '[Note]:水平修正
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosX(mL(stageNo)) = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosX(mL(stageNo)) + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosY(mL(stageNo)) = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosY(mL(stageNo)) + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY
                                                    Next

                                                    '[Note]:判斷此Node是否使用Align定位
                                                    If recipe.Node(stageNo)(mChildNodeName(stageNo)).AlignmentEnable = True Then
                                                        '[Note]:以該層取像位置作為旋轉中心，根據旋轉角度、下一層ScanPos求出下一層ScanPos的旋轉修正量
                                                        Select Case gStageMap(stageNo).Node(mChildNodeName(stageNo)).AlignType
                                                            Case enmAlignType.DevicePos1
                                                                mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX
                                                                mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY
                                                                Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                                '[Note]:ScanPos=ScanPos-修正量  
                                                                '       修正量=水平偏移+旋轉偏移
                                                                '[Note]:旋轉修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX = mCalPos(stageNo).PosX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY = mCalPos(stageNo).PosY
                                                                '[Note]:水平修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                                            Case enmAlignType.DevicePos2
                                                                mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX
                                                                mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY
                                                                Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                                '[Note]:ScanPos=ScanPos-修正量  
                                                                '       修正量=水平偏移+旋轉偏移
                                                                '[Note]:旋轉修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX = mCalPos(stageNo).PosX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY = mCalPos(stageNo).PosY
                                                                '[Note]:水平修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                                                mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2
                                                                mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2
                                                                Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                                '[Note]:ScanPos=ScanPos-修正量  
                                                                '       修正量=水平偏移+旋轉偏移
                                                                '[Note]:旋轉修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2 = mCalPos(stageNo).PosX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2 = mCalPos(stageNo).PosY
                                                                '[Note]:水平修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2 = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2 + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2 = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2 + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                                            Case enmAlignType.DevicePos3
                                                                mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX
                                                                mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY
                                                                Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                                '[Note]:ScanPos=ScanPos-修正量  
                                                                '       修正量=水平偏移+旋轉偏移
                                                                '[Note]:旋轉修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX = mCalPos(stageNo).PosX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY = mCalPos(stageNo).PosY
                                                                '[Note]:水平修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                                                mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2
                                                                mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2
                                                                Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                                '[Note]:ScanPos=ScanPos-修正量  
                                                                '       修正量=水平偏移+旋轉偏移
                                                                '[Note]:旋轉修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2 = mCalPos(stageNo).PosX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2 = mCalPos(stageNo).PosY
                                                                '[Note]:水平修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2 = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2 + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2 = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2 + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                                                mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX3
                                                                mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY3
                                                                Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                                '[Note]:ScanPos=ScanPos-修正量  
                                                                '       修正量=水平偏移+旋轉偏移
                                                                '[Note]:旋轉修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX3 = mCalPos(stageNo).PosX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY3 = mCalPos(stageNo).PosY
                                                                '[Note]:水平修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX3 = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX3 + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY3 = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY3 + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                                        End Select
                                                    End If
                                                Next
                                            Next
                                        Next
                                    End If
                                Else
                                    '[Note]:這一層定位失敗，下一層就直接不用再做了
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = True
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassDispensingAction = True
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanGlueAction = True
                                    '[Note]:更新目前處理的狀態
                                    'gStageMap(stageNo).Node(nodeName).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.AlignFail

                                    gStageMap(stageNo).Node(nodeName).SetCCDLaserStatus(mI(stageNo), mJ(stageNo), eDieStatus.AlignFail)
                                    Call WriteDieStatusForMappingData("GG", eDataType.eNonDispensing, stageNo, nodeName, mI(stageNo), mJ(stageNo), eDieStatus.AlignFail)

                                    '[Note]:直接宣告下一層不用做了
                                    For mK(stageNo) = 0 To recipe.Node(stageNo)(nodeName).ChildNodes.Count - 1
                                        mChildNodeName(stageNo) = recipe.Node(stageNo)(nodeName).ChildNodes(mK(stageNo))
                                        For mChildArrayX(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 1)
                                            For mChildArrayY(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 2)
                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassLaserAction = True
                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassDispensingAction = True
                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassCCDScanGlueAction = True
                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsParentAlignFail = True
                                                '[Note]:更新目前處理的狀態
                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SBinMapData(mChildArrayX(stageNo), mChildArrayY(stageNo)).Status = eDieStatus.AlignFail
                                                Call WriteDieStatusForMappingData("GG", eDataType.eNonDispensing, stageNo, mChildNodeName(stageNo), mChildArrayX(stageNo), mChildArrayY(stageNo), eDieStatus.AlignFail)
                                            Next
                                        Next
                                    Next
                                End If
                            End If
                        Next
                    Next
                    Return True

                Case enmAlignType.DevicePos2
                    For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(nodeName).SRecipePos, 1)
                        For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(nodeName).SRecipePos, 2)
                            If gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).IsParentAlignFail = True Then
                                '[Note]:父層定位異常，子層當然就是為定位失敗並且不做任何修正，只需跟下一層說，父層定位失敗
                                For mK(stageNo) = 0 To recipe.Node(stageNo)(nodeName).ChildNodes.Count - 1
                                    mChildNodeName(stageNo) = recipe.Node(stageNo)(nodeName).ChildNodes(mK(stageNo))
                                    For mChildArrayX(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 1)
                                        For mChildArrayY(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 2)
                                            '[Note]:紀錄這一層CCD水平修正量與修正時所用的旋轉中心，給下一層使用(預留)
                                            gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsParentAlignFail = True
                                            gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassLaserAction = True
                                            gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassDispensingAction = True
                                            gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassCCDScanGlueAction = True
                                        Next
                                    Next
                                Next
                            Else
                                '[Note]:先求出旋轉角度(By ScanPos1 ScanPos2)
                                '[Note]:在計算角度之前，先確認完成影像定位
                                If gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No1)) = True Then
                                    If gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No2)) = True Then
                                        mAlignNo1(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosX - gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignmentData(eAlignIndex.No1).AlignOffsetX
                                        mAlignNo1(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosY - gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignmentData(eAlignIndex.No1).AlignOffsetY
                                        mAlignNo2(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosX2 - gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignmentData(eAlignIndex.No2).AlignOffsetX
                                        mAlignNo2(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosY2 - gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignmentData(eAlignIndex.No2).AlignOffsetY
                                        mRealNo1(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosX - gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                        mRealNo1(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosY - gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY
                                        mRealNo2(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosX2 - gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDOffsetX2
                                        mRealNo2(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosY2 - gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDOffsetY2
                                        Call EstimateRotationByTwoAlign(mAlignNo1(stageNo), mAlignNo2(stageNo), mRealNo1(stageNo), mRealNo2(stageNo), mTh(stageNo))
                                        '[Note]:若忽略旋轉，只修正XY偏移
                                        If gCRecipe.BypassRotationCorrection = True Then
                                            gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosTh = 0
                                        Else
                                            gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosTh = mTh(stageNo)
                                        End If

                                        '[Note]:若角度大於上限也是判定此顆產品有問題
                                        If Math.Abs(gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosTh) >= gSSystemParameter.CCDAngelLimit Then
                                            mAlignFinish(stageNo) = False
                                            '[Note]:定位的資料若不正確，則清掉CCDFinish，須重做
                                            gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No1)) = False
                                            gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No2)) = False
                                        Else
                                            mAlignFinish(stageNo) = True
                                        End If
                                    Else
                                        mAlignFinish(stageNo) = False
                                        '[Note]:定位的資料若不正確，則清掉CCDFinish，須重做
                                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No1)) = False
                                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No2)) = False
                                    End If
                                Else
                                    mAlignFinish(stageNo) = False
                                    '[Note]:定位的資料若不正確，則清掉CCDFinish，須重做
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No1)) = False
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CCDFinish(CInt(eAlignIndex.No2)) = False
                                End If

                                '[Note]:利用CCDFinish來判斷此顆產品是否定位有問題-->有問題就不需Update
                                If mAlignFinish(stageNo) = True Then
                                    'Step1:修正本層的RealBasicPos
                                    '[Note]:以該層的Golden Pattern作為旋轉中心，根據旋轉角度、RealBasicPos求出RealBasicPos的旋轉修正量
                                    mTh(stageNo) = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosTh
                                    mCenter(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosX - gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignmentData(eAlignIndex.No1).AlignOffsetX
                                    mCenter(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosY - gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignmentData(eAlignIndex.No1).AlignOffsetY
                                    mPos(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosX - recipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignBasicOffsetX
                                    mPos(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosY - recipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignBasicOffsetY
                                    Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                    '[Note]:RealBasicPos=RealBasicPosX-修正量  
                                    '       修正量=水平偏移+旋轉偏移
                                    '[Note]:旋轉修正
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosX = mCalPos(stageNo).PosX
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosY = mCalPos(stageNo).PosY
                                    '[Note]:水平修正
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosX + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosY + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY
                                    gSyslog.CCDSave("BasicPos: " & mCalPos(stageNo).PosX & " , " & mCalPos(stageNo).PosY, CSystemLog.eCCDMessageProcess.Add)

                                    '[Note]:以該層的Golden Pattern作為旋轉中心，根據旋轉角度、LaserPos求出LaserPos通過旋轉後的數值
                                    For mL(stageNo) = 0 To gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).LaserData.Count - 1
                                        mPos(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosX(mL(stageNo))
                                        mPos(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosY(mL(stageNo))
                                        Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                        '[Note]:LaserPos=LaserPos-修正量  
                                        '       修正量=水平偏移+旋轉偏移
                                        '[Note]:旋轉修正
                                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosX(mL(stageNo)) = mCalPos(stageNo).PosX
                                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosY(mL(stageNo)) = mCalPos(stageNo).PosY
                                        '[Note]:水平修正
                                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosX(mL(stageNo)) = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosX(mL(stageNo)) + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosY(mL(stageNo)) = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosY(mL(stageNo)) + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY
                                    Next

                                    'Step2:修正下一層的ScanPos、RealBasicPos
                                    '[Note]:以該層的Golden Pattern作為旋轉中心，根據旋轉角度、下一層RealBasicPos求出下一層RealBasicPos的旋轉修正量
                                    '[Note]:以該層的Golden Pattern作為旋轉中心，根據旋轉角度、下一層ScanPos求出下一層ScanPos的旋轉修正量
                                    mTh(stageNo) = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosTh
                                    mCenter(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosX - gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignmentData(eAlignIndex.No1).AlignOffsetX
                                    mCenter(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosY - gCRecipe.Node(stageNo)(nodeName).ConveyorPos(conveyorNo).AlignmentData(eAlignIndex.No1).AlignOffsetY
                                    '[Note]:對下一個Node只須修正一次即可(針對(0,0)進來做修正)
                                    If mI(stageNo) = 0 And mJ(stageNo) = 0 Then
                                        For mK(stageNo) = 0 To recipe.Node(stageNo)(nodeName).ChildNodes.Count - 1
                                            mChildNodeName(stageNo) = recipe.Node(stageNo)(nodeName).ChildNodes(mK(stageNo))
                                            Dim mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(stageNo)(mChildNodeName(stageNo)).Array)
                                            For mChildArrayX(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 1)
                                                For mChildArrayY(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 2)
                                                    '[Note]:紀錄這一層CCD水平修正量與修正時所用的旋轉中心，給下一層使用(預留)
                                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentCenterPosX = mCenter(stageNo).PosX
                                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentCenterPosY = mCenter(stageNo).PosY
                                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentOffsetX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentOffsetY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY
                                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentTh = mTh(stageNo)
                                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsParentAlignFail = False

                                                    '[Note]:若下一層沒有定位，才修正RealBasicPos位置
                                                    If recipe.Node(stageNo)(mChildNodeName(stageNo)).AlignmentEnable = False Then
                                                        mPos(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosX - recipe.Node(stageNo)(mChildNodeName(stageNo)).ConveyorPos(conveyorNo).ParentAlignBasicOffsetX + mMultiArrayAdapter.GetMemoryPosX(mChildArrayX(stageNo), mChildArrayY(stageNo))
                                                        mPos(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ScanPosY - recipe.Node(stageNo)(mChildNodeName(stageNo)).ConveyorPos(conveyorNo).ParentAlignBasicOffsetY + mMultiArrayAdapter.GetMemoryPosY(mChildArrayX(stageNo), mChildArrayY(stageNo))
                                                        Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                        '[Note]:RealBasicPos=RealBasicPosX-修正量  
                                                        '       修正量=水平偏移+旋轉偏移
                                                        '[Note]:旋轉修正
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosX = mCalPos(stageNo).PosX
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosY = mCalPos(stageNo).PosY
                                                        '[Note]:水平修正
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosX + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosY + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY
                                                    End If

                                                    '[Note]:以該層的Golden Pattern作為旋轉中心，根據旋轉角度、下一層LaserPos求出下一層LaserPos通過旋轉後的數值
                                                    For mL(stageNo) = 0 To gCRecipe.Node(stageNo)(mChildNodeName(stageNo)).ConveyorPos(conveyorNo).LaserData.Count - 1
                                                        mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosX(mL(stageNo))
                                                        mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosY(mL(stageNo))
                                                        Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                        '[Note]:LaserPos=LaserPos-修正量  
                                                        '       修正量=水平偏移+旋轉偏移
                                                        '[Note]:旋轉修正
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosX(mL(stageNo)) = mCalPos(stageNo).PosX
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosY(mL(stageNo)) = mCalPos(stageNo).PosY
                                                        '[Note]:水平修正
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosX(mL(stageNo)) = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosX(mL(stageNo)) + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosY(mL(stageNo)) = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosY(mL(stageNo)) + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY
                                                    Next

                                                    '[Note]:判斷此Node是否使用Align定位
                                                    If recipe.Node(stageNo)(mChildNodeName(stageNo)).AlignmentEnable = True Then
                                                        '[Note]:以該層取像位置作為旋轉中心，根據旋轉角度、下一層ScanPos求出下一層ScanPos的旋轉修正量
                                                        Select Case gStageMap(stageNo).Node(mChildNodeName(stageNo)).AlignType
                                                            Case enmAlignType.DevicePos1
                                                                mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX
                                                                mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY
                                                                Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                                '[Note]:ScanPos=ScanPos-修正量  
                                                                '       修正量=水平偏移+旋轉偏移
                                                                '[Note]:旋轉修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX = mCalPos(stageNo).PosX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY = mCalPos(stageNo).PosY
                                                                '[Note]:水平修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                                            Case enmAlignType.DevicePos2
                                                                mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX
                                                                mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY
                                                                Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                                '[Note]:ScanPos=ScanPos-修正量  
                                                                '       修正量=水平偏移+旋轉偏移
                                                                '[Note]:旋轉修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX = mCalPos(stageNo).PosX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY = mCalPos(stageNo).PosY
                                                                '[Note]:水平修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                                                mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2
                                                                mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2
                                                                Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                                '[Note]:ScanPos=ScanPos-修正量  
                                                                '       修正量=水平偏移+旋轉偏移
                                                                '[Note]:旋轉修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2 = mCalPos(stageNo).PosX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2 = mCalPos(stageNo).PosY
                                                                '[Note]:水平修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2 = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2 + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2 = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2 + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                                            Case enmAlignType.DevicePos3
                                                                mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX
                                                                mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY
                                                                Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                                '[Note]:ScanPos=ScanPos-修正量  
                                                                '       修正量=水平偏移+旋轉偏移
                                                                '[Note]:旋轉修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX = mCalPos(stageNo).PosX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY = mCalPos(stageNo).PosY
                                                                '[Note]:水平修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                                                mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2
                                                                mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2
                                                                Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                                '[Note]:ScanPos=ScanPos-修正量  
                                                                '       修正量=水平偏移+旋轉偏移
                                                                '[Note]:旋轉修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2 = mCalPos(stageNo).PosX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2 = mCalPos(stageNo).PosY
                                                                '[Note]:水平修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2 = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX2 + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2 = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY2 + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                                                mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX3
                                                                mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY3
                                                                Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                                                '[Note]:ScanPos=ScanPos-修正量  
                                                                '       修正量=水平偏移+旋轉偏移
                                                                '[Note]:旋轉修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX3 = mCalPos(stageNo).PosX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY3 = mCalPos(stageNo).PosY
                                                                '[Note]:水平修正
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX3 = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosX3 + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY3 = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ScanPosY3 + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                                        End Select
                                                    End If
                                                Next
                                            Next
                                        Next
                                    End If
                                Else
                                    '[Note]:這一層定位失敗，下一層就直接不用再做了
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = True
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassDispensingAction = True
                                    gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassCCDScanGlueAction = True
                                    '[Note]:更新目前處理的狀態
                                    'gStageMap(stageNo).Node(nodeName).SBinMapData(mI(stageNo), mJ(stageNo)).Status = eDieStatus.AlignFail
                                    gStageMap(stageNo).Node(nodeName).SetCCDLaserStatus(mI(stageNo), mJ(stageNo), eDieStatus.AlignFail)

                                    Call WriteDieStatusForMappingData("GG", eDataType.eNonDispensing, stageNo, nodeName, mI(stageNo), mJ(stageNo), eDieStatus.AlignFail)

                                    '[Note]:直接宣告下一層不用做了
                                    For mK(stageNo) = 0 To recipe.Node(stageNo)(nodeName).ChildNodes.Count - 1
                                        mChildNodeName(stageNo) = recipe.Node(stageNo)(nodeName).ChildNodes(mK(stageNo))
                                        For mChildArrayX(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 1)
                                            For mChildArrayY(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 2)
                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassLaserAction = True
                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassDispensingAction = True
                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassCCDScanGlueAction = True
                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsParentAlignFail = True
                                                '[Note]:更新目前處理的狀態
                                                gStageMap(stageNo).Node(mChildNodeName(stageNo)).SBinMapData(mChildArrayX(stageNo), mChildArrayY(stageNo)).Status = eDieStatus.AlignFail
                                                Call WriteDieStatusForMappingData("GG", eDataType.eNonDispensing, stageNo, mChildNodeName(stageNo), mChildArrayX(stageNo), mChildArrayY(stageNo), eDieStatus.AlignFail)
                                            Next
                                        Next
                                    Next
                                End If
                            End If
                        Next
                    Next
                    Return True

                Case enmAlignType.DevicePos3
                    '[Note]:目前不支援
                    Return False

                Case Else
                    Return False

            End Select
        Else
            '[Note]:本層的RealBasicPos與下一層的ScanPos是沒辦法修的
            '[Note]:只能根據上一層的資料來修正下一層的BasicPos、LaserPos
            '[Note]:以該層的Golden Pattern作為旋轉中心，根據旋轉角度、下一層RealBasicPos求出下一層RealBasicPos的旋轉修正量
            For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(nodeName).SRecipePos, 1)
                For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(nodeName).SRecipePos, 2)
                    '[Note]:拿上一層的修正資料來看(上一層的修正會先預塞在ParentOffset、ParentTh、IsParentAlignFail)
                    If gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).IsParentAlignFail = True Then
                        '[Note]:父層定位異常，子層當然就是為定位失敗並且不做任何修正，只需跟下一層說，父層定位失敗
                        For mK(stageNo) = 0 To recipe.Node(stageNo)(nodeName).ChildNodes.Count - 1
                            mChildNodeName(stageNo) = recipe.Node(stageNo)(nodeName).ChildNodes(mK(stageNo))
                            For mChildArrayX(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 1)
                                For mChildArrayY(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 2)
                                    '[Note]:紀錄這一層CCD水平修正量與修正時所用的旋轉中心，給下一層使用(預留)
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsParentAlignFail = True
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassLaserAction = True
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassDispensingAction = True
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsByPassCCDScanGlueAction = True
                                Next
                            Next
                        Next
                    Else
                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).RealBasicPosTh = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ParentTh
                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ParentOffsetX
                        gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ParentOffsetY
                        '[Note]:以該層的Golden Pattern作為旋轉中心，根據旋轉角度、下一層RealBasicPos求出下一層RealBasicPos的旋轉修正量
                        mTh(stageNo) = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ParentTh
                        mCenter(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ParentCenterPosX
                        mCenter(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ParentCenterPosY

                        'Step1:修正下一層的RealBasicPos、LaserPos
                        For mK(stageNo) = 0 To recipe.Node(stageNo)(nodeName).ChildNodes.Count - 1
                            mChildNodeName(stageNo) = recipe.Node(stageNo)(nodeName).ChildNodes(mK(stageNo))
                            Dim mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(stageNo)(mChildNodeName(stageNo)).Array)
                            For mChildArrayX(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 1)
                                For mChildArrayY(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos, 2)
                                    '[Note]:紀錄這一層CCD水平修正量與修正時所用的旋轉中心，給下一層使用(預留)
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentCenterPosX = mCenter(stageNo).PosX
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentCenterPosY = mCenter(stageNo).PosY
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentOffsetX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentOffsetY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).ParentTh = mTh(stageNo)
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).IsParentAlignFail = False

                                    '[Note]:以該層的Golden Pattern作為旋轉中心，根據旋轉角度、下一層RealBasicPos求出下一層RealBasicPos的旋轉修正量
                                    mPos(stageNo).PosX = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ParentCenterPosX - recipe.Node(stageNo)(mChildNodeName(stageNo)).ConveyorPos(conveyorNo).ParentAlignBasicOffsetX + mMultiArrayAdapter.GetMemoryPosX(mChildArrayX(stageNo), mChildArrayY(stageNo))
                                    mPos(stageNo).PosY = gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).ParentCenterPosY - recipe.Node(stageNo)(mChildNodeName(stageNo)).ConveyorPos(conveyorNo).ParentAlignBasicOffsetY + mMultiArrayAdapter.GetMemoryPosY(mChildArrayX(stageNo), mChildArrayY(stageNo))
                                    Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))
                                    '[Note]:RealBasicPos=RealBasicPosX-修正量  
                                    '       修正量=水平偏移+旋轉偏移
                                    '[Note]:旋轉修正
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosX = mCalPos(stageNo).PosX
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosY = mCalPos(stageNo).PosY
                                    '[Note]:水平修正
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosX + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                    gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).RealBasicPosY + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY

                                    '[Note]:以該層的Golden Pattern作為旋轉中心，根據旋轉角度、下一層LaserPos求出下一層LaserPos通過旋轉後的數值
                                    For mL(stageNo) = 0 To gCRecipe.Node(stageNo)(mChildNodeName(stageNo)).ConveyorPos(conveyorNo).LaserData.Count - 1
                                        mPos(stageNo).PosX = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosX(mL(stageNo))
                                        mPos(stageNo).PosY = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosY(mL(stageNo))
                                        Call ModifyByRotation(mTh(stageNo), mCenter(stageNo), mPos(stageNo), mCalPos(stageNo))

                                        '[Note]:RealBasicPos=RealBasicPosX-修正量  
                                        '       修正量=水平偏移+旋轉偏移
                                        '[Note]:旋轉修正
                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosX(mL(stageNo)) = mCalPos(stageNo).PosX
                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosY(mL(stageNo)) = mCalPos(stageNo).PosY
                                        '[Note]:水平修正
                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosX(mL(stageNo)) = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosX(mL(stageNo)) + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetX
                                        gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosY(mL(stageNo)) = gStageMap(stageNo).Node(mChildNodeName(stageNo)).SRecipePos(mChildArrayX(stageNo), mChildArrayY(stageNo)).LaserPosY(mL(stageNo)) + gStageMap(stageNo).Node(nodeName).SRecipePos(mI(stageNo), mJ(stageNo)).CcdOffsetY
                                    Next
                                Next
                            Next
                        Next
                    End If
                Next
            Next

            Return True
        End If

    End Function


    ''' <summary>[更新CCD Fix Reselt]</summary>
    ''' <param name="cCDNo"></param>
    ''' <param name="stageNo"></param>
    ''' <param name="cCDFixPathRegister"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateCCDFixReselt(ByVal cCDNo As Integer, ByVal stageNo As enmStage, ByVal cCDFixPathRegister As List(Of sCCDFixPath), Optional ByVal conveyorNo As eConveyor = eConveyor.ConveyorNo1) As eCCDAlginStatus

        Dim mI(enmStage.Max) As Integer
        Dim mTicket(enmStage.Max) As Integer

        For mI(stageNo) = 0 To cCDFixPathRegister.Count - 1
            With cCDFixPathRegister(mI(stageNo))
                Select Case .AlignIndex
                    Case eAlignIndex.No1
                        mTicket(stageNo) = gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).Ticket

                    Case eAlignIndex.No2
                        mTicket(stageNo) = gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).Ticket2

                    Case eAlignIndex.No3
                        mTicket(stageNo) = gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).Ticket3

                    Case eAlignIndex.SkipMark
                        mTicket(stageNo) = gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).SkipMarkTicket
                End Select

                '[Note]:確認影像進入運算
                If gCCDAlignResultDict(cCDNo).ContainsKey(mTicket(stageNo)) = False Then
                    '[Note]:尚未處理完，基本上不可能發生，若發生表示有問題
                    Return eCCDAlginStatus.eBusy
                End If

                '[Note]:確認影像運算完成
                If gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).IsRunSuccess = False Then
                    '[Note]:尚未處理完，基本上不可能發生，若發生表示有問題
                    Return eCCDAlginStatus.eBusy
                End If

                '[Note]:是否忽略運算結果
                If gCRecipe.BypassCCDResult = True Then
                    Select Case .AlignIndex
                        Case eAlignIndex.No1
                            gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDFinish(.AlignIndex) = True
                            gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CcdOffsetX = 0
                            gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CcdOffsetY = 0
                            gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CcdRotationAngle = 0

                        Case eAlignIndex.No2
                            gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDFinish(.AlignIndex) = True
                            gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDOffsetX2 = 0
                            gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDOffsetY2 = 0

                        Case eAlignIndex.No3
                            gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDFinish(.AlignIndex) = True
                            gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDOffsetX3 = 0
                            gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDOffsetY3 = 0

                        Case eAlignIndex.SkipMark
                            '[Note]不生產點不需要補正
                            gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).SKFinish = False
                    End Select
                Else
                    '[Note]:確認運算結果(目前只支援單顆，不允許多顆)

                    '[Note]gCCDAlignResultDict(ccdNo)(ticket).Result.Count 為cognex工具原始資料，不能修改
                    '[Note]gCCDAlignResultDict(ccdNo)(ticket).Count 為軟體紀錄資料
                    If gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result.Count = 1 Then

                        Select Case .AlignIndex
                            Case eAlignIndex.No1
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDFinish(.AlignIndex) = True
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CcdOffsetX = gCRecipe.Node(stageNo)(.NodeName).ConveyorPos(conveyorNo).AlignmentData(.AlignIndex).AlignOffsetX - gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0).AbsOffsetX
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CcdOffsetY = gCRecipe.Node(stageNo)(.NodeName).ConveyorPos(conveyorNo).AlignmentData(.AlignIndex).AlignOffsetY - gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0).AbsOffsetY
                                If gCRecipe.BypassRotationCorrection = True Then
                                    gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CcdRotationAngle = 0
                                Else
                                    gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CcdRotationAngle = gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0).Rotation - gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0).Rotation
                                End If

                            Case eAlignIndex.No2
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDFinish(.AlignIndex) = True
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDOffsetX2 = gCRecipe.Node(stageNo)(.NodeName).ConveyorPos(conveyorNo).AlignmentData(.AlignIndex).AlignOffsetX - gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0).AbsOffsetX
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDOffsetY2 = gCRecipe.Node(stageNo)(.NodeName).ConveyorPos(conveyorNo).AlignmentData(.AlignIndex).AlignOffsetY - gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0).AbsOffsetY


                            Case eAlignIndex.No3
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDFinish(.AlignIndex) = True
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDOffsetX3 = gCRecipe.Node(stageNo)(.NodeName).ConveyorPos(conveyorNo).AlignmentData(.AlignIndex).AlignOffsetX - gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0).AbsOffsetX
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDOffsetY3 = gCRecipe.Node(stageNo)(.NodeName).ConveyorPos(conveyorNo).AlignmentData(.AlignIndex).AlignOffsetY - gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0).AbsOffsetY


                            Case eAlignIndex.SkipMark '不生產點
                                '[Note]:判斷是否有BadMark 有則該產品bypass不做後續動作，若無則繼續資料填入
                                '[Note需確認]CCDFinish 與 IsByPassCCDScanFixAction
                                gStageMap(stageNo).Node(.NodeName).SBinMapData(.IndexX, .IndexY).Disable = True '[Note]不生產點顯示
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).SKFinish = True
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).IsByPassLaserAction = True
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).IsByPassDispensingAction = True
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).IsByPassCCDScanGlueAction = True
                        End Select
                        'Debug.Print("OFFSET: " & .IndexX & " , " & .IndexY & " , " & gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0).AbsOffsetX & " , " & gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0).AbsOffsetY)
                        gSyslog.CCDSave("CCDFixReselt: " & .IndexX & " , " & .IndexY & " , " & gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0).PixelTranslationX & " , " & gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0).PixelTranslationY, CSystemLog.eCCDMessageProcess.Add)

                    Else
                        '[Note]:定位失敗

                        Select Case .AlignIndex
                            Case eAlignIndex.SkipMark
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).SKFinish = False
                            Case Else
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).CCDFinish(.AlignIndex) = False
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).IsByPassLaserAction = True
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).IsByPassDispensingAction = True
                                gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).IsByPassCCDScanGlueAction = True
                        End Select


                        If gCRecipe.IsStopAlignNG = True Then
                            Return eCCDAlginStatus.eNGStop
                        End If
                    End If
                End If
            End With
        Next
        Return eCCDAlginStatus.eOK
    End Function

    Public Sub SaveCCDFixReselt(ByVal AlignIndex As eAlignIndex, ByVal CCDNo As Integer, ByVal CCDRound As Integer, ByVal IndexX As Integer, ByVal IndexY As Integer, ByVal Count As Integer, ByVal CCDFixResult As sAlignResult) 'ByVal PixelX As Decimal, ByVal PixelY As Decimal)
        Dim e As New Object

        SyncLock e
            Dim mStartTime As String = Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & " " &
            Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00")
            Dim mEndTime As String = " "
            Dim mDuringTime As String = " "
            Dim folderName As String
            Dim fileName As String

            Dim sw As System.IO.StreamWriter
            '檔案路徑確認是否存在
            folderName = "D:\PIIData\DataLog\" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & "\"

            If Not System.IO.Directory.Exists(folderName) Then
                System.IO.Directory.CreateDirectory(folderName)
            End If
            fileName = folderName & "CCDFixReselt" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & "_" & (CCDNo + 1) & "_" & CCDRound & ".csv"

            'sw = New IO.StreamWriter(fileName, False)


            Dim data As String
            If Count = 1 Then
                data = mStartTime &
                "," & IndexX &
                "," & IndexY &
                "," & AlignIndex &
                "," & Count &
                "," & CCDFixResult.PixelTranslationX &
                "," & CCDFixResult.PixelTranslationY &
                "," & CCDFixResult.Rotation &
                "," & CCDFixResult.Score &
                ","
            Else
                data = mStartTime &
                "," & IndexX &
                "," & IndexY &
                "," & AlignIndex &
                "," & Count &
                "," & 0 &
                "," & 0 &
                "," & 0 &
                "," & 0 &
                ","
            End If


            '檔案存在
            If System.IO.File.Exists(fileName) Then
                Dim fInfo As New System.IO.FileInfo(fileName)
                sw = New IO.StreamWriter(fileName, True)
                sw.WriteLineAsync(data)
                sw.Close()
            Else
                '檔案不存在,另開新檔
                sw = New IO.StreamWriter(fileName, False)
                '項次 

                Dim header As String = "StartTime" & ",IndexX" & ",IndexY" & ",AlignIndex" & ",Count" & ",PixelX" & ",PixelY" & ",Rotation" & ",Score"
                sw.WriteLineAsync(header)
                sw.WriteLineAsync(data)
                sw.Close()
            End If

        End SyncLock

    End Sub

    Dim TestCount As Integer
    Dim CCDRound(enmStage.Max) As Integer
    ''' <summary>[CCDScanFix動作流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub DispStage_CCDFixAction(ByRef sys As sSysParam)
        Static mSYS(enmStage.Max) As sCCDFixSysParam                                        '[CCD Fix動作模組參數 用於動作命令下達]
        Static mFixList(enmStage.Max) As List(Of sCCDFixData)                               '[排列出定位順序(Array)]
        Static mNodeName(enmStage.Max) As String                                            '[節點]
        Static mAlignScene(enmStage.Max) As String                                          '[定位場景]
        Static mCCDFixListIndex(enmStage.Max) As sIndex                                     '[紀錄目前的Index(DispenseRelationshipList)]
        Static mSubDispStageNo1(enmMachineStation.MaxMachine) As Integer                    '[紀錄左側Stage對應的esys.SubDisp]
        Static mSubDispStageNo2(enmMachineStation.MaxMachine) As Integer                    '[紀錄右側Stage對應的esys.SubDisp]
        Static mCCDFixPathRegister(enmStage.Max) As List(Of sCCDFixPath)
        Static mCCDFixPathIndex(enmStage.Max) As sIndex                                     '[紀錄目前的Index(mCCDFixPathRegister)]
        Static mSubCCDFixPathRegister(enmStage.Max) As List(Of sCCDFixPath)                 '[紀錄要都給下一層做的時候的資料(mCCDFixPathRegister)]
        Static mCCDFixParam(enmStage.Max) As sCCDFixParam                                   '[記錄暫存資料]
        'Static mCCDOnFlyPathRegister As List(Of sPatternPath)
        Static mCCDRecivedErrorCount(enmStage.Max) As Integer                               '[資料傳輸錯誤之紀錄次數]
        Static mValveCylStopWatch(enmStage.Max) As Stopwatch                                '[用來記錄B閥汽缸的TimeOut]
        Static mTimeStopWatch(enmStage.Max) As Stopwatch
        Static mStartTime(enmStage.Max) As Decimal
        Static mPosB(enmStage.Max) As Decimal                                               '[座標(B Axis-->Tilt Axis)]
        Static mTriggerBoardPath(enmStage.Max) As List(Of sPatternPath)                     '[暫存塞給Trigger Board的資訊]

        Dim mTicket(enmStage.Max) As Integer
        Dim mAxisXState(enmStage.Max) As CommandStatus                                      '[X軸的狀態]
        Dim mAxisYState(enmStage.Max) As CommandStatus                                      '[Y軸的狀態]
        Dim mAxisZState(enmStage.Max) As CommandStatus                                      '[Z軸的狀態]
        Dim mAxisBState(enmStage.Max) As CommandStatus                                      '[Tilt軸的狀態]
        Dim mI(enmStage.Max) As Integer
        Dim mJ(enmStage.Max) As Integer
        Dim mAlignIndex(enmStage.Max) As Integer
        Dim mIndexX(enmStage.Max) As Integer
        Dim mIndexY(enmStage.Max) As Integer
        Dim mCommandPosB(enmStage.Max) As Decimal                                           '[取出目前Tilt角度(Command Pos)]
        Static mTicketRegister(enmStage.Max) As Integer                                         '[紀錄目前SK的Ticket]onFly

        '============================================
        Static mDispPathRegister(enmStage.Max) As List(Of sPatternPath)
        '=============================================

        If IsNothing(mSYS(sys.StageNo)) Then
            mSYS(sys.StageNo) = New sCCDFixSysParam
        End If

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart

                TestCount += 1
                CCDRound(sys.CCDNo) = TestCount 'GetCCDFixRound(sys.CCDNo)
                gSyslog.CCDSave("", CSystemLog.eCCDMessageProcess.Restart)
                If IsNothing(mTimeStopWatch(sys.StageNo)) = True Then
                    mTimeStopWatch(sys.StageNo) = New Stopwatch
                End If
                If IsNothing(mValveCylStopWatch(sys.StageNo)) = True Then
                    mValveCylStopWatch(sys.StageNo) = New Stopwatch
                End If
                If IsNothing(mCCDTimeStopWatch(sys.StageNo)) = True Then
                    mCCDTimeStopWatch(sys.StageNo) = New Stopwatch
                End If
                mTimeStopWatch(sys.StageNo).Restart()
                mCCDRecivedErrorCount(sys.StageNo) = 0
                Call gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False)
                gCMotion.SetCurve(sys.AxisX, eCurveMode.SCurve)
                gCMotion.SetCurve(sys.AxisY, eCurveMode.SCurve)
                '[Note]:清除上一Run的光源數值。
                mSYS(sys.StageNo).IsResetLightValue = True
                mSYS(sys.StageNo).CCDFixModel = gCRecipe.CCDFixModel

                If gCRecipe.CCDFixModel = eCCDFixModel.OnFly Then
                    gAOICollection.SetLiveTriggerMode(sys.CCDNo, eTriggerType.HardwareTrigger)
                    gTriggerBoard.GetVisionCounts(sys.StageNo, True) '[Note]先清空觸發板內部資料
                End If

                '[說明]:設定CCD運轉模式
                Call gAOICollection.SetCCDRunType(sys.CCDNo, enmCCDRunType.Fix)

                '[Note]:先塞群組之加減速  
                With mCCDFixParam(sys.StageNo)
                    .Acc = gCMotion.SyncParameter(sys.StageNo).Velocity.Acc * gCMotion.SyncParameter(sys.StageNo).Velocity.AccRatio
                    .Dec = gCMotion.SyncParameter(sys.StageNo).Velocity.Dec * gCMotion.SyncParameter(sys.StageNo).Velocity.DecRatio
                    .VelHigh = gCMotion.SyncParameter(sys.StageNo).Velocity.VelHigh
                    .LeadAngle.Degress = 180 '[Note]飛拍時用180度半徑1mm的弧角去串接
                    .LeadAngle.Distance = 1 '[Note]飛拍時用180度半徑1mm的弧角去串接
                End With

                '[說明]:速度載入
                If gCMotion.SetVelAccDec(sys.AxisX) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030017", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030017))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042017", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042017))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060017", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060017))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067017", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067017))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetVelAccDec(sys.AxisY) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031017", "DispStage_CCDScanFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031017))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043017", "DispStage_CCDScanFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043017))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061017", "DispStage_CCDScanFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061017))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068017", "DispStage_CCDScanFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068017))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetVelAccDec(sys.AxisZ) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032017", "DispStage_CCDScanFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032017))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044017", "DispStage_CCDScanFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044017))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062017", "DispStage_CCDScanFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062017))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069017", "DispStage_CCDScanFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069017))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetVelAccDec(sys.AxisB) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1034017", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034017))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1046017", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046017))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1064017", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064017))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1071017", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071017))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                '[Note]:不管怎樣，進入先將汽缸上升
                Call ValveCylinderAction(sys.StageNo, eValveWorkMode.Valve1, enmUpDown.Up, mValveCylStopWatch(sys.StageNo))
                gSyslog.CCDSave("CCDFixAction Start:" & TestCount, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 1100

            Case 1100
                '[Note]:檢查汽缸Sensor &汽缸動作是否逾時
                If ValveCylinderSensor(sys.StageNo, eValveWorkMode.Valve1, enmUpDown.Up) = True Then
                    gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 1200
                Else
                    If IsTimeOut(mValveCylStopWatch(sys.StageNo), gSSystemParameter.TimeOut2) = True Then
                        'TODO:Valve汽缸做動逾時
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2004000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2004000))
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2004000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2004000))
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2004000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2004000))
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2004000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2004000))
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                End If

            Case 1200
                '[說明]:只有KEYENCE需要藉由通訊方式做切換場景(分Fix、ScanGlue)
                If gAOICollection.IsCCDTimeOut(sys.CCDNo) = True Then
                    '[說明]:若資料傳輸異常 則再重新設定 異常次數超過3次就停下來
                    If mCCDRecivedErrorCount(sys.StageNo) > 3 Then
                        gEqpMsg.AddHistoryAlarm("Alarm_2000010", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2000010))
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        '[說明]:回應失敗
                        mCCDRecivedErrorCount(sys.StageNo) += 1
                        '[說明]:設定CCD運轉模式
                        Call gAOICollection.SetCCDRunType(sys.CCDNo, enmCCDRunType.Fix)
                    End If
                Else
                    '[說明]:資料接收完成(更改設定)
                    If gAOICollection.IsCCDReceiveBusy(sys.CCDNo) = False Then
                        If gAOICollection.IsCCDReceivedDataOK(sys.CCDNo) = True Then
                            gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 1300
                        End If
                    End If
                End If

            Case 1300
                '[Note]:確認Tilt有無存在，存在則流程會增加Z軸上升-->Tilt旋轉
                If sys.AxisB <> -1 Then
                    Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                    gCMotion.GetCommandValue(sys.AxisB, mCommandPosB(sys.StageNo))
                    If mCommandPosB(sys.StageNo) = mPosB(sys.StageNo) Then
                        '[Note]:若角度相同，則Tilt不再做旋轉
                        gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 2000
                    Else
                        gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 1400
                    End If
                Else
                    gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 2000
                End If

            Case 1400
                '[Note]:先將Z軸升至安全位置
                'ReviseVelocity(sys.AxisZ, gSSystemParameter.Pos.TiltSafePosZ, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.TiltSafePosZ) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 1500

            Case 1500
                '[Note]:等待Table Stop
                mAxisZState(sys.StageNo) = gCMotion.MotionDone(sys.AxisZ)
                If mAxisZState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                    Exit Sub
                End If
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 1600

            Case 1600
                '[Note]:根據Recipe給的Tilt角度決定轉至該位置
                ReviseVelocity(sys.AxisB, mPosB(sys.StageNo), gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisB, mPosB(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1034000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1046000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1064000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1071000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 1700

            Case 1700
                '[Note]:等待Table Stop
                mAxisBState(sys.StageNo) = gCMotion.MotionDone(sys.AxisB)
                If mAxisBState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisB) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1034004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1046004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1064004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1071004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                    Exit Sub
                End If
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 2000

            Case 2000
                '[Note]:Node順序排列
                Call CCDFixRecipeNodeSort(gCRecipe, sys.StageNo, sys.ConveyorNo)

                '[Note]:會有重複執行的問題，但沒關係
                Select Case sys.MachineNo
                    Case enmMachineStation.MachineA
                        '[Note]:先做配接-->根據機種，決定需要哪幾組Stage復歸
                        Select Case gSSystemParameter.MachineType
                            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                                mSubDispStageNo1(sys.MachineNo) = eSys.SubDisp1
                                mSubDispStageNo2(sys.MachineNo) = eSys.SubDisp2

                            Case Else
                                mSubDispStageNo1(sys.MachineNo) = eSys.SubDisp1
                                mSubDispStageNo2(sys.MachineNo) = eSys.SubDisp1

                        End Select

                    Case enmMachineStation.MachineB
                        mSubDispStageNo1(sys.MachineNo) = eSys.SubDisp3
                        mSubDispStageNo2(sys.MachineNo) = eSys.SubDisp4

                End Select

                gNodeLevel(sys.StageNo) = 1
                mCCDFixListIndex(sys.StageNo).Start = -1
                mCCDFixListIndex(sys.StageNo).Ending = -1
                mCCDFixListIndex(sys.StageNo).Done = -1
                mSYS(sys.StageNo).LastTicket = -1

                '[Note]:先清除CCD定位紀錄，在取完定位資訊後也會做清除的動作(只有這二個地方會做清除的動作)
                If Not IsNothing(gCCDAlignResultDict(sys.CCDNo)) = True Then
                    gCCDAlignResultDict(sys.CCDNo).Clear()
                End If
                gAOICollection.ClearTicket(sys.CCDNo)
                mTicketRegister(sys.CCDNo) = 0


                If IsNeedCCDFixProcess(gCRecipe, sys.StageNo) = True Then
                    gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 2100
                Else
                    If sys.StageNo = enmStage.No2 Or sys.StageNo = enmStage.No4 Then
                        gIsLSideWorking(sys.MachineNo) = True
                    End If
                    '[Note]:若有一側不用作業，則直接給NodeLevel至Max(應該不可能大於9999層)
                    gNodeLevel(sys.StageNo) = 9999
                    gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 8800
                End If

            Case 2100
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                '[Note]:確認R側已經開始作業後，L側才可以開始作業
                Select Case sys.StageNo
                    Case enmStage.No1, enmStage.No3
                        If gIsLSideWorking(sys.MachineNo) = True Then
                            gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 2200
                        End If

                    Case Else
                        gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 2200

                End Select

            Case 2200
                '[Note]:判斷是否已跑完所有Node(不生產點與定位點)
                If mCCDFixListIndex(sys.StageNo).Done = CCDFixRelationshipList(sys.StageNo).Count - 1 Then
                    '[Note]:完成所有Node
                    gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 8800
                Else

                    '[Note]:取下一個CCDFixRelationshipList(同一層下一個Node)
                    mCCDFixListIndex(sys.StageNo).Start = mCCDFixListIndex(sys.StageNo).Ending + 1
                    mCCDFixListIndex(sys.StageNo).Ending = mCCDFixListIndex(sys.StageNo).Ending + 1
                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
                    mFixList(sys.StageNo) = New List(Of sCCDFixData)
                    mFixList(sys.StageNo).Capacity = 1
                    gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 2300
                End If

            Case 2300
                '[Note]:先判斷該層有幾個Node
                '[Note]:先判斷該層是否需要定位
                gNodeLevel(sys.StageNo) = CCDFixRelationshipList(sys.StageNo)(mCCDFixListIndex(sys.StageNo).Start).NodeLevel
                Dim mAlignType As eCCDAlignType = Nothing
                Dim mNowAlignType As eCCDAlignType = Nothing
                Dim mAlignNodeName As String
                Dim mAlignEndIndex As Integer
                Dim AlginList As List(Of SNodeAlignScene)
                AlginList = New List(Of SNodeAlignScene)
                For mI(sys.StageNo) = mCCDFixListIndex(sys.StageNo).Start To CCDFixRelationshipList(sys.StageNo).Count - 1
                    If gNodeLevel(sys.StageNo) = CCDFixRelationshipList(sys.StageNo)(mI(sys.StageNo)).NodeLevel Then

                        mNowAlignType = CCDFixRelationshipList(sys.StageNo)(mI(sys.StageNo)).AlignType

                        Select Case mNowAlignType
                            Case eCCDAlignType.eSkipMark
                                Call CCDSkipMarkSortList(gCRecipe, sys.StageNo, CCDFixRelationshipList(sys.StageNo)(mI(sys.StageNo)), mFixList(sys.StageNo))
                                '[Note]定位方式有改變
                                mCCDFixListIndex(sys.StageNo).Ending = mI(sys.StageNo)
                                Exit For

                            Case eCCDAlignType.eAlign
                                '[Note]:抓出有個幾Align點
                                mAlignNodeName = CCDFixRelationshipList(sys.StageNo)(mI(sys.StageNo)).NodeName
                                mAlignEndIndex = mI(sys.StageNo)

                                AlginList.Clear()
                                AlginList.Add(CCDFixRelationshipList(sys.StageNo)(mI(sys.StageNo)))
                                Select Case gCRecipe.CCDFixModel
                                    Case eCCDFixModel.NonOnFly
                                        '[Note]第一點→第二點→第一點→第二點
                                        For mJ(sys.StageNo) = mI(sys.StageNo) + 1 To CCDFixRelationshipList(sys.StageNo).Count - 1
                                            If mAlignNodeName = CCDFixRelationshipList(sys.StageNo)(mJ(sys.StageNo)).NodeName Then
                                                AlginList.Add(CCDFixRelationshipList(sys.StageNo)(mJ(sys.StageNo)))
                                                mAlignEndIndex = mJ(sys.StageNo)
                                            Else
                                                Exit For
                                            End If
                                        Next
                                    Case eCCDFixModel.OnFly
                                        '[Note]第一點全掃完才掃第二點

                                End Select
                                Call CCDFixSortList(gCRecipe, sys.StageNo, AlginList, mFixList(sys.StageNo))
                                '[Note]定位方式有改變
                                mCCDFixListIndex(sys.StageNo).Ending = mAlignEndIndex
                                Exit For


                        End Select
                        'If mAlignType = Nothing Or mAlignType = mNowAlignType Then
                        '    Debug.Print("第幾層" & gNodeLevel(sys.StageNo) & "定位方式" & mNowAlignType)
                        '    Select Case mNowAlignType
                        '        Case eAlignIndex.SkipMark
                        '            Call CCDSkipMarkSortList(gCRecipe, sys.StageNo, CCDFixRelationshipList(sys.StageNo)(mI(sys.StageNo)), mFixList(sys.StageNo))
                        '        Case Else
                        '            Call CCDFixSortList(gCRecipe, sys.StageNo, CCDFixRelationshipList(sys.StageNo)(mI(sys.StageNo)), mFixList(sys.StageNo))
                        '    End Select

                        'Else
                        '    '[Note]定位方式有改變
                        '    mCCDFixListIndex(sys.StageNo).Ending = mI(sys.StageNo) - 1
                        '    Exit For
                        'End If

                    Else
                        '[Note]:進入不同層或者 定位方式有改變
                        mCCDFixListIndex(sys.StageNo).Ending = mI(sys.StageNo) - 1
                        Exit For
                    End If
                    If mI(sys.StageNo) = CCDFixRelationshipList(sys.StageNo).Count - 1 Then
                        '[Note]:最後一個了
                        mCCDFixListIndex(sys.StageNo).Ending = mI(sys.StageNo)
                    End If
                    '[紀錄上一層使用的定位點方式]
                    mAlignType = CCDFixRelationshipList(sys.StageNo)(mI(sys.StageNo)).AlignType
                Next
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 2400

            Case 2400
                ''[Note]:判斷該層是否有Node是需要CCD Fix
                'If mFixList(sys.StageNo).Count > 0 Then
                '    If CreateCCDFixPosList(gCRecipe, sys.StageNo, mFixList(sys.StageNo), mCCDFixPathRegister(sys.StageNo)) = True Then
                '        '[Note]:
                '        mCCDFixPathIndex(sys.StageNo).Start = -1
                '        mCCDFixPathIndex(sys.StageNo).Ending = -1
                '        mCCDFixPathIndex(sys.StageNo).Done = -1
                '        sys.SysNum = 2500
                '    Else
                '        'TODO:異常訊息(CreateCCDFixPosList Fail)
                '        gEqpMsg.AddHistoryAlarm("Error_1019004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1019004), eMessageLevel.Error)
                '        sys.RunStatus = enmRunStatus.Alarm
                '        Exit Sub
                '    End If
                'Else
                '    '[Note]:該層不需要定位，直接跳到Update Real Basic Pos
                '    sys.SysNum = 8000
                'End If

                If CreateCCDFixPosList(gCRecipe, sys.StageNo, mFixList(sys.StageNo), mCCDFixPathRegister(sys.StageNo)) = True Then
                    '[Note]:
                    mCCDFixPathIndex(sys.StageNo).Start = -1
                    mCCDFixPathIndex(sys.StageNo).Ending = -1
                    mCCDFixPathIndex(sys.StageNo).Done = -1
                    gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 2500
                Else
                    'TODO:異常訊息(CreateCCDFixPosList Fail)
                    gEqpMsg.AddHistoryAlarm("Error_1000012", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000012), eMessageLevel.Error)
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If


            Case 2500
                '[Note]:判斷(該層)不生產點或定位點都完成了
                If mCCDFixPathIndex(sys.StageNo).Done = mCCDFixPathRegister(sys.StageNo).Count - 1 Then
                    '[Note]:完成所有定位點-->進入Update RealBacis，但進入之前須先進入防撞機制
                    gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 3000
                Else
                    mCCDFixPathIndex(sys.StageNo).Start = mCCDFixPathIndex(sys.StageNo).Ending + 1
                    mCCDFixPathIndex(sys.StageNo).Ending = mCCDFixPathIndex(sys.StageNo).Ending + 1
                    gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 2600
                End If

            Case 2600
                '[Note]:先決定從哪一顆做到哪一顆
                Select Case mSYS(sys.StageNo).CCDFixModel
                    Case eCCDFixModel.NonOnFly
                        If EstimateCCDFixPath(sys.StageNo, mCCDFixPathRegister(sys.StageNo), mCCDFixPathIndex(sys.StageNo).Start, mCCDFixPathIndex(sys.StageNo).Ending, mSubCCDFixPathRegister(sys.StageNo), mCCDFixProtect(sys.StageNo).TargetPos) = True Then
                            gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 3000
                        Else
                            'TODO:異常訊息(EstimateCCDFixMultiDie Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1000012", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000012), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                        End If
                    Case eCCDFixModel.OnFly
                        Dim mStartIndex(sys.StageNo) As Integer
                        'Dim mEndIndex(sys.StageNo) As Integer
                        Dim isFirstData As Boolean = True
                        '[Note]S型
                        For mStartIndex(sys.StageNo) = mCCDFixPathIndex(sys.StageNo).Start To mCCDFixPathRegister(sys.StageNo).Count - 1
                            If EstimateCCDFixMultiDieOnFly(sys.StageNo, mCCDFixPathRegister(sys.StageNo), mStartIndex(sys.StageNo), mCCDFixPathIndex(sys.StageNo).Ending, mSubCCDFixPathRegister(sys.StageNo), mCCDFixProtect(sys.StageNo).TargetPos, isFirstData) = True Then
                                CreateDispList(sys.StageNo, mCCDFixParam(sys.StageNo), mSubCCDFixPathRegister(sys.StageNo), mStartIndex(sys.StageNo), mDispPathRegister(sys.StageNo), mTriggerBoardPath(sys.StageNo), mCCDFixProtect(sys.StageNo).TargetPos, isFirstData, False)
                                mStartIndex(sys.StageNo) = mCCDFixPathIndex(sys.StageNo).Ending 'mEndIndex(sys.StageNo)
                                isFirstData = False
                            Else
                                'TODO:異常訊息(EstimateCCDFixMultiDie Fail)
                                gEqpMsg.AddHistoryAlarm("Error_1000012", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000012), eMessageLevel.Error)
                                sys.RunStatus = enmRunStatus.Alarm
                            End If
                        Next
                        gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 3000
                    Case Else
                        'TODO:異常訊息(EstimateCCDFixMultiDie Fail)
                        gEqpMsg.AddHistoryAlarm("Error_1000012", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000012), eMessageLevel.Error)
                        sys.RunStatus = enmRunStatus.Alarm
                End Select


                ''[Note]:先決定從哪一顆做到哪一顆
                'If EstimateCCDFixMultiDie(sys.StageNo, gCRecipe.CCDFixModel, mCCDFixPathRegister(sys.StageNo), mCCDFixPathIndex(sys.StageNo).Start, mCCDFixPathIndex(sys.StageNo).Ending, mSubCCDFixPathRegister(sys.StageNo), mCCDFixProtect(sys.StageNo).TargetPos) = True Then
                '    Select Case mSYS(sys.StageNo).CCDFixModel
                '        Case eCCDFixModel.NonOnFly
                '            '[Note]不須將路徑資料轉換
                '        Case eCCDFixModel.OnFly
                '            '[Note]路徑資料轉換為觸發版資料
                '            CreateDispList(sys.StageNo, mCCDFixParam(sys.StageNo), mSubCCDFixPathRegister(sys.StageNo), mDispPathRegister(sys.StageNo), mTriggerBoardPath(sys.StageNo), mCCDFixProtect(sys.StageNo).TargetPos, True, False)
                '    End Select
                '    sys.SysNum = 3000
                'Else
                '    'TODO:異常訊息(EstimateCCDFixMultiDie Fail)
                '    gEqpMsg.AddHistoryAlarm("Error_1000012", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000012), eMessageLevel.Error)
                '    'gEqpMsg.AddHistoryAlarm("Error_1019004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1019004), eMessageLevel.Error)
                '    sys.RunStatus = enmRunStatus.Alarm
                'End If

                '*************************************************************************************************
                '******************************************防撞機制(退後)*****************************************
                '*************************************************************************************************
            Case 3000
                '[Note]:安全位置檢察 XY移動前位置確認(只有800AQ需要做此項確認)
                '[Note]:更新各軸的目標位置
                '[Note]:L側動完後，R側就可以動了
                gIsLSideWorking(sys.MachineNo) = True
                gProtectData(sys.StageNo).TargetPos = mCCDFixProtect(sys.StageNo).TargetPos
                If EstimateIsSafePos(sys.StageNo, gNodeLevel) = True Then
                    '[Note]:判斷所有定位點都完成了(該層)
                    If mCCDFixPathIndex(sys.StageNo).Done = mCCDFixPathRegister(sys.StageNo).Count - 1 Then
                        '[Note]:完成所有定位點-->進入Update RealBacis，但進入之前須先進入防撞機制
                        gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 8000
                    Else
                        gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 4000
                    End If
                Else
                    gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 3100
                End If

            Case 3100
                '[Note]:檢查有沒有人應該退至安全區
                Select Case sys.StageNo
                    Case enmStage.No1, enmStage.No3
                        '[說明]:當單Stage動作時,另一頭Node為0時(不動作Stage)!!!是不會啟動移動至安全位置,移動Stage只能無限等待(另一頭回安全位置)
                        '目前確認另一頭Node為0時(不動作Stage)會移至原點位置,如果此位置還會觸發安全位置檢察失敗,表示設定位置已超出極限,需修改Recipe or 放寬安全位置(不會撞機為原則)<=不建議
                        Select Case sys.StageNo
                            Case enmStage.No1
                                If gSYS(eSys.DispStage2).RunStatus = enmRunStatus.Finish And gIsRSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1042005", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042005), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                            Case enmStage.No3
                                If gSYS(eSys.DispStage4).RunStatus = enmRunStatus.Finish And gIsRSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1042005", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042005), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                        End Select

                        '[Note]:檢查R側是否有叫L側退開
                        If gIsLSideNeedGoToSafePos(sys.MachineNo) = True Then
                            '[Note]:根據另一側的資訊決定要退到哪去
                            Call EstimateGoBackPos(sys.StageNo, gProtectData, gProtectData(sys.StageNo).TargetPos)
                            gIsLSideNeedGoToSafePos(sys.MachineNo) = False
                            gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 3200
                        Else
                            gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 3000
                        End If

                    Case enmStage.No2, enmStage.No4
                        '[說明]:當單Stage動作時,另一頭Node為0時(不動作Stage)!!!是不會啟動移動至安全位置,移動Stage只能無限等待(另一頭回安全位置)
                        '目前確認另一頭Node為0時(不動作Stage)會移至原點位置,如果此位置還會觸發安全位置檢察失敗,表示設定位置已超出極限,需修改Recipe or 放寬安全位置(不會撞機為原則)<=不建議
                        Select Case sys.StageNo
                            Case enmStage.No2
                                If gSYS(eSys.DispStage1).RunStatus = enmRunStatus.Finish And gIsLSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1030006", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030006), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                            Case enmStage.No4
                                If gSYS(eSys.DispStage3).RunStatus = enmRunStatus.Finish And gIsLSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1030006", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030006), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                        End Select

                        '[Note]:檢查L側是否有叫R側退開
                        '       若叫R側退, 則優先(因為NodeLevel比對方低)
                        If gIsRSideNeedGoToSafePos(sys.MachineNo) = True Then
                            '[Note]:根據另一側的資訊決定要退到哪去
                            Call EstimateGoBackPos(sys.StageNo, gProtectData, gProtectData(sys.StageNo).TargetPos)
                            gIsRSideNeedGoToSafePos(sys.MachineNo) = False
                            gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 3200
                        Else
                            gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 3000
                        End If

                End Select

            Case 3200
                '[Note]:退至安全區 
                ReviseVelocity(sys.AxisY, gProtectData(sys.StageNo).TargetPos.PosY, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisY, gProtectData(sys.StageNo).TargetPos.PosY) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                ReviseVelocity(sys.AxisX, gProtectData(sys.StageNo).TargetPos.PosX, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisX, gProtectData(sys.StageNo).TargetPos.PosX) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 3300

            Case 3300
                '[Note]:等待Table Stop
                mAxisXState(sys.StageNo) = gCMotion.MotionDone(sys.AxisX)
                mAxisYState(sys.StageNo) = gCMotion.MotionDone(sys.AxisY)
                If mAxisXState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1030004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1042004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1060004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1067004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                If mAxisYState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisY) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1031004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1043004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1061004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1068004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 3000


                '*******************************************************************************
                '****************派工給NonOnFlyCCDFix或OnFlyCCDFix點膠*******************
                '*******************************************************************************
            Case 4000
                '[Note]:下命令給點膠子流程
                '[Note]:系統狀態改為運行中，且由起始索引開始工作
                '[Note]:塞資料，把資料打包往下一層送-->決定走哪個流程

                With mSYS(sys.StageNo)
                    .AxisX = sys.AxisX
                    .AxisY = sys.AxisY
                    .AxisZ = sys.AxisZ
                    .AxisA = sys.AxisA
                    .AxisB = sys.AxisB
                    .AxisC = sys.AxisC
                    .CCDNo = sys.CCDNo
                    .Tag = sys.Tag
                    .Timer = sys.Timer
                    .StageNo = sys.StageNo
                    .CCDFixPathRegister = mSubCCDFixPathRegister(sys.StageNo)
                    .CCDOnFlyPathRegister = mDispPathRegister(sys.StageNo)
                    .CCDFixParam = mCCDFixParam(sys.StageNo)
                    .CCDFixModel = gCRecipe.CCDFixModel
                    .TriggerBoardPathRegister = mTriggerBoardPath(sys.StageNo)
                    .SysNum = sCCDFixSysParam.SysLoopStart
                    .RunStatus = enmRunStatus.Running
                    .IsCanPause = True
                    .StartIndex = mTicketRegister(sys.CCDNo)
                    '.EndIndex = mTicketRegister(sys.CCDNo) + mCCDFixPathRegister(sys.StageNo).Count - 1 ' mSubCCDFixPathRegister(sys.StageNo).Count - 1
                End With
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 4100

            Case 4100
                '[Note]:跑CCDFix子流程(更新流程)
                '       若外部給暫停的命令且流程中是允許暫停，則才可以暫停
                If (sys.ExternalPause = True And mSYS(sys.StageNo).IsCanPause = True) Then
                    '[Note]切換回軟體觸發
                    gAOICollection.SetLiveTriggerMode(sys.CCDNo, eTriggerType.SoftwareTrigger)
                    Exit Sub
                End If

                If mSYS(sys.StageNo).RunStatus = enmRunStatus.Running Then
                    Select Case mSYS(sys.StageNo).CCDFixModel
                        Case eCCDFixModel.NonOnFly
                            CCDFixNonOnFlyModelAction(mSYS(sys.StageNo))

                        Case eCCDFixModel.OnFly
                            CCDFixOnFlyModelAction(mSYS(sys.StageNo))
                    End Select
                End If

                '[Note]:判斷子流程是否完成
                If mSYS(sys.StageNo).RunStatus = enmRunStatus.Finish Then
                    '[Note]:已經執行過一次，所以不用再紀錄直清掉了(清掉的目的是清除上一run的數值)。
                    mSYS(sys.StageNo).IsResetLightValue = False
                    gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 5000
                End If

                '***********************************************************************************************
                '***********************************************************************************************
                '***********************************************************************************************
            Case 5000
                '[Note]:檢查有沒有人應該退至安全區
                Select Case sys.StageNo
                    Case enmStage.No1, enmStage.No3
                        '[說明]:當單Stage動作時,另一頭Node為0時(不動作Stage)!!!是不會啟動移動至安全位置,移動Stage只能無限等待(另一頭回安全位置)
                        '目前確認另一頭Node為0時(不動作Stage)會移至原點位置,如果此位置還會觸發安全位置檢察失敗,表示設定位置已超出極限,需修改Recipe or 放寬安全位置(不會撞機為原則)<=不建議
                        Select Case sys.StageNo
                            Case enmStage.No1
                                If gSYS(eSys.DispStage2).RunStatus = enmRunStatus.Finish And gIsRSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1042005", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042005), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                            Case enmStage.No3
                                If gSYS(eSys.DispStage4).RunStatus = enmRunStatus.Finish And gIsRSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1042005", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042005), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                        End Select

                        '[Note]:檢查R側是否有叫L側退開
                        If gIsLSideNeedGoToSafePos(sys.MachineNo) = True Then
                            '[Note]:根據另一側的資訊決定要退到哪去
                            Call EstimateGoBackPos(sys.StageNo, gProtectData, gProtectData(sys.StageNo).TargetPos)
                            gIsLSideNeedGoToSafePos(sys.MachineNo) = False
                            gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 5100
                        Else
                            gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 6000
                        End If

                    Case enmStage.No2, enmStage.No4
                        '[說明]:當單Stage動作時,另一頭Node為0時(不動作Stage)!!!是不會啟動移動至安全位置,移動Stage只能無限等待(另一頭回安全位置)
                        '目前確認另一頭Node為0時(不動作Stage)會移至原點位置,如果此位置還會觸發安全位置檢察失敗,表示設定位置已超出極限,需修改Recipe or 放寬安全位置(不會撞機為原則)<=不建議
                        Select Case sys.StageNo
                            Case enmStage.No2
                                If gSYS(eSys.DispStage1).RunStatus = enmRunStatus.Finish And gIsLSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1030006", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030006), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                            Case enmStage.No4
                                If gSYS(eSys.DispStage3).RunStatus = enmRunStatus.Finish And gIsLSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1030006", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030006), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                        End Select

                        '[Note]:檢查L側是否有叫R側退開
                        '       若叫R側退, 則優先(因為NodeLevel比對方低)
                        If gIsRSideNeedGoToSafePos(sys.MachineNo) = True Then
                            '[Note]:根據另一側的資訊決定要退到哪去
                            Call EstimateGoBackPos(sys.StageNo, gProtectData, gProtectData(sys.StageNo).TargetPos)
                            gIsRSideNeedGoToSafePos(sys.MachineNo) = False
                            gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 5100
                        Else
                            gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 6000
                        End If

                End Select

            Case 5100
                '[Note]:退至安全區 
                ReviseVelocity(sys.AxisY, gProtectData(sys.StageNo).TargetPos.PosY, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisY, gProtectData(sys.StageNo).TargetPos.PosY) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                ReviseVelocity(sys.AxisX, gProtectData(sys.StageNo).TargetPos.PosX, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisX, gProtectData(sys.StageNo).TargetPos.PosX) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 5200

            Case 5200
                '[Note]:等待Table Stop
                mAxisXState(sys.StageNo) = gCMotion.MotionDone(sys.AxisX)
                mAxisYState(sys.StageNo) = gCMotion.MotionDone(sys.AxisY)

                If mAxisXState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1030004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1042004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1060004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1067004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                If mAxisYState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisY) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1031004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1043004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1061004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1068004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 6000

            Case 6000
                '[Note]:完成
                mCCDFixPathIndex(sys.StageNo).Done = mCCDFixPathIndex(sys.StageNo).Ending
                mTicketRegister(sys.CCDNo) = mTicketRegister(sys.CCDNo) + mCCDFixPathRegister(sys.StageNo).Count 'mSubCCDFixPathRegister(sys.StageNo).Count
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 2500

            Case 8000
                If mCCDFixPathRegister(sys.StageNo).Count = 0 Then
                    '[Note]:這一層沒有定位資料
                    gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 8100
                Else
                    '[Note]:等待CCD運算完成(利用最後一張取像點確認是否運算完成)
                    If gCCDAlignResultDict(sys.CCDNo).ContainsKey(mSYS(sys.StageNo).LastTicket) = True Then
                        If gCCDAlignResultDict(sys.CCDNo)(mSYS(sys.StageNo).LastTicket).IsRunSuccess = True Then
                            gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 8100
                        End If
                    End If
                End If


            Case 8100
                '[Note]:接收資料結果
                Select Case UpdateCCDFixReselt(sys.CCDNo, sys.StageNo, mCCDFixPathRegister(sys.StageNo), sys.ConveyorNo)
                    Case eCCDAlginStatus.eOK
                        gSyslog.CCDSave("CCD拍照至取像完成時間:" & mCCDTimeStopWatch(sys.CCDNo).ElapsedMilliseconds, CSystemLog.eCCDMessageProcess.Add)
                        '[Note]存定位資料檔
                        SaveCCDResult(sys.CCDNo, sys.StageNo, mCCDFixPathRegister(sys.StageNo))
                        gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 8500
                    Case eCCDAlginStatus.eBusy

                    Case eCCDAlginStatus.eNGStop
                        'TODO:異常訊息(Align NG Stop)

                        'Toby  modify 修改異常訊息顯示_20161005_Start
                        If gCCDAlignResultDict(sys.CCDNo)(mTicket(sys.StageNo)).Result.Count > 1 Then  'CCD 定位結果 數量多餘1
                            Select Case sys.StageNo '判斷stage
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Alarm_2012102", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2012102), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Alarm_2012402", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2012402), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Alarm_2012702", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2012702), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Alarm_2013002", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2013002), eMessageLevel.Alarm)
                            End Select
                        Else 'CCD 定位結果 數量=0
                            Select Case sys.StageNo '判斷stage
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Alarm_2012103", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2012103), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Alarm_2012403", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2012403), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Alarm_2012703", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2012703), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Alarm_2013003", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2013003), eMessageLevel.Alarm)
                            End Select
                        End If
                        'Toby  modify 修改異常訊息顯示_20161005_End

                        'gEqpMsg.AddHistoryAlarm("Error_1031004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub

                End Select


            Case 8500

                '[Note]:(更新本層的所有Node) Update Real Bacis與下一層的Real Basic、ScanPos(Align Pos) 全部更新完成後，在進入下一層繼續做
                '       同一個Node只需更新一次即可
                mNodeName(sys.StageNo) = ""
                For mI(sys.StageNo) = mCCDFixListIndex(sys.StageNo).Start To mCCDFixListIndex(sys.StageNo).Ending
                    If mNodeName(sys.StageNo) <> CCDFixRelationshipList(sys.StageNo)(mI(sys.StageNo)).NodeName Then
                        mNodeName(sys.StageNo) = CCDFixRelationshipList(sys.StageNo)(mI(sys.StageNo)).NodeName
                        Select Case CCDFixRelationshipList(sys.StageNo)(mI(sys.StageNo)).AlignIndex
                            Case eAlignIndex.SkipMark
                                Call UpdatePosBySkipMark(sys.MachineNo, sys.StageNo, gCRecipe, mNodeName(sys.StageNo), sys.ConveyorNo)
                            Case Else
                                Select Case mSYS(sys.StageNo).CCDFixModel
                                    Case eCCDFixModel.NonOnFly
                                        Call UpdatePosByAlign(sys.MachineNo, sys.StageNo, gCRecipe, mNodeName(sys.StageNo), sys.ConveyorNo)
                                    Case eCCDFixModel.OnFly
                                        Select Case gCRecipe.Node(sys.StageNo)(mNodeName(sys.StageNo)).AlignType
                                            Case enmAlignType.DevicePos1
                                                Call UpdatePosByAlign(sys.MachineNo, sys.StageNo, gCRecipe, mNodeName(sys.StageNo), sys.ConveyorNo)
                                            Case enmAlignType.DevicePos2
                                                If CCDFixRelationshipList(sys.StageNo)(mI(sys.StageNo)).AlignIndex = eAlignIndex.No2 Then
                                                    '[Note]兩點定位時，第二點才更新狀況
                                                    Call UpdatePosByAlign(sys.MachineNo, sys.StageNo, gCRecipe, mNodeName(sys.StageNo), sys.ConveyorNo)
                                                Else
                                                    '[Note] 不更新狀態
                                                End If
                                        End Select
                                End Select
                        End Select


                    End If
                Next
                mCCDFixListIndex(sys.StageNo).Done = mCCDFixListIndex(sys.StageNo).Ending

                '[Note]:不能清除，因為會影響到frmMain的圖像更新
                ''[Note]:先清除CCD定位紀錄，在取完定位資訊後也會做清除的動作(只有這二個地方會做清除的動作)
                'gCCDAlignResultDict(sys.CCDNo).Clear()
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 2200

            Case 8800
                '[Note]定位流程完成，關閉光源
                Call gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), enmONOFF.eOff)
                Call gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), enmONOFF.eOff)
                Call gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), enmONOFF.eOff)
                Call gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), enmONOFF.eOff)
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 9000
            Case 9000
                '[Note]:點膠完成後，退至安全區等待(針對有作業的情況下)
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        '[Note]:直接退到安全位置
                        gProtectData(sys.StageNo).TargetPos.PosX = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosX(sys.SelectValve)
                        gProtectData(sys.StageNo).TargetPos.PosY = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosY(sys.SelectValve)
                        gProtectData(sys.StageNo).TargetPos.PosZ = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosZ(sys.SelectValve)

                        mCCDFixProtect(sys.StageNo).TargetPos.PosX = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosX(sys.SelectValve)
                        mCCDFixProtect(sys.StageNo).TargetPos.PosY = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosY(sys.SelectValve)

                    Case Else
                        gProtectData(sys.StageNo).TargetPos.PosZ = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosZ(sys.SelectValve)
                End Select
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 9100

            Case 9100
                '[Note]Z軸上升
                'ReviseVelocity(sys.AxisZ, gSSystemParameter.Pos.ZUpPos, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If


                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        '[Note]:直接退到安全位置
                        Select Case sys.StageNo
                            Case enmStage.No1
                                If gCRecipe.Node(enmStage.No2).Count = 0 Then
                                    sys.SysNum = 9300
                                Else
                                    sys.SysNum = 9200
                                End If
                            Case enmStage.No2
                                If gCRecipe.Node(enmStage.No1).Count = 0 Then
                                    sys.SysNum = 9300
                                Else
                                    sys.SysNum = 9200
                                End If
                            Case enmStage.No3
                                If gCRecipe.Node(enmStage.No4).Count = 0 Then
                                    sys.SysNum = 9300
                                Else
                                    sys.SysNum = 9200
                                End If
                            Case enmStage.No4
                                If gCRecipe.Node(enmStage.No3).Count = 0 Then
                                    sys.SysNum = 9300
                                Else
                                    sys.SysNum = 9200
                                End If
                        End Select
                        
                    Case Else
                        sys.SysNum = 9300
                End Select

            Case 9200
                ReviseVelocity(sys.AxisY, gProtectData(sys.StageNo).TargetPos.PosY, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisY, gProtectData(sys.StageNo).TargetPos.PosY) <> CommandStatus.Sucessed Then
                    '[Note]:Y軸移動異常
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                ReviseVelocity(sys.AxisX, gProtectData(sys.StageNo).TargetPos.PosX, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisX, gProtectData(sys.StageNo).TargetPos.PosX) <> CommandStatus.Sucessed Then
                    '[Note]:X軸移動異常
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                'ReviseVelocity(sys.AxisZ, gSSystemParameter.Pos.ZUpPos, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                'If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                '    Select Case sys.StageNo
                '        Case 0
                '            gEqpMsg.AddHistoryAlarm("Error_1032000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                '        Case 1
                '            gEqpMsg.AddHistoryAlarm("Error_1044000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                '        Case 2
                '            gEqpMsg.AddHistoryAlarm("Error_1062000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                '        Case 3
                '            gEqpMsg.AddHistoryAlarm("Error_1069000", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                '    End Select
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 9300

            Case 9300
                '[Note]:Check Motion Done
                mAxisXState(sys.StageNo) = gCMotion.MotionDone(sys.AxisX)
                mAxisYState(sys.StageNo) = gCMotion.MotionDone(sys.AxisY)
                mAxisZState(sys.StageNo) = gCMotion.MotionDone(sys.AxisZ)

                If mAxisXState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1030004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1042004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1060004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1067004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                If mAxisYState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisY) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1031004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1043004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1061004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1068004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                If mAxisZState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069004", "DispStage_CCDFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 9400

            Case 9400
                gAOICollection.SetLiveTriggerMode(sys.CCDNo, eTriggerType.SoftwareTrigger)
                sys.RunStatus = enmRunStatus.Finish
                gSyslog.CCDSave("CCDFixAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                gSyslog.CCDSave("", CSystemLog.eCCDMessageProcess.Save)
                Exit Sub

        End Select

    End Sub

    Public Function GetCCDFixRound(ByVal cCDNo As Integer) As Integer
        Dim mStartTime As String = Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & " " &
        Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00")
        Dim mEndTime As String = " "
        Dim mDuringTime As String = " "
        Dim folderName As String

        Dim mfileName As String

        folderName = "D:\PIIData\DataLog\" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & "\"

        If Not System.IO.Directory.Exists(folderName) Then
            System.IO.Directory.CreateDirectory(folderName)
        End If

        mfileName = "CCDFixReselt" & Format(Now.Year, "0000") & Format(Now.Month, "00") & Format(Now.Day, "00") & "_" & (CCDNo + 1) & "_" '& CCDRound & ".csv"

        Dim mRound As Integer = 0
        For Each file In System.IO.Directory.GetFiles(folderName)
            Dim fInfo As New System.IO.FileInfo(file)
            Dim mName As String = fInfo.Name.Replace(".csv", "") '取得不含後綴名的短檔名 
            If mName.StartsWith("CCDFixReselt") Then
                If CInt(mName.Substring(mfileName.Length, mName.Length - mfileName.Length)) > mRound Then '[Note]取最大值
                    mRound = CInt(mName.Substring(mfileName.Length, mName.Length - mfileName.Length))
                End If
            End If
        Next
        'Debug.Print(" mRound:  " & mRound)
        mRound = mRound + 1
        Return mRound
    End Function


    Private Function SaveCCDResult(ByVal cCDNo As Integer, ByVal stageNo As enmStage, ByVal cCDFixPathRegister As List(Of sCCDFixPath)) As Boolean
        Dim mI(enmStage.Max) As Integer
        Dim mTicket(enmStage.Max) As Integer


        For mI(stageNo) = 0 To cCDFixPathRegister.Count - 1
            Dim CCDResult As sAlignResult

            With cCDFixPathRegister(mI(stageNo))
                Select Case .AlignIndex
                    Case eAlignIndex.No1
                        mTicket(stageNo) = gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).Ticket

                        If gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result.Count = 1 Then
                            CCDResult = gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0)
                        End If

                        SaveCCDFixReselt((.AlignIndex + 1), cCDNo, CCDRound(cCDNo), .IndexX, .IndexY, gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result.Count, CCDResult)

                    Case eAlignIndex.No2
                        mTicket(stageNo) = gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).Ticket2
                        If gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result.Count = 1 Then
                            CCDResult = gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result(0)
                        End If

                        SaveCCDFixReselt((.AlignIndex + 1), cCDNo, CCDRound(cCDNo), .IndexX, .IndexY, gCCDAlignResultDict(cCDNo)(mTicket(stageNo)).Result.Count, CCDResult)

                    Case eAlignIndex.No3
                        mTicket(stageNo) = gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).Ticket3

                    Case eAlignIndex.SkipMark
                        mTicket(stageNo) = gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).SkipMarkTicket
                End Select


            End With
        Next

        Return True
    End Function


    ''' <summary>[非On Fly Model定位流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Private Sub CCDFixNonOnFlyModelAction(ByRef sys As sCCDFixSysParam)
        Static mCCDStableTimeStopWatch(enmStage.Max) As Stopwatch
        Static mPosX(enmStage.Max) As Decimal                                   '[目標座標:X]
        Static mPosY(enmStage.Max) As Decimal                                   '[目標座標:Y]
        Static mPosZ(enmStage.Max) As Decimal                                   '[目標座標:Z]
        Static mScene(enmStage.Max) As String                                   '[場景]
        Static mLightValue1(enmStage.Max) As Integer                            '[暫存光源數值]
        Static mLightValue2(enmStage.Max) As Integer                            '[暫存光源數值]
        Static mLightValue3(enmStage.Max) As Integer                            '[暫存光源數值]
        Static mLightValue4(enmStage.Max) As Integer                            '[暫存光源數值]
        Static mPath(enmStage.Max) As sCCDFixPath                               '[暫存路徑資訊]
        Static mLightCmdFailCount(enmStage.Max) As Integer
        Static mlight(enmStage.Max) As enmLight                                 '[光源]

        Dim mLightValue As Integer

        Dim mAxisXState(enmStage.Max) As CommandStatus                          '[X軸的狀態]
        Dim mAxisYState(enmStage.Max) As CommandStatus                          '[Y軸的狀態]
        Dim mAxisZState(enmStage.Max) As CommandStatus                          '[Z軸的狀態]

        Dim mAcc(enmStage.Max) As Decimal
        Dim mDec(enmStage.Max) As Decimal

        'Static mRec(enmStage.Max) As Stopwatch '時間量測

        Select Case sys.SysNum
            Case sCCDFixSysParam.SysLoopStart
                '[Note]:載入資料，初始化
                If IsNothing(mCCDStableTimeStopWatch(sys.StageNo)) Then
                    mCCDStableTimeStopWatch(sys.StageNo) = New Stopwatch
                End If

                If IsNothing(mCCDStableTimeStopWatch(sys.StageNo)) Then
                    mCCDStableTimeStopWatch(sys.StageNo) = New Stopwatch
                End If
                'If IsNothing(mRec(sys.StageNo)) Then
                '    mRec(sys.StageNo) = New Stopwatch
                'End If

                '[Note]:取定位座標
                mPath(sys.StageNo) = sys.CCDFixPathRegister(0)

                mPosX(sys.StageNo) = mPath(sys.StageNo).PosX
                mPosY(sys.StageNo) = mPath(sys.StageNo).PosY
                mPosZ(sys.StageNo) = mPath(sys.StageNo).PosZ

                '[Note]:
                If sys.IsResetLightValue = True Then
                    mLightValue1(sys.StageNo) = 0
                    mLightValue2(sys.StageNo) = 0
                    mLightValue3(sys.StageNo) = 0
                    mLightValue4(sys.StageNo) = 0
                    mScene(sys.StageNo) = ""
                End If
                gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 1100
            Case 1100
                '[Note]:檢查取像是否完成，可以移至下個取像位置(基本上不需要檢查，因為進來時候就必須是可以取像的狀態)
                If gAOICollection.IsCCDReady(sys.CCDNo) = True Then
                    gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 1200
                End If

            Case 1200
                '[Note]:移動至取像位置
                Dim mVelocity As Decimal = gCMotion.AxisParameter(sys.AxisX).Velocity.MaxVel

                If mGpSCurve = eCurveMode.SCurve Then
                    mAcc(sys.StageNo) = gCMotion.AxisParameter(sys.AxisX).Velocity.Acc * gCMotion.AxisParameter(sys.AxisX).Velocity.AccRatio * mSCurveRatio
                    mDec(sys.StageNo) = gCMotion.AxisParameter(sys.AxisX).Velocity.Dec * gCMotion.AxisParameter(sys.AxisX).Velocity.DecRatio * mSCurveRatio
                Else
                    mAcc(sys.StageNo) = gCMotion.AxisParameter(sys.AxisX).Velocity.Acc * gCMotion.AxisParameter(sys.AxisX).Velocity.AccRatio
                    mDec(sys.StageNo) = gCMotion.AxisParameter(sys.AxisX).Velocity.Dec * gCMotion.AxisParameter(sys.AxisX).Velocity.DecRatio
                End If


                With gCMotion.AxisParameter(sys.AxisX).Velocity
                    Dim mDeltaX As Decimal
                    Dim mDeltaY As Decimal
                    mDeltaX = mPosX(sys.StageNo) - gCMotion.GetPositionValue(sys.AxisX)
                    mDeltaY = mPosY(sys.StageNo) - gCMotion.GetPositionValue(sys.AxisY)

                    Dim mDistance As Decimal = Math.Sqrt(mDeltaX ^ 2 + mDeltaY ^ 2)

                    Premtek.CDispensingMath.GetCrossVelocity(gSSystemParameter.MaxCrossDeviceVelocity, mAcc(sys.StageNo), mDec(sys.StageNo), mDistance, 0, mVelocity)
                    'CDispensingMath.GetCrossVelocity(gSSystemParameter.MaxCrossDeviceVelocity, mAcc(sys.StageNo), mDec(sys.StageNo), mDistance, gSSystemParameter.CrossVerticalTime, mVelocity)
                    If mVelocity = 0 Then
                        mVelocity = gSSystemParameter.MaxCrossDeviceVelocity
                    End If
                    gCMotion.SyncParameter(sys.StageNo).TargetPos(0) = mPosX(sys.StageNo)
                    gCMotion.SyncParameter(sys.StageNo).TargetPos(1) = mPosY(sys.StageNo)
                    gCMotion.SyncParameter(sys.StageNo).TargetPos(2) = gCMotion.GetPositionValue(sys.AxisZ)
                    gCMotion.SyncParameter(sys.StageNo).Velocity.VelLow = 0
                    gCMotion.SyncParameter(sys.StageNo).Velocity.VelHigh = mVelocity
                    'Debug.Print("抑振速度:" & mVelocity)
                    'Debug.Print("平均加速度:" & mAcc(sys.StageNo) & "平均減速度:" & mDec(sys.StageNo))
                End With
                If gCMotion.GpSetVelLow(gCMotion.SyncParameter(sys.StageNo), 0) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036014", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036014), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048014", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048014), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066014", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066014), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073014", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073014), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetVelHigh(gCMotion.SyncParameter(sys.StageNo), mVelocity) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036013", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036013), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048013", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048013), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066013", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066013), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073013", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073013), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetAcc(gCMotion.SyncParameter(sys.StageNo), mAcc(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036011", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036011), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048011", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048011), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066011", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066011), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073011", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073011), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetDec(gCMotion.SyncParameter(sys.StageNo), mDec(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036012", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036012), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048012", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048012), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066012", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066012), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073012", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073012), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.GpSetCurve(gCMotion.SyncParameter(sys.StageNo), mGpSCurve) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036010", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036010), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048010", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048010), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066010", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066010), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073010", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073010), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpMoveLinearAbsXYZ(gCMotion.SyncParameter(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036000", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048000", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066000", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073000", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                'mRec(sys.StageNo).Restart() '計時開始
                
                gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 1700
            Case 1700
                '[說明]:等待到位
                If gCMotion.GpMoveDone(gCMotion.SyncParameter(sys.StageNo)) = CommandStatus.Sucessed Then
                    sys.SysNum = 1800
                End If
                

            Case 1800
                If gCMotion.AbsMove(sys.AxisZ, mPosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032000", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044000", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062000", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069000", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                Else
                    gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 1900
                End If

            Case 1900
                '[說明]:等待Z軸到位
                mAxisZState(sys.StageNo) = gCMotion.MotionDone(sys.AxisZ)
                If mAxisZState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032004", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044004", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062004", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069004", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                    Exit Sub
                Else
                    'Debug.Print("拍照移動到位" & mRec(sys.StageNo).ElapsedMilliseconds)
                    gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    mCCDStableTimeStopWatch(sys.StageNo).Restart()
                    sys.SysNum = 2000
                End If

            Case 2000
                '[Note]:切換場景
                '[Note]:場景不同時才做切換
                '[Note]:取場景名稱
                If mScene(sys.StageNo) <> mPath(sys.StageNo).AlignScene Then
                    mScene(sys.StageNo) = mPath(sys.StageNo).AlignScene
                    If Not gAOICollection.SceneDictionary.ContainsKey(mScene(sys.StageNo)) Then
                        'gEqpMsg.AddHistoryAlarm("Alarm_2012107", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2012107), eMessageLevel.Alarm)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2012107", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2012107), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2012407", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2012407), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2012707", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2012707), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2013007", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2013007), eMessageLevel.Alarm)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Call gAOICollection.SetCCDScene(sys.CCDNo, mScene(sys.StageNo))
                    End If
                End If
                gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                'mRec(sys.StageNo).Restart()
                sys.SysNum = 2100

            Case 2100
                '[Note]:光源切換
                '[Note]:總共四組光源(一個Stage)，光源開啟後暫無確認機制，若後續發現打光時間不夠，再補打光時間
                '[Note]:第一組光源
                mlight(sys.StageNo) = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
                Call gSysAdapter.SetLightOnOff(mlight(sys.StageNo), gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No1))
                If gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No1) = True Then
                    mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No1)
                    If mLightValue1(sys.StageNo) <> mLightValue Then
                        mLightCmdFailCount(sys.StageNo) = 0
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 2200
                    Else
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 2400
                    End If
                Else
                    gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 2400
                End If

            Case 2200
                mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No1)
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = False Then
                    If gLightCollection.SetCCDLight(sys.CCDNo, mlight(sys.StageNo), mLightValue) = True Then
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 2300
                    End If
                End If

            Case 2300
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gLightCollection.IsTimeOut(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 2200
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gLightCollection.GetResultStatus(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        mLightValue1(sys.StageNo) = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No1)
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 2400
                    Else
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 2200
                        End If
                    End If
                End If

            Case 2400
                '[Note]:第二組光源
                mlight(sys.StageNo) = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
                Call gSysAdapter.SetLightOnOff(mlight(sys.StageNo), gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No2))
                If gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No2) = True Then
                    mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No2)
                    If mLightValue2(sys.StageNo) <> mLightValue Then
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 2500
                    Else
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 2700
                    End If
                Else
                    gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 2700
                End If

            Case 2500
                mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No2)
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = False Then
                    If gLightCollection.SetCCDLight(sys.CCDNo, mlight(sys.StageNo), mLightValue) = True Then
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 2600
                    End If
                End If

            Case 2600
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gLightCollection.IsTimeOut(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 2500
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gLightCollection.GetResultStatus(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        mLightValue1(sys.StageNo) = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No2)
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 2700
                    Else
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 2500
                        End If
                    End If
                End If

            Case 2700
                '[Note]:第三組光源
                mlight(sys.StageNo) = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3)
                Call gSysAdapter.SetLightOnOff(mlight(sys.StageNo), gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No3))
                If gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No3) = True Then
                    mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No3)
                    If mLightValue3(sys.StageNo) <> mLightValue Then
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 2800
                    Else
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 3000
                    End If
                Else
                    gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 3000
                End If

            Case 2800
                mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No3)
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = False Then
                    If gLightCollection.SetCCDLight(sys.CCDNo, mlight(sys.StageNo), mLightValue) = True Then
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 2900
                    End If
                End If

            Case 2900
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gLightCollection.IsTimeOut(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 2800
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gLightCollection.GetResultStatus(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        mLightValue1(sys.StageNo) = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No3)
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 3000
                    Else
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 2800
                        End If
                    End If
                End If

            Case 3000
                '[Note]:第四組光源
                mlight(sys.StageNo) = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4)
                Call gSysAdapter.SetLightOnOff(mlight(sys.StageNo), gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No4))
                If gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No4) = True Then
                    mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No4)
                    If mLightValue4(sys.StageNo) <> mLightValue Then
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 3100
                    Else
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        'Debug.Print("到位後光源切換:" & mRec(sys.StageNo).ElapsedMilliseconds)
                        sys.SysNum = 4000
                    End If
                Else
                    gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    'Debug.Print("到位後光源切換:" & mRec(sys.StageNo).ElapsedMilliseconds)
                    sys.SysNum = 4000
                End If

            Case 3100
                mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No4)
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = False Then
                    If gLightCollection.SetCCDLight(sys.CCDNo, mlight(sys.StageNo), mLightValue) = True Then
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 3200
                    End If
                End If

            Case 3200
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gLightCollection.IsTimeOut(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 3100
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gLightCollection.GetResultStatus(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        mLightValue1(sys.StageNo) = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No4)
                        gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                        sys.SysNum = 4000
                    Else
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                            sys.SysNum = 3100
                        End If
                    End If
                End If

            Case 4000
                '[Note]:等待整定時間
                If mCCDStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds > gSSystemParameter.StableTime.CCDStableTime Then
                    mCCDStableTimeStopWatch(sys.StageNo).Stop()
                    gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    'Debug.Print("到位後穩定時間:" & mRec(sys.StageNo).ElapsedMilliseconds)
                    sys.SysNum = 4200
                End If

            Case 4200
                '[Note]:塞對應的Ticket數值(藉由SetCCDTrigger去撈對應的Ticket，OFF的時候去撈的話，是目前最後一個Index)
                '       SetCCDTrigger回傳的數值表示目前正準備接收第個影像，取完像之後數值才會加1。可參考CThreadCogAcqFifoTool的ToolBlock_Run
                'mRec(sys.StageNo).Restart() '
                Select Case mPath(sys.StageNo).AlignIndex
                    Case eAlignIndex.No1
                        gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SRecipePos(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY).Ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False)

                    Case eAlignIndex.No2
                        gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SRecipePos(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY).Ticket2 = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False)

                    Case eAlignIndex.No3
                        gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SRecipePos(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY).Ticket3 = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False)

                    Case eAlignIndex.SkipMark
                        gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SRecipePos(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY).SkipMarkTicket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False)
                End Select
                gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 4300

            Case 4300
                '[Note]:執行CCD拍照取像(並記錄最後的Ticket是多少)
                mCCDTimeStopWatch(sys.CCDNo).Restart()
                sys.LastTicket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False)
                'Debug.Print("Position @CCD Trigger Ticket:" & sys.LastTicket & " (" & gCMotion.GetPositionValue(sys.AxisX) & "," & gCMotion.GetPositionValue(sys.AxisY) & "," & gCMotion.GetPositionValue(sys.AxisZ) & ")") 'Soni + 2016.10.02待確認觸發時的位置是否正確.
                gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 4400

            Case 4400
                '[Note]:關閉取像，目前是下命令拍照 非I/O觸發取像，若改為I/O觸發取像，可能要考慮I/O保持之時間
                gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False)
                gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                sys.SysNum = 4500

            Case 4500
                '[Note]:檢查取像是否完成，可以完成此次的取像任務
                If gAOICollection.IsCCDReady(sys.CCDNo) = True Then
                    'Debug.Print("到位後穩定時間:" & mRec(sys.StageNo).ElapsedMilliseconds)
                    gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                    sys.SysNum = 9000
                End If

            Case 9000
                sys.RunStatus = enmRunStatus.Finish
                gSyslog.CCDSave("CCDFixNonOnFlyModelAction :" & sys.SysNum, CSystemLog.eCCDMessageProcess.Add)
                Exit Sub
        End Select

    End Sub




    ''' <summary>[從Recipe裡面取出Tilt角度]</summary>
    ''' <param name="recipe"></param>
    ''' <param name="stageNo"></param>
    ''' <param name="value"></param>
    ''' <param name="nodeName"></param>
    ''' <param name="roundNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTiltValue(ByVal recipe As CRecipe, ByVal stageNo As enmStage, ByRef value As Decimal, Optional ByVal nodeName As String = "", Optional ByVal roundNo As Integer = 0) As Boolean

        Dim mI As Integer
        Dim mNodeName As String
        Dim mPatternName As String
        Dim mStepNo As Integer
        Dim mValue As Decimal
        Dim mIsExit As Boolean

        mIsExit = False
        For mI = 0 To recipe.DispenseTraversal(stageNo).Count - 1
            '[說明]:找出對應的Node & Patterm
            mNodeName = recipe.DispenseTraversal(stageNo)(mI)
            mPatternName = recipe.Node(stageNo)(mNodeName).PatternName
            If nodeName <> "" Then
                '[Note]:根據節點名稱找到對應的Patterm
                If mNodeName = nodeName Then
                    If recipe.Pattern(mPatternName).Round.Count > 0 Then
                        If recipe.Pattern(mPatternName).Round(roundNo).StepCount > 0 Then
                            For mStepNo = 0 To recipe.Pattern(mPatternName).Round(roundNo).StepCount - 1
                                Select Case recipe.Pattern(mPatternName).Round(roundNo).CStep(mStepNo).StepType
                                    Case eStepFunctionType.SelectValve
                                        mValue = recipe.Pattern(mPatternName).Round(roundNo).CStep(mStepNo).SelectValve.PosB
                                        mIsExit = True
                                        Exit For
                                End Select
                            Next
                        End If
                    End If
                    If mIsExit = True Then
                        Exit For
                    End If
                End If
            Else
                If recipe.Pattern(mPatternName).Round.Count > 0 Then
                    If recipe.Pattern(mPatternName).Round(roundNo).StepCount > 0 Then
                        For mStepNo = 0 To recipe.Pattern(mPatternName).Round(roundNo).StepCount - 1
                            Select Case recipe.Pattern(mPatternName).Round(roundNo).CStep(mStepNo).StepType
                                Case eStepFunctionType.SelectValve
                                    mValue = recipe.Pattern(mPatternName).Round(roundNo).CStep(mStepNo).SelectValve.PosB
                                    mIsExit = True
                                    Exit For
                            End Select
                        Next
                    End If
                End If
                If mIsExit = True Then
                    Exit For
                End If
            End If
        Next

        If mIsExit = True Then
            value = mValue
            Return True
        Else
            '[Note]:找不到角度的資訊，所以將其視為0度
            value = 0
            Return True
        End If

    End Function




#Region "OnFly"

    ''' <summary>[計算助跑資料]</summary>
    ''' <param name="acc"></param>
    ''' <param name="dispPath"></param>
    ''' <param name="runUpParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DetermineRunUpTimeDistance(ByVal acc As Decimal, ByVal dispPath As sPatternPath, ByRef runUpParam As sExtendParam) As Boolean
        Dim mVelocity As Decimal                    '[mm/s]
        Dim mRunUpTime As Decimal                   '[助跑時間(s)]
        Dim mRunUpDistance As Decimal               '[助跑距離(mm)]
        Dim mValue As Decimal
        Dim mAcc As Decimal
        Const mDelayTimeBySignal As Decimal = 0.02  '[訊號追隨延遲時間(s)]
        '[Note]:計算助跑時間&距離
        mAcc = acc
        mVelocity = dispPath.Line3D.Velocity
        'mAcc = gSSystemParameter.GlobalAcc * gSSystemParameter.AccRatio
        mRunUpTime = mDelayTimeBySignal + (mVelocity - 0) / mAcc

        '[Note]:若速度為零，則完全不需助跑距離，連命令落後也不必理會
        If mVelocity = 0 Then
            mValue = 0
        Else
            mValue = CDec(Format(0.5 * mAcc * mRunUpTime * mRunUpTime, "0.0000#"))
        End If
        '20171005
        '[Note]:單純為確保(不相信機構跟得上所以加上去的)
        mRunUpDistance = mValue * gSSystemParameter.PathMultiple

        '[Note]:因為距離被放大4倍，故時間相對放大2倍=>改成手動設定
        With runUpParam
            .Velocity = mVelocity
            .Time = mRunUpTime * Math.Sqrt(gSSystemParameter.PathMultiple)
            .Distance = mRunUpDistance
        End With
        Return True
    End Function

    ''' <summary>
    ''' [飛拍觸發訊號 開啟 關閉]
    ''' </summary>
    ''' <param name="ccdNo"></param>
    ''' <param name="state"></param>
    ''' <remarks></remarks>
    Private Sub SetCCDonFlyTrigger(ByVal ccdNo As Integer, ByVal state As enmONOFF)
        Dim mIsOn As Boolean
        If state = enmONOFF.eON Then
            mIsOn = True
        Else
            mIsOn = False
        End If
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCS_350A
                'gDOCollection.GetSetState(enmDO.CcdImageTrigger) = mIsOn
                If gDOCollection.GetState(enmDO.DispensingTrigger1) <> mIsOn Then
                    gDOCollection.SetState(enmDO.DispensingTrigger1, mIsOn)
                End If
            Case enmMachineType.DCSW_800AQ
                Select Case ccdNo
                    Case enmCCD.CCD1
                        If gDOCollection.GetState(enmDO.DispensingTrigger1) <> mIsOn Then
                            gDOCollection.SetState(enmDO.DispensingTrigger1, mIsOn)
                        End If
                    Case enmCCD.CCD2
                        If gDOCollection.GetState(enmDO.DispensingTrigger2) <> mIsOn Then
                            gDOCollection.SetState(enmDO.DispensingTrigger2, mIsOn)
                        End If
                    Case enmCCD.CCD3
                        If gDOCollection.GetState(enmDO.DispensingTrigger3) <> mIsOn Then
                            gDOCollection.SetState(enmDO.DispensingTrigger3, mIsOn)
                        End If
                    Case enmCCD.CCD4
                        If gDOCollection.GetState(enmDO.DispensingTrigger4) <> mIsOn Then
                            gDOCollection.SetState(enmDO.DispensingTrigger4, mIsOn)
                        End If
                End Select
            Case enmMachineType.DCS_500AD
                Select Case ccdNo
                    Case enmCCD.CCD1
                        If gDOCollection.GetState(enmDO.DispensingTrigger1) <> mIsOn Then
                            gDOCollection.SetState(enmDO.DispensingTrigger1, mIsOn)
                        End If
                    Case enmCCD.CCD2
                        If gDOCollection.GetState(enmDO.DispensingTrigger2) <> mIsOn Then
                            gDOCollection.SetState(enmDO.DispensingTrigger2, mIsOn)
                        End If
                End Select

            Case Else
                '[Note]其他機台無飛拍
        End Select
    End Sub

    Private Sub SetLightOnOff(ByVal state As enmONOFF)
        Dim mIsOn As Boolean
        If state = enmONOFF.eON Then
            mIsOn = True
        Else
            mIsOn = False
        End If
        'Debug.Print("SetLightOnOff:" & mIsOn)
        gDOCollection.SetState(enmDO.CCDLight, mIsOn)
        gDOCollection.SetState(enmDO.CCDLight2, mIsOn)
        gDOCollection.SetState(enmDO.CCDLight3, mIsOn)
        gDOCollection.SetState(enmDO.CCDLight4, mIsOn)
        gDOCollection.SetState(enmDO.CCDLight5, mIsOn)
        gDOCollection.SetState(enmDO.CCDLight6, mIsOn)
        gDOCollection.SetState(enmDO.CCDLight7, mIsOn)
        gDOCollection.SetState(enmDO.CCDLight8, mIsOn)

    End Sub

    ''' <summary>[估算打點數(此次路徑移動)]</summary>
    ''' <param name="pathIndex"></param>
    ''' <param name="dispType"></param>
    ''' <param name="triggerDispPathRegister"></param>
    ''' <param name="parameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EstimateDispensingDotsCounts(ByVal pathIndex As Integer, ByVal dispType As enmTriggerDispType, ByVal MotionPath As sPatternPath, ByVal triggerDispPathRegister As List(Of sPatternPath), ByRef parameter As sTriggerVisionCmdParam) As Boolean

        Dim mI As Integer
        Dim mPatternPath As sPatternPath
        Dim mDotCounts As Integer                               '[打點顆數]
        Dim mTotalDotCounts As Integer                          '[統計打點總數]
        Dim mTriggerVisionCmdParam As sTriggerVisionCmdParam         '[助跑資訊]

        '[Note]:Only First Cmd--> Uesed in F Command

        parameter.ApproachPosX = 0
        parameter.ApproachPosY = 0
        parameter.TotalPointCounts = 0

        If pathIndex <> 0 Then
            Return False
        End If

        Select Case MotionPath.PathType
            Case ePathType.Dot3D
                With mTriggerVisionCmdParam
                    .ApproachPosX = triggerDispPathRegister.Item(0).Dot3D.PosX
                    .ApproachPosY = triggerDispPathRegister.Item(0).Dot3D.PosY
                    If triggerDispPathRegister.Count = 1 Then
                        .DelayTime = 0
                    Else
                        .DelayTime = gCRecipe.CCDOnFlyDelayTime
                    End If

                End With

            Case ePathType.Line3D
                With mTriggerVisionCmdParam
                    .ApproachPosX = (triggerDispPathRegister.Item(0).Line3D.StartPosX - MotionPath.Line3D.ExtendOffsetX)
                    .ApproachPosY = (triggerDispPathRegister.Item(0).Line3D.StartPosY - MotionPath.Line3D.ExtendOffsetY)
                    .DelayTime = gCRecipe.CCDOnFlyDelayTime
                End With

        End Select

        For mI = pathIndex To triggerDispPathRegister.Count - 1
            mPatternPath = triggerDispPathRegister.Item(mI)
            Select Case dispType
                Case enmTriggerDispType.VisionRecipe
                    Select Case mPatternPath.PathType
                        Case ePathType.Line3D
                            mDotCounts = mPatternPath.Line3D.CcdTiggerCount
                            mTotalDotCounts = mTotalDotCounts + mDotCounts

                        Case ePathType.Dot3D
                            mDotCounts = mPatternPath.Dot3D.CcdTiggerCount
                            mTotalDotCounts = mTotalDotCounts + mDotCounts

                        Case Else
                            Return False

                    End Select

            End Select
        Next
        mTriggerVisionCmdParam.TotalPointCounts = mTotalDotCounts
        parameter = mTriggerVisionCmdParam
        Return True
    End Function


    Private Function EditTriggerPathByVisionCmd(ByVal stageNo As enmStage, ByVal isTransmissionResuming As Boolean, ByVal pathIndex As Integer, ByVal lastPathIndex As Integer, ByVal dispType As enmTriggerDispType, ByVal motionPah As sPatternPath, ByVal triggerFixPathRegister As List(Of sPatternPath), Optional ByRef totalFixCounts As Integer = 0) As Boolean
        Dim mI As Integer
        Dim mIs1stPath As Boolean                       '[判斷是否為第一筆資料]
        Dim mIsLastPath As Boolean                      '[判斷是否為最後一筆資料]
        Dim mPatternPath As sPatternPath
        Dim mVisionCmdStep As sTriggerVisionCmdStep
        'Dim mDotCounts As Integer                       '[打點顆數]
        Dim mTriggerVisionCmdParam As sTriggerVisionCmdParam      '[助跑資訊]

        '[Note]S型時需補上EstimateDispensingDotsCounts
        If EstimateDispensingDotsCounts(pathIndex, dispType, motionPah, triggerFixPathRegister, mTriggerVisionCmdParam) = True Then
            totalFixCounts = mTriggerVisionCmdParam.TotalPointCounts
        End If

        '[Note]:保護不做在這一層(在資料串接時就要做到Valve之檢查)
        '       這邊只決定使用哪種類型串接& Command之串接

        If lastPathIndex > triggerFixPathRegister.Count - 1 Then
            '[Note]:基本上不可能發生，若發生則為前段估算錯誤
            lastPathIndex = triggerFixPathRegister.Count - 1
        End If

        For mI = pathIndex To lastPathIndex
            mPatternPath = triggerFixPathRegister.Item(mI)
            Select Case dispType
                Case enmTriggerDispType.VisionRecipe
                    Select Case mPatternPath.PathType
                        Case ePathType.Dot3D
                            With mVisionCmdStep
                                .Path = eTriggerBoardPathType.Dot
                                .Dir = eArcDir.CW
                                .Velocity = mPatternPath.Dot3D.Velocity
                                .PointCounts = mPatternPath.Dot3D.CcdTiggerCount
                                .StartPosX = mPatternPath.Dot3D.PosX
                                .StartPosY = mPatternPath.Dot3D.PosY
                                .EndPosX = 0
                                .EndPosY = 0
                                .CenPosX = 0
                                .CenPosY = 0
                            End With

                        Case ePathType.Line3D
                            With mVisionCmdStep
                                .Path = eTriggerBoardPathType.Line '[Note] Point
                                .Dir = eArcDir.CW
                                .Velocity = mPatternPath.Line3D.Velocity
                                .PointCounts = mPatternPath.Line3D.CcdTiggerCount
                                .StartPosX = mPatternPath.Line3D.StartPosX
                                .StartPosY = mPatternPath.Line3D.StartPosY
                                .EndPosX = mPatternPath.Line3D.EndPosX
                                .EndPosY = mPatternPath.Line3D.EndPosY
                                .CenPosX = 0
                                .CenPosY = 0
                            End With

                        Case Else
                            '[Note]:正常不應該發生，若發生要往回查資料是否錯誤(目前飛拍只有支援Dot、Line)
                            Return False

                    End Select

                    If mI = pathIndex Then
                        mIs1stPath = True
                    Else
                        mIs1stPath = False
                    End If
                    If mI = lastPathIndex Then
                        mIsLastPath = True
                    Else
                        mIsLastPath = False
                    End If

                    If mI = 0 Then
                        '[Note]:第一筆資料絕對不可能是續傳
                        Call gTriggerBoard.AddVisionRecipe(stageNo, mIs1stPath, mVisionCmdStep, mIsLastPath, mTriggerVisionCmdParam)
                    Else
                        If isTransmissionResuming = True Then
                            'Call gTriggerBoard.AddJetRecipeUseTransmissionResuming(stageNo, mIs1stPath, mVisionCmdStep, mIsLastPath)
                        Else
                            Call gTriggerBoard.AddVisionRecipe(stageNo, mIs1stPath, mVisionCmdStep, mIsLastPath)
                        End If
                    End If
            End Select
        Next

        Return True
    End Function



    ''' <summary>[判斷是否為最後一筆路徑且為不延伸路徑]</summary>
    ''' <param name="index"></param>
    ''' <param name="lastPathCount"></param>
    ''' <param name="patternPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsLastNonExtendPath(ByVal index As Integer, ByVal lastPathCount As Integer, ByVal patternPath As sPatternPath) As Boolean
        If index = lastPathCount - 1 Then
            Select Case patternPath.PathType
                Case ePathType.Arc2D
                    If patternPath.Arc2D.IsExtendOn = False Then
                        Return True
                    End If

                Case ePathType.Arc3D
                    If patternPath.Arc3D.IsExtendOn = False Then
                        Return True
                    End If
                Case ePathType.Dot3D
                    If patternPath.Dot3D.IsExtendOn = False Then
                        Return True
                    End If
                Case ePathType.Line3D
                    If patternPath.Line3D.IsExtendOn = False Then
                        Return True
                    End If
            End Select
            Return False
        Else
            Return False
        End If
    End Function

    ''' <summary>[判斷是否為下一顆的第一個Path]</summary>
    ''' <param name="mIs1stMotionPath"></param>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Is1stPathInDie(ByVal mIs1stMotionPath As Boolean, ByVal path As sPatternPath) As Boolean
        If mIs1stMotionPath = True Then
            Return True
        Else
            Select Case path.PathType
                Case ePathType.Arc2D
                    Return path.Arc2D.IsFristPathInDie
                Case ePathType.Arc3D
                    Return path.Arc3D.IsFristPathInDie
                Case ePathType.Dot3D
                    Return path.Dot3D.IsFristPathInDie
                Case ePathType.Line3D
                    Return path.Line3D.IsFristPathInDie
                Case Else
                    Return False
            End Select
            'Return False
        End If
    End Function

    ''' <summary>[路徑編輯]</summary>
    ''' <param name="FixParam"></param>
    ''' <param name="FixPathRegister"></param>
    ''' <param name="motionDispPathRegister"></param>
    ''' <param name="triggerDispPathRegister"></param>
    ''' <param name="maxExtendDistance"></param>
    ''' <param name="maxBlendTime"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditTriggerPath(ByVal FixParam As sCCDFixParam, ByVal FixPathRegister As List(Of sPatternPath), ByRef motionDispPathRegister As List(Of sPatternPath), ByRef triggerDispPathRegister As List(Of sPatternPath), ByRef maxExtendDistance As Decimal, ByRef maxBlendTime As Decimal, Optional ByVal updateTriggerDispPathRegister As Boolean = True) As Boolean

        Dim mRunUp As sExtendParam                                  '[助跑資料]
        Dim mCrossRunUp As sExtendParam                             '[跨元件助跑資料]
        Dim mLastPatternPath As New sPatternPath                    '[上一筆路徑資訊]
        Dim mArcExtendLinePath As New sPatternPath                  '[上一筆路徑資訊(由Arc延伸出來的線段)]
        Dim mIs1stMotionPath As Boolean                             '[為第一筆點膠資訊]
        Dim mI As Integer
        Dim mdx As Decimal
        Dim mdy As Decimal
        Dim mr As Decimal
        Dim mMotionDispPathRegister As List(Of sPatternPath)        '[motion]
        Dim mTriggerDispPathRegister As List(Of sPatternPath)       '[trigger]
        Dim mRegisterPath As sPatternPath
        Dim mExtendOffset As Premtek.sPos
        Dim mCrossExtendOffset As Premtek.sPos
        Dim mLastPos As Premtek.sPos
        Dim mPath As New sPatternPath
        Dim mTriggerPath As New sPatternPath
        Dim mMaxExtendDistance As Decimal
        Dim mMaxBlendTime As Decimal
        Dim mIs1stTriggerCommand As Boolean
        Dim mDotPitch As Decimal


        '[Note]:重新路徑規劃
        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
        mMotionDispPathRegister = New List(Of sPatternPath)
        mMotionDispPathRegister.Clear()
        mTriggerDispPathRegister = New List(Of sPatternPath)
        mTriggerDispPathRegister.Clear()
        mIs1stMotionPath = True
        mMaxExtendDistance = 0
        mMaxBlendTime = 0
        mIs1stTriggerCommand = True

        If FixPathRegister.Count > 0 Then
            For mI = 0 To FixPathRegister.Count - 1
                mRegisterPath = FixPathRegister.Item(mI)
                If Is1stPathInDie(mIs1stMotionPath, mRegisterPath) Then
                    '[Note]:第一筆需移動的Path
                    '       第一筆資料須與助跑做結合
                    Select Case mRegisterPath.PathType
                        Case ePathType.Line3D
                            '[Note]:算出需助跑的資料
                            Call DetermineRunUpTimeDistance(FixParam.Acc, FixPathRegister.Item(mI), mRunUp)
                            mCrossRunUp = mRunUp
                            '[Note]:起始點位置不用再向外擴
                            If mI = 0 Then
                                mCrossRunUp = mRunUp
                            End If
                            If mRunUp.Distance > mMaxExtendDistance Then
                                mMaxExtendDistance = mRunUp.Distance
                            End If
                            If mRunUp.Time > mMaxBlendTime Then
                                mMaxBlendTime = mRunUp.Time
                            End If

                            With mRegisterPath.Line3D
                                mdx = .EndPosX - .StartPosX
                                mdy = .EndPosY - .StartPosY
                                mr = CDec(Sqrt(mdx * mdx + mdy * mdy))
                                If mr = 0 Then
                                    mExtendOffset.PosX = 0
                                    mExtendOffset.PosY = 0
                                    mCrossExtendOffset.PosX = 0
                                    mCrossExtendOffset.PosY = 0
                                Else
                                    mExtendOffset.PosX = mRunUp.Distance * mdx / mr
                                    mExtendOffset.PosY = mRunUp.Distance * mdy / mr
                                    mCrossExtendOffset.PosX = mCrossRunUp.Distance * mdx / mr
                                    mCrossExtendOffset.PosY = mCrossRunUp.Distance * mdy / mr
                                End If
                            End With

                            '[Note]:跨元件用不同速走
                            If mI <> 0 Then
                                mPath.PathType = ePathType.Dot3D
                                mPath.Dot3D.PosX = mRegisterPath.Line3D.StartPosX - mCrossExtendOffset.PosX
                                mPath.Dot3D.PosY = mRegisterPath.Line3D.StartPosY - mCrossExtendOffset.PosY
                                mPath.Dot3D.PosZ = mRegisterPath.Line3D.EndPosZ
                                mPath.Dot3D.Velocity = mRunUp.Velocity
                                mPath.Dot3D.ParameterType = ePathParameterType.Line
                                mPath.Dot3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                mPath.Dot3D.IsDispense = False
                                mPath.IsFristPathInDie = True
                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                mMotionDispPathRegister.Add(mPath)
                                'Debug.Print("C1st: " & mPath.Line3D.StartPosX & " , " & mPath.Line3D.StartPosY & " --> " & mPath.Line3D.EndPosX & " , " & mPath.Line3D.EndPosY)
                            End If

                            mPath.PathType = ePathType.Line3D
                            mPath.Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mCrossExtendOffset.PosX
                            mPath.Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mCrossExtendOffset.PosY
                            mPath.Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ

                            If IsLastNonExtendPath(mI, FixPathRegister.Count, mRegisterPath) = True Then
                                mPath.Line3D.EndPosX = mRegisterPath.Line3D.EndPosX
                                mPath.Line3D.EndPosY = mRegisterPath.Line3D.EndPosY
                            Else
                                mPath.Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                mPath.Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                            End If
                            mPath.Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                            mPath.Line3D.ExtendOffsetX = mExtendOffset.PosX
                            mPath.Line3D.ExtendOffsetY = mExtendOffset.PosY
                            mPath.Line3D.Velocity = mRunUp.Velocity
                            mPath.Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                            mPath.Line3D.ParameterType = ePathParameterType.Line
                            mPath.Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                            mPath.IsFristPathInDie = False
                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                            mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                            mMotionDispPathRegister.Add(mPath)
                            'Debug.Print("1st: " & mPath.Line3D.StartPosX & " , " & mPath.Line3D.StartPosY & " --> " & mPath.Line3D.EndPosX & " , " & mPath.Line3D.EndPosY)
                            mLastPos.PosX = mPath.Line3D.EndPosX
                            mLastPos.PosY = mPath.Line3D.EndPosY

                            '[Note]:紀錄最近一筆的路徑資料
                            With mLastPatternPath
                                .PathType = ePathType.Line3D
                                .Line3D.StartPosX = mPath.Line3D.StartPosX
                                .Line3D.StartPosY = mPath.Line3D.StartPosY
                                .Line3D.StartPosZ = mPath.Line3D.StartPosZ
                                .Line3D.EndPosX = mPath.Line3D.EndPosX
                                .Line3D.EndPosY = mPath.Line3D.EndPosY
                                .Line3D.EndPosZ = mPath.Line3D.EndPosZ
                                .Line3D.ExtendOffsetX = mPath.Line3D.ExtendOffsetX
                                .Line3D.ExtendOffsetY = mPath.Line3D.ExtendOffsetY
                                .Line3D.Velocity = mPath.Line3D.Velocity
                            End With
                            '***************************************************
                            If mRegisterPath.Line3D.IsDispense = True And updateTriggerDispPathRegister = True Then
                                '[Note]:塞給Trigger board Data(只有第一組Trigger Cmd 需要助跑資訊，所以要把這資料保留給Cmd)
                                mTriggerPath = mRegisterPath
                                If mIs1stTriggerCommand = True Then
                                    mTriggerPath.Line3D.ExtendOffsetX = mCrossExtendOffset.PosX
                                    mTriggerPath.Line3D.ExtendOffsetY = mCrossExtendOffset.PosY
                                    mIs1stTriggerCommand = False
                                End If
                                mTriggerPath.Line3D.Velocity = mRunUp.Velocity
                                mTriggerPath.Line3D.WeightControl.dotPitch = mDotPitch
                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                mTriggerDispPathRegister.Capacity = mTriggerDispPathRegister.Count + 1
                                mTriggerDispPathRegister.Add(mTriggerPath)
                            End If
                            '**************************************************
                        Case ePathType.Wait
                            '[Note]:不可能進來
                    End Select
                End If
            Next
        End If
        '[Note]:mMaxBlendTime20 單位:s
        maxBlendTime = mMaxBlendTime
        maxExtendDistance = mMaxExtendDistance
        motionDispPathRegister = mMotionDispPathRegister
        triggerDispPathRegister = mTriggerDispPathRegister
        Return True
    End Function

    ''' <summary>[與前一筆與這目前這筆路徑的關係]</summary>
    ''' <param name="lastPath"></param>
    ''' <param name="nowPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TwoPathModel(ByVal lastPath As sPatternPath, ByVal nowPath As sPatternPath) As eTwoPathModel
        '[Note]:不可能會有Wait(已經被擋掉了)
        Select Case lastPath.PathType
            Case ePathType.Arc2D
                Select Case nowPath.PathType
                    Case ePathType.Arc2D
                        Return eTwoPathModel.ArcArc

                    Case ePathType.Arc3D
                        Return eTwoPathModel.ArcArc

                    Case ePathType.Dot3D
                        Return eTwoPathModel.ArcDots

                    Case ePathType.Line3D
                        Return eTwoPathModel.ArcLine

                End Select

            Case ePathType.Arc3D
                Select Case nowPath.PathType
                    Case ePathType.Arc2D
                        Return eTwoPathModel.ArcArc

                    Case ePathType.Arc3D
                        Return eTwoPathModel.ArcArc

                    Case ePathType.Dot3D
                        Return eTwoPathModel.ArcDots

                    Case ePathType.Line3D
                        Return eTwoPathModel.ArcLine

                End Select

            Case ePathType.Dot3D
                Select Case nowPath.PathType
                    Case ePathType.Arc2D
                        Return eTwoPathModel.DotsArc

                    Case ePathType.Arc3D
                        Return eTwoPathModel.DotsArc

                    Case ePathType.Dot3D
                        Return eTwoPathModel.DotsDots

                    Case ePathType.Line3D
                        Return eTwoPathModel.DotsLine

                End Select

            Case ePathType.Line3D
                Select Case nowPath.PathType
                    Case ePathType.Arc2D
                        Return eTwoPathModel.LineArc

                    Case ePathType.Arc3D
                        Return eTwoPathModel.LineArc

                    Case ePathType.Dot3D
                        Return eTwoPathModel.LineDots

                    Case ePathType.Line3D
                        Return eTwoPathModel.LineLine
                End Select
        End Select
        Return eTwoPathModel.None
    End Function



    ''' <summary>[判斷是否需要走延伸路徑]</summary>
    ''' <param name="lastPattternPath"></param>
    ''' <param name="nowPatternPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsUseExtendPath(ByVal lastPattternPath As sPatternPath, ByVal nowPatternPath As sPatternPath) As Boolean
        Dim mExtendPath As Boolean

        mExtendPath = True
        Select Case nowPatternPath.PathType
            Case ePathType.Arc2D
                If nowPatternPath.Arc2D.IsExtendOn = False Then
                    mExtendPath = False
                End If

            Case ePathType.Arc3D
                If nowPatternPath.Arc3D.IsExtendOn = False Then
                    mExtendPath = False
                End If

            Case ePathType.Dot3D
                If nowPatternPath.Dot3D.IsExtendOn = False Then
                    mExtendPath = False
                End If

            Case ePathType.Line3D
                If nowPatternPath.Line3D.IsExtendOn = False Then
                    mExtendPath = False
                End If

        End Select

        Return mExtendPath
    End Function

    ''' <summary>[根據(圓的直徑)線段決定圓弧之行進方向]</summary>
    ''' <param name="vectorU1">(圓的直徑)線段之法向量</param>
    ''' <param name="vectorU2">(圓的直徑)線段之法向量</param>
    ''' <param name="circlePos">圓的起始點</param>
    ''' <param name="centerPos">圓心</param>
    ''' <param name="aidPos">輔助判斷點</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UseLineToDetermineArcDirection(ByVal vectorU1 As Decimal, ByVal vectorU2 As Decimal, ByVal circlePos As Premtek.sPos, ByVal centerPos As Premtek.sPos, ByVal aidPos As Premtek.sPos) As eArcDirection

        Dim mPosNo1 As Premtek.sPos
        Dim mPosNo2 As Premtek.sPos
        Dim mCos1 As Decimal
        Dim mSin1 As Decimal
        Dim mCos2 As Decimal
        Dim mSin2 As Decimal
        Dim mAp As Decimal
        Dim mBp As Decimal
        Dim mCp As Decimal
        Dim mD1 As Decimal
        Dim mD2 As Decimal
        Dim mD3 As Decimal

        '[Note]:藉由中心點&圓上的點尋求與其夾角90度的二點座標
        '       第一組為CCW  逆轉 --->轉回來為順時針
        '       第二組為CW   正轉 --->轉回來為逆時針
        'mSin1 = SinTrigonometricFunction(90)
        'mCos1 = CosTrigonometricFunction(90)
        'mSin2 = SinTrigonometricFunction(-90)
        'mCos2 = CosTrigonometricFunction(-90)
        mSin1 = 1
        mCos1 = 0
        mSin2 = -1
        mCos2 = 0

        mPosNo1.PosX = mCos1 * (circlePos.PosX - centerPos.PosX) - mSin1 * (circlePos.PosY - centerPos.PosY) + centerPos.PosX
        mPosNo1.PosY = mSin1 * (circlePos.PosX - centerPos.PosX) + mCos1 * (circlePos.PosY - centerPos.PosY) + centerPos.PosY
        mPosNo2.PosX = mCos2 * (circlePos.PosX - centerPos.PosX) - mSin2 * (circlePos.PosY - centerPos.PosY) + centerPos.PosX
        mPosNo2.PosY = mSin2 * (circlePos.PosX - centerPos.PosX) + mCos2 * (circlePos.PosY - centerPos.PosY) + centerPos.PosY
        mAp = vectorU1
        mBp = vectorU2
        mCp = -((mAp * circlePos.PosX) + (mBp * circlePos.PosY))
        '[Note]:L:ApX+BpY+Cp=0
        '       判斷(mPosNo1)(aidPos)是否在同一側
        '       判斷(mPosNo2)(aidPos)是否在同一側
        mD1 = (mAp * mPosNo1.PosX) + (mBp * mPosNo1.PosY) + mCp
        mD2 = (mAp * aidPos.PosX) + (mBp * aidPos.PosY) + mCp
        mD3 = (mAp * mPosNo2.PosX) + (mBp * mPosNo2.PosY) + mCp

        If mD1 * mD2 > 0 Then
            '[Note]:同一側
            Return eArcDirection.CW
        Else
            If mD2 * mD3 > 0 Then
                '[Note]:同一側
                Return eArcDirection.CCW
            Else
                '[Note]:例外情況
                Return eArcDirection.CW
            End If
        End If

    End Function



    ''' <summary>[根據交點判斷是否要延伸此線段]</summary>
    ''' <param name="path"></param>
    ''' <param name="isLastPath"></param>
    ''' <param name="noodle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsExtendLine(ByVal path As sPatternPath, ByVal isLastPath As Boolean, ByVal noodle As Premtek.sPos) As Boolean

        Dim mN1 As Decimal    '[N=(n1,n2)]
        Dim mN2 As Decimal
        Dim mdN1 As Decimal
        Dim mdN2 As Decimal
        Dim mdNr As Decimal
        Dim mM1 As Decimal    '[M=(m1,m2)]
        Dim mM2 As Decimal
        Dim mdM1 As Decimal
        Dim mdM2 As Decimal
        Dim mdMr As Decimal

        '[Note]:由於這邊求的都是單位向量，所以容許誤差設定為0.01(1%)



        '[Note]:根據交點判斷要延伸哪一邊的線段(藉由向量來判斷)
        With path.Line3D
            mN1 = .EndPosX - .StartPosX
            mN2 = .EndPosY - .StartPosY
        End With
        mdNr = CDec(Sqrt(mN1 * mN1 + mN2 * mN2))
        If mdNr = 0 Then
            mdN1 = 0
            mdN2 = 0
        Else
            mdN1 = mN1 / mdNr
            mdN2 = mN2 / mdNr
        End If

        If isLastPath = True Then
            If path.Line3D.EndPosX = noodle.PosX And path.Line3D.EndPosY = noodle.PosY Then
                '[Note]:同一點
                Return True
            Else
                mM1 = noodle.PosX - path.Line3D.EndPosX
                mM2 = noodle.PosY - path.Line3D.EndPosY
                mdMr = CDec(Sqrt(mM1 * mM1 + mM2 * mM2))
                If mdMr = 0 Then
                    '[Note]:同一點
                    mdM1 = 0
                    mdMr = 0
                    Return True
                Else
                    mdM1 = mM1 / mdMr
                    mdM2 = mM2 / mdMr
                    If Math.Abs(mdN1 - mdM1) < gSSystemParameter.MotionTolerance And Math.Abs(mdN2 - mdM2) < gSSystemParameter.MotionTolerance Then
                        '[Note]:mdN1 = mdM1  And   mdN2 = mdM2
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If
        Else
            If path.Line3D.StartPosX = noodle.PosX And path.Line3D.StartPosY = noodle.PosY Then
                '[Note]:同一點
                Return True
            Else
                mM1 = path.Line3D.StartPosX - noodle.PosX
                mM2 = path.Line3D.StartPosY - noodle.PosY
                mdMr = CDec(Sqrt(mM1 * mM1 + mM2 * mM2))
                If mdMr = 0 Then
                    '[Note]:同一點
                    mdM1 = 0
                    mdMr = 0
                    Return True
                Else
                    mdM1 = mM1 / mdMr
                    mdM2 = mM2 / mdMr
                    If Math.Abs(mdN1 - mdM1) < gSSystemParameter.MotionTolerance And Math.Abs(mdN2 - mdM2) < gSSystemParameter.MotionTolerance Then
                        '[Note]:mdN1 = mdM1  And   mdN2 = mdM2
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If
        End If

    End Function


    ''' <summary>[判斷要延伸哪一條線段]</summary>
    ''' <param name="velHigh"></param>
    ''' <param name="acc"></param>
    ''' <param name="dec"></param>
    ''' <param name="lastPath"></param>
    ''' <param name="newPath"></param>
    ''' <param name="pathList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DetermineExtendWhichLine(ByVal velHigh As Decimal, ByVal acc As Decimal, ByVal dec As Decimal, ByVal lastPath As sPatternPath, ByVal newPath As sPatternPath, Optional ByRef pathList As List(Of sPatternPath) = Nothing) As Boolean

        Dim mA As Decimal
        Dim mB As Decimal
        Dim mC1 As Decimal
        Dim mC2 As Decimal
        Dim mU1 As Decimal   '[U=(u1,u2)]
        Dim mU2 As Decimal
        Dim mNoodleNo1 As Premtek.sPos
        Dim mNoodleNo2 As Premtek.sPos
        Dim mPos As Premtek.sPos
        Dim mDistance As Decimal '[二條線段的距離]
        Dim mIsAddNoodleNo1 As Boolean
        Dim mIsAddNoodleNo2 As Boolean
        Dim mPatternPath As New sPatternPath
        Dim mCenterPos As Premtek.sPos
        Dim mCirclePos As Premtek.sPos
        Dim mAidPos As Premtek.sPos
        Dim mDir As eArcDirection
        Dim mDisDx As Decimal
        Dim mDisDy As Decimal
        Dim mSubDistance As Decimal

        If lastPath.PathType = ePathType.Line3D And newPath.PathType = ePathType.Line3D Then
            'Step1:求二條線之線段方程式(利用向量求得)
            '             L1:AX+BY+C1=0  LastPath
            '             L2:AX+BY+C2=0  NewPath
            With newPath.Line3D
                mU1 = .EndPosX - .StartPosX
                mU2 = .EndPosY - .StartPosY
            End With
            mA = -mU2
            mB = mU1
            mC1 = -((mA * lastPath.Line3D.EndPosX) + (mB * lastPath.Line3D.EndPosY))
            mC2 = -((mA * newPath.Line3D.StartPosX) + (mB * newPath.Line3D.StartPosY))

            'Step2:求點至條線之交點線段方程式(利用向量求得)
            '             (x1,y1)至L2:AX+BY+C2=0之交點   -->上一條線的EndPos與下一條線的交點(mNoodleX1,mNoodleY1)
            '             (x2,y2)至L1:AX+BY+C1=0之交點   -->下一條線的StartPos與上一條線的交點(mNoodleX1,mNoodleY1)
            mPos.PosX = lastPath.Line3D.EndPosX
            mPos.PosY = lastPath.Line3D.EndPosY
            mNoodleNo1.PosX = ((mB * mB * mPos.PosX) - (mA * mB * mPos.PosY) - (mA * mC2)) / (mA * mA + mB * mB)
            mNoodleNo1.PosY = (-(mA * mB * mPos.PosX) + (mA * mA * mPos.PosY) - (mB * mC2)) / (mA * mA + mB * mB)

            mPos.PosX = newPath.Line3D.StartPosX
            mPos.PosY = newPath.Line3D.StartPosY
            mNoodleNo2.PosX = ((mB * mB * mPos.PosX) - (mA * mB * mPos.PosY) - (mA * mC1)) / (mA * mA + mB * mB)
            mNoodleNo2.PosY = (-(mA * mB * mPos.PosX) + (mA * mA * mPos.PosY) - (mB * mC1)) / (mA * mA + mB * mB)
            mDistance = (Math.Abs(mC1 - mC2)) / CDec(Sqrt(mA * mA + mB * mB))
            'Step3:根據交點判斷要延伸哪一邊的線段(藉由向量來判斷)
            If IsExtendLine(newPath, False, mNoodleNo1) Then
                mIsAddNoodleNo1 = True
            Else
                mIsAddNoodleNo1 = False
            End If

            If IsExtendLine(lastPath, True, mNoodleNo2) Then
                mIsAddNoodleNo2 = True
            Else
                mIsAddNoodleNo2 = False
            End If

            If mIsAddNoodleNo1 = True Then
                If mIsAddNoodleNo2 = True Then
                    '[Note]:二個端點，直接用弧連起來-->NewPath
                    '[Note]:線段方程式(垂直於二平行線之端點)
                    mCenterPos.PosX = (newPath.Line3D.StartPosX + lastPath.Line3D.EndPosX) / 2
                    mCenterPos.PosY = (newPath.Line3D.StartPosY + lastPath.Line3D.EndPosY) / 2
                    mCirclePos.PosX = lastPath.Line3D.EndPosX
                    mCirclePos.PosY = lastPath.Line3D.EndPosY
                    mAidPos.PosX = newPath.Line3D.EndPosX
                    mAidPos.PosY = newPath.Line3D.EndPosY
                    mDir = UseLineToDetermineArcDirection(mU1, mU2, mCirclePos, mCenterPos, mAidPos)
                    '[Note]:畫圓弧
                    With mPatternPath
                        .PathType = ePathType.Arc2D
                        .Arc2D.StartPosX = mNoodleNo2.PosX
                        .Arc2D.StartPosY = mNoodleNo2.PosY
                        .Arc2D.StartPosZ = newPath.Line3D.StartPosZ
                        .Arc2D.EndPosX = newPath.Line3D.StartPosX
                        .Arc2D.EndPosY = newPath.Line3D.StartPosY
                        .Arc2D.EndPosZ = newPath.Line3D.StartPosZ
                        .Arc2D.CenterPosX = (newPath.Line3D.StartPosX + mNoodleNo2.PosX) / 2
                        .Arc2D.CenterPosY = (newPath.Line3D.StartPosY + mNoodleNo2.PosY) / 2
                        .Arc2D.CenterPosZ = newPath.Line3D.StartPosZ
                        .Arc2D.ArcDirection = mDir
                        .Arc2D.Velocity = newPath.Line3D.Velocity
                        mDisDx = (.Arc2D.EndPosX - .Arc2D.CenterPosX)
                        mDisDy = (.Arc2D.EndPosY - .Arc2D.CenterPosY)
                        mSubDistance = CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)) * Math.PI
                        'Call EstimateMaxVel(velHigh, acc, dec, mSubDistance, newPath.Line3D.Velocity, .Arc2D.Velocity)
                        velHigh = Premtek.CDispensingMath.GetMaxTangentialVelocity(acc, Math.Sqrt((.Arc2D.StartPosX - .Arc2D.CenterPosX) ^ 2 + (.Arc2D.StartPosY - .Arc2D.CenterPosY) ^ 2)) 'Soni + 2017.08.31 取得切線速度限制
                        If gSSystemParameter.MaxCrossDeviceVelocity > 0 And gSSystemParameter.MaxCrossDeviceVelocity < velHigh Then '數值有設定且低於極限值才能用
                            velHigh = gSSystemParameter.MaxCrossDeviceVelocity
                        End If
                        Premtek.CDispensingMath.GetCrossVelocity(velHigh, acc, dec, mSubDistance, gSSystemParameter.CrossVerticalTime, .Arc2D.Velocity) 'Soni Soni + 2017.08.14 推算速度公式修改

                        .Arc2D.IsDispense = False
                        .IsFristPathInDie = False
                    End With

                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                    pathList.Capacity = pathList.Count + 1
                    pathList.Add(mPatternPath)
                    'Debug.Print("Pos: " & mPatternPath.Arc2D.StartPosX & " , " & mPatternPath.Arc2D.StartPosY & " --> " & mPatternPath.Arc2D.EndPosX & " , " & mPatternPath.Arc2D.EndPosY)
                    'Debug.Print("Center " & mPatternPath.Arc2D.CenterPosX & " , " & mPatternPath.Arc2D.CenterPosY)

                    '[Note]:補原本的線段
                    With mPatternPath
                        .PathType = ePathType.Line3D
                        .Line3D.StartPosX = newPath.Line3D.StartPosX
                        .Line3D.StartPosY = newPath.Line3D.StartPosY
                        .Line3D.StartPosZ = newPath.Line3D.StartPosZ
                        .Line3D.EndPosX = newPath.Line3D.EndPosX
                        .Line3D.EndPosY = newPath.Line3D.EndPosY
                        .Line3D.EndPosZ = newPath.Line3D.EndPosZ
                        .Line3D.Velocity = newPath.Line3D.Velocity
                        .Line3D.IsDispense = newPath.Line3D.IsDispense
                        .Line3D.ParameterType = ePathParameterType.Line
                        .Line3D.ParameterName = newPath.Line3D.ParameterName
                        .IsFristPathInDie = False
                    End With

                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                    pathList.Capacity = pathList.Count + 1
                    pathList.Add(mPatternPath)
                    'Debug.Print("Pos: " & mPatternPath.Line3D.StartPosX & " , " & mPatternPath.Line3D.StartPosY & " --> " & mPatternPath.Line3D.EndPosX & " , " & mPatternPath.Line3D.EndPosY)
                    Return True
                Else
                    '[Note]:畫圓弧-->延伸線段(with NewPath)
                    '[Note]:線段方程式(垂直於二平行線之端點)
                    mCenterPos.PosX = (lastPath.Line3D.EndPosX + mNoodleNo1.PosX) / 2
                    mCenterPos.PosY = (lastPath.Line3D.EndPosY + mNoodleNo1.PosY) / 2
                    mCirclePos.PosX = lastPath.Line3D.EndPosX
                    mCirclePos.PosY = lastPath.Line3D.EndPosY
                    mAidPos.PosX = lastPath.Line3D.StartPosX
                    mAidPos.PosY = lastPath.Line3D.StartPosY
                    mDir = UseLineToDetermineArcDirection(mU1, mU2, mCirclePos, mCenterPos, mAidPos)
                    '[Note]:畫圓弧
                    With mPatternPath
                        .PathType = ePathType.Arc2D
                        .Arc2D.StartPosX = lastPath.Line3D.EndPosX
                        .Arc2D.StartPosY = lastPath.Line3D.EndPosY
                        .Arc2D.StartPosZ = newPath.Line3D.StartPosZ
                        .Arc2D.EndPosX = mNoodleNo1.PosX
                        .Arc2D.EndPosY = mNoodleNo1.PosY
                        .Arc2D.EndPosZ = newPath.Line3D.StartPosZ
                        .Arc2D.CenterPosX = (lastPath.Line3D.EndPosX + mNoodleNo1.PosX) / 2
                        .Arc2D.CenterPosY = (lastPath.Line3D.EndPosY + mNoodleNo1.PosY) / 2
                        .Arc2D.CenterPosZ = newPath.Line3D.StartPosZ
                        .Arc2D.ArcDirection = mDir
                        .Arc2D.Velocity = newPath.Line3D.Velocity
                        mDisDx = (.Arc2D.EndPosX - .Arc2D.CenterPosX)
                        mDisDy = (.Arc2D.EndPosY - .Arc2D.CenterPosY)
                        mSubDistance = CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)) * Math.PI
                        'Call EstimateMaxVel(velHigh, acc, dec, mSubDistance, newPath.Line3D.Velocity, .Arc2D.Velocity)
                        velHigh = Premtek.CDispensingMath.GetMaxTangentialVelocity(acc, Math.Sqrt((.Arc2D.StartPosX - .Arc2D.CenterPosX) ^ 2 + (.Arc2D.StartPosY - .Arc2D.CenterPosY) ^ 2)) 'Soni + 2017.08.31 取得切線速度限制
                        If gSSystemParameter.MaxCrossDeviceVelocity > 0 And gSSystemParameter.MaxCrossDeviceVelocity < velHigh Then '數值有設定且低於極限值才能用
                            velHigh = gSSystemParameter.MaxCrossDeviceVelocity
                        End If
                        Premtek.CDispensingMath.GetCrossVelocity(velHigh, acc, dec, mSubDistance, gSSystemParameter.CrossVerticalTime, .Arc2D.Velocity) 'Soni Soni + 2017.08.14 推算速度公式修改
                        .Arc2D.IsDispense = False
                        .Line3D.ParameterType = ePathParameterType.Line
                        .Line3D.ParameterName = newPath.Line3D.ParameterName
                        .IsFristPathInDie = False
                    End With

                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                    pathList.Capacity = pathList.Count + 1
                    pathList.Add(mPatternPath)

                    '*******************************************************************
                    'Debug.Print("Pos: " & mPatternPath.Arc2D.StartPosX & " , " & mPatternPath.Arc2D.StartPosY & " --> " & mPatternPath.Arc2D.EndPosX & " , " & mPatternPath.Arc2D.EndPosY)
                    'Debug.Print("Center " & mPatternPath.Arc2D.CenterPosX & " , " & mPatternPath.Arc2D.CenterPosY)

                    '[Note]:延伸線段(原先線段)
                    With mPatternPath
                        .PathType = ePathType.Line3D
                        .Line3D.StartPosX = mNoodleNo1.PosX
                        .Line3D.StartPosY = mNoodleNo1.PosY
                        .Line3D.StartPosZ = newPath.Line3D.StartPosZ
                        .Line3D.EndPosX = newPath.Line3D.EndPosX
                        .Line3D.EndPosY = newPath.Line3D.EndPosY
                        .Line3D.EndPosZ = newPath.Line3D.EndPosZ
                        .Line3D.Velocity = newPath.Line3D.Velocity
                        .Line3D.IsDispense = newPath.Line3D.IsDispense
                        .Line3D.ParameterType = ePathParameterType.Line
                        .Line3D.ParameterName = newPath.Line3D.ParameterName
                        .IsFristPathInDie = False
                    End With

                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                    pathList.Capacity = pathList.Count + 1
                    pathList.Add(mPatternPath)
                    'Debug.Print("Pos: " & mPatternPath.Line3D.StartPosX & " , " & mPatternPath.Line3D.StartPosY & " --> " & mPatternPath.Line3D.EndPosX & " , " & mPatternPath.Line3D.EndPosY)
                    Return True
                End If
            Else
                If mIsAddNoodleNo2 = True Then
                    '[Note]:延伸線段(修改上一線段的端點)-->畫圓弧-->NewPath
                    '[Note]:線段方程式(垂直於二平行線之端點)
                    mCenterPos.PosX = (newPath.Line3D.StartPosX + mNoodleNo2.PosX) / 2
                    mCenterPos.PosY = (newPath.Line3D.StartPosY + mNoodleNo2.PosY) / 2
                    mCirclePos.PosX = mNoodleNo2.PosX
                    mCirclePos.PosY = mNoodleNo2.PosY
                    mAidPos.PosX = newPath.Line3D.EndPosX
                    mAidPos.PosY = newPath.Line3D.EndPosY
                    mDir = UseLineToDetermineArcDirection(mU1, mU2, mCirclePos, mCenterPos, mAidPos)

                    '[Note]:延伸線段(修改上一線段的端點)
                    mPatternPath = pathList.Last
                    pathList.Remove(mPatternPath)
                    With mPatternPath
                        .Line3D.EndPosX = mNoodleNo2.PosX
                        .Line3D.EndPosY = mNoodleNo2.PosY
                    End With
                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                    pathList.Capacity = pathList.Count + 1
                    pathList.Add(mPatternPath)
                    '[Note]:畫圓弧
                    With mPatternPath
                        .PathType = ePathType.Arc2D
                        .Arc2D.StartPosX = mNoodleNo2.PosX
                        .Arc2D.StartPosY = mNoodleNo2.PosY
                        .Arc2D.StartPosZ = newPath.Line3D.StartPosZ
                        .Arc2D.EndPosX = newPath.Line3D.StartPosX
                        .Arc2D.EndPosY = newPath.Line3D.StartPosY
                        .Arc2D.EndPosZ = newPath.Line3D.StartPosZ
                        .Arc2D.CenterPosX = (newPath.Line3D.StartPosX + mNoodleNo2.PosX) / 2
                        .Arc2D.CenterPosY = (newPath.Line3D.StartPosY + mNoodleNo2.PosY) / 2
                        .Arc2D.CenterPosZ = newPath.Line3D.StartPosZ
                        .Arc2D.ArcDirection = mDir
                        .Arc2D.Velocity = newPath.Line3D.Velocity
                        mDisDx = (.Arc2D.EndPosX - .Arc2D.CenterPosX)
                        mDisDy = (.Arc2D.EndPosY - .Arc2D.CenterPosY)
                        mSubDistance = CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)) * Math.PI
                        'Call EstimateMaxVel(velHigh, acc, dec, mSubDistance, newPath.Line3D.Velocity, .Arc2D.Velocity)
                        velHigh = Premtek.CDispensingMath.GetMaxTangentialVelocity(acc, Math.Sqrt((.Arc2D.StartPosX - .Arc2D.CenterPosX) ^ 2 + (.Arc2D.StartPosY - .Arc2D.CenterPosY) ^ 2)) 'Soni + 2017.08.31 取得切線速度限制
                        If gSSystemParameter.MaxCrossDeviceVelocity > 0 And gSSystemParameter.MaxCrossDeviceVelocity < velHigh Then '數值有設定且低於極限值才能用
                            velHigh = gSSystemParameter.MaxCrossDeviceVelocity
                        End If
                        Premtek.CDispensingMath.GetCrossVelocity(velHigh, acc, dec, mSubDistance, gSSystemParameter.CrossVerticalTime, .Arc2D.Velocity) 'Soni Soni + 2017.08.14 推算速度公式修改
                        .Arc2D.IsDispense = False
                        .Line3D.ParameterType = ePathParameterType.Line
                        .Line3D.ParameterName = newPath.Line3D.ParameterName
                        .IsFristPathInDie = False
                    End With

                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                    pathList.Capacity = pathList.Count + 1
                    pathList.Add(mPatternPath)
                    'Debug.Print("Pos: " & mPatternPath.Arc2D.StartPosX & " , " & mPatternPath.Arc2D.StartPosY & " --> " & mPatternPath.Arc2D.EndPosX & " , " & mPatternPath.Arc2D.EndPosY)
                    'Debug.Print("Center " & mPatternPath.Arc2D.CenterPosX & " , " & mPatternPath.Arc2D.CenterPosY)

                    '[Note]:加入原先線段
                    With mPatternPath
                        .PathType = ePathType.Line3D
                        .Line3D.StartPosX = newPath.Line3D.StartPosX
                        .Line3D.StartPosY = newPath.Line3D.StartPosY
                        .Line3D.StartPosZ = newPath.Line3D.StartPosZ
                        .Line3D.EndPosX = newPath.Line3D.EndPosX
                        .Line3D.EndPosY = newPath.Line3D.EndPosY
                        .Line3D.EndPosZ = newPath.Line3D.EndPosZ
                        .Line3D.Velocity = newPath.Line3D.Velocity
                        .Line3D.IsDispense = newPath.Line3D.IsDispense
                        .Line3D.ParameterType = ePathParameterType.Line
                        .Line3D.ParameterName = newPath.Line3D.ParameterName
                        .IsFristPathInDie = False
                    End With

                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                    pathList.Capacity = pathList.Count + 1
                    pathList.Add(mPatternPath)
                    'Debug.Print("Pos: " & mPatternPath.Line3D.StartPosX & " , " & mPatternPath.Line3D.StartPosY & " --> " & mPatternPath.Line3D.EndPosX & " , " & mPatternPath.Line3D.EndPosY)
                    Return True
                Else
                    '[Note]:無解(異常)
                    '[Note]:直接加了
                    With mPatternPath
                        .PathType = ePathType.Line3D
                        .Line3D.StartPosX = newPath.Line3D.StartPosX
                        .Line3D.StartPosY = newPath.Line3D.StartPosY
                        .Line3D.StartPosZ = newPath.Line3D.StartPosZ
                        .Line3D.EndPosX = newPath.Line3D.EndPosX
                        .Line3D.EndPosY = newPath.Line3D.EndPosY
                        .Line3D.EndPosZ = newPath.Line3D.EndPosZ
                        .Line3D.Velocity = newPath.Line3D.Velocity
                        .Line3D.IsDispense = newPath.Line3D.IsDispense
                        .Line3D.ParameterType = ePathParameterType.Line
                        .Line3D.ParameterName = newPath.Line3D.ParameterName
                        .IsFristPathInDie = False
                    End With
                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                    pathList.Capacity = pathList.Count + 1
                    pathList.Add(mPatternPath)
                    'Debug.Print("Pos: " & mPatternPath.Line3D.StartPosX & " , " & mPatternPath.Line3D.StartPosY & " --> " & mPatternPath.Line3D.EndPosX & " , " & mPatternPath.Line3D.EndPosY)
                    Return False
                End If
            End If
        Else
            '[Note]:無解(異常)
            '[Note]:直接加了
            With mPatternPath
                .PathType = ePathType.Line3D
                .Line3D.StartPosX = newPath.Line3D.StartPosX
                .Line3D.StartPosY = newPath.Line3D.StartPosY
                .Line3D.StartPosZ = newPath.Line3D.StartPosZ
                .Line3D.EndPosX = newPath.Line3D.EndPosX
                .Line3D.EndPosY = newPath.Line3D.EndPosY
                .Line3D.EndPosZ = newPath.Line3D.EndPosZ
                .Line3D.Velocity = newPath.Line3D.Velocity
                .Line3D.IsDispense = newPath.Line3D.IsDispense
                .Line3D.ParameterType = ePathParameterType.Line
                .Line3D.ParameterName = newPath.Line3D.ParameterName
                .IsFristPathInDie = False
            End With
            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
            pathList.Capacity = pathList.Count + 1
            pathList.Add(mPatternPath)
            'Debug.Print("Pos: " & mPatternPath.Line3D.StartPosX & " , " & mPatternPath.Line3D.StartPosY & " --> " & mPatternPath.Line3D.EndPosX & " , " & mPatternPath.Line3D.EndPosY)
            Return False
        End If

    End Function


    ''' <summary>[判斷最近的二筆的Path是否為平行(只有Dot、Line的組合)]</summary>
    ''' <param name="LastPath"></param>
    ''' <param name="NewPath"></param>
    ''' <param name="NextPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EstimatePathNoArcRelationship(ByVal acc As Decimal, ByVal LastPath As sPatternPath, ByVal NewPath As sPatternPath, Optional NextPath As sPatternPath = Nothing) As TwoPathRelationship


        Dim mUV As Decimal          '[U。V]
        Dim mdUV As Decimal         '[||U|| ||V||]
        Dim mWV As Decimal          '[W。V]
        Dim mdWV As Decimal         '[||W|| ||V||]
        Dim mYV As Decimal          '[Y。V]
        Dim mdYV As Decimal         '[||Y|| ||V||]


        Dim mU1 As Decimal          '[U=(u1,u2)]  LastPath
        Dim mU2 As Decimal
        Dim mV1 As Decimal          '[V=(v1,v2)]  NowPath  
        Dim mV2 As Decimal
        Dim mW1 As Decimal          '[W=(w1,w2)] LastPath.End-->NowPath.Start
        Dim mW2 As Decimal
        Dim mY1 As Decimal          '[Y=(y1,y2)] LastPath.End(ExtendOff)-->NowPath.Start
        Dim mY2 As Decimal

        Dim mdU As Decimal          '[||U||]
        Dim mdV As Decimal          '[||V||]
        Dim mdW As Decimal          '[||W||]
        Dim mdY As Decimal          '[||Y||]
        Dim mTolerance As Decimal   '[容許誤差]

        Dim mW1V2 As Decimal
        Dim mW2V1 As Decimal
        Dim mY1V2 As Decimal
        Dim mY2V1 As Decimal

        Dim dx As Decimal
        Dim dy As Decimal


        Dim mVelocityDiffDistance As Decimal
        Dim mDistance As Decimal


        Select Case LastPath.PathType
            Case ePathType.Line3D
                Select Case NewPath.PathType
                    Case ePathType.Line3D
                        With LastPath.Line3D
                            mU1 = .EndPosX - .StartPosX
                            mU2 = .EndPosY - .StartPosY
                        End With
                        mdU = CDec(Sqrt(mU1 * mU1 + mU2 * mU2))

                        With NewPath.Line3D
                            mV1 = .EndPosX - .StartPosX
                            mV2 = .EndPosY - .StartPosY
                        End With
                        mdV = CDec(Sqrt(mV1 * mV1 + mV2 * mV2))

                        mW1 = NewPath.Line3D.StartPosX - LastPath.Line3D.EndPosX
                        mW2 = NewPath.Line3D.StartPosY - LastPath.Line3D.EndPosY
                        mdW = CDec(Sqrt(mW1 * mW1 + mW2 * mW2))

                        mY1 = NewPath.Line3D.StartPosX - (LastPath.Line3D.EndPosX - LastPath.Line3D.ExtendOffsetX)
                        mY2 = NewPath.Line3D.StartPosY - (LastPath.Line3D.EndPosY - LastPath.Line3D.ExtendOffsetY)
                        mdY = CDec(Sqrt(mY1 * mY1 + mY2 * mY2))



                        mUV = (mU1 * mV1) + (mU2 * mV2)
                        mdUV = mdU * mdV

                        mWV = (mW1 * mV1) + (mW2 * mV2)
                        mdWV = mdW * mdV

                        mYV = (mY1 * mV1) + (mY2 * mV2)
                        mdYV = mdY * mdV



                        mW1V2 = mW1 * mV2
                        mW2V1 = mW2 * mV1

                        mY1V2 = mY1 * mV2
                        mY2V1 = mY2 * mV1

                        mVelocityDiffDistance = Math.Abs((LastPath.Line3D.Velocity * LastPath.Line3D.Velocity) - (NewPath.Line3D.Velocity * NewPath.Line3D.Velocity)) / (2 * acc)

                        dx = NewPath.Line3D.StartPosX - LastPath.Line3D.EndPosX
                        dy = NewPath.Line3D.StartPosY - LastPath.Line3D.EndPosY

                        mDistance = Math.Sqrt((dx * dx) + (dy * dy))

                        '[Note]:容許誤差就抓1%
                        mTolerance = mdUV * gSSystemParameter.MotionTolerance

                        If Math.Abs(mUV - mdUV) < mTolerance Then
                            '[Note]:mUV = mdUV
                            '[Note]:二條線為平行&同方向
                            If Math.Abs(mW1V2 - mW2V1) < mTolerance Then
                                '[Note]:mU1V2 = mU2V1
                                '[Note]:共線
                                If Math.Abs(mWV - mdWV) < mTolerance Then
                                    '[Note]:行進路徑為同方向
                                    Return TwoPathRelationship.LineAndDirectionIsTheSame
                                Else
                                    If Math.Abs(mWV + mdWV) < mTolerance Then
                                        '[Note]:行進路徑為反方向

                                        '[Note]:若行進路徑為同方且若變速的距離大於實際所需的距離(這是特例喔)
                                        If Math.Abs(mYV - mdYV) < mTolerance Then
                                            '[Note]:若變速的距離小於實際所需的距離
                                            If mVelocityDiffDistance <= mDistance Then
                                                Return TwoPathRelationship.LineAndDirectionIsTheSame
                                            End If
                                        End If

                                        Return TwoPathRelationship.LineIsTheSameButDirectionIsNot

                                    Else
                                        '[Note]:基本上不可能，預到再來看
                                        Return TwoPathRelationship.None
                                    End If
                                End If
                            Else
                                Return TwoPathRelationship.ParallelAndDirectionIsSame
                            End If
                        Else
                            If Math.Abs(mUV + mdUV) < mTolerance Then
                                '[Note]:mUV = -mdUV
                                '[Note]:二條線為平行&反方向
                                If Math.Abs(mW1V2 - mW2V1) < mTolerance Then
                                    '[Note]:mU1V2 = mU2V1
                                    Return TwoPathRelationship.LineIsTheSameButDirectionIsNot
                                Else
                                    Return TwoPathRelationship.ParallelAndDirectionIsNonSame
                                End If
                            Else
                                Return TwoPathRelationship.None
                            End If
                        End If

                    Case ePathType.Dot3D
                        '[Note]:線&點的比對(須判斷是否在同一條直線上[在增補前後二段距離之前])
                        With LastPath.Line3D
                            mU1 = .EndPosX - .StartPosX
                            mU2 = .EndPosY - .StartPosY
                        End With
                        mdU = CDec(Sqrt(mU1 * mU1 + mU2 * mU2))

                        '[去掉延伸出來的那一段]
                        mV1 = NewPath.Dot3D.PosX - (LastPath.Line3D.EndPosX - LastPath.Line3D.ExtendOffsetX)
                        mV2 = NewPath.Dot3D.PosY - (LastPath.Line3D.EndPosY - LastPath.Line3D.ExtendOffsetY)
                        mdV = CDec(Sqrt(mV1 * mV1 + mV2 * mV2))

                        mW1 = NewPath.Dot3D.PosX - LastPath.Line3D.EndPosX
                        mW2 = NewPath.Dot3D.PosY - LastPath.Line3D.EndPosY
                        mdW = CDec(Sqrt(mW1 * mW1 + mW2 * mW2))


                        mUV = (mU1 * mV1) + (mU2 * mV2)
                        mdUV = mdU * mdV

                        mWV = (mW1 * mV1) + (mW2 * mV2)
                        mdWV = mdW * mdV

                        mW1V2 = mW1 * mV2
                        mW2V1 = mW2 * mV1

                        '[Note]:容許誤差就抓1%
                        mTolerance = mdUV * gSSystemParameter.MotionTolerance

                        If Math.Abs(mUV - mdUV) < mTolerance Then
                            '[Note]:mUV = mdUV
                            '[Note]:二條線為平行&同方向
                            If Math.Abs(mW1V2 - mW2V1) < mTolerance Then
                                '[Note]:mU1V2 = mU2V1
                                '[Note]:共線
                                If Math.Abs(mWV - mdWV) < mTolerance Then
                                    '[Note]:行進路徑為同方向
                                    Return TwoPathRelationship.LineAndDirectionIsTheSame
                                Else
                                    If Math.Abs(mWV + mdWV) < mTolerance Then
                                        '[Note]:行進路徑為反方向
                                        Return TwoPathRelationship.LineIsTheSameButDirectionIsNot
                                    Else
                                        '[Note]:基本上不可能，預到再來看
                                        Return TwoPathRelationship.None
                                    End If
                                End If
                            End If
                        Else
                            If Math.Abs(mUV + mdUV) < mTolerance Then
                                '[Note]:mUV = -mdUV
                                '[Note]:二條線為平行&反方向
                                If Math.Abs(mW1V2 - mW2V1) < mTolerance Then
                                    '[Note]:mU1V2 = mU2V1
                                    '[Note]:共線
                                    Return TwoPathRelationship.LineIsTheSameButDirectionIsNot
                                End If
                            End If
                        End If

                        '[Note]:再做一次判斷，加入下一條路徑的資訊進來，此次主要是判斷是否為(ParallelAndDirectionIsNonSame)
                        If IsNothing(NextPath) = True Then
                            '[Note]:沒有下一條線可以協助判斷
                            Return TwoPathRelationship.None
                        Else
                            Select Case NextPath.PathType
                                Case ePathType.Dot3D
                                    mV1 = NextPath.Dot3D.PosX - NewPath.Dot3D.PosX
                                    mV2 = NextPath.Dot3D.PosY - NewPath.Dot3D.PosY

                                Case ePathType.Line3D
                                    mV1 = NextPath.Line3D.StartPosX - NewPath.Dot3D.PosX
                                    mV2 = NextPath.Line3D.StartPosY - NewPath.Dot3D.PosY

                                Case ePathType.Arc2D
                                    '[Note]:在路線規劃上需視為不同線段
                                    Return TwoPathRelationship.None

                                Case ePathType.Arc3D
                                    '[Note]:在路線規劃上需視為不同線段
                                    Return TwoPathRelationship.None

                            End Select
                        End If


                        With LastPath.Line3D
                            mU1 = .EndPosX - .StartPosX
                            mU2 = .EndPosY - .StartPosY
                        End With
                        mdU = CDec(Sqrt(mU1 * mU1 + mU2 * mU2))

                        mdV = CDec(Sqrt(mV1 * mV1 + mV2 * mV2))

                        mW1 = NewPath.Dot3D.PosX - LastPath.Line3D.EndPosX
                        mW2 = NewPath.Dot3D.PosY - LastPath.Line3D.EndPosY
                        mdW = CDec(Sqrt(mW1 * mW1 + mW2 * mW2))


                        mUV = (mU1 * mV1) + (mU2 * mV2)
                        mdUV = mdU * mdV

                        mWV = (mW1 * mV1) + (mW2 * mV2)
                        mdWV = mdW * mdV

                        mW1V2 = mW1 * mV2
                        mW2V1 = mW2 * mV1

                        '[Note]:容許誤差就抓1%
                        mTolerance = mdUV * gSSystemParameter.MotionTolerance

                        '[Note]:判斷是否為 ParallelAndDirectionIsNonSame
                        '[Note]:只要非ParallelAndDirectionIsNonSame，則在路線規劃上需視為不同線段

                        If Math.Abs(mUV - mdUV) < mTolerance Then
                            '[Note]:mUV = mdUV
                            '[Note]:二條線為平行&同方向
                            Return TwoPathRelationship.None
                        Else
                            If Math.Abs(mUV + mdUV) < mTolerance Then
                                '[Note]:mUV = -mdUV
                                '[Note]:二條線為平行&反方向
                                If Math.Abs(mW1V2 - mW2V1) < mTolerance Then
                                    '[Note]:mU1V2 = mU2V1
                                    '[Note]:共線，雖然共線但在路線規劃上需視為不同線段
                                    Return TwoPathRelationship.None
                                Else
                                    Return TwoPathRelationship.ParallelAndDirectionIsNonSame
                                End If
                            Else
                                Return TwoPathRelationship.None
                            End If
                        End If

                    Case Else
                        Return TwoPathRelationship.None

                End Select

            Case ePathType.Dot3D
                Select Case NewPath.PathType
                    Case ePathType.Line3D
                        '[Note]:點&線的比對(須判斷是否在同一條直線上[在增補前後二段距離之前])
                        '[Note]:去掉延伸出來的那一段
                        mU1 = NewPath.Line3D.StartPosX - (LastPath.Dot3D.PosX - LastPath.Dot3D.ExtendOffsetX)
                        mU2 = NewPath.Line3D.StartPosY - (LastPath.Dot3D.PosY - LastPath.Dot3D.ExtendOffsetY)
                        mdU = CDec(Sqrt(mU1 * mU1 + mU2 * mU2))

                        With NewPath.Line3D
                            mV1 = .EndPosX - .StartPosX
                            mV2 = .EndPosY - .StartPosY
                        End With
                        mdV = CDec(Sqrt(mV1 * mV1 + mV2 * mV2))

                        mW1 = NewPath.Line3D.StartPosX - (LastPath.Dot3D.PosX - LastPath.Dot3D.ExtendOffsetX)
                        mW2 = NewPath.Line3D.StartPosY - (LastPath.Dot3D.PosY - LastPath.Dot3D.ExtendOffsetY)
                        mdW = CDec(Sqrt(mW1 * mW1 + mW2 * mW2))

                        mUV = (mU1 * mV1) + (mU2 * mV2)
                        mdUV = mdU * mdV

                        mWV = (mW1 * mV1) + (mW2 * mV2)
                        mdWV = mdW * mdV

                        mW1V2 = mW1 * mV2
                        mW2V1 = mW2 * mV1

                        '[Note]:容許誤差就抓1%
                        mTolerance = mdUV * gSSystemParameter.MotionTolerance

                        If Math.Abs(mUV - mdUV) < mTolerance Then
                            '[Note]:mUV = mdUV
                            '[Note]:二條線為平行&同方向
                            If Math.Abs(mW1V2 - mW2V1) < mTolerance Then
                                '[Note]:mU1V2 = mU2V1
                                '[Note]:共線
                                If Math.Abs(mWV - mdWV) < mTolerance Then
                                    '[Note]:行進路徑為同方向
                                    Return TwoPathRelationship.LineAndDirectionIsTheSame
                                Else
                                    If Math.Abs(mWV + mdWV) < mTolerance Then
                                        '[Note]:行進路徑為反方向
                                        Return TwoPathRelationship.LineIsTheSameButDirectionIsNot
                                    Else
                                        '[Note]:基本上不可能，預到再來看
                                        Return TwoPathRelationship.None
                                    End If
                                End If
                            Else
                                '[Note]:只要是非(共線)則在路線規劃上需視為不同線段
                                Return TwoPathRelationship.None
                            End If
                        Else
                            If Math.Abs(mUV + mdUV) < mTolerance Then
                                '[Note]:mUV = -mdUV
                                '[Note]:二條線為平行&反方向
                                If Math.Abs(mW1V2 - mW2V1) < mTolerance Then
                                    '[Note]:mU1V2 = mU2V1
                                    '[Note]:共線
                                    Return TwoPathRelationship.LineIsTheSameButDirectionIsNot
                                Else
                                    '[Note]:只要是非(共線)則在路線規劃上需視為不同線段
                                    Return TwoPathRelationship.None
                                End If
                            Else
                                '[Note]:只要是非(平行&共線)則在路線規劃上需視為不同線段
                                Return TwoPathRelationship.None
                            End If
                        End If

                    Case Else
                        Return TwoPathRelationship.None

                End Select

            Case Else
                Return TwoPathRelationship.None

        End Select

    End Function


    ''' <summary>[求最佳圓弧之切點]</summary>
    ''' <param name="leadAngle"></param>
    ''' <param name="circlePos">[圓上的點]</param>
    ''' <param name="inCenterPos">[圓心]</param>
    ''' <param name="posNo1"></param>
    ''' <param name="posNo2"></param>
    ''' <param name="outCenterPos">[求出來的圓心]</param>
    ''' <param name="noodlePos">[求出來的切點]</param>
    ''' <param name="dir"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FindArcNoodle(ByVal leadAngle As sLeadAngle, ByVal circlePos As Premtek.sPos, ByVal inCenterPos As Premtek.sPos, ByVal posNo1 As Premtek.sPos, ByVal posNo2 As Premtek.sPos, ByRef outCenterPos As Premtek.sPos, ByRef noodlePos As Premtek.sPos, ByRef dir As eArcDirection) As Boolean

        Dim mD1 As Decimal
        Dim mD2 As Decimal
        Dim mD3 As Decimal
        Dim mIsNoodleNo1 As Boolean         '[判斷mFindCenterNo1、posNo1 同一側]
        Dim mIsNoodleNo2 As Boolean         '[判斷mFindCenterNo2、posNo1 同一側]
        Dim mCos1 As Decimal
        Dim mSin1 As Decimal
        Dim mCos2 As Decimal
        Dim mSin2 As Decimal
        Dim mCos3 As Decimal
        Dim mSin3 As Decimal
        Dim mFindCenterNo1 As Premtek.sPos
        Dim mFindCenterNo2 As Premtek.sPos
        Dim mA As Decimal                   '[mCY=mAX+mB]
        Dim mB As Decimal
        Dim mC As Decimal
        Dim mStep1ArcDir As eArcDirection
        Dim mStep2ArcDir As eArcDirection
        Dim mFail As Boolean

        mFail = False
        'Step1:先判斷使用哪一測的圓心與(順  or 逆)
        mSin1 = SinTrigonometricFunction(90)       '<--逆時鐘
        mCos1 = CosTrigonometricFunction(90)
        mSin2 = SinTrigonometricFunction(-90)  '<--順時鐘
        mCos2 = CosTrigonometricFunction(-90)

        mFindCenterNo1.PosX = mCos1 * (circlePos.PosX - inCenterPos.PosX) - mSin1 * (circlePos.PosY - inCenterPos.PosY) + inCenterPos.PosX
        mFindCenterNo1.PosY = mSin1 * (circlePos.PosX - inCenterPos.PosX) + mCos1 * (circlePos.PosY - inCenterPos.PosY) + inCenterPos.PosY
        mFindCenterNo2.PosX = mCos2 * (circlePos.PosX - inCenterPos.PosX) - mSin2 * (circlePos.PosY - inCenterPos.PosY) + inCenterPos.PosX
        mFindCenterNo2.PosY = mSin2 * (circlePos.PosX - inCenterPos.PosX) + mCos2 * (circlePos.PosY - inCenterPos.PosY) + inCenterPos.PosY

        '[Note]:CY=AX+B
        '[Note]:posNo2、circlePos為一條線
        If circlePos.PosX <> posNo2.PosX And circlePos.PosY <> posNo2.PosY Then
            mC = 1
            mA = (circlePos.PosY - posNo2.PosY) / (circlePos.PosX - posNo2.PosX)
            mB = (posNo2.PosY * circlePos.PosX - circlePos.PosY * posNo2.PosX) / (circlePos.PosX - posNo2.PosX)
        Else
            If circlePos.PosX = posNo2.PosX Then
                mC = 0
                mA = 1
                mB = -circlePos.PosX
            End If

            If circlePos.PosY = posNo2.PosY Then
                mC = 1
                mA = 0
                mB = circlePos.PosY
            End If
        End If

        '[Note]:CY=AX+B
        '       判斷(mFindCenterNo1)(posNo1)是否在同一側
        '       判斷(mFindCenterNo2)(posNo1)是否在同一側
        mD1 = (mC * mFindCenterNo1.PosY) - (mA * mFindCenterNo1.PosX) - mB
        mD2 = (mC * posNo1.PosY) - (mA * posNo1.PosX) - mB
        mD3 = (mC * mFindCenterNo2.PosY) - (mA * mFindCenterNo2.PosX) - mB

        '[Note]:判斷是否為同一側
        If mD1 * mD2 > 0 Then
            '[Note]:mFindCenterNo1、posNo1 同一側
            mIsNoodleNo1 = True
        Else
            mIsNoodleNo1 = False
        End If

        If mD2 * mD3 > 0 Then
            '[Note]:mFindCenterNo2、posNo1 同一側
            mIsNoodleNo2 = True
        Else
            mIsNoodleNo2 = False
        End If


        '[Note]:若mD2=0，表示上一個圓弧的切線方向與目前切線方向是相同的，表示隨便找一個切就可以了
        If mD2 = 0 Then
            mIsNoodleNo1 = True
            mIsNoodleNo2 = False
        End If

        If mIsNoodleNo1 = True Then
            If mIsNoodleNo2 = True Then
                '[Note]:不太正常
                outCenterPos.PosX = mFindCenterNo1.PosX
                outCenterPos.PosY = mFindCenterNo1.PosY
                mStep1ArcDir = eArcDirection.CCW
                mFail = True
            Else
                outCenterPos.PosX = mFindCenterNo1.PosX
                outCenterPos.PosY = mFindCenterNo1.PosY
                mStep1ArcDir = eArcDirection.CCW
            End If
        Else
            If mIsNoodleNo2 = True Then
                outCenterPos.PosX = mFindCenterNo2.PosX
                outCenterPos.PosY = mFindCenterNo2.PosY
                mStep1ArcDir = eArcDirection.CW
            Else
                '[Note]:不太正常
                outCenterPos.PosX = circlePos.PosX
                outCenterPos.PosY = circlePos.PosY
                mStep1ArcDir = eArcDirection.CCW
                mFail = True
            End If
        End If

        'Step2:藉由Step1算出預想的圓弧導角之中心點與方向，藉此推算出切點位置與方向
        Select Case mStep1ArcDir
            Case eArcDirection.CCW
                mSin3 = SinTrigonometricFunction(-1 * leadAngle.Degress)
                mCos3 = CosTrigonometricFunction(-1 * leadAngle.Degress)
                mStep2ArcDir = eArcDirection.CCW
            Case eArcDirection.CW
                mSin3 = SinTrigonometricFunction(leadAngle.Degress)
                mCos3 = CosTrigonometricFunction(leadAngle.Degress)
                mStep2ArcDir = eArcDirection.CW
        End Select

        noodlePos.PosX = mCos3 * (inCenterPos.PosX - outCenterPos.PosX) - mSin3 * (inCenterPos.PosY - outCenterPos.PosY) + outCenterPos.PosX
        noodlePos.PosY = mSin3 * (inCenterPos.PosX - outCenterPos.PosX) + mCos3 * (inCenterPos.PosY - outCenterPos.PosY) + outCenterPos.PosY

        dir = mStep2ArcDir


        If mFail = True Then
            Return False
        Else
            Return True
        End If


    End Function



    ''' <summary>[軸卡路徑編輯]</summary>
    ''' <param name="FixParam"></param>
    ''' <param name="FixPathRegister"></param>
    ''' <param name="motionDispPathRegister"></param>
    ''' <param name="maxExtendDistance"></param>
    ''' <param name="maxBlendTime"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditMotionPath(ByVal FixParam As sCCDFixParam, ByVal FixPathRegister As List(Of sPatternPath), ByRef motionDispPathRegister As List(Of sPatternPath), ByRef motionPath As sPatternPath, ByRef maxExtendDistance As Decimal, ByRef maxBlendTime As Decimal, Optional ByVal updateTriggerDispPathRegister As Boolean = True) As Boolean

        Dim mRunUp As sExtendParam                                  '[助跑資料]
        Dim mCrossRunUp As sExtendParam                             '[跨元件助跑資料]
        Dim mLastPatternPath As New sPatternPath                    '[上一筆路徑資訊]
        Dim mArcExtendLinePath As New sPatternPath                  '[上一筆路徑資訊(由Arc延伸出來的線段)]
        Dim mIs1stMotionPath As Boolean                             '[為第一筆點膠資訊]
        Dim mI As Integer
        Dim mdx As Decimal
        Dim mdy As Decimal
        Dim mr As Decimal
        Dim mMotionDispPathRegister As List(Of sPatternPath)        '[motion]
        Dim mRegisterPath As sPatternPath
        Dim mExtendOffset As Premtek.sPos
        Dim mCrossExtendOffset As Premtek.sPos
        Dim mLastPos As Premtek.sPos
        Dim mMotionPath As New sPatternPath
        'Dim mTriggerPath As New sPatternPath
        Dim mMemoryPatternPath As sPatternPath
        Dim mMaxExtendDistance As Decimal
        Dim mMaxBlendTime As Decimal
        Dim mIs1stTriggerCommand As Boolean


        Dim mArcOffset As Premtek.sPos
        Dim mCenterPos As Premtek.sPos
        Dim mCirclePos As Premtek.sPos
        Dim mDisExtend As Premtek.sPos
        Dim mDisDx As Decimal
        Dim mDisDy As Decimal
        Dim mDistance As Decimal
        Dim mRunUpDisance As Decimal
        Dim mRunDownDisance As Decimal

        Dim mAidPosNo1 As Premtek.sPos
        Dim mAidPosNo2 As Premtek.sPos
        Dim mAddCenterPos As Premtek.sPos
        Dim mNoodlePos As Premtek.sPos
        Dim mArcDir As eArcDirection

        '[Note]:重新路徑規劃
        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
        mMotionDispPathRegister = New List(Of sPatternPath)
        mMotionDispPathRegister.Clear()

        mIs1stMotionPath = True
        mMaxExtendDistance = 0
        mMaxBlendTime = 0
        mIs1stTriggerCommand = True

        If FixPathRegister.Count > 0 Then
            For mI = 0 To FixPathRegister.Count - 1
                mRegisterPath = FixPathRegister.Item(mI)
                If Is1stPathInDie(mIs1stMotionPath, mRegisterPath) Then
                    '[Note]:第一筆需移動的Path
                    '       第一筆資料須與助跑做結合
                    Select Case mRegisterPath.PathType
                        Case ePathType.Line3D
                            '[Note]:算出需助跑的資料
                            Call DetermineRunUpTimeDistance(FixParam.Acc, FixPathRegister.Item(mI), mRunUp)
                            mCrossRunUp = mRunUp
                            '[Note]:起始點位置不用再向外擴
                            If mRunUp.Distance > mMaxExtendDistance Then
                                mMaxExtendDistance = mRunUp.Distance
                            End If
                            If mRunUp.Time > mMaxBlendTime Then
                                mMaxBlendTime = mRunUp.Time
                            End If

                            With mRegisterPath.Line3D
                                mdx = .EndPosX - .StartPosX
                                mdy = .EndPosY - .StartPosY
                                mr = CDec(Sqrt(mdx * mdx + mdy * mdy))
                                If mr = 0 Then
                                    mExtendOffset.PosX = 0
                                    mExtendOffset.PosY = 0
                                    mCrossExtendOffset.PosX = 0
                                    mCrossExtendOffset.PosY = 0
                                Else
                                    mExtendOffset.PosX = mRunUp.Distance * mdx / mr
                                    mExtendOffset.PosY = mRunUp.Distance * mdy / mr
                                    mCrossExtendOffset.PosX = mCrossRunUp.Distance * mdx / mr
                                    mCrossExtendOffset.PosY = mCrossRunUp.Distance * mdy / mr
                                End If
                            End With

                            mMotionPath.PathType = ePathType.Line3D
                            mMotionPath.Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mCrossExtendOffset.PosX
                            mMotionPath.Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mCrossExtendOffset.PosY
                            mMotionPath.Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ

                            If IsLastNonExtendPath(mI, FixPathRegister.Count, mRegisterPath) = True Then
                                mMotionPath.Line3D.EndPosX = mRegisterPath.Line3D.EndPosX
                                mMotionPath.Line3D.EndPosY = mRegisterPath.Line3D.EndPosY
                            Else
                                mMotionPath.Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                mMotionPath.Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                            End If
                            mMotionPath.Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                            mMotionPath.Line3D.ExtendOffsetX = mExtendOffset.PosX
                            mMotionPath.Line3D.ExtendOffsetY = mExtendOffset.PosY
                            mMotionPath.Line3D.Velocity = mRunUp.Velocity
                            mMotionPath.Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                            mMotionPath.Line3D.ParameterType = ePathParameterType.Line
                            mMotionPath.Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                            mMotionPath.IsFristPathInDie = False
                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                            mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                            mMotionDispPathRegister.Add(mMotionPath)
                            'Debug.Print("1st: " & mPath.Line3D.StartPosX & " , " & mPath.Line3D.StartPosY & " --> " & mPath.Line3D.EndPosX & " , " & mPath.Line3D.EndPosY)
                            mLastPos.PosX = mMotionPath.Line3D.EndPosX
                            mLastPos.PosY = mMotionPath.Line3D.EndPosY

                            '[Note]:紀錄最近一筆的路徑資料
                            With mLastPatternPath
                                .PathType = ePathType.Line3D
                                .Line3D.StartPosX = mMotionPath.Line3D.StartPosX
                                .Line3D.StartPosY = mMotionPath.Line3D.StartPosY
                                .Line3D.StartPosZ = mMotionPath.Line3D.StartPosZ
                                .Line3D.EndPosX = mMotionPath.Line3D.EndPosX
                                .Line3D.EndPosY = mMotionPath.Line3D.EndPosY
                                .Line3D.EndPosZ = mMotionPath.Line3D.EndPosZ
                                .Line3D.ExtendOffsetX = mMotionPath.Line3D.ExtendOffsetX
                                .Line3D.ExtendOffsetY = mMotionPath.Line3D.ExtendOffsetY
                                .Line3D.Velocity = mMotionPath.Line3D.Velocity
                            End With

                        Case ePathType.Dot3D
                            '[Note]:算出需助跑的資料
                            Call DetermineRunUpTimeDistance(FixParam.Acc, FixPathRegister.Item(mI), mRunUp)
                            '[Note]:起始點位置不用再向外擴
                            If mRunUp.Distance > mMaxExtendDistance Then
                                mMaxExtendDistance = mRunUp.Distance
                            End If
                            If mRunUp.Time > mMaxBlendTime Then
                                mMaxBlendTime = mRunUp.Time
                            End If

                            With mMotionPath
                                .PathType = ePathType.Dot3D
                                .Dot3D.PosX = mRegisterPath.Dot3D.PosX
                                .Dot3D.PosY = mRegisterPath.Dot3D.PosY
                                .Dot3D.PosZ = mRegisterPath.Dot3D.PosZ
                                .Dot3D.ExtendOffsetX = 0
                                .Dot3D.ExtendOffsetY = 0
                                .Dot3D.Velocity = mRunUp.Velocity
                                .Dot3D.IsDispense = mRegisterPath.Dot3D.IsDispense
                                .IsFristPathInDie = False
                                .Dot3D.ParameterType = ePathParameterType.Dot
                                .Dot3D.ParameterName = mRegisterPath.Dot3D.ParameterName
                            End With
                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                            mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                            mMotionDispPathRegister.Add(mMotionPath)

                        Case ePathType.Wait
                            '[Note]:不可能進來
                    End Select
                    mIs1stMotionPath = False
                Else
                    '[Note]:第二~n筆移動式的Path資料(且不是下一顆的第一個路徑)
                    Select Case TwoPathModel(mLastPatternPath, mRegisterPath)
                        Case eTwoPathModel.LineLine
                            '[Note]:算出需助跑的資料
                            If DetermineRunUpTimeDistance(FixParam.Acc, FixPathRegister.Item(mI), mRunUp) = True Then
                                If mRunUp.Distance > mMaxExtendDistance Then
                                    mMaxExtendDistance = mRunUp.Distance
                                End If
                                If mRunUp.Time > mMaxBlendTime Then
                                    mMaxBlendTime = mRunUp.Time
                                End If

                                With mRegisterPath.Line3D
                                    mdx = .EndPosX - .StartPosX
                                    mdy = .EndPosY - .StartPosY
                                    mr = CDec(Sqrt(mdx * mdx + mdy * mdy))
                                    If mr = 0 Then
                                        mExtendOffset.PosX = 0
                                        mExtendOffset.PosY = 0
                                    Else
                                        mExtendOffset.PosX = mRunUp.Distance * mdx / mr
                                        mExtendOffset.PosY = mRunUp.Distance * mdy / mr
                                        mArcOffset.PosX = FixParam.LeadAngle.Distance * mdx / mr
                                        mArcOffset.PosY = FixParam.LeadAngle.Distance * mdy / mr
                                    End If
                                End With

                                '[Note]:先判斷是否為直接串接之路徑
                                If IsUseExtendPath(mLastPatternPath, mRegisterPath) Then
                                    Select Case EstimatePathNoArcRelationship(FixParam.Acc, mLastPatternPath, mRegisterPath)
                                        Case TwoPathRelationship.None, TwoPathRelationship.ParallelAndDirectionIsSame
                                            '[Note]:需要增加圓弧導角
                                            With mRegisterPath.Line3D
                                                mdx = .EndPosX - .StartPosX
                                                mdy = .EndPosY - .StartPosY
                                                mr = CDec(Sqrt(mdx * mdx + mdy * mdy))
                                                If mr = 0 Then
                                                    mExtendOffset.PosX = 0
                                                    mExtendOffset.PosY = 0
                                                Else
                                                    mExtendOffset.PosX = mRunUp.Distance * mdx / mr
                                                    mExtendOffset.PosY = mRunUp.Distance * mdy / mr
                                                    mArcOffset.PosX = FixParam.LeadAngle.Distance * mdx / mr
                                                    mArcOffset.PosY = FixParam.LeadAngle.Distance * mdy / mr
                                                End If
                                            End With

                                            With mMotionPath
                                                .PathType = ePathType.Line3D
                                                .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                .Line3D.Velocity = mRunUp.Velocity
                                                .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                .Line3D.ParameterType = ePathParameterType.Line
                                                .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                .IsFristPathInDie = False
                                            End With

                                            If FixParam.LeadAngle.Degress <> 0 Then
                                                '*************************************************************************************
                                                'Step1:找最佳圓弧導角的圓心
                                                With mMotionPath.Line3D
                                                    mMotionPath.PathType = ePathType.Line3D
                                                    mCirclePos.PosX = .StartPosX
                                                    mCirclePos.PosY = .StartPosY
                                                    mCenterPos.PosX = .StartPosX - mArcOffset.PosX
                                                    mCenterPos.PosY = .StartPosY - mArcOffset.PosY
                                                    mAidPosNo1.PosX = mLastPos.PosX
                                                    mAidPosNo1.PosY = mLastPos.PosY
                                                    mAidPosNo2.PosX = .EndPosX
                                                    mAidPosNo2.PosY = .EndPosY
                                                    Call FindArcNoodle(FixParam.LeadAngle, mCirclePos, mCenterPos, mAidPosNo1, mAidPosNo2, mAddCenterPos, mNoodlePos, mArcDir)
                                                End With

                                                '[Note]:先加圓弧切點
                                                With mMotionPath
                                                    .PathType = ePathType.Dot3D
                                                    .Dot3D.PosX = mNoodlePos.PosX
                                                    .Dot3D.PosY = mNoodlePos.PosY
                                                    .Dot3D.PosZ = mRegisterPath.Line3D.StartPosZ
                                                    .Dot3D.Velocity = mRunUp.Velocity
                                                    .Dot3D.IsDispense = False
                                                    .Dot3D.ParameterType = ePathParameterType.Line
                                                    .Dot3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                    .IsFristPathInDie = False
                                                End With
                                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                mMotionDispPathRegister.Add(mMotionPath)

                                                '[Note]:再加圓弧路徑
                                                With mMotionPath
                                                    .PathType = ePathType.Arc2D
                                                    .Arc2D.StartPosX = mNoodlePos.PosX
                                                    .Arc2D.StartPosY = mNoodlePos.PosY
                                                    .Arc2D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                    .Arc2D.CenterPosX = mAddCenterPos.PosX
                                                    .Arc2D.CenterPosY = mAddCenterPos.PosY
                                                    .Arc2D.CenterPosZ = mRegisterPath.Line3D.StartPosZ
                                                    .Arc2D.EndPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX - mArcOffset.PosX
                                                    .Arc2D.EndPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY - mArcOffset.PosY
                                                    .Arc2D.EndPosZ = mRegisterPath.Line3D.StartPosZ
                                                    .Arc2D.ArcDirection = mArcDir
                                                    .Arc2D.Velocity = mRunUp.Velocity
                                                    Dim velHigh As Decimal
                                                    mDisDx = (.Arc2D.EndPosX - .Arc2D.CenterPosX)
                                                    mDisDy = (.Arc2D.EndPosY - .Arc2D.CenterPosY)
                                                    mDistance = CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)) * Math.PI
                                                    'Call EstimateMaxVel(velHigh, acc, dec, mSubDistance, newPath.Line3D.Velocity, .Arc2D.Velocity)
                                                    velHigh = Premtek.CDispensingMath.GetMaxTangentialVelocity(FixParam.Acc, Math.Sqrt((.Arc2D.StartPosX - .Arc2D.CenterPosX) ^ 2 + (.Arc2D.StartPosY - .Arc2D.CenterPosY) ^ 2)) 'Soni + 2017.08.31 取得切線速度限制
                                                    If gSSystemParameter.MaxCrossDeviceVelocity > 0 And gSSystemParameter.MaxCrossDeviceVelocity < velHigh Then '數值有設定且低於極限值才能用
                                                        velHigh = gSSystemParameter.MaxCrossDeviceVelocity
                                                    End If
                                                    Premtek.CDispensingMath.GetCrossVelocity(velHigh, FixParam.Acc, FixParam.Dec, mDistance, gSSystemParameter.CrossVerticalTime, .Arc2D.Velocity) 'Soni Soni + 2017.08.14 推算速度公式修改
                                                    .Arc2D.IsDispense = False
                                                    .Arc2D.ParameterType = ePathParameterType.Line
                                                    .Arc2D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                    .IsFristPathInDie = False
                                                End With
                                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                mMotionDispPathRegister.Add(mMotionPath)

                                                '[Note]:最後再加入原先的路徑
                                                If IsLastNonExtendPath(mI, FixPathRegister.Count, mRegisterPath) = True Then
                                                    With mMotionPath
                                                        .PathType = ePathType.Line3D
                                                        .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX - mArcOffset.PosX
                                                        .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY - mArcOffset.PosY
                                                        .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                        .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX
                                                        .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY
                                                        .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                        .Line3D.Velocity = mRunUp.Velocity
                                                        .Line3D.ExtendOffsetX = 0
                                                        .Line3D.ExtendOffsetY = 0
                                                        .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                        .Line3D.ParameterType = ePathParameterType.Line
                                                        .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                        .IsFristPathInDie = False
                                                    End With
                                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                    mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                    mMotionDispPathRegister.Add(mMotionPath)
                                                Else
                                                    With mMotionPath
                                                        .PathType = ePathType.Line3D
                                                        .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX - mArcOffset.PosX
                                                        .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY - mArcOffset.PosY
                                                        .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                        .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                        .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                        .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                        .Line3D.Velocity = mRunUp.Velocity
                                                        .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                        .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                        .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                        .Line3D.ParameterType = ePathParameterType.Line
                                                        .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                        .IsFristPathInDie = False
                                                    End With
                                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                    mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                    mMotionDispPathRegister.Add(mMotionPath)

                                                    mLastPos.PosX = mMotionPath.Line3D.EndPosX
                                                    mLastPos.PosY = mMotionPath.Line3D.EndPosY

                                                    '[Note]:紀錄最近一筆的路徑資料
                                                    With mLastPatternPath
                                                        .PathType = ePathType.Line3D
                                                        .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX - mArcOffset.PosX
                                                        .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY - mArcOffset.PosY
                                                        .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                        .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                        .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                        .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                        .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                        .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                        .Line3D.Velocity = mRunUp.Velocity
                                                    End With
                                                End If
                                                '*************************************************************************************
                                            Else
                                                '[Note]:直接加，不額外增加路徑

                                                '[Note]:直接將二個延伸出來的端點接起來
                                                With mMotionPath
                                                    .PathType = ePathType.Line3D
                                                    .Line3D.StartPosX = mLastPos.PosX
                                                    .Line3D.StartPosY = mLastPos.PosY
                                                    .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                    .Line3D.EndPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                    .Line3D.EndPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                    .Line3D.EndPosZ = mRegisterPath.Line3D.StartPosZ
                                                    .Line3D.Velocity = mRunUp.Velocity
                                                    .Line3D.IsDispense = False
                                                    .Line3D.ParameterType = ePathParameterType.Line
                                                    .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                    .IsFristPathInDie = False
                                                End With
                                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                mMotionDispPathRegister.Add(mMotionPath)

                                                If IsLastNonExtendPath(mI, FixPathRegister.Count, mRegisterPath) = True Then
                                                    With mMotionPath
                                                        .PathType = ePathType.Line3D
                                                        .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                        .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                        .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                        .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX
                                                        .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY
                                                        .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                        .Line3D.ExtendOffsetX = 0
                                                        .Line3D.ExtendOffsetY = 0
                                                        .Line3D.Velocity = mRunUp.Velocity
                                                        .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                        .Line3D.ParameterType = ePathParameterType.Line
                                                        .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                        .IsFristPathInDie = False
                                                    End With
                                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                    mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                    mMotionDispPathRegister.Add(mMotionPath)
                                                Else
                                                    With mMotionPath
                                                        .PathType = ePathType.Line3D
                                                        .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                        .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                        .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                        .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                        .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                        .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                        .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                        .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                        .Line3D.Velocity = mRunUp.Velocity
                                                        .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                        .Line3D.ParameterType = ePathParameterType.Line
                                                        .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                        .IsFristPathInDie = False
                                                    End With
                                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                    mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                    mMotionDispPathRegister.Add(mMotionPath)
                                                    mLastPos.PosX = mMotionPath.Line3D.EndPosX
                                                    mLastPos.PosY = mMotionPath.Line3D.EndPosY

                                                    '[Note]:紀錄最近一筆的路徑資料
                                                    With mLastPatternPath
                                                        .PathType = ePathType.Line3D
                                                        .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                        .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                        .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                        .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                        .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                        .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                        .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                        .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                        .Line3D.Velocity = mRunUp.Velocity
                                                    End With
                                                End If
                                            End If

                                        Case TwoPathRelationship.LineAndDirectionIsTheSame
                                            '*****************************************************************************************
                                            If Math.Abs(mLastPatternPath.Line3D.Velocity - mRunUp.Velocity) < 1 Then
                                                '[Note]:若速度相近，則直接把線段作連接，不再作猜解的動作

                                                '[Note]:若是同一條線，則直接修改端點座標
                                                '[Note]:把前一Path的延伸去掉-->再加上本體這一條
                                                mMotionPath = mMotionDispPathRegister.Item(mMotionDispPathRegister.Count - 1)
                                                mMemoryPatternPath = mMotionDispPathRegister.Item(mMotionDispPathRegister.Count - 1)

                                                '[Note]:再把當下一個Path加上去(起點不延伸，端點延伸)
                                                If IsLastNonExtendPath(mI, FixPathRegister.Count, mRegisterPath) = True Then
                                                    '[Note]:只改終點
                                                    With mMotionPath.Line3D
                                                        .EndPosX = mRegisterPath.Line3D.EndPosX
                                                        .EndPosY = mRegisterPath.Line3D.EndPosY
                                                        .EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                        .ExtendOffsetX = 0
                                                        .ExtendOffsetY = 0
                                                        .ParameterType = ePathParameterType.Line
                                                        .ParameterName = mRegisterPath.Line3D.ParameterName
                                                    End With

                                                    '[Note]:先去掉舊的，再把新的加上去
                                                    mMotionDispPathRegister.RemoveAt(mMotionDispPathRegister.Count - 1)
                                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                    mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                    mMotionDispPathRegister.Add(mMotionPath)
                                                Else
                                                    '[Note]:只改終點
                                                    With mMotionPath.Line3D
                                                        .EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                        .EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                        .EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                        .ExtendOffsetX = mExtendOffset.PosX
                                                        .ExtendOffsetY = mExtendOffset.PosY
                                                        .ParameterType = ePathParameterType.Line
                                                        .ParameterName = mRegisterPath.Line3D.ParameterName
                                                    End With
                                                    '[Note]:先去掉舊的，再把新的加上去
                                                    mMotionDispPathRegister.RemoveAt(mMotionDispPathRegister.Count - 1)
                                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                    mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                    mMotionDispPathRegister.Add(mMotionPath)

                                                    mLastPos.PosX = mMotionPath.Line3D.EndPosX
                                                    mLastPos.PosY = mMotionPath.Line3D.EndPosY

                                                    '[Note]:紀錄最近一筆的路徑資料
                                                    With mLastPatternPath
                                                        .PathType = ePathType.Line3D
                                                        .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                        .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                        .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                        .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                        .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                        .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                        .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                        .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                        .Line3D.Velocity = mRunUp.Velocity
                                                    End With
                                                End If
                                            Else

                                                '[Note]:先判斷二條線端點的距離(不含助跑)
                                                '距離若大於(減速段+助跑段)，則安心的直接加上去
                                                '    若小於(減速段+助跑段)，則縮短二條線的助跑段(前一條的剎車段與這一條的加速段距離相同)
                                                '    若為0，去掉上一條的減速段後，再加上當下的這一條並於結束段延伸出去(速度就不理他了)
                                                With mRegisterPath.Line3D
                                                    mdx = .EndPosX - .StartPosX
                                                    mdy = .EndPosY - .StartPosY
                                                    mr = CDec(Sqrt(mdx * mdx + mdy * mdy))
                                                    If mr = 0 Then
                                                        mExtendOffset.PosX = 0
                                                        mExtendOffset.PosY = 0
                                                    Else
                                                        mExtendOffset.PosX = mRunUp.Distance * mdx / mr
                                                        mExtendOffset.PosY = mRunUp.Distance * mdy / mr
                                                    End If
                                                End With

                                                '[Note]:二線段的距離
                                                mDisDx = mRegisterPath.Line3D.StartPosX - (mLastPatternPath.Line3D.EndPosX - mLastPatternPath.Line3D.ExtendOffsetX)
                                                mDisDy = mRegisterPath.Line3D.StartPosY - (mLastPatternPath.Line3D.EndPosY - mLastPatternPath.Line3D.ExtendOffsetY)
                                                mDistance = Math.Sqrt((mDisDx * mDisDx) + (mDisDy * mDisDy))

                                                If mDistance = 0 Then
                                                    '[Note]:若是同一條線，則直接修改端點座標
                                                    '[Note]:把前一Path的延伸去掉-->再加上本體這一條
                                                    mMotionPath = mMotionDispPathRegister.Item(mMotionDispPathRegister.Count - 1)
                                                    mMemoryPatternPath = mMotionDispPathRegister.Item(mMotionDispPathRegister.Count - 1)

                                                    '[Note]:只改終點
                                                    With mMotionPath.Line3D
                                                        .EndPosX = mMemoryPatternPath.Line3D.EndPosX - mMemoryPatternPath.Line3D.ExtendOffsetX
                                                        .EndPosY = mMemoryPatternPath.Line3D.EndPosY - mMemoryPatternPath.Line3D.ExtendOffsetY
                                                    End With
                                                    '[Note]:先去掉舊的，再把新的加上去
                                                    mMotionDispPathRegister.RemoveAt(mMotionDispPathRegister.Count - 1)
                                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                    mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                    mMotionDispPathRegister.Add(mMotionPath)

                                                    '[Note]:再把當下一個Path加上去(起點不延伸，端點延伸)
                                                    If IsLastNonExtendPath(mI, FixPathRegister.Count, mRegisterPath) = True Then
                                                        With mMotionPath
                                                            .PathType = ePathType.Line3D
                                                            .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX
                                                            .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY
                                                            .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                            .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX
                                                            .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY
                                                            .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                            .Line3D.Velocity = mRunUp.Velocity
                                                            .Line3D.ExtendOffsetX = 0
                                                            .Line3D.ExtendOffsetY = 0
                                                            .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                            .Line3D.ParameterType = ePathParameterType.Line
                                                            .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                            .IsFristPathInDie = False
                                                        End With
                                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                        mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                        mMotionDispPathRegister.Add(mMotionPath)
                                                    Else
                                                        With mMotionPath
                                                            .PathType = ePathType.Line3D
                                                            .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX
                                                            .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY
                                                            .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                            .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                            .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                            .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                            .Line3D.Velocity = mRunUp.Velocity
                                                            .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                            .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                            .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                            .Line3D.ParameterType = ePathParameterType.Line
                                                            .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                            .IsFristPathInDie = False
                                                        End With
                                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                        mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                        mMotionDispPathRegister.Add(mMotionPath)
                                                        mLastPos.PosX = mMotionPath.Line3D.EndPosX
                                                        mLastPos.PosY = mMotionPath.Line3D.EndPosY

                                                        '[Note]:紀錄最近一筆的路徑資料
                                                        With mLastPatternPath
                                                            .PathType = ePathType.Line3D
                                                            .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                            .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                            .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                            .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                            .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                            .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                            .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                            .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                            .Line3D.Velocity = mRunUp.Velocity
                                                        End With
                                                    End If
                                                Else
                                                    With mLastPatternPath.Line3D
                                                        mRunDownDisance = Math.Sqrt((.ExtendOffsetX * .ExtendOffsetX) + (.ExtendOffsetY * .ExtendOffsetY))
                                                    End With
                                                    mRunUpDisance = mRunUp.Distance

                                                    If mDistance >= (mRunDownDisance + mRunUpDisance) Then
                                                        '[Note]:'距離若大於(減速段+助跑段)，則安心的直接加上去

                                                        '[Note]:直接將二個延伸出來的端點接起來
                                                        With mMotionPath
                                                            .PathType = ePathType.Line3D
                                                            .Line3D.StartPosX = mLastPos.PosX
                                                            .Line3D.StartPosY = mLastPos.PosY
                                                            .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                            .Line3D.EndPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                            .Line3D.EndPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                            .Line3D.EndPosZ = mRegisterPath.Line3D.StartPosZ
                                                            .Line3D.Velocity = mRunUp.Velocity
                                                            .Line3D.IsDispense = False
                                                            .Line3D.ParameterType = ePathParameterType.Line
                                                            .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                            .IsFristPathInDie = False
                                                        End With
                                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                        mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                        mMotionDispPathRegister.Add(mMotionPath)

                                                        If IsLastNonExtendPath(mI, FixPathRegister.Count, mRegisterPath) = True Then
                                                            With mMotionPath
                                                                .PathType = ePathType.Line3D
                                                                .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                                .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                                .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                                .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX
                                                                .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY
                                                                .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                                .Line3D.Velocity = mRunUp.Velocity
                                                                .Line3D.ExtendOffsetX = 0
                                                                .Line3D.ExtendOffsetY = 0
                                                                .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                                .Line3D.ParameterType = ePathParameterType.Line
                                                                .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                                .IsFristPathInDie = False
                                                            End With
                                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                            mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                            mMotionDispPathRegister.Add(mMotionPath)
                                                        Else
                                                            With mMotionPath
                                                                .PathType = ePathType.Line3D
                                                                .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                                .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                                .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                                .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                                .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                                .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                                .Line3D.Velocity = mRunUp.Velocity
                                                                .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                                .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                                .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                                .Line3D.ParameterType = ePathParameterType.Line
                                                                .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                                .IsFristPathInDie = False
                                                            End With
                                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                            mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                            mMotionDispPathRegister.Add(mMotionPath)
                                                            mLastPos.PosX = mMotionPath.Line3D.EndPosX
                                                            mLastPos.PosY = mMotionPath.Line3D.EndPosY

                                                            '[Note]:紀錄最近一筆的路徑資料
                                                            With mLastPatternPath
                                                                .PathType = ePathType.Line3D
                                                                .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                                .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                                .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                                .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                                .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                                .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                                .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                                .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                                .Line3D.Velocity = mRunUp.Velocity
                                                            End With
                                                        End If
                                                    Else
                                                        '[Note]:距離若小於(減速段+助跑段)，則縮短二條線的助跑段(前一條的剎車段與這一條的加速段距離相同)
                                                        mDisExtend.PosX = mDisDx / 2
                                                        mDisExtend.PosY = mDisDy / 2

                                                        '[Note]:把前一Path的延伸縮短-->再加上本體這一條(延伸一樣縮短)
                                                        mMotionPath = mMotionDispPathRegister.Item(mMotionDispPathRegister.Count - 1)
                                                        mMemoryPatternPath = mMotionDispPathRegister.Item(mMotionDispPathRegister.Count - 1)

                                                        '[Note]:只改終點
                                                        With mMotionPath.Line3D
                                                            .EndPosX = mMemoryPatternPath.Line3D.EndPosX - mMemoryPatternPath.Line3D.ExtendOffsetX + mDisExtend.PosX
                                                            .EndPosY = mMemoryPatternPath.Line3D.EndPosY - mMemoryPatternPath.Line3D.ExtendOffsetY + mDisExtend.PosY
                                                        End With
                                                        '[Note]:先去掉舊的，再把新的加上去
                                                        mMotionDispPathRegister.RemoveAt(mMotionDispPathRegister.Count - 1)
                                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                        mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                        mMotionDispPathRegister.Add(mMotionPath)

                                                        '[Note]:直接將二個延伸出來的端點接起來
                                                        With mMotionPath
                                                            .PathType = ePathType.Line3D
                                                            .Line3D.StartPosX = mMotionPath.Line3D.EndPosX
                                                            .Line3D.StartPosY = mMotionPath.Line3D.EndPosY
                                                            .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                            .Line3D.EndPosX = mRegisterPath.Line3D.StartPosX - mDisExtend.PosX
                                                            .Line3D.EndPosY = mRegisterPath.Line3D.StartPosY - mDisExtend.PosY
                                                            .Line3D.EndPosZ = mRegisterPath.Line3D.StartPosZ
                                                            .Line3D.Velocity = mRunUp.Velocity
                                                            .Line3D.IsDispense = False
                                                            .Line3D.ParameterType = ePathParameterType.Line
                                                            .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                            .IsFristPathInDie = False
                                                        End With
                                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                        mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                        mMotionDispPathRegister.Add(mMotionPath)

                                                        '[Note]:再把當下一個Path加上去(起點延伸縮短，端點延伸不變)
                                                        If IsLastNonExtendPath(mI, FixPathRegister.Count, mRegisterPath) = True Then
                                                            With mMotionPath
                                                                .PathType = ePathType.Line3D
                                                                .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mDisExtend.PosX
                                                                .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mDisExtend.PosY
                                                                .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                                .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX
                                                                .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY
                                                                .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                                .Line3D.Velocity = mRunUp.Velocity
                                                                .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                                .Line3D.ExtendOffsetX = 0
                                                                .Line3D.ExtendOffsetY = 0
                                                                .Line3D.ParameterType = ePathParameterType.Line
                                                                .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                                .IsFristPathInDie = False
                                                            End With
                                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                            mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                            mMotionDispPathRegister.Add(mMotionPath)
                                                        Else
                                                            With mMotionPath
                                                                .PathType = ePathType.Line3D
                                                                .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mDisExtend.PosX
                                                                .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mDisExtend.PosY
                                                                .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                                .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                                .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                                .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                                .Line3D.Velocity = mRunUp.Velocity
                                                                .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                                .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                                .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                                .Line3D.ParameterType = ePathParameterType.Line
                                                                .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                                .IsFristPathInDie = False
                                                            End With
                                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                            mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                            mMotionDispPathRegister.Add(mMotionPath)
                                                            mLastPos.PosX = mMotionPath.Line3D.EndPosX
                                                            mLastPos.PosY = mMotionPath.Line3D.EndPosY

                                                            '[Note]:紀錄最近一筆的路徑資料
                                                            With mLastPatternPath
                                                                .PathType = ePathType.Line3D
                                                                .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mDisExtend.PosX
                                                                .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mDisExtend.PosY
                                                                .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                                .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                                .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                                .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                                .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                                .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                                .Line3D.Velocity = mRunUp.Velocity
                                                            End With
                                                        End If
                                                    End If
                                                End If
                                            End If

                                            '*****************************************************************************************

                                        Case TwoPathRelationship.ParallelAndDirectionIsNonSame
                                            '*****************************************************************************************
                                            '[Note]:找最佳路徑(加原先的路徑)
                                            With mRegisterPath.Line3D
                                                mdx = .EndPosX - .StartPosX
                                                mdy = .EndPosY - .StartPosY
                                                mr = CDec(Sqrt(mdx * mdx + mdy * mdy))
                                                If mr = 0 Then
                                                    mExtendOffset.PosX = 0
                                                    mExtendOffset.PosY = 0
                                                Else
                                                    mExtendOffset.PosX = mRunUp.Distance * mdx / mr
                                                    mExtendOffset.PosY = mRunUp.Distance * mdy / mr
                                                End If
                                            End With

                                            If IsLastNonExtendPath(mI, FixPathRegister.Count, mRegisterPath) = True Then
                                                With mMotionPath
                                                    .PathType = ePathType.Line3D
                                                    .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                    .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                    .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                    .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX
                                                    .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY
                                                    .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                    .Line3D.Velocity = mRunUp.Velocity
                                                    .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                    .Line3D.ExtendOffsetX = 0
                                                    .Line3D.ExtendOffsetY = 0
                                                    .Line3D.ParameterType = ePathParameterType.Line
                                                    .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                    .IsFristPathInDie = False
                                                End With
                                            Else
                                                With mMotionPath
                                                    .PathType = ePathType.Line3D
                                                    .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                    .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                    .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                    .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                    .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                    .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                    .Line3D.Velocity = mRunUp.Velocity
                                                    .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                    .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                    .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                    .Line3D.ParameterType = ePathParameterType.Line
                                                    .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                    .IsFristPathInDie = False
                                                End With
                                            End If

                                            Call DetermineExtendWhichLine(FixParam.VelHigh, FixParam.Acc, FixParam.Dec, mLastPatternPath, mMotionPath, mMotionDispPathRegister)
                                            mLastPos.PosX = mMotionPath.Line3D.EndPosX
                                            mLastPos.PosY = mMotionPath.Line3D.EndPosY

                                            '[Note]:紀錄最近一筆的路徑資料
                                            With mLastPatternPath
                                                .PathType = ePathType.Line3D
                                                .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                .Line3D.Velocity = mRunUp.Velocity
                                            End With
                                            '*****************************************************************************************

                                        Case TwoPathRelationship.LineIsTheSameButDirectionIsNot
                                            '[Note]:直接加入即可，風險為可能誤觸Trigger訊號
                                            With mRegisterPath.Line3D
                                                mdx = .EndPosX - .StartPosX
                                                mdy = .EndPosY - .StartPosY
                                                mr = CDec(Sqrt(mdx * mdx + mdy * mdy))
                                                If mr = 0 Then
                                                    mExtendOffset.PosX = 0
                                                    mExtendOffset.PosY = 0
                                                Else
                                                    mExtendOffset.PosX = mRunUp.Distance * mdx / mr
                                                    mExtendOffset.PosY = mRunUp.Distance * mdy / mr
                                                End If
                                            End With

                                            '[Note]:直接將二個延伸出來的端點接起來
                                            With mMotionPath
                                                .PathType = ePathType.Line3D
                                                .Line3D.StartPosX = mLastPos.PosX
                                                .Line3D.StartPosY = mLastPos.PosY
                                                .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                .Line3D.EndPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                .Line3D.EndPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                .Line3D.EndPosZ = mRegisterPath.Line3D.StartPosZ
                                                .Line3D.Velocity = mRunUp.Velocity
                                                .Line3D.IsDispense = False
                                                .Line3D.ParameterType = ePathParameterType.Line
                                                .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                .IsFristPathInDie = False
                                            End With
                                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                            mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                            mMotionDispPathRegister.Add(mMotionPath)

                                            If IsLastNonExtendPath(mI, FixPathRegister.Count, mRegisterPath) = True Then
                                                With mMotionPath
                                                    .PathType = ePathType.Line3D
                                                    .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                    .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                    .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                    .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX
                                                    .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY
                                                    .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                    .Line3D.Velocity = mRunUp.Velocity
                                                    .Line3D.ExtendOffsetX = 0
                                                    .Line3D.ExtendOffsetY = 0
                                                    .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                    .Line3D.ParameterType = ePathParameterType.Line
                                                    .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                    .IsFristPathInDie = False
                                                End With
                                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                mMotionDispPathRegister.Add(mMotionPath)
                                            Else
                                                With mMotionPath
                                                    .PathType = ePathType.Line3D
                                                    .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                    .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                    .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                    .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                    .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                    .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                    .Line3D.Velocity = mRunUp.Velocity
                                                    .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                    .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                    .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                                    .Line3D.ParameterType = ePathParameterType.Line
                                                    .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                                    .IsFristPathInDie = False
                                                End With
                                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                                mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                                mMotionDispPathRegister.Add(mMotionPath)
                                                'Debug.Print("Pos: " & mPath.Line3D.StartPosX & " , " & mPath.Line3D.StartPosY & " --> " & mPath.Line3D.EndPosX & " , " & mPath.Line3D.EndPosY)

                                                mLastPos.PosX = mMotionPath.Line3D.EndPosX
                                                mLastPos.PosY = mMotionPath.Line3D.EndPosY

                                                '[Note]:紀錄最近一筆的路徑資料
                                                With mLastPatternPath
                                                    .PathType = ePathType.Line3D
                                                    .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX - mExtendOffset.PosX
                                                    .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY - mExtendOffset.PosY
                                                    .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                                    .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                                    .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                                    .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                                    .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                                    .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                                    .Line3D.Velocity = mRunUp.Velocity
                                                End With
                                            End If

                                    End Select
                                Else
                                    '[Note]:直接接起來
                                    '[Note]:先修改前一條路徑，把延伸出來的縮回去-->再把新的路徑加進來
                                    mMotionPath = mMotionDispPathRegister.Item(mMotionDispPathRegister.Count - 1)
                                    mMemoryPatternPath = mMotionDispPathRegister.Item(mMotionDispPathRegister.Count - 1)
                                    '[Note]:只改終點
                                    With mMotionPath.Line3D
                                        .EndPosX = mMemoryPatternPath.Line3D.EndPosX - mMemoryPatternPath.Line3D.ExtendOffsetX
                                        .EndPosY = mMemoryPatternPath.Line3D.EndPosY - mMemoryPatternPath.Line3D.ExtendOffsetY
                                    End With
                                    '[Note]:先去掉舊的，再把新的加上去
                                    mMotionDispPathRegister.RemoveAt(mMotionDispPathRegister.Count - 1)
                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                    mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                    mMotionDispPathRegister.Add(mMotionPath)

                                    '[Note]:再把當下一個Path加上去
                                    If IsLastNonExtendPath(mI, FixPathRegister.Count, mRegisterPath) = True Then
                                        With mMotionPath
                                            .PathType = ePathType.Line3D
                                            .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX
                                            .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY
                                            .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                            .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX
                                            .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY
                                            .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                            .Line3D.ExtendOffsetX = 0
                                            .Line3D.ExtendOffsetY = 0
                                            .Line3D.Velocity = mRunUp.Velocity
                                            .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                            .Line3D.ParameterType = ePathParameterType.Line
                                            .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                            .IsFristPathInDie = False
                                        End With
                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                        mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                        mMotionDispPathRegister.Add(mMotionPath)
                                    Else
                                        With mMotionPath
                                            .PathType = ePathType.Line3D
                                            .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX
                                            .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY
                                            .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                            .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                            .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                            .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                            .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                            .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                            .Line3D.Velocity = mRunUp.Velocity
                                            .Line3D.IsDispense = mRegisterPath.Line3D.IsDispense
                                            .Line3D.ParameterType = ePathParameterType.Line
                                            .Line3D.ParameterName = mRegisterPath.Line3D.ParameterName
                                            .IsFristPathInDie = False
                                        End With
                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                        mMotionDispPathRegister.Capacity = mMotionDispPathRegister.Count + 1
                                        mMotionDispPathRegister.Add(mMotionPath)

                                        mLastPos.PosX = mMotionPath.Line3D.EndPosX
                                        mLastPos.PosY = mMotionPath.Line3D.EndPosY

                                        '[Note]:紀錄最近一筆的路徑資料
                                        With mLastPatternPath
                                            .PathType = ePathType.Line3D
                                            .Line3D.StartPosX = mRegisterPath.Line3D.StartPosX
                                            .Line3D.StartPosY = mRegisterPath.Line3D.StartPosY
                                            .Line3D.StartPosZ = mRegisterPath.Line3D.StartPosZ
                                            .Line3D.EndPosX = mRegisterPath.Line3D.EndPosX + mExtendOffset.PosX
                                            .Line3D.EndPosY = mRegisterPath.Line3D.EndPosY + mExtendOffset.PosY
                                            .Line3D.EndPosZ = mRegisterPath.Line3D.EndPosZ
                                            .Line3D.ExtendOffsetX = mExtendOffset.PosX
                                            .Line3D.ExtendOffsetY = mExtendOffset.PosY
                                            .Line3D.Velocity = mRunUp.Velocity
                                        End With
                                    End If
                                End If
                            Else
                                '[Note]:還沒開放的路徑  或  有問題(Bug)
                                Return False
                            End If
                    End Select
                End If
            Next
        End If
        '[Note]:mMaxBlendTime20 單位:s
        maxBlendTime = mMaxBlendTime
        maxExtendDistance = mMaxExtendDistance
        motionDispPathRegister = mMotionDispPathRegister
        motionPath = mMotionDispPathRegister.Item(0)

        Return True
    End Function

    ''' <summary>[是否可以省略Trigger Cmd之傳送]</summary>
    ''' <param name="pathIndex"></param>
    ''' <param name="dispType"></param>
    ''' <param name="triggerDispPathRegister"></param>
    ''' <param name="FixCount"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsPassTriggerCmd(ByVal pathIndex As Integer, ByVal dispType As enmTriggerDispType, ByVal triggerDispPathRegister As List(Of sPatternPath), ByVal FixCount As Decimal, ByRef triggerPathMenory As sPatternPath, ByRef isOnPurge As Boolean) As Boolean
        Dim mPatternPath As sPatternPath
        'Dim mDotCounts As Integer                       '[打點顆數]

        '[Note]:若之前有做過Purge，則不能省略Trigger cmd之傳送
        If isOnPurge = True Then
            triggerPathMenory = Nothing
            '[Note]:清除記錄的唯一地方
            isOnPurge = False
            Return False
        End If

        '[Note]:單Dot之條件，TriggerPath只能一條且為Dot且Dot數為1
        If pathIndex <> 0 Then
            triggerPathMenory = Nothing
            '[Note]:清除記錄的唯一地方
            isOnPurge = False
            Return False
        End If
        If triggerDispPathRegister.Count <> 1 Then
            triggerPathMenory = Nothing
            '[Note]:清除記錄的唯一地方
            isOnPurge = False
            Return False
        End If
        'mDotCounts = 0
        mPatternPath = triggerDispPathRegister.Item(0)
        Select Case dispType
            'Case enmTriggerDispType.JetRecipe
            Case enmTriggerDispType.VisionRecipe
                '[Note]:塞給TriggerBoard只有Arc、Dot、Line
                If FixCount > 0 Then
                    triggerPathMenory = Nothing
                    '[Note]:清除記錄的唯一地方
                    isOnPurge = False
                    Return False
                End If
                If FixCount <> 1 Then
                    triggerPathMenory = Nothing
                    '[Note]:清除記錄的唯一地方
                    isOnPurge = False
                    Return False
                End If

                Select Case mPatternPath.PathType
                    Case ePathType.Dot3D
                        If mPatternPath.Dot3D.ExtendOffsetX = 0 And mPatternPath.Dot3D.ExtendOffsetY = 0 Then
                            If IsNothing(triggerPathMenory) = True Then
                                triggerPathMenory.PathType = ePathType.Dot3D
                                triggerPathMenory.Dot3D.ExtendOffsetX = mPatternPath.Dot3D.ExtendOffsetX
                                triggerPathMenory.Dot3D.ExtendOffsetY = mPatternPath.Dot3D.ExtendOffsetY
                                triggerPathMenory.Dot3D.IsDispense = mPatternPath.Dot3D.IsDispense
                                '[Note]:清除記錄的唯一地方
                                isOnPurge = False
                                Return False
                            Else
                                If triggerPathMenory.PathType = ePathType.Dot3D And triggerPathMenory.Dot3D.IsDispense = True Then
                                    If triggerPathMenory.Dot3D.ExtendOffsetX = 0 And triggerPathMenory.Dot3D.ExtendOffsetY = 0 Then
                                        Return True
                                    Else
                                        triggerPathMenory.PathType = ePathType.Dot3D
                                        triggerPathMenory.Dot3D.ExtendOffsetX = mPatternPath.Dot3D.ExtendOffsetX
                                        triggerPathMenory.Dot3D.ExtendOffsetY = mPatternPath.Dot3D.ExtendOffsetY
                                        triggerPathMenory.Dot3D.IsDispense = mPatternPath.Dot3D.IsDispense
                                        '[Note]:清除記錄的唯一地方
                                        isOnPurge = False
                                        Return False
                                    End If
                                Else
                                    triggerPathMenory.PathType = ePathType.Dot3D
                                    triggerPathMenory.Dot3D.ExtendOffsetX = mPatternPath.Dot3D.ExtendOffsetX
                                    triggerPathMenory.Dot3D.ExtendOffsetY = mPatternPath.Dot3D.ExtendOffsetY
                                    triggerPathMenory.Dot3D.IsDispense = mPatternPath.Dot3D.IsDispense
                                    '[Note]:清除記錄的唯一地方
                                    isOnPurge = False
                                    Return False
                                End If
                            End If
                        Else
                            triggerPathMenory = Nothing
                            '[Note]:清除記錄的唯一地方
                            isOnPurge = False
                            Return False
                        End If
                    Case Else
                        triggerPathMenory = Nothing
                        '[Note]:清除記錄的唯一地方
                        isOnPurge = False
                        Return False
                End Select

            Case Else
                triggerPathMenory = Nothing
                '[Note]:清除記錄的唯一地方
                isOnPurge = False
                Return False
        End Select
        Return True

    End Function


    ''' <summary>[[估算速度上限(研華提供)]]</summary>
    ''' <param name="velHigh"></param>
    ''' <param name="acc"></param>
    ''' <param name="dec"></param>
    ''' <param name="distance"></param>
    ''' <param name="velocity"></param>
    ''' <param name="optimalVel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EstimateMaxVel(ByVal velHigh As Decimal, ByVal acc As Decimal, ByVal dec As Decimal, ByVal distance As Decimal, ByVal velocity As Decimal, ByRef optimalVel As Decimal) As Boolean
        Dim mMaxVel As Decimal
        Dim mAcc As Decimal
        Dim mVel As Decimal

        'mAcc = gSSystemParameter.GlobalAcc * gSSystemParameter.AccRatio
        mAcc = acc
        mMaxVel = Math.Sqrt(mAcc * 2 * distance)

        '[Note]:估算跨線段速度
        If mMaxVel > velocity Then
            '[Note]:若二者速度落差過大，則更改為二者的平均值，並且卡上限(最大速)。
            mVel = (mMaxVel + velocity) / 2
            If mVel > velHigh Then
                mVel = velHigh
            End If
            optimalVel = mVel
            'optimalVel = velocity
        Else
            optimalVel = mMaxVel
        End If
        Return True

    End Function

    ''' <summary>[估算跨元件的速度(給座標)]</summary>
    ''' <param name="velHigh"></param>
    ''' <param name="acc"></param>
    ''' <param name="dec"></param>
    ''' <param name="lastPos"></param>
    ''' <param name="nowPos"></param>
    ''' <param name="blendingTime"></param>
    ''' <param name="lastPathVelocity"></param>
    ''' <param name="nowPathVelocity"></param>
    ''' <param name="velocity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EstimateCrossVelocity(ByVal velHigh As Decimal, ByVal acc As Decimal, ByVal dec As Decimal, ByVal lastPos As Premtek.sPos, ByVal nowPos As Premtek.sPos, ByVal blendingTime As Decimal, ByVal lastPathVelocity As Decimal, ByVal nowPathVelocity As Decimal, ByRef velocity As Decimal) As Boolean
        Dim mAcc As Decimal
        Dim mVelocity As Decimal
        Dim mMinVelocity As Decimal
        Dim mdx As Decimal
        Dim mdy As Decimal
        Dim mdr As Decimal
        Dim mVelocityDistanceL As Decimal
        Dim mVelocityDistanceN As Decimal
        mMinVelocity = 99999

        'mAcc = CDec(gSSystemParameter.GlobalAcc * gSSystemParameter.AccRatio)
        mAcc = acc

        '[Note]:單位: mm/s
        'blendingTime  ms-->s
        '[Note]:V=V0+0.5*att
        mVelocity = lastPathVelocity + (0.001 * blendingTime * mAcc)
        If mMinVelocity > mVelocity Then
            mMinVelocity = mVelocity
        End If
        mVelocity = nowPathVelocity + (0.001 * blendingTime * mAcc)
        If mMinVelocity > mVelocity Then
            mMinVelocity = mVelocity
        End If

        '[Note]:由距離來算最大速(不可能為非線段，若是非線段-->有鬼)
        mdx = nowPos.PosX - lastPos.PosX
        mdy = nowPos.PosY - lastPos.PosY
        mdr = Sqrt(mdx * mdx + mdy * mdy)

        '[Note]:距離只能取一半，速度上去還要再下來
        '[Note]:V^2=V0^2+2ax
        mVelocityDistanceL = Sqrt(lastPathVelocity * lastPathVelocity + mAcc * mdr)
        If mMinVelocity > mVelocityDistanceL Then
            mMinVelocity = mVelocityDistanceL
        End If
        mVelocityDistanceN = Sqrt(nowPathVelocity * nowPathVelocity + mAcc * mdr)
        If mMinVelocity > mVelocityDistanceN Then
            mMinVelocity = mVelocityDistanceN
        End If

        Call EstimateMaxVel(velHigh, mAcc, dec, mdr, mMinVelocity, velocity)

        Return True
    End Function

    ''' <summary>[估算跨元件的速度(給距離)]</summary>
    ''' <param name="distance"></param>
    ''' <param name="blendingTime"></param>
    ''' <param name="lastPathVelocity"></param>
    ''' <param name="nowPathVelocity"></param>
    ''' <param name="velocity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EstimateCrossVelocity(ByVal velHigh As Decimal, ByVal acc As Decimal, ByVal dec As Decimal, ByVal distance As Decimal, ByVal blendingTime As Decimal, ByVal lastPathVelocity As Decimal, ByVal nowPathVelocity As Decimal, ByRef velocity As Decimal) As Boolean
        Dim mAcc As Decimal
        Dim mVelocity As Decimal
        Dim mMinVelocity As Decimal
        Dim mVelocityDistanceL As Decimal
        Dim mVelocityDistanceN As Decimal
        mMinVelocity = 99999

        'mAcc = CDec(gSSystemParameter.GlobalAcc * gSSystemParameter.AccRatio)
        mAcc = acc
        '[Note]:單位: mm/s
        'blendingTime  ms-->s
        '[Note]:V=V0+0.5*att
        mVelocity = lastPathVelocity + (0.001 * blendingTime * mAcc)
        If mMinVelocity > mVelocity Then
            mMinVelocity = mVelocity
        End If
        mVelocity = nowPathVelocity + (0.001 * blendingTime * mAcc)
        If mMinVelocity > mVelocity Then
            mMinVelocity = mVelocity
        End If

        '[Note]:距離只能取一半，速度上去還要再下來
        '[Note]:V^2=V0^2+2ax
        mVelocityDistanceL = Sqrt(lastPathVelocity * lastPathVelocity + mAcc * distance)
        If mMinVelocity > mVelocityDistanceL Then
            mMinVelocity = mVelocityDistanceL
        End If
        mVelocityDistanceN = Sqrt(nowPathVelocity * nowPathVelocity + mAcc * distance)
        If mMinVelocity > mVelocityDistanceN Then
            mMinVelocity = mVelocityDistanceN
        End If

        Call EstimateMaxVel(velHigh, acc, dec, distance, mMinVelocity, velocity)

        Return True
    End Function


    ''' <summary>[估算Blending Time]</summary>
    ''' <param name="inBlendTime">[單位:s]</param>
    ''' <param name="outBlendingTime">[單位:ms]</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EstimateBlendingTime(ByVal inBlendTime As Decimal, ByRef outBlendingTime As Integer) As Boolean

        Dim mBlendingTime As Integer
        Dim mValue As Integer

        '[Note]:轉換成軸卡接受的資料
        mBlendingTime = CInt(inBlendTime * 1000)
        mValue = mBlendingTime Mod 2
        If mValue = 0 Then
            outBlendingTime = mBlendingTime
        Else
            outBlendingTime = mBlendingTime + 1
        End If
        outBlendingTime = mBlendingTime * 2
        'Debug.Print("BlendingTime: " & outBlendingTime)
        Return True

    End Function


    ''' <summary>[增加點的路徑]</summary>
    ''' <param name="path"></param>
    ''' <param name="motionDispPathList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddMotionPathDot3D(ByVal path As sMotionPathRegister, ByRef motionDispPathList As List(Of sMotionPathRegister)) As Boolean

        Dim mAddPath As New sMotionPathRegister
        mAddPath.PathType = ePathRegisterType.Dot3D
        mAddPath.Dot3D.PosX = path.Dot3D.PosX
        mAddPath.Dot3D.PosY = path.Dot3D.PosY
        mAddPath.Dot3D.PosZ = path.Dot3D.PosZ
        mAddPath.Dot3D.Velocity = path.Dot3D.Velocity

        If motionDispPathList Is Nothing Then
            motionDispPathList = New List(Of sMotionPathRegister)
            motionDispPathList.Clear()
            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
            motionDispPathList.Capacity = 1
        End If

        '[Note]:把第一筆資料的起始點保留，不要加入-->直接移動到第一點座標，不要串入路徑內
        If motionDispPathList.Count = 0 Then
            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
            motionDispPathList.Capacity = motionDispPathList.Count + 1
            motionDispPathList.Add(mAddPath)
        Else
            '[Note]:判斷上一筆資料與預新增的點是否相同，若相同則不加
            Select Case motionDispPathList.Item(motionDispPathList.Count - 1).PathType
                Case ePathRegisterType.Arc2D
                    With motionDispPathList.Item(motionDispPathList.Count - 1).Arc2D
                        '[Note]:若二點座標太過於相近(1um)，視為同一點
                        'Debug.Print("GG1: " & mAddPath.Dot3D.PosX & " , " & mAddPath.Dot3D.PosY & " , " & mAddPath.Dot3D.PosZ)
                        'Debug.Print("GG2: " & .EndPosX & " , " & .EndPosY & " , " & .EndPosZ)
                        '[Note]:先忽略Z軸，以後再想
                        If Math.Abs(.EndPosX - mAddPath.Dot3D.PosX) > 0.001 Or Math.Abs(.EndPosY - mAddPath.Dot3D.PosY) > 0.001 Then
                            'If Math.Abs(.EndPosX - mAddPath.Dot3D.PosX) > 0.001 Or Math.Abs(.EndPosY - mAddPath.Dot3D.PosY) > 0.001 Or Math.Abs(.EndPosZ - mAddPath.Dot3D.PosZ) > 0.001 Then
                            'If .EndPosX <> mAddPath.Dot3D.PosX Or .EndPosY <> mAddPath.Dot3D.PosY Then
                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
                            motionDispPathList.Capacity = motionDispPathList.Count + 1
                            motionDispPathList.Add(mAddPath)
                        Else
                            'Debug.Print("GGR: " & .EndPosX & " , " & .EndPosY)
                        End If
                    End With
                Case ePathRegisterType.Dot3D
                    With motionDispPathList.Item(motionDispPathList.Count - 1).Dot3D
                        '[Note]:若二點座標太過於相近(1um)，視為同一點
                        'Debug.Print("GG1: " & mAddPath.Dot3D.PosX & " , " & mAddPath.Dot3D.PosY & " , " & mAddPath.Dot3D.PosZ)
                        'Debug.Print("GG2: " & .PosX & " , " & .PosY & " , " & .PosZ)
                        '[Note]:先忽略Z軸，以後再想
                        If Math.Abs(.PosX - mAddPath.Dot3D.PosX) > 0.001 Or Math.Abs(.PosY - mAddPath.Dot3D.PosY) > 0.001 Then
                            'If Math.Abs(.PosX - mAddPath.Dot3D.PosX) > 0.001 Or Math.Abs(.PosY - mAddPath.Dot3D.PosY) > 0.001 Or Math.Abs(.PosZ - mAddPath.Dot3D.PosZ) > 0.001 Then
                            'If .PosX <> mAddPath.Dot3D.PosX Or .PosY <> mAddPath.Dot3D.PosY Then
                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
                            motionDispPathList.Capacity = motionDispPathList.Count + 1
                            motionDispPathList.Add(mAddPath)
                        Else
                            'Debug.Print("GGR: " & .PosX & " , " & .PosY)
                        End If
                    End With

                Case ePathRegisterType.Wait
                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
                    motionDispPathList.Capacity = motionDispPathList.Count + 1
                    motionDispPathList.Add(mAddPath)

            End Select
        End If
        Return True

    End Function

    ''' <summary>[增加點的路徑]</summary>
    ''' <param name="path"></param>
    ''' <param name="motionDispPathList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddMotionPathArc2D(ByVal path As sMotionPathRegister, ByRef motionDispPathList As List(Of sMotionPathRegister)) As Boolean

        '[Note]:把第一筆資料的起始點保留，不要加入-->直接移動到第一點座標，不要串入路徑內
        '[Note]:Are2D-->前面已經加工處理過了，所以不可能為第一筆資料
        '[Note]:直接加上去了-->後面再來看有沒有梗

        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
        motionDispPathList.Capacity = motionDispPathList.Count + 1
        motionDispPathList.Add(path)
        Return True

    End Function

    ''' <summary>[把飛拍路徑轉換成Motion看得懂的資料格式]</summary>
    ''' <param name="velHigh"></param>
    ''' <param name="acc"></param>
    ''' <param name="dec"></param>
    ''' <param name="maxExtendDistance"></param>
    ''' <param name="maxBlendTime"></param>
    ''' <param name="motionDispPathRegister"></param>
    ''' <param name="motionDispPathList"></param>
    ''' <param name="firstpath"></param>
    ''' <param name="maxDispenseSpeed"></param>
    ''' <param name="lastPos"></param>
    ''' <param name="lastPosVelocity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MotionPathCoversion(ByVal velHigh As Decimal, ByVal acc As Decimal, ByVal dec As Decimal, ByVal maxExtendDistance As Decimal, ByVal maxBlendTime As Decimal, ByVal motionDispPathRegister As List(Of sPatternPath), ByRef motionDispPathList As List(Of sMotionPathRegister), ByRef firstpath As sDot3DPath, ByRef lastPath As sDot3DPath, Optional ByRef maxDispenseSpeed As Decimal = 0, Optional ByRef lastPos As Premtek.sPos = Nothing, Optional ByRef lastPosVelocity As Decimal = 0) As Boolean

        Dim mI As Integer
        Dim mPath As sPatternPath
        Dim mMotionPath As New sMotionPathRegister
        'Dim mLastMotionPath As New sMotionPathRegister
        Dim mBlendingTime As Integer
        Dim mLastPathVelocity As Decimal
        Dim mLastPos As Premtek.sPos
        Dim mMaxDispenseSpeed As Decimal

        Call EstimateBlendingTime(maxBlendTime, mBlendingTime)
        mLastPathVelocity = 0
        mMaxDispenseSpeed = 0

        '[Note]:有梗-->若路徑裡面只有Wait那就取不到第一點座標了(除非被惡搞不然不會發生)
        '[Note]:把第一筆資料的起始點保留，不要加入-->直接移動到第一點座標，不要串入路徑內
        For mI = 0 To motionDispPathRegister.Count - 1
            mPath = motionDispPathRegister.Item(mI)
            Select Case mPath.PathType

                Case ePathType.Arc2D
                    'Debug.Print("Arc2D: " & mPath.Arc2D.StartPosX & " , " & mPath.Arc2D.StartPosY & " , " & mPath.Arc2D.CenterPosX & " , " & mPath.Arc2D.CenterPosY & " , " & mPath.Arc2D.EndPosX & " , " & mPath.Arc2D.EndPosY)
                    If mI = 0 Then
                        '[Note]:1st-->起始點保留
                        firstpath.PosX = mPath.Arc2D.StartPosX
                        firstpath.PosY = mPath.Arc2D.StartPosY
                        firstpath.PosZ = mPath.Arc2D.StartPosZ
                        firstpath.ParameterType = mPath.Arc2D.ParameterType
                        firstpath.ParameterName = mPath.Arc2D.ParameterName
                        '[Note]:後面再補Z軸下去的速度跟相關資訊

                        '[Note]:需要給第一個點
                        With mMotionPath
                            .PathType = ePathRegisterType.Dot3D
                            .Dot3D.PosX = mPath.Arc2D.StartPosX
                            .Dot3D.PosY = mPath.Arc2D.StartPosY
                            .Dot3D.PosZ = mPath.Arc2D.StartPosZ
                            .Dot3D.Velocity = mPath.Arc2D.Velocity
                        End With
                        Call AddMotionPathArc2D(mMotionPath, motionDispPathList)
                    Else
                        '[Note]:移動起始點-->在畫圓
                        With mMotionPath
                            .PathType = ePathRegisterType.Dot3D
                            .Dot3D.PosX = mPath.Arc2D.StartPosX
                            .Dot3D.PosY = mPath.Arc2D.StartPosY
                            .Dot3D.PosZ = mPath.Arc2D.StartPosZ
                            .Dot3D.Velocity = mPath.Arc2D.Velocity
                        End With
                        Call AddMotionPathDot3D(mMotionPath, motionDispPathList)
                    End If

                    With mMotionPath
                        .PathType = ePathRegisterType.Arc2D
                        .Arc2D.CenterPosX = mPath.Arc2D.CenterPosX
                        .Arc2D.CenterPosY = mPath.Arc2D.CenterPosY
                        .Arc2D.CenterPosZ = mPath.Arc2D.CenterPosZ
                        .Arc2D.EndPosX = mPath.Arc2D.EndPosX
                        .Arc2D.EndPosY = mPath.Arc2D.EndPosY
                        .Arc2D.EndPosZ = mPath.Arc2D.EndPosZ
                        .Arc2D.ArcDirection = mPath.Arc2D.ArcDirection
                        .Arc2D.Velocity = mPath.Arc2D.Velocity
                    End With
                    Call AddMotionPathArc2D(mMotionPath, motionDispPathList)
                    mLastPathVelocity = mPath.Arc2D.Velocity
                    mLastPos.PosX = mPath.Arc2D.EndPosX
                    mLastPos.PosY = mPath.Arc2D.EndPosY
                    lastPath.ParameterType = mPath.Arc2D.ParameterType
                    lastPath.ParameterName = mPath.Arc2D.ParameterName
                    If mPath.Arc2D.IsDispense = True Then
                        If mMaxDispenseSpeed < mPath.Arc2D.Velocity Then
                            mMaxDispenseSpeed = mPath.Arc2D.Velocity
                        End If
                    End If

                Case ePathType.Arc3D
                    'Debug.Print("Arc3D: " & mPath.Arc3D.StartPosX & " , " & mPath.Arc3D.StartPosY & " , " & mPath.Arc3D.CenterPosX & " , " & mPath.Arc3D.CenterPosY & " , " & mPath.Arc3D.EndPosX & " , " & mPath.Arc3D.EndPosY)
                    If mI = 0 Then
                        '[Note]:1st-->起始點保留
                        firstpath.PosX = mPath.Arc3D.StartPosX
                        firstpath.PosY = mPath.Arc3D.StartPosY
                        firstpath.PosZ = mPath.Arc3D.StartPosZ
                        firstpath.ParameterType = mPath.Arc3D.ParameterType
                        firstpath.ParameterName = mPath.Arc3D.ParameterName
                        '[Note]:後面再補Z軸下去的速度跟相關資訊
                    Else
                        '[Note]:移動起始點-->在畫圓
                        With mMotionPath
                            .PathType = ePathRegisterType.Dot3D
                            .Dot3D.PosX = mPath.Arc3D.StartPosX
                            .Dot3D.PosY = mPath.Arc3D.StartPosY
                            .Dot3D.PosZ = mPath.Arc3D.StartPosZ
                            .Dot3D.Velocity = mPath.Arc3D.Velocity
                        End With
                        Call AddMotionPathDot3D(mMotionPath, motionDispPathList)
                    End If

                    With mMotionPath
                        .PathType = ePathRegisterType.Arc2D
                        .Arc2D.CenterPosX = mPath.Arc3D.CenterPosX
                        .Arc2D.CenterPosY = mPath.Arc3D.CenterPosY
                        .Arc2D.CenterPosZ = mPath.Arc3D.CenterPosZ
                        .Arc2D.EndPosX = mPath.Arc3D.EndPosX
                        .Arc2D.EndPosY = mPath.Arc3D.EndPosY
                        .Arc2D.EndPosZ = mPath.Arc3D.EndPosZ
                        .Arc2D.ArcDirection = mPath.Arc3D.ArcDirection
                        .Arc2D.Velocity = mPath.Arc3D.Velocity
                    End With
                    Call AddMotionPathArc2D(mMotionPath, motionDispPathList)
                    mLastPathVelocity = mPath.Arc3D.Velocity
                    mLastPos.PosX = mPath.Arc3D.EndPosX
                    mLastPos.PosY = mPath.Arc3D.EndPosY
                    lastPath.ParameterType = mPath.Arc3D.ParameterType
                    lastPath.ParameterName = mPath.Arc3D.ParameterName

                    If mPath.Arc3D.IsDispense = True Then
                        If mMaxDispenseSpeed < mPath.Arc3D.Velocity Then
                            mMaxDispenseSpeed = mPath.Arc3D.Velocity
                        End If
                    End If

                Case ePathType.Dot3D
                    'Debug.Print("Dot3D: " & mPath.Dot3D.PosX & " , " & mPath.Dot3D.PosY)
                    If mI = 0 Then
                        '[Note]:1st-->起始點保留
                        firstpath.PosX = mPath.Dot3D.PosX
                        firstpath.PosY = mPath.Dot3D.PosY
                        firstpath.PosZ = mPath.Dot3D.PosZ
                        firstpath.ParameterType = mPath.Dot3D.ParameterType
                        firstpath.ParameterName = mPath.Dot3D.ParameterName
                        mLastPathVelocity = mPath.Dot3D.Velocity
                        mLastPos.PosX = mPath.Dot3D.PosX
                        mLastPos.PosY = mPath.Dot3D.PosY
                        'Debug.Print("1st(X,Y): " & firstpath.PosX & " , " & firstpath.PosY & " , " & firstpath.PosZ)
                    Else
                        With mMotionPath
                            .PathType = ePathRegisterType.Dot3D
                            .Dot3D.PosX = mPath.Dot3D.PosX
                            .Dot3D.PosY = mPath.Dot3D.PosY
                            .Dot3D.PosZ = mPath.Dot3D.PosZ
                            .Dot3D.Velocity = mPath.Dot3D.Velocity
                        End With
                        Call AddMotionPathDot3D(mMotionPath, motionDispPathList)
                        mLastPathVelocity = mMotionPath.Dot3D.Velocity
                        mLastPos.PosX = mMotionPath.Dot3D.PosX
                        mLastPos.PosY = mMotionPath.Dot3D.PosY
                        If mPath.Dot3D.IsDispense = True Then
                            If mMaxDispenseSpeed < mPath.Dot3D.Velocity Then
                                mMaxDispenseSpeed = mPath.Dot3D.Velocity
                            End If
                        End If
                        'Debug.Print("(X,Y): " & mMotionPath.Dot3D.PosX & " , " & mMotionPath.Dot3D.PosY & "," & mMotionPath.Dot3D.Velocity)
                    End If
                    lastPath.ParameterType = mPath.Dot3D.ParameterType
                    lastPath.ParameterName = mPath.Dot3D.ParameterName

                Case ePathType.Line3D
                    'Debug.Print("Line3D: " & mPath.Line3D.StartPosX & " , " & mPath.Line3D.StartPosY & " , " & mPath.Line3D.EndPosX & " , " & mPath.Line3D.EndPosY)
                    If mI = 0 Then
                        '[Note]:1st-->起始點保留
                        firstpath.PosX = mPath.Line3D.StartPosX
                        firstpath.PosY = mPath.Line3D.StartPosY
                        firstpath.PosZ = mPath.Line3D.StartPosZ
                        firstpath.ParameterType = mPath.Line3D.ParameterType
                        firstpath.ParameterName = mPath.Line3D.ParameterName
                        'Debug.Print("1st(X,Y): " & firstpath.PosX & " , " & firstpath.PosY & " , " & firstpath.PosZ)

                        With mMotionPath
                            .PathType = ePathRegisterType.Dot3D
                            .Dot3D.PosX = mPath.Line3D.EndPosX
                            .Dot3D.PosY = mPath.Line3D.EndPosY
                            .Dot3D.PosZ = mPath.Line3D.EndPosZ
                            .Dot3D.Velocity = mPath.Line3D.Velocity
                        End With
                        If mPath.Line3D.StartPosX = mPath.Line3D.EndPosX And mPath.Line3D.StartPosY = mPath.Line3D.EndPosY Then
                            '[Note]:同一點就不加了                        
                        Else
                            Call AddMotionPathDot3D(mMotionPath, motionDispPathList)
                        End If
                    Else
                        With mMotionPath
                            .PathType = ePathRegisterType.Dot3D
                            .Dot3D.PosX = mPath.Line3D.StartPosX
                            .Dot3D.PosY = mPath.Line3D.StartPosY
                            .Dot3D.PosZ = mPath.Line3D.StartPosZ
                            .Dot3D.Velocity = mPath.Line3D.Velocity
                        End With
                        Call AddMotionPathDot3D(mMotionPath, motionDispPathList)
                        'Debug.Print("(X,Y): " & mMotionPath.Dot3D.PosX & " , " & mMotionPath.Dot3D.PosY & "," & mMotionPath.Dot3D.Velocity)

                        With mMotionPath
                            .PathType = ePathRegisterType.Dot3D
                            .Dot3D.PosX = mPath.Line3D.EndPosX
                            .Dot3D.PosY = mPath.Line3D.EndPosY
                            .Dot3D.PosZ = mPath.Line3D.EndPosZ
                            .Dot3D.Velocity = mPath.Line3D.Velocity
                        End With
                        Call AddMotionPathDot3D(mMotionPath, motionDispPathList)
                    End If

                    mLastPathVelocity = mMotionPath.Dot3D.Velocity
                    mLastPos.PosX = mMotionPath.Dot3D.PosX
                    mLastPos.PosY = mMotionPath.Dot3D.PosY
                    lastPath.ParameterType = mPath.Line3D.ParameterType
                    lastPath.ParameterName = mPath.Line3D.ParameterName
                    If mPath.Line3D.IsDispense = True Then
                        If mMaxDispenseSpeed < mPath.Line3D.Velocity Then
                            mMaxDispenseSpeed = mPath.Line3D.Velocity
                        End If
                    End If
                    'Debug.Print("(X,Y): " & mMotionPath.Dot3D.PosX & " , " & mMotionPath.Dot3D.PosY & "," & mMotionPath.Dot3D.Velocity)

                Case ePathType.Wait
                    With mMotionPath
                        .PathType = ePathRegisterType.Wait
                        .Wait.DwellTimeInMs = mPath.Wait.DwellTimeInMs
                    End With
                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                    motionDispPathList.Capacity = motionDispPathList.Count + 1
                    motionDispPathList.Add(mMotionPath)
            End Select
        Next
        lastPosVelocity = mLastPathVelocity
        lastPos = mLastPos
        maxDispenseSpeed = mMaxDispenseSpeed
        Return True
    End Function


    ''' <summary>[判斷資料傳輸(由哪個傳到哪個)]</summary>
    ''' <param name="pathIndex"></param>
    ''' <param name="triggerDispPathRegister"></param>
    ''' <param name="lastPathIndex"></param>
    ''' <param name="isTransmissionResuming"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EstimateTriggerStep(ByVal pathIndex As Integer, ByVal triggerDispPathRegister As List(Of sPatternPath), ByRef lastPathIndex As Integer, ByRef isTransmissionResuming As Boolean) As Boolean

        Const mMaxStepCounts As Integer = 100       '[資料傳輸之上限(第一筆資料)]
        Const mMaxTRStepCounts As Integer = 20      '[資料傳輸之上限(非第一筆資料)]

        If triggerDispPathRegister.Count > mMaxStepCounts Then
            If pathIndex = 0 Then
                '[Note]:1stPath
                lastPathIndex = mMaxStepCounts - 1
                isTransmissionResuming = False
            Else
                '[Note]:2st~Last Path
                If (pathIndex + mMaxTRStepCounts - 1) > triggerDispPathRegister.Count - 1 Then
                    lastPathIndex = triggerDispPathRegister.Count - 1
                Else
                    lastPathIndex = pathIndex + mMaxTRStepCounts - 1
                End If
                isTransmissionResuming = True
            End If
        Else
            lastPathIndex = triggerDispPathRegister.Count - 1
            isTransmissionResuming = False
        End If
        Return True
    End Function

    ''' <summary>[取最佳化的初速度，使初速不為0]</summary>
    ''' <param name="lastVelHigh"></param>
    ''' <param name="nowVelHigh"></param>
    ''' <param name="nowVelLow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OptimizationVelLow(ByVal lastVelHigh As Decimal, ByVal nowVelHigh As Decimal, ByRef nowVelLow As Decimal) As Boolean
        If nowVelHigh > lastVelHigh Then
            If lastVelHigh = 0 Then
                '[Note]:初速為最大速的一半好了
                nowVelLow = nowVelHigh * 0.5
            Else
                nowVelLow = lastVelHigh
            End If
        Else
            '[Note]:初速為最大速的一半好了
            nowVelLow = nowVelHigh * 0.5
            If nowVelLow < 0 Then
                nowVelLow = 0
            End If
        End If
        Return True
    End Function

    ''' <summary>[將路徑資料丟給Motion Card]</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="motionDispPathList"></param>
    ''' <param name="maxExtendDistance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SendPathToMotion(ByVal stageNo As Integer, ByVal motionDispPathList As List(Of sMotionPathRegister), ByVal maxExtendDistance As Decimal, ByVal maxBlendTime As Decimal) As Boolean

        Dim mI As Integer
        Dim mAcc As Decimal
        Dim mDec As Decimal
        Dim mVelLow As Decimal
        Dim mVelHigh As Decimal
        Dim mPathCount As Integer
        Dim mBlendingTime As Integer
        Dim mLastVelHigh As Decimal
        Const mm As Decimal = 0.001

        '[Note]:判斷有無動作
        If motionDispPathList.Count = 0 Then
            Return True
        End If



        Call EstimateBlendingTime(maxBlendTime, mBlendingTime)
        mAcc = gCMotion.SyncParameter(stageNo).Velocity.Acc * gCMotion.SyncParameter(stageNo).Velocity.AccRatio
        mDec = gCMotion.SyncParameter(stageNo).Velocity.Dec * gCMotion.SyncParameter(stageNo).Velocity.DecRatio
        mLastVelHigh = 0
        mVelLow = 0
        mPathCount = 0

        'Call gCMotion.GPSetRunMode(gCMotion.SyncParameter(stageNo), gCRecipe.GPSetRunMode, mBlendingTime)
        '[Note]:飛拍用Fly Mode，但需要有二個前提條件
        '                       1.不會有變速的情況發生(所有的路徑都是使用相同速度)
        '                       2.不允許有任何直角的動作，全程都是很平順的動作包含所有的轉彎
        Call gCMotion.GPSetRunMode(gCMotion.SyncParameter(stageNo), eRunMode.BlendingMode, mBlendingTime) 'Soni / 2017.09.11 FlyMode跑出來的速度前面幾筆都是50, 只有最後一筆是100, 原因不明, 改為BlendingMode後速度為100.

        '[Note]:清除同動內Buffer的資料
        If gCMotion.GpClearMovePath(gCMotion.SyncParameter(stageNo)) <> CommandStatus.Sucessed Then
            Return False
        End If

        '[Note]:將座標塞進Motion
        For mI = 0 To motionDispPathList.Count - 1
            Select Case motionDispPathList.Item(mI).PathType
                Case ePathRegisterType.Arc2D
                    '[Note]:最後一筆連續移動的命令，需用End Path
                    mVelHigh = motionDispPathList.Item(mI).Arc2D.Velocity
                    If mI = 0 Or mI = motionDispPathList.Count - 1 Then '初速末速為0
                        mVelLow = 0
                    Else
                        'OptimizationVelLow(mLastVelHigh, mVelHigh, mVelLow)
                        mVelLow = mLastVelHigh
                    End If
                    mLastVelHigh = mVelHigh
                    If gCMotion.GpSetVelLow(gCMotion.SyncParameter(stageNo), mVelLow) <> CommandStatus.Sucessed Then
                        Return False
                    End If
                    If gCMotion.GpSetVelHigh(gCMotion.SyncParameter(stageNo), mVelHigh) <> CommandStatus.Sucessed Then
                        Return False
                    End If
                    If gCMotion.GpSetAcc(gCMotion.SyncParameter(stageNo), mAcc) <> CommandStatus.Sucessed Then
                        Return False
                    End If
                    If gCMotion.GpSetDec(gCMotion.SyncParameter(stageNo), mDec) <> CommandStatus.Sucessed Then
                        Return False
                    End If
                    Select Case motionDispPathList.Item(mI).Arc2D.ArcDirection
                        Case eArcDirection.CW
                            gCMotion.SyncParameter(stageNo).Cmd = Advantech.Motion.PathCmd.Abs2DArcCW

                        Case eArcDirection.CCW
                            gCMotion.SyncParameter(stageNo).Cmd = Advantech.Motion.PathCmd.Abs2DArcCCW

                    End Select
                    gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) = motionDispPathList.Item(mI).Arc2D.EndPosX
                    gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) = motionDispPathList.Item(mI).Arc2D.EndPosY
                    gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ) = motionDispPathList.Item(mI).Arc2D.EndPosZ
                    gCMotion.SyncParameter(stageNo).CenterPos(sAxis.AxisX) = motionDispPathList.Item(mI).Arc2D.CenterPosX
                    gCMotion.SyncParameter(stageNo).CenterPos(sAxis.AxisY) = motionDispPathList.Item(mI).Arc2D.CenterPosY
                    gCMotion.SyncParameter(stageNo).CenterPos(sAxis.AxisZ) = motionDispPathList.Item(mI).Arc2D.CenterPosZ
                    gCMotion.SyncParameter(stageNo).SetVelLow(mVelLow)
                    gCMotion.SyncParameter(stageNo).SetVelHigh(mVelHigh)
                    'Debug.Print("Path(Arc): " & gCMotion.SyncParameter(stageNo).Cmd & " , " & mVelHigh & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ) & " , " & gCMotion.SyncParameter(stageNo).CenterPos(sAxis.AxisX) & " , " & gCMotion.SyncParameter(stageNo).CenterPos(sAxis.AxisY) & " , " & gCMotion.SyncParameter(stageNo).CenterPos(sAxis.AxisZ))
                    If gCMotion.GpAddArcPath(gCMotion.SyncParameter(stageNo), False) <> CommandStatus.Sucessed Then
                        Return False
                    Else
                        mPathCount = mPathCount + 1
                    End If
                    If mI = motionDispPathList.Count - 1 And motionDispPathList.Count >= ProjectMotion.PathCountLimit Then
                        '[Note]:重新塞數值，不能刪
                        gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) = motionDispPathList.Item(mI).Arc2D.EndPosX
                        gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) = motionDispPathList.Item(mI).Arc2D.EndPosY
                        gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ) = motionDispPathList.Item(mI).Arc2D.EndPosZ
                        gCMotion.SyncParameter(stageNo).CenterPos(sAxis.AxisX) = motionDispPathList.Item(mI).Arc2D.CenterPosX
                        gCMotion.SyncParameter(stageNo).CenterPos(sAxis.AxisY) = motionDispPathList.Item(mI).Arc2D.CenterPosY
                        gCMotion.SyncParameter(stageNo).CenterPos(sAxis.AxisZ) = motionDispPathList.Item(mI).Arc2D.CenterPosZ
                        gCMotion.SyncParameter(stageNo).SetVelLow(mVelLow)
                        gCMotion.SyncParameter(stageNo).SetVelHigh(mVelHigh)
                        'Debug.Print("Path(Arc): " & gCMotion.SyncParameter(stageNo).Cmd & " , " & mVelHigh & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ) & " , " & gCMotion.SyncParameter(stageNo).CenterPos(sAxis.AxisX) & " , " & gCMotion.SyncParameter(stageNo).CenterPos(sAxis.AxisY) & " , " & gCMotion.SyncParameter(stageNo).CenterPos(sAxis.AxisZ))
                        If gCMotion.GpAddArcPath(gCMotion.SyncParameter(stageNo), True) = False Then
                            Return False
                        End If
                    End If

                Case ePathRegisterType.Dot3D
                    '[Note]:最後一筆連續移動的命令，需用End Path
                    mVelHigh = motionDispPathList.Item(mI).Dot3D.Velocity
                    If mI = 0 Or mI = motionDispPathList.Count - 1 Then '初速末速為0
                        mVelLow = 0
                    Else
                        'OptimizationVelLow(mLastVelHigh, mVelHigh, mVelLow)
                        mVelLow = mLastVelHigh
                    End If
                    mLastVelHigh = mVelHigh
                    If gCMotion.GpSetVelLow(gCMotion.SyncParameter(stageNo), mVelLow) <> CommandStatus.Sucessed Then
                        Return False
                    End If
                    If gCMotion.GpSetVelHigh(gCMotion.SyncParameter(stageNo), mVelHigh) <> CommandStatus.Sucessed Then
                        Return False
                    End If
                    If gCMotion.GpSetAcc(gCMotion.SyncParameter(stageNo), mAcc) <> CommandStatus.Sucessed Then
                        Return False
                    End If
                    If gCMotion.GpSetDec(gCMotion.SyncParameter(stageNo), mDec) <> CommandStatus.Sucessed Then
                        Return False
                    End If
                    gCMotion.SyncParameter(stageNo).Cmd = Advantech.Motion.PathCmd.Abs3DLine
                    gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) = motionDispPathList.Item(mI).Dot3D.PosX
                    gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) = motionDispPathList.Item(mI).Dot3D.PosY
                    gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ) = motionDispPathList.Item(mI).Dot3D.PosZ
                    gCMotion.SyncParameter(stageNo).SetVelLow(mVelLow)
                    gCMotion.SyncParameter(stageNo).SetVelHigh(mVelHigh)
                    'Debug.Print("Path(Dot): " & gCMotion.SyncParameter(stageNo).Cmd & " , " & mVelHigh & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ))
                    If gCMotion.GpAddDotPath(gCMotion.SyncParameter(stageNo), False) <> CommandStatus.Sucessed Then
                        Return False
                    Else
                        mPathCount = mPathCount + 1
                    End If

                    If mI = motionDispPathList.Count - 1 And motionDispPathList.Count >= ProjectMotion.PathCountLimit Then
                        '[Note]:重新塞數值，不能刪
                        gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) = motionDispPathList.Item(mI).Dot3D.PosX
                        gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) = motionDispPathList.Item(mI).Dot3D.PosY
                        gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ) = motionDispPathList.Item(mI).Dot3D.PosZ
                        gCMotion.SyncParameter(stageNo).SetVelLow(mVelLow)
                        gCMotion.SyncParameter(stageNo).SetVelHigh(mVelHigh)
                        'Debug.Print("Path(Dot): " & gCMotion.SyncParameter(stageNo).Cmd & " , " & mVelHigh & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ))
                        If gCMotion.GpAddDotPath(gCMotion.SyncParameter(stageNo), True) <> CommandStatus.Sucessed Then
                            Return False
                        End If
                    End If

                Case ePathRegisterType.Wait
                    gCMotion.SyncParameter(stageNo).Cmd = Advantech.Motion.PathCmd.GPDELAY
                    gCMotion.SyncParameter(stageNo).GpDelay = motionDispPathList.Item(mI).Wait.DwellTimeInMs
                    If gCMotion.GpAddDwell(gCMotion.SyncParameter(stageNo)) <> CommandStatus.Sucessed Then
                        Return False
                    Else
                        mPathCount = mPathCount + 1
                    End If

            End Select
        Next

        '[Note]:資料點需大於三筆，不足則塞同一個點補足三筆的最低限制)，都只補Dot，不補Arc
        If motionDispPathList.Count < ProjectMotion.PathCountLimit Then
            Select Case motionDispPathList.Item(motionDispPathList.Count - 1).PathType
                'Case ePathRegisterType.Arc2D
                '    '[Note]:把最後一筆的資料帶進來，但是增加資料的部分以增加Dot為基準，偷偷拉Z上升，不動XY。
                '    mVelHigh = motionDispPathList.Item(motionDispPathList.Count - 1).Arc2D.Velocity
                '    OptimizationVelLow(mLastVelHigh, mVelHigh, mVelLow)
                '    mLastVelHigh = mVelHigh
                '    If gCMotion.GpSetVelLow(gCMotion.SyncParameter(stageNo), mVelLow) <> CommandStatus.Sucessed Then
                '        Return False
                '    End If
                '    If gCMotion.GpSetVelHigh(gCMotion.SyncParameter(stageNo), mVelHigh) <> CommandStatus.Sucessed Then
                '        Return False
                '    End If
                '    If gCMotion.GpSetAcc(gCMotion.SyncParameter(stageNo), mAcc) <> CommandStatus.Sucessed Then
                '        Return False
                '    End If
                '    If gCMotion.GpSetDec(gCMotion.SyncParameter(stageNo), mDec) <> CommandStatus.Sucessed Then
                '        Return False
                '    End If

                '    gCMotion.SyncParameter(stageNo).Cmd = Advantech.Motion.PathCmd.Abs3DLine

                '    For mI = 1 To CMotionCollection.PathCountLimit
                '        If mPathCount < CMotionCollection.PathCountLimit Then
                '            gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) = motionDispPathList.Item(motionDispPathList.Count - 1).Arc2D.EndPosX
                '            gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) = motionDispPathList.Item(motionDispPathList.Count - 1).Arc2D.EndPosY
                '            gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ) =Math.Round( motionDispPathList.Item(motionDispPathList.Count - 1).Arc2D.EndPosZ + (mm * mI),3)
                '            gCMotion.SyncParameter(stageNo).SetVelLow(mVelLow)
                '            gCMotion.SyncParameter(stageNo).SetVelHigh(mVelHigh)
                '            'Debug.Print("Path(Dot): " & gCMotion.SyncParameter(stageNo).Cmd & " , " & mVelHigh & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ))
                '            If gCMotion.GpAddDotPath(gCMotion.SyncParameter(stageNo), False) <> CommandStatus.Sucessed Then
                '                Return False
                '            Else
                '                mPathCount = mPathCount + 1
                '            End If

                '            '[Note]:跟軸卡說是最後一筆資料了
                '            If mPathCount >= CMotionCollection.PathCountLimit Then
                '                '[Note]:重新塞數值，不能刪
                '                gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) = motionDispPathList.Item(motionDispPathList.Count - 1).Arc2D.EndPosX
                '                gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) = motionDispPathList.Item(motionDispPathList.Count - 1).Arc2D.EndPosY
                '                gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ) = Math.Round(motionDispPathList.Item(motionDispPathList.Count - 1).Arc2D.EndPosZ + (mm * mI),3)
                '                gCMotion.SyncParameter(stageNo).SetVelLow(mVelLow)
                '                gCMotion.SyncParameter(stageNo).SetVelHigh(mVelHigh)
                '                'Debug.Print("Path(Dot): " & gCMotion.SyncParameter(stageNo).Cmd & " , " & mVelHigh & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ))
                '                If gCMotion.GpAddDotPath(gCMotion.SyncParameter(stageNo), True) <> CommandStatus.Sucessed Then
                '                    Return False
                '                End If
                '                Exit For
                '            End If
                '        Else
                '            Exit For
                '        End If
                '    Next

                Case ePathRegisterType.Dot3D
                    mVelHigh = motionDispPathList.Item(motionDispPathList.Count - 1).Dot3D.Velocity
                    If mI = 0 Or mI = motionDispPathList.Count - 1 Then '初速末速為0
                        mVelLow = 0
                    Else
                        'OptimizationVelLow(mLastVelHigh, mVelHigh, mVelLow)
                        mVelLow = mLastVelHigh
                    End If
                    mLastVelHigh = mVelHigh
                    If gCMotion.GpSetVelLow(gCMotion.SyncParameter(stageNo), mVelLow) <> CommandStatus.Sucessed Then
                        Return False
                    End If
                    If gCMotion.GpSetVelHigh(gCMotion.SyncParameter(stageNo), mVelHigh) <> CommandStatus.Sucessed Then
                        Return False
                    End If
                    If gCMotion.GpSetAcc(gCMotion.SyncParameter(stageNo), mAcc) <> CommandStatus.Sucessed Then
                        Return False
                    End If
                    If gCMotion.GpSetDec(gCMotion.SyncParameter(stageNo), mDec) <> CommandStatus.Sucessed Then
                        Return False
                    End If
                    gCMotion.SyncParameter(stageNo).Cmd = Advantech.Motion.PathCmd.Abs3DLine

                    For mI = 1 To ProjectMotion.PathCountLimit
                        If mPathCount < ProjectMotion.PathCountLimit Then
                            gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) = motionDispPathList.Item(motionDispPathList.Count - 1).Dot3D.PosX
                            gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) = motionDispPathList.Item(motionDispPathList.Count - 1).Dot3D.PosY
                            gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ) = Math.Round(motionDispPathList.Item(motionDispPathList.Count - 1).Dot3D.PosZ + (mm * mI), 3)
                            gCMotion.SyncParameter(stageNo).SetVelLow(mVelLow)
                            gCMotion.SyncParameter(stageNo).SetVelHigh(mVelHigh)
                            'Debug.Print("Path(Dot): " & gCMotion.SyncParameter(stageNo).Cmd & " , " & mVelHigh & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ))
                            If gCMotion.GpAddDotPath(gCMotion.SyncParameter(stageNo), False) <> CommandStatus.Sucessed Then
                                Return False
                            Else
                                mPathCount = mPathCount + 1
                            End If

                            '[Note]:跟軸卡說是最後一筆資料了
                            If mPathCount >= ProjectMotion.PathCountLimit Then
                                '[Note]:重新塞數值，不能刪
                                gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) = motionDispPathList.Item(motionDispPathList.Count - 1).Dot3D.PosX
                                gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) = motionDispPathList.Item(motionDispPathList.Count - 1).Dot3D.PosY
                                gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ) = Math.Round(motionDispPathList.Item(motionDispPathList.Count - 1).Dot3D.PosZ + (mm * mI), 3)
                                gCMotion.SyncParameter(stageNo).SetVelLow(mVelLow)
                                gCMotion.SyncParameter(stageNo).SetVelHigh(mVelHigh)
                                'Debug.Print("Path(Dot): " & gCMotion.SyncParameter(stageNo).Cmd & " , " & mVelHigh & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisX) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisY) & " , " & gCMotion.SyncParameter(stageNo).TargetPos(sAxis.AxisZ))
                                If gCMotion.GpAddDotPath(gCMotion.SyncParameter(stageNo), True) <> CommandStatus.Sucessed Then
                                    Return False
                                End If
                                Exit For
                            End If
                        Else
                            Exit For
                        End If
                    Next


                Case ePathRegisterType.Wait
                    '[Note]:若遇到這個就GG了，目前尚沒遇過，尚不知該怎麼處理
                    Return False

            End Select
        End If

        Return True

    End Function

    ''' <summary>
    ''' [塞飛拍的Ticket]
    ''' </summary>
    ''' <param name="stageNo"></param>
    ''' <param name="ccdFixList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetOnFlyTicket(ByVal stageNo As enmStage, ByVal ccdFixList As List(Of sCCDFixPath), ByVal startIndex As Integer) As Boolean

        'If (ccdFixList.Count) <> (endIndex - startIndex + 1) Then
        '    Return False
        'End If
        Dim mTicket As Integer = startIndex
        For FixCount As Integer = 0 To ccdFixList.Count - 1
            Select Case ccdFixList(stageNo).AlignIndex
                Case eAlignIndex.No1
                    gStageMap(stageNo).Node(ccdFixList(FixCount).NodeName).SRecipePos(ccdFixList(FixCount).IndexX, ccdFixList(FixCount).IndexY).Ticket = mTicket

                Case eAlignIndex.No2
                    gStageMap(stageNo).Node(ccdFixList(FixCount).NodeName).SRecipePos(ccdFixList(FixCount).IndexX, ccdFixList(FixCount).IndexY).Ticket2 = mTicket

                Case eAlignIndex.No3
                    gStageMap(stageNo).Node(ccdFixList(FixCount).NodeName).SRecipePos(ccdFixList(FixCount).IndexX, ccdFixList(FixCount).IndexY).Ticket3 = mTicket

                Case eAlignIndex.SkipMark
                    gStageMap(stageNo).Node(ccdFixList(FixCount).NodeName).SRecipePos(ccdFixList(FixCount).IndexX, ccdFixList(FixCount).IndexY).SkipMarkTicket = mTicket
            End Select
            mTicket = mTicket + 1
        Next


        'For FixCount As Integer = startIndex To endIndex
        '    Select Case ccdFixList(stageNo).AlignIndex
        '        Case eAlignIndex.No1
        '            gStageMap(stageNo).Node(ccdFixList(FixCount).NodeName).SRecipePos(ccdFixList(FixCount).IndexX, ccdFixList(FixCount).IndexY).Ticket = FixCount

        '        Case eAlignIndex.No2
        '            gStageMap(stageNo).Node(ccdFixList(FixCount).NodeName).SRecipePos(ccdFixList(FixCount).IndexX, ccdFixList(FixCount).IndexY).Ticket2 = FixCount

        '        Case eAlignIndex.No3
        '            gStageMap(stageNo).Node(ccdFixList(FixCount).NodeName).SRecipePos(ccdFixList(FixCount).IndexX, ccdFixList(FixCount - startIndex).IndexY).Ticket3 = FixCount

        '        Case eAlignIndex.SkipMark
        '            gStageMap(stageNo).Node(ccdFixList(FixCount).NodeName).SRecipePos(ccdFixList(FixCount).IndexX, ccdFixList(FixCount - startIndex).IndexY).SkipMarkTicket = FixCount
        '    End Select
        'Next

        Return True
    End Function



    ''' <summary>[On Fly Model定位流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Private Sub CCDFixOnFlyModelAction(ByRef sys As sCCDFixSysParam)
        Static mTransmissionResumingStopWatch(enmStage.Max) As Stopwatch        '[記錄資料續傳的時間]
        Static mTransmissionResumingStartTime(enmStage.Max) As Long             '[記錄資料續傳的時間]
        Static mTriggerIOStopWatch(enmStage.Max) As Stopwatch                   '[紀錄Trigger IO訊號反應時間]
        Static mPosB(enmStage.Max) As Decimal                                   '[座標(B Axis-->Tilt Axis)]
        Static mDebugStopWatch(enmStage.Max) As Stopwatch                       '[抓頓點問題]
        Static mMeasurePitch(enmStage.Max) As Decimal                           '[Measure Picth]
        Static mMeasureLength(enmStage.Max) As Decimal                          '[Measure Length]
        Static mFixCounts(enmStage.Max) As Integer                              '[記錄這一Run飛拍的顆數]
        Static mMaxDispenseSpeed(enmStage.Max) As Decimal                       '[紀錄點膠時的最大速度，用來計算Measure Picth]
        Static mTriggerStableTimeStopWatch(enmStage.Max) As Stopwatch
        Static mScene(enmStage.Max) As String                                   '[場景]
        Static mLightValue1(enmStage.Max) As Integer                            '[暫存光源數值]
        Static mLightValue2(enmStage.Max) As Integer                            '[暫存光源數值]
        Static mLightValue3(enmStage.Max) As Integer                            '[暫存光源數值]
        Static mLightValue4(enmStage.Max) As Integer                            '[暫存光源數值]
        Static mPath(enmStage.Max) As sCCDFixPath                               '[暫存路徑資訊]
        Static mLightCmdFailCount(enmStage.Max) As Integer
        Static mlight(enmStage.Max) As enmLight                                 '[光源]
        Static m1stMotionPath(enmStage.Max) As sPatternPath                     '[暫存第一筆路徑資訊]
        Static mCCDChangeModeStopWatch(enmStage.Max) As Stopwatch               '[紀錄CCD換Mode反應時間]


        Const mTransmissionResumingTime As Decimal = 100                        '[續傳檢查約100ms]
        Const mTriggerIOTime As Decimal = 3                                     '[Trigger IO訊號約3ms]

        Dim mI(enmStage.Max) As Integer
        Dim mJ(enmStage.Max) As Integer
        Dim mAxisXState(enmStage.Max) As CommandStatus                          '[X軸的狀態]
        Dim mAxisYState(enmStage.Max) As CommandStatus                          '[Y軸的狀態]
        Dim mAxisZState(enmStage.Max) As CommandStatus                          '[Z軸的狀態]
        Dim mAxisBState(enmStage.Max) As CommandStatus                          '[Tilt軸的狀態]
        Dim mCommandPosB(enmStage.Max) As Decimal                               '[取出目前Tilt角度(Command Pos)]
        Dim mTiggerDelayTime(enmStage.Max) As Decimal
        Dim mLightValue As Integer
        Static mTriggerCounter(enmStage.Max) As Integer

        Dim mAcc(enmStage.Max) As Decimal
        Dim mDec(enmStage.Max) As Decimal


        Select Case sys.SysNum
            Case sCCDFixSysParam.SysLoopStart
                If IsNothing(mTriggerStableTimeStopWatch(sys.StageNo)) Then
                    mTriggerStableTimeStopWatch(sys.StageNo) = New Stopwatch
                End If

                '[Note]:清空暫存座標
                Call gMotionPathList(sys.StageNo).Clear()

                If mTransmissionResumingStopWatch(sys.StageNo) Is Nothing Then
                    mTransmissionResumingStopWatch(sys.StageNo) = New Stopwatch
                End If

                If mTriggerIOStopWatch(sys.StageNo) Is Nothing Then
                    mTriggerIOStopWatch(sys.StageNo) = New Stopwatch
                End If

                If mDebugStopWatch(sys.StageNo) Is Nothing Then
                    mDebugStopWatch(sys.StageNo) = New Stopwatch
                End If

                If mCCDChangeModeStopWatch(sys.StageNo) Is Nothing Then
                    mCCDChangeModeStopWatch(sys.StageNo) = New Stopwatch
                End If


                '[Note]:
                If sys.IsResetLightValue = True Then
                    mLightValue1(sys.StageNo) = 0
                    mLightValue2(sys.StageNo) = 0
                    mLightValue3(sys.StageNo) = 0
                    mLightValue4(sys.StageNo) = 0
                    mScene(sys.StageNo) = ""
                End If

                '[Note]:取定位起點座標
                mPath(sys.StageNo) = sys.CCDFixPathRegister(0)

                '[Note]:CCD模式切到硬體觸發->提早至定位流程一開始
                'gAOICollection.SetLiveTriggerMode(sys.CCDNo, eTriggerType.HardwareTrigger)
                mCCDChangeModeStopWatch(sys.StageNo).Start()
                '[Note]:CCDonFlyTrigger Off
                Call SetCCDonFlyTrigger(sys.CCDNo, enmONOFF.eOff)
                sys.SysNum = 1100

            Case 1100
                '[Note]:         檢查取像是否完成, 可以移至下個取像位置(基本上不需要檢查, 因為進來時候就必須是可以取像的狀態)
                If gAOICollection.IsCCDReady(sys.CCDNo) = True Then
                    '[Note]WENDATODO 取像逾時判斷
                    sys.SysNum = 1200
                End If

            Case 1200
                '[Note]:切換場景
                '[Note]:場景不同時才做切換
                '[Note]:取場景名稱
                If mScene(sys.StageNo) <> mPath(sys.StageNo).AlignScene Then
                    mScene(sys.StageNo) = mPath(sys.StageNo).AlignScene
                    If Not gAOICollection.SceneDictionary.ContainsKey(mScene(sys.StageNo)) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2012107", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2012107), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2012407", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2012407), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2012707", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2012707), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2013007", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2013007), eMessageLevel.Alarm)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Call gAOICollection.SetCCDScene(sys.CCDNo, mScene(sys.StageNo))
                    End If
                End If
                sys.SysNum = 1300

            Case 1300
                '[Note]:         光源切換()
                '[Note]:總共四組光源(一個Stage)，光源開啟後暫無確認機制，若後續發現打光時間不夠，再補打光時間
                '[Note]:         第一組光源()
                mlight(sys.StageNo) = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
                Call gSysAdapter.SetLightOnOff(mlight(sys.StageNo), gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No1))
                If gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No1) = True Then
                    mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No1)
                    If mLightValue1(sys.StageNo) <> mLightValue Then
                        mLightCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 1400
                    Else
                        sys.SysNum = 1600
                    End If
                Else
                    sys.SysNum = 1600
                End If

            Case 1400
                '[Note]:         光源設定成功()
                mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No1)
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = False Then
                    If gLightCollection.SetCCDLight(sys.CCDNo, mlight(sys.StageNo), mLightValue) = True Then
                        sys.SysNum = 1500
                    End If
                End If

            Case 1500
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gLightCollection.IsTimeOut(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1400
                        End If
                    End If
                Else
                    '[Note]:             檢查接收資料()
                    If gLightCollection.GetResultStatus(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        mLightValue1(sys.StageNo) = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No1)
                        sys.SysNum = 1600
                    Else
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1400
                        End If
                    End If
                End If

            Case 1600
                '[Note]:         第二組光源()
                mlight(sys.StageNo) = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
                Call gSysAdapter.SetLightOnOff(mlight(sys.StageNo), gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No2))
                If gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No2) = True Then
                    mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No2)
                    If mLightValue2(sys.StageNo) <> mLightValue Then
                        sys.SysNum = 1700
                    Else
                        sys.SysNum = 1900
                    End If
                Else
                    sys.SysNum = 1900
                End If

            Case 1700
                mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No2)
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = False Then
                    If gLightCollection.SetCCDLight(sys.CCDNo, mlight(sys.StageNo), mLightValue) = True Then
                        sys.SysNum = 1800
                    End If
                End If

            Case 1800
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gLightCollection.IsTimeOut(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1700
                        End If
                    End If
                Else
                    '[Note]:             檢查接收資料()
                    If gLightCollection.GetResultStatus(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        mLightValue1(sys.StageNo) = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No2)
                        sys.SysNum = 1900
                    Else
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            ' TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1700
                        End If
                    End If
                End If

            Case 1900
                '[Note]:         第三組光源()
                mlight(sys.StageNo) = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3)
                Call gSysAdapter.SetLightOnOff(mlight(sys.StageNo), gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No3))
                If gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No3) = True Then
                    mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No3)
                    If mLightValue3(sys.StageNo) <> mLightValue Then
                        sys.SysNum = 2000
                    Else
                        sys.SysNum = 3000
                    End If
                Else
                    sys.SysNum = 3000
                End If

            Case 2000
                mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No3)
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = False Then
                    If gLightCollection.SetCCDLight(sys.CCDNo, mlight(sys.StageNo), mLightValue) = True Then
                        sys.SysNum = 2100
                    End If
                End If

            Case 2100
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gLightCollection.IsTimeOut(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 2000
                        End If
                    End If
                Else
                    '[Note]:             檢查接收資料()
                    If gLightCollection.GetResultStatus(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        mLightValue1(sys.StageNo) = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No3)
                        sys.SysNum = 2200
                    Else
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 2000
                        End If
                    End If
                End If

            Case 2200
                '[Note]:         第四組光源()
                mlight(sys.StageNo) = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4)
                Call gSysAdapter.SetLightOnOff(mlight(sys.StageNo), gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No4))
                If gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightEnable(enmValveLight.No4) = True Then
                    mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No4)
                    If mLightValue4(sys.StageNo) <> mLightValue Then
                        sys.SysNum = 2300
                    Else
                        sys.SysNum = 4000
                    End If
                Else
                    sys.SysNum = 4000
                End If

            Case 2300
                mLightValue = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No4)
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = False Then
                    If gLightCollection.SetCCDLight(sys.CCDNo, mlight(sys.StageNo), mLightValue) = True Then
                        sys.SysNum = 2400
                    End If
                End If

            Case 2400
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gLightCollection.IsBusy(sys.CCDNo, mlight(sys.StageNo)) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gLightCollection.IsTimeOut(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 2300
                        End If
                    End If
                Else
                    '[Note]:             檢查接收資料()
                    If gLightCollection.GetResultStatus(sys.CCDNo, mlight(sys.StageNo)) = True Then
                        mLightValue1(sys.StageNo) = gAOICollection.SceneDictionary(mScene(sys.StageNo)).LightValue(enmValveLight.No4)
                        sys.SysNum = 3000
                    Else
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLightCmdFailCount(sys.StageNo) = mLightCmdFailCount(sys.StageNo) + 1
                        If mLightCmdFailCount(sys.StageNo) > gLightCmdMaxFailCounts Then
                            'TODO:異常訊息(Light Fail)
                            gEqpMsg.AddHistoryAlarm("Error_1022003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1022003), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 2300
                        End If
                    End If
                End If

            Case 3000
                '[Note]:路徑串接(Motion)
                mDebugStopWatch(sys.StageNo).Restart()
                'mFixCount(sys.StageNo) = sys.CCDFixPathRegister.Count '[Note]有幾筆飛拍資料
                Call EditMotionPath(sys.CCDFixParam, sys.CCDOnFlyPathRegister, mMotionDispPathRegister(sys.StageNo), m1stMotionPath(sys.StageNo), mMaxExtendDistance(sys.StageNo), mMaxBlendTime(sys.StageNo), True)
                sys.SysNum = 3100

            Case 3100
                '[Note]:轉成終端Motion要的資訊
                Call MotionPathCoversion(sys.CCDFixParam.VelHigh, sys.CCDFixParam.Acc, sys.CCDFixParam.Dec, mMaxExtendDistance(sys.StageNo), mMaxBlendTime(sys.StageNo), mMotionDispPathRegister(sys.StageNo), gMotionPathList(sys.StageNo), m1stPath(sys.StageNo), mlastPath(sys.StageNo), mMaxDispenseSpeed(sys.StageNo))
                sys.SysNum = 3200


            Case 3200
                '[說明]:載入移動-->移動到起始座標
                Dim mVelocity As Decimal = gCMotion.AxisParameter(sys.AxisX).Velocity.MaxVel

                If mGpSCurve = eCurveMode.SCurve Then
                    mAcc(sys.StageNo) = gCMotion.AxisParameter(sys.AxisX).Velocity.Acc * gCMotion.AxisParameter(sys.AxisX).Velocity.AccRatio * mSCurveRatio
                    mDec(sys.StageNo) = gCMotion.AxisParameter(sys.AxisX).Velocity.Dec * gCMotion.AxisParameter(sys.AxisX).Velocity.DecRatio * mSCurveRatio
                Else
                    mAcc(sys.StageNo) = gCMotion.AxisParameter(sys.AxisX).Velocity.Acc * gCMotion.AxisParameter(sys.AxisX).Velocity.AccRatio
                    mDec(sys.StageNo) = gCMotion.AxisParameter(sys.AxisX).Velocity.Dec * gCMotion.AxisParameter(sys.AxisX).Velocity.DecRatio
                End If


                With gCMotion.AxisParameter(sys.AxisX).Velocity
                    Dim mDeltaX As Decimal
                    Dim mDeltaY As Decimal
                    mDeltaX = m1stPath(sys.StageNo).PosX - gCMotion.GetPositionValue(sys.AxisX)
                    mDeltaY = m1stPath(sys.StageNo).PosY - gCMotion.GetPositionValue(sys.AxisY)

                    Dim mDistance As Decimal = Math.Sqrt(mDeltaX ^ 2 + mDeltaY ^ 2)

                    Premtek.CDispensingMath.GetCrossVelocity(gSSystemParameter.MaxCrossDeviceVelocity, mAcc(sys.StageNo), mDec(sys.StageNo), mDistance, 0, mVelocity)
                    'CDispensingMath.GetCrossVelocity(gSSystemParameter.MaxCrossDeviceVelocity, mAcc(sys.StageNo), mDec(sys.StageNo), mDistance, gSSystemParameter.CrossVerticalTime, mVelocity)
                    If mVelocity = 0 Then
                        mVelocity = gSSystemParameter.MaxCrossDeviceVelocity
                    End If
                    gCMotion.SyncParameter(sys.StageNo).TargetPos(0) = m1stPath(sys.StageNo).PosX
                    gCMotion.SyncParameter(sys.StageNo).TargetPos(1) = m1stPath(sys.StageNo).PosY
                    gCMotion.SyncParameter(sys.StageNo).TargetPos(2) = gCMotion.GetPositionValue(sys.AxisZ)
                    gCMotion.SyncParameter(sys.StageNo).Velocity.VelLow = 0
                    gCMotion.SyncParameter(sys.StageNo).Velocity.VelHigh = mVelocity
                    'Debug.Print("抑振速度:" & mVelocity)
                    'Debug.Print("平均加速度:" & mAcc(sys.StageNo) & "平均減速度:" & mDec(sys.StageNo))
                End With
                If gCMotion.GpSetVelLow(gCMotion.SyncParameter(sys.StageNo), 0) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036014", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036014), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048014", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048014), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066014", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066014), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073014", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073014), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetVelHigh(gCMotion.SyncParameter(sys.StageNo), mVelocity) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036013", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036013), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048013", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048013), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066013", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066013), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073013", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073013), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetAcc(gCMotion.SyncParameter(sys.StageNo), mAcc(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036011", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036011), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048011", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048011), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066011", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066011), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073011", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073011), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetDec(gCMotion.SyncParameter(sys.StageNo), mDec(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036012", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036012), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048012", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048012), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066012", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066012), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073012", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073012), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.GpSetCurve(gCMotion.SyncParameter(sys.StageNo), mGpSCurve) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036010", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036010), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048010", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048010), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066010", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066010), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073010", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073010), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.GpMoveLinearAbsXYZ(gCMotion.SyncParameter(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                'ReviseVelocity(sys.AxisX, m1stPath(sys.StageNo).PosX, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                'If gCMotion.AbsMove(sys.AxisX, m1stPath(sys.StageNo).PosX) <> CommandStatus.Sucessed Then
                '    Select Case sys.StageNo
                '        Case 0
                '            gEqpMsg.AddHistoryAlarm("Error_1030000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                '        Case 1
                '            gEqpMsg.AddHistoryAlarm("Error_1042000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                '        Case 2
                '            gEqpMsg.AddHistoryAlarm("Error_1060000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                '        Case 3
                '            gEqpMsg.AddHistoryAlarm("Error_1067000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                '    End Select
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If
                'ReviseVelocity(sys.AxisY, m1stPath(sys.StageNo).PosY, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                'If gCMotion.AbsMove(sys.AxisY, m1stPath(sys.StageNo).PosY) <> CommandStatus.Sucessed Then
                '    Select Case sys.StageNo
                '        Case 0
                '            gEqpMsg.AddHistoryAlarm("Error_1031000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                '        Case 1
                '            gEqpMsg.AddHistoryAlarm("Error_1043000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                '        Case 2
                '            gEqpMsg.AddHistoryAlarm("Error_1061000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                '        Case 3
                '            gEqpMsg.AddHistoryAlarm("Error_1068000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                '    End Select
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If

                sys.SysNum = 3210

            Case 3210
                '[說明]:等待到位
                If gCMotion.GpMoveDone(gCMotion.SyncParameter(sys.StageNo)) = CommandStatus.Sucessed Then
                    sys.SysNum = 3220
                End If
                'mAxisXState(sys.StageNo) = gCMotion.MotionDone(sys.AxisX)
                'mAxisYState(sys.StageNo) = gCMotion.MotionDone(sys.AxisY)

                'If mAxisXState(sys.StageNo) <> CommandStatus.Sucessed Then
                '    If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                '        Select Case sys.StageNo
                '            Case 0
                '                gEqpMsg.AddHistoryAlarm("Error_1030004", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                '            Case 1
                '                gEqpMsg.AddHistoryAlarm("Error_1042004", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                '            Case 2
                '                gEqpMsg.AddHistoryAlarm("Error_1060004", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                '            Case 3
                '                gEqpMsg.AddHistoryAlarm("Error_1067004", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
                '        End Select
                '        sys.RunStatus = enmRunStatus.Alarm
                '        Exit Sub
                '    Else
                '        Exit Sub
                '    End If
                'End If
                'If mAxisYState(sys.StageNo) <> CommandStatus.Sucessed Then
                '    If gCMotion.IsMoveTimeOut(sys.AxisY) Then
                '        Select Case sys.StageNo
                '            Case 0
                '                gEqpMsg.AddHistoryAlarm("Error_1031004", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                '            Case 1
                '                gEqpMsg.AddHistoryAlarm("Error_1043004", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                '            Case 2
                '                gEqpMsg.AddHistoryAlarm("Error_1061004", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                '            Case 3
                '                gEqpMsg.AddHistoryAlarm("Error_1068004", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                '        End Select
                '        sys.RunStatus = enmRunStatus.Alarm
                '        Exit Sub
                '    Else
                '        Exit Sub
                '    End If
                'End If
                'sys.SysNum = 3220

            Case 3220
                'Z軸移動
                If gCMotion.AbsMove(sys.AxisZ, m1stPath(sys.StageNo).PosZ) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                Else
                    sys.SysNum = 3230
                End If


            Case 3230
                '[說明]:等待Z軸到位
                mAxisZState(sys.StageNo) = gCMotion.MotionDone(sys.AxisZ)

                If mAxisZState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032004", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044004", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062004", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069004", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                Else
                    sys.SysNum = 3300
                End If

            Case 3300
                '[Note]:路徑串接(Trigger Board)
                mTriggerFixPathIndex(sys.StageNo).Start = 0
                mTriggerFixPathIndex(sys.StageNo).Done = -1
                mDispType(sys.StageNo) = enmTriggerDispType.VisionRecipe
                Call EstimateTriggerStep(mTriggerFixPathIndex(sys.StageNo).Start, sys.TriggerBoardPathRegister, mTriggerFixPathIndex(sys.StageNo).Ending, False)
                Call EditTriggerPathByVisionCmd(sys.StageNo, False, mTriggerFixPathIndex(sys.StageNo).Start, mTriggerFixPathIndex(sys.StageNo).Ending, mDispType(sys.StageNo), m1stMotionPath(sys.StageNo), sys.TriggerBoardPathRegister, mFixCounts(sys.StageNo))
                sys.SysNum = 3400

            Case 3400
                '[Note]:(Step1.)Reset Alarm
                '               Check trigger board is ready before send command
                If IsTriggerBoardAlarm(sys.StageNo) = True Then
                    If IsTriggerBoardReady(sys.StageNo) = True Then
                        If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                            If gTriggerBoard.SetResetAlarm(sys.StageNo, False) = True Then
                                sys.SysNum = 3500
                            End If
                        End If
                    End If
                Else
                    sys.SysNum = 3600
                End If

            Case 3500
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
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016001", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016101", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016201", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016301", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 3400
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gTriggerBoard.ResetAlarm(sys.StageNo).Status = True Then
                        mTriggerCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 3600
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("ResetAlarm: " & gTriggerBoard.ResetAlarm(sys.StageNo).STR)
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016001", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016101", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016201", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016200), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016301", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 3400
                        End If
                    End If
                End If

            Case 3600
                '[Note]:判斷是否需要傳送Trigger cmd
                'If IsPassTriggerCmd(mTriggerFixPathIndex(sys.StageNo).Start, mDispType(sys.StageNo), mTriggerDispPathRegister(sys.StageNo), mFixCount(sys.StageNo), mTriggerPathMenory(sys.StageNo), gIsOnPurge(sys.StageNo)) = True Then
                '    '[Note]:已完成了
                '    mTriggerFixPathIndex(sys.StageNo).Done = mTriggerFixPathIndex(sys.StageNo).Ending
                '    sys.SysNum = 5100
                'Else
                sys.SysNum = 3700
                'End If

            Case 3700
                '[Note]:取出valve parameter
                'TODO:後續再追加自動估算(MeasureLength、MeasurePitch、MeasureCounts)
                'If gJetValveDB.ContainsKey(sys.DispParam.Recipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)) = True Then
                '    With gJetValveDB(sys.DispParam.Recipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch
                '        mTiggerDelayTime(sys.StageNo) = .PulseTime + .JetTime + .DelayTime
                '    End With
                Call EstimateMeasurePitchLength(mMaxDispenseSpeed(sys.StageNo), mTiggerDelayTime(sys.StageNo), mMeasurePitch(sys.StageNo), mMeasureLength(sys.StageNo))
                With mTriggerGCmdParameter(sys.StageNo)
                    .HeadNo = 0
                    .PulseTime = 1000
                    .JetTime = 300
                    .Stroke = 100             '[要轉換成百分比]
                    .GluePressure = 0 'gCRecipe.StageParts(sys.StageNo).SyringPressure(sys.SelectValve)
                    .Tolerance = mMeasureLength(sys.StageNo)
                    .MeasureLength = mMeasureLength(sys.StageNo)
                    .MeasurePitch = mMeasurePitch(sys.StageNo)
                    '[Note]Non-history mode measure counts為 0
                    '          History mode measure counts不為0
                    .MeasureCounts = 0
                    .JetPressure = 0        '[視硬體有無決定]
                End With
                'Else
                ''[Note]:若有表示資料異常，待查
                ''TODO:異常訊息(Jet valve DataBase Fail)
                'gEqpMsg.AddHistoryAlarm("Error_1019003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1019003), eMessageLevel.Error)
                'sys.RunStatus = enmRunStatus.Alarm
                'Exit Sub
                'End If
                mTriggerCmdFailCount(sys.StageNo) = 0
                sys.SysNum = 3800

            Case 3800
                '[Note]:(Step2.)Send G Cmd
                '               Check trigger board is ready before send command
                If IsTriggerBoardReady(sys.StageNo) = True Then
                    If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                        If gTriggerBoard.SetJetParameter(sys.StageNo, mTriggerGCmdParameter(sys.StageNo), False) = True Then
                            sys.SysNum = 3900
                        End If
                    End If
                End If

            Case 3900
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                    '[Note]:還在接收傳送資料中
                    If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1016003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1016103", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1016203", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1016303", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 3800
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gTriggerBoard.JetParameter(sys.StageNo).Status = True Then
                        mTriggerCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 4000
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("JetParameter(G Cmd): " & gTriggerBoard.JetParameter(sys.StageNo).STR)
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1016003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1016103", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1016203", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1016303", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 3800
                        End If
                    End If
                End If

            Case 4000
                '[Note]:(Step3.)Send L Cmd
                '               Check trigger board is ready before send command
                If IsTriggerBoardReady(sys.StageNo) = True Then
                    If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                        If gTriggerBoard.SetVisionRecipe(sys.StageNo, False) = True Then
                            '[Note]:已完成了
                            mTriggerFixPathIndex(sys.StageNo).Done = mTriggerFixPathIndex(sys.StageNo).Ending
                            '[Note]:下個從哪裡開始做
                            mTriggerFixPathIndex(sys.StageNo).Start = mTriggerFixPathIndex(sys.StageNo).Ending + 1
                            sys.SysNum = 4100
                        End If
                    End If
                End If

            Case 4100
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                    '[Note]:還在接收傳送資料中
                    If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Set Jet Recipe Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1016003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1016103", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1016203", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1016303", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 4000
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gTriggerBoard.VisionRecipe(sys.StageNo).Status = True Then
                        mTriggerCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 4200
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("VisionRecipe(L Cmd): " & gTriggerBoard.VisionRecipe(sys.StageNo).STR)
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Set Jet Recipe Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1016003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1016103", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1016203", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1016303", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 4000
                        End If
                    End If
                End If

            Case 4200
                '[Note]:(Step4.)Send D Cmd
                '               Check trigger board is ready before send command
                If IsTriggerBoardReady(sys.StageNo) = True Then
                    If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                        mDispType(sys.StageNo) = enmTriggerDispType.VisionRecipe
                        If gTriggerBoard.SetDummyRun(sys.StageNo, mDispType(sys.StageNo), enmValve.No1, 0, False) = True Then
                            sys.SysNum = 4300
                        End If
                    End If
                End If

            Case 4300
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                    '[Note]:還在接收傳送資料中
                    If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Set Dummy Run Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1016003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1016103", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1016203", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1016303", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 4200
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gTriggerBoard.DummyRun(sys.StageNo).Status = True Then
                        mTriggerCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 4400
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("DummyRun(D Cmd): " & gTriggerBoard.DummyRun(sys.StageNo).STR)
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Set Dummy Run Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1016003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1016103", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1016203", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1016303", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 4200
                        End If
                    End If
                End If

            Case 4400
                '[Note]:(Step5.)Send X Cmd
                If IsTriggerBoardReady(sys.StageNo) = True Then
                    If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                        mDispType(sys.StageNo) = enmTriggerDispType.VisionRecipe
                        If gTriggerBoard.SetDispenseRun(sys.StageNo, mDispType(sys.StageNo), enmValve.No1, 0, 0, False) = True Then
                            sys.SysNum = 4500
                        End If
                    End If
                End If

            Case 4500
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                    '[Note]:還在接收傳送資料中
                    If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Set Dispense Run Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1016003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1016103", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1016203", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1016303", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 4400
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gTriggerBoard.DispenseRun(sys.StageNo).Status = True Then
                        mTriggerCmdFailCount(sys.StageNo) = 0
                        '   mComputerStopWatch.Restart()
                        ' Debug.Print("Computer Time(EditTriggerPath-->Trigger Board Cmd End): " & mComputerStopWatch(sys.StageNo).ElapsedMilliseconds)
                        sys.SysNum = 4600
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("DispenseRun(X Cmd): " & gTriggerBoard.DispenseRun(sys.StageNo).STR)
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Set Dispense Run Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1016003", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1016103", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1016203", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1016303", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 4400
                        End If
                    End If
                End If

            Case 4600
                '[Note]:Need to check motion done & 將路徑塞給軸卡
                If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                    mTriggerStableTimeStopWatch(sys.StageNo).Restart()
                    If SendPathToMotion(sys.StageNo, gMotionPathList(sys.StageNo), mMaxExtendDistance(sys.StageNo), mMaxBlendTime(sys.StageNo)) = True Then
                        sys.SysNum = 5000
                    Else
                        'TODO:異常訊息(路徑串接異常)
                        gEqpMsg.AddHistoryAlarm("Error_1000012", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000012), eMessageLevel.Error)
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                End If

            Case 5000
                '[Note]:等待整定時間
                If mTriggerStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds > gSSystemParameter.StableTime.CCDStableTime Then
                    mTriggerStableTimeStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 5100
                End If

            Case 5100
                '[Note]:(Step6.)CCDonFlyTrigger On
                If mCCDChangeModeStopWatch(sys.StageNo).ElapsedMilliseconds > gSSystemParameter.StableTime.CCDChangeModeStableTime Then
                    sys.IsCanPause = False
                    Call SetCCDonFlyTrigger(sys.CCDNo, enmONOFF.eON)
                    mTriggerStableTimeStopWatch(sys.StageNo).Restart()
                    'Debug.Print("CCDonFlyTrigger time:" & mCCDStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds)
                    sys.SysNum = 5200
                End If

            Case 5200
                '[Note]:(Step7.)移動
                If gCMotion.GpMoving(gCMotion.SyncParameter(sys.StageNo)) <> CommandStatus.Sucessed Then
                    'TODO:異常訊息(路徑移動異常)
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073000", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                Else
                    '  Debug.Print("FunctionCCDFix Computer Time(EditTriggerPath-->Trigger Board Cmd End): " & mComputerStopWatch.ElapsedMilliseconds)
                    sys.SysNum = 5500
                End If

            Case 5500
                '[Note]:判斷已經將所有Trigger路徑資料
                '[Note]:判斷已經做完此Round的所有DispList
                If mTriggerFixPathIndex(sys.StageNo).Done = sys.TriggerBoardPathRegister.Count - 1 Then
                    '[Note]:全部都丟完了
                    sys.SysNum = 6000
                Else
                    '[Note]:路徑串接(Trigger Board)-->L Cmd
                    mDispType(sys.StageNo) = enmTriggerDispType.VisionRecipe
                    Call EstimateTriggerStep(mTriggerFixPathIndex(sys.StageNo).Start, sys.TriggerBoardPathRegister, mTriggerFixPathIndex(sys.StageNo).Ending, False)
                    Call EditTriggerPathByVisionCmd(sys.StageNo, False, mTriggerFixPathIndex(sys.StageNo).Start, mTriggerFixPathIndex(sys.StageNo).Ending, mDispType(sys.StageNo), m1stMotionPath(sys.StageNo), sys.TriggerBoardPathRegister)
                    sys.SysNum = 5600
                End If

            Case 5600
                '[Note]:(Step9.)Send L Cmd(資料續傳)
                '               Check Trigger Board I/O is Busy
                If IsTriggerBoardReady(sys.StageNo) = True Then
                    If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                        If gTriggerBoard.SetJetRecipeByTransmissionResuming(sys.StageNo, False) = True Then
                            '[Note]:已完成了
                            mTriggerFixPathIndex(sys.StageNo).Done = mTriggerFixPathIndex(sys.StageNo).Ending
                            '[Note]:下個從哪裡開始做
                            mTriggerFixPathIndex(sys.StageNo).Start = mTriggerFixPathIndex(sys.StageNo).Ending + 1
                            mTransmissionResumingStopWatch(sys.StageNo).Restart()
                            mTransmissionResumingStartTime(sys.StageNo) = mTransmissionResumingStopWatch(sys.StageNo).ElapsedMilliseconds
                            sys.SysNum = 5700

                        End If
                    End If
                End If

            Case 5700
                If Math.Abs(mTransmissionResumingStartTime(sys.StageNo) - mTransmissionResumingStopWatch(sys.StageNo).ElapsedMilliseconds) > mTransmissionResumingTime Then
                    mTransmissionResumingStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 5500
                End If

            Case 6000
                '[Note]:(Step10.)等待移動停止
                If gCMotion.GpMoveDone(gCMotion.SyncParameter(sys.StageNo)) = CommandStatus.Sucessed Then
                    If mTriggerStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds > gSSystemParameter.StableTime.TriggerBoardIOStableTime Then
                        '[Note]:SetCCDonFlyTrigger Off
                        sys.IsCanPause = True
                        '[Note]CCD模式切回軟體觸發
                        'gAOICollection.SetLiveTriggerMode(sys.CCDNo, eTriggerType.SoftwareTrigger)
                        Call SetCCDonFlyTrigger(sys.CCDNo, enmONOFF.eOff)
                        mTriggerIOStopWatch(sys.StageNo).Restart()
                        sys.SysNum = 6400
                    End If
                End If

            Case 6400
                '[Note]:等待Trigger I/O訊號反應時間
                If mTriggerIOStopWatch(sys.StageNo).ElapsedMilliseconds > mTriggerIOTime Then
                    mTriggerIOStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 6500
                End If

            Case 6500
                '[Note]:檢查中途有無資料傳送異常
                If IsTriggerBoardAlarm(sys.StageNo) = True Then
                    '[Note]:資料有問題
                    Dim mErrorCode As String
                    mErrorCode = ""
                    If gTriggerBoard.GetErrorCode(sys.StageNo, True, mErrorCode) = True Then

                    End If
                    Debug.Print("Error Code: " & mErrorCode)
                    'TODO:異常訊息(Trigger Board Alarm)
                    Dim errorCodeTrigger As String
                    errorCodeTrigger = ""
                    errorCodeTrigger = trigger_error(sys.StageNo, mErrorCode)

                    gEqpMsg.AddHistoryAlarm("Alarm_" & errorCodeTrigger, "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(errorCodeTrigger), eMessageLevel.Alarm)
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                Else
                    sys.SysNum = 6600
                End If

            Case 6600
                '[Note]:檢查路徑狀態
                Dim mRemainCount As Long
                If gCMotion.GpGetPathStatus(gCMotion.SyncParameter(sys.StageNo), mRemainCount) = CommandStatus.Sucessed Then
                    sys.SysNum = 7000
                Else
                    'TODO:異常訊息(路徑狀態異常)
                    gEqpMsg.AddHistoryAlarm("Error_1000012", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000012), eMessageLevel.Alarm)
                End If

                sys.SysNum = 7000
            Case 7000
                '[Note]:         塞對應的Ticket數值(藉由SetCCDTrigger去撈對應的Ticket, OFF的時候去撈的話, 是目前最後一個Index)
                'SetCCDTrigger回傳的數值表示目前正準備接收第個影像，取完像之後數值才會加1。可參考CThreadCogAcqFifoTool的ToolBlock_Run
                If SetOnFlyTicket(sys.StageNo, sys.CCDFixPathRegister, sys.StartIndex) = False Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2024004", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2024004), eMessageLevel.Alarm)
                End If
                mTriggerCmdFailCount(sys.StageNo) = 0
                sys.SysNum = 7200

            Case 7200
                '[Note]:取像數量檢查
                '               Check trigger board is ready before send command
                If IsTriggerBoardReady(sys.StageNo) = True Then
                    If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                        If gTriggerBoard.GetVisionCounts(sys.StageNo) = True Then
                            sys.SysNum = 7300
                        End If
                    End If
                End If

            Case 7300
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
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016001", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016101", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016201", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016301", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 7200
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gTriggerBoard.VisionCounts(sys.StageNo).Status = True Then
                        mTriggerCounter(sys.StageNo) = gTriggerBoard.VisionCounts(sys.StageNo).Value
                        mTriggerCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 7400
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("ResetAlarm: " & gTriggerBoard.ResetAlarm(sys.StageNo).STR)
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016001", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016101", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016201", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016301", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 7200
                        End If
                    End If
                End If

            Case 7400
                '[Note]:檢查實際觸發數量是否與資料相同
                If mTriggerCounter(sys.StageNo) <> sys.CCDFixPathRegister.Count Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Alarm_2016023", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016023), eMessageLevel.Alarm)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Alarm_2016123", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016123), eMessageLevel.Alarm)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Alarm_2016223", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016223), eMessageLevel.Alarm)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Alarm_2016323", "CCDFixOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016323), eMessageLevel.Alarm)
                    End Select
                    gSyslog.Save("Cmd Trigger Count: " & sys.CCDFixPathRegister.Count, "", eMessageLevel.Alarm)
                    gSyslog.Save("Real Trigger Count: " & mTriggerCounter(sys.StageNo), "", eMessageLevel.Alarm)
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                Else
                    sys.SysNum = 7500
                End If

            Case 7500
                '[Note]:檢查取像是否完成，可以完成此次的取像任務
                If gAOICollection.IsCCDReady(sys.CCDNo) = True Then
                    sys.SysNum = 9000
                End If
            Case 9000
                'gAOICollection.SetLiveTriggerMode(sys.CCDNo, eTriggerType.SoftwareTrigger) '[Note]移至拍照流程最後處理

                '[Note]:執行CCD拍照取像(並記錄最後的Ticket是多少)
                '[Note]:實際觸發的數量
                'sys.LastTicket = mTriggerCounter(sys.StageNo) - 1
                '[Note]:預期要有的數量
                sys.LastTicket = mFixCounts(sys.StageNo) - 1
                sys.RunStatus = enmRunStatus.Finish
                Exit Sub
                '***********************************************************************************************************************************************************

        End Select

    End Sub

#End Region



End Module

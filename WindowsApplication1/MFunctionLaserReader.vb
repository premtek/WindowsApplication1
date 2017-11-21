Imports ProjectCore
Imports ProjectIO
Imports ProjectRecipe
Imports ProjectMotion
Imports ProjectAOI
Imports ProjectTriggerBoard
Imports System.Math
Imports System.IO
Imports ProjectLaserInterferometer
Imports ProjectConveyor

Module MFunctionLaserReader
    '20160103 test
    Dim mLaserStableTimeStopWatch As New Stopwatch
    ''' <summary>[定位資料接收狀態]</summary>
    ''' <remarks></remarks>


#Region "區域變數"
    Private mLaserProtect(enmStage.Max) As sProtectData                                  '[紀錄各自的相關座標]

#End Region

    ''' <summary>
    ''' [Node內測高關係的清單]
    ''' </summary>
    ''' <remarks></remarks>
    Public LaserRelationshipList(enmStage.Max) As List(Of SNodeHeight)
    ''' <summary>
    ''' [NonArray 用]
    ''' </summary>
    ''' <remarks></remarks>
    Public LaserHeight(enmStage.Max) As List(Of Decimal)

    Public Structure SNodeHeight
        ''' <summary>[Node名稱]</summary>
        ''' <remarks></remarks>
        Public NodeName As String
        ''' <summary>[哪一層的Node]</summary>
        ''' <remarks></remarks>
        Public NodeLevel As Integer
        ''' <summary>[該Node是否省略定位]</summary>
        ''' <remarks></remarks>
        Public IsEnableLaser As Boolean
    End Structure

    ''' <summary>[相關資訊]</summary>
    ''' <remarks></remarks>
    Private Structure sLaserData
        ''' <summary>[陣列(X)]</summary>
        ''' <remarks></remarks>
        Friend IndexX As Integer
        ''' <summary>[陣列(Y)]</summary>
        ''' <remarks></remarks>
        Friend IndexY As Integer
        ''' <summary>[Node名稱]</summary>
        ''' <remarks></remarks>
        Friend NodeName As String
        ''' <summary>[哪一層的Node]</summary>
        ''' <remarks></remarks>
        Friend NodeLevel As Integer
    End Structure


    ''' <summary>[塞路徑規劃]</summary>
    ''' <remarks></remarks>
    Private Structure sLaserPath
        ''' <summary>[定位點座標(X)]</summary>
        ''' <remarks></remarks>
        Friend PosX As Decimal
        ''' <summary>[定位點座標(Y)]</summary>
        ''' <remarks></remarks>
        Friend PosY As Decimal
        ''' <summary>[定位點座標(Z)]</summary>
        ''' <remarks></remarks>
        Friend PosZ As Decimal
        ''' <summary>[陣列(X)]</summary>
        ''' <remarks></remarks>
        Friend IndexX As Integer
        ''' <summary>[陣列(Y)]</summary>
        ''' <remarks></remarks>
        Friend IndexY As Integer
        ''' <summary>[Node名稱]</summary>
        ''' <remarks></remarks>
        Friend NodeName As String
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

    ''' <summary>[相關參數]</summary>
    ''' <remarks></remarks>
    Private Structure sLaserParam
        ''' <summary>[群組加速度]</summary>
        ''' <remarks></remarks>
        Friend Acc As Decimal
        ''' <summary>[群組減速度]</summary>
        ''' <remarks></remarks>
        Friend Dec As Decimal
        ''' <summary>[群組最大速度(用來卡上限用的)]</summary>
        ''' <remarks></remarks>
        Friend VelHigh As Decimal
    End Structure

    ''' <summary>[動作基本參數]</summary>
    ''' <remarks></remarks>
    Private Class sLaserSysParam
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
        ''' <summary>[搭配Laser索引]</summary>
        ''' <remarks></remarks>
        Friend laserNo As Integer
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
        ''' <summary>[時間紀錄]</summary>
        ''' <remarks></remarks>
        Friend Timer As New Stopwatch
        ''' <summary>[定位參數]</summary>
        ''' <remarks></remarks>
        Friend LaserParam As sLaserParam
        ''' <summary>[排列出點膠順序(Path)]</summary>
        ''' <remarks></remarks>
        Friend LaserPathRegister As List(Of sLaserPath)
        ''' <summary>[由流程決定外部是否可暫停]</summary>
        ''' <remarks></remarks>
        Friend IsCanPause As Boolean
        ''' <summary>[測高方式]</summary>
        ''' <remarks></remarks>
        Friend LaserModel As eHeightModel

    End Class

    ''' <summary>[將Node串接起來]</summary>
    ''' <param name="Recipe"></param>
    ''' <param name="StageNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LaserRecipeNodeSort(ByVal recipe As CRecipe, ByVal stageNo As enmStage, ByVal conveyorNo As eConveyor) As Boolean

        Dim mI(enmStage.Max) As Integer
        Dim mNodeName(enmStage.Max) As String
        Dim mNodeAlignScene(enmStage.Max) As SNodeHeight
        Dim mAlignScene(enmStage.Max) As String                   '測高點名稱
        Dim mNodeLevel(enmStage.Max) As Integer


        mNodeLevel(stageNo) = 1
        LaserRelationshipList(stageNo) = New List(Of SNodeHeight)
        LaserRelationshipList(stageNo).Clear()

        For mI(stageNo) = 0 To recipe.LaserTraversal(stageNo).Count - 1
            '[說明]:找出對應的Node 
            mNodeName(stageNo) = recipe.LaserTraversal(stageNo)(mI(stageNo))
            Call GetNodeLevel(mNodeName(stageNo), mNodeLevel(stageNo))

            If recipe.Node(stageNo)(mNodeName(stageNo)).LaserEnable Then
                '[Note]:使用測高資訊

                Select Case gCRecipe.LaserRunMode
                    Case eLaserRunModel.Array
                        mAlignScene(stageNo) = recipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyorNo).LaserData(0).LaserData1
                        With mNodeAlignScene(stageNo)
                            .IsEnableLaser = True
                            .NodeName = mNodeName(stageNo)
                            .NodeLevel = mNodeLevel(stageNo)
                        End With
                        LaserRelationshipList(stageNo).Add(mNodeAlignScene(stageNo))
                        'node name以及level 和array 方式相同
                    Case eLaserRunModel.NonArray
                        mAlignScene(stageNo) = recipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyorNo).LaserData(0).LaserData1
                        With mNodeAlignScene(stageNo)
                            .IsEnableLaser = True
                            .NodeName = mNodeName(stageNo)
                            .NodeLevel = mNodeLevel(stageNo)
                        End With
                        LaserRelationshipList(stageNo).Add(mNodeAlignScene(stageNo))
                End Select

            Else
                '[Note]:省略測高
                With mNodeAlignScene(stageNo)
                    .IsEnableLaser = False
                    .NodeName = mNodeName(stageNo)
                    .NodeLevel = mNodeLevel(stageNo)

                End With
                LaserRelationshipList(stageNo).Add(mNodeAlignScene(stageNo))
            End If
        Next

        Return True

    End Function

    ''' <summary>[判斷是否需要執行測高程序]</summary>
    ''' <param name="recipe"></param>
    ''' <param name="stageNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsNeedLaserProcess(ByVal recipe As CRecipe, ByVal stageNo As Integer) As Boolean

        '[Note]:完全沒有節點，該側根本沒有動作需要執行
        If recipe.Node(stageNo).Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>[列出定位順序]</summary>
    ''' <param name="recipe"></param>
    ''' <param name="stageNo"></param>
    ''' <param name="LaserRelationshipList"></param>
    ''' <param name="LaserList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LaserSortList(ByVal recipe As CRecipe, ByVal stageNo As enmStage, ByVal LaserRelationshipList As SNodeHeight, ByRef LaserList As List(Of sLaserData), ByVal conveyor As eConveyor) As Boolean

        Dim mI(enmStage.Max) As Integer
        Dim mJ(enmStage.Max) As Integer
        Dim mIndex(enmStage.Max) As sLaserData
        Dim mNodeName(enmStage.Max) As String
        Dim mAlignIndex(enmStage.Max) As eAlignIndex
        'Dim mAlignScene(enmStage.Max) As String
        Dim mNodeLevel(enmStage.Max) As Integer

        mNodeName(stageNo) = LaserRelationshipList.NodeName
        mNodeLevel(stageNo) = LaserRelationshipList.NodeLevel

        If mAlignIndex(stageNo) = eAlignIndex.None Then
            '[Note]:就是不需要啦
            Return True
        End If

        If LaserRelationshipList.IsEnableLaser = True Then
Select Case gCRecipe.LaserRunMode
                Case eLaserRunModel.Array
                    'Array 才有分搜尋mode
            Select Case recipe.SearchType
                Case enmSearchType.Y_ZigZag
                    For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                        '[正向]
                        For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                            '[Note]:判斷是否Mapping啟用該Die
                            'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = False Then
                            If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = False Then
                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SLaserValue(mI(stageNo), mJ(stageNo)).LaserFinish(0) = False Then
                                    With mIndex(stageNo)
                                        .IndexX = mI(stageNo)
                                        .IndexY = mJ(stageNo)
                                        .NodeName = mNodeName(stageNo)
                                        .NodeLevel = mNodeLevel(stageNo)
                                    End With
                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                    LaserList.Capacity = LaserList.Count + 1
                                    LaserList.Add(mIndex(stageNo))
                                    Debug.Print("LaserSortList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")")
                                End If
                            End If
                        Next mJ
                    Next mI

                Case enmSearchType.Y_Snake
                    For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                        If mI(stageNo) Mod 2 = 0 Then
                            '[正向]
                            For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                                '[Note]:判斷是否Mapping啟用該Die
                                'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = False Then
                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = False Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SLaserValue(mI(stageNo), mJ(stageNo)).LaserFinish(0) = False Then
                                        With mIndex(stageNo)
                                            .IndexX = mI(stageNo)
                                            .IndexY = mJ(stageNo)
                                            .NodeName = mNodeName(stageNo)
                                            .NodeLevel = mNodeLevel(stageNo)
                                        End With
                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                        LaserList.Capacity = LaserList.Count + 1
                                        LaserList.Add(mIndex(stageNo))
                                        Debug.Print("LaserSortList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")")
                                    End If
                                End If
                            Next mJ
                        Else
                            '[逆向]
                            For mJ(stageNo) = UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2) To 0 Step -1
                                '[Note]:判斷是否Mapping啟用該Die
                                'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = False Then
                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = False Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SLaserValue(mI(stageNo), mJ(stageNo)).LaserFinish(0) = False Then
                                        With mIndex(stageNo)
                                            .IndexX = mI(stageNo)
                                            .IndexY = mJ(stageNo)
                                            .NodeName = mNodeName(stageNo)
                                            .NodeLevel = mNodeLevel(stageNo)
                                        End With
                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                        LaserList.Capacity = LaserList.Count + 1
                                        LaserList.Add(mIndex(stageNo))
                                        Debug.Print("LaserSortList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")")
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
                                '[Note]:判斷是否Mapping啟用該Die
                                'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = False Then
                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = False Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SLaserValue(mI(stageNo), mJ(stageNo)).LaserFinish(0) = False Then
                                        With mIndex(stageNo)
                                            .IndexX = mI(stageNo)
                                            .IndexY = mJ(stageNo)
                                            .NodeName = mNodeName(stageNo)
                                            .NodeLevel = mNodeLevel(stageNo)
                                        End With
                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                        LaserList.Capacity = LaserList.Count + 1
                                        LaserList.Add(mIndex(stageNo))
                                        Debug.Print("LaserSortList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")")
                                    End If
                                End If
                            Next
                        Else
                            '[逆向]
                            For mI(stageNo) = UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1) To 0 Step -1
                                '[Note]:判斷是否Mapping啟用該Die
                                'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = False Then
                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = False Then
                                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SLaserValue(mI(stageNo), mJ(stageNo)).LaserFinish(0) = False Then
                                        With mIndex(stageNo)
                                            .IndexX = mI(stageNo)
                                            .IndexY = mJ(stageNo)
                                            .NodeName = mNodeName(stageNo)
                                            .NodeLevel = mNodeLevel(stageNo)
                                        End With
                                        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                        LaserList.Capacity = LaserList.Count + 1
                                        LaserList.Add(mIndex(stageNo))
                                        Debug.Print("LaserSortList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")")
                                    End If
                                End If
                            Next
                        End If
                    Next

                Case enmSearchType.X_ZigZag
                    For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 2)
                        '[正向]
                        For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData, 1)
                            '[Note]:判斷是否Mapping啟用該Die
                            'If gStageMap(stageNo).Node(mNodeName(stageNo)).SBinMapData(mI(stageNo), mJ(stageNo)).Disable = False And gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = False Then
                            If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mI(stageNo), mJ(stageNo)).IsByPassLaserAction = False Then
                                If gStageMap(stageNo).Node(mNodeName(stageNo)).SLaserValue(mI(stageNo), mJ(stageNo)).LaserFinish(0) = False Then
                                    With mIndex(stageNo)
                                        .IndexX = mI(stageNo)
                                        .IndexY = mJ(stageNo)
                                        .NodeName = mNodeName(stageNo)
                                        .NodeLevel = mNodeLevel(stageNo)
                                    End With
                                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                    LaserList.Capacity = LaserList.Count + 1
                                    LaserList.Add(mIndex(stageNo))
                                    Debug.Print("LaserSortList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")")
                                End If
                            End If
                        Next
                    Next

            End Select
         Case eLaserRunModel.NonArray
                    If gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(0, 0).IsByPassLaserAction = False Then
                        If gStageMap(stageNo).Node(mNodeName(stageNo)).SLaserValue(0, 0).LaserFinish(0) = False Then
                            '有幾個laserdata 就加幾次(放入indexX 為我們參考點)
                            For mI(stageNo) = 0 To gCRecipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyor).LaserData.Count - 1
                                With mIndex(stageNo)
                                    .IndexX = mI(stageNo)
                                    .IndexY = 0
                                    .NodeName = mNodeName(stageNo)
                                    .NodeLevel = mNodeLevel(stageNo)
                                End With
                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                LaserList.Capacity = LaserList.Count + 1
                                LaserList.Add(mIndex(stageNo))
                                Debug.Print("LaserSortList(" & stageNo & "," & mI(stageNo) & "," & mJ(stageNo) & "," & CInt(mAlignIndex(stageNo)) & ")")
                            Next
                        End If
                    End If
            End Select
        Else
            '[Note]:就是不需要啦
            Return True
        End If

        Return True
    End Function

    ''' <summary>[取出測高座標]</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="recipe"></param>
    ''' <param name="LaserData"></param>
    ''' <param name="LaserPos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EstimateLaserPos(ByVal stageNo As Integer, ByVal recipe As CRecipe, ByVal LaserData As sLaserData, ByRef LaserPos As Premtek.sPos, ByVal conveyorNo As eConveyor) As Boolean

        Dim mNodeName(enmStage.Max) As String
        Dim mIndexX(enmStage.Max) As Integer
        Dim mIndexY(enmStage.Max) As Integer

        Dim teachX(enmStage.Max) As Integer
        Dim teachY(enmStage.Max) As Integer


        Select Case gCRecipe.LaserRunMode
            Case eLaserRunModel.Array
                mNodeName(stageNo) = LaserData.NodeName
                mIndexX(stageNo) = LaserData.IndexX
                mIndexY(stageNo) = LaserData.IndexY

                LaserPos.PosX = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).LaserPosX(0)
                LaserPos.PosY = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).LaserPosY(0)
                LaserPos.PosZ = gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(mIndexX(stageNo), mIndexY(stageNo)).LaserPosZ(0)
            Case eLaserRunModel.NonArray
                mNodeName(stageNo) = LaserData.NodeName
                teachX(stageNo) = gCRecipe.Node(stageNo)(LaserData.NodeName).TeachIndexX
                teachY(stageNo) = gCRecipe.Node(stageNo)(LaserData.NodeName).TeachIndexY
                '將indexX當作第幾LaserData
                mIndexX(stageNo) = LaserData.IndexX
                LaserPos.PosX = gCRecipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyorNo).LaserData(mIndexX(stageNo)).LaserPositionX + gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(teachX(stageNo), teachY(stageNo)).CcdOffsetX
                LaserPos.PosY = gCRecipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyorNo).LaserData(mIndexX(stageNo)).LaserPositionY + gStageMap(stageNo).Node(mNodeName(stageNo)).SRecipePos(teachX(stageNo), teachY(stageNo)).CcdOffsetY
                LaserPos.PosZ = gCRecipe.Node(stageNo)(mNodeName(stageNo)).ConveyorPos(conveyorNo).LaserData(mIndexX(stageNo)).LaserPositionZ
        End Select
        Return True

    End Function


    ''' <summary>[將所有的測高資訊先串起來，後面再做運算處理(Motion)]</summary>
    ''' <param name="recipe"></param>
    ''' <param name="stageNo"></param>
    ''' <param name="LaserList"></param>
    ''' <param name="LaserPosRegister"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateLaserPosList(ByVal recipe As CRecipe, ByVal stageNo As enmStage, ByVal LaserList As List(Of sLaserData), ByRef LaserPosRegister As List(Of sLaserPath), ByVal conveyorNo As eConveyor) As Boolean

        Dim mCount(enmStage.Max) As Integer
        Dim mPath(enmStage.Max) As sLaserPath
        Dim mLaserPathRegister(enmStage.Max) As List(Of sLaserPath)
        Dim mLimitPos(enmStage.Max) As Premtek.sPos                                 '[估算極限位置]
        Dim mLaserPosX(enmStage.Max) As Decimal
        Dim mLaserPosY(enmStage.Max) As Decimal
        Dim mLaserPos(enmStage.Max) As Premtek.sPos


        '[Note]:將所有路徑座標存至暫存器
        '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
        mLaserPathRegister(stageNo) = New List(Of sLaserPath)
        mLaserPathRegister(stageNo).Clear()
        mLaserPathRegister(stageNo).Capacity = 1

        For mCount(stageNo) = 0 To LaserList.Count - 1
            If EstimateLaserPos(stageNo, recipe, LaserList(mCount(stageNo)), mLaserPos(stageNo), conveyorNo) = True Then
                With mPath(stageNo)
                    .PosX = mLaserPos(stageNo).PosX
                    .PosY = mLaserPos(stageNo).PosY
                    .PosZ = mLaserPos(stageNo).PosZ
                    .IndexX = LaserList(mCount(stageNo)).IndexX
                    .IndexY = LaserList(mCount(stageNo)).IndexY
                    .NodeName = LaserList(mCount(stageNo)).NodeName
                End With
                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                mLaserPathRegister(stageNo).Capacity = mLaserPathRegister(stageNo).Count + 1
                mLaserPathRegister(stageNo).Add(mPath(stageNo))
            Else
                '[Note]:若發生要查原因
                Return False
            End If
        Next
        LaserPosRegister = mLaserPathRegister(stageNo)
        Return True
    End Function

    ''' <summary>[估算要從哪一顆做到哪一顆，順便把將資料存至暫存區]</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="LaserModel"></param>
    ''' <param name="LaserPosRegister"></param>
    ''' <param name="startIndex"></param>
    ''' <param name="endIndex"></param>
    ''' <param name="subLaserPosRegister"></param>
    ''' <param name="limitPos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EstimateLaserMultiDie(ByVal stageNo As enmStage, ByVal LaserModel As eHeightModel, ByVal LaserPosRegister As List(Of sLaserPath), ByVal startIndex As Integer, ByRef endIndex As Integer, ByRef subLaserPosRegister As List(Of sLaserPath), ByRef limitPos As Premtek.sPos) As Boolean

        Dim mI(enmStage.Max) As Integer
        Dim mTempLimitPos(enmStage.Max) As Premtek.sPos
        Dim mLimitPos(enmStage.Max) As Premtek.sPos
        Dim mLaserPathRegister(enmStage.Max) As List(Of sLaserPath)
        Dim mPath(enmStage.Max) As sLaserPath


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

        mLaserPathRegister(stageNo) = New List(Of sLaserPath)
        mLaserPathRegister(stageNo).Clear()
        mLaserPathRegister(stageNo).Capacity = 1

        '[Note]:極限位置-->若沒有一個過，那就放一個吧，反正後面還會卡住
        '               -->所以至少都會有一個

        Select Case LaserModel
            Case eHeightModel.Contact
                '[Note]:只會有一個
                mPath(stageNo) = LaserPosRegister(startIndex)
                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                mLaserPathRegister(stageNo).Capacity = mLaserPathRegister(stageNo).Count + 1
                mLaserPathRegister(stageNo).Add(mPath(stageNo))

                mLimitPos(stageNo).PosX = mPath(stageNo).PosX
                mLimitPos(stageNo).PosY = mPath(stageNo).PosY

                subLaserPosRegister = mLaserPathRegister(stageNo)
                endIndex = startIndex
                limitPos = mLimitPos(stageNo)
                Return True

            Case eHeightModel.Laser_NonOnFly
                '[Note]:只會有一個
                mPath(stageNo) = LaserPosRegister(startIndex)
                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                mLaserPathRegister(stageNo).Capacity = mLaserPathRegister(stageNo).Count + 1
                mLaserPathRegister(stageNo).Add(mPath(stageNo))

                mLimitPos(stageNo).PosX = mPath(stageNo).PosX
                mLimitPos(stageNo).PosY = mPath(stageNo).PosY

                subLaserPosRegister = mLaserPathRegister(stageNo)
                endIndex = startIndex
                limitPos = mLimitPos(stageNo)
                Return True

            Case eHeightModel.Laser_OnFly
                '[Note]:至少要有一個
                For mI(stageNo) = startIndex To LaserPosRegister.Count - 1
                    mPath(stageNo) = LaserPosRegister(startIndex)
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
                        mLaserPathRegister(stageNo).Capacity = mLaserPathRegister(stageNo).Count + 1
                        mLaserPathRegister(stageNo).Add(mPath(stageNo))
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
                            mVectorX(stageNo) = mdy(stageNo) / mr(stageNo)

                            '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                            mLaserPathRegister(stageNo).Capacity = mLaserPathRegister(stageNo).Count + 1
                            mLaserPathRegister(stageNo).Add(mPath(stageNo))
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
                            If Math.Abs(mVectorX(stageNo) - (mdx(stageNo) / mr(stageNo))) < gSSystemParameter.MotionTolerance And Math.Abs(mVectorX(stageNo) - (mdy(stageNo) / mr(stageNo))) < gSSystemParameter.MotionTolerance Then
                                '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                                mLaserPathRegister(stageNo).Capacity = mLaserPathRegister(stageNo).Count + 1
                                mLaserPathRegister(stageNo).Add(mPath(stageNo))
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
                If mLaserPathRegister(stageNo).Count = 0 Then
                    mPath(stageNo) = LaserPosRegister(startIndex)
                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item(每次Add前都要再提醒系統一次)
                    mLaserPathRegister(stageNo).Capacity = mLaserPathRegister(stageNo).Count + 1
                    mLaserPathRegister(stageNo).Add(mPath(stageNo))

                    mTempLimitPos(stageNo).PosX = mPath(stageNo).PosX
                    mTempLimitPos(stageNo).PosY = mPath(stageNo).PosY
                    '[Note]:更新LimitPos
                    mLimitPos(stageNo) = mTempLimitPos(stageNo)
                    subLaserPosRegister = mLaserPathRegister(stageNo)
                    endIndex = startIndex
                End If
                limitPos = mLimitPos(stageNo)
                Return True

            Case Else
                Return False

        End Select

    End Function
    ''' <summary>[更新 Laser Reselt]</summary>
    ''' <param name="laserNO"></param>
    ''' <param name="stageNo"></param>
    ''' <param name="LaserPathRegister"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateLaserReselt(ByVal laserNO As Integer, ByVal stageNo As enmStage, ByVal LaserPathRegister As List(Of sLaserPath)) As enmLaserStatus

        Dim mI(enmStage.Max) As Integer
        Dim mJ(enmStage.Max) As Integer
        Static mPinZ(enmStage.Max) As Decimal
        Dim mNodeLevelNo As Integer = 0
        Dim SumRealBasicZ As Decimal
        Dim AveRealBasicZ As Decimal = 0
        Dim nodename As String

      
        Select Case gCRecipe.LaserRunMode
            Case eLaserRunModel.Array
                For mI(stageNo) = 0 To LaserPathRegister.Count - 1
                    With LaserPathRegister(mI(stageNo))
                        'laserEnable判斷
                        If gCRecipe.Node(stageNo)(.NodeName).LaserEnable = True Then
                            gStageMap(stageNo).Node(.NodeName).SLaserValue(.IndexX, .IndexY).FilterZHeigh = gStageMap(stageNo).Node(.NodeName).SLaserValue(.IndexX, .IndexY).ZHigh(0)
                            '[Pin=(Laser-Reader)-Offset]
                            '[Note]:設變RealBasicZHigh=(Laser-Reader)，減Offset這個動作在點膠那端再處理，因為需Tilt角度
                            '230 原本 減掉量測值( 350改為加上量測值)__待確認 
                            mPinZ(stageNo) = (gStageMap(stageNo).Node(.NodeName).SRecipePos(.IndexX, .IndexY).LaserPosZ(0) + gStageMap(stageNo).Node(.NodeName).SLaserValue(.IndexX, .IndexY).FilterZHeigh)
                            gStageMap(stageNo).Node(.NodeName).SLaserValue(.IndexX, .IndexY).RealBasicZHigh = mPinZ(stageNo)
                            Debug.Print("Laser Reader: " & gStageMap(stageNo).Node(.NodeName).SLaserValue(.IndexX, .IndexY).RealBasicZHigh)
                        End If
                    End With
                Next
            Case eLaserRunModel.NonArray
                If LaserPathRegister.Count > 0 Then
                    nodename = LaserPathRegister(stageNo).NodeName
                    If gCRecipe.Node(stageNo)(nodename).LaserEnable = True Then
                        For mI(stageNo) = 0 To UBound(gStageMap(stageNo).Node(nodename).SBinMapData, 1)
                            For mJ(stageNo) = 0 To UBound(gStageMap(stageNo).Node(nodename).SBinMapData, 2)
                                gStageMap(stageNo).Node(nodename).SLaserValue(mI(stageNo), mJ(stageNo)).FilterZHeigh = gStageMap(stageNo).Node(nodename).SLaserValue(mI(stageNo), mJ(stageNo)).ZHigh(0)
                                mPinZ(stageNo) = (gStageMap(stageNo).Node(nodename).SRecipePos(mI(stageNo), mJ(stageNo)).LaserPosZ(0) + gStageMap(stageNo).Node(nodename).SLaserValue(mI(stageNo), mJ(stageNo)).FilterZHeigh)
                                gStageMap(stageNo).Node(nodename).SLaserValue(mI(stageNo), mJ(stageNo)).RealBasicZHigh = mPinZ(stageNo)
                                Debug.Print("Laser Reader: " & gStageMap(stageNo).Node(nodename).SLaserValue(mI(stageNo), mJ(stageNo)).RealBasicZHigh)
                            Next
                        Next
                    End If
                End If
        End Select

        For nodei = 0 To gStageMap(stageNo).Node.Keys.Count - 1
            nodename = gStageMap(stageNo).Node.Keys(nodei)
            SumRealBasicZ = 0
            For x As Integer = 0 To gStageMap(stageNo).Node(nodename).SLaserValue.GetUpperBound(0)
                For y As Integer = 0 To gStageMap(stageNo).Node(nodename).SLaserValue.GetUpperBound(1)
                    SumRealBasicZ = SumRealBasicZ + gStageMap(stageNo).Node(nodename).SLaserValue(x, y).RealBasicZHigh
                Next
            Next
            AveRealBasicZ = SumRealBasicZ / gStageMap(stageNo).Node(nodename).SLaserValue.Length


            If GetNodeLevel(nodename, mNodeLevelNo) = True Then
                For I As Integer = 0 To gStageMap(stageNo).Node.Keys.Count - 1
                    If gStageMap(stageNo).Node.Keys(I).StartsWith((nodename)) Then
                        Dim mChildName As String = gStageMap(stageNo).Node.Keys(I)
                        Dim mChildLevelNo As Integer = 0
                        If GetNodeLevel(mChildName, mChildLevelNo) = True Then
                            If mNodeLevelNo = mChildLevelNo - 1 Then
                                If gCRecipe.Node(stageNo)(mChildName).LaserEnable = False Then
                                    For x As Integer = 0 To gStageMap(stageNo).Node(mChildName).SLaserValue.GetUpperBound(0)
                                        For y As Integer = 0 To gStageMap(stageNo).Node(mChildName).SLaserValue.GetUpperBound(1)
                                            gStageMap(stageNo).Node(mChildName).SLaserValue(x, y).RealBasicZHigh = AveRealBasicZ
                                        Next
                                    Next
                                End If
                            End If
                        End If
                    End If
                Next
            End If


        Next


        Return True
    End Function


    ''' <summary>[測高動作流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub DispStage_LaserReaderAction(ByRef sys As sSysParam)

        Static mSYS(enmStage.Max) As sLaserSysParam                                        '[動作模組參數 用於動作命令下達]
        Static mFixList(enmStage.Max) As List(Of sLaserData)                               '[排列出定位順序(Array)]
        Static mNodeName(enmStage.Max) As String                                            '[節點]
        Static mAlignScene(enmStage.Max) As String                                          '[定位場景]
        Static mLaserListIndex(enmStage.Max) As sIndex                                     '[紀錄目前的Index(DispenseRelationshipList)]
        Static mSubDispStageNo1(enmMachineStation.MaxMachine) As Integer                    '[紀錄左側Stage對應的esys.SubDisp]
        Static mSubDispStageNo2(enmMachineStation.MaxMachine) As Integer                    '[紀錄右側Stage對應的esys.SubDisp]
        Static mLaserPathRegister(enmStage.Max) As List(Of sLaserPath)
        Static mLaserPathIndex(enmStage.Max) As sIndex                                     '[紀錄目前的Index(mLaserPathRegister)]
        Static mSubLaserPathRegister(enmStage.Max) As List(Of sLaserPath)                 '[紀錄要都給下一層做的時候的資料(mLaserPathRegister)]
        Static mLaserParam(enmStage.Max) As sLaserParam                                   '[記錄暫存資料]

        Static mLaserRecivedErrorCount(enmStage.Max) As Integer                               '[資料傳輸錯誤之紀錄次數]

        Static mTimeStopWatch(enmStage.Max) As Stopwatch
        Static mStartTime(enmStage.Max) As Decimal
        Static mPosB(enmStage.Max) As Decimal                                               '[座標(B Axis-->Tilt Axis)]

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
        Dim teachX(enmStage.Max) As Integer
        Dim teachY(enmStage.Max) As Integer

        If IsNothing(mSYS(sys.StageNo)) Then
            mSYS(sys.StageNo) = New sLaserSysParam
        End If

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                If IsNothing(mTimeStopWatch(sys.StageNo)) = True Then
                    mTimeStopWatch(sys.StageNo) = New Stopwatch
                End If
                gCMotion.SetCurve(sys.AxisX, eCurveMode.SCurve)
                gCMotion.SetCurve(sys.AxisY, eCurveMode.SCurve)
                gCMotion.SetCurve(sys.AxisZ, eCurveMode.SCurve)
                mLaserRecivedErrorCount(sys.StageNo) = 0

                '[說明]:速度載入
                If gCMotion.SetVelAccDec(sys.AxisX) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030017))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042017))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060017))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067017))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetVelAccDec(sys.AxisY) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031017))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043017))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061017))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068017))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetVelAccDec(sys.AxisZ) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032017))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044017))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062017))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069017))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetVelAccDec(sys.AxisB) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1034017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034017))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1046017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046017))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1064017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064017))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1071017", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071017))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                '[Module]
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                    Case eMechanismModule.TwoValveOneStage
                        ValveCylinderAction(sys.StageNo, eValveWorkMode.Valve1, enmUpDown.Down) 'Soni + 2017.04.27 增加氣缸動作
                        ''[說明]:第二隻Valve的氣缸上昇
                End Select


                sys.SysNum = 1020
            Case 1020
                If ValveCylinderSensor(sys.StageNo, eValveWorkMode.Valve1, enmUpDown.Down) = True Then 'Soni + 2017.04.27 增加氣缸動作
                    sys.SysNum = 1200
                Else
                     gEqpMsg.AddHistoryAlarm("Warn_3019103", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3019103), eMessageLevel.Error)
					 sys.RunStatus = enmRunStatus.Alarm
                End If

            Case 1200
                '[Note]:確認Tilt有無存在，存在則流程會增加Z軸上升-->Tilt旋轉
                If sys.AxisB <> -1 Then
                    Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
                    gCMotion.GetCommandValue(sys.AxisB, mCommandPosB(sys.StageNo))
                    If mCommandPosB(sys.StageNo) = mPosB(sys.StageNo) Then
                        '[Note]:若角度相同，則Tilt不再做旋轉
                        sys.SysNum = 2000
                    Else
                        sys.SysNum = 1300
                    End If
                Else
                    sys.SysNum = 2000
                End If

            Case 1300
                '[Note]:先將Z軸升至安全位置
                'ReviseVelocity(sys.AxisZ, gSSystemParameter.Pos.TiltSafePosZ, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.TiltSafePosZ) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 1400

            Case 1400
                '[Note]:等待Table Stop
                mAxisZState(sys.StageNo) = gCMotion.MotionDone(sys.AxisZ)
                If mAxisZState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                sys.SysNum = 1500

            Case 1500
                '[Note]:根據Recipe給的Tilt角度決定轉至該位置
                ReviseVelocity(sys.AxisB, mPosB(sys.StageNo), gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisB, mPosB(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1034000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1046000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1064000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1071000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 1600

            Case 1600
                '[Note]:等待Table Stop
                mAxisBState(sys.StageNo) = gCMotion.MotionDone(sys.AxisB)
                If mAxisBState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisB) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1034004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1046004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1064004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1071004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                sys.SysNum = 2000


            Case 2000
                '[Note]:Node順序排列
                Call LaserRecipeNodeSort(gCRecipe, sys.StageNo, sys.ConveyorNo)
                '[Note]:會有重複執行的問題，但沒關係
                Select Case sys.MachineNo
                    Case enmMachineStation.MachineA
                        '[Note]:先做配接-->根據機種，決定需要哪幾組Stage復歸
                        Select Case gSSystemParameter.MachineType
                            Case enmMachineType.DCSW_800AQ, enmMachineType.DCS_500AD
                                mSubDispStageNo1(sys.MachineNo) = eSys.SubDisp1
                                mSubDispStageNo2(sys.MachineNo) = eSys.SubDisp2

                            Case enmMachineType.eDTS_2S2V
                                mSubDispStageNo1(sys.MachineNo) = eSys.SubDisp1
                                mSubDispStageNo2(sys.MachineNo) = eSys.SubDisp1

                            Case Else
                                mSubDispStageNo1(sys.MachineNo) = eSys.SubDisp1
                                mSubDispStageNo2(sys.MachineNo) = eSys.SubDisp1

                        End Select

                    Case enmMachineStation.MachineB
                        mSubDispStageNo1(sys.MachineNo) = eSys.SubDisp3
                        mSubDispStageNo2(sys.MachineNo) = eSys.SubDisp4

                End Select

                gNodeLevel(sys.StageNo) = 1
                mLaserListIndex(sys.StageNo).Start = -1
                mLaserListIndex(sys.StageNo).Ending = -1
                mLaserListIndex(sys.StageNo).Done = -1

                If IsNeedLaserProcess(gCRecipe, sys.StageNo) = True Then
                    sys.SysNum = 2100
                Else
                    If sys.StageNo = enmStage.No2 Or sys.StageNo = enmStage.No4 Then
                        gIsLSideWorking(sys.MachineNo) = True
                    End If
                    '[Note]:若有一側不用作業，則直接給NodeLevel至Max(應該不可能大於9999層)
                    gNodeLevel(sys.StageNo) = 9999
                    sys.SysNum = 9000
                End If

            Case 2100
                '[Note]:確認R側已經開始作業後，L側才可以開始作業
                Select Case sys.StageNo
                    Case enmStage.No1, enmStage.No3
                        If gIsLSideWorking(sys.MachineNo) = True Then
                            sys.SysNum = 2200
                        End If

                    Case Else
                        sys.SysNum = 2200

                End Select

            Case 2200
                '[Note]:判斷是否已跑完所有Node
                If mLaserListIndex(sys.StageNo).Done = LaserRelationshipList(sys.StageNo).Count - 1 Then
                    '[Note]:完成所有Node
                    sys.SysNum = 9000
                Else
                    '[Note]:取下一個LaserRelationshipList(不一定是下一個Node)
                    mLaserListIndex(sys.StageNo).Start = mLaserListIndex(sys.StageNo).Ending + 1
                    mLaserListIndex(sys.StageNo).Ending = mLaserListIndex(sys.StageNo).Ending + 1
                    '[Note]:使用List一定要宣告Capacity大小，不然有機會中梗-->一次加入大量的Item
                    mFixList(sys.StageNo) = New List(Of sLaserData)
                    mFixList(sys.StageNo).Capacity = 1

                    'Array 狀況下走2300
                    'NonArray 狀況下走2350
                    Select Case gCRecipe.LaserRunMode
                        Case eLaserRunModel.Array
                            sys.SysNum = 2300
                        Case eLaserRunModel.NonArray
                            'NonArray 資料佔存區
                            LaserHeight(sys.StageNo) = New List(Of Decimal)
                            LaserHeight(sys.StageNo).Capacity = 1
                            sys.SysNum = 2350
                    End Select

                End If

            Case 2300
                '[Note]:先判斷該層有幾個Node
                '[Note]:先判斷該層是否需要定位
                gNodeLevel(sys.StageNo) = LaserRelationshipList(sys.StageNo)(mLaserListIndex(sys.StageNo).Start).NodeLevel
                For mI(sys.StageNo) = mLaserListIndex(sys.StageNo).Start To LaserRelationshipList(sys.StageNo).Count - 1
                    If gNodeLevel(sys.StageNo) = LaserRelationshipList(sys.StageNo)(mI(sys.StageNo)).NodeLevel Then
                        Call LaserSortList(gCRecipe, sys.StageNo, LaserRelationshipList(sys.StageNo)(mI(sys.StageNo)), mFixList(sys.StageNo), sys.ConveyorNo)
                    Else
                        '[Note]:進入不同層
                        mLaserListIndex(sys.StageNo).Ending = mI(sys.StageNo) - 1
                        Exit For
                    End If
                    If mI(sys.StageNo) = LaserRelationshipList(sys.StageNo).Count - 1 Then
                        '[Note]:最後一個了
                        mLaserListIndex(sys.StageNo).Ending = mI(sys.StageNo)
                    End If
                Next
                sys.SysNum = 2400

            Case 2350
                '[Note]:取出node做處理
                Call LaserSortList(gCRecipe, sys.StageNo, LaserRelationshipList(sys.StageNo)(mLaserListIndex(sys.StageNo).Start), mFixList(sys.StageNo), sys.ConveyorNo)
                sys.SysNum = 2400

            Case 2400

                If CreateLaserPosList(gCRecipe, sys.StageNo, mFixList(sys.StageNo), mLaserPathRegister(sys.StageNo), sys.ConveyorNo) = True Then
                    '[Note]:
                    mLaserPathIndex(sys.StageNo).Start = -1
                    mLaserPathIndex(sys.StageNo).Ending = -1
                    mLaserPathIndex(sys.StageNo).Done = -1
                    sys.SysNum = 2500
                Else
                    'TODO:異常訊息(CreateLaserPosList Fail)
                    gEqpMsg.AddHistoryAlarm("Error_1000012", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000012), eMessageLevel.Error)
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If


            Case 2500
                '[Note]:判斷所有定位點都完成了(該層)
                If mLaserPathIndex(sys.StageNo).Done = mLaserPathRegister(sys.StageNo).Count - 1 Then
                    '[Note]:完成所有定位點-->進入Update RealBacis，但進入之前須先進入防撞機制
                    sys.SysNum = 3000
                Else
                    mLaserPathIndex(sys.StageNo).Start = mLaserPathIndex(sys.StageNo).Ending + 1
                    mLaserPathIndex(sys.StageNo).Ending = mLaserPathIndex(sys.StageNo).Ending + 1
                    sys.SysNum = 2600
                End If

            Case 2600
                '[Note]:先決定從哪一顆做到哪一顆
                If EstimateLaserMultiDie(sys.StageNo, gCRecipe.LaserFixMode, mLaserPathRegister(sys.StageNo), mLaserPathIndex(sys.StageNo).Start, mLaserPathIndex(sys.StageNo).Ending, mSubLaserPathRegister(sys.StageNo), mLaserProtect(sys.StageNo).TargetPos) = True Then
                    sys.SysNum = 3000
                Else
                    'TODO:異常訊息(EstimateLaserMultiDie Fail)
                    gEqpMsg.AddHistoryAlarm("Error_1000012", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000012), eMessageLevel.Error)
                    sys.RunStatus = enmRunStatus.Alarm
                End If

                '*************************************************************************************************
                '******************************************防撞機制(退後)*****************************************
                '*************************************************************************************************
            Case 3000
                '[Note]:安全位置檢察 XY移動前位置確認(只有800AQ需要做此項確認)
                '[Note]:更新各軸的目標位置
                '[Note]:L側動完後，R側就可以動了
                gIsLSideWorking(sys.MachineNo) = True
                gProtectData(sys.StageNo).TargetPos = mLaserProtect(sys.StageNo).TargetPos
                If EstimateIsSafePos(sys.StageNo, gNodeLevel) = True Then
                    '[Note]:判斷所有定位點都完成了(該層)
                    If mLaserPathIndex(sys.StageNo).Done = mLaserPathRegister(sys.StageNo).Count - 1 Then
                        '[Note]:完成所有定位點-->進入Update RealBacis，但進入之前須先進入防撞機制
                        sys.SysNum = 8000
                    Else
                        sys.SysNum = 4000
                    End If
                Else
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
                                    gEqpMsg.AddHistoryAlarm("Error_1042005", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042005), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                            Case enmStage.No3
                                If gSYS(eSys.DispStage4).RunStatus = enmRunStatus.Finish And gIsRSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1042005", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042005), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                        End Select

                        '[Note]:檢查R側是否有叫L側退開
                        If gIsLSideNeedGoToSafePos(sys.MachineNo) = True Then
                            '[Note]:根據另一側的資訊決定要退到哪去
                            Call EstimateGoBackPos(sys.StageNo, gProtectData, gProtectData(sys.StageNo).TargetPos)
                            gIsLSideNeedGoToSafePos(sys.MachineNo) = False
                            sys.SysNum = 3200
                        Else
                            sys.SysNum = 3000
                        End If

                    Case enmStage.No2, enmStage.No4
                        '[說明]:當單Stage動作時,另一頭Node為0時(不動作Stage)!!!是不會啟動移動至安全位置,移動Stage只能無限等待(另一頭回安全位置)
                        '目前確認另一頭Node為0時(不動作Stage)會移至原點位置,如果此位置還會觸發安全位置檢察失敗,表示設定位置已超出極限,需修改Recipe or 放寬安全位置(不會撞機為原則)<=不建議
                        Select Case sys.StageNo
                            Case enmStage.No2
                                If gSYS(eSys.DispStage1).RunStatus = enmRunStatus.Finish And gIsLSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1030006", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030006), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                            Case enmStage.No4
                                If gSYS(eSys.DispStage3).RunStatus = enmRunStatus.Finish And gIsLSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1030006", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030006), eMessageLevel.Error)
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
                            sys.SysNum = 3200
                        Else
                            sys.SysNum = 3000
                        End If

                End Select

            Case 3200
                '[Note]:退至安全區 
                ReviseVelocity(sys.AxisY, gProtectData(sys.StageNo).TargetPos.PosY, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisY, gProtectData(sys.StageNo).TargetPos.PosY) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                ReviseVelocity(sys.AxisX, gProtectData(sys.StageNo).TargetPos.PosX, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisX, gProtectData(sys.StageNo).TargetPos.PosX) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 3300

            Case 3300
                '[Note]:等待Table Stop
                mAxisXState(sys.StageNo) = gCMotion.MotionDone(sys.AxisX)
                mAxisYState(sys.StageNo) = gCMotion.MotionDone(sys.AxisY)
                If mAxisXState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1030004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1042004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1060004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1067004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
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
                                gEqpMsg.AddHistoryAlarm("Error_1031004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1043004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1061004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1068004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                sys.SysNum = 3000


                '*******************************************************************************
                '****************派工給LaserNonOnFly或Contact*******************
                '*******************************************************************************
            Case 4000
                '[Note]:下命令給點膠子流程
                '[Note]:系統狀態改為運行中，且由起始索引開始工作

                '[Note]:塞資料，把資料打包往下一層送-->決定走哪個流程
                '[Note]:先塞群組之加減速  
                With mLaserParam(sys.StageNo)
                    .Acc = gCMotion.SyncParameter(sys.StageNo).Velocity.Acc * gCMotion.SyncParameter(sys.StageNo).Velocity.AccRatio
                    .Dec = gCMotion.SyncParameter(sys.StageNo).Velocity.Dec * gCMotion.SyncParameter(sys.StageNo).Velocity.DecRatio
                    .VelHigh = gCMotion.SyncParameter(sys.StageNo).Velocity.VelHigh
                End With

                With mSYS(sys.StageNo)
                    .AxisX = sys.AxisX
                    .AxisY = sys.AxisY
                    .AxisZ = sys.AxisZ
                    .AxisA = sys.AxisA
                    .AxisB = sys.AxisB
                    .AxisC = sys.AxisC
                    .laserNo = sys.LaserNo
                    '.Tag = sys.Tag
                    .Timer = sys.Timer
                    .StageNo = sys.StageNo
                    .LaserPathRegister = mSubLaserPathRegister(sys.StageNo)
                    .LaserParam = mLaserParam(sys.StageNo)
                    .LaserModel = gCRecipe.LaserFixMode
                    .SysNum = sLaserSysParam.SysLoopStart
                    .RunStatus = enmRunStatus.Running
                    .IsCanPause = True
                End With
                sys.SysNum = 4100

            Case 4100
                '[Note]:跑Laser子流程(更新流程)
                '       若外部給暫停的命令且流程中是允許暫停，則才可以暫停
                If (sys.ExternalPause = True And mSYS(sys.StageNo).IsCanPause = True) Then
                    Exit Sub
                End If

                If mSYS(sys.StageNo).RunStatus = enmRunStatus.Running Then

                    Select Case gCRecipe.LaserRunMode
                        Case eLaserRunModel.Array

                            Select Case mSYS(sys.StageNo).LaserModel
                                Case eHeightModel.Laser_OnFly
                                Case eHeightModel.Laser_NonOnFly
                                    LaserNonOnFlyModelAction(mSYS(sys.StageNo))
                                Case eHeightModel.Contact
                                    ContactModelAction(mSYS(sys.StageNo))
                                Case Else
                            End Select

                        Case eLaserRunModel.NonArray
                            '將LaserHeight 以一併傳入Function
                            Select Case mSYS(sys.StageNo).LaserModel
                                Case eHeightModel.Laser_OnFly
                                Case eHeightModel.Laser_NonOnFly
                                    LaserNonOnFlyModelActionNonArray(mSYS(sys.StageNo), LaserHeight(sys.StageNo))
                                Case eHeightModel.Contact
                                    ContactModelActionNonArray(mSYS(sys.StageNo), LaserHeight(sys.StageNo))
                                Case Else
                            End Select

                    End Select


                End If

                '[Note]:判斷子流程是否完成
                If mSYS(sys.StageNo).RunStatus = enmRunStatus.Finish Then
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
                                    gEqpMsg.AddHistoryAlarm("Error_1042005", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042005), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                            Case enmStage.No3
                                If gSYS(eSys.DispStage4).RunStatus = enmRunStatus.Finish And gIsRSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1042005", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042005), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                        End Select

                        '[Note]:檢查R側是否有叫L側退開
                        If gIsLSideNeedGoToSafePos(sys.MachineNo) = True Then
                            '[Note]:根據另一側的資訊決定要退到哪去
                            Call EstimateGoBackPos(sys.StageNo, gProtectData, gProtectData(sys.StageNo).TargetPos)
                            gIsLSideNeedGoToSafePos(sys.MachineNo) = False
                            sys.SysNum = 5100
                        Else
                            sys.SysNum = 6000
                        End If

                    Case enmStage.No2, enmStage.No4
                        '[說明]:當單Stage動作時,另一頭Node為0時(不動作Stage)!!!是不會啟動移動至安全位置,移動Stage只能無限等待(另一頭回安全位置)
                        '目前確認另一頭Node為0時(不動作Stage)會移至原點位置,如果此位置還會觸發安全位置檢察失敗,表示設定位置已超出極限,需修改Recipe or 放寬安全位置(不會撞機為原則)<=不建議
                        Select Case sys.StageNo
                            Case enmStage.No2
                                If gSYS(eSys.DispStage1).RunStatus = enmRunStatus.Finish And gIsLSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1030006", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030006), eMessageLevel.Error)
                                    sys.RunStatus = enmRunStatus.Alarm
                                    Exit Sub
                                End If
                            Case enmStage.No4
                                If gSYS(eSys.DispStage3).RunStatus = enmRunStatus.Finish And gIsLSideNeedGoToSafePos(sys.MachineNo) = True Then
                                    gEqpMsg.AddHistoryAlarm("Error_1030006", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030006), eMessageLevel.Error)
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
                            sys.SysNum = 5100
                        Else
                            sys.SysNum = 6000
                        End If

                End Select

            Case 5100
                '[Note]:退至安全區 
                ReviseVelocity(sys.AxisY, gProtectData(sys.StageNo).TargetPos.PosY, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisY, gProtectData(sys.StageNo).TargetPos.PosY) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                ReviseVelocity(sys.AxisX, gProtectData(sys.StageNo).TargetPos.PosX, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisX, gProtectData(sys.StageNo).TargetPos.PosX) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
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
                                gEqpMsg.AddHistoryAlarm("Error_1030004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1042004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1060004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1067004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
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
                                gEqpMsg.AddHistoryAlarm("Error_1031004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1043004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1061004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1068004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                sys.SysNum = 6000

            Case 6000
                '[Note]:完成
                mLaserPathIndex(sys.StageNo).Done = mLaserPathIndex(sys.StageNo).Ending
                sys.SysNum = 2500

            Case 8000
                '增加判斷Array/NonArray
                '若為Array 直接跳到 Case 8100
                '若為NonArray 計算LaserHeight(sys.stagoNo)平均值後，更新資料，然後跳到Case 8100
                Select Case gCRecipe.LaserRunMode
                    Case eLaserRunModel.Array
                        sys.SysNum = 8100
                    Case eLaserRunModel.NonArray
                        Dim NodeName As String
                        Dim Average As Decimal
                        Dim Sum As Decimal = 0
                        If mLaserPathRegister(sys.StageNo).Count > 0 Then
                            NodeName = mLaserPathRegister(sys.StageNo)(0).NodeName
                            For i = 0 To LaserHeight(sys.StageNo).Count - 1
                                Sum = Sum + LaserHeight(sys.StageNo)(i)
                            Next

                            Average = Math.Round(Sum / LaserHeight(sys.StageNo).Count, 3)

                            For mI(sys.StageNo) = 0 To UBound(gStageMap(sys.StageNo).Node(NodeName).SBinMapData, 1)
                                For mJ(sys.StageNo) = 0 To UBound(gStageMap(sys.StageNo).Node(NodeName).SBinMapData, 2)
                                    '塞入此Node 資料
                                    gStageMap(sys.StageNo).Node(NodeName).SLaserValue(mI(sys.StageNo), mJ(sys.StageNo)).ZHigh(0) = Average
                                    gStageMap(sys.StageNo).Node(NodeName).SLaserValue(mI(sys.StageNo), mJ(sys.StageNo)).LaserFinish(0) = True
                                Next
                            Next
                        End If
                        sys.SysNum = 8100
                End Select

            Case 8100
                '[Note]:接收資料結果
                If UpdateLaserReselt(sys.LaserNo, sys.StageNo, mLaserPathRegister(sys.StageNo)) Then

                    sys.SysNum = 8500
                End If

            Case 8500
                '[Note]:(更新本層的所有Node) Update Real Bacis與下一層的Real Basic、ScanPos(Align Pos) 全部更新完成後，在進入下一層繼續做
                '       同一個Node只需更新一次即可
                mNodeName(sys.StageNo) = ""
                For mI(sys.StageNo) = mLaserListIndex(sys.StageNo).Start To mLaserListIndex(sys.StageNo).Ending
                    If mNodeName(sys.StageNo) <> LaserRelationshipList(sys.StageNo)(mI(sys.StageNo)).NodeName Then
                        mNodeName(sys.StageNo) = LaserRelationshipList(sys.StageNo)(mI(sys.StageNo)).NodeName
                    End If
                Next
                mLaserListIndex(sys.StageNo).Done = mLaserListIndex(sys.StageNo).Ending

                sys.SysNum = 2200

            Case 9000
                '[Note]:點膠完成後，退至安全區等待(針對有作業的情況下)
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        '[Note]:直接退到安全位置
                        gProtectData(sys.StageNo).TargetPos.PosX = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosX(sys.SelectValve)
                        gProtectData(sys.StageNo).TargetPos.PosY = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosY(sys.SelectValve)
                        gProtectData(sys.StageNo).TargetPos.PosZ = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosZ(sys.SelectValve)


                        mLaserProtect(sys.StageNo).TargetPos.PosX = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosX(sys.SelectValve)
                        mLaserProtect(sys.StageNo).TargetPos.PosY = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosY(sys.SelectValve)

                        Select Case sys.StageNo
                            Case enmStage.No1
                                If gCRecipe.Node(enmStage.No2).Count = 0 Then
                                    sys.SysNum = 9200
                                Else
                                    sys.SysNum = 9100
                                End If
                            Case enmStage.No2
                                If gCRecipe.Node(enmStage.No1).Count = 0 Then
                                    sys.SysNum = 9200
                                Else
                                    sys.SysNum = 9100
                                End If
                            Case enmStage.No3
                                If gCRecipe.Node(enmStage.No4).Count = 0 Then
                                    sys.SysNum = 9200
                                Else
                                    sys.SysNum = 9100
                                End If
                            Case enmStage.No4
                                If gCRecipe.Node(enmStage.No3).Count = 0 Then
                                    sys.SysNum = 9200
                                Else
                                    sys.SysNum = 9100
                                End If
                        End Select
                    Case Else
                        gProtectData(sys.StageNo).TargetPos.PosZ = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosZ(sys.SelectValve)
                        sys.SysNum = 9200

                End Select

            Case 9100
                ReviseVelocity(sys.AxisY, gProtectData(sys.StageNo).TargetPos.PosY, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisY, gProtectData(sys.StageNo).TargetPos.PosY) <> CommandStatus.Sucessed Then
                    '[Note]:X軸移動異常
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                ReviseVelocity(sys.AxisX, gProtectData(sys.StageNo).TargetPos.PosX, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisX, gProtectData(sys.StageNo).TargetPos.PosX) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                'ReviseVelocity(sys.AxisZ, gSSystemParameter.Pos.SafePosZ, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 9300

            Case 9200
                'ReviseVelocity(sys.AxisZ, gSSystemParameter.Pos.SafePosZ, gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069000", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
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
                                gEqpMsg.AddHistoryAlarm("Error_1030004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1042004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1060004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1067004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
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
                                gEqpMsg.AddHistoryAlarm("Error_1031004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1043004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1061004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1068004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
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
                                gEqpMsg.AddHistoryAlarm("Error_1032004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                sys.SysNum = 9400

            Case 9400
                sys.RunStatus = enmRunStatus.Finish
                Exit Sub

        End Select

    End Sub

    ''' <summary>[非On Fly Model測高流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Private Sub LaserNonOnFlyModelAction(ByRef sys As sLaserSysParam)

        Static mLaserStableTimeStopWatch(enmStage.Max) As Stopwatch
        Static mLaserReaderErrorCount(enmStage.Max) As Integer          '[紀錄Laser Reader錯誤次數]
        Static mPosX(enmStage.Max) As Decimal                                   '[目標座標:X]
        Static mPosY(enmStage.Max) As Decimal                                   '[目標座標:Y]
        Static mPosZ(enmStage.Max) As Decimal                                   '[目標座標:Z]
        Static mScene(enmStage.Max) As String                                   '[場景]
        Static mPath(enmStage.Max) As sLaserPath                               '[暫存路徑資訊]
        Static mLaserStableTimeStartTime(enmStage.Max) As Decimal


        Dim mAxisXState(enmStage.Max) As CommandStatus                          '[X軸的狀態]
        Dim mAxisYState(enmStage.Max) As CommandStatus                          '[Y軸的狀態]
        Dim mAxisZState(enmStage.Max) As CommandStatus                          '[Z軸的狀態]
        Static mLaserValue(enmStage.Max) As Decimal                     '[Laser Reader測高取出的數值]
        Dim mAcc(enmStage.Max) As Decimal
        Dim mDec(enmStage.Max) As Decimal


        Select Case sys.SysNum
            Case sLaserSysParam.SysLoopStart
                '[Note]:載入資料，初始化
                If IsNothing(mLaserStableTimeStopWatch(sys.StageNo)) = True Then
                    mLaserStableTimeStopWatch(sys.StageNo) = New Stopwatch
                End If

                '[Note]:取定位座標
                mPath(sys.StageNo) = sys.LaserPathRegister(0)
                mPosX(sys.StageNo) = mPath(sys.StageNo).PosX
                mPosY(sys.StageNo) = mPath(sys.StageNo).PosY
                mPosZ(sys.StageNo) = mPath(sys.StageNo).PosZ

                sys.SysNum = 1100

            Case 1100
                sys.SysNum = 1200


            Case 1200
                '[Note]:移動至取像位置_X&Y
                gCMotion.GPSetRunMode(gCMotion.SyncParameter(sys.StageNo), eRunMode.BlendingMode)

                Dim mVelocity As Decimal = gCMotion.AxisParameter(sys.AxisX).Velocity.MaxVel
                With gCMotion.AxisParameter(sys.AxisX).Velocity
                    Dim mDeltaX As Decimal
                    Dim mDeltaY As Decimal
                    mDeltaX = mPosX(sys.StageNo) - gCMotion.GetPositionValue(sys.AxisX)
                    mDeltaY = mPosY(sys.StageNo) - gCMotion.GetPositionValue(sys.AxisY)

                    Dim mDistance As Decimal = Math.Sqrt(mDeltaX ^ 2 + mDeltaY ^ 2)
                    If mGpSCurve = eCurveMode.SCurve Then
                        mAcc(sys.StageNo) = .Acc * .AccRatio * mSCurveRatio
                        mDec(sys.StageNo) = .Dec * .DecRatio * mSCurveRatio
                    Else
                        mAcc(sys.StageNo) = .Acc * .AccRatio
                        mDec(sys.StageNo) = .Dec * .DecRatio
                    End If

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
                End With
                If gCMotion.GpSetVelLow(gCMotion.SyncParameter(sys.StageNo), 0) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036014", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036014), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048014", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048014), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066014", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066014), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073014", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073014), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetVelHigh(gCMotion.SyncParameter(sys.StageNo), mVelocity) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036013", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036013), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048013", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048013), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066013", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066013), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073013", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073013), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetAcc(gCMotion.SyncParameter(sys.StageNo), mAcc(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036011", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036011), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048011", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048011), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066011", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066011), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073011", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073011), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetDec(gCMotion.SyncParameter(sys.StageNo), mDec(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036012", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036012), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048012", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048012), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066012", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066012), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073012", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073012), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.GpSetCurve(gCMotion.SyncParameter(sys.StageNo), mGpSCurve) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036010", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036010), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048010", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048010), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066010", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066010), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073010", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073010), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.GpMoveLinearAbsXYZ(gCMotion.SyncParameter(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                'ReviseVelocity(sys.AxisY, mPosY(sys.StageNo), gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                'If gCMotion.AbsMove(sys.AxisY, mPosY(sys.StageNo)) <> CommandStatus.Sucessed Then
                '    Select Case sys.StageNo
                '        Case 0
                '            gEqpMsg.AddHistoryAlarm("Error_1031000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                '        Case 1
                '            gEqpMsg.AddHistoryAlarm("Error_1043000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                '        Case 2
                '            gEqpMsg.AddHistoryAlarm("Error_1061000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                '        Case 3
                '            gEqpMsg.AddHistoryAlarm("Error_1068000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                '    End Select
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If
                'ReviseVelocity(sys.AxisX, mPosX(sys.StageNo), gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                'If gCMotion.AbsMove(sys.AxisX, mPosX(sys.StageNo)) <> CommandStatus.Sucessed Then
                '    Select Case sys.StageNo
                '        Case 0
                '            gEqpMsg.AddHistoryAlarm("Error_1030000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                '        Case 1
                '            gEqpMsg.AddHistoryAlarm("Error_1042000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                '        Case 2
                '            gEqpMsg.AddHistoryAlarm("Error_1060000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                '        Case 3
                '            gEqpMsg.AddHistoryAlarm("Error_1067000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                '    End Select
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If
                'If gCMotion.AbsMove(sys.AxisZ, mPosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                '    Select Case sys.StageNo
                '        Case 0
                '            gEqpMsg.AddHistoryAlarm("Error_1032000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                '        Case 1
                '            gEqpMsg.AddHistoryAlarm("Error_1044000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                '        Case 2
                '            gEqpMsg.AddHistoryAlarm("Error_1062000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                '        Case 3
                '            gEqpMsg.AddHistoryAlarm("Error_1069000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                '    End Select
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If
                sys.SysNum = 1900

            Case 1900
                '[說明]:等待Table Stop 
                If gCMotion.GpMoveDone(gCMotion.SyncParameter(sys.StageNo)) = CommandStatus.Sucessed Then
                    sys.SysNum = 2000
                End If
                'mAxisXState(sys.StageNo) = gCMotion.MotionDone(sys.AxisX)
                'mAxisYState(sys.StageNo) = gCMotion.MotionDone(sys.AxisY)

                'If mAxisXState(sys.StageNo) <> CommandStatus.Sucessed Then
                '    If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                '        Select Case sys.StageNo
                '            Case 0
                '                gEqpMsg.AddHistoryAlarm("Error_1030004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                '            Case 1
                '                gEqpMsg.AddHistoryAlarm("Error_1042004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                '            Case 2
                '                gEqpMsg.AddHistoryAlarm("Error_1060004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                '            Case 3
                '                gEqpMsg.AddHistoryAlarm("Error_1067004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
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
                '                gEqpMsg.AddHistoryAlarm("Error_1031004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                '            Case 1
                '                gEqpMsg.AddHistoryAlarm("Error_1043004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                '            Case 2
                '                gEqpMsg.AddHistoryAlarm("Error_1061004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                '            Case 3
                '                gEqpMsg.AddHistoryAlarm("Error_1068004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                '        End Select
                '        sys.RunStatus = enmRunStatus.Alarm
                '        Exit Sub
                '    Else
                '        Exit Sub
                '    End If
                'End If
                'sys.SysNum = 2000

            Case 2000
                '[Note]:移動至取像位置_Z
                'ReviseVelocity(sys.AxisZ, mPosZ(sys.StageNo), gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisZ, mPosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 3000

            Case 3000
                '[說明]:等待Table Stop
                mAxisZState(sys.StageNo) = gCMotion.MotionDone(sys.AxisZ)

                If mAxisZState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If

                mLaserStableTimeStopWatch(sys.StageNo).Restart()
                mLaserStableTimeStartTime(sys.StageNo) = mLaserStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds
                sys.SysNum = 3100

            Case 3100
                Dim mStableTime As Decimal = IIf(gSSystemParameter.StableTime.LaserStableTime > 1000, gSSystemParameter.StableTime.LaserStableTime, 1000) '至少一秒穩定
                If Math.Abs(mLaserStableTimeStartTime(sys.StageNo) - mLaserStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds) > mStableTime Then
                    mLaserStableTimeStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 3200
                End If

            Case 3200
                Dim readerValue As String = ""
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.IsBusy(sys.laserNo) = False Then
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.laserNo, 0) = True Then '發出命令
                        sys.SysNum = 3220
                    Else '讀取失敗
                        gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SetCCDLaserStatus(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY, eDieStatus.LaserFail)
                        mLaserValue(sys.StageNo) = 10
                        sys.SysNum = 3300
                    End If

                End If


            Case 3220 ''通過, 往下走
                Dim readerValue As String = ""
                '判斷是否收到命令
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.IsBusy(sys.laserNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.IsTimeOut(sys.laserNo) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLaserReaderErrorCount(sys.StageNo) = mLaserReaderErrorCount(sys.StageNo) + 1
                        If mLaserReaderErrorCount(sys.StageNo) > 3 Then
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1014004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1014104", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014104), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1014204", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014204), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1014304", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014304), eMessageLevel.Error)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 3200
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetLaserValue(sys.laserNo, readerValue) Then
                        mLaserValue(sys.StageNo) = CDec(readerValue)
                        sys.SysNum = 3300
                    Else 'Status 尚未為True 重新做loop一次
                        mLaserValue(sys.StageNo) = 10
                    End If
                End If

            Case 3300
                '[Note]:忽略Laser數值
                If gCRecipe.BypassLaserResult = True Then
                    gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SLaserValue(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY).ZHigh(0) = 0
                    gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SLaserValue(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY).LaserFinish(0) = True

                    '[說明]:紀錄Laser Reader良率
                    With gSSystemParameter.ProductState
                        .LaserReaderOKPcs = .LaserReaderOKPcs + 1
                        .TotoalLaserReaderPcs = .TotoalLaserReaderPcs + 1
                    End With
                    sys.SysNum = 9000
                Else
                    '[Note]:             數值大於9都是不正常的數字()
                    If Math.Abs(mLaserValue(sys.StageNo)) > ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.ReaderMaxValue Then
                        mLaserReaderErrorCount(sys.StageNo) = mLaserReaderErrorCount(sys.StageNo) + 1
                        If mLaserReaderErrorCount(sys.StageNo) > 3 Then
                            '[說明]:Laser Reader 異常
                            '[說明]:紀錄Scan & LaserReader狀態 &  圖形顏色寫入

                            With gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).ChipState(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY)
                                .DieState = enmDieState.NG
                                .NeedUpdate = True
                            End With
                            'Call DetermineResultState(mLaserIndex(sys.StageNo), sys)

                            '[說明]:判斷NG 就不用做劃膠
                            With gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SRecipePos(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY)
                                .IsByPassDispensingAction = True
                            End With

                            '[Note]:更新目前處理的狀態
                            gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SBinMapData(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY).Status = eDieStatus.LaserFail
                            'Call WriteDieStatusForMappingData("GG", eDataType.eNonDispensing, sys.StageNo, mPath(sys.StageNo).NodeName, mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY, eDieStatus.LaserFail)
                            sys.SysNum = 9000
                        End If

                    Else
                        With gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SLaserValue(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY)
                            .ZHigh(0) = mLaserValue(sys.StageNo)
                            .LaserFinish(0) = True
                        End With
                        '[說明]:紀錄Laser Reader良率
                        With gSSystemParameter.ProductState
                            .LaserReaderOKPcs = .LaserReaderOKPcs + 1
                            .TotoalLaserReaderPcs = .TotoalLaserReaderPcs + 1
                        End With
                        sys.SysNum = 9000
                    End If
                End If

            Case 9000
                sys.RunStatus = enmRunStatus.Finish
                Exit Sub

        End Select

    End Sub
    ''' <summary>[非On Fly Model測高流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Private Sub LaserNonOnFlyModelActionNonArray(ByRef sys As sLaserSysParam, ByVal LaserHeightList As List(Of Decimal))

        Static mLaserStableTimeStopWatch(enmStage.Max) As Stopwatch
        Static mLaserReaderErrorCount(enmStage.Max) As Integer          '[紀錄Laser Reader錯誤次數]
        Static mPosX(enmStage.Max) As Decimal                                   '[目標座標:X]
        Static mPosY(enmStage.Max) As Decimal                                   '[目標座標:Y]
        Static mPosZ(enmStage.Max) As Decimal                                   '[目標座標:Z]
        Static mScene(enmStage.Max) As String                                   '[場景]
        Static mPath(enmStage.Max) As sLaserPath                               '[暫存路徑資訊]
        Static mLaserStableTimeStartTime(enmStage.Max) As Decimal


        Dim mAxisXState(enmStage.Max) As CommandStatus                          '[X軸的狀態]
        Dim mAxisYState(enmStage.Max) As CommandStatus                          '[Y軸的狀態]
        Dim mAxisZState(enmStage.Max) As CommandStatus                          '[Z軸的狀態]
        Static mLaserValue(enmStage.Max) As Decimal                     '[Laser Reader測高取出的數值]
        Dim mAcc(enmStage.Max) As Decimal
        Dim mDec(enmStage.Max) As Decimal


        Select Case sys.SysNum
            Case sLaserSysParam.SysLoopStart
                '[Note]:載入資料，初始化
                If IsNothing(mLaserStableTimeStopWatch(sys.StageNo)) = True Then
                    mLaserStableTimeStopWatch(sys.StageNo) = New Stopwatch
                End If

                '[Note]:取定位座標
                mPath(sys.StageNo) = sys.LaserPathRegister(0)
                mPosX(sys.StageNo) = mPath(sys.StageNo).PosX
                mPosY(sys.StageNo) = mPath(sys.StageNo).PosY
                mPosZ(sys.StageNo) = mPath(sys.StageNo).PosZ

                sys.SysNum = 1100

            Case 1100
                sys.SysNum = 1200


            Case 1200
                '[Note]:移動至取像位置_X&Y
                gCMotion.GPSetRunMode(gCMotion.SyncParameter(sys.StageNo), eRunMode.BlendingMode)

                Dim mVelocity As Decimal = gCMotion.AxisParameter(sys.AxisX).Velocity.MaxVel
                With gCMotion.AxisParameter(sys.AxisX).Velocity
                    Dim mDeltaX As Decimal
                    Dim mDeltaY As Decimal
                    mDeltaX = mPosX(sys.StageNo) - gCMotion.GetPositionValue(sys.AxisX)
                    mDeltaY = mPosY(sys.StageNo) - gCMotion.GetPositionValue(sys.AxisY)

                    Dim mDistance As Decimal = Math.Sqrt(mDeltaX ^ 2 + mDeltaY ^ 2)
                    If mGpSCurve = eCurveMode.SCurve Then
                        mAcc(sys.StageNo) = .Acc * .AccRatio * mSCurveRatio
                        mDec(sys.StageNo) = .Dec * .DecRatio * mSCurveRatio
                    Else
                        mAcc(sys.StageNo) = .Acc * .AccRatio
                        mDec(sys.StageNo) = .Dec * .DecRatio
                    End If

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
                End With
                If gCMotion.GpSetVelLow(gCMotion.SyncParameter(sys.StageNo), 0) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036014", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036014), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048014", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048014), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066014", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066014), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073014", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073014), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetVelHigh(gCMotion.SyncParameter(sys.StageNo), mVelocity) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036013", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036013), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048013", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048013), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066013", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066013), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073013", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073013), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetAcc(gCMotion.SyncParameter(sys.StageNo), mAcc(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036011", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036011), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048011", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048011), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066011", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066011), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073011", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073011), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetDec(gCMotion.SyncParameter(sys.StageNo), mDec(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036012", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036012), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048012", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048012), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066012", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066012), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073012", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073012), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.GpSetCurve(gCMotion.SyncParameter(sys.StageNo), mGpSCurve) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036010", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036010), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048010", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048010), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066010", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066010), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073010", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073010), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.GpMoveLinearAbsXYZ(gCMotion.SyncParameter(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 1900

            Case 1900
                '[說明]:等待Table Stop 
                If gCMotion.GpMoveDone(gCMotion.SyncParameter(sys.StageNo)) = CommandStatus.Sucessed Then
                    sys.SysNum = 2000
                End If

            Case 2000
                '[Note]:移動至取像位置_Z
                'ReviseVelocity(sys.AxisZ, mPosZ(sys.StageNo), gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisZ, mPosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069000", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 3000

            Case 3000
                '[說明]:等待Table Stop
                mAxisZState(sys.StageNo) = gCMotion.MotionDone(sys.AxisZ)

                If mAxisZState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If

                mLaserStableTimeStopWatch(sys.StageNo).Restart()
                mLaserStableTimeStartTime(sys.StageNo) = mLaserStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds
                sys.SysNum = 3100

            Case 3100
                Dim mStableTime As Decimal = IIf(gSSystemParameter.StableTime.LaserStableTime > 1000, gSSystemParameter.StableTime.LaserStableTime, 1000) '至少一秒穩定
                If Math.Abs(mLaserStableTimeStartTime(sys.StageNo) - mLaserStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds) > mStableTime Then
                    mLaserStableTimeStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 3200
                End If


            Case 3200
                Dim readerValue As String = ""
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.IsBusy(sys.laserNo) = False Then
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.laserNo, 0) = True Then '發出命令
                        sys.SysNum = 3220
                    Else '讀取失敗
                        gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SetCCDLaserStatus(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY, eDieStatus.LaserFail)
                        mLaserValue(sys.StageNo) = 10
                        sys.SysNum = 3300
                    End If

                End If


            Case 3220 ''通過, 往下走
                Dim readerValue As String = ""
                '判斷是否收到命令
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.IsBusy(sys.laserNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.IsTimeOut(sys.laserNo) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLaserReaderErrorCount(sys.StageNo) = mLaserReaderErrorCount(sys.StageNo) + 1
                        If mLaserReaderErrorCount(sys.StageNo) > 3 Then
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1014004", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1014104", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014104), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1014204", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014204), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1014304", "LaserNonOnFlyModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014304), eMessageLevel.Error)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 3200
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetLaserValue(sys.laserNo, readerValue) Then
                        mLaserValue(sys.StageNo) = CDec(readerValue)
                        sys.SysNum = 3300
                    Else 'Status 尚未為True 重新做loop一次
                        mLaserValue(sys.StageNo) = 10
                    End If
                End If

            Case 3300
                '[Note]:忽略Laser數值
                If gCRecipe.BypassLaserResult = True Then

                    LaserHeightList.Capacity = LaserHeightList.Count + 1
                    LaserHeightList.Add(0)

                    '[說明]:紀錄Laser Reader良率
                    With gSSystemParameter.ProductState
                        .LaserReaderOKPcs = .LaserReaderOKPcs + 1
                        .TotoalLaserReaderPcs = .TotoalLaserReaderPcs + 1
                    End With
                    sys.SysNum = 9000
                Else
                    '[Note]:             數值大於9都是不正常的數字()
                    If Math.Abs(mLaserValue(sys.StageNo)) > ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.ReaderMaxValue Then
                        mLaserReaderErrorCount(sys.StageNo) = mLaserReaderErrorCount(sys.StageNo) + 1
                        If mLaserReaderErrorCount(sys.StageNo) > 3 Then
                            '[說明]:Laser Reader 異常
                            '[說明]:紀錄Scan & LaserReader狀態 &  圖形顏色寫入

                            'errort 超過3次塞入一個極值(當作判斷)
                            LaserHeightList.Capacity = LaserHeightList.Count + 1
                            LaserHeightList.Add(100)
                            sys.SysNum = 9000
                        End If

                    Else
                        'errort 超過3次塞入一個極值(當作判斷)
                        LaserHeightList.Capacity = LaserHeightList.Count + 1
                        LaserHeightList.Add(mLaserValue(sys.StageNo))
                        '[說明]:紀錄Laser Reader良率
                        With gSSystemParameter.ProductState
                            .LaserReaderOKPcs = .LaserReaderOKPcs + 1
                            .TotoalLaserReaderPcs = .TotoalLaserReaderPcs + 1
                        End With
                        sys.SysNum = 9000
                    End If
                End If

            Case 9000
                sys.RunStatus = enmRunStatus.Finish
                Exit Sub

        End Select

    End Sub
    ''' <summary>[接觸式測高流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Private Sub ContactModelAction(ByRef sys As sLaserSysParam)

        Static mLaserStableTimeStopWatch(enmStage.Max) As Stopwatch
        Static mLaserReaderErrorCount(enmStage.Max) As Integer          '[紀錄Laser Reader錯誤次數]
        Static mPosX(enmStage.Max) As Decimal                                   '[目標座標:X]
        Static mPosY(enmStage.Max) As Decimal                                   '[目標座標:Y]
        Static mPosZ(enmStage.Max) As Decimal                                   '[目標座標:Z]
        Static mScene(enmStage.Max) As String                                   '[場景]
        Static mPath(enmStage.Max) As sLaserPath                               '[暫存路徑資訊]
        Static mLaserStableTimeStartTime(enmStage.Max) As Decimal


        Dim mAxisXState(enmStage.Max) As CommandStatus                          '[X軸的狀態]
        Dim mAxisYState(enmStage.Max) As CommandStatus                          '[Y軸的狀態]
        Dim mAxisZState(enmStage.Max) As CommandStatus                          '[Z軸的狀態]
        Static mLaserValue(enmStage.Max) As Decimal                     '[Laser Reader測高取出的數值]

        Dim mAcc(enmStage.Max) As Decimal
        Dim mDec(enmStage.Max) As Decimal

        Select Case sys.SysNum
            Case sLaserSysParam.SysLoopStart
                '[Note]:載入資料，初始化
                If IsNothing(mLaserStableTimeStopWatch(sys.StageNo)) = True Then
                    mLaserStableTimeStopWatch(sys.StageNo) = New Stopwatch
                End If

                '[Note]:取定位座標
                mPath(sys.StageNo) = sys.LaserPathRegister(0)
                mPosX(sys.StageNo) = mPath(sys.StageNo).PosX
                mPosY(sys.StageNo) = mPath(sys.StageNo).PosY
                mPosZ(sys.StageNo) = mPath(sys.StageNo).PosZ

                sys.SysNum = 1100

            Case 1100
                sys.SysNum = 1200


            Case 1200
                '[Note]:移動至取像位置_X&Y
                ''[說明]:載入移動-->移動到起始座標
                gCMotion.GPSetRunMode(gCMotion.SyncParameter(sys.StageNo), eRunMode.BlendingMode)

                Dim mVelocity As Decimal = gCMotion.AxisParameter(sys.AxisX).Velocity.MaxVel
                With gCMotion.AxisParameter(sys.AxisX).Velocity
                    Dim mDeltaX As Decimal
                    Dim mDeltaY As Decimal
                    mDeltaX = mPosX(sys.StageNo) - gCMotion.GetPositionValue(sys.AxisX)
                    mDeltaY = mPosY(sys.StageNo) - gCMotion.GetPositionValue(sys.AxisY)

                    Dim mDistance As Decimal = Math.Sqrt(mDeltaX ^ 2 + mDeltaY ^ 2)
                    If mGpSCurve = eCurveMode.SCurve Then
                        mAcc(sys.StageNo) = .Acc * .AccRatio * mSCurveRatio
                        mDec(sys.StageNo) = .Dec * .DecRatio * mSCurveRatio
                    Else
                        mAcc(sys.StageNo) = .Acc * .AccRatio
                        mDec(sys.StageNo) = .Dec * .DecRatio
                    End If
                    
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
                End With
                If gCMotion.GpSetVelLow(gCMotion.SyncParameter(sys.StageNo), 0) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036014", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036014), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048014", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048014), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066014", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066014), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073014", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073014), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetVelHigh(gCMotion.SyncParameter(sys.StageNo), mVelocity) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036013", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036013), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048013", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048013), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066013", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066013), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073013", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073013), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetAcc(gCMotion.SyncParameter(sys.StageNo), mAcc(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036011", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036011), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048011", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048011), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066011", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066011), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073011", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073011), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetDec(gCMotion.SyncParameter(sys.StageNo), mDec(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036012", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036012), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048012", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048012), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066012", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066012), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073012", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073012), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.GpSetCurve(gCMotion.SyncParameter(sys.StageNo), mGpSCurve) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036010", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036010), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048010", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048010), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066010", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066010), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073010", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073010), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.GpMoveLinearAbsXYZ(gCMotion.SyncParameter(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If


                'ReviseVelocity(sys.AxisY, mPosY(sys.StageNo), gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                'If gCMotion.AbsMove(sys.AxisY, mPosY(sys.StageNo)) <> CommandStatus.Sucessed Then
                '    Select Case sys.StageNo
                '        Case 0
                '            gEqpMsg.AddHistoryAlarm("Error_1031000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                '        Case 1
                '            gEqpMsg.AddHistoryAlarm("Error_1043000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                '        Case 2
                '            gEqpMsg.AddHistoryAlarm("Error_1061000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                '        Case 3
                '            gEqpMsg.AddHistoryAlarm("Error_1068000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                '    End Select
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If
                'ReviseVelocity(sys.AxisX, mPosX(sys.StageNo), gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                'If gCMotion.AbsMove(sys.AxisX, mPosX(sys.StageNo)) <> CommandStatus.Sucessed Then
                '    Select Case sys.StageNo
                '        Case 0
                '            gEqpMsg.AddHistoryAlarm("Error_1030000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                '        Case 1
                '            gEqpMsg.AddHistoryAlarm("Error_1042000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                '        Case 2
                '            gEqpMsg.AddHistoryAlarm("Error_1060000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                '        Case 3
                '            gEqpMsg.AddHistoryAlarm("Error_1067000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                '    End Select
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If
               
                sys.SysNum = 1900

            Case 1900
                '[說明]:等待Table Stop 
                'mAxisXState(sys.StageNo) = gCMotion.MotionDone(sys.AxisX)
                'mAxisYState(sys.StageNo) = gCMotion.MotionDone(sys.AxisY)
                If gCMotion.GpMoveDone(gCMotion.SyncParameter(sys.StageNo)) = CommandStatus.Sucessed Then
                    sys.SysNum = 2000
                End If
                'If mAxisXState(sys.StageNo) <> CommandStatus.Sucessed Then
                '    If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                '        Select Case sys.StageNo
                '            Case 0
                '                gEqpMsg.AddHistoryAlarm("Error_1030004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                '            Case 1
                '                gEqpMsg.AddHistoryAlarm("Error_1042004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                '            Case 2
                '                gEqpMsg.AddHistoryAlarm("Error_1060004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                '            Case 3
                '                gEqpMsg.AddHistoryAlarm("Error_1067004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
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
                '                gEqpMsg.AddHistoryAlarm("Error_1031004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                '            Case 1
                '                gEqpMsg.AddHistoryAlarm("Error_1043004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                '            Case 2
                '                gEqpMsg.AddHistoryAlarm("Error_1061004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                '            Case 3
                '                gEqpMsg.AddHistoryAlarm("Error_1068004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                '        End Select
                '        sys.RunStatus = enmRunStatus.Alarm
                '        Exit Sub
                '    Else
                '        Exit Sub
                '    End If
                'End If
                'sys.SysNum = 2000

            Case 2000
                '[Note]:移動至取像位置_Z
                'ReviseVelocity(sys.AxisZ, mPosZ(sys.StageNo), gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisZ, mPosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 3000

            Case 3000
                '[說明]:等待Table Stop
                mAxisZState(sys.StageNo) = gCMotion.MotionDone(sys.AxisZ)

                If mAxisZState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If

                mLaserStableTimeStopWatch(sys.StageNo).Restart()
                mLaserStableTimeStartTime(sys.StageNo) = mLaserStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds
                '推接觸式汽缸IO
                gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, True)
                Call gDOCollection.RefreshDO()
                sys.SysNum = 3100

            Case 3100
                Dim mStableTime As Decimal = IIf(gSSystemParameter.StableTime.LaserStableTime > 1000, gSSystemParameter.StableTime.LaserStableTime, 1000) '至少一秒穩定
                If Math.Abs(mLaserStableTimeStartTime(sys.StageNo) - mLaserStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds) > mStableTime Then
                    mLaserStableTimeStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 3110
                End If


            Case 3110
                'System.Threading.Thread.CurrentThread.Join(1000)
                sys.SysNum = 3200
                '判斷汽缸狀態__次數(?)
            Case 3120


            Case 3200
                Dim readerValue As String = ""
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.IsBusy(sys.laserNo) = False Then
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.laserNo, 0) = True Then '發出命令
                        sys.SysNum = 3220
                    Else '讀取失敗
                        mLaserValue(sys.StageNo) = 10
                        sys.SysNum = 3300
                    End If

                End If

            Case 3220 ''通過, 往下走
                Dim readerValue As String = ""
                '判斷是否收到命令
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.IsBusy(sys.laserNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.IsTimeOut(sys.laserNo) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLaserReaderErrorCount(sys.StageNo) = mLaserReaderErrorCount(sys.StageNo) + 1
                        If mLaserReaderErrorCount(sys.StageNo) > 3 Then
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1014004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1014104", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014104), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1014204", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014204), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1014304", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014304), eMessageLevel.Error)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 3200
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetLaserValue(sys.laserNo, readerValue) Then
                        mLaserValue(sys.StageNo) = CDec(readerValue)
                        sys.SysNum = 3300
                    Else 'Status 尚未為True 重新做loop一次
                        mLaserValue(sys.StageNo) = 10
                    End If
                End If

            Case 3300
                '[Note]:忽略Laser數值
                If gCRecipe.BypassLaserResult = True Then
                    gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SLaserValue(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY).ZHigh(0) = 0
                    gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SLaserValue(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY).LaserFinish(0) = True

                    '[說明]:紀錄Laser Reader良率
                    With gSSystemParameter.ProductState
                        .LaserReaderOKPcs = .LaserReaderOKPcs + 1
                        .TotoalLaserReaderPcs = .TotoalLaserReaderPcs + 1
                    End With
                    sys.SysNum = 8000
                Else
                    '[Note]:             數值大於9都是不正常的數字()
                    If Math.Abs(mLaserValue(sys.StageNo)) > ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.ReaderMaxValue Then
                        mLaserReaderErrorCount(sys.StageNo) = mLaserReaderErrorCount(sys.StageNo) + 1
                        If mLaserReaderErrorCount(sys.StageNo) > 3 Then
                            '[說明]:Laser Reader 異常
                            '[說明]:紀錄Scan & LaserReader狀態 &  圖形顏色寫入

                            With gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).ChipState(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY)
                                .DieState = enmDieState.NG
                                .NeedUpdate = True
                            End With
                            'Call DetermineResultState(mLaserIndex(sys.StageNo), sys)

                            '[說明]:判斷NG 就不用做劃膠
                            With gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SRecipePos(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY)
                                .IsByPassDispensingAction = True
                            End With

                            '[Note]:更新目前處理的狀態
                            gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SBinMapData(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY).Status = eDieStatus.LaserFail
                            'Call WriteDieStatusForMappingData("GG", eDataType.eNonDispensing, sys.StageNo, mPath(sys.StageNo).NodeName, mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY, eDieStatus.LaserFail)
                            sys.SysNum = 8000
                        End If

                    Else
                        With gStageMap(sys.StageNo).Node(mPath(sys.StageNo).NodeName).SLaserValue(mPath(sys.StageNo).IndexX, mPath(sys.StageNo).IndexY)
                            .ZHigh(0) = mLaserValue(sys.StageNo)
                            .LaserFinish(0) = True
                        End With
                        '[說明]:紀錄Laser Reader良率
                        With gSSystemParameter.ProductState
                            .LaserReaderOKPcs = .LaserReaderOKPcs + 1
                            .TotoalLaserReaderPcs = .TotoalLaserReaderPcs + 1
                        End With
                        sys.SysNum = 8000
                    End If
                End If


                '收回接觸式汽缸IO
            Case 8000
                '汽缸動作(收回)
                gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
                Call gDOCollection.RefreshDO()
                mLaserStableTimeStopWatch(sys.StageNo).Restart()
                mLaserStableTimeStartTime(sys.StageNo) = mLaserStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds
                sys.SysNum = 8100

            Case 8100
                If Math.Abs(mLaserStableTimeStartTime(sys.StageNo) - mLaserStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds) > 1000 Then
                    mLaserStableTimeStopWatch(sys.StageNo).Stop()
                    '汽缸動作_end
                    sys.SysNum = 9000

                    '判斷汽缸狀態__次數(?)
                End If
               
            Case 9000
                sys.RunStatus = enmRunStatus.Finish
                Exit Sub

        End Select

    End Sub
    ''' <summary>[接觸式測高流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Private Sub ContactModelActionNonArray(ByRef sys As sLaserSysParam, ByVal LaserHeightList As List(Of Decimal))

        Static mLaserStableTimeStopWatch(enmStage.Max) As Stopwatch
        Static mLaserReaderErrorCount(enmStage.Max) As Integer          '[紀錄Laser Reader錯誤次數]
        Static mPosX(enmStage.Max) As Decimal                                   '[目標座標:X]
        Static mPosY(enmStage.Max) As Decimal                                   '[目標座標:Y]
        Static mPosZ(enmStage.Max) As Decimal                                   '[目標座標:Z]
        Static mScene(enmStage.Max) As String                                   '[場景]
        Static mPath(enmStage.Max) As sLaserPath                               '[暫存路徑資訊]
        Static mLaserStableTimeStartTime(enmStage.Max) As Decimal
        Dim mAxisXState(enmStage.Max) As CommandStatus                          '[X軸的狀態]
        Dim mAxisYState(enmStage.Max) As CommandStatus                          '[Y軸的狀態]
        Dim mAxisZState(enmStage.Max) As CommandStatus                          '[Z軸的狀態]
        Static mLaserValue(enmStage.Max) As Decimal                     '[Laser Reader測高取出的數值]

        Dim mAcc(enmStage.Max) As Decimal
        Dim mDec(enmStage.Max) As Decimal

        Select Case sys.SysNum
            Case sLaserSysParam.SysLoopStart
                '[Note]:載入資料，初始化
                If IsNothing(mLaserStableTimeStopWatch(sys.StageNo)) = True Then
                    mLaserStableTimeStopWatch(sys.StageNo) = New Stopwatch
                End If

                '[Note]:取定位座標
                mPath(sys.StageNo) = sys.LaserPathRegister(0)
                mPosX(sys.StageNo) = mPath(sys.StageNo).PosX
                mPosY(sys.StageNo) = mPath(sys.StageNo).PosY
                mPosZ(sys.StageNo) = mPath(sys.StageNo).PosZ

                sys.SysNum = 1100

            Case 1100
                sys.SysNum = 1200


            Case 1200
                '[Note]:移動至取像位置_X&Y
                ''[說明]:載入移動-->移動到起始座標
                gCMotion.GPSetRunMode(gCMotion.SyncParameter(sys.StageNo), eRunMode.BlendingMode)

                Dim mVelocity As Decimal = gCMotion.AxisParameter(sys.AxisX).Velocity.MaxVel
                With gCMotion.AxisParameter(sys.AxisX).Velocity
                    Dim mDeltaX As Decimal
                    Dim mDeltaY As Decimal
                    mDeltaX = mPosX(sys.StageNo) - gCMotion.GetPositionValue(sys.AxisX)
                    mDeltaY = mPosY(sys.StageNo) - gCMotion.GetPositionValue(sys.AxisY)

                    Dim mDistance As Decimal = Math.Sqrt(mDeltaX ^ 2 + mDeltaY ^ 2)
                    If mGpSCurve = eCurveMode.SCurve Then
                        mAcc(sys.StageNo) = .Acc * .AccRatio * mSCurveRatio
                        mDec(sys.StageNo) = .Dec * .DecRatio * mSCurveRatio
                    Else
                        mAcc(sys.StageNo) = .Acc * .AccRatio
                        mDec(sys.StageNo) = .Dec * .DecRatio
                    End If

                    Premtek.CDispensingMath.GetCrossVelocity(gSSystemParameter.MaxCrossDeviceVelocity, mAcc(sys.StageNo), mDec(sys.StageNo), mDistance, 0, mVelocity)
                    If mVelocity = 0 Then
                        mVelocity = gSSystemParameter.MaxCrossDeviceVelocity
                    End If
                    gCMotion.SyncParameter(sys.StageNo).TargetPos(0) = mPosX(sys.StageNo)
                    gCMotion.SyncParameter(sys.StageNo).TargetPos(1) = mPosY(sys.StageNo)
                    gCMotion.SyncParameter(sys.StageNo).TargetPos(2) = gCMotion.GetPositionValue(sys.AxisZ)
                    gCMotion.SyncParameter(sys.StageNo).Velocity.VelLow = 0
                    gCMotion.SyncParameter(sys.StageNo).Velocity.VelHigh = mVelocity
                End With
                If gCMotion.GpSetVelLow(gCMotion.SyncParameter(sys.StageNo), 0) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036014", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036014), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048014", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048014), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066014", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066014), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073014", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073014), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetVelHigh(gCMotion.SyncParameter(sys.StageNo), mVelocity) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036013", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036013), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048013", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048013), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066013", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066013), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073013", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073013), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetAcc(gCMotion.SyncParameter(sys.StageNo), mAcc(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036011", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036011), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048011", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048011), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066011", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066011), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073011", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073011), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                If gCMotion.GpSetDec(gCMotion.SyncParameter(sys.StageNo), mDec(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036012", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036012), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048012", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048012), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066012", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066012), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073012", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073012), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.GpSetCurve(gCMotion.SyncParameter(sys.StageNo), mGpSCurve) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036010", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036010), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048010", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048010), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066010", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066010), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073010", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073010), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.GpMoveLinearAbsXYZ(gCMotion.SyncParameter(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1036000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1036000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1048000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1048000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1066000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1066000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1073000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1073000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 1900

            Case 1900
                '[說明]:等待Table Stop 
                'mAxisXState(sys.StageNo) = gCMotion.MotionDone(sys.AxisX)
                'mAxisYState(sys.StageNo) = gCMotion.MotionDone(sys.AxisY)
                If gCMotion.GpMoveDone(gCMotion.SyncParameter(sys.StageNo)) = CommandStatus.Sucessed Then
                    sys.SysNum = 2000
                End If

            Case 2000
                '[Note]:移動至取像位置_Z
                'ReviseVelocity(sys.AxisZ, mPosZ(sys.StageNo), gSSystemParameter.MaxCrossDeviceVelocity) 'Soni + 2017.08.23 修改速度為優化速度
                If gCMotion.AbsMove(sys.AxisZ, mPosZ(sys.StageNo)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069000", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 3000

            Case 3000
                '[說明]:等待Table Stop
                mAxisZState(sys.StageNo) = gCMotion.MotionDone(sys.AxisZ)

                If mAxisZState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If

                mLaserStableTimeStopWatch(sys.StageNo).Restart()
                mLaserStableTimeStartTime(sys.StageNo) = mLaserStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds
                '推接觸式汽缸IO
                gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, True)
                Call gDOCollection.RefreshDO()
                sys.SysNum = 3100

            Case 3100
                Dim mStableTime As Decimal = IIf(gSSystemParameter.StableTime.LaserStableTime > 1000, gSSystemParameter.StableTime.LaserStableTime, 1000) '至少一秒穩定
                If Math.Abs(mLaserStableTimeStartTime(sys.StageNo) - mLaserStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds) > mStableTime Then
                    mLaserStableTimeStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 3110
                End If


            Case 3110
                'System.Threading.Thread.CurrentThread.Join(1000)
                sys.SysNum = 3200
                '判斷汽缸狀態__次數(?)
            Case 3120


            Case 3200
                Dim readerValue As String = ""
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.IsBusy(sys.laserNo) = False Then
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.laserNo, 0) = True Then '發出命令
                        sys.SysNum = 3220
                    Else '讀取失敗
                        mLaserValue(sys.StageNo) = 10
                        sys.SysNum = 3300
                    End If

                End If

            Case 3220 ''通過, 往下走
                Dim readerValue As String = ""
                '判斷是否收到命令
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.IsBusy(sys.laserNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.IsTimeOut(sys.laserNo) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mLaserReaderErrorCount(sys.StageNo) = mLaserReaderErrorCount(sys.StageNo) + 1
                        If mLaserReaderErrorCount(sys.StageNo) > 3 Then
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1014004", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014004), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1014104", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014104), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1014204", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014204), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1014304", "ContactModelAction", sys.SysNum, gMsgHandler.GetMessage(Error_1014304), eMessageLevel.Error)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 3200
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetLaserValue(sys.laserNo, readerValue) Then
                        mLaserValue(sys.StageNo) = CDec(readerValue)
                        sys.SysNum = 3300
                    Else 'Status 尚未為True 重新做loop一次
                        mLaserValue(sys.StageNo) = 10
                    End If
                End If

            Case 3300
                '[Note]:忽略Laser數值
                If gCRecipe.BypassLaserResult = True Then

                    LaserHeightList.Capacity = LaserHeightList.Count + 1
                    LaserHeightList.Add(0)

                    '[說明]:紀錄Laser Reader良率
                    With gSSystemParameter.ProductState
                        .LaserReaderOKPcs = .LaserReaderOKPcs + 1
                        .TotoalLaserReaderPcs = .TotoalLaserReaderPcs + 1
                    End With
                    sys.SysNum = 8000
                Else
                    '[Note]:             數值大於9都是不正常的數字()
                    If Math.Abs(mLaserValue(sys.StageNo)) > ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.ReaderMaxValue Then
                        mLaserReaderErrorCount(sys.StageNo) = mLaserReaderErrorCount(sys.StageNo) + 1
                        If mLaserReaderErrorCount(sys.StageNo) > 3 Then
                            '[說明]:Laser Reader 異常
                            '[說明]:紀錄Scan & LaserReader狀態 &  圖形顏色寫入

                            'errort 超過3次塞入一個極值(當作判斷)
                            LaserHeightList.Capacity = LaserHeightList.Count + 1
                            LaserHeightList.Add(100)

                            sys.SysNum = 8000
                        End If

                    Else
                        LaserHeightList.Capacity = LaserHeightList.Count + 1
                        LaserHeightList.Add(mLaserValue(sys.StageNo))

                        '[說明]:紀錄Laser Reader良率
                        With gSSystemParameter.ProductState
                            .LaserReaderOKPcs = .LaserReaderOKPcs + 1
                            .TotoalLaserReaderPcs = .TotoalLaserReaderPcs + 1
                        End With
                        sys.SysNum = 8000
                    End If
                End If


                '收回接觸式汽缸IO
            Case 8000
                '汽缸動作(收回)
                gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
                Call gDOCollection.RefreshDO()
                mLaserStableTimeStopWatch(sys.StageNo).Restart()
                mLaserStableTimeStartTime(sys.StageNo) = mLaserStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds
                sys.SysNum = 8100

            Case 8100
                If Math.Abs(mLaserStableTimeStartTime(sys.StageNo) - mLaserStableTimeStopWatch(sys.StageNo).ElapsedMilliseconds) > 1000 Then
                    mLaserStableTimeStopWatch(sys.StageNo).Stop()
                    '汽缸動作_end
                    sys.SysNum = 9000

                    '判斷汽缸狀態__次數(?)
                End If

            Case 9000
                sys.RunStatus = enmRunStatus.Finish
                Exit Sub

        End Select

    End Sub

    Private Sub WriteDieStatusForMappingData(p1 As String, eDataType As eDataType, StageNo As Integer, NodeName As String, IndexX As Integer, IndexY As Integer, eDieStatus As eDieStatus)
        Throw New NotImplementedException
    End Sub

End Module

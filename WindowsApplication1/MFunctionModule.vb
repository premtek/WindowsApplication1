﻿Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Math
Imports ProjectFeedback
Imports ProjectAOI
Imports ProjectRecipe
Imports ProjectMotion
Imports ProjectIO
Imports ProjectCore
Imports ProjectConveyor
Imports ProjectTriggerBoard
Imports ProjectLaserInterferometer
Imports MapData
Imports ProjectValveController
Imports WetcoConveyor

Module MFunctionModule
    '***************************************Fenix+ 2015/12/11********************************************
    ' ''' <summary>[判斷是否走手動模式]</summary>
    ' ''' <remarks></remarks>
    'Public gMachineIsManual As Boolean
    ' ''' <summary>
    ' ''' [Pattern資料陣列]
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public gPattern As New List(Of CRecipePattern)
    ''' <summary>
    ''' [PatternList使用，只記錄過出現的Pattern，不做排序以及修改，其順序為gPattern掃描順序]
    ''' </summary>
    ''' <remarks></remarks>
    Public PatternList As New List(Of CRecipePattern)

    ''' <summary>[紀錄各自的相關座標]</summary>
    ''' <remarks></remarks>
    Public gProtectData(enmStage.Max) As sProtectData


    ''' <summary>[R側是否可以開始作業(在同一層的條件下，R側優先權永遠大於L側)]</summary>
    ''' <remarks></remarks>
    Public gIsLSideWorking(enmMachineStation.MaxMachine) As Boolean
    ''' <summary>[紀錄做到哪一層了，用來判斷是否要閃避]</summary>
    ''' <remarks></remarks>
    Public gNodeLevel(enmStage.Max) As Integer
    ''' <summary>[L側呼叫R側閃開(退至安全區)]</summary>
    ''' <remarks></remarks>
    Public gIsRSideNeedGoToSafePos(enmMachineStation.MaxMachine) As Boolean
    ''' <summary>[R側呼叫L側閃開(退至安全區)]</summary>
    ''' <remarks></remarks>
    Public gIsLSideNeedGoToSafePos(enmMachineStation.MaxMachine) As Boolean

    ''TODO: 請確認該物件是否要全域 用於AutoValveCalibrationAction
    ' ''' <summary>[計算CCD腳位切換之時間] </summary>
    ' ''' <remarks></remarks>
    'Private mCCDIOStopWatch As New Stopwatch
    ''TODO: 散裝參數找地方打包存放 目前用在DispStage_ChangeGlueAction、DispStage_PurgeAction、DispStage_ClearGlueAction、DispStage_DispenserAutoSearchAction、DispStage_AutoValveCalibrationAction
    ' ''' <summary>[紀錄檢查Sensor之起始時間]</summary>
    ' ''' <remarks></remarks>
    'Private mCheckSensorStart As Decimal

    'Private mCcdWaitIOTime As Decimal

    ''' <summary>
    ''' A機Map data位址 : 手動模式下使用
    ''' </summary>
    Public gMapDataPathA As String
    ''' <summary>
    ''' B機Map data位址 : 手動模式下使用
    ''' </summary>
    Public gMapDataPathB As String
    ''' <summary>
    ''' Loader Cassette A 的 Map data 位址陣列 : 自動模式下使用
    ''' </summary>
    Public gCaseteAMapDataList(30) As String
    ''' <summary>
    ''' Loader Cassette B 的 Map data 位址陣列 : 自動模式下使用
    ''' </summary>
    Public gCaseteBMapDataList(30) As String
    ''' <summary>
    ''' 使用自動(true)/手動模式(false) Map data位址
    ''' </summary>
    Public gAutoMapPath As Boolean = True

    Public gMapInfo As CMapInfo   '20170612 Toby_add

    ' ''' <summary>[紀錄使用哪幾個閥]</summary>
    ' ''' <remarks></remarks>
    'Public mUseDispenserState As enmUseDispenserNo

    ' ''' <summary>[判斷移置換膠位置後有沒有要移回來]</summary>
    ' ''' <remarks></remarks>
    'Public gblnChangeGlueComeBack As Boolean = True
    ' ''' <summary>[判斷移置Purge位置後有沒有要移回來] </summary>
    ' ''' <remarks></remarks>
    'Public gblnPurgeComeBack As Boolean = True

    ' ''' <summary>[判斷是否需要更新產品資訊(丟給CASTEC的資料)] </summary>
    ' ''' <remarks></remarks>
    'Public gblnUpdateProductState As Boolean
    ' ''' <summary>[清完膠需不需要回到原先的位置]</summary>
    ' ''' <remarks></remarks>
    'Public gblnClearGlueComeBack As Boolean

    ' ''' <summary>[解除煞車馬達] </summary>
    ' ''' <remarks></remarks>
    'Public gblnMotorUnlock As Boolean = False
    ' ''' <summary>[在Pass IO的情況下Auto Run不停 用來測試馬達]</summary>
    ' ''' <remarks></remarks>
    'Public gblnAutoRunNoStopForMotorTest As Boolean = False

    ' ''' <summary>[校正哪一支閥] </summary>
    ' ''' <remarks></remarks>
    'Public genmAutoValveCalibrationDispenserNo As enmValve

    ''' <summary>[預熱等待時間之計時器(入料完成到開始點膠的這段時間)]</summary>
    ''' <remarks></remarks>
    Public gPriorHeatTimer(enmMachineStation.MaxMachine) As Stopwatch

    ''' <summary>[協助記錄是否途中有執行Purge的動作]</summary>
    ''' <remarks></remarks>
    Public gIsOnPurge(enmStage.Max) As Boolean
    ''' <summary>[點膠是否有使用ValveNo1]</summary>
    ''' <remarks></remarks>
    Public gIsUseValveNo1(enmStage.Max) As Boolean
    ''' <summary>[點膠是否有使用ValveNo2]</summary>
    ''' <remarks></remarks>
    Public gIsUseValveNo2(enmStage.Max) As Boolean

    '****************************************************************************************************

    ' ''' <summary>[入料的情況與預期此批入料要的做工作內容]</summary>
    ' ''' <remarks></remarks>
    'Private mLoadingType As enmLoadingType

    ''' <summary>[Conveyor 狀態]</summary>
    ''' <remarks></remarks>
    Private Enum eConveyorStatus
        ''' <summary>[A機作動/流道A作動]</summary>
        ''' <remarks></remarks>
        Station_A = 0
        ''' <summary>[A、B機作動/流道A、B作動]</summary>
        ''' <remarks></remarks>
        Station_AB = 1
        ''' <summary>[B機作動/流道B作動]</summary>
        ''' <remarks></remarks>
        Station_B = 2
        ''' <summary>[不用動]</summary>
        ''' <remarks></remarks>
        Station_None = 3
    End Enum

#Region "330A"
    Function Conveyor_LoadAAction(ByRef sys As sSysParam) As enmRunStatus
        With sys
            Select Case .SysNum
                Case sSysParam.SysLoopStart
                    sys.Act(eAct.LoadA).RunStatus = enmRunStatus.Finish
                    Return enmRunStatus.Finish
            End Select
        End With
        Return enmRunStatus.Running
    End Function

    Function Conveyor_LoadBAction(ByRef sys As sSysParam) As enmRunStatus
        With sys
            Select Case .SysNum
                Case sSysParam.SysLoopStart
                    sys.Act(eAct.LoadB).RunStatus = enmRunStatus.Finish
                    Return enmRunStatus.Finish
            End Select
        End With
        Return enmRunStatus.Running
    End Function

    Function Conveyor_UnloadAAction(ByRef sys As sSysParam) As enmRunStatus
        With sys
            Select Case .SysNum
                Case sSysParam.SysLoopStart
                    sys.Act(eAct.UnloadA).RunStatus = enmRunStatus.Finish
                    Return enmRunStatus.Finish
            End Select
        End With
        Return enmRunStatus.Running
    End Function

    Function Conveyor_UnloadBAction(ByRef sys As sSysParam) As enmRunStatus
        With sys
            Select Case .SysNum
                Case sSysParam.SysLoopStart
                    sys.Act(eAct.UnloadB).RunStatus = enmRunStatus.Finish
                    Return enmRunStatus.Finish
            End Select
        End With
        Return enmRunStatus.Running
    End Function

#End Region

#Region "膠材管理"
    ''' <summary>[膠材1計數超過上限]</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsPasteLifeCountOver(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode) As Boolean
        If gCRecipe Is Nothing Then '未知的Recipe物件
            Return False
        End If
        If stageNo < enmStage.No1 Then
            Return False
        End If
        If valveNo < eValveWorkMode.Valve1 Then '不存在的閥
            Return False
        End If
        If stageNo >= gCRecipe.StageParts.Count Then
            Return False
        End If
        If gCRecipe.StageParts(stageNo).PasteName(valveNo) = "" Then '未設定膠材名稱
            Return False
        End If
        If gPasteDataBase.ContainsKey(gCRecipe.StageParts(stageNo).PasteName(valveNo)) = False Then
            Return False
        End If
        If gPasteDataBase(gCRecipe.StageParts(stageNo).PasteName(valveNo)).PotLifeCountEnable = False Then '膠材壽命判定未啟用
            Return False
        End If
        If gSSystemParameter.StageParts.PasteLifeTime(stageNo) Is Nothing Then '壽命管理物件不存在
            Return False
        End If
        If gSSystemParameter.StageParts.PasteLifeTime(stageNo).LifeCountAlarm(valveNo) = True Then '膠材壽命逾時警報, 避免重複跳
            Return False
        End If
        If gPasteDataBase(gCRecipe.StageParts(stageNo).PasteName(valveNo)).PotLifeCount <= 0 Then '設定值無法計數
            Return False
        End If
        If gSSystemParameter.StageParts.PasteLifeTime(stageNo).DotsCount(valveNo) > gPasteDataBase(gCRecipe.StageParts(stageNo).PasteName(valveNo)).PotLifeCount Then '計數次數到達
            Return True
        End If

        Return False
    End Function


    ''' <summary>膠材壽命超過上限</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsPasteLifeTimeOut(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode) As Boolean
        If gCRecipe Is Nothing Then '未知的Recipe物件
            Return False
        End If
        If stageNo < enmStage.No1 Then
            Return False
        End If
        If valveNo < eValveWorkMode.Valve1 Then '不存在的閥
            Return False
        End If
        If gCRecipe.StageParts(stageNo).PasteName(valveNo) = "" Then '未設定膠材名稱
            Return False
        End If
        If gPasteDataBase.ContainsKey(gCRecipe.StageParts(stageNo).PasteName(valveNo)) = False Then
            Return False
        End If
        If gPasteDataBase(gCRecipe.StageParts(stageNo).PasteName(valveNo)).PotLifeEnable = False Then '膠材壽命判定未啟用
            Return False
        End If
        If gSSystemParameter.StageParts.PasteLifeTime(stageNo) Is Nothing Then '壽命管理物件不存在
            Return False
        End If
        If gSSystemParameter.StageParts.PasteLifeTime(stageNo).LifeTimeAlarm(valveNo) = True Then '膠材壽命逾時警報, 避免重複跳
            Return False
        End If

        'If (gPasteDataBase(gCRecipe.StageParts(stageNo).PasteName(valveNo)).PotLife * 3600 - DateDiff(DateInterval.Second, gSSystemParameter.StageParts.PasteLifeTime(stageNo).StartLifeTime(valveNo), Now)) Then
        '    Return True
        'End If
        If (DateDiff(DateInterval.Second, gSSystemParameter.StageParts.PasteLifeTime(stageNo).StartLifeTime(valveNo), Now) > gPasteDataBase(gCRecipe.StageParts(stageNo).PasteName(valveNo)).PotLife * 3600) Then
            Return True
        End If

        Return False

    End Function

    Function GetPasteLifeCount(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode) As String
        If gCRecipe Is Nothing Then
            Return "------" '"Unknown Recipe"
        End If

        If gCRecipe.StageParts(stageNo).PasteName(valveNo) = "" Then
            Return "------" '"Unknown Paste"
        End If
        If gPasteDataBase.ContainsKey(gCRecipe.StageParts(stageNo).PasteName(valveNo)) = False Then
            Return "------" '"Unknown Paste"
        End If
        If Not gPasteDataBase(gCRecipe.StageParts(stageNo).PasteName(valveNo)).PotLifeCountEnable Then
            Return "------" '"Unused"
        End If
        Return gPasteDataBase(gCRecipe.StageParts(stageNo).PasteName(valveNo)).PotLifeCount
    End Function
    ''' <summary>取得膠材剩餘壽命</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPasteLifeTime(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode) As String
        If gCRecipe Is Nothing Then
            Return "------" '"Unknown Recipe"
        End If

        If gCRecipe.StageParts(stageNo).PasteName(valveNo) = "" Then
            Return "------" '"Unknown Paste"
        End If

        If gPasteDataBase.ContainsKey(gCRecipe.StageParts(stageNo).PasteName(valveNo)) = False Then
            Return "------" '"Unknown Paste"
        End If

        If Not gPasteDataBase(gCRecipe.StageParts(stageNo).PasteName(valveNo)).PotLifeEnable Then
            Return "------" '"Unused"
        End If

        With gSSystemParameter.StageParts.PasteLifeTime(stageNo)
            Dim Glue1Life As New TimeSpan(0, 0, gPasteDataBase(gCRecipe.StageParts(stageNo).PasteName(valveNo)).PotLife * 3600)
            Glue1Life -= DateTime.Now - .StartLifeTime(valveNo)
            If Glue1Life.Seconds < 0 Then
                Return "Expired."
            Else
                Return (Glue1Life.Days * 24 + Glue1Life.Hours).ToString("00") & ":" & Glue1Life.Minutes.ToString("00") & ":" & Glue1Life.Seconds.ToString("00")
            End If

        End With
    End Function

#End Region

    'Function Machine_ManualProduce(ByVal sys As sSysParam, Optional ByVal IsReadPII As Boolean = False) As enmRunStatus
    '    With sys
    '        '手動生產模式，User自行承靠
    '        Select Case .SysNum
    '            Case sSysParam.SysLoopStart
    '                'gMachineIsManual = True
    '                'TODO Conveyor操作UI，需設定chuck吸真空，加熱參數，並手動頂起
    '                .SysNum = 1100
    '            Case 1100
    '                .SysNum = 1200

    '            Case 1200
    '                'TODO Jeff手動設定MappingData介面與資料[後補手動選擇，目前是自動選一個]
    '                Dim FilePath As String = GetNewsMapping(gCRecipe.ProductType, True)

    '                If (CoverMapData(sys.MachineNo, FilePath) = False) Then
    '                    gEqpMsg.Add("Machine_ManualProduce", Error_1025000, eMessageLevel.Warning)
    '                    sys.Act(eAct.ManualAction).RunStatus = enmRunStatus.Alarm
    '                    gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
    '                    Return enmRunStatus.Alarm
    '                End If
    '                .SysNum = 2000

    '            Case 2000
    '                'TODO Jeff手動設定Global Alignment資料[手動設定定位點，及旋轉量，不用設定偏移量]，且須將定位資料填入STAGEMAP
    '                '範例 將設定好的定位點及旋轉量塞入STAGEMAP，請注意遵守手動Recipe設定
    '                'Dim LeftGlobalNode As CPatternMap = gStageMap(enmStage.No1 + sys.MachineNo * 2).Node(gCRecipe.ScanTraversal(enmStage.No1 + sys.MachineNo * 2)(0))
    '                'Dim RightGlobalNode As CPatternMap = gStageMap(enmStage.No2 + sys.MachineNo * 2).Node(gCRecipe.ScanTraversal(enmStage.No2 + sys.MachineNo * 2)(0))
    '                'LeftGlobalNode.SRecipePos(0, 0).RealBasicPosX = ""
    '                'LeftGlobalNode.SRecipePos(0, 0).RealBasicPosY = ""
    '                'LeftGlobalNode.SRecipePos(0, 0).RealBasicPosTh = ""
    '                'RightGlobalNode.SRecipePos(0, 0).RealBasicPosX = ""
    '                'RightGlobalNode.SRecipePos(0, 0).RealBasicPosY = ""
    '                'RightGlobalNode.SRecipePos(0, 0).RealBasicPosTh = ""
    '                .SysNum = 3000

    '            Case 3000
    '                .SysNum = 3100
    '            Case 3100
    '                gSYS(eSys.DispStage1 + .MachineNo * 2).Command = eSysCommand.CCDFix
    '                gSYS(eSys.DispStage2 + .MachineNo * 2).Command = eSysCommand.CCDFix
    '                .SysNum = 3500
    '            Case 3500
    '                'TODO Jeff介面顯示產品Mapping Data供確認、修改(可Bypass)
    '                .SysNum = 4000
    '            Case 4000
    '                If gSYS(eSys.DispStage1 + .MachineNo * 2).RunStatus = enmRunStatus.Finish And gSYS(eSys.DispStage2 + .MachineNo * 2).RunStatus = enmRunStatus.Finish Then
    '                    gSYS(eSys.DispStage1 + sys.MachineNo * 2).Act(eAct.CCDSCanFix).RunStatus = enmRunStatus.Finish
    '                    gSYS(eSys.DispStage2 + sys.MachineNo * 2).Act(eAct.CCDSCanFix).RunStatus = enmRunStatus.Finish
    '                    gSYS(eSys.MachineA + sys.MachineNo).Act(eAct.CCDSCanFix).RunStatus = enmRunStatus.Finish
    '                    gSYS(eSys.DispStage1 + .MachineNo * 2).Command = eSysCommand.LaserReader
    '                    gSYS(eSys.DispStage2 + .MachineNo * 2).Command = eSysCommand.LaserReader
    '                    .SysNum = 5000
    '                End If
    '            Case 5000
    '                If gSYS(eSys.DispStage1 + .MachineNo * 2).RunStatus = enmRunStatus.Finish And gSYS(eSys.DispStage2 + .MachineNo * 2).RunStatus = enmRunStatus.Finish Then
    '                    gSYS(eSys.DispStage1 + sys.MachineNo * 2).Act(eAct.LaserReader).RunStatus = enmRunStatus.Finish
    '                    gSYS(eSys.DispStage2 + sys.MachineNo * 2).Act(eAct.LaserReader).RunStatus = enmRunStatus.Finish
    '                    gSYS(eSys.MachineA + sys.MachineNo).Act(eAct.LaserReader).RunStatus = enmRunStatus.Finish

    '                    gSYS(eSys.DispStage1 + .MachineNo * 2).Command = eSysCommand.Dispensing
    '                    gSYS(eSys.DispStage2 + .MachineNo * 2).Command = eSysCommand.Dispensing
    '                    .SysNum = 9000
    '                End If
    '            Case 9000
    '                '由於不UnLoad，故點完膠就結束
    '                If gSYS(eSys.DispStage1 + .MachineNo * 2).RunStatus = enmRunStatus.Finish And gSYS(eSys.DispStage2 + .MachineNo * 2).RunStatus = enmRunStatus.Finish Then
    '                    'gMachineIsManual = False

    '                    gSYS(eSys.DispStage1 + sys.MachineNo * 2).Act(eAct.Dispensing).RunStatus = enmRunStatus.Finish
    '                    gSYS(eSys.DispStage2 + sys.MachineNo * 2).Act(eAct.Dispensing).RunStatus = enmRunStatus.Finish
    '                    gSYS(eSys.MachineA + sys.MachineNo).Act(eAct.Dispensing).RunStatus = enmRunStatus.Finish
    '                    Return enmRunStatus.Finish
    '                End If

    '        End Select
    '    End With
    '    Return enmRunStatus.Running
    'End Function

    Public Function RoundStringToNumber(ByVal str As String) As Integer
        Dim numbers As String = "0123456789" '//("[0-9]") 
        Dim letters As String = "abcdefghijklmnopqrstuvwxyz"
        '[Note]a=10, b=11, c=12 .....

        Dim mStr As String
        mStr = str.ToLower
        If numbers.Contains(mStr) Then
            Return CInt(mStr)
        End If

        If letters.Contains(mStr) Then
            '轉ASC後回傳
            '97		a
            Dim ascii() As Byte = System.Text.Encoding.Default.GetBytes(mStr)
            Dim count As Integer = ascii.Length
            Dim hexArray(count - 1) As String
            For idx As Integer = 0 To count - 1
                hexArray(idx) = ascii(idx).ToString("x2")
            Next
            Return CInt(hexArray(0) - 51)
        End If
        Return 0
    End Function
    ''' <summary>
    ''' [MappingData轉換到StageMap]
    ''' </summary>
    ''' <param name="Map"></param>
    ''' <param name="StageMap"></param>
    ''' <param name="RecipeNode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function MapDataCoverToStageMap(ByVal Map As clsMapData, ByRef StageMap As CPatternMap, ByVal RecipeNode As CRecipeNode) As Boolean
        Try
            Dim nodeArray As CMultiArrayAdapter = New CMultiArrayAdapter(RecipeNode.Array)
            Dim startingX As Integer = RecipeNode.NodeStartingX - 1
            Dim startingY As Integer = RecipeNode.NodeStartingY - 1
            Dim mapX As Integer
            Dim mapY As Integer

            For x As Integer = 0 To nodeArray.GetMemoryCountX - 1
                For y As Integer = 0 To nodeArray.GetMemoryCountY - 1
                    If Not IsNothing(StageMap) Then
                        mapX = x + startingX
                        mapY = y + startingY
                        If gCRecipe.sBinData.ContainsKey(Map.Substrates(0).DieArray(mapX, mapY).Bin) Then
                            If Map.Substrates(0).DieArray(mapX, mapY).IsComplete = False And gCRecipe.sBinData(Map.Substrates(0).DieArray(mapX, mapY).Bin).Disable = False Then
                                Select Case StageMap.SBinMapData(x, y).Status
                                    Case eDieStatus.None
                                        StageMap.SBinMapData(x, y).Disable = False

                                    Case eDieStatus.Finish, eDieStatus.DispensingFail
                                        StageMap.SBinMapData(x, y).Disable = True

                                    Case eDieStatus.AlignFail, eDieStatus.LaserFail
                                        StageMap.SBinMapData(x, y).Disable = False

                                    Case Else
                                        StageMap.SBinMapData(x, y).Disable = False
                                End Select
                            Else
                                StageMap.SBinMapData(x, y).Disable = True
                            End If
                        Else
                            StageMap.SBinMapData(x, y).Disable = True
                        End If

                        StageMap.SBinMapData(x, y).BinName = Map.Substrates(0).DieArray(mapX, mapY).Bin
                        StageMap.SBinMapData(x, y).PatternName = Map.Substrates(0).DieArray(mapX, mapY).Pattern
                        StageMap.SRecipePos(x, y).IsByPassCCDScanFixAction = StageMap.SBinMapData(x, y).Disable
                        StageMap.SRecipePos(x, y).IsByPassDispensingAction = StageMap.SBinMapData(x, y).Disable
                        StageMap.SRecipePos(x, y).IsByPassLaserAction = StageMap.SBinMapData(x, y).Disable

                        
                        'RerunMap 點膠 cycle
                        For i = 0 To StageMap.Round.Count - 1
                            Dim sRound As String
                            sRound = (i + 1)
                            If (Map.Substrates(0).DieArray(mapX, mapY).Cycle = "@") Then
                                StageMap.Round(i).DispensingStatus(x, y) = eDispensingStatus.OK
                                StageMap.SBinMapData(x, y).Status = eDieStatus.Finish
                                StageMap.SRecipePos(x, y).IsByPassCCDScanFixAction = True
                                StageMap.SRecipePos(x, y).IsByPassDispensingAction = True
                                StageMap.SRecipePos(x, y).IsByPassLaserAction = True
                            ElseIf (Map.Substrates(0).DieArray(mapX, mapY).Cycle = "#") Then '[Note]#表示上次定位測高已經失敗，故Rerun時不重複定位或測高
                                StageMap.Round(i).DispensingStatus(x, y) = eDispensingStatus.None
                                StageMap.SBinMapData(x, y).Disable = True
                            ElseIf (RoundStringToNumber(Map.Substrates(0).DieArray(mapX, mapY).Cycle) >= sRound) Then
                                StageMap.Round(i).DispensingStatus(x, y) = eDispensingStatus.OK
                            Else
                                StageMap.Round(i).DispensingStatus(x, y) = eDispensingStatus.None
                            End If
                            'If (Map.Substrates(0).DieArray(mapX, mapY).Cycle <> ".") AndAlso Map.Substrates(0).DieArray(mapX, mapY).Cycle <> "#" Then
                            '    If (StageMap.Round(i).DispensingStatus(x, y) = eDispensingStatus.OK) Then
                            '        If (i = StageMap.Round.Count - 1) Then
                            '            Map.Substrates(0).DieArray(mapX, mapY).Cycle = "@"
                            '        Else
                            '            Map.Substrates(0).DieArray(mapX, mapY).Cycle = (i + 1).ToString()
                            '        End If
                            '    End If
                            'End If
                        Next



                    End If
                Next
            Next

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Return False
        End Try
    End Function

    ''' <summary>
    ''' [StageMap轉換到MappingData]
    ''' </summary>
    Function StageMapCoverToMapData(ByRef Map As clsMapData, ByVal StageMap As CPatternMap, ByVal RecipeNode As CRecipeNode) As Boolean
        Try
            Dim nodeArray As CMultiArrayAdapter = New CMultiArrayAdapter(RecipeNode.Array)
            Dim startingX As Integer = RecipeNode.NodeStartingX - 1
            Dim startingY As Integer = RecipeNode.NodeStartingY - 1
            Dim mapX As Integer
            Dim mapY As Integer

            For x As Integer = 0 To nodeArray.GetMemoryCountX - 1
                For y As Integer = 0 To nodeArray.GetMemoryCountY - 1
                    If Not IsNothing(StageMap) Then
                        mapX = x + startingX
                        mapY = y + startingY
                        If gCRecipe.sBinData.ContainsKey(Map.Substrates(0).DieArray(mapX, mapY).Bin) Then
                            If gCRecipe.sBinData(Map.Substrates(0).DieArray(mapX, mapY).Bin).Disable = False Then
                                Select Case StageMap.SBinMapData(x, y).Status
                                    Case eDieStatus.None
                                        Map.Substrates(0).DieArray(mapX, mapY).Bin = StageMap.SBinMapData(x, y).BinName
                                    Case eDieStatus.Finish

                                    Case eDieStatus.AlignFail, eDieStatus.LaserFail, eDieStatus.DispensingFail
                                        Map.Substrates(0).DieArray(mapX, mapY).Bin = "X"

                                    Case Else
                                        Map.Substrates(0).DieArray(mapX, mapY).Bin = StageMap.SBinMapData(x, y).BinName
                                End Select
                            End If
                        End If

                        If (StageMap.SBinMapData(x, y).BinName = ".") Then
                            Map.Substrates(0).DieArray(mapX, mapY).Cycle = "."   '空die = "."
                        ElseIf (Map.Substrates(0).DieArray(mapX, mapY).Bin = "X") Then
                            Map.Substrates(0).DieArray(mapX, mapY).Cycle = "#"  '不作業(定位失敗/不做的Bin) = "#"
                        ElseIf (StageMap.SBinMapData(x, y).Disable = True) Then
                            Map.Substrates(0).DieArray(mapX, mapY).Cycle = "#"  '不作業(定位失敗/不做的Bin) = "#"
                        End If

                        If (StageMap.SRecipePos(x, y).IsByPassCCDScanFixAction AndAlso StageMap.SRecipePos(x, y).IsByPassDispensingAction AndAlso StageMap.SRecipePos(x, y).IsByPassLaserAction) Then
                            Map.Substrates(0).DieArray(mapX, mapY).Cycle = "#"  '不作業(定位失敗/不做的Bin) = "#"
                        End If

                        For i = 0 To StageMap.Round.Count - 1
                            If (Map.Substrates(0).DieArray(mapX, mapY).Cycle <> ".") AndAlso Map.Substrates(0).DieArray(mapX, mapY).Cycle <> "#" Then
                                If (StageMap.Round(i).DispensingStatus(x, y) = eDispensingStatus.OK) Then
                                    If (i = StageMap.Round.Count - 1) Then
                                        Map.Substrates(0).DieArray(mapX, mapY).Cycle = "@" '製程完成 = "@"
                                    Else
                                        Map.Substrates(0).DieArray(mapX, mapY).Cycle = (i + 1).ToString()
                                    End If
                                End If
                            End If
                        Next

                    End If
                Next
            Next

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Return False
        End Try
    End Function

    ''' <summary>
    ''' [雙閥Map轉成單閥]
    ''' </summary>
    ''' <param name="MachineNo">機台別</param>
    ''' <param name="GoodStageNumber">可正常工作閥編號</param>
    ''' <param name="Map">MapData</param>
    ''' <param name="StageMap">可正常工作的StageMap</param>
    ''' <remarks></remarks>
    Sub TwoValveMappingToOneValveMapping(ByVal MachineNo As Integer, ByVal GoodStageNumber As Integer, ByVal GoodNodeId As String, ByVal BadNodeId As String, ByVal Map As clsMapData, ByRef StageMap As CPatternMap, ByVal conveyorNo As Integer)
        Dim ArrayX As Integer = Map.Substrates(0).Columns + Map.Substrates(1).Columns - 1
        Dim ArrayY As Integer = Map.Substrates(0).Rows - 1

        Dim NewMap As New CPatternMap

        ReDim NewMap.ChipState(ArrayX, ArrayY)
        ReDim NewMap.SRecipePos(ArrayX, ArrayY)
        ReDim NewMap.SLaserValue(ArrayX, ArrayY)
        ReDim NewMap.SBinMapData(ArrayX, ArrayY)
        ReDim NewMap.ScanGlueArray(ArrayX, ArrayY)
        ReDim NewMap.SDispenseGlue(ArrayX, ArrayY)
        NewMap.PatternName = StageMap.PatternName
        NewMap.Round = StageMap.Round
        Dim mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(GoodStageNumber)(GoodNodeId).Array)
        Dim OrignalX As Integer = mMultiArrayAdapter.GetMemoryCountX() - 1
        Dim OrignalY As Integer = mMultiArrayAdapter.GetMemoryCountY() - 1
        Dim mMultiArrayAdapter2 As New CMultiArrayAdapter(gCRecipe.Node(GoodStageNumber + 1)(GoodNodeId).Array)
        Dim mMultiArrayAdapter3 As New CMultiArrayAdapter(gCRecipe.Node(GoodStageNumber - 1)(BadNodeId).Array)
        Select Case GoodStageNumber
            '判斷可正常工作的軸數
            Case enmStage.No1 Or enmStage.No3
                For IntX As Integer = 0 To ArrayX
                    For IntY As Integer = 0 To ArrayY
                        '將原側資料回填
                        If IntX <= OrignalX And IntY <= ArrayY Then
                            NewMap.ChipState(IntX, IntY) = StageMap.ChipState(IntX, IntY)
                            NewMap.SRecipePos(IntX, IntY) = StageMap.SRecipePos(IntX, IntY)
                            NewMap.SLaserValue(IntX, IntY) = StageMap.SLaserValue(IntX, IntY)
                            NewMap.SBinMapData(IntX, IntY) = StageMap.SBinMapData(IntX, IntY)
                            NewMap.ScanGlueArray(IntX, IntY) = StageMap.ScanGlueArray(IntX, IntY)
                            NewMap.SDispenseGlue(IntX, IntY) = StageMap.SDispenseGlue(IntX, IntY)
                        Else
                            '將另一側資料回填
                            ReDim NewMap.SRecipePos(IntX, IntY).CCDFinish(2)
                            If gCRecipe.sBinData.ContainsKey(Map.Substrates(1).DieArray(IntX - OrignalX - 1, IntY).Bin) Then
                                If gCRecipe.sBinData(Map.Substrates(1).DieArray(IntX - OrignalX - 1, IntY).Bin).Disable = False And Map.Substrates(1).DieArray(IntX - OrignalX - 1, IntY).IsComplete = False Then
                                    '填入側高點位
                                    If gCRecipe.Node(GoodStageNumber + 1)(BadNodeId).AlignType >= 0 Then
                                        NewMap.SRecipePos(IntX, IntY).ScanPosX = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX + mMultiArrayAdapter2.GetMemoryPosX(IntX, IntY)
                                        NewMap.SRecipePos(IntX, IntY).ScanPosY = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY + mMultiArrayAdapter2.GetMemoryPosY(IntX, IntY)
                                        NewMap.SRecipePos(IntX, IntY).ScanPosZ = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
                                    End If

                                    If gCRecipe.Node(GoodStageNumber + 1)(BadNodeId).AlignType >= 1 Then
                                        NewMap.SRecipePos(IntX, IntY).ScanPosX2 = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosX + mMultiArrayAdapter2.GetMemoryPosX(IntX, IntY)
                                        NewMap.SRecipePos(IntX, IntY).ScanPosY2 = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosY + mMultiArrayAdapter2.GetMemoryPosY(IntX, IntY)
                                        NewMap.SRecipePos(IntX, IntY).ScanPosZ2 = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosZ
                                    End If

                                    If gCRecipe.Node(GoodStageNumber + 1)(BadNodeId).AlignType >= 2 Then
                                        NewMap.SRecipePos(IntX, IntY).ScanPosX3 = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosX + mMultiArrayAdapter2.GetMemoryPosX(IntX, IntY)
                                        NewMap.SRecipePos(IntX, IntY).ScanPosY3 = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosY + mMultiArrayAdapter2.GetMemoryPosY(IntX, IntY)
                                        NewMap.SRecipePos(IntX, IntY).ScanPosZ3 = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosZ
                                    End If
                                    ReDim NewMap.SRecipePos(IntX, IntY).LaserPosX(gCRecipe.Node(GoodStageNumber + 1)(BadNodeId).ConveyorPos(conveyorNo).LaserData.Count - 1)
                                    ReDim NewMap.SRecipePos(IntX, IntY).LaserPosY(gCRecipe.Node(GoodStageNumber + 1)(BadNodeId).ConveyorPos(conveyorNo).LaserData.Count - 1)
                                    ReDim NewMap.SRecipePos(IntX, IntY).LaserPosZ(gCRecipe.Node(GoodStageNumber + 1)(BadNodeId).ConveyorPos(conveyorNo).LaserData.Count - 1)
                                    ReDim NewMap.SLaserValue(IntX, IntY).ZHigh(gCRecipe.Node(GoodStageNumber + 1)(BadNodeId).ConveyorPos(conveyorNo).LaserData.Count - 1)
                                    ReDim NewMap.SLaserValue(IntX, IntY).LaserFinish(gCRecipe.Node(GoodStageNumber + 1)(BadNodeId).ConveyorPos(conveyorNo).LaserData.Count - 1)
                                    '測高點資料塞入
                                    For LaserCount As Integer = 0 To gCRecipe.Node(GoodStageNumber + 1)(BadNodeId).ConveyorPos(conveyorNo).LaserData.Count - 1
                                        NewMap.SRecipePos(IntX, IntY).LaserPosX(LaserCount) = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).LaserData(LaserCount).LaserPositionX + mMultiArrayAdapter2.GetMemoryPosX(IntX, IntY)
                                        NewMap.SRecipePos(IntX, IntY).LaserPosY(LaserCount) = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).LaserData(LaserCount).LaserPositionY + mMultiArrayAdapter2.GetMemoryPosY(IntX, IntY)
                                        NewMap.SRecipePos(IntX, IntY).LaserPosZ(LaserCount) = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).LaserData(LaserCount).LaserPositionZ
                                        '[Note]:暫時要求重測，數值依據重測後的數值做基準
                                        NewMap.SLaserValue(IntX, IntY).RealBasicZHigh = 0
                                    Next

                                    NewMap.SRecipePos(IntX, IntY).IsCCDOffsetReady = False
                                    NewMap.SRecipePos(IntX, IntY).IsCCDOffsetReady2 = False
                                    NewMap.SRecipePos(IntX, IntY).IsCCDOffsetReady3 = False
                                    NewMap.SBinMapData(IntX, IntY).Disable = False
                                Else
                                    NewMap.SBinMapData(IntX, IntY).Disable = True
                                End If
                            Else
                                NewMap.SBinMapData(IntX, IntY).Disable = True
                            End If
                            NewMap.SBinMapData(IntX, IntY).PatternName = gCRecipe.Node(GoodStageNumber + 1)(BadNodeId).PatternName
                            NewMap.SBinMapData(IntX, IntY).BinName = Map.Substrates(1).DieArray(IntX - OrignalX - 1, IntY).Bin
                            NewMap.SRecipePos(IntX, IntY).IsByPassCCDScanFixAction = NewMap.SBinMapData(IntX, IntY).Disable
                            NewMap.SRecipePos(IntX, IntY).IsByPassDispensingAction = NewMap.SBinMapData(IntX, IntY).Disable
                            NewMap.SRecipePos(IntX, IntY).IsByPassLaserAction = NewMap.SBinMapData(IntX, IntY).Disable
                        End If
                    Next
                Next

            Case enmStage.No2 Or enmStage.No4
                For IntX As Integer = 0 To ArrayX
                    For IntY As Integer = 0 To ArrayY
                        '將原側資料回填
                        If IntX > OrignalX And IntY <= ArrayY Then
                            NewMap.ChipState(IntX, IntY) = StageMap.ChipState(IntX - OrignalX - 1, IntY)
                            NewMap.SRecipePos(IntX, IntY) = StageMap.SRecipePos(IntX - OrignalX - 1, IntY)
                            NewMap.SLaserValue(IntX, IntY) = StageMap.SLaserValue(IntX - OrignalX - 1, IntY)
                            NewMap.SBinMapData(IntX, IntY) = StageMap.SBinMapData(IntX - OrignalX - 1, IntY)
                            NewMap.ScanGlueArray(IntX, IntY) = StageMap.ScanGlueArray(IntX - OrignalX - 1, IntY)
                            NewMap.SDispenseGlue(IntX, IntY) = StageMap.SDispenseGlue(IntX - OrignalX - 1, IntY)
                        Else
                            '將另一側資料回填
                            ReDim NewMap.SRecipePos(IntX, IntY).CCDFinish(2)
                            If gCRecipe.sBinData.ContainsKey(Map.Substrates(0).DieArray(IntX, IntY).Bin) Then
                                If Map.Substrates(0).DieArray(IntX, IntY).IsComplete = False And gCRecipe.sBinData(Map.Substrates(0).DieArray(IntX, IntY).Bin).Disable = False Then
                                    '填入側高點位
                                    If gCRecipe.Node(GoodStageNumber - 1)(BadNodeId).AlignType >= 0 Then
                                        NewMap.SRecipePos(IntX, IntY).ScanPosX = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX + mMultiArrayAdapter3.GetMemoryPosX(IntX - OrignalX - 1, IntY)
                                        NewMap.SRecipePos(IntX, IntY).ScanPosY = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY + mMultiArrayAdapter3.GetMemoryPosY(IntX - OrignalX - 1, IntY)
                                        NewMap.SRecipePos(IntX, IntY).ScanPosZ = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
                                    End If

                                    If gCRecipe.Node(GoodStageNumber - 1)(BadNodeId).AlignType >= 1 Then
                                        NewMap.SRecipePos(IntX, IntY).ScanPosX2 = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosX + mMultiArrayAdapter3.GetMemoryPosX(IntX - OrignalX - 1, IntY)
                                        NewMap.SRecipePos(IntX, IntY).ScanPosY2 = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosY + mMultiArrayAdapter3.GetMemoryPosY(IntX - OrignalX - 1, IntY)
                                        NewMap.SRecipePos(IntX, IntY).ScanPosZ2 = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosZ
                                    End If

                                    If gCRecipe.Node(GoodStageNumber - 1)(BadNodeId).AlignType >= 2 Then
                                        NewMap.SRecipePos(IntX, IntY).ScanPosX3 = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosX + mMultiArrayAdapter3.GetMemoryPosX(IntX - OrignalX - 1, IntY)
                                        NewMap.SRecipePos(IntX, IntY).ScanPosY3 = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosY + mMultiArrayAdapter3.GetMemoryPosY(IntX - OrignalX - 1, IntY)
                                        NewMap.SRecipePos(IntX, IntY).ScanPosZ3 = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosZ
                                    End If
                                    ReDim NewMap.SRecipePos(IntX, IntY).LaserPosX(gCRecipe.Node(GoodStageNumber - 1)(BadNodeId).ConveyorPos(conveyorNo).LaserData.Count - 1)
                                    ReDim NewMap.SRecipePos(IntX, IntY).LaserPosY(gCRecipe.Node(GoodStageNumber - 1)(BadNodeId).ConveyorPos(conveyorNo).LaserData.Count - 1)
                                    ReDim NewMap.SRecipePos(IntX, IntY).LaserPosZ(gCRecipe.Node(GoodStageNumber - 1)(BadNodeId).ConveyorPos(conveyorNo).LaserData.Count - 1)
                                    ReDim NewMap.SLaserValue(IntX, IntY).ZHigh(gCRecipe.Node(GoodStageNumber - 1)(BadNodeId).ConveyorPos(conveyorNo).LaserData.Count - 1)
                                    ReDim NewMap.SLaserValue(IntX, IntY).LaserFinish(gCRecipe.Node(GoodStageNumber - 1)(BadNodeId).ConveyorPos(conveyorNo).LaserData.Count - 1)
                                    '測高點資料塞入
                                    For LaserCount As Integer = 0 To gCRecipe.Node(GoodStageNumber - 1)(BadNodeId).ConveyorPos(conveyorNo).LaserData.Count - 1
                                        NewMap.SRecipePos(IntX, IntY).LaserPosX(LaserCount) = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).LaserData(LaserCount).LaserPositionX + mMultiArrayAdapter3.GetMemoryPosX(IntX - OrignalX - 1, IntY)
                                        NewMap.SRecipePos(IntX, IntY).LaserPosY(LaserCount) = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).LaserData(LaserCount).LaserPositionY + mMultiArrayAdapter3.GetMemoryPosY(IntX - OrignalX - 1, IntY)
                                        NewMap.SRecipePos(IntX, IntY).LaserPosZ(LaserCount) = gCRecipe.Node(GoodStageNumber)(GoodNodeId).ConveyorPos(conveyorNo).LaserData(LaserCount).LaserPositionZ
                                        '[Note]:暫時要求重測，數值依據重測後的數值做基準
                                        NewMap.SLaserValue(IntX, IntY).RealBasicZHigh = 0
                                    Next

                                    NewMap.SRecipePos(IntX, IntY).IsCCDOffsetReady = False
                                    NewMap.SRecipePos(IntX, IntY).IsCCDOffsetReady2 = False
                                    NewMap.SRecipePos(IntX, IntY).IsCCDOffsetReady3 = False
                                    NewMap.SBinMapData(IntX, IntY).Disable = False
                                Else
                                    NewMap.SBinMapData(IntX, IntY).Disable = True
                                End If
                            Else
                                NewMap.SBinMapData(IntX, IntY).Disable = True
                            End If
                            NewMap.SBinMapData(IntX, IntY).PatternName = gCRecipe.Node(GoodStageNumber - 1)(BadNodeId).PatternName
                            NewMap.SBinMapData(IntX, IntY).BinName = Map.Substrates(0).DieArray(IntX, IntY).Bin
                            NewMap.SRecipePos(IntX, IntY).IsByPassCCDScanFixAction = NewMap.SBinMapData(IntX, IntY).Disable
                            NewMap.SRecipePos(IntX, IntY).IsByPassDispensingAction = NewMap.SBinMapData(IntX, IntY).Disable
                            NewMap.SRecipePos(IntX, IntY).IsByPassLaserAction = NewMap.SBinMapData(IntX, IntY).Disable
                        End If
                    Next
                Next
        End Select
        'TODO: 這段會有問題需要再修
        gCRecipe.Node(GoodStageNumber)(GoodNodeId).Array(0).Array.CountX = Map.Substrates(0).Columns + Map.Substrates(1).Columns
        gCRecipe.Node(GoodStageNumber)(GoodNodeId).Array(0).Array.CountY = Map.Substrates(0).Rows
        ReDim StageMap.ChipState(ArrayX, ArrayY)
        ReDim StageMap.SRecipePos(ArrayX, ArrayY)
        ReDim StageMap.SLaserValue(ArrayX, ArrayY)
        ReDim StageMap.SBinMapData(ArrayX, ArrayY)
        ReDim StageMap.ScanGlueArray(ArrayX, ArrayY)
        ReDim StageMap.SDispenseGlue(ArrayX, ArrayY)

        StageMap = NewMap
    End Sub

    'TODO: Soni 移入gSSysyemParamter
    Function GetUserLevel(ByVal refAuth As enmUserLevel, ByVal nowAuth As enmUserLevel) As Boolean
        If refAuth < nowAuth Then
            Return False
        End If
        Return True
    End Function


    'Function Machine_PauseDoorOpen(ByVal sys As sSysParam) As enmRunStatus
    '    With sys

    '        Select Case .SysNum
    '            Case sSysParam.SysLoopStart
    '                '紀錄停止時的位置
    '                gStageMap(enmStage.No1 + .MachineNo * 2).ReBackPos.PosX = gCMotion.GetPositionValue(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisX)
    '                gStageMap(enmStage.No1 + .MachineNo * 2).ReBackPos.PosY = gCMotion.GetPositionValue(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisY)
    '                gStageMap(enmStage.No1 + .MachineNo * 2).ReBackPos.PosZ = gCMotion.GetPositionValue(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisZ)
    '                gStageMap(enmStage.No1 + .MachineNo * 2).ReBackPos.PosA = gCMotion.GetPositionValue(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisA)
    '                gStageMap(enmStage.No1 + .MachineNo * 2).ReBackPos.PosB = gCMotion.GetPositionValue(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisB)
    '                gStageMap(enmStage.No1 + .MachineNo * 2).ReBackPos.PosC = gCMotion.GetPositionValue(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisC)
    '                gStageMap(enmStage.No2 + .MachineNo * 2).ReBackPos.PosX = gCMotion.GetPositionValue(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisX)
    '                gStageMap(enmStage.No2 + .MachineNo * 2).ReBackPos.PosY = gCMotion.GetPositionValue(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisY)
    '                gStageMap(enmStage.No2 + .MachineNo * 2).ReBackPos.PosZ = gCMotion.GetPositionValue(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisZ)
    '                gStageMap(enmStage.No2 + .MachineNo * 2).ReBackPos.PosA = gCMotion.GetPositionValue(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisA)
    '                gStageMap(enmStage.No2 + .MachineNo * 2).ReBackPos.PosB = gCMotion.GetPositionValue(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisB)
    '                gStageMap(enmStage.No2 + .MachineNo * 2).ReBackPos.PosC = gCMotion.GetPositionValue(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisC)
    '                .SysNum = 1100
    '            Case 1100
    '                '判斷是A機或B機
    '                Select Case .MachineNo
    '                    Case 0
    '                        '判斷是否有開門解鎖
    '                        If gDOCollection.GetSetState(enmDO.DoorLock) = False Then
    '                            .SysNum = 2000
    '                        End If
    '                    Case 1
    '                        '判斷是否有開門解鎖
    '                        If gDOCollection.GetSetState(enmDO.DoorLock2) = False Then
    '                            .SysNum = 2000
    '                        End If
    '                End Select
    '            Case 2000
    '                '保護一旦解鎖，要回復到檢查狀態，並禁止移動
    '                If gDOCollection.GetSetState(enmDO.DoorLock) Or gDOCollection.GetSetState(enmDO.DoorLock2) Then
    '                    .SysNum = 1100
    '                    Return enmRunStatus.Running
    '                End If

    '                If gCMotion.MotionDone(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisX) And gCMotion.MotionDone(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisY) And
    '                    gCMotion.MotionDone(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisZ) And gCMotion.MotionDone(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisX) And
    '                    gCMotion.MotionDone(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisY) And gCMotion.MotionDone(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisZ) And
    '                    gSYS(eSys.DispStage1 + .MachineNo * 2).RunStatus = enmRunStatus.Finish And gSYS(eSys.DispStage2 + .MachineNo * 2).RunStatus = enmRunStatus.Finish Then
    '                    Select Case .PuaseAction
    '                        Case sPauseAction.GoToBackPos
    '                            gCMotion.AbsMove(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisX, gStageMap(enmStage.No1 + .MachineNo * 2).ReBackPos.PosX)
    '                            gCMotion.AbsMove(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisY, gStageMap(enmStage.No1 + .MachineNo * 2).ReBackPos.PosY)
    '                            gCMotion.AbsMove(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisZ, gStageMap(enmStage.No1 + .MachineNo * 2).ReBackPos.PosZ)
    '                            gCMotion.AbsMove(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisX, gStageMap(enmStage.No2 + .MachineNo * 2).ReBackPos.PosX)
    '                            gCMotion.AbsMove(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisY, gStageMap(enmStage.No2 + .MachineNo * 2).ReBackPos.PosY)
    '                            gCMotion.AbsMove(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisZ, gStageMap(enmStage.No2 + .MachineNo * 2).ReBackPos.PosZ)
    '                        Case sPauseAction.GoToSetPos
    '                            '走道設定位置時，Z軸固定在原點
    '                            gCMotion.AbsMove(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisX, gSSystemParameter.Pos.PauseStopPos(enmStage.No1 + .MachineNo * 2).PosX)
    '                            gCMotion.AbsMove(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisY, gSSystemParameter.Pos.PauseStopPos(enmStage.No1 + .MachineNo * 2).PosY)
    '                            gCMotion.AbsMove(gSYS(eSys.DispStage1 + .MachineNo * 2).AxisZ, 0)
    '                            gCMotion.AbsMove(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisX, gSSystemParameter.Pos.PauseStopPos(enmStage.No2 + .MachineNo * 2).PosX)
    '                            gCMotion.AbsMove(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisY, gSSystemParameter.Pos.PauseStopPos(enmStage.No2 + .MachineNo * 2).PosY)
    '                            gCMotion.AbsMove(gSYS(eSys.DispStage2 + .MachineNo * 2).AxisZ, 0)
    '                        Case sPauseAction.Purge
    '                            gSYS(eSys.DispStage1 + .MachineNo * 2).Command = eSysCommand.Purge
    '                            gSYS(eSys.DispStage2 + .MachineNo * 2).Command = eSysCommand.Purge
    '                        Case sPauseAction.WeightLeft
    '                            gSYS(eSys.DispStage1 + .MachineNo * 2).Command = eSysCommand.WeightUnit
    '                        Case sPauseAction.WeightRight
    '                            gSYS(eSys.DispStage2 + .MachineNo * 2).Command = eSysCommand.WeightUnit
    '                    End Select
    '                End If
    '        End Select
    '        Return enmRunStatus.Running
    '    End With
    'End Function

#Region "系統命令配接"

    ''' <summary>[主控層(LevelNo1端:控制 MachineA、MachineB、 Conveyor)]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub System_OverAll(ByRef sys As sSysParam)
        '[Note]:判斷系統狀態是否為可接收命令
        If sys.RunStatus = enmRunStatus.None Or sys.RunStatus = enmRunStatus.Finish Then
            '[Note]:有下一筆命令
            If sys.Command <> eSysCommand.None Then
                sys.ExecuteCommand = sys.Command            '接收命令至執行命令
                sys.Command = eSysCommand.None              '接收暫存器清空 避免Finish又進來
                sys.SysNum = sSysParam.SysLoopStart         '系統起始索引
                sys.RunStatus = enmRunStatus.Running        '系統狀態改為運行中
            End If
        End If

        '[Note]:外部暫停，且系統可暫停時，不執行動作流程
        If sys.ExternalPause = True And sys.IsCanPause = True Then
            Exit Sub
        End If

        If sys.RunStatus = enmRunStatus.Running Then
            Select Case sys.ExecuteCommand
                Case eSysCommand.Home '整機復歸
                    Overall_HomeAction(sys)
                Case eSysCommand.AutoRun '自動生產
                    Select Case gSSystemParameter.MachineType
                        Case enmMachineType.DCS_350A

                            Overall_MulitConveyorAutoRunAction(sys)

                        Case Else
                            Overall_AutoRunAction(sys)
                    End Select

                Case eSysCommand.AbnormalUnload '異常,強制退料
                    Overall_AbnormalUnloadAction(sys)
            End Select
        End If
    End Sub

    ''' <summary>
    ''' A機外部操作
    ''' </summary>
    ''' <remarks></remarks>
    Sub MachineAExternalOp()
        If enmDI.StartButton >= 0 AndAlso enmDO.StartButtonLight >= 0 AndAlso enmDO.PauseButtonLight >= 0 Then
            If gDICollection.GetState(enmDI.StartButton) = True Then
                gDOCollection.SetState(enmDO.StartButtonLight, True)
                gDOCollection.SetState(enmDO.PauseButtonLight, False)
                MachineAStart() '外部按鍵 A機開始
            End If
        End If

        If enmDI.PauseButton >= 0 AndAlso enmDO.PauseButtonLight >= 0 AndAlso enmDO.StartButtonLight >= 0 Then
            If gDICollection.GetState(enmDI.PauseButton) = True Then
                MachineAPause()
                gDOCollection.SetState(enmDO.StartButtonLight, False)
                gDOCollection.SetState(enmDO.PauseButtonLight, True)
            End If
        End If


    End Sub
    ''' <summary>
    ''' B機外部操作
    ''' </summary>
    ''' <remarks></remarks>
    Sub MachineBExternalOp()
        If enmDI.StartButton2 >= 0 AndAlso enmDO.StartButtonLight2 >= 0 AndAlso enmDO.PauseButtonLight2 >= 0 Then
            If gDICollection.GetState(enmDI.StartButton2) = True Then
                gDOCollection.SetState(enmDO.StartButtonLight2, True)
                gDOCollection.SetState(enmDO.PauseButtonLight2, False)
                MachineBStart() '外部按鍵 B機開始
            End If
        End If
        If enmDI.PauseButton2 >= 0 AndAlso enmDO.PauseButtonLight2 >= 0 AndAlso enmDO.StartButtonLight2 >= 0 Then
            If gDICollection.GetState(enmDI.PauseButton2) = True Then
                MachineBPause()
                gDOCollection.SetState(enmDO.StartButtonLight2, False)
                gDOCollection.SetState(enmDO.PauseButtonLight2, True)
            End If
        End If
    End Sub
    ''' <summary>
    ''' A機生產
    ''' </summary>
    ''' <remarks></remarks>
    Sub MachineAStart()
        gSyslog.Save("[CSystemThread]" & vbTab & "[MachineAStart]" & vbTab & "Click")
        '20171114
        Dim mIsExternalPause As Boolean

        '[Note]:若是外部請求暫停的指令，那開始的指令是作接續的動作，而不是在下新的命令
        mIsExternalPause = gSYS(eSys.OverAll).ExternalPause


        '[Note]:若再復歸的時候，則可以接續復歸的動作
        If gSYS(eSys.MachineA).ExecuteCommand <> eSysCommand.Home Then
            If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
                gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
                gDOCollection.SetState(enmDO.StartButtonLight, True) ' False '按下開始 但不能開始 
                gDOCollection.SetState(enmDO.PauseButtonLight, True) 'True
                Exit Sub
            End If
        ElseIf gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '復歸中
            gSYS(eSys.DispStage1).ExternalPause = False '暫停清除
            gSYS(eSys.DispStage2).ExternalPause = False '暫停清除
            gDOCollection.SetState(enmDO.StartButtonLight, True) '按下開始 但不能開始 
            gDOCollection.SetState(enmDO.PauseButtonLight, True) 'True
            Exit Sub 'Soni + 2016.08.29 復歸中 執行無效
            '^^^^^^^
        End If

        'If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '    MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
        '    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
        '    gDOCollection.GetSetState(enmDO.StartButtonLight) = False
        '    gDOCollection.GetSetState(enmDO.PauseButtonLight) = True
        '    Exit Sub
        '    '^^^^^^^
        'End If
        '[說明]:判斷有無開啟Recipe
        If gCRecipe.strName = "" Then
            MessageBox.Show(gMsgHandler.GetMessage(Warn_3000011)) '找不到 Recipe 檔案!!
            gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
            gDOCollection.SetState(enmDO.StartButtonLight, True) 'False
            gDOCollection.SetState(enmDO.PauseButtonLight, False) 'True
            Exit Sub
            '^^^^^^^
        End If

        If gAOICollection.LoadSceneStatus = CAOICollection.enmStatus.Loading Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000030))
            MsgBox(gMsgHandler.GetMessage(Warn_3000030), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Scene is Loading, Please Wait.")
            Exit Sub
        End If
        If gAOICollection.LoadSceneStatus = CAOICollection.enmStatus.NG Then
            '場景載入失敗
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000038))
            MsgBox(gMsgHandler.GetMessage(Warn_3000038), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gSSystemParameter.IsCompareWithMapData <> 0 Then
            '[說明]:判斷使用Map功能時Recipe有無選擇Node
            Dim isNodeReady As Boolean = True
            If (gSSystemParameter.StageMax = eSys.DispStage1) Then
                If (gCRecipe.NodeToMap(enmStage.No1) IsNot Nothing) Then
                    If (gCRecipe.NodeToMap(enmStage.No1).Count = 0) Then
                        isNodeReady = False
                    End If
                End If
            ElseIf (gSSystemParameter.StageMax = eSys.DispStage4) Then
                If (gCRecipe.NodeToMap(enmStage.No1) IsNot Nothing) AndAlso (gCRecipe.NodeToMap(enmStage.No2) IsNot Nothing) AndAlso (gCRecipe.NodeToMap(enmStage.No3) IsNot Nothing) AndAlso (gCRecipe.NodeToMap(enmStage.No4) IsNot Nothing) Then
                    If (gCRecipe.NodeToMap(enmStage.No1).Count = 0) AndAlso (gCRecipe.NodeToMap(enmStage.No2).Count = 0) AndAlso (gCRecipe.NodeToMap(enmStage.No3).Count = 0) AndAlso (gCRecipe.NodeToMap(enmStage.No4).Count = 0) Then
                        isNodeReady = False
                    End If
                End If
            End If

            If isNodeReady = False Then
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
                MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Exit Sub
            End If
        End If

        '[說明]:清除Alarm
        If Not gfrmUIViewer Is Nothing Then
            gEqpMsg.ClearAlarmCmpTable(gfrmUIViewer.cboAlarmMessage, True)

            InvokeClearMessage(gfrmUIViewer.cboAlarmMessage)
        End If


        '[說明]:是否為InterLock之Alarm
        If gInterlockCollection.IsAlarm = True Then
            gDOCollection.SetState(enmDO.StartButtonLight, True) 'False
            gDOCollection.SetState(enmDO.PauseButtonLight, False) 'True
            Exit Sub
            '^^^^^^^
        End If


        If gEqpInfo.Status = enmEqpStatus.RunPause Then '暫停後開始, 確認項目
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    If gEqpInfo.IsW800AQPauseCanContinue = False Then
                        'Sue0710
                        '資料不相符.無法繼續執行
                        gEqpMsg.AddHistoryAlarm("Warn_3000039", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Warn_3000039), eMessageLevel.Warning)
                        gDOCollection.SetState(enmDO.StartButtonLight, True) 'False
                        gDOCollection.SetState(enmDO.PauseButtonLight, False) 'True
                        Exit Sub
                        '^^^^^^^
                    End If
                Case enmMachineType.DCS_F230A

            End Select

        End If

        'If continueParam.IsProductMapNotFinish Then '未完成生產需選擇接續模式
        '    Dim gfrmRerunCheck As New frmRerunCheck
        '    gfrmRerunCheck.ShowDialog()
        '    continueParam.IsProductMapNotFinish = False '設定完成 
        '    Select Case continueParam.Mode
        '        Case enmContinueMode.AutoClean '強迫退料
        '            Exit Sub
        '            '^^^^^^^
        '        Case enmContinueMode.ContinueRun '接續生產
        '            'continueParam.UseValve1 使用閥1
        '            'continueParam.UseValve2 使用閥2
        '            'continueParam.UseValve3 使用閥3
        '            'continueParam.UseValve4 使用閥4
        '        Case enmContinueMode.ManualClean '手動取料
        '            gDOCollection.SetState(enmDO.StartButtonLight, True) 'False
        '            gDOCollection.SetState(enmDO.PauseButtonLight, False) 'True
        '            Exit Sub
        '            '^^^^^^^
        '    End Select
        'End If

        gSYS(eSys.DispStage1).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage2).ExternalPause = False '暫停清除
        gDOCollection.SetState(enmDO.DoorLock, True) 'A機門鎖
        gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖
        gDOCollection.SetState(enmDO.StartButtonLight, False) '按下開始可做動, 開始燈暗 表示不能按開始
        gDOCollection.SetState(enmDO.PauseButtonLight, True) '按下開始可做動, 暫停燈亮 表示可按暫停
        gSSystemParameter.AutoRunMachineStartTime(0) = DateTime.Now '生產開始時間

        'If gSYS(eSys.MachineA).RunStatus <> enmRunStatus.Running Then '不是生產中則下命令開始
        '    gSYS(eSys.MachineA).Command = eSysCommand.AutoRun
        'End If

        gSYS(eSys.OverAll).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage1).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage2).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage3).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage4).ExternalPause = False '暫停清除
        gSYS(eSys.Conveyor1).ExternalPause = False '暫停清除

        gDOCollection.SetState(enmDO.StartButtonLight, True)
        gDOCollection.SetState(enmDO.PauseButtonLight, False)
        gDOCollection.SetState(enmDO.StartButtonLight2, True)
        gDOCollection.SetState(enmDO.PauseButtonLight2, False)
        gDOCollection.SetState(enmDO.DoorLock, True) 'A機門鎖
        gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖

        '[Note]:若是外部請求暫停的指令，那開始的指令是作接續的動作，而不是在下新的命令
        If mIsExternalPause = False Then

            gSYS(eSys.OverAll).Command = eSysCommand.AutoRun
            gSSystemParameter.AutoRunStartTime = DateTime.Now '生產開始時間
        End If

        '[說明]:記錄開始結束時間   20161205
        gSyslog.Save("AutoRunStartTime is " & Format(Now.Year, "0000") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Day, "00") & " " & Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00"))

    End Sub


    ''' <summary>
    ''' B機生產
    ''' </summary>
    ''' <remarks></remarks>
    Sub MachineBStart()
        gSyslog.Save("[CSystemThread]" & vbTab & "[MachineBStart]" & vbTab & "Click")

        '[Note]:若再復歸的時候，則可以接續復歸的動作
        If gSYS(eSys.MachineB).ExecuteCommand <> eSysCommand.Home Then
            If gSYS(eSys.MachineB).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
                gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
                gDOCollection.SetState(enmDO.StartButtonLight2, True) '按下開始 但不能開始 
                gDOCollection.SetState(enmDO.PauseButtonLight2, True)
                Exit Sub
            End If
        ElseIf gSYS(eSys.MachineB).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '復歸中
            gSYS(eSys.DispStage3).ExternalPause = False '暫停清除
            gSYS(eSys.DispStage4).ExternalPause = False '暫停清除
            gDOCollection.SetState(enmDO.StartButtonLight2, True) '按下開始 但不能開始 
            gDOCollection.SetState(enmDO.PauseButtonLight2, True)
            Exit Sub 'Soni + 2016.08.29 復歸中 執行無效
            '^^^^^^^
        End If

        'If gSYS(eSys.MachineB).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '    MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
        '    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
        '    gDOCollection.GetSetState(enmDO.StartButtonLight2) = False
        '    gDOCollection.GetSetState(enmDO.PauseButtonLight2) = True
        '    Exit Sub
        '    '^^^^^^^
        'End If
        '[說明]:判斷有無開啟Recipe
        If gCRecipe.strName = "" Then
            MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '找不到 Recipe 檔案!!
            gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
            gDOCollection.SetState(enmDO.StartButtonLight2, True) 'False
            gDOCollection.SetState(enmDO.PauseButtonLight2, False) 'True
            Exit Sub
            '^^^^^^^
        End If

        '[說明]:是否為InterLock之Alarm
        If gInterlockCollection.IsAlarm = True Then
            gDOCollection.SetState(enmDO.StartButtonLight2, True) 'False
            gDOCollection.SetState(enmDO.PauseButtonLight2, False) 'True
            Exit Sub
            '^^^^^^^
        End If


        If gEqpInfo.Status = enmEqpStatus.RunPause Then '暫停後開始, 確認項目
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    If gEqpInfo.IsW800AQPauseCanContinue = False Then
                        'Sue0710
                        '資料不相符.無法繼續執行
                        gEqpMsg.AddHistoryAlarm("Warn_3000039", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Warn_3000039), eMessageLevel.Warning)
                        gDOCollection.SetState(enmDO.StartButtonLight2, True) 'False
                        gDOCollection.SetState(enmDO.PauseButtonLight2, False) 'True
                        Exit Sub
                        '^^^^^^^
                    End If
                Case enmMachineType.DCS_F230A

            End Select

        End If

        'If continueParam.IsProductMapNotFinish Then '未完成生產需選擇接續模式
        '    Dim gfrmRerunCheck As New frmRerunCheck
        '    gfrmRerunCheck.ShowDialog()
        '    continueParam.IsProductMapNotFinish = False '設定完成 
        '    Select Case continueParam.Mode
        '        Case enmContinueMode.AutoClean '強迫退料
        '            Exit Sub
        '            '^^^^^^^
        '        Case enmContinueMode.ContinueRun '接續生產
        '            'continueParam.UseValve1 使用閥1
        '            'continueParam.UseValve2 使用閥2
        '            'continueParam.UseValve3 使用閥3
        '            'continueParam.UseValve4 使用閥4
        '        Case enmContinueMode.ManualClean '手動取料
        '            gDOCollection.SetState(enmDO.StartButtonLight2, False) 'False
        '            gDOCollection.SetState(enmDO.PauseButtonLight2, False) 'True
        '            Exit Sub
        '            '^^^^^^^
        '    End Select
        'End If

        gSYS(eSys.DispStage3).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage4).ExternalPause = False '暫停清除
        gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖
        gDOCollection.SetState(enmDO.StartButtonLight2, False) '按下開始 開始燈暗 不能按開始
        gDOCollection.SetState(enmDO.PauseButtonLight2, True) 'ˋ按下開始 暫停燈亮 可以按暫停
        gSSystemParameter.AutoRunMachineStartTime(1) = DateTime.Now '生產開始時間
        If gSYS(eSys.MachineB).RunStatus <> enmRunStatus.Running Then '不是生產中則下命令開始
            gSYS(eSys.MachineB).Command = eSysCommand.AutoRun
        End If

    End Sub

    ''' <summary>A機暫停</summary>
    ''' <remarks></remarks>
    Sub MachineAPause()
        gSyslog.Save("[CSystemThread]" & vbTab & "[MachineAPause]" & vbTab & "Click")
        '[說明]:回Home完成才能執行 
        '整機生產, 整機暫停
        If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
            '20171114
            gSYS(eSys.OverAll).ExternalPause = True '整機生產, 整機暫停
            gSYS(eSys.DispStage1).ExternalPause = True
            gSYS(eSys.DispStage2).ExternalPause = True
        End If
    End Sub

    ''' <summary>B機暫停</summary>
    ''' <remarks></remarks>
    Sub MachineBPause()
        gSyslog.Save("[CSystemThread]" & vbTab & "[MachineBPause]" & vbTab & "Click")
        '[說明]:回Home完成才能執行 
        '整機生產, 整機暫停
        If gSYS(eSys.MachineB).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
            gSYS(eSys.DispStage3).ExternalPause = True
            gSYS(eSys.DispStage4).ExternalPause = True
        End If
    End Sub

    ''' <summary>機台控制層(LevelNo2端:控制 DispStage)</summary>
    ''' <param name="sys"></param>
    ''' <param name="MachineType"></param>
    ''' <remarks></remarks>
    Sub System_Machine(ByRef sys As sSysParam, ByVal MachineType As Integer)
        '=== 外部IO操作 ===
        Select Case MachineType
            Case eSys.MachineA
                MachineAExternalOp()
            Case Else
                MachineBExternalOp()
        End Select
        '=== 外部IO操作 ===

        '[Note]:判斷系統狀態是否為可接收命令
        If sys.RunStatus = enmRunStatus.None Or sys.RunStatus = enmRunStatus.Finish Then
            '[Note]:有下一筆命令
            If sys.Command <> eSysCommand.None Then
                sys.ExecuteCommand = sys.Command            '接收命令至執行命令
                sys.Command = eSysCommand.None              '接收暫存器清空 避免Finish又進來
                sys.SysNum = sSysParam.SysLoopStart         '系統起始索引
                sys.RunStatus = enmRunStatus.Running        '系統狀態改為運行中
            End If
        End If

        '[Note]:外部暫停，且系統可暫停時，不執行動作流程
        If sys.ExternalPause = True And sys.IsCanPause = True Then
            Exit Sub
        End If

        Select Case sys.RunStatus
            Case enmRunStatus.Running
                Select Case sys.ExecuteCommand
                    Case eSysCommand.Home
                        Machine_Home(sys)

                    Case eSysCommand.AutoRun

                        Machine_AutoRun(sys)

                    Case eSysCommand.PrevDispense
                        Machine_PrevDispense(sys)

                    Case eSysCommand.ContinueLastRun
                        Machine_ContinueLastRun(sys)

                        'Case eSysCommand.ManualProduceMode 'TODO: 待重整
                        '    sys.RunStatus = Machine_ManualProduce(sys)

                        'Case eSysCommand.SingleAction
                        '    Machine_SingleAction(sys)

                    Case eSysCommand.AbnormalUnload '異常退料
                        Machine_AbnormalUnloadAction(sys)
                End Select

                'Case enmRunStatus.Pause
                '    sys.PauseSysNum = sys.SysNum

                'Case enmRunStatus.PauseDoorOpen
                '    sys.RunStatus = Machine_PauseDoorOpen(sys)

                'Case enmRunStatus.PauseResume
                '    Select Case sys.MachineNo
                '        Case 0
                '            If gDICollection.GetState(enmDI.DoorClose) = False Or gDOCollection.GetSetState(enmDO.DoorLock) = False Then
                '                '門沒關或是門鎖沒上自動切回暫停開門動作
                '                sys.RunStatus = enmRunStatus.PauseDoorOpen
                '            End If
                '        Case 1
                '            If gDICollection.GetState(enmDI.DoorClose2) = False Or gDOCollection.GetSetState(enmDO.DoorLock2) = False Then
                '                '門沒關或是門鎖沒上自動切回暫停開門動作
                '                sys.RunStatus = enmRunStatus.PauseDoorOpen
                '            End If
                '    End Select

                '    If gCMotion.GetPositionValue(gSYS(eSys.DispStage1 + sys.MachineNo * 2).AxisX) = gSSystemParameter.Pos.PauseStopPos(enmStage.No1 + sys.MachineNo * 2).PosX And
                '       gCMotion.GetPositionValue(gSYS(eSys.DispStage1 + sys.MachineNo * 2).AxisY) = gSSystemParameter.Pos.PauseStopPos(enmStage.No1 + sys.MachineNo * 2).PosY And
                '       gCMotion.GetPositionValue(gSYS(eSys.DispStage1 + sys.MachineNo * 2).AxisZ) = gSSystemParameter.Pos.PauseStopPos(enmStage.No1 + sys.MachineNo * 2).PosZ And
                '       gCMotion.GetPositionValue(gSYS(eSys.DispStage2 + sys.MachineNo * 2).AxisX) = gSSystemParameter.Pos.PauseStopPos(enmStage.No2 + sys.MachineNo * 2).PosX And
                '       gCMotion.GetPositionValue(gSYS(eSys.DispStage2 + sys.MachineNo * 2).AxisY) = gSSystemParameter.Pos.PauseStopPos(enmStage.No2 + sys.MachineNo * 2).PosY And
                '       gCMotion.GetPositionValue(gSYS(eSys.DispStage2 + sys.MachineNo * 2).AxisZ) = gSSystemParameter.Pos.PauseStopPos(enmStage.No2 + sys.MachineNo * 2).PosZ Then
                '        'TODO 跟Mobary獲得可暫停的SYSNUM
                '        sys.SysNum = sys.PauseSysNum
                '        sys.RunStatus = sys.PauseStatus
                '    End If

        End Select
    End Sub

    ''' <summary>Stage馬達狀態確認</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub CheckStageMotorAlarm(ByRef sys As sSysParam)
        If sys.AxisX <> -1 Then
            If gCMotion.GetAxisState(sys.AxisX) = Premtek.Base.CommandStatus.Sucessed Then
                If gCMotion.AxisParameter(sys.AxisX).MotionStatus = AxisState.STA_AX_ERROR_STOP Then
                    If gCMotion.AxisResetError(sys.AxisX) = Premtek.Base.CommandStatus.Sucessed Then
                        gCMotion.AxisParameter(sys.AxisX).MotionStatus = Nothing
                    End If
                End If
            End If
            If gCMotion.CheckMotorStatus(sys.AxisX) = Premtek.Base.CommandStatus.Sucessed Then
                If gCMotion.AxisParameter(sys.AxisX).MotionIOStatus.blnPEL = True Then
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
                        '[Note]:碰極限後就強制停止動作
                        Call gCMotion.GpPauseMovePath(gCMotion.SyncParameter(sys.StageNo))
                        Call gCMotion.GpResetPath(gCMotion.SyncParameter(sys.StageNo))
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---
                        Select Case sys.StageNo
                            Case 0 'enmAxis.XAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1030007, eMessageLevel.Error) '[X軸馬達Alarm]
                            Case 1 'enmAxis.UAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1042007, eMessageLevel.Error) '[X軸馬達Alarm]
                            Case 2 'enmAxis.RAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1060007, eMessageLevel.Error) '[X軸馬達Alarm]
                            Case 3 'enmAxis.OAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1067007, eMessageLevel.Error) '[X軸馬達Alarm]
                        End Select
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---

                        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If
                If gCMotion.AxisParameter(sys.AxisX).MotionIOStatus.blnNEL = True Then
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
                        '[Note]:碰極限後就強制停止動作
                        Call gCMotion.GpPauseMovePath(gCMotion.SyncParameter(sys.StageNo))
                        Call gCMotion.GpResetPath(gCMotion.SyncParameter(sys.StageNo))
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---
                        Select Case sys.StageNo
                            Case 0 'enmAxis.XAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1030008, eMessageLevel.Error) '[X軸馬達Alarm]
                            Case 1 'enmAxis.UAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1042008, eMessageLevel.Error) '[X軸馬達Alarm]
                            Case 2 'enmAxis.RAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1060008, eMessageLevel.Error) '[X軸馬達Alarm]
                            Case 3 'enmAxis.OAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1067008, eMessageLevel.Error) '[X軸馬達Alarm]
                        End Select
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---
                        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If
                If gCMotion.AxisParameter(sys.AxisX).MotionIOStatus.blnALM = True Then
                    Select Case sys.StageNo
                        Case 0 'enmAxis.XAxis
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1030002, eMessageLevel.Error) '[X軸馬達Alarm]
                        Case 1 'enmAxis.UAxis
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1042002, eMessageLevel.Error) '[X軸馬達Alarm]
                        Case 2 'enmAxis.RAxis
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1060002, eMessageLevel.Error) '[X軸馬達Alarm]
                        Case 3 'enmAxis.OAxis
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1067002, eMessageLevel.Error) '[X軸馬達Alarm]
                    End Select

                    gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                    sys.RunStatus = enmRunStatus.Alarm
                End If
            End If
        End If
        If sys.AxisY <> -1 Then
            If gCMotion.GetAxisState(sys.AxisY) = Premtek.Base.CommandStatus.Sucessed Then
                If gCMotion.AxisParameter(sys.AxisY).MotionStatus = AxisState.STA_AX_ERROR_STOP Then
                    If gCMotion.AxisResetError(sys.AxisY) = Premtek.Base.CommandStatus.Sucessed Then
                        gCMotion.AxisParameter(sys.AxisY).MotionStatus = Nothing
                    End If
                End If
            End If
            If gCMotion.CheckMotorStatus(sys.AxisY) = Premtek.Base.CommandStatus.Sucessed Then
                If gCMotion.AxisParameter(sys.AxisY).MotionIOStatus.blnPEL = True Then
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
                        '[Note]:碰極限後就強制停止動作
                        Call gCMotion.GpPauseMovePath(gCMotion.SyncParameter(sys.StageNo))
                        Call gCMotion.GpResetPath(gCMotion.SyncParameter(sys.StageNo))
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---
                        Select Case sys.StageNo
                            Case 0 'enmAxis.Y1Axis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1031007, eMessageLevel.Error) '[Y軸馬達Alarm]
                            Case 1 'enmAxis.VAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1043007, eMessageLevel.Error) '[Y軸馬達Alarm]
                            Case 2 'enmAxis.SAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1061007, eMessageLevel.Error) '[Y軸馬達Alarm]
                            Case 3 'enmAxis.PAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1068007, eMessageLevel.Error) '[Y軸馬達Alarm]
                        End Select
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---
                        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If
                If gCMotion.AxisParameter(sys.AxisY).MotionIOStatus.blnNEL = True Then
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
                        '[Note]:碰極限後就強制停止動作
                        Call gCMotion.GpPauseMovePath(gCMotion.SyncParameter(sys.StageNo))
                        Call gCMotion.GpResetPath(gCMotion.SyncParameter(sys.StageNo))
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---
                        Select Case sys.StageNo
                            Case 0 'enmAxis.Y1Axis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1031008, eMessageLevel.Error) '[Y軸馬達Alarm]
                            Case 1 'enmAxis.VAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1043008, eMessageLevel.Error) '[Y軸馬達Alarm]
                            Case 2 'enmAxis.SAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1061008, eMessageLevel.Error) '[Y軸馬達Alarm]
                            Case 3 'enmAxis.PAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1068008, eMessageLevel.Error) '[Y軸馬達Alarm]
                        End Select
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---
                        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If
                If gCMotion.AxisParameter(sys.AxisY).MotionIOStatus.blnALM = True Then
                    '--- Soni / 2017.01.20 訊息錯誤修正 ---
                    Select Case sys.StageNo
                        Case 0 'enmAxis.Y1Axis
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1031002, eMessageLevel.Error) '[Y軸馬達Alarm]
                        Case 1 'enmAxis.VAxis
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1043002, eMessageLevel.Error) '[Y軸馬達Alarm]
                        Case 2 'enmAxis.SAxis
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1061002, eMessageLevel.Error) '[Y軸馬達Alarm]
                        Case 3 'enmAxis.PAxis
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1068002, eMessageLevel.Error) '[Y軸馬達Alarm]
                    End Select
                    '--- Soni / 2017.01.20 訊息錯誤修正 ---
                    gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                    sys.RunStatus = enmRunStatus.Alarm
                End If
            End If
        End If
        If sys.AxisZ <> -1 Then
            If gCMotion.GetAxisState(sys.AxisZ) = Premtek.Base.CommandStatus.Sucessed Then
                If gCMotion.AxisParameter(sys.AxisZ).MotionStatus = AxisState.STA_AX_ERROR_STOP Then
                    If gCMotion.AxisResetError(sys.AxisZ) = Premtek.Base.CommandStatus.Sucessed Then
                        gCMotion.AxisParameter(sys.AxisZ).MotionStatus = Nothing
                    End If
                End If
            End If
            If gCMotion.CheckMotorStatus(sys.AxisZ) = Premtek.Base.CommandStatus.Sucessed Then
                If gCMotion.AxisParameter(sys.AxisZ).MotionIOStatus.blnPEL = True Then
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
                        '[Note]:碰極限後就強制停止動作
                        Call gCMotion.GpPauseMovePath(gCMotion.SyncParameter(sys.StageNo))
                        Call gCMotion.GpResetPath(gCMotion.SyncParameter(sys.StageNo))
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---
                        Select Case sys.StageNo
                            Case 0 'enmAxis.ZAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1032007, eMessageLevel.Error) '[Z軸馬達Alarm]
                            Case 1 'enmAxis.WAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1044007, eMessageLevel.Error) '[Z軸馬達Alarm]
                            Case 2 'enmAxis.TAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1062007, eMessageLevel.Error) '[Z軸馬達Alarm]
                            Case 3 'enmAxis.QAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1069007, eMessageLevel.Error) '[Z軸馬達Alarm]
                        End Select
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---

                        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If
                If gCMotion.AxisParameter(sys.AxisZ).MotionIOStatus.blnNEL = True Then
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
                        '[Note]:碰極限後就強制停止動作
                        Call gCMotion.GpPauseMovePath(gCMotion.SyncParameter(sys.StageNo))
                        Call gCMotion.GpResetPath(gCMotion.SyncParameter(sys.StageNo))
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---
                        Select Case sys.StageNo
                            Case 0 'enmAxis.ZAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1032008, eMessageLevel.Error) '[Z軸馬達Alarm]
                            Case 1 'enmAxis.WAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1044008, eMessageLevel.Error) '[Z軸馬達Alarm]
                            Case 2 'enmAxis.TAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1062008, eMessageLevel.Error) '[Z軸馬達Alarm]
                            Case 3 'enmAxis.QAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1069008, eMessageLevel.Error) '[Z軸馬達Alarm]
                        End Select
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---

                        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If
                If gCMotion.AxisParameter(sys.AxisZ).MotionIOStatus.blnALM = True Then
                    '--- Soni / 2017.01.20 訊息錯誤修正 ---
                    Select Case sys.StageNo
                        Case 0 'enmAxis.ZAxis
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1032002, eMessageLevel.Error) '[Z軸馬達Alarm]
                        Case 1 'enmAxis.WAxis
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1044002, eMessageLevel.Error) '[Z軸馬達Alarm]
                        Case 2 'enmAxis.TAxis
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1062002, eMessageLevel.Error) '[Z軸馬達Alarm]
                        Case 3 'enmAxis.QAxis
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1069002, eMessageLevel.Error) '[Z軸馬達Alarm]
                    End Select
                    '--- Soni / 2017.01.20 訊息錯誤修正 ---
                    gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                    sys.RunStatus = enmRunStatus.Alarm
                End If

                '[說明]:解除煞車，不再強制更改剎車
                'If gblnMotorUnlock = True Then

                'Else
                'Z 軸Ready訊號亮則關閉剎車，否則開啟剎車
                If gCMotion.AxisParameter(sys.AxisZ).MotionIOStatus.blnRDY = True Then
                    gDOCollection.SetState(enmDO.UnlockZAxis, True) '解除煞車
                Else
                    gDOCollection.SetState(enmDO.UnlockZAxis, False) '煞車
                End If
                'End If
            End If
        End If

        If sys.AxisA <> -1 Then
            If gCMotion.GetAxisState(sys.AxisA) = Premtek.Base.CommandStatus.Sucessed Then
                If gCMotion.AxisParameter(sys.AxisA).MotionStatus = AxisState.STA_AX_ERROR_STOP Then
                    If gCMotion.AxisResetError(sys.AxisA) = Premtek.Base.CommandStatus.Sucessed Then
                        gCMotion.AxisParameter(sys.AxisA).MotionStatus = Nothing
                    End If
                End If
            End If
            If gCMotion.CheckMotorStatus(sys.AxisA) = Premtek.Base.CommandStatus.Sucessed Then
                If gCMotion.AxisParameter(sys.AxisA).MotionIOStatus.blnPEL = True Then
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
                        '[Note]:碰極限後就強制停止動作
                        Call gCMotion.GpPauseMovePath(gCMotion.SyncParameter(sys.StageNo))
                        Call gCMotion.GpResetPath(gCMotion.SyncParameter(sys.StageNo))


                        gEqpMsg.Add("CSystem_Thread.Action", Error_1030002, eMessageLevel.Error) '[A軸馬達Alarm]
                        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If
                If gCMotion.AxisParameter(sys.AxisA).MotionIOStatus.blnNEL = True Then
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
                        '[Note]:碰極限後就強制停止動作
                        Call gCMotion.GpPauseMovePath(gCMotion.SyncParameter(sys.StageNo))
                        Call gCMotion.GpResetPath(gCMotion.SyncParameter(sys.StageNo))
                        gEqpMsg.Add("CSystem_Thread.Action", Error_1030002, eMessageLevel.Error) '[A軸馬達Alarm]
                        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If
                If gCMotion.AxisParameter(sys.AxisA).MotionIOStatus.blnALM = True Then
                    'TODO:AxisA Alram
                    gEqpMsg.Add("CSystem_Thread.Action", Error_1031002, eMessageLevel.Error) '[A軸馬達Alarm]
                    gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                    sys.RunStatus = enmRunStatus.Alarm
                End If
            End If
        End If

        If sys.AxisB <> -1 Then
            If gCMotion.GetAxisState(sys.AxisB) = Premtek.Base.CommandStatus.Sucessed Then
                If gCMotion.AxisParameter(sys.AxisB).MotionStatus = AxisState.STA_AX_ERROR_STOP Then
                    If gCMotion.AxisResetError(sys.AxisB) = Premtek.Base.CommandStatus.Sucessed Then
                        gCMotion.AxisParameter(sys.AxisB).MotionStatus = Nothing
                    End If
                End If
            End If
            If gCMotion.CheckMotorStatus(sys.AxisB) = Premtek.Base.CommandStatus.Sucessed Then
                If gCMotion.AxisParameter(sys.AxisB).MotionIOStatus.blnPEL = True Then
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
                        '[Note]:碰極限後就強制停止動作
                        Call gCMotion.GpPauseMovePath(gCMotion.SyncParameter(sys.StageNo))
                        Call gCMotion.GpResetPath(gCMotion.SyncParameter(sys.StageNo))
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---
                        Select Case sys.StageNo
                            Case 0 'enmAxis.BAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1034007, eMessageLevel.Error) '[X軸馬達Alarm]
                            Case 1
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1046007, eMessageLevel.Error) '[X軸馬達Alarm]
                            Case 2
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1064007, eMessageLevel.Error) '[X軸馬達Alarm]
                            Case 3
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1071007, eMessageLevel.Error) '[X軸馬達Alarm]
                        End Select
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---

                        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If
                If gCMotion.AxisParameter(sys.AxisB).MotionIOStatus.blnNEL = True Then
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
                        '[Note]:碰極限後就強制停止動作
                        Call gCMotion.GpPauseMovePath(gCMotion.SyncParameter(sys.StageNo))
                        Call gCMotion.GpResetPath(gCMotion.SyncParameter(sys.StageNo))
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---
                        Select Case sys.StageNo
                            Case 0 'enmAxis.BAxis
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1034008, eMessageLevel.Error) '[X軸馬達Alarm]
                            Case 1
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1046008, eMessageLevel.Error) '[X軸馬達Alarm]
                            Case 2
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1064008, eMessageLevel.Error) '[X軸馬達Alarm]
                            Case 3
                                gEqpMsg.Add("CSystem_Thread.Action", Error_1071008, eMessageLevel.Error) '[X軸馬達Alarm]
                        End Select
                        '--- Soni / 2017.01.20 訊息錯誤修正 ---

                        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If
                If gCMotion.AxisParameter(sys.AxisB).MotionIOStatus.blnALM = True Then
                    '--- Soni / 2017.01.20 訊息錯誤修正 ---
                    Select Case sys.StageNo
                        Case 0 'enmAxis.BAxis
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1034002, eMessageLevel.Error) '[X軸馬達Alarm]
                        Case 1
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1046002, eMessageLevel.Error) '[X軸馬達Alarm]
                        Case 2
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1064002, eMessageLevel.Error) '[X軸馬達Alarm]
                        Case 3
                            gEqpMsg.Add("CSystem_Thread.Action", Error_1071002, eMessageLevel.Error) '[X軸馬達Alarm]
                    End Select
                    '--- Soni / 2017.01.20 訊息錯誤修正 ---

                    gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                    sys.RunStatus = enmRunStatus.Alarm
                End If
            End If
        End If

        If sys.AxisC <> -1 Then
            If gCMotion.GetAxisState(sys.AxisC) = Premtek.Base.CommandStatus.Sucessed Then
                If gCMotion.AxisParameter(sys.AxisC).MotionStatus = AxisState.STA_AX_ERROR_STOP Then
                    If gCMotion.AxisResetError(sys.AxisC) = Premtek.Base.CommandStatus.Sucessed Then
                        gCMotion.AxisParameter(sys.AxisC).MotionStatus = Nothing
                    End If
                End If
            End If
            If gCMotion.CheckMotorStatus(sys.AxisC) = Premtek.Base.CommandStatus.Sucessed Then
                If gCMotion.AxisParameter(sys.AxisC).MotionIOStatus.blnPEL = True Then
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
                        '[Note]:碰極限後就強制停止動作
                        Call gCMotion.GpPauseMovePath(gCMotion.SyncParameter(sys.StageNo))
                        Call gCMotion.GpResetPath(gCMotion.SyncParameter(sys.StageNo))
                        gEqpMsg.Add("CSystem_Thread.Action", Error_1030002, eMessageLevel.Error) '[C軸馬達Alarm]
                        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If
                If gCMotion.AxisParameter(sys.AxisC).MotionIOStatus.blnNEL = True Then
                    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
                        '[Note]:碰極限後就強制停止動作
                        Call gCMotion.GpPauseMovePath(gCMotion.SyncParameter(sys.StageNo))
                        Call gCMotion.GpResetPath(gCMotion.SyncParameter(sys.StageNo))
                        gEqpMsg.Add("CSystem_Thread.Action", Error_1030002, eMessageLevel.Error) '[C軸馬達Alarm]
                        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If
                If gCMotion.AxisParameter(sys.AxisC).MotionIOStatus.blnALM = True Then
                    'TODO:AxisC Alram
                    gEqpMsg.Add("CSystem_Thread.Action", Error_1031002, eMessageLevel.Error) '[C軸馬達Alarm]
                    gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                    sys.RunStatus = enmRunStatus.Alarm
                End If
            End If
        End If
    End Sub

    ''' <summary>[Stage控制層(LevelNo3端:控制底層{Initial、定位、測高、點膠......})]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub System_DispStage(ByRef sys As sSysParam)
        '=== 外部狀態確認 ===
        CheckStageMotorAlarm(sys)
        '=== 外部狀態確認 ===

        '[Note]:判斷系統狀態是否為可接收命令
        If sys.RunStatus = enmRunStatus.None Or sys.RunStatus = enmRunStatus.Finish Then
            '[Note]:有下一筆命令
            If sys.Command <> eSysCommand.None Then
                sys.ExecuteCommand = sys.Command            '接收命令至執行命令
                sys.Command = eSysCommand.None              '接收暫存器清空 避免Finish又進來
                sys.SysNum = sSysParam.SysLoopStart         '系統起始索引
                sys.RunStatus = enmRunStatus.Running        '系統狀態改為運行中
            End If
        End If

        '[Note]:外部暫停, 且系統可暫停時, 不執行動作流程
        If sys.ExternalPause = True And sys.IsCanPause = True Then
            Exit Sub
        End If

        If sys.RunStatus = enmRunStatus.Running Then
            Select Case sys.ExecuteCommand
                Case eSysCommand.Home
                    DispStage_HomeAction(sys)

                Case eSysCommand.ProductLoading
                    DispStage_ProductLoadingAction(sys)

                Case eSysCommand.CCDFix
                    DispStage_CCDFixAction(sys)

                Case eSysCommand.LaserReader
                    DispStage_LaserReaderAction(sys)

                Case eSysCommand.Dispensing
                    DispStage_DispensingAction(sys)

                Case eSysCommand.ManuallyWeightUnit
                    sys.RunStatus = gWeight.SubDispStage_WeighingAction(sys, gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve), gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve))

                Case eSysCommand.PrevDispense
                    '目前沒有這個處理程序先預留
                    sys.RunStatus = enmRunStatus.Finish

                Case eSysCommand.AbnormalUnload '異常 強制退料
                    gCActionAbnormalUnload800AQ.Run(sys)

                Case eSysCommand.Purge
                    sys.RunStatus = gPurgeAction.Run(sys)

                Case eSysCommand.WeightUnit
                    sys.RunStatus = gWeight.SubDispStage_WeighingAction(sys, gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve), gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve))

                Case eSysCommand.Safe
                    DispStage_SafeAction(sys)

            End Select
        End If
    End Sub

    ''' <summary>[Stage控制層(LevelNo3端:監控{溫度})]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub System_MonitorDisp(ByRef sys As sSysParam)
        '[Note]:判斷系統狀態是否為可接收命令
        If sys.RunStatus = enmRunStatus.None Or sys.RunStatus = enmRunStatus.Finish Then
            '[Note]:有下一筆命令
            If sys.Command <> eSysCommand.None Then
                sys.ExecuteCommand = sys.Command            '接收命令至執行命令
                sys.Command = eSysCommand.None              '接收暫存器清空 避免Finish又進來
                sys.SysNum = sSysParam.SysLoopStart         '系統起始索引
                sys.RunStatus = enmRunStatus.Running        '系統狀態改為運行中
            End If
        End If

        If sys.RunStatus = enmRunStatus.Running Then
            Select Case sys.ExecuteCommand
                Case eSysCommand.Monitor
                    MonitorDisp_MonitorAction(sys)

            End Select
        Else
            Debug.Print("GG")
        End If
    End Sub

    ''' <summary>[Stage控制層(LevelNo4端:控制點膠周邊動作-->清膠、除膠、秤重)]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub System_SubDisp(ByRef sys As sSysParam)
        '[Note]:判斷系統狀態是否為可接收命令
        If sys.RunStatus = enmRunStatus.None Or sys.RunStatus = enmRunStatus.Finish Then
            '[Note]:有下一筆命令
            If sys.Command <> eSysCommand.None Then
                sys.ExecuteCommand = sys.Command            '接收命令至執行命令
                sys.Command = eSysCommand.None              '接收暫存器清空 避免Finish又進來
                sys.SysNum = sSysParam.SysLoopStart         '系統起始索引
                sys.RunStatus = enmRunStatus.Running        '系統狀態改為運行中
            End If
        End If

        '[Note]:外部暫停, 且系統可暫停時, 不執行動作流程
        If sys.ExternalPause = True And sys.IsCanPause = True Then
            Exit Sub
        End If

        If sys.RunStatus = enmRunStatus.Running Then
            Select Case sys.ExecuteCommand
                Case eSysCommand.ChangeGlue
                    gActionChangeGlue.Run(sys)

                Case eSysCommand.Purge
                    Select Case sys.StageNo
                        Case enmStage.No1
                            gSYS(eSys.SubDisp1).RunStatus = gPurgeAction.Run(sys)
                        Case enmStage.No2
                            gSYS(eSys.SubDisp2).RunStatus = gPurgeAction.Run(sys)
                        Case enmStage.No3
                            gSYS(eSys.SubDisp3).RunStatus = gPurgeAction.Run(sys)
                        Case enmStage.No4
                            gSYS(eSys.SubDisp4).RunStatus = gPurgeAction.Run(sys)
                    End Select

                Case eSysCommand.WeightUnit
                    sys.RunStatus = gWeight.SubDispStage_WeighingAction(sys, gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve), gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve))

                Case eSysCommand.DispenserAutoSearch
                    gValveHeightAutoSearchAction.Run(sys) 'Soni / 2017.05.05 SubDispStage_DispenserAutoSearchAction方法抽出成CActionValveHeightAutoSearch類別

                Case eSysCommand.CCDValveAutoCalibrationXY
                    Select Case sys.StageNo
                        Case enmStage.No1
                            gSYS(eSys.SubDisp1).RunStatus = gCCDValveAutoCalibXYAction.Run(sys)
                        Case enmStage.No2
                            gSYS(eSys.SubDisp2).RunStatus = gCCDValveAutoCalibXYAction.Run(sys)
                        Case enmStage.No3
                            gSYS(eSys.SubDisp3).RunStatus = gCCDValveAutoCalibXYAction.Run(sys)
                        Case enmStage.No4
                            gSYS(eSys.SubDisp4).RunStatus = gCCDValveAutoCalibXYAction.Run(sys)
                    End Select

                Case eSysCommand.Safe
                    gMoveToSafePosAction.Run(sys)

            End Select
        End If
    End Sub

    ''' <summary>[傳送帶系統]</summary>
    ''' <param name="SYS"></param>
    ''' <remarks></remarks>
    Sub System_Conveyor1(ByRef SYS As sSysParam)
        '[Note]:判斷系統狀態是否為可接收命令
        If SYS.RunStatus = enmRunStatus.None Or SYS.RunStatus = enmRunStatus.Finish Then
            '[Note]:有下一筆命令
            If SYS.Command <> eSysCommand.None Then
                SYS.ExecuteCommand = SYS.Command            '接收命令至執行命令
                SYS.Command = eSysCommand.None              '接收暫存器清空 避免Finish又進來
                SYS.SysNum = sSysParam.SysLoopStart         '系統起始索引
                SYS.RunStatus = enmRunStatus.Running        '系統狀態改為運行中
            End If
        End If

        If SYS.RunStatus = enmRunStatus.Running Then
            Select Case SYS.ExecuteCommand
                Case eSysCommand.Home
                    Select Case gSSystemParameter.MachineType
                        Case enmMachineType.eDTS330A
                            SYS.RunStatus = ConveyorA.ConveyorHome(SYS)
                        Case enmMachineType.DCSW_800AQ, enmMachineType.DCS_500AD
                            gConveyorW800AQ.Home(SYS)
                        Case enmMachineType.DCS_F230A
                            gConveyorF230A.HomePartA(SYS)
                        Case enmMachineType.DCS_350A
                    End Select

                Case eSysCommand.AutoRun '整機生產
                    Select Case gSSystemParameter.MachineType
                        Case enmMachineType.eDTS330A
                            ConveyorA.ConveyorStation1(gSYS(eSys.Station1)) '[說明] Conveyor Station1 動作流程
                            ConveyorA.ConveyorStation2(gSYS(eSys.Station1)) '[說明] Conveyor Station2 動作流程
                            ConveyorA.ConveyorStation3(gSYS(eSys.Station1)) '[說明] Conveyor Station3 動作流程
                        Case enmMachineType.DCSW_800AQ
                        Case enmMachineType.DCS_F230A
                        Case enmMachineType.DCS_350A
                    End Select

                Case eSysCommand.HomeA
                    Select Case gSSystemParameter.MachineType
                        Case enmMachineType.eDTS330A
                        Case enmMachineType.DCSW_800AQ, enmMachineType.DCS_500AD
                            gConveyorW800AQ.HomePartA(SYS)
                        Case enmMachineType.DCS_F230A
                            gConveyorF230A.HomePartA(SYS)
                        Case enmMachineType.DCS_350A
                            gConveyor350A.Initial(SYS)
                    End Select

                Case eSysCommand.Loading

                Case eSysCommand.LoadA '進料A
                    Select Case gSSystemParameter.MachineType
                        Case enmMachineType.eDTS330A
                            SYS.RunStatus = Conveyor_LoadAAction(gSYS(eSys.Conveyor1))
                        Case enmMachineType.DCSW_800AQ, enmMachineType.DCS_500AD
                            gConveyorW800AQ.LoadA(SYS, Not gSSystemParameter.PassLUL)
                        Case enmMachineType.DCS_F230A
                            gConveyorF230A.LoadA(SYS, Not gSSystemParameter.PassLUL)
                        Case enmMachineType.DCS_350A
                            gConveyor350A.Load(SYS, Not gSSystemParameter.PassLUL)
                    End Select

                Case eSysCommand.UnloadA '退料A
                    Select Case gSSystemParameter.MachineType
                        Case enmMachineType.eDTS330A
                            SYS.RunStatus = Conveyor_UnloadAAction(gSYS(eSys.Conveyor1))
                        Case enmMachineType.DCSW_800AQ, enmMachineType.DCS_500AD
                            gConveyorW800AQ.UnloadA(SYS, Not gSSystemParameter.PassLUL)
                        Case enmMachineType.DCS_F230A
                            gConveyorF230A.UnloadA(SYS, Not gSSystemParameter.PassLUL)
                        Case enmMachineType.DCS_350A
                            gConveyor350A.Unload(SYS, Not gSSystemParameter.PassLUL)
                    End Select

                Case eSysCommand.HomeB
                    Select Case gSSystemParameter.MachineType
                        Case enmMachineType.eDTS330A
                        Case enmMachineType.DCSW_800AQ
                            gConveyorW800AQ.HomePartB(SYS)
                        Case enmMachineType.DCS_F230A
                        Case enmMachineType.DCS_350A
                    End Select

                Case eSysCommand.LoadB '進料B
                    Select Case gSSystemParameter.MachineType
                        Case enmMachineType.eDTS330A
                            SYS.RunStatus = Conveyor_LoadBAction(gSYS(eSys.Conveyor1))
                        Case enmMachineType.DCSW_800AQ
                            gConveyorW800AQ.LoadB(SYS, Not gSSystemParameter.PassLUL)
                        Case enmMachineType.DCS_F230A
                        Case enmMachineType.DCS_350A
                    End Select

                Case eSysCommand.UnloadB '退料B
                    Select Case gSSystemParameter.MachineType
                        Case enmMachineType.eDTS330A
                            SYS.RunStatus = Conveyor_UnloadBAction(gSYS(eSys.Conveyor1))
                        Case enmMachineType.DCSW_800AQ
                            gConveyorW800AQ.UnloadB(SYS, Not gSSystemParameter.PassLUL)
                        Case enmMachineType.DCS_F230A
                        Case enmMachineType.DCS_350A
                    End Select
            End Select
        End If
    End Sub

#End Region

#Region "主控層(LevelNo1端:控制 MachineA、MachineB、 Conveyor)"
    ''' <summary>[整機復歸流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub Overall_HomeAction(ByRef sys As sSysParam)

        Static mMachineNo As Integer
        Static mConveyorStatus As Premtek.eConveyorStatus

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                '[Note]:先將狀態清除
                gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None
                gSYS(eSys.OverAll).Act(eAct.AutoRun).RunStatus = enmRunStatus.None
                For mMachineNo = eSys.MachineA To gSSystemParameter.MachineMax
                    gSYS(mMachineNo).RunStatus = enmRunStatus.None
                Next

                '[Note]:自行判斷哪一側的模組狀況
                Call ReDimStageState()
                sys.SysNum = 1500

            Case 1500
                '[Note]:對各機組下復歸之命令

                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        gSYS(eSys.MachineA).Command = eSysCommand.Home
                        gSYS(eSys.MachineB).Command = eSysCommand.Home

                    Case Else
                        gSYS(eSys.MachineA).Command = eSysCommand.Home

                End Select
                sys.SysNum = 2000

                ''[Note]:I/O Reset(TODO:一邊作業一邊復歸會掛)
                'If gDOCollection.ReSetDO() Then
                '    sys.SysNum = 1500
                'Else
                '    gEqpMsg.AddHistoryAlarm("Alarm_2080002", "Overall_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2080002))
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If

            Case 2000
                '[Note]:I/O Reset()
                If gDOCollection.ReSetDO() = False Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2080002", "Overall_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2080002))
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                '[說明]:初始化後Heater紀錄開啟 For 800A   20161129
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        '   If gfrmUIViewer.Heater = True Then   20161206
                        If gSSystemParameter.EnableInitialHotPlate = True Then
                            If gfrmUIViewer.HeaterOn(0) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn1, True)
                            End If
                            If gfrmUIViewer.HeaterOn(1) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn2, True)
                            End If
                            If gfrmUIViewer.HeaterOn(2) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn3, True)
                            End If
                            If gfrmUIViewer.HeaterOn(3) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn4, True)
                            End If
                            If gfrmUIViewer.HeaterOn(4) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn5, True)
                            End If
                            If gfrmUIViewer.HeaterOn(5) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn6, True)
                            End If
                            If gfrmUIViewer.HeaterOn(6) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn7, True)
                            End If
                            If gfrmUIViewer.HeaterOn(7) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn8, True)
                            End If
                            If gfrmUIViewer.HeaterOn(8) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn9, True)
                            End If
                            If gfrmUIViewer.HeaterOn(9) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn10, True)
                            End If
                            If gfrmUIViewer.HeaterOn(10) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn11, True)
                            End If
                            If gfrmUIViewer.HeaterOn(11) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn12, True)
                            End If
                        End If
                    Case enmMachineType.DCS_500AD
                        If gSSystemParameter.EnableInitialHotPlate = True Then
                            If gfrmUIViewer.HeaterOn(0) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn1, True)
                            End If
                            If gfrmUIViewer.HeaterOn(1) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn2, True)
                            End If
                            If gfrmUIViewer.HeaterOn(2) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn3, True)
                            End If
                            If gfrmUIViewer.HeaterOn(3) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn4, True)
                            End If
                            If gfrmUIViewer.HeaterOn(4) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn5, True)
                            End If
                            If gfrmUIViewer.HeaterOn(5) = True Then
                                gDOCollection.SetState(enmDO.HeaterOn6, True)
                            End If
                        End If
                End Select

                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        If IsChuckVacuumReady(enmMachineStation.MachineA) = True Then
                            If IsChuckVacuumReady(enmMachineStation.MachineB) = True Then
                                '[Note]:把A機Conveyor原本要動的I/O省略掉
                                gDOCollection.SetState(enmDO.Station2ChuckVacuum2, True)
                                gDOCollection.SetState(enmDO.Station2StopperUp, True)
                                gDOCollection.SetState(enmDO.Station2StopperDown, False)
                                '[Note]:把B機Conveyor原本要動的I/O省略掉
                                gDOCollection.SetState(enmDO.Station3ChuckVacuum2, True)
                                gDOCollection.SetState(enmDO.Station3StopperUp, True)
                                gDOCollection.SetState(enmDO.Station3StopperDown, False)
                            Else
                                '[Note]:把A機Conveyor原本要動的I/O省略掉
                                gDOCollection.SetState(enmDO.Station2ChuckVacuum2, True)
                                gDOCollection.SetState(enmDO.Station2StopperUp, True)
                                gDOCollection.SetState(enmDO.Station2StopperDown, False)
                            End If
                        Else
                            If IsChuckVacuumReady(enmMachineStation.MachineB) = True Then
                                '[Note]:把B機Conveyor原本要動的I/O省略掉
                                gDOCollection.SetState(enmDO.Station3ChuckVacuum2, True)
                                gDOCollection.SetState(enmDO.Station3StopperUp, True)
                                gDOCollection.SetState(enmDO.Station3StopperDown, False)
                            End If
                        End If
                    Case enmMachineType.DCS_500AD
                        If IsChuckVacuumReady(enmMachineStation.MachineA) = True Then
                            '[Note]:把A機Conveyor原本要動的I/O省略掉
                            gDOCollection.SetState(enmDO.Station2ChuckVacuum2, True)
                            gDOCollection.SetState(enmDO.Station2StopperUp, True)
                            gDOCollection.SetState(enmDO.Station2StopperDown, False)
                        End If

                    Case Else
                        If IsChuckVacuumReady(enmMachineStation.MachineA) = True Then
                            '[Note]:把A機Conveyor原本要動的I/O省略掉
                            gDOCollection.SetState(enmDO.Station2ChuckVacuum2, True)
                            gDOCollection.SetState(enmDO.Station2StopperUp, True)
                            gDOCollection.SetState(enmDO.Station2StopperDown, False)
                        End If

                End Select
                sys.SysNum = 2100

            Case 2100
                '[Note]:流道誰復歸
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        If IsChuckVacuumReady(enmMachineStation.MachineA) = True Then
                            If IsChuckVacuumReady(enmMachineStation.MachineB) = True Then
                                mConveyorStatus = Premtek.eConveyorStatus.Station_None
                            Else
                                mConveyorStatus = Premtek.eConveyorStatus.Station_B
                                gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.None
                                gSYS(eSys.Conveyor1).Command = eSysCommand.HomeB 'B機復歸
                            End If
                        Else
                            If IsChuckVacuumReady(enmMachineStation.MachineB) = True Then
                                mConveyorStatus = Premtek.eConveyorStatus.Station_A
                                gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.None
                                gSYS(eSys.Conveyor1).Command = eSysCommand.HomeA 'A機復歸
                            Else
                                mConveyorStatus = Premtek.eConveyorStatus.Station_AB
                                gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.None
                                gSYS(eSys.Conveyor1).Command = eSysCommand.Home 'AB機復歸
                            End If

                        End If


                        If (cls800AQ_LUL.Loader.IsOpen = False) Then
                            cls800AQ_LUL.Loader.Open(cls800AQ_LUL.LoaderPort.PortName, cls800AQ_LUL.LoaderPort.BuadRade, cls800AQ_LUL.LoaderPort.Parity, cls800AQ_LUL.LoaderPort.DataBits, cls800AQ_LUL.LoaderPort.StopBits)
                        End If

                        If (cls800AQ_LUL.Unloader.IsOpen = False) Then
                            cls800AQ_LUL.Unloader.Open(cls800AQ_LUL.UnloaderPort.PortName, cls800AQ_LUL.UnloaderPort.BuadRade, cls800AQ_LUL.UnloaderPort.Parity, cls800AQ_LUL.UnloaderPort.DataBits, cls800AQ_LUL.UnloaderPort.StopBits)
                        End If
                    Case enmMachineType.DCS_500AD
                        If IsChuckVacuumReady(enmMachineStation.MachineA) = True Then
                            mConveyorStatus = Premtek.eConveyorStatus.Station_None
                        Else
                            mConveyorStatus = Premtek.eConveyorStatus.Station_A
                            gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.None
                            gSYS(eSys.Conveyor1).Command = eSysCommand.HomeA 'A機復歸
                        End If


                        If cls800AQ_LUL.IsLoaderPass = True Then

                        ElseIf cls800AQ_LUL.IsLoaderPass = False Then
                            If (cls800AQ_LUL.Loader.IsOpen = False) Then
                                cls800AQ_LUL.Loader.Open(cls800AQ_LUL.LoaderPort.PortName, cls800AQ_LUL.LoaderPort.BuadRade, cls800AQ_LUL.LoaderPort.Parity, cls800AQ_LUL.LoaderPort.DataBits, cls800AQ_LUL.LoaderPort.StopBits)
                            End If

                            If (cls800AQ_LUL.Unloader.IsOpen = False) Then
                                cls800AQ_LUL.Unloader.Open(cls800AQ_LUL.UnloaderPort.PortName, cls800AQ_LUL.UnloaderPort.BuadRade, cls800AQ_LUL.UnloaderPort.Parity, cls800AQ_LUL.UnloaderPort.DataBits, cls800AQ_LUL.UnloaderPort.StopBits)
                            End If
                        End If
                    Case enmMachineType.DCS_350A
                        If IsChuckVacuumReady(enmMachineStation.MachineA) = False Then
                            mConveyorStatus = Premtek.eConveyorStatus.Station_A
                            gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.None
                            gSYS(eSys.Conveyor1).Command = eSysCommand.HomeA 'A機復歸
                        End If

                        If IsChuckVacuumReady(enmMachineStation.MachineB) = False Then
                            mConveyorStatus = Premtek.eConveyorStatus.Station_B
                            gSYS(eSys.Conveyor2).RunStatus = enmRunStatus.None
                            gSYS(eSys.Conveyor2).Command = eSysCommand.HomeA 'B機復歸
                        End If

                        If IsChuckVacuumReady(enmMachineStation.MachineA) = False AndAlso IsChuckVacuumReady(enmMachineStation.MachineB) = False Then
                            mConveyorStatus = Premtek.eConveyorStatus.Station_AB
                        End If

                    Case Else
                        mConveyorStatus = Premtek.eConveyorStatus.Station_None
                        If IsChuckVacuumReady(enmMachineStation.MachineA) = True Then
                            mConveyorStatus = Premtek.eConveyorStatus.Station_None
                        Else
                            gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.None
                            gSYS(eSys.Conveyor1).Command = eSysCommand.Home 'A機復歸
                        End If

                End Select
                sys.SysNum = 2500

            Case 2500
                '[Note]:確認復歸完成
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home Then
                            If gSYS(eSys.MachineB).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineB).ExecuteCommand = eSysCommand.Home Then
                                sys.SysNum = 3000
                            ElseIf gSYS(eSys.MachineB).RunStatus = enmRunStatus.Alarm Then 'Home Error
                                sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                                sys.RunStatus = enmRunStatus.Alarm
                                Exit Sub
                            End If
                        ElseIf gSYS(eSys.MachineA).RunStatus = enmRunStatus.Alarm Then 'Home Error
                            sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                    Case enmMachineType.DCS_500AD
                        If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home Then
                            sys.SysNum = 3000
                        ElseIf gSYS(eSys.MachineA).RunStatus = enmRunStatus.Alarm Then 'Home Error
                            sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                    Case Else
                        If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home Then
                            sys.SysNum = 3000
                        ElseIf gSYS(eSys.MachineA).RunStatus = enmRunStatus.Alarm Then 'Home Error
                            sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If

                End Select

            Case 3000
                '[Note]:確認復歸完成
                Select Case mConveyorStatus
                    Case Premtek.eConveyorStatus.Station_A
                        If gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.HomeA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish Then
                            sys.SysNum = 9000
                        ElseIf gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.HomeA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Alarm Then '有料應該還是要能做, 狀態後面再改
                            sys.SysNum = 9000
                        End If

                    Case Premtek.eConveyorStatus.Station_AB
                        If (gSSystemParameter.MachineType = enmMachineType.DCS_350A) Then
                            If gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.HomeA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish AndAlso gSYS(eSys.Conveyor2).ExecuteCommand = eSysCommand.HomeA AndAlso gSYS(eSys.Conveyor2).RunStatus = enmRunStatus.Finish Then
                                sys.SysNum = 9000
                            End If
                        Else
                            If gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.Home AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish Then
                                sys.SysNum = 9000
                            End If
                        End If

                    Case Premtek.eConveyorStatus.Station_B
                        If (gSSystemParameter.MachineType = enmMachineType.DCS_350A) Then
                            If gSYS(eSys.Conveyor2).ExecuteCommand = eSysCommand.HomeA AndAlso gSYS(eSys.Conveyor2).RunStatus = enmRunStatus.Finish Then
                                sys.SysNum = 9000
                            End If
                        Else
                            If gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.HomeB AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish Then
                                sys.SysNum = 9000
                            End If
                        End If

                    Case Premtek.eConveyorStatus.Station_None
                        sys.SysNum = 9000

                End Select

            Case 9000
                '[Note]:完成整機復歸
                sys.Act(eAct.Home).RunStatus = enmRunStatus.Finish
                sys.RunStatus = enmRunStatus.Finish
                Debug.Print("OverAll_HomeAction: " & sys.Act(eAct.Home).RunStatus)
                Exit Sub

        End Select

    End Sub

    ''' <summary>[整機生產流程(Auto Run)]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub Overall_AutoRunAction(ByRef sys As sSysParam)

        Static mIsBypassMachineA As Boolean
        Static mIsBypassMachineB As Boolean

        Static mConveyorStatus As Premtek.eConveyorStatus
        Static mTimeStopWatch As Stopwatch
        Static mStartTime As Decimal

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                If IsNothing(mTimeStopWatch) = True Then
                    mTimeStopWatch = New Stopwatch
                End If
                If IsNothing(gPriorHeatTimer(enmMachineStation.MachineA)) = True Then
                    gPriorHeatTimer(enmMachineStation.MachineA) = New Stopwatch
                End If
                If IsNothing(gPriorHeatTimer(enmMachineStation.MachineB)) = True Then
                    gPriorHeatTimer(enmMachineStation.MachineB) = New Stopwatch
                End If

                mTimeStopWatch.Restart()
                mStartTime = mTimeStopWatch.ElapsedMilliseconds

                mConveyorStatus = Premtek.eConveyorStatus.Station_None
                Call GetMachineBypassState(mIsBypassMachineA, mIsBypassMachineB)

                '[Note]:第一次進入流程時就先檢查需不需要點膠前動作(秤重含Purge)
                sys.SysNum = 1100

            Case 1100
                '[Note]:判斷本次作業會使用會使用到哪幾個Valve
                For mStageNo As Integer = eSys.DispStage1 To gSSystemParameter.StageMax
                    Call WhichValveIsUsed(gCRecipe, gSYS(mStageNo).StageNo, gIsUseValveNo1(gSYS(mStageNo).StageNo), gIsUseValveNo2(gSYS(mStageNo).StageNo))
                Next
                sys.SysNum = 1500

            Case 1500
                '[Note]:呼叫A機作點膠前動作(秤重含Purge)
                If mIsBypassMachineA = False Then
                    '[Note]:判斷要不要做
                    mConveyorStatus = Premtek.eConveyorStatus.Station_A
                    gSYS(eSys.MachineA).Command = eSysCommand.PrevDispense
                End If
                '[Note]:呼叫B機作點膠前動作(秤重含Purge)
                If mIsBypassMachineB = False Then
                    '[Note]:判斷要不要做
                    gSYS(eSys.MachineB).Command = eSysCommand.PrevDispense
                    If mIsBypassMachineA = False Then
                        mConveyorStatus = Premtek.eConveyorStatus.Station_AB
                    Else
                        mConveyorStatus = Premtek.eConveyorStatus.Station_B
                    End If
                End If
                sys.SysNum = 1600

            Case 1600
                '[Note]:若AB機都Bypass就GG了-->不應該成立，即便成立了也是在此迴圈繞
                If mIsBypassMachineB = False Then
                    '[Note]:B機先進料
                    sys.SysNum = 2000
                Else
                    If mIsBypassMachineA = False Then
                        '[Note]:A機進料
                        sys.SysNum = 3000
                    Else
                        gEqpMsg.AddHistoryAlarm("0000", "Overall_AutoRunAction", sys.SysNum, "請確認Recipe有生產步驟!", eMessageLevel.Warning)
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If

            Case 2000
                '[Note]:B機進料
                If Unit.B_IsVacuum(False) = True Or gSSystemParameter.IsBypassConveyor = True Then
                    '[Note]:上面有料片不用再進料
                    sys.SysNum = 2200
                Else
                    '[Note]:先給可入料之訊號
                    gSYS(eSys.Conveyor1).Command = eSysCommand.LoadB
                    sys.SysNum = 2100
                End If

            Case 2100
                '[Note]:B機進料完成
                If gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.LoadB AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish Then
                    'Debug.Print("B_LoadFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                    sys.SysNum = 2200
                End If

            Case 2200
                '[Note]:進料完成後，先檢查前一次的點膠前動作(秤重含Purge)跑完了沒，完成後才可以接續生產
                '[Note]:從入料完成後開始計時
                gPriorHeatTimer(enmMachineStation.MachineB).Restart()
                If mConveyorStatus = Premtek.eConveyorStatus.Station_AB Or mConveyorStatus = Premtek.eConveyorStatus.Station_B Then
                    If gSYS(eSys.MachineB).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineB).ExecuteCommand = eSysCommand.PrevDispense Then
                        sys.SysNum = 2300
                    End If
                Else
                    sys.SysNum = 2300
                End If

            Case 2300
                '[Note]:B機開始生產
                If gSSystemParameter.IsContinueLastRun = True Then
                    '[Note]:接續流程
                    gSYS(eSys.MachineB).Command = eSysCommand.ContinueLastRun
                Else
                    '[Note]:正常流程
                    gSYS(eSys.MachineB).Command = eSysCommand.AutoRun
                End If

                If mIsBypassMachineA = False Then
                    sys.SysNum = 3000
                Else
                    '[Note]:跳過A機檢查
                    sys.SysNum = 4000
                End If

            Case 3000
                '[Note]:A機進料
                If Unit.A_IsVacuum(False) = True Or gSSystemParameter.IsBypassConveyor = True Then
                    '[Note]:上面有料片不用再進料
                    sys.SysNum = 3200
                Else
                    '[Note]:先給可入料之訊號
                    gSYS(eSys.Conveyor1).Command = eSysCommand.LoadA
                    sys.SysNum = 3100
                End If

            Case 3100
                '[Note]:A機進料完成
                If gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.LoadA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish Then
                    'Debug.Print("A_LoadFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                    sys.SysNum = 3200
                End If

            Case 3200
                '[Note]:進料完成後，先檢查前一次的點膠前動作(秤重含Purge)跑完了沒，完成後才可以接續生產
                '[Note]:從入料完成後開始計時
                gPriorHeatTimer(enmMachineStation.MachineA).Restart()
                If mConveyorStatus = Premtek.eConveyorStatus.Station_AB Or mConveyorStatus = Premtek.eConveyorStatus.Station_A Then
                    If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.PrevDispense Then
                        sys.SysNum = 3300
                    End If
                Else
                    sys.SysNum = 3300
                End If

            Case 3300
                '[Note]:A機開始生產
                If gSSystemParameter.IsContinueLastRun = True Then
                    '[Note]:接續流程
                    gSYS(eSys.MachineA).Command = eSysCommand.ContinueLastRun
                Else
                    '[Note]:正常流程
                    gSYS(eSys.MachineA).Command = eSysCommand.AutoRun
                End If
                If mIsBypassMachineB = False Then
                    sys.SysNum = 4000
                Else
                    '[Note]:跳過B機檢查
                    sys.SysNum = 5000
                End If

            Case 4000
                '[Note]:B機生產完成
                If gSSystemParameter.IsContinueLastRun = True Then
                    If gSYS(eSys.MachineB).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineB).ExecuteCommand = eSysCommand.ContinueLastRun Then
                        'Debug.Print("B_AutoRunFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                        sys.SysNum = 4050
                    End If
                Else
                    If gSYS(eSys.MachineB).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineB).ExecuteCommand = eSysCommand.AutoRun Then
                        'Debug.Print("B_AutoRunFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                        sys.SysNum = 4050
                    End If
                End If

            Case 4050   '退料前匯出Rerun map 
                Dim name As String = ""
                Dim fileName As String = gSSystemParameter.RerunDataFolderPath
                If (gSSystemParameter.IsCompareWithMapData <> 0) Then
                    Dim path As String() = Split(gMapDataPathB, "\") '取得Map檔名
                    name = path(path.Length - 1)
                End If

                '檔案路徑
                fileName = fileName & name & "_" & System.DateTime.Now.ToString("yyyyMMdd_HH.mm.ss") & ".txt"
                '匯出Rerun map
                If OutputRerunMap(enmMachineStation.MachineB, fileName) = False Then
                    gEqpMsg.AddHistoryAlarm("0000", "Overall_AutoRunAction()", 4050, "Output rerun map fail", eMessageLevel.Information)
                End If

                sys.SysNum = 4100

            Case 4100
                '[Note]:呼叫B機作點膠前動作(秤重含Purge)(TODO:後續要不要A機一起秤，看數值會不會跳再決定要不要做)
                gSYS(eSys.MachineB).Command = eSysCommand.PrevDispense

                If gSSystemParameter.IsBypassConveyor = False Then
                    '[Note]:B機退料
                    gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadB
                    sys.SysNum = 4200
                Else
                    '[Note]:B機是優先退料，所以前面不會有東西肯定為(Station_None)
                    mConveyorStatus = Premtek.eConveyorStatus.Station_B
                    If mIsBypassMachineA = False Then
                        sys.SysNum = 5000
                    Else
                        '[Note]:跳過A機檢查
                        sys.SysNum = 6000
                    End If
                End If


            Case 4200
                '[Note]:B機退料完成
                If gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.UnloadB AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish Then
                    'Debug.Print("B_UnloadFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                    '[Note]:B機是優先退料，所以前面不會有東西肯定為(Station_None)
                    mConveyorStatus = Premtek.eConveyorStatus.Station_B
                    If mIsBypassMachineA = False Then
                        sys.SysNum = 5000
                    Else
                        '[Note]:跳過A機檢查
                        sys.SysNum = 6000
                    End If
                End If

            Case 5000
                '[Note]:A機生產完成
                If gSSystemParameter.IsContinueLastRun = True Then
                    If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.ContinueLastRun Then
                        'Debug.Print("A_AutoRunFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                        sys.SysNum = 5050
                    End If
                Else
                    If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.AutoRun Then
                        'Debug.Print("A_AutoRunFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                        sys.SysNum = 5050
                    End If
                End If

            Case 5050   '退料前匯出Rerun map 
                Dim name As String = ""
                Dim fileName As String = gSSystemParameter.RerunDataFolderPath
                If (gSSystemParameter.IsCompareWithMapData <> 0) Then
                    Dim path As String() = Split(gMapDataPathA, "\") '取得Map檔名
                    name = path(path.Length - 1)
                End If

                '檔案路徑
                fileName = fileName & name & "_" & System.DateTime.Now.ToString("yyyyMMdd_HH.mm.ss") & ".txt"
                '匯出Rerun map
                If OutputRerunMap(enmMachineStation.MachineA, fileName) = False Then
                    gEqpMsg.AddHistoryAlarm("0000", "Overall_AutoRunAction()", 5050, "Output rerun map fail", eMessageLevel.Information)
                End If

                sys.SysNum = 5100


            Case 5100
                '[Note]:呼叫A機作點膠前動作(秤重含Purge)
                gSYS(eSys.MachineA).Command = eSysCommand.PrevDispense

                If gSSystemParameter.IsBypassConveyor = False Then
                    '[Note]:A機退料
                    gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadA
                    sys.SysNum = 5200
                Else
                    '[Note]:前面只有進B機料片，所以狀態只有可能是(Station_B or Station_None)
                    Select Case mConveyorStatus
                        Case Premtek.eConveyorStatus.Station_B
                            mConveyorStatus = Premtek.eConveyorStatus.Station_AB

                        Case Premtek.eConveyorStatus.Station_None
                            mConveyorStatus = Premtek.eConveyorStatus.Station_A
                    End Select
                    sys.SysNum = 6000
                End If


            Case 5200
                '[Note]:A機退料完成
                '[Note]:點膠前動作(秤重含Purge)完成再下一個迴圈做確認
                If gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.UnloadA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish Then
                    'Debug.Print("A_UnloadFinish: " & Math.Abs(mStartTime - mTimeStopWatch.ElapsedMilliseconds))
                    '[Note]:前面只有進B機料片，所以狀態只有可能是(Station_B or Station_None)
                    Select Case mConveyorStatus
                        Case Premtek.eConveyorStatus.Station_B
                            mConveyorStatus = Premtek.eConveyorStatus.Station_AB

                        Case Premtek.eConveyorStatus.Station_None
                            mConveyorStatus = Premtek.eConveyorStatus.Station_A
                    End Select
                    sys.SysNum = 6000
                End If

            Case 6000
                '[Note]:完成-->從頭再來吧(無窮迴圈)
                'jimmy add 20170424
                ClearGGFile("GG")
                If gSSystemParameter.IsContinueLastRun = True Then
                    gSSystemParameter.IsContinueLastRun = False
                    gSSystemParameter.SaveContinueLastRun(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
                    '[Note]:整個流程跑一次就結束
                    sys.Act(eAct.AutoRun).RunStatus = enmRunStatus.Finish
                    sys.RunStatus = enmRunStatus.Finish
                    Exit Sub
                Else
                    If gCRecipe.IsJustOneRun = True Then
                        '[Note]:整個流程跑一次就結束(這是暫時速解的方法，後續再補手動流程改這個問題)
                        sys.Act(eAct.AutoRun).RunStatus = enmRunStatus.Finish
                        sys.RunStatus = enmRunStatus.Finish
                        Exit Sub
                    Else
                        sys.SysNum = 1500
                    End If
                End If

        End Select

    End Sub

    ''' <summary>[整機生產流程(Auto Run)(雙流道)]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub Overall_MulitConveyorAutoRunAction(ByRef sys As sSysParam)

        Static mTimeStopWatch As Stopwatch
        Static mStartTime As Decimal
        Static mConveyorStatus(eConveyor.Max) As Premtek.eConveyorStatus
        Static mConveyorNo1 As eConveyor
        Static mConveyorNo2 As eConveyor
        Static mSysConveyorNo1 As Integer
        Static mSysConveyorNo2 As Integer


        Static old_sys(enmStage.Max) As Integer
        If old_sys(sys.StageNo) <> sys.SysNum Then
            Debug.Print("多軌流程:" & sys.SysNum)
            old_sys(sys.StageNo) = sys.SysNum
        End If

        '[Note]:雙流道版本，基本上只適用在單機上不適用在雙機上

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                If IsNothing(mTimeStopWatch) = True Then
                    mTimeStopWatch = New Stopwatch
                End If
                If IsNothing(gPriorHeatTimer(enmMachineStation.MachineA)) = True Then
                    gPriorHeatTimer(enmMachineStation.MachineA) = New Stopwatch
                End If
                If IsNothing(gPriorHeatTimer(enmMachineStation.MachineB)) = True Then
                    gPriorHeatTimer(enmMachineStation.MachineB) = New Stopwatch
                End If

                '[Note]:使用流道的配置
                Select Case gSSystemParameter.ConveyorModel
                    Case eConveyorModel.eConveyorNo1
                        mConveyorNo1 = eConveyor.ConveyorNo1
                        mSysConveyorNo1 = eSys.Conveyor1

                    Case eConveyorModel.eConveyorNo2
                        mConveyorNo1 = eConveyor.ConveyorNo2
                        mSysConveyorNo1 = eSys.Conveyor2

                    Case eConveyorModel.eConveyorNo1No2
                        mConveyorNo1 = eConveyor.ConveyorNo1
                        mConveyorNo2 = eConveyor.ConveyorNo2
                        mSysConveyorNo1 = eSys.Conveyor1
                        mSysConveyorNo2 = eSys.Conveyor2
                End Select

                mTimeStopWatch.Restart()
                mStartTime = mTimeStopWatch.ElapsedMilliseconds
                sys.SysNum = 1500

            Case 1500
                '[Note]:判斷本次作業會使用會使用到哪幾個Valve
                For mStageNo As Integer = eSys.DispStage1 To gSSystemParameter.StageMax
                    Call WhichValveIsUsed(gCRecipe, gSYS(mStageNo).StageNo, gIsUseValveNo1(gSYS(mStageNo).StageNo), gIsUseValveNo2(gSYS(mStageNo).StageNo))
                Next
                sys.SysNum = 2000

            Case 2000
                '[Note]:呼叫機台作點膠前動作(秤重含Purge)
                '[Note]:第一次進入流程時就先檢查需不需要點膠前動作(秤重含Purge)
                gSYS(eSys.MachineA).Command = eSysCommand.PrevDispense
                sys.SysNum = 2100

            Case 2100
                '[Note]:流道A Load
                If IsChuckVacuumReady(mConveyorNo1) = True Or gSSystemParameter.IsBypassConveyor = True Then
                    '[Note]:上面有料片不用再進料
                    mConveyorStatus(mConveyorNo1) = Premtek.eConveyorStatus.Station_None
                    '[Note]:從入料完成後開始計時
                    gPriorHeatTimer(mConveyorNo1).Restart()
                    sys.SysNum = 2300
                Else
                    '[Note]:先給可入料之訊號
                    gSYS(mSysConveyorNo1).Command = eSysCommand.LoadA
                    mConveyorStatus(mConveyorNo1) = Premtek.eConveyorStatus.Station_A
                    sys.SysNum = 2200
                End If

            Case 2200
                '[Note]:流道A Load Finish
                If gSYS(mSysConveyorNo1).ExecuteCommand = eSysCommand.LoadA AndAlso gSYS(mSysConveyorNo1).RunStatus = enmRunStatus.Finish Then
                    '[Note]:從入料完成後開始計時
                    gPriorHeatTimer(mConveyorNo1).Restart()
                    sys.SysNum = 2300
                End If

            Case 2300
                '[Note]:進料完成後，先檢查前一次的點膠前動作(秤重含Purge)跑完了沒，完成後才可以接續生產
                If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.PrevDispense Then
                    sys.SysNum = 3000
                End If

            Case 3000
                '[Note]:流道A  AutoRun
                If gSSystemParameter.IsContinueLastRun = True Then
                    '[Note]:接續流程
                    gSYS(eSys.MachineA).Command = eSysCommand.ContinueLastRun
                Else
                    '[Note]:正常流程
                    gSYS(eSys.MachineA).Command = eSysCommand.AutoRun
                End If
                gSYS(eSys.MachineA).ConveyorNo = mConveyorNo1
                sys.SysNum = 3100

            Case 3100
                '[Note]:確認是否會有雙流道生產
                If gSSystemParameter.ConveyorModel = eConveyorModel.eConveyorNo1No2 Then
                    '[Note]:流道B Load
                    If IsChuckVacuumReady(mConveyorNo2) = True Or gSSystemParameter.IsBypassConveyor = True Then
                        '[Note]:上面有料片不用再進料
                        mConveyorStatus(mConveyorNo2) = Premtek.eConveyorStatus.Station_None
                        sys.SysNum = 3300
                    Else
                        '[Note]:先給可入料之訊號
                        gSYS(mSysConveyorNo2).Command = eSysCommand.LoadA
                        mConveyorStatus(mConveyorNo2) = Premtek.eConveyorStatus.Station_B
                        sys.SysNum = 3200
                    End If
                Else
                    sys.SysNum = 3200
                End If

            Case 3200
                '[Note]:流道A AutoRun Finish
                If gSSystemParameter.IsContinueLastRun = True Then
                    If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.ContinueLastRun Then
                        sys.SysNum = 3300
                    End If
                Else
                    If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.AutoRun Then
                        sys.SysNum = 3300
                    End If
                End If

            Case 3300
                '[Note]:呼叫機台作點膠前動作(秤重含Purge)、流道A開始退料
                If gCRecipe.IsJustOneRun = False Then 'Soni / 2017.04.27 跑單次時, 退料不做Purge秤重
                    gSYS(eSys.MachineA).Command = eSysCommand.PrevDispense
                ElseIf gSSystemParameter.ConveyorModel = eConveyorModel.eConveyorNo1No2 Then 'Soni + 2017.04.27 JustOneRun時, 雙閥仍做
                    gSYS(eSys.MachineA).Command = eSysCommand.PrevDispense
                End If


                '[Note]:流道A Unload
                If gSSystemParameter.IsBypassConveyor = False Then
                    gSYS(mSysConveyorNo1).Command = eSysCommand.UnloadA
                    mConveyorStatus(mConveyorNo1) = Premtek.eConveyorStatus.Station_A
                Else
                    mConveyorStatus(mConveyorNo1) = Premtek.eConveyorStatus.Station_None
                End If
                sys.SysNum = 3400

            Case 3400
                '[Note]:確認是否會有雙流道生產
                If gSSystemParameter.ConveyorModel = eConveyorModel.eConveyorNo1No2 Then
                    '[Note]:流道B Load Finish
                    If mConveyorStatus(mConveyorNo2) = Premtek.eConveyorStatus.Station_B Then
                        If gSYS(mSysConveyorNo2).ExecuteCommand = eSysCommand.LoadA AndAlso gSYS(mSysConveyorNo2).RunStatus = enmRunStatus.Finish Then
                            '[Note]:從入料完成後開始計時
                            gPriorHeatTimer(mConveyorNo2).Restart()
                            sys.SysNum = 3500
                        End If
                    Else
                        '[Note]:從入料完成後開始計時
                        gPriorHeatTimer(mConveyorNo2).Restart()
                        sys.SysNum = 3500
                    End If
                Else
                    sys.SysNum = 3600
                End If

            Case 3500
                '[Note]:進料完成後，先檢查前一次的點膠前動作(秤重含Purge)跑完了沒，完成後才可以接續生產
                If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.PrevDispense Then
                    sys.SysNum = 4000
                End If

            Case 3600
                '[Note]:確認流道A Unload Finish
                If mConveyorStatus(mConveyorNo1) = Premtek.eConveyorStatus.Station_None Then
                    If gCRecipe.IsJustOneRun = True Or gCRecipe.IsRerunRun = True Then 'Soni / 2017.04.27 由原流程移植
                        '[Note]:整個流程跑一次就結束(這是暫時速解的方法，後續再補手動流程改這個問題)
                        sys.Act(eAct.AutoRun).RunStatus = enmRunStatus.Finish
                        sys.RunStatus = enmRunStatus.Finish
                        Exit Sub
                    Else
                        sys.SysNum = 2100
                    End If

                Else
                    If gSYS(mSysConveyorNo1).ExecuteCommand = eSysCommand.UnloadA AndAlso gSYS(mSysConveyorNo1).RunStatus = enmRunStatus.Finish Then
                        If gCRecipe.IsJustOneRun = True Or gCRecipe.IsRerunRun = True Then 'Soni / 2017.04.27 由原流程移植
                            '[Note]:整個流程跑一次就結束(這是暫時速解的方法，後續再補手動流程改這個問題)
                            sys.Act(eAct.AutoRun).RunStatus = enmRunStatus.Finish
                            sys.RunStatus = enmRunStatus.Finish
                            Exit Sub
                        Else
                            sys.SysNum = 2100
                        End If

                    End If
                End If

            Case 4000
                '[Note]:流道B AutoRun 
                If gSSystemParameter.IsContinueLastRun = True Then
                    '[Note]:接續流程
                    gSYS(eSys.MachineA).Command = eSysCommand.ContinueLastRun
                Else
                    '[Note]:正常流程
                    gSYS(eSys.MachineA).Command = eSysCommand.AutoRun
                End If
                gSYS(eSys.MachineA).ConveyorNo = mConveyorNo2
                sys.SysNum = 4100

            Case 4100
                '[Note]:確認流道A Unload Finish
                If mConveyorStatus(mConveyorNo1) = Premtek.eConveyorStatus.Station_None Then
                    sys.SysNum = 4200
                Else
                    If gSYS(mSysConveyorNo1).ExecuteCommand = eSysCommand.UnloadA AndAlso gSYS(mSysConveyorNo1).RunStatus = enmRunStatus.Finish Then
                        sys.SysNum = 4200
                    End If
                End If

            Case 4200
                '[Note]:流道A Load
                If IsChuckVacuumReady(mConveyorNo1) = True Or gSSystemParameter.IsBypassConveyor = True Then
                    '[Note]:上面有料片不用再進料
                    mConveyorStatus(mConveyorNo1) = Premtek.eConveyorStatus.Station_None
                    sys.SysNum = 4300
                Else
                    '[Note]:先給可入料之訊號
                    gSYS(mSysConveyorNo1).Command = eSysCommand.LoadA
                    mConveyorStatus(mConveyorNo1) = Premtek.eConveyorStatus.Station_A
                    sys.SysNum = 4300
                End If

            Case 4300
                '[Note]:流道B AutoRun Finish
                If gSSystemParameter.IsContinueLastRun = True Then
                    If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.ContinueLastRun Then
                        sys.SysNum = 4400
                    End If
                Else
                    If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.AutoRun Then
                        sys.SysNum = 4400
                    End If
                End If

            Case 4400
                If gCRecipe.IsJustOneRun = False Then
                    '[Note]:呼叫機台作點膠前動作(秤重含Purge)、流道A開始退料
                    gSYS(eSys.MachineA).Command = eSysCommand.PrevDispense
                End If
                '[Note]:流道B Unload
                If gSSystemParameter.IsBypassConveyor = False Then
                    gSYS(mSysConveyorNo2).Command = eSysCommand.UnloadA
                    mConveyorStatus(mConveyorNo2) = Premtek.eConveyorStatus.Station_A
                Else
                    mConveyorStatus(mConveyorNo2) = Premtek.eConveyorStatus.Station_None
                End If
                If gCRecipe.IsJustOneRun = False Then
                    sys.SysNum = 4500
                Else '跑單次流程結束
                    sys.SysNum = 4420
                End If

            Case 4420
                '[Note]:流道A Unload Finish
                If mConveyorStatus(mConveyorNo2) = Premtek.eConveyorStatus.Station_A Then
                    If gSYS(mConveyorNo2).ExecuteCommand = eSysCommand.UnloadA AndAlso gSYS(mSysConveyorNo1).RunStatus = enmRunStatus.Finish Then
                        sys.Act(eAct.AutoRun).RunStatus = enmRunStatus.Finish
                        sys.RunStatus = enmRunStatus.Finish
                        Exit Sub
                    End If
                Else
                    sys.Act(eAct.AutoRun).RunStatus = enmRunStatus.Finish
                    sys.RunStatus = enmRunStatus.Finish
                    Exit Sub
                End If


            Case 4500
                '[Note]:流道A Load Finish
                If mConveyorStatus(mConveyorNo1) = Premtek.eConveyorStatus.Station_A Then
                    If gSYS(mSysConveyorNo1).ExecuteCommand = eSysCommand.LoadA AndAlso gSYS(mSysConveyorNo1).RunStatus = enmRunStatus.Finish Then
                        '[Note]:從入料完成後開始計時
                        gPriorHeatTimer(mConveyorNo1).Restart()
                        sys.SysNum = 4600
                    End If
                Else
                    '[Note]:從入料完成後開始計時
                    gPriorHeatTimer(mConveyorNo1).Restart()
                    sys.SysNum = 4600
                End If

            Case 4600
                '[Note]:進料完成後，先檢查前一次的點膠前動作(秤重含Purge)跑完了沒，完成後才可以接續生產
                If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.PrevDispense Then
                    sys.SysNum = 5000
                End If

            Case 5000
                '[Note]:流道A AutoRun 
                If gSSystemParameter.IsContinueLastRun = True Then
                    '[Note]:接續流程
                    gSYS(eSys.MachineA).Command = eSysCommand.ContinueLastRun
                Else
                    '[Note]:正常流程
                    gSYS(eSys.MachineA).Command = eSysCommand.AutoRun
                End If
                gSYS(eSys.MachineA).ConveyorNo = mConveyorNo1
                sys.SysNum = 5100

            Case 5100
                '[Note]:確認流道B Unload Finish
                If mConveyorStatus(mConveyorNo2) = Premtek.eConveyorStatus.Station_None Then
                    sys.SysNum = 5200
                Else
                    If gSYS(mSysConveyorNo2).ExecuteCommand = eSysCommand.UnloadA AndAlso gSYS(mSysConveyorNo2).RunStatus = enmRunStatus.Finish Then
                        sys.SysNum = 5200
                    End If
                End If

            Case 5200
                '[Note]:流道B Load
                If IsChuckVacuumReady(eConveyor.ConveyorNo2) = True Or gSSystemParameter.IsBypassConveyor = True Then
                    '[Note]:上面有料片不用再進料
                    mConveyorStatus(mConveyorNo2) = Premtek.eConveyorStatus.Station_None
                    sys.SysNum = 5300
                Else
                    '[Note]:先給可入料之訊號
                    gSYS(mSysConveyorNo2).Command = eSysCommand.LoadA
                    mConveyorStatus(mConveyorNo2) = Premtek.eConveyorStatus.Station_A
                    sys.SysNum = 5300
                End If

            Case 5300
                '[Note]:流道A AutoRun Finish
                If gSSystemParameter.IsContinueLastRun = True Then
                    If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.ContinueLastRun Then
                        sys.SysNum = 5400
                    End If
                Else
                    If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.AutoRun Then
                        sys.SysNum = 5400
                    End If
                End If

            Case 5400
                '[Note]:呼叫機台作點膠前動作(秤重含Purge)、流道A開始退料
                gSYS(eSys.MachineA).Command = eSysCommand.PrevDispense

                '[Note]:流道A Unload
                If gSSystemParameter.IsBypassConveyor = False Then
                    gSYS(mSysConveyorNo1).Command = eSysCommand.UnloadA
                    mConveyorStatus(mConveyorNo1) = Premtek.eConveyorStatus.Station_A
                Else
                    mConveyorStatus(mConveyorNo1) = Premtek.eConveyorStatus.Station_None
                End If
                sys.SysNum = 5500

            Case 5500
                '[Note]:流道B Load Finish
                If mConveyorStatus(mConveyorNo2) = Premtek.eConveyorStatus.Station_B Then
                    If gSYS(mSysConveyorNo2).ExecuteCommand = eSysCommand.LoadA AndAlso gSYS(mSysConveyorNo2).RunStatus = enmRunStatus.Finish Then
                        '[Note]:從入料完成後開始計時
                        gPriorHeatTimer(mConveyorNo2).Restart()
                        sys.SysNum = 5600
                    End If
                Else
                    '[Note]:從入料完成後開始計時
                    gPriorHeatTimer(mConveyorNo2).Restart()
                    sys.SysNum = 5600
                End If

            Case 5600
                '[Note]:進料完成後，先檢查前一次的點膠前動作(秤重含Purge)跑完了沒，完成後才可以接續生產
                If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.PrevDispense Then
                    sys.SysNum = 4000
                End If

                'Case 6000
                '    '[Note]:完成-->從頭再來吧(無窮迴圈)
                '    If gSSystemParameter.IsContinueLastRun = True Then
                '        gSSystemParameter.IsContinueLastRun = False
                '        '[Note]:整個流程跑一次就結束
                '        sys.Act(eAct.AutoRun).RunStatus = enmRunStatus.Finish
                '        sys.RunStatus = enmRunStatus.Finish
                '        Exit Sub
                '    Else
                '        If gCRecipe.IsJustOneRun = True Then
                '            '[Note]:整個流程跑一次就結束(這是暫時速解的方法，後續再補手動流程改這個問題)
                '            sys.Act(eAct.AutoRun).RunStatus = enmRunStatus.Finish
                '            sys.RunStatus = enmRunStatus.Finish
                '            Exit Sub
                '        Else
                '            sys.SysNum = 1500
                '        End If
                '    End If

        End Select

    End Sub

    ''' <summary>[強制退料流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub Overall_AbnormalUnloadAction(ByRef sys As sSysParam)

        Static mIsBypassMachineA As Boolean
        Static mIsBypassMachineB As Boolean

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                '[Note]:先將狀態清除
                'gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None  'Soni - 2016.09.16 清除時顯示為需要復歸, 怪怪的
                gSYS(eSys.OverAll).Act(eAct.AutoRun).RunStatus = enmRunStatus.None
                gSYS(eSys.OverAll).Act(eAct.AbnormalUnload).RunStatus = enmRunStatus.None

                For mMachineNo = eSys.MachineA To gSSystemParameter.MachineMax 'Soni / 2016.09.16 引數錯誤 造成Overall自身狀態被清除
                    'For mMachineNo = enmMachineStation.MachineA To gSSystemParameter.MachineMax
                    gSYS(mMachineNo).RunStatus = enmRunStatus.None
                Next

                Call GetMachineBypassState(mIsBypassMachineA, mIsBypassMachineB)
                sys.SysNum = 2000

            Case 2000
                '[Note]:強制退料
                If mIsBypassMachineB = False Then
                    If mIsBypassMachineA = False Then
                        '[Note]:AB機強制退料
                        gSYS(eSys.MachineA).Command = eSysCommand.AbnormalUnload
                        gSYS(eSys.MachineB).Command = eSysCommand.AbnormalUnload
                        sys.SysNum = 2100
                    End If
                Else
                    If mIsBypassMachineA = False Then
                        '[Note]:A機強制退料
                        gSYS(eSys.MachineA).Command = eSysCommand.AbnormalUnload
                        sys.SysNum = 2100
                    Else
                        '[Note]:都Bypass就完成了
                        sys.SysNum = 9000
                    End If
                End If

            Case 2100
                '[Note]:確認強制退料完成
                If mIsBypassMachineB = False Then
                    If mIsBypassMachineA = False Then
                        '[Note]:AB機強制退料
                        If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.AbnormalUnload Then
                            If gSYS(eSys.MachineB).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineB).ExecuteCommand = eSysCommand.AbnormalUnload Then
                                sys.SysNum = 3000
                            End If
                        End If
                    End If
                Else
                    If mIsBypassMachineA = False Then
                        '[Note]:A機強制退料
                        If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Finish And gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.AbnormalUnload Then
                            sys.SysNum = 3000
                        End If
                    Else
                        '[Note]:都Bypass就完成了
                        sys.SysNum = 9000
                    End If
                End If

            Case 3000
                '[Note]:B機退料(TODO:先檢查上面有無產品，有才能退)
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        'WetcoConveyor.mGlobalPool.Conveyor.Motion(WetcoConveyor.clsDTSConveyor.enmMotion.B_Unload)
                        gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadB 'Soni / 2016.09.14 新舊版本更換
                        sys.SysNum = 3100
                    Case Else
                        If mIsBypassMachineA = False Then
                            sys.SysNum = 4000
                        Else
                            '[Note]:跳過A機檢查
                            sys.SysNum = 9000
                        End If
                End Select

            Case 3100
                '[Note]:B機退料完成
                If gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish And gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.UnloadB Then 'Soni / 2016.09.14 新舊版本更換
                    If mIsBypassMachineA = False Then
                        sys.SysNum = 4000
                    Else
                        '[Note]:跳過A機檢查
                        sys.SysNum = 9000
                    End If
                End If

            Case 4000
                '[Note]:A機退料(TODO:先檢查上面有無產品，有才能退)
                gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadA 'Soni / 2016.09.14 新舊版本更換
                'WetcoConveyor.mGlobalPool.Conveyor.Motion(WetcoConveyor.clsDTSConveyor.enmMotion.A_Unload)
                sys.SysNum = 4100

            Case 4100
                '[Note]:A機退料完成
                If gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish And gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.UnloadA Then 'Soni / 2016.09.14 新舊版本更換
                    sys.SysNum = 9000
                End If

            Case 9000
                '[Note]:完成整機強制退片
                sys.Act(eAct.AbnormalUnload).RunStatus = enmRunStatus.Finish
                sys.RunStatus = enmRunStatus.Finish

        End Select
    End Sub
#End Region

#Region "Machine 機台控制層(LevelNo2端:控制 DispStage)"
    ''' <summary>[機台復歸流程(LevelNo2)]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub Machine_Home(ByVal sys As sSysParam)

        Static mDispStageNo1(enmMachineStation.MaxMachine) As Integer       '[第一組Stage]
        Static mDispStageNo2(enmMachineStation.MaxMachine) As Integer       '[第二組Stage]
        Dim mStageNo(enmMachineStation.MaxMachine) As Integer

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                'gMachineIsManual = False
                Select Case sys.MachineNo
                    Case enmMachineStation.MachineA
                        '[Note]:先做配接-->根據機種，決定需要哪幾組Stage復歸
                        Select Case gSSystemParameter.MachineType
                            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                                mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                                mDispStageNo2(sys.MachineNo) = eSys.DispStage2

                            Case Else
                                mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                                mDispStageNo2(sys.MachineNo) = eSys.DispStage1

                        End Select

                    Case enmMachineStation.MachineB
                        mDispStageNo1(sys.MachineNo) = eSys.DispStage3
                        mDispStageNo2(sys.MachineNo) = eSys.DispStage4

                End Select

                '[Note]:先清除狀態
                sys.Act(eAct.Home).RunStatus = enmRunStatus.None
                sys.Act(eAct.AutoRun).RunStatus = enmRunStatus.None
                For mStageNo(sys.MachineNo) = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                    gSYS(mStageNo(sys.MachineNo)).RunStatus = enmRunStatus.None
                Next

                sys.SysNum = 1200

                'Case 1100
                '    '[Note]:I/O Reset(TODO:一邊作業一邊復歸會掛)
                '    If gDOCollection.ReSetDO() Then
                '        sys.SysNum = 1200
                '    Else
                '        Select Case sys.MachineNo
                '            Case enmMachineStation.MachineA
                '                gEqpMsg.AddHistoryAlarm("Alarm_2081002", "MachineA_Home", sys.SysNum, gMsgHandler.GetMessage(Alarm_2081002))

                '            Case enmMachineStation.MachineB
                '                gEqpMsg.AddHistoryAlarm("Alarm_2081002", "MachineB_Home", sys.SysNum, gMsgHandler.GetMessage(Alarm_2081002))

                '        End Select
                '        Return enmRunStatus.Alarm
                '    End If



            Case 1200
                '[Note]:復歸
                For mStageNo(sys.MachineNo) = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                    gSYS(mStageNo(sys.MachineNo)).Command = eSysCommand.Home
                Next
                sys.SysNum = 1300

            Case 1300
                '[Note]:判斷復歸完成
                If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.Home Then
                    If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.Home Then
                        sys.SysNum = 9000
                    ElseIf gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Alarm Then 'Home Error
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                ElseIf gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Alarm Then 'Home Error
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                End If

            Case 9000
                '[Note]:完成復歸
                sys.Act(eAct.Home).RunStatus = enmRunStatus.Finish
                sys.RunStatus = enmRunStatus.Finish
                Exit Sub

        End Select

    End Sub

    ''' <summary>[機台端生產流程(LevelNo2)]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub Machine_AutoRun(ByRef sys As sSysParam)
        Static mDispStageNo1(enmMachineStation.MaxMachine) As Integer
        Static mDispStageNo2(enmMachineStation.MaxMachine) As Integer

        Static mDispValveNo1(enmStage.Max) As Boolean
        Static mDispValveNo2(enmStage.Max) As Boolean
        Static mbUseStageNo(enmStage.Max) As Boolean
        Try
            Select Case sys.SysNum
                Case sSysParam.SysLoopStart
                    'gMachineIsManual = False
                    Select Case sys.MachineNo
                        Case enmMachineStation.MachineA
                            '[Note]:先做配接-->根據機種，決定需要哪幾組Stage復歸
                            Select Case gSSystemParameter.MachineType
                                Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                                    mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                                    mDispStageNo2(sys.MachineNo) = eSys.DispStage2

                                Case Else
                                    mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                                    mDispStageNo2(sys.MachineNo) = eSys.DispStage1

                            End Select

                        Case enmMachineStation.MachineB
                            mDispStageNo1(sys.MachineNo) = eSys.DispStage3
                            mDispStageNo2(sys.MachineNo) = eSys.DispStage4

                    End Select
                    ''jimmy add 20170424
                    'For index = enmStage.No1 To gSSystemParameter.StageCount - 1
                    '    If gCRecipe.Node(index).Count > 0 Then
                    '        mbUseStageNo(index) = True
                    '    Else
                    '        mbUseStageNo(index) = False
                    '    End If
                    'Next

                    'Select Case sys.MachineNo
                    '    Case enmMachineStation.MachineA
                    '        '[Note]:先做配接-->根據機種，決定需要哪幾組Stage復歸
                    '        Select Case mbUseStageNo(0)
                    '            Case True
                    '                Select Case mbUseStageNo(1)
                    '                    Case True
                    '                        mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                    '                        mDispStageNo2(sys.MachineNo) = eSys.DispStage2
                    '                    Case False
                    '                        mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                    '                        mDispStageNo2(sys.MachineNo) = eSys.DispStage1
                    '                End Select
                    '            Case False
                    '                Select Case mbUseStageNo(1)
                    '                    Case True
                    '                        mDispStageNo1(sys.MachineNo) = eSys.DispStage2
                    '                        mDispStageNo2(sys.MachineNo) = eSys.DispStage2
                    '                    Case False
                    '                        '[Note]:不正常喔，不正常就配置第一組
                    '                        mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                    '                        mDispStageNo2(sys.MachineNo) = eSys.DispStage1
                    '                End Select
                    '        End Select
                    '    Case enmMachineStation.MachineB
                    '        Select Case mbUseStageNo(2)
                    '            Case True
                    '                Select Case mbUseStageNo(3)
                    '                    Case True
                    '                        mDispStageNo1(sys.MachineNo) = eSys.DispStage3
                    '                        mDispStageNo2(sys.MachineNo) = eSys.DispStage4
                    '                    Case False
                    '                        mDispStageNo1(sys.MachineNo) = eSys.DispStage3
                    '                        mDispStageNo2(sys.MachineNo) = eSys.DispStage3
                    '                End Select
                    '            Case False
                    '                Select Case mbUseStageNo(3)
                    '                    Case True
                    '                        mDispStageNo1(sys.MachineNo) = eSys.DispStage4
                    '                        mDispStageNo2(sys.MachineNo) = eSys.DispStage4

                    '                    Case False
                    '                        '[Note]:不正常喔，不正常就配置第一組
                    '                        mDispStageNo1(sys.MachineNo) = eSys.DispStage3
                    '                        mDispStageNo2(sys.MachineNo) = eSys.DispStage3
                    '                End Select
                    '        End Select
                    'End Select
                    sys.SysNum = 1500

                Case 1500
                    '[Note]:沒有MapData
                    '[Note]:ReDim
                    Select Case sys.MachineNo
                        Case enmMachineStation.MachineA
                            If gSSystemParameter.StageCount = 1 Then
                                gCRecipe.Initial_StageMap(enmStage.No1, gSSystemParameter.IsBypassCCD, sys.ConveyorNo)
                            ElseIf gSSystemParameter.StageCount >= 2 Then
                                gCRecipe.Initial_StageMap(enmStage.No1, gSSystemParameter.IsBypassCCD, sys.ConveyorNo)
                                gCRecipe.Initial_StageMap(enmStage.No2, gSSystemParameter.IsBypassCCD, sys.ConveyorNo)
                            End If

                        Case enmMachineStation.MachineB
                            gCRecipe.Initial_StageMap(enmStage.No3, gSSystemParameter.IsBypassCCD, sys.ConveyorNo)
                            gCRecipe.Initial_StageMap(enmStage.No4, gSSystemParameter.IsBypassCCD, sys.ConveyorNo)
                    End Select

                    '[Note]:Map Data處理
                    If gSSystemParameter.IsCompareWithMapData <> 0 Then

                        Dim path As String = ""
                        Dim casNo As Integer
                        If (sys.MachineNo = enmMachineStation.MachineA) Then
                            If (gAutoMapPath) Then
                                casNo = (cls800AQ_LUL.A_ProductNum Mod 100) - 1
                                If (200 > cls800AQ_LUL.A_ProductNum > 99) Then
                                    If casNo >= 0 And casNo < gCaseteAMapDataList.Count Then
                                        path = gCaseteAMapDataList(casNo)
                                    Else
                                        sys.RunStatus = enmRunStatus.Alarm
                                        gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "自動帶入的產品編號錯誤, 請重新確認.", eMessageLevel.Warning)
                                        Exit Sub
                                    End If

                                Else
                                    If casNo >= 0 And casNo < gCaseteAMapDataList.Count Then
                                        path = gCaseteBMapDataList(casNo)
                                    Else
                                        sys.RunStatus = enmRunStatus.Alarm
                                        gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "自動帶入的產品編號錯誤, 請重新確認.", eMessageLevel.Warning)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                path = gMapDataPathA
                            End If

                        ElseIf (sys.MachineNo = enmMachineStation.MachineB) Then
                            If (gAutoMapPath) Then '自動帶Map
                                casNo = (cls800AQ_LUL.B_ProductNum Mod 100) - 1
                                If (200 > cls800AQ_LUL.B_ProductNum > 99) Then
                                    If casNo >= 0 And casNo < gCaseteAMapDataList.Count Then
                                        path = gCaseteAMapDataList(casNo)
                                    Else
                                        sys.RunStatus = enmRunStatus.Alarm
                                        gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "自動帶入的產品編號錯誤, 請重新確認.", eMessageLevel.Warning)
                                        Exit Sub
                                    End If

                                Else
                                    If casNo >= 0 And casNo < gCaseteAMapDataList.Count Then
                                        path = gCaseteBMapDataList(casNo)
                                    Else
                                        sys.RunStatus = enmRunStatus.Alarm
                                        gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "自動帶入的產品編號錯誤, 請重新確認.", eMessageLevel.Warning)
                                        Exit Sub
                                    End If
                                End If
                                If path = "" Then
                                    sys.RunStatus = enmRunStatus.Alarm
                                    gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "自動帶入的路徑空白, 請重新確認.", eMessageLevel.Warning)
                                    Exit Sub
                                End If
                            Else '手動帶Map
                                path = gMapDataPathB
                            End If
                        End If

                        Dim returnValue As MapErrorCode = CoverMapData(sys.MachineNo, path)
                        Select Case returnValue
                            Case MapErrorCode.Success
                                sys.SysNum = 1900 'Toby_Modify_20170103
                            Case MapErrorCode.PathEmpty '路徑設定空值
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "路徑空白, 請重新選取路徑.", eMessageLevel.Warning)
                                Exit Sub
                            Case MapErrorCode.FileNotExists '檔案不存在
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "MAP檔案不存在", eMessageLevel.Warning)
                                Exit Sub
                            Case MapErrorCode.ArraySizeNotEqual
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "Map陣列大小與Recipe選擇節點大小不符", eMessageLevel.Warning)
                                Exit Sub
                            Case MapErrorCode.FileAnalyzeError
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "Map檔解析失敗, 請確認選取的檔案正確, 該格式受支援", eMessageLevel.Warning)
                                Exit Sub
                            Case MapErrorCode.InfoNotEqual
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "Recipe資訊與Map資訊比對不符, 請確認選擇的Map與Recipe對應相符", eMessageLevel.Warning)
                                Exit Sub
                            Case MapErrorCode.NodeFillMapLost
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "Map資料填入時發現Recipe設定遺漏, 請確認Recipe內的節點Map設定正確", eMessageLevel.Warning)
                                Exit Sub
                            Case MapErrorCode.UnknownException
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "不明例外, 請洽原廠工程師處理", eMessageLevel.Warning)
                                Exit Sub
                        End Select

                    Else
                        sys.SysNum = 1900
                    End If

                Case 1900 ' 手動設定Map 
                    'Toby add for test_Start
                    If gSSystemParameter.IsManualMap = False Then
                        sys.SysNum = 2000
                    Else
                        '20170612_Toby
                        gMapInfo = New CMapInfo()


                        Select Case gSSystemParameter.MachineType
                            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                                Select Case sys.MachineNo
                                    Case enmMachineStation.MachineA
                                        If gStageMap(0).Node.Count > 0 Or gStageMap(1).Node.Count > 0 Then '節點Pattern有數量
                                            '判斷Recipe是否有stage Node資料
                                            'Machine A
                                            Dim ChangeMap As frmModifyDie = New frmModifyDie(gMapInfo.gDrewMapPos_L)
                                            ChangeMap.Text = "Machine A"
                                            ChangeMap.ShowDialog()
                                        End If
                                        sys.SysNum = 2000
                                    Case enmMachineStation.MachineB
                                        If gStageMap(2).Node.Count > 0 Or gStageMap(3).Node.Count > 0 Then '節點Pattern有數量
                                            Dim ChangeMap As frmModifyDie = New frmModifyDie(gMapInfo.gDrewMapPos_R)
                                            ChangeMap.Text = "Machine B"
                                            ChangeMap.ShowDialog()
                                        End If
                                        sys.SysNum = 2000
                                End Select

                            Case Else
                                If gStageMap(0).Node.Count > 0 Then '節點Pattern有數量
                                    Dim ChangeMap As frmModifyDie = New frmModifyDie(gMapInfo.gDrewMapPos_L)
                                    ChangeMap.Text = "Map"
                                    ChangeMap.ShowDialog()
                                    sys.SysNum = 2000
                                End If
                        End Select
                    End If
                    'Toby add for test_end

                Case 2000
                    '[Note]:ProductLoading流程
                    For mStageMax = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        gSYS(mStageMax).Command = eSysCommand.ProductLoading
                    Next
                    gSyslog.Save("ProductLoading Start", "0000", eMessageLevel.Information)
                    sys.SysNum = 2100

                Case 2100
                    '[Note]:判斷ProductLoading完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.ProductLoading Then
                        If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.ProductLoading Then
                            gSyslog.Save("ProductLoading End", "0000", eMessageLevel.Information)
                            sys.SysNum = 2200
                        End If
                    End If

                Case 2200
                    '[Note]:防撞保護再次開啟(Scan)
                    Select Case sys.MachineNo
                        Case enmMachineStation.MachineA
                            Select Case gSSystemParameter.MachineType
                                Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD

                                    '如果只有左側作業時
                                    If mbUseStageNo(0) = True And mbUseStageNo(1) = False Then
                                        gIsLSideWorking(sys.MachineNo) = True
                                    Else
                                        gIsLSideWorking(sys.MachineNo) = False
                                    End If


                                Case Else
                                    gIsLSideWorking(sys.MachineNo) = True

                            End Select

                        Case enmMachineStation.MachineB

                            '如果只有左側作業時
                            If mbUseStageNo(2) = True And mbUseStageNo(3) = False Then
                                gIsLSideWorking(sys.MachineNo) = True
                            Else
                                gIsLSideWorking(sys.MachineNo) = False
                            End If


                    End Select
                    gIsRSideNeedGoToSafePos(sys.MachineNo) = False
                    gIsLSideNeedGoToSafePos(sys.MachineNo) = False
                    sys.SysNum = 3000

                Case 3000
                    '[Note]:Scan流程
                    For mStageMax = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        gSYS(mStageMax).Command = eSysCommand.CCDFix
                    Next
                    gSyslog.Save("Fids Start", "0000", eMessageLevel.Information)
                    sys.SysNum = 3100

                Case 3100
                    '[Note]:判斷Scan完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.CCDFix Then
                        If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.CCDFix Then
                            gSyslog.Save("Fids End", "0000", eMessageLevel.Information)
                            sys.SysNum = 3200
                        End If
                    End If

                Case 3200
                    '[Note]:防撞保護再次開啟(Laser)
                    Select Case sys.MachineNo
                        Case enmMachineStation.MachineA
                            Select Case gSSystemParameter.MachineType
                                Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD

                                    '如果只有左側作業時
                                    If mbUseStageNo(0) = True And mbUseStageNo(1) = False Then
                                        gIsLSideWorking(sys.MachineNo) = True
                                    Else
                                        gIsLSideWorking(sys.MachineNo) = False
                                    End If


                                Case Else
                                    gIsLSideWorking(sys.MachineNo) = True

                            End Select

                        Case enmMachineStation.MachineB

                            '如果只有左側作業時
                            If mbUseStageNo(2) = True And mbUseStageNo(3) = False Then
                                gIsLSideWorking(sys.MachineNo) = True
                            Else
                                gIsLSideWorking(sys.MachineNo) = False
                            End If

                    End Select
                    gIsRSideNeedGoToSafePos(sys.MachineNo) = False
                    gIsLSideNeedGoToSafePos(sys.MachineNo) = False
                    sys.SysNum = 4000

                Case 4000
                    '[Note]:Laser Reader測高流程
                    For mStageMax = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        gSYS(mStageMax).Command = eSysCommand.LaserReader
                    Next
                    gSyslog.Save("FindHeight Start", "0000", eMessageLevel.Information)
                    sys.SysNum = 4100

                Case 4100
                    '[Note]:判斷Laser Reader完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.LaserReader Then
                        If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.LaserReader Then
                            gSyslog.Save("FindHeight End", "0000", eMessageLevel.Information)
                            sys.SysNum = 4200
                        End If
                    End If

                Case 4200
                    '[Note]:防撞保護再次開啟(Laser)
                    Select Case sys.MachineNo
                        Case enmMachineStation.MachineA
                            Select Case gSSystemParameter.MachineType
                                Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD

                                    '如果只有左側作業時
                                    If mbUseStageNo(0) = True And mbUseStageNo(1) = False Then
                                        gIsLSideWorking(sys.MachineNo) = True
                                    Else
                                        gIsLSideWorking(sys.MachineNo) = False
                                    End If


                                Case Else
                                    gIsLSideWorking(sys.MachineNo) = True

                            End Select

                        Case enmMachineStation.MachineB

                            '如果只有左側作業時
                            If mbUseStageNo(2) = True And mbUseStageNo(3) = False Then
                                gIsLSideWorking(sys.MachineNo) = True
                            Else
                                gIsLSideWorking(sys.MachineNo) = False
                            End If

                    End Select
                    gIsRSideNeedGoToSafePos(sys.MachineNo) = False
                    gIsLSideNeedGoToSafePos(sys.MachineNo) = False
                    sys.SysNum = 4300

                Case 4300
                    '[Note]:判斷有無使用第二組閥
                    If gIsUseValveNo2(gSYS(mDispStageNo1(sys.MachineNo)).StageNo) = True Or gIsUseValveNo2(gSYS(mDispStageNo2(sys.MachineNo)).StageNo) = True Then
                        If gIsUseValveNo2(gSYS(mDispStageNo1(sys.MachineNo)).StageNo) = True Then
                            gSYS(mDispStageNo1(sys.MachineNo)).SelectValve = eValveWorkMode.Valve2
                        End If
                        If gIsUseValveNo2(gSYS(mDispStageNo2(sys.MachineNo)).StageNo) = True Then
                            gSYS(mDispStageNo2(sys.MachineNo)).SelectValve = eValveWorkMode.Valve2
                        End If
                        sys.SysNum = 5000
                    Else
                        sys.SysNum = 5300
                    End If

                Case 5000
                    '[Note]:Purge流程
                    '       開啟Pre-Dispense Purge
                    For mDispStage As Integer = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        If gPurgeDB.ContainsKey(gCRecipe.StageParts(sys.StageNo).PurgeName(gSYS(mDispStage).SelectValve)) = True Then
                            If gPurgeDB(gCRecipe.StageParts(sys.StageNo).PurgeName(gSYS(mDispStage).SelectValve)).IsPreDispenePurge = True Then
                                gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gPurgeDB(gCRecipe.StageParts(sys.StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns
                                gSYS(mDispStage).Command = eSysCommand.Purge
                                '[Note]:協助記錄中途是否做過Purge
                                Select Case sys.MachineNo
                                    Case enmMachineStation.MachineA
                                        If mDispStage = eSys.DispStage1 Then
                                            gIsOnPurge(enmStage.No1) = True
                                        End If
                                        If mDispStage = eSys.DispStage2 Then
                                            gIsOnPurge(enmStage.No2) = True
                                        End If
                                    Case enmMachineStation.MachineB
                                        If mDispStage = eSys.DispStage1 Then
                                            gIsOnPurge(enmStage.No3) = True
                                        End If
                                        If mDispStage = eSys.DispStage2 Then
                                            gIsOnPurge(enmStage.No4) = True
                                        End If
                                End Select
                            End If
                        End If
                    Next
                    sys.SysNum = 5100

                Case 5100
                    '[Note]:判斷Purge完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge Then
                        If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish Then
                            If gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge Then
                                If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish Then
                                    sys.SysNum = 5200
                                End If
                            Else
                                sys.SysNum = 5200
                            End If
                        End If
                    Else
                        If gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge Then
                            If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish Then
                                sys.SysNum = 5200
                            End If
                        Else
                            sys.SysNum = 5200
                        End If
                    End If

                Case 5200
                    '[Note]:判斷有無使用第一組閥
                    If gIsUseValveNo1(gSYS(mDispStageNo1(sys.MachineNo)).StageNo) = True Or gIsUseValveNo1(gSYS(mDispStageNo2(sys.MachineNo)).StageNo) = True Then
                        If gIsUseValveNo1(gSYS(mDispStageNo1(sys.MachineNo)).StageNo) = True Then
                            gSYS(mDispStageNo1(sys.MachineNo)).SelectValve = eValveWorkMode.Valve1
                        End If
                        If gIsUseValveNo1(gSYS(mDispStageNo2(sys.MachineNo)).StageNo) = True Then
                            gSYS(mDispStageNo2(sys.MachineNo)).SelectValve = eValveWorkMode.Valve1
                        End If
                        sys.SysNum = 5300
                    Else
                        sys.SysNum = 5500
                    End If

                Case 5300
                    '[Note]:Purge流程
                    '       開啟Pre-Dispense Purge
                    For mDispStage As Integer = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        If gPurgeDB.ContainsKey(gCRecipe.StageParts(sys.StageNo).PurgeName(gSYS(mDispStage).SelectValve)) = True Then
                            If gPurgeDB(gCRecipe.StageParts(sys.StageNo).PurgeName(gSYS(mDispStage).SelectValve)).IsPreDispenePurge = True Then
                                gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gPurgeDB(gCRecipe.StageParts(sys.StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns
                                gSYS(mDispStage).Command = eSysCommand.Purge
                                '[Note]:協助記錄中途是否做過Purge
                                Select Case sys.MachineNo
                                    Case enmMachineStation.MachineA
                                        If mDispStage = eSys.DispStage1 Then
                                            gIsOnPurge(enmStage.No1) = True
                                        End If
                                        If mDispStage = eSys.DispStage2 Then
                                            gIsOnPurge(enmStage.No2) = True
                                        End If
                                    Case enmMachineStation.MachineB
                                        If mDispStage = eSys.DispStage1 Then
                                            gIsOnPurge(enmStage.No3) = True
                                        End If
                                        If mDispStage = eSys.DispStage2 Then
                                            gIsOnPurge(enmStage.No4) = True
                                        End If
                                End Select
                            End If
                        End If
                    Next
                    sys.SysNum = 5400

                Case 5400
                    '[Note]:判斷Purge完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge Then
                        If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish Then
                            If gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge Then
                                If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish Then
                                    sys.SysNum = 5500
                                End If
                            Else
                                sys.SysNum = 5500
                            End If
                        End If
                    Else
                        If gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge Then
                            If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish Then
                                sys.SysNum = 5500
                            End If
                        Else
                            sys.SysNum = 5500
                        End If
                    End If

                Case 5500
                    '[Note]:等待料片預熱時間，時間到了才可以進入點膠流程   
                    If gTempDB.ContainsKey(gCRecipe.TempName) = True Then
                        If IsWaitPriorHeat(sys.MachineNo, sys.ConveyorNo, gPriorHeatTimer, gTempDB(gCRecipe.TempName).PriorHeatTime) = True Then
                            sys.SysNum = 6000
                        End If
                    Else
                        'Sue0504
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1018003", "Machine_AutoRun", , gMsgHandler.GetMessage(Error_1018003), eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1018003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1018103", "Machine_AutoRun", , gMsgHandler.GetMessage(Error_1018103), eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1018103), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1018203", "Machine_AutoRun", , gMsgHandler.GetMessage(Error_1018203), eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1018203), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1018303", "Machine_AutoRun", , gMsgHandler.GetMessage(Error_1018303), eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1018303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If

                Case 6000
                    '[Note]:Dispensing流程
                    For mStageMax = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        gSYS(mStageMax).Command = eSysCommand.Dispensing
                    Next
                    gSyslog.Save("Dispensing Start", "0000", eMessageLevel.Information)
                    sys.SysNum = 6100

                Case 6100
                    '[Note]:判斷Dispensing完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.Dispensing Then
                        If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.Dispensing Then
                            gSyslog.Save("Dispensing End", "0000", eMessageLevel.Information)
                            sys.SysNum = 9000
                        End If
                    End If

                Case 9000
                    '[Note]:完成生產
                    sys.RunStatus = enmRunStatus.Finish
                    Exit Sub
            End Select


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            gSyslog.Save(ex.Message & " " & ex.StackTrace, "", eMessageLevel.Error)
            sys.RunStatus = enmRunStatus.Alarm
            Exit Sub
        End Try
    End Sub

    ''' <summary>[機台接續生產流程(LevelNo2)]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub Machine_ContinueLastRun(ByVal sys As sSysParam)

        Static mDispStageNo1(enmMachineStation.MaxMachine) As Integer
        Static mDispStageNo2(enmMachineStation.MaxMachine) As Integer

        Static mbUseStageNo(enmStage.Max) As Boolean
        Try
            Select Case sys.SysNum
                Case sSysParam.SysLoopStart
                    'gMachineIsManual = False
                    'Select Case sys.MachineNo
                    '    Case enmMachineStation.MachineA
                    '        '[Note]:先做配接-->根據機種，決定需要哪幾組Stage復歸
                    '        Select Case gSSystemParameter.MachineType
                    '            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V
                    '                mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                    '                mDispStageNo2(sys.MachineNo) = eSys.DispStage2

                    '            Case Else
                    '                mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                    '                mDispStageNo2(sys.MachineNo) = eSys.DispStage1

                    '        End Select

                    '    Case enmMachineStation.MachineB
                    '        mDispStageNo1(sys.MachineNo) = eSys.DispStage3
                    '        mDispStageNo2(sys.MachineNo) = eSys.DispStage4

                    'End Select
                    'jimmy add 20170424
                    For index = enmStage.No1 To gSSystemParameter.StageCount - 1
                        If gCRecipe.Node(index).Count > 0 Then
                            mbUseStageNo(index) = True
                        Else
                            mbUseStageNo(index) = False
                        End If
                    Next

                    Select Case sys.MachineNo
                        Case enmMachineStation.MachineA
                            '[Note]:先做配接-->根據機種，決定需要哪幾組Stage復歸
                            Select Case mbUseStageNo(0)
                                Case True
                                    Select Case mbUseStageNo(1)
                                        Case True
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage2

                                        Case False
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage1

                                    End Select

                                Case False
                                    Select Case mbUseStageNo(1)
                                        Case True
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage2
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage2

                                        Case False
                                            '[Note]:不正常喔，不正常就配置第一組
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage1
                                    End Select

                            End Select

                        Case enmMachineStation.MachineB
                            Select Case mbUseStageNo(2)
                                Case True
                                    Select Case mbUseStageNo(3)
                                        Case True
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage3
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage4

                                        Case False
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage3
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage3

                                    End Select

                                Case False
                                    Select Case mbUseStageNo(3)
                                        Case True
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage4
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage4

                                        Case False
                                            '[Note]:不正常喔，不正常就配置第一組
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage3
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage3
                                    End Select

                            End Select

                    End Select
                    sys.SysNum = 1500

                    'Case 1400
                    '    '[Note]:載入先前作業儲存的資料
                    '    Call LoadDieStatusForMappingData("GG")
                    '    sys.SysNum = 1500

                Case 1500
                    '[Note]:Map Data處理
                    If gSSystemParameter.IsCompareWithMapData <> 0 Then
                        Dim path As String = ""
                        If (sys.MachineNo = enmMachineStation.MachineA) Then
                            If (gAutoMapPath) Then
                                If (200 > cls800AQ_LUL.A_ProductNum > 99) Then
                                    path = gCaseteAMapDataList((cls800AQ_LUL.A_ProductNum Mod 100) - 1)
                                Else
                                    path = gCaseteBMapDataList((cls800AQ_LUL.A_ProductNum Mod 100) - 1)
                                End If
                            Else
                                path = gMapDataPathA
                            End If

                        ElseIf (sys.MachineNo = enmMachineStation.MachineB) Then
                            If (gAutoMapPath) Then
                                If (200 > cls800AQ_LUL.B_ProductNum > 99) Then
                                    path = gCaseteAMapDataList((cls800AQ_LUL.B_ProductNum Mod 100) - 1)
                                Else
                                    path = gCaseteBMapDataList((cls800AQ_LUL.B_ProductNum Mod 100) - 1)
                                End If
                            Else
                                path = gMapDataPathB
                            End If
                        End If

                        Dim returnValue As MapErrorCode = CoverMapData(sys.MachineNo, path)
                        Select Case returnValue
                            Case MapErrorCode.Success
                                sys.SysNum = 2000 'Toby_Modify_20170103
                            Case MapErrorCode.PathEmpty '路徑設定空值
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "路徑空白, 請重新選取路徑.", eMessageLevel.Warning)
                                Exit Sub
                            Case MapErrorCode.FileNotExists '檔案不存在
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "MAP檔案不存在", eMessageLevel.Warning)
                                Exit Sub
                            Case MapErrorCode.ArraySizeNotEqual
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "Map陣列大小與Recipe選擇節點大小不符", eMessageLevel.Warning)
                                Exit Sub
                            Case MapErrorCode.FileAnalyzeError
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "Map檔解析失敗, 請確認選取的檔案正確, 該格式受支援", eMessageLevel.Warning)
                                Exit Sub
                            Case MapErrorCode.InfoNotEqual
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "Recipe資訊與Map資訊比對不符, 請確認選擇的Map與Recipe對應相符", eMessageLevel.Warning)
                                Exit Sub
                            Case MapErrorCode.NodeFillMapLost
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "Map資料填入時發現Recipe設定遺漏, 請確認Recipe內的節點Map設定正確", eMessageLevel.Warning)
                                Exit Sub
                            Case MapErrorCode.UnknownException
                                sys.RunStatus = enmRunStatus.Alarm
                                gEqpMsg.AddHistoryAlarm("0000", "Machine_AutoRun", sys.SysNum, "不明例外, 請洽原廠工程師處理", eMessageLevel.Warning)
                                Exit Sub
                        End Select

                        'If CoverMapData(sys.MachineNo, path) = MapErrorCode.Success Then
                        '    sys.SysNum = 2000
                        'Else
                        '    'TODO:Map檔案處理異常，然後呢?
                        '    sys.RunStatus = enmRunStatus.Alarm
                        '    gEqpMsg.AddHistoryAlarm("Error_1025002", "Machine_AutoRun", sys.SysNum, gMsgHandler.GetMessage(Error_1025002), eMessageLevel.Warning)
                        '    Exit Sub
                        'End If
                    Else
                        '[Note]:沒有MapData
                        '[Note]:ReDim
                        Select Case sys.MachineNo
                            Case enmMachineStation.MachineA
                                If gSSystemParameter.StageCount = 1 Then
                                    gCRecipe.Initial_StageMap(enmStage.No1, gSSystemParameter.IsBypassCCD, sys.ConveyorNo)
                                ElseIf gSSystemParameter.StageCount >= 2 Then
                                    gCRecipe.Initial_StageMap(enmStage.No1, gSSystemParameter.IsBypassCCD, sys.ConveyorNo)
                                    gCRecipe.Initial_StageMap(enmStage.No2, gSSystemParameter.IsBypassCCD, sys.ConveyorNo)
                                End If

                            Case enmMachineStation.MachineB
                                gCRecipe.Initial_StageMap(enmStage.No3, gSSystemParameter.IsBypassCCD, sys.ConveyorNo)
                                gCRecipe.Initial_StageMap(enmStage.No4, gSSystemParameter.IsBypassCCD, sys.ConveyorNo)
                        End Select

                        sys.SysNum = 1600
                    End If
                Case 1600
                    '[Note]:載入先前作業儲存的資料
                    Call LoadDieStatusForMappingData("GG")
                    sys.SysNum = 2000
                Case 2000
                    '[Note]:ProductLoading流程
                    For mStageMax = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        gSYS(mStageMax).Command = eSysCommand.ProductLoading
                    Next
                    sys.SysNum = 2100

                Case 2100
                    '[Note]:判斷ProductLoading完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.ProductLoading Then
                        If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.ProductLoading Then
                            sys.SysNum = 2200
                        End If
                    End If

                Case 2200
                    '[Note]:防撞保護再次開啟(Scan)
                    Select Case sys.MachineNo
                        Case enmMachineStation.MachineA
                            Select Case gSSystemParameter.MachineType
                                Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                                    gIsLSideWorking(sys.MachineNo) = False

                                Case Else
                                    gIsLSideWorking(sys.MachineNo) = True

                            End Select

                        Case enmMachineStation.MachineB
                            gIsLSideWorking(sys.MachineNo) = False

                    End Select
                    gIsRSideNeedGoToSafePos(sys.MachineNo) = False
                    gIsLSideNeedGoToSafePos(sys.MachineNo) = False
                    sys.SysNum = 3000

                Case 3000
                    '[Note]:Scan流程
                    For mStageMax = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        gSYS(mStageMax).Command = eSysCommand.CCDFix
                    Next
                    sys.SysNum = 3100

                Case 3100
                    '[Note]:判斷Scan完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.CCDFix Then
                        If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.CCDFix Then
                            sys.SysNum = 3200
                        End If
                    End If

                Case 3200
                    '[Note]:防撞保護再次開啟(Laser)
                    Select Case sys.MachineNo
                        Case enmMachineStation.MachineA
                            Select Case gSSystemParameter.MachineType
                                Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                                    gIsLSideWorking(sys.MachineNo) = False

                                Case Else
                                    gIsLSideWorking(sys.MachineNo) = True

                            End Select

                        Case enmMachineStation.MachineB
                            gIsLSideWorking(sys.MachineNo) = False

                    End Select
                    gIsRSideNeedGoToSafePos(sys.MachineNo) = False
                    gIsLSideNeedGoToSafePos(sys.MachineNo) = False
                    sys.SysNum = 4000

                Case 4000
                    '[Note]:Laser Reader測高流程
                    For mStageMax = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        gSYS(mStageMax).Command = eSysCommand.LaserReader
                    Next
                    sys.SysNum = 4100

                Case 4100
                    '[Note]:判斷Laser Reader完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.LaserReader Then
                        If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.LaserReader Then
                            sys.SysNum = 4200
                        End If
                    End If

                Case 4200
                    '[Note]:防撞保護再次開啟(Laser)
                    Select Case sys.MachineNo
                        Case enmMachineStation.MachineA
                            Select Case gSSystemParameter.MachineType
                                Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                                    gIsLSideWorking(sys.MachineNo) = False

                                Case Else
                                    gIsLSideWorking(sys.MachineNo) = True

                            End Select

                        Case enmMachineStation.MachineB
                            gIsLSideWorking(sys.MachineNo) = False

                    End Select
                    gIsRSideNeedGoToSafePos(sys.MachineNo) = False
                    gIsLSideNeedGoToSafePos(sys.MachineNo) = False
                    sys.SysNum = 4300

                Case 4300
                    '[Note]:等待料片預熱時間，時間到了才可以進入點膠流程    20161206
                    If gTempDB.ContainsKey(gCRecipe.TempName) = True Then
                        If gPriorHeatTimer(sys.MachineNo).ElapsedMilliseconds > gTempDB(gCRecipe.TempName).PriorHeatTime Then 'gSSystemParameter.StableTime.PriorHeatTime Then
                            gPriorHeatTimer(sys.MachineNo).Stop()
                            sys.SysNum = 5000
                        End If
                    Else
                        'Sue0504
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1018003", "Machine_AutoRun", , gMsgHandler.GetMessage(Error_1018003), eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1018003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1018103", "Machine_AutoRun", , gMsgHandler.GetMessage(Error_1018103), eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1018103), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1018203", "Machine_AutoRun", , gMsgHandler.GetMessage(Error_1018203), eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1018203), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1018303", "Machine_AutoRun", , gMsgHandler.GetMessage(Error_1018303), eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1018303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If


                Case 5000
                    '[Note]:Dispensing流程
                    For mStageMax = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        gSYS(mStageMax).Command = eSysCommand.Dispensing
                    Next
                    sys.SysNum = 5100

                Case 5100
                    '[Note]:判斷Dispensing完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.Dispensing Then
                        If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.Dispensing Then
                            sys.SysNum = 9000
                        End If
                    End If

                Case 9000
                    '[Note]:完成生產
                    ClearGGFile("GG")
                    sys.RunStatus = enmRunStatus.Finish
                    Exit Sub
            End Select


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            gSyslog.Save(ex.Message & " " & ex.StackTrace, "", eMessageLevel.Error)
            sys.RunStatus = enmRunStatus.Alarm
            Exit Sub
        End Try
    End Sub

    ''' <summary>[強制退料流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub Machine_AbnormalUnloadAction(ByRef sys As sSysParam)

        Static mDispStageNo1(enmMachineStation.MaxMachine) As Integer       '[第一組Stage]
        Static mDispStageNo2(enmMachineStation.MaxMachine) As Integer       '[第二組Stage]
        Dim mStageNo(enmMachineStation.MaxMachine) As Integer

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                'gMachineIsManual = False
                Select Case sys.MachineNo
                    Case enmMachineStation.MachineA
                        '[Note]:先做配接-->根據機種，決定需要哪幾組Stage復歸
                        Select Case gSSystemParameter.MachineType
                            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                                mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                                mDispStageNo2(sys.MachineNo) = eSys.DispStage2

                            Case Else
                                mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                                mDispStageNo2(sys.MachineNo) = eSys.DispStage1

                        End Select

                    Case enmMachineStation.MachineB
                        mDispStageNo1(sys.MachineNo) = eSys.DispStage3
                        mDispStageNo2(sys.MachineNo) = eSys.DispStage4

                End Select

                '[Note]:先清除狀態
                For mStageNo(sys.MachineNo) = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                    gSYS(mStageNo(sys.MachineNo)).RunStatus = enmRunStatus.None
                Next

                sys.SysNum = 1200
            Case 1200
                '[Note]:強制退料流程
                For mStageNo(sys.MachineNo) = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                    gSYS(mStageNo(sys.MachineNo)).Command = eSysCommand.AbnormalUnload
                Next
                sys.SysNum = 1300

            Case 1300
                '[Note]:判斷復歸完成
                If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.AbnormalUnload Then
                    If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.AbnormalUnload Then
                        sys.SysNum = 9000
                    End If
                End If

            Case 9000
                '[Note]:完成復歸
                sys.Act(eAct.AbnormalUnload).RunStatus = enmRunStatus.Finish
                sys.RunStatus = enmRunStatus.Finish

        End Select
    End Sub

    ''' <summary>[點膠前流程，ex:秤重、Purge(LevelNo2)]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub Machine_PrevDispense(ByVal sys As sSysParam)

        Static mDispStageNo1(enmMachineStation.MaxMachine) As Integer                       '[紀錄左側Stage對應的esys.DispStage]
        Static mDispStageNo2(enmMachineStation.MaxMachine) As Integer                       '[紀錄右側Stage對應的esys.DispStage]
        Static mIsPurgeDispStageNo1(enmMachineStation.MaxMachine) As Boolean                '[紀錄左側Stage是否需要做Purge]
        Static mIsPurgeDispStageNo2(enmMachineStation.MaxMachine) As Boolean                '[紀錄右側Stage是否需要做Purge]
        Static mIsNeedFlowRate(enmMachineStation.MaxMachine) As Boolean                     '[紀錄需不需要做秤重]
        Static mbUseStageNo(enmStage.Max) As Boolean

        Try
            Select Case sys.SysNum
                Case sSysParam.SysLoopStart
                    For index = enmStage.No1 To gSSystemParameter.StageCount - 1
                        If gCRecipe.Node(index).Count > 0 Then
                            mbUseStageNo(index) = True
                        Else
                            mbUseStageNo(index) = False
                        End If
                    Next

                    Select Case sys.MachineNo
                        Case enmMachineStation.MachineA
                            '[Note]:先做配接-->根據機種，決定需要哪幾組Stage復歸
                            Select Case mbUseStageNo(0)
                                Case True
                                    Select Case mbUseStageNo(1)
                                        Case True
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage2

                                        Case False
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage1

                                    End Select

                                Case False
                                    Select Case mbUseStageNo(1)
                                        Case True
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage2
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage2

                                        Case False
                                            '[Note]:不正常喔，不正常就配置第一組
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage1
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage1
                                    End Select

                            End Select

                        Case enmMachineStation.MachineB
                            Select Case mbUseStageNo(2)
                                Case True
                                    Select Case mbUseStageNo(3)
                                        Case True
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage3
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage4

                                        Case False
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage3
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage3

                                    End Select

                                Case False
                                    Select Case mbUseStageNo(3)
                                        Case True
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage4
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage4

                                        Case False
                                            '[Note]:不正常喔，不正常就配置第一組
                                            mDispStageNo1(sys.MachineNo) = eSys.DispStage3
                                            mDispStageNo2(sys.MachineNo) = eSys.DispStage3
                                    End Select

                            End Select

                    End Select
                    mIsNeedFlowRate(sys.MachineNo) = False
                    sys.SysNum = 2000

                Case 2000
                    '[Note]:判斷有無使用第二組閥
                    '[Note]:秤重條件&Pruge條件判斷
                    '       若要秤重，強制作Purge

                    '[Note]:雙閥同動點膠的條件下，秤重只看第一組的資料，故無需對第二支閥做秤重，但Purge需要
                    If gSSystemParameter.MultiDispenseEnable = True Then
                        sys.SysNum = 4000
                    Else
                        If gIsUseValveNo2(gSYS(mDispStageNo1(sys.MachineNo)).StageNo) = True Or gIsUseValveNo2(gSYS(mDispStageNo2(sys.MachineNo)).StageNo) = True Then
                            If gIsUseValveNo2(gSYS(mDispStageNo1(sys.MachineNo)).StageNo) = True Then
                                gSYS(mDispStageNo1(sys.MachineNo)).SelectValve = eValveWorkMode.Valve2
                            End If
                            If gIsUseValveNo2(gSYS(mDispStageNo2(sys.MachineNo)).StageNo) = True Then
                                gSYS(mDispStageNo2(sys.MachineNo)).SelectValve = eValveWorkMode.Valve2
                            End If
                            sys.SysNum = 2100
                        Else
                            sys.SysNum = 4000
                        End If
                    End If

                Case 2100
                    '[Note]:秤重條件&Pruge條件判斷
                    '       若要秤重，強制作Purge
                    For mDispStage As Integer = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        If gFlowRateDB.ContainsKey(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)) = True Then
                            Select Case gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).BaseOn
                                Case eInspectionType.OnTimerOrRuns
                                    If gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnTimer(gSYS(mDispStage).SelectValve) >=
                                        gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnTimer Then
                                        gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                        gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                        gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnRuns
                                        mIsNeedFlowRate(sys.MachineNo) = True
                                        Exit For
                                    Else
                                        If gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnRuns <> 0 Then
                                            '[Note]:進入時就做一次(次數的時候)
                                            If gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) <= 0 Then
                                                gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                                gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                                gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnRuns
                                                mIsNeedFlowRate(sys.MachineNo) = True
                                                Exit For
                                            End If
                                        End If
                                    End If

                                Case eInspectionType.OnTimer
                                    If gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnTimer(gSYS(mDispStage).SelectValve) >= gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnTimer Then
                                        gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                        gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                        gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnRuns
                                        mIsNeedFlowRate(sys.MachineNo) = True
                                        Exit For
                                    End If

                                Case eInspectionType.OnRuns
                                    If gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnRuns <> 0 Then
                                        '[Note]:進入時就做一次(次數的時候)
                                        If gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) <= 0 Then
                                            gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                            gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                            gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnRuns
                                            mIsNeedFlowRate(sys.MachineNo) = True
                                            Exit For
                                        End If
                                    End If

                                Case eInspectionType.Noen

                            End Select
                        End If
                    Next

                    If mIsNeedFlowRate(sys.MachineNo) = True Then
                        '[Note]:協助記錄中途是否有做過Purge的動作
                        Select Case sys.MachineNo
                            Case enmMachineStation.MachineA
                                gIsOnPurge(enmStage.No1) = True
                                gIsOnPurge(enmStage.No2) = True

                            Case enmMachineStation.MachineB
                                gIsOnPurge(enmStage.No3) = True
                                gIsOnPurge(enmStage.No4) = True

                        End Select
                        sys.SysNum = 2200
                    Else
                        sys.SysNum = 4000
                    End If

                Case 2200
                    '[Note]:Purge流程
                    For mDispStage As Integer = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        gSYS(mDispStage).Command = eSysCommand.Purge
                    Next
                    sys.SysNum = 2300

                Case 2300
                    '[Note]:判斷Purge完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge Then
                        If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge Then
                            sys.SysNum = 3000
                        End If
                    End If

                Case 3000
                    '[Note]:左側先秤重，完成後再換右側秤重
                    gSYS(mDispStageNo1(sys.MachineNo)).Command = eSysCommand.WeightUnit
                    sys.SysNum = 3200

                Case 3200
                    '[Note]:判斷秤重完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.WeightUnit Then
                        sys.SysNum = 3300
                    End If

                Case 3300
                    '[Note]:移至安全位置
                    gSYS(mDispStageNo1(sys.MachineNo)).Command = eSysCommand.Safe
                    sys.SysNum = 3400

                Case 3400
                    '[Note]:判斷秤重完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.Safe Then
                        sys.SysNum = 3500
                    End If

                Case 3500
                    '[Note]:左側先秤重，完成後再換右側秤重
                    If mDispStageNo1(sys.MachineNo) = mDispStageNo2(sys.MachineNo) Then
                        '[Note]:表示只有單邊作業，則只需秤一次即可。
                        sys.SysNum = 4000
                    Else
                        gSYS(mDispStageNo2(sys.MachineNo)).Command = eSysCommand.WeightUnit
                        sys.SysNum = 3600
                    End If

                Case 3600
                    '[Note]:判斷秤重完成
                    If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.WeightUnit Then
                        sys.SysNum = 3700
                    End If

                Case 3700
                    '[Note]:移至安全位置
                    gSYS(mDispStageNo2(sys.MachineNo)).Command = eSysCommand.Safe
                    sys.SysNum = 3800

                Case 3800
                    '[Note]:判斷秤重完成
                    If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.Safe Then
                        sys.SysNum = 4000
                    End If

                Case 4000
                    '[Note]:判斷有無使用第一組閥
                    '[Note]:秤重條件&Pruge條件判斷
                    '       若要秤重，強制作Purge
                    If gIsUseValveNo1(gSYS(mDispStageNo1(sys.MachineNo)).StageNo) = True Or gIsUseValveNo1(gSYS(mDispStageNo2(sys.MachineNo)).StageNo) = True Then
                        If gIsUseValveNo1(gSYS(mDispStageNo1(sys.MachineNo)).StageNo) = True Then
                            gSYS(mDispStageNo1(sys.MachineNo)).SelectValve = eValveWorkMode.Valve1
                        End If
                        If gIsUseValveNo1(gSYS(mDispStageNo2(sys.MachineNo)).StageNo) = True Then
                            gSYS(mDispStageNo2(sys.MachineNo)).SelectValve = eValveWorkMode.Valve1
                        End If
                        sys.SysNum = 4100
                    Else
                        sys.SysNum = 6000
                    End If

                Case 4100
                    '[Note]:秤重條件&Pruge條件判斷
                    '       若要秤重，強制作Purge
                    For mDispStage As Integer = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        If gFlowRateDB.ContainsKey(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)) = True Then
                            Select Case gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).BaseOn
                                Case eInspectionType.OnTimerOrRuns
                                    If gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnTimer(gSYS(mDispStage).SelectValve) >= gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnTimer Then
                                        gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                        gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                        gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnRuns
                                        mIsNeedFlowRate(sys.MachineNo) = True
                                        Exit For
                                    Else
                                        If gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnRuns <> 0 Then
                                            '[Note]:進入時就做一次(次數的時候)
                                            If gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) <= 0 Then
                                                gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                                gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                                gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnRuns
                                                mIsNeedFlowRate(sys.MachineNo) = True
                                                Exit For
                                            End If
                                        End If
                                    End If

                                Case eInspectionType.OnTimer
                                    If gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnTimer(gSYS(mDispStage).SelectValve) >= gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnTimer Then
                                        gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                        gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                        gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnRuns
                                        mIsNeedFlowRate(sys.MachineNo) = True
                                        Exit For
                                    End If

                                Case eInspectionType.OnRuns
                                    If gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnRuns <> 0 Then
                                        '[Note]:進入時就做一次(次數的時候)
                                        If gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) <= 0 Then
                                            gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                            gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                            gSSystemParameter.StageParts.FlowRate(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gFlowRateDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).FlowRateName(gSYS(mDispStage).SelectValve)).OnRuns
                                            mIsNeedFlowRate(sys.MachineNo) = True
                                            Exit For
                                        End If
                                    End If

                                Case eInspectionType.Noen

                            End Select
                        End If
                    Next

                    If mIsNeedFlowRate(sys.MachineNo) = True Then
                        '[Note]:協助記錄中途是否有做過Purge的動作
                        Select Case sys.MachineNo
                            Case enmMachineStation.MachineA
                                gIsOnPurge(enmStage.No1) = True
                                gIsOnPurge(enmStage.No2) = True

                            Case enmMachineStation.MachineB
                                gIsOnPurge(enmStage.No3) = True
                                gIsOnPurge(enmStage.No4) = True

                        End Select
                        sys.SysNum = 4200
                    Else
                        sys.SysNum = 6000
                    End If

                Case 4200
                    '[Note]:Purge流程
                    For mDispStage As Integer = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        gSYS(mDispStage).Command = eSysCommand.Purge
                    Next
                    sys.SysNum = 4300

                Case 4300
                    '[Note]:判斷Purge完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge Then
                        If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge Then
                            sys.SysNum = 5000
                        End If
                    End If

                Case 5000
                    '[Note]:左側先秤重，完成後再換右側秤重
                    gSYS(mDispStageNo1(sys.MachineNo)).Command = eSysCommand.WeightUnit
                    sys.SysNum = 5200

                Case 5200
                    '[Note]:判斷秤重完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.WeightUnit Then
                        sys.SysNum = 5300
                    End If

                Case 5300
                    '[Note]:移至安全位置
                    gSYS(mDispStageNo1(sys.MachineNo)).Command = eSysCommand.Safe
                    sys.SysNum = 5400

                Case 5400
                    '[Note]:判斷秤重完成
                    If gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.Safe Then
                        sys.SysNum = 5500
                    End If

                Case 5500
                    '[Note]:左側先秤重，完成後再換右側秤重
                    If mDispStageNo1(sys.MachineNo) = mDispStageNo2(sys.MachineNo) Then
                        '[Note]:表示只有單邊作業，則只需秤一次即可。
                        sys.SysNum = 6000
                    Else
                        gSYS(mDispStageNo2(sys.MachineNo)).Command = eSysCommand.WeightUnit
                        sys.SysNum = 5600
                    End If

                Case 5600
                    '[Note]:判斷秤重完成
                    If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.WeightUnit Then
                        sys.SysNum = 5700
                    End If

                Case 5700
                    '[移至安全位置]
                    gSYS(mDispStageNo2(sys.MachineNo)).Command = eSysCommand.Safe
                    sys.SysNum = 5800

                Case 5800
                    '[Note]:判斷秤重完成
                    If gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.Safe Then
                        sys.SysNum = 6000
                    End If

                Case 6000
                    '[Note]:判斷有無使用第二組閥
                    '[Note]:Purge流程 & 判斷是否需要做Purge
                    If gIsUseValveNo2(gSYS(mDispStageNo1(sys.MachineNo)).StageNo) = True Or gIsUseValveNo2(gSYS(mDispStageNo2(sys.MachineNo)).StageNo) = True Then
                        If gIsUseValveNo2(gSYS(mDispStageNo1(sys.MachineNo)).StageNo) = True Then
                            gSYS(mDispStageNo1(sys.MachineNo)).SelectValve = eValveWorkMode.Valve2
                        End If
                        If gIsUseValveNo2(gSYS(mDispStageNo2(sys.MachineNo)).StageNo) = True Then
                            gSYS(mDispStageNo2(sys.MachineNo)).SelectValve = eValveWorkMode.Valve2
                        End If
                        sys.SysNum = 6100
                    Else
                        sys.SysNum = 7000
                    End If

                Case 6100
                    '[Note]:Purge流程 & 判斷是否需要做Purge
                    mIsPurgeDispStageNo1(sys.MachineNo) = False
                    mIsPurgeDispStageNo2(sys.MachineNo) = False
                    For mDispStage As Integer = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        If gPurgeDB.ContainsKey(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)) = True Then
                            '[Note]:若點膠前需要Purge，則定位前就不做Purge的動作。
                            If gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).IsPreDispenePurge = False Then
                                Select Case gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).BaseOn
                                    Case eInspectionType.OnTimerOrRuns
                                        If gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnTimer(gSYS(mDispStage).SelectValve) >= gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnTimer Then
                                            gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                            gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                            gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns
                                            gSYS(mDispStage).Command = eSysCommand.Purge
                                            If mDispStage = mDispStageNo1(sys.MachineNo) Then
                                                mIsPurgeDispStageNo1(sys.MachineNo) = True
                                            ElseIf mDispStage = mDispStageNo2(sys.MachineNo) Then
                                                mIsPurgeDispStageNo2(sys.MachineNo) = True
                                            End If
                                        Else
                                            If gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns <> 0 Then
                                                '[Note]:進入時就做一次(次數的時候)
                                                If gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) <= 0 Then
                                                    gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                                    gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                                    gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns
                                                    gSYS(mDispStage).Command = eSysCommand.Purge
                                                    If mDispStage = mDispStageNo1(sys.MachineNo) Then
                                                        mIsPurgeDispStageNo1(sys.MachineNo) = True
                                                    ElseIf mDispStage = mDispStageNo2(sys.MachineNo) Then
                                                        mIsPurgeDispStageNo2(sys.MachineNo) = True
                                                    End If
                                                End If
                                            End If
                                        End If

                                    Case eInspectionType.OnTimer
                                        If gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnTimer(gSYS(mDispStage).SelectValve) >= gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnTimer Then
                                            gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                            gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                            gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns
                                            gSYS(mDispStage).Command = eSysCommand.Purge
                                            If mDispStage = mDispStageNo1(sys.MachineNo) Then
                                                mIsPurgeDispStageNo1(sys.MachineNo) = True
                                            ElseIf mDispStage = mDispStageNo2(sys.MachineNo) Then
                                                mIsPurgeDispStageNo2(sys.MachineNo) = True
                                            End If
                                        End If

                                    Case eInspectionType.OnRuns
                                        If gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns <> 0 Then
                                            '[Note]:進入時就做一次(次數的時候)
                                            If gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) <= 0 Then
                                                gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                                gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                                gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns
                                                gSYS(mDispStage).Command = eSysCommand.Purge
                                                If mDispStage = mDispStageNo1(sys.MachineNo) Then
                                                    mIsPurgeDispStageNo1(sys.MachineNo) = True
                                                ElseIf mDispStage = mDispStageNo2(sys.MachineNo) Then
                                                    mIsPurgeDispStageNo2(sys.MachineNo) = True
                                                End If
                                            End If
                                        End If

                                    Case eInspectionType.Noen

                                End Select
                            End If
                        End If
                    Next

                    '[Note]:協助記錄哪些是中途作Purge
                    Select Case sys.MachineNo
                        Case enmMachineStation.MachineA
                            If mIsPurgeDispStageNo1(sys.MachineNo) = True And mDispStageNo1(sys.MachineNo) = eSys.DispStage1 Then
                                gIsOnPurge(enmStage.No1) = True
                            End If
                            If mIsPurgeDispStageNo2(sys.MachineNo) = True And mDispStageNo1(sys.MachineNo) = eSys.DispStage2 Then
                                gIsOnPurge(enmStage.No2) = True
                            End If

                        Case enmMachineStation.MachineB
                            If mIsPurgeDispStageNo1(sys.MachineNo) = True And mDispStageNo1(sys.MachineNo) = eSys.DispStage3 Then
                                gIsOnPurge(enmStage.No3) = True
                            End If
                            If mIsPurgeDispStageNo2(sys.MachineNo) = True And mDispStageNo1(sys.MachineNo) = eSys.DispStage4 Then
                                gIsOnPurge(enmStage.No4) = True
                            End If
                    End Select

                    sys.SysNum = 6200

                Case 6200
                    '[Note]:判斷Purge完成
                    If (gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge) Or mIsPurgeDispStageNo1(sys.MachineNo) = False Then
                        If (gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge) Or mIsPurgeDispStageNo2(sys.MachineNo) = False Then
                            sys.SysNum = 7000
                        End If
                    End If

                Case 7000
                    '[Note]:判斷有無使用第一組閥
                    '[Note]:秤重條件&Pruge條件判斷
                    '       若要秤重，強制作Purge
                    If gIsUseValveNo1(gSYS(mDispStageNo1(sys.MachineNo)).StageNo) = True Or gIsUseValveNo1(gSYS(mDispStageNo2(sys.MachineNo)).StageNo) = True Then
                        If gIsUseValveNo1(gSYS(mDispStageNo1(sys.MachineNo)).StageNo) = True Then
                            gSYS(mDispStageNo1(sys.MachineNo)).SelectValve = eValveWorkMode.Valve1
                        End If
                        If gIsUseValveNo1(gSYS(mDispStageNo2(sys.MachineNo)).StageNo) = True Then
                            gSYS(mDispStageNo2(sys.MachineNo)).SelectValve = eValveWorkMode.Valve1
                        End If
                        sys.SysNum = 7100
                    Else
                        sys.SysNum = 9000
                    End If

                Case 7100
                    '[Note]:Purge流程 & 判斷是否需要做Purge
                    mIsPurgeDispStageNo1(sys.MachineNo) = False
                    mIsPurgeDispStageNo2(sys.MachineNo) = False
                    For mDispStage As Integer = mDispStageNo1(sys.MachineNo) To mDispStageNo2(sys.MachineNo)
                        If gPurgeDB.ContainsKey(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)) = True Then
                            '[Note]:若點膠前需要Purge，則定位前就不做Purge的動作。
                            If gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).IsPreDispenePurge = False Then
                                Select Case gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).BaseOn
                                    Case eInspectionType.OnTimerOrRuns
                                        If gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnTimer(gSYS(mDispStage).SelectValve) >= gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnTimer Then
                                            gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                            gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                            gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns
                                            gSYS(mDispStage).Command = eSysCommand.Purge
                                            If mDispStage = mDispStageNo1(sys.MachineNo) Then
                                                mIsPurgeDispStageNo1(sys.MachineNo) = True
                                            ElseIf mDispStage = mDispStageNo2(sys.MachineNo) Then
                                                mIsPurgeDispStageNo2(sys.MachineNo) = True
                                            End If
                                        Else
                                            If gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns <> 0 Then
                                                '[Note]:進入時就做一次(次數的時候)
                                                If gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) <= 0 Then
                                                    gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                                    gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                                    gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns
                                                    gSYS(mDispStage).Command = eSysCommand.Purge
                                                    If mDispStage = mDispStageNo1(sys.MachineNo) Then
                                                        mIsPurgeDispStageNo1(sys.MachineNo) = True
                                                    ElseIf mDispStage = mDispStageNo2(sys.MachineNo) Then
                                                        mIsPurgeDispStageNo2(sys.MachineNo) = True
                                                    End If
                                                End If
                                            End If
                                        End If

                                    Case eInspectionType.OnTimer
                                        If gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnTimer(gSYS(mDispStage).SelectValve) >= gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnTimer Then
                                            gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                            gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                            gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns
                                            gSYS(mDispStage).Command = eSysCommand.Purge
                                            If mDispStage = mDispStageNo1(sys.MachineNo) Then
                                                mIsPurgeDispStageNo1(sys.MachineNo) = True
                                            ElseIf mDispStage = mDispStageNo2(sys.MachineNo) Then
                                                mIsPurgeDispStageNo2(sys.MachineNo) = True
                                            End If
                                        End If

                                    Case eInspectionType.OnRuns
                                        If gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns <> 0 Then
                                            '[Note]:進入時就做一次(次數的時候)
                                            If gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) <= 0 Then
                                                gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).LastTime(gSYS(mDispStage).SelectValve) = 0
                                                gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).StartTime(gSYS(mDispStage).SelectValve) = Now
                                                gSSystemParameter.StageParts.Purge(gSYS(mDispStage).StageNo).OnRuns(gSYS(mDispStage).SelectValve) = gPurgeDB(gCRecipe.StageParts(gSYS(mDispStage).StageNo).PurgeName(gSYS(mDispStage).SelectValve)).OnRuns
                                                gSYS(mDispStage).Command = eSysCommand.Purge
                                                If mDispStage = mDispStageNo1(sys.MachineNo) Then
                                                    mIsPurgeDispStageNo1(sys.MachineNo) = True
                                                ElseIf mDispStage = mDispStageNo2(sys.MachineNo) Then
                                                    mIsPurgeDispStageNo2(sys.MachineNo) = True
                                                End If
                                            End If
                                        End If

                                    Case eInspectionType.Noen

                                End Select
                            End If
                        End If
                    Next

                    '[Note]:協助記錄哪些是中途作Purge
                    Select Case sys.MachineNo
                        Case enmMachineStation.MachineA
                            If mIsPurgeDispStageNo1(sys.MachineNo) = True And mDispStageNo1(sys.MachineNo) = eSys.DispStage1 Then
                                gIsOnPurge(enmStage.No1) = True
                            End If
                            If mIsPurgeDispStageNo2(sys.MachineNo) = True And mDispStageNo1(sys.MachineNo) = eSys.DispStage2 Then
                                gIsOnPurge(enmStage.No2) = True
                            End If

                        Case enmMachineStation.MachineB
                            If mIsPurgeDispStageNo1(sys.MachineNo) = True And mDispStageNo1(sys.MachineNo) = eSys.DispStage3 Then
                                gIsOnPurge(enmStage.No3) = True
                            End If
                            If mIsPurgeDispStageNo2(sys.MachineNo) = True And mDispStageNo1(sys.MachineNo) = eSys.DispStage4 Then
                                gIsOnPurge(enmStage.No4) = True
                            End If
                    End Select
                    sys.SysNum = 7200

                Case 7200
                    '[Note]:判斷Purge完成
                    If (gSYS(mDispStageNo1(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo1(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge) Or mIsPurgeDispStageNo1(sys.MachineNo) = False Then
                        If (gSYS(mDispStageNo2(sys.MachineNo)).RunStatus = enmRunStatus.Finish And gSYS(mDispStageNo2(sys.MachineNo)).ExecuteCommand = eSysCommand.Purge) Or mIsPurgeDispStageNo2(sys.MachineNo) = False Then
                            sys.SysNum = 9000
                        End If
                    End If

                Case 9000
                    '[Note]:完成
                    sys.RunStatus = enmRunStatus.Finish
                    Exit Sub

            End Select


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            gSyslog.Save(ex.Message & " " & ex.StackTrace, "", eMessageLevel.Error)
            sys.RunStatus = enmRunStatus.Alarm
            Exit Sub
        End Try
    End Sub

#End Region

#Region "Stage控制層(LevelNo3端:控制底層<Initial、定位、測高、點膠......>)"

    ''' <summary>[回Home動作流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub DispStage_HomeAction(ByRef sys As sSysParam)

        Static mHomeStopWatch(enmStage.Max) As Stopwatch
        Static mValveCmdFailCount(enmStage.Max) As Integer                                  '[紀錄資料傳輸異常次數]
        Static mValveRespose(enmStage.Max) As sPicoValveCommandResponseData                 '[Valve Controller之命令接收狀態]
        Static mServoStopWatch(enmStage.Max) As Stopwatch
        Static mValveCylStopWatch(enmStage.Max) As Stopwatch                                '[用來記錄B閥汽缸的TimeOut]
        Static mSubStageNo(enmStage.Max) As enmStage
        Dim mI(enmStage.Max) As Integer
        Dim mIsHomeTime(enmStage.Max) As Boolean

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                '[Note]:先清除狀態
                If IsNothing(mHomeStopWatch(sys.StageNo)) = True Then
                    mHomeStopWatch(sys.StageNo) = New Stopwatch
                End If
                If mValveCylStopWatch(sys.StageNo) Is Nothing Then
                    mValveCylStopWatch(sys.StageNo) = New Stopwatch
                End If
                If IsNothing(mServoStopWatch(sys.StageNo)) = True Then
                    mServoStopWatch(sys.StageNo) = New Stopwatch
                End If

                sys.Act(eAct.Home).RunStatus = enmRunStatus.None
                sys.Act(eAct.ProductLoading).RunStatus = enmRunStatus.None
                sys.Act(eAct.CCDSCanFix).RunStatus = enmRunStatus.None
                sys.Act(eAct.LaserReader).RunStatus = enmRunStatus.None
                sys.Act(eAct.Dispensing).RunStatus = enmRunStatus.None
                sys.Act(eAct.ChangeGlue).RunStatus = enmRunStatus.None
                sys.Act(eAct.ClearGlue).RunStatus = enmRunStatus.None
                sys.Act(eAct.Purge).RunStatus = enmRunStatus.None
                sys.Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.None
                sys.Act(eAct.CCDSCanGlue).RunStatus = enmRunStatus.None
                sys.Act(eAct.ProductUnload).RunStatus = enmRunStatus.None
                sys.Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.None
                sys.Act(eAct.Loading).RunStatus = enmRunStatus.None
                sys.Act(eAct.WeightUnit).RunStatus = enmRunStatus.None
                '20170929 _True:減速停止   False:緊急停止
                If gCMotion.SetSNELReact(sys.AxisX, False) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetSNELReact(sys.AxisY, False) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetSNELReact(sys.AxisZ, False) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                '0927 Toby 設定碰到正極限處置方式
                If gCMotion.SetSPELReact(sys.AxisX, False) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetSPELReact(sys.AxisY, False) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetSPELReact(sys.AxisZ, False) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If


                If gCMotion.SetSNEL(sys.AxisX, gCMotion.AxisParameter(sys.AxisX).Limit.NegativeLimit) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetSPEL(sys.AxisX, gCMotion.AxisParameter(sys.AxisX).Limit.PosivtiveLimit) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetSNEL(sys.AxisY, gCMotion.AxisParameter(sys.AxisY).Limit.NegativeLimit) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetSPEL(sys.AxisY, gCMotion.AxisParameter(sys.AxisY).Limit.PosivtiveLimit) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetSNEL(sys.AxisZ, gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetSPEL(sys.AxisZ, gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetNELEnable(sys.AxisX, False) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetPELEnable(sys.AxisX, False) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetNELEnable(sys.AxisY, False) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetPELEnable(sys.AxisY, False) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetNELEnable(sys.AxisZ, False) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetPELEnable(sys.AxisZ, False) <> CommandStatus.Sucessed Then
                    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                '[Note]:清除SubDisp命令
                Select Case sys.StageNo
                    Case enmStage.No1
                        mSubStageNo(sys.StageNo) = eSys.SubDisp1

                    Case enmStage.No2
                        mSubStageNo(sys.StageNo) = eSys.SubDisp2

                    Case enmStage.No3
                        mSubStageNo(sys.StageNo) = eSys.SubDisp3

                    Case enmStage.No4
                        mSubStageNo(sys.StageNo) = eSys.SubDisp4

                End Select

                gSYS(mSubStageNo(sys.StageNo)).RunStatus = enmRunStatus.None

                'gblnMotorUnlock = False

                '[Note]:復歸前先讓所有的動作停止
                Call gCMotion.EmgStop(sys.AxisX)
                Call gCMotion.EmgStop(sys.AxisY)
                Call gCMotion.EmgStop(sys.AxisZ)
                Call gCMotion.EmgStop(sys.AxisB)
                Call gCMotion.EmgStop(sys.AxisC)

                '[Note]:清除異常狀態
                Call gCMotion.AxisResetError(sys.AxisX)
                Call gCMotion.AxisResetError(sys.AxisY)
                Call gCMotion.AxisResetError(sys.AxisZ)
                Call gCMotion.AxisResetError(sys.AxisB)
                Call gCMotion.AxisResetError(sys.AxisC)

                '[Note]:設定跑S Curve
                Call gCMotion.SetCurve(sys.AxisA, eCurveMode.SCurve)
                Call gCMotion.SetCurve(sys.AxisY, eCurveMode.SCurve)
                Call gCMotion.SetCurve(sys.AxisZ, eCurveMode.SCurve)
                Call gCMotion.SetCurve(sys.AxisB, eCurveMode.SCurve)
                Call gCMotion.SetCurve(sys.AxisC, eCurveMode.SCurve)

                '[Note]:清除同動內Buffer的資料
                For mI(sys.StageNo) = 0 To gCMotion.SyncParameter.Count - 1
                    Call gCMotion.GpClearMovePath(gCMotion.SyncParameter(mI(sys.StageNo)))
                Next

                '[Note]:Dispenesing Trigger Off
                Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eOff)

                '[說明]:關閉出膠 & 給壓力
                '[Note]:膠管壓力開關，取決於閥體種類(IOReset時預設膠管給壓力)
                For mI(sys.StageNo) = 0 To gSSystemParameter.StageUseValveCount - 1
                    Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).ValveType(mI(sys.StageNo))
                        Case enmValveType.Jet
                            '[Note]:Jet Valve氣壓要給，其餘的在點膠的時候才給氣壓
                            Call gSysAdapter.SetSyringePressure(sys.StageNo, mI(sys.StageNo), enmONOFF.eON)

                            Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).JetValve(mI(sys.StageNo))
                                Case eValveModel.Advanjet
                                    Call gSysAdapter.SetValvePressure(sys.StageNo, mI(sys.StageNo), enmONOFF.eON)

                            End Select

                        Case Else
                            Call gSysAdapter.SetSyringePressure(sys.StageNo, mI(sys.StageNo), enmONOFF.eOff)
                            Call gSysAdapter.SetValvePressure(sys.StageNo, mI(sys.StageNo), enmONOFF.eOff)

                    End Select
                Next

                mValveCmdFailCount(sys.StageNo) = 0
                sys.SysNum = 2000

                '******************************************************************************************************************
                '****************************************  SysNum=2000~2999-->Valve參數設定  **************************************                                          
                '******************************************************************************************************************
            Case 2000
                '[Note]:Power On
                '       Check trigger board is ready before send command
                If gValvecontrollerCollection.IsBusy(sys.ValveControllerNo1) = False Then
                    If gValvecontrollerCollection.SetValvePower(sys.ValveControllerNo1, True) = enmCommandState.Success Then
                        sys.SysNum = 2100
                    End If
                End If

            Case 2100
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gValvecontrollerCollection.IsBusy(sys.ValveControllerNo1) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gValvecontrollerCollection.IsTimeOut(sys.ValveControllerNo1) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mValveCmdFailCount(sys.StageNo) = mValveCmdFailCount(sys.StageNo) + 1
                        If mValveCmdFailCount(sys.StageNo) > gVavleCmdMaxFailCounts Then
                            'TODO:異常訊息(Valve Controller Set Valve Power Fail)
                            gEqpMsg.AddHistoryAlarm("Alarm_2024001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2024001), eMessageLevel.Error)
                            sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 2000
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    mValveRespose(sys.ValveControllerNo1) = gValvecontrollerCollection.GetCommandResponseValvePower(sys.ValveControllerNo1)
                    If mValveRespose(sys.ValveControllerNo1).Status = True Then
                        mValveCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 3000
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("Power On: " & mValveRespose(sys.ValveControllerNo1).STR)
                        mValveCmdFailCount(sys.StageNo) = mValveCmdFailCount(sys.StageNo) + 1
                        If mValveCmdFailCount(sys.StageNo) > gVavleCmdMaxFailCounts Then
                            'TODO:異常訊息(Valve Controller Set Valve Power Fail)
                            gEqpMsg.AddHistoryAlarm("Alarm_2024000", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2024000), eMessageLevel.Error)
                            sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 2000
                        End If
                    End If
                End If

                '******************************************************************************************************************
                '**************************************  SysNum=3000~3999-->Z軸&汽缸復歸  *****************************************                                          
                '******************************************************************************************************************

            Case 3000
                '[說明]:設定回Home速度
                If gCMotion.SetHomeVelAccDec(sys.AxisZ) = False Then
                    'TODO:異常訊息(設定復歸速度異常)
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032017), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044017), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062017), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069017), eMessageLevel.Error)
                    End Select
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 3100

            Case 3100
                '[Note]:汽缸先回至上升位置，Z軸先回Home，完成後XY軸再回Home
                Call ValveCylinderAction(sys.StageNo, eValveWorkMode.Valve1, enmUpDown.Down, mValveCylStopWatch(sys.StageNo))
                If gCMotion.Home(sys.AxisZ) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032016))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044016))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062016))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069016))
                    End Select
                    'gEqpMsg.AddHistoryAlarm("Error_1032016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032016))
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                mHomeStopWatch(sys.StageNo).Restart()
                sys.SysNum = 3200

            Case 3200
                '[Note]:Check Cylinder Up Sensor
                If ValveCylinderSensor(sys.StageNo, eValveWorkMode.Valve1, enmUpDown.Down) = True Then
                    mValveCylStopWatch(sys.StageNo).Stop()
                    sys.SysNum = 3300
                Else
                    If IsTimeOut(mValveCylStopWatch(sys.StageNo), gSSystemParameter.TimeOut2) = True Then
                        mHomeStopWatch(sys.StageNo).Stop()
                        gEqpMsg.AddHistoryAlarm("Alarm_2004000", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2004000))   '[Cylinder Up Down Sensor Alarm]
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                End If

            Case 3300
                '[Note]:檢查Z軸回HOME完成 or至ORG，若到了則XY就可以動了，若逾時(30Sec)則報警
                If gCMotion.HomeFinish(sys.AxisZ) = CommandStatus.Sucessed Or gCMotion.AxisParameter(sys.AxisZ).MotionIOStatus.blnORG = True Then
                    mHomeStopWatch(sys.StageNo).Stop()
                    '是否有Tilt
                    If sys.AxisB <> -1 Then
                        sys.SysNum = 3500
                    Else
                        sys.SysNum = 4000
                    End If
                Else
                    If IsTimeOut(mHomeStopWatch(sys.StageNo), gSSystemParameter.TimeOut5) = True Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032001))
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044001))
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062001))
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069001))
                        End Select
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    End If
                End If

            Case 3500
                '[Note]:設定Tilt回Home速度
                If gCMotion.SetHomeVelAccDec(sys.AxisB, gCMotion.AxisParameter(sys.AxisB).HomeParameter.IsHomeDouble) = False Then
                    'TODO:異常訊息(設定復歸速度異常)
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1034017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034017), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1046017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046017), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1064017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064017), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1071017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071017), eMessageLevel.Error)
                    End Select
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                Else
                    sys.SysNum = 3600
                End If

            Case 3600
                '[Note]:Tilt軸先回Home(Tilt Home位置不是在0度)，完成後XY軸再回Home
                If gCMotion.Home(sys.AxisB) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1034016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034016))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1046016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046016))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1064016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064016))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1071016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071016))
                    End Select
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 4000


                '******************************************************************************************************************
                '**************************************  SysNum=4000~4999-->XY軸復歸  ********************************************                                          
                '******************************************************************************************************************
            Case 4000
                '[說明]:設定回Home速度
                If gCMotion.SetHomeVelAccDec(sys.AxisX, gCMotion.AxisParameter(sys.AxisX).HomeParameter.IsHomeDouble) = False Then
                    'TODO:異常訊息(設定復歸速度異常)
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030017), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042017), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060017), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067017), eMessageLevel.Error)
                    End Select
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetHomeVelAccDec(sys.AxisY) = False Then
                    'TODO:異常訊息(設定復歸速度異常)
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031017), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043017), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061017), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068017), eMessageLevel.Error)
                    End Select
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 4100

            Case 4100
                '[Note]:復歸
                If gCMotion.Home(sys.AxisX, gCMotion.AxisParameter(sys.AxisX).HomeParameter.IsHomeDouble) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030016))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042016))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060016))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067016))
                    End Select
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.Home(sys.AxisY) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031016))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043016))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061016))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068016))
                    End Select
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                mHomeStopWatch(sys.StageNo).Restart()
                If gCMotion.AxisParameter(sys.AxisX).HomeParameter.IsHomeDouble = True Then
                    sys.SysNum = 4200
                Else
                    sys.SysNum = 6000
                End If

            Case 4200
                '[Note]:確認X軸復歸完成
                If gCMotion.HomeFinish(sys.AxisX) = CommandStatus.Sucessed Then
                    sys.SysNum = 4300
                Else
                    If IsTimeOut(mHomeStopWatch(sys.StageNo), gSSystemParameter.TimeOutHome) = True Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1030001", "ClearGlueAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030001))
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1042001", "ClearGlueAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042001))
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1060001", "ClearGlueAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060001))
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1067001", "ClearGlueAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067001))
                        End Select
                        sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                        sys.RunStatus = enmRunStatus.Alarm
                    End If
                End If

            Case 4300
                '[Note]:Servo Off
                Call gCMotion.Servo(sys.AxisX, enmONOFF.eOff)
                mServoStopWatch(sys.StageNo).Restart()
                sys.SysNum = 4400

            Case 4400
                If IsTimeOut(mServoStopWatch(sys.StageNo), 500) = True Then
                    sys.SysNum = 4500
                End If

            Case 4500
                '[Note]:Servo On
                Call gCMotion.Servo(sys.AxisX, enmONOFF.eON)
                mServoStopWatch(sys.StageNo).Restart()
                sys.SysNum = 4600

            Case 4600
                If IsTimeOut(mServoStopWatch(sys.StageNo), 200) = True Then
                    sys.SysNum = 4700
                End If

            Case 4700
                '[說明]:設定回Home速度(第二次復歸，使用原始參數即可)
                If gCMotion.SetHomeVelAccDec(sys.AxisX) = False Then
                    'TODO:異常訊息(設定復歸速度異常)
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030017), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042017), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060017), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067017", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067017), eMessageLevel.Error)
                    End Select
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 4800

            Case 4800
                '[Note]:復歸
                If gCMotion.Home(sys.AxisX) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030016))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042016))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060016))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067016", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067016))
                    End Select
                    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                Else
                    sys.SysNum = 6000
                End If

            Case 6000
                '[Note]:確認X、Y、Z軸復歸完成
                If gCMotion.HomeFinish(sys.AxisX) = CommandStatus.Sucessed AndAlso gCMotion.HomeFinish(sys.AxisY) = CommandStatus.Sucessed AndAlso gCMotion.HomeFinish(sys.AxisZ) = CommandStatus.Sucessed Then
                    sys.SysNum = 6100
                Else
                    If IsTimeOut(mHomeStopWatch(sys.StageNo), gSSystemParameter.TimeOutHome) = True Then
                        mIsHomeTime(sys.StageNo) = False
                        If gCMotion.HomeFinish(sys.AxisX) <> CommandStatus.Sucessed Then
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1030001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030001))
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1042001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042001))
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1060001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060001))
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1067001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067001))
                            End Select
                            mIsHomeTime(sys.StageNo) = True
                        End If
                        If gCMotion.HomeFinish(sys.AxisY) <> CommandStatus.Sucessed Then
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1031001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031001))
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1043001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043001))
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1061001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061001))
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1068001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068001))
                            End Select
                            mIsHomeTime(sys.StageNo) = True
                        End If
                        If gCMotion.HomeFinish(sys.AxisZ) <> CommandStatus.Sucessed Then
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032001))
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044001))
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062001))
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069001))
                            End Select
                            mIsHomeTime(sys.StageNo) = True
                        End If
                        If mIsHomeTime(sys.StageNo) = True Then
                            sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                    End If
                End If

            Case 6100
                '[Note]:確認B軸復歸完成
                If sys.AxisB <> -1 Then
                    If gCMotion.HomeFinish(sys.AxisB) = CommandStatus.Sucessed Then
                        mHomeStopWatch(sys.StageNo).Stop()
                        sys.SysNum = 9000
                    Else
                        If IsTimeOut(mHomeStopWatch(sys.StageNo), gSSystemParameter.TimeOutHome) = True Then
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1032001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032001))
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1044001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044001))
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1062001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062001))
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1069001", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069001))
                            End Select
                            sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                    End If
                Else
                    sys.SysNum = 9000
                End If

            Case 9000
                '若有軸卡補償，這時候設定為enable
                If gSSystemParameter.StageFixMode = 2 Then
                    gCMotion.Dev2DCompensateTableEnable(enmAxis.XAxis, enmAxis.Y1Axis, True)
                Else
                    gCMotion.Dev2DCompensateTableEnable(enmAxis.XAxis, enmAxis.Y1Axis, False)
                End If
                '20171102 將軟體極限都設成false(不改成true)
                'If gCMotion.SetNELEnable(sys.AxisX, True) <> CommandStatus.Sucessed Then
                '    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                '    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If
                'If gCMotion.SetPELEnable(sys.AxisX, True) <> CommandStatus.Sucessed Then
                '    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                '    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If
                'If gCMotion.SetNELEnable(sys.AxisY, True) <> CommandStatus.Sucessed Then
                '    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                '    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If
                'If gCMotion.SetPELEnable(sys.AxisY, True) <> CommandStatus.Sucessed Then
                '    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                '    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If
                'If gCMotion.SetNELEnable(sys.AxisZ, True) <> CommandStatus.Sucessed Then
                '    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                '    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If
                'If gCMotion.SetPELEnable(sys.AxisZ, True) <> CommandStatus.Sucessed Then
                '    gEqpMsg.AddHistoryAlarm("Error_1000002", "DispStage_HomeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1000002), eMessageLevel.Error)
                '    sys.Act(eAct.Home).RunStatus = enmRunStatus.Alarm
                '    sys.RunStatus = enmRunStatus.Alarm
                '    Exit Sub
                'End If

                '[Note]:紀錄各軸的位置(更新)
                gProtectData(sys.StageNo).TargetPos.PosX = 0
                gProtectData(sys.StageNo).TargetPos.PosY = 0
                gProtectData(sys.StageNo).NowPos.PosX = 0
                gProtectData(sys.StageNo).NowPos.PosY = 0
                sys.Act(eAct.Home).RunStatus = enmRunStatus.Finish
                sys.RunStatus = enmRunStatus.Finish
                Exit Sub

        End Select

    End Sub

    ''' <summary>[進料初始化、Map、狀態檢查流程]</summary>
    ''' <param name="sys"></param>
    ''' <remarks></remarks>
    Sub DispStage_ProductLoadingAction(ByRef sys As sSysParam)

        Static mEPVStopWatch(enmStage.Max) As Stopwatch
        Static mEPVCmdFailCount(enmStage.Max) As Integer                                    '[紀錄資料傳輸異常次數(EPV)]
        Static mTriggerCmdFailCount(enmStage.Max) As Integer                                '[紀錄資料傳輸異常次數(Trigger Board)]
        Static mHeadNo(enmStage.Max) As eValveWorkMode
        Static mTemperature(enmStage.Max) As Decimal
        Static mNozzleNo(enmStage.Max) As enmTemp                                           '[取加熱時對應的NozzleNo]
        Static mSyringPressure(enmStage.Max, eValveWorkMode.MaxValve) As Decimal
        Static mValvePressure(enmStage.Max, eValveWorkMode.MaxValve) As Decimal
        Dim mAxisXState(enmStage.Max) As CommandStatus                                      '[X軸的狀態]
        Dim mAxisYState(enmStage.Max) As CommandStatus                                      '[Y軸的狀態]
        Dim mAxisZState(enmStage.Max) As CommandStatus                                      '[Z軸的狀態]
        Dim mI(enmStage.Max) As Integer

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                '[Note]:ReDim
                mEPVCmdFailCount(sys.StageNo) = 0
                mTriggerCmdFailCount(sys.StageNo) = 0
                If IsNothing(mEPVStopWatch(sys.StageNo)) = True Then
                    mEPVStopWatch(sys.StageNo) = New Stopwatch
                End If
                If gStageMap(sys.StageNo).Node.Count = 0 Then
                    sys.SysNum = 9000
                Else
                    sys.SysNum = 1100
                End If

            Case 1100
                '[Note]:移動Z軸至上升高度
                If gCMotion.SetVelAccDec(sys.AxisZ) = False Then 'Soni + 2016.09.04 移動前給定速度 避免速度緩慢
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                    End Select
                    'gEqpMsg.AddHistoryAlarm("Error_1031000", "DispStage_CCDScanFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 1200

            Case 1200
                If CDec(gCMotion.GetPositionValue(sys.AxisZ)) > gSSystemParameter.Pos.SafePosZ Then
                    sys.SysNum = 1300
                End If

            Case 1300
                '[Note]:移至安全區待命
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        If gCMotion.SetVelAccDec(sys.AxisX) = False Then 'Soni + 2016.09.04 移動前給定速度 避免速度緩慢
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If

                        If gCMotion.SetVelAccDec(sys.AxisY) = False Then 'Soni + 2016.09.04 移動前給定速度 避免速度緩慢
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If

                        gProtectData(sys.StageNo).TargetPos.PosX = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosX(sys.SelectValve)
                        gProtectData(sys.StageNo).TargetPos.PosY = gSSystemParameter.Pos.SafeRegion(sys.StageNo).PosY(sys.SelectValve)
                        If gCMotion.AbsMove(sys.AxisY, gProtectData(sys.StageNo).TargetPos.PosY) <> CommandStatus.Sucessed Then
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1031000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1043000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1061000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1068000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1031000", "DispStage_CCDScanFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                        If gCMotion.AbsMove(sys.AxisX, gProtectData(sys.StageNo).TargetPos.PosX) <> CommandStatus.Sucessed Then
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1030000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1042000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1060000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1067000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                            End Select
                            'gEqpMsg.AddHistoryAlarm("Error_1030000", "DispStage_CCDScanFixAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        End If
                        mEPVStopWatch(sys.StageNo).Restart()
                        sys.SysNum = 1500

                    Case Else
                        mEPVStopWatch(sys.StageNo).Restart()
                        sys.SysNum = 1500

                End Select



                '*********************************************************************************************
                '***************************Case 1500~1699:更新膠管壓力資訊****************************
                '*********************************************************************************************
            Case 1500
                '[Note]:ValveNo1膠管壓力設定-->ValveNo1閥體壓力設定-->ValveNo2膠管壓力設定-->ValveN2閥體壓力設定
                '[Note]:設定膠管壓力
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Syringe) = False Then
                    If gEPVCollection.SetValue(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Syringe, gCRecipe.StageParts(sys.StageNo).SyringePressure(eValveWorkMode.Valve1), False) = True Then
                        'Debug.Print("StageNo EPVNo: " & sys.StageNo & " , " & sys.EPVNo)
                        sys.SysNum = 1520
                    End If
                End If

            Case 1520
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Syringe) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gEPVCollection.IsTimeOut(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Syringe) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1500
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Syringe).Status = True Then
                        mEPVCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 1540
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("SetValue: " & gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Syringe).STR)
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1500
                        End If
                    End If
                End If

            Case 1540
                '[Note]:確認設定的膠管壓力
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Syringe) = False Then
                    If gEPVCollection.GetValue(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Syringe) = True Then
                        'Debug.Print("StageNo EPVNo: " & sys.StageNo & " , " & sys.EPVNo)
                        sys.SysNum = 1560
                    End If
                End If

            Case 1560
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Syringe) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gEPVCollection.IsTimeOut(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Syringe) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1540
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Syringe).Status = True Then
                        mSyringPressure(sys.StageNo, eValveWorkMode.Valve1) = gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Syringe).Value
                        mEPVCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 1580
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("SetValue: " & gEPVCollection.Result(sys.EPVNo).STR)
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1540
                        End If
                    End If
                End If

            Case 1580
                '[Note]:確認設定值與回傳值
                If Math.Abs(gCRecipe.StageParts(sys.StageNo).SyringePressure(eValveWorkMode.Valve1) - mSyringPressure(sys.StageNo, eValveWorkMode.Valve1)) > gSSystemParameter.PressureTolerance Then
                    If mEPVStopWatch(sys.StageNo).ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                        mEPVStopWatch(sys.StageNo).Stop()
                        'TODO:跳Alarm
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2019100", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019100), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2019200", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019200), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2019300", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019300), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm

                    Else
                        sys.SysNum = 1540
                    End If
                Else
                    Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).ValveType(eValveWorkMode.Valve1)
                        Case enmValveType.Jet
                            '[Note]:Jet Valve氣壓要給，其餘的在點膠的時候才給氣壓
                            Call gSysAdapter.SetSyringePressure(sys.StageNo, eValveWorkMode.Valve1, enmONOFF.eON)
                            Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).JetValve(eValveWorkMode.Valve1)
                                Case eValveModel.Advanjet
                                    mEPVStopWatch(sys.StageNo).Restart()
                                    sys.SysNum = 1600

                                Case Else
                                    If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                                        mEPVStopWatch(sys.StageNo).Restart()
                                        sys.SysNum = 1700
                                    Else
                                        sys.SysNum = 2000
                                    End If

                            End Select

                        Case Else
                            If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                                mEPVStopWatch(sys.StageNo).Restart()
                                sys.SysNum = 1700
                            Else
                                sys.SysNum = 2000
                            End If

                    End Select
                End If

            Case 1600
                '[Note]:設定膠管壓力
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Valve) = False Then
                    If gEPVCollection.SetValue(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Valve, gCRecipe.StageParts(sys.StageNo).ValvePressure(eValveWorkMode.Valve1), False) = True Then
                        'Debug.Print("StageNo EPVNo: " & sys.StageNo & " , " & sys.EPVNo)
                        sys.SysNum = 1620
                    End If
                End If

            Case 1620
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Valve) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gEPVCollection.IsTimeOut(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Valve) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1600
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Valve).Status = True Then
                        mEPVCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 1640
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("SetValue: " & gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Valve).STR)
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1600
                        End If
                    End If
                End If

            Case 1640
                '[Note]:確認設定的膠管壓力
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Valve) = False Then
                    If gEPVCollection.GetValue(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Valve) = True Then
                        'Debug.Print("StageNo EPVNo: " & sys.StageNo & " , " & sys.EPVNo)
                        sys.SysNum = 1660
                    End If
                End If

            Case 1660
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Valve) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gEPVCollection.IsTimeOut(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Valve) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1640
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Valve).Status = True Then
                        mValvePressure(sys.StageNo, eValveWorkMode.Valve1) = gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve1, eEPVPressureType.Valve).Value
                        mEPVCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 1680
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("SetValue: " & gEPVCollection.Result(sys.EPVNo).STR)
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1640
                        End If
                    End If
                End If

            Case 1680
                '[Note]:確認設定值與回傳值
                If Math.Abs(gCRecipe.StageParts(sys.StageNo).ValvePressure(eValveWorkMode.Valve1) - mValvePressure(sys.StageNo, eValveWorkMode.Valve1)) > gSSystemParameter.PressureTolerance Then
                    If mEPVStopWatch(sys.StageNo).ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                        mEPVStopWatch(sys.StageNo).Stop()
                        'TODO:跳Alarm
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2019100", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019100), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2019200", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019200), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2019300", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019300), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                    Else
                        sys.SysNum = 1640
                    End If
                Else
                    Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).ValveType(eValveWorkMode.Valve1)
                        Case enmValveType.Jet
                            Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).JetValve(eValveWorkMode.Valve1)
                                Case eValveModel.Advanjet
                                    '[Note]:Jet Valve氣壓要給，其餘的在點膠的時候才給氣壓
                                    Call gSysAdapter.SetValvePressure(sys.StageNo, eValveWorkMode.Valve1, enmONOFF.eON)

                            End Select

                        Case Else

                    End Select

                    If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                        mEPVStopWatch(sys.StageNo).Restart()
                        sys.SysNum = 1700
                    Else
                        sys.SysNum = 2000
                    End If
                End If

            Case 1700
                '[Note]:設定膠管壓力
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Syringe) = False Then
                    If gEPVCollection.SetValue(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Syringe, gCRecipe.StageParts(sys.StageNo).SyringePressure(eValveWorkMode.Valve2), False) = True Then
                        'Debug.Print("StageNo EPVNo: " & sys.StageNo & " , " & sys.EPVNo)
                        sys.SysNum = 1720
                    End If
                End If

            Case 1720
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Syringe) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gEPVCollection.IsTimeOut(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Syringe) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1700
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Syringe).Status = True Then
                        mEPVCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 1740
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("SetValue: " & gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Syringe).STR)
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1700
                        End If
                    End If
                End If

            Case 1740
                '[Note]:確認設定的膠管壓力
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Syringe) = False Then
                    If gEPVCollection.GetValue(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Syringe) = True Then
                        'Debug.Print("StageNo EPVNo: " & sys.StageNo & " , " & sys.EPVNo)
                        sys.SysNum = 1760
                    End If
                End If

            Case 1760
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Syringe) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gEPVCollection.IsTimeOut(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Syringe) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1740
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Syringe).Status = True Then
                        mSyringPressure(sys.StageNo, eValveWorkMode.Valve2) = gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Syringe).Value
                        mEPVCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 1780
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("SetValue: " & gEPVCollection.Result(sys.EPVNo).STR)
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1740
                        End If
                    End If
                End If

            Case 1780
                '[Note]:確認設定值與回傳值
                If Math.Abs(gCRecipe.StageParts(sys.StageNo).SyringePressure(eValveWorkMode.Valve2) - mSyringPressure(sys.StageNo, eValveWorkMode.Valve2)) > gSSystemParameter.PressureTolerance Then
                    If mEPVStopWatch(sys.StageNo).ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                        mEPVStopWatch(sys.StageNo).Stop()
                        'TODO:跳Alarm
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2019100", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019100), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2019200", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019200), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2019300", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019300), eMessageLevel.Error)
                        End Select
                    Else
                        sys.SysNum = 1740
                    End If
                Else
                    Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).ValveType(eValveWorkMode.Valve2)
                        Case enmValveType.Jet
                            '[Note]:Jet Valve氣壓要給，其餘的在點膠的時候才給氣壓
                            Call gSysAdapter.SetSyringePressure(sys.StageNo, eValveWorkMode.Valve2, enmONOFF.eON)
                            Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).JetValve(eValveWorkMode.Valve2)
                                Case eValveModel.Advanjet
                                    mEPVStopWatch(sys.StageNo).Restart()
                                    sys.SysNum = 1800

                                Case Else
                                    sys.SysNum = 2000
                            End Select

                        Case Else
                            sys.SysNum = 2000

                    End Select
                End If

            Case 1800
                '[Note]:設定膠管壓力
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Valve) = False Then
                    If gEPVCollection.SetValue(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Valve, gCRecipe.StageParts(sys.StageNo).ValvePressure(eValveWorkMode.Valve2), False) = True Then
                        'Debug.Print("StageNo EPVNo: " & sys.StageNo & " , " & sys.EPVNo)
                        sys.SysNum = 1820
                    End If
                End If

            Case 1820
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Valve) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gEPVCollection.IsTimeOut(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Valve) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1800
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Valve).Status = True Then
                        mEPVCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 1840
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("SetValue: " & gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Valve).STR)
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1800
                        End If
                    End If
                End If

            Case 1840
                '[Note]:確認設定的膠管壓力
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Valve) = False Then
                    If gEPVCollection.GetValue(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Valve) = True Then
                        'Debug.Print("StageNo EPVNo: " & sys.StageNo & " , " & sys.EPVNo)
                        sys.SysNum = 1860
                    End If
                End If

            Case 1860
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gEPVCollection.IsBusy(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Valve) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    If gEPVCollection.IsTimeOut(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Valve) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1840
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Valve).Status = True Then
                        mValvePressure(sys.StageNo, eValveWorkMode.Valve2) = gEPVCollection.Result(sys.StageNo, eValveWorkMode.Valve2, eEPVPressureType.Valve).Value
                        mEPVCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 1880
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("SetValue: " & gEPVCollection.Result(sys.EPVNo).STR)
                        mEPVCmdFailCount(sys.StageNo) = mEPVCmdFailCount(sys.StageNo) + 1
                        If mEPVCmdFailCount(sys.StageNo) > gEPVCmdMaxFailCounts Then
                            gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 1840
                        End If
                    End If
                End If

            Case 1880
                '[Note]:確認設定值與回傳值
                If Math.Abs(gCRecipe.StageParts(sys.StageNo).ValvePressure(eValveWorkMode.Valve2) - mValvePressure(sys.StageNo, eValveWorkMode.Valve2)) > gSSystemParameter.PressureTolerance Then
                    If mEPVStopWatch(sys.StageNo).ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                        mEPVStopWatch(sys.StageNo).Stop()
                        'TODO:跳Alarm
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2019000", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019000), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2019100", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019100), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2019200", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019200), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2019300", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2019300), eMessageLevel.Error)
                        End Select
                    Else
                        sys.SysNum = 1840
                    End If
                Else
                    Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).ValveType(eValveWorkMode.Valve2)
                        Case enmValveType.Jet
                            Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).JetValve(eValveWorkMode.Valve2)
                                Case eValveModel.Advanjet
                                    '[Note]:Jet Valve氣壓要給，其餘的在點膠的時候才給氣壓
                                    Call gSysAdapter.SetValvePressure(sys.StageNo, eValveWorkMode.Valve2, enmONOFF.eON)

                            End Select

                        Case Else

                    End Select
                    sys.SysNum = 2000
                End If

            Case 2000
                'STOPWATCH 停止
                mEPVStopWatch(sys.StageNo).Stop()

                '[Note]:開啟膠管、閥體壓力(膠管壓力開關，取決於閥體種類)
                For mI(sys.StageNo) = 0 To gSSystemParameter.StageUseValveCount - 1
                    Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).ValveType(mI(sys.StageNo))
                        Case enmValveType.Jet
                            '[Note]:Jet Valve氣壓要給，其餘的在點膠的時候才給氣壓
                            Call gSysAdapter.SetSyringePressure(sys.StageNo, mI(sys.StageNo), enmONOFF.eON)

                            Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).JetValve(mI(sys.StageNo))
                                Case eValveModel.Advanjet
                                    Call gSysAdapter.SetValvePressure(sys.StageNo, mI(sys.StageNo), enmONOFF.eON)

                            End Select

                        Case Else
                            Call gSysAdapter.SetSyringePressure(sys.StageNo, mI(sys.StageNo), enmONOFF.eOff)
                            Call gSysAdapter.SetValvePressure(sys.StageNo, mI(sys.StageNo), enmONOFF.eOff)

                    End Select
                Next
                sys.SysNum = 3000

            Case 3000
                '[Note]:生產前檢查(CCD未Ready報警)
                If gAOICollection.IsCCDReady(sys.CCDNo) = False Then
                    Select Case sys.StageNo
                        Case enmStage.No1
                            gEqpMsg.AddHistoryAlarm("Alarm_2012002", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1012002), eMessageLevel.Alarm)
                        Case enmStage.No2
                            gEqpMsg.AddHistoryAlarm("Alarm_2012102", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1012101), eMessageLevel.Alarm)
                        Case enmStage.No3
                            gEqpMsg.AddHistoryAlarm("Alarm_2012202", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1012201), eMessageLevel.Alarm)
                        Case enmStage.No4
                            gEqpMsg.AddHistoryAlarm("Alarm_2012302", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1012301), eMessageLevel.Alarm)
                    End Select
                    sys.Act(eAct.ProductLoading).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                    gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                Else
                    sys.SysNum = 3100
                End If

            Case 3100
                '[Note]:膠管之膠材檢測
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        Select Case sys.StageNo
                            Case enmStage.No1
                                If gSSystemParameter.StageParts.ValveData(sys.StageNo).EnableDetectPaste(sys.SelectValve) = True Then
                                    If gDICollection.GetState(enmDI.DetectSyringeSensor1) = False Then
                                        gEqpMsg.AddHistoryAlarm("Warn_3019006", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3019006), eMessageLevel.Warning)
                                        sys.Act(eAct.ProductLoading).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                                        sys.RunStatus = enmRunStatus.Alarm
                                        Exit Sub
                                    End If
                                End If

                            Case enmStage.No2
                                If gSSystemParameter.StageParts.ValveData(sys.StageNo).EnableDetectPaste(sys.SelectValve) = True Then
                                    If gDICollection.GetState(enmDI.DetectSyringeSensor2) = False Then
                                        gEqpMsg.AddHistoryAlarm("Warn_3019106", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3019106), eMessageLevel.Warning)
                                        sys.Act(eAct.ProductLoading).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                                        sys.RunStatus = enmRunStatus.Alarm
                                        Exit Sub
                                    End If
                                End If

                            Case enmStage.No3
                                If gSSystemParameter.StageParts.ValveData(sys.StageNo).EnableDetectPaste(sys.SelectValve) = True Then
                                    If gDICollection.GetState(enmDI.DetectSyringeSensor3) = False Then
                                        gEqpMsg.AddHistoryAlarm("Warn_3019206", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3019206), eMessageLevel.Warning)
                                        sys.Act(eAct.ProductLoading).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                                        sys.RunStatus = enmRunStatus.Alarm
                                        Exit Sub
                                    End If
                                End If

                            Case enmStage.No4
                                If gSSystemParameter.StageParts.ValveData(sys.StageNo).EnableDetectPaste(sys.SelectValve) = True Then
                                    If gDICollection.GetState(enmDI.DetectSyringeSensor4) = False Then
                                        gEqpMsg.AddHistoryAlarm("Warn_3019306", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3019306), eMessageLevel.Warning)
                                        sys.Act(eAct.ProductLoading).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                                        sys.RunStatus = enmRunStatus.Alarm
                                        Exit Sub
                                    End If
                                End If

                        End Select

                    Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        Select Case sys.StageNo
                            Case enmStage.No1
                                If gSSystemParameter.StageParts.ValveData(sys.StageNo).EnableDetectPaste(sys.SelectValve) = True Then
                                    If gDICollection.GetState(enmDI.DetectSyringeSensor1) = False Then
                                        gEqpMsg.AddHistoryAlarm("Warn_3019006", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3019006), eMessageLevel.Warning)
                                        sys.Act(eAct.ProductLoading).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                                        sys.RunStatus = enmRunStatus.Alarm
                                        Exit Sub
                                    End If
                                End If

                            Case enmStage.No2
                                If gSSystemParameter.StageParts.ValveData(sys.StageNo).EnableDetectPaste(sys.SelectValve) = True Then
                                    If gDICollection.GetState(enmDI.DetectSyringeSensor2) = False Then
                                        gEqpMsg.AddHistoryAlarm("Warn_3019106", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3019106), eMessageLevel.Warning)
                                        sys.Act(eAct.ProductLoading).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                                        sys.RunStatus = enmRunStatus.Alarm
                                        Exit Sub
                                    End If
                                End If

                        End Select

                    Case Else
                        If gSSystemParameter.StageParts.ValveData(sys.StageNo).EnableDetectPaste(sys.SelectValve) = True Then
                            Select Case sys.SelectValve
                                Case eValveWorkMode.Valve1
                                    If gDICollection.GetState(enmDI.DetectSyringeSensor1) = False Then
                                        gEqpMsg.AddHistoryAlarm("Warn_3019006", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3019006), eMessageLevel.Warning)
                                        sys.Act(eAct.ProductLoading).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                                        sys.RunStatus = enmRunStatus.Alarm
                                        Exit Sub
                                    End If

                                Case eValveWorkMode.Valve2
                                    If gDICollection.GetState(enmDI.DetectSyringeSensor2) = False Then
                                        gEqpMsg.AddHistoryAlarm("Warn_3019106", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Warn_3019106), eMessageLevel.Warning)
                                        sys.Act(eAct.ProductLoading).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                                        sys.RunStatus = enmRunStatus.Alarm
                                        Exit Sub
                                    End If

                            End Select
                        End If

                End Select
                sys.SysNum = 4000

            Case 4000
                '[Note]:設定閥體溫度
                '[Note]:(Step1.)Reset Alarm
                '               Check trigger board is ready before send command
                If IsTriggerBoardAlarm(sys.StageNo) = True Then
                    If IsTriggerBoardReady(sys.StageNo) = True Then
                        If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                            If gTriggerBoard.SetResetAlarm(sys.StageNo, False) = True Then
                                sys.SysNum = 4100
                            End If
                        End If
                    End If
                Else
                    sys.SysNum = 4200
                End If

            Case 4100
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
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016001", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016101", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016201", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016301", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 4000
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gTriggerBoard.ResetAlarm(sys.StageNo).Status = True Then
                        mTriggerCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 4200
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("ResetAlarm: " & gTriggerBoard.ResetAlarm(sys.StageNo).STR)
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016001", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016101", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016201", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Alarm_2016301", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 4000
                        End If
                    End If
                End If

            Case 4200
                '[Note]:閥體溫度設定
                mHeadNo(sys.StageNo) = eValveWorkMode.Valve1
                If gSSystemParameter.MultiDispenseEnable = True Then
                    mHeadNo(sys.StageNo) = eValveWorkMode.MultiValve
                    If gTempDB.ContainsKey(gCRecipe.TempName) = True Then
                        GetValveIndex(sys.StageNo, eValveWorkMode.Valve1, mNozzleNo(sys.StageNo))
                        If gTempDB(gCRecipe.TempName).TempParam(mNozzleNo(sys.StageNo)).Enabled = CheckState.Checked Then
                            mTemperature(sys.StageNo) = gTempDB(gCRecipe.TempName).TempParam(mNozzleNo(sys.StageNo)).SetValue
                            sys.SysNum = 4300
                        Else
                            '[Note]:不做加熱
                            sys.SysNum = 5000
                        End If
                    Else
                        '[Note]:不做加熱
                        sys.SysNum = 5000
                    End If
                Else
                    If gIsUseValveNo1(sys.StageNo) = True Then
                        mHeadNo(sys.StageNo) = eValveWorkMode.Valve1
                        If gTempDB.ContainsKey(gCRecipe.TempName) = True Then
                            If gIsUseValveNo1(sys.StageNo) = True Then
                                GetValveIndex(sys.StageNo, eValveWorkMode.Valve1, mNozzleNo(sys.StageNo))
                                If gTempDB(gCRecipe.TempName).TempParam(mNozzleNo(sys.StageNo)).Enabled = CheckState.Checked Then
                                    mTemperature(sys.StageNo) = gTempDB(gCRecipe.TempName).TempParam(mNozzleNo(sys.StageNo)).SetValue
                                    sys.SysNum = 4300
                                Else
                                    '[Note]:不做加熱
                                    sys.SysNum = 4500
                                End If
                            Else
                                '[Note]:不做加熱
                                sys.SysNum = 4500
                            End If
                        Else
                            '[Note]:不做加熱
                            sys.SysNum = 4500
                        End If
                    Else
                        '[Note]:不做加熱
                        sys.SysNum = 4500
                    End If
                End If
                mTriggerCmdFailCount(sys.StageNo) = 0

            Case 4300
                '[Note]:(Step3.)Send S Cmd
                '               Check trigger board is ready before send command
                If IsTriggerBoardReady(sys.StageNo) = True Then
                    If gTriggerBoard.IsBusy(sys.StageNo) = False Then
                        If gTriggerBoard.SetTempture(sys.StageNo, mHeadNo(sys.StageNo), mTemperature(sys.StageNo), False) = True Then
                            sys.SysNum = 4400
                        End If
                    End If
                End If

            Case 4400
                '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
                If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                    '[Note]:還在接收傳送資料中
                    If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                        '[Note]:超過時間還沒處裡完-->在下一次
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)_20170508
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1016003", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1016103", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1016203", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1016303", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 4300
                        End If
                    End If
                Else
                    '[Note]:檢查接收資料
                    If gTriggerBoard.Temperature(sys.StageNo).Status = True Then
                        mTriggerCmdFailCount(sys.StageNo) = 0
                        sys.SysNum = 4500
                    Else
                        '[Note]:查看收到的內容是????
                        Debug.Print("Temperature(S Cmd): " & gTriggerBoard.Temperature(sys.StageNo).STR)
                        mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                        If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                            'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                            Select Case sys.StageNo
                                Case 0
                                    gEqpMsg.AddHistoryAlarm("Error_1016003", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                Case 1
                                    gEqpMsg.AddHistoryAlarm("Error_1016103", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                Case 2
                                    gEqpMsg.AddHistoryAlarm("Error_1016203", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                Case 3
                                    gEqpMsg.AddHistoryAlarm("Error_1016303", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                            End Select
                            sys.RunStatus = enmRunStatus.Alarm
                            Exit Sub
                        Else
                            sys.SysNum = 4300
                        End If
                    End If
                End If

            Case 4500
                '[Note]:閥體溫度設定
                If gSSystemParameter.MultiDispenseEnable = False Then
                    If gIsUseValveNo2(sys.StageNo) = True Then
                        If mHeadNo(sys.StageNo) < gSSystemParameter.StageUseValveCount - 1 Then
                            mHeadNo(sys.StageNo) = eValveWorkMode.Valve2
                            If gTempDB.ContainsKey(gCRecipe.TempName) = True Then
                                If gIsUseValveNo2(sys.StageNo) = True Then
                                    GetValveIndex(sys.StageNo, eValveWorkMode.Valve2, mNozzleNo(sys.StageNo))
                                    If gTempDB(gCRecipe.TempName).TempParam(mNozzleNo(sys.StageNo)).Enabled = CheckState.Checked Then
                                        mTemperature(sys.StageNo) = gTempDB(gCRecipe.TempName).TempParam(mNozzleNo(sys.StageNo)).SetValue
                                        sys.SysNum = 4300
                                    Else
                                        '[Note]:不做加熱
                                        sys.SysNum = 5000
                                    End If
                                Else
                                    '[Note]:不做加熱
                                    sys.SysNum = 5000
                                End If
                            Else
                                '[Note]:不做加熱
                                sys.SysNum = 5000
                            End If
                        Else
                            sys.SysNum = 5000
                        End If
                    Else
                        '[Note]:不做加熱
                        sys.SysNum = 5000
                    End If
                End If
                mTriggerCmdFailCount(sys.StageNo) = 0

            Case 5000
                '[Note]:等待Table Stop
                mAxisXState(sys.StageNo) = gCMotion.MotionDone(sys.AxisX)
                mAxisYState(sys.StageNo) = gCMotion.MotionDone(sys.AxisY)
                mAxisZState(sys.StageNo) = gCMotion.MotionDone(sys.AxisZ)

                If mAxisXState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisX) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1030004", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1042004", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1060004", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1067004", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067004), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1030004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030004), eMessageLevel.Error)       'X軸等待到位逾時
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
                                gEqpMsg.AddHistoryAlarm("Error_1031004", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1043004", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1061004", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1068004", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068004), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1031004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)       'Y軸等待到位逾時
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
                                gEqpMsg.AddHistoryAlarm("Error_1031004", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1043004", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1061004", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1068004", "DispStage_ProductLoadingAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                        End Select
                        'gEqpMsg.AddHistoryAlarm("Error_1031004", "DispStage_LaserReaderAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031004), eMessageLevel.Error)       'Y軸等待到位逾時
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                sys.SysNum = 9000

            Case 9000
                sys.RunStatus = enmRunStatus.Finish
                Exit Sub

        End Select

    End Sub



    ''' <summary>[跑至安全位置動作流程]</summary>
    ''' <remarks></remarks>
    Sub DispStage_SafeAction(ByRef sys As sSysParam)

        Dim mAxisXState(enmStage.Max) As CommandStatus                          '[X軸的狀態]
        Dim mAxisYState(enmStage.Max) As CommandStatus                          '[Y軸的狀態]
        Dim mAxisZState(enmStage.Max) As CommandStatus                          '[Z軸的狀態]
        Dim mAxisBState(enmStage.Max) As CommandStatus                          '[Tilt軸的狀態]

        Select Case sys.SysNum
            Case sSysParam.SysLoopStart
                sys.SelectValve = eValveWorkMode.Valve1

                If sys.AxisZ = 0 Then
                    '[Note]:先抓異常避免撞機，但這是最終端，仍須從源頭找是誰偷改
                    gEqpMsg.AddHistoryAlarm("Error_1030010", "DispStage_SafeAction", sys.SysNum, "Safe Action 資料異常")
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If

                '[說明]:速度載入
                If gCMotion.SetVelAccDec(sys.AxisX) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030010))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042010))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060010))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067010))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetVelAccDec(sys.AxisY) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031010))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043010))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061010))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068010))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetVelAccDec(sys.AxisZ) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032010))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044010))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062010))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069010))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.SetVelAccDec(sys.AxisB) = False Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1034010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1034010))
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1046010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1046010))
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1064010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1064010))
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1071010", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1071010))
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 2000

            Case 2000
                '[Note]:先將Z軸升至安全位置
                If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.TiltSafePosZ) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1032000", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1044000", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1062000", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1069000", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 3000

            Case 3000
                '[Note]:等待Table Stop
                mAxisZState(sys.StageNo) = gCMotion.MotionDone(sys.AxisZ)
                If mAxisZState(sys.StageNo) <> CommandStatus.Sucessed Then
                    If gCMotion.IsMoveTimeOut(sys.AxisZ) Then
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1032004", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1032004), eMessageLevel.Error)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1044004", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1044004), eMessageLevel.Error)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1062004", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1062004), eMessageLevel.Error)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1069004", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1069004), eMessageLevel.Error)
                        End Select
                        sys.RunStatus = enmRunStatus.Alarm
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
                sys.SysNum = 4000

            Case 4000
                '[說明]:載入移動-->移動到起始座標
                If gCMotion.AbsMove(sys.AxisX, gSSystemParameter.Pos.PurgeCalibration(sys.StageNo).ValvePosX(sys.SelectValve)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1030000", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1030000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1042000", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1042000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1060000", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1060000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1067000", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1067000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                If gCMotion.AbsMove(sys.AxisY, gSSystemParameter.Pos.PurgeCalibration(sys.StageNo).ValvePosY(sys.SelectValve)) <> CommandStatus.Sucessed Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1031000", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1031000), eMessageLevel.Error)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1043000", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1043000), eMessageLevel.Error)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1061000", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1061000), eMessageLevel.Error)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1068000", "DispStage_SafeAction", sys.SysNum, gMsgHandler.GetMessage(Error_1068000), eMessageLevel.Error)
                    End Select
                    sys.RunStatus = enmRunStatus.Alarm
                    Exit Sub
                End If
                sys.SysNum = 5000

            Case 5000
                If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) = CommandStatus.Sucessed Then
                    sys.SysNum = 9000
                End If

            Case 9000
                sys.RunStatus = enmRunStatus.Finish
                Exit Sub

                '***********************************************************************************************************************************************************

        End Select



    End Sub


#End Region

#Region "Stage控制層(LevelNo3端:監控<溫度>)"

#End Region

    '#Region "SubDisp控制層(LevelNo4端:控制點膠周邊動作<清膠、除膠、秤重>)"

    '#End Region




#Region " Other Function"

    ''' <summary>[判斷是否已經達到預熱時間]</summary>
    ''' <param name="machineNo"></param>
    ''' <param name="conveyor"></param>
    ''' <param name="stopwatch"></param>
    ''' <param name="time"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsWaitPriorHeat(ByVal machineNo As enmMachineStation, ByVal conveyor As eConveyor, ByVal stopwatch() As Stopwatch, ByVal time As Decimal)
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If stopwatch(machineNo).ElapsedMilliseconds > CLng(time) Then
                    stopwatch(machineNo).Stop()
                    Return True
                Else
                    Debug.Print("Wait Time: " & stopwatch(machineNo).ElapsedMilliseconds)
                    Return False
                End If

            Case Else
                If stopwatch(conveyor).ElapsedMilliseconds > CLng(time) Then
                    stopwatch(conveyor).Stop()
                    Return True
                Else
                    Debug.Print("Wait Time: " & stopwatch(conveyor).ElapsedMilliseconds)
                    Return False
                End If

        End Select

    End Function

    ''' <summary>[抓取那些機台需作業]</summary>
    ''' <param name="isBypassMachineA"></param>
    ''' <param name="isBypassMachineB"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMachineBypassState(ByRef isBypassMachineA As Boolean, ByRef isBypassMachineB As Boolean) As Boolean

        Dim mIsBypassMachineA As Boolean
        Dim mIsBypassMachineB As Boolean
        Dim mStageNo As Integer

        mIsBypassMachineA = True
        mIsBypassMachineB = True


        For mStageNo = enmStage.No1 To gSSystemParameter.StageCount - 1
            '[Note]:TODO:CCDFix改版需要修改
            If gCRecipe.ScanTraversal(mStageNo).Count <> 0 Then
                Select Case mStageNo
                    Case enmStage.No1, enmStage.No2
                        mIsBypassMachineA = False

                    Case enmStage.No3, enmStage.No4
                        mIsBypassMachineB = False

                End Select
            End If
        Next

        isBypassMachineA = mIsBypassMachineA
        isBypassMachineB = mIsBypassMachineB
        Return True
    End Function

    Public Enum MapErrorCode
        ''' <summary>成功
        ''' </summary>
        ''' <remarks></remarks>
        Success = 0
        ''' <summary>路徑為空
        ''' </summary>
        ''' <remarks></remarks>
        PathEmpty
        ''' <summary>路徑不存在
        ''' </summary>
        ''' <remarks></remarks>
        FileNotExists
        ''' <summary>檔案解析失敗, 無符合格式
        ''' </summary>
        ''' <remarks></remarks>
        FileAnalyzeError
        ''' <summary>資訊不符
        ''' </summary>
        ''' <remarks></remarks>
        InfoNotEqual
        ''' <summary>陣列大小不符
        ''' </summary>
        ''' <remarks></remarks>
        ArraySizeNotEqual
        ''' <summary>節點與Map對應資料遺漏
        ''' </summary>
        ''' <remarks></remarks>
        NodeFillMapLost
        ''' <summary>不明例外
        ''' </summary>
        ''' <remarks></remarks>
        UnknownException

    End Enum
    ''' <summary>
    ''' [Mapping Data轉換處理]
    ''' </summary>
    ''' <param name="machineNo"></param>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CoverMapData(ByVal machineNo As enmMachineStation, ByVal path As String) As MapErrorCode
        Dim mStageNo1 As enmStage
        Dim mStageNo2 As enmStage

        Try
            If path = "" Then
                Return MapErrorCode.PathEmpty
            End If
            If Not File.Exists(path) Then
                Return MapErrorCode.FileNotExists
            End If
            Select Case machineNo
                Case enmMachineStation.MachineA
                    mStageNo1 = enmStage.No1
                    mStageNo2 = enmStage.No2

                Case enmMachineStation.MachineB
                    mStageNo1 = enmStage.No3
                    mStageNo2 = enmStage.No4
            End Select

            '[Note]:根據進料的資訊、取出Wafer Map的檔案路徑、名稱
            Dim returnValue As MapErrorCode = MapToData(machineNo, path)
            If returnValue <> MapErrorCode.Success Then
                Return returnValue
            End If

            ''[Note]:取得Notch方向
            gMapData(machineNo).ResetMapNotch(gCRecipe.NotchDir(machineNo))

            '[Note]:比對Mapping Data是否相符, 無此資訊則不比對
            If gMapData(machineNo).Information.Type <> "N/A" And gMapData(machineNo).Information.Type <> "" Then
                If gMapData(machineNo).Information.Type <> gCRecipe.ProductType Then
                    Return MapErrorCode.InfoNotEqual
                End If
            End If

            '[Note]:合併NodeToMap裡的Node, 取得總陣列大小
            Dim maxColumn As Integer = 1000
            Dim maxRow As Integer = 1000
            Dim trayArray(maxColumn, maxRow) As Integer
            If gCRecipe.NodeToMap(mStageNo1) IsNot Nothing Then
                For Each node2Map In gCRecipe.NodeToMap(mStageNo1)
                    If gCRecipe.Node(mStageNo1).ContainsKey(node2Map) Then
                        Dim x As Integer = gCRecipe.Node(mStageNo1)(node2Map).NodeStartingX - 1
                        Dim y As Integer = gCRecipe.Node(mStageNo1)(node2Map).NodeStartingY - 1
                        Dim nodeArray As CMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(mStageNo1)(node2Map).Array)
                        Dim column As Integer = nodeArray.GetMemoryCountX
                        Dim row As Integer = nodeArray.GetMemoryCountY
                        For c = 0 To column - 1
                            For r = 0 To row - 1
                                trayArray(x + c, y + r) += 1
                            Next
                        Next
                    End If
                Next
            End If

            If gCRecipe.NodeToMap(mStageNo2) IsNot Nothing Then
                For Each node2Map In gCRecipe.NodeToMap(mStageNo2)
                    If gCRecipe.Node(mStageNo2).ContainsKey(node2Map) Then
                        Dim x As Integer = gCRecipe.Node(mStageNo2)(node2Map).NodeStartingX - 1
                        Dim y As Integer = gCRecipe.Node(mStageNo2)(node2Map).NodeStartingY - 1
                        Dim nodeArray As CMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(mStageNo2)(node2Map).Array)
                        Dim column As Integer = nodeArray.GetMemoryCountX
                        Dim row As Integer = nodeArray.GetMemoryCountY
                        For c = 0 To column - 1
                            For r = 0 To row - 1
                                trayArray(x + c, y + r) += 1
                            Next
                        Next
                    End If
                Next
            End If


            Dim nodeColumn As Integer
            For i = 0 To maxColumn
                If (trayArray(i, 0) <> 1) Then
                    nodeColumn = i
                    Exit For
                End If
            Next

            Dim nodeRow As Integer
            For i = 0 To maxRow
                If (trayArray(0, i) <> 1) Then
                    nodeRow = i
                    Exit For
                End If
            Next

            '[Note]:比對Map與Node List陣列大小
            If ((gMapData(machineNo).Substrates(0).Columns <> nodeColumn) Or (gMapData(machineNo).Substrates(0).Rows <> nodeRow)) Then
                Return MapErrorCode.ArraySizeNotEqual
            End If

            '[Note]:檢查Node陣列是否為一個完整的矩形陣列,並排除重複(value > 1)或遺漏(value = 0)的地方
            For c = 0 To nodeColumn - 1
                For r = 0 To nodeRow - 1
                    If (trayArray(c, r) <> 1) Then
                        Return MapErrorCode.NodeFillMapLost
                    End If
                Next
            Next

            '[Note]:將MappingData丟入StageMap
            If gCRecipe.NodeToMap(mStageNo1) IsNot Nothing Then
                For Each node In gCRecipe.NodeToMap(mStageNo1)
                    If MapDataCoverToStageMap(gMapData(machineNo), gStageMap(mStageNo1).Node(node), gCRecipe.Node(mStageNo1)(node)) = False Then
                        Return MapErrorCode.UnknownException
                    End If
                Next
            End If

            '[Note]:將MappingData丟入StageMap
            If gCRecipe.NodeToMap(mStageNo2) IsNot Nothing Then
                For Each node In gCRecipe.NodeToMap(mStageNo2)
                    If MapDataCoverToStageMap(gMapData(machineNo), gStageMap(mStageNo2).Node(node), gCRecipe.Node(mStageNo2)(node)) = False Then
                        Return MapErrorCode.UnknownException
                    End If
                Next
            End If

            Return MapErrorCode.Success

        Catch ex As Exception
            Return MapErrorCode.UnknownException
        End Try
    End Function

    ''' <summary>
    ''' [取得最新的Map Data]
    ''' </summary>
    ''' <param name="ProductType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNewsMapping(ByVal ProductType As enmProductType, Optional ByVal IsReRun As Boolean = False) As String
        Try
            '判別產品類型選擇字串
            Dim ProductTypeName As String = ""

            Select Case ProductType
                Case enmProductType.Wafer
                    ProductTypeName = "Wafer"
                Case enmProductType.Strip
                    ProductTypeName = "Strip"
                Case enmProductType.Panel
                    ProductTypeName = "Panel"
            End Select

            Dim Filename As String = ""
            If IsReRun Then
                '找到目前最新的MappingData
                For Each Str As String In System.IO.Directory.EnumerateFiles("D:\\PIIData\\MappingData\\Rerun\\")      '此路徑是與Cray定義路徑
                    If Str.Contains(ProductTypeName) Then
                        If System.IO.File.Exists(Filename) Then
                            If System.IO.File.GetLastWriteTime(Str) > System.IO.File.GetLastWriteTime(Filename) Then
                                Filename = Str
                            End If
                        Else
                            Filename = Str
                        End If
                    End If
                Next

                Return Filename
            Else
                '找到目前最新的MappingData
                For Each Str As String In System.IO.Directory.EnumerateFiles("D:\\PIIData\\MappingData\\Source\\")      '此路徑是與Cray定義路徑
                    If Str.Contains(ProductTypeName) Then
                        If System.IO.File.Exists(Filename) Then
                            If System.IO.File.GetLastWriteTime(Str) > System.IO.File.GetLastWriteTime(Filename) Then
                                Filename = Str
                            End If
                        Else
                            Filename = Str
                        End If
                    End If
                Next

                Return Filename
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Return "Nothing"
        End Try
    End Function

    ''' <summary>[安全位置檢查]</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="nodeLevel"></param>
    ''' <param name="roundLevel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EstimateIsSafePos(ByVal stageNo As enmStage, ByVal nodeLevel() As Integer, Optional ByVal roundLevel() As Integer = Nothing) As Boolean

        '[Note]:只有左側需要停下來等，右側有作業優先權
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                Select Case stageNo
                    Case enmStage.No1
                        If IsSafePos(stageNo, gProtectData(enmStage.No2), gProtectData(stageNo).TargetPos) = True Then
                            Return True
                        Else
                            '[Note]:只有在NodeLevel小於對方的時候，可以叫R側退到安全區
                            If nodeLevel(enmStage.No1) < nodeLevel(enmStage.No2) Then
                                gIsRSideNeedGoToSafePos(enmMachineStation.MachineA) = True
                            Else
                                '[Note]:若RoundLevel(R)>RoundLevel(L)，則R退(不能要求L側退)
                                '       若RoundLevel(R)<=RoundLevel(L)，則L退
                                If Not IsNothing(roundLevel) = True Then
                                    If roundLevel(enmStage.No1) < roundLevel(enmStage.No2) Then
                                        gIsRSideNeedGoToSafePos(enmMachineStation.MachineA) = True
                                    Else
                                        gIsLSideNeedGoToSafePos(enmMachineStation.MachineA) = True
                                    End If
                                Else
                                    gIsLSideNeedGoToSafePos(enmMachineStation.MachineA) = True
                                End If
                            End If
                            Return False
                        End If

                    Case enmStage.No2
                        If IsSafePos(stageNo, gProtectData(enmStage.No1), gProtectData(stageNo).TargetPos) = True Then
                            Return True
                        Else
                            '[Note]:只有在NodeLevel大於對方的時候，可以叫R側退到安全區
                            If nodeLevel(enmStage.No1) < nodeLevel(enmStage.No2) Then
                                gIsRSideNeedGoToSafePos(enmMachineStation.MachineA) = True
                            Else
                                '[Note]:若RoundLevel(R)>RoundLevel(L)，則R退(不能要求L側退)
                                '       若RoundLevel(R)<=RoundLevel(L)，則L退
                                If Not IsNothing(roundLevel) = True Then
                                    If roundLevel(enmStage.No1) < roundLevel(enmStage.No2) Then
                                        gIsRSideNeedGoToSafePos(enmMachineStation.MachineA) = True
                                    Else
                                        gIsLSideNeedGoToSafePos(enmMachineStation.MachineA) = True
                                    End If
                                Else
                                    gIsLSideNeedGoToSafePos(enmMachineStation.MachineA) = True
                                End If
                                Return False
                            End If
                        End If

                    Case enmStage.No3
                        If IsSafePos(stageNo, gProtectData(enmStage.No4), gProtectData(stageNo).TargetPos) = True Then
                            Return True
                        Else
                            '[Note]:只有在NodeLevel小於對方的時候，可以叫R側退到安全區
                            If nodeLevel(enmStage.No3) < nodeLevel(enmStage.No4) Then
                                gIsRSideNeedGoToSafePos(enmMachineStation.MachineB) = True
                            Else
                                '[Note]:若RoundLevel(R)>RoundLevel(L)，則R退(不能要求L側退)
                                '       若RoundLevel(R)<=RoundLevel(L)，則L退
                                If Not IsNothing(roundLevel) = True Then
                                    If roundLevel(enmStage.No3) < roundLevel(enmStage.No4) Then
                                        gIsRSideNeedGoToSafePos(enmMachineStation.MachineB) = True
                                    Else
                                        gIsLSideNeedGoToSafePos(enmMachineStation.MachineB) = True
                                    End If
                                Else
                                    gIsLSideNeedGoToSafePos(enmMachineStation.MachineB) = True
                                End If
                            End If
                            Return False
                        End If

                    Case enmStage.No4
                        If IsSafePos(stageNo, gProtectData(enmStage.No3), gProtectData(stageNo).TargetPos) = True Then
                            Return True
                        Else
                            '[Note]:只有在NodeLevel大於對方的時候，可以叫R側退到安全區
                            If nodeLevel(enmStage.No3) < nodeLevel(enmStage.No4) Then
                                gIsRSideNeedGoToSafePos(enmMachineStation.MachineB) = True
                            Else
                                '[Note]:若RoundLevel(R)>RoundLevel(L)，則R退(不能要求L側退)
                                '       若RoundLevel(R)<=RoundLevel(L)，則L退
                                If Not IsNothing(roundLevel) = True Then
                                    If roundLevel(enmStage.No3) < roundLevel(enmStage.No4) Then
                                        gIsRSideNeedGoToSafePos(enmMachineStation.MachineB) = True
                                    Else
                                        gIsLSideNeedGoToSafePos(enmMachineStation.MachineB) = True
                                    End If
                                Else
                                    gIsLSideNeedGoToSafePos(enmMachineStation.MachineB) = True
                                End If
                                Return False
                            End If

                        End If

                End Select

            Case Else
                Return True

        End Select

        Return False

    End Function

    ''' <summary>[估測要退到哪裡的位置]</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="dispProtect">[二側資料]</param>
    ''' <param name="estimatePos">[目標位置(估測)]</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EstimateGoBackPos(ByVal stageNo As enmStage, ByRef dispProtect() As sProtectData, ByRef estimatePos As Premtek.sPos)

        Dim mEstimateNowPos As Premtek.sPos
        Dim mEstimateTargetPos As Premtek.sPos

        Select Case stageNo
            Case enmStage.No1
                '[說明]:更新另一側的目前座標
                dispProtect(enmStage.No2).NowPos.PosX = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage2).AxisX))
                dispProtect(enmStage.No2).NowPos.PosY = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage2).AxisY))
                '[Note]:先檢查X方向，Y方向視情況再決定要不要加
                '       跨距-L+R(Now)=S
                mEstimateNowPos.PosX = gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX + dispProtect(enmStage.No2).NowPos.PosX
                mEstimateTargetPos.PosX = gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX + dispProtect(enmStage.No2).TargetPos.PosX

                '[Note]:-1的目的是避免NowPos取值時差0.01
                '[Note]:反正都要退了，就退多一點也比較安全
                If mEstimateNowPos.PosX > mEstimateTargetPos.PosX Then
                    estimatePos.PosX = mEstimateTargetPos.PosX - 1
                Else
                    estimatePos.PosX = mEstimateNowPos.PosX - 1
                End If


            Case enmStage.No2
                dispProtect(enmStage.No1).NowPos.PosX = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage1).AxisX))
                dispProtect(enmStage.No1).NowPos.PosY = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage1).AxisY))
                '[Note]:先檢查X方向，Y方向視情況再決定要不要加
                '       跨距-L(Now)+R>S
                mEstimateNowPos.PosX = gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX + dispProtect(enmStage.No1).NowPos.PosX
                mEstimateTargetPos.PosX = gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX + dispProtect(enmStage.No1).TargetPos.PosX
                '[Note]:+1的目的是避免NowPos取值時差0.01
                '[Note]:反正都要退了，就退多一點也比較安全
                If mEstimateNowPos.PosX > mEstimateTargetPos.PosX Then
                    estimatePos.PosX = mEstimateNowPos.PosX + 1
                Else
                    estimatePos.PosX = mEstimateTargetPos.PosX + 1
                End If

            Case enmStage.No3
                dispProtect(enmStage.No4).NowPos.PosX = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage4).AxisX))
                dispProtect(enmStage.No4).NowPos.PosY = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage4).AxisY))
                '[Note]:先檢查X方向，Y方向視情況再決定要不要加
                '       跨距-L+R(Now)>S
                mEstimateNowPos.PosX = gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SpreadX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SafeDistanceX + dispProtect(enmStage.No4).NowPos.PosX
                mEstimateTargetPos.PosX = gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SpreadX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SafeDistanceX + dispProtect(enmStage.No4).TargetPos.PosX
                '[Note]:-1的目的是避免NowPos取值時差0.01
                '[Note]:反正都要退了，就退多一點也比較安全
                If mEstimateNowPos.PosX > mEstimateTargetPos.PosX Then
                    estimatePos.PosX = mEstimateTargetPos.PosX - 1
                Else
                    estimatePos.PosX = mEstimateNowPos.PosX - 1
                End If

            Case enmStage.No4
                dispProtect(enmStage.No3).NowPos.PosX = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage3).AxisX))
                dispProtect(enmStage.No3).NowPos.PosY = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage3).AxisY))
                '[Note]:先檢查X方向，Y方向視情況再決定要不要加
                '       跨距-L+R(Now)>S
                mEstimateNowPos.PosX = gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SafeDistanceX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SpreadX + dispProtect(enmStage.No3).NowPos.PosX
                mEstimateTargetPos.PosX = gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SafeDistanceX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SpreadX + dispProtect(enmStage.No3).TargetPos.PosX
                '[Note]:+1的目的是避免NowPos取值時差0.01
                '[Note]:反正都要退了，就退多一點也比較安全
                If mEstimateNowPos.PosX > mEstimateTargetPos.PosX Then
                    estimatePos.PosX = mEstimateNowPos.PosX + 1
                Else
                    estimatePos.PosX = mEstimateTargetPos.PosX + 1
                End If

        End Select

        Return True
    End Function


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

    ''' <summary>[確認真空建立(用來檢查有無料件)]</summary>
    ''' <param name="machineNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsChuckVacuumReady(ByVal machineNo As enmMachineStation) As Boolean
        Select Case machineNo
            Case enmMachineStation.MachineA
                '[Note]:確認真空建立
                If gDICollection.GetState(enmDI.Station2ChuckVacuumReady, True) = True Then
                    If gDICollection.GetState(enmDI.Station2ChuckVacuumReady2, True) = True Then
                        If gDICollection.GetState(enmDI.Station2ChuckVacuumReady3, True) = True Then
                            If gDICollection.GetState(enmDI.Station2ChuckVacuumReady4, True) = True Then
                                If gDICollection.GetState(enmDI.Station2ChuckVacuumReady5, True) = True Then
                                    If gDICollection.GetState(enmDI.Station2ChuckVacuumReady6, True) = True Then
                                        Return True
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                Return False

            Case enmMachineStation.MachineB
                '[Note]:確認真空建立
                If gDICollection.GetState(enmDI.Station3ChuckVacuumReady, True) = True Then
                    If gDICollection.GetState(enmDI.Station3ChuckVacuumReady2, True) = True Then
                        If gDICollection.GetState(enmDI.Station3ChuckVacuumReady3, True) = True Then
                            If gDICollection.GetState(enmDI.Station3ChuckVacuumReady4, True) = True Then
                                If gDICollection.GetState(enmDI.Station3ChuckVacuumReady5, True) = True Then
                                    If gDICollection.GetState(enmDI.Station3ChuckVacuumReady6, True) = True Then
                                        Return True
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                Return False

        End Select

        Return False
    End Function

    ''' <summary>[確認真空建立(用來檢查有無料件)]</summary>
    ''' <param name="conveyor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsChuckVacuumReady(ByVal conveyor As eConveyor) As Boolean
        Select Case conveyor
            Case eConveyor.ConveyorNo1
                '[Note]:確認真空建立
                If gDICollection.GetState(enmDI.Station2ChuckVacuumReady, True) = True Then
                    If gDICollection.GetState(enmDI.Station2ChuckVacuumReady2, True) = True Then
                        If gDICollection.GetState(enmDI.Station2ChuckVacuumReady3, True) = True Then
                            If gDICollection.GetState(enmDI.Station2ChuckVacuumReady4, True) = True Then
                                If gDICollection.GetState(enmDI.Station2ChuckVacuumReady5, True) = True Then
                                    If gDICollection.GetState(enmDI.Station2ChuckVacuumReady6, True) = True Then
                                        Return True
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                Return False

            Case eConveyor.ConveyorNo2
                '[Note]:確認真空建立
                If gDICollection.GetState(enmDI.Station3ChuckVacuumReady, True) = True Then
                    If gDICollection.GetState(enmDI.Station3ChuckVacuumReady2, True) = True Then
                        If gDICollection.GetState(enmDI.Station3ChuckVacuumReady3, True) = True Then
                            If gDICollection.GetState(enmDI.Station3ChuckVacuumReady4, True) = True Then
                                If gDICollection.GetState(enmDI.Station3ChuckVacuumReady5, True) = True Then
                                    If gDICollection.GetState(enmDI.Station3ChuckVacuumReady6, True) = True Then
                                        Return True
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                Return False
        End Select

        Return False
    End Function

    ''' <summary>
    ''' 判斷檔名是否正確
    ''' </summary>
    ''' <param name="sFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsillegalFileName(ByVal sFileName As String) As Boolean
        ''檔名限制定義
        Dim sError As String() = {"@", "?", "*", "!", "/", "\", "|"}
        Try
            For i = 0 To sError.Length - 1
                If sFileName.Contains(sError(i)) = True Then
                    Return True
                End If
            Next
            Return False

        Catch ex As Exception
            Return False
        End Try

    End Function
    ''' <summary>判斷資料夾名稱是否正確</summary>
    ''' <param name="sFolderName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsIllegalFolderName(ByVal sFolderName As String) As Boolean
        ''檔名限制定義
        Dim sError As String() = {"@", "?", "*", "!", "/", "|"}
        Try
            For i = 0 To sError.Length - 1
                If sFolderName.Contains(sError(i)) = True Then
                    Return True
                End If
            Next
            Return False

        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' ASE/PII Map 讀檔
    ''' </summary>
    ''' <param name="machineNo">A/B機</param>
    ''' <param name="path">ASE/PII Map 檔案路徑</param>
    ''' <returns>成功/失敗</returns>
    ''' <remarks></remarks>
    Public Function MapToData(ByVal machineNo As enmMachineStation, ByVal path As String) As MapErrorCode
        Try
            Dim arrStr() As String = path.Split("\\")
            Dim fileName() As String = arrStr(arrStr.Length - 1).Split(".")
            Dim savePath As String = "D:\\PIIData\\MappingData\\Source\\"
            Dim filePath As String = "D:\\PIIData\\MappingData\\Source\\" & fileName(0) & ".csv"

            If (gMapData(machineNo).OpenFile(path)) Then
                Return MapErrorCode.Success
            ElseIf (WaferMapConvertToPIIMap(path, savePath)) Then
                If gMapData(machineNo).OpenFile(filePath) Then
                    Return MapErrorCode.Success
                End If
            End If
            Return MapErrorCode.FileAnalyzeError
        Catch ex As Exception
            Return MapErrorCode.UnknownException
        End Try
    End Function

    Public Function GetSysParamTilePos(ByVal sys As sSysParam) As Decimal
        Select Case sys.StageNo
            Case 0
                Return gSSystemParameter.Stage1TiltAngle
            Case 1
                Return gSSystemParameter.Stage1TiltAngle
            Case 2
                Return gSSystemParameter.Stage1TiltAngle
            Case 3
                Return gSSystemParameter.Stage1TiltAngle
        End Select
        Return 0 'Soni + 2017.01.20 標準角度應為0
    End Function

    Public Sub SetAHotPlate()

        '[說明]:判斷有無開啟Recipe
        If gCRecipe.strName = "" Then
            MessageBox.Show("Not load Recipe!!!")
            gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmOperation btnStart", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
            Exit Sub
        End If

        If gCRecipe.TempName = "" Then
            MsgBox("No Temperature File", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If Not gTempDB.ContainsKey(gCRecipe.TempName) Then
            MsgBox("No Temperature File", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '溫控器寫入SV參數
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.A1, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.A2, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.A3, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.A4, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.A5, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.A6, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        WetcoConveyor.mGlobalPool.SV = gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue

        'hot plate設定
        Dim hpA As WetcoConveyor.HotPlate
        hpA.HotPlate1 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateA1).Enabled
        hpA.HotPlate2 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateA2).Enabled
        hpA.HotPlate3 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateA3).Enabled
        hpA.HotPlate4 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateA4).Enabled
        hpA.HotPlate5 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateA5).Enabled
        hpA.HotPlate6 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateA6).Enabled
        WetcoConveyor.Unit.A_SetHotPlate(hpA)

    End Sub

    Public Sub SetBHotPlate()

        '[說明]:判斷有無開啟Recipe
        If gCRecipe.strName = "" Then
            MessageBox.Show("Not load Recipe!!!")
            gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmOperation btnStart", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
            Exit Sub
        End If

        If gCRecipe.TempName = "" Then
            MsgBox("No Temperature File", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If Not gTempDB.ContainsKey(gCRecipe.TempName) Then
            MsgBox("No Temperature File", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '溫控器寫入SV參數
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.B1, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.B2, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.B3, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.B4, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.B5, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.B6, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        WetcoConveyor.mGlobalPool.SV = gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue

        'hot plate設定
        Dim hpB As WetcoConveyor.HotPlate

        hpB.HotPlate1 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateB1).Enabled
        hpB.HotPlate2 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateB2).Enabled
        hpB.HotPlate3 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateB3).Enabled
        hpB.HotPlate4 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateB4).Enabled
        hpB.HotPlate5 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateB5).Enabled
        hpB.HotPlate6 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateB6).Enabled
        WetcoConveyor.Unit.B_SetHotPlate(hpB)
    End Sub

    ''' <summary>
    ''' Map data 匯出成 ASE 格式 map file
    ''' </summary>
    ''' <param name="machineNo"></param>
    ''' <param name="path"></param>
    Public Function DataOutputAseMap(ByVal machineNo As enmMachineStation, ByVal path As String) As Boolean
        Dim mStageNo1 As enmStage
        Dim mStageNo2 As enmStage

        Try
            Select Case machineNo
                Case enmMachineStation.MachineA
                    mStageNo1 = enmStage.No1
                    mStageNo2 = enmStage.No2

                Case enmMachineStation.MachineB
                    mStageNo1 = enmStage.No3
                    mStageNo2 = enmStage.No4
            End Select

            '[Note]:合併NodeToMap裡的Node, 取得總陣列大小
            Dim maxColumn As Integer = 1000
            Dim maxRow As Integer = 1000
            Dim trayArray(maxColumn, maxRow) As Integer

            If (gCRecipe.NodeToMap(mStageNo1) IsNot Nothing) Then
                For Each node2Map In gCRecipe.NodeToMap(mStageNo1)
                    If gCRecipe.Node(mStageNo1).ContainsKey(node2Map) Then
                        Dim x As Integer = gCRecipe.Node(mStageNo1)(node2Map).NodeStartingX - 1
                        Dim y As Integer = gCRecipe.Node(mStageNo1)(node2Map).NodeStartingY - 1
                        Dim nodeArray As CMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(mStageNo1)(node2Map).Array)
                        Dim column As Integer = nodeArray.GetMemoryCountX
                        Dim row As Integer = nodeArray.GetMemoryCountY
                        For c = 0 To column - 1
                            For r = 0 To row - 1
                                trayArray(x + c, y + r) += 1
                            Next
                        Next
                    End If
                Next
            End If

            If (gCRecipe.NodeToMap(mStageNo2) IsNot Nothing) Then
                For Each node2Map In gCRecipe.NodeToMap(mStageNo2)
                    If gCRecipe.Node(mStageNo2).ContainsKey(node2Map) Then
                        Dim x As Integer = gCRecipe.Node(mStageNo2)(node2Map).NodeStartingX - 1
                        Dim y As Integer = gCRecipe.Node(mStageNo2)(node2Map).NodeStartingY - 1
                        Dim nodeArray As CMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(mStageNo2)(node2Map).Array)
                        Dim column As Integer = nodeArray.GetMemoryCountX
                        Dim row As Integer = nodeArray.GetMemoryCountY
                        For c = 0 To column - 1
                            For r = 0 To row - 1
                                trayArray(x + c, y + r) += 1
                            Next
                        Next
                    End If
                Next
            End If

            Dim nodeColumn As Integer
            For i = 0 To maxColumn
                If (trayArray(i, 0) <> 1) Then
                    nodeColumn = i
                    Exit For
                End If
            Next

            Dim nodeRow As Integer
            For i = 0 To maxRow
                If (trayArray(0, i) <> 1) Then
                    nodeRow = i
                    Exit For
                End If
            Next

            '[Note]:比對Map與Node List陣列大小
            If ((gMapData(machineNo).Substrates(0).Columns <> nodeColumn) Or (gMapData(machineNo).Substrates(0).Rows <> nodeRow)) Then
                Return False
            End If

            '[Note]:檢查Node陣列是否為一個完整的矩形陣列,並排除重複(value > 1)或遺漏(value = 0)的地方
            For c = 0 To nodeColumn - 1
                For r = 0 To nodeRow - 1
                    If (trayArray(c, r) <> 1) Then
                        Return False
                    End If
                Next
            Next

            If (gCRecipe.NodeToMap(mStageNo1) IsNot Nothing) Then
                '[Note]:將StageMap丟入MappingData
                For Each node In gCRecipe.NodeToMap(mStageNo1)
                    If StageMapCoverToMapData(gMapData(machineNo), gStageMap(mStageNo1).Node(node), gCRecipe.Node(mStageNo1)(node)) = False Then
                        Return False
                    End If
                Next
            End If
            If (gCRecipe.NodeToMap(mStageNo2) IsNot Nothing) Then
                '[Note]:將StageMap丟入MappingData
                For Each node In gCRecipe.NodeToMap(mStageNo2)
                    If StageMapCoverToMapData(gMapData(machineNo), gStageMap(mStageNo2).Node(node), gCRecipe.Node(mStageNo2)(node)) = False Then
                        Return False
                    End If
                Next
            End If

            '[Note]:旋轉Map陣列, 將Notch方向轉至下方
            gMapData(machineNo).ResetMapNotch(clsMapData.enmDirection.Bottom)

            '輸出ASE map
            If (gMapData(machineNo).OutputAseMap(path) = False) Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 匯出 Rerun Map 檔案
    ''' </summary>
    ''' <param name="machineNo"></param>
    ''' <param name="path"></param>
    Public Function OutputRerunMap(ByVal machineNo As enmMachineStation, ByVal path As String) As Boolean
        Dim mStageNo1 As enmStage
        Dim mStageNo2 As enmStage
        Dim mapData As New MapData.clsMapData

        Try
            Select Case machineNo
                Case enmMachineStation.MachineA
                    mStageNo1 = enmStage.No1
                    mStageNo2 = enmStage.No2

                Case enmMachineStation.MachineB
                    mStageNo1 = enmStage.No3
                    mStageNo2 = enmStage.No4
            End Select

            '[Note]:合併NodeToMap裡的Node, 取得總陣列大小
            Dim maxColumn As Integer = 1000
            Dim maxRow As Integer = 1000
            Dim trayArray(maxColumn, maxRow) As Integer

            If (gCRecipe.NodeToMap(mStageNo1) IsNot Nothing) Then
                For Each node2Map In gCRecipe.NodeToMap(mStageNo1)
                    If gCRecipe.Node(mStageNo1).ContainsKey(node2Map) Then
                        Dim x As Integer = gCRecipe.Node(mStageNo1)(node2Map).NodeStartingX - 1
                        Dim y As Integer = gCRecipe.Node(mStageNo1)(node2Map).NodeStartingY - 1
                        Dim nodeArray As CMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(mStageNo1)(node2Map).Array)
                        Dim column As Integer = nodeArray.GetMemoryCountX
                        Dim row As Integer = nodeArray.GetMemoryCountY
                        For c = 0 To column - 1
                            For r = 0 To row - 1
                                trayArray(x + c, y + r) += 1
                            Next
                        Next
                    End If
                Next
            End If

            If (gCRecipe.NodeToMap(mStageNo2) IsNot Nothing) Then
                For Each node2Map In gCRecipe.NodeToMap(mStageNo2)
                    If gCRecipe.Node(mStageNo2).ContainsKey(node2Map) Then
                        Dim x As Integer = gCRecipe.Node(mStageNo2)(node2Map).NodeStartingX - 1
                        Dim y As Integer = gCRecipe.Node(mStageNo2)(node2Map).NodeStartingY - 1
                        Dim nodeArray As CMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(mStageNo2)(node2Map).Array)
                        Dim column As Integer = nodeArray.GetMemoryCountX
                        Dim row As Integer = nodeArray.GetMemoryCountY
                        For c = 0 To column - 1
                            For r = 0 To row - 1
                                trayArray(x + c, y + r) += 1
                            Next
                        Next
                    End If
                Next
            End If

            Dim nodeColumn As Integer
            For i = 0 To maxColumn
                If (trayArray(i, 0) <> 1) Then
                    nodeColumn = i
                    Exit For
                End If
            Next

            Dim nodeRow As Integer
            For i = 0 To maxRow
                If (trayArray(0, i) <> 1) Then
                    nodeRow = i
                    Exit For
                End If
            Next

            ReDim mapData.Substrates(0)
            mapData.Substrates(0) = New Substrate
            ReDim mapData.Substrates(0).DieArray(nodeColumn - 1, nodeRow - 1)
            For c As Integer = 0 To nodeColumn - 1
                For r As Integer = 0 To nodeRow - 1
                    mapData.Substrates(0).DieArray(c, r) = New Die
                Next
            Next

            '[Note]:比對Map與Node List陣列大小
            If ((mapData.Substrates(0).Columns <> nodeColumn) Or (mapData.Substrates(0).Rows <> nodeRow)) Then
                Return False
            End If

            '[Note]:檢查Node陣列是否為一個完整的矩形陣列,並排除重複(value > 1)或遺漏(value = 0)的地方
            For c = 0 To nodeColumn - 1
                For r = 0 To nodeRow - 1
                    If (trayArray(c, r) <> 1) Then
                        Return False
                    End If
                Next
            Next

            If (gCRecipe.NodeToMap(mStageNo1) IsNot Nothing) Then
                '[Note]:將StageMap丟入MappingData
                For Each node In gCRecipe.NodeToMap(mStageNo1)
                    If StageMapCoverToMapData(mapData, gStageMap(mStageNo1).Node(node), gCRecipe.Node(mStageNo1)(node)) = False Then
                        Return False
                    End If
                Next
            End If

            If (gCRecipe.NodeToMap(mStageNo2) IsNot Nothing) Then
                '[Note]:將StageMap丟入MappingData
                For Each node In gCRecipe.NodeToMap(mStageNo2)
                    If StageMapCoverToMapData(mapData, gStageMap(mStageNo2).Node(node), gCRecipe.Node(mStageNo2)(node)) = False Then
                        Return False
                    End If
                Next
            End If

            '[Note]:旋轉Map陣列, 將Notch方向轉至下方
            mapData.ResetMapNotch(clsMapData.enmDirection.Bottom)

            '輸出Rerun Map
            If (mapData.OutputdRerunMap(path) = False) Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 轉換Tilt位置為角度 顯示用
    ''' </summary>
    ''' <param name="dTiltpos"></param>
    ''' <param name="iDriverPrm"></param>
    ''' <returns></returns>
    Public Function ConverTiltAngle(ByVal dTiltpos As Double, Optional ByVal iDriverPrm As Integer = 1) As Double
        Dim tiltangle As Double
        'driver PRM35 設定為1(1倍)時 欲讓DD馬達轉90度,須送出90/360 X 540672 = 135618
        'driver PRM35 設定為5(16倍)時 欲讓DD馬達轉90度,須送出135618/16 = 8477
        dTiltpos = dTiltpos * 1000
        tiltangle = 540672 / (Math.Pow(2, (iDriverPrm - 1)))
        tiltangle = (360 / tiltangle) * dTiltpos
        Return tiltangle

    End Function
#End Region
    ''' <summary>
    ''' Check LoadRecipe Status
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function CheckLoadRecipeStatus() As Boolean
        Dim mIsJetValveDBExit As Boolean

        '[Note]:沒有檔案
        If gCRecipe.strName = "" Then
            Return True
        End If

        '[說明]:硬體檢查
        Dim str As String = ""
        For mStageNo As enmStage = enmStage.No1 To gSSystemParameter.StageCount - 1
            For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                For mI As Integer = 0 To gJetValveDB.Count - 1
                    If gCRecipe.StageParts(mStageNo).ValveName(mValveNo) = gJetValveDB.Keys(mI) Then
                        Select Case gSSystemParameter.StageParts.ValveData(mStageNo).ValveType(mValveNo)
                            Case enmValveType.Jet
                                If gJetValveDB.ContainsKey(gJetValveDB.Keys(mI)) = True Then
                                    If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel <> gSSystemParameter.StageParts.ValveData(mStageNo).JetValve(mValveNo) Then
                                        str = "ValveTypeModel is not correspond"
                                        MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                        Return False
                                    End If
                                End If

                            Case enmValveType.Auger
                                str = "ValveTypeModel is not correspond"
                                MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Return False

                        End Select
                    End If
                Next
                If gCRecipe.StageParts(mStageNo).FlowRateName(mValveNo) = "" Or gCRecipe.StageParts(mStageNo).PurgeName(mValveNo) = "" Then
                    str = "Please Check StageNo" & mStageNo & " ValveNo" & (mValveNo + 1) & " FlowRateName or PurgeName"
                    MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Return False
                End If
            Next
        Next
        '[說明]:LoadRecipe完 Send 設定溫度給Loader & Unloader  20161010
        If gCRecipe.TempName <> "" Then
            If gTempDB.ContainsKey(gCRecipe.TempName) = True Then
                Dim LoaderTemNum As Double = gTempDB(gCRecipe.TempName).TempParam(enmTemp.Loader).SetValue
                Dim UnLoaderTemNum As Double = gTempDB(gCRecipe.TempName).TempParam(enmTemp.Unloader).SetValue

                '[Note]:若沒有啟用加熱功能，則不須跳訊息列通知 mobary+ 2016.11.12
                If gTempDB(gCRecipe.TempName).TempParam(enmTemp.Loader).Enabled = CheckState.Checked Then
                    If LoaderTemNum > 0 Then
                        If (mGlobalPool.cls800AQ_LUL.SetTargetTemp(LoaderTemNum, cls800AQLul.enmDevice.Loader) = False) Then
                            '進料端溫控設定失敗
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2054002))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2054002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Return False
                        End If
                    Else
                        '進料端溫控設定為0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2054004))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2054004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Return False
                    End If
                End If
                If gTempDB(gCRecipe.TempName).TempParam(enmTemp.Unloader).Enabled = CheckState.Checked Then
                    If UnLoaderTemNum > 0 Then
                        If (mGlobalPool.cls800AQ_LUL.SetTargetTemp(UnLoaderTemNum, cls800AQLul.enmDevice.Unloader) = False) Then
                            '退料端溫控設定失敗
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2054003))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2054003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Return False
                        End If
                    Else
                        '退料端溫控設定為0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2054005))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2054005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Return False
                    End If
                End If

                SetAHotPlate()
                SetBHotPlate()
            Else
                '溫控檔不存在
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000032))
                MsgBox(gMsgHandler.GetMessage(Warn_3000032), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If
        End If

        '[說明]:檢查JetDB對應參數是否設定正常 

        If gTempDB.ContainsKey(gCRecipe.TempName) = False Then
            str = "Please Check  TempName "
            MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If

        For mStageNo As enmStage = enmStage.No1 To gSSystemParameter.StageCount - 1
            For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                mIsJetValveDBExit = False
                For mI As Integer = 0 To gJetValveDB.Count - 1
                    If gCRecipe.StageParts(mStageNo).ValveName(mValveNo) = gJetValveDB.Keys(mI) Then
                        Select Case gSSystemParameter.StageParts.ValveData(mStageNo).ValveType(mValveNo)
                            Case enmValveType.Jet
                                '[說明]: Check Valva X 的設定與gSSystemParameter.ValveData Data 及跳出警告視窗
                                If gJetValveDB.ContainsKey(gJetValveDB.Keys(mI)) = True Then
                                    If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel <> gSSystemParameter.StageParts.ValveData(mStageNo).JetValve(mValveNo) Or gJetValveDB.ContainsKey(gCRecipe.StageParts(mStageNo).ValveName(mValveNo)) = False Then
                                        str = "ValveType is not Chose!!Please enter Process Parameter chose Valve"
                                        MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                        Return False
                                    Else
                                        mIsJetValveDBExit = True
                                    End If
                                End If

                            Case enmValveType.Auger
                                str = "ValveType is not Chose!!Please enter Process Parameter chose Valve"
                                MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Return False

                        End Select
                    End If
                Next
                If mIsJetValveDBExit = False Then
                    str = "ValveType is not Exit!!Please enter Process Parameter chose Valve"
                    MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Return False
                End If


                If gPurgeDB.ContainsKey(gCRecipe.StageParts(mStageNo).PurgeName(mValveNo)) = False Then
                    str = "Please Check StageNo" & mStageNo & " ValveNo" & (mValveNo + 1) & " PurgeName"
                    MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Return False
                End If
                If gPasteDataBase.ContainsKey(gCRecipe.StageParts(mStageNo).PasteName(mValveNo)) = False Then
                    str = "Please Check StageNo" & mStageNo & " ValveNo" & (mValveNo + 1) & " PasteName"
                    MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Return False
                End If
                If gFlowRateDB.ContainsKey(gCRecipe.StageParts(mStageNo).FlowRateName(mValveNo)) = False Then
                    str = "Please Check StageNo" & mStageNo & " ValveNo" & (mValveNo + 1) & " FlowRateName"
                    MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Return False
                End If

                '20170520
                gCRecipe.StageParts(mStageNo).AverageWeightPerDot(mValveNo) = gFlowRateDB(gCRecipe.StageParts(mStageNo).FlowRateName(mValveNo)).WeighingAverageWeightDot

                '[說明]:檢查是Weight or Complex
                If gCRecipe.RunType = eWeightControlType.Weight Then 'Or gCRecipe.RunType = eWeightControlType.Complex Then
                    '[說明]:檢查(微量天平資料平均單點重量) 20170505
                    If gFlowRateDB(gCRecipe.StageParts(mStageNo).FlowRateName(mValveNo)).WeighingAverageWeightDot < gFlowRateDB(gCRecipe.StageParts(mStageNo).FlowRateName(mValveNo)).WeighingWeightDotMin Or
                       gFlowRateDB(gCRecipe.StageParts(mStageNo).FlowRateName(mValveNo)).WeighingAverageWeightDot > gFlowRateDB(gCRecipe.StageParts(mStageNo).FlowRateName(mValveNo)).WeighingWeightDotMax Then

                        Select Case gSSystemParameter.LanguageType
                            Case enmLanguageType.eEnglish
                                str = "DotWeight < WeighingWeightDotMin or DotWeight > WeighingWeightDotMax!!Please re-weigh the weight or adjust weight the maximum and minimum single"
                            Case enmLanguageType.eSimplifiedChinese
                                str = "单点重量小于最小或单点重量大于最大!!请重作秤重动作或调整单颗最大最小值!!"
                            Case enmLanguageType.eTraditionalChinese
                                str = "單點重量小於最小或單點重量大於最大!!請重作秤重動作或調整單顆最大最小值!!"
                        End Select
                        MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Return False
                    End If
                End If


            Next
        Next

        '2017_0515_Toby1
        Select Case gSSystemParameter.MeasureType
            Case enmMeasureType.Contact
                If gCRecipe.LaserFixMode = eHeightModel.Laser_NonOnFly Or gCRecipe.LaserFixMode = eHeightModel.Laser_OnFly Then
                    MessageBox.Show("Please select Measure Z Height Mode")
                    Return False
                End If

            Case enmMeasureType.Laser
                If gCRecipe.LaserFixMode = eHeightModel.Contact Then
                    MessageBox.Show("Please select Measure Z Height Mode")
                    Return False
                End If
        End Select

        Return True
    End Function

End Module
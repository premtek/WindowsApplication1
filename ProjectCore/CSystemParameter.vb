''' <summary>
''' SystemParameter
''' </summary>
''' <remarks></remarks>
Public Class CSystemParameter
    ''' <summary>CCD影像存檔模式</summary>
    ''' <remarks></remarks>
    Public CCDImageSaveMode As eCCDImageProcess
    ''' <summary>CCD影像存檔資料夾路徑</summary>
    ''' <remarks></remarks>
    Public CCDImageFolderPath As String
    ''' <summary> 是否啟用AOI模組化介面 </summary>
    ''' <remarks></remarks>
    Public CCDAlignModuleEnable As Boolean
    ''' <summary>製程完成Map data存檔資料夾路徑</summary>
    ''' <remarks></remarks>
    Public RerunDataFolderPath As String

    ''' <summary>與正交(Orthogonal, 90度)的角度差
    ''' </summary>
    ''' <remarks></remarks>
    Public OrthgonalAngleDiff(enmStage.Max) As Decimal

#Region "機型型別"
    ''' <summary>[機台軟體運作模式]</summary>
    ''' <remarks></remarks>
    Public RunMode As enmRunMode
    ''' <summary>[機台類別]</summary>
    ''' <remarks></remarks>
    Public MachineType As enmMachineType
#End Region


#Region "硬體配置"
    ''' <summary>[一組Stage配幾個Valve]</summary>
    ''' <remarks></remarks>
    Public StageUseValveCount As eMechanismModule
    ''' <summary>[Stage組數]</summary>
    ''' <remarks></remarks>
    Public StageCount As Integer
    ''' <summary>[CCD與Z軸之關係(CCD隨Z軸移動 或CCD不隨Z軸移動)]</summary>
    ''' <remarks></remarks>
    Public CCDModuleType As enmCCDModule
    ''' <summary>[測高設備]</summary>
    ''' <remarks></remarks>
    Public MeasureType As enmMeasureType
    ''' <summary> [飛拍設備]</summary>
    ''' <remarks></remarks>
    Public CCDOnFlyEnable As Boolean
    ''' <summary>[多閥同動點膠]</summary>
    ''' <remarks></remarks>
    Public MultiDispenseEnable As Boolean
#End Region

#Region "軟硬體配置選項"
    ''' <summary>[平台最大編號]</summary>
    ''' <remarks></remarks>
    Public StageMax As Integer
    ''' <summary>[機台最大編號]</summary>
    ''' <remarks></remarks>
    Public MachineMax As Integer
    ''' <summary>[平台最大編號(周邊動作)]</summary>
    ''' <remarks></remarks>
    Public SubDispMax As Integer
    ''' <summary>[平台最大編號(監控)]</summary>
    ''' <remarks></remarks>
    Public MonitorDispMax As Integer
    ''' <summary>[流道最大編號]</summary>
    ''' <remarks></remarks>
    Public ConveyorMax As Integer
    ''' <summary>使用哪個Conveyor做生產</summary>
    ''' <remarks></remarks>
    Public ConveyorModel As eConveyorModel
    ''' <summary>[座標關係式]</summary>
    ''' <remarks></remarks>
    Public CoordType As enmCoordinateRelationType
    ''' <summary>[語系]</summary>
    ''' <remarks></remarks>
    Public LanguageType As enmLanguageType

    ''' <summary>[使用者權限設定]</summary>
    ''' <remarks></remarks>
    Public UserAuth(enmUserAuthItem.Max) As enmUserLevel
    ''' <summary>生產開始時間(不存檔)</summary>
    ''' <remarks></remarks>
    Public AutoRunStartTime As DateTime
    ''' <summary>生產結束時間(不存檔)  20161205</summary>
    ''' <remarks></remarks>
    Public AutoRunEndTime As DateTime
    ''' <summary>生產開始時間 By A,B機(不存檔)</summary>
    ''' <remarks></remarks>
    Public AutoRunMachineStartTime(1) As DateTime
    ''' <summary>[Motion Tolerance (允許多少誤差內視為同一點 單位:mm) or 單位向量，但代表的容許誤差為0.01(1%)]</summary>
    ''' <remarks></remarks>
    Public MotionTolerance As Decimal
    ''' <summary>[Trigger Board Tolerance(單位:mm)]</summary>
    ''' <remarks></remarks>
    Public TriggerTolerance As Decimal
    ''' <summary>[Precision Tolerance(單位:mm)]</summary>
    ''' <remarks></remarks>
    Public PrecisionTolerance As Decimal
    ''' <summary>[內定Trigger Board Tolerance(目前只能在SysParam.ini修改,初始值0.2)(單位:mm)]</summary>
    ''' <remarks></remarks>
    Public RanTriggerTolerance As Decimal

    ''' <summary>[氣壓Tolerance(單位:Mpa)]</summary>
    ''' <remarks></remarks>
    Public PressureTolerance As Decimal
    ''' <summary>[溫度Tolerance(單位:℃)]</summary>
    ''' <remarks></remarks>
    Public TemperatureTolerance As Decimal
    ''' <summary>手動移動用最大速度</summary>
    ''' <remarks></remarks>
    Public ManualVelHigh As Decimal
    ''' <summary>[是否讀取Map Data做比對; 0:關閉,1:開啟(自動+手動),2:自動匯入,3:手動匯入]</summary>
    ''' <remarks></remarks>
    Public IsCompareWithMapData As Integer
    ''' <summary>[是否手動修改Map]</summary> 'Toby Add_20170103
    ''' <remarks></remarks>
    Public IsManualMap As Boolean
    ''' <summary>Log資料夾路徑</summary>
    ''' <remarks></remarks>
    Public LogFolderPath As String
    ''' <summary>機台名稱</summary>
    ''' <remarks></remarks>
    Public MachineID As String
    ''' <summary>[是否開啟天平計數功能A]</summary>
    ''' <remarks></remarks>
    Public EnableWeightMeasureA As Boolean
    ''' <summary>[紀錄目前天平重量=>何時該換A]</summary>
    ''' <remarks></remarks>
    Public WeightMeasureA As Double
    ''' <summary>[是否開啟天平計數功能B]</summary>
    ''' <remarks></remarks>
    Public EnableWeightMeasureB As Boolean
    ''' <summary>[紀錄目前天平重量=>何時該換B]</summary>
    ''' <remarks></remarks>
    Public WeightMeasureB As Double
    ''' <summary>[設定重量]</summary>
    ''' <remarks></remarks>
    Public WeightSet As Double
    ''' <summary>[設定補正次數]</summary>
    ''' <remarks></remarks>
    Public CorrectionNum As Double
    ''' <summary>[設定補正重量]</summary>
    ''' <remarks></remarks>
    Public NonCorrection As Boolean
    ''' <summary>[設定補正重量]</summary>
    ''' <remarks></remarks>
    Public Correction As Boolean
    ''' <summary>[設定補正重量USL]</summary>
    ''' <remarks></remarks>
    Public CorrectionUSL As Double
    ''' <summary>[設定補正重量LSL]</summary>
    ''' <remarks></remarks>
    Public CorrectionLSL As Double
    ''' <summary>[設定補正重量WeightUpper]</summary>
    ''' <remarks></remarks>
    Public CorrectionWeightUpper As Double
    ''' <summary>[設定補正重量WeightLower]</summary>
    ''' <remarks></remarks>
    Public CorrectionWeightLower As Double
    ''' <summary>[是否開啟Initial Hot Plate]</summary>
    ''' <remarks></remarks>
    Public EnableInitialHotPlate As Boolean
    ''' <summary>[微量天平秤重counter計數器微量天平資料]</summary>               
    ''' <remarks></remarks>
    Public SystemWeighingDataCounter As Integer


    '20171001
    ''' <summary>[閥空跑路徑]</summary>
    ''' <remarks></remarks>
    Public EnableDryRun As Boolean


    '20171005
    ''' <summary>[鬼影跑法助跑路徑倍數]</summary>
    ''' <remarks></remarks>
    Public PathMultiple As Double

    '20171005
    ''' <summary>[視覺膠路]</summary>
    ''' <remarks></remarks>
    Public EnableVideoRun As Boolean

    ''' <summary>
    ''' Purge and Weight 動作使用的角度
    ''' </summary>
    ''' <remarks></remarks>
    Public Stage1TiltAngle As Decimal
    Public Stage2TiltAngle As Decimal
    Public Stage3TiltAngle As Decimal
    Public Stage4TiltAngle As Decimal

    ''' <summary>
    ''' 流道皮帶 Load 方向
    ''' </summary>
    ''' <remarks>true:正轉; false:反轉</remarks>
    Public LoadDirection As Boolean
    ''' <summary>
    ''' 流道皮帶 Unload 方向
    ''' </summary>
    ''' <remarks>true:正轉; false:反轉</remarks>
    Public UnloadDirection As Boolean

    ''' <summary>非點膠步驟速度上限(mm/s)</summary>
    ''' <remarks></remarks>
    Public MaxCrossStepVelocity As Decimal
    ''' <summary>跨元件速度上限(mm/s)</summary>
    ''' <remarks></remarks>
    Public MaxCrossDeviceVelocity As Decimal
    ''' <summary>
    ''' 最大點膠速度上限(mm/s) 主要用途是將非點膠, 跨元件速度清掉, 避免點膠速度被限制.
    ''' </summary>
    ''' <remarks></remarks>
    Public MaxDispVelocity As Decimal
    ''' <summary>非點膠/跨元件的等速段時間(Sec)</summary>
    ''' <remarks></remarks>
    Public CrossVerticalTime As Decimal

    '20171114
    ''' <summary>紀錄EMSReset點擊紀錄(目的EMS發生後不點擊重置無法初始化)</summary>
    ''' <remarks></remarks>
    Public EMSResetButton As Boolean


#End Region

#Region "陣列化硬體連線設定"
    ''' <summary>流道連線設定 (0):第一軌 (1):第二軌</summary>
    ''' <remarks></remarks>
    Public sConveyor(enmConveyor.Max) As sConveyorConnectionParameter
#End Region

#Region "待重新定義"
    ''' <summary>[是否忽略對傳送帶交握訊號]</summary>
    ''' <remarks></remarks>
    Public IsBypassConveyor As Boolean
    ''' <summary>[忽略CCD檢測異常之訊號(空跑盲打用)]</summary>
    ''' <remarks></remarks>
    Public IsBypassCCD As Boolean
    ''' <summary>忽略LUL</summary>
    ''' <remarks></remarks>
    Public PassLUL As Boolean
#End Region

#Region "CCD比例"
    ''' <summary>X方向Pixel對X方向mm的比例</summary>
    ''' <remarks></remarks>
    Public CCDScaleX2X(enmCCD.ConstMax) As Decimal
    ''' <summary>X方向Pixel對Y方向mm的比例</summary>
    ''' <remarks></remarks>
    Public CCDScaleX2Y(enmCCD.ConstMax) As Decimal
    ''' <summary>Y方向Pixel對X方向mm的比例</summary>
    ''' <remarks></remarks>
    Public CCDScaleY2X(enmCCD.ConstMax) As Decimal
    ''' <summary>Y方向Pixel對Y方向mm的比例</summary>
    ''' <remarks></remarks>
    Public CCDScaleY2Y(enmCCD.ConstMax) As Decimal

    ''' <summary> 校正位置X </summary>
    ''' <remarks></remarks>
    Public CCDPosX(enmCCD.ConstMax) As Decimal
    ''' <summary> 校正位置Y </summary>
    ''' <remarks></remarks>
    Public CCDPosY(enmCCD.ConstMax) As Decimal
    ''' <summary> 校正位置Z </summary>
    ''' <remarks></remarks>
    Public CCDPosZ(enmCCD.ConstMax) As Decimal

    ''' <summary>治具穩定時間</summary>
    ''' <remarks></remarks>
    Public CCDStableTime(enmCCD.ConstMax) As Decimal

    ''' <summary>讀取CCD比例 mm/Pixel</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadCCDScale(ByVal strFileName As String) As Boolean
        Dim strSection As String
        For i As Integer = enmCCD.CCD1 To enmCCD.Max
            strSection = "CCD" & (i + 1).ToString
            With Me
                .CCDPosX(i) = Val(ReadIniString(strSection, "CCDPosX", strFileName, 0))
                .CCDPosY(i) = Val(ReadIniString(strSection, "CCDPosY", strFileName, 0))
                .CCDPosZ(i) = Val(ReadIniString(strSection, "CCDPosZ", strFileName, 0))
                .CCDStableTime(i) = Val(ReadIniString(strSection, "CCDStableTime", strFileName, 1000))
                .CCDScaleX2X(i) = Val(ReadIniString(strSection, "ScaleX2X", strFileName, -0.01))
                .CCDScaleX2Y(i) = Val(ReadIniString(strSection, "ScaleX2Y", strFileName, 0.0))
                .CCDScaleY2X(i) = Val(ReadIniString(strSection, "ScaleY2X", strFileName, 0))
                .CCDScaleY2Y(i) = Val(ReadIniString(strSection, "ScaleY2Y", strFileName, -0.01))
            End With
        Next
        Return True
    End Function

    ''' <summary>儲存CCD比例 mm/Pixel</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCCDSCale(ByVal strFileName As String) As Boolean
        Dim strSection As String
        For i As Integer = enmCCD.CCD1 To enmCCD.Max
            strSection = "CCD" & (i + 1).ToString
            With Me
                Call SaveIniString(strSection, "CCDPosX", .CCDPosX(i), strFileName)
                Call SaveIniString(strSection, "CCDPosY", .CCDPosY(i), strFileName)
                Call SaveIniString(strSection, "CCDPosZ", .CCDPosZ(i), strFileName)
                Call SaveIniString(strSection, "CCDStableTime", .CCDStableTime(i), strFileName)
                Call SaveIniString(strSection, "ScaleX2X", .CCDScaleX2X(i), strFileName)
                Call SaveIniString(strSection, "ScaleX2Y", .CCDScaleX2Y(i), strFileName)
                Call SaveIniString(strSection, "ScaleY2X", .CCDScaleY2X(i), strFileName)
                Call SaveIniString(strSection, "ScaleY2Y", .CCDScaleY2Y(i), strFileName)
            End With
        Next
        Return True
    End Function
#End Region


#Region "CCD標靶"


    Public CCDTargetDataList As List(Of sCCDTargetData) = New List(Of sCCDTargetData)

    ''' <summary>讀取CCD標靶資料</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadCCDTargetData(ByVal strFileName As String) As Boolean
        Dim strSection As String
        strSection = "CCDTargetData"
        Dim mStepCount = Val(ReadIniString(strSection, "StepCount", strFileName, 0))
        For i As Integer = 0 To mStepCount - 1
            strSection = "CCDTargetData" & (i + 1).ToString
            Dim tmpData As sCCDTargetData
            tmpData.CCDTargetType = Val(ReadIniString(strSection, "CCDTargetType", strFileName, 0))
            tmpData.CCDTargetColor = Val(ReadIniString(strSection, "CCDTargetColor", strFileName, 0))
            tmpData.Radius = Val(ReadIniString(strSection, "Radius", strFileName, 0))
            tmpData.Width = Val(ReadIniString(strSection, "Width", strFileName, 0))
            tmpData.Height = Val(ReadIniString(strSection, "Height", strFileName, 0))
            CCDTargetDataList.Add(tmpData)
        Next
        Return True
    End Function

    ''' <summary>儲存CCD標靶資料</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCCDTargetData(ByVal strFileName As String) As Boolean
        Dim strSection As String
        strSection = "CCDTargetData"
        Call SaveIniString(strSection, "StepCount", CCDTargetDataList.Count, strFileName)
        For i As Integer = 0 To CCDTargetDataList.Count - 1
            strSection = "CCDTargetData" & (i + 1).ToString
            With Me
                Call SaveIniString(strSection, "CCDTargetType", CCDTargetDataList(i).CCDTargetType, strFileName)
                Call SaveIniString(strSection, "CCDTargetColor", CCDTargetDataList(i).CCDTargetColor, strFileName)
                Call SaveIniString(strSection, "Radius", CCDTargetDataList(i).Radius, strFileName)
                Call SaveIniString(strSection, "Width", CCDTargetDataList(i).Width, strFileName)
                Call SaveIniString(strSection, "Height", CCDTargetDataList(i).Height, strFileName)
            End With
        Next
        Return True
    End Function
#End Region


#Region "逾時保護設定值五階段"
    ''' <summary>逾時保護(預設1S)</summary>
    ''' <remarks></remarks>
    Public TimeOut1 As Decimal
    ''' <summary>逾時保護(預設5S)</summary>
    ''' <remarks></remarks>
    Public TimeOut2 As Decimal
    ''' <summary>逾時保護(預設10S)</summary>
    ''' <remarks></remarks>
    Public TimeOut3 As Decimal
    ''' <summary>逾時保護(預設20S)</summary>
    ''' <remarks></remarks>
    Public TimeOut4 As Decimal
    ''' <summary>逾時保護(預設30S)</summary>
    ''' <remarks></remarks>
    Public TimeOut5 As Decimal
    ''' <summary>逾時保護(預設1800S(30min))</summary>
    ''' <remarks></remarks>
    Public TimeOut6 As Decimal
    ''' <summary>復歸逾時保護(預設60S)</summary>
    ''' <remarks></remarks>
    Public TimeOutHome As Decimal

    ''' <summary>UnLoad 230等待產品出片時間</summary>
    ''' <remarks></remarks>
    Public UnLoad230WaitProductOut As Decimal

#End Region
    ''' <summary>
    ''' 安全開門溫度(度C)
    ''' </summary>
    ''' <remarks></remarks>
    Public SafeTemperature As Decimal
    ''' <summary>閥最大溫度(度C)</summary>
    ''' <remarks></remarks>
    Public ValveMaxTemperature As Decimal
    ''' <summary>加熱板最大溫度(度C)</summary>
    ''' <remarks></remarks>
    Public HotplateMaxTemperature As Decimal
    ''' <summary>加熱板最小溫度(度C)</summary>
    ''' <remarks></remarks>
    Public HotplateMinTemperature As Decimal
    ''' <summary>
    ''' [CCD偵測的偏移值上限]
    ''' </summary>
    ''' <remarks></remarks>
    Public CCDOffsetLimit As Decimal
    ''' <summary>
    ''' [CCD偵測的旋轉值上限]
    ''' </summary>
    ''' <remarks></remarks>
    Public CCDAngelLimit As Decimal
    ''' <summary>[位置]</summary>
    ''' <remarks></remarks>
    Public Pos As New CPosition
    ''' <summary>[Stage下對應閥的資料]</summary>
    ''' <remarks></remarks>
    Public StageParts As New CParmStageParts
    ''' <summary>[紀錄點膠生產資訊(NG OK)]</summary>
    ''' <remarks></remarks>
    Public ProductState As SDispenserState
    ''' <summary>[安定時間]</summary>
    ''' <remarks></remarks>
    Public StableTime As New CStableTime
    ''' <summary>[機台安全位置資料]</summary>
    ''' <remarks></remarks>
    Public MachineSafeData As New List(Of SSaftPosData)
    ''' <summary>[是否走接續流程]</summary>
    ''' <remarks></remarks>
    Public IsContinueLastRun As Boolean
    ''' <summary>[入料完成後是否要跳出介面給使用者修改哪幾顆點膠or不點膠]</summary>
    ''' <remarks></remarks>
    Public IsStopToCheckDieStatus As Boolean

    '20170630
    ''' <summary>
    ''' 是否有清膠裝置
    ''' </summary>
    ''' <remarks></remarks>
    Public IsCleanDevice As Boolean

#Region "System Parameter"

    ''' <summary>讀取系統位置</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadSystemPos(ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "Pos"
        With Me.Pos
            .SafePosZ = CDbl(ReadIniString(strSection, "ZSafePos", fileName, 0))
            .ZUpPos = CDbl(ReadIniString(strSection, "ZUpPos", fileName, 0))
            .TiltSafePosZ = CDbl(ReadIniString(strSection, "TiltSafePosZ", fileName, 0))
            For i As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
                .DispStageCalibrationPosX(i) = CDecimal(ReadIniString(strSection, "DispStage" & (i + 1).ToString & "CalibrationPosX", fileName, 0))
                .DispStageCalibrationPosY(i) = CDecimal(ReadIniString(strSection, "DispStage" & (i + 1).ToString & "CalibrationPosY", fileName, 0))
            Next
        End With
        Return True
    End Function

    ''' <summary>儲存系統位置</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSystemPos(ByVal fileName As String) As Boolean

        Dim strSection As String
        strSection = "Pos"
        With Me.Pos
            Call SaveIniString(strSection, "ZSafePos", .SafePosZ, fileName)
            Call SaveIniString(strSection, "ZUpPos", .ZUpPos, fileName)
            Call SaveIniString(strSection, "TiltSafePosZ", .TiltSafePosZ, fileName)
            For i As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
                Call SaveIniString(strSection, "DispStage" & (i + 1).ToString & "CalibrationPosX", .DispStageCalibrationPosX(i), fileName)
                Call SaveIniString(strSection, "DispStage" & (i + 1).ToString & "CalibrationPosY", .DispStageCalibrationPosY(i), fileName)
                Call SaveIniString(strSection, "DispStage" & (i + 1).ToString & "PauseStopPosX", .PauseStopPos(i).PosX, fileName)
                Call SaveIniString(strSection, "DispStage" & (i + 1).ToString & "PauseStopPosX", .PauseStopPos(i).PosY, fileName)
                Call SaveIniString(strSection, "DispStage" & (i + 1).ToString & "PauseStopPosX", .PauseStopPos(i).PosZ, fileName)
                Call SaveIniString(strSection, "DispStage" & (i + 1).ToString & "PauseStopPosX", .PauseStopPos(i).PosA, fileName)
                Call SaveIniString(strSection, "DispStage" & (i + 1).ToString & "PauseStopPosX", .PauseStopPos(i).PosB, fileName)
                Call SaveIniString(strSection, "DispStage" & (i + 1).ToString & "PauseStopPosX", .PauseStopPos(i).PosC, fileName)
            Next
        End With
        Return True
    End Function

    ''' <summary>ReadSystemParameter 讀取系統參數</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadSystemParameter(ByVal fileName As String) As Boolean

        Dim strSection As String

        strSection = "WorkSize"
        With Me
            .LogFolderPath = ReadIniString(strSection, "LogFolderPath", fileName, "D:\PIIData\DataLog\")
            .MachineID = ReadIniString(strSection, "MachineID", fileName, "Untitled")
            .CoordType = Val(ReadIniString(strSection, "CoordType", fileName, 0))
            '20170214_Toby
            .AirPressureUnit = Val(ReadIniString(strSection, "AirPressureUnit", fileName, 0))
            .IsBypassConveyor = CBool(ReadIniString(strSection, "IsBypassConveyor", fileName, 0))  'True=-1  False=0
            '.blnPassCCD = Val(ReadIniString(strSection, "PassCCD", fileName, 0))    'True=-1  False=0
            .PassLUL = Val(ReadIniString(strSection, "PassLUL", fileName, 0))    'True=-1  False=0
            '[說明]:Pass CCD通常都是要打在玻璃片上，此時是無法量測TrimGap
            .LanguageType = CInt(ReadIniString(strSection, "LanguageType", fileName, 0))    '[0:繁中]
            .MotionTolerance = Val(ReadIniString(strSection, "MotionTolerance", fileName, 0.01))
            .TriggerTolerance = Val(ReadIniString(strSection, "TriggerTolerance", fileName, 0.4))
            .PrecisionTolerance = Val(ReadIniString(strSection, "PrecisionTolerance", fileName, 0.1))
            .RanTriggerTolerance = Val(ReadIniString(strSection, "RanTriggerTolerance", fileName, 0.2))
            .PressureTolerance = Val(ReadIniString(strSection, "PressureTolerance", fileName, 0.01))
            .TemperatureTolerance = Val(ReadIniString(strSection, "TemperatureTolerance", fileName, 2))
            .ConveyorModel = Val(ReadIniString(strSection, "ConveyorModel", fileName, 0))
            .IsCompareWithMapData = Val(ReadIniString(strSection, "IsCompareWithMapData", fileName, 0)) 'Asa add
            .IsManualMap = CBool(ReadIniString(strSection, "IsManualMap", fileName, 0))
            .TimeOut1 = 1000
            .TimeOut2 = 5000
            .TimeOut3 = 10000
            .TimeOut4 = 20000
            .TimeOut5 = 30000
            .TimeOut6 = 1800000
            .TimeOutHome = Val(ReadIniString(strSection, "TimeOutHome", fileName, 60000))

            .UnLoad230WaitProductOut = Val(ReadIniString(strSection, "UnLoad230WaitProdictOut", fileName, 3000))

            For i As Integer = 0 To enmStage.Max
                .OrthgonalAngleDiff(i) = Val(ReadIniString(strSection, "OrthgonalAngleDiff" + i.ToString(), fileName, 0))
            Next


            .ManualVelHigh = Val(ReadIniString(strSection, "ManualVelHigh", fileName, 60))
            .SafeTemperature = Val(ReadIniString(strSection, "SafeTemperature", fileName, 45))
            .ValveMaxTemperature = Val(ReadIniString(strSection, "ValveMaxTemperature", fileName, 200))
            .HotplateMaxTemperature = Val(ReadIniString(strSection, "HotplateMaxTemperature", fileName, 200))
            .HotplateMinTemperature = Val(ReadIniString(strSection, "HotplateMinTemperature", fileName, 10))
            .CCDOffsetLimit = CDec(ReadIniString(strSection, "CCDOffsetLimit", fileName, 2))
            .CCDAngelLimit = CDec(ReadIniString(strSection, "CCDAngelLimit", fileName, 2))
            .CCDImageSaveMode = CInt(ReadIniString(strSection, "CCDImageSaveMode", fileName, 0))
            .CCDImageFolderPath = ReadIniString(strSection, "CCDImageFolderPath", fileName, "D:\PIIData\Image\")
            .CCDAlignModuleEnable = CBool(ReadIniString(strSection, "CCDAlignModuleEnable", fileName, 1)) '[預設為啟用模組化]
            .EnableWeightMeasureA = CBool(ReadIniString(strSection, "EnableWeightMeasureA", fileName, 0))
            .WeightMeasureA = Val(ReadIniString(strSection, "WeightMeasureA", fileName, 0))
            .EnableWeightMeasureB = CBool(ReadIniString(strSection, "EnableWeightMeasureB", fileName, 0))
            .WeightMeasureB = Val(ReadIniString(strSection, "WeightMeasureB", fileName, 0))
            .CorrectionNum = Val(ReadIniString(strSection, "CorrectionSet", fileName, 0))
            .NonCorrection = CBool(ReadIniString(strSection, "NonCorrection", fileName, 0))
            .Correction = CBool(ReadIniString(strSection, "Correction", fileName, 0))
            .CorrectionUSL = Val(ReadIniString(strSection, "CorrectionUSL", fileName, 0))
            .CorrectionLSL = Val(ReadIniString(strSection, "CorrectionLSL", fileName, 0))
            .CorrectionWeightUpper = Val(ReadIniString(strSection, "CorrectionWeightUpper", fileName, 0))
            .CorrectionWeightLower = Val(ReadIniString(strSection, "CorrectionWeightLower", fileName, 0))
            .WeightSet = Val(ReadIniString(strSection, "WeightSet", fileName, 0))
            .SystemWeighingDataCounter = Val(ReadIniString(strSection, "SystemWeighingDataCounter", fileName, 1))
            .Stage1TiltAngle = Val(ReadIniString(strSection, "Stage1TiltAngle", fileName, 0))
            .Stage2TiltAngle = Val(ReadIniString(strSection, "Stage2TiltAngle", fileName, 0))
            .Stage3TiltAngle = Val(ReadIniString(strSection, "Stage3TiltAngle", fileName, 0))
            .Stage4TiltAngle = Val(ReadIniString(strSection, "Stage4TiltAngle", fileName, 0))
            .EnableInitialHotPlate = Val(ReadIniString(strSection, "EnableInitialHotPlate", fileName, 0))
            .IsContinueLastRun = Val(ReadIniString(strSection, "ContinueLastRun", fileName, 0))
            .LoadDirection = Val(ReadIniString(strSection, "CvLoadDirection", fileName, 0))
            .UnloadDirection = Val(ReadIniString(strSection, "CvUnloadDirection", fileName, 0))


            '20171005
            .PathMultiple = Val(ReadIniString(strSection, "PathMultiple", fileName, 1))

            '20171001
            .EnableDryRun = CBool(ReadIniString(strSection, "EnableDryRun", fileName, 0))

            'jimmy 20170630
            .IsCleanDevice = Val(ReadIniString(strSection, "CleanDevice", fileName, 0))

            .RerunDataFolderPath = ReadIniString(strSection, "MapDataFolderPath", fileName, "D:\\PIIData\\RerunData\\")

            If Not System.IO.Directory.Exists(.RerunDataFolderPath) Then '資料夾確保
                System.IO.Directory.CreateDirectory(.RerunDataFolderPath)
            End If

            '建立前先清空
            .MachineSafeData.Clear()
            '判斷機型，以建立安全位置保護資料
            Select Case .MachineType
                Case enmMachineType.DCSW_800AQ   '雙軌四閥，建立二組
                    Dim mMachineASafe As New SSaftPosData
                    strSection = "MachineA Safe Data"
                    mMachineASafe.Load(fileName, strSection)
                    .MachineSafeData.Add(mMachineASafe)

                    strSection = "MachineB Safe Data"
                    Dim mMachineBSafe = New SSaftPosData
                    mMachineBSafe.Load(fileName, strSection)
                    .MachineSafeData.Add(mMachineBSafe)

                Case enmMachineType.eDTS_2S2V   '單軌雙閥，只建立一組
                    Dim tempSafeData As New SSaftPosData
                    strSection = "MachineA Safe Data"
                    tempSafeData.Load(fileName, strSection)
                    .MachineSafeData.Add(tempSafeData)

                    '20171106
                Case enmMachineType.DCS_500AD   '單軌雙閥，只建立一組
                    Dim tempSafeData As New SSaftPosData
                    strSection = "MachineA Safe Data"
                    tempSafeData.Load(fileName, strSection)
                    .MachineSafeData.Add(tempSafeData)

                Case Else       '其餘不建立

            End Select
        End With
        Return True

ErrorHandler:  ' Error-handling routine.
        MsgBox(Err.GetException.ToString(), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        Err.Clear()
        Return False
    End Function

#Region "軸卡補償設定檔"
    Public Function LoadStageFile(ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "WorkSize"

        For i As Integer = 0 To enmStage.Max
            Me.StageFile(i) = ReadIniString(strSection, "Stage" & (i + 1).ToString & "File", fileName, "")
        Next
        Return True
    End Function

    Public Function SaveStageFile(ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "WorkSize"
        For i As Integer = 0 To enmStage.Max
            Call SaveIniString(strSection, "Stage" & (i + 1).ToString & "File", Me.StageFile(i), fileName)
        Next
        Return True
    End Function

#End Region

    ''' <summary>儲存速度設定</summary>
    ''' <param name="fileName">完整檔案路徑</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveVelocity(ByVal fileName As String) As Boolean
        Dim mSection As String = "Velocity"
        With Me
            Call SaveIniString(mSection, "MaxCrossDeviceVelocity", .MaxCrossDeviceVelocity, fileName)
            Call SaveIniString(mSection, "MaxCrossStepVelocity", .MaxCrossStepVelocity, fileName)
            Call SaveIniString(mSection, "MaxDispVelocity", .MaxDispVelocity, fileName)
            Call SaveIniString(mSection, "CrossVerticalTime", .CrossVerticalTime, fileName)
        End With
        Return True
    End Function

    ''' <summary>
    ''' 讀取速度設定
    ''' </summary>
    ''' <param name="fileName">完整檔案路徑</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadVelocity(ByVal fileName As String) As Boolean
        Dim mSection As String = "Velocity"
        With Me
            .MaxCrossDeviceVelocity = Val(ReadIniString(mSection, "MaxCrossDeviceVelocity", fileName, 0))
            .MaxCrossStepVelocity = Val(ReadIniString(mSection, "MaxCrossStepVelocity", fileName, 100))
            .MaxDispVelocity = Val(ReadIniString(mSection, "MaxDispVelocity", fileName, 0))
            .CrossVerticalTime = Val(ReadIniString(mSection, "CrossVerticalTime", fileName, 0.04))
        End With
        Return True
    End Function

    ''' <summary>
    ''' 儲存系統參數
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSystemParameter(ByVal fileName As String) As Boolean

        Dim strSection As String

        strSection = "WorkSize"
        With Me
            Call SaveIniString(strSection, "LogFolderPath", .LogFolderPath, fileName)
            Call SaveIniString(strSection, "MachineID", .MachineID, fileName)
            Call SaveIniString(strSection, "CoordType", .CoordType, fileName)
            Call SaveIniString(strSection, "AirPressureUnit", CInt(.AirPressureUnit), fileName)
            Call SaveIniString(strSection, "IsBypassConveyor", CInt(.IsBypassConveyor), fileName)
            Call SaveIniString(strSection, "PassCCD", CInt(.IsBypassCCD), fileName)
            Call SaveIniString(strSection, "PassLUL", CInt(.PassLUL), fileName)
            Call SaveIniString(strSection, "MotionTolerance", .MotionTolerance, fileName)
            Call SaveIniString(strSection, "TriggerTolerance", .TriggerTolerance, fileName)
            Call SaveIniString(strSection, "PrecisionTolerance", .PrecisionTolerance, fileName)
            Call SaveIniString(strSection, "RanTriggerTolerance", .RanTriggerTolerance, fileName)
            Call SaveIniString(strSection, "PressureTolerance", .PressureTolerance, fileName)
            Call SaveIniString(strSection, "TemperatureTolerance", .TemperatureTolerance, fileName)
            Call SaveIniString(strSection, "ConveyorModel", .ConveyorModel, fileName)
            Call SaveIniString(strSection, "IsCompareWithMapData", CInt(.IsCompareWithMapData), fileName)
            Call SaveIniString(strSection, "IsManualMap", CInt(.IsManualMap), fileName)
            Call SaveIniString(strSection, "TimeOut1", .TimeOut1, fileName)
            Call SaveIniString(strSection, "TimeOut2", .TimeOut2, fileName)
            Call SaveIniString(strSection, "TimeOut3", .TimeOut3, fileName)
            Call SaveIniString(strSection, "TimeOut4", .TimeOut4, fileName)
            Call SaveIniString(strSection, "TimeOut5", .TimeOut5, fileName)
            Call SaveIniString(strSection, "TimeOut6", .TimeOut6, fileName)
            Call SaveIniString(strSection, "TimeOutHome", .TimeOutHome, fileName)
            For i As Integer = 0 To enmStage.Max
                Call SaveIniString(strSection, "OrthgonalAngleDiff" + i.ToString(), .OrthgonalAngleDiff(i), fileName)
            Next

            Call SaveIniString(strSection, "ManualVelHigh", .ManualVelHigh, fileName)
            Call SaveIniString(strSection, "SafeTemperature", .SafeTemperature, fileName)
            Call SaveIniString(strSection, "ValveMaxTemperature", .ValveMaxTemperature, fileName)
            Call SaveIniString(strSection, "HotplateMaxTemperature", .HotplateMaxTemperature, fileName)
            Call SaveIniString(strSection, "HotplateMinTemperature", .HotplateMinTemperature, fileName)
            Call SaveIniString(strSection, "CCDOffsetLimit", .CCDOffsetLimit, fileName)
            Call SaveIniString(strSection, "CCDAngelLimit", .CCDAngelLimit, fileName)
            Call SaveIniString(strSection, "CCDImageSaveMode", CInt(.CCDImageSaveMode), fileName)
            Call SaveIniString(strSection, "CCDAlignModuleEnable", .CCDAlignModuleEnable, fileName)
            Call SaveIniString(strSection, "CCDImageFolderPath", .CCDImageFolderPath, fileName)
            Call SaveIniString(strSection, "LanguageType", CInt(.LanguageType), fileName)
            Call SaveIniString(strSection, "EnableWeightMeasureA", .EnableWeightMeasureA, fileName)
            Call SaveIniString(strSection, "WeightmeasureA", CInt(.WeightMeasureA), fileName)
            Call SaveIniString(strSection, "EnableWeightMeasureB", .EnableWeightMeasureB, fileName)
            Call SaveIniString(strSection, "WeightmeasureB", CInt(.WeightMeasureB), fileName)
            Call SaveIniString(strSection, "WeightSet", CInt(.WeightSet), fileName)
            Call SaveIniString(strSection, "SystemWeighingDataCounter", CInt(.SystemWeighingDataCounter), fileName)
            Call SaveIniString(strSection, "CorrectionSet", .CorrectionNum, fileName)
            Call SaveIniString(strSection, "NonCorrection", .NonCorrection, fileName)
            Call SaveIniString(strSection, "Correction", .Correction, fileName)
            Call SaveIniString(strSection, "CorrectionUSL", .CorrectionUSL, fileName)
            Call SaveIniString(strSection, "CorrectionLSL", .CorrectionLSL, fileName)
            Call SaveIniString(strSection, "CorrectionWeightUpper", .CorrectionWeightUpper, fileName)
            Call SaveIniString(strSection, "CorrectionWeightLower", .CorrectionWeightLower, fileName)
            Call SaveIniString(strSection, "Stage1TiltAngle", Val(.Stage1TiltAngle), fileName)
            Call SaveIniString(strSection, "Stage2TiltAngle", Val(.Stage2TiltAngle), fileName)
            Call SaveIniString(strSection, "Stage3TiltAngle", Val(.Stage3TiltAngle), fileName)
            Call SaveIniString(strSection, "Stage4TiltAngle", Val(.Stage4TiltAngle), fileName)
            Call SaveIniString(strSection, "EnableInitialHotPlate", Val(.EnableInitialHotPlate), fileName)
            Call SaveIniString(strSection, "ContinueLastRun", Val(.IsContinueLastRun), fileName)
            Call SaveIniString(strSection, "CvLoadDirection", Val(.LoadDirection), fileName)
            Call SaveIniString(strSection, "CvUnloadDirection", Val(.UnloadDirection), fileName)

            '20171005
            Call SaveIniString(strSection, "PathMultiple", Val(.PathMultiple), fileName)

            'jimmy 20170630
            Call SaveIniString(strSection, "CleanDevice", Val(.IsCleanDevice), fileName)

            Call SaveIniString(strSection, "RerunDataFolderPath", .RerunDataFolderPath, fileName)

            '判斷機型，以儲存安全位置保護資料
            Select Case .MachineType
                Case enmMachineType.DCSW_800AQ   '雙軌四閥，儲存二組
                    strSection = "MachineA Safe Data"
                    .MachineSafeData(enmMachineStation.MachineA).Save(fileName, strSection)
                    strSection = "MachineB Safe Data"
                    .MachineSafeData(enmMachineStation.MachineB).Save(fileName, strSection)

                Case enmMachineType.eDTS_2S2V   '單軌雙閥，只儲存一組
                    strSection = "MachineA Safe Data"
                    .MachineSafeData(enmMachineStation.MachineA).Save(fileName, strSection)

                Case Else       '其餘不儲存

            End Select
        End With

        Return True

ErrorHandler:  ' Error-handling routine.
        MsgBox(Err.GetException.ToString(), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        Err.Clear()
        Return False
    End Function

    'jimmy add 20170424
    Public Sub SaveContinueLastRun(ByVal fileName As String)
        SaveIniString("WorkSize", "ContinueLastRun", Val(Me.IsContinueLastRun), fileName)
    End Sub
    ''' <summary>讀取硬體配置</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadHardwareParameter(ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "WorkSize"
        With Me
            .StageUseValveCount = ReadIniString(strSection, "StageUseValveCount", fileName, 1)
            .StageCount = ReadIniString(strSection, "StageCount", fileName, 1)
            .CCDModuleType = ReadIniString(strSection, "CCDModuleType", fileName, 1)
            .MeasureType = ReadIniString(strSection, "MeasureType", fileName, 0)
            .CCDOnFlyEnable = ReadIniString(strSection, "CCDOnFlyEnable", fileName, False)
            .MultiDispenseEnable = ReadIniString(strSection, "MultiDispenseEnable", fileName, False)
        End With
        Return True
    End Function

    ''' <summary>讀取硬體配置</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveHardwareParameter(ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "WorkSize"
        With Me
            Call SaveIniString(strSection, "StageUseValveCount", .StageUseValveCount, fileName)
            Call SaveIniString(strSection, "StageCount", .StageCount, fileName)
            Call SaveIniString(strSection, "CCDModuleType", .CCDModuleType, fileName)
            Call SaveIniString(strSection, "MeasureType", .MeasureType, fileName)
            Call SaveIniString(strSection, "CCDOnFlyEnable", .CCDOnFlyEnable, fileName)
            Call SaveIniString(strSection, "MultiDispenseEnable", .MultiDispenseEnable, fileName)
        End With
        Return True

    End Function


    ''' <summary>儲存計數器(定位判定次數/測高判定次數/點膠判定次數)</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCounter(ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "ProductState"
        With Me.ProductState
            Call SaveIniString(strSection, "CCDScanOKPcs", .CCDScanOKPcs, fileName)
            Call SaveIniString(strSection, "CCDScanNGPcs", .CCDScanNGPcs, fileName)
            Call SaveIniString(strSection, "TotalCCDScanPcs", .TotalCCDScanPcs, fileName)

            Call SaveIniString(strSection, "LaserReaderOKPcs", .LaserReaderOKPcs, fileName)
            Call SaveIniString(strSection, "LaserReaderNGPcs", .LaserReaderNGPcs, fileName)
            Call SaveIniString(strSection, "TotoalLaserReaderPcs", .TotoalLaserReaderPcs, fileName)

            Call SaveIniString(strSection, "DispensingOKPcs", .DispensingOKPcs, fileName)
            Call SaveIniString(strSection, "DispensingNGPcs", .DispensingNGPcs, fileName)
            Call SaveIniString(strSection, "TotalDispensingPcs", .TotalDispensingPcs, fileName)
        End With
        Return True
    End Function
    ''' <summary>讀取計數器(定位判定次數/測高判定次數/點膠判定次數)</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadCounter(ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "ProductState"
        With Me.ProductState
            .CCDScanOKPcs = CLng(ReadIniString(strSection, "CCDScanOKPcs", fileName, 0))
            .CCDScanNGPcs = CLng(ReadIniString(strSection, "CCDScanNGPcs", fileName, 0))
            .TotalCCDScanPcs = CLng(ReadIniString(strSection, "TotalCCDScanPcs", fileName, 0))

            .LaserReaderOKPcs = CLng(ReadIniString(strSection, "LaserReaderOKPcs", fileName, 0))
            .LaserReaderNGPcs = CLng(ReadIniString(strSection, "LaserReaderNGPcs", fileName, 0))
            .TotoalLaserReaderPcs = CLng(ReadIniString(strSection, "TotoalLaserReaderPcs", fileName, 0))

            .DispensingOKPcs = CLng(ReadIniString(strSection, "DispensingOKPcs", fileName, 0))
            .DispensingNGPcs = CLng(ReadIniString(strSection, "DispensingNGPcs", fileName, 0))
            .TotalDispensingPcs = CLng(ReadIniString(strSection, "TotalDispensingPcs", fileName, 0))
        End With
        Return True
    End Function

    ''' <summary>權限讀檔</summary>
    ''' <remarks></remarks>
    Public Function LoadAuthority(ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "Authority-Main"

        UserAuth(enmUserAuthItem.Recipe) = CInt(ReadIniString(strSection, "Recipe", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.Calibration) = CInt(ReadIniString(strSection, "Calibration", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.EngineMode) = CInt(ReadIniString(strSection, "EngineMode", fileName, CInt(enmUserLevel.eEngineer)))

        strSection = "Authority-Engine Mode"
        '=== 整機操作類型 ===
        UserAuth(enmUserAuthItem.Manual) = CInt(ReadIniString(strSection, "Manual", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.ManualMotor) = CInt(ReadIniString(strSection, "ManualMotor", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.IOSetup) = CInt(ReadIniString(strSection, "IOSetup", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.IOTable) = CInt(ReadIniString(strSection, "IOTable", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetPurgePos) = CInt(ReadIniString(strSection, "PurgePos", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetClearPos) = CInt(ReadIniString(strSection, "ClearPos", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetChangePos) = CInt(ReadIniString(strSection, "ChangePos", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetWeightPos) = CInt(ReadIniString(strSection, "WeightPos", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetUserLevel) = CInt(ReadIniString(strSection, "SetUserLevel", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetMessageLanguage) = CInt(ReadIniString(strSection, "SetMessageLanguage", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.setHardwareConfig) = CInt(ReadIniString(strSection, "setHardwareConfig", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetInterlock) = CInt(ReadIniString(strSection, "SetInterlock", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetStageSafe) = CInt(ReadIniString(strSection, "SetStageSafe", fileName, CInt(enmUserLevel.eEngineer)))
        '=== 整機操作類型 ===

        '=== 模組操作類型 ===
        UserAuth(enmUserAuthItem.SetModuleConveyor) = CInt(ReadIniString(strSection, "SetModuleConveyor", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetModuleAOI) = CInt(ReadIniString(strSection, "SetModuleAOI", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetModuleTowerLight) = CInt(ReadIniString(strSection, "SetModuleTowerLight", fileName, CInt(enmUserLevel.eEngineer)))
        '=== 模組操作類型 ===

        '=== 部件操作類型 ===
        UserAuth(enmUserAuthItem.SetPartHotPlate) = CInt(ReadIniString(strSection, "SetPartHotPlate", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetValveController) = CInt(ReadIniString(strSection, "SetValveController", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetCCD) = CInt(ReadIniString(strSection, "SetCCD", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetLight) = CInt(ReadIniString(strSection, "SetLight", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetTriggerController) = CInt(ReadIniString(strSection, "SetTriggerController", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetBalance) = CInt(ReadIniString(strSection, "SetBalance", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetLaserReader) = CInt(ReadIniString(strSection, "SetLaserReader", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetFMCS) = CInt(ReadIniString(strSection, "SetFMCS", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetConveyor) = CInt(ReadIniString(strSection, "SetConveyor", fileName, CInt(enmUserLevel.eEngineer)))

        UserAuth(enmUserAuthItem.SetTilt) = CInt(ReadIniString(strSection, "SetTilt", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetElectricCylinder) = CInt(ReadIniString(strSection, "SetElectricCylinder", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetTemperature) = CInt(ReadIniString(strSection, "SetTemperature", fileName, CInt(enmUserLevel.eEngineer)))
        UserAuth(enmUserAuthItem.SetElectroPneumaticValve) = CInt(ReadIniString(strSection, "SetElectroPneumaticValve", fileName, CInt(enmUserLevel.eEngineer)))
        '=== 部件操作類型 ===

        Return True
    End Function
    ''' <summary>權限存檔</summary>
    ''' <remarks></remarks>
    Public Function SaveAuthority(ByVal fileName As String) As Boolean
        'Dim strFileName As String
        Dim strSection As String
        strSection = "Authority-Main"
        Call SaveIniString(strSection, "Recipe", CInt(UserAuth(enmUserAuthItem.Recipe)), fileName)
        Call SaveIniString(strSection, "Calibration", CInt(UserAuth(enmUserAuthItem.Calibration)), fileName)
        Call SaveIniString(strSection, "EngineMode", CInt(UserAuth(enmUserAuthItem.EngineMode)), fileName)

        strSection = "Authority-Engine Mode"
        '=== 整機操作類型 ===
        Call SaveIniString(strSection, "Manual", CInt(UserAuth(enmUserAuthItem.Manual)), fileName)
        Call SaveIniString(strSection, "ManualMotor", CInt(UserAuth(enmUserAuthItem.ManualMotor)), fileName)
        Call SaveIniString(strSection, "IOSetup", CInt(UserAuth(enmUserAuthItem.IOSetup)), fileName)
        Call SaveIniString(strSection, "IOTable", CInt(UserAuth(enmUserAuthItem.IOTable)), fileName)
        Call SaveIniString(strSection, "PurgePos", CInt(UserAuth(enmUserAuthItem.SetPurgePos)), fileName)
        Call SaveIniString(strSection, "ClearPos", CInt(UserAuth(enmUserAuthItem.SetClearPos)), fileName)
        Call SaveIniString(strSection, "ChangePos", CInt(UserAuth(enmUserAuthItem.SetChangePos)), fileName)
        Call SaveIniString(strSection, "WeightPos", CInt(UserAuth(enmUserAuthItem.SetWeightPos)), fileName)
        Call SaveIniString(strSection, "SetUserLevel", CInt(UserAuth(enmUserAuthItem.SetUserLevel)), fileName)
        Call SaveIniString(strSection, "SetMessageLanguage", CInt(UserAuth(enmUserAuthItem.SetMessageLanguage)), fileName)
        Call SaveIniString(strSection, "setHardwareConfig", CInt(UserAuth(enmUserAuthItem.setHardwareConfig)), fileName)
        Call SaveIniString(strSection, "SetInterlock", CInt(UserAuth(enmUserAuthItem.SetInterlock)), fileName)
        '=== 整機操作類型 ===

        '=== 模組操作類型 ===
        Call SaveIniString(strSection, "SetModuleConveyor", CInt(UserAuth(enmUserAuthItem.SetModuleConveyor)), fileName)
        Call SaveIniString(strSection, "SetModuleAOI", CInt(UserAuth(enmUserAuthItem.SetModuleAOI)), fileName)
        Call SaveIniString(strSection, "SetModuleTowerLight", CInt(UserAuth(enmUserAuthItem.SetModuleTowerLight)), fileName)
        '=== 模組操作類型 ===

        '=== 部件操作類型 ===
        Call SaveIniString(strSection, "SetPartHotPlate", CInt(UserAuth(enmUserAuthItem.SetPartHotPlate)), fileName)
        Call SaveIniString(strSection, "SetValveController", CInt(UserAuth(enmUserAuthItem.SetValveController)), fileName)
        Call SaveIniString(strSection, "SetCCD", CInt(UserAuth(enmUserAuthItem.SetCCD)), fileName)
        Call SaveIniString(strSection, "SetLight", CInt(UserAuth(enmUserAuthItem.SetLight)), fileName)
        Call SaveIniString(strSection, "SetTriggerController", CInt(UserAuth(enmUserAuthItem.SetTriggerController)), fileName)
        Call SaveIniString(strSection, "SetBalance", CInt(UserAuth(enmUserAuthItem.SetBalance)), fileName)
        Call SaveIniString(strSection, "SetLaserReader", CInt(UserAuth(enmUserAuthItem.SetLaserReader)), fileName)
        Call SaveIniString(strSection, "SetFMCS", CInt(UserAuth(enmUserAuthItem.SetFMCS)), fileName)
        Call SaveIniString(strSection, "SetConveyor", CInt(UserAuth(enmUserAuthItem.SetConveyor)), fileName)

        Call SaveIniString(strSection, "SetTilt", CInt(UserAuth(enmUserAuthItem.SetTilt)), fileName)
        Call SaveIniString(strSection, "SetElectricCylinder", CInt(UserAuth(enmUserAuthItem.SetElectricCylinder)), fileName)
        Call SaveIniString(strSection, "SetTemperature", CInt(UserAuth(enmUserAuthItem.SetTemperature)), fileName)
        Call SaveIniString(strSection, "SetElectroPneumaticValve", CInt(UserAuth(enmUserAuthItem.SetElectroPneumaticValve)), fileName)
        '=== 部件操作類型 ===


        Return True
    End Function


#End Region

    ''' <summary>氣壓單位-人機顯示</summary>
    ''' <remarks></remarks>
    Public AirPressureUnit As enmAirPressureUnit
    ''' <summary>位置單位-人機顯示</summary>
    ''' <remarks></remarks>
    Public PositionUnit As enmPositionUnit

    'Eason 20170303 Ticket:100100 , XY Offset from CSV File
    ''' <summary>
    ''' 0 = 不開啟
    ''' 1 = SW
    ''' 2 = 軸卡
    ''' </summary>
    ''' <remarks></remarks>
    Public StageFixMode As Integer = 0

    ''' <summary>軸卡補償設定檔路徑</summary>
    ''' <remarks></remarks>
    Public StageFile(enmStage.Max) As String
End Class

''' <summary>氣壓單位</summary>
''' <remarks></remarks>
Public Enum enmAirPressureUnit
    ''' <summary>
    ''' 兆帕
    ''' </summary>
    ''' <remarks></remarks>
    MPa = 0
    ''' <summary>
    ''' 千克力/厘米2
    ''' </summary>
    ''' <remarks></remarks>
    Kgcm2 = 1
    ''' <summary>
    ''' 巴
    ''' </summary>
    ''' <remarks></remarks>
    Bar = 2
    ''' <summary>
    ''' 磅/英寸2
    ''' </summary>
    ''' <remarks></remarks>
    psi = 3
    ''' <summary>
    ''' 托
    ''' </summary>
    ''' <remarks></remarks>
    Torr = 4
End Enum

Public Enum enmPositionUnit
    ''' <summary>公制-毫米</summary>
    ''' <remarks></remarks>
    mm = 0
    ''' <summary>英制-英吋</summary>
    ''' <remarks></remarks>
    Inch = 1
End Enum

''' <summary>氣壓單位轉換</summary>
''' <remarks></remarks>
Public Class AirPressureTransform
    ''' <summary>氣壓單位轉換表</summary>
    ''' <remarks></remarks>
    Shared AirPressureTable(,) As Decimal = {{1, 0.0980665, 0.1, 0.006895, 0.0001332}, {10.19716, 1, 1.01972, 0.07031, 0.0013595}, {10, 0.980665, 1, 0.06895, 0.0013332}, {145.036, 14.2231, 14.5036, 1, 0.0193364}, {7500.61, 735.559, 750.062, 51.7157, 1}}
   
    ''' <summary>氣壓單位轉換</summary>
    ''' <param name="value"></param>
    ''' <param name="fromUnit"></param>
    ''' <param name="toUnit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ToAirPressureUnit(ByVal value As Decimal, ByVal fromUnit As enmAirPressureUnit, ByVal toUnit As enmAirPressureUnit) As Decimal
        Return value * AirPressureTable(toUnit, fromUnit)
    End Function
End Class
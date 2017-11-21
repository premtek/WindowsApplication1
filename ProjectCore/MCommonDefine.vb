''' <summary>各機型不同的項目定義放在此</summary>
''' <remarks></remarks>
Public Module MCommonDefine

    ''' <summary>
    ''' 電空閥虛擬編號
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmEPV
        No1 = 0
        No2 = 1
        No3 = 2
        No4 = 3
        Max = No4
    End Enum

    ''' <summary>天平型號</summary>
    ''' <remarks></remarks>
    Public Enum enmBalanceType
        ''' <summary>無</summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>四位數天平</summary>
        ''' <remarks></remarks>
        AD4212A100 = 1
        ''' <summary>Sartorius天平</summary>
        ''' <remarks></remarks>
        WZA215_LC = 2
    End Enum

    ''' <summary>FMCS型號</summary>
    ''' <remarks></remarks>
    Public Enum enmFMCSType
        ''' <summary>未使用</summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>FMCS</summary>
        ''' <remarks></remarks>
        FMCS = 1
        ''' <summary>
        ''' FMCS-P
        ''' </summary>
        ''' <remarks></remarks>
        FMCS_P = 2
        ''' <summary>
        ''' FMCS-PI
        ''' </summary>
        ''' <remarks></remarks>
        FMCS_PI = 3
        ''' <summary>
        ''' FMCS-PII
        ''' </summary>
        ''' <remarks></remarks>
        FMCS_PII = 4
    End Enum

    ''' <summary>
    ''' 使用者權限
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmUserLevel
        ''' <summary>我司軟體工程師
        ''' </summary>
        ''' <remarks></remarks>
        eSoftwareMaker = 0
        ''' <summary>我司客服權限
        ''' </summary>
        ''' <remarks></remarks>
        eAdministrator = 1
        ''' <summary>客戶最高權限(帳號管理)</summary>
        ''' <remarks></remarks>
        eManager = 2
        ''' <summary>客戶工程師</summary>
        ''' <remarks></remarks>
        eEngineer = 3
        ''' <summary>客戶作業員</summary>
        ''' <remarks></remarks>
        eOperator = 4
    End Enum
    
    Public Function UserLevelToString(ByVal userLevel As enmUserLevel) As String
        Select Case userLevel
            Case enmUserLevel.eAdministrator
                Return "Administrator"
            Case enmUserLevel.eEngineer
                Return "Engineer"
            Case enmUserLevel.eManager
                Return "Manager"
            Case enmUserLevel.eOperator
                Return "Operator"
            Case enmUserLevel.eSoftwareMaker
                Return "Software"
        End Select
        Return "Unknown"
    End Function
    ''' <summary>
    ''' 點膠機台類別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmMachineType
        ''' <summary>未定義</summary>
        ''' <remarks></remarks>
        Undefined = 0
        ''' <summary>[DTS獨立機#2-#8]</summary>
        ''' <remarks></remarks>
        eDTS300A = 2
        ''' <summary>一平台一閥 桌上型</summary>
        ''' <remarks></remarks>
        eDTS_Desktop = 3
        ''' <summary>一平台二閥</summary>
        ''' <remarks></remarks>
        eDTS330A = 4
        ''' <summary>二平台二閥</summary>
        ''' <remarks></remarks>
        eDTS_2S2V = 5
        ''' <summary>四平台四閥</summary>
        ''' <remarks></remarks>
        DCSW_800AQ = 6
        ''' <summary>正一特例</summary>
        ''' <remarks></remarks>
        eDTS330ACR1 = 7
        ''' <summary>正一窄版客製機</summary>
        ''' <remarks></remarks>
        DCS_F230A = 8
        ''' <summary>標準機</summary>
        ''' <remarks></remarks>
        DCS_350A = 9
        ''' <summary>二平台二閥 800AQ分拆版
        ''' </summary>
        ''' <remarks></remarks>
        DCS_500AD = 10
    End Enum

    ''' <summary>機台型號轉顯示字串</summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MachineTypeToString(ByVal index As enmMachineType) As String
        '--- Soni + 機台型號顯示  2014.09.29 ---
        Select Case index
            Case enmMachineType.eDTS300A
                Return "DTS-300A"
            Case enmMachineType.eDTS330A
                Return "DTS-330A"
            Case enmMachineType.eDTS_2S2V
                Return "DTS-2S2V"
            Case enmMachineType.DCSW_800AQ
                Return "DCSW-800AQ"
            Case enmMachineType.DCS_F230A
                Return "DCS-F230A"
            Case enmMachineType.DCS_350A
                Return "DCS-350A"
            Case enmMachineType.DCS_500AD
                Return "DCS-500AD"
        End Select
        '--- Soni + 機台型號顯示  2014.09.29 ---
        Return "Undefined"
    End Function

    ''' <summary>傳送帶組數</summary>
    ''' <remarks></remarks>
    Public Enum enmConveyor
        ''' <summary>第一組傳送帶</summary>
        ''' <remarks></remarks>
        No1 = 0
        ''' <summary>第二組傳送帶(雙軌)</summary>
        ''' <remarks></remarks>
        No2 = 1
        Max = No1
    End Enum

    ''' <summary>微量天平組數</summary>
    ''' <remarks></remarks>SS
    Public Enum enmBalance
        ''' <summary>第一組天平</summary>
        ''' <remarks></remarks>
        No1 = 0
        ''' <summary>第二組天平</summary>
        ''' <remarks></remarks>
        No2 = 1
        Max = No2
    End Enum

    ''' <summary>雷射讀值組數(干涉儀)</summary>
    ''' <remarks></remarks>
    Public Enum enmLaserReader
        ''' <summary>第一組</summary>
        ''' <remarks></remarks>
        No1 = 0
        ''' <summary>第二組</summary>
        ''' <remarks></remarks>
        No2 = 1
        ''' <summary>第三組</summary>
        ''' <remarks></remarks>
        No3 = 2
        ''' <summary>第四組</summary>
        ''' <remarks></remarks>
        No4 = 3
        ''' <summary>最大數量</summary>
        ''' <remarks></remarks>
        Max = 3
    End Enum
    Public Enum enmValveController
        ''' <summary>第一組</summary>
        ''' <remarks></remarks>
        No1 = 0
        ''' <summary>第二組</summary>
        ''' <remarks></remarks>
        No2 = 1
        ''' <summary>第三組</summary>
        ''' <remarks></remarks>
        No3 = 2
        ''' <summary>第四組</summary>
        ''' <remarks></remarks>
        No4 = 3
        ''' <summary>最大數量</summary>
        ''' <remarks></remarks>
        Max = 3
    End Enum

    ''' <summary>光源組數</summary>
    ''' <remarks></remarks>
    Public Enum enmLight
        None = -1
        ''' <summary>第一點</summary>
        ''' <remarks></remarks>
        No1 = 0
        ''' <summary>第二點</summary>
        ''' <remarks></remarks>
        No2 = 1
        ''' <summary>第三點</summary>
        ''' <remarks></remarks>
        No3 = 2
        ''' <summary>第四點</summary>
        ''' <remarks></remarks>
        No4 = 3
        ''' <summary>第五點</summary>
        ''' <remarks></remarks>
        No5 = 4
        ''' <summary>第六點</summary>
        ''' <remarks></remarks>
        No6 = 5
        ''' <summary>第七點</summary>
        ''' <remarks></remarks>
        No7 = 6
        ''' <summary>第八點</summary>
        ''' <remarks></remarks>
        No8 = 7
        Max = No8
    End Enum
    ''' <summary>程控光源單閥配接點</summary>
    ''' <remarks></remarks>
    Public Enum enmValveLight
        ''' <summary>第一點</summary>
        ''' <remarks></remarks>
        No1 = 0
        ''' <summary>第二點</summary>
        ''' <remarks></remarks>
        No2 = 1
        ''' <summary>第三點</summary>
        ''' <remarks></remarks>
        No3 = 2
        ''' <summary>第四點</summary>
        ''' <remarks></remarks>
        No4 = 3
        Max = No4
    End Enum

    ''' <summary>觸發板索引</summary>
    ''' <remarks></remarks>
    Public Enum enmTriggerBoard
        ''' <summary>第一組</summary>
        ''' <remarks></remarks>
        No1 = 0
        ''' <summary>第二組</summary>
        ''' <remarks></remarks>
        No2 = 1
        ''' <summary>第三組</summary>
        ''' <remarks></remarks>
        No3 = 2
        ''' <summary>第四組</summary>
        ''' <remarks></remarks>
        No4 = 3
        ''' <summary>最大數量</summary>
        ''' <remarks></remarks>
        Max = No4
    End Enum
    ''' <summary>
    ''' 原enmDsipenserNo
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmValve
        ''' <summary>第一組</summary>
        ''' <remarks></remarks>
        No1 = 0
        ''' <summary>第二組</summary>
        ''' <remarks></remarks>
        No2 = 1
        ''' <summary>第三組</summary>
        ''' <remarks></remarks>
        No3 = 2
        ''' <summary>第四組</summary>
        ''' <remarks></remarks>
        No4 = 3
        ''' <summary>最大數量</summary>
        ''' <remarks></remarks>
        Max = No4
    End Enum

    ''' <summary>單平台作業閥No.</summary>
    ''' <remarks></remarks>
    Public Enum eValveWorkMode
        None = -1
        Valve1 = 0
        Valve2 = 1
        ''' <summary>[多閥同動點膠]</summary>
        ''' <remarks></remarks>
        MultiValve = 99
        MaxValve = Valve2
        'Valve3 = 4
        'Valve4 = 8
        '預留參數，暫不開放
        'ComboValve12 = 3
        'ComboValve13 = 5
        'ComboValve14 = 9
        'ComboValve23 = 6
        'ComboValve24 = 10
        'ComboValve34 = 12
        'ComboValve123 = 7
        'ComboValve124 = 11
        'ComboValve134 = 13
        'ComboValve234 = 14
        'ComboValve1234 = 15
    End Enum
    ''' <summary>平台索引</summary>
    ''' <remarks></remarks>
    Public Enum enmStage
        ''' <summary>第一組</summary>
        ''' <remarks></remarks>
        No1 = 0
        ''' <summary>第二組</summary>
        ''' <remarks></remarks>
        No2 = 1
        ''' <summary>第三組</summary>
        ''' <remarks></remarks>
        No3 = 2
        ''' <summary>第四組</summary>
        ''' <remarks></remarks>
        No4 = 3
        ''' <summary>最大數量</summary>
        ''' <remarks></remarks>
        Max = No4
    End Enum

    ''' <summary>平台最大編號</summary>
    ''' <remarks></remarks>
    Public StageMax As Integer
    ''' <summary>
    ''' 使用哪幾隻點膠閥
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmUseDispenserNo 'TODO: Mobary 請確認此項目是否仍留存
        None = 0
        No1 = 1
        No2 = 2
        No1No2 = 3
    End Enum

    ''' <summary>
    ''' [機台索引值]
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmMachineStation
        ''' <summary>機台A</summary>
        ''' <remarks></remarks>
        MachineA = 0
        ''' <summary>機台B</summary>
        ''' <remarks></remarks>
        MachineB = 1
        ''' <summary>總部的機台數量</summary>
        ''' <remarks></remarks>
        MaxMachine = 1
    End Enum

    ''' <summary>[Conveyor索引值]</summary>
    ''' <remarks></remarks>
    Public Enum eConveyor
        ''' <summary>[第一組Conveyor]</summary>
        ''' <remarks></remarks>
        ConveyorNo1 = 0
        ''' <summary>第二組Conveyor</summary>
        ''' <remarks></remarks>
        ConveyorNo2 = 1
        Max = ConveyorNo2
    End Enum


    ''' <summary>[指定使用哪個Conveyor生產]</summary>
    ''' <remarks></remarks>
    Public Enum eConveyorModel
        ''' <summary>[指定使用ConveyorNo1生產]</summary>
        ''' <remarks></remarks>
        eConveyorNo1 = 0
        ''' <summary>[指定使用ConveyorNo2生產]</summary>
        ''' <remarks></remarks>
        eConveyorNo2 = 1
        ''' <summary>[指定使用ConveyorNo1、ConveyorNo2生產]</summary>
        ''' <remarks></remarks>
        eConveyorNo1No2 = 2
    End Enum


    ''' <summary>讀取機台型號並轉為字串</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMachineTypeFromFileToString() As String
        Dim strFileName As String
        Dim strSection As String
        strFileName = Application.StartupPath & "\system\SystemParamter.ini"

        strSection = "WorkSize"
        With gSSystemParameter
            .RunMode = Val(ReadIniString(strSection, "RunMode", strFileName, 0))
            .MachineType = Val(ReadIniString(strSection, "MachineType", strFileName, enmMachineType.DCS_350A))
            .StageFixMode = Val(ReadIniString(strSection, "StageFixMode", strFileName, 0)) 'Eason 20170303 Ticket:100100 , XY Offset from CSV File
        End With
        Return MachineTypeToString(gSSystemParameter.MachineType)
    End Function
    ''' <summary></summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveMachineType() As Boolean
        Dim strSection As String
        Dim fileName As String
        fileName = Application.StartupPath & "\system\SystemParamter.ini"

        strSection = "WorkSize"
        With gSSystemParameter
            Call SaveIniString(strSection, "RunMode", CInt(.RunMode), fileName)
            Call SaveIniString(strSection, "MachineType", CInt(.MachineType), fileName)
        End With
        Return True
    End Function

    ''' <summary>機台名稱(DTS-330A..etc) 待收入SystemParameter</summary>
    ''' <remarks></remarks>
    Public MachineName As String

    ''' <summary>
    ''' 機台軟體運作模式 0:模擬(無硬體) 1:生產
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmRunMode
        ''' <summary>模擬(無硬體)</summary>
        ''' <remarks></remarks>
        Simulation = 0
        ''' <summary>生產</summary>
        ''' <remarks></remarks>
        Run = 1
    End Enum

    ''' <summary>系統命令</summary>
    ''' <remarks></remarks>
    Public Enum eSysCommand
        ''' <summary>無命令</summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>復歸</summary>
        ''' <remarks></remarks>
        Home = 1
        ''' <summary>自動生產</summary>
        ''' <remarks></remarks>
        AutoRun = 2
        ''' <summary>擦膠</summary>
        ''' <remarks></remarks>
        ClearGlue = 3
        ''' <summary>排膠</summary>
        ''' <remarks></remarks>
        Purge = 4
        ''' <summary>CCD閥自動校正</summary>
        ''' <remarks></remarks>
        CCDValveAutoCalibrationXY = 5
        ''' <summary>單邊微量天平秤重</summary>
        ''' <remarks></remarks>
        WeightUnit = 6
        ''' <summary>單邊手動微量天平秤重</summary>
        ''' <remarks></remarks>
        ManuallyWeightUnit = 7
        ''' <summary>單邊換膠</summary>
        ''' <remarks></remarks>
        ChangeGlue = 8
        ''' <summary>單邊閥自動測高</summary>
        ''' <remarks></remarks>
        DispenserAutoSearch = 9
        ''' <summary>單邊雷射測高</summary>
        ''' <remarks></remarks>
        LaserReader = 10
        ''' <summary>單邊CCD定位</summary>
        ''' <remarks></remarks>
        CCDFix = 11
        ''' <summary>單邊CCD後檢</summary>
        ''' <remarks></remarks>
        CCDSCanGlue = 12
        ''' <summary>單邊點膠</summary>
        ''' <remarks></remarks>
        Dispensing = 13
        ''' <summary>產品資料載入</summary>
        ''' <remarks></remarks>
        ProductLoading = 14
        ''' <summary>退料</summary>
        ''' <remarks></remarks>
        ProductUnload = 15
        ''' <summary>進料</summary>
        ''' <remarks></remarks>
        Loading = 16
        ''' <summary>A機進料</summary>
        ''' <remarks></remarks>
        LoadA = 17
        ''' <summary>B機進料</summary>
        ''' <remarks></remarks>
        LoadB = 18
        ''' <summary>B機退料 </summary>
        ''' <remarks></remarks>
        UnloadB = 19
        ''' <summary>A機退料</summary>
        ''' <remarks></remarks>
        UnloadA = 20
        ''' <summary>點膠前動作(秤重、Purge)</summary>
        ''' <remarks></remarks>
        PrevDispense = 21
        ''' <summary>CCD-閥全自動校正(XYZ)</summary>
        ''' <remarks></remarks>
        CCDValveFullAutoCalibration = 22
        ' ''' <summary>手動生產模式</summary>
        ' ''' <remarks></remarks>
        'ManualProduceMode = 23
        ' ''' <summary>單動模式[可單獨執行特定動作，例如：測重、清膠等等]</summary>
        ' ''' <remarks></remarks>
        'SingleAction = 24
        ''' <summary>[接續流程(接續之前沒做完的生產流程)]</summary>
        ''' <remarks></remarks>
        ContinueLastRun = 25
        ''' <summary>[強制退料]</summary>
        ''' <remarks></remarks>
        AbnormalUnload = 26
        ''' <summary>A機復歸</summary>
        ''' <remarks></remarks>
        HomeA = 27
        ''' <summary>B機復歸</summary>
        ''' <remarks></remarks>
        HomeB = 28
        ''' <summary>安全</summary>
        ''' <remarks></remarks>
        Safe = 29
        ''' <summary>[監控]</summary>
        ''' <remarks></remarks>
        Monitor = 30

    End Enum

    ''' <summary>暫停時例外動作</summary>
    ''' <remarks></remarks>
    Public Enum sPauseAction
        ''' <summary>走到設定位置</summary>
        ''' <remarks></remarks>
        GoToSetPos = 0
        ''' <summary>清膠動作</summary>
        ''' <remarks></remarks>
        Purge = 1
        ''' <summary>左側秤重動作</summary>
        ''' <remarks></remarks>
        WeightLeft = 2
        ''' <summary>右側秤重動作</summary>
        ''' <remarks></remarks>
        WeightRight = 3
        ''' <summary>走回原本暫停時的位置</summary>
        ''' <remarks></remarks>
        GoToBackPos = 4
        ''' <summary>回復到原本生產動作</summary>
        ''' <remarks></remarks>
        ReturnToProcess = 5
    End Enum

    ''' <summary>系統模組命令接收層</summary>
    ''' <remarks></remarks>
    Public Structure eSys
        ''' <summary>手動操作</summary>
        ''' <remarks></remarks>
        Public Shared Manual As Integer = 0
        ''' <summary>整機系統</summary>
        ''' <remarks></remarks>
        Public Shared OverAll As Integer = 1
        ''' <summary>A機(進料端)</summary>
        ''' <remarks></remarks>
        Public Shared MachineA As Integer = 2
        ''' <summary>B機(退料端)</summary>
        ''' <remarks></remarks>
        Public Shared MachineB As Integer = 3
        ''' <summary>點膠平台1</summary>
        ''' <remarks></remarks>
        Public Shared DispStage1 As Integer = 4
        ''' <summary>點膠平台2</summary>
        ''' <remarks></remarks>
        Public Shared DispStage2 As Integer = 5
        ''' <summary>點膠平台3</summary>
        ''' <remarks></remarks>
        Public Shared DispStage3 As Integer = 6
        ''' <summary>點膠平台4</summary>
        ''' <remarks></remarks>
        Public Shared DispStage4 As Integer = 7
        ''' <summary>傳送帶1</summary>
        ''' <remarks></remarks>
        Public Shared Conveyor1 As Integer = 8
        ''' <summary>傳送帶2</summary>
        ''' <remarks></remarks>
        Public Shared Conveyor2 As Integer = 9
        ''' <summary>流道站1</summary>
        ''' <remarks></remarks>
        Public Shared Station1 As Integer = 10
        ''' <summary>流道站2</summary>
        ''' <remarks></remarks>
        Public Shared Station2 As Integer = 11
        ''' <summary>流道站3</summary>
        ''' <remarks></remarks>
        Public Shared Station3 As Integer = 12
        ''' <summary>點膠平台周邊動作1</summary>
        ''' <remarks></remarks>
        Public Shared SubDisp1 As Integer = 13
        ''' <summary>點膠平台周邊動作2</summary>
        ''' <remarks></remarks>
        Public Shared SubDisp2 As Integer = 14
        ''' <summary>點膠平台周邊動作3</summary>
        ''' <remarks></remarks>
        Public Shared SubDisp3 As Integer = 15
        ''' <summary>點膠平台周邊動作4</summary>
        ''' <remarks></remarks>
        Public Shared SubDisp4 As Integer = 16
        ''' <summary>[點膠平台監測1]</summary>
        ''' <remarks></remarks>
        Public Shared MonitorDisp1 As Integer = 17
        ''' <summary>[點膠平台監測2]</summary>
        ''' <remarks></remarks>
        Public Shared MonitorDisp2 As Integer = 18
        ''' <summary>[點膠平台監測3]</summary>
        ''' <remarks></remarks>
        Public Shared MonitorDisp3 As Integer = 19
        ''' <summary>[點膠平台監測4]</summary>
        ''' <remarks></remarks>
        Public Shared MonitorDisp4 As Integer = 20

        Public Shared Max As Integer = 20
    End Structure

    ''' <summary>動作</summary>
    ''' <remarks></remarks>
    Public Structure eAct
        ''' <summary>微量天平秤重</summary>
        ''' <remarks></remarks>
        Public Shared WeightUnit As Integer = 2
        ''' <summary>機台復歸</summary>
        ''' <remarks></remarks>
        Public Shared Home As Integer = 3
        ''' <summary>自動生產</summary>
        ''' <remarks></remarks>
        Public Shared AutoRun As Integer = 4
        ''' <summary>真空除膠</summary>
        ''' <remarks></remarks>
        Public Shared Purge As Integer = 5
        ''' <summary>換膠</summary>
        ''' <remarks></remarks>
        Public Shared ChangeGlue As Integer = 6
        ''' <summary>擦膠</summary>
        ''' <remarks></remarks>
        Public Shared ClearGlue As Integer = 7
        ''' <summary>閥自動測高</summary>
        ''' <remarks></remarks>
        Public Shared DispenserAutoSearch As Integer = 8
        ''' <summary>雷射測高</summary>
        ''' <remarks></remarks>
        Public Shared LaserReader As Integer = 9
        ''' <summary>CCD定位</summary>
        ''' <remarks></remarks>
        Public Shared CCDSCanFix As Integer = 10
        ''' <summary>CCD後檢</summary>
        ''' <remarks></remarks>
        Public Shared CCDSCanGlue As Integer = 11
        ''' <summary>點膠</summary>
        ''' <remarks></remarks>
        Public Shared Dispensing As Integer = 12
        ''' <summary>產品資料載入</summary>
        ''' <remarks></remarks>
        Public Shared ProductLoading As Integer = 13
        ''' <summary>退料</summary>
        ''' <remarks></remarks>
        Public Shared ProductUnload As Integer = 14
        ''' <summary>進料</summary>
        ''' <remarks></remarks>
        Public Shared Loading As Integer = 15

        ''' <summary>閥自動校正</summary>
        ''' <remarks></remarks>
        Public Shared AutoValveCalibration As Integer = 16
        ''' <summary>A機進料</summary>
        ''' <remarks></remarks>
        Public Shared LoadA As Integer = 17
        ''' <summary>B機進料</summary>
        ''' <remarks></remarks>
        Public Shared LoadB As Integer = 18
        ''' <summary>A機退料</summary>
        ''' <remarks></remarks>
        Public Shared UnloadA As Integer = 19
        ''' <summary>B機退料</summary>
        ''' <remarks></remarks>
        Public Shared UnloadB As Integer = 20
        ''' <summary>手動流程</summary>
        ''' <remarks></remarks>
        Public Shared ManualAction As Integer = 21
        ''' <summary>Rerun流程</summary>
        ''' <remarks></remarks>
        Public Shared Rerun As Integer = 22
        ''' <summary>[強制退料]</summary>
        ''' <remarks></remarks>
        Public Shared AbnormalUnload As Integer = 23
        ''' <summary>[點膠前動作]</summary>
        ''' <remarks></remarks>
        Public Shared PrevDispense As Integer = 24
        ''' <summary>最大值</summary>
        ''' <remarks></remarks>
        Public Shared Max As Integer = 24
    End Structure

    ''' <summary>
    ''' Stage移動的軸向
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmStageAxis
        eXAxis = 0
        eY1Axis = 1
        eZAxis = 2
        eXY1Axis = 3
    End Enum

    Public Structure enmMDO
        Public Shared GlueOn As Integer = 5
    End Structure

    ''' <summary>
    ''' 方向
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eDirection
        ''' <summary>正向</summary>
        ''' <remarks></remarks>
        Positive = 0
        ''' <summary>反向</summary>
        ''' <remarks></remarks>
        Negative = 1
    End Enum

    ''' <summary>[圓弧方向]</summary>
    ''' <remarks></remarks>
    Public Enum eArcDirection
        ''' <summary>[順時針]</summary>
        ''' <remarks></remarks>
        CW = 0
        ''' <summary>[逆時針]</summary>
        ''' <remarks></remarks>
        CCW = 1
    End Enum


    ''' <summary>IO點位配接</summary>
    ''' <remarks></remarks>
    Public Sub SetIOIndex()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                enmDI.EMS = 0
                enmDI.CDA = 1
                enmDI.EMO = 3
                enmDI.DoorClose = 4
                enmDI.MC2 = 5
                enmDI.MC3 = 6
                enmDI.StartButton = 8
                enmDI.PauseButton = 9
                enmDI.ResetButton = 10
                enmDI.BoardAvailable = 12
                enmDI.MachineReadyToRecieve = 13
                enmDI.PrevAlarm = 14
                enmDI.NextAlarm = 15
                enmDI.ValveControllerAlarm1 = 16
                enmDI.TriggerBoardReady1 = 17
                enmDI.TriggerBoardAlarm1 = 18
                enmDI.ValveControllerAlarm2 = 20
                enmDI.TriggerBoardReady2 = 21
                enmDI.TriggerBoardAlarm2 = 22
                enmDI.CcdBusy = 24
                enmDI.CCDBusy2 = 25
                enmDI.Station2StopperUpReady = 28
                enmDI.Station2StopperDownReady = 29
                enmDI.Station1StopperUpReady = 30
                enmDI.Station1StopperDownReady = 31
                enmDI.MoveInMotorAlarm = 32
                enmDI.Station2TrayReady = 34
                enmDI.Station2TrayInSensor = 35
                enmDI.Station2TrayOutSensor = 36
                enmDI.Station2Heater1CylinderUpReady = 40
                enmDI.Station2Heater1CylinderDownReady = 41
                enmDI.Station2Heater2CylinderUpReady = 42
                enmDI.Station2Heater2CylinderDownReady = 43
                enmDI.Station2Heater3CylinderUpReady = 44
                enmDI.Station2Heater3CylinderDownReady = 45
                enmDI.Station2Heater4CylinderUpReady = 46
                enmDI.Station2Heater4CylinderDownReady = 47
                enmDI.Station2Heater5CylinderUpReady = 48
                enmDI.Station2Heater5CylinderDownReady = 49
                enmDI.Station2Heater6CylinderUpReady = 50
                enmDI.Station2Heater6CylinderDownReady = 51
                enmDI.PurgeVacuumReady = 52
                enmDI.PurgeVacuumReady2 = 54
                enmDI.DetectSyringeSensor1 = 56
                enmDI.DetectSyringeSensor2 = 58
                enmDI.OverTemperature = 64
                enmDI.OverTemperature2 = 65
                enmDI.OverTemperature3 = 66
                enmDI.OverTemperature4 = 67
                enmDI.OverTemperature5 = 68
                enmDI.OverTemperature6 = 69
                enmDI.HeaterAlarm1 = 72
                enmDI.HeaterAlarm2 = 73
                enmDI.HeaterAlarm3 = 74
                enmDI.HeaterAlarm4 = 75
                enmDI.HeaterAlarm5 = 76
                enmDI.HeaterAlarm6 = 77
                enmDI.Station2ChuckVacuumReady = 80
                enmDI.Station2ChuckVacuumReady2 = 81
                enmDI.Station2ChuckVacuumReady3 = 82
                enmDI.Station2ChuckVacuumReady4 = 83
                enmDI.Station2ChuckVacuumReady5 = 84
                enmDI.Station2ChuckVacuumReady6 = 85
                enmDI.OverTemperature7 = 96
                enmDI.OverTemperature8 = 97
                enmDI.OverTemperature9 = 98
                enmDI.OverTemperature10 = 99
                enmDI.OverTemperature11 = 100
                enmDI.OverTemperature12 = 101
                enmDI.HeaterAlarm7 = 104
                enmDI.HeaterAlarm8 = 105
                enmDI.HeaterAlarm9 = 106
                enmDI.HeaterAlarm10 = 107
                enmDI.HeaterAlarm11 = 108
                enmDI.HeaterAlarm12 = 109
                enmDI.Station3ChuckVacuumReady = 112
                enmDI.Station3ChuckVacuumReady2 = 113
                enmDI.Station3ChuckVacuumReady3 = 114
                enmDI.Station3ChuckVacuumReady4 = 115
                enmDI.Station3ChuckVacuumReady5 = 116
                enmDI.Station3ChuckVacuumReady6 = 117
                enmDI.EMS2 = 128
                enmDI.CDA2 = 129
                enmDI.DoorClose2 = 132
                enmDI.MC_Motor2 = 133
                enmDI.MC_Heater2 = 134
                enmDI.StartButton2 = 136
                enmDI.PauseButton2 = 137
                enmDI.ResetButton2 = 138
                enmDI.BoardAvailable2 = 140
                enmDI.MachineReadyToRecieve2 = 141
                enmDI.PrevAlarm2 = 142
                enmDI.NextAlarm2 = 143
                enmDI.ValveControllerAlarm3 = 144
                enmDI.TriggerBoardReady3 = 145
                enmDI.TriggerBoardAlarm3 = 146
                enmDI.ValveControllerAlarm4 = 148
                enmDI.DetectSyringeSensor4 = 149
                enmDI.TriggerBoardAlarm4 = 150
                enmDI.CCDBusy3 = 152
                enmDI.CCDBusy4 = 153
                enmDI.Station3StopperUpReady = 156
                enmDI.Station3StopperDownReady = 157
                enmDI.MoveInMotorAlarm2 = 160
                enmDI.Station3TrayReady = 162
                enmDI.Station3TrayInSensor = 163
                enmDI.Station3TrayOutSensor = 164
                enmDI.Station23ConcateTraySensor = 165
                enmDI.Station3Heater1CylinderUpReady = 168
                enmDI.Station3Heater1CylinderDownReady = 169
                enmDI.Station3Heater2CylinderUpReady = 170
                enmDI.Station3Heater2CylinderDownReady = 171
                enmDI.Station3Heater3CylinderUpReady = 172
                enmDI.Station3Heater3CylinderDownReady = 173
                enmDI.Station3Heater4CylinderUpReady = 174
                enmDI.Station3Heater4CylinderDownReady = 175
                enmDI.Station3Heater5CylinderUpReady = 176
                enmDI.Station3Heater5CylinderDownReady = 177
                enmDI.Station3Heater6CylinderUpReady = 178
                enmDI.Station3Heater6CylinderDownReady = 179
                enmDI.PurgeVacuumReady3 = 180
                enmDI.PurgeVacuumReady4 = 182
                enmDI.DetectSyringeSensor3 = 184
                enmDI.DetectSyringeSensor4 = 186
                enmDI.Max = 186
                '*******************************************************************************
                enmDO.RedIndicator = 0
                enmDO.YellowIndicator = 1
                enmDO.GreenIndicator = 2
                enmDO.BlueIndicator = 3
                enmDO.Buzzer = 4
                enmDO.DoorLock = 5
                enmDO.StartButtonLight = 8
                enmDO.PauseButtonLight = 9
                enmDO.MachineReadyToRecieve = 12
                enmDO.BoardAvailable = 13
                enmDO.SystemAlarm = 14
                enmDO.TriggerBoardReset1 = 15
                enmDO.LaserReaderReset1 = 16
                enmDO.ValveControllerReset1 = 17
                enmDO.DispensingTrigger1 = 18
                enmDO.ValveAugerDir1 = 19
                enmDO.LaserReaderReset2 = 20
                enmDO.ValveControllerReset2 = 21
                enmDO.DispensingTrigger2 = 22
                enmDO.ValveAugerDir2 = 23
                enmDO.CcdImageTrigger = 24
                enmDO.CcdImageTrigger2 = 25
                enmDO.TriggerBoardReset2 = 26
                enmDO.Station2StopperUp = 28
                enmDO.Station2StopperDown = 29
                enmDO.Station1StopperUpDown = 30
                enmDO.Station1StopperDown = 31
                enmDO.MoveInMotorCW = 32
                enmDO.MoveInMotorCCW = 33
                enmDO.MoveInMotorSlow = 34
                enmDO.MoveInMotorReset = 35
                enmDO.CCDLight = 36
                enmDO.CCDLight2 = 37
                enmDO.CCDLight3 = 38
                enmDO.CCDLight4 = 39
                enmDO.HeaterCylinderUp1 = 40
                enmDO.HeaterCylinderDown1 = 41
                enmDO.HeaterCylinderUp2 = 42
                enmDO.HeaterCylinderDown2 = 43
                enmDO.HeaterCylinderUp3 = 44
                enmDO.HeaterCylinderDown3 = 45
                enmDO.HeaterCylinderUp4 = 46
                enmDO.HeaterCylinderDown4 = 47
                enmDO.HeaterCylinderUp5 = 48
                enmDO.HeaterCylinderDown5 = 49
                enmDO.HeaterCylinderUp6 = 50
                enmDO.HeaterCylinderDown6 = 51
                enmDO.Purge = 52
                enmDO.PurgeVacuumBreaker = 53
                enmDO.PurgeVacuum2 = 54
                enmDO.PurgeVacuumBreaker2 = 55
                enmDO.SyringePressure1 = 56
                enmDO.SyringePressure2 = 57
                enmDO.HeaterOn1 = 64
                enmDO.HeaterOn2 = 65
                enmDO.HeaterOn3 = 66
                enmDO.HeaterOn4 = 67
                enmDO.HeaterOn5 = 68
                enmDO.HeaterOn6 = 69
                enmDO.Station2ChuckVacuum = 72
                enmDO.Station2ChuckVacuum2 = 73
                enmDO.Station2ChuckVacuum3 = 74
                enmDO.Station2ChuckVacuum4 = 75
                enmDO.Station2ChuckVacuum5 = 76
                enmDO.Station2ChuckVacuum6 = 77
                enmDO.Station2ChuckVacuum = 80
                enmDO.Station2ChuckVacuum2 = 81
                enmDO.Station2ChuckVacuum3 = 82
                enmDO.Station2ChuckVacuum4 = 83
                enmDO.Station2ChuckVacuum5 = 84
                enmDO.Station2ChuckVacuum6 = 85
                enmDO.HeaterOn7 = 96
                enmDO.HeaterOn8 = 97
                enmDO.HeaterOn9 = 98
                enmDO.HeaterOn10 = 99
                enmDO.HeaterOn11 = 100
                enmDO.HeaterOn12 = 101
                enmDO.Station3ChuckVacuumBreak1 = 104
                enmDO.Station3ChuckVacuumBreak2 = 105
                enmDO.Station3ChuckVacuumBreak3 = 106
                enmDO.Station3ChuckVacuumBreak4 = 107
                enmDO.Station3ChuckVacuumBreak5 = 108
                enmDO.Station3ChuckVacuumBreak6 = 109
                enmDO.Station3ChuckVacuum = 112
                enmDO.Station3ChuckVacuum2 = 113
                enmDO.Station3ChuckVacuum3 = 114
                enmDO.Station3ChuckVacuum4 = 115
                enmDO.Station3ChuckVacuum5 = 116
                enmDO.Station3ChuckVacuum6 = 117
                enmDO.DoorLock2 = 133
                enmDO.StartButtonLight2 = 136
                enmDO.PauseButtonLight2 = 137
                enmDO.MachineReadyToRecieve2 = 140
                enmDO.BoardAvailable2 = 141
                enmDO.SystemAlarm2 = 142
                enmDO.TriggerBoardReset3 = 143
                enmDO.LaserReaderReset3 = 144
                enmDO.ValveControllerReset3 = 145
                enmDO.DispensingTrigger3 = 146
                enmDO.ValveAugerDir3 = 147
                enmDO.ResetLaserReader4 = 148
                enmDO.ValveControllerReset4 = 149
                enmDO.DispensingTrigger4 = 150
                enmDO.ValveAugerDir4 = 151
                enmDO.CcdImageTrigger3 = 152
                enmDO.CcdImageTrigger4 = 153
                enmDO.TriggerBoardReset4 = 154
                enmDO.Station3StopperUp = 156
                enmDO.Station3StopperDown = 157
                enmDO.MoveInMotorCW2 = 160
                enmDO.MoveInMotorCCW2 = 161
                enmDO.MoveInMotorSlow2 = 162
                enmDO.MoveInMotorReset2 = 163
                enmDO.CCDLight5 = 164
                enmDO.CCDLight6 = 165
                enmDO.CCDLight7 = 166
                enmDO.CCDLight8 = 167
                enmDO.HeaterCylinderUp7 = 168
                enmDO.HeaterCylinderDown7 = 169
                enmDO.HeaterCylinderUp8 = 170
                enmDO.HeaterCylinderDown8 = 171
                enmDO.HeaterCylinderUp9 = 172
                enmDO.HeaterCylinderDown9 = 173
                enmDO.HeaterCylinderUp10 = 174
                enmDO.HeaterCylinderDown10 = 175
                enmDO.HeaterCylinderUp11 = 176
                enmDO.HeaterCylinderDown11 = 177
                enmDO.HeaterCylinderUp12 = 178
                enmDO.HeaterCylinderDown12 = 179
                enmDO.PurgeVacuum3 = 180
                enmDO.PurgeVacuumBreaker3 = 181
                enmDO.PurgeVacuum4 = 182
                enmDO.PurgeVacuumBreaker4 = 183
                enmDO.SyringePressure3 = 184
                enmDO.SyringePressure4 = 185
                enmDO.Max = 185

            Case enmMachineType.eDTS330ACR1
                enmDI.CDA = 0
                enmDI.EMO = 1
                enmDI.DoorClose = 2
                enmDI.OverTemperature = 3
                enmDI.MC1 = 4
                enmDI.MC2 = 5
                enmDI.MC3 = 6
                enmDI.DetectSyringeSensor1 = 7
                enmDI.DetectSyringeSensor2 = 8
                enmDI.ValveControllerAlarm2 = 9
                enmDI.ValveControllerAlarm1 = 11
                enmDI.TriggerBoardAlarm1 = 13
                enmDI.TriggerBoardAlarm2 = 14
                enmDI.PurgeVacuumReady = 29
                enmDI.ValveCylUpReady1 = 18
                enmDI.ValveCylDownReady1 = 19
                enmDI.ClearGlueClampOnSensor = 20
                enmDI.ClearGlueClampOffSensor = 21
                enmDI.CcdGate = 22
                enmDI.CcdOutputResult = 23
                enmDI.CcdBusy = 24
                enmDI.CcdReady = 25
                enmDI.CcdAlarm = 26
                enmDI.CcdDO1 = 27
                enmDI.CcdDO2 = 28
                enmDI.ConveyerReady = 31
                enmDI.Max = 31
                '*******************************************************************************
                enmDO.SystemOn = 0
                enmDO.SystemAlarm = 1
                enmDO.UnlockZAxis = 2
                enmDO.LaserReaderReset1 = 3
                enmDO.CCDLight = 5
                enmDO.SyringePressure1 = 7
                enmDO.SyringePressure2 = 8
                enmDO.Screw1ControllerReset = 9
                enmDO.Screw2ControllerReset = 10
                enmDO.ValveControllerReset1 = 11
                enmDO.ValveControllerReset2 = 12
                enmDO.TriggerBoardReset1 = 13
                enmDO.TriggerBoardReset2 = 14
                enmDO.Purge = 15
                enmDO.ValveCylUp1 = 18
                enmDO.ValveCylDown1 = 19
                enmDO.ClearGlueClampOn = 20
                enmDO.DummyVacuum = 21
                enmDO.DispenserNo1ScrewValveCCW = 22
                enmDO.DispenserNo2ScrewValveCCW = 23
                enmDO.Pump = 24
                enmDO.MotionCardReset = 25
                enmDO.CcdImageTrigger = 26
                enmDO.CcdBit0 = 27
                enmDO.CcdBit1 = 28
                enmDO.CcdReset = 29
                enmDO.Max = 29
                '*******************************************************************************
                enmAI.DispenserNo1EPRegulator = 0
                enmAI.DispenserNo2EPRegulator = 1
                enmAI.DispenserNo1OverloadCT = 2
                enmAI.DispenserNo2OverloadCT = 3
                enmAI.LaserReader = 4
                enmAI.Max = 4
                '*******************************************************************************
                enmAO.DispenserNo1EPRegulator = 0
                enmAO.DispenserNo2EPRegulator = 1
                enmAO.Max = 1

            Case enmMachineType.eDTS300A
                enmDI.CDA = 0
                enmDI.EMO = 1
                enmDI.DoorClose = 2
                enmDI.OverTemperature = 3
                enmDI.MC1 = 4
                enmDI.MC2 = 5
                enmDI.MC3 = 6
                enmDI.DetectSyringeSensor1 = 7
                enmDI.DetectSyringeSensor2 = 8
                enmDI.ValveControllerAlarm2 = 9
                enmDI.ValveControllerAlarm1 = 11
                enmDI.TriggerBoardAlarm1 = 13
                enmDI.TriggerBoardAlarm2 = 14
                enmDI.ConveyerReady = 15
                enmDI.ValveCylUpReady1 = 18
                enmDI.ValveCylDownReady1 = 19
                enmDI.ClearGlueClampOnSensor = 20
                enmDI.CcdGate = 22
                enmDI.CcdOutputResult = 23
                enmDI.CcdBusy = 24
                enmDI.CcdReady = 25
                enmDI.CcdAlarm = 26
                enmDI.CcdDO1 = 27
                enmDI.CcdDO2 = 28
                enmDI.PlateformVacuumReady = 30
                enmDI.Max = 30
                '*******************************************************************************
                enmDO.SystemOn = 0
                enmDO.SystemAlarm = 1
                enmDO.UnlockZAxis = 2
                enmDO.LaserReaderReset1 = 3
                enmDO.DoorLock = 4
                enmDO.SyringePressure1 = 7
                enmDO.SyringePressure2 = 8
                enmDO.Screw1ControllerReset = 9
                enmDO.Screw2ControllerReset = 10
                enmDO.ValveControllerReset1 = 11
                enmDO.ValveControllerReset2 = 12
                enmDO.TriggerBoardReset1 = 13
                enmDO.TriggerBoardReset2 = 14
                enmDO.Purge = 15
                enmDO.ValveCylUp1 = 18
                enmDO.ValveCylDown1 = 19
                enmDO.ClearGlueClampOn = 20
                enmDO.DummyVacuum = 21
                enmDO.DispenserNo1ScrewValveCCW = 22
                enmDO.DispenserNo2ScrewValveCCW = 23
                enmDO.Pump = 24
                enmDO.CcdImageTrigger = 26
                enmDO.CcdBit0 = 27
                enmDO.CcdBit1 = 28
                enmDO.CcdReset = 29
                enmDO.Max = 29
                '*******************************************************************************
                enmAI.DispenserNo1EPRegulator = 0
                enmAI.DispenserNo2EPRegulator = 1
                enmAI.DispenserNo1OverloadCT = 2
                enmAI.DispenserNo2OverloadCT = 3
                enmAI.LaserReader = 4
                enmAI.Max = 4
                '*******************************************************************************
                enmAO.DispenserNo1EPRegulator = 0
                enmAO.DispenserNo2EPRegulator = 1
                enmAO.Max = 1

            Case enmMachineType.eDTS330A
                enmDI.CDA = 0
                enmDI.EMO = 1
                enmDI.DoorClose = 2
                enmDI.OverTemperature = 3
                enmDI.MC1 = 4
                enmDI.MC2 = 5
                enmDI.MC3 = 6
                enmDI.DetectSyringeSensor1 = 7
                enmDI.DetectSyringeSensor2 = 8
                enmDI.ValveControllerAlarm2 = 9
                enmDI.ValveControllerAlarm1 = 11
                enmDI.TriggerBoardAlarm2 = 14
                enmDI.CDA2 = 15
                enmDI.ValveCylUpReady1 = 18
                enmDI.ValveCylDownReady1 = 19
                enmDI.ClearGlueClampOnSensor = 20
                enmDI.ClearGlueClampOffSensor = 21
                enmDI.CcdGate = 22
                enmDI.CcdOutputResult = 23
                enmDI.CcdBusy = 24
                enmDI.CcdReady = 25
                enmDI.CcdAlarm = 26
                enmDI.CcdDO1 = 27
                enmDI.CcdDO2 = 28
                enmDI.PurgeVacuumReady = 29
                enmDI.PlateformVacuumReady = 30
                enmDI.BoardAvailable = 32
                enmDI.MachineReadyToRecieve = 33
                enmDI.Station1ChuckVacuumReady = 34
                enmDI.Station2ChuckVacuumReady = 35
                enmDI.Station3ChuckVacuumReady = 36
                enmDI.Station1TrayReady = 37
                enmDI.Station2TrayReady = 38
                enmDI.Station3TrayReady = 39
                enmDI.TrayClamperOnReady = 40
                enmDI.TrayClamperOffReady = 41
                enmDI.Station1StopperUpReady = 42
                enmDI.Station1StopperDownReady = 43
                enmDI.Station2StopperUpReady = 44
                enmDI.Station2StopperDownReady = 45
                enmDI.Station3StopperUpReady = 46
                enmDI.Station3StopperDownReady = 47
                enmDI.Station1TopLiftUpReady = 48
                enmDI.Station1TopLiftDownReady = 49
                enmDI.Station2TopLiftUpReady = 50
                enmDI.Station2TopLiftDownReady = 51
                enmDI.Station3TopLiftUpReady = 52
                enmDI.Station3TopLiftDownReady = 53
                enmDI.TriggerBoardAlarm1 = 54
                enmDI.TriggerBoardReady1 = 55
                enmDI.Max = 55
                '*******************************************************************************
                enmDO.RedIndicator = 0
                enmDO.YellowIndicator = 1
                enmDO.GreenIndicator = 2
                enmDO.BlueIndicator = 3
                enmDO.Buzzer = 4
                enmDO.Station2Unlock = 5
                enmDO.UnlockZAxis = 6
                enmDO.SyringePressure1 = 7
                enmDO.SyringePressure2 = 8
                enmDO.Screw1ControllerReset = 9
                enmDO.Screw2ControllerReset = 10
                enmDO.ValveControllerReset1 = 11
                enmDO.ValveControllerReset2 = 12
                enmDO.TriggerBoardReset1 = 13
                enmDO.TriggerBoardReset2 = 14
                enmDO.HeaterControllerReset = 15
                enmDO.HeaterPower = 16
                enmDO.DoorLock = 17
                enmDO.ValveCylUp1 = 18
                enmDO.ValveCylDown1 = 19
                enmDO.ClearGlueClampOn = 20
                enmDO.Purge = 21
                enmDO.DummyVacuum = 22
                enmDO.DispenserNo1ScrewValveCCW = 23
                enmDO.DispenserNo2ScrewValveCCW = 24
                enmDO.MotionCardReset = 25
                enmDO.CcdImageTrigger = 26
                enmDO.CcdBit0 = 27
                enmDO.CcdBit1 = 28
                enmDO.CcdReset = 29
                enmDO.LaserReaderReset1 = 30
                enmDO.MachineReadyToRecieve = 32
                enmDO.BoardAvailable = 33
                enmDO.Station1TopLiftUpDown = 34
                enmDO.Station2TopLiftUpDown = 35
                enmDO.Station3TopLiftUpDown = 36
                enmDO.Station1ChuckVacuum = 37
                enmDO.Station2ChuckVacuum = 38
                enmDO.Station3ChuckVacuum = 39
                enmDO.TrayClamper = 40
                enmDO.HoldBack = 41
                enmDO.Station1StopperUpDown = 42
                enmDO.Station2StopperUp = 43
                enmDO.Station3StopperUp = 44
                enmDO.SteppingMotor = 45
                enmDO.Max = 45
                '*******************************************************************************
                enmAI.DispenserNo1EPRegulator = 0
                enmAI.DispenserNo2EPRegulator = 1
                enmAI.DispenserNo1OverloadCT = 2
                enmAI.DispenserNo2OverloadCT = 3
                enmAI.LaserReader = 4
                enmAI.Max = 4
                '*******************************************************************************
                enmAO.DispenserNo1EPRegulator = 0
                enmAO.DispenserNo2EPRegulator = 1
                enmAO.Max = 1

            Case enmMachineType.DCS_F230A
                'TODO:待補
            Case enmMachineType.DCS_350A
                'TODO:待補
            Case enmMachineType.DCS_500AD
                'TODO:待補

        End Select
    End Sub
    ''' <summary>[登入人員等級]</summary>
    ''' <remarks></remarks>
    Public gUserLevel As enmUserLevel

    '20161115
    Public gUsername As String
    Public gUserPassword As String

    Public Enum enmPLCX
        LoadReady = 0
        UnloadReady = 1
        EMO = 3
        Station1TrayReady = 4
        Station2TrayReady = 5
        Station3TrayReady = 6
        AStopTop1 = 8
        AStopTop2 = 9
        AStopTop3 = 10
        AStopBot1 = 11
        AStopBot2 = 12
        AStopBot3 = 13
        AChuckGauge1 = 20
        AChuckGauge2 = 21
        AChuckGauge3 = 22
        DispenserBusy = 23
        ATrayClampOn = 30
        ATrayClampOff = 31
        LoadEMS = 32
        UnloadEMS = 33
        DispenserFinish = 39
    End Enum

    Public Enum enmPLCY
        TrayLoad = 0
        StepperController = 2
        HeaterController1 = 3
        HeaterController2 = 4
        HeaterController3 = 5
        HeaterSystemOn = 6
        AVacSOL1 = 8
        AVacSOL2 = 9
        AVacSOL3 = 10
        BVacSOL1 = 11
        BVacSOL2 = 12
        BVacSOL3 = 13
        DispenserStart = 14
        AStopSOL1 = 16
        AStopSOL2 = 17
        AStopSOL3 = 18
        ARiseSOL1 = 19
        ARiseSOL2 = 20
        ARiseSOL3 = 21
        ConveyorSOL = 22
        TrayAlignSOL = 23
        BStopSOL1 = 24
        BStopSOL2 = 25
        BStopSOL3 = 26
        BRiseSOL1 = 27
        BRiseSOL2 = 28
        BRiseSOL3 = 29
        RedLight = 32
        YellowLight = 33
        GreenLight = 34
        BlueLight = 35
        Buzzer = 36
    End Enum

    Public Enum enmPLCM
        DispenserBusy = 202
        DispenserFinish = 206
        Station2Up = 1416
        Station2Down = 1428
        PCAlarmOutput = 1610
        PLCAlarmInput = 1611
        Station2Vacuum00 = 5000
        Station2Vacuum01 = 5001
        Station2Vacuum02 = 5002
        Station2Vacuum03 = 5003
        Station2Vacuum04 = 5004
        Station2Vacuum05 = 5005
        Station2Vacuum06 = 5006
        Station2Vacuum07 = 5007
        Station2Vacuum08 = 5008
        Station2Vacuum09 = 5009
        Station2Vacuum10 = 5010
        Station2Vacuum11 = 5011
        Station2Vacuum12 = 5012
        Station2Vacuum13 = 5013
        Station2Vacuum14 = 5014
        Station2Vacuum15 = 5015
        Station2Vacuum16 = 5016
        Station2Vacuum17 = 5017
        Station2Vacuum18 = 5018
        Station2Vacuum19 = 5019
        Station2Vacuum20 = 5020
        Station2Vacuum21 = 5021
        Station2Vacuum22 = 5022
        Station2Vacuum23 = 5023
        Station2Vacuum24 = 5024
        Station2Vacuum25 = 5025
        Station2Vacuum26 = 5026
        Station2Vacuum27 = 5027
        Station2Vacuum28 = 5028
        Station2Vacuum29 = 5029
        Station2Vacuum30 = 5030
        Station2Vacuum31 = 5031
        Station2Vacuum32 = 5032
        Station2Vacuum33 = 5033
        Station2Vacuum34 = 5034
        Station2Vacuum35 = 5035
    End Enum

    ''' <summary>
    ''' 光控器
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmLightController
        No1 = 0
        No2 = 1
        No3 = 2
        No4 = 3
        Max = No4
    End Enum

    ''' <summary>CCD清單</summary>
    ''' <remarks></remarks>
    Public Structure enmCCD
        Public Shared None As Integer = -1
        Public Shared CCD1 As Integer = 0
        Public Shared CCD2 As Integer = 1
        Public Shared CCD3 As Integer = 2
        Public Shared CCD4 As Integer = 3
        Public Shared Max As Integer = 0
        ''' <summary>用於讀檔前開陣列空間用</summary>
        ''' <remarks></remarks>
        Public Const ConstMax As Integer = 3

        Public Shared Function LoadAOIIndex(ByVal strFileName As String) As Boolean
            Dim strSection As String
            strSection = "AOIIndex"
            enmCCD.Max = Val(ReadIniString(strSection, "CCD.Max", strFileName, 0)) '
            Return True
        End Function

        Public Shared Sub SaveAOIIndex(ByVal strFileName As String)
            Dim strSection As String
            strSection = "AOIIndex"
            Call SaveIniString(strSection, "CCD.Max", CInt(enmCCD.Max), strFileName)
        End Sub
    End Structure

    Public Structure sCCDTargetData
        ''' <summary>[標靶類型]</summary>
        ''' <remarks></remarks>
        Public CCDTargetType As enmCCDTargetType
        ''' <summary>[標靶顏色]</summary>
        ''' <remarks></remarks>
        Public CCDTargetColor As enmCCDTargetColor
        ''' <summary>[圓半徑]</summary>
        ''' <remarks></remarks>
        Public Radius As Decimal
        ''' <summary> [高] </summary>
        ''' <remarks></remarks>
        Public Height As Decimal
        ''' <summary>[寬] </summary>
        ''' <remarks></remarks>
        Public Width As Decimal
    End Structure

    Public Enum eMapEditType
        SingleDie = 0
        Row = 1 '→
        Column = 2 '↓
        All = 3
    End Enum

    ''' <summary>通訊結果傳回結構</summary>
    ''' <remarks></remarks>
    Public Structure sReceiveStatus
        ''' <summary>[是否接收完成]</summary>
        ''' <remarks></remarks>
        Public Status As Boolean
        ''' <summary>[原始字串]</summary>
        ''' <remarks></remarks>
        Public STR As String
        ''' <summary>[結果(處理完的資料內容)]</summary>
        ''' <remarks></remarks>
        Public Value As String
    End Structure
End Module

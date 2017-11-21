
Public Module MSystemParameter

#Region "光學檢測AOI"
    ''' <summary>
    ''' CCD類型
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmCCDType
        None = 0
        ''' <summary>歐姆龍FZS2M 使用UDP通訊</summary>
        ''' <remarks></remarks>
        OmronFZS2MUDP = 1
        ''' <summary>歐姆龍FZS2M 使用TCP通訊</summary>
        ''' <remarks></remarks>
        OmronFZS2MTCP = 2
        ''' <summary>基恩斯 使用TCP通訊</summary>
        ''' <remarks></remarks>
        KeyenceCV200CTCP = 3
        ''' <summary>康耐視</summary>
        ''' <remarks></remarks>
        CognexVPRO = 4
        ''' <summary>Halcon</summary>
        ''' <remarks></remarks>
        Halcon = 5
    End Enum
    Public Function GetCCDTypeString(ByVal ccdType As enmCCDType) As String
        Select Case ccdType
            Case enmCCDType.CognexVPRO
                Return "Cognex VPRO"
            Case enmCCDType.KeyenceCV200CTCP
                Return "Keyence CV200C TCP"
            Case enmCCDType.OmronFZS2MTCP
                Return "Omron FZS2M TCP"
            Case enmCCDType.OmronFZS2MUDP
                Return "Omron FZS2M UDP"
            Case enmCCDType.Halcon
                Return "Halcon"
        End Select
        Return "Undefined."
    End Function

    ''' <summary>
    ''' Cognex  連線設定 目前可支援CCD裝置
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmCCDDeciveType
        None = -1
        ''' <summary>康耐視</summary>
        ''' <remarks></remarks>
        COGNEXCIC2000 = 0
        ''' <summary>一寶實達</summary>
        ''' <remarks></remarks>
        BFLYPGE20E4C = 1
    End Enum
    ''' <summary>
    '''  影像格式
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmVideoFormatType
        None = -1
        ''' <summary>黑白:Mono8</summary>
        ''' <remarks></remarks>
        Mono = 0
        ''' <summary>彩色:Bayer</summary>
        ''' <remarks></remarks>
        Bayer = 1
    End Enum

    Public Function GetCCDDeciveString(ByVal ccdDeviceType As enmCCDDeciveType) As String
        Select Case ccdDeviceType
            Case enmCCDDeciveType.COGNEXCIC2000
                Return "GigE Vision: COGNEX: CIC-2000"
            Case enmCCDDeciveType.BFLYPGE20E4C
                Return "GigE Vision: Point Grey Research: Blackfly BFLY-PGE-20E4C"
        End Select
        Return "Undefined."
    End Function
    Public Function GetCCDStringToDeciveNum(ByVal ccdDeviceType As String) As Integer
        Select Case ccdDeviceType
            Case "GigE Vision: COGNEX: CIC-2000"
                Return enmCCDDeciveType.COGNEXCIC2000
            Case "GigE Vision: Point Grey Research: Blackfly BFLY-PGE-20E4C"
                Return enmCCDDeciveType.BFLYPGE20E4C
        End Select
        Return enmCCDDeciveType.None
    End Function

    ''' <summary>
    ''' 目前只開放支援Mono與RGB
    ''' </summary>
    ''' <param name="ccdVideoType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TransformCognexVideoStrToNum(ByVal ccdVideoType As String) As Integer
        'Generic GigEVision (Bayer Color)
        'Generic GigEVision (Mono)
        'Generic GigEVision (Mono12 Packed)
        'Generic GigEVision (Mono16)
        'Generic GigEVision (RGB8)
        'Generic GigEVision (YUV422 Packed)

        Select Case ccdVideoType
            Case "Generic GigEVision (Mono)"
                Return enmVideoFormatType.Mono
            Case "Generic GigEVision (Bayer Color)"
                Return enmVideoFormatType.Bayer
        End Select
        Return enmVideoFormatType.None
    End Function

    'Generic GigEVision (Bayer Color)
    'Generic GigEVision (Mono)
    'Generic GigEVision (Mono12 Packed)
    'Generic GigEVision (Mono16)
    'Generic GigEVision (RGB8)
    'Generic GigEVision (YUV422 Packed)
    Public Function TransformVideoNumToCognexStr(ByVal ccdVideoNum As Integer) As String
        Select Case ccdVideoNum
            Case enmVideoFormatType.Mono
                Return "Generic GigEVision (Mono)"
            Case enmVideoFormatType.Bayer
                Return "Generic GigEVision (Bayer Color)"
        End Select
        Return ""
    End Function

    Public Function TransformVideoNumToStr(ByVal ccdVideoNum As Integer) As String
        Select Case ccdVideoNum
            Case enmVideoFormatType.Mono
                Return "Mono"
            Case enmVideoFormatType.Bayer
                Return "Bayer"
        End Select
        Return ""
    End Function


    Public Enum enmCCDTargetType
        None = -1
        ''' <summary>
        ''' 十字
        ''' </summary>
        ''' <remarks></remarks>
        Cross = 0
        ''' <summary>
        ''' X
        ''' </summary>
        ''' <remarks></remarks>
        CrossX = 1
        ''' <summary>
        ''' 刻度線
        ''' </summary>
        ''' <remarks></remarks>
        TickMark = 2
        ''' <summary>
        ''' 刻度線 X方向
        ''' </summary>
        ''' <remarks></remarks>
        TickMarkX = 3
        ''' <summary>
        ''' 圓
        ''' </summary>
        ''' <remarks></remarks>
        Circle = 4
        ''' <summary>
        ''' 長方形
        ''' </summary>
        ''' <remarks></remarks>
        Rectangle = 5
    End Enum

    Public Enum enmCCDTargetColor
        None = -1
        Black = 0
        Blue = 1
        Red = 2
        Yellow = 3
    End Enum



    ''' <summary>r
    ''' 抽測方式
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmCcdGlueCheckType
        ''' <summary>[全檢] </summary>
        ''' <remarks></remarks>
        All = 0
        ''' <summary>[隨機抽檢]</summary>
        ''' <remarks></remarks>
        Random = 1
        ''' <summary>[固定抽檢(1~12)] </summary>
        ''' <remarks></remarks>
        Fix = 2
        ''' <summary>[固定檢查有點膠的最後一顆] </summary>
        ''' <remarks></remarks>
        FixLastOne = 3
        ''' <summary>[關閉膠型偵測]</summary>
        ''' <remarks></remarks>
        Bypass = 4
    End Enum
    ''' <summary>搜尋模式定義
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmSearchType

        ''' <summary>[往X方向走 貪食蛇走法]</summary>
        ''' <remarks></remarks>
        X_Snake
        ''' <summary>[往Y方向走 貪食蛇走法]</summary>
        ''' <remarks></remarks>
        Y_Snake
        ''' <summary>[往X方向走  鋸齒狀走法]</summary>
        ''' <remarks></remarks>
        X_ZigZag
        ''' <summary>[往Y方向走  鋸齒狀走法]</summary>
        ''' <remarks></remarks>
        Y_ZigZag
    End Enum

    ''' <summary>
    ''' [Laser測高取值方式]
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmLaserMode
        ''' <summary>
        ''' [平均取值]
        ''' </summary>
        ''' <remarks></remarks>
        AverageHigh = 0
        ''' <summary>
        ''' [取最大值]
        ''' </summary>
        ''' <remarks></remarks>
        MaxHigh = 1
    End Enum
    ''' <summary>
    ''' CCD組態模式(組裝方式)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmCCDModule
        ''' <summary>[CCD不隨Z軸做上下移動] </summary>
        ''' <remarks></remarks>
        eFix = 0
        ''' <summary>[CCD隨Z軸做上下移動]</summary>
        ''' <remarks></remarks>
        eFree = 1
    End Enum
    ''' <summary>測高類型</summary>
    ''' <remarks></remarks>
    Public Enum enmMeasureType
        ''' <summary>不存在(相當於PassIO)</summary>
        ''' <remarks></remarks>
        Contact = 1
        ''' <summary>友上版本0</summary>
        ''' <remarks></remarks>
        Laser = 0
        ''' <summary>PLC版本0</summary>
        ''' <remarks></remarks>
        Both = 2
    End Enum
    ''' <summary>[光源類別]</summary>
    ''' <remarks></remarks>
    Public Enum enmProgramLightType
        None = 0
        KeyenceCV200CTCP = 1
        ''' <summary>
        ''' 丞基技研
        ''' </summary>
        ''' <remarks></remarks>
        GLCTD12V30W = 2
        ''' <summary>
        ''' 晶毓科技 多通道 RS232定電流 LED閃頻控制器
        ''' </summary>
        ''' <remarks></remarks>
        CTK_P_RS = 3
        ''' <summary>
        ''' 奥普特OPT RS232 4Ch 光源控制器
        ''' </summary>
        ''' <remarks></remarks>
        OPT_DP1024_4 = 4
    End Enum
    ''' <summary>
    ''' CCD 運作模式
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmCCDRunType
        ''' <summary>定位</summary>
        ''' <remarks></remarks>
        Fix = 0
        ''' <summary>膠量檢測</summary>
        ''' <remarks></remarks>
        ScanGlue = 1
    End Enum
    ''' <summary>
    ''' CCD 圖像顯示模式
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmCCDImageShowType
        ''' <summary>[動態]</summary>
        ''' <remarks></remarks>
        Dynamic = 0
        ''' <summary>[靜態]</summary>
        ''' <remarks></remarks>
        Freeze = 1
    End Enum
    ''' <summary>CCD對位模式</summary>
    ''' <remarks></remarks>
    Public Enum enmCCDAlignMode
        ''' <summary>不做動作/不計算</summary>
        ''' <remarks></remarks>
        Blind
        ''' <summary>做動作/不計算</summary>
        ''' <remarks></remarks>
        MoveOnly
        ''' <summary>動作/計算</summary>
        ''' <remarks></remarks>
        Normal
    End Enum

    ''' <summary>[紀錄Die處理狀態(用在與MapData筆對)(存已作業的狀態)]</summary>
    ''' <remarks></remarks>
    Public Enum eDieStatus
        ''' <summary>[尚未處理]</summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>[定位失敗]</summary>
        ''' <remarks></remarks>
        AlignFail = 1
        ''' <summary>[測高失敗]</summary>
        ''' <remarks></remarks>
        LaserFail = 2
        ''' <summary>[點膠失敗]</summary>
        ''' <remarks></remarks>
        DispensingFail = 3
        ''' <summary>[完成]</summary>
        ''' <remarks></remarks>
        Finish = 4
    End Enum
    ' ''' <summary>
    ' ''' Ccd Function
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Structure SCcdFunction
    '    ''' <summary>[CCD膠量檢測閥值](Total)</summary>
    '    ''' <remarks></remarks>
    '    Public intCcdGlueCheckThreshold As Integer
    '    ''' <summary>[在此批的抽測已經進行了幾顆(Ex:依照35顆抽測頻率 目前已經計數到計幾個了)]</summary>
    '    ''' <remarks></remarks>
    '    Public intCcdGlueCheckCount As Integer
    '    ''' <summary>[在此批的抽測已經進行了幾顆(Ex:依照35顆抽測頻率 要抽測哪一顆)]</summary>
    '    ''' <remarks></remarks>
    '    Public intCcdGlueCheckIndex As Integer
    '    ''' <summary>[膠量抽測方式]</summary>
    '    ''' <remarks></remarks>
    '    Public typCcdGlueCheckType As enmCcdGlueCheckType
    '    ''' <summary>[膠量檢查頻率]</summary>
    '    ''' <remarks></remarks>
    '    Public intCcdGlueCheckFrequency As Integer
    '    ''' <summary>[膠量固定抽測哪幾顆]</summary>
    '    ''' <remarks></remarks>
    '    Public lstCcdGlueCheckList As List(Of Integer)
    '    ''' <summary>[CCD影像定位之相似度閥值]</summary>
    '    ''' <remarks></remarks>
    '    Public intCcdSimilarThreshold As Integer
    '    ''' <summary>[CCD膠量Shift檢測之閥值]</summary>
    '    ''' <remarks></remarks>
    '    Public intCcdGlueShiftThreshold As Integer
    '    ''' <summary>[CCD膠量檢測閥值](AreaNo1)</summary>
    '    ''' <remarks></remarks>
    '    Public intCcdGlueCheckAreaNo1Threshold As Integer
    '    ''' <summary>[CCD膠量檢測閥值](AreaNo2) </summary>
    '    ''' <remarks></remarks>
    '    Public intCcdGlueCheckAreaNo2Threshold As Integer
    '    ''' <summary>[CCD膠量檢測閥值](AreaNo3)</summary>
    '    ''' <remarks></remarks>
    '    Public intCcdGlueCheckAreaNo3Threshold As Integer
    '    ''' <summary>[CCD膠量檢測閥值](AreaNo4)</summary>
    '    ''' <remarks></remarks>
    '    Public intCcdGlueCheckAreaNo4Threshold As Integer
    '    ''' <summary>[CCD影像定位之膠量覆蓋率](大於此數值，則判定為已點膠，反之則為未點膠)</summary>
    '    ''' <remarks></remarks>
    '    Public intCcdFixGlueCoverRateThreshold As Integer
    '    ''' <summary>[CCD膠量覆蓋率上限]</summary>
    '    ''' <remarks></remarks>
    '    Public intCcdGlueCoverRateLimit As Integer
    'End Structure
    ''' <summary>
    ''' 紀錄CCD補償之數值
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure SCcdOffsetPos
        ''' <summary>第一組影像修正量是否準備完成</summary>
        ''' <remarks></remarks>
        Public IsCCDOffsetReady As Boolean
        ''' <summary>第二組影像修正量是否準備完成</summary>
        ''' <remarks></remarks>
        Public IsCCDOffsetReady2 As Boolean
        ''' <summary>第三組影像修正量是否準備完成</summary>
        ''' <remarks></remarks>
        Public IsCCDOffsetReady3 As Boolean
        ''' <summary>[第一組拍照影像修正量X]</summary>
        ''' <remarks></remarks>
        Public CcdOffsetX As Decimal
        ''' <summary>[第一組拍照影像修正量Y]</summary>
        ''' <remarks></remarks>
        Public CcdOffsetY As Decimal
        ''' <summary>第二組拍照影像修正量X</summary>
        ''' <remarks></remarks>
        Public CCDOffsetX2 As Decimal
        ''' <summary>第二組拍照影像修正量Y</summary>
        ''' <remarks></remarks>
        Public CCDOffsetY2 As Decimal
        ''' <summary>第二組拍照影像修正量X</summary>
        ''' <remarks></remarks>
        Public CCDOffsetX3 As Decimal
        ''' <summary>第二組拍照影像修正量Y</summary>
        ''' <remarks></remarks>
        Public CCDOffsetY3 As Decimal
        ''' <summary>定位點Ticket</summary>
        ''' <remarks></remarks>
        Public Ticket As Integer
        ''' <summary>定位點Ticket2</summary>
        ''' <remarks></remarks>
        Public Ticket2 As Integer
        ''' <summary>定位點Ticket3</summary>
        ''' <remarks></remarks>
        Public Ticket3 As Integer
        ''' <summary>不生產點Ticket</summary>
        ''' <remarks></remarks>
        Public SkipMarkTicket As Integer
        ''' <summary>
        ''' [SkipMark完成的紀錄(確認成功取像並且有找到特徵)]
        ''' </summary>
        ''' <remarks></remarks>
        Public SKFinish As Boolean
        ''' <summary>
        ''' [拍照動作完成的紀錄(確認成功取像並且有找到特徵)-->合Map用(讓另外一側知道需不需要再重新拍照)]
        ''' </summary>
        ''' <remarks></remarks>
        Public CCDFinish() As Boolean
        ''' <summary>[影像修正量R]</summary>
        ''' <remarks></remarks>
        Public CcdRotationAngle As Decimal
        ''' <summary>[省略SkipMark]</summary>
        ''' <remarks></remarks>
        Public IsByPassCCDSkipMarkAction As Boolean
        ''' <summary>[省略CCDScanFix]</summary>
        ''' <remarks></remarks>
        Public IsByPassCCDScanFixAction As Boolean
        ''' <summary>[省略CCDScanGlue]</summary>
        ''' <remarks></remarks>
        Public IsByPassCCDScanGlueAction As Boolean
        ''' <summary>[省略雷射測高]</summary>
        ''' <remarks></remarks>
        Public IsByPassLaserAction As Boolean
        ''' <summary>[省略塗膠]</summary>
        ''' <remarks></remarks>
        Public IsByPassDispensingAction As Boolean
        ''' <summary>[SkipMark時的座標]</summary>
        ''' <remarks></remarks>
        Public SkipMarkPosX As Decimal
        ''' <summary>[SkipMark時的座標]</summary>
        ''' <remarks></remarks>
        Public SkipMarkPosY As Decimal
        ''' <summary>[SkipMark時的座標]</summary>
        ''' <remarks></remarks>
        Public SkipMarkPosZ As Decimal
        ''' <summary>[Scan時的座標]</summary>
        ''' <remarks></remarks>
        Public ScanPosX As Decimal
        ''' <summary>[Scan時的座標]</summary>
        ''' <remarks></remarks>
        Public ScanPosY As Decimal
        ''' <summary>[Scan時的座標]</summary>
        ''' <remarks></remarks>
        Public ScanPosZ As Decimal
        ''' <summary>第二組Scan座標</summary>
        ''' <remarks></remarks>
        Public ScanPosX2 As Decimal
        ''' <summary>第二組Scan座標</summary>
        ''' <remarks></remarks>
        Public ScanPosY2 As Decimal
        ''' <summary>第二組Scan座標</summary>
        ''' <remarks></remarks>
        Public ScanPosZ2 As Decimal
        ''' <summary>第三組定位座標</summary>
        ''' <remarks></remarks>
        Public ScanPosX3 As Decimal
        ''' <summary>第三組定位座標</summary>
        ''' <remarks></remarks>
        Public ScanPosY3 As Decimal
        ''' <summary>第三組定位座標</summary>
        ''' <remarks></remarks>
        Public ScanPosZ3 As Decimal
        ''' <summary>[LaserX位置]</summary>
        ''' <remarks></remarks>
        Public LaserPosX() As Decimal
        ''' <summary>[LaserY位置]</summary>
        ''' <remarks></remarks>
        Public LaserPosY() As Decimal
        ''' <summary>[LaserZ位置]</summary>
        ''' <remarks></remarks>
        Public LaserPosZ() As Decimal
        ''' <summary>[Scan Glue時的座標]</summary>
        ''' <remarks></remarks>
        Public dblScanGluePosX As Decimal
        ''' <summary>[Scan Glue時的座標]</summary>
        ''' <remarks></remarks>
        Public dblScanGluePosY As Decimal
        ''' <summary>[生產]真正確認後基準點X</summary>
        ''' <remarks>CCD定位後將資料填入此處</remarks>
        Public RealBasicPosX As Decimal
        ''' <summary>[生產]真正確認後基準點Y</summary>
        ''' <remarks>CCD定位後將資料填入此處</remarks>
        Public RealBasicPosY As Decimal
        ''' <summary>[生產]真正確認後基準點(角度)</summary>
        ''' <remarks>CCD定位後將資料填入此處</remarks>
        Public RealBasicPosTh As Decimal
        ''' <summary>[旋轉中心X(用於紀錄上一層的資料)]</summary>
        ''' <remarks></remarks>
        Public ParentCenterPosX As Decimal
        ''' <summary>[旋轉中心Y(用於紀錄上一層的資料)]</summary>
        ''' <remarks></remarks>
        Public ParentCenterPosY As Decimal
        ''' <summary>[旋轉角度(用於紀錄上一層的資料)]</summary>
        ''' <remarks></remarks>
        Public ParentTh As Decimal
        ''' <summary>[偏移量(用於紀錄上一層的資料)]</summary>
        ''' <remarks></remarks>
        Public ParentOffsetX As Decimal
        ''' <summary>[偏移量(用於紀錄上一層的資料)]</summary>
        ''' <remarks></remarks>
        Public ParentOffsetY As Decimal
        ''' <summary>[定位是否失敗(用於紀錄上一層的資料)]</summary>
        ''' <remarks></remarks>
        Public IsParentAlignFail As Boolean
    End Structure

    ''' <summary>
    ''' [Die的Mapping Data 結構]
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure BinMappingData
        ''' <summary>
        ''' [Bin Name]
        ''' </summary>
        ''' <remarks></remarks>
        Public BinName As String
        ''' <summary>
        ''' [此顆Die是否不需要點膠(False:表示該顆點膠)]
        ''' </summary>
        ''' <remarks></remarks>
        Public Disable As Boolean
        ''' <summary>
        ''' [本顆Die的PatternName，目前預設為Node的PatternName，預留未來可修改PatternName使用]
        ''' </summary>
        ''' <remarks></remarks>
        Public PatternName As String
        ''' <summary>[紀錄該顆處理狀態]</summary>
        ''' <remarks></remarks>
        Public Status As eDieStatus
    End Structure

    Public Structure AxisPos
        Public PosX As Decimal
        Public PosY As Decimal
        Public PosZ As Decimal
        Public PosA As Decimal
        Public PosB As Decimal
        Public PosC As Decimal
    End Structure
#End Region

#Region "閥件控制Valve"
    ''' <summary>
    ''' 一個平台有幾個閥
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eMechanismModule
        ''' <summary>[沒掛閥]</summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>[單平台單閥(機構沒有第二組閥的上下汽缸)]</summary>
        ''' <remarks></remarks>
        OneValveOneStage = 1
        ''' <summary>[單平台雙閥(機構有第二組閥的上下汽缸)] </summary>
        ''' <remarks></remarks>
        TwoValveOneStage = 2
    End Enum

    ''' <summary>
    ''' 控制閥的使用模式
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmValveWorkType
        ''' <summary>[固定模式(能控制開與關)]</summary>
        ''' <remarks></remarks>
        Permanent = 0
        ''' <summary>[觸發模式(只能控制開 不能控制關 要打完固定發數才會關)]</summary>
        ''' <remarks></remarks>
        Trigger = 1
    End Enum
    ''' <summary>
    ''' 閥體的型態
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmValveType
        ''' <summary>[Jetting Valve]</summary>
        ''' <remarks></remarks>
        Jet = 0
        ''' <summary>[Auger Valve]</summary>
        ''' <remarks></remarks>
        Auger = 1
        ''' <summary>[不使用] </summary>
        ''' <remarks></remarks>
        None = 2
    End Enum

    ''' <summary>[Jet Vavle的種類]</summary>
    ''' <remarks></remarks>
    Public Enum eValveModel
        ''' <summary>[Pico Pulse(EFD)]</summary>
        ''' <remarks></remarks>
        PicoPulse = 0
        ''' <summary>[HV2000(Advanjet)]</summary>
        ''' <remarks></remarks>
        Advanjet = 1

        Max = 1
    End Enum

    Public Enum eValveControllerType
        ''' <summary>未連線</summary>
        ''' <remarks></remarks>
        None
        ''' <summary>自製控制器</summary>
        ''' <remarks></remarks>
        Pandora
        ''' <summary>EFD Pico Pulse閥控制器</summary>
        ''' <remarks></remarks>
        PicoTouch
    End Enum

#End Region

#Region "測高模組Laser"
    ''' <summary>
    ''' Laser Reader之狀態
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmLaserStatus
        eNone = -1
        eOK = 0
        eNG = 1
    End Enum
    ' ''' <summary>
    ' ''' 雷射干涉儀(資料格式之順序)
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum enmLaserInterferometer
    '    Zhigh = 0
    '    TrimGapNo1 = 1
    '    TrimGapNo2 = 2
    '    TrimHighNo1 = 3
    '    TrimHighNo2 = 4
    'End Enum
    ''' <summary>
    ''' 干涉儀類型
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmLaserInterferometerType
        ''' <summary>虛擬</summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>電壓式AI輸入</summary>
        ''' <remarks></remarks>
        KeyenceILS065Voltage = 1
        ''' <summary>線掃描網路輸入</summary>
        ''' <remarks></remarks>
        KeyenceLJV7060TCP = 2
        ''' <summary>RS232通訊</summary>
        ''' <remarks></remarks>
        KeyenceIL065DLRS1A = 3
    End Enum
    ''' <summary>
    ''' Laser 雷射測距數值
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure SLaser
        ''' <summary>
        ''' [該次雷射測高動作是否完成]
        ''' </summary>
        ''' <remarks></remarks>
        Public LaserFinish() As Boolean
        ''' <summary>[Z High]</summary>
        ''' <remarks></remarks>
        Public ZHigh() As Decimal
        ''' <summary>[加工後的Z High數值(取最大or平均)]</summary>
        ''' <remarks></remarks>
        Public FilterZHeigh As Decimal
        ''' <summary>
        ''' [量測後計算完的高度(不包含修正量-->修正量到點膠那段再做處理，因為需考慮到Tilt角度)]
        ''' </summary>
        ''' <remarks></remarks>
        Public RealBasicZHigh As Decimal

    End Structure
    ' ''' <summary>
    ' ''' 干涉儀之偏移量
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Structure SLaserInterferometerOffset
    '    Public dblNo1 As AxisPos
    'End Structure
    ''' <summary>
    ''' 干涉儀 Program ID
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure SLaserInterferometerProgramID
        ''' <summary>[忽略剖面量測，只做Z High量測之ProgramID]</summary>
        ''' <remarks></remarks>
        Public intPassMeasureSectionType As Integer
        ''' <summary>[生產用之ProgramID]</summary>
        ''' <remarks></remarks>
        Public intProduceType As Integer
    End Structure
#End Region

#Region "平台位置Stage"
    ''' <summary>座標關係
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmCoordinateRelationType
        eGN2 = 0 '預設 軸座標X>YV CCD座標X>YV
        eDTS = 2 '     軸座標X>Y^ CCD座標X>YV
    End Enum

    ''' <summary>
    ''' Stage移動時的速度是走哪種模式
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmStageMotionType
        ''' <summary>[回Home] </summary>
        ''' <remarks></remarks>
        Home = 0
        ''' <summary>[一般移動型態] </summary>
        ''' <remarks></remarks>
        Moving = 1
        ''' <summary>[點膠運動模式] </summary>
        ''' <remarks></remarks>
        Dispensing = 2
    End Enum

    ''' <summary>
    ''' 系統參數(Position)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CPosition
        ''' <summary>CCD對雷射偏移校正</summary>
        ''' <remarks></remarks>
        Public CCDLaserCalibration(enmStage.Max) As CCDLaserCalibration
        ''' <summary>CCD 對 TiltValve 校正</summary>
        ''' <remarks></remarks>
        Public CCDTiltVavleCalbration(enmStage.Max) As CCCDTiltValveCalibration
        ''' <summary>Laser 對 TiltValve 校正</summary>
        ''' <remarks></remarks>
        Public LaserTiltValveCalbration(enmStage.Max) As CLaserTiltValveCalibration
        ''' <summary>平台精準度驗證</summary>
        ''' <remarks></remarks>
        Public StageVerification(enmStage.Max) As CStageVerification
        ''' <summary>[Purge校正]</summary>
        ''' <remarks></remarks>
        Public PurgeCalibration(enmStage.Max) As CPurgeCalibration
        ''' <summary>[秤重校正]</summary>
        ''' <remarks></remarks>
        Public WeightCalibration(enmStage.Max) As CWeightCalibration
        ''' <summary>平台間校正絕對位置</summary>
        ''' <remarks></remarks>
        Public DispStageCalibrationPosX(enmStage.Max) As Decimal
        ''' <summary>平台間校正絕對位置</summary>
        ''' <remarks></remarks>
        Public DispStageCalibrationPosY(enmStage.Max) As Decimal
        ''' <summary>[安全區位置]</summary>
        ''' <remarks></remarks>
        Public SafeRegion(enmStage.Max) As CSafeRegion
        ''' <summary>[膠管更換位置]</summary>
        ''' <remarks></remarks>
        Public ChangePotCalibration(enmStage.Max) As CChangePotCalibration
        ''' <summary>[擦拭閥頭位置]</summary>
        ''' <remarks></remarks>
        Public CleanValveCalibration(enmStage.Max) As CCleanValveCalibration
        ''' <summary>[z軸上升位置]</summary>
        ''' <remarks></remarks>
        Public ZUpPos As Decimal
        ''' <summary>[飛行高度(Z軸)，不會撞機的高度]</summary>
        ''' <remarks></remarks>
        Public SafePosZ As Decimal
        ''' <summary>[Z軸安全位置(有Tilt軸時)]</summary>
        ''' <remarks></remarks>
        Public TiltSafePosZ As Decimal
        ''' <summary>
        ''' [暫停開門動作設定位置紀錄]
        ''' </summary>
        ''' <remarks></remarks>
        Public PauseStopPos(enmStage.Max) As AxisPos

        Sub Load(p1 As String)
            Throw New NotImplementedException
        End Sub
    End Class

    Public Class CParmStageParts
        ''' <summary>[閥頭相關資訊]</summary>
        ''' <remarks></remarks>
        Public ValveData(enmStage.Max) As CValveData
        ''' <summary>[膠材壽命管理]</summary>
        ''' <remarks></remarks>
        Public PasteLifeTime(enmStage.Max) As CPasteLifeTime
        ''' <summary>[紀錄目前Purge的計數]</summary>
        ''' <remarks></remarks>
        Public Purge(enmStage.Max) As CInpectionCondition
        ''' <summary>[紀錄目前秤重的計數]</summary>
        ''' <remarks></remarks>
        Public FlowRate(enmStage.Max) As CInpectionCondition
    End Class

#End Region

    ''' <summary>
    ''' 記錄狀態(AutoRunAction、Home)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmRunStatus
        ''' <summary>[尚未執行] </summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>[Runing] </summary>
        ''' <remarks></remarks>
        Running = 1
        ''' <summary>停止生產, 下一步必須復歸</summary>
        ''' <remarks></remarks>
        [Stop] = 2
        ' ''' <summary>[Pause] </summary>
        ' ''' <remarks></remarks>
        'Pause = 2
        ''' <summary>[Alarm]</summary>
        ''' <remarks></remarks>
        Alarm = 3
        ''' <summary>[完成] </summary>
        ''' <remarks></remarks>
        Finish = 4
        ''' <summary>[等待] </summary>
        ''' <remarks></remarks>
        Waiting = 5
        ' ''' <summary>暫停開門</summary>
        ' ''' <remarks></remarks>
        'PauseDoorOpen = 6
        ' ''' <summary>暫停開門再繼續</summary>
        ' ''' <remarks></remarks>
        'PauseResume = 7
    End Enum
    ''' <summary>動作基本參數</summary>
    ''' <remarks></remarks>
    Public Structure sActParam
        ''' <summary>動作執行狀態</summary>
        ''' <remarks></remarks>
        Public RunStatus As enmRunStatus

    End Structure

    ''' <summary>
    ''' 動作基本參數
    ''' </summary>
    ''' <remarks></remarks>
    Public Class sSysParam
        ''' <summary>[紀錄ESYS號碼]</summary>
        ''' <remarks></remarks>
        Public EsysNum As Integer
        ''' <summary>接收命令</summary>
        ''' <remarks></remarks>
        Public Command As eSysCommand
        ''' <summary>執行命令</summary>
        ''' <remarks></remarks>
        Public ExecuteCommand As eSysCommand
        ''' <summary>系統執行步驟</summary>
        ''' <remarks></remarks>
        Public SysNum As Integer
        ''' <summary>命令執行狀態</summary>
        ''' <remarks></remarks>
        Public RunStatus As enmRunStatus
        ''' <summary>起始步驟</summary>
        ''' <remarks></remarks>
        Public Const SysLoopStart As Integer = 1000
        ''' <summary>搭配平台索引</summary>
        ''' <remarks></remarks>
        Public StageNo As enmStage
        ''' <summary>設定閥件索引陣列</summary>
        ''' <remarks></remarks>
        Public ValveNo(enmValve.Max) As enmValve
        Private _ValveWorkMode As eValveWorkMode
        ''' <summary>
        ''' [Stage的閥體工作模式]
        ''' </summary>
        ''' <remarks></remarks>
        Public Property SelectValve As eValveWorkMode
            Get
                Return _ValveWorkMode
            End Get
            Set(value As eValveWorkMode)
                Select Case value
                    Case eValveWorkMode.Valve1
                        _ValveWorkMode = eValveWorkMode.Valve1
                    Case eValveWorkMode.Valve2
                        _ValveWorkMode = eValveWorkMode.Valve2
                End Select
            End Set
        End Property
        ''' <summary>搭配天平索引</summary>
        ''' <remarks></remarks>
        Public BalanceNo As enmBalance
        ''' <summary>搭配CCD索引</summary>
        ''' <remarks></remarks>
        Public CCDNo As Integer
        ''' <summary>搭配測高索引</summary>
        ''' <remarks></remarks>
        Public PinNo As Integer
        ''' <summary>搭配FMCS索引</summary>
        ''' <remarks></remarks>
        Public FMCSNo As Integer
        ''' <summary>搭配清膠機構索引</summary>
        ''' <remarks></remarks>
        Public ClearNo As Integer
        ''' <summary>搭配測高機構索引</summary>
        ''' <remarks></remarks>
        Public LaserNo As Integer
        ''' <summary>[電空閥索引]</summary>
        ''' <remarks></remarks>
        Public EPVNo As Integer
        ''' <summary>[搭配ValveControllerNo1機構索引]</summary>
        ''' <remarks></remarks>
        Public ValveControllerNo1 As Integer
        ''' <summary>搭配ValveControllerNo2機構索引</summary>
        ''' <remarks></remarks>
        Public ValveControllerNo2 As Integer
        ''' <summary>搭配Conveyor索引</summary>
        ''' <remarks></remarks>
        Public ConveyorNo As eConveyor
        ''' <summary>搭配的Machine索引</summary>
        ''' <remarks></remarks>
        Public MachineNo As enmMachineStation
        ''' <summary>等效X軸</summary>
        ''' <remarks></remarks>
        Public AxisX As Integer
        ''' <summary>等效Y軸</summary>
        ''' <remarks></remarks>
        Public AxisY As Integer
        ''' <summary>等效Z軸</summary>
        ''' <remarks></remarks>
        Public AxisZ As Integer
        ''' <summary>等效A軸</summary>
        ''' <remarks></remarks>
        Public AxisA As Integer
        ''' <summary>等效B軸</summary>
        ''' <remarks></remarks>
        Public AxisB As Integer
        ''' <summary>等效C軸</summary>
        ''' <remarks></remarks>
        Public AxisC As Integer
        ''' <summary>[等效Converter軸]</summary>
        ''' <remarks></remarks>
        Public AxisConverter As Integer
        ''' <summary>未定義暫存用標記</summary>
        ''' <remarks></remarks>
        Public Tag As Object
        ''' <summary>產品進入的ID</summary>
        ''' <remarks></remarks>
        Public WaferID As String
        ''' <summary>動作運行狀態 用於流程判定與保護 因系統動作無法自保持,因此另開記錄區間</summary>
        ''' <remarks></remarks>
        Public Act(eAct.Max) As sActParam
        Public Timer As New Stopwatch
        ''' <summary>外部要求暫停</summary>
        ''' <remarks></remarks>
        Public ExternalPause As Boolean
        ''' <summary>由流程決定外部是否可暫停</summary>
        ''' <remarks></remarks>
        Public IsCanPause As Boolean = True
    End Class

    ''' <summary>系統動作模組參數 用於動作命令下達</summary>
    ''' <remarks></remarks>
    Public gSYS(eSys.Max) As sSysParam

#Region "權限Authority"
    ''' <summary>權限項目列舉</summary>
    ''' <remarks></remarks>
    Public Enum enmUserAuthItem
        None = 0
        ''' <summary>
        ''' 手動其他操作
        ''' </summary>
        ''' <remarks></remarks>
        Manual = 1
        ''' <summary>
        ''' 產品編輯
        ''' </summary>
        ''' <remarks></remarks>
        Recipe = 2
        ''' <summary>
        ''' IO設定
        ''' </summary>
        ''' <remarks></remarks>
        IOSetup = 3
        ''' <summary>
        ''' 傳送帶模組
        ''' </summary>
        ''' <remarks></remarks>
        SetModuleConveyor = 4
        ''' <summary>
        ''' 使用者權限
        ''' </summary>
        ''' <remarks></remarks>
        SetUserLevel = 5
        ''' <summary>
        ''' 互鎖安全保護
        ''' </summary>
        ''' <remarks></remarks>
        SetInterlock = 6
        ''' <summary>
        ''' 系統配置設定
        ''' </summary>
        ''' <remarks></remarks>
        setHardwareConfig = 7
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        SystemMotor = 8
        ''' <summary>
        ''' IO表
        ''' </summary>
        ''' <remarks></remarks>
        IOTable = 9
        ''' <summary>
        ''' CCD模組
        ''' </summary>
        ''' <remarks></remarks>
        SetModuleAOI = 10
        ''' <summary>
        ''' 校正
        ''' </summary>
        ''' <remarks></remarks>
        Calibration = 11
        ''' <summary>
        ''' 工程模式
        ''' </summary>
        ''' <remarks></remarks>
        EngineMode = 12
        ''' <summary>
        ''' 手動馬達操作
        ''' </summary>
        ''' <remarks></remarks>
        ManualMotor = 13
        ''' <summary>
        ''' 設定除膠位置
        ''' </summary>
        ''' <remarks></remarks>
        SetPurgePos = 14
        ''' <summary>
        ''' 設定清膠位置
        ''' </summary>
        ''' <remarks></remarks>
        SetClearPos = 15
        ''' <summary>
        ''' 設定換膠位置
        ''' </summary>
        ''' <remarks></remarks>
        SetChangePos = 16
        ''' <summary>
        ''' 設定秤重位置
        ''' </summary>
        ''' <remarks></remarks>
        SetWeightPos = 17
        ''' <summary>
        ''' 設定閥控制器
        ''' </summary>
        ''' <remarks></remarks>
        SetValveController = 18
        ''' <summary>
        ''' 設定三色燈
        ''' </summary>
        ''' <remarks></remarks>
        SetModuleTowerLight = 19
        ''' <summary>
        ''' 設定訊息
        ''' </summary>
        ''' <remarks></remarks>
        SetMessageLanguage = 20
        ''' <summary>
        ''' 設定加熱器
        ''' </summary>
        ''' <remarks></remarks>
        SetPartHotPlate = 21
        ''' <summary>
        ''' 設定CCD
        ''' </summary>
        ''' <remarks></remarks>
        SetCCD = 22
        ''' <summary>
        ''' 設定光源
        ''' </summary>
        ''' <remarks></remarks>
        SetLight = 23
        ''' <summary>
        ''' 設定觸發控制器
        ''' </summary>
        ''' <remarks></remarks>
        SetTriggerController = 24
        ''' <summary>
        ''' 設定天平
        ''' </summary>
        ''' <remarks></remarks>
        SetBalance = 25
        ''' <summary>
        ''' 設定干涉儀
        ''' </summary>
        ''' <remarks></remarks>
        SetLaserReader = 26
        ''' <summary>
        ''' 設定流量計
        ''' </summary>
        ''' <remarks></remarks>
        SetFMCS = 27
        ''' <summary>
        ''' 設定傳送帶
        ''' </summary>
        ''' <remarks></remarks>
        SetConveyor = 28
        ''' <summary>
        ''' 電空閥
        ''' </summary>
        ''' <remarks></remarks>
        SetElectroPneumaticValve = 29
        ''' <summary>
        ''' 設定傾斜軸
        ''' </summary>
        ''' <remarks></remarks>
        SetTilt = 30
        ''' <summary>設定電動缸</summary>
        ''' <remarks></remarks>
        SetElectricCylinder = 31
        ''' <summary>
        ''' 設定溫度
        ''' </summary>
        ''' <remarks></remarks>
        SetTemperature = 32
        ''' <summary>平台安全保護設定</summary>
        ''' <remarks></remarks>
        SetStageSafe = 33
        Max
    End Enum
    ''' <summary>
    ''' 使用者權限設定
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure sAuthority



#Region "主介面權限"
        ''' <summary>
        ''' 配方設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Recipe As enmUserLevel
        ''' <summary>
        ''' 校正
        ''' </summary>
        ''' <remarks></remarks>
        Public Calibration As enmUserLevel
        ''' <summary>
        ''' 工程模式
        ''' </summary>
        ''' <remarks></remarks>
        Public EngineMode As enmUserLevel
#End Region

#Region "工程模式權限"

        ''' <summary>
        ''' 手動操作設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Manual As enmUserLevel
        ''' <summary>手動馬達操作設定</summary>
        ''' <remarks></remarks>
        Public ManualMotor As enmUserLevel
        ''' <summary>
        ''' 手動IO操作設定
        ''' </summary>
        ''' <remarks></remarks>
        Public IOSetup As enmUserLevel
        ''' <summary>
        ''' IO表檢查
        ''' </summary>
        ''' <remarks></remarks>
        Public IOTable As enmUserLevel
        ''' <summary>
        ''' 除膠位置設定
        ''' </summary>
        ''' <remarks></remarks>
        Public PurgePos As enmUserLevel
        ''' <summary>
        ''' 清膠位置設定
        ''' </summary>
        ''' <remarks></remarks>
        Public ClearPos As enmUserLevel
        ''' <summary>
        ''' 換膠位置設定
        ''' </summary>
        ''' <remarks></remarks>
        Public ChangePos As enmUserLevel
        ''' <summary>
        ''' 秤重位置設定
        ''' </summary>
        ''' <remarks></remarks>
        Public WeightPos As enmUserLevel
        ''' <summary>
        ''' 設定使用者權限
        ''' </summary>
        ''' <remarks></remarks>
        Public SetUserLevel As enmUserLevel
        ''' <summary>
        ''' 設定訊息語系
        ''' </summary>
        ''' <remarks></remarks>
        Public SetMessageLanguage As enmUserLevel
        ''' <summary>
        ''' 設定硬體配置
        ''' </summary>
        ''' <remarks></remarks>
        Public setHardwareConfig As enmUserLevel

        ''' <summary>
        ''' 設定傳送帶模組
        ''' </summary>
        ''' <remarks></remarks>
        Public SetModuleConveyor As enmUserLevel
        ''' <summary>
        ''' 設定AOI模組
        ''' </summary>
        ''' <remarks></remarks>
        Public SetModuleAOI As enmUserLevel
        ''' <summary>
        ''' 設定四色燈模組
        ''' </summary>
        ''' <remarks></remarks>
        Public SetModuleTowerLight As enmUserLevel
        ''' <summary>
        ''' 設定加熱器部件
        ''' </summary>
        ''' <remarks></remarks>
        Public SetPartHotPlate As enmUserLevel

        ''' <summary>
        ''' 設定閥控制器部件
        ''' </summary>
        ''' <remarks></remarks>
        Public SetValveController As enmUserLevel
        ''' <summary>
        ''' 設定CCD部件
        ''' </summary>
        ''' <remarks></remarks>
        Public SetCCD As enmUserLevel
        ''' <summary>
        ''' 設定光源部件
        ''' </summary>
        ''' <remarks></remarks>
        Public SetLight As enmUserLevel
        ''' <summary>
        ''' 設定觸發板部件
        ''' </summary>
        ''' <remarks></remarks>
        Public SetTriggerController As enmUserLevel
        ''' <summary>
        ''' 設定天平部件
        ''' </summary>
        ''' <remarks></remarks>
        Public SetBalance As enmUserLevel
        ''' <summary>
        ''' 設定干涉儀部件
        ''' </summary>
        ''' <remarks></remarks>
        Public SetLaserReader As enmUserLevel
        ''' <summary>
        ''' 設定流量計部件
        ''' </summary>
        ''' <remarks></remarks>
        Public SetFMCS As enmUserLevel
        ''' <summary>
        ''' 設定互鎖保護
        ''' </summary>
        ''' <remarks></remarks>
        Public SetInterlock As enmUserLevel
        ''' <summary>
        ''' 設定傳送帶部件
        ''' </summary>
        ''' <remarks></remarks>
        Public SetConveyor As enmUserLevel

#End Region

    End Structure

#End Region

#Region "通訊設定Communication"
    ''' <summary>
    ''' Address
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure SAddress
        Public intPort As Integer
        Public strIP As String
    End Structure
    ' ''' <summary>
    ' ''' 與CASTEC通訊之狀態
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum enmCASTECCommunication
    '    ''' <summary>[初始化(無作用)]</summary>
    '    ''' <remarks></remarks>
    '    eInitial = 0
    '    ''' <summary>[回應Ack成功] </summary>
    '    ''' <remarks></remarks>
    '    eOK = 1
    '    ''' <summary>[無回應或通訊失敗] </summary>
    '    ''' <remarks></remarks>
    '    eNG = 2
    'End Enum
    ''' <summary>
    ''' 通訊Port Baud Rate
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmBaudRate
        e9600 = 9600
        e14400 = 14400
        e19200 = 19200
        e38400 = 38400
        e57600 = 57600
        e115200 = 115200
    End Enum
#End Region

#Region "傳送帶模組Conveyor"
    Public Structure sConveyorConnectionParameter
        Public PLCType As String       'PLC型號
        Public ConveyorType As String  '流道模組型號
        Public PLCPortName As String   'PLC連接埠
    End Structure
#End Region

#Region "燈源控制模組LightContorl"
    Public Structure sLightConnectionParameter
        ''' <summary>[光源]</summary>
        ''' <remarks></remarks>
        Public CardType As enmProgramLightType
        ''' <summary>COM通訊埠</summary>
        ''' <remarks></remarks>
        Public PortName As String   '連接埠
        ''' <summary>Baud Rate</summary>
        ''' <remarks></remarks>
        Public BaudRate As enmBaudRate
        Public DataBit As String
        ''' <summary>通道可輸出的上限值</summary>
        ''' <remarks></remarks>
        Public ChannelMaxValue1 As Integer
        Public ChannelMaxValue2 As Integer
        Public ChannelMaxValue3 As Integer
        Public ChannelMaxValue4 As Integer
        ''' <summary>
        ''' 電流正規化 0→  255
        ''' </summary>
        ''' <remarks></remarks>
        Public Unit As Integer
        ''' <summary>
        ''' 每個通道的scale值
        ''' </summary>
        ''' <remarks></remarks>
        Public ChannelScale1 As Decimal
        Public ChannelScale2 As Decimal
        Public ChannelScale3 As Decimal
        Public ChannelScale4 As Decimal

    End Structure
#End Region

#Region "流量監控模組FMCS"
    ''' <summary>FMCS參數</summary>
    ''' <remarks></remarks>
    Public Structure sFMCSConnectionParameter
        Public Function CardTypeToString()
            Select Case CardType
                Case enmFMCSType.None
                    Return "None"
                Case enmFMCSType.FMCS
                    Return "FMCS"
                Case enmFMCSType.FMCS_P
                    Return "FMCS-P"
                Case enmFMCSType.FMCS_PI
                    Return "FMCS-PI"
                Case enmFMCSType.FMCS_PII
                    Return "FMCS-PII"
            End Select
            Return "Unknown"
        End Function
        ''' <summary>型號</summary>
        ''' <remarks></remarks>
        Public CardType As enmFMCSType
        ''' <summary>機型</summary>
        ''' <remarks></remarks>
        Public strFMCSType As String
        ''' <summary>COM埠</summary>
        ''' <remarks></remarks>
        Public PortName As String
        ''' <summary>鮑率</summary>
        ''' <remarks></remarks>
        Public BaudRate As Integer
    End Structure
#End Region

#Region "微量天秤模組Weight"
    ''' <summary>天平模式</summary>
    ''' <remarks></remarks>
    Public Enum enmWeighingMode
        ''' <summary>無</summary>
        ''' <remarks></remarks>
        None = -1
        ''' <summary>自動記錄</summary>
        ''' <remarks></remarks>
        AutoRecord
    End Enum

    ''' <summary>Production Weighing Unit (Ex:AD-4212A/B) Soni + 2014.10.22 
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure sBalanceConnectionParameter
        Public Shared Function CardTypeToString(ByVal cardType As enmBalanceType) As String
            Select Case cardType
                Case enmBalanceType.None
                    Return "None"
                Case enmBalanceType.AD4212A100
                    Return "AD4212A-100"
                Case enmBalanceType.WZA215_LC
                    Return "WZA215_LC"
            End Select
            Return "Unknown"
        End Function
        ''' <summary>元件類型</summary>
        ''' <remarks></remarks>
        Public CardType As enmBalanceType
        ''' <summary>COM Port Name</summary>
        ''' <remarks></remarks>
        Public PortName As String
        ''' <summary>COM Baud Rate</summary>
        ''' <remarks></remarks>
        Public BaudRate As String
        ' ''' <summary>機台量測值1(範圍內較小值)</summary>
        ' ''' <remarks></remarks>
        'Public dblX1 As Decimal
        ' ''' <summary>機台量測值2(範圍內較大值)</summary>
        ' ''' <remarks></remarks>
        'Public dblX2 As Decimal
        ' ''' <summary>標準天平量測值1</summary>
        ' ''' <remarks></remarks>
        'Public dblY1 As Decimal
        ' ''' <summary>標準天平量測值2</summary>
        ' ''' <remarks></remarks>
        'Public dblY2 As Decimal
        ' ''' <summary>微量天平穩定時間(Sec)</summary>
        ' ''' <remarks></remarks>
        'Public dblStableTimeInSec As Decimal
        '''' <summary>多少顆量測一次</summary>
        '''' <remarks></remarks>
        'Public intFrequencyInDevice As Integer
        ' ''' <summary>自動記錄</summary>
        ' ''' <remarks></remarks>
        'Public enmWeighingMode As enmWeighingMode
        ''' <summary>
        ''' [天秤的TimeOUT設定]
        ''' </summary>
        ''' <remarks></remarks>
        Public TimeoutTimer As Double
        
        ' ''' <summary>移到天秤後多少ms穩定後,才開始打點</summary>
        ' ''' <remarks>單位:msec</remarks>
        'Public MoveToBalanceStableTime As Decimal
    End Structure
#End Region

    '#Region "程控光源Light"
    '    Public Structure sProgramLight
    '        ''' <summary>控制型號</summary>
    '        ''' <remarks></remarks>
    '        Public ControlType As String
    '        ''' <summary>連接埠</summary>
    '        ''' <remarks></remarks>
    '        Public PortName As String
    '    End Structure
    '#End Region

    ''' <summary>[Language Type]</summary>
    ''' <remarks></remarks>
    Public Enum enmLanguageType
        ''' <summary>[英文]</summary>
        ''' <remarks></remarks>
        eEnglish = 0
        ''' <summary>[繁中]</summary>
        ''' <remarks></remarks>
        eTraditionalChinese = 1
        ''' <summary>[簡中]</summary>
        ''' <remarks></remarks>
        eSimplifiedChinese = 2
    End Enum

    ''' <summary>
    ''' 點膠前影像辨識狀態
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmDieState
        ''' <summary>[未處理] </summary>
        ''' <remarks></remarks>
        None = -1
        ''' <summary>[無晶粒]</summary>
        ''' <remarks></remarks>
        NoDie = 0
        ''' <summary>[有晶粒]</summary>
        ''' <remarks></remarks>
        OK = 1
        ''' <summary>[有晶粒 但歪斜過大]</summary>
        ''' <remarks></remarks>
        NG = 2
    End Enum

    ''' <summary>
    ''' 點膠處理狀態
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmDispenserState
        ''' <summary>[未處理]</summary>
        ''' <remarks></remarks>
        None = -1
        ''' <summary>[完成] </summary>
        ''' <remarks></remarks>
        Done = 0
    End Enum

    ''' <summary>
    ''' 點膠後的檢測狀態
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmDieDetectState
        ''' <summary>[沒有檢測]</summary>
        ''' <remarks></remarks>
        None = -1
        ''' <summary>[檢測 正常]</summary>
        ''' <remarks></remarks>
        OK = 0
        ''' <summary>[檢測 有異常] </summary>
        ''' <remarks></remarks>
        NG = 1
    End Enum

    ''' <summary>
    ''' 晶粒處理完之狀態
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmResultState
        ''' <summary>[尚未處理]</summary>
        ''' <remarks></remarks>
        None = 3
        ''' <summary>[無晶粒] </summary>
        ''' <remarks></remarks>
        NoDie = 0
        ''' <summary>[處理完ＯＫ]</summary>
        ''' <remarks></remarks>
        OK = 1
        ''' <summary>[處理完NG]</summary>
        ''' <remarks></remarks>
        NG = 2
        ''' <summary>[尚未處理完成]</summary>
        ''' <remarks></remarks>
        Unfinished = 4

    End Enum

    ''' <summary>
    ''' 生產線狀態
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmProductLineStatus
        None = 0
        Loading = 1
        Purge = 2
        Scan = 3
        LaserReader = 4
        ClearGlue = 5
        Dispensing = 6
        CheckGlue = 7
        Unloading = 8
        PreDispensing = 9
    End Enum

    ' ''' <summary>
    ' ''' Tray Block Index
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum enmTrayBlock
    '    None = -1
    '    BlockNo1 = 0
    '    BlocKNo2 = 1
    '    BlockNo3 = 2
    'End Enum

    ''' <summary>
    ''' 汽缸上升下降
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enmUpDown
        ''' <summary>[Up]</summary>
        ''' <remarks></remarks>
        Up = 0
        ''' <summary>[Down]</summary>
        ''' <remarks></remarks>
        Down = 1
    End Enum
    ''' <summary>傳送帶類型</summary>
    ''' <remarks></remarks>
    Public Enum enmConveyorType
        ''' <summary>PLC版本0</summary>
        ''' <remarks></remarks>
        PLC0 = 0
        ''' <summary>廠內版本0</summary>
        ''' <remarks></remarks>
        Conveyor0 = 1
    End Enum

    ' ''' <summary>[入料的情況與此入料預期做的工作內容]</summary>
    ' ''' <remarks></remarks>
    'Public Enum enmLoadingType
    '    ''' <summary>[正常入料 Run]</summary>
    '    ''' <remarks></remarks>
    '    None = 0
    '    ''' <summary>[空Tray]</summary>
    '    ''' <remarks></remarks>
    '    NoComponentTray = 1
    '    ''' <summary>[Pass Dispensing] </summary>
    '    ''' <remarks></remarks>
    '    PassDispenser = 2
    'End Enum

    Public Enum eCCDImageProcess
        ''' <summary>不儲存</summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>儲存不良影像</summary>
        ''' <remarks></remarks>
        SaveNG = 1
        ''' <summary>儲存全部影像</summary>
        ''' <remarks></remarks>
        SaveAll = 2
    End Enum
   
    ' ''' <summary>
    ' ''' Tray盤類別(跑哪一種貨) 此會攸關與CASTEC交握方式
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum enmTrayType
    '    X176 = 0
    '    X162 = 1
    '    X257 = 2
    '    X360 = 3
    '    X404 = 4
    'End Enum

    ''' <summary>[Stable Time]</summary>
    ''' <remarks></remarks>
    Public Class CStableTime
        ''' <summary>[CCD在Busy完成後等待IO切換之時間]</summary>
        ''' <remarks></remarks>
        Public CcdIOStableTime As Decimal
        ''' <summary>[CCD通訊TimeOut時間]</summary>
        ''' <remarks></remarks>
        Public CcdTimeOutTime As Decimal
        ''' <summary>[檢查Sensor逾時之上限](ms)</summary>
        ''' <remarks></remarks>
        Public CheckSensorTimeout As Decimal
        ''' <summary>[CCD取像之機構整定時間](ms)</summary>
        ''' <remarks></remarks>
        Public CCDStableTime As Decimal
        ''' <summary>[CCD切換觸發模式之整定時間](ms)</summary>
        ''' <remarks></remarks>
        Public CCDChangeModeStableTime As Decimal
        ''' <summary>[Laser取值之機構整定時間](ms)</summary>
        ''' <remarks></remarks>
        Public LaserStableTime As Decimal
        ''' <summary>[料片預熱時間(入料後，需等待料片加熱一段時間後才能進行點膠)(ms)]</summary>
        ''' <remarks></remarks>
        Public PriorHeatTime As Decimal
        ''' <summary>[觸發版Trigger I/O訊號反應時間]</summary>
        ''' <remarks></remarks>
        Public TriggerBoardIOStableTime As Decimal

        ''' <summary>讀取穩定時間</summary>
        ''' <param name="fileName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Load(ByVal fileName As String) As Boolean
            Dim strSection As String
            strSection = "StableTime"
            With Me
                .CcdIOStableTime = CDecimal(ReadIniString(strSection, "CcdIOStableTime", fileName, 10))
                .CcdTimeOutTime = CDecimal(ReadIniString(strSection, "CcdTimeOutTime", fileName, 5000))
                .CheckSensorTimeout = CDecimal(ReadIniString(strSection, "CheckSensorTimeout", fileName, 5000))
                .CCDStableTime = CDecimal(ReadIniString(strSection, "CCDStableTime", fileName, 50))
                .CCDChangeModeStableTime = CDecimal(ReadIniString(strSection, "CCDChangeModeStableTime", fileName, 20))
                .LaserStableTime = CDecimal(ReadIniString(strSection, "LaserStableTime", fileName, 10))
                .PriorHeatTime = CDec(ReadIniString(strSection, "PriorHeatTime", fileName, 10))
                .TriggerBoardIOStableTime = CDec(ReadIniString(strSection, "TriggerBoardIOStableTime", fileName, 20))
            End With
            Return True

        End Function

        ''' <summary>儲存穩定時間</summary>
        ''' <param name="fileName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Save(ByVal fileName As String) As Boolean
            Dim strSection As String
            strSection = "StableTime"
            With Me
                Call SaveIniString(strSection, "CcdIOStableTime", .CcdIOStableTime, fileName)
                Call SaveIniString(strSection, "CcdTimeOutTime", .CcdTimeOutTime, fileName)
                Call SaveIniString(strSection, "CheckSensorTimeout", .CheckSensorTimeout, fileName)
                Call SaveIniString(strSection, "CCDStableTime", .CCDStableTime, fileName)
                Call SaveIniString(strSection, "CCDChangeModeStableTime", .CCDStableTime, fileName)
                Call SaveIniString(strSection, "LaserStableTime", .LaserStableTime, fileName)
                Call SaveIniString(strSection, "PriorHeatTime", .PriorHeatTime, fileName)
                Call SaveIniString(strSection, "TriggerBoardIOStableTime", .TriggerBoardIOStableTime, fileName)
            End With
            Return True
        End Function

    End Class

    ''' <summary>
    ''' 紀錄點膠的資訊(OK NG)
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure SDispenserState
        ''' <summary>
        ''' 定位判定良品
        ''' </summary>
        ''' <remarks></remarks>
        Public CCDScanOKPcs As Long
        ''' <summary>
        ''' 定位判定不良
        ''' </summary>
        ''' <remarks></remarks>
        Public CCDScanNGPcs As Long
        ''' <summary>
        ''' 總定位判定數
        ''' </summary>
        ''' <remarks></remarks>
        Public TotalCCDScanPcs As Long
        ''' <summary>
        ''' 測高判定良品
        ''' </summary>
        ''' <remarks></remarks>
        Public LaserReaderOKPcs As Long
        ''' <summary>
        ''' 測高判定不良
        ''' </summary>
        ''' <remarks></remarks>
        Public LaserReaderNGPcs As Long
        ''' <summary>
        ''' 總測高判定數
        ''' </summary>
        ''' <remarks></remarks>
        Public TotoalLaserReaderPcs As Long
        ''' <summary>
        ''' 點膠不良
        ''' </summary>
        ''' <remarks></remarks>
        Public DispensingNGPcs As Long
        ''' <summary>
        ''' 點膠良品
        ''' </summary>
        ''' <remarks></remarks>
        Public DispensingOKPcs As Long
        ''' <summary>
        ''' 總點膠數
        ''' </summary>
        ''' <remarks></remarks>
        Public TotalDispensingPcs As Long
    End Structure


    ''' <summary>[機台安全位置結構，單軌雙閥形式]</summary>
    ''' <remarks></remarks>
    Public Structure SSaftPosData
        ''' <summary>
        ''' [安全距離X，意旨左邊與右邊須相對多少為安全距離]
        ''' </summary>
        ''' <remarks></remarks>
        Public SafeDistanceX As Decimal
        ''' <summary>
        ''' [安全距離Y，意旨左邊與右邊須相對多少為安全距離]
        ''' </summary>
        ''' <remarks></remarks>
        Public SafeDistanceY As Decimal
        ''' <summary>[單機行程大小 X]</summary>
        ''' <remarks></remarks>
        Public SpreadX As Decimal
        ''' <summary>[單機行程大小 Y]</summary>
        ''' <remarks></remarks>
        Public SpreadY As Decimal

        ''' <summary>儲存檔案</summary>
        ''' <param name="fileName">檔案路徑</param>
        ''' <param name="sectionName">區段名稱</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Save(ByVal fileName As String, ByVal sectionName As String) As Boolean
            Call SaveIniString(sectionName, "SafeDistanceX", Me.SafeDistanceX, fileName)
            Call SaveIniString(sectionName, "SafeDistanceY", Me.SafeDistanceY, fileName)
            Call SaveIniString(sectionName, "SpreadX", Me.SpreadX, fileName)
            Call SaveIniString(sectionName, "SpreadY", Me.SpreadY, fileName)
            Return True
        End Function
        ''' <summary>讀取檔案 </summary>
        ''' <param name="fileName">檔案路徑</param>
        ''' <param name="sectionName">區段名稱</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Load(ByVal fileName As String, ByVal sectionName As String) As Boolean
            Me.SafeDistanceX = CDecimal(ReadIniString(sectionName, "SafeDistanceX", fileName, 100))
            Me.SafeDistanceY = CDecimal(ReadIniString(sectionName, "SafeDistanceY", fileName, 100))
            Me.SpreadX = CDecimal(ReadIniString(sectionName, "SpreadX", fileName, 450))
            Me.SpreadY = CDecimal(ReadIniString(sectionName, "SpreadY", fileName, 450))
            Return True
        End Function

    End Structure

    Public gSSystemParameter As New CSystemParameter
End Module

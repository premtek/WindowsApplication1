Imports ProjectCore

''' <summary>[重量控制]</summary>
''' <remarks></remarks>
Public Structure SWeightControl
    ''' <summary>[重量控制類型]</summary>
    ''' <remarks></remarks>
    Public Type As eWeightControlType
    ''' <summary>[重量(mg)]</summary>
    ''' <remarks></remarks>
    Public Weight As Decimal
    ''' <summary>[顆數(pcs)] 限定最低兩顆</summary>
    ''' <remarks></remarks>
    Public DotCounts As Decimal
    
    ''' <summary>[速度(mm/s)]</summary>
    ''' <remarks></remarks>
    Public Velocity As Decimal
  
    ''' <summary>[Dot間的間距](um)</summary>
    ''' <remarks></remarks>
    Public dotPitch As Decimal
   

End Structure


#Region " StepType"
''' <summary>[選擇閥組]</summary>
''' <remarks></remarks>
Public Structure SSelectValveParameter
    ''' <summary>[噴膠頭編號] </summary>
    ''' <remarks></remarks>
    Public ValveNo As eValveWorkMode
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public PosB As Decimal
End Structure

''' <summary>[路徑串接]</summary>
''' <remarks></remarks>
Public Structure SSeriesParameter
    ''' <summary>[路徑串接的關係]</summary>
    ''' <remarks></remarks>
    Public PathMode As ePathMode
End Structure

''' <summary>[等待]</summary>
''' <remarks></remarks>
Public Structure SWaitParameter
    ''' <summary>[Dwell等待時間(ms)]</summary>
    ''' <remarks></remarks>
    Public DwellTimeInMs As Decimal
End Structure

''' <summary>畫圓(2D)</summary>
''' <remarks></remarks>
Public Structure SCircle2DParameter
    ''' <summary>[圓弧起始點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public StartPosX As Decimal
    ''' <summary>[圓弧起始點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public StartPosY As Decimal
    ''' <summary>[圓弧終點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosX As Decimal
    ''' <summary>[圓弧終點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosY As Decimal
    ''' <summary>[圓弧中繼點2座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public Middle2PosX As Decimal
    ''' <summary>[圓弧中繼點2座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public Middle2PosY As Decimal
    ''' <summary>[圓弧圓心座標X(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public CenterPosX As Decimal
    ''' <summary>[圓弧圓心座標Y(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public CenterPosY As Decimal
    ''' <summary>[圓弧中繼點座標X(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosX As Decimal
    ''' <summary>[圓弧中繼點座標Y(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosY As Decimal
    ''' <summary>[轉速(Auger Valve)]</summary>
    ''' <remarks></remarks>
    Public RPM As Integer
    ''' <summary>[Direction][方向 0:CW  1:CCW]</summary>
    ''' <remarks></remarks>
    Public Direction As eArcDirection
    ''' <summary>[間距(um)]</summary>
    ''' <remarks></remarks>
    Public Pitch As Decimal
    ''' <summary>[重量控制]</summary>
    ''' <remarks></remarks>
    Public WeightControl As SWeightControl
    ''' <summary>[圓弧參數設定檔]</summary> 'Eason 20170216 Ticket:100080 , Add Arc Type Parameter
    ''' <remarks></remarks> 
    Public ArcParameterName As String
    ''' <summary>註解說明</summary>
    ''' <remarks></remarks>
    Public Comment As String
    '20171016
    ''' <summary>[線段的初速(mm/s)]</summary>
    ''' <remarks></remarks>
    Public StartVel As Decimal
End Structure

''' <summary>[畫弧(2D)]</summary>
''' <remarks></remarks>
Public Structure SArc2DParameter
    ''' <summary>[圓弧起始點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public StartPosX As Decimal
    ''' <summary>[圓弧起始點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public StartPosY As Decimal
    ''' <summary>[圓弧終點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosX As Decimal
    ''' <summary>[圓弧終點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosY As Decimal
    ''' <summary>[圓弧圓心座標X(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public CenterPosX As Decimal
    ''' <summary>[圓弧圓心座標Y(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public CenterPosY As Decimal
    ''' <summary>[圓弧中繼點座標X(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosX As Decimal
    ''' <summary>[圓弧中繼點座標Y(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosY As Decimal
    ''' <summary>[圓弧中繼點座標Z(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosZ As Decimal
    ''' <summary>[圓弧半徑] </summary>
    ''' <remarks></remarks>
    Public Radius As Decimal
    ''' <summary>[圓弧運動角度(以正負號決定旋轉方向)]</summary>
    ''' <remarks></remarks>
    Public Angle As Decimal
    ''' <summary>[轉速(Auger Valve)]</summary>
    ''' <remarks></remarks>
    Public RPM As Integer
    ''' <summary>[Direction][方向 0:CW  1:CCW]</summary>
    ''' <remarks></remarks>
    Public Direction As eArcDirection
    ''' <summary>[間距(um)]</summary>
    ''' <remarks></remarks>
    Public Pitch As Decimal
    ''' <summary>[重量控制]</summary>
    ''' <remarks></remarks>
    Public WeightControl As SWeightControl
    ''' <summary>[弧參數設定檔]</summary> 'Eason 20170216 Ticket:100080 , Add Arc Type Parameter
    ''' <remarks></remarks> 
    Public ArcParameterName As String
    ''' <summary>註解說明</summary>
    ''' <remarks></remarks>
    Public Comment As String
    '20171016
    ''' <summary>[線段的初速(mm/s)]</summary>
    ''' <remarks></remarks>
    Public StartVel As Decimal

End Structure

''' <summary>[畫點(3D)]</summary>
''' <remarks></remarks>
Public Structure SDots3DParameter
    ''' <summary>[點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public PosX As Decimal
    ''' <summary>[點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public PosY As Decimal
    ''' <summary>[點座標Z(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public PosZ As Decimal
    ''' <summary>[轉速(Auger Valve)]</summary>
    ''' <remarks></remarks>
    Public RPM As Integer
    ''' <summary>[重量控制]</summary>
    ''' <remarks></remarks>
    Public WeightControl As SWeightControl
    ''' <summary>[點參數設定檔]</summary>
    ''' <remarks></remarks> 
    Public DotParameterName As String
    '20171016
    ''' <summary>[線段的初速(mm/s)]</summary>
    ''' <remarks></remarks>
    Public StartVel As Decimal
End Structure

''' <summary>[畫線(3D)]</summary>
''' <remarks></remarks>
Public Structure SLine3DParameter
    ''' <summary>[線段起始點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public StartPosX As Decimal
    ''' <summary>[線段起始點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public StartPosY As Decimal
    ''' <summary>[線段起始點座標Z(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public StartPosZ As Decimal
    ''' <summary>[線段終點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosX As Decimal
    ''' <summary>[線段終點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosY As Decimal
    ''' <summary>[線段終點座標Z(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosZ As Decimal
    ''' <summary>[轉速(Auger Valve)]</summary>
    ''' <remarks></remarks>
    Public RPM As Integer
    ''' <summary>[間距(um)]</summary>
    ''' <remarks></remarks>
    Public Pitch As Decimal
    ''' <summary>[重量控制]</summary>
    ''' <remarks></remarks>
    Public WeightControl As SWeightControl
    ''' <summary>[線參數設定檔]</summary>
    ''' <remarks></remarks> 
    Public LineParameterName As String
    ''' <summary>註解說明</summary>
    ''' <remarks></remarks>
    Public Comment As String
    '20171016
    ''' <summary>[線段的初速(mm/s)]</summary>
    ''' <remarks></remarks>
    Public StartVel As Decimal
End Structure

''' <summary>[畫圓(3D)]</summary>
''' <remarks></remarks>
Public Structure SCircle3DParameter
    ''' <summary>[圓弧起始點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public StartPosX As Decimal
    ''' <summary>[圓弧起始點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public StartPosY As Decimal
    ''' <summary>[圓弧起始點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public StartPosZ As Decimal
    ''' <summary>[圓弧終點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosX As Decimal
    ''' <summary>[圓弧終點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosY As Decimal
    ''' <summary>[圓弧終點座標Z(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosZ As Decimal
    ''' <summary>[圓弧圓心座標X(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public CenterPosX As Decimal
    ''' <summary>[圓弧圓心座標Y(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public CenterPosY As Decimal
    ''' <summary>[圓弧圓心座標Z(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public CenterPosZ As Decimal
    ''' <summary>[圓弧中繼點座標X(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosX As Decimal
    ''' <summary>[圓弧中繼點座標Y(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosY As Decimal
    ''' <summary>[圓弧中繼點座標Z(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosZ As Decimal
    ''' <summary>[圓弧中繼點座標B(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosB As Decimal
    ''' <summary>[圓弧中繼點座標C(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosC As Decimal
    ''' <summary>[轉速(Auger Valve)]</summary>
    ''' <remarks></remarks>
    Public RPM As Integer
    ''' <summary>[Direction][方向 0:CW  1:CCW]</summary>
    ''' <remarks></remarks>
    Public Direction As eArcDirection
    ''' <summary>[間距(um)] </summary>
    ''' <remarks></remarks>
    Public Pitch As Integer
    ''' <summary>[重量控制]</summary>
    ''' <remarks></remarks>
    Public WeightControl As SWeightControl
    ''' <summary>[圓參數設定檔]</summary> 'Eason 20170216 Ticket:100080 , Add Arc Type Parameter
    ''' <remarks></remarks> 
    Public ArcParameterName As String
End Structure

''' <summary>[畫弧(3D)]</summary>
''' <remarks></remarks>
Public Structure SArc3DParameter
    ''' <summary>[圓弧起始點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public StartPosX As Decimal
    ''' <summary>[圓弧起始點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public StartPosY As Decimal
    ''' <summary>[圓弧起始點座標Z(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public StartPosZ As Decimal
    ''' <summary>[圓弧終點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosX As Decimal
    ''' <summary>[圓弧終點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosY As Decimal
    ''' <summary>[圓弧終點座標Z(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosZ As Decimal
    ''' <summary>[圓弧圓心座標X(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public CenterPosX As Decimal
    ''' <summary>[圓弧圓心座標Y(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public CenterPosY As Decimal
    ''' <summary>[圓弧圓心座標Z(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public CenterPosZ As Decimal
    ''' <summary>[圓弧中繼點座標X(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosX As Decimal
    ''' <summary>[圓弧中繼點座標Y(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosY As Decimal
    ''' <summary>[圓弧中繼點座標Z(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosZ As Decimal
    ''' <summary>[圓弧中繼點座標B(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosB As Decimal
    ''' <summary>[圓弧中繼點座標C(相對於Basic基準點)]</summary>
    ''' <remarks></remarks>
    Public MiddlePosC As Decimal
    ''' <summary>[圓弧半徑] </summary>
    ''' <remarks></remarks>
    Public Radius As Decimal
    ''' <summary>圓弧運動角度(以正負號決定旋轉方向)</summary>
    ''' <remarks></remarks>
    Public Angle As Decimal
    ''' <summary>[轉速(Auger Valve)]</summary>
    ''' <remarks></remarks>
    Public RPM As Integer
    ''' <summary>[Direction][方向 0:CW  1:CCW]</summary>
    ''' <remarks></remarks>
    Public Direction As eArcDirection
    ''' <summary>[間距(um)] </summary>
    ''' <remarks></remarks>
    Public Pitch As Integer
    ''' <summary>[重量控制]</summary>
    ''' <remarks></remarks>
    Public WeightControl As SWeightControl
    ''' <summary>[弧參數設定檔]</summary> 'Eason 20170216 Ticket:100080 , Add Arc Type Parameter
    ''' <remarks></remarks> 
    Public ArcParameterName As String
End Structure

''' <summary>[移動(3D)]</summary>
''' <remarks></remarks>
Public Structure SMove3DParameter
    ''' <summary>[移動點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosX As Decimal
    ''' <summary>[移動點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosY As Decimal
    ''' <summary>[移動點座標Z(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosZ As Decimal
End Structure


''' <summary>
''' 陣列呼叫設定
''' </summary>
''' <remarks></remarks>
Public Structure SArrayParameter
    ''' <summary>配接用物件</summary>
    ''' <remarks></remarks>
    Public Pattern As CPatternMap
    ''' <summary>叫用Pattern名稱</summary>
    ''' <remarks></remarks>
    Public PatternID As String
    ''' <summary>基準點偏移量X</summary>
    ''' <remarks></remarks>
    Public BasicOffsetX As Decimal
    ''' <summary>基準點偏移量Y</summary>
    ''' <remarks></remarks>
    Public BasicOffsetY As Decimal
    ''' <summary>基準點偏移量Z</summary>
    ''' <remarks></remarks>
    Public BasicOffsetZ As Decimal
    ''' <summary>陣列間距</summary>
    ''' <remarks></remarks>
    Public PitchX As Decimal
    ''' <summary>陣列間距</summary>
    ''' <remarks></remarks>
    Public PitchY As Decimal
    ''' <summary>陣列數量</summary>
    ''' <remarks></remarks>
    Public CountX As Integer
    ''' <summary>陣列數量</summary>
    ''' <remarks></remarks>
    Public CountY As Integer
End Structure

''' <summary>
''' 單一呼叫設定
''' </summary>
''' <remarks></remarks>
Public Structure SSubPatternParameter
    ''' <summary>配接用物件</summary>
    ''' <remarks></remarks>
    Public Pattern As CPatternMap
    ''' <summary>叫用Pattern名稱</summary>
    ''' <remarks></remarks>
    Public PatternID As String
    ''' <summary>基準點偏移量X</summary>
    ''' <remarks></remarks>
    Public BasicOffsetX As Decimal
    ''' <summary>基準點偏移量Y</summary>
    ''' <remarks></remarks>
    Public BasicOffsetY As Decimal
    ''' <summary>基準點偏移量Z</summary>
    ''' <remarks></remarks>
    Public BasicOffsetZ As Decimal
End Structure

Public Structure SFindHeightParameter
    ''' <summary>[移動點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosX As Decimal
    ''' <summary>[移動點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosY As Decimal
    ''' <summary>[移動點座標Z(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosZ As Decimal
    ''' <summary>具備雷射主機款式者需要選Recipe</summary>
    ''' <remarks></remarks>
    Public ProgramID As String
End Structure

''' <summary>定位設定</summary>
''' <remarks></remarks>
Public Structure SAlignParameter
    ''' <summary>[移動點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosX As Decimal
    ''' <summary>[移動點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosY As Decimal
    ''' <summary>[移動點座標Z(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosZ As Decimal
    ''' <summary>場景名稱</summary>
    ''' <remarks></remarks>
    Public SceneID As String
End Structure

''' <summary>
''' 秤重設定
''' </summary>
''' <remarks></remarks>
Public Structure SWeightParamter
    ''' <summary>平台閥號</summary>
    ''' <remarks></remarks>
    Public ValveNo As Integer
End Structure

''' <summary>
''' 檢測設定
''' </summary>
''' <remarks></remarks>
Public Structure SInspectParameter
    ''' <summary>[移動點座標X(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosX As Decimal
    ''' <summary>[移動點座標Y(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosY As Decimal
    ''' <summary>[移動點座標Z(相對於Basic基準點)] </summary>
    ''' <remarks></remarks>
    Public EndPosZ As Decimal
    Public SceneID As String
    Public InspectionData As List(Of SAOIResultProperty)
End Structure

''' <summary>影像結果屬性</summary>
''' <remarks></remarks>
Public Structure SAOIResultProperty
    ''' <summary>名稱</summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>屬性實際值</summary>
    ''' <remarks></remarks>
    Public Value As Decimal
    ''' <summary>最大可接受值</summary>
    ''' <remarks></remarks>
    Public MaxValue As Decimal
    ''' <summary>最小可接受值</summary>
    ''' <remarks></remarks>
    Public MinValue As Decimal
    ''' <summary>顯示單位</summary>
    ''' <remarks></remarks>
    Public Unit As String
End Structure

#End Region


''' <summary>[膠重控制模式(點數、重量)]</summary>
''' <remarks></remarks>
Public Enum eWeightControlType
    ''' <summary>[點數]</summary>
    ''' <remarks></remarks>
    Dots = 0
    ''' <summary>[重量]</summary>
    ''' <remarks></remarks>
    Weight = 1
    ' ''' <summary>[點數、重量混搭]</summary>
    ' ''' <remarks></remarks>
    'Complex = 2
    ''' <summary>[速度]</summary>
    ''' <remarks></remarks>
    Velocity = 3
End Enum


''' <summary>[是根據的怎樣的條件做秤重]</summary>
''' <remarks></remarks>
Public Enum eFlowRate
    ''' <summary>[不做]</summary>
    ''' <remarks></remarks>
    Noen = 0
    ''' <summary>[幾分鐘做一次]</summary>
    ''' <remarks></remarks>
    OnTimer = 1
    ''' <summary>[幾片做一次]</summary>
    ''' <remarks></remarks>
    OnRuns = 2
    ''' <summary>[幾分鐘or幾片做一次]</summary>
    ''' <remarks></remarks>
    OnTimerOrRuns = 3
End Enum

''' <summary>[數值的型態]</summary>
''' <remarks></remarks>
Public Structure SValue
    ''' <summary>[設定值]</summary>
    ''' <remarks></remarks>
    Public Setting As Decimal
    ''' <summary>[真實值]</summary>
    ''' <remarks></remarks>
    Public Real As Decimal
End Structure

Public Enum eCleanType
    ''' <summary>真空除膠</summary>
    ''' <remarks></remarks>
    VacuumClean = 0
    ''' <summary>噴頭擦拭</summary>
    ''' <remarks></remarks>
    JetClean = 1
    ''' <summary>針式擦拭</summary>
    ''' <remarks></remarks>
    AugerClean = 2

    '20160920
    ''' <summary>真空除膠+噴頭擦拭</summary>
    ''' <remarks></remarks>
    VacuumJetClean = 3
    ''' <summary>真空除膠+針式擦拭</summary>
    ''' <remarks></remarks>
    VacuumAugerClean = 4

End Enum

''' <summary>[檢查的機制是根據怎樣的條件]</summary>
''' <remarks></remarks>
Public Enum eInspectionType
    ''' <summary>[不做]</summary>
    ''' <remarks></remarks>
    Noen = 0
    ''' <summary>[幾分鐘做一次]</summary>
    ''' <remarks></remarks>
    OnTimer = 1
    ''' <summary>[幾片做一次]</summary>
    ''' <remarks></remarks>
    OnRuns = 2
    ''' <summary>[幾分鐘or幾片做一次]</summary>
    ''' <remarks></remarks>
    OnTimerOrRuns = 3
End Enum

''' <summary>[速度產生的方式]</summary>
''' <remarks></remarks>
Public Enum eVelocityMode
    ''' <summary>[自動產生]</summary>
    ''' <remarks></remarks>
    Auto = 0
    ''' <summary>[手動Key In]</summary>
    ''' <remarks></remarks>
    Manual = 1
End Enum

''' <summary>外部資料</summary>
''' <remarks></remarks>
Public Structure SExternData
    ''' <summary>外部輸入資料名稱</summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>外部輸入資料值</summary>
    ''' <remarks></remarks>
    Public Value As Decimal
    ''' <summary>外部輸入資料規格上界</summary>
    ''' <remarks></remarks>
    Public USL As Decimal
    ''' <summary>外部輸入資料規格下界</summary>
    ''' <remarks></remarks>
    Public LSL As Decimal
    ''' <summary>外部輸入資料單位</summary>
    ''' <remarks></remarks>
    Public Unit As String
    ''' <summary>外部輸入資料異常警報等級</summary>
    ''' <remarks></remarks>
    Public AlarmLevel As Integer

End Structure


''' <summary>[點膠時間控制模式]</summary>
''' <remarks></remarks>
Public Enum eProcessTimeType
    ''' <summary>[非點膠時間控制]</summary>
    ''' <remarks></remarks>
    None = 0
    ''' <summary>[隔多久時間返回點下一道]</summary>
    ''' <remarks></remarks>
    ReturnTime = 1
    ''' <summary>[這道點完等多久才點下一道]</summary>
    ''' <remarks></remarks>
    NextRoundDelayTime = 2
End Enum


''' <summary>單步參數</summary>
''' <remarks></remarks>
Public Class CPatternStep

#Region "可設定參數"
    ''' <summary>Inspect影像場景ID</summary>
    ''' <remarks></remarks>
    Public SceneID As String
    ''' <summary>Laser測高對應的場景ID</summary>
    ''' <remarks></remarks>
    Public LaserProgramID As String
    ''' <summary>[Step編號] </summary>
    ''' <remarks></remarks>
    Public StepNo As Integer
    ''' <summary>[步驟樣式]</summary>
    ''' <remarks></remarks>
    Public StepType As eStepFunctionType
    ''' <summary>[選擇閥組]</summary>
    ''' <remarks></remarks>
    Public SelectValve As SSelectValveParameter
    ''' <summary>[等待]</summary>
    ''' <remarks></remarks>
    Public Wait As SWaitParameter
    ''' <summary>[畫圓(2D)]</summary>
    ''' <remarks></remarks>
    Public Circle2D As SCircle2DParameter
    ''' <summary>[畫弧(2D)]</summary>
    ''' <remarks></remarks>
    Public Arc2D As SArc2DParameter
    ''' <summary>[畫點(3D)]</summary>
    ''' <remarks></remarks>
    Public Dots3D As SDots3DParameter
    ''' <summary>[畫線3D]</summary>
    ''' <remarks></remarks>
    Public Line3D As SLine3DParameter
    ''' <summary>[畫圓(3D)]</summary>
    ''' <remarks></remarks>
    Public Circle3D As SCircle3DParameter
    ''' <summary>[畫弧(3D)]</summary>
    ''' <remarks></remarks>
    Public Arc3D As SArc3DParameter
    ''' <summary>[移動(3D)]</summary>
    ''' <remarks></remarks>
    Public Move3D As SMove3DParameter
    ''' <summary>陣列型叫用Pattern(預留)</summary>
    ''' <remarks></remarks>
    Public Array As SArrayParameter
    ''' <summary>單一叫用Pattern(預留)</summary>
    ''' <remarks></remarks>
    Public SubPattern As SSubPatternParameter
    ''' <summary>Laser/CCD等外部輸入資料清單</summary>
    ''' <remarks></remarks>
    Public InputData As New List(Of SExternData)
    ''' <summary>註解說明</summary>
    ''' <remarks></remarks>
    Public Comment As String

#End Region

    Public Function enmGraphicTypeToString() As String
        Select Case StepType
            Case eStepFunctionType.Arc2D
                Return "Arc"
            Case eStepFunctionType.Arc3D
                Return "Arc3D"
            Case eStepFunctionType.CCDEnd
                Return "CCDEnd"
            Case eStepFunctionType.CCDLine
                Return "CCDLine"
            Case eStepFunctionType.CCDStart
                Return "CCDStart"
            Case eStepFunctionType.Circle2D
                Return "Circle"
            Case eStepFunctionType.Circle3D
                Return "Circle3D"
            Case eStepFunctionType.ContiEnd
                Return "ContiEnd"
            Case eStepFunctionType.ContiStart
                Return "ContiStart"
            Case eStepFunctionType.Dots3D
                Return "Dots"
            Case eStepFunctionType.EndLine
                Return "EndLine"
            Case eStepFunctionType.FirstLine
                Return "FirstLine"
            Case eStepFunctionType.Inspect
                Return "Inspect"
            Case eStepFunctionType.Line3D
                Return "Line"
            Case eStepFunctionType.Move3D
                Return "Move"
            Case eStepFunctionType.Picture
                Return "Picture"
            Case eStepFunctionType.Rectangle
                Return "Rectangle"
            Case eStepFunctionType.SelectValve
                Return "SelectValve"
            Case eStepFunctionType.Wait
                Return "Wait"
            Case eStepFunctionType.ExtendOn
                Return "ExtendOn"
            Case eStepFunctionType.ExtendOff
                Return "ExtendOff"
        End Select
        Return "Undefined"
    End Function

    ''' <summary>
    ''' 複製成另一份副本
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Clone() As Object
        Dim CloneType As New CPatternStep

        CloneType.SceneID = SceneID
        CloneType.LaserProgramID = LaserProgramID
        CloneType.StepNo = StepNo
        CloneType.StepType = StepType
        CloneType.SelectValve = SelectValve
        CloneType.Wait = Wait
        CloneType.Circle2D = Circle2D
        CloneType.Arc2D = Arc2D
        CloneType.Dots3D = Dots3D
        CloneType.Line3D = Line3D
        CloneType.Circle3D = Circle3D
        CloneType.Arc3D = Arc3D
        CloneType.Move3D = Move3D
        CloneType.Array = Array
        CloneType.SubPattern = SubPattern
        CloneType.Comment = Comment

        CloneType.InputData = New List(Of SExternData)
        For Each item As SExternData In InputData
            CloneType.InputData.Add(item)
        Next

        Return CloneType
    End Function

End Class



''' <summary>單步參數</summary>
''' <remarks></remarks>
Public Structure SPatternStep
    ''' <summary>Inspect影像場景ID</summary>
    ''' <remarks></remarks>
    Public SceneID As String
    ''' <summary>Laser測高對應的場景ID</summary>
    ''' <remarks></remarks>
    Public LaserProgramID As String
    ''' <summary>[Step編號] </summary>
    ''' <remarks></remarks>
    Public StepNo As Integer
    ''' <summary>[步驟樣式]</summary>
    ''' <remarks></remarks>
    Public StepType As eStepFunctionType
    ''' <summary>[選擇閥組]</summary>
    ''' <remarks></remarks>
    Public SelectValve As SSelectValveParameter
    ''' <summary>[等待]</summary>
    ''' <remarks></remarks>
    Public Wait As SWaitParameter
    ''' <summary>[畫圓(2D)]</summary>
    ''' <remarks></remarks>
    Public Circle2D As SCircle2DParameter
    ''' <summary>[畫弧(2D)]</summary>
    ''' <remarks></remarks>
    Public Arc2D As SArc2DParameter
    ''' <summary>[畫點(3D)]</summary>
    ''' <remarks></remarks>
    Public Dots3D As SDots3DParameter
    ''' <summary>[畫線3D]</summary>
    ''' <remarks></remarks>
    Public Line3D As SLine3DParameter
    ''' <summary>[畫圓(3D)]</summary>
    ''' <remarks></remarks>
    Public Circle3D As SCircle3DParameter
    ''' <summary>[畫弧(3D)]</summary>
    ''' <remarks></remarks>
    Public Arc3D As SArc3DParameter
    ''' <summary>[移動(3D)]</summary>
    ''' <remarks></remarks>
    Public Move3D As SMove3DParameter
    ''' <summary>陣列型叫用Pattern(預留)</summary>
    ''' <remarks></remarks>
    Public Array As SArrayParameter
    ''' <summary>單一叫用Pattern(預留)</summary>
    ''' <remarks></remarks>
    Public SubPattern As SSubPatternParameter
    ''' <summary>Laser/CCD等外部輸入資料清單</summary>
    ''' <remarks></remarks>
    Public InputData As List(Of SExternData)
    ''' <summary>註解說明</summary>
    ''' <remarks></remarks>
    Public Comment As String
End Structure
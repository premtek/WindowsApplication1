
''' <summary>
''' [定位結構]
''' </summary>
''' <remarks></remarks>
Public Class AlignmentStructure
    ''' <summary>
    ''' [定位點X軸座標]
    ''' </summary>
    ''' <remarks>陣列索引(0,0)</remarks>
    Public AlignPosX As Decimal
    ''' <summary>
    ''' [定位點Y軸座標]
    ''' </summary>
    ''' <remarks>陣列索引(0,0)</remarks>
    Public AlignPosY As Decimal
    ''' <summary>
    ''' [定位點Z軸座標]
    ''' </summary>
    ''' <remarks>陣列索引(0,0)</remarks>
    Public AlignPosZ As Decimal
    ''' <summary>介面教導位置
    ''' </summary>
    ''' <remarks>不限定索引</remarks>
    Public TeachPosX As Decimal
    ''' <summary>介面教導位置
    ''' </summary>
    ''' <remarks>不限定索引</remarks>
    Public TeachPosY As Decimal
    ''' <summary>介面教導位置
    ''' </summary>
    ''' <remarks>不限定索引</remarks>
    Public TeachPosZ As Decimal
    ''' <summary>
    ''' [定位點場景編號]
    ''' </summary>
    ''' <remarks></remarks>
    Public AlignScene As String
    ''' <summary>[教導時與中心的點的偏移量(Golden Pattern與畫面中心點偏移量)]</summary>
    ''' <remarks></remarks>
    Public AlignOffsetX As Decimal
    ''' <summary>[教導時與中心的點的偏移量(Golden Pattern與畫面中心點偏移量)]</summary>
    ''' <remarks></remarks>
    Public AlignOffsetY As Decimal
    ''' <summary>[教導時的角度偏移(Deg)]</summary>
    ''' <remarks></remarks>
    Public AlignRoation As Decimal

    ''' <summary>
    ''' 複製
    ''' </summary>
    Public Function Clone()
        Dim copyAS As New AlignmentStructure
        copyAS.AlignPosX = AlignPosX
        copyAS.AlignPosY = AlignPosY
        copyAS.AlignPosZ = AlignPosZ
        copyAS.AlignScene = AlignScene
        copyAS.AlignOffsetX = AlignOffsetX
        copyAS.AlignOffsetY = AlignOffsetY
        copyAS.AlignRoation = AlignRoation
        copyAS.TeachPosX = TeachPosX
        copyAS.TeachPosY = TeachPosY
        copyAS.TeachPosZ = TeachPosZ
        Return copyAS
    End Function
End Class

''' <summary>
''' [雷射測高結構]
''' </summary>
''' <remarks></remarks>
Public Class LaserStructure
    ''' <summary>介面教導位置
    ''' </summary>
    ''' <remarks>不限定索引</remarks>
    Public TeachPosX As Decimal
    ''' <summary>介面教導位置
    ''' </summary>
    ''' <remarks>不限定索引</remarks>
    Public TeachPosY As Decimal
    ''' <summary>介面教導位置
    ''' </summary>
    ''' <remarks>不限定索引</remarks>
    Public TeachPosZ As Decimal
    ''' <summary>
    ''' [雷射測高X軸座標]
    ''' </summary>
    ''' <remarks></remarks>
    Public LaserPositionX As Decimal
    ''' <summary>
    ''' [雷射測高Y軸座標]
    ''' </summary>
    ''' <remarks></remarks>
    Public LaserPositionY As Decimal
    ''' <summary>
    ''' [雷射測高Z軸座標]
    ''' </summary>
    ''' <remarks></remarks>
    Public LaserPositionZ As Decimal
    ''' <summary>
    ''' [雷射資料編號1]
    ''' </summary>
    ''' <remarks></remarks>
    Public LaserData1 As Decimal
    ''' <summary>
    ''' [校正後雷射量測點X]
    ''' </summary>
    ''' <remarks></remarks>
    Public LaserCalibrationCCDPosX As Decimal
    ''' <summary>
    ''' [校正後雷射量測點Y]
    ''' </summary>
    ''' <remarks></remarks>
    Public LaserCalibrationCCDPosY As Decimal
    ''' <summary>
    ''' [校正後雷射量測點Z]
    ''' </summary>
    ''' <remarks></remarks>
    Public LaserCalibrationCCDPosZ As Decimal
End Class
''' <summary>
''' [檢測資料結構]
''' </summary>
''' <remarks></remarks>
Public Structure InspectorStructure
    ''' <summary>
    ''' [檢測點X軸座標]
    ''' </summary>
    ''' <remarks></remarks>
    Public InspectorPositionX As Decimal
    ''' <summary>
    ''' [檢測點Y軸座標]
    ''' </summary>
    ''' <remarks></remarks>
    Public InspectorPositionY As Decimal
    ''' <summary>
    ''' [檢測點Z軸座標]
    ''' </summary>
    ''' <remarks></remarks>
    Public InspectorPositionZ As Decimal
    ''' <summary>
    ''' [檢測點場景編號]
    ''' </summary>
    ''' <remarks></remarks>
    Public InspectorScene As Integer
    ''' <summary>
    ''' [檢測資料編號1]
    ''' </summary>
    ''' <remarks></remarks>
    Public InspectorData1 As Decimal
    ''' <summary>
    ''' [檢測資料編號2]
    ''' </summary>
    ''' <remarks></remarks>
    Public InspectorData2 As Decimal
    ''' <summary>
    ''' [檢測資料編號3]
    ''' </summary>
    ''' <remarks></remarks>
    Public InspectorData3 As Decimal
    ''' <summary>
    ''' [檢測資料編號4]
    ''' </summary>
    ''' <remarks></remarks>
    Public InspectorData4 As Decimal
    ''' <summary>
    ''' [檢測資料編號5]
    ''' </summary>
    ''' <remarks></remarks>
    Public InspectorData5 As Decimal
    ''' <summary>
    ''' [檢測資料編號6]
    ''' </summary>
    ''' <remarks></remarks>
    Public InspectorData6 As Decimal
    ''' <summary>
    ''' [校正後檢測的拍照點X]
    ''' </summary>
    ''' <remarks></remarks>
    Public InspectorCalibrationPosX As Decimal
    ''' <summary>
    ''' [校正後檢測的拍照點Y]
    ''' </summary>
    ''' <remarks></remarks>
    Public InspectorCalibrationPosY As Decimal
    ''' <summary>
    ''' [校正後檢測的拍照點Z]
    ''' </summary>
    ''' <remarks></remarks>
    Public InspectorCalibrationPosZ As Decimal
End Structure
''' <summary>
''' [節點偏移資料結構]
''' </summary>
''' <remarks></remarks>
Public Structure NodeOffset
    ''' <summary>
    ''' [Node名稱]
    ''' </summary>
    ''' <remarks></remarks>
    Public NodeName As String
    ''' <summary>
    ''' [節點X軸偏移量]
    ''' </summary>
    ''' <remarks></remarks>
    Public NodeOffsetX As Decimal
    ''' <summary>
    ''' [節點Y軸偏移量]
    ''' </summary>
    ''' <remarks></remarks>
    Public NodeOffsetY As Decimal
    ''' <summary>
    ''' [節點Z軸偏移量]
    ''' </summary>
    ''' <remarks></remarks>
    Public NodeOffsetZ As Decimal
End Structure
''' <summary>
''' [校正後定位相關資料]
''' </summary>
''' <remarks></remarks>
Public Structure CalibrationData
    ''' <summary>
    ''' [校正過後定位點X]
    ''' </summary>
    ''' <remarks></remarks>
    Public BasicCalibrationPosX As Decimal
    ''' <summary>
    ''' [校正過後定位點Y]
    ''' </summary>
    ''' <remarks></remarks>
    Public BasicCalibrationPosY As Decimal
    ''' <summary>
    ''' [校正過後定位點Z]
    ''' </summary>
    ''' <remarks></remarks>
    Public BasicCalibrationPosZ As Decimal
    ''' <summary>
    ''' [Pattern X軸向偏移]
    ''' </summary>
    ''' <remarks></remarks>
    Public PatternOffsetX As Decimal
    ''' <summary>
    ''' [Pattern Y軸向偏移]
    ''' </summary>
    ''' <remarks></remarks>
    Public PatternOffsetY As Decimal
    ''' <summary>
    ''' [Pattern旋轉量]
    ''' </summary>
    ''' <remarks></remarks>
    Public PatternRotation As Decimal
End Structure

' ''' <summary>陣列設定</summary>
' ''' <remarks></remarks>
'Public Class CArray
'    ''' <summary>X軸方向間距(mm)</summary>
'    ''' <remarks></remarks>
'    Public PitchX As Decimal
'    ''' <summary>Y軸方向間距(mm)</summary>
'    ''' <remarks></remarks>
'    Public PitchY As Decimal
'    ''' <summary>X軸方向數量</summary>
'    ''' <remarks></remarks>
'    Public CountX As Integer = 1
'    ''' <summary>Y軸方向數量</summary>
'    ''' <remarks></remarks>
'    Public CountY As Integer = 1
'    ''' <summary>旋轉角度</summary>
'    ''' <remarks></remarks>
'    Public Theta As Decimal

'    ''' <summary>開始位置</summary>
'    ''' <remarks></remarks>
'    Public StartPosX As Decimal
'    ''' <summary>開始位置</summary>
'    ''' <remarks></remarks>
'    Public StartPosY As Decimal
'    ''' <summary>結束位置</summary>
'    ''' <remarks></remarks>
'    Public EndPosX As Decimal
'    ''' <summary>結束位置</summary>
'    ''' <remarks></remarks>
'    Public EndPosY As Decimal

'End Class

''' <summary>
''' Pattern設定值
''' </summary>
''' <remarks></remarks>
Public Class CRecipePattern
    Inherits TreeNode '繼承 盡可能不要使用
    'TODO: CA2237	ISerializable 型別必須標記 SerializableAttribute	將 [Serializable] 加入 'CRecipePattern'，因為這個型別可以實作 ISerializable。	ProjectRecipe	CRecipePattern.vb	227

    ''' <summary>總Round數量</summary>
    ''' <remarks></remarks>
    Public RoundCount As Integer
    ''' <summary>Round</summary>
    ''' <remarks></remarks>
    Public Round As New List(Of CPatternRound)
    ''' <summary>[點膠時間控制模式]</summary>
    ''' <remarks></remarks>
    Public ProcessTimeType As eProcessTimeType
    ''' <summary>
    ''' [啟用節點旗標]
    ''' </summary>
    ''' <remarks></remarks>
    Public Enable As Boolean
    ''' <summary>
    ''' [Recipe連結名稱]
    ''' </summary>
    ''' <remarks></remarks>
    Public RecipeName As String
    ''' <summary>
    ''' [點膠啟用路徑]
    ''' </summary>
    ''' <remarks></remarks>
    Public DispenseNode As String

    ''' <summary>[Process time 顆數(for Underfill)]</summary>
    ''' <remarks></remarks>
    Public Diecount As Integer

    ''' <summary>
    ''' [Pattern定位資料]
    ''' </summary>
    ''' <remarks></remarks>
    Public PatternCalibration As New CalibrationData
    Sub New(ByVal Name As String)
        MyBase.New()
        Me.Name = Name
        Me.Text = Name
    End Sub

    Sub New()
        MyBase.New()
        Me.Name = ""
    End Sub

    Public Overrides Function Clone() As Object
        Dim CloneType As New CRecipePattern

        With CloneType
            .ProcessTimeType = ProcessTimeType
            .RoundCount = RoundCount

            .Round = New List(Of CPatternRound)
            For Each item As CPatternRound In Round
                .Round.Add(item.Clone)
            Next
            .Diecount = Diecount
            .DispenseNode = DispenseNode
            .PatternCalibration.BasicCalibrationPosX = PatternCalibration.BasicCalibrationPosX
            .PatternCalibration.BasicCalibrationPosY = PatternCalibration.BasicCalibrationPosY
            .PatternCalibration.BasicCalibrationPosZ = PatternCalibration.BasicCalibrationPosZ
            .PatternCalibration.PatternOffsetX = PatternCalibration.PatternOffsetX
            .PatternCalibration.PatternOffsetY = PatternCalibration.PatternOffsetY
            .PatternCalibration.PatternRotation = PatternCalibration.PatternRotation
            .Enable = Enable
            .RecipeName = RecipeName
            .Name = Name
            .Text = Text
        End With

        Return CloneType
    End Function

End Class

''' <summary>Pattern內第幾輪動作(Step)</summary>
''' <remarks></remarks>
Public Class CPatternRound
    ''' <summary>[Step總數量] </summary>
    ''' <remarks></remarks>
    Public StepCount As Integer
    ''' <summary>單步資訊</summary>
    ''' <remarks></remarks>
    Public CStep As New List(Of CPatternStep)
    ''' <summary>[Round生產時間(Sec)]</summary>
    ''' <remarks></remarks>
    Public ProcessTime As Decimal

    ''' <summary>建立時預設內容</summary>
    ''' <remarks></remarks>
    Public Sub New()
        Dim mStep1 As New CPatternStep
        Dim mStep2 As New CPatternStep
        Dim mStep3 As New CPatternStep
        mStep1.StepType = eStepFunctionType.SelectValve
        mStep1.SelectValve.ValveNo = ProjectCore.eValveWorkMode.Valve1
        mStep2.StepType = eStepFunctionType.ContiStart
        mStep3.StepType = eStepFunctionType.ContiEnd
        CStep.Add(mStep1)
        CStep.Add(mStep2)
        CStep.Add(mStep3)
    End Sub

    ''' <summary>
    ''' 複製成另一份副本
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Clone() As Object
        Dim CloneType As New CPatternRound

        CloneType.CStep = New List(Of CPatternStep)
        For Each item As CPatternStep In CStep
            CloneType.CStep.Add(item.Clone)
        Next

        CloneType.StepCount = StepCount
        CloneType.ProcessTime = ProcessTime

        Return CloneType
    End Function
End Class

''' <summary>[點膠時間控制模式]</summary>
''' <remarks></remarks>
Public Structure SProcessTime
    ''' <summary>[模式]</summary>
    ''' <remarks></remarks>
    Public Type As eProcessTimeType
    ''' <summary>[時間]</summary>
    ''' <remarks></remarks>
    Public Time As Decimal
End Structure



''' <summary>[Pattern結構(紀錄)]</summary>
''' <remarks></remarks>
Public Structure SRecipePattern
    ''' <summary>[點膠的Cycle Time]</summary>
    ''' <remarks></remarks>
    Public CycleTime As Decimal
    ''' <summary>總Round數量</summary>
    ''' <remarks></remarks>
    Public RoundCount As Integer
    ''' <summary>Round</summary>
    ''' <remarks></remarks>
    Public Round As List(Of SPatternRound)
    ''' <summary>[點膠時間控制模式]</summary>
    ''' <remarks></remarks>
    Public ProcessTimeType As eProcessTimeType
End Structure


''' <summary>Pattern內第幾輪動作(Step)</summary>
''' <remarks></remarks>
Public Structure SPatternRound
    ''' <summary>[Step總數量] </summary>
    ''' <remarks></remarks>
    Public StepCount As Integer
    ''' <summary>單步資訊</summary>
    ''' <remarks></remarks>
    Public SStep As List(Of SPatternStep)
    ''' <summary>[Round生產時間(Sec)]</summary>
    ''' <remarks></remarks>
    Public ProcessTime As Decimal
    ''' <summary>[Round點膠時間控制模式]</summary>
    ''' <remarks></remarks>
    Public ProcessTimeType As eProcessTimeType
End Structure

''' <summary>Recipe層級類型</summary>
''' <remarks></remarks>
Public Enum enmRecipeLevelType
    ''' <summary>陣列型</summary>
    ''' <remarks></remarks>
    Array
    ''' <summary>非陣列型</summary>
    ''' <remarks></remarks>
    NonArray
End Enum


''' <summary>分層名稱</summary>
''' <remarks></remarks>
Public Enum enmRecipeLevel
    ''' <summary>工件層</summary>
    ''' <remarks></remarks>
    Workpiece = 0
    ''' <summary>區塊層</summary>
    ''' <remarks></remarks>
    Blocks = 1
    ''' <summary>元件層</summary>
    ''' <remarks></remarks>
    Area = 2
End Enum

''' <summary>Recipe分層數</summary>
''' <remarks></remarks>
Public Enum enmRecipeLevelCount
    Unknown = 0
    ''' <summary>只有一個Block,n個Area</summary>
    ''' <remarks></remarks>
    Level1 = 1
    ''' <summary>一個Workpiece,n個Block,nxm個Area</summary>
    ''' <remarks></remarks>
    Level2 = 2
    ''' <summary>一個Base,n個Workpiece,nxm個Block,nxmxo個Area</summary>
    ''' <remarks></remarks>
    Level3 = 3
    MaxLevelCount = 3
End Enum


''' <summary>分層參數</summary>
''' <remarks></remarks>
Public Class CRecipeLevel
    ''' <summary>基準座標X(CCD取像位置)</summary>
    ''' <remarks></remarks>
    Public RefPosX As Double
    ''' <summary>基準座標Y(CCD取像位置)</summary>
    ''' <remarks></remarks>
    Public RefPosY As Double
    ''' <summary>基準座標Z(CCD取像高度)</summary>
    ''' <remarks></remarks>
    Public RefPosZ As Double
    ''' <summary>基準座標X(CCD取像位置) 第二組</summary>
    ''' <remarks></remarks>
    Public RefPosX2 As Double
    ''' <summary>基準座標Y(CCD取像位置) 第二組</summary>
    ''' <remarks></remarks>
    Public RefPosY2 As Double

    ''' <summary>層級形式(陣列,非陣列)</summary>
    ''' <remarks></remarks>
    Public LevelType As enmRecipeLevelType
    ''' <summary>陣列型X方向數量</summary>
    ''' <remarks></remarks>
    Public ArrayNumX As Double
    ''' <summary>陣列型Y方向數量</summary>
    ''' <remarks></remarks>
    Public ArrayNumY As Double
    ''' <summary>陣列型X方向間距</summary>
    ''' <remarks></remarks>
    Public ArrayPitchX As Double
    ''' <summary>陣列型Y方向間距</summary>
    ''' <remarks></remarks>
    Public ArrayPitchY As Double
    ''' <summary>陣列型角度(Degree)</summary>
    ''' <remarks></remarks>
    Public ArrayTheta As Double

    ''' <summary>非陣列數量</summary>
    ''' <remarks></remarks>
    Public NonArrayCount As Integer
    ''' <summary>非陣列偏移量X</summary>
    ''' <remarks></remarks>
    Public NonArrayOffsetX As New List(Of Double)
    ''' <summary>非陣列偏移量Y</summary>
    ''' <remarks></remarks>
    Public NonArrayOffsetY As New List(Of Double)
    ''' <summary>非陣列角度T</summary>
    ''' <remarks></remarks>
    Public NonArrayTheta As New List(Of Double)
    ''' <summary>
    ''' [定位起始場景] TODO:彈性流程完成後拆除
    ''' </summary>
    ''' <remarks></remarks>
    Public CCDStartScene As String
    ''' <summary>
    ''' [定位2起始場景] TODO:彈性流程完成後拆除
    ''' </summary>
    ''' <remarks></remarks>
    Public CCD2StartScene As String
    ''' <summary>定位檢測場景  TODO:彈性流程完成後拆除</summary>
    ''' <remarks></remarks>
    Public CCDStartInspectScene As String
End Class

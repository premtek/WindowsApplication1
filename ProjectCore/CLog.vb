


''' <summary>Stage索引集合</summary>
''' <remarks></remarks>
Public Structure sLevelIndexCollection
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public StageNo As enmStage
    ''' <summary>Pattern Name</summary>
    ''' <remarks></remarks>
    Public PatternName As String
    ''' <summary>節點 Name</summary>
    ''' <remarks></remarks>
    Public NodeName As String
    ''' <summary>Area Index X</summary>
    ''' <remarks></remarks>
    Public Xf As Integer
    ''' <summary>Area Index Y</summary>
    ''' <remarks></remarks>
    Public Yf As Integer
    ''' <summary>反向串接</summary>
    ''' <remarks></remarks>
    Public IsReverse As Boolean
    ''' <summary>
    ''' [List Index Count]
    ''' </summary>
    ''' <remarks></remarks>
    Public IndexCount As Integer        'Fenix+ 2016/01/09
    ''' <summary>
    ''' [判斷是否為每層的結束點]
    ''' </summary>
    ''' <remarks></remarks>
    Public IsLevelEnd As Boolean        'Fenix +2016/02/17
    ''' <summary>[走到第幾層了]</summary>
    ''' <remarks></remarks>
    Public LevelNo As Integer
    ''' <summary>[TreeNode Path]</summary>
    ''' <remarks></remarks>
    Public path As String


    Public Sub Clear()
        Xf = 0
        Yf = 0
        IndexCount = 0
        IsLevelEnd = False
    End Sub
End Structure


Imports ProjectCore
Imports ProjectRecipe
Imports ProjectTriggerBoard

Module MFunctionEditPath

#Region "轉換並紀錄最後塞給軸卡的資料內容"

    ''' <summary>[點膠行徑路線型態]</summary>
    ''' <remarks></remarks>
    Public Enum eWalkPathType
        ''' <summary>[Jet Valve]</summary>
        ''' <remarks></remarks>
        Jet = 0
        ''' <summary>[Auger Valve]</summary>
        ''' <remarks></remarks>
        Auger = 1
    End Enum

    ''' <summary>[塞路徑規劃]</summary>
    ''' <remarks></remarks>
    Public Structure sPatternPath
        ''' <summary>[點膠Pattern類型]</summary>
        ''' <remarks></remarks>
        Public PathType As ePathType
        ''' <summary>[點類型]</summary>
        ''' <remarks></remarks>
        Public Dot3D As sDot3DPath
        ''' <summary>[線類型]</summary>
        ''' <remarks></remarks>
        Public Line3D As sLine3DPath
        ''' <summary>[圓弧類型]</summary>
        ''' <remarks></remarks>
        Public Arc2D As sArc2DPath
        ''' <summary>[圓弧類型]</summary>
        ''' <remarks></remarks>
        Public Arc3D As sArc2DPath
        ''' <summary>[Dwell類型]</summary>
        ''' <remarks></remarks>
        Public Wait As sWaitPath
        ''' <summary>[為該顆產品的第一條路徑]</summary>
        ''' <remarks></remarks>
        Public IsFristPathInDie As Boolean

        ''' <summary>[取出速度、座標資訊]</summary>
        ''' <param name="velocity"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetPathVelocity(ByRef velocity As Decimal, ByRef pos As Premtek.sPos) As Boolean
            Dim mVelocity As Decimal
            Dim mPos As Premtek.sPos
            mVelocity = 0
            Select Case Me.PathType
                Case ePathType.Arc2D
                    mVelocity = Me.Arc2D.Velocity
                    mPos.PosX = Me.Arc2D.EndPosX
                    mPos.PosY = Me.Arc2D.EndPosY

                Case ePathType.Arc3D
                    mVelocity = Me.Arc3D.Velocity
                    mPos.PosX = Me.Arc3D.EndPosX
                    mPos.PosY = Me.Arc3D.EndPosY

                Case ePathType.Dot3D
                    mVelocity = Me.Dot3D.Velocity
                    mPos.PosX = Me.Dot3D.PosX
                    mPos.PosY = Me.Dot3D.PosY

                Case ePathType.Line3D
                    mVelocity = Me.Line3D.Velocity
                    mPos.PosX = Me.Line3D.EndPosX
                    mPos.PosY = Me.Line3D.EndPosY
            End Select

            velocity = mVelocity
            pos = mPos
            Return True
        End Function
    End Structure


    Public Enum ePathType
        ''' <summary>[點(3D)]</summary>
        ''' <remarks></remarks>
        Dot3D = 0
        ''' <summary>[線段(3D)]</summary>
        ''' <remarks></remarks>
        Line3D = 1
        ''' <summary>[圓弧(2D)]</summary>
        ''' <remarks></remarks>
        Arc2D = 2
        ''' <summary>[圓弧(3D)]</summary>
        ''' <remarks></remarks>
        Arc3D = 3
        ''' <summary>[Dwell]</summary>
        ''' <remarks></remarks>
        Wait = 4
    End Enum

    ''' <summary>[Dwell資訊]</summary>
    ''' <remarks></remarks>
    Public Structure sWaitPath
        ''' <summary>[Dwell等待時間(ms)]</summary>
        ''' <remarks></remarks>
        Public DwellTimeInMs As Decimal
        ''' <summary>[是否為出膠路徑]</summary>
        ''' <remarks></remarks>
        Public IsDispense As Boolean
        ''' <summary>[為該顆產品的第一條路徑]</summary>
        ''' <remarks></remarks>
        Public IsFristPathInDie As Boolean
        ''' <summary>[該 Row or Column的最後一個]</summary>
        ''' <remarks></remarks>
        Public IsLastRowColumn As Boolean
        ''' <summary>[強制延伸路徑]</summary>
        ''' <remarks></remarks>
        Public IsExtendOn As Boolean
    End Structure

    ''' <summary>[圓弧(2D)資訊]</summary>
    ''' <remarks></remarks>
    Public Structure sArc2DPath
        ''' <summary>[圓弧的起點座標X]</summary>
        ''' <remarks></remarks>
        Public StartPosX As Decimal
        ''' <summary>[圓弧的起點座標Y]</summary>
        ''' <remarks></remarks>
        Public StartPosY As Decimal
        ''' <summary>[圓弧的起點座標Z]</summary>
        ''' <remarks></remarks>
        Public StartPosZ As Decimal
        ''' <summary>[圓弧的中心點座標X]</summary>
        ''' <remarks></remarks>
        Public CenterPosX As Decimal
        ''' <summary>[圓弧的中心點座標Y]</summary>
        ''' <remarks></remarks>
        Public CenterPosY As Decimal
        ''' <summary>[圓弧的中心點座標Z]</summary>
        ''' <remarks></remarks>
        Public CenterPosZ As Decimal
        ''' <summary>[圓弧的終點座標X]</summary>
        ''' <remarks></remarks>
        Public EndPosX As Decimal
        ''' <summary>[圓弧的中心點座標Y]</summary>
        ''' <remarks></remarks>
        Public EndPosY As Decimal
        ''' <summary>[圓弧的中心點座標Z]</summary>
        ''' <remarks></remarks>
        Public EndPosZ As Decimal
        ''' <summary>[往X方向延伸的量]</summary>
        ''' <remarks></remarks>
        Public ExtendOffsetX As Decimal
        ''' <summary>[往Y方向延伸的量]</summary>
        ''' <remarks></remarks>
        Public ExtendOffsetY As Decimal
        ''' <summary>[往Z方向延伸的量]</summary>
        ''' <remarks></remarks>
        Public ExtendOffsetZ As Decimal
        ''' <summary>[圓弧的行徑方向]</summary>
        ''' <remarks></remarks>
        Public ArcDirection As eArcDirection
        ''' <summary>[是否為出膠路徑]</summary>
        ''' <remarks></remarks>
        Public IsDispense As Boolean
        ''' <summary>[移動速度]</summary>
        ''' <remarks></remarks>
        Public Velocity As Decimal
        ''' <summary>[為該顆產品的第一條路徑]</summary>
        ''' <remarks></remarks>
        Public IsFristPathInDie As Boolean
        ''' <summary>[該 Row or Column的最後一個]</summary>
        ''' <remarks></remarks>
        Public IsLastRowColumn As Boolean
        ''' <summary>[角度(不須正負號，單純用來計算弧長)]</summary>
        ''' <remarks></remarks>
        Public Angle As Decimal
        ''' <summary>[重量控制]</summary>
        ''' <remarks></remarks>
        Public WeightControl As SWeightControl
        ''' <summary>[強制延伸路徑]</summary>
        ''' <remarks></remarks>
        Public IsExtendOn As Boolean
        ''' <summary>[參數類型]</summary>
        ''' <remarks></remarks>
        Public ParameterType As ePathParameterType
        ''' <summary>[搭配的Parameter名稱]</summary>
        ''' <remarks></remarks>
        Public ParameterName As String
        '20171016
        ''' <summary>[線段的初速(mm/s)]</summary>
        ''' <remarks></remarks>
        Public StartVel As Decimal

    End Structure

    ''' <summary>[點(3D)資訊]</summary>
    ''' <remarks></remarks>
    Public Structure sDot3DPath
        ''' <summary>[點座標X] </summary>
        ''' <remarks></remarks>
        Public PosX As Decimal
        ''' <summary>[點座標Y] </summary>
        ''' <remarks></remarks>
        Public PosY As Decimal
        ''' <summary>[點座標Z] </summary>
        ''' <remarks></remarks>
        Public PosZ As Decimal
        ''' <summary>[往X方向延伸的量]</summary>
        ''' <remarks></remarks>
        Public ExtendOffsetX As Decimal
        ''' <summary>[往Y方向延伸的量]</summary>
        ''' <remarks></remarks>
        Public ExtendOffsetY As Decimal
        ''' <summary>[往Z方向延伸的量]</summary>
        ''' <remarks></remarks>
        Public ExtendOffsetZ As Decimal
        ''' <summary>[是否為出膠路徑]</summary>
        ''' <remarks></remarks>
        Public IsDispense As Boolean
        ''' <summary>[移動速度]</summary>
        ''' <remarks></remarks>
        Public Velocity As Decimal
        ''' <summary>[為該顆產品的第一條路徑]</summary>
        ''' <remarks></remarks>
        Public IsFristPathInDie As Boolean
        ''' <summary>[該 Row or Column的最後一個]</summary>
        ''' <remarks></remarks>
        Public IsLastRowColumn As Boolean
        ''' <summary>[重量控制]</summary>
        ''' <remarks></remarks>
        Public WeightControl As SWeightControl
        ''' <summary>[強制延伸路徑]</summary>
        ''' <remarks></remarks>
        Public IsExtendOn As Boolean
        ''' <summary>[參數類型]</summary>
        ''' <remarks></remarks>
        Public ParameterType As ePathParameterType
        ''' <summary>[搭配的Parameter名稱]</summary>
        ''' <remarks></remarks>
        Public ParameterName As String
        ''' <summary>[Dots內觸發幾個點(只會有單點喔)]</summary>
        ''' <remarks></remarks>
        Public CcdTiggerCount As Integer
        '20171016
        ''' <summary>[線段的初速(mm/s)]</summary>
        ''' <remarks></remarks>
        Public StartVel As Decimal
    End Structure

    ''' <summary>[線段(3D)資訊]</summary>
    ''' <remarks></remarks>
    Public Structure sLine3DPath
        ''' <summary>[線段的起始點座標X] </summary>
        ''' <remarks></remarks>
        Public StartPosX As Decimal
        ''' <summary>[線段的起始點座標Y] </summary>
        ''' <remarks></remarks>
        Public StartPosY As Decimal
        ''' <summary>[線段的起始點座標Z] </summary>
        ''' <remarks></remarks>
        Public StartPosZ As Decimal
        ''' <summary>[線段的終點座標Z] </summary>
        ''' <remarks></remarks>
        Public EndPosX As Decimal
        ''' <summary>[線段的終點座標Z] </summary>
        ''' <remarks></remarks>
        Public EndPosY As Decimal
        ''' <summary>[線段的終點座標Z] </summary>
        ''' <remarks></remarks>
        Public EndPosZ As Decimal
        ''' <summary>[往X方向延伸的量]</summary>
        ''' <remarks></remarks>
        Public ExtendOffsetX As Decimal
        ''' <summary>[往Y方向延伸的量]</summary>
        ''' <remarks></remarks>
        Public ExtendOffsetY As Decimal
        ''' <summary>[往Z方向延伸的量]</summary>
        ''' <remarks></remarks>
        Public ExtendOffsetZ As Decimal
        ''' <summary>[是否為出膠路徑]</summary>
        ''' <remarks></remarks>
        Public IsDispense As Boolean
        ''' <summary>[是否為可延伸路徑]</summary>
        ''' <remarks></remarks>
        Public IsExtend As Boolean
        ''' <summary>[移動速度]</summary>
        ''' <remarks></remarks>
        Public Velocity As Decimal
        ''' <summary>[為該顆產品的第一條路徑]</summary>
        ''' <remarks></remarks>
        Public IsFristPathInDie As Boolean
        ''' <summary>[該 Row or Column的最後一個]</summary>
        ''' <remarks></remarks>
        Public IsLastRowColumn As Boolean
        ''' <summary>[重量控制]</summary>
        ''' <remarks></remarks>
        Public WeightControl As SWeightControl
        ''' <summary>[強制延伸路徑]</summary>
        ''' <remarks></remarks>
        Public IsExtendOn As Boolean
        ''' <summary>[參數類型]</summary>
        ''' <remarks></remarks>
        Public ParameterType As ePathParameterType
        ''' <summary>[搭配的Parameter名稱]</summary>
        ''' <remarks></remarks>
        Public ParameterName As String
        ''' <summary>[線段內處發幾個點]</summary>
        ''' <remarks></remarks>
        Public CcdTiggerCount As Integer

        '20171016
        ''' <summary>[線段的初速(mm/s)]</summary>
        ''' <remarks></remarks>
        Public StartVel As Decimal

    End Structure

    ''' <summary>[圓弧(3D)資訊]</summary>
    ''' <remarks></remarks>
    Public Structure sArc3DPath
        ''' <summary>[圓弧的起點座標X]</summary>
        ''' <remarks></remarks>
        Public StartPosX As Decimal
        ''' <summary>[圓弧的起點座標Y]</summary>
        ''' <remarks></remarks>
        Public StartPosY As Decimal
        ''' <summary>[圓弧的起點座標Z]</summary>
        ''' <remarks></remarks>
        Public StartPosZ As Decimal
        ''' <summary>[圓弧的中心點座標X]</summary>
        ''' <remarks></remarks>
        Public CenterPosX As Decimal
        ''' <summary>[圓弧的中心點座標Y]</summary>
        ''' <remarks></remarks>
        Public CenterPosY As Decimal
        ''' <summary>[圓弧的中心點座標Z]</summary>
        ''' <remarks></remarks>
        Public CenterPosZ As Decimal
        ''' <summary>[圓弧的終點座標X]</summary>
        ''' <remarks></remarks>
        Public EndPosX As Decimal
        ''' <summary>[圓弧的終點座標Y]</summary>
        ''' <remarks></remarks>
        Public EndPosY As Decimal
        ''' <summary>[圓弧的終點座標Z]</summary>
        ''' <remarks></remarks>
        Public EndPosZ As Decimal
        ''' <summary>[往X方向延伸的量]</summary>
        ''' <remarks></remarks>
        Public ExtendOffsetX As Decimal
        ''' <summary>[往Y方向延伸的量]</summary>
        ''' <remarks></remarks>
        Public ExtendOffsetY As Decimal
        ''' <summary>[往Z方向延伸的量]</summary>
        ''' <remarks></remarks>
        Public ExtendOffsetZ As Decimal
        ''' <summary>[圓弧的行徑方向]</summary>
        ''' <remarks></remarks>
        Public ArcDirection As eArcDirection
        ''' <summary>[是否為出膠路徑]</summary>
        ''' <remarks></remarks>
        Public IsDispense As Boolean
        ''' <summary>[移動速度]</summary>
        ''' <remarks></remarks>
        Public Velocity As Decimal
        ''' <summary>[為該顆產品的第一條路徑]</summary>
        ''' <remarks></remarks>
        Public IsFristPathInDie As Boolean
        ''' <summary>[該 Row or Column的最後一個]</summary>
        ''' <remarks></remarks>
        Public IsLastRowColumn As Boolean
        ''' <summary>[角度(不須正負號，單純用來計算弧長)]</summary>
        ''' <remarks></remarks>
        Public Angle As Decimal
        ''' <summary>[重量控制]</summary>
        ''' <remarks></remarks>
        Public WeightControl As SWeightControl
        ''' <summary>[強制延伸路徑]</summary>
        ''' <remarks></remarks>
        Public IsExtendOn As Boolean
        ''' <summary>[參數類型]</summary>
        ''' <remarks></remarks>
        Public ParameterType As ePathParameterType
        ''' <summary>[搭配的Parameter名稱]</summary>
        ''' <remarks></remarks>
        Public ParameterName As String
    End Structure


    ''' <summary>[點膠的路徑(最後要給軸卡的資料)]</summary>
    ''' <remarks></remarks>
    Public Structure sMotionPathRegister
        ''' <summary>[點膠Pattern類型]</summary>
        ''' <remarks></remarks>
        Public PathType As ePathRegisterType
        ''' <summary>[點類型(點、線段)]</summary>
        ''' <remarks></remarks>
        Public Dot3D As sDot3DPath
        ''' <summary>[圓弧類型(圓、弧)]</summary>
        ''' <remarks></remarks>
        Public Arc2D As sArc2DPath
        ''' <summary>[Dwell類型]</summary>
        ''' <remarks></remarks>
        Public Wait As sWaitPath
    End Structure

    Public Enum ePathRegisterType
        ''' <summary>[包含點、線段]</summary>
        ''' <remarks></remarks>
        Dot3D = 0
        ''' <summary>[包含圓、弧]</summary>
        ''' <remarks></remarks>
        Arc2D = 1
        ''' <summary>[Dwell類型]</summary>
        ''' <remarks></remarks>
        Wait = 2
    End Enum

    ''' <summary>[路徑參數類型]</summary>
    ''' <remarks></remarks>
    Public Enum ePathParameterType
        ''' <summary>[Dot的參數]</summary>
        ''' <remarks></remarks>
        Dot = 0
        ''' <summary>[Line的參數]</summary>
        ''' <remarks></remarks>
        Line = 1
        ''' <summary>[Arc的參數(基本上Line跟Arc參數應該要相同才對)]</summary>
        ''' <remarks></remarks>
        Arc = 2

    End Enum

#End Region



End Module

''' <summary></summary>
''' <remarks></remarks>
Public Enum eLevelType
    Array = 0
    NoneArray = 1
End Enum

''' <summary>單層參數設定</summary>
''' <remarks></remarks>
Public Class CRecipeNodeLevel
    ''' <summary>單層型態設定</summary>
    ''' <remarks></remarks>
    Public LevelType As eLevelType
    ''' <summary>陣列型設定</summary>
    ''' <remarks></remarks>
    Public Array As New CArray
    ''' <summary>非陣列型設定</summary>
    ''' <remarks></remarks>
    Public NonArray As New List(Of NonArray)

    ''' <summary>
    ''' 複製
    ''' </summary>
    Public Function Clone()
        Dim copyNodeLevel As New CRecipeNodeLevel
        copyNodeLevel.LevelType = LevelType
        copyNodeLevel.Array = Array.Clone()

        For Each temp In NonArray
            copyNodeLevel.NonArray.Add(temp)
        Next

        Return copyNodeLevel
    End Function
End Class

''' <summary>非陣列型參數設定</summary>
''' <remarks></remarks>
Public Structure NonArray
    ''' <summary>相對位置X</summary>
    ''' <remarks></remarks>
    Public RelPosX As Decimal
    ''' <summary>相對位置Y</summary>
    ''' <remarks></remarks>
    Public RelPosY As Decimal
    
End Structure

''' <summary>陣列設定</summary>
''' <remarks></remarks>
Public Class CArray
    ''' <summary>X軸方向間距(mm)</summary>
    ''' <remarks></remarks>
    Public PitchX As Decimal
    ''' <summary>Y軸方向間距(mm)</summary>
    ''' <remarks></remarks>
    Public PitchY As Decimal
    ''' <summary>X軸方向數量</summary>
    ''' <remarks></remarks>
    Public CountX As Integer = 1
    ''' <summary>Y軸方向數量</summary>
    ''' <remarks></remarks>
    Public CountY As Integer = 1
    ''' <summary>旋轉角度</summary>
    ''' <remarks></remarks>
    Public Theta As Decimal

    ''' <summary>開始位置</summary>
    ''' <remarks></remarks>
    Public StartPosX As Decimal
    ''' <summary>開始位置</summary>
    ''' <remarks></remarks>
    Public StartPosY As Decimal
    ''' <summary>開始位置</summary>
    ''' <remarks></remarks>
    Public StartPosZ As Decimal 'Soni + 2017.01.10 多層陣列
    ''' <summary>結束位置</summary>
    ''' <remarks></remarks>
    Public EndPosX As Decimal
    ''' <summary>結束位置</summary>
    ''' <remarks></remarks>
    Public EndPosY As Decimal

    ''' <summary>
    ''' 複製
    ''' </summary>
    Public Function Clone()
        Dim temp As New CArray
        temp.PitchX = PitchX
        temp.PitchY = PitchY
        temp.CountX = CountX
        temp.CountY = CountY
        temp.Theta = Theta
        temp.StartPosX = StartPosX
        temp.StartPosY = StartPosY
        temp.StartPosZ = StartPosZ
        temp.EndPosX = EndPosX
        temp.EndPosY = EndPosY
        Return temp
    End Function


End Class

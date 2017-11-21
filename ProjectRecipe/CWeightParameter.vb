Imports ProjectCore

''' <summary>
''' 秤重模式列舉
''' </summary>
''' <remarks></remarks>
Public Enum enmWeightMode
    ''' <summary>
    ''' 無,預設值
    ''' </summary>
    ''' <remarks></remarks>
    None
    ''' <summary>
    ''' 計時
    ''' </summary>
    ''' <remarks></remarks>
    ByTimer
    ''' <summary>
    ''' 板數
    ''' </summary>
    ''' <remarks></remarks>
    ByBoardCount
    ''' <summary>
    ''' 元件數
    ''' </summary>
    ''' <remarks></remarks>
    ByComponentCount
End Enum
''' <summary>天平參數</summary>
''' <remarks></remarks>
Public Class CWeightParameter
    ''' <summary>設定名稱</summary>
    ''' <remarks></remarks>
    Public Name As String

    ''' <summary>
    '''功能開關
    ''' </summary>
    ''' <remarks></remarks>
    Public Enabled As Boolean
    Public WeightMode As enmWeightMode

    ''' <summary>[期望重量]</summary>               
    ''' <remarks></remarks>
    Public TargetWeight As Decimal
    ''' <summary>[打點數]</summary>
    ''' <remarks></remarks>
    Public DotCount As Integer
    ''' <summary>[打點週期]</summary>               
    ''' <remarks></remarks>
    Public CycleTime As Decimal
    ''' <summary>[開閥時間]</summary>               
    ''' <remarks></remarks>
    Public OpenTime As Decimal

    ''' <summary>[微量天平 SteadyTime　等待多久再讀值]</summary>               
    ''' <remarks></remarks>
    Public BalanceStableTime As Decimal
    ''' <summary>[微量天平 移至天平上後等待多久才開始出膠]</summary>               
    ''' <remarks></remarks>
    Public StageStableTime As Decimal

    ''' <summary>[微量天平秤重Ratio]</summary>               
    ''' <remarks></remarks>
    Public AcceptTolerance As Decimal

    ''' <summary>[微量天平秤重次數]</summary>               
    ''' <remarks></remarks>
    Public RunCount As Decimal

    ''' <summary>[微量天平資料counter計數器]</summary>               
    ''' <remarks></remarks>
    Public DataCount As Integer

    ''' <summary>[微量天平資料平均單點重量]</summary>              20160526               
    ''' <remarks></remarks>
    Public AverageWeightDot As Decimal

    ''' <summary>多久秤重一次(秒)</summary>
    ''' <remarks></remarks>
    Public OnTimer As Decimal
    ''' <summary>
    ''' 多少片秤重一次
    ''' </summary>
    ''' <remarks></remarks>
    Public OnBoardCount As Integer
    ''' <summary>
    ''' 多少顆秤重一次
    ''' </summary>
    ''' <remarks></remarks>
    Public OnComponentCount As Integer

    ''' <summary>儲存溫度設定</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "Temperature"
        With Me
            Call SaveIniString(strSection, "Name", .Name, fileName)
            Call SaveIniString(strSection, "Enabled", CInt(.Enabled), fileName)
            Call SaveIniString(strSection, "WeightMode", CInt(.WeightMode), fileName)
            Call SaveIniString(strSection, "AcceptTolerance", .AcceptTolerance, fileName)
            Call SaveIniString(strSection, "AverageWeightDot", .AverageWeightDot, fileName)
            Call SaveIniString(strSection, "BalanceStableTime", .BalanceStableTime, fileName)
            Call SaveIniString(strSection, "CycleTime", .CycleTime, fileName)
            Call SaveIniString(strSection, "DataCount", .DataCount, fileName)
            Call SaveIniString(strSection, "DotCount", .DotCount, fileName)
            Call SaveIniString(strSection, "PulseTime", .OpenTime, fileName)
            Call SaveIniString(strSection, "RunCount", .RunCount, fileName)
            Call SaveIniString(strSection, "TableStableTime", .StageStableTime, fileName)
            Call SaveIniString(strSection, "TargetWeight", .TargetWeight, fileName)
            Call SaveIniString(strSection, "OnTimer", .OnTimer, fileName)
            Call SaveIniString(strSection, "OnBoardCount", .OnBoardCount, fileName)
            Call SaveIniString(strSection, "OnComponentCount", .OnComponentCount, fileName)
        End With
        Return True
    End Function

    ''' <summary>讀取溫度設定 </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "Temperature"
        With Me
            .Name = ReadIniString(strSection, "Name", fileName, "Untitled")
            .Enabled = Val(ReadIniString(strSection, "Enabled", fileName, 0))
            .WeightMode = Val(ReadIniString(strSection, "WeightMode", fileName, 0))
            .AcceptTolerance = ReadIniString(strSection, "AcceptTolerance", fileName, 0.1)
            .AverageWeightDot = ReadIniString(strSection, "AverageWeightDot", fileName, 0.04)
            .BalanceStableTime = ReadIniString(strSection, "BalanceStableTime", fileName, 10)
            .CycleTime = ReadIniString(strSection, "CycleTime", fileName, 4)
            .DataCount = ReadIniString(strSection, "DataCount", fileName, 5)
            .DotCount = ReadIniString(strSection, "DotCount", fileName, 100)
            .OpenTime = ReadIniString(strSection, "PulseTime", fileName, 2)
            .RunCount = ReadIniString(strSection, "RunCount", fileName, 1)
            .StageStableTime = ReadIniString(strSection, "TableStableTime", fileName, 5)
            .TargetWeight = ReadIniString(strSection, "TargetWeight", fileName, 1)
            .OnTimer = ReadIniString(strSection, "OnTimer", fileName, 3600)
            .OnBoardCount = ReadIniString(strSection, "OnBoardCount", fileName, 1)
            .OnComponentCount = ReadIniString(strSection, "OnComponentCount", fileName, 12)
        End With
        Return True
    End Function
End Class

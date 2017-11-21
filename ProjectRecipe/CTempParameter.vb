Imports ProjectCore

''' <summary>加熱點索引</summary>
''' <remarks></remarks>
Public Enum enmTemp
    ''' <summary>預熱站</summary>
    ''' <remarks></remarks>
    PreStation = 0
    ''' <summary>作業站</summary>
    ''' <remarks></remarks>
    WorkStation = 1
    ''' <summary>保溫站</summary>
    ''' <remarks></remarks>
    PostStation = 2
    ''' <summary>閥頭1</summary>
    ''' <remarks></remarks>
    Nozzle1 = 3
    ''' <summary>閥管1</summary>
    ''' <remarks></remarks>
    ValveBody1 = 4
    ''' <summary>膠管1</summary>
    ''' <remarks></remarks>
    SyringeBody1 = 5
    ''' <summary>閥頭2</summary>
    ''' <remarks></remarks>
    Nozzle2 = 6
    ''' <summary>閥管2</summary>
    ''' <remarks></remarks>
    ValveBody2 = 7
    ''' <summary>膠管2</summary>
    ''' <remarks></remarks>
    SyringeBody2 = 8
    ''' <summary>閥頭3</summary>
    ''' <remarks></remarks>
    Nozzle3 = 9
    ''' <summary>閥管3</summary>
    ''' <remarks></remarks>
    ValveBody3 = 10
    ''' <summary>膠管3</summary>
    ''' <remarks></remarks>
    SyringeBody3 = 11
    ''' <summary>閥頭4</summary>
    ''' <remarks></remarks>
    Nozzle4 = 12
    ''' <summary>閥管4</summary>
    ''' <remarks></remarks>
    ValveBody4 = 13
    ''' <summary>膠管4</summary>
    ''' <remarks></remarks>
    SyringeBody4 = 14

    ''' <summary>A機加熱平台1</summary>
    ''' <remarks></remarks>
    HotPlateA1 = 15
    ''' <summary>A機加熱平台2</summary>
    ''' <remarks></remarks>
    HotPlateA2 = 16
    ''' <summary>A機加熱平台3</summary>
    ''' <remarks></remarks>
    HotPlateA3 = 17
    ''' <summary>A機加熱平台4</summary>
    ''' <remarks></remarks>
    HotPlateA4 = 18
    ''' <summary>A機加熱平台5</summary>
    ''' <remarks></remarks>
    HotPlateA5 = 19
    ''' <summary>A機加熱平台6</summary>
    ''' <remarks></remarks>
    HotPlateA6 = 20
    ''' <summary>B機加熱平台1</summary>
    ''' <remarks></remarks>
    HotPlateB1 = 21
    ''' <summary>B機加熱平台2</summary>
    ''' <remarks></remarks>
    HotPlateB2 = 22
    ''' <summary>B機加熱平台3</summary>
    ''' <remarks></remarks>
    HotPlateB3 = 23
    ''' <summary>B機加熱平台4</summary>
    ''' <remarks></remarks>
    HotPlateB4 = 24
    ''' <summary>B機加熱平台5</summary>
    ''' <remarks></remarks>
    HotPlateB5 = 25
    ''' <summary>B機加熱平台6</summary>
    ''' <remarks></remarks>
    HotPlateB6 = 26

    '20160920
    ''' <summary>Loader加熱</summary>
    ''' <remarks></remarks>
    Loader = 27
    ''' <summary>UnLoader加熱</summary>
    ''' <remarks></remarks>
    Unloader = 28

    ''' <summary>總計</summary>
    ''' <remarks></remarks>
    Count = 29
End Enum

''' <summary>溫度設定值</summary>
''' <remarks></remarks>
Public Structure sTemperatureConfig
    ''' <summary>功能啟用</summary>
    ''' <remarks></remarks>
    Public Enabled As CheckState
    ''' <summary>設定溫度(度)</summary>
    ''' <remarks></remarks>
    Public SetValue As Decimal
    ''' <summary>設定值修正(度)</summary>
    ''' <remarks></remarks>
    Public PVOS As Decimal
    ' ''' <summary>溫度上限(度)</summary>
    ' ''' <remarks></remarks>
    'Public MaxValue As Decimal
End Structure

Public Class CTempParameter

    Sub New(ByVal name As String)
        Me.Name = name
        PriorHeatTime = 0
        For i As Integer = 0 To TempParam.Count - 1
            TempParam(i).Enabled = False
            TempParam(i).PVOS = 0
            TempParam(i).SetValue = 0
        Next
    End Sub

    ''' <summary>設定檔名</summary>
    ''' <remarks></remarks>
    Public Name As String
    '20161206
    ''' <summary>[料片預熱時間(入料後，需等待料片加熱一段時間後才能進行點膠)(ms)]</summary>
    ''' <remarks></remarks>
    Public PriorHeatTime As Decimal
    ''' <summary>溫度參數設定</summary>
    ''' <remarks></remarks>
    Public TempParam(enmTemp.Count - 1) As sTemperatureConfig 'Soni + 2015.09.06

    ''' <summary>儲存溫度設定</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function save(ByVal fileName As String) As Boolean
        Dim strSection As String
        strSection = "Temperature"
        With Me
            Call SaveIniString(strSection, "Name", .Name, fileName)
            '20161206
            Call SaveIniString(strSection, "PriorHeatTime", .PriorHeatTime, fileName)
            '--- 溫度設定 ---
            For i As Integer = 0 To enmTemp.Count - 1
                Call SaveIniString(strSection, "Temp_Enabled" & i, Math.Abs(CInt(.TempParam(i).Enabled)), fileName)
                Call SaveIniString(strSection, "Temp_SetValue" & i, .TempParam(i).SetValue, fileName)
                Call SaveIniString(strSection, "PVOS" & i, .TempParam(i).PVOS, fileName)
                'Call SaveIniString(strSection, "Temp_MaxValue" & i, .TempParam(i).MaxValue, strFileName)
            Next
            '--- 溫度設定 ---
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
            '20161206
            .PriorHeatTime = Val(ReadIniString(strSection, "PriorHeatTime", fileName, 0))
            '--- 溫度設定 ---
            For i As Integer = 0 To enmTemp.Count - 1
                .TempParam(i).Enabled = Val(ReadIniString(strSection, "Temp_Enabled" & i, fileName, 0))
                .TempParam(i).SetValue = Val(ReadIniString(strSection, "Temp_SetValue" & i, fileName, 0))
                .TempParam(i).PVOS = Val(ReadIniString(strSection, "PVOS" & i, fileName, 0))
                '.TempParam(i).MaxValue = Val(ReadIniString(strSection, "Temp_MaxValue" & i, strFileName, 0))
            Next
            '--- 溫度設定 ---
        End With
        Return True
    End Function
End Class

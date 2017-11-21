Imports ProjectAOI.CVX100_EthernetN_Parser
Imports ProjectCore

Public Class CVX100_EthernetN_Generator
    ''' <summary>快門速度
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ShutterSpeed
        S1_15 = 0
        S1_30 = 1
        S1_60 = 2
        S1_120 = 3
        S1_240 = 4
        S1_500 = 5
        S1_1k = 6
        S1_2k = 7
        S1_5k = 8
        S1_10k = 9
        S1_20k = 10
    End Enum
    Public Enum LimitType
        UpperLimit = 0
        LowerLimit = 1
    End Enum

    ''' <summary>分隔符
    ''' </summary>
    ''' <remarks></remarks>
    Const SEPARATOR = vbCr


    ''' <summary>
    ''' 發送一般的指令
    ''' </summary>
    ''' <param name="strcmd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendCommand(ByVal strcmd As String) As String
        Return strcmd & SEPARATOR
    End Function

    ''' <summary>發行指定的觸發
    ''' </summary>
    ''' <param name="TriggerIdx"></param>
    ''' <remarks></remarks>
    Public Function SoftwareTrigger(Optional TriggerIdx As Long = -1) As String
        Select Case TriggerIdx
            Case -1
                Return "TA" & SEPARATOR
            Case 1
                Return "T1" & SEPARATOR
            Case 2
                Return "T2" & SEPARATOR
            Case 3
                Return "T3" & SEPARATOR
            Case 4
                Return "T4" & SEPARATOR
            Case Else
                Return "SoftwareTrigger Parameter Error"
        End Select
    End Function
    ''' <summary>從設定模式遷移至運轉模式/從設定模式遷移至運轉模式
    ''' </summary>
    ''' <param name="mode"></param>
    ''' <remarks></remarks>
    Public Function SetMode(ByVal mode As ControllerMode) As String
        Select Case mode
            Case ControllerMode.Run
                Return "R0" & SEPARATOR
            Case ControllerMode.Setup
                Return "S0" & SEPARATOR
            Case Else
                Return "SetMode Error"
        End Select
    End Function
    Public Function ReadMode() As String
        Return "RM" & SEPARATOR
    End Function
    ''' <summary>清除所有包含圖像的各種緩存
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ClearData() As String
        Return "RS" & SEPARATOR
    End Function
    ''' <summary>保存當前的檢測設定，重新啟動。
    ''' </summary>    
    ''' <remarks></remarks>
    Public Function Reboot() As String
        Return "RB" & SEPARATOR
    End Function
    ''' <summary>保存當前的檢測設定、環境設定。
    ''' </summary>
    ''' <remarks></remarks>
    Public Function SaveConfig() As String
        Return "SS" & SEPARATOR
    End Function
    ''' <summary>清除錯誤狀態。非錯誤狀態時，會正常結束。
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ClearError() As String
        Return "CE" & SEPARATOR
    End Function
    ''' <summary>在指定運轉畫面及CCD畫面上切換顯示。
    ''' </summary>
    ''' <remarks></remarks>
    Public Function SetCCDView(ByVal CCDIdx As Integer) As String
        Return "VW,0," & CCDIdx & SEPARATOR
    End Function
    Public Function SetRunView(ByVal RunIdx As Integer) As String
        Return "VW,1," & RunIdx & SEPARATOR
    End Function
    Public Function ResetTrigger() As String
        Return "RE" & SEPARATOR
    End Function

    ''' <summary>偵測設定
    ''' </summary>
    ''' <param name="DetectName"></param>
    ''' <param name="CardID"></param>
    ''' <remarks></remarks>
    Public Function SetConfigDetect(ByVal DetectName As String, Optional CardID As Integer = 1) As String
        If CardID <> 1 And CardID <> 2 Then
            Throw New Exception("Illegal CardID @SetConfigDetect")
            Exit Function
        End If
        Debug.Print("PW," & CardID & "," & DetectName & SEPARATOR)
        Return "PW," & CardID & "," & DetectName & SEPARATOR
    End Function

    ''' <summary>設定快門速度
    ''' </summary>
    ''' <param name="ss"></param>
    ''' <param name="CardID"></param>
    ''' <remarks></remarks>
    Public Function SetCCDShutterSpeed(ByVal ss As ShutterSpeed, Optional CardID As Integer = 1) As String
        Return "CSH," & CardID & "," & CInt(ss) & SEPARATOR
    End Function
    ''' <summary>設定CCD靈敏度
    ''' </summary>
    ''' <param name="sensitivity"></param>
    ''' <param name="CardID"></param>
    ''' <remarks></remarks>
    Public Function SetCCDSensitivity(ByVal sensitivity As Integer, Optional CardID As Integer = 1) As String
        Return "CSE," & CardID & "," & sensitivity & SEPARATOR
    End Function
    Public Function SetTriggerDelay(ByVal ms As Integer, Optional CardID As Integer = 1) As String
        Return "CTD," & CardID & "," & ms & SEPARATOR
    End Function
    ''' <summary>在0~127間設定照度
    ''' </summary>
    ''' <param name="lightValue"></param>
    ''' <param name="CardID"></param>
    ''' <remarks></remarks>
    Public Function SetCCDLightValue(ByVal lightValue As Integer, Optional CardID As Integer = 1) As String
        Return "CLV," & CardID & "," & lightValue & SEPARATOR
    End Function
    ''' <summary>寫入執行條件
    ''' </summary>
    ''' <param name="ConditionIdx"></param>
    ''' <remarks></remarks>
    Public Function SetExecuteCondition(ConditionIdx As Integer) As String
        Return "EXW," & ConditionIdx & SEPARATOR
    End Function
    ''' <summary>讀出執行條件
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ReadExecuteCondition() As String
        Return "EXR" & SEPARATOR
    End Function
    Public Function SetConditionValue(ByVal tooID As Integer, ByVal itemID As Integer, ByVal limitType As LimitType, ByVal value As Integer) As String
        If limitType = CVX100_EthernetN_Generator.LimitType.LowerLimit Then
            Return "DW," & tooID & "," & itemID & ",0," & value & SEPARATOR
        Else
            Return "DW," & tooID & "," & itemID & ",1," & value & SEPARATOR
        End If
    End Function
    Public Function ReadConditionValue(ByVal tooID As Integer, ByVal itemID As Integer, ByVal limitType As LimitType) As String
        If limitType = CVX100_EthernetN_Generator.LimitType.LowerLimit Then
            Return "DR," & tooID & "," & itemID & ",0" & SEPARATOR
        Else
            Return "DR," & tooID & "," & itemID & ",1" & SEPARATOR
        End If
    End Function
    ''' <summary>設定損傷等級
    ''' </summary>
    ''' <param name="tooID"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Function SetDefectToolLevel(ByVal tooID As Integer, ByVal value As Integer) As String
        Return "SLW," & tooID & "," & value & SEPARATOR
    End Function
    ''' <summary>讀取損傷等級
    ''' </summary>
    ''' <param name="tooID"></param>
    ''' <remarks></remarks>
    Public Function ReadDefectToolLevel(ByVal tooID As Integer) As String
        Return "SLR," & tooID & SEPARATOR
    End Function
    ''' <summary>觸發輸入許可/禁止
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Public Function SetTriggerEnabled(ByVal enabled As Integer) As String
        Return "TE," & enabled & SEPARATOR
    End Function

    ''' <summary>輸出許可/禁止
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Public Function SetOutputEnabled(ByVal enabled As Integer) As String
        Return "TE," & enabled & SEPARATOR
    End Function
    ''' <summary>清除統計數據
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ClearStatistic() As String
        Return "TC" & SEPARATOR
    End Function
    ''' <summary>統計數據儲存
    ''' </summary>
    ''' <remarks></remarks>
    Public Function SaveStatistic() As String
        Return "TS" & SEPARATOR
    End Function
    ''' <summary>清除歷史圖像
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ClearHistory() As String
        Return "HC" & SEPARATOR
    End Function
    ''' <summary>清除歷史圖像
    ''' </summary>
    ''' <remarks></remarks>
    Public Function SaveHistory() As String
        Return "HS" & SEPARATOR
    End Function
    ''' <summary>畫面擷取
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ImageSave(Optional ftp As Integer = 1) As String
        If ftp = 1 Then
            Return "BC,1" & SEPARATOR
        Else
            Return "BC,0" & SEPARATOR
        End If
    End Function
    ''' <summary>切換輸出文件／資料夾
    ''' </summary>
    ''' <param name="dir"></param>
    ''' <remarks></remarks>
    Public Function ChangeOutputDirectory(Optional dir As Integer = 1) As String
        If dir = 1 Then
            Return "OW,1" & SEPARATOR
        Else
            Return "OW,0" & SEPARATOR
        End If
    End Function
    ''' <summary>外部機器會直接返回發送的字串。
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <remarks></remarks>
    Public Function EchoTest(ByVal msg As String) As String
        Return "EC," & msg & SEPARATOR
    End Function
    Public Function GetVersion() As String
        Return "VI" & SEPARATOR
    End Function
End Class

Imports ProjectCore

Public Enum enmControlType
    ''' <summary>流量氣壓控制</summary>
    ''' <remarks></remarks>
    FlowToAir = 0
    ''' <summary>流量體積控制</summary>
    ''' <remarks></remarks>
    FlowToPoints = 1
    ''' <summary>體積氣壓控制</summary>
    ''' <remarks></remarks>
    VolumeToAir = 2
    ''' <summary>體積點數控制</summary>
    ''' <remarks></remarks>
    VolumeToPoints = 3
    ''' <summary>重量氣壓控制</summary>
    ''' <remarks></remarks>
    WeightToAir = 4
    ''' <summary>重量點數控制</summary>
    ''' <remarks></remarks>
    WeightToPoints = 5
End Enum

''' <summary>FMCS體積控制</summary>
''' <remarks></remarks>
Public Class CFMCSVolumeControl
    Public ControlType As enmControlType

    '量測一定數量設定目標體積, 
    '量測一定數量取得回授體積
    '體積誤差回饋
    '不採用D作微分,是因為微分是雜訊放大, 由於流量計有40單位的雜訊.
    'PI控制器
    'u(k) = u(k-1) + (Ki*T+Kp) * e(k) - Kp*e(k-1)
    Public Event OnError(sender As Object, ByVal e As DataEventArgs)

    ''' <summary>功能啟用</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Enabled As Boolean = False
    ''' <summary>累積取樣點數</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FeekBackPerChuck As Integer = 1
    ''' <summary>資料平均點數</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AverageCount As Integer = 1

    ''' <summary>取樣點計數</summary>
    ''' <remarks></remarks>
    Dim feedbackIndex As Integer = 0
    ''' <summary>取樣累積</summary>
    ''' <remarks></remarks>
    Dim feedbackValue_0 As Double = 0
    Public feedbackValueList As New List(Of Double)

    ''' <summary>控制器比例參數</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Kp As Double = 1
    ''' <summary>控制器積分參數</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Ki As Double = 0
    ''' <summary>允收誤差</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AcceptLevel As Double
    ''' <summary>單一圖樣目標值</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetPatternValue As Double
    ''' <summary>平均體積規格上限</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AvgValueUSL As Double = Double.NaN
    ''' <summary>平均體積規格下限</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AvgValueLSL As Double = Double.NaN

    ''' <summary>輸出值(n-1)備存</summary>
    ''' <remarks></remarks>
    Dim output_1 As Double
    ''' <summary>輸出值(n)備存</summary>
    ''' <remarks></remarks>
    Public output_0 As Double
    ''' <summary>誤差值(n)備存</summary>
    ''' <remarks></remarks>
    Dim error_0 As Double
    ''' <summary>誤差值(n-1)備存</summary>
    ''' <remarks></remarks>
    Dim error_1 As Double

    ''' <summary>重置</summary>
    ''' <param name="output">預設輸出值(ex:Recipe氣壓)</param>
    ''' <remarks></remarks>
    Public Sub Reset(Optional ByVal output As Double = 0)
        output_0 = output
        output_1 = output
        error_1 = 0
        feedbackIndex = 0
        feedbackValue_0 = 0
    End Sub

    ''' <summary>FMCS持續更新資料,計數累計到達時,計算目標誤差,並以控制器調整</summary>
    ''' <param name="feedbackValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AirControl(ByVal feedbackValue As Double, ByRef outputvalue As Double) As Boolean
        'feedbackTotalVolume += feedbackVolume '體積累加

        feedbackValue_0 = feedbackValue '體積紀錄

        feedbackValueList.Add(feedbackValue)
        If feedbackValueList.Count > AverageCount Then
            feedbackValueList.RemoveAt(0)
        End If

        feedbackIndex += 1
        'sampleTime 

        If feedbackIndex >= FeekBackPerChuck Then '
            feedbackIndex = 0
            error_1 = error_0 '誤差迭代
            '--- 規格上限保護 ---
            If Not Double.IsNaN(AvgValueUSL) Then
                'If feedbackTotalVolume / DataCount > AvgVolumeUSL Then
                If feedbackValue_0 > AvgValueUSL Then
                    'Debug.Print("Alarm USL")
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2017001), "Alarm_2017001", eMessageLevel.Alarm) 'FMCS1資料超過規格上界!
                    gSyslog.Save("Feedback Value:" & feedbackValue_0 & ">" & AvgValueUSL)
                    RaiseEvent OnError(Me, New DataEventArgs("Alarm Volume Control Out of USL"))
                    Return False
                End If
            End If
            '--- 規格上限保護 ---

            '--- 規格下限保護 ---
            If Not Double.IsNaN(AvgValueLSL) Then
                If feedbackValue_0 < AvgValueLSL Then
                    'If feedbackTotalVolume / DataCount < AvgVolumeLSL Then
                    'Debug.Print("Alarm LSL")
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2017002), "Alarm_2017002", eMessageLevel.Alarm) 'FMCS1資料低於規格下界!
                    gSyslog.Save("Feedback Value:" & feedbackValue_0 & "<" & AvgValueLSL)
                    RaiseEvent OnError(Me, New DataEventArgs("Alarm Volume Control Out of LSL"))
                    Return False
                End If
            End If
            '--- 規格下限保護 ---

            error_0 = TargetPatternValue - feedbackValueList.Average ' feedbackValue_0
            'error_0 = TargetPatternVolume * DataCount - feedbackTotalVolume
            If Ki = 0 Then
                output_0 = Kp * error_0 '/ 1000
            Else
                'u(k) = u(k-1) + (Ki*T+Kp) * e(k) - kp*e(k-1)
                'u(k) =u(k-1) + (kp+ Ki*T/2)*e(k) - (kp-Ki*T/2)*e(k-1)
                output_0 = output_1 + (Kp + Ki / 2) * error_0 - (Kp - Ki / 2) * error_1
            End If

            feedbackValue_0 = 0 '體積重置

            '上下限
            If output_0 > 0.5 Then output_0 = 0.5
            If output_0 < 0 Then output_0 = 0

            output_1 = output_0 '迭代
            Return True
        End If
        Return False
    End Function

    Public Function PointsControl(ByVal feedbackVolume As Double) As Double
        feedbackValue_0 = feedbackVolume '體積
        'feedbackTotalValue += feedbackVolume '體積累加
        feedbackIndex += 1
        'sampleTime 

        If feedbackIndex >= FeekBackPerChuck Then '
            feedbackIndex = 0
            error_1 = error_0 '誤差迭代
            '--- 規格上限保護 ---
            If Not Double.IsNaN(AvgValueUSL) Then
                If feedbackValue_0 > AvgValueUSL Then
                    'Debug.Print("Alarm USL")
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2017001), "Alarm_2017001", eMessageLevel.Alarm) 'FMCS1資料超過規格上界!
                    gSyslog.Save("Feedback Value:" & feedbackValue_0 & ">" & AvgValueUSL)
                    RaiseEvent OnError(Me, New DataEventArgs("Alarm Volume Control Out of USL"))
                End If
            End If
            '--- 規格上限保護 ---

            '--- 規格下限保護 ---
            If Not Double.IsNaN(AvgValueLSL) Then
                If feedbackValue_0 < AvgValueLSL Then
                    'Debug.Print("Alarm LSL")
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2017002), "Alarm_2017002", eMessageLevel.Alarm) 'FMCS1資料低於規格下界!
                    gSyslog.Save("Feedback Value:" & feedbackValue_0 & "<" & AvgValueLSL)
                    RaiseEvent OnError(Me, New DataEventArgs("Alarm Volume Control Out of LSL"))
                End If
            End If
            '--- 規格下限保護 ---

            '點數修正
            output_0 = output_1 * TargetPatternValue / feedbackValue_0
            'output_0 = output_1 * TargetPatternValue * DataCount / feedbackTotalValue

            feedbackValue_0 = 0 '累計體積重置
            output_1 = output_0 '迭代
        End If
        Return output_0
    End Function

    ''' <summary>儲存設定</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal strFileName As String) As Boolean
        Dim strSection As String
        strSection = "Volume-Control"
        Call SaveIniString(strSection, "UnitCount", FeekBackPerChuck, strFileName)
        Call SaveIniString(strSection, "AverageCount", AverageCount, strFileName)
        Call SaveIniString(strSection, "TargetValue", TargetPatternValue, strFileName)
        Call SaveIniString(strSection, "Kp", Kp, strFileName)
        Call SaveIniString(strSection, "Ki", Ki, strFileName)
        'Call SaveIniString(strSection, "T", T, strFileName)
        Call SaveIniString(strSection, "Enabled", CInt(Enabled), strFileName)
        Return True
    End Function

    ''' <summary>讀取設定</summary>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal strFileName As String) As Boolean
        Dim strSection As String
        strSection = "Volume-Control"
        FeekBackPerChuck = CInt(ReadIniString(strSection, "UnitCount", strFileName, 1))
        AverageCount = CInt(ReadIniString(strSection, "AverageCount", strFileName, 1))
        TargetPatternValue = CDbl(ReadIniString(strSection, "TargetValue", strFileName, 0))
        Kp = CDbl(ReadIniString(strSection, "Kp", strFileName, 1))
        Ki = CDbl(ReadIniString(strSection, "Ki", strFileName, 0))
        'T = CDbl(ReadIniString(strSection, "T", strFileName, 20))
        Enabled = CInt(ReadIniString(strSection, "Enabled", strFileName, 0))
        Return True
    End Function

End Class

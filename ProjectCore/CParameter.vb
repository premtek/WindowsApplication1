Public Class CValveData
    ''' <summary>[閥件類型]</summary>
    ''' <remarks></remarks>
    Public ValveType(enmValve.Max) As enmValveType
    ''' <summary>[噴射閥類型]</summary>
    ''' <remarks></remarks>
    Public JetValve(enmValve.Max) As eValveModel
    ''' <summary>[Purge時間(Jet Valve)]</summary>
    ''' <remarks></remarks>
    Public PurgeTime(enmValve.Max) As Decimal
    ''' <summary>閥膠管膠量檢測致能</summary>
    ''' <remarks></remarks>
    Public EnableDetectPaste(enmValve.Max) As Boolean
    ''' <summary>[螺桿閥電流保護值]</summary>
    ''' <remarks></remarks>
    Public CTThreshold(enmValve.Max) As Decimal
    ''' <summary>Lucas建議增加的開機預壓參數</summary>
    ''' <remarks></remarks>
    Public PrePressure(enmValve.Max) As Decimal
    ''' <summary>[清膠每次的間隔]</summary>
    ''' <remarks></remarks>
    Public CleanPastePitch(enmValve.Max) As Decimal
    ''' <summary>[紀錄已經清膠幾次]</summary>
    ''' <remarks></remarks>
    Public CleanPasteNum(enmValve.Max) As Integer
    ''' <summary>[使用清膠的上限次數]</summary>
    ''' <remarks></remarks>
    Public CleanPasteNumLimit(enmValve.Max) As Integer
    ''' <summary>[清膠時Z軸移動的距離]</summary>
    ''' <remarks></remarks>
    Public CleanPasteDistanceZ(enmValve.Max) As Decimal
    ''' <summary>[清膠後出壓力之時間]</summary>
    ''' <remarks></remarks>
    Public CleanPastePressureTime(enmValve.Max) As Decimal
    ''' <summary>[清膠平台之工作長度]</summary>
    ''' <remarks></remarks>
    Public CleanPasteTableLength(enmValve.Max) As Decimal
    ''' <summary>[清膠擦拭移動距離Y移動距離 ]</summary>
    ''' <remarks></remarks>
    Public CleanPasteOffset(enmValve.Max) As Decimal
    ''' <summary>[清膠擦拭移動速度]</summary>
    ''' <remarks></remarks>
    Public CleanPasteSpeed(enmValve.Max) As Decimal
    ''' <summary>[清膠馬達旋轉方向]</summary>
    ''' <remarks></remarks>
    Public CleanPasteDir(enmValve.Max) As Boolean

    ''' <summary>儲存平台內所有閥</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub Save(ByVal fileName As String)
        Try
            With Me
                Dim strSection As String
                For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                    strSection = "Valve" & (mValveNo + 1).ToString
                    Call SaveIniString(strSection, "ValveData_ValveType" & (mValveNo + 1).ToString, .ValveType(mValveNo), fileName)
                    Call SaveIniString(strSection, "ValveData_JetValve" & (mValveNo + 1).ToString, .JetValve(mValveNo), fileName)
                    Call SaveIniString(strSection, "ValveData_PurgeTime" & (mValveNo + 1).ToString, .PurgeTime(mValveNo), fileName)
                    Call SaveIniString(strSection, "ValveData_EnableDetectPaste" & (mValveNo + 1).ToString, CInt(.EnableDetectPaste(mValveNo)), fileName)
                    Call SaveIniString(strSection, "ValveData_CTThreshold" & (mValveNo + 1).ToString, .CTThreshold(mValveNo), fileName)
                    Call SaveIniString(strSection, "ValveData_PrePressure" & (mValveNo + 1).ToString, .PrePressure(mValveNo), fileName)
                    Call SaveIniString(strSection, "ValveData_CleanPastePitch" & (mValveNo + 1).ToString, .CleanPastePitch(mValveNo), fileName)
                    Call SaveIniString(strSection, "ValveData_CleanPasteNum" & (mValveNo + 1).ToString, .CleanPasteNum(mValveNo), fileName)
                    Call SaveIniString(strSection, "ValveData_CleanPasteNumLimit" & (mValveNo + 1).ToString, .CleanPasteNumLimit(mValveNo), fileName)
                    Call SaveIniString(strSection, "ValveData_CleanPasteDistanceZ" & (mValveNo + 1).ToString, .CleanPasteDistanceZ(mValveNo), fileName)
                    Call SaveIniString(strSection, "ValveData_CleanPastePressureTime" & (mValveNo + 1).ToString, .CleanPastePressureTime(mValveNo), fileName)
                    Call SaveIniString(strSection, "ValveData_CleanPasteTableLength" & (mValveNo + 1).ToString, .CleanPasteTableLength(mValveNo), fileName)
                    Call SaveIniString(strSection, "ValveData_CleanPasteOffset" & (mValveNo + 1).ToString, .CleanPasteOffset(mValveNo), fileName)
                    Call SaveIniString(strSection, "ValveData_CleanPasteSpeed" & (mValveNo + 1).ToString, .CleanPasteSpeed(mValveNo), fileName)
                    Call SaveIniString(strSection, "ValveData_CleanPasteDir" & (mValveNo + 1).ToString, CInt(.CleanPasteDir(mValveNo)), fileName)
                Next
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002029), "Error_1002029", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002029) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub
    ''' <summary>讀取平台內所有閥</summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub Load(ByVal fileName As String)
        Try
            Dim strSection As String
            With Me
                For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                    strSection = "Valve" & (mValveNo + 1).ToString
                    .ValveType(mValveNo) = Val(ReadIniString(strSection, "ValveData_ValveType" & (mValveNo + 1).ToString, fileName, 0))
                    .JetValve(mValveNo) = Val(ReadIniString(strSection, "ValveData_JetValve" & (mValveNo + 1).ToString, fileName, 0))
                    .PurgeTime(mValveNo) = Val(ReadIniString(strSection, "ValveData_PurgeTime" & (mValveNo + 1).ToString, fileName, 0)) 'TODO:待確認是否放在CPurgeParameter
                    .EnableDetectPaste(mValveNo) = CBool(Val(ReadIniString(strSection, "ValveData_EnableDetectPaste" & (mValveNo + 1).ToString, fileName, 0)))
                    .CTThreshold(mValveNo) = Val(ReadIniString(strSection, "ValveData_CTThreshold" & (mValveNo + 1).ToString, fileName, 0))
                    .PrePressure(mValveNo) = Val(ReadIniString(strSection, "ValveData_PrePressure" & (mValveNo + 1).ToString, fileName, 0))
                    .CleanPastePitch(mValveNo) = Val(ReadIniString(strSection, "ValveData_JetValve" & (mValveNo + 1).ToString, fileName, 0))
                    .CleanPasteNum(mValveNo) = Val(ReadIniString(strSection, "ValveData_CleanPasteNum" & (mValveNo + 1).ToString, fileName, 0))
                    .CleanPasteNumLimit(mValveNo) = Val(ReadIniString(strSection, "ValveData_CleanPasteNumLimit" & (mValveNo + 1).ToString, fileName, 0))
                    .CleanPasteDistanceZ(mValveNo) = Val(ReadIniString(strSection, "ValveData_CleanPasteDistanceZ" & (mValveNo + 1).ToString, fileName, 0))
                    .CleanPastePressureTime(mValveNo) = Val(ReadIniString(strSection, "ValveData_CleanPastePressureTime" & (mValveNo + 1).ToString, fileName, 0))
                    .CleanPasteTableLength(mValveNo) = Val(ReadIniString(strSection, "ValveData_CleanPasteTableLength" & (mValveNo + 1).ToString, fileName, 0))
                    .CleanPasteOffset(mValveNo) = Val(ReadIniString(strSection, "ValveData_CleanPasteOffset" & (mValveNo + 1).ToString, fileName, 0))
                    .CleanPasteSpeed(mValveNo) = Val(ReadIniString(strSection, "ValveData_CleanPasteSpeed" & (mValveNo + 1).ToString, fileName, 0))
                    .CleanPasteDir(mValveNo) = CBool(Val(ReadIniString(strSection, "ValveData_CleanPasteDir" & (mValveNo + 1).ToString, fileName, 0)))
                Next
            End With

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002028), "Error_1002028", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002028) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub

End Class

''' <summary>
''' Lift Time
''' </summary>
''' <remarks></remarks>
Public Class CPasteLifeTime
    ''' <summary>[是否有提示過膠材計時壽命到期]</summary>
    ''' <remarks></remarks>
    Public LifeTimeAlarm(enmValve.Max) As Boolean
    ''' <summary>[是否有提示過膠材計數壽命到期]</summary>
    ''' <remarks></remarks>
    Public LifeCountAlarm(enmValve.Max) As Boolean
    ''' <summary>[Glue No1 剩下的時間]</summary>
    ''' <remarks></remarks>
    Public StartLifeTime(enmValve.Max) As Date
    ''' <summary>[Glue No1 噴了幾顆Chip]</summary>
    ''' <remarks></remarks>
    Public DotsCount(enmValve.Max) As Long
    ''' <summary>儲存膠材壽命</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal fileName As String) As Boolean
        Dim strSection As String
        With Me
            For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                strSection = "Valve" & (mValveNo + 1).ToString
                Call SaveIniString(strSection, "PasteLifeTime_StartLifeTime" & (mValveNo + 1).ToString, .StartLifeTime(mValveNo), fileName)
                Call SaveIniString(strSection, "PasteLifeTime_DotsCount" & (mValveNo + 1).ToString, .DotsCount(mValveNo), fileName)
            Next
        End With
        Return True
    End Function

    ''' <summary>讀取膠材壽命</summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal fileName As String) As Boolean
        Dim strSection As String
        With Me
            For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                strSection = "Valve" & (mValveNo + 1).ToString
                .StartLifeTime(mValveNo) = CDate(ReadIniString(strSection, "PasteLifeTime_StartLifeTime" & (mValveNo + 1).ToString, fileName, Now))
                .DotsCount(mValveNo) = CLng(ReadIniString(strSection, "PasteLifeTime_DotsCount" & (mValveNo + 1).ToString, fileName, 0))
            Next
        End With
        Return True
    End Function
End Class

''' <summary>[Purge、FlowRate....檢查的條件為何]</summary>
''' <remarks></remarks>
Public Class CInpectionCondition
    ''' <summary>[程式重啟的時候，Timer是否重新計數]</summary>
    ''' <remarks></remarks>
    Public IsReset(enmValve.Max) As Boolean
    ''' <summary>[紀錄此次開始時間(程式開啟就更新了)]</summary>
    ''' <remarks></remarks>
    Public StartTime(enmValve.Max) As Date
    ''' <summary>[紀錄上次離開時已經做了多久(程式關閉時必須更新)]</summary>
    ''' <remarks></remarks>
    Public LastTime(enmValve.Max) As Long
    ''' <summary>[盤數(已經做了幾盤了)]</summary>
    ''' <remarks></remarks>
    Public OnRuns(enmValve.Max) As Integer
    ''' <summary>時間(已經生產多久了)</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property OnTimer(ByVal valveNo As eValveWorkMode) As Long
        Get
            If IsReset(valveNo) = True Then
                Return DateDiff(DateInterval.Second, StartTime(valveNo), Now)
            Else
                Return LastTime(valveNo) + DateDiff(DateInterval.Minute, StartTime(valveNo), Now)
            End If
        End Get
    End Property

    ''' <summary>[存檔]</summary>
    ''' <param name="fileName"></param>
    ''' <param name="subName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal fileName As String, ByVal subName As String) As Boolean

        Dim strSection As String
        With Me
            For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                strSection = "Valve" & (mValveNo + 1).ToString
                Call SaveIniString(strSection, subName & "_IsReset" & (mValveNo + 1).ToString, .IsReset(mValveNo), fileName)
                Call SaveIniString(strSection, subName & "_StartTime" & (mValveNo + 1).ToString, .StartTime(mValveNo), fileName)
                Call SaveIniString(strSection, subName & "_LastTime" & (mValveNo + 1).ToString, .LastTime(mValveNo), fileName)
                Call SaveIniString(strSection, subName & "_OnRuns" & (mValveNo + 1).ToString, .OnRuns(mValveNo), fileName)
            Next
        End With
        Return True

    End Function
    ''' <summary>[讀檔]</summary>
    ''' <param name="fileName"></param>
    ''' <param name="subName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal fileName As String, ByVal subName As String) As Boolean
        Dim strSection As String
        With Me
            For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                strSection = "Valve" & (mValveNo + 1).ToString
                .IsReset(mValveNo) = CBool(ReadIniString(strSection, subName & "_IsReset" & (mValveNo + 1).ToString, fileName, 0))
                .LastTime(mValveNo) = CLng(ReadIniString(strSection, subName & "_LastTime" & (mValveNo + 1).ToString, fileName, 0))
                .OnRuns(mValveNo) = CInt(ReadIniString(strSection, subName & "_OnRuns" & (mValveNo + 1).ToString, fileName, 0))
            Next
        End With
        Return True
    End Function

    Public Sub ResetRuns()
        With Me
            For mValveNo As enmValve = enmValve.No1 To gSSystemParameter.StageUseValveCount - 1
                .OnRuns(mValveNo) = 0
            Next
        End With
    End Sub
End Class
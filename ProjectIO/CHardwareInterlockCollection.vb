Imports ProjectCore

''' <summary>
''' HardwardAlarm列表(DI)
''' </summary>
''' <remarks></remarks>
Public Enum enmHardwardAlarm
    ''' <summary>[CDA異常]</summary>
    ''' <remarks></remarks>
    CDA = 0
    ''' <summary>[EMO停止]</summary>
    ''' <remarks></remarks>
    EMO = 1
    ''' <summary>A機EMS異常</summary>
    ''' <remarks></remarks>
    EMS = 2
    ''' <summary>A機開門停機保護</summary>
    ''' <remarks></remarks>
    DoorClose = 3
    ''' <summary>A機馬達電力</summary>
    ''' <remarks></remarks>
    MC_Motor = 4
    ''' <summary>A機溫控器電源</summary>
    ''' <remarks></remarks>
    MC_Heater = 5
    ''' <summary>B機CDA異常</summary>
    ''' <remarks></remarks>
    CDA2 = 6
    ''' <summary>B機EMS異常</summary>
    ''' <remarks></remarks>
    EMS2 = 7
    ''' <summary>B機開門停機保護</summary>
    ''' <remarks></remarks>
    DoorClose2 = 8
    ''' <summary>B機馬達電力</summary>
    ''' <remarks></remarks>
    MC_Motor2 = 9
    ''' <summary>B機溫控器電源</summary>
    ''' <remarks></remarks>
    MC_Heater2 = 10
    Max = 10
End Enum

''' <summary>Interlock層級設定</summary>
''' <remarks></remarks>
Public Enum eInterlock
    ''' <summary>無</summary>
    ''' <remarks></remarks>
    None = 0
    ''' <summary>嚴重(Alarm)</summary>
    ''' <remarks></remarks>
    Alarm = 1
    ''' <summary>警告(Warn)</summary>
    ''' <remarks></remarks>
    Warn = 2
End Enum
''' <summary>硬體互鎖保護設定</summary>
''' <remarks></remarks>
Public Structure sHardwareInterlock
    ''' <summary>記錄硬體互鎖</summary>
    ''' <remarks></remarks>
    Public Status As Boolean
    ''' <summary>發生時的傳回狀態</summary>
    ''' <remarks></remarks>
    Public Level As eInterlock
End Structure


''' <summary>
''' 硬體互鎖集合
''' </summary>
''' <remarks></remarks>
Public Class CInterlockCollection

    ''' <summary>硬體互鎖保護設定清單</summary>
    ''' <remarks></remarks>
    Public Items(enmHardwardAlarm.Max) As sHardwareInterlock

    ''' <summary>硬體警報重置</summary>
    ''' <remarks></remarks>
    Public Sub ResetHardwardAlarm()
        For mAlarmNo = Items.GetLowerBound(0) To Items.GetUpperBound(0)
            Items(mAlarmNo).Status = False
        Next
    End Sub
    ''' <summary>[故障]硬體InterLock </summary>
    ''' <remarks></remarks>
    Public IsAlarm As Boolean = False

    ''' <summary>整機硬體保護</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetOverallHardwareInterlock() As eInterlock
        If enmHardwardAlarm.EMO >= 0 Then
            If Items(enmHardwardAlarm.EMO).Status = True Then
                Return Items(enmHardwardAlarm.EMO).Level
            End If
        End If
        Return eInterlock.None
    End Function

    ''' <summary>A機硬體保護</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMachineAHardwareInterlock() As eInterlock
        If enmHardwardAlarm.EMO >= 0 Then
            If Items(enmHardwardAlarm.EMO).Status = True Then 'PLCM(1611) = PLC EMO
                If Items(enmHardwardAlarm.CDA).Level = eInterlock.Alarm Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2000001", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000001)) 'CDA異常!!
                End If
                Return Items(enmHardwardAlarm.EMO).Level
            End If
        End If
        If enmHardwardAlarm.EMS >= 0 Then
            If Items(enmHardwardAlarm.EMS).Status = True Then
                Return Items(enmHardwardAlarm.EMS).Level
            End If
        End If
        If enmHardwardAlarm.CDA >= 0 Then
            If Items(enmHardwardAlarm.CDA).Status = True Then
                If Items(enmHardwardAlarm.CDA).Level = eInterlock.Alarm Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2000000", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000000)) 'CDA異常!!
                End If
                Return Items(enmHardwardAlarm.CDA).Level
            End If
        End If
        If enmHardwardAlarm.DoorClose >= 0 Then
            If Items(enmHardwardAlarm.DoorClose).Status = True Then
                Return Items(enmHardwardAlarm.DoorClose).Level
            End If
        End If
        If enmHardwardAlarm.MC_Motor >= 0 Then
            If Items(enmHardwardAlarm.MC_Motor).Status = True Then
                Return Items(enmHardwardAlarm.MC_Motor).Level
            End If
        End If
        If enmHardwardAlarm.MC_Heater >= 0 Then
            If Items(enmHardwardAlarm.MC_Heater).Status = True Then
                Return Items(enmHardwardAlarm.MC_Heater).Level
            End If
        End If
        Return eInterlock.None
    End Function

    ''' <summary>
    ''' B機硬體保護
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMachineBHardwareInterlock() As eInterlock
        If gSSystemParameter.MachineType <> enmMachineType.DCSW_800AQ Then
            Return eInterlock.None
        End If
        If enmHardwardAlarm.EMO >= 0 Then
            If Items(enmHardwardAlarm.EMO).Status = True Then 'PLCM(1611) = PLC EMO
                If Items(enmHardwardAlarm.CDA).Level = eInterlock.Alarm Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2000001", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000001)) 'CDA異常!!
                End If
                Return Items(enmHardwardAlarm.EMO).Level
            End If
        End If
        If enmHardwardAlarm.EMS2 >= 0 Then
            If Items(enmHardwardAlarm.EMS2).Status = True Then
                Return Items(enmHardwardAlarm.EMS2).Level
            End If
        End If
        If enmHardwardAlarm.CDA2 >= 0 Then
            If Items(enmHardwardAlarm.CDA2).Status = True Then
                If Items(enmHardwardAlarm.CDA).Level = eInterlock.Alarm Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2000000", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000000)) 'CDA異常!!
                End If
                Return Items(enmHardwardAlarm.CDA2).Level
            End If
        End If
        If enmHardwardAlarm.DoorClose2 >= 0 Then
            If Items(enmHardwardAlarm.DoorClose2).Status = True Then
                Return Items(enmHardwardAlarm.DoorClose2).Level
            End If
        End If
        If enmHardwardAlarm.MC_Motor2 >= 0 Then
            If Items(enmHardwardAlarm.MC_Motor2).Status = True Then
                Return Items(enmHardwardAlarm.MC_Motor2).Level
            End If
        End If
        If enmHardwardAlarm.MC_Heater2 >= 0 Then
            If Items(enmHardwardAlarm.MC_Heater2).Status = True Then
                Return Items(enmHardwardAlarm.MC_Heater2).Level
            End If
        End If

        Return eInterlock.None
    End Function
    ''' <summary>儲存設定檔</summary>
    ''' <param name="fileName">設定檔名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal fileName As String) As Boolean
        Dim strSection As String = "Hardware-Interlock"
        For mItemNo As Integer = 0 To Items.Count - 1
            Call SaveIniString(strSection, "Interlock" & (mItemNo + 1).ToString, Items(mItemNo).Level, fileName)
        Next
        Return True
    End Function
    ''' <summary>讀取設定檔</summary>
    ''' <param name="fileName">設定檔名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal fileName As String) As Boolean
        Dim strSection As String = "Hardware-Interlock"
        For mItemNo As Integer = 0 To Items.Count - 1
            Items(mItemNo).Level = CInt(ReadIniString(strSection, "Interlock" & (mItemNo + 1).ToString, fileName, CInt(eInterlock.Alarm)))
        Next
        Return True
    End Function
End Class

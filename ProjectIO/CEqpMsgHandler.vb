Imports ProjectIO
Imports System.Windows.Forms
Imports ProjectCore
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' 錯誤訊息歷史紀錄
''' </summary>
''' <remarks></remarks>
Public Structure sAlarmHistory
    ''' <summary>[發生時間]</summary>
    ''' <remarks></remarks>
    Public DateTime As Date
    ''' <summary>[發生函式]</summary>
    ''' <remarks></remarks>
    Public FunctionName As String
    ''' <summary>[發生步驟]</summary>
    ''' <remarks></remarks>
    Public SysNum As String
    ''' <summary>[錯誤代碼]</summary>
    ''' <remarks></remarks>
    Public ALID As String
    ''' <summary>[錯誤訊息]</summary>
    ''' <remarks></remarks>
    Public AlarmString As String
End Structure

''' <summary>
''' 設備訊息處理
''' </summary>
''' <remarks></remarks>
Public Class CEqpMsgHandler

    ''' <summary>最新狀態(以最嚴重為主)</summary>
    ''' <remarks></remarks>
    Public MsgLevel As eMessageLevel

    ''' <summary>[錯誤訊息列表]</summary>
    ''' <remarks></remarks>
    Public AlarmList As New List(Of sAlarmHistory)

    Public Event OnAlarmCanPause(ByRef sender As Object, ByVal e As EventArgs)

    ''' <summary>Alarm顯示在DataGridView中,如有相同代碼將不顯示</summary>
    ''' <param name="funcName"></param>
    ''' <param name="alid"></param>
    ''' <param name="msgLevel"></param>
    ''' <param name="sysNum"></param>
    ''' <remarks></remarks>
    Public Sub Add(ByVal funcName As String, ByVal alid As Integer, msgLevel As eMessageLevel, Optional sysNum As String = "0000")
        AddHistoryAlarm(alid.ToString(), funcName, sysNum, gMsgHandler.GetMessage(alid), msgLevel)
    End Sub

    ''' <summary>訊息顯示與記錄</summary>
    ''' <param name="strAlarmCode">ALID</param>
    ''' <param name="LoopName">發生函式名稱</param>
    ''' <param name="Sys_Num">系統步驟編號</param>
    ''' <param name="strLog">要記錄的Log</param>
    ''' <param name="msgLevel">訊息等級</param>
    ''' <remarks></remarks>
    Public Sub AddHistoryAlarm(ByVal strAlarmCode As String, ByVal LoopName As String, Optional ByVal Sys_Num As String = "0000", Optional ByVal strLog As String = "", Optional ByVal msgLevel As eMessageLevel = eMessageLevel.Alarm)

        Dim mIsAlarmExist As Boolean = False
        Dim tmp As sAlarmHistory
        Dim mAlamID As String

        If strAlarmCode.Contains("_") Then
            mAlamID = strAlarmCode.Split("_")(1)
        Else
            mAlamID = strAlarmCode
        End If


        '[說明]:同一異常不要一直顯示
        '[說明]:先判斷有無相同的Alarm
        If AlarmList.Count > 0 Then
            For Each p As sAlarmHistory In AlarmList
                '[Note]:檢查List 有無相同 AlarmCode
                If p.FunctionName = LoopName And p.SysNum = Sys_Num And p.ALID = mAlamID And p.AlarmString = strLog Then
                    mIsAlarmExist = True
                    Exit For
                End If
            Next
        Else
            mIsAlarmExist = False
        End If

        If mIsAlarmExist = False Then
            Select Case msgLevel 'Soni + 2016.09.09 訊息等級(for燈號..etc)
                Case eMessageLevel.Error
                    Me.MsgLevel = eMessageLevel.Error '最高異警

                Case eMessageLevel.Alarm
                    If Me.MsgLevel = eMessageLevel.Error Then
                    Else
                        Me.MsgLevel = eMessageLevel.Alarm '次高異警
                    End If

                Case eMessageLevel.Warning
                    If Me.MsgLevel = eMessageLevel.Error Then
                    ElseIf Me.MsgLevel = eMessageLevel.Alarm Then
                    Else
                        Me.MsgLevel = eMessageLevel.Warning
                    End If

                Case eMessageLevel.Information '訊息提示不呈現
                    Me.MsgLevel = eMessageLevel.Information
                Case eMessageLevel.Running
                    Me.MsgLevel = eMessageLevel.Running

                Case eMessageLevel.Idle
                    Me.MsgLevel = eMessageLevel.Idle

            End Select

            '[Note]:記錄跳出的訊息
            gSyslog.Save(strLog, strAlarmCode, msgLevel)

            '[Note]:加入Alarm Code
            tmp.DateTime = Format(Now, "yyyy/MM/dd hh:mm:ss")
            tmp.FunctionName = LoopName
            tmp.SysNum = Sys_Num
            tmp.ALID = mAlamID
            tmp.AlarmString = strLog
            AlarmList.Add(tmp)
        End If

        '[說明]:只要一有Alarm，就必須要能暫停，終止流程
        RaiseEvent OnAlarmCanPause(Me, Nothing)

ErrorHandler:
        If Err.Number <> 0 Then  ' No error. Do nothing.
            MessageBox.Show(Err.GetException.StackTrace, "AddHistroyAlarm", MessageBoxButtons.OK)
            Err.Clear()         '清除錯誤資訊
            Exit Sub            '離開Sub(跳出函式)
        End If
    End Sub

    ''' <summary>
    ''' 顯示警告訊息
    ''' </summary>
    ''' <param name="cboAlarmMessage"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ShowAlarm(ByRef cboAlarmMessage As ComboBox) As Boolean

        Dim sAlarm As sAlarmHistory
        Dim strGetString As String

        '[說明]:只要有新的Alarm進來就更新
        If AlarmList.Count > 0 Then
            If cboAlarmMessage.Items.Count = AlarmList.Count Then
                'If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Running And gSYS(eSys.OverAll).ExecuteCommand = eSysCommand.Home Then
                '    AlarmList.Clear()
                '    cboAlarmMessage.Items.Clear()
                '    InvokeComboBox(cboAlarmMessage, "")
                'End If
            Else
                cboAlarmMessage.Items.Clear()
                For i = 0 To AlarmList.Count - 1
                    sAlarm = AlarmList.Item(i)
                    strGetString = " [" & sAlarm.FunctionName & "] " & " [" & sAlarm.SysNum & "] " & " [" & sAlarm.ALID & "] " & " [" & sAlarm.AlarmString & "] "   '加入Alarm Code
                    cboAlarmMessage.Items.Add(strGetString)
                    cboAlarmMessage.Text = strGetString
                Next
                RaiseEvent OnAlarmCanPause(Me, Nothing)
            End If
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 清除警告訊息重複比對表
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ClearAlarmCmpTable(ByRef cboAlarmMessage As ComboBox) As Boolean
        AlarmList.Clear()
        cboAlarmMessage.Items.Clear()
        InvokeComboBox(cboAlarmMessage, "")
        Me.MsgLevel = eMessageLevel.Idle
        Return True
    End Function

End Class

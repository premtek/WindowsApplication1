Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectAOI
Imports ProjectLaserInterferometer
Imports ProjectTriggerBoard
Imports System.Threading
'Imports Premtek.Base

'20170920
Public Class frmValvePm
    Public sys As sSysParam
    Dim mTriggerCmdFailCount(enmStage.Max) As Integer                               '[紀錄資料傳輸異常次數]
    Dim mStopWatch As New Stopwatch
    Dim bTimeOut As Boolean = False
    Dim mPurgeTime(enmStage.Max) As Double                                          '實際出膠時間

    Private mContextUI As System.Threading.SynchronizationContext = Nothing 'Eason 20170413 Ticket:100110 , Use SynchronizationContext replace DoEvent Refresh UI 
    Private mTaskTokenSource As CancellationTokenSource = New CancellationTokenSource()
    Private mTaskToken As CancellationToken
    Private mRunLongProcessTask As Task(Of Integer) = Nothing

    ''' <summary>計算作業派工執行緒</summary>
    ''' <remarks></remarks>
    Dim mThreadStart As New System.Threading.ThreadStart(AddressOf Dispatch)
    ''' <summary>作業派工執行緒</summary>
    ''' <remarks></remarks>
    Dim mThread As New System.Threading.Thread(mThreadStart)


    ''' <summary>派工作業</summary>
    ''' <remarks></remarks>
    Sub Dispatch()
        Do
            If btnValvePmTrigger.BackColor = Color.Blue Then
                mStopWatch.Restart()
                Do
                    bTimeOut = IsTimeOut(mStopWatch, mPurgeTime(sys.StageNo))
                    If btnValvePmTrigger.BackColor = SystemColors.Control Then
                        Exit Do
                    End If
                Loop While (bTimeOut = False)
                bTimeOut = False

                '[說明]:關膠  
                Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eOff)
                Call gDOCollection.RefreshDO()  '寫入卡片 DO點
                btnValvePmTrigger.BackColor = SystemColors.Control
            End If

            System.Threading.Thread.Sleep(5)
        Loop
    End Sub


    Private Sub btnPower_Click(sender As Object, e As EventArgs) Handles btnValvePmPower.Click
        Dim mStopWatch As New Stopwatch
        Dim bPowerOnAlarm As Boolean = False
        Dim bCheckPowerOnAlarm As Boolean = False
        Dim bTimeOut As Boolean = False
        Dim PowerStatus As Boolean

        '[說明]:檢查狀態
        If btnValvePmPower.BackColor = Color.Blue Then
            PowerStatus = False
        ElseIf btnValvePmPower.BackColor = SystemColors.Control Then
            PowerStatus = True
        End If

        '[說明]:TriggerBoardResetAlarm
        If TriggerBoardResetAlarm() = False Then
            Exit Sub
        End If

        '[說明]:SetPower On/Off 
        mStopWatch.Restart()
        Do
            If gTriggerBoard.SetValvePower(sys.StageNo, sys.SelectValve, PowerStatus) = True Then
                bPowerOnAlarm = True
            End If
            '[說明]:TimeOut時間
            Do
                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                If bTimeOut = True Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                    End Select
                    Exit Sub
                End If
            Loop While (bTimeOut = True)
        Loop While (bPowerOnAlarm = False)

        bPowerOnAlarm = False

        '[說明]:Check SetPower On/Off 
        Do
            If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                '[Note]:還在接收傳送資料中
                If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'Reset Alarm
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetValvePower(sys.StageNo, sys.SelectValve, PowerStatus) = True Then
                                bPowerOnAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bPowerOnAlarm = False)
                        bPowerOnAlarm = False
                    End If
                End If
            Else

                '   gTriggerBoard.mTemperature.Status = True
                '[Note]:檢查接收資料
                If gTriggerBoard.Parameter(sys.StageNo).Status = True Then
                    mTriggerCmdFailCount(sys.StageNo) = 0
                    bCheckPowerOnAlarm = True
                Else
                    '[Note]:查看收到的內容是?
                    Debug.Print("SetValvePower: " & gTriggerBoard.Parameter(sys.StageNo).STR)
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'Reset Alarm
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetValvePower(sys.StageNo, sys.SelectValve, PowerStatus) = True Then
                                bPowerOnAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm Power", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bPowerOnAlarm = False)
                        bPowerOnAlarm = False
                    End If
                End If
            End If
        Loop While (bCheckPowerOnAlarm = False)

        '[說明]:塞入狀態
        If PowerStatus = False Then
            btnValvePmPower.BackColor = SystemColors.Control
        ElseIf PowerStatus = True Then
            btnValvePmPower.BackColor = Color.Blue
        End If
    End Sub

    Private Sub btnValvePmSetTemperature_Click(sender As Object, e As EventArgs) Handles btnValvePmSetTemperature.Click
        Dim mStopWatch As New Stopwatch
        Dim bSetTemperatureAlarm As Boolean = False
        Dim bCheckSetTemperatureAlarm As Boolean = False
        Dim bTimeOut As Boolean = False


        '[說明]:TriggerBoardResetAlarm
        If TriggerBoardResetAlarm() = False Then
            Exit Sub
        End If

        ''[說明]:SetTemperature
        mStopWatch.Restart()
        Do
            If gTriggerBoard.SetTempture(sys.StageNo, sys.SelectValve, txtValvePmTemperature.Text) = True Then
                bSetTemperatureAlarm = True
            End If
            '[說明]:TimeOut時間
            Do
                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                If bTimeOut = True Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                    End Select
                    Exit Sub
                End If
            Loop While (bTimeOut = True)
        Loop While (bSetTemperatureAlarm = False)

        bSetTemperatureAlarm = False

        '[說明]:Check Temperature 
        Do
            If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                '[Note]:還在接收傳送資料中
                If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'Reset Alarm
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetTempture(sys.StageNo, sys.SelectValve, txtValvePmTemperature.Text) = True Then
                                bSetTemperatureAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bSetTemperatureAlarm = False)
                        bSetTemperatureAlarm = False
                    End If
                End If
            Else
                '[Note]:檢查接收資料
                If gTriggerBoard.Temperature(sys.StageNo).Status = True Then
                    mTriggerCmdFailCount(sys.StageNo) = 0
                    bCheckSetTemperatureAlarm = True
                Else
                    '[Note]:查看收到的內容是?
                    Debug.Print("SetTemperature: " & gTriggerBoard.Temperature(sys.StageNo).STR)
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'Reset Alarm
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetTempture(sys.StageNo, sys.SelectValve, txtValvePmTemperature.Text) = True Then
                                bSetTemperatureAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm Temperature", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bSetTemperatureAlarm = False)
                        bSetTemperatureAlarm = False
                    End If
                End If
            End If
        Loop While (bCheckSetTemperatureAlarm = False)

    End Sub

    Private Sub btnPurge_Click(sender As Object, e As EventArgs) Handles btnValvePmPurge.Click
        Dim mStopWatch As New Stopwatch
        Dim bPurgeOnAlarm As Boolean = False
        Dim bTimeOut As Boolean = False
        Dim bCheckPurgeOnAlarm As Boolean = False

        Dim PurgeStatus As Boolean

        '[說明]:檢查狀態
        If btnValvePmPurge.BackColor = Color.Blue Then
            PurgeStatus = False
        ElseIf btnValvePmPurge.BackColor = SystemColors.Control Then
            PurgeStatus = True
        End If


        '[說明]:TriggerBoardResetAlarm
        If TriggerBoardResetAlarm() = False Then
            Exit Sub
        End If

        '[說明]:Purge On/Off
        mStopWatch.Restart()
        Do
            If gTriggerBoard.SetPurge(sys.StageNo, sys.SelectValve, PurgeStatus) = True Then
                bPurgeOnAlarm = True
            End If
            '[說明]:TimeOut時間
            Do
                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                If bTimeOut = True Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                    End Select
                    Exit Sub
                End If
            Loop While (bTimeOut = True)
        Loop While (bPurgeOnAlarm = False)

        bPurgeOnAlarm = False

        '[說明]:Check Purge On/Off Status
        Do
            If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                '[Note]:還在接收傳送資料中
                If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetPurge(sys.StageNo, sys.SelectValve, PurgeStatus) = True Then
                                bPurgeOnAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bPurgeOnAlarm = False)
                        bPurgeOnAlarm = False
                    End If
                End If
            Else
                ''[Note]:檢查接收資料  
                If gTriggerBoard.Parameter(sys.StageNo).Status Then
                    mTriggerCmdFailCount(sys.StageNo) = 0
                    bCheckPurgeOnAlarm = True
                Else
                    '[Note]:查看收到的內容是?
                    Debug.Print("ValvePurge: " & gTriggerBoard.Parameter(sys.StageNo).Status)
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetPurge(sys.StageNo, sys.SelectValve, PurgeStatus) = True Then
                                bPurgeOnAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Purge", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bPurgeOnAlarm = False)
                        bPurgeOnAlarm = False
                    End If
                End If
            End If
        Loop While (bCheckPurgeOnAlarm = False)

        '[說明]:塞入狀態
        If PurgeStatus = False Then
            btnValvePmPurge.BackColor = SystemColors.Control
        ElseIf PurgeStatus = True Then
            btnValvePmPurge.BackColor = Color.Blue
        End If

    End Sub

    Private Sub btnHeater_Click(sender As Object, e As EventArgs) Handles btnValvePmHeater.Click
        Dim mStopWatch As New Stopwatch
        Dim bCheckHeaterOnAlarm As Boolean = False
        Dim bTimeOut As Boolean = False
        Dim bHeaterOnAlarm As Boolean = False
        Dim TemperatureStatus As Boolean

        ''[說明]:檢查狀態
        If btnValvePmHeater.BackColor = Color.Blue Then
            TemperatureStatus = False
        ElseIf btnValvePmHeater.BackColor = SystemColors.Control Then
            TemperatureStatus = True
        End If


        '[說明]:TriggerBoardResetAlarm
        If TriggerBoardResetAlarm() = False Then
            Exit Sub
        End If
        '[說明]:Heater On/Off
        mStopWatch.Restart()
        Do
            If gTriggerBoard.SetTemptureOnOff(sys.StageNo, sys.SelectValve, TemperatureStatus) = True Then
                bHeaterOnAlarm = True
            End If
            '[說明]:TimeOut時間
            Do
                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                If bTimeOut = True Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                    End Select
                    Exit Sub
                End If
            Loop While (bTimeOut = True)
        Loop While (bHeaterOnAlarm = False)

        bHeaterOnAlarm = False

        '[說明]:Check Heater On/Off Status
        Do
            If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                '[Note]:還在接收傳送資料中
                If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetTemptureOnOff(sys.StageNo, sys.SelectValve, TemperatureStatus) = True Then
                                bHeaterOnAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bHeaterOnAlarm = False)
                        bHeaterOnAlarm = False
                    End If
                End If
            Else

                ''[Note]:檢查接收資料  CycleRecipe
                If gTriggerBoard.Parameter(sys.StageNo).Status Then
                    mTriggerCmdFailCount(sys.StageNo) = 0
                    bCheckHeaterOnAlarm = True
                Else
                    '[Note]:查看收到的內容是????
                    Debug.Print("ValveHeater: " & gTriggerBoard.Temperature(sys.StageNo).STR)
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'SetCycleRecipe
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetTemptureOnOff(sys.StageNo, sys.SelectValve, TemperatureStatus) = True Then
                                bHeaterOnAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Heater", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bHeaterOnAlarm = False)
                        bHeaterOnAlarm = False
                    End If
                End If
            End If
        Loop While (bCheckHeaterOnAlarm = False)


        ''[說明]:塞入狀態
        If TemperatureStatus = False Then
            btnValvePmHeater.BackColor = SystemColors.Control
        ElseIf TemperatureStatus = True Then
            btnValvePmHeater.BackColor = Color.Blue
        End If

    End Sub

    Private Sub btnTrigger_Click(sender As Object, e As EventArgs) Handles btnValvePmTrigger.Click
        Dim mIsOn As Boolean

        '[說明]:Check Trigger 狀態
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.eDTS_2S2V
                Select Case sys.StageNo
                    Case enmStage.No1
                        Select Case sys.SelectValve
                            Case eValveWorkMode.Valve1
                                mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger1)
                            Case eValveWorkMode.Valve2
                                '[Note]:目前沒有此I/O點位規劃
                            Case Else
                        End Select
                    Case enmStage.No2
                        Select Case sys.SelectValve
                            Case eValveWorkMode.Valve1
                                mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger2)
                            Case eValveWorkMode.Valve2
                                '[Note]:目前沒有此I/O點位規劃
                            Case Else
                        End Select
                End Select
            Case enmMachineType.DCSW_800AQ
                Select Case sys.StageNo
                    Case enmStage.No1
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger1)
                    Case enmStage.No2
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger2)
                    Case enmStage.No3
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger3)
                    Case enmStage.No4
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger4)
                End Select
            Case enmMachineType.DCS_500AD
                Select Case sys.StageNo
                    Case enmStage.No1
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger1)
                    Case enmStage.No2
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger2)
                End Select
            Case Else
                Select Case sys.SelectValve
                    Case eValveWorkMode.Valve1
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger1)

                    Case eValveWorkMode.Valve2
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger1)
                    Case Else
                End Select
        End Select

        If mIsOn = True Then
            Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eOff)
            Call gDOCollection.RefreshDO()  '寫入卡片 DO點
            btnValvePmTrigger.BackColor = SystemColors.Control
            Exit Sub
        ElseIf mIsOn = False Then
            '繼續執行點膠動作
        End If

        '=======================================================點膠命令=================================================================================
        '觸發板用資料
        Dim mCyleParam As sTriggerTPCmdParam
        Dim mPosB(enmStage.Max) As Decimal

        Dim bCheckCycleRecipeAlarm As Boolean = False
        Dim bCycleRecipeAlarm As Boolean = False

        Dim bCheckDispenseRunAlarm As Boolean = False
        Dim bDispenseRunAlarm As Boolean = False


        mPurgeTime(sys.StageNo) = 0

        '[說明]:關閉膠閥  
        Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eOff)

        '[說明]:開啟膠桶壓力
        gSysAdapter.SetSyringePressure(sys.StageNo, sys.SelectValve, enmONOFF.eON)

        Call gDOCollection.RefreshDO()  '寫入卡片 DO點

        mStopWatch.Restart()
        Do
            bTimeOut = IsTimeOut(mStopWatch, 100)
        Loop While (bTimeOut = False)

        If sys.AxisB <> -1 Then
            '[說明]:設定出膠壓力
            'gEPVCollection.SetValue(sys.StageNo, sys.SelectValve, eEPVPressureType.Syringe, gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decAirPressure(sys.SelectValve, mPosB(sys.StageNo)), False)
            gEPVCollection.SetValue(sys.StageNo, sys.SelectValve, eEPVPressureType.Syringe, gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decAirPressure(sys.SelectValve, mPosB(sys.StageNo)), False)
        Else
            '[說明]:設定出膠壓力
            'gEPVCollection.SetValue(sys.StageNo, sys.SelectValve, eEPVPressureType.Syringe, gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decAirPressure(sys.SelectValve, 0), False)
            gEPVCollection.SetValue(sys.StageNo, sys.SelectValve, eEPVPressureType.Syringe, txtValvePmPressure.Text, False)
        End If

        '[說明]:TriggerBoardResetAlarm
        If TriggerBoardResetAlarm() = False Then
            Exit Sub
        End If

        '[說明]:設定Jetting Valve Trigger Controller 為固定頻率打點模式
        mCyleParam.DotCounts = txtValvePmPointNumber.Text
        mCyleParam.JetPressure = 0
        mCyleParam.GluePressure = 0
        Select Case gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).ValveModel
            Case eValveModel.Advanjet
                mPurgeTime(sys.StageNo) = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).Advanjet.CycleTime * txtValvePmPointNumber.Text) + 500
                mCyleParam.CycleTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).Advanjet.CycleTime * 1000)
                mCyleParam.PulseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).Advanjet.PulseTime * 1000)
            Case eValveModel.PicoPulse
                mPurgeTime(sys.StageNo) = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CycleTime * txtValvePmPointNumber.Text) + 500
                mCyleParam.CycleTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CycleTime * 1000)
                mCyleParam.PulseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.ValveOnTime * 1000)
                mCyleParam.OpenTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.OpenTime * 1000)
                mCyleParam.CloseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CloseTime * 1000)
                mCyleParam.Stroke = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.Stroke
                mCyleParam.CloseVoltage = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CloseVoltage
        End Select

        'SetCycleRecipe(T Command)
        mStopWatch.Restart()
        Do
            If gTriggerBoard.SetCycleRecipe(sys.StageNo, mCyleParam, False) = True Then
                bCycleRecipeAlarm = True
            End If
            '[說明]:TimeOut時間
            Do
                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                If bTimeOut = True Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                    End Select
                    Exit Sub
                End If
            Loop While (bTimeOut = True)
        Loop While (bCycleRecipeAlarm = False)

        bCycleRecipeAlarm = False

        'Check CycleRecipe
        Do
            If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                '[Note]:還在接收傳送資料中
                If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'SetCycleRecipe
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetCycleRecipe(sys.StageNo, mCyleParam, False) = True Then
                                bCycleRecipeAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bCycleRecipeAlarm = False)
                        bCycleRecipeAlarm = False
                    End If
                End If
            Else
                ''[Note]:檢查接收資料  CycleRecipe
                If gTriggerBoard.CycleRecipe(sys.StageNo).Status Then
                    mTriggerCmdFailCount(sys.StageNo) = 0
                    bCheckCycleRecipeAlarm = True
                Else
                    '[Note]:查看收到的內容是????
                    Debug.Print("CycleRecipe(T Cmd): " & gTriggerBoard.CycleRecipe(sys.StageNo).STR)
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'SetCycleRecipe
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetCycleRecipe(sys.StageNo, mCyleParam, False) = True Then
                                bCycleRecipeAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Trigger", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bCycleRecipeAlarm = False)
                        bCycleRecipeAlarm = False
                    End If
                End If
            End If
        Loop While (bCheckCycleRecipeAlarm = False)

        'SetDispenseRun (X Command)
        mStopWatch.Restart()
        Do
            If gTriggerBoard.SetDispenseRun(sys.StageNo, enmTriggerDispType.CycleRecipe, sys.SelectValve, 0, 0, False) = True Then
                bDispenseRunAlarm = True
            End If
            '[說明]:TimeOut時間
            Do
                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                If bTimeOut = True Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                    End Select
                    Exit Sub
                End If
            Loop While (bTimeOut = True)
        Loop While (bDispenseRunAlarm = False)

        bDispenseRunAlarm = False

        'Check SetDispenseRun
        Do
            '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
            If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                '[Note]:還在接收傳送資料中
                If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'SetCycleRecipe
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetDispenseRun(sys.StageNo, enmTriggerDispType.CycleRecipe, sys.SelectValve, 0, 0, False) = True Then
                                bDispenseRunAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bDispenseRunAlarm = False)
                        bDispenseRunAlarm = False
                    End If
                End If
            Else
                ''[Note]:檢查接收資料
                If gTriggerBoard.DispenseRun(sys.StageNo).Status Then
                    mTriggerCmdFailCount(sys.StageNo) = 0
                    bCheckDispenseRunAlarm = True
                Else
                    '[Note]:查看收到的內容是????
                    Debug.Print("DispenseRun(X Cmd): " & gTriggerBoard.DispenseRun(sys.StageNo).STR)
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'SetCycleRecipe
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetDispenseRun(sys.StageNo, enmTriggerDispType.CycleRecipe, sys.SelectValve, 0, 0, False) = True Then
                                bDispenseRunAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm DispenseRun", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bDispenseRunAlarm = False)
                        bDispenseRunAlarm = False
                    End If
                End If
            End If
        Loop While (bCheckDispenseRunAlarm = False)

        '[說明]:出膠  
        Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eON)
        Call gDOCollection.RefreshDO()  '寫入卡片 DO點
        btnValvePmTrigger.BackColor = Color.Blue

        '  gSSystemParameter.StageParts.ValveData(sys.StageNo).JetValve(sys.SelectValve)
        '[說明]:顯示點膠資料
        '  Select Case gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).ValveModel
        Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).JetValve(sys.SelectValve)
            Case eValveModel.Advanjet
                labPulseTime.Visible = True
                txtValvePmPulseTime.Visible = True
                labPulseTimeUnit.Visible = True

                labCycleTime.Visible = True
                txtValvePmCycleTime.Visible = True
                labCycleTimeUnit.Visible = True

                labCloseVolt.Visible = False
                txtValvePmCloseVolt.Visible = False
                labCloseVoltUnit.Visible = False

                labStroke.Visible = False
                txtValvePmStroke.Visible = False
                labStrokeUnit.Visible = False

                labOpenTime.Visible = False
                txtValvePmOpenTime.Visible = False
                labOpenTimeUnit.Visible = False

                labCloseTime.Visible = False
                txtValvePmCloseTime.Visible = False
                labCloseTimeUnit.Visible = False

                txtValvePmCycleTime.Text = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).Advanjet.CycleTime
                txtValvePmPulseTime.Text = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).Advanjet.PulseTime
            Case eValveModel.PicoPulse
                labPulseTime.Visible = True
                txtValvePmPulseTime.Visible = True
                labPulseTimeUnit.Visible = True

                labCycleTime.Visible = True
                txtValvePmCycleTime.Visible = True
                labCycleTimeUnit.Visible = True

                labCloseVolt.Visible = True
                txtValvePmCloseVolt.Visible = True
                labCloseVoltUnit.Visible = True

                labStroke.Visible = True
                txtValvePmStroke.Visible = True
                labStrokeUnit.Visible = True

                labOpenTime.Visible = True
                txtValvePmOpenTime.Visible = True
                labOpenTimeUnit.Visible = True

                labCloseTime.Visible = True
                txtValvePmCloseTime.Visible = True
                labCloseTimeUnit.Visible = True

                txtValvePmPulseTime.Text = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.ValveOnTime
                txtValvePmCycleTime.Text = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CycleTime
                txtValvePmCloseVolt.Text = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CloseTime
                txtValvePmStroke.Text = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.Stroke
                txtValvePmOpenTime.Text = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.OpenTime
                txtValvePmCloseTime.Text = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CloseTime
        End Select

    End Sub

    Public Function TriggerBoardResetAlarm() As Boolean
        Dim bResetAlarm As Boolean = False
        Dim bTimeOut As Boolean = False
        Dim bCheckResetAlarm As Boolean = False

        Dim mStopWatch As New Stopwatch

        '[說明]:SetResetAlarm
        mStopWatch.Restart()
        Do
            If gTriggerBoard.SetResetAlarm(sys.StageNo, False) = True Then
                bResetAlarm = True
            End If
            '[說明]:TimeOut時間
            Do
                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                If bTimeOut = True Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                    End Select
                    Return False
                    Exit Function
                End If
            Loop While (bTimeOut = True)
        Loop While (bResetAlarm = False)

        bResetAlarm = False

        '[說明]:Check ResetAlarm Status
        Do
            If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                '[Note]:還在接收傳送資料中
                If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Return False
                        Exit Function
                    Else
                        'Reset Alarm
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetResetAlarm(sys.StageNo, False) = True Then
                                bResetAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                    End Select
                                    Return False
                                    Exit Function
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bResetAlarm = False)
                    End If
                End If
            Else
                '[Note]:檢查接收資料
                If gTriggerBoard.ResetAlarm(sys.StageNo).Status = True Then
                    mTriggerCmdFailCount(sys.StageNo) = 0
                    bCheckResetAlarm = True
                Else
                    '[Note]:查看收到的內容是????
                    Debug.Print("ResetAlarm: " & gTriggerBoard.ResetAlarm(sys.StageNo).STR)
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Return False
                        Exit Function
                    Else
                        'Reset Alarm
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetResetAlarm(sys.StageNo, False) = True Then
                                bResetAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm ResetAlarm", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                    End Select
                                    Return False
                                    Exit Function
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bResetAlarm = False)

                    End If
                End If
            End If
        Loop While (bCheckResetAlarm = False)

        Return True
    End Function

    Private Sub frmValvePm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim mIsOn As Boolean

        '測試用
        'mThread.SetApartmentState(Threading.ApartmentState.STA)
        'mThread.Start()

        'Exit Sub

        '[說明]:Check Pressure On/Off 狀態
        Select Case sys.StageNo
            Case 0
                Select Case sys.SelectValve
                    Case eValveWorkMode.Valve1
                        Select Case gDOCollection.GetState(enmDO.SyringePressure1)
                            Case True
                                btnValvePmSetSyringe1OnOff.Text = GetString("On")
                            Case False
                                btnValvePmSetSyringe1OnOff.Text = GetString("Off")
                        End Select
                    Case eValveWorkMode.Valve2
                        Select Case gDOCollection.GetState(enmDO.SyringePressure2)
                            Case True
                                btnValvePmSetSyringe1OnOff.Text = GetString("On")
                            Case False
                                btnValvePmSetSyringe1OnOff.Text = GetString("Off")
                        End Select
                End Select

            Case 1
                Select Case gDOCollection.GetState(enmDO.SyringePressure2)
                    Case True
                        btnValvePmSetSyringe1OnOff.Text = GetString("On")
                    Case False
                        btnValvePmSetSyringe1OnOff.Text = GetString("Off")
                End Select
            Case 2
                Select Case gDOCollection.GetState(enmDO.SyringePressure3)
                    Case True
                        btnValvePmSetSyringe1OnOff.Text = GetString("On")
                    Case False
                        btnValvePmSetSyringe1OnOff.Text = GetString("Off")
                End Select
            Case 3
                Select Case gDOCollection.GetState(enmDO.SyringePressure4)
                    Case True
                        btnValvePmSetSyringe1OnOff.Text = GetString("On")
                    Case False
                        btnValvePmSetSyringe1OnOff.Text = GetString("Off")
                End Select
        End Select
        '=====================================================================================================================

        '[說明]:Check Trigger 狀態
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.eDTS_2S2V
                Select Case sys.StageNo
                    Case enmStage.No1
                        Select Case sys.SelectValve
                            Case eValveWorkMode.Valve1
                                mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger1)
                            Case eValveWorkMode.Valve2
                                '[Note]:目前沒有此I/O點位規劃
                            Case Else
                        End Select
                    Case enmStage.No2
                        Select Case sys.SelectValve
                            Case eValveWorkMode.Valve1
                                mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger2)
                            Case eValveWorkMode.Valve2
                                '[Note]:目前沒有此I/O點位規劃
                            Case Else
                        End Select
                End Select
            Case enmMachineType.DCSW_800AQ, enmMachineType.DCS_500AD
                Select Case sys.StageNo
                    Case enmStage.No1
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger1)
                    Case enmStage.No2
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger2)
                    Case enmStage.No3
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger3)
                    Case enmStage.No4
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger4)
                End Select
            Case Else
                Select Case sys.SelectValve
                    Case eValveWorkMode.Valve1
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger1)

                    Case eValveWorkMode.Valve2
                        mIsOn = gDOCollection.GetState(enmDO.DispensingTrigger1)
                    Case Else
                End Select
        End Select

        Select Case mIsOn
            Case True
                btnValvePmTrigger.BackColor = Color.Blue
            Case False
                btnValvePmTrigger.BackColor = SystemColors.Control
        End Select
        '=====================================================================================================================
        '--- Joytick 配置 ---
        With UcJoyStick1
            .AxisX = sys.AxisX
            .AxisY = sys.AxisY
            .AxisZ = sys.AxisZ
            .AXisA = sys.AxisA
            .AXisB = sys.AxisB
            .AXisC = sys.AxisC
        End With
        UcJoyStick1.SetSpeedType(SpeedType.Slow)
        UcJoyStick1.RefreshPosition()

        If (gSSystemParameter.StageCount > 1) Then
            If (gSSystemParameter.MachineSafeData.Count > 0) Then
                UcJoyStick1.InverseAxisX.SafeDistance = gSSystemParameter.MachineSafeData(sys.MachineNo).SafeDistanceX
                UcJoyStick1.InverseAxisX.Spread = gSSystemParameter.MachineSafeData(sys.MachineNo).SpreadX

                If (sys.StageNo = enmStage.No1) Then
                    UcJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage2).AxisX    '對立軸
                    UcJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Posivtive
                ElseIf (sys.StageNo = enmStage.No2) Then
                    UcJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage1).AxisX    '對立軸
                    UcJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Negative
                ElseIf (sys.StageNo = enmStage.No3) Then
                    UcJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage4).AxisX    '對立軸
                    UcJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Posivtive
                ElseIf (sys.StageNo = enmStage.No4) Then
                    UcJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage3).AxisX    '對立軸
                    UcJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Negative
                End If
            End If
        End If
        '=====================================================================================================================
        Dim bGetSwitchAlarm As Boolean
        Dim bCheckGetSwitchAlarm As Boolean

        '[說明]:判斷按鍵狀態
        Dim tempArray As String = ""
        '[說明]:TriggerBoardResetAlarm
        If TriggerBoardResetAlarm() = False Then
            Exit Sub
        End If


        '[說明]:GetSwitch 
        mStopWatch.Restart()
        Do
            If gTriggerBoard.GetSwitch(sys.StageNo, False) = True Then
                bGetSwitchAlarm = True
            End If
            '[說明]:TimeOut時間
            Do
                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                If bTimeOut = True Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1016003", "ValvePm Switch", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1016103", "ValvePm Switch", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1016203", "ValvePm Switch", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1016303", "ValvePm Switch", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                    End Select
                    Exit Sub
                End If
            Loop While (bTimeOut = True)
        Loop While (bGetSwitchAlarm = False)

        bGetSwitchAlarm = False


        '[說明]:Check GetSwitch On/Off 
        Do
            If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                '[Note]:還在接收傳送資料中
                If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'Reset Alarm
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.GetSwitch(sys.StageNo, False) = True Then
                                bGetSwitchAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bGetSwitchAlarm = False)
                        bGetSwitchAlarm = False
                    End If
                End If
            Else
                '[Note]:檢查接收資料
                If gTriggerBoard.Temperature(sys.StageNo).Status = True Then
                    mTriggerCmdFailCount(sys.StageNo) = 0
                    bCheckGetSwitchAlarm = True
                Else
                    '[Note]:查看收到的內容是?
                    Debug.Print("SetGetSwitch: " & gTriggerBoard.Parameter(sys.StageNo).STR)
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'Reset Alarm
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.GetSwitch(sys.StageNo, False) = True Then
                                bGetSwitchAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016001", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016101", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016201", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016301", "ValvePm GetSwitch", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bGetSwitchAlarm = False)
                        bGetSwitchAlarm = False
                    End If
                End If
            End If
        Loop While (bCheckGetSwitchAlarm = False)

        '   gTriggerBoard.Temperature(sys.StageNo).Value
        '  gTriggerBoard.Temperature

        Select Case sys.SelectValve
            Case eValveWorkMode.Valve1
                If gTriggerBoard.Temperature(sys.StageNo).Value.Split(",")(1) = 1 Then
                    btnValvePmPower.BackColor = Color.Blue
                ElseIf gTriggerBoard.Temperature(sys.StageNo).Value.Split(",")(1) = 0 Then
                    btnValvePmPower.BackColor = SystemColors.Control
                End If
                If gTriggerBoard.Temperature(sys.StageNo).Value.Split(",")(3) = 1 Then
                    btnValvePmHeater.BackColor = Color.Blue
                ElseIf gTriggerBoard.Temperature(sys.StageNo).Value.Split(",")(3) = 0 Then
                    btnValvePmHeater.BackColor = SystemColors.Control
                End If
                If gTriggerBoard.Temperature(sys.StageNo).Value.Split(",")(5) = 1 Then
                    btnValvePmPurge.BackColor = Color.Blue
                ElseIf gTriggerBoard.Temperature(sys.StageNo).Value.Split(",")(5) = 0 Then
                    btnValvePmPurge.BackColor = SystemColors.Control
                End If
            Case eValveWorkMode.Valve2
                If gTriggerBoard.Temperature(sys.StageNo).Value.Split(",")(2) = 1 Then
                    btnValvePmPower.BackColor = Color.Blue
                ElseIf gTriggerBoard.Temperature(sys.StageNo).Value.Split(",")(2) = 0 Then
                    btnValvePmPower.BackColor = SystemColors.Control
                End If
                If gTriggerBoard.Temperature(sys.StageNo).Value.Split(",")(4) = 1 Then
                    btnValvePmHeater.BackColor = Color.Blue
                ElseIf gTriggerBoard.Temperature(sys.StageNo).Value.Split(",")(4) = 0 Then
                    btnValvePmHeater.BackColor = SystemColors.Control
                End If
                If gTriggerBoard.Temperature(sys.StageNo).Value.Split(",")(6) = 1 Then
                    btnValvePmPurge.BackColor = Color.Blue
                ElseIf gTriggerBoard.Temperature(sys.StageNo).Value.Split(",")(6) = 0 Then
                    btnValvePmPurge.BackColor = SystemColors.Control
                End If
        End Select
        '=====================================================================================================================

        mThread.SetApartmentState(Threading.ApartmentState.STA)
        mThread.Name = "frmValvePm"
        mThread.Start()

    End Sub

    Private Sub btnValvePmValveParameter_Click(sender As Object, e As EventArgs) Handles btnValvePmValveParameter.Click
        Dim frmShow As New Form

        Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).JetValve(sys.SelectValve)
            Case eValveModel.PicoPulse
                'Pico
                '-----------------------------------------------
                Dim ucValveParameterPicoShow As New ucValveParameterPico(frmShow)
                With ucValveParameterPicoShow
                    .Dock = DockStyle.Fill
                    .gSys = sys
                End With
                '-----------------------------------------------
                '   Dim nPoint As Point = New Point(1080, 460)
                ' 580, 540
                With frmShow
                    .Width = 1025
                    .Height = 620 '525 = 原始大小 + 邊寬30
                    .MinimizeBox = False
                    .MaximizeBox = False
                    .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                    .Text = "Valve Parameter!!!"
                    .StartPosition = FormStartPosition.Manual
                    '  .Location = nPoint
                    .Controls.Add(ucValveParameterPicoShow)
                    .ShowDialog()
                End With
                '-----------------------------------------------
            Case eValveModel.Advanjet
                '-----------------------------------------------
                'Advanjet
                Dim ucValveParameterAdvanjetShow As New ucValveParameterAdvanjet(frmShow)
                With ucValveParameterAdvanjetShow
                    .Dock = DockStyle.Fill
                    .gSys = sys
                End With
                '-----------------------------------------------
                '   Dim nPoint As Point = New Point(1080, 460)
                ' 580, 540
                With frmShow
                    .Width = 900
                    .Height = 500 '525 = 原始大小 + 邊寬30
                    .MinimizeBox = False
                    .MaximizeBox = False
                    .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                    .Text = "Valve Parameter!!!"
                    .StartPosition = FormStartPosition.Manual
                    '  .Location = nPoint
                    .Controls.Add(ucValveParameterAdvanjetShow)
                    .ShowDialog()
                End With
                '-----------------------------------------------
        End Select
    End Sub

    Private Sub btnValvePmSetAirPressure_Click(sender As Object, e As EventArgs) Handles btnValvePmSetPressure.Click
        gSyslog.Save("[frmValvePm]" & vbTab & "[btnValvePmSetAirPressure]" & vbTab & "Click")

        Select Case sys.StageNo
            Case 0
                Select Case sys.SelectValve
                    Case eValveWorkMode.Valve1
                        gEPVCollection.SetValue(eEPVPressureType.Syringe, enmEPV.No1, Val(txtValvePmPressure.Text), True)
                    Case eValveWorkMode.Valve2
                        gEPVCollection.SetValue(eEPVPressureType.Syringe, enmEPV.No2, Val(txtValvePmPressure.Text), True)
                End Select
            Case 1
                gEPVCollection.SetValue(eEPVPressureType.Syringe, enmEPV.No2, Val(txtValvePmPressure.Text), True)
            Case 2
                gEPVCollection.SetValue(eEPVPressureType.Syringe, enmEPV.No3, Val(txtValvePmPressure.Text), True)
            Case 3
                gEPVCollection.SetValue(eEPVPressureType.Syringe, enmEPV.No4, Val(txtValvePmPressure.Text), True)
        End Select
    End Sub

    Private Sub btnValvePmSetSyringe1OnOff_Click(sender As Object, e As EventArgs) Handles btnValvePmSetSyringe1OnOff.Click
        gSyslog.Save("[frmValvePm]" & vbTab & "[btnValvePmSetSyringe1OnOff]" & vbTab & "Click")
        Select Case sys.StageNo
            Case 0
                Select Case sys.SelectValve
                    Case eValveWorkMode.Valve1
                        gDOCollection.SetState(enmDO.SyringePressure1, Not gDOCollection.GetState(enmDO.SyringePressure1))
                        Select Case gDOCollection.GetState(enmDO.SyringePressure1)
                            Case True
                                btnValvePmSetSyringe1OnOff.Text = GetString("On")
                            Case False
                                btnValvePmSetSyringe1OnOff.Text = GetString("Off")
                        End Select
                    Case eValveWorkMode.Valve2
                        gDOCollection.SetState(enmDO.SyringePressure2, Not gDOCollection.GetState(enmDO.SyringePressure2))
                        Select Case gDOCollection.GetState(enmDO.SyringePressure2)
                            Case True
                                btnValvePmSetSyringe1OnOff.Text = GetString("On")
                            Case False
                                btnValvePmSetSyringe1OnOff.Text = GetString("Off")
                        End Select
                End Select

            Case 1
                gDOCollection.SetState(enmDO.SyringePressure2, Not gDOCollection.GetState(enmDO.SyringePressure2))
                Select Case gDOCollection.GetState(enmDO.SyringePressure2)
                    Case True
                        btnValvePmSetSyringe1OnOff.Text = GetString("On")
                    Case False
                        btnValvePmSetSyringe1OnOff.Text = GetString("Off")
                End Select
            Case 2
                gDOCollection.SetState(enmDO.SyringePressure3, Not gDOCollection.GetState(enmDO.SyringePressure3))
                Select Case gDOCollection.GetState(enmDO.SyringePressure3)
                    Case True
                        btnValvePmSetSyringe1OnOff.Text = GetString("On")
                    Case False
                        btnValvePmSetSyringe1OnOff.Text = GetString("Off")
                End Select
            Case 3
                gDOCollection.SetState(enmDO.SyringePressure4, Not gDOCollection.GetState(enmDO.SyringePressure4))
                Select Case gDOCollection.GetState(enmDO.SyringePressure4)
                    Case True
                        btnValvePmSetSyringe1OnOff.Text = GetString("On")
                    Case False
                        btnValvePmSetSyringe1OnOff.Text = GetString("Off")
                End Select
        End Select
    End Sub

    Public Function GetString(ByVal value As String) As String
        Select Case value
            Case "Syringe Pressure On"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Syringe Pressure On"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "注射器压力开"
                    Case enmLanguageType.eTraditionalChinese
                        Return "膠管氣壓開"
                End Select
            Case "Syringe Pressure Off"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Syringe Pressure Off"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "注射器压力关"
                    Case enmLanguageType.eTraditionalChinese
                        Return "膠管氣壓關"
                End Select
            Case "On"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "On"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "开"
                    Case enmLanguageType.eTraditionalChinese
                        Return "開"
                End Select
            Case "Off"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Off"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "关"
                    Case enmLanguageType.eTraditionalChinese
                        Return "關"
                End Select
        End Select
        Return "Undefined."
    End Function

    Private Sub frmValvePm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        '[說明]:關閉執行續
        If (mThread.IsAlive) Then
            mThread.Abort()
        End If

        UcJoyStick1.ManualDispose()
        Me.Dispose(True)

        '[說明]:如有Recipe DB ReLoad回來
        If gCRecipe.strName <> "" Then
            Dim DefaultDirectory As String = Application.StartupPath & "\Recipe\"
            DefaultDirectory = Application.StartupPath & "\Recipe\" & gCRecipe.strName
            'Recipe讀檔
            gCRecipe.LoadStageParts(DefaultDirectory) '檔案讀取
        End If

        GC.Collect()

    End Sub

    Private Sub txtValvePmPointNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtValvePmPointNumber.KeyPress, txtValvePmTemperature.KeyPress, txtValvePmPressure.KeyPress
        Premtek.Base.CKeyPress.CheckUDecimal(sender, e)
    End Sub


End Class
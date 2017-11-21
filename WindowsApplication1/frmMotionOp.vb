Imports ProjectCore
Imports ProjectMotion
Imports ProjectIO

'Soni / 2016.08.30由ucjoyStick控制項移植功能自此
Public Class frmMotionOp
    Public myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMotionOp))
    Public DelayTimer As New CDelayTimer
    Dim pos1Loop As New frmMotionLoopConfig
    Dim pos2Loop As New frmMotionLoopConfig
    
    Dim frmAxisAdvance As New frmMotionOption
    ''' <summary>簡易IO顯示</summary>
    ''' <remarks></remarks>
    Dim frmIoStatus As New frmIOStatus

    Dim CmdQueue As New Queue(Of CommandType)

    ' '' <summary>派工執行緒</summary>
    ' ''' <remarks></remarks>
    'Dim mThreadStart As New System.Threading.ThreadStart(AddressOf Dispatch)
    ' ''' <summary>計算作業派工執行緒</summary>
    ' ''' <remarks></remarks>
    'Dim mThread As New System.Threading.Thread(mThreadStart)
    Dim mIsDisposing As Boolean

    ''' <summary>執行緒事件通知</summary>
    ''' <remarks></remarks>
    Dim mAutoWait As New System.Threading.AutoResetEvent(False)

    ''' <summary>速度形式</summary>
    ''' <remarks></remarks>
    Dim mSpeedType As SpeedType
    ''' <summary>速度形式設定</summary>
    ''' <param name="axisIndex"></param>
    ''' <param name="speed"></param>
    ''' <remarks></remarks>
    Public Sub SetSpeedType(ByVal axisIndex As Integer, ByVal speed As SpeedType)
        Select Case speed
            Case SpeedType.Fast
                btnSpeed.BackgroundImage = My.Resources.SpeedHigh
                'btnSpeed.Text = "Fast"
                btnSpeed.BackColor = Color.Red
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetVelHigh(axisIndex, 100)
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetAcc(axisIndex, 100)
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetDec(axisIndex, 100)
                gCMotion.WaitCmdStatus(axisIndex)

            Case SpeedType.Medium
                btnSpeed.BackgroundImage = My.Resources.SpeedMid
                'btnSpeed.Text = "Midden"
                btnSpeed.BackColor = Color.Yellow
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetVelHigh(axisIndex, 10)
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetAcc(axisIndex, 10)
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetDec(axisIndex, 10)
                gCMotion.WaitCmdStatus(axisIndex)
            Case SpeedType.Slow
                btnSpeed.BackgroundImage = My.Resources.SpeedLow
                btnSpeed.BackColor = Color.Green
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetVelHigh(axisIndex, 1)
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetAcc(axisIndex, 1)
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetDec(axisIndex, 1)
                gCMotion.WaitCmdStatus(axisIndex)

        End Select
    End Sub

    ''' <summary>是否計時可持續運轉</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsTimerCanRun() As Boolean
        If gCMotion.AxisParameter(AxisNo).CardParameter.CardType = enmMotionCardType.PCI_1245 Or gCMotion.AxisParameter(AxisNo).CardParameter.CardType = enmMotionCardType.PCI_1285 Then
            Return True
        End If
        Return False
    End Function

#Region "實際動作"
    Sub Left_MouseDown()
        gSyslog.Save("[ucJoyStick]" & vbTab & "[btnLeft]" & vbTab & "MouseDown")
        If IsTimerCanRun() Then
            SetSpeedType(AxisNo, mSpeedType)
            tmrMotorMenu.Start()
        End If

        '[說明]:按下去移動，放掉就停止
        Dim dir As eDirection '移動方向
        Dim mAxis As Integer

        mAxis = AxisNo

        If gCMotion.AxisParameter(mAxis).Parameter.ButtonDirection = eDirection.Negative Then
            dir = eDirection.Positive
        Else
            dir = eDirection.Negative
        End If

        gSyslog.Save("Click btnRight@UcJoyStick(Jog" & mSpeedType.ToString() & ")")
        'If gCMotion.VelMove(mAxis, dir) = False Then
        '    gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnLeft", , gMsgHandler.GetMessage(Error_1009006))
        'End If

        '20170712_往軟體極限絕對移動
        If dir = eDirection.Positive Then
            If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit) = False Then
                gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Left_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
            End If
        ElseIf dir = eDirection.Negative Then
            If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.NegativeLimit) = False Then
                gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Left_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
            End If
        End If

        btnLeft.BackColor = Color.Yellow '按鍵顏色

        Select Case gCMotion.AxisParameter(mAxis).CardParameter.CardType
            Case enmMotionCardType.ModBus
                tmrMotorMenu.Start()
        End Select
    End Sub

    Sub Right_MouseDown()
        gSyslog.Save("[ucJoyStick]" & vbTab & "[btnRight]" & vbTab & "MouseDown")
        If IsTimerCanRun() Then
            SetSpeedType(AxisNo, mSpeedType)
            tmrMotorMenu.Start()
        End If
        '[說明]:按下去移動，放掉就停止
        Dim dir As eDirection
        Dim mAxis As Integer

        mAxis = AxisNo

        If gCMotion.AxisParameter(mAxis).Parameter.ButtonDirection = eDirection.Negative Then
            dir = eDirection.Negative
        Else
            dir = eDirection.Positive
        End If

        gSyslog.Save("Click btnRight@UcJoyStick(Jog" & mSpeedType.ToString() & ")")
        'If gCMotion.VelMove(mAxis, dir) = False Then
        '    gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnRight", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
        'End If

        '20170712_往軟體極限絕對移動
        If dir = eDirection.Positive Then
            If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit) = False Then
                gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Right_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
            End If
        ElseIf dir = eDirection.Negative Then
            If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.NegativeLimit) = False Then
                gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Right_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
            End If
        End If


        btnRight.BackColor = Color.Yellow '按鍵顏色
        Select Case gCMotion.AxisParameter(mAxis).CardParameter.CardType
            Case enmMotionCardType.ModBus
                tmrMotorMenu.Start()
        End Select
    End Sub

    Sub Down_MouseDown()
        gSyslog.Save("[ucJoyStick]" & vbTab & "[btnDown]" & vbTab & "MouseDown")
        If IsTimerCanRun() Then
            SetSpeedType(AxisNo, mSpeedType)
            tmrMotorMenu.Start()
        End If
        '[說明]:按下去移動，放掉就停止
        Dim dir As eDirection
        Dim mAxis As Integer
        mAxis = AxisNo

        If gCMotion.AxisParameter(mAxis).Parameter.ButtonDirection = eDirection.Negative Then
            dir = eDirection.Negative
        Else
            dir = eDirection.Positive
        End If

        gSyslog.Save("Click btnDown@UcJoyStick(Jog" & mSpeedType.ToString() & ")")
        'If gCMotion.VelMove(mAxis, dir) = False Then
        '    gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
        'End If

        '20170712_往軟體極限絕對移動
        If dir = eDirection.Positive Then
            If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit) = False Then
                gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
            End If
        ElseIf dir = eDirection.Negative Then
            If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.NegativeLimit) = False Then
                gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
            End If
        End If

        btnDown.BackColor = Color.Yellow '按鍵顏色
        Select Case gCMotion.AxisParameter(mAxis).CardParameter.CardType
            Case enmMotionCardType.ModBus
                tmrMotorMenu.Start()
        End Select
    End Sub

    Sub Up_MouseDown()
        gSyslog.Save("[ucJoyStick]" & vbTab & "[btnUp]" & vbTab & "MouseDown")
        If IsTimerCanRun() Then
            SetSpeedType(AxisNo, mSpeedType)
            tmrMotorMenu.Start()
        End If
        '[說明]:按下去移動，放掉就停止
        Dim dir As eDirection '移動方向
        Dim mAxis As Integer
        mAxis = AxisNo
        If gCMotion.AxisParameter(mAxis).Parameter.ButtonDirection = eDirection.Negative Then
            dir = eDirection.Positive
        Else
            dir = eDirection.Negative
        End If

        gSyslog.Save("Click btnUp@UcJoyStick(Jog" & mSpeedType.ToString() & ")")
        'If gCMotion.VelMove(mAxis, dir) = False Then
        '    gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnUp", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
        'End If

        '20170712_往軟體極限絕對移動
        If dir = eDirection.Positive Then
            If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit) = False Then
                gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
            End If
        ElseIf dir = eDirection.Negative Then
            If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.NegativeLimit) = False Then
                gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
            End If
        End If

        btnUp.BackColor = Color.Yellow '按鍵顏色
        Select Case gCMotion.AxisParameter(mAxis).CardParameter.CardType
            Case enmMotionCardType.ModBus
                tmrMotorMenu.Start()
        End Select
    End Sub
    Sub Up_MouseUp()
        BtnMouseUp(AxisNo, btnUp, lblAxisPosition)
    End Sub

    Sub Down_MouseUp()
        BtnMouseUp(AxisNo, btnDown, lblAxisPosition)
    End Sub

    Sub Right_MouseUp()
        BtnMouseUp(AxisNo, btnRight, lblAxisPosition)
    End Sub

    Sub Left_MouseUp()
        BtnMouseUp(AxisNo, btnLeft, lblAxisPosition)
    End Sub


    Sub BtnMouseUp(ByVal axisIndex As Integer, sender As Object, ByRef lblAxisPosition As Label)

        Dim mDec As Double = 5000
        '[說明]:按下去移動，放掉就停止
        Select Case mSpeedType
            Case SpeedType.Fast
                mDec = 5000
            Case SpeedType.Medium
                mDec = 2500
            Case SpeedType.Slow
                mDec = 100
        End Select


        Call gCMotion.GetMotionState(axisIndex)
        Debug.Print("Status: " + gCMotion.AxisParameter(axisIndex).AxisMotionStatus.ToString)
        '若是大於1024&小於2048 及已在減速段
        If gCMotion.AxisParameter(axisIndex).AxisMotionStatus >= 1024 And gCMotion.AxisParameter(axisIndex).AxisMotionStatus < 2048 Then
            Debug.Print("已在減速段..............")
        Else
            Debug.Print("減速停止")
            gCMotion.SlowStop(axisIndex, mDec)
        End If

        gSyslog.Save(gCMotion.AxisParameter(axisIndex).AxisName & " SlowStop(" & mDec & "mm/s^2)UcJoyStick")
        'Debug.Print("SlowStop 3000000")
        If gCMotion.AxisParameter(axisIndex).CardParameter.CardType = enmMotionCardType.ModBus Then 'ModBus放掉就停止
            tmrMotorMenu.Stop()
        End If

        '確保命令發完
        gCMotion.WaitCmdStatus(axisIndex)
        System.Threading.Thread.Sleep(100) '等100ms

        gCMotion.GetPositionValue(axisIndex)
        gCMotion.WaitCmdStatus(axisIndex)
        gCMotion.GetPositionValue(axisIndex)
        gCMotion.WaitCmdStatus(axisIndex)

        InvokeLabel(lblAxisPosition, gCMotion.GetPositionValue(axisIndex)) '更新位置

        CType(sender, Button).BackColor = SystemColors.Control '按鍵顏色
        CType(sender, Button).UseVisualStyleBackColor = True
    End Sub

#End Region
    Sub Dispatch()
        Do
            If CmdQueue.Count > 0 Then '如果有待處理影像
                Dim mCmd As CommandType = CmdQueue.Dequeue
                Select Case mCmd

                    Case CommandType.DownDown
                        Down_MouseDown()
                    Case CommandType.DownUp
                        Down_MouseUp()
                    Case CommandType.LeftDown
                        Left_MouseDown()
                    Case CommandType.LeftUp
                        Left_MouseUp()
                    Case CommandType.RightDown
                        Right_MouseDown()
                    Case CommandType.RightUp
                        Right_MouseUp()
                    Case CommandType.UpDown
                        Up_MouseDown()
                    Case CommandType.UpUp
                        Up_MouseUp()
                End Select
            Else
                mAutoWait.WaitOne()
            End If

            If mIsDisposing Then
                Exit Sub
            End If
        Loop
    End Sub

    ''' <summary>接收外部Axis</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AxisNo As Integer

    Private int_TaskX As Integer = 0

    Private Sub tmrMotorMenu_Tick(sender As Object, e As EventArgs) Handles tmrMotorMenu.Tick

        
        If btnHome.Enabled = False Then '復歸中,只看HomeFinish
            If gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed Then
                If gCMotion.HomeFinish(AxisNo) = CommandStatus.Sucessed Then
                    Select Case gCMotion.AxisParameter(AxisNo).CardParameter.CardType
                        Case enmMotionCardType.ModBus
                            '確保命令發完
                            System.Threading.Thread.Sleep(100) '等100ms
                            gCMotion.GetPositionValue(AxisNo)
                            Do
                                'Application.DoEvents()
                            Loop Until gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed
                            lblAxisPosition.Text = gCMotion.GetPositionValue(AxisNo) '更新位置
                            Do
                                'Application.DoEvents()
                            Loop Until gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed
                            tmrMotorMenu.Stop()

                    End Select
                    If gCMotion.SetNELEnable(AxisNo, True) <> CommandStatus.Sucessed Then
                        MsgBox("To Be Continued.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        btnHome.Enabled = True
                        Exit Sub
                    End If
                    If gCMotion.SetPELEnable(AxisNo, True) <> CommandStatus.Sucessed Then
                        MsgBox("To Be Continued.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        btnHome.Enabled = True
                        Exit Sub
                    End If
                    btnHome.Enabled = True

                End If
            End If

        Else
            If chkLoop.Checked Then '復歸完成,兩點來回
                Select Case int_TaskX
                    Case 0
                        If gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed Then
                            int_TaskX += 1
                        End If

                    Case 1 '更改Acc、Dec、Speed
                        If gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed Then
                            gCMotion.SetVelHigh(AxisNo, pos1Loop.nmuVelHigh.Value)
                            int_TaskX += 1
                        End If

                    Case 2
                        If gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed Then
                            gCMotion.SetAcc(AxisNo, pos1Loop.nmuAcceleration.Value)
                            int_TaskX += 1
                        End If

                    Case 3
                        If gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed Then
                            gCMotion.SetDec(AxisNo, pos1Loop.nmuDeleration.Value)
                            int_TaskX += 1
                        End If

                    Case 4   'X Go Start Pos
                        If gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed Then
                            Select Case gCMotion.AbsMove(AxisNo, nmcTargetPos1.Text.Trim)
                                Case CommandStatus.Sucessed

                                    int_TaskX += 1
                                Case CommandStatus.Alarm
                                    gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Timer1 CheckBoxX", int_TaskX, gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                            End Select
                        End If
                    Case 5
                        If gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed Then
                            If gCMotion.MotionDone(AxisNo) = CommandStatus.Sucessed Then
                                int_TaskX += 1
                            End If
                        End If
                    Case 6   'Delay
                        If DelayTimer.MillisecondDelay(pos1Loop.nmuStableTime.Value) = 1 Then
                            int_TaskX += 1
                        End If

                    Case 7   'X Go End Pos
                        If gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed Then
                            gCMotion.SetVelHigh(AxisNo, pos2Loop.nmuVelHigh.Value)
                            int_TaskX += 1
                        End If
                    Case 8
                        If gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed Then
                            gCMotion.SetAcc(AxisNo, pos2Loop.nmuAcceleration.Value)
                            int_TaskX += 1
                        End If

                    Case 9
                        If gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed Then
                            gCMotion.SetDec(AxisNo, pos2Loop.nmuDeleration.Value)
                            int_TaskX += 1
                        End If

                    Case 10
                        If gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed Then
                            Select Case gCMotion.AbsMove(AxisNo, nmcTargetPos2.Text.Trim)
                                Case CommandStatus.Sucessed
                                    int_TaskX += 1
                                Case CommandStatus.Alarm
                                    gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Timer1 CheckBoxX", int_TaskX, gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                            End Select
                        End If

                    Case 11  'Move Finish
                        If gCMotion.GetCmdStatus(AxisNo) = CommandStatus.Sucessed Then
                            If gCMotion.MotionDone(AxisNo) = CommandStatus.Sucessed Then
                                int_TaskX += 1
                            End If
                        End If

                    Case 12   'Delay
                        If DelayTimer.MillisecondDelay(pos2Loop.nmuStableTime.Value) = 1 Then
                            int_TaskX += 1
                        End If

                    Case 13
                        int_TaskX = 1

                End Select
            Else '復歸完成,單軸移動
                Static flag As Boolean
                If flag Then
                    lblAxisPosition.Text = gCMotion.GetPositionValue(AxisNo)
                    flag = False
                Else
                    flag = True
                    If gCMotion.MotionDone(AxisNo) = CommandStatus.Sucessed Then
                        btnGo1.BackColor = SystemColors.Control
                        btnGo2.BackColor = SystemColors.Control
                    End If
                End If

                int_TaskX = 0
            End If
        End If


    End Sub

    Private Sub frmMotionOp_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        btnConfig1.BackColor = SystemColors.Control
        pos1Loop.Hide()
        btnConfig2.BackColor = SystemColors.Control
        pos2Loop.Hide()
        btnAdvance.BackColor = SystemColors.Control
        Select Case gCMotion.AxisParameter(AxisNo).CardParameter.CardType
            Case enmMotionCardType.ModBus
                tmrMotorMenu.Stop()
            Case enmMotionCardType.PCI_1245, enmMotionCardType.PCI_1285
                tmrMotorMenu.Start()
        End Select

    End Sub

    Private Sub frmMotionOp_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        mIsDisposing = True
        mAutoWait.Set()
        DelayTimer = Nothing
        pos1Loop.Dispose()
        pos2Loop.Dispose()
        frmAxisAdvance.Dispose()
        frmIoStatus.Dispose()
        CmdQueue.Clear()
        'mThread.Abort()
        mAutoWait.Dispose()
        'mThreadStart = Nothing
        Me.Dispose(True) 'Soni / 2017.01.10 記憶體累積
    End Sub

    Private Sub frmMotorMenu_Load(sender As Object, e As EventArgs) Handles Me.Load
        If AxisNo > gCMotion.AxisParameter.Count - 1 Then
            Me.Close()
            gSyslog.Save(gMsgHandler.GetMessage(Error_1000003), "Error_1000003", eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1000003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Select Case gCMotion.AxisParameter(AxisNo).CardParameter.CardType
            Case enmMotionCardType.PCI_1245, enmMotionCardType.PCI_1285
                tmrMotorMenu.Start()
            Case enmMotionCardType.ModBus
                tmrMotorMenu.Stop()
        End Select
        chkLoop.Checked = False '進入時強制關閉loop動作
        lblAxisName.Text = gCMotion.AxisParameter(AxisNo).AxisName

        btnHome.Enabled = True

        If gCMotion.MotionDone(AxisNo) = CommandStatus.Sucessed Then
            btnGo1.BackColor = SystemColors.Control '按鍵顏色
            btnGo2.BackColor = SystemColors.Control '按鍵顏色
        End If

        gCMotion.WaitCmdStatus(AxisNo) 'Soni / 2017.05.10
        mSpeedType = SpeedType.Slow
        'btnSpeed.Text = "Slow"
        btnSpeed.BackColor = Color.Green
        SetSpeedType(AxisNo, mSpeedType)
        Select Case gCMotion.AxisParameter(AxisNo).CardParameter.CardType
            Case enmMotionCardType.ModBus
                tmrMotorMenu.Interval = 500
                btnServo.Visible = False
            Case enmMotionCardType.PCI_1245, enmMotionCardType.PCI_1285
                tmrMotorMenu.Interval = 100
                btnServo.Visible = True
        End Select
        Select Case gCMotion.AxisParameter(AxisNo).Coordinate
            Case CoordinateType.Coordinate1X
                btnUp.Enabled = False
                btnDown.Enabled = False
                btnLeft.Enabled = True
                btnRight.Enabled = True
            Case CoordinateType.Coordinate1Y
                btnUp.Enabled = True
                btnDown.Enabled = True
                btnLeft.Enabled = False
                btnRight.Enabled = False
            Case CoordinateType.Coordinate1Z
                btnUp.Enabled = True
                btnDown.Enabled = True
                btnLeft.Enabled = False
                btnRight.Enabled = False
            Case CoordinateType.Coordinate1A
                btnUp.Enabled = True
                btnDown.Enabled = True
                btnLeft.Enabled = False
                btnRight.Enabled = False
            Case CoordinateType.Coordinate1B
                btnUp.Enabled = False
                btnDown.Enabled = False
                btnLeft.Enabled = True
                btnRight.Enabled = True
            Case CoordinateType.Coordinate1C
                btnUp.Enabled = False
                btnDown.Enabled = False
                btnLeft.Enabled = True
                btnRight.Enabled = True
        End Select

        'System.Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf Dispatch))
        'Eason 20170221 Ticket:100033 , Memory Free Part4 [S]
        Task.Run(Sub()
                     Dispatch()
                 End Sub)
        'Eason 20170221 Ticket:100033 , Memory Free Part4 [E]
        Timer1.Start()
    End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click

        '[說明]:紀錄按了哪些按鈕
        'Call WriteButtonLog(gUserLevel, "frmMotionOp", "btnHome_Click")
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnHome]" & vbTab & "Click")

        If gCMotion.AxisParameter(AxisNo).MotionIOStatus.blnALM = True Then
            gEqpMsg.AddHistoryAlarm("Error_1009007", "frmMotionOp btnHome_Click", , gMsgHandler.GetMessage(Error_1009007), eMessageLevel.Error)      '[Z軸馬達Alarm]
            Exit Sub
        End If
        If gCMotion.SetNELEnable(AxisNo, False) <> CommandStatus.Sucessed Then
            MsgBox("To Be Continued.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnHome.Enabled = True
            Exit Sub
        End If
        If gCMotion.SetPELEnable(AxisNo, False) <> CommandStatus.Sucessed Then
            MsgBox("To Be Continued.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnHome.Enabled = True
            Exit Sub
        End If
        gCMotion.WaitCmdStatus(AxisNo) 'Soni / 2017.05.10
        gCMotion.SetHomeVelLow(AxisNo, gCMotion.AxisParameter(AxisNo).Velocity.HomeVelLow)
        gCMotion.WaitCmdStatus(AxisNo) 'Soni / 2017.05.10
        gCMotion.SetHomeVelHigh(AxisNo, gCMotion.AxisParameter(AxisNo).Velocity.HomeVelHigh)
        gCMotion.WaitCmdStatus(AxisNo) 'Soni / 2017.05.10
        gCMotion.SetHomeAcc(AxisNo, gCMotion.AxisParameter(AxisNo).Velocity.HomeAcc)
        gCMotion.WaitCmdStatus(AxisNo) 'Soni / 2017.05.10
        gCMotion.SetHomeDec(AxisNo, gCMotion.AxisParameter(AxisNo).Velocity.HomeDec)
        gCMotion.WaitCmdStatus(AxisNo) 'Soni / 2017.05.10
        gCMotion.AxisResetError(AxisNo)
        gCMotion.Home(AxisNo)
        tmrMotorMenu.Start()
        btnHome.Enabled = False
    End Sub

    Private Sub btnServo_Click(sender As Object, e As EventArgs) Handles btnServo.Click
        '[說明]:紀錄按了哪些按鈕
        'Call WriteButtonLog(gUserLevel, "frmMotionOp", "btnServo")
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnServo]" & vbTab & "Click")
        If gCMotion.AxisParameter(AxisNo).MotionIOStatus.blnSVON = True Then
            gCMotion.Servo(AxisNo, enmONOFF.eOff)
            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '視為沒Initial過
            chkLoop.Checked = False
            gSyslog.Save(gCMotion.AxisParameter(AxisNo).AxisName & gMsgHandler.GetMessage(INFO_6001068)) 'Servo Off.
        Else
            gCMotion.Servo(AxisNo, enmONOFF.eON)
            gSyslog.Save(gCMotion.AxisParameter(AxisNo).AxisName & gMsgHandler.GetMessage(INFO_6001067)) 'Servo On.
        End If
    End Sub

    Private Sub btnUp_MouseDown(sender As Object, e As MouseEventArgs) Handles btnUp.MouseDown
        gSyslog.Save("[FrmMotionOp]" & vbTab & "[btnUp]" & vbTab & "MouseDown")
        CmdQueue.Enqueue(CommandType.UpDown)
        mAutoWait.Set()
    End Sub

    Private Sub btnDown_MouseDown(sender As Object, e As MouseEventArgs) Handles btnDown.MouseDown
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnDown]" & vbTab & "MouseDown")
        CmdQueue.Enqueue(CommandType.DownDown)
        mAutoWait.Set()      
    End Sub

    Private Sub btnRight_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRight.MouseDown
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnRight]" & vbTab & "MouseDown")
        CmdQueue.Enqueue(CommandType.RightDown)
        mAutoWait.Set()
    End Sub

    Private Sub btnLeft_MouseDown(sender As Object, e As MouseEventArgs) Handles btnLeft.MouseDown
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnLeft]" & vbTab & "MouseDown")
        CmdQueue.Enqueue(CommandType.LeftDown)
        mAutoWait.Set()
    End Sub
    Private Sub btnDown_MouseUp(sender As Object, e As MouseEventArgs) Handles btnDown.MouseUp
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnDown]" & vbTab & "MouseUp")
        CmdQueue.Enqueue(CommandType.DownUp)
        mAutoWait.Set()
    End Sub
    Private Sub btnRight_MouseUp(sender As Object, e As MouseEventArgs) Handles btnRight.MouseUp
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnRight]" & vbTab & "MouseUp")
        CmdQueue.Enqueue(CommandType.RightUp)
        mAutoWait.Set()
    End Sub
    Private Sub btnLeft_MouseUp(sender As Object, e As MouseEventArgs) Handles btnLeft.MouseUp
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnLeft]" & vbTab & "MouseUp")
        CmdQueue.Enqueue(CommandType.LeftUp)
        mAutoWait.Set()
    End Sub

    Private Sub btnUp_MouseUp(sender As Object, e As MouseEventArgs) Handles btnUp.MouseUp
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btn]" & vbTab & "MouseUp")
        CmdQueue.Enqueue(CommandType.UpUp)
        mAutoWait.Set()
    End Sub
    ''' <summary>速度設定</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSpeed_Click(sender As Object, e As EventArgs) Handles btnSpeed.Click
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnSpeed]" & vbTab & "Click")
        Select Case mSpeedType
            Case SpeedType.Fast
                mSpeedType = SpeedType.Slow
                'btnSpeed.Text = "Slow"

                SetSpeedType(AxisNo, mSpeedType)
            Case SpeedType.Medium
                mSpeedType = SpeedType.Fast

                SetSpeedType(AxisNo, mSpeedType)
            Case SpeedType.Slow
                mSpeedType = SpeedType.Medium

                SetSpeedType(AxisNo, mSpeedType)
        End Select
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnExit]" & vbTab & "Click")
        tmrMotorMenu.Stop()
        Me.Close()

    End Sub

    Private Sub btnGo1_Click(sender As Object, e As EventArgs) Handles btnGo1.Click
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnGo1]" & vbTab & "Click")
        gCMotion.SetVelHigh(AxisNo, pos1Loop.nmuVelHigh.Value)
        gCMotion.SetAcc(AxisNo, pos1Loop.nmuAcceleration.Value)
        gCMotion.SetDec(AxisNo, pos1Loop.nmuDeleration.Value)
        gCMotion.AbsMove(AxisNo, Val(nmcTargetPos1.Text))
        CType(sender, Button).BackColor = Color.Yellow '按鍵顏色
        tmrMotorMenu.Start()
    End Sub

    Private Sub btnGo2_Click(sender As Object, e As EventArgs) Handles btnGo2.Click
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnGo2]" & vbTab & "Click")
        gCMotion.SetVelHigh(AxisNo, pos2Loop.nmuVelHigh.Value)
        gCMotion.SetAcc(AxisNo, pos2Loop.nmuAcceleration.Value)
        gCMotion.SetDec(AxisNo, pos2Loop.nmuDeleration.Value)
        gCMotion.AbsMove(AxisNo, Val(nmcTargetPos2.Text))
        CType(sender, Button).BackColor = Color.Yellow '按鍵顏色
        tmrMotorMenu.Start()
    End Sub

    Private Sub btnConfig1_Click(sender As Object, e As EventArgs) Handles btnConfig1.Click
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnConfig1]" & vbTab & "Click")
        btnConfig1.BackColor = Color.GreenYellow
        If pos1Loop Is Nothing Then
            pos1Loop = New frmMotionLoopConfig
        ElseIf pos1Loop.IsDisposed Then
            pos1Loop = New frmMotionLoopConfig
        End If
        With pos1Loop
            .Location = New Point(Me.Location.X + Me.Width, Me.Location.Y)
            .Show()
            .BringToFront()
        End With

    End Sub

    Private Sub btnConfig2_Click(sender As Object, e As EventArgs) Handles btnConfig2.Click
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnConfig2]" & vbTab & "Click")
        btnConfig2.BackColor = Color.GreenYellow
        If pos2Loop Is Nothing Then
            pos2Loop = New frmMotionLoopConfig
        ElseIf pos2Loop.IsDisposed Then
            pos2Loop = New frmMotionLoopConfig
        End If
        With pos2Loop
            .Location = New Point(Me.Location.X + Me.Width, Me.Location.Y)
            .Show()
            .BringToFront()
        End With


    End Sub

    Private Sub btnAdvance_Click(sender As Object, e As EventArgs) Handles btnAdvance.Click
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnAdvance]" & vbTab & "Click")
        If btnAdvance.Enabled = False Then
            Exit Sub
        End If
        btnAdvance.Enabled = False
        btnAdvance.BackColor = Color.GreenYellow

        If gUserLevel <= enmUserLevel.eAdministrator Then
            If frmAxisAdvance Is Nothing Then
                frmAxisAdvance = New frmMotionOption
            ElseIf frmAxisAdvance.IsDisposed Then
                frmAxisAdvance = New frmMotionOption
            End If
            frmAxisAdvance.AxisNo = AxisNo
            frmAxisAdvance.SyncForm = Me
            frmAxisAdvance.Location = New Point(Me.Location.X + Me.Width, Me.Location.Y)
            frmAxisAdvance.Show()
            frmAxisAdvance.BringToFront()
        Else
            frmIoStatus = New frmIOStatus
            frmIoStatus.AxisNo = AxisNo
            frmIoStatus.SyncForm = Me
            frmIoStatus.Location = New Point(Me.Location.X + Me.Width, Me.Location.Y)
            frmIoStatus.Show()
            frmIoStatus.BringToFront()
        End If

        btnAdvance.Enabled = True
    End Sub

    Private Sub btnNextAxis_Click(sender As Object, e As EventArgs) Handles btnNextAxis.Click
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnNextAxis]" & vbTab & "Click")
        If btnNextAxis.Enabled = False Then
            Exit Sub
        End If
        btnNextAxis.Enabled = False
        For i As Integer = 0 To 10 '往下找10個軸
            AxisNo += 1
            If AxisNo > gCMotion.AxisParameter.Count - 1 Then
                AxisNo = 0
            End If
            If gCMotion.AxisParameter(AxisNo).AxisName <> "" Then '只要名稱不為空即可
                Exit For
            End If
        Next

        If frmAxisAdvance Is Nothing Then
            frmAxisAdvance = New frmMotionOption
        ElseIf frmAxisAdvance.IsDisposed Then
            frmAxisAdvance = New frmMotionOption
        End If
        frmAxisAdvance.AxisNo = AxisNo

        frmMotorMenu_Load(Me, e)
        btnNextAxis.Enabled = True
    End Sub

    Private Sub btnPrevAxis_Click(sender As Object, e As EventArgs) Handles btnPrevAxis.Click
        gSyslog.Save("[frmMotionOp]" & vbTab & "[btnPrevAxis]" & vbTab & "Click")
        If btnPrevAxis.Enabled = False Then
            Exit Sub
        End If
        btnPrevAxis.Enabled = False
        For i As Integer = 0 To 10
            AxisNo -= 1
            If AxisNo < 0 Then
                AxisNo = gCMotion.AxisParameter.Count - 1
            End If
            If gCMotion.AxisParameter(AxisNo).AxisName <> "" Then '只要名稱不為空即可
                Exit For
            End If
        Next

        If frmAxisAdvance Is Nothing Then
            frmAxisAdvance = New frmMotionOption
        ElseIf frmAxisAdvance.IsDisposed Then
            frmAxisAdvance = New frmMotionOption
        End If
        frmAxisAdvance.AxisNo = AxisNo

        frmMotorMenu_Load(Me, e)
        btnPrevAxis.Enabled = True
    End Sub


    Private Sub chkLoop_CheckedChanged(sender As Object, e As EventArgs) Handles chkLoop.CheckedChanged
        gSyslog.Save("[frmMotionOp]" & vbTab & "[chkLoop]" & vbTab & "CheckedChanged")
        If chkLoop.Checked Then
            tmrMotorMenu.Start()
        Else
            tmrMotorMenu.Stop()
        End If
    End Sub

    'Private Sub btnCalculater1_Click(sender As Object, e As EventArgs)
    '    gSyslog.Save("[frmMotionOp]" & vbTab & "[btnDigitKeyboard1]" & vbTab & "Click")
    '    btnDigitKeyboard1.BackColor = Color.GreenYellow

    '    If frmDigitKB Is Nothing Then
    '        frmDigitKB = New frmDigitKeyboard
    '    ElseIf frmDigitKB.IsDisposed Then
    '        frmDigitKB = New frmDigitKeyboard
    '    End If
    '    frmDigitKB.Location = New Point(btnDigitKeyboard1.Location.X + btnDigitKeyboard1.Width, btnDigitKeyboard1.Location.Y)
    '    frmDigitKB.ShowDialog()
    '    nmcTargetPos1.Text = frmDigitKB.UcCalculater1.Value
    '    btnDigitKeyboard1.BackColor = SystemColors.Control
    'End Sub

    'Private Sub btnCalculater2_Click(sender As Object, e As EventArgs)
    '    gSyslog.Save("[frmMotionOp]" & vbTab & "[btnDigitKeyboard2]" & vbTab & "Click")
    '    btnDigitKeyboard2.BackColor = Color.GreenYellow
    '    If frmDigitKB Is Nothing Then
    '        frmDigitKB = New frmDigitKeyboard
    '    ElseIf frmDigitKB.IsDisposed Then
    '        frmDigitKB = New frmDigitKeyboard
    '    End If
    '    frmDigitKB.Location = New Point(btnDigitKeyboard2.Location.X + btnDigitKeyboard2.Width, btnDigitKeyboard2.Location.Y)
    '    frmDigitKB.ShowDialog()
    '    nmcTargetPos2.Text = frmDigitKB.UcCalculater1.Value
    '    btnDigitKeyboard2.BackColor = SystemColors.Control
    'End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Select Case CheckBox1.Checked
            Case False
                gCMotion.SetCurve(AxisNo, eCurveMode.TCurve)
            Case True
                gCMotion.SetCurve(AxisNo, eCurveMode.SCurve)
        End Select
    End Sub

    Private Sub frmMotionOp_Move(sender As Object, e As EventArgs) Handles Me.Move
        If frmAxisAdvance Is Nothing Then
            frmAxisAdvance = New frmMotionOption
        ElseIf frmAxisAdvance.IsDisposed Then
            frmAxisAdvance = New frmMotionOption
        End If
        frmAxisAdvance.Location = New Point(Me.Location.X + Me.Width, Me.Location.Y)
    End Sub


  
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '[說明]:X軸極限、Home Sensor狀態、Servo 狀態

        SetSensorBackgroundImage(palNEL, gCMotion.AxisParameter(AxisNo).MotionIOStatus.blnNEL, False)
        SetSensorBackgroundImage(palPEL, gCMotion.AxisParameter(AxisNo).MotionIOStatus.blnPEL, False)
        SetSensorBackgroundImage(palORG, gCMotion.AxisParameter(AxisNo).MotionIOStatus.blnORG, False)
        SetSensorBackgroundImage(palALM, gCMotion.AxisParameter(AxisNo).MotionIOStatus.blnALM, False)

        If gDICollection.GetState(enmDI.EMO, False) = True Then
            gCMotion.EmgStop(AxisNo)
            chkLoop.Checked = False
            Exit Sub
        End If

        Select Case gCMotion.AxisParameter(AxisNo).CardParameter.CardType
            Case enmMotionCardType.ModBus '先不提供該功能
            Case enmMotionCardType.PCI_1245, enmMotionCardType.PCI_1285
                If gCMotion.AxisParameter(AxisNo).MotionIOStatus.blnSVON = False Then
                    'btnServo.Text = myResource.GetString("btnServo2.Text") ' "ServoOff" 
                    btnServo.BackgroundImage = My.Resources.ServoOff
                    'btnServo.BackColor = Color.Red
                Else
                    'btnServo.Text = myResource.GetString("btnServo.Text") ' "ServoOn"
                    btnServo.BackgroundImage = My.Resources.ServoOn
                    'btnServo.BackColor = SystemColors.Control
                End If
                lblAxisPosition.Text = gCMotion.GetPositionValue(AxisNo) '更新位置
        End Select

    End Sub
End Class
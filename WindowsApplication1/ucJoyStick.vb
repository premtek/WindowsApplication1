Imports ProjectMotion
Imports ProjectCore
Imports ProjectIO
Imports System.Threading

Public Enum CommandType
    ''' <summary>左鍵按下</summary>
    ''' <remarks></remarks>
    LeftDown
    ''' <summary>左鍵放開</summary>
    ''' <remarks></remarks>
    LeftUp
    ''' <summary>
    ''' 右鍵按下
    ''' </summary>
    ''' <remarks></remarks>
    RightDown
    ''' <summary>
    ''' 右鍵放開
    ''' </summary>
    ''' <remarks></remarks>
    RightUp
    ''' <summary>
    ''' 上鍵按下
    ''' </summary>
    ''' <remarks></remarks>
    UpDown
    ''' <summary>
    ''' 上鍵放開
    ''' </summary>
    ''' <remarks></remarks>
    UpUp
    ''' <summary>
    ''' 下鍵按下
    ''' </summary>
    ''' <remarks></remarks>
    DownDown
    ''' <summary>
    ''' 下鍵放開
    ''' </summary>
    ''' <remarks></remarks>
    DownUp
    ''' <summary>
    ''' 前鍵按下
    ''' </summary>
    ''' <remarks></remarks>
    ForwardUp
    ''' <summary>
    ''' 前鍵放開
    ''' </summary>
    ''' <remarks></remarks>
    ForwardDown
    ''' <summary>
    ''' 後鍵按下
    ''' </summary>
    ''' <remarks></remarks>
    BackwardUp
    ''' <summary>
    ''' 後鍵放開
    ''' </summary>
    ''' <remarks></remarks>
    BackwardDown
End Enum

Public Class ucJoyStick

    Dim CmdQueue As New Queue(Of CommandType)

    Private mTaskTokenSource As CancellationTokenSource = New CancellationTokenSource()

    Private mTaskToken As CancellationToken
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

    ''' <summary>
    ''' 多語系資源檔
    ''' </summary>
    ''' <remarks></remarks>
    Public myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucJoyStick))

    WithEvents mTimer As System.Timers.Timer

    ''' <summary>直角座標X</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AxisX As Integer = -1
    ''' <summary>直角座標Y</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AxisY As Integer = -1
    ''' <summary>直角座標Z</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AxisZ As Integer = -1
    ''' <summary>對X軸旋轉</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AXisA As Integer = -1
    ''' <summary>對Y軸旋轉</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AXisB As Integer = -1
    ''' <summary>對Z軸旋轉</summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AXisC As Integer = -1
    ''' <summary>
    ''' 對立軸相關參數
    ''' </summary>
    ''' <remarks></remarks>
    Public InverseAxisX As New InverseAxis

    ''' <summary>速度形式</summary>
    ''' <remarks></remarks>
    Dim mSpeedType As SpeedType

    ''' <summary>全速度設定 </summary>
    ''' <param name="speed"></param>
    ''' <remarks></remarks>
    Public Sub SetSpeedType(ByVal speed As SpeedType)
        mSpeedType = speed
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Parent)) Then
            Me.BeginInvoke(Sub()
                               Select Case mSpeedType
                                   Case SpeedType.Fast '高速
                                       btnSpeed.Image = My.Resources.SpeedHigh
                                       btnSpeed.BackColor = Color.Orange
                                   Case SpeedType.Medium '中速
                                       btnSpeed.Image = My.Resources.SpeedMid
                                       btnSpeed.BackColor = Color.Yellow
                                   Case SpeedType.Slow '低速
                                       btnSpeed.Image = My.Resources.SpeedLow
                                       btnSpeed.BackColor = Color.Green
                               End Select
                           End Sub)
        End If

        SetMode()

    End Sub

    ''' <summary>設定模式.速度.顯示</summary>
    ''' <remarks></remarks>
    Sub SetMode()
        Select Case mMode
            Case eMode.Jog '等速移動
                InvokeButton(btnSpeed, myResource.GetString("btnSpeed.Text"))
                gSyslog.Save("JoyStick Mode: Speed " & mSpeedType.ToString())
                Select Case mCoord
                    Case eCoord.Linear '設定XYZ速度
                        SetSpeedType(AxisX, mSpeedType)
                        SetSpeedType(AxisY, mSpeedType)
                        SetSpeedType(AxisZ, mSpeedType)
                    Case eCoord.Rotation '設定ABC速度
                        SetSpeedType(AXisA, mSpeedType)
                        SetSpeedType(AXisB, mSpeedType)
                        SetSpeedType(AXisC, mSpeedType)
                End Select

            Case eMode.Distance '等距離移動
                Select Case mSpeedType
                    Case SpeedType.Fast
                        btnSpeed.BeginInvoke(Sub()
                                                 btnSpeed.Text = "100um"
                                             End Sub)
                        'InvokeButton(btnSpeed, "100um")
                        gSyslog.Save("JoyStick Mode: Distance(100um)")
                    Case SpeedType.Medium
                        btnSpeed.BeginInvoke(Sub()
                                                 btnSpeed.Text = "10um"
                                             End Sub)
                        'InvokeButton(btnSpeed, "10um")
                        gSyslog.Save("JoyStick Mode: Distance(10um)")
                    Case SpeedType.Slow
                        btnSpeed.BeginInvoke(Sub()
                                                 btnSpeed.Text = "1um"
                                             End Sub)
                        'InvokeButton(btnSpeed, "1um")
                        gSyslog.Save("JoyStick Mode: Distance(1um)")
                End Select

        End Select
    End Sub

    ''' <summary>是否計時可持續運轉</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsTimerCanRun() As Boolean
        Dim mIsAXisACanRun As Boolean
        Dim mIsAXisBCanRun As Boolean
        Dim mIsAXisCCanRun As Boolean

        Select Case mCoord
            Case eCoord.Linear
                If gCMotion.AxisParameter(AxisX).CardParameter.CardType = enmMotionCardType.PCI_1245 Or gCMotion.AxisParameter(AxisX).CardParameter.CardType = enmMotionCardType.PCI_1285 Then
                    If gCMotion.AxisParameter(AxisY).CardParameter.CardType = enmMotionCardType.PCI_1245 Or gCMotion.AxisParameter(AxisY).CardParameter.CardType = enmMotionCardType.PCI_1285 Then
                        If gCMotion.AxisParameter(AxisZ).CardParameter.CardType = enmMotionCardType.PCI_1245 Or gCMotion.AxisParameter(AxisZ).CardParameter.CardType = enmMotionCardType.PCI_1285 Then
                            Return True
                        End If
                    End If
                End If
            Case eCoord.Rotation
                'If gCMotion.AxisParameter(AXisA).CardParameter.CardType = enmMotionCardType.PCI_1245 Or gCMotion.AxisParameter(AXisA).CardParameter.CardType = enmMotionCardType.PCI_1285 Then
                '    If gCMotion.AxisParameter(AXisB).CardParameter.CardType = enmMotionCardType.PCI_1245 Or gCMotion.AxisParameter(AXisB).CardParameter.CardType = enmMotionCardType.PCI_1285 Then
                '        If gCMotion.AxisParameter(AXisC).CardParameter.CardType = enmMotionCardType.PCI_1245 Or gCMotion.AxisParameter(AXisC).CardParameter.CardType = enmMotionCardType.PCI_1285 Then
                '            Return True
                '        End If
                '    End If
                'End If

                If AXisA <> -1 Then
                    If gCMotion.AxisParameter(AXisA).CardParameter.CardType = enmMotionCardType.PCI_1245 Or gCMotion.AxisParameter(AXisA).CardParameter.CardType = enmMotionCardType.PCI_1285 Then
                        mIsAXisACanRun = True
                    Else
                        mIsAXisACanRun = False
                    End If
                Else
                    mIsAXisACanRun = True
                End If

                If AXisB <> -1 Then
                    If gCMotion.AxisParameter(AXisB).CardParameter.CardType = enmMotionCardType.PCI_1245 Or gCMotion.AxisParameter(AXisB).CardParameter.CardType = enmMotionCardType.PCI_1285 Then
                        mIsAXisBCanRun = True
                    Else
                        mIsAXisBCanRun = False
                    End If
                Else
                    mIsAXisBCanRun = True
                End If

                If AXisC <> -1 Then
                    If gCMotion.AxisParameter(AXisC).CardParameter.CardType = enmMotionCardType.PCI_1245 Or gCMotion.AxisParameter(AXisC).CardParameter.CardType = enmMotionCardType.PCI_1285 Then
                        mIsAXisCCanRun = True
                    Else
                        mIsAXisCCanRun = False
                    End If
                Else
                    mIsAXisCCanRun = True
                End If

                If mIsAXisACanRun = True And mIsAXisBCanRun = True And mIsAXisCCanRun = True Then
                    Return True
                Else
                    Return False
                End If

        End Select

        Return False
    End Function

    ''' <summary>速度形式設定</summary>
    ''' <param name="axisIndex"></param>
    ''' <param name="speed"></param>
    ''' <remarks></remarks>
    Private Sub SetSpeedType(ByVal axisIndex As Integer, ByVal speed As SpeedType)
        Select Case speed
            Case SpeedType.Fast
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetVelHigh(axisIndex, 60)
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetAcc(axisIndex, 60)
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetDec(axisIndex, 60)
                gCMotion.WaitCmdStatus(axisIndex)
            Case SpeedType.Medium
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetVelHigh(axisIndex, 30)
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetAcc(axisIndex, 30)
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetDec(axisIndex, 30)
                gCMotion.WaitCmdStatus(axisIndex)
            Case SpeedType.Slow
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetVelHigh(axisIndex, 10)
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetAcc(axisIndex, 10)
                gCMotion.WaitCmdStatus(axisIndex)
                gCMotion.SetDec(axisIndex, 10)
                gCMotion.WaitCmdStatus(axisIndex)

        End Select
    End Sub

#Region "命令"
    Private Sub btnLeft_MouseDown(sender As Object, e As MouseEventArgs) Handles btnLeft.MouseDown
        CmdQueue.Enqueue(CommandType.LeftDown)
        mAutoWait.Set()
        Debug.Print("btnLeft_MouseDown")
    End Sub
    Private Sub btnRight_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRight.MouseDown

        CmdQueue.Enqueue(CommandType.RightDown)
        mAutoWait.Set()

    End Sub
    Private Sub btnBack_MouseDown(sender As Object, e As MouseEventArgs) Handles btnBack.MouseDown
        CmdQueue.Enqueue(CommandType.BackwardDown)
        mAutoWait.Set()
        'Exit Sub
        'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Back_MouseDown))

    End Sub
    Private Sub btnForward_MouseDown(sender As Object, e As MouseEventArgs) Handles btnForward.MouseDown
        CmdQueue.Enqueue(CommandType.ForwardDown)
        mAutoWait.Set()
        'Exit Sub
        'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Forward_MouseDown))
    End Sub
    Private Sub btnDown_MouseDown(sender As Object, e As MouseEventArgs) Handles btnDown.MouseDown
        CmdQueue.Enqueue(CommandType.DownDown)
        mAutoWait.Set()
        'Exit Sub
        'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Down_MouseDown))
    End Sub
    Private Sub btnUp_MouseDown(sender As Object, e As MouseEventArgs) Handles btnUp.MouseDown
        CmdQueue.Enqueue(CommandType.UpDown)
        mAutoWait.Set()
        'Exit Sub
        'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Up_MouseDown))
    End Sub

    Private Sub btnUp_MouseUp(sender As Object, e As MouseEventArgs) Handles btnUp.MouseUp
        CmdQueue.Enqueue(CommandType.UpUp)
        mAutoWait.Set()
        'Exit Sub
        'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Up_MouseUp))
    End Sub
    Private Sub btnDown_MouseUp(sender As Object, e As MouseEventArgs) Handles btnDown.MouseUp
        CmdQueue.Enqueue(CommandType.DownUp)
        mAutoWait.Set()
        'Exit Sub
        'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Down_MouseUp))
    End Sub
    Private Sub btnRight_MouseUp(sender As Object, e As MouseEventArgs) Handles btnRight.MouseUp
        CmdQueue.Enqueue(CommandType.RightUp)
        mAutoWait.Set()
        'Exit Sub
        'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Right_MouseUp))
    End Sub
    Private Sub btnLeft_MouseUp(sender As Object, e As MouseEventArgs) Handles btnLeft.MouseUp
        CmdQueue.Enqueue(CommandType.LeftUp)
        mAutoWait.Set()
        'Exit Sub
        'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Left_MouseUp))
    End Sub
    Private Sub btnBack_MouseUp(sender As Object, e As MouseEventArgs) Handles btnBack.MouseUp
        CmdQueue.Enqueue(CommandType.BackwardUp)
        mAutoWait.Set()
        'Exit Sub
        'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Back_MouseUp))
    End Sub
    Private Sub btnForward_MouseUp(sender As Object, e As MouseEventArgs) Handles btnForward.MouseUp
        CmdQueue.Enqueue(CommandType.ForwardUp)
        mAutoWait.Set()
        'Exit Sub
        'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Forward_MouseUp))
    End Sub

#End Region

#Region "實際動作"
    Sub Left_MouseDown()
        gSyslog.Save("[ucJoyStick]" & vbTab & "[btnLeft]" & vbTab & "MouseDown")
        If IsTimerCanRun() Then
            SetSpeedType(mSpeedType)
            mTimer.Enabled = True
        End If

        '[說明]:按下去移動，放掉就停止
        Dim dir As eDirection '移動方向
        Dim mAxis As Integer
        Select Case mCoord
            Case eCoord.Linear
                mAxis = AxisX
            Case eCoord.Rotation
                mAxis = AXisA
        End Select
        If gCMotion.AxisParameter(mAxis).Parameter.ButtonDirection = eDirection.Negative Then
            dir = eDirection.Positive
        Else
            dir = eDirection.Negative
        End If

        Dim cmdPosX As Decimal
        Dim posivtiveLimit As Decimal = gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit
        Dim negativeLimit As Decimal = gCMotion.AxisParameter(mAxis).Limit.NegativeLimit
        If (InverseAxisX.Axis <> -1 Or InverseAxisX.SafeDistance <> -1 Or InverseAxisX.Spread <> -1) Then
            gCMotion.GetTargetPos(InverseAxisX.Axis, cmdPosX)
            cmdPosX = gCMotion.GetPositionValue(InverseAxisX.Axis)
            '重新計算正極限
            If (InverseAxisX.Direction = InverseAxis.enmDirection.Posivtive) Then
                posivtiveLimit = InverseAxisX.Spread - Math.Abs(cmdPosX) - InverseAxisX.SafeDistance
            End If
            '重新計算負極限
            If (InverseAxisX.Direction = InverseAxis.enmDirection.Negative) Then
                negativeLimit = -(InverseAxisX.Spread - Math.Abs(cmdPosX) - InverseAxisX.SafeDistance)
            End If
        End If

        Select Case mMode
            Case eMode.Distance
                If gCMotion.MotionDone(mAxis) = CommandStatus.Sucessed Then '停止才能下
                    Dim direction As Double = IIf(dir = eDirection.Negative, -1, 1)
                    Dim nowPos As Double = (gCMotion.GetPositionValue(mAxis))
                    Dim movePos As Double
                    Select Case mSpeedType
                        Case SpeedType.Slow
                            movePos = nowPos + (0.001 * direction)
                            If movePos > posivtiveLimit Or movePos < negativeLimit Then
                                MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Else
                                gCMotion.RelMove(mAxis, 0.001 * direction)
                                gSyslog.Save("Click btnLeft@UcJoyStick(Step 1 um)")
                            End If
                        Case SpeedType.Medium
                            movePos = nowPos + (0.01 * direction)
                            If movePos > posivtiveLimit Or movePos < negativeLimit Then
                                MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Else
                                gCMotion.RelMove(mAxis, 0.01 * direction)
                                gSyslog.Save("Click btnLeft@UcJoyStick(Step 10 um)")
                            End If
                        Case SpeedType.Fast
                            movePos = nowPos + (0.1 * direction)
                            If movePos > posivtiveLimit Or movePos < negativeLimit Then
                                MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Else
                                gCMotion.RelMove(mAxis, 0.1 * direction)
                                gSyslog.Save("Click btnLeft@UcJoyStick(Step 100 um)")
                            End If
                    End Select
                End If

            Case eMode.Jog
                gSyslog.Save("Click btnLeft@UcJoyStick(Jog" & mSpeedType.ToString() & ")")
                'If gCMotion.VelMove(mAxis, dir) = False Then
                '    gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnLeft", , gMsgHandler.GetMessage(Error_1009006))
                'End If

                '20170712_往軟體極限絕對移動
                If dir = eDirection.Positive Then
                    If gCMotion.AbsMove(mAxis, posivtiveLimit) = False Then
                        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Left_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                    End If
                ElseIf dir = eDirection.Negative Then
                    If gCMotion.AbsMove(mAxis, negativeLimit) = False Then
                        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Left_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                    End If
                End If

        End Select

        btnLeft.BackColor = Color.Yellow '按鍵顏色

        Select Case gCMotion.AxisParameter(mAxis).CardParameter.CardType
            Case enmMotionCardType.ModBus
                mTimer.Enabled = True
        End Select
    End Sub

    Sub Right_MouseDown()
        gSyslog.Save("[ucJoyStick]" & vbTab & "[btnRight]" & vbTab & "MouseDown")
        If IsTimerCanRun() Then
            SetSpeedType(mSpeedType)
            mTimer.Enabled = True
        End If
        '[說明]:按下去移動，放掉就停止
        Dim dir As eDirection
        Dim mAxis As Integer
        Select Case mCoord
            Case eCoord.Linear
                mAxis = AxisX
            Case eCoord.Rotation
                mAxis = AXisA
        End Select
        If gCMotion.AxisParameter(mAxis).Parameter.ButtonDirection = eDirection.Negative Then
            dir = eDirection.Negative
        Else
            dir = eDirection.Positive
        End If

        Dim cmdPosX As Decimal
        Dim posivtiveLimit As Decimal = gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit
        Dim negativeLimit As Decimal = gCMotion.AxisParameter(mAxis).Limit.NegativeLimit
        If (InverseAxisX.Axis <> -1 Or InverseAxisX.SafeDistance <> -1 Or InverseAxisX.Spread <> -1) Then
            gCMotion.GetTargetPos(InverseAxisX.Axis, cmdPosX)
            cmdPosX = gCMotion.GetPositionValue(InverseAxisX.Axis)
            '重新計算正極限
            If (InverseAxisX.Direction = InverseAxis.enmDirection.Posivtive) Then
                posivtiveLimit = InverseAxisX.Spread - Math.Abs(cmdPosX) - InverseAxisX.SafeDistance
            End If
            '重新計算負極限
            If (InverseAxisX.Direction = InverseAxis.enmDirection.Negative) Then
                negativeLimit = -(InverseAxisX.Spread - Math.Abs(cmdPosX) - InverseAxisX.SafeDistance)
            End If
        End If

        Select Case mMode
            Case eMode.Distance
                Dim direction As Double = IIf(dir = eDirection.Negative, -1, 1)
                Dim nowPos As Double = (gCMotion.GetPositionValue(mAxis))
                Dim movePos As Double
                Select Case mSpeedType
                    Case SpeedType.Slow
                        movePos = nowPos + (0.001 * direction)
                        If movePos > posivtiveLimit Or movePos < negativeLimit Then
                            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            gCMotion.RelMove(mAxis, 0.001 * direction)
                            gSyslog.Save("Click btnRight@UcJoyStick(Step 1 um)")
                        End If
                    Case SpeedType.Medium
                        movePos = nowPos + (0.01 * direction)
                        If movePos > posivtiveLimit Or movePos < negativeLimit Then
                            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            gCMotion.RelMove(mAxis, 0.01 * direction)
                            gSyslog.Save("Click btnRight@UcJoyStick(Step 10 um)")
                        End If
                    Case SpeedType.Fast
                        movePos = nowPos + (0.1 * direction)
                        If movePos > posivtiveLimit Or movePos < negativeLimit Then
                            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            gCMotion.RelMove(mAxis, 0.1 * direction)
                            gSyslog.Save("Click btnRight@UcJoyStick(Step 100 um)")
                        End If
                End Select
            Case eMode.Jog
                gSyslog.Save("Click btnRight@UcJoyStick(Jog" & mSpeedType.ToString() & ")")
                'If gCMotion.VelMove(mAxis, dir) = False Then
                '    gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnRight", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                'End If

                '20170712_往軟體極限絕對移動
                If dir = eDirection.Positive Then
                    If gCMotion.AbsMove(mAxis, posivtiveLimit) = False Then
                        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Right_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                    End If
                ElseIf dir = eDirection.Negative Then
                    If gCMotion.AbsMove(mAxis, negativeLimit) = False Then
                        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Right_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                    End If
                End If
        End Select

        btnRight.BackColor = Color.Yellow '按鍵顏色
        Select Case gCMotion.AxisParameter(mAxis).CardParameter.CardType
            Case enmMotionCardType.ModBus
                mTimer.Enabled = True
        End Select
    End Sub
    Sub Back_MouseDown()
        SyncLock Me
            Debug.Print("btnBack_MouseDown")
            gSyslog.Save("[ucJoyStick]" & vbTab & "[btnBack]" & vbTab & "MouseDown")

            If IsTimerCanRun() Then
                SetSpeedType(mSpeedType)
                mTimer.Enabled = True
            End If
            '[說明]:按下去移動，放掉就停止

            Dim dir As eDirection '移動方向
            Dim mAxis As Integer
            Select Case mCoord
                Case eCoord.Linear
                    mAxis = AxisY
                Case eCoord.Rotation
                    mAxis = AXisB
            End Select

            If gCMotion.AxisParameter(mAxis).Parameter.ButtonDirection = eDirection.Negative Then
                dir = eDirection.Positive
            Else
                dir = eDirection.Negative
            End If
            Select Case mMode
                Case eMode.Distance
                    Dim direction As Double = IIf(dir = eDirection.Negative, -1, 1)
                    Dim nowPos As Double = (gCMotion.GetPositionValue(mAxis))
                    Dim movePos As Double
                    Select Case mSpeedType
                        Case SpeedType.Slow
                            movePos = nowPos + (0.001 * direction)
                            If movePos > gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit Or movePos < gCMotion.AxisParameter(mAxis).Limit.NegativeLimit Then
                                MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Else
                                gCMotion.RelMove(mAxis, 0.001 * direction)
                                gSyslog.Save("Click btnBack@UcJoyStick(Step 1 um)")
                            End If
                        Case SpeedType.Medium
                            movePos = nowPos + (0.01 * direction)
                            If movePos > gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit Or movePos < gCMotion.AxisParameter(mAxis).Limit.NegativeLimit Then
                                MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Else
                                gCMotion.RelMove(mAxis, 0.01 * direction)
                                gSyslog.Save("Click btnBack@UcJoyStick(Step 10 um)")
                            End If
                        Case SpeedType.Fast
                            movePos = nowPos + (0.1 * direction)
                            If movePos > gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit Or movePos < gCMotion.AxisParameter(mAxis).Limit.NegativeLimit Then
                                MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Else
                                gCMotion.RelMove(mAxis, 0.1 * direction)
                                gSyslog.Save("Click btnBack@UcJoyStick(Step 100 um)")
                            End If
                    End Select
                Case eMode.Jog
                    gSyslog.Save("Click btnBack@UcJoyStick(Jog" & mSpeedType.ToString() & ")")
                    'If gCMotion.VelMove(mAxis, dir) = False Then
                    '    gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnBack", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                    'End If
                    '20170712 往軟體極限絕對移動
                    If dir = eDirection.Positive Then
                        If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit) = False Then
                            gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Back_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                        End If
                    ElseIf dir = eDirection.Negative Then
                        If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.NegativeLimit) = False Then
                            gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Back_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                        End If
                    End If

            End Select

            btnBack.BackColor = Color.Yellow '按鍵顏色
            Select Case gCMotion.AxisParameter(mAxis).CardParameter.CardType
                Case enmMotionCardType.ModBus
                    mTimer.Enabled = True
            End Select
        End SyncLock
    End Sub

    Sub Forward_MouseDown()
        gSyslog.Save("[ucJoyStick]" & vbTab & "[btnForward]" & vbTab & "MouseDown")
        If IsTimerCanRun() Then
            SetSpeedType(mSpeedType)
            mTimer.Enabled = True
        End If
        '[說明]:按下去移動，放掉就停止
        Dim dir As eDirection
        Dim mAxis As Integer
        Select Case mCoord
            Case eCoord.Linear
                mAxis = AxisY
            Case eCoord.Rotation
                mAxis = AXisB
        End Select
        If gCMotion.AxisParameter(mAxis).Parameter.ButtonDirection = eDirection.Negative Then
            dir = eDirection.Negative
        Else
            dir = eDirection.Positive
        End If
        Select Case mMode
            Case eMode.Distance
                Dim direction As Double = IIf(dir = eDirection.Negative, -1, 1)
                Dim nowPos As Double = (gCMotion.GetPositionValue(mAxis))
                Dim movePos As Double
                Select Case mSpeedType
                    Case SpeedType.Slow
                        movePos = nowPos + (0.001 * direction)
                        If movePos > gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit Or movePos < gCMotion.AxisParameter(mAxis).Limit.NegativeLimit Then
                            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            gCMotion.RelMove(mAxis, 0.001 * direction)
                            gSyslog.Save("Click btnForward@UcJoyStick(Step 1 um)")
                        End If
                    Case SpeedType.Medium
                        movePos = nowPos + (0.01 * direction)
                        If movePos > gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit Or movePos < gCMotion.AxisParameter(mAxis).Limit.NegativeLimit Then
                            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            gCMotion.RelMove(mAxis, 0.01 * direction)
                            gSyslog.Save("Click btnForward@UcJoyStick(Step 10 um)")
                        End If
                    Case SpeedType.Fast
                        movePos = nowPos + (0.1 * direction)
                        If movePos > gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit Or movePos < gCMotion.AxisParameter(mAxis).Limit.NegativeLimit Then
                            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            gCMotion.RelMove(mAxis, 0.1 * direction)
                            gSyslog.Save("Click btnForward@UcJoyStick(Step 100 um)")
                        End If
                End Select
            Case eMode.Jog
                gSyslog.Save("Click btnForward@UcJoyStick(Jog" & mSpeedType.ToString() & ")")
                'If gCMotion.VelMove(mAxis, dir) = False Then
                '    gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnForward", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                'End If

                '20170712_往軟體極限絕對移動
                If dir = eDirection.Positive Then
                    If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit) = False Then
                        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Forward_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                    End If
                ElseIf dir = eDirection.Negative Then
                    If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.NegativeLimit) = False Then
                        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Forward_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                    End If
                End If

        End Select

        btnForward.BackColor = Color.Yellow '按鍵顏色
        Select Case gCMotion.AxisParameter(mAxis).CardParameter.CardType
            Case enmMotionCardType.ModBus
                mTimer.Enabled = True
        End Select
    End Sub

    Sub Down_MouseDown()
        gSyslog.Save("[ucJoyStick]" & vbTab & "[btnDown]" & vbTab & "MouseDown")
        If IsTimerCanRun() Then
            SetSpeedType(mSpeedType)
            mTimer.Enabled = True
        End If
        '[說明]:按下去移動，放掉就停止
        Dim dir As eDirection
        Dim mAxis As Integer
        Select Case mCoord
            Case eCoord.Linear
                mAxis = AxisZ
            Case eCoord.Rotation
                mAxis = AXisC
        End Select
        If gCMotion.AxisParameter(mAxis).Parameter.ButtonDirection = eDirection.Negative Then
            dir = eDirection.Negative
        Else
            dir = eDirection.Positive
        End If
        Select Case mMode
            Case eMode.Distance
                Dim direction As Double = IIf(dir = eDirection.Negative, -1, 1)
                Dim nowPos As Double = (gCMotion.GetPositionValue(mAxis))
                Dim movePos As Double
                Select Case mSpeedType
                    Case SpeedType.Slow
                        movePos = nowPos + (0.001 * direction)
                        If movePos > gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit Or movePos < gCMotion.AxisParameter(mAxis).Limit.NegativeLimit Then
                            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            gCMotion.RelMove(mAxis, 0.001 * direction)
                            gSyslog.Save("Click btnDown@UcJoyStick(Step 1 um)")
                        End If
                    Case SpeedType.Medium
                        movePos = nowPos + (0.01 * direction)
                        If movePos > gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit Or movePos < gCMotion.AxisParameter(mAxis).Limit.NegativeLimit Then
                            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            gCMotion.RelMove(mAxis, 0.01 * direction)
                            gSyslog.Save("Click btnDown@UcJoyStick(Step 10 um)")
                        End If
                    Case SpeedType.Fast
                        movePos = nowPos + (0.1 * direction)
                        If movePos > gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit Or movePos < gCMotion.AxisParameter(mAxis).Limit.NegativeLimit Then
                            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            gCMotion.RelMove(mAxis, 0.1 * direction)
                            gSyslog.Save("Click btnDown@UcJoyStick(Step 100 um)")
                        End If
                End Select
            Case eMode.Jog
                gSyslog.Save("Click btnDown@UcJoyStick(Jog" & mSpeedType.ToString() & ")")
                'If gCMotion.VelMove(mAxis, dir) = False Then
                '    gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                'End If

                '20170712_往軟體極限絕對移動
                If dir = eDirection.Positive Then
                    If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit) = False Then
                        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Down_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                    End If
                ElseIf dir = eDirection.Negative Then
                    If gCMotion.AbsMove(mAxis, gCMotion.AxisParameter(mAxis).Limit.NegativeLimit) = False Then
                        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Down_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                    End If
                End If
        End Select

        btnDown.BackColor = Color.Yellow '按鍵顏色
        Select Case gCMotion.AxisParameter(mAxis).CardParameter.CardType
            Case enmMotionCardType.ModBus
                mTimer.Enabled = True
        End Select
    End Sub

    Sub Up_MouseDown()
        gSyslog.Save("[ucJoyStick]" & vbTab & "[btnUp]" & vbTab & "MouseDown")
        If IsTimerCanRun() Then
            SetSpeedType(mSpeedType)
            mTimer.Enabled = True
        End If
        '[說明]:按下去移動，放掉就停止
        Dim dir As eDirection '移動方向
        Dim mAxis As Integer
        Select Case mCoord
            Case eCoord.Linear
                mAxis = AxisZ
            Case eCoord.Rotation
                mAxis = AXisC
        End Select
        If gCMotion.AxisParameter(mAxis).Parameter.ButtonDirection = eDirection.Negative Then
            dir = eDirection.Positive
        Else
            dir = eDirection.Negative
        End If
        Select Case mMode
            Case eMode.Distance
                Dim direction As Double = IIf(dir = eDirection.Negative, -1, 1)
                Dim nowPos As Double = (gCMotion.GetPositionValue(mAxis))
                Dim movePos As Double
                Select Case mSpeedType
                    Case SpeedType.Slow
                        movePos = nowPos + (0.001 * direction)
                        If movePos > gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit Or movePos < gCMotion.AxisParameter(mAxis).Limit.NegativeLimit Then
                            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            gCMotion.RelMove(mAxis, 0.001 * direction)
                            gSyslog.Save("Click btnUp@UcJoyStick(Step 1 um)")
                        End If
                    Case SpeedType.Medium
                        movePos = nowPos + (0.01 * direction)
                        If movePos > gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit Or movePos < gCMotion.AxisParameter(mAxis).Limit.NegativeLimit Then
                            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            gCMotion.RelMove(mAxis, 0.01 * direction)
                            gSyslog.Save("Click btnUp@UcJoyStick(Step 10 um)")
                        End If
                    Case SpeedType.Fast
                        movePos = nowPos + (0.1 * direction)
                        If movePos > gCMotion.AxisParameter(mAxis).Limit.PosivtiveLimit Or movePos < gCMotion.AxisParameter(mAxis).Limit.NegativeLimit Then
                            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Else
                            gCMotion.RelMove(mAxis, 0.1 * direction)
                            gSyslog.Save("Click btnUp@UcJoyStick(Step 100 um)")
                        End If
                End Select
            Case eMode.Jog
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
        End Select

        btnUp.BackColor = Color.Yellow '按鍵顏色
        Select Case gCMotion.AxisParameter(mAxis).CardParameter.CardType
            Case enmMotionCardType.ModBus
                mTimer.Enabled = False
        End Select
    End Sub
    Async Sub Up_MouseUp()
        Await Task.Run(Sub()
                           Select Case mCoord
                               Case eCoord.Linear
                                   BtnMouseUp(AxisZ, btnUp, txtPosZ)
                               Case eCoord.Rotation
                                   BtnMouseUp(AXisC, btnUp, txtPosZ)
                           End Select
                       End Sub)

    End Sub

    Async Sub Down_MouseUp()
        Await Task.Run(Sub()
                           Select Case mCoord
                               Case eCoord.Linear
                                   BtnMouseUp(AxisZ, btnDown, txtPosZ)
                               Case eCoord.Rotation
                                   BtnMouseUp(AXisC, btnDown, txtPosZ)
                           End Select
                       End Sub)
    End Sub

    Async Sub Right_MouseUp()
        Await Task.Run(Sub()
                           Select Case mCoord
                               Case eCoord.Linear
                                   BtnMouseUp(AxisX, btnRight, txtPosX)
                               Case eCoord.Rotation
                                   BtnMouseUp(AXisA, btnRight, txtPosX)
                           End Select
                       End Sub)

    End Sub

    Async Sub Left_MouseUp()
        Await Task.Run(Sub()
                           Select Case mCoord
                               Case eCoord.Linear
                                   BtnMouseUp(AxisX, btnLeft, txtPosX)
                               Case eCoord.Rotation
                                   BtnMouseUp(AXisA, btnLeft, txtPosX)
                           End Select
                       End Sub)

    End Sub

    Async Sub Back_MouseUp()
        Await Task.Run(Sub()
                           Select Case mCoord
                               Case eCoord.Linear
                                   BtnMouseUp(AxisY, btnBack, txtPosY)
                               Case eCoord.Rotation
                                   BtnMouseUp(AXisB, btnBack, txtPosY)
                           End Select
                       End Sub)
    End Sub

    Async Sub Forward_MouseUp()
        Await Task.Run(Sub()
                           Select Case mCoord
                               Case eCoord.Linear
                                   BtnMouseUp(AxisY, btnForward, txtPosY)
                               Case eCoord.Rotation
                                   BtnMouseUp(AXisB, btnForward, txtPosY)
                           End Select
                       End Sub)
    End Sub

    Sub BtnMouseUp(ByVal axisIndex As Integer, sender As Object, ByRef lblAxisPosition As TextBox)

        Select Case mMode
            Case eMode.Distance
                CType(sender, Button).BackColor = SystemColors.Control '按鍵顏色
                CType(sender, Button).UseVisualStyleBackColor = True
                InvokeTextBox(lblAxisPosition, gCMotion.GetPositionValue(axisIndex)) '更新位置
                Exit Sub
        End Select

        Dim mDec As Double = 0.2 * 9800
        '[說明]:按下去移動，放掉就停止
        Select Case mSpeedType
            Case SpeedType.Fast
                mDec = 0.2 * 9800
            Case SpeedType.Medium
                mDec = 0.1 * 9800
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

        If Not mTimer Is Nothing Then
            '在更新一次位置
            mTimer.Enabled = True
        End If

        '確保命令發完
        Do
            'Application.DoEvents()
        Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed
        System.Threading.Thread.Sleep(100) '等100ms

        gCMotion.GetPositionValue(axisIndex)
        Do
            'Application.DoEvents()
        Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed
        gCMotion.GetPositionValue(axisIndex)
        Do
            'Application.DoEvents()
        Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed


        InvokeTextBox(lblAxisPosition, gCMotion.GetPositionValue(axisIndex)) '更新位置

        CType(sender, Button).BackColor = SystemColors.Control '按鍵顏色
        CType(sender, Button).UseVisualStyleBackColor = True
    End Sub

#End Region
    Sub Dispatch()
        Do
            'Eason 20170120 Ticket:100030 , Memory Freed [S]
            If mIsDisposing Then
                Exit Sub
            End If
            'Eason 20170120 Ticket:100030 , Memory Freed [E]

            If CmdQueue.Count > 0 Then '如果有待處理影像
                Dim mCmd As CommandType = CmdQueue.Dequeue
                Select Case mCmd
                    Case CommandType.BackwardDown
                        Back_MouseDown()
                    Case CommandType.BackwardUp
                        Back_MouseUp()
                    Case CommandType.DownDown
                        Down_MouseDown()
                    Case CommandType.DownUp
                        Down_MouseUp()
                    Case CommandType.ForwardDown
                        Forward_MouseDown()
                    Case CommandType.ForwardUp
                        Forward_MouseUp()
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

    Private Sub btnSpeed_Click(sender As Object, e As EventArgs) Handles btnSpeed.Click
        gSyslog.Save("[ucJoyStick]" & vbTab & "[btnSpeed]" & vbTab & "Click")
        Select Case mSpeedType
            Case SpeedType.Fast
                mSpeedType = SpeedType.Slow
                SetSpeedType(mSpeedType)
            Case SpeedType.Medium
                mSpeedType = SpeedType.Fast
                SetSpeedType(mSpeedType)
            Case SpeedType.Slow
                mSpeedType = SpeedType.Medium
                SetSpeedType(mSpeedType)
        End Select
    End Sub


    ''' <summary>更新X,Y,Z軸座標</summary>
    ''' <remarks></remarks>
    Public Sub RefreshPosition()

        Task.Run(Sub()
                     Dim mTimeOutStopWatch As New Stopwatch
                     mTimeOutStopWatch.Restart()
                     Select Case mCoord
                         Case eCoord.Linear

                             '=== 狀態更新 ===
                             Do
                                 'Application.DoEvents()
                                 If mTimeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                                     MsgBox("Axis X Command is TimeOut!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                     Exit Sub
                                 End If
                             Loop Until gCMotion.GetCmdStatus(AxisX) = CommandStatus.Sucessed
                             '=== 狀態更新 ===
                             If txtPosX.IsDisposed = False Then
                                 txtPosX.BeginInvoke(Sub()
                                                         txtPosX.Text = gCMotion.GetPositionValue(AxisX)
                                                     End Sub)
                             End If


                             'InvokeTextBox(txtPosX, gCMotion.GetPositionValue(AxisX))

                             '=== 狀態更新 ===
                             mTimeOutStopWatch.Restart()
                             Do
                                 'Application.DoEvents()
                                 If mTimeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                                     'X軸命令執行逾時
                                     gSyslog.Save(gMsgHandler.GetMessage(Error_1030019))
                                     MsgBox("Axis X Command is TimeOut!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                     Exit Sub
                                 End If
                             Loop Until gCMotion.GetCmdStatus(AxisX) = CommandStatus.Sucessed
                             '=== 狀態更新 ===
                             If txtPosY.IsDisposed = False Then
                                 txtPosY.BeginInvoke(Sub()
                                                         txtPosY.Text = gCMotion.GetPositionValue(AxisY)
                                                     End Sub)
                             End If

                             'InvokeTextBox(txtPosY, gCMotion.GetPositionValue(AxisY))

                             '=== 狀態更新 ===
                             mTimeOutStopWatch.Restart()
                             Do
                                 'Application.DoEvents()
                                 If mTimeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                                     MsgBox("Axis Y Command is TimeOut!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                     Exit Sub
                                 End If
                             Loop Until gCMotion.GetCmdStatus(AxisY) = CommandStatus.Sucessed
                             '=== 狀態更新 ===
                             If txtPosZ.IsDisposed = False Then
                                 txtPosZ.BeginInvoke(Sub()
                                                         txtPosZ.Text = gCMotion.GetPositionValue(AxisZ)
                                                     End Sub)
                             End If

                             'InvokeTextBox(txtPosZ, gCMotion.GetPositionValue(AxisZ))

                             '=== 狀態更新 ===
                             mTimeOutStopWatch.Restart()
                             Do
                                 'Application.DoEvents()
                                 If mTimeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                                     MsgBox("Axis Z Command is TimeOut!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                     Exit Sub
                                 End If
                             Loop Until gCMotion.GetCmdStatus(AxisZ) = CommandStatus.Sucessed
                             '=== 狀態更新 ===

                         Case eCoord.Rotation
                             '=== 狀態更新 ===
                             mTimeOutStopWatch.Restart()
                             Do
                                 'Application.DoEvents()
                                 If mTimeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                                     MsgBox("Axis A Command is TimeOut!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                     Exit Sub
                                 End If
                             Loop Until gCMotion.GetCmdStatus(AXisA) = CommandStatus.Sucessed
                             '=== 狀態更新 ===
                             If txtPosX.IsDisposed = False Then
                                 txtPosX.BeginInvoke(Sub()
                                                         txtPosX.Text = gCMotion.GetPositionValue(AXisA)
                                                     End Sub)
                             End If

                             'InvokeTextBox(txtPosX, gCMotion.GetPositionValue(AXisA))

                             '=== 狀態更新 ===
                             mTimeOutStopWatch.Restart()
                             Do
                                 'Application.DoEvents()
                                 If mTimeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                                     MsgBox("Axis A Command is TimeOut!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                     Exit Sub
                                 End If
                             Loop Until gCMotion.GetCmdStatus(AXisA) = CommandStatus.Sucessed
                             '=== 狀態更新 ===
                             If txtPosY.IsDisposed = False Then
                                 txtPosY.BeginInvoke(Sub()
                                                         txtPosY.Text = gCMotion.GetPositionValue(AXisB)
                                                     End Sub)
                             End If

                             'InvokeTextBox(txtPosY, gCMotion.GetPositionValue(AXisB))

                             '=== 狀態更新 ===
                             mTimeOutStopWatch.Restart()
                             Do
                                 'Application.DoEvents()
                                 If mTimeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                                     MsgBox("Axis B Command is TimeOut!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                     Exit Sub
                                 End If
                             Loop Until gCMotion.GetCmdStatus(AXisB) = CommandStatus.Sucessed
                             '=== 狀態更新 ===
                             If txtPosZ.IsDisposed = False Then
                                 txtPosZ.BeginInvoke(Sub()
                                                         txtPosZ.Text = gCMotion.GetPositionValue(AXisC)
                                                     End Sub)
                             End If

                             'InvokeTextBox(txtPosZ, gCMotion.GetPositionValue(AXisC)) 'txtPosZ.Text = gCMotion.GetPositionValue(AXisC)

                             '=== 狀態更新 ===
                             mTimeOutStopWatch.Restart()
                             Do
                                 'Application.DoEvents()
                                 If mTimeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
                                     MsgBox("Axis C Command is TimeOut!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                     Exit Sub
                                 End If
                             Loop Until gCMotion.GetCmdStatus(AXisC) = CommandStatus.Sucessed
                             '=== 狀態更新 ===
                             '造成800異常 先mark Toby 0928
                             'Me.Refresh()
                     End Select

                 End Sub)


    End Sub

    Private Sub mTimer_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles mTimer.Elapsed
        'Debug.Print("mTimer.Elapsed")
        Dim mAxisX As Integer = AxisX '左右
        Dim mAxisY As Integer = AxisY '前後
        Dim mAxisZ As Integer = AxisZ '上下
        If mCoord = eCoord.Rotation Then
            mAxisX = AXisA
            mAxisY = AXisB
            mAxisZ = AXisC
        End If
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Parent)) Then
            Me.BeginInvoke(Sub()
                               txtPosX.Text = gCMotion.GetPositionValue(mAxisX)
                               txtPosY.Text = gCMotion.GetPositionValue(mAxisY)
                               txtPosZ.Text = gCMotion.GetPositionValue(mAxisZ)
                           End Sub)
        End If

        If gCMotion.MotionDone(mAxisX) = CommandStatus.Sucessed Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Parent)) Then
                Me.BeginInvoke(Sub()
                                   btnLeft.BackColor = SystemColors.Control
                                   btnRight.BackColor = SystemColors.Control
                                   btnLeft.UseVisualStyleBackColor = True
                                   btnRight.UseVisualStyleBackColor = True
                               End Sub)
            End If
        End If
        If gCMotion.MotionDone(mAxisY) = CommandStatus.Sucessed Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Parent)) Then
                Me.BeginInvoke(Sub()
                                   btnBack.BackColor = SystemColors.Control
                                   btnForward.BackColor = SystemColors.Control
                                   btnBack.UseVisualStyleBackColor = True
                                   btnForward.UseVisualStyleBackColor = True
                               End Sub)
            End If
        End If
        If gCMotion.MotionDone(mAxisZ) = CommandStatus.Sucessed Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Parent)) Then
                Me.BeginInvoke(Sub()
                                   btnUp.BackColor = SystemColors.Control
                                   btnDown.BackColor = SystemColors.Control
                                   btnUp.UseVisualStyleBackColor = True
                                   btnDown.UseVisualStyleBackColor = True
                               End Sub)
            End If
        End If

        'x y z 都到位將Timer 改成 False
        If gCMotion.MotionDone(mAxisZ) = CommandStatus.Sucessed And gCMotion.MotionDone(mAxisY) = CommandStatus.Sucessed And gCMotion.MotionDone(mAxisX) = CommandStatus.Sucessed Then
            mTimer.Enabled = False
        End If


    End Sub

    'Private Sub tmrMotorMenu_Tick(sender As Object, e As EventArgs)
    '    Select Case mCoord
    '        Case eCoord.Linear
    '            txtPosX.BeginInvoke(Sub()
    '                                    txtPosX.Text = gCMotion.GetPositionValue(AxisX)
    '                                End Sub)
    '            txtPosY.BeginInvoke(Sub()
    '                                    txtPosY.Text = gCMotion.GetPositionValue(AxisY)
    '                                End Sub)
    '            txtPosZ.BeginInvoke(Sub()
    '                                    txtPosZ.Text = gCMotion.GetPositionValue(AxisZ)
    '                                End Sub)
    '            Debug.Print("X:" & gCMotion.GetPositionValue(AxisX))
    '            'InvokeTextBox(txtPosX, gCMotion.GetPositionValue(AxisX))
    '            'InvokeTextBox(txtPosY, gCMotion.GetPositionValue(AxisY))
    '            'InvokeTextBox(txtPosZ, gCMotion.GetPositionValue(AxisZ))
    '            If gCMotion.MotionDone(AxisX) = CommandStatus.Sucessed Then
    '                btnLeft.BackColor = SystemColors.Control
    '                btnRight.BackColor = SystemColors.Control
    '                btnLeft.UseVisualStyleBackColor = True
    '                btnRight.UseVisualStyleBackColor = True
    '            End If
    '            If gCMotion.MotionDone(AxisY) = CommandStatus.Sucessed Then
    '                btnBack.BackColor = SystemColors.Control
    '                btnForward.BackColor = SystemColors.Control
    '                btnBack.UseVisualStyleBackColor = True
    '                btnForward.UseVisualStyleBackColor = True
    '            End If
    '            If gCMotion.MotionDone(AxisZ) = CommandStatus.Sucessed Then
    '                btnUp.BackColor = SystemColors.Control
    '                btnDown.BackColor = SystemColors.Control
    '                btnUp.UseVisualStyleBackColor = True
    '                btnDown.UseVisualStyleBackColor = True
    '            End If

    '        Case eCoord.Rotation
    '            InvokeTextBox(txtPosX, gCMotion.GetPositionValue(AXisA))
    '            InvokeTextBox(txtPosY, gCMotion.GetPositionValue(AXisB))
    '            InvokeTextBox(txtPosZ, gCMotion.GetPositionValue(AXisC))
    '            If gCMotion.MotionDone(AXisA) = CommandStatus.Sucessed Then
    '                btnLeft.BackColor = SystemColors.Control
    '                btnRight.BackColor = SystemColors.Control
    '                btnLeft.UseVisualStyleBackColor = True
    '                btnRight.UseVisualStyleBackColor = True
    '            End If
    '            If gCMotion.MotionDone(AXisB) = CommandStatus.Sucessed Then
    '                btnBack.BackColor = SystemColors.Control
    '                btnForward.BackColor = SystemColors.Control
    '                btnBack.UseVisualStyleBackColor = True
    '                btnForward.UseVisualStyleBackColor = True
    '            End If
    '            If gCMotion.MotionDone(AXisC) = CommandStatus.Sucessed Then
    '                btnUp.BackColor = SystemColors.Control
    '                btnDown.BackColor = SystemColors.Control
    '                btnUp.UseVisualStyleBackColor = True
    '                btnDown.UseVisualStyleBackColor = True
    '            End If

    '    End Select

    'End Sub

    Private Sub ucJoyStick_Load(sender As Object, e As EventArgs) Handles Me.Load

        grpJoyStick.Text = myResource.GetString("grpJoyStick.Text")
        btnCoordinate.Text = GetString("Linear")
        btnMode.Text = GetString("Jog")
        btnSpeed.Text = myResource.GetString("btnSpeed.Text")

        btnLeft.Enabled = IIf(AxisX = -1, False, True)
        btnRight.Enabled = IIf(AxisX = -1, False, True)
        btnBack.Enabled = IIf(AxisY = -1, False, True)
        btnForward.Enabled = IIf(AxisY = -1, False, True)
        btnUp.Enabled = IIf(AxisZ = -1, False, True)
        btnDown.Enabled = IIf(AxisZ = -1, False, True)
        mTimer = New System.Timers.Timer(100)
        mTimer.AutoReset = True
        mTimer.Enabled = False
        mTimer.Interval = 100
        'Eason 20170217 Ticket:100032 , Memory Free Part3
        'System.Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf Dispatch))
        'mThread.Start() 'Eason 20170217 Ticket:100032 , Memory Free Part3
        mTaskTokenSource = New CancellationTokenSource()
        mTaskToken = mTaskTokenSource.Token
        Task.Run(AddressOf Dispatch, mTaskToken)
    End Sub

    ''' <summary>運動模式</summary>
    ''' <remarks></remarks>
    Enum eMode
        ''' <summary>等速移動</summary>
        ''' <remarks></remarks>
        Jog = 0
        ''' <summary>等距移動</summary>
        ''' <remarks></remarks>
        Distance = 1
    End Enum
    Dim mMode As eMode

    Private Sub btnMode_Click(sender As Object, e As EventArgs) Handles btnMode.Click
        gSyslog.Save("[ucJoyStick]" & vbTab & "[btnMode]" & vbTab & "Click")
        Select Case mMode
            Case eMode.Distance '等距移動
                mMode = eMode.Jog
                btnMode.Text = GetString("Jog") '"Jog"

            Case eMode.Jog '等速移動
                mMode = eMode.Distance
                btnMode.Text = GetString("Dis") '"Dis"
        End Select
        SetMode()
    End Sub


    Public Function GetString(ByVal value As String) As String
        Select Case value
            Case "Jog"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Jog"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "等速"
                    Case enmLanguageType.eTraditionalChinese
                        Return "等速"
                End Select
            Case "Dis"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Dis"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "寸动"
                    Case enmLanguageType.eTraditionalChinese
                        Return "吋動"
                End Select
            Case "Rotate"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Rotate"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "旋转"
                    Case enmLanguageType.eTraditionalChinese
                        Return "旋轉"
                End Select
            Case "Linear"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Linear"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "平移"
                    Case enmLanguageType.eTraditionalChinese
                        Return "平移"
                End Select
        End Select
        Return ""
    End Function

#Region "座標系切換"
    ''' <summary>座標系</summary>
    ''' <remarks></remarks>
    Enum eCoord
        ''' <summary>直角座標系</summary>
        ''' <remarks></remarks>
        Linear = 0
        ''' <summary>旋轉座標系</summary>
        ''' <remarks></remarks>
        Rotation = 1
    End Enum

    '<summary>決定目前對應的是XYZ的直角坐標系,或是ABC的旋轉座標系</summary>
    'Toby add  test_預設
    Dim mCoord As eCoord = eCoord.Linear
    Private Sub btnCoordinate_Click(sender As Object, e As EventArgs) Handles btnCoordinate.Click
        'Toby add  test
        If AXisB = -1 Then
            Exit Sub
        End If


        Select Case mCoord
            Case eCoord.Linear
                mCoord = eCoord.Rotation
                btnCoordinate.Text = GetString("Rotate")
                btnLeft.Text = "A"
                btnRight.Text = "A"
                btnBack.Text = "B"
                btnForward.Text = "B"
                btnUp.Text = "C"
                btnDown.Text = "C"
                lblAxis0.Text = "A"
                lblAxis1.Text = "B"
                lblAxis2.Text = "C"
                lblAxis0Unit.Text = myResource.GetString("Degree") ' "Deg"
                lblAxis1Unit.Text = myResource.GetString("Degree") '"Deg"
                lblAxis2Unit.Text = myResource.GetString("Degree") '"Deg"

                btnLeft.Enabled = IIf(AXisA = -1, False, True)
                btnRight.Enabled = IIf(AXisA = -1, False, True)
                btnBack.Enabled = IIf(AXisB = -1, False, True)
                btnForward.Enabled = IIf(AXisB = -1, False, True)
                btnUp.Enabled = IIf(AXisC = -1, False, True)
                btnDown.Enabled = IIf(AXisC = -1, False, True)

            Case eCoord.Rotation
                mCoord = eCoord.Linear
                btnCoordinate.Text = GetString("Linear")
                btnLeft.Text = "X"
                btnRight.Text = "X"
                btnBack.Text = "Y"
                btnForward.Text = "Y"
                btnUp.Text = "Z"
                btnDown.Text = "Z"
                lblAxis0.Text = "X"
                lblAxis1.Text = "Y"
                lblAxis2.Text = "Z"
                lblAxis0Unit.Text = "mm"
                lblAxis1Unit.Text = "mm"
                lblAxis2Unit.Text = "mm"
                btnLeft.Enabled = IIf(AxisX = -1, False, True)
                btnRight.Enabled = IIf(AxisX = -1, False, True)
                btnBack.Enabled = IIf(AxisY = -1, False, True)
                btnForward.Enabled = IIf(AxisY = -1, False, True)
                btnUp.Enabled = IIf(AxisZ = -1, False, True)
                btnDown.Enabled = IIf(AxisZ = -1, False, True)
        End Select
        RefreshPosition()
    End Sub

#End Region


    Public Sub btnGo1_Click(sender As Object, e As EventArgs) Handles btnGo1.Click
        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = AxisX
        AxisNo(1) = AxisY
        AxisNo(2) = AxisZ
        AxisNo(3) = AXisB
        AxisNo(4) = AXisC

        TargetPos(0) = Val(txtPosX.Text)
        TargetPos(1) = Val(txtPosY.Text)
        TargetPos(2) = Val(txtPosZ.Text)
        TargetPos(3) = 0
        TargetPos(4) = 0


        If (Val(txtPosX.Text) > gCMotion.AxisParameter(AxisX).Limit.PosivtiveLimit) Or (Val(txtPosX.Text) < gCMotion.AxisParameter(AxisX).Limit.NegativeLimit) Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000029))
            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            '讓timer 更新位置
            If Not mTimer Is Nothing Then
                mTimer.Enabled = True
            End If
            Exit Sub
        End If
        If (Val(txtPosY.Text) > gCMotion.AxisParameter(AxisY).Limit.PosivtiveLimit) Or (Val(txtPosY.Text) < gCMotion.AxisParameter(AxisY).Limit.NegativeLimit) Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000029))
            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            '讓timer 更新位置
            If Not mTimer Is Nothing Then
                mTimer.Enabled = True
            End If
            Exit Sub
        End If
        If (Val(txtPosZ.Text) > gCMotion.AxisParameter(AxisZ).Limit.PosivtiveLimit) Or (Val(txtPosZ.Text) < gCMotion.AxisParameter(AxisZ).Limit.NegativeLimit) Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000029))
            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            '讓timer 更新位置
            If Not mTimer Is Nothing Then
                mTimer.Enabled = True
            End If
            Exit Sub
        End If
        ButtonSafeMovePos(sender, AxisNo, TargetPos, Nothing)
    End Sub

    Dim mStopWatch As New Stopwatch
    ''' <summary>按鍵用 安全移動 Z軸先到0再水平移動 最後再動Z</summary>
    ''' <param name="sender"></param>
    ''' <remarks>AxisNo的順序必須是X,Y,Z,B,C且X,Y,Z必備</remarks>
    Public Sub ButtonSafeMovePos(ByRef sender As Button, ByVal AxisNo() As Integer, ByVal TargetPos() As Decimal, ByVal sys As sSysParam)
        Dim isINP(4) As Boolean
        'Const TIME_OUT_IN_MS As Integer = 5000
        If AxisNo.Count > 5 Then
            MsgBox("ButtonSafeMovePos Not Supports.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If AxisNo.Count < 5 Then
            MsgBox("ButtonSafeMovePos Not Supports.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not sys Is Nothing Then
            If sys.Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                '請先復歸
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005))
                MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Exit Sub
            End If
        End If
        Dim btn As Button = CType(sender, Button)
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Parent)) Then
            Me.BeginInvoke(Sub()
                               btn.BackColor = Color.Yellow '按鍵顏色
                           End Sub)
        End If

        Task.Run(Sub()

                     Dim StartPos(AxisNo.Count - 1) As Decimal

                     For i As Integer = 0 To AxisNo.Count - 1
                         StartPos(i) = gCMotion.GetPositionValue(AxisNo(i))
                     Next

                     Dim isTheSamePosition As Boolean = True
                     For i As Integer = 0 To AxisNo.Count - 1
                         If Math.Abs(TargetPos(i) - StartPos(i)) > 0.001 Then '任一軸座標不相等
                             isTheSamePosition = False
                             Exit For
                         End If
                     Next
                     If isTheSamePosition Then
                         '20170929 Toby_ Add 判斷
                         If (Not IsNothing(Parent)) Then
                             Me.BeginInvoke(Sub()
                                                btn.BackColor = SystemColors.Control
                                                btn.UseVisualStyleBackColor = True
                                            End Sub)
                         End If
                         gSyslog.Save("Target Position = Current Position.")
                         Exit Sub
                     End If
                     Debug.Print(".AxisParameter.Count:" & gCMotion.AxisParameter.Count)
                     For i As Integer = 0 To AxisNo.Count - 1
                         If AxisNo(i) < gCMotion.AxisParameter.Count Then
                             If AxisNo(i) >= 0 Then
                                 gCMotion.SetVelHigh(AxisNo(i), gSSystemParameter.ManualVelHigh) 'gCMotion.AxisParameter(AxisNo(i)).Velocity.VelHigh)
                                 gCMotion.SetVelLow(AxisNo(i), gCMotion.AxisParameter(AxisNo(i)).Velocity.VelLow)
                                 gCMotion.SetAcc(AxisNo(i), gCMotion.AxisParameter(AxisNo(i)).Velocity.Acc * gCMotion.AxisParameter(AxisNo(i)).Velocity.AccRatio)
                                 gCMotion.SetDec(AxisNo(i), gCMotion.AxisParameter(AxisNo(i)).Velocity.Dec * gCMotion.AxisParameter(AxisNo(i)).Velocity.DecRatio)
                             End If

                         End If
                     Next

                     If gCMotion.AbsMove(AxisNo(2), 0) <> CommandStatus.Sucessed Then 'Z上飛高
                         '20170929 Toby_ Add 判斷
                         If (Not IsNothing(Parent)) Then
                             Me.BeginInvoke(Sub()
                                                btn.BackColor = Color.Red
                                            End Sub)
                         End If
                         'Z軸移動失敗
                         Select Case sys.StageNo
                             Case 0
                                 gSyslog.Save(gMsgHandler.GetMessage(Error_1032000))
                                 MsgBox(gMsgHandler.GetMessage(Error_1032000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                             Case 1
                                 gSyslog.Save(gMsgHandler.GetMessage(Error_1044000))
                                 MsgBox(gMsgHandler.GetMessage(Error_1044000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                             Case 2
                                 gSyslog.Save(gMsgHandler.GetMessage(Error_1062000))
                                 MsgBox(gMsgHandler.GetMessage(Error_1062000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                             Case 3
                                 gSyslog.Save(gMsgHandler.GetMessage(Error_1069000))
                                 MsgBox(gMsgHandler.GetMessage(Error_1069000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                         End Select
                         'MsgBox("Z軸移動到0,失敗!")
                         Exit Sub
                     End If
                     gSyslog.Save(gCMotion.AxisParameter(AxisNo(2)).AxisName & " Move to 0")
                     System.Threading.Thread.CurrentThread.Join(100) '移動命令下達後不能立刻看到位
                     mStopWatch.Restart()
                     Do

                         System.Threading.Thread.CurrentThread.Join(1)
                         If gCMotion.MotionDone(AxisNo(2)) = CommandStatus.Sucessed Then '到位 離開等待迴圈
                             Exit Do
                         End If
                         If AxisNo(2) > -1 Then
                             If gCMotion.IsMoveTimeOut(AxisNo(2)) Then '逾時 中斷離開
                                 '20170929 Toby_ Add 判斷
                                 If (Not IsNothing(Parent)) Then
                                     Me.BeginInvoke(Sub()
                                                        btn.BackColor = Color.Red
                                                    End Sub)
                                 End If

                                 gEqpMsg.Add("ChangeGlueAction", Error_1032004, eMessageLevel.Error) '移動逾時
                                 MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                                 Exit Sub
                             End If
                         End If
                     Loop
                     gSyslog.Save(gCMotion.AxisParameter(AxisNo(2)).AxisName & " Move OK.")

                     For i As Integer = 3 To AxisNo.Count - 1
                         If gCMotion.AbsMove(AxisNo(i), TargetPos(i)) <> CommandStatus.Sucessed Then
                             MsgBox(gCMotion.AxisParameter(AxisNo(i)).AxisName & " AbsMove Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                             Exit Sub
                         End If
                     Next
                     mStopWatch.Restart()
                     System.Threading.Thread.CurrentThread.Join(100) '移動命令下達後不能立刻看到位

                     Do
                         System.Threading.Thread.CurrentThread.Join(1)
                         isINP(3) = True 'Assume INP is Ready
                         isINP(4) = True
                         If AxisNo(3) > -1 Then 'B軸存在
                             If gCMotion.MotionDone(AxisNo(3)) <> CommandStatus.Sucessed Then
                                 isINP(3) = False 'Axis is Not Inposition
                                 If gCMotion.IsMoveTimeOut(AxisNo(3)) = True Then '到位逾時
                                     '20170929 Toby_ Add 判斷
                                     If (Not IsNothing(Parent)) Then
                                         Me.BeginInvoke(Sub()
                                                            btn.BackColor = Color.Red
                                                        End Sub)
                                     End If

                                     gSyslog.Save(gCMotion.AxisParameter(AxisNo(3)).AxisName & " Move Time Out!", , eMessageLevel.Warning)
                                     MsgBox(gCMotion.AxisParameter(AxisNo(3)).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                                     Exit Sub
                                 End If
                             End If
                         End If
                         If AxisNo(4) > -1 Then 'C軸存在
                             If gCMotion.MotionDone(AxisNo(4)) <> CommandStatus.Sucessed Then
                                 isINP(4) = False 'Axis is Not Inposition
                                 If gCMotion.IsMoveTimeOut(AxisNo(4)) Then '到位逾時
                                     '20170929 Toby_ Add 判斷
                                     If (Not IsNothing(Parent)) Then
                                         Me.BeginInvoke(Sub()
                                                            btn.BackColor = Color.Red
                                                        End Sub)
                                     End If

                                     gSyslog.Save(gCMotion.AxisParameter(AxisNo(4)).AxisName & " Move Time Out!", , eMessageLevel.Warning)
                                     MsgBox(gCMotion.AxisParameter(AxisNo(4)).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                                 End If
                             End If

                         End If
                         If isINP(3) And isINP(4) Then '到位 離開等待迴圈
                             Exit Do
                         End If

                     Loop

                     If gCMotion.AbsMove(AxisNo(0), TargetPos(0)) <> CommandStatus.Sucessed Then
                         '20170929 Toby_ Add 判斷
                         If (Not IsNothing(Parent)) Then
                             Me.BeginInvoke(Sub()
                                                btn.BackColor = Color.Red
                                            End Sub)
                         End If

                         MsgBox("X軸移動到" & TargetPos(0) & "失敗!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                         Exit Sub
                     End If
                     If gCMotion.AbsMove(AxisNo(1), TargetPos(1)) <> CommandStatus.Sucessed Then '水平移動
                         '20170929 Toby_ Add 判斷
                         If (Not IsNothing(Parent)) Then
                             Me.BeginInvoke(Sub()
                                                btn.BackColor = Color.Red
                                            End Sub)
                         End If

                         MsgBox("Y軸移動到" & TargetPos(1) & "失敗!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                         Exit Sub
                     End If
                     gSyslog.Save(gCMotion.AxisParameter(AxisNo(0)).AxisName & " Move To (" & TargetPos(0) & ")")
                     gSyslog.Save(gCMotion.AxisParameter(AxisNo(1)).AxisName & " Move To (" & TargetPos(1) & ")")
                     mStopWatch.Restart()
                     System.Threading.Thread.CurrentThread.Join(100) '移動命令下達後不能立刻看到位
                     Do
                         System.Threading.Thread.CurrentThread.Join(1)
                         isINP(0) = True 'Assume INP is Ready
                         isINP(1) = True
                         If AxisNo(0) > -1 Then 'B軸存在
                             If gCMotion.MotionDone(AxisNo(0)) <> CommandStatus.Sucessed Then
                                 isINP(0) = False 'Axis is Not Inposition
                                 If gCMotion.IsMoveTimeOut(AxisNo(0)) = True Then '到位逾時
                                     '20170929 Toby_ Add 判斷
                                     If (Not IsNothing(Parent)) Then
                                         Me.BeginInvoke(Sub()
                                                            btn.BackColor = Color.Red
                                                        End Sub)
                                     End If

                                     gSyslog.Save(gCMotion.AxisParameter(AxisNo(0)).AxisName & " Move Time Out!", , eMessageLevel.Warning)
                                     MsgBox(gCMotion.AxisParameter(AxisNo(0)).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                                     Exit Sub
                                 End If
                             End If
                         End If
                         If AxisNo(1) > -1 Then 'C軸存在
                             If gCMotion.MotionDone(AxisNo(1)) <> CommandStatus.Sucessed Then
                                 isINP(1) = False 'Axis is Not Inposition
                                 If gCMotion.IsMoveTimeOut(AxisNo(1)) Then '到位逾時
                                     '20170929 Toby_ Add 判斷
                                     If (Not IsNothing(Parent)) Then
                                         Me.BeginInvoke(Sub()
                                                            btn.BackColor = Color.Red
                                                        End Sub)
                                     End If

                                     gSyslog.Save(gCMotion.AxisParameter(AxisNo(1)).AxisName & " Move Time Out!", , eMessageLevel.Warning)
                                     MsgBox(gCMotion.AxisParameter(AxisNo(1)).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                                 End If
                             End If

                         End If
                         If isINP(0) And isINP(1) Then '到位 離開等待迴圈
                             Exit Do
                         End If
                     Loop
                     gSyslog.Save(gCMotion.AxisParameter(enmAxis.XAxis).AxisName & " Move OK.")
                     gSyslog.Save(gCMotion.AxisParameter(enmAxis.Y1Axis).AxisName & " Move OK.")
                     'XY到位
                     If gCMotion.AbsMove(AxisNo(2), TargetPos(2)) <> CommandStatus.Sucessed Then 'Z軸下降 then
                         '20170929 Toby_ Add 判斷
                         If (Not IsNothing(Parent)) Then
                             Me.BeginInvoke(Sub()
                                                btn.BackColor = Color.Red
                                            End Sub)
                         End If
                         MsgBox("Z軸移動到" & TargetPos(2) & "失敗!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                         Exit Sub
                     End If
                     gSyslog.Save(gCMotion.AxisParameter(AxisNo(2)).AxisName & " Move To (" & TargetPos(2) & ")")
                     System.Threading.Thread.CurrentThread.Join(100) '移動命令下達後不能立刻看到位
                     mStopWatch.Restart()
                     Do
                         System.Threading.Thread.CurrentThread.Join(1)
                         isINP(2) = True
                         If AxisNo(2) > -1 Then 'C軸存在
                             If gCMotion.MotionDone(AxisNo(2)) <> CommandStatus.Sucessed Then
                                 isINP(2) = False 'Axis is Not Inposition
                                 If gCMotion.IsMoveTimeOut(AxisNo(2)) Then '到位逾時
                                     '20170929 Toby_ Add 判斷
                                     If (Not IsNothing(Parent)) Then
                                         Me.BeginInvoke(Sub()
                                                            btn.BackColor = Color.Red
                                                        End Sub)
                                     End If

                                     gSyslog.Save(gCMotion.AxisParameter(AxisNo(2)).AxisName & " Move Time Out!", , eMessageLevel.Warning)
                                     MsgBox(gCMotion.AxisParameter(AxisNo(2)).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                                 End If
                             End If

                         End If
                         If isINP(2) Then '到位 離開等待迴圈
                             Exit Do
                         End If
                     Loop
                     gSyslog.Save(gCMotion.AxisParameter(AxisNo(2)).AxisName & " Move OK.")
                     '動作完成
                     '20170929 Toby_ Add 判斷
                     If (Not IsNothing(Parent)) Then
                         Me.BeginInvoke(Sub()
                                            btn.BackColor = SystemColors.Control
                                            btn.UseVisualStyleBackColor = True
                                        End Sub)
                     End If

                 End Sub)

    End Sub


    Private Sub txtPosX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPosX.KeyPress, txtPosZ.KeyPress, txtPosY.KeyPress
        If Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Chr(8) Or e.KeyChar = Convert.ToChar(3) Or e.KeyChar = Convert.ToChar(22) Or e.KeyChar = Convert.ToChar(24) Then

            If e.KeyChar = "." And InStr(sender.Text, ".") > 0 Then

                e.Handled = True
            Else
                e.Handled = False


            End If

        ElseIf e.KeyChar = "-" And InStr(sender.Text, "-") = 0 Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    'Eason 20170120 Ticket:100030 , Memory Freed [S]
    Public Sub ManualDispose()

        Try
            If Not mTimer Is Nothing Then
                mTimer.Dispose()
            End If
            mIsDisposing = True
            mAutoWait.Set()
            'mThread.Abort()
            mTaskTokenSource.Cancel()
        Catch ex As Exception
            Dim strException As String = ex.ToString 'Only Debug view
            'Throw ex 'If need throw Exception , Re-Mark it
        Finally
            'mThread = Nothing
            mAutoWait.Dispose()
            mAutoWait = Nothing
            CmdQueue.Clear()
        End Try

    End Sub
    'Eason 20170120 Ticket:100030 , Memory Freed [E]



End Class

''' <summary>
''' 干涉軸相關參數
''' </summary>
''' <remarks></remarks>
Public Class InverseAxis

    Enum enmDirection
        Posivtive
        Negative
    End Enum

    ''' <summary>[軸號]</summary>
    ''' <remarks></remarks>
    Public Property Axis As Integer = -1
    ''' <summary>[干涉方向]</summary>
    ''' <remarks></remarks>
    Public Property Direction As enmDirection = enmDirection.Posivtive
    ''' <summary>[單機行程大小]</summary>
    ''' <remarks></remarks>
    Public Property Spread As Decimal = -1
    ''' <summary>
    ''' [安全距離，意旨對立軸須相對多少為安全距離]
    ''' </summary>
    ''' <remarks></remarks>
    Public Property SafeDistance As Decimal = -1

End Class
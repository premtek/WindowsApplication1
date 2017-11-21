Imports ProjectCore
Imports ProjectMotion
Imports ProjectIO
Imports ProjectTriggerBoard
Imports ProjectLaserInterferometer


Public Class frmTilt
    Private Enum ButtonCmd
        Home = 0
        Go = 1
        JogP = 2
        JogN = 3
        Stp = 4
        None = 5
    End Enum
    ''' <summary>
    '''移動計時
    ''' </summary>
    ''' <remarks></remarks>
    Private iTimer As Integer = 6000
    ''' <summary>
    ''' 介面馬達選擇
    ''' </summary>
    ''' <remarks></remarks>
    Private iButtonSelect As Integer = -1
    ''' <summary>
    ''' 實際對應馬達軸
    ''' </summary>
    ''' <remarks></remarks>
    Private iAxis As Integer = -1
    ''' <summary>
    ''' 移動命令
    ''' </summary>
    ''' <remarks></remarks>
    Private eCmd As ButtonCmd = ButtonCmd.None
    ''' <summary>
    ''' Go動作Index
    ''' </summary>
    ''' <remarks></remarks>
    Private iGoActionIndex As Integer = -1
    ''' <summary>
    ''' Home動作Index
    ''' </summary>
    ''' <remarks></remarks>
    Private iHomeActionIndex As Integer = -1
    ''' <summary>
    ''' 按鍵防護
    ''' </summary>
    ''' <remarks></remarks>
    Private bButtonLock As Boolean = False
    ''' <summary>速度形式</summary>
    ''' <remarks></remarks>
    Private mSpeedType As SpeedType
    ''' <summary>
    ''' 動作計時器
    ''' </summary>
    ''' <remarks></remarks>
    Private stopwatcher As Stopwatch = New Stopwatch
    ''' <summary>
    ''' 馬達設定軸
    ''' </summary>
    ''' <remarks></remarks>
    Private motorProtype As TiltMotor

    Private temp As clsWetcoTilt
    Public Sub New()
        ' 此為設計工具所需的呼叫。
        InitializeComponent()
        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        temp = New clsWetcoTilt
    End Sub

    Private Sub frmTitl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private mTiltSelect As enmTiltIndex
    Public Property _TiltSelect() As enmTiltIndex
        Get
            Return mTiltSelect
        End Get
        Set(ByVal value As enmTiltIndex)
            mTiltSelect = value
            Me.Show()
        End Set
    End Property

    Private Sub btnSet1_Click(sender As Object, e As EventArgs) Handles btnSet1.Click, Button5.Click, Button4.Click, Button3.Click, Button2.Click
        Dim readerValue As String = ""
        Dim sPos As TiltPos = New TiltPos
        Dim iTemp As Integer = DirectCast(sender, Button).Tag
        Select Case iTemp
            Case 1 '0度
                txtX0.Text = Val(gCMotion.GetPositionValue(temp._NowTilt._mTiltMotor.X))
                txtY0.Text = Val(gCMotion.GetPositionValue(temp._NowTilt._mTiltMotor.Y))
                gLaserReaderCollection.GetValue(0, readerValue)
                txtZ0.Text = readerValue
                sPos.dTiltPosX0 = Val(gCMotion.GetPositionValue(temp._NowTilt._mTiltMotor.X))
                sPos.dTiltPosY0 = Val(gCMotion.GetPositionValue(temp._NowTilt._mTiltMotor.Y))
                sPos.dTiltPosZ0 = readerValue
                sPos.dWorkDegrees0 = Val(gCMotion.GetPositionValue(temp._NowTilt._mTiltMotor.U))
            Case 2
                txtX1.Text = Val(gCMotion.GetPositionValue(temp._NowTilt._mTiltMotor.X))
                txtY1.Text = Val(gCMotion.GetPositionValue(temp._NowTilt._mTiltMotor.Y))
                gLaserReaderCollection.GetValue(0, readerValue)
                txtZ1.Text = readerValue
            Case 3
                txtX2.Text = Val(gCMotion.GetPositionValue(temp._NowTilt._mTiltMotor.X))
                txtY2.Text = Val(gCMotion.GetPositionValue(temp._NowTilt._mTiltMotor.Y))
                gLaserReaderCollection.GetValue(0, readerValue)
                txtZ2.Text = readerValue
            Case 4
                txtX3.Text = Val(gCMotion.GetPositionValue(temp._NowTilt._mTiltMotor.X))
                txtY3.Text = Val(gCMotion.GetPositionValue(temp._NowTilt._mTiltMotor.Y))
                gLaserReaderCollection.GetValue(0, readerValue)
                txtZ3.Text = readerValue
            Case 5
                txtX4.Text = Val(gCMotion.GetPositionValue(temp._NowTilt._mTiltMotor.X))
                txtY4.Text = Val(gCMotion.GetPositionValue(temp._NowTilt._mTiltMotor.Y))
                gLaserReaderCollection.GetValue(0, readerValue)
                txtZ4.Text = readerValue
        End Select

    End Sub

    Private Sub btnGetOffset_Click(sender As Object, e As EventArgs) Handles btnGetOffset.Click

        txtOffsetX1.Text = Val(txtX0.Text) - Val(txtX1.Text)
        txtOffsetY1.Text = Val(txtY0.Text) - Val(txtY1.Text)
        txtOffsetZ1.Text = Val(txtZ0.Text) - Val(txtZ1.Text)

        txtOffsetX2.Text = Val(txtX0.Text) - Val(txtX2.Text)
        txtOffsetY2.Text = Val(txtY0.Text) - Val(txtY2.Text)
        txtOffsetZ2.Text = Val(txtZ0.Text) - Val(txtZ2.Text)

        txtOffsetX3.Text = Val(txtX0.Text) - Val(txtX3.Text)
        txtOffsetY3.Text = Val(txtY0.Text) - Val(txtY3.Text)
        txtOffsetZ4.Text = Val(txtZ0.Text) - Val(txtZ3.Text)

        txtOffsetX4.Text = Val(txtX0.Text) - Val(txtX4.Text)
        txtOffsetY4.Text = Val(txtY0.Text) - Val(txtY4.Text)
        txtOffsetZ4.Text = Val(txtZ0.Text) - Val(txtZ4.Text)

        txtOffsetX5.Text = Val(txtX0.Text) - Val(txtX5.Text)
        txtOffsetY5.Text = Val(txtY0.Text) - Val(txtY5.Text)
        txtOffsetZ5.Text = Val(txtZ0.Text) - Val(txtZ5.Text)
    End Sub

    Private Sub frmTilt_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim tPos As TiltPos = New TiltPos
        tPos = temp.LoadPos(mTiltSelect)
        SetMotorComponent()
        eCmd = ButtonCmd.None
        iGoActionIndex = -1
        iHomeActionIndex = -1
        bButtonLock = False
        tmrCmd.Stop()
        tmrStatus.Start()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles txtTest.Click
        temp.Motion(enmTiltMotion.Initial, mTiltSelect)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim tPos As TiltPos = New TiltPos
        tPos.dTiltPosX0 = Val(txtX0.Text)
        tPos.dTiltPosX1 = Val(txtX1.Text)
        tPos.dTiltPosX2 = Val(txtX2.Text)
        tPos.dTiltPosX3 = Val(txtX3.Text)
        tPos.dTiltPosX4 = Val(txtX4.Text)
        tPos.dTiltPosX5 = Val(txtX5.Text)

        tPos.dTiltPosY0 = Val(txtY0.Text)
        tPos.dTiltPosY1 = Val(txtY1.Text)
        tPos.dTiltPosY2 = Val(txtY2.Text)
        tPos.dTiltPosY3 = Val(txtY3.Text)
        tPos.dTiltPosY4 = Val(txtY4.Text)
        tPos.dTiltPosY5 = Val(txtY5.Text)

        tPos.dTiltPosZ0 = Val(txtZ0.Text)
        tPos.dTiltPosZ1 = Val(txtZ1.Text)
        tPos.dTiltPosZ2 = Val(txtZ2.Text)
        tPos.dTiltPosZ3 = Val(txtZ3.Text)
        tPos.dTiltPosZ4 = Val(txtZ4.Text)
        tPos.dTiltPosZ5 = Val(txtZ5.Text)

        tPos.dWorkDegrees0 = Val(txtWorkU0.Text)
        tPos.dWorkDegrees1 = Val(txtWorkU1.Text)
        tPos.dWorkDegrees2 = Val(txtWorkU2.Text)
        tPos.dWorkDegrees3 = Val(txtWorkU3.Text)
        tPos.dWorkDegrees4 = Val(txtWorkU4.Text)
        tPos.dWorkDegrees5 = Val(txtWorkU5.Text)
        temp.SavePos(mTiltSelect, tPos)
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim sTilt As TiltPos = New TiltPos
        sTilt = temp.LoadPos(mTiltSelect)

    End Sub

    Public Sub SetMotorComponent()
        motorProtype = New TiltMotor
        motorProtype = temp._NowTilt._mTiltMotor
        'UcJoyStick1.AxisX = motorProtype.X
        'UcJoyStick1.AxisY = motorProtype.Y
        'UcJoyStick1.AxisZ = motorProtype.Z
        'UcJoyStick1.AXisA = motorProtype.U
    End Sub
   
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        eCmd = ButtonCmd.Go
        iGoActionIndex = 1
        stopwatcher.Start()
        tmrCmd.Start()
    End Sub
    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        eCmd = ButtonCmd.Stp
        gCMotion.SlowStop(iAxis, 100)
    End Sub
   
    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        eCmd = ButtonCmd.Home
        iHomeActionIndex = 1
        stopwatcher.Start()
        tmrCmd.Start()
    End Sub
    Private Sub tmrStatus_Tick(sender As Object, e As EventArgs) Handles tmrStatus.Tick
        Select Case eCmd
            Case ButtonCmd.Go
                btnGo.Enabled = False
                btnHome.Enabled = False
                btnJogN.Enabled = False
                btnJogP.Enabled = False
                btnGo.BackColor = Color.Chartreuse
            Case ButtonCmd.Home
                btnGo.Enabled = False
                btnHome.Enabled = False
                btnJogN.Enabled = False
                btnJogP.Enabled = False
                btnHome.BackColor = Color.Chartreuse
            Case ButtonCmd.JogN
                btnJogN.BackColor = Color.Chartreuse
            Case ButtonCmd.JogP
                btnJogP.BackColor = Color.Chartreuse
            Case ButtonCmd.Stp, ButtonCmd.None
                btnGo.Enabled = True
                btnHome.Enabled = True
                btnJogN.Enabled = True
                btnJogP.Enabled = True
                btnJogN.BackColor = DefaultBackColor
                btnJogP.BackColor = DefaultBackColor
                btnGo.BackColor = DefaultBackColor
                btnHome.BackColor = DefaultBackColor
        End Select
        Select Case iButtonSelect
            Case 1
                btnX.BackColor = Color.Chartreuse
                btnY.BackColor = DefaultBackColor
                btnZ.BackColor = DefaultBackColor
                btnU.BackColor = DefaultBackColor
                iAxis = motorProtype.X
            Case 2
                btnX.BackColor = DefaultBackColor
                btnY.BackColor = Color.Chartreuse
                btnZ.BackColor = DefaultBackColor
                btnU.BackColor = DefaultBackColor
                iAxis = motorProtype.Y
            Case 3
                btnX.BackColor = DefaultBackColor
                btnY.BackColor = DefaultBackColor
                btnZ.BackColor = Color.Chartreuse
                btnU.BackColor = DefaultBackColor
                iAxis = motorProtype.Z
            Case 4
                btnX.BackColor = DefaultBackColor
                btnY.BackColor = DefaultBackColor
                btnZ.BackColor = DefaultBackColor
                btnU.BackColor = Color.Chartreuse
                iAxis = motorProtype.U
        End Select
        If iButtonSelect > 0 And iButtonSelect < 5 Then
            SetSensorBackColor(lblNel, gCMotion.AxisParameter(iAxis).MotionIOStatus.blnNEL, False)
            SetSensorBackColor(lblPel, gCMotion.AxisParameter(iAxis).MotionIOStatus.blnPEL, False)
            SetSensorBackColor(lblOrg, gCMotion.AxisParameter(iAxis).MotionIOStatus.blnORG, False)
            SetSensorBackColor(lblALM, gCMotion.AxisParameter(iAxis).MotionIOStatus.blnALM, False)
            SetSensorBackColor(lblBusy, gCMotion.AxisParameter(iAxis).MotionIOStatus.blnRDY, False)
        End If
    End Sub
    
    Private Sub tmrCmd_Tick(sender As Object, e As EventArgs) Handles tmrCmd.Tick
        Select Case eCmd
            ''GO
            Case ButtonCmd.Go
                Select Case iGoActionIndex
                    Case 1
                        If gCMotion.AbsMove(iAxis, Val(txtPos.Text)) = CommandStatus.Sucessed Then
                            iGoActionIndex = 2
                        Else
                            'Alarm
                        End If
                    Case 2
                        If gCMotion.MotionDone(iAxis) = CommandStatus.Sucessed Then
                            iGoActionIndex = 999
                        Else
                            iGoActionIndex = 3
                        End If
                    Case 3
                        If TimeOut(iTimer) = True Then
                            'Alarm time out
                            gCMotion.SlowStop(iAxis, 100)
                            tmrCmd.Stop()
                        Else
                            iGoActionIndex = 2
                        End If
                    Case 999
                        eCmd = ButtonCmd.None
                        tmrCmd.Stop()
                End Select
                'Home
            Case ButtonCmd.Home
                Select Case iHomeActionIndex
                    Case 1
                        If gCMotion.Home(iAxis) = CommandStatus.Sucessed Then
                            iHomeActionIndex = 2
                        Else
                            'alarm  
                        End If
                    Case 2
                        If gCMotion.HomeFinish(iAxis) = CommandStatus.Sucessed Then
                            iHomeActionIndex = 999
                        Else
                            iHomeActionIndex = 3
                            'alarm
                        End If
                    Case 3
                        If TimeOut(iTimer) = True Then
                            'Alarm time out
                            gCMotion.SlowStop(iAxis, 100)
                            tmrCmd.Stop()
                        Else
                            iGoActionIndex = 2
                        End If
                    Case 999
                        eCmd = ButtonCmd.None
                        tmrCmd.Stop()
                End Select
            Case ButtonCmd.JogN

            Case ButtonCmd.JogP
            Case ButtonCmd.Stp
                If gCMotion.SlowStop(iAxis, 100) = CommandStatus.Sucessed Then
                Else
                    'alarm
                End If
        End Select
    End Sub
    Private Function TimeOut(ByVal time As Integer) As Boolean
        If (stopwatcher.ElapsedMilliseconds > time) Then
            Return True
        End If
        Return False
    End Function

    Private Sub frmTilt_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        tmrCmd.Enabled = False
        tmrStatus.Enabled = False
    End Sub

    Private Sub btnAxisSelect_Click(sender As Object, e As EventArgs) Handles btnAxisSelect.Click, btnX.Click, btnZ.Click, btnY.Click, btnU.Click
        iButtonSelect = DirectCast(sender, Button).Tag

    End Sub

    ''' <summary>Sensor標籤背景色套用</summary>
    ''' <param name="refLabel"></param>
    ''' <param name="status"></param>
    ''' <param name="isPass"></param>
    ''' <remarks></remarks>
    Public Sub SetSensorBackColor(ByRef refLabel As System.Windows.Forms.Label, ByVal status As Boolean, ByVal isPass As Boolean)
        If isPass Then
            refLabel.BackColor = System.Drawing.Color.Gray
            Exit Sub
        End If
        If status Then
            refLabel.BackColor = System.Drawing.Color.Red
        Else
            refLabel.BackColor = System.Drawing.Color.White
        End If
    End Sub

    Private Sub btnSpeed_Click(sender As Object, e As EventArgs) Handles btnSpeed.Click
        'gSyslog.Save("[frmMotionOp]" & vbTab & "[btnSpeed]" & vbTab & "Click")
        Select Case mSpeedType
            Case SpeedType.Fast
                mSpeedType = SpeedType.Slow
                'btnSpeed.Text = "Slow"

                SetSpeedType(iAxis, mSpeedType)
            Case SpeedType.Medium
                mSpeedType = SpeedType.Fast

                SetSpeedType(iAxis, mSpeedType)
            Case SpeedType.Slow
                mSpeedType = SpeedType.Medium

                SetSpeedType(iAxis, mSpeedType)
        End Select
    End Sub
    ''' <summary>速度形式設定</summary>
    ''' <param name="axisIndex"></param>
    ''' <param name="speed"></param>
    ''' <remarks></remarks>
    Public Sub SetSpeedType(ByVal axisIndex As Integer, ByVal speed As SpeedType)
        Select Case speed
            Case SpeedType.Fast
                btnSpeed.Image = My.Resources._1_1
                'btnSpeed.Text = "Fast"
                btnSpeed.BackColor = Color.Orange
                Do
                    Application.DoEvents()
                Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed
                gCMotion.SetVelHigh(axisIndex, 100)
                Do
                    Application.DoEvents()
                Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed
                gCMotion.SetAcc(axisIndex, 100)
                Do
                    Application.DoEvents()
                Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed
                gCMotion.SetDec(axisIndex, 100)
                Do
                    Application.DoEvents()
                Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed

            Case SpeedType.Medium
                btnSpeed.Image = My.Resources._1_2
                'btnSpeed.Text = "Midden"
                btnSpeed.BackColor = Color.Yellow
                Do
                    Application.DoEvents()
                Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed
                gCMotion.SetVelHigh(axisIndex, 10)
                Do
                    Application.DoEvents()
                Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed
                gCMotion.SetAcc(axisIndex, 10)
                Do
                    Application.DoEvents()
                Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed
                gCMotion.SetDec(axisIndex, 10)
                Do
                    Application.DoEvents()
                Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed
            Case SpeedType.Slow
                btnSpeed.Image = My.Resources._1_1
                btnSpeed.BackColor = Color.Green
                Do
                    Application.DoEvents()
                Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed
                gCMotion.SetVelHigh(axisIndex, 1)
                Do
                    Application.DoEvents()
                Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed
                gCMotion.SetAcc(axisIndex, 1)
                Do
                    Application.DoEvents()
                Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed
                gCMotion.SetDec(axisIndex, 1)
                Do
                    Application.DoEvents()
                Loop Until gCMotion.GetCmdStatus(axisIndex) = CommandStatus.Sucessed

        End Select
    End Sub
    Private Sub btnJog_MouseDown(sender As Object, e As MouseEventArgs) Handles btnJogN.MouseDown, btnJogP.MouseDown
        Dim dir As eDirection '移動方向
        If CType(sender, Button).Name = "btnJogN" Then
            dir = eDirection.Negative
            eCmd = ButtonCmd.JogN
        Else
            dir = eDirection.Positive
            eCmd = ButtonCmd.JogP
        End If
        Select Case gCMotion.AxisParameter(iAxis).CardParameter.CardType
            Case enmMotionCardType.ModBus
            Case enmMotionCardType.PCI_1245, enmMotionCardType.PCI_1285
                SetSpeedType(iAxis, mSpeedType) '移動前套用速度設定
        End Select
        '[說明]:按下去移動，放掉就停止
        If gCMotion.VelMove(iAxis, dir) = False Then
            gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnLeft_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
        End If
        CType(sender, Button).BackColor = Color.Yellow '按鍵顏色

        'Select Case gCMotion.AxisParameter(AxisNo).CardParameter.CardType
        '    Case enmMotionCardType.ModBus
        '        tmrMotorMenu.Start()
        'End Select
    End Sub
    
    Private Sub btnJog_MouseUp(sender As Object, e As MouseEventArgs) Handles btnJogN.MouseUp, btnJogP.MouseUp
        Select Case mSpeedType
            Case SpeedType.Fast
                Call gCMotion.SlowStop(iAxis, 3000)
                If gCMotion.AxisParameter(iAxis).CardParameter.CardType = enmMotionCardType.ModBus Then
                    'tmrMotorMenu.Stop()
                End If
            Case SpeedType.Medium
                Call gCMotion.SlowStop(iAxis, 1000)
                If gCMotion.AxisParameter(iAxis).CardParameter.CardType = enmMotionCardType.ModBus Then
                    'tmrMotorMenu.Stop()
                End If

            Case SpeedType.Slow
                Call gCMotion.SlowStop(iAxis, 100)
                If gCMotion.AxisParameter(iAxis).CardParameter.CardType = enmMotionCardType.ModBus Then
                    'tmrMotorMenu.Stop()
                End If

        End Select
        '確保命令發完
        Do
            Application.DoEvents()
        Loop Until gCMotion.GetCmdStatus(iAxis) = CommandStatus.Sucessed
        System.Threading.Thread.Sleep(100) '等100ms

        gCMotion.GetPositionValue(iAxis)
        Do
            Application.DoEvents()
        Loop Until gCMotion.GetCmdStatus(iAxis) = CommandStatus.Sucessed
        gCMotion.CheckMotorStatus(iAxis)
        Do
            Application.DoEvents()
        Loop Until gCMotion.GetCmdStatus(iAxis) = CommandStatus.Sucessed

        eCmd = ButtonCmd.None
    End Sub

  
End Class

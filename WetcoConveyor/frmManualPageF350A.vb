Imports ProjectCore
Imports ProjectMotion
Imports ProjectIO

Public Class frmManualPageF350A

    Enum enmConveyor
        Cv1
        Cv2
    End Enum

    Private _conveyor As enmConveyor = enmConveyor.Cv1
    Public Property Conveyor As enmConveyor
        Get
            Return _conveyor
        End Get
        Set(ByVal value As enmConveyor)
            _conveyor = value
        End Set
    End Property

    Dim Roller As IRoller

    Private Sub frmManualPageF350A_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConveterInitial()

        If (ProjectRecipe.gCRecipe.ConveyorSpeed > 0) Then
            nmcStartLocX.Value = ProjectRecipe.gCRecipe.ConveyorSpeed
        End If

        If (Unit.A_IsVacuum) Then
            btnVacuum.Text = "OFF"
        Else
            btnVacuum.Text = "ON"
        End If

        timer_UpdateStatus.Start()
    End Sub

    Private Sub rbtnConveyor1_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnConveyor2.CheckedChanged, rbtnConveyor1.CheckedChanged
        If (rbtnConveyor1.Checked) Then
            Conveyor = enmConveyor.Cv1
            Roller = Unit.A_Roller
        Else
            Conveyor = enmConveyor.Cv2
            Roller = Unit.B_Roller
        End If
    End Sub

    Private Sub btnStoperUp_Click(sender As Object, e As EventArgs) Handles btnStoperUp.Click
        If (Conveyor = enmConveyor.Cv1) Then
            Unit.A_Stoper(clsUnit.enmDirection.Up)
        Else
            Unit.B_Stoper(clsUnit.enmDirection.Up)
        End If
    End Sub

    Private Sub btnStoperDown_Click(sender As Object, e As EventArgs) Handles btnStoperDown.Click
        If (Conveyor = enmConveyor.Cv1) Then
            Unit.A_Stoper(clsUnit.enmDirection.Down)
        Else
            Unit.B_Stoper(clsUnit.enmDirection.Down)
        End If
    End Sub

    Private Sub btnCylinderUp_Click(sender As Object, e As EventArgs) Handles btnCylinderUp.Click
        If (Conveyor = enmConveyor.Cv1) Then
            Unit.A_Lifter(IConveyorUnit.enmDirection.Up)
        Else
            Unit.B_Lifter(IConveyorUnit.enmDirection.Up)
        End If
    End Sub

    Private Sub btnCylinderDown_Click(sender As Object, e As EventArgs) Handles btnCylinderDown.Click
        If (Conveyor = enmConveyor.Cv1) Then
            Unit.A_Lifter(IConveyorUnit.enmDirection.Down)
        Else
            Unit.B_Lifter(IConveyorUnit.enmDirection.Down)
        End If
    End Sub

    Private Sub btnVacuum_Click(sender As Object, e As EventArgs) Handles btnVacuum.Click
        If (btnVacuum.Text = "ON") Then
            btnVacuum.Text = "OFF"
            If (Conveyor = enmConveyor.Cv1) Then
                Unit.A_VacuumControl(True)
            Else
                Unit.B_VacuumControl(True)
            End If
        Else
            btnVacuum.Text = "ON"
            If (Conveyor = enmConveyor.Cv1) Then
                Unit.A_VacuumControl(False)
            Else
                Unit.B_VacuumControl(False)
            End If
        End If
    End Sub

    Private Sub nmcStartLocX_ValueChanged(sender As Object, e As EventArgs) Handles nmcStartLocX.ValueChanged
        Roller.SetSpeed(0, nmcStartLocX.Value)
    End Sub

    Private Sub btnRollerStart_Click(sender As Object, e As EventArgs) Handles btnRollerStart.Click
        If (rbtnForward.Checked) Then
            Roller.Run(IRoller.enmDirection.Forward)
        Else
            Roller.Run(IRoller.enmDirection.Reversal)
        End If
    End Sub

    Private Sub btnRollerStop_Click(sender As Object, e As EventArgs) Handles btnRollerStop.Click
        Roller.Stop()
    End Sub

    Private Sub btnInitial_Click(sender As Object, e As EventArgs) Handles btnInitial.Click
        gSSystemParameter.PassLUL = IIf(chkboxAuto.Checked, False, True)
        If (Conveyor = enmConveyor.Cv1) Then
            gSYS(eSys.Conveyor1).Command = eSysCommand.HomeA
        Else
            gSYS(eSys.Conveyor2).Command = eSysCommand.HomeA
        End If

    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        gSSystemParameter.PassLUL = IIf(chkboxAuto.Checked, False, True)
        If (Conveyor = enmConveyor.Cv1) Then
            gSYS(eSys.Conveyor1).Command = eSysCommand.LoadA
        Else
            gSYS(eSys.Conveyor2).Command = eSysCommand.LoadA
        End If
    End Sub

    Private Sub btnUnload_Click(sender As Object, e As EventArgs) Handles btnUnload.Click
        gSSystemParameter.PassLUL = IIf(chkboxAuto.Checked, False, True)
        If (Conveyor = enmConveyor.Cv1) Then
            gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadA
        Else
            gSYS(eSys.Conveyor2).Command = eSysCommand.UnloadA
        End If
    End Sub

    Private Sub btnMotionStop_Click(sender As Object, e As EventArgs) Handles btnMotionStop.Click
        If (Conveyor = enmConveyor.Cv1) Then
            gSYS(eSys.Conveyor1).Command = eSysCommand.None
            gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.None
            gSYS(eSys.Conveyor1).RunStatus = eSysCommand.None
            Unit.A_Roller.Stop()
        Else
            gSYS(eSys.Conveyor2).Command = eSysCommand.None
            gSYS(eSys.Conveyor2).ExecuteCommand = eSysCommand.None
            gSYS(eSys.Conveyor2).RunStatus = eSysCommand.None
            Unit.B_Roller.Stop()
        End If
    End Sub

    Private Sub timer_UpdateStatus_Tick(sender As Object, e As EventArgs) Handles timer_UpdateStatus.Tick
        If (Conveyor = enmConveyor.Cv1) Then
            picStoperUp.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station2StopperUpReady), 0, 1))
            picStoperDown.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station2StopperDownReady), 0, 1))
            picStoperSensor.Image = imgListItems.Images(IIf(Unit.A_StoperSensor(True), 0, 1))
            picCylinderUp.Image = imgListItems.Images(IIf(Unit.A_LifterLocation = IConveyorUnit.enmDirection.Up, 0, 1))
            picCylinderDown.Image = imgListItems.Images(IIf(Unit.A_LifterLocation = IConveyorUnit.enmDirection.Down, 0, 1))
            picInSensor.Image = imgListItems.Images(IIf(Unit.A_EntranceSensor, 0, 1))
            picVacuum.Image = imgListItems.Images(IIf(Unit.A_IsVacuum, 0, 1))

            picInitial.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.HomeA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Running, 0, 1))
            picLoad.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.LoadA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Running, 0, 1))
            picUnload.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.UnloadA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Running, 0, 1))
        Else
            picStoperUp.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station3StopperUpReady), 0, 1))
            picStoperDown.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station3StopperDownReady), 0, 1))
            picStoperSensor.Image = imgListItems.Images(IIf(Unit.B_StoperSensor(), 0, 1))
            picCylinderUp.Image = imgListItems.Images(IIf(Unit.B_LifterLocation = IConveyorUnit.enmDirection.Up, 0, 1))
            picCylinderDown.Image = imgListItems.Images(IIf(Unit.B_LifterLocation = IConveyorUnit.enmDirection.Down, 0, 1))
            picInSensor.Image = imgListItems.Images(IIf(Unit.B_EntranceSensor, 0, 1))
            picVacuum.Image = imgListItems.Images(IIf(Unit.B_IsVacuum, 0, 1))

            picInitial.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor2).ExecuteCommand = eSysCommand.HomeA AndAlso gSYS(eSys.Conveyor2).RunStatus = enmRunStatus.Running, 0, 1))
            picLoad.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor2).ExecuteCommand = eSysCommand.LoadA AndAlso gSYS(eSys.Conveyor2).RunStatus = enmRunStatus.Running, 0, 1))
            picUnload.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor2).ExecuteCommand = eSysCommand.UnloadA AndAlso gSYS(eSys.Conveyor2).RunStatus = enmRunStatus.Running, 0, 1))
        End If

    End Sub

#Region "流道寬度控制"

    Private Function ConveterInitial() As Boolean
        Call gCMotion.Servo(enmAxis.Converter, enmONOFF.eON) '通電確保
        Call gCMotion.AxisResetError(enmAxis.Converter) '清除錯誤
        Call gCMotion.SetCurve(enmAxis.Converter, eCurveMode.TCurve) '命令規畫確保
        gCMotion.DOOutput(enmAxis.Converter, 7, enmCardIOONOFF.eOFF) 'Alarm重製
        gCMotion.DOOutput(enmAxis.Converter, 7, enmCardIOONOFF.eON)

        gCMotion.SlowStop(enmAxis.Converter, 100)
        gCMotion.SetPosition(enmAxis.Converter, 0)

        gCMotion.AxisParameter(enmAxis.Converter).Velocity.AccRatio = 0.01
        gCMotion.AxisParameter(enmAxis.Converter).Velocity.DecRatio = 0.01

        If gCMotion.SetAcc(enmAxis.Converter, gCMotion.AxisParameter(enmAxis.Converter).Velocity.Acc * gCMotion.AxisParameter(enmAxis.Converter).Velocity.AccRatio) <> CommandStatus.Sucessed Then
            Return False
        End If

        If gCMotion.SetDec(enmAxis.Converter, gCMotion.AxisParameter(enmAxis.Converter).Velocity.Dec * gCMotion.AxisParameter(enmAxis.Converter).Velocity.DecRatio) <> CommandStatus.Sucessed Then
            Return False
        End If

        Return True
    End Function

    Private Sub btnConveterClose_MouseUp(sender As Object, e As Windows.Forms.MouseEventArgs) Handles btnConveterOpen.MouseUp, btnConveterClose.MouseUp
        gCMotion.EmgStop(enmAxis.Converter)
    End Sub

    Private Sub btnConveterOpen_MouseDown(sender As Object, e As Windows.Forms.MouseEventArgs) Handles btnConveterOpen.MouseDown
        gCMotion.VelMove(enmAxis.Converter, eDirection.Positive)
    End Sub

    Private Sub btnConveterClose_MouseDown(sender As Object, e As Windows.Forms.MouseEventArgs) Handles btnConveterClose.MouseDown
        gCMotion.VelMove(enmAxis.Converter, eDirection.Negative)
    End Sub

#End Region

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        gCMotion.DOOutput(enmAxis.Converter, 7, enmCardIOONOFF.eOFF) 'Alarm重製
        gCMotion.DOOutput(enmAxis.Converter, 7, enmCardIOONOFF.eON)
    End Sub
End Class
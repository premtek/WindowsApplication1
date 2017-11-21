Imports ProjectCore
Imports ProjectIO
Imports System.Windows.Forms
Imports ProjectMotion
Imports System.Resources

Public Class frmMain
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetPicBoxParentA()
        SetPicBoxParentB()
        SetPicBoxParentF230A()

        If (Unit.A_IsVacuum) Then
            btnVacuumA.Text = "OFF"
        Else
            btnVacuumA.Text = "ON"
        End If

        If (Unit.B_IsVacuum) Then
            btnVacuumB.Text = "OFF"
        Else
            btnVacuumB.Text = "ON"
        End If

        If (gDICollection.GetState(enmDI.Station2ChuckVacuumReady, False)) Then
            btnVacuum230.Text = "OFF"
        Else
            btnVacuum230.Text = "ON"
        End If

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                TabControl1.Controls.Remove(tbMachineF230A)
                TabControl1.Controls.Remove(tbMachineF350A)
            Case enmMachineType.DCS_500AD
                TabControl1.Controls.Remove(tbMachineB)
                TabControl1.Controls.Remove(tbMachineF230A)
                TabControl1.Controls.Remove(tbMachineF350A)

            Case enmMachineType.DCS_F230A
                TabControl1.Controls.Remove(tbMachineA)
                TabControl1.Controls.Remove(tbMachineB)
                TabControl1.Controls.Remove(tbMachineF350A)

            Case enmMachineType.DCS_350A
                TabControl1.Controls.Remove(tbMachineA)
                TabControl1.Controls.Remove(tbMachineB)
                TabControl1.Controls.Remove(tbMachineF230A)
                Dim ManualF350A As New frmManualPageF350A
                ManualF350A.TopLevel = False
                SetFormParent(Panel1, ManualF350A)

        End Select

        timer_CheckStatus.Start()
    End Sub

    '監控IO狀態, 更新UI顯示
    Private Sub timer_CheckStatus_Tick(sender As Object, e As EventArgs) Handles timer_CheckStatus.Tick
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                CheckStatusWectoA()
                CheckStatusWectoB()

                'If (Conveyor.Status = enmMotionStatus.Finish Or Conveyor.Status = enmMotionStatus.Stop) Then
                '    btnInitialA.Enabled = True
                '    btnLoadA.Enabled = True
                '    btnUnloadA.Enabled = True
                '    btnInitialB.Enabled = True
                '    btnLoadB.Enabled = True
                '    btnUnloadB.Enabled = True
                'Else
                '    btnInitialA.Enabled = False
                '    btnLoadA.Enabled = False
                '    btnUnloadA.Enabled = False
                '    btnInitialB.Enabled = False
                '    btnLoadB.Enabled = False
                '    btnUnloadB.Enabled = False
                'End If
            Case enmMachineType.DCS_500AD
                CheckStatusWectoA()
            Case enmMachineType.DCS_F230A
                CheckStatusF230A()

            Case enmMachineType.DCS_350A
                'TODO:待補

        End Select

        For Each Ctl As Control In Me.Controls
            If TypeOf Ctl Is PictureBox Then
                Ctl.Invalidate()
            End If
        Next

    End Sub

    Private Sub SetParent(ByVal parent As PictureBox, ByVal child As PictureBox)
        Dim newLocation As New Drawing.Point
        newLocation.X = child.Location.X - parent.Location.X
        newLocation.Y = child.Location.Y - parent.Location.Y
        child.Parent = parent
        child.Location = newLocation
    End Sub

    Private Sub SetFormParent(ByVal parent As Panel, ByVal child As Form)
        child.Parent = parent
        child.Show()
    End Sub

    Private Sub btnMStop_Click(sender As Object, e As EventArgs) Handles btnMStopA.Click, btnMStopB.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnMStop]" & vbTab & "Click")
        'Conveyor.Stop()
        gSYS(eSys.Conveyor1).Command = eSysCommand.None
        gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.None
        gSYS(eSys.Conveyor1).RunStatus = eSysCommand.None
        Unit.A_Roller.Stop()
        Unit.B_Roller.Stop()
    End Sub

    Private Sub btnMStop230_Click(sender As Object, e As EventArgs) Handles btnMStop230.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnMStop230]" & vbTab & "Click")
        'Conveyor.Stop()
        gSYS(eSys.Conveyor1).Command = eSysCommand.None
        gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.None
        gSYS(eSys.Conveyor1).RunStatus = eSysCommand.None

        gDOCollection.SetState(enmDO.MoveInMotorCW, False)
        gDOCollection.SetState(enmDO.MoveInMotorSlow, False)
    End Sub

#Region "A機控制"
    Private Sub SetPicBoxParentA()
        SetParent(picMachineA, pic2CheckInA)
        SetParent(picMachineA, pic2CheckOutA)
        SetParent(picMachineA, pic2Stoper2SensorA)
        SetParent(picMachineA, pic2RollerA1)
        SetParent(picMachineA, pic2RollerA2)
        SetParent(picMachineA, pic2RollerA3)
        SetParent(picMachineA, pic2RollerA4)
        SetParent(picMachineA, pic2Stoper1UpA)
        SetParent(picMachineA, pic2Stoper1DownA)
        SetParent(picMachineA, pic2Stoper2UpA)
        SetParent(picMachineA, pic2Stoper2DownA)
        SetParent(picMachineA, pic2ECyUpA)
        SetParent(picMachineA, pic2ECyDownA)
        SetParent(picMachineA, pic2ECyHomeA)
    End Sub

    Private Sub CheckStatusWectoA()
        'A機Vacuum狀態
        picVacuumA.Image = imgListItems.Images(IIf(mGlobalPool.Unit.A_IsVacuum, 0, 1))
        'A機Stoper狀態
        picStoper1UpA.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station1StopperUpReady), 0, 1))
        picStoper1DownA.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station1StopperDownReady), 0, 1))
        picStoper2UpA.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station2StopperUpReady), 0, 1))
        picStoper2DownA.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station2StopperDownReady), 0, 1))
        picStoper2SensorA.Image = imgListItems.Images(IIf(Unit.A_StoperSensor(False), 0, 1))
        'A機進出料Sensor狀態
        picCheckInA.Image = imgListItems.Images(IIf(Unit.A_EntranceSensor, 0, 1))
        picCheckOutA.Image = imgListItems.Images(IIf(Unit.A_ExitSensor, 0, 1))
        'A機電動缸狀態
        picECylinderUpA.Image = imgListItems.Images(IIf(Unit.A_ECylinderLocation = clsUnit.enmLocation.Top, 0, 1))
        picECylinderDownA.Image = imgListItems.Images(IIf(Unit.A_ECylinderLocation = clsUnit.enmLocation.Bottom, 0, 1))
        'picECylinderHomeA.Image = imgListItems.Images(IIf(Unit.A_ECylinderLocation = clsUnit.enmLocation.Home, 0, 1))
        If (Unit.A_ECylinderLocation = clsUnit.enmLocation.Home) Then
            picECylinderHomeA.Image = imgListItems.Images(0)
            btnECylinderUpA.Enabled = True
            btnECylinderDownA.Enabled = True
        Else
            picECylinderHomeA.Image = imgListItems.Images(1)
        End If

        pic2CheckInA.Image = imgListItems.Images(IIf(Unit.A_EntranceSensor, 0, 1))
        pic2CheckOutA.Image = imgListItems.Images(IIf(Unit.A_ExitSensor, 0, 1))
        pic2Stoper2SensorA.Image = imgListItems.Images(IIf(Unit.A_StoperSensor(False), 0, 1))
        pic2ECyUpA.Image = imgListItems.Images(IIf(Unit.A_ECylinderLocation = clsUnit.enmLocation.Top, 4, 3))
        pic2ECyDownA.Image = imgListItems.Images(IIf(Unit.A_ECylinderLocation = clsUnit.enmLocation.Bottom, 6, 5))
        pic2ECyHomeA.Image = imgListItems.Images(IIf(Unit.A_ECylinderLocation = clsUnit.enmLocation.Home, 8, 7))
        pic2Stoper1UpA.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station1StopperUpReady), 10, 9))
        pic2Stoper1DownA.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station1StopperDownReady), 12, 11))
        pic2Stoper2UpA.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station2StopperUpReady), 10, 9))
        pic2Stoper2DownA.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station2StopperDownReady), 12, 11))

        If (chkboxAutoA.Checked <> True) Then
            tbSlotNumA.Text = cls800AQ_LUL.A_ProductNum.ToString()
        End If

        picInitialA.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.HomeA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Running, 0, 1))
        picLoadA.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.LoadA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Running, 0, 1))
        picUnloadA.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.UnloadA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Running, 0, 1))
    End Sub

    Private Sub btnVacuumA_Click(sender As Object, e As EventArgs) Handles btnVacuumA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnVacuumA]" & vbTab & "Click")
        If (btnVacuumA.Text = "ON") Then
            btnVacuumA.Text = "OFF"
            Unit.A_VacuumControl(True)
        Else
            btnVacuumA.Text = "ON"
            Unit.A_VacuumControl(False)
        End If
    End Sub

    Private Sub btnECylinderUpA_Click(sender As Object, e As EventArgs) Handles btnECylinderUpA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnECylinderUpA]" & vbTab & "Click")
        Unit.A_ElectricCylinder(clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnECylinderDownA_Click(sender As Object, e As EventArgs) Handles btnECylinderDownA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnECylinderDownA]" & vbTab & "Click")
        If (Unit.A_IsVacuum = False) Then
            Unit.A_ElectricCylinder(clsUnit.enmDirection.Down)
        Else
            '請先破真空
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000071))
            MsgBox(gMsgHandler.GetMessage(Warn_3000071), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    Private Sub btnECylinderHomeA_Click(sender As Object, e As EventArgs) Handles btnECylinderHomeA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnECylinderHomeA]" & vbTab & "Click")
        If (Unit.A_IsVacuum = False) Then
            Unit.A_ElectricCylinder(clsUnit.enmDirection.Home)
        Else
            '請先破真空
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000071))
            MsgBox(gMsgHandler.GetMessage(Warn_3000071), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    Private Sub btnStoper2UpA_Click(sender As Object, e As EventArgs) Handles btnStoper2UpA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnStoper2UpA]" & vbTab & "Click")
        Unit.A_Stoper(clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnStoper2DownA_Click(sender As Object, e As EventArgs) Handles btnStoper2DownA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnStoper2DownA]" & vbTab & "Click")
        Unit.A_Stoper(clsUnit.enmDirection.Down)
    End Sub

    Private Sub btnStoper1UpA_Click(sender As Object, e As EventArgs) Handles btnStoper1UpA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnStoper1UpA]" & vbTab & "Click")
        Unit.A_FrontStoper(clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnStoper1DownA_Click(sender As Object, e As EventArgs) Handles btnStoper1DownA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnStoper1DownA]" & vbTab & "Click")
        Unit.A_FrontStoper(clsUnit.enmDirection.Down)
    End Sub

    Private Sub btnRollerStartA_Click(sender As Object, e As EventArgs) Handles btnRollerStartA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnRollerStartA]" & vbTab & "Click")
        Unit.A_Roller.Load(IRoller.enmSpeed.Normal)
        pic2RollerA1.Image = My.Resources.ArrowBlack3
        pic2RollerA2.Image = My.Resources.ArrowBlack3
        pic2RollerA3.Image = My.Resources.ArrowBlack3
        pic2RollerA4.Image = My.Resources.ArrowBlack3

    End Sub

    Private Sub btnRollerSlowA_Click(sender As Object, e As EventArgs) Handles btnRollerSlowA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnRollerSlowA]" & vbTab & "Click")
        Unit.A_Roller.Load(IRoller.enmSpeed.Slow)
        pic2RollerA1.Image = My.Resources.ArrowBlack2
        pic2RollerA2.Image = My.Resources.ArrowBlack2
        pic2RollerA3.Image = My.Resources.ArrowBlack2
        pic2RollerA4.Image = My.Resources.ArrowBlack2
    End Sub

    Private Sub btnRollerStopA_Click(sender As Object, e As EventArgs) Handles btnRollerStopA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnRollerStopA]" & vbTab & "Click")
        Unit.A_Roller.Stop()
        pic2RollerA1.Image = imgListItems.Images(17)
        pic2RollerA2.Image = imgListItems.Images(17)
        pic2RollerA3.Image = imgListItems.Images(17)
        pic2RollerA4.Image = imgListItems.Images(17)
    End Sub

    Private Sub btnInitialA_Click(sender As Object, e As EventArgs) Handles btnInitialA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnInitialA]" & vbTab & "Click")
        'Conveyor.Motion(WetcoConveyor.clsDTSConveyor.enmMotion.A_Initial, False)
        gSSystemParameter.PassLUL = True
        gSYS(eSys.Conveyor1).Command = eSysCommand.HomeA
    End Sub

    Private Sub btnLoadA_Click(sender As Object, e As EventArgs) Handles btnLoadA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnLoadA]" & vbTab & "Click")
        'Conveyor.Motion(WetcoConveyor.clsDTSConveyor.enmMotion.A_Load, False)
        gSSystemParameter.PassLUL = Not chkboxAutoA.Checked
        gSYS(eSys.Conveyor1).Command = eSysCommand.LoadA
    End Sub

    Private Sub btnUnloadA_Click(sender As Object, e As EventArgs) Handles btnUnloadA.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnUnloadA]" & vbTab & "Click")
        'Conveyor.Motion(WetcoConveyor.clsDTSConveyor.enmMotion.A_Unload, False)
        cls800AQ_LUL.A_ProductNum = Convert.ToInt32(tbSlotNumA.Text)
        gSSystemParameter.PassLUL = Not chkboxAutoA.Checked
        gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadA
    End Sub
#End Region

#Region "B機控制"
    Private Sub SetPicBoxParentB()
        SetParent(picMachineB, pic2CheckInB)
        SetParent(picMachineB, pic2CheckOutB)
        SetParent(picMachineB, pic2StoperSensorB)
        SetParent(picMachineB, pic2RollerB1)
        SetParent(picMachineB, pic2RollerB2)
        SetParent(picMachineB, pic2RollerB3)
        SetParent(picMachineB, pic2RollerB4)
        SetParent(picMachineB, pic2StoperUpB)
        SetParent(picMachineB, pic2StoperDownB)
        SetParent(picMachineB, pic2ECyUpB)
        SetParent(picMachineB, pic2ECyDownB)
        SetParent(picMachineB, pic2ECyHomeB)
    End Sub

    Private Sub CheckStatusWectoB()
        'B機Vacuum狀態
        picVacuumB.Image = imgListItems.Images(IIf(mGlobalPool.Unit.B_IsVacuum, 0, 1))
        'B機Stoper狀態
        picStoperUpB.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station3StopperUpReady), 0, 1))
        picStoperDownB.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station3StopperDownReady), 0, 1))
        picStoperSensorB.Image = imgListItems.Images(IIf(Unit.B_StoperSensor, 0, 1))
        'B機進出料Sensor狀態
        picCheckInB.Image = imgListItems.Images(IIf(Unit.B_EntranceSensor, 0, 1))
        picCheckOutB.Image = imgListItems.Images(IIf(Unit.B_ExitSensor, 0, 1))
        'B機電動缸狀態
        picECylinderUpB.Image = imgListItems.Images(IIf(Unit.B_ECylinderLocation = clsUnit.enmLocation.Top, 0, 1))
        picECylinderDownB.Image = imgListItems.Images(IIf(Unit.B_ECylinderLocation = clsUnit.enmLocation.Bottom, 0, 1))
        'picECylinderHomeB.Image = imgListItems.Images(IIf(Unit.B_ECylinderLocation = clsUnit.enmLocation.Home, 0, 1))
        If (Unit.B_ECylinderLocation = clsUnit.enmLocation.Home) Then
            picECylinderHomeB.Image = imgListItems.Images(0)
            btnECylinderUpB.Enabled = True
            btnECylinderDownB.Enabled = True
        Else
            picECylinderHomeB.Image = imgListItems.Images(1)
        End If

        pic2CheckInB.Image = imgListItems.Images(IIf(Unit.B_EntranceSensor, 0, 1))
        pic2CheckOutB.Image = imgListItems.Images(IIf(Unit.B_ExitSensor, 0, 1))
        pic2StoperSensorB.Image = imgListItems.Images(IIf(Unit.B_StoperSensor(), 0, 1))
        pic2ECyUpB.Image = imgListItems.Images(IIf(Unit.B_ECylinderLocation = clsUnit.enmLocation.Top, 4, 3))
        pic2ECyDownB.Image = imgListItems.Images(IIf(Unit.B_ECylinderLocation = clsUnit.enmLocation.Bottom, 6, 5))
        pic2ECyHomeB.Image = imgListItems.Images(IIf(Unit.B_ECylinderLocation = clsUnit.enmLocation.Home, 8, 7))
        pic2StoperUpB.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station3StopperUpReady), 10, 9))
        pic2StoperDownB.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station3StopperDownReady), 12, 11))

        If (chkboxAutoB.Checked <> True) Then
            tbSlotNumB.Text = cls800AQ_LUL.B_ProductNum.ToString()
        End If

        picInitialB.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.HomeB AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Running, 0, 1))
        picLoadB.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.LoadB AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Running, 0, 1))
        picUnloadB.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.UnloadB AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Running, 0, 1))
    End Sub

    Private Sub btnVacuumB_Click(sender As Object, e As EventArgs) Handles btnVacuumB.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnVacuumB]" & vbTab & "Click")
        If (btnVacuumB.Text = "ON") Then
            btnVacuumB.Text = "OFF"
            Unit.B_VacuumControl(True)
        Else
            btnVacuumB.Text = "ON"
            Unit.B_VacuumControl(False)
        End If
    End Sub

    Private Sub btnECylinderUpB_Click(sender As Object, e As EventArgs) Handles btnECylinderUpB.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnECylinderUpB]" & vbTab & "Click")
        Unit.B_ElectricCylinder(clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnECylinderDownB_Click(sender As Object, e As EventArgs) Handles btnECylinderDownB.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnECylinderDownB]" & vbTab & "Click")
        If (Unit.B_IsVacuum = False) Then
            Unit.B_ElectricCylinder(clsUnit.enmDirection.Down)
        Else
            '請先破真空
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000071))
            MsgBox(gMsgHandler.GetMessage(Warn_3000071), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Please break the vacuum.")
        End If
    End Sub

    Private Sub btnECylinderHomeB_Click(sender As Object, e As EventArgs) Handles btnECylinderHomeB.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnECylinderHomeB]" & vbTab & "Click")
        If (Unit.B_IsVacuum = False) Then
            Unit.B_ElectricCylinder(clsUnit.enmDirection.Home)
        Else
            '請先破真空
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000071))
            MsgBox(gMsgHandler.GetMessage(Warn_3000071), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Please break the vacuum.")
        End If
    End Sub

    Private Sub btnStoperUpB_Click(sender As Object, e As EventArgs) Handles btnStoperUpB.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnStoperUpB]" & vbTab & "Click")
        Unit.B_Stoper(clsUnit.enmDirection.Up)
    End Sub

    Private Sub btnStoperDownB_Click(sender As Object, e As EventArgs) Handles btnStoperDownB.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnStoperDownB]" & vbTab & "Click")
        Unit.B_Stoper(clsUnit.enmDirection.Down)
    End Sub

    Private Sub btnRollerStartB_Click(sender As Object, e As EventArgs) Handles btnRollerStartB.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnRollerStartB]" & vbTab & "Click")
        Unit.B_Roller.Load(IRoller.enmSpeed.Normal)
        pic2RollerB1.Image = My.Resources.ArrowBlack3
        pic2RollerB2.Image = My.Resources.ArrowBlack3
        pic2RollerB3.Image = My.Resources.ArrowBlack3
        pic2RollerB4.Image = My.Resources.ArrowBlack3
    End Sub

    Private Sub btnRollerSlowB_Click(sender As Object, e As EventArgs) Handles btnRollerSlowB.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnRollerSlowB]" & vbTab & "Click")
        Unit.B_Roller.Load(IRoller.enmSpeed.Slow)
        pic2RollerB1.Image = My.Resources.ArrowBlack2
        pic2RollerB2.Image = My.Resources.ArrowBlack2
        pic2RollerB3.Image = My.Resources.ArrowBlack2
        pic2RollerB4.Image = My.Resources.ArrowBlack2
    End Sub

    Private Sub btnRollerStopB_Click(sender As Object, e As EventArgs) Handles btnRollerStopB.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnRollerStopB]" & vbTab & "Click")
        Unit.B_Roller.Stop()
        pic2RollerB1.Image = imgListItems.Images(17)
        pic2RollerB2.Image = imgListItems.Images(17)
        pic2RollerB3.Image = imgListItems.Images(17)
        pic2RollerB4.Image = imgListItems.Images(17)
    End Sub

    Private Sub btnInitialB_Click(sender As Object, e As EventArgs) Handles btnInitialB.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnInitialB]" & vbTab & "Click")
        'Conveyor.Motion(WetcoConveyor.clsDTSConveyor.enmMotion.B_Initial, False)
        gSSystemParameter.PassLUL = True
        gSYS(eSys.Conveyor1).Command = eSysCommand.HomeB
    End Sub

    Private Sub btnLoadB_Click(sender As Object, e As EventArgs) Handles btnLoadB.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnLoadB]" & vbTab & "Click")
        'Conveyor.Motion(WetcoConveyor.clsDTSConveyor.enmMotion.B_Load, False)
        gSSystemParameter.PassLUL = Not chkboxAutoB.Checked
        gSYS(eSys.Conveyor1).Command = eSysCommand.LoadB
    End Sub

    Private Sub btnUnloadB_Click(sender As Object, e As EventArgs) Handles btnUnloadB.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnUnloadB]" & vbTab & "Click")
        'Conveyor.Motion(WetcoConveyor.clsDTSConveyor.enmMotion.B_Unload, False)
        cls800AQ_LUL.B_ProductNum = Convert.ToInt32(tbSlotNumB.Text)
        gSSystemParameter.PassLUL = Not chkboxAutoB.Checked
        gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadB
    End Sub
#End Region

#Region "F230A"
    Private Sub SetPicBoxParentF230A()
        

    End Sub

    Private Sub CheckStatusF230A()
        If (btnVacuum230.Text = "ON") Then
            If (gDICollection.GetState(enmDI.Station2ChuckVacuumReady, False) = False) Then '已破真空
                gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, False)   '吹氣OFF
            End If
        End If

        picStoperUp230.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station2StopperUpReady, False), 0, 1))
        picStoperDown230.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station2StopperDownReady, False), 0, 1))
        picStoperSensor230.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station2TrayReady, False), 0, 1))
        picFixCyOpen230.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.TrayClamperOffReady, False), 0, 1))
        picFixCyClose230.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.TrayClamperOnReady, False), 0, 1))
        picCyUp230.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station2Heater1CylinderUpReady, False), 0, 1))
        picCyDown230.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady, False), 0, 1))
        picVacuum230.Image = imgListItems.Images(IIf(gDICollection.GetState(enmDI.Station2ChuckVacuumReady, False), 0, 1))

        picInitial230.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.HomeA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Running, 0, 1))
        picLoad230.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.LoadA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Running, 0, 1))
        picUnload230.Image = imgListItems.Images(IIf(gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.UnloadA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Running, 0, 1))
    End Sub

    Private Sub btnStoperUp230_Click(sender As Object, e As EventArgs) Handles btnStoperUp230.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnStoperUp230]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.Station2StopperDown, False)
        gDOCollection.SetState(enmDO.Station2StopperUp, True)
    End Sub

    Private Sub btnStoperDown230_Click(sender As Object, e As EventArgs) Handles btnStoperDown230.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnStoperDown230]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.Station2StopperUp, False)
        gDOCollection.SetState(enmDO.Station2StopperDown, True)
    End Sub

    Private Sub btnRollerStart230_Click(sender As Object, e As EventArgs) Handles btnRollerStart230.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnRollerStart230]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.MoveInMotorCW, True)
        gDOCollection.SetState(enmDO.MoveInMotorSlow, False)
    End Sub

    Private Sub btnRollerStop230_Click(sender As Object, e As EventArgs) Handles btnRollerStop230.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnRollerStop230]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.MoveInMotorCW, False)
        gDOCollection.SetState(enmDO.MoveInMotorSlow, False)
    End Sub

    Private Sub btnFixCyOpen230_Click(sender As Object, e As EventArgs) Handles btnFixCyOpen230.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnFixCyOpen230]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.TrayClamperOn, False) '料盤逼緊缸:夾
        gDOCollection.SetState(enmDO.TrayClamperOff, True) '料盤逼緊缸:放
    End Sub

    Private Sub btnFixCyClose230_Click(sender As Object, e As EventArgs) Handles btnFixCyClose230.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnFixCyClose230]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.TrayClamperOff, False) '料盤逼緊缸:放
        gDOCollection.SetState(enmDO.TrayClamperOn, True) '料盤逼緊缸:夾
    End Sub

    Private Sub btnCyUp230_Click(sender As Object, e As EventArgs) Handles btnCyUp230.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnCyUp230]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterCylinderDown1, False) '頂升汽缸下降
        gDOCollection.SetState(enmDO.HeaterCylinderUp1, True) '頂升汽缸上升
    End Sub

    Private Sub btnCyDown230_Click(sender As Object, e As EventArgs) Handles btnCyDown230.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnCyDown230]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterCylinderUp1, False) '頂升汽缸上升
        gDOCollection.SetState(enmDO.HeaterCylinderDown1, True) '頂升汽缸下降
    End Sub

    Private Sub btnVacuum230_Click(sender As Object, e As EventArgs) Handles btnVacuum230.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnVacuum230]" & vbTab & "Click")
        If (btnVacuum230.Text = "ON") Then
            btnVacuum230.Text = "OFF"
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, False)
            gDOCollection.SetState(enmDO.Station2ChuckVacuum, True)
        Else
            btnVacuum230.Text = "ON"
            gDOCollection.SetState(enmDO.Station2ChuckVacuum, False)
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, True)
        End If
    End Sub

    Private Sub btnInitial230_Click(sender As Object, e As EventArgs) Handles btnInitial230.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnInitial230]" & vbTab & "Click")
        gSSystemParameter.PassLUL = True
        gSYS(eSys.Conveyor1).Command = eSysCommand.HomeA
    End Sub

    Private Sub btnLoad230_Click(sender As Object, e As EventArgs) Handles btnLoad230.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnLoad230]" & vbTab & "Click")
        gSSystemParameter.PassLUL = Not chkboxAuto230.Checked
        gSYS(eSys.Conveyor1).Command = eSysCommand.LoadA
    End Sub

    Private Sub btnUnload230_Click(sender As Object, e As EventArgs) Handles btnUnload230.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnUnload230]" & vbTab & "Click")
        gSSystemParameter.PassLUL = Not chkboxAuto230.Checked
        gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadA
    End Sub

#End Region

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        timer_CheckStatus.Stop()
        Me.Dispose(True)
    End Sub

    Private Sub chkboxAutoA_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxAutoA.CheckedChanged
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[chkboxAutoA]" & vbTab & "CheckedChanged")
        tbSlotNumA.Enabled = chkboxAutoA.Checked
    End Sub

    Private Sub chkboxAutoB_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxAutoB.CheckedChanged
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[chkboxAutoB]" & vbTab & "CheckedChanged")
        tbSlotNumB.Enabled = chkboxAutoB.Checked
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Sue20170627
        gSyslog.Save("[WetcoConveyor_frmMain]" & vbTab & "[btnExit]" & vbTab & "Click")
        Me.Close()
    End Sub

End Class
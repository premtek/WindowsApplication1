Imports ProjectIO
Imports ProjectCore
Imports System.Drawing
Imports ProjectMotion
Imports ProjectConveyor
Imports ProjectTriggerBoard
Imports ProjectValveController
Imports ProjectFeedback
Imports ProjectAOI
Imports WetcoConveyor
Imports ProjectLaserInterferometer

Public Class frmDiag

    Private Sub frmDiag_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub
    Private Sub frmDiag_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'gUserLevel = enmUserLevel.eAdministrator
        'gUserLevel = enmUserLevel.eSoftwareMaker

        MachineName = MachineTypeToString(gSSystemParameter.MachineType)
        Me.Text = "Diagnostic (Machine type = " & MachineName & ")"

        If (gUserLevel = enmUserLevel.eSoftwareMaker Or gUserLevel = enmUserLevel.eAdministrator) Then
            MessageBox.Visible = True
        Else
            MessageBox.Visible = False
        End If
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub btnDiagRun_Click(sender As Object, e As EventArgs) Handles btnDiagRun.Click
        'Sue20170627
        gSyslog.Save("[frmDiag]" & vbTab & "[btnDiagRun]" & vbTab & "Click")

        '20170602按鍵保護
        If btnDiagRun.Enabled = False Then '防連按
            Exit Sub
        End If

        ResetPal()
        MessageBox.Text = ""
        btnDiagRun.Text = resDiag.GetString("Processing.Text")

        '20170602按鍵保護
        btnDiagRun.Enabled = False
        btnBack.Enabled = False

        '  MessageBox.Text &= gMsgHandler.GetMessage(Error_1032000) & vbNewLine

        'btnDiagRun.Text = "Processing"
        btnDiagRun.BackColor = System.Drawing.Color.Yellow
        Threading.Thread.CurrentThread.Join(1000)
        ''''''''''''''''
        ''  DI Diag   ''
        ''''''''''''''''
        If ckbDIAlarm.Checked = True Then
            '' Machine A ''
            RefreshPal(palEMS, gDICollection.GetState(enmDI.EMS)) 'EMS異常
            RefreshPal(palCDA, gDICollection.GetState(enmDI.CDA)) '廠務氣壓偵測
            RefreshPal(palDoorClose, gDICollection.GetState(enmDI.DoorClose)) ' 開門停機保護
            '    RefreshPal(palAMCSystem, gDICollection.GetState(enmDI.MC1))  'DI-MC1 控制電源 
            RefreshPal(palMCMotor, gDICollection.GetState(enmDI.MC2))   'DI-MC2 馬達動力
            RefreshPal(palMCHeater, gDICollection.GetState(enmDI.MC3))  'DI-MC3 加熱電源
            RefreshPal(palPrevAlarm, gDICollection.GetState(enmDI.PrevAlarm)) 'B:上站異常
            RefreshPal(palNextAlarm, gDICollection.GetState(enmDI.NextAlarm)) 'B:下站異常
            RefreshPal(palLValveControllerAlarm, gDICollection.GetState(enmDI.ValveControllerAlarm1)) 'L 噴射閥控制器異常
            RefreshPal(palDetectGlueSensor, Not gDICollection.GetState(enmDI.DetectSyringeSensor1)) 'L 膠槍膠量偵測
            RefreshPal(palMoveInMotorAlarm, gDICollection.GetState(enmDI.MoveInMotorAlarm2))  'B:入料馬達 異常      
        End If

        ' '''''''''''''''''''''''
        ' ''   Communication   ''
        ' '''''''''''''''''''''''
        If ckbCommunication.Checked = True Then
            Dim BalanceValue As Decimal
            Dim HeightValue As String = ""
            ' '' Valve Controller ''   
            If gValvecontrollerCollection.SetValvePower(0, True) = enmCommandState.Success Then
                palValveController1.BackgroundImage = My.Resources.li_03
            Else
                palValveController1.BackgroundImage = My.Resources.li_08
            End If
            ' '' Trigger Board ''
            gTriggerBoardVersion(0) = ""
            If gTriggerBoard.GetVersion(0, True) = True Then
                gTriggerBoardVersion(0) = gTriggerBoard.Version(0).Value
                MessageBox.Text &= "A: TRG board1 Ver:" & gTriggerBoardVersion(0) & vbNewLine
                palTriggerBoard1.BackgroundImage = My.Resources.li_03
            Else
                MessageBox.Text &= "A: TRG board1 Ver: Null" & vbNewLine
                palTriggerBoard1.BackgroundImage = My.Resources.li_08
            End If
            '' '' Laser Reader ''
            If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", 0, HeightValue, True) = True Then
                palLaserReader1.BackgroundImage = My.Resources.li_03
                MessageBox.Text &= "A: Laser reader1: " & Val(HeightValue) & vbNewLine
            Else
                palLaserReader1.BackgroundImage = My.Resources.li_08
                MessageBox.Text &= "A: Laser reader1: Null" & vbNewLine
            End If
            '' '' Balance Scale ''
            If gBalanceCollection.RequestCurrentValue(0, BalanceValue, True) Then
                palBalance.BackgroundImage = My.Resources.li_03
                MessageBox.Text &= "A: Balance value: " & BalanceValue & vbNewLine
            Else
                palBalance.BackgroundImage = My.Resources.li_08
                MessageBox.Text &= "A: Balance value: Null" & vbNewLine
            End If
        End If
        '''''''''''''''''''
        ''  Vacuum Diag  ''
        '''''''''''''''''''
        If ckbVacuum.Checked = True Then
            ' MsgBox("Machine A: Please setup Vacuum environment", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            MsgBox(resDiag.GetString("MessageVacuum.Text"), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)



            '' Chuck段真空建立(錶頭) ''
            If (Unit.A_IsVacuum) Then
                palChuckVacuumReady.BackgroundImage = My.Resources.li_03
            Else
                RefreshPal(palChuckVacuumReady, Not Unit.A_VacuumControl(True))
                Unit.A_VacuumControl(False)
            End If
            '' Purge Vacuum ''
            If (gDICollection.GetState(enmDI.PurgeVacuumReady)) Then  '' Purge Vacuum left
                palPurgeVacuum1.BackgroundImage = My.Resources.li_03
            Else
                gDOCollection.SetState(enmDO.Purge, True)
                Threading.Thread.CurrentThread.Join(300)
                RefreshPal(palPurgeVacuum1, Not gDICollection.GetState(enmDI.PurgeVacuumReady))
                gDOCollection.SetState(enmDO.Purge, False)
            End If
        End If
        ''''''''''''''''''''''
        ''   EP Valve Diag  ''
        ''''''''''''''''''''''
        If ckbEPValve.Checked = True Then
            Dim CurrentReturnValue1, ReturnValue As Decimal
            ' '' turn on Vacuum
            'gDOCollection.GetSetState(enmDO.GlueNo1SyringePressure) = True
            'gDOCollection.GetSetState(enmDO.GlueNo2SyringePressure) = True
            'gDOCollection.GetSetState(enmDO.SyringePressure3) = True
            'gDOCollection.GetSetState(enmDO.SyringePressure4) = True
            '' Get current Valve value
            gEPVCollection.GetValue(0, CurrentReturnValue1, True)
            txtbAEPValve1Current.Text = CurrentReturnValue1
            '' Set Valve value
            'gEPVCollection.SetValue(0, Val(txtbAEPValve1Set.Text), True)
            gEPVCollection.SetValue(eEPVPressureType.Syringe, 0, Val(txtbAEPValve1Set.Text), True)
            gEPVCollection.GetValue(0, ReturnValue, True)
            txtbAEPValve1Return.Text = ReturnValue
            RefreshPal(palEPValve1, Not (Math.Abs(ReturnValue - Val(txtbAEPValve1Set.Text)) < 0.05))
            '' Recover Valve value
            gEPVCollection.SetValue(0, CurrentReturnValue1, True)
        End If
        '''''''''''''''''''
        ''   Light Diag  ''
        '''''''''''''''''''
        If ckbLight.Checked = True Then

            Dim LightType As Integer = gLightCollection.Cards(0).CardType
            Dim iLight1CurrentValue, iLight2CurrentValue, iLight3CurrentValue, iLight4CurrentValue As Integer
            Dim iLight1ReturnValue, iLight2ReturnValue, iLight3ReturnValue, iLight4ReturnValue As Integer

            '' turn on light control
            gDOCollection.SetState(enmDO.CCDLight, True)
            gDOCollection.SetState(enmDO.CCDLight2, True)
            gDOCollection.SetState(enmDO.CCDLight3, True)
            gDOCollection.SetState(enmDO.CCDLight4, True)
            '' Get light current value
            gLightCollection.Items(0).GetLightValue(0, iLight1CurrentValue, True)
            gLightCollection.Items(0).GetLightValue(1, iLight2CurrentValue, True)
            gLightCollection.Items(0).GetLightValue(2, iLight3CurrentValue, True)
            gLightCollection.Items(0).GetLightValue(3, iLight4CurrentValue, True)
            txtbLight1Current.Text = iLight1CurrentValue
            txtbLight2Current.Text = iLight2CurrentValue
            txtbLight3Current.Text = iLight3CurrentValue
            txtbLight4Current.Text = iLight4CurrentValue
            '' Set light value
            gLightCollection.Items(0).SetLightValue(0, Val(txtbLight1Set.Text), True)
            gLightCollection.Items(0).GetLightValue(0, iLight1ReturnValue, True)
            txtbLight1Return.Text = iLight1ReturnValue
            RefreshPal(palLight1, Not (Math.Abs(Val(txtbLight1Set.Text) - iLight1ReturnValue) <= 1))
            gLightCollection.Items(0).SetLightValue(1, Val(txtbLight2Set.Text), True)
            gLightCollection.Items(0).GetLightValue(1, iLight2ReturnValue, True)
            txtbLight2Return.Text = iLight2ReturnValue
            RefreshPal(palLight2, Not (Math.Abs(Val(txtbLight2Set.Text) - iLight2ReturnValue) <= 1))
            gLightCollection.Items(0).SetLightValue(2, Val(txtbLight3Set.Text), True)
            gLightCollection.Items(0).GetLightValue(2, iLight3ReturnValue, True)
            txtbLight3Return.Text = iLight3ReturnValue
            RefreshPal(palLight3, Not (Math.Abs(Val(txtbLight3Set.Text) - iLight3ReturnValue) <= 1))
            gLightCollection.Items(0).SetLightValue(3, Val(txtbLight4Set.Text), True)
            gLightCollection.Items(0).GetLightValue(3, iLight4ReturnValue, True)
            txtbLight4Return.Text = iLight4ReturnValue
            RefreshPal(palLight4, Not (Math.Abs(Val(txtbLight4Set.Text) - iLight4ReturnValue) <= 1))
            '' Recover light value
            gLightCollection.Items(0).SetLightValue(0, iLight1CurrentValue, True)
            gLightCollection.Items(0).SetLightValue(1, iLight2CurrentValue, True)
            gLightCollection.Items(0).SetLightValue(2, iLight3CurrentValue, True)
            gLightCollection.Items(0).SetLightValue(3, iLight4CurrentValue, True)
            '' turn off light control
            gDOCollection.SetState(enmDO.CCDLight, False)
            gDOCollection.SetState(enmDO.CCDLight2, False)
            gDOCollection.SetState(enmDO.CCDLight3, False)
            gDOCollection.SetState(enmDO.CCDLight4, False)
        End If

        ''''''''''''''''''''
        ''  Cylinder Diag ''
        ''''''''''''''''''''

        If ckbCylinder1.Checked = True Then

            ' Fix Cylinder On/Off
            lblFixCylinderOn.BackColor = System.Drawing.Color.Yellow
            gDOCollection.SetState(enmDO.TrayClamperOn, True) '料盤逼緊缸: On
            gDOCollection.SetState(enmDO.TrayClamperOff, False)
            Threading.Thread.CurrentThread.Join(1000)
            RefreshPal(palFixCylinderOn, Not gDICollection.GetState(enmDI.TrayClamperOnReady))
            lblFixCylinderOn.BackColor = System.Drawing.SystemColors.Control

            lblFixCylinderOff.BackColor = System.Drawing.Color.Yellow
            gDOCollection.SetState(enmDO.TrayClamperOff, True) '料盤逼緊缸: Off
            gDOCollection.SetState(enmDO.TrayClamperOn, False)
            Threading.Thread.CurrentThread.Join(1000)
            RefreshPal(palFixCylinderOff, Not gDICollection.GetState(enmDI.TrayClamperOffReady))
            lblFixCylinderOff.BackColor = System.Drawing.SystemColors.Control

            ' Stopper Up/Down
            lblStopper2Up.BackColor = System.Drawing.Color.Yellow
            gDOCollection.SetState(enmDO.Station2StopperDown, False) '' A Stopper2 Up
            gDOCollection.SetState(enmDO.Station2StopperUp, True)

            Threading.Thread.CurrentThread.Join(1000)
            lblStopper2Up.BackColor = System.Drawing.SystemColors.Control
            If (gDICollection.GetState(enmDI.Station2StopperUpReady)) Then
                palStopper2Up.BackgroundImage = My.Resources.li_03
            Else
                palStopper2Up.BackgroundImage = My.Resources.li_08
            End If
            lblStopper2Down.BackColor = System.Drawing.Color.Yellow
            gDOCollection.SetState(enmDO.Station2StopperUp, False)  '' A Stopper2 Down
            gDOCollection.SetState(enmDO.Station2StopperDown, True)

            Threading.Thread.CurrentThread.Join(1000)
            lblStopper2Down.BackColor = System.Drawing.SystemColors.Control
            If (gDICollection.GetState(enmDI.Station2StopperDownReady)) Then
                palStopper2Down.BackgroundImage = My.Resources.li_03
            Else
                palStopper2Down.BackgroundImage = My.Resources.li_08
            End If

            ' Chuck Up/Down
            If MsgBox(resDiag.GetString("MessageChuck_1.Text"), MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = MsgBoxResult.Yes Then  ''Please confirm if the product on the chuck is removed ?
                lblChuckUp.BackColor = System.Drawing.Color.Yellow
                gDOCollection.SetState(enmDO.HeaterCylinderUp1, True) '頂升汽缸上升
                gDOCollection.SetState(enmDO.HeaterCylinderDown1, False)
                Threading.Thread.CurrentThread.Join(5000)
                RefreshPal(palChuckUp, Not gDICollection.GetState(enmDI.Station2Heater1CylinderUpReady))
                lblChuckUp.BackColor = System.Drawing.SystemColors.Control

                lblChuckDown.BackColor = System.Drawing.Color.Yellow
                gDOCollection.SetState(enmDO.HeaterCylinderDown1, True) '頂升汽缸下降
                gDOCollection.SetState(enmDO.HeaterCylinderUp1, False)
                Threading.Thread.CurrentThread.Join(5000)
                RefreshPal(palChuckDown, Not gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady))
                lblChuckDown.BackColor = System.Drawing.SystemColors.Control
            Else
                MsgBox(resDiag.GetString("MessageChuck_2.Text"), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) ''Please remove product  before checking cylinder?
            End If

        End If

        btnDiagRun.BackColor = System.Drawing.SystemColors.Control
        btnDiagRun.Text = resDiagWetco.GetString("Run.Text")
        'btnDiagRun.Text = "Run"

        '20170602按鍵保護
        btnDiagRun.Enabled = True
        btnBack.Enabled = True

    End Sub
    Sub RefreshPal(ByRef palLight As Panel, ByVal blnIsError As Boolean)

        If blnIsError Then
            palLight.BackgroundImage = My.Resources.Red
        Else
            palLight.BackgroundImage = My.Resources.Green
        End If
    End Sub
    Sub ResetPal()
        ''  DI Diag   ''
        palEMS.BackgroundImage = My.Resources.Grey
        palCDA.BackgroundImage = My.Resources.Grey
        palDoorClose.BackgroundImage = My.Resources.Grey
        '  palAMCSystem.BackgroundImage = My.Resources.Grey
        palMCMotor.BackgroundImage = My.Resources.Grey
        palMCHeater.BackgroundImage = My.Resources.Grey
        palPrevAlarm.BackgroundImage = My.Resources.Grey
        palNextAlarm.BackgroundImage = My.Resources.Grey
        palLValveControllerAlarm.BackgroundImage = My.Resources.Grey
        palDetectGlueSensor.BackgroundImage = My.Resources.Grey
        palMoveInMotorAlarm.BackgroundImage = My.Resources.Grey
        ''   Communication  ''
        palValveController1.BackgroundImage = My.Resources.Grey
        palTriggerBoard1.BackgroundImage = My.Resources.Grey
        palLaserReader1.BackgroundImage = My.Resources.Grey
        palBalance.BackgroundImage = My.Resources.Grey
        ''   Vacuum Diag  ''
        palChuckVacuumReady.BackgroundImage = My.Resources.Grey
        palPurgeVacuum1.BackgroundImage = My.Resources.Grey
        ''   EP Valve Diag  ''
        palEPValve1.BackgroundImage = My.Resources.Grey
        ''   Light Diag  '' 
        palLight1.BackgroundImage = My.Resources.Grey
        palLight2.BackgroundImage = My.Resources.Grey
        palLight3.BackgroundImage = My.Resources.Grey
        palLight4.BackgroundImage = My.Resources.Grey
        ''  Cylinder Diag ''
        palFixCylinderOn.BackgroundImage = My.Resources.Grey
        palFixCylinderOff.BackgroundImage = My.Resources.Grey
        palStopper2Up.BackgroundImage = My.Resources.Grey
        palStopper2Down.BackgroundImage = My.Resources.Grey
        palChuckUp.BackgroundImage = My.Resources.Grey
        palChuckDown.BackgroundImage = My.Resources.Grey

        palEPValve1.BackgroundImage = My.Resources.Grey
        palEPValve1.BackgroundImage = My.Resources.Grey
    End Sub
    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        ckbDIAlarm.Checked = True
        ckbCommunication.Checked = True
        ckbEPValve.Checked = True
        ckbLight.Checked = True
        ckbCylinder1.Checked = True
        ckbVacuum.Checked = True
    End Sub

    Private Sub btnDeselectAll_Click(sender As Object, e As EventArgs) Handles btnDeselectAll.Click
        ckbDIAlarm.Checked = False
        ckbCommunication.Checked = False
        ckbEPValve.Checked = False
        ckbLight.Checked = False
        ckbCylinder1.Checked = False
        ckbVacuum.Checked = False
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        'Sue20170627
        gSyslog.Save("[frmDiag]" & vbTab & "[btnBack]" & vbTab & "Click")
        Me.Close()
    End Sub
End Class
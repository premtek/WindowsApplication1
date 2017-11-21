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
Public Class frmDiag350A
    Private Sub frmDiag350A_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MachineName = MachineTypeToString(gSSystemParameter.MachineType)
        Me.Text = "Diagnostic (Machine type = " & MachineName & ")"

        If (gUserLevel = enmUserLevel.eSoftwareMaker Or gUserLevel = enmUserLevel.eAdministrator) Then
            MessageBox.Visible = True
        Else
            MessageBox.Visible = False
        End If
    End Sub
    Private Sub btnDiagRun_Click(sender As Object, e As EventArgs) Handles btnDiagRun.Click

        '20170602按鍵保護
        If btnDiagRun.Enabled = False Then '防連按
            Exit Sub
        End If

        ResetPal()
        MessageBox.Text = ""
        btnDiagRun.Text = resDiag.GetString("Processing.Text")

        btnDiagRun.BackColor = System.Drawing.Color.Yellow
        Threading.Thread.CurrentThread.Join(1000)

        '20170602按鍵保護
        btnDiagRun.Enabled = False
        btnBack.Enabled = False

        ''''''''''''''''
        ''  DI Diag   ''
        ''''''''''''''''
        If ckbDIAlarm.Checked = True Then
            '' Machine A ''
            RefreshPal(palEMO, gDICollection.GetState(enmDI.EMO)) 'EMO異常(3)
            RefreshPal(palCDA, gDICollection.GetState(enmDI.CDA)) '廠務氣壓偵測(1)
            RefreshPal(palDoorClose, gDICollection.GetState(enmDI.DoorClose)) ' 開門停機保護(4)
            RefreshPal(palMCMotor, gDICollection.GetState(enmDI.MC2))   'DI-MC2 馬達動力(5)
            RefreshPal(palMCHeater, gDICollection.GetState(enmDI.MC3))  'DI-MC3 加熱電源(6)
            RefreshPal(palMC1, Not gDICollection.GetState(enmDI.MC1))  'DI-MC1 控制電源(7)
            RefreshPal(palPrevAlarm1, gDICollection.GetState(enmDI.BoardAvailable)) 'No1:上站異常(12)
            RefreshPal(palNextAlarm1, gDICollection.GetState(enmDI.MachineReadyToRecieve)) 'No1:下站異常(13)
            RefreshPal(palPrevAlarm2, gDICollection.GetState(enmDI.BoardAvailable2)) 'No2:上站異常(14)
            RefreshPal(palNextAlarm2, gDICollection.GetState(enmDI.MachineReadyToRecieve2)) 'No2:下站異常(15)
            RefreshPal(palLValve1ControllerAlarm, gDICollection.GetState(enmDI.TriggerBoardAlarm1)) '閥1-控制器異常(16)
            RefreshPal(palLValve2ControllerAlarm, gDICollection.GetState(enmDI.TriggerBoardAlarm2)) '閥2-控制器異常(20)
            RefreshPal(palDetectGlueSensor1, gDICollection.GetState(enmDI.DetectSyringeSensor1)) '膠槍1-膠量偵測(18)
            RefreshPal(palDetectGlueSensor2, gDICollection.GetState(enmDI.DetectSyringeSensor2)) '膠槍2-膠量偵測(22)

            '' 點膠作業區1之加熱器異常 ''
            RefreshPal(palWorkingHeaterAlarm1, gDICollection.GetState(enmDI.HeaterAlarm1))
            '' 點膠作業區2之加熱器異常 ''
            RefreshPal(palWorkingHeaterAlarm2, gDICollection.GetState(enmDI.HeaterAlarm7))

        End If

        ' '''''''''''''''''''''''
        ' ''   Communication   ''
        ' '''''''''''''''''''''''
        If ckbCommunication.Checked = True Then
            Dim BalanceValue As Decimal
            Dim HeightValue As String = ""


            ' '' Valve Controller ''  
           
            gTriggerBoardVersion(0) = ""
            gTriggerBoardVersion(1) = ""

            If gTriggerBoard.GetVersion(0, True) = True Then
                gTriggerBoardVersion(0) = gTriggerBoard.Version(0).Value
                MessageBox.Text &= "TRG board1 Ver:" & gTriggerBoardVersion(0) & vbNewLine
                palValveController1.BackgroundImage = My.Resources.li_03
            Else
                MessageBox.Text &= "TRG board1 Ver: Null" & vbNewLine
                palValveController1.BackgroundImage = My.Resources.li_08
            End If

            If gTriggerBoard.GetVersion(1, True) = True Then
                gTriggerBoardVersion(1) = gTriggerBoard.Version(1).Value
                MessageBox.Text &= "TRG board2 Ver:" & gTriggerBoardVersion(1) & vbNewLine
                palValveController2.BackgroundImage = My.Resources.li_03
            Else
                MessageBox.Text &= "TRG board2 Ver: Null" & vbNewLine
                palValveController2.BackgroundImage = My.Resources.li_08
            End If
           

            '' '' Laser Reader ''
            Select Case gSSystemParameter.MeasureType
                Case enmMeasureType.Contact
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", 0, HeightValue, True) = True Then
                        palLaserReader1.BackgroundImage = My.Resources.li_03
                        MessageBox.Text &= "Laser reader1: " & Val(HeightValue) & vbNewLine
                    Else
                        palLaserReader1.BackgroundImage = My.Resources.li_08
                        MessageBox.Text &= "Laser reader1: Null" & vbNewLine
                    End If
                Case enmMeasureType.Laser
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", 0, HeightValue, True) = True Then
                        palLaserReader1.BackgroundImage = My.Resources.li_03
                        MessageBox.Text &= "Laser reader1: " & Val(HeightValue) & vbNewLine
                    Else
                        palLaserReader1.BackgroundImage = My.Resources.li_08
                        MessageBox.Text &= "Laser reader1: Null" & vbNewLine
                    End If
            End Select

            '' '' Balance Scale ''
            If gBalanceCollection.RequestCurrentValue(0, BalanceValue, True) Then
                palBalance.BackgroundImage = My.Resources.li_03
                MessageBox.Text &= "Balance value: " & BalanceValue & vbNewLine
            Else
                palBalance.BackgroundImage = My.Resources.li_08
                MessageBox.Text &= "Balance value: Null" & vbNewLine
            End If
        End If

        '''''''''''''''''''
        ''  Vacuum Diag  ''
        '''''''''''''''''''
        If ckbVacuum.Checked = True Then

            MsgBox(resDiag.GetString("MessageVacuum.Text"), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            '' No.1_Chuck 真空建立(錶頭)''
            If (Unit.A_IsVacuum) Then
                palChuckVacuumReady1.BackgroundImage = My.Resources.li_03
            Else
                RefreshPal(palChuckVacuumReady1, Not Unit.A_VacuumControl(True))
                Unit.A_VacuumControl(False)
            End If

            '' No.2_Chuck 真空建立(錶頭)''
            If (Unit.B_IsVacuum) Then
                palChuckVacuumReady2.BackgroundImage = My.Resources.li_03
            Else
                RefreshPal(palChuckVacuumReady2, Not Unit.B_VacuumControl(True))
                Unit.B_VacuumControl(False)
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
            Dim CurrentReturnValue1, ReturnValue1 As Decimal
            Dim CurrentReturnValue2, ReturnValue2 As Decimal
         
            '' Get current Valve value
            gEPVCollection.GetValue(0, CurrentReturnValue1, True)
            txtbAEPValve1Current.Text = CurrentReturnValue1
            gEPVCollection.GetValue(1, CurrentReturnValue2, True)
            txtbAEPValve2Current.Text = CurrentReturnValue2

            '' Set Valve value
            'gEPVCollection.SetValue(0, Val(txtbAEPValve1Set.Text), True)
            gEPVCollection.SetValue(eEPVPressureType.Syringe, 0, Val(txtbAEPValve1Set.Text), True)
            gEPVCollection.GetValue(0, ReturnValue1, True)
            txtbAEPValve1Return.Text = ReturnValue1
            RefreshPal(palEPValve1, Not (Math.Abs(ReturnValue1 - Val(txtbAEPValve1Set.Text)) < 0.05))
            'gEPVCollection.SetValue(1, Val(txtbAEPValve2Set.Text), True)
            gEPVCollection.SetValue(eEPVPressureType.Syringe, 1, Val(txtbAEPValve2Set.Text), True)

            gEPVCollection.GetValue(1, ReturnValue2, True)
            txtbAEPValve2Return.Text = ReturnValue2
            RefreshPal(palEPValve2, Not (Math.Abs(ReturnValue2 - Val(txtbAEPValve2Set.Text)) < 0.05))

            '' Recover Valve value
            gEPVCollection.SetValue(0, CurrentReturnValue1, True)
            gEPVCollection.SetValue(1, CurrentReturnValue2, True)

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


        '*******************************************************************************************'
        ''''''''''''''''''''
        ''   Heater Diag  ''
        ''''''''''''''''''''
        If ckbHeater.Checked = True Then

            If IsNothing(Unit.TempController.A1_PidController.PV) Then
                txtbAHeater1Value.Text = "Null"
                palHeater1Error.BackgroundImage = My.Resources.li_08
            Else
                txtbAHeater1Value.Text = Unit.TempController.A1_PidController.PV
                palHeater1Error.BackgroundImage = My.Resources.li_03
            End If
            If IsNothing(Unit.TempController.B1_PidController.PV) Then
                txtbAHeater2Value.Text = "Null"
                palHeater2Error.BackgroundImage = My.Resources.li_08
            Else
                txtbAHeater2Value.Text = Unit.TempController.B1_PidController.PV
                palHeater2Error.BackgroundImage = My.Resources.li_03
            End If
        End If
        '==========================================================================================='

        ''''''''''''''''''''
        ''  Cylinder Diag ''
        ''''''''''''''''''''

        If ckbCylinder1.Checked = True Then

            ' Stopper Up/Down
            lblStopper1Up.BackColor = System.Drawing.Color.Yellow
            gDOCollection.SetState(enmDO.Station2StopperUp, True)  ' Stopper1 Up(34)
            Threading.Thread.CurrentThread.Join(1000)
            lblStopper1Up.BackColor = System.Drawing.SystemColors.ControlLightLight

            If (gDICollection.GetState(enmDI.Station2StopperUpReady)) Then
                palStopper1Up.BackgroundImage = My.Resources.li_03
            Else
                palStopper1Up.BackgroundImage = My.Resources.li_08
            End If


            lblStopper1Down.BackColor = System.Drawing.Color.Yellow
            gDOCollection.SetState(enmDO.Station2StopperUp, False) ' A Stopper1 Down
            Threading.Thread.CurrentThread.Join(1000)
            lblStopper1Down.BackColor = System.Drawing.SystemColors.ControlLightLight

            If (gDICollection.GetState(enmDI.Station2StopperDownReady)) Then
                palStopper1Down.BackgroundImage = My.Resources.li_03
            Else
                palStopper1Down.BackgroundImage = My.Resources.li_08
            End If

            ' Chuck Up/Down
            If MsgBox(resDiag.GetString("MessageChuck_1.Text"), MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = MsgBoxResult.Yes Then  ''Please confirm if the product on the chuck is removed ?
                lblChuck1Up.BackColor = System.Drawing.Color.Yellow
                gDOCollection.SetState(enmDO.HeaterCylinderUp1, True) '頂升汽缸上升
                gDOCollection.SetState(enmDO.HeaterCylinderDown1, False)
                Threading.Thread.CurrentThread.Join(5000)
                RefreshPal(palChuckUp1, Not gDICollection.GetState(enmDI.Station2Heater1CylinderUpReady))
                lblChuck1Up.BackColor = System.Drawing.SystemColors.ControlLightLight


                lblChuck1Down.BackColor = System.Drawing.Color.Yellow
                gDOCollection.SetState(enmDO.HeaterCylinderDown1, True) '頂升汽缸下降
                gDOCollection.SetState(enmDO.HeaterCylinderUp1, False)
                Threading.Thread.CurrentThread.Join(5000)
                RefreshPal(palChuckDown1, Not gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady))
                lblChuck1Down.BackColor = System.Drawing.SystemColors.ControlLightLight
            Else
                MsgBox(resDiag.GetString("MessageChuck_1.Text"), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) ''Please remove product  before checking cylinder?
            End If

        End If


        '*******************************************************************************************'
        If ckbCylinder2.Checked = True Then

            ' Stopper Up/Down
            lblStopper2Up.BackColor = System.Drawing.Color.Yellow
            gDOCollection.SetState(enmDO.Station3StopperUp, True) ' A Stopper2 Up
            Threading.Thread.CurrentThread.Join(1000)
            lblStopper2Up.BackColor = System.Drawing.SystemColors.ControlLightLight
            If (gDICollection.GetState(enmDI.Station3StopperUpReady)) Then
                palStopper2Up.BackgroundImage = My.Resources.li_03
            Else
                palStopper2Up.BackgroundImage = My.Resources.li_08
            End If

            lblStopper2Down.BackColor = System.Drawing.Color.Yellow
            gDOCollection.SetState(enmDO.Station3StopperUp, False)  '' A Stopper2 Down
            Threading.Thread.CurrentThread.Join(1000)
            lblStopper2Down.BackColor = System.Drawing.SystemColors.ControlLightLight
            If (gDICollection.GetState(enmDI.Station3StopperDownReady)) Then
                palStopper2Down.BackgroundImage = My.Resources.li_03
            Else
                palStopper2Down.BackgroundImage = My.Resources.li_08
            End If

            ' Chuck Up/Down
            If MsgBox(resDiag.GetString("MessageChuck_2.Text"), MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = MsgBoxResult.Yes Then  ''Please confirm if the product on the chuck is removed ?
                lblChuck2Up.BackColor = System.Drawing.Color.Yellow
                gDOCollection.SetState(enmDO.HeaterCylinderUp7, True) '頂升汽缸上升
                gDOCollection.SetState(enmDO.HeaterCylinderDown7, False)
                Threading.Thread.CurrentThread.Join(5000)
                RefreshPal(palChuckUp2, Not gDICollection.GetState(enmDI.Station3Heater1CylinderUpReady))
                lblChuck2Up.BackColor = System.Drawing.SystemColors.ControlLightLight

                lblChuck2Down.BackColor = System.Drawing.Color.Yellow
                gDOCollection.SetState(enmDO.HeaterCylinderDown7, True) '頂升汽缸下降
                gDOCollection.SetState(enmDO.HeaterCylinderUp7, False)
                Threading.Thread.CurrentThread.Join(5000)
                RefreshPal(palChuckDown2, Not gDICollection.GetState(enmDI.Station3Heater1CylinderDownReady))
                lblChuck2Down.BackColor = System.Drawing.SystemColors.ControlLightLight
            Else
                MsgBox(resDiag.GetString("MessageChuck_2.Text"), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) ''Please remove product  before checking cylinder?
            End If

        End If

        btnDiagRun.BackColor = System.Drawing.SystemColors.Control
        btnDiagRun.Text = resDiag.GetString("Run.Text")

        '20170602按鍵保護
        btnDiagRun.Enabled = True
        btnBack.Enabled = True

    End Sub
    Sub RefreshPal(ByRef palLight As Panel, ByVal blnIsError As Boolean)

        If blnIsError Then
            palLight.BackgroundImage = My.Resources.li_08
        Else
            palLight.BackgroundImage = My.Resources.li_03
        End If
    End Sub
    Sub ResetPal()
        ''  DI Diag   ''
        palEMO.BackgroundImage = My.Resources.Grey
        palCDA.BackgroundImage = My.Resources.Grey
        palDoorClose.BackgroundImage = My.Resources.Grey
        palMCMotor.BackgroundImage = My.Resources.Grey
        palMCHeater.BackgroundImage = My.Resources.Grey
        palMC1.BackgroundImage = My.Resources.Grey
        palPrevAlarm1.BackgroundImage = My.Resources.Grey
        palNextAlarm1.BackgroundImage = My.Resources.Grey
        palPrevAlarm2.BackgroundImage = My.Resources.Grey
        palNextAlarm2.BackgroundImage = My.Resources.Grey
        palLValve1ControllerAlarm.BackgroundImage = My.Resources.Grey
        palLValve2ControllerAlarm.BackgroundImage = My.Resources.Grey
        palDetectGlueSensor1.BackgroundImage = My.Resources.Grey
        palDetectGlueSensor2.BackgroundImage = My.Resources.Grey
        palWorkingHeaterAlarm1.BackgroundImage = My.Resources.Grey
        palWorkingHeaterAlarm2.BackgroundImage = My.Resources.Grey

        ''   Communication  ''
        palValveController1.BackgroundImage = My.Resources.Grey
        palValveController2.BackgroundImage = My.Resources.Grey
        palLaserReader1.BackgroundImage = My.Resources.Grey
        palBalance.BackgroundImage = My.Resources.Grey

        ''   Vacuum Diag  ''
        palChuckVacuumReady1.BackgroundImage = My.Resources.Grey
        palChuckVacuumReady2.BackgroundImage = My.Resources.Grey
        palPurgeVacuum1.BackgroundImage = My.Resources.Grey

        ''   EP Valve Diag  ''
        palEPValve1.BackgroundImage = My.Resources.Grey
        palEPValve2.BackgroundImage = My.Resources.Grey
        txtbAEPValve1Current.Text = ""
        txtbAEPValve1Return.Text = ""
        txtbAEPValve2Current.Text = ""
        txtbAEPValve2Return.Text = ""

        ''   Light Diag  '' 
        palLight1.BackgroundImage = My.Resources.Grey
        palLight2.BackgroundImage = My.Resources.Grey
        palLight3.BackgroundImage = My.Resources.Grey
        palLight4.BackgroundImage = My.Resources.Grey
        txtbLight1Current.Text = ""
        txtbLight2Current.Text = ""
        txtbLight3Current.Text = ""
        txtbLight4Current.Text = ""
        txtbLight1Return.Text = ""
        txtbLight2Return.Text = ""
        txtbLight3Return.Text = ""
        txtbLight4Return.Text = ""

        ''   Heater Diag  '' 
        palHeater1Error.BackgroundImage = My.Resources.Grey
        palHeater2Error.BackgroundImage = My.Resources.Grey

        ''  Cylinder Diag ''       
        palStopper1Up.BackgroundImage = My.Resources.Grey
        palStopper1Down.BackgroundImage = My.Resources.Grey
        palChuckUp1.BackgroundImage = My.Resources.Grey
        palChuckDown1.BackgroundImage = My.Resources.Grey
        palStopper2Up.BackgroundImage = My.Resources.Grey
        palStopper2Down.BackgroundImage = My.Resources.Grey
        palChuckUp2.BackgroundImage = My.Resources.Grey
        palChuckDown2.BackgroundImage = My.Resources.Grey
    End Sub
    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click

        ckbDIAlarm.Checked = True
        ckbCommunication.Checked = True
        ckbEPValve.Checked = True
        ckbLight.Checked = True
        ckbCylinder1.Checked = True
        ckbVacuum.Checked = True
        ckbHeater.Checked = True
        ckbCylinder2.Checked = True

    End Sub

    Private Sub btnDeselectAll_Click(sender As Object, e As EventArgs) Handles btnDeselectAll.Click

        ckbDIAlarm.Checked = False
        ckbCommunication.Checked = False
        ckbEPValve.Checked = False
        ckbLight.Checked = False
        ckbCylinder1.Checked = False
        ckbVacuum.Checked = False
        ckbHeater.Checked = False
        ckbCylinder2.Checked = False


    End Sub
    Private Sub txtbLight1Set_TextChanged(sender As Object, e As EventArgs) Handles txtbLight1Set.TextChanged
        If CInt(txtbLight1Set.Text) > 100 Then
            txtbLight1Set.Text = "100"
        End If
    End Sub
    Private Sub txtbLight2Set_TextChanged(sender As Object, e As EventArgs) Handles txtbLight2Set.TextChanged
        If CInt(txtbLight2Set.Text) > 100 Then
            txtbLight2Set.Text = "100"
        End If
    End Sub
    Private Sub txtbLight3Set_TextChanged(sender As Object, e As EventArgs) Handles txtbLight3Set.TextChanged
        If CInt(txtbLight3Set.Text) > 100 Then
            txtbLight3Set.Text = "100"
        End If
    End Sub
    Private Sub txtbLight4Set_TextChanged(sender As Object, e As EventArgs) Handles txtbLight4Set.TextChanged
        If CInt(txtbLight4Set.Text) > 100 Then
            txtbLight4Set.Text = "100"
        End If
    End Sub
    Private Sub txtbLightSet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbLight1Set.KeyPress, txtbLight4Set.KeyPress, txtbLight3Set.KeyPress, txtbLight2Set.KeyPress
        Premtek.Base.CKeyPress.CheckUInteger(sender, e)
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub
End Class
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


Public Class frmDiagWetco

    Private Sub frmDiagWetco_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub
    Private Sub frmDiag_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'gUserLevel = enmUserLevel.eAdministrator
        'gUserLevel = enmUserLevel.eSoftwareMaker
        If gSSystemParameter.MachineMax = eSys.MachineA Then
            ckbMachineB.Visible = False
            ckbMachineB.Checked = False
            palBBalance.Visible = False
            palBCDA.Visible = False
            palBChuckVacuumReady.Visible = False
            palBCylinderNo1Down.Visible = False
            palBCylinderNo1Up.Visible = False
            palBCylinderNo2Down.Visible = False
            palBCylinderNo2Up.Visible = False
            palBCylinderNo3Down.Visible = False
            palBCylinderNo3Up.Visible = False
            palBCylinderNo4Down.Visible = False
            palBCylinderNo4Up.Visible = False
            palBCylinderNo5Down.Visible = False
            palBCylinderNo5Up.Visible = False
            palBCylinderNo6Down.Visible = False
            palBCylinderNo6Up.Visible = False
            palBDoorClose.Visible = False
            palBECylinderDown.Visible = False
            palBECylinderUp.Visible = False
            palBEMS.Visible = False
            palBEPValve1.Visible = False
            palBEPValve2.Visible = False
            palBHeater1Error.Visible = False
            palBHeater2Error.Visible = False
            palBHeater3Error.Visible = False
            palBHeater4Error.Visible = False
            palBHeater5Error.Visible = False
            palBHeater6Error.Visible = False
            palBLaserReader1.Visible = False
            palBLaserReader2.Visible = False
            palBLDetectGlueSensor.Visible = False
            palBLValveControllerAlarm.Visible = False
            palBMCHeater.Visible = False
            palBMCMotor.Visible = False
            palBMoveInMotorAlarm.Visible = False
            palBNextAlarm.Visible = False
            palBOverTemperature1.Visible = False
            palBOverTemperature2.Visible = False
            palBOverTemperature3.Visible = False
            palBOverTemperature4.Visible = False
            palBOverTemperature5.Visible = False
            palBOverTemperature6.Visible = False
            palBPrevAlarm.Visible = False
            palBPurgeVacuum1.Visible = False
            palBPurgeVacuum2.Visible = False
            palBRDetectGlueSensor.Visible = False
            palBRValveControllerAlarm.Visible = False
            palBStopper2Down.Visible = False
            palBStopper2Up.Visible = False
            palBTriggerBoard1.Visible = False
            palBTriggerBoard2.Visible = False
            palBValveController1.Visible = False
            palBValveController2.Visible = False
            palBWorkingHeaterAlarm1.Visible = False
            palBWorkingHeaterAlarm2.Visible = False
            palBWorkingHeaterAlarm3.Visible = False
            palBWorkingHeaterAlarm4.Visible = False
            palBWorkingHeaterAlarm5.Visible = False
            palBWorkingHeaterAlarm6.Visible = False

            Label63.Visible = False
            Label69.Visible = False
            Label1.Visible = False
            Label25.Visible = False
            Label79.Visible = False
            Label40.Visible = False
            Label35.Visible = False
            Label42.Visible = False
            Label44.Visible = False
            Label46.Visible = False
            Label48.Visible = False
            Label81.Visible = False
            Label6.Visible = False
        ElseIf gSSystemParameter.MachineMax = eSys.MachineB Then
            ckbMachineB.Visible = True
            palBBalance.Visible = True
            palBCDA.Visible = True
            palBChuckVacuumReady.Visible = True
            palBCylinderNo1Down.Visible = True
            palBCylinderNo1Up.Visible = True
            palBCylinderNo2Down.Visible = True
            palBCylinderNo2Up.Visible = True
            palBCylinderNo3Down.Visible = True
            palBCylinderNo3Up.Visible = True
            palBCylinderNo4Down.Visible = True
            palBCylinderNo4Up.Visible = True
            palBCylinderNo5Down.Visible = True
            palBCylinderNo5Up.Visible = True
            palBCylinderNo6Down.Visible = True
            palBCylinderNo6Up.Visible = True
            palBDoorClose.Visible = True
            palBECylinderDown.Visible = True
            palBECylinderUp.Visible = True
            palBEMS.Visible = True
            palBEPValve1.Visible = True
            palBEPValve2.Visible = True
            palBHeater1Error.Visible = True
            palBHeater2Error.Visible = True
            palBHeater3Error.Visible = True
            palBHeater4Error.Visible = True
            palBHeater5Error.Visible = True
            palBHeater6Error.Visible = True
            palBLaserReader1.Visible = True
            palBLaserReader2.Visible = True
            palBLDetectGlueSensor.Visible = True
            palBLValveControllerAlarm.Visible = True
            palBMCHeater.Visible = True
            palBMCMotor.Visible = True
            palBMoveInMotorAlarm.Visible = True
            palBNextAlarm.Visible = True
            palBOverTemperature1.Visible = True
            palBOverTemperature2.Visible = True
            palBOverTemperature3.Visible = True
            palBOverTemperature4.Visible = True
            palBOverTemperature5.Visible = True
            palBOverTemperature6.Visible = True
            palBPrevAlarm.Visible = True
            palBPurgeVacuum1.Visible = True
            palBPurgeVacuum2.Visible = True
            palBRDetectGlueSensor.Visible = True
            palBRValveControllerAlarm.Visible = True
            palBStopper2Down.Visible = True
            palBStopper2Up.Visible = True
            palBTriggerBoard1.Visible = True
            palBTriggerBoard2.Visible = True
            palBValveController1.Visible = True
            palBValveController2.Visible = True
            palBWorkingHeaterAlarm1.Visible = True
            palBWorkingHeaterAlarm2.Visible = True
            palBWorkingHeaterAlarm3.Visible = True
            palBWorkingHeaterAlarm4.Visible = True
            palBWorkingHeaterAlarm5.Visible = True
            palBWorkingHeaterAlarm6.Visible = True

            Label63.Visible = True
            Label69.Visible = True
            Label1.Visible = True
            Label25.Visible = True
            Label79.Visible = True
            Label40.Visible = True
            Label35.Visible = True
            Label42.Visible = True
            Label44.Visible = True
            Label46.Visible = True
            Label48.Visible = True
            Label81.Visible = True
            Label6.Visible = True
        End If
        MachineName = MachineTypeToString(gSSystemParameter.MachineType)
        Me.Text = "Diagnostic (Machine type = " & MachineName & ")"

        If (gUserLevel = enmUserLevel.eSoftwareMaker Or gUserLevel = enmUserLevel.eAdministrator) Then
            ckbCylinder2.Enabled = True
            MessageBox.Visible = True
        Else
            ckbCylinder2.Checked = False
            ckbCylinder2.Enabled = False
            MessageBox.Visible = False
        End If
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub btnDiagRun_Click(sender As Object, e As EventArgs) Handles btnDiagRun.Click
        'Sue20170627
        gSyslog.Save("[frmDiagWetco]" & vbTab & "[btnDiagRun]" & vbTab & "Click")
        '20170602按鍵保護
        If btnDiagRun.Enabled = False Then '防連按
            Exit Sub
        End If

        ResetPal()
        MessageBox.Text = ""
        btnDiagRun.Text = resDiagWetco.GetString("Processing.Text")
        'btnDiagRun.Text = "Processing"
        btnDiagRun.BackColor = System.Drawing.Color.Yellow
        Threading.Thread.CurrentThread.Join(1000)

        '20170602按鍵保護
        btnDiagRun.Enabled = False
        btnBack.Enabled = False

        'MsgBox(gMsgHandler.GetMessage(Error_1032000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        ''''''''''''''''
        ''  DI Diag   ''
        ''''''''''''''''
        If ckbDIAlarm.Checked = True Then
            '' Machine A ''
            If ckbMachineA.Checked = True Then
                RefreshPal(palAEMS, gDICollection.GetState(enmDI.EMS)) 'EMS異常
                RefreshPal(palACDA, gDICollection.GetState(enmDI.CDA)) '廠務氣壓偵測
                RefreshPal(palADoorClose, gDICollection.GetState(enmDI.DoorClose)) ' 開門停機保護
                '    RefreshPal(palAMCSystem, gDICollection.GetState(enmDI.MC1))  'DI-MC1 控制電源 
                RefreshPal(palAMCMotor, gDICollection.GetState(enmDI.MC2))   'DI-MC2 馬達動力
                RefreshPal(palAMCHeater, gDICollection.GetState(enmDI.MC3))  'DI-MC3 加熱電源
                RefreshPal(palAPrevAlarm, gDICollection.GetState(enmDI.PrevAlarm)) 'B:上站異常
                RefreshPal(palANextAlarm, gDICollection.GetState(enmDI.NextAlarm)) 'B:下站異常
                RefreshPal(palALValveControllerAlarm, gDICollection.GetState(enmDI.ValveControllerAlarm1)) 'L 噴射閥控制器異常
                RefreshPal(palARValveControllerAlarm, gDICollection.GetState(enmDI.ValveControllerAlarm2)) 'R 噴射閥控制器異常
                RefreshPal(palALDetectGlueSensor, Not gDICollection.GetState(enmDI.DetectSyringeSensor1)) 'L 膠槍膠量偵測
                RefreshPal(palARDetectGlueSensor, Not gDICollection.GetState(enmDI.DetectSyringeSensor2)) 'R 膠槍膠量偵測 
                RefreshPal(palAMoveInMotorAlarm, gDICollection.GetState(enmDI.MoveInMotorAlarm2))  'B:入料馬達 異常
                '' 加熱區溫度Interlock異常 ''
                RefreshPal(palAOverTemperature1, gDICollection.GetState(enmDI.OverTemperature))
                RefreshPal(palAOverTemperature2, gDICollection.GetState(enmDI.OverTemperature2))
                RefreshPal(palAOverTemperature3, gDICollection.GetState(enmDI.OverTemperature3))
                RefreshPal(palAOverTemperature4, gDICollection.GetState(enmDI.OverTemperature4))
                RefreshPal(palAOverTemperature5, gDICollection.GetState(enmDI.OverTemperature5))
                RefreshPal(palAOverTemperature6, gDICollection.GetState(enmDI.OverTemperature6))
                '' 點膠作業區1之加熱器異常 ''
                RefreshPal(palAWorkingHeaterAlarm1, gDICollection.GetState(enmDI.HeaterAlarm1))
                RefreshPal(palAWorkingHeaterAlarm2, gDICollection.GetState(enmDI.HeaterAlarm2))
                RefreshPal(palAWorkingHeaterAlarm3, gDICollection.GetState(enmDI.HeaterAlarm3))
                RefreshPal(palAWorkingHeaterAlarm4, gDICollection.GetState(enmDI.HeaterAlarm4))
                RefreshPal(palAWorkingHeaterAlarm5, gDICollection.GetState(enmDI.HeaterAlarm5))
                RefreshPal(palAWorkingHeaterAlarm6, gDICollection.GetState(enmDI.HeaterAlarm6))
            End If
            '' Machine B ''
            If ckbMachineB.Checked = True Then
                RefreshPal(palBEMS, gDICollection.GetState(enmDI.EMS2)) 'EMS異常     
                RefreshPal(palBCDA, gDICollection.GetState(enmDI.CDA2)) '廠務氣壓偵測
                RefreshPal(palBDoorClose, gDICollection.GetState(enmDI.DoorClose2)) '開門停機保護 
                RefreshPal(palBMCMotor, gDICollection.GetState(enmDI.MC_Motor2))    'DI-MC2 馬達動力
                RefreshPal(palBMCHeater, gDICollection.GetState(enmDI.MC_Heater2))  'DI-MC3 加熱電源
                RefreshPal(palBPrevAlarm, gDICollection.GetState(enmDI.PrevAlarm2)) 'B:上站異常
                RefreshPal(palBNextAlarm, gDICollection.GetState(enmDI.NextAlarm2)) 'B:下站異常
                RefreshPal(palBLValveControllerAlarm, gDICollection.GetState(enmDI.ValveControllerAlarm3)) 'L 噴射閥控制器異常
                RefreshPal(palBRValveControllerAlarm, gDICollection.GetState(enmDI.ValveControllerAlarm4)) 'R 噴射閥控制器異常
                RefreshPal(palBLDetectGlueSensor, Not gDICollection.GetState(enmDI.DetectSyringeSensor3)) 'L 膠槍膠量偵測
                RefreshPal(palBRDetectGlueSensor, Not gDICollection.GetState(enmDI.DetectSyringeSensor4)) 'R 膠槍膠量偵測
                RefreshPal(palBMoveInMotorAlarm, gDICollection.GetState(enmDI.MoveInMotorAlarm2))  'B:入料馬達 異常 
                '' 加熱區溫度Interlock異常 ''
                RefreshPal(palBOverTemperature1, gDICollection.GetState(enmDI.OverTemperature7))
                RefreshPal(palBOverTemperature2, gDICollection.GetState(enmDI.OverTemperature8))
                RefreshPal(palBOverTemperature3, gDICollection.GetState(enmDI.OverTemperature9))
                RefreshPal(palBOverTemperature4, gDICollection.GetState(enmDI.OverTemperature10))
                RefreshPal(palBOverTemperature5, gDICollection.GetState(enmDI.OverTemperature11))
                RefreshPal(palBOverTemperature6, gDICollection.GetState(enmDI.OverTemperature12))
                '' 點膠作業區1之加熱器異常 ''
                RefreshPal(palBWorkingHeaterAlarm1, gDICollection.GetState(enmDI.HeaterAlarm7))
                RefreshPal(palBWorkingHeaterAlarm2, gDICollection.GetState(enmDI.HeaterAlarm8))
                RefreshPal(palBWorkingHeaterAlarm3, gDICollection.GetState(enmDI.HeaterAlarm9))
                RefreshPal(palBWorkingHeaterAlarm4, gDICollection.GetState(enmDI.HeaterAlarm10))
                RefreshPal(palBWorkingHeaterAlarm5, gDICollection.GetState(enmDI.HeaterAlarm11))
                RefreshPal(palBWorkingHeaterAlarm6, gDICollection.GetState(enmDI.HeaterAlarm12))
            End If
        End If

        ' '''''''''''''''''''''''
        ' ''   Communication   ''
        ' '''''''''''''''''''''''
        If ckbCommunication.Checked = True Then

            Dim BalanceValue As Decimal
            Dim HeightValue As String = ""
            '' Machine A ''
            If ckbMachineA.Checked = True Then
                ' '' Valve Controller ''         
                If gValvecontrollerCollection.SetValvePower(0, True) = enmCommandState.Success Then
                    palAValveController1.BackgroundImage = My.Resources.li_03
                Else
                    palAValveController1.BackgroundImage = My.Resources.li_08
                End If
                If gValvecontrollerCollection.SetValvePower(1, True) = enmCommandState.Success Then
                    palAValveController2.BackgroundImage = My.Resources.li_03
                Else
                    palAValveController2.BackgroundImage = My.Resources.li_08
                End If
                ' '' Trigger Board ''
                gTriggerBoardVersion(0) = ""
                gTriggerBoardVersion(1) = ""
                If gTriggerBoard.GetVersion(0, True) = True Then
                    gTriggerBoardVersion(0) = gTriggerBoard.Version(0).Value
                    MessageBox.Text &= "A: TRG board1 Ver:" & gTriggerBoardVersion(0) & vbNewLine
                    palATriggerBoard1.BackgroundImage = My.Resources.li_03
                Else
                    MessageBox.Text &= "A: TRG board1 Ver: Null" & vbNewLine
                    palATriggerBoard1.BackgroundImage = My.Resources.li_08
                End If
                If gTriggerBoard.GetVersion(1, True) = True Then
                    gTriggerBoardVersion(1) = gTriggerBoard.Version(1).Value
                    MessageBox.Text &= "A: TRG board2 Ver:" & gTriggerBoardVersion(1) & vbNewLine
                    palATriggerBoard2.BackgroundImage = My.Resources.li_03
                Else
                    MessageBox.Text &= "A: TRG board2 Ver: Null" & vbNewLine
                    palATriggerBoard2.BackgroundImage = My.Resources.li_08
                End If
                '' '' Laser Reader ''
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", 0, HeightValue, True) = True Then
                    palALaserReader1.BackgroundImage = My.Resources.li_03
                    MessageBox.Text &= "A: Laser reader1: " & Val(HeightValue) & vbNewLine
                Else
                    palALaserReader1.BackgroundImage = My.Resources.li_08
                    MessageBox.Text &= "A: Laser reader1: Null" & vbNewLine
                End If
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", 1, HeightValue, True) = True Then
                    palALaserReader2.BackgroundImage = My.Resources.li_03
                    MessageBox.Text &= "A: Laser reader2: " & Val(HeightValue) & vbNewLine
                Else
                    palALaserReader2.BackgroundImage = My.Resources.li_08
                    MessageBox.Text &= "A: Laser reader2: Null" & vbNewLine
                End If
                '' '' Balance Scale ''
                If gBalanceCollection.RequestCurrentValue(0, BalanceValue, True) Then
                    palABalance.BackgroundImage = My.Resources.li_03
                    MessageBox.Text &= "A: Balance value: " & BalanceValue & vbNewLine
                Else
                    palABalance.BackgroundImage = My.Resources.li_08
                    MessageBox.Text &= "A: Balance value: Null" & vbNewLine
                End If
            End If
            '' Machine B ''
            If ckbMachineB.Checked = True Then
                ' '' Valve Controller ''         
                If gValvecontrollerCollection.SetValvePower(2, True) = enmCommandState.Success Then
                    palBValveController1.BackgroundImage = My.Resources.li_03
                Else
                    palBValveController1.BackgroundImage = My.Resources.li_08
                End If
                If gValvecontrollerCollection.SetValvePower(3, True) = enmCommandState.Success Then
                    palBValveController2.BackgroundImage = My.Resources.li_03
                Else
                    palBValveController2.BackgroundImage = My.Resources.li_08
                End If
                '' '' Trigger Board ''
                gTriggerBoardVersion(2) = ""
                gTriggerBoardVersion(3) = ""
                If gTriggerBoard.GetVersion(2, True) = True Then
                    gTriggerBoardVersion(2) = gTriggerBoard.Version(2).Value
                    MessageBox.Text &= "B: TRG board1 Ver:" & gTriggerBoardVersion(2) & vbNewLine
                    palBTriggerBoard1.BackgroundImage = My.Resources.li_03
                Else
                    MessageBox.Text &= "B: TRG board1 Ver: Null" & vbNewLine
                    palBTriggerBoard1.BackgroundImage = My.Resources.li_08
                End If

                If gTriggerBoard.GetVersion(3, True) = True Then
                    gTriggerBoardVersion(3) = gTriggerBoard.Version(3).Value
                    MessageBox.Text &= "B: TRG board2 Ver:" & gTriggerBoardVersion(3) & vbNewLine
                    palBTriggerBoard2.BackgroundImage = My.Resources.li_03
                Else
                    MessageBox.Text &= "B: TRG board2 Ver: Null" & vbNewLine
                    palBTriggerBoard2.BackgroundImage = My.Resources.li_08
                End If
                '' '' Laser Reader ''
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", 2, HeightValue, True) = True Then
                    palBLaserReader1.BackgroundImage = My.Resources.li_03
                    MessageBox.Text &= "B: Laser reader1: " & HeightValue & vbNewLine
                Else
                    palBLaserReader1.BackgroundImage = My.Resources.li_08
                    MessageBox.Text &= "B: Laser reader1: Null" & vbNewLine
                End If
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", 3, HeightValue, True) = True Then
                    palBLaserReader2.BackgroundImage = My.Resources.li_03
                    MessageBox.Text &= "B: Laser reader2: " & HeightValue & vbNewLine
                Else
                    palBLaserReader2.BackgroundImage = My.Resources.li_08
                    MessageBox.Text &= "B: Laser reader2: Null" & vbNewLine
                End If
                '' '' Balance Scale ''
                If gBalanceCollection.RequestCurrentValue(1, BalanceValue, True) Then
                    palBBalance.BackgroundImage = My.Resources.li_03
                    MessageBox.Text &= "B: Balance value: " & BalanceValue & vbNewLine
                Else
                    palBBalance.BackgroundImage = My.Resources.li_08
                    MessageBox.Text &= "B: Balance value: Null" & vbNewLine
                End If

            End If
        End If
        '''''''''''''''''''
        ''  Vacuum Diag  ''
        '''''''''''''''''''
        If ckbVacuum.Checked = True Then
            '' Machine A ''
            If ckbMachineA.Checked = True Then
                MsgBox("A: " & resDiagWetco.GetString("MessageVacuum.Text"), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) 'A: Please setup Vacuum environment
                '' Chuck段真空建立(錶頭) ''
                If (Unit.A_IsVacuum) Then
                    palAChuckVacuumReady.BackgroundImage = My.Resources.li_03
                Else
                    RefreshPal(palAChuckVacuumReady, Not Unit.A_VacuumControl(True))
                    Unit.A_VacuumControl(False)
                End If
                '' Purge Vacuum ''
                If (gDICollection.GetState(enmDI.PurgeVacuumReady)) Then  '' Purge Vacuum left
                    palAPurgeVacuum1.BackgroundImage = My.Resources.li_03
                Else
                    gDOCollection.SetState(enmDO.Purge, True)
                    Threading.Thread.CurrentThread.Join(300)
                    RefreshPal(palAPurgeVacuum1, Not gDICollection.GetState(enmDI.PurgeVacuumReady))
                    gDOCollection.SetState(enmDO.Purge, False)
                End If
                If (gDICollection.GetState(enmDI.PurgeVacuumReady2)) Then  '' Purge Vacuum right
                    palAPurgeVacuum2.BackgroundImage = My.Resources.li_03
                Else
                    gDOCollection.SetState(enmDO.PurgeVacuum2, True)
                    Threading.Thread.CurrentThread.Join(300)
                    RefreshPal(palAPurgeVacuum2, Not gDICollection.GetState(enmDI.PurgeVacuumReady2))
                    gDOCollection.SetState(enmDO.PurgeVacuum2, False)
                End If
            End If
            '' Machine B ''
            If ckbMachineB.Checked = True Then
                MsgBox("B: " & resDiagWetco.GetString("MessageVacuum.Text"), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) 'B: Please setup Vacuum environment
                '' Chuck段真空建立(錶頭) ''
                If (Unit.B_IsVacuum) Then
                    palBChuckVacuumReady.BackgroundImage = My.Resources.li_03
                Else
                    RefreshPal(palBChuckVacuumReady, Not Unit.B_VacuumControl(True))
                    Unit.B_VacuumControl(False)
                End If
                '' Purge Vacuum ''
                If (gDICollection.GetState(enmDI.PurgeVacuumReady3)) Then  '' Purge Vacuum left
                    palBPurgeVacuum1.BackgroundImage = My.Resources.li_03
                Else
                    gDOCollection.SetState(enmDO.PurgeVacuum3, True)
                    Threading.Thread.CurrentThread.Join(300)
                    RefreshPal(palBPurgeVacuum1, Not gDICollection.GetState(enmDI.PurgeVacuumReady3))
                    gDOCollection.SetState(enmDO.PurgeVacuum3, False)
                End If
                If (gDICollection.GetState(enmDI.PurgeVacuumReady4)) Then  '' Purge Vacuum right
                    palBPurgeVacuum2.BackgroundImage = My.Resources.li_03
                Else
                    gDOCollection.SetState(enmDO.PurgeVacuum4, True)
                    Threading.Thread.CurrentThread.Join(300)
                    RefreshPal(palBPurgeVacuum2, Not gDICollection.GetState(enmDI.PurgeVacuumReady4))
                    gDOCollection.SetState(enmDO.PurgeVacuum4, False)
                End If
            End If
        End If
        ''''''''''''''''''''''
        ''   EP Valve Diag  ''
        ''''''''''''''''''''''
        If ckbEPValve.Checked = True Then
            Dim CurrentReturnValue1, CurrentReturnValue2, ReturnValue As Decimal

            ' '' turn on Vacuum
            'gDOCollection.GetSetState(enmDO.GlueNo1SyringePressure) = True
            'gDOCollection.GetSetState(enmDO.GlueNo2SyringePressure) = True
            'gDOCollection.GetSetState(enmDO.SyringePressure3) = True
            'gDOCollection.GetSetState(enmDO.SyringePressure4) = True

            '' Machine A ''
            If ckbMachineA.Checked = True Then
                '' Get current Valve value
                gEPVCollection.GetValue(0, CurrentReturnValue1, True)
                txtbAEPValve1Current.Text = CurrentReturnValue1
                gEPVCollection.GetValue(1, CurrentReturnValue2, True)
                txtbAEPValve2Current.Text = CurrentReturnValue2
                '' Set Valve value
                'gEPVCollection.SetValue(0, Val(txtbAEPValve1Set.Text), True)
                gEPVCollection.SetValue(eEPVPressureType.Syringe, 0, Val(txtbAEPValve1Set.Text), True)
                gEPVCollection.GetValue(0, ReturnValue, True)
                txtbAEPValve1Return.Text = ReturnValue
                RefreshPal(palAEPValve1, Not (Math.Abs(ReturnValue - Val(txtbAEPValve1Set.Text)) < 0.05))
                'gEPVCollection.SetValue(1, Val(txtbAEPValve2Set.Text), True)
                gEPVCollection.SetValue(eEPVPressureType.Syringe, 1, Val(txtbAEPValve2Set.Text), True)
                gEPVCollection.GetValue(1, ReturnValue, True)
                txtbAEPValve2Return.Text = ReturnValue
                RefreshPal(palAEPValve2, Not (Math.Abs(ReturnValue - Val(txtbAEPValve2Set.Text)) < 0.05))
                '' Recover Valve value
                gEPVCollection.SetValue(0, CurrentReturnValue1, True)
                gEPVCollection.SetValue(1, CurrentReturnValue2, True)
            End If
            '' Machine B ''
            If ckbMachineB.Checked = True Then
                '' Get current Valve value
                gEPVCollection.GetValue(2, CurrentReturnValue1, True)
                txtbBEPValve1Current.Text = CurrentReturnValue1
                gEPVCollection.GetValue(3, CurrentReturnValue2, True)
                txtbBEPValve2Current.Text = CurrentReturnValue2
                '' Set Valve value
                'gEPVCollection.SetValue(2, Val(txtbBEPValve1Set.Text), True)
                gEPVCollection.SetValue(eEPVPressureType.Syringe, 2, Val(txtbBEPValve1Set.Text), True)
                gEPVCollection.GetValue(2, ReturnValue, True)
                txtbBEPValve1Return.Text = ReturnValue
                RefreshPal(palBEPValve1, Not (Math.Abs(ReturnValue - Val(txtbBEPValve1Set.Text)) < 0.05))
                'gEPVCollection.SetValue(3, Val(txtbBEPValve2Set.Text), True)
                gEPVCollection.SetValue(eEPVPressureType.Syringe, 3, Val(txtbBEPValve2Set.Text), True)
                gEPVCollection.GetValue(3, ReturnValue, True)
                txtbBEPValve2Return.Text = ReturnValue
                RefreshPal(palBEPValve2, Not (Math.Abs(ReturnValue - Val(txtbBEPValve2Set.Text)) < 0.05))
                '' Recover Valve value
                gEPVCollection.SetValue(2, CurrentReturnValue1, True)
                gEPVCollection.SetValue(3, CurrentReturnValue2, True)
            End If
        End If
        '''''''''''''''''''
        ''   Light Diag  ''
        '''''''''''''''''''
        If ckbLight.Checked = True Then

            Dim LightType As Integer = gLightCollection.Cards(0).CardType
            Dim iLight1CurrentValue, iLight2CurrentValue, iLight3CurrentValue, iLight4CurrentValue, iLight5CurrentValue, iLight6CurrentValue, iLight7CurrentValue, iLight8CurrentValue As Integer
            Dim iLight1ReturnValue, iLight2ReturnValue, iLight3ReturnValue, iLight4ReturnValue, iLight5ReturnValue, iLight6ReturnValue, iLight7ReturnValue, iLight8ReturnValue As Integer

            '' Machine A ''
            If ckbMachineA.Checked = True Then
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
            '' Machine B ''
            If ckbMachineB.Checked = True Then
                '' turn on light control 
                gDOCollection.SetState(enmDO.CCDLight5, True)
                gDOCollection.SetState(enmDO.CCDLight6, True)
                gDOCollection.SetState(enmDO.CCDLight7, True)
                gDOCollection.SetState(enmDO.CCDLight8, True)
                '' Get light current value
                gLightCollection.Items(1).GetLightValue(0, iLight5CurrentValue, True)
                gLightCollection.Items(1).GetLightValue(1, iLight6CurrentValue, True)
                gLightCollection.Items(1).GetLightValue(2, iLight7CurrentValue, True)
                gLightCollection.Items(1).GetLightValue(3, iLight8CurrentValue, True)
                txtbLight5Current.Text = iLight5CurrentValue
                txtbLight6Current.Text = iLight6CurrentValue
                txtbLight7Current.Text = iLight7CurrentValue
                txtbLight8Current.Text = iLight8CurrentValue
                ' '' Set light value
                gLightCollection.Items(1).SetLightValue(0, Val(txtbLight5Set.Text), True)
                gLightCollection.Items(1).GetLightValue(0, iLight5ReturnValue, True)
                txtbLight5Return.Text = iLight5ReturnValue
                RefreshPal(palLight5, Not (Math.Abs(Val(txtbLight5Set.Text) - iLight5ReturnValue) <= 1))
                gLightCollection.Items(1).SetLightValue(1, Val(txtbLight6Set.Text), True)
                gLightCollection.Items(1).GetLightValue(1, iLight6ReturnValue, True)
                txtbLight6Return.Text = iLight6ReturnValue
                RefreshPal(palLight6, Not (Math.Abs(Val(txtbLight6Set.Text) - iLight6ReturnValue) <= 1))
                gLightCollection.Items(1).SetLightValue(2, Val(txtbLight7Set.Text), True)
                gLightCollection.Items(1).GetLightValue(2, iLight7ReturnValue, True)
                txtbLight7Return.Text = iLight7ReturnValue
                RefreshPal(palLight7, Not (Math.Abs(Val(txtbLight7Set.Text) - iLight7ReturnValue) <= 1))
                gLightCollection.Items(1).SetLightValue(3, Val(txtbLight8Set.Text), True)
                gLightCollection.Items(1).GetLightValue(3, iLight8ReturnValue, True)
                txtbLight8Return.Text = iLight8ReturnValue
                RefreshPal(palLight8, Not (Math.Abs(Val(txtbLight8Set.Text) - iLight8ReturnValue) <= 1))
                '' Recover light value
                gLightCollection.Items(1).SetLightValue(0, iLight5CurrentValue, True)
                gLightCollection.Items(1).SetLightValue(1, iLight6CurrentValue, True)
                gLightCollection.Items(1).SetLightValue(2, iLight7CurrentValue, True)
                gLightCollection.Items(1).SetLightValue(3, iLight8CurrentValue, True)
                ' turn off light control
                gDOCollection.SetState(enmDO.CCDLight5, False)
                gDOCollection.SetState(enmDO.CCDLight6, False)
                gDOCollection.SetState(enmDO.CCDLight7, False)
                gDOCollection.SetState(enmDO.CCDLight8, False)
            End If
        End If
        ''''''''''''''''''''
        ''   Heater Diag  ''
        ''''''''''''''''''''
        If ckbHeater.Checked = True Then
            '' Machine A ''
            If ckbMachineA.Checked = True Then
                If IsNothing(Unit.TempController.A1_PidController.PV) Then
                    txtbAHeater1Value.Text = "Null"
                    palAHeater1Error.BackgroundImage = My.Resources.li_08
                Else
                    txtbAHeater1Value.Text = Unit.TempController.A1_PidController.PV
                    palAHeater1Error.BackgroundImage = My.Resources.li_03
                End If
                If IsNothing(Unit.TempController.A2_PidController.PV) Then
                    txtbAHeater2Value.Text = "Null"
                    palAHeater2Error.BackgroundImage = My.Resources.li_08
                Else
                    txtbAHeater2Value.Text = Unit.TempController.A2_PidController.PV
                    palAHeater2Error.BackgroundImage = My.Resources.li_03
                End If
                If IsNothing(Unit.TempController.A3_PidController.PV) Then
                    txtbAHeater3Value.Text = "Null"
                    palAHeater3Error.BackgroundImage = My.Resources.li_08
                Else
                    txtbAHeater3Value.Text = Unit.TempController.A3_PidController.PV
                    palAHeater3Error.BackgroundImage = My.Resources.li_03
                End If
                If IsNothing(Unit.TempController.A4_PidController.PV) Then
                    txtbAHeater4Value.Text = "Null"
                    palAHeater4Error.BackgroundImage = My.Resources.li_08
                Else
                    txtbAHeater4Value.Text = Unit.TempController.A4_PidController.PV
                    palAHeater4Error.BackgroundImage = My.Resources.li_03
                End If
                If IsNothing(Unit.TempController.A5_PidController.PV) Then
                    txtbAHeater5Value.Text = "Null"
                    palAHeater5Error.BackgroundImage = My.Resources.li_08
                Else
                    txtbAHeater5Value.Text = Unit.TempController.A5_PidController.PV
                    palAHeater5Error.BackgroundImage = My.Resources.li_03
                End If
                If IsNothing(Unit.TempController.A6_PidController.PV) Then
                    txtbAHeater6Value.Text = "Null"
                    palAHeater6Error.BackgroundImage = My.Resources.li_08
                Else
                    txtbAHeater6Value.Text = Unit.TempController.A6_PidController.PV
                    palAHeater6Error.BackgroundImage = My.Resources.li_03
                End If
            End If
            '' Machine B ''
            If ckbMachineB.Checked = True Then
                If IsNothing(Unit.TempController.B1_PidController.PV) Then
                    txtbBHeater1Value.Text = "Null"
                    palBHeater1Error.BackgroundImage = My.Resources.li_08
                Else
                    txtbBHeater1Value.Text = Unit.TempController.B1_PidController.PV
                    palBHeater1Error.BackgroundImage = My.Resources.li_03
                End If
                If IsNothing(Unit.TempController.B2_PidController.PV) Then
                    txtbBHeater2Value.Text = "Null"
                    palBHeater2Error.BackgroundImage = My.Resources.li_08
                Else
                    txtbBHeater2Value.Text = Unit.TempController.B2_PidController.PV
                    palBHeater2Error.BackgroundImage = My.Resources.li_03
                End If
                If IsNothing(Unit.TempController.B3_PidController.PV) Then
                    txtbBHeater3Value.Text = "Null"
                    palBHeater3Error.BackgroundImage = My.Resources.li_08
                Else
                    txtbBHeater3Value.Text = Unit.TempController.B3_PidController.PV
                    palBHeater3Error.BackgroundImage = My.Resources.li_03
                End If
                If IsNothing(Unit.TempController.B4_PidController.PV) Then
                    txtbBHeater4Value.Text = "Null"
                    palBHeater4Error.BackgroundImage = My.Resources.li_08
                Else
                    txtbBHeater4Value.Text = Unit.TempController.B4_PidController.PV
                    palBHeater4Error.BackgroundImage = My.Resources.li_03
                End If
                If IsNothing(Unit.TempController.B5_PidController.PV) Then
                    txtbBHeater5Value.Text = "Null"
                    palBHeater5Error.BackgroundImage = My.Resources.li_08
                Else
                    txtbBHeater5Value.Text = Unit.TempController.B5_PidController.PV
                    palBHeater5Error.BackgroundImage = My.Resources.li_03
                End If
                If IsNothing(Unit.TempController.B6_PidController.PV) Then
                    txtbBHeater6Value.Text = "Null"
                    palBHeater6Error.BackgroundImage = My.Resources.li_08
                Else
                    txtbBHeater6Value.Text = Unit.TempController.B6_PidController.PV
                    palBHeater6Error.BackgroundImage = My.Resources.li_03
                End If
            End If
        End If
        ''''''''''''''''''''
        ''  Cylinder1 Diag ''
        ''''''''''''''''''''
        '  Dim bStatus As Boolean = True

        If ckbCylinder1.Checked = True Then
            ''Machine A
            If ckbMachineA.Checked = True Then
                ' Stopper Up/Down
                lblStopper1Up.BackColor = System.Drawing.Color.Yellow
                gDOCollection.SetState(enmDO.Station1StopperDown, False) ' A Stopper1 Up
                gDOCollection.SetState(enmDO.Station1StopperUpDown, True)
                '     gDOCollection.SetState(enmDO.Station1StopperUpDown) = False  ' A Stopper1 Up
                Threading.Thread.CurrentThread.Join(1000)
                lblStopper1Up.BackColor = System.Drawing.SystemColors.Control
                RefreshPal(palAStopper1Up, Not gDICollection.GetState(enmDI.Station1StopperUpReady))
                lblStopper1Down.BackColor = System.Drawing.Color.Yellow
                gDOCollection.SetState(enmDO.Station1StopperDown, True) ' A Stopper1 Down
                gDOCollection.SetState(enmDO.Station1StopperUpDown, False)
                'gDOCollection.SetState(enmDO.Station1StopperUpDown) = True  ' A Stopper1 Down
                Threading.Thread.CurrentThread.Join(1000)
                lblStopper1Down.BackColor = System.Drawing.SystemColors.Control
                RefreshPal(palAStopper1Down, Not gDICollection.GetState(enmDI.Station1StopperDownReady))

                lblStopper2Up.BackColor = System.Drawing.Color.Yellow
                gDOCollection.SetState(enmDO.Station2StopperDown, False) ' A Stopper2 Up
                gDOCollection.SetState(enmDO.Station2StopperUp, True)
                'gDOCollection.SetState(enmDO.Station2StopperUp) = False  ' A Stopper2 Up
                Threading.Thread.CurrentThread.Join(1000)
                lblStopper2Up.BackColor = System.Drawing.SystemColors.Control
                If (gDICollection.GetState(enmDI.Station2StopperUpReady)) Then
                    palAStopper2Up.BackgroundImage = My.Resources.li_03
                Else
                    palAStopper2Up.BackgroundImage = My.Resources.li_08
                End If
                lblStopper2Down.BackColor = System.Drawing.Color.Yellow
                gDOCollection.SetState(enmDO.Station2StopperDown, True) ' A Stopper2 Down
                gDOCollection.SetState(enmDO.Station2StopperUp, False)
                'gDOCollection.SetState(enmDO.Station2StopperUp) = True  ' A Stopper2 Down
                Threading.Thread.CurrentThread.Join(1000)
                lblStopper2Down.BackColor = System.Drawing.SystemColors.Control
                If (gDICollection.GetState(enmDI.Station2StopperDownReady)) Then
                    palAStopper2Down.BackgroundImage = My.Resources.li_03
                Else
                    palAStopper2Down.BackgroundImage = My.Resources.li_08
                End If

                If MsgBox("A: " & resDiagWetco.GetString("MessageCylinder1_1.Text"), MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = MsgBoxResult.Yes Then  ''Please confirm if the chuck is removed ?
                    '' ECylinder -> Home -> Up -> Down
                    Unit.A_ElectricCylinder(clsUnit.enmDirection.Home) '' A ECylinder -> Home
                    Threading.Thread.CurrentThread.Join(2000)
                    If (Unit.A_ECylinderLocation <> clsUnit.enmLocation.Home) Then
                        '    MsgBox("A: Electric Cylinder Home failed", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        MessageBox.Text &= "A: Electric cylinder Home failed" & vbNewLine
                    End If
                    Threading.Thread.CurrentThread.Join(2000)

                    lblECylinderUp.BackColor = System.Drawing.Color.Yellow
                    Unit.A_ElectricCylinder(clsUnit.enmDirection.Up) '' A ECylinder -> Up
                    Threading.Thread.CurrentThread.Join(2500)
                    If (Unit.A_ECylinderLocation <> clsUnit.enmLocation.Top) Then
                        palAECylinderUp.BackgroundImage = My.Resources.li_08
                    Else
                        palAECylinderUp.BackgroundImage = My.Resources.li_03
                    End If
                    lblECylinderUp.BackColor = System.Drawing.SystemColors.Control

                    lblECylinderDown.BackColor = System.Drawing.Color.Yellow
                    Unit.A_ElectricCylinder(clsUnit.enmDirection.Down) '' A ECylinder -> Down
                    Threading.Thread.CurrentThread.Join(2500)
                    If (Unit.A_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
                        palAECylinderDown.BackgroundImage = My.Resources.li_08
                    Else
                        palAECylinderDown.BackgroundImage = My.Resources.li_03
                    End If
                    lblECylinderDown.BackColor = System.Drawing.SystemColors.Control
                Else
                    MsgBox("A: " & resDiagWetco.GetString("MessageCylinder1_2.Text"), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) ''Please remove the Chuck before checking cylinder1?
                End If
            End If

            'If bStatus Then
            '    '' ECylinder -> Home -> Down
            '    Unit.A_ElectricCylinder(clsUnit.enmDirection.Home) '' A ECylinder -> Home
            '    Threading.Thread.CurrentThread.Join(1000)
            '    If (Unit.A_ECylinderLocation <> clsUnit.enmLocation.Home) Then
            '        '    MsgBox("A: Electric Cylinder Home failed", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            '        TextBox1.Text &= "A: Electric cylinder Home failed" & vbNewLine
            '        bStatus = False
            '    End If
            '    Unit.A_ElectricCylinder(clsUnit.enmDirection.Down) '' A ECylinder -> Down
            '    Threading.Thread.CurrentThread.Join(1000)
            '    If (Unit.A_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
            '        Threading.Thread.CurrentThread.Join(1000)
            '        If (Unit.A_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
            '            '      MsgBox("A: Electric Cylinder Down failed", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            '            TextBox1.Text &= "A: Electric cylinder Down failed" & vbNewLine
            '            bStatus = False
            '        End If
            '    End If
            '    If (gDICollection.GetState(enmDI.Station2TrayReady)) Then  '' A 料盤流入檢知 => check Station2TrayReady when "Stopper2=Down" & "ECylinder=Down"
            '        '     MsgBox("A: Tray Station isn't empty", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            '        TextBox1.Text &= "A: Tray station isn't empty" & vbNewLine
            '        bStatus = False
            '    End If
            'End If
            '
            '    If bStatus Then
            '        '' ECylinder Up/Down
            '        lblECylinderUp.BackColor = System.Drawing.Color.Yellow
            '        Unit.A_ElectricCylinder(clsUnit.enmDirection.Up) '' A ECylinder -> Up
            '        Threading.Thread.CurrentThread.Join(1000)
            '        '        RefreshPal(palAECylinderUp, Not (Unit.A_ECylinderLocation = clsUnit.enmLocation.Top))
            '        If (Unit.A_ECylinderLocation <> clsUnit.enmLocation.Top) Then
            '            Threading.Thread.CurrentThread.Join(1000)
            '            If (Unit.A_ECylinderLocation <> clsUnit.enmLocation.Top) Then
            '                palAECylinderUp.BackgroundImage = My.Resources.li_08
            '                bStatus = False
            '            Else
            '                palAECylinderUp.BackgroundImage = My.Resources.li_03
            '            End If
            '        Else
            '            palAECylinderUp.BackgroundImage = My.Resources.li_03
            '        End If
            '        lblECylinderUp.BackColor = System.Drawing.SystemColors.Control

            '        lblECylinderDown.BackColor = System.Drawing.Color.Yellow
            '        Unit.A_ElectricCylinder(clsUnit.enmDirection.Down) '' A ECylinder -> Down
            '        Threading.Thread.CurrentThread.Join(1000)
            '        ' RefreshPal(palAECylinderDown, Not (Unit.A_ECylinderLocation = clsUnit.enmLocation.Bottom))
            '        If (Unit.A_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
            '            Threading.Thread.CurrentThread.Join(1000)
            '            If (Unit.A_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
            '                palAECylinderDown.BackgroundImage = My.Resources.li_08
            '                bStatus = False
            '            Else
            '                palAECylinderDown.BackgroundImage = My.Resources.li_03
            '            End If
            '        Else
            '            palAECylinderDown.BackgroundImage = My.Resources.li_03
            '        End If
            '        lblECylinderDown.BackColor = System.Drawing.SystemColors.Control
            '    End If
            'End If

            ''Machine B
            If ckbMachineB.Checked = True Then
                '  bStatus = True
                ' Stopper Up/Down
                lblStopper2Up.BackColor = System.Drawing.Color.Yellow
                gDOCollection.SetState(enmDO.Station3StopperDown, False) ' B Stopper2 Up
                gDOCollection.SetState(enmDO.Station3StopperUp, True)
                'gDOCollection.SetState(enmDO.Station3StopperUp) = False  ' B Stopper2 Up
                Threading.Thread.CurrentThread.Join(1000)
                lblStopper2Up.BackColor = System.Drawing.SystemColors.Control
                If (gDICollection.GetState(enmDI.Station3StopperUpReady)) Then
                    palBStopper2Up.BackgroundImage = My.Resources.li_03
                Else
                    palBStopper2Up.BackgroundImage = My.Resources.li_08
                End If
                lblStopper2Down.BackColor = System.Drawing.Color.Yellow
                gDOCollection.SetState(enmDO.Station3StopperDown, True) ' B Stopper2 Down
                gDOCollection.SetState(enmDO.Station3StopperUp, False)
                'gDOCollection.SetState(enmDO.Station3StopperUp) = True  '' B Stopper2 Down
                Threading.Thread.CurrentThread.Join(1000)
                lblStopper2Down.BackColor = System.Drawing.SystemColors.Control
                If (gDICollection.GetState(enmDI.Station3StopperDownReady)) Then
                    palBStopper2Down.BackgroundImage = My.Resources.li_03
                Else
                    palBStopper2Down.BackgroundImage = My.Resources.li_08
                End If

                '' ECylinder -> Home -> Up -> Down
                If MsgBox("B: " & resDiagWetco.GetString("MessageCylinder1_1.Text"), MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = MsgBoxResult.Yes Then  ''Please confirm if the chuck is removed ?
                    Unit.B_ElectricCylinder(clsUnit.enmDirection.Home) '' B ECylinder -> Home
                    Threading.Thread.CurrentThread.Join(2000)
                    If (Unit.B_ECylinderLocation <> clsUnit.enmLocation.Home) Then
                        '   MsgBox("B: Electric Cylinder Home failed", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        MessageBox.Text &= "B: Electric cylinder Home failed" & vbNewLine
                    End If
                    Threading.Thread.CurrentThread.Join(2000)

                    lblECylinderUp.BackColor = System.Drawing.Color.Yellow
                    Unit.B_ElectricCylinder(clsUnit.enmDirection.Up) '' B ECylinder -> Up
                    Threading.Thread.CurrentThread.Join(2500)
                    If (Unit.B_ECylinderLocation <> clsUnit.enmLocation.Top) Then
                        palBECylinderUp.BackgroundImage = My.Resources.li_08
                    Else
                        palBECylinderUp.BackgroundImage = My.Resources.li_03
                    End If
                    lblECylinderUp.BackColor = System.Drawing.SystemColors.Control

                    lblECylinderDown.BackColor = System.Drawing.Color.Yellow
                    Unit.B_ElectricCylinder(clsUnit.enmDirection.Down) '' B ECylinder -> Down
                    Threading.Thread.CurrentThread.Join(2500)
                    If (Unit.B_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
                        palBECylinderDown.BackgroundImage = My.Resources.li_08
                    Else
                        palBECylinderDown.BackgroundImage = My.Resources.li_03
                    End If
                    lblECylinderDown.BackColor = System.Drawing.SystemColors.Control
                Else
                    MsgBox("B: " & resDiagWetco.GetString("MessageCylinder1_2.Text"), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) ''Please remove the Chuck before checking cylinder1?
                End If

                'If bStatus Then
                '    '' ECylinder -> Home -> Down
                '    Unit.B_ElectricCylinder(clsUnit.enmDirection.Home) '' B ECylinder -> Home
                '    Threading.Thread.CurrentThread.Join(1000)
                '    If (Unit.B_ECylinderLocation <> clsUnit.enmLocation.Home) Then
                '        '   MsgBox("B: Electric Cylinder Home failed", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                '        TextBox1.Text &= "B: Electric cylinder Home failed" & vbNewLine
                '        bStatus = False
                '    End If
                '    Unit.B_ElectricCylinder(clsUnit.enmDirection.Down) '' B ECylinder -> Down
                '    Threading.Thread.CurrentThread.Join(1000)
                '    If (Unit.B_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
                '        Threading.Thread.CurrentThread.Join(1000)
                '        If (Unit.B_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
                '            '    MsgBox("B: Electric Cylinder Down failed", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                '            TextBox1.Text &= "B: Electric cylinder Down failed" & vbNewLine
                '            bStatus = False
                '        End If
                '    End If

                '    If (gDICollection.GetState(enmDI.Station3TrayReady)) Then  '' B 料盤流入檢知 => check Station2TrayReady when "Stopper2=Down" & "ECylinder=Down"
                '        '   MsgBox("B: Tray Station isn't empty", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                '        TextBox1.Text &= "B: Tray station isn't empty" & vbNewLine
                '        bStatus = False
                '    End If
                'End If

                'If bStatus Then
                '    '' ECylinder Up/Down
                '    lblECylinderUp.BackColor = System.Drawing.Color.Yellow
                '    Unit.B_ElectricCylinder(clsUnit.enmDirection.Up) '' B ECylinder -> Up
                '    Threading.Thread.CurrentThread.Join(1000)
                '    ' RefreshPal(palBECylinderUp, Not (Unit.B_ECylinderLocation = clsUnit.enmLocation.Top))
                '    If (Unit.B_ECylinderLocation <> clsUnit.enmLocation.Top) Then
                '        Threading.Thread.CurrentThread.Join(1000)
                '        If (Unit.B_ECylinderLocation <> clsUnit.enmLocation.Top) Then
                '            palBECylinderUp.BackgroundImage = My.Resources.li_08
                '            bStatus = False
                '        Else
                '            palBECylinderUp.BackgroundImage = My.Resources.li_03
                '        End If
                '    Else
                '        palBECylinderUp.BackgroundImage = My.Resources.li_03
                '    End If
                '    lblECylinderUp.BackColor = System.Drawing.SystemColors.Control

                '    lblECylinderDown.BackColor = System.Drawing.Color.Yellow
                '    Unit.B_ElectricCylinder(clsUnit.enmDirection.Down) '' B ECylinder -> Down
                '    Threading.Thread.CurrentThread.Join(1000)
                '    ' RefreshPal(palBECylinderDown, Not (Unit.B_ECylinderLocation = clsUnit.enmLocation.Bottom))
                '    If (Unit.B_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
                '        Threading.Thread.CurrentThread.Join(1000)
                '        If (Unit.B_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
                '            palBECylinderDown.BackgroundImage = My.Resources.li_08
                '            bStatus = False
                '        Else
                '            palBECylinderDown.BackgroundImage = My.Resources.li_03
                '        End If
                '    Else
                '        palBECylinderDown.BackgroundImage = My.Resources.li_03
                '    End If
                '    lblECylinderDown.BackColor = System.Drawing.SystemColors.Control
                'End If
            End If
        End If
        '''''''''''''''''''''
        ''  Cylinder2 Diag ''
        '''''''''''''''''''''
        If (gUserLevel = enmUserLevel.eSoftwareMaker Or gUserLevel = enmUserLevel.eAdministrator) Then
            If ckbCylinder2.Checked = True Then
                ''Machine A
                If ckbMachineA.Checked = True Then
                    If MsgBox("A: " & resDiagWetco.GetString("MessageCylinder2_1.Text"), MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = MsgBoxResult.Yes Then 'A: Please confirm if the chuck is removed ?
                        'bStatus = True
                        ' '' check if chuck exist by "Station2TrayReady" sensor 
                        'If (Not gDICollection.GetState(enmDI.Station2StopperDownReady)) Then
                        '    gDOCollection.GetSetState(enmDO.Station2StopperUp) = True  '' A Stopper2 Down 
                        '    Threading.Thread.CurrentThread.Join(1000)
                        'End If

                        'If (gDICollection.GetState(enmDI.Station2StopperDownReady)) Then
                        '    '' ECylinder -> Home -> Down
                        '    Unit.A_ElectricCylinder(clsUnit.enmDirection.Home) '' A ECylinder -> Home
                        '    Threading.Thread.CurrentThread.Join(1000)
                        '    If (Unit.A_ECylinderLocation <> clsUnit.enmLocation.Home) Then
                        '        '    MsgBox("A: Electric Cylinder Home failed", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        '        TextBox1.Text &= "A: Electric cylinder Home failed" & vbNewLine
                        '        bStatus = False
                        '    End If
                        '    Unit.A_ElectricCylinder(clsUnit.enmDirection.Down) '' A ECylinder -> Down
                        '    Threading.Thread.CurrentThread.Join(1000)
                        '    If (Unit.A_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
                        '        Threading.Thread.CurrentThread.Join(1000)
                        '        If (Unit.A_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
                        '            '      MsgBox("A: Electric Cylinder Down failed", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        '            TextBox1.Text &= "A: Electric cylinder Down failed" & vbNewLine
                        '            bStatus = False
                        '        End If
                        '    End If
                        '    If (gDICollection.GetState(enmDI.Station2TrayReady)) Then  '' A 料盤流入檢知 => check Station2TrayReady when "Stopper2=Down" & "ECylinder=Down"
                        '        '     MsgBox("A: Tray Station isn't empty", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        '        TextBox1.Text &= "A: Chuck isn't removed !!!" & vbNewLine
                        '        bStatus = False
                        '    End If
                        'Else
                        '    TextBox1.Text &= "A: Stopper Down failed" & vbNewLine
                        '    bStatus = False
                        'End If
                        'If bStatus Then

                        ''Machine A Cylinder Up
                        lblCylinderNo1Up.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo2Up.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo3Up.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo4Up.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo5Up.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo6Up.BackColor = System.Drawing.Color.Yellow
                        gDOCollection.SetState(enmDO.HeaterCylinderUp1, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown1, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp2, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown2, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp3, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown3, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp4, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown4, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp5, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown5, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp6, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown6, False)
                        Threading.Thread.CurrentThread.Join(2000)
                        RefreshPal(palACylinderNo1Up, Not gDICollection.GetState(enmDI.Station2Heater1CylinderUpReady))
                        RefreshPal(palACylinderNo2Up, Not gDICollection.GetState(enmDI.Station2Heater2CylinderUpReady))
                        RefreshPal(palACylinderNo3Up, Not gDICollection.GetState(enmDI.Station2Heater3CylinderUpReady))
                        RefreshPal(palACylinderNo4Up, Not gDICollection.GetState(enmDI.Station2Heater4CylinderUpReady))
                        RefreshPal(palACylinderNo5Up, Not gDICollection.GetState(enmDI.Station2Heater5CylinderUpReady))
                        RefreshPal(palACylinderNo6Up, Not gDICollection.GetState(enmDI.Station2Heater6CylinderUpReady))
                        ''Machine A Cylinder Down
                        lblCylinderNo1Up.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo2Up.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo3Up.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo4Up.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo5Up.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo6Up.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo1DOwn.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo2DOwn.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo3DOwn.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo4DOwn.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo5DOwn.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo6DOwn.BackColor = System.Drawing.Color.Yellow
                        gDOCollection.SetState(enmDO.HeaterCylinderUp1, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown1, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp2, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown2, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp3, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown3, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp4, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown4, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp5, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown5, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp6, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown6, True)
                        Threading.Thread.CurrentThread.Join(2000)
                        RefreshPal(palACylinderNo1Down, Not gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady))
                        RefreshPal(palACylinderNo2Down, Not gDICollection.GetState(enmDI.Station2Heater2CylinderDownReady))
                        RefreshPal(palACylinderNo3Down, Not gDICollection.GetState(enmDI.Station2Heater3CylinderDownReady))
                        RefreshPal(palACylinderNo4Down, Not gDICollection.GetState(enmDI.Station2Heater4CylinderDownReady))
                        RefreshPal(palACylinderNo5Down, Not gDICollection.GetState(enmDI.Station2Heater5CylinderDownReady))
                        RefreshPal(palACylinderNo6Down, Not gDICollection.GetState(enmDI.Station2Heater6CylinderDownReady))
                        lblCylinderNo1DOwn.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo2DOwn.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo3DOwn.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo4DOwn.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo5DOwn.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo6DOwn.BackColor = System.Drawing.SystemColors.Control
                        'End If
                    Else
                        MsgBox("A: " & resDiagWetco.GetString("MessageCylinder2_2.Text"), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) 'A: Please remove chunk before checking cylinder 2 !!!
                        MessageBox.Text &= "A: Chuck isn't removed !!!" & vbNewLine
                    End If
                End If

                ''Machine B
                If ckbMachineB.Checked = True Then
                    If MsgBox("B: " & resDiagWetco.GetString("MessageCylinder2_1.Text"), MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = MsgBoxResult.Yes Then 'B: Please confirm if the chuck is removed ?
                        'bStatus = True
                        ' '' check if chuck exist by "Station2TrayReady" sensor 
                        'If (Not gDICollection.GetState(enmDI.Station3StopperDownReady)) Then
                        '    gDOCollection.GetSetState(enmDO.Station3StopperUp) = True  '' B Stopper2 Down
                        '    Threading.Thread.CurrentThread.Join(1000)
                        'End If
                        'If (gDICollection.GetState(enmDI.Station3StopperDownReady)) Then
                        '    '' ECylinder -> Home -> Down
                        '    Unit.B_ElectricCylinder(clsUnit.enmDirection.Home) '' B ECylinder -> Home
                        '    Threading.Thread.CurrentThread.Join(1000)
                        '    If (Unit.B_ECylinderLocation <> clsUnit.enmLocation.Home) Then
                        '        '   MsgBox("B: Electric Cylinder Home failed", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        '        TextBox1.Text &= "B: Electric cylinder Home failed" & vbNewLine
                        '        bStatus = False
                        '    End If
                        '    Unit.B_ElectricCylinder(clsUnit.enmDirection.Down) '' B ECylinder -> Down
                        '    Threading.Thread.CurrentThread.Join(1000)
                        '    If (Unit.B_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
                        '        Threading.Thread.CurrentThread.Join(1000)
                        '        If (Unit.B_ECylinderLocation <> clsUnit.enmLocation.Bottom) Then
                        '            '    MsgBox("B: Electric Cylinder Down failed", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        '            TextBox1.Text &= "B: Electric cylinder Down failed" & vbNewLine
                        '            bStatus = False
                        '        End If
                        '    End If
                        '    If (gDICollection.GetState(enmDI.Station2TrayReady)) Then  '' B 料盤流入檢知 => check Station2TrayReady when "Stopper2=Down" & "ECylinder=Down"
                        '        '   MsgBox("B: Tray Station isn't empty", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        '        TextBox1.Text &= "B: Chuck isn't removed !!!" & vbNewLine
                        '        bStatus = False
                        '    End If
                        'Else
                        '    TextBox1.Text &= "B: Stopper Down failed" & vbNewLine
                        '    bStatus = False
                        'End If
                        '    If bStatus Then
                        ''Machine B Cylinder Up
                        lblCylinderNo1Up.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo2Up.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo3Up.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo4Up.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo5Up.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo6Up.BackColor = System.Drawing.Color.Yellow
                        gDOCollection.SetState(enmDO.HeaterCylinderUp7, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown7, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp8, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown8, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp9, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown9, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp10, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown10, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp11, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown11, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp12, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown12, False)
                        Threading.Thread.CurrentThread.Join(2000)
                        RefreshPal(palBCylinderNo1Up, Not gDICollection.GetState(enmDI.Station3Heater1CylinderUpReady))
                        RefreshPal(palBCylinderNo2Up, Not gDICollection.GetState(enmDI.Station3Heater2CylinderUpReady))
                        RefreshPal(palBCylinderNo3Up, Not gDICollection.GetState(enmDI.Station3Heater3CylinderUpReady))
                        RefreshPal(palBCylinderNo4Up, Not gDICollection.GetState(enmDI.Station3Heater4CylinderUpReady))
                        RefreshPal(palBCylinderNo5Up, Not gDICollection.GetState(enmDI.Station3Heater5CylinderUpReady))
                        RefreshPal(palBCylinderNo6Up, Not gDICollection.GetState(enmDI.Station3Heater6CylinderUpReady))
                        ''Machine B Cylinder Down
                        lblCylinderNo1Up.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo2Up.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo3Up.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo4Up.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo5Up.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo6Up.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo1DOwn.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo2DOwn.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo3DOwn.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo4DOwn.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo5DOwn.BackColor = System.Drawing.Color.Yellow
                        lblCylinderNo6DOwn.BackColor = System.Drawing.Color.Yellow
                        gDOCollection.SetState(enmDO.HeaterCylinderUp7, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown7, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp8, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown8, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp9, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown9, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp10, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown10, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp11, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown11, True)
                        gDOCollection.SetState(enmDO.HeaterCylinderUp12, False)
                        gDOCollection.SetState(enmDO.HeaterCylinderDown12, True)
                        Threading.Thread.CurrentThread.Join(2000)
                        RefreshPal(palBCylinderNo1Down, Not gDICollection.GetState(enmDI.Station3Heater1CylinderDownReady))
                        RefreshPal(palBCylinderNo2Down, Not gDICollection.GetState(enmDI.Station3Heater2CylinderDownReady))
                        RefreshPal(palBCylinderNo3Down, Not gDICollection.GetState(enmDI.Station3Heater3CylinderDownReady))
                        RefreshPal(palBCylinderNo4Down, Not gDICollection.GetState(enmDI.Station3Heater4CylinderDownReady))
                        RefreshPal(palBCylinderNo5Down, Not gDICollection.GetState(enmDI.Station3Heater5CylinderDownReady))
                        RefreshPal(palBCylinderNo6Down, Not gDICollection.GetState(enmDI.Station3Heater6CylinderDownReady))
                        lblCylinderNo1DOwn.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo2DOwn.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo3DOwn.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo4DOwn.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo5DOwn.BackColor = System.Drawing.SystemColors.Control
                        lblCylinderNo6DOwn.BackColor = System.Drawing.SystemColors.Control
                        'End If
                    Else
                        MsgBox("B: " & resDiagWetco.GetString("MessageCylinder2_2.Text"), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) 'B: Please remove chunk before checking cylinder 2 !!!
                        MessageBox.Text &= "B: Chuck isn't removed !!!" & vbNewLine
                    End If
                End If
            End If
        End If

        btnDiagRun.BackColor = System.Drawing.SystemColors.Control
        btnDiagRun.Text = resDiagWetco.GetString("btnDiagRun.Text")

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
        palAEMS.BackgroundImage = My.Resources.Grey
        palACDA.BackgroundImage = My.Resources.Grey
        palADoorClose.BackgroundImage = My.Resources.Grey
        '  palAMCSystem.BackgroundImage = My.Resources.Grey
        palAMCMotor.BackgroundImage = My.Resources.Grey
        palAMCHeater.BackgroundImage = My.Resources.Grey
        palAPrevAlarm.BackgroundImage = My.Resources.Grey
        palANextAlarm.BackgroundImage = My.Resources.Grey
        palALValveControllerAlarm.BackgroundImage = My.Resources.Grey
        palALDetectGlueSensor.BackgroundImage = My.Resources.Grey
        palARValveControllerAlarm.BackgroundImage = My.Resources.Grey
        palARDetectGlueSensor.BackgroundImage = My.Resources.Grey
        palAMoveInMotorAlarm.BackgroundImage = My.Resources.Grey
        palAOverTemperature1.BackgroundImage = My.Resources.Grey
        palAOverTemperature2.BackgroundImage = My.Resources.Grey
        palAOverTemperature3.BackgroundImage = My.Resources.Grey
        palAOverTemperature4.BackgroundImage = My.Resources.Grey
        palAOverTemperature5.BackgroundImage = My.Resources.Grey
        palAOverTemperature6.BackgroundImage = My.Resources.Grey
        palAWorkingHeaterAlarm1.BackgroundImage = My.Resources.Grey
        palAWorkingHeaterAlarm2.BackgroundImage = My.Resources.Grey
        palAWorkingHeaterAlarm3.BackgroundImage = My.Resources.Grey
        palAWorkingHeaterAlarm4.BackgroundImage = My.Resources.Grey
        palAWorkingHeaterAlarm5.BackgroundImage = My.Resources.Grey
        palAWorkingHeaterAlarm6.BackgroundImage = My.Resources.Grey
        palBEMS.BackgroundImage = My.Resources.Grey
        palBCDA.BackgroundImage = My.Resources.Grey
        palBDoorClose.BackgroundImage = My.Resources.Grey
        palBMCMotor.BackgroundImage = My.Resources.Grey
        palBMCHeater.BackgroundImage = My.Resources.Grey
        palBPrevAlarm.BackgroundImage = My.Resources.Grey
        palBNextAlarm.BackgroundImage = My.Resources.Grey
        palBLValveControllerAlarm.BackgroundImage = My.Resources.Grey
        palBLDetectGlueSensor.BackgroundImage = My.Resources.Grey
        palBRValveControllerAlarm.BackgroundImage = My.Resources.Grey
        palBRDetectGlueSensor.BackgroundImage = My.Resources.Grey
        palBMoveInMotorAlarm.BackgroundImage = My.Resources.Grey
        palBOverTemperature1.BackgroundImage = My.Resources.Grey
        palBOverTemperature2.BackgroundImage = My.Resources.Grey
        palBOverTemperature3.BackgroundImage = My.Resources.Grey
        palBOverTemperature4.BackgroundImage = My.Resources.Grey
        palBOverTemperature5.BackgroundImage = My.Resources.Grey
        palBOverTemperature6.BackgroundImage = My.Resources.Grey
        palBWorkingHeaterAlarm1.BackgroundImage = My.Resources.Grey
        palBWorkingHeaterAlarm2.BackgroundImage = My.Resources.Grey
        palBWorkingHeaterAlarm3.BackgroundImage = My.Resources.Grey
        palBWorkingHeaterAlarm4.BackgroundImage = My.Resources.Grey
        palBWorkingHeaterAlarm5.BackgroundImage = My.Resources.Grey
        palBWorkingHeaterAlarm6.BackgroundImage = My.Resources.Grey
        ''   Communication  ''
        palAValveController1.BackgroundImage = My.Resources.Grey
        palAValveController2.BackgroundImage = My.Resources.Grey
        palATriggerBoard1.BackgroundImage = My.Resources.Grey
        palATriggerBoard2.BackgroundImage = My.Resources.Grey
        palALaserReader1.BackgroundImage = My.Resources.Grey
        palALaserReader2.BackgroundImage = My.Resources.Grey
        palABalance.BackgroundImage = My.Resources.Grey
        palBValveController1.BackgroundImage = My.Resources.Grey
        palBValveController2.BackgroundImage = My.Resources.Grey
        palBTriggerBoard1.BackgroundImage = My.Resources.Grey
        palBTriggerBoard2.BackgroundImage = My.Resources.Grey
        palBLaserReader1.BackgroundImage = My.Resources.Grey
        palBLaserReader2.BackgroundImage = My.Resources.Grey
        palBBalance.BackgroundImage = My.Resources.Grey
        ''   Vacuum Diag  ''
        palAChuckVacuumReady.BackgroundImage = My.Resources.Grey
        palAPurgeVacuum1.BackgroundImage = My.Resources.Grey
        palAPurgeVacuum2.BackgroundImage = My.Resources.Grey
        palBChuckVacuumReady.BackgroundImage = My.Resources.Grey
        palBPurgeVacuum1.BackgroundImage = My.Resources.Grey
        palBPurgeVacuum2.BackgroundImage = My.Resources.Grey
        ''   EP Valve Diag  ''
        palAEPValve1.BackgroundImage = My.Resources.Grey
        palAEPValve2.BackgroundImage = My.Resources.Grey
        palBEPValve1.BackgroundImage = My.Resources.Grey
        palBEPValve2.BackgroundImage = My.Resources.Grey
        ''   Light Diag  '' 
        palLight1.BackgroundImage = My.Resources.Grey
        palLight2.BackgroundImage = My.Resources.Grey
        palLight3.BackgroundImage = My.Resources.Grey
        palLight4.BackgroundImage = My.Resources.Grey
        palLight5.BackgroundImage = My.Resources.Grey
        palLight6.BackgroundImage = My.Resources.Grey
        palLight7.BackgroundImage = My.Resources.Grey
        palLight8.BackgroundImage = My.Resources.Grey
        ''   Heater  Diag ''  
        palAHeater1Error.BackgroundImage = My.Resources.Grey
        palAHeater2Error.BackgroundImage = My.Resources.Grey
        palAHeater3Error.BackgroundImage = My.Resources.Grey
        palAHeater4Error.BackgroundImage = My.Resources.Grey
        palAHeater5Error.BackgroundImage = My.Resources.Grey
        palAHeater6Error.BackgroundImage = My.Resources.Grey
        palBHeater1Error.BackgroundImage = My.Resources.Grey
        palBHeater2Error.BackgroundImage = My.Resources.Grey
        palBHeater3Error.BackgroundImage = My.Resources.Grey
        palBHeater4Error.BackgroundImage = My.Resources.Grey
        palBHeater5Error.BackgroundImage = My.Resources.Grey
        palBHeater6Error.BackgroundImage = My.Resources.Grey
        ''  Cylinder Diag ''
        palAStopper1Up.BackgroundImage = My.Resources.Grey
        palAStopper1Down.BackgroundImage = My.Resources.Grey
        palAStopper2Up.BackgroundImage = My.Resources.Grey
        palAStopper2Down.BackgroundImage = My.Resources.Grey
        palACylinderNo1Up.BackgroundImage = My.Resources.Grey
        palACylinderNo1Down.BackgroundImage = My.Resources.Grey
        palACylinderNo2Up.BackgroundImage = My.Resources.Grey
        palACylinderNo2Down.BackgroundImage = My.Resources.Grey
        palACylinderNo3Up.BackgroundImage = My.Resources.Grey
        palACylinderNo3Down.BackgroundImage = My.Resources.Grey
        palACylinderNo4Up.BackgroundImage = My.Resources.Grey
        palACylinderNo4Down.BackgroundImage = My.Resources.Grey
        palACylinderNo5Up.BackgroundImage = My.Resources.Grey
        palACylinderNo5Down.BackgroundImage = My.Resources.Grey
        palACylinderNo6Up.BackgroundImage = My.Resources.Grey
        palACylinderNo6Down.BackgroundImage = My.Resources.Grey
        palAECylinderUp.BackgroundImage = My.Resources.Grey
        palAECylinderDown.BackgroundImage = My.Resources.Grey
        palBStopper2Up.BackgroundImage = My.Resources.Grey
        palBStopper2Down.BackgroundImage = My.Resources.Grey
        palBCylinderNo1Up.BackgroundImage = My.Resources.Grey
        palBCylinderNo1Down.BackgroundImage = My.Resources.Grey
        palBCylinderNo2Up.BackgroundImage = My.Resources.Grey
        palBCylinderNo2Down.BackgroundImage = My.Resources.Grey
        palBCylinderNo3Up.BackgroundImage = My.Resources.Grey
        palBCylinderNo3Down.BackgroundImage = My.Resources.Grey
        palBCylinderNo4Up.BackgroundImage = My.Resources.Grey
        palBCylinderNo4Down.BackgroundImage = My.Resources.Grey
        palBCylinderNo5Up.BackgroundImage = My.Resources.Grey
        palBCylinderNo5Down.BackgroundImage = My.Resources.Grey
        palBCylinderNo6Up.BackgroundImage = My.Resources.Grey
        palBCylinderNo6Down.BackgroundImage = My.Resources.Grey
        palBECylinderUp.BackgroundImage = My.Resources.Grey
        palBECylinderDown.BackgroundImage = My.Resources.Grey

        palAEPValve1.BackgroundImage = My.Resources.Grey
        palAEPValve1.BackgroundImage = My.Resources.Grey
        palAEPValve2.BackgroundImage = My.Resources.Grey
        palBEPValve1.BackgroundImage = My.Resources.Grey
        palBEPValve2.BackgroundImage = My.Resources.Grey
    End Sub
    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        ckbMachineA.Checked = True
        ckbMachineB.Checked = True
        ckbDIAlarm.Checked = True
        ckbCommunication.Checked = True
        ckbEPValve.Checked = True
        ckbLight.Checked = True
        ckbHeater.Checked = True
        ckbCylinder1.Checked = True
        ckbVacuum.Checked = True
        If (gUserLevel = enmUserLevel.eSoftwareMaker Or gUserLevel = enmUserLevel.eAdministrator) Then
            ckbCylinder2.Checked = True
        Else
            ckbCylinder2.Checked = False
        End If
    End Sub

    Private Sub btnDeselectAll_Click(sender As Object, e As EventArgs) Handles btnDeselectAll.Click
        ckbMachineA.Checked = False
        ckbMachineB.Checked = False
        ckbDIAlarm.Checked = False
        ckbCommunication.Checked = False
        ckbEPValve.Checked = False
        ckbLight.Checked = False
        ckbHeater.Checked = False
        ckbCylinder1.Checked = False
        ckbCylinder2.Checked = False
        ckbVacuum.Checked = False
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub
End Class
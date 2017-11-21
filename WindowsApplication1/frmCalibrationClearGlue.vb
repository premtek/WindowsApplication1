﻿Imports ProjectCore
Imports ProjectMotion
Imports ProjectIO

Public Class frmCalibrationClearGlue
    Public myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalibrationClearGlue))
    Public sys As New sSysParam
    Dim backUpPos As CPosition
    Dim backUpValve() As CValveData

    
    Public CalibrationEnable As Boolean

    Private Sub frmClearGlue_Load(sender As Object, e As EventArgs) Handles Me.Load
        '20170327
        Me.Text = "Cleaner Position " & "Stage" & (sys.EsysNum - eSys.DispStage1 + 1) & " Valve" & (sys.SelectValve + 1)

        With cmbValve.Items
            .Clear()
            '20170321
            'Select Case gSSystemParameter.enmMachineType
            '    Case enmMachineType.DCSW_800AQ
            '        .Add(myResource.GetString("cmbValve.Items"))
            '        .Add(myResource.GetString("cmbValve.Items1"))
            '        .Add(myResource.GetString("cmbValve.Items2"))
            '        .Add(myResource.GetString("cmbValve.Items3"))
            '        If cmbValve.SelectedIndex = -1 Then cmbValve.SelectedIndex = 0
            '        cmbValve.Visible = True
            '        lblSelctValve.Visible = True

            '    Case enmMachineType.eDTS_2S2V
            '        .Add(myResource.GetString("cmbValve.Items"))
            '        .Add(myResource.GetString("cmbValve.Items1"))
            '        If cmbValve.SelectedIndex = -1 Then cmbValve.SelectedIndex = 0
            '        cmbValve.Visible = True
            '        lblSelctValve.Visible = True

            '    Case Else
            '        Select Case gSSystemParameter.MechanismModule
            '            Case enmMechanismModule.OneValveOneStage
            '                .Add(myResource.GetString("cmbValve.Items"))
            '                If cmbValve.SelectedIndex = -1 Then cmbValve.SelectedIndex = 0
            '                cmbValve.Visible = False
            '                lblSelctValve.Visible = False

            '            Case enmMechanismModule.TwoValveOneStage
            '                .Add(myResource.GetString("cmbValve.Items"))
            '                .Add(myResource.GetString("cmbValve.Items1"))
            '                If cmbValve.SelectedIndex = -1 Then cmbValve.SelectedIndex = 0
            '                cmbValve.Visible = True
            '                lblSelctValve.Visible = True

            '        End Select
            'End Select

        End With

        '  If CalibrationEnable = True Then
        btnClearGluePrev.Visible = True
        ' ElseIf CalibrationEnable = False Then
        'btnClearGluePrev.Visible = False
        '  End If

        grpClearConfig.Enabled = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetClearPos), gUserLevel)
        backUpPos = gSSystemParameter.Pos
        backUpValve = gSSystemParameter.StageParts.ValveData

        '20170321
        With gSSystemParameter.StageParts.ValveData(sys.StageNo)
            nmcClearGluePitch.Value = .CleanPastePitch(sys.SelectValve)
            nmcClearGlueNumLimit.Value = .CleanPasteNumLimit(sys.SelectValve)
            nmcClearGlueDistanceZ.Value = .CleanPasteDistanceZ(sys.SelectValve)
            nmcClearGluePressureTime.Value = .CleanPastePressureTime(sys.SelectValve)
            nmcClearGlueMoveOffset.Value = .CleanPasteOffset(sys.SelectValve)
            nmcClearGlueMoveSpeed.Value = .CleanPasteSpeed(sys.SelectValve)
            chkMotorDir.Checked = .CleanPasteDir(sys.SelectValve)
        End With

        '20170321
        With gSSystemParameter.Pos.CleanValveCalibration(sys.StageNo)
            nmcValveClearGluePosX.Text = .PosX(sys.SelectValve)
            nmcValveClearGluePosY.Text = .PosY(sys.SelectValve)
            nmcValveClearGluePosZ.Text = .PosZ(sys.SelectValve)
        End With

        Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).ValveType(sys.SelectValve)
            Case enmValveType.Jet
                grpClearPos.Visible = True
            Case enmValveType.None
                grpClearPos.Visible = False
            Case enmValveType.Auger
                grpClearPos.Visible = True
        End Select

        If Not gSYS(sys.EsysNum) Is Nothing Then
            sys.AxisX = gSYS(sys.EsysNum).AxisX
            sys.AxisY = gSYS(sys.EsysNum).AxisY
            sys.AxisZ = gSYS(sys.EsysNum).AxisZ
            sys.AxisB = gSYS(sys.EsysNum).AxisB
            sys.AxisC = gSYS(sys.EsysNum).AxisC
        End If
        UcJoyStick1.AxisX = sys.AxisX
        UcJoyStick1.AxisY = sys.AxisY
        UcJoyStick1.AxisZ = sys.AxisZ
        UcJoyStick1.AXisA = sys.AxisA
        UcJoyStick1.AXisB = sys.AxisB
        UcJoyStick1.AXisC = sys.AxisC
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

        '--- 避免當機,放最後 ---
        Timer1.Enabled = True
        Call Timer1_Tick(sender, e)
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub btnGetDispenserNo1ClearGluePos_Click(sender As Object, e As EventArgs) Handles btnGetDispenserNo1ClearGluePos.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmClearGlue]" & vbTab & "[btnGetDispenserNo1ClearGluePos]" & vbTab & "Click")
        btnGetDispenserNo1ClearGluePos.Enabled = False
        UcJoyStick1.RefreshPosition()
        With gSSystemParameter.Pos
            nmcValveClearGluePosX.Text = CDbl(gCMotion.GetPositionValue(sys.AxisX))
            nmcValveClearGluePosY.Text = CDbl(gCMotion.GetPositionValue(sys.AxisY))
            nmcValveClearGluePosZ.Text = CDbl(gCMotion.GetPositionValue(sys.AxisZ))
        End With
        btnGetDispenserNo1ClearGluePos.Enabled = True
    End Sub


    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmClearGlue]" & vbTab & "[btnOK]" & vbTab & "Click")
        btnOK.Enabled = False
        ''[說明]:紀錄按之前後的狀態
        With gSSystemParameter.Pos.CleanValveCalibration(sys.StageNo)
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6019038, .PosX(enmValve.No1), .PosY(enmValve.No1), .PosZ(enmValve.No1)), "INFO_6019038")
        End With

        '20170321
        With gSSystemParameter.Pos.CleanValveCalibration(sys.StageNo)
            .PosX(sys.SelectValve) = CDbl(nmcValveClearGluePosX.Text)
            .PosY(sys.SelectValve) = CDbl(nmcValveClearGluePosY.Text)
            .PosZ(sys.SelectValve) = CDbl(nmcValveClearGluePosZ.Text)
        End With

        '20170321
        gSSystemParameter.Pos.CleanValveCalibration(sys.StageNo).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini")


        ''[說明]:紀錄按之前後的狀態
        With gSSystemParameter.Pos.CleanValveCalibration(sys.StageNo)
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6019039, .PosX(enmValve.No1), .PosY(enmValve.No1), .PosZ(enmValve.No1)), "INFO_6019039")
        End With

        gSSystemParameter.SaveSystemPos(Application.StartupPath & "\System\" & MachineName & "\SysPos.ini") '儲存系統位置(by機型)


        '[說明]:紀錄按了哪些按鈕
        '[說明]:紀錄按之前後的狀態
        With gSSystemParameter.StageParts.ValveData(sys.StageNo)
            gSyslog.Save("Old Clear Glue Parameter")
            gSyslog.Save("Pitch:" & .CleanPastePitch(sys.SelectValve) & "mm")
            gSyslog.Save("CountLimit: " & .CleanPasteNumLimit(sys.SelectValve))
            gSyslog.Save("Offset: " & .CleanPasteDistanceZ(sys.SelectValve) & "mm")
            gSyslog.Save("AP On Time: " & .CleanPastePressureTime(sys.SelectValve) & "ms")
            gSyslog.Save("ClearGlueMoveOffset: " & .CleanPasteOffset(sys.SelectValve) & "mm")
            gSyslog.Save("ClearGlueMoveSpeed: " & .CleanPasteSpeed(sys.SelectValve))
            gSyslog.Save("MotorDir: " & .CleanPasteDir(sys.SelectValve))
        End With

        With gSSystemParameter.StageParts.ValveData(sys.StageNo)
            .CleanPastePitch(sys.SelectValve) = CDbl(nmcClearGluePitch.Text)
            .CleanPasteNumLimit(sys.SelectValve) = CDbl(nmcClearGlueNumLimit.Text)
            .CleanPasteDistanceZ(sys.SelectValve) = CDbl(nmcClearGlueDistanceZ.Text)
            .CleanPastePressureTime(sys.SelectValve) = CDbl(nmcClearGluePressureTime.Text)
            .CleanPasteNum(sys.SelectValve) = 0
            .CleanPasteOffset(sys.SelectValve) = CDbl(nmcClearGlueMoveOffset.Text)
            .CleanPasteSpeed(sys.SelectValve) = CDbl(nmcClearGlueMoveSpeed.Text)
            .CleanPasteDir(sys.SelectValve) = chkMotorDir.Checked

            If .CleanPastePitch(sys.SelectValve) * .CleanPasteNumLimit(sys.SelectValve) > .CleanPasteTableLength(sys.SelectValve) Then
                .CleanPastePitch(sys.SelectValve) = Format(.CleanPasteTableLength(sys.SelectValve) / .CleanPasteNumLimit(sys.SelectValve), "0.0000")
                nmcClearGluePitch.Text = .CleanPastePitch(sys.SelectValve).ToString()
            End If
        End With

        '[說明]:紀錄按之前後的狀態
        With gSSystemParameter.StageParts.ValveData(sys.StageNo)
            gSyslog.Save("Old Clear Glue Parameter")
            gSyslog.Save("Pitch:" & .CleanPastePitch(sys.SelectValve) & "mm")
            gSyslog.Save("CountLimit: " & .CleanPasteNumLimit(sys.SelectValve))
            gSyslog.Save("Offset: " & .CleanPasteDistanceZ(sys.SelectValve) & "mm")
            gSyslog.Save("AP On Time: " & .CleanPastePressureTime(sys.SelectValve) & "ms")
            gSyslog.Save("ClearGlueMoveOffset: " & .CleanPasteOffset(sys.SelectValve) & "mm")
            gSyslog.Save("ClearGlueMoveSpeed: " & .CleanPasteSpeed(sys.SelectValve))
            gSyslog.Save("MotorDir: " & .CleanPasteDir(sys.SelectValve))
        End With

        nmcClearGluePitch.BackColor = Color.White
        nmcClearGlueNumLimit.BackColor = Color.White
        nmcClearGlueDistanceZ.BackColor = Color.White
        nmcClearGluePressureTime.BackColor = Color.White


        nmcClearGlueMoveOffset.BackColor = Color.White
        nmcClearGlueMoveSpeed.BackColor = Color.White

        gSSystemParameter.StageParts.ValveData(sys.StageNo).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini")
        btnOK.Enabled = True

        If CalibrationEnable = True Then
            '存檔成功 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        ElseIf CalibrationEnable = False Then
            '存檔成功 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmClearGlue]" & vbTab & "[btnCancel]" & vbTab & "Click")
        btnCancel.Enabled = False
        gSSystemParameter.Pos = backUpPos '資料還原
        gSSystemParameter.StageParts.ValveData = backUpValve
        Timer1.Enabled = False
        btnCancel.Enabled = True

        '20170623 離開時Z 軸升至安全高度
        gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.WeightCalibration(sys.StageNo).SafePosZ(sys.SelectValve))
        Me.Close()
        '20171108
        If sys.SelectValve = eValveWorkMode.Valve2 Then
            ValveCylinderAction(sys.StageNo, sys.SelectValve, enmUpDown.Up) '汽缸上
        End If

    End Sub

    Dim selectedSysStage As Integer = eSys.DispStage1


    Private Sub cmbValve_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbValve.SelectedIndexChanged
        If cmbValve.SelectedIndex < 0 Then
            Exit Sub
        End If
        '20170321
        'sys.SelectValve = cmbValve.SelectedIndex
        'With gSSystemParameter.StageParts.ValveData(sys.StageNo)
        '    nmcClearGluePitch.Value = .CleanPastePitch(sys.SelectValve)
        '    nmcClearGlueNumLimit.Value = .CleanPasteNumLimit(sys.SelectValve)
        '    nmcClearGlueDistanceZ.Value = .CleanPasteDistanceZ(sys.SelectValve)
        '    nmcClearGluePressureTime.Value = .CleanPastePressureTime(sys.SelectValve)
        '    nmcClearGlueMoveOffset.Value = .CleanPasteOffset(sys.SelectValve)
        '    nmcClearGlueMoveSpeed.Value = .CleanPasteSpeed(sys.SelectValve)
        '    chkMotorDir.Checked = .CleanPasteDir(sys.SelectValve)
        'End With

        'With gSSystemParameter.Pos.CleanValveCalibration(sys.StageNo)
        '    nmcValveClearGluePosX.Text = .PosX(sys.SelectValve)
        '    nmcValveClearGluePosY.Text = .PosX(sys.SelectValve)
        '    nmcValveClearGluePosZ.Text = .PosX(sys.SelectValve)
        'End With

        'Select Case gSSystemParameter.StageParts.ValveData(sys.StageNo).ValveType(sys.SelectValve)
        '    Case enmValveType.Jet
        '        grpClearPos.Visible = True
        '    Case enmValveType.None
        '        grpClearPos.Visible = False
        '    Case enmValveType.Auger
        '        grpClearPos.Visible = True
        'End Select

        'Select Case mAbsValveNo
        '    Case 0
        '        selectedSysStage = eSys.DispStage1
        '    Case 1
        '        selectedSysStage = eSys.DispStage2
        '    Case 2
        '        selectedSysStage = eSys.DispStage3
        '    Case 3
        '        selectedSysStage = eSys.DispStage4
        'End Select
        'If Not gSYS(selectedSysStage) Is Nothing Then
        '    sys.AxisX = gSYS(selectedSysStage).AxisX
        '    sys.AxisY = gSYS(selectedSysStage).AxisY
        '    sys.AxisZ = gSYS(selectedSysStage).AxisZ
        '    sys.AxisB = gSYS(selectedSysStage).AxisB
        '    sys.AxisC = gSYS(selectedSysStage).AxisC
        'End If
        'UcJoyStick1.AxisX = sys.AxisX
        'UcJoyStick1.AxisY = sys.AxisY
        'UcJoyStick1.AxisZ = sys.AxisZ
        'UcJoyStick1.RefreshPosition()

    End Sub

    Private Sub btnClearGlue_Click(sender As Object, e As EventArgs) Handles btnClearGlue.Click
        gSyslog.Save("[frmClearGlue]" & vbTab & "[btnClearGlue]" & vbTab & "Click")
        '[說明]:紀錄按了哪些按鈕
        btnClearGlue.Enabled = False
        '20170602按鍵保護
        btnGo.Enabled = False
        btnGetDispenserNo1ClearGluePos.Enabled = False
        btnClearGluePrev.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        UcJoyStick1.Enabled = False

        '[說明]:回Home完成才能執行
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOperation btnClearGlue", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!
            btnClearGlue.Enabled = True

            '20170602按鍵保護
            btnGo.Enabled = True
            btnGetDispenserNo1ClearGluePos.Enabled = True
            btnClearGluePrev.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True
            UcJoyStick1.Enabled = True

            Exit Sub
        End If

        If gSYS(eSys.OverAll).Act(eAct.AutoRun).RunStatus = enmRunStatus.Running Then '[說明]:只有非Running的時候才能動
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            btnClearGlue.Enabled = True


            '20170602按鍵保護
            btnGo.Enabled = True
            btnGetDispenserNo1ClearGluePos.Enabled = True
            btnClearGluePrev.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True
            UcJoyStick1.Enabled = True

            Exit Sub
        End If

        '[說明]:enmRunStatus.Nono, enmRunStatus.Pause, enmRunStatus.Alarm, enmRunStatus.Running
        '[說明]:判斷是否已經在做ClearGlue
        '20170321
        If gSYS(sys.EsysNum).Act(eAct.ClearGlue).RunStatus = enmRunStatus.Running Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            btnClearGlue.Enabled = True


            '20170602按鍵保護
            btnGo.Enabled = True
            btnGetDispenserNo1ClearGluePos.Enabled = True
            btnClearGluePrev.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True
            UcJoyStick1.Enabled = True

            Exit Sub
        End If
        '20170321
        'gSYS(sys.EsysNum).RunStatus = enmRunStatus.Finish '強制接收命令
        'gSYS(sys.EsysNum).Command = eSysCommand.Purge
        Select Case sys.StageNo
            Case enmStage.No1
                gSYS(eSys.SubDisp1).SelectValve = sys.SelectValve
                gSYS(eSys.SubDisp1).RunStatus = enmRunStatus.Finish '強制接收命令                
                gSYS(eSys.SubDisp1).Command = eSysCommand.Purge
            Case enmStage.No2
                gSYS(eSys.SubDisp2).SelectValve = sys.SelectValve
                gSYS(eSys.SubDisp2).RunStatus = enmRunStatus.Finish '強制接收命令
                gSYS(eSys.SubDisp2).Command = eSysCommand.Purge
            Case enmStage.No3
                gSYS(eSys.SubDisp3).SelectValve = sys.SelectValve
                gSYS(eSys.SubDisp3).RunStatus = enmRunStatus.Finish '強制接收命令
                gSYS(eSys.SubDisp3).Command = eSysCommand.Purge
            Case enmStage.No4
                gSYS(eSys.SubDisp4).SelectValve = sys.SelectValve
                gSYS(eSys.SubDisp4).RunStatus = enmRunStatus.Finish '強制接收命令
                gSYS(eSys.SubDisp4).Command = eSysCommand.Purge
        End Select

        'gblnClearGlueComeBack = True
        gblnUpdateClearGlue = True
        btnClearGlue.Enabled = True

        '20170602按鍵保護
        btnGo.Enabled = True
        btnGetDispenserNo1ClearGluePos.Enabled = True
        btnClearGluePrev.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True


    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        gSyslog.Save("[frmClearGlue]" & vbTab & "[btnGo]" & vbTab & "Click")
        btnGo.Enabled = False
        '20170602按鍵保護
        btnGetDispenserNo1ClearGluePos.Enabled = False
        btnClearGlue.Enabled = False
        btnClearGluePrev.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        UcJoyStick1.Enabled = False

        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC
        '20170807 先移動XY
        TargetPos(0) = nmcValveClearGluePosX.Value
        TargetPos(1) = nmcValveClearGluePosY.Value
        TargetPos(2) = 0
        'TargetPos(2) = nmcValveClearGluePosZ.Value
        TargetPos(3) = 0
        TargetPos(4) = 0
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)

        '20170807 變更Z軸位置
        TargetPos(2) = nmcValveClearGluePosZ.Value

        '20170807
        If MsgBox("Z Stage Will move down，please check PosZ:" + nmcValveClearGluePosZ.Value.ToString + " is safe", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
            gSyslog.Save("[frmCalibrationClearGule]" & vbTab & "[btnGo_Click]" & vbTab & "Stage in Safe Pos By User Check")
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, TargetPos(0), TargetPos(1), TargetPos(2)), "INFO_6019015")
            ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        End If

        UcJoyStick1.RefreshPosition()
        btnGo.Enabled = True
        '20170602按鍵保護
        btnGetDispenserNo1ClearGluePos.Enabled = True
        btnClearGlue.Enabled = True
        btnClearGluePrev.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True
    End Sub

    '[說明]:按鍵安全保護
    Private Sub nmcValveClearGluePosX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles nmcValveClearGluePosX.KeyPress, nmcValveClearGluePosZ.KeyPress, nmcValveClearGluePosY.KeyPress, nmcClearGluePressureTime.KeyPress, nmcClearGluePitch.KeyPress, nmcClearGlueNumLimit.KeyPress, nmcClearGlueDistanceZ.KeyPress
        '20160920
        If Char.IsDigit(e.KeyChar) Or e.KeyChar = Chr(8) Then
            If IsNumeric(sender.Text) Or sender.Text = "" Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        Else
            If IsNumeric(sender.Text & e.KeyChar) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub frmCalibrationClearGlue_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Me.Dispose(True)
        'GC.Collect()
        Timer1.Enabled = False

        '20170321
        UcJoyStick1.ManualDispose()
        Me.Dispose(True)
    End Sub

    Private Sub btnClearGluePrev_Click(sender As Object, e As EventArgs) Handles btnClearGluePrev.Click
        gSyslog.Save("[frmClearGlue]" & vbTab & "[btnCancel]" & vbTab & "Click")
        gSSystemParameter.Pos = backUpPos '資料還原
        gSSystemParameter.StageParts.ValveData = backUpValve
        Me.Close()

        If gfrmPurge Is Nothing Then
            gfrmPurge = New frmCalibrationPurge
        ElseIf gfrmPurge.IsDisposed Then
            gfrmPurge = New frmCalibrationPurge
        End If
        '    Dim mfrmPurge = New frmCalibrationPurge
        With gfrmPurge
            .sys = gSYS(sys.EsysNum)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = True
            .ShowDialog()
        End With
    End Sub

    Private Sub frmCalibrationClearGlue_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Eason 20170124 Ticket:100031 , Memory Freed [S]
        'UcJoyStick1.ManualDispose()
        'Me.Dispose(True)
        'Eason 20170124 Ticket:100031 , Memory Freed [E]
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '20170810 JOG防護
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按          
            btnGo.Enabled = False
            btnGetDispenserNo1ClearGluePos.Enabled = False
            UcJoyStick1.Enabled = False
        End If
    End Sub

    Private Sub btnCylinder_Click(sender As Object, e As EventArgs) Handles btnCylinder.Click
        Dim iDO As Integer = enmDO.ClearGlueMotorPowerOn
        Dim state As Boolean = Not gDOCollection.GetState(iDO)
        gDOCollection.SetState(iDO, state)
        If gDOCollection.GetState(iDO, False) = False Then
            btnCylinder.Text = "PowerOff"
        Else
            btnCylinder.Text = "PowerOn"
        End If
    End Sub
End Class
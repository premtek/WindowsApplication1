Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectAOI

Public Class frmCalibrationCCD2Valve2
    Public sys As sSysParam
    ''' <summary>閥件絕對索引</summary>
    ''' <remarks></remarks>
    Dim mAbsValveNo As Integer = enmValve.No2

    Private Sub robAutoScrewCalibration_CheckedChanged(sender As Object, e As EventArgs) Handles btnAutoCalibrationSetup.Click

        '[說明]:回Home完成才能執行
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            'robAutoScrewCalibration.Checked = False '按鈕上昇
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) '請先復歸.
            Exit Sub
        End If
        If gSYS(eSys.OverAll).Act(eAct.AutoRun).RunStatus = enmRunStatus.Running Then '[說明]:只有非Running的時候才能動
            'robAutoScrewCalibration.Checked = False '按鈕上昇
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000007), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) '運行中,請稍後.
            Exit Sub
        End If
        '[說明]:enmRunStatus.Nono, enmRunStatus.Pause, enmRunStatus.Alarm, enmRunStatus.Running
        '[說明]:判斷是否已經在做Purge
        '[說明]:判斷是否已經在做ChangeGlue
        '[說明]:判斷是否已經在做ClearGlue
        'gSYS(eSys.Manual).SelectValve = sys.SelectValve
        'gSYS(eSys.Manual).StageNo = sys.StageNo
        'gSYS(eSys.Manual).CCDNo = sys.CCDNo
        'gSYS(eSys.Manual).RunStatus = enmRunStatus.None '強制接收處理
        'gSYS(eSys.Manual).Command = eSysCommand.CCDValveAutoCalibrationXY
        'robAutoScrewCalibration.Checked = True '按鈕下沉
        gSyslog.Save("Click robAutoScrewCalibration@frmCalibrationCCD2Valve2")


    End Sub
    Private Sub btnSetCcdPosX_Click(sender As Object, e As EventArgs) Handles btnSetCcdPosX.Click
        gSyslog.Save("[frmCalibrationCCD2Valve2]" & vbTab & "[btnSetCcdPosX]" & vbTab & "Click")
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        'WriteButtonLog(gUserLevel, "frmCalibrationCCD2Valve2", "btnSetCCDPosX")
        txtTeachCCD2X.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtTeachCCD2Y.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtTeachCCD2Z.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        UcJoyStick1.RefreshPosition()
        gSyslog.Save("X:" & txtTeachCCD2X.Text)
        gSyslog.Save("Y:" & txtTeachCCD2Y.Text)
        gSyslog.Save("Z:" & txtTeachCCD2Z.Text)

    End Sub

    Private Sub btnSetValvePosX_Click(sender As Object, e As EventArgs) Handles btnSetValvePosX.Click
        gSyslog.Save("[frmCalibrationCCD2Valve2]" & vbTab & "[btnSetValvePosX]" & vbTab & "Click")
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        'WriteButtonLog(gUserLevel, "frmCalibrationCCD2Valve2", "btnSetValvePosX")
        txtTeachDispenser2X.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtTeachDispenser2Y.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtTeachDispenser2Z.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        UcJoyStick1.RefreshPosition()
        gSyslog.Save("X:" & txtTeachDispenser2X.Text)
        gSyslog.Save("Y:" & txtTeachDispenser2Y.Text)
        gSyslog.Save("Z:" & txtTeachDispenser2Z.Text)

    End Sub

    Private Sub frmCalibrationCCD2Valve2_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170124 Ticket:100031 , Memory Freed [S]
        UcDisplay1.EndLive()
        UcDisplay1.ManualDispose()
        UcJoyStick1.ManualDispose()
        Me.Dispose(True)
        'Eason 20170124 Ticket:100031 , Memory Freed [E]
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub frmCalibrationCCD2Valve1_Load(sender As Object, e As EventArgs) Handles Me.Load

        '[Note]:使用該組Stage的第二組閥   20170321   此部分參數帶入不寫死
        'sys.SelectValve = eValveWorkMode.Valve2

        'With gSSystemParameter.Pos.CCDValveCalibration(sys.StageNo)
        '        .LoadCalibrationValve(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini")
        '        mAbsValveNo = sys.ValveNo(enmValveWorkMode.Valve1) '取得絕對索引避免被外部影響
        '        txtTeachCCD2X.Text = .CCDCalibPosX(mAbsValveNo)
        '        txtTeachCCD2Y.Text = .CCDCalibPosY(mAbsValveNo)
        '        txtTeachCCD2Z.Text = .CCDCalibPosZ(mAbsValveNo)
        '        txtTeachDispenser2X.Text = .ValveCalibPosX(mAbsValveNo)
        '        txtTeachDispenser2Y.Text = .ValveCalibPosY(mAbsValveNo)
        '        txtTeachDispenser2Z.Text = .ValveCalibPosZ(mAbsValveNo)
        '    End With

        '    With UcJoyStick1
        '        .AxisX = sys.AxisX
        '        .AxisY = sys.AxisY
        '        .AxisZ = sys.AxisZ
        '    End With
        '    UcJoyStick1.SetSpeedType(SpeedType.Slow)
        '    UcJoyStick1.RefreshPosition()
        '    UcCalibrationStatus1.Status = ucCalibrationStatus.enmCalibrationStatus.CCD2Valve2

        '    Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        '    Me.Height = Screen.PrimaryScreen.WorkingArea.Height
        '    Select Case gAOICollection.GetCCDType(sys.CCDNo) 'gSSystemParameter.enmCCDType
        '        Case enmCCDType.CognexVPRO
        '            UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
        '        Case enmCCDType.OmronFZS2MUDP
        '            UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
        '        Case Else
        '            UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
        '    End Select

        '    If UcDisplay1.StartLive(sys.CCDNo) = False Then 'Soni / 2016.09.07 增加異常時顯示與紀錄
        '        gEqpMsg.Add("Error_1012002", Error_1012002, eMessageLevel.Error)
        '        MsgBox(gMsgHandler.GetMessage(Error_1012002))
        '    End If
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub btnPrevPage_Click(sender As Object, e As EventArgs) Handles btnPrevPage.Click
        gSyslog.Save("[frmCalibrationCCD2Valve2]" & vbTab & "[btnPrevPage]" & vbTab & "Click (->Valve1.)")

        Me.Hide()
        'WriteButtonLog(gUserLevel, "frmCalibrationCCD2Valve2", "btnPrevPage")
        If gfrmCalibrationCCD2Valve1 Is Nothing Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        ElseIf gfrmCalibrationCCD2Valve1 Is Nothing Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        End If
        With gfrmCalibrationCCD2Valve1
            .sys = sys
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            'gSyslog.Save("Click Prev Page.(->Valve1.)")
            .Show()
            .BringToFront()
        End With

    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click
        '    gSyslog.Save("[frmCalibrationCCD2Valve2]" & vbTab & "[btnNextPage]" & vbTab & "Click (->Height Sensor)")
        '    UcDisplay1.EndLive()
        '    With gSSystemParameter.Pos.CCDValveCalibration(sys.StageNo)
        '        .CCDCalibPosX(1) = txtTeachCCD2X.Text
        '        .CCDCalibPosY(1) = txtTeachCCD2Y.Text
        '        .CCDCalibPosZ(1) = txtTeachCCD2Z.Text
        '        .ValveCalibPosX(1) = txtTeachDispenser2X.Text
        '        .ValveCalibPosY(1) = txtTeachDispenser2Y.Text
        '        .ValveCalibPosZ(1) = txtTeachDispenser2Z.Text
        '        .SaveCalibrationValve(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini") '儲存設定
        '    End With
        '    'WriteButtonLog(gUserLevel, "frmCalibrationCCD2Valve2", "btnNextPage")
        '    If gfrmCalibrationZHeight Is Nothing Then
        '        gfrmCalibrationZHeight = New frmCalibrationZHeight
        '    ElseIf gfrmCalibrationZHeight.IsDisposed Then
        '        gfrmCalibrationZHeight = New frmCalibrationZHeight
        '    End If
        '    With gfrmCalibrationZHeight
        '        .sys = sys
        '        .WindowState = FormWindowState.Maximized
        '        .StartPosition = FormStartPosition.Manual
        '        .Location = New Point(0, 0)
        '        .Show()
        '        .BringToFront()
        '    End With
        '    Me.Hide()
    End Sub

    Private Sub btnGo1_Click(sender As Object, e As EventArgs) Handles btnGo1.Click
        ''gSyslog.Save("[frmCalibrationCCD2Valve2]" & vbTab & "[btnGo1]" & vbTab & "Click")
        ' ''WriteButtonLog(gUserLevel, "frmCalibrationCCD2Valve2", "btnGo1")
        ''With gSSystemParameter.Pos.CCDValveCalibration(sys.StageNo)
        ''    gSyslog.Save("X:" & .CCDCalibPosX(mAbsValveNo))
        ''    gSyslog.Save("Y:" & .CCDCalibPosY(mAbsValveNo))
        ''    gSyslog.Save("Z:" & .CCDCalibPosZ(mAbsValveNo))
        ''    Dim AxisNo(4) As Integer
        ''    Dim TargetPos(4) As Decimal
        ''    AxisNo(0) = sys.AxisX
        ''    AxisNo(1) = sys.AxisY
        ''    AxisNo(2) = sys.AxisZ
        ''    AxisNo(3) = sys.AxisB
        ''    AxisNo(4) = sys.AxisC

        ''    TargetPos(0) = .CCDCalibPosX(mAbsValveNo)
        ''    TargetPos(1) = .CCDCalibPosY(mAbsValveNo)
        ''    TargetPos(2) = .CCDCalibPosZ(mAbsValveNo)
        ''    TargetPos(3) = 0
        ''    TargetPos(4) = 0
        ''    ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        ''End With

    End Sub

    Private Sub btnGo2_Click(sender As Object, e As EventArgs) Handles btnGo2.Click
        'gSyslog.Save("[frmCalibrationCCD2Valve2]" & vbTab & "[btnGo2]" & vbTab & "Click")
        ''WriteButtonLog(gUserLevel, "frmCalibrationCCD2Valve2", "btnGo2")
        'With gSSystemParameter.Pos.CCDValveCalibration(sys.StageNo)
        '    gSyslog.Save("X:" & .ValveCalibPosX(mAbsValveNo))
        '    gSyslog.Save("Y:" & .ValveCalibPosY(mAbsValveNo))
        '    gSyslog.Save("Z:" & .ValveCalibPosZ(mAbsValveNo))
        '    Dim AxisNo(4) As Integer
        '    Dim TargetPos(4) As Decimal
        '    AxisNo(0) = sys.AxisX
        '    AxisNo(1) = sys.AxisY
        '    AxisNo(2) = sys.AxisZ
        '    AxisNo(3) = sys.AxisB
        '    AxisNo(4) = sys.AxisC

        '    TargetPos(0) = .ValveCalibPosX(mAbsValveNo)
        '    TargetPos(1) = .ValveCalibPosY(mAbsValveNo)
        '    TargetPos(2) = .ValveCalibPosZ(mAbsValveNo)
        '    TargetPos(3) = 0
        '    TargetPos(4) = 0
        '    ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        'End With

    End Sub

    Private Sub btnValve2On_Click(sender As Object, e As EventArgs) Handles btnValve2On.Click
        gSyslog.Save("[frmCalibrationCCD2Valve2]" & vbTab & "[btnValve2On]" & vbTab & "Click")
        'WriteButtonLog(gUserLevel, "frmCalibrationCCD2Valve2", "btnValve2On")
        btnValve2On.BackColor = Color.Yellow
        btnValve2On.Refresh() 'Soni / 2017.05.10
        Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eON)
        System.Threading.Thread.Sleep(10)
        Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eOff)
        btnValve2On.BackColor = SystemColors.Control
        btnValve2On.Refresh() 'Soni / 2017.05.10
       
    End Sub


    Private Sub btnDispenserNo1EPRegulator_Click(sender As Object, e As EventArgs) Handles btnDispenserNo1EPRegulator.Click
        gSyslog.Save("[frmCalibrationCCD2Valve2]" & vbTab & "[btnDispenserNo1EPRegulator]" & vbTab & "Click")
        '[說明]:紀錄按了哪些按鈕
        'Call WriteButtonLog(gUserLevel, "frmManual", "btnDispenserNo1EPRegulator")

        '[說明]:AO_人機設定值-1號膠槍電空閥壓力設定
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6001011, txtDispenserNo1EPRegulatorAO.Text), "INFO_6001011")
        If IsNumeric(txtDispenserNo1EPRegulatorAO.Text) = True Then
            gAOCollection.Value(enmAO.DispenserNo2EPRegulator) = txtDispenserNo1EPRegulatorAO.Text
            'Do
            '    If gUpdateIOState.Advantech_AO = True Then
            '        Call AO_SetVoltage()    '寫入卡片內的AO值
            '        Exit Do
            '    End If
            '    Application.DoEvents()
            'Loop
            txtDispenserNo1EPRegulatorAO.BackColor = Color.White
        Else
            txtDispenserNo1EPRegulatorAO.BackColor = Color.Red
        End If

    End Sub

    Private Sub btnGlueNo1SyringePressure_Click(sender As Object, e As EventArgs) Handles btnGlueNo1SyringePressure.Click
        gSyslog.Save("[frmCalibrationCCD2Valve2]" & vbTab & "[btnGlueNo1SyringePressure]" & vbTab & "Click")
        '[說明]:紀錄按了哪些按鈕
        'Call WriteButtonLog(gUserLevel, "frmManual", "btnGlueNo1SyringePressure")

        '[說明]:DO-1號膠桶正壓
        gDOCollection.SetState(enmDO.SyringePressure2, Not gDOCollection.GetState(enmDO.SyringePressure2))
        If gSSystemParameter.RunMode = enmRunMode.Run Then
            Call gDOCollection.RefreshDO() '寫入卡片 DO點
        End If

        If gDOCollection.GetState(enmDO.SyringePressure2) Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6001022), "INFO_6001022")
            btnGlueNo1SyringePressure.Text = "Glue Syringe Pressure On"
            btnGlueNo1SyringePressure.BackColor = Color.Yellow
        Else
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6001023), "INFO_6001023")
            btnGlueNo1SyringePressure.Text = "Glue Syringe Pressure Off"
            btnGlueNo1SyringePressure.BackColor = SystemColors.Control
        End If
    End Sub

End Class
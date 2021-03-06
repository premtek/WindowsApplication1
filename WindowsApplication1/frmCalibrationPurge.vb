﻿Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectAOI
Imports ProjectLaserInterferometer

Public Class frmCalibrationPurge
    ''' <summary>目前類別名稱</summary>
    ''' <remarks></remarks>
    Private Const ClassName As String = "frmCalibrationFlowRate"
    Dim mAbsValveNo As Integer = enmValve.No1
    Private Const CalibSceneName As String = "CALIBMark"

    Private Const LightSceneName As String = "CALIB"
    
    ''' <summary>對外</summary>
    ''' <remarks></remarks>
    Public sys As sSysParam
    Public CalibrationEnable As Boolean

    ''' <summary>設定CCD位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCalibrationPurgeSetCcdPos_Click(sender As Object, e As EventArgs) Handles btnCalibrationPurgeSetCcdPos.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationPurgeSetCcdPos]" & vbTab & "Click")
        btnCalibrationPurgeSetCcdPos.Enabled = False
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnCalibrationPurgeSetCcdPos.Enabled = True
            Exit Sub
        End If
        UcJoyStick1.RefreshPosition()
        'jimmy 20161130 Tilt參考水平位置
        Select Case sys.StageNo
            Case 0
                txtCalibrationPurgePosX.Text = gCMotion.GetPositionValue(sys.AxisX) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage1TiltAngle)
                txtCalibrationPurgePosY.Text = gCMotion.GetPositionValue(sys.AxisY) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage1TiltAngle)
                txtCalibrationPurgePosZ.Text = gCMotion.GetPositionValue(sys.AxisZ) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetZ(sys.SelectValve, gSSystemParameter.Stage1TiltAngle)
            Case 1
                txtCalibrationPurgePosX.Text = gCMotion.GetPositionValue(sys.AxisX) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage2TiltAngle)
                txtCalibrationPurgePosY.Text = gCMotion.GetPositionValue(sys.AxisY) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage2TiltAngle)
                txtCalibrationPurgePosZ.Text = gCMotion.GetPositionValue(sys.AxisZ) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetZ(sys.SelectValve, gSSystemParameter.Stage2TiltAngle)
            Case 2
                txtCalibrationPurgePosX.Text = gCMotion.GetPositionValue(sys.AxisX) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage3TiltAngle)
                txtCalibrationPurgePosY.Text = gCMotion.GetPositionValue(sys.AxisY) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage3TiltAngle)
                txtCalibrationPurgePosZ.Text = gCMotion.GetPositionValue(sys.AxisZ) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetZ(sys.SelectValve, gSSystemParameter.Stage3TiltAngle)
            Case 3
                txtCalibrationPurgePosX.Text = gCMotion.GetPositionValue(sys.AxisX) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage4TiltAngle)
                txtCalibrationPurgePosY.Text = gCMotion.GetPositionValue(sys.AxisY) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage4TiltAngle)
                txtCalibrationPurgePosZ.Text = gCMotion.GetPositionValue(sys.AxisZ) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetZ(sys.SelectValve, gSSystemParameter.Stage4TiltAngle)


        End Select

        txtCalibrationPurgeCCDPosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtCalibrationPurgeCCDPosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtCalibrationPurgeCCDPosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        btnCalibrationPurgeSetCcdPos.Enabled = True
    End Sub

    ''' <summary>設定Purge位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCalibrationSetPurgePos_Click(sender As Object, e As EventArgs) Handles btnCalibrationSetPurgePos.Click
        btnCalibrationSetPurgePos.Enabled = False
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnSetPurgePos]" & vbTab & "Click")
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnCalibrationSetPurgePos.Enabled = True
            Exit Sub
        End If

        UcJoyStick1.RefreshPosition()
        txtCalibrationPurgePosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtCalibrationPurgePosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtCalibrationPurgePosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        btnCalibrationSetPurgePos.Enabled = True
    End Sub

    ''' <summary>關閉表單</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCalibrationPurge_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'UcDisplay1.EndLive() 'Eason 20170124 Ticket:100031 , Memory Freed : Change to FormClosed 
        'Timer1.Enabled = False 'Eason 20170124 Ticket:100031 , Memory Freed : Change to FormClosed 
        'Me.Dispose(True) 'Eason 20170124 Ticket:100031 , Memory Freed : Change to FormClosed 
        'GC.Collect()'Eason 20170124 Ticket:100031 , Memory Freed : Change to FormClosed 

        '20170321
        UcDisplay1.EndLive()
        Timer1.Enabled = False
        UcDisplay1.ManualDispose()
        UcLightControl1.ManualDispose()
        UcJoyStick1.ManualDispose()
        Me.Dispose(True)
    End Sub

    ''' <summary>載入表單</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCalibrationPurge_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '20170327
        Me.Text = "Purge Position " & "Stage" & (sys.EsysNum - eSys.DispStage1 + 1) & " Valve" & (sys.SelectValve + 1)


        'jimmy 20170630
        If gSSystemParameter.IsCleanDevice = True Then
            btnPurgeNext.Visible = True
            btnPurgePrev.Visible = True
        Else
            btnPurgeNext.Visible = False
            btnPurgePrev.Visible = True
        End If


        '20170630
        'If gCRecipe.strName <> "" Then
        '    If gPurgeDB(gCRecipe.StageParts(sys.StageNo).PurgeName(sys.SelectValve)).CleanType = eCleanType.VacuumClean Then
        '        btnPurgeNext.Visible = False
        '    End If
        'End If


        Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height
        lblSceneSet.Text = CalibSceneName & (sys.CCDNo + 1).ToString

        '[Note]:使用該組Stage的第一組閥   20170321   此部分參數帶入不寫死
        'sys.SelectValve = eValveWorkMode.Valve1

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCS_F230A, enmMachineType.DCS_350A
                grpPurgeAlignPos.Visible = True
            Case Else
                grpPurgeAlignPos.Visible = False
        End Select

        With gSSystemParameter.Pos.PurgeCalibration(sys.StageNo)
            '此部分使用enmValveWorkMode.Valve1還待商榷
            mAbsValveNo = sys.ValveNo(eValveWorkMode.Valve1)
            nmcDispenserNo1PurgeTime.Text = gSSystemParameter.StageParts.ValveData(sys.StageNo).PurgeTime(sys.SelectValve)
            txtCalibrationPurgeCCDPosX.Text = .CCDPosX(sys.SelectValve)
            txtCalibrationPurgeCCDPosY.Text = .CCDPosY(sys.SelectValve)
            txtCalibrationPurgeCCDPosZ.Text = .CCDPosZ(sys.SelectValve)

            txtCalibrationPurgeAlign1PosX.Text = .CCDAlign1PosX(sys.SelectValve)
            txtCalibrationPurgeAlign1PosY.Text = .CCDAlign1PosY(sys.SelectValve)
            txtCalibrationPurgeAlign1PosZ.Text = .CCDAlign1PosZ(sys.SelectValve)
            txtCalibrationPurgeAlign2PosX.Text = .CCDAlign2PosX(sys.SelectValve)
            txtCalibrationPurgeAlign2PosY.Text = .CCDAlign2PosY(sys.SelectValve)
            txtCalibrationPurgeAlign2PosZ.Text = .CCDAlign2PosZ(sys.SelectValve)
            txtCalibrationPurgeAlign3PosX.Text = .CCDAlign3PosX(sys.SelectValve)
            txtCalibrationPurgeAlign3PosY.Text = .CCDAlign3PosY(sys.SelectValve)
            txtCalibrationPurgeAlign3PosZ.Text = .CCDAlign3PosZ(sys.SelectValve)

            txtCalibrationPurgePosX.Text = .ValvePosX(sys.SelectValve)
            txtCalibrationPurgePosY.Text = .ValvePosY(sys.SelectValve)
            txtCalibrationPurgePosZ.Text = .ValvePosZ(sys.SelectValve)
        End With

        With UcJoyStick1
            .AxisX = sys.AxisX
            .AxisY = sys.AxisY
            .AxisZ = sys.AxisZ
            .AXisA = sys.AxisA
            .AXisB = sys.AxisB
            .AXisC = sys.AxisC
        End With
        UcJoyStick1.SetSpeedType(SpeedType.Slow)
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

        Select Case gAOICollection.GetCCDType(sys.CCDNo)
            Case enmCCDType.CognexVPRO
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
            Case enmCCDType.OmronFZS2MUDP
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
            Case Else
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
        End Select

        Dim fileName As String
        fileName = Application.StartupPath & "\System\" & MachineName & "\" & CalibSceneName & (sys.CCDNo + 1).ToString & ".ini" '光源設定檔路徑
        gAOICollection.LoadSceneParameter(lblSceneSet.Text, fileName) '讀取光源,曝光值等設定
        'If gAOICollection.SceneDictionary.ContainsKey(CalibSceneName & (sys.CCDNo + 1).ToString) Then
        '    nmcExposure.Value = gAOICollection.SceneDictionary(CalibSceneName & (sys.CCDNo + 1).ToString).CCDExposureTime
        'Else
        '    nmcExposure.Value = 5
        'End If


        '--- 2016.06.18 + 開燈,曝光,觸發後才會改 ---
        UcLightControl1.CCDNo = sys.CCDNo
        If gAOICollection.SceneDictionary.ContainsKey(LightSceneName & (sys.CCDNo + 1).ToString) Then
            UcLightControl1.SceneName = LightSceneName & (sys.CCDNo + 1).ToString
            UcLightControl1.ShowUI()
            SelectScene(LightSceneName & (sys.CCDNo + 1).ToString) '場景開光
        End If
        'If gAOICollection.IsSceneExist(sys.CCDNo, CalibSceneName & (sys.CCDNo + 1).ToString) Then
        '    gAOICollection.SetCCDScene(sys.CCDNo, CalibSceneName & (sys.CCDNo + 1).ToString) '曝光,亮度
        'End If

        ''TODO:已無曝光切換射計, Form_Load觸發可刪除
        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        'System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照
        'System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        ''--- 2016.06.18 + 開燈,曝光,觸發後才會改 ---

        If UcDisplay1.StartLive(sys.CCDNo) = False Then 'Soni / 2016.09.07 增加異常時顯示與紀錄
            'CCD 取像TimeOut
            Select Case sys.StageNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012003))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012303))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 2
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012603))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012603), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 3
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012903))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012903), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
        End If


        Select Case sys.StageNo
            Case 0
                txtTiltValvePosB.Text = gSSystemParameter.Stage1TiltAngle.ToString
            Case 1
                txtTiltValvePosB.Text = gSSystemParameter.Stage2TiltAngle.ToString
            Case 2
                txtTiltValvePosB.Text = gSSystemParameter.Stage3TiltAngle.ToString
            Case 3
                txtTiltValvePosB.Text = gSSystemParameter.Stage4TiltAngle.ToString
        End Select

        If sys.AxisB <> -1 Then
            gpTiltMove.Visible = True
        Else
            gpTiltMove.Visible = False
        End If
        UcJoyStick1.RefreshPosition()
        Timer1.Enabled = True
        Call Timer1_Tick(sender, e)

        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    ''' <summary>選擇場景, 開啟光源</summary>
    ''' <param name="sceneName"></param>
    ''' <remarks></remarks>
    Sub SelectScene(ByVal sceneName As String)
        If gAOICollection.SceneDictionary.ContainsKey(sceneName) Then
            Dim light As enmLight
            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
            If light >= 0 Then
                gLightCollection.SetCCDLight(sys.CCDNo, light, gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No1), True)
                If gLightCollection.GetResultStatus(sys.CCDNo, light) = False Then
                    MsgBox(sceneName & gMsgHandler.GetMessage(Error_1022002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End If
                gSysAdapter.SetLightOnOff(light, gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No1))
            End If


            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
            If light >= 0 Then
                gLightCollection.SetCCDLight(sys.CCDNo, light, gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No2), True)
                If gLightCollection.GetResultStatus(sys.CCDNo, light) = False Then
                    MsgBox(sceneName & gMsgHandler.GetMessage(Error_1022002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End If
                gSysAdapter.SetLightOnOff(light, gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No2))
            End If


            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3)
            If light >= 0 Then
                gLightCollection.SetCCDLight(sys.CCDNo, light, gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No3), True)
                If gLightCollection.GetResultStatus(sys.CCDNo, light) = False Then
                    MsgBox(sceneName & gMsgHandler.GetMessage(Error_1022002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End If
                gSysAdapter.SetLightOnOff(light, gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No3))
            End If


            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4)
            If light >= 0 Then
                gLightCollection.SetCCDLight(sys.CCDNo, light, gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No4), True)
                If gLightCollection.GetResultStatus(sys.CCDNo, light) = False Then
                    MsgBox(sceneName & gMsgHandler.GetMessage(Error_1022002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End If
                gSysAdapter.SetLightOnOff(light, gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No4))
            End If
        Else
            '場景不存在
            gSyslog.Save(sceneName & gMsgHandler.GetMessage(Warn_3000020))
            MsgBox(sceneName & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox(sceneName & " Not Exists!")
        End If
    End Sub


    ''' <summary>移動到CCD位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCalibrationPurgeGoCCDPos_Click(sender As Object, e As EventArgs) Handles btnCalibrationPurgeGoCCDPos.Click
        'Dim mlight As enmLight                                    '[光源]

        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnPurgeGoCCDPos]" & vbTab & "Click")

        '[說明]:位置安全保護
        If CheckGoCCDPos() = False Then
            Exit Sub
        End If

        btnCalibrationPurgeGoCCDPos.Enabled = False
        '20170602按鍵保護
        btnGoTilt.Enabled = False
        btnCalibrationPurgeSetCcdPos.Enabled = False
        btnCalibrationSetPurgePos.Enabled = False
        btnCalibrationPurgeGoPos.Enabled = False
        btnPurge.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationPurgeSetAlign1Pos.Enabled = False
        btnCalibrationPurgeGoAlign1Pos.Enabled = False
        btnCalibrationPurgeSetAlign2Pos.Enabled = False
        btnCalibrationPurgeGoAlign2Pos.Enabled = False
        btnCalibrationPurgeSetAlign3Pos.Enabled = False
        btnCalibrationPurgeGoAlign3Pos.Enabled = False
        btnAlign.Enabled = False
        btnPurgePrev.Enabled = False
        btnPurgeNext.Enabled = False
        btnCalibrationPurgeOK.Enabled = False
        btnCalibrationPurgeCancel.Enabled = False
        UcJoyStick1.Enabled = False

        '[Note]:開啓光源
        Dim mlight As enmLight
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
        Call gSysAdapter.SetLightOnOff(mlight, UcLightControl1.GetLight1_OnOff)
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
        Call gSysAdapter.SetLightOnOff(mlight, UcLightControl1.GetLight2_OnOff)
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3)
        Call gSysAdapter.SetLightOnOff(mlight, UcLightControl1.GetLight3_OnOff)
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4)
        Call gSysAdapter.SetLightOnOff(mlight, UcLightControl1.GetLight4_OnOff)

        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = -1
        AxisNo(4) = sys.AxisC

        TargetPos(0) = txtCalibrationPurgeCCDPosX.Text
        TargetPos(1) = txtCalibrationPurgeCCDPosY.Text
        TargetPos(2) = txtCalibrationPurgeCCDPosZ.Text
        TargetPos(3) = 0
        TargetPos(4) = 0
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6001052, TargetPos(0), TargetPos(1), TargetPos(2)), "INFO_6001052")
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        UcJoyStick1.RefreshPosition()
        btnCalibrationPurgeGoCCDPos.Enabled = True

        '20170602按鍵保護
        btnGoTilt.Enabled = True
        btnCalibrationPurgeSetCcdPos.Enabled = True
        btnCalibrationSetPurgePos.Enabled = True
        btnCalibrationPurgeGoPos.Enabled = True
        btnPurge.Enabled = True
        btnTrainScene.Enabled = True
        btnCalibrationPurgeSetAlign1Pos.Enabled = True
        btnCalibrationPurgeGoAlign1Pos.Enabled = True
        btnCalibrationPurgeSetAlign2Pos.Enabled = True
        btnCalibrationPurgeGoAlign2Pos.Enabled = True
        btnCalibrationPurgeSetAlign3Pos.Enabled = True
        btnCalibrationPurgeGoAlign3Pos.Enabled = True
        btnAlign.Enabled = True
        btnPurgePrev.Enabled = True
        btnPurgeNext.Enabled = True
        btnCalibrationPurgeOK.Enabled = True
        btnCalibrationPurgeCancel.Enabled = True
        UcJoyStick1.Enabled = True

    End Sub

    Public Function CheckGoCCDPos() As Boolean
        If Val(txtCalibrationPurgeCCDPosX.Text) < gCMotion.AxisParameter(sys.AxisX).Limit.NegativeLimit Or Val(txtCalibrationPurgeCCDPosX.Text) > gCMotion.AxisParameter(sys.AxisX).Limit.PosivtiveLimit Or
           Val(txtCalibrationPurgeCCDPosY.Text) < gCMotion.AxisParameter(sys.AxisY).Limit.NegativeLimit Or Val(txtCalibrationPurgeCCDPosY.Text) > gCMotion.AxisParameter(sys.AxisY).Limit.PosivtiveLimit Or
           Val(txtCalibrationPurgeCCDPosZ.Text) < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Or Val(txtCalibrationPurgeCCDPosZ.Text) > gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit Then
            btnCalibrationPurgeGoCCDPos.Enabled = False
            Return False
        Else
            btnCalibrationPurgeGoCCDPos.Enabled = True
            Return True
        End If
    End Function

    Public Function CheckGoPurgePos() As Boolean
        If Val(txtCalibrationPurgePosX.Text) < gCMotion.AxisParameter(sys.AxisX).Limit.NegativeLimit Or Val(txtCalibrationPurgePosX.Text) > gCMotion.AxisParameter(sys.AxisX).Limit.PosivtiveLimit Or
           Val(txtCalibrationPurgePosY.Text) < gCMotion.AxisParameter(sys.AxisY).Limit.NegativeLimit Or Val(txtCalibrationPurgePosY.Text) > gCMotion.AxisParameter(sys.AxisY).Limit.PosivtiveLimit Or
           Val(txtCalibrationPurgePosZ.Text) < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Or Val(txtCalibrationPurgePosZ.Text) > gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit Then
            btnCalibrationPurgeGoPos.Enabled = False
            Return False
        Else
            btnCalibrationPurgeGoPos.Enabled = True
            Return True
        End If
    End Function
    Public Function CheckGoPurgeAlignPos() As Boolean
        If Val(txtCalibrationPurgeAlign1PosX.Text) < gCMotion.AxisParameter(sys.AxisX).Limit.NegativeLimit Or Val(txtCalibrationPurgeAlign1PosX.Text) > gCMotion.AxisParameter(sys.AxisX).Limit.PosivtiveLimit Or
           Val(txtCalibrationPurgeAlign1PosY.Text) < gCMotion.AxisParameter(sys.AxisY).Limit.NegativeLimit Or Val(txtCalibrationPurgeAlign1PosY.Text) > gCMotion.AxisParameter(sys.AxisY).Limit.PosivtiveLimit Or
           Val(txtCalibrationPurgeAlign1PosZ.Text) < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Or Val(txtCalibrationPurgeAlign1PosZ.Text) > gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit Then
            btnCalibrationPurgeGoAlign1Pos.Enabled = False
            Return False
        Else
            btnCalibrationPurgeGoAlign1Pos.Enabled = True
            Return True
        End If
    End Function


    ''' <summary>移動到測Purge位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCalibrationPurgeGoPos_Click(sender As Object, e As EventArgs) Handles btnCalibrationPurgeGoPos.Click
        Dim mlight As enmLight                                    '[光源]
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnPurgeGoPos]" & vbTab & "Click")

        '[說明]:位置安全保護
        If CheckGoPurgePos() = False Then
            Exit Sub
        End If

        btnCalibrationPurgeGoPos.Enabled = False

        '20170602按鍵保護
        btnGoTilt.Enabled = False
        btnCalibrationPurgeSetCcdPos.Enabled = False
        btnCalibrationSetPurgePos.Enabled = False
        btnCalibrationPurgeGoCCDPos.Enabled = False
        btnPurge.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationPurgeSetAlign1Pos.Enabled = False
        btnCalibrationPurgeGoAlign1Pos.Enabled = False
        btnCalibrationPurgeSetAlign2Pos.Enabled = False
        btnCalibrationPurgeGoAlign2Pos.Enabled = False
        btnCalibrationPurgeSetAlign3Pos.Enabled = False
        btnCalibrationPurgeGoAlign3Pos.Enabled = False
        btnAlign.Enabled = False
        btnPurgePrev.Enabled = False
        btnPurgeNext.Enabled = False
        btnCalibrationPurgeOK.Enabled = False
        btnCalibrationPurgeCancel.Enabled = False
        UcJoyStick1.Enabled = False


        '[Note]:關閉光源
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
        Call gSysAdapter.SetLightOnOff(mlight, False)
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
        Call gSysAdapter.SetLightOnOff(mlight, False)


        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        '20170807先移動XY&Tilt
        TargetPos(0) = txtCalibrationPurgePosX.Text
        TargetPos(1) = txtCalibrationPurgePosY.Text
        'TargetPos(2) = txtCalibrationPurgePosZ.Text
        TargetPos(2) = 0
        TargetPos(3) = CDec(Val(txtTiltValvePosB.Text))
        TargetPos(4) = 0
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)

        '20170807變更z軸高度
        TargetPos(2) = txtCalibrationPurgePosZ.Text

        '20170807
        If MsgBox("Z Stage Will move down，please check PosZ:" + txtCalibrationPurgePosZ.Text + " is safe", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
            gSyslog.Save("[frmCalibrationPurge]" & vbTab & "[btnCalibrationPurgeGoPos_Click]" & vbTab & "Stage in Safe Pos By User Check")
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, TargetPos(0), TargetPos(1), TargetPos(2)), "INFO_6019015")
            ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        End If

        UcJoyStick1.RefreshPosition()
        btnCalibrationPurgeGoPos.Enabled = True
        '20170602按鍵保護
        btnGoTilt.Enabled = True
        btnCalibrationPurgeSetCcdPos.Enabled = True
        btnCalibrationSetPurgePos.Enabled = True
        btnCalibrationPurgeGoCCDPos.Enabled = True
        btnPurge.Enabled = True
        btnTrainScene.Enabled = True
        btnCalibrationPurgeSetAlign1Pos.Enabled = True
        btnCalibrationPurgeGoAlign1Pos.Enabled = True
        btnCalibrationPurgeSetAlign2Pos.Enabled = True
        btnCalibrationPurgeGoAlign2Pos.Enabled = True
        btnCalibrationPurgeSetAlign3Pos.Enabled = True
        btnCalibrationPurgeGoAlign3Pos.Enabled = True
        btnAlign.Enabled = True
        btnPurgePrev.Enabled = True
        btnPurgeNext.Enabled = True
        btnCalibrationPurgeOK.Enabled = True
        btnCalibrationPurgeCancel.Enabled = True
        UcJoyStick1.Enabled = True

    End Sub

    ''' <summary>放棄離開</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCalibrationPurgeCancel_Click(sender As Object, e As EventArgs) Handles btnCalibrationPurgeCancel.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click.")
        '20170623 離開時Z 軸升至安全高度
        gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.WeightCalibration(sys.StageNo).SafePosZ(sys.SelectValve))

        '20171108
        If sys.SelectValve = eValveWorkMode.Valve2 Then
            ValveCylinderAction(sys.StageNo, sys.SelectValve, enmUpDown.Up) '汽缸上
        End If
        Me.Close()
    End Sub




    Private Sub btnCalibrationPurgeOK_Click(sender As Object, e As EventArgs) Handles btnCalibrationPurgeOK.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationPurgeOK]" & vbTab & "Click.")
        btnCalibrationPurgeOK.Enabled = False

        '此部分需取代
        'gSSystemParameter.ValvePurgePosX(enmValve.No1)
        'gSSystemParameter.Pos.ValveWeightSafePosX(enmValve.No1) = nmcSafePosX.Value
        'CCDFlowRateCalibration

        With gSSystemParameter.Pos.PurgeCalibration(sys.StageNo)
            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
            gSyslog.Save("CCD Pos X:" & .CCDPosX(sys.SelectValve))
            gSyslog.Save("CCD Pos Y:" & .CCDPosY(sys.SelectValve))
            gSyslog.Save("CCD Pos Z:" & .CCDPosZ(sys.SelectValve))
            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")

            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ")  Start")
            gSyslog.Save("FlowRate Pos X:" & .ValvePosX(sys.SelectValve))
            gSyslog.Save("FlowRate Pos Y:" & .ValvePosY(sys.SelectValve))
            gSyslog.Save("FlowRate Pos Z:" & .ValvePosZ(sys.SelectValve))
            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ")  End")

            'CCD校正位置
            .CCDPosX(sys.SelectValve) = CDec(txtCalibrationPurgeCCDPosX.Text)
            .CCDPosY(sys.SelectValve) = CDec(txtCalibrationPurgeCCDPosY.Text)
            .CCDPosZ(sys.SelectValve) = CDec(txtCalibrationPurgeCCDPosZ.Text)
            'CCD特徵點位置
            .CCDAlign1PosX(sys.SelectValve) = CDec(txtCalibrationPurgeAlign1PosX.Text)
            .CCDAlign1PosY(sys.SelectValve) = CDec(txtCalibrationPurgeAlign1PosY.Text)
            .CCDAlign1PosZ(sys.SelectValve) = CDec(txtCalibrationPurgeAlign1PosZ.Text)
            .CCDAlign2PosX(sys.SelectValve) = CDec(txtCalibrationPurgeAlign2PosX.Text)
            .CCDAlign2PosY(sys.SelectValve) = CDec(txtCalibrationPurgeAlign2PosY.Text)
            .CCDAlign2PosZ(sys.SelectValve) = CDec(txtCalibrationPurgeAlign2PosZ.Text)
            .CCDAlign3PosX(sys.SelectValve) = CDec(txtCalibrationPurgeAlign3PosX.Text)
            .CCDAlign3PosY(sys.SelectValve) = CDec(txtCalibrationPurgeAlign3PosY.Text)
            .CCDAlign3PosZ(sys.SelectValve) = CDec(txtCalibrationPurgeAlign3PosZ.Text)
            'FlowRate校正位置
            .ValvePosX(sys.SelectValve) = CDec(txtCalibrationPurgePosX.Text)
            .ValvePosY(sys.SelectValve) = CDec(txtCalibrationPurgePosY.Text)
            .ValvePosZ(sys.SelectValve) = CDec(txtCalibrationPurgePosZ.Text)

            gSSystemParameter.Pos.PurgeCalibration(sys.StageNo).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini") '儲存系統位置(by機型)
            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
            gSyslog.Save("CCD Pos X:" & .CCDPosX(sys.SelectValve))
            gSyslog.Save("CCD Pos Y:" & .CCDPosY(sys.SelectValve))
            gSyslog.Save("CCD Pos Z:" & .CCDPosZ(sys.SelectValve))
            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")

            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ")  Start")
            gSyslog.Save("FlowRate Pos X:" & .ValvePosX(sys.SelectValve))
            gSyslog.Save("FlowRate Pos Y:" & .ValvePosY(sys.SelectValve))
            gSyslog.Save("FlowRate Pos Z:" & .ValvePosZ(sys.SelectValve))
            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ")  End")

            gSSystemParameter.StageParts.ValveData(sys.StageNo).PurgeTime(sys.SelectValve) = CDbl(nmcDispenserNo1PurgeTime.Text)
            gSSystemParameter.StageParts.ValveData(sys.StageNo).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini")
        End With
        '場景曝光與光源存檔
        Dim SceneName As String = LightSceneName & (sys.CCDNo + 1).ToString
        Dim fileName As String = Application.StartupPath & "\System\" & MachineName & "\" & SceneName & ".ini"  'System.IO.Path.GetDirectoryName(gCRecipe.strFileName) & "\" & SceneName & ".ini" '光源設定檔路徑

        Dim mScene As New CSceneParameter
        With mScene
            .LightValue(0) = UcLightControl1.GetLight1Value
            .LightValue(1) = UcLightControl1.GetLight2Value
            .LightValue(2) = UcLightControl1.GetLight3Value
            .LightValue(3) = UcLightControl1.GetLight4Value

            .LightEnable(0) = UcLightControl1.GetLight1_OnOff
            .LightEnable(1) = UcLightControl1.GetLight2_OnOff
            .LightEnable(2) = UcLightControl1.GetLight3_OnOff
            .LightEnable(3) = UcLightControl1.GetLight4_OnOff
            '.CCDExposureTime = nmcExposure.Value
        End With

        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            gAOICollection.SceneDictionary(SceneName) = mScene
        Else
            gAOICollection.SceneDictionary.Add(SceneName, mScene)
        End If
        gAOICollection.SaveSceneParameter(SceneName, fileName)

        btnCalibrationPurgeOK.Enabled = True

        If CalibrationEnable = True Then
            '存檔成功 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'UcDisplay1.EndLive()
        ElseIf CalibrationEnable = False Then
            '存檔成功 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'Me.Close()
        End If
    End Sub



    Private Sub btnPurge_Click(sender As Object, e As EventArgs) Handles btnPurge.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnPurge]" & vbTab & "Click")

        If btnPurge.Enabled = False Then
            Exit Sub
        End If

        btnPurge.Enabled = False
        '20170602: 按鍵保護()
        btnGoTilt.Enabled = False
        btnCalibrationPurgeSetCcdPos.Enabled = False
        btnCalibrationPurgeGoCCDPos.Enabled = False
        btnCalibrationSetPurgePos.Enabled = False
        btnCalibrationPurgeGoPos.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationPurgeSetAlign1Pos.Enabled = False
        btnCalibrationPurgeGoAlign1Pos.Enabled = False
        btnCalibrationPurgeSetAlign2Pos.Enabled = False
        btnCalibrationPurgeGoAlign2Pos.Enabled = False
        btnCalibrationPurgeSetAlign3Pos.Enabled = False
        btnCalibrationPurgeGoAlign3Pos.Enabled = False
        btnAlign.Enabled = False
        btnPurgePrev.Enabled = False
        btnPurgeNext.Enabled = False
        btnCalibrationPurgeOK.Enabled = False
        btnCalibrationPurgeCancel.Enabled = False
        UcJoyStick1.Enabled = False


        '[說明]:回Home完成才能執行
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmCalibrationPurge btnPurge", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!
            btnPurge.Enabled = True
            '20170602按鍵保護
            btnGoTilt.Enabled = True
            btnCalibrationPurgeSetCcdPos.Enabled = True
            btnCalibrationPurgeGoCCDPos.Enabled = True
            btnCalibrationSetPurgePos.Enabled = True
            btnCalibrationPurgeGoPos.Enabled = True
            btnTrainScene.Enabled = True
            btnCalibrationPurgeSetAlign1Pos.Enabled = True
            btnCalibrationPurgeGoAlign1Pos.Enabled = True
            btnCalibrationPurgeSetAlign2Pos.Enabled = True
            btnCalibrationPurgeGoAlign2Pos.Enabled = True
            btnCalibrationPurgeSetAlign3Pos.Enabled = True
            btnCalibrationPurgeGoAlign3Pos.Enabled = True
            btnAlign.Enabled = True
            btnPurgePrev.Enabled = True
            btnPurgeNext.Enabled = True
            btnCalibrationPurgeOK.Enabled = True
            btnCalibrationPurgeCancel.Enabled = True
            UcJoyStick1.Enabled = True


            Exit Sub
        End If

        '[說明]:判斷有無開啟Recipe 20161111
        'If gCRecipe.strName = "" Then
        '    MessageBox.Show("Not load Recipe!!!")
        '    gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmCalibrationPurge Purge", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
        '    btnPurge.Enabled = True
        '    '20170602按鍵保護
        '    btnGoTilt.Enabled = True
        '    btnCalibrationPurgeSetCcdPos.Enabled = True
        '    btnCalibrationPurgeGoCCDPos.Enabled = True
        '    btnCalibrationSetPurgePos.Enabled = True
        '    btnCalibrationPurgeGoPos.Enabled = True
        '    btnTrainScene.Enabled = True
        '    btnCalibrationPurgeSetAlign1Pos.Enabled = True
        '    btnCalibrationPurgeGoAlign1Pos.Enabled = True
        '    btnCalibrationPurgeSetAlign2Pos.Enabled = True
        '    btnCalibrationPurgeGoAlign2Pos.Enabled = True
        '    btnCalibrationPurgeSetAlign3Pos.Enabled = True
        '    btnCalibrationPurgeGoAlign3Pos.Enabled = True
        '    btnAlign.Enabled = True
        '    btnPurgePrev.Enabled = True
        '    btnPurgeNext.Enabled = True
        '    btnCalibrationPurgeOK.Enabled = True
        '    btnCalibrationPurgeCancel.Enabled = True
        '    UcJoyStick1.Enabled = True

        '    Exit Sub
        'End If

        If gSYS(eSys.OverAll).Act(eAct.AutoRun).RunStatus = enmRunStatus.Running Then '[說明]:只有非Running的時候才能動
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)

            btnPurge.Enabled = True
            '20170602按鍵保護
            btnGoTilt.Enabled = True
            btnCalibrationPurgeSetCcdPos.Enabled = True
            btnCalibrationPurgeGoCCDPos.Enabled = True
            btnCalibrationSetPurgePos.Enabled = True
            btnCalibrationPurgeGoPos.Enabled = True
            btnTrainScene.Enabled = True
            btnCalibrationPurgeSetAlign1Pos.Enabled = True
            btnCalibrationPurgeGoAlign1Pos.Enabled = True
            btnCalibrationPurgeSetAlign2Pos.Enabled = True
            btnCalibrationPurgeGoAlign2Pos.Enabled = True
            btnCalibrationPurgeSetAlign3Pos.Enabled = True
            btnCalibrationPurgeGoAlign3Pos.Enabled = True
            btnAlign.Enabled = True
            btnPurgePrev.Enabled = True
            btnPurgeNext.Enabled = True
            btnCalibrationPurgeOK.Enabled = True
            btnCalibrationPurgeCancel.Enabled = True
            UcJoyStick1.Enabled = True

            Exit Sub
        End If

        '[說明]:enmRunStatus.Nono, enmRunStatus.Pause, enmRunStatus.Alarm, enmRunStatus.Running
        '[說明]:判斷是否已經在做Purge
        If sys.RunStatus = enmRunStatus.Running Then '執行中
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            btnPurge.Enabled = True
            '20170602按鍵保護
            btnGoTilt.Enabled = True
            btnCalibrationPurgeSetCcdPos.Enabled = True
            btnCalibrationPurgeGoCCDPos.Enabled = True
            btnCalibrationSetPurgePos.Enabled = True
            btnCalibrationPurgeGoPos.Enabled = True
            btnTrainScene.Enabled = True
            btnCalibrationPurgeSetAlign1Pos.Enabled = True
            btnCalibrationPurgeGoAlign1Pos.Enabled = True
            btnCalibrationPurgeSetAlign2Pos.Enabled = True
            btnCalibrationPurgeGoAlign2Pos.Enabled = True
            btnCalibrationPurgeSetAlign3Pos.Enabled = True
            btnCalibrationPurgeGoAlign3Pos.Enabled = True
            btnAlign.Enabled = True
            btnPurgePrev.Enabled = True
            btnPurgeNext.Enabled = True
            btnCalibrationPurgeOK.Enabled = True
            btnCalibrationPurgeCancel.Enabled = True
            UcJoyStick1.Enabled = True

            Exit Sub
        End If

        btnPurge.BackColor = Color.Blue
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

        'gblnPurgeComeBack = True
        gblnUpdatePurge = True
    End Sub

    Private Async Sub btnAlign_Click(sender As Object, e As EventArgs) Handles btnAlign.Click 'Soni / 2017.05.16 去除DoEvents
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnAlign]" & vbTab & "Click")
        If btnAlign.Enabled = False Then '防連按
            Exit Sub
        End If

        btnAlign.Enabled = False

        '20170602按鍵保護
        btnGoTilt.Enabled = False
        btnCalibrationPurgeSetCcdPos.Enabled = False
        btnCalibrationSetPurgePos.Enabled = False
        btnCalibrationPurgeGoCCDPos.Enabled = False
        btnPurge.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationPurgeSetAlign1Pos.Enabled = False
        btnCalibrationPurgeGoPos.Enabled = False
        btnCalibrationPurgeSetAlign2Pos.Enabled = False
        btnCalibrationPurgeGoAlign1Pos.Enabled = False
        btnCalibrationPurgeSetAlign3Pos.Enabled = False
        btnCalibrationPurgeGoAlign2Pos.Enabled = False
        btnCalibrationPurgeGoAlign3Pos.Enabled = False
        btnPurgePrev.Enabled = False
        btnPurgeNext.Enabled = False
        btnCalibrationPurgeOK.Enabled = False
        btnCalibrationPurgeCancel.Enabled = False
        UcJoyStick1.Enabled = False

        '===設置場景與光源
        Dim CCDScene As String = CalibSceneName & (sys.CCDNo + 1).ToString
        gAOICollection.SetCCDScene(sys.CCDNo, CCDScene) '選擇場景
        UcLightControl1.SceneName = CCDScene
        UcLightControl1.ShowUI()


        Await Task.Run(Sub()


                           '參數設定
                           'Dim ticket As Integer = 0
                           'Dim timeOutStopWatch As New Stopwatch '逾時計時器
                           Dim centerPosX As Decimal
                           Dim centerPosY As Decimal
                           Dim AlignPos1 As New CPoint3D
                           Dim AlignPos2 As New CPoint3D
                           Dim AlignPos3 As New CPoint3D
                           AlignPos1.PointX = Val(txtCalibrationPurgeAlign1PosX.Text)
                           AlignPos1.PointY = Val(txtCalibrationPurgeAlign1PosY.Text)
                           AlignPos1.PointZ = Val(txtCalibrationPurgeAlign1PosZ.Text)
                           AlignPos2.PointX = Val(txtCalibrationPurgeAlign2PosX.Text)
                           AlignPos2.PointY = Val(txtCalibrationPurgeAlign2PosY.Text)
                           AlignPos2.PointZ = Val(txtCalibrationPurgeAlign2PosZ.Text)
                           AlignPos3.PointX = Val(txtCalibrationPurgeAlign3PosX.Text)
                           AlignPos3.PointY = Val(txtCalibrationPurgeAlign3PosY.Text)
                           AlignPos3.PointZ = Val(txtCalibrationPurgeAlign3PosZ.Text)

                           If AlignPos3FindCenterAction.Run(sys, CalibSceneName & (sys.StageNo + 1).ToString, AlignPos1, AlignPos2, AlignPos3, centerPosX, centerPosY) Then
                               'jimmy 20161130 Purge時Tilt參考水平位置 0度
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      Select Case sys.StageNo
                                                          Case 0
                                                              txtCalibrationPurgePosX.Text = centerPosX - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage1TiltAngle)
                                                              txtCalibrationPurgePosY.Text = centerPosY - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage1TiltAngle)
                                                              txtCalibrationPurgePosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                                          Case 1
                                                              txtCalibrationPurgePosX.Text = centerPosX - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage2TiltAngle)
                                                              txtCalibrationPurgePosY.Text = centerPosY - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage2TiltAngle)
                                                              txtCalibrationPurgePosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                                          Case 2
                                                              txtCalibrationPurgePosX.Text = centerPosX - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage3TiltAngle)
                                                              txtCalibrationPurgePosY.Text = centerPosY - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage3TiltAngle)
                                                              txtCalibrationPurgePosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                                          Case 3
                                                              txtCalibrationPurgePosX.Text = centerPosX - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage4TiltAngle)
                                                              txtCalibrationPurgePosY.Text = centerPosY - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage4TiltAngle)
                                                              txtCalibrationPurgePosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                                      End Select

                                                      txtCalibrationPurgeCCDPosX.Text = Val(centerPosX)
                                                      txtCalibrationPurgeCCDPosY.Text = Val(centerPosY)
                                                      txtCalibrationPurgeCCDPosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                                      '20170929 Toby_ Add 判斷
                                                      If (Not IsNothing(Me)) Then

                                                          Me.BeginInvoke(Sub()
                                                                             btnAlign.Enabled = True

                                                                             '20170602按鍵保護
                                                                             btnGoTilt.Enabled = True
                                                                             btnCalibrationPurgeSetCcdPos.Enabled = True
                                                                             btnCalibrationSetPurgePos.Enabled = True
                                                                             btnCalibrationPurgeGoCCDPos.Enabled = True
                                                                             btnPurge.Enabled = True
                                                                             btnTrainScene.Enabled = True
                                                                             btnCalibrationPurgeSetAlign1Pos.Enabled = True
                                                                             btnCalibrationPurgeGoPos.Enabled = True
                                                                             btnCalibrationPurgeSetAlign2Pos.Enabled = True
                                                                             btnCalibrationPurgeGoAlign1Pos.Enabled = True
                                                                             btnCalibrationPurgeSetAlign3Pos.Enabled = True
                                                                             btnCalibrationPurgeGoAlign2Pos.Enabled = True
                                                                             btnCalibrationPurgeGoAlign3Pos.Enabled = True
                                                                             btnPurgePrev.Enabled = True
                                                                             btnPurgeNext.Enabled = True
                                                                             btnCalibrationPurgeOK.Enabled = True
                                                                             btnCalibrationPurgeCancel.Enabled = True
                                                                             UcJoyStick1.Enabled = True



                                                                         End Sub)
                                                      End If
                                                  End Sub)
                               End If
                           Else
                               'CCD 定位失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Alarm_2036000))
                                       MsgBox(gMsgHandler.GetMessage(Alarm_2036000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Alarm_2048000))
                                       MsgBox(gMsgHandler.GetMessage(Alarm_2048000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Alarm_2066000))
                                       MsgBox(gMsgHandler.GetMessage(Alarm_2066000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Alarm_2073000))
                                       MsgBox(gMsgHandler.GetMessage(Alarm_2073000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select
                           End If
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  btnAlign.Enabled = True

                                                  '20170602按鍵保護
                                                  btnGoTilt.Enabled = True
                                                  btnCalibrationPurgeSetCcdPos.Enabled = True
                                                  btnCalibrationSetPurgePos.Enabled = True
                                                  btnCalibrationPurgeGoCCDPos.Enabled = True
                                                  btnPurge.Enabled = True
                                                  btnTrainScene.Enabled = True
                                                  btnCalibrationPurgeSetAlign1Pos.Enabled = True
                                                  btnCalibrationPurgeGoPos.Enabled = True
                                                  btnCalibrationPurgeSetAlign2Pos.Enabled = True
                                                  btnCalibrationPurgeGoAlign1Pos.Enabled = True
                                                  btnCalibrationPurgeSetAlign3Pos.Enabled = True
                                                  btnCalibrationPurgeGoAlign2Pos.Enabled = True
                                                  btnCalibrationPurgeGoAlign3Pos.Enabled = True
                                                  btnPurgePrev.Enabled = True
                                                  btnPurgeNext.Enabled = True
                                                  btnCalibrationPurgeOK.Enabled = True
                                                  btnCalibrationPurgeCancel.Enabled = True
                                                  UcJoyStick1.Enabled = True



                                              End Sub)
                           End If

                       End Sub)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按          
            UcJoyStick1.Enabled = False
        
        End If

        If btnPurge.BackColor = Color.Blue Then
            '[說明]:Auto執行按鍵保護
            Select Case sys.StageNo
                Case enmStage.No1
                    If gSYS(eSys.SubDisp1).RunStatus = enmRunStatus.Running Then
                        btnAlign.Enabled = False
                        '20170602按鍵保護
                        btnGoTilt.Enabled = False
                        btnCalibrationPurgeSetCcdPos.Enabled = False
                        btnCalibrationSetPurgePos.Enabled = False
                        btnCalibrationPurgeGoCCDPos.Enabled = False
                        btnPurge.Enabled = False
                        btnTrainScene.Enabled = False
                        btnCalibrationPurgeSetAlign1Pos.Enabled = False
                        btnCalibrationPurgeGoPos.Enabled = False
                        btnCalibrationPurgeSetAlign2Pos.Enabled = False
                        btnCalibrationPurgeGoAlign1Pos.Enabled = False
                        btnCalibrationPurgeSetAlign3Pos.Enabled = False
                        btnCalibrationPurgeGoAlign2Pos.Enabled = False
                        btnCalibrationPurgeGoAlign3Pos.Enabled = False
                        btnPurgePrev.Enabled = False
                        btnPurgeNext.Enabled = False
                        btnCalibrationPurgeOK.Enabled = False
                        btnCalibrationPurgeCancel.Enabled = False
                        UcJoyStick1.Enabled = False
                    ElseIf gSYS(eSys.SubDisp1).RunStatus = enmRunStatus.Finish Or gSYS(eSys.SubDisp1).RunStatus = enmRunStatus.Alarm Then
                        btnPurge.BackColor = SystemColors.Control
                        btnAlign.Enabled = True
                        '20170602按鍵保護
                        btnGoTilt.Enabled = True
                        btnCalibrationPurgeSetCcdPos.Enabled = True
                        btnCalibrationSetPurgePos.Enabled = True
                        btnCalibrationPurgeGoCCDPos.Enabled = True
                        btnPurge.Enabled = True
                        btnTrainScene.Enabled = True
                        btnCalibrationPurgeSetAlign1Pos.Enabled = True
                        btnCalibrationPurgeGoPos.Enabled = True
                        btnCalibrationPurgeSetAlign2Pos.Enabled = True
                        btnCalibrationPurgeGoAlign1Pos.Enabled = True
                        btnCalibrationPurgeSetAlign3Pos.Enabled = True
                        btnCalibrationPurgeGoAlign2Pos.Enabled = True
                        btnCalibrationPurgeGoAlign3Pos.Enabled = True
                        btnPurgePrev.Enabled = True
                        btnPurgeNext.Enabled = True
                        btnCalibrationPurgeOK.Enabled = True
                        btnCalibrationPurgeCancel.Enabled = True
                        UcJoyStick1.Enabled = True
                    End If
                Case enmStage.No2
                    If gSYS(eSys.SubDisp2).RunStatus = enmRunStatus.Running Then
                        btnAlign.Enabled = False
                        '20170602按鍵保護
                        btnGoTilt.Enabled = False
                        btnCalibrationPurgeSetCcdPos.Enabled = False
                        btnCalibrationSetPurgePos.Enabled = False
                        btnCalibrationPurgeGoCCDPos.Enabled = False
                        btnPurge.Enabled = False
                        btnTrainScene.Enabled = False
                        btnCalibrationPurgeSetAlign1Pos.Enabled = False
                        btnCalibrationPurgeGoPos.Enabled = False
                        btnCalibrationPurgeSetAlign2Pos.Enabled = False
                        btnCalibrationPurgeGoAlign1Pos.Enabled = False
                        btnCalibrationPurgeSetAlign3Pos.Enabled = False
                        btnCalibrationPurgeGoAlign2Pos.Enabled = False
                        btnCalibrationPurgeGoAlign3Pos.Enabled = False
                        btnPurgePrev.Enabled = False
                        btnPurgeNext.Enabled = False
                        btnCalibrationPurgeOK.Enabled = False
                        btnCalibrationPurgeCancel.Enabled = False
                        UcJoyStick1.Enabled = False
                    ElseIf gSYS(eSys.SubDisp2).RunStatus = enmRunStatus.Finish Or gSYS(eSys.SubDisp2).RunStatus = enmRunStatus.Alarm Then
                        btnPurge.BackColor = SystemColors.Control
                        btnAlign.Enabled = True
                        '20170602按鍵保護
                        btnGoTilt.Enabled = True
                        btnCalibrationPurgeSetCcdPos.Enabled = True
                        btnCalibrationSetPurgePos.Enabled = True
                        btnCalibrationPurgeGoCCDPos.Enabled = True
                        btnPurge.Enabled = True
                        btnTrainScene.Enabled = True
                        btnCalibrationPurgeSetAlign1Pos.Enabled = True
                        btnCalibrationPurgeGoPos.Enabled = True
                        btnCalibrationPurgeSetAlign2Pos.Enabled = True
                        btnCalibrationPurgeGoAlign1Pos.Enabled = True
                        btnCalibrationPurgeSetAlign3Pos.Enabled = True
                        btnCalibrationPurgeGoAlign2Pos.Enabled = True
                        btnCalibrationPurgeGoAlign3Pos.Enabled = True
                        btnPurgePrev.Enabled = True
                        btnPurgeNext.Enabled = True
                        btnCalibrationPurgeOK.Enabled = True
                        btnCalibrationPurgeCancel.Enabled = True
                        UcJoyStick1.Enabled = True
                    End If
                Case enmStage.No3
                    If gSYS(eSys.SubDisp3).RunStatus = enmRunStatus.Running Then
                        btnAlign.Enabled = False
                        '20170602按鍵保護
                        btnGoTilt.Enabled = False
                        btnCalibrationPurgeSetCcdPos.Enabled = False
                        btnCalibrationSetPurgePos.Enabled = False
                        btnCalibrationPurgeGoCCDPos.Enabled = False
                        btnPurge.Enabled = False
                        btnTrainScene.Enabled = False
                        btnCalibrationPurgeSetAlign1Pos.Enabled = False
                        btnCalibrationPurgeGoPos.Enabled = False
                        btnCalibrationPurgeSetAlign2Pos.Enabled = False
                        btnCalibrationPurgeGoAlign1Pos.Enabled = False
                        btnCalibrationPurgeSetAlign3Pos.Enabled = False
                        btnCalibrationPurgeGoAlign2Pos.Enabled = False
                        btnCalibrationPurgeGoAlign3Pos.Enabled = False
                        btnPurgePrev.Enabled = False
                        btnPurgeNext.Enabled = False
                        btnCalibrationPurgeOK.Enabled = False
                        btnCalibrationPurgeCancel.Enabled = False
                        UcJoyStick1.Enabled = False
                    ElseIf gSYS(eSys.SubDisp3).RunStatus = enmRunStatus.Finish Or gSYS(eSys.SubDisp3).RunStatus = enmRunStatus.Alarm Then
                        btnPurge.BackColor = SystemColors.Control
                        btnAlign.Enabled = True
                        '20170602按鍵保護
                        btnGoTilt.Enabled = True
                        btnCalibrationPurgeSetCcdPos.Enabled = True
                        btnCalibrationSetPurgePos.Enabled = True
                        btnCalibrationPurgeGoCCDPos.Enabled = True
                        btnPurge.Enabled = True
                        btnTrainScene.Enabled = True
                        btnCalibrationPurgeSetAlign1Pos.Enabled = True
                        btnCalibrationPurgeGoPos.Enabled = True
                        btnCalibrationPurgeSetAlign2Pos.Enabled = True
                        btnCalibrationPurgeGoAlign1Pos.Enabled = True
                        btnCalibrationPurgeSetAlign3Pos.Enabled = True
                        btnCalibrationPurgeGoAlign2Pos.Enabled = True
                        btnCalibrationPurgeGoAlign3Pos.Enabled = True
                        btnPurgePrev.Enabled = True
                        btnPurgeNext.Enabled = True
                        btnCalibrationPurgeOK.Enabled = True
                        btnCalibrationPurgeCancel.Enabled = True
                        UcJoyStick1.Enabled = True
                    End If
                Case enmStage.No4
                    If gSYS(eSys.SubDisp4).RunStatus = enmRunStatus.Running Then
                        btnAlign.Enabled = False
                        '20170602按鍵保護
                        btnGoTilt.Enabled = False
                        btnCalibrationPurgeSetCcdPos.Enabled = False
                        btnCalibrationSetPurgePos.Enabled = False
                        btnCalibrationPurgeGoCCDPos.Enabled = False
                        btnPurge.Enabled = False
                        btnTrainScene.Enabled = False
                        btnCalibrationPurgeSetAlign1Pos.Enabled = False
                        btnCalibrationPurgeGoPos.Enabled = False
                        btnCalibrationPurgeSetAlign2Pos.Enabled = False
                        btnCalibrationPurgeGoAlign1Pos.Enabled = False
                        btnCalibrationPurgeSetAlign3Pos.Enabled = False
                        btnCalibrationPurgeGoAlign2Pos.Enabled = False
                        btnCalibrationPurgeGoAlign3Pos.Enabled = False
                        btnPurgePrev.Enabled = False
                        btnPurgeNext.Enabled = False
                        btnCalibrationPurgeOK.Enabled = False
                        btnCalibrationPurgeCancel.Enabled = False
                        UcJoyStick1.Enabled = False
                    ElseIf gSYS(eSys.SubDisp4).RunStatus = enmRunStatus.Finish Or gSYS(eSys.SubDisp4).RunStatus = enmRunStatus.Alarm Then
                        btnPurge.BackColor = SystemColors.Control
                        btnAlign.Enabled = True
                        '20170602按鍵保護
                        btnGoTilt.Enabled = True
                        btnCalibrationPurgeSetCcdPos.Enabled = True
                        btnCalibrationSetPurgePos.Enabled = True
                        btnCalibrationPurgeGoCCDPos.Enabled = True
                        btnPurge.Enabled = True
                        btnTrainScene.Enabled = True
                        btnCalibrationPurgeSetAlign1Pos.Enabled = True
                        btnCalibrationPurgeGoPos.Enabled = True
                        btnCalibrationPurgeSetAlign2Pos.Enabled = True
                        btnCalibrationPurgeGoAlign1Pos.Enabled = True
                        btnCalibrationPurgeSetAlign3Pos.Enabled = True
                        btnCalibrationPurgeGoAlign2Pos.Enabled = True
                        btnCalibrationPurgeGoAlign3Pos.Enabled = True
                        btnPurgePrev.Enabled = True
                        btnPurgeNext.Enabled = True
                        btnCalibrationPurgeOK.Enabled = True
                        btnCalibrationPurgeCancel.Enabled = True
                        UcJoyStick1.Enabled = True
                    End If
            End Select
        End If

        If gSysAdapter.IsPurgeVacuumReady(sys.StageNo, sys.SelectValve) Then
            palVacuumReady.BackgroundImage = My.Resources.li_08
        Else
            palVacuumReady.BackgroundImage = My.Resources.li_23
        End If
    End Sub

    Public Function ButtonCheckMovePos(ByRef sender As Button, ByVal PosX As Decimal, ByVal PosY As Decimal, ByVal PosZ As Decimal) As Boolean
        If PosX < gCMotion.AxisParameter(sys.AxisX).Limit.NegativeLimit Or PosX > gCMotion.AxisParameter(sys.AxisX).Limit.PosivtiveLimit Or
           PosY < gCMotion.AxisParameter(sys.AxisY).Limit.NegativeLimit Or PosY > gCMotion.AxisParameter(sys.AxisY).Limit.PosivtiveLimit Or
           PosZ < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Or PosZ > gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit Then
            If Not sender Is Nothing Then
                '位置異常!軸向超出極限位置
                gSyslog.Save("[" & ClassName & "]" & vbTab & gMsgHandler.GetMessage(Warn_3000029))
                MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MsgBox("AxisParameter is Out Of Range")
                'gSyslog.Save("[" & ClassName & "]" & vbTab & "AxisParameter is Out Of Range")
                CType(sender, Button).Enabled = False
                CType(sender, Button).BackColor = Color.Red '按鍵顏色
                Return False
            End If
        End If
        If Not sender Is Nothing Then
            CType(sender, Button).Enabled = True
            CType(sender, Button).BackColor = Color.Yellow '按鍵顏色
        End If
        Return True
    End Function

    Private Sub btnCalibrationPurgeSetAlignPos_Click(sender As Object, e As EventArgs) Handles btnCalibrationPurgeSetAlign1Pos.Click
        btnCalibrationPurgeSetAlign1Pos.Enabled = False
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationPurgeSetAlign1Pos]" & vbTab & "Click")
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnCalibrationPurgeSetAlign1Pos.Enabled = True
            Exit Sub
        End If

        UcJoyStick1.RefreshPosition()
        txtCalibrationPurgeAlign1PosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtCalibrationPurgeAlign1PosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtCalibrationPurgeAlign1PosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        btnCalibrationPurgeSetAlign1Pos.Enabled = True
    End Sub

    Private Sub btnCalibrationPurgeGoAlignPos_Click(sender As Object, e As EventArgs) Handles btnCalibrationPurgeGoAlign1Pos.Click
        'Dim mlight As enmLight                                    '[光源]
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationPurgeGoAlign1Pos]" & vbTab & "Click")

        '[說明]:位置安全保護
        'If CheckGoPurgeAlignPos() = False Then
        '    Exit Sub
        'End If
        If ButtonCheckMovePos(sender, CDec(txtCalibrationPurgeAlign1PosX.Text), CDec(txtCalibrationPurgeAlign1PosY.Text), CDec(txtCalibrationPurgeAlign1PosZ.Text)) = False Then
            Exit Sub
        End If


        btnCalibrationPurgeGoAlign1Pos.Enabled = False
     
        '20170602按鍵保護
        btnGoTilt.Enabled = False
        btnCalibrationPurgeSetCcdPos.Enabled = False
        btnCalibrationSetPurgePos.Enabled = False
        btnCalibrationPurgeGoCCDPos.Enabled = False
        btnPurge.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationPurgeSetAlign1Pos.Enabled = False
        btnCalibrationPurgeGoPos.Enabled = False
        btnCalibrationPurgeSetAlign2Pos.Enabled = False
        btnCalibrationPurgeGoAlign2Pos.Enabled = False
        btnCalibrationPurgeSetAlign3Pos.Enabled = False
        btnCalibrationPurgeGoAlign3Pos.Enabled = False
        btnAlign.Enabled = False
        btnPurgePrev.Enabled = False
        btnPurgeNext.Enabled = False
        btnCalibrationPurgeOK.Enabled = False
        btnCalibrationPurgeCancel.Enabled = False
        UcJoyStick1.Enabled = False


        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        TargetPos(0) = txtCalibrationPurgeAlign1PosX.Text
        TargetPos(1) = txtCalibrationPurgeAlign1PosY.Text
        TargetPos(2) = txtCalibrationPurgeAlign1PosZ.Text
        TargetPos(3) = 0
        TargetPos(4) = 0
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, TargetPos(0), TargetPos(1), TargetPos(2)), "INFO_6019015")
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        UcJoyStick1.RefreshPosition()

        btnCalibrationPurgeGoAlign1Pos.Enabled = True

        '20170602按鍵保護
        btnGoTilt.Enabled = True
        btnCalibrationPurgeSetCcdPos.Enabled = True
        btnCalibrationSetPurgePos.Enabled = True
        btnCalibrationPurgeGoCCDPos.Enabled = True
        btnPurge.Enabled = True
        btnTrainScene.Enabled = True
        btnCalibrationPurgeSetAlign1Pos.Enabled = True
        btnCalibrationPurgeGoPos.Enabled = True
        btnCalibrationPurgeSetAlign2Pos.Enabled = True
        btnCalibrationPurgeGoAlign2Pos.Enabled = True
        btnCalibrationPurgeSetAlign3Pos.Enabled = True
        btnCalibrationPurgeGoAlign3Pos.Enabled = True
        btnAlign.Enabled = True
        btnPurgePrev.Enabled = True
        btnPurgeNext.Enabled = True
        btnCalibrationPurgeOK.Enabled = True
        btnCalibrationPurgeCancel.Enabled = True
        UcJoyStick1.Enabled = True


    End Sub

    Private Sub btnTrainScene_Click(sender As Object, e As EventArgs) Handles btnTrainScene.Click
        If gSSystemParameter.CCDAlignModuleEnable Then
            '[Note]模組化測試中----------
            Dim mfrmCogToolBlock As frmAlignModule
            If btnTrainScene.Enabled = False Then
                Exit Sub
            End If
            gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnTrainValveScene]" & vbTab & "Click")
            btnTrainScene.Enabled = False
            Dim mScene As String = ""
            If gAOICollection.SetCCDScene(sys.CCDNo, lblSceneSet.Text) Then
                mScene = lblSceneSet.Text
            Else
                '場景不存在
                gSyslog.Save(lblSceneSet.Text & gMsgHandler.GetMessage(Warn_3000020))
                MsgBox(lblSceneSet.Text & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            End If

            Try
                mfrmCogToolBlock = New frmAlignModule
                With mfrmCogToolBlock
                    .Sys = sys
                    .SceneName = mScene 'gCRecipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
                    .IsRecipeScene = False
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                End With
            Catch ex As Exception
                MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Try
            btnTrainScene.Enabled = True
            '[Note]模組化測試中----------

        Else
            Dim mfrmCogToolBlock As frmCalibAlignTool '場景不存在也要能進去
            gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnTrainScene]" & vbTab & "Click")
            btnTrainScene.Enabled = False
            Dim CCDScene As String = CalibSceneName & (sys.CCDNo + 1).ToString

            If gAOICollection.SetCCDScene(sys.CCDNo, CCDScene) = False Then

                'CCD 定位場景名稱不存在
                Select Case sys.StageNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012107))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012107), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012407))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012407), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012707))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012707), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2013007))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2013007), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select

                mfrmCogToolBlock = New frmCalibAlignTool '場景不存在也要能進去
                With mfrmCogToolBlock
                    .Sys = sys
                    .SceneName = CCDScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                End With
                btnTrainScene.Enabled = True
                Exit Sub
            End If
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
            System.Threading.Thread.CurrentThread.Join(100)
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照

            Dim stopwatch As New Stopwatch
            stopwatch.Restart()
            System.Threading.Thread.CurrentThread.Join(100)
            Do
                If stopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                    'CCD 取像TimeOut
                    Select Case sys.StageNo
                        Case 0
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012003))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2012003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 1
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012303))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2012303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 2
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012603))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2012603), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 3
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012903))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2012903), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    btnTrainScene.Enabled = True
                    Exit Sub
                End If
            Loop Until Not gAOICollection.IsCCDCBusy(sys.CCDNo) '取像完成
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉

            mfrmCogToolBlock = New frmCalibAlignTool
            With mfrmCogToolBlock
                .Sys = sys
                .SceneName = CCDScene
                .StartPosition = FormStartPosition.Manual
                .Location = New Point(0, 0)
                .ShowDialog()
            End With
            btnTrainScene.Enabled = True
        End If

    End Sub

    Private Sub btnCalibrationPurgeSetAlign2Pos_Click(sender As Object, e As EventArgs) Handles btnCalibrationPurgeSetAlign2Pos.Click
        btnCalibrationPurgeSetAlign2Pos.Enabled = False
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationPurgeSetAlign2Pos]" & vbTab & "Click")
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnCalibrationPurgeSetAlign2Pos.Enabled = True
            Exit Sub
        End If

        UcJoyStick1.RefreshPosition()
        txtCalibrationPurgeAlign2PosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtCalibrationPurgeAlign2PosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtCalibrationPurgeAlign2PosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        btnCalibrationPurgeSetAlign2Pos.Enabled = True
    End Sub

    Private Sub btnCalibrationPurgeGoAlign2Pos_Click(sender As Object, e As EventArgs) Handles btnCalibrationPurgeGoAlign2Pos.Click
        'Dim mlight As enmLight                                    '[光源]
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationPurgeGoAlign2Pos]" & vbTab & "Click")

        '[說明]:位置安全保護
        'If CheckGoPurgeAlignPos() = False Then
        '    Exit Sub
        'End If
        If ButtonCheckMovePos(sender, CDec(txtCalibrationPurgeAlign2PosX.Text), CDec(txtCalibrationPurgeAlign2PosY.Text), CDec(txtCalibrationPurgeAlign2PosZ.Text)) = False Then
            Exit Sub
        End If

        btnCalibrationPurgeGoAlign2Pos.Enabled = False

        '20170602按鍵保護
        btnGoTilt.Enabled = False
        btnCalibrationPurgeSetCcdPos.Enabled = False
        btnCalibrationSetPurgePos.Enabled = False
        btnCalibrationPurgeGoCCDPos.Enabled = False
        btnPurge.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationPurgeSetAlign1Pos.Enabled = False
        btnCalibrationPurgeGoPos.Enabled = False
        btnCalibrationPurgeSetAlign2Pos.Enabled = False
        btnCalibrationPurgeGoAlign1Pos.Enabled = False
        btnCalibrationPurgeSetAlign3Pos.Enabled = False
        btnCalibrationPurgeGoAlign3Pos.Enabled = False
        btnAlign.Enabled = False
        btnPurgePrev.Enabled = False
        btnPurgeNext.Enabled = False
        btnCalibrationPurgeOK.Enabled = False
        btnCalibrationPurgeCancel.Enabled = False
        UcJoyStick1.Enabled = False


        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        TargetPos(0) = txtCalibrationPurgeAlign2PosX.Text
        TargetPos(1) = txtCalibrationPurgeAlign2PosY.Text
        TargetPos(2) = txtCalibrationPurgeAlign2PosZ.Text
        TargetPos(3) = 0
        TargetPos(4) = 0
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, TargetPos(0), TargetPos(1), TargetPos(2)), "INFO_6019015")
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        UcJoyStick1.RefreshPosition()

        btnCalibrationPurgeGoAlign2Pos.Enabled = True

        '20170602按鍵保護
        btnGoTilt.Enabled = True
        btnCalibrationPurgeSetCcdPos.Enabled = True
        btnCalibrationSetPurgePos.Enabled = True
        btnCalibrationPurgeGoCCDPos.Enabled = True
        btnPurge.Enabled = True
        btnTrainScene.Enabled = True
        btnCalibrationPurgeSetAlign1Pos.Enabled = True
        btnCalibrationPurgeGoPos.Enabled = True
        btnCalibrationPurgeSetAlign2Pos.Enabled = True
        btnCalibrationPurgeGoAlign1Pos.Enabled = True
        btnCalibrationPurgeSetAlign3Pos.Enabled = True
        btnCalibrationPurgeGoAlign3Pos.Enabled = True
        btnAlign.Enabled = True
        btnPurgePrev.Enabled = True
        btnPurgeNext.Enabled = True
        btnCalibrationPurgeOK.Enabled = True
        btnCalibrationPurgeCancel.Enabled = True
        UcJoyStick1.Enabled = True


    End Sub

    Private Sub btnCalibrationPurgeSetAlign3Pos_Click(sender As Object, e As EventArgs) Handles btnCalibrationPurgeSetAlign3Pos.Click
        btnCalibrationPurgeSetAlign3Pos.Enabled = False
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationPurgeSetAlign3Pos]" & vbTab & "Click")
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnCalibrationPurgeSetAlign1Pos.Enabled = True
            Exit Sub
        End If

        UcJoyStick1.RefreshPosition()
        txtCalibrationPurgeAlign3PosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtCalibrationPurgeAlign3PosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtCalibrationPurgeAlign3PosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        btnCalibrationPurgeSetAlign3Pos.Enabled = True
    End Sub

    Private Sub btnCalibrationPurgeGoAlign3Pos_Click(sender As Object, e As EventArgs) Handles btnCalibrationPurgeGoAlign3Pos.Click
        'Dim mlight As enmLight                                    '[光源]
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationPurgeGoAlign3Pos]" & vbTab & "Click")

        '[說明]:位置安全保護
        'If CheckGoPurgeAlignPos() = False Then
        '    Exit Sub
        'End If
        If ButtonCheckMovePos(sender, CDec(txtCalibrationPurgeAlign3PosX.Text), CDec(txtCalibrationPurgeAlign3PosY.Text), CDec(txtCalibrationPurgeAlign3PosZ.Text)) = False Then
            Exit Sub
        End If

        btnCalibrationPurgeGoAlign3Pos.Enabled = False

        '20170602按鍵保護
        btnGoTilt.Enabled = False
        btnCalibrationPurgeSetCcdPos.Enabled = False
        btnCalibrationSetPurgePos.Enabled = False
        btnCalibrationPurgeGoCCDPos.Enabled = False
        btnPurge.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationPurgeSetAlign1Pos.Enabled = False
        btnCalibrationPurgeGoPos.Enabled = False
        btnCalibrationPurgeSetAlign2Pos.Enabled = False
        btnCalibrationPurgeGoAlign1Pos.Enabled = False
        btnCalibrationPurgeSetAlign3Pos.Enabled = False
        btnCalibrationPurgeGoAlign2Pos.Enabled = False
        btnAlign.Enabled = False
        btnPurgePrev.Enabled = False
        btnPurgeNext.Enabled = False
        btnCalibrationPurgeOK.Enabled = False
        btnCalibrationPurgeCancel.Enabled = False
        UcJoyStick1.Enabled = False


        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        TargetPos(0) = txtCalibrationPurgeAlign3PosX.Text
        TargetPos(1) = txtCalibrationPurgeAlign3PosY.Text
        TargetPos(2) = txtCalibrationPurgeAlign3PosZ.Text
        TargetPos(3) = 0
        TargetPos(4) = 0
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, TargetPos(0), TargetPos(1), TargetPos(2)), "INFO_6019015")
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        UcJoyStick1.RefreshPosition()

        btnCalibrationPurgeGoAlign3Pos.Enabled = True

        '20170602按鍵保護
        btnGoTilt.Enabled = True
        btnCalibrationPurgeSetCcdPos.Enabled = True
        btnCalibrationSetPurgePos.Enabled = True
        btnCalibrationPurgeGoCCDPos.Enabled = True
        btnPurge.Enabled = True
        btnTrainScene.Enabled = True
        btnCalibrationPurgeSetAlign1Pos.Enabled = True
        btnCalibrationPurgeGoPos.Enabled = True
        btnCalibrationPurgeSetAlign2Pos.Enabled = True
        btnCalibrationPurgeGoAlign1Pos.Enabled = True
        btnCalibrationPurgeSetAlign3Pos.Enabled = True
        btnCalibrationPurgeGoAlign2Pos.Enabled = True
        btnAlign.Enabled = True
        btnPurgePrev.Enabled = True
        btnPurgeNext.Enabled = True
        btnCalibrationPurgeOK.Enabled = True
        btnCalibrationPurgeCancel.Enabled = True
        UcJoyStick1.Enabled = True

       
    End Sub

    Private Sub nmcExposure_ValueChanged(sender As Object, e As EventArgs) Handles nmcExposure.ValueChanged
        'gAOICollection.SetExposure(sys.CCDNo, nmcExposure.Value) '設定曝光值
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照開  
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
    End Sub


    
    Private Sub btnGoTilt_Click(sender As Object, e As EventArgs) Handles btnGoTilt.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnGoCCDPos]" & vbTab & "Click")
        'Dim deg As Double = Format(CDbl(txtTiltValvePosB.Text), ".##")
        btnGoTilt.Enabled = False

        '20170602按鍵保護
        btnCalibrationPurgeSetCcdPos.Enabled = False
        btnCalibrationPurgeGoCCDPos.Enabled = False
        btnCalibrationSetPurgePos.Enabled = False
        btnCalibrationPurgeGoPos.Enabled = False
        btnPurge.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationPurgeSetAlign1Pos.Enabled = False
        btnCalibrationPurgeGoAlign1Pos.Enabled = False
        btnCalibrationPurgeSetAlign2Pos.Enabled = False
        btnCalibrationPurgeGoAlign2Pos.Enabled = False
        btnCalibrationPurgeSetAlign3Pos.Enabled = False
        btnCalibrationPurgeGoAlign3Pos.Enabled = False
        btnAlign.Enabled = False
        btnPurgePrev.Enabled = False
        btnPurgeNext.Enabled = False
        btnCalibrationPurgeOK.Enabled = False
        btnCalibrationPurgeCancel.Enabled = False
        UcJoyStick1.Enabled = False



        'With gSSystemParameter.Pos.CCDValveCalibration(sys.StageNo)
      
            Dim AxisNo(1) As Integer
            Dim TargetPos(1) As Decimal
            AxisNo(0) = sys.AxisZ
        AxisNo(1) = sys.AxisB
        TargetPos(0) = 0
       
            TargetPos(1) = CDec(Val(txtTiltValvePosB.Text))
            ButtonSafeMoveTiltPos(sender, AxisNo, TargetPos, sys)

            UcJoyStick1.RefreshPosition()
        btnGoTilt.Enabled = True

        '20170602按鍵保護
        btnCalibrationPurgeSetCcdPos.Enabled = True
        btnCalibrationPurgeGoCCDPos.Enabled = True
        btnCalibrationSetPurgePos.Enabled = True
        btnCalibrationPurgeGoPos.Enabled = True
        btnPurge.Enabled = True
        btnTrainScene.Enabled = True
        btnCalibrationPurgeSetAlign1Pos.Enabled = True
        btnCalibrationPurgeGoAlign1Pos.Enabled = True
        btnCalibrationPurgeSetAlign2Pos.Enabled = True
        btnCalibrationPurgeGoAlign2Pos.Enabled = True
        btnCalibrationPurgeSetAlign3Pos.Enabled = True
        btnCalibrationPurgeGoAlign3Pos.Enabled = True
        btnAlign.Enabled = True
        btnPurgePrev.Enabled = True
        btnPurgeNext.Enabled = True
        btnCalibrationPurgeOK.Enabled = True
        btnCalibrationPurgeCancel.Enabled = True
        UcJoyStick1.Enabled = True

    End Sub


    Private Sub btnPurgePrev_Click(sender As Object, e As EventArgs) Handles btnPurgePrev.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click.")
        Me.Close()


        If gfrmWeightPosition Is Nothing Then
            gfrmWeightPosition = New frmCalibrationFlowRate
        ElseIf gfrmWeightPosition.IsDisposed Then
            gfrmWeightPosition = New frmCalibrationFlowRate
        End If

        With gfrmWeightPosition
            .sys = gSYS(sys.EsysNum)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = True
            .ShowDialog()
        End With
    End Sub

    Private Sub btnPurgeNext_Click(sender As Object, e As EventArgs) Handles btnPurgeNext.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click.")
        Me.Close()


        If gfrmClearGlue Is Nothing Then
            gfrmClearGlue = New frmCalibrationClearGlue
        ElseIf gfrmClearGlue.IsDisposed Then
            gfrmClearGlue = New frmCalibrationClearGlue
        End If

        With gfrmClearGlue
            .sys = gSYS(sys.EsysNum)
            .StartPosition = FormStartPosition.CenterScreen
            .CalibrationEnable = True

            gfrmClearGlue.ShowDialog()
        End With
    End Sub

    Private Sub frmCalibrationPurge_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Eason 20170124 Ticket:100031 , Memory Freed [S]
        'UcDisplay1.EndLive()
        'Timer1.Enabled = False
        'UcDisplay1.ManualDispose()
        'UcLightControl1.ManualDispose()
        'UcJoyStick1.ManualDispose()
        'Me.Dispose(True)
        'Eason 20170124 Ticket:100031 , Memory Freed [E]
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub btnPurgeVacuumOnOff_Click(sender As Object, e As EventArgs) Handles btnPurgeVacuumOnOff.Click
        Static isOn As enmONOFF
        Select Case isOn
            Case enmONOFF.eOff
                gSysAdapter.SetPurgeVacuum(sys.StageNo, sys.SelectValve, enmONOFF.eON)
                isOn = enmONOFF.eON
                btnPurgeVacuumOnOff.Text = "On"
            Case enmONOFF.eON
                gSysAdapter.SetPurgeVacuum(sys.StageNo, sys.SelectValve, enmONOFF.eOff)
                isOn = enmONOFF.eOff
                btnPurgeVacuumOnOff.Text = "Off"
        End Select

    End Sub
End Class
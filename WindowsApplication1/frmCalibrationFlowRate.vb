﻿Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectAOI
Imports ProjectLaserInterferometer

Public Class frmCalibrationFlowRate

    ''' <summary>目前類別名稱</summary>
    ''' <remarks></remarks>
    Private Const ClassName As String = "frmCalibrationFlowRate"

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
    Private Sub btnCalibrationFlowRateSetCcdPos_Click(sender As Object, e As EventArgs) Handles btnCalibrationFlowRateSetCcdPos.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnFlowRateSetCcdPos]" & vbTab & "Click")
        If btnCalibrationFlowRateSetCcdPos.Enabled = False Then '防連按
            Exit Sub
        End If
        btnCalibrationFlowRateSetCcdPos.Enabled = False
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnCalibrationFlowRateSetCcdPos.Enabled = True
            Exit Sub
        End If
        UcJoyStick1.RefreshPosition()

        'jimmy 20161130 Tilt參考水平位置
        Select Case sys.StageNo
            Case 0
                txtCalibrationFlowRatePosX.Text = gCMotion.GetPositionValue(sys.AxisX) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage1TiltAngle)
                txtCalibrationFlowRatePosY.Text = gCMotion.GetPositionValue(sys.AxisY) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage1TiltAngle)
                txtCalibrationFlowRatePosZ.Text = gCMotion.GetPositionValue(sys.AxisZ) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetZ(sys.SelectValve, gSSystemParameter.Stage1TiltAngle) + 2 'CCD設定時對Z軸修正CCD對閥OffsetZ+2mm作為安全高度.給製程微調
            Case 1
                txtCalibrationFlowRatePosX.Text = gCMotion.GetPositionValue(sys.AxisX) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage2TiltAngle)
                txtCalibrationFlowRatePosY.Text = gCMotion.GetPositionValue(sys.AxisY) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage2TiltAngle)
                txtCalibrationFlowRatePosZ.Text = gCMotion.GetPositionValue(sys.AxisZ) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetZ(sys.SelectValve, gSSystemParameter.Stage2TiltAngle) + 2 'CCD設定時對Z軸修正CCD對閥OffsetZ+2mm作為安全高度.給製程微調
            Case 2
                txtCalibrationFlowRatePosX.Text = gCMotion.GetPositionValue(sys.AxisX) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage3TiltAngle)
                txtCalibrationFlowRatePosY.Text = gCMotion.GetPositionValue(sys.AxisY) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage3TiltAngle)
                txtCalibrationFlowRatePosZ.Text = gCMotion.GetPositionValue(sys.AxisZ) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetZ(sys.SelectValve, gSSystemParameter.Stage3TiltAngle) + 2 'CCD設定時對Z軸修正CCD對閥OffsetZ+2mm作為安全高度.給製程微調
            Case 3
                txtCalibrationFlowRatePosX.Text = gCMotion.GetPositionValue(sys.AxisX) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage4TiltAngle)
                txtCalibrationFlowRatePosY.Text = gCMotion.GetPositionValue(sys.AxisY) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage4TiltAngle)
                txtCalibrationFlowRatePosZ.Text = gCMotion.GetPositionValue(sys.AxisZ) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetZ(sys.SelectValve, gSSystemParameter.Stage4TiltAngle) + 2 'CCD設定時對Z軸修正CCD對閥OffsetZ+2mm作為安全高度.給製程微調
        End Select

        txtCalibrationFlowRateCCDPosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtCalibrationFlowRateCCDPosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtCalibrationFlowRateCCDPosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))

        btnCalibrationFlowRateSetCcdPos.Enabled = True
    End Sub

    Public Function CheckGoCCDPos() As Boolean
        If Val(txtCalibrationFlowRateCCDPosX.Text) < gCMotion.AxisParameter(sys.AxisX).Limit.NegativeLimit Or Val(txtCalibrationFlowRateCCDPosX.Text) > gCMotion.AxisParameter(sys.AxisX).Limit.PosivtiveLimit Or
           Val(txtCalibrationFlowRateCCDPosY.Text) < gCMotion.AxisParameter(sys.AxisY).Limit.NegativeLimit Or Val(txtCalibrationFlowRateCCDPosY.Text) > gCMotion.AxisParameter(sys.AxisY).Limit.PosivtiveLimit Or
           Val(txtCalibrationFlowRateCCDPosZ.Text) < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Or Val(txtCalibrationFlowRateCCDPosZ.Text) > gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit Then
            btnCalibrationFlowRateGoCCDPos.Enabled = False
            Return False
        Else
            btnCalibrationFlowRateGoCCDPos.Enabled = True
            Return True
        End If
    End Function

    Public Function CheckGoFlowRatePos() As Boolean
        If Val(txtCalibrationFlowRatePosX.Text) < gCMotion.AxisParameter(sys.AxisX).Limit.NegativeLimit Or Val(txtCalibrationFlowRatePosX.Text) > gCMotion.AxisParameter(sys.AxisX).Limit.PosivtiveLimit Or
           Val(txtCalibrationFlowRatePosY.Text) < gCMotion.AxisParameter(sys.AxisY).Limit.NegativeLimit Or Val(txtCalibrationFlowRatePosY.Text) > gCMotion.AxisParameter(sys.AxisY).Limit.PosivtiveLimit Or
           Val(txtCalibrationFlowRatePosZ.Text) < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Or Val(txtCalibrationFlowRatePosZ.Text) > gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit Then
            btnCalibrationFlowRateGoPos.Enabled = False
            Return False
        Else
            btnCalibrationFlowRateGoPos.Enabled = True
            Return True
        End If
    End Function


    ''' <summary>設定FlowRate位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCalibrationSetFlowRatePos_Click(sender As Object, e As EventArgs) Handles btnCalibrationSetFlowRatePos.Click
        If btnCalibrationSetFlowRatePos.Enabled = False Then '防連按
            Exit Sub
        End If
        btnCalibrationSetFlowRatePos.Enabled = False
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnSetFlowRatePos]" & vbTab & "Click")
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnCalibrationSetFlowRatePos.Enabled = True
            Exit Sub
        End If

        UcJoyStick1.RefreshPosition()
        txtCalibrationFlowRatePosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtCalibrationFlowRatePosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtCalibrationFlowRatePosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        btnCalibrationSetFlowRatePos.Enabled = True
    End Sub

    ''' <summary>關閉表單</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub frmCalibrationFlowRate_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'UcDisplay1.EndLive() 'Eason 20170124 Ticket:100031 , Memory Freed : Change to FormClosed 
        'Me.Dispose(True)'Eason 20170124 Ticket:100031 , Memory Freed : Change to FormClosed 
        'GC.Collect()'Eason 20170124 Ticket:100031 , Memory Freed : Change to FormClosed 
        'gfrmWeightPosition = Nothing

        Timer1.Enabled = False
        '20170321
        UcDisplay1.EndLive()
        UcDisplay1.ManualDispose()
        UcLightControl1.ManualDispose()
        UcJoyStick1.ManualDispose()

        Me.Dispose(True)
    End Sub
    ''' <summary>載入表單</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCalibrationFlowRate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '20170327
        Me.Text = "Flow Rate Position " & "Stage" & (sys.EsysNum - eSys.DispStage1 + 1) & " Valve" & (sys.SelectValve + 1)


        Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height

        '[Note]:使用該組Stage的第一個閥    20170321   此部分參數帶入不寫死
        ' sys.SelectValve = eValveWorkMode.Valve1

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCS_F230A, enmMachineType.DCS_350A
                grpFlowRateAlignPos.Visible = True
            Case Else
                grpFlowRateAlignPos.Visible = False
        End Select

        Select Case sys.StageNo
            Case enmStage.No1
                txtTiltValvePosB.Text = gSSystemParameter.Stage1TiltAngle.ToString

            Case enmStage.No2
                txtTiltValvePosB.Text = gSSystemParameter.Stage2TiltAngle.ToString

            Case enmStage.No3
                txtTiltValvePosB.Text = gSSystemParameter.Stage3TiltAngle.ToString

            Case enmStage.No4
                txtTiltValvePosB.Text = gSSystemParameter.Stage4TiltAngle.ToString

        End Select
        If sys.AxisB <> -1 Then
            gpTiltMove.Visible = True
        Else
            gpTiltMove.Visible = False
        End If
        lblSceneSet.Text = CalibSceneName & (sys.CCDNo + 1).ToString
        With gSSystemParameter.Pos.WeightCalibration(sys.StageNo)
            txtCalibrationFlowRateCCDPosX.Text = .CCDPosX(sys.SelectValve)
            txtCalibrationFlowRateCCDPosY.Text = .CCDPosY(sys.SelectValve)
            txtCalibrationFlowRateCCDPosZ.Text = .CCDPosZ(sys.SelectValve)
            txtCalibrationWeighingAlign1PosX.Text = .CCDAlign1PosX(sys.SelectValve)
            txtCalibrationWeighingAlign1PosY.Text = .CCDAlign1PosY(sys.SelectValve)
            txtCalibrationWeighingAlign1PosZ.Text = .CCDAlign1PosZ(sys.SelectValve)
            txtCalibrationWeighingAlign2PosX.Text = .CCDAlign2PosX(sys.SelectValve)
            txtCalibrationWeighingAlign2PosY.Text = .CCDAlign2PosY(sys.SelectValve)
            txtCalibrationWeighingAlign2PosZ.Text = .CCDAlign2PosZ(sys.SelectValve)
            txtCalibrationWeighingAlign3PosX.Text = .CCDAlign3PosX(sys.SelectValve)
            txtCalibrationWeighingAlign3PosY.Text = .CCDAlign3PosY(sys.SelectValve)
            txtCalibrationWeighingAlign3PosZ.Text = .CCDAlign3PosZ(sys.SelectValve)
            txtCalibrationFlowRatePosX.Text = .ValvePosX(sys.SelectValve)
            txtCalibrationFlowRatePosY.Text = .ValvePosY(sys.SelectValve)
            txtCalibrationFlowRatePosZ.Text = .ValvePosZ(sys.SelectValve)
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

        Select Case gAOICollection.GetCCDType(sys.CCDNo) 'gSSystemParameter.enmCCDType
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
            gEqpMsg.Add("Error_1012002", Error_1012002, eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1012002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If

        '  If CalibrationEnable = True Then
        btnFlowRatePrev.Visible = True
        btnFlowRateNext.Visible = True
        '   ElseIf CalibrationEnable = False Then
        '    btnFlowRatePrev.Visible = False
        '    btnFlowRateNext.Visible = False
        '   End If

        UcJoyStick1.RefreshPosition()

        '--- 避免當機,放最後 ---
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
    Private Sub btnCalibrationFlowRateGoCCDPos_Click(sender As Object, e As EventArgs) Handles btnCalibrationFlowRateGoCCDPos.Click
        Dim mlight As enmLight                                    '[光源]

        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnGoCCDPos]" & vbTab & "Click")

        '[說明]:位置安全保護
        If CheckGoCCDPos() = False Then
            '位置異常!軸向超出極限位置
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000029))
            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("AxisParameter is Out Of Range")
            btnCalibrationFlowRateGoCCDPos.Enabled = True

            '20170602按鍵保護
            btnCalibrationFlowRateSetCcdPos.Enabled = True
            btnGoTilt.Enabled = True
            btnCalibrationSetFlowRatePos.Enabled = True
            btnCalibrationFlowRateGoPos.Enabled = True
            btnTrainScene.Enabled = True
            btnCalibrationWeighingSetAlign1Pos.Enabled = True
            btnCalibrationWeighingGoAlign1Pos.Enabled = True
            btnCalibrationWeighingSetAlign2Pos.Enabled = True
            btnCalibrationWeighingGoAlign2Pos.Enabled = True
            btnCalibrationWeighingSetAlign3Pos.Enabled = True
            btnCalibrationWeighingGoAlign3Pos.Enabled = True
            btnALign.Enabled = True
            btnFlowRatePrev.Enabled = True
            btnFlowRateNext.Enabled = True
            btnCalibrationFlowRateOK.Enabled = True
            btnCalibrationFlowRateCancel.Enabled = True
            UcJoyStick1.Enabled = True

            Exit Sub
        End If

        btnCalibrationFlowRateGoCCDPos.Enabled = False

        '20170602按鍵保護
        btnCalibrationFlowRateSetCcdPos.Enabled = False
        btnGoTilt.Enabled = False
        btnCalibrationSetFlowRatePos.Enabled = False
        btnCalibrationFlowRateGoPos.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationWeighingSetAlign1Pos.Enabled = False
        btnCalibrationWeighingGoAlign1Pos.Enabled = False
        btnCalibrationWeighingSetAlign2Pos.Enabled = False
        btnCalibrationWeighingGoAlign2Pos.Enabled = False
        btnCalibrationWeighingSetAlign3Pos.Enabled = False
        btnCalibrationWeighingGoAlign3Pos.Enabled = False
        btnALign.Enabled = False
        btnFlowRatePrev.Enabled = False
        btnFlowRateNext.Enabled = False
        btnCalibrationFlowRateOK.Enabled = False
        btnCalibrationFlowRateCancel.Enabled = False
        UcJoyStick1.Enabled = False



        '[Note]:開啓光源
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

        TargetPos(0) = txtCalibrationFlowRateCCDPosX.Text
        TargetPos(1) = txtCalibrationFlowRateCCDPosY.Text
        TargetPos(2) = txtCalibrationFlowRateCCDPosZ.Text
        TargetPos(3) = 0
        TargetPos(4) = 0
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6001052, TargetPos(0), TargetPos(1), TargetPos(2)), "INFO_6001052")
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        UcJoyStick1.RefreshPosition()
        btnCalibrationFlowRateGoCCDPos.Enabled = True

        '20170602按鍵保護
        btnCalibrationFlowRateSetCcdPos.Enabled = True
        btnGoTilt.Enabled = True
        btnCalibrationSetFlowRatePos.Enabled = True
        btnCalibrationFlowRateGoPos.Enabled = True
        btnTrainScene.Enabled = True
        btnCalibrationWeighingSetAlign1Pos.Enabled = True
        btnCalibrationWeighingGoAlign1Pos.Enabled = True
        btnCalibrationWeighingSetAlign2Pos.Enabled = True
        btnCalibrationWeighingGoAlign2Pos.Enabled = True
        btnCalibrationWeighingSetAlign3Pos.Enabled = True
        btnCalibrationWeighingGoAlign3Pos.Enabled = True
        btnALign.Enabled = True
        btnFlowRatePrev.Enabled = True
        btnFlowRateNext.Enabled = True
        btnCalibrationFlowRateOK.Enabled = True
        btnCalibrationFlowRateCancel.Enabled = True
        UcJoyStick1.Enabled = True

    End Sub

    ''' <summary>移動到測流量位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCalibrationFlowRateGoPos_Click(sender As Object, e As EventArgs) Handles btnCalibrationFlowRateGoPos.Click
        Dim mlight As enmLight                                    '[光源]
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationFlowRateGoPos]" & vbTab & "Click")

        '[說明]:位置安全保護
        If CheckGoFlowRatePos() = False Then
            '位置異常!軸向超出極限位置
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000029))
            MsgBox(gMsgHandler.GetMessage(Warn_3000029), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("AxisParameter is Out Of Range")
            btnCalibrationFlowRateGoPos.Enabled = True

            '20170602按鍵保護
            btnCalibrationFlowRateSetCcdPos.Enabled = True
            btnGoTilt.Enabled = True
            btnCalibrationSetFlowRatePos.Enabled = True
            btnCalibrationFlowRateGoCCDPos.Enabled = True
            btnTrainScene.Enabled = True
            btnCalibrationWeighingSetAlign1Pos.Enabled = True
            btnCalibrationWeighingGoAlign1Pos.Enabled = True
            btnCalibrationWeighingSetAlign2Pos.Enabled = True
            btnCalibrationWeighingGoAlign2Pos.Enabled = True
            btnCalibrationWeighingSetAlign3Pos.Enabled = True
            btnCalibrationWeighingGoAlign3Pos.Enabled = True
            btnALign.Enabled = True
            btnFlowRatePrev.Enabled = True
            btnFlowRateNext.Enabled = True
            btnCalibrationFlowRateOK.Enabled = True
            btnCalibrationFlowRateCancel.Enabled = True
            UcJoyStick1.Enabled = True

            Exit Sub
        End If

        btnCalibrationFlowRateGoPos.Enabled = False
        '20170602按鍵保護
        btnCalibrationFlowRateSetCcdPos.Enabled = False
        btnGoTilt.Enabled = False
        btnCalibrationSetFlowRatePos.Enabled = False
        btnCalibrationFlowRateGoCCDPos.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationWeighingSetAlign1Pos.Enabled = False
        btnCalibrationWeighingGoAlign1Pos.Enabled = False
        btnCalibrationWeighingSetAlign2Pos.Enabled = False
        btnCalibrationWeighingGoAlign2Pos.Enabled = False
        btnCalibrationWeighingSetAlign3Pos.Enabled = False
        btnCalibrationWeighingGoAlign3Pos.Enabled = False
        btnALign.Enabled = False
        btnFlowRatePrev.Enabled = False
        btnFlowRateNext.Enabled = False
        btnCalibrationFlowRateOK.Enabled = False
        btnCalibrationFlowRateCancel.Enabled = False
        UcJoyStick1.Enabled = False

        '[Note]:關閉光源
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
        Call gSysAdapter.SetLightOnOff(mlight, False)
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
        Call gSysAdapter.SetLightOnOff(mlight, False)
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3)
        Call gSysAdapter.SetLightOnOff(mlight, False)
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4)
        Call gSysAdapter.SetLightOnOff(mlight, False)


        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        '20170807 先移動XY&Tilt
        TargetPos(0) = txtCalibrationFlowRatePosX.Text
        TargetPos(1) = txtCalibrationFlowRatePosY.Text
        'TargetPos(2) = txtCalibrationFlowRatePosZ.Text
        TargetPos(2) = 0
        TargetPos(3) = CDec(Val(txtTiltValvePosB.Text))
        TargetPos(4) = 0
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)

        '20170807 變更Z軸高度
        TargetPos(2) = txtCalibrationFlowRatePosZ.Text

        '20170807
        If MsgBox("Z Stage Will move down，please check PosZ:" + txtCalibrationFlowRatePosZ.Text + " is safe", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
            gSyslog.Save("[frmCalibrationFlow]" & vbTab & "[btnCalibrationFlowRateGoPos_Click]" & vbTab & "Stage in Safe Pos By User Check")
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, TargetPos(0), TargetPos(1), TargetPos(2)), "INFO_6019015")
            ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        End If

        UcJoyStick1.RefreshPosition()
        btnCalibrationFlowRateGoPos.Enabled = True
        '20170602按鍵保護
        btnCalibrationFlowRateSetCcdPos.Enabled = True
        btnGoTilt.Enabled = True
        btnCalibrationSetFlowRatePos.Enabled = True
        btnCalibrationFlowRateGoCCDPos.Enabled = True
        btnTrainScene.Enabled = True
        btnCalibrationWeighingSetAlign1Pos.Enabled = True
        btnCalibrationWeighingGoAlign1Pos.Enabled = True
        btnCalibrationWeighingSetAlign2Pos.Enabled = True
        btnCalibrationWeighingGoAlign2Pos.Enabled = True
        btnCalibrationWeighingSetAlign3Pos.Enabled = True
        btnCalibrationWeighingGoAlign3Pos.Enabled = True
        btnALign.Enabled = True
        btnFlowRatePrev.Enabled = True
        btnFlowRateNext.Enabled = True
        btnCalibrationFlowRateOK.Enabled = True
        btnCalibrationFlowRateCancel.Enabled = True
        UcJoyStick1.Enabled = True

    End Sub

    ''' <summary>放棄離開</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCalibrationFlowRateCancel_Click(sender As Object, e As EventArgs) Handles btnCalibrationFlowRateCancel.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click.")
        Timer1.Enabled = False
        '20170623 離開時Z 軸升至安全高度

        gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.WeightCalibration(sys.StageNo).SafePosZ(sys.SelectValve))



        '20171108
        If sys.SelectValve = eValveWorkMode.Valve2 Then
            ValveCylinderAction(sys.StageNo, sys.SelectValve, enmUpDown.Up) '汽缸上
        End If
        Me.Close()
    End Sub


    ''' <summary>儲存設定</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCalibrationFlowRateOK_Click(sender As Object, e As EventArgs) Handles btnCalibrationFlowRateOK.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationFlowRateOK]" & vbTab & "Click.")
        btnCalibrationFlowRateOK.Enabled = False

        With gSSystemParameter.Pos.WeightCalibration(sys.StageNo)
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
            .CCDPosX(sys.SelectValve) = CDec(txtCalibrationFlowRateCCDPosX.Text)
            .CCDPosY(sys.SelectValve) = CDec(txtCalibrationFlowRateCCDPosY.Text)
            .CCDPosZ(sys.SelectValve) = CDec(txtCalibrationFlowRateCCDPosZ.Text)
            'CCD特徵點位置
            .CCDAlign1PosX(sys.SelectValve) = CDec(txtCalibrationWeighingAlign1PosX.Text)
            .CCDAlign1PosY(sys.SelectValve) = CDec(txtCalibrationWeighingAlign1PosY.Text)
            .CCDAlign1PosZ(sys.SelectValve) = CDec(txtCalibrationWeighingAlign1PosZ.Text)
            .CCDAlign2PosX(sys.SelectValve) = CDec(txtCalibrationWeighingAlign2PosX.Text)
            .CCDAlign2PosY(sys.SelectValve) = CDec(txtCalibrationWeighingAlign2PosY.Text)
            .CCDAlign2PosZ(sys.SelectValve) = CDec(txtCalibrationWeighingAlign2PosZ.Text)
            .CCDAlign3PosX(sys.SelectValve) = CDec(txtCalibrationWeighingAlign3PosX.Text)
            .CCDAlign3PosY(sys.SelectValve) = CDec(txtCalibrationWeighingAlign3PosY.Text)
            .CCDAlign3PosZ(sys.SelectValve) = CDec(txtCalibrationWeighingAlign3PosZ.Text)
            'FlowRate校正位置
            .ValvePosX(sys.SelectValve) = CDec(txtCalibrationFlowRatePosX.Text)
            .ValvePosY(sys.SelectValve) = CDec(txtCalibrationFlowRatePosY.Text)
            .ValvePosZ(sys.SelectValve) = CDec(txtCalibrationFlowRatePosZ.Text)

            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
            gSyslog.Save("CCD Pos X:" & .CCDPosX(sys.SelectValve))
            gSyslog.Save("CCD Pos Y:" & .CCDPosY(sys.SelectValve))
            gSyslog.Save("CCD Pos Z:" & .CCDPosZ(sys.SelectValve))
            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")

            gSSystemParameter.Pos.WeightCalibration(sys.StageNo).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini") '儲存系統位置(by機型)

            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ")  Start")
            gSyslog.Save("FlowRate Pos X:" & .ValvePosX(sys.SelectValve))
            gSyslog.Save("FlowRate Pos Y:" & .ValvePosY(sys.SelectValve))
            gSyslog.Save("FlowRate Pos Z:" & .ValvePosZ(sys.SelectValve))
            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ")  End")
        End With

        '場景曝光與光源存檔
        Dim SceneName As String = LightSceneName & (sys.CCDNo + 1).ToString
        Dim fileName As String = Application.StartupPath & "\System\" & MachineName & "\" & SceneName & ".ini" '光源設定檔路徑
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

        btnCalibrationFlowRateOK.Enabled = True

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


    Private Sub txtCalibrationFlowRateCCDPosX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCalibrationFlowRateCCDPosX.KeyPress, txtCalibrationFlowRateCCDPosY.KeyPress, txtCalibrationFlowRateCCDPosZ.KeyPress, txtCalibrationFlowRatePosX.KeyPress, txtCalibrationFlowRatePosY.KeyPress, txtCalibrationFlowRatePosZ.KeyPress
        Premtek.Base.CKeyPress.CheckDecimal(sender, e)
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

    Private Sub btnCalibrationWeighingSetAlign1Pos_Click(sender As Object, e As EventArgs) Handles btnCalibrationWeighingSetAlign1Pos.Click
        btnCalibrationWeighingSetAlign1Pos.Enabled = False
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationWeighingSetAlign1Pos]" & vbTab & "Click")
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnCalibrationWeighingSetAlign1Pos.Enabled = True
            Exit Sub
        End If

        UcJoyStick1.RefreshPosition()
        txtCalibrationWeighingAlign1PosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtCalibrationWeighingAlign1PosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtCalibrationWeighingAlign1PosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        btnCalibrationWeighingSetAlign1Pos.Enabled = True
    End Sub

    Private Sub btnCalibrationWeighingGoAlign1Pos_Click(sender As Object, e As EventArgs) Handles btnCalibrationWeighingGoAlign1Pos.Click
        'Dim mlight As enmLight                                    '[光源]
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationWeighingGoAlign1Pos]" & vbTab & "Click")

        '[說明]:位置安全保護
        'If CheckGoPurgeAlignPos() = False Then
        '    Exit Sub
        'End If
        If ButtonCheckMovePos(sender, CDec(txtCalibrationWeighingAlign1PosX.Text), CDec(txtCalibrationWeighingAlign1PosY.Text), CDec(txtCalibrationWeighingAlign1PosZ.Text)) = False Then
            Exit Sub
        End If


        btnCalibrationWeighingGoAlign1Pos.Enabled = False

        '20170602按鍵保護
        btnGoTilt.Enabled = False
        btnCalibrationFlowRateSetCcdPos.Enabled = False
        btnCalibrationFlowRateGoCCDPos.Enabled = False
        btnCalibrationSetFlowRatePos.Enabled = False
        btnCalibrationFlowRateGoPos.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationWeighingSetAlign1Pos.Enabled = False
        btnCalibrationWeighingSetAlign2Pos.Enabled = False
        btnCalibrationWeighingGoAlign2Pos.Enabled = False
        btnCalibrationWeighingSetAlign3Pos.Enabled = False
        btnCalibrationWeighingGoAlign3Pos.Enabled = False
        btnALign.Enabled = False
        btnFlowRatePrev.Enabled = False
        btnFlowRateNext.Enabled = False
        btnCalibrationFlowRateOK.Enabled = False
        btnCalibrationFlowRateCancel.Enabled = False
        UcJoyStick1.Enabled = False


        '[Note]:開啟光源
        'mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
        'Call gSysAdapter.SetLightOnOff(mlight, True)
        'mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
        'Call gSysAdapter.SetLightOnOff(mlight, True)


        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        TargetPos(0) = txtCalibrationWeighingAlign1PosX.Text
        TargetPos(1) = txtCalibrationWeighingAlign1PosY.Text
        TargetPos(2) = txtCalibrationWeighingAlign1PosZ.Text
        TargetPos(3) = 0
        TargetPos(4) = 0
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, TargetPos(0), TargetPos(1), TargetPos(2)), "INFO_6019015")
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        UcJoyStick1.RefreshPosition()

        btnCalibrationWeighingGoAlign1Pos.Enabled = True

        '20170602按鍵保護
        btnGoTilt.Enabled = True
        btnCalibrationFlowRateSetCcdPos.Enabled = True
        btnCalibrationFlowRateGoCCDPos.Enabled = True
        btnCalibrationSetFlowRatePos.Enabled = True
        btnCalibrationFlowRateGoPos.Enabled = True
        btnTrainScene.Enabled = True
        btnCalibrationWeighingSetAlign1Pos.Enabled = True
        btnCalibrationWeighingSetAlign2Pos.Enabled = True
        btnCalibrationWeighingGoAlign2Pos.Enabled = True
        btnCalibrationWeighingSetAlign3Pos.Enabled = True
        btnCalibrationWeighingGoAlign3Pos.Enabled = True
        btnALign.Enabled = True
        btnFlowRatePrev.Enabled = True
        btnFlowRateNext.Enabled = True
        btnCalibrationFlowRateOK.Enabled = True
        btnCalibrationFlowRateCancel.Enabled = True
        UcJoyStick1.Enabled = True

    End Sub

    Private Sub btnCalibrationWeighingSetAlign2Pos_Click(sender As Object, e As EventArgs) Handles btnCalibrationWeighingSetAlign2Pos.Click
        btnCalibrationWeighingSetAlign2Pos.Enabled = False
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationWeighingSetAlign2Pos]" & vbTab & "Click")
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnCalibrationWeighingSetAlign2Pos.Enabled = True
            Exit Sub
        End If

        UcJoyStick1.RefreshPosition()
        txtCalibrationWeighingAlign2PosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtCalibrationWeighingAlign2PosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtCalibrationWeighingAlign2PosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        btnCalibrationWeighingSetAlign2Pos.Enabled = True
    End Sub

    Private Sub btnCalibrationWeighingGoAlign2Pos_Click(sender As Object, e As EventArgs) Handles btnCalibrationWeighingGoAlign2Pos.Click
        'Dim mlight As enmLight                                    '[光源]
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationWeighingGoAlign2Pos]" & vbTab & "Click")

        '[說明]:位置安全保護
        'If CheckGoPurgeAlignPos() = False Then
        '    Exit Sub
        'End If
        If ButtonCheckMovePos(sender, CDec(txtCalibrationWeighingAlign2PosX.Text), CDec(txtCalibrationWeighingAlign2PosY.Text), CDec(txtCalibrationWeighingAlign2PosZ.Text)) = False Then
            Exit Sub
        End If

        btnCalibrationWeighingGoAlign2Pos.Enabled = False

        '20170602按鍵保護
        btnGoTilt.Enabled = False
        btnCalibrationFlowRateSetCcdPos.Enabled = False
        btnCalibrationFlowRateGoCCDPos.Enabled = False
        btnCalibrationSetFlowRatePos.Enabled = False
        btnCalibrationFlowRateGoPos.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationWeighingSetAlign1Pos.Enabled = False
        btnCalibrationWeighingSetAlign2Pos.Enabled = False
        btnCalibrationWeighingGoAlign1Pos.Enabled = False
        btnCalibrationWeighingSetAlign3Pos.Enabled = False
        btnCalibrationWeighingGoAlign3Pos.Enabled = False
        btnALign.Enabled = False
        btnFlowRatePrev.Enabled = False
        btnFlowRateNext.Enabled = False
        btnCalibrationFlowRateOK.Enabled = False
        btnCalibrationFlowRateCancel.Enabled = False
        UcJoyStick1.Enabled = False


        '[Note]:開啟光源
        'mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
        'Call gSysAdapter.SetLightOnOff(mlight, True)
        'mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
        'Call gSysAdapter.SetLightOnOff(mlight, True)


        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        TargetPos(0) = txtCalibrationWeighingAlign2PosX.Text
        TargetPos(1) = txtCalibrationWeighingAlign2PosY.Text
        TargetPos(2) = txtCalibrationWeighingAlign2PosZ.Text
        TargetPos(3) = 0
        TargetPos(4) = 0
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, TargetPos(0), TargetPos(1), TargetPos(2)), "INFO_6019015")
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        UcJoyStick1.RefreshPosition()

        btnCalibrationWeighingGoAlign2Pos.Enabled = True

        '20170602按鍵保護
        btnGoTilt.Enabled = True
        btnCalibrationFlowRateSetCcdPos.Enabled = True
        btnCalibrationFlowRateGoCCDPos.Enabled = True
        btnCalibrationSetFlowRatePos.Enabled = True
        btnCalibrationFlowRateGoPos.Enabled = True
        btnTrainScene.Enabled = True
        btnCalibrationWeighingSetAlign1Pos.Enabled = True
        btnCalibrationWeighingSetAlign2Pos.Enabled = True
        btnCalibrationWeighingGoAlign1Pos.Enabled = True
        btnCalibrationWeighingSetAlign3Pos.Enabled = True
        btnCalibrationWeighingGoAlign3Pos.Enabled = True
        btnALign.Enabled = True
        btnFlowRatePrev.Enabled = True
        btnFlowRateNext.Enabled = True
        btnCalibrationFlowRateOK.Enabled = True
        btnCalibrationFlowRateCancel.Enabled = True
        UcJoyStick1.Enabled = True

    End Sub

    Private Sub btnCalibrationWeighingSetAlign3Pos_Click(sender As Object, e As EventArgs) Handles btnCalibrationWeighingSetAlign3Pos.Click
        btnCalibrationWeighingSetAlign3Pos.Enabled = False
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationWeighingSetAlign3Pos]" & vbTab & "Click")
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnCalibrationWeighingSetAlign3Pos.Enabled = True
            Exit Sub
        End If

        UcJoyStick1.RefreshPosition()
        txtCalibrationWeighingAlign3PosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtCalibrationWeighingAlign3PosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtCalibrationWeighingAlign3PosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        btnCalibrationWeighingSetAlign3Pos.Enabled = True
    End Sub

    Private Sub btnCalibrationWeighingGoAlign3Pos_Click(sender As Object, e As EventArgs) Handles btnCalibrationWeighingGoAlign3Pos.Click
        'Dim mlight As enmLight                                    '[光源]
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibrationWeighingGoAlign3Pos]" & vbTab & "Click")

        '[說明]:位置安全保護
        'If CheckGoPurgeAlignPos() = False Then
        '    Exit Sub
        'End If
        If ButtonCheckMovePos(sender, CDec(txtCalibrationWeighingAlign3PosX.Text), CDec(txtCalibrationWeighingAlign3PosY.Text), CDec(txtCalibrationWeighingAlign3PosZ.Text)) = False Then
            Exit Sub
        End If

        btnCalibrationWeighingGoAlign3Pos.Enabled = False

        '20170602按鍵保護
        btnGoTilt.Enabled = False
        btnCalibrationFlowRateSetCcdPos.Enabled = False
        btnCalibrationFlowRateGoCCDPos.Enabled = False
        btnCalibrationSetFlowRatePos.Enabled = False
        btnCalibrationFlowRateGoPos.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationWeighingSetAlign1Pos.Enabled = False
        btnCalibrationWeighingSetAlign2Pos.Enabled = False
        btnCalibrationWeighingGoAlign1Pos.Enabled = False
        btnCalibrationWeighingSetAlign3Pos.Enabled = False
        btnCalibrationWeighingGoAlign2Pos.Enabled = False
        btnALign.Enabled = False
        btnFlowRatePrev.Enabled = False
        btnFlowRateNext.Enabled = False
        btnCalibrationFlowRateOK.Enabled = False
        btnCalibrationFlowRateCancel.Enabled = False
        UcJoyStick1.Enabled = False

        '[Note]:開啟光源
        'mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
        'Call gSysAdapter.SetLightOnOff(mlight, True)
        'mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
        'Call gSysAdapter.SetLightOnOff(mlight, True)


        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        TargetPos(0) = txtCalibrationWeighingAlign3PosX.Text
        TargetPos(1) = txtCalibrationWeighingAlign3PosY.Text
        TargetPos(2) = txtCalibrationWeighingAlign3PosZ.Text
        TargetPos(3) = 0
        TargetPos(4) = 0
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, TargetPos(0), TargetPos(1), TargetPos(2)), "INFO_6019015")
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        UcJoyStick1.RefreshPosition()

        btnCalibrationWeighingGoAlign3Pos.Enabled = True

        '20170602按鍵保護
        btnGoTilt.Enabled = True
        btnCalibrationFlowRateSetCcdPos.Enabled = True
        btnCalibrationFlowRateGoCCDPos.Enabled = True
        btnCalibrationSetFlowRatePos.Enabled = True
        btnCalibrationFlowRateGoPos.Enabled = True
        btnTrainScene.Enabled = True
        btnCalibrationWeighingSetAlign1Pos.Enabled = True
        btnCalibrationWeighingSetAlign2Pos.Enabled = True
        btnCalibrationWeighingGoAlign1Pos.Enabled = True
        btnCalibrationWeighingSetAlign3Pos.Enabled = True
        btnCalibrationWeighingGoAlign2Pos.Enabled = True
        btnALign.Enabled = True
        btnFlowRatePrev.Enabled = True
        btnFlowRateNext.Enabled = True
        btnCalibrationFlowRateOK.Enabled = True
        btnCalibrationFlowRateCancel.Enabled = True
        UcJoyStick1.Enabled = True

    End Sub

    Private Async Sub btnALign_Click(sender As Object, e As EventArgs) Handles btnALign.Click 'Soni / 2017.05.16 去除DoEvents
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnAlign]" & vbTab & "Click")
        If btnALign.Enabled = False Then '防連按
            Exit Sub
        End If

        btnALign.Enabled = False
        '20170602按鍵保護
        btnGoTilt.Enabled = False
        btnCalibrationFlowRateSetCcdPos.Enabled = False
        btnCalibrationFlowRateGoCCDPos.Enabled = False
        btnCalibrationSetFlowRatePos.Enabled = False
        btnCalibrationFlowRateGoPos.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationWeighingSetAlign1Pos.Enabled = False
        btnCalibrationWeighingSetAlign2Pos.Enabled = False
        btnCalibrationWeighingGoAlign1Pos.Enabled = False
        btnCalibrationWeighingSetAlign3Pos.Enabled = False
        btnCalibrationWeighingGoAlign2Pos.Enabled = False
        btnCalibrationWeighingGoAlign3Pos.Enabled = False
        btnFlowRatePrev.Enabled = False
        btnFlowRateNext.Enabled = False
        btnCalibrationFlowRateOK.Enabled = False
        btnCalibrationFlowRateCancel.Enabled = False
        UcJoyStick1.Enabled = False

        UcLightControl1.SceneName = CalibSceneName & (sys.CCDNo + 1).ToString
        UcLightControl1.ShowUI()

        '參數設定
        Await Task.Run(Sub()

                           Dim centerPosX As Decimal
                           Dim centerPosY As Decimal
                           Dim AlignPos1 As New CPoint3D
                           Dim AlignPos2 As New CPoint3D
                           Dim AlignPos3 As New CPoint3D
                           AlignPos1.PointX = Val(txtCalibrationWeighingAlign1PosX.Text)
                           AlignPos1.PointY = Val(txtCalibrationWeighingAlign1PosY.Text)
                           AlignPos1.PointZ = Val(txtCalibrationWeighingAlign1PosZ.Text)
                           AlignPos2.PointX = Val(txtCalibrationWeighingAlign2PosX.Text)
                           AlignPos2.PointY = Val(txtCalibrationWeighingAlign2PosY.Text)
                           AlignPos2.PointZ = Val(txtCalibrationWeighingAlign2PosZ.Text)
                           AlignPos3.PointX = Val(txtCalibrationWeighingAlign3PosX.Text)
                           AlignPos3.PointY = Val(txtCalibrationWeighingAlign3PosY.Text)
                           AlignPos3.PointZ = Val(txtCalibrationWeighingAlign3PosZ.Text)

                           If AlignPos3FindCenterAction.Run(sys, CalibSceneName & (sys.StageNo + 1).ToString, AlignPos1, AlignPos2, AlignPos3, centerPosX, centerPosY) Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      'jimmy 20161130 Purge時Tilt參考水平位置 0度
                                                      Select Case sys.StageNo
                                                          Case 0
                                                              txtCalibrationFlowRatePosX.Text = centerPosX - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage1TiltAngle)
                                                              txtCalibrationFlowRatePosY.Text = centerPosY - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage1TiltAngle)
                                                              txtCalibrationFlowRatePosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                                          Case 1
                                                              txtCalibrationFlowRatePosX.Text = centerPosX - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage2TiltAngle)
                                                              txtCalibrationFlowRatePosY.Text = centerPosY - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage2TiltAngle)
                                                              txtCalibrationFlowRatePosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                                          Case 2
                                                              txtCalibrationFlowRatePosX.Text = centerPosX - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage3TiltAngle)
                                                              txtCalibrationFlowRatePosY.Text = centerPosY - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage3TiltAngle)
                                                              txtCalibrationFlowRatePosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                                          Case 3
                                                              txtCalibrationFlowRatePosX.Text = centerPosX - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, gSSystemParameter.Stage4TiltAngle)
                                                              txtCalibrationFlowRatePosY.Text = centerPosY - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, gSSystemParameter.Stage4TiltAngle)
                                                              txtCalibrationFlowRatePosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                                      End Select

                                                      txtCalibrationFlowRateCCDPosX.Text = Val(centerPosX)
                                                      txtCalibrationFlowRateCCDPosY.Text = Val(centerPosY)
                                                      txtCalibrationFlowRateCCDPosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                                      '20170929 Toby_ Add 判斷
                                                      If (Not IsNothing(Me)) Then
                                                          Me.BeginInvoke(Sub()
                                                                             btnALign.Enabled = True
                                                                             '20170602按鍵保護
                                                                             btnGoTilt.Enabled = True
                                                                             btnCalibrationFlowRateSetCcdPos.Enabled = True
                                                                             btnCalibrationFlowRateGoCCDPos.Enabled = True
                                                                             btnCalibrationSetFlowRatePos.Enabled = True
                                                                             btnCalibrationFlowRateGoPos.Enabled = True
                                                                             btnTrainScene.Enabled = True
                                                                             btnCalibrationWeighingSetAlign1Pos.Enabled = True
                                                                             btnCalibrationWeighingSetAlign2Pos.Enabled = True
                                                                             btnCalibrationWeighingGoAlign1Pos.Enabled = True
                                                                             btnCalibrationWeighingSetAlign3Pos.Enabled = True
                                                                             btnCalibrationWeighingGoAlign2Pos.Enabled = True
                                                                             btnCalibrationWeighingGoAlign3Pos.Enabled = True
                                                                             btnFlowRatePrev.Enabled = True
                                                                             btnFlowRateNext.Enabled = True
                                                                             btnCalibrationFlowRateOK.Enabled = True
                                                                             btnCalibrationFlowRateCancel.Enabled = True
                                                                             UcJoyStick1.Enabled = True
                                                                         End Sub)
                                                      End If
                                                  End Sub)
                               End If
                           Else
                               gSyslog.Save("[" & ClassName & "]" & vbTab & "AlignPos3FindCenterAction  ERROR")
                               MsgBox("AlignPos3FindCenterAction  ERROR", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                           End If
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  btnALign.Enabled = True
                                                  '20170602按鍵保護
                                                  btnGoTilt.Enabled = True
                                                  btnCalibrationFlowRateSetCcdPos.Enabled = True
                                                  btnCalibrationFlowRateGoCCDPos.Enabled = True
                                                  btnCalibrationSetFlowRatePos.Enabled = True
                                                  btnCalibrationFlowRateGoPos.Enabled = True
                                                  btnTrainScene.Enabled = True
                                                  btnCalibrationWeighingSetAlign1Pos.Enabled = True
                                                  btnCalibrationWeighingSetAlign2Pos.Enabled = True
                                                  btnCalibrationWeighingGoAlign1Pos.Enabled = True
                                                  btnCalibrationWeighingSetAlign3Pos.Enabled = True
                                                  btnCalibrationWeighingGoAlign2Pos.Enabled = True
                                                  btnCalibrationWeighingGoAlign3Pos.Enabled = True
                                                  btnFlowRatePrev.Enabled = True
                                                  btnFlowRateNext.Enabled = True
                                                  btnCalibrationFlowRateOK.Enabled = True
                                                  btnCalibrationFlowRateCancel.Enabled = True
                                                  UcJoyStick1.Enabled = True

                                              End Sub)
                           End If
                       End Sub)
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
            Dim mfrmCogToolBlock As frmCalibAlignTool
            gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnTrainScene]" & vbTab & "Click")
            btnTrainScene.Enabled = False
            Dim CCDScene As String = CalibSceneName & (sys.CCDNo + 1).ToString
            If gAOICollection.SetCCDScene(sys.CCDNo, CCDScene) = False Then
                '場景不存在
                gSyslog.Save(CCDScene & gMsgHandler.GetMessage(Warn_3000020))
                MsgBox(CCDScene & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MsgBox("Scene (" & CCDScene & ") Not Exists!")
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
                    'MsgBox("Acquisition is Timeout!")
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
        btnCalibrationFlowRateSetCcdPos.Enabled = False
        btnCalibrationFlowRateGoCCDPos.Enabled = False
        btnCalibrationSetFlowRatePos.Enabled = False
        btnCalibrationFlowRateGoPos.Enabled = False
        btnTrainScene.Enabled = False
        btnCalibrationWeighingSetAlign1Pos.Enabled = False
        btnCalibrationWeighingGoAlign1Pos.Enabled = False
        btnCalibrationWeighingSetAlign2Pos.Enabled = False
        btnCalibrationWeighingGoAlign2Pos.Enabled = False
        btnCalibrationWeighingSetAlign3Pos.Enabled = False
        btnCalibrationWeighingGoAlign3Pos.Enabled = False
        btnALign.Enabled = False
        btnFlowRatePrev.Enabled = False
        btnFlowRateNext.Enabled = False
        btnCalibrationFlowRateOK.Enabled = False
        btnCalibrationFlowRateCancel.Enabled = False
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
        btnCalibrationFlowRateSetCcdPos.Enabled = True
        btnCalibrationFlowRateGoCCDPos.Enabled = True
        btnCalibrationSetFlowRatePos.Enabled = True
        btnCalibrationFlowRateGoPos.Enabled = True
        btnTrainScene.Enabled = True
        btnCalibrationWeighingSetAlign1Pos.Enabled = True
        btnCalibrationWeighingGoAlign1Pos.Enabled = True
        btnCalibrationWeighingSetAlign2Pos.Enabled = True
        btnCalibrationWeighingGoAlign2Pos.Enabled = True
        btnCalibrationWeighingSetAlign3Pos.Enabled = True
        btnCalibrationWeighingGoAlign3Pos.Enabled = True
        btnALign.Enabled = True
        btnFlowRatePrev.Enabled = True
        btnFlowRateNext.Enabled = True
        btnCalibrationFlowRateOK.Enabled = True
        btnCalibrationFlowRateCancel.Enabled = True
        UcJoyStick1.Enabled = True
    End Sub



    Private Sub btnCCDToHightPrev_Click(sender As Object, e As EventArgs) Handles btnFlowRatePrev.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click.")
        Me.Close()


        If gfrmCalibrationZHeight Is Nothing Then
            gfrmCalibrationZHeight = New frmCalibrationZHeight
        ElseIf gfrmCalibrationZHeight.IsDisposed Then
            gfrmCalibrationZHeight = New frmCalibrationZHeight
        End If

        '       Dim mfrmCalibrationZHeight = New frmCalibrationZHeight

        With gfrmCalibrationZHeight
            .sys = gSYS(sys.EsysNum)
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = True
            .ShowDialog()
        End With
    End Sub

    Private Sub btnCCDToHightNext_Click(sender As Object, e As EventArgs) Handles btnFlowRateNext.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click.")
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

    Private Sub frmCalibrationFlowRate_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'Eason 20170124 Ticket:100031 , Memory Freed [S]
        'UcDisplay1.EndLive()
        'UcDisplay1.ManualDispose()
        'UcLightControl1.ManualDispose()
        'UcJoyStick1.ManualDispose()
        'Me.Dispose(True)
        'Eason 20170124 Ticket:100031 , Memory Freed [E]
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '20170810 JOG防護
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
            btnCalibrationFlowRateGoCCDPos.Enabled = False
            btnCalibrationFlowRateSetCcdPos.Enabled = False
            btnGoTilt.Enabled = False
            btnCalibrationSetFlowRatePos.Enabled = False
            btnCalibrationFlowRateGoPos.Enabled = False
            btnTrainScene.Enabled = False
            btnCalibrationWeighingSetAlign1Pos.Enabled = False
            btnCalibrationWeighingGoAlign1Pos.Enabled = False
            btnCalibrationWeighingSetAlign2Pos.Enabled = False
            btnCalibrationWeighingGoAlign2Pos.Enabled = False
            btnCalibrationWeighingSetAlign3Pos.Enabled = False
            btnCalibrationWeighingGoAlign3Pos.Enabled = False
            btnALign.Enabled = False
            UcJoyStick1.Enabled = False
        End If
    End Sub
End Class
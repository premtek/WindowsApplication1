﻿Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectAOI
Imports ProjectLaserInterferometer    '20160920
Imports ProjectTriggerBoard

Public Class frmCalibrationCCD2Valve1
    Public sys As sSysParam
    'Public CalibrationEnable As Boolean

    Public myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalibrationCCD2Valve1))

    Private Const ClassName As String = "frmCalibrationCCD2Valve1"
    Private Const CalibSceneName As String = "CALIBValve"
    Private Const LightSceneName As String = "CALIB" '[Note]預設場景光源
    Private blPrePage As Boolean = False
    Private oLastSelectCobValve As Object
    WriteOnly Property _oLastSelectCobValve() As Object
        'Get
        '    Return slastSelectCobValve
        'End Get
        Set(ByVal value As Object)
            oLastSelectCobValve = value
            blPrePage = True
        End Set
    End Property

    ''' <summary>[紀錄目前Tilt角度]</summary>
    ''' <remarks></remarks>
    Dim mTiltCommandValue As Decimal

    ''' <summary>介面更新</summary>
    ''' <remarks></remarks>
    Public Sub RefreshUI()
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
            btnAutoCalibration.Enabled = False
            btnGoCCDPos.Enabled = False
            btnGoValve1Pos.Enabled = False
            btnSetCcdPos.Enabled = False
            btnSetValvePos.Enabled = False
            UcJoyStick1.Enabled = False
        Else
            btnAutoCalibration.Enabled = True
            btnGoCCDPos.Enabled = True
            btnGoValve1Pos.Enabled = True
            btnSetCcdPos.Enabled = True
            btnSetValvePos.Enabled = True
            UcJoyStick1.Enabled = True
        End If

    End Sub

    Private Sub btnAutoCalibration_Click(sender As Object, e As EventArgs) Handles btnAutoCalibration.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnAutoCalibration]" & vbTab & "Click")
        Static mTimeoutStopWatch(enmStage.Max) As Stopwatch

        If IsNothing(mTimeoutStopWatch(sys.StageNo)) Then
            mTimeoutStopWatch(sys.StageNo) = New Stopwatch
        End If

        '[說明]:回Home完成才能執行
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            'robAutoJettingCalibration.Checked = False '按鈕上昇
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) '請先復歸.
            Exit Sub
        End If
        If gSYS(eSys.OverAll).Act(eAct.AutoRun).RunStatus = enmRunStatus.Running Then '[說明]:只有非Running的時候才能動
            'robAutoJettingCalibration.Checked = False '按鈕上昇
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000007), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) '運行中,請稍後.
            Exit Sub
        End If

        If sys.RunStatus = enmRunStatus.Running Then '運行中
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000010), "Warn_3000010", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) '自動校正中
            Exit Sub
        End If

        '[說明]:enmRunStatus.Nono, enmRunStatus.Pause, enmRunStatus.Alarm, enmRunStatus.Running
        '[說明]:判斷是否已經在做Purge
        '[說明]:判斷是否已經在做ChangeGlue
        '[說明]:判斷是否已經在做ClearGlue


        btnAutoCalibration.BackColor = Color.Blue
        Select Case sys.StageNo
            Case enmStage.No1
                gSYS(eSys.SubDisp1).SelectValve = sys.SelectValve
                gSYS(eSys.SubDisp1).RunStatus = enmRunStatus.Finish '強制接收命令
                gSYS(eSys.SubDisp1).Command = eSysCommand.CCDValveAutoCalibrationXY
            Case enmStage.No2
                gSYS(eSys.SubDisp2).SelectValve = sys.SelectValve
                gSYS(eSys.SubDisp2).RunStatus = enmRunStatus.Finish '強制接收命令
                gSYS(eSys.SubDisp2).Command = eSysCommand.CCDValveAutoCalibrationXY
            Case enmStage.No3
                gSYS(eSys.SubDisp3).SelectValve = sys.SelectValve
                gSYS(eSys.SubDisp3).RunStatus = enmRunStatus.Finish '強制接收命令
                gSYS(eSys.SubDisp3).Command = eSysCommand.CCDValveAutoCalibrationXY
            Case enmStage.No4
                gSYS(eSys.SubDisp4).SelectValve = sys.SelectValve
                gSYS(eSys.SubDisp4).RunStatus = enmRunStatus.Finish '強制接收命令
                gSYS(eSys.SubDisp4).Command = eSysCommand.CCDValveAutoCalibrationXY
        End Select


        ''[說明]:等待一下,命令進入迴圈   20161129
        'mTimeoutStopWatch(sys.StageNo).Restart()
        'Do
        '    If mTimeoutStopWatch(sys.StageNo).ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
        '        Exit Do
        '    End If
        'Loop




        'Select Case sys.StageNo
        '    Case enmStage.No1
        '        Do
        '            Application.DoEvents()
        '            If gSYS(eSys.SubDisp1).RunStatus = enmRunStatus.Alarm Then
        '                MsgBox(myResource.GetString("AutoCalibFailed SubDisp1")) ' "Auto Calbiration Failed!")
        '                Exit Sub
        '            End If
        '        Loop Until gSYS(eSys.SubDisp1).RunStatus = enmRunStatus.Finish
        '    Case enmStage.No2
        '        Do
        '            Application.DoEvents()
        '            If gSYS(eSys.SubDisp2).RunStatus = enmRunStatus.Alarm Then
        '                MsgBox(myResource.GetString("AutoCalibFailed SubDisp2")) ' "Auto Calbiration Failed!")
        '                Exit Sub
        '            End If
        '        Loop Until gSYS(eSys.SubDisp2).RunStatus = enmRunStatus.Finish
        '    Case enmStage.No3
        '        Do
        '            Application.DoEvents()
        '            If gSYS(eSys.SubDisp3).RunStatus = enmRunStatus.Alarm Then
        '                MsgBox(myResource.GetString("AutoCalibFailed SubDisp3")) ' "Auto Calbiration Failed!")
        '                Exit Sub
        '            End If
        '        Loop Until gSYS(eSys.SubDisp3).RunStatus = enmRunStatus.Finish
        '    Case enmStage.No4
        '        Do
        '            Application.DoEvents()
        '            If gSYS(eSys.SubDisp4).RunStatus = enmRunStatus.Alarm Then
        '                MsgBox(myResource.GetString("AutoCalibFailed SubDisp4")) ' "Auto Calbiration Failed!")
        '                Exit Sub
        '            End If
        '        Loop Until gSYS(eSys.SubDisp4).RunStatus = enmRunStatus.Finish
        'End Select



        'UcJoyStick1.RefreshPosition()
        ''jimmy 20161005
        'With gSSystemParameter.Pos.CCDTiltVavleCalbration(gSYS(eSys.Manual).StageNo)
        '    '20161124
        '    If sys.AxisB <> -1 Then
        '        Call GetTiltValue(gCRecipe, sys.StageNo, Val(cboB.SelectedItem))
        '        .CCDX(mAbsValveNo, Val(cboB.SelectedItem)) = .CCDX(mAbsValveNo, Val(cboB.SelectedItem)) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).AutoResultX(mAbsValveNo, Val(cboB.SelectedItem))
        '        '20161129
        '        .CCDY(mAbsValveNo, Val(cboB.SelectedItem)) = .CCDY(mAbsValveNo, Val(cboB.SelectedItem)) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).AutoResultY(mAbsValveNo, Val(cboB.SelectedItem))
        '        txtCCDPosX.Text = .CCDX(mAbsValveNo, Val(cboB.SelectedItem))
        '        txtCCDPosY.Text = .CCDY(mAbsValveNo, Val(cboB.SelectedItem))
        '        txtCCDPosZ.Text = .CCDZ(mAbsValveNo, Val(cboB.SelectedItem))
        '        MsgBox("Auto Calib OK." & vbCrLf & "OffsetX:" & gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).AutoResultX(mAbsValveNo, Val(cboB.SelectedItem)) & vbCrLf &
        '               "OffsetY: " & gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).AutoResultY(mAbsValveNo, Val(cboB.SelectedItem)))
        '    Else
        '        .CCDX(mAbsValveNo, 0) = .CCDX(mAbsValveNo, 0) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).AutoResultX(mAbsValveNo, 0)
        '        .CCDY(mAbsValveNo, 0) = .CCDY(mAbsValveNo, 0) - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).AutoResultY(mAbsValveNo, 0)
        '        txtCCDPosX.Text = .CCDX(mAbsValveNo, 0)
        '        txtCCDPosY.Text = .CCDY(mAbsValveNo, 0)
        '        txtCCDPosZ.Text = .CCDZ(mAbsValveNo, 0)
        '        MsgBox("Auto Calib OK." & vbCrLf & "OffsetX:" & gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).AutoResultX(mAbsValveNo, 0) & vbCrLf &
        '               "OffsetY: " & gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).AutoResultY(mAbsValveNo, 0))
        '    End If

        'End With


    End Sub

    Private Sub btnSetCcdPos_Click(sender As Object, e As EventArgs) Handles btnSetCcdPos.Click
        If mTiltCommandValue = -1 Then
            'Tilt 位置錯誤!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080024))
            MsgBox(gMsgHandler.GetMessage(Alarm_2080024), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("Tilt Pos Error!")
            Exit Sub
        End If
        '[Note]:Tilt Axis不用GetPositionValue，改用GetCommandValue(因位數值會跳動，數值以命令數值為準)
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnSetCcdPos]" & vbTab & "Click")
        btnSetCcdPos.Enabled = False
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetCcdPos.Enabled = True
            Exit Sub
        End If

        UcJoyStick1.RefreshPosition()
        txtCCDPosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtCCDPosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtCCDPosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        'txtTiltValvePosB.Text = Val(gCMotion.GetPositionValue(sys.AxisB))
        SaveCCDPos()
        btnSetCcdPos.Enabled = True
    End Sub

    Private Sub btnSetValvePos_Click(sender As Object, e As EventArgs) Handles btnSetValvePos.Click
        If mTiltCommandValue = -1 Then
            'Tilt 位置錯誤!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080024))
            MsgBox(gMsgHandler.GetMessage(Alarm_2080024), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("Tilt Pos Error!")
            Exit Sub
        End If
        '[Note]:Tilt Axis不用GetPositionValue，改用GetCommandValue(因位數值會跳動，數值以命令數值為準)

        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnSetValvePos]" & vbTab & "Click")
        btnSetValvePos.Enabled = False
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetValvePos.Enabled = True
            Exit Sub
        End If

        UcJoyStick1.RefreshPosition()
        txtValvePosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtValvePosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtValvePosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        'txtTiltValvePosB.Text = Val(gCMotion.GetPositionValue(sys.AxisB))
        '[Note]:Tilt Axis不用GetPositionValue，改用GetCommandValue(因位數值會跳動，數值以命令數值為準)

        SaveTiltValvePinPos()
        btnSetValvePos.Enabled = True
    End Sub


    Private Sub frmCalibrationCCD2Valve1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        '20170215
        'Eason 20170120 Ticket:100030 , Memory Freed [S]
        'UcDisplay1.EndLive()
        'Timer1.Enabled = False
        'UcDisplay1.ManualDispose()
        'UcLightControl1.ManualDispose()
        'UcJoyStick1.ManualDispose()
        'Me.Dispose(True)
        'GC.Collect() 'Eason 20170120 Ticket:100030 , Memory Freed , Be careful Use it .


        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)


        'UcDisplay1.EndLive()
        'Timer1.Enabled = False
        'Me.Dispose(True)
        'Eason 20170120 Ticket:100030 , Memory Freed [E]
    End Sub

    Private Sub frmCalibrationCCD2Valve1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        blPrePage = False
        UcDisplay1.EndLive()
        Timer1.Enabled = False
        UcDisplay1.ManualDispose()
        UcLightControl1.ManualDispose()
        UcJoyStick1.ManualDispose()
        Me.Dispose(True)

        '20170630
        '[說明]:如有Recipe DB ReLoad回來
        If gCRecipe.strName <> "" Then
            Dim DefaultDirectory As String = Application.StartupPath & "\Recipe\"
            DefaultDirectory = Application.StartupPath & "\Recipe\" & gCRecipe.strName
            'Recipe讀檔
            gCRecipe.LoadStageParts(DefaultDirectory) '檔案讀取
        End If

        GC.Collect() 'Eason 20170120 Ticket:100030 , Memory Freed , Be careful Use it .

        'UcDisplay1.EndLive()
        'Timer1.Enabled = False
        'UcLightControl1.MyDispose()
        'UcDisplay1.MyDispose()
        'UcJoyStick1.MyDispose()
        'UcStatusBar1.MyDispose()
        'Me.Dispose(True)
        ''  GC.SuppressFinalize(Me)

        'GC.Collect()
    End Sub

    Private Async Sub frmCalibrationCCD2Valve1_Load(sender As Object, e As EventArgs) Handles Me.Load
        '20170327
        Me.Text = "Calibration" & " Stage" & (sys.EsysNum - eSys.DispStage1 + 1) & " CCD & Valve" & sys.SelectValve + 1


        '=== 介面大小自動調整 ===
        Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height

        '[Note]:使用該Stage的第一組閥  20170321   此部分參數帶入不寫死
        'sys.SelectValve = eValveWorkMode.Valve1

        gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini")

        gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) 'Soni + 2017.04.27 第二閥氣缸下降前, Z先上升


        '[Note]:先將所有的資料列出來，預設是使用第一組設定值
        UpdateComboxB()

        '上一頁按回來
        If blPrePage = True Then
            cboB.SelectedIndex = cboB.Items.IndexOf(oLastSelectCobValve)
            mTiltCommandValue = cboB.SelectedItem
        Else
            cboB.SelectedIndex = 0
            mTiltCommandValue = cboB.SelectedItem
        End If


        With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
            txtCount.Text = .iCCDValveCount(sys.SelectValve, mTiltCommandValue)
            txtPitch.Text = .decPitch(sys.SelectValve, mTiltCommandValue)
            txtAirPressure.Text = .decAirPressure(sys.SelectValve, mTiltCommandValue)
            '20170520
            txtCCDStableTime.Text = .decCCDStableTime(sys.SelectValve, mTiltCommandValue)
        End With

        If sys.AxisB <> -1 Then
            lbDegree.Visible = True
            cboB.Visible = True
            btnRemoveKey.Visible = True
            gbTiltSet.Visible = True
            lbConvertTiltAngle.Visible = True
        Else
            lbDegree.Visible = False
            cboB.Visible = False
            btnRemoveKey.Visible = False
            gbTiltSet.Visible = False
            lbConvertTiltAngle.Visible = False
        End If
        'Toby Marked_0927 在復歸時就應該設定好
        ''jimmy 20170823
        'If gCMotion.SetNELEnable(sys.AxisX, True) <> CommandStatus.Sucessed Then
        '    MessageBox.Show("GpSetSwMelEnable Fail")
        'End If
        'If gCMotion.SetPELEnable(sys.AxisX, True) <> CommandStatus.Sucessed Then
        '    MessageBox.Show("GpSetSwPelEnable Fail")
        'End If
        'If gCMotion.SetSNEL(sys.AxisX, gCMotion.AxisParameter(sys.AxisX).Limit.NegativeLimit) <> CommandStatus.Sucessed Then
        '    MessageBox.Show("GpSetMelValve Fail")
        'End If
        'If gCMotion.SetSPEL(sys.AxisX, gCMotion.AxisParameter(sys.AxisX).Limit.PosivtiveLimit) <> CommandStatus.Sucessed Then
        '    MessageBox.Show("GpSetPelValve Fail")
        'End If
        'If gCMotion.SetSPELReact(sys.AxisX, False) <> CommandStatus.Sucessed Then
        '    MessageBox.Show("GpSetPelReact Fail")
        'End If
        'If gCMotion.SetSNELReact(sys.AxisX, False) <> CommandStatus.Sucessed Then
        '    MessageBox.Show("GpSetMelReact Fail")
        'End If

        '--- Joytick 配置 ---
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

        '--- CCD顯示設定 ---
        Select Case gAOICollection.GetCCDType(sys.CCDNo) 'gSSystemParameter.enmCCDType
            Case enmCCDType.CognexVPRO
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
            Case enmCCDType.OmronFZS2MUDP
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
            Case Else
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
        End Select

        'Dim fileName As String
        'fileName = Application.StartupPath & "\System\" & MachineName & "\" & CalibSceneName & (sys.CCDNo + 1).ToString & ".ini" '光源設定檔路徑
        'gAOICollection.LoadSceneParameter(CalibSceneName & (sys.CCDNo + 1).ToString, fileName) '讀取光源,曝光值等設定
        'If gAOICollection.SceneDictionary.ContainsKey(CalibSceneName & (sys.CCDNo + 1).ToString) Then
        '    nmcExposure.Value = gAOICollection.SceneDictionary(CalibSceneName & (sys.CCDNo + 1).ToString).CCDExposureTime
        'Else
        '    nmcExposure.Value = 5
        'End If
        lblSceneSet.Text = CalibSceneName & (sys.CCDNo + 1).ToString
        '20161124
        gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decCCDValveCalibrationSceneName(sys.SelectValve, Val(cboB.SelectedItem)) = lblSceneSet.Text

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
        'System.Threading.Thread.CurrentThread.Join(10)
        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照
        'System.Threading.Thread.CurrentThread.Join(10)
        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        ''--- 2016.06.18 + 開燈,曝光,觸發後才會改 ---

        '--- CCD顯示設定 ---
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

        '--- 避免當機,放最後 ---
        Timer1.Enabled = True
        Call Timer1_Tick(sender, e)
        RefreshUI()

        'If CalibrationEnable = True Then
        ' btnCCDToValveNext.Visible = True
        ' ElseIf CalibrationEnable = False Then
        ' btnCCDToValveNext.Visible = False
        ' End If

        Await Task.Run(Sub()
                           System.Threading.Thread.Sleep(300)
                           Do
                           Loop Until gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed
                       End Sub)
        ValveCylinderAction(sys.StageNo, sys.SelectValve, enmUpDown.Down) 'Soni + 2017.04.27 氣缸下

        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)

    End Sub

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

    Private Sub btnPrevPage_Click(sender As Object, e As EventArgs)
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnPrevPage]" & vbTab & "Click (Exit Calibration.)")

        Me.Close()
    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs)
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnNextPage]" & vbTab & "Click(->Valve2)")

        'jimmy 20161013
        '--- 參數儲存 ---
        With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
            gSyslog.Save("CCD Pos X:" & .CCDX(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("CCD Pos Y:" & .CCDY(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("CCD Pos Z:" & .CCDZ(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")

            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
            gSyslog.Save("Valve Pos X:" & .ValveX(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("Valve Pos Y:" & .ValveY(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("Valve Pos Z:" & .ValveZ(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")

            .CCDX(sys.SelectValve, Val(cboB.SelectedItem)) = Val(txtCCDPosX.Text)
            .CCDY(sys.SelectValve, Val(cboB.SelectedItem)) = Val(txtCCDPosY.Text)
            .CCDZ(sys.SelectValve, Val(cboB.SelectedItem)) = Val(txtCCDPosZ.Text)
            .ValveX(sys.SelectValve, Val(cboB.SelectedItem)) = Val(txtValvePosX.Text)
            .ValveY(sys.SelectValve, Val(cboB.SelectedItem)) = Val(txtValvePosY.Text)
            .ValveZ(sys.SelectValve, Val(cboB.SelectedItem)) = Val(txtValvePosZ.Text)

            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
            gSyslog.Save("CCD Pos X:" & .CCDX(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("CCD Pos Y:" & .CCDY(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("CCD Pos Z:" & .CCDZ(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")

            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
            gSyslog.Save("Valve Pos X:" & .ValveX(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("Valve Pos Y:" & .ValveY(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("Valve Pos Z:" & .ValveZ(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")

            '存檔成功 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        End With
        '--- 參數儲存 ---
        '--- 下一頁顯示 ---
        If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then '單平台雙閥, 需校正第二組
            gfrmCalibrationCCD2Valve2 = New frmCalibrationCCD2Valve2
            With gfrmCalibrationCCD2Valve2
                .sys = sys
                .WindowState = FormWindowState.Maximized
                .StartPosition = FormStartPosition.Manual
                .Location = New Point(0, 0)
                .Show()
                .BringToFront()
            End With
        End If

        Me.Close()
    End Sub

    Private Sub btnGoCCDPos_Click(sender As Object, e As EventArgs) Handles btnGoCCDPos.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnGoCCDPos]" & vbTab & "Click")
        btnGoCCDPos.Enabled = False

        '20170602按鍵保護
        btnSetCcdPos.Enabled = False
        btnSetValvePos.Enabled = False
        btnGoValve1Pos.Enabled = False
        btnGetLaser.Enabled = False
        btnSetTiltPos.Enabled = False
        btnGoTilt.Enabled = False
        btnTrainValveScene.Enabled = False
        btnALign.Enabled = False
        btnRemoveKey.Enabled = False
        btnAutoCalibration.Enabled = False
        btnCCDToValveNext.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        UcJoyStick1.Enabled = False


        With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015,
                                                .CCDX(sys.SelectValve, Val(cboB.SelectedItem)),
                                                .CCDY(sys.SelectValve, Val(cboB.SelectedItem)),
                                                .CCDY(sys.SelectValve, Val(cboB.SelectedItem)),
                                                "INFO_6019015"))
            Dim AxisNo(4) As Integer
            Dim TargetPos(4) As Decimal
            AxisNo(0) = sys.AxisX
            AxisNo(1) = sys.AxisY
            AxisNo(2) = sys.AxisZ
            AxisNo(3) = sys.AxisB '沒有也要放-1 來忽略動作
            AxisNo(4) = sys.AxisC

            TargetPos(0) = Val(txtCCDPosX.Text)
            TargetPos(1) = Val(txtCCDPosY.Text)
            TargetPos(2) = Val(txtCCDPosZ.Text)
            TargetPos(3) = mTiltCommandValue
            TargetPos(4) = 0
            ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        End With
        UcJoyStick1.RefreshPosition()
        btnGoCCDPos.Enabled = True

        '20170602按鍵保護
        btnSetCcdPos.Enabled = True
        btnSetValvePos.Enabled = True
        btnGoValve1Pos.Enabled = True
        btnGetLaser.Enabled = True
        btnSetTiltPos.Enabled = True
        btnGoTilt.Enabled = True
        btnTrainValveScene.Enabled = True
        btnALign.Enabled = True
        btnRemoveKey.Enabled = True
        btnAutoCalibration.Enabled = True
        btnCCDToValveNext.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True


    End Sub

    Private Sub btnGoValve1Pos_Click(sender As Object, e As EventArgs) Handles btnGoValve1Pos.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnGoValve1Pos]" & vbTab & "Click")
        btnGoValve1Pos.Enabled = False

        '20170602按鍵保護
        btnSetCcdPos.Enabled = False
        btnSetValvePos.Enabled = False
        btnGoCCDPos.Enabled = False
        btnGetLaser.Enabled = False
        btnSetTiltPos.Enabled = False
        btnGoTilt.Enabled = False
        btnTrainValveScene.Enabled = False
        btnALign.Enabled = False
        btnRemoveKey.Enabled = False
        btnAutoCalibration.Enabled = False
        btnCCDToValveNext.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        UcJoyStick1.Enabled = False

        With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015,
                                            .ValveX(sys.SelectValve, Val(cboB.SelectedItem)),
                                            .ValveY(sys.SelectValve, Val(cboB.SelectedItem)),
                                            .ValveZ(sys.SelectValve, Val(cboB.SelectedItem)),
                                            "INFO_6019015"))
            Dim AxisNo(4) As Integer
            Dim TargetPos(4) As Decimal
            AxisNo(0) = sys.AxisX
            AxisNo(1) = sys.AxisY
            AxisNo(2) = sys.AxisZ
            AxisNo(3) = sys.AxisB
            AxisNo(4) = sys.AxisC
            '20170807 先移動xy& Tilt
            TargetPos(0) = Val(txtValvePosX.Text)
            TargetPos(1) = Val(txtValvePosY.Text)
            TargetPos(2) = 0
            'TargetPos(2) = Val(txtValvePosZ.Text)
            TargetPos(3) = mTiltCommandValue
            TargetPos(4) = 0
            ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)

            '20170807 更改Z軸位置
            TargetPos(2) = Val(txtValvePosZ.Text)

            '20170807
            If MsgBox("Z Stage Will move down，please check PosZ:" + txtValvePosZ.Text + " is safe", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                gSyslog.Save("[frmCalibrationCCD2Valve1]" & vbTab & "[btnGoValve1Pos_Click]" & vbTab & "Stage in Safe Pos By User Check")
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, TargetPos(0), TargetPos(1), TargetPos(2)), "INFO_6019015")
                ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
            End If

        End With
        UcJoyStick1.RefreshPosition()

        btnGoValve1Pos.Enabled = True
        '20170602按鍵保護
        btnSetCcdPos.Enabled = True
        btnSetValvePos.Enabled = True
        btnGoCCDPos.Enabled = True
        btnGetLaser.Enabled = True
        btnSetTiltPos.Enabled = True
        btnGoTilt.Enabled = True
        btnTrainValveScene.Enabled = True
        btnALign.Enabled = True
        btnRemoveKey.Enabled = True
        btnAutoCalibration.Enabled = True
        btnCCDToValveNext.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True


    End Sub

    ''' <summary>
    ''' 單點觸發
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnValve1On_Click(sender As Object, e As EventArgs) Handles btnValve1On.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnValve1On]" & vbTab & "Click")
        btnValve1On.Enabled = False
        btnValve1On.BackColor = Color.Yellow
        btnValve1On.Refresh() 'Soni / 2017.05.10
        Call SetDispensingTrigger(sys.StageNo, eValveWorkMode.Valve1, enmONOFF.eON)
        System.Threading.Thread.Sleep(10)
        Call SetDispensingTrigger(sys.StageNo, eValveWorkMode.Valve1, enmONOFF.eOff)
        btnValve1On.BackColor = SystemColors.Control
        btnValve1On.Refresh() 'Soni / 2017.05.10
        btnValve1On.Enabled = True
    End Sub

    ''' <summary>設定膠管氣壓</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSetSyringePressure_Click(sender As Object, e As EventArgs) Handles btnSetSyringePressure.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnSetSyringePressure]" & vbTab & "Click")
        '[說明]:紀錄按了哪些按鈕
        btnSetSyringePressure.Enabled = False
        '[說明]:AO_人機設定值-1號膠槍電空閥壓力設定
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6001010, txtSyringePressure.Text), "INFO_6001010")
        'gEPVCollection.SetValue(enmEPV.No1, Val(txtSyringePressure.Text), True)
        gEPVCollection.SetValue(eEPVPressureType.Syringe, enmEPV.No1, Val(txtSyringePressure.Text), True)

        'If IsNumeric(txtSyringePressure.Text) = True Then
        '    gAOCollection.Value(enmAO.DispenserNo1EPRegulator) = txtSyringePressure.Text
        '    txtSyringePressure.BackColor = Color.White
        'Else
        '    txtSyringePressure.BackColor = Color.Red
        'End If
        btnSetSyringePressure.Enabled = True
    End Sub

    Public Function GetString(ByVal value As String) As String
        Select Case value
            Case "Syringe Pressure On"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Syringe Pressure On"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "注射器压力开"
                    Case enmLanguageType.eTraditionalChinese
                        Return "膠管氣壓開"
                End Select
            Case "Syringe Pressure Off"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Syringe Pressure Off"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "注射器压力关"
                    Case enmLanguageType.eTraditionalChinese
                        Return "膠管氣壓關"
                End Select
            Case "On"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "On"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "开"
                    Case enmLanguageType.eTraditionalChinese
                        Return "開"
                End Select
            Case "Off"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Off"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "关"
                    Case enmLanguageType.eTraditionalChinese
                        Return "關"
                End Select
        End Select
        Return "Undefined."
    End Function
    ''' <summary>
    ''' 閥膠管氣壓開關
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnValve1SyringePressureOnOff_Click(sender As Object, e As EventArgs) Handles btnValve1SyringePressureOnOff.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnValve1SyringePressureOnOff]" & vbTab & "Click")
        btnValve1SyringePressureOnOff.Enabled = False
        '[說明]:DO-1號膠桶正壓
        gDOCollection.SetState(enmDO.SyringePressure1, Not gDOCollection.GetState(enmDO.SyringePressure1))
        If gSSystemParameter.RunMode = enmRunMode.Run Then
            Call gDOCollection.RefreshDO() '寫入卡片 DO點
        End If

        If gDOCollection.GetState(enmDO.SyringePressure1) Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6001014), "INFO_6001014")
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    btnValve1SyringePressureOnOff.Text = GetString("Syringe Pressure On")
                Case enmLanguageType.eSimplifiedChinese
                    btnValve1SyringePressureOnOff.Text = GetString("阀胶管气压-开启")
                Case enmLanguageType.eTraditionalChinese
                    btnValve1SyringePressureOnOff.Text = GetString("閥膠管氣壓-開啟")
            End Select
            'btnValve1SyringePressureOnOff.Text = GetString("Syringe Pressure On")
            btnValve1SyringePressureOnOff.BackColor = Color.Yellow
        Else
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6001015), "INFO_6001015")
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    btnValve1SyringePressureOnOff.Text = GetString("Syringe Pressure Off")
                Case enmLanguageType.eSimplifiedChinese
                    btnValve1SyringePressureOnOff.Text = GetString("阀胶管气压-关闭")
                Case enmLanguageType.eTraditionalChinese
                    btnValve1SyringePressureOnOff.Text = GetString("閥膠管氣壓-關閉")
            End Select
            'btnValve1SyringePressureOnOff.Text = GetString("Syringe Pressure Off")
            btnValve1SyringePressureOnOff.BackColor = SystemColors.Control
        End If
        btnValve1SyringePressureOnOff.Enabled = True
    End Sub

    Private Sub txtCount_KeyPress(sender As Object, e As KeyPressEventArgs)
        Premtek.Base.CKeyPress.CheckUInteger(sender, e)
    End Sub

    Private Sub txtPitch_KeyPress(sender As Object, e As KeyPressEventArgs)
        Premtek.Base.CKeyPress.CheckDecimal(sender, e)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Not sys Is Nothing Then
            If sys.AxisZ <> -1 Then
                If gCMotion.AxisParameter(sys.AxisZ).MotionIOStatus.blnLTC = True Then '手動判定LTC是否正常
                    lblLTC.BackColor = SystemColors.Control
                Else
                    lblLTC.BackColor = Color.Red
                End If
            End If
        End If
        RefreshUI()

        If btnAutoCalibration.BackColor = Color.Blue Then
            '[說明]:   Auto執行按鍵保護()
            Select Case sys.StageNo
                Case enmStage.No1
                    If gSYS(eSys.SubDisp1).RunStatus = enmRunStatus.Running Then
                        btnSetCcdPos.Enabled = False
                        btnGoCCDPos.Enabled = False
                        btnSetValvePos.Enabled = False
                        btnGoValve1Pos.Enabled = False
                        btnGetLaser.Enabled = False
                        btnSetTiltPos.Enabled = False
                        btnGoTilt.Enabled = False
                        btnTrainValveScene.Enabled = False
                        btnALign.Enabled = False
                        btnRemoveKey.Enabled = False
                        btnAutoCalibration.Enabled = False
                        btnCCDToValveNext.Enabled = False
                        btnOK.Enabled = False
                        btnCancel.Enabled = False
                        UcJoyStick1.Enabled = False
                    ElseIf gSYS(eSys.SubDisp1).RunStatus = enmRunStatus.Finish Or gSYS(eSys.SubDisp1).RunStatus = enmRunStatus.Alarm Then
                        btnAutoCalibration.BackColor = SystemColors.Control
                        btnSetCcdPos.Enabled = True
                        btnGoCCDPos.Enabled = True
                        btnSetValvePos.Enabled = True
                        btnGoValve1Pos.Enabled = True
                        btnGetLaser.Enabled = True
                        btnSetTiltPos.Enabled = True
                        btnGoTilt.Enabled = True
                        btnTrainValveScene.Enabled = True
                        btnALign.Enabled = True
                        btnRemoveKey.Enabled = True
                        btnAutoCalibration.Enabled = True
                        btnCCDToValveNext.Enabled = True
                        btnOK.Enabled = True
                        btnCancel.Enabled = True
                        UcJoyStick1.Enabled = True
                    End If
                Case enmStage.No2
                    If gSYS(eSys.SubDisp2).RunStatus = enmRunStatus.Running Then
                        btnSetCcdPos.Enabled = False
                        btnGoCCDPos.Enabled = False
                        btnSetValvePos.Enabled = False
                        btnGoValve1Pos.Enabled = False
                        btnGetLaser.Enabled = False
                        btnSetTiltPos.Enabled = False
                        btnGoTilt.Enabled = False
                        btnTrainValveScene.Enabled = False
                        btnALign.Enabled = False
                        btnRemoveKey.Enabled = False
                        btnAutoCalibration.Enabled = False
                        btnCCDToValveNext.Enabled = False
                        btnOK.Enabled = False
                        btnCancel.Enabled = False
                        UcJoyStick1.Enabled = False
                    ElseIf gSYS(eSys.SubDisp2).RunStatus = enmRunStatus.Finish Or gSYS(eSys.SubDisp2).RunStatus = enmRunStatus.Alarm Then
                        btnAutoCalibration.BackColor = SystemColors.Control
                        btnSetCcdPos.Enabled = True
                        btnGoCCDPos.Enabled = True
                        btnSetValvePos.Enabled = True
                        btnGoValve1Pos.Enabled = True
                        btnGetLaser.Enabled = True
                        btnSetTiltPos.Enabled = True
                        btnGoTilt.Enabled = True
                        btnTrainValveScene.Enabled = True
                        btnALign.Enabled = True
                        btnRemoveKey.Enabled = True
                        btnAutoCalibration.Enabled = True
                        btnCCDToValveNext.Enabled = True
                        btnOK.Enabled = True
                        btnCancel.Enabled = True
                        UcJoyStick1.Enabled = True
                    End If
                Case enmStage.No3
                    If gSYS(eSys.SubDisp3).RunStatus = enmRunStatus.Running Then
                        btnSetCcdPos.Enabled = False
                        btnGoCCDPos.Enabled = False
                        btnSetValvePos.Enabled = False
                        btnGoValve1Pos.Enabled = False
                        btnGetLaser.Enabled = False
                        btnSetTiltPos.Enabled = False
                        btnGoTilt.Enabled = False
                        btnTrainValveScene.Enabled = False
                        btnALign.Enabled = False
                        btnRemoveKey.Enabled = False
                        btnAutoCalibration.Enabled = False
                        btnCCDToValveNext.Enabled = False
                        btnOK.Enabled = False
                        btnCancel.Enabled = False
                        UcJoyStick1.Enabled = False
                    ElseIf gSYS(eSys.SubDisp3).RunStatus = enmRunStatus.Finish Or gSYS(eSys.SubDisp3).RunStatus = enmRunStatus.Alarm Then
                        btnAutoCalibration.BackColor = SystemColors.Control
                        btnSetCcdPos.Enabled = True
                        btnGoCCDPos.Enabled = True
                        btnSetValvePos.Enabled = True
                        btnGoValve1Pos.Enabled = True
                        btnGetLaser.Enabled = True
                        btnSetTiltPos.Enabled = True
                        btnGoTilt.Enabled = True
                        btnTrainValveScene.Enabled = True
                        btnALign.Enabled = True
                        btnRemoveKey.Enabled = True
                        btnAutoCalibration.Enabled = True
                        btnCCDToValveNext.Enabled = True
                        btnOK.Enabled = True
                        btnCancel.Enabled = True
                        UcJoyStick1.Enabled = True
                    End If
                Case enmStage.No4
                    If gSYS(eSys.SubDisp4).RunStatus = enmRunStatus.Running Then
                        btnSetCcdPos.Enabled = False
                        btnGoCCDPos.Enabled = False
                        btnSetValvePos.Enabled = False
                        btnGoValve1Pos.Enabled = False
                        btnGetLaser.Enabled = False
                        btnSetTiltPos.Enabled = False
                        btnGoTilt.Enabled = False
                        btnTrainValveScene.Enabled = False
                        btnALign.Enabled = False
                        btnRemoveKey.Enabled = False
                        btnAutoCalibration.Enabled = False
                        btnCCDToValveNext.Enabled = False
                        btnOK.Enabled = False
                        btnCancel.Enabled = False
                        UcJoyStick1.Enabled = False
                    ElseIf gSYS(eSys.SubDisp4).RunStatus = enmRunStatus.Finish Or gSYS(eSys.SubDisp4).RunStatus = enmRunStatus.Alarm Then
                        btnAutoCalibration.BackColor = SystemColors.Control
                        btnSetCcdPos.Enabled = True
                        btnGoCCDPos.Enabled = True
                        btnSetValvePos.Enabled = True
                        btnGoValve1Pos.Enabled = True
                        btnGetLaser.Enabled = True
                        btnSetTiltPos.Enabled = True
                        btnGoTilt.Enabled = True
                        btnTrainValveScene.Enabled = True
                        btnALign.Enabled = True
                        btnRemoveKey.Enabled = True
                        btnAutoCalibration.Enabled = True
                        btnCCDToValveNext.Enabled = True
                        btnOK.Enabled = True
                        btnCancel.Enabled = True
                        UcJoyStick1.Enabled = True
                    End If
            End Select
        End If
    End Sub

    ''' <summary>取消操作,不存檔離開</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click (Exit Calibration.)")
        btnCancel.Enabled = False
        Timer1.Enabled = False
        btnCancel.Enabled = True
        '20170623 離開時Z 軸升至安全高度
        gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.WeightCalibration(sys.StageNo).SafePosZ(sys.SelectValve))

        '20171108
        If sys.SelectValve = eValveWorkMode.Valve2 Then
            ValveCylinderAction(sys.StageNo, sys.SelectValve, enmUpDown.Up) '汽缸上
        End If
        Me.Close()
    End Sub

    ''' <summary>確認參數, 存檔, 如果有下一支閥, 切換到下一頁.</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnOK]" & vbTab & "Click(->Valve2)")
        btnOK.Enabled = False

        'jimmy 20161005

        '--- 參數儲存 ---
        With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
            gSyslog.Save("CCD Pos X:" & .CCDX(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("CCD Pos Y:" & .CCDY(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("CCD Pos Z:" & .CCDZ(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")

            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
            gSyslog.Save("Valve Pos X:" & .ValveX(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("Valve Pos Y:" & .ValveY(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("Valve Pos Z:" & .ValveZ(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")

            gSyslog.Save("Valve Pin Z Hight:" & .ValvePinZHight(sys.SelectValve, Val(cboB.SelectedItem)))

            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
            gSyslog.Save("CCD Pos X:" & .CCDX(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("CCD Pos Y:" & .CCDY(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("CCD Pos Z:" & .CCDZ(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")

            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
            gSyslog.Save("Valve Pos X:" & .ValveX(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("Valve Pos Y:" & .ValveY(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("Valve Pos Z:" & .ValveZ(sys.SelectValve, Val(cboB.SelectedItem)))
            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")
            '20160920
            gSyslog.Save("Valve Pin Z Hight:" & .ValvePinZHight(sys.SelectValve, Val(cboB.SelectedItem)))

            .SaveCalibrationValve(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini") '儲存設定
            gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).SaveCalibrationValve(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini")
        End With

        '場景曝光與光源存檔
        Dim SceneName As String = LightSceneName & (sys.CCDNo + 1).ToString
        Dim fileName As String = Application.StartupPath & "\System\" & MachineName & "\" & SceneName & ".ini"

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


        ''--- 下一頁顯示 ---  20170321
        'If gSSystemParameter.MechanismModule = enmMechanismModule.TwoValveOneStage Then '單平台雙閥, 需校正第二組
        '    gfrmCalibrationCCD2Valve2 = New frmCalibrationCCD2Valve2
        '    With gfrmCalibrationCCD2Valve2
        '        .sys = sys
        '        .WindowState = FormWindowState.Maximized
        '        .StartPosition = FormStartPosition.Manual
        '        .Location = New Point(0, 0)
        '        .Show()
        '        .BringToFront()
        '    End With
        'End If


        Timer1.Enabled = False
        btnOK.Enabled = True

        'If CalibrationEnable = True Then
        '存檔成功 
        'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        '  MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '   ElseIf CalibrationEnable = False Then
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        'Me.Close()
        '   End If

    End Sub

    Private Sub btnTrainScene_Click(sender As Object, e As EventArgs)
        Dim mfrmAlignPR01 As frmAlignPR01

        gSyslog.Save("[frmRecipe01]" & vbTab & "[btnTrainScene]" & vbTab & "Click")
        '    btnTrainScene.Enabled = False
        'If Not gCRecipe.Editable Then
        '    BtnReadOnlyBehavior(sender)
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    btnTrainScene.Enabled = True
        '    Exit Sub
        'End If

        Dim mSceneName As String = "CALIB" & sys.StageNo
        'If mSceneIName = "" Then
        '    MsgBox("Illegal Scene Name!!")
        '    btnTrainScene.Enabled = True
        '    Exit Sub
        'End If

        If gAOICollection.SetCCDScene(sys.CCDNo, mSceneName) = False Then
            '場景不存在
            gSyslog.Save(mSceneName & gMsgHandler.GetMessage(Warn_3000020))
            MsgBox(mSceneName & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            mfrmAlignPR01 = New frmAlignPR01 '場景不存在也要能進去
            With mfrmAlignPR01
                .Sys = sys
                .SceneName = mSceneName
                .StartPosition = FormStartPosition.Manual
                .Location = New Point(0, 0)
                .Visible = True
                .Show()
                .BringToFront()
            End With
            '      btnTrainScene.Enabled = True
            Exit Sub
        End If
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        System.Threading.Thread.CurrentThread.Join(10)
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
                '     btnTrainScene.Enabled = True
                Exit Sub
            End If
        Loop Until Not gAOICollection.IsCCDCBusy(sys.CCDNo) '取像完成
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉

        Dim AlignCount As Integer = 1

        Dim AlignSceneName() As String = gAOICollection.GetAlignmentToolNameList(sys.CCDNo)
        If Not AlignSceneName Is Nothing Then
            For mSceneNo As Integer = 0 To AlignSceneName.Count - 1
                gAOICollection.SetAlignImage(sys.CCDNo, AlignSceneName(mSceneNo))
            Next
        End If

        mfrmAlignPR01 = New frmAlignPR01
        With mfrmAlignPR01
            .Sys = sys
            .SceneName = mSceneName
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .Visible = True
            .Show()
            .BringToFront()
        End With
        '   btnTrainScene.Enabled = True
    End Sub

    Private Sub txtCCDPosX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCCDPosX.KeyPress, txtCCDPosY.KeyPress, txtCCDPosZ.KeyPress, txtValvePosX.KeyPress, txtValvePosY.KeyPress, txtValvePosZ.KeyPress
        Premtek.Base.CKeyPress.CheckDecimal(sender, e)
    End Sub

    Private Sub txtSyringePressure_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSyringePressure.KeyPress
        Premtek.Base.CKeyPress.CheckUDecimal(sender, e)
    End Sub

    '20160920
    Private Sub btnGetLaser_Click(sender As Object, e As EventArgs) Handles btnGetLaser.Click

        btnGetLaser.Enabled = False

        '20170602按鍵保護
        btnSetCcdPos.Enabled = False
        btnSetValvePos.Enabled = False
        btnGoValve1Pos.Enabled = False
        btnGoCCDPos.Enabled = False
        btnSetTiltPos.Enabled = False
        btnGoTilt.Enabled = False
        btnTrainValveScene.Enabled = False
        btnALign.Enabled = False
        btnRemoveKey.Enabled = False
        btnAutoCalibration.Enabled = False
        btnCCDToValveNext.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        UcJoyStick1.Enabled = False

        Dim HeightValue As String = ""
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnGetLaser]" & vbTab & "Click")
        'Toby_Start
        Select gSSystemParameter.MeasureType
            Case enmMeasureType.Contact '接觸式測高
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                    '測高儀讀值失敗
                    Select Case sys.StageNo
                        Case enmStage.No1
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014004), "Error_1014004", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No2
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014104), "Error_1014104", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014104), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No3
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014204), "Error_1014204", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014204), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No4
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014304), "Error_1014304", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    btnGetLaser.Enabled = True

                    '20170602按鍵保護
                    btnSetCcdPos.Enabled = True
                    btnSetValvePos.Enabled = True
                    btnGoValve1Pos.Enabled = True
                    btnGoCCDPos.Enabled = True
                    btnSetTiltPos.Enabled = True
                    btnGoTilt.Enabled = True
                    btnTrainValveScene.Enabled = True
                    btnALign.Enabled = True
                    btnRemoveKey.Enabled = True
                    btnAutoCalibration.Enabled = True
                    btnCCDToValveNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True

                    Exit Sub
                End If
            Case enmMeasureType.Laser
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                    '測高儀讀值失敗
                    Select Case sys.StageNo
                        Case enmStage.No1
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014004), "Error_1014004", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No2
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014104), "Error_1014104", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014104), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No3
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014204), "Error_1014204", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014204), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No4
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014304), "Error_1014304", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    btnGetLaser.Enabled = True

                    '20170602按鍵保護
                    btnSetCcdPos.Enabled = True
                    btnSetValvePos.Enabled = True
                    btnGoValve1Pos.Enabled = True
                    btnGoCCDPos.Enabled = True
                    btnSetTiltPos.Enabled = True
                    btnGoTilt.Enabled = True
                    btnTrainValveScene.Enabled = True
                    btnALign.Enabled = True
                    btnRemoveKey.Enabled = True
                    btnAutoCalibration.Enabled = True
                    btnCCDToValveNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True
                    Exit Sub
                End If
        End Select

        btnGetLaser.Enabled = True

        '20170602按鍵保護
        btnSetCcdPos.Enabled = True
        btnSetValvePos.Enabled = True
        btnGoValve1Pos.Enabled = True
        btnGoCCDPos.Enabled = True
        btnSetTiltPos.Enabled = True
        btnGoTilt.Enabled = True
        btnTrainValveScene.Enabled = True
        btnALign.Enabled = True
        btnRemoveKey.Enabled = True
        btnAutoCalibration.Enabled = True
        btnCCDToValveNext.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True

        lblLaserReaderValue.Text = HeightValue
    End Sub

    Private Sub nmcExposure_ValueChanged(sender As Object, e As EventArgs) Handles nmcExposure.ValueChanged
        'gAOICollection.SetExposure(sys.CCDNo, nmcExposure.Value) '設定曝光值
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照開  
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
    End Sub
    Public Sub SaveTiltPos()
        'CCD跟閥
        Dim sTemp As SCCDTiltValveCalibration = New SCCDTiltValveCalibration
        sTemp.ValveCalibPosX = CDec(Val(txtValvePosX.Text))
        sTemp.ValveCalibPosY = CDec(Val(txtValvePosY.Text))
        sTemp.ValveCalibPosZ = CDec(Val(txtValvePosZ.Text))
        sTemp.CCDCalibPosX = CDec(Val(txtCCDPosX.Text))
        sTemp.CCDCalibPosY = CDec(Val(txtCCDPosY.Text))
        sTemp.CCDCalibPosZ = CDec(Val(txtCCDPosZ.Text))
        With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
            .ReMoveCCDTiltValve(sys.SelectValve, mTiltCommandValue)
            .AddCCDTiltCCDValve(sys.SelectValve, mTiltCommandValue, sTemp)
        End With
        '雷射跟閥 此頁面針對"雷射跟閥"只增加角度,xyz等位置,由"雷射跟閥"的頁面新增
        Dim sTemp2 As SLaserTiltValveCalibration = New SLaserTiltValveCalibration
        'Valve跟Laser內若無此角度再新增
        With gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo)
            If .DicLaserTiltValve(sys.SelectValve).ContainsKey(mTiltCommandValue) = False Then
                .ReMoveLaserTiltValve(sys.SelectValve, mTiltCommandValue)
                .AddLaserTiltValve(sys.SelectValve, mTiltCommandValue, sTemp2)
            End If
        End With
        Call UpdateComboxB()
        '[Note]:將SelectInedex指回去
        cboB.SelectedItem = mTiltCommandValue
    End Sub
    Public Sub SaveTiltValvePinPos()
        '[Note]:數值統一記錄在記憶體(mTiltCommandValue)
        Dim sTemp As SCCDTiltValveCalibration = New SCCDTiltValveCalibration
        sTemp = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).DicCCDTiltValveCalib(sys.SelectValve).Item(mTiltCommandValue)
        sTemp.ValveCalibPosX = Val(txtValvePosX.Text)
        sTemp.ValveCalibPosY = Val(txtValvePosY.Text)
        sTemp.ValveCalibPosZ = Val(txtValvePosZ.Text)
        With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
            .ReMoveCCDTiltValve(sys.SelectValve, mTiltCommandValue)
            .AddCCDTiltCCDValve(sys.SelectValve, mTiltCommandValue, sTemp)
        End With
    End Sub
    Public Sub SaveCCDPos()
        Dim sTemp As SCCDTiltValveCalibration = New SCCDTiltValveCalibration
        sTemp = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).DicCCDTiltValveCalib(sys.SelectValve).Item(mTiltCommandValue)
        sTemp.CCDCalibPosX = Val(txtCCDPosX.Text)
        sTemp.CCDCalibPosY = Val(txtCCDPosY.Text)
        sTemp.CCDCalibPosZ = Val(txtCCDPosZ.Text)
        With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
            .ReMoveCCDTiltValve(sys.SelectValve, mTiltCommandValue)
            .AddCCDTiltCCDValve(sys.SelectValve, mTiltCommandValue, sTemp)
        End With
    End Sub
    Private Sub btnRemoveKey_Click(sender As Object, e As EventArgs) Handles btnRemoveKey.Click
        gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).ReMoveCCDTiltValve(sys.SelectValve, mTiltCommandValue)
        gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).ReMoveLaserTiltValve(sys.SelectValve, mTiltCommandValue)
        Call RemoveComB()
    End Sub

    Public Sub UpdateComboxB()
        cboB.Items.Clear()
        For i As Integer = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).DicCCDTiltValveCalib(sys.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
            cboB.Items.Add(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).DicCCDTiltValveCalib(sys.SelectValve).Keys(i))
        Next
    End Sub
    Public Sub RemoveComB()
        cboB.Items.Clear()
        For i As Integer = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).DicCCDTiltValveCalib(sys.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
            cboB.Items.Add(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).DicCCDTiltValveCalib(sys.SelectValve).Keys(i))
        Next
        If cboB.Items.Count > 0 Then
            cboB.SelectedIndex = 0
        End If
    End Sub
    Private Sub cboB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboB.SelectedIndexChanged
        txtTiltValvePosB.Text = cboB.Text.ToString
        mTiltCommandValue = CDec(txtTiltValvePosB.Text)
        Dim sTemp As SCCDTiltValveCalibration = New SCCDTiltValveCalibration
        sTemp = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).DicCCDTiltValveCalib(sys.SelectValve)(mTiltCommandValue)
        txtValvePosX.Text = sTemp.ValveCalibPosX
        txtValvePosY.Text = sTemp.ValveCalibPosY
        txtValvePosZ.Text = sTemp.ValveCalibPosZ
        txtCCDPosX.Text = sTemp.CCDCalibPosX
        txtCCDPosY.Text = sTemp.CCDCalibPosY
        txtCCDPosZ.Text = sTemp.CCDCalibPosZ

        lbConvertTiltAngle.Text = ConverTiltAngle(CDbl(mTiltCommandValue), 3).ToString("##.###")

    End Sub

    Private Sub btnSetTiltPos_Click(sender As Object, e As EventArgs) Handles btnSetTiltPos.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnSetTiltPos]" & vbTab & "Click")
        mTiltCommandValue = txtTiltValvePosB.Text
        SaveTiltPos()
    End Sub

    Private Sub btnGoTilt_Click(sender As Object, e As EventArgs) Handles btnGoTilt.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnGoTilt]" & vbTab & "Click")

        btnGoTilt.Enabled = False

        '20170602按鍵保護
        btnSetCcdPos.Enabled = False
        btnSetValvePos.Enabled = False
        btnGoValve1Pos.Enabled = False
        btnGetLaser.Enabled = False
        btnSetTiltPos.Enabled = False
        btnGoCCDPos.Enabled = False
        btnTrainValveScene.Enabled = False
        btnALign.Enabled = False
        btnRemoveKey.Enabled = False
        btnAutoCalibration.Enabled = False
        btnCCDToValveNext.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        UcJoyStick1.Enabled = False


        With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015,
                                                .CCDX(sys.SelectValve, Val(cboB.SelectedItem)),
                                                .CCDY(sys.SelectValve, Val(cboB.SelectedItem)),
                                                .CCDY(sys.SelectValve, Val(cboB.SelectedItem)),
                                                "INFO_6019015"))
            Dim AxisNo(1) As Integer
            Dim TargetPos(1) As Decimal
            AxisNo(0) = sys.AxisZ
            AxisNo(1) = sys.AxisB
            TargetPos(0) = 0
            TargetPos(1) = CDec(Val(txtTiltValvePosB.Text))
            ButtonSafeMoveTiltPos(sender, AxisNo, TargetPos, sys)
        End With
        UcJoyStick1.RefreshPosition()
        btnGoTilt.Enabled = True

        '20170602按鍵保護
        btnSetCcdPos.Enabled = True
        btnSetValvePos.Enabled = True
        btnGoValve1Pos.Enabled = True
        btnGetLaser.Enabled = True
        btnSetTiltPos.Enabled = True
        btnGoCCDPos.Enabled = True
        btnTrainValveScene.Enabled = True
        btnALign.Enabled = True
        btnRemoveKey.Enabled = True
        btnAutoCalibration.Enabled = True
        btnCCDToValveNext.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True


    End Sub


    Private Sub btnTrainValveScene_Click(sender As Object, e As EventArgs) Handles btnTrainValveScene.Click
        If gSSystemParameter.CCDAlignModuleEnable Then
            '[Note]模組化測試中----------
            Dim mfrmCogToolBlock As frmAlignModule
            If btnTrainValveScene.Enabled = False Then
                Exit Sub
            End If
            gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnTrainValveScene]" & vbTab & "Click")
            btnTrainValveScene.Enabled = False
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
            btnTrainValveScene.Enabled = True
            '[Note]模組化測試中----------

        Else
            Dim mfrmCogToolBlock As frmCalibAlignTool
            gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnTrainValveScene]" & vbTab & "Click")
            btnTrainValveScene.Enabled = False
            Dim CCDScene As String = CalibSceneName & (sys.CCDNo + 1).ToString

            If gAOICollection.SetCCDScene(sys.CCDNo, CCDScene) = False Then
                '場景不存在
                gSyslog.Save(CCDScene & gMsgHandler.GetMessage(Warn_3000020))
                MsgBox(CCDScene & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                mfrmCogToolBlock = New frmCalibAlignTool '場景不存在也要能進去
                With mfrmCogToolBlock
                    .Sys = sys
                    .SceneName = CCDScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                End With
                btnTrainValveScene.Enabled = True
                Exit Sub
            End If
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False)
            System.Threading.Thread.CurrentThread.Join(10)
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
                    btnTrainValveScene.Enabled = True
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
            btnTrainValveScene.Enabled = True
        End If


    End Sub

    Private Sub btnALign_Click(sender As Object, e As EventArgs) Handles btnALign.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnALign]" & vbTab & "Click")
        Dim ticket As Integer = 0
        Dim timeOutStopwatch As New Stopwatch
        Dim CCDScene As String = CalibSceneName & (sys.CCDNo + 1).ToString
        lblOffsetX.Text = "0"
        lblOffsetY.Text = "0"

        btnALign.Enabled = False

        '20170602按鍵保護
        btnSetCcdPos.Enabled = False
        btnSetValvePos.Enabled = False
        btnGoCCDPos.Enabled = False
        btnGetLaser.Enabled = False
        btnSetTiltPos.Enabled = False
        btnGoTilt.Enabled = False
        btnTrainValveScene.Enabled = False
        btnGoValve1Pos.Enabled = False
        btnRemoveKey.Enabled = False
        btnAutoCalibration.Enabled = False
        btnCCDToValveNext.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        UcJoyStick1.Enabled = False



        '===設置場景與光源
        gAOICollection.SetCCDScene(sys.CCDNo, CCDScene) '選擇場景
        UcLightControl1.SceneName = CCDScene
        UcLightControl1.ShowUI()

        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
        'System.Threading.Thread.CurrentThread.Join(10)
        'ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
        'System.Threading.Thread.CurrentThread.Join(10)
        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保
        timeOutStopwatch.Restart()
        Do
            Application.DoEvents()
            If timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                'CCD 取像TimeOut
                Select Case sys.StageNo
                    Case 0
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012603), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012903), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
                btnALign.Enabled = True

                '20170602按鍵保護
                btnSetCcdPos.Enabled = True
                btnSetValvePos.Enabled = True
                btnGoCCDPos.Enabled = True
                btnGetLaser.Enabled = True
                btnSetTiltPos.Enabled = True
                btnGoTilt.Enabled = True
                btnTrainValveScene.Enabled = True
                btnGoValve1Pos.Enabled = True
                btnRemoveKey.Enabled = True
                btnAutoCalibration.Enabled = True
                btnCCDToValveNext.Enabled = True
                btnOK.Enabled = True
                btnCancel.Enabled = True
                UcJoyStick1.Enabled = True


                'MsgBox("Acquisition is Timeout!")
                Exit Sub
            End If
        Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
        Debug.Print("IsCCDCBusy:" & timeOutStopwatch.ElapsedMilliseconds)

        System.Threading.Thread.CurrentThread.Join(10)
        'Debug.Print("InvokeUcDisplay  Done")
        timeOutStopwatch.Restart()
        'gCCDAlignResultDict(sys.CCDNo)(ticket)
        Do
            If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                Exit Do
            End If
            Application.DoEvents()
            If timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                'CCD 計算TimeOut
                Select Case sys.StageNo
                    Case 0
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012604), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012904), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
                'MsgBox("Calculation is Timeout!")
                btnALign.Enabled = True

                '20170602按鍵保護
                btnSetCcdPos.Enabled = True
                btnSetValvePos.Enabled = True
                btnGoCCDPos.Enabled = True
                btnGetLaser.Enabled = True
                btnSetTiltPos.Enabled = True
                btnGoTilt.Enabled = True
                btnTrainValveScene.Enabled = True
                btnGoValve1Pos.Enabled = True
                btnRemoveKey.Enabled = True
                btnAutoCalibration.Enabled = True
                btnCCDToValveNext.Enabled = True
                btnOK.Enabled = True
                btnCancel.Enabled = True
                UcJoyStick1.Enabled = True

                Exit Sub
            End If
        Loop

        timeOutStopwatch.Restart()
        Do
            If gCCDAlignResultDict(sys.CCDNo)(ticket).IsRunSuccess = True Then
                Exit Do
            End If
            Application.DoEvents()
            If timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                'CCD 計算TimeOut
                Select Case sys.StageNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012004))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012304))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012604))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012604), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012904))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2012904), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
                btnALign.Enabled = True

                '20170602按鍵保護
                btnSetCcdPos.Enabled = True
                btnSetValvePos.Enabled = True
                btnGoCCDPos.Enabled = True
                btnGetLaser.Enabled = True
                btnSetTiltPos.Enabled = True
                btnGoTilt.Enabled = True
                btnTrainValveScene.Enabled = True
                btnGoValve1Pos.Enabled = True
                btnRemoveKey.Enabled = True
                btnAutoCalibration.Enabled = True
                btnCCDToValveNext.Enabled = True
                btnOK.Enabled = True
                btnCancel.Enabled = True
                UcJoyStick1.Enabled = True


                Exit Sub
            End If
        Loop

        '=== 取得拍照結果 ===
        Dim offsetX, offsetY As Decimal
        If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
            offsetX = 0
            offsetY = 0
            'Dim degree As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
            'CCD 找不到特徵點(等於0)
            Select Case sys.StageNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012103))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012103), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012403))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012403), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 2
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012703))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012703), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 3
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2013003))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2013003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            btnALign.Enabled = True

            '20170602按鍵保護
            btnSetCcdPos.Enabled = True
            btnSetValvePos.Enabled = True
            btnGoCCDPos.Enabled = True
            btnGetLaser.Enabled = True
            btnSetTiltPos.Enabled = True
            btnGoTilt.Enabled = True
            btnTrainValveScene.Enabled = True
            btnGoValve1Pos.Enabled = True
            btnRemoveKey.Enabled = True
            btnAutoCalibration.Enabled = True
            btnCCDToValveNext.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True
            UcJoyStick1.Enabled = True


            Exit Sub
        ElseIf gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count > 1 Then '畫面有多個特徵
            offsetX = 0
            offsetY = 0
            'CCD 找到多個特徵點(大於1)
            Select Case sys.StageNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012102))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012102), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012402))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012402), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 2
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012702))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2012702), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 3
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2013002))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2013002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            btnALign.Enabled = True

            '20170602按鍵保護
            btnSetCcdPos.Enabled = True
            btnSetValvePos.Enabled = True
            btnGoCCDPos.Enabled = True
            btnGetLaser.Enabled = True
            btnSetTiltPos.Enabled = True
            btnGoTilt.Enabled = True
            btnTrainValveScene.Enabled = True
            btnGoValve1Pos.Enabled = True
            btnRemoveKey.Enabled = True
            btnAutoCalibration.Enabled = True
            btnCCDToValveNext.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True
            UcJoyStick1.Enabled = True

            Exit Sub
        Else '有一個特徵
            offsetX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
            offsetY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
            lblOffsetX.Text = Math.Round(offsetX, 3)
            lblOffsetY.Text = Math.Round(offsetY, 4)
            btnALign.Enabled = True

            '20170602按鍵保護
            btnSetCcdPos.Enabled = True
            btnSetValvePos.Enabled = True
            btnGoCCDPos.Enabled = True
            btnGetLaser.Enabled = True
            btnSetTiltPos.Enabled = True
            btnGoTilt.Enabled = True
            btnTrainValveScene.Enabled = True
            btnGoValve1Pos.Enabled = True
            btnRemoveKey.Enabled = True
            btnAutoCalibration.Enabled = True
            btnCCDToValveNext.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True
            UcJoyStick1.Enabled = True


        End If

        btnALign.Enabled = True

        '20170602按鍵保護
        btnSetCcdPos.Enabled = True
        btnSetValvePos.Enabled = True
        btnGoCCDPos.Enabled = True
        btnGetLaser.Enabled = True
        btnSetTiltPos.Enabled = True
        btnGoTilt.Enabled = True
        btnTrainValveScene.Enabled = True
        btnGoValve1Pos.Enabled = True
        btnRemoveKey.Enabled = True
        btnAutoCalibration.Enabled = True
        btnCCDToValveNext.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True


    End Sub

    Private Sub txtPressure_TextChanged(sender As Object, e As EventArgs) Handles txtAirPressure.TextChanged
        If IsNothing(sys) = True Then
            Exit Sub
        End If
        If sys.StageNo < 0 Then
            Exit Sub
        End If
        gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decAirPressure(sys.SelectValve, Val(cboB.SelectedItem)) = Val(txtAirPressure.Text)
    End Sub

    Private Sub txtCount_TextChanged(sender As Object, e As EventArgs) Handles txtCount.TextChanged
        If IsNothing(sys) = True Then
            Exit Sub
        End If
        If sys.StageNo < 0 Then
            Exit Sub
        End If
        gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).iCCDValveCount(sys.SelectValve, Val(cboB.SelectedItem)) = Val(txtCount.Text)
    End Sub

    Private Sub txtPitch_TextChanged(sender As Object, e As EventArgs) Handles txtPitch.TextChanged
        If IsNothing(sys) = True Then
            Exit Sub
        End If
        If sys.StageNo < 0 Then
            Exit Sub
        End If
        gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decPitch(sys.SelectValve, Val(cboB.SelectedItem)) = Val(txtPitch.Text)
    End Sub


    Private Sub btnCCDToValveNext_Click_1(sender As Object, e As EventArgs) Handles btnCCDToValveNext.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnCCDToValveNext]" & vbTab & "Click")
        Me.Close()

        If gfrmCalibrationCCD2Laser Is Nothing Then
            gfrmCalibrationCCD2Laser = New frmCalibrationCCD2Height
        ElseIf gfrmCalibrationCCD2Laser.IsDisposed Then
            gfrmCalibrationCCD2Laser = New frmCalibrationCCD2Height
        End If

        With gfrmCalibrationCCD2Laser
            .sys = gSYS(sys.EsysNum)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            '.CalibrationEnable = True
            ._oLastSelectCobValve = cboB.SelectedItem
            .ShowDialog()
        End With

    End Sub


    Private Sub btnSetAirPressure_Click(sender As Object, e As EventArgs) Handles btnSetAirPressure.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnSetAirPressure]" & vbTab & "Click")
        Select Case sys.StageNo
            Case 0
                Select Case sys.SelectValve
                    Case eValveWorkMode.Valve1
                        'gEPVCollection.SetValue(enmEPV.No1, Val(txtAirPressure.Text), True)
                        gEPVCollection.SetValue(eEPVPressureType.Syringe, enmEPV.No1, Val(txtAirPressure.Text), True)
                    Case eValveWorkMode.Valve2
                        'gEPVCollection.SetValue(enmEPV.No2, Val(txtAirPressure.Text), True)
                        gEPVCollection.SetValue(eEPVPressureType.Syringe, enmEPV.No2, Val(txtAirPressure.Text), True)
                End Select

            Case 1
                'gEPVCollection.SetValue(enmEPV.No2, Val(txtAirPressure.Text), True)
                gEPVCollection.SetValue(eEPVPressureType.Syringe, enmEPV.No2, Val(txtAirPressure.Text), True)

            Case 2
                'gEPVCollection.SetValue(enmEPV.No3, Val(txtAirPressure.Text), True)
                gEPVCollection.SetValue(eEPVPressureType.Syringe, enmEPV.No3, Val(txtAirPressure.Text), True)
            Case 3
                'gEPVCollection.SetValue(enmEPV.No4, Val(txtAirPressure.Text), True
                gEPVCollection.SetValue(eEPVPressureType.Syringe, enmEPV.No4, Val(txtAirPressure.Text), True)
        End Select

    End Sub

    Private Sub btnSetSyringe1OnOff_Click(sender As Object, e As EventArgs) Handles btnSetSyringe1OnOff.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnSetSyringe1OnOff]" & vbTab & "Click")
        Select Case sys.StageNo
            Case 0

                Select Case sys.SelectValve
                    Case eValveWorkMode.Valve1
                        gDOCollection.SetState(enmDO.SyringePressure1, Not gDOCollection.GetState(enmDO.SyringePressure1))
                        Select Case gDOCollection.GetState(enmDO.SyringePressure1)
                            Case True
                                btnSetSyringe1OnOff.Text = GetString("On")
                            Case False
                                btnSetSyringe1OnOff.Text = GetString("Off")
                        End Select
                    Case eValveWorkMode.Valve2
                        gDOCollection.SetState(enmDO.SyringePressure2, Not gDOCollection.GetState(enmDO.SyringePressure2))
                        Select Case gDOCollection.GetState(enmDO.SyringePressure2)
                            Case True
                                btnSetSyringe1OnOff.Text = GetString("On")
                            Case False
                                btnSetSyringe1OnOff.Text = GetString("Off")
                        End Select
                End Select

            Case 1
                gDOCollection.SetState(enmDO.SyringePressure2, Not gDOCollection.GetState(enmDO.SyringePressure2))
                Select Case gDOCollection.GetState(enmDO.SyringePressure2)
                    Case True
                        btnSetSyringe1OnOff.Text = GetString("On")
                    Case False
                        btnSetSyringe1OnOff.Text = GetString("Off")
                End Select
            Case 2
                gDOCollection.SetState(enmDO.SyringePressure3, Not gDOCollection.GetState(enmDO.SyringePressure3))
                Select Case gDOCollection.GetState(enmDO.SyringePressure3)
                    Case True
                        btnSetSyringe1OnOff.Text = GetString("On")
                    Case False
                        btnSetSyringe1OnOff.Text = GetString("Off")
                End Select
            Case 3
                gDOCollection.SetState(enmDO.SyringePressure4, Not gDOCollection.GetState(enmDO.SyringePressure4))
                Select Case gDOCollection.GetState(enmDO.SyringePressure4)
                    Case True
                        btnSetSyringe1OnOff.Text = GetString("On")
                    Case False
                        btnSetSyringe1OnOff.Text = GetString("Off")
                End Select
        End Select

    End Sub

    '20170520
    Private Sub txtCCDStableTime_TextChanged(sender As Object, e As EventArgs) Handles txtCCDStableTime.TextChanged
        If IsNothing(sys) = True Then
            Exit Sub
        End If
        If sys.StageNo < 0 Then
            Exit Sub
        End If
        gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decCCDStableTime(sys.SelectValve, Val(cboB.SelectedItem)) = Val(txtCCDStableTime.Text)
    End Sub

    '20170520
    Private Sub btCalibrationValveParameter_Click(sender As Object, e As EventArgs) Handles btCalibrationValveParameter.Click

        '    Dim sChoiseType As String = cbDotTypeSelect.Text
        '-----------------------------------------------
        Dim frmShow As New Form

        Select gSSystemParameter.StageParts.ValveData(sys.StageNo).JetValve(sys.SelectValve)
            Case eValveModel.PicoPulse
                'Pico
                '-----------------------------------------------
                Dim ucValveParameterPicoShow As New ucValveParameterPico(frmShow)
                With ucValveParameterPicoShow
                    .Dock = DockStyle.Fill
                    .gSys = sys
                End With
                '-----------------------------------------------
                '   Dim nPoint As Point = New Point(1080, 460)
                ' 580, 540
                With frmShow
                    .Width = 1025
                    .Height = 620 '525 = 原始大小 + 邊寬30
                    .MinimizeBox = False
                    .MaximizeBox = False
                    .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                    .Text = "Valve Parameter!!!"
                    .StartPosition = FormStartPosition.Manual
                    '  .Location = nPoint
                    .Controls.Add(ucValveParameterPicoShow)
                    .ShowDialog()
                End With
                '-----------------------------------------------
            Case eValveModel.Advanjet
                '-----------------------------------------------
                'Advanjet
                Dim ucValveParameterAdvanjetShow As New ucValveParameterAdvanjet(frmShow)
                With ucValveParameterAdvanjetShow
                    .Dock = DockStyle.Fill
                    .gSys = sys
                End With
                '-----------------------------------------------
                '   Dim nPoint As Point = New Point(1080, 460)
                ' 580, 540
                With frmShow
                    .Width = 900
                    .Height = 500 '525 = 原始大小 + 邊寬30
                    .MinimizeBox = False
                    .MaximizeBox = False
                    .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                    .Text = "Valve Parameter!!!"
                    .StartPosition = FormStartPosition.Manual
                    '  .Location = nPoint
                    .Controls.Add(ucValveParameterAdvanjetShow)
                    .ShowDialog()
                End With
                '-----------------------------------------------
        End Select
    End Sub

    '20170520
    Private Sub txtCCDStableTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCCDStableTime.KeyPress
        Premtek.Base.CKeyPress.CheckUDecimal(sender, e)
    End Sub

    Private Sub btnCalibrationValveDot_Click(sender As Object, e As EventArgs) Handles btnCalibrationValveDot.Click
        '觸發板用資料
        Dim mCyleParam As sTriggerTPCmdParam
        Dim mTriggerCmdFailCount(enmStage.Max) As Integer                               '[紀錄資料傳輸異常次數]
        Dim mStopWatch As New Stopwatch
        Dim bTimeOut As Boolean = False

        Dim bCheckResetAlarm As Boolean = False
        Dim bCheckCycleRecipeAlarm As Boolean = False
        Dim bCheckDispenseRunAlarm As Boolean = False

        Dim bResetAlarm As Boolean = False
        Dim bCycleRecipeAlarm As Boolean = False
        Dim bDispenseRunAlarm As Boolean = False
        Dim mPosB(enmStage.Max) As Decimal

        '給氣壓
        '[說明]:關閉膠閥  
        Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eOff)

        '[說明]:開啟膠桶壓力
        gSysAdapter.SetSyringePressure(sys.StageNo, sys.SelectValve, enmONOFF.eON)

        Call gDOCollection.RefreshDO()  '寫入卡片 DO點

        mStopWatch.Restart()
        Do
            bTimeOut = IsTimeOut(mStopWatch, 100)
        Loop While (bTimeOut = False)

        If sys.AxisB <> -1 Then
            '[說明]:設定出膠壓力
            gEPVCollection.SetValue(sys.StageNo, sys.SelectValve, eEPVPressureType.Syringe, gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decAirPressure(sys.SelectValve, mPosB(sys.StageNo)), False)
        Else
            '[說明]:設定出膠壓力
            gEPVCollection.SetValue(sys.StageNo, sys.SelectValve, eEPVPressureType.Syringe, gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).decAirPressure(sys.SelectValve, 0), False)
        End If

        '====================================================================================================================================================================================
        'Reset Alarm
        mStopWatch.Restart()
        Do
            If gTriggerBoard.SetResetAlarm(sys.StageNo, False) = True Then
                bResetAlarm = True
            End If
            '[說明]:TimeOut時間
            Do
                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                If bTimeOut = True Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Alarm_2016001", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Alarm_2016101", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Alarm_2016201", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Alarm_2016301", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                    End Select
                    Exit Sub
                End If
            Loop While (bTimeOut = True)
        Loop While (bResetAlarm = False)

        Do
            If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                '[Note]:還在接收傳送資料中
                If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'Reset Alarm
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetResetAlarm(sys.StageNo, False) = True Then
                                bResetAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016001", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016101", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016201", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016301", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bResetAlarm = False)
                    End If
                End If
            Else
                '[Note]:檢查接收資料
                If gTriggerBoard.ResetAlarm(sys.StageNo).Status = True Then
                    mTriggerCmdFailCount(sys.StageNo) = 0
                    bCheckResetAlarm = True
                Else
                    '[Note]:查看收到的內容是????
                    Debug.Print("ResetAlarm: " & gTriggerBoard.ResetAlarm(sys.StageNo).STR)
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'Reset Alarm
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetResetAlarm(sys.StageNo, False) = True Then
                                bResetAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016001", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016101", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016201", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Alarm_2016301", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bResetAlarm = False)
                    End If
                End If
            End If
        Loop While (bCheckResetAlarm = False)

        '[說明]:設定Jetting Valve Trigger Controller 為固定頻率打點模式
        mCyleParam.DotCounts = 1
        mCyleParam.JetPressure = 0
        mCyleParam.GluePressure = 0
        Select Case gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).ValveModel
            Case eValveModel.Advanjet
                mCyleParam.CycleTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).Advanjet.CycleTime * 1000)
                mCyleParam.PulseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).Advanjet.PulseTime * 1000)
            Case eValveModel.PicoPulse
                mCyleParam.CycleTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CycleTime * 1000)
                mCyleParam.PulseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.ValveOnTime * 1000)
                mCyleParam.OpenTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.OpenTime * 1000)
                mCyleParam.CloseTime = CInt(gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CloseTime * 1000)
                mCyleParam.Stroke = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.Stroke
                mCyleParam.CloseVoltage = gJetValveDB(gCRecipe.StageParts(sys.StageNo).ValveName(sys.SelectValve)).PicoTouch.CloseVoltage
        End Select




        'SetCycleRecipe
        mStopWatch.Restart()
        Do
            If gTriggerBoard.SetCycleRecipe(sys.StageNo, mCyleParam, False) = True Then
                bCycleRecipeAlarm = True
            End If
            '[說明]:TimeOut時間
            Do
                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                If bTimeOut = True Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1016003", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1016103", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1016203", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1016303", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                    End Select
                    Exit Sub
                End If
            Loop While (bTimeOut = True)
        Loop While (bCycleRecipeAlarm = False)


        Do
            If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                '[Note]:還在接收傳送資料中
                If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'SetCycleRecipe
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetCycleRecipe(sys.StageNo, mCyleParam, False) = True Then
                                bCycleRecipeAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Error_1016003", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Error_1016103", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Error_1016203", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Error_1016303", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bCycleRecipeAlarm = False)
                    End If
                End If
            Else
                ''[Note]:檢查接收資料  CycleRecipe
                If gTriggerBoard.CycleRecipe(sys.StageNo).Status Then
                    mTriggerCmdFailCount(sys.StageNo) = 0
                    bCheckCycleRecipeAlarm = True
                Else
                    '[Note]:查看收到的內容是????
                    Debug.Print("CycleRecipe(T Cmd): " & gTriggerBoard.CycleRecipe(sys.StageNo).STR)
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1016003", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1016103", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1016203", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1016303", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'SetCycleRecipe
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetCycleRecipe(sys.StageNo, mCyleParam, False) = True Then
                                bCycleRecipeAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Error_1016003", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Error_1016103", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Error_1016203", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Error_1016303", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bCycleRecipeAlarm = False)
                    End If
                End If
            End If
        Loop While (bCheckCycleRecipeAlarm = False)

        'SetCycleRecipe
        mStopWatch.Restart()
        Do
            If gTriggerBoard.SetDispenseRun(sys.StageNo, enmTriggerDispType.CycleRecipe, sys.SelectValve, 0, 0, False) = True Then
                bDispenseRunAlarm = True
            End If
            '[說明]:TimeOut時間
            Do
                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                If bTimeOut = True Then
                    Select Case sys.StageNo
                        Case 0
                            gEqpMsg.AddHistoryAlarm("Error_1016003", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                        Case 1
                            gEqpMsg.AddHistoryAlarm("Error_1016103", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                        Case 2
                            gEqpMsg.AddHistoryAlarm("Error_1016203", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                        Case 3
                            gEqpMsg.AddHistoryAlarm("Error_1016303", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                    End Select
                    Exit Sub
                End If
            Loop While (bTimeOut = True)
        Loop While (bDispenseRunAlarm = False)

        Do
            '[Note]:判斷資料接收完成了沒? & 是否 TimeOut
            If gTriggerBoard.IsBusy(sys.StageNo) = True Then
                '[Note]:還在接收傳送資料中
                If gTriggerBoard.IsTimeOut(sys.StageNo) = True Then
                    '[Note]:超過時間還沒處裡完-->在下一次
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Reset Alarm Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Alarm_2016001", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016001), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Alarm_2016101", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016101), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Alarm_2016201", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016201), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Alarm_2016301", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Alarm_2016301), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'SetCycleRecipe
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetDispenseRun(sys.StageNo, enmTriggerDispType.CycleRecipe, sys.SelectValve, 0, 0, False) = True Then
                                bDispenseRunAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Error_1016003", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Error_1016103", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Error_1016203", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Error_1016303", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bDispenseRunAlarm = False)
                    End If
                End If
            Else
                ''[Note]:檢查接收資料
                If gTriggerBoard.DispenseRun(sys.StageNo).Status Then
                    mTriggerCmdFailCount(sys.StageNo) = 0
                    bCheckDispenseRunAlarm = True
                Else
                    '[Note]:查看收到的內容是????
                    Debug.Print("DispenseRun(X Cmd): " & gTriggerBoard.DispenseRun(sys.StageNo).STR)
                    mTriggerCmdFailCount(sys.StageNo) = mTriggerCmdFailCount(sys.StageNo) + 1
                    If mTriggerCmdFailCount(sys.StageNo) > gTriggerCmdMaxFailCounts Then
                        'TODO:異常訊息(Trigger Board Set Jet Parameter Command Fail)
                        Select Case sys.StageNo
                            Case 0
                                gEqpMsg.AddHistoryAlarm("Error_1016003", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                            Case 1
                                gEqpMsg.AddHistoryAlarm("Error_1016103", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                            Case 2
                                gEqpMsg.AddHistoryAlarm("Error_1016203", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                            Case 3
                                gEqpMsg.AddHistoryAlarm("Error_1016303", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                        End Select
                        Exit Sub
                    Else
                        'SetCycleRecipe
                        mStopWatch.Restart()
                        Do
                            If gTriggerBoard.SetDispenseRun(sys.StageNo, enmTriggerDispType.CycleRecipe, sys.SelectValve, 0, 0, False) = True Then
                                bDispenseRunAlarm = True
                            End If
                            '[說明]:TimeOut時間
                            Do
                                bTimeOut = IsTimeOut(mStopWatch, gSSystemParameter.TimeOut1)
                                If bTimeOut = True Then
                                    Select Case sys.StageNo
                                        Case 0
                                            gEqpMsg.AddHistoryAlarm("Error_1016003", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016003), eMessageLevel.Alarm)
                                        Case 1
                                            gEqpMsg.AddHistoryAlarm("Error_1016103", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016103), eMessageLevel.Alarm)
                                        Case 2
                                            gEqpMsg.AddHistoryAlarm("Error_1016203", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016203), eMessageLevel.Alarm)
                                        Case 3
                                            gEqpMsg.AddHistoryAlarm("Error_1016303", "AutoValveCalibrationAction", sys.SysNum, gMsgHandler.GetMessage(Error_1016303), eMessageLevel.Alarm)
                                    End Select
                                    Exit Sub
                                End If
                            Loop While (bTimeOut = True)
                        Loop While (bDispenseRunAlarm = False)
                    End If
                End If
            End If
        Loop While (bCheckDispenseRunAlarm = False)

        '[說明]:出膠  
        Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eON)
        Call gDOCollection.RefreshDO()  '寫入卡片 DO點

        mStopWatch.Restart()
        Do
            bTimeOut = IsTimeOut(mStopWatch, 100)
        Loop While (bTimeOut = False)

        '[說明]:關膠  
        Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eOff)
        Call gDOCollection.RefreshDO()  '寫入卡片 DO點
        Exit Sub
    End Sub
End Class
Imports ProjectCore
Imports ProjectMotion
Imports ProjectIO
Imports ProjectRecipe
Imports ProjectLaserInterferometer
Imports ProjectAOI

Public Class frmCalibrationZHeight
    ''' <summary>外部傳入</summary>
    ''' <remarks></remarks>
    Public sys As sSysParam
    ''' <summary>校正流程換頁</summary>
    ''' <remarks></remarks>
    Public CalibrationEnable As Boolean

    Private Const LightSceneName As String = "CALIB"
    ''' <summary>有前一頁</summary>
    ''' <remarks></remarks>
    Private blPrePage As Boolean = False
    ''' <summary>
    ''' TODO:型別???
    ''' </summary>
    ''' <remarks></remarks>
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
    Dim mTiltCommandValue As Decimal = -1


    Public Sub RefreshUI()
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
            btnAutoZFind.Enabled = False
            btnSetValvePosX.Enabled = False
            btnSetValve1Z.Enabled = False
            btnRemoveKey.Enabled = False
            btnSetTiltPos.Enabled = False
            btnGoLaserPinPos.Enabled = False
            btnSetLaserPinPos.Enabled = False
            btnGo2.Enabled = False
            btnLaserSensor.Enabled = False
            btnGoTiltPos.Enabled = False
            UcJoyStick1.Enabled = False
            grpValvePos.Enabled = False
            grpLaserPos.Enabled = False
            gbTiltSet.Enabled = False
            UcLightControl1.Enabled = False

            btnValveToHightPrev.Enabled = True
            btnValveToHightNext.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True


        End If
       
    End Sub

    Private Sub frmCalibrationZHeight_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        '20170215
        'Eason 20170120 Ticket:100030 , Memory Freed [S]
        'UcDisplay1.EndLive()
        'Timer1.Enabled = False
        'UcDisplay1.ManualDispose()
        'UcLightControl1.ManualDispose()
        'UcJoyStick1.ManualDispose()
        'UcStatusBar1.ManualDispose()
        'Me.Dispose(True)
        'GC.Collect() 'Eason 20170120 Ticket:100030 , Memory Freed , Be careful Use it .

        'UcDisplay1.EndLive()
        'Timer1.Enabled = False
        'Me.Dispose(True)
        'Eason 20170120 Ticket:100030 , Memory Freed [E]


        ''Eason 20170209 Ticket:100060 :Memory Log
        'gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub frmCalibrationZHeight_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        blPrePage = False
        UcDisplay1.EndLive()
        Timer1.Enabled = False
        UcDisplay1.ManualDispose()
        UcLightControl1.ManualDispose()
        UcJoyStick1.ManualDispose()
        UcStatusBar1.ManualDispose()
        Me.Dispose(True)
        GC.Collect() 'Eason 20170120 Ticket:100030 , Memory Freed , Be careful Use it .

        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)


        'UcDisplay1.EndLive()
        'Timer1.Enabled = False
        'UcLightControl1.ManualDispose()
        'UcDisplay1.MyDispose()
        'UcJoyStick1.MyDispose()
        'UcStatusBar1.ManualDispose()
        'Me.Dispose(True)
        'GC.Collect()
    End Sub

    Private Sub frmCalibrationZHeight_Load(sender As Object, e As EventArgs) Handles Me.Load

        '20170327
        Me.Text = "Calibration Height Sensor " & " Stage" & (sys.EsysNum - eSys.DispStage1 + 1) & " Valve" & sys.SelectValve + 1 & " Height"


        Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height

        '[Note]:使用該組Stage的第一組閥   20170321   此部分參數帶入不寫死
        ' sys.SelectValve = eValveWorkMode.Valve1
       
        With UcJoyStick1
            .AxisX = sys.AxisX
            .AxisY = sys.AxisY
            .AxisZ = sys.AxisZ
            .AXisA = sys.AxisA
            .AXisB = sys.AxisB
            .AXisC = sys.AxisC
        End With
        UcJoyStick1.SetSpeedType(SpeedType.Slow)

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

        gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini")

        '[Note]:先將所有的資料列出來，預設是使用第一組設定值
        UpdateComboxB()
        If blPrePage = True Then
            cboB.SelectedIndex = cboB.Items.IndexOf(oLastSelectCobValve)
            mTiltCommandValue = cboB.SelectedItem
        Else
            cboB.SelectedIndex = 0
            mTiltCommandValue = cboB.SelectedItem
        End If


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
        '20160519 Jeffadd 新增顯示 測高Pin的位置
        'With gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo)
        '    txtValvePosX.Text = .ValvePinPosX(mAbsValveNo)
        '    txtValvePosY.Text = .ValvePinPosY(mAbsValveNo)
        '    txtValvePosZ.Text = .ValvePinPosZ(mAbsValveNo)

        '    txtLaserPosX.Text = .LaserPinPosX(mAbsValveNo)
        '    txtLaserPosY.Text = .LaserPinPosY(mAbsValveNo)
        '    txtLaserPosZ.Text = .LaserPinPosZ(mAbsValveNo)
        'End With

        'UcCalibrationStatus1.Status = ucCalibrationStatus.enmCalibrationStatus.ZHeight

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

        ' If CalibrationEnable = True Then
        btnValveToHightNext.Visible = True
        btnValveToHightPrev.Visible = True
        '    ElseIf CalibrationEnable = False Then
        ' btnValveToHightNext.Visible = False
        '  btnValveToHightPrev.Visible = False
        '    End If


        '[Note]20171107 Ernest要求校正頁面光源亮度統一，所以不關燈
        'Dim mlight As enmLight                                    '[光源]
        ''[Note]:關閉光源
        'mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
        'Call gSysAdapter.SetLightOnOff(mlight, False)
        'mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
        'Call gSysAdapter.SetLightOnOff(mlight, False)


        Timer1.Enabled = True

        '移動到0
        Dim mAxisNo(4) As Integer
        Dim mTargetPos(4) As Decimal
        mAxisNo(0) = sys.AxisX
        mAxisNo(1) = sys.AxisY
        mAxisNo(2) = sys.AxisZ
        mAxisNo(3) = sys.AxisB
        mAxisNo(4) = sys.AxisC
        mTargetPos(0) = gCMotion.GetPositionValue(mAxisNo(0))
        mTargetPos(1) = gCMotion.GetPositionValue(mAxisNo(1))
        mTargetPos(2) = CDec(0)
        mTargetPos(3) = 0
        mTargetPos(4) = 0
        ButtonSafeMovePos(Nothing, mAxisNo, mTargetPos, sys)
        UcJoyStick1.RefreshPosition()

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

    ''' <summary>設定閥頭測高Z</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSetValve1Z_Click(sender As Object, e As EventArgs) Handles btnSetValve1Z.Click
        If mTiltCommandValue = -1 Then
            'Tilt 位置錯誤!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080024))
            MsgBox(gMsgHandler.GetMessage(Alarm_2080024), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnSetValve1Z]" & vbTab & "Click")
        btnSetValve1Z.Enabled = False
        If gSYS(sys.EsysNum).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetValve1Z.Enabled = True
            Exit Sub
        End If

        txtValvePosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6000055, txtValvePosZ.Text), "INFO_6000055")
        UcJoyStick1.RefreshPosition()
        btnSetValve1Z.Enabled = True
    End Sub

    ''' <summary>讀取雷射值</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLaserSensor_Click(sender As Object, e As EventArgs) Handles btnLaserSensor.Click
        If mTiltCommandValue = -1 Then
            'Tilt 位置錯誤(參數無效)
            Select Case sys.StageNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1034010))
                    MsgBox(gMsgHandler.GetMessage(Error_1034010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1046010))
                    MsgBox(gMsgHandler.GetMessage(Error_1046010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 2
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1064010))
                    MsgBox(gMsgHandler.GetMessage(Error_1064010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 3
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1071010))
                    MsgBox(gMsgHandler.GetMessage(Error_1071010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            Exit Sub
        End If
        gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnLaserSensor]" & vbTab & "Click")
        btnLaserSensor.Enabled = False
        Dim value As String = ""

        Select Case gSSystemParameter.MeasureType
            Case enmMeasureType.Contact '接觸式測高
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, value, True) Then
                    btnLaserSensor.BackColor = SystemColors.Control
                    btnLaserSensor.UseVisualStyleBackColor = True
                    txtLaserValueZ.Text = Val(value)
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6000056, txtLaserValueZ.Text), "INFO_6000056")
                Else
                    '測高讀值失敗
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
                    btnLaserSensor.BackColor = Color.Red
                End If
            Case enmMeasureType.Laser
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, value, True) Then
                    btnLaserSensor.BackColor = SystemColors.Control
                    btnLaserSensor.UseVisualStyleBackColor = True
                    txtLaserValueZ.Text = Val(value)
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6000056, txtLaserValueZ.Text), "INFO_6000056")
                Else
                    '測高讀值失敗
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
                    btnLaserSensor.BackColor = Color.Red
                End If
        End Select

        btnLaserSensor.Enabled = True
    End Sub

    ''' <summary>
    ''' 閥頭測高位置設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSetValvePosX_Click(sender As Object, e As EventArgs) Handles btnSetValvePosX.Click
        If mTiltCommandValue = -1 Then
            'Tilt 位置錯誤(參數無效)
            Select Case sys.StageNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1034010))
                    MsgBox(gMsgHandler.GetMessage(Error_1034010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1046010))
                    MsgBox(gMsgHandler.GetMessage(Error_1046010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 2
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1064010))
                    MsgBox(gMsgHandler.GetMessage(Error_1064010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 3
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1071010))
                    MsgBox(gMsgHandler.GetMessage(Error_1071010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            Exit Sub
        End If

        gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnSetValvePosX]" & vbTab & "Click")
        btnSetValvePosX.Enabled = False
        If gSYS(sys.EsysNum).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetValvePosX.Enabled = True
            Exit Sub
        End If
        gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
        gSyslog.Save("Valve Pos X:" & txtValvePosX.Text)
        gSyslog.Save("Valve Pos Y:" & txtValvePosY.Text)
        gSyslog.Save("Valve Pos Z:" & txtValvePosZ.Text)
        gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")
        txtValvePosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtValvePosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtValvePosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))

        SaveTiltValvePinPos()
        'With gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo)
        '    .ValvePinPosX(mAbsValveNo) = Val(txtValvePosX.Text)
        '    .ValvePinPosY(mAbsValveNo) = Val(txtValvePosY.Text)
        '    .ValvePinPosZ(mAbsValveNo) = Val(txtValvePosZ.Text)
        '    .Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini") '儲存設定
        'End With
        UcJoyStick1.RefreshPosition()
        gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
        gSyslog.Save("Valve Pos X:" & txtValvePosX.Text)
        gSyslog.Save("Valve Pos Y:" & txtValvePosY.Text)
        gSyslog.Save("Valve Pos Z:" & txtValvePosZ.Text)
        gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")
        btnSetValvePosX.Enabled = True
    End Sub

    ''' <summary>移動到閥頭測高位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGo2_Click(sender As Object, e As EventArgs) Handles btnGo2.Click
        gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnGo2]" & vbTab & "Click")
        btnGo2.Enabled = False

        '20170602按鍵保護
        btnSetValvePosX.Enabled = False
        btnSetValve1Z.Enabled = False
        btnRemoveKey.Enabled = False
        btnSetTiltPos.Enabled = False
        btnGoTiltPos.Enabled = False
        btnSetLaserPinPos.Enabled = False
        btnGoLaserPinPos.Enabled = False
        btnLaserSensor.Enabled = False
        btnAutoZFind.Enabled = False
        btnValveToHightPrev.Enabled = False
        btnValveToHightNext.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        UcJoyStick1.Enabled = False


        With gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo)
            'gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, .ValvePinPosX(0), .ValvePinPosY(0), .ValvePinPosZ(0)), "INFO_6019015")
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015,
                                             .LaserPinX(enmValve.No1, Val(cboB.SelectedItem)),
                                             .LaserPinY(enmValve.No1, Val(cboB.SelectedItem)),
                                             .LaserPinZ(enmValve.No1, Val(cboB.SelectedItem)),
                                             "INFO_6019015"))
            Dim AxisNo(4) As Integer
            Dim TargetPos(4) As Decimal
            AxisNo(0) = sys.AxisX
            AxisNo(1) = sys.AxisY
            AxisNo(2) = sys.AxisZ
            AxisNo(3) = sys.AxisB
            AxisNo(4) = sys.AxisC

            '20170807 先移動XY&Tilt
            TargetPos(0) = Val(txtValvePosX.Text) '.ValvePinX(mAbsValveNo, Val(cboB.SelectedItem))
            TargetPos(1) = Val(txtValvePosY.Text) '.ValvePinY(mAbsValveNo, Val(cboB.SelectedItem))
            'TargetPos(2) = Val(txtValvePosZ.Text) '.ValvePinZ(mAbsValveNo, Val(cboB.SelectedItem))
            TargetPos(2) = 0
            'TargetPos(3) = Val(txtTiltValvePosB.Text)
            TargetPos(3) = mTiltCommandValue
            TargetPos(4) = 0

            ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)

            '20170807 變更Z軸位置
            TargetPos(2) = Val(txtValvePosZ.Text)

            '20170807
            If MsgBox("Z Stage Will move down，please check PosZ:" + txtValvePosZ.Text + " is safe", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnGo2_Click]" & vbTab & "Stage in Safe Pos By User Check")
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, TargetPos(0), TargetPos(1), TargetPos(2)), "INFO_6019015")
                ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
            End If

        End With
        UcJoyStick1.RefreshPosition()
        btnGo2.Enabled = True

        '20170602按鍵保護
        btnSetValvePosX.Enabled = True
        btnSetValve1Z.Enabled = True
        btnRemoveKey.Enabled = True
        btnSetTiltPos.Enabled = True
        btnGoTiltPos.Enabled = True
        btnSetLaserPinPos.Enabled = True
        btnGoLaserPinPos.Enabled = True
        btnLaserSensor.Enabled = True
        btnAutoZFind.Enabled = True
        btnValveToHightPrev.Enabled = True
        btnValveToHightNext.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Not sys Is Nothing Then
            If sys.AxisZ >= 0 Then
                If gCMotion.AxisParameter(sys.AxisZ).MotionIOStatus.blnLTC Then
                    palLTC.BackgroundImage = My.Resources.li_08
                    'lblLTC.BackColor = Color.Red
                Else
                    palLTC.BackgroundImage = My.Resources.li_23
                    'lblLTC.BackColor = Color.White
                End If
            End If
        End If
        RefreshUI()
    End Sub

    ''' <summary>設定雷射測高位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSetLaserPinPos_Click(sender As Object, e As EventArgs) Handles btnSetLaserPinPos.Click
        If mTiltCommandValue = -1 Then
            'Tilt 位置錯誤!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080024))
            MsgBox(gMsgHandler.GetMessage(Alarm_2080024), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        ''[Note]:Tilt Axis不用GetPositionValue，改用GetCommandValue(因位數值會跳動，數值以命令數值為準)
        'Dim mCommandPos As Decimal

        gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnSetLaserPinPos]" & vbTab & "Click")
        btnSetLaserPinPos.Enabled = False
        If gSYS(sys.EsysNum).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetLaserPinPos.Enabled = True
            Exit Sub
        End If
        gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
        gSyslog.Save("Laser Pin Pos X:" & txtLaserPosX.Text)
        gSyslog.Save("Laser Pin Pos Y:" & txtLaserPosY.Text)
        gSyslog.Save("Laser Pin Pos Z:" & txtLaserPosZ.Text)
        gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")
        'jimmy 20161006
        txtLaserPosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtLaserPosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtLaserPosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        '0807
        txtValvePosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX)) - (gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, mTiltCommandValue) - gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve))
        txtValvePosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY)) - (gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, mTiltCommandValue) - gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve))

        ''txtTiltValvePosB.Text = Val(gCMotion.GetPositionValue(sys.AxisB))
        'gCMotion.GetCommandValue(sys.AxisB, mCommandPos)
        'txtTiltValvePosB.Text = mCommandPos
        'mTiltCommandValue = mCommandPos
        SaveLaserPinPos()

        'With gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo)
        '    .LaserPinPosX(mAbsValveNo) = txtLaserPosX.Text
        '    .LaserPinPosY(mAbsValveNo) = txtLaserPosY.Text
        '    .LaserPinPosZ(mAbsValveNo) = txtLaserPosZ.Text
        '    .Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini") '儲存設定
        'End With
        UcJoyStick1.RefreshPosition()
        gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
        gSyslog.Save("Laser Pin Pos X:" & txtLaserPosX.Text)
        gSyslog.Save("Laser Pin Pos Y:" & txtLaserPosY.Text)
        gSyslog.Save("Laser Pin Pos Z:" & txtLaserPosZ.Text)
        gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")
        btnSetLaserPinPos.Enabled = True
    End Sub

    ''' <summary>移動到雷射測高位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGoLaserPinPos_Click(sender As Object, e As EventArgs) Handles btnGoLaserPinPos.Click
        'Dim mlight As enmLight                                    '[光源]
        btnGoLaserPinPos.Enabled = False

        '20170602按鍵保護
        btnSetValvePosX.Enabled = False
        btnSetValve1Z.Enabled = False
        btnRemoveKey.Enabled = False
        btnSetTiltPos.Enabled = False
        btnGoTiltPos.Enabled = False
        btnSetLaserPinPos.Enabled = False
        btnGo2.Enabled = False
        btnLaserSensor.Enabled = False
        btnAutoZFind.Enabled = False
        btnValveToHightPrev.Enabled = False
        btnValveToHightNext.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        UcJoyStick1.Enabled = False

        gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnGoLaserPinPos]" & vbTab & "Click")

        ''[Note]:關閉光源
        'mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
        'Call gSysAdapter.SetLightOnOff(mlight, False)
        'mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
        'Call gSysAdapter.SetLightOnOff(mlight, False)

        'Call WriteButtonLog(gUserLevel, "frmCalibrationCCD2Valve1", "btnGo2")
        'jimmy 20161019
        ' With gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo)
        With gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo)
            'gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, .LaserPinPosX(0), .LaserPinPosY(0), .LaserPinPosZ(0)), "INFO_6019015")
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, .LaserPinX(0, Val(cboB.SelectedItem)), .LaserPinY(0, Val(cboB.SelectedItem)), .LaserPinZ(0, Val(cboB.SelectedItem))), "INFO_6019015")
            Dim AxisNo(4) As Integer
            Dim TargetPos(4) As Decimal
            AxisNo(0) = sys.AxisX
            AxisNo(1) = sys.AxisY
            AxisNo(2) = sys.AxisZ
            AxisNo(3) = sys.AxisB
            AxisNo(4) = sys.AxisC

            TargetPos(0) = Val(txtLaserPosX.Text) '.LaserPinX(mAbsValveNo, Val(cboB.SelectedItem))
            TargetPos(1) = Val(txtLaserPosY.Text) '.LaserPinY(mAbsValveNo, Val(cboB.SelectedItem))
            TargetPos(2) = Val(txtLaserPosZ.Text) '.LaserPinZ(mAbsValveNo, Val(cboB.SelectedItem))
            'TargetPos(3) = Val(txtTiltValvePosB.Text) 'Val(cboB.SelectedItem)
            TargetPos(3) = mTiltCommandValue
            TargetPos(4) = 0
            ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        End With
        UcJoyStick1.RefreshPosition()
        btnGoLaserPinPos.Enabled = True

        '20170602按鍵保護
        btnSetValvePosX.Enabled = True
        btnSetValve1Z.Enabled = True
        btnRemoveKey.Enabled = True
        btnSetTiltPos.Enabled = True
        btnGoTiltPos.Enabled = True
        btnSetLaserPinPos.Enabled = True
        btnGo2.Enabled = True
        btnLaserSensor.Enabled = True
        btnAutoZFind.Enabled = True
        btnValveToHightPrev.Enabled = True
        btnValveToHightNext.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True

    End Sub

    ''' <summary>自動測Pin高(雷射,閥頭)</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub BtnAutoZFind_Click(sender As Object, e As EventArgs) Handles btnAutoZFind.Click 'Soni / 2017.05.12 抽成Task.Run & 處理跨執行緒issue
        Dim HeightValue As String = ""
        Dim mVelZ As Decimal

        gSyslog.Save("[frmOpStatus]" & vbTab & "[BtnAutoZFind]" & vbTab & "Click")

        btnAutoZFind.Enabled = False

        '20170602按鍵保護
        btnSetValvePosX.Enabled = False
        btnSetValve1Z.Enabled = False
        btnRemoveKey.Enabled = False
        btnSetTiltPos.Enabled = False
        btnGoLaserPinPos.Enabled = False
        btnSetLaserPinPos.Enabled = False
        btnGo2.Enabled = False
        btnLaserSensor.Enabled = False
        btnGoTiltPos.Enabled = False
        btnValveToHightPrev.Enabled = False
        btnValveToHightNext.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        UcJoyStick1.Enabled = False

        grpValvePos.Enabled = False
        grpLaserPos.Enabled = False
        gbTiltSet.Enabled = False
        UcLightControl1.Enabled = False

        btnAutoZFind.Refresh()

        Select gSSystemParameter.MeasureType
            Case enmMeasureType.Contact '接觸式測高
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                    btnAutoZFind.BackColor = Color.Red
                    Select Case sys.LaserNo
                        Case 1
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014004), "Error_1014004", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 2
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014104), "Error_1014104", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014104), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 3
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014204), "Error_1014204", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014204), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 4
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014304), "Error_1014304", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    btnAutoZFind.Enabled = True
                    btnSetValvePosX.Enabled = True
                    btnSetValve1Z.Enabled = True
                    btnRemoveKey.Enabled = True
                    btnSetTiltPos.Enabled = True
                    btnGoLaserPinPos.Enabled = True
                    btnSetLaserPinPos.Enabled = True
                    btnGo2.Enabled = True
                    btnLaserSensor.Enabled = True
                    btnGoTiltPos.Enabled = True
                    btnValveToHightPrev.Enabled = True
                    btnValveToHightNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True
                    grpValvePos.Enabled = True
                    grpLaserPos.Enabled = True
                    gbTiltSet.Enabled = True
                    UcLightControl1.Enabled = True
                    Exit Sub
                End If
                txtLaserValueZ.Text = HeightValue
                '20170623 測高保護機制
                If HeightValue < 5 Then
                    MsgBox("請將接觸式測高儀校正", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnAutoZFind.Enabled = True
                    btnAutoZFind.Enabled = True
                    btnSetValvePosX.Enabled = True
                    btnSetValve1Z.Enabled = True
                    btnRemoveKey.Enabled = True
                    btnSetTiltPos.Enabled = True
                    btnGoLaserPinPos.Enabled = True
                    btnSetLaserPinPos.Enabled = True
                    btnGo2.Enabled = True
                    btnLaserSensor.Enabled = True
                    btnGoTiltPos.Enabled = True
                    btnValveToHightPrev.Enabled = True
                    btnValveToHightNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True
                    grpValvePos.Enabled = True
                    grpLaserPos.Enabled = True
                    gbTiltSet.Enabled = True
                    UcLightControl1.Enabled = True
                    Exit Sub
                End If
        End Select

        '記錄原始位置
        Dim posX As Decimal = gCMotion.GetPositionValue(sys.AxisX)
        Dim posY As Decimal = gCMotion.GetPositionValue(sys.AxisY)
        Dim posZ As Decimal = gCMotion.GetPositionValue(sys.AxisZ)

        Dim dCurrentZ, dLaserZeroZ As Decimal
        Dim iRetryCount As Integer = 30
        Dim iSearchStep As Integer = 10
        Dim TimeOutStopwatch As New Stopwatch
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            BtnHomeFirstBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
           
            btnAutoZFind.Enabled = True

            '20170602按鍵保護
            btnSetValvePosX.Enabled = True
            btnSetValve1Z.Enabled = True
            btnRemoveKey.Enabled = True
            btnSetTiltPos.Enabled = True
            btnGoLaserPinPos.Enabled = True
            btnSetLaserPinPos.Enabled = True
            btnGo2.Enabled = True
            btnLaserSensor.Enabled = True
            btnGoTiltPos.Enabled = True
            btnValveToHightPrev.Enabled = True
            btnValveToHightNext.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True
            UcJoyStick1.Enabled = True

            grpValvePos.Enabled = True
            grpLaserPos.Enabled = True
            gbTiltSet.Enabled = True
            UcLightControl1.Enabled = True

            Exit Sub
        End If

        btnAutoZFind.BackColor = System.Drawing.Color.Yellow
        btnAutoZFind.Refresh()

        '目標位置
        Dim targetPosX As Decimal
        Dim targetPosY As Decimal
        Dim targetPosZ As Decimal
        Dim targetPosB As Decimal

        targetPosX = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserPinX(sys.SelectValve, Val(cboB.SelectedItem))
        targetPosY = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserPinY(sys.SelectValve, Val(cboB.SelectedItem))
        targetPosZ = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserPinZ(sys.SelectValve, Val(cboB.SelectedItem))
        targetPosB = CDec(Val(cboB.SelectedItem))

        Dim ValvePinX As Decimal = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).ValvePinX(sys.SelectValve, Val(cboB.SelectedItem))
        Dim ValvePinY As Decimal = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).ValvePinY(sys.SelectValve, Val(cboB.SelectedItem))
        Dim ValvePinZ As Decimal = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).ValvePinZ(sys.SelectValve, Val(cboB.SelectedItem))
        Dim LaserPinLimitZ As Decimal = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserPinLimitZ(sys.SelectValve, Val(cboB.SelectedItem))

        Await Task.Run(Sub()

                           '關閉出膠
                           Call SetDispensingTrigger(sys.StageNo, sys.SelectValve, enmONOFF.eOff)
                           gCMotion.SetVelAccDec(sys.AxisX) '設定移動速度
                           gCMotion.SetVelAccDec(sys.AxisY)
                           gCMotion.SetVelAccDec(sys.AxisZ)


                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If

                           '=== Z軸到安全高度 ===
                           If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If

                               'Z 軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1032000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1032000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1044000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1044000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1062000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1062000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1069000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1069000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                               Exit Sub
                           End If


                           TimeOutStopwatch.Restart()
                           Do
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)

                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If

                               If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                   Exit Do
                               ElseIf TimeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If

                                   'Z軸移動Timeout 
                                   Select Case sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1032004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1044004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1062004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1069004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select

                                   Exit Sub
                               End If
                           Loop
                           '=== Z軸到安全高度 ===


                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           'Tilt
                           If gCMotion.AbsMove(sys.AxisB, targetPosB) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If

                               'Z 軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1032000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1032000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1044000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1044000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1062000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1062000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1069000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1069000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select
                               'btnAutoZFind.Enabled = True
                               Exit Sub
                           End If
                           TimeOutStopwatch.Restart()


                           Do
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)
                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If
                               If gCMotion.MotionDone(sys.AxisB) = CommandStatus.Sucessed Then
                                   Exit Do
                               ElseIf TimeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If

                                   'Z軸移動Timeout 
                                   Select Case sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1032004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1044004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1062004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1069004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select

                                   Exit Sub
                               End If
                           Loop



                           '=== XY到雷射測Pin位置 ===
                           'jimmy 20161019
                           'targetPosX = gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo).LaserPinPosX(mAbsValveNo)
                           'targetPosY = gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo).LaserPinPosY(mAbsValveNo)
                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           If gCMotion.AbsMove(sys.AxisX, targetPosX) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If
                               'X軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1030000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1030000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1042000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1042000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1060000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1060000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1067000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1067000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                               Exit Sub
                           End If

                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           If gCMotion.AbsMove(sys.AxisY, targetPosY) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If
                               'Y軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1031000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1031000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1043000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1043000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1061000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1061000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1068000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1068000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                               Exit Sub
                           End If
                           TimeOutStopwatch.Restart()


                           Do
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)
                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If
                               If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) = CommandStatus.Sucessed Then
                                   Exit Do
                               ElseIf TimeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'X & Y 軸移動Timeout
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080010))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2080010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                                   Exit Sub
                               End If
                           Loop
                           '=== XY到雷射測Pin位置 ===

                           '=== Z軸到雷射測Pin位置 ===
                           'jimmy 20161019
                           'targetPosZ = gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo).LaserPinPosZ(mAbsValveNo)

                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           If gCMotion.AbsMove(sys.AxisZ, targetPosZ) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If
                               'Z軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1032000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1032000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1044000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1044000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1062000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1062000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1069000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1069000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                               Exit Sub
                           End If
                           TimeOutStopwatch.Restart()
                           Do
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)

                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If

                               If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                   Exit Do
                               ElseIf TimeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'Z軸移動Timeout 
                                   Select Case sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1032004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1044004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1062004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1069004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select

                                   Exit Sub
                               End If
                           Loop
                           '=== Z軸到雷射測Pin位置 ===

                           'Z軸速度設定
                           gCMotion.SetVelLow(sys.AxisZ, 0)
                           gCMotion.SetVelHigh(sys.AxisZ, 30)
                           gCMotion.SetAcc(sys.AxisZ, 1000)
                           gCMotion.SetDec(sys.AxisZ, 1000)

                           Select Case gSSystemParameter.MeasureType
                               Case enmMeasureType.Contact  '接觸式測高

                                   If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                       Exit Sub
                                   End If
                                   gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, True)
                                   Call gDOCollection.RefreshDO()
                                   System.Threading.Thread.CurrentThread.Join(1000)

                                   If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              btnAutoZFind.Enabled = True
                                                              btnAutoZFind.BackColor = Color.Red

                                                              '20170602按鍵保護
                                                              btnSetValvePosX.Enabled = True
                                                              btnSetValve1Z.Enabled = True
                                                              btnRemoveKey.Enabled = True
                                                              btnSetTiltPos.Enabled = True
                                                              btnGoLaserPinPos.Enabled = True
                                                              btnSetLaserPinPos.Enabled = True
                                                              btnGo2.Enabled = True
                                                              btnLaserSensor.Enabled = True
                                                              btnGoTiltPos.Enabled = True
                                                              btnValveToHightPrev.Enabled = True
                                                              btnValveToHightNext.Enabled = True
                                                              btnOK.Enabled = True
                                                              btnCancel.Enabled = True
                                                              UcJoyStick1.Enabled = True

                                                              grpValvePos.Enabled = True
                                                              grpLaserPos.Enabled = True
                                                              gbTiltSet.Enabled = True
                                                              UcLightControl1.Enabled = True
                                                          End Sub)
                                       End If
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

                                       Exit Sub
                                   End If
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          txtLaserValueZ.Text = HeightValue
                                                      End Sub)
                                   End If


                                   dCurrentZ = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                   UcJoyStick1.SetSpeedType(SpeedType.Fast)
                                   Dim dSearchZ As Double
                                   If Val(HeightValue) < -10 Or Val(HeightValue) > 10 Then

                                       dSearchZ = dCurrentZ
                                       While (dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)
                                           SafeLaserZMove(dSearchZ)
                                           dSearchZ = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                           If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                                               '20170929 Toby_ Add 判斷
                                               If (Not IsNothing(Me)) Then
                                                   Me.BeginInvoke(Sub()
                                                                      btnAutoZFind.Enabled = True
                                                                      btnAutoZFind.BackColor = Color.Red

                                                                      '20170602按鍵保護
                                                                      btnSetValvePosX.Enabled = True
                                                                      btnSetValve1Z.Enabled = True
                                                                      btnRemoveKey.Enabled = True
                                                                      btnSetTiltPos.Enabled = True
                                                                      btnGoLaserPinPos.Enabled = True
                                                                      btnSetLaserPinPos.Enabled = True
                                                                      btnGo2.Enabled = True
                                                                      btnLaserSensor.Enabled = True
                                                                      btnGoTiltPos.Enabled = True
                                                                      btnValveToHightPrev.Enabled = True
                                                                      btnValveToHightNext.Enabled = True
                                                                      btnOK.Enabled = True
                                                                      btnCancel.Enabled = True
                                                                      UcJoyStick1.Enabled = True

                                                                      grpValvePos.Enabled = True
                                                                      grpLaserPos.Enabled = True
                                                                      gbTiltSet.Enabled = True
                                                                      UcLightControl1.Enabled = True
                                                                  End Sub)
                                               End If
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

                                               Exit Sub
                                           End If
                                           txtLaserValueZ.Text = HeightValue

                                           dSearchZ += iSearchStep
                                           iRetryCount -= 1
                                       End While
                                       dSearchZ = dCurrentZ
                                       While (dSearchZ > gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)

                                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                               Exit Sub
                                           End If

                                           SafeLaserZMove(dSearchZ)
                                           dSearchZ = Val(gCMotion.GetPositionValue(sys.AxisZ))

                                           If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                                               '20170929 Toby_ Add 判斷
                                               If (Not IsNothing(Me)) Then
                                                   Me.BeginInvoke(Sub()
                                                                      btnAutoZFind.Enabled = True
                                                                      btnAutoZFind.BackColor = Color.Red

                                                                      '20170602按鍵保護
                                                                      btnSetValvePosX.Enabled = True
                                                                      btnSetValve1Z.Enabled = True
                                                                      btnRemoveKey.Enabled = True
                                                                      btnSetTiltPos.Enabled = True
                                                                      btnGoLaserPinPos.Enabled = True
                                                                      btnSetLaserPinPos.Enabled = True
                                                                      btnGo2.Enabled = True
                                                                      btnLaserSensor.Enabled = True
                                                                      btnGoTiltPos.Enabled = True
                                                                      btnValveToHightPrev.Enabled = True
                                                                      btnValveToHightNext.Enabled = True
                                                                      btnOK.Enabled = True
                                                                      btnCancel.Enabled = True
                                                                      UcJoyStick1.Enabled = True

                                                                      grpValvePos.Enabled = True
                                                                      grpLaserPos.Enabled = True
                                                                      gbTiltSet.Enabled = True
                                                                      UcLightControl1.Enabled = True
                                                                  End Sub)
                                               End If
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

                                               Exit Sub
                                           End If
                                           '20170929 Toby_ Add 判斷
                                           If (Not IsNothing(Me)) Then
                                               Me.BeginInvoke(Sub()
                                                                  txtLaserValueZ.Text = HeightValue
                                                              End Sub)
                                           End If

                                           dSearchZ -= iSearchStep
                                           iRetryCount -= 1
                                       End While
                                   End If

                                   UcJoyStick1.SetSpeedType(SpeedType.Slow)
                                   If iRetryCount <= 0 Then
                                       MsgBox("Auto Z-Find function failed (retry count > 30) !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              btnAutoZFind.Enabled = True
                                                              btnAutoZFind.BackColor = Color.Red

                                                              '20170602按鍵保護
                                                              btnSetValvePosX.Enabled = True
                                                              btnSetValve1Z.Enabled = True
                                                              btnRemoveKey.Enabled = True
                                                              btnSetTiltPos.Enabled = True
                                                              btnGoLaserPinPos.Enabled = True
                                                              btnSetLaserPinPos.Enabled = True
                                                              btnGo2.Enabled = True
                                                              btnLaserSensor.Enabled = True
                                                              btnGoTiltPos.Enabled = True
                                                              btnValveToHightPrev.Enabled = True
                                                              btnValveToHightNext.Enabled = True
                                                              btnOK.Enabled = True
                                                              btnCancel.Enabled = True
                                                              UcJoyStick1.Enabled = True

                                                              grpValvePos.Enabled = True
                                                              grpLaserPos.Enabled = True
                                                              gbTiltSet.Enabled = True
                                                              UcLightControl1.Enabled = True
                                                          End Sub)
                                       End If

                                       Exit Sub
                                   End If
                                   If dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Then                    '
                                       'Z 軸碰到負極限
                                       Select Case sys.StageNo
                                           Case 0
                                               gSyslog.Save(gMsgHandler.GetMessage(Error_1032008))
                                               MsgBox(gMsgHandler.GetMessage(Error_1032008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 1
                                               gSyslog.Save(gMsgHandler.GetMessage(Error_1044008))
                                               MsgBox(gMsgHandler.GetMessage(Error_1044008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 2
                                               gSyslog.Save(gMsgHandler.GetMessage(Error_1062008))
                                               MsgBox(gMsgHandler.GetMessage(Error_1062008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 3
                                               gSyslog.Save(gMsgHandler.GetMessage(Error_1069008))
                                               MsgBox(gMsgHandler.GetMessage(Error_1069008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       End Select
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              btnAutoZFind.Enabled = True
                                                              btnAutoZFind.BackColor = Color.Red

                                                              '20170602按鍵保護
                                                              btnSetValvePosX.Enabled = True
                                                              btnSetValve1Z.Enabled = True
                                                              btnRemoveKey.Enabled = True
                                                              btnSetTiltPos.Enabled = True
                                                              btnGoLaserPinPos.Enabled = True
                                                              btnSetLaserPinPos.Enabled = True
                                                              btnGo2.Enabled = True
                                                              btnLaserSensor.Enabled = True
                                                              btnGoTiltPos.Enabled = True
                                                              btnValveToHightPrev.Enabled = True
                                                              btnValveToHightNext.Enabled = True
                                                              btnOK.Enabled = True
                                                              btnCancel.Enabled = True
                                                              UcJoyStick1.Enabled = True

                                                              grpValvePos.Enabled = True
                                                              grpLaserPos.Enabled = True
                                                              gbTiltSet.Enabled = True
                                                              UcLightControl1.Enabled = True
                                                          End Sub)
                                       End If
                                       Exit Sub
                                   End If


                                   For i As Integer = 0 To 10
                                       If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                                           '20170929 Toby_ Add 判斷
                                           If (Not IsNothing(Me)) Then
                                               Me.BeginInvoke(Sub()
                                                                  btnAutoZFind.Enabled = True
                                                                  btnAutoZFind.BackColor = Color.Red

                                                                  '20170602按鍵保護
                                                                  btnSetValvePosX.Enabled = True
                                                                  btnSetValve1Z.Enabled = True
                                                                  btnRemoveKey.Enabled = True
                                                                  btnSetTiltPos.Enabled = True
                                                                  btnGoLaserPinPos.Enabled = True
                                                                  btnSetLaserPinPos.Enabled = True
                                                                  btnGo2.Enabled = True
                                                                  btnLaserSensor.Enabled = True
                                                                  btnGoTiltPos.Enabled = True
                                                                  btnValveToHightPrev.Enabled = True
                                                                  btnValveToHightNext.Enabled = True
                                                                  btnOK.Enabled = True
                                                                  btnCancel.Enabled = True
                                                                  UcJoyStick1.Enabled = True

                                                                  grpValvePos.Enabled = True
                                                                  grpLaserPos.Enabled = True
                                                                  gbTiltSet.Enabled = True
                                                                  UcLightControl1.Enabled = True
                                                              End Sub)
                                           End If
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

                                           '汽缸動作(收回)
                                           gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
                                           Call gDOCollection.RefreshDO()
                                           '汽缸動作_end

                                           Exit Sub
                                       End If
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              txtLaserValueZ.Text = HeightValue
                                                          End Sub)
                                       End If

                                       dCurrentZ = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                       dLaserZeroZ = dCurrentZ + Val(HeightValue)
                                       If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                           Exit Sub
                                       End If
                                       SafeLaserZMove(dLaserZeroZ)
                                   Next

                                   '汽缸動作(收回)
                                   gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
                                   Call gDOCollection.RefreshDO()
                                   '汽缸動作_end

                               Case enmMeasureType.Laser   'Laser測高

                                   If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              btnAutoZFind.Enabled = True
                                                              btnAutoZFind.BackColor = Color.Red

                                                              '20170602按鍵保護
                                                              btnSetValvePosX.Enabled = True
                                                              btnSetValve1Z.Enabled = True
                                                              btnRemoveKey.Enabled = True
                                                              btnSetTiltPos.Enabled = True
                                                              btnGoLaserPinPos.Enabled = True
                                                              btnSetLaserPinPos.Enabled = True
                                                              btnGo2.Enabled = True
                                                              btnLaserSensor.Enabled = True
                                                              btnGoTiltPos.Enabled = True
                                                              btnValveToHightPrev.Enabled = True
                                                              btnValveToHightNext.Enabled = True
                                                              btnOK.Enabled = True
                                                              btnCancel.Enabled = True
                                                              UcJoyStick1.Enabled = True

                                                              grpValvePos.Enabled = True
                                                              grpLaserPos.Enabled = True
                                                              gbTiltSet.Enabled = True
                                                              UcLightControl1.Enabled = True
                                                          End Sub)
                                       End If
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

                                       Exit Sub
                                   End If
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          txtLaserValueZ.Text = HeightValue
                                                      End Sub)
                                   End If


                                   dCurrentZ = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                   UcJoyStick1.SetSpeedType(SpeedType.Fast)
                                   Dim dSearchZ As Double
                                   If Val(HeightValue) < -10 Or Val(HeightValue) > 10 Then

                                       dSearchZ = dCurrentZ
                                       While (dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)
                                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                               Exit Sub
                                           End If
                                           SafeLaserZMove(dSearchZ)
                                           dSearchZ = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                           If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                                               '20170929 Toby_ Add 判斷
                                               If (Not IsNothing(Me)) Then
                                                   Me.BeginInvoke(Sub()
                                                                      btnAutoZFind.Enabled = True
                                                                      btnAutoZFind.BackColor = Color.Red

                                                                      '20170602按鍵保護
                                                                      btnSetValvePosX.Enabled = True
                                                                      btnSetValve1Z.Enabled = True
                                                                      btnRemoveKey.Enabled = True
                                                                      btnSetTiltPos.Enabled = True
                                                                      btnGoLaserPinPos.Enabled = True
                                                                      btnSetLaserPinPos.Enabled = True
                                                                      btnGo2.Enabled = True
                                                                      btnLaserSensor.Enabled = True
                                                                      btnGoTiltPos.Enabled = True
                                                                      btnValveToHightPrev.Enabled = True
                                                                      btnValveToHightNext.Enabled = True
                                                                      btnOK.Enabled = True
                                                                      btnCancel.Enabled = True
                                                                      UcJoyStick1.Enabled = True

                                                                      grpValvePos.Enabled = True
                                                                      grpLaserPos.Enabled = True
                                                                      gbTiltSet.Enabled = True
                                                                      UcLightControl1.Enabled = True
                                                                  End Sub)
                                               End If
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

                                               Exit Sub
                                           End If
                                           '20170929 Toby_ Add 判斷
                                           If (Not IsNothing(Me)) Then
                                               Me.BeginInvoke(Sub()
                                                                  txtLaserValueZ.Text = HeightValue
                                                              End Sub)
                                           End If


                                           dSearchZ += iSearchStep
                                           iRetryCount -= 1
                                       End While
                                       dSearchZ = dCurrentZ
                                       While (dSearchZ > gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)
                                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                               Exit Sub
                                           End If
                                           SafeLaserZMove(dSearchZ)
                                           dSearchZ = Val(gCMotion.GetPositionValue(sys.AxisZ))

                                           If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                                               '20170929 Toby_ Add 判斷
                                               If (Not IsNothing(Me)) Then
                                                   Me.BeginInvoke(Sub()
                                                                      btnAutoZFind.Enabled = True
                                                                      btnAutoZFind.BackColor = Color.Red

                                                                      '20170602按鍵保護
                                                                      btnSetValvePosX.Enabled = True
                                                                      btnSetValve1Z.Enabled = True
                                                                      btnRemoveKey.Enabled = True
                                                                      btnSetTiltPos.Enabled = True
                                                                      btnGoLaserPinPos.Enabled = True
                                                                      btnSetLaserPinPos.Enabled = True
                                                                      btnGo2.Enabled = True
                                                                      btnLaserSensor.Enabled = True
                                                                      btnGoTiltPos.Enabled = True
                                                                      btnValveToHightPrev.Enabled = True
                                                                      btnValveToHightNext.Enabled = True
                                                                      btnOK.Enabled = True
                                                                      btnCancel.Enabled = True
                                                                      UcJoyStick1.Enabled = True

                                                                      grpValvePos.Enabled = True
                                                                      grpLaserPos.Enabled = True
                                                                      gbTiltSet.Enabled = True
                                                                      UcLightControl1.Enabled = True
                                                                  End Sub)
                                               End If
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

                                               Exit Sub
                                           End If
                                           '20170929 Toby_ Add 判斷
                                           If (Not IsNothing(Me)) Then
                                               Me.BeginInvoke(Sub()
                                                                  txtLaserValueZ.Text = HeightValue
                                                              End Sub)
                                           End If

                                           dSearchZ -= iSearchStep
                                           iRetryCount -= 1
                                       End While
                                   End If

                                   UcJoyStick1.SetSpeedType(SpeedType.Slow)
                                   If iRetryCount <= 0 Then
                                       MsgBox("Auto Z-Find function failed (retry count > 30) !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              btnAutoZFind.Enabled = True
                                                              btnAutoZFind.BackColor = Color.Red

                                                              '20170602按鍵保護
                                                              btnSetValvePosX.Enabled = True
                                                              btnSetValve1Z.Enabled = True
                                                              btnRemoveKey.Enabled = True
                                                              btnSetTiltPos.Enabled = True
                                                              btnGoLaserPinPos.Enabled = True
                                                              btnSetLaserPinPos.Enabled = True
                                                              btnGo2.Enabled = True
                                                              btnLaserSensor.Enabled = True
                                                              btnGoTiltPos.Enabled = True
                                                              btnValveToHightPrev.Enabled = True
                                                              btnValveToHightNext.Enabled = True
                                                              btnOK.Enabled = True
                                                              btnCancel.Enabled = True
                                                              UcJoyStick1.Enabled = True

                                                              grpValvePos.Enabled = True
                                                              grpLaserPos.Enabled = True
                                                              gbTiltSet.Enabled = True
                                                              UcLightControl1.Enabled = True
                                                          End Sub)
                                       End If
                                       Exit Sub
                                   End If
                                   If dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Then
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              btnAutoZFind.Enabled = True
                                                              btnAutoZFind.BackColor = Color.Red

                                                              '20170602按鍵保護
                                                              btnSetValvePosX.Enabled = True
                                                              btnSetValve1Z.Enabled = True
                                                              btnRemoveKey.Enabled = True
                                                              btnSetTiltPos.Enabled = True
                                                              btnGoLaserPinPos.Enabled = True
                                                              btnSetLaserPinPos.Enabled = True
                                                              btnGo2.Enabled = True
                                                              btnLaserSensor.Enabled = True
                                                              btnGoTiltPos.Enabled = True
                                                              btnValveToHightPrev.Enabled = True
                                                              btnValveToHightNext.Enabled = True
                                                              btnOK.Enabled = True
                                                              btnCancel.Enabled = True
                                                              UcJoyStick1.Enabled = True

                                                              grpValvePos.Enabled = True
                                                              grpLaserPos.Enabled = True
                                                              gbTiltSet.Enabled = True
                                                              UcLightControl1.Enabled = True
                                                          End Sub)
                                       End If
                                       'Z 軸碰到負極限
                                       Select Case sys.StageNo
                                           Case 0
                                               gSyslog.Save(gMsgHandler.GetMessage(Error_1032008))
                                               MsgBox(gMsgHandler.GetMessage(Error_1032008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 1
                                               gSyslog.Save(gMsgHandler.GetMessage(Error_1044008))
                                               MsgBox(gMsgHandler.GetMessage(Error_1044008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 2
                                               gSyslog.Save(gMsgHandler.GetMessage(Error_1062008))
                                               MsgBox(gMsgHandler.GetMessage(Error_1062008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 3
                                               gSyslog.Save(gMsgHandler.GetMessage(Error_1069008))
                                               MsgBox(gMsgHandler.GetMessage(Error_1069008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       End Select

                                       Exit Sub
                                   End If

                                   For i As Integer = 0 To 5
                                       If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                                           '20170929 Toby_ Add 判斷
                                           If (Not IsNothing(Me)) Then
                                               Me.BeginInvoke(Sub()
                                                                  btnAutoZFind.Enabled = True
                                                                  btnAutoZFind.BackColor = Color.Red

                                                                  '20170602按鍵保護
                                                                  btnSetValvePosX.Enabled = True
                                                                  btnSetValve1Z.Enabled = True
                                                                  btnRemoveKey.Enabled = True
                                                                  btnSetTiltPos.Enabled = True
                                                                  btnGoLaserPinPos.Enabled = True
                                                                  btnSetLaserPinPos.Enabled = True
                                                                  btnGo2.Enabled = True
                                                                  btnLaserSensor.Enabled = True
                                                                  btnGoTiltPos.Enabled = True
                                                                  btnValveToHightPrev.Enabled = True
                                                                  btnValveToHightNext.Enabled = True
                                                                  btnOK.Enabled = True
                                                                  btnCancel.Enabled = True
                                                                  UcJoyStick1.Enabled = True

                                                                  grpValvePos.Enabled = True
                                                                  grpLaserPos.Enabled = True
                                                                  gbTiltSet.Enabled = True
                                                                  UcLightControl1.Enabled = True
                                                              End Sub)
                                           End If
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

                                           Exit Sub
                                       End If
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              txtLaserValueZ.Text = HeightValue
                                                          End Sub)
                                       End If

                                       dCurrentZ = Val(gCMotion.GetPositionValue(sys.AxisZ))
                                       dLaserZeroZ = dCurrentZ + Val(HeightValue)
                                       If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                           Exit Sub
                                       End If
                                       SafeLaserZMove(dLaserZeroZ)
                                   Next
                           End Select

                           '--- 雷射位置找0點完成 ---
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  txtLaserPosZ.Text = dLaserZeroZ '更新數值 並切換顏色提示 Soni / 2016.08.24 不要再蓋錯了...
                                                  txtLaserPosZ.BackColor = Color.Yellow
                                                  txtLaserPosZ.Refresh()
                                              End Sub)
                           End If

                           System.Threading.Thread.Sleep(300)
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  txtLaserPosZ.BackColor = Color.White
                                                  txtLaserPosZ.Refresh()
                                              End Sub)
                           End If

                           gCMotion.SetVelAccDec(sys.AxisX) '設定移動速度
                           gCMotion.SetVelAccDec(sys.AxisY)
                           gCMotion.SetVelAccDec(sys.AxisZ)

                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           '=== Z軸到0 ===
                           If gCMotion.AbsMove(sys.AxisZ, 0) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If

                               'Z 軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1032000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1032000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1044000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1044000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1062000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1062000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1069000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1069000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                               Exit Sub
                           End If
                           TimeOutStopwatch.Restart()
                           Do
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)
                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If
                               If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                   Exit Do
                               ElseIf TimeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'Z軸移動Timeout 
                                   Select Case sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1032004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1044004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1062004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1069004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select

                                   Exit Sub
                               End If
                           Loop
                           '=== Z軸到0 ===

                           '=== XY到閥測Pin位置 ===
                           'jimmy 20161019
                           'targetPosX = gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo).ValvePinPosX(mAbsValveNo)
                           'targetPosY = gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo).ValvePinPosY(mAbsValveNo)
                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           If gCMotion.AbsMove(sys.AxisX, ValvePinX) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If
                               'X軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1030000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1030000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1042000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1042000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1060000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1060000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1067000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1067000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                               Exit Sub
                           End If
                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           If gCMotion.AbsMove(sys.AxisY, ValvePinY) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If
                               'Y軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1031000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1031000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1043000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1043000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1061000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1061000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1068000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1068000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                               Exit Sub
                           End If
                           TimeOutStopwatch.Restart()
                           Do
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)
                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If
                               If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) Then
                                   Exit Do
                               ElseIf TimeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'X & Y 軸移動Timeout
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080010))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2080010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                                   Exit Sub
                               End If
                           Loop
                           '=== XY到閥測Pin位置 ===


                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           '=== Z軸到閥測Pin位置(往上拉一點安全高度) ===
                           Dim mSafeOffsetZ As Decimal = 3
                           'jimmy 20161019
                           ' targetPosZ = gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo).ValvePinPosZ(mAbsValveNo) + mSafeOffsetZ
                           targetPosZ = ValvePinZ + mSafeOffsetZ
                           If gCMotion.AbsMove(sys.AxisZ, targetPosZ) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If
                               'Z 軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1032000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1032000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1044000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1044000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1062000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1062000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1069000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1069000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                               Exit Sub
                           End If
                           TimeOutStopwatch.Restart()
                           Do
                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)
                               If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                   Exit Do
                               ElseIf TimeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'Z軸移動Timeout 
                                   Select Case sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1032004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1044004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1062004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1069004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select

                                   Exit Sub
                               End If
                           Loop
                           '=== Z軸到閥測Pin位置 ===
                           System.Threading.Thread.Sleep(500) 'Soni + 2017.06.26
                           gCMotion.SetVelLow(sys.AxisZ, 0) '速度設定
                           gCMotion.SetVelHigh(sys.AxisZ, 4)
                           gCMotion.SetAcc(sys.AxisZ, 1)
                           gCMotion.SetDec(sys.AxisZ, 1)
                           gCMotion.ResetLatch(sys.AxisZ) 'LTC設定

                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           '=== 等速往下 ===
                           If gCMotion.VelMove(sys.AxisZ, eDirection.Negative) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If
                               'Z 軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1032000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1032000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1044000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1044000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1062000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1062000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1069000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1069000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                               Exit Sub
                           End If

                           Do
                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)
                               'jimmy 20161019
                               'If gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo).ValvePinLimitZ(mAbsValveNo) <> 0 AndAlso gCMotion.GetPositionValue(sys.AxisZ) < gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo).ValvePinLimitZ(mAbsValveNo) Then
                               If LaserPinLimitZ <> 0 AndAlso
                                   gCMotion.GetPositionValue(sys.AxisZ) < LaserPinLimitZ Then
                                   Call gCMotion.EmgStop(sys.AxisZ)
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'Z 軸碰到負極限
                                   Select Case sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1032008))
                                           MsgBox(gMsgHandler.GetMessage(Error_1032008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1044008))
                                           MsgBox(gMsgHandler.GetMessage(Error_1044008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1062008))
                                           MsgBox(gMsgHandler.GetMessage(Error_1062008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1069008))
                                           MsgBox(gMsgHandler.GetMessage(Error_1069008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select

                                   Exit Sub
                               End If
                               gCMotion.CheckMotorStatus(sys.AxisZ)
                               If gCMotion.AxisParameter(sys.AxisZ).MotionIOStatus.blnLTC = False Then '接觸LTC
                                   Call gCMotion.EmgStop(sys.AxisZ)
                                   Exit Do
                               End If
                           Loop
                           System.Threading.Thread.Sleep(500) 'Soni + 2017.06.26
                           '=== 等待移動停止 ===
                           TimeOutStopwatch.Restart()
                           Do
                               System.Threading.Thread.Sleep(1)
                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If

                               If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                   Exit Do
                               ElseIf TimeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'Z軸移動Timeout 
                                   Select Case sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1032004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1044004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1062004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1069004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select

                                   Exit Sub
                               End If
                           Loop
                           '=== 等待移動停止 ===

                           '=== 往上拉到離開 ===
                           gCMotion.SetVelLow(sys.AxisZ, 0)
                           gCMotion.SetVelHigh(sys.AxisZ, 2)
                           gCMotion.SetAcc(sys.AxisZ, 1)
                           gCMotion.SetDec(sys.AxisZ, 1)
                           gCMotion.ResetLatch(sys.AxisZ)

                           System.Threading.Thread.Sleep(500) 'Soni + 2017.06.26
                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           If gCMotion.VelMove(sys.AxisZ, eDirection.Positive) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If
                               'Z 軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1032000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1032000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1044000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1044000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1062000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1062000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1069000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1069000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                               Exit Sub
                           End If
                           Do
                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)
                               If gCMotion.GetPositionValue(sys.AxisZ) > gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit Then
                                   Call gCMotion.EmgStop(sys.AxisZ)
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'Z 軸碰到正極限
                                   Select Case sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1032007))
                                           MsgBox(gMsgHandler.GetMessage(Error_1032007), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1044007))
                                           MsgBox(gMsgHandler.GetMessage(Error_1044007), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1062007))
                                           MsgBox(gMsgHandler.GetMessage(Error_1062007), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1069007))
                                           MsgBox(gMsgHandler.GetMessage(Error_1069007), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select

                                   Exit Sub
                               End If
                               gCMotion.CheckMotorStatus(sys.AxisZ)
                               If gCMotion.AxisParameter(sys.AxisZ).MotionIOStatus.blnLTC = True Then '高電壓是Off
                                   Call gCMotion.EmgStop(sys.AxisZ)
                                   Exit Do
                               End If
                           Loop
                           '=== 往上拉到離開 ===

                           '=== 等待移動停止 ===
                           TimeOutStopwatch.Restart()
                           Do
                               System.Threading.Thread.Sleep(1)
                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If
                               If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                   Exit Do
                               ElseIf TimeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'Z軸移動Timeout 
                                   Select Case sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1032004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1044004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1062004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1069004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select

                                   Exit Sub
                               End If
                           Loop
                           '=== 等待移動停止 ===

                           System.Threading.Thread.Sleep(500)
                           '=== 第二段找LTC ===
                           gCMotion.SetVelLow(sys.AxisZ, 0)
                           gCMotion.SetVelHigh(sys.AxisZ, 1)
                           gCMotion.SetAcc(sys.AxisZ, 1)
                           gCMotion.SetDec(sys.AxisZ, 1)
                           gCMotion.ResetLatch(sys.AxisZ)
                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           If gCMotion.VelMove(sys.AxisZ, eDirection.Negative) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If
                               'Z 軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1032000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1032000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1044000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1044000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1062000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1062000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1069000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1069000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                           End If

                           Do
                               System.Threading.Thread.Sleep(1)
                               'jimmy 20161019
                               ' If gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo).ValvePinLimitZ(mAbsValveNo) <> 0 AndAlso gCMotion.GetPositionValue(sys.AxisZ) < gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo).ValvePinLimitZ(mAbsValveNo) Then
                               If LaserPinLimitZ <> 0 AndAlso
                                   gCMotion.GetPositionValue(sys.AxisZ) < LaserPinLimitZ Then
                                   Call gCMotion.EmgStop(sys.AxisZ)
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'Z 軸碰到負極限
                                   Select Case sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1032008))
                                           MsgBox(gMsgHandler.GetMessage(Error_1032008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1044008))
                                           MsgBox(gMsgHandler.GetMessage(Error_1044008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1062008))
                                           MsgBox(gMsgHandler.GetMessage(Error_1062008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1069008))
                                           MsgBox(gMsgHandler.GetMessage(Error_1069008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select

                                   Exit Sub
                               End If
                               gCMotion.CheckMotorStatus(sys.AxisZ)
                               If gCMotion.AxisParameter(sys.AxisZ).MotionIOStatus.blnLTC = False Then '接觸LTC
                                   Call gCMotion.EmgStop(sys.AxisZ)
                                   Exit Do
                               End If
                           Loop

                           '=== 第二段找LTC ===

                           '=== 等待移動停止 ===
                           TimeOutStopwatch.Restart()
                           Do
                               System.Threading.Thread.Sleep(1)
                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If
                               If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                   Exit Do
                               ElseIf TimeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'Z軸移動Timeout 
                                   Select Case sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1032004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1044004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1062004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1069004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select

                                   Exit Sub
                               End If
                           Loop

                           mVelZ = gCMotion.GetPositionValue(sys.AxisZ)

                           '=== 等待移動停止 ===
                           gCMotion.SetVelAccDec(sys.AxisX) '設定移動速度
                           gCMotion.SetVelAccDec(sys.AxisY)
                           gCMotion.SetVelAccDec(sys.AxisZ)

                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           '=== Z軸到安全高度 ===
                           If gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If
                               'Z 軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1032000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1032000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1044000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1044000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1062000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1062000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1069000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1069000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                               Exit Sub
                           End If
                           TimeOutStopwatch.Restart()
                           Do
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)
                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If
                               If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                   Exit Do
                               ElseIf TimeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'Z軸移動Timeout 
                                   Select Case sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1032004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1044004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1062004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1069004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select

                                   Exit Sub
                               End If
                           Loop
                           '=== Z軸到安全高度 ===

                           '=== 取得Latch位置 ===
                           Dim latchPos As Decimal


                           Call gCMotion.GetLatchPosition(sys.AxisZ, enmPositionType.ActualPosition, latchPos)

                           Debug.Print("LatchPosition = " & latchPos & " Encoder Z = " & mVelZ)

                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  txtValvePosZ.Text = latchPos
                                                  txtValvePosZ.BackColor = Color.Yellow
                                                  txtValvePosZ.Refresh()
                                              End Sub)
                           End If

                           System.Threading.Thread.Sleep(300)
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  txtValvePosZ.BackColor = Color.White
                                                  txtValvePosZ.Refresh()
                                              End Sub)
                           End If

                           '=== 取得Latch位置 ===

                           '=== XY到起始位置 ===
                           targetPosX = posX
                           targetPosY = posY

                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If

                           If gCMotion.AbsMove(sys.AxisX, targetPosX) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If
                               'X軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1030000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1030000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1042000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1042000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1060000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1060000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1067000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1067000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                               Exit Sub
                           End If

                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           If gCMotion.AbsMove(sys.AxisY, targetPosY) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If
                               'Y軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1031000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1031000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1043000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1043000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1061000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1061000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1068000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1068000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select
                               Exit Sub
                           End If
                           TimeOutStopwatch.Restart()
                           Do
                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)
                               If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) Then
                                   Exit Do
                               ElseIf TimeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'X & Y 軸移動Timeout
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080010))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2080010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                                   Exit Sub
                               End If
                           Loop
                           '=== XY到起始位置 ===

                           '=== Z軸到起始位置 ===
                           If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                               Exit Sub
                           End If
                           targetPosZ = posZ
                           If gCMotion.AbsMove(sys.AxisZ, targetPosZ) <> CommandStatus.Sucessed Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnAutoZFind.Enabled = True
                                                      btnAutoZFind.BackColor = Color.Red

                                                      '20170602按鍵保護
                                                      btnSetValvePosX.Enabled = True
                                                      btnSetValve1Z.Enabled = True
                                                      btnRemoveKey.Enabled = True
                                                      btnSetTiltPos.Enabled = True
                                                      btnGoLaserPinPos.Enabled = True
                                                      btnSetLaserPinPos.Enabled = True
                                                      btnGo2.Enabled = True
                                                      btnLaserSensor.Enabled = True
                                                      btnGoTiltPos.Enabled = True
                                                      btnValveToHightPrev.Enabled = True
                                                      btnValveToHightNext.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                      UcJoyStick1.Enabled = True

                                                      grpValvePos.Enabled = True
                                                      grpLaserPos.Enabled = True
                                                      gbTiltSet.Enabled = True
                                                      UcLightControl1.Enabled = True
                                                  End Sub)
                               End If
                               'Z 軸移動失敗
                               Select Case sys.StageNo
                                   Case 0
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1032000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1032000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 1
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1044000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1044000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 2
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1062000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1062000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Case 3
                                       gSyslog.Save(gMsgHandler.GetMessage(Error_1069000))
                                       MsgBox(gMsgHandler.GetMessage(Error_1069000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               End Select

                               Exit Sub
                           End If
                           TimeOutStopwatch.Restart()
                           Do
                               If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
                                   Exit Do
                               End If
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)
                               If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                   Exit Do
                               ElseIf TimeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnAutoZFind.Enabled = True
                                                          btnAutoZFind.BackColor = Color.Red

                                                          '20170602按鍵保護
                                                          btnSetValvePosX.Enabled = True
                                                          btnSetValve1Z.Enabled = True
                                                          btnRemoveKey.Enabled = True
                                                          btnSetTiltPos.Enabled = True
                                                          btnGoLaserPinPos.Enabled = True
                                                          btnSetLaserPinPos.Enabled = True
                                                          btnGo2.Enabled = True
                                                          btnLaserSensor.Enabled = True
                                                          btnGoTiltPos.Enabled = True
                                                          btnValveToHightPrev.Enabled = True
                                                          btnValveToHightNext.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True
                                                          UcJoyStick1.Enabled = True

                                                          grpValvePos.Enabled = True
                                                          grpLaserPos.Enabled = True
                                                          gbTiltSet.Enabled = True
                                                          UcLightControl1.Enabled = True
                                                      End Sub)
                                   End If
                                   'Z軸移動Timeout 
                                   Select Case sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1032004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1044004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1062004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Error_1069004))
                                           MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select

                                   Exit Sub
                               End If
                           Loop
                           '=== Z軸到起始位置 ===

                           UcJoyStick1.RefreshPosition()
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  btnAutoZFind.BackColor = System.Drawing.SystemColors.Control
                                                  btnAutoZFind.Enabled = True

                                                  '20170602按鍵保護
                                                  btnSetValvePosX.Enabled = True
                                                  btnSetValve1Z.Enabled = True
                                                  btnRemoveKey.Enabled = True
                                                  btnSetTiltPos.Enabled = True
                                                  btnGoLaserPinPos.Enabled = True
                                                  btnSetLaserPinPos.Enabled = True
                                                  btnGo2.Enabled = True
                                                  btnLaserSensor.Enabled = True
                                                  btnGoTiltPos.Enabled = True
                                                  btnValveToHightPrev.Enabled = True
                                                  btnValveToHightNext.Enabled = True
                                                  btnOK.Enabled = True
                                                  btnCancel.Enabled = True
                                                  UcJoyStick1.Enabled = True

                                                  grpValvePos.Enabled = True
                                                  grpLaserPos.Enabled = True
                                                  gbTiltSet.Enabled = True
                                                  UcLightControl1.Enabled = True
                                              End Sub)
                           End If

                       End Sub)

    End Sub

    ''' <summary>Z軸安全移動操作</summary>
    ''' <param name="TargetPos"></param>
    ''' <remarks></remarks>
    Private Sub SafeLaserZMove(ByVal TargetPos As Decimal)
        Dim mStopWatch As New Stopwatch

        If gCMotion.AbsMove(sys.AxisZ, TargetPos) <> CommandStatus.Sucessed Then
            'Z 軸移動失敗
            Select Case sys.StageNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1032000))
                    MsgBox(gMsgHandler.GetMessage(Error_1032000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1044000))
                    MsgBox(gMsgHandler.GetMessage(Error_1044000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 2
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1062000))
                    MsgBox(gMsgHandler.GetMessage(Error_1062000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 3
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1069000))
                    MsgBox(gMsgHandler.GetMessage(Error_1069000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            Exit Sub
        End If

        'gCMotion.AbsMove(sys.AxisZ, TargetPos) '移至targetPos
        System.Threading.Thread.CurrentThread.Join(200) '移動命令下達後不能立刻看到位


        mStopWatch.Restart()
        Do
            'Application.DoEvents()
            System.Threading.Thread.Sleep(1)
            If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then '到位 離開等待迴圈
                Exit Do
            End If
            If sys.AxisZ > -1 Then
                If gCMotion.IsMoveTimeOut(sys.AxisZ) Then '逾時 中斷離開
                    'Z軸移動Timeout 
                    Select Case sys.StageNo
                        Case 0
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1032004))
                            MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 1
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1044004))
                            MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 2
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1062004))
                            MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 3
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1069004))
                            MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    Exit Do
                    Exit Sub
                End If
            End If

            If mStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut4 Then
                'Z軸移動Timeout 
                Select Case sys.StageNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1032004))
                        MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1044004))
                        MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1062004))
                        MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1069004))
                        MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
                Exit Do
                Exit Sub
            End If

        Loop

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnCancel]" & vbTab & "Click")
        '20170623 離開時Z 軸升至安全高度
        gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.WeightCalibration(sys.StageNo).SafePosZ(sys.SelectValve))
        '20171108
        If sys.SelectValve = eValveWorkMode.Valve2 Then
            ValveCylinderAction(sys.StageNo, sys.SelectValve, enmUpDown.Up) '汽缸上
        End If
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnOK]" & vbTab & "Click")
        btnOK.Enabled = False

        If cboB.SelectedIndex <> -1 Then
            Dim sTemp As SLaserTiltValveCalibration = New SLaserTiltValveCalibration

            sTemp.ValvePinPosX = CDec(Val(txtValvePosX.Text))
            sTemp.ValvePinPosY = CDec(Val(txtValvePosY.Text))
            sTemp.ValvePinPosZ = CDec(Val(txtValvePosZ.Text))

            sTemp.LaserPinValue = CDec(Val(txtLaserValueZ.Text))
            sTemp.LaserPinPosX = CDec(Val(txtLaserPosX.Text))
            sTemp.LaserPinPosY = CDec(Val(txtLaserPosY.Text))
            sTemp.LaserPinPosZ = CDec(Val(txtLaserPosZ.Text))
            gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).DicLaserTiltValve(sys.SelectValve)(mTiltCommandValue) = sTemp

            '--------------------------------------------------------------------------------------------------------------------------------------------------------------------
            ' ''[說明]:同Stage不同閥不需校正兩次Valve Height Sensor 位置   20170810 
            'Dim sTemp1 As SLaserTiltValveCalibration = New SLaserTiltValveCalibration
            'For i = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
            '    sTemp1 = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).DicLaserTiltValve(i)(mTiltCommandValue)
            '    If i = sys.SelectValve Then
            '        sTemp1.ValvePinPosX = CDec(Val(txtValvePosX.Text))
            '        sTemp1.ValvePinPosY = CDec(Val(txtValvePosY.Text))
            '        sTemp1.ValvePinPosZ = CDec(Val(txtValvePosZ.Text))
            '    End If
            '    sTemp1.LaserPinValue = CDec(Val(txtLaserValueZ.Text))
            '    sTemp1.LaserPinPosX = CDec(Val(txtLaserPosX.Text))
            '    sTemp1.LaserPinPosY = CDec(Val(txtLaserPosY.Text))
            '    sTemp1.LaserPinPosZ = CDec(Val(txtLaserPosZ.Text))
            '    gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).DicLaserTiltValve(i)(mTiltCommandValue) = sTemp1
            'Next
            '--------------------------------------------------------------------------------------------------------------------------------------------------------------------

            With gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo)
                gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).SaveCalibrationValve(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini")
            End With
        End If
        'With gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo)
        '    .ValvePinPosX(mAbsValveNo) = Val(txtValvePosX.Text)
        '    .ValvePinPosY(mAbsValveNo) = Val(txtValvePosY.Text)
        '    .ValvePinPosZ(mAbsValveNo) = Val(txtValvePosZ.Text)

        '    .LaserPinPosX(mAbsValveNo) = Val(txtLaserPosX.Text)
        '    .LaserPinPosY(mAbsValveNo) = Val(txtLaserPosY.Text)
        '    .LaserPinPosZ(mAbsValveNo) = Val(txtLaserPosZ.Text)
        '    .LaserPinValue(mAbsValveNo) = Val(txtLaserValueZ.Text)

        '    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002066, .ValvePinPosX(mAbsValveNo), .ValvePinPosY(mAbsValveNo), .ValvePinPosZ(mAbsValveNo), .ValvePinLimitZ(mAbsValveNo)), "INFO_6002066")
        '    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002069, .ValvePinPosX(enmValve.No2), .ValvePinPosY(enmValve.No2), .ValvePinPosZ(enmValve.No2), .ValvePinLimitZ(enmValve.No2)), "INFO_6002069") ' & " Standard: " & .dblStandardPinNo2ZPos)
        'End With


        'gSSystemParameter.Pos.LaserValveCalibration(sys.StageNo).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini")

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


        btnOK.Enabled = True

        If CalibrationEnable = True Then
            '存檔成功 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        ElseIf CalibrationEnable = False Then
            '存檔成功 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
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

    Private Sub btnRemoveKey_Click(sender As Object, e As EventArgs) Handles btnRemoveKey.Click
        gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnRemoveKey]" & vbTab & "Click")
        With gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo)
            .ReMoveLaserTiltValve(sys.SelectValve, mTiltCommandValue)
            '.ReMoveLaserTiltValve(mAbsValveNo, CDec(Val(txtTiltValvePosB.Text)))
            ' .SaveCalibrationValve(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini") '儲存設定
        End With
        Call RemoveComB()
    End Sub
    Public Sub UpdateComboxB()
        cboB.Items.Clear()
        'gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LoadCalibrationValve(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini")
        For i As Integer = 0 To gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).DicLaserTiltValve(sys.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
            cboB.Items.Add(gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).DicLaserTiltValve(sys.SelectValve).Keys(i))
        Next
    End Sub
    Public Sub RemoveComB()
        cboB.Items.Clear()
        For i As Integer = 0 To gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).DicLaserTiltValve(sys.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
            cboB.Items.Add(gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).DicLaserTiltValve(sys.SelectValve).Keys(i))
        Next
        If cboB.Items.Count > 0 Then
            cboB.SelectedIndex = 0
        End If
    End Sub
    Private Sub cboB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboB.SelectedIndexChanged
        txtTiltValvePosB.Text = cboB.Text.ToString
        mTiltCommandValue = CDec(txtTiltValvePosB.Text)
        Dim sTemp As SLaserTiltValveCalibration = New SLaserTiltValveCalibration
        'sTemp = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).DicLaserTiltValve(mAbsValveNo)(CDec(Val(txtTiltValvePosB.Text)))
        sTemp = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).DicLaserTiltValve(sys.SelectValve)(mTiltCommandValue)
        txtLaserPosX.Text = sTemp.LaserPinPosX
        txtLaserPosY.Text = sTemp.LaserPinPosY
        txtLaserPosZ.Text = sTemp.LaserPinPosZ
        txtValvePosX.Text = sTemp.ValvePinPosX
        txtValvePosY.Text = sTemp.ValvePinPosY
        txtValvePosZ.Text = sTemp.ValvePinPosZ

        lbConvertTiltAngle.Text = ConverTiltAngle(CDbl(mTiltCommandValue), 3).ToString("##.###")
    End Sub
    Public Sub SaveTiltValvePinPos()

        '[Note]:數值統一記錄在記憶體(mTiltCommandValue)
        Dim sTemp As SLaserTiltValveCalibration = New SLaserTiltValveCalibration
        sTemp.ValvePinPosX = CDec(Val(txtValvePosX.Text))
        sTemp.ValvePinPosY = CDec(Val(txtValvePosY.Text))
        sTemp.ValvePinPosZ = CDec(Val(txtValvePosZ.Text))
        sTemp.LaserPinPosX = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserPinX(sys.SelectValve, mTiltCommandValue) 'Soni / 2016.12.27 警告:問題復發一次
        sTemp.LaserPinPosY = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserPinY(sys.SelectValve, mTiltCommandValue) 'Soni / 2016.12.27 警告:問題復發一次
        sTemp.LaserPinPosZ = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserPinZ(sys.SelectValve, mTiltCommandValue) 'Soni / 2016.12.27 警告:問題復發一次
        sTemp.LaserPinValue = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserPinLimitZ(sys.SelectValve, mTiltCommandValue) 'Soni / 2016.12.27 警告:問題復發一次
        With gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo)
            .ReMoveLaserTiltValve(sys.SelectValve, Val(txtTiltValvePosB.Text))
            .ADDLaserTiltValve(sys.SelectValve, mTiltCommandValue, sTemp)
        End With

    End Sub
    Public Sub SaveLaserPinPos()
        '[Note]:數值統一記錄在記憶體(mTiltCommandValue)
        Dim sTemp As SLaserTiltValveCalibration = New SLaserTiltValveCalibration
        'sTemp.ValvePinPosX = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).ValvePinX(sys.SelectValve, mTiltCommandValue)
        'sTemp.ValvePinPosY = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).ValvePinY(sys.SelectValve, mTiltCommandValue)
        sTemp.ValvePinPosX = Val(txtValvePosX.Text)
        sTemp.ValvePinPosY = Val(txtValvePosY.Text)
        sTemp.ValvePinPosZ = gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).ValvePinZ(sys.SelectValve, mTiltCommandValue)
        sTemp.LaserPinPosX = Val(txtLaserPosX.Text)
        sTemp.LaserPinPosY = Val(txtLaserPosY.Text)
        sTemp.LaserPinPosZ = Val(txtLaserPosZ.Text)
        With gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo)
            .ReMoveLaserTiltValve(sys.SelectValve, mTiltCommandValue)
            .ADDLaserTiltValve(sys.SelectValve, mTiltCommandValue, sTemp)
        End With
    End Sub
    Public Sub SaveTiltPos()

        Dim sTemp As SLaserTiltValveCalibration = New SLaserTiltValveCalibration
        sTemp.ValvePinPosX = CDec(Val(txtValvePosX.Text))
        sTemp.ValvePinPosY = CDec(Val(txtValvePosY.Text))
        sTemp.ValvePinPosZ = CDec(Val(txtValvePosZ.Text))
        sTemp.LaserPinPosX = CDec(Val(txtLaserPosX.Text))
        sTemp.LaserPinPosY = CDec(Val(txtLaserPosY.Text))
        sTemp.LaserPinPosZ = CDec(Val(txtLaserPosZ.Text))
        With gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo)
            .ReMoveLaserTiltValve(sys.SelectValve, mTiltCommandValue)
            .ADDLaserTiltValve(sys.SelectValve, mTiltCommandValue, sTemp)
        End With

        '[Note]:只有在做閥的校正時候可以UpDate
        Call UpdateComboxB()
        '[Note]:將SelectInedex指回去
        cboB.SelectedItem = mTiltCommandValue
    End Sub

    Private Sub btnSetTiltPos_Click(sender As Object, e As EventArgs) Handles btnSetTiltPos.Click
        gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnSetTiltPos]" & vbTab & "Click")
        '[Note]: Tilt(Axis不用GetPositionValue, 改用GetCommandValue(因位數值會跳動, 數值以命令數值為準))
        ' Dim mCommandPos As Decimal
        '[Note]:Tilt Axis不用GetPositionValue，改用GetCommandValue(因位數值會跳動，數值以命令數值為準)
        'gCMotion.GetCommandValue(sys.AxisB, mCommandPos)
        'txtTiltValvePosB.Text = mCommandPos
        'mTiltCommandValue = mCommandPos
        mTiltCommandValue = txtTiltValvePosB.Text
        SaveTiltPos()
    End Sub

    Private Sub btnGoTiltPos_Click(sender As Object, e As EventArgs) Handles btnGoTiltPos.Click
        gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnGoTilt]" & vbTab & "Click")

        btnGoTiltPos.Enabled = False

        '20170602按鍵保護
        btnSetValvePosX.Enabled = False
        btnSetValve1Z.Enabled = False
        btnRemoveKey.Enabled = False
        btnSetTiltPos.Enabled = False
        btnGoLaserPinPos.Enabled = False
        btnSetLaserPinPos.Enabled = False
        btnGo2.Enabled = False
        btnLaserSensor.Enabled = False
        btnAutoZFind.Enabled = False
        btnValveToHightPrev.Enabled = False
        btnValveToHightNext.Enabled = False
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
        btnGoTiltPos.Enabled = True
        '20170602按鍵保護
        btnSetValvePosX.Enabled = True
        btnSetValve1Z.Enabled = True
        btnRemoveKey.Enabled = True
        btnSetTiltPos.Enabled = True
        btnGoLaserPinPos.Enabled = True
        btnSetLaserPinPos.Enabled = True
        btnGo2.Enabled = True
        btnLaserSensor.Enabled = True
        btnAutoZFind.Enabled = True
        btnValveToHightPrev.Enabled = True
        btnValveToHightNext.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True

    End Sub



    Private Sub btnValveToHightPrev_Click_1(sender As Object, e As EventArgs) Handles btnValveToHightPrev.Click
        gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()


        If gfrmCalibrationCCD2Laser Is Nothing Then
            gfrmCalibrationCCD2Laser = New frmCalibrationCCD2Height
        ElseIf gfrmCalibrationCCD2Laser.IsDisposed Then
            gfrmCalibrationCCD2Laser = New frmCalibrationCCD2Height
        End If
        ' Dim mfrmCalibrationCCD1Laser = New frmCalibrationCCD2Height
        With gfrmCalibrationCCD2Laser
            .sys = gSYS(sys.EsysNum)
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            '.CalibrationEnable = True
            ._oLastSelectCobValve = cboB.SelectedItem
            .ShowDialog()
        End With

    End Sub

    Private Sub btnValveToHightNext_Click_1(sender As Object, e As EventArgs) Handles btnValveToHightNext.Click
        gSyslog.Save("[frmCalibrationZHeight]" & vbTab & "[btnCancel]" & vbTab & "Click")
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

End Class

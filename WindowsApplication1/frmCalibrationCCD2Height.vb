Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectAOI
Imports ProjectLaserInterferometer

Public Class frmCalibrationCCD2Height
    ''' <summary>目前類別名稱</summary>
    ''' <remarks></remarks>
    Private Const ClassName As String = "frmCalibrationCCD2Height"

    Private Const CalibSceneName As String = "CALIB"
    ' Public CalibrationEnable As Boolean

    Private oLastSelectCobValve As Object
    WriteOnly Property _oLastSelectCobValve() As Object
        'Get
        '    Return slastSelectCobValve
        'End Get
        Set(ByVal value As Object)
            oLastSelectCobValve = value
        End Set
    End Property



    ''' <summary>對外</summary>
    ''' <remarks></remarks>
    Public sys As sSysParam

    ''' <summary>介面更新</summary>
    ''' <remarks></remarks>
    Public Sub RefreshUI()
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
            btnAutoZero.Enabled = False
            btnGoCCDPos.Enabled = False
            btnGoSensorPos.Enabled = False
            btnSetCcdPos.Enabled = False
            btnSetHightSensorPos.Enabled = False
            btnReZero.Enabled = False
        Else
            btnAutoZero.Enabled = True
            btnGoCCDPos.Enabled = True
            btnGoSensorPos.Enabled = True
            btnSetCcdPos.Enabled = True
            btnSetHightSensorPos.Enabled = True
            btnReZero.Enabled = True
        End If

        'Re-Zero 按鍵保護,若為laser測高則隱藏
        Select Case gSSystemParameter.MeasureType
            Case enmMeasureType.Laser
                btnReZero.Visible = False
        End Select


        Select Case gUserLevel
            Case enmUserLevel.eEngineer
                btnReZero.Visible = False
            Case enmUserLevel.eOperator
                btnReZero.Visible = False
        End Select

    End Sub
    ''' <summary>設定CCD位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSetCcdPos_Click(sender As Object, e As EventArgs) Handles btnSetCcdPos.Click

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
        btnSetCcdPos.Enabled = True
    End Sub

    ''' <summary>設定測高位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSetHightSensorPos_Click(sender As Object, e As EventArgs) Handles btnSetHightSensorPos.Click
        btnSetHightSensorPos.Enabled = False
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnSetHightSensorPos]" & vbTab & "Click")
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetHightSensorPos.Enabled = True
            Exit Sub
        End If

        UcJoyStick1.RefreshPosition()
        txtLaserPosX.Text = Val(gCMotion.GetPositionValue(sys.AxisX))
        txtLaserPosY.Text = Val(gCMotion.GetPositionValue(sys.AxisY))
        txtLaserPosZ.Text = Val(gCMotion.GetPositionValue(sys.AxisZ))
        btnSetHightSensorPos.Enabled = True

    End Sub

    ''' <summary>關閉表單</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCalibrationCCD2Height_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        '20170215
        'Eason 20170120 Ticket:100030 , Memory Freed [S]
        'UcDisplay1.EndLive()
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
    End Sub

    Private Sub frmCalibrationCCD2Height_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        'gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.WeightCalibration(sys.StageNo).SafePosZ(sys.SelectValve))

        ValveCylinderAction(sys.StageNo, sys.SelectValve, enmUpDown.Down)

        Timer1.Enabled = False
        UcDisplay1.EndLive()
        UcDisplay1.ManualDispose()
        UcLightControl1.ManualDispose()
        UcJoyStick1.ManualDispose()
        UcStatusBar1.ManualDispose()
        Me.Dispose(True)
        GC.Collect() 'Eason 20170120 Ticket:100030 , Memory Freed , Be careful Use it .


        'UcDisplay1.EndLive()
        'UcLightControl1.ManualDispose()
        'UcDisplay1.MyDispose()
        'UcJoyStick1.MyDispose()
        'UcStatusBar1.MyDispose()
        'Me.Dispose(True)
        'GC.Collect()
    End Sub

    ''' <summary>載入表單</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCalibrationCCD2Height_Load(sender As Object, e As EventArgs) Handles Me.Load
        '20170327
        Me.Text = "Calibration" & " Stage" & (sys.EsysNum - eSys.DispStage1 + 1) & " CCD & Valve" & sys.SelectValve + 1 & " Height"



        Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height
        With gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo)
            .Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini")
            '[Note]:使用該組Stage的第一組Valve  20170321  此部分參數帶入不寫死
            'sys.SelectValve = eValveWorkMode.Valve1

            txtCCDPosX.Text = .CCDCalibPosX(sys.SelectValve)
            txtCCDPosY.Text = .CCDCalibPosY(sys.SelectValve)
            txtCCDPosZ.Text = .CCDCalibPosZ(sys.SelectValve)

            txtLaserPosX.Text = .LaserCalibPosX(sys.SelectValve)
            txtLaserPosY.Text = .LaserCalibPosY(sys.SelectValve)
            txtLaserPosZ.Text = .LaserCalibPosZ(sys.SelectValve)

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
        gAOICollection.LoadSceneParameter(CalibSceneName & (sys.CCDNo + 1).ToString, fileName) '讀取光源,曝光值等設定
        'If gAOICollection.SceneDictionary.ContainsKey(CalibSceneName & (sys.CCDNo + 1).ToString) Then
        '    nmcExposure.Value = gAOICollection.SceneDictionary(CalibSceneName & (sys.CCDNo + 1).ToString).CCDExposureTime
        'Else
        '    nmcExposure.Value = 5
        'End If
        '--- 2016.06.18 + 開燈,曝光,觸發後才會改 ---
        UcLightControl1.CCDNo = sys.CCDNo
        If gAOICollection.SceneDictionary.ContainsKey(CalibSceneName & (sys.CCDNo + 1).ToString) Then
            UcLightControl1.SceneName = CalibSceneName & (sys.CCDNo + 1).ToString
            UcLightControl1.ShowUI()
            SelectScene(CalibSceneName & (sys.CCDNo + 1).ToString) '場景開光
        End If
        If gAOICollection.IsSceneExist(sys.CCDNo, CalibSceneName & (sys.CCDNo + 1).ToString) Then
            gAOICollection.SetCCDScene(sys.CCDNo, CalibSceneName & (sys.CCDNo + 1).ToString) '曝光,亮度
        End If

        ''TODO:已無曝光切換射計, Form_Load觸發可刪除
        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        'System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照
        'System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        ''--- 2016.06.18 + 開燈,曝光,觸發後才會改 ---

        ValveCylinderAction(sys.StageNo, eValveWorkMode.Valve2, enmUpDown.Up)

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

        '  If CalibrationEnable = True Then
        '     btnCCDToHightPrev.Visible = True
        '   btnCCDToHightNext.Visible = True
        ' ElseIf CalibrationEnable = False Then
        'btnCCDToHightPrev.Visible = False
        '   btnCCDToHightNext.Visible = False
        '  End If

        '--- 避免當機,放最後 ---
        Timer1.Enabled = True
        Call Timer1_Tick(sender, e)
        RefreshUI() '介面更新
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
    Private Sub btnGoCCDPos_Click(sender As Object, e As EventArgs) Handles btnGoCCDPos.Click
        '20170602按鍵保護
        If btnGoCCDPos.Enabled = False Then
            Exit Sub
        End If

        Dim mlight As enmLight                                    '[光源]

        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnGoCCDPos]" & vbTab & "Click")
        btnGoCCDPos.Enabled = False

        '20170602按鍵保護
        btnSetCcdPos.Enabled = False
        btnSetHightSensorPos.Enabled = False
        btnGoSensorPos.Enabled = False
        btnAutoZero.Enabled = False
        btnReZero.Enabled = False
        btnCCDToHightPrev.Enabled = False
        btnCCDToHightNext.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
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

        'Call WriteButtonLog(gUserLevel, "frmCalibrationCCD2Valve1", "btnGo1")
        With gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo)
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6001052, .CCDCalibPosX(sys.SelectValve), .CCDCalibPosY(sys.SelectValve), .CCDCalibPosZ(sys.SelectValve)), "INFO_6001052")
            Dim AxisNo(4) As Integer
            Dim TargetPos(4) As Decimal
            AxisNo(0) = sys.AxisX
            AxisNo(1) = sys.AxisY
            AxisNo(2) = sys.AxisZ
            AxisNo(3) = -1
            AxisNo(4) = sys.AxisC

            TargetPos(0) = txtCCDPosX.Text
            TargetPos(1) = txtCCDPosY.Text
            TargetPos(2) = txtCCDPosZ.Text
            TargetPos(3) = 0
            TargetPos(4) = 0
            ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        End With
        UcJoyStick1.RefreshPosition()
        btnGoCCDPos.Enabled = True

        '20170602按鍵保護
        btnSetCcdPos.Enabled = True
        btnSetHightSensorPos.Enabled = True
        btnGoSensorPos.Enabled = True
        btnAutoZero.Enabled = True
        btnReZero.Enabled = True
        btnCCDToHightPrev.Enabled = True
        btnCCDToHightNext.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True

    End Sub

    ''' <summary>移動到測高位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGoSensorPos_Click(sender As Object, e As EventArgs) Handles btnGoSensorPos.Click
        '20170602按鍵保護
        If btnGoSensorPos.Enabled = False Then
            Exit Sub
        End If

        CType(sender, Button).Tag = "PassTiltAction"
        Dim mlight As enmLight                                    '[光源]
        btnGoSensorPos.Enabled = False

        '20170602按鍵保護
        btnSetCcdPos.Enabled = False
        btnSetHightSensorPos.Enabled = False
        btnGoCCDPos.Enabled = False
        btnAutoZero.Enabled = False
        btnReZero.Enabled = False
        btnCCDToHightPrev.Enabled = False
        btnCCDToHightNext.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        UcJoyStick1.Enabled = False

        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnGoSensorPos]" & vbTab & "Click")
        '[Note]:關閉光源
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
        Call gSysAdapter.SetLightOnOff(mlight, False)
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
        Call gSysAdapter.SetLightOnOff(mlight, False)
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3)
        Call gSysAdapter.SetLightOnOff(mlight, False)
        mlight = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4)
        Call gSysAdapter.SetLightOnOff(mlight, False)

        With gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo)
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, .LaserCalibPosX(sys.SelectValve), .LaserCalibPosY(sys.SelectValve), .LaserCalibPosZ(sys.SelectValve)), "INFO_6019015")
            Dim AxisNo(4) As Integer
            Dim TargetPos(4) As Decimal
            AxisNo(0) = sys.AxisX
            AxisNo(1) = sys.AxisY
            AxisNo(2) = sys.AxisZ
            AxisNo(3) = sys.AxisB
            AxisNo(4) = sys.AxisC

            TargetPos(0) = txtLaserPosX.Text
            TargetPos(1) = txtLaserPosY.Text
            TargetPos(2) = txtLaserPosZ.Text
            TargetPos(3) = 0
            TargetPos(4) = 0
            ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)

        End With
        UcJoyStick1.RefreshPosition()
        '[Note]:更新Laser Reader Value
        Dim HeightValue As String = ""

        'Toby_Start
        Select Case gSSystemParameter.MeasureType
            Case enmMeasureType.Contact '接觸式測高
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                    btnAutoZero.BackColor = Color.Red
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
                    btnGoSensorPos.Enabled = True

                    '20170602按鍵保護
                    btnSetCcdPos.Enabled = True
                    btnSetHightSensorPos.Enabled = True
                    btnGoCCDPos.Enabled = True
                    btnAutoZero.Enabled = True
                    btnReZero.Enabled = True
                    btnCCDToHightPrev.Enabled = True
                    btnCCDToHightNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True

                    Exit Sub
                End If
            Case enmMeasureType.Laser

                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                    btnAutoZero.BackColor = Color.Red
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
                    btnGoSensorPos.Enabled = True

                    '20170602按鍵保護
                    btnSetCcdPos.Enabled = True
                    btnSetHightSensorPos.Enabled = True
                    btnGoCCDPos.Enabled = True
                    btnAutoZero.Enabled = True
                    btnReZero.Enabled = True
                    btnCCDToHightPrev.Enabled = True
                    btnCCDToHightNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True

                    Exit Sub
                End If
        End Select

        lblLaserReaderValue.Text = HeightValue
        btnGoSensorPos.Enabled = True
        UcJoyStick1.RefreshPosition()
        '20170602按鍵保護
        btnSetCcdPos.Enabled = True
        btnSetHightSensorPos.Enabled = True
        btnGoCCDPos.Enabled = True
        btnAutoZero.Enabled = True
        btnReZero.Enabled = True
        btnCCDToHightPrev.Enabled = True
        btnCCDToHightNext.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True

    End Sub

    ''' <summary>放棄離開</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click.")
        '20170623 離開時Z 軸升至安全高度
        Timer1.Enabled = False
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
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnOK]" & vbTab & "Click.")
        btnOK.Enabled = False

        With gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo)

            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
            gSyslog.Save("CCD Pos X:" & .CCDCalibPosX(sys.SelectValve))
            gSyslog.Save("CCD Pos Y:" & .CCDCalibPosY(sys.SelectValve))
            gSyslog.Save("CCD Pos Z:" & .CCDCalibPosZ(sys.SelectValve))
            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")

            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ")  Start")
            gSyslog.Save("Laser Pos X:" & .LaserCalibPosX(sys.SelectValve))
            gSyslog.Save("Laser Pos Y:" & .LaserCalibPosY(sys.SelectValve))
            gSyslog.Save("Laser Pos Z:" & .LaserCalibPosZ(sys.SelectValve))
            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ")  End")

            'CCD校正位置
            '.CCDCalibPosX(sys.SelectValve) = CDec(txtCCDPosX.Text)
            '.CCDCalibPosY(sys.SelectValve) = CDec(txtCCDPosY.Text)
            '.CCDCalibPosZ(sys.SelectValve) = CDec(txtCCDPosZ.Text)

            '20170810
            '雷射校正位置
            '.LaserCalibPosX(sys.SelectValve) = CDec(txtLaserPosX.Text)
            '.LaserCalibPosY(sys.SelectValve) = CDec(txtLaserPosY.Text)
            '.LaserCalibPosZ(sys.SelectValve) = CDec(txtLaserPosZ.Text)

            '[說明]:同Stage不同閥不需校正兩次Valve Height Sensor 位置 
            For i = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                .CCDCalibPosX(i) = CDec(txtCCDPosX.Text)
                .CCDCalibPosY(i) = CDec(txtCCDPosY.Text)
                .CCDCalibPosZ(i) = CDec(txtCCDPosZ.Text)

                .LaserCalibPosX(i) = CDec(txtLaserPosX.Text)
                .LaserCalibPosY(i) = CDec(txtLaserPosY.Text)
                .LaserCalibPosZ(i) = CDec(txtLaserPosZ.Text)
            Next






            .Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini") '儲存設定
            .Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini") '設定間距
            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") Start")
            gSyslog.Save("CCD Pos X:" & .CCDCalibPosX(sys.SelectValve))
            gSyslog.Save("CCD Pos Y:" & .CCDCalibPosY(sys.SelectValve))
            gSyslog.Save("CCD Pos Z:" & .CCDCalibPosZ(sys.SelectValve))
            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Valve(" & (sys.SelectValve + 1).ToString & ") End")

            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ")  Start")
            gSyslog.Save("Laser Pos X:" & .LaserCalibPosX(sys.SelectValve))
            gSyslog.Save("Laser Pos Y:" & .LaserCalibPosY(sys.SelectValve))
            gSyslog.Save("Laser Pos Z:" & .LaserCalibPosZ(sys.SelectValve))
            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ")  End")

        End With
        '場景曝光與光源存檔
        Dim SceneName As String = CalibSceneName & (sys.CCDNo + 1).ToString
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


        btnOK.Enabled = True

        'If CalibrationEnable = True Then
        'Sue0512
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '  ElseIf CalibrationEnable = False Then
        'Sue0512
        '存檔成功 
        ' gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        'MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        'Me.Close()
        'End If

    End Sub

    Private Sub btnAutoZero_Click(sender As Object, e As EventArgs) Handles btnAutoZero.Click

        Dim dCurrentZ, dLaserZeroZ As Decimal
        Dim iRetryCount As Integer = 30
        Dim iSearchStep As Integer = 10
        Dim TimeOutStopwatch As New Stopwatch
        Dim HeightValue As String = ""
        Dim dSearchZ As Double
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnAutoZero]" & vbTab & "Click.")

        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            BtnHomeFirstBehavior(sender)
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            '20170602按鍵保護
            btnAutoZero.Enabled = True
            btnSetCcdPos.Enabled = True
            btnSetHightSensorPos.Enabled = True
            btnGoCCDPos.Enabled = True
            btnReZero.Enabled = True
            btnGoSensorPos.Enabled = True
            btnCCDToHightPrev.Enabled = True
            btnCCDToHightNext.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True
            UcJoyStick1.Enabled = True

            grpCCDPos.Enabled = True
            grpHeightSensorPos.Enabled = True
            Exit Sub
        End If

        If btnAutoZero.Enabled = False Then
            Exit Sub
        End If

        btnAutoZero.Enabled = False

        '20170602按鍵保護
        btnSetCcdPos.Enabled = False
        btnSetHightSensorPos.Enabled = False
        btnGoCCDPos.Enabled = False
        btnReZero.Enabled = False
        btnGoSensorPos.Enabled = False
        btnCCDToHightPrev.Enabled = False
        btnCCDToHightNext.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        UcJoyStick1.Enabled = False
        grpCCDPos.Enabled = False
        grpHeightSensorPos.Enabled = False

        '移動到設定位置
        With gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo)
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, .LaserCalibPosX(sys.SelectValve), .LaserCalibPosY(sys.SelectValve), .LaserCalibPosZ(sys.SelectValve)), "INFO_6019015")
            Dim AxisNo(4) As Integer
            Dim TargetPos(4) As Decimal
            AxisNo(0) = sys.AxisX
            AxisNo(1) = sys.AxisY
            AxisNo(2) = sys.AxisZ
            AxisNo(3) = sys.AxisB
            AxisNo(4) = sys.AxisC

            TargetPos(0) = txtLaserPosX.Text
            TargetPos(1) = txtLaserPosY.Text
            TargetPos(2) = txtLaserPosZ.Text
            TargetPos(3) = 0
            TargetPos(4) = 0
            ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)

        End With
        UcJoyStick1.RefreshPosition()


        btnAutoZero.BackColor = System.Drawing.Color.Yellow

        'Toby_Start
        Select Case gSSystemParameter.MeasureType
            Case enmMeasureType.Contact '接觸式測高
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                    btnAutoZero.BackColor = Color.Red
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

                    btnAutoZero.Enabled = True

                    '20170602按鍵保護
                    btnSetCcdPos.Enabled = True
                    btnSetHightSensorPos.Enabled = True
                    btnGoCCDPos.Enabled = True
                    btnReZero.Enabled = True
                    btnGoSensorPos.Enabled = True
                    btnCCDToHightPrev.Enabled = True
                    btnCCDToHightNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True
                    grpCCDPos.Enabled = True
                    grpHeightSensorPos.Enabled = True
                    Exit Sub
                End If
                lblLaserReaderValue.Text = HeightValue

                '20170623 測高保護機制
                If HeightValue < 5 Then
                    MsgBox("請將接觸式測高儀校正", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnAutoZero.Enabled = True
                    '20170602按鍵保護
                    btnSetCcdPos.Enabled = True
                    btnSetHightSensorPos.Enabled = True
                    btnGoCCDPos.Enabled = True
                    btnReZero.Enabled = True
                    btnGoSensorPos.Enabled = True
                    btnCCDToHightPrev.Enabled = True
                    btnCCDToHightNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True
                    grpCCDPos.Enabled = True
                    grpHeightSensorPos.Enabled = True
                    Exit Sub
                End If


                '汽缸動作
                gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, True)
                Call gDOCollection.RefreshDO()
                System.Threading.Thread.CurrentThread.Join(1000)
                '汽缸動作_end


                dCurrentZ = Val(gCMotion.GetPositionValue(sys.AxisZ))
                UcJoyStick1.SetSpeedType(SpeedType.Fast)

                If Val(HeightValue) < -10 Or Val(HeightValue) > 10 Then

                    dSearchZ = dCurrentZ
                    While (dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)
                        SafeLaserZMove(dSearchZ)
                        dSearchZ = Val(gCMotion.GetPositionValue(UcJoyStick1.AxisZ))

                        If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                            btnAutoZero.BackColor = Color.Red
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
                            btnAutoZero.Enabled = True

                            '20170602按鍵保護
                            btnSetCcdPos.Enabled = True
                            btnSetHightSensorPos.Enabled = True
                            btnGoCCDPos.Enabled = True
                            btnReZero.Enabled = True
                            btnGoSensorPos.Enabled = True
                            btnCCDToHightPrev.Enabled = True
                            btnCCDToHightNext.Enabled = True
                            btnOK.Enabled = True
                            btnCancel.Enabled = True
                            UcJoyStick1.Enabled = True
                            grpCCDPos.Enabled = True
                            grpHeightSensorPos.Enabled = True
                            Exit Sub
                        End If
                        lblLaserReaderValue.Text = HeightValue

                        dSearchZ += iSearchStep
                        iRetryCount -= 1
                    End While
                    dSearchZ = dCurrentZ
                    While (dSearchZ > gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)

                        SafeLaserZMove(dSearchZ)
                        dSearchZ = Val(gCMotion.GetPositionValue(UcJoyStick1.AxisZ))
                        If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                            btnAutoZero.BackColor = Color.Red
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
                            btnAutoZero.Enabled = True

                            '20170602按鍵保護
                            btnSetCcdPos.Enabled = True
                            btnSetHightSensorPos.Enabled = True
                            btnGoCCDPos.Enabled = True
                            btnReZero.Enabled = True
                            btnGoSensorPos.Enabled = True
                            btnCCDToHightPrev.Enabled = True
                            btnCCDToHightNext.Enabled = True
                            btnOK.Enabled = True
                            btnCancel.Enabled = True
                            UcJoyStick1.Enabled = True
                            grpCCDPos.Enabled = True
                            grpHeightSensorPos.Enabled = True
                            Exit Sub
                        End If
                        lblLaserReaderValue.Text = HeightValue
                        dSearchZ -= iSearchStep
                        iRetryCount -= 1
                    End While
                End If

                UcJoyStick1.SetSpeedType(SpeedType.Slow)
                If iRetryCount <= 0 Then
                    '測高儀自動測高失敗
                    Select Case sys.StageNo
                        Case 0
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014003))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2014003), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 1
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014103))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2014103), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 2
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014203))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2014203), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 3
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014303))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2014303), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    btnAutoZero.BackColor = System.Drawing.Color.Red

                    btnAutoZero.Enabled = True

                    '20170602按鍵保護
                    btnSetCcdPos.Enabled = True
                    btnSetHightSensorPos.Enabled = True
                    btnGoCCDPos.Enabled = True
                    btnReZero.Enabled = True
                    btnGoSensorPos.Enabled = True
                    btnCCDToHightPrev.Enabled = True
                    btnCCDToHightNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True
                    grpCCDPos.Enabled = True
                    grpHeightSensorPos.Enabled = True
                    Exit Sub
                End If
                If dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Then
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
                    btnAutoZero.BackColor = System.Drawing.Color.Red
                    btnAutoZero.Enabled = True

                    '20170602按鍵保護
                    btnSetCcdPos.Enabled = True
                    btnSetHightSensorPos.Enabled = True
                    btnGoCCDPos.Enabled = True
                    btnReZero.Enabled = True
                    btnGoSensorPos.Enabled = True
                    btnCCDToHightPrev.Enabled = True
                    btnCCDToHightNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True
                    grpCCDPos.Enabled = True
                    grpHeightSensorPos.Enabled = True
                    Exit Sub
                End If

                For i As Integer = 0 To 5
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                        btnAutoZero.BackColor = Color.Red
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
                        btnAutoZero.Enabled = True

                        '20170602按鍵保護
                        btnSetCcdPos.Enabled = True
                        btnSetHightSensorPos.Enabled = True
                        btnGoCCDPos.Enabled = True
                        btnReZero.Enabled = True
                        btnGoSensorPos.Enabled = True
                        btnCCDToHightPrev.Enabled = True
                        btnCCDToHightNext.Enabled = True
                        btnOK.Enabled = True
                        btnCancel.Enabled = True
                        UcJoyStick1.Enabled = True
                        grpCCDPos.Enabled = True
                        grpHeightSensorPos.Enabled = True
                        '汽缸動作(收回)
                        gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
                        Call gDOCollection.RefreshDO()
                        '汽缸動作_end

                        Exit Sub
                    End If
                    lblLaserReaderValue.Text = HeightValue
                    dCurrentZ = Val(gCMotion.GetPositionValue(UcJoyStick1.AxisZ))
                    dLaserZeroZ = dCurrentZ + Val(HeightValue)
                    SafeLaserZMove(dLaserZeroZ)


                Next

                UcJoyStick1.RefreshPosition()
                txtLaserPosZ.Text = dLaserZeroZ


                '汽缸動作(收回)
                gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
                Call gDOCollection.RefreshDO()
                '汽缸動作_end



            Case Else 'Laser 測高
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                    btnAutoZero.BackColor = Color.Red
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
                    btnAutoZero.Enabled = True

                    '20170602按鍵保護
                    btnSetCcdPos.Enabled = True
                    btnSetHightSensorPos.Enabled = True
                    btnGoCCDPos.Enabled = True
                    btnReZero.Enabled = True
                    btnGoSensorPos.Enabled = True
                    btnCCDToHightPrev.Enabled = True
                    btnCCDToHightNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True
                    grpCCDPos.Enabled = True
                    grpHeightSensorPos.Enabled = True
                    Exit Sub
                End If
                lblLaserReaderValue.Text = HeightValue

                dCurrentZ = Val(gCMotion.GetPositionValue(sys.AxisZ))
                UcJoyStick1.SetSpeedType(SpeedType.Fast)
                If Val(HeightValue) < -10 Or Val(HeightValue) > 10 Then

                    dSearchZ = dCurrentZ
                    While (dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)
                        SafeLaserZMove(dSearchZ)
                        dSearchZ = Val(gCMotion.GetPositionValue(UcJoyStick1.AxisZ))

                        If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                            btnAutoZero.BackColor = Color.Red
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
                            btnAutoZero.Enabled = True

                            '20170602按鍵保護
                            btnSetCcdPos.Enabled = True
                            btnSetHightSensorPos.Enabled = True
                            btnGoCCDPos.Enabled = True
                            btnReZero.Enabled = True
                            btnGoSensorPos.Enabled = True
                            btnCCDToHightPrev.Enabled = True
                            btnCCDToHightNext.Enabled = True
                            btnOK.Enabled = True
                            btnCancel.Enabled = True
                            UcJoyStick1.Enabled = True
                            grpCCDPos.Enabled = True
                            grpHeightSensorPos.Enabled = True
                            Exit Sub
                        End If
                        lblLaserReaderValue.Text = HeightValue

                        dSearchZ += iSearchStep
                        iRetryCount -= 1
                    End While
                    dSearchZ = dCurrentZ
                    While (dSearchZ > gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)

                        SafeLaserZMove(dSearchZ)
                        dSearchZ = Val(gCMotion.GetPositionValue(UcJoyStick1.AxisZ))
                        If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                            btnAutoZero.BackColor = Color.Red
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
                            btnAutoZero.Enabled = True

                            '20170602按鍵保護
                            btnSetCcdPos.Enabled = True
                            btnSetHightSensorPos.Enabled = True
                            btnGoCCDPos.Enabled = True
                            btnReZero.Enabled = True
                            btnGoSensorPos.Enabled = True
                            btnCCDToHightPrev.Enabled = True
                            btnCCDToHightNext.Enabled = True
                            btnOK.Enabled = True
                            btnCancel.Enabled = True
                            UcJoyStick1.Enabled = True
                            grpCCDPos.Enabled = True
                            grpHeightSensorPos.Enabled = True
                            Exit Sub
                        End If
                        lblLaserReaderValue.Text = HeightValue
                        dSearchZ -= iSearchStep
                        iRetryCount -= 1
                    End While
                End If

                UcJoyStick1.SetSpeedType(SpeedType.Slow)
                If iRetryCount <= 0 Then
                    '測高儀自動測高失敗
                    Select Case sys.StageNo
                        Case 0
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014003))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2014003), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 1
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014103))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2014103), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 2
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014203))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2014203), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case 3
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014303))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2014303), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    btnAutoZero.BackColor = System.Drawing.Color.Red
                    btnAutoZero.Enabled = True

                    '20170602按鍵保護
                    btnSetCcdPos.Enabled = True
                    btnSetHightSensorPos.Enabled = True
                    btnGoCCDPos.Enabled = True
                    btnReZero.Enabled = True
                    btnGoSensorPos.Enabled = True
                    btnCCDToHightPrev.Enabled = True
                    btnCCDToHightNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True
                    grpCCDPos.Enabled = True
                    grpHeightSensorPos.Enabled = True
                    Exit Sub
                End If
                If dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Then
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
                    btnAutoZero.BackColor = System.Drawing.Color.Red
                    btnAutoZero.Enabled = True

                    '20170602按鍵保護
                    btnSetCcdPos.Enabled = True
                    btnSetHightSensorPos.Enabled = True
                    btnGoCCDPos.Enabled = True
                    btnReZero.Enabled = True
                    btnGoSensorPos.Enabled = True
                    btnCCDToHightPrev.Enabled = True
                    btnCCDToHightNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True
                    grpCCDPos.Enabled = True
                    grpHeightSensorPos.Enabled = True
                    Exit Sub
                End If

                For i As Integer = 0 To 5
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                        btnAutoZero.BackColor = Color.Red
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

                        btnAutoZero.BackColor = System.Drawing.Color.Red
                        btnAutoZero.Enabled = True

                        btnSetCcdPos.Enabled = True
                        btnSetHightSensorPos.Enabled = True
                        btnGoCCDPos.Enabled = True
                        btnReZero.Enabled = True
                        btnGoSensorPos.Enabled = True
                        btnCCDToHightPrev.Enabled = True
                        btnCCDToHightNext.Enabled = True
                        btnOK.Enabled = True
                        btnCancel.Enabled = True
                        UcJoyStick1.Enabled = True
                        grpCCDPos.Enabled = True
                        grpHeightSensorPos.Enabled = True

                        Exit Sub
                    End If
                    lblLaserReaderValue.Text = HeightValue
                    dCurrentZ = Val(gCMotion.GetPositionValue(UcJoyStick1.AxisZ))
                    dLaserZeroZ = dCurrentZ + Val(HeightValue)
                    SafeLaserZMove(dLaserZeroZ)
                Next
                UcJoyStick1.RefreshPosition()
                txtLaserPosZ.Text = dLaserZeroZ


        End Select
        'Toby_End
        btnAutoZero.BackColor = System.Drawing.SystemColors.Control
        btnAutoZero.UseVisualStyleBackColor = True
        btnAutoZero.Enabled = True
        btnSetCcdPos.Enabled = True
        btnSetHightSensorPos.Enabled = True
        btnGoCCDPos.Enabled = True
        btnReZero.Enabled = True
        btnGoSensorPos.Enabled = True
        btnCCDToHightPrev.Enabled = True
        btnCCDToHightNext.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True
        grpCCDPos.Enabled = True
        grpHeightSensorPos.Enabled = True
    End Sub


    Private Sub SafeLaserZMove(ByVal TargetPos As Decimal)
        Dim mStopWatch As New Stopwatch

        If gCMotion.AbsMove(sys.AxisZ, TargetPos) <> CommandStatus.Sucessed Then
            'Z 軸移動失敗
            Select Case sys.StageNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1032000) & TargetPos)
                    MsgBox(gMsgHandler.GetMessage(Error_1032000) & TargetPos, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1044000) & TargetPos)
                    MsgBox(gMsgHandler.GetMessage(Error_1044000) & TargetPos, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 2
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1062000) & TargetPos)
                    MsgBox(gMsgHandler.GetMessage(Error_1062000) & TargetPos, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 3
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1069000) & TargetPos)
                    MsgBox(gMsgHandler.GetMessage(Error_1069000) & TargetPos, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            Exit Sub
        End If

        'gCMotion.AbsMove(sys.AxisZ, TargetPos) '移至targetPos
        System.Threading.Thread.CurrentThread.Join(200) '移動命令下達後不能立刻看到位


        mStopWatch.Restart()
        Do
            Application.DoEvents() 'Toby Modify_20170513(拿掉doEvents)
            If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then '到位 離開等待迴圈
                Exit Do
            End If
            If sys.AxisZ > -1 Then
                If gCMotion.IsMoveTimeOut(sys.AxisZ) Then '逾時 中斷離開
                    Select Case sys.StageNo
                        Case enmStage.No1
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1032004), "Error_1032004", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No2
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1044004), "Error_1044004", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No3
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1062004), "Error_1062004", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No4
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1069004), "Error_1069004", eMessageLevel.Error) '
                            MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    MsgBox(gCMotion.AxisParameter(sys.AxisZ).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                    Exit Do
                    Exit Sub
                End If
            End If

            If mStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut4 Then
                Select Case sys.StageNo
                    Case enmStage.No1
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1032004), "Error_1032004", eMessageLevel.Error)
                        MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case enmStage.No2
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1044004), "Error_1044004", eMessageLevel.Error)
                        MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case enmStage.No3
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1062004), "Error_1062004", eMessageLevel.Error)
                        MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case enmStage.No4
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1069004), "Error_1069004", eMessageLevel.Error)
                        MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
                MsgBox(gCMotion.AxisParameter(sys.AxisZ).AxisName & " Move Time Out!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Exit Do
                Exit Sub
            End If

        Loop

    End Sub

    Private Sub txtCCDPosX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCCDPosX.KeyPress, txtCCDPosY.KeyPress, txtCCDPosZ.KeyPress, txtLaserPosX.KeyPress, txtLaserPosY.KeyPress, txtLaserPosZ.KeyPress
        Premtek.Base.CKeyPress.CheckDecimal(sender, e)
    End Sub

    Private Sub nmcExposure_ValueChanged(sender As Object, e As EventArgs) Handles nmcExposure.ValueChanged
        'gAOICollection.SetExposure(sys.CCDNo, nmcExposure.Value) '設定曝光值
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照開  
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
    End Sub

    Private Sub btnCCDToHightPrev_Click(sender As Object, e As EventArgs) Handles btnCCDToHightPrev.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click.")
        Me.Close()

        If gfrmCalibrationCCD2Valve1 Is Nothing Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        ElseIf gfrmCalibrationCCD2Valve1.IsDisposed Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        End If

        With gfrmCalibrationCCD2Valve1
            .sys = gSYS(sys.EsysNum)
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            '.CalibrationEnable = True
            ._oLastSelectCobValve = oLastSelectCobValve
            .ShowDialog()
        End With
    End Sub

    Private Sub btnCCDToHightNext_Click(sender As Object, e As EventArgs) Handles btnCCDToHightNext.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCCDToHightNext]" & vbTab & "Click.")
        Me.Close()

        If gfrmCalibrationZHeight Is Nothing Then
            gfrmCalibrationZHeight = New frmCalibrationZHeight
        ElseIf gfrmCalibrationZHeight.IsDisposed Then
            gfrmCalibrationZHeight = New frmCalibrationZHeight
        End If

        With gfrmCalibrationZHeight
            .sys = gSYS(sys.EsysNum)
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            '.CalibrationEnable = True
            ._oLastSelectCobValve = oLastSelectCobValve
            .ShowDialog()
        End With
    End Sub



    Private Sub btnReZero_Click(sender As Object, e As EventArgs) Handles btnReZero.Click

        gSyslog.Save("[" & ClassName & "]" & vbTab & "[BtnReZero]" & vbTab & "Click")
        Dim dCurrentZ, dLaserZeroZ As Decimal
        Dim iRetryCount As Integer = 30
        Dim iSearchStep As Integer = 10
        Dim TimeOutStopwatch As New Stopwatch
        Dim HeightValue As String = ""
        Dim dSearchZ As Double

        '20170602按鍵保護
        If btnReZero.Enabled = False Then
            Exit Sub
        End If

        'Z軸速度設定
        gCMotion.SetVelLow(sys.AxisZ, 0)
        gCMotion.SetVelHigh(sys.AxisZ, 50)
        gCMotion.SetAcc(sys.AxisZ, 1000)
        gCMotion.SetDec(sys.AxisZ, 1000)

        '20170602按鍵保護
        btnReZero.Enabled = False
        btnAutoZero.Enabled = False
        btnSetCcdPos.Enabled = False
        btnSetHightSensorPos.Enabled = False
        btnGoCCDPos.Enabled = False
        btnGoSensorPos.Enabled = False
        btnCCDToHightPrev.Enabled = False
        btnCCDToHightNext.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        UcJoyStick1.Enabled = False

        grpCCDPos.Enabled = False
        grpHeightSensorPos.Enabled = False


        SafeLaserZMove(0)

        '汽缸動作
        gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, True)
        Call gDOCollection.RefreshDO()
        System.Threading.Thread.CurrentThread.Join(500)
        '歸零 Reader
        gDOCollection.SetState(enmDO.ContactHeightReZero, True)
        Call gDOCollection.RefreshDO()
        System.Threading.Thread.CurrentThread.Join(500)

        '歸零DO 回復
        gDOCollection.SetState(enmDO.ContactHeightReZero, False)
        Call gDOCollection.RefreshDO()
        MsgBox(gMsgHandler.GetMessage(Error_1014004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '汽缸動作_end

        If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
            btnReZero.BackColor = Color.Red
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
            btnReZero.BackColor = System.Drawing.Color.Red
            btnReZero.Enabled = True

            '20170602按鍵保護
            btnAutoZero.Enabled = True
            btnSetCcdPos.Enabled = True
            btnSetHightSensorPos.Enabled = True
            btnGoCCDPos.Enabled = True
            btnGoSensorPos.Enabled = True
            btnCCDToHightPrev.Enabled = True
            btnCCDToHightNext.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True
            UcJoyStick1.Enabled = True

            grpCCDPos.Enabled = True
            grpHeightSensorPos.Enabled = True

            Exit Sub
        End If
        lblLaserReaderValue.Text = HeightValue

        dCurrentZ = Val(gCMotion.GetPositionValue(sys.AxisZ))
        UcJoyStick1.SetSpeedType(SpeedType.Fast)

        If Val(HeightValue) < -10 Or Val(HeightValue) > 10 Then

            dSearchZ = dCurrentZ
            While (dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)
                SafeLaserZMove(dSearchZ)
                dSearchZ = Val(gCMotion.GetPositionValue(UcJoyStick1.AxisZ))

                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                    btnAutoZero.BackColor = Color.Red
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
                    btnReZero.BackColor = System.Drawing.Color.Red
                    btnReZero.Enabled = True

                    '20170602按鍵保護
                    btnAutoZero.Enabled = True
                    btnSetCcdPos.Enabled = True
                    btnSetHightSensorPos.Enabled = True
                    btnGoCCDPos.Enabled = True
                    btnGoSensorPos.Enabled = True
                    btnCCDToHightPrev.Enabled = True
                    btnCCDToHightNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True

                    grpCCDPos.Enabled = True
                    grpHeightSensorPos.Enabled = True

                    Exit Sub
                End If
                lblLaserReaderValue.Text = HeightValue

                dSearchZ += iSearchStep
                iRetryCount -= 1
            End While
            dSearchZ = dCurrentZ
            While (dSearchZ > gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)

                SafeLaserZMove(dSearchZ)
                dSearchZ = Val(gCMotion.GetPositionValue(UcJoyStick1.AxisZ))
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                    btnAutoZero.BackColor = Color.Red
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
                    btnReZero.BackColor = System.Drawing.Color.Red
                    btnReZero.Enabled = True

                    '20170602按鍵保護
                    btnAutoZero.Enabled = True
                    btnSetCcdPos.Enabled = True
                    btnSetHightSensorPos.Enabled = True
                    btnGoCCDPos.Enabled = True
                    btnGoSensorPos.Enabled = True
                    btnCCDToHightPrev.Enabled = True
                    btnCCDToHightNext.Enabled = True
                    btnOK.Enabled = True
                    btnCancel.Enabled = True
                    UcJoyStick1.Enabled = True

                    grpCCDPos.Enabled = True
                    grpHeightSensorPos.Enabled = True

                    Exit Sub
                End If
                lblLaserReaderValue.Text = HeightValue
                dSearchZ -= iSearchStep
                iRetryCount -= 1
            End While
        End If

        UcJoyStick1.SetSpeedType(SpeedType.Slow)
        If iRetryCount <= 0 Then
            '測高儀自動測高失敗
            Select Case sys.StageNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014003))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2014003), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014103))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2014103), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 2
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014203))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2014203), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 3
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014303))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2014303), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            btnReZero.BackColor = System.Drawing.Color.Red
            btnReZero.Enabled = True

            '20170602按鍵保護
            btnAutoZero.Enabled = True
            btnSetCcdPos.Enabled = True
            btnSetHightSensorPos.Enabled = True
            btnGoCCDPos.Enabled = True
            btnGoSensorPos.Enabled = True
            btnCCDToHightPrev.Enabled = True
            btnCCDToHightNext.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True
            UcJoyStick1.Enabled = True

            grpCCDPos.Enabled = True
            grpHeightSensorPos.Enabled = True

            Exit Sub
        End If
        If dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Then
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
            btnReZero.BackColor = System.Drawing.Color.Red
            btnReZero.Enabled = True

            '20170602按鍵保護
            btnAutoZero.Enabled = True
            btnSetCcdPos.Enabled = True
            btnSetHightSensorPos.Enabled = True
            btnGoCCDPos.Enabled = True
            btnGoSensorPos.Enabled = True
            btnCCDToHightPrev.Enabled = True
            btnCCDToHightNext.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True
            UcJoyStick1.Enabled = True

            grpCCDPos.Enabled = True
            grpHeightSensorPos.Enabled = True

            Exit Sub
        End If


        For i As Integer = 0 To 10
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
                btnReZero.BackColor = System.Drawing.Color.Red
                btnReZero.Enabled = True

                '20170602按鍵保護
                btnAutoZero.Enabled = True
                btnSetCcdPos.Enabled = True
                btnSetHightSensorPos.Enabled = True
                btnGoCCDPos.Enabled = True
                btnGoSensorPos.Enabled = True
                btnCCDToHightPrev.Enabled = True
                btnCCDToHightNext.Enabled = True
                btnOK.Enabled = True
                btnCancel.Enabled = True
                UcJoyStick1.Enabled = True

                grpCCDPos.Enabled = True
                grpHeightSensorPos.Enabled = True


                '汽缸動作(收回)
                gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
                Call gDOCollection.RefreshDO()
                '汽缸動作_end

                Exit Sub
            End If
            lblLaserReaderValue.Text = HeightValue
            dCurrentZ = Val(gCMotion.GetPositionValue(UcJoyStick1.AxisZ))
            dLaserZeroZ = dCurrentZ + (Val(HeightValue) - 6)
            SafeLaserZMove(dLaserZeroZ)
            System.Threading.Thread.CurrentThread.Join(500)
        Next



        '歸零 Reader
        gDOCollection.SetState(enmDO.ContactHeightReZero, True)
        Call gDOCollection.RefreshDO()
        System.Threading.Thread.CurrentThread.Join(500)

        '歸零DO 回復
        gDOCollection.SetState(enmDO.ContactHeightReZero, False)
        Call gDOCollection.RefreshDO()
        System.Threading.Thread.CurrentThread.Join(500)

        '汽缸動作(收回)
        gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
        Call gDOCollection.RefreshDO()
        '汽缸動作_end


        SafeLaserZMove(0)
        btnReZero.Enabled = True

        '20170602按鍵保護
        btnAutoZero.Enabled = True
        btnSetCcdPos.Enabled = True
        btnSetHightSensorPos.Enabled = True
        btnGoCCDPos.Enabled = True
        btnGoSensorPos.Enabled = True
        btnCCDToHightPrev.Enabled = True
        btnCCDToHightNext.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        UcJoyStick1.Enabled = True

        grpCCDPos.Enabled = True
        grpHeightSensorPos.Enabled = True

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
            btnAutoZero.Enabled = False
            btnGoCCDPos.Enabled = False
            btnGoSensorPos.Enabled = False
            btnSetCcdPos.Enabled = False
            btnSetHightSensorPos.Enabled = False
            btnReZero.Enabled = False
            UcJoyStick1.Enabled = False
        Else
            btnAutoZero.Enabled = True
            btnGoCCDPos.Enabled = True
            btnGoSensorPos.Enabled = True
            btnSetCcdPos.Enabled = True
            btnSetHightSensorPos.Enabled = True
            btnReZero.Enabled = True
            UcJoyStick1.Enabled = True
        End If
    End Sub
End Class
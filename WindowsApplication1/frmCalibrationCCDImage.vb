Imports ProjectCore
Imports ProjectIO
Imports ProjectAOI
Imports ProjectMotion

Public Class frmCalibrationCCDImage

    ''' <summary>目前類別名稱</summary>
    ''' <remarks></remarks>
    Private Const ClassName As String = "frmCCDImageCalibration"
    Dim mIsCanClosed As Boolean = True
    Private Const CalibSceneName As String = "CALIBScale"
    ''' <summary>對外接入操作系統設定, 或是內部配接使用</summary>
    ''' <remarks></remarks>
    Public sys As sSysParam

    Private Sub frmCalibrationCCDImage_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170124 Ticket:100031 , Memory Freed [S]
        UcDisplay1.EndLive()
        UcDisplay1.ManualDispose()
        UcDisplay2.EndLive()
        UcDisplay2.ManualDispose()
        UcLightControl1.ManualDispose()
        UcJoyStick1.ManualDispose()
        Me.Dispose(True)
        'Eason 20170124 Ticket:100031 , Memory Freed [E]
    End Sub

    Private Sub frmCalibrationCCDImage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                UcDisplay2.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
            Case enmCCDType.OmronFZS2MUDP
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
                UcDisplay2.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
            Case Else
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
                UcDisplay2.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
        End Select

        Dim fileName As String
        fileName = Application.StartupPath & "\System\" & MachineName & "\" & CalibSceneName & (sys.CCDNo + 1).ToString & ".ini"  '光源設定檔路徑
        gAOICollection.LoadSceneParameter(CalibSceneName & (sys.StageNo + 1).ToString, fileName) '讀取光源,曝光值等設定
        lblSceneSet.Text = CalibSceneName & (sys.CCDNo + 1).ToString
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

        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        'System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照
        'System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        '--- 2016.06.18 + 開燈,曝光,觸發後才會改 ---
        'nmcExposure.Value = gAOICollection.GetExposure(sys.CCDNo)

        '[Note]校正資料顯示
        LoadCCDCalibrationData()


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

        If gAOICollection.GetCCDFormatsType(sys.CCDNo) = enmVideoFormatType.Bayer Then
            btnAWB.Visible = True
        Else
            btnAWB.Visible = False
        End If
        '[Note]是否顯示校正介面
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCS_F230A, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                grpCalibration.Visible = True
            Case Else
                grpCalibration.Visible = False
        End Select

    End Sub

    ''' <summary>
    ''' 資料顯示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadCCDCalibrationData()

        '[Note]Scale校正位置
        txtCCDPosX.Text = gSSystemParameter.CCDPosX(sys.CCDNo)
        txtCCDPosY.Text = gSSystemParameter.CCDPosY(sys.CCDNo)
        txtCCDPosZ.Text = gSSystemParameter.CCDPosZ(sys.CCDNo)


        '[Note]Scale數據 1個Pixel幾mm
        txtA11.Text = gSSystemParameter.CCDScaleX2X(sys.CCDNo)
        txtA12.Text = gSSystemParameter.CCDScaleY2X(sys.CCDNo)
        txtA21.Text = gSSystemParameter.CCDScaleX2Y(sys.CCDNo)
        txtA22.Text = gSSystemParameter.CCDScaleY2Y(sys.CCDNo)
    End Sub


    ''' <summary>
    ''' 按鈕保護
    ''' </summary>
    ''' <param name="disable"></param>
    ''' <remarks></remarks>
    Public Sub Btn_Control(ByVal disable As Boolean)
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Me)) Then
            Me.BeginInvoke(Sub()

                               btnSetCcdPosX.Enabled = disable
                               btnGoCCD.Enabled = disable
                               btnTrainScene.Enabled = disable
                               btnScalePR.Enabled = disable

                               btnCalibration.Enabled = disable
                               btnAWB.Enabled = disable

                               btnOK.Enabled = disable
                               btnCancel.Enabled = disable

                               UcLightControl1.Enabled = disable
                               UcJoyStick1.Enabled = disable

                           End Sub)
        End If

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

    Private Sub btnAWB_Click(sender As Object, e As EventArgs) Handles btnAWB.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        'btnAWB.Enabled = False
        '20170602按鍵保護
        Btn_Control(False)

        TabControl1.SelectTab(TabPageCCD)

        gAOICollection.SetAutoWhiteBalance(sys.CCDNo, True)
        System.Threading.Thread.CurrentThread.Join(1000)
        gAOICollection.SetAutoWhiteBalance(sys.CCDNo, False)

        'btnAWB.Enabled = True
        ''20170602按鍵保護
        Btn_Control(True)

    End Sub

    Private Sub nmcExposure_ValueChanged(sender As Object, e As EventArgs) Handles nmcExposure.ValueChanged
        'gAOICollection.SetExposure(sys.CCDNo, nmcExposure.Value) '設定曝光值
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照開  
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        Btn_Control(False)

        gSSystemParameter.CCDPosX(sys.CCDNo) = Val(txtCCDPosX.Text)
        gSSystemParameter.CCDPosY(sys.CCDNo) = Val(txtCCDPosY.Text)
        gSSystemParameter.CCDPosZ(sys.CCDNo) = Val(txtCCDPosZ.Text)
        gSSystemParameter.CCDStableTime(sys.CCDNo) = Val(NumSteadyTime.Value)
        gSSystemParameter.CCDScaleX2X(sys.CCDNo) = Val(txtA11.Text)
        gSSystemParameter.CCDScaleY2X(sys.CCDNo) = Val(txtA12.Text)
        gSSystemParameter.CCDScaleX2Y(sys.CCDNo) = Val(txtA21.Text)
        gSSystemParameter.CCDScaleY2Y(sys.CCDNo) = Val(txtA22.Text)
        gSSystemParameter.SaveCCDSCale(Application.StartupPath & "\System\" & MachineName & "\SysCCD.ini")

        '[Note]存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        Btn_Control(True)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'Sue20170627
        Dim btn As Button = sender
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
        '20170623 離開時Z 軸升至安全高度
        gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.WeightCalibration(sys.StageNo).SafePosZ(sys.SelectValve))
        Me.Close()
    End Sub

    Private Async Sub btnCalibration_Click(sender As Object, e As EventArgs) Handles btnCalibration.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCalibration]" & vbTab & "Click")
        If btnCalibration.Enabled = False Then '防連按
            Exit Sub
        End If

        btnCalibration.BackColor = Color.Yellow
        btnCalibration.Enabled = False
        '20170602按鍵保護
        Btn_Control(False)


        Await Task.Run(Sub()
                           Dim InputImage As Object
                           Dim OutputImage As Object
                           '拍照
                           'gAOICollection.SetCCDScene(sys.CCDNo, lstScene.SelectedItem)
                           'gAOICollection.SetExposure(sys.CCDNo, nmcExposure.Value) '設定曝光值
                           gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
                           System.Threading.Thread.CurrentThread.Join(10)
                           gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照開  
                           System.Threading.Thread.CurrentThread.Join(10)
                           gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保

                           Dim stopWatch As New Stopwatch
                           stopWatch.Restart()
                           Do
                               System.Threading.Thread.Sleep(1)
                               If stopWatch.ElapsedMilliseconds > 1000 Then
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
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnCalibration.Enabled = True
                                                          btnCalibration.BackColor = Color.White
                                                          '20170602按鍵保護
                                                          Btn_Control(True)
                                                      End Sub)
                                   End If

                                   Exit Sub
                               End If
                           Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
                           '取像
                           InputImage = gAOICollection.CalibBoardCalibration(sys.CCDNo, gAOICollection.GetAcqOutputImage(sys.CCDNo), False, 0) '20170317Wenda gAOICollection.GetAcqOutputImage(sys.CCDNo)
                           Try
                               '校正
                               OutputImage = gAOICollection.CalibBoardCalibration(sys.CCDNo, InputImage, True, numTileSize.Value)
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      UcDisplay2.CogDisplay1.Image = OutputImage
                                                      TabControl1.SelectTab(TabPageCalib) '切換校正介面
                                                  End Sub)
                               End If
                           Catch ex As Exception
                               MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                           End Try
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  btnCalibration.Enabled = True
                                                  btnCalibration.BackColor = Color.White
                                                  '20170602按鍵保護
                                                  Btn_Control(True)
                                              End Sub)
                           End If
                       End Sub)

        '20170602按鍵保護
        Btn_Control(True)

    End Sub




    Private Sub btnSetCcdPosX_Click(sender As Object, e As EventArgs) Handles btnSetCcdPosX.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        '[Note]按鈕保護
        Btn_Control(False)

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish And gSYS(eSys.MachineB).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Btn_Control(True)
                    Exit Sub
                End If

            Case Else
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Btn_Control(True)
                    Exit Sub
                End If
        End Select

        '[Note]是否超出極限
        Dim PosValue As Premtek.sPos
        PosValue.PosX = gCMotion.GetPositionValue(sys.AxisX)
        PosValue.PosY = gCMotion.GetPositionValue(sys.AxisY)
        PosValue.PosZ = gCMotion.GetPositionValue(sys.AxisZ)
        Dim AxisNo(4) As Integer
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC
        If EstimateSafePos(PosValue, AxisNo) = False Then
            Btn_Control(True)
            Exit Sub
        End If

        txtCCDPosX.Text = gCMotion.GetPositionValue(sys.AxisX)
        txtCCDPosY.Text = gCMotion.GetPositionValue(sys.AxisY)
        txtCCDPosZ.Text = gCMotion.GetPositionValue(sys.AxisZ)

        '[Note]按鈕保護
        Btn_Control(True)
    End Sub

    Private Function GoCCDButtonAction(ByVal btn As Button, ByVal mPos As Premtek.sPos) As Boolean
        'Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Return False
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            BtnHomeFirstBehavior(btn)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            Return False
        End If


        btn.Enabled = False
        Btn_Control(False)

        '[說明]:X、Y、Z軸
        If sys.AxisX > -1 Then
            gCMotion.SetVelAccDec(sys.AxisX)
        End If
        If sys.AxisY > -1 Then
            gCMotion.SetVelAccDec(sys.AxisY)
        End If
        If sys.AxisZ > -1 Then
            gCMotion.SetVelAccDec(sys.AxisZ)
        End If
        If sys.AxisB > -1 Then
            gCMotion.SetVelAccDec(sys.AxisB)
        End If
        If sys.AxisC > -1 Then
            gCMotion.SetVelAccDec(sys.AxisC)
        End If

        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC


        Select Case gSSystemParameter.CCDModuleType
            Case enmCCDModule.eFix
                TargetPos(0) = mPos.PosX
                TargetPos(1) = mPos.PosY
                TargetPos(2) = 0
                TargetPos(3) = 0
                TargetPos(4) = 0

            Case enmCCDModule.eFree
                TargetPos(0) = mPos.PosX
                TargetPos(1) = mPos.PosY
                TargetPos(2) = mPos.PosZ
                TargetPos(3) = 0
                TargetPos(4) = 0
        End Select
        ButtonSafeMovePos(btn, AxisNo, TargetPos, sys)
        UcJoyStick1.RefreshPosition()

        btn.Enabled = True
        '20170602按鍵保護
        Btn_Control(True)

        Return True
    End Function


    Private Sub btnGoCCD_Click(sender As Object, e As EventArgs) Handles btnGoCCD.Click
        Dim tmpPos As New Premtek.sPos
        tmpPos.PosX = txtCCDPosX.Text
        tmpPos.PosY = txtCCDPosY.Text
        tmpPos.PosZ = txtCCDPosZ.Text
        If GoCCDButtonAction(sender, tmpPos) = False Then
            Exit Sub
        End If
    End Sub


#Region "比例教導"

    ''' <summary>移動取像定位</summary>
    ''' <param name="sceneName">指定定位場景索引</param>
    ''' <param name="targetPosX">目標位置X(mm)</param>
    ''' <param name="targetPosY">目標位置Y(mm)</param>
    ''' <param name="pixPosX">像素座標X(pixel)</param>
    ''' <param name="pixPosY">像素座標Y(pixel)</param>
    ''' <param name="realPosX">實際座標X(mm)</param>
    ''' <param name="realPosY">實際座標Y(mm)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DoLoopMoveAcqAlign(ByVal enmAxisX As Integer, ByVal enmAxisY As Integer, ByVal sceneName As String, ByVal targetPosX As Decimal, ByVal targetPosY As Decimal, ByRef realPosX As Decimal, ByRef realPosY As Decimal, ByRef pixPosX As Decimal, ByRef pixPosY As Decimal) As Boolean
        If gAOICollection.IsCCDExist(sys.CCDNo) = False Then
            Return False
        End If
        gSyslog.Save("AbsMove To Target Pos(" & targetPosX & "," & targetPosY & ") & Acquisition.")

        Dim mStopWatch As New Stopwatch
        'Const MoveTimeOut = 10000 '移動逾時
        Const AcqTimeOut = 1000 '拍照逾時
        Dim StartPosX As Decimal = gCMotion.GetPositionValue(enmAxisX)
        Dim StartPosY As Decimal = gCMotion.GetPositionValue(enmAxisY)
        Dim Ticket As Integer

        gAOICollection.SetCCDScene(sys.CCDNo, sceneName)
        gAOICollection.SetCCDRunType(sys.CCDNo, enmCCDRunType.Fix)
        If gCMotion.AbsMove(enmAxisX, targetPosX) <> CommandStatus.Sucessed Then
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
            Return False
        End If
        If gCMotion.AbsMove(enmAxisY, targetPosY) <> CommandStatus.Sucessed Then
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
            Return False
        End If

        Threading.Thread.CurrentThread.Join(100) '避免立刻取的判定錯誤
        mStopWatch.Restart()

        Do
            System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
            If gCMotion.MotionDone(enmAxisX) And gCMotion.MotionDone(enmAxisY) Then
                Exit Do
            ElseIf gCMotion.IsMoveTimeOut(mStopWatch, StartPosX, targetPosX, gCMotion.AxisParameter(enmAxisX)) Then '
                'X軸移動Timeout
                Select Case sys.StageNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1031004))
                        MsgBox(gMsgHandler.GetMessage(Error_1031004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1042004))
                        MsgBox(gMsgHandler.GetMessage(Error_1042004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1060004))
                        MsgBox(gMsgHandler.GetMessage(Error_1060004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1067004))
                        MsgBox(gMsgHandler.GetMessage(Error_1067004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
                Return False
            ElseIf gCMotion.IsMoveTimeOut(mStopWatch, StartPosY, targetPosY, gCMotion.AxisParameter(enmAxisY)) Then
                'Y軸移動Timeout
                Select Case sys.StageNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1031004))
                        MsgBox(gMsgHandler.GetMessage(Error_1031004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1043004))
                        MsgBox(gMsgHandler.GetMessage(Error_1043004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1061004))
                        MsgBox(gMsgHandler.GetMessage(Error_1061004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1068004))
                        MsgBox(gMsgHandler.GetMessage(Error_1068004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
                Return False
            End If
        Loop
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False)
        Threading.Thread.CurrentThread.Join(NumSteadyTime.Value) '到位穩定

        '=== 取像 ===
        Ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False)
        mStopWatch.Restart()
        Do
            System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
            If Not gAOICollection.IsCCDCBusy(sys.CCDNo) Then '取像完成
                Exit Do
            ElseIf mStopWatch.ElapsedMilliseconds > AcqTimeOut Then '取像逾時
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

                Return False
            End If
        Loop
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False)

        If Not gAOICollection.IsSceneExist(sys.CCDNo, sceneName) Then
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000009), "Alarm_2000009", eMessageLevel.Error)
            gSyslog.Save("SceneID: " & sceneName)
            MsgBox(gMsgHandler.GetMessage(Alarm_2000009) & sceneName, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        Threading.Thread.CurrentThread.Join(100)
        gAOICollection.SetSceneInputImage(sys.CCDNo, sceneName, gAOICollection.CalibBoardCalibration(sys.CCDNo, gAOICollection.GetAcqOutputImage(sys.CCDNo), False, 0)) '20170317Wenda
        UcDisplay1.CogDisplay1.Image = gAOICollection.CalibBoardCalibration(sys.CCDNo, gAOICollection.GetAcqOutputImage(sys.CCDNo), False, 0) '20170317Wenda gAOICollection.GetAcqOutputImage(sys.CCDNo)
        '=== 取像 ===

        '=== 計算 ===
        Threading.Thread.CurrentThread.Join(100)

        mStopWatch.Restart()
        Do
            System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
            If gCCDAlignResultDict(sys.CCDNo).ContainsKey(Ticket) AndAlso gCCDAlignResultDict(sys.CCDNo)(Ticket).IsRunSuccess Then '定位計算完成
                Exit Do
            ElseIf mStopWatch.ElapsedMilliseconds > AcqTimeOut Then '定位計算逾時
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

                Return False
            End If
        Loop

        Threading.Thread.CurrentThread.Join(100) '避免立刻取的判定錯誤

        If Not gCCDAlignResultDict(sys.CCDNo).ContainsKey(Ticket) Then
            '定位資料不存在
            gSyslog.Save(Ticket & gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(Ticket & gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        If gCCDAlignResultDict(sys.CCDNo)(Ticket).Result.Count = 0 Then
            '定位資料不存在
            gSyslog.Save(Ticket & gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(Ticket & gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If

        'If gCCDAlignResultDict(sys.CCDNo)(Ticket).Result(0).Result <> enmResultConstants.Accept Then
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000022), "Warn_3000022", eMessageLevel.Warning)
        '    MsgBox(gMsgHandler.GetMessage(Warn_3000022))
        '    Return False
        'End If
        '=== 計算 ===

        'gAOICollection.ShowAlignResult(sys.CCDNo, Ticket, UcDisplay1)
        InvokeUcDisplay(UcDisplay1, gAOICollection, sys, sceneName, 0, 0, enmDisplayShape.Alignment) '更新控制項
        Threading.Thread.CurrentThread.Join(500) '[Note]停頓一下讓使用者看定位位置
        InvokeUcDisplay(UcDisplay1, gAOICollection, sys, sceneName, 0, 0, enmDisplayShape.None) '更新控制項

        realPosX = gCMotion.GetPositionValue(enmAxisX) '踏出的實際位置
        realPosY = gCMotion.GetPositionValue(enmAxisY)

        pixPosX = Convert.ToDecimal(gCCDAlignResultDict(sys.CCDNo)(Ticket).Result(0).PixelTranslationX)
        pixPosY = Convert.ToDecimal(gCCDAlignResultDict(sys.CCDNo)(Ticket).Result(0).PixelTranslationY)
        gSyslog.Save("AbsMove To Real Pos(" & realPosX & "," & realPosY & ") mm & Image Offset(" & pixPosX & "," & pixPosY & ") Pixel")
        Return True
    End Function

    ''' <summary>比例教導 mm/Pixel</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnScalePR_Click(sender As Object, e As EventArgs) Handles btnScalePR.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        'btnScalePR.Enabled = False
        Btn_Control(False)

        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            '請先復歸
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005))
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'btnScalePR.Enabled = True
            Btn_Control(True)
            Exit Sub
        End If

        Dim mScene As String = CalibSceneName & (sys.CCDNo + 1).ToString

        If mScene = "" Then
            'CCD 定位場景不存在
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

            'btnScalePR.Enabled = True
            Btn_Control(True)
            Exit Sub
        End If
        If Not gAOICollection.IsSceneExist(sys.CCDNo, mScene) Then
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000009), "Alarm_2000009", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Alarm_2000009), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'btnScalePR.Enabled = True
            Btn_Control(True)
            Exit Sub
        End If

        Dim MsgString As String
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eSimplifiedChinese
                MsgString = "是否要進行比例教導?"
            Case enmLanguageType.eTraditionalChinese
                MsgString = "是否要進行比例教導?"
            Case Else
                MsgString = "Do you want to do CCD Scale?"
        End Select

        If MsgBox(MsgString, vbOKCancel + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek") <> MsgBoxResult.Ok Then
            gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Cancel")
            'btnScalePR.Enabled = True
            Btn_Control(True)
            Exit Sub
        End If
        btnScalePR.BackColor = Color.Yellow '運行中
        btnScalePR.Refresh()

        'Await Task.Run(Sub()

        Dim RefPosX As Decimal '參考基準點 位置(mm)
        Dim RefPosY As Decimal '參考基準點 位置(mm)
        Dim RefPosZ As Decimal '參考基準點 位置(mm)

        '定位點位置
        RefPosX = txtCCDPosX.Text
        RefPosY = txtCCDPosY.Text
        RefPosZ = txtCCDPosZ.Text

        Dim stepCount As Integer = 0

        Dim Xi0 As Decimal '參考基準點 CCD(Pixel)
        Dim Yi0 As Decimal '參考基準點 CCD(Pixel)
        Dim Xo0 As Decimal '參考基準點
        Dim Yo0 As Decimal '參考基準點

        Dim Xi1 As Decimal '第一點 CCD(Pixel)
        Dim Xi2 As Decimal '第一點 CCD(Pixel)
        Dim Xi3 As Decimal '第一點 CCD(Pixel)
        Dim Yi1 As Decimal '第一點 CCD(Pixel)
        Dim Yi2 As Decimal '第一點 CCD(Pixel)
        Dim Yi3 As Decimal '第一點 CCD(Pixel)
        Dim Xo1 As Decimal '第一點 CCD(mm)
        Dim Xo2 As Decimal '第一點 CCD(mm)
        Dim Xo3 As Decimal '第一點 CCD(mm)
        Dim Yo1 As Decimal '第一點 CCD(mm)
        Dim Yo2 As Decimal '第一點 CCD(mm)
        Dim Yo3 As Decimal '第一點 CCD(mm)

        If sys.AxisX > -1 Then
            gCMotion.SetVelAccDec(sys.AxisX)
        End If
        If sys.AxisY > -1 Then
            gCMotion.SetVelAccDec(sys.AxisY)
        End If
        If sys.AxisZ > -1 Then
            gCMotion.SetVelAccDec(sys.AxisZ)
        End If
        If sys.AxisB > -1 Then
            gCMotion.SetVelAccDec(sys.AxisB)
        End If
        If sys.AxisC > -1 Then
            gCMotion.SetVelAccDec(sys.AxisC)
        End If

        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        TargetPos(0) = RefPosX
        TargetPos(1) = RefPosY
        TargetPos(2) = RefPosZ
        TargetPos(3) = 0
        TargetPos(4) = 0

        If ButtonSafeMovePos(sender, AxisNo, TargetPos, sys) = False Then 'TODO:增加例外離開
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   'btnScalePR.Enabled = True
                                   Btn_Control(True)
                               End Sub)
            End If
            Exit Sub
        End If


        'Dim sceneName As String = gCRecipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene

        '第一基準點 移動 取像 計算
        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, mScene, RefPosX, RefPosY, Xo0, Yo0, Xi0, Yi0) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   'btnScalePR.Enabled = True
                                   Btn_Control(True)
                               End Sub)
            End If

            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000011), "Alarm_2000011", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Alarm_2000011), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
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

            Exit Sub '例外中斷
        End If

        '輸入影像寬度
        Dim mImageWidth As Integer
        '輸入影像高度
        Dim mImageHeight As Integer
        Dim stepPitch As Decimal = 0.5
        gAOICollection.GetAcquistionSideLength(sys.CCDNo, mImageWidth, mImageHeight)
        'Dim mSceneName As String = gCRecipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene '"TB_ALIGN0" '場景編號
        Dim mTrainWidth As Decimal '教導Pattern大小
        Dim mTrainHeight As Decimal '教導Pattern大小
        Dim mRotation As Decimal = 0 '教導Pattern角度
        gAOICollection.GetAlignTrainSideLength(sys.CCDNo, mScene, mTrainWidth, mTrainHeight, mRotation)
        '第二基準點 移動
        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, mScene, RefPosX - stepPitch, RefPosY + stepPitch, Xo1, Yo1, Xi1, Yi1) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   'btnScalePR.Enabled = True
                                   Btn_Control(True)
                               End Sub)
            End If

            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000012), "Alarm_2000012", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Alarm_2000012), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
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

            Exit Sub '例外中斷
        End If


        '第三基準點 移動
        stepCount = 1
        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, mScene, RefPosX + stepPitch, RefPosY + stepPitch, Xo2, Yo2, Xi2, Yi2) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   'btnScalePR.Enabled = True
                                   Btn_Control(True)
                               End Sub)
            End If

            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000013), "Alarm_2000013", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Alarm_2000013), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
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

            Exit Sub '例外中斷
        End If

        '第四基準點 移動
        stepCount = 1
        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, mScene, RefPosX, RefPosY - stepPitch, Xo3, Yo3, Xi3, Yi3) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   'btnScalePR.Enabled = True
                                   Btn_Control(True)
                               End Sub)
            End If

            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000014), "Alarm_2000014", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Alarm_2000014), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
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

            Exit Sub '例外中斷
        End If

        '由基準往外相對移動量
        Xi1 -= Xi0 '相當於由中心點往外移動Pixel數
        Xi2 -= Xi0 '相當於由中心點往外移動Pixel數
        Xi3 -= Xi0 '相當於由中心點往外移動Pixel數
        Yi1 -= Yi0 '相當於由中心點往外移動Pixel數
        Yi2 -= Yi0 '相當於由中心點往外移動Pixel數
        Yi3 -= Yi0 '相當於由中心點往外移動Pixel數
        Xo1 -= Xo0 '相當於原特徵往外移動mm數
        Xo2 -= Xo0 '相當於原特徵往外移動mm數
        Xo3 -= Xo0 '相當於原特徵往外移動mm數
        Yo1 -= Yo0 '相當於原特徵往外移動mm數
        Yo2 -= Yo0 '相當於原特徵往外移動mm數
        Yo3 -= Yo0 '相當於原特徵往外移動mm數
        Debug.Print("I1 " & Xi1 & "," & Yi1)
        Debug.Print("O1 " & Xo1 & "," & Yo1)
        Debug.Print("I2 " & Xi2 & "," & Yi2)
        Debug.Print("O2 " & Xo2 & "," & Yo2)
        Debug.Print("I3 " & Xi3 & "," & Yi3)
        Debug.Print("O3 " & Xo3 & "," & Yo3)

        Dim A11 As Decimal
        Dim A12 As Decimal
        Dim A21 As Decimal
        Dim A22 As Decimal
        Dim B11 As Decimal
        Dim B21 As Decimal

        '三對點輸入輸出求轉移函數. 
        CMath.Point3CalcTranslation(Xi1, Xi2, Xi3, Yi1, Yi2, Yi3, Xo1, Xo2, Xo3, Yo1, Yo2, Yo3, A11, A12, A21, A22, B11, B21)
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6012003, A11, A12, A21, A22, B11, B21), "INFO_6012003")
        Dim targetPosX As Decimal
        Dim targetPosY As Decimal
        Dim TargetPixelX As Decimal
        Dim TargetPixelY As Decimal

        CMath.Rotation(mTrainWidth, mTrainHeight, mRotation, mTrainWidth, mTrainHeight) '旋轉修正

        TargetPixelX = mTrainWidth * 0.5 - Xi0    '左X
        TargetPixelY = mTrainHeight * 0.5 - Yi0    '上Y

        targetPosX = RefPosX + A11 * TargetPixelX + A12 * TargetPixelY '+ B11
        targetPosY = RefPosY + A21 * TargetPixelX + A22 * TargetPixelY '+ B21

        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, mScene, targetPosX, targetPosY, Xo1, Yo1, Xi1, Yi1) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   'btnScalePR.Enabled = True
                                   Btn_Control(True)
                               End Sub)
            End If

            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000015), "Alarm_2000015", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Alarm_2000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
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

            Exit Sub '例外中斷
        End If

        TargetPixelX = mImageWidth - mTrainWidth * 0.5 - Xi0   '右X
        TargetPixelY = mTrainHeight * 0.5 - Yi0    '上Y
        targetPosX = RefPosX + A11 * TargetPixelX + A12 * TargetPixelY '+ B11
        targetPosY = RefPosY + A21 * TargetPixelX + A22 * TargetPixelY '+ B21

        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, mScene, targetPosX, targetPosY, Xo2, Yo2, Xi2, Yi2) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   'btnScalePR.Enabled = True
                                   Btn_Control(True)
                               End Sub)
            End If

            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000016), "Alarm_2000016", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Alarm_2000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
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

            Exit Sub '例外中斷
        End If

        TargetPixelX = mTrainWidth * 0.5 - Xi0    '左X
        TargetPixelY = mImageHeight - mTrainHeight * 0.5 - Yi0   '下Y
        targetPosX = RefPosX + A11 * TargetPixelX + A12 * TargetPixelY '+ B11
        targetPosY = RefPosY + A21 * TargetPixelX + A22 * TargetPixelY '+ B21

        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, mScene, targetPosX, targetPosY, Xo3, Yo3, Xi3, Yi3) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   'btnScalePR.Enabled = True
                                   Btn_Control(True)
                               End Sub)
            End If

            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000017), "Alarm_2000017", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Alarm_2000017), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
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

            Exit Sub '例外中斷
        End If

        '由基準往外相對移動量
        Xi1 -= Xi0 '相當於由中心點往外移動Pixel數
        Xi2 -= Xi0 '相當於由中心點往外移動Pixel數
        Xi3 -= Xi0 '相當於由中心點往外移動Pixel數
        Yi1 -= Yi0 '相當於由中心點往外移動Pixel數
        Yi2 -= Yi0 '相當於由中心點往外移動Pixel數
        Yi3 -= Yi0 '相當於由中心點往外移動Pixel數
        Xo1 -= Xo0 '相當於原特徵往外移動mm數
        Xo2 -= Xo0 '相當於原特徵往外移動mm數
        Xo3 -= Xo0 '相當於原特徵往外移動mm數
        Yo1 -= Yo0 '相當於原特徵往外移動mm數
        Yo2 -= Yo0 '相當於原特徵往外移動mm數
        Yo3 -= Yo0 '相當於原特徵往外移動mm數
        CMath.Point3CalcTranslation(Xi1, Xi2, Xi3, Yi1, Yi2, Yi3, Xo1, Xo2, Xo3, Yo1, Yo2, Yo3, A11, A12, A21, A22, B11, B21)
        'Debug.Print("第二次 A11:" & A11 & ",A12:" & A12 & ",A21:" & A21 & ",A22:" & A22 & ",B11:" & B11 & ",B21:" & B21)
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6012003, A11, A12, A21, A22, B11, B21), "INFO_6012003")


        gSSystemParameter.CCDScaleX2X(sys.CCDNo) = A11
        gSSystemParameter.CCDScaleY2X(sys.CCDNo) = A12
        gSSystemParameter.CCDScaleX2Y(sys.CCDNo) = A21
        gSSystemParameter.CCDScaleY2Y(sys.CCDNo) = A22
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Me)) Then
            Me.BeginInvoke(Sub()
                               btnScalePR.BackColor = SystemColors.Control
                               txtA11.Text = gSSystemParameter.CCDScaleX2X(sys.CCDNo)
                               txtA12.Text = gSSystemParameter.CCDScaleY2X(sys.CCDNo)
                               txtA21.Text = gSSystemParameter.CCDScaleX2Y(sys.CCDNo)
                               txtA22.Text = gSSystemParameter.CCDScaleY2Y(sys.CCDNo)
                           End Sub)
        End If

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

        If ButtonSafeMovePos(sender, AxisNo, TargetPos, sys) = False Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   'btnScalePR.Enabled = True
                                   Btn_Control(True)
                               End Sub)
            End If
            Exit Sub
        End If
        '回起始位置

        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eSimplifiedChinese
                MsgString = "比例教導成功."
            Case enmLanguageType.eTraditionalChinese
                MsgString = "比例教導成功."
            Case Else
                MsgString = "CCD Scale done"
        End Select
        MsgBox(MsgString, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Me)) Then
            Me.BeginInvoke(Sub()
                               btnScalePR.BackColor = SystemColors.Control
                               'btnScalePR.Enabled = True
                               Btn_Control(True)
                           End Sub)
        End If

        'End Sub)
    End Sub

#End Region


#Region "定位教導"
    Private Sub btnTrainScene_Click(sender As Object, e As EventArgs) Handles btnTrainScene.Click

        If gSSystemParameter.CCDAlignModuleEnable Then
            '[Note]模組化測試中----------
            Dim mfrmCogToolBlock As frmAlignModule
            If btnTrainScene.Enabled = False Then
                Exit Sub
            End If
            gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnTrainScene]" & vbTab & "Click")
            Btn_Control(False)

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
                    .SceneName = mScene
                    .IsRecipeScene = False
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]更新光源控制項
                    UcLightControl1.ShowUI()
                End With
            Catch ex As Exception
                MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Try
            btnTrainScene.Enabled = True
            Btn_Control(True)
            '[Note]模組化測試中----------
        Else

            Dim mfrmCogToolBlock As frmCalibAlignTool '場景不存在也要能進去
            gSyslog.Save("[frmStageVerification]" & vbTab & "[btnTrainScene]" & vbTab & "Click")
            'btnTrainScene.Enabled = False
            Btn_Control(False)
            If gAOICollection.SetCCDScene(sys.CCDNo, lblSceneSet.Text) = False Then
                '場景不存在
                gSyslog.Save(lblSceneSet.Text & gMsgHandler.GetMessage(Warn_3000020))
                MsgBox(lblSceneSet.Text & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MsgBox("Scene (" & btnScene.Text & ") Not Exists!")
                mfrmCogToolBlock = New frmCalibAlignTool '場景不存在也要能進去
                With mfrmCogToolBlock
                    .Sys = gSYS(eSys.DispStage1)
                    .SceneName = lblSceneSet.Text
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    'cmbScence.Items.Clear()
                    'cmbScence.Items.AddRange(gAOICollection.GetAlignmentToolNameList(sys.CCDNo))
                    'cmbScence.SelectedItem = gfrmAlignPR01.lstScene.SelectedItem
                End With
                'btnTrainScene.Enabled = True
                Btn_Control(True)
                Exit Sub
            End If
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
            System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
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
                    'btnTrainScene.Enabled = True
                    Btn_Control(True)
                    Exit Sub
                End If
            Loop Until Not gAOICollection.IsCCDCBusy(sys.CCDNo) '取像完成
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉

            'Dim AlignCount As Integer = 1
            'Dim AlignSceneName() As String = gAOICollection.GetAlignmentToolNameList(sys.CCDNo)
            'If Not AlignSceneName Is Nothing Then
            '    For mSceneNo As Integer = 0 To AlignSceneName.Count - 1
            '        gAOICollection.SetAlignImage(sys.CCDNo, AlignSceneName(mSceneNo))
            '    Next
            'End If
            mfrmCogToolBlock = New frmCalibAlignTool
            With mfrmCogToolBlock
                .Sys = sys
                .SceneName = lblSceneSet.Text
                .StartPosition = FormStartPosition.Manual
                .Location = New Point(0, 0)
                .ShowDialog()
                'cmbScence.Items.Clear()
                'cmbScence.Items.AddRange(gAOICollection.GetAlignmentToolNameList(sys.CCDNo))
                'cmbScence.SelectedItem = gfrmAlignPR01.lstScene.SelectedItem
            End With
            'btnTrainScene.Enabled = True
            Btn_Control(True)
        End If
    End Sub


#End Region






End Class
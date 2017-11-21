Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectAOI


Imports System.IO


Public Class frmStageVerificationNew
    ''' <summary>目前類別名稱</summary>
    ''' <remarks></remarks>
    Private Const ClassName As String = "frmStageVerification"
    Dim mIsCanClosed As Boolean = True
    Dim SVTRunSpeed As Integer = 100
    Dim ActionSpeed As Integer = 100
    Public Flag_Pause As Boolean = False
    Public Flag_ManualDot As Boolean = False
    ''' <summary>對外接入操作系統設定, 或是內部配接使用</summary>
    ''' <remarks></remarks>
    Public sys As sSysParam

    Public Enum enmSVTmassage
        ''' <summary>驗機完成</summary>
        ''' <remarks></remarks>
        Done = 0
        ''' <summary>軸移動失敗</summary>
        ''' <remarks></remarks>
        AxisMove_ERROR = 1
        ''' <summary>軸移動超過時間</summary>
        ''' <remarks></remarks>
        AxisMove_TimeOut = 2
        ''' <summary>相機取像超過時間</summary>
        ''' <remarks></remarks>
        CCDAcq_TimeOut = 3
        ''' <summary>相機計算超過時間</summary>
        ''' <remarks></remarks>
        CCDCal_TimeOut = 4
        ''' <summary>使用者按下暫停</summary>
        ''' <remarks></remarks>
        Btn_Pause = 5
    End Enum

    ''' <summary>介面更新</summary>
    ''' <remarks></remarks>
    Public Sub RefreshUI()
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
            btnSetStartPos.Enabled = False
            btnGoStartPos.Enabled = False
            btnSetCornerPos.Enabled = False
            btnGoCornerPos.Enabled = False
            btnSetEndPos.Enabled = False
            btnGoEndPos.Enabled = False
            UcJoyStick1.Enabled = False
        Else
            btnSetStartPos.Enabled = True
            btnGoStartPos.Enabled = True
            btnSetCornerPos.Enabled = True
            btnGoCornerPos.Enabled = True
            btnSetEndPos.Enabled = True
            btnGoEndPos.Enabled = True
            UcJoyStick1.Enabled = True
        End If

    End Sub
    ''' <summary>
    ''' 軸移動時 btn保護
    ''' </summary>
    ''' <param name="disable"></param>
    ''' <remarks></remarks>
    Public Sub Btn_Control(ByVal disable As Boolean) 'Soni / 2017.05.16 確保是介面執行
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Me)) Then
            Me.BeginInvoke(Sub()
                               btnSetStartPos.Enabled = disable '起始點
                               btnGoStartPos.Enabled = disable
                               btnSetCornerPos.Enabled = disable  '轉角點
                               btnGoCornerPos.Enabled = disable
                               btnSetEndPos.Enabled = disable  '終點
                               btnGoEndPos.Enabled = disable
                               btnSetPitch.Enabled = disable '設定間隔

                               BtnHorizontalRun.Enabled = disable
                               BtnVerticalRun.Enabled = disable
                               BtnReRun.Enabled = disable

                               btnGoCCDAlignPos.Enabled = disable
                               btnCalibPos.Enabled = disable

                               UcJoyStick1.Enabled = disable

                               mIsCanClosed = disable
                           End Sub)
        End If

    End Sub
    ''' <summary>
    ''' 限制三點在同一平面上
    ''' </summary>
    ''' <param name="Pos"></param>
    ''' <remarks></remarks>
    Public Sub AxisZ_Control(ByVal Pos As Decimal) 'Soni / 2017.05.16 確保是介面執行
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Me)) Then
            Me.BeginInvoke(Sub()
                               nmcStartPosZ.Value = Pos
                               nmcCornerPosZ.Value = Pos
                               nmcEndPosZ.Value = Pos
                           End Sub)
        End If

    End Sub


    Private Sub frmStageAccuracy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height

        '[Note]:使用該組Stage的第一組閥
        sys.SelectValve = eValveWorkMode.Valve1
        ValveCylinderAction(sys.StageNo, sys.SelectValve, enmUpDown.Up) 'Soni + 2017.04.27 氣缸上 避免干涉
        lblStageNo.Text = "Stage" & (sys.StageNo + 1).ToString
        btnScene.Text = "CALIBSVT" & (sys.StageNo + 1).ToString
        lblAction.Text = "CALIBSVT"
        With gSSystemParameter.Pos.StageVerification(sys.StageNo)
            .Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini")

            nmcStartPosX.Value = .StageStartPosX
            nmcStartPosY.Value = .StageStartPosY
            nmcStartPosZ.Value = .StageStartPosZ
            nmcCornerPosX.Value = .StageCornerPosX
            nmcCornerPosY.Value = .StageCornerPosY
            nmcCornerPosZ.Value = .StageCornerPosZ
            nmcEndPosX.Value = .StageEndPosX
            nmcEndPosY.Value = .StageEndPosY
            nmcEndPosZ.Value = .StageEndPosZ
            nmcArrayXCount.Value = .ArrayXCount
            nmcArrayYCount.Value = .ArrayYCount
            nmcPitchHX.Value = .PitchHX
            nmcPitchHY.Value = .PitchHY
            nmcPitchVX.Value = .PitchVX
            nmcPitchVY.Value = .PitchVY
            lblAngle.Text = .StageVerificationAngle
            NumSteadyTime.Value = .SteadyTime
            txtStage1Data.Text = .StageVerificationData
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

        Dim fileName As String
        fileName = Application.StartupPath & "\System\" & MachineName & "\" & btnScene.Text & ".ini" 'System.IO.Path.GetDirectoryName(gCRecipe.strFileName) & "\" & SceneName & ".ini" '光源設定檔路徑
        gAOICollection.LoadSceneParameter(btnScene.Text, fileName) '讀取光源,曝光值等設定

        Select Case gAOICollection.GetCCDType(sys.CCDNo) 'gSSystemParameter.enmCCDType
            Case enmCCDType.CognexVPRO
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
            Case enmCCDType.OmronFZS2MUDP
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
            Case Else
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
        End Select
        '--- 2016.06.18 + 開燈,曝光,觸發後才會改 ---
        gAOICollection.SetCCDScene(sys.CCDNo, btnScene.Text) '曝光,亮度
        SelectScene(btnScene.Text) '場景開光

        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照
        System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        '--- 2016.06.18 + 開燈,曝光,觸發後才會改 ---

        UcLightControl1.CCDNo = sys.CCDNo
        UcLightControl1.SceneName = btnScene.Text
        UcLightControl1.ShowUI()

        With cmbXa.Items
            .Clear()
            For i As Integer = 0 To nmcArrayXCount.Value - 1
                .Add(i.ToString("000"))
            Next
        End With
        With cmbYa.Items
            .Clear()
            For i As Integer = 0 To nmcArrayYCount.Value - 1
                .Add(i.ToString("000"))
            Next
        End With

        btnCalibPos.Enabled = False
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

        If AnalysisVacuumStatus() = True Then
            btnVacuum.Text = "ON"
            btnVacuum.BackColor = Color.Yellow
        Else
            btnVacuum.Text = "OFF"
            btnVacuum.BackColor = SystemColors.Control
        End If

        '代表有設定檔案
        If txtStage1Data.TextLength > 0 Then
            gStageOffsetFromSCV(sys.StageNo) = New CStageCalibrationOffsetFromSCV()
            gStageOffsetFromSCV(sys.StageNo).LoadFile(txtStage1Data.Text, gSSystemParameter.StageFixMode)
            gStageOffsetFromSCV(sys.StageNo).Enable = True
            Dim Commresult As CommandStatus = CommandStatus.Alarm
            Commresult = gCMotion.Dev2DCompensateTable(enmAxis.XAxis, enmAxis.Y1Axis, gStageOffsetFromSCV(sys.StageNo).StartPositionX, gStageOffsetFromSCV(sys.StageNo).StartPositionY, gStageOffsetFromSCV(sys.StageNo).PitchX, gStageOffsetFromSCV(sys.StageNo).PitchY, gStageOffsetFromSCV(sys.StageNo).OffsetX, gStageOffsetFromSCV(sys.StageNo).OffsetY)
            If (Commresult = CommandStatus.Sucessed) Then
                gCMotion.Dev2DCompensateTableEnable(enmAxis.XAxis, enmAxis.Y1Axis, True)
            End If
        Else
            gStageOffsetFromSCV(sys.StageNo).Clear()
            gStageOffsetFromSCV(sys.StageNo).Enable = False
            txtStage1Data.Text = ""
            gCMotion.Dev2DCompensateTableEnable(enmAxis.XAxis, enmAxis.Y1Axis, False)
        End If



        RefreshUI() '介面更新
    End Sub

    Private Sub frmStageVerification_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If mIsCanClosed = False Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>關閉表單</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmStageVerification_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        UcDisplay1.EndLive()
        'Eason 20170124 Ticket:100031 , Memory Freed [S]
        UcDisplay1.ManualDispose()
        UcLightControl1.ManualDispose()
        UcJoyStick1.ManualDispose()
        Me.Dispose(True)
        'Eason 20170124 Ticket:100031 , Memory Freed [E]
    End Sub

    ''' <summary>選擇場景, 開啟光源</summary>
    ''' <param name="sceneName"></param>
    ''' <remarks></remarks>
    Sub SelectScene(ByVal sceneName As String)
        If gAOICollection.SceneDictionary.ContainsKey(sceneName) Then
            Dim light As enmLight
            Dim lightValue1 As Integer
            Dim lightValue2 As Integer
            Dim lightValue3 As Integer
            Dim lightValue4 As Integer
            Dim lightEnable1 As Boolean
            Dim lightEnable2 As Boolean
            Dim lightEnable3 As Boolean
            Dim lightEnable4 As Boolean
            lightValue1 = gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No1)
            lightValue2 = gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No2)
            lightValue3 = gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No3)
            lightValue4 = gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No4)
            lightEnable1 = gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No1)
            lightEnable2 = gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No2)
            lightEnable3 = gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No3)
            lightEnable4 = gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No4)
            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)

            gLightCollection.SetCCDLight(sys.CCDNo, light, lightValue1, True)
            gSysAdapter.SetLightOnOff(light, lightEnable1)
            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
            gLightCollection.SetCCDLight(sys.CCDNo, light, lightValue2, True)
            gSysAdapter.SetLightOnOff(light, lightEnable2)
            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3)
            gLightCollection.SetCCDLight(sys.CCDNo, light, lightValue3, True)
            gSysAdapter.SetLightOnOff(light, lightEnable3)
            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4)
            gLightCollection.SetCCDLight(sys.CCDNo, light, lightValue4, True)
            gSysAdapter.SetLightOnOff(light, lightEnable4)

        Else
            '場景不存在
            gSyslog.Save(sceneName & gMsgHandler.GetMessage(Warn_3000020))
            MsgBox(sceneName & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox(sceneName & " Not Exists!")
        End If
    End Sub

    Function SetAxisSpeed(ByVal Axis As Integer, ByVal VelLow As Integer, ByVal VelHigh As Integer, ByVal Acc As Integer, ByVal Dec As Integer) As Boolean
        '[說明]:設定移動速度

        If gCMotion.SetVelLow(Axis, VelLow) <> CommandStatus.Sucessed Then
            gSyslog.Save("CALIB  Axis" & Axis & " SetVelLow " & VelLow & " Fail!  ")
            Return False
        End If
        If gCMotion.SetVelHigh(Axis, VelHigh) <> CommandStatus.Sucessed Then
            gSyslog.Save("CALIB  Axis" & Axis & " SetVelHigh " & VelHigh & " Fail!  ")
            Return False
        End If
        If gCMotion.SetAcc(Axis, Acc) <> CommandStatus.Sucessed Then
            gSyslog.Save("CALIB  Axis" & Axis & " SetAcc " & Acc & " Fail!  ")
            Return False
        End If
        If gCMotion.SetDec(Axis, Dec) <> CommandStatus.Sucessed Then
            gSyslog.Save("CALIB  Axis" & Axis & " SetDec " & Dec & " Fail!  ")
            Return False
        End If

        Return True
    End Function
    Dim str As String
    Private Sub btnSetPitch_Click(sender As Object, e As EventArgs) Handles btnSetPitch.Click
        gSyslog.Save("[frmStageVerification]" & vbTab & "[btnSetPitchX]" & vbTab & "Click")
        btnSetPitch.Enabled = False
        If Val(nmcArrayXCount.Value) < 2 Then
            MsgBox(lblArrayXCount.Text & " Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetPitch.Enabled = True
            Exit Sub
        End If
        If Val(nmcArrayYCount.Value) = 0 Then
            MsgBox(lblArrayYCount.Text & " Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetPitch.Enabled = True
            Exit Sub
        End If

        If nmcStartPosX.Value = 0 Or nmcCornerPosX.Value = 0 Or nmcEndPosX.Value = 0 Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            'MsgBox(" Please Set 3 Points! ")
            Exit Sub
        End If

        'Dim DistanceH As Decimal '= Val(nmcCornerPosX.Value - nmcStartPosX.Value)
        'Dim DistanceV As Decimal '= Val(nmcEndPosY.Value - nmcCornerPosY.Value)
        'nmcArrayXPitch.Value = Math.Round(DistanceX / (Val(nmcArrayXCount.Value) - 1), 3)
        'nmcArrayYPitch.Value = Math.Round(DistanceY / (Val(nmcArrayYCount.Value) - 1), 3)
        'DistanceH = CalPointToPointDistance(nmcStartPosX.Value, nmcStartPosY.Value, nmcCornerPosX.Value, nmcCornerPosY.Value) '水平方向
        'DistanceV = CalPointToPointDistance(nmcCornerPosX.Value, nmcCornerPosY.Value, nmcEndPosX.Value, nmcEndPosY.Value) '垂直方向

        Dim PitchHX As Decimal
        Dim PitchHY As Decimal
        Dim PitchVX As Decimal
        Dim PitchVY As Decimal
        Dim CalibAngle As Decimal
        PitchHX = Math.Round((nmcCornerPosX.Value - nmcStartPosX.Value) / (Val(nmcArrayXCount.Value) - 1), 4)
        PitchHY = Math.Round((nmcCornerPosY.Value - nmcStartPosY.Value) / (Val(nmcArrayXCount.Value) - 1), 4)
        PitchVX = Math.Round((nmcEndPosX.Value - nmcCornerPosX.Value) / (Val(nmcArrayYCount.Value) - 1), 4)
        PitchVY = Math.Round((nmcEndPosY.Value - nmcCornerPosY.Value) / (Val(nmcArrayYCount.Value) - 1), 4)
        nmcPitchHX.Value = PitchHX
        nmcPitchHY.Value = PitchHY
        nmcPitchVX.Value = PitchVX
        nmcPitchVY.Value = PitchVY


        Dim center, start, final As New CPoint
        center.PointX = nmcStartPosX.Value
        center.PointY = nmcStartPosY.Value
        final.PointX = nmcStartPosX.Value + 50 '延伸的水平線
        final.PointY = nmcStartPosY.Value
        start.PointX = nmcCornerPosX.Value
        start.PointY = nmcCornerPosY.Value


        CalibAngle = CMath.GetAngleby3Point(center, start, final, CMath.GetClockwiseby3Point(center, start, final))
        lblAngle.Text = Math.Round(CalibAngle, 3)
        Dim NeCalibAngle As Decimal = -CalibAngle


        '=====wenda test 
        Dim outputX, outputY As Decimal
        CMath.TransformRotation(False, NeCalibAngle, center.PointX, center.PointY, start.PointX, start.PointY, outputX, outputY)
        str = "NeCalibAngle: " & NeCalibAngle & "  OutPutX: " & outputX & "   outputY: " & outputY
        txtList.Text = str
        '=====wenda test


        btnSetPitch.Enabled = True

    End Sub

    Function AnalysisPitchResult() As Boolean
        Dim AccuracyUnit As Decimal = 0.005
        Debug.WriteLine("X AccuracyUnit: " & (CDec(nmcStartPosX.Value) + CDec(nmcArrayXCount.Value - 1) * Val(nmcPitchHX.Value)) - CDec(nmcCornerPosX.Value))
        Debug.WriteLine("Y AccuracyUnit: " & (CDec(nmcCornerPosY.Value) + CDec(nmcArrayYCount.Value - 1) * Val(nmcPitchVY.Value)) - CDec(nmcEndPosY.Value))
        If Math.Abs((CDec(nmcStartPosX.Value) + CDec(nmcArrayXCount.Value - 1) * Val(nmcPitchHX.Value)) - CDec(nmcCornerPosX.Value)) > AccuracyUnit Then
            Return False
        End If
        If Math.Abs((CDec(nmcCornerPosY.Value) + CDec(nmcArrayYCount.Value - 1) * Val(nmcPitchVY.Value)) - CDec(nmcEndPosY.Value)) > AccuracyUnit Then
            Return False
        End If
        Return True
    End Function

    Function CalPointToPointDistance(ByVal Point1X As Decimal, ByVal Point1Y As Decimal, ByVal Point2X As Decimal, ByVal Point2Y As Decimal)
        Dim Distance As Decimal
        Distance = Math.Sqrt((Point2X - Point1X) ^ 2 + (Point2Y - Point1Y) ^ 2)
        Debug.WriteLine("Dis: " & Distance)
        Return Distance
    End Function
    Private Async Sub btnSetStartPos_Click(sender As Object, e As EventArgs) Handles btnSetStartPos.Click  'Soni / 2017.05.16 修改DoEvents
        gSyslog.Save("[frmStageVerification]" & vbTab & "[btnSetStartPos]" & vbTab & "Click")
        'btnSetStartPos.Enabled = False
        Btn_Control(False)

        Dim timeOutStopWatch As New Stopwatch '逾時計時器
        Dim mOriginPosX As Decimal
        Dim mOriginPosY As Decimal
        Dim mOriginPosZ As Decimal

        Dim Count As Integer = 0
        Dim AccuracyUnit As Decimal = 0.0015

        Dim offsetX As Decimal = 0 '特徵點距離CCD中心的距離(mm)
        Dim offsetY As Decimal = 0
        Dim targetPosX As Decimal
        Dim targetPosY As Decimal
        Dim targetPosZ As Decimal
        Await Task.Run(Sub()
                           Do
                               '將X軸與Y軸移動到收斂
                               mOriginPosX = gCMotion.GetPositionValue(sys.AxisX)
                               mOriginPosY = gCMotion.GetPositionValue(sys.AxisY)
                               mOriginPosZ = gCMotion.GetPositionValue(sys.AxisZ) '限制三點同平面
                               '=== Z軸到指定位置 ===
                               targetPosZ = mOriginPosZ
                               If gCMotion.AbsMove(sys.AxisZ, targetPosZ) <> CommandStatus.Sucessed Then
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If

                               timeOutStopWatch.Restart()
                               Do
                                   UcJoyStick1.RefreshPosition()
                                   System.Threading.Thread.Sleep(1)
                                   If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                       Exit Do
                                   ElseIf timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
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
                                       Btn_Control(True)
                                       Exit Sub
                                   End If
                               Loop


                               '=== Z軸到指定位置 ===
                               '=== XY到指定位置 ===
                               targetPosX = mOriginPosX - offsetX
                               targetPosY = mOriginPosY - offsetY
                               If gCMotion.AbsMove(sys.AxisX, targetPosX) <> CommandStatus.Sucessed Then
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                               If gCMotion.AbsMove(sys.AxisY, targetPosY) <> CommandStatus.Sucessed Then
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                               System.Threading.Thread.Sleep(100) 'Test: 先等100ms再判斷
                               timeOutStopWatch.Restart()
                               Do
                                   UcJoyStick1.RefreshPosition()
                                   System.Threading.Thread.Sleep(1)
                                   If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) = CommandStatus.Sucessed Then
                                       Exit Do
                                   ElseIf timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                       'X & Y 軸移動逾時
                                       gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080010))
                                       MsgBox(gMsgHandler.GetMessage(Alarm_2080010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       'MsgBox("Axis X,Y Wait Inposition Time Out!!!", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Btn_Control(True)
                                       Exit Sub
                                   End If
                               Loop
                               '=== XY到指定位置 ===

                               '[Note]:確認目前的狀態是可以取像
                               Do
                                   System.Threading.Thread.Sleep(1)
                                   If gAOICollection.IsCCDReady(sys.CCDNo) = True Then
                                       Exit Do
                                   End If
                               Loop Until False

                               '拍照取得特徵點中心位置
                               gAOICollection.SetCCDScene(sys.CCDNo, btnScene.Text) '選擇場景
                               Dim ticket As Integer = 0
                               gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
                               System.Threading.Thread.CurrentThread.Join(100)
                               ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
                               System.Threading.Thread.CurrentThread.Join(100)
                               gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保

                               timeOutStopWatch.Restart()
                               Do
                                   System.Threading.Thread.Sleep(1)
                                   If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
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

                                       Btn_Control(True)
                                       Exit Sub
                                   End If
                               Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
                               Debug.Print("IsCCDCBusy:" & timeOutStopWatch.ElapsedMilliseconds)
                               'InvokeUcDisplay(UcDisplay1, gAOICollection, sys, sceneID,0,0) '更新控制項,必要條件 frmMain必須是實體
                               timeOutStopWatch.Restart()
                               Do
                                   System.Threading.Thread.Sleep(1)
                                   If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                                       If gCCDAlignResultDict(sys.CCDNo)(ticket).IsRunSuccess = True Then
                                           Exit Do
                                       End If
                                   End If
                                   If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
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

                                       Btn_Control(True)
                                       Exit Sub
                                   End If
                               Loop Until False
                               System.Threading.Thread.Sleep(10)
                               If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If

                               If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count > 1 Then '畫面有多個特徵
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                               System.Threading.Thread.Sleep(10)
                               '=== 取得拍照結果 ===
                               offsetX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
                               offsetY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY

                               'Debug.WriteLine("X精準度: " & Math.Abs(offsetX) & "  Y精準度: " & Math.Abs(offsetY) & "  Count: " & Count)
                               Count = Count + 1
                           Loop Until ((Math.Abs(offsetX) < AccuracyUnit) And (Math.Abs(offsetY) < AccuracyUnit)) Or Count > 5
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  nmcStartPosX.Value = gCMotion.GetPositionValue(sys.AxisX)
                                                  nmcStartPosY.Value = gCMotion.GetPositionValue(sys.AxisY)
                                              End Sub)
                           End If

                           'nmcStartPosZ.Value = gCMotion.GetPositionValue(sys.AxisZ)
                           AxisZ_Control(gCMotion.GetPositionValue(sys.AxisZ))


                           'btnSetStartPos.Enabled = True
                           Btn_Control(True)
                       End Sub)

    End Sub

    Private Sub btnGoStartPos_Click(sender As Object, e As EventArgs) Handles btnGoStartPos.Click
        gSyslog.Save("[frmStageVerification]" & vbTab & "[btnGoStartPos]" & vbTab & "Click")

        '[說明]:位置安全保護
        If CheckGoPos(Val(nmcStartPosX.Value), Val(nmcStartPosY.Value), Val(nmcStartPosZ.Value)) = False Then
            Exit Sub
        End If
        'btnGoStartPos.Enabled = False
        Btn_Control(False)
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            BtnHomeFirstBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            'btnGoStartPos.Enabled = True
            Btn_Control(True)
            Exit Sub
        End If

        '[說明]:X、Y、Z軸
        gCMotion.SetVelAccDec(sys.AxisX)
        gCMotion.SetVelAccDec(sys.AxisY)
        gCMotion.SetVelAccDec(sys.AxisZ)
        gCMotion.SetVelAccDec(sys.AxisB)
        gCMotion.SetVelAccDec(sys.AxisC)

        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        TargetPos(0) = Val(nmcStartPosX.Value)
        TargetPos(1) = Val(nmcStartPosY.Value)
        TargetPos(2) = Val(nmcStartPosZ.Value)

        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        UcJoyStick1.RefreshPosition()
        'btnGoStartPos.Enabled = True
        Btn_Control(True)
    End Sub

    Private Async Sub btnSetCornerPos_Click(sender As Object, e As EventArgs) Handles btnSetCornerPos.Click  'Soni / 2017.05.16 去除DoEvents
        gSyslog.Save("[frmStageVerification]" & vbTab & "[btnSetCornerPos]" & vbTab & "Click")
        Await Task.Run(Sub()


                           Btn_Control(False)
                           Dim timeOutStopWatch As New Stopwatch '逾時計時器
                           Dim mOriginPosX As Decimal
                           Dim mOriginPosY As Decimal
                           Dim mOriginPosZ As Decimal

                           Dim Count As Integer = 0
                           Dim AccuracyUnit As Decimal = 0.0015

                           Dim offsetX As Decimal = 0 '特徵點距離CCD中心的距離(mm)
                           Dim offsetY As Decimal = 0
                           Dim targetPosX As Decimal
                           Dim targetPosY As Decimal
                           Dim targetPosZ As Decimal
                           Do
                               '將X軸與Y軸移動到收斂
                               mOriginPosX = gCMotion.GetPositionValue(sys.AxisX)
                               mOriginPosY = gCMotion.GetPositionValue(sys.AxisY)
                               mOriginPosZ = gCMotion.GetPositionValue(sys.AxisZ)
                               '=== Z軸到指定位置 ===
                               targetPosZ = mOriginPosZ
                               If gCMotion.AbsMove(sys.AxisZ, targetPosZ) <> CommandStatus.Sucessed Then
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                               timeOutStopWatch.Restart()
                               Do
                                   UcJoyStick1.RefreshPosition()
                                   System.Threading.Thread.Sleep(1)
                                   If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                       Exit Do
                                   ElseIf timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
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
                                       Btn_Control(True)
                                       Exit Sub
                                   End If
                               Loop
                               '=== Z軸到指定位置 ===
                               '=== XY到指定位置 ===
                               targetPosX = mOriginPosX - offsetX
                               targetPosY = mOriginPosY - offsetY
                               If gCMotion.AbsMove(sys.AxisX, targetPosX) <> CommandStatus.Sucessed Then
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                               If gCMotion.AbsMove(sys.AxisY, targetPosY) <> CommandStatus.Sucessed Then
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                               System.Threading.Thread.Sleep(100) '等待一段時間再判斷
                               timeOutStopWatch.Restart()
                               Do
                                   UcJoyStick1.RefreshPosition()
                                   System.Threading.Thread.Sleep(1)
                                   If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) = CommandStatus.Sucessed Then
                                       Exit Do
                                   ElseIf timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                       'X & Y 軸移動逾時
                                       gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080010))
                                       MsgBox(gMsgHandler.GetMessage(Alarm_2080010))
                                       Btn_Control(True)
                                       Exit Sub
                                   End If
                               Loop
                               '=== XY到指定位置 ===

                               '[Note]:確認目前的狀態是可以取像
                               Do
                                   System.Threading.Thread.Sleep(1)
                                   If gAOICollection.IsCCDReady(sys.CCDNo) = True Then
                                       Exit Do
                                   End If
                               Loop Until False

                               '拍照取得特徵點中心位置
                               gAOICollection.SetCCDScene(sys.CCDNo, btnScene.Text) '選擇場景
                               Dim ticket As Integer = 0
                               gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
                               System.Threading.Thread.CurrentThread.Join(10)
                               ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
                               System.Threading.Thread.CurrentThread.Join(10)
                               gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保
                               timeOutStopWatch.Restart()
                               Do
                                   System.Threading.Thread.Sleep(1)
                                   If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
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

                                       Btn_Control(True)
                                       Exit Sub
                                   End If
                               Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
                               Debug.Print("IsCCDCBusy:" & timeOutStopWatch.ElapsedMilliseconds)
                               'InvokeUcDisplay(UcDisplay1, gAOICollection, sys, sceneID) '更新控制項,必要條件 frmMain必須是實體
                               timeOutStopWatch.Restart()
                               Do
                                   System.Threading.Thread.Sleep(1)
                                   If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                                       If gCCDAlignResultDict(sys.CCDNo)(ticket).IsRunSuccess = True Then
                                           Exit Do
                                       End If
                                   End If
                                   If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
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

                                       Btn_Control(True)
                                       Exit Sub
                                   End If
                               Loop Until False
                               System.Threading.Thread.Sleep(10)
                               If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                               System.Threading.Thread.Sleep(10)
                               If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count > 1 Then '畫面有多個特徵
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                               '=== 取得拍照結果 ===
                               offsetX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
                               offsetY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
                               'Debug.WriteLine("X精準度: " & Math.Abs(offsetX) & "  Y精準度: " & Math.Abs(offsetY) & "  Count: " & Count)
                               Count = Count + 1
                           Loop Until ((Math.Abs(offsetX) < AccuracyUnit) And (Math.Abs(offsetY) < AccuracyUnit)) Or Count > 5
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  nmcCornerPosX.Value = gCMotion.GetPositionValue(sys.AxisX)
                                                  nmcCornerPosY.Value = gCMotion.GetPositionValue(sys.AxisY)
                                                  nmcCornerPosZ.Value = gCMotion.GetPositionValue(sys.AxisZ)
                                              End Sub)
                           End If
                           AxisZ_Control(gCMotion.GetPositionValue(sys.AxisZ))

                           'btnSetCornerPos.Enabled = True
                           Btn_Control(True)
                       End Sub)
    End Sub

    Private Sub btnGoCornerPos_Click(sender As Object, e As EventArgs) Handles btnGoCornerPos.Click
        gSyslog.Save("[frmStageVerification]" & vbTab & "[btnGoCornerPos]" & vbTab & "Click")
        '[說明]:位置安全保護
        If CheckGoPos(Val(nmcCornerPosX.Value), Val(nmcCornerPosY.Value), Val(nmcStartPosZ.Value)) = False Then
            Exit Sub
        End If
        'btnGoCornerPos.Enabled = False
        Btn_Control(False)
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            BtnHomeFirstBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            'btnGoCornerPos.Enabled = True
            Btn_Control(True)
            Exit Sub
        End If

        '[說明]:X、Y、Z軸
        gCMotion.SetVelAccDec(sys.AxisX)
        gCMotion.SetVelAccDec(sys.AxisY)
        gCMotion.SetVelAccDec(sys.AxisZ)
        gCMotion.SetVelAccDec(sys.AxisB)
        gCMotion.SetVelAccDec(sys.AxisC)

        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        TargetPos(0) = Val(nmcCornerPosX.Value)
        TargetPos(1) = Val(nmcCornerPosY.Value)
        TargetPos(2) = Val(nmcStartPosZ.Value) 'Val(nmcCornerPosZ.Value) 限制三個定位點在同平面上

        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        UcJoyStick1.RefreshPosition()
        'btnGoCornerPos.Enabled = True
        Btn_Control(True)
    End Sub

    Private Async Sub btnSetEndPos_Click(sender As Object, e As EventArgs) Handles btnSetEndPos.Click  'Soni / 2017.05.16 去除DoEvents
        Await Task.Run(Sub()

                           gSyslog.Save("[frmStageVerification]" & vbTab & "[btnSetEndPos]" & vbTab & "Click")
                           'btnSetEndPos.Enabled = False
                           Btn_Control(False)

                           Dim timeOutStopWatch As New Stopwatch '逾時計時器
                           Dim mOriginPosX As Decimal
                           Dim mOriginPosY As Decimal
                           Dim mOriginPosZ As Decimal

                           Dim Count As Integer = 0
                           Dim AccuracyUnit As Decimal = 0.0015

                           Dim offsetX As Decimal = 0 '特徵點距離CCD中心的距離(mm)
                           Dim offsetY As Decimal = 0
                           Dim targetPosX As Decimal
                           Dim targetPosY As Decimal
                           Dim targetPosZ As Decimal

                           Do
                               '將X軸與Y軸移動到收斂
                               mOriginPosX = gCMotion.GetPositionValue(sys.AxisX)
                               mOriginPosY = gCMotion.GetPositionValue(sys.AxisY)
                               mOriginPosZ = gCMotion.GetPositionValue(sys.AxisZ)  '限制三點同平面
                               '=== Z軸到指定位置 ===
                               targetPosZ = mOriginPosZ
                               If gCMotion.AbsMove(sys.AxisZ, targetPosZ) <> CommandStatus.Sucessed Then
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                               timeOutStopWatch.Restart()
                               Do
                                   UcJoyStick1.RefreshPosition()
                                   System.Threading.Thread.Sleep(1)
                                   If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                       Exit Do
                                   ElseIf timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
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
                                       Btn_Control(True)
                                       Exit Sub
                                   End If
                               Loop
                               '=== Z軸到指定位置 ===
                               '=== XY到指定位置 ===
                               targetPosX = mOriginPosX - offsetX
                               targetPosY = mOriginPosY - offsetY
                               If gCMotion.AbsMove(sys.AxisX, targetPosX) <> CommandStatus.Sucessed Then
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                               If gCMotion.AbsMove(sys.AxisY, targetPosY) <> CommandStatus.Sucessed Then
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                               System.Threading.Thread.Sleep(100) '等待一段時間再判斷
                               timeOutStopWatch.Restart()
                               Do
                                   UcJoyStick1.RefreshPosition()
                                   System.Threading.Thread.Sleep(1)
                                   If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) = CommandStatus.Sucessed Then
                                       Exit Do
                                   ElseIf timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                       'X & Y 軸移動逾時
                                       gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080010))
                                       MsgBox(gMsgHandler.GetMessage(Alarm_2080010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Btn_Control(True)
                                       Exit Sub
                                   End If
                               Loop
                               '=== XY到指定位置 ===

                               '[Note]:確認目前的狀態是可以取像
                               Do
                                   System.Threading.Thread.Sleep(1)
                                   If gAOICollection.IsCCDReady(sys.CCDNo) = True Then
                                       Exit Do
                                   End If
                               Loop Until False

                               '拍照取得特徵點中心位置
                               gAOICollection.SetCCDScene(sys.CCDNo, btnScene.Text) '選擇場景
                               Dim ticket As Integer = 0
                               gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
                               System.Threading.Thread.CurrentThread.Join(10)
                               ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
                               System.Threading.Thread.CurrentThread.Join(10)
                               gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保

                               timeOutStopWatch.Restart()
                               Do
                                   System.Threading.Thread.Sleep(1)
                                   If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
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

                                       Btn_Control(True)
                                       Exit Sub
                                   End If
                               Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
                               Debug.Print("IsCCDCBusy:" & timeOutStopWatch.ElapsedMilliseconds)
                               'InvokeUcDisplay(UcDisplay1, gAOICollection, sys, sceneID) '更新控制項,必要條件 frmMain必須是實體
                               timeOutStopWatch.Restart()
                               Do
                                   System.Threading.Thread.Sleep(1)
                                   If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                                       If gCCDAlignResultDict(sys.CCDNo)(ticket).IsRunSuccess = True Then
                                           Exit Do
                                       End If
                                   End If
                                   If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
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

                                       Btn_Control(True)
                                       Exit Sub
                                   End If
                               Loop Until False
                               System.Threading.Thread.Sleep(10)
                               If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
                                   'CCD 找不到特徵點
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If

                               If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count > 1 Then '畫面有多個特徵
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                               System.Threading.Thread.Sleep(10)
                               '=== 取得拍照結果 ===
                               offsetX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
                               offsetY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY

                               'Debug.WriteLine("X精準度: " & Math.Abs(offsetX) & "  Y精準度: " & Math.Abs(offsetY) & "  Count: " & Count)
                               Count = Count + 1
                           Loop Until ((Math.Abs(offsetX) < AccuracyUnit) And (Math.Abs(offsetY) < AccuracyUnit)) Or Count > 5
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  nmcEndPosX.Value = gCMotion.GetPositionValue(sys.AxisX)
                                                  nmcEndPosY.Value = gCMotion.GetPositionValue(sys.AxisY)
                                              End Sub)
                           End If

                           AxisZ_Control(gCMotion.GetPositionValue(sys.AxisZ)) '限制三點在同一平面上
                           Btn_Control(True)
                       End Sub)
    End Sub

    Private Sub btnGoEndPos_Click(sender As Object, e As EventArgs) Handles btnGoEndPos.Click
        gSyslog.Save("[frmStageVerification]" & vbTab & "[btnGoEndPos]" & vbTab & "Click")
        '[說明]:位置安全保護
        If CheckGoPos(Val(nmcEndPosX.Value), Val(nmcEndPosY.Value), Val(nmcStartPosZ.Value)) = False Then
            Exit Sub
        End If
        'btnGoEndPos.Enabled = False
        Btn_Control(False)
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            BtnHomeFirstBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            'btnGoEndPos.Enabled = True
            Btn_Control(True)
            Exit Sub
        End If

        '[說明]:X、Y、Z軸
        gCMotion.SetVelAccDec(sys.AxisX)
        gCMotion.SetVelAccDec(sys.AxisY)
        gCMotion.SetVelAccDec(sys.AxisZ)
        gCMotion.SetVelAccDec(sys.AxisB)
        gCMotion.SetVelAccDec(sys.AxisC)

        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        TargetPos(0) = Val(nmcEndPosX.Value)
        TargetPos(1) = Val(nmcEndPosY.Value)
        TargetPos(2) = Val(nmcStartPosZ.Value) 'Val(nmcEndPosZ.Value) '限制三個定位點在同平面上

        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        UcJoyStick1.RefreshPosition()
        'btnGoEndPos.Enabled = True
        Btn_Control(True)
    End Sub


    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmStageVerification]" & vbTab & "[btnOK]" & vbTab & "Click.")
        btnOK.Enabled = False
        'UcDisplay1.EndLive()
        With gSSystemParameter.Pos.StageVerification(sys.StageNo)

            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Start")
            gSyslog.Save("Stage StartPosX:" & .StageStartPosX)
            gSyslog.Save("Stage StartPosY:" & .StageStartPosY)
            gSyslog.Save("Stage StartPosZ:" & .StageStartPosZ)

            gSyslog.Save("Stage CornerPosX:" & .StageCornerPosX)
            gSyslog.Save("Stage CornerPosY:" & .StageCornerPosX)
            gSyslog.Save("Stage CornerPosZ:" & .StageCornerPosX)

            gSyslog.Save("Stage EndPosX:" & .StageEndPosX)
            gSyslog.Save("Stage EndPosY:" & .StageEndPosX)
            gSyslog.Save("Stage EndPosZ:" & .StageEndPosX)
            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ")  End")

            .StageVerificationData = txtStage1Data.Text
            '平台精準度驗證 起始位置
            .StageStartPosX = CDec(nmcStartPosX.Value)
            .StageStartPosY = CDec(nmcStartPosY.Value)
            .StageStartPosZ = CDec(nmcStartPosZ.Value)
            '平台精準度驗證 轉角位置
            .StageCornerPosX = CDec(nmcCornerPosX.Value)
            .StageCornerPosY = CDec(nmcCornerPosY.Value)
            .StageCornerPosZ = CDec(nmcCornerPosZ.Value)
            '平台精準度驗證 結束位置
            .StageEndPosX = CDec(nmcEndPosX.Value)
            .StageEndPosY = CDec(nmcEndPosY.Value)
            .StageEndPosZ = CDec(nmcEndPosZ.Value)

            '陣列數量
            .ArrayXCount = CDec(nmcArrayXCount.Value)
            .ArrayYCount = CDec(nmcArrayYCount.Value)
            .PitchHX = CDec(nmcPitchHX.Value)
            .PitchHY = CDec(nmcPitchHY.Value)
            .PitchVX = CDec(nmcPitchVX.Value)
            .PitchVY = CDec(nmcPitchVY.Value)
            .StageVerificationAngle = CDec(lblAngle.Text)
            '穩定時間
            .SteadyTime = CInt(NumSteadyTime.Value)

            .SaveParameter(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini") '儲存設定
            .Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini") '設定間距


            gSyslog.Save("New Parameter - Stage(" & (sys.StageNo + 1).ToString & ") Start")
            gSyslog.Save("Stage StartPosX:" & .StageStartPosX)
            gSyslog.Save("Stage StartPosY:" & .StageStartPosY)
            gSyslog.Save("Stage StartPosZ:" & .StageStartPosZ)

            gSyslog.Save("Stage CornerPosX:" & .StageCornerPosX)
            gSyslog.Save("Stage CornerPosY:" & .StageCornerPosX)
            gSyslog.Save("Stage CornerPosZ:" & .StageCornerPosX)

            gSyslog.Save("Stage EndPosX:" & .StageEndPosX)
            gSyslog.Save("Stage EndPosY:" & .StageEndPosX)
            gSyslog.Save("Stage EndPosZ:" & .StageEndPosX)
            gSyslog.Save("Old Parameter - Stage(" & (sys.StageNo + 1).ToString & ")  End")



            Dim SceneName As String = btnScene.Text
            Dim fileName As String = Application.StartupPath & "\System\" & MachineName & "\" & btnScene.Text & ".ini" '光源設定檔路徑

            gAOICollection.SaveSceneParameter(SceneName, fileName)
        End With

        'Sue20170627
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        btnOK.Enabled = True

        'Me.Close()
    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmStageVerification]" & vbTab & "[btnCancel]" & vbTab & "Click.")
        Me.Close()
    End Sub

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
            If gAOICollection.SetCCDScene(sys.CCDNo, btnScene.Text) Then
                mScene = btnScene.Text
            Else
                '場景不存在
                gSyslog.Save(btnScene.Text & gMsgHandler.GetMessage(Warn_3000020))
                MsgBox(btnScene.Text & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
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
            Btn_Control(True)
            '[Note]模組化測試中----------
        Else

            Dim mfrmCogToolBlock As frmCalibAlignTool '場景不存在也要能進去
            gSyslog.Save("[frmStageVerification]" & vbTab & "[btnTrainScene]" & vbTab & "Click")
            'btnTrainScene.Enabled = False
            Btn_Control(False)
            If gAOICollection.SetCCDScene(sys.CCDNo, btnScene.Text) = False Then
                '場景不存在
                gSyslog.Save(btnScene.Text & gMsgHandler.GetMessage(Warn_3000020))
                MsgBox(btnScene.Text & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MsgBox("Scene (" & btnScene.Text & ") Not Exists!")
                mfrmCogToolBlock = New frmCalibAlignTool '場景不存在也要能進去
                With mfrmCogToolBlock
                    .Sys = gSYS(eSys.DispStage1)
                    .SceneName = btnScene.Text
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
                .SceneName = btnScene.Text
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

    Private Async Sub btnGoCCDAlignPos_Click(sender As Object, e As EventArgs) Handles btnGoCCDAlignPos.Click  'Soni / 2017.05.16 去除DoEvents
        gSyslog.Save("[frmStageVerification]" & vbTab & "[btnSetEndPos]" & vbTab & "Click")

        If nmcStartPosX.Value = 0 Or nmcCornerPosX.Value = 0 Or nmcEndPosX.Value = 0 Then '先教導
            '請先教導場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
            MsgBox(gMsgHandler.GetMessage(Warn_3000027), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If (nmcPitchHX.Value = "0" And nmcPitchHY.Value = "0") Or (nmcPitchVX.Value = "0" And nmcPitchVY.Value = "0") Or lblAngle.Text = "0" Then '先教導
            '請先教導場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
            MsgBox(gMsgHandler.GetMessage(Warn_3000027), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If btnGoCCDAlignPos.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If

        'btnGoCCDAlignPos.Enabled = False
        Btn_Control(False)
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            BtnHomeFirstBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            btnGoCCDAlignPos.Enabled = True
            Btn_Control(True)
            Exit Sub
        End If
        Dim mXa As Integer = cmbXa.SelectedIndex 'Soni / 2017.05.23 跨執行緒修正
        Dim mYa As Integer = cmbYa.SelectedIndex

        Await Task.Run(Sub()


                           Dim ticket As Integer = 0
                           Dim offsetX As Decimal = 0 '特徵點距離CCD中心的距離(mm)
                           Dim offsetY As Decimal = 0
                           Dim timeOutStopWatch As New Stopwatch '逾時計時器
                           Dim mOriginPosX As Decimal
                           Dim mOriginPosY As Decimal
                           Dim mOriginPosZ As Decimal

                           Dim OutputPosX As Decimal 'CCD定位後旋轉的位置
                           Dim OutputPosY As Decimal
                           Dim NeCalibAngle As Decimal = -(lblAngle.Text)
                           Dim OffsetmmX As Decimal
                           Dim OffsetmmY As Decimal
                           Dim CCDWidth As Integer
                           Dim CCDHeight As Integer
                           gAOICollection.GetAcquistionSideLength(sys.CCDNo, CCDWidth, CCDHeight) '取得CCD的寬跟高

                           mOriginPosX = gCMotion.GetPositionValue(sys.AxisX)
                           mOriginPosY = gCMotion.GetPositionValue(sys.AxisY)
                           mOriginPosZ = gCMotion.GetPositionValue(sys.AxisZ)

                           Dim intXf As Integer = 0
                           Dim intYf As Integer = 0
                           If mXa < 0 Then
                               intXf = 0
                           Else
                               intXf = mXa
                           End If
                           If mYa < 0 Then
                               intYf = 0
                           Else
                               intYf = mYa
                           End If

                           gCMotion.SetVelAccDec(sys.AxisX)
                           gCMotion.SetVelAccDec(sys.AxisY)
                           gCMotion.SetVelAccDec(sys.AxisZ)
                           gCMotion.SetVelAccDec(sys.AxisB)
                           gCMotion.SetVelAccDec(sys.AxisC)

                           Dim AxisNo(4) As Integer
                           Dim TargetPos(4) As Decimal
                           AxisNo(0) = sys.AxisX
                           AxisNo(1) = sys.AxisY
                           AxisNo(2) = sys.AxisZ
                           AxisNo(3) = sys.AxisB
                           AxisNo(4) = sys.AxisC

                           TargetPos(0) = nmcStartPosX.Value + intXf * CDec(nmcPitchHX.Value) + intYf * CDec(nmcPitchVX.Value)
                           TargetPos(1) = nmcStartPosY.Value + intXf * CDec(nmcPitchHY.Value) + intYf * CDec(nmcPitchVY.Value)
                           TargetPos(2) = nmcStartPosZ.Value

                           ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
                           UcJoyStick1.RefreshPosition()


                           System.Threading.Thread.CurrentThread.Join(200) '待穩定後開始拍照
                           '====拍照
                           gAOICollection.SetCCDScene(sys.CCDNo, btnScene.Text) '選擇場景
                           gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
                           System.Threading.Thread.CurrentThread.Join(10)
                           ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
                           System.Threading.Thread.CurrentThread.Join(10)
                           gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保

                           timeOutStopWatch.Restart()
                           Do
                               System.Threading.Thread.Sleep(1)
                               If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
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

                                   Btn_Control(True)
                                   Exit Sub
                               End If
                           Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
                           Debug.Print("IsCCDCBusy:" & timeOutStopWatch.ElapsedMilliseconds)

                           System.Threading.Thread.CurrentThread.Join(10)
                           Debug.Print("InvokeUcDisplay  Done")
                           timeOutStopWatch.Restart()
                           Do
                               If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                                   Exit Do
                               End If
                               System.Threading.Thread.Sleep(1)
                               If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                           Loop

                           '=== 取得拍照結果 ===
                           If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
                               offsetX = 0
                               offsetY = 0
                           ElseIf gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count > 1 Then '畫面有多個特徵
                               offsetX = 0
                               offsetY = 0
                           Else '有一個特徵
                               offsetX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).PixelTranslationX
                               offsetY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).PixelTranslationY
                               '對著CCD中心做逆旋轉讓x軸為水平，y軸為垂直
                               CMath.TransformRotation(False, NeCalibAngle, CCDWidth * 0.5, CCDHeight * 0.5, offsetX, offsetY, OutputPosX, OutputPosY)
                               Debug.WriteLine("offsetX: " & offsetX & " offsetY: " & offsetY & " OutputPosX: " & OutputPosX & " OutputPosY: " & OutputPosY)
                               '旋轉後減掉CCD中心位置(Pixel)
                               OutputPosX = OutputPosX - CCDWidth * 0.5
                               OutputPosY = OutputPosY - CCDHeight * 0.5
                               '換算成mm  
                               OffsetmmX = Math.Round(gSSystemParameter.CCDScaleX2X(sys.CCDNo) * (OutputPosX) + gSSystemParameter.CCDScaleY2X(sys.CCDNo) * (OutputPosY), 4)
                               OffsetmmY = Math.Round(gSSystemParameter.CCDScaleX2Y(sys.CCDNo) * (OutputPosX) + gSSystemParameter.CCDScaleY2Y(sys.CCDNo) * (OutputPosY), 4)
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      lblOffsetX.Text = OffsetmmX.ToString
                                                      lblOffsetY.Text = OffsetmmY.ToString
                                                  End Sub)
                               End If

                           End If

                           Btn_Control(True)
                       End Sub)

    End Sub

    Private Async Sub btnCalibPos_Click(sender As Object, e As EventArgs) Handles btnCalibPos.Click  'Soni / 2017.05.16 去除DoEvents
        gSyslog.Save("[frmStageVerification]" & vbTab & "[btnCalibPos]" & vbTab & "Click")

        If lblOffsetX.Text = "0" Or lblOffsetY.Text = "0" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            'MsgBox("Please set array ")
            Exit Sub
        End If
        Await Task.Run(Sub()
                           Btn_Control(False)
                           Dim timeOutStopWatch As New Stopwatch '逾時計時器
                           Dim mOriginPosX As Decimal
                           Dim mOriginPosY As Decimal
                           Dim mOriginPosZ As Decimal
                           Dim targetPosX As Decimal
                           Dim targetPosY As Decimal
                           Dim targetPosZ As Decimal
                           Dim outputOffsetX As Decimal
                           Dim outputOffsetY As Decimal

                           Dim CCDWidth As Integer
                           Dim CCDHeight As Integer
                           gAOICollection.GetAcquistionSideLength(sys.CCDNo, CCDWidth, CCDHeight) '取得CCD的寬跟高

                           mOriginPosX = gCMotion.GetPositionValue(sys.AxisX)
                           mOriginPosY = gCMotion.GetPositionValue(sys.AxisY)
                           mOriginPosZ = gCMotion.GetPositionValue(sys.AxisZ)

                           '將逆旋轉過的結果正旋轉回去
                           CMath.TransformRotation(False, CDec(lblAngle.Text), 0, 0, CDec(lblOffsetX.Text), CDec(lblOffsetY.Text), outputOffsetX, outputOffsetY)
                           '=== Z軸到指定位置 ===
                           targetPosZ = nmcStartPosZ.Value
                           If gCMotion.AbsMove(sys.AxisZ, targetPosZ) <> CommandStatus.Sucessed Then
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
                               Btn_Control(True)
                               Exit Sub
                           End If
                           timeOutStopWatch.Restart()
                           Do
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)
                               If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                   Exit Do
                               ElseIf timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
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
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                           Loop
                           '=== Z軸到指定位置 ===
                           '=== XY到指定位置 ===
                           targetPosX = mOriginPosX - outputOffsetX
                           targetPosY = mOriginPosY - outputOffsetY
                           If gCMotion.AbsMove(sys.AxisX, targetPosX) <> CommandStatus.Sucessed Then
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
                               Btn_Control(True)
                               Exit Sub
                           End If
                           If gCMotion.AbsMove(sys.AxisY, targetPosY) <> CommandStatus.Sucessed Then
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
                               Btn_Control(True)
                               Exit Sub
                           End If
                           timeOutStopWatch.Restart()
                           Do
                               UcJoyStick1.RefreshPosition()
                               System.Threading.Thread.Sleep(1)
                               If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) = CommandStatus.Sucessed Then
                                   Exit Do
                               ElseIf timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                   'X & Y 軸移動逾時
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080010))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2080010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   Btn_Control(True)
                                   Exit Sub
                               End If
                           Loop
                           '=== XY到指定位置 ===
                           UcJoyStick1.RefreshPosition()
                       End Sub)

    End Sub

    Function StageVerification_HorizontalAction() As enmSVTmassage
        Dim ticket As Integer = 0
        Dim OutputPosX As Decimal 'CCD定位後旋轉的位置
        Dim OutputPosY As Decimal
        Dim OutputPosX_1, OutputPosY_1 As Decimal '特徵2 CCD定位後旋轉的位置
        Dim NeCalibAngle As Decimal = -(lblAngle.Text)
        Dim OffsetmmX As Decimal
        Dim OffsetmmY As Decimal
        Dim OffsetmmX_1, OffsetmmY_1 As Decimal '特徵2 CCD定位後旋轉的位置
        Dim Item_mmX As Decimal
        Dim Item_mmY As Decimal
        Dim CCDWidth As Integer
        Dim CCDHeight As Integer
        gAOICollection.GetAcquistionSideLength(sys.CCDNo, CCDWidth, CCDHeight) '取得CCD的寬跟高
        '目標位置
        Dim targetPosX As Decimal
        Dim targetPosY As Decimal
        Dim targetPosZ As Decimal
        Dim timeOutStopwatch As New Stopwatch

        '暫存位置
        Dim TempPosX As Decimal
        Dim TempPosY As Decimal
        Dim TempPosZ As Decimal



        '[Note]:先清除CCD定位紀錄
        If Not IsNothing(gCCDAlignResultDict(sys.CCDNo)) = True Then
            gCCDAlignResultDict(sys.CCDNo).Clear()
        End If
        gAOICollection.ClearTicket(sys.CCDNo)



        '開檔
        Dim Path As String = "d:\\Stage" & (sys.StageNo + 1).ToString & "  Horizontal Test.csv"
        If File.Exists(Path) Then
            File.Delete(Path)
        End If
        Dim sw As StreamWriter = New StreamWriter(Path)
        sw.WriteLine("Type=1")
        sw.WriteLine("Xcount=" & nmcArrayXCount.Value)
        sw.WriteLine("Ycount=" & nmcArrayYCount.Value)
        sw.WriteLine("Xstart=" & nmcStartPosX.Value) '* 1000)
        sw.WriteLine("Ystart=" & nmcStartPosY.Value) '* 1000)

        Dim mPitchX As Decimal = Math.Sqrt((nmcPitchHX.Value) ^ 2 + (nmcPitchHY.Value) ^ 2)
        Dim mPitchY As Decimal = Math.Sqrt((nmcPitchVX.Value) ^ 2 + (nmcPitchVY.Value) ^ 2)
        mPitchX = Math.Round(mPitchX, 3)
        mPitchY = Math.Round(mPitchY, 3)
        sw.WriteLine("Xpitch=" & mPitchX) ' * 1000)
        sw.WriteLine("Ypitch=" & -mPitchY) '* 1000)
        sw.WriteLine("OffsetmmXDirection=-1")
        sw.WriteLine("OffsetmmYDirection=-1")
        sw.WriteLine("[Start Pos], X:," & nmcStartPosX.Value & ", Y:," & nmcStartPosY.Value & ", Z:," & nmcStartPosZ.Value)
        sw.WriteLine("[Pitch], HX:," & nmcPitchHX.Value & ", HY:," & nmcPitchHY.Value & ", VX:," & nmcPitchVX.Value & ", VY:," & nmcPitchVY.Value)
        sw.WriteLine("[DelayTime], " & NumSteadyTime.Value)
        sw.WriteLine("[STAGE Verification INFORMATION]")
        'gCMotion.SetVelAccDec(sys.AxisX) '設定移動速度
        'gCMotion.SetVelAccDec(sys.AxisY)
        'gCMotion.SetVelAccDec(sys.AxisZ)
        SetAxisSpeed(sys.AxisX, 0, 100, 1000, 1000)
        SetAxisSpeed(sys.AxisY, 0, 100, 1000, 1000)
        SetAxisSpeed(sys.AxisZ, 0, 100, 1000, 1000)


        '將另一個Stage移到安全位置
        'gSSystemParameter.Pos.SafeRegion(enmStage.No1).PosX
        '等到位

        'Eason 20170221 Ticket:100032 , Memory Free Part3 [S]
        'For Each item In gCCDAlignResultDict
        '    For Each items In item
        '        items.Value.CogPMAlignResults = New Cognex.VisionPro.PMAlign.CogPMAlignResults()
        '        items.Value.CogPMAlignResults = Nothing
        '        items.Value.Image = Nothing
        '        items.Value.Result.Clear()
        '    Next
        '    item.Clear()
        'Next
        'Eason 20170221 Ticket:100032 , Memory Free Part3 [S]


        For j As Integer = 0 To nmcArrayYCount.Value - 1
            For i As Integer = 0 To nmcArrayXCount.Value - 1
                If Flag_Pause = True Then '按下暫停
                    Flag_Pause = False
                    sw.Close()
                    Return enmSVTmassage.Btn_Pause
                End If

                '====計算位置
                targetPosX = nmcStartPosX.Value + i * mPitchX '* CDec(lblPitchHX.Text) + j * CDec(lblPitchVX.Text)
                targetPosY = nmcStartPosY.Value - j * mPitchY '+ i * CDec(lblPitchHY.Text) + j * CDec(lblPitchVY.Text)
                targetPosZ = nmcStartPosZ.Value

                'Eason 20170303 Ticket:100100 , XY Offset from CSV File [S]
                Dim GetCSVOffsetIndexX As Integer = gStageOffsetFromSCV(sys.StageNo).GetIndexfromPosX(targetPosX)
                Dim GetCSVOffsetIndexY As Integer = gStageOffsetFromSCV(sys.StageNo).GetIndexfromPosY(targetPosY)
                Dim GetCSVOffsetX As Decimal = gStageOffsetFromSCV(sys.StageNo).GetOffsetfromIndexX(GetCSVOffsetIndexX, GetCSVOffsetIndexY)
                Dim GetCSVOffsetY As Decimal = gStageOffsetFromSCV(sys.StageNo).GetOffsetfromIndexY(GetCSVOffsetIndexX, GetCSVOffsetIndexY)

                targetPosX = targetPosX + GetCSVOffsetX
                targetPosY = targetPosY + GetCSVOffsetY
                targetPosZ = nmcStartPosZ.Value

                'Debug.Print("拍照位置(" & i & "," & j & "):" & targetPosX & "," & targetPosY & "," & targetPosZ)

                'Eason 20170303 Ticket:100100 , XY Offset from CSV File [E]

                '[說明]:位置安全保護
                If CheckGoPos(targetPosX, targetPosY, targetPosZ) = False Then
                    Return enmSVTmassage.AxisMove_ERROR
                End If
                '=== Z軸到指定位置 ===
                If gCMotion.AbsMove(sys.AxisZ, targetPosZ) <> CommandStatus.Sucessed Then
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
                    Return enmSVTmassage.AxisMove_ERROR
                    'Exit Function
                End If
                timeOutStopwatch.Restart()
                Do
                    UcJoyStick1.RefreshPosition()
                    System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
                    If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                        Exit Do
                    ElseIf timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
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
                        Return enmSVTmassage.AxisMove_TimeOut
                        'Exit Function
                    End If
                Loop
                '=== Z軸到指定位置 ===

                '=== XY到指定位置 ===
                If gCMotion.AbsMove(sys.AxisX, targetPosX) <> CommandStatus.Sucessed Then
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
                    Return enmSVTmassage.AxisMove_ERROR
                    'Exit Function
                End If
                If gCMotion.AbsMove(sys.AxisY, targetPosY) <> CommandStatus.Sucessed Then
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
                    Return enmSVTmassage.AxisMove_ERROR
                    'Exit Function
                End If
                System.Threading.Thread.Sleep(100) '
                timeOutStopwatch.Restart()
                Do
                    UcJoyStick1.RefreshPosition()
                    System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
                    If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) = CommandStatus.Sucessed Then
                        Exit Do
                    ElseIf timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                        'X & Y 軸移動逾時
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080010))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2080010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        'Exit Function
                        Return enmSVTmassage.AxisMove_TimeOut
                    End If
                Loop
                '=== XY到指定位置 ===

                System.Threading.Thread.CurrentThread.Join(CInt(NumSteadyTime.Value)) '待穩定後開始拍照
                '====拍照
                gAOICollection.SetCCDScene(sys.CCDNo, btnScene.Text) '選擇場景
                gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
                System.Threading.Thread.CurrentThread.Join(10)
                ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
                System.Threading.Thread.CurrentThread.Join(10)
                gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保
                System.Threading.Thread.Sleep(100) '
                timeOutStopwatch.Restart()
                Do
                    System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
                    If timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
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

                        Return enmSVTmassage.CCDAcq_TimeOut
                    End If
                Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
                'Debug.Print("IsCCDCBusy:" & timeOutStopwatch.ElapsedMilliseconds)
                System.Threading.Thread.CurrentThread.Join(10)
                timeOutStopwatch.Restart()
                Do
                    If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                        Exit Do
                    End If
                    System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
                    If timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
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

                        Return enmSVTmassage.CCDCal_TimeOut
                    End If
                Loop
                System.Threading.Thread.Sleep(30)
                '=== 取得拍照結果 ===
                Dim offsetX, offsetY As Decimal '特徵1
                Dim offsetX_1, offsetY_1 As Decimal '特徵2
                If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
                    offsetX = 0
                    offsetY = 0
                    'Dim degree As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
                    '請先選取Recipe Pattern.
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000022))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Flag_Pause = True
                    'Return False
                    sw.WriteLine("X:" & "," & i & "," & "Y:" & "," & j & "," & "OffsetmmX:" & "," & "x" & "," & "OffsetmmY:" & "," & "x")
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
                    'MsgBox("Wrong Pattern!!")
                    'Return False
                    sw.WriteLine("X:" & "," & i & "," & "Y:" & "," & j & "," & "OffsetmmX:" & "," & "x" & "," & "OffsetmmY:" & "," & "x")
                Else '有一個特徵
                    'Debug.Print("CCD Time:" & timeOutStopwatch.ElapsedMilliseconds & " ms")
                    offsetX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).PixelTranslationX
                    offsetY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).PixelTranslationY
                    offsetX_1 = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Item_1_PixelX
                    offsetY_1 = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Item_1_PixelY
                    'sw.WriteLine("Pre-X:" & "," & i & "," & "Y:" & "," & j & "," & "AbsOffsetX:" & "," & gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX & "," & "AbsOffsetY:" & "," & gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY)
                    '對著CCD中心做逆旋轉讓x軸為水平，y軸為垂直
                    CMath.TransformRotation(False, NeCalibAngle, CCDWidth * 0.5, CCDHeight * 0.5, offsetX, offsetY, OutputPosX, OutputPosY)
                    CMath.TransformRotation(False, NeCalibAngle, CCDWidth * 0.5, CCDHeight * 0.5, offsetX_1, offsetY_1, OutputPosX_1, OutputPosY_1)
                    'Debug.WriteLine("offsetX: " & offsetX & " offsetY: " & offsetY & " OutputPosX: " & OutputPosX & " OutputPosY: " & OutputPosY)
                    '旋轉後減掉CCD中心位置(Pixel)
                    OutputPosX = OutputPosX - CCDWidth * 0.5
                    OutputPosY = OutputPosY - CCDHeight * 0.5

                    OutputPosX_1 = OutputPosX_1 - CCDWidth * 0.5
                    OutputPosY_1 = OutputPosY_1 - CCDHeight * 0.5
                    '特徵1換算成mm  
                    OffsetmmX = Math.Round(gSSystemParameter.CCDScaleX2X(sys.CCDNo) * (OutputPosX) + gSSystemParameter.CCDScaleY2X(sys.CCDNo) * (OutputPosY), 4)
                    OffsetmmY = Math.Round(gSSystemParameter.CCDScaleX2Y(sys.CCDNo) * (OutputPosX) + gSSystemParameter.CCDScaleY2Y(sys.CCDNo) * (OutputPosY), 4)
                    '特徵2換算成mm  
                    OffsetmmX_1 = Math.Round(gSSystemParameter.CCDScaleX2X(sys.CCDNo) * (OutputPosX_1) + gSSystemParameter.CCDScaleY2X(sys.CCDNo) * (OutputPosY_1), 4)
                    OffsetmmY_1 = Math.Round(gSSystemParameter.CCDScaleX2Y(sys.CCDNo) * (OutputPosX_1) + gSSystemParameter.CCDScaleY2Y(sys.CCDNo) * (OutputPosY_1), 4)

                    '將補償值放在OffsetmmX2,Y2備查
                    gCMotion.GetCompensatePosition(sys.AxisX, OffsetmmX_1)
                    gCMotion.GetCompensatePosition(sys.AxisY, OffsetmmY_1)
                    '兩特徵差異
                    Item_mmX = OffsetmmX_1 - OffsetmmX
                    Item_mmY = OffsetmmY_1 - OffsetmmY
                    'sw.WriteLine("X:" & "," & i & "," & "Y:" & "," & j & ", OffsetmmX1:," & OffsetmmX.ToString() & ", OffsetmmY1:," & OffsetmmY.ToString() & ", OffsetmmX2:," & OffsetmmX_1.ToString() & ", OffsetmmY2:," & OffsetmmY_1.ToString() & ", TargetX:," & targetPosX.ToString() & ", TargetY:," & targetPosY.ToString())

                    'Toby 紀錄目前X & Y 實際位置_20170728
                    sw.WriteLine("X:" & "," & i & "," & "Y:" & "," & j & ", OffsetmmX1:," & OffsetmmX.ToString() & ", OffsetmmY1:," & OffsetmmY.ToString() & ", OffsetmmX2:," & OffsetmmX_1.ToString() & ", OffsetmmY2:," & OffsetmmY_1.ToString() & ", DismmX:," & Item_mmX.ToString() & ", DismmY:," & Item_mmY.ToString() & ", CurrentPosX:," & gCMotion.GetPositionValue(sys.AxisX) & ", CurrentPosY:," & gCMotion.GetPositionValue(sys.AxisY))

                    'Debug.Print("拍照結果(" & i & "," & j & "):" & OffsetmmX & "," & OffsetmmY)
                    If Flag_ManualDot = True Then '為偵測打點精準度先暫時新增
                        '取得軸與CCD定位位置 (因不統計資料，所以不將旋轉量考慮)
                        'ARRAY位置- CCD定位位置- 閥位置

                        TempPosX = targetPosX - gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, 0)
                        TempPosY = targetPosY - gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, 0)
                        TempPosZ = nmcStartPosZ.Value '- gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetZ(sys.SelectValve, 0)
                        '[說明]:位置安全保護
                        If CheckGoPos(TempPosX, TempPosY, TempPosZ) = False Then
                            Return enmSVTmassage.AxisMove_ERROR
                        End If
                        '=== Z軸到指定位置 ===
                        If gCMotion.AbsMove(sys.AxisZ, TempPosZ) <> CommandStatus.Sucessed Then
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
                            Return enmSVTmassage.AxisMove_ERROR
                            'Exit Function
                        End If
                        timeOutStopwatch.Restart()
                        Do
                            UcJoyStick1.RefreshPosition()
                            System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
                            If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                Exit Do
                            ElseIf timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
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
                                Return enmSVTmassage.AxisMove_TimeOut
                                'Exit Function
                            End If
                        Loop
                        '=== Z軸到指定位置 ===

                        '=== XY到指定位置 ===
                        If gCMotion.AbsMove(sys.AxisX, TempPosX) <> CommandStatus.Sucessed Then
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
                            Return enmSVTmassage.AxisMove_ERROR
                            'Exit Function
                        End If
                        If gCMotion.AbsMove(sys.AxisY, TempPosY) <> CommandStatus.Sucessed Then
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
                            Return enmSVTmassage.AxisMove_ERROR
                            'Exit Function
                        End If
                        timeOutStopwatch.Restart()
                        Do
                            UcJoyStick1.RefreshPosition()
                            System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
                            If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) = CommandStatus.Sucessed Then
                                Exit Do
                            ElseIf timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                'X & Y 軸移動逾時
                                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080010))
                                MsgBox(gMsgHandler.GetMessage(Alarm_2080010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                'Exit Function
                                Return enmSVTmassage.AxisMove_TimeOut
                            End If
                        Loop
                        '=== XY到指定位置 ===
                        MsgBox("請手動打點", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                    End If

                End If
                InvokeUcDisplay(UcDisplay1, gAOICollection, sys, btnScene.Text, 0, 0, enmDisplayShape.Alignment) '更新控制項,必要條件 frmMain必須是實體
            Next
            'sw.WriteLine("================================================================================")
        Next
        sw.Close()
        Return enmSVTmassage.Done
    End Function


    Private Async Sub BtnHorizontalRun_Click(sender As Object, e As EventArgs) Handles BtnHorizontalRun.Click  'Soni / 2017.05.16 去除DoEvents
        'Eason 20170221 Event Log 
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name & " START")

        gSyslog.Save("[frmStageAccuracy]" & vbTab & "[BtnHorizontalRun]" & vbTab & "Click")
        If (nmcPitchHX.Value = "0") Or (nmcPitchVY.Value = "0") Then '先教導
            '請先教導場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
            MsgBox(gMsgHandler.GetMessage(Warn_3000027), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        'If (nmcStartPosX.Value = nmcCornerPosX.Value) Or (nmcStartPosX.Value = nmcEndPosX.Value) Then '先教導
        '    '請先教導場景
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
        '    MsgBox(gMsgHandler.GetMessage(Warn_3000027),MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    Exit Sub
        'End If
        'If (nmcStartPosY.Value = nmcEndPosY.Value) Or (nmcCornerPosY.Value = nmcEndPosY.Value) Then '先教導
        '    '請先教導場景
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
        '    MsgBox(gMsgHandler.GetMessage(Warn_3000027),MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    Exit Sub
        'End If
        'If AnalysisPitchResult() = False Then '教導錯誤
        '    '請先教導場景
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
        '    MsgBox(gMsgHandler.GetMessage(Warn_3000027),MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    Exit Sub
        'End If
        If BtnHorizontalRun.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        Await Task.Run(Sub()
                           Btn_Control(False)
                           Flag_Pause = False
                           Dim RunStatus As enmSVTmassage
                           RunStatus = StageVerification_HorizontalAction()
                           If RunStatus <> enmSVTmassage.Done Then
                               gSyslog.Save("[BtnHorizontalRun] RunStatus:" & RunStatus, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               MsgBox("Stage Verification Horizontal Error: " & RunStatus, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                           Else
                               MsgBox("Stage Verification Horizontal Done ", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                           End If
                           InvokeUcDisplay(UcDisplay1, gAOICollection, sys, btnScene.Text, 0, 0, enmDisplayShape.None) '更新控制項,必要條件 frmMain必須是實體
                           'BtnHorizontalRun.Enabled = True
                           Btn_Control(True)

                           'Eason 20170221 Event Log 
                           gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name & " END")
                       End Sub)
    End Sub


    Function StageVerification_VerticalAction() As enmSVTmassage
        Dim ticket As Integer = 0
        Dim OutputPosX As Decimal 'CCD定位後旋轉的位置
        Dim OutputPosY As Decimal
        Dim OutputPosX_1, OutputPosY_1 As Decimal '特徵2 CCD定位後旋轉的位置
        Dim NeCalibAngle As Decimal = -(lblAngle.Text)
        Dim OffsetmmX As Decimal
        Dim OffsetmmY As Decimal
        Dim OffsetmmX_1, OffsetmmY_1 As Decimal '特徵2 CCD定位後旋轉的位置
        Dim Item_mmX As Decimal
        Dim Item_mmY As Decimal
        Dim CCDWidth As Integer
        Dim CCDHeight As Integer
        gAOICollection.GetAcquistionSideLength(sys.CCDNo, CCDWidth, CCDHeight) '取得CCD的寬跟高
        '目標位置 array
        Dim targetPosX As Decimal
        Dim targetPosY As Decimal
        Dim targetPosZ As Decimal
        Dim timeOutStopwatch As New Stopwatch
        '暫存位置
        Dim TempPosX As Decimal
        Dim TempPosY As Decimal
        Dim TempPosZ As Decimal


        '[Note]:先清除CCD定位紀錄
        If Not IsNothing(gCCDAlignResultDict(sys.CCDNo)) = True Then
            gCCDAlignResultDict(sys.CCDNo).Clear()
        End If
        gAOICollection.ClearTicket(sys.CCDNo)



        '開檔
        Dim Path As String = "d:\\Stage" & (sys.StageNo + 1).ToString & "  Verification Test.csv"
        If File.Exists(Path) Then
            File.Delete(Path)
        End If

        'Eason 20170221 Ticket:100032 , Memory Free Part3 [S]
        'For Each item In gCCDAlignResultDict
        '    For Each items In item
        '        items.Value.CogPMAlignResults = New Cognex.VisionPro.PMAlign.CogPMAlignResults()
        '        items.Value.CogPMAlignResults = Nothing
        '        items.Value.Image = Nothing
        '        items.Value.Result.Clear()
        '    Next
        '    item.Clear()
        'Next
        'Eason 20170221 Ticket:100032 , Memory Free Part3 [S]

        Dim sw As StreamWriter = New StreamWriter(Path)
        sw.WriteLine("Type=1")
        sw.WriteLine("Xcount=" & nmcArrayXCount.Value)
        sw.WriteLine("Ycount=" & nmcArrayYCount.Value)
        sw.WriteLine("Xstart=" & nmcStartPosX.Value) '* 1000)
        sw.WriteLine("Ystart=" & nmcStartPosY.Value) ' * 1000)
        Dim mPitchX As Decimal = Math.Sqrt((nmcPitchHX.Value) ^ 2 + (nmcPitchHY.Value) ^ 2)
        Dim mPitchY As Decimal = Math.Sqrt((nmcPitchVX.Value) ^ 2 + (nmcPitchVY.Value) ^ 2)
        mPitchX = Math.Round(mPitchX, 3)
        mPitchY = Math.Round(mPitchY, 3)
        sw.WriteLine("Xpitch=" & mPitchX) '* 1000)
        sw.WriteLine("Ypitch=" & -mPitchY) '* 1000)
        sw.WriteLine("OffsetmmXDirection=-1")
        sw.WriteLine("OffsetmmYDirection=-1")
        sw.WriteLine("[Start Pos], X:," & nmcStartPosX.Value & ", Y:," & nmcStartPosY.Value & ", Z:," & nmcStartPosZ.Value)
        sw.WriteLine("[Pitch], HX:," & nmcPitchHX.Value & ", HY:," & nmcPitchHY.Value & ", VX:," & nmcPitchVX.Value & ", VY:," & nmcPitchVY.Value)
        sw.WriteLine("[DelayTime], " & NumSteadyTime.Value)
        sw.WriteLine("[STAGE Verification INFORMATION]")

        'gCMotion.SetVelAccDec(sys.AxisX) '設定移動速度
        'gCMotion.SetVelAccDec(sys.AxisY)
        'gCMotion.SetVelAccDec(sys.AxisZ)
        SetAxisSpeed(sys.AxisX, 0, 100, 1000, 1000)
        SetAxisSpeed(sys.AxisY, 0, 100, 1000, 1000)
        SetAxisSpeed(sys.AxisZ, 0, 100, 1000, 1000)

        For i As Integer = 0 To nmcArrayXCount.Value - 1
            For j As Integer = 0 To nmcArrayYCount.Value - 1
                If Flag_Pause = True Then '按下暫停
                    Flag_Pause = False
                    sw.Close()
                    Return enmSVTmassage.Btn_Pause
                End If
                targetPosX = nmcStartPosX.Value + i * mPitchX 'CDec(lblPitchHX.Text) + j * CDec(lblPitchVX.Text)
                targetPosY = nmcStartPosY.Value - j * mPitchY ' i * CDec(lblPitchHY.Text) + j * CDec(lblPitchVY.Text)
                targetPosZ = nmcStartPosZ.Value

                'Eason 20170303 Ticket:100100 , XY Offset from CSV File [S]
                Dim GetCSVOffsetIndexX As Integer = gStageOffsetFromSCV(sys.StageNo).GetIndexfromPosX(targetPosX)
                Dim GetCSVOffsetIndexY As Integer = gStageOffsetFromSCV(sys.StageNo).GetIndexfromPosY(targetPosY)
                Dim GetCSVOffsetX As Decimal = gStageOffsetFromSCV(sys.StageNo).GetOffsetfromIndexX(GetCSVOffsetIndexX, GetCSVOffsetIndexY)
                Dim GetCSVOffsetY As Decimal = gStageOffsetFromSCV(sys.StageNo).GetOffsetfromIndexY(GetCSVOffsetIndexX, GetCSVOffsetIndexY)

                targetPosX = targetPosX + GetCSVOffsetX
                targetPosY = targetPosY + GetCSVOffsetY
                targetPosZ = nmcStartPosZ.Value
                'Eason 20170303 Ticket:100100 , XY Offset from CSV File [E]

                '[說明]:位置安全保護
                If CheckGoPos(targetPosX, targetPosY, targetPosZ) = False Then
                    Return enmSVTmassage.AxisMove_ERROR
                End If

                '=== Z軸到指定位置 ===
                If gCMotion.AbsMove(sys.AxisZ, targetPosZ) <> CommandStatus.Sucessed Then
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
                    Return enmSVTmassage.AxisMove_ERROR
                End If
                timeOutStopwatch.Restart()
                Do
                    UcJoyStick1.RefreshPosition()
                    System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
                    If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                        Exit Do
                    ElseIf timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
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
                        Return enmSVTmassage.AxisMove_TimeOut
                        'Exit Function
                    End If
                Loop
                '=== Z軸到指定位置 ===

                '=== XY到指定位置 ===
                If gCMotion.AbsMove(sys.AxisX, targetPosX) <> CommandStatus.Sucessed Then
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
                    Return enmSVTmassage.AxisMove_ERROR
                    'Exit Function
                End If
                If gCMotion.AbsMove(sys.AxisY, targetPosY) <> CommandStatus.Sucessed Then
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
                    Return enmSVTmassage.AxisMove_ERROR
                    'Exit Function
                End If
                timeOutStopwatch.Restart()
                Do
                    UcJoyStick1.RefreshPosition()
                    System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
                    If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) = CommandStatus.Sucessed Then
                        Exit Do
                    ElseIf timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                        'X & Y 軸移動逾時
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080010))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2080010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        'Exit Function
                        Return enmSVTmassage.AxisMove_TimeOut
                    End If
                Loop
                '=== XY到指定位置 ===

                System.Threading.Thread.CurrentThread.Join(CInt(NumSteadyTime.Value)) '待穩定後開始拍照
                '====拍照
                gAOICollection.SetCCDScene(sys.CCDNo, btnScene.Text) '選擇場景
                gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
                System.Threading.Thread.CurrentThread.Join(10)
                ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
                System.Threading.Thread.CurrentThread.Join(10)
                gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保
                timeOutStopwatch.Restart()
                Do
                    System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
                    If timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
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

                        Return enmSVTmassage.CCDAcq_TimeOut
                    End If
                Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
                'Debug.Print("IsCCDCBusy:" & timeOutStopwatch.ElapsedMilliseconds)

                System.Threading.Thread.CurrentThread.Join(10)
                'Debug.Print("InvokeUcDisplay  Done")
                timeOutStopwatch.Restart()
                Do
                    If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                        Exit Do
                    End If
                    System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
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

                        Return enmSVTmassage.CCDCal_TimeOut
                    End If
                Loop
                System.Threading.Thread.Sleep(30)
                '=== 取得拍照結果 ===
                Dim offsetX, offsetY As Decimal '特徵1
                Dim offsetX_1, offsetY_1 As Decimal '特徵2
                If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
                    offsetX = 0
                    offsetY = 0
                    'Dim degree As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
                    '請先選取Recipe Pattern.
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000013), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Flag_Pause = True
                    'Return False
                    sw.WriteLine("X:" & "," & i & "," & "Y:" & "," & j & "," & "OffsetmmX:" & "," & "x" & "," & "OffsetmmY:" & "," & "x")
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
                    'MsgBox("Wrong Pattern!!")
                    'Return False
                    sw.WriteLine("X:" & "," & i & "," & "Y:" & "," & j & "," & "OffsetmmX:" & "," & "x" & "," & "OffsetmmY:" & "," & "x")
                Else '有一個特徵
                    'Debug.Print("CCD Time:" & timeOutStopwatch.ElapsedMilliseconds & " ms")
                    'offsetX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).PixelTranslationX
                    'offsetY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).PixelTranslationY
                    'Item_mmX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Item_DisX
                    'Item_mmY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Item_DisY
                    ''sw.WriteLine("Pre-X:" & "," & i & "," & "Y:" & "," & j & "," & "AbsOffsetX:" & "," & gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX & "," & "AbsOffsetY:" & "," & gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY)
                    ''對著CCD中心做逆旋轉讓x軸為水平，y軸為垂直
                    'CMath.TransformRotation(False, NeCalibAngle, CCDWidth * 0.5, CCDHeight * 0.5, offsetX, offsetY, OutputPosX, OutputPosY)
                    ''Debug.WriteLine("offsetX: " & offsetX & " offsetY: " & offsetY & " OutputPosX: " & OutputPosX & " OutputPosY: " & OutputPosY)
                    ''旋轉後減掉CCD中心位置(Pixel)
                    'OutputPosX = OutputPosX - CCDWidth * 0.5
                    'OutputPosY = OutputPosY - CCDHeight * 0.5
                    ''換算成mm  
                    'OffsetmmX = Math.Round(gSSystemParameter.CCDScaleX2X(sys.CCDNo) * (OutputPosX) + gSSystemParameter.CCDScaleY2X(sys.CCDNo) * (OutputPosY), 4)
                    'OffsetmmY = Math.Round(gSSystemParameter.CCDScaleX2Y(sys.CCDNo) * (OutputPosX) + gSSystemParameter.CCDScaleY2Y(sys.CCDNo) * (OutputPosY), 4)
                    'Item_mmX = Math.Round(Item_mmX, 4)
                    'Item_mmY = Math.Round(Item_mmY, 4)
                    'sw.WriteLine("X:" & "," & i & "," & "Y:" & "," & j & "," & "OffsetmmX:" & "," & OffsetmmX.ToString() & "," & "OffsetmmY:" & "," & OffsetmmY.ToString() & ", Item_DisXmm:" & "," & Item_mmX.ToString() & "," & "Item_DisYmm:" & "," & Item_mmY.ToString())

                    Debug.Print("CCD Time:" & timeOutStopwatch.ElapsedMilliseconds & " ms")
                    offsetX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).PixelTranslationX
                    offsetY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).PixelTranslationY
                    offsetX_1 = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Item_1_PixelX
                    offsetY_1 = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Item_1_PixelY
                    'sw.WriteLine("Pre-X:" & "," & i & "," & "Y:" & "," & j & "," & "AbsOffsetX:" & "," & gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX & "," & "AbsOffsetY:" & "," & gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY)
                    '對著CCD中心做逆旋轉讓x軸為水平，y軸為垂直
                    CMath.TransformRotation(False, NeCalibAngle, CCDWidth * 0.5, CCDHeight * 0.5, offsetX, offsetY, OutputPosX, OutputPosY)
                    CMath.TransformRotation(False, NeCalibAngle, CCDWidth * 0.5, CCDHeight * 0.5, offsetX_1, offsetY_1, OutputPosX_1, OutputPosY_1)
                    'Debug.WriteLine("offsetX: " & offsetX & " offsetY: " & offsetY & " OutputPosX: " & OutputPosX & " OutputPosY: " & OutputPosY)
                    '旋轉後減掉CCD中心位置(Pixel)
                    OutputPosX = OutputPosX - CCDWidth * 0.5
                    OutputPosY = OutputPosY - CCDHeight * 0.5

                    OutputPosX_1 = OutputPosX_1 - CCDWidth * 0.5
                    OutputPosY_1 = OutputPosY_1 - CCDHeight * 0.5
                    '特徵1換算成mm  
                    OffsetmmX = Math.Round(gSSystemParameter.CCDScaleX2X(sys.CCDNo) * (OutputPosX) + gSSystemParameter.CCDScaleY2X(sys.CCDNo) * (OutputPosY), 4)
                    OffsetmmY = Math.Round(gSSystemParameter.CCDScaleX2Y(sys.CCDNo) * (OutputPosX) + gSSystemParameter.CCDScaleY2Y(sys.CCDNo) * (OutputPosY), 4)
                    '特徵2換算成mm  
                    OffsetmmX_1 = Math.Round(gSSystemParameter.CCDScaleX2X(sys.CCDNo) * (OutputPosX_1) + gSSystemParameter.CCDScaleY2X(sys.CCDNo) * (OutputPosY_1), 4)
                    OffsetmmY_1 = Math.Round(gSSystemParameter.CCDScaleX2Y(sys.CCDNo) * (OutputPosX_1) + gSSystemParameter.CCDScaleY2Y(sys.CCDNo) * (OutputPosY_1), 4)

                    '將補償值放在OffsetmmX2,Y2備查
                    gCMotion.GetCompensatePosition(sys.AxisX, OffsetmmX_1)
                    gCMotion.GetCompensatePosition(sys.AxisY, OffsetmmY_1)

                    '兩特徵差異
                    Item_mmX = OffsetmmX_1 - OffsetmmX
                    Item_mmY = OffsetmmY_1 - OffsetmmY
                    'sw.WriteLine("X:" & "," & i & "," & "Y:" & "," & j & ", OffsetmmX1:," & OffsetmmX.ToString() & ", OffsetmmY1:," & OffsetmmY.ToString() & ", OffsetmmX2:," & OffsetmmX_1.ToString() & ", OffsetmmY2:," & OffsetmmY_1.ToString() & ", DismmX:," & Item_mmX.ToString() & ", DismmY:," & Item_mmY.ToString())
                    'Toby 紀錄目前X & Y 實際位置_20170728
                    sw.WriteLine("X:" & "," & i & "," & "Y:" & "," & j & ", OffsetmmX1:," & OffsetmmX.ToString() & ", OffsetmmY1:," & OffsetmmY.ToString() & ", OffsetmmX2:," & OffsetmmX_1.ToString() & ", OffsetmmY2:," & OffsetmmY_1.ToString() & ", DismmX:," & Item_mmX.ToString() & ", DismmY:," & Item_mmY.ToString() & ", CurrentPosX:," & gCMotion.GetPositionValue(sys.AxisX) & ", CurrentPosY:," & gCMotion.GetPositionValue(sys.AxisY))


                    If Flag_ManualDot = True Then '為偵測打點精準度先暫時新增
                        '取得軸與CCD定位位置 (因不統計資料，所以不將旋轉量考慮)
                        'ARRAY位置- CCD定位位置- 閥位置
                        TempPosX = targetPosX - gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, 0)
                        TempPosY = targetPosY - gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, 0)
                        TempPosZ = nmcStartPosZ.Value '- gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetZ(sys.SelectValve, 0)
                        '[說明]:位置安全保護
                        If CheckGoPos(TempPosX, TempPosY, TempPosZ) = False Then
                            Return enmSVTmassage.AxisMove_ERROR
                        End If
                        '=== Z軸到指定位置 ===
                        If gCMotion.AbsMove(sys.AxisZ, TempPosZ) <> CommandStatus.Sucessed Then
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
                            Return enmSVTmassage.AxisMove_ERROR
                            'Exit Function
                        End If
                        timeOutStopwatch.Restart()
                        Do
                            UcJoyStick1.RefreshPosition()
                            System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
                            If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                                Exit Do
                            ElseIf timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
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
                                Return enmSVTmassage.AxisMove_TimeOut
                                'Exit Function
                            End If
                        Loop
                        '=== Z軸到指定位置 ===

                        '=== XY到指定位置 ===
                        If gCMotion.AbsMove(sys.AxisX, TempPosX) <> CommandStatus.Sucessed Then
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
                            Return enmSVTmassage.AxisMove_ERROR
                            'Exit Function
                        End If
                        If gCMotion.AbsMove(sys.AxisY, TempPosY) <> CommandStatus.Sucessed Then
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
                            Return enmSVTmassage.AxisMove_ERROR
                            'Exit Function
                        End If
                        timeOutStopwatch.Restart()
                        Do
                            UcJoyStick1.RefreshPosition()
                            System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16 去除DoEvents
                            If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) = CommandStatus.Sucessed Then
                                Exit Do
                            ElseIf timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                                'X & Y 軸移動逾時
                                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080010))
                                MsgBox(gMsgHandler.GetMessage(Alarm_2080010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                'Exit Function
                                Return enmSVTmassage.AxisMove_TimeOut
                            End If
                        Loop
                        '=== XY到指定位置 ===
                        MsgBox("請手動打點", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                    End If

                End If
                InvokeUcDisplay(UcDisplay1, gAOICollection, sys, btnScene.Text, 0, 0, enmDisplayShape.Alignment) '更新控制項,必要條件 frmMain必須是實體


            Next
            'sw.WriteLine("================================================================================")
        Next
        sw.Close()
        Return enmSVTmassage.Done
    End Function


    Private Async Sub BtnVerticalRun_Click(sender As Object, e As EventArgs) Handles BtnVerticalRun.Click
        Await Task.Run(Sub()
                           'Eason 20170221 Event Log 
                           gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name & " START")

                           gSyslog.Save("[frmStageAccuracy]" & vbTab & "[BtnVerticalRun]" & vbTab & "Click")
                           If (nmcPitchHX.Value = "0") Or (nmcPitchVY.Value = "0") Then '先教導
                               '請先教導場景
                               gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
                               MsgBox(gMsgHandler.GetMessage(Warn_3000027), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               Exit Sub
                           End If
                           'If (nmcStartPosX.Value = nmcCornerPosX.Value) Or (nmcStartPosX.Value = nmcEndPosX.Value) Then '先教導
                           '    '請先教導場景
                           '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
                           '    MsgBox(gMsgHandler.GetMessage(Warn_3000027),MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                           '    Exit Sub
                           'End If
                           'If (nmcStartPosY.Value = nmcEndPosY.Value) Or (nmcCornerPosY.Value = nmcEndPosY.Value) Then '先教導
                           '    '請先教導場景
                           '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
                           '    MsgBox(gMsgHandler.GetMessage(Warn_3000027),MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                           '    Exit Sub
                           'End If
                           'If AnalysisPitchResult() = False Then '教導錯誤
                           '    '請先教導場景
                           '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
                           '    MsgBox(gMsgHandler.GetMessage(Warn_3000027),MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                           '    Exit Sub
                           'End If
                           If BtnVerticalRun.Enabled = False Then '防連按
                               Exit Sub
                               '^^^^^^^
                           End If
                           'BtnVerticalRun.Enabled = False
                           Btn_Control(False)
                           Flag_Pause = False
                           Dim RunStatus As enmSVTmassage
                           RunStatus = StageVerification_VerticalAction()
                           If RunStatus <> enmSVTmassage.Done Then
                               gSyslog.Save("[BtnVerticalRun] RunStatus:" & RunStatus)
                               MsgBox("Stage Verification Vertical Error: " & RunStatus, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                           Else
                               MsgBox("Stage Verification Vertical Done ", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                           End If
                           InvokeUcDisplay(UcDisplay1, gAOICollection, sys, btnScene.Text, 0, 0, enmDisplayShape.None) '更新控制項,必要條件 frmMain必須是實體
                           'BtnVerticalRun.Enabled = True
                           Btn_Control(True)

                           'Eason 20170221 Event Log 
                           gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name & " END")
                       End Sub)

    End Sub

    Function StageVerification_ReAction(ByVal Round As Integer) As enmSVTmassage
        Dim ticket As Integer = 0
        Dim OutputPosX As Decimal 'CCD定位後旋轉的位置
        Dim OutputPosY As Decimal
        Dim NeCalibAngle As Decimal = -(lblAngle.Text)
        Dim OffsetmmX As Decimal
        Dim OffsetmmY As Decimal
        Dim CCDWidth As Integer
        Dim CCDHeight As Integer
        gAOICollection.GetAcquistionSideLength(sys.CCDNo, CCDWidth, CCDHeight) '取得CCD的寬跟高
        '目標位置
        Dim targetPosX As Decimal
        Dim targetPosY As Decimal
        Dim targetPosZ As Decimal
        Dim timeOutStopwatch As New Stopwatch

        '開檔
        Dim Path As String = "d:\\Stage" & (sys.StageNo + 1).ToString & " ReRun_" & Round.ToString & " Test.csv"
        If File.Exists(Path) Then
            File.Delete(Path)
        End If

        'Eason 20170221 Ticket:100032 , Memory Free Part3 [S]
        For Each item In gCCDAlignResultDict
            For Each items In item
                items.Value.CogPMAlignResults = New Cognex.VisionPro.PMAlign.CogPMAlignResults()
                items.Value.CogPMAlignResults = Nothing
                items.Value.Image = Nothing
                items.Value.Result.Clear()
            Next
            item.Clear()
        Next
        'Eason 20170221 Ticket:100032 , Memory Free Part3 [S]

        Dim sw As StreamWriter = New StreamWriter(Path)
        sw.WriteLine("[STAGE Verification INFORMATION]")

        'gCMotion.SetVelAccDec(sys.AxisX) '設定移動速度
        'gCMotion.SetVelAccDec(sys.AxisY)
        'gCMotion.SetVelAccDec(sys.AxisZ)
        SetAxisSpeed(sys.AxisX, 0, 100, 1000, 1000)
        SetAxisSpeed(sys.AxisY, 0, 100, 1000, 1000)
        SetAxisSpeed(sys.AxisZ, 0, 100, 1000, 1000)

        For j As Integer = 0 To Round - 1
            sw.WriteLine("Round:" & j)
            For i As Integer = 0 To nmcArrayXCount.Value - 1
                If Flag_Pause = True Then '按下暫停
                    Flag_Pause = False
                    sw.Close()
                    Return enmSVTmassage.Btn_Pause
                End If
                '====計算位置
                If j Mod 2 = 0 Then '往x軸正向走
                    targetPosX = nmcStartPosX.Value + i * CDec(nmcPitchHX.Value) '+ j * CDec(lblPitchVX.Text)
                    targetPosY = nmcStartPosY.Value + i * CDec(nmcPitchHY.Value) '+ j * CDec(lblPitchVY.Text)
                Else    '往x軸反向走
                    targetPosX = nmcCornerPosX.Value - i * CDec(nmcPitchHX.Value) '+ j * CDec(lblPitchVX.Text)
                    targetPosY = nmcCornerPosY.Value - i * CDec(nmcPitchHY.Value) '+ j * CDec(lblPitchVY.Text)
                End If
                targetPosZ = nmcStartPosZ.Value
                '[說明]:位置安全保護
                If CheckGoPos(targetPosX, targetPosY, targetPosZ) = False Then
                    Return enmSVTmassage.AxisMove_ERROR
                End If

                '=== Z軸到指定位置 ===
                If gCMotion.AbsMove(sys.AxisZ, targetPosZ) <> CommandStatus.Sucessed Then
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
                    Return enmSVTmassage.AxisMove_ERROR
                End If
                timeOutStopwatch.Restart()
                Do
                    UcJoyStick1.RefreshPosition()
                    System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16
                    If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then
                        Exit Do
                    ElseIf timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
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
                        Return enmSVTmassage.AxisMove_TimeOut
                        'Exit Function
                    End If
                Loop
                '=== Z軸到指定位置 ===

                '=== XY到指定位置 ===
                If gCMotion.AbsMove(sys.AxisX, targetPosX) <> CommandStatus.Sucessed Then
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
                    Return enmSVTmassage.AxisMove_ERROR
                    'Exit Function
                End If
                If gCMotion.AbsMove(sys.AxisY, targetPosY) <> CommandStatus.Sucessed Then
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
                    Return enmSVTmassage.AxisMove_ERROR
                    'Exit Function
                End If
                timeOutStopwatch.Restart()
                Do
                    UcJoyStick1.RefreshPosition()
                    System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16
                    If gCMotion.MotionDone(sys.AxisX) = CommandStatus.Sucessed And gCMotion.MotionDone(sys.AxisY) = CommandStatus.Sucessed Then
                        Exit Do
                    ElseIf timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut3 Then
                        'X & Y 軸移動逾時
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080010))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2080010), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        'Exit Function
                        Return enmSVTmassage.AxisMove_TimeOut
                    End If
                Loop
                '=== XY到指定位置 ===
                System.Threading.Thread.CurrentThread.Join(CInt(NumSteadyTime.Value)) '待穩定後開始拍照
                '====拍照
                gAOICollection.SetCCDScene(sys.CCDNo, btnScene.Text) '選擇場景
                gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
                System.Threading.Thread.CurrentThread.Join(10)
                ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
                System.Threading.Thread.CurrentThread.Join(10)
                gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保
                timeOutStopwatch.Restart()
                Do
                    System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16
                    If timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
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
                        Return enmSVTmassage.CCDAcq_TimeOut
                    End If
                Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
                Debug.Print("IsCCDCBusy:" & timeOutStopwatch.ElapsedMilliseconds)

                System.Threading.Thread.CurrentThread.Join(10)
                Debug.Print("InvokeUcDisplay  Done")
                timeOutStopwatch.Restart()
                Do
                    If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                        Exit Do
                    End If
                    System.Threading.Thread.Sleep(1) 'Soni / 2017.05.16
                    If timeOutStopwatch.ElapsedMilliseconds > gSSystemParameter.TimeOut2 Then
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
                        Return enmSVTmassage.CCDCal_TimeOut
                    End If
                Loop

                '=== 取得拍照結果 ===
                Dim offsetX, offsetY As Decimal
                If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
                    offsetX = 0
                    offsetY = 0
                    'Dim degree As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
                    'MsgBox("Pattern Not Found.")
                    'Return False
                    sw.WriteLine("X:" & "," & i & "," & "Y:" & "," & j & "," & "OffsetmmX:" & "," & "x" & "," & "OffsetmmY:" & "," & "x")
                ElseIf gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count > 1 Then '畫面有多個特徵
                    offsetX = 0
                    offsetY = 0
                    'MsgBox("Wrong Pattern!!")
                    'Return False
                    sw.WriteLine("X:" & "," & i & "," & "Y:" & "," & j & "," & "OffsetmmX:" & "," & "x" & "," & "OffsetmmY:" & "," & "x")
                Else '有一個特徵
                    offsetX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).PixelTranslationX
                    offsetY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).PixelTranslationY
                    'sw.WriteLine("Pre-X:" & "," & i & "," & "Y:" & "," & j & "," & "AbsOffsetX:" & "," & gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX & "," & "AbsOffsetY:" & "," & gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY)
                    '對著CCD中心做逆旋轉讓x軸為水平，y軸為垂直
                    CMath.TransformRotation(False, NeCalibAngle, CCDWidth * 0.5, CCDHeight * 0.5, offsetX, offsetY, OutputPosX, OutputPosY)
                    Debug.WriteLine("offsetX: " & offsetX & " offsetY: " & offsetY & " OutputPosX: " & OutputPosX & " OutputPosY: " & OutputPosY)
                    '旋轉後減掉CCD中心位置(Pixel)
                    OutputPosX = OutputPosX - CCDWidth * 0.5
                    OutputPosY = OutputPosY - CCDHeight * 0.5
                    '換算成mm  
                    OffsetmmX = Math.Round(gSSystemParameter.CCDScaleX2X(sys.CCDNo) * (OutputPosX) + gSSystemParameter.CCDScaleY2X(sys.CCDNo) * (OutputPosY), 4)
                    OffsetmmY = Math.Round(gSSystemParameter.CCDScaleX2Y(sys.CCDNo) * (OutputPosX) + gSSystemParameter.CCDScaleY2Y(sys.CCDNo) * (OutputPosY), 4)
                    sw.WriteLine("X:" & "," & i & "," & "Y:" & "," & j & "," & "OffsetmmX:" & "," & OffsetmmX.ToString() & "," & "OffsetmmY:" & "," & OffsetmmY.ToString())
                End If
                InvokeUcDisplay(UcDisplay1, gAOICollection, sys, btnScene.Text, 0, 0, enmDisplayShape.Alignment) '更新控制項,必要條件 frmMain必須是實體
            Next
            'sw.WriteLine("================================================================================")
        Next
        sw.Close()
        Return enmSVTmassage.Done
    End Function

    Private Async Sub BtnReRun_Click(sender As Object, e As EventArgs) Handles BtnReRun.Click  'Soni / 2017.05.16 為了去除DoEvents
        'Eason 20170221 Event Log 
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name & " START")

        gSyslog.Save("[frmStageAccuracy]" & vbTab & "[BtnReRun]" & vbTab & "Click")
        If (nmcPitchHX.Value = "0" And nmcPitchHY.Value = "0") Or (nmcPitchVX.Value = "0" And nmcPitchVY.Value = "0") Or lblAngle.Text = "0" Then '先教導
            '請先教導場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
            MsgBox(gMsgHandler.GetMessage(Warn_3000027), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If (nmcStartPosX.Value = nmcCornerPosX.Value) Or (nmcStartPosX.Value = nmcEndPosX.Value) Then '先教導
            '請先教導場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
            MsgBox(gMsgHandler.GetMessage(Warn_3000027), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If (nmcStartPosY.Value = nmcEndPosY.Value) Or (nmcCornerPosY.Value = nmcEndPosY.Value) Then '先教導
            '請先教導場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
            MsgBox(gMsgHandler.GetMessage(Warn_3000027), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If AnalysisPitchResult() = False Then '教導錯誤
            '請先教導場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000027))
            MsgBox(gMsgHandler.GetMessage(Warn_3000027), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If BtnReRun.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        'BtnReRun.Enabled = False

        Await Task.Run(Sub()
                           Btn_Control(False)
                           Flag_Pause = False

                           Dim RunStatus As enmSVTmassage
                           RunStatus = StageVerification_ReAction(4)
                           If RunStatus <> enmSVTmassage.Done Then
                               gSyslog.Save("[BtnReRun] RunStatus:" & RunStatus)
                               MsgBox("Stage ReRun Vertical Error: " & RunStatus, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                           End If
                           'BtnReRun.Enabled = True
                           Btn_Control(True)

                           'Eason 20170221 Event Log 
                           gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name & " END")
                       End Sub)

    End Sub

    Public Function CheckGoPos(ByVal PosX As Decimal, ByVal PosY As Decimal, ByVal PosZ As Decimal) As Boolean
        If PosX < gCMotion.AxisParameter(sys.AxisX).Limit.NegativeLimit Or PosX > gCMotion.AxisParameter(sys.AxisX).Limit.PosivtiveLimit Or
           PosY < gCMotion.AxisParameter(sys.AxisY).Limit.NegativeLimit Or PosY > gCMotion.AxisParameter(sys.AxisY).Limit.PosivtiveLimit Or
           PosZ < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Or PosZ > gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub btnPause_Click(sender As Object, e As EventArgs) Handles btnPause.Click
        gSyslog.Save("[frmStageAccuracy]" & vbTab & "[btnPause]" & vbTab & "Click")
        btnPause.Enabled = False
        Flag_Pause = True
        btnPause.Enabled = True
    End Sub



    Function AnalysisVacuumStatus() As Boolean
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If sys.MachineNo = enmMachineStation.MachineA Then
                    If WetcoConveyor.Unit.A_IsVacuum = True Then
                        Return True
                    End If
                ElseIf sys.MachineNo = enmMachineStation.MachineB Then
                    If WetcoConveyor.Unit.B_IsVacuum = True Then
                        Return True
                    End If
                Else
                    Return False
                End If
            Case enmMachineType.DCS_F230A
                If gConveyorF230A.IsChuckAVacuumReady = True Then
                    Return True
                End If

            Case enmMachineType.DCS_350A
                'TODO:待補
        End Select

        Return False
    End Function

    Private Sub btnVacuum_Click(sender As Object, e As EventArgs) Handles btnVacuum.Click
        gSyslog.Save("[frmStageAccuracy]" & vbTab & "[btnVacuum]" & vbTab & "Click")

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If (btnVacuum.Text = "ON") Then
                    btnVacuum.Text = "OFF"
                    btnVacuum.BackColor = SystemColors.Control
                    If sys.MachineNo = enmMachineStation.MachineA Then
                        WetcoConveyor.Unit.A_VacuumControl(False)
                    ElseIf sys.MachineNo = enmMachineStation.MachineB Then
                        WetcoConveyor.Unit.B_VacuumControl(False)
                    Else
                        btnVacuum.Text = "OFF"
                        btnVacuum.BackColor = Color.Red
                        WetcoConveyor.Unit.A_VacuumControl(False)
                        WetcoConveyor.Unit.B_VacuumControl(False)
                    End If
                Else
                    btnVacuum.Text = "ON"
                    btnVacuum.BackColor = Color.Yellow
                    If sys.MachineNo = enmMachineStation.MachineA Then
                        WetcoConveyor.Unit.A_VacuumControl(True)

                        If WetcoConveyor.Unit.A_IsVacuum = False Then
                            btnVacuum.Text = "OFF"
                            btnVacuum.BackColor = Color.Red
                            WetcoConveyor.Unit.A_VacuumControl(False)
                        End If
                    ElseIf sys.MachineNo = enmMachineStation.MachineB Then
                        WetcoConveyor.Unit.B_VacuumControl(True)

                        If WetcoConveyor.Unit.B_IsVacuum = False Then
                            btnVacuum.Text = "OFF"
                            btnVacuum.BackColor = Color.Red
                            WetcoConveyor.Unit.B_VacuumControl(False)
                        End If
                    Else
                        btnVacuum.Text = "OFF"
                        btnVacuum.BackColor = Color.Red
                        WetcoConveyor.Unit.A_VacuumControl(False)
                        WetcoConveyor.Unit.B_VacuumControl(False)
                    End If
                End If

            Case Else
                If (btnVacuum.Text = "ON") Then
                    btnVacuum.Text = "OFF"
                    btnVacuum.BackColor = SystemColors.Control
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, False)
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum, True)
                Else
                    btnVacuum.Text = "ON"
                    btnVacuum.BackColor = Color.Yellow
                    gDOCollection.SetState(enmDO.Station2ChuckVacuum, False)
                    gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, True)
                    System.Threading.Thread.CurrentThread.Join(1000)
                    If gDICollection.GetState(enmDI.Station2ChuckVacuumReady, True) = False Then
                        btnVacuum.Text = "OFF"
                        btnVacuum.BackColor = Color.Red
                    End If
                End If

        End Select
    End Sub

    Private btn_Count As Integer = 0
    Private Sub btnScene_Click(sender As Object, e As EventArgs) Handles btnScene.Click
        gSyslog.Save("[frmStageVerification]" & vbTab & "[btnScene]" & vbTab & "Click")
        If btnScene.Enabled = False Then '防連按
            Exit Sub
        End If
        btnScene.Enabled = False

        'Dim SceneStr As String = "SVT" & (sys.StageNo + 1).ToString

        btn_Count = btn_Count + 1
        Select Case (btn_Count)
            Case 0 '平台精準度測試
                btnScene.Text = "CALIBSVT" & (sys.StageNo + 1).ToString
                Flag_ManualDot = False
                lblAction.Text = "CALIBSVT"
            Case 1 '點膠偏差測試
                btnScene.Text = "CALIBMR" & (sys.StageNo + 1).ToString
                Flag_ManualDot = False
                lblAction.Text = "Measure"
            Case 2 '手動點膠測試
                btnScene.Text = "CALIBSVT" & (sys.StageNo + 1).ToString
                Flag_ManualDot = True
                btn_Count = -1
                lblAction.Text = "Maunal Dot"
            Case Else
                btnScene.Text = "CALIBSVT" & (sys.StageNo + 1).ToString
                Flag_ManualDot = False
                lblAction.Text = "CALIBSVT"
                btn_Count = -1
        End Select

        'If btnScene.Text = SceneStr Then
        '    btnScene.Text = "Precision" & (sys.StageNo + 1).ToString
        'ElseIf btnScene.Text = SceneStr Then

        '    btnScene.Text = "SVT" & (sys.StageNo + 1).ToString
        'End If
        '場景設定
        Dim fileName As String
        fileName = Application.StartupPath & "\System\" & MachineName & "\" & btnScene.Text & ".ini" '光源設定檔路徑
        gAOICollection.LoadSceneParameter(btnScene.Text, fileName) '讀取光源,曝光值等設定
        If gAOICollection.IsSceneExist(sys.CCDNo, btnScene.Text) Then
            gAOICollection.SetCCDScene(sys.CCDNo, btnScene.Text) '曝光,亮度
            SelectScene(btnScene.Text) '場景開光
            UcLightControl1.CCDNo = sys.CCDNo
            UcLightControl1.SceneName = btnScene.Text
            UcLightControl1.ShowUI()
        End If

        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照
        System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        '--- 2016.06.18 + 開燈,曝光,觸發後才會改 ---




        btnScene.Enabled = True
    End Sub

   

    Private Sub bt_LoadStage_Click(sender As Object, e As EventArgs) Handles btnStage1Read.Click, btnStage2Read.Click, btnStage3Read.Click, btnStage4Read.Click
        'Eason 20170303 Ticket:100100 , XY Offset from CSV File [S]
        Dim BufferButton As Button = CType(sender, Button)
        Dim NowStage As enmStage = CType(BufferButton.Tag, Integer)
        Dim TextMap() As TextBox = New TextBox() {txtStage1Data, txtStage2Data, txtStage3Data, txtStage4Data}

        gStageOffsetFromSCV(NowStage) = New CStageCalibrationOffsetFromSCV()

        Dim FileName As String = gStageOffsetFromSCV(NowStage).OpenFile()

        TextMap(NowStage).ForeColor = Color.Black
        If (gStageOffsetFromSCV(NowStage).LoadFile(FileName, gSSystemParameter.StageFixMode) = 0) Then
            TextMap(NowStage).Text = FileName
            gStageOffsetFromSCV(NowStage).Enable = True
            Dim Commresult As CommandStatus = CommandStatus.Alarm
            Commresult = gCMotion.Dev2DCompensateTable(enmAxis.XAxis, enmAxis.Y1Axis, gStageOffsetFromSCV(NowStage).StartPositionX, gStageOffsetFromSCV(NowStage).StartPositionY, gStageOffsetFromSCV(NowStage).PitchX, gStageOffsetFromSCV(NowStage).PitchY, gStageOffsetFromSCV(NowStage).OffsetX, gStageOffsetFromSCV(NowStage).OffsetY)
            If (Commresult = CommandStatus.Sucessed) Then
                gCMotion.Dev2DCompensateTableEnable(enmAxis.XAxis, enmAxis.Y1Axis, True)
            End If
        Else
            gStageOffsetFromSCV(NowStage).Enable = False
            TextMap(NowStage).ForeColor = Color.Red
            TextMap(NowStage).Text = ""
            gCMotion.Dev2DCompensateTableEnable(enmAxis.XAxis, enmAxis.Y1Axis, False)
        End If
        'Eason 20170303 Ticket:100100 , XY Offset from CSV File [E]
    End Sub

    Private Sub bt_CleanStage_Click(sender As Object, e As EventArgs) Handles btnStage1Clean.Click, btnStage2Clean.Click, btnStage3Clean.Click, btnStage4Clean.Click
        'Eason 20170303 Ticket:100100 , XY Offset from CSV File [S]
        Dim BufferButton As Button = CType(sender, Button)
        Dim NowStage As enmStage = CType(BufferButton.Tag, Integer)
        Dim TextMap() As TextBox = New TextBox() {txtStage1Data, txtStage2Data, txtStage3Data, txtStage4Data}
        gStageOffsetFromSCV(NowStage).Clear()
        gStageOffsetFromSCV(NowStage).Enable = False
        TextMap(NowStage).Text = ""
        gCMotion.Dev2DCompensateTableEnable(enmAxis.XAxis, enmAxis.Y1Axis, False)
        'Eason 20170303 Ticket:100100 , XY Offset from CSV File [E]
    End Sub
End Class

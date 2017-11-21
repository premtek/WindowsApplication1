Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectLaserInterferometer
Imports ProjectAOI

Public Class frmRecipe02
    ''' <summary>外部配接
    ''' </summary>
    ''' <remarks></remarks>
    Public sys As sSysParam

    ''' <summary>外部配接Recipe
    ''' </summary>
    ''' <remarks></remarks>
    Public RecipeEdit As ProjectRecipe.CRecipe

    Public NodeID As String
    Public PatternID As String
    ''' <summary>多層陣列資料配接器</summary>
    ''' <remarks></remarks>
    Dim mMultiArrayAdapter As CMultiArrayAdapter
    Dim StageNo As Integer
    Dim mAbsValveNo As Integer = enmValve.No1
    Public Arraytemp As New List(Of CRecipeNodeLevel)
    Private Sub frmRecipe02_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        mMultiArrayAdapter.ManualDispose() '釋放陣列資料
        'Eason 20170124 Ticket:100031 , Memory Freed [S]
        UcDisplay1.EndLive()
        UcDisplay1.ManualDispose()
        UcLightControl1.ManualDispose()
        ucJoyStick1.ManualDispose()
        Me.Dispose(True)
        'Eason 20170124 Ticket:100031 , Memory Freed [E]

    End Sub

    ''' <summary>
    ''' 介面更新
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshUI()
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
            ucJoyStick1.Enabled = False
        Else
            ucJoyStick1.Enabled = True
        End If

        UcLevelArray1.RefreshUI()
        UcLevelNonArray1.RefreshUI()
        mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(StageNo)(NodeID).Array)
        mMultiArrayAdapter.Draw(picMap) 'Soni + 2017.01.24 更新介面一併繪圖
        ShowArray()
        UcLevelNonArray1.LockNumericUpDown()
    End Sub

    Dim backupPitchX As Decimal
    Dim backupPitchY As Decimal
    Dim conveyorNo As eConveyor

    Private Sub frmRecipe02_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '[Note]:使用該組Stage的第一組閥
        sys.SelectValve = eValveWorkMode.Valve1
        mAbsValveNo = sys.ValveNo(eValveWorkMode.Valve1)
        ucJoyStick1.AxisX = sys.AxisX
        ucJoyStick1.AxisY = sys.AxisY
        ucJoyStick1.AxisZ = sys.AxisZ
        ucJoyStick1.AXisB = sys.AxisB
        ucJoyStick1.AXisC = sys.AxisC
        ucJoyStick1.AXisA = sys.AxisA
        ucJoyStick1.SetSpeedType(SpeedType.Slow)
        ucJoyStick1.RefreshPosition()

        If (gSSystemParameter.StageCount > 1) Then
            If (gSSystemParameter.MachineSafeData.Count > 0) Then
                ucJoyStick1.InverseAxisX.SafeDistance = gSSystemParameter.MachineSafeData(sys.MachineNo).SafeDistanceX
                ucJoyStick1.InverseAxisX.Spread = gSSystemParameter.MachineSafeData(sys.MachineNo).SpreadX

                If (sys.StageNo = enmStage.No1) Then
                    ucJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage2).AxisX    '對立軸
                    ucJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Posivtive
                ElseIf (sys.StageNo = enmStage.No2) Then
                    ucJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage1).AxisX    '對立軸
                    ucJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Negative
                ElseIf (sys.StageNo = enmStage.No3) Then
                    ucJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage4).AxisX    '對立軸
                    ucJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Posivtive
                ElseIf (sys.StageNo = enmStage.No4) Then
                    ucJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage3).AxisX    '對立軸
                    ucJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Negative
                End If
            End If
        End If

        UcLevelArray1.PatternID = PatternID
        UcLevelArray1.NodeID = NodeID
        UcLevelArray1.sys = sys
        UcLevelArray1.RecipeEdit = RecipeEdit
        UcLevelNonArray1.NodeID = NodeID
        UcLevelNonArray1.sys = sys



        Me.Text = "Node(" & NodeID & ") Array"
        'Jeffadd 設定StageNum
        StageNo = CInt(NodeID.Split(",")(1))
        If Not RecipeEdit.Node(StageNo).ContainsKey(NodeID) Then '節點不存在, 建立會失敗
            '請先選擇Node
            gSyslog.Save(NodeID & gMsgHandler.GetMessage(Warn_3000025))
            MsgBox(NodeID & gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Me.Close()
            Exit Sub
        End If

        '=== 層級清單更新 ===
        UpdateCmbLevel(0)
        '=== 層級清單更新 ===




        nmcStartLocX.Value = IIf(RecipeEdit.Node(StageNo)(NodeID).NodeStartingX < 1, 1, RecipeEdit.Node(StageNo)(NodeID).NodeStartingX)
        nmcStartLocY.Value = IIf(RecipeEdit.Node(StageNo)(NodeID).NodeStartingY < 1, 1, RecipeEdit.Node(StageNo)(NodeID).NodeStartingY)


        Select Case gAOICollection.GetCCDType(sys.CCDNo)
            Case enmCCDType.CognexVPRO
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
            Case enmCCDType.OmronFZS2MUDP
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
            Case Else
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
        End Select

        '--- 2016.06.18 + 開燈,曝光,觸發後才會改 ---
        Dim mSceneName As String = "CALIB" & (sys.CCDNo + 1).ToString '預設CALIB1校正場景
        If RecipeEdit.Node(StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count > 0 Then 'Soni 2017.02.09 雙軌資料結構
            If RecipeEdit.Node(StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene <> "" Then '如果定位點已設定可使用,則採用第一定位點
                If gAOICollection.SceneDictionary.ContainsKey(RecipeEdit.Node(StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene) Then
                    mSceneName = RecipeEdit.Node(StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
                End If
            End If
        End If

        UcLightControl1.CCDNo = sys.CCDNo
        If gAOICollection.SceneDictionary.ContainsKey(mSceneName) Then
            '[Note]:使用定位點場景光源，若沒有則使用CALIB1校正場景
            UcLightControl1.SceneName = mSceneName
            UcLightControl1.ShowUI()
            'SelectScene(mSceneName) '場景開光
        End If
        If gAOICollection.IsSceneExist(sys.CCDNo, mSceneName) Then
            gAOICollection.SetCCDScene(sys.CCDNo, mSceneName) '曝光,亮度
        End If


        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        '--- 2016.06.18 + 開燈,曝光,觸發後才會改 ---

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
        'jimmy 20170808 暫存Array
        Arraytemp = New List(Of CRecipeNodeLevel)
        Dim clsTemp As CRecipeNodeLevel

        For i = 0 To RecipeEdit.Node(StageNo)(NodeID).Array.Count - 1
            clsTemp = New CRecipeNodeLevel
            clsTemp.Array.CountX = RecipeEdit.Node(StageNo)(NodeID).Array.Item(i).Array.CountX
            clsTemp.Array.CountY = RecipeEdit.Node(StageNo)(NodeID).Array.Item(i).Array.CountY
            clsTemp.Array.EndPosX = RecipeEdit.Node(StageNo)(NodeID).Array.Item(i).Array.EndPosX
            clsTemp.Array.EndPosY = RecipeEdit.Node(StageNo)(NodeID).Array.Item(i).Array.EndPosY
            clsTemp.Array.PitchX = RecipeEdit.Node(StageNo)(NodeID).Array.Item(i).Array.PitchX
            clsTemp.Array.PitchY = RecipeEdit.Node(StageNo)(NodeID).Array.Item(i).Array.PitchY
            clsTemp.Array.StartPosX = RecipeEdit.Node(StageNo)(NodeID).Array.Item(i).Array.StartPosX
            clsTemp.Array.StartPosY = RecipeEdit.Node(StageNo)(NodeID).Array.Item(i).Array.StartPosY
            clsTemp.Array.StartPosZ = RecipeEdit.Node(StageNo)(NodeID).Array.Item(i).Array.StartPosZ
            clsTemp.Array.Theta = RecipeEdit.Node(StageNo)(NodeID).Array.Item(i).Array.Theta
            clsTemp.LevelType = RecipeEdit.Node(StageNo)(NodeID).Array.Item(i).LevelType
            Arraytemp.Add(clsTemp)
        Next

        '' Arraytemp = Recipe.Node(StageNo)(NodeID).Array
        'Arraytemp.AddRange(Recipe.Node(StageNo)(NodeID).Array)
        ReDim UcLevelNonArray1.NonArrayTemp(RecipeEdit.Node(StageNo)(NodeID).Array.Count - 1)
        For index = 0 To RecipeEdit.Node(StageNo)(NodeID).Array.Count - 1
            UcLevelNonArray1.NonArrayTemp(index) = New List(Of NonArray)
            UcLevelNonArray1.NonArrayTemp(index).AddRange(RecipeEdit.Node(StageNo)(NodeID).Array(index).NonArray)
        Next
        RefreshUI()
        UcLevelArray1.IsFormLoaded = True
        UcLevelNonArray1.IsFormLoaded = True
    End Sub



    Private Sub ShowArray()
        cmbArrayX.Items.Clear()
        If mMultiArrayAdapter.GetMemoryCountX() <= 0 Then
            cmbArrayX.Items.Add(0)
            cmbArrayX.SelectedIndex = 0
        Else
            For i As Integer = 1 To mMultiArrayAdapter.GetMemoryCountX()
                cmbArrayX.Items.Add(i.ToString("000"))
            Next
            Premtek.ControlMisc.SetComboBox(cmbArrayX, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexX, 1)
        End If

        cmbArrayY.Items.Clear()
        If mMultiArrayAdapter.GetMemoryCountY() <= 0 Then
            cmbArrayY.Items.Add(0)
            cmbArrayY.SelectedIndex = 0
        Else
            For i As Integer = 1 To mMultiArrayAdapter.GetMemoryCountY()
                cmbArrayY.Items.Add(i.ToString("000"))
            Next
            Premtek.ControlMisc.SetComboBox(cmbArrayY, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexY, 1)
        End If

        If cmbArrayX.Items.Count = 1 And cmbArrayY.Items.Count = 1 Then '都是1選1,沒得選
            cmbArrayX.SelectedIndex = 0
            cmbArrayY.SelectedIndex = 0
            grpIndex.Visible = False
        Else
            grpIndex.Visible = True
        End If
    End Sub

    Sub SetNumericValue(ByRef nmc As NumericUpDown, ByVal value As Decimal)
        If nmc Is Nothing Then
            Exit Sub
        End If
        If nmc.Minimum > value Then
            nmc.BackColor = Color.Red
            Exit Sub
        End If
        If nmc.Maximum < value Then
            nmc.BackColor = Color.Red
            Exit Sub
        End If
        nmc.Value = value
    End Sub

    ''' <summary>
    ''' 場景選擇
    ''' </summary>
    ''' <param name="sceneName"></param>
    ''' <remarks></remarks>
    Sub SelectScene(ByVal sceneName As String)
        If gAOICollection.SceneDictionary.ContainsKey(sceneName) Then
            Dim light As enmLight
            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1)
            gLightCollection.SetCCDLight(sys.CCDNo, light, gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No1), True)
            gSysAdapter.SetLightOnOff(light, gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No1))
            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2)
            gLightCollection.SetCCDLight(sys.CCDNo, light, gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No2), True)
            gSysAdapter.SetLightOnOff(light, gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No2))
            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3)
            gLightCollection.SetCCDLight(sys.CCDNo, light, gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No3), True)
            gSysAdapter.SetLightOnOff(light, gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No3))
            light = gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4)
            gLightCollection.SetCCDLight(sys.CCDNo, light, gAOICollection.SceneDictionary(sceneName).LightValue(enmValveLight.No4), True)
            gSysAdapter.SetLightOnOff(light, gAOICollection.SceneDictionary(sceneName).LightEnable(enmValveLight.No4))

        Else
            '場景不存在
            gSyslog.Save(sceneName & gMsgHandler.GetMessage(Warn_3000020))
            MsgBox(sceneName & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    'Private Sub btnGoCCDAlignPos_Click(sender As Object, e As EventArgs)
    '    gSyslog.Save("[frmRecipe02]" & vbTab & "[btnGoCCDAlignPos]" & vbTab & "Click")
    '    'btnGoCCDAlignPos.Enabled = False
    '    '20170602按鍵保護
    '    Btn_Control(False)

    '    Select Case gSYS(eSys.OverAll).Act(eAct.Home).RunStatus
    '        Case enmRunStatus.Finish
    '        Case enmRunStatus.Alarm
    '            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)

    '            'btnGoCCDAlignPos.Enabled = True
    '            '20170602按鍵保護
    '            Btn_Control(True)
    '            Exit Sub
    '        Case enmRunStatus.Running
    '            gEqpMsg.AddHistoryAlarm("Warn_3000006", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000006), eMessageLevel.Warning)

    '            'btnGoCCDAlignPos.Enabled = True
    '            '20170602按鍵保護
    '            Btn_Control(True)
    '            Exit Sub
    '            'Case enmRunStatus.Pause
    '        Case enmRunStatus.None
    '            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)

    '            'btnGoCCDAlignPos.Enabled = True
    '            '20170602按鍵保護
    '            Btn_Control(True)
    '            Exit Sub
    '    End Select

    '    If RecipeEdit Is Nothing Then
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011), "Warn_3000011", eMessageLevel.Warning)
    '        'btnGoCCDAlignPos.Enabled = True
    '        '20170602按鍵保護
    '        Btn_Control(True)
    '        Exit Sub
    '    End If

    '    If RecipeEdit.strFileName = "" Then
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011), "Warn_3000011", eMessageLevel.Warning)
    '        'btnGoCCDAlignPos.Enabled = True
    '        '20170602按鍵保護
    '        Btn_Control(True)
    '        Exit Sub
    '    End If
    '    'Soni 2017.02.09 雙軌資料結構
    '    If RecipeEdit.Node(StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count <= 0 Then
    '        'TODO:請Soni確認
    '        gSyslog.Save(gMsgHandler.GetMessage(Error_1000000))
    '        MsgBox(gMsgHandler.GetMessage(Error_1000000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    '        'MsgBox("Alignment Not Exists!")
    '        'btnGoCCDAlignPos.Enabled = True
    '        '20170602按鍵保護
    '        Btn_Control(True)
    '        Exit Sub
    '    End If
    '    Dim targetPosX As Decimal = RecipeEdit.Node(StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX
    '    Dim targetPosY As Decimal = RecipeEdit.Node(StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY


    '    '[說明]:回Home完成才能執行
    '    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
    '        'If Not Recipe Is Nothing Then
    '        '[說明]:X、Y、Z軸
    '        If sys.AxisX > -1 Then
    '            gCMotion.SetVelAccDec(sys.AxisX)
    '        End If
    '        If sys.AxisY > -1 Then
    '            gCMotion.SetVelAccDec(sys.AxisY)
    '        End If
    '        If sys.AxisZ > -1 Then
    '            gCMotion.SetVelAccDec(sys.AxisZ)
    '        End If
    '        If sys.AxisB > -1 Then
    '            gCMotion.SetVelAccDec(sys.AxisB)
    '        End If
    '        If sys.AxisC > -1 Then
    '            gCMotion.SetVelAccDec(sys.AxisC)
    '        End If
    '        Select Case gSSystemParameter.CCDModuleType
    '            Case enmCCDModule.eFix

    '            Case enmCCDModule.eFree

    '                Call gCMotion.AbsMove(sys.AxisZ, RecipeEdit.Node(StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ) 'Soni 2017.02.09 雙軌資料結構
    '        End Select

    '        Dim intXw As Integer = 0
    '        Dim intYw As Integer = 0
    '        Dim intXb As Integer = 0
    '        Dim intYb As Integer = 0
    '        Dim intXf As Integer = 0
    '        Dim intYf As Integer = 0


    '        Dim idx As New sLevelIndexCollection
    '        idx.Xf = intXf
    '        idx.Yf = intYf
    '        idx.NodeName = NodeID
    '        Select Case RecipeEdit.Node(StageNo)(gfrmRecipe04.SelectedNodePath).AlignType
    '            Case enmAlignType.DevicePos1
    '                Call GetCCDScanPos(sys, idx, targetPosX, targetPosY, enmAlignType.DevicePos1, mMultiArrayAdapter)
    '            Case enmAlignType.DevicePos2
    '                Static alignIndex As enmAlignType
    '                If alignIndex = enmAlignType.DevicePos1 Then
    '                    Call GetCCDScanPos(sys, idx, targetPosX, targetPosY, enmAlignType.DevicePos1, mMultiArrayAdapter)
    '                    alignIndex = enmAlignType.DevicePos2
    '                Else
    '                    Call GetCCDScanPos(sys, idx, targetPosX, targetPosY, enmAlignType.DevicePos2, mMultiArrayAdapter)
    '                    alignIndex = enmAlignType.DevicePos1
    '                End If
    '        End Select

    '        Dim AxisNo(4) As Integer
    '        Dim TargetPos(4) As Decimal
    '        AxisNo(0) = sys.AxisX
    '        AxisNo(1) = sys.AxisY
    '        AxisNo(2) = sys.AxisZ
    '        AxisNo(3) = sys.AxisB
    '        AxisNo(4) = sys.AxisC



    '        TargetPos(0) = targetPosX
    '        TargetPos(1) = targetPosY
    '        TargetPos(2) = RecipeEdit.Node(StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
    '        TargetPos(3) = 0
    '        TargetPos(4) = 0
    '        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)

    '        ucJoyStick1.RefreshPosition()
    '        'End If
    '    End If
    '    'btnGoCCDAlignPos.Enabled = True
    '    '20170602按鍵保護
    '    Btn_Control(True)
    'End Sub

    'Private Async Sub btnGoFindHeightPos_Click(sender As Object, e As EventArgs)
    '    gSyslog.Save("[frmRecipe02]" & vbTab & "[btnGoFindHeightPos]" & vbTab & "Click")
    '    'btnGoFindHeightPos.Enabled = False
    '    '20170602按鍵保護
    '    Btn_Control(False)
    '    Select Case gSYS(eSys.OverAll).Act(eAct.Home).RunStatus
    '        Case enmRunStatus.Finish
    '        Case enmRunStatus.Alarm
    '            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)

    '            'btnGoFindHeightPos.Enabled = True
    '            '20170602按鍵保護
    '            Btn_Control(True)
    '            Exit Sub
    '        Case enmRunStatus.Running
    '            gEqpMsg.AddHistoryAlarm("Warn_3000006", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000006), eMessageLevel.Warning)

    '            'btnGoFindHeightPos.Enabled = True
    '            '20170602按鍵保護
    '            Btn_Control(True)
    '            Exit Sub
    '            'Case enmRunStatus.Pause
    '        Case enmRunStatus.None
    '            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)

    '            'btnGoFindHeightPos.Enabled = True
    '            '20170602按鍵保護
    '            Btn_Control(True)
    '            Exit Sub
    '    End Select

    '    If RecipeEdit Is Nothing Then
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011), "Warn_3000011", eMessageLevel.Warning)
    '        'btnGoFindHeightPos.Enabled = True
    '        '20170602按鍵保護
    '        Btn_Control(True)
    '        Exit Sub
    '    End If

    '    If RecipeEdit.strFileName = "" Then
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011), "Warn_3000011", eMessageLevel.Warning)
    '        'btnGoFindHeightPos.Enabled = True
    '        '20170602按鍵保護
    '        Btn_Control(True)
    '        Exit Sub
    '    End If
    '    Await Task.Run(Sub()
    '                       If gCMotion.GetPositionValue(sys.AxisZ) > gSSystemParameter.Pos.SafePosZ Then
    '                           gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos)
    '                           Do
    '                               System.Threading.Thread.Sleep(1)
    '                               If gDICollection.GetState(enmDI.EMO, True) = True Then
    '                                   'btnGoFindHeightPos.Enabled = True
    '                                   '20170602按鍵保護
    '                                   Btn_Control(True)
    '                                   Exit Sub
    '                               End If
    '                           Loop Until gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed
    '                       End If
    '                   End Sub)

    '    gCMotion.SetVelAccDec(sys.AxisX)
    '    gCMotion.SetVelAccDec(sys.AxisY)
    '    '[說明]:Y軸

    '    Dim intXf As Integer, intYf As Integer


    '    Dim targetPosX As Decimal
    '    Dim targetPosY As Decimal
    '    Dim index As New sLevelIndexCollection
    '    index.Xf = intXf
    '    index.Yf = intYf
    '    index.NodeName = NodeID
    '    Call GetLaserPos(sys, index, targetPosX, targetPosY, mMultiArrayAdapter)

    '    Dim AxisNo(4) As Integer
    '    Dim TargetPos(4) As Decimal
    '    AxisNo(0) = sys.AxisX
    '    AxisNo(1) = sys.AxisY
    '    AxisNo(2) = sys.AxisZ
    '    AxisNo(3) = sys.AxisB
    '    AxisNo(4) = sys.AxisC

    '    TargetPos(0) = targetPosX
    '    TargetPos(1) = targetPosY
    '    TargetPos(2) = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).LaserData(0).LaserPositionZ
    '    TargetPos(3) = 0
    '    TargetPos(4) = 0
    '    ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
    '    ucJoyStick1.RefreshPosition()
    '    'btnGoFindHeightPos.Enabled = True
    '    '20170602按鍵保護
    '    Btn_Control(True)
    'End Sub

    'Private Async Sub btnGoDispenseBasePos_Click(sender As Object, e As EventArgs)
    '    gSyslog.Save("[frmRecipe02]" & vbTab & "[btnGoDispenseBasePos]" & vbTab & "Click")
    '    'btnGoDispenseBasePos.Enabled = False
    '    '20170602按鍵保護
    '    Btn_Control(False)
    '    Select Case gSYS(eSys.OverAll).Act(eAct.Home).RunStatus
    '        Case enmRunStatus.Finish
    '        Case enmRunStatus.Alarm
    '            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)

    '            'btnGoDispenseBasePos.Enabled = True
    '            '20170602按鍵保護
    '            Btn_Control(True)
    '            Exit Sub
    '        Case enmRunStatus.Running
    '            gEqpMsg.AddHistoryAlarm("Warn_3000006", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000006), eMessageLevel.Warning)

    '            'btnGoDispenseBasePos.Enabled = True
    '            '20170602按鍵保護
    '            Btn_Control(True)
    '            Exit Sub
    '            'Case enmRunStatus.Pause
    '        Case enmRunStatus.None
    '            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)

    '            'btnGoDispenseBasePos.Enabled = True
    '            '20170602按鍵保護
    '            Btn_Control(True)
    '            Exit Sub
    '    End Select

    '    If RecipeEdit Is Nothing Then
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011), "Warn_3000011", eMessageLevel.Warning)
    '        'btnGoDispenseBasePos.Enabled = True
    '        '20170602按鍵保護
    '        Btn_Control(True)
    '        Exit Sub
    '    End If

    '    If RecipeEdit.strFileName = "" Then
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011), "Warn_3000011", eMessageLevel.Warning)
    '        'btnGoDispenseBasePos.Enabled = True
    '        '20170602按鍵保護
    '        Btn_Control(True)
    '        Exit Sub
    '    End If
    '    Await Task.Run(Sub()
    '                       If gCMotion.GetPositionValue(sys.AxisZ) > gSSystemParameter.Pos.SafePosZ Then
    '                           gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos)
    '                           Do
    '                               System.Threading.Thread.Sleep(1)
    '                               If gDICollection.GetState(enmDI.EMO, True) = True Then
    '                                   '20170602按鍵保護
    '                                   Btn_Control(True)
    '                                   Exit Sub
    '                               End If
    '                           Loop Until gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed
    '                       End If
    '                   End Sub)
    '    gCMotion.SetVelAccDec(sys.AxisX)
    '    gCMotion.SetVelAccDec(sys.AxisY)

    '    Dim intXf As Integer, intYf As Integer



    '    Dim targetPosX As Decimal
    '    Dim targetPosY As Decimal
    '    Dim index As New sLevelIndexCollection

    '    index.Xf = intXf
    '    index.Yf = intYf
    '    index.NodeName = NodeID
    '    GetCCDScanPos(sys, index, targetPosX, targetPosY, RecipeEdit.Node(sys.StageNo)(gfrmRecipe04.SelectedNodePath).AlignType, mMultiArrayAdapter)
    '    'jimmy 20161006


    '    Dim AxisNo(4) As Integer
    '    Dim TargetPos(4) As Decimal
    '    AxisNo(0) = sys.AxisX
    '    AxisNo(1) = sys.AxisY
    '    AxisNo(2) = sys.AxisZ
    '    AxisNo(3) = sys.AxisB
    '    AxisNo(4) = sys.AxisC

    '    TargetPos(0) = targetPosX
    '    TargetPos(1) = targetPosY
    '    TargetPos(2) = gSSystemParameter.Pos.ZUpPos
    '    TargetPos(3) = 0
    '    TargetPos(4) = 0
    '    ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
    '    ucJoyStick1.RefreshPosition()
    '    'btnGoDispenseBasePos.Enabled = True
    '    '20170602按鍵保護
    '    Btn_Control(True)
    'End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmRecipe02]" & vbTab & "[btnOK]" & vbTab & "Click")
        If cmbArrayX.SelectedIndex <> -1 Then RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexX = cmbArrayX.SelectedIndex
        If cmbArrayY.SelectedIndex <> -1 Then RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexY = cmbArrayY.SelectedIndex
        RecipeEdit.UpdateOriginData(sys.StageNo, NodeID, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexX, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexY, mMultiArrayAdapter) '設定陣列時, 依索引重新更新(0,0)資料

        RecipeEdit.UpdateOriginDataConveyorHeight(sys.StageNo, NodeID, eConveyor.ConveyorNo1, 0, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexX, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexY, mMultiArrayAdapter)
        RecipeEdit.UpdateOriginDataConveyorHeight(sys.StageNo, NodeID, eConveyor.ConveyorNo2, 0, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexX, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexY, mMultiArrayAdapter)
        ReDim UcLevelNonArray1.NonArrayTemp(RecipeEdit.Node(StageNo)(NodeID).Array.Count - 1)
        RecipeEdit.Node(StageNo)(NodeID).NodeStartingX = nmcStartLocX.Value
        RecipeEdit.Node(StageNo)(NodeID).NodeStartingY = nmcStartLocY.Value

        'RecipeEdit.SaveNodeArray(RecipeEdit.strFileName, StageNo, NodeID)
        'jimmy 201170808 暫存
        Arraytemp.Clear()
        Arraytemp.AddRange(RecipeEdit.Node(StageNo)(NodeID).Array)
        For index = 0 To RecipeEdit.Node(StageNo)(NodeID).Array.Count - 1
            UcLevelNonArray1.NonArrayTemp(index) = New List(Of NonArray)
            UcLevelNonArray1.NonArrayTemp(index).AddRange(RecipeEdit.Node(StageNo)(NodeID).Array(index).NonArray)
        Next
        RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexX = cmbArrayX.SelectedIndex
        RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexY = cmbArrayY.SelectedIndex
        'RecipeEdit.SaveNode(RecipeEdit.strFileName)   'Mobary+ 2016.10.01
        'Sue20170627
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'jimmy 201170808 暫存
        RecipeEdit.Node(StageNo)(NodeID).Array = Arraytemp
        For index = 0 To RecipeEdit.Node(StageNo)(NodeID).Array.Count - 1
            RecipeEdit.Node(StageNo)(NodeID).Array(index).NonArray = UcLevelNonArray1.NonArrayTemp(index)
        Next
        gSyslog.Save("[frmRecipe02]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub




    Private Sub cmbLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLevel.SelectedIndexChanged
        Dim mLevelNo As Integer = cmbLevel.SelectedIndex
        UcLevelArray1.LevelNo = mLevelNo
        UcLevelNonArray1.LevelNo = mLevelNo

        If cmbLevel.SelectedIndex < 0 Then
            Exit Sub
        End If



        btnArray.Enabled = True
        btnNonArray.Enabled = True


        Select Case RecipeEdit.Node(StageNo)(NodeID).Array(mLevelNo).LevelType
            Case eLevelType.Array
                UcLevelArray1.Visible = True
                UcLevelNonArray1.Visible = False
                btnArray.BackColor = Color.Yellow
                btnNonArray.BackColor = SystemColors.Control
            Case eLevelType.NoneArray
                UcLevelArray1.Visible = False
                UcLevelNonArray1.Visible = True
                btnArray.BackColor = SystemColors.Control
                btnNonArray.BackColor = Color.Yellow
        End Select

        If RecipeEdit.Node(StageNo)(NodeID).Array.Count > 0 Then '陣列資料載入
            UcLevelArray1.IsFormLoaded = False
            UcLevelArray1.LoadData()
            UcLevelArray1.IsFormLoaded = True

            '--- 資料備份 ---
            backupPitchX = RecipeEdit.Node(StageNo)(NodeID).Array(mLevelNo).Array.PitchX
            backupPitchY = RecipeEdit.Node(StageNo)(NodeID).Array(mLevelNo).Array.PitchY
            '--- 資料備份 ---
        End If

        UcLevelArray1.RefreshUI()

        UcLevelNonArray1.BasicPosX = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX 'Soni / 2017.07.03 傳入第一定位點拍照位置為基準. 使用CCD定位位置是因為提供CCD設定的位置較點膠位置直觀
        UcLevelNonArray1.BasicPosY = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY 'Soni / 2017.07.03
        UcLevelNonArray1.NonArray = RecipeEdit.Node(StageNo)(NodeID).Array(mLevelNo).NonArray '非陣列資料載入
        UcLevelNonArray1.RefreshUI()
    End Sub

    Sub UpdateCmbLevel(ByVal Defaultselect As Integer)
        '=== 層級清單 === 
        With cmbLevel.Items
            .Clear()
            For i As Integer = 0 To RecipeEdit.Node(StageNo)(NodeID).Array.Count - 1
                .Add(i)
            Next
            If Defaultselect < 0 Then
                Defaultselect = 0
            End If
            If .Count > Defaultselect Then
                cmbLevel.SelectedIndex = Defaultselect
            Else
                btnArray.Enabled = False
                btnNonArray.Enabled = False

            End If
        End With
        '=== 層級清單 ===
        If cmbLevel.Items.Count < 2 Then
            btnLevelDelete.Enabled = False
        Else
            btnLevelDelete.Enabled = True
        End If

    End Sub
    Private Sub btnLevelAdd_Click(sender As Object, e As EventArgs) Handles btnLevelAdd.Click
        'Sue20170627
        gSyslog.Save("[frmRecipe02]" & vbTab & "[btnLevelAdd]" & vbTab & "Click")
        Dim mLevel As New CRecipeNodeLevel
        mLevel.Array = New CArray
        mLevel.NonArray = New List(Of NonArray)
        RecipeEdit.Node(StageNo)(NodeID).Array.Add(mLevel)

        UpdateCmbLevel(RecipeEdit.Node(StageNo)(NodeID).Array.Count - 1)
        RefreshUI()
    End Sub

    Private Sub btnLevelDelete_Click(sender As Object, e As EventArgs) Handles btnLevelDelete.Click
        'Sue20170627
        gSyslog.Save("[frmRecipe02]" & vbTab & "[btnLevelDelete]" & vbTab & "Click")
        If cmbLevel.SelectedIndex < 0 Then
            '請選擇項目
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000063))
            MsgBox(gMsgHandler.GetMessage(Warn_3000063), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Dim mSelected As Integer = cmbLevel.SelectedIndex
        RecipeEdit.Node(StageNo)(NodeID).Array.RemoveAt(mSelected)
        mSelected -= 1 '刪除選前一筆
        If mSelected < 0 Then mSelected = 0
        UpdateCmbLevel(mSelected)
        RefreshUI()
    End Sub



    Private Sub UcLevelArray1_OnMoved(sender As Object, e As EventArgs) Handles UcLevelArray1.OnMoved, UcLevelNonArray1.OnMoved
        ucJoyStick1.RefreshPosition()
    End Sub


    Private Sub UcLevelArray1_Saved(sender As Object, e As EventArgs) Handles UcLevelArray1.ValueChanged, UcLevelNonArray1.ValueChanged

        RefreshUI()

    End Sub

    Private Sub btnArray_Click(sender As Object, e As EventArgs) Handles btnArray.Click
        'Sue20170627
        gSyslog.Save("[frmRecipe02]" & vbTab & "[btnArray]" & vbTab & "Click")
        UcLevelArray1.Visible = True
        UcLevelNonArray1.Visible = False
        RecipeEdit.Node(StageNo)(NodeID).Array(cmbLevel.SelectedIndex).LevelType = eLevelType.Array
        btnArray.BackColor = Color.Yellow
        btnNonArray.BackColor = SystemColors.Control
    End Sub

    Private Sub btnNonArray_Click(sender As Object, e As EventArgs) Handles btnNonArray.Click
        'Sue20170627
        gSyslog.Save("[frmRecipe02]" & vbTab & "[btnNonArray]" & vbTab & "Click")
        UcLevelArray1.Visible = False
        UcLevelNonArray1.Visible = True
        RecipeEdit.Node(StageNo)(NodeID).Array(cmbLevel.SelectedIndex).LevelType = eLevelType.NoneArray

        If RecipeEdit.Node(StageNo)(NodeID).Array(cmbLevel.SelectedIndex).NonArray.Count = 0 Then
            Dim DefaultArray As NonArray
            DefaultArray.RelPosX = 0
            DefaultArray.RelPosY = 0
            RecipeEdit.Node(StageNo)(NodeID).Array(cmbLevel.SelectedIndex).NonArray.Add(DefaultArray)
            UcLevelNonArray1.UpdateNonArray(sender, e)
        End If

        btnArray.BackColor = SystemColors.Control
        btnNonArray.BackColor = Color.Yellow
    End Sub
    '20170602按鍵保護
    ''' <summary>
    ''' 移動時 btn保護
    ''' </summary>
    ''' <param name="disable"></param>
    ''' <remarks></remarks>
    Public Sub Btn_Control(ByVal disable As Boolean) '
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Me)) Then
            Me.BeginInvoke(Sub()

                               UcLevelNonArray1.Enabled = disable
                               UcLevelArray1.Enabled = disable
                               ucJoyStick1.Enabled = disable
                               UcLightControl1.Enabled = disable

                               btnOK.Enabled = disable
                               btnCancel.Enabled = disable

                           End Sub)
        End If

    End Sub

    Private Sub btnMultiArryMove_Click(sender As Object, e As EventArgs) Handles btnMultiArryMove.Click
        Dim mfrmGoArray As frmGoArrayPosition = New frmGoArrayPosition
        With mfrmGoArray
            ._ArrayLevelCount = cmbLevel.Items.Count - 1
            ._Node = NodeID
            ._StageNo = StageNo
            .sys = sys
            .RecipeEdit = Me.RecipeEdit
            .mMultiArrayAdapter = mMultiArrayAdapter
            .ShowDialog()
            .Dispose()
        End With
    End Sub

    Private Sub btnWaferMapFilter_Click(sender As Object, e As EventArgs) Handles btnWaferMapFilter.Click
        Dim mfrmWaferMap As New frmSetWaferFilter
        mfrmWaferMap.sys = sys
        mfrmWaferMap.RecipeEdit = Me.RecipeEdit
        mfrmWaferMap.NodeID = NodeID
        mfrmWaferMap.NodeStartingX = nmcStartLocX.Value
        mfrmWaferMap.NodeStartingY = nmcStartLocY.Value
        mfrmWaferMap.ShowDialog()
        cmbLevel_SelectedIndexChanged(sender, e)
        RefreshUI()
        'ShowArray() '更新Align Die Index

    End Sub

   
End Class
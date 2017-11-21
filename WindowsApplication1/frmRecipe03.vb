Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectLaserInterferometer
Imports ProjectAOI

Public Class frmRecipe03
    ''' <summary>外部傳入參數</summary>
    ''' <remarks></remarks>
    Public sys As sSysParam
    ''' <summary>節點ID</summary>
    ''' <remarks></remarks>
    Public NodeID As String
    Dim mMultiArrayAdapter As CMultiArrayAdapter
    Public RecipeEdit As ProjectRecipe.CRecipe
    ''' <summary>
    ''' Form_Load時備份,Cancel時還原
    ''' </summary>
    ''' <remarks></remarks>
    Dim backup As CRecipeNode
    ''' <summary>
    ''' 非陣列清單
    ''' </summary>
    ''' <remarks></remarks>
    Dim LaserList_1 As New List(Of LaserStructure) 'Conveyor1
    Dim LaserList_2 As New List(Of LaserStructure) 'Conveyor2


    ''' <summary>[紀錄目前Tilt角度]</summary>
    ''' <remarks></remarks>
    Dim mTiltCommandValue As Decimal = -1
    Dim substarte As MeasurementSubstrate
    Dim conveyorNo As eConveyor


    ''' <summary>介面更新</summary>
    ''' <remarks></remarks>
    Public Sub RefreshUI()
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
            btnAutoZero.Enabled = False
            btnGoCCDPos.Enabled = False
            'btnGoLaserPos.Enabled = False
            btnSetPos.Enabled = False
            btnAutoZero_2.Enabled = False
            btnGoCCDPos_2.Enabled = False
            'btnGoLaserPos.Enabled = False
            btnSetPos_2.Enabled = False
            btnSetZPos_2.Enabled = False
            btnSetZPos.Enabled = False
            ucJoyStick1.Enabled = False
        Else
            btnAutoZero.Enabled = True
            btnGoCCDPos.Enabled = True
            'btnGoLaserPos.Enabled = True
            btnSetPos.Enabled = True
            btnAutoZero_2.Enabled = True
            btnGoCCDPos_2.Enabled = True
            'btnGoLaserPos.Enabled = True
            btnSetPos_2.Enabled = True
            btnSetZPos_2.Enabled = True
            btnSetZPos.Enabled = True
            ucJoyStick1.Enabled = True
        End If



        '[Note]:使用流道的配置
        Select Case gSSystemParameter.ConveyorModel
            Case eConveyorModel.eConveyorNo1
                TabConveyorControl_CCD_Laser.Controls.Remove(TabPage2)
                TabConveyorControl_CCD_Laser_NonArray.Controls.Remove(TabPage4)
                'TabConveyorControl_CCD_Laser.TabPages(1).Parent = Nothing
            Case eConveyorModel.eConveyorNo2
                TabConveyorControl_CCD_Laser.Controls.Remove(TabPage1)
                TabConveyorControl_CCD_Laser_NonArray.Controls.Remove(TabPage3)
                'TabConveyorControl_CCD_Laser.TabPages(0).Parent = Nothing
            Case eConveyorModel.eConveyorNo1No2
                '[Note]介面不隱藏

        End Select

        'set Z POS 保護
        If chkLaserReaderEnable.Checked Then
            btnSetZPos.Visible = False
            btnSetPos_2.Visible = False
        Else
            btnSetZPos.Visible = True
            btnSetPos_2.Visible = True
        End If

        'NonArray 功能開啟
        If RecipeEdit.LaserRunMode = eLaserRunModel.NonArray Then
            TabConveyorControl_CCD_Laser_NonArray.Visible = True
        Else
            TabConveyorControl_CCD_Laser_NonArray.Visible = False
        End If

        If LaserList_1.Count > 1 Then
            btnDelete.Enabled = True
        Else
            btnDelete.Enabled = False
        End If

        If LaserList_2.Count > 1 Then
            btnDelete_2.Enabled = True
        Else
            btnDelete_2.Enabled = False
        End If

    End Sub

    ''' <summary>設定測高參考位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSetPos_Click(sender As Object, e As EventArgs) Handles btnSetPos.Click, btnSetPos_2.Click
        gSyslog.Save("[frmRecipe03]" & vbTab & "[btnSetPos]" & vbTab & "Click")
        btnSetPos.Enabled = False
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish And gSYS(eSys.MachineB).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetPos.Enabled = True
                    Exit Sub
                End If

            Case Else
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetPos.Enabled = True
                    Exit Sub
                End If

        End Select

        'Toby Add_20170513_Start
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
            btnSetPos.Enabled = True
            Exit Sub
        End If
        'Toby Add_20170513_End

        Select Case conveyorNo
            Case eConveyor.ConveyorNo1
                txtPosX.Text = gCMotion.GetPositionValue(sys.AxisX) - gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve)
                txtPosY.Text = gCMotion.GetPositionValue(sys.AxisY) - gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve)
                txtPosZ.Text = gCMotion.GetPositionValue(sys.AxisZ) - gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.SelectValve)
                txtCCDPosX.Text = gCMotion.GetPositionValue(sys.AxisX)
                txtCCDPosY.Text = gCMotion.GetPositionValue(sys.AxisY)
                txtCCDPosZ.Text = gCMotion.GetPositionValue(sys.AxisZ)
                txtDispenseBasicZ.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, CDec(Val(cboB.SelectedItem)))

                '系統為雙閥才顯示valve2 點膠位置
                If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                    txtDispenseBasicZvalve2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(enmValve.No2, CDec(Val(cboB.SelectedItem)))
                End If

            Case eConveyor.ConveyorNo2
                txtPosX_2.Text = gCMotion.GetPositionValue(sys.AxisX) - gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve)
                txtPosY_2.Text = gCMotion.GetPositionValue(sys.AxisY) - gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve)
                txtPosZ_2.Text = gCMotion.GetPositionValue(sys.AxisZ) - gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.SelectValve)
                txtCCDPosX_2.Text = gCMotion.GetPositionValue(sys.AxisX)
                txtCCDPosY_2.Text = gCMotion.GetPositionValue(sys.AxisY)
                txtCCDPosZ_2.Text = gCMotion.GetPositionValue(sys.AxisZ)
                txtDispenseBasicZ_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, CDec(Val(cboB.SelectedItem)))

                '系統為雙閥才顯示valve2 點膠位置
                If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                    txtDispenseBasicZValve2_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(enmValve.No2, CDec(Val(cboB.SelectedItem)))
                End If

        End Select

        CheckGoCCDPos()
        btnSetPos.Enabled = True

    End Sub


    Sub CheckGoCCDPos()
        If Val(txtCCDPosX.Text) < gCMotion.AxisParameter(sys.AxisX).Limit.NegativeLimit Or Val(txtCCDPosX.Text) > gCMotion.AxisParameter(sys.AxisX).Limit.PosivtiveLimit Or Val(txtCCDPosY.Text) < gCMotion.AxisParameter(sys.AxisY).Limit.NegativeLimit Or Val(txtCCDPosY.Text) > gCMotion.AxisParameter(sys.AxisY).Limit.PosivtiveLimit Or Val(txtCCDPosZ.Text) < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Or Val(txtCCDPosZ.Text) > gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit Then
            btnGoCCDPos.Enabled = False
        Else
            btnGoCCDPos.Enabled = True
        End If
    End Sub

    Private Sub frmRecipe03_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170124 Ticket:100031 , Memory Freed [S]
        UcDisplay1.EndLive()
        UcDisplay1.ManualDispose()
        UcLightControl1.ManualDispose()
        ucJoyStick1.ManualDispose()
        Me.Dispose(True)
        'Eason 20170124 Ticket:100031 , Memory Freed [E]
    End Sub

    Private Sub frmRecipe03_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height

        Dim TempData As LaserStructure

        '[Note]:使用該組Stage的第一組閥
        sys.SelectValve = eValveWorkMode.Valve1

        ucJoyStick1.AxisX = sys.AxisX
        ucJoyStick1.AxisY = sys.AxisY
        ucJoyStick1.AxisZ = sys.AxisZ
        ucJoyStick1.AXisA = sys.AxisA
        ucJoyStick1.AXisB = sys.AxisB
        ucJoyStick1.AXisC = sys.AxisC
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

        mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(sys.StageNo)(NodeID).Array)
        '[Note]備份
        backup = RecipeEdit.Node(sys.StageNo)(NodeID).Clone
        UpdateComboxB()
        Me.Text = "Node(" & NodeID & ") Laser Reader. (" & RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexX & "," & RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexY & ")"
        If cboB.Items.Count <> 0 Then
            cboB.SelectedIndex = 0
        End If

        '只有單閥，不顯示Valve2 高度
        If gSSystemParameter.StageUseValveCount = eMechanismModule.OneValveOneStage Then
            Label24.Visible = False
            txtDispenseBasicZvalve2.Visible = False
            Label25.Visible = False
            Label26.Visible = False
            txtDispenseBasicZValve2_2.Visible = False
            Label27.Visible = False
        End If

        If sys.StageNo >= 0 Then
            If sys.StageNo < gSSystemParameter.StageCount Then
                If RecipeEdit.Node(sys.StageNo).ContainsKey(NodeID) Then
                    Select Case RecipeEdit.LaserRunMode
                        Case eLaserRunModel.Array
                            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData.Count > 0 Then
                                txtPosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).TeachPosX
                                txtPosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).TeachPosY
                                txtPosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).TeachPosZ
                                txtCCDPosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).TeachPosX + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve)
                                txtCCDPosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).TeachPosY + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve)
                                txtCCDPosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).TeachPosZ + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.SelectValve)
                                txtDispenseBasicZ.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, cboB.SelectedItem)
                            End If

                            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData.Count > 0 Then
                                txtPosX_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).TeachPosX
                                txtPosY_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).TeachPosY
                                txtPosZ_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).TeachPosZ
                                txtCCDPosX_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).TeachPosX + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve)
                                txtCCDPosY_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).TeachPosY + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve)
                                txtCCDPosZ_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).TeachPosZ + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.SelectValve)
                                txtDispenseBasicZ_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, cboB.SelectedItem)
                            End If
                        Case eLaserRunModel.NonArray
                            'lstNonArray.SelectedIndex = 0
                            lstNonArray.Items.Clear()
                            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData.Count > 0 Then
                                'read LaserData
                                For mi = 0 To RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData.Count - 1
                                    TempData = New LaserStructure
                                    TempData.LaserPositionX = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(mi).LaserPositionX
                                    TempData.LaserPositionY = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(mi).LaserPositionY
                                    TempData.LaserPositionZ = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(mi).LaserPositionZ
                                    TempData.LaserData1 = mi
                                    LaserList_1.Add(TempData)
                                    lstNonArray.Items.Add((mi).ToString("000"))
                                    'lstNonArray.SelectedIndex = LaserList_1.Count - 1
                                Next
                                lstNonArray.SelectedIndex = 0
                                txtPosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(lstNonArray.SelectedIndex).LaserPositionX
                                txtPosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(lstNonArray.SelectedIndex).LaserPositionY
                                txtPosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(lstNonArray.SelectedIndex).LaserPositionZ
                                txtCCDPosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(lstNonArray.SelectedIndex).LaserPositionX + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve)
                                txtCCDPosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(lstNonArray.SelectedIndex).LaserPositionY + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve)
                                txtCCDPosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(lstNonArray.SelectedIndex).LaserPositionZ + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.SelectValve)
                                txtDispenseBasicZ.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, cboB.SelectedItem)
                            End If
                            lstNonArray_2.Items.Clear()
                            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData.Count > 0 Then
                                'read LaserData
                                For mi = 0 To RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData.Count - 1
                                    TempData = New LaserStructure
                                    TempData.LaserPositionX = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(mi).LaserPositionX
                                    TempData.LaserPositionY = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(mi).LaserPositionY
                                    TempData.LaserPositionZ = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(mi).LaserPositionZ
                                    TempData.LaserData1 = mi
                                    LaserList_2.Add(TempData)
                                    lstNonArray_2.Items.Add((mi).ToString("000"))
                                    'lstNonArray_2.SelectedIndex = LaserList_2.Count - 1
                                Next
                                lstNonArray_2.SelectedIndex = LaserList_2.Count - 1
                                txtPosX_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(lstNonArray_2.SelectedIndex).LaserPositionX
                                txtPosY_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(lstNonArray_2.SelectedIndex).LaserPositionY
                                txtPosZ_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(lstNonArray_2.SelectedIndex).LaserPositionZ
                                txtCCDPosX_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(lstNonArray_2.SelectedIndex).LaserPositionX + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve)
                                txtCCDPosY_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(lstNonArray_2.SelectedIndex).LaserPositionY + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve)
                                txtCCDPosZ_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(lstNonArray_2.SelectedIndex).LaserPositionZ + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.SelectValve)
                                txtDispenseBasicZ_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, cboB.SelectedItem)
                            End If
                        Case eLaserRunModel.None
                            gSyslog.Save(NodeID & gMsgHandler.GetMessage(Error_1000002))
                            MsgBox(NodeID & gMsgHandler.GetMessage(Error_1000002), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    CheckGoCCDPos()
                    chkLaserReaderEnable.Checked = RecipeEdit.Node(sys.StageNo)(NodeID).LaserEnable
                Else
                    '請先選擇Node
                    gSyslog.Save(NodeID & gMsgHandler.GetMessage(Warn_3000025))
                    MsgBox(NodeID & gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End If
            End If
        End If

        Select Case gAOICollection.GetCCDType(enmCCD.CCD1) 'gSSystemParameter.enmCCDType
            Case enmCCDType.CognexVPRO
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
            Case enmCCDType.OmronFZS2MUDP
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
            Case Else
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
        End Select

        '--- 2016.06.23 + 開燈,曝光,觸發後才會改 ---
        Dim mSceneName As String = "CALIB" & (sys.CCDNo + 1).ToString '預設CALIB1校正場景
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(sys.ConveyorNo).AlignmentData.Count > 0 Then
            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene <> "" Then '如果定位點已設定可使用,則採用第一定位點
                If gAOICollection.SceneDictionary.ContainsKey(RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene) Then
                    mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignScene
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
        System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照
        System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        '--- 2016.06.23 + 開燈,曝光,觸發後才會改 ---

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

        RefreshUI()
    End Sub

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

    'Private Sub btnBack_Click(sender As Object, e As EventArgs)
    '    If RecipeEdit.Editable Then
    '        Dim tempLaser As LaserStructure = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0)
    '        Dim tempLaser1 As LaserStructure = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0)
    '        tempLaser.LaserPositionX = Val(txtPosX.Text)
    '        tempLaser.LaserPositionY = Val(txtPosY.Text)
    '        tempLaser.LaserPositionZ = Val(txtPosZ.Text)
    '        RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0) = tempLaser


    '        tempLaser1.LaserPositionX = Val(txtPosX_2.Text)
    '        tempLaser1.LaserPositionY = Val(txtPosY_2.Text)
    '        tempLaser1.LaserPositionZ = Val(txtPosZ_2.Text)
    '        RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0) = tempLaser1

    '        RecipeEdit.SaveNode(RecipeEdit.strFileName)
    '    End If
    '    Me.Close()
    'End Sub

    Private Sub chkLaserReaderEnable_CheckedChanged(sender As Object, e As EventArgs) Handles chkLaserReaderEnable.CheckedChanged
        If RecipeEdit.Editable Then
            RecipeEdit.Node(sys.StageNo)(NodeID).LaserEnable = chkLaserReaderEnable.Checked
            RefreshUI()
        End If
    End Sub

    Private Sub btnGoCCDPos_Click(sender As Object, e As EventArgs) Handles btnGoCCDPos.Click, btnGoCCDPos_2.Click
        gSyslog.Save("[frmRecipe03]" & vbTab & "[btnGoCCDPos]" & vbTab & "Click")
        '20170602按鍵保護
        Btn_Control(False)

        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            BtnHomeFirstBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            Exit Sub
        End If
        If RecipeEdit Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
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

        Select Case conveyorNo
            Case eConveyor.ConveyorNo1
                TargetPos(0) = Val(txtCCDPosX.Text) ' + mMultiArrayAdapter.GetMemoryPosX(Recipe.Node(sys.StageNo)(NodeID).TeachIndexX, Recipe.Node(sys.StageNo)(NodeID).TeachIndexY)
                TargetPos(1) = Val(txtCCDPosY.Text) ' + mMultiArrayAdapter.GetMemoryPosY(Recipe.Node(sys.StageNo)(NodeID).TeachIndexX, Recipe.Node(sys.StageNo)(NodeID).TeachIndexY)
                TargetPos(2) = Val(txtCCDPosZ.Text)
                TargetPos(3) = 0
                TargetPos(4) = 0
            Case eConveyor.ConveyorNo2
                TargetPos(0) = Val(txtCCDPosX_2.Text) ' + mMultiArrayAdapter.GetMemoryPosX(Recipe.Node(sys.StageNo)(NodeID).TeachIndexX, Recipe.Node(sys.StageNo)(NodeID).TeachIndexY)
                TargetPos(1) = Val(txtCCDPosY_2.Text) ' + mMultiArrayAdapter.GetMemoryPosY(Recipe.Node(sys.StageNo)(NodeID).TeachIndexX, Recipe.Node(sys.StageNo)(NodeID).TeachIndexY)
                TargetPos(2) = Val(txtCCDPosZ_2.Text)
                TargetPos(3) = 0
                TargetPos(4) = 0
        End Select
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        ucJoyStick1.RefreshPosition()
        '20170602按鍵保護
        Btn_Control(True)
    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmRecipe03]" & vbTab & "[btnCancel]" & vbTab & "Click")
        RecipeEdit.Node(sys.StageNo)(NodeID) = backup.Clone '還原
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmRecipe03]" & vbTab & "[btnOK]" & vbTab & "Click")
        Dim StageNo As Integer = CInt(NodeID.Split(",")(1))
        '--- 可編輯才儲存 ---
        If RecipeEdit.Editable Then
            Dim tempLaser As LaserStructure
            Dim tempLaser1 As LaserStructure



            Select Case RecipeEdit.LaserRunMode
                Case eLaserRunModel.Array
                    If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData.Count > 0 Then
                        tempLaser = New LaserStructure
                        tempLaser.TeachPosX = Val(txtPosX.Text)
                        tempLaser.TeachPosY = Val(txtPosY.Text)
                        tempLaser.TeachPosZ = Val(txtPosZ.Text)
                        RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0) = tempLaser
                    End If
                    If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData.Count > 0 Then
                        tempLaser1 = New LaserStructure
                        tempLaser1.TeachPosX = Val(txtPosX.Text)
                        tempLaser1.TeachPosY = Val(txtPosY.Text)
                        tempLaser1.TeachPosZ = Val(txtPosZ.Text)
                        RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0) = tempLaser1
                    End If
                    RecipeEdit.UpdateOriginDataConveyorHeight(sys.StageNo, NodeID, 0, 0, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexX, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexY, mMultiArrayAdapter)
                    RecipeEdit.UpdateOriginDataConveyorHeight(sys.StageNo, NodeID, 1, 0, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexX, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexY, mMultiArrayAdapter)

                Case eLaserRunModel.NonArray
                    If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData.Count > 0 Then
                        RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData.Clear()
                        For mi = 0 To LaserList_1.Count - 1
                            tempLaser = New LaserStructure
                            tempLaser.LaserPositionX = LaserList_1(mi).LaserPositionX
                            tempLaser.LaserPositionY = LaserList_1(mi).LaserPositionY
                            tempLaser.LaserPositionZ = LaserList_1(mi).LaserPositionZ
                            tempLaser.LaserData1 = mi
                            RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData.Add(tempLaser)
                        Next
                    End If
                    If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData.Count > 0 Then
                        RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData.Clear()
                        For mi = 0 To LaserList_2.Count - 1
                            tempLaser1 = New LaserStructure
                            tempLaser1.LaserPositionX = LaserList_2(mi).LaserPositionX
                            tempLaser1.LaserPositionY = LaserList_2(mi).LaserPositionY
                            tempLaser1.LaserPositionZ = LaserList_2(mi).LaserPositionZ
                            tempLaser1.LaserData1 = mi
                            RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData.Add(tempLaser1)
                        Next
                    End If
            End Select
            backup = RecipeEdit.Node(sys.StageNo)(NodeID).Clone
            'RecipeEdit.SaveNode(RecipeEdit.strFileName)
            'Sue20170627
            '存檔成功 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Save OK")
        End If
        '--- 可編輯才儲存 ---
        'Me.Close()
    End Sub


    Private Sub btnAutoZero_Click(sender As Object, e As EventArgs) Handles btnAutoZero.Click, btnAutoZero_2.Click

        gSyslog.Save("[frmRecipe03]" & vbTab & "[btnGoLaserPos]" & vbTab & "Click")

        '20170602按鍵保護
        Btn_Control(False)
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            BtnHomeFirstBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)

            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If
        If RecipeEdit Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)

            '20170602按鍵保護
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



        If mMultiArrayAdapter.GetMemoryCountX < 1 Or mMultiArrayAdapter.GetMemoryCountY < 1 Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000045), "Warn_3000045", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000045), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Btn_Control(True)
            Exit Sub
        End If


        Select Case conveyorNo
            Case eConveyor.ConveyorNo1
                TargetPos(0) = Val(txtPosX.Text) '+ mMultiArrayAdapter.GetMemoryPosX(Recipe.Node(sys.StageNo)(NodeID).TeachIndexX, Recipe.Node(sys.StageNo)(NodeID).TeachIndexY)
                TargetPos(1) = Val(txtPosY.Text) '+ mMultiArrayAdapter.GetMemoryPosY(Recipe.Node(sys.StageNo)(NodeID).TeachIndexX, Recipe.Node(sys.StageNo)(NodeID).TeachIndexY)
                TargetPos(2) = Val(txtPosZ.Text)
                TargetPos(3) = 0
                TargetPos(4) = 0
            Case eConveyor.ConveyorNo2
                TargetPos(0) = Val(txtPosX_2.Text) '+ mMultiArrayAdapter.GetMemoryPosX(Recipe.Node(sys.StageNo)(NodeID).TeachIndexX, Recipe.Node(sys.StageNo)(NodeID).TeachIndexY)
                TargetPos(1) = Val(txtPosY_2.Text) '+ mMultiArrayAdapter.GetMemoryPosY(Recipe.Node(sys.StageNo)(NodeID).TeachIndexX, Recipe.Node(sys.StageNo)(NodeID).TeachIndexY)
                TargetPos(2) = Val(txtPosZ_2.Text)
                TargetPos(3) = 0
                TargetPos(4) = 0
        End Select


        'TargetPos(0) = Val(txtPosX.Text) + mMultiArrayAdapter.GetMemoryPosX(Recipe.Node(sys.StageNo)(NodeID).TeachIndexX, Recipe.Node(sys.StageNo)(NodeID).TeachIndexY)
        'TargetPos(1) = Val(txtPosY.Text) + mMultiArrayAdapter.GetMemoryPosY(Recipe.Node(sys.StageNo)(NodeID).TeachIndexX, Recipe.Node(sys.StageNo)(NodeID).TeachIndexY)
        'TargetPos(2) = Val(txtPosZ.Text)
        'TargetPos(3) = 0
        'TargetPos(4) = 0
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)


        '[Note]:Auto Reader Focus
        Dim dCurrentZ, dLaserZeroZ As Decimal
        Dim iRetryCount As Integer = 30
        Dim iSearchStep As Integer = 10
        Dim TimeOutStopwatch As New Stopwatch
        Dim dSearchZ As Double

        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            BtnHomeFirstBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            '請先復歸!!
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005))
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If

        '20170623 測高保護機制
        If gSSystemParameter.MeasureType = enmMeasureType.Contact Then
            If RecipeEdit.LaserFixMode <> eHeightModel.Contact Then
                MsgBox("請確認測高模式是否正確", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Btn_Control(True)
                Exit Sub
            End If
        ElseIf gSSystemParameter.MeasureType = enmMeasureType.Laser Then
            If RecipeEdit.LaserFixMode <> eHeightModel.Laser_NonOnFly Then
                MsgBox("請確認測高模式是否正確", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Btn_Control(True)
                Exit Sub
            End If
        End If

        If Object.Equals(CType(sender, Button), btnAutoZero) Then
            btnAutoZero.BackColor = System.Drawing.Color.Yellow
        Else
            btnAutoZero_2.BackColor = System.Drawing.Color.Yellow
        End If

        Dim HeightValue As String = ""

        'Z軸速度設定
        gCMotion.SetVelLow(sys.AxisZ, 0)
        gCMotion.SetVelHigh(sys.AxisZ, 50)


        txtDispenseBasicZ.BackColor = System.Drawing.SystemColors.Control



        If Object.Equals(CType(sender, Button), btnAutoZero) Then
            txtDispenseBasicZ.BackColor = System.Drawing.SystemColors.Control
        Else
            txtDispenseBasicZ_2.BackColor = System.Drawing.SystemColors.Control
        End If

        Select Case RecipeEdit.LaserFixMode
            Case eHeightModel.Contact  '接觸式測高

                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                    If Object.Equals(CType(sender, Button), btnAutoZero) Then
                        btnAutoZero.BackColor = Color.Red
                    Else
                        btnAutoZero_2.BackColor = Color.Red
                    End If

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

                    '20170602按鍵保護
                    Btn_Control(True)
                    Exit Sub
                End If

                dCurrentZ = Val(gCMotion.GetPositionValue(sys.AxisZ))


                '20170623 測高保護機制
                If HeightValue < 5 Then
                    MsgBox("請將接觸式測高校正", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Btn_Control(True)
                    Exit Sub
                End If




                ucJoyStick1.SetSpeedType(SpeedType.Fast)

                '汽缸動作
                gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, True)
                Call gDOCollection.RefreshDO()
                System.Threading.Thread.CurrentThread.Join(1000)
                '汽缸動作_end

                If Val(HeightValue) < -10 Or Val(HeightValue) > 10 Then

                    dSearchZ = dCurrentZ
                    While (dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)
                        SafeLaserZMove(dSearchZ)
                        dSearchZ = Val(gCMotion.GetPositionValue(ucJoyStick1.AxisZ))

                        If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                            If Object.Equals(CType(sender, Button), btnAutoZero) Then
                                btnAutoZero.BackColor = Color.Red
                            Else
                                btnAutoZero_2.BackColor = Color.Red
                            End If
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

                            '20170602按鍵保護
                            Btn_Control(True)
                            Exit Sub
                        End If
                        dSearchZ += iSearchStep
                        iRetryCount -= 1
                    End While
                    dSearchZ = dCurrentZ
                    While (dSearchZ > gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)

                        SafeLaserZMove(dSearchZ)
                        dSearchZ = Val(gCMotion.GetPositionValue(ucJoyStick1.AxisZ))
                        If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                            If Object.Equals(CType(sender, Button), btnAutoZero) Then
                                btnAutoZero.BackColor = Color.Red
                            Else
                                btnAutoZero_2.BackColor = Color.Red
                            End If
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

                            '20170602按鍵保護
                            Btn_Control(True)
                            Exit Sub
                        End If
                        dSearchZ -= iSearchStep
                        iRetryCount -= 1
                    End While
                End If

                ucJoyStick1.SetSpeedType(SpeedType.Slow)
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
                    'MsgBox("Auto Zero function failed (retry count > 30) !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    If Object.Equals(CType(sender, Button), btnAutoZero) Then
                        btnAutoZero.BackColor = System.Drawing.Color.Red
                    Else

                        btnAutoZero_2.BackColor = System.Drawing.Color.Red
                    End If
                    '20170602按鍵保護
                    Btn_Control(True)
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
                    If Object.Equals(CType(sender, Button), btnAutoZero) Then
                        btnAutoZero.BackColor = System.Drawing.Color.Red
                    Else

                        btnAutoZero_2.BackColor = System.Drawing.Color.Red
                    End If

                    '20170602按鍵保護
                    Btn_Control(True)
                    Exit Sub
                End If

                For i As Integer = 0 To 10
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                        If Object.Equals(CType(sender, Button), btnAutoZero) Then
                            btnAutoZero.BackColor = Color.Red
                        Else
                            btnAutoZero_2.BackColor = Color.Red
                        End If

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

                        '20170602按鍵保護
                        Btn_Control(True)
                        '汽缸動作(收回)
                        gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
                        Call gDOCollection.RefreshDO()
                        '汽缸動作_end

                        Exit Sub
                    End If


                    dCurrentZ = Val(gCMotion.GetPositionValue(ucJoyStick1.AxisZ))
                    dLaserZeroZ = dCurrentZ + Val(HeightValue)
                    SafeLaserZMove(dLaserZeroZ)
                Next

                If Object.Equals(CType(sender, Button), btnAutoZero) Then
                    txtPosZ.Text = dLaserZeroZ
                    btnAutoZero.BackColor = System.Drawing.SystemColors.Control
                    btnAutoZero.UseVisualStyleBackColor = True
                    txtDispenseBasicZ.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(CInt(sys.ValveNo(0)), CDec(Val(cboB.SelectedItem)))
                    '0808增加valve2高度顯示
                    If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                        txtDispenseBasicZvalve2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(CInt(sys.ValveNo(1)), CDec(Val(cboB.SelectedItem)))
                    End If
                    'txtDispenseBasicZvalve2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(CInt(sys.ValveNo(1)), CDec(Val(cboB.SelectedItem)))
                Else

                    txtPosZ_2.Text = dLaserZeroZ
                    btnAutoZero_2.BackColor = System.Drawing.SystemColors.Control
                    btnAutoZero_2.UseVisualStyleBackColor = True
                    txtDispenseBasicZ_2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(CInt(sys.ValveNo(0)), CDec(Val(cboB.SelectedItem)))
                    '0808增加valve2高度顯示
                    If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                        txtDispenseBasicZValve2_2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(CInt(sys.ValveNo(1)), CDec(Val(cboB.SelectedItem)))
                    End If
                    'txtDispenseBasicZValve2_2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(CInt(sys.ValveNo(1)), CDec(Val(cboB.SelectedItem)))
                End If



                '汽缸動作(收回)
                gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
                Call gDOCollection.RefreshDO()
                '汽缸動作_end

            Case Else  'Laser測高

                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                    If Object.Equals(CType(sender, Button), btnAutoZero) Then
                        btnAutoZero.BackColor = Color.Red
                    Else
                        btnAutoZero_2.BackColor = Color.Red
                    End If
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

                    '20170602按鍵保護
                    Btn_Control(True)
                    Exit Sub
                End If

                dCurrentZ = Val(gCMotion.GetPositionValue(sys.AxisZ))
                ucJoyStick1.SetSpeedType(SpeedType.Fast)
                If Val(HeightValue) < -10 Or Val(HeightValue) > 10 Then

                    dSearchZ = dCurrentZ
                    While (dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)
                        SafeLaserZMove(dSearchZ)
                        dSearchZ = Val(gCMotion.GetPositionValue(ucJoyStick1.AxisZ))

                        If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                            If Object.Equals(CType(sender, Button), btnAutoZero) Then
                                btnAutoZero.BackColor = Color.Red
                            Else
                                btnAutoZero_2.BackColor = Color.Red
                            End If
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

                            '20170602按鍵保護
                            Btn_Control(True)
                            Exit Sub
                        End If
                        dSearchZ += iSearchStep
                        iRetryCount -= 1
                    End While
                    dSearchZ = dCurrentZ
                    While (dSearchZ > gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)

                        SafeLaserZMove(dSearchZ)
                        dSearchZ = Val(gCMotion.GetPositionValue(ucJoyStick1.AxisZ))
                        If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                            If Object.Equals(CType(sender, Button), btnAutoZero) Then
                                btnAutoZero.BackColor = Color.Red
                            Else
                                btnAutoZero_2.BackColor = Color.Red
                            End If
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

                            '20170602按鍵保護
                            Btn_Control(True)
                            Exit Sub
                        End If
                        dSearchZ -= iSearchStep
                        iRetryCount -= 1
                    End While
                End If

                ucJoyStick1.SetSpeedType(SpeedType.Slow)
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
                    'MsgBox("Auto Zero function failed (retry count > 30) !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    If Object.Equals(CType(sender, Button), btnAutoZero) Then
                        btnAutoZero.BackColor = System.Drawing.Color.Red
                    Else
                        btnAutoZero_2.BackColor = System.Drawing.Color.Red
                    End If
                    '20170602按鍵保護
                    Btn_Control(True)
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
                    If Object.Equals(CType(sender, Button), btnAutoZero) Then
                        btnAutoZero.BackColor = System.Drawing.Color.Red
                    Else
                        btnAutoZero_2.BackColor = System.Drawing.Color.Red
                    End If
                    '20170602按鍵保護
                    Btn_Control(True)
                    Exit Sub
                End If

                For i As Integer = 0 To 5
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.LaserNo, HeightValue, True) = False Then
                        If Object.Equals(CType(sender, Button), btnAutoZero) Then
                            btnAutoZero.BackColor = Color.Red
                        Else
                            btnAutoZero_2.BackColor = Color.Red
                        End If
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

                        '20170602按鍵保護
                        Btn_Control(True)
                        Exit Sub
                    End If
                    dCurrentZ = Val(gCMotion.GetPositionValue(ucJoyStick1.AxisZ))
                    dLaserZeroZ = dCurrentZ + Val(HeightValue)
                    SafeLaserZMove(dLaserZeroZ)
                Next
                If Object.Equals(CType(sender, Button), btnAutoZero) Then
                    txtPosZ.Text = dLaserZeroZ
                    btnAutoZero.BackColor = System.Drawing.SystemColors.Control
                    btnAutoZero.UseVisualStyleBackColor = True
                    txtDispenseBasicZ.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, CDec(Val(cboB.SelectedItem)))
                    '0808增加valve2高度顯示
                    If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                        txtDispenseBasicZvalve2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(CInt(sys.ValveNo(1)), CDec(Val(cboB.SelectedItem)))
                    End If


                    'txtDispenseBasicZvalve2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(CInt(sys.ValveNo(1)), CDec(Val(cboB.SelectedItem)))
                Else

                    txtPosZ_2.Text = dLaserZeroZ
                    btnAutoZero_2.BackColor = System.Drawing.SystemColors.Control
                    btnAutoZero_2.UseVisualStyleBackColor = True
                    txtDispenseBasicZ_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, CDec(Val(cboB.SelectedItem)))
                    '0808增加valve2高度顯示
                    If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                        txtDispenseBasicZValve2_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(CInt(sys.ValveNo(1)), CDec(Val(cboB.SelectedItem)))
                    End If
                    'txtDispenseBasicZValve2_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(CInt(sys.ValveNo(1)), CDec(Val(cboB.SelectedItem)))
                End If

        End Select

        If Object.Equals(CType(sender, Button), btnAutoZero) Then

            If CDbl(txtPosZ.Text) < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Or CDbl(txtDispenseBasicZ.Text) < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Then
                'Z軸超出軟體極限
                txtDispenseBasicZ.BackColor = Color.Red

                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        MsgBox("Z Axis Command is Out of limit", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case enmLanguageType.eSimplifiedChinese
                        MsgBox("Z轴位置超出极限", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case enmLanguageType.eTraditionalChinese
                        MsgBox("Z軸位置超出極限!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select


                gEqpMsg.AddHistoryAlarm("Warn_3000074", "AutoZero", sys.SysNum, gMsgHandler.GetMessage(Warn_3000074), eMessageLevel.Warning)
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000074))
                MsgBox(gMsgHandler.GetMessage(Warn_3000074), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            End If
        Else

            If CDbl(txtPosZ_2.Text) < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Or CDbl(txtDispenseBasicZ_2.Text) < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Then
                'Z軸超出軟體極限

                txtDispenseBasicZ_2.BackColor = Color.Red

                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        MsgBox("Z Axis Command is Out of limit", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case enmLanguageType.eSimplifiedChinese
                        MsgBox("Z轴位置超出极限", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case enmLanguageType.eTraditionalChinese
                        MsgBox("Z軸位置超出極限!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select


                gEqpMsg.AddHistoryAlarm("Warn_3000074", "AutoZero", sys.SysNum, gMsgHandler.GetMessage(Warn_3000074), eMessageLevel.Warning)
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000074))
                MsgBox(gMsgHandler.GetMessage(Warn_3000074), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            End If
        End If



        ucJoyStick1.RefreshPosition()

        '20170602按鍵保護
        Btn_Control(True)
    End Sub

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
            Application.DoEvents()
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

    Private Sub txtCCDPosX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCCDPosX.KeyPress, txtCCDPosY.KeyPress, txtCCDPosZ.KeyPress, txtPosX.KeyPress, txtPosY.KeyPress, txtPosZ.KeyPress
        Premtek.Base.CKeyPress.CheckDecimal(sender, e)
    End Sub
    Public Sub UpdateComboxB()
        Dim iSelectIndexTemp As Integer = cboB.SelectedIndex
        cboB.Items.Clear()

        For i As Integer = 0 To gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).DicLaserTiltValve(sys.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
            cboB.Items.Add(gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).DicLaserTiltValve(sys.SelectValve).Keys(i))
        Next
        cboB.SelectedIndex = iSelectIndexTemp
    End Sub

    Private Sub cboB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboB.SelectedIndexChanged
        If sys.StageNo >= 0 Then
            If sys.StageNo < gSSystemParameter.StageCount Then
                If RecipeEdit.Node(sys.StageNo).ContainsKey(NodeID) Then
                    If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData.Count > 0 Then
                        txtPosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).LaserPositionX
                        txtPosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).LaserPositionY
                        txtPosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).LaserPositionZ
                        txtCCDPosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).LaserPositionX + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve)
                        txtCCDPosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).LaserPositionY + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve)
                        txtCCDPosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).LaserPositionZ + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.SelectValve)
                        txtDispenseBasicZ.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, cboB.SelectedItem)
                        '0808增加valve2高度顯示
                        If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                            txtDispenseBasicZvalve2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(eValveWorkMode.Valve2, cboB.SelectedItem)
                        End If

                        'txtDispenseBasicZvalve2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(eValveWorkMode.Valve2, cboB.SelectedItem)

                    End If

                    If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData.Count > 0 Then
                        txtPosX_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).LaserPositionX
                        txtPosY_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).LaserPositionY
                        txtPosZ_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).LaserPositionZ
                        txtCCDPosX_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).LaserPositionX + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve)
                        txtCCDPosY_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).LaserPositionY + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve)
                        txtCCDPosZ_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).LaserPositionZ + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.SelectValve)
                        txtDispenseBasicZ_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, cboB.SelectedItem)
                        '0808增加valve2高度顯示
                        If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                            txtDispenseBasicZValve2_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(eValveWorkMode.Valve2, cboB.SelectedItem)
                        End If
                        'txtDispenseBasicZValve2_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(eValveWorkMode.Valve2, cboB.SelectedItem)
                    End If

                    CheckGoCCDPos()
                    chkLaserReaderEnable.Checked = RecipeEdit.Node(sys.StageNo)(NodeID).LaserEnable
                Else
                    '請先選擇Node
                    gSyslog.Save(NodeID & gMsgHandler.GetMessage(Warn_3000025))
                    MsgBox(NodeID & gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MsgBox("Node(" & NodeID & ") Not Found.")
                End If
            End If
        End If
    End Sub
    ''' <summary>依選擇功能,顯示介面配置</summary>
    ''' <remarks></remarks>
    Sub RefreshPos(Optional ByVal conveyorNo As eConveyor = eConveyor.ConveyorNo1)
        If NodeID = "" Then '沒節點
            Exit Sub
        End If
        If sys Is Nothing Then '沒系統
            Exit Sub
        End If
        If sys.StageNo < 0 Then '系統平台錯誤
            Exit Sub
        End If
        If sys.StageNo > gSSystemParameter.StageCount Then '系統平台錯誤
            Exit Sub
        End If
        If Not RecipeEdit.Node(sys.StageNo).ContainsKey(NodeID) Then '節點不存在
            Exit Sub
        End If

        txtPosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).LaserPositionX
        txtPosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).LaserPositionY
        txtPosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).LaserPositionZ
        txtCCDPosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).LaserPositionX + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.ValveNo(0))
        txtCCDPosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).LaserPositionY + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.ValveNo(0))
        txtCCDPosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).LaserPositionZ + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.ValveNo(0))
        txtDispenseBasicZ.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, cboB.SelectedItem)
        '0808增加valve2高度顯示
        If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
            txtDispenseBasicZvalve2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(eValveWorkMode.Valve2, cboB.SelectedItem)
        End If
        'txtDispenseBasicZvalve2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(eValveWorkMode.Valve2, cboB.SelectedItem)

        txtPosX_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).LaserPositionX
        txtPosY_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).LaserPositionY
        txtPosZ_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).LaserPositionZ
        txtCCDPosX_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).LaserPositionX + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.ValveNo(0))
        txtCCDPosY_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).LaserPositionY + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.ValveNo(0))
        txtCCDPosZ_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).LaserPositionZ + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.ValveNo(0))
        txtDispenseBasicZ_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, cboB.SelectedItem)
        '0808增加valve2高度顯示
        If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
            txtDispenseBasicZValve2_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(eValveWorkMode.Valve2, cboB.SelectedItem)
        End If

    End Sub
    Private Sub TabConveyorControl_CCD_Laser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabConveyorControl_CCD_Laser.SelectedIndexChanged
        Select Case gSSystemParameter.ConveyorModel
            Case eConveyorModel.eConveyorNo1
                conveyorNo = eConveyor.ConveyorNo1

            Case eConveyorModel.eConveyorNo2
                conveyorNo = eConveyor.ConveyorNo2

            Case eConveyorModel.eConveyorNo1No2
                If TabConveyorControl_CCD_Laser.SelectedIndex = 1 Then
                    conveyorNo = eConveyor.ConveyorNo2
                Else
                    conveyorNo = eConveyor.ConveyorNo1
                End If
                TabConveyorControl_CCD_Laser.SelectTab(conveyorNo)
                TabConveyorControl_CCD_Laser_NonArray.SelectTab(conveyorNo)
        End Select

    End Sub
    Private Sub TabConveyorControl_CCD_Laser_NonArray_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabConveyorControl_CCD_Laser_NonArray.SelectedIndexChanged

        Select Case gSSystemParameter.ConveyorModel
            Case eConveyorModel.eConveyorNo1
                conveyorNo = eConveyor.ConveyorNo1

            Case eConveyorModel.eConveyorNo2
                conveyorNo = eConveyor.ConveyorNo2

            Case eConveyorModel.eConveyorNo1No2
                If TabConveyorControl_CCD_Laser_NonArray.SelectedIndex = 1 Then
                    conveyorNo = eConveyor.ConveyorNo2
                Else
                    conveyorNo = eConveyor.ConveyorNo1
                End If
                TabConveyorControl_CCD_Laser.SelectTab(conveyorNo)
                TabConveyorControl_CCD_Laser_NonArray.SelectTab(conveyorNo)
        End Select

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        substarte = New MeasurementSubstrate()
        substarte.sys = sys
        substarte.Show()
    End Sub

    Public Sub New()

        ' 此為設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。

    End Sub
    '20170602按鍵保護
    ''' <summary>
    ''' 軸移動時 btn保護
    ''' </summary>
    ''' <param name="disable"></param>
    ''' <remarks></remarks>
    Public Sub Btn_Control(ByVal disable As Boolean) 'Soni / 2017.05.16 確保是介面執行
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Me)) Then
            Me.BeginInvoke(Sub()
                               btnSetPos.Enabled = disable
                               btnGoCCDPos.Enabled = disable
                               btnAutoZero.Enabled = disable
                               Button1.Enabled = disable
                               btnSetPos_2.Enabled = disable
                               btnGoCCDPos_2.Enabled = disable
                               btnAutoZero_2.Enabled = disable
                               btnOK.Enabled = disable
                               btnCancel.Enabled = disable
                               UcLightControl1.Enabled = disable
                               ucJoyStick1.Enabled = disable
                               GroupBox2.Enabled = disable
                           End Sub)
        End If

    End Sub

    Private Sub btnSetZPos_Click(sender As Object, e As EventArgs) Handles btnSetZPos.Click, btnSetZPos_2.Click

        Select Case conveyorNo
            Case eConveyor.ConveyorNo1
                txtDispenseBasicZ.BackColor = System.Drawing.SystemColors.Control
                txtDispenseBasicZ.Text = gCMotion.GetPositionValue(ucJoyStick1.AxisZ)
                txtPosZ.Text = gCMotion.GetPositionValue(ucJoyStick1.AxisZ) + gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, CDec(Val(cboB.SelectedItem))) ''測試用
                '0808增加valve2高度顯示
                If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                    txtDispenseBasicZvalve2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(eValveWorkMode.Valve2, cboB.SelectedItem)
                End If
                'txtDispenseBasicZvalve2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(eValveWorkMode.Valve2, cboB.SelectedItem)
            Case eConveyor.ConveyorNo2
                txtDispenseBasicZ_2.BackColor = System.Drawing.SystemColors.Control
                txtDispenseBasicZ_2.Text = gCMotion.GetPositionValue(ucJoyStick1.AxisZ)
                txtPosZ_2.Text = gCMotion.GetPositionValue(ucJoyStick1.AxisZ) + gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, CDec(Val(cboB.SelectedItem))) ''測試用
                '0808增加valve2高度顯示
                If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                    txtDispenseBasicZValve2_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(eValveWorkMode.Valve2, cboB.SelectedItem)
                End If
                'txtDispenseBasicZValve2_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(eValveWorkMode.Valve2, cboB.SelectedItem)
        End Select

    End Sub


    Private Sub lstNonArray_2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstNonArray_2.SelectedIndexChanged
        Select Case RecipeEdit.LaserRunMode
            Case eLaserRunModel.Array
                If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData.Count > 0 Then
                    txtPosX_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).TeachPosX
                    txtPosY_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).TeachPosY
                    txtPosZ_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).TeachPosZ
                    txtCCDPosX_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).TeachPosX + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve)
                    txtCCDPosY_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).TeachPosY + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve)
                    txtCCDPosZ_2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).LaserData(0).TeachPosZ + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.SelectValve)
                    txtDispenseBasicZ_2.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, cboB.SelectedItem)
                End If
            Case eLaserRunModel.NonArray
                If lstNonArray_2.SelectedIndex >= 0 Then
                    If LaserList_2.Count > lstNonArray_2.SelectedIndex Then
                        'read LaserData
                        txtPosX_2.Text = LaserList_2(lstNonArray_2.SelectedIndex).LaserPositionX
                        txtPosY_2.Text = LaserList_2(lstNonArray_2.SelectedIndex).LaserPositionY
                        txtPosZ_2.Text = LaserList_2(lstNonArray_2.SelectedIndex).LaserPositionZ
                        txtCCDPosX_2.Text = LaserList_2(lstNonArray_2.SelectedIndex).LaserPositionX + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve)
                        txtCCDPosY_2.Text = LaserList_2(lstNonArray_2.SelectedIndex).LaserPositionY + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve)
                        txtCCDPosZ_2.Text = LaserList_2(lstNonArray_2.SelectedIndex).LaserPositionZ + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.SelectValve)
                        txtDispenseBasicZ_2.Text = Val(txtPosZ_2.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, cboB.SelectedItem)
                    End If
                End If
        End Select

    End Sub

    Private Sub lstNonArray_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstNonArray.SelectedIndexChanged

        Select Case RecipeEdit.LaserRunMode
            Case eLaserRunModel.Array
                If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData.Count > 0 Then
                    txtPosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).TeachPosX
                    txtPosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).TeachPosY
                    txtPosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).TeachPosZ
                    txtCCDPosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).TeachPosX + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve)
                    txtCCDPosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).TeachPosY + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve)
                    txtCCDPosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).LaserData(0).TeachPosZ + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.SelectValve)
                    txtDispenseBasicZ.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, cboB.SelectedItem)
                End If
            Case eLaserRunModel.NonArray
                If lstNonArray.SelectedIndex >= 0 Then
                    If LaserList_1.Count > lstNonArray.SelectedIndex Then
                        'read LaserData
                        txtPosX.Text = LaserList_1(lstNonArray.SelectedIndex).LaserPositionX
                        txtPosY.Text = LaserList_1(lstNonArray.SelectedIndex).LaserPositionY
                        txtPosZ.Text = LaserList_1(lstNonArray.SelectedIndex).LaserPositionZ
                        txtCCDPosX.Text = LaserList_1(lstNonArray.SelectedIndex).LaserPositionX + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve)
                        txtCCDPosY.Text = LaserList_1(lstNonArray.SelectedIndex).LaserPositionY + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve)
                        txtCCDPosZ.Text = LaserList_1(lstNonArray.SelectedIndex).LaserPositionZ + gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetZ(sys.SelectValve)
                        txtDispenseBasicZ.Text = Val(txtPosZ.Text) - gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LaserTiltValveOffsetZ(sys.SelectValve, cboB.SelectedItem)
                    End If
                End If
        End Select

    End Sub

    Private Sub btnDelete_2_Click(sender As Object, e As EventArgs) Handles btnDelete_2.Click
        Dim TempList As New List(Of LaserStructure) 'tempList

        '先記錄LaserList to tempList
        TempList.Clear()
        For mi = 0 To LaserList_2.Count - 1
            TempList.Add(LaserList_2(mi))
        Next
        Dim TempData As LaserStructure

        For mi = lstNonArray_2.SelectedIndex To TempList.Count - 1
            TempList.RemoveAt(lstNonArray.SelectedIndex)
        Next

        For mi = lstNonArray.SelectedIndex + 1 To LaserList_2.Count - 1
            TempData = New LaserStructure
            TempData.LaserPositionX = LaserList_2(mi).LaserPositionX
            TempData.LaserPositionY = LaserList_2(mi).LaserPositionY
            TempData.LaserPositionZ = LaserList_2(mi).LaserPositionZ
            TempData.LaserData1 = LaserList_2(mi).LaserData1 - 1
            TempList.Add(TempData)
        Next

        '將 tempList to LaserList
        LaserList_2.Clear()
        For mi = 0 To TempList.Count - 1
            LaserList_2.Add(TempList(mi))
        Next

        lstNonArray_2.Items.Clear()
        For mi = 0 To LaserList_2.Count - 1
            lstNonArray_2.Items.Add((mi).ToString("000"))
        Next

        lstNonArray_2.SelectedIndex = LaserList_2.Count - 1

        If LaserList_2.Count > 1 Then
            btnDelete_2.Enabled = True
        Else
            btnDelete_2.Enabled = False
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim TempList As New List(Of LaserStructure) 'tempList

        '先記錄LaserList to tempList
        TempList.Clear()
        For mi = 0 To LaserList_1.Count - 1
            TempList.Add(LaserList_1(mi))
        Next
        Dim TempData As LaserStructure

        For mi = lstNonArray.SelectedIndex To TempList.Count - 1
            TempList.RemoveAt(lstNonArray.SelectedIndex)
        Next

        For mi = lstNonArray.SelectedIndex + 1 To LaserList_1.Count - 1
            TempData = New LaserStructure
            TempData.LaserPositionX = LaserList_1(mi).LaserPositionX
            TempData.LaserPositionY = LaserList_1(mi).LaserPositionY
            TempData.LaserPositionZ = LaserList_1(mi).LaserPositionZ
            TempData.LaserData1 = LaserList_1(mi).LaserData1 - 1
            TempList.Add(TempData)
        Next

        '將 tempList to LaserList
        LaserList_1.Clear()
        For mi = 0 To TempList.Count - 1
            LaserList_1.Add(TempList(mi))
        Next

        lstNonArray.Items.Clear()
        For mi = 0 To LaserList_1.Count - 1
            lstNonArray.Items.Add((mi).ToString("000"))
        Next

        lstNonArray.SelectedIndex = LaserList_1.Count - 1

        If LaserList_1.Count > 1 Then
            btnDelete.Enabled = True
        Else
            btnDelete.Enabled = False
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        btnAdd.Enabled = False

        Dim AddData As New LaserStructure

        AddData.LaserPositionX = 0
        AddData.LaserPositionY = 0
        AddData.LaserPositionZ = 0
        AddData.LaserData1 = LaserList_1.Count
        LaserList_1.Add(AddData)

        '清空重新顯示
        lstNonArray.Items.Clear()
        For mi = 0 To LaserList_1.Count - 1
            lstNonArray.Items.Add((mi).ToString("000"))
        Next
        lstNonArray.SelectedIndex = LaserList_1.Count - 1


        If LaserList_1.Count > 1 Then
            btnDelete.Enabled = True
        Else
            btnDelete.Enabled = False
        End If

        btnAdd.Enabled = True
    End Sub

    Private Sub btnAdd_2_Click(sender As Object, e As EventArgs) Handles btnAdd_2.Click
        btnAdd_2.Enabled = False

        Dim AddData As New LaserStructure

        AddData.LaserPositionX = 0
        AddData.LaserPositionY = 0
        AddData.LaserPositionZ = 0
        AddData.LaserData1 = LaserList_2.Count
        LaserList_2.Add(AddData)

        '清空重新顯示
        lstNonArray_2.Items.Clear()
        For mi = 0 To LaserList_2.Count - 1
            lstNonArray_2.Items.Add((mi).ToString("000"))
        Next
        lstNonArray_2.SelectedIndex = LaserList_2.Count - 1


        If LaserList_2.Count > 1 Then
            btnDelete_2.Enabled = True
        Else
            btnDelete_2.Enabled = False
        End If

        btnAdd_2.Enabled = True
    End Sub

    Private Sub btnSetPos_NonArray_Click(sender As Object, e As EventArgs) Handles btnSetPos_NonArray.Click
        btnSetPos.Enabled = False

        Dim SetData As New LaserStructure

        SetData.LaserPositionX = txtPosX.Text
        SetData.LaserPositionY = txtPosY.Text
        SetData.LaserPositionZ = txtPosZ.Text
        SetData.LaserData1 = lstNonArray.SelectedIndex
        LaserList_1(lstNonArray.SelectedIndex) = SetData

        btnSetPos.Enabled = True
    End Sub

    Private Sub btnSetPos_NonArray_2_Click(sender As Object, e As EventArgs) Handles btnSetPos_NonArray_2.Click
        btnSetPos_2.Enabled = False

        Dim SetData As New LaserStructure

        SetData.LaserPositionX = txtPosX_2.Text
        SetData.LaserPositionY = txtPosY_2.Text
        SetData.LaserPositionZ = txtPosZ_2.Text
        SetData.LaserData1 = lstNonArray_2.SelectedIndex
        LaserList_2(lstNonArray_2.SelectedIndex) = SetData

        btnSetPos_2.Enabled = True
    End Sub

  
End Class
﻿Imports ProjectRecipe
Imports ProjectMotion
Imports ProjectCore
Imports ProjectAOI
Imports ProjectIO
Imports System.Threading

Public Class frmRecipe01
    Public myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRecipe01))
    Dim mMultiArrayAdapter As CMultiArrayAdapter
    Public RecipeEdit As ProjectRecipe.CRecipe
    ''' <summary>外部傳入系統配置</summary>
    ''' <remarks></remarks>
    Public sys As sSysParam
    ''' <summary>外部傳入NodeID</summary>
    ''' <remarks></remarks>
    Public NodeID As String
    ''' <summary>外部傳入PatternID</summary>
    ''' <remarks></remarks>
    Public PatternID As String
    ''' <summary>
    ''' Form_Load時備份,Cancel時還原
    ''' </summary>
    ''' <remarks></remarks>
    Dim backup As CRecipeNode

    Dim conveyorNo As eConveyor = eConveyor.ConveyorNo1

    Private mContextUI As System.Threading.SynchronizationContext = Nothing 'Eason 20170413 Ticket:100110 , Use SynchronizationContext replace DoEvent Refresh UI 

    Private mTaskTokenSource As CancellationTokenSource = New CancellationTokenSource()

    Private mTaskToken As CancellationToken

    Private mRunLongProcessTask As Task(Of Integer) = Nothing
    ''' <summary>定位點在陣列索引 預設0
    ''' </summary>
    ''' <remarks></remarks>
    Dim AbsIdxX As Integer
    ''' <summary>定位點在陣列索引 預設0
    ''' </summary>
    ''' <remarks></remarks>
    Dim AbsIdxY As Integer

#Region "From動作"

    Private Sub frmRecipe01_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        UcDisplay1.EndLive() '2016.06.22 改成關閉才EndLive
        'Eason 20170124 Ticket:100031 , Memory Freed [S]
        UcDisplay1.ManualDispose()
        UcLightControl1.ManualDispose()
        UcJoyStick1.ManualDispose()
        Me.Dispose(True)
        'Eason 20170124 Ticket:100031 , Memory Freed [E]
    End Sub

    Private Sub frmRecipeMultiDevice01_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "Pattern(" & NodeID & ") Alignment."
        RefreshConveyorUI(gSSystemParameter.ConveyorModel)
        RefreshCCDButtonPos(conveyorNo)

        mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(sys.StageNo)(NodeID).Array)
        Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height

        '[Note]確認From是否可以開啟
        CheckFromClose()

        Select Case gAOICollection.GetCCDType(enmCCD.CCD1) 'gSSystemParameter.enmCCDType
            Case enmCCDType.CognexVPRO
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
            Case enmCCDType.OmronFZS2MUDP
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
            Case Else
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
        End Select

        '[Note]備份
        backup = RecipeEdit.Node(sys.StageNo)(NodeID).Clone

        gAOICollection.LoadSceneList("")
        '[Note]cmbSceneList狀態更新
        RefreshScene()

        '[Note]cmbArray資料顯示
        AbsIdxX = RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexX
        AbsIdxY = RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexY

        '[Note]更新按钮狀態
        RefreshButton(RecipeEdit.Editable)

        For i As Integer = 0 To 1
            For j As Integer = 0 To 2
                mIsAlignSuccess(i, j) = True
            Next
        Next

        ShowRecipeData()

        '[Note]:不管有沒有啟用，都預先載入設定值
        '[Note]:製程表示若沒資料載入預設光源，若有資料顯示第一組定位場景光源
        Dim mSceneName As String = "CALIB" & (sys.CCDNo + 1).ToString '預設CALIB1校正場景
        UcLightControl1.CCDNo = sys.CCDNo
        If gAOICollection.SceneDictionary.ContainsKey(mSceneName) Then
            UcLightControl1.SceneName = mSceneName '[Note]不能載入使用的場景，否則在Recipe01調整亮度時，會直接更改到場景亮度
        End If

        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene <> "" And RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene IsNot Nothing Then
            '[Note]若有定位場景，讓預設場景顯示正確資料即可
            mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
            If gAOICollection.SceneDictionary.ContainsKey(mSceneName) Then
                '[Note]開燈
                SetLight(sys.CCDNo, mSceneName)
                '[Note]光源控制項顯示
                UpdateUcLightData(mSceneName)
            End If

        Else
            '[Note]顯示預設場景資料
            UcLightControl1.ShowUI()
        End If


        ''[Note]舊有設計可以切換曝光時間，為飛拍不開放曝光時間設定，所以不需要觸發拍照切換場景亮度  wenda 20170811
        'If Recipe.Node(sys.StageNo)(NodeID).AlignmentEnable = True Then 'Soni + 2016.09.24 未啟用不設場景
        '    If Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count > 0 Then
        '        If gAOICollection.SetCCDScene(sys.CCDNo, Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene) = False Then
        '            '場景不存在
        '            gSyslog.Save(Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene & gMsgHandler.GetMessage(Warn_3000020))
        '            MsgBox(Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene & gMsgHandler.GetMessage(Warn_3000020))
        '        Else
        '            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        '            System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        '            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照
        '            System.Threading.Thread.CurrentThread.Join(10) '加快開啟介面時間
        '            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉
        '        End If
        '    Else
        '        '定位資料不存在
        '        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
        '        MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    End If
        'End If

        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
            RefreshButton(False)
        Else
            RefreshButton(True)
        End If

        UcJoyStick1.AxisX = sys.AxisX
        UcJoyStick1.AxisY = sys.AxisY
        UcJoyStick1.AxisZ = sys.AxisZ
        UcJoyStick1.AXisA = sys.AxisA
        UcJoyStick1.AXisB = sys.AxisB
        UcJoyStick1.AXisC = sys.AxisC
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

        If gUserLevel = enmUserLevel.eSoftwareMaker Then
            btnTargetEdit.Visible = True
        Else
            btnTargetEdit.Visible = False
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

    End Sub

    ''' <summary>
    ''' 確認From是否可開啟
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CheckFromClose()

        If IsRecipeNormal() = False Then
            Me.Close()
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 檢察Recipe設定是否正常
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsRecipeNormal() As Boolean
        '[Note]系統失敗
        If sys Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Error_1000013))
            MsgBox(gMsgHandler.GetMessage(Error_1000013), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If

        '[Note]系統平台錯誤
        If sys.StageNo < 0 Then
            gSyslog.Save(gMsgHandler.GetMessage(Error_1000014))
            MsgBox(gMsgHandler.GetMessage(Error_1000014), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If

        '[Note]Node 不存在,請先選擇Node
        If NodeID = "" Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
            MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If

        '[Note]請先選擇Node
        If Not RecipeEdit.Node(sys.StageNo).ContainsKey(NodeID) Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
            MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        Return True
    End Function

#End Region

#Region "資料顯示"
    ''' <summary>
    ''' 更新光源控制項顯示
    ''' </summary>
    ''' <param name="mSceneName"></param>
    ''' <remarks></remarks>
    Private Sub UpdateUcLightData(ByVal mSceneName As String)
        If gAOICollection.SceneDictionary.ContainsKey(mSceneName) Then
            UcLightControl1.chkLight1.Checked = gAOICollection.SceneDictionary(mSceneName).LightEnable(enmValveLight.No1)
            UcLightControl1.chkLight2.Checked = gAOICollection.SceneDictionary(mSceneName).LightEnable(enmValveLight.No2)
            UcLightControl1.chkLight3.Checked = gAOICollection.SceneDictionary(mSceneName).LightEnable(enmValveLight.No3)
            UcLightControl1.chkLight4.Checked = gAOICollection.SceneDictionary(mSceneName).LightEnable(enmValveLight.No4)
            UcLightControl1.nmcLight1.Value = gAOICollection.SceneDictionary(mSceneName).LightValue(enmValveLight.No1)
            UcLightControl1.nmcLight2.Value = gAOICollection.SceneDictionary(mSceneName).LightValue(enmValveLight.No2)
            UcLightControl1.nmcLight3.Value = gAOICollection.SceneDictionary(mSceneName).LightValue(enmValveLight.No3)
            UcLightControl1.nmcLight4.Value = gAOICollection.SceneDictionary(mSceneName).LightValue(enmValveLight.No4)
        End If

    End Sub



    ''' <summary>
    ''' 資料顯示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowRecipeData()

        If IsRecipeNormal() = False Then
            Exit Sub
        End If

        '[Note]Scale數據 1個Pixel幾mm
        txtA11.Text = gSSystemParameter.CCDScaleX2X(sys.StageNo)
        txtA12.Text = gSSystemParameter.CCDScaleY2X(sys.StageNo)
        txtA21.Text = gSSystemParameter.CCDScaleX2Y(sys.StageNo)
        txtA22.Text = gSSystemParameter.CCDScaleY2Y(sys.StageNo)

        'Soni 2017.02.09 雙軌資料結構
        '[Note]產品基準點位置

        txtBasicPosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).TeachBasicPosX
        txtBasicPosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).TeachBasicPosY
        txtBasicPosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).TeachBasicPosZ
        txtBasicPos2X.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).TeachBasicPosX
        txtBasicPos2Y.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).TeachBasicPosY
        txtBasicPos2Z.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).TeachBasicPosZ

        '[Note]是否定位 是否辨別不生產點
        chkAlignmentEnable.Checked = RecipeEdit.Node(sys.StageNo)(NodeID).AlignmentEnable
        chkAlignmentEnable2.Checked = RecipeEdit.Node(sys.StageNo)(NodeID).AlignmentEnable
        chkSkipMarkEnable.Checked = RecipeEdit.Node(sys.StageNo)(NodeID).SkipMarkEnable
        chkSkipMarkEnable2.Checked = RecipeEdit.Node(sys.StageNo)(NodeID).SkipMarkEnable

        '[Note]因擔心頁面開燈場景不一致，最後才顯示Conveyor1 AlignData
        Dim mSceneName As String

        '雙軌資料結構 Conveyor2 SkipMarkData
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).SkipMarkData.Count > 0 Then '陣列資料存在
            txtSkipMark2PosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).SkipMarkData(0).TeachPosX '+ mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
            txtSkipMark2PosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).SkipMarkData(0).TeachPosY '+ mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
            txtSkipMark2PosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).SkipMarkData(0).TeachPosZ
            mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).SkipMarkData(0).AlignScene
            '[Note]:場景是空的就不用了
            If mSceneName <> "" Then
                SetCmbSceneID(cmbSkipMark2Scene, mSceneName)
            End If
        Else
            'Conveyor2 SkipMark資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2084129))
            MsgBox(gMsgHandler.GetMessage(Alarm_2084129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If

        'Soni 2017.02.09 雙軌資料結構 Conveyor2 AlignData
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).AlignmentData.Count > 1 Then '陣列資料存在 
            '[Note]第二點定位資料
            txtCCD2PosX2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).AlignmentData(1).TeachPosX '+ mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
            txtCCD2PosY2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).AlignmentData(1).TeachPosY '+ mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
            mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).AlignmentData(1).AlignScene
            '[Note]:場景是空的就不用了
            If mSceneName <> "" Then
                SetCmbSceneID(cmbScene2ID2, mSceneName)
            End If
            '[Note]第一點定位資料
            txtCCD2PosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).AlignmentData(0).TeachPosX '+ mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
            txtCCD2PosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).AlignmentData(0).TeachPosY '+ mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
            txtCCD2PosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).AlignmentData(0).TeachPosZ
            mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).AlignmentData(0).AlignScene
            '[Note]:場景是空的就不用了
            If mSceneName <> "" Then
                SetCmbSceneID(cmbScene2ID, mSceneName)
            End If
        Else
            'Conveyor2 定位資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2084128))
            MsgBox(gMsgHandler.GetMessage(Alarm_2084128), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If



        '雙軌資料結構 Conveyor1 SkipMarkData
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).SkipMarkData.Count > 0 Then '陣列資料存在
            '[Note]不生產點資料
            txtSkipMarkPosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).SkipMarkData(0).TeachPosX '+ mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
            txtSkipMarkPosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).SkipMarkData(0).TeachPosY '+ mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
            txtSkipMarkPosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).SkipMarkData(0).TeachPosZ
            mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).SkipMarkData(0).AlignScene
            '[Note]:場景是空的就不用了
            If mSceneName <> "" Then
                SetCmbSceneID(cmbSkipMarkScene, mSceneName)
            End If
        Else
            'Conveyor1 SkipMark資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2083129))
            MsgBox(gMsgHandler.GetMessage(Alarm_2083129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If



        'Soni 2017.02.09 雙軌資料結構 Conveyor1 AlignData
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).AlignmentData.Count > 2 Then '陣列資料存在 
            '[Note]第二點定位資料
            txtCCDPosX2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).AlignmentData(1).TeachPosX '+ mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
            txtCCDPosY2.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).AlignmentData(1).TeachPosY '+ mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
            mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).AlignmentData(1).AlignScene
            '[Note]:場景是空的就不用了
            If mSceneName <> "" Then
                SetCmbSceneID(cmbSceneID2, mSceneName)
            End If
            '[Note]第一點定位資料
            txtCCDPosX.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).AlignmentData(0).TeachPosX '+ mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
            txtCCDPosY.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).AlignmentData(0).TeachPosY '+ mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
            txtCCDPosZ.Text = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).AlignmentData(0).TeachPosZ
            mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(0).AlignmentData(0).AlignScene
            '[Note]:場景是空的就不用了
            If mSceneName <> "" Then
                SetCmbSceneID(cmbSceneID, mSceneName)
            End If
        Else
            'Conveyor1 定位資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2083128))
            MsgBox(gMsgHandler.GetMessage(Alarm_2083128), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If

    End Sub

#End Region

    ''' <summary>
    ''' 按鈕狀況更新
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshButton(ByVal state As Boolean)
        btnScalePR.Enabled = state
        btnSetBasicPos.Enabled = state
        btnMoveBasicPos.Enabled = state
        btnSetBasicPos2.Enabled = state
        btnMoveBasicPos2.Enabled = state

        btnALign.Enabled = state
        btnSetCcdPosX.Enabled = state
        btnGoCCD.Enabled = state
        btnALign2.Enabled = state
        btnSetCcdPosX2.Enabled = state
        btnGoCCD2.Enabled = state
        btnALign3.Enabled = state
        btnSetCCDPosX3.Enabled = state
        btnGoCCD3.Enabled = state
        btnSkipMarkAlign.Enabled = state
        btnSetSkipMarkPosX.Enabled = state
        btnGoSkipMark.Enabled = state

        btn2ALign.Enabled = state
        btnSetCcd2PosX.Enabled = state
        btnGo2CCD.Enabled = state
        btn2ALign2.Enabled = state
        btnSetCcd2PosX2.Enabled = state
        btnGo2CCD2.Enabled = state
        btn2ALign3.Enabled = state
        btnSetCcd2PosX3.Enabled = state
        btnGo2CCD3.Enabled = state
        btnSkipMark2Align.Enabled = state
        btnSetSkipMark2PosX.Enabled = state
        btnGoSkipMark2.Enabled = state

        UcJoyStick1.Enabled = state
    End Sub

    Public Sub RefreshScene()
        '[Note]定位場景
        cmbSceneID.Items.Clear()
        cmbSceneID2.Items.Clear()
        cmbScene2ID.Items.Clear()
        cmbScene2ID2.Items.Clear()
        cmbSceneID3.Items.Clear()
        cmbScene2ID3.Items.Clear()
        Dim mSceneName As String
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count > 2 Then 'Soni 2017.02.09 雙軌資料結構
            If Not gAOICollection.GetSceneList Is Nothing Then
                cmbSceneID.Items.AddRange(gAOICollection.GetSceneList)
                cmbScene2ID.Items.AddRange(gAOICollection.GetSceneList)
                mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
                '[Note]:場景是空的就不用了
                If mSceneName <> "" Then
                    SetCmbSceneID(cmbSceneID, mSceneName)
                    SetCmbSceneID(cmbScene2ID, mSceneName)
                End If

                cmbSceneID2.Items.AddRange(gAOICollection.GetSceneList)
                cmbScene2ID2.Items.AddRange(gAOICollection.GetSceneList)
                mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene
                '[Note]:場景是空的就不用了
                If mSceneName <> "" Then
                    SetCmbSceneID(cmbSceneID2, mSceneName)
                    SetCmbSceneID(cmbScene2ID2, mSceneName)
                End If

                cmbSceneID3.Items.AddRange(gAOICollection.GetSceneList)
                cmbScene2ID3.Items.AddRange(gAOICollection.GetSceneList)
                mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene
                '[Note]:場景是空的就不用了
                If mSceneName <> "" Then
                    SetCmbSceneID(cmbSceneID3, mSceneName)
                    SetCmbSceneID(cmbScene2ID3, mSceneName)
                End If
            End If
        End If

        '[Note]不生產點場景
        cmbSkipMarkScene.Items.Clear()
        cmbSkipMark2Scene.Items.Clear()
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData.Count >= 1 Then
            If Not gAOICollection.GetSceneList Is Nothing Then
                cmbSkipMarkScene.Items.AddRange(gAOICollection.GetSceneList)
                cmbSkipMark2Scene.Items.AddRange(gAOICollection.GetSceneList)
                mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignScene
                '[Note]:場景是空的就不用了
                If mSceneName <> "" Then
                    SetCmbSceneID(cmbSkipMarkScene, mSceneName)
                    SetCmbSceneID(cmbSkipMark2Scene, mSceneName)
                End If
            End If
        End If

    End Sub

    ''' <summary>依選擇功能,顯示介面配置</summary>
    ''' <remarks></remarks>
    Sub RefreshCCDButtonPos(Optional ByVal conveyorNo As eConveyor = eConveyor.ConveyorNo1)

        If IsRecipeNormal() = False Then
            Exit Sub
        End If

        Select Case RecipeEdit.Node(sys.StageNo)(NodeID).AlignType 'Soni 2017.02.09 雙軌資料結構
            Case ProjectRecipe.enmAlignType.DevicePos1
                btnAlignType.Text = GetString("DevicePos1") ' "1 Alignment Pos"
                btnAlignType2.Text = GetString("DevicePos1") ' "1 Alignment Pos"
                palAlignPos2.Visible = False
                palAlignPos3.Visible = False
                palAlign2Pos2.Visible = False
                palAlign2Pos3.Visible = False
            Case ProjectRecipe.enmAlignType.DevicePos2
                btnAlignType.Text = GetString("DevicePos2")  '"2 Alignemnt Pos"
                btnAlignType2.Text = GetString("DevicePos2")  '"2 Alignemnt Pos"
                palAlignPos2.Visible = True
                palAlignPos3.Visible = False
                palAlign2Pos2.Visible = True
                palAlign2Pos3.Visible = False
            Case enmAlignType.DevicePos3
                btnAlignType.Text = GetString("DevicePos3")  '"3 Alignemnt Pos"
                btnAlignType2.Text = GetString("DevicePos3")  '"3 Alignemnt Pos"
                palAlignPos2.Visible = True
                palAlignPos3.Visible = True
                palAlign2Pos2.Visible = True
                palAlign2Pos3.Visible = True
        End Select
    End Sub

    Sub SetCmbSceneID(ByRef cmb As ComboBox, ByVal sceneID As String)
        If sceneID Is Nothing Then
            'cmb.BackColor = Color.Red
            '請建立場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000023))
            MsgBox(gMsgHandler.GetMessage(Warn_3000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not cmb.Items.Contains(sceneID) Then
            'cmb.BackColor = Color.Red
            '場景不存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000020) & sceneID)
            MsgBox(gMsgHandler.GetMessage(Warn_3000020) & sceneID, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        cmb.SelectedItem = sceneID
    End Sub

    Sub SelectScene(ByVal sceneName As String, Optional ByVal isUpdateLight As Boolean = True)
        If sceneName = "" Or sceneName Is Nothing Then '場景為空
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(sceneName) Then '場景不存在
            Dim RecipeDirectoryName As String = Application.StartupPath & "\Scene\" & MachineName
            Dim file As String = RecipeDirectoryName & "\" & sceneName & ".vpp"
            If gAOICollection.LoadVision(sys.CCDNo, file) = False Then
                '場景不存在
                gSyslog.Save(sceneName & gMsgHandler.GetMessage(Warn_3000020))
                MsgBox(sceneName & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Exit Sub
            End If

            Dim mFileName = RecipeDirectoryName & "\" & sceneName & ".ini" '光源設定檔路徑
            If System.IO.File.Exists(mFileName) Then '如果設定檔存在
                gAOICollection.LoadSceneParameter(sceneName, mFileName) '讀取光源,曝光值等設定
            Else
                '參數檔案不存在
                gSyslog.Save(sceneName & gMsgHandler.GetMessage(Warn_3000041))
                MsgBox(sceneName & gMsgHandler.GetMessage(Warn_3000041), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Exit Sub
            End If

        End If

        If isUpdateLight = True Then
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
        End If

    End Sub

    Public Function GetString(ByVal value As String) As String
        Select Case value
            Case "DevicePos1"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "1-Alignment Pos"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "单点定位"
                    Case enmLanguageType.eTraditionalChinese
                        Return "單點定位"
                End Select
            Case "DevicePos2"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "2-Alignment Pos"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "两点定位"
                    Case enmLanguageType.eTraditionalChinese
                        Return "兩點定位"
                End Select
            Case "DevicePos3"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "3-Alignment Pos"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "三点定位"
                    Case enmLanguageType.eTraditionalChinese
                        Return "三點定位"
                End Select
                '----
            Case "Not Exists!"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Not Exists!"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "不存在!"
                    Case enmLanguageType.eTraditionalChinese
                        Return "不存在!"
                End Select

            Case "Please click Conveyor"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Please click Conveyor"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "请确认流道"
                    Case enmLanguageType.eTraditionalChinese
                        Return "請確認流道"
                End Select
        End Select
        Return ""
    End Function


#Region "介面按钮"

    Private Function CheckAlignOffsetResult(ByVal ConveyorStart As Integer, ByVal ConveyorEnd As Integer, ByVal AlignType As enmAlignType) As Boolean

        For i As Integer = ConveyorStart To ConveyorEnd
            Select Case AlignType
                Case enmAlignType.DevicePos1
                    gSyslog.Save("Conveyor: " & i & " CheckAlignOffsetResult: " & mIsAlignSuccess(i, 0))
                    If mIsAlignSuccess(i, 0) = False Then
                        'If Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(i).AlignmentData(0).AlignOffsetX = 0 Then
                        Select Case gSSystemParameter.LanguageType
                            Case enmLanguageType.eEnglish
                                MsgBox("Please click Conveyor" & (i + 1).ToString & " Align button", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case enmLanguageType.eSimplifiedChinese
                                MsgBox("请点擊流道" & (i + 1).ToString & "的定位按钮", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case enmLanguageType.eTraditionalChinese
                                MsgBox("请點擊流道" & (i + 1).ToString & "的定位按钮", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        End Select
                        Return False
                    End If

                Case enmAlignType.DevicePos2
                    gSyslog.Save("Conveyor: " & i & " CheckAlignOffsetResult: " & mIsAlignSuccess(i, 0))
                    gSyslog.Save("Conveyor: " & i & " CheckAlignOffsetResult: " & mIsAlignSuccess(i, 1))
                    If mIsAlignSuccess(i, 0) = False Then
                        'If Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(i).AlignmentData(0).AlignOffsetX = 0 Then
                        'MsgBox("Please click Conveyor" & (i + 1).ToString & " Align button")
                        Select Case gSSystemParameter.LanguageType
                            Case enmLanguageType.eEnglish
                                MsgBox("Please click Conveyor" & (i + 1).ToString & " Align button", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case enmLanguageType.eSimplifiedChinese
                                MsgBox("请点擊流道" & (i + 1).ToString & "的定位按钮", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case enmLanguageType.eTraditionalChinese
                                MsgBox("请點擊流道" & (i + 1).ToString & "的定位按钮", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        End Select
                        Return False
                    End If
                    If mIsAlignSuccess(i, 1) = False Then
                        'If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(i).AlignmentData(1).AlignOffsetX = 0 Then
                        'MsgBox("Please click Conveyor" & (i + 1).ToString & " Align2 button")
                        Select Case gSSystemParameter.LanguageType
                            Case enmLanguageType.eEnglish
                                MsgBox("Please click Conveyor" & (i + 1).ToString & " Align2 button", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case enmLanguageType.eSimplifiedChinese
                                MsgBox("请点擊流道" & (i + 1).ToString & "的定位2按钮", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case enmLanguageType.eTraditionalChinese
                                MsgBox("请點擊流道" & (i + 1).ToString & "的定位2按钮", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        End Select
                        Return False
                    End If
                Case enmAlignType.DevicePos3
                    '[Note]目前不支援三點定位

                    Return False
            End Select
        Next
        Return True
    End Function

    ''' <summary>
    ''' 存檔
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        If RecipeEdit.Editable Then
            gSyslog.Save("RecipeEdit.Editable")
            conveyorNo = eConveyor.ConveyorNo1
            With RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0)
                .TeachPosX = Val(txtCCDPosX.Text)
                .TeachPosY = Val(txtCCDPosY.Text)
                .TeachPosZ = Val(txtCCDPosZ.Text)
            End With
            With RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1)
                .TeachPosX = Val(txtCCDPosX2.Text)
                .TeachPosY = Val(txtCCDPosY2.Text)
                .TeachPosZ = Val(txtCCDPosZ.Text)
            End With
            With RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2)
                .TeachPosX = Val(txtCCDPosX3.Text)
                .TeachPosY = Val(txtCCDPosY3.Text)
                .TeachPosZ = Val(txtCCDPosZ.Text)
            End With
            With RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0)
                .TeachPosX = Val(txtSkipMarkPosX.Text)
                .TeachPosY = Val(txtSkipMarkPosY.Text)
                .TeachPosZ = Val(txtSkipMarkPosZ.Text)
            End With
            With RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo)
                .TeachBasicPosX = Val(txtBasicPosX.Text)
                .TeachBasicPosY = Val(txtBasicPosY.Text)
                .TeachBasicPosZ = Val(txtBasicPosZ.Text)
            End With
            conveyorNo = eConveyor.ConveyorNo2
            With RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0)
                .TeachPosX = Val(txtCCD2PosX.Text)
                .TeachPosY = Val(txtCCD2PosY.Text)
                .TeachPosZ = Val(txtCCD2PosZ.Text)
            End With
            With RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1)
                .TeachPosX = Val(txtCCD2PosX2.Text)
                .TeachPosY = Val(txtCCD2PosY2.Text)
                .TeachPosZ = Val(txtCCD2PosZ.Text)
            End With
            With RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2)
                .TeachPosX = Val(txtCCD2PosX3.Text)
                .TeachPosY = Val(txtCCD2PosY3.Text)
                .TeachPosZ = Val(txtCCD2PosZ.Text)
            End With
            With RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0)
                .TeachPosX = Val(txtSkipMark2PosX.Text)
                .TeachPosY = Val(txtSkipMark2PosY.Text)
                .TeachPosZ = Val(txtSkipMark2PosZ.Text)
            End With
            With RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo)
                .TeachBasicPosX = Val(txtBasicPos2X.Text)
                .TeachBasicPosY = Val(txtBasicPos2Y.Text)
                .TeachBasicPosZ = Val(txtBasicPos2Z.Text)
            End With
            RecipeEdit.UpdateOriginData(sys.StageNo, NodeID, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexX, RecipeEdit.Node(sys.StageNo)(NodeID).TeachIndexY, mMultiArrayAdapter)

            If RecipeEdit.Node(sys.StageNo)(NodeID).AlignmentEnable = True Then
                Dim ConveyorStart As Integer
                Dim ConveyorEnd As Integer
                '[Note]:使用流道的配置
                Select Case gSSystemParameter.ConveyorModel
                    Case eConveyorModel.eConveyorNo1
                        ConveyorStart = eConveyor.ConveyorNo1
                        ConveyorEnd = eConveyor.ConveyorNo1
                    Case eConveyorModel.eConveyorNo2
                        ConveyorStart = eConveyor.ConveyorNo2
                        ConveyorEnd = eConveyor.ConveyorNo2
                    Case eConveyorModel.eConveyorNo1No2
                        ConveyorStart = eConveyor.ConveyorNo1
                        ConveyorEnd = eConveyor.ConveyorNo2
                End Select
                If CheckAlignOffsetResult(ConveyorStart, ConveyorEnd, RecipeEdit.Node(sys.StageNo)(NodeID).AlignType) = False Then
                    Exit Sub
                End If
            End If

            gSSystemParameter.CCDScaleX2X(0) = Val(txtA11.Text)
            gSSystemParameter.CCDScaleY2X(0) = Val(txtA12.Text)
            gSSystemParameter.CCDScaleX2Y(0) = Val(txtA21.Text)
            gSSystemParameter.CCDScaleY2Y(0) = Val(txtA22.Text)
            gSSystemParameter.SaveCCDSCale(Application.StartupPath & "\System\" & MachineName & "\SysCCD.ini")
            'Soni 2017.02.09 雙軌資料結構
            'Recipe.Node(sys.StageNo)(NodeID).TeachIndexX = cmbArrayX.SelectedIndex
            'Recipe.Node(sys.StageNo)(NodeID).TeachIndexY = cmbArrayY.SelectedIndex
            '[Note]:需儲存場景名稱 -->Mobary+ 2016.10.01
            'RecipeEdit.SaveNode(RecipeEdit.strFileName)   'Mobary+ 2016.10.01
            '存檔成功 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
        ''--- 可編輯才儲存 ---
        Debug.Print("存檔前 場景:" & RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene)
        backup = RecipeEdit.Node(sys.StageNo)(NodeID).Clone
        Debug.Print("存檔後 場景:" & RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene)
        'Sue20170627
        'Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")


        RecipeEdit.Node(sys.StageNo)(NodeID) = backup.Clone '還原

        Me.Close()
    End Sub

    ''' <summary>
    ''' 顯示半徑
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSetRadius_Click(sender As Object, e As EventArgs) Handles btnSetRadius.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        Dim AssistLineRadius As Double = Val(txtRadius.Text)
        If AssistLineRadius < 0 Then
            '輸入資料錯誤(半徑)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        btnSetRadius.Enabled = False

        Dim AssistLinePixelX As Integer
        Dim AssistLinePixelY As Integer
        If gSSystemParameter.CCDScaleX2X(sys.CCDNo) = 0 Or gSSystemParameter.CCDScaleY2Y(sys.CCDNo) = 0 Then
            AssistLinePixelX = 0
            AssistLinePixelY = 0
        Else
            AssistLinePixelX = Val(txtRadius.Text) / gSSystemParameter.CCDScaleX2X(sys.CCDNo)
            AssistLinePixelY = Val(txtRadius.Text) / gSSystemParameter.CCDScaleY2Y(sys.CCDNo)
        End If

        InvokeUcDisplay(UcDisplay1, gAOICollection, sys, cmbSceneID.SelectedItem, AssistLinePixelX, AssistLinePixelY, enmDisplayShape.Circle) '更新控制項,必要條件 frmMain必須是實體

        btnSetRadius.Enabled = True
    End Sub

#End Region


#Region "Conveyor頁面切換"

    Private Sub TabConveyorBasicPosition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabConveyorBasicPosition.SelectedIndexChanged
        Select Case gSSystemParameter.ConveyorModel
            Case eConveyorModel.eConveyorNo1
                conveyorNo = eConveyor.ConveyorNo1

            Case eConveyorModel.eConveyorNo2
                conveyorNo = eConveyor.ConveyorNo2

            Case eConveyorModel.eConveyorNo1No2
                If TabConveyorBasicPosition.SelectedIndex = 1 Then
                    conveyorNo = eConveyor.ConveyorNo2
                Else
                    conveyorNo = eConveyor.ConveyorNo1
                End If
                TabConveyorControl.SelectTab(conveyorNo)

        End Select
    End Sub

    Private Sub TabConveyorControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabConveyorControl.SelectedIndexChanged
        Select Case gSSystemParameter.ConveyorModel
            Case eConveyorModel.eConveyorNo1
                conveyorNo = eConveyor.ConveyorNo1

            Case eConveyorModel.eConveyorNo2
                conveyorNo = eConveyor.ConveyorNo2

            Case eConveyorModel.eConveyorNo1No2
                If TabConveyorControl.SelectedIndex = 1 Then
                    conveyorNo = eConveyor.ConveyorNo2
                Else
                    conveyorNo = eConveyor.ConveyorNo1
                End If
                TabConveyorBasicPosition.SelectTab(conveyorNo)

        End Select

    End Sub

    ''' <summary>
    ''' 雙軌介面更新
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshConveyorUI(ByVal ConveyorModel As eConveyorModel)
        '[Note]:使用流道的配置
        Select Case gSSystemParameter.ConveyorModel
            Case eConveyorModel.eConveyorNo1
                TabConveyorControl.Controls.Remove(TabPageConveyor2)
                TabConveyorBasicPosition.Controls.Remove(TabBasicPotion2)
                'TabConveyorControl.TabPages(1).Parent = Nothing
                'TabConveyorBasicPosition.TabPages(1).Parent = Nothing

            Case eConveyorModel.eConveyorNo2
                TabConveyorControl.Controls.Remove(TabPageConveyor1)
                TabConveyorBasicPosition.Controls.Remove(TabBasicPotion1)
                'TabConveyorControl.TabPages(0).Parent = Nothing
                'TabConveyorBasicPosition.TabPages(0).Parent = Nothing

            Case eConveyorModel.eConveyorNo1No2
                '[Note]介面不隱藏

        End Select
    End Sub
#End Region


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
        Threading.Thread.CurrentThread.Join(100) '到位穩定

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
        gAOICollection.ShowAlignResult(sys.CCDNo, Ticket, UcDisplay1)

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

        btnScalePR.Enabled = False

        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            '請先復歸
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005))
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnScalePR.Enabled = True
            Exit Sub
        End If

        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 1 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnScalePR.Enabled = True
            Exit Sub
        End If
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene = "" Then
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

            btnScalePR.Enabled = True
            Exit Sub
        End If
        If Not gAOICollection.IsSceneExist(sys.CCDNo, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene) Then
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000009), "Alarm_2000009", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Alarm_2000009), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnScalePR.Enabled = True
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
            btnScalePR.Enabled = True
            Exit Sub
        End If
        btnScalePR.BackColor = Color.Yellow '運行中
        btnScalePR.Refresh()

        'Await Task.Run(Sub()

        Dim RefPosX As Decimal '參考基準點 位置(mm)
        Dim RefPosY As Decimal '參考基準點 位置(mm)
        Dim RefPosZ As Decimal '參考基準點 位置(mm)

        '定位點位置
        RefPosX = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX
        RefPosY = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY
        RefPosZ = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ

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

        TargetPos(0) = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX
        TargetPos(1) = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY
        TargetPos(2) = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
        TargetPos(3) = 0
        TargetPos(4) = 0

        If ButtonSafeMovePos(sender, AxisNo, TargetPos, sys) = False Then 'TODO:增加例外離開
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   btnScalePR.Enabled = True
                               End Sub)
            End If
            Exit Sub
        End If


        Dim sceneName As String = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene

        '第一基準點 移動 取像 計算
        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, sceneName, RefPosX, RefPosY, Xo0, Yo0, Xi0, Yi0) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   btnScalePR.Enabled = True
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
        Dim mSceneName As String = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene '"TB_ALIGN0" '場景編號
        Dim mTrainWidth As Decimal '教導Pattern大小
        Dim mTrainHeight As Decimal '教導Pattern大小
        Dim mRotation As Decimal = 0 '教導Pattern角度
        gAOICollection.GetAlignTrainSideLength(sys.CCDNo, mSceneName, mTrainWidth, mTrainHeight, mRotation)
        '第二基準點 移動
        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, sceneName, RefPosX - stepPitch, RefPosY + stepPitch, Xo1, Yo1, Xi1, Yi1) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   btnScalePR.Enabled = True
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
        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, sceneName, RefPosX + stepPitch, RefPosY + stepPitch, Xo2, Yo2, Xi2, Yi2) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   btnScalePR.Enabled = True
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
        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, sceneName, RefPosX, RefPosY - stepPitch, Xo3, Yo3, Xi3, Yi3) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   btnScalePR.Enabled = True
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

        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, sceneName, targetPosX, targetPosY, Xo1, Yo1, Xi1, Yi1) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   btnScalePR.Enabled = True
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

        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, sceneName, targetPosX, targetPosY, Xo2, Yo2, Xi2, Yi2) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   btnScalePR.Enabled = True
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

        If Not DoLoopMoveAcqAlign(sys.AxisX, sys.AxisY, sceneName, targetPosX, targetPosY, Xo3, Yo3, Xi3, Yi3) Then
            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnScalePR.BackColor = SystemColors.Control
                                   btnScalePR.Enabled = True
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
                                   btnScalePR.Enabled = True
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
                               btnScalePR.Enabled = True
                           End Sub)
        End If

        'End Sub)
    End Sub

#End Region


#Region "BasicPos 基準點動作"


    Private Sub btnSetBasicPos_Click(sender As Object, e As EventArgs) Handles btnSetBasicPos.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnSetBasicPos.Enabled = False
        If Not RecipeEdit.Editable Then '[Note]配方不能編輯
            BtnReadOnlyBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            btnSetBasicPos.Enabled = True
            Exit Sub
        End If
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetBasicPos.Enabled = True
            Exit Sub
        End If
        txtBasicPosX.Text = gCMotion.GetPositionValue(sys.AxisX)
        txtBasicPosY.Text = gCMotion.GetPositionValue(sys.AxisY)
        txtBasicPosZ.Text = gCMotion.GetPositionValue(sys.AxisZ)

        btnSetBasicPos.Enabled = True
    End Sub

    Private Sub btnSetBasicPos2_Click(sender As Object, e As EventArgs) Handles btnSetBasicPos2.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnSetBasicPos2.Enabled = False
        If Not RecipeEdit.Editable Then
            BtnReadOnlyBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            btnSetBasicPos2.Enabled = True
            Exit Sub
        End If
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetBasicPos2.Enabled = True
            Exit Sub
        End If
        txtBasicPos2X.Text = gCMotion.GetPositionValue(sys.AxisX)
        txtBasicPos2Y.Text = gCMotion.GetPositionValue(sys.AxisY)
        txtBasicPos2Z.Text = gCMotion.GetPositionValue(sys.AxisZ)

        btnSetBasicPos2.Enabled = True
    End Sub

    ''' <summary>移動到(Golden Sample)Pattern基準點</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnMoveBasicPos_Click(sender As Object, e As EventArgs) Handles btnMoveBasicPos.Click

        Dim tmpPos As New AlignmentStructure
        tmpPos.AlignPosX = Val(txtBasicPosX.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionX
        tmpPos.AlignPosY = Val(txtBasicPosY.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionY
        tmpPos.AlignPosZ = Val(txtBasicPosZ.Text) '  Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionZ

        If GoCCDButtonAction(sender, tmpPos) = False Then
            Exit Sub
        End If

        'Dim btn As Button = sender
        'gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
        ''gSyslog.Save("[frmRecipe01]" & vbTab & "[btnMoveBasicPos]" & vbTab & "Click")
        ''btnMoveBasicPos.Enabled = False
        ''20170602按鍵保護
        'Btn_Control(False)
        'If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '    BtnHomeFirstBehavior(sender)
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnMoveBasicPos.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If
        'If Recipe Is Nothing Then
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnMoveBasicPos.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If

        ''[說明]:X、Y、Z軸
        'gCMotion.SetVelAccDec(sys.AxisX)
        'gCMotion.SetVelAccDec(sys.AxisY)
        'gCMotion.SetVelAccDec(sys.AxisZ)
        'gCMotion.SetVelAccDec(sys.AxisB)
        'gCMotion.SetVelAccDec(sys.AxisC)


        'Dim AxisNo(4) As Integer
        'Dim TargetPos(4) As Decimal
        'AxisNo(0) = sys.AxisX
        'AxisNo(1) = sys.AxisY
        'AxisNo(2) = sys.AxisZ
        'AxisNo(3) = sys.AxisB
        'AxisNo(4) = sys.AxisC

        'Select Case gSSystemParameter.CCDModuleType
        '    Case enmCCDModule.eFix
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = 0
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0

        '    Case enmCCDModule.eFree
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionZ
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0
        'End Select
        'ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        'UcJoyStick1.RefreshPosition()
        ''btnMoveBasicPos.Enabled = True
        ''20170602按鍵保護
        'Btn_Control(True)
    End Sub


    Private Sub btnMoveBasicPos2_Click(sender As Object, e As EventArgs) Handles btnMoveBasicPos2.Click
        Dim tmpPos As New AlignmentStructure
        tmpPos.AlignPosX = Val(txtBasicPos2X.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionX
        tmpPos.AlignPosY = Val(txtBasicPos2Y.Text) '  Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionY
        tmpPos.AlignPosZ = Val(txtBasicPos2Z.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionZ

        If GoCCDButtonAction(sender, tmpPos) = False Then
            Exit Sub
        End If

        'Dim btn As Button = sender
        'gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
        ''gSyslog.Save("[frmRecipe01]" & vbTab & "[btnMoveBasicPos2]" & vbTab & "Click")
        ''btnMoveBasicPos.Enabled = False
        ''20170602按鍵保護
        'Btn_Control(False)
        'If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '    BtnHomeFirstBehavior(sender)
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnMoveBasicPos2.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If
        'If Recipe Is Nothing Then
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnMoveBasicPos2.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If

        ''[說明]:X、Y、Z軸
        'gCMotion.SetVelAccDec(sys.AxisX)
        'gCMotion.SetVelAccDec(sys.AxisY)
        'gCMotion.SetVelAccDec(sys.AxisZ)
        'gCMotion.SetVelAccDec(sys.AxisB)
        'gCMotion.SetVelAccDec(sys.AxisC)


        'Dim AxisNo(4) As Integer
        'Dim TargetPos(4) As Decimal
        'AxisNo(0) = sys.AxisX
        'AxisNo(1) = sys.AxisY
        'AxisNo(2) = sys.AxisZ
        'AxisNo(3) = sys.AxisB
        'AxisNo(4) = sys.AxisC

        'Select Case gSSystemParameter.CCDModuleType
        '    Case enmCCDModule.eFix
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = 0
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0

        '    Case enmCCDModule.eFree
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionZ
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0
        'End Select
        'ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        'UcJoyStick1.RefreshPosition()
        ''btnMoveBasicPos2.Enabled = True
        ''20170602按鍵保護
        'Btn_Control(True)

    End Sub


#End Region


#Region "是否使用定位或不生產點"

    Private Sub chkAlignmentEnable_CheckedChanged(sender As Object, e As EventArgs) Handles chkAlignmentEnable.CheckedChanged
        If RecipeEdit.Editable Then
            RecipeEdit.Node(sys.StageNo)(NodeID).AlignmentEnable = chkAlignmentEnable.Checked
            chkAlignmentEnable2.CheckState = chkAlignmentEnable.CheckState
        End If
    End Sub

    Private Sub chkAlignmentEnable2_CheckedChanged(sender As Object, e As EventArgs) Handles chkAlignmentEnable2.CheckedChanged
        If RecipeEdit.Editable Then
            RecipeEdit.Node(sys.StageNo)(NodeID).AlignmentEnable = chkAlignmentEnable2.Checked
            chkAlignmentEnable.CheckState = chkAlignmentEnable2.CheckState
        End If
    End Sub

    Private Sub chkSkipMarkEnable_CheckedChanged(sender As Object, e As EventArgs) Handles chkSkipMarkEnable.CheckedChanged
        If RecipeEdit.Editable Then
            RecipeEdit.Node(sys.StageNo)(NodeID).SkipMarkEnable = chkSkipMarkEnable.Checked
            chkSkipMarkEnable2.CheckState = chkSkipMarkEnable.CheckState
        End If
    End Sub

    Private Sub chkSkipMarkEnable2_CheckedChanged(sender As Object, e As EventArgs) Handles chkSkipMarkEnable2.CheckedChanged
        If RecipeEdit.Editable Then
            RecipeEdit.Node(sys.StageNo)(NodeID).SkipMarkEnable = chkSkipMarkEnable2.Checked
            chkSkipMarkEnable.CheckState = chkSkipMarkEnable2.CheckState
        End If
    End Sub

#End Region


#Region "Align場景動作"
#Region "AlignType"

    Private Function SetAlignTypeAction(ByVal btn As Button) As Boolean
        'Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Return False
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")


        If Not RecipeEdit.Editable Then
            BtnReadOnlyBehavior(btn)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            Return False
        End If
        If NodeID = "" Then
            'Node 不存在,請先選擇Node
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
            MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If

        btn.Enabled = False
        Dim mSceneName As String
        Select Case RecipeEdit.Node(sys.StageNo)(NodeID).AlignType
            Case enmAlignType.DevicePos1
                RecipeEdit.Node(sys.StageNo)(NodeID).AlignType = ProjectRecipe.enmAlignType.DevicePos2
                mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene
                If mSceneName <> "" Then
                    SetCmbSceneID(cmbSceneID2, mSceneName)
                End If

                mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
                If mSceneName <> "" Then
                    SetCmbSceneID(cmbSceneID, mSceneName)
                End If

            Case enmAlignType.DevicePos2
                RecipeEdit.Node(sys.StageNo)(NodeID).AlignType = ProjectRecipe.enmAlignType.DevicePos1
                mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
                If mSceneName <> "" Then
                    SetCmbSceneID(cmbSceneID, mSceneName)
                End If

            Case enmAlignType.DevicePos3
                RecipeEdit.Node(sys.StageNo)(NodeID).AlignType = ProjectRecipe.enmAlignType.DevicePos1
                mSceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
                If mSceneName <> "" Then
                    SetCmbSceneID(cmbSceneID, mSceneName)
                End If

        End Select

        For i As Integer = 0 To ProjectRecipe.enmAlignType.Max
            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count > RecipeEdit.Node(sys.StageNo)(NodeID).AlignType Then
                Exit For
            Else
                RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Add(New AlignmentStructure)
            End If
        Next

        RefreshCCDButtonPos(conveyorNo)
        btn.Enabled = True
        Return True
    End Function

    Private Sub btnAlignType_Click(sender As Object, e As EventArgs) Handles btnAlignType.Click

        If SetAlignTypeAction(sender) = False Then
            Exit Sub
        End If

        'Dim btn As Button = sender
        'gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
        ''gSyslog.Save("[frmRecipe01]" & vbTab & "[btnAlignType]" & vbTab & "Click")
        'If btn.Enabled = False Then '[Note]防連按
        '    Exit Sub
        'End If
        'btnAlignType.Enabled = False
        'If Not Recipe.Editable Then
        '    BtnReadOnlyBehavior(sender)
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    btnAlignType.Enabled = True
        '    Exit Sub
        'End If
        'If NodeID = "" Then
        '    'Node 不存在,請先選擇Node
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
        '    MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    btnAlignType.Enabled = True
        '    Exit Sub
        'End If
        'Dim mSceneName As String
        'Select Case Recipe.Node(sys.StageNo)(NodeID).AlignType
        '    Case enmAlignType.DevicePos1
        '        Recipe.Node(sys.StageNo)(NodeID).AlignType = ProjectRecipe.enmAlignType.DevicePos2
        '        mSceneName = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene
        '        If mSceneName <> "" Then
        '            SetCmbSceneID(cmbSceneID2, mSceneName)
        '        End If

        '        mSceneName = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
        '        If mSceneName <> "" Then
        '            SetCmbSceneID(cmbSceneID, mSceneName)
        '        End If

        '    Case enmAlignType.DevicePos2
        '        Recipe.Node(sys.StageNo)(NodeID).AlignType = ProjectRecipe.enmAlignType.DevicePos1
        '        mSceneName = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
        '        If mSceneName <> "" Then
        '            SetCmbSceneID(cmbSceneID, mSceneName)
        '        End If

        '    Case enmAlignType.DevicePos3
        '        Recipe.Node(sys.StageNo)(NodeID).AlignType = ProjectRecipe.enmAlignType.DevicePos1
        '        mSceneName = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
        '        If mSceneName <> "" Then
        '            SetCmbSceneID(cmbSceneID, mSceneName)
        '        End If

        'End Select

        'For i As Integer = 0 To ProjectRecipe.enmAlignType.Max
        '    If Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count > Recipe.Node(sys.StageNo)(NodeID).AlignType Then
        '        Exit For
        '    Else
        '        Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Add(New AlignmentStructure)
        '    End If
        'Next

        'RefreshCCDButtonPos(conveyorNo)
        'btnAlignType.Enabled = True
    End Sub

    Private Sub btnAlignType2_Click(sender As Object, e As EventArgs) Handles btnAlignType2.Click
        If SetAlignTypeAction(sender) = False Then
            Exit Sub
        End If
        'Dim btn As Button = sender
        'gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
        ''gSyslog.Save("[frmRecipe01]" & vbTab & "[btnAlignType2]" & vbTab & "Click")
        'If btn.Enabled = False Then '[Note]防連按
        '    Exit Sub
        'End If
        'btnAlignType2.Enabled = False
        'If Not Recipe.Editable Then
        '    BtnReadOnlyBehavior(sender)
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    btnAlignType2.Enabled = True
        '    Exit Sub
        'End If
        'If NodeID = "" Then
        '    'Node 不存在,請先選擇Node
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
        '    MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    btnAlignType2.Enabled = True
        '    Exit Sub
        'End If
        'Dim mSceneName As String
        'Select Case Recipe.Node(sys.StageNo)(NodeID).AlignType
        '    Case enmAlignType.DevicePos1
        '        Recipe.Node(sys.StageNo)(NodeID).AlignType = ProjectRecipe.enmAlignType.DevicePos2
        '        mSceneName = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene
        '        If mSceneName <> "" Then
        '            SetCmbSceneID(cmbScene2ID2, mSceneName)
        '        End If

        '        mSceneName = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
        '        If mSceneName <> "" Then
        '            SetCmbSceneID(cmbScene2ID, mSceneName)
        '        End If

        '    Case enmAlignType.DevicePos2
        '        Recipe.Node(sys.StageNo)(NodeID).AlignType = ProjectRecipe.enmAlignType.DevicePos1
        '        mSceneName = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
        '        If mSceneName <> "" Then
        '            SetCmbSceneID(cmbScene2ID, mSceneName)
        '        End If

        '    Case enmAlignType.DevicePos3
        '        Recipe.Node(sys.StageNo)(NodeID).AlignType = ProjectRecipe.enmAlignType.DevicePos1
        '        mSceneName = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
        '        If mSceneName <> "" Then
        '            SetCmbSceneID(cmbScene2ID, mSceneName)
        '        End If

        'End Select

        'For i As Integer = 0 To ProjectRecipe.enmAlignType.Max
        '    If Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count > Recipe.Node(sys.StageNo)(NodeID).AlignType Then
        '        Exit For
        '    Else
        '        Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Add(New AlignmentStructure)
        '    End If
        'Next

        'RefreshCCDButtonPos(conveyorNo)
        'btnAlignType2.Enabled = True
    End Sub

#End Region

#Region "SetCCDPos"
    Private Function SetCCDPosAction(ByVal btn As Button, ByRef AlignPos As AlignmentStructure, ByRef CCDPosX As TextBox, ByRef CCDPosY As TextBox, ByRef CCDPosZ As TextBox) As Boolean
        'Dim btn As Button = sender
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
        If btn.Enabled = False Then '[Note]防連按
            Return False
        End If
        If Not RecipeEdit.Editable Then
            BtnReadOnlyBehavior(btn)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            btn.Enabled = True
            Return False
        End If


        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btn.Enabled = True
            Return False
        End If
        'Select Case gSSystemParameter.MachineType
        '    Case enmMachineType.DCSW_800AQ
        '        If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish And gSYS(eSys.MachineB).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
        '            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            btn.Enabled = True
        '            Return False
        '        End If

        '    Case Else
        '        If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
        '            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            btn.Enabled = True
        '            Return False
        '        End If
        'End Select


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
            btn.Enabled = True
            Return False
        End If

        CCDPosX.Text = gCMotion.GetPositionValue(sys.AxisX) '- mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex) ' cmbArrayX.SelectedIndex * Recipe.Node(sys.StageNo)(NodeID).Array.PitchX
        CCDPosY.Text = gCMotion.GetPositionValue(sys.AxisY) '- mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex) 'cmbArrayY.SelectedIndex * Recipe.Node(sys.StageNo)(NodeID).Array.PitchY
        CCDPosZ.Text = gCMotion.GetPositionValue(sys.AxisZ)

        Dim tempAlignment As New AlignmentStructure

        tempAlignment = AlignPos

        tempAlignment.AlignPosX = Val(txtCCDPosX.Text)
        tempAlignment.AlignPosY = Val(txtCCDPosY.Text)
        tempAlignment.AlignPosZ = Val(txtCCDPosZ.Text)
        tempAlignment.AlignOffsetX = 0
        tempAlignment.AlignOffsetY = 0
        tempAlignment.AlignRoation = 0

        RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0) = tempAlignment

        Dim levelNo As Integer
        If GetNodeLevel(NodeID, levelNo) = True Then
            If levelNo > 1 Then 'Soni + 2016.09.14 計算相對偏移量
                Dim parentNodeID As String = "" 'NodeID.Substring(0, NodeID.Length - 4) '取父節點名稱
                If RecipeEdit.GetParentNodeID(NodeID, parentNodeID) = True Then 'Soni + 2017.03.05 修正名稱過長錯誤
                    RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).ParentAlignAlignOffsetX = RecipeEdit.Node(sys.StageNo)(parentNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX - RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX
                    RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).ParentAlignAlignOffsetY = RecipeEdit.Node(sys.StageNo)(parentNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY - RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY
                    RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).ParentAlignAlignOffsetZ = RecipeEdit.Node(sys.StageNo)(parentNodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ - RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ

                    RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignBasicOffsetX = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX - RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionX
                    RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignBasicOffsetY = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY - RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionY
                    RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignBasicOffsetZ = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ - RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionZ
                End If

            Else
                RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).ParentAlignAlignOffsetX = -RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX
                RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).ParentAlignAlignOffsetY = -RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY
                RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).ParentAlignAlignOffsetZ = -RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ

                RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignBasicOffsetX = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX - RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionX
                RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignBasicOffsetY = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY - RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionY
                RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignBasicOffsetZ = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ - RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionZ
            End If
        End If

        SetCmbSceneID(cmbSceneID, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene)

        btn.Enabled = True
        Return True
    End Function

    Private Sub btnSetCcdPosX_Click(sender As Object, e As EventArgs) Handles btnSetCcdPosX.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnSetCcdPosX.Enabled = False
        If Not RecipeEdit.Editable Then
            BtnReadOnlyBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            btnSetCcdPosX.Enabled = True
            Exit Sub
        End If

        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 1 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetCcdPosX.Enabled = True
            Exit Sub
        End If

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish And gSYS(eSys.MachineB).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetCcdPosX.Enabled = True
                    Exit Sub
                End If

            Case Else
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetCcdPosX.Enabled = True
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
            btnSetCcdPosX.Enabled = True
            Exit Sub
        End If

        txtCCDPosX.Text = gCMotion.GetPositionValue(sys.AxisX)
        txtCCDPosY.Text = gCMotion.GetPositionValue(sys.AxisY)
        txtCCDPosZ.Text = gCMotion.GetPositionValue(sys.AxisZ)

        btnSetCcdPosX.Enabled = True
    End Sub

    Private Sub btnSetCcdPosX2_Click(sender As Object, e As EventArgs) Handles btnSetCcdPosX2.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnSetCcdPosX2.Enabled = False
        If Not RecipeEdit.Editable Then
            BtnReadOnlyBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            btnSetCcdPosX2.Enabled = True
            Exit Sub
        End If
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 2 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetCcdPosX2.Enabled = True
            Exit Sub
        End If
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish And gSYS(eSys.MachineB).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetCcdPosX2.Enabled = True
                    Exit Sub
                End If

            Case Else
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetCcdPosX2.Enabled = True
                    Exit Sub
                End If

        End Select

        '[Note]如果和第一定位點位置相同，結束
        Dim AlignNo01, AlignNo02 As Premtek.sPos
        AlignNo01.PosX = Val(txtCCDPosX.Text)
        AlignNo01.PosY = Val(txtCCDPosY.Text)
        AlignNo02.PosX = gCMotion.GetPositionValue(sys.AxisX)
        AlignNo02.PosY = gCMotion.GetPositionValue(sys.AxisY)
        If IsSamePos(AlignNo01, AlignNo02) = True Then
            btnSetCcdPosX2.Enabled = True
            Exit Sub
        End If
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
            btnSetCcdPosX2.Enabled = True
            Exit Sub
        End If
        txtCCDPosX2.Text = gCMotion.GetPositionValue(sys.AxisX)
        txtCCDPosY2.Text = gCMotion.GetPositionValue(sys.AxisY)

        btnSetCcdPosX2.Enabled = True
    End Sub

    Private Sub btnSetCCDPos3_Click(sender As Object, e As EventArgs) Handles btnSetCCDPosX3.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")


        btnSetCCDPosX3.Enabled = False
        If Not RecipeEdit.Editable Then
            BtnReadOnlyBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            btnSetCCDPosX3.Enabled = True
            Exit Sub
        End If

        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetCCDPosX3.Enabled = True
            Exit Sub
        End If

        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 3 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetCCDPosX3.Enabled = True
            Exit Sub
        End If

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
            btnSetCCDPosX3.Enabled = True
            Exit Sub
        End If

        txtCCDPosX3.Text = gCMotion.GetPositionValue(sys.AxisX)
        txtCCDPosY3.Text = gCMotion.GetPositionValue(sys.AxisY)

        btnSetCCDPosX3.Enabled = True
    End Sub

    Private Sub btnSetCcd2PosX_Click(sender As Object, e As EventArgs) Handles btnSetCcd2PosX.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnSetCcd2PosX.Enabled = False
        If Not RecipeEdit.Editable Then
            BtnReadOnlyBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            btnSetCcd2PosX.Enabled = True
            Exit Sub
        End If

        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 1 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetCcd2PosX.Enabled = True
            Exit Sub
        End If
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish And gSYS(eSys.MachineB).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetCcd2PosX.Enabled = True
                    Exit Sub
                End If

            Case Else
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetCcd2PosX.Enabled = True
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
            btnSetCcd2PosX.Enabled = True
            Exit Sub
        End If

        Dim mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(sys.StageNo)(NodeID).Array)
        txtCCD2PosX.Text = gCMotion.GetPositionValue(sys.AxisX)
        txtCCD2PosY.Text = gCMotion.GetPositionValue(sys.AxisY)
        txtCCD2PosZ.Text = gCMotion.GetPositionValue(sys.AxisZ)

        btnSetCcd2PosX.Enabled = True

    End Sub

    Private Sub btnSetCcd2PosX2_Click(sender As Object, e As EventArgs) Handles btnSetCcd2PosX2.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnSetCcd2PosX2.Enabled = False
        If Not RecipeEdit.Editable Then
            BtnReadOnlyBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            btnSetCcd2PosX2.Enabled = True
            Exit Sub
        End If
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 2 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetCcd2PosX2.Enabled = True
            Exit Sub
        End If
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish And gSYS(eSys.MachineB).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetCcd2PosX2.Enabled = True
                    Exit Sub
                End If

            Case Else
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetCcd2PosX2.Enabled = True
                    Exit Sub
                End If

        End Select

        '[Note]如果和第一定位點位置相同，結束
        Dim AlignNo01, AlignNo02 As Premtek.sPos
        AlignNo01.PosX = Val(txtCCD2PosX.Text)
        AlignNo01.PosY = Val(txtCCD2PosY.Text)
        AlignNo02.PosX = gCMotion.GetPositionValue(sys.AxisX)
        AlignNo02.PosY = gCMotion.GetPositionValue(sys.AxisY)
        If IsSamePos(AlignNo01, AlignNo02) = True Then
            btnSetCcd2PosX2.Enabled = True
            Exit Sub
        End If

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
            btnSetCcd2PosX2.Enabled = True
            Exit Sub
        End If

        txtCCD2PosX2.Text = gCMotion.GetPositionValue(sys.AxisX)
        txtCCD2PosY2.Text = gCMotion.GetPositionValue(sys.AxisY)

        btnSetCcd2PosX2.Enabled = True
    End Sub

    Private Sub btnSetCcd2PosX3_Click(sender As Object, e As EventArgs) Handles btnSetCcd2PosX3.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")


        btnSetCcd2PosX3.Enabled = False
        If Not RecipeEdit.Editable Then
            BtnReadOnlyBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            btnSetCcd2PosX3.Enabled = True
            Exit Sub
        End If

        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetCcd2PosX3.Enabled = True
            Exit Sub
        End If

        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 3 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSetCcd2PosX3.Enabled = True
            Exit Sub
        End If

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
            btnSetCcd2PosX3.Enabled = True
            Exit Sub
        End If

        txtCCD2PosX3.Text = gCMotion.GetPositionValue(sys.AxisX)
        txtCCD2PosY3.Text = gCMotion.GetPositionValue(sys.AxisY)

        btnSetCcd2PosX3.Enabled = True
    End Sub


#End Region

#Region "GoCCDPos"

    Public Function CheckGoPos(ByVal PosX As Decimal, ByVal PosY As Decimal, ByVal PosZ As Decimal) As Boolean
        If PosX < gCMotion.AxisParameter(sys.AxisX).Limit.NegativeLimit Or PosX > gCMotion.AxisParameter(sys.AxisX).Limit.PosivtiveLimit Or
           PosY < gCMotion.AxisParameter(sys.AxisY).Limit.NegativeLimit Or PosY > gCMotion.AxisParameter(sys.AxisY).Limit.PosivtiveLimit Or
           PosZ < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Or PosZ > gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function GoCCDButtonAction(ByVal btn As Button, ByVal AlignPos As AlignmentStructure) As Boolean
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
        If RecipeEdit Is Nothing Then
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
                TargetPos(0) = AlignPos.AlignPosX '+ mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
                TargetPos(1) = AlignPos.AlignPosY '+ mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
                TargetPos(2) = 0
                TargetPos(3) = 0
                TargetPos(4) = 0

            Case enmCCDModule.eFree
                TargetPos(0) = AlignPos.AlignPosX '+ mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
                TargetPos(1) = AlignPos.AlignPosY '+ mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
                TargetPos(2) = AlignPos.AlignPosZ
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
        Dim tmpPos As New AlignmentStructure
        tmpPos.AlignPosX = Val(txtCCDPosX.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX
        tmpPos.AlignPosY = Val(txtCCDPosY.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY
        tmpPos.AlignPosZ = Val(txtCCDPosZ.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
        If GoCCDButtonAction(sender, tmpPos) = False Then
            Exit Sub
        End If
        'Dim btn As Button = sender
        'gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
        'If btn.Enabled = False Then '[Note]防連按
        '    Exit Sub
        'End If

        'btnGoCCD.Enabled = False
        ''20170602按鍵保護
        'Btn_Control(False)
        'If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '    BtnHomeFirstBehavior(sender)
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    btnGoCCD.Enabled = True
        '    Exit Sub
        'End If
        'If Recipe Is Nothing Then
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnGoCCD.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If

        ''[說明]:X、Y、Z軸
        'If sys.AxisX > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisX)
        'End If
        'If sys.AxisY > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisY)
        'End If
        'If sys.AxisZ > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisZ)
        'End If
        'If sys.AxisB > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisB)
        'End If
        'If sys.AxisC > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisC)
        'End If

        'Dim AxisNo(4) As Integer
        'Dim TargetPos(4) As Decimal
        'AxisNo(0) = sys.AxisX
        'AxisNo(1) = sys.AxisY
        'AxisNo(2) = sys.AxisZ
        'AxisNo(3) = sys.AxisB
        'AxisNo(4) = sys.AxisC

        'If Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 1 Then
        '    '定位資料不存在
        '    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
        '    MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    'btnGoCCD.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If

        'Select Case gSSystemParameter.CCDModuleType
        '    Case enmCCDModule.eFix
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = 0
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0

        '    Case enmCCDModule.eFree
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0
        'End Select
        'ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        'UcJoyStick1.RefreshPosition()
        ''btnGoCCD.Enabled = True
        ''20170602按鍵保護
        'Btn_Control(True)
    End Sub

    Private Sub btnGoCCD2_Click(sender As Object, e As EventArgs) Handles btnGoCCD2.Click
        Dim tmpPos As New AlignmentStructure
        tmpPos.AlignPosX = Val(txtCCDPosX2.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosX
        tmpPos.AlignPosY = Val(txtCCDPosY2.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosY
        tmpPos.AlignPosZ = Val(txtCCDPosZ.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ

        If GoCCDButtonAction(sender, tmpPos) = False Then
            Exit Sub
        End If


        'gSyslog.Save("[frmRecipe01]" & vbTab & "[btnGoCCD2]" & vbTab & "Click")
        ''btnGoCCD2.Enabled = False
        ''20170602按鍵保護
        'Btn_Control(False)
        'If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '    BtnHomeFirstBehavior(sender)
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnGoCCD2.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If
        'If Recipe Is Nothing Then
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnGoCCD2.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If


        ''[說明]:X、Y、Z軸
        'If sys.AxisX > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisX)
        'End If
        'If sys.AxisY > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisY)
        'End If
        'If sys.AxisZ > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisZ)
        'End If
        'If sys.AxisB > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisB)
        'End If
        'If sys.AxisC > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisC)
        'End If

        'Dim AxisNo(4) As Integer
        'Dim TargetPos(4) As Decimal
        'AxisNo(0) = sys.AxisX
        'AxisNo(1) = sys.AxisY
        'AxisNo(2) = sys.AxisZ
        'AxisNo(3) = sys.AxisB
        'AxisNo(4) = sys.AxisC

        'If Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 2 Then
        '    '定位資料不存在
        '    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
        '    MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    'btnGoCCD2.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If

        'Select Case gSSystemParameter.CCDModuleType
        '    Case enmCCDModule.eFix
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = 0
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0

        '    Case enmCCDModule.eFree
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0
        'End Select
        'ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)

        'UcJoyStick1.RefreshPosition()
        ''btnGoCCD2.Enabled = True
        ''20170602按鍵保護
        'Btn_Control(True)
    End Sub

    Private Sub btnGoCCD3_Click(sender As Object, e As EventArgs) Handles btnGoCCD3.Click
        Dim tmpPos As New AlignmentStructure
        tmpPos.AlignPosX = Val(txtCCDPosX3.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosX
        tmpPos.AlignPosY = Val(txtCCDPosY3.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosY
        tmpPos.AlignPosZ = Val(txtCCDPosZ.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ

        If GoCCDButtonAction(sender, tmpPos) = False Then
            Exit Sub
        End If

        'gSyslog.Save("[frmRecipe01]" & vbTab & "[btnGoCCD3]" & vbTab & "Click")
        ''btnGoCCD3.Enabled = False
        ''20170602按鍵保護
        'Btn_Control(False)
        'If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '    BtnHomeFirstBehavior(sender)
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnGoCCD3.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If
        'If Recipe Is Nothing Then
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnGoCCD3.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If

        ''[說明]:X、Y、Z軸
        'If sys.AxisX > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisX)
        'End If
        'If sys.AxisY > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisY)
        'End If
        'If sys.AxisZ > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisZ)
        'End If
        'If sys.AxisB > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisB)
        'End If
        'If sys.AxisC > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisC)
        'End If

        'Dim AxisNo(4) As Integer
        'Dim TargetPos(4) As Decimal
        'AxisNo(0) = sys.AxisX
        'AxisNo(1) = sys.AxisY
        'AxisNo(2) = sys.AxisZ
        'AxisNo(3) = sys.AxisB
        'AxisNo(4) = sys.AxisC

        'If Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 3 Then
        '    '定位資料不存在
        '    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
        '    MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    'btnGoCCD3.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If

        'Select Case gSSystemParameter.CCDModuleType
        '    Case enmCCDModule.eFix
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = 0
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0

        '    Case enmCCDModule.eFree
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0
        'End Select
        'ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        'UcJoyStick1.RefreshPosition()
        ''btnGoCCD3.Enabled = True
        ''20170602按鍵保護
        'Btn_Control(True)
    End Sub

    Private Sub btnGo2CCD_Click(sender As Object, e As EventArgs) Handles btnGo2CCD.Click
        Dim tmpPos As New AlignmentStructure
        tmpPos.AlignPosX = Val(txtCCD2PosX.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX
        tmpPos.AlignPosY = Val(txtCCD2PosX.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY
        tmpPos.AlignPosZ = Val(txtCCD2PosZ.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ

        If GoCCDButtonAction(sender, tmpPos) = False Then
            Exit Sub
        End If


        'gSyslog.Save("[frmRecipe01]" & vbTab & "[btnGo2CCD]" & vbTab & "Click")
        ''btnGo2CCD.Enabled = False
        ''20170602按鍵保護
        'Btn_Control(False)
        'If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '    BtnHomeFirstBehavior(sender)
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnGo2CCD.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If
        'If Recipe Is Nothing Then
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnGo2CCD.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If

        ''[說明]:X、Y、Z軸
        'If sys.AxisX > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisX)
        'End If
        'If sys.AxisY > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisY)
        'End If
        'If sys.AxisZ > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisZ)
        'End If
        'If sys.AxisB > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisB)
        'End If
        'If sys.AxisC > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisC)
        'End If

        'Dim AxisNo(4) As Integer
        'Dim TargetPos(4) As Decimal
        'AxisNo(0) = sys.AxisX
        'AxisNo(1) = sys.AxisY
        'AxisNo(2) = sys.AxisZ
        'AxisNo(3) = sys.AxisB
        'AxisNo(4) = sys.AxisC

        'If Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 1 Then
        '    '定位資料不存在
        '    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
        '    MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    'btnGo2CCD.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If

        'Select Case gSSystemParameter.CCDModuleType
        '    Case enmCCDModule.eFix
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = 0
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0

        '    Case enmCCDModule.eFree
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0
        'End Select
        'ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        'UcJoyStick1.RefreshPosition()
        ''btnGo2CCD.Enabled = True
        ''20170602按鍵保護
        'Btn_Control(True)
    End Sub

    Private Sub btnGo2CCD2_Click(sender As Object, e As EventArgs) Handles btnGo2CCD2.Click

        Dim tmpPos As New AlignmentStructure
        tmpPos.AlignPosX = Val(txtCCD2PosX.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosX
        tmpPos.AlignPosY = Val(txtCCD2PosY.Text) 'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosY
        tmpPos.AlignPosZ = Val(txtCCD2PosZ.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ

        If GoCCDButtonAction(sender, tmpPos) = False Then
            Exit Sub
        End If


        'gSyslog.Save("[frmRecipe01]" & vbTab & "[btnGo2CCD2]" & vbTab & "Click")
        ''btnGo2CCD2.Enabled = False
        ''20170602按鍵保護
        'Btn_Control(False)
        'If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '    BtnHomeFirstBehavior(sender)
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnGo2CCD2.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If
        'If Recipe Is Nothing Then
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnGo2CCD2.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If


        ''[說明]:X、Y、Z軸
        'If sys.AxisX > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisX)
        'End If
        'If sys.AxisY > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisY)
        'End If
        'If sys.AxisZ > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisZ)
        'End If
        'If sys.AxisB > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisB)
        'End If
        'If sys.AxisC > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisC)
        'End If

        'Dim AxisNo(4) As Integer
        'Dim TargetPos(4) As Decimal
        'AxisNo(0) = sys.AxisX
        'AxisNo(1) = sys.AxisY
        'AxisNo(2) = sys.AxisZ
        'AxisNo(3) = sys.AxisB
        'AxisNo(4) = sys.AxisC

        'If Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 2 Then
        '    '定位資料不存在
        '    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
        '    MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    'btnGo2CCD2.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If

        'Select Case gSSystemParameter.CCDModuleType
        '    Case enmCCDModule.eFix
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = 0
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0

        '    Case enmCCDModule.eFree
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0
        'End Select
        'ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)

        'UcJoyStick1.RefreshPosition()
        ''btnGo2CCD2.Enabled = True
        ''20170602按鍵保護
        'Btn_Control(True)
    End Sub

    Private Sub btnGo2CCD3_Click(sender As Object, e As EventArgs) Handles btnGo2CCD3.Click
        Dim tmpPos As New AlignmentStructure
        tmpPos.AlignPosX = Val(txtCCD2PosX3.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosX
        tmpPos.AlignPosY = Val(txtCCD2PosY3.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosY
        tmpPos.AlignPosZ = Val(txtCCD2PosZ.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ

        If GoCCDButtonAction(sender, tmpPos) = False Then
            Exit Sub
        End If

        'gSyslog.Save("[frmRecipe01]" & vbTab & "[btnGo2CCD3]" & vbTab & "Click")
        ''btnGo2CCD3.Enabled = False
        ''20170602按鍵保護
        'Btn_Control(False)
        'If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '    BtnHomeFirstBehavior(sender)
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnGo2CCD3.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If
        'If Recipe Is Nothing Then
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnGo2CCD3.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If

        ''[說明]:X、Y、Z軸
        'If sys.AxisX > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisX)
        'End If
        'If sys.AxisY > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisY)
        'End If
        'If sys.AxisZ > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisZ)
        'End If
        'If sys.AxisB > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisB)
        'End If
        'If sys.AxisC > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisC)
        'End If

        'Dim AxisNo(4) As Integer
        'Dim TargetPos(4) As Decimal
        'AxisNo(0) = sys.AxisX
        'AxisNo(1) = sys.AxisY
        'AxisNo(2) = sys.AxisZ
        'AxisNo(3) = sys.AxisB
        'AxisNo(4) = sys.AxisC

        'If Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 3 Then
        '    '定位資料不存在
        '    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
        '    MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    'btnGo2CCD3.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If

        'Select Case gSSystemParameter.CCDModuleType
        '    Case enmCCDModule.eFix
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = 0
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0

        '    Case enmCCDModule.eFree
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0
        'End Select
        'ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        'UcJoyStick1.RefreshPosition()
        ''btnGo2CCD3.Enabled = True
        ''20170602按鍵保護
        'Btn_Control(True)
    End Sub
#End Region

#Region "Train"
    Private Function TrainSceneAction(ByVal btn As Button, ByVal mScene As String, ByVal cmb As ComboBox) As Boolean
        '[Note]模組化測試中----------
        'Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Return False
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        If Not RecipeEdit.Editable Then
            BtnReadOnlyBehavior(btn)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            Return False
        End If
        If NodeID = "" Then
            '請先選擇Node
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
            MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If

        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 1 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If

        btn.Enabled = False
        'Dim mScene As String = ""
        If gAOICollection.SetCCDScene(sys.CCDNo, mScene) = False Then
            MsgBox("Scene (" & mScene & ") Not Exists!")
        End If

        Dim mfrmAlignPR01 As frmAlignModule
        Try
            mfrmAlignPR01 = New frmAlignModule
            With mfrmAlignPR01
                .Sys = sys
                .SceneName = mScene 'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
                .IsRecipeScene = True
                .StartPosition = FormStartPosition.Manual
                .Location = New Point(0, 0)
                .ShowDialog()
                '[Note]:回復原始設定
                'cmb.Items.Clear()
                'cmb.Items.AddRange(gAOICollection.GetSceneList)
                gAOICollection.LoadSceneList("") '[Note]更新場景清單
                RefreshScene() '[Note]更新所有場景控制項
                If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                    cmb.SelectedItem = .RecipeSceneName
                    UpdateUcLightData(.RecipeSceneName) '[Note]更新光源控制項顯示
                End If
            End With
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
        btn.Enabled = True
        '[Note]模組化測試中----------
        Return True
    End Function



    Private Sub btnTrainScene_Click(sender As Object, e As EventArgs) Handles btnTrainScene.Click

        If gSSystemParameter.CCDAlignModuleEnable Then
            If TrainSceneAction(btnTrainScene, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene, cmbSceneID) = False Then
                Exit Sub
            End If
        Else
            Dim mfrmAlignPR01 As frmAlignPR01
            gSyslog.Save("[frmRecipe01]" & vbTab & "[btnTrainScene]" & vbTab & "Click")
            If btnTrainScene.Enabled = False Then
                Exit Sub
            End If

            btnTrainScene.Enabled = False
            If Not RecipeEdit.Editable Then
                BtnReadOnlyBehavior(sender)
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
                btnTrainScene.Enabled = True
                Exit Sub
            End If
            If NodeID = "" Then
                '請先選擇Node
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
                MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnTrainScene.Enabled = True
                Exit Sub
            End If

            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 1 Then
                '定位資料不存在
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
                MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MsgBox("Alignment Data Not Exists!")
                btnTrainScene.Enabled = True
                Exit Sub
            End If
            If gAOICollection.SetCCDScene(sys.CCDNo, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene) = False Then
                MsgBox("Scene (" & RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene & ") Not Exists!")

                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbSceneID.Items.Clear()
                    cmbSceneID.Items.AddRange(gAOICollection.GetSceneList)
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbSceneID.SelectedItem = .RecipeSceneName
                    End If
                End With
                btnTrainScene.Enabled = True
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

                    btnTrainScene.Enabled = True
                    Exit Sub
                End If
            Loop Until Not gAOICollection.IsCCDCBusy(sys.CCDNo) '取像完成
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉


            Try
                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbSceneID.Items.Clear()
                    cmbSceneID.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbSceneID.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbSceneID.SelectedItem = .RecipeSceneName
                    End If
                End With
            Catch ex As Exception
                MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Try
            btnTrainScene.Enabled = True

        End If


    End Sub

    Private Sub btnTrainScene2_Click(sender As Object, e As EventArgs) Handles btnTrainScene2.Click
        If gSSystemParameter.CCDAlignModuleEnable Then
            If TrainSceneAction(btnTrainScene2, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene, cmbSceneID2) = False Then
                Exit Sub
            End If
        Else
            Dim mfrmAlignPR01 As frmAlignPR01
            gSyslog.Save("[frmRecipe01]" & vbTab & "[btnTrainScene2]" & vbTab & "Click")
            If btnTrainScene2.Enabled = False Then
                Exit Sub
            End If
            btnTrainScene2.Enabled = False 'Soni / 2016.09.09 修正教導防連按錯誤
            If Not RecipeEdit.Editable Then
                BtnReadOnlyBehavior(sender)
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
                btnTrainScene2.Enabled = True
                Exit Sub
            End If
            If NodeID = "" Then
                '請先選擇Node
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
                MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MsgBox("Select Node First!")
                btnTrainScene2.Enabled = True
                Exit Sub
            End If
            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 2 Then
                '定位資料不存在
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
                MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MsgBox("Alignment Data Not Exists!")
                btnTrainScene2.Enabled = True
                Exit Sub
            End If
            If gAOICollection.SetCCDScene(sys.CCDNo, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene) = False Then
                '場景不存在
                gSyslog.Save(RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene & gMsgHandler.GetMessage(Warn_3000020))
                MsgBox(RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene & gMsgHandler.GetMessage(Warn_3000020))
                'MsgBox("Scene (" & Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene & ") Not Exists!")

                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbSceneID2.Items.Clear()
                    cmbSceneID2.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbSceneID2.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbSceneID2.SelectedItem = .RecipeSceneName
                    End If
                End With
                btnTrainScene2.Enabled = True
                Exit Sub
            End If
            gAOICollection.SetCCDScene(sys.CCDNo, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene)
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

                    btnTrainScene2.Enabled = True
                    Exit Sub
                End If
            Loop Until Not gAOICollection.IsCCDCBusy(sys.CCDNo) '取像完成
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉

            'If gAOICollection.SetAlignImage(sys.CCDNo) = False Then
            '    btnTrainScene2.Enabled = True
            '    Exit Sub
            'End If

            Try
                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbSceneID2.Items.Clear()
                    cmbSceneID2.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbSceneID2.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbSceneID2.SelectedItem = .RecipeSceneName
                    End If
                End With
            Catch ex As Exception 'Soni + 2016.09.13 工具例外跳脫保護
                MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Try
            btnTrainScene2.Enabled = True
        End If


    End Sub

    Private Sub btnTrainScene3_Click(sender As Object, e As EventArgs) Handles btnTrainScene3.Click
        If gSSystemParameter.CCDAlignModuleEnable Then
            If TrainSceneAction(btnTrainScene3, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene, cmbSceneID3) Then
                Exit Sub
            End If

        Else
            Dim mfrmAlignPR01 As frmAlignPR01
            gSyslog.Save("[frmRecipe01]" & vbTab & "[btnTrainScene3]" & vbTab & "Click")
            If btnTrainScene3.Enabled = False Then
                Exit Sub
            End If
            btnTrainScene3.Enabled = False
            If Not RecipeEdit.Editable Then
                BtnReadOnlyBehavior(sender)
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
                btnTrainScene3.Enabled = True
                Exit Sub
            End If
            If NodeID = "" Then
                '請先選擇Node
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
                MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MsgBox("Select Node First!")
                btnTrainScene3.Enabled = True
                Exit Sub
            End If
            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 3 Then
                '定位資料不存在
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
                MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MsgBox("Alignment Data Not Exists!")
                btnTrainScene3.Enabled = True
                Exit Sub
            End If
            If gAOICollection.SetCCDScene(sys.CCDNo, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene) = False Then
                '請選擇場景
                gSyslog.Save(RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene & gMsgHandler.GetMessage(Warn_3000015))
                MsgBox(RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene & gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MsgBox("Scene (" & Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene & ") Not Exists!")

                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbSceneID3.Items.Clear()
                    cmbSceneID3.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbSceneID3.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbSceneID3.SelectedItem = .RecipeSceneName
                    End If
                End With
                btnTrainScene3.Enabled = True
                Exit Sub
            End If
            gAOICollection.SetCCDScene(sys.CCDNo, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene)
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

                    btnTrainScene3.Enabled = True
                    Exit Sub
                End If
            Loop Until Not gAOICollection.IsCCDCBusy(sys.CCDNo) '取像完成
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉

            'If gAOICollection.SetAlignImage(sys.CCDNo) = False Then
            '    btnTrainScene3.Enabled = True
            '    Exit Sub
            'End If

            Try
                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbSceneID3.Items.Clear()
                    cmbSceneID3.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbSceneID3.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbSceneID3.SelectedItem = .RecipeSceneName
                    End If
                End With
            Catch ex As Exception 'Soni + 2016.09.13 工具例外跳脫保護
                MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Try

            btnTrainScene3.Enabled = True
        End If

    End Sub

    Private Sub btnTrain2Scene_Click(sender As Object, e As EventArgs) Handles btnTrain2Scene.Click

        If gSSystemParameter.CCDAlignModuleEnable Then
            If TrainSceneAction(btnTrain2Scene, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene, cmbScene2ID) = False Then
                Exit Sub
            End If
        Else

            Dim mfrmAlignPR01 As frmAlignPR01
            gSyslog.Save("[frmRecipe01]" & vbTab & "[btnTrain2Scene]" & vbTab & "Click")
            If btnTrain2Scene.Enabled = False Then
                Exit Sub
            End If

            btnTrain2Scene.Enabled = False
            If Not RecipeEdit.Editable Then
                BtnReadOnlyBehavior(sender)
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
                btnTrain2Scene.Enabled = True
                Exit Sub
            End If
            If NodeID = "" Then
                '請先選擇Node
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
                MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnTrain2Scene.Enabled = True
                Exit Sub
            End If

            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 1 Then
                '定位資料不存在
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
                MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnTrain2Scene.Enabled = True
                Exit Sub
            End If
            If gAOICollection.SetCCDScene(sys.CCDNo, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene) = False Then
                MsgBox("Scene (" & RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene & ") Not Exists!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbScene2ID.Items.Clear()
                    cmbScene2ID.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbScene2ID.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbScene2ID.SelectedItem = .RecipeSceneName
                    End If
                End With
                btnTrain2Scene.Enabled = True
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

                    btnTrain2Scene.Enabled = True
                    Exit Sub
                End If
            Loop Until Not gAOICollection.IsCCDCBusy(sys.CCDNo) '取像完成
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉

            'Dim AlignCount As Integer = 1
            'Dim AlignSceneName() As String = gAOICollection.GetSceneList ' gAOICollection.GetAlignmentToolNameList(sys.CCDNo)
            'If Not AlignSceneName Is Nothing Then
            '    For mSceneNo As Integer = 0 To AlignSceneName.Count - 1
            '        gAOICollection.SetAlignImage(sys.CCDNo, AlignSceneName(mSceneNo))
            '    Next
            'End If
            Try
                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbScene2ID.Items.Clear()
                    cmbScene2ID.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbScene2ID.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbScene2ID.SelectedItem = .RecipeSceneName
                    End If
                End With
            Catch ex As Exception
                MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Try

            btnTrain2Scene.Enabled = True
        End If

    End Sub

    Private Sub btnTrain2Scene2_Click(sender As Object, e As EventArgs) Handles btnTrain2Scene2.Click
        If gSSystemParameter.CCDAlignModuleEnable Then
            If TrainSceneAction(btnTrain2Scene2, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene, cmbScene2ID2) = False Then
                Exit Sub
            End If
        Else

            Dim mfrmAlignPR01 As frmAlignPR01
            gSyslog.Save("[frmRecipe01]" & vbTab & "[btnTrain2Scene2]" & vbTab & "Click")

            If btnTrain2Scene2.Enabled = False Then
                Exit Sub
            End If
            btnTrain2Scene2.Enabled = False 'Soni / 2016.09.09 修正教導防連按錯誤
            If Not RecipeEdit.Editable Then
                BtnReadOnlyBehavior(sender)
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
                btnTrain2Scene2.Enabled = True
                Exit Sub
            End If
            If NodeID = "" Then
                '請先選擇Node
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
                MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnTrain2Scene2.Enabled = True
                Exit Sub
            End If
            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 2 Then
                '定位資料不存在
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
                MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnTrain2Scene2.Enabled = True
                Exit Sub
            End If
            If gAOICollection.SetCCDScene(sys.CCDNo, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene) = False Then
                MsgBox("Scene (" & RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene & ") Not Exists!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbScene2ID2.Items.Clear()
                    cmbScene2ID2.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbScene2ID2.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbScene2ID2.SelectedItem = .RecipeSceneName
                    End If
                End With
                btnTrain2Scene2.Enabled = True
                Exit Sub
            End If
            gAOICollection.SetCCDScene(sys.CCDNo, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene)
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

                    btnTrain2Scene2.Enabled = True
                    Exit Sub
                End If
            Loop Until Not gAOICollection.IsCCDCBusy(sys.CCDNo) '取像完成
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉

            'If gAOICollection.SetAlignImage(sys.CCDNo) = False Then
            '    btnTrain2Scene2.Enabled = True
            '    Exit Sub
            'End If

            Try
                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbScene2ID2.Items.Clear()
                    cmbScene2ID2.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbScene2ID2.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbScene2ID2.SelectedItem = .RecipeSceneName
                    End If
                End With
            Catch ex As Exception 'Soni + 2016.09.13 工具例外跳脫保護
                MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Try
            btnTrain2Scene2.Enabled = True

        End If

    End Sub

    Private Sub btnTrain2Scene3_Click(sender As Object, e As EventArgs) Handles btnTrain2Scene3.Click
        If gSSystemParameter.CCDAlignModuleEnable Then
            If TrainSceneAction(btnTrain2Scene3, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignScene, cmbScene2ID3) = False Then
                Exit Sub
            End If
        Else

            Dim mfrmAlignPR01 As frmAlignPR01
            gSyslog.Save("[frmRecipe01]" & vbTab & "[btnTrain2Scene3]" & vbTab & "Click")
            If btnTrain2Scene3.Enabled = False Then
                Exit Sub
            End If
            btnTrain2Scene3.Enabled = False
            If Not RecipeEdit.Editable Then
                BtnReadOnlyBehavior(sender)
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
                btnTrain2Scene3.Enabled = True
                Exit Sub
            End If
            If NodeID = "" Then
                '請先選擇Node
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
                MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnTrain2Scene3.Enabled = True
                Exit Sub
            End If
            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 3 Then
                '定位資料不存在
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
                MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnTrain2Scene3.Enabled = True
                Exit Sub
            End If
            If gAOICollection.SetCCDScene(sys.CCDNo, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene) = False Then
                '場景不存在
                gSyslog.Save(RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene & gMsgHandler.GetMessage(Warn_3000020))
                MsgBox(RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene & gMsgHandler.GetMessage(Warn_3000020))
                'MsgBox("Scene (" & Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene & ") Not Exists!")

                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbScene2ID3.Items.Clear()
                    cmbScene2ID3.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbScene2ID3.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbScene2ID3.SelectedItem = .RecipeSceneName
                    End If
                End With
                btnTrain2Scene3.Enabled = True
                Exit Sub
            End If
            gAOICollection.SetCCDScene(sys.CCDNo, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene)
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

                    btnTrain2Scene3.Enabled = True
                    Exit Sub
                End If
            Loop Until Not gAOICollection.IsCCDCBusy(sys.CCDNo) '取像完成
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉

            'If gAOICollection.SetAlignImage(sys.CCDNo) = False Then
            '    btnTrain2Scene3.Enabled = True
            '    Exit Sub
            'End If

            Try
                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbScene2ID3.Items.Clear()
                    cmbScene2ID3.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbScene2ID3.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbScene2ID3.SelectedItem = .RecipeSceneName
                    End If
                End With
            Catch ex As Exception 'Soni + 2016.09.13 工具例外跳脫保護
                MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Try

            btnTrain2Scene3.Enabled = True
        End If
    End Sub


#End Region

#Region "定位"
    Private Async Sub btnAlign_Click(sender As Object, e As EventArgs) Handles btnALign.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        '20170602按鍵保護
        Btn_Control(False)
        If cmbSceneID.SelectedItem Is Nothing Then '未選Recipe
            gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "No Scene")
            Await Task.Run(Sub()
                               cmbSceneID.BeginInvoke(Sub()
                                                          cmbSceneID.BackColor = Color.Yellow
                                                      End Sub)
                               System.Threading.Thread.Sleep(500)
                               cmbSceneID.BeginInvoke(Sub()
                                                          cmbSceneID.BackColor = Color.White
                                                      End Sub)
                           End Sub)
            'btnALign.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gAOICollection.IsCCDExist(sys.CCDNo) = False Then
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'btnALign.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If
        Dim Update As Boolean
        'If cmbArrayX.SelectedIndex = 0 And cmbArrayY.SelectedIndex = 0 Then
        Update = True
        'End If

        'If gAOICollection.SceneDictionary.ContainsKey(cmbSceneID.Text) Then
        '    SetLight(sys.CCDNo, cmbSceneID.Text)
        '    UpdateUcLightData(cmbSceneID.Text)
        'End If
        AlignSub(0, 0, cmbSceneID, Update) '[流道1,定位點1]
        ''[Note]紀錄Recipe的Golden Sample位置
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignOffsetX = AlignResult.AbsOffsetX ' gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignOffsetY = AlignResult.AbsOffsetY ' gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignRoation = AlignResult.Rotation 'gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation

        '使用定位結果計算RealBasicPosX->移動至Recipe04的align button計算
        'Dim offsetX As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).OffsetX
        'Dim offsetY As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).OffsetY
        'Dim degree As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).RealBasicPosX = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionX - offsetX
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).RealBasicPosY = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionY - offsetY
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).RealBasicPosC = degree

        'MsgBox("RealBasicPos setup OK, you can train pattern by aligned Fidicual.")

    End Sub

    Private Async Sub btnALign2_Click(sender As Object, e As EventArgs) Handles btnALign2.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        '20170602按鍵保護
        Btn_Control(False)
        If cmbSceneID2.SelectedItem Is Nothing Then '未選Recipe
            Await Task.Run(Sub()
                               cmbSceneID2.BeginInvoke(Sub()
                                                           cmbSceneID2.BackColor = Color.Yellow
                                                       End Sub)
                               System.Threading.Thread.Sleep(500)
                               cmbSceneID2.BeginInvoke(Sub()
                                                           cmbSceneID2.BackColor = Color.White
                                                       End Sub)
                           End Sub)
            'btnALign2.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gAOICollection.IsCCDExist(sys.CCDNo) = False Then
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'btnALign2.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If
        Dim Update As Boolean
        'If cmbArrayX.SelectedIndex = 0 And cmbArrayY.SelectedIndex = 0 Then
        Update = True
        'End If

        AlignSub(0, 1, cmbSceneID2, Update) '[流道1,定位點2]

        ''[Note]紀錄Recipe的Golden Sample位置
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignOffsetX = AlignResult.AbsOffsetX 'gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignOffsetY = AlignResult.AbsOffsetY 'gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignRoation = AlignResult.Rotation 'gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation

        '使用定位結果計算RealBasicPosX->移動至Recipe04的align button計算
        'Dim offsetX As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).OffsetX
        'Dim offsetY As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).OffsetY
        'Dim degree As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
        'Recipe.Node(sys.StageNo)(NodeID).RealBasicPosX = Recipe.Node(sys.StageNo)(NodeID).BasicPositionX - offsetX
        'Recipe.Node(sys.StageNo)(NodeID).RealBasicPosY = Recipe.Node(sys.StageNo)(NodeID).BasicPositionY - offsetY
        'Recipe.Node(sys.StageNo)(NodeID).RealBasicPosC = degree
        'MsgBox("RealBasicPos setup OK, you can train pattern by aligned Fidicual.")

    End Sub

    Private Async Sub btnALign3_Click(sender As Object, e As EventArgs) Handles btnALign3.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        '20170602按鍵保護
        Btn_Control(False)
        If cmbSceneID3.SelectedItem Is Nothing Then '未選Recipe
            Await Task.Run(Sub()
                               cmbSceneID3.BeginInvoke(Sub()
                                                           cmbSceneID3.BackColor = Color.Yellow
                                                       End Sub)
                               System.Threading.Thread.Sleep(500)
                               cmbSceneID3.BeginInvoke(Sub()
                                                           cmbSceneID3.BackColor = Color.White
                                                       End Sub)
                           End Sub)

            'btnALign3.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gAOICollection.IsCCDExist(sys.CCDNo) = False Then
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'btnALign3.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If
        Dim Update As Boolean
        'If cmbArrayX.SelectedIndex = 0 And cmbArrayY.SelectedIndex = 0 Then
        Update = True
        'End If
        'If gAOICollection.SceneDictionary.ContainsKey(cmbSceneID3.Text) Then
        '    SetLight(sys.CCDNo, cmbSceneID3.Text)
        'End If
        AlignSub(0, 2, cmbSceneID3, Update) '[流道1,定位點3]

        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignOffsetX = AlignResult.AbsOffsetX 'gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignOffsetY = AlignResult.AbsOffsetY 'gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignRoation = AlignResult.Rotation 'gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation

    End Sub

    Private Async Sub btn2ALign_Click(sender As Object, e As EventArgs) Handles btn2ALign.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        '20170602按鍵保護
        Btn_Control(False)

        If cmbScene2ID.SelectedItem Is Nothing Then '未選Recipe
            Await Task.Run(Sub()
                               cmbScene2ID.BeginInvoke(Sub()
                                                           cmbScene2ID.BackColor = Color.Yellow
                                                       End Sub)
                               System.Threading.Thread.Sleep(500)
                               cmbScene2ID.BeginInvoke(Sub()
                                                           cmbScene2ID.BackColor = Color.White
                                                       End Sub)
                           End Sub)
            'btn2ALign.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gAOICollection.IsCCDExist(sys.CCDNo) = False Then
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Acquisition Object Not Exists.")
            'btn2ALign.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If
        Dim Update As Boolean
        'If cmbArrayX.SelectedIndex = 0 And cmbArrayY.SelectedIndex = 0 Then
        Update = True
        'End If

        'If gAOICollection.SceneDictionary.ContainsKey(cmbScene2ID.Text) Then
        '    SetLight(sys.CCDNo, cmbScene2ID.Text)
        'End If

        AlignSub(1, 0, cmbScene2ID, Update) '[流道2,定位點1]
        'If cmbScene2ID.SelectedItem Is Nothing Then '未選Recipe
        '    cmbScene2ID.BackColor = Color.Yellow
        '    Application.DoEvents()
        '    System.Threading.Thread.CurrentThread.Join(100)
        '    cmbScene2ID.BackColor = Color.White
        '    btn2ALign.Enabled = True
        '    MsgBox("Please select SceneID")
        '    Exit Sub
        'End If
        ''Live View輔助線
        'Dim AssistLinePixelX As Integer
        'Dim AssistLinePixelY As Integer
        'If gSSystemParameter.CCDScaleX2X(sys.CCDNo) = 0 Or gSSystemParameter.CCDScaleY2Y(sys.CCDNo) = 0 Then
        '    AssistLinePixelX = 0
        '    AssistLinePixelY = 0
        'Else
        '    AssistLinePixelX = Val(txtRadius.Text) / gSSystemParameter.CCDScaleX2X(sys.CCDNo)
        '    AssistLinePixelY = Val(txtRadius.Text) / gSSystemParameter.CCDScaleY2Y(sys.CCDNo)
        'End If

        'Dim AlignResult As sAlignResult = Nothing
        'If gAOICollection.TakePicture(sys.CCDNo, cmbScene2ID.SelectedItem, AlignResult) = False Then
        '    InvokeUcDisplay(UcDisplay1, gAOICollection, sys, cmbScene2ID.SelectedItem, AssistLinePixelX, AssistLinePixelY, enmDisplayShape.None)
        '    btn2ALign.Enabled = True
        '    Exit Sub
        'End If
        'InvokeUcDisplay(UcDisplay1, gAOICollection, sys, cmbScene2ID.SelectedItem, AssistLinePixelX, AssistLinePixelY, enmDisplayShape.Alignment) '更新控制項,必要條件 frmMain必須是實體

        '使用定位結果計算RealBasicPosX->移動至Recipe04的align button計算
        'Dim offsetX As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).OffsetX
        'Dim offsetY As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).OffsetY
        'Dim degree As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).RealBasicPosX = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionX - offsetX
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).RealBasicPosY = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).BasicPositionY - offsetY
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).RealBasicPosC = degree
        'MsgBox("RealBasicPos setup OK, you can train pattern by aligned Fidicual.")
        '[Note]紀錄Recipe的Golden Sample位置
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignOffsetX = AlignResult.AbsOffsetX ' gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignOffsetY = AlignResult.AbsOffsetY 'gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0).AlignRoation = AlignResult.Rotation 'gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation


    End Sub

    Private Async Sub btn2ALign2_Click(sender As Object, e As EventArgs) Handles btn2ALign2.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        '20170602按鍵保護
        Btn_Control(False)

        If cmbScene2ID2.SelectedItem Is Nothing Then '未選Recipe
            Await Task.Run(Sub()
                               cmbScene2ID2.BeginInvoke(Sub()
                                                            cmbScene2ID2.BackColor = Color.Yellow
                                                        End Sub)
                               System.Threading.Thread.Sleep(500)
                               cmbScene2ID2.BeginInvoke(Sub()
                                                            cmbScene2ID2.BackColor = Color.White
                                                        End Sub)
                           End Sub)
            'btn2ALign2.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gAOICollection.IsCCDExist(sys.CCDNo) = False Then
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Acquisition Object Not Exists.")
            'btn2ALign2.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If
        Dim Update As Boolean
        'If cmbArrayX.SelectedIndex = 0 And cmbArrayY.SelectedIndex = 0 Then
        Update = True
        'End If

        'If gAOICollection.SceneDictionary.ContainsKey(cmbScene2ID2.Text) Then
        '    SetLight(sys.CCDNo, cmbScene2ID2.Text)
        'End If
        AlignSub(1, 1, cmbScene2ID2, Update) '[流道2,定位點2]
        'If cmbScene2ID2.SelectedItem Is Nothing Then '未選Recipe
        '    cmbScene2ID2.BackColor = Color.Yellow
        '    Application.DoEvents()
        '    System.Threading.Thread.CurrentThread.Join(100)
        '    cmbScene2ID2.BackColor = Color.White
        '    btn2ALign2.Enabled = True
        '    MsgBox("Please select SceneID2")
        '    Exit Sub
        'End If

        ''Live View輔助線
        'Dim AssistLinePixelX As Integer
        'Dim AssistLinePixelY As Integer
        'If gSSystemParameter.CCDScaleX2X(sys.CCDNo) = 0 Or gSSystemParameter.CCDScaleY2Y(sys.CCDNo) = 0 Then
        '    AssistLinePixelX = 0
        '    AssistLinePixelY = 0
        'Else
        '    AssistLinePixelX = Val(txtRadius.Text) / gSSystemParameter.CCDScaleX2X(sys.CCDNo)
        '    AssistLinePixelY = Val(txtRadius.Text) / gSSystemParameter.CCDScaleY2Y(sys.CCDNo)
        'End If
        'InvokeUcDisplay(UcDisplay1, gAOICollection, sys, cmbScene2ID2.SelectedItem, AssistLinePixelX, AssistLinePixelY, enmDisplayShape.Alignment) '更新控制項,必要條件 frmMain必須是實體


        'Dim AlignResult As sAlignResult = Nothing
        'If gAOICollection.TakePicture(sys.CCDNo, cmbScene2ID2.SelectedItem, AlignResult) = False Then
        '    InvokeUcDisplay(UcDisplay1, gAOICollection, sys, cmbScene2ID2.SelectedItem, AssistLinePixelX, AssistLinePixelY, enmDisplayShape.None)
        '    btn2ALign2.Enabled = True
        '    Exit Sub
        'End If

        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignOffsetX = AlignResult.AbsOffsetX 'gCCDAlignResultDict(sys.CCDNo)(ticket).Result(1).AbsOffsetX
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignOffsetY = AlignResult.AbsOffsetY 'gCCDAlignResultDict(sys.CCDNo)(ticket).Result(1).AbsOffsetY
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1).AlignRoation = AlignResult.Rotation 'gCCDAlignResultDict(sys.CCDNo)(ticket).Result(1).Rotation

        ''使用定位結果計算RealBasicPosX -> 移至
        'Dim offsetX As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).OffsetX
        'Dim offsetY As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).OffsetY
        'Dim degree As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
        'Recipe.Node(sys.StageNo)(NodeID).RealBasicPosX = Recipe.Node(sys.StageNo)(NodeID).BasicPositionX - offsetX
        'Recipe.Node(sys.StageNo)(NodeID).RealBasicPosY = Recipe.Node(sys.StageNo)(NodeID).BasicPositionY - offsetY
        'Recipe.Node(sys.StageNo)(NodeID).RealBasicPosC = degree
        'MsgBox("RealBasicPos setup OK, you can train pattern by aligned Fidicual.")

    End Sub

    Private Async Sub btn2ALign3_Click(sender As Object, e As EventArgs) Handles btn2ALign3.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        '20170602按鍵保護
        Btn_Control(False)
        If cmbScene2ID3.SelectedItem Is Nothing Then '未選Recipe
            Await Task.Run(Sub()
                               cmbScene2ID3.BeginInvoke(Sub()
                                                            cmbScene2ID3.BackColor = Color.Yellow
                                                        End Sub)
                               System.Threading.Thread.Sleep(500)
                               cmbScene2ID3.BeginInvoke(Sub()
                                                            cmbScene2ID3.BackColor = Color.White
                                                        End Sub)
                           End Sub)

            'btn2ALign3.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gAOICollection.IsCCDExist(sys.CCDNo) = False Then
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Acquisition Object Not Exists.")
            'btn2ALign3.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If
        Dim Update As Boolean
        'If cmbArrayX.SelectedIndex = 0 And cmbArrayY.SelectedIndex = 0 Then
        Update = True
        'End If

        'If gAOICollection.SceneDictionary.ContainsKey(cmbScene2ID3.Text) Then
        '    SetLight(sys.CCDNo, cmbScene2ID3.Text)
        'End If
        AlignSub(1, 2, cmbScene2ID3, Update) '[流道2,定位點3]
        'If cmbScene2ID3.SelectedItem Is Nothing Then '未選Recipe
        '    cmbScene2ID3.BackColor = Color.Yellow
        '    Application.DoEvents()
        '    System.Threading.Thread.CurrentThread.Join(100)
        '    cmbScene2ID3.BackColor = Color.White
        '    btn2ALign3.Enabled = True
        '    MsgBox("Please select SceneID3")
        '    Exit Sub
        'End If

        'If gAOICollection.IsCCDExist(sys.CCDNo) = False Then
        '    MsgBox("Acquisition Object Not Exists.")
        '    btn2ALign3.Enabled = True
        '    Exit Sub
        'End If

        'gAOICollection.SetCCDScene(sys.CCDNo, cmbScene2ID3.SelectedItem) '選擇場景
        'Dim ticket As Integer = 0
        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
        'System.Threading.Thread.CurrentThread.Join(10)
        'ticket = gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eON, False, False) '觸發拍照開
        'System.Threading.Thread.CurrentThread.Join(10)
        'gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保

        'Dim timeOutStopWatch As New Stopwatch '逾時計時器
        'timeOutStopWatch.Restart()
        'Do
        '    Application.DoEvents()
        '    If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
        '        'CCD 取像TimeOut
        '        Select Case sys.StageNo
        '            Case 0
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012003))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            Case 1
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012303))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            Case 2
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012603))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012603), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            Case 3
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012903))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012903), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '        End Select

        '        btn2ALign3.Enabled = True
        '        Exit Sub
        '    End If
        'Loop Until gAOICollection.IsCCDCBusy(sys.CCDNo) = False
        'Debug.Print("IsCCDCBusy:" & timeOutStopWatch.ElapsedMilliseconds)
        ''Live View輔助線
        'Dim AssistLinePixelX As Integer
        'Dim AssistLinePixelY As Integer
        'If gSSystemParameter.CCDScaleX2X(sys.CCDNo) = 0 Or gSSystemParameter.CCDScaleY2Y(sys.CCDNo) = 0 Then
        '    AssistLinePixelX = 0
        '    AssistLinePixelY = 0
        'Else
        '    AssistLinePixelX = Val(txtRadius.Text) / gSSystemParameter.CCDScaleX2X(sys.CCDNo)
        '    AssistLinePixelY = Val(txtRadius.Text) / gSSystemParameter.CCDScaleY2Y(sys.CCDNo)
        'End If
        'InvokeUcDisplay(UcDisplay1, gAOICollection, sys, cmbScene2ID3.SelectedItem, AssistLinePixelX, AssistLinePixelY, enmDisplayShape.Alignment) '更新控制項,必要條件 frmMain必須是實體

        'timeOutStopWatch.Restart()
        'Do
        '    If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
        '        Exit Do
        '    End If
        '    Application.DoEvents()
        '    If timeOutStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
        '        'CCD 計算TimeOut
        '        Select Case sys.StageNo
        '            Case 0
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012004))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            Case 1
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012304))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            Case 2
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012604))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012604), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '            Case 3
        '                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012904))
        '                MsgBox(gMsgHandler.GetMessage(Alarm_2012904), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '        End Select

        '        btn2ALign3.Enabled = True
        '        Exit Sub
        '    End If
        'Loop

        'If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
        '    'CCD 找不到特徵點(等於0)
        '    Select Case sys.StageNo
        '        Case 0
        '            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012103))
        '            MsgBox(gMsgHandler.GetMessage(Alarm_2012103), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '        Case 1
        '            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012403))
        '            MsgBox(gMsgHandler.GetMessage(Alarm_2012403), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '        Case 2
        '            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012703))
        '            MsgBox(gMsgHandler.GetMessage(Alarm_2012703), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '        Case 3
        '            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2013003))
        '            MsgBox(gMsgHandler.GetMessage(Alarm_2013003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    End Select
        '    btn2ALign3.Enabled = True
        '    Exit Sub
        'End If

        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignOffsetX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignOffsetY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2).AlignRoation = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
        ' ''使用定位結果計算RealBasicPosX
        'Dim offsetX As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).OffsetX
        'Dim offsetY As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).OffsetY
        'Dim degree As Decimal = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
        'Recipe.Node(sys.StageNo)(NodeID).RealBasicPosX = Recipe.Node(sys.StageNo)(NodeID).BasicPositionX - offsetX
        'Recipe.Node(sys.StageNo)(NodeID).RealBasicPosY = Recipe.Node(sys.StageNo)(NodeID).BasicPositionY - offsetY
        'Recipe.Node(sys.StageNo)(NodeID).RealBasicPosC = degree
        'MsgBox("RealBasicPos setup OK, you can train pattern by aligned Fidicual.")

    End Sub

#End Region

#Region "場景清單"
    Private Sub cmbSceneID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSceneID.SelectedIndexChanged
        If cmbSceneID.SelectedIndex < 0 Then
            Exit Sub
        End If
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 1 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Dim tempAlignment As AlignmentStructure
        tempAlignment = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0)
        tempAlignment.AlignScene = cmbSceneID.SelectedItem

        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0) = tempAlignment
        For i As Integer = 0 To eConveyor.Max
            '[Note] Conveyor  場景連動
            RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(i).AlignmentData(0).AlignScene = tempAlignment.AlignScene
        Next
        cmbScene2ID.SelectedItem = cmbSceneID.SelectedItem
        SelectScene(tempAlignment.AlignScene, False)

    End Sub

    Private Sub cmbSceneID2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSceneID2.SelectedIndexChanged
        If cmbSceneID2.SelectedIndex < 0 Then
            Exit Sub
        End If
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 2 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Dim tempAlignment As New AlignmentStructure
        tempAlignment = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1)

        tempAlignment.AlignScene = cmbSceneID2.SelectedItem
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1) = tempAlignment
        For i As Integer = 0 To eConveyor.Max
            '[Note] Conveyor  場景連動
            RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(i).AlignmentData(1).AlignScene = tempAlignment.AlignScene
        Next
        cmbScene2ID2.SelectedItem = cmbSceneID2.SelectedItem
        SelectScene(tempAlignment.AlignScene, False)
    End Sub

    Private Sub cmbSceneID3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSceneID3.SelectedIndexChanged
        If cmbSceneID3.SelectedIndex < 0 Then
            Exit Sub
        End If
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 3 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Dim tempAlignment As New AlignmentStructure
        tempAlignment = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2)

        tempAlignment.AlignScene = cmbSceneID3.SelectedItem
        RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2) = tempAlignment
        RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(1).AlignmentData(2).AlignScene = cmbSceneID3.SelectedItem

        SelectScene(tempAlignment.AlignScene, False)
    End Sub

    Private Sub cmbScene2ID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbScene2ID.SelectedIndexChanged

        If cmbScene2ID.SelectedIndex < 0 Then
            Exit Sub
        End If
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 1 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Dim tempAlignment As AlignmentStructure
        tempAlignment = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0)
        tempAlignment.AlignScene = cmbScene2ID.SelectedItem

        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(0) = tempAlignment
        For i As Integer = 0 To eConveyor.Max
            '[Note] Conveyor  場景連動
            RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(i).AlignmentData(0).AlignScene = tempAlignment.AlignScene
        Next
        cmbSceneID.SelectedItem = cmbScene2ID.SelectedItem
        SelectScene(tempAlignment.AlignScene, False)
    End Sub

    Private Sub cmbScene2ID2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbScene2ID2.SelectedIndexChanged
        If cmbScene2ID2.SelectedIndex < 0 Then
            Exit Sub
        End If
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 2 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Dim tempAlignment As New AlignmentStructure
        tempAlignment = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1)

        tempAlignment.AlignScene = cmbScene2ID2.SelectedItem
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(1) = tempAlignment
        For i As Integer = 0 To eConveyor.Max
            '[Note] Conveyor  場景連動
            RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(i).AlignmentData(1).AlignScene = tempAlignment.AlignScene
        Next
        cmbSceneID2.SelectedItem = cmbScene2ID2.SelectedItem
        SelectScene(tempAlignment.AlignScene, False)
    End Sub

    Private Sub cmbScene2ID3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbScene2ID3.SelectedIndexChanged
        If cmbScene2ID3.SelectedIndex < 0 Then
            Exit Sub
        End If
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData.Count < 3 Then
            '定位資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000022))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Dim tempAlignment As New AlignmentStructure
        tempAlignment = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2)

        tempAlignment.AlignScene = cmbScene2ID3.SelectedItem
        RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(2) = tempAlignment
        SelectScene(tempAlignment.AlignScene, False)
    End Sub



#End Region

#End Region


#Region "SkipMark場景動作"

#Region "SetCCDPos"

    Private Sub btnSetSkipMarkPosX_Click(sender As Object, e As EventArgs) Handles btnSetSkipMarkPosX.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnSetSkipMarkPosX.Enabled = False

        If Not RecipeEdit.Editable Then
            BtnReadOnlyBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            btnSetSkipMarkPosX.Enabled = True
            Exit Sub
        End If
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData.Count < 1 Then
            'SkipMark資料不存在
            Select Case conveyorNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2083129))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2083129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2084129))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2084129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            btnSetSkipMarkPosX.Enabled = True
            Exit Sub
        End If
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish And gSYS(eSys.MachineB).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetSkipMarkPosX.Enabled = True
                    Exit Sub
                End If

            Case Else
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetSkipMarkPosX.Enabled = True
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
            btnSetSkipMarkPosX.Enabled = True
            Exit Sub
        End If
        txtSkipMarkPosX.Text = gCMotion.GetPositionValue(sys.AxisX)
        txtSkipMarkPosY.Text = gCMotion.GetPositionValue(sys.AxisY)
        txtSkipMarkPosZ.Text = gCMotion.GetPositionValue(sys.AxisZ)

        btnSetSkipMarkPosX.Enabled = True
    End Sub

    Private Sub btnSetSkipMark2PosX_Click(sender As Object, e As EventArgs) Handles btnSetSkipMark2PosX.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnSetSkipMark2PosX.Enabled = False

        If Not RecipeEdit.Editable Then
            BtnReadOnlyBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            btnSetSkipMark2PosX.Enabled = True
            Exit Sub
        End If
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData.Count < 1 Then
            'SkipMark資料不存在
            Select Case conveyorNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2083129))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2083129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2084129))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2084129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            btnSetSkipMark2PosX.Enabled = True
            Exit Sub
        End If
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish And gSYS(eSys.MachineB).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetSkipMark2PosX.Enabled = True
                    Exit Sub
                End If

            Case Else
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    btnSetSkipMark2PosX.Enabled = True
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
            btnSetSkipMark2PosX.Enabled = True
            Exit Sub
        End If

        txtSkipMark2PosX.Text = gCMotion.GetPositionValue(sys.AxisX)
        txtSkipMark2PosY.Text = gCMotion.GetPositionValue(sys.AxisY)
        txtSkipMark2PosZ.Text = gCMotion.GetPositionValue(sys.AxisZ)

        btnSetSkipMark2PosX.Enabled = True
    End Sub
#End Region

#Region "GoCCDPos"


    'Private Function GoCCDButton_SkipMarkAction(ByVal btn As Button, ByVal SkipMarkPos As AlignmentStructure) As Boolean
    '    'Dim btn As Button = sender
    '    gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
    '    If btn.Enabled = False Then '[Note]防連按
    '        Return False
    '    End If
    '    If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
    '        BtnHomeFirstBehavior(btn)
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
    '        Return False
    '    End If
    '    If Recipe Is Nothing Then
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
    '        Return False
    '    End If

    '    btn.Enabled = False
    '    Btn_Control(False)

    '    '[說明]:X、Y、Z軸
    '    If sys.AxisX > -1 Then
    '        gCMotion.SetVelAccDec(sys.AxisX)
    '    End If
    '    If sys.AxisY > -1 Then
    '        gCMotion.SetVelAccDec(sys.AxisY)
    '    End If
    '    If sys.AxisZ > -1 Then
    '        gCMotion.SetVelAccDec(sys.AxisZ)
    '    End If
    '    If sys.AxisB > -1 Then
    '        gCMotion.SetVelAccDec(sys.AxisB)
    '    End If
    '    If sys.AxisC > -1 Then
    '        gCMotion.SetVelAccDec(sys.AxisC)
    '    End If

    '    Dim AxisNo(4) As Integer
    '    Dim TargetPos(4) As Decimal
    '    AxisNo(0) = sys.AxisX
    '    AxisNo(1) = sys.AxisY
    '    AxisNo(2) = sys.AxisZ
    '    AxisNo(3) = sys.AxisB
    '    AxisNo(4) = sys.AxisC


    '    Select Case gSSystemParameter.CCDModuleType
    '        Case enmCCDModule.eFix
    '            TargetPos(0) = SkipMarkPos.AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
    '            TargetPos(1) = SkipMarkPos.AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
    '            TargetPos(2) = 0
    '            TargetPos(3) = 0
    '            TargetPos(4) = 0

    '        Case enmCCDModule.eFree
    '            TargetPos(0) = SkipMarkPos.AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
    '            TargetPos(1) = SkipMarkPos.AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
    '            TargetPos(2) = SkipMarkPos.AlignPosZ
    '            TargetPos(3) = 0
    '            TargetPos(4) = 0
    '    End Select
    '    ButtonSafeMovePos(btn, AxisNo, TargetPos, sys)
    '    UcJoyStick1.RefreshPosition()

    '    btn.Enabled = True
    '    '20170602按鍵保護
    '    Btn_Control(True)

    '    Return True
    'End Function



    Private Sub btnGoSkipMark_Click(sender As Object, e As EventArgs) Handles btnGoSkipMark.Click

        Dim tmpPos As New AlignmentStructure
        tmpPos.AlignPosX = Val(txtSkipMarkPosX.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosX
        tmpPos.AlignPosY = Val(txtSkipMarkPosY.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosY
        tmpPos.AlignPosZ = Val(txtSkipMarkPosZ.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosZ

        If GoCCDButtonAction(btnGoSkipMark, tmpPos) = False Then
            Exit Sub
        End If

        'gSyslog.Save("[frmRecipe01]" & vbTab & "[btnGoSkipMark]" & vbTab & "Click")
        ''btnGoSkipMark.Enabled = False
        ''20170602按鍵保護
        'Btn_Control(False)
        'If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '    BtnHomeFirstBehavior(sender)
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnGoSkipMark.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If
        'If Recipe Is Nothing Then
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    'btnGoSkipMark.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If


        ''[說明]:X、Y、Z軸
        'If sys.AxisX > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisX)
        'End If
        'If sys.AxisY > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisY)
        'End If
        'If sys.AxisZ > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisZ)
        'End If
        'If sys.AxisB > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisB)
        'End If
        'If sys.AxisC > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisC)
        'End If

        'Dim AxisNo(4) As Integer
        'Dim TargetPos(4) As Decimal
        'AxisNo(0) = sys.AxisX
        'AxisNo(1) = sys.AxisY
        'AxisNo(2) = sys.AxisZ
        'AxisNo(3) = sys.AxisB
        'AxisNo(4) = sys.AxisC

        'If Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData.Count < 1 Then
        '    'SkipMark資料不存在
        '    Select Case conveyorNo
        '        Case 0
        '            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2083129))
        '            MsgBox(gMsgHandler.GetMessage(Alarm_2083129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '        Case 1
        '            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2084129))
        '            MsgBox(gMsgHandler.GetMessage(Alarm_2084129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    End Select
        '    'btnGoSkipMark.Enabled = True
        '    '20170602按鍵保護
        '    Btn_Control(True)
        '    Exit Sub
        'End If

        'Select Case gSSystemParameter.CCDModuleType
        '    Case enmCCDModule.eFix
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = 0
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0

        '    Case enmCCDModule.eFree
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosZ
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0
        'End Select
        'ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)

        'UcJoyStick1.RefreshPosition()
        ''btnGoSkipMark.Enabled = True
        ''20170602按鍵保護
        'Btn_Control(True)
    End Sub

    Private Sub btnGoSkipMark2_Click(sender As Object, e As EventArgs) Handles btnGoSkipMark2.Click
        Dim tmpPos As New AlignmentStructure
        tmpPos.AlignPosX = Val(txtSkipMark2PosX.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosX
        tmpPos.AlignPosY = Val(txtSkipMark2PosY.Text) ' .Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosY
        tmpPos.AlignPosZ = Val(txtSkipMark2PosZ.Text) ' Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosZ

        If GoCCDButtonAction(btnGoSkipMark2, tmpPos) = False Then
            Exit Sub
        End If

        'gSyslog.Save("[frmRecipe01]" & vbTab & "[btnGoSkipMark2]" & vbTab & "Click")
        'btnGoSkipMark2.Enabled = False
        'If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '    BtnHomeFirstBehavior(sender)
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    btnGoSkipMark2.Enabled = True
        '    Exit Sub
        'End If
        'If Recipe Is Nothing Then
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
        '    btnGoSkipMark2.Enabled = True
        '    Exit Sub
        'End If

        ''[說明]:X、Y、Z軸
        'If sys.AxisX > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisX)
        'End If
        'If sys.AxisY > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisY)
        'End If
        'If sys.AxisZ > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisZ)
        'End If
        'If sys.AxisB > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisB)
        'End If
        'If sys.AxisC > -1 Then
        '    gCMotion.SetVelAccDec(sys.AxisC)
        'End If

        'Dim AxisNo(4) As Integer
        'Dim TargetPos(4) As Decimal
        'AxisNo(0) = sys.AxisX
        'AxisNo(1) = sys.AxisY
        'AxisNo(2) = sys.AxisZ
        'AxisNo(3) = sys.AxisB
        'AxisNo(4) = sys.AxisC

        'If Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData.Count < 1 Then
        '    'SkipMark資料不存在
        '    Select Case conveyorNo
        '        Case 0
        '            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2083129))
        '            MsgBox(gMsgHandler.GetMessage(Alarm_2083129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '        Case 1
        '            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2084129))
        '            MsgBox(gMsgHandler.GetMessage(Alarm_2084129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    End Select
        '    btnGoSkipMark2.Enabled = True
        '    Exit Sub
        'End If

        'Select Case gSSystemParameter.CCDModuleType
        '    Case enmCCDModule.eFix
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = 0
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0

        '    Case enmCCDModule.eFree
        '        TargetPos(0) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosX + mMultiArrayAdapter.GetMemoryPosX(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(1) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosY + mMultiArrayAdapter.GetMemoryPosY(cmbArrayX.SelectedIndex, cmbArrayY.SelectedIndex)
        '        TargetPos(2) = Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignPosZ
        '        TargetPos(3) = 0
        '        TargetPos(4) = 0
        'End Select
        'ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)

        'UcJoyStick1.RefreshPosition()
        'btnGoSkipMark2.Enabled = True
    End Sub

#End Region

#Region "Train"

    Private Sub btnTrainSkipMarkScene_Click(sender As Object, e As EventArgs) Handles btnTrainSkipMarkScene.Click
        If gSSystemParameter.CCDAlignModuleEnable Then
            If TrainSceneAction(btnTrainSkipMarkScene, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignScene, cmbSkipMarkScene) Then
                Exit Sub
            End If
        Else
            Dim mfrmAlignPR01 As frmAlignPR01
            gSyslog.Save("[frmRecipe01]" & vbTab & "[btnTrainSkipMarkScene]" & vbTab & "Click")
            If btnTrainSkipMarkScene.Enabled = False Then
                Exit Sub
            End If

            btnTrainSkipMarkScene.Enabled = False
            If Not RecipeEdit.Editable Then
                BtnReadOnlyBehavior(sender)
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
                btnTrainSkipMarkScene.Enabled = True
                Exit Sub
            End If
            If NodeID = "" Then
                '請先選擇Node
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
                MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnTrainSkipMarkScene.Enabled = True
                Exit Sub
            End If

            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData.Count < 1 Then
                'SkipMark資料不存在
                Select Case conveyorNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2083129))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2083129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2084129))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2084129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
                btnTrainSkipMarkScene.Enabled = True
                Exit Sub
            End If
            If gAOICollection.SetCCDScene(sys.CCDNo, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignScene) = False Then
                MsgBox("Scene (" & RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignScene & ") Not Exists!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbSkipMarkScene.Items.Clear()
                    cmbSkipMarkScene.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbSceneID.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbSkipMarkScene.SelectedItem = .RecipeSceneName
                    End If
                End With
                btnTrainSkipMarkScene.Enabled = True
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

                    btnTrainScene.Enabled = True
                    Exit Sub
                End If
            Loop Until Not gAOICollection.IsCCDCBusy(sys.CCDNo) '取像完成
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉

            'Dim AlignCount As Integer = 1
            'Dim AlignSceneName() As String = gAOICollection.GetSceneList ' gAOICollection.GetAlignmentToolNameList(sys.CCDNo)
            'If Not AlignSceneName Is Nothing Then
            '    For mSceneNo As Integer = 0 To AlignSceneName.Count - 1
            '        gAOICollection.SetAlignImage(sys.CCDNo, AlignSceneName(mSceneNo))
            '    Next
            'End If
            Try
                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbSkipMarkScene.Items.Clear()
                    cmbSkipMarkScene.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbSkipMarkScene.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbSkipMarkScene.SelectedItem = .RecipeSceneName
                    End If
                End With
            Catch ex As Exception
                MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Try

            btnTrainSkipMarkScene.Enabled = True
        End If

    End Sub

    Private Sub btnTrainSkipMark2Scene_Click(sender As Object, e As EventArgs) Handles btnTrainSkipMark2Scene.Click
        If gSSystemParameter.CCDAlignModuleEnable Then
            If TrainSceneAction(btnTrainSkipMark2Scene, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignScene, cmbSkipMark2Scene) = False Then
                Exit Sub
            End If
        Else

            Dim mfrmAlignPR01 As frmAlignPR01
            gSyslog.Save("[frmRecipe01]" & vbTab & "[btnTrainSkipMark2Scene]" & vbTab & "Click")
            If btnTrainSkipMark2Scene.Enabled = False Then
                Exit Sub
            End If

            btnTrainSkipMark2Scene.Enabled = False
            If Not RecipeEdit.Editable Then
                BtnReadOnlyBehavior(sender)
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
                btnTrainSkipMark2Scene.Enabled = True
                Exit Sub
            End If
            If NodeID = "" Then
                '請先選擇Node
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
                MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnTrainSkipMark2Scene.Enabled = True
                Exit Sub
            End If

            If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData.Count < 1 Then
                'SkipMark資料不存在
                Select Case conveyorNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2083129))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2083129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2084129))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2084129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
                btnTrainSkipMark2Scene.Enabled = True
                Exit Sub
            End If
            If gAOICollection.SetCCDScene(sys.CCDNo, RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignScene) = False Then
                MsgBox("Scene (" & RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignScene & ") Not Exists!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbSkipMark2Scene.Items.Clear()
                    cmbSkipMark2Scene.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbSceneID.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbSkipMark2Scene.SelectedItem = .RecipeSceneName
                    End If
                End With
                btnTrainSkipMark2Scene.Enabled = True
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

                    btnTrainSkipMark2Scene.Enabled = True
                    Exit Sub
                End If
            Loop Until Not gAOICollection.IsCCDCBusy(sys.CCDNo) '取像完成
            gAOICollection.SetCCDTrigger(sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關閉

            'Dim AlignCount As Integer = 1
            'Dim AlignSceneName() As String = gAOICollection.GetSceneList ' gAOICollection.GetAlignmentToolNameList(sys.CCDNo)
            'If Not AlignSceneName Is Nothing Then
            '    For mSceneNo As Integer = 0 To AlignSceneName.Count - 1
            '        gAOICollection.SetAlignImage(sys.CCDNo, AlignSceneName(mSceneNo))
            '    Next
            'End If
            Try
                mfrmAlignPR01 = New frmAlignPR01
                With mfrmAlignPR01
                    .Sys = sys
                    .Recipe = Me.RecipeEdit
                    .SceneName = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0).AlignScene
                    .StartPosition = FormStartPosition.Manual
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    '[Note]:回復原始設定
                    cmbSkipMark2Scene.Items.Clear()
                    cmbSkipMark2Scene.Items.AddRange(gAOICollection.GetSceneList)
                    'cmbSkipMark2Scene.SelectedItem = .lstScene.SelectedItem
                    If gAOICollection.IsSceneExist(sys.CCDNo, .RecipeSceneName) Then
                        cmbSkipMark2Scene.SelectedItem = .RecipeSceneName
                    End If
                End With
            Catch ex As Exception
                MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Try

            btnTrainSkipMark2Scene.Enabled = True
        End If
    End Sub
#End Region

#Region "定位"
    Private Async Sub btnSkipMarkAlign_Click(sender As Object, e As EventArgs) Handles btnSkipMarkAlign.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")


        '20170602按鍵保護
        Btn_Control(False)
        If cmbSkipMarkScene.SelectedItem Is Nothing Then '未選Recipe
            Await Task.Run(Sub()
                               cmbSkipMarkScene.BeginInvoke(Sub()
                                                                cmbSkipMarkScene.BackColor = Color.Yellow
                                                            End Sub)
                               System.Threading.Thread.Sleep(500)
                               cmbSkipMarkScene.BeginInvoke(Sub()
                                                                cmbSkipMarkScene.BackColor = Color.White
                                                            End Sub)
                           End Sub)
            'btnSkipMarkAlign.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gAOICollection.IsCCDExist(sys.CCDNo) = False Then
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Acquisition Object Not Exists.")
            'btnSkipMarkAlign.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If

        SkipMarkSub(0, cmbSkipMarkScene)


    End Sub

    Private Async Sub btnSkipMark2Align_Click(sender As Object, e As EventArgs) Handles btnSkipMark2Align.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnSkipMark2Align.Enabled = False

        If cmbSkipMark2Scene.SelectedItem Is Nothing Then '未選Recipe
            Await Task.Run(Sub()
                               cmbSkipMark2Scene.BeginInvoke(Sub()
                                                                 cmbSkipMark2Scene.BackColor = Color.Yellow
                                                             End Sub)
                               System.Threading.Thread.Sleep(500)
                               cmbSkipMark2Scene.BeginInvoke(Sub()
                                                                 cmbSkipMark2Scene.BackColor = Color.White
                                                             End Sub)
                           End Sub)
            btnSkipMark2Align.Enabled = True
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gAOICollection.IsCCDExist(sys.CCDNo) = False Then
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Acquisition Object Not Exists.")
            btnSkipMark2Align.Enabled = True
            Exit Sub
        End If
        'If gAOICollection.SceneDictionary.ContainsKey(cmbSkipMark2Scene.Text) Then
        '    SetLight(sys.CCDNo, cmbSkipMark2Scene.Text)
        'End If
        SkipMarkSub(0, cmbSkipMark2Scene)

    End Sub

#End Region

#Region "場景清單"

    Private Sub cmbSkipMarkScene_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkipMarkScene.SelectedIndexChanged
        If cmbSkipMarkScene.SelectedIndex < 0 Then
            Exit Sub
        End If
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData.Count < 1 Then
            'SkipMark資料不存在
            Select Case conveyorNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2083129))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2083129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2084129))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2084129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            Exit Sub
        End If
        Dim tempSkipMark As AlignmentStructure
        tempSkipMark = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0)
        tempSkipMark.AlignScene = cmbSkipMarkScene.SelectedItem
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0) = tempSkipMark
        For i As Integer = 0 To eConveyor.Max
            '[Note] Conveyor  場景連動
            RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(i).SkipMarkData(0).AlignScene = tempSkipMark.AlignScene
        Next
        cmbSkipMark2Scene.SelectedItem = cmbSkipMarkScene.SelectedItem
        SelectScene(tempSkipMark.AlignScene, False)
    End Sub

    Private Sub cmbSkipMark2Scene_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSkipMark2Scene.SelectedIndexChanged
        If cmbSkipMark2Scene.SelectedIndex < 0 Then
            Exit Sub
        End If
        If RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData.Count < 1 Then
            'SkipMark資料不存在
            Select Case conveyorNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2083129))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2083129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2084129))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2084129), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            Exit Sub
        End If
        Dim tempSkipMark As AlignmentStructure
        tempSkipMark = RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0)
        tempSkipMark.AlignScene = cmbSkipMark2Scene.SelectedItem
        'Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(0) = tempSkipMark
        For i As Integer = 0 To eConveyor.Max
            '[Note] Conveyor  場景連動
            RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(i).SkipMarkData(0).AlignScene = tempSkipMark.AlignScene
        Next
        cmbSkipMarkScene.SelectedItem = cmbSkipMark2Scene.SelectedItem
        SelectScene(tempSkipMark.AlignScene, False)
    End Sub


#End Region

#End Region



    Public Function IsSamePos(ByVal PosNo1 As Premtek.sPos, ByVal PosNo2 As Premtek.sPos) As Boolean
        If (PosNo1.PosX = PosNo2.PosX And PosNo1.PosY = PosNo2.PosY) Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("The second Position is the same as the first Position", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("第二定位点和第一定位点相同", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("第二定位點和第一定位點相同", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            Return True
        End If
        Return False
    End Function

    ''' <summary>定位成功旗標
    ''' </summary>
    ''' <remarks></remarks>
    Dim mIsAlignSuccess(2, 3) As Boolean  '[流道數量,定位數量]
    ''' <summary>非同步定位</summary>
    ''' <param name="alignID"></param>
    ''' <param name="cmbBox"></param>
    ''' <remarks></remarks>
    Async Sub AlignSub(ByVal ConveyorNo As Integer, ByVal alignID As Integer, ByVal cmbBox As ComboBox, ByVal Update As Boolean)

        mTaskTokenSource = New CancellationTokenSource()
        mTaskToken = mTaskTokenSource.Token
        Dim sSelectedItem As String = cmbBox.SelectedItem.ToString()
        Dim msysCCDNo As Integer = sys.CCDNo

        If gAOICollection.SceneDictionary.ContainsKey(cmbBox.Text) Then
            SetLight(msysCCDNo, sSelectedItem)
            UpdateUcLightData(sSelectedItem)
        End If
        mIsAlignSuccess(ConveyorNo, alignID) = False
        mRunLongProcessTask = Task.Run(Function()
                                           gSyslog.Save("AlignSub Task.Run ")
                                           System.Threading.Thread.CurrentThread.Join(300) '防連按測試
                                           gAOICollection.SetCCDScene(msysCCDNo, sSelectedItem) '選擇場景
                                           Dim ticket As Integer = 0
                                           gAOICollection.SetCCDTrigger(msysCCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
                                           System.Threading.Thread.Sleep(10)
                                           ticket = gAOICollection.SetCCDTrigger(msysCCDNo, enmONOFF.eON, False, False) '觸發拍照開
                                           System.Threading.Thread.Sleep(10)
                                           gAOICollection.SetCCDTrigger(msysCCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保

                                           Dim stopWatch As New Stopwatch
                                           gSyslog.Save("AlignSub stopWatch.Run ")
                                           stopWatch.Start()
                                           Do
                                               If (mTaskToken.IsCancellationRequested) Then
                                                   gSyslog.Save("AlignSub mTaskToken.IsCancellationRequested ")
                                                   Return 0
                                               End If
                                               If stopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                                                   gSyslog.Save("AlignSub mTaskToken.TimeOut1 ")
                                                   Return 1
                                               End If
                                               Threading.Thread.Sleep(1)
                                           Loop Until gAOICollection.IsCCDCBusy(msysCCDNo) = False

                                           stopWatch.Restart()
                                           gSyslog.Save("AlignSub stopWatch.Restart ")
                                           Do

                                               If (mTaskToken.IsCancellationRequested) Then
                                                   gSyslog.Save("AlignSub mTaskToken.IsCancellationRequested1 ")
                                                   Return 0
                                               End If

                                               If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                                                   gSyslog.Save("AlignSub mTaskToken ContainsKey(ticket) ")
                                                   Exit Do
                                               End If
                                               If stopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                                                   gSyslog.Save("AlignSub mTaskToken.TimeOut2 ")
                                                   Return 2
                                               End If
                                               Threading.Thread.Sleep(1)
                                           Loop

                                           If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
                                               gSyslog.Save("AlignSub Result.Count0 ")
                                               Return 3
                                           End If

                                           If Update Then '[Note]為此層的Golden sample才更新
                                               gSyslog.Save("AlignSub AbsOffsetX " & gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX & "AbsOffsetY " & gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY)
                                               RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(alignID).AlignOffsetX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
                                               RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(alignID).AlignOffsetY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
                                               RecipeEdit.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(alignID).AlignRoation = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
                                               'Debug.Print("***AlignRoation:" & Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).AlignmentData(alignID).AlignRoation)
                                           End If


                                           '-----------------------------------------------
                                           Me.Invoke(Sub()
                                                         'Live View輔助線
                                                         Dim AssistLinePixelX As Integer
                                                         Dim AssistLinePixelY As Integer
                                                         If gSSystemParameter.CCDScaleX2X(sys.CCDNo) = 0 Or gSSystemParameter.CCDScaleY2Y(sys.CCDNo) = 0 Then
                                                             AssistLinePixelX = 0
                                                             AssistLinePixelY = 0
                                                         Else
                                                             AssistLinePixelX = Val(txtRadius.Text) / gSSystemParameter.CCDScaleX2X(sys.CCDNo)
                                                             AssistLinePixelY = Val(txtRadius.Text) / gSSystemParameter.CCDScaleY2Y(sys.CCDNo)
                                                         End If
                                                         InvokeUcDisplay(UcDisplay1, gAOICollection, sys, sSelectedItem, AssistLinePixelX, AssistLinePixelY, enmDisplayShape.Alignment) '更新控制項,必要條件 frmMain必須是實體
                                                     End Sub)
                                           '-----------------------------------------------

                                           gSyslog.Save("AlignSub mTaskToken.End ")
                                           Return 0
                                       End Function, mTaskToken)

        Await mRunLongProcessTask
        gSyslog.Save("AlignSub mRunLongProcessTask.Result: " & mRunLongProcessTask.Result)
        Select Case mRunLongProcessTask.Result
            Case 0 'Success
                mIsAlignSuccess(ConveyorNo, alignID) = True '定位成功
                gSyslog.Save("AlignSub mRunLongProcessTask.Result: mIsAlignSuccess = True")
                MsgBox("Align Success", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Case 1
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
            Case 2
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
            Case 3
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

        End Select
        Select Case alignID 'Soni / 2017.05.11 修正Enable解開時間到Await之後
            Case 0
                btnALign.Enabled = True
                btn2ALign.Enabled = True
                '20170602按鍵保護
                Btn_Control(True)
            Case 1
                btnALign2.Enabled = True
                btn2ALign2.Enabled = True
                '20170602按鍵保護
                Btn_Control(True)
            Case 2
                btnALign3.Enabled = True
                btn2ALign3.Enabled = True
                '20170602按鍵保護
                Btn_Control(True)
        End Select

    End Sub

    Async Sub SkipMarkSub(ByVal SkipMarkID As Integer, ByVal cmbBox As ComboBox)
        mTaskTokenSource = New CancellationTokenSource()
        mTaskToken = mTaskTokenSource.Token
        Dim sSelectedItem As String = cmbBox.SelectedItem.ToString()
        Dim msysCCDNo As Integer = sys.CCDNo

        If gAOICollection.SceneDictionary.ContainsKey(cmbBox.Text) Then
            SetLight(sys.CCDNo, sSelectedItem)
            UpdateUcLightData(sSelectedItem)
        End If

        mRunLongProcessTask = Task.Run(Function()

                                           gAOICollection.SetCCDScene(msysCCDNo, sSelectedItem) '選擇場景
                                           Dim ticket As Integer = 0
                                           gAOICollection.SetCCDTrigger(msysCCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保 會進計算流程
                                           System.Threading.Thread.Sleep(10)
                                           ticket = gAOICollection.SetCCDTrigger(msysCCDNo, enmONOFF.eON, False, False) '觸發拍照開
                                           System.Threading.Thread.Sleep(10)
                                           gAOICollection.SetCCDTrigger(msysCCDNo, enmONOFF.eOff, False, False) '觸發拍照關確保

                                           Dim stopWatch As New Stopwatch
                                           stopWatch.Start()
                                           Do
                                               If (mTaskToken.IsCancellationRequested) Then
                                                   Return 0
                                               End If
                                               If stopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                                                   Return 1
                                               End If
                                               Threading.Thread.Sleep(1)
                                           Loop Until gAOICollection.IsCCDCBusy(msysCCDNo) = False

                                           stopWatch.Restart()

                                           Do

                                               If (mTaskToken.IsCancellationRequested) Then
                                                   Return 0
                                               End If

                                               If gCCDAlignResultDict(sys.CCDNo).ContainsKey(ticket) Then
                                                   Exit Do
                                               End If
                                               If stopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
                                                   Return 2
                                               End If
                                               Threading.Thread.Sleep(1)
                                           Loop

                                           If gCCDAlignResultDict(sys.CCDNo)(ticket).Result.Count = 0 Then '結果必須存在
                                               Return 3
                                           End If
                                           '[Note]SkipMark不用更新
                                           'If Update Then '[Note]為此層的Golden sample才更新
                                           '    Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(SkipMarkID).AlignOffsetX = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetX
                                           '    Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(SkipMarkID).AlignOffsetY = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).AbsOffsetY
                                           '    Recipe.Node(sys.StageNo)(NodeID).ConveyorPos(conveyorNo).SkipMarkData(SkipMarkID).AlignRoation = gCCDAlignResultDict(sys.CCDNo)(ticket).Result(0).Rotation
                                           'End If
                                           '-----------------------------------------------
                                           Me.Invoke(Sub()
                                                         'Live View輔助線
                                                         Dim AssistLinePixelX As Integer
                                                         Dim AssistLinePixelY As Integer
                                                         If gSSystemParameter.CCDScaleX2X(sys.CCDNo) = 0 Or gSSystemParameter.CCDScaleY2Y(sys.CCDNo) = 0 Then
                                                             AssistLinePixelX = 0
                                                             AssistLinePixelY = 0
                                                         Else
                                                             AssistLinePixelX = Val(txtRadius.Text) / gSSystemParameter.CCDScaleX2X(sys.CCDNo)
                                                             AssistLinePixelY = Val(txtRadius.Text) / gSSystemParameter.CCDScaleY2Y(sys.CCDNo)
                                                         End If
                                                         InvokeUcDisplay(UcDisplay1, gAOICollection, sys, sSelectedItem, AssistLinePixelX, AssistLinePixelY, enmDisplayShape.Alignment) '更新控制項,必要條件 frmMain必須是實體
                                                     End Sub)
                                           '-----------------------------------------------
                                           Return 0
                                       End Function, mTaskToken)

        Await mRunLongProcessTask

        Select Case mRunLongProcessTask.Result
            Case 0 'Success
            Case 1
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
            Case 2
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
            Case 3
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

        End Select
        'Select Case SkipMarkID 
        ' Case 0
        btnSkipMarkAlign.Enabled = True
        'Case 1
        btnSkipMark2Align.Enabled = True
        'End Select

        '20170602按鍵保護
        Btn_Control(True)
    End Sub

    Public Sub SetLight(ByVal mCCD As Integer, ByVal mScene As String)
        If mScene = Nothing Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(mScene) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Dim light As enmLight = gSysAdapter.CCDLightMapping(mCCD, enmValveLight.No1)
        gLightCollection.SetLightOnOff(light, gAOICollection.SceneDictionary(mScene).LightEnable(enmValveLight.No1))
        gLightCollection.SetCCDLight(mCCD, light, gAOICollection.SceneDictionary(mScene).LightValue(enmValveLight.No1), True)

        Dim light2 As enmLight = gSysAdapter.CCDLightMapping(mCCD, enmValveLight.No2)
        gLightCollection.SetLightOnOff(light2, gAOICollection.SceneDictionary(mScene).LightEnable(enmValveLight.No2))
        gLightCollection.SetCCDLight(mCCD, light2, gAOICollection.SceneDictionary(mScene).LightValue(enmValveLight.No2), True)

        Dim light3 As enmLight = gSysAdapter.CCDLightMapping(mCCD, enmValveLight.No3)
        gLightCollection.SetLightOnOff(light3, gAOICollection.SceneDictionary(mScene).LightEnable(enmValveLight.No3))
        gLightCollection.SetCCDLight(mCCD, light3, gAOICollection.SceneDictionary(mScene).LightValue(enmValveLight.No3), True)

        Dim light4 As enmLight = gSysAdapter.CCDLightMapping(mCCD, enmValveLight.No4)
        gLightCollection.SetLightOnOff(light4, gAOICollection.SceneDictionary(mScene).LightEnable(enmValveLight.No4))
        gLightCollection.SetCCDLight(mCCD, light4, gAOICollection.SceneDictionary(mScene).LightValue(enmValveLight.No4), True)

    End Sub








    ''' <summary>
    ''' 軸移動時 btn保護
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
                               btnALign.Enabled = disable
                               btnSetCcdPosX2.Enabled = disable
                               btnGoCCD2.Enabled = disable
                               btnTrainScene2.Enabled = disable
                               btnALign2.Enabled = disable
                               btnSetCCDPosX3.Enabled = disable
                               btnGoCCD3.Enabled = disable
                               btnTrainScene3.Enabled = disable
                               btnALign3.Enabled = disable
                               btnSetBasicPos.Enabled = disable
                               btnMoveBasicPos.Enabled = disable
                               btnScalePR.Enabled = disable
                               btnSetCcd2PosX.Enabled = disable
                               btnGo2CCD.Enabled = disable
                               btnTrain2Scene.Enabled = disable
                               btn2ALign.Enabled = disable
                               btnSetCcd2PosX2.Enabled = disable
                               btnGo2CCD2.Enabled = disable
                               btnTrain2Scene2.Enabled = disable
                               btn2ALign2.Enabled = disable
                               btnSetCcd2PosX3.Enabled = disable
                               btnGo2CCD3.Enabled = disable
                               btnTrain2Scene3.Enabled = disable
                               btn2ALign3.Enabled = disable
                               btnSetSkipMarkPosX.Enabled = disable
                               btnGoSkipMark.Enabled = disable
                               btnTrainSkipMarkScene.Enabled = disable
                               btnSkipMarkAlign.Enabled = disable
                               btnSetSkipMark2PosX.Enabled = disable
                               btnGoSkipMark2.Enabled = disable
                               btnTrainSkipMark2Scene.Enabled = disable
                               btnSkipMark2Align.Enabled = disable
                               btnSetRadius.Enabled = disable
                               btnSetBasicPos2.Enabled = disable
                               btnMoveBasicPos2.Enabled = disable
                               btnOK.Enabled = disable
                               btnCancel.Enabled = disable

                               UcLightControl1.Enabled = disable
                               UcJoyStick1.Enabled = disable


                           End Sub)
        End If

    End Sub

    Private Sub btnTargetEdit_Click(sender As Object, e As EventArgs) Handles btnTargetEdit.Click

        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")


        Try
            Dim frmCCDTarget = New frmCCDTargerEdit
            With frmCCDTarget
                .sys = sys
                .StartPosition = FormStartPosition.Manual
                .Location = New Point(0, 0)
                .ShowDialog()
                UcDisplay1.DrawClear(sys.CCDNo)
                UcDisplay1.DrawTarget(sys.CCDNo, gSSystemParameter.CCDTargetDataList)
            End With
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try




        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "ClickEnd")

    End Sub


End Class
Imports ProjectRecipe
Imports ProjectMotion
Imports ProjectCore
Imports ProjectIO
Imports ProjectAOI
Imports ProjectFeedback
Imports Cognex.VisionPro
Imports Cognex.VisionPro.PMAlign
Imports MapData
Imports System.Drawing
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Random
Imports System.IO


Public Class frmRecipe04Step
    Public RecipeEdit As ProjectRecipe.CRecipe

    Dim TmpFrm As Windows.Forms.Form
    Dim TmpFrmXX As frmRecipe04
    Dim bAddNewList As Boolean        '[說明]:判斷資料是新增還是須修改的
    Public ErrMessage As String
    Public sys As sSysParam
    Public [Step] As CPatternStep
    Dim mIsCanClosed As Boolean = True
    '20160920 計算Pitch用
    Dim mDisDx As Double
    Dim mDisDy As Double
    Dim mSubDistance As Decimal
    Dim Dot As Integer
    Dim mConveyorNo As eConveyorModel
    ''' <summary>選用的閥號. ex. Valve1 Valve2</summary>
    ''' <remarks></remarks>
    Dim WorkValve As enmValve
    ''' <summary>選用的閥DB名稱</summary>
    ''' <remarks></remarks>
    Dim mSelectedValveDBName As String
    ''' <summary>目前節點名稱</summary>
    ''' <remarks></remarks>
    Dim mNowNodeID As String
    ''' <summary>
    ''' 目前使用Round編號
    ''' </summary>
    ''' <remarks></remarks>
    Dim mRoundNo As Integer
    ''' <summary>
    ''' 目前使用步驟編號
    ''' </summary>
    ''' <remarks></remarks>
    Dim mStepNo As Integer
    Dim mCycleTimes(enmStage.Max) As Decimal
    '20170606_Toby add  初始為true
    Public AddNewStep As Boolean = True
    ''' <summary>
    ''' 是否表單載入完畢, 載入完介面連動才能生效
    ''' </summary>
    ''' <remarks></remarks>
    Dim mIsLoaded As Boolean

    '20160820
    Public Sub New(ByVal tmpForm As frmRecipe04, ByVal bAddList As Boolean, ByRef recipeEdit As CRecipe)

        ' 此為設計工具所需的呼叫。
        InitializeComponent()
        bAddNewList = bAddList
        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Me.RecipeEdit = recipeEdit
        TmpFrmXX = tmpForm
        sys = tmpForm.sys

        If TmpFrmXX.SelectedNodePath.Length > 8 Then
            Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
            mRoundNo = TmpFrmXX.lstRoundNo.SelectedIndex
            mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
            Dim mNowStep As CPatternStep = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            mNowNodeID = TmpFrmXX.SelectedNodePath
            WorkValve = FindSelectValve(mNowNodeID, nodename, mRoundNo, mStepNo)
            mSelectedValveDBName = RecipeEdit.StageParts(sys.StageNo).ValveName(WorkValve)

            If gJetValveDB.ContainsKey(mSelectedValveDBName) = True Then
                Select Case gJetValveDB(mSelectedValveDBName).ValveModel
                    Case eValveModel.PicoPulse
                        mCycleTimes(sys.StageNo) = gJetValveDB(mSelectedValveDBName).PicoTouch.CycleTime
                    Case eValveModel.Advanjet
                        mCycleTimes(sys.StageNo) = gJetValveDB(mSelectedValveDBName).Advanjet.CycleTime
                End Select
            Else
                mCycleTimes(sys.StageNo) = 2 '預設值
            End If

        End If

    End Sub

    '避免無止盡增加
    Private Sub frmRecipe04Step_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        'Me.Close() 'Eason - 20170106 : Soni 說拿掉
    End Sub

    Private Sub frmRecipe04Step_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170124 Ticket:100031 , Memory Freed [S]
        UcJoyStick1.ManualDispose()
        Me.Dispose(True)
        'Eason 20170124 Ticket:100031 , Memory Freed [E]
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub frmRecipe04Step_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If mIsCanClosed = False Then
            e.Cancel = True
            Exit Sub
        End If
        '20170808   Del
        '  RecipeEdit.SavePattern(RecipeEdit.strFileName)

    End Sub

    ''' <summary>由是否可編輯決定控制項致能</summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Sub UpdateUIbyEditable(ByVal value As Boolean)
        btnArc3DDone.Enabled = value
        btnArcDone.Enabled = value
        btnCircle3DDone.Enabled = value
        btnCircleDone.Enabled = value
        btnContiEndDone.Enabled = value
        btnContiStartDone.Enabled = value
        btnDots3DDone.Enabled = value
        btnExtendOnDone.Enabled = value
        btnExtendOffDone.Enabled = value
        btnLine3DDone.Enabled = value
        btnMove3DDone.Enabled = value
        btnSelectValveDone.Enabled = value
        btnWaitDone.Enabled = value

    End Sub
    ''' <summary>由膠路設定方式決定控制項致能</summary>
    ''' <param name="runType"></param>
    ''' <remarks></remarks>
    Sub UpdateUIByWeightCtrlType(ByVal runType As eWeightControlType)
        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                txtDot3DWeight.Enabled = False
                lblDot3DWeight.Enabled = False
                lblDot3DWeightUnit.Enabled = False

                nmuLine3DWeight.Enabled = False
                lblLine3DWeight.Enabled = False
                lblLine3DWeightUnit.Enabled = False
                lblLine3DVelocity.Enabled = False
                nmuLine3DVelocity.Enabled = False
                lblLine3DVelocityUnit.Enabled = False


                nmuArcWeight.Enabled = False
                lblArcWeight.Enabled = False
                lblArcWeightUnit.Enabled = False
                lblArcVelocity.Enabled = False
                nmuArcVelocity.Enabled = False
                lblArcVelocityUnit.Enabled = False

                nmuCircleWeight.Enabled = False
                lblCircleWeight.Enabled = False
                lblCircleWeightUnit.Enabled = False
                lblCircleVelocity.Enabled = False
                nmuCircleVelocity.Enabled = False
                lblCircleVelocityUnit.Enabled = False

            Case eWeightControlType.Weight
                lblDots3DDot.Enabled = False 'Soni + 2017.07.04
                txtDots3DDot.Enabled = False


                nmuLine3DDot.Enabled = False
                lblLine3DDot.Enabled = False
                nmuLine3DDot.Enabled = False
                lblLine3DDot.Enabled = False

                lblLine3DVelocity.Enabled = False
                nmuLine3DVelocity.Enabled = False
                lblLine3DVelocityUnit.Enabled = False

                nmuArcDot.Enabled = False
                lblArcDot.Enabled = False
                lblArcVelocity.Enabled = False
                nmuArcVelocity.Enabled = False
                lblArcVelocityUnit.Enabled = False

                nmuCircleDot.Enabled = False
                lblCircleDot.Enabled = False
                lblCircleVelocity.Enabled = False
                nmuCircleVelocity.Enabled = False
                lblCircleVelocityUnit.Enabled = False

            Case eWeightControlType.Velocity
                lblDots3DDot.Enabled = False 'Soni + 2017.07.04
                txtDots3DDot.Enabled = False
                txtDot3DWeight.Enabled = False
                lblDot3DWeight.Enabled = False
                lblDot3DWeightUnit.Enabled = False

                nmuLine3DDot.Enabled = False
                lblLine3DDot.Enabled = False
                nmuLine3DWeight.Enabled = False
                lblLine3DWeight.Enabled = False
                lblLine3DWeightUnit.Enabled = False

                nmuArcDot.Enabled = False
                lblArcDot.Enabled = False
                nmuArcWeight.Enabled = False
                lblArcWeight.Enabled = False
                lblArcWeightUnit.Enabled = False

                nmuCircleDot.Enabled = False
                lblCircleDot.Enabled = False
                nmuCircleWeight.Enabled = False
                lblCircleWeight.Enabled = False
                lblCircleWeightUnit.Enabled = False
        End Select

    End Sub

    ''' <summary>套用參數</summary>
    ''' <param name="nmc"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Sub SetNumericUpDownValue(ByRef nmc As NumericUpDown, ByVal value As Decimal)
        If value > nmc.Maximum Then
            nmc.Value = nmc.Maximum
            Exit Sub
        End If
        If value < nmc.Minimum Then
            nmc.Value = nmc.Minimum
            Exit Sub
        End If
        nmc.Value = value
    End Sub

    Private Sub frmRecipe04Step_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim mVmax As Decimal, mVmin As Decimal
        Dim mDotmax As Decimal, mDotmin As Decimal
        Dim mWmax As Decimal, mWmin As Decimal

        '[Note]:選擇對應的Conveyor，根據系統設定使用哪個convyor生產來決定
        Select Case gSSystemParameter.ConveyorModel
            Case eConveyorModel.eConveyorNo1
                mConveyorNo = eConveyorModel.eConveyorNo1

            Case eConveyorModel.eConveyorNo2
                mConveyorNo = eConveyorModel.eConveyorNo2

            Case eConveyorModel.eConveyorNo1No2
                mConveyorNo = eConveyorModel.eConveyorNo1

        End Select

        '[Note]光源亮度
        UcLightControl1.CCDNo = sys.CCDNo
        If RecipeEdit.Node(sys.StageNo)(mNowNodeID).ConveyorPos(mConveyorNo).AlignmentData.Count > 1 Then
            If Not RecipeEdit.Node(sys.StageNo)(mNowNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene Is Nothing Then
                If gAOICollection.SceneDictionary.ContainsKey(RecipeEdit.Node(sys.StageNo)(mNowNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene) Then
                    UcLightControl1.SceneName = RecipeEdit.Node(sys.StageNo)(mNowNodeID).ConveyorPos(mConveyorNo).AlignmentData(0).AlignScene
                    UcLightControl1.ShowUI()
                Else
                    '[Note]預設值
                    If gAOICollection.SceneDictionary.ContainsKey("CALIB" & (sys.CCDNo + 1).ToString) Then
                        UcLightControl1.SceneName = "CALIB" & (sys.CCDNo + 1).ToString
                        UcLightControl1.ShowUI()
                    End If
                End If
            Else
                '[Note]預設值
                If gAOICollection.SceneDictionary.ContainsKey("CALIB" & (sys.CCDNo + 1).ToString) Then
                    UcLightControl1.SceneName = "CALIB" & (sys.CCDNo + 1).ToString
                    UcLightControl1.ShowUI()
                End If
            End If
        End If


        '20170814 jog防護
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按          
            UcJoyStick1.Enabled = False
        Else
            UcJoyStick1.Enabled = True
        End If

        '20170215
        'Me.tabStep.Location = New Point(2, -22) '隱藏標題
        Me.tabStep.Location = New Point(2, -32) '隱藏標題 'Eason 20170125 . Location Error
        Me.Location = New Point(700, 180)

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

        UpdateUIbyEditable(RecipeEdit.Editable) 'Soni / 2017.09.19 抽出成Func.
        '20170308_Toby 卡Align沒有 Pass
        Call AlignmentRefreshUI()
        UpdateUIByWeightCtrlType(RecipeEdit.RunType) 'Soni / 2017.09.19 由Visible改為Enabled.

        SetNumericUpDownLimit() '設定控制項上下限

        Select Case [Step].StepType
            Case eStepFunctionType.Arc2D
                '--- 起終點共點保護 ---  20160805  相同座標點,強制給預設值   20160820
                If [Step].Arc2D.StartPosX = [Step].Arc2D.MiddlePosX And [Step].Arc2D.StartPosY = [Step].Arc2D.MiddlePosY Then
                    [Step].Arc2D.StartPosX = 0
                    [Step].Arc2D.StartPosY = 0
                    [Step].Arc2D.MiddlePosX = 10
                    [Step].Arc2D.MiddlePosY = 10
                End If
                If [Step].Arc2D.StartPosX = [Step].Arc2D.EndPosX And [Step].Arc2D.StartPosY = [Step].Arc2D.EndPosY Then
                    [Step].Arc2D.StartPosX = 0
                    [Step].Arc2D.StartPosY = 0
                    [Step].Arc2D.EndPosX = -10
                    [Step].Arc2D.EndPosY = 10
                End If
                If [Step].Arc2D.MiddlePosX = [Step].Arc2D.EndPosX And [Step].Arc2D.MiddlePosY = [Step].Arc2D.EndPosY Then
                    [Step].Arc2D.MiddlePosX = 10
                    [Step].Arc2D.MiddlePosY = 10
                    [Step].Arc2D.EndPosX = -10
                    [Step].Arc2D.EndPosY = 10
                End If
                '--- 起終點共點保護 ---

                '20160808
                '---三點共線保護--- 公式:兩點斜率相同,給預設值
                If (([Step].Arc2D.EndPosY - [Step].Arc2D.StartPosY) * ([Step].Arc2D.MiddlePosX - [Step].Arc2D.StartPosX)) =
                    (([Step].Arc2D.MiddlePosY - [Step].Arc2D.StartPosY) * ([Step].Arc2D.EndPosX - [Step].Arc2D.StartPosX)) Then
                    [Step].Arc2D.StartPosX = 0
                    [Step].Arc2D.StartPosY = 0
                    [Step].Arc2D.MiddlePosX = 10
                    [Step].Arc2D.MiddlePosY = 10
                    [Step].Arc2D.EndPosX = -10
                    [Step].Arc2D.EndPosY = 10
                End If
                '---三點共線保護---

                '[Note]:預設值保護 mobary+ 2016.10.08
                Select Case RecipeEdit.RunType
                    Case eWeightControlType.Dots
                        '20161129
                        If [Step].Arc2D.WeightControl.DotCounts <= 2 Then
                            [Step].Arc2D.WeightControl.DotCounts = 2
                        End If

                    Case eWeightControlType.Weight
                        If [Step].Arc2D.WeightControl.Weight <= 0 Then
                            [Step].Arc2D.WeightControl.Weight = 10
                        End If

                    Case eWeightControlType.Velocity
                        If [Step].Arc2D.WeightControl.Velocity <= 0 Then
                            [Step].Arc2D.WeightControl.Velocity = 10
                        End If

                End Select

                tabStep.SelectedTab = tabArc
                grpArc.Text = "Arc " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath) & GetBasicIndex(TmpFrmXX.SelectedNodePath)
                txtArcStartPosX.Text = [Step].Arc2D.StartPosX
                txtArcStartPosY.Text = [Step].Arc2D.StartPosY
                txtArcStartPosZ.Text = 0
                txtArcEndPosX.Text = [Step].Arc2D.EndPosX
                txtArcEndPosY.Text = [Step].Arc2D.EndPosY
                txtArcEndPosZ.Text = 0
                txtArcMidPosX.Text = [Step].Arc2D.MiddlePosX
                txtArcMidPosY.Text = [Step].Arc2D.MiddlePosY
                txtArcMidPosZ.Text = [Step].Arc2D.MiddlePosZ



                Dim mPitch As Decimal = [Step].Arc2D.Pitch
                txtArcPitch.Text = mPitch.ToString("##0.###")
                '20170120
                txtArcComment.Text = [Step].Arc2D.Comment
                '20170505
                txtArcDotWeight.Text = Format(RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve), "0.##########")



                Dim mRadius As Decimal
                GetArcLengthRadius(mSubDistance, mRadius)
                Dim mAcc As Decimal = gCMotion.SyncParameter(0).Velocity.Acc * gCMotion.SyncParameter(0).Velocity.AccRatio

                GetLineDotLimit(mSubDistance, mDotmin, mDotmax)
                nmuArcDot.Maximum = mDotmax
                nmuArcDot.Minimum = mDotmin
                GetArcVelocityLimit(mSubDistance, mRadius, mAcc, mVmin, mVmax)
                nmuArcVelocity.Minimum = mVmin
                nmuArcVelocity.Maximum = mVmax

                If GetLineWeightLimit(mSubDistance, mWmin, mWmax) Then
                    nmuArcWeight.Minimum = mWmin
                    nmuArcWeight.Maximum = mWmax
                End If

                Select Case RecipeEdit.RunType
                    Case eWeightControlType.Dots
                        'Pitch = 總長 / 點數 * 1000(mm轉um)
                        mPitch = mSubDistance / Val([Step].Arc2D.WeightControl.DotCounts - 1) * 1000
                        txtArcPitch.Text = mPitch.ToString("##0.###")

                    Case eWeightControlType.Weight
                        '[說明]:有做秤重才能計算Pitch
                        If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) > 0 Then
                            '單點重量 = 總重 / 單顆Dot重量
                            Dot = Val([Step].Arc2D.WeightControl.Weight) / RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                            '20161115
                            'Pitch = 總長 / 點數 * 1000(mm轉um)
                            If Dot > 0 Then
                                mPitch = mSubDistance / (Dot - 1) * 1000
                                txtArcPitch.Text = mPitch.ToString("##0.###")
                            ElseIf Dot <= 0 Then
                                txtArcPitch.Text = ""
                            End If
                        End If

                    Case eWeightControlType.Velocity
                        'TODO:待補


                End Select
                SetNumericUpDownValue(nmuArcDot, [Step].Arc2D.WeightControl.DotCounts)
                SetNumericUpDownValue(nmuArcWeight, [Step].Arc2D.WeightControl.Weight)
                SetNumericUpDownValue(nmuArcVelocity, [Step].Arc2D.WeightControl.Velocity)

                '20171016
                SetNumericUpDownValue(nmuArcStartVelocity, [Step].Arc2D.StartVel)

                'Eason 20170216 Ticket:100080 , Add Arc Type Parameter[S]
                cbArcTypeSelect.Items.Clear()
                For i As Integer = 0 To gArcValueDB.Count - 1
                    cbArcTypeSelect.Items.Add(gArcValueDB.Keys(i))
                Next
                Dim indexfind As Integer = cbArcTypeSelect.FindStringExact([Step].Arc2D.ArcParameterName)
                If (indexfind >= 0) Then
                    cbArcTypeSelect.SelectedIndex = indexfind
                End If
                'Eason 20170216 Ticket:100080 , Add Arc Type Parameter[E]


            Case eStepFunctionType.Arc3D
                tabStep.SelectedTab = tabArc3D
                grpArc3D.Text = "Arc3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath) & GetBasicIndex(TmpFrmXX.SelectedNodePath)
                txtArc3DAngle.Text = [Step].Arc3D.Angle
                txtArc3DCenterPosX.Text = [Step].Arc3D.CenterPosX
                txtArc3DCenterPosY.Text = [Step].Arc3D.CenterPosY
                txtArc3DCenterPosZ.Text = [Step].Arc3D.CenterPosZ
                txtArc3DEndPosX.Text = [Step].Arc3D.EndPosX
                txtArc3DEndPosY.Text = [Step].Arc3D.EndPosY
                txtArc3DEndPosZ.Text = [Step].Arc3D.EndPosZ
                txtArc3DStartPosX.Text = [Step].Arc3D.StartPosX
                txtArc3DStartPosY.Text = [Step].Arc3D.StartPosY
                txtArc3DStartPosZ.Text = [Step].Arc3D.StartPosZ
                '[Note]:預設值保護 mobary+ 2016.10.08
                Select Case RecipeEdit.RunType
                    Case eWeightControlType.Dots
                        If [Step].Arc3D.WeightControl.DotCounts <= 0 Then
                            [Step].Arc3D.WeightControl.DotCounts = 10
                        End If

                    Case eWeightControlType.Weight
                        If [Step].Arc3D.WeightControl.Weight <= 0 Then
                            [Step].Arc3D.WeightControl.Weight = 10
                        End If

                    Case eWeightControlType.Velocity
                        'TODO:待補

                End Select
                'Eason 20170216 Ticket:100080 , Add Arc Type Parameter[S]
                cbArc3DTypeSelect.Items.Clear()
                For i As Integer = 0 To gArcValueDB.Count - 1 '目前共用同一組參數
                    cbArc3DTypeSelect.Items.Add(gArcValueDB.Keys(i))
                Next
                Dim indexfind As Integer = cbArc3DTypeSelect.FindStringExact([Step].Arc3D.ArcParameterName)
                If (indexfind >= 0) Then
                    cbArc3DTypeSelect.SelectedIndex = indexfind
                End If
                'Eason 20170216 Ticket:100080 , Add Arc Type Parameter[E]

            Case eStepFunctionType.Array

            Case eStepFunctionType.CCDEnd

            Case eStepFunctionType.CCDLine

            Case eStepFunctionType.CCDStart

            Case eStepFunctionType.Circle2D
                '--- 起終點共點保護 ---  20160805  相同座標點,強制給預設值   20160820
                If [Step].Circle2D.StartPosX = [Step].Circle2D.MiddlePosX And [Step].Circle2D.StartPosY = [Step].Circle2D.MiddlePosY Then
                    [Step].Circle2D.StartPosX = 0
                    [Step].Circle2D.StartPosY = 0
                    [Step].Circle2D.MiddlePosX = 10
                    [Step].Circle2D.MiddlePosY = 10
                End If
                If [Step].Circle2D.StartPosX = [Step].Circle2D.Middle2PosX And [Step].Circle2D.StartPosY = [Step].Circle2D.Middle2PosY Then
                    [Step].Circle2D.StartPosX = 0
                    [Step].Circle2D.StartPosY = 0
                    [Step].Circle2D.Middle2PosX = -10
                    [Step].Circle2D.Middle2PosY = 10
                End If
                If [Step].Circle2D.MiddlePosX = [Step].Circle2D.Middle2PosX And [Step].Circle2D.MiddlePosY = [Step].Circle2D.Middle2PosY Then
                    [Step].Circle2D.MiddlePosX = 10
                    [Step].Circle2D.MiddlePosY = 10
                    [Step].Circle2D.Middle2PosX = -10
                    [Step].Circle2D.Middle2PosY = 10
                End If
                '--- 起終點共點保護 ---

                '20160808
                '---三點共線保護--- 公式:兩點斜率相同,給預設值
                If (([Step].Circle2D.Middle2PosY - [Step].Circle2D.StartPosY) * ([Step].Circle2D.MiddlePosX - [Step].Circle2D.StartPosX)) =
                    (([Step].Circle2D.MiddlePosY - [Step].Circle2D.StartPosY) * ([Step].Circle2D.Middle2PosX - [Step].Circle2D.StartPosX)) Then
                    [Step].Circle2D.StartPosX = 0
                    [Step].Circle2D.StartPosY = 0
                    [Step].Circle2D.MiddlePosX = 10
                    [Step].Circle2D.MiddlePosY = 10
                    [Step].Circle2D.Middle2PosX = -10
                    [Step].Circle2D.Middle2PosY = 10
                End If
                '---三點共線保護---

                '[Note]:預設值保護 mobary+ 2016.10.08
                Select Case RecipeEdit.RunType
                    Case eWeightControlType.Dots
                        '20161129
                        If [Step].Circle2D.WeightControl.DotCounts <= 2 Then
                            [Step].Circle2D.WeightControl.DotCounts = 2
                        End If

                    Case eWeightControlType.Weight
                        If [Step].Circle2D.WeightControl.Weight <= 0 Then
                            [Step].Circle2D.WeightControl.Weight = 10
                        End If

                    Case eWeightControlType.Velocity
                        If [Step].Circle2D.WeightControl.Velocity <= 0 Then
                            [Step].Circle2D.WeightControl.Velocity = 10
                        End If

                End Select

                tabStep.SelectedTab = tabCircle
                grpDot3D.Text = "Circle2D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath) & GetBasicIndex(TmpFrmXX.SelectedNodePath)
                txtCircleStartPosX.Text = [Step].Circle2D.StartPosX
                txtCircleStartPosY.Text = [Step].Circle2D.StartPosY
                txtCircleStartPosZ.Text = 0
                txtCircleMidPosX.Text = [Step].Circle2D.MiddlePosX
                txtCircleMidPosY.Text = [Step].Circle2D.MiddlePosY
                txtCircleMidPosZ.Text = 0
                txtCircleCenterPosX.Text = [Step].Circle2D.CenterPosX
                txtCircleCenterPosY.Text = [Step].Circle2D.CenterPosY
                txtCircleCenterPosZ.Text = 0
                txtCircleMid2PosX.Text = [Step].Circle2D.Middle2PosX
                txtCircleMid2PosY.Text = [Step].Circle2D.Middle2PosY
                txtCircleMid2PosZ.Text = 0
                'txtCircleEndPosB.Text = 0
                'txtCircleEndPosC.Text = 0
                txtCirclePitch.Text = [Step].Circle2D.Pitch
                '20170120
                txtCircleComment.Text = [Step].Circle2D.Comment
                '20170505
                txtCircleDotWeight.Text = Format(RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve), "0.##########")

                Dim mRadius As Decimal
                GetCircleLengthRadius(mSubDistance, mRadius)
                Dim mAcc As Decimal = gCMotion.SyncParameter(0).Velocity.Acc * gCMotion.SyncParameter(0).Velocity.AccRatio

                GetCircleDotLimit(mSubDistance, mRadius, mAcc, mDotmin, mDotmax)
                nmuCircleDot.Maximum = mDotmax
                nmuCircleDot.Minimum = mDotmin
                GetArcVelocityLimit(mSubDistance, mRadius, mAcc, mVmin, mVmax)
                nmuCircleVelocity.Minimum = mVmin
                nmuCircleVelocity.Maximum = mVmax

                If GetCircleWeightLimit(mSubDistance, mRadius, mAcc, mWmin, mWmax) Then
                    nmuCircleWeight.Minimum = mWmin
                    nmuCircleWeight.Maximum = mWmax
                End If

                Select Case RecipeEdit.RunType
                    Case eWeightControlType.Dots
                        'Pitch = 總長 / 點數 * 1000(mm轉um)
                        txtCirclePitch.Text = (mSubDistance / Val([Step].Circle2D.WeightControl.DotCounts) * 1000).ToString("##0.###")

                    Case eWeightControlType.Weight
                        '[說明]:有做秤重才能計算Pitch
                        If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) > 0 Then
                            '單點重量 = 總重 / 單顆Dot重量
                            Dot = Val([Step].Circle2D.WeightControl.Weight) / RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                            '20161115
                            'Pitch = 總長 / 點數 * 1000(mm轉um)
                            If Dot > 0 Then
                                ' txtCirclePitch.Text = mSubDistance / Dot * 1000
                                txtCirclePitch.Text = (mSubDistance / Dot * 1000).ToString("##0.###")
                            ElseIf Dot <= 0 Then
                                txtCirclePitch.Text = ""
                            End If
                        End If

                    Case eWeightControlType.Velocity
                        'TODO:待補

                End Select
                SetNumericUpDownValue(nmuCircleDot, [Step].Circle2D.WeightControl.DotCounts)
                SetNumericUpDownValue(nmuCircleWeight, [Step].Circle2D.WeightControl.Weight)
                SetNumericUpDownValue(nmuCircleVelocity, [Step].Circle2D.WeightControl.Velocity)

                '20171016
                SetNumericUpDownValue(nmuCircleStartVelocity, [Step].Circle2D.StartVel)

                'Eason 20170216 Ticket:100080 , Add Arc Type Parameter[S]
                cbCircleTypeSelect.Items.Clear()
                For i As Integer = 0 To gArcValueDB.Count - 1 '目前共用同一組參數
                    cbCircleTypeSelect.Items.Add(gArcValueDB.Keys(i))
                Next
                Dim indexfind As Integer = cbCircleTypeSelect.FindStringExact([Step].Circle2D.ArcParameterName)
                If (indexfind >= 0) Then
                    cbCircleTypeSelect.SelectedIndex = indexfind
                End If
                'Eason 20170216 Ticket:100080 , Add Arc Type Parameter[E]

            Case eStepFunctionType.Circle3D
                tabStep.SelectedTab = tabCircle3D
                grpDot3D.Text = "Circle3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath) & GetBasicIndex(TmpFrmXX.SelectedNodePath)
                txtCircle3DCenterPosX.Text = [Step].Circle3D.CenterPosX
                txtCircle3DCenterPosY.Text = [Step].Circle3D.CenterPosY
                txtCircle3DCenterPosZ.Text = [Step].Circle3D.CenterPosZ
                txtCircle3DEndPosX.Text = [Step].Circle3D.EndPosX
                txtCircle3DEndPosY.Text = [Step].Circle3D.EndPosY
                txtCircle3DEndPosZ.Text = [Step].Circle3D.EndPosZ
                'txtCircle3DEndPosB.Text = 0
                'txtCircle3DEndPosC.Text = 0
                '[Note]:預設值保護 mobary+ 2016.10.08
                Select Case RecipeEdit.RunType
                    Case eWeightControlType.Dots
                        If [Step].Circle3D.WeightControl.DotCounts <= 0 Then
                            [Step].Circle3D.WeightControl.DotCounts = 10
                        End If

                    Case eWeightControlType.Weight
                        If [Step].Circle3D.WeightControl.Weight <= 0 Then
                            [Step].Circle3D.WeightControl.Weight = 10
                        End If

                    Case eWeightControlType.Velocity
                        'TODO:待補

                End Select
                'Eason 20170216 Ticket:100080 , Add Arc Type Parameter[S]
                cbCircle3DTypeSelect.Items.Clear()
                For i As Integer = 0 To gArcValueDB.Count - 1 '目前共用同一組參數
                    cbCircle3DTypeSelect.Items.Add(gArcValueDB.Keys(i))
                Next
                Dim indexfind As Integer = cbCircle3DTypeSelect.FindStringExact([Step].Circle3D.ArcParameterName)
                If (indexfind >= 0) Then
                    cbCircle3DTypeSelect.SelectedIndex = indexfind
                End If
                'Eason 20170216 Ticket:100080 , Add Arc Type Parameter[E]

            Case eStepFunctionType.ContiEnd
                tabStep.SelectedTab = TabContiEnd
            Case eStepFunctionType.ContiStart
                tabStep.SelectedTab = TabContiStart

            Case eStepFunctionType.Dots3D
                tabStep.SelectedTab = tabDots3D
                grpDot3D.Text = "Dots3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath) & GetBasicIndex(TmpFrmXX.SelectedNodePath)
                txtDots3DPosX.Text = [Step].Dots3D.PosX
                txtDots3DPosY.Text = [Step].Dots3D.PosY
                txtDots3DPosZ.Text = [Step].Dots3D.PosZ
                'Sue0511                
                'txtDots3DPosB.Text = 0
                'txtDots3DPosC.Text = 0

                '20170505
                txtDots3DDotWeight.Text = Format(RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve), "0.##########")

                ''[Note]:預設值保護 mobary+ 2016.10.08
                ''[Note]:Dots是強制切換成Velocity
                '[Step].Dots3D.WeightControl.Type = eWeightControlType.Velocity

                Select Case RecipeEdit.RunType
                    Case eWeightControlType.Velocity
                        'If [Step].Dots3D.WeightControl.Velocity <= 0 Then
                        '    [Step].Dots3D.WeightControl.Velocity = 10
                        'End If
                        txtDots3DVelocity.Text = [Step].Dots3D.WeightControl.Velocity

                    Case eWeightControlType.Weight
                        'TODO:保護待補
                        txtDot3DWeight.Text = [Step].Dots3D.WeightControl.Weight

                    Case eWeightControlType.Dots
                        If [Step].Dots3D.WeightControl.DotCounts < 1 Then
                            [Step].Dots3D.WeightControl.DotCounts = 1
                        End If
                        txtDots3DDot.Text = [Step].Dots3D.WeightControl.DotCounts

                End Select
                'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [S]
                cbDotTypeSelect.Items.Clear()
                For i As Integer = 0 To gDotValueDB.Count - 1
                    cbDotTypeSelect.Items.Add(gDotValueDB.Keys(i))
                Next
                Dim indexfind As Integer = cbDotTypeSelect.FindStringExact([Step].Dots3D.DotParameterName)
                If (indexfind >= 0) Then
                    cbDotTypeSelect.SelectedIndex = indexfind
                End If
                'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [E]
            Case eStepFunctionType.EndLine
            Case eStepFunctionType.ExtendOff
                tabStep.SelectedTab = tabExtendOff
            Case eStepFunctionType.ExtendOn
                tabStep.SelectedTab = tabExtendOn
            Case eStepFunctionType.FirstLine
            Case eStepFunctionType.Inspect

            Case eStepFunctionType.Line3D
                '[Note]:預設值保護 mobary+ 2016.10.08

                '--- 起終點共點保護 ---   相同座標點,強制給預設值   
                If [Step].Line3D.StartPosX = [Step].Line3D.EndPosX And [Step].Line3D.StartPosY = [Step].Line3D.EndPosY Then
                    [Step].Line3D.StartPosX = 10
                    [Step].Line3D.StartPosY = 10
                    [Step].Line3D.EndPosX = -10
                    [Step].Line3D.EndPosY = -10
                End If
                '--- 起終點共點保護 ---

                tabStep.SelectedTab = tabLine3D
                grpLine3D.Text = "Line3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath) & GetBasicIndex(TmpFrmXX.SelectedNodePath)
                txtLine3DEndPosX.Text = [Step].Line3D.EndPosX
                txtLine3DEndPosY.Text = [Step].Line3D.EndPosY
                txtLine3DEndPosZ.Text = [Step].Line3D.EndPosZ
                'txtLine3DEndPosB.Text = 0
                'txtLine3DEndPosC.Text = 0
                txtLine3DPitch.Text = [Step].Line3D.Pitch
                txtLine3DStartPosX.Text = [Step].Line3D.StartPosX
                txtLine3DStartPosY.Text = [Step].Line3D.StartPosY
                txtLine3DStartPosZ.Text = [Step].Line3D.StartPosZ
                'txtLine3DStartPosB.Text = 0
                'txtLine3DStartPosC.Text = 0
                '20170120
                txtLine3DComment.Text = [Step].Line3D.Comment
20170505:
                txtLine3DDotWeight.Text = Format(RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve), "0.##########")


                '[說明]:計算Pitch長度 20161010
                mDisDx = (txtLine3DEndPosX.Text - txtLine3DStartPosX.Text)
                mDisDy = (txtLine3DEndPosY.Text - txtLine3DStartPosY.Text)
                mSubDistance = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)))

                GetLineDotLimit(mSubDistance, mDotmin, mDotmax)
                nmuLine3DDot.Maximum = mDotmax
                nmuLine3DDot.Minimum = mDotmin

                GetLineVelocityLimit(mSubDistance, mVmin, mVmax)
                nmuLine3DVelocity.Minimum = mVmin
                nmuLine3DVelocity.Maximum = mVmax
                If GetLineWeightLimit(mSubDistance, mWmin, mWmax) Then
                    nmuLine3DWeight.Minimum = mWmin
                    nmuLine3DWeight.Maximum = mWmax
                End If
                Select Case RecipeEdit.RunType
                    Case eWeightControlType.Dots

                        SetNumericUpDownValue(nmuLine3DDot, [Step].Line3D.WeightControl.DotCounts)
                        '0804
                        [Step].Line3D.WeightControl.dotPitch = mSubDistance / (Val(nmuLine3DDot.Value) - 1) * 1000
                        txtLine3DPitch.Text = [Step].Line3D.WeightControl.dotPitch.ToString("##0.###") 'dot數量減1算pitch

                        [Step].Line3D.WeightControl.Velocity = [Step].Line3D.WeightControl.dotPitch / mCycleTimes(sys.StageNo)
                        SetNumericUpDownValue(nmuLine3DVelocity, [Step].Line3D.WeightControl.Velocity)
                        [Step].Line3D.WeightControl.Weight = [Step].Line3D.WeightControl.DotCounts * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                        SetNumericUpDownValue(nmuLine3DWeight, [Step].Line3D.WeightControl.Weight)

                    Case eWeightControlType.Weight


                        SetNumericUpDownValue(nmuLine3DWeight, [Step].Line3D.WeightControl.Weight)
                        If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) > 0 Then
                            [Step].Line3D.WeightControl.DotCounts = [Step].Line3D.WeightControl.Weight / RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                            If [Step].Line3D.WeightControl.DotCounts > 0 Then
                                [Step].Line3D.WeightControl.dotPitch = mSubDistance / [Step].Line3D.WeightControl.DotCounts * 1000
                                txtLine3DPitch.Text = [Step].Line3D.WeightControl.dotPitch
                            ElseIf [Step].Line3D.WeightControl.DotCounts <= 0 Then
                                txtLine3DPitch.Text = ""
                            End If
                        End If
                        SetNumericUpDownValue(nmuLine3DDot, [Step].Line3D.WeightControl.DotCounts)
                        [Step].Line3D.WeightControl.Velocity = [Step].Line3D.WeightControl.dotPitch / mCycleTimes(sys.StageNo)
                        SetNumericUpDownValue(nmuLine3DVelocity, [Step].Line3D.WeightControl.Velocity)

                    Case eWeightControlType.Velocity


                        SetNumericUpDownValue(nmuLine3DVelocity, [Step].Line3D.WeightControl.Velocity)
                        [Step].Line3D.WeightControl.dotPitch = [Step].Line3D.WeightControl.Velocity * mCycleTimes(sys.StageNo) / 1000
                        If [Step].Line3D.WeightControl.dotPitch < 10 / 1000 Then
                            [Step].Line3D.WeightControl.dotPitch = 10 / 1000
                        End If
                        [Step].Line3D.WeightControl.DotCounts = CInt(mSubDistance / [Step].Line3D.WeightControl.dotPitch) + 1

                        SetNumericUpDownValue(nmuLine3DDot, [Step].Line3D.WeightControl.DotCounts)
                        [Step].Line3D.WeightControl.Weight = [Step].Line3D.WeightControl.DotCounts * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                        SetNumericUpDownValue(nmuLine3DWeight, [Step].Line3D.WeightControl.Weight)

                        '----------------------------------
                        'mDisDx = (txtLine3DEndPosX.Text - txtLine3DStartPosX.Text)
                        'mDisDy = (txtLine3DEndPosY.Text - txtLine3DStartPosY.Text)
                        'mSubDistance = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)))
                        'Dim mVmin As Decimal
                        'Dim mVmax As Decimal
                        'GetLineVelocityLimit(mSubDistance, mVmin, mVmax)
                        'nmuLine3DVelocity.Maximum = mVmax ' 1000 * mSubDistance / mCycleTimes(sys.StageNo) '速度上限
                        'nmuLine3DVelocity.Minimum = mVmin ' 10 / mCycleTimes(sys.StageNo) '速度下限
                        '[Step].Line3D.WeightControl.Velocity = nmuLine3DVelocity.Value
                        '[Step].Line3D.WeightControl.dotPitch = [Step].Line3D.WeightControl.Velocity * mCycleTimes(sys.StageNo) / 1000

                        '[Step].Line3D.WeightControl.DotCounts = CInt(mSubDistance / [Step].Line3D.WeightControl.dotPitch) + 1
                        'If [Step].Line3D.WeightControl.DotCounts > 1 Then
                        '    [Step].Line3D.WeightControl.dotPitch = mSubDistance / ([Step].Line3D.WeightControl.DotCounts - 1) * 1000
                        '    txtLine3DPitch.Text = [Step].Line3D.WeightControl.dotPitch.ToString("##0.###") 'dot數量減1算pitch


                        '    SetNumericUpDownValue(nmuLine3DDot, [Step].Line3D.WeightControl.DotCounts)
                        '    [Step].Line3D.WeightControl.Weight = [Step].Line3D.WeightControl.DotCounts * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                        '    SetNumericUpDownValue(nmuLine3DWeight, [Step].Line3D.WeightControl.Weight)
                        'End If


                End Select

                '20171016
                SetNumericUpDownValue(nmuLine3DStartVelocity, [Step].Line3D.StartVel)

                'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [S]
                cbLineTypeSelect.Items.Clear()
                For i As Integer = 0 To gLineValueDB.Count - 1
                    cbLineTypeSelect.Items.Add(gLineValueDB.Keys(i))
                Next
                Dim indexfind As Integer = cbLineTypeSelect.FindStringExact([Step].Line3D.LineParameterName)
                If (indexfind >= 0) Then
                    cbLineTypeSelect.SelectedIndex = indexfind
                End If
                'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [E]
            Case eStepFunctionType.Move3D
                tabStep.SelectedTab = tabMove3D
                grpMove3D.Text = "Move3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath) & GetBasicIndex(TmpFrmXX.SelectedNodePath)
                txtMove3DEndPosX.Text = [Step].Move3D.EndPosX
                txtMove3DEndPosY.Text = [Step].Move3D.EndPosY
                txtMove3DEndPosZ.Text = [Step].Move3D.EndPosZ

            Case eStepFunctionType.Picture
            Case eStepFunctionType.Rectangle

            Case eStepFunctionType.SelectValve
                tabStep.SelectedTab = tabSelectValve
                cmbValve.Items.Clear()
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        cmbValve.Items.Add("Valve1")

                    Case eMechanismModule.TwoValveOneStage
                        If gSSystemParameter.MultiDispenseEnable = True Then
                            cmbValve.Items.Add("Valve1")
                        Else
                            cmbValve.Items.Add("Valve1")
                            cmbValve.Items.Add("Valve2")
                        End If

                    Case Else
                        cmbValve.Items.Add("Valve1")

                End Select

                Select Case [Step].SelectValve.ValveNo
                    Case eValveWorkMode.Valve1
                        cmbValve.SelectedIndex = 0

                    Case eValveWorkMode.Valve2
                        If gSSystemParameter.MultiDispenseEnable = True Then
                            cmbValve.SelectedIndex = 0
                        Else
                            If cmbValve.Items.Count > 1 Then
                                cmbValve.SelectedIndex = 1
                            End If
                        End If

                    Case Else
                        cmbValve.SelectedIndex = 0
                End Select

                '=== Tilt角度選單 ===
                cmbPosB.Items.Clear()
                For i As Integer = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).DicCCDTiltValveCalib([Step].SelectValve.ValveNo).Keys.Count - 1 '對每個角度Key, 加入清單選項
                    cmbPosB.Items.Add(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).DicCCDTiltValveCalib([Step].SelectValve.ValveNo).Keys(i))
                Next

                If cmbPosB.Items.Count > 0 Then '如果有選項才做選項配接
                    If cmbPosB.Items.Contains([Step].SelectValve.PosB) Then '如果選項內有該角度, 才選
                        cmbPosB.SelectedItem = [Step].SelectValve.PosB
                    Else '校正無該角度資料, 則預選為第0項
                        cmbPosB.SelectedIndex = 0
                    End If
                End If

            Case eStepFunctionType.SubPattern
            Case eStepFunctionType.Wait
                tabStep.SelectedTab = tabWait
                txtWaitDwellTime.Text = [Step].Wait.DwellTimeInMs
        End Select

        mIsLoaded = True
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    ''' <summary>
    ''' 設定控制項的上下限
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetNumericUpDownLimit()
        nmuLine3DVelocity.Minimum = 5 '觸發板最小限制速度 4mm/s
        If gSSystemParameter.MaxDispVelocity > 0 Then
            nmuLine3DVelocity.Maximum = gSSystemParameter.MaxDispVelocity
        Else
            nmuLine3DVelocity.Maximum = 1000
        End If

    End Sub

    ''' <summary>設定Pattern位置偏移量</summary>
    ''' <param name="sender">按鍵</param>
    ''' <param name="txtPosX">要顯示的文字框</param>
    ''' <param name="txtPosY">要顯示的文字框</param>
    ''' <param name="txtPosZ">要顯示的文字框</param>
    ''' <param name="txtPosB">要顯示的文字框</param>
    ''' <param name="txtPosC">要顯示的文字框</param>
    ''' <remarks></remarks>
    Public Sub SetOffsetXYZBC(ByRef sender As Object, ByRef txtPosX As TextBox, ByRef txtPosY As TextBox, ByRef txtPosZ As TextBox, ByRef txtPosB As TextBox, ByRef txtPosC As TextBox)
        If Not RecipeEdit.Editable Then
            If TypeOf sender Is Button Then 'Soni + 2016.09.09 避免型別不同
                BtnReadOnlyBehavior(sender)
            End If
            '請先解除Recipe鎖定
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
            Exit Sub
        End If

        '取得平台編號
        Dim StageNo As Integer = CInt(TmpFrmXX.SelectedNodePath.Split(",")(1))
        If StageNo < 0 Then
            Exit Sub
        End If
        If StageNo > gSSystemParameter.StageCount Then
            Exit Sub
        End If
        If Not RecipeEdit.Node(StageNo).ContainsKey(TmpFrmXX.SelectedNodePath) Then
            Exit Sub
        End If

        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '復歸保護
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        Dim mBasicX As Decimal
        Dim mBasicY As Decimal
        Dim mBasicZ As Decimal

        '取得基準點位置
        TryGetBasicPos(StageNo, TmpFrmXX.SelectedNodePath, RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).TeachIndexX, RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).TeachIndexY, 0, mBasicX, mBasicY, mBasicZ)
        ''有實際值時套用實際值, 無實際值時,使用理論值

        Dim offsetX As Decimal = (gCMotion.GetPositionValue(sys.AxisX) - mBasicX).ToString("##0.000") '取得設定值相對位置
        Dim offsetY As Decimal = (gCMotion.GetPositionValue(sys.AxisY) - mBasicY).ToString("##0.000") '取得設定值相對位置
        Dim offsetZ As Decimal = (gCMotion.GetPositionValue(sys.AxisZ) - mBasicZ).ToString("##0.000") '取得設定值相對位置
        Dim outX As Decimal
        Dim outY As Decimal
        If RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosC = 0 Then
            outX = offsetX
            outY = offsetY
        Else
            CMath.Rotation(offsetX, offsetY, RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosC, outX, outY)
        End If
        If Not txtPosX Is Nothing Then
            txtPosX.Text = outX
        End If
        If Not txtPosY Is Nothing Then
            txtPosY.Text = outY
        End If
        If Not txtPosZ Is Nothing Then
            offsetZ = 0 '先封印 有用螺桿閥再開
            txtPosZ.Text = offsetZ
        End If
        If Not txtPosB Is Nothing Then
            txtPosB.Text = gCMotion.GetPositionValue(sys.AxisB)
        End If
        If Not txtPosC Is Nothing Then
            txtPosC.Text = gCMotion.GetPositionValue(sys.AxisC)
        End If

    End Sub

    ''' <summary>移動到Pattern位置</summary>
    ''' <param name="sender"></param>
    ''' <param name="txtPosX"></param>
    ''' <param name="txtPosY"></param>
    ''' <param name="txtPosZ"></param>
    ''' <param name="txtPosB"></param>
    ''' <param name="txtPosC"></param>
    ''' <remarks></remarks>
    Public Function MoveOffsetXYZBC(ByRef sender As Object, ByRef txtPosX As TextBox, ByRef txtPosY As TextBox, ByRef txtPosZ As TextBox, ByRef txtPosB As TextBox, ByRef txtPosC As TextBox) As Boolean
        Dim mNodeID As String = TmpFrmXX.SelectedNodePath
        Dim mStageNo As Integer = CInt(TmpFrmXX.SelectedNodePath.Split(",")(1))

        '=== 介面設定值取得 ===
        Dim mOffsetX As Decimal
        Dim mOffsetY As Decimal
        Dim mOffsetZ As Decimal


        '20170520
        If Not txtPosX Is Nothing Then
            mOffsetX = CDec(Val(txtPosX.Text))
        End If
        If Not txtPosY Is Nothing Then
            mOffsetY = CDec(Val(txtPosY.Text))
        End If
        If Not txtPosZ Is Nothing Then
            mOffsetZ = CDec(Val(txtPosZ.Text))
        End If


        '=== 介面設定值取得 ===

        '=== 旋轉修正 ===
        Dim mOutX As Decimal
        Dim mOutY As Decimal
        If TmpFrmXX.SelectedNodePath = "" Then '代選
            TmpFrmXX.treePattern.SelectedNode = TmpFrmXX.treePattern.Nodes(0)
            Return False
        End If
        If TmpFrmXX.SelectedNodePath.Length < 8 Then
            TmpFrmXX.treePattern.SelectedNode.BackColor = Color.Yellow
            System.Threading.Thread.CurrentThread.Join(300)
            TmpFrmXX.treePattern.SelectedNode.BackColor = Color.White
            Return False
        End If
        If Not RecipeEdit.Node(mStageNo).ContainsKey(TmpFrmXX.SelectedNodePath) Then '節點不存在
            TmpFrmXX.treePattern.SelectedNode.BackColor = Color.Yellow
            System.Threading.Thread.CurrentThread.Join(300)
            TmpFrmXX.treePattern.SelectedNode.BackColor = Color.White
            Return False
        End If
        If RecipeEdit.Node(mStageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosC = 0 Then
            mOutX = mOffsetX
            mOutY = mOffsetY
        Else
            CMath.Rotation(mOffsetX, mOffsetY, RecipeEdit.Node(mStageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosC, mOutX, mOutY)
        End If
        '=== 旋轉修正 ===

        '=== 當前索引基準點取得 ===
        Dim basicX As Decimal
        Dim basicY As Decimal
        Dim basicZ As Decimal
        TryGetBasicPos(mStageNo, mNodeID, RecipeEdit.Node(mStageNo)(mNodeID).TeachIndexX, RecipeEdit.Node(mStageNo)(mNodeID).TeachIndexY, 0, basicX, basicY, basicZ) '取得基準點
        '=== 當前索引基準點取得 ===

        '=== 目標位置計算 ===
        Dim posX As Decimal = mOutX + basicX
        Dim posY As Decimal = mOutY + basicY
        Dim posZ As Decimal = mOffsetZ + basicZ
        Dim posB As Decimal = 0
        Dim posC As Decimal = 0
        If Not txtPosB Is Nothing Then
            posB = CDec(Val(txtPosB.Text))
        End If
        If Not txtPosC Is Nothing Then
            posC = CDec(Val(txtPosC.Text))
        End If
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6001042, posX, posY, posZ, posB, posC), "INFO_6001042")
        '=== 目標位置計算 ===

        '=== 移動到目標位置 ===
        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal

        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        TargetPos(0) = posX
        TargetPos(1) = posY
        TargetPos(2) = posZ
        TargetPos(3) = posB
        TargetPos(4) = posC
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        '=== 移動到目標位置 ===

        UcJoyStick1.RefreshPosition()
        Return True
    End Function


    ''' <summary>編輯用步驟</summary>
    ''' <remarks></remarks>
    Dim mCStep As New CPatternStep

    Public Sub brtnContiStartDone_Click(sender As Object, e As EventArgs) Handles btnContiStartDone.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[brtnContiStartDone]" & vbTab & "Click")
        If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex

        '[說明]:判斷是新增項目,還是須修改的項目   20160820
        Dim mStepNo As Integer
        If bAddNewList = True Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex + 1
            End If
        ElseIf bAddNewList = False Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
            End If
        End If

        '[說明]:判斷是否為新增項目   20160820
        If bAddNewList = True Then
            If WriteStepList(eStepFunctionType.ContiStart) <> True Then
                Exit Sub
            End If
            bAddNewList = False
        End If

        '[說明]:防止第一次ADD
        If mStepNo > RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            mStepNo = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If

        ''--- Soni + 20160606 防無Step時更新Step ---
        'If mStepNo >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    MsgBox("Select Step First!")
        '    Exit Sub
        'End If
        ''--- Soni + 20160606 防無Step時更新Step ---



        With RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            .StepType = eStepFunctionType.ContiStart

            TmpFrmXX.ListRefresh(mStepNo)
            Dim logData As String = "TreeNode(" & nodename & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            Debug.Print(logData)
            gSyslog.Save("StepUpdate: " & logData)

            'Sue20170627
            '存檔成功 
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        End With
        mIsCanClosed = True
        'Me.Close()
    End Sub

    Public Sub btnContiEndDone_Click(sender As Object, e As EventArgs) Handles btnContiEndDone.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnContiEndDone]" & vbTab & "Click")
        If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex

        '[說明]:判斷是新增項目,還是須修改的項目   20160820
        Dim mStepNo As Integer
        If bAddNewList = True Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex + 1
            End If

        ElseIf bAddNewList = False Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
            End If
        End If

        '[說明]:防止第一次ADD
        If mStepNo > RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            mStepNo = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If


        ''--- Soni + 20160606 防無Step時更新Step ---
        'If mStepNo >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    MsgBox("Select Step First!")
        '    Exit Sub
        'End If
        ''--- Soni + 20160606 防無Step時更新Step ---


        '[說明]:判斷是否為新增項目   20160820
        If bAddNewList = True Then
            If WriteStepList(eStepFunctionType.ContiEnd) <> True Then
                Exit Sub
            End If
            bAddNewList = False
        End If

        With RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            .StepType = eStepFunctionType.ContiEnd

            TmpFrmXX.ListRefresh(mStepNo)
            Dim logData As String = "TreeNode(" & nodename & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            Debug.Print(logData)
            gSyslog.Save("StepUpdate: " & logData)
            'Sue20170627
            '存檔成功 
            'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End With
        mIsCanClosed = True
        'Me.Close()
    End Sub



#Region "圓弧運動"

    Public Sub butArcStartSet_Click(sender As Object, e As EventArgs) Handles btnArcStartSet.Click

        'Toby_20171026
        gSyslog.Save("[frmRecipe04]" & vbTab & "[tnArcStartSet]" & vbTab & "Click")
        SetOffsetXYZBC(sender, txtArcStartPosX, txtArcStartPosY, txtArcStartPosZ, Nothing, Nothing)

        'gSyslog.Save("[frmRecipe04]" & vbTab & "[btnArcCenterSet]" & vbTab & "Click")
        'If Not RecipeEdit.Editable Then
        '    BtnReadOnlyBehavior(sender)
        '    '請先解除Recipe鎖定
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012))
        '    Exit Sub
        'End If
        'If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000005), "Warn_3000005", eMessageLevel.Warning)
        '    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    Exit Sub
        'End If
        'Dim StageNo As Integer = CInt(TmpFrmXX.SelectedNodePath.Split(",")(1))
        'If StageNo < 0 Then
        '    Exit Sub
        'End If
        'If sys.StageNo > gSSystemParameter.StageCount Then
        '    Exit Sub
        'End If
        'If Not RecipeEdit.Node(StageNo).ContainsKey(TmpFrmXX.SelectedNodePath) Then
        '    Exit Sub
        'End If

        ''有實際值時套用實際值, 無實際值時,使用理論值
        'If RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosX = 0 Then
        '    RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosX = RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).BasicPositionX
        'End If

        'If RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosY = 0 Then
        '    RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosY = RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).BasicPositionY
        'End If

        'If RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosZ = 0 Then
        '    RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosZ = RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).BasicPositionZ
        'End If

        'Dim offsetX As Decimal '相對於基準點的offset
        'Dim offsetY As Decimal
        'Dim offsetZ As Decimal
        'Dim outX As Decimal '輸出
        'Dim outY As Decimal
        'offsetX = (gCMotion.GetPositionValue(sys.AxisX) - RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosX).ToString("##0.000")
        'offsetY = (gCMotion.GetPositionValue(sys.AxisY) - RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosY).ToString("##0.000")
        'offsetZ = (gCMotion.GetPositionValue(sys.AxisZ) - RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosZ).ToString("##0.000")
        'If RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosC = 0 Then
        '    outX = offsetX
        '    outY = offsetY
        'Else
        '    CMath.Rotation(offsetX, offsetY, RecipeEdit.Node(StageNo)(TmpFrmXX.SelectedNodePath).ConveyorPos(mConveyorNo).RealBasicPosC, outX, outY) '純旋轉

        'End If
        'txtArcStartPosX.Text = outX
        'txtArcStartPosY.Text = outY
        'txtArcStartPosZ.Text = offsetZ

    End Sub

    Public Sub butArcEndSet_Click(sender As Object, e As EventArgs) Handles btnArcEndSet.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnArcEndSet]" & vbTab & "Click")
        SetOffsetXYZBC(sender, txtArcEndPosX, txtArcEndPosY, txtArcEndPosZ, Nothing, Nothing)
    End Sub

    Public Sub butArcEndMove_Click(sender As Object, e As EventArgs) Handles btnArcEndMove.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnArcEndMove]" & vbTab & "Click")

        '20170602按鍵保護
        If btnArcEndMove.Enabled = False Then '防連按
            Exit Sub
        End If
        btnArcStartSet.Enabled = False
        btnArcStartMove.Enabled = False
        btnArcGetPos.Enabled = False
        btnArcMidSet.Enabled = False
        btnArcMidMove.Enabled = False
        btnArcEndSet.Enabled = False
        btnArcEndMove.Enabled = False
        btnArcRefresh.Enabled = False
        btnArcDone.Enabled = False
        btnArcCancel.Enabled = False
        UcJoyStick1.Enabled = False
        tabStep.Enabled = False

        MoveOffsetXYZBC(sender, txtArcEndPosX, txtArcEndPosY, txtArcEndPosZ, Nothing, Nothing)

        '20170602按鍵保護
        btnArcStartSet.Enabled = True
        btnArcStartMove.Enabled = True
        btnArcGetPos.Enabled = True
        btnArcMidSet.Enabled = True
        btnArcMidMove.Enabled = True
        btnArcEndSet.Enabled = True
        btnArcEndMove.Enabled = True
        btnArcRefresh.Enabled = True
        btnArcDone.Enabled = True
        btnArcCancel.Enabled = True
        UcJoyStick1.Enabled = True
        tabStep.Enabled = True

    End Sub

    Public Sub butArcStartMove_Click(sender As Object, e As EventArgs) Handles btnArcStartMove.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnArcStartMove]" & vbTab & "Click")

        '20170602按鍵保護
        If btnArcStartMove.Enabled = False Then '防連按
            Exit Sub
        End If
        btnArcStartSet.Enabled = False
        btnArcStartMove.Enabled = False
        btnArcGetPos.Enabled = False
        btnArcMidSet.Enabled = False
        btnArcMidMove.Enabled = False
        btnArcEndSet.Enabled = False
        btnArcEndMove.Enabled = False
        btnArcRefresh.Enabled = False
        btnArcDone.Enabled = False
        btnArcCancel.Enabled = False
        UcJoyStick1.Enabled = False
        tabStep.Enabled = False


        MoveOffsetXYZBC(sender, txtArcStartPosX, txtArcStartPosY, txtArcStartPosZ, Nothing, Nothing)

        '20170602按鍵保護
        btnArcStartSet.Enabled = True
        btnArcStartMove.Enabled = True
        btnArcGetPos.Enabled = True
        btnArcMidSet.Enabled = True
        btnArcMidMove.Enabled = True
        btnArcEndSet.Enabled = True
        btnArcEndMove.Enabled = True
        btnArcRefresh.Enabled = True
        btnArcDone.Enabled = True
        btnArcCancel.Enabled = True
        UcJoyStick1.Enabled = True
        tabStep.Enabled = True

    End Sub

    Public Sub btnArcCenterSet_Click(sender As Object, e As EventArgs) Handles btnArcMidSet.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnArcCenterSet]" & vbTab & "Click")
        SetOffsetXYZBC(sender, txtArcMidPosX, txtArcMidPosY, txtArcMidPosZ, Nothing, Nothing)
    End Sub

    Public Sub butArcMidMove_Click(sender As Object, e As EventArgs) Handles btnArcMidMove.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnArcMidMove]" & vbTab & "Click")
        '20170602按鍵保護
        If btnArcMidMove.Enabled = False Then '防連按
            Exit Sub
        End If
        btnArcStartSet.Enabled = False
        btnArcStartMove.Enabled = False
        btnArcGetPos.Enabled = False
        btnArcMidSet.Enabled = False
        btnArcMidMove.Enabled = False
        btnArcEndSet.Enabled = False
        btnArcEndMove.Enabled = False
        btnArcRefresh.Enabled = False
        btnArcDone.Enabled = False
        btnArcCancel.Enabled = False
        UcJoyStick1.Enabled = False
        tabStep.Enabled = False

        MoveOffsetXYZBC(sender, txtArcMidPosX, txtArcMidPosY, txtArcMidPosZ, Nothing, Nothing)    'mobary-->2016.9.24

        '20170602按鍵保護
        btnArcStartSet.Enabled = True
        btnArcStartMove.Enabled = True
        btnArcGetPos.Enabled = True
        btnArcMidSet.Enabled = True
        btnArcMidMove.Enabled = True
        btnArcEndSet.Enabled = True
        btnArcEndMove.Enabled = True
        btnArcRefresh.Enabled = True
        btnArcDone.Enabled = True
        btnArcCancel.Enabled = True
        UcJoyStick1.Enabled = True
        tabStep.Enabled = True

    End Sub

    Public Sub btnArcDone_Click(sender As Object, e As EventArgs) Handles btnArcDone.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnArcDone]" & vbTab & "Click")
        If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex

        '20170904
        If gJetValveDB.ContainsKey(mSelectedValveDBName) = True Then
            Select Case gJetValveDB(mSelectedValveDBName).ValveModel
                Case eValveModel.PicoPulse
                    mCycleTimes(sys.StageNo) = gJetValveDB(mSelectedValveDBName).PicoTouch.CycleTime
                Case eValveModel.Advanjet
                    mCycleTimes(sys.StageNo) = gJetValveDB(mSelectedValveDBName).Advanjet.CycleTime
            End Select
        Else
            Exit Sub
        End If

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                If Val(nmuArcDot.Value) < 2 Then
                    ErrMessage = "Dot Counts 不可小於2!!"
                    mIsCanClosed = False
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If
            Case eWeightControlType.Weight
                If Val(nmuArcWeight.Value) <= 0 Then
                    ErrMessage = "Weigh預設值不可小於零!!"
                    mIsCanClosed = False
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If

            Case eWeightControlType.Velocity
                If Val(nmuArcVelocity.Value) <= 0 Then
                    ErrMessage = "Velocity預設值不可小於零!!"
                    mIsCanClosed = False
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If

        End Select

        '--- 起終點共點保護 ---  20160805
        If Val(txtArcStartPosX.Text) = Val(txtArcMidPosX.Text) And Val(txtArcStartPosY.Text) = Val(txtArcMidPosY.Text) Then
            '請確認開始與中間位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000047))
            MsgBox(gMsgHandler.GetMessage(Warn_3000047), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            ' ErrMessage = "Stsrt and Mid 點位不可相等!!"
            mIsCanClosed = False
            'MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
            Exit Sub
        End If
        If Val(txtArcStartPosX.Text) = Val(txtArcEndPosX.Text) And Val(txtArcStartPosY.Text) = Val(txtArcEndPosY.Text) Then
            '請確認開始與結束位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000017))
            MsgBox(gMsgHandler.GetMessage(Warn_3000017), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'ErrMessage = "Stsrt and End 點位不可相等!!"
            mIsCanClosed = False
            'MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
            Exit Sub
        End If
        If Val(txtArcMidPosX.Text) = Val(txtArcEndPosX.Text) And Val(txtArcMidPosY.Text) = Val(txtArcEndPosY.Text) Then
            '請確認中間與結束位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000048))
            MsgBox(gMsgHandler.GetMessage(Warn_3000048), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'ErrMessage = "Mid and End 點位不可相等!!"
            mIsCanClosed = False
            'MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
            Exit Sub
        End If
        '--- 起終點共點保護 ---

        '---三點共線保護--- 公式:兩點斜率相同 為共線充要條件
        If ((txtArcEndPosY.Text - txtArcStartPosY.Text) * (txtArcMidPosX.Text - txtArcStartPosX.Text)) =
            ((txtArcMidPosY.Text - txtArcStartPosY.Text) * (txtArcEndPosX.Text - txtArcStartPosX.Text)) Then
            '請確認三點共線問題
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000049))
            MsgBox(gMsgHandler.GetMessage(Warn_3000049), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'ErrMessage = "三點共線不能成Arc!!"
            mIsCanClosed = False
            'MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
            Exit Sub
        End If
        '---三點共線保護---


        Dim Circle As Circle
        Dim x, y, z As CPoint
        'Jeffadd 20160615
        x = New CPoint(Val(txtArcStartPosX.Text), Val(txtArcStartPosY.Text))
        y = New CPoint(Val(txtArcMidPosX.Text), Val(txtArcMidPosY.Text))
        z = New CPoint(Val(txtArcEndPosX.Text), Val(txtArcEndPosY.Text))

        '[說明]:計算Arc圓心座標
        Circle = CMath.GetCircleby3Point(x, y, z)

        '[說明]:計算Pitch長度 20160920
        '[Note]:弧長=2*PI*R*Angle/360
        '           =PI*R*Angle/180
        mDisDx = (z.PointX - Circle.PointX)
        mDisDy = (z.PointY - Circle.PointY)
        mSubDistance = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)) * Math.PI * Circle.Angle / 180)

        '20170904
        Dim mMAXVelocity As Decimal
        mMAXVelocity = Premtek.CDispensingMath.GetMaxTangentialVelocity(gCMotion.SyncParameter(sys.StageNo).Velocity.Acc * gCMotion.SyncParameter(sys.StageNo).Velocity.AccRatio,
                                                             Math.Sqrt((Val(txtArcStartPosX.Text) - Val(txtArcMidPosX.Text)) ^ 2 +
                                                             (Val(txtArcStartPosY.Text) - Val(txtArcMidPosY.Text)) ^ 2))

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                'Pitch = 總長 / 點數 * 1000(mm轉um)
                Dim mPitch As Decimal = mSubDistance / Val(nmuArcDot.Value - 1) * 1000
                txtArcPitch.Text = mPitch.ToString("##0.###")
                '[說明]:檢查長度避免Trigger異常  20170329
                If Val(txtArcPitch.Text) > 1000 Then
                    Select Case gSSystemParameter.LanguageType
                        Case enmLanguageType.eEnglish
                            MsgBox("Pitch to long,Please add more dots!!!Pitch = " & Val(txtArcPitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eSimplifiedChinese
                            MsgBox("Pitch數過長,請增加顆數!!!Pitch = " & Val(txtArcPitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eTraditionalChinese
                            MsgBox("Pitch數過長,請增加顆數!!!Pitch = " & Val(txtArcPitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    mIsCanClosed = False
                    Exit Sub
                ElseIf Val(txtArcPitch.Text) < 10 Then
                    Select Case gSSystemParameter.LanguageType
                        Case enmLanguageType.eEnglish
                            MsgBox("Pitch to short,Please del more dots!!!Pitch = " & Val(txtArcPitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eSimplifiedChinese
                            MsgBox("Pitch數過短,請減少顆數!!!Pitch = " & Val(txtArcPitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eTraditionalChinese
                            MsgBox("Pitch數過短,請減少顆數!!!Pitch = " & Val(txtArcPitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    mIsCanClosed = False
                    Exit Sub
                    '20170904
                ElseIf (mMAXVelocity > 0) AndAlso (Val(txtArcPitch.Text) / mCycleTimes(sys.StageNo)) > mMAXVelocity Then
                    MsgBox("速度超過圓弧物理上限: " & (Val(txtArcPitch.Text) / mCycleTimes(sys.StageNo)).ToString & ">" & mMAXVelocity.ToString, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    mIsCanClosed = False
                    Exit Sub
                ElseIf (gSSystemParameter.MaxDispVelocity > 0) AndAlso (Val(txtArcPitch.Text) / mCycleTimes(sys.StageNo)) > gSSystemParameter.MaxDispVelocity Then
                    MsgBox("速度超過點膠速度上限: " & (Val(txtArcPitch.Text) / mCycleTimes(sys.StageNo)).ToString & ">" & gSSystemParameter.MaxDispVelocity.ToString, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    mIsCanClosed = False
                    Exit Sub
                End If

            Case eWeightControlType.Weight
                '[說明]:有做秤重才能計算Pitch
                If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) > 0 Then
                    '單點重量 = 總重 / 單顆Dot重量
                    Dot = Val(nmuArcWeight.Value) / RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                    'Pitch = 總長 / 點數 * 1000(mm轉um)
                    If Dot < 2 Then
                        txtArcPitch.Text = 0
                        ErrMessage = "重量不足以打2個Dots"
                        mIsCanClosed = False
                        MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                        Exit Sub
                    Else
                        Dim mPitch As Decimal = mSubDistance / (Dot - 1) * 1000
                        txtArcPitch.Text = mPitch.ToString("##0.###")
                        '[說明]:檢察長度避免Trigger異常  20170505
                        If Val(txtArcPitch.Text) > 1000 Then
                            Select Case gSSystemParameter.LanguageType
                                Case enmLanguageType.eEnglish
                                    MsgBox("Pitch to long!!!Pitch = " & Val(txtArcPitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmLanguageType.eSimplifiedChinese
                                    MsgBox("Pitch數過長!!!Pitch = " & Val(txtArcPitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmLanguageType.eTraditionalChinese
                                    MsgBox("Pitch數過長!!!Pitch = " & Val(txtArcPitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            End Select
                            mIsCanClosed = False
                            Exit Sub
                        ElseIf Val(txtArcPitch.Text) < 10 Then
                            Select Case gSSystemParameter.LanguageType
                                Case enmLanguageType.eEnglish
                                    MsgBox("Pitch to short!!!Pitch = " & Val(txtArcPitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmLanguageType.eSimplifiedChinese
                                    MsgBox("Pitch數過短!!!Pitch = " & Val(txtArcPitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmLanguageType.eTraditionalChinese
                                    MsgBox("Pitch數過短!!!Pitch = " & Val(txtArcPitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            End Select
                            mIsCanClosed = False
                            Exit Sub
                            '20170904
                        ElseIf (mMAXVelocity > 0) AndAlso (Val(txtArcPitch.Text) / mCycleTimes(sys.StageNo)) > mMAXVelocity Then
                            MsgBox("速度超過上限: " & (Val(txtArcPitch.Text) / mCycleTimes(sys.StageNo)).ToString & ">" & mMAXVelocity.ToString, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            mIsCanClosed = False
                            Exit Sub
                        End If

                    End If
                End If

            Case eWeightControlType.Velocity

                'TODO:待補
                '20170905
                If (mMAXVelocity > 0) AndAlso Val(nmuArcVelocity.Value) > mMAXVelocity Then
                    MsgBox("速度超過上限: " & nmuArcVelocity.Value & ">" & mMAXVelocity.ToString, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    mIsCanClosed = False
                    Exit Sub
                End If


        End Select

        '[說明]:判斷是新增項目,還是須修改的項目   20160820
        Dim mStepNo As Integer

        If bAddNewList = True Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex + 1
            End If

        ElseIf bAddNewList = False Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
            End If
        End If

        '[說明]:防止第一次ADD
        If mStepNo > RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            mStepNo = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If

        '20170327
        If cbArcTypeSelect.Text = "" Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("Please set parameter select item!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("請設定弧形速度參數!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("請設定弧形速度參數!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            mIsCanClosed = False
            Exit Sub
        End If

        '[說明]:判斷是否為新增項目   20160820
        If bAddNewList = True Then
            If WriteStepList(eStepFunctionType.Arc2D) <> True Then
                Exit Sub
            End If
            bAddNewList = False
        End If

        ''--- Soni + 20160606 防無Step時更新Step ---
        'If mStepNo >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    MsgBox("Select Step First!")
        '    Exit Sub
        'End If
        ''--- Soni + 20160606 防無Step時更新Step ---


        With RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            .StepType = eStepFunctionType.Arc2D
            'Jeffadd 20160615
            .Arc2D.StartPosX = Val(txtArcStartPosX.Text)
            .Arc2D.StartPosY = Val(txtArcStartPosY.Text)
            .Arc2D.MiddlePosX = Val(txtArcMidPosX.Text)
            .Arc2D.MiddlePosY = Val(txtArcMidPosY.Text)
            .Arc2D.EndPosX = Val(txtArcEndPosX.Text)
            .Arc2D.EndPosY = Val(txtArcEndPosY.Text)

            '20160527 新增角度與方向
            .Arc2D.CenterPosX = Circle.PointX
            .Arc2D.CenterPosY = Circle.PointY
            .Arc2D.Angle = Circle.Angle
            If Circle.clockwise = True Then
                .Arc2D.Direction = eArcDirection.CW
            ElseIf Circle.clockwise = False Then
                .Arc2D.Direction = eArcDirection.CCW
            End If

            'Jeffadd 20160615
            .Arc2D.Pitch = Val(txtArcPitch.Text)
            '20160920
            .Arc2D.Comment = txtArcComment.Text

            '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
            Select Case RecipeEdit.RunType
                Case eWeightControlType.Dots
                    .Arc2D.WeightControl.Type = eWeightControlType.Dots
                    .Arc2D.WeightControl.DotCounts = Val(nmuArcDot.Value)

                Case eWeightControlType.Weight
                    .Arc2D.WeightControl.Type = eWeightControlType.Weight
                    .Arc2D.WeightControl.Weight = Val(nmuArcWeight.Value)

                Case eWeightControlType.Velocity
                    .Arc2D.WeightControl.Type = eWeightControlType.Velocity
                    .Arc2D.WeightControl.Velocity = Val(nmuArcVelocity.Value)

            End Select

            '20171016
            .Arc2D.StartVel = Val(nmuArcStartVelocity.Value)

            .Arc2D.ArcParameterName = cbArcTypeSelect.Text 'Eason 20170216 Ticket:100080 , Add Arc Type Parameter
            mIsCanClosed = True
            TmpFrmXX.ListRefresh(mStepNo)
            Dim logData As String = "TreeNode(" & nodename & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            gSyslog.Save("StepUpdate: " & logData)
            'Sue20170627
            '存檔成功 
            'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        End With

        '[說明]:繪製Path
        Call gfrmRecipe04.DrawPCSSingleGraphics()


        'Me.Close()
    End Sub

    '20160820
    Public Function WriteStepList(ByVal stepType As eStepFunctionType) As Boolean
        Dim mStepNo As Integer

        '=== Soni / 2016.09.13 條件式順序變更 ===
        If TmpFrmXX.dgvStep.Rows.Count = 0 Then
            mStepNo = 0
        Else
            If TmpFrmXX.dgvStep.SelectedCells(0).RowIndex < 0 Then
                mStepNo = TmpFrmXX.dgvStep.Rows.Count
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex + 1
            End If

        End If
        '=== Soni / 2016.09.13 條件式順序變更 ===

        'If TmpFrmXX.dgvStep.SelectedCells(0).RowIndex < 0 Then
        '    If TmpFrmXX.dgvStep.Rows.Count = 0 Then
        '        mStepNo = 0
        '    Else
        '        mStepNo = TmpFrmXX.dgvStep.Rows.Count
        '    End If
        'Else
        '    mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex + 1
        'End If
        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Return False
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex

        '[說明]:防止第一次ADD
        If mStepNo > RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            mStepNo = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If


        '[說明]:新增後Index需指向新物件
        mCStep = New CPatternStep '新開步驟避免記憶體重複

        With mCStep
            .StepType = stepType '外部給定
            .StepNo = mStepNo
        End With

        RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Insert(mStepNo, mCStep)
        RecipeEdit.Pattern(nodename).Round(mRoundNo).StepCount = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count

        TmpFrmXX.SelectValveRefresh()
        TmpFrmXX.ListRefresh(mStepNo)

        If mStepNo >= TmpFrmXX.dgvStep.Rows.Count Then
            '請先選取Pattern Step
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000014))
            MsgBox(gMsgHandler.GetMessage(Warn_3000014), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            Return False
        End If

        If TmpFrmXX.dgvStep.Rows(mStepNo).Cells.Count = 0 Then
            MsgBox("Cell Not Exists!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If


        ' TmpFrmXX.tabView.SelectTab(0)
        'Call DrawSingleGraphics(TmpFrmXX.picPcsSingleGraph, gSYS(eSys.Manual), TmpFrmXX.mDrawIdx)
        TmpFrmXX.dgvStep.CurrentCell = TmpFrmXX.dgvStep.Rows(mStepNo).Cells(0)
        '20170616_Start_End

        Return True
    End Function

#End Region

#Region "繞圓運動"

    Public Sub butCircleStartMove_Click(sender As Object, e As EventArgs) Handles btnCircleStartMove.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnCircleStartMove]" & vbTab & "Click")

        '20170602按鍵保護
        If btnCircleStartMove.Enabled = False Then '防連按
            Exit Sub
        End If
        btnCircleStartSet.Enabled = False
        btnCircleStartMove.Enabled = False
        btnCircleMidSet.Enabled = False
        btnCircleMidMove.Enabled = False
        btnCircleEndSet.Enabled = False
        btnCircleEndMove.Enabled = False
        btnCircleRefresh.Enabled = False
        btnCircleDone.Enabled = False
        btnCircleCancel.Enabled = False
        UcJoyStick1.Enabled = False
        tabStep.Enabled = False

        MoveOffsetXYZBC(sender, txtCircleStartPosX, txtCircleStartPosY, txtCircleStartPosZ, Nothing, Nothing)

        '20170602按鍵保護
        btnCircleStartSet.Enabled = True
        btnCircleStartMove.Enabled = True
        btnCircleMidSet.Enabled = True
        btnCircleMidMove.Enabled = True
        btnCircleEndSet.Enabled = True
        btnCircleEndMove.Enabled = True
        btnCircleRefresh.Enabled = True
        btnCircleDone.Enabled = True
        btnCircleCancel.Enabled = True
        UcJoyStick1.Enabled = True
        tabStep.Enabled = True

    End Sub

    Public Sub butCircleMidMove_Click(sender As Object, e As EventArgs) Handles btnCircleMidMove.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnCircleMidMove]" & vbTab & "Click")

        '20170602按鍵保護
        If btnCircleMidMove.Enabled = False Then '防連按
            Exit Sub
        End If
        btnCircleStartSet.Enabled = False
        btnCircleStartMove.Enabled = False
        btnCircleMidSet.Enabled = False
        btnCircleMidMove.Enabled = False
        btnCircleEndSet.Enabled = False
        btnCircleEndMove.Enabled = False
        btnCircleRefresh.Enabled = False
        btnCircleDone.Enabled = False
        btnCircleCancel.Enabled = False
        UcJoyStick1.Enabled = False
        tabStep.Enabled = False

        MoveOffsetXYZBC(sender, txtCircleMidPosX, txtCircleMidPosY, txtCircleMidPosZ, Nothing, Nothing)

        '20170602按鍵保護
        btnCircleStartSet.Enabled = True
        btnCircleStartMove.Enabled = True
        btnCircleMidSet.Enabled = True
        btnCircleMidMove.Enabled = True
        btnCircleEndSet.Enabled = True
        btnCircleEndMove.Enabled = True
        btnCircleRefresh.Enabled = True
        btnCircleDone.Enabled = True
        btnCircleCancel.Enabled = True
        UcJoyStick1.Enabled = True
        tabStep.Enabled = True

    End Sub

    Public Sub butCircleEndMove_Click(sender As Object, e As EventArgs) Handles btnCircleEndMove.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnArcCenterGo]" & vbTab & "Click")

        '20170602按鍵保護
        If btnCircleEndMove.Enabled = False Then '防連按
            Exit Sub
        End If
        btnCircleStartSet.Enabled = False
        btnCircleStartMove.Enabled = False
        btnCircleMidSet.Enabled = False
        btnCircleMidMove.Enabled = False
        btnCircleEndSet.Enabled = False
        btnCircleEndMove.Enabled = False
        btnCircleRefresh.Enabled = False
        btnCircleDone.Enabled = False
        btnCircleCancel.Enabled = False
        UcJoyStick1.Enabled = False
        tabStep.Enabled = False

        MoveOffsetXYZBC(sender, txtCircleMid2PosX, txtCircleMid2PosY, txtCircleMid2PosZ, Nothing, Nothing)

        '20170602按鍵保護
        btnCircleStartSet.Enabled = True
        btnCircleStartMove.Enabled = True
        btnCircleMidSet.Enabled = True
        btnCircleMidMove.Enabled = True
        btnCircleEndSet.Enabled = True
        btnCircleEndMove.Enabled = True
        btnCircleRefresh.Enabled = True
        btnCircleDone.Enabled = True
        btnCircleCancel.Enabled = True
        UcJoyStick1.Enabled = True
        tabStep.Enabled = True

    End Sub

    Public Sub butCircleStartSet_Click(sender As Object, e As EventArgs) Handles btnCircleStartSet.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnCircleStartSet]" & vbTab & "Click")
        SetOffsetXYZBC(sender, txtCircleStartPosX, txtCircleStartPosY, txtCircleStartPosZ, Nothing, Nothing)
    End Sub

    Public Sub butCircleMidSet_Click(sender As Object, e As EventArgs) Handles btnCircleMidSet.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnCircleMidSet]" & vbTab & "Click")
        SetOffsetXYZBC(sender, txtCircleMidPosX, txtCircleMidPosY, txtCircleMidPosZ, Nothing, Nothing)
    End Sub

    Public Sub butCircleEndSet_Click(sender As Object, e As EventArgs) Handles btnCircleEndSet.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnCircleEndSet]" & vbTab & "Click")
        SetOffsetXYZBC(sender, txtCircleMid2PosX, txtCircleMid2PosY, txtCircleMid2PosZ, Nothing, Nothing)
    End Sub

    Public Sub btnCircleDone_Click(sender As Object, e As EventArgs) Handles btnCircleDone.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnCircleDone]" & vbTab & "Click")
        If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex

        If gJetValveDB.ContainsKey(mSelectedValveDBName) = True Then
            Select Case gJetValveDB(mSelectedValveDBName).ValveModel
                Case eValveModel.PicoPulse
                    mCycleTimes(sys.StageNo) = gJetValveDB(mSelectedValveDBName).PicoTouch.CycleTime
                Case eValveModel.Advanjet
                    mCycleTimes(sys.StageNo) = gJetValveDB(mSelectedValveDBName).Advanjet.CycleTime
            End Select
        Else
            Exit Sub
        End If

        '[說明]:判斷是新增項目,還是須修改的項目   20160820
        Dim mStepNo As Integer
        If bAddNewList = True Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex + 1
            End If

        ElseIf bAddNewList = False Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
            End If
        End If

        '[說明]:防止第一次ADD
        If mStepNo > RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            mStepNo = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                If Val(nmuCircleDot.Value) < 2 Then
                    ErrMessage = "Dot Counts 不可小於2!!"
                    mIsCanClosed = False
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If

            Case eWeightControlType.Weight
                If Val(nmuCircleWeight.Value) < 0 Then
                    ErrMessage = "Weigh預設值不可小於零!!"
                    mIsCanClosed = False
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If

            Case eWeightControlType.Velocity
                If Val(nmuCircleVelocity.Value) < 0 Then
                    ErrMessage = "Velocity預設值不可小於零!!"
                    mIsCanClosed = False
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If

        End Select

        '--- 起終點共點保護 ---  20160805
        If Val(txtCircleStartPosX.Text) = Val(txtCircleMidPosX.Text) And Val(txtCircleStartPosY.Text) = Val(txtCircleMidPosY.Text) Then
            '請確認開始與中間位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000047))
            MsgBox(gMsgHandler.GetMessage(Warn_3000047), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            'ErrMessage = "Stsrt and Mid 點位不可相等!!"
            mIsCanClosed = False
            'MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
            Exit Sub
        End If
        If Val(txtCircleStartPosX.Text) = Val(txtCircleMid2PosX.Text) And Val(txtCircleStartPosY.Text) = Val(txtCircleMid2PosY.Text) Then
            '請確認開始與結束位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000017))
            MsgBox(gMsgHandler.GetMessage(Warn_3000017), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            'ErrMessage = "Stsrt and End 點位不可相等!!"
            mIsCanClosed = False
            'MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
            Exit Sub
        End If
        If Val(txtCircleMidPosX.Text) = Val(txtCircleMid2PosX.Text) And Val(txtCircleMidPosY.Text) = Val(txtCircleMid2PosY.Text) Then
            '請確認中間與結束位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000048))
            MsgBox(gMsgHandler.GetMessage(Warn_3000048), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            'ErrMessage = "Mid and End 點位不可相等!!"
            mIsCanClosed = False
            'MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
            Exit Sub
        End If
        '--- 起終點共點保護 ---

        '---三點共線保護--- 公式:兩點斜率相同 為共線充要條件
        If ((txtCircleMid2PosY.Text - txtCircleStartPosY.Text) * (txtCircleMidPosX.Text - txtCircleStartPosX.Text)) =
            ((txtCircleMidPosY.Text - txtCircleStartPosY.Text) * (txtCircleMid2PosX.Text - txtCircleStartPosX.Text)) Then
            '請確認三點共線問題
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000049))
            MsgBox(gMsgHandler.GetMessage(Warn_3000049), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            'ErrMessage = "三點共線不能成Circle!!"
            mIsCanClosed = False
            'MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
            Exit Sub
        End If
        '---三點共線保護---


        Dim Circle As Circle
        Dim x, y, z As CPoint
        '[說明]:計算圓心座標及畫圓方向
        'Jeffadd 20160615
        x = New CPoint(Val(txtCircleStartPosX.Text), Val(txtCircleStartPosY.Text))
        y = New CPoint(Val(txtCircleMidPosX.Text), Val(txtCircleMidPosY.Text))
        z = New CPoint(Val(txtCircleMid2PosX.Text), Val(txtCircleMid2PosY.Text))

        '[說明]:計算圓心座標及畫圓方向
        Circle = CMath.GetCircleby3Point(x, y, z)

        '[說明]:計算Pitch長度 20160920
        '[Note]:弧長= 2 * PI * R * Angle / 360
        '           = 2 * PI * R * 360 / 360
        '           = 2 * PI * R
        mDisDx = (z.PointX - Circle.PointX)
        mDisDy = (z.PointY - Circle.PointY)
        mSubDistance = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)) * Math.PI * 2)

        Dim mMAXVelocity As Decimal
        mMAXVelocity = Premtek.CDispensingMath.GetMaxTangentialVelocity(gCMotion.SyncParameter(sys.StageNo).Velocity.Acc * gCMotion.SyncParameter(sys.StageNo).Velocity.AccRatio,
                                                             mSubDistance)


        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                'Pitch = 總長 / 點數 * 1000(mm轉um)
                txtCirclePitch.Text = mSubDistance / Val(nmuCircleDot.Value) * 1000


                '[說明]:檢察長度避免Trigger異常  20170329
                If Val(txtCirclePitch.Text) > 1000 Then
                    Select Case gSSystemParameter.LanguageType
                        Case enmLanguageType.eEnglish
                            MsgBox("Pitch to long,Please add more dots!!!Pitch = " & Val(txtCirclePitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eSimplifiedChinese
                            MsgBox("Pitch數過長,請增加顆數!!!Pitch = " & Val(txtCirclePitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eTraditionalChinese
                            MsgBox("Pitch數過長,請增加顆數!!!Pitch = " & Val(txtCirclePitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    mIsCanClosed = False
                    Exit Sub
                ElseIf Val(txtCirclePitch.Text) < 10 Then
                    Select Case gSSystemParameter.LanguageType
                        Case enmLanguageType.eEnglish
                            MsgBox("Pitch to short,Please del more dots!!!Pitch = " & Val(txtCirclePitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eSimplifiedChinese
                            MsgBox("Pitch數過短,請減少顆數!!!Pitch = " & Val(txtCirclePitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eTraditionalChinese
                            MsgBox("Pitch數過短,請減少顆數!!!Pitch = " & Val(txtCirclePitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    mIsCanClosed = False
                    Exit Sub
                ElseIf (mMAXVelocity > 0) AndAlso (Val(txtCirclePitch.Text) / mCycleTimes(sys.StageNo)) > mMAXVelocity Then
                    MsgBox("速度超過上限: " & (Val(txtCirclePitch.Text) / mCycleTimes(sys.StageNo)).ToString & ">" & mMAXVelocity.ToString, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    mIsCanClosed = False
                    Exit Sub

                End If
            Case eWeightControlType.Weight
                '[說明]:有做秤重才能計算Pitch
                If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) > 0 Then
                    '單點重量 = 總重 / 單顆Dot重量
                    Dot = Val(nmuCircleWeight.Value) / RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                    'Pitch = 總長 / 點數 * 1000(mm轉um)
                    If Dot < 2 Then
                        txtCirclePitch.Text = 0
                        ErrMessage = "重量不足以打2個Dots"
                        mIsCanClosed = False
                        MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                        Exit Sub
                    Else
                        'txtCirclePitch.Text = mSubDistance / Dot * 1000
                        txtCirclePitch.Text = (mSubDistance / Dot * 1000).ToString("##0.###")

                        '[說明]:檢察長度避免Trigger異常  20170505
                        If Val(txtCirclePitch.Text) > 1000 Then
                            Select Case gSSystemParameter.LanguageType
                                Case enmLanguageType.eEnglish
                                    MsgBox("Pitch to long!!!Pitch = " & Val(txtCirclePitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmLanguageType.eSimplifiedChinese
                                    MsgBox("Pitch數過長!!!Pitch = " & Val(txtCirclePitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmLanguageType.eTraditionalChinese
                                    MsgBox("Pitch數過長!!!Pitch = " & Val(txtCirclePitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            End Select
                            mIsCanClosed = False
                            Exit Sub
                        ElseIf Val(txtCirclePitch.Text) < 10 Then
                            Select Case gSSystemParameter.LanguageType
                                Case enmLanguageType.eEnglish
                                    MsgBox("Pitch to short!!!Pitch = " & Val(txtCirclePitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmLanguageType.eSimplifiedChinese
                                    MsgBox("Pitch數過短!!!Pitch = " & Val(txtCirclePitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmLanguageType.eTraditionalChinese
                                    MsgBox("Pitch數過短!!!Pitch = " & Val(txtCirclePitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            End Select
                            mIsCanClosed = False
                            Exit Sub
                        ElseIf (mMAXVelocity > 0) AndAlso (Val(txtCirclePitch.Text) / mCycleTimes(sys.StageNo)) > mMAXVelocity Then
                            MsgBox("速度超過上限: " & (Val(txtCirclePitch.Text) / mCycleTimes(sys.StageNo)).ToString & ">" & mMAXVelocity.ToString, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            mIsCanClosed = False
                            Exit Sub
                        End If

                    End If
                End If

            Case eWeightControlType.Velocity
                'TODO:待補
                If (mMAXVelocity > 0) AndAlso Val(nmuCircleVelocity.Value) > mMAXVelocity Then
                    MsgBox("速度超過上限: " & nmuCircleVelocity.Value & ">" & mMAXVelocity.ToString, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    mIsCanClosed = False
                    Exit Sub
                End If


        End Select

        '20170327
        If cbCircleTypeSelect.Text = "" Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("Please set parameter select item!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("請設定圆形速度參數!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("請設定圓形速度參數!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            mIsCanClosed = False
            Exit Sub
        End If

        '[說明]:判斷是否為新增項目   20160820
        If bAddNewList = True Then
            If WriteStepList(eStepFunctionType.Circle2D) <> True Then
                Exit Sub
            End If
            bAddNewList = False
        End If


        ''--- Soni + 20160606 防無Step時更新Step ---
        'If mStepNo >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    MsgBox("Select Step First!")
        '    Exit Sub
        'End If
        ''--- Soni + 20160606 防無Step時更新Step ---

        With RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            .StepType = eStepFunctionType.Circle2D
            'Jeffadd 20160615
            .Circle2D.StartPosX = Val(txtCircleStartPosX.Text)
            .Circle2D.StartPosY = Val(txtCircleStartPosY.Text)
            .Circle2D.MiddlePosX = Val(txtCircleMidPosX.Text)
            .Circle2D.MiddlePosY = Val(txtCircleMidPosY.Text)
            .Circle2D.Middle2PosX = Val(txtCircleMid2PosX.Text)
            .Circle2D.Middle2PosY = Val(txtCircleMid2PosY.Text)
            .Circle2D.EndPosX = Val(txtCircleStartPosX.Text)
            .Circle2D.EndPosY = Val(txtCircleStartPosY.Text)
            .Circle2D.CenterPosX = Circle.PointX
            .Circle2D.CenterPosY = Circle.PointY

            If Circle.clockwise = True Then
                .Circle2D.Direction = eArcDirection.CW
            ElseIf Circle.clockwise = False Then
                .Circle2D.Direction = eArcDirection.CCW
            End If

            .Circle2D.Pitch = Val(txtCirclePitch.Text)
            '20160920
            .Circle2D.Comment = txtCircleComment.Text

            '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
            Select Case RecipeEdit.RunType
                Case eWeightControlType.Dots
                    .Circle2D.WeightControl.Type = eWeightControlType.Dots
                    .Circle2D.WeightControl.DotCounts = Val(nmuCircleDot.Value)

                Case eWeightControlType.Weight
                    .Circle2D.WeightControl.Type = eWeightControlType.Weight
                    .Circle2D.WeightControl.Weight = Val(nmuCircleWeight.Value)

                Case eWeightControlType.Velocity
                    .Circle2D.WeightControl.Type = eWeightControlType.Velocity
                    .Circle2D.WeightControl.Velocity = Val(nmuCircleVelocity.Value)

            End Select

            '20171016
            .Circle2D.StartVel = Val(nmuCircleStartVelocity.Value)

            .Circle2D.ArcParameterName = cbCircleTypeSelect.Text 'Eason 20170216 Ticket:100080 , Add Arc Type Parameter
            mIsCanClosed = True
            TmpFrmXX.ListRefresh(mStepNo)
            Dim logData As String = "TreeNode(" & nodename & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            Debug.Print(logData)
            gSyslog.Save("StepUpdate: " & logData)
            'Sue20170627
            '存檔成功 
            'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End With

        '20160808
        Call gfrmRecipe04.DrawPCSSingleGraphics()
        'Me.Close()
    End Sub
    '20170616_End

#End Region

#Region "直線運動"

    Public Sub butLine3DStartSet_Click(sender As Object, e As EventArgs) Handles btnLine3DStartSet.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnLine3DStartSet]" & vbTab & "Click")
        'Sue0511
        SetOffsetXYZBC(sender, txtLine3DStartPosX, txtLine3DStartPosY, txtLine3DStartPosZ, Nothing, Nothing)
    End Sub

    Public Sub butLine3DEndSet_Click(sender As Object, e As EventArgs) Handles btnLine3DEndSet.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnLine3DEndSet]" & vbTab & "Click")
        'Sue0511
        SetOffsetXYZBC(sender, txtLine3DEndPosX, txtLine3DEndPosY, txtLine3DEndPosZ, Nothing, Nothing)
    End Sub

    Public Sub butLine3DStartMove_Click(sender As Object, e As EventArgs) Handles btnLine3DStartMove.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnLine3DStartMove]" & vbTab & "Click")
        '20170602按鍵保護
        If btnLine3DStartMove.Enabled = False Then '防連按
            Exit Sub
        End If
        btnLine3DStartSet.Enabled = False
        btnLine3DStartMove.Enabled = False
        btnLine3DGetPos.Enabled = False
        btnLine3DEndSet.Enabled = False
        btnLine3DEndMove.Enabled = False
        btnLine3DRefresh.Enabled = False
        btnLine3DDone.Enabled = False
        btnLine3DCancel.Enabled = False
        UcJoyStick1.Enabled = False
        tabStep.Enabled = False

        'Sue0511
        MoveOffsetXYZBC(sender, txtLine3DStartPosX, txtLine3DStartPosY, txtLine3DStartPosZ, Nothing, Nothing)

        '20170602按鍵保護
        btnLine3DStartSet.Enabled = True
        btnLine3DStartMove.Enabled = True
        btnLine3DGetPos.Enabled = True
        btnLine3DEndSet.Enabled = True
        btnLine3DEndMove.Enabled = True
        btnLine3DRefresh.Enabled = True
        btnLine3DDone.Enabled = True
        btnLine3DCancel.Enabled = True
        UcJoyStick1.Enabled = True
        tabStep.Enabled = True

    End Sub

    Public Sub butLine3DEndMove_Click(sender As Object, e As EventArgs) Handles btnLine3DEndMove.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnLine3DEndMove]" & vbTab & "Click")

        '20170602按鍵保護
        If btnLine3DEndMove.Enabled = False Then '防連按
            Exit Sub
        End If
        btnLine3DStartSet.Enabled = False
        btnLine3DStartMove.Enabled = False
        btnLine3DGetPos.Enabled = False
        btnLine3DEndSet.Enabled = False
        btnLine3DEndMove.Enabled = False
        btnLine3DRefresh.Enabled = False
        btnLine3DDone.Enabled = False
        btnLine3DCancel.Enabled = False
        UcJoyStick1.Enabled = False
        tabStep.Enabled = False


        'Sue0511
        MoveOffsetXYZBC(sender, txtLine3DEndPosX, txtLine3DEndPosY, txtLine3DEndPosZ, Nothing, Nothing)

        '20170602按鍵保護
        btnLine3DStartSet.Enabled = True
        btnLine3DStartMove.Enabled = True
        btnLine3DGetPos.Enabled = True
        btnLine3DEndSet.Enabled = True
        btnLine3DEndMove.Enabled = True
        btnLine3DRefresh.Enabled = True
        btnLine3DDone.Enabled = True
        btnLine3DCancel.Enabled = True
        UcJoyStick1.Enabled = True
        tabStep.Enabled = True

    End Sub

    Public Sub btnLine3DDone_Click(sender As Object, e As EventArgs) Handles btnLine3DDone.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnLine3DDone]" & vbTab & "Click")
        If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If

        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex

        '[說明]:判斷是新增項目,還是須修改的項目   20160820
        Dim mStepNo As Integer
        If bAddNewList = True Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex + 1
            End If

        ElseIf bAddNewList = False Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
            End If
        End If

        '[說明]:防止第一次ADD
        If mStepNo > RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            mStepNo = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                If Val(nmuLine3DDot.Value) < 2 Then
                    ErrMessage = "Dot Counts 不可小於2!!"
                    mIsCanClosed = False
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If

            Case eWeightControlType.Weight
                If Val(nmuLine3DWeight.Value) < 0 Then
                    ErrMessage = "Weight預設值不可小於零!!"
                    mIsCanClosed = False
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If

            Case eWeightControlType.Velocity
                If Val(nmuLine3DVelocity.Value) < 0 Then
                    ErrMessage = "Velocity預設值不可小於零!!"
                    mIsCanClosed = False
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If

        End Select


        '--- 起終點共點保護 ---   20160808
        If Val(txtLine3DStartPosX.Text) = Val(txtLine3DEndPosX.Text) And
           Val(txtLine3DStartPosY.Text) = Val(txtLine3DEndPosY.Text) Then
            '請確認開始與結束位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000017))
            MsgBox(gMsgHandler.GetMessage(Warn_3000017), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            'ErrMessage = "Stsrt and End 點位不可相等!!"
            mIsCanClosed = False
            'MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
            Exit Sub
        End If
        '--- 起終點共點保護 ---

        '[說明]:計算Pitch長度 20160920
        mDisDx = (txtLine3DEndPosX.Text - txtLine3DStartPosX.Text)
        mDisDy = (txtLine3DEndPosY.Text - txtLine3DStartPosY.Text)
        mSubDistance = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)))

        '20170904

        If gJetValveDB.ContainsKey(mSelectedValveDBName) = True Then
            Select Case gJetValveDB(mSelectedValveDBName).ValveModel
                Case eValveModel.PicoPulse
                    mCycleTimes(sys.StageNo) = gJetValveDB(mSelectedValveDBName).PicoTouch.CycleTime
                Case eValveModel.Advanjet
                    mCycleTimes(sys.StageNo) = gJetValveDB(mSelectedValveDBName).Advanjet.CycleTime
            End Select
        Else
            Exit Sub
        End If

        Dim mPitch As Decimal

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                'Pitch = 總長 / 點數 * 1000(mm轉um)
                'txtLine3DPitch.Text = mSubDistance / Val(txtLine3DDot.Text) * 1000
                '0804
                mPitch = mSubDistance / (Val(nmuLine3DDot.Value) - 1) * 1000
                txtLine3DPitch.Text = mPitch.ToString("##0.###") 'dot數量減1算pitch

                '[說明]:檢察長度避免Trigger異常  20170329
                If Val(txtLine3DPitch.Text) > 1000 Then
                    Select Case gSSystemParameter.LanguageType
                        Case enmLanguageType.eEnglish
                            MsgBox("Pitch to long,Please add more dots!!!Pitch = " & Val(txtLine3DPitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eSimplifiedChinese
                            MsgBox("Pitch數過長,請增加顆數!!!Pitch = " & Val(txtLine3DPitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eTraditionalChinese
                            MsgBox("Pitch數過長,請增加顆數!!!Pitch = " & Val(txtLine3DPitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    mIsCanClosed = False
                    Exit Sub
                ElseIf Val(txtLine3DPitch.Text) < 10 Then
                    Select Case gSSystemParameter.LanguageType
                        Case enmLanguageType.eEnglish
                            MsgBox("Pitch to short,Please del more dots!!!Pitch = " & Val(txtLine3DPitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eSimplifiedChinese
                            MsgBox("Pitch數過短,請減少顆數!!!Pitch = " & Val(txtLine3DPitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmLanguageType.eTraditionalChinese
                            MsgBox("Pitch數過短,請減少顆數!!!Pitch = " & Val(txtLine3DPitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    mIsCanClosed = False
                    Exit Sub
                    '20170904 超過Max Velocity
                ElseIf (gSSystemParameter.MaxDispVelocity > 0) AndAlso Val(txtLine3DPitch.Text) / mCycleTimes(sys.StageNo) > gSSystemParameter.MaxDispVelocity Then
                    MsgBox("速度超過上限: " & (Val(txtLine3DPitch.TabIndex) / mCycleTimes(sys.StageNo)) & ">" & gSSystemParameter.MaxDispVelocity, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    mIsCanClosed = False
                    Exit Sub
                End If
            Case eWeightControlType.Weight
                '[說明]:有做秤重才能計算Pitch
                If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) > 0 Then
                    '0804
                    '單點重量 = 總重 / 單顆Dot重量
                    Dot = (Val(nmuLine3DWeight.Value) / RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)) - 1 'Soni 2017.07.21 數量減1
                    '單點重量 = 總重 / 單顆Dot重量
                    'Dot = CInt(Val(txtLine3DWeight.Text) / RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve))
                    'Pitch = 總長 / 點數 * 1000(mm轉um)
                    If Dot < 2 Then
                        txtLine3DPitch.Text = 0
                        ErrMessage = "重量不足以打2個Dots"
                        mIsCanClosed = False
                        MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                        Exit Sub
                    Else
                        txtLine3DPitch.Text = mSubDistance / Dot * 1000
                        '[說明]:檢察長度避免Trigger異常  20170505
                        If Val(txtLine3DPitch.Text) > 1000 Then
                            Select Case gSSystemParameter.LanguageType
                                Case enmLanguageType.eEnglish
                                    MsgBox("Pitch to long,Please add more dots!!!Pitch = " & Val(txtLine3DPitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmLanguageType.eSimplifiedChinese
                                    MsgBox("Pitch數過長,請增加顆數!!!Pitch = " & Val(txtLine3DPitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmLanguageType.eTraditionalChinese
                                    MsgBox("Pitch數過長,請增加顆數!!!Pitch = " & Val(txtLine3DPitch.Text) & ">" & "1000", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            End Select
                            mIsCanClosed = False
                            Exit Sub
                        ElseIf Val(txtLine3DPitch.Text) < 10 Then
                            Select Case gSSystemParameter.LanguageType
                                Case enmLanguageType.eEnglish
                                    MsgBox("Pitch to short,Please del more dots!!!Pitch = " & Val(txtLine3DPitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmLanguageType.eSimplifiedChinese
                                    MsgBox("Pitch數過短,請減少顆數!!!Pitch = " & Val(txtLine3DPitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmLanguageType.eTraditionalChinese
                                    MsgBox("Pitch數過短,請減少顆數!!!Pitch = " & Val(txtLine3DPitch.Text) & "<" & "10", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            End Select
                            mIsCanClosed = False
                            Exit Sub
                            '20170904
                        ElseIf (gSSystemParameter.MaxDispVelocity > 0) AndAlso (Val(txtLine3DPitch.TabIndex) / mCycleTimes(sys.StageNo)) > gSSystemParameter.MaxDispVelocity Then
                            MsgBox("速度超過上限: " & Val(txtLine3DPitch.TabIndex) / mCycleTimes(sys.StageNo) & "<" & gSSystemParameter.MaxDispVelocity, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            mIsCanClosed = False
                            Exit Sub

                        End If
                    End If
                End If

            Case eWeightControlType.Velocity
                'TODO:待補
                '20170905
                If (gSSystemParameter.MaxDispVelocity > 0) AndAlso Val(nmuLine3DVelocity.Value) > gSSystemParameter.MaxDispVelocity Then
                    MsgBox("速度超過上限: " & nmuLine3DVelocity.Value & ">" & gSSystemParameter.MaxDispVelocity.ToString, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    mIsCanClosed = False
                    Exit Sub
                End If
        End Select


        '20170215
        If cbLineTypeSelect.Text = "" Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("Please set parameter select item!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("請設定線段速度參數!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("請設定線段速度參數!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            mIsCanClosed = False
            Exit Sub
        End If

        '[說明]:判斷是否為新增項目   20160820
        If bAddNewList = True Then
            If WriteStepList(eStepFunctionType.Line3D) <> True Then
                Exit Sub
            End If
            bAddNewList = False
        End If

        ''--- Soni + 20160606 防無Step時更新Step ---
        'If mStepNo >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    MsgBox("Select Step First!")
        '    Exit Sub
        'End If
        ''--- Soni + 20160606 防無Step時更新Step ---

        With RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            .StepType = eStepFunctionType.Line3D
            'Jeffadd 20160615
            .Line3D.StartPosX = Val(txtLine3DStartPosX.Text)
            .Line3D.StartPosY = Val(txtLine3DStartPosY.Text)
            .Line3D.StartPosZ = Val(txtLine3DStartPosZ.Text)
            .Line3D.EndPosX = Val(txtLine3DEndPosX.Text)
            .Line3D.EndPosY = Val(txtLine3DEndPosY.Text)
            .Line3D.EndPosZ = Val(txtLine3DEndPosZ.Text)
            .Line3D.Pitch = Val(txtLine3DPitch.Text)
            '20160920
            .Line3D.Comment = txtLine3DComment.Text

            '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
            Select Case RecipeEdit.RunType
                Case eWeightControlType.Dots
                    .Line3D.WeightControl.Type = eWeightControlType.Dots
                    .Line3D.WeightControl.DotCounts = Val(nmuLine3DDot.Value)

                Case eWeightControlType.Weight
                    .Line3D.WeightControl.Type = eWeightControlType.Weight
                    .Line3D.WeightControl.Weight = Val(nmuLine3DWeight.Value)

                Case eWeightControlType.Velocity
                    .Line3D.WeightControl.Type = eWeightControlType.Velocity
                    .Line3D.WeightControl.Velocity = Val(nmuLine3DVelocity.Value)

            End Select
            '20171016
            .Line3D.StartVel = Val(nmuLine3DStartVelocity.Value)

            .Line3D.LineParameterName = cbLineTypeSelect.Text 'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter
            TmpFrmXX.ListRefresh(mStepNo)

            Dim logData As String = "TreeNode(" & nodename & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            Debug.Print(logData)
            gSyslog.Save("StepUpdate: " & logData)
            'Sue20170627
            '存檔成功 
            'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        End With

        '20160808
        Call gfrmRecipe04.DrawPCSSingleGraphics()
        mIsCanClosed = True
        'Me.Close()
    End Sub

#End Region

#Region "純移動"

    Public Sub btnMove3DSet_Click(sender As Object, e As EventArgs) Handles btnMove3DSet.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnMove3DSet]" & vbTab & "Click")
        'Sue0511
        SetOffsetXYZBC(sender, txtMove3DEndPosX, txtMove3DEndPosY, txtMove3DEndPosZ, Nothing, Nothing)
    End Sub
    Public Sub btnMove3DGo_Click(sender As Object, e As EventArgs) Handles btnMove3DGo.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnMove3DGo]" & vbTab & "Click")
        '20170602按鍵保護
        If btnMove3DGo.Enabled = False Then '防連按
            Exit Sub
        End If
        btnMove3DGo.Enabled = False
        btnMove3DSet.Enabled = False
        btnMove3DDone.Enabled = False
        btnMove3DCancel.Enabled = False
        UcJoyStick1.Enabled = False
        tabStep.Enabled = False

        'Sue0511
        MoveOffsetXYZBC(sender, txtMove3DEndPosX, txtMove3DEndPosY, txtMove3DEndPosZ, Nothing, Nothing)

        '20170602按鍵保護
        btnMove3DGo.Enabled = True
        btnMove3DSet.Enabled = True
        btnMove3DDone.Enabled = True
        btnMove3DCancel.Enabled = True
        UcJoyStick1.Enabled = True
        tabStep.Enabled = True

    End Sub
    Public Sub btnMove3DDone_Click(sender As Object, e As EventArgs) Handles btnMove3DDone.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnStepUpdate]" & vbTab & "Click")
        If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex

        '[說明]:判斷是新增項目,還是須修改的項目   20160820
        Dim mStepNo As Integer
        If bAddNewList = True Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex + 1
            End If
        ElseIf bAddNewList = False Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
            End If
        End If

        '[說明]:防止第一次ADD
        If mStepNo > RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            mStepNo = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If



        '[說明]:判斷是否為新增項目   20160820
        If bAddNewList = True Then
            If WriteStepList(eStepFunctionType.Move3D) <> True Then
                Exit Sub
            End If
            bAddNewList = False
        End If

        ''--- Soni + 20160606 防無Step時更新Step ---
        'If mStepNo >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    MsgBox("Select Step First!")
        '    Exit Sub
        'End If
        ''--- Soni + 20160606 防無Step時更新Step ---

        With RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            .StepType = eStepFunctionType.Move3D

            'Jeffadd 20160615
            .Move3D.EndPosX = Val(txtMove3DEndPosX.Text)
            .Move3D.EndPosY = Val(txtMove3DEndPosY.Text)
            .Move3D.EndPosZ = Val(txtMove3DEndPosZ.Text)

            TmpFrmXX.ListRefresh(mStepNo)
            Dim logData As String = "TreeNode(" & nodename & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            Debug.Print(logData)
            gSyslog.Save("StepUpdate: " & logData)
            'Sue20170627
            '存檔成功 
            'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End With
        'Me.Close()
    End Sub

#End Region

#Region "點運動"
    Public Sub btnDots3DSet_Click(sender As Object, e As EventArgs) Handles btnDots3DSet.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnDots3DSet]" & vbTab & "Click")
        'Sue0511
        SetOffsetXYZBC(sender, txtDots3DPosX, txtDots3DPosY, txtDots3DPosZ, Nothing, Nothing)
    End Sub

    Public Sub btnDots3DGo_Click(sender As Object, e As EventArgs) Handles btnDots3DGo.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnDots3DGo]" & vbTab & "Click")
        '20170602按鍵保護
        If btnDots3DGo.Enabled = False Then '防連按
            Exit Sub
        End If
        btnDots3DSet.Enabled = False
        btnDots3DGo.Enabled = False
        btnDot3DGetPos.Enabled = False
        btnDots3DDone.Enabled = False
        btnDot3DCancel.Enabled = False
        UcJoyStick1.Enabled = False
        tabStep.Enabled = False

        'Sue0511
        MoveOffsetXYZBC(sender, txtDots3DPosX, txtDots3DPosY, Nothing, Nothing, Nothing)

        '20170602按鍵保護
        btnDots3DSet.Enabled = True
        btnDots3DGo.Enabled = True
        btnDot3DGetPos.Enabled = True
        btnDots3DDone.Enabled = True
        btnDot3DCancel.Enabled = True
        UcJoyStick1.Enabled = True
        tabStep.Enabled = True

    End Sub

    Public Sub btnDots3DDone_Click(sender As Object, e As EventArgs) Handles btnDots3DDone.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnStepUpdate]" & vbTab & "Click")
        If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If

        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text

        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex

        '[說明]:判斷是新增項目,還是須修改的項目   20160820
        Dim mStepNo As Integer
        If bAddNewList = True Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex + 1
            End If
        ElseIf bAddNewList = False Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
            End If
        End If

        '[說明]:防止第一次ADD
        If mStepNo > RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            mStepNo = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If

        '20170215
        If cbDotTypeSelect.Text = "" Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("Please set parameter select item!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("請設定點數速度參數!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("請設定點數速度參數!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            mIsCanClosed = False
            Exit Sub
        End If

        '[說明]:判斷是否為新增項目   20160820
        If bAddNewList = True Then
            If WriteStepList(eStepFunctionType.Dots3D) <> True Then
                Exit Sub
            End If
            bAddNewList = False
        End If

        ''--- Soni + 20160606 防無Step時更新Step ---
        'If mStepNo >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    MsgBox("Select Step First!")
        '    Exit Sub
        'End If
        ''--- Soni + 20160606 防無Step時更新Step ---

        With RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            .StepType = eStepFunctionType.Dots3D
            .Dots3D.PosX = Val(txtDots3DPosX.Text)
            .Dots3D.PosY = Val(txtDots3DPosY.Text)
            'Sue0511
            '.Dots3D.PosZ = Val(txtDots3DPosZ.Text)

            '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
            '暫時解法為只要是Dot就強制切成速度模式

            '[Note]:不再限制打點數量
            Select Case RecipeEdit.RunType
                Case eWeightControlType.Velocity
                    .Dots3D.WeightControl.Type = eWeightControlType.Velocity
                    .Dots3D.WeightControl.Velocity = Val(txtDots3DVelocity.Text)

                Case eWeightControlType.Dots
                    .Dots3D.WeightControl.Type = eWeightControlType.Dots
                    .Dots3D.WeightControl.DotCounts = Val(txtDots3DDot.Text)

                Case eWeightControlType.Weight
                    .Dots3D.WeightControl.Type = eWeightControlType.Weight
                    .Dots3D.WeightControl.Weight = Val(txtDot3DWeight.Text)

            End Select

            '20171016
            .Dots3D.StartVel = 0

            'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter
            .Dots3D.DotParameterName = cbDotTypeSelect.Text
            '--- 參數保護 ---
            TmpFrmXX.ListRefresh(mStepNo)
            Dim logData As String = "TreeNode(" & nodename & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            Debug.Print(logData)
            gSyslog.Save("StepUpdate: " & logData)
            'Sue20170627
            '存檔成功 
            'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End With


        '20160808
        Call gfrmRecipe04.DrawPCSSingleGraphics()
        'Me.Close()
    End Sub

#End Region

#Region "基本方法"
    ''' <summary>取得基準位置</summary>
    ''' <param name="stageNo">所在Stage</param>
    ''' <param name="nodeID">所在節點</param>
    ''' <param name="indexX">所在顆索引</param>
    ''' <param name="indexY">所在顆索引</param>
    ''' <param name="basicX">傳回基準位置X</param>
    ''' <param name="basicY">傳回基準位置Y</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function TryGetBasicPos(ByVal stageNo As Integer, ByVal nodeID As String, ByVal indexX As Integer, ByVal indexY As Integer, ByRef isAligned As Boolean, ByRef basicX As Decimal, ByRef basicY As Decimal, ByRef basicZ As Decimal) As Boolean

        With RecipeEdit.Node(stageNo)(nodeID)
            Dim mBasicX As Decimal = .ConveyorPos(mConveyorNo).BasicPositionX
            Dim mBasicY As Decimal = .ConveyorPos(mConveyorNo).BasicPositionY
            Dim mBasicZ As Decimal = .ConveyorPos(mConveyorNo).BasicPositionZ
            Dim mIsAligned As Boolean
            If Not (.ConveyorPos(mConveyorNo).RealBasicPosX = 0 And .ConveyorPos(mConveyorNo).RealBasicPosY = 0) Then 'And .ConveyorPos(mConveyorNo).RealBasicPosZ = 0) Then '任一不為0則表示有教導
                mBasicX = .ConveyorPos(mConveyorNo).RealBasicPosX
                mBasicY = .ConveyorPos(mConveyorNo).RealBasicPosY
                mBasicZ = .ConveyorPos(mConveyorNo).RealBasicPosZ
                mIsAligned = True
            End If
            '=== 教導顆 陣列索引修正 ==
            Dim mMultiArrayAdapter = New CMultiArrayAdapter(RecipeEdit.Node(stageNo)(nodeID).Array)
            Dim offsetX As Decimal = mMultiArrayAdapter.GetMemoryPosX(indexX, indexY)
            Dim offsetY As Decimal = mMultiArrayAdapter.GetMemoryPosY(indexX, indexY)
            mBasicX += offsetX
            mBasicY += offsetY
            '=== 教導顆 陣列索引修正 ==
            basicX = mBasicX
            basicY = mBasicY
            basicZ = mBasicZ
            Return True
        End With
    End Function
    ''' <summary>取得基準位置字串</summary>
    ''' <param name="selectedNodePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetNodeBasicPos(ByVal selectedNodePath As String) As String
        If selectedNodePath.Length < 8 Then '資料長度不足
            Return "Basic(NA,NA,NA)"
        End If
        Dim mStageNo As Integer = Val(selectedNodePath.Substring(2, 1)) '0,0,1,0,2,0 取第二個字元後一個字元
        If mStageNo >= gSSystemParameter.StageCount Then 'Stage索引超過範圍
            Return "Basic(NA,NA,NA)"
        End If
        If mStageNo < 0 Then 'Stage索引超過範圍
            Return "Basic(NA,NA,NA)"
        End If
        If Not RecipeEdit.Node(mStageNo).ContainsKey(selectedNodePath) Then
            Return "Basic(NA,NA,NA)"
        End If
        With RecipeEdit.Node(mStageNo)(selectedNodePath)

            Dim basicX As Decimal = .ConveyorPos(mConveyorNo).BasicPositionX
            Dim basicY As Decimal = .ConveyorPos(mConveyorNo).BasicPositionY
            Dim basicZ As Decimal = .ConveyorPos(mConveyorNo).BasicPositionZ

            Dim mIsAligned As Boolean
            If TryGetBasicPos(mStageNo, selectedNodePath, RecipeEdit.Node(mStageNo)(selectedNodePath).TeachIndexX, RecipeEdit.Node(mStageNo)(selectedNodePath).TeachIndexY, mIsAligned, basicX, basicY, basicZ) Then
                If mIsAligned Then
                    Return "Real Basic(" & basicX.ToString("#.000") & "," & basicY.ToString("#.000") & "," & basicZ.ToString("#.000") & ")"
                Else
                    Return "Basic(" & basicX.ToString("#.000") & "," & basicY.ToString("#.000") & "," & basicZ.ToString("#.000") & ")"
                End If
            End If

        End With
        Return "Basic(NA,NA,NA)"
    End Function
    ''' <summary>取得傳回索引</summary>
    ''' <param name="selectedNodePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBasicIndex(ByVal selectedNodePath As String) As String
        If selectedNodePath.Length < 8 Then '資料長度不足
            Return "(NA,NA)"
        End If
        Dim mStageNo As Integer = Val(selectedNodePath.Substring(2, 1))
        If mStageNo >= gSSystemParameter.StageCount Then 'Stage索引超過範圍
            Return "(NA,NA)"
        End If
        If mStageNo < 0 Then 'Stage索引超過範圍
            Return "(NA,NA)"
        End If
        If Not RecipeEdit.Node(mStageNo).ContainsKey(selectedNodePath) Then
            Return "(NA,NA)"
        End If
        Return "(" & RecipeEdit.Node(mStageNo)(selectedNodePath).TeachIndexX & "," & RecipeEdit.Node(mStageNo)(selectedNodePath).TeachIndexY & ")"
    End Function

#End Region


    Public Sub btnWaitDone_Click(sender As Object, e As EventArgs) Handles btnWaitDone.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnWaitDone]" & vbTab & "Click")
        If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If

        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text

        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex

        '[說明]:判斷是新增項目,還是須修改的項目   20160820
        Dim mStepNo As Integer
        If bAddNewList = True Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex + 1
            End If
        ElseIf bAddNewList = False Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
            End If
        End If

        '[說明]:防止第一次ADD
        If mStepNo > RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            mStepNo = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If


        '[說明]:判斷是否為新增項目   20160820
        If bAddNewList = True Then
            If WriteStepList(eStepFunctionType.Wait) <> True Then
                Exit Sub
            End If
            bAddNewList = False
        End If

        ''--- Soni + 20160606 防無Step時更新Step ---
        'If mStepNo >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    MsgBox("Select Step First!")
        '    Exit Sub
        'End If
        ''--- Soni + 20160606 防無Step時更新Step ---

        With RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            .StepType = eStepFunctionType.Wait
            'Jeffadd 20160615
            .Wait.DwellTimeInMs = Val(txtWaitDwellTime.Text)

            TmpFrmXX.ListRefresh(mStepNo)
            Dim logData As String = "TreeNode(" & nodename & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            Debug.Print(logData)
            gSyslog.Save("StepUpdate: " & logData)
            'Sue20170627
            '存檔成功 
            'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        End With
        mIsCanClosed = True
        'Me.Close()
    End Sub

    Private Sub btnCircle3DDone_Click(sender As Object, e As EventArgs) Handles btnCircle3DDone.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnCircle3DDone]" & vbTab & "Click")
        If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex
        Dim mStepNo As Integer
        If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
            mStepNo = 0
        Else
            mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
        End If

        If cbCircle3DTypeSelect.Text = "" Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("Please set parameter select item!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("請設定弧形速度參數!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("請設定弧形速度參數!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            mIsCanClosed = False
            Exit Sub
        End If


        '--- Soni + 20160606 防無Step時更新Step ---
        If mStepNo >= RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            '請先選取Pattern Step
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000014))
            MsgBox(gMsgHandler.GetMessage(Warn_3000014), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        '--- Soni + 20160606 防無Step時更新Step ---

        With RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            .StepType = eStepFunctionType.Circle3D
            .Circle3D.EndPosX = Val(txtCircle3DEndPosX.Text)
            .Circle3D.EndPosY = Val(txtCircle3DEndPosY.Text)
            .Circle3D.EndPosZ = Val(txtCircle3DEndPosZ.Text)
            .Circle3D.CenterPosX = Val(txtCircle3DCenterPosX.Text)
            .Circle3D.CenterPosY = Val(txtCircle3DCenterPosY.Text)
            .Circle3D.CenterPosZ = Val(txtCircle3DCenterPosZ.Text)

            .Circle3D.ArcParameterName = cbCircle3DTypeSelect.Text 'Eason 20170216 Ticket:100080 , Add Arc Type Parameter

            TmpFrmXX.ListRefresh(mStepNo)
            Dim logData As String = "TreeNode(" & nodename & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            Debug.Print(logData)
            gSyslog.Save("StepUpdate: " & logData)
            'Sue20170627
            '存檔成功 
            'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End With
        'Me.Close()
    End Sub

    Private Sub btnArc3DDone_Click(sender As Object, e As EventArgs) Handles btnArc3DDone.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnArc3DDone]" & vbTab & "Click")
        If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If

        'If TmpFrmXX.dgvStep.SelectedCells.Count <= 0 Then '未選
        '    gSyslog.Save("Please, Select Step First!", , eMessageLevel.Warning)
        '    Exit Sub
        'End If
        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex
        Dim mStepNo As Integer
        If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
            mStepNo = 0
        Else
            mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
        End If
        If cbArc3DTypeSelect.Text = "" Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("Please set parameter select item!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("請設定弧形速度參數!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("請設定弧形速度參數!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            mIsCanClosed = False
            Exit Sub
        End If
        '--- Soni + 20160606 防無Step時更新Step ---
        If mStepNo >= RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            '請先選取Pattern Step
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000014))
            MsgBox(gMsgHandler.GetMessage(Warn_3000014), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '--- Soni + 20160606 防無Step時更新Step ---

        With RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            .StepType = eStepFunctionType.Arc3D
            .Arc3D.StartPosX = Val(txtArc3DStartPosX.Text)
            .Arc3D.StartPosY = Val(txtArc3DStartPosY.Text)
            .Arc3D.StartPosZ = Val(txtArc3DStartPosZ.Text)
            .Arc3D.EndPosX = Val(txtArc3DEndPosX.Text)
            .Arc3D.EndPosY = Val(txtArc3DEndPosY.Text)
            .Arc3D.EndPosZ = Val(txtArc3DEndPosZ.Text)
            .Arc3D.CenterPosX = Val(txtArc3DCenterPosX.Text)
            .Arc3D.CenterPosY = Val(txtArc3DCenterPosY.Text)
            .Arc3D.CenterPosZ = Val(txtArc3DCenterPosZ.Text)
            .Arc3D.Angle = Val(txtArc3DAngle.Text)
            .Arc3D.ArcParameterName = cbArc3DTypeSelect.Text 'Eason 20170216 Ticket:100080 , Add Arc Type Parameter

            TmpFrmXX.ListRefresh(mStepNo)
            Dim logData As String = "TreeNode(" & nodename & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            Debug.Print(logData)
            gSyslog.Save("StepUpdate: " & logData)
            'Sue20170627
            '存檔成功 
            'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        End With
        'Me.Close()
    End Sub

    Public Sub btnExtendOnDone_Click(sender As Object, e As EventArgs) Handles btnExtendOnDone.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnExtendOnDone]" & vbTab & "Click")
        If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            'gSyslog.Save("Please, Select pattern First!", , eMessageLevel.Warning)
            Exit Sub
        End If
        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex

        '[說明]:判斷是新增項目,還是須修改的項目   20160820
        Dim mStepNo As Integer
        If bAddNewList = True Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex + 1
            End If

        ElseIf bAddNewList = False Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
            End If
        End If

        '[說明]:防止第一次ADD
        If mStepNo > RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            mStepNo = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If


        '[說明]:判斷是否為新增項目   20160820
        If bAddNewList = True Then
            If WriteStepList(eStepFunctionType.ExtendOn) <> True Then
                Exit Sub
            End If
            bAddNewList = False
        End If

        ''--- Soni + 20160606 防無Step時更新Step ---
        'If mStepNo >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    MsgBox("Select Step First!")
        '    Exit Sub
        'End If
        ''--- Soni + 20160606 防無Step時更新Step ---

        With RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            .StepType = eStepFunctionType.ExtendOn

            TmpFrmXX.ListRefresh(mStepNo)
            Dim logData As String = "TreeNode(" & nodename & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            Debug.Print(logData)
            gSyslog.Save("StepUpdate: " & logData)
            'Sue20170627
            '存檔成功 
            'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        End With
        mIsCanClosed = True
        'Me.Close()
    End Sub

    Public Sub btnExtendOffDone_Click(sender As Object, e As EventArgs) Handles btnExtendOffDone.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnExtendOnDone]" & vbTab & "Click")
        If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex

        '[說明]:判斷是新增項目,還是須修改的項目   20160820
        Dim mStepNo As Integer
        If bAddNewList = True Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex + 1
            End If
        ElseIf bAddNewList = False Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
            End If
        End If

        '[說明]:防止第一次ADD
        If mStepNo > RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            mStepNo = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If


        '[說明]:判斷是否為新增項目   20160820
        If bAddNewList = True Then
            If WriteStepList(eStepFunctionType.ExtendOff) <> True Then
                Exit Sub
            End If
            bAddNewList = False
        End If

        ''--- Soni + 20160606 防無Step時更新Step ---
        'If mStepNo >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    MsgBox("Select Step First!")
        '    Exit Sub
        'End If
        ''--- Soni + 20160606 防無Step時更新Step ---

        With RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            .StepType = eStepFunctionType.ExtendOff
            TmpFrmXX.ListRefresh(mStepNo)
            Dim logData As String = "TreeNode(" & nodename & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            Debug.Print(logData)
            gSyslog.Save("StepUpdate: " & logData)
            'Sue20170627
            '存檔成功 
            'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        End With
        mIsCanClosed = True
        'Me.Close()
    End Sub

    ''' <summary>
    ''' 選閥確定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub btnSelectValveDone_Click(sender As Object, e As EventArgs) Handles btnSelectValveDone.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnSelectValveDone]" & vbTab & "Click")
        If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        '20170616_Start
        If TmpFrmXX.SelectedNodePath.Length < 8 Then '未選
            '請先選取Recipe Pattern.
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000013))
            Exit Sub
        End If
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex

        '[說明]:判斷是新增項目,還是須修改的項目   20160820
        Dim mStepNo As Integer
        If bAddNewList = True Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex + 1
            End If
        ElseIf bAddNewList = False Then
            If TmpFrmXX.dgvStep.SelectedCells.Count = 0 Then
                mStepNo = 0
            Else
                mStepNo = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
            End If
        End If

        '[說明]:防止第一次ADD
        If mStepNo > RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count Then
            mStepNo = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep.Count '過大直接卡範圍
        End If

        '[說明]:判斷是否為新增項目   20160820
        If bAddNewList = True Then
            If WriteStepList(eStepFunctionType.SelectValve) <> True Then
                Exit Sub
            End If
            bAddNewList = False
        End If

        ''--- Soni + 20160606 防無Step時更新Step ---
        'If mStepNo >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    MsgBox("Select Step First!")
        '    Exit Sub
        'End If
        ''--- Soni + 20160606 防無Step時更新Step ---

        With RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
            .StepType = eStepFunctionType.SelectValve
            Select Case cmbValve.SelectedIndex
                Case 0
                    .SelectValve.ValveNo = eValveWorkMode.Valve1
                    .SelectValve.PosB = CDec(Val(cmbPosB.SelectedItem))
                Case 1
                    .SelectValve.ValveNo = eValveWorkMode.Valve2
                    .SelectValve.PosB = CDec(Val(cmbPosB.SelectedItem))
            End Select

            TmpFrmXX.ListRefresh(mStepNo)
            Dim logData As String = "TreeNode(" & nodename & ") Round(" & mRoundNo & ") Step(" & mStepNo & ")" & .enmGraphicTypeToString
            Debug.Print(logData)
            gSyslog.Save("StepUpdate: " & logData)
            'Sue20170627
            '存檔成功 
            'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End With

        mIsCanClosed = True
        'Me.Close()
    End Sub


    Public Sub ShowNowRowFunction()
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex
        'Dim Basic As String

        If TmpFrmXX.dgvStep.SelectedCells(0).RowIndex >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
            Exit Sub
        End If
        With RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count - 1)

            Select Case .StepType
                Case eStepFunctionType.Arc3D
                    tabStep.SelectedTab = tabArc3D
                    grpArc3D.Text = "Arc3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtArc3DStartPosX.Text = .Arc3D.StartPosX
                    txtArc3DStartPosY.Text = .Arc3D.StartPosY
                    txtArc3DStartPosZ.Text = .Arc3D.StartPosZ

                    txtArc3DEndPosX.Text = .Arc3D.EndPosX
                    txtArc3DEndPosY.Text = .Arc3D.EndPosY
                    txtArc3DEndPosZ.Text = .Arc3D.EndPosZ

                    txtArcCenterPosX.Text = .Arc3D.CenterPosX
                    txtArcCenterPosY.Text = .Arc3D.CenterPosY
                    txtArcCenterPosZ.Text = .Arc3D.CenterPosZ
                    txtArcAngle.Text = .Arc3D.Angle
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
                    Dim indexfind As Integer = cbArc3DTypeSelect.FindStringExact(.Arc3D.ArcParameterName)
                    If (indexfind >= 0) Then
                        cbArc3DTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]

                Case eStepFunctionType.Arc2D
                    tabStep.SelectedTab = tabArc
                    grpArc.Text = "Arc2D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtArcStartPosX.Text = .Arc2D.StartPosX
                    txtArcStartPosY.Text = .Arc2D.StartPosY
                    txtArcMidPosX.Text = .Arc2D.MiddlePosX
                    txtArcMidPosY.Text = .Arc2D.MiddlePosY
                    txtArcEndPosX.Text = .Arc2D.EndPosX
                    txtArcEndPosY.Text = .Arc2D.EndPosY
                    txtArcPitch.Text = .Arc2D.Pitch
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    Select Case RecipeEdit.RunType
                        Case eWeightControlType.Dots
                            SetNumericUpDownValue(nmuArcDot, .Arc2D.WeightControl.DotCounts)

                        Case eWeightControlType.Weight
                            SetNumericUpDownValue(nmuArcWeight, .Arc2D.WeightControl.Weight)

                    End Select
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
                    Dim indexfind As Integer = cbArcTypeSelect.FindStringExact(.Arc2D.ArcParameterName)
                    If (indexfind >= 0) Then
                        cbArcTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]
                Case eStepFunctionType.Circle2D
                    tabStep.SelectedTab = tabCircle
                    grpCircle.Text = "Circle " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtCircleCenterPosX.Text = .Circle2D.CenterPosX
                    txtCircleCenterPosY.Text = .Circle2D.CenterPosY
                    txtCircleMid2PosX.Text = .Circle2D.Middle2PosX
                    txtCircleMid2PosY.Text = .Circle2D.Middle2PosY
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    Select Case RecipeEdit.RunType
                        Case eWeightControlType.Dots
                            nmuCircleDot.Value = .Circle2D.WeightControl.DotCounts

                        Case eWeightControlType.Weight
                            nmuCircleWeight.Value = .Circle2D.WeightControl.Weight

                    End Select
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
                    Dim indexfind As Integer = cbCircleTypeSelect.FindStringExact(.Circle2D.ArcParameterName)
                    If (indexfind >= 0) Then
                        cbCircleTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]
                Case eStepFunctionType.Circle3D
                    tabStep.SelectedTab = tabCircle3D
                    grpCircle3D.Text = "Circle3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtCircle3DEndPosX.Text = .Circle3D.EndPosX
                    txtCircle3DEndPosY.Text = .Circle3D.EndPosY
                    txtCircle3DEndPosZ.Text = .Circle3D.EndPosZ
                    'txtCircle3DEndPosB.Text = 0
                    'txtCircle3DEndPosC.Text = 0
                    txtCircle3DCenterPosX.Text = .Circle3D.CenterPosX
                    txtCircle3DCenterPosY.Text = .Circle3D.CenterPosY
                    txtCircle3DCenterPosZ.Text = .Circle3D.CenterPosZ
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
                    Dim indexfind As Integer = cbCircle3DTypeSelect.FindStringExact(.Circle3D.ArcParameterName)
                    If (indexfind >= 0) Then
                        cbCircle3DTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]
                Case eStepFunctionType.ContiEnd
                    tabStep.SelectedTab = TabContiEnd
                Case eStepFunctionType.ContiStart
                    tabStep.SelectedTab = TabContiStart
                Case eStepFunctionType.Dots3D
                    tabStep.SelectedTab = tabDots3D
                    grpDot3D.Text = "Dot3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtDots3DPosX.Text = .Dots3D.PosX
                    txtDots3DPosY.Text = .Dots3D.PosY
                    txtDots3DPosZ.Text = .Dots3D.PosZ
                    ''[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    ''[Note]:Dots強制切換成Velocity
                    '.Dots3D.WeightControl.Type = eWeightControlType.Velocity
                    'Select Case .Dots3D.WeightControl.Type
                    '    Case eWeightControlType.Velocity
                    '        txtDots3DVelocity.Text = .Dots3D.WeightControl.Velocity

                    'End Select

                    '[Note]:不再限制打點數量
                    Select Case RecipeEdit.RunType
                        Case eWeightControlType.Velocity
                            txtDots3DVelocity.Text = .Dots3D.WeightControl.Velocity

                        Case eWeightControlType.Dots
                            txtDots3DDot.Text = .Dots3D.WeightControl.DotCounts

                        Case eWeightControlType.Weight
                            txtDots3DDotWeight.Text = .Dots3D.WeightControl.Weight

                    End Select



                    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [S]
                    Dim indexfind As Integer = cbDotTypeSelect.FindStringExact(.Dots3D.DotParameterName)
                    If (indexfind >= 0) Then
                        cbDotTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [E]
                Case eStepFunctionType.ExtendOff
                    tabStep.SelectedTab = tabExtendOff
                Case eStepFunctionType.ExtendOn
                    tabStep.SelectedTab = tabExtendOn
                Case eStepFunctionType.Move3D
                    tabStep.SelectedTab = tabMove3D
                    grpMove3D.Text = "Move3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtMove3DEndPosX.Text = .Move3D.EndPosX
                    txtMove3DEndPosY.Text = .Move3D.EndPosY
                    txtMove3DEndPosZ.Text = .Move3D.EndPosZ
                    'txtMove3DEndPosB.Text = 0
                    'txtMove3DEndPosC.Text = 0


                Case eStepFunctionType.Line3D
                    tabStep.SelectedTab = tabLine3D
                    grpLine3D.Text = "Line3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtLine3DStartPosX.Text = .Line3D.StartPosX
                    txtLine3DStartPosY.Text = .Line3D.StartPosY
                    txtLine3DStartPosZ.Text = .Line3D.StartPosZ
                    'txtLine3DStartPosB.Text = 0
                    'txtLine3DStartPosC.Text = 0

                    txtLine3DEndPosX.Text = .Line3D.EndPosX
                    txtLine3DEndPosY.Text = .Line3D.EndPosY
                    txtLine3DEndPosZ.Text = .Line3D.EndPosZ
                    'txtLine3DEndPosB.Text = 0
                    'txtLine3DEndPosC.Text = 0
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    Select Case RecipeEdit.RunType
                        Case eWeightControlType.Dots
                            nmuLine3DDot.Value = .Line3D.WeightControl.DotCounts
                        Case eWeightControlType.Weight
                            nmuLine3DWeight.Value = .Line3D.WeightControl.Weight
                    End Select
                    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [S]
                    Dim indexfind As Integer = cbLineTypeSelect.FindStringExact(.Line3D.LineParameterName)
                    If (indexfind >= 0) Then
                        cbLineTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [E]
                Case eStepFunctionType.Rectangle
                Case eStepFunctionType.SelectValve
                    tabStep.SelectedTab = tabSelectValve
                Case eStepFunctionType.Wait
                    tabStep.SelectedTab = tabWait

            End Select
        End With
    End Sub
    Public Sub ShowSelectRowFunction()
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex
        'Dim Basic As String

        If TmpFrmXX.dgvStep.SelectedCells(0).RowIndex >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
            Exit Sub
        End If
        With RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(TmpFrmXX.dgvStep.SelectedCells(0).RowIndex)

            Select Case .StepType
                Case eStepFunctionType.Arc3D
                    tabStep.SelectedTab = tabArc3D
                    grpArc3D.Text = "Arc3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtArc3DStartPosX.Text = .Arc3D.StartPosX
                    txtArc3DStartPosY.Text = .Arc3D.StartPosY
                    txtArc3DStartPosZ.Text = .Arc3D.StartPosZ

                    txtArc3DEndPosX.Text = .Arc3D.EndPosX
                    txtArc3DEndPosY.Text = .Arc3D.EndPosY
                    txtArc3DEndPosZ.Text = .Arc3D.EndPosZ

                    txtArcCenterPosX.Text = .Arc3D.CenterPosX
                    txtArcCenterPosY.Text = .Arc3D.CenterPosY
                    txtArcCenterPosZ.Text = .Arc3D.CenterPosZ
                    txtArcAngle.Text = .Arc3D.Angle
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
                    Dim indexfind As Integer = cbArc3DTypeSelect.FindStringExact(.Arc3D.ArcParameterName)
                    If (indexfind >= 0) Then
                        cbArc3DTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]
                Case eStepFunctionType.Arc2D
                    tabStep.SelectedTab = tabArc

                    grpArc.Text = "Arc2D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtArcStartPosX.Text = .Arc2D.StartPosX
                    txtArcStartPosY.Text = .Arc2D.StartPosY
                    txtArcMidPosX.Text = .Arc2D.MiddlePosX
                    txtArcMidPosY.Text = .Arc2D.MiddlePosY
                    txtArcEndPosX.Text = .Arc2D.EndPosX
                    txtArcEndPosY.Text = .Arc2D.EndPosY

                    txtArcPitch.Text = .Arc2D.Pitch
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    Select Case RecipeEdit.RunType
                        Case eWeightControlType.Dots
                            SetNumericUpDownValue(nmuArcDot, .Arc2D.WeightControl.DotCounts)

                        Case eWeightControlType.Weight
                            SetNumericUpDownValue(nmuArcWeight, .Arc2D.WeightControl.Weight)

                    End Select
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
                    Dim indexfind As Integer = cbArcTypeSelect.FindStringExact(.Arc2D.ArcParameterName)
                    If (indexfind >= 0) Then
                        cbArcTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]
                Case eStepFunctionType.Circle2D
                    tabStep.SelectedTab = tabCircle
                    grpCircle.Text = "Circle " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtCircleCenterPosX.Text = .Circle2D.CenterPosX
                    txtCircleCenterPosY.Text = .Circle2D.CenterPosY
                    txtCircleMid2PosX.Text = .Circle2D.Middle2PosX
                    txtCircleMid2PosY.Text = .Circle2D.Middle2PosY
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    Select Case RecipeEdit.RunType
                        Case eWeightControlType.Dots
                            SetNumericUpDownValue(nmuCircleDot, .Circle2D.WeightControl.DotCounts)

                        Case eWeightControlType.Weight
                            SetNumericUpDownValue(nmuCircleWeight, .Circle2D.WeightControl.Weight)

                    End Select
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
                    Dim indexfind As Integer = cbCircleTypeSelect.FindStringExact(.Circle2D.ArcParameterName)
                    If (indexfind >= 0) Then
                        cbCircleTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]
                Case eStepFunctionType.Circle3D
                    tabStep.SelectedTab = tabCircle3D
                    grpCircle3D.Text = "Circle3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtCircle3DEndPosX.Text = .Circle3D.EndPosX
                    txtCircle3DEndPosY.Text = .Circle3D.EndPosY
                    txtCircle3DEndPosZ.Text = .Circle3D.EndPosZ
                    'txtCircle3DEndPosB.Text = 0
                    'txtCircle3DEndPosC.Text = 0

                    txtCircle3DCenterPosX.Text = .Circle3D.CenterPosX
                    txtCircle3DCenterPosY.Text = .Circle3D.CenterPosY
                    txtCircle3DCenterPosZ.Text = .Circle3D.CenterPosZ
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
                    Dim indexfind As Integer = cbCircle3DTypeSelect.FindStringExact(.Circle3D.ArcParameterName)
                    If (indexfind >= 0) Then
                        cbCircle3DTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]

                Case eStepFunctionType.ContiEnd
                    tabStep.SelectedTab = TabContiEnd
                Case eStepFunctionType.ContiStart
                    tabStep.SelectedTab = TabContiStart
                Case eStepFunctionType.Dots3D
                    tabStep.SelectedTab = tabDots3D
                    grpDot3D.Text = "Dot3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtDots3DPosX.Text = .Dots3D.PosX
                    txtDots3DPosY.Text = .Dots3D.PosY
                    txtDots3DPosZ.Text = .Dots3D.PosZ
                    ''[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    ''[Note]:Dots強制切換成Velocity模式
                    '.Dots3D.WeightControl.Type = eWeightControlType.Velocity
                    'Select Case .Dots3D.WeightControl.Type
                    '    Case eWeightControlType.Velocity
                    '        txtDots3DVelocity.Text = .Dots3D.WeightControl.Velocity

                    'End Select
                    '[Note]:不再限制打點數量
                    Select Case RecipeEdit.RunType
                        Case eWeightControlType.Velocity
                            .Dots3D.WeightControl.Velocity = Val(txtDots3DVelocity.Text)

                        Case eWeightControlType.Dots
                            .Dots3D.WeightControl.DotCounts = Val(txtDots3DDot.Text)

                        Case eWeightControlType.Weight
                            .Dots3D.WeightControl.Weight = Val(txtDots3DDotWeight.Text)

                    End Select


                    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [S]
                    Dim indexfind As Integer = cbDotTypeSelect.FindStringExact(.Dots3D.DotParameterName)
                    If (indexfind >= 0) Then
                        cbDotTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [E]
                Case eStepFunctionType.ExtendOff
                    tabStep.SelectedTab = tabExtendOff
                Case eStepFunctionType.ExtendOn
                    tabStep.SelectedTab = tabExtendOn
                Case eStepFunctionType.Move3D
                    tabStep.SelectedTab = tabMove3D
                    grpMove3D.Text = "Move3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtMove3DEndPosX.Text = .Move3D.EndPosX
                    txtMove3DEndPosY.Text = .Move3D.EndPosY
                    txtMove3DEndPosZ.Text = .Move3D.EndPosZ
                    'txtMove3DEndPosB.Text = 0
                    'txtMove3DEndPosC.Text = 0


                Case eStepFunctionType.Line3D
                    tabStep.SelectedTab = tabLine3D
                    grpLine3D.Text = "Line3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtLine3DStartPosX.Text = .Line3D.StartPosX
                    txtLine3DStartPosY.Text = .Line3D.StartPosY
                    txtLine3DStartPosZ.Text = .Line3D.StartPosZ
                    'txtLine3DStartPosB.Text = 0
                    'txtLine3DStartPosC.Text = 0

                    txtLine3DEndPosX.Text = .Line3D.EndPosX
                    txtLine3DEndPosY.Text = .Line3D.EndPosY
                    txtLine3DEndPosZ.Text = .Line3D.EndPosZ
                    'txtLine3DEndPosB.Text = 0
                    'txtLine3DEndPosC.Text = 0
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    Select Case RecipeEdit.RunType
                        Case eWeightControlType.Dots
                            nmuLine3DDot.Value = .Line3D.WeightControl.DotCounts

                        Case eWeightControlType.Weight
                            nmuLine3DWeight.Value = .Line3D.WeightControl.Weight

                    End Select
                    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [S]
                    Dim indexfind As Integer = cbLineTypeSelect.FindStringExact(.Line3D.LineParameterName)
                    If (indexfind >= 0) Then
                        cbLineTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [E]
                Case eStepFunctionType.Rectangle
                Case eStepFunctionType.SelectValve
                    tabStep.SelectedTab = tabSelectValve
                Case eStepFunctionType.Wait
                    tabStep.SelectedTab = tabWait

            End Select
        End With

    End Sub
    Public Sub ShowSelectRowFunction(ByVal iPatternID As Object, ByVal iRoundNo As Object, ByVal iRowIndex As Integer)
        '[說明]:顯示資料
        Dim mPatternID As String = iPatternID
        Dim mRoundNo As Integer = iRoundNo

        'If dgvStep.SelectedCells(0).RowIndex >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    Exit Sub
        'End If
        If Not RecipeEdit.Pattern.ContainsKey(mPatternID) Then
            Exit Sub
        End If
        If mRoundNo >= RecipeEdit.Pattern(mPatternID).Round.Count Then
            Exit Sub
        End If
        If iRowIndex >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
            Exit Sub
        End If
        With RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(iRowIndex)
            'With RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count - 1)

            Select Case .StepType
                Case eStepFunctionType.Arc3D
                    'tabStep.SelectedTab = tabArc3D
                    'grpArc3D.Text = "Arc3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtArc3DStartPosX.Text = .Arc3D.StartPosX
                    txtArc3DStartPosY.Text = .Arc3D.StartPosY
                    txtArc3DStartPosZ.Text = .Arc3D.StartPosZ

                    txtArc3DEndPosX.Text = .Arc3D.EndPosX
                    txtArc3DEndPosY.Text = .Arc3D.EndPosY
                    txtArc3DEndPosZ.Text = .Arc3D.EndPosZ

                    txtArcCenterPosX.Text = .Arc3D.CenterPosX
                    txtArcCenterPosY.Text = .Arc3D.CenterPosY
                    txtArcCenterPosZ.Text = .Arc3D.CenterPosZ
                    txtArcAngle.Text = .Arc3D.Angle
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
                    Dim indexfind As Integer = cbArc3DTypeSelect.FindStringExact(.Arc3D.ArcParameterName)
                    If (indexfind >= 0) Then
                        cbArc3DTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]
                Case eStepFunctionType.Arc2D

                    'tabStep.SelectedTab = tabAr
                    'grpArc.Text = "Arc2D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)

                    txtArcStartPosX.Text = .Arc2D.StartPosX
                    txtArcStartPosY.Text = .Arc2D.StartPosY
                    txtArcMidPosX.Text = .Arc2D.MiddlePosX
                    txtArcMidPosY.Text = .Arc2D.MiddlePosY
                    txtArcEndPosX.Text = .Arc2D.EndPosX
                    txtArcEndPosY.Text = .Arc2D.EndPosY

                    txtArcPitch.Text = .Arc2D.Pitch
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    Select Case RecipeEdit.RunType
                        Case eWeightControlType.Dots
                            SetNumericUpDownValue(nmuArcDot, .Arc2D.WeightControl.DotCounts)

                        Case eWeightControlType.Weight
                            SetNumericUpDownValue(nmuArcWeight, .Arc2D.WeightControl.Weight)

                    End Select
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
                    Dim indexfind As Integer = cbArcTypeSelect.FindStringExact(.Arc2D.ArcParameterName)
                    If (indexfind >= 0) Then
                        cbArcTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]
                Case eStepFunctionType.Circle2D
                    'tabStep.SelectedTab = tabCircle
                    'grpCircle.Text = "Circle " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtCircleCenterPosX.Text = .Circle2D.CenterPosX
                    txtCircleCenterPosY.Text = .Circle2D.CenterPosY
                    txtCircleMid2PosX.Text = .Circle2D.Middle2PosX
                    txtCircleMid2PosY.Text = .Circle2D.Middle2PosY
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    Select Case RecipeEdit.RunType
                        Case eWeightControlType.Dots
                            SetNumericUpDownValue(nmuCircleDot, .Circle2D.WeightControl.DotCounts)

                        Case eWeightControlType.Weight
                            SetNumericUpDownValue(nmuCircleWeight, .Circle2D.WeightControl.Weight)

                    End Select
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
                    Dim indexfind As Integer = cbCircleTypeSelect.FindStringExact(.Circle2D.ArcParameterName)
                    If (indexfind >= 0) Then
                        cbCircleTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]
                Case eStepFunctionType.Circle3D
                    'tabStep.SelectedTab = tabCircle3D
                    'grpCircle3D.Text = "Circle3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)

                    txtCircle3DEndPosX.Text = .Circle3D.EndPosX
                    txtCircle3DEndPosY.Text = .Circle3D.EndPosY
                    txtCircle3DEndPosZ.Text = .Circle3D.EndPosZ
                    'txtCircle3DEndPosB.Text = 0
                    'txtCircle3DEndPosC.Text = 0

                    txtCircle3DCenterPosX.Text = .Circle3D.CenterPosX
                    txtCircle3DCenterPosY.Text = .Circle3D.CenterPosY
                    txtCircle3DCenterPosZ.Text = .Circle3D.CenterPosZ

                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
                    Dim indexfind As Integer = cbCircle3DTypeSelect.FindStringExact(.Circle3D.ArcParameterName)
                    If (indexfind >= 0) Then
                        cbCircle3DTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]

                Case eStepFunctionType.ContiEnd
                    'tabStep.SelectedTab = TabContiEnd
                Case eStepFunctionType.ContiStart
                    'tabStep.SelectedTab = TabContiStart
                Case eStepFunctionType.Dots3D
                    'tabStep.SelectedTab = tabDots3D
                    'grpDot3D.Text = "Dot3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtDots3DPosX.Text = .Dots3D.PosX
                    txtDots3DPosY.Text = .Dots3D.PosY
                    txtDots3DPosZ.Text = .Dots3D.PosZ
                    ''[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    ''[Note]:Dots強制切換成Velocity模式
                    '.Dots3D.WeightControl.Type = eWeightControlType.Velocity
                    'Select Case .Dots3D.WeightControl.Type
                    '    Case eWeightControlType.Velocity
                    '        txtDots3DVelocity.Text = .Dots3D.WeightControl.Velocity

                    'End Select

                    '[Note]:不再限制打點數量
                    Select Case RecipeEdit.RunType
                        Case eWeightControlType.Velocity
                            txtDots3DVelocity.Text = .Dots3D.WeightControl.Velocity

                        Case eWeightControlType.Dots
                            txtDots3DDot.Text = .Dots3D.WeightControl.DotCounts

                        Case eWeightControlType.Weight
                            txtDots3DDotWeight.Text = .Dots3D.WeightControl.Weight

                    End Select

                    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [S]
                    Dim indexfind As Integer = cbDotTypeSelect.FindStringExact(.Dots3D.DotParameterName)
                    If (indexfind >= 0) Then
                        cbDotTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [E]
                Case eStepFunctionType.ExtendOff
                    'tabStep.SelectedTab = tabExtendOff
                Case eStepFunctionType.ExtendOn
                    'tabStep.SelectedTab = tabExtendOn
                Case eStepFunctionType.Move3D
                    ' tabStep.SelectedTab = tabMove3D
                    'grpMove3D.Text = "Move3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtMove3DEndPosX.Text = .Move3D.EndPosX
                    txtMove3DEndPosY.Text = .Move3D.EndPosY
                    txtMove3DEndPosZ.Text = .Move3D.EndPosZ
                    'txtMove3DEndPosB.Text = 0
                    'txtMove3DEndPosC.Text = 0
                Case eStepFunctionType.Line3D
                    'tabStep.SelectedTab = tabLine3D
                    ' grpLine3D.Text = "Line3D " & GetNodeBasicPos(TmpFrmXX.SelectedNodePath)
                    txtLine3DStartPosX.Text = .Line3D.StartPosX
                    txtLine3DStartPosY.Text = .Line3D.StartPosY
                    txtLine3DStartPosZ.Text = .Line3D.StartPosZ
                    'txtLine3DStartPosB.Text = 0
                    'txtLine3DStartPosC.Text = 0

                    txtLine3DEndPosX.Text = .Line3D.EndPosX
                    txtLine3DEndPosY.Text = .Line3D.EndPosY
                    txtLine3DEndPosZ.Text = .Line3D.EndPosZ
                    'txtLine3DEndPosB.Text = 0
                    'txtLine3DEndPosC.Text = 0
                    '[Note]:點數、重量的新的資料格式    'Moary+ 2016.10.03
                    Select Case RecipeEdit.RunType
                        Case eWeightControlType.Dots
                            SetNumericUpDownValue(nmuLine3DDot, .Line3D.WeightControl.DotCounts)

                        Case eWeightControlType.Weight
                            SetNumericUpDownValue(nmuLine3DWeight, .Line3D.WeightControl.Weight)

                    End Select
                    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [S]
                    Dim indexfind As Integer = cbLineTypeSelect.FindStringExact(.Line3D.LineParameterName)
                    If (indexfind >= 0) Then
                        cbLineTypeSelect.SelectedIndex = indexfind
                    End If
                    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [E]
                Case eStepFunctionType.Rectangle
                Case eStepFunctionType.SelectValve
                    'tabStep.SelectedTab = tabSelectValve
                Case eStepFunctionType.Wait
                    With RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(iRowIndex)
                        txtWaitDwellTime.Text = .Wait.DwellTimeInMs
                    End With
            End Select
        End With
    End Sub

    Private Sub btnSelectValveCancel_Click(sender As Object, e As EventArgs) Handles btnSelectValveCancel.Click, btnContiStartCancel.Click, btnContiEndCancel.Click, btnMove3DCancel.Click, btnDot3DCancel.Click, btnLine3DCancel.Click, btnArcCancel.Click, btnArc3DCancel.Click, btnCircle3DCancel.Click, btnCircleCancel.Click, btnWaitCancel.Click, btnExtendOffCancel.Click, btnExtendOnCancel.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnSelectValveCancel]" & vbTab & "Click")
        '20160820
        'If TmpFrmXX.lstPatternID.SelectedIndex < 0 Then '未選
        '    gSyslog.Save("Please, Select pattern First!", , eMessageLevel.Warning)
        '    Exit Sub
        'End If

        'If TmpFrmXX.dgvStep.SelectedCells.Count <= 0 Then '未選
        '    gSyslog.Save("Please, Select Step First!", , eMessageLevel.Warning)
        '    Exit Sub
        'End If
        'Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        'Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex
        'Dim mStepNo As Integer = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
        '--- Soni + 20160606 防無Step時更新Step ---
        'If mStepNo >= RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count Then
        '    MsgBox("Select Step First!")
        '    Exit Sub
        'End If
        '--- Soni + 20160606 防無Step時更新Step ---

        'RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.RemoveAt(mStepNo)
        'RecipeEdit.Pattern(mPatternID).Round(mRoundNo).StepCount = RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count
        'TmpFrmXX.ListRefresh(mStepNo)
        mIsCanClosed = True
        Me.Close()
    End Sub


    Private Sub txtMove3DEndPosX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMove3DEndPosX.KeyPress, txtMove3DEndPosY.KeyPress, txtMove3DEndPosZ.KeyPress, _
        txtDots3DPosX.KeyPress, txtDots3DPosY.KeyPress, txtDots3DPosZ.KeyPress, _
        txtLine3DStartPosX.KeyPress, txtLine3DStartPosY.KeyPress, txtLine3DStartPosZ.KeyPress, _
        txtLine3DEndPosX.KeyPress, txtLine3DEndPosY.KeyPress, txtLine3DEndPosZ.KeyPress, _
        txtArcStartPosX.KeyPress, txtArcStartPosY.KeyPress, txtArcStartPosZ.KeyPress, _
        txtArcMidPosX.KeyPress, txtArcMidPosY.KeyPress, txtArcMidPosZ.KeyPress, _
        txtArcEndPosX.KeyPress, txtArcEndPosY.KeyPress, txtArcEndPosZ.KeyPress, _
        txtArcAngle.KeyPress, _
        txtCircleStartPosX.KeyPress, txtCircleStartPosY.KeyPress, txtCircleStartPosZ.KeyPress, _
        txtCircleMidPosX.KeyPress, txtCircleMidPosY.KeyPress, txtCircleMidPosZ.KeyPress, _
        txtCircleMid2PosX.KeyPress, txtCircleMid2PosY.KeyPress, txtCircleMid2PosZ.KeyPress, _
        txtCircle3DEndPosX.KeyPress, txtCircle3DEndPosY.KeyPress, txtCircle3DEndPosZ.KeyPress, _
        txtCircle3DCenterPosX.KeyPress, txtCircle3DCenterPosY.KeyPress, txtCircle3DCenterPosZ.KeyPress, _
        txtArc3DStartPosX.KeyPress, txtArc3DStartPosY.KeyPress, txtArc3DStartPosZ.KeyPress, _
        txtArc3DCenterPosX.KeyPress, txtArc3DCenterPosY.KeyPress, txtArc3DCenterPosZ.KeyPress, _
        txtArc3DEndPosX.KeyPress, txtArc3DEndPosY.KeyPress, txtArc3DEndPosZ.KeyPress, _
        txtArc3DAngle.KeyPress

        Premtek.Base.CKeyPress.CheckDecimal(sender, e)
    End Sub

    Private Sub txtLine3DWeight_KeyPress(sender As Object, e As KeyPressEventArgs) Handles _
        txtWaitDwellTime.KeyPress
        Premtek.Base.CKeyPress.CheckUDecimal(sender, e)
    End Sub

    Private Sub txtLine3DDot_KeyPress(sender As Object, e As KeyPressEventArgs)
        Premtek.Base.CKeyPress.CheckUInteger(sender, e)
    End Sub

    '20161102
    Private Sub btnLine3DRefresh_Click(sender As Object, e As EventArgs) Handles btnLine3DRefresh.Click
        '--- 起終點共點保護 ---   
        If Val(txtLine3DStartPosX.Text) = Val(txtLine3DEndPosX.Text) And
           Val(txtLine3DStartPosY.Text) = Val(txtLine3DEndPosY.Text) Then
            '請確認開始與結束位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000017))
            MsgBox(gMsgHandler.GetMessage(Warn_3000017), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        '--- 起終點共點保護 ---

        '[說明]:計算Pitch長度 
        mDisDx = (txtLine3DEndPosX.Text - txtLine3DStartPosX.Text)
        mDisDy = (txtLine3DEndPosY.Text - txtLine3DStartPosY.Text)
        mSubDistance = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)))
        Dim mPitch As Decimal
        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                'Pitch = 總長 / 點數 * 1000(mm轉um)
                If Val(nmuLine3DDot.Value) < 2 Then
                    ErrMessage = "Dot Counts 不可小於2!!"
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If
                mPitch = mSubDistance / (Val(nmuLine3DDot.Value) - 1) * 1000
                txtLine3DPitch.Text = mPitch.ToString("##0.###") 'Soni 2017.07.21 數量減1

            Case eWeightControlType.Weight
                '[說明]:有做秤重才能計算Pitch
                If Val(nmuLine3DWeight.Value) < 0 Then
                    ErrMessage = "Weight預設值不可小於零!!"
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If
                If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) > 0 Then
                    '單點重量 = 總重 / 單顆Dot重量
                    Dot = (Val(nmuLine3DWeight.Value) / RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)) - 1 'Soni 2017.07.21 數量減1
                    If Dot > 0 Then
                        'Pitch = 總長 / 點數 * 1000(mm轉um)
                        txtLine3DPitch.Text = (mSubDistance / Dot * 1000).ToString("##0.###") 'Soni 2017.03.22
                    Else
                        txtLine3DPitch.Text = ""
                    End If
                End If

                '20171027 Jeff
            Case eWeightControlType.Velocity
                mDisDx = (txtLine3DEndPosX.Text - txtLine3DStartPosX.Text)
                mDisDy = (txtLine3DEndPosY.Text - txtLine3DStartPosY.Text)
                mSubDistance = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)))
                Dim mVmin As Decimal
                Dim mVmax As Decimal
                GetLineVelocityLimit(mSubDistance, mVmin, mVmax)
                nmuLine3DVelocity.Maximum = mVmax ' 1000 * mSubDistance / mCycleTimes(sys.StageNo) '速度上限
                nmuLine3DVelocity.Minimum = mVmin ' 10 / mCycleTimes(sys.StageNo) '速度下限
                [Step].Line3D.WeightControl.Velocity = nmuLine3DVelocity.Value
                [Step].Line3D.WeightControl.dotPitch = [Step].Line3D.WeightControl.Velocity * mCycleTimes(sys.StageNo) / 1000
                [Step].Line3D.WeightControl.DotCounts = CInt(mSubDistance / [Step].Line3D.WeightControl.dotPitch) + 1
                If [Step].Line3D.WeightControl.DotCounts > 1 Then
                    [Step].Line3D.WeightControl.dotPitch = mSubDistance / ([Step].Line3D.WeightControl.DotCounts - 1) * 1000
                    txtLine3DPitch.Text = [Step].Line3D.WeightControl.dotPitch.ToString("##0.###") 'dot數量減1算pitch
                    SetNumericUpDownValue(nmuLine3DDot, [Step].Line3D.WeightControl.DotCounts)
                    [Step].Line3D.WeightControl.Weight = [Step].Line3D.WeightControl.DotCounts * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                    SetNumericUpDownValue(nmuLine3DWeight, [Step].Line3D.WeightControl.Weight)
                End If
        End Select
    End Sub

    '20161102
    Private Sub btnArcRefresh_Click(sender As Object, e As EventArgs) Handles btnArcRefresh.Click
        '--- 起終點共點保護 ---  
        If Val(txtArcStartPosX.Text) = Val(txtArcMidPosX.Text) And Val(txtArcStartPosY.Text) = Val(txtArcMidPosY.Text) Then
            '請確認開始與中間位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000047))
            MsgBox(gMsgHandler.GetMessage(Warn_3000047), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Val(txtArcStartPosX.Text) = Val(txtArcEndPosX.Text) And Val(txtArcStartPosY.Text) = Val(txtArcEndPosY.Text) Then
            '請確認開始與結束位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000017))
            MsgBox(gMsgHandler.GetMessage(Warn_3000017), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Val(txtArcMidPosX.Text) = Val(txtArcEndPosX.Text) And Val(txtArcMidPosY.Text) = Val(txtArcEndPosY.Text) Then
            '請確認中間與結束位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000048))
            MsgBox(gMsgHandler.GetMessage(Warn_3000048), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        '--- 起終點共點保護 ---


        '---三點共線保護--- 公式:兩點斜率相同 為共線充要條件
        If ((txtArcEndPosY.Text - txtArcStartPosY.Text) * (txtArcMidPosX.Text - txtArcStartPosX.Text)) =
            ((txtArcMidPosY.Text - txtArcStartPosY.Text) * (txtArcEndPosX.Text - txtArcStartPosX.Text)) Then
            '請確認三點共線問題
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000049))
            MsgBox(gMsgHandler.GetMessage(Warn_3000049), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        '---三點共線保護---


        Dim Circle As Circle
        Dim x, y, z As CPoint
        'Jeffadd 20160615
        x = New CPoint(Val(txtArcStartPosX.Text), Val(txtArcStartPosY.Text))
        y = New CPoint(Val(txtArcMidPosX.Text), Val(txtArcMidPosY.Text))
        z = New CPoint(Val(txtArcEndPosX.Text), Val(txtArcEndPosY.Text))

        '[說明]:計算Arc圓心座標
        Circle = CMath.GetCircleby3Point(x, y, z)

        '[說明]:計算Pitch長度 20160920
        '[Note]:弧長=2*PI*R*Angle/360
        '           =PI*R*Angle/180
        mDisDx = (z.PointX - Circle.PointX)
        mDisDy = (z.PointY - Circle.PointY)
        mSubDistance = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)) * Math.PI * Circle.Angle / 180)
        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                'Pitch = 總長 / 點數 * 1000(mm轉um)
                If Val(nmuArcDot.Value) < 2 Then
                    ErrMessage = "Dot Counts 不可小於2!!"
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If
                txtArcPitch.Text = (mSubDistance / (Val(nmuArcDot.Value) - 1) * 1000).ToString("##0.###") 'Soni 2017.07.21 數量減1

            Case eWeightControlType.Weight
                '[說明]:有做秤重才能計算Pitch
                If Val(nmuArcWeight.Value) < 0 Then
                    ErrMessage = "Weigh預設值不可小於零!!"
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If
                If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) > 0 Then
                    '單點重量 = 總重 / 單顆Dot重量
                    Dot = (Val(nmuArcWeight.Value) / RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)) - 1  'Soni 2017.07.21 數量減1
                    'Pitch = 總長 / 點數 * 1000(mm轉um)
                    txtArcPitch.Text = (mSubDistance / Dot * 1000).ToString("##0.###") 'Soni 2017.03.22
                End If

        End Select

    End Sub

    '20161102
    Private Sub btnCircleRefresh_Click(sender As Object, e As EventArgs) Handles btnCircleRefresh.Click

        '--- 起終點共點保護 ---  
        If Val(txtCircleStartPosX.Text) = Val(txtCircleMidPosX.Text) And Val(txtCircleStartPosY.Text) = Val(txtCircleMidPosY.Text) Then
            '請確認開始與中間位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000047))
            MsgBox(gMsgHandler.GetMessage(Warn_3000047), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Val(txtCircleStartPosX.Text) = Val(txtCircleMid2PosX.Text) And Val(txtCircleStartPosY.Text) = Val(txtCircleMid2PosY.Text) Then
            '請確認開始與結束位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000017))
            MsgBox(gMsgHandler.GetMessage(Warn_3000017), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Val(txtCircleMidPosX.Text) = Val(txtCircleMid2PosX.Text) And Val(txtCircleMidPosY.Text) = Val(txtCircleMid2PosY.Text) Then
            '請確認中間與結束位置非重合
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000048))
            MsgBox(gMsgHandler.GetMessage(Warn_3000048), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        '--- 起終點共點保護 ---

        '---三點共線保護--- 公式:兩點斜率相同 為共線充要條件
        If ((txtCircleMid2PosY.Text - txtCircleStartPosY.Text) * (txtCircleMidPosX.Text - txtCircleStartPosX.Text)) =
            ((txtCircleMidPosY.Text - txtCircleStartPosY.Text) * (txtCircleMid2PosX.Text - txtCircleStartPosX.Text)) Then
            '請確認三點共線問題
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000049))
            MsgBox(gMsgHandler.GetMessage(Warn_3000049), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        '---三點共線保護---


        Dim Circle As Circle
        Dim x, y, z As CPoint
        '[說明]:計算圓心座標及畫圓方向
        'Jeffadd 20160615
        x = New CPoint(Val(txtCircleStartPosX.Text), Val(txtCircleStartPosY.Text))
        y = New CPoint(Val(txtCircleMidPosX.Text), Val(txtCircleMidPosY.Text))
        z = New CPoint(Val(txtCircleMid2PosX.Text), Val(txtCircleMid2PosY.Text))

        '[說明]:計算圓心座標及畫圓方向
        Circle = CMath.GetCircleby3Point(x, y, z)

        '[說明]:計算Pitch長度 20160920
        '[Note]:弧長= 2 * PI * R * Angle / 360
        '           = 2 * PI * R * 360 / 360
        '           = 2 * PI * Rsys.SelectValve
        mDisDx = (z.PointX - Circle.PointX)
        mDisDy = (z.PointY - Circle.PointY)

        mSubDistance = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)) * Math.PI * 2)
        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                'Pitch = 總長 / 點數 * 1000(mm轉um)
                If Val(nmuCircleDot.Value) < 2 Then
                    ErrMessage = "Dot Counts 不可小於2!!"
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If
                txtCirclePitch.Text = (mSubDistance / Val(nmuCircleDot.Value) * 1000).ToString("##0.###") 'Soni 2017.03.22

            Case eWeightControlType.Weight
                '[說明]:有做秤重才能計算Pitch
                If Val(nmuCircleWeight.Value) < 0 Then
                    ErrMessage = "Weigh預設值不可小於零!!"
                    MsgBox(ErrMessage, vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Error!!!")
                    Exit Sub
                End If
                If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) > 0 Then
                    '單點重量 = 總重 / 單顆Dot重量
                    Dot = Val(nmuCircleWeight.Value) / RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                    'Pitch = 總長 / 點數 * 1000(mm轉um)
                    txtCirclePitch.Text = (mSubDistance / Dot * 1000).ToString("##0.###") 'Soni 2017.03.22
                End If

        End Select
    End Sub

    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [S]
    Private Sub btDotTypeSelect_Click(sender As Object, e As EventArgs) Handles btDotTypeSelect.Click

        If (Not cbDotTypeSelect.Text = "") Then

            Dim sChoiseType As String = cbDotTypeSelect.Text
            '-----------------------------------------------
            Dim ucDotValueShow As New ucDotTypeParameter
            With ucDotValueShow
                .Type = ucDotTypeParameter.ShowType.Min
                .Dock = DockStyle.Fill
                .SelectItemName = sChoiseType
                .Enabled = False
            End With
            '-----------------------------------------------
            Dim frmShow As New Form
            With frmShow
                .Width = 526
                .Height = 530 + 30 '525 = 原始大小 + 邊寬30
                .MinimizeBox = False
                .MaximizeBox = False
                .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                .Text = "Dot Parameter Recipe Name :" & sChoiseType
                .Controls.Add(ucDotValueShow)
                .ShowDialog()
            End With
            '-----------------------------------------------
        End If

    End Sub
    Private Sub btLineTypeSelect_Click(sender As Object, e As EventArgs) Handles btLineTypeSelect.Click

        If (Not cbLineTypeSelect.Text = "") Then
            Dim sChoiseType As String = cbLineTypeSelect.Text

            '-----------------------------------------------
            Dim ucLineValueShow As New ucLineTypeParameter
            With ucLineValueShow
                .Type = ucLineTypeParameter.ShowType.Min
                .Dock = DockStyle.Fill
                .SelectItemName = sChoiseType
                .Enabled = False
            End With
            '-----------------------------------------------
            Dim frmShow As New Form
            With frmShow
                .Width = 526
                .Height = 620 + 30 '610 = 原始大小 + 邊寬30
                .MinimizeBox = False
                .MaximizeBox = False
                .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                .Text = "Line Parameter Recipe Name :" & sChoiseType
                .Controls.Add(ucLineValueShow)
                .ShowDialog()
            End With
            '-----------------------------------------------
        End If
    End Sub
    'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter [E]   

    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [S]
    Private Sub btArcTypeSelect_Click(sender As Object, e As EventArgs) Handles btArcTypeSelect.Click
        If (Not cbArcTypeSelect.Text = "") Then
            Dim sChoiseType As String = cbArcTypeSelect.Text

            '-----------------------------------------------
            Dim ucArcValueShow As New ucArcTypeParameter
            With ucArcValueShow
                .Type = ucArcTypeParameter.ShowType.Min
                .Dock = DockStyle.Fill
                .SelectItemName = sChoiseType
                .Enabled = False
            End With
            '-----------------------------------------------
            Dim frmShow As New Form
            With frmShow
                .Width = 526
                .Height = 620 + 30 '610 = 原始大小 + 邊寬30
                .MinimizeBox = False
                .MaximizeBox = False
                .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                .Text = "Arc Parameter Recipe Name :" & sChoiseType
                .Controls.Add(ucArcValueShow)
                .ShowDialog()
            End With
            '-----------------------------------------------
        End If
    End Sub

    Private Sub btCircleTypeSelect_Click(sender As Object, e As EventArgs) Handles btCircleTypeSelect.Click
        If (Not cbCircleTypeSelect.Text = "") Then
            Dim sChoiseType As String = cbCircleTypeSelect.Text

            '-----------------------------------------------
            Dim ucCircleValueShow As New ucArcTypeParameter
            With ucCircleValueShow
                .Type = ucArcTypeParameter.ShowType.Min
                .Dock = DockStyle.Fill
                .SelectItemName = sChoiseType
                .Enabled = False
            End With
            '-----------------------------------------------
            Dim frmShow As New Form
            With frmShow
                .Width = 526
                .Height = 620 + 30 '610 = 原始大小 + 邊寬30
                .MinimizeBox = False
                .MaximizeBox = False
                .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                .Text = "Circle Parameter Recipe Name :" & sChoiseType
                .Controls.Add(ucCircleValueShow)
                .ShowDialog()
            End With
            '-----------------------------------------------
        End If
    End Sub

    Private Sub btArc3DTypeSelect_Click(sender As Object, e As EventArgs) Handles btArc3DTypeSelect.Click
        If (Not cbArc3DTypeSelect.Text = "") Then
            Dim sChoiseType As String = cbArc3DTypeSelect.Text

            '-----------------------------------------------
            Dim ucArc3DValueShow As New ucArcTypeParameter
            With ucArc3DValueShow
                .Type = ucArcTypeParameter.ShowType.Min
                .Dock = DockStyle.Fill
                .SelectItemName = sChoiseType
                .Enabled = False
            End With
            '-----------------------------------------------
            Dim frmShow As New Form
            With frmShow
                .Width = 526
                .Height = 620 + 30 '610 = 原始大小 + 邊寬30
                .MinimizeBox = False
                .MaximizeBox = False
                .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                .Text = "Arc3D Parameter Recipe Name :" & sChoiseType
                .Controls.Add(ucArc3DValueShow)
                .ShowDialog()
            End With
            '-----------------------------------------------
        End If
    End Sub

    Private Sub btCircle3DTypeSelect_Click(sender As Object, e As EventArgs) Handles btCircle3DTypeSelect.Click
        If (Not cbCircle3DTypeSelect.Text = "") Then
            Dim sChoiseType As String = cbCircle3DTypeSelect.Text

            '-----------------------------------------------
            Dim ucCircle3DValueShow As New ucArcTypeParameter
            With ucCircle3DValueShow
                .Type = ucArcTypeParameter.ShowType.Min
                .Dock = DockStyle.Fill
                .SelectItemName = sChoiseType
                .Enabled = False
            End With
            '-----------------------------------------------
            Dim frmShow As New Form
            With frmShow
                .Width = 526
                .Height = 620 + 30 '610 = 原始大小 + 邊寬30
                .MinimizeBox = False
                .MaximizeBox = False
                .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                .Text = "Circle3D Parameter Recipe Name :" & sChoiseType
                .Controls.Add(ucCircle3DValueShow)
                .ShowDialog()
            End With
            '-----------------------------------------------
        End If
    End Sub
    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter [E]

    '=== Soni 2017.03.22 Mobary路徑串接需求 ===
    Function GetStepPos(ByVal nowNodeID As String, ByVal oldNodeID As String, ByRef oldStep As CPatternStep, ByRef posX As TextBox, ByRef posY As TextBox, ByRef posZ As TextBox) As Boolean
        Dim mPosX As Decimal
        Dim mPosY As Decimal
        Dim mPosZ As Decimal
        If oldStep Is Nothing Then
            Return True
        End If
        Select Case oldStep.StepType
            Case eStepFunctionType.Arc2D
                mPosX = oldStep.Arc2D.EndPosX + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionX - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                mPosY = oldStep.Arc2D.EndPosY + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionY - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                mPosZ = 0 'RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionZ - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                posX.Text = mPosX.ToString("##0.000")
                posY.Text = mPosY.ToString("##0.000")
                posZ.Text = mPosZ.ToString("##0.000")
            Case eStepFunctionType.Arc3D
                mPosX = oldStep.Arc3D.EndPosX + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionX - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                mPosY = oldStep.Arc3D.EndPosY + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionY - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                mPosZ = oldStep.Arc3D.EndPosZ + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionZ - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                posX.Text = mPosX.ToString("##0.000")
                posY.Text = mPosY.ToString("##0.000")
                posZ.Text = mPosZ.ToString("##0.000")
            Case eStepFunctionType.Circle2D
                mPosX = oldStep.Circle2D.EndPosX + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionX - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                mPosY = oldStep.Circle2D.EndPosY + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionY - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                mPosZ = 0 'RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionZ - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                posX.Text = mPosX.ToString("##0.000")
                posY.Text = mPosY.ToString("##0.000")
                posZ.Text = mPosZ.ToString("##0.000")
            Case eStepFunctionType.Circle3D
                mPosX = oldStep.Circle3D.EndPosX + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionX - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                mPosY = oldStep.Circle3D.EndPosY + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionY - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                mPosZ = oldStep.Circle3D.EndPosZ + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionZ - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                posX.Text = mPosX.ToString("##0.000")
                posY.Text = mPosY.ToString("##0.000")
                posZ.Text = mPosZ.ToString("##0.000")
            Case eStepFunctionType.Dots3D
                mPosX = oldStep.Dots3D.PosX + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionX - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                mPosY = oldStep.Dots3D.PosY + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionY - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                mPosZ = oldStep.Dots3D.PosZ + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionZ - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                posX.Text = mPosX.ToString("##0.000")
                posY.Text = mPosY.ToString("##0.000")
                posZ.Text = mPosZ.ToString("##0.000")
            Case eStepFunctionType.Line3D
                mPosX = oldStep.Line3D.EndPosX + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionX - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                mPosY = oldStep.Line3D.EndPosY + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionY - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                mPosZ = oldStep.Line3D.EndPosZ + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionZ - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                posX.Text = mPosX.ToString("##0.000")
                posY.Text = mPosY.ToString("##0.000")
                posZ.Text = mPosZ.ToString("##0.000")
            Case eStepFunctionType.Move3D
                mPosX = oldStep.Move3D.EndPosX + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionX - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionX
                mPosY = oldStep.Move3D.EndPosY + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionY - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionY
                mPosZ = oldStep.Move3D.EndPosZ + RecipeEdit.Node(sys.StageNo)(oldNodeID).ConveyorPos(mConveyorNo).BasicPositionZ - RecipeEdit.Node(sys.StageNo)(nowNodeID).ConveyorPos(mConveyorNo).BasicPositionZ
                posX.Text = mPosX.ToString("##0.000")
                posY.Text = mPosY.ToString("##0.000")
                posZ.Text = mPosZ.ToString("##0.000")
        End Select
        Return True
    End Function

    Function FindOldMotionStep(ByVal nodeID As String, ByVal patternID As String, ByVal roundNo As Integer, ByVal stepNo As Integer, ByRef oldNodeID As String) As CPatternStep
        '=== 當前步驟異常 ===
        If sys.StageNo >= RecipeEdit.Node.Count Then
            Return Nothing
        End If
        If Not RecipeEdit.Node(sys.StageNo).ContainsKey(nodeID) Then
            Return Nothing
        End If
        If Not RecipeEdit.Pattern.ContainsKey(patternID) Then
            Return Nothing
        End If
        If roundNo >= RecipeEdit.Pattern(patternID).Round.Count Then
            Return Nothing
        End If
        If stepNo >= RecipeEdit.Pattern(patternID).Round(roundNo).CStep.Count Then
            Return Nothing
        End If
        '=== 當前步驟異常 ===

        Dim mIsNodeStart As Boolean = False

        Dim mNodeList As Dictionary(Of String, CRecipeNode).KeyCollection = RecipeEdit.Node(sys.StageNo).Keys
        For mNodeNo As Integer = mNodeList.Count - 1 To 0 Step -1 '對每個節點
            Dim mRoundMax As Integer
            Dim mStepMax As Integer
            Dim mPatternID As String
            If mNodeList(mNodeNo) = nodeID Then '目前節點, 採用相應資料
                mIsNodeStart = True
                mPatternID = patternID
                mRoundMax = roundNo
            Else '前一節點, 採用前節點Pattern資料
                mPatternID = RecipeEdit.Node(sys.StageNo)(mNodeList(mNodeNo)).PatternName
                mRoundMax = RecipeEdit.Pattern(mPatternID).Round.Count - 1
            End If
            If mIsNodeStart Then
                For mRoundNo As Integer = mRoundMax To 0 Step -1
                    If mNodeList(mNodeNo) = nodeID AndAlso mRoundNo = roundNo Then '同節點同一Round
                        mStepMax = stepNo - 1 '從前一步開始找
                    Else '不同Round, 使用該Round最大值來找
                        mStepMax = RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep.Count - 1
                    End If
                    For mStepNo As Integer = mStepMax To 0 Step -1 '往前找Step
                        Select Case RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(mStepNo).StepType
                            Case eStepFunctionType.Arc2D, eStepFunctionType.Arc3D, eStepFunctionType.Circle2D, eStepFunctionType.Circle3D, eStepFunctionType.Dots3D, eStepFunctionType.EndLine, eStepFunctionType.FirstLine, eStepFunctionType.Line3D, eStepFunctionType.Move3D
                                oldNodeID = mNodeList(mNodeNo)
                                Return RecipeEdit.Pattern(mPatternID).Round(mRoundNo).CStep(mStepNo)
                        End Select
                    Next
                Next

            End If
        Next

        Return Nothing '找不到
    End Function
    Function FindSelectValve(ByVal nodeID As String, ByVal patternID As String, ByVal roundNo As Integer, ByVal stepNo As Integer) As enmValve
        '=== 當前步驟異常 ===
        If sys.StageNo >= RecipeEdit.Node.Count Then
            Return Nothing
        End If
        If Not RecipeEdit.Node(sys.StageNo).ContainsKey(nodeID) Then
            Return Nothing
        End If
        If Not RecipeEdit.Pattern.ContainsKey(patternID) Then
            Return Nothing
        End If
        If roundNo >= RecipeEdit.Pattern(patternID).Round.Count Then
            Return Nothing
        End If
        If stepNo >= RecipeEdit.Pattern(patternID).Round(roundNo).CStep.Count Then
            Return Nothing
        End If
        '=== 當前步驟異常 ===

        For i = stepNo To 0 Step -1
            If RecipeEdit.Pattern(patternID).Round(roundNo).CStep(i).StepType = eStepFunctionType.SelectValve Then
                Select Case RecipeEdit.Pattern(patternID).Round(roundNo).CStep(i).SelectValve.ValveNo
                    Case eValveWorkMode.Valve1
                        Return enmValve.No1 '閥號暫存
                    Case eValveWorkMode.Valve2
                        Return enmValve.No2 '閥號暫存
                End Select
            End If
        Next
        Return enmValve.No1 '找不到

    End Function

    Private Sub btnDot3DGetPos_Click(sender As Object, e As EventArgs) Handles btnDot3DGetPos.Click
        '20170616_Start
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex
        Dim mStepNo As Integer = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
        Dim mNowStep As CPatternStep = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
        Dim mOldNodeID As String = ""
        Dim mNowNodeID As String = TmpFrmXX.SelectedNodePath
        Dim mOldStep As CPatternStep = FindOldMotionStep(mNowNodeID, nodename, mRoundNo, mStepNo, mOldNodeID)

        '20170606_Toby add
        '判斷是不是新增的step
        If AddNewStep = False Then
            '非新增，抓取前一個step資訊
            GetStepPos(mNowNodeID, mOldNodeID, mOldStep, txtDots3DPosX, txtDots3DPosY, txtDots3DPosZ)
        Else
            '新增，抓取目前選擇step 資訊
            GetStepPos(mNowNodeID, mNowNodeID, mNowStep, txtDots3DPosX, txtDots3DPosY, txtDots3DPosZ)
        End If

        '20170616_End
    End Sub

    Private Sub btnLine3DGetPos_Click(sender As Object, e As EventArgs) Handles btnLine3DGetPos.Click
        '20170616_Start
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex
        Dim mStepNo As Integer = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
        Dim mNowStep As CPatternStep = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
        Dim mOldNodeID As String = ""
        Dim mNowNodeID As String = TmpFrmXX.SelectedNodePath
        Dim mOldStep As CPatternStep = FindOldMotionStep(mNowNodeID, nodename, mRoundNo, mStepNo, mOldNodeID)

        '20170606_Toby add
        '判斷是不是新增的step
        If AddNewStep = False Then
            '非新增，抓取前一個step資訊
            GetStepPos(mNowNodeID, mOldNodeID, mOldStep, txtLine3DStartPosX, txtLine3DStartPosY, txtLine3DStartPosZ)
        Else
            '新增，抓取目前選擇step 資訊
            GetStepPos(mNowNodeID, mNowNodeID, mNowStep, txtLine3DStartPosX, txtLine3DStartPosY, txtLine3DStartPosZ)
        End If
        '20170616_End
    End Sub

    Private Sub btnArcGetPos_Click(sender As Object, e As EventArgs) Handles btnArcGetPos.Click
        '20170616_Start
        Dim nodename As String = TmpFrmXX.treePattern.SelectedNode.Text
        Dim mPatternID As String = TmpFrmXX.lstPatternID.SelectedItem
        Dim mRoundNo As Integer = TmpFrmXX.lstRoundNo.SelectedIndex
        Dim mStepNo As Integer = TmpFrmXX.dgvStep.SelectedCells(0).RowIndex
        Dim mNowStep As CPatternStep = RecipeEdit.Pattern(nodename).Round(mRoundNo).CStep(mStepNo)
        Dim mOldNodeID As String = ""
        Dim mNowNodeID As String = TmpFrmXX.SelectedNodePath
        Dim mOldStep As CPatternStep = FindOldMotionStep(mNowNodeID, nodename, mRoundNo, mStepNo, mOldNodeID)

        '20170606_Toby add
        '判斷是不是新增的step
        If AddNewStep = False Then
            '非新增，抓取前一個step資訊
            GetStepPos(mNowNodeID, mOldNodeID, mOldStep, txtArcStartPosX, txtArcStartPosY, txtArcStartPosZ)
        Else
            '新增，抓取目前選擇step 資訊
            GetStepPos(mNowNodeID, mNowNodeID, mNowStep, txtArcStartPosX, txtArcStartPosY, txtArcStartPosZ)
        End If
        '20170616_End
    End Sub
    '=== Soni 2017.03.22 Mobary路徑串接需求 ===


    Private Sub cmbPosB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPosB.SelectedIndexChanged
        lbConvertTiltAngle.Text = ConverTiltAngle(CDbl(cmbPosB.Text), 3).ToString("##.###")
    End Sub



    Private Sub cmbValve_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbValve.SelectedIndexChanged
        Dim mSelectParameter As SSelectValveParameter

        mSelectParameter.ValveNo = cmbValve.SelectedIndex
        '=== Tilt角度選單 ===
        cmbPosB.Items.Clear()
        For i As Integer = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).DicCCDTiltValveCalib(mSelectParameter.ValveNo).Keys.Count - 1 '對每個角度Key, 加入清單選項
            cmbPosB.Items.Add(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).DicCCDTiltValveCalib(mSelectParameter.ValveNo).Keys(i))
        Next

        If cmbPosB.Items.Count > 0 Then '如果有選項才做選項配接
            If cmbPosB.Items.Contains(mSelectParameter.PosB) Then '如果選項內有該角度, 才選
                cmbPosB.SelectedItem = mSelectParameter.PosB
            Else '校正無該角度資料, 則預選為第0項
                cmbPosB.SelectedIndex = 0
            End If
        End If

    End Sub
    '20170308_Toby
    Private Sub AlignmentRefreshUI()

        Select Case [Step].StepType
            Case eStepFunctionType.Arc2D
                txtArcStartPosX.Enabled = RecipeEdit.Alignable
                txtArcStartPosY.Enabled = RecipeEdit.Alignable
                txtArcStartPosZ.Enabled = RecipeEdit.Alignable
                btnArcStartSet.Enabled = RecipeEdit.Alignable
                btnArcStartMove.Enabled = RecipeEdit.Alignable
                txtArcMidPosX.Enabled = RecipeEdit.Alignable
                txtArcMidPosY.Enabled = RecipeEdit.Alignable
                txtArcMidPosZ.Enabled = RecipeEdit.Alignable
                btnArcMidSet.Enabled = RecipeEdit.Alignable
                btnArcMidMove.Enabled = RecipeEdit.Alignable
                txtArcEndPosX.Enabled = RecipeEdit.Alignable
                txtArcEndPosY.Enabled = RecipeEdit.Alignable
                txtArcEndPosZ.Enabled = RecipeEdit.Alignable
                btnArcEndSet.Enabled = RecipeEdit.Alignable
                btnArcEndMove.Enabled = RecipeEdit.Alignable
                txtArcAngle.Enabled = RecipeEdit.Alignable
                txtArcCenterPosX.Enabled = RecipeEdit.Alignable
                txtArcCenterPosY.Enabled = RecipeEdit.Alignable
                txtArcCenterPosZ.Enabled = RecipeEdit.Alignable
                nmuArcWeight.Enabled = RecipeEdit.Alignable
                nmuArcDot.Enabled = RecipeEdit.Alignable

                txtArcComment.Enabled = RecipeEdit.Alignable
                btnArcGetPos.Enabled = RecipeEdit.Alignable
                btnArcDone.Enabled = RecipeEdit.Alignable
            Case eStepFunctionType.Arc3D
                txtArc3DStartPosX.Enabled = RecipeEdit.Alignable
                txtArc3DStartPosY.Enabled = RecipeEdit.Alignable
                txtArc3DStartPosZ.Enabled = RecipeEdit.Alignable
                btnArc3DStartSet.Enabled = RecipeEdit.Alignable
                btnArc3DStartrMove.Enabled = RecipeEdit.Alignable
                txtArc3DCenterPosX.Enabled = RecipeEdit.Alignable
                txtArc3DCenterPosY.Enabled = RecipeEdit.Alignable
                txtArc3DCenterPosZ.Enabled = RecipeEdit.Alignable
                btnArc3DCenterSet.Enabled = RecipeEdit.Alignable
                btnArc3DCenterMove.Enabled = RecipeEdit.Alignable
                txtArc3DEndPosX.Enabled = RecipeEdit.Alignable
                txtArc3DEndPosY.Enabled = RecipeEdit.Alignable
                txtArc3DEndPosZ.Enabled = RecipeEdit.Alignable
                txtArc3DAngle.Enabled = RecipeEdit.Alignable
                btnArc3DEndSet.Enabled = RecipeEdit.Alignable
                btnArc3DEndMove.Enabled = RecipeEdit.Alignable
                btnArc3DDone.Enabled = RecipeEdit.Alignable
            Case eStepFunctionType.Circle2D

                txtCircleStartPosX.Enabled = RecipeEdit.Alignable
                txtCircleStartPosY.Enabled = RecipeEdit.Alignable
                txtCircleStartPosZ.Enabled = RecipeEdit.Alignable
                btnCircleStartSet.Enabled = RecipeEdit.Alignable
                btnCircleStartMove.Enabled = RecipeEdit.Alignable
                txtCircleMidPosX.Enabled = RecipeEdit.Alignable
                txtCircleMidPosY.Enabled = RecipeEdit.Alignable
                txtCircleMidPosZ.Enabled = RecipeEdit.Alignable
                btnCircleMidSet.Enabled = RecipeEdit.Alignable
                btnCircleMidMove.Enabled = RecipeEdit.Alignable
                txtCircleMid2PosX.Enabled = RecipeEdit.Alignable
                txtCircleMid2PosY.Enabled = RecipeEdit.Alignable
                txtCircleMid2PosZ.Enabled = RecipeEdit.Alignable
                btnCircleEndSet.Enabled = RecipeEdit.Alignable
                btnCircleEndMove.Enabled = RecipeEdit.Alignable
                txtCircleCenterPosX.Enabled = RecipeEdit.Alignable
                txtCircleCenterPosY.Enabled = RecipeEdit.Alignable
                txtCircleCenterPosZ.Enabled = RecipeEdit.Alignable
                nmuCircleWeight.Enabled = RecipeEdit.Alignable
                nmuCircleDot.Enabled = RecipeEdit.Alignable

                txtCircleComment.Enabled = RecipeEdit.Alignable
                btnCircleDone.Enabled = RecipeEdit.Alignable

            Case eStepFunctionType.Circle3D
                txtCircle3DEndPosX.Enabled = RecipeEdit.Alignable
                txtCircle3DEndPosY.Enabled = RecipeEdit.Alignable
                txtCircle3DEndPosZ.Enabled = RecipeEdit.Alignable
                btnCircle3DEndSet.Enabled = RecipeEdit.Alignable
                btnCircle3DEndMove.Enabled = RecipeEdit.Alignable
                txtCircle3DCenterPosX.Enabled = RecipeEdit.Alignable
                txtCircle3DCenterPosY.Enabled = RecipeEdit.Alignable
                txtCircle3DCenterPosZ.Enabled = RecipeEdit.Alignable
                btnCircleCenterSet.Enabled = RecipeEdit.Alignable
                btnCircle3DCenterMove.Enabled = RecipeEdit.Alignable
                btnCircle3DDone.Enabled = RecipeEdit.Alignable
            Case eStepFunctionType.ContiEnd
                btnContiEndDone.Enabled = RecipeEdit.Alignable
            Case eStepFunctionType.ContiStart
                btnContiStartDone.Enabled = RecipeEdit.Alignable
            Case eStepFunctionType.Dots3D
                txtDots3DPosX.Enabled = RecipeEdit.Alignable
                txtDots3DPosY.Enabled = RecipeEdit.Alignable
                txtDots3DPosZ.Enabled = RecipeEdit.Alignable
                txtDots3DVelocity.Enabled = RecipeEdit.Alignable
                btnDots3DSet.Enabled = RecipeEdit.Alignable
                btnDots3DGo.Enabled = RecipeEdit.Alignable
                cbDotTypeSelect.Enabled = RecipeEdit.Alignable
                btDotTypeSelect.Enabled = RecipeEdit.Alignable
                btnDot3DGetPos.Enabled = RecipeEdit.Alignable
                btnDots3DDone.Enabled = RecipeEdit.Alignable
            Case eStepFunctionType.ExtendOff
                btnExtendOffDone.Enabled = RecipeEdit.Alignable
            Case eStepFunctionType.ExtendOn
                btnExtendOnDone.Enabled = RecipeEdit.Alignable
            Case eStepFunctionType.Line3D
                txtLine3DStartPosX.Enabled = RecipeEdit.Alignable
                txtLine3DStartPosY.Enabled = RecipeEdit.Alignable
                txtLine3DStartPosZ.Enabled = RecipeEdit.Alignable
                cbLineTypeSelect.Enabled = RecipeEdit.Alignable
                btLineTypeSelect.Enabled = RecipeEdit.Alignable
                btnLine3DStartSet.Enabled = RecipeEdit.Alignable
                btnLine3DStartMove.Enabled = RecipeEdit.Alignable
                txtLine3DEndPosX.Enabled = RecipeEdit.Alignable
                txtLine3DEndPosY.Enabled = RecipeEdit.Alignable
                txtLine3DEndPosZ.Enabled = RecipeEdit.Alignable
                btnLine3DEndSet.Enabled = RecipeEdit.Alignable
                btnLine3DEndMove.Enabled = RecipeEdit.Alignable
                nmuLine3DWeight.Enabled = RecipeEdit.Alignable
                nmuLine3DDot.Enabled = RecipeEdit.Alignable

                txtLine3DComment.Enabled = RecipeEdit.Alignable
                btnLine3DGetPos.Enabled = RecipeEdit.Alignable
                btnLine3DDone.Enabled = RecipeEdit.Alignable
            Case eStepFunctionType.Move3D
                txtMove3DEndPosX.Enabled = RecipeEdit.Alignable
                txtMove3DEndPosY.Enabled = RecipeEdit.Alignable
                txtMove3DEndPosZ.Enabled = RecipeEdit.Alignable
                btnMove3DSet.Enabled = RecipeEdit.Alignable
                btnMove3DGo.Enabled = RecipeEdit.Alignable
                btnMove3DDone.Enabled = RecipeEdit.Alignable
            Case eStepFunctionType.SelectValve
                cmbValve.Enabled = RecipeEdit.Alignable
                cmbPosB.Enabled = RecipeEdit.Alignable
                btnSelectValveDone.Enabled = RecipeEdit.Alignable
            Case eStepFunctionType.Wait
                txtWaitDwellTime.Enabled = RecipeEdit.Alignable
                btnWaitDone.Enabled = RecipeEdit.Alignable

        End Select
    End Sub

    Private Sub nmuLine3DWeight_ValueChanged(sender As Object, e As EventArgs) Handles nmuLine3DWeight.ValueChanged
        If mIsLoaded = False Then
            Exit Sub
        End If

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Weight
                mDisDx = (txtLine3DEndPosX.Text - txtLine3DStartPosX.Text)
                mDisDy = (txtLine3DEndPosY.Text - txtLine3DStartPosY.Text)
                mSubDistance = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)))
                Dim mWmin As Decimal, mWmax As Decimal
                If GetLineWeightLimit(mSubDistance, mWmin, mWmax) Then
                    nmuLine3DWeight.Minimum = mWmin
                    nmuLine3DWeight.Maximum = mWmax
                End If

                [Step].Line3D.WeightControl.Weight = nmuLine3DWeight.Value
                If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) > 0 Then
                    [Step].Line3D.WeightControl.DotCounts = [Step].Line3D.WeightControl.Weight / RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                    If [Step].Line3D.WeightControl.DotCounts > 0 Then
                        [Step].Line3D.WeightControl.dotPitch = mSubDistance / [Step].Line3D.WeightControl.DotCounts * 1000
                        txtLine3DPitch.Text = [Step].Line3D.WeightControl.dotPitch.ToString("##0.###")
                    ElseIf [Step].Line3D.WeightControl.DotCounts <= 0 Then
                        txtLine3DPitch.Text = ""
                    End If
                End If
                SetNumericUpDownValue(nmuLine3DDot, [Step].Line3D.WeightControl.DotCounts)
                [Step].Line3D.WeightControl.Velocity = [Step].Line3D.WeightControl.dotPitch / mCycleTimes(sys.StageNo)
                [Step].Line3D.WeightControl.dotPitch = [Step].Line3D.WeightControl.Velocity / mCycleTimes(sys.StageNo)
                SetNumericUpDownValue(nmuLine3DVelocity, [Step].Line3D.WeightControl.Velocity)

        End Select
    End Sub

    Private Sub nmuLine3DDot_ValueChanged(sender As Object, e As EventArgs) Handles nmuLine3DDot.ValueChanged
        If mIsLoaded = False Then
            Exit Sub
        End If

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                '0804
                mDisDx = (txtLine3DEndPosX.Text - txtLine3DStartPosX.Text)
                mDisDy = (txtLine3DEndPosY.Text - txtLine3DStartPosY.Text)
                mSubDistance = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)))
                Dim mDotMax, mDotMin As Decimal
                GetLineDotLimit(mSubDistance, mDotMin, mDotMax)
                nmuLine3DDot.Maximum = mDotMax
                nmuLine3DDot.Minimum = mDotMin
                [Step].Line3D.WeightControl.dotPitch = mSubDistance / (Val(nmuLine3DDot.Value) - 1) * 1000

                txtLine3DPitch.Text = [Step].Line3D.WeightControl.dotPitch.ToString("##0.###") 'dot數量減1算pitch
                'SetNumericUpDownValue(nmuLine3DDot, mSubDistance * 1000 / [Step].Line3D.WeightControl.dotPitch) '用Pitch限定點數

                [Step].Line3D.WeightControl.Velocity = [Step].Line3D.WeightControl.dotPitch / mCycleTimes(sys.StageNo)
                SetNumericUpDownValue(nmuLine3DVelocity, [Step].Line3D.WeightControl.Velocity)

                'SetNumericUpDownValue(nmuLine3DDot, mSubDistance * 1000 / [Step].Line3D.WeightControl.dotPitch) '用速度限定點數
                '[Step].Line3D.WeightControl.DotCounts = nmuLine3DDot.Value

                [Step].Line3D.WeightControl.Weight = [Step].Line3D.WeightControl.DotCounts * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                SetNumericUpDownValue(nmuLine3DWeight, [Step].Line3D.WeightControl.Weight)
        End Select
    End Sub

    Private Sub nmuLine3DVelocity_ValueChanged(sender As Object, e As EventArgs) Handles nmuLine3DVelocity.ValueChanged
        If mIsLoaded = False Then
            Exit Sub
        End If

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Velocity
                mDisDx = (txtLine3DEndPosX.Text - txtLine3DStartPosX.Text)
                mDisDy = (txtLine3DEndPosY.Text - txtLine3DStartPosY.Text)
                mSubDistance = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)))
                Dim mVmin As Decimal
                Dim mVmax As Decimal
                GetLineVelocityLimit(mSubDistance, mVmin, mVmax)
                nmuLine3DVelocity.Maximum = mVmax ' 1000 * mSubDistance / mCycleTimes(sys.StageNo) '速度上限
                nmuLine3DVelocity.Minimum = mVmin ' 10 / mCycleTimes(sys.StageNo) '速度下限
                [Step].Line3D.WeightControl.Velocity = nmuLine3DVelocity.Value
                [Step].Line3D.WeightControl.dotPitch = [Step].Line3D.WeightControl.Velocity * mCycleTimes(sys.StageNo) / 1000

                [Step].Line3D.WeightControl.DotCounts = CInt(mSubDistance / [Step].Line3D.WeightControl.dotPitch) + 1
                If [Step].Line3D.WeightControl.DotCounts > 1 Then
                    [Step].Line3D.WeightControl.dotPitch = mSubDistance / ([Step].Line3D.WeightControl.DotCounts - 1) * 1000
                    txtLine3DPitch.Text = [Step].Line3D.WeightControl.dotPitch.ToString("##0.###") 'dot數量減1算pitch


                    SetNumericUpDownValue(nmuLine3DDot, [Step].Line3D.WeightControl.DotCounts)
                    [Step].Line3D.WeightControl.Weight = [Step].Line3D.WeightControl.DotCounts * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                    SetNumericUpDownValue(nmuLine3DWeight, [Step].Line3D.WeightControl.Weight)
                End If

        End Select
    End Sub

    ''' <summary>
    ''' 圓,弧的速度限制
    ''' </summary>
    ''' <param name="arcLength"></param>
    ''' <param name="radius"></param>
    ''' <param name="acc"></param>
    ''' <param name="vmin"></param>
    ''' <param name="vmax"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetArcVelocityLimit(ByVal arcLength As Decimal, ByVal radius As Decimal, ByVal acc As Decimal, ByRef vmin As Decimal, ByRef vmax As Decimal) As Boolean
        If GetLineVelocityLimit(arcLength, vmin, vmax) Then
            Dim mVmax As Decimal = Math.Sqrt(acc * radius)
            If vmax > mVmax Then vmax = mVmax
            Return True
        End If
        Return False
    End Function

    ''' <summary>弧的點數限制
    ''' </summary>
    ''' <param name="arcLength"></param>
    ''' <param name="radius"></param>
    ''' <param name="acc"></param>
    ''' <param name="dmin"></param>
    ''' <param name="dmax"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetArcDotLimit(ByVal arcLength As Decimal, ByVal radius As Decimal, ByVal acc As Decimal, ByRef dmin As Decimal, ByRef dmax As Decimal) As Boolean
        If GetLineDotLimit(arcLength, dmin, dmax) Then
            Dim mVmax As Decimal = Math.Sqrt(acc * radius)
            Dim mPitchMax As Decimal = mVmax * mCycleTimes(sys.StageNo)
            If mPitchMax > 0 Then
                Dim mDotMin As Decimal = (arcLength / mPitchMax) + 1
                If dmin < mDotMin Then dmin = mDotMin
            End If

            Return True
        End If
        Return False
    End Function



    Function GetArcWeightLimit(ByVal arcLength As Decimal, ByVal radius As Decimal, ByVal acc As Decimal, ByRef wmin As Decimal, ByRef wmax As Decimal) As Boolean
        If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) = 0 Then '無法換算
            Return False
        End If
        Dim dmin As Decimal, dmax As Decimal
        If GetArcDotLimit(arcLength, radius, acc, dmin, dmax) = True Then
            wmin = dmin * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
            wmax = dmax * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
            Return True
        Else
            Return False
        End If
    End Function

    Function GetCircleWeightLimit(ByVal arcLength As Decimal, ByVal radius As Decimal, ByVal acc As Decimal, ByRef wmin As Decimal, ByRef wmax As Decimal) As Boolean
        If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) = 0 Then '無法換算
            Return False
        End If
        Dim dmin As Decimal, dmax As Decimal
        If GetCircleDotLimit(arcLength, radius, acc, dmin, dmax) = True Then
            wmin = dmin * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
            wmax = dmax * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
            Return True
        Else
            Return False
        End If
    End Function



    ''' <summary>
    ''' 線的速度線制
    ''' </summary>
    ''' <param name="length"></param>
    ''' <param name="vmin"></param>
    ''' <param name="vmax"></param>
    ''' <returns></returns>
    ''' <remarks>與弧的差異在於向心加速度限制</remarks>
    Function GetLineVelocityLimit(ByVal length As Decimal, ByRef vmin As Decimal, ByRef vmax As Decimal) As Boolean
        Dim mPitchmin As Decimal = 0.01 '最短打點間距0.01mm.
        If length < mPitchmin Then '點數不足
            vmin = 20
            vmax = 20
            Return False
        End If
        vmin = mPitchmin / mCycleTimes(sys.StageNo)
        If vmin < 20 Then vmin = 20 '最低移動速度限制20mm/s
        vmax = 1000 * length / mCycleTimes(sys.StageNo)
        If vmax > gSSystemParameter.MaxDispVelocity Then
            If gSSystemParameter.MaxDispVelocity <> 0 Then
                vmax = gSSystemParameter.MaxDispVelocity
            End If

        End If
        Return True
    End Function

    ''' <summary>
    ''' 圓的點數限制
    ''' </summary>
    ''' <param name="length"></param>
    ''' <param name="dotmin"></param>
    ''' <param name="dotmax"></param>
    ''' <returns></returns>
    ''' <remarks>差別在於, 圓起終點相同, 不需多加1點</remarks>
    Function GetCircleDotLimit(ByVal length As Decimal, ByVal radius As Decimal, ByVal acc As Decimal, ByRef dotmin As Decimal, ByRef dotmax As Decimal) As Boolean
        Dim mPitchmin As Decimal = 0.01 'mm
        dotmin = 2
        If length < mPitchmin Then '點數不足
            dotmax = 2
            Return False
        End If
        Dim mDotMax1 As Decimal = CInt(length / mPitchmin)
        Dim mVmin As Decimal = 20 'mm/s
        Dim mVmax As Decimal = gSSystemParameter.MaxDispVelocity
        Dim mPitchmin2 As Decimal = mVmin * mCycleTimes(sys.StageNo) / 1000

        If mPitchmin2 > 0 Then
            Dim mDotMax2 As Decimal = CInt(length / mPitchmin2)
            If mDotMax1 > mDotMax2 Then
                dotmax = mDotMax2
            Else
                dotmax = mDotMax1
            End If
        Else
            dotmax = mDotMax1
        End If
        If mVmax > 0 Then
            Dim mPitchmax2 As Decimal
            mPitchmax2 = mVmax * mCycleTimes(sys.StageNo) / 1000
            If mPitchmax2 > 0 Then
                Dim mDotmin2 As Decimal = CInt(length / mPitchmax2)
                If mDotmin2 > dotmin Then
                    dotmin = mDotmin2
                End If
            End If
        End If
        Dim mVmax2 As Decimal = Math.Sqrt(acc * radius)
        Dim mPitchMax As Decimal = mVmax2 * mCycleTimes(sys.StageNo)
        If mPitchMax > 0 Then
            Dim mDotMin As Decimal = (length / mPitchMax) + 1
            If dotmin < mDotMin Then dotmin = mDotMin
        End If

        Return True
    End Function

    ''' <summary>
    ''' 線,弧的點數限制
    ''' </summary>
    ''' <param name="length"></param>
    ''' <param name="dotmin"></param>
    ''' <param name="dotmax"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLineDotLimit(ByVal length As Decimal, ByRef dotmin As Decimal, ByRef dotmax As Decimal) As Boolean
        Dim mPitchmin As Decimal = 0.01 'mm
        dotmin = 2
        If length < mPitchmin Then '點數不足
            dotmax = 2
            Return False
        End If
        Dim mDotMax1 As Decimal = CInt(length / mPitchmin) + 1
        Dim mVmin As Decimal = 20 'mm/s
        Dim mVmax As Decimal = gSSystemParameter.MaxDispVelocity
        Dim mPitchmin2 As Decimal = mVmin * mCycleTimes(sys.StageNo) / 1000

        If mPitchmin2 > 0 Then
            Dim mDotMax2 As Decimal = CInt(length / mPitchmin2) + 1
            If mDotMax1 > mDotMax2 Then
                dotmax = mDotMax2
            Else
                dotmax = mDotMax1
            End If
        Else
            dotmax = mDotMax1
        End If
        If mVmax > 0 Then
            Dim mPitchmax2 As Decimal
            mPitchmax2 = mVmax * mCycleTimes(sys.StageNo) / 1000
            If mPitchmax2 > 0 Then
                Dim mDotmin2 As Decimal = CInt(length / mPitchmax2) + 1
                If mDotmin2 > dotmin Then
                    dotmin = mDotmin2
                End If
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' 線的重量限制
    ''' </summary>
    ''' <param name="length"></param>
    ''' <param name="wmin"></param>
    ''' <param name="wmax"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLineWeightLimit(ByVal length As Decimal, ByRef wmin As Decimal, ByRef wmax As Decimal) As Boolean
        If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) = 0 Then '無法換算
            Return False
        End If
        Dim dmin As Decimal, dmax As Decimal
        If GetLineDotLimit(length, dmin, dmax) = True Then
            wmin = dmin * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
            wmax = dmax * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub nmuArcVelocity_ValueChanged(sender As Object, e As EventArgs) Handles nmuArcVelocity.ValueChanged
        If mIsLoaded = False Then
            Exit Sub
        End If

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Velocity
                Dim mRadius As Decimal
                GetArcLengthRadius(mSubDistance, mRadius)

                Dim mAcc As Decimal = gCMotion.SyncParameter(0).Velocity.Acc * gCMotion.SyncParameter(0).Velocity.AccRatio

                Dim mVmin As Decimal
                Dim mVmax As Decimal
                GetArcVelocityLimit(mSubDistance, mRadius, mAcc, mVmin, mVmax)
                nmuArcVelocity.Maximum = mVmax ' 1000 * mSubDistance / mCycleTimes(sys.StageNo) '速度上限
                nmuArcVelocity.Minimum = mVmin ' 10 / mCycleTimes(sys.StageNo) '速度下限
                [Step].Arc2D.WeightControl.Velocity = nmuArcVelocity.Value
                [Step].Arc2D.WeightControl.dotPitch = [Step].Arc2D.WeightControl.Velocity * mCycleTimes(sys.StageNo) / 1000
                [Step].Arc2D.WeightControl.DotCounts = CInt(mSubDistance / [Step].Arc2D.WeightControl.dotPitch) + 1


                If [Step].Arc2D.WeightControl.DotCounts > 1 Then
                    [Step].Arc2D.WeightControl.dotPitch = mSubDistance / ([Step].Arc2D.WeightControl.DotCounts - 1) * 1000
                    txtArcPitch.Text = [Step].Arc2D.WeightControl.dotPitch.ToString("##0.###") 'dot數量減1算pitch
                    SetNumericUpDownValue(nmuArcDot, [Step].Arc2D.WeightControl.DotCounts)
                    [Step].Arc2D.WeightControl.Weight = [Step].Arc2D.WeightControl.DotCounts * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                    SetNumericUpDownValue(nmuArcWeight, [Step].Arc2D.WeightControl.Weight)
                Else
                    txtArcPitch.Text = ""
                End If

        End Select
    End Sub

    Private Sub nmuArcDot_ValueChanged(sender As Object, e As EventArgs) Handles nmuArcDot.ValueChanged
        If mIsLoaded = False Then
            Exit Sub
        End If

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                Dim mRadius As Decimal
                GetArcLengthRadius(mSubDistance, mRadius)

                Dim mAcc As Decimal = gCMotion.SyncParameter(0).Velocity.Acc * gCMotion.SyncParameter(0).Velocity.AccRatio
                Dim mDotMax, mDotMin As Decimal
                GetArcDotLimit(mSubDistance, mRadius, mAcc, mDotMin, mDotMax)
                nmuArcDot.Maximum = mDotMax
                nmuArcDot.Minimum = mDotMin
                [Step].Arc2D.WeightControl.dotPitch = mSubDistance / (Val(nmuArcDot.Value) - 1) * 1000

                txtArcPitch.Text = [Step].Arc2D.WeightControl.dotPitch.ToString("##0.###") 'dot數量減1算pitch

                [Step].Arc2D.WeightControl.Velocity = [Step].Arc2D.WeightControl.dotPitch / mCycleTimes(sys.StageNo)
                SetNumericUpDownValue(nmuArcVelocity, [Step].Arc2D.WeightControl.Velocity)

                [Step].Arc2D.WeightControl.Weight = [Step].Arc2D.WeightControl.DotCounts * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                SetNumericUpDownValue(nmuArcWeight, [Step].Arc2D.WeightControl.Weight)
        End Select
    End Sub

    Function GetCircleLengthRadius(ByRef circleLength As Decimal, ByRef radius As Decimal) As Boolean
        Dim Circle As Circle
        Dim x, y, z As CPoint
        '[說明]:計算圓心座標及畫圓方向

        x = New CPoint(Val(txtCircleStartPosX.Text), Val(txtCircleStartPosY.Text))
        y = New CPoint(Val(txtCircleMidPosX.Text), Val(txtCircleMidPosY.Text))
        z = New CPoint(Val(txtCircleMid2PosX.Text), Val(txtCircleMid2PosY.Text))

        '[說明]:計算圓心座標及畫圓方向
        Circle = CMath.GetCircleby3Point(x, y, z)

        '[說明]:計算Pitch長度 
        '[Note]:弧長= 2 * PI * R * Angle / 360
        '           = 2 * PI * R * 360 / 360
        '           = 2 * PI * R
        'mDisDx = (z.PointX - Circle.PointX)
        'mDisDy = (z.PointY - Circle.PointY)
        'mSubDistance = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)) * Math.PI * Circle.Angle * 2)
        '[說明]:計算Pitch長度 20160920
        '[Note]:弧長= 2 * PI * R * Angle / 360
        '           = 2 * PI * R * 360 / 360
        '           = 2 * PI * R
        mDisDx = (z.PointX - Circle.PointX)
        mDisDy = (z.PointY - Circle.PointY)
        radius = CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2))
        circleLength = Math.Abs(CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2)) * Math.PI * 2)
        Return True
    End Function

    Function GetArcLengthRadius(ByRef arcLength As Decimal, ByRef radius As Decimal) As Boolean
        Dim Circle As Circle
        Dim x, y, z As CPoint

        x = New CPoint(Val(txtArcStartPosX.Text), Val(txtArcStartPosY.Text))
        y = New CPoint(Val(txtArcMidPosX.Text), Val(txtArcMidPosY.Text))
        z = New CPoint(Val(txtArcEndPosX.Text), Val(txtArcEndPosY.Text))

        '[說明]:計算Arc圓心座標
        Circle = CMath.GetCircleby3Point(x, y, z)

        '[說明]:計算Pitch長度 
        '[Note]:弧長=2*PI*R*Angle/360
        '           =PI*R*Angle/180
        mDisDx = (z.PointX - Circle.PointX)
        mDisDy = (z.PointY - Circle.PointY)
        radius = CDec(Math.Sqrt(mDisDx ^ 2 + mDisDy ^ 2))
        arcLength = Math.Abs(radius * Math.PI * Circle.Angle / 180)
        Return True
    End Function

    Private Sub nmuArcWeight_ValueChanged(sender As Object, e As EventArgs) Handles nmuArcWeight.ValueChanged
        If mIsLoaded = False Then
            Exit Sub
        End If

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Weight
                Dim mRadius As Decimal
                GetArcLengthRadius(mSubDistance, mRadius)
                Dim mAcc As Decimal = gCMotion.SyncParameter(0).Velocity.Acc * gCMotion.SyncParameter(0).Velocity.AccRatio
                Dim mWmin As Decimal, mWmax As Decimal
                If GetArcWeightLimit(mSubDistance, mRadius, mAcc, mWmin, mWmax) Then
                    nmuArcWeight.Minimum = mWmin
                    nmuArcWeight.Maximum = mWmax
                End If

                [Step].Arc2D.WeightControl.Weight = nmuArcWeight.Value
                If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) > 0 Then
                    [Step].Arc2D.WeightControl.DotCounts = [Step].Arc2D.WeightControl.Weight / RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                    If [Step].Arc2D.WeightControl.DotCounts > 0 Then
                        [Step].Arc2D.WeightControl.dotPitch = mSubDistance / [Step].Arc2D.WeightControl.DotCounts * 1000
                        txtArcPitch.Text = [Step].Arc2D.WeightControl.dotPitch.ToString("##0.###")
                    ElseIf [Step].Arc2D.WeightControl.DotCounts <= 0 Then
                        txtArcPitch.Text = ""
                    End If
                Else
                    txtArcPitch.Text = ""
                End If
                SetNumericUpDownValue(nmuArcDot, [Step].Arc2D.WeightControl.DotCounts)
                [Step].Arc2D.WeightControl.Velocity = [Step].Arc2D.WeightControl.dotPitch / mCycleTimes(sys.StageNo)
                [Step].Arc2D.WeightControl.dotPitch = [Step].Arc2D.WeightControl.Velocity / mCycleTimes(sys.StageNo)
                SetNumericUpDownValue(nmuArcVelocity, [Step].Arc2D.WeightControl.Velocity)

        End Select
    End Sub


    Private Sub nmuCircleWeight_ValueChanged(sender As Object, e As EventArgs) Handles nmuCircleWeight.ValueChanged
        If mIsLoaded = False Then
            Exit Sub
        End If

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Weight
                Dim mRadius As Decimal
                GetCircleLengthRadius(mSubDistance, mRadius)
                Dim mAcc As Decimal = gCMotion.SyncParameter(0).Velocity.Acc * gCMotion.SyncParameter(0).Velocity.AccRatio
                Dim mWmin As Decimal, mWmax As Decimal
                If GetCircleWeightLimit(mSubDistance, mRadius, mAcc, mWmin, mWmax) Then
                    nmuCircleWeight.Minimum = mWmin
                    nmuCircleWeight.Maximum = mWmax
                End If

                [Step].Circle2D.WeightControl.Weight = nmuCircleWeight.Value
                If RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve) > 0 Then
                    [Step].Circle2D.WeightControl.DotCounts = [Step].Circle2D.WeightControl.Weight / RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                    If [Step].Circle2D.WeightControl.DotCounts > 0 Then
                        [Step].Circle2D.WeightControl.dotPitch = mSubDistance / [Step].Circle2D.WeightControl.DotCounts * 1000
                        txtCirclePitch.Text = [Step].Circle2D.WeightControl.dotPitch.ToString("##0.###")
                    ElseIf [Step].Circle2D.WeightControl.DotCounts <= 0 Then
                        txtCirclePitch.Text = ""
                    End If
                Else
                    txtCirclePitch.Text = ""
                End If
                SetNumericUpDownValue(nmuCircleDot, [Step].Circle2D.WeightControl.DotCounts)
                [Step].Circle2D.WeightControl.Velocity = [Step].Circle2D.WeightControl.dotPitch / mCycleTimes(sys.StageNo)
                [Step].Circle2D.WeightControl.dotPitch = [Step].Circle2D.WeightControl.Velocity / mCycleTimes(sys.StageNo)
                SetNumericUpDownValue(nmuCircleVelocity, [Step].Circle2D.WeightControl.Velocity)

        End Select
    End Sub

    Private Sub nmuCircleDot_ValueChanged(sender As Object, e As EventArgs) Handles nmuCircleDot.ValueChanged
        If mIsLoaded = False Then
            Exit Sub
        End If

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                Dim mRadius As Decimal
                GetCircleLengthRadius(mSubDistance, mRadius)

                Dim mAcc As Decimal = gCMotion.SyncParameter(0).Velocity.Acc * gCMotion.SyncParameter(0).Velocity.AccRatio
                Dim mDotMax, mDotMin As Decimal
                GetCircleDotLimit(mSubDistance, mRadius, mAcc, mDotMin, mDotMax)
                nmuCircleDot.Maximum = mDotMax
                nmuCircleDot.Minimum = mDotMin
                [Step].Circle2D.WeightControl.dotPitch = mSubDistance / (Val(nmuCircleDot.Value) - 1) * 1000

                txtCirclePitch.Text = [Step].Circle2D.WeightControl.dotPitch.ToString("##0.###") 'dot數量減1算pitch

                [Step].Circle2D.WeightControl.Velocity = [Step].Circle2D.WeightControl.dotPitch / mCycleTimes(sys.StageNo)
                SetNumericUpDownValue(nmuCircleVelocity, [Step].Circle2D.WeightControl.Velocity)

                [Step].Circle2D.WeightControl.Weight = [Step].Circle2D.WeightControl.DotCounts * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                SetNumericUpDownValue(nmuCircleWeight, [Step].Circle2D.WeightControl.Weight)
        End Select
    End Sub


    Private Sub nmuCircleVelocity_ValueChanged(sender As Object, e As EventArgs) Handles nmuCircleVelocity.ValueChanged
        If mIsLoaded = False Then
            Exit Sub
        End If

        Select Case RecipeEdit.RunType
            Case eWeightControlType.Velocity
                Dim mRadius As Decimal
                GetCircleLengthRadius(mSubDistance, mRadius)

                Dim mAcc As Decimal = gCMotion.SyncParameter(0).Velocity.Acc * gCMotion.SyncParameter(0).Velocity.AccRatio

                Dim mVmin As Decimal
                Dim mVmax As Decimal
                GetArcVelocityLimit(mSubDistance, mRadius, mAcc, mVmin, mVmax)
                nmuCircleVelocity.Maximum = mVmax ' 1000 * mSubDistance / mCycleTimes(sys.StageNo) '速度上限
                nmuCircleVelocity.Minimum = mVmin ' 10 / mCycleTimes(sys.StageNo) '速度下限
                [Step].Circle2D.WeightControl.Velocity = nmuCircleVelocity.Value
                [Step].Circle2D.WeightControl.dotPitch = [Step].Circle2D.WeightControl.Velocity * mCycleTimes(sys.StageNo)
                [Step].Circle2D.WeightControl.DotCounts = CInt(mSubDistance * 1000 / [Step].Circle2D.WeightControl.dotPitch + 1)
                If [Step].Circle2D.WeightControl.DotCounts > 1 Then
                    [Step].Circle2D.WeightControl.dotPitch = mSubDistance / ([Step].Circle2D.WeightControl.DotCounts - 1) * 1000
                    txtCirclePitch.Text = [Step].Circle2D.WeightControl.dotPitch.ToString("##0.###") 'dot數量減1算pitch
                    SetNumericUpDownValue(nmuCircleDot, [Step].Circle2D.WeightControl.DotCounts)
                    [Step].Circle2D.WeightControl.Weight = [Step].Circle2D.WeightControl.DotCounts * RecipeEdit.StageParts(sys.StageNo).AverageWeightPerDot(WorkValve)
                    SetNumericUpDownValue(nmuCircleWeight, [Step].Circle2D.WeightControl.Weight)
                Else
                    txtCirclePitch.Text = ""
                End If

        End Select
    End Sub
End Class
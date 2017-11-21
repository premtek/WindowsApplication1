Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectLaserInterferometer
Imports ProjectAOI

Public Class frmGoArrayPosition

    Dim conveyorNo As eConveyor
    Public sys As sSysParam
    Public RecipeEdit As ProjectRecipe.CRecipe
    Public mMultiArrayAdapter As CMultiArrayAdapter
    Dim mAbsValveNo As Integer = enmValve.No1
    Private mStageNo As Integer
    Public Property _StageNo() As Integer
        Get
            Return mStageNo
        End Get
        Set(ByVal value As Integer)
            mStageNo = value
        End Set
    End Property

    Private mNode As String
    Public Property _Node() As String
        Get
            Return mNode
        End Get
        Set(ByVal value As String)
            mNode = value
        End Set
    End Property


    Private mArrayLevelCount As Integer
    Public Property _ArrayLevelCount() As Integer
        Get
            Return mArrayLevelCount
        End Get
        Set(ByVal value As Integer)
            mArrayLevelCount = value
        End Set
    End Property


    Private Sub frmGoArrayPosition_Shown(sender As Object, e As EventArgs)


    End Sub

    Private Sub btnGoCCDAlignPos_Click(sender As Object, e As EventArgs) Handles btnGoCCDAlignPos.Click
        gSyslog.Save("[frmRecipe02]" & vbTab & "[btnGoCCDAlignPos]" & vbTab & "Click")
        'btnGoCCDAlignPos.Enabled = False
        '20170602按鍵保護
        Btn_Control(False)

        Select Case gSYS(eSys.OverAll).Act(eAct.Home).RunStatus
            Case enmRunStatus.Finish
            Case enmRunStatus.Alarm
                gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)
                BtnHomeFirstBehavior(btnGoCCDAlignPos)
                'btnGoCCDAlignPos.Enabled = True
                '20170602按鍵保護
                Btn_Control(True)
                Exit Sub
            Case enmRunStatus.Running
                gEqpMsg.AddHistoryAlarm("Warn_3000006", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000006), eMessageLevel.Warning)
                BtnHomeFirstBehavior(btnGoCCDAlignPos)
                'btnGoCCDAlignPos.Enabled = True
                '20170602按鍵保護
                Btn_Control(True)
                Exit Sub
                'Case enmRunStatus.Pause
            Case enmRunStatus.None
                gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)
                BtnHomeFirstBehavior(btnGoCCDAlignPos)
                'btnGoCCDAlignPos.Enabled = True
                '20170602按鍵保護
                Btn_Control(True)
                Exit Sub
        End Select

        If RecipeEdit Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011), "Warn_3000011", eMessageLevel.Warning)
            'btnGoCCDAlignPos.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If

        If RecipeEdit.strFileName = "" Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011), "Warn_3000011", eMessageLevel.Warning)
            'btnGoCCDAlignPos.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If
        'Soni 2017.02.09 雙軌資料結構
        If RecipeEdit.Node(mStageNo)(mNode).ConveyorPos(conveyorNo).AlignmentData.Count <= 0 Then
            'TODO:請Soni確認
            gSyslog.Save(gMsgHandler.GetMessage(Error_1000000))
            MsgBox(gMsgHandler.GetMessage(Error_1000000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Alignment Not Exists!")
            'btnGoCCDAlignPos.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If
        Dim targetPosX As Decimal = RecipeEdit.Node(mStageNo)(mNode).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosX
        Dim targetPosY As Decimal = RecipeEdit.Node(mStageNo)(mNode).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosY


        '[說明]:回Home完成才能執行
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
            'If Not Recipe Is Nothing Then
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
            Select Case gSSystemParameter.CCDModuleType
                Case enmCCDModule.eFix
                Case enmCCDModule.eFree
                    Call gCMotion.AbsMove(sys.AxisZ, RecipeEdit.Node(mStageNo)(mNode).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ) 'Soni 2017.02.09 雙軌資料結構
            End Select

            Dim idx As New sLevelIndexCollection
            Dim uctemp As ucSelectArrayGoPosition = New ucSelectArrayGoPosition
            For index = 0 To Me.Controls.Count - 1
                If Me.Controls(index) IsNot Nothing Then
                    If Object.ReferenceEquals(Me.Controls(index).GetType, uctemp.GetType) Then
                        If DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbXa.SelectedIndex < 0 Then
                            DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbXa.SelectedIndex = 0
                        End If
                        If DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbYa.SelectedIndex < 0 Then
                            DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbYa.SelectedIndex = 0
                        End If
                        'MessageBox.Show(Me.Controls(index).Name)

                        If DirectCast(Me.Controls(index), ucSelectArrayGoPosition)._LevelNo <= mArrayLevelCount Then
                            If DirectCast(Me.Controls(index), ucSelectArrayGoPosition)._LevelNo = 0 Then
                                idx.Xf = DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbXa.SelectedIndex
                                idx.Yf = DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbYa.SelectedIndex
                            Else
                                idx.Xf = idx.Xf + DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbXa.SelectedIndex * mMultiArrayAdapter.GetLevelCountX(DirectCast(Me.Controls(index), ucSelectArrayGoPosition)._LevelNo - 1)
                                idx.Yf = idx.Yf + DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbYa.SelectedIndex * mMultiArrayAdapter.GetLevelCountY(DirectCast(Me.Controls(index), ucSelectArrayGoPosition)._LevelNo - 1)
                            End If
                        Else
                        End If
                    End If
                End If
            Next
            '    idx.Xf = intXf
            '    idx.Yf = intYf
            idx.NodeName = mNode
            Select Case RecipeEdit.Node(mStageNo)(gfrmRecipe04.SelectedNodePath).AlignType
                Case enmAlignType.DevicePos1
                    Call GetCCDScanPos(sys, idx, targetPosX, targetPosY, enmAlignType.DevicePos1, mMultiArrayAdapter)
                Case enmAlignType.DevicePos2
                    Static alignIndex As enmAlignType
                    If alignIndex = enmAlignType.DevicePos1 Then
                        Call GetCCDScanPos(sys, idx, targetPosX, targetPosY, enmAlignType.DevicePos1, mMultiArrayAdapter)
                        alignIndex = enmAlignType.DevicePos2
                    Else
                        Call GetCCDScanPos(sys, idx, targetPosX, targetPosY, enmAlignType.DevicePos2, mMultiArrayAdapter)
                        alignIndex = enmAlignType.DevicePos1
                    End If
            End Select
            Dim AxisNo(4) As Integer
            Dim TargetPos(4) As Decimal
            AxisNo(0) = sys.AxisX
            AxisNo(1) = sys.AxisY
            AxisNo(2) = sys.AxisZ
            AxisNo(3) = sys.AxisB
            AxisNo(4) = sys.AxisC
            TargetPos(0) = targetPosX
            TargetPos(1) = targetPosY
            TargetPos(2) = RecipeEdit.Node(mStageNo)(mNode).ConveyorPos(conveyorNo).AlignmentData(0).AlignPosZ
            TargetPos(3) = 0
            TargetPos(4) = 0
            ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
            'ucJoyStick1.RefreshPosition()

        End If
        btnGoCCDAlignPos.Enabled = True
        '20170602按鍵保護
        Btn_Control(True)
    End Sub


    Public Sub Btn_Control(ByVal disable As Boolean) '
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Me)) Then
            Me.BeginInvoke(Sub()
                               btnGoCCDAlignPos.Enabled = disable
                               btnGoFindHeightPos.Enabled = disable
                               btnGoDispenseBasePos.Enabled = disable
                           End Sub)
        End If

    End Sub

    Private Sub btnGoFindHeightPos_Click(sender As Object, e As EventArgs) Handles btnGoFindHeightPos.Click
        gSyslog.Save("[frmRecipe02]" & vbTab & "[btnGoFindHeightPos]" & vbTab & "Click")
        'btnGoFindHeightPos.Enabled = False
        '20170602按鍵保護
        Btn_Control(False)
        Select Case gSYS(eSys.OverAll).Act(eAct.Home).RunStatus
            Case enmRunStatus.Finish
            Case enmRunStatus.Alarm
                gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)
                BtnHomeFirstBehavior(btnGoCCDAlignPos)
                'btnGoFindHeightPos.Enabled = True
                '20170602按鍵保護
                Btn_Control(True)
                Exit Sub
            Case enmRunStatus.Running
                gEqpMsg.AddHistoryAlarm("Warn_3000006", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000006), eMessageLevel.Warning)
                BtnHomeFirstBehavior(btnGoCCDAlignPos)
                'btnGoFindHeightPos.Enabled = True
                '20170602按鍵保護
                Btn_Control(True)
                Exit Sub
                'Case enmRunStatus.Pause
            Case enmRunStatus.None
                gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)
                BtnHomeFirstBehavior(btnGoCCDAlignPos)
                'btnGoFindHeightPos.Enabled = True
                '20170602按鍵保護
                Btn_Control(True)
                Exit Sub
        End Select

        If RecipeEdit Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011), "Warn_3000011", eMessageLevel.Warning)
            'btnGoFindHeightPos.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If

        If RecipeEdit.strFileName = "" Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011), "Warn_3000011", eMessageLevel.Warning)
            'btnGoFindHeightPos.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If
        'Await Task.Run(Sub()
        '                   If gCMotion.GetPositionValue(sys.AxisZ) > gSSystemParameter.Pos.SafePosZ Then
        '                       gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos)
        '                       Do
        '                           System.Threading.Thread.Sleep(1)
        '                           If gDICollection.GetState(enmDI.EMO, True) = True Then
        '                               'btnGoFindHeightPos.Enabled = True
        '                               '20170602按鍵保護
        '                               Btn_Control(True)
        '                               Exit Sub
        '                           End If
        '                       Loop Until gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed
        '                   End If
        '               End Sub)

        gCMotion.SetVelAccDec(sys.AxisX)
        gCMotion.SetVelAccDec(sys.AxisY)
        '[說明]:Y軸


        Dim targetPosX As Decimal
        Dim targetPosY As Decimal
        'Dim index As New sLevelIndexCollection
        Dim idx As New sLevelIndexCollection
        Dim uctemp As ucSelectArrayGoPosition = New ucSelectArrayGoPosition
        For index = 0 To Me.Controls.Count - 1
            If Me.Controls(index) IsNot Nothing Then
                If Object.ReferenceEquals(Me.Controls(index).GetType, uctemp.GetType) Then
                    If DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbXa.SelectedIndex < 0 Then
                        DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbXa.SelectedIndex = 0
                    End If
                    If DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbYa.SelectedIndex < 0 Then
                        DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbYa.SelectedIndex = 0
                    End If
                    'MessageBox.Show(Me.Controls(index).Name)

                    If DirectCast(Me.Controls(index), ucSelectArrayGoPosition)._LevelNo <= mArrayLevelCount Then
                        If DirectCast(Me.Controls(index), ucSelectArrayGoPosition)._LevelNo = 0 Then
                            idx.Xf = DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbXa.SelectedIndex
                            idx.Yf = DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbYa.SelectedIndex
                        Else
                            idx.Xf = idx.Xf + DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbXa.SelectedIndex * mMultiArrayAdapter.GetLevelCountX(DirectCast(Me.Controls(index), ucSelectArrayGoPosition)._LevelNo - 1)
                            idx.Yf = idx.Yf + DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbYa.SelectedIndex * mMultiArrayAdapter.GetLevelCountY(DirectCast(Me.Controls(index), ucSelectArrayGoPosition)._LevelNo - 1)
                        End If
                    Else
                    End If
                End If
            End If
        Next
        'index.Xf = intXf
        'index.Yf = intYf
        idx.NodeName = mNode
        Call GetLaserPos(sys, idx, targetPosX, targetPosY, mMultiArrayAdapter)

        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        TargetPos(0) = targetPosX
        TargetPos(1) = targetPosY
        TargetPos(2) = RecipeEdit.Node(sys.StageNo)(mNode).ConveyorPos(conveyorNo).LaserData(0).LaserPositionZ
        TargetPos(3) = 0
        TargetPos(4) = 0
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        'ucJoyStick1.RefreshPosition()
        'btnGoFindHeightPos.Enabled = True
        '20170602按鍵保護
        Btn_Control(True)
    End Sub

    Private Sub frmGoArrayPosition_Shown_1(sender As Object, e As EventArgs) Handles MyBase.Shown
        For index = 0 To mArrayLevelCount
            Dim ucArrayControls As ucSelectArrayGoPosition = New ucSelectArrayGoPosition
            ucArrayControls._LevelNo = index
            ucArrayControls.cmbXa.Items.Clear()
            ucArrayControls.cmbYa.Items.Clear()

            If RecipeEdit.Node(mStageNo)(mNode).Array.Count > 0 Then
                Select Case RecipeEdit.Node(mStageNo)(mNode).Array(index).LevelType
                    Case eLevelType.Array
                        For i As Integer = 0 To RecipeEdit.Node(mStageNo)(mNode).Array(index).Array.CountX - 1
                            ucArrayControls.cmbXa.Items.Add(i.ToString("000"))
                        Next
                        For i As Integer = 0 To RecipeEdit.Node(mStageNo)(mNode).Array(index).Array.CountY - 1
                            ucArrayControls.cmbYa.Items.Add(i.ToString("000"))
                        Next
                    Case eLevelType.NoneArray
                        For i As Integer = 0 To RecipeEdit.Node(mStageNo)(mNode).Array(index).NonArray.Count - 1
                            ucArrayControls.cmbXa.Items.Add(i.ToString("000"))
                        Next
                        ucArrayControls.cmbYa.Items.Add(0.ToString("000"))
                End Select

            End If

            ucArrayControls.Name = "ucArrayControls" & index
            ucArrayControls.Top = index * (ucArrayControls.Height + 10)
            UpdateComboxB()
            If cboB.Items.Count <> 0 Then
                cboB.SelectedIndex = 0
            End If
            Me.Controls.Add(ucArrayControls)
        Next
    End Sub
    Public Sub UpdateComboxB()
        Dim iSelectIndexTemp As Integer = cboB.SelectedIndex
        cboB.Items.Clear()
        'gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).LoadCalibrationValve(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (sys.StageNo + 1).ToString & ".ini")
        For i As Integer = 0 To gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).DicLaserTiltValve(mAbsValveNo).Keys.Count - 1   '對每個角度Key, 加入清單選項
            cboB.Items.Add(gSSystemParameter.Pos.LaserTiltValveCalbration(sys.StageNo).DicLaserTiltValve(mAbsValveNo).Keys(i))
        Next
        cboB.SelectedIndex = iSelectIndexTemp
    End Sub
    Private Sub btnGoDispenseBasePos_Click(sender As Object, e As EventArgs) Handles btnGoDispenseBasePos.Click
        gSyslog.Save("[frmRecipe02]" & vbTab & "[btnGoDispenseBasePos]" & vbTab & "Click")
        'btnGoDispenseBasePos.Enabled = False
        '20170602按鍵保護
        Btn_Control(False)
        Select Case gSYS(eSys.OverAll).Act(eAct.Home).RunStatus
            Case enmRunStatus.Finish
            Case enmRunStatus.Alarm
                gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)
                BtnHomeFirstBehavior(btnGoCCDAlignPos)
                'btnGoDispenseBasePos.Enabled = True
                '20170602按鍵保護
                Btn_Control(True)
                Exit Sub
            Case enmRunStatus.Running
                gEqpMsg.AddHistoryAlarm("Warn_3000006", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000006), eMessageLevel.Warning)
                BtnHomeFirstBehavior(btnGoCCDAlignPos)
                'btnGoDispenseBasePos.Enabled = True
                '20170602按鍵保護
                Btn_Control(True)
                Exit Sub
                'Case enmRunStatus.Pause
            Case enmRunStatus.None
                gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmRecipe02", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)
                BtnHomeFirstBehavior(btnGoCCDAlignPos)
                'btnGoDispenseBasePos.Enabled = True
                '20170602按鍵保護
                Btn_Control(True)
                Exit Sub
        End Select

        If RecipeEdit Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011), "Warn_3000011", eMessageLevel.Warning)
            'btnGoDispenseBasePos.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If

        If RecipeEdit.strFileName = "" Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011), "Warn_3000011", eMessageLevel.Warning)
            'btnGoDispenseBasePos.Enabled = True
            '20170602按鍵保護
            Btn_Control(True)
            Exit Sub
        End If
        'Await Task.Run(Sub()
        '                   If gCMotion.GetPositionValue(sys.AxisZ) > gSSystemParameter.Pos.SafePosZ Then
        '                       gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos)
        '                       Do
        '                           System.Threading.Thread.Sleep(1)
        '                           If gDICollection.GetState(enmDI.EMO, True) = True Then
        '                               '20170602按鍵保護
        '                               Btn_Control(True)
        '                               Exit Sub
        '                           End If
        '                       Loop Until gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed
        '                   End If
        '               End Sub)
        gCMotion.SetVelAccDec(sys.AxisX)
        gCMotion.SetVelAccDec(sys.AxisY)

        'Dim intXf As Integer, intYf As Integer


        'If cmbXa.SelectedIndex < 0 Then
        '    intXf = 0
        'Else
        '    intXf = cmbXa.SelectedIndex
        'End If
        'If cmbYa.SelectedIndex < 0 Then
        '    intYf = 0
        'Else
        '    intYf = cmbYa.SelectedIndex
        'End If
        Dim targetPosX As Decimal
        Dim targetPosY As Decimal

        Dim idx As New sLevelIndexCollection
        Dim uctemp As ucSelectArrayGoPosition = New ucSelectArrayGoPosition
        For index = 0 To Me.Controls.Count - 1
            If Me.Controls(index) IsNot Nothing Then
                If Object.ReferenceEquals(Me.Controls(index).GetType, uctemp.GetType) Then
                    If DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbXa.SelectedIndex < 0 Then
                        DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbXa.SelectedIndex = 0
                    End If
                    If DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbYa.SelectedIndex < 0 Then
                        DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbYa.SelectedIndex = 0
                    End If
                    'MessageBox.Show(Me.Controls(index).Name)

                    If DirectCast(Me.Controls(index), ucSelectArrayGoPosition)._LevelNo <= mArrayLevelCount Then
                        If DirectCast(Me.Controls(index), ucSelectArrayGoPosition)._LevelNo = 0 Then
                            idx.Xf = DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbXa.SelectedIndex
                            idx.Yf = DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbYa.SelectedIndex
                        Else
                            idx.Xf = idx.Xf + DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbXa.SelectedIndex * mMultiArrayAdapter.GetLevelCountX(DirectCast(Me.Controls(index), ucSelectArrayGoPosition)._LevelNo - 1)
                            idx.Yf = idx.Yf + DirectCast(Me.Controls(index), ucSelectArrayGoPosition).cmbYa.SelectedIndex * mMultiArrayAdapter.GetLevelCountY(DirectCast(Me.Controls(index), ucSelectArrayGoPosition)._LevelNo - 1)
                        End If
                    Else
                    End If
                End If
            End If
        Next

        'index.Xf = intXf
        'index.Yf = intYf
        idx.NodeName = mNode
        GetCCDScanPos(sys, idx, targetPosX, targetPosY, RecipeEdit.Node(sys.StageNo)(gfrmRecipe04.SelectedNodePath).AlignType, mMultiArrayAdapter)
        'jimmy 20161006
        'Dim angle As Decimal = cboB.SelectedItem '
        If sys.AxisB = -1 Then
            targetPosX = targetPosX - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, 0)
            targetPosY = targetPosY - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, 0)
        Else
            targetPosX = targetPosX - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetX(sys.SelectValve, Val(cboB.SelectedItem))
            targetPosY = targetPosY - gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDTiltValveOffsetY(sys.SelectValve, Val(cboB.SelectedItem))
        End If
        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC

        TargetPos(0) = targetPosX
        TargetPos(1) = targetPosY
        TargetPos(2) = gSSystemParameter.Pos.ZUpPos
        TargetPos(3) = 0
        TargetPos(4) = 0
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        ' ucJoyStick1.RefreshPosition()
        'btnGoDispenseBasePos.Enabled = True
        '20170602按鍵保護
        Btn_Control(True)
    End Sub

    Private Sub frmGoArrayPosition_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    ''' <summary>取得陣列型次層參考位置</summary>
    ''' <param name="refposX"></param>
    ''' <param name="refPosY"></param>
    ''' <param name="refPosT"></param>
    ''' <param name="pitchX"></param>
    ''' <param name="pitchY"></param>
    ''' <param name="iX">索引X</param>
    ''' <param name="iY">索引Y</param>
    ''' <param name="posX">次層參考位置X</param>
    ''' <param name="posY">次層參考位置Y</param>
    ''' <remarks></remarks>
    Public Sub GetLevelArrayPos(ByVal refposX As Decimal,
                              ByVal refPosY As Decimal,
                              ByVal refPosT As Decimal,
                              ByVal pitchX As Decimal,
                              ByVal pitchY As Decimal,
                              ByVal iX As Integer,
                              ByVal iY As Integer,
                              ByRef posX As Decimal,
                              ByRef posY As Decimal,
                              ByVal mMultiArrayAdapter As CMultiArrayAdapter)
        'Dim rad As Decimal = refPosT * Math.PI / 180
        'posX = refposX + pitchX * iX * Convert.ToDecimal(Math.Cos(rad)) - pitchY * iY * Convert.ToDecimal(Math.Sin(rad))
        'posY = refPosY + pitchX * iX * Convert.ToDecimal(Math.Sin(rad)) + pitchY * iY * Convert.ToDecimal(Math.Cos(rad))
        posX = refposX + mMultiArrayAdapter.GetMemoryPosX(iX, iY)
        posY = refPosY + mMultiArrayAdapter.GetMemoryPosY(iX, iY)
    End Sub


    ''' <summary>輸入索引,取得相應拍照位置
    ''' </summary>
    ''' <param name="posX">在索引XY的CCD拍照位置X</param>
    ''' <param name="posY">在索引XY的CCD拍照位置Y</param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function GetCCDScanPos(ByVal sys As sSysParam,
                                   ByVal index As sLevelIndexCollection,
                                   ByRef posX As Decimal,
                                   ByRef posY As Decimal,
                                   ByVal alignIndex As enmAlignType,
                                   ByVal MultiArrayAdapter As CMultiArrayAdapter) As Boolean
        If sys Is Nothing Then
            Return False
        End If
        If sys.StageNo < 0 Then '編號索引低於上限
            Return False
        End If
        'If sys.StageNo >= gStageMap.Count Then '編號索引超過上限
        '    Return False
        'End If

        'If gStageMap(sys.StageNo).Node.Count = 0 Then '沒有節點
        '    Return False
        'End If

        If index.NodeName Is Nothing Then
            Return False
        End If
        'If Not gStageMap(sys.StageNo).Node.ContainsKey(index.NodeID) Then '節點不含名稱
        '    Return False
        'End If

        'If gStageMap(sys.StageNo).Node(index.NodeID).PatternName = "" Then
        '    Return False
        'End If

        Dim RefPosX As Decimal '參考點
        Dim RefPosY As Decimal '參考點
        With RecipeEdit.Node(sys.StageNo)(index.NodeName)
            'With gStageMap(sys.StageNo).Node(index.NodeID)
            Select Case alignIndex
                Case enmAlignType.DevicePos1 '元件第一參考位置
                    RefPosX = .ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignPosX
                    RefPosY = .ConveyorPos(sys.ConveyorNo).AlignmentData(0).AlignPosY
                Case enmAlignType.DevicePos2 '元件第二參考位置
                    RefPosX = .ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignPosX
                    RefPosY = .ConveyorPos(sys.ConveyorNo).AlignmentData(1).AlignPosY
            End Select
        End With
        With RecipeEdit.Node(sys.StageNo)(index.NodeName) '使用目前所在節點的名稱取得陣列設定資料
            Call GetLevelArrayPos(RefPosX,
                                  RefPosY,
                                  .Array(0).Array.Theta,
                                  .Array(0).Array.PitchX,
                                  .Array(0).Array.PitchY,
                                  index.Xf, index.Yf, posX, posY,
                                  MultiArrayAdapter)
            '取得陣列計算位置 'TODO: Soni 還需要修改
        End With

        Return True

    End Function

    Public Function GetLaserPos(ByVal sys As sSysParam, ByVal index As sLevelIndexCollection, ByRef posX As Decimal, ByRef posY As Decimal,
                                 ByVal mMultiArrayAdapter As CMultiArrayAdapter) As Boolean
        If sys.StageNo < 0 Then
            Return False
        End If
        If sys.StageNo >= RecipeEdit.StageNodeID.Count Then
            Return False
        End If
        If index.NodeName = "" Then
            Return False
        End If
        If Not RecipeEdit.Node(sys.StageNo).ContainsKey(index.NodeName) Then
            Return False
        End If
        If sys.CCDNo < 0 Then
            Return False
        End If
        'jimmy 20161011
        '不知角度 先塞0度
        ' If sys.CCDNo >= gSSystemParameter.Pos.CCDValveCalibration.Count Then
        If sys.CCDNo >= gSSystemParameter.Pos.CCDTiltVavleCalbration.Count Then
            Return False
        End If

        With RecipeEdit.Node(sys.StageNo)(index.NodeName)
            Call GetLevelArrayPos(.ConveyorPos(sys.ConveyorNo).LaserData(0).LaserPositionX,
                                  .ConveyorPos(sys.ConveyorNo).LaserData(0).LaserPositionY,
                                  .Array(0).Array.Theta, .Array(0).Array.PitchX, .Array(0).Array.PitchY, index.Xf, index.Yf, posX, posY, mMultiArrayAdapter) 'TODO: Soni 還需要修改
        End With

        'posX = posX - gSSystemParameter.Pos.gCCDValveCalibration(sys.CCDNo).dblCCDToSnOffsetX
        'posY = posY - gSSystemParameter.Pos.gCCDValveCalibration(sys.CCDNo).dblCCDToSnOffsetY

        'Debug.Print("CCD Snr校正:" & gSSystemParameter.Pos.gCCDValveCalibration(sys.CCDNo).dblCCDToSnOffsetX)
        'Debug.Print("CCD Snr校正:" & gSSystemParameter.Pos.gCCDValveCalibration(sys.CCDNo).dblCCDToSnOffsetY)

        Return True
    End Function


End Class
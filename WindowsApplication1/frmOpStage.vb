﻿Imports ProjectCore
Imports ProjectIO
Imports ProjectRecipe
Imports ProjectMotion

Public Class frmOpStage
    Dim mSysStageNo As Integer = eSys.DispStage1
    Dim mSubStageNo As Integer = eSys.SubDisp1
    '20161129
    ''' <summary>對外</summary>
    ''' <remarks></remarks>
    Public sys As sSysParam

    ''' <summary>按鍵依序排列</summary>
    ''' <param name="btnList"></param>
    ''' <remarks></remarks>
    Sub AppendButton(ByRef btnList As List(Of Button))
        Dim basicPosX As Integer
        Dim basicPosY As Integer
        Dim height As Integer
        If btnList.count > 0 Then
            basicPosX = btnList(0).Location.X
            basicPosY = btnList(0).Location.Y
            height = btnList(0).Height
        End If
        Dim visibleBtnId As Integer = 0
        For i As Integer = 0 To btnList.Count - 1
            If btnList(i).Visible Then
                btnList(i).Location = New Point(basicPosX, basicPosY + visibleBtnId * height)
                visibleBtnId += 1
            End If
        Next

    End Sub

    '20161129
    Public Sub New(ByVal sysStageNo As Integer, ByVal sysSys As sSysParam)
        InitializeComponent()
        mSysStageNo = sysStageNo
        Select Case sysStageNo
            Case eSys.DispStage1, eSys.SubDisp1
                mSubStageNo = eSys.SubDisp1
                btnDispenserAutoValveCalibration1.Text = "Valve1 Calib."
            Case eSys.DispStage2, eSys.SubDisp2
                mSubStageNo = eSys.SubDisp2
                btnDispenserAutoValveCalibration1.Text = "Valve2 Calib."
            Case eSys.DispStage3, eSys.SubDisp3
                mSubStageNo = eSys.SubDisp3
                btnDispenserAutoValveCalibration1.Text = "Valve3 Calib."
            Case eSys.DispStage4, eSys.SubDisp4
                mSubStageNo = eSys.SubDisp4
                btnDispenserAutoValveCalibration1.Text = "Valve4 Calib."
        End Select
        sys = sysSys

    End Sub

    ''' <summary>表單是否可關閉</summary>
    ''' <remarks></remarks>
    Dim mIsCanClose As Boolean = True

    Private Sub frmOpStage_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Timer1.Enabled = False
    End Sub

    ''' <summary>表單關閉保護</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmOpStage_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not mIsCanClose Then '不能關閉則取消
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' 開啟介面時切換顯示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmStageOp_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Select Case gSYS(mSysStageNo).Act(eAct.ChangeGlue).RunStatus
        '    Case enmRunStatus.Finish, enmRunStatus.None, enmRunStatus.Alarm
        '        btnChangeGluePos.Enabled = True
        '    Case Else
        '        btnChangeGluePos.Enabled = False
        'End Select
        'Select Case gSYS(mSysStageNo).Act(eAct.ClearGlue).RunStatus
        '    Case enmRunStatus.Finish, enmRunStatus.None, enmRunStatus.Alarm
        '        btnClearGlue1.Enabled = True
        '    Case Else
        '        btnClearGlue1.Enabled = False
        'End Select
        'Select Case gSYS(mSysStageNo).Act(eAct.WeightUnit).RunStatus
        '    Case enmRunStatus.Finish, enmRunStatus.None, enmRunStatus.Alarm
        '        btnValve1Weight.Enabled = True
        '    Case Else
        '        btnValve1Weight.Enabled = False
        'End Select
        'Select Case gSYS(mSysStageNo).Act(eAct.DispenserAutoSearch).RunStatus
        '    Case enmRunStatus.Finish, enmRunStatus.None, enmRunStatus.Alarm
        '        btnDispenserAutoSearch1.Enabled = True
        '    Case Else
        '        btnDispenserAutoSearch1.Enabled = False
        'End Select
        'Select Case gSYS(mSysStageNo).Act(eAct.Purge).RunStatus
        '    Case enmRunStatus.Finish, enmRunStatus.None, enmRunStatus.Alarm
        '        btnPurge1.Enabled = True
        '    Case Else
        '        btnPurge1.Enabled = False
        'End Select
        Dim btnList As New List(Of Button)
        btnList.Add(btnChangeGluePos)
        btnList.Add(btnClearGlue1)
        btnList.Add(btnValve1Weight)
        btnList.Add(btnDispenserAutoSearch1)
        btnList.Add(btnPurge1)
        btnList.Add(btnDispenserAutoValveCalibration1)
        '20170920
        btnList.Add(btnPm1)
        AppendButton(btnList)
        cmbValve.Items.Clear()
        'jimmy 20170822
        Select Case gSSystemParameter.StageUseValveCount
            Case eMechanismModule.OneValveOneStage
                cmbValve.Items.Add("Valve1")
            Case eMechanismModule.TwoValveOneStage
                cmbValve.Items.Add("Valve1")
                cmbValve.Items.Add("Valve2")
            Case Else
                cmbValve.Items.Add("Valve1")
        End Select
        '20170824 
        cmbValve.SelectedIndex = 0
        mIsCanClose = True
        Timer1_Tick(sender, e) '先更新一次介面
        Timer1.Enabled = True
    End Sub

    ''' <summary>
    ''' 換膠
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnChangeGluePos_Click(sender As Object, e As EventArgs) Handles btnChangeGluePos.Click
        If btnChangeGluePos.Enabled = False Then '防連按
            Exit Sub
        End If
        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnChangeGluePos]" & vbTab & "Click")

        '[說明]:回Home完成才能執行 沒個別初始Check
        If gSYS(mSysStageNo).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            '請先復歸
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOpStage btnChangeGluePos", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
            Exit Sub
        End If

        '[說明]:只有非Running的時候才能動
        If gSYS(mSubStageNo).Act(eAct.AutoRun).RunStatus = enmRunStatus.Running Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            gEqpMsg.AddHistoryAlarm("Warn_3000007", "frmOpStage btnChangeGluePos", , gMsgHandler.GetMessage(Warn_3000007), eMessageLevel.Warning)
            Exit Sub
        End If

        '[說明]:enmRunStatus.Nono, enmRunStatus.Pause, enmRunStatus.Alarm, enmRunStatus.Running
        '[說明]:判斷是否已經在做ChangeGlue
        If gSYS(mSubStageNo).RunStatus = enmRunStatus.Running Then '運行中暫停
            If gSYS(mSubStageNo).ExternalPause = False Then
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
                gEqpMsg.AddHistoryAlarm("Warn_3000007", "frmOpStage btnChangeGluePos", , gMsgHandler.GetMessage(Warn_3000007), eMessageLevel.Warning)
                Exit Sub
            End If
        End If



        If btnChangeGluePos.Enabled = False Then
            Exit Sub
        End If
        btnChangeGluePos.Enabled = False
        Dim strBuffer As String
        strBuffer = "[Jetting Controller]-->[Menu]-->[Purge]-->[Valve]-->[Stop]" & vbCrLf

        strBuffer += "Change Pos:" & vbCrLf
        strBuffer += gSSystemParameter.Pos.ChangePotCalibration(gSYS(mSysStageNo).StageNo).PosX(gSYS(mSysStageNo).SelectValve) & vbCrLf
        strBuffer += gSSystemParameter.Pos.ChangePotCalibration(gSYS(mSysStageNo).StageNo).PosY(gSYS(mSysStageNo).SelectValve) & vbCrLf
        strBuffer += gSSystemParameter.Pos.ChangePotCalibration(gSYS(mSysStageNo).StageNo).PosZ(gSYS(mSysStageNo).SelectValve) & vbCrLf
        mIsCanClose = False

        If MsgBox(strBuffer, MsgBoxStyle.YesNo, "閥座拆裝時，請參照下述步驟調整控制器") = MsgBoxResult.Yes Then
            '--- 移動前門鎖上鎖 ---
            Select Case mSubStageNo
                Case eSys.SubDisp1, eSys.SubDisp2
                    gDOCollection.SetState(enmDO.DoorLock, True) 'A機門鎖
                Case eSys.SubDisp3, eSys.SubDisp4
                    gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖
            End Select
            '--- 移動前門鎖上鎖 ---


            '[說明]:設定動作Flg & 按鍵安全機制 & Command
            gSYS(mSubStageNo).RunStatus = enmRunStatus.None

            If cmbValve.SelectedIndex = -1 Then

                btnChangeGluePos.Enabled = True
                mIsCanClose = True
                MessageBox.Show("Please select the vavle ")
                Exit Sub
            Else
                Select Case cmbValve.SelectedIndex
                    Case 0
                        gActionChangeGlue._ManualSelectValve = eValveWorkMode.Valve1
                    Case 1
                        gActionChangeGlue._ManualSelectValve = eValveWorkMode.Valve2
                End Select
            End If
            gActionChangeGlue.SetTempValveMode(sys)
            gActionChangeGlue._bManualChangeGlueAction = True
            gSYS(mSubStageNo).Command = eSysCommand.ChangeGlue

        End If

        btnChangeGluePos.Enabled = True
        mIsCanClose = True
    End Sub



    ''' <summary>
    ''' 清膠
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClearGlue1_Click(sender As Object, e As EventArgs) Handles btnClearGlue1.Click
        If btnClearGlue1.Enabled = False Then
            Exit Sub
        End If
        mIsCanClose = False
        btnClearGlue1.Enabled = False
        ClearGlue(mSubStageNo)
        mIsCanClose = True
        btnClearGlue1.Enabled = True
    End Sub
    ''' <summary>
    ''' 清膠
    ''' </summary>
    ''' <param name="DispStage"></param>
    ''' <remarks></remarks>
    Public Sub ClearGlue(ByRef DispStage As Integer)
        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnClearGlue]" & vbTab & "Click")

        '[說明]:回Home完成才能執行 沒個別初始Check
        If gSYS(DispStage).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            '請先復歸
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOperation btnClearGlue", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)
            Exit Sub
        End If

        If gSYS(DispStage).Act(eAct.AutoRun).RunStatus = enmRunStatus.Running Then '[說明]:只有非Running的時候才能動
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            Exit Sub
        End If

        '[說明]:enmRunStatus.Nono, enmRunStatus.Pause, enmRunStatus.Alarm, enmRunStatus.Running
        '[說明]:判斷是否已經在做ClearGlue
        If gSYS(DispStage).Act(eAct.ClearGlue).RunStatus = enmRunStatus.Running Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            Exit Sub
        End If

        '[說明]:設定動作Flg & 按鍵安全機制 & Command
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                Select Case DispStage '多Stage機台需先閃安全位置
                    Case eSys.DispStage1, eSys.DispStage2
                        gDOCollection.SetState(enmDO.DoorLock, True) 'A機門鎖
                        MachineSafeMove(0)
                    Case eSys.DispStage3, eSys.DispStage4
                        gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖
                        MachineSafeMove(1)
                End Select

            Case Else
                gDOCollection.SetState(enmDO.DoorLock, True)

        End Select

        gSYS(DispStage).Command = eSysCommand.ClearGlue
        'gblnClearGlueComeBack = True

    End Sub

    ''' <summary>單邊機台到安全位置</summary>
    ''' <param name="machineNo"></param>
    ''' <remarks></remarks>
    Sub MachineSafeMove(ByVal machineNo As Integer)
        Select Case machineNo
            Case 0
                gCMotion.SetVelAccDec(gSYS(eSys.DispStage1).AxisX)
                gCMotion.SetVelAccDec(gSYS(eSys.DispStage1).AxisY)
                gCMotion.SetVelAccDec(gSYS(eSys.DispStage1).AxisZ)
                gCMotion.SetVelAccDec(gSYS(eSys.DispStage2).AxisX)
                gCMotion.SetVelAccDec(gSYS(eSys.DispStage2).AxisY)
                gCMotion.SetVelAccDec(gSYS(eSys.DispStage2).AxisZ)
                'Z軸到安全高度
                If DoLoopAbsMove2(gSYS(eSys.DispStage1).AxisZ, gSSystemParameter.Pos.SafeRegion(0).PosZ(gSYS(eSys.DispStage1).SelectValve), gSYS(eSys.DispStage2).AxisZ, gSSystemParameter.Pos.SafeRegion(1).PosZ(gSYS(eSys.DispStage2).SelectValve)) = False Then
                    Exit Sub
                End If
                'X軸拉開
                If DoLoopAbsMove2(gSYS(eSys.DispStage1).AxisX, gSSystemParameter.Pos.SafeRegion(0).PosX(gSYS(eSys.DispStage1).SelectValve), gSYS(eSys.DispStage2).AxisX, gSSystemParameter.Pos.SafeRegion(1).PosX(gSYS(eSys.DispStage2).SelectValve)) = False Then
                    Exit Sub
                End If
                'Y軸拉開
                If DoLoopAbsMove2(gSYS(eSys.DispStage1).AxisY, gSSystemParameter.Pos.SafeRegion(0).PosY(gSYS(eSys.DispStage1).SelectValve), gSYS(eSys.DispStage2).AxisY, gSSystemParameter.Pos.SafeRegion(1).PosY(gSYS(eSys.DispStage2).SelectValve)) = False Then
                    Exit Sub
                End If

            Case 1
                gCMotion.SetVelAccDec(gSYS(eSys.DispStage3).AxisX)
                gCMotion.SetVelAccDec(gSYS(eSys.DispStage3).AxisY)
                gCMotion.SetVelAccDec(gSYS(eSys.DispStage3).AxisZ)
                gCMotion.SetVelAccDec(gSYS(eSys.DispStage4).AxisX)
                gCMotion.SetVelAccDec(gSYS(eSys.DispStage4).AxisY)
                gCMotion.SetVelAccDec(gSYS(eSys.DispStage4).AxisZ)
                'Z軸到安全高度
                If DoLoopAbsMove2(gSYS(eSys.DispStage3).AxisZ, gSSystemParameter.Pos.SafeRegion(2).PosZ(gSYS(eSys.DispStage3).SelectValve), gSYS(eSys.DispStage4).AxisZ, gSSystemParameter.Pos.SafeRegion(3).PosZ(gSYS(eSys.DispStage4).SelectValve)) = False Then
                    Exit Sub
                End If
                'X軸拉開
                If DoLoopAbsMove2(gSYS(eSys.DispStage3).AxisX, gSSystemParameter.Pos.SafeRegion(2).PosX(gSYS(eSys.DispStage3).SelectValve), gSYS(eSys.DispStage4).AxisX, gSSystemParameter.Pos.SafeRegion(3).PosX(gSYS(eSys.DispStage4).SelectValve)) = False Then
                    Exit Sub
                End If
                'Y軸拉開
                If DoLoopAbsMove2(gSYS(eSys.DispStage3).AxisY, gSSystemParameter.Pos.SafeRegion(2).PosY(gSYS(eSys.DispStage3).SelectValve), gSYS(eSys.DispStage4).AxisY, gSSystemParameter.Pos.SafeRegion(3).PosY(gSYS(eSys.DispStage4).SelectValve)) = False Then
                    Exit Sub
                End If

        End Select
    End Sub

    ''' <summary>
    ''' 單軸移動
    ''' </summary>
    ''' <param name="enmAxis"></param>
    ''' <param name="pos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DoLoopAbsMove(ByVal enmAxis As Integer, ByVal pos As Decimal) As Boolean
        If gCMotion.AbsMove(enmAxis, pos) <> CommandStatus.Sucessed Then
            MsgBox(gCMotion.AxisParameter(enmAxis).AxisName & " Command Failed! @" & pos)
            Return False
        End If
        Dim stopWatch As New Stopwatch
        stopWatch.Restart()
        Do
            If gCMotion.MotionDone(enmAxis) = CommandStatus.Sucessed Then
                Exit Do
            ElseIf stopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut4 Then
                MsgBox(gCMotion.AxisParameter(enmAxis).AxisName & " Move Time Out!")
                Return False
            End If
        Loop
        Return True
    End Function

    ''' <summary>雙軸點對點移動</summary>
    ''' <param name="enmAxis"></param>
    ''' <param name="pos"></param>
    ''' <param name="enmAxis2"></param>
    ''' <param name="pos2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DoLoopAbsMove2(ByVal enmAxis As Integer, ByVal pos As Decimal, ByVal enmAxis2 As Integer, ByVal pos2 As Decimal) As Boolean
        If gCMotion.AbsMove(enmAxis, pos) <> CommandStatus.Sucessed Then
            MsgBox(gCMotion.AxisParameter(enmAxis).AxisName & " Command Failed! @" & pos)
            Return False
        End If
        If gCMotion.AbsMove(enmAxis2, pos2) <> CommandStatus.Sucessed Then
            MsgBox(gCMotion.AxisParameter(enmAxis2).AxisName & " Command Failed! @" & pos)
            Return False
        End If
        Dim stopWatch As New Stopwatch
        stopWatch.Restart()

        Dim stopWatch2 As New Stopwatch
        stopWatch2.Restart()
        Do
            If gCMotion.MotionDone(enmAxis) = CommandStatus.Sucessed Then
                Exit Do
            ElseIf stopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut4 Then
                MsgBox(gCMotion.AxisParameter(enmAxis).AxisName & " Move Time Out!")
                Return False
            End If
        Loop
        Return True
        Do
            If gCMotion.MotionDone(enmAxis2) = CommandStatus.Sucessed Then
                Exit Do
            ElseIf stopWatch2.ElapsedMilliseconds > gSSystemParameter.TimeOut4 Then
                MsgBox(gCMotion.AxisParameter(enmAxis2).AxisName & " Move Time Out!")
                Return False
            End If
        Loop
        Return True
    End Function

    ''' <summary>
    ''' 天平秤重
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnValve1Weight_Click(sender As Object, e As EventArgs) Handles btnValve1Weight.Click
        If btnValve1Weight.Enabled = False Then '防連按
            Exit Sub
        End If
        btnValve1Weight.Enabled = False
        mIsCanClose = False
        AutoWeight(mSubStageNo)
        mIsCanClose = True
        btnValve1Weight.Enabled = True
    End Sub

    ''' <summary>
    ''' 天平秤重
    ''' </summary>
    ''' <param name="DispStage"></param>
    ''' <remarks></remarks>
    Public Sub AutoWeight(ByRef DispStage As Integer)
        gSyslog.Save("[frmWeight]" & vbTab & "[btnWeighing]" & vbTab & "Click")
        'Dim str As String

        '[說明]:回Home完成才能執行
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Or gSYS(eSys.MachineB).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOperation btnStart", , gMsgHandler.GetMessage(Warn_3000005)) 'System Home 尚未完成!!
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Exit Sub
                End If

            Case Else
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOperation btnStart", , gMsgHandler.GetMessage(Warn_3000005)) 'System Home 尚未完成!!
                    MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Exit Sub
                End If

        End Select


        '[說明]:判斷有無開啟Recipe
        If gCRecipe.strName = "" Then
            '找不到 Recipe 檔案!!
            gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmOperation btnStart", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000011), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '[說明]:執行狀態下檢查
        If gSYS(DispStage).Act(eAct.WeightUnit).RunStatus = enmRunStatus.Running Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000007), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        Dim msg As String = "Weight Glue?"

        msg = "Weight Glue at Pos(" & gSSystemParameter.Pos.WeightCalibration(gSYS(DispStage).StageNo).ValvePosX(gSYS(DispStage).SelectValve) & "," &
                                     gSSystemParameter.Pos.WeightCalibration(gSYS(DispStage).StageNo).ValvePosY(gSYS(DispStage).SelectValve) & "," &
                                     gSSystemParameter.Pos.WeightCalibration(gSYS(DispStage).StageNo).ValvePosZ(gSYS(DispStage).SelectValve) & ")?"

        If MsgBox(msg, MsgBoxStyle.OkCancel, "Weigh Glue") <> MsgBoxResult.Ok Then
            gSyslog.Save("[frmWeight]" & vbTab & "[btnWeighing]" & vbTab & "Cancel")
            Exit Sub
        End If


        '[說明]:設定動作Flg & 按鍵安全機制 & Command
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                Select Case DispStage '多Stage機台需先閃安全位置
                    Case eSys.DispStage1, eSys.DispStage2
                        gDOCollection.SetState(enmDO.DoorLock, True) 'A機門鎖
                        MachineSafeMove(0)
                    Case eSys.DispStage3, eSys.DispStage4
                        gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖
                        MachineSafeMove(1)
                End Select

            Case Else
                gDOCollection.SetState(enmDO.DoorLock, True)

        End Select
        gSYS(DispStage).Act(eAct.WeightUnit).RunStatus = enmRunStatus.Running

        '20160527 測試用 By Jeff

        If cmbValve.SelectedIndex = -1 Then

            btnChangeGluePos.Enabled = True
            mIsCanClose = True
            MessageBox.Show("Please select the vavle ")
            Exit Sub
        Else
            Select Case cmbValve.SelectedIndex
                Case 0
                    gWeight._ManualSelectValve = eValveWorkMode.Valve1
                Case 1
                    gWeight._ManualSelectValve = eValveWorkMode.Valve2
            End Select

        End If
        gWeight._bManualWeightAction = True
        gWeight.SetTempValveMode(sys)
        gSYS(mSubStageNo).Command = eSysCommand.WeightUnit
        gWeight.gblnWeighingComeBack = True
        gWeight.gblnUpdateWeighing = True
    End Sub

    ''' <summary>
    ''' 測高
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDispenserAutoSearch1_Click(sender As Object, e As EventArgs) Handles btnDispenserAutoSearch1.Click
        If btnDispenserAutoSearch1.Enabled = False Then '防連按
            Exit Sub
        End If
        btnDispenserAutoSearch1.Enabled = False
        mIsCanClose = False
        DispenserAutoSearch(mSubStageNo)
        mIsCanClose = True
        btnDispenserAutoSearch1.Enabled = True
    End Sub

    ''' <summary>
    ''' 測高
    ''' </summary>
    ''' <param name="DispStage"></param>
    ''' <remarks></remarks>
    Public Sub DispenserAutoSearch(ByRef DispStage As Integer)

        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnDispenserNo2AutoSearch]" & vbTab & "Click")

        '[說明]:回Home完成才能執行 沒個別初始Check
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005))
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOperation btnChangeGluePos", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gSYS(mSubStageNo).RunStatus = enmRunStatus.Running Then '[說明]:只有非Running的時候才能動
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000007), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '[說明]:enmRunStatus.Nono, enmRunStatus.Pause, enmRunStatus.Alarm, enmRunStatus.Running
        '[說明]:判斷是否已經在做DispenserNo2AutoSearch
        If gSYS(DispStage).Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Running Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000007), vbOKOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '[說明]:設定動作Flg & 按鍵安全機制 & Command
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                Select Case DispStage '多Stage機台需先閃安全位置
                    Case eSys.DispStage1, eSys.DispStage2
                        gDOCollection.SetState(enmDO.DoorLock, True) 'A機門鎖
                        MachineSafeMove(0)
                    Case eSys.DispStage3, eSys.DispStage4
                        gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖
                        MachineSafeMove(1)
                End Select

            Case Else
                gDOCollection.SetState(enmDO.DoorLock, True)

        End Select
        If cmbValve.SelectedIndex = -1 Then
            btnChangeGluePos.Enabled = True
            mIsCanClose = True
            MessageBox.Show("Please select the vavle ")
            Exit Sub
        Else
            Select Case cmbValve.SelectedIndex
                Case 0
                    gValveHeightAutoSearchAction._ManualSelectValve = eValveWorkMode.Valve1
                Case 1
                    gValveHeightAutoSearchAction._ManualSelectValve = eValveWorkMode.Valve2
            End Select
        End If
        gValveHeightAutoSearchAction._bManualHeightAutoSearchAction = True
        gValveHeightAutoSearchAction.SetTempValveMode(sys)
        gSYS(mSubStageNo).Command = eSysCommand.DispenserAutoSearch
    End Sub

    ''' <summary>
    ''' 排膠
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPurge1_Click(sender As Object, e As EventArgs) Handles btnPurge1.Click
        If btnPurge1.Enabled = False Then
            Exit Sub
        End If
        btnPurge1.Enabled = False
        mIsCanClose = False
        Purge(mSubStageNo)
        mIsCanClose = True
        btnPurge1.Enabled = True
    End Sub

    ''' <summary>
    ''' 排膠
    ''' </summary>
    ''' <param name="DispStage"></param>
    ''' <remarks></remarks>
    Public Sub Purge(ByRef DispStage As Integer)

        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnPurge]" & vbTab & "Click")

        '[說明]:回Home完成才能執行 沒個別初始Check
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOperation btnChangeGluePos", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
            Exit Sub
        End If

        If gSYS(mSubStageNo).RunStatus = enmRunStatus.Running Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            Exit Sub
        End If

        '[說明]:判斷有無開啟Recipe 20161111
        'If gCRecipe.strName = "" Then
        '    MsgBox(gMsgHandler.GetMessage(Warn_3000011), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmOperation Purge", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
        '    Exit Sub
        'End If

        '[說明]:enmRunStatus.Nono, enmRunStatus.Pause, enmRunStatus.Alarm, enmRunStatus.Running
        '[說明]:判斷是否已經在做Purge
        If gSYS(mSubStageNo).Act(eAct.Purge).RunStatus = enmRunStatus.Running Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            Exit Sub
        End If

        '[說明]:設定動作Flg & 按鍵安全機制 & Command
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                Select Case DispStage '多Stage機台需先閃安全位置
                    Case eSys.DispStage1, eSys.DispStage2
                        gDOCollection.SetState(enmDO.DoorLock, True) 'A機門鎖
                        MachineSafeMove(0)
                    Case eSys.DispStage3, eSys.DispStage4
                        gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖
                        MachineSafeMove(1)
                End Select

            Case Else
                gDOCollection.SetState(enmDO.DoorLock, True)

        End Select
        gSYS(mSubStageNo).RunStatus = enmRunStatus.None

        If cmbValve.SelectedIndex = -1 Then
            btnChangeGluePos.Enabled = True
            mIsCanClose = True
            MessageBox.Show("Please select the vavle ")
            Exit Sub
        Else
            Select Case cmbValve.SelectedIndex
                Case 0
                    gPurgeAction._ManualSelectValve = eValveWorkMode.Valve1
                Case 1
                    gPurgeAction._ManualSelectValve = eValveWorkMode.Valve2
            End Select
        End If
        gPurgeAction._bManualPurgeAction = True
        gPurgeAction.SetTempValveMode(sys)
        gSYS(mSubStageNo).Command = eSysCommand.Purge
        'gblnPurgeComeBack = True
    End Sub

    ''' <summary>
    ''' XY自動校正
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDispenserAutoValveCalibration1_Click(sender As Object, e As EventArgs) Handles btnDispenserAutoValveCalibration1.Click
        If btnDispenserAutoValveCalibration1.Enabled = False Then '防連按
            Exit Sub
        End If
        btnDispenserAutoValveCalibration1.Enabled = False
        mIsCanClose = False
        DispenserAutoValveCalibration(mSysStageNo)
        mIsCanClose = True
        btnDispenserAutoValveCalibration1.Enabled = True
    End Sub

    ' ''' <summary>
    ' ''' XY自動校正
    ' ''' </summary>
    ' ''' <param name="DispStage"></param>
    ' ''' <remarks></remarks>
    'Public Sub DispenserAutoValveCalibration(ByRef DispStage As Integer)

    '    gSyslog.Save("[frmUiViewer]" & vbTab & "[btnDispenserNo1AutoValveCalibration]" & vbTab & "Click")

    '    '[說明]:回Home完成才能執行 沒個別初始Check
    '    If gSYS(DispStage).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
    '        MessageBox.Show("Not Initial!!!")
    '        gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOperation btnChangeGluePos", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
    '        Exit Sub
    '    End If

    '    If gSYS(DispStage).Act(eAct.AutoRun).RunStatus = enmRunStatus.Running Then '[說明]:只有非Running的時候才能動
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
    '        gEqpMsg.AddHistoryAlarm("Warn_3000007", "frmUiViewer btnDispenserNo1AutoValveCalibration", , gMsgHandler.GetMessage(Warn_3000007), eMessageLevel.Warning)
    '        Exit Sub
    '    End If

    '    '[說明]:enmRunStatus.Nono, enmRunStatus.Pause, enmRunStatus.Alarm, enmRunStatus.Running
    '    '[說明]:判斷是否已經在做DispenserNo2AutoSearch
    '    If gSYS(DispStage).Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Running Then
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
    '        gEqpMsg.AddHistoryAlarm("Warn_3000007", "frmUiViewer btnDispenserNo1AutoValveCalibration", , gMsgHandler.GetMessage(Warn_3000007), eMessageLevel.Warning)
    '        Exit Sub
    '    End If

    '    '[說明]:設定動作Flg & 按鍵安全機制 & Command
    '    Select Case gSSystemParameter.enmMachineType
    '        Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V
    '            Select Case DispStage '多Stage機台需先閃安全位置
    '                Case eSys.DispStage1, eSys.DispStage2
    '                    gDOCollection.GetSetState(enmDO.DoorLock) = True 'A機門鎖
    '                    MachineSafeMove(0)
    '                Case eSys.DispStage3, eSys.DispStage4
    '                    gDOCollection.GetSetState(enmDO.DoorLock2) = True 'B機門鎖
    '                    MachineSafeMove(1)
    '            End Select
    '        Case Else '單Stage機台不必
    '    End Select
    '    ' gSYS(DispStage).ValveNo(0) = enmValve.No1     ????
    '    gSYS(mSubStageNo).Command = eSysCommand.CCDValveAutoCalibrationXY

    'End Sub
    Public Sub DispenserAutoValveCalibration(ByRef DispStage As Integer)

        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnDispenserNo1AutoValveCalibration]" & vbTab & "Click")
        Static mTimeoutStopWatch(enmStage.Max) As Stopwatch
        Static mPosB(enmStage.Max) As Decimal

        If IsNothing(mTimeoutStopWatch(sys.StageNo)) Then
            mTimeoutStopWatch(sys.StageNo) = New Stopwatch
        End If


        '[說明]:回Home完成才能執行 沒個別初始Check
        If gSYS(DispStage).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOperation btnChangeGluePos", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
            Exit Sub
        End If

        If gSYS(DispStage).Act(eAct.AutoRun).RunStatus = enmRunStatus.Running Then '[說明]:只有非Running的時候才能動
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            gEqpMsg.AddHistoryAlarm("Warn_3000007", "frmUiViewer btnDispenserNo1AutoValveCalibration", , gMsgHandler.GetMessage(Warn_3000007), eMessageLevel.Warning)
            Exit Sub
        End If

        '[說明]:enmRunStatus.Nono, enmRunStatus.Pause, enmRunStatus.Alarm, enmRunStatus.Running
        '[說明]:判斷是否已經在做DispenserNo2AutoSearch
        If gSYS(DispStage).Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Running Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            gEqpMsg.AddHistoryAlarm("Warn_3000007", "frmUiViewer btnDispenserNo1AutoValveCalibration", , gMsgHandler.GetMessage(Warn_3000007), eMessageLevel.Warning)
            Exit Sub
        End If

        '[說明]:設定動作Flg & 按鍵安全機制 & Command
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                Select Case DispStage '多Stage機台需先閃安全位置
                    Case eSys.DispStage1, eSys.DispStage2
                        gDOCollection.SetState(enmDO.DoorLock, True) 'A機門鎖
                        MachineSafeMove(0)
                    Case eSys.DispStage3, eSys.DispStage4
                        gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖
                        MachineSafeMove(1)
                End Select

            Case Else
                gDOCollection.SetState(enmDO.DoorLock, True)

        End Select
        If cmbValve.SelectedIndex = -1 Then
            btnChangeGluePos.Enabled = True
            mIsCanClose = True
            MessageBox.Show("Please select the vavle ")
            Exit Sub
        Else
            Select Case cmbValve.SelectedIndex
                Case 0
                    gCCDValveAutoCalibXYAction._ManualSelectValve = eValveWorkMode.Valve1
                Case 1
                    gCCDValveAutoCalibXYAction._ManualSelectValve = eValveWorkMode.Valve2
            End Select
        End If
        gCCDValveAutoCalibXYAction.SetTempValveMode(sys)
        gCCDValveAutoCalibXYAction._bManualAutoCalibXY = True
        gSYS(mSubStageNo).Command = eSysCommand.CCDValveAutoCalibrationXY

        ''[說明]:等待一下,命令進入迴圈   
        'mTimeoutStopWatch(sys.StageNo).Restart()
        'Do
        '    If mTimeoutStopWatch(sys.StageNo).ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
        '        Exit Do
        '    End If
        'Loop

        ''[說明]:等待動作完成
        'Do
        '    Application.DoEvents()
        '    If gSYS(mSubStageNo).RunStatus = enmRunStatus.Alarm Then
        '        MsgBox("AutoCalibFailed SubDisp" & (sys.StageNo + 1))
        '        Exit Sub
        '    End If
        'Loop Until gSYS(mSubStageNo).RunStatus = enmRunStatus.Finish


        'With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
        '    If sys.AxisB <> -1 Then
        '        Call GetTiltValue(gCRecipe, sys.StageNo, mPosB(sys.StageNo))
        '        .CCDX(sys.GetValveNo, mPosB(sys.StageNo)) = .CCDX(sys.GetValveNo, mPosB(sys.StageNo)) - .AutoResultX(sys.GetValveNo, mPosB(sys.StageNo))
        '        .CCDY(sys.GetValveNo, mPosB(sys.StageNo)) = .CCDY(sys.GetValveNo, mPosB(sys.StageNo)) - .AutoResultY(sys.GetValveNo, mPosB(sys.StageNo))
        '        MsgBox("Auto Calib OK." & vbCrLf & "OffsetX:" & .AutoResultX(sys.GetValveNo, mPosB(sys.StageNo)) & vbCrLf &
        '               "OffsetY: " & .AutoResultY(sys.GetValveNo, mPosB(sys.StageNo)))
        '    Else
        '        .CCDX(sys.GetValveNo, 0) = .CCDX(sys.GetValveNo, 0) - .AutoResultX(sys.GetValveNo, 0)
        '        .CCDY(sys.GetValveNo, 0) = .CCDY(sys.GetValveNo, 0) - .AutoResultY(sys.GetValveNo, 0)
        '        MsgBox("Auto Calib OK." & vbCrLf & "OffsetX:" & .AutoResultX(sys.GetValveNo, 0) & vbCrLf &
        '               "OffsetY: " & .AutoResultY(sys.GetValveNo, 0))
        '    End If
        'End With

    End Sub



    ''' <summary>
    ''' 介面關閉
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmValveOp_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Close()
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '需要復歸時不能按
            btnChangeGluePos.Enabled = False
            btnClearGlue1.Enabled = False
            btnDispenserAutoSearch1.Enabled = False
            btnDispenserAutoValveCalibration1.Enabled = False
            btnPurge1.Enabled = False
            btnValve1Weight.Enabled = False
            Exit Sub
        End If
        '--- 門鎖保護 ---
        Select Case mSubStageNo
            Case eSys.SubDisp1, eSys.SubDisp2
                If gDICollection.GetState(enmDI.DoorClose, False) = True Then
                    btnChangeGluePos.Enabled = False
                    btnClearGlue1.Enabled = False
                    btnDispenserAutoSearch1.Enabled = False
                    btnDispenserAutoValveCalibration1.Enabled = False
                    btnPurge1.Enabled = False
                    btnValve1Weight.Enabled = False
                    Exit Sub
                End If
            Case eSys.SubDisp3, eSys.SubDisp4
                If gDICollection.GetState(enmDI.DoorClose2, False) = True Then
                    btnChangeGluePos.Enabled = False
                    btnClearGlue1.Enabled = False
                    btnDispenserAutoSearch1.Enabled = False
                    btnDispenserAutoValveCalibration1.Enabled = False
                    btnPurge1.Enabled = False
                    btnValve1Weight.Enabled = False
                    Exit Sub
                End If
        End Select
        '--- 門鎖保護 ---

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                Select Case mSysStageNo
                    Case eSys.DispStage1, eSys.DispStage2
                        If gSYS(eSys.SubDisp1).RunStatus = enmRunStatus.Running Or gSYS(eSys.SubDisp2).RunStatus = enmRunStatus.Running Then '任一邊運行中即全部封鎖
                            'gDOCollection.GetSetState(enmDO.DoorLock) = True '上鎖
                            btnPurge1.Enabled = False
                            btnDispenserAutoValveCalibration1.Enabled = False
                            btnChangeGluePos.Enabled = False
                            btnClearGlue1.Enabled = False
                            btnDispenserAutoSearch1.Enabled = False
                            btnValve1Weight.Enabled = False
                        Else
                            'gDOCollection.GetSetState(enmDO.DoorLock) = False '解鎖
                            btnPurge1.Enabled = True
                            btnChangeGluePos.Enabled = True
                            btnClearGlue1.Enabled = True
                            If gSYS(mSysStageNo).ExecuteCommand = eSysCommand.CCDFix Or gSYS(mSysStageNo).ExecuteCommand = eSysCommand.CCDSCanGlue Then 'Soni + 2016.09.22 暫停在定位功能中不能做自動校正
                                btnDispenserAutoValveCalibration1.Enabled = False
                            Else
                                btnDispenserAutoValveCalibration1.Enabled = True
                            End If
                            If gSYS(mSysStageNo).ExecuteCommand = eSysCommand.LaserReader Then '
                                btnDispenserAutoSearch1.Enabled = False
                            Else
                                btnDispenserAutoSearch1.Enabled = True
                            End If
                            btnValve1Weight.Enabled = True
                        End If

                    Case eSys.DispStage3, eSys.DispStage4
                        If gSYS(eSys.SubDisp3).RunStatus = enmRunStatus.Running Or gSYS(eSys.SubDisp4).RunStatus = enmRunStatus.Running Then '任一邊運行中即全部封鎖
                            'gDOCollection.GetSetState(enmDO.DoorLock2) = True '上鎖
                            btnPurge1.Enabled = False
                            btnDispenserAutoValveCalibration1.Enabled = False
                            btnChangeGluePos.Enabled = False
                            btnClearGlue1.Enabled = False
                            btnDispenserAutoSearch1.Enabled = False
                            btnValve1Weight.Enabled = False
                        Else
                            'gDOCollection.GetSetState(enmDO.DoorLock2) = False '解鎖
                            btnPurge1.Enabled = True
                            btnChangeGluePos.Enabled = True
                            btnClearGlue1.Enabled = True
                            If gSYS(mSysStageNo).ExecuteCommand = eSysCommand.CCDFix Or gSYS(mSysStageNo).ExecuteCommand = eSysCommand.CCDSCanGlue Then 'Soni + 2016.09.22 暫停在定位功能中不能做自動校正
                                btnDispenserAutoValveCalibration1.Enabled = False
                            Else
                                btnDispenserAutoValveCalibration1.Enabled = True
                            End If
                            If gSYS(mSysStageNo).ExecuteCommand = eSysCommand.LaserReader Then
                                btnDispenserAutoSearch1.Enabled = False
                            Else
                                btnDispenserAutoSearch1.Enabled = True
                            End If
                            btnValve1Weight.Enabled = True
                        End If
                End Select

            Case Else
                Select Case gSYS(mSubStageNo).RunStatus
                    Case enmRunStatus.Running '運行中全部封鎖
                        'gDOCollection.GetSetState(enmDO.DoorLock) = True '上鎖
                        btnPurge1.Enabled = False
                        btnDispenserAutoValveCalibration1.Enabled = False
                        btnChangeGluePos.Enabled = False
                        btnClearGlue1.Enabled = False
                        btnDispenserAutoSearch1.Enabled = False
                        btnValve1Weight.Enabled = False
                    Case Else
                        'gDOCollection.GetSetState(enmDO.DoorLock) = False '解鎖
                        btnPurge1.Enabled = True
                        btnDispenserAutoValveCalibration1.Enabled = True
                        btnChangeGluePos.Enabled = True
                        btnClearGlue1.Enabled = True
                        btnDispenserAutoSearch1.Enabled = True
                        btnValve1Weight.Enabled = True
                End Select

        End Select

    End Sub

    '20170920
    Private Sub btnPm1_Click(sender As Object, e As EventArgs) Handles btnPm1.Click
        If btnPm1.Enabled = False Then
            Exit Sub
        End If
        btnPm1.Enabled = False
        mIsCanClose = False
        Pm(mSysStageNo)
        mIsCanClose = True
        btnPm1.Enabled = True
    End Sub

    ''' <summary>
    ''' PM
    ''' </summary>
    ''' <param name="DispStage"></param>
    ''' <remarks></remarks>
    Public Sub Pm(ByRef DispStage As Integer)

        gSyslog.Save("[frmOpStage]" & vbTab & "[btnPm]" & vbTab & "Click")

        '[說明]:回Home完成才能執行 沒個別初始Check
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOperation btnPm", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
            Exit Sub
        End If

        If gSYS(mSubStageNo).RunStatus = enmRunStatus.Running Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            Exit Sub
        End If

        '[說明]:判斷有無開啟Recipe 20161111
        'If gCRecipe.strName = "" Then
        '    MsgBox(gMsgHandler.GetMessage(Warn_3000011), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmOperation Purge", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
        '    Exit Sub
        'End If

        '[說明]:設定動作Flg & 按鍵安全機制 & Command
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                Select Case DispStage '多Stage機台需先閃安全位置
                    Case eSys.DispStage1, eSys.DispStage2
                        gDOCollection.SetState(enmDO.DoorLock, True) 'A機門鎖
                        MachineSafeMove(0)
                    Case eSys.DispStage3, eSys.DispStage4
                        gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖
                        MachineSafeMove(1)
                End Select
            Case Else
                gDOCollection.SetState(enmDO.DoorLock, True)
        End Select

        If cmbValve.SelectedIndex = -1 Then
            btnChangeGluePos.Enabled = True
            mIsCanClose = True
            MessageBox.Show("Please select the vavle ")
            Exit Sub
        Else
            If gfrmValvePm Is Nothing Then
                gfrmValvePm = New frmValvePm
            ElseIf gfrmValvePm.IsDisposed Then
                gfrmValvePm = New frmValvePm
            End If

            With gfrmValvePm

                '[說明]:選擇Stage and Valve
                Select Case mSysStageNo
                    Case enmStage.No1, eSys.DispStage1
                        Select Case cmbValve.SelectedIndex
                            Case eValveWorkMode.Valve1
                                gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
                            Case eValveWorkMode.Valve2
                                gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve2
                        End Select
                        .sys = gSYS(eSys.DispStage1)
                    Case enmStage.No2, eSys.DispStage2

                        Select Case cmbValve.SelectedIndex
                            Case eValveWorkMode.Valve1
                                gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve1
                            Case eValveWorkMode.Valve2
                                gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve2
                        End Select
                        .sys = gSYS(eSys.DispStage2)
                    Case enmStage.No3, eSys.DispStage3

                        Select Case cmbValve.SelectedIndex
                            Case eValveWorkMode.Valve1
                                gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve1
                            Case eValveWorkMode.Valve2
                                gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve2
                        End Select
                        .sys = gSYS(eSys.DispStage3)
                    Case enmStage.No4, eSys.DispStage4

                        Select Case cmbValve.SelectedIndex
                            Case eValveWorkMode.Valve1
                                gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve1
                            Case eValveWorkMode.Valve2
                                gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve2
                        End Select
                        .sys = gSYS(eSys.DispStage4)
                End Select

                .StartPosition = FormStartPosition.Manual
                .Location = New Point(0, 0)
                .ShowDialog()
            End With

        End If
    End Sub
End Class
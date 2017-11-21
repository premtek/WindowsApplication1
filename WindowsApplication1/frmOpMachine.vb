Imports ProjectCore
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectFeedback
Imports ProjectAOI
Imports ProjectMotion
Imports ProjectConveyor
Imports ProjectLaserInterferometer
Imports WetcoConveyor
Imports System.Random

''' <summary>暫停時動作處理</summary>
''' <remarks></remarks>
Public Class frmOpMachine

    Dim TmpFrmXX As frmOperator
    Public SysMachineNo As Integer = eSys.MachineA

    Dim mIsCanClose As Boolean

    Public Sub New(ByVal tmpForm As frmOperator)

        ' 此為設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。

        TmpFrmXX = tmpForm
    End Sub

    Private Sub frmUIViewerPauseMotor_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Close()
    End Sub

    Private Sub frmOpMachine_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub frmOpMachine_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not mIsCanClose Then
            e.Cancel = True
        End If
    End Sub

    Private Sub frmUIViewerPauseMotor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case SysMachineNo
            Case eSys.MachineA
                btnValve1Weight.Text = "Valve1 Weight"
                btnValve2Weight.Text = "Valve2 Weight"
            Case eSys.MachineB
                btnValve1Weight.Text = "Valve3 Weight"
                btnValve2Weight.Text = "Valve4 Weight"
        End Select

        If gSYS(SysMachineNo).Act(eAct.AutoRun).RunStatus = enmRunStatus.Running Then
            btnStartAllMachineA.Enabled = False
            btnValve1Purge.Enabled = False
            btnValve1Weight.Enabled = False
            btnValve2Weight.Enabled = False
            btnExchangePosAllMachineA.Enabled = False
            btnBackStartAllMachineA.Enabled = False
            btnLockAllMachineA.Enabled = False
            'btnUnLockAllMachineA.Enabled = False
            btnPauseAllMachineA.Enabled = True
            If gSYS(eSys.DispStage1).IsCanPause And gSYS(eSys.DispStage2).IsCanPause Then
                btnValve1Purge.Enabled = True
                btnValve1Weight.Enabled = True
                btnExchangePosAllMachineA.Enabled = True
                btnBackStartAllMachineA.Enabled = True
                btnLockAllMachineA.Enabled = True
                ' btnUnLockAllMachineA.Enabled = True
            End If
        Else
            btnStartAllMachineA.Enabled = False
            btnValve1Purge.Enabled = False
            btnValve1Weight.Enabled = False
            btnValve2Weight.Enabled = False
            btnExchangePosAllMachineA.Enabled = False
            btnBackStartAllMachineA.Enabled = False
            btnLockAllMachineA.Enabled = False
            ' btnUnLockAllMachineA.Enabled = False
            btnPauseAllMachineA.Enabled = False
        End If
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

#Region "單機開始生產"

    'Private Sub btnStartAllMachineA_Click(sender As Object, e As EventArgs) Handles btnStartAllMachineA.Click
    '    AllMachineStartAction(SysMachineNo)
    'End Sub

    ''[說明]:移動至原來點膠位置
    'Public Sub AllMachineStartAction(ByRef MachineType As Integer)
    '    gSyslog.Save("[frmUiViewer]" & vbTab & "[btnStart]" & vbTab & "Click")

    '    '[說明]:詢問是否執行動作
    '    If MessageBox.Show("Do you want AutoRun Again?", "My Application", MessageBoxButtons.YesNo) = DialogResult.No Then
    '        Exit Sub
    '    End If

    '    '[說明]:回Home完成才能執行 
    '    If gSYS(MachineType).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
    '        MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸.
    '        gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOperation btnChangeGluePos", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
    '        Exit Sub
    '    End If

    '    '[說明]:判斷有無開啟Recipe
    '    If gCRecipe.strName = "" Then
    '        MessageBox.Show(gMsgHandler.GetMessage(Warn_3000011)) '請先選取Recipe.
    '        gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmOperation btnStart", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
    '        Exit Sub
    '    End If

    '    '[說明]:此部分需確認命令下法~
    '    gSYS(MachineType).PuaseAction = sPauseAction.ReturnToProcess
    'End Sub

#End Region

#Region "單機暫停生產"

    Private Sub btnPauseAllMachineA_Click(sender As Object, e As EventArgs) Handles btnPauseAllMachineA.Click
        AutoPauseMachineAction(SysMachineNo)
    End Sub


    Public Sub AutoPauseMachineAction(ByRef MachineType As Integer)
        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnStop]" & vbTab & "Click")

        'ToDo:請點膠站幫忙,紀錄現在馬達位置

        '[說明]:詢問是否執行動作Pause
        Select Case MachineType
            Case eSys.MachineA
                If MessageBox.Show("Do you want Pause MachineA?", "My Application", MessageBoxButtons.YesNo) = DialogResult.No Then
                    Exit Sub
                End If
            Case eSys.MachineB
                If MessageBox.Show("Do you want Pause MachineB?", "My Application", MessageBoxButtons.YesNo) = DialogResult.No Then
                    Exit Sub
                End If
        End Select

        '[說明]:確認是否回Home
        If gSYS(MachineType).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
           
            '[說明]:SetPase
            Select Case MachineType
                Case eSys.MachineA
                    For mStageNo As Integer = eSys.DispStage1 To eSys.DispStage2
                        gSYS(mStageNo).ExternalPause = True
                    Next

                Case eSys.MachineB
                    For mStageNo As Integer = eSys.DispStage3 To eSys.DispStage4
                        gSYS(mStageNo).ExternalPause = True
                    Next

            End Select

        Else
            '[說明]:Initial中按暫停，停止移動
            'TmpFrmXX.StopMotorByAB(MachineType) '2016.06.27 增加復歸停止
            gCMotion.EmgStop(gSYS(MachineType).AxisX)
            gCMotion.EmgStop(gSYS(MachineType).AxisY)
            gCMotion.EmgStop(gSYS(MachineType).AxisZ)
            gCMotion.EmgStop(gSYS(MachineType).AxisB)
            gCMotion.EmgStop(gSYS(MachineType).AxisC)
            gSYS(MachineType).RunStatus = enmRunStatus.None

        End If

        '[說明]:按Pause後所有按鈕就可以使用了   

        btnValve1Purge.Enabled = True
        btnValve1Weight.Enabled = True
        btnExchangePosAllMachineA.Enabled = True
        btnBackStartAllMachineA.Enabled = True
        btnLockAllMachineA.Enabled = True
        'btnUnLockAllMachineA.Enabled = True
    End Sub

    ''' <summary>Stage流程暫停</summary>
    ''' <param name="DispStage"></param>
    ''' <remarks></remarks>
    Public Sub SetStagePause(ByRef DispStage As Integer)
        '[說明]:如果已經開始做測高，則可以中途暫停，停止做動,並且需整機回Home
        'If gSYS(DispStage).Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Running Then
        '    gSYS(DispStage).Act(eAct.DispenserAutoSearch).RunStatus = enmRunStatus.Pause
        '    TmpFrmXX.StopMotor()
        '    gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
        'End If

        ''[說明]:如果已經開始做Calibration，則可以中途暫停，停止做動,並且需整機回Home
        'If gSYS(DispStage).Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Running Then
        '    gSYS(DispStage).Act(eAct.AutoValveCalibration).RunStatus = enmRunStatus.Pause
        '    TmpFrmXX.StopMotor()
        '    gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None   '[須重新初始化]
        'End If

        ''[說明]:CCDScanFix時按暫停
        'If gSYS(DispStage).Act(eAct.CCDSCanFix).RunStatus = enmRunStatus.Running Then
        '    gSYS(DispStage).Act(eAct.CCDSCanFix).RunStatus = enmRunStatus.Pause
        'End If

        ''[說明]:干涉儀取值時按暫停
        'If gSYS(DispStage).Act(eAct.LaserReader).RunStatus = enmRunStatus.Running Then
        '    gSYS(DispStage).Act(eAct.LaserReader).RunStatus = enmRunStatus.Pause
        'End If

        ''[說明]:Dispensing時按暫停
        'If gSYS(DispStage).Act(eAct.Dispensing).RunStatus = enmRunStatus.Running Then
        '    gSYS(DispStage).Act(eAct.Dispensing).RunStatus = enmRunStatus.Pause
        'End If

        ''[說明]:CCDScanGlue時按暫停
        'If gSYS(DispStage).Act(eAct.CCDSCanGlue).RunStatus = enmRunStatus.Running Then
        '    gSYS(DispStage).Act(eAct.CCDSCanGlue).RunStatus = enmRunStatus.Pause
        'End If

        ''[說明]:Castec Unloading時按暫停
        'If gSYS(DispStage).Act(eAct.ProductUnload).RunStatus = enmRunStatus.Running Then
        '    gSYS(DispStage).Act(eAct.ProductUnload).RunStatus = enmRunStatus.Pause
        'End If

        ''[說明]:Castec Loading時按暫停
        'If gSYS(DispStage).Act(eAct.ProductLoading).RunStatus = enmRunStatus.Running Then
        '    gSYS(DispStage).Act(eAct.ProductLoading).RunStatus = enmRunStatus.Pause
        'End If
        ''[說明]:Loading時按暫停
        'If gSYS(DispStage).Act(eAct.Loading).RunStatus = enmRunStatus.Running Then
        '    gSYS(DispStage).Act(eAct.Loading).RunStatus = enmRunStatus.Pause
        'End If


        'If gSYS(DispStage).RunStatus = enmRunStatus.Running Then
        '    gSYS(DispStage).RunStatus = enmRunStatus.Pause
        'End If

        TmpFrmXX.btnStart.Enabled = True
    End Sub

#End Region

#Region "門鎖操作"

    Private Sub btnLockAllMachineA_Click(sender As Object, e As EventArgs) Handles btnLockAllMachineA.Click
        Select Case SysMachineNo
            Case eSys.MachineA
                gDOCollection.SetState(enmDO.DoorLock, Not gDOCollection.GetState(enmDO.DoorLock))
                Select Case gDOCollection.GetState(enmDO.DoorLock)
                    Case True
                        btnLockAllMachineA.Text = "Lock"
                    Case False
                        btnLockAllMachineA.Text = "UnLock"
                End Select
            Case eSys.MachineB
                gDOCollection.SetState(enmDO.DoorLock2, Not gDOCollection.GetState(enmDO.DoorLock2))
                Select Case gDOCollection.GetState(enmDO.DoorLock2)
                    Case True
                        btnLockAllMachineA.Text = "Lock"
                    Case False
                        btnLockAllMachineA.Text = "UnLock"
                End Select
        End Select

    End Sub

#End Region

#Region "天秤操作"

    Private Sub btnValve1Weight_Click(sender As Object, e As EventArgs) Handles btnValve1Weight.Click
        Select Case SysMachineNo
            Case eSys.MachineA
                AutoWeightByPauseDoorOpen(eSys.DispStage1)
            Case eSys.MachineB
                AutoWeightByPauseDoorOpen(eSys.DispStage3)
        End Select

    End Sub

    Private Sub btnValve2Weight_Click(sender As Object, e As EventArgs) Handles btnValve2Weight.Click
        Select Case SysMachineNo
            Case eSys.MachineA
                AutoWeightByPauseDoorOpen(eSys.DispStage2)
            Case eSys.MachineB
                AutoWeightByPauseDoorOpen(eSys.DispStage4)
        End Select

    End Sub

    Public Sub AutoWeightByPauseDoorOpen(ByRef DispStage As Integer)
        gSyslog.Save("[frmWeight]" & vbTab & "[btnWeighing]" & vbTab & "Click")

        '[說明]:回Home完成才能執行
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            '請先復歸
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmWeight btnWeighing", , gMsgHandler.GetMessage(Warn_3000005))
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '[說明]:判斷有無開啟Recipe
        If gCRecipe.strName = "" Then
            '找不到 Recipe 檔案!!
            gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmOperation btnStart", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
            MsgBox(gMsgHandler.GetMessage(Warn_3000011), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
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
        TmpFrmXX.DisabledButton(gSYS(DispStage).MachineNo)

        If MsgBox(msg, MsgBoxStyle.OkCancel, "Weigh Glue") <> MsgBoxResult.Ok Then
            gSyslog.Save("[frmWeight]" & vbTab & "[btnWeighing]" & vbTab & "Cancel")
            Exit Sub
        End If
        gWeight.gblnWeighingComeBack = True
        gWeight.gblnUpdateWeighing = True
    End Sub
#End Region

#Region "Purge"

    Private Sub btnPurgeAllMachineA_Click(sender As Object, e As EventArgs) Handles btnValve1Purge.Click
        AutoPurge(SysMachineNo)
    End Sub

    Public Sub AutoPurge(ByRef MachineType As Integer)
        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnPurge]" & vbTab & "Click")

        '[說明]:回Home完成才能執行 沒個別初始Check
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            '請先復歸
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOpMachine AutoPurge", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '[說明]:enmRunStatus.Running
        '[說明]:判斷是否已經在做Purge
        If gSYS(MachineType).Act(eAct.Purge).RunStatus = enmRunStatus.Running Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            Exit Sub
        End If

        '[說明]:設定動作Flg & 按鍵安全機制 & Command
        'Select Case MachineType
        '    Case eSys.MachineA
        '        gblnPurge(0) = True
        '        gblnPurge(1) = True
        '        TmpFrmXX.DisabledButton(eSys.MachineA)
        '        gSYS(MachineType).PuaseAction = sPauseAction.Purge
        '    Case eSys.MachineB
        '        gblnPurge(2) = True
        '        gblnPurge(3) = True
        '        TmpFrmXX.DisabledButton(eSys.MachineB)
        '        gSYS(MachineType).PuaseAction = sPauseAction.Purge
        'End Select

        gSYS(MachineType).Act(eAct.Purge).RunStatus = enmRunStatus.Running
        'gblnPurgeComeBack = True
    End Sub

#End Region

#Region "返回流程點膠位置"

    Private Sub btnBackStartAllMachineA_Click(sender As Object, e As EventArgs) Handles btnBackStartAllMachineA.Click
        AutoBackStartMachineAction(SysMachineNo)
        Me.btnStartAllMachineA.Enabled = True
        Me.btnValve1Purge.Enabled = False
        Me.btnValve1Weight.Enabled = False
        Me.btnExchangePosAllMachineA.Enabled = False
        Me.btnBackStartAllMachineA.Enabled = False
        Me.btnLockAllMachineA.Enabled = False
        ' Me.btnUnLockAllMachineA.Enabled = False
        Me.btnPauseAllMachineA.Enabled = True
    End Sub


    '[說明]:移動至原來點膠位置
    Public Sub AutoBackStartMachineAction(ByRef MachineType As Integer)
        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnPurge]" & vbTab & "Click")

        '[說明]:回Home完成才能執行 
        If gSYS(MachineType).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            '請先復歸
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOpMachine AutoBackStartMachineAction", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        ''[說明]:使用Finex命令模式 面移動方式 待測試
        'gSYS(MachineType).Command = enmRunStatus.PauseDoorOpen
        'gSYS(MachineType).PuaseAction = sPauseAction.GoToBackPos

    End Sub

#End Region

#Region "換膠位置"

    Private Sub btnExchangePosAllMachineA_Click(sender As Object, e As EventArgs) Handles btnExchangePosAllMachineA.Click
        AutoMovePosMachineAction(SysMachineNo)
    End Sub

    Public Sub AutoMovePosMachineAction(ByRef MachineType As Integer)
        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnPurge]" & vbTab & "Click")

        '[說明]:回Home完成才能執行 沒個別初始Check
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            '請先復歸
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmOpMachine AutoMovePosMachineAction", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '[說明]:使用Finex命令模式 面移動方式 待測試 
        'gSYS(MachineType).Command = enmRunStatus.PauseDoorOpen
        'gSYS(MachineType).PuaseAction = sPauseAction.GoToSetPos

    End Sub

#End Region







End Class
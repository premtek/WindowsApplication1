﻿Imports ProjectCore
Imports ProjectIO
Imports ProjectFeedback
Imports ProjectRecipe
Imports ProjectMotion
Imports System.Text.RegularExpressions
Imports System.Data.OleDb
'Imports System.Web.UI.DataVisualization.Charting       '//Chart Web 伺服器控制項的方法和屬性
Imports System.Drawing                                 '//繪圖功能的存取
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Random
Imports System.IO

Public Class frmWeight


    ''' <summary>對外</summary>
    ''' <remarks></remarks>
    Public sys As sSysParam


    Dim ErrMessage As String = ""
    Private Sub btnWeighing_Click(sender As Object, e As EventArgs) Handles btnWeighingRun.Click

        gSyslog.Save("[frmWeight]" & vbTab & "[btnWeighing]" & vbTab & "Click")
        '[說明]:紀錄按了哪些按鈕
        'Call WriteButtonLog(gUserLevel, "frmRecipe", "btnWeighing")

        '[說明]:回Home完成才能執行
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmWeight btnWeighing", , gMsgHandler.GetMessage(Warn_3000005)) 'System Home 尚未完成!!
            MsgBox(gMsgHandler.GetMessage(Warn_3000005), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '[說明]:正在執行Auto
        If gSYS(eSys.OverAll).Act(eAct.AutoRun).RunStatus = enmRunStatus.Running Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000007), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '[說明]:判斷有無開啟Recipe 20161111
        If gCRecipe.strName = "" Then
            gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmWeight WeighingRun", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
            MsgBox(gMsgHandler.GetMessage(Warn_3000011), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If


        '[說明]:正在執行Manual Status 判斷是否已經在做秤重          '20161208
        If gSYS(sys.EsysNum).RunStatus = enmRunStatus.Running Then
            Exit Sub
        End If

        '20161208
        If gSYS(sys.EsysNum).Act(eAct.WeightUnit).RunStatus = enmRunStatus.Running Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000007), "Warn_3000007", eMessageLevel.Warning)
            MsgBox(gMsgHandler.GetMessage(Warn_3000007), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If


        Dim msg As String = "Weight Glue?"
        msg = "Weight Glue at Pos(" & gSSystemParameter.Pos.WeightCalibration(sys.StageNo).ValvePosX(sys.SelectValve) & "," &
                                     gSSystemParameter.Pos.WeightCalibration(sys.StageNo).ValvePosY(sys.SelectValve) & "," &
                                     gSSystemParameter.Pos.WeightCalibration(sys.StageNo).ValvePosZ(sys.SelectValve) & ")?"

        If MsgBox(msg, MsgBoxStyle.OkCancel + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Weigh Glue") <> MsgBoxResult.Ok Then
            gSyslog.Save("[frmWeight]" & vbTab & "[btnWeighing]" & vbTab & "Cancel")
            Exit Sub
        End If

        If gWeight.GetMFunctionWeight_WeightCounter(sys.StageNo) = 0 Then
            '[說明]:清除圖式
            listWeight.Items.Clear()
            chartDotsWeight.Series.Clear()
            chartWeight.Series.Clear()
        End If
        Dim Upper As Double          '[說明]:重量上限
        Dim Lower As Double          '[說明]:重量下限

        '[說明]:計算秤重上下限 公式:重量 = 預設重量 + ((預設重量 * (比率))
        Upper = Math.Round((gFlowRateDB(gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)).WeighingWeight + ((gFlowRateDB(gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)).WeighingWeight * gFlowRateDB(gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)).WeighingTolerance / 100))), 2)
        Lower = Math.Round((gFlowRateDB(gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)).WeighingWeight - ((gFlowRateDB(gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)).WeighingWeight * gFlowRateDB(gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)).WeighingTolerance / 100))), 2)

        InvokeCreateChart1(Me.chartWeight, Upper, Lower, gFlowRateDB(gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)).WeighingPointNumber, gWeight.GetMFunctionWeight_WeightCounter(sys.StageNo))
        InvokeCreateChart2(Me.chartDotsWeight, gFlowRateDB(gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)).WeighingWeightDotMax, gFlowRateDB(gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)).WeighingWeightDotMin, gFlowRateDB(gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)).WeighingPointNumber, gWeight.GetMFunctionWeight_WeightCounter(sys.StageNo))

        Select gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.DCS_500AD
                If MsgBox("請確認另一個Stage目前處於安全位置", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                    gSyslog.Save("[frmWeight]" & vbTab & "[btnWeighing]" & vbTab & "Stage in Safe Pos By User Check")
                    '20161208
                    gSYS(sys.EsysNum).Act(eAct.WeightUnit).RunStatus = enmRunStatus.Running
                    gSYS(sys.EsysNum).Command = eSysCommand.ManuallyWeightUnit
                    gWeight.gblnWeighingComeBack = True
                    gWeight.gblnUpdateWeighing = True
                Else
                    gSyslog.Save("[frmWeight]" & vbTab & "[btnWeighing]" & vbTab & "Stage in NonSafe Pos By User Check")
                    MsgBox("請移動Stage位置，避免撞機", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End If
            Case Else
                ''20161208
                gSYS(sys.EsysNum).Act(eAct.WeightUnit).RunStatus = enmRunStatus.Running
                gSYS(sys.EsysNum).Command = eSysCommand.ManuallyWeightUnit
                gWeight.gblnWeighingComeBack = True
                gWeight.gblnUpdateWeighing = True
        End Select

    End Sub

    Private Sub btnProductionWeighingUnit_Click(sender As Object, e As EventArgs)
        gSyslog.Save("[frmWeight]" & vbTab & "[btnProductionWeighingUnit]" & vbTab & "Click")
        If cmbBalance.SelectedIndex < 0 Then
            '請先選擇天秤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000028))
            MsgBox(gMsgHandler.GetMessage(Warn_3000028), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        Dim value As Double
        If gBalanceCollection.RequestCurrentValue(cmbBalance.SelectedIndex, value, True) = False Then
            MsgBox(gMsgHandler.GetMessage(Error_1015002), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If gSSystemParameter.MachineType = enmMachineType.DCSW_800AQ Then
            lblWeight.Text = value.ToString("F2") '顯示小數點後2位
        Else
            lblWeight.Text = value.ToString("F1") '顯示小數點後1位

        End If

    End Sub


    Private Async Sub frmWeight_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '20160812
        Dim toolTip1 As New ToolTip()
        gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.ZUpPos) 'Soni + 2017.04.27 第二閥氣缸下降前, Z先上升

        ' Set up the delays for the ToolTip.
        toolTip1.AutoPopDelay = 5000
        toolTip1.InitialDelay = 1000
        toolTip1.ReshowDelay = 500
        ' Force the ToolTip text to be displayed whether or not the form is active.
        toolTip1.ShowAlways = True


        toolTip1.SetToolTip(Me.btnWeightRequestStableValue, "Get Value")
        toolTip1.SetToolTip(Me.btnWeightClean, "Clean Zero")
        toolTip1.SetToolTip(Me.btnWeightLoad, "Load")
        toolTip1.SetToolTip(Me.btnWeighingRun, "Run")
        toolTip1.SetToolTip(Me.butWeighingPause, "Pause")
        toolTip1.SetToolTip(Me.btnWeighingStop, "Stop")

        toolTip1.SetToolTip(Me.btnWeightDBAdd, "Add Item")
        toolTip1.SetToolTip(Me.btnWeightDBDel, "Delete Item")
        toolTip1.SetToolTip(Me.btnSave, "Save")
        toolTip1.SetToolTip(Me.btnClose, "Close Frm")

        '初始CPK資料
        lblCPKData.Text = Format(0, "#0.00000")

        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eEnglish
                Me.chartWeight.ChartAreas(0).AxisX.Title = "Run"
                Me.chartWeight.ChartAreas(0).AxisY.Title = "Weight"
                Me.chartDotsWeight.ChartAreas(0).AxisX.Title = "Run"
                Me.chartDotsWeight.ChartAreas(0).AxisY.Title = "Dot Weight"
            Case enmLanguageType.eSimplifiedChinese
                Me.chartWeight.ChartAreas(0).AxisX.Title = "次數"
                Me.chartWeight.ChartAreas(0).AxisY.Title = "重量"
                Me.chartDotsWeight.ChartAreas(0).AxisX.Title = "次數"
                Me.chartDotsWeight.ChartAreas(0).AxisY.Title = "Dot重量"
            Case enmLanguageType.eTraditionalChinese
                Me.chartWeight.ChartAreas(0).AxisX.Title = "次數"
                Me.chartWeight.ChartAreas(0).AxisY.Title = "重量"
                Me.chartDotsWeight.ChartAreas(0).AxisX.Title = "次數"
                Me.chartDotsWeight.ChartAreas(0).AxisY.Title = "Dot重量"
        End Select





        cmbBalance.Items.Clear()
        For i As Integer = 0 To enmBalance.Max
            cmbBalance.Items.Add("Balance" & (i + 1))
        Next

        If cmbBalance.Items.Count > 0 Then
            If cmbBalance.SelectedIndex < 0 Then
                cmbBalance.SelectedIndex = 0
            End If
        End If

        Select Case sys.StageNo
            Case 0
                txtTiltValvePosB.Text = gSSystemParameter.Stage1TiltAngle.ToString
            Case 1
                txtTiltValvePosB.Text = gSSystemParameter.Stage2TiltAngle.ToString
            Case 2
                txtTiltValvePosB.Text = gSSystemParameter.Stage3TiltAngle.ToString
            Case 3
                txtTiltValvePosB.Text = gSSystemParameter.Stage4TiltAngle.ToString
        End Select

        '20170720 進秤重頁面先關燈源通道
        gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No1), False)
        gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No2), False)
        gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No3), False)
        gSysAdapter.SetLightOnOff(gSysAdapter.CCDLightMapping(sys.CCDNo, enmValveLight.No4), False)



        ''暫時先visable 
        If sys.AxisB <> -1 Then
            gpTiltMove.Visible = False
        Else
            gpTiltMove.Visible = False
        End If
        WeightDB_Update(sys)

        Await Task.Run(Sub()
                           System.Threading.Thread.Sleep(300)
                           Do
                           Loop Until gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed
                       End Sub)
        ValveCylinderAction(sys.StageNo, sys.SelectValve, enmUpDown.Down) 'Soni + 2017.04.27 氣缸下

        Timer1.Start()
        Timer1.Enabled = True
        Timer1.Interval = 200

    End Sub



    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnSave.Click


        If lstWeighDB.SelectedIndex < 0 Then
            lstWeighDB.BackColor = Color.Yellow
            lstWeighDB.Refresh() ''Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstWeighDB.BackColor = Color.White
            Exit Sub
        End If

        If GetWeightDB(gFlowRateDB(lstWeighDB.SelectedItem)) Then
            Dim folderPath As String = Application.StartupPath & "\Database\Weight\"
            Dim fileName As String = folderPath & gFlowRateDB(lstWeighDB.SelectedItem).Name & ".wdb"
            gFlowRateDB(lstWeighDB.SelectedItem).Save(fileName)
            'Sue20170627
            '存檔成功 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
        'WeightDB_Update()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnWeightRequestStableValue.Click
        gSyslog.Save("[frmWeight]" & vbTab & "[btnProductionWeighingUnit]" & vbTab & "Click")
        If cmbBalance.SelectedIndex < 0 Then
            '請先選擇天秤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000028))
            MsgBox(gMsgHandler.GetMessage(Warn_3000028), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Dim value As Double

        '20160830
        If gBalanceCollection.RequestStableValue(sys.BalanceNo, value, True) = False Then
            '微量天平重量不穩定!
            Select Case sys.StageNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1015006))
                    MsgBox(gMsgHandler.GetMessage(Error_1015006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1015006))
                    MsgBox(gMsgHandler.GetMessage(Error_1015006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 2
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1015106))
                    MsgBox(gMsgHandler.GetMessage(Error_1015106), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 3
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1015106))
                    MsgBox(gMsgHandler.GetMessage(Error_1015106), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            'Dim Str As String = "Balance is unstabitily!!"
            'MsgBox(Str, MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox(gMsgHandler.GetMessage(Error_1015002), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gSSystemParameter.MachineType = enmMachineType.DCSW_800AQ Then
            lblWeight.Text = value.ToString("F2") '顯示小數點2位
        Else
            lblWeight.Text = value.ToString("F1") '顯示小數點1位
        End If
        'gBalanceCollection.GetValue(cmbBalance.SelectedIndex)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnWeightClean.Click

        '    gBalanceCollection.Rezero(cmbBalance.SelectedIndex)
        '    lblWeight.Text = "0"


        Do
            If Not gBalanceCollection.IsBusy(sys.BalanceNo) Then
                gBalanceCollection.Rezero(sys.BalanceNo)
                lblWeight.Text = "0"
                Exit Do
            End If
            If gBalanceCollection.IsTimeOut(sys.BalanceNo) Then
                MsgBox(gMsgHandler.GetMessage(Error_1015002), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Exit Sub
            End If
        Loop
    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Dim Upper As Double
        'Dim Lower As Double
        'Dim DotsWeight As Double
        Dim RndNum As Random = New Random



        'Dim d1, d2, d3, d4 As Decimal
        '[說明]:計算秤重上下限 公式:重量 = 預設重量 + ((預設重量 * (比率))/2)
        'Upper = gFlowRateDB(gCRecipe.FlowRateName(sys.EsysNum)).WeighingWeight + ((gFlowRateDB(gCRecipe.FlowRateName(sys.EsysNum)).WeighingWeight * (gFlowRateDB(gCRecipe.FlowRateName(sys.EsysNum)).WeighingTolerance / 100)) / 2)
        'Lower = gFlowRateDB(gCRecipe.FlowRateName(sys.EsysNum)).WeighingWeight - ((gFlowRateDB(gCRecipe.FlowRateName(sys.EsysNum)).WeighingWeight * (gFlowRateDB(gCRecipe.FlowRateName(sys.EsysNum)).WeighingTolerance / 100)) / 2)


        'Format((RndNum.NextDouble() + gFlowRateDB(gCRecipe.FlowRateName(sys.EsysNum)).WeighingWeight), ".000")
        'chartWeight.Series("標準上限值").Points.AddXY(WeightCounter, Upper)
        'chartWeight.Series("重量").Points.AddXY(WeightCounter, Format((RndNum.NextDouble() + gCRecipe.WeighingWeight(gSYS(eSys.Manual).ValveNo)), ".000"))
        'chartWeight.Series("標準下限值").Points.AddXY(WeightCounter, Lower)
        ' WeightCounter += 1

        If gSYS(sys.EsysNum).RunStatus = enmRunStatus.Finish Then
            btnWeighingRun.Enabled = True
            btnClose.Enabled = True
            '20170602按鍵保護
            btnGoTilt.Enabled = True
            btnSave.Enabled = True
        ElseIf gSYS(sys.EsysNum).RunStatus = enmRunStatus.Running Then
            btnWeighingRun.Enabled = False
            btnClose.Enabled = False
            '20170602按鍵保護
            btnGoTilt.Enabled = False
            btnSave.Enabled = False
        ElseIf gSYS(sys.EsysNum).RunStatus = enmRunStatus.Alarm Then
            btnWeighingRun.Enabled = True
            btnClose.Enabled = True
            '20170602按鍵保護
            btnGoTilt.Enabled = True
            btnSave.Enabled = True
        End If
    End Sub

    Public Sub UpdateChart(ByVal WeighingWeight As Double, ByVal WeighingTolerance As Double)
        Dim Upper As Double
        Dim Lower As Double
        'Dim DotsWeight As Double
        Dim RndNum As Random = New Random

        '[說明]:Update chartWeight
        chartWeight.Titles.Clear()
        chartWeight.Series.Clear()
        Dim newTitlesWeight1 As New Title("重量分布圖") '建立标题
        chartWeight.Titles.Add(newTitlesWeight1)

        ' Dim newSeries1 As New Series("標準上限值") '新增数据集
        Dim newSeries1 As New Series("Upper") '新增数据集
        newSeries1.ChartType = SeriesChartType.Line '直线
        newSeries1.BorderWidth = 2
        newSeries1.Color = Color.Red
        '  newSeries1.XValueType = ChartValueType.Time
        newSeries1.IsValueShownAsLabel = False
        chartWeight.Series.Add(newSeries1)

        'Dim newSeries2 As New Series("重量") '新增数据集
        Dim newSeries2 As New Series("Weight") '新增数据集
        newSeries2.ChartType = SeriesChartType.Line
        newSeries2.BorderWidth = 2
        newSeries2.Color = Color.Green
        '  newSeries2.XValueType = ChartValueType.Time
        newSeries2.IsValueShownAsLabel = True
        newSeries2.MarkerStyle = MarkerStyle.Square
        chartWeight.Series.Add(newSeries2)

        'Dim newSeries3 As New Series("標準下限值") '新增数据集
        Dim newSeries3 As New Series("Lower") '新增数据集
        newSeries3.ChartType = SeriesChartType.Line
        newSeries3.BorderWidth = 2
        newSeries3.Color = Color.Red
        '   newSeries3.XValueType = ChartValueType.Time
        newSeries3.IsValueShownAsLabel = False
        chartWeight.Series.Add(newSeries3)

        '[說明]:計算秤重上下限 公式:重量 = 預設重量 + ((預設重量 * (比率)))
        Upper = WeighingWeight + (WeighingWeight * ((WeighingTolerance / 100)))
        Lower = WeighingWeight - (WeighingWeight * ((WeighingTolerance / 100)))


        Dim max As Double = Upper + (Upper - Lower) * 4
        Dim min As Double = Lower - (Upper - Lower) * 4
        max = Math.Round(max, 2)
        min = Math.Round(min, 2)
        chartWeight.ChartAreas(0).AxisY.Maximum = max
        chartWeight.ChartAreas(0).AxisY.Minimum = min


        Dim Dot_max As Double = gFlowRateDB(gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)).WeighingWeightDotMax
        Dim Dot_min As Double = gFlowRateDB(gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve)).WeighingWeightDotMin
        Dot_max = Math.Round(Dot_max, 2)
        Dot_min = Math.Round(Dot_min, 2)


        '[說明]:Update chartDotsWeight
        chartDotsWeight.Titles.Clear()
        chartDotsWeight.Series.Clear()
        Dim newTitles1 As New Title("單點重量分布圖") '建立标题
        newTitles1.Text = "重量分布圖"
        chartDotsWeight.Titles.Add(newTitles1)

        Dim newSeries4 As New Series("Dot_Max") '新增数据集
        newSeries4.ChartType = SeriesChartType.Column '直條圖
        newSeries4.BorderWidth = 2
        newSeries4.Color = Color.Red
        newSeries4.IsValueShownAsLabel = False
        chartDotsWeight.Series.Add(newSeries4)

        Dim newSeries5 As New Series("Dot_Weight")
        newSeries5.ChartType = SeriesChartType.Column
        newSeries5.BorderWidth = 2
        newSeries5.Color = Color.Green
        newSeries5.IsValueShownAsLabel = True
        newSeries5.MarkerStyle = MarkerStyle.Square
        chartDotsWeight.Series.Add(newSeries5)

        Dim newSeries6 As New Series("Dot_Min")
        newSeries6.ChartType = SeriesChartType.Column
        newSeries6.BorderWidth = 2
        newSeries6.Color = Color.Red
        newSeries6.IsValueShownAsLabel = False
        chartDotsWeight.Series.Add(newSeries6)

        '[說明]:畫Dot的保留
        '      chartDotsWeight.ChartAreas(0).AxisY.Minimum = Math.Round(((Lower / gCRecipe.intWeighingPointNumber(gSYS(eSys.Manual).ValveNo)) - ((Upper / gCRecipe.intWeighingPointNumber(gSYS(eSys.Manual).ValveNo)) - (Lower / gCRecipe.intWeighingPointNumber(gSYS(eSys.Manual).ValveNo))) / 2), 3)
        '      chartDotsWeight.ChartAreas(0).AxisY.Maximum = Math.Round(((Upper / gCRecipe.intWeighingPointNumber(gSYS(eSys.Manual).ValveNo)) + ((Upper / gCRecipe.intWeighingPointNumber(gSYS(eSys.Manual).ValveNo)) - (Lower / gCRecipe.intWeighingPointNumber(gSYS(eSys.Manual).ValveNo))) / 2), 3)

        chartDotsWeight.ChartAreas(0).AxisY.Maximum = Dot_max
        chartDotsWeight.ChartAreas(0).AxisY.Minimum = Dot_min
    End Sub


    Private Sub frmWeight_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        '[說明]:紀錄名稱
        Dim Str As String = Me.Text
        Dim RunStatus As String

        If gSYS(sys.EsysNum).Act(eAct.WeightUnit).RunStatus = enmRunStatus.Running Then

            RunStatus = "Running.... "
            e.Cancel = True
            Me.Hide()
            Exit Sub
        End If

        '[說明]:如有Recipe DB ReLoad回來
        If gCRecipe.strName <> "" Then
            Dim DefaultDirectory As String = Application.StartupPath & "\Recipe\"
            DefaultDirectory = Application.StartupPath & "\Recipe\" & gCRecipe.strName
            'Recipe讀檔
            gCRecipe.LoadStageParts(DefaultDirectory) '檔案讀取
        End If

        Timer1.Enabled = False
        Me.Dispose()
        '  GC.SuppressFinalize(Me)

        If Str = "Valve1 Weight" Then
            gfrmWeightValve1 = Nothing
        ElseIf Str = "Valve2 Weight" Then
            gfrmWeightValve2 = Nothing
        ElseIf Str = "Valve3 Weight" Then
            gfrmWeightValve3 = Nothing
        ElseIf Str = "Valve4 Weight" Then
            gfrmWeightValve4 = Nothing
        ElseIf Str = "MachineA FlowRate" Then
            gfrmWeight = Nothing
        ElseIf Str = "MachineB FlowRate" Then
            gfrmWeightB = Nothing
        End If

        '[說明]:離開frm即刻清零,不然畫Chart那邊會有問題   20170711
        gWeight.InitialMFunctionWeight_WeightCounter(enmStage.Max)

    End Sub

    Private Sub butChart_Click(sender As Object, e As EventArgs) Handles btnWeightLoad.Click
        '[說明]:選取Recipe檔案
        Dim newDataTable As New DataTable
        Dim CloumnNameFlag As Boolean = True
        Dim Weight(1000) As String
        Dim DotsWeight(1000) As String
        Dim WeightCounter As Double = 0
        Dim DotsWeightCounter As Double = 0
        Dim Upper As Double
        Dim Lower As Double
        Dim WeighingWeight As Double
        Dim WeighingTolerance As Double

        With OFDLoadRecipe
            .InitialDirectory = "D:\PIIData\DataLog\"               '取得或設定檔案對話方塊所顯示的初始目錄。
            .Filter = "txt files (*.csv)|*.txt|All files (*.*)|*.*"
            .FilterIndex = 2                                        '取得或設定檔案對話方塊中目前所選取之篩選條件的索引。
            .RestoreDirectory = True                                '取得或設定值，指出對話方塊是否在關閉前將目錄還原至先前選取的目錄。
            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If

            '[說明]:確認檔案是否開啟
            If FileOpened(.FileName) = False Then
                Dim sr As IO.StreamReader = New IO.StreamReader(.FileName)
                Dim Tempstr As String = Nothing
                Tempstr = sr.ReadLine
                While Tempstr IsNot Nothing
                    If CloumnNameFlag = True Then
                        For i As Integer = 0 To Tempstr.Split(",").Length - 1
                            newDataTable.Columns.Add(Tempstr.Split(",")(i))
                        Next
                        CloumnNameFlag = False
                    Else
                        Dim row As DataRow = newDataTable.NewRow
                        For i As Integer = 0 To Tempstr.Split(",").Length - 1
                            If i = 3 Then
                                Weight(WeightCounter) = Tempstr.Split(",")(i)
                                WeightCounter += 1
                            ElseIf i = 4 Then
                                DotsWeight(DotsWeightCounter) = Tempstr.Split(",")(i)
                                DotsWeightCounter += 1
                            ElseIf i = 5 Then
                                WeighingWeight = Tempstr.Split(",")(i)
                            ElseIf i = 6 Then
                                WeighingTolerance = Tempstr.Split(",")(i)
                            End If

                            'row.Item(i) = Tempstr.Split(",")(i)
                        Next
                        'newDataTable.Rows.Add(row)
                    End If
                    Tempstr = sr.ReadLine
                End While
                sr.Close()

                '[說明]:清除圖式
                listWeight.Items.Clear()
                chartDotsWeight.Series.Clear()
                chartWeight.Series.Clear()

                '[說明]:設置前置參數
                UpdateChart(WeighingWeight, WeighingTolerance)

                '[說明]:計算秤重上下限 公式:重量 = 預設重量 + ((預設重量 * (比率)))
                Upper = WeighingWeight + (WeighingWeight * (WeighingTolerance / 100))
                Lower = WeighingWeight - (WeighingWeight * (WeighingTolerance / 100))

                For i As Integer = 0 To WeightCounter - 1
                    Dim x As Double = Weight(i)
                    x = Math.Round(x, 2)
                    chartWeight.Series("Upper").Points.AddXY(i + 1, Math.Round(Upper, 2))
                    chartWeight.Series("Weight").Points.AddXY(i + 1, Val(x))
                    chartWeight.Series("Lower").Points.AddXY(i + 1, Math.Round(Lower, 2))
                Next

                For i As Integer = 0 To WeightCounter - 1
                    'Dim x As Double = DotsWeight(i)
                    ' x = Math.Round(x, 4)
                    Dim x As Double = Weight(i)
                    x = Math.Round(x, 2)

                    chartDotsWeight.Series("Weight").Points.AddXY(i + 1, Val(x))

                Next
            Else
                MsgBox("檔案已開啟!!!", 0 + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        End With
    End Sub

    '確認檔案是否開啟
    Public Function FileOpened(ByVal Name As String) As Boolean
        On Error GoTo Err
        Dim s2 As New FileStream(Name, FileMode.Open, FileAccess.Read, FileShare.None)
        s2.Close()
        FileOpened = False
        Exit Function
Err:
        FileOpened = True
    End Function

    '讀檔範例
    Public Function CSVFileToDT(ByVal FullFileName As String) As DataTable
        Dim newDataTable As New DataTable
        Dim CloumnNameFlag As Boolean = True

        With OFDLoadRecipe
            .InitialDirectory = "D:\PIIData\DataLog\"               '取得或設定檔案對話方塊所顯示的初始目錄。
            .Filter = "txt files (*.csv)|*.txt|All files (*.*)|*.*"
            .FilterIndex = 2                                        '取得或設定檔案對話方塊中目前所選取之篩選條件的索引。
            .RestoreDirectory = True                                '取得或設定值，指出對話方塊是否在關閉前將目錄還原至先前選取的目錄。
            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                Return Nothing
            End If

            Dim sr As IO.StreamReader = New IO.StreamReader(FullFileName)
            Dim Tempstr As String = Nothing
            Tempstr = sr.ReadLine
            While Tempstr IsNot Nothing
                If CloumnNameFlag = True Then
                    For i As Integer = 0 To Tempstr.Split(",").Length - 1
                        newDataTable.Columns.Add(Tempstr.Split(",")(i))
                    Next
                    CloumnNameFlag = False
                Else
                    Dim row As DataRow = newDataTable.NewRow
                    For i As Integer = 0 To Tempstr.Split(",").Length - 1
                        row.Item(i) = Tempstr.Split(",")(i)
                    Next
                    newDataTable.Rows.Add(row)
                End If
                Tempstr = sr.ReadLine
            End While
            sr.Close()


        End With
        Return newDataTable
    End Function
    Public Function LoadRecipeCsv(ByVal FileName As String) As Boolean
        Dim sr As System.IO.StreamReader
        Dim data As String = ""
        Try
            Do
                'CSV檔案開啟只支援ANSI,Big5格式
                'StreamReader開ANSI會變亂碼.
                sr = New System.IO.StreamReader(FileName, System.Text.Encoding.Unicode)
                data = sr.ReadLine
                If Not data.StartsWith("//") Then '不是標頭

                End If
            Loop Until sr.EndOfStream

        Catch ex As Exception
            MsgBox(data & vbCrLf & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try

        Return True
    End Function

    Private Sub butPause_Click(sender As Object, e As EventArgs) Handles butWeighingPause.Click

        gSYS(sys.EsysNum).Act(eAct.WeightUnit).RunStatus = enmRunStatus.Finish
        gSYS(sys.EsysNum).RunStatus = enmRunStatus.Finish

    End Sub


    Private Sub txtWeighingTolerance_KeyPress(sender As Object, e As KeyPressEventArgs)
        'If Char.IsDigit(e.KeyChar) Or e.KeyChar = Chr(8) Then
        '    '20160901
        '    If IsNumeric(sender.Text) Or sender.Text = "" Then
        '        e.Handled = False
        '    Else
        '        e.Handled = True
        '    End If
        'Else
        '    If IsNumeric(sender.Text & e.KeyChar) Then
        '        e.Handled = False
        '    Else
        '        e.Handled = True
        '    End If
        'End If

        Premtek.Base.CKeyPress.CheckUDecimal(sender, e)
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        'Sue20170627
        gSyslog.Save("[frmWeight]" & vbTab & "[btnClose]" & vbTab & "Click")
        Timer1.Enabled = False
        '20170623 離開時Z 軸升至安全高度
        gCMotion.AbsMove(sys.AxisZ, gSSystemParameter.Pos.WeightCalibration(sys.StageNo).SafePosZ(sys.SelectValve))
        Me.Close()

    End Sub


    Function GetWeightDB(ByRef item As CFlowRateParameter) As Boolean
        'Jeffadd 20160726
        With item
            '20161122
            If nmuWeightTimes.Value <= 0 Then
                .WeighingTimes = 1
                nmuWeightTimes.Value = 1
            Else
                .WeighingTimes = Val(nmuWeightTimes.Value)
            End If

            If Val(nmuWeightTimes.Value) >= 500000 Then
                nmuWeightTimes.Value = 500000
                .WeighingTimes = 500000
            End If


            .WeighingPointNumber = nmuWeighingPointNumber.Value
            If cbEnableAverageWeight.Checked = True Then
                .WeighingEnableDoAverageWeight = True
            Else
                .WeighingEnableDoAverageWeight = False
            End If

            If cbEnableAverageDot.Checked = True Then
                .WeighingEnableDoAverageDot = True
            Else
                .WeighingEnableDoAverageDot = False
            End If

            '.WeighingWeight = txtWeighingWeight.Text
            .WeighingWaitCalibrationTime = nmuWeighingWaitCalibrationTime.Value
            .WeighingTolerance = nmuWeighingTolerance.Value
            .WeighingWeightDotMin = nmuWeightDotMin.Value
            .WeighingWeightDotMax = nmuWeightDotMax.Value
            If cbEnableProductionRunFail.Checked = True Then
                .WeighingEnableProductionRunFail = True
            Else
                .WeighingEnableProductionRunFail = False
            End If
            .WeighingPulseTimes = nmuWeighingPulseTime.Value
            .WeighingCycleTime = nmuWeighingCycleTime.Value
            .WeighingBlanceSteadyTimes = nmuWeighingBlanceSteadyTime.Value
            .WeighingTableTimes = nmuWeighingTableSteadyTime.Value
            .WeighingPressure = nmuWeighingPressure.Value

            If nmuWeighingWeight.Value < (.WeighingPointNumber * .WeighingWeightDotMin) / 4 Then
                MsgBox("設定目標重量異常", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                nmuWeighingWeight.Value = (.WeighingPointNumber * .WeighingWeightDotMin) / 4
            End If
            .WeighingWeight = CDbl(nmuWeighingWeight.Value)


            .WeighingGetBalance = nmuWeighingGetBalance.Value
        End With
        Return True
    End Function

    Function SetWeightDB(ByRef item As CFlowRateParameter) As Boolean
        'Jeffadd 20160726
        With item

            If .WeighingTimes <= 0 Then
                nmuWeightTimes.Value = 1
                .WeighingTimes = 1
            Else
                nmuWeightTimes.Value = .WeighingTimes
            End If

            If .WeighingTimes >= 500000 Then
                nmuWeightTimes.Value = 500000
                .WeighingTimes = 500000
            End If
            Premtek.ControlMisc.SetNumericValue(nmuWeighingPointNumber, .WeighingPointNumber)

            'EnableDoAverageWeight
            If .WeighingEnableDoAverageWeight = True Then
                cbEnableAverageWeight.Checked = True
            Else
                cbEnableAverageWeight.Checked = False
                '[說明]:確認False 關閉以下選項
                'txtWeighingWeight.Enabled = False
            End If
            'EnableDoAverageDot
            If .WeighingEnableDoAverageDot = True Then
                cbEnableAverageDot.Checked = True
            Else
                cbEnableAverageDot.Checked = False
                '[說明]:確認False 關閉以下選項
                nmuWeightDotMin.Enabled = False
                nmuWeightDotMax.Enabled = False
            End If

            If .WeighingEnableDoAverageWeight = True Or .WeighingEnableDoAverageDot = True Then
                nmuWeighingWaitCalibrationTime.Enabled = True
                nmuWeighingTolerance.Enabled = True
            Else
                'txtWeighingWaitCalibrationTime.Enabled = False
                'txtWeighingTolerance.Enabled = False
            End If


            'txtWeighingWeight.Text = .WeighingWeight
            Premtek.ControlMisc.SetNumericValue(nmuWeighingWaitCalibrationTime, .WeighingWaitCalibrationTime)
            Premtek.ControlMisc.SetNumericValue(nmuWeighingTolerance, .WeighingTolerance)
            Premtek.ControlMisc.SetNumericValue(nmuWeightDotMin, .WeighingWeightDotMin)
            Premtek.ControlMisc.SetNumericValue(nmuWeightDotMax, .WeighingWeightDotMax)

            If .WeighingEnableProductionRunFail = True Then
                cbEnableProductionRunFail.Checked = True
            Else
                cbEnableProductionRunFail.Checked = False
            End If
            Premtek.ControlMisc.SetNumericValue(nmuWeighingPulseTime, .WeighingPulseTimes)
            Premtek.ControlMisc.SetNumericValue(nmuWeighingCycleTime, .WeighingCycleTime)
            Premtek.ControlMisc.SetNumericValue(nmuWeighingBlanceSteadyTime, .WeighingBlanceSteadyTimes)
            Premtek.ControlMisc.SetNumericValue(nmuWeighingTableSteadyTime, .WeighingTableTimes)
            Premtek.ControlMisc.SetNumericValue(nmuWeighingPressure, .WeighingPressure)
            Premtek.ControlMisc.SetNumericValue(nmuWeighingGetBalance, .WeighingGetBalance)
         

            If .WeighingWeight < (.WeighingPointNumber * .WeighingWeightDotMin) / 4 Then
                'MsgBox("設定目標重量異常")
                .WeighingWeight = (.WeighingPointNumber * .WeighingWeightDotMin) / 4
            End If
            Premtek.ControlMisc.SetNumericValue(nmuWeighingWeight, .WeighingWeight)
        End With

        Return True
    End Function


    ''' <summary>更新選單</summary>
    ''' <remarks></remarks>
    Public Sub WeightDB_Update(ByVal sysParam As sSysParam)
        'Jeffadd 20160726
        lstWeighDB.Items.Clear()
        For mI As Integer = 0 To gFlowRateDB.Count - 1
            lstWeighDB.Items.Add(gFlowRateDB.Keys(mI))
            '[說明]顯示No1的資料
            If gFlowRateDB.Keys(mI) = gCRecipe.StageParts(sysParam.StageNo).FlowRateName(sysParam.SelectValve) Then
                SetWeightDB(gFlowRateDB(gCRecipe.StageParts(sysParam.StageNo).FlowRateName(sysParam.SelectValve)))
                txtWeightDB.Text = gCRecipe.StageParts(sysParam.StageNo).FlowRateName(sysParam.SelectValve)
                lstWeighDB.SelectedIndex = mI
            End If
        Next
    End Sub

    '20161114
    ''' <summary>更新選單</summary>
    ''' <remarks></remarks>
    Public Sub WeightDB_UpdateAddData(ByRef NewName As String)
        lstWeighDB.Items.Clear()

        For i As Integer = 0 To gFlowRateDB.Count - 1
            lstWeighDB.Items.Add(gFlowRateDB.Keys(i))
        Next

        '[說明]顯示點擊的資料
        For i As Integer = 0 To gFlowRateDB.Count - 1
            If gFlowRateDB.Keys(i) = NewName Then
                SetWeightDB(gFlowRateDB(NewName))

                txtWeightDB.Text = NewName
                lstWeighDB.SelectedIndex = i
            End If
        Next
    End Sub

    ''' <summary>更新選單</summary>
    ''' <remarks></remarks>
    Public Sub WeightDB_UpdateDel()
        'Jeffadd 20160726
        lstWeighDB.Items.Clear()

        For i As Integer = 0 To gFlowRateDB.Count - 1
            lstWeighDB.Items.Add(gFlowRateDB.Keys(i))
        Next

        txtWeightDB.Text = ""
        lstWeighDB.SelectedIndex = 0
    End Sub

    Private Sub btnWeightDBDel_Click(sender As Object, e As EventArgs) Handles btnWeightDBDel.Click
        If lstWeighDB.SelectedIndex < 0 Then
            lstWeighDB.BackColor = Color.Yellow
            lstWeighDB.Refresh() ''Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstWeighDB.BackColor = Color.White
            Exit Sub
        End If

        Dim folderPath As String = Application.StartupPath & "\Database\Weight\" '噴射閥資料
        Dim fileName As String = folderPath & lstWeighDB.SelectedItem & ".wdb"
        If System.IO.File.Exists(fileName) Then
            System.IO.File.Delete(fileName)
        End If
        gFlowRateDB.Remove(lstWeighDB.SelectedItem)
        WeightDB_UpdateDel()
    End Sub

    Private Sub btnWeightDBAdd_Click(sender As Object, e As EventArgs) Handles btnWeightDBAdd.Click
        'Jeffadd 20160726

        If txtWeightDB.Text.Trim = "" Then
            txtWeightDB.BackColor = Color.Yellow
            txtWeightDB.Refresh() ''Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstWeighDB.BackColor = Color.White
            Exit Sub
        End If
        Dim mWeightDB As New CFlowRateParameter("Default") 'Soni / 2017.04.26
        mWeightDB.Name = txtWeightDB.Text
        'If gWeightDB.ContainsKey(mWeightDB.Name) Then
        If gFlowRateDB.ContainsKey(txtWeightDB.Text) Then
            If MsgBox("Weight Data already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                lstWeighDB.SelectedItem = mWeightDB.Name
                GetWeightDB(gFlowRateDB(lstWeighDB.SelectedItem))
            End If
            Exit Sub
        End If

        If GetWeightDB(mWeightDB) Then
            gFlowRateDB.Add(mWeightDB.Name, mWeightDB)
            Dim folderPath As String = Application.StartupPath & "\Database\Weight\" '溫控資料
            Dim fileName As String = folderPath & mWeightDB.Name & ".wdb"
            If System.IO.File.Exists(fileName) Then
                If MsgBox("Weight File already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                    mWeightDB.Save(fileName)
                End If
            Else
                mWeightDB.Save(fileName)
            End If
            ' WeightDB_Update()

            WeightDB_UpdateAddData(mWeightDB.Name)
        End If
    End Sub

    Private Sub lstWeighDB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstWeighDB.SelectedIndexChanged
        If lstWeighDB.SelectedIndex < 0 Then
            lstWeighDB.BackColor = Color.Yellow
            lstWeighDB.Refresh() ''Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstWeighDB.BackColor = Color.White
            Exit Sub
        End If
        txtWeightDB.Text = lstWeighDB.SelectedItem
        SetWeightDB(gFlowRateDB(lstWeighDB.SelectedItem))
        '[說明]:暫時修改名稱 frm關閉後再Load回來   20161114
        gCRecipe.StageParts(sys.StageNo).FlowRateName(sys.SelectValve) = lstWeighDB.SelectedItem
    End Sub

    Private Sub frmWeight_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        'WeightDB_Update()   20161114
    End Sub


    Private Sub cbEnableAverageWeight_CheckedChanged(sender As Object, e As EventArgs) Handles cbEnableAverageWeight.CheckedChanged
        'txtWeighingWeight.Enabled = cbEnableAverageWeight.Checked

        'If cbEnableAverageWeight.Checked = True Or cbEnableAverageDot.Checked = True Then
        '    txtWeighingWaitCalibrationTime.Enabled = True
        '    txtWeighingTolerance.Enabled = True
        'Else
        '    txtWeighingWaitCalibrationTime.Enabled = False
        '    txtWeighingTolerance.Enabled = False
        'End If

    End Sub

    Private Sub cbEnableAverageDot_CheckedChanged_1(sender As Object, e As EventArgs) Handles cbEnableAverageDot.CheckedChanged
        nmuWeightDotMin.Enabled = cbEnableAverageDot.Checked
        nmuWeightDotMax.Enabled = cbEnableAverageDot.Checked

        'If cbEnableAverageWeight.Checked = True Or cbEnableAverageDot.Checked = True Then
        '    txtWeighingWaitCalibrationTime.Enabled = True
        '    txtWeighingTolerance.Enabled = True
        'Else
        '    txtWeighingWaitCalibrationTime.Enabled = False
        '    txtWeighingTolerance.Enabled = False
        'End If
    End Sub

    Private Sub btnWeighingCleanCount_Click(sender As Object, e As EventArgs) Handles btnWeighingStop.Click
        '[說明]:清除Runs Counter,Pase後Run不用在用之前餘留下的參數
        ' If MsgBox("Do you Want Clean Runs Counter ? ", MsgBoxStyle.YesNo, "Weighing Clean Counter Function") = MsgBoxResult.Yes Then

        gSYS(sys.EsysNum).Act(eAct.WeightUnit).RunStatus = enmRunStatus.Finish
        gSYS(sys.EsysNum).RunStatus = enmRunStatus.Finish

        'MFunctionWeight.WeightCounter = 0
        gWeight.InitialMFunctionWeight_WeightCounter(enmStage.Max) 'Eason 20170406
        '[說明]:一次Run寫一份檔案 Counter 數By Recipe 更新
        gWeight.SaveWeighingDataCounter(sys)


        'Dim Str As String
        'Str = "RunsCounter = " & CStr(WeightCounter)
        'MsgBox(Str)
        ' End If
    End Sub


    Private Sub btnGoTilt_Click(sender As Object, e As EventArgs) Handles btnGoTilt.Click
        'gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnGoCCDPos]" & vbTab & "Click")
        'Dim deg As Double = Format(CDbl(txtTiltValvePosB.Text), ".##")
        btnGoTilt.Enabled = False
        'With gSSystemParameter.Pos.CCDValveCalibration(sys.StageNo)

        Dim AxisNo(1) As Integer
        Dim TargetPos(1) As Decimal
        AxisNo(0) = sys.AxisZ
        AxisNo(1) = sys.AxisB
        TargetPos(0) = 0

        TargetPos(1) = CDec(Val(txtTiltValvePosB.Text))
        ButtonSafeMoveTiltPos(sender, AxisNo, TargetPos, sys)

        '  UcJoyStick1.RefreshPosition()
        btnGoTilt.Enabled = True
    End Sub
    Public Sub MoveTiltToSysparam()
        Dim AxisNo(1) As Integer
        Dim TargetPos(1) As Decimal
        AxisNo(0) = sys.AxisZ
        AxisNo(1) = sys.AxisB
        TargetPos(0) = 0

        TargetPos(1) = CDec(Val(txtTiltValvePosB.Text))
        ButtonSafeMoveTiltPos(Nothing, AxisNo, TargetPos, sys)
    End Sub

    
End Class
﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWeight
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWeight))
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.grpProductionWeighing = New System.Windows.Forms.GroupBox()
        Me.nmuWeighingPressure = New System.Windows.Forms.NumericUpDown()
        Me.nmuWeighingTableSteadyTime = New System.Windows.Forms.NumericUpDown()
        Me.nmuWeighingBlanceSteadyTime = New System.Windows.Forms.NumericUpDown()
        Me.nmuWeighingCycleTime = New System.Windows.Forms.NumericUpDown()
        Me.nmuWeighingPulseTime = New System.Windows.Forms.NumericUpDown()
        Me.nmuWeighingGetBalance = New System.Windows.Forms.NumericUpDown()
        Me.nmuWeighingWaitCalibrationTime = New System.Windows.Forms.NumericUpDown()
        Me.nmuWeighingTolerance = New System.Windows.Forms.NumericUpDown()
        Me.nmuWeighingWeight = New System.Windows.Forms.NumericUpDown()
        Me.nmuWeighingPointNumber = New System.Windows.Forms.NumericUpDown()
        Me.nmuWeightTimes = New System.Windows.Forms.NumericUpDown()
        Me.lblCPKData = New System.Windows.Forms.Label()
        Me.lblCPK = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cbEnableAverageWeight = New System.Windows.Forms.CheckBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.gpTiltMove = New System.Windows.Forms.GroupBox()
        Me.txtTiltValvePosB = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btnGoTilt = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.nmuWeightDotMax = New System.Windows.Forms.NumericUpDown()
        Me.nmuWeightDotMin = New System.Windows.Forms.NumericUpDown()
        Me.cbEnableAverageDot = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtWeightDB = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lstWeighDB = New System.Windows.Forms.ListBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.btnWeightDBAdd = New System.Windows.Forms.Button()
        Me.btnWeightDBDel = New System.Windows.Forms.Button()
        Me.cbEnableProductionRunFail = New System.Windows.Forms.CheckBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnWeighingStop = New System.Windows.Forms.Button()
        Me.btnWeighingRun = New System.Windows.Forms.Button()
        Me.butWeighingPause = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnWeightRequestStableValue = New System.Windows.Forms.Button()
        Me.btnWeightClean = New System.Windows.Forms.Button()
        Me.btnWeightLoad = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cbEnableWeightFunc = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Pressure = New System.Windows.Forms.Label()
        Me.chartDotsWeight = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.listWeight = New System.Windows.Forms.ListBox()
        Me.chartWeight = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblSelectBalance = New System.Windows.Forms.Label()
        Me.cmbBalance = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbValve = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblWeight = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.OFDLoadRecipe = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpProductionWeighing.SuspendLayout()
        CType(Me.nmuWeighingPressure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuWeighingTableSteadyTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuWeighingBlanceSteadyTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuWeighingCycleTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuWeighingPulseTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuWeighingGetBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuWeighingWaitCalibrationTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuWeighingTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuWeighingWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuWeighingPointNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuWeightTimes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpTiltMove.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.nmuWeightDotMax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuWeightDotMin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.chartDotsWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpProductionWeighing
        '
        Me.grpProductionWeighing.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.grpProductionWeighing.Controls.Add(Me.nmuWeighingPressure)
        Me.grpProductionWeighing.Controls.Add(Me.nmuWeighingTableSteadyTime)
        Me.grpProductionWeighing.Controls.Add(Me.nmuWeighingBlanceSteadyTime)
        Me.grpProductionWeighing.Controls.Add(Me.nmuWeighingCycleTime)
        Me.grpProductionWeighing.Controls.Add(Me.nmuWeighingPulseTime)
        Me.grpProductionWeighing.Controls.Add(Me.nmuWeighingGetBalance)
        Me.grpProductionWeighing.Controls.Add(Me.nmuWeighingWaitCalibrationTime)
        Me.grpProductionWeighing.Controls.Add(Me.nmuWeighingTolerance)
        Me.grpProductionWeighing.Controls.Add(Me.nmuWeighingWeight)
        Me.grpProductionWeighing.Controls.Add(Me.nmuWeighingPointNumber)
        Me.grpProductionWeighing.Controls.Add(Me.nmuWeightTimes)
        Me.grpProductionWeighing.Controls.Add(Me.lblCPKData)
        Me.grpProductionWeighing.Controls.Add(Me.lblCPK)
        Me.grpProductionWeighing.Controls.Add(Me.Label22)
        Me.grpProductionWeighing.Controls.Add(Me.UcStatusBar1)
        Me.grpProductionWeighing.Controls.Add(Me.Label20)
        Me.grpProductionWeighing.Controls.Add(Me.cbEnableAverageWeight)
        Me.grpProductionWeighing.Controls.Add(Me.Label28)
        Me.grpProductionWeighing.Controls.Add(Me.Label29)
        Me.grpProductionWeighing.Controls.Add(Me.gpTiltMove)
        Me.grpProductionWeighing.Controls.Add(Me.GroupBox4)
        Me.grpProductionWeighing.Controls.Add(Me.Label6)
        Me.grpProductionWeighing.Controls.Add(Me.txtWeightDB)
        Me.grpProductionWeighing.Controls.Add(Me.Label9)
        Me.grpProductionWeighing.Controls.Add(Me.lstWeighDB)
        Me.grpProductionWeighing.Controls.Add(Me.Label23)
        Me.grpProductionWeighing.Controls.Add(Me.btnWeightDBAdd)
        Me.grpProductionWeighing.Controls.Add(Me.btnWeightDBDel)
        Me.grpProductionWeighing.Controls.Add(Me.cbEnableProductionRunFail)
        Me.grpProductionWeighing.Controls.Add(Me.Label24)
        Me.grpProductionWeighing.Controls.Add(Me.Label21)
        Me.grpProductionWeighing.Controls.Add(Me.Label25)
        Me.grpProductionWeighing.Controls.Add(Me.GroupBox3)
        Me.grpProductionWeighing.Controls.Add(Me.GroupBox2)
        Me.grpProductionWeighing.Controls.Add(Me.btnClose)
        Me.grpProductionWeighing.Controls.Add(Me.Label17)
        Me.grpProductionWeighing.Controls.Add(Me.Label13)
        Me.grpProductionWeighing.Controls.Add(Me.Label14)
        Me.grpProductionWeighing.Controls.Add(Me.cbEnableWeightFunc)
        Me.grpProductionWeighing.Controls.Add(Me.Label11)
        Me.grpProductionWeighing.Controls.Add(Me.Label10)
        Me.grpProductionWeighing.Controls.Add(Me.Pressure)
        Me.grpProductionWeighing.Controls.Add(Me.chartDotsWeight)
        Me.grpProductionWeighing.Controls.Add(Me.btnSave)
        Me.grpProductionWeighing.Controls.Add(Me.listWeight)
        Me.grpProductionWeighing.Controls.Add(Me.chartWeight)
        Me.grpProductionWeighing.Controls.Add(Me.Label5)
        Me.grpProductionWeighing.Controls.Add(Me.Label3)
        Me.grpProductionWeighing.Controls.Add(Me.Label2)
        Me.grpProductionWeighing.Controls.Add(Me.Label8)
        Me.grpProductionWeighing.Controls.Add(Me.Label7)
        Me.grpProductionWeighing.Controls.Add(Me.lblSelectBalance)
        Me.grpProductionWeighing.Controls.Add(Me.cmbBalance)
        Me.grpProductionWeighing.Controls.Add(Me.Label1)
        Me.grpProductionWeighing.Controls.Add(Me.cmbValve)
        Me.grpProductionWeighing.Controls.Add(Me.Label12)
        Me.grpProductionWeighing.Controls.Add(Me.Label4)
        Me.grpProductionWeighing.Controls.Add(Me.Label16)
        Me.grpProductionWeighing.Controls.Add(Me.lblWeight)
        resources.ApplyResources(Me.grpProductionWeighing, "grpProductionWeighing")
        Me.grpProductionWeighing.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpProductionWeighing.Name = "grpProductionWeighing"
        Me.grpProductionWeighing.TabStop = False
        '
        'nmuWeighingPressure
        '
        Me.nmuWeighingPressure.DecimalPlaces = 2
        resources.ApplyResources(Me.nmuWeighingPressure, "nmuWeighingPressure")
        Me.nmuWeighingPressure.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmuWeighingPressure.Name = "nmuWeighingPressure"
        Me.nmuWeighingPressure.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmuWeighingTableSteadyTime
        '
        resources.ApplyResources(Me.nmuWeighingTableSteadyTime, "nmuWeighingTableSteadyTime")
        Me.nmuWeighingTableSteadyTime.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.nmuWeighingTableSteadyTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmuWeighingTableSteadyTime.Name = "nmuWeighingTableSteadyTime"
        Me.nmuWeighingTableSteadyTime.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'nmuWeighingBlanceSteadyTime
        '
        resources.ApplyResources(Me.nmuWeighingBlanceSteadyTime, "nmuWeighingBlanceSteadyTime")
        Me.nmuWeighingBlanceSteadyTime.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.nmuWeighingBlanceSteadyTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmuWeighingBlanceSteadyTime.Name = "nmuWeighingBlanceSteadyTime"
        Me.nmuWeighingBlanceSteadyTime.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'nmuWeighingCycleTime
        '
        Me.nmuWeighingCycleTime.DecimalPlaces = 2
        resources.ApplyResources(Me.nmuWeighingCycleTime, "nmuWeighingCycleTime")
        Me.nmuWeighingCycleTime.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nmuWeighingCycleTime.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmuWeighingCycleTime.Name = "nmuWeighingCycleTime"
        Me.nmuWeighingCycleTime.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nmuWeighingPulseTime
        '
        Me.nmuWeighingPulseTime.DecimalPlaces = 2
        resources.ApplyResources(Me.nmuWeighingPulseTime, "nmuWeighingPulseTime")
        Me.nmuWeighingPulseTime.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nmuWeighingPulseTime.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmuWeighingPulseTime.Name = "nmuWeighingPulseTime"
        Me.nmuWeighingPulseTime.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nmuWeighingGetBalance
        '
        resources.ApplyResources(Me.nmuWeighingGetBalance, "nmuWeighingGetBalance")
        Me.nmuWeighingGetBalance.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.nmuWeighingGetBalance.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmuWeighingGetBalance.Name = "nmuWeighingGetBalance"
        Me.nmuWeighingGetBalance.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nmuWeighingWaitCalibrationTime
        '
        resources.ApplyResources(Me.nmuWeighingWaitCalibrationTime, "nmuWeighingWaitCalibrationTime")
        Me.nmuWeighingWaitCalibrationTime.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.nmuWeighingWaitCalibrationTime.Name = "nmuWeighingWaitCalibrationTime"
        Me.nmuWeighingWaitCalibrationTime.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nmuWeighingTolerance
        '
        Me.nmuWeighingTolerance.DecimalPlaces = 3
        resources.ApplyResources(Me.nmuWeighingTolerance, "nmuWeighingTolerance")
        Me.nmuWeighingTolerance.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.nmuWeighingTolerance.Name = "nmuWeighingTolerance"
        Me.nmuWeighingTolerance.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nmuWeighingWeight
        '
        Me.nmuWeighingWeight.DecimalPlaces = 3
        resources.ApplyResources(Me.nmuWeighingWeight, "nmuWeighingWeight")
        Me.nmuWeighingWeight.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nmuWeighingWeight.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmuWeighingWeight.Name = "nmuWeighingWeight"
        Me.nmuWeighingWeight.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nmuWeighingPointNumber
        '
        resources.ApplyResources(Me.nmuWeighingPointNumber, "nmuWeighingPointNumber")
        Me.nmuWeighingPointNumber.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.nmuWeighingPointNumber.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmuWeighingPointNumber.Name = "nmuWeighingPointNumber"
        Me.nmuWeighingPointNumber.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmuWeightTimes
        '
        resources.ApplyResources(Me.nmuWeightTimes, "nmuWeightTimes")
        Me.nmuWeightTimes.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmuWeightTimes.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmuWeightTimes.Name = "nmuWeightTimes"
        Me.nmuWeightTimes.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblCPKData
        '
        Me.lblCPKData.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblCPKData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.lblCPKData, "lblCPKData")
        Me.lblCPKData.Name = "lblCPKData"
        '
        'lblCPK
        '
        resources.ApplyResources(Me.lblCPK, "lblCPK")
        Me.lblCPK.Name = "lblCPK"
        '
        'Label22
        '
        resources.ApplyResources(Me.Label22, "Label22")
        Me.Label22.Name = "Label22"
        '
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        '
        'Label20
        '
        resources.ApplyResources(Me.Label20, "Label20")
        Me.Label20.Name = "Label20"
        '
        'cbEnableAverageWeight
        '
        Me.cbEnableAverageWeight.Checked = True
        Me.cbEnableAverageWeight.CheckState = System.Windows.Forms.CheckState.Checked
        resources.ApplyResources(Me.cbEnableAverageWeight, "cbEnableAverageWeight")
        Me.cbEnableAverageWeight.Name = "cbEnableAverageWeight"
        Me.cbEnableAverageWeight.UseVisualStyleBackColor = True
        '
        'Label28
        '
        resources.ApplyResources(Me.Label28, "Label28")
        Me.Label28.Name = "Label28"
        '
        'Label29
        '
        resources.ApplyResources(Me.Label29, "Label29")
        Me.Label29.Name = "Label29"
        '
        'gpTiltMove
        '
        Me.gpTiltMove.Controls.Add(Me.txtTiltValvePosB)
        Me.gpTiltMove.Controls.Add(Me.Label26)
        Me.gpTiltMove.Controls.Add(Me.Label27)
        Me.gpTiltMove.Controls.Add(Me.btnGoTilt)
        resources.ApplyResources(Me.gpTiltMove, "gpTiltMove")
        Me.gpTiltMove.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gpTiltMove.Name = "gpTiltMove"
        Me.gpTiltMove.TabStop = False
        '
        'txtTiltValvePosB
        '
        Me.txtTiltValvePosB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtTiltValvePosB, "txtTiltValvePosB")
        Me.txtTiltValvePosB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtTiltValvePosB.Name = "txtTiltValvePosB"
        '
        'Label26
        '
        resources.ApplyResources(Me.Label26, "Label26")
        Me.Label26.Name = "Label26"
        '
        'Label27
        '
        resources.ApplyResources(Me.Label27, "Label27")
        Me.Label27.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label27.Name = "Label27"
        '
        'btnGoTilt
        '
        Me.btnGoTilt.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        resources.ApplyResources(Me.btnGoTilt, "btnGoTilt")
        Me.btnGoTilt.FlatAppearance.BorderSize = 0
        Me.btnGoTilt.Name = "btnGoTilt"
        Me.ToolTip1.SetToolTip(Me.btnGoTilt, resources.GetString("btnGoTilt.ToolTip"))
        Me.btnGoTilt.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.nmuWeightDotMax)
        Me.GroupBox4.Controls.Add(Me.nmuWeightDotMin)
        Me.GroupBox4.Controls.Add(Me.cbEnableAverageDot)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.Label19)
        resources.ApplyResources(Me.GroupBox4, "GroupBox4")
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.TabStop = False
        '
        'nmuWeightDotMax
        '
        Me.nmuWeightDotMax.DecimalPlaces = 3
        resources.ApplyResources(Me.nmuWeightDotMax, "nmuWeightDotMax")
        Me.nmuWeightDotMax.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.nmuWeightDotMax.Name = "nmuWeightDotMax"
        Me.nmuWeightDotMax.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nmuWeightDotMin
        '
        Me.nmuWeightDotMin.DecimalPlaces = 3
        resources.ApplyResources(Me.nmuWeightDotMin, "nmuWeightDotMin")
        Me.nmuWeightDotMin.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.nmuWeightDotMin.Name = "nmuWeightDotMin"
        Me.nmuWeightDotMin.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'cbEnableAverageDot
        '
        Me.cbEnableAverageDot.Checked = True
        Me.cbEnableAverageDot.CheckState = System.Windows.Forms.CheckState.Checked
        resources.ApplyResources(Me.cbEnableAverageDot, "cbEnableAverageDot")
        Me.cbEnableAverageDot.Name = "cbEnableAverageDot"
        Me.cbEnableAverageDot.UseVisualStyleBackColor = True
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.Name = "Label15"
        '
        'Label18
        '
        resources.ApplyResources(Me.Label18, "Label18")
        Me.Label18.Name = "Label18"
        '
        'Label19
        '
        resources.ApplyResources(Me.Label19, "Label19")
        Me.Label19.Name = "Label19"
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'txtWeightDB
        '
        resources.ApplyResources(Me.txtWeightDB, "txtWeightDB")
        Me.txtWeightDB.Name = "txtWeightDB"
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'lstWeighDB
        '
        resources.ApplyResources(Me.lstWeighDB, "lstWeighDB")
        Me.lstWeighDB.FormattingEnabled = True
        Me.lstWeighDB.Name = "lstWeighDB"
        '
        'Label23
        '
        resources.ApplyResources(Me.Label23, "Label23")
        Me.Label23.Name = "Label23"
        '
        'btnWeightDBAdd
        '
        resources.ApplyResources(Me.btnWeightDBAdd, "btnWeightDBAdd")
        Me.btnWeightDBAdd.Name = "btnWeightDBAdd"
        Me.btnWeightDBAdd.UseVisualStyleBackColor = True
        '
        'btnWeightDBDel
        '
        resources.ApplyResources(Me.btnWeightDBDel, "btnWeightDBDel")
        Me.btnWeightDBDel.Name = "btnWeightDBDel"
        Me.btnWeightDBDel.UseVisualStyleBackColor = True
        '
        'cbEnableProductionRunFail
        '
        Me.cbEnableProductionRunFail.Checked = True
        Me.cbEnableProductionRunFail.CheckState = System.Windows.Forms.CheckState.Checked
        resources.ApplyResources(Me.cbEnableProductionRunFail, "cbEnableProductionRunFail")
        Me.cbEnableProductionRunFail.Name = "cbEnableProductionRunFail"
        Me.cbEnableProductionRunFail.UseVisualStyleBackColor = True
        '
        'Label24
        '
        resources.ApplyResources(Me.Label24, "Label24")
        Me.Label24.Name = "Label24"
        '
        'Label21
        '
        resources.ApplyResources(Me.Label21, "Label21")
        Me.Label21.Name = "Label21"
        '
        'Label25
        '
        resources.ApplyResources(Me.Label25, "Label25")
        Me.Label25.Name = "Label25"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnWeighingStop)
        Me.GroupBox3.Controls.Add(Me.btnWeighingRun)
        Me.GroupBox3.Controls.Add(Me.butWeighingPause)
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        '
        'btnWeighingStop
        '
        Me.btnWeighingStop.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnWeighingStop, "btnWeighingStop")
        Me.btnWeighingStop.FlatAppearance.BorderSize = 0
        Me.btnWeighingStop.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnWeighingStop.Name = "btnWeighingStop"
        Me.ToolTip1.SetToolTip(Me.btnWeighingStop, resources.GetString("btnWeighingStop.ToolTip"))
        Me.btnWeighingStop.UseVisualStyleBackColor = True
        '
        'btnWeighingRun
        '
        Me.btnWeighingRun.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnWeighingRun, "btnWeighingRun")
        Me.btnWeighingRun.FlatAppearance.BorderSize = 0
        Me.btnWeighingRun.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnWeighingRun.Name = "btnWeighingRun"
        Me.ToolTip1.SetToolTip(Me.btnWeighingRun, resources.GetString("btnWeighingRun.ToolTip"))
        Me.btnWeighingRun.UseVisualStyleBackColor = True
        '
        'butWeighingPause
        '
        Me.butWeighingPause.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.butWeighingPause, "butWeighingPause")
        Me.butWeighingPause.FlatAppearance.BorderSize = 0
        Me.butWeighingPause.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.butWeighingPause.Name = "butWeighingPause"
        Me.ToolTip1.SetToolTip(Me.butWeighingPause, resources.GetString("butWeighingPause.ToolTip"))
        Me.butWeighingPause.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnWeightRequestStableValue)
        Me.GroupBox2.Controls.Add(Me.btnWeightClean)
        Me.GroupBox2.Controls.Add(Me.btnWeightLoad)
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'btnWeightRequestStableValue
        '
        resources.ApplyResources(Me.btnWeightRequestStableValue, "btnWeightRequestStableValue")
        Me.btnWeightRequestStableValue.Name = "btnWeightRequestStableValue"
        Me.ToolTip1.SetToolTip(Me.btnWeightRequestStableValue, resources.GetString("btnWeightRequestStableValue.ToolTip"))
        Me.btnWeightRequestStableValue.UseVisualStyleBackColor = True
        '
        'btnWeightClean
        '
        resources.ApplyResources(Me.btnWeightClean, "btnWeightClean")
        Me.btnWeightClean.Name = "btnWeightClean"
        Me.ToolTip1.SetToolTip(Me.btnWeightClean, resources.GetString("btnWeightClean.ToolTip"))
        Me.btnWeightClean.UseVisualStyleBackColor = True
        '
        'btnWeightLoad
        '
        Me.btnWeightLoad.AutoEllipsis = True
        resources.ApplyResources(Me.btnWeightLoad, "btnWeightLoad")
        Me.btnWeightLoad.FlatAppearance.BorderSize = 0
        Me.btnWeightLoad.Name = "btnWeightLoad"
        Me.ToolTip1.SetToolTip(Me.btnWeightLoad, resources.GetString("btnWeightLoad.ToolTip"))
        Me.btnWeightLoad.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.Image = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnClose.Name = "btnClose"
        Me.ToolTip1.SetToolTip(Me.btnClose, resources.GetString("btnClose.ToolTip"))
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label17
        '
        resources.ApplyResources(Me.Label17, "Label17")
        Me.Label17.Name = "Label17"
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        '
        'cbEnableWeightFunc
        '
        Me.cbEnableWeightFunc.Checked = True
        Me.cbEnableWeightFunc.CheckState = System.Windows.Forms.CheckState.Checked
        resources.ApplyResources(Me.cbEnableWeightFunc, "cbEnableWeightFunc")
        Me.cbEnableWeightFunc.Name = "cbEnableWeightFunc"
        Me.cbEnableWeightFunc.UseVisualStyleBackColor = True
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'Pressure
        '
        resources.ApplyResources(Me.Pressure, "Pressure")
        Me.Pressure.Name = "Pressure"
        '
        'chartDotsWeight
        '
        ChartArea1.AxisX.Title = "Run"
        ChartArea1.AxisY.Title = "Weight"
        ChartArea1.Name = "ChartArea1"
        Me.chartDotsWeight.ChartAreas.Add(ChartArea1)
        resources.ApplyResources(Me.chartDotsWeight, "chartDotsWeight")
        Me.chartDotsWeight.Name = "chartDotsWeight"
        Series1.ChartArea = "ChartArea1"
        Series1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Series1.Name = "Title1"
        Series1.ToolTip = "重量分布圖"
        Me.chartDotsWeight.Series.Add(Series1)
        Title1.Name = "Title1"
        Me.chartDotsWeight.Titles.Add(Title1)
        '
        'btnSave
        '
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.Image = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnSave.Name = "btnSave"
        Me.ToolTip1.SetToolTip(Me.btnSave, resources.GetString("btnSave.ToolTip"))
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'listWeight
        '
        resources.ApplyResources(Me.listWeight, "listWeight")
        Me.listWeight.FormattingEnabled = True
        Me.listWeight.Items.AddRange(New Object() {resources.GetString("listWeight.Items"), resources.GetString("listWeight.Items1"), resources.GetString("listWeight.Items2"), resources.GetString("listWeight.Items3"), resources.GetString("listWeight.Items4"), resources.GetString("listWeight.Items5"), resources.GetString("listWeight.Items6"), resources.GetString("listWeight.Items7"), resources.GetString("listWeight.Items8"), resources.GetString("listWeight.Items9"), resources.GetString("listWeight.Items10"), resources.GetString("listWeight.Items11"), resources.GetString("listWeight.Items12"), resources.GetString("listWeight.Items13"), resources.GetString("listWeight.Items14"), resources.GetString("listWeight.Items15"), resources.GetString("listWeight.Items16"), resources.GetString("listWeight.Items17"), resources.GetString("listWeight.Items18"), resources.GetString("listWeight.Items19"), resources.GetString("listWeight.Items20"), resources.GetString("listWeight.Items21"), resources.GetString("listWeight.Items22"), resources.GetString("listWeight.Items23"), resources.GetString("listWeight.Items24")})
        Me.listWeight.Name = "listWeight"
        '
        'chartWeight
        '
        ChartArea2.AxisX.Title = "Run"
        ChartArea2.AxisY.Title = "Weight"
        ChartArea2.Name = "ChartArea1"
        Me.chartWeight.ChartAreas.Add(ChartArea2)
        resources.ApplyResources(Me.chartWeight, "chartWeight")
        Me.chartWeight.Name = "chartWeight"
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Series2.Name = "Series1"
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Series3.Name = "Series2"
        Series4.ChartArea = "ChartArea1"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Series4.Name = "Series3"
        Me.chartWeight.Series.Add(Series2)
        Me.chartWeight.Series.Add(Series3)
        Me.chartWeight.Series.Add(Series4)
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'lblSelectBalance
        '
        resources.ApplyResources(Me.lblSelectBalance, "lblSelectBalance")
        Me.lblSelectBalance.Name = "lblSelectBalance"
        '
        'cmbBalance
        '
        Me.cmbBalance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cmbBalance, "cmbBalance")
        Me.cmbBalance.FormattingEnabled = True
        Me.cmbBalance.Items.AddRange(New Object() {resources.GetString("cmbBalance.Items"), resources.GetString("cmbBalance.Items1")})
        Me.cmbBalance.Name = "cmbBalance"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'cmbValve
        '
        Me.cmbValve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cmbValve, "cmbValve")
        Me.cmbValve.FormattingEnabled = True
        Me.cmbValve.Name = "cmbValve"
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'Label16
        '
        resources.ApplyResources(Me.Label16, "Label16")
        Me.Label16.Name = "Label16"
        '
        'lblWeight
        '
        Me.lblWeight.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.lblWeight, "lblWeight")
        Me.lblWeight.Name = "lblWeight"
        '
        'Timer1
        '
        '
        'OFDLoadRecipe
        '
        Me.OFDLoadRecipe.FileName = "OpenFileDialog1"
        '
        'frmWeight
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ControlBox = False
        Me.Controls.Add(Me.grpProductionWeighing)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWeight"
        Me.grpProductionWeighing.ResumeLayout(False)
        Me.grpProductionWeighing.PerformLayout()
        CType(Me.nmuWeighingPressure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuWeighingTableSteadyTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuWeighingBlanceSteadyTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuWeighingCycleTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuWeighingPulseTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuWeighingGetBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuWeighingWaitCalibrationTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuWeighingTolerance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuWeighingWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuWeighingPointNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuWeightTimes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpTiltMove.ResumeLayout(False)
        Me.gpTiltMove.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.nmuWeightDotMax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuWeightDotMin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.chartDotsWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartWeight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpProductionWeighing As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblWeight As System.Windows.Forms.Label
    Public WithEvents btnWeighingRun As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbValve As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelectBalance As System.Windows.Forms.Label
    Friend WithEvents cmbBalance As System.Windows.Forms.ComboBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnWeightRequestStableValue As System.Windows.Forms.Button
    Friend WithEvents btnWeightClean As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chartWeight As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents listWeight As System.Windows.Forms.ListBox
    Friend WithEvents chartDotsWeight As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents butWeighingPause As System.Windows.Forms.Button
    Friend WithEvents Pressure As System.Windows.Forms.Label
    Friend WithEvents OFDLoadRecipe As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnWeightLoad As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cbEnableWeightFunc As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbEnableAverageWeight As System.Windows.Forms.CheckBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cbEnableProductionRunFail As System.Windows.Forms.CheckBox
    Friend WithEvents txtWeightDB As System.Windows.Forms.TextBox
    Friend WithEvents lstWeighDB As System.Windows.Forms.ListBox
    Friend WithEvents btnWeightDBAdd As System.Windows.Forms.Button
    Friend WithEvents btnWeightDBDel As System.Windows.Forms.Button
    Public WithEvents btnWeighingStop As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cbEnableAverageDot As System.Windows.Forms.CheckBox
    Friend WithEvents gpTiltMove As System.Windows.Forms.GroupBox
    Friend WithEvents txtTiltValvePosB As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents btnGoTilt As System.Windows.Forms.Button
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblCPKData As System.Windows.Forms.Label
    Friend WithEvents lblCPK As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents nmuWeighingPressure As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuWeighingTableSteadyTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuWeighingBlanceSteadyTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuWeighingCycleTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuWeighingPulseTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuWeighingGetBalance As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuWeighingWaitCalibrationTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuWeighingTolerance As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuWeighingWeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuWeighingPointNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuWeightTimes As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuWeightDotMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuWeightDotMin As System.Windows.Forms.NumericUpDown
End Class

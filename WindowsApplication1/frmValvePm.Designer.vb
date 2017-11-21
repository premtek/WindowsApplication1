<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmValvePm
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
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnValvePmSetTemperature = New System.Windows.Forms.Button()
        Me.txtValvePmTemperature = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtValvePmPressure = New System.Windows.Forms.TextBox()
        Me.txtValvePmPointNumber = New System.Windows.Forms.TextBox()
        Me.btnValvePmSetSyringe1OnOff = New System.Windows.Forms.Button()
        Me.btnValvePmSetPressure = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnValvePmTrigger = New System.Windows.Forms.Button()
        Me.btnValvePmHeater = New System.Windows.Forms.Button()
        Me.btnValvePmPower = New System.Windows.Forms.Button()
        Me.btnValvePmPurge = New System.Windows.Forms.Button()
        Me.btnValvePmValveParameter = New System.Windows.Forms.Button()
        Me.gpbPm = New System.Windows.Forms.GroupBox()
        Me.labCloseTimeUnit = New System.Windows.Forms.Label()
        Me.txtValvePmCloseTime = New System.Windows.Forms.TextBox()
        Me.labCloseTime = New System.Windows.Forms.Label()
        Me.labOpenTimeUnit = New System.Windows.Forms.Label()
        Me.txtValvePmOpenTime = New System.Windows.Forms.TextBox()
        Me.labOpenTime = New System.Windows.Forms.Label()
        Me.labStrokeUnit = New System.Windows.Forms.Label()
        Me.txtValvePmStroke = New System.Windows.Forms.TextBox()
        Me.labStroke = New System.Windows.Forms.Label()
        Me.labCloseVoltUnit = New System.Windows.Forms.Label()
        Me.txtValvePmCloseVolt = New System.Windows.Forms.TextBox()
        Me.labCloseVolt = New System.Windows.Forms.Label()
        Me.labCycleTimeUnit = New System.Windows.Forms.Label()
        Me.txtValvePmCycleTime = New System.Windows.Forms.TextBox()
        Me.labCycleTime = New System.Windows.Forms.Label()
        Me.labPulseTimeUnit = New System.Windows.Forms.Label()
        Me.txtValvePmPulseTime = New System.Windows.Forms.TextBox()
        Me.labPulseTime = New System.Windows.Forms.Label()
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.GroupBox4.SuspendLayout()
        Me.gpbPm.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnValvePmSetTemperature)
        Me.GroupBox4.Controls.Add(Me.txtValvePmTemperature)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.txtValvePmPressure)
        Me.GroupBox4.Controls.Add(Me.txtValvePmPointNumber)
        Me.GroupBox4.Controls.Add(Me.btnValvePmSetSyringe1OnOff)
        Me.GroupBox4.Controls.Add(Me.btnValvePmSetPressure)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.lblCount)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.GroupBox4.Location = New System.Drawing.Point(194, 32)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(519, 272)
        Me.GroupBox4.TabIndex = 542
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Valve Calibration"
        '
        'btnValvePmSetTemperature
        '
        Me.btnValvePmSetTemperature.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnValvePmSetTemperature.Location = New System.Drawing.Point(330, 213)
        Me.btnValvePmSetTemperature.Name = "btnValvePmSetTemperature"
        Me.btnValvePmSetTemperature.Size = New System.Drawing.Size(70, 50)
        Me.btnValvePmSetTemperature.TabIndex = 548
        Me.btnValvePmSetTemperature.Text = "Set"
        Me.btnValvePmSetTemperature.UseVisualStyleBackColor = True
        '
        'txtValvePmTemperature
        '
        Me.txtValvePmTemperature.BackColor = System.Drawing.Color.White
        Me.txtValvePmTemperature.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.txtValvePmTemperature.Location = New System.Drawing.Point(294, 175)
        Me.txtValvePmTemperature.Name = "txtValvePmTemperature"
        Me.txtValvePmTemperature.Size = New System.Drawing.Size(90, 33)
        Me.txtValvePmTemperature.TabIndex = 547
        Me.txtValvePmTemperature.Text = "20"
        Me.txtValvePmTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label16.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label16.Location = New System.Drawing.Point(408, 175)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(99, 30)
        Me.Label16.TabIndex = 546
        Me.Label16.Text = "C"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label17.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label17.Location = New System.Drawing.Point(11, 175)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(275, 30)
        Me.Label17.TabIndex = 545
        Me.Label17.Text = "Temperature:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtValvePmPressure
        '
        Me.txtValvePmPressure.BackColor = System.Drawing.Color.White
        Me.txtValvePmPressure.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.txtValvePmPressure.Location = New System.Drawing.Point(294, 80)
        Me.txtValvePmPressure.Name = "txtValvePmPressure"
        Me.txtValvePmPressure.Size = New System.Drawing.Size(90, 33)
        Me.txtValvePmPressure.TabIndex = 543
        Me.txtValvePmPressure.Text = "0.1"
        Me.txtValvePmPressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtValvePmPointNumber
        '
        Me.txtValvePmPointNumber.BackColor = System.Drawing.Color.White
        Me.txtValvePmPointNumber.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.txtValvePmPointNumber.Location = New System.Drawing.Point(294, 38)
        Me.txtValvePmPointNumber.Name = "txtValvePmPointNumber"
        Me.txtValvePmPointNumber.Size = New System.Drawing.Size(90, 33)
        Me.txtValvePmPointNumber.TabIndex = 544
        Me.txtValvePmPointNumber.Text = "10"
        Me.txtValvePmPointNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnValvePmSetSyringe1OnOff
        '
        Me.btnValvePmSetSyringe1OnOff.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnValvePmSetSyringe1OnOff.Location = New System.Drawing.Point(411, 119)
        Me.btnValvePmSetSyringe1OnOff.Name = "btnValvePmSetSyringe1OnOff"
        Me.btnValvePmSetSyringe1OnOff.Size = New System.Drawing.Size(70, 50)
        Me.btnValvePmSetSyringe1OnOff.TabIndex = 0
        Me.btnValvePmSetSyringe1OnOff.Text = "On"
        Me.btnValvePmSetSyringe1OnOff.UseVisualStyleBackColor = True
        '
        'btnValvePmSetPressure
        '
        Me.btnValvePmSetPressure.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnValvePmSetPressure.Location = New System.Drawing.Point(330, 119)
        Me.btnValvePmSetPressure.Name = "btnValvePmSetPressure"
        Me.btnValvePmSetPressure.Size = New System.Drawing.Size(70, 50)
        Me.btnValvePmSetPressure.TabIndex = 1
        Me.btnValvePmSetPressure.Text = "Set"
        Me.btnValvePmSetPressure.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label10.Location = New System.Drawing.Point(407, 80)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(99, 30)
        Me.Label10.TabIndex = 522
        Me.Label10.Text = "MPa"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(10, 80)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(275, 30)
        Me.Label9.TabIndex = 519
        Me.Label9.Text = "Air Pressure:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCount
        '
        Me.lblCount.BackColor = System.Drawing.Color.Transparent
        Me.lblCount.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblCount.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblCount.Location = New System.Drawing.Point(6, 41)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(279, 30)
        Me.lblCount.TabIndex = 410
        Me.lblCount.Text = "Number of Dispenses:"
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(407, 41)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(99, 30)
        Me.Label8.TabIndex = 518
        Me.Label8.Text = "dot Num"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnValvePmTrigger
        '
        Me.btnValvePmTrigger.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnValvePmTrigger.FlatAppearance.BorderSize = 0
        Me.btnValvePmTrigger.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnValvePmTrigger.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnValvePmTrigger.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnValvePmTrigger.Location = New System.Drawing.Point(25, 301)
        Me.btnValvePmTrigger.Margin = New System.Windows.Forms.Padding(0)
        Me.btnValvePmTrigger.Name = "btnValvePmTrigger"
        Me.btnValvePmTrigger.Size = New System.Drawing.Size(140, 73)
        Me.btnValvePmTrigger.TabIndex = 541
        Me.btnValvePmTrigger.Text = "Trigger"
        Me.btnValvePmTrigger.UseVisualStyleBackColor = True
        '
        'btnValvePmHeater
        '
        Me.btnValvePmHeater.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnValvePmHeater.FlatAppearance.BorderSize = 0
        Me.btnValvePmHeater.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnValvePmHeater.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnValvePmHeater.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnValvePmHeater.Location = New System.Drawing.Point(25, 211)
        Me.btnValvePmHeater.Margin = New System.Windows.Forms.Padding(0)
        Me.btnValvePmHeater.Name = "btnValvePmHeater"
        Me.btnValvePmHeater.Size = New System.Drawing.Size(140, 73)
        Me.btnValvePmHeater.TabIndex = 540
        Me.btnValvePmHeater.Text = "Heater"
        Me.btnValvePmHeater.UseVisualStyleBackColor = True
        '
        'btnValvePmPower
        '
        Me.btnValvePmPower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnValvePmPower.FlatAppearance.BorderSize = 0
        Me.btnValvePmPower.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnValvePmPower.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnValvePmPower.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnValvePmPower.Location = New System.Drawing.Point(25, 41)
        Me.btnValvePmPower.Margin = New System.Windows.Forms.Padding(0)
        Me.btnValvePmPower.Name = "btnValvePmPower"
        Me.btnValvePmPower.Size = New System.Drawing.Size(140, 73)
        Me.btnValvePmPower.TabIndex = 539
        Me.btnValvePmPower.Text = "Power"
        Me.btnValvePmPower.UseVisualStyleBackColor = True
        '
        'btnValvePmPurge
        '
        Me.btnValvePmPurge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnValvePmPurge.FlatAppearance.BorderSize = 0
        Me.btnValvePmPurge.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnValvePmPurge.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnValvePmPurge.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnValvePmPurge.Location = New System.Drawing.Point(25, 127)
        Me.btnValvePmPurge.Margin = New System.Windows.Forms.Padding(0)
        Me.btnValvePmPurge.Name = "btnValvePmPurge"
        Me.btnValvePmPurge.Size = New System.Drawing.Size(140, 73)
        Me.btnValvePmPurge.TabIndex = 538
        Me.btnValvePmPurge.Text = "Purge"
        Me.btnValvePmPurge.UseVisualStyleBackColor = True
        '
        'btnValvePmValveParameter
        '
        Me.btnValvePmValveParameter.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        Me.btnValvePmValveParameter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnValvePmValveParameter.FlatAppearance.BorderSize = 0
        Me.btnValvePmValveParameter.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnValvePmValveParameter.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnValvePmValveParameter.Location = New System.Drawing.Point(25, 389)
        Me.btnValvePmValveParameter.Margin = New System.Windows.Forms.Padding(0)
        Me.btnValvePmValveParameter.Name = "btnValvePmValveParameter"
        Me.btnValvePmValveParameter.Size = New System.Drawing.Size(73, 73)
        Me.btnValvePmValveParameter.TabIndex = 537
        Me.btnValvePmValveParameter.UseVisualStyleBackColor = True
        '
        'gpbPm
        '
        Me.gpbPm.Controls.Add(Me.labCloseTimeUnit)
        Me.gpbPm.Controls.Add(Me.txtValvePmCloseTime)
        Me.gpbPm.Controls.Add(Me.labCloseTime)
        Me.gpbPm.Controls.Add(Me.labOpenTimeUnit)
        Me.gpbPm.Controls.Add(Me.txtValvePmOpenTime)
        Me.gpbPm.Controls.Add(Me.labOpenTime)
        Me.gpbPm.Controls.Add(Me.labStrokeUnit)
        Me.gpbPm.Controls.Add(Me.txtValvePmStroke)
        Me.gpbPm.Controls.Add(Me.labStroke)
        Me.gpbPm.Controls.Add(Me.labCloseVoltUnit)
        Me.gpbPm.Controls.Add(Me.txtValvePmCloseVolt)
        Me.gpbPm.Controls.Add(Me.labCloseVolt)
        Me.gpbPm.Controls.Add(Me.labCycleTimeUnit)
        Me.gpbPm.Controls.Add(Me.txtValvePmCycleTime)
        Me.gpbPm.Controls.Add(Me.labCycleTime)
        Me.gpbPm.Controls.Add(Me.labPulseTimeUnit)
        Me.gpbPm.Controls.Add(Me.txtValvePmPulseTime)
        Me.gpbPm.Controls.Add(Me.labPulseTime)
        Me.gpbPm.Controls.Add(Me.GroupBox4)
        Me.gpbPm.Controls.Add(Me.btnValvePmPurge)
        Me.gpbPm.Controls.Add(Me.btnValvePmTrigger)
        Me.gpbPm.Controls.Add(Me.btnValvePmPower)
        Me.gpbPm.Controls.Add(Me.btnValvePmValveParameter)
        Me.gpbPm.Controls.Add(Me.btnValvePmHeater)
        Me.gpbPm.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.gpbPm.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gpbPm.Location = New System.Drawing.Point(2, 12)
        Me.gpbPm.Name = "gpbPm"
        Me.gpbPm.Size = New System.Drawing.Size(840, 539)
        Me.gpbPm.TabIndex = 482
        Me.gpbPm.TabStop = False
        Me.gpbPm.Text = "Pm"
        '
        'labCloseTimeUnit
        '
        Me.labCloseTimeUnit.BackColor = System.Drawing.Color.Transparent
        Me.labCloseTimeUnit.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.labCloseTimeUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.labCloseTimeUnit.Location = New System.Drawing.Point(616, 494)
        Me.labCloseTimeUnit.Name = "labCloseTimeUnit"
        Me.labCloseTimeUnit.Size = New System.Drawing.Size(55, 30)
        Me.labCloseTimeUnit.TabIndex = 560
        Me.labCloseTimeUnit.Text = "ms"
        Me.labCloseTimeUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.labCloseTimeUnit.Visible = False
        '
        'txtValvePmCloseTime
        '
        Me.txtValvePmCloseTime.BackColor = System.Drawing.Color.White
        Me.txtValvePmCloseTime.Enabled = False
        Me.txtValvePmCloseTime.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.txtValvePmCloseTime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtValvePmCloseTime.Location = New System.Drawing.Point(530, 494)
        Me.txtValvePmCloseTime.Name = "txtValvePmCloseTime"
        Me.txtValvePmCloseTime.ReadOnly = True
        Me.txtValvePmCloseTime.Size = New System.Drawing.Size(80, 33)
        Me.txtValvePmCloseTime.TabIndex = 559
        Me.txtValvePmCloseTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtValvePmCloseTime.Visible = False
        '
        'labCloseTime
        '
        Me.labCloseTime.BackColor = System.Drawing.Color.Transparent
        Me.labCloseTime.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.labCloseTime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.labCloseTime.Location = New System.Drawing.Point(190, 494)
        Me.labCloseTime.Name = "labCloseTime"
        Me.labCloseTime.Size = New System.Drawing.Size(316, 30)
        Me.labCloseTime.TabIndex = 558
        Me.labCloseTime.Text = "Close Time"
        Me.labCloseTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.labCloseTime.Visible = False
        '
        'labOpenTimeUnit
        '
        Me.labOpenTimeUnit.BackColor = System.Drawing.Color.Transparent
        Me.labOpenTimeUnit.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.labOpenTimeUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.labOpenTimeUnit.Location = New System.Drawing.Point(616, 458)
        Me.labOpenTimeUnit.Name = "labOpenTimeUnit"
        Me.labOpenTimeUnit.Size = New System.Drawing.Size(55, 30)
        Me.labOpenTimeUnit.TabIndex = 557
        Me.labOpenTimeUnit.Text = "ms"
        Me.labOpenTimeUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.labOpenTimeUnit.Visible = False
        '
        'txtValvePmOpenTime
        '
        Me.txtValvePmOpenTime.BackColor = System.Drawing.Color.White
        Me.txtValvePmOpenTime.Enabled = False
        Me.txtValvePmOpenTime.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.txtValvePmOpenTime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtValvePmOpenTime.Location = New System.Drawing.Point(530, 458)
        Me.txtValvePmOpenTime.Name = "txtValvePmOpenTime"
        Me.txtValvePmOpenTime.ReadOnly = True
        Me.txtValvePmOpenTime.Size = New System.Drawing.Size(80, 33)
        Me.txtValvePmOpenTime.TabIndex = 556
        Me.txtValvePmOpenTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtValvePmOpenTime.Visible = False
        '
        'labOpenTime
        '
        Me.labOpenTime.BackColor = System.Drawing.Color.Transparent
        Me.labOpenTime.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.labOpenTime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.labOpenTime.Location = New System.Drawing.Point(190, 458)
        Me.labOpenTime.Name = "labOpenTime"
        Me.labOpenTime.Size = New System.Drawing.Size(316, 30)
        Me.labOpenTime.TabIndex = 555
        Me.labOpenTime.Text = "Open Time"
        Me.labOpenTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.labOpenTime.Visible = False
        '
        'labStrokeUnit
        '
        Me.labStrokeUnit.BackColor = System.Drawing.Color.Transparent
        Me.labStrokeUnit.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.labStrokeUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.labStrokeUnit.Location = New System.Drawing.Point(616, 422)
        Me.labStrokeUnit.Name = "labStrokeUnit"
        Me.labStrokeUnit.Size = New System.Drawing.Size(55, 30)
        Me.labStrokeUnit.TabIndex = 554
        Me.labStrokeUnit.Text = "%"
        Me.labStrokeUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.labStrokeUnit.Visible = False
        '
        'txtValvePmStroke
        '
        Me.txtValvePmStroke.BackColor = System.Drawing.Color.White
        Me.txtValvePmStroke.Enabled = False
        Me.txtValvePmStroke.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.txtValvePmStroke.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtValvePmStroke.Location = New System.Drawing.Point(530, 422)
        Me.txtValvePmStroke.Name = "txtValvePmStroke"
        Me.txtValvePmStroke.ReadOnly = True
        Me.txtValvePmStroke.Size = New System.Drawing.Size(80, 33)
        Me.txtValvePmStroke.TabIndex = 553
        Me.txtValvePmStroke.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtValvePmStroke.Visible = False
        '
        'labStroke
        '
        Me.labStroke.BackColor = System.Drawing.Color.Transparent
        Me.labStroke.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.labStroke.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.labStroke.Location = New System.Drawing.Point(190, 422)
        Me.labStroke.Name = "labStroke"
        Me.labStroke.Size = New System.Drawing.Size(316, 30)
        Me.labStroke.TabIndex = 552
        Me.labStroke.Text = "Stroke"
        Me.labStroke.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.labStroke.Visible = False
        '
        'labCloseVoltUnit
        '
        Me.labCloseVoltUnit.BackColor = System.Drawing.Color.Transparent
        Me.labCloseVoltUnit.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.labCloseVoltUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.labCloseVoltUnit.Location = New System.Drawing.Point(616, 386)
        Me.labCloseVoltUnit.Name = "labCloseVoltUnit"
        Me.labCloseVoltUnit.Size = New System.Drawing.Size(55, 30)
        Me.labCloseVoltUnit.TabIndex = 551
        Me.labCloseVoltUnit.Text = "V"
        Me.labCloseVoltUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.labCloseVoltUnit.Visible = False
        '
        'txtValvePmCloseVolt
        '
        Me.txtValvePmCloseVolt.BackColor = System.Drawing.Color.White
        Me.txtValvePmCloseVolt.Enabled = False
        Me.txtValvePmCloseVolt.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.txtValvePmCloseVolt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtValvePmCloseVolt.Location = New System.Drawing.Point(530, 386)
        Me.txtValvePmCloseVolt.Name = "txtValvePmCloseVolt"
        Me.txtValvePmCloseVolt.ReadOnly = True
        Me.txtValvePmCloseVolt.Size = New System.Drawing.Size(80, 33)
        Me.txtValvePmCloseVolt.TabIndex = 550
        Me.txtValvePmCloseVolt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtValvePmCloseVolt.Visible = False
        '
        'labCloseVolt
        '
        Me.labCloseVolt.BackColor = System.Drawing.Color.Transparent
        Me.labCloseVolt.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.labCloseVolt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.labCloseVolt.Location = New System.Drawing.Point(190, 386)
        Me.labCloseVolt.Name = "labCloseVolt"
        Me.labCloseVolt.Size = New System.Drawing.Size(316, 30)
        Me.labCloseVolt.TabIndex = 549
        Me.labCloseVolt.Text = "CloseVolt"
        Me.labCloseVolt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.labCloseVolt.Visible = False
        '
        'labCycleTimeUnit
        '
        Me.labCycleTimeUnit.BackColor = System.Drawing.Color.Transparent
        Me.labCycleTimeUnit.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.labCycleTimeUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.labCycleTimeUnit.Location = New System.Drawing.Point(616, 347)
        Me.labCycleTimeUnit.Name = "labCycleTimeUnit"
        Me.labCycleTimeUnit.Size = New System.Drawing.Size(55, 30)
        Me.labCycleTimeUnit.TabIndex = 548
        Me.labCycleTimeUnit.Text = "ms"
        Me.labCycleTimeUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.labCycleTimeUnit.Visible = False
        '
        'txtValvePmCycleTime
        '
        Me.txtValvePmCycleTime.BackColor = System.Drawing.Color.White
        Me.txtValvePmCycleTime.Enabled = False
        Me.txtValvePmCycleTime.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.txtValvePmCycleTime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtValvePmCycleTime.Location = New System.Drawing.Point(530, 347)
        Me.txtValvePmCycleTime.Name = "txtValvePmCycleTime"
        Me.txtValvePmCycleTime.ReadOnly = True
        Me.txtValvePmCycleTime.Size = New System.Drawing.Size(80, 33)
        Me.txtValvePmCycleTime.TabIndex = 547
        Me.txtValvePmCycleTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtValvePmCycleTime.Visible = False
        '
        'labCycleTime
        '
        Me.labCycleTime.BackColor = System.Drawing.Color.Transparent
        Me.labCycleTime.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.labCycleTime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.labCycleTime.Location = New System.Drawing.Point(190, 347)
        Me.labCycleTime.Name = "labCycleTime"
        Me.labCycleTime.Size = New System.Drawing.Size(316, 30)
        Me.labCycleTime.TabIndex = 546
        Me.labCycleTime.Text = "Cycle Time(ms)"
        Me.labCycleTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.labCycleTime.Visible = False
        '
        'labPulseTimeUnit
        '
        Me.labPulseTimeUnit.BackColor = System.Drawing.Color.Transparent
        Me.labPulseTimeUnit.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.labPulseTimeUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.labPulseTimeUnit.Location = New System.Drawing.Point(616, 310)
        Me.labPulseTimeUnit.Name = "labPulseTimeUnit"
        Me.labPulseTimeUnit.Size = New System.Drawing.Size(55, 30)
        Me.labPulseTimeUnit.TabIndex = 545
        Me.labPulseTimeUnit.Text = "ms"
        Me.labPulseTimeUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.labPulseTimeUnit.Visible = False
        '
        'txtValvePmPulseTime
        '
        Me.txtValvePmPulseTime.BackColor = System.Drawing.Color.White
        Me.txtValvePmPulseTime.Enabled = False
        Me.txtValvePmPulseTime.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.txtValvePmPulseTime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtValvePmPulseTime.Location = New System.Drawing.Point(530, 310)
        Me.txtValvePmPulseTime.Name = "txtValvePmPulseTime"
        Me.txtValvePmPulseTime.ReadOnly = True
        Me.txtValvePmPulseTime.Size = New System.Drawing.Size(80, 33)
        Me.txtValvePmPulseTime.TabIndex = 544
        Me.txtValvePmPulseTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtValvePmPulseTime.Visible = False
        '
        'labPulseTime
        '
        Me.labPulseTime.BackColor = System.Drawing.Color.Transparent
        Me.labPulseTime.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.labPulseTime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.labPulseTime.Location = New System.Drawing.Point(190, 310)
        Me.labPulseTime.Name = "labPulseTime"
        Me.labPulseTime.Size = New System.Drawing.Size(316, 30)
        Me.labPulseTime.TabIndex = 543
        Me.labPulseTime.Text = "Pulse Time(ms)"
        Me.labPulseTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.labPulseTime.Visible = False
        '
        'UcJoyStick1
        '
        Me.UcJoyStick1.AXisA = 0
        Me.UcJoyStick1.AXisB = 0
        Me.UcJoyStick1.AXisC = 0
        Me.UcJoyStick1.AxisX = 0
        Me.UcJoyStick1.AxisY = 0
        Me.UcJoyStick1.AxisZ = 0
        Me.UcJoyStick1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcJoyStick1.Location = New System.Drawing.Point(867, 12)
        Me.UcJoyStick1.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.UcJoyStick1.Name = "UcJoyStick1"
        Me.UcJoyStick1.Size = New System.Drawing.Size(275, 374)
        Me.UcJoyStick1.TabIndex = 480
        '
        'frmValvePm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(1179, 563)
        Me.Controls.Add(Me.gpbPm)
        Me.Controls.Add(Me.UcJoyStick1)
        Me.Name = "frmValvePm"
        Me.Text = "frmValvePm"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.gpbPm.ResumeLayout(False)
        Me.gpbPm.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcJoyStick1 As WindowsApplication1.ucJoyStick
    Friend WithEvents gpbPm As System.Windows.Forms.GroupBox
    Friend WithEvents btnValvePmValveParameter As System.Windows.Forms.Button
    Friend WithEvents btnValvePmTrigger As System.Windows.Forms.Button
    Friend WithEvents btnValvePmHeater As System.Windows.Forms.Button
    Friend WithEvents btnValvePmPower As System.Windows.Forms.Button
    Friend WithEvents btnValvePmPurge As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnValvePmSetSyringe1OnOff As System.Windows.Forms.Button
    Friend WithEvents btnValvePmSetPressure As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtValvePmPressure As System.Windows.Forms.TextBox
    Friend WithEvents txtValvePmPointNumber As System.Windows.Forms.TextBox
    Friend WithEvents labPulseTimeUnit As System.Windows.Forms.Label
    Friend WithEvents txtValvePmPulseTime As System.Windows.Forms.TextBox
    Friend WithEvents labPulseTime As System.Windows.Forms.Label
    Friend WithEvents labCloseTimeUnit As System.Windows.Forms.Label
    Friend WithEvents txtValvePmCloseTime As System.Windows.Forms.TextBox
    Friend WithEvents labCloseTime As System.Windows.Forms.Label
    Friend WithEvents labOpenTimeUnit As System.Windows.Forms.Label
    Friend WithEvents txtValvePmOpenTime As System.Windows.Forms.TextBox
    Friend WithEvents labOpenTime As System.Windows.Forms.Label
    Friend WithEvents labStrokeUnit As System.Windows.Forms.Label
    Friend WithEvents txtValvePmStroke As System.Windows.Forms.TextBox
    Friend WithEvents labStroke As System.Windows.Forms.Label
    Friend WithEvents labCloseVoltUnit As System.Windows.Forms.Label
    Friend WithEvents txtValvePmCloseVolt As System.Windows.Forms.TextBox
    Friend WithEvents labCloseVolt As System.Windows.Forms.Label
    Friend WithEvents labCycleTimeUnit As System.Windows.Forms.Label
    Friend WithEvents txtValvePmCycleTime As System.Windows.Forms.TextBox
    Friend WithEvents labCycleTime As System.Windows.Forms.Label
    Friend WithEvents btnValvePmSetTemperature As System.Windows.Forms.Button
    Friend WithEvents txtValvePmTemperature As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSystemSet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSystemSet))
        Me.gpbFunction = New System.Windows.Forms.GroupBox()
        Me.cboxMapDataModel = New System.Windows.Forms.ComboBox()
        Me.lblCleanMachine = New System.Windows.Forms.Label()
        Me.btnCleanDevice = New System.Windows.Forms.Button()
        Me.lblContinueRun = New System.Windows.Forms.Label()
        Me.btnContinueRun = New System.Windows.Forms.Button()
        Me.btnManualMap = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.btnPassLUL = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblConveyor = New System.Windows.Forms.Label()
        Me.btnDisableConveyor = New System.Windows.Forms.Button()
        Me.btnPreviousPage = New System.Windows.Forms.Button()
        Me.gpbAugerValveCTThreshold = New System.Windows.Forms.GroupBox()
        Me.lblValve4CTUnit = New System.Windows.Forms.Label()
        Me.lblValve2CTUnit = New System.Windows.Forms.Label()
        Me.lblValve3CTUnit = New System.Windows.Forms.Label()
        Me.lblValve1CTUnit = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnAugerValveCTThreshold = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblScrewValveNo2CTThreshold = New System.Windows.Forms.Label()
        Me.txtAugerValveCTThreshold3 = New System.Windows.Forms.TextBox()
        Me.lblScrewValveNo1CTThreshold = New System.Windows.Forms.Label()
        Me.txtAugerValveCTThreshold4 = New System.Windows.Forms.TextBox()
        Me.txtAugerValveCTThreshold1 = New System.Windows.Forms.TextBox()
        Me.txtAugerValveCTThreshold2 = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabMechanismModule = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnSaveMachineType = New System.Windows.Forms.Button()
        Me.cboMachineType = New System.Windows.Forms.ComboBox()
        Me.lblMachineType = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.gpbMechanismSoftware = New System.Windows.Forms.GroupBox()
        Me.cboConveyorModel = New System.Windows.Forms.ComboBox()
        Me.lblConveyorModel = New System.Windows.Forms.Label()
        Me.cboCCDOnFly = New System.Windows.Forms.ComboBox()
        Me.lblCCDOnFly = New System.Windows.Forms.Label()
        Me.cboMultiDispense = New System.Windows.Forms.ComboBox()
        Me.lblMultiDispense = New System.Windows.Forms.Label()
        Me.cboZHeightDevice = New System.Windows.Forms.ComboBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btnMechanismModule = New System.Windows.Forms.Button()
        Me.lblCCDModuleType = New System.Windows.Forms.Label()
        Me.cboCCDModuleType = New System.Windows.Forms.ComboBox()
        Me.lblStageUseValveCount = New System.Windows.Forms.Label()
        Me.cboStageUseValveCount = New System.Windows.Forms.ComboBox()
        Me.gpbValveType = New System.Windows.Forms.GroupBox()
        Me.cboDispenserNo4ValveTypeModel = New System.Windows.Forms.ComboBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.cboDispenserNo3ValveTypeModel = New System.Windows.Forms.ComboBox()
        Me.cboDispenserNo2ValveTypeModel = New System.Windows.Forms.ComboBox()
        Me.cboDispenserNo1ValveTypeModel = New System.Windows.Forms.ComboBox()
        Me.cboDispenserNo4ValveType = New System.Windows.Forms.ComboBox()
        Me.lblDispenserNo4ValveType = New System.Windows.Forms.Label()
        Me.cboDispenserNo3ValveType = New System.Windows.Forms.ComboBox()
        Me.lblDispenserNo3ValveType = New System.Windows.Forms.Label()
        Me.cboDispenserNo2ValveType = New System.Windows.Forms.ComboBox()
        Me.lblDispenserNo2ValveType = New System.Windows.Forms.Label()
        Me.cboDispenserNo1ValveType = New System.Windows.Forms.ComboBox()
        Me.lblDispenserNo1ValveType = New System.Windows.Forms.Label()
        Me.btnValveType = New System.Windows.Forms.Button()
        Me.tabBasicFunction = New System.Windows.Forms.TabPage()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.btnMapPath = New System.Windows.Forms.Button()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtMapDataPath = New System.Windows.Forms.TextBox()
        Me.btnSetMapPath = New System.Windows.Forms.Button()
        Me.btnBack3 = New System.Windows.Forms.Button()
        Me.grpAirPressureUnit = New System.Windows.Forms.GroupBox()
        Me.rdbTorr = New System.Windows.Forms.RadioButton()
        Me.rdbBar = New System.Windows.Forms.RadioButton()
        Me.rdbPsi = New System.Windows.Forms.RadioButton()
        Me.rdbKg = New System.Windows.Forms.RadioButton()
        Me.rdbMPa = New System.Windows.Forms.RadioButton()
        Me.btnSetAirPressureUnit = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rdbInch = New System.Windows.Forms.RadioButton()
        Me.rdbmm = New System.Windows.Forms.RadioButton()
        Me.btnSetPositionUnit = New System.Windows.Forms.Button()
        Me.grpCCDImage = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbCCDImageSaveMode = New System.Windows.Forms.ComboBox()
        Me.btnCCDImagePath = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCCDImageFolderPath = New System.Windows.Forms.TextBox()
        Me.btnSaveImage = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnLogPath = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtLogPath = New System.Windows.Forms.TextBox()
        Me.txtMachineID = New System.Windows.Forms.TextBox()
        Me.btnSetMachine = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.lblLanguageType = New System.Windows.Forms.Label()
        Me.btnSaveLanguage = New System.Windows.Forms.Button()
        Me.cboLanguageType = New System.Windows.Forms.ComboBox()
        Me.tabCommonFunction = New System.Windows.Forms.TabPage()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.txtPathMultiple = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.btnExtendOn = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtMaxDispVelocity = New System.Windows.Forms.TextBox()
        Me.txtMaxCrossDevieVelocity = New System.Windows.Forms.TextBox()
        Me.txtMaxCrossStepVelocity = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.btnSetVelocity = New System.Windows.Forms.Button()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtConstVelocityTime = New System.Windows.Forms.TextBox()
        Me.grpConveyor = New System.Windows.Forms.GroupBox()
        Me.btnSetCvDirection = New System.Windows.Forms.Button()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.rbtnUnloadReversal = New System.Windows.Forms.RadioButton()
        Me.rbtnUnloadForward = New System.Windows.Forms.RadioButton()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.rbtnLoadReversal = New System.Windows.Forms.RadioButton()
        Me.rbtnLoadForward = New System.Windows.Forms.RadioButton()
        Me.gpbTolerance = New System.Windows.Forms.GroupBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtPrecisionTolerance = New System.Windows.Forms.TextBox()
        Me.lblTemperatureToleranceUnit = New System.Windows.Forms.Label()
        Me.lblTemperatureTolerance = New System.Windows.Forms.Label()
        Me.txtTemperatureTolerance = New System.Windows.Forms.TextBox()
        Me.lblPressureToleranceUnit = New System.Windows.Forms.Label()
        Me.lblPressureTolerance = New System.Windows.Forms.Label()
        Me.txtPressureTolerance = New System.Windows.Forms.TextBox()
        Me.lblTriggerToleranceUnit = New System.Windows.Forms.Label()
        Me.lblMotionToleranceUnit = New System.Windows.Forms.Label()
        Me.btnTolerance = New System.Windows.Forms.Button()
        Me.lblTirggerTolerance = New System.Windows.Forms.Label()
        Me.lblMotionTolerance = New System.Windows.Forms.Label()
        Me.txtMotionTolerance = New System.Windows.Forms.TextBox()
        Me.txtTirggerTolerance = New System.Windows.Forms.TextBox()
        Me.gpTiltSet = New System.Windows.Forms.GroupBox()
        Me.rb2 = New System.Windows.Forms.RadioButton()
        Me.rb3 = New System.Windows.Forms.RadioButton()
        Me.rb4 = New System.Windows.Forms.RadioButton()
        Me.rb1 = New System.Windows.Forms.RadioButton()
        Me.btnGoTilt = New System.Windows.Forms.Button()
        Me.lbTiltStage4 = New System.Windows.Forms.Label()
        Me.lbTiltStage3 = New System.Windows.Forms.Label()
        Me.lbTiltStage2 = New System.Windows.Forms.Label()
        Me.lbTiltStage1 = New System.Windows.Forms.Label()
        Me.cboB4 = New System.Windows.Forms.ComboBox()
        Me.btnSetTilt = New System.Windows.Forms.Button()
        Me.cboB3 = New System.Windows.Forms.ComboBox()
        Me.cboB2 = New System.Windows.Forms.ComboBox()
        Me.cboB1 = New System.Windows.Forms.ComboBox()
        Me.grpWeight = New System.Windows.Forms.GroupBox()
        Me.labCorrectionmg4 = New System.Windows.Forms.Label()
        Me.txtCorrectionWeightLower = New System.Windows.Forms.TextBox()
        Me.labCorrectionmg3 = New System.Windows.Forms.Label()
        Me.txtCorrectionWeightUpper = New System.Windows.Forms.TextBox()
        Me.labWeightLower = New System.Windows.Forms.Label()
        Me.labWeightUpper = New System.Windows.Forms.Label()
        Me.labCorrectionmg2 = New System.Windows.Forms.Label()
        Me.txtCorrectionLSL = New System.Windows.Forms.TextBox()
        Me.labCorrectionmg1 = New System.Windows.Forms.Label()
        Me.txtCorrectionUSL = New System.Windows.Forms.TextBox()
        Me.labCorrectionLSL = New System.Windows.Forms.Label()
        Me.btnCorrectionSet = New System.Windows.Forms.Button()
        Me.labCorrectionUSL = New System.Windows.Forms.Label()
        Me.labCorrection = New System.Windows.Forms.Label()
        Me.txtCorrectionSet = New System.Windows.Forms.TextBox()
        Me.cbCorrection = New System.Windows.Forms.CheckBox()
        Me.cbNonCorrection = New System.Windows.Forms.CheckBox()
        Me.btnWeightSet = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtWeighingSet = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cbEnableBWeightMeasure = New System.Windows.Forms.CheckBox()
        Me.btnCleanWeightB = New System.Windows.Forms.Button()
        Me.txtWeightmeasurementB = New System.Windows.Forms.TextBox()
        Me.cbEnableAWeightMeasure = New System.Windows.Forms.CheckBox()
        Me.btnCleanWeightA = New System.Windows.Forms.Button()
        Me.txtWeightmeasurementA = New System.Windows.Forms.TextBox()
        Me.grpStableTime = New System.Windows.Forms.GroupBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.nmcPriorHeatTime = New System.Windows.Forms.NumericUpDown()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.nmcSensorTimeOut = New System.Windows.Forms.NumericUpDown()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.nmcLaserStableTime = New System.Windows.Forms.NumericUpDown()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btnSetTime = New System.Windows.Forms.Button()
        Me.nmcCCDStableTime = New System.Windows.Forms.NumericUpDown()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.grpSafeTemperature = New System.Windows.Forms.GroupBox()
        Me.cbEnableInitialHotPlate = New System.Windows.Forms.CheckBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.nmcMinHotplateTemp = New System.Windows.Forms.NumericUpDown()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.nmcMaxHotplateTemp = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.nmcMaxValveTemp = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnSetSafeTemp = New System.Windows.Forms.Button()
        Me.nmcSafeTemperature = New System.Windows.Forms.NumericUpDown()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblSafeOpenDoor = New System.Windows.Forms.Label()
        Me.gpbDetectGlue = New System.Windows.Forms.GroupBox()
        Me.chkIsReset = New System.Windows.Forms.CheckBox()
        Me.lblGlueDetector4 = New System.Windows.Forms.Label()
        Me.lblGlueDetector3 = New System.Windows.Forms.Label()
        Me.lblGlueDetector2 = New System.Windows.Forms.Label()
        Me.lblGlueDetector1 = New System.Windows.Forms.Label()
        Me.btnBypassDetectGlueNo4 = New System.Windows.Forms.Button()
        Me.btnBypassDetectGlueNo3 = New System.Windows.Forms.Button()
        Me.btnBypassDetectGlueNo2 = New System.Windows.Forms.Button()
        Me.btnBypassDetectGlueNo1 = New System.Windows.Forms.Button()
        Me.btnBack2 = New System.Windows.Forms.Button()
        Me.tabCharge = New System.Windows.Forms.TabPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.gpbFunction.SuspendLayout()
        Me.gpbAugerValveCTThreshold.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tabMechanismModule.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.gpbMechanismSoftware.SuspendLayout()
        Me.gpbValveType.SuspendLayout()
        Me.tabBasicFunction.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.grpAirPressureUnit.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grpCCDImage.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.tabCommonFunction.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpConveyor.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.gpbTolerance.SuspendLayout()
        Me.gpTiltSet.SuspendLayout()
        Me.grpWeight.SuspendLayout()
        Me.grpStableTime.SuspendLayout()
        CType(Me.nmcPriorHeatTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcSensorTimeOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcLaserStableTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcCCDStableTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSafeTemperature.SuspendLayout()
        CType(Me.nmcMinHotplateTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcMaxHotplateTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcMaxValveTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcSafeTemperature, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpbDetectGlue.SuspendLayout()
        Me.SuspendLayout()
        '
        'gpbFunction
        '
        resources.ApplyResources(Me.gpbFunction, "gpbFunction")
        Me.gpbFunction.Controls.Add(Me.cboxMapDataModel)
        Me.gpbFunction.Controls.Add(Me.lblCleanMachine)
        Me.gpbFunction.Controls.Add(Me.btnCleanDevice)
        Me.gpbFunction.Controls.Add(Me.lblContinueRun)
        Me.gpbFunction.Controls.Add(Me.btnContinueRun)
        Me.gpbFunction.Controls.Add(Me.btnManualMap)
        Me.gpbFunction.Controls.Add(Me.Label28)
        Me.gpbFunction.Controls.Add(Me.btnPassLUL)
        Me.gpbFunction.Controls.Add(Me.Label4)
        Me.gpbFunction.Controls.Add(Me.Label1)
        Me.gpbFunction.Controls.Add(Me.lblConveyor)
        Me.gpbFunction.Controls.Add(Me.btnDisableConveyor)
        Me.gpbFunction.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gpbFunction.Name = "gpbFunction"
        Me.gpbFunction.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gpbFunction, resources.GetString("gpbFunction.ToolTip"))
        '
        'cboxMapDataModel
        '
        resources.ApplyResources(Me.cboxMapDataModel, "cboxMapDataModel")
        Me.cboxMapDataModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxMapDataModel.FormattingEnabled = True
        Me.cboxMapDataModel.Items.AddRange(New Object() {resources.GetString("cboxMapDataModel.Items"), resources.GetString("cboxMapDataModel.Items1"), resources.GetString("cboxMapDataModel.Items2"), resources.GetString("cboxMapDataModel.Items3")})
        Me.cboxMapDataModel.Name = "cboxMapDataModel"
        Me.ToolTip1.SetToolTip(Me.cboxMapDataModel, resources.GetString("cboxMapDataModel.ToolTip"))
        '
        'lblCleanMachine
        '
        resources.ApplyResources(Me.lblCleanMachine, "lblCleanMachine")
        Me.lblCleanMachine.Name = "lblCleanMachine"
        Me.ToolTip1.SetToolTip(Me.lblCleanMachine, resources.GetString("lblCleanMachine.ToolTip"))
        '
        'btnCleanDevice
        '
        resources.ApplyResources(Me.btnCleanDevice, "btnCleanDevice")
        Me.btnCleanDevice.BackColor = System.Drawing.SystemColors.Control
        Me.btnCleanDevice.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnCleanDevice.FlatAppearance.BorderSize = 0
        Me.btnCleanDevice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnCleanDevice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnCleanDevice.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCleanDevice.Name = "btnCleanDevice"
        Me.btnCleanDevice.Tag = "7"
        Me.ToolTip1.SetToolTip(Me.btnCleanDevice, resources.GetString("btnCleanDevice.ToolTip"))
        Me.btnCleanDevice.UseVisualStyleBackColor = True
        '
        'lblContinueRun
        '
        resources.ApplyResources(Me.lblContinueRun, "lblContinueRun")
        Me.lblContinueRun.Name = "lblContinueRun"
        Me.ToolTip1.SetToolTip(Me.lblContinueRun, resources.GetString("lblContinueRun.ToolTip"))
        '
        'btnContinueRun
        '
        resources.ApplyResources(Me.btnContinueRun, "btnContinueRun")
        Me.btnContinueRun.BackColor = System.Drawing.SystemColors.Control
        Me.btnContinueRun.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnContinueRun.FlatAppearance.BorderSize = 0
        Me.btnContinueRun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnContinueRun.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnContinueRun.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnContinueRun.Name = "btnContinueRun"
        Me.btnContinueRun.Tag = "7"
        Me.ToolTip1.SetToolTip(Me.btnContinueRun, resources.GetString("btnContinueRun.ToolTip"))
        Me.btnContinueRun.UseVisualStyleBackColor = True
        '
        'btnManualMap
        '
        resources.ApplyResources(Me.btnManualMap, "btnManualMap")
        Me.btnManualMap.BackColor = System.Drawing.SystemColors.Control
        Me.btnManualMap.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnManualMap.FlatAppearance.BorderSize = 0
        Me.btnManualMap.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnManualMap.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnManualMap.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnManualMap.Name = "btnManualMap"
        Me.btnManualMap.Tag = "7"
        Me.ToolTip1.SetToolTip(Me.btnManualMap, resources.GetString("btnManualMap.ToolTip"))
        Me.btnManualMap.UseVisualStyleBackColor = True
        '
        'Label28
        '
        resources.ApplyResources(Me.Label28, "Label28")
        Me.Label28.Name = "Label28"
        Me.ToolTip1.SetToolTip(Me.Label28, resources.GetString("Label28.ToolTip"))
        '
        'btnPassLUL
        '
        resources.ApplyResources(Me.btnPassLUL, "btnPassLUL")
        Me.btnPassLUL.BackColor = System.Drawing.SystemColors.Control
        Me.btnPassLUL.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnPassLUL.FlatAppearance.BorderSize = 0
        Me.btnPassLUL.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnPassLUL.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnPassLUL.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnPassLUL.Name = "btnPassLUL"
        Me.btnPassLUL.Tag = "7"
        Me.ToolTip1.SetToolTip(Me.btnPassLUL, resources.GetString("btnPassLUL.ToolTip"))
        Me.btnPassLUL.UseVisualStyleBackColor = True
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        Me.ToolTip1.SetToolTip(Me.Label4, resources.GetString("Label4.ToolTip"))
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'lblConveyor
        '
        resources.ApplyResources(Me.lblConveyor, "lblConveyor")
        Me.lblConveyor.Name = "lblConveyor"
        Me.ToolTip1.SetToolTip(Me.lblConveyor, resources.GetString("lblConveyor.ToolTip"))
        '
        'btnDisableConveyor
        '
        resources.ApplyResources(Me.btnDisableConveyor, "btnDisableConveyor")
        Me.btnDisableConveyor.BackColor = System.Drawing.SystemColors.Control
        Me.btnDisableConveyor.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnDisableConveyor.FlatAppearance.BorderSize = 0
        Me.btnDisableConveyor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnDisableConveyor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnDisableConveyor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnDisableConveyor.Name = "btnDisableConveyor"
        Me.btnDisableConveyor.Tag = "7"
        Me.ToolTip1.SetToolTip(Me.btnDisableConveyor, resources.GetString("btnDisableConveyor.ToolTip"))
        Me.btnDisableConveyor.UseVisualStyleBackColor = True
        '
        'btnPreviousPage
        '
        resources.ApplyResources(Me.btnPreviousPage, "btnPreviousPage")
        Me.btnPreviousPage.BackColor = System.Drawing.Color.DarkOrange
        Me.btnPreviousPage.Name = "btnPreviousPage"
        Me.btnPreviousPage.Tag = "3"
        Me.ToolTip1.SetToolTip(Me.btnPreviousPage, resources.GetString("btnPreviousPage.ToolTip"))
        Me.btnPreviousPage.UseVisualStyleBackColor = True
        '
        'gpbAugerValveCTThreshold
        '
        resources.ApplyResources(Me.gpbAugerValveCTThreshold, "gpbAugerValveCTThreshold")
        Me.gpbAugerValveCTThreshold.Controls.Add(Me.lblValve4CTUnit)
        Me.gpbAugerValveCTThreshold.Controls.Add(Me.lblValve2CTUnit)
        Me.gpbAugerValveCTThreshold.Controls.Add(Me.lblValve3CTUnit)
        Me.gpbAugerValveCTThreshold.Controls.Add(Me.lblValve1CTUnit)
        Me.gpbAugerValveCTThreshold.Controls.Add(Me.Label9)
        Me.gpbAugerValveCTThreshold.Controls.Add(Me.btnAugerValveCTThreshold)
        Me.gpbAugerValveCTThreshold.Controls.Add(Me.Label7)
        Me.gpbAugerValveCTThreshold.Controls.Add(Me.lblScrewValveNo2CTThreshold)
        Me.gpbAugerValveCTThreshold.Controls.Add(Me.txtAugerValveCTThreshold3)
        Me.gpbAugerValveCTThreshold.Controls.Add(Me.lblScrewValveNo1CTThreshold)
        Me.gpbAugerValveCTThreshold.Controls.Add(Me.txtAugerValveCTThreshold4)
        Me.gpbAugerValveCTThreshold.Controls.Add(Me.txtAugerValveCTThreshold1)
        Me.gpbAugerValveCTThreshold.Controls.Add(Me.txtAugerValveCTThreshold2)
        Me.gpbAugerValveCTThreshold.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gpbAugerValveCTThreshold.Name = "gpbAugerValveCTThreshold"
        Me.gpbAugerValveCTThreshold.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gpbAugerValveCTThreshold, resources.GetString("gpbAugerValveCTThreshold.ToolTip"))
        '
        'lblValve4CTUnit
        '
        resources.ApplyResources(Me.lblValve4CTUnit, "lblValve4CTUnit")
        Me.lblValve4CTUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblValve4CTUnit.Name = "lblValve4CTUnit"
        Me.ToolTip1.SetToolTip(Me.lblValve4CTUnit, resources.GetString("lblValve4CTUnit.ToolTip"))
        '
        'lblValve2CTUnit
        '
        resources.ApplyResources(Me.lblValve2CTUnit, "lblValve2CTUnit")
        Me.lblValve2CTUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblValve2CTUnit.Name = "lblValve2CTUnit"
        Me.ToolTip1.SetToolTip(Me.lblValve2CTUnit, resources.GetString("lblValve2CTUnit.ToolTip"))
        '
        'lblValve3CTUnit
        '
        resources.ApplyResources(Me.lblValve3CTUnit, "lblValve3CTUnit")
        Me.lblValve3CTUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblValve3CTUnit.Name = "lblValve3CTUnit"
        Me.ToolTip1.SetToolTip(Me.lblValve3CTUnit, resources.GetString("lblValve3CTUnit.ToolTip"))
        '
        'lblValve1CTUnit
        '
        resources.ApplyResources(Me.lblValve1CTUnit, "lblValve1CTUnit")
        Me.lblValve1CTUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblValve1CTUnit.Name = "lblValve1CTUnit"
        Me.ToolTip1.SetToolTip(Me.lblValve1CTUnit, resources.GetString("lblValve1CTUnit.ToolTip"))
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Name = "Label9"
        Me.ToolTip1.SetToolTip(Me.Label9, resources.GetString("Label9.ToolTip"))
        '
        'btnAugerValveCTThreshold
        '
        resources.ApplyResources(Me.btnAugerValveCTThreshold, "btnAugerValveCTThreshold")
        Me.btnAugerValveCTThreshold.BackColor = System.Drawing.SystemColors.Control
        Me.btnAugerValveCTThreshold.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnAugerValveCTThreshold.FlatAppearance.BorderSize = 0
        Me.btnAugerValveCTThreshold.Name = "btnAugerValveCTThreshold"
        Me.ToolTip1.SetToolTip(Me.btnAugerValveCTThreshold, resources.GetString("btnAugerValveCTThreshold.ToolTip"))
        Me.btnAugerValveCTThreshold.UseVisualStyleBackColor = True
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Name = "Label7"
        Me.ToolTip1.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip"))
        '
        'lblScrewValveNo2CTThreshold
        '
        resources.ApplyResources(Me.lblScrewValveNo2CTThreshold, "lblScrewValveNo2CTThreshold")
        Me.lblScrewValveNo2CTThreshold.BackColor = System.Drawing.Color.Transparent
        Me.lblScrewValveNo2CTThreshold.Name = "lblScrewValveNo2CTThreshold"
        Me.ToolTip1.SetToolTip(Me.lblScrewValveNo2CTThreshold, resources.GetString("lblScrewValveNo2CTThreshold.ToolTip"))
        '
        'txtAugerValveCTThreshold3
        '
        resources.ApplyResources(Me.txtAugerValveCTThreshold3, "txtAugerValveCTThreshold3")
        Me.txtAugerValveCTThreshold3.BackColor = System.Drawing.Color.White
        Me.txtAugerValveCTThreshold3.Name = "txtAugerValveCTThreshold3"
        Me.ToolTip1.SetToolTip(Me.txtAugerValveCTThreshold3, resources.GetString("txtAugerValveCTThreshold3.ToolTip"))
        '
        'lblScrewValveNo1CTThreshold
        '
        resources.ApplyResources(Me.lblScrewValveNo1CTThreshold, "lblScrewValveNo1CTThreshold")
        Me.lblScrewValveNo1CTThreshold.BackColor = System.Drawing.Color.Transparent
        Me.lblScrewValveNo1CTThreshold.Name = "lblScrewValveNo1CTThreshold"
        Me.ToolTip1.SetToolTip(Me.lblScrewValveNo1CTThreshold, resources.GetString("lblScrewValveNo1CTThreshold.ToolTip"))
        '
        'txtAugerValveCTThreshold4
        '
        resources.ApplyResources(Me.txtAugerValveCTThreshold4, "txtAugerValveCTThreshold4")
        Me.txtAugerValveCTThreshold4.BackColor = System.Drawing.Color.White
        Me.txtAugerValveCTThreshold4.Name = "txtAugerValveCTThreshold4"
        Me.ToolTip1.SetToolTip(Me.txtAugerValveCTThreshold4, resources.GetString("txtAugerValveCTThreshold4.ToolTip"))
        '
        'txtAugerValveCTThreshold1
        '
        resources.ApplyResources(Me.txtAugerValveCTThreshold1, "txtAugerValveCTThreshold1")
        Me.txtAugerValveCTThreshold1.BackColor = System.Drawing.Color.White
        Me.txtAugerValveCTThreshold1.Name = "txtAugerValveCTThreshold1"
        Me.ToolTip1.SetToolTip(Me.txtAugerValveCTThreshold1, resources.GetString("txtAugerValveCTThreshold1.ToolTip"))
        '
        'txtAugerValveCTThreshold2
        '
        resources.ApplyResources(Me.txtAugerValveCTThreshold2, "txtAugerValveCTThreshold2")
        Me.txtAugerValveCTThreshold2.BackColor = System.Drawing.Color.White
        Me.txtAugerValveCTThreshold2.Name = "txtAugerValveCTThreshold2"
        Me.ToolTip1.SetToolTip(Me.txtAugerValveCTThreshold2, resources.GetString("txtAugerValveCTThreshold2.ToolTip"))
        '
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.tabMechanismModule)
        Me.TabControl1.Controls.Add(Me.tabBasicFunction)
        Me.TabControl1.Controls.Add(Me.tabCommonFunction)
        Me.TabControl1.Controls.Add(Me.tabCharge)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.ToolTip1.SetToolTip(Me.TabControl1, resources.GetString("TabControl1.ToolTip"))
        '
        'tabMechanismModule
        '
        resources.ApplyResources(Me.tabMechanismModule, "tabMechanismModule")
        Me.tabMechanismModule.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabMechanismModule.Controls.Add(Me.GroupBox4)
        Me.tabMechanismModule.Controls.Add(Me.btnBack)
        Me.tabMechanismModule.Controls.Add(Me.gpbMechanismSoftware)
        Me.tabMechanismModule.Controls.Add(Me.gpbValveType)
        Me.tabMechanismModule.Name = "tabMechanismModule"
        Me.ToolTip1.SetToolTip(Me.tabMechanismModule, resources.GetString("tabMechanismModule.ToolTip"))
        '
        'GroupBox4
        '
        resources.ApplyResources(Me.GroupBox4, "GroupBox4")
        Me.GroupBox4.Controls.Add(Me.btnSaveMachineType)
        Me.GroupBox4.Controls.Add(Me.cboMachineType)
        Me.GroupBox4.Controls.Add(Me.lblMachineType)
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox4, resources.GetString("GroupBox4.ToolTip"))
        '
        'btnSaveMachineType
        '
        resources.ApplyResources(Me.btnSaveMachineType, "btnSaveMachineType")
        Me.btnSaveMachineType.BackColor = System.Drawing.SystemColors.Control
        Me.btnSaveMachineType.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSaveMachineType.Name = "btnSaveMachineType"
        Me.ToolTip1.SetToolTip(Me.btnSaveMachineType, resources.GetString("btnSaveMachineType.ToolTip"))
        Me.btnSaveMachineType.UseVisualStyleBackColor = True
        '
        'cboMachineType
        '
        resources.ApplyResources(Me.cboMachineType, "cboMachineType")
        Me.cboMachineType.BackColor = System.Drawing.Color.White
        Me.cboMachineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMachineType.FormattingEnabled = True
        Me.cboMachineType.Name = "cboMachineType"
        Me.ToolTip1.SetToolTip(Me.cboMachineType, resources.GetString("cboMachineType.ToolTip"))
        '
        'lblMachineType
        '
        resources.ApplyResources(Me.lblMachineType, "lblMachineType")
        Me.lblMachineType.BackColor = System.Drawing.Color.Transparent
        Me.lblMachineType.Name = "lblMachineType"
        Me.ToolTip1.SetToolTip(Me.lblMachineType, resources.GetString("lblMachineType.ToolTip"))
        '
        'btnBack
        '
        resources.ApplyResources(Me.btnBack, "btnBack")
        Me.btnBack.BackColor = System.Drawing.SystemColors.Control
        Me.btnBack.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnBack.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnBack.Name = "btnBack"
        Me.ToolTip1.SetToolTip(Me.btnBack, resources.GetString("btnBack.ToolTip"))
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'gpbMechanismSoftware
        '
        resources.ApplyResources(Me.gpbMechanismSoftware, "gpbMechanismSoftware")
        Me.gpbMechanismSoftware.Controls.Add(Me.cboConveyorModel)
        Me.gpbMechanismSoftware.Controls.Add(Me.lblConveyorModel)
        Me.gpbMechanismSoftware.Controls.Add(Me.cboCCDOnFly)
        Me.gpbMechanismSoftware.Controls.Add(Me.lblCCDOnFly)
        Me.gpbMechanismSoftware.Controls.Add(Me.cboMultiDispense)
        Me.gpbMechanismSoftware.Controls.Add(Me.lblMultiDispense)
        Me.gpbMechanismSoftware.Controls.Add(Me.cboZHeightDevice)
        Me.gpbMechanismSoftware.Controls.Add(Me.Label27)
        Me.gpbMechanismSoftware.Controls.Add(Me.btnMechanismModule)
        Me.gpbMechanismSoftware.Controls.Add(Me.lblCCDModuleType)
        Me.gpbMechanismSoftware.Controls.Add(Me.cboCCDModuleType)
        Me.gpbMechanismSoftware.Controls.Add(Me.lblStageUseValveCount)
        Me.gpbMechanismSoftware.Controls.Add(Me.cboStageUseValveCount)
        Me.gpbMechanismSoftware.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gpbMechanismSoftware.Name = "gpbMechanismSoftware"
        Me.gpbMechanismSoftware.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gpbMechanismSoftware, resources.GetString("gpbMechanismSoftware.ToolTip"))
        '
        'cboConveyorModel
        '
        resources.ApplyResources(Me.cboConveyorModel, "cboConveyorModel")
        Me.cboConveyorModel.BackColor = System.Drawing.Color.White
        Me.cboConveyorModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboConveyorModel.FormattingEnabled = True
        Me.cboConveyorModel.Name = "cboConveyorModel"
        Me.ToolTip1.SetToolTip(Me.cboConveyorModel, resources.GetString("cboConveyorModel.ToolTip"))
        '
        'lblConveyorModel
        '
        resources.ApplyResources(Me.lblConveyorModel, "lblConveyorModel")
        Me.lblConveyorModel.BackColor = System.Drawing.Color.Transparent
        Me.lblConveyorModel.Name = "lblConveyorModel"
        Me.ToolTip1.SetToolTip(Me.lblConveyorModel, resources.GetString("lblConveyorModel.ToolTip"))
        '
        'cboCCDOnFly
        '
        resources.ApplyResources(Me.cboCCDOnFly, "cboCCDOnFly")
        Me.cboCCDOnFly.AutoCompleteCustomSource.AddRange(New String() {resources.GetString("cboCCDOnFly.AutoCompleteCustomSource"), resources.GetString("cboCCDOnFly.AutoCompleteCustomSource1"), resources.GetString("cboCCDOnFly.AutoCompleteCustomSource2")})
        Me.cboCCDOnFly.BackColor = System.Drawing.Color.White
        Me.cboCCDOnFly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCCDOnFly.FormattingEnabled = True
        Me.cboCCDOnFly.Name = "cboCCDOnFly"
        Me.ToolTip1.SetToolTip(Me.cboCCDOnFly, resources.GetString("cboCCDOnFly.ToolTip"))
        '
        'lblCCDOnFly
        '
        resources.ApplyResources(Me.lblCCDOnFly, "lblCCDOnFly")
        Me.lblCCDOnFly.BackColor = System.Drawing.Color.Transparent
        Me.lblCCDOnFly.Name = "lblCCDOnFly"
        Me.ToolTip1.SetToolTip(Me.lblCCDOnFly, resources.GetString("lblCCDOnFly.ToolTip"))
        '
        'cboMultiDispense
        '
        resources.ApplyResources(Me.cboMultiDispense, "cboMultiDispense")
        Me.cboMultiDispense.AutoCompleteCustomSource.AddRange(New String() {resources.GetString("cboMultiDispense.AutoCompleteCustomSource"), resources.GetString("cboMultiDispense.AutoCompleteCustomSource1"), resources.GetString("cboMultiDispense.AutoCompleteCustomSource2")})
        Me.cboMultiDispense.BackColor = System.Drawing.Color.White
        Me.cboMultiDispense.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMultiDispense.FormattingEnabled = True
        Me.cboMultiDispense.Name = "cboMultiDispense"
        Me.ToolTip1.SetToolTip(Me.cboMultiDispense, resources.GetString("cboMultiDispense.ToolTip"))
        '
        'lblMultiDispense
        '
        resources.ApplyResources(Me.lblMultiDispense, "lblMultiDispense")
        Me.lblMultiDispense.BackColor = System.Drawing.Color.Transparent
        Me.lblMultiDispense.Name = "lblMultiDispense"
        Me.ToolTip1.SetToolTip(Me.lblMultiDispense, resources.GetString("lblMultiDispense.ToolTip"))
        '
        'cboZHeightDevice
        '
        resources.ApplyResources(Me.cboZHeightDevice, "cboZHeightDevice")
        Me.cboZHeightDevice.AutoCompleteCustomSource.AddRange(New String() {resources.GetString("cboZHeightDevice.AutoCompleteCustomSource"), resources.GetString("cboZHeightDevice.AutoCompleteCustomSource1"), resources.GetString("cboZHeightDevice.AutoCompleteCustomSource2")})
        Me.cboZHeightDevice.BackColor = System.Drawing.Color.White
        Me.cboZHeightDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboZHeightDevice.FormattingEnabled = True
        Me.cboZHeightDevice.Name = "cboZHeightDevice"
        Me.ToolTip1.SetToolTip(Me.cboZHeightDevice, resources.GetString("cboZHeightDevice.ToolTip"))
        '
        'Label27
        '
        resources.ApplyResources(Me.Label27, "Label27")
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Name = "Label27"
        Me.ToolTip1.SetToolTip(Me.Label27, resources.GetString("Label27.ToolTip"))
        '
        'btnMechanismModule
        '
        resources.ApplyResources(Me.btnMechanismModule, "btnMechanismModule")
        Me.btnMechanismModule.BackColor = System.Drawing.SystemColors.Control
        Me.btnMechanismModule.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnMechanismModule.FlatAppearance.BorderSize = 0
        Me.btnMechanismModule.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnMechanismModule.Name = "btnMechanismModule"
        Me.ToolTip1.SetToolTip(Me.btnMechanismModule, resources.GetString("btnMechanismModule.ToolTip"))
        Me.btnMechanismModule.UseVisualStyleBackColor = True
        '
        'lblCCDModuleType
        '
        resources.ApplyResources(Me.lblCCDModuleType, "lblCCDModuleType")
        Me.lblCCDModuleType.BackColor = System.Drawing.Color.Transparent
        Me.lblCCDModuleType.Name = "lblCCDModuleType"
        Me.ToolTip1.SetToolTip(Me.lblCCDModuleType, resources.GetString("lblCCDModuleType.ToolTip"))
        '
        'cboCCDModuleType
        '
        resources.ApplyResources(Me.cboCCDModuleType, "cboCCDModuleType")
        Me.cboCCDModuleType.BackColor = System.Drawing.Color.White
        Me.cboCCDModuleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCCDModuleType.FormattingEnabled = True
        Me.cboCCDModuleType.Name = "cboCCDModuleType"
        Me.ToolTip1.SetToolTip(Me.cboCCDModuleType, resources.GetString("cboCCDModuleType.ToolTip"))
        '
        'lblStageUseValveCount
        '
        resources.ApplyResources(Me.lblStageUseValveCount, "lblStageUseValveCount")
        Me.lblStageUseValveCount.BackColor = System.Drawing.Color.Transparent
        Me.lblStageUseValveCount.Name = "lblStageUseValveCount"
        Me.ToolTip1.SetToolTip(Me.lblStageUseValveCount, resources.GetString("lblStageUseValveCount.ToolTip"))
        '
        'cboStageUseValveCount
        '
        resources.ApplyResources(Me.cboStageUseValveCount, "cboStageUseValveCount")
        Me.cboStageUseValveCount.BackColor = System.Drawing.Color.White
        Me.cboStageUseValveCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStageUseValveCount.FormattingEnabled = True
        Me.cboStageUseValveCount.Name = "cboStageUseValveCount"
        Me.ToolTip1.SetToolTip(Me.cboStageUseValveCount, resources.GetString("cboStageUseValveCount.ToolTip"))
        '
        'gpbValveType
        '
        resources.ApplyResources(Me.gpbValveType, "gpbValveType")
        Me.gpbValveType.Controls.Add(Me.cboDispenserNo4ValveTypeModel)
        Me.gpbValveType.Controls.Add(Me.ComboBox1)
        Me.gpbValveType.Controls.Add(Me.cboDispenserNo3ValveTypeModel)
        Me.gpbValveType.Controls.Add(Me.cboDispenserNo2ValveTypeModel)
        Me.gpbValveType.Controls.Add(Me.cboDispenserNo1ValveTypeModel)
        Me.gpbValveType.Controls.Add(Me.cboDispenserNo4ValveType)
        Me.gpbValveType.Controls.Add(Me.lblDispenserNo4ValveType)
        Me.gpbValveType.Controls.Add(Me.cboDispenserNo3ValveType)
        Me.gpbValveType.Controls.Add(Me.lblDispenserNo3ValveType)
        Me.gpbValveType.Controls.Add(Me.cboDispenserNo2ValveType)
        Me.gpbValveType.Controls.Add(Me.lblDispenserNo2ValveType)
        Me.gpbValveType.Controls.Add(Me.cboDispenserNo1ValveType)
        Me.gpbValveType.Controls.Add(Me.lblDispenserNo1ValveType)
        Me.gpbValveType.Controls.Add(Me.btnValveType)
        Me.gpbValveType.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gpbValveType.Name = "gpbValveType"
        Me.gpbValveType.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gpbValveType, resources.GetString("gpbValveType.ToolTip"))
        '
        'cboDispenserNo4ValveTypeModel
        '
        resources.ApplyResources(Me.cboDispenserNo4ValveTypeModel, "cboDispenserNo4ValveTypeModel")
        Me.cboDispenserNo4ValveTypeModel.BackColor = System.Drawing.Color.White
        Me.cboDispenserNo4ValveTypeModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDispenserNo4ValveTypeModel.FormattingEnabled = True
        Me.cboDispenserNo4ValveTypeModel.Name = "cboDispenserNo4ValveTypeModel"
        Me.ToolTip1.SetToolTip(Me.cboDispenserNo4ValveTypeModel, resources.GetString("cboDispenserNo4ValveTypeModel.ToolTip"))
        '
        'ComboBox1
        '
        resources.ApplyResources(Me.ComboBox1, "ComboBox1")
        Me.ComboBox1.BackColor = System.Drawing.Color.White
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Name = "ComboBox1"
        Me.ToolTip1.SetToolTip(Me.ComboBox1, resources.GetString("ComboBox1.ToolTip"))
        '
        'cboDispenserNo3ValveTypeModel
        '
        resources.ApplyResources(Me.cboDispenserNo3ValveTypeModel, "cboDispenserNo3ValveTypeModel")
        Me.cboDispenserNo3ValveTypeModel.BackColor = System.Drawing.Color.White
        Me.cboDispenserNo3ValveTypeModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDispenserNo3ValveTypeModel.FormattingEnabled = True
        Me.cboDispenserNo3ValveTypeModel.Name = "cboDispenserNo3ValveTypeModel"
        Me.ToolTip1.SetToolTip(Me.cboDispenserNo3ValveTypeModel, resources.GetString("cboDispenserNo3ValveTypeModel.ToolTip"))
        '
        'cboDispenserNo2ValveTypeModel
        '
        resources.ApplyResources(Me.cboDispenserNo2ValveTypeModel, "cboDispenserNo2ValveTypeModel")
        Me.cboDispenserNo2ValveTypeModel.BackColor = System.Drawing.Color.White
        Me.cboDispenserNo2ValveTypeModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDispenserNo2ValveTypeModel.FormattingEnabled = True
        Me.cboDispenserNo2ValveTypeModel.Name = "cboDispenserNo2ValveTypeModel"
        Me.ToolTip1.SetToolTip(Me.cboDispenserNo2ValveTypeModel, resources.GetString("cboDispenserNo2ValveTypeModel.ToolTip"))
        '
        'cboDispenserNo1ValveTypeModel
        '
        resources.ApplyResources(Me.cboDispenserNo1ValveTypeModel, "cboDispenserNo1ValveTypeModel")
        Me.cboDispenserNo1ValveTypeModel.BackColor = System.Drawing.Color.White
        Me.cboDispenserNo1ValveTypeModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDispenserNo1ValveTypeModel.FormattingEnabled = True
        Me.cboDispenserNo1ValveTypeModel.Name = "cboDispenserNo1ValveTypeModel"
        Me.ToolTip1.SetToolTip(Me.cboDispenserNo1ValveTypeModel, resources.GetString("cboDispenserNo1ValveTypeModel.ToolTip"))
        '
        'cboDispenserNo4ValveType
        '
        resources.ApplyResources(Me.cboDispenserNo4ValveType, "cboDispenserNo4ValveType")
        Me.cboDispenserNo4ValveType.BackColor = System.Drawing.Color.White
        Me.cboDispenserNo4ValveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDispenserNo4ValveType.FormattingEnabled = True
        Me.cboDispenserNo4ValveType.Name = "cboDispenserNo4ValveType"
        Me.ToolTip1.SetToolTip(Me.cboDispenserNo4ValveType, resources.GetString("cboDispenserNo4ValveType.ToolTip"))
        '
        'lblDispenserNo4ValveType
        '
        resources.ApplyResources(Me.lblDispenserNo4ValveType, "lblDispenserNo4ValveType")
        Me.lblDispenserNo4ValveType.BackColor = System.Drawing.Color.Transparent
        Me.lblDispenserNo4ValveType.Name = "lblDispenserNo4ValveType"
        Me.ToolTip1.SetToolTip(Me.lblDispenserNo4ValveType, resources.GetString("lblDispenserNo4ValveType.ToolTip"))
        '
        'cboDispenserNo3ValveType
        '
        resources.ApplyResources(Me.cboDispenserNo3ValveType, "cboDispenserNo3ValveType")
        Me.cboDispenserNo3ValveType.BackColor = System.Drawing.Color.White
        Me.cboDispenserNo3ValveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDispenserNo3ValveType.FormattingEnabled = True
        Me.cboDispenserNo3ValveType.Name = "cboDispenserNo3ValveType"
        Me.ToolTip1.SetToolTip(Me.cboDispenserNo3ValveType, resources.GetString("cboDispenserNo3ValveType.ToolTip"))
        '
        'lblDispenserNo3ValveType
        '
        resources.ApplyResources(Me.lblDispenserNo3ValveType, "lblDispenserNo3ValveType")
        Me.lblDispenserNo3ValveType.BackColor = System.Drawing.Color.Transparent
        Me.lblDispenserNo3ValveType.Name = "lblDispenserNo3ValveType"
        Me.ToolTip1.SetToolTip(Me.lblDispenserNo3ValveType, resources.GetString("lblDispenserNo3ValveType.ToolTip"))
        '
        'cboDispenserNo2ValveType
        '
        resources.ApplyResources(Me.cboDispenserNo2ValveType, "cboDispenserNo2ValveType")
        Me.cboDispenserNo2ValveType.BackColor = System.Drawing.Color.White
        Me.cboDispenserNo2ValveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDispenserNo2ValveType.FormattingEnabled = True
        Me.cboDispenserNo2ValveType.Name = "cboDispenserNo2ValveType"
        Me.ToolTip1.SetToolTip(Me.cboDispenserNo2ValveType, resources.GetString("cboDispenserNo2ValveType.ToolTip"))
        '
        'lblDispenserNo2ValveType
        '
        resources.ApplyResources(Me.lblDispenserNo2ValveType, "lblDispenserNo2ValveType")
        Me.lblDispenserNo2ValveType.BackColor = System.Drawing.Color.Transparent
        Me.lblDispenserNo2ValveType.Name = "lblDispenserNo2ValveType"
        Me.ToolTip1.SetToolTip(Me.lblDispenserNo2ValveType, resources.GetString("lblDispenserNo2ValveType.ToolTip"))
        '
        'cboDispenserNo1ValveType
        '
        resources.ApplyResources(Me.cboDispenserNo1ValveType, "cboDispenserNo1ValveType")
        Me.cboDispenserNo1ValveType.BackColor = System.Drawing.Color.White
        Me.cboDispenserNo1ValveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDispenserNo1ValveType.FormattingEnabled = True
        Me.cboDispenserNo1ValveType.Name = "cboDispenserNo1ValveType"
        Me.ToolTip1.SetToolTip(Me.cboDispenserNo1ValveType, resources.GetString("cboDispenserNo1ValveType.ToolTip"))
        '
        'lblDispenserNo1ValveType
        '
        resources.ApplyResources(Me.lblDispenserNo1ValveType, "lblDispenserNo1ValveType")
        Me.lblDispenserNo1ValveType.BackColor = System.Drawing.Color.Transparent
        Me.lblDispenserNo1ValveType.Name = "lblDispenserNo1ValveType"
        Me.ToolTip1.SetToolTip(Me.lblDispenserNo1ValveType, resources.GetString("lblDispenserNo1ValveType.ToolTip"))
        '
        'btnValveType
        '
        resources.ApplyResources(Me.btnValveType, "btnValveType")
        Me.btnValveType.BackColor = System.Drawing.SystemColors.Control
        Me.btnValveType.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnValveType.FlatAppearance.BorderSize = 0
        Me.btnValveType.Name = "btnValveType"
        Me.ToolTip1.SetToolTip(Me.btnValveType, resources.GetString("btnValveType.ToolTip"))
        Me.btnValveType.UseVisualStyleBackColor = True
        '
        'tabBasicFunction
        '
        resources.ApplyResources(Me.tabBasicFunction, "tabBasicFunction")
        Me.tabBasicFunction.Controls.Add(Me.GroupBox9)
        Me.tabBasicFunction.Controls.Add(Me.btnBack3)
        Me.tabBasicFunction.Controls.Add(Me.grpAirPressureUnit)
        Me.tabBasicFunction.Controls.Add(Me.GroupBox2)
        Me.tabBasicFunction.Controls.Add(Me.grpCCDImage)
        Me.tabBasicFunction.Controls.Add(Me.GroupBox3)
        Me.tabBasicFunction.Controls.Add(Me.GroupBox5)
        Me.tabBasicFunction.Name = "tabBasicFunction"
        Me.ToolTip1.SetToolTip(Me.tabBasicFunction, resources.GetString("tabBasicFunction.ToolTip"))
        Me.tabBasicFunction.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        resources.ApplyResources(Me.GroupBox9, "GroupBox9")
        Me.GroupBox9.Controls.Add(Me.btnMapPath)
        Me.GroupBox9.Controls.Add(Me.Label39)
        Me.GroupBox9.Controls.Add(Me.txtMapDataPath)
        Me.GroupBox9.Controls.Add(Me.btnSetMapPath)
        Me.GroupBox9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox9, resources.GetString("GroupBox9.ToolTip"))
        '
        'btnMapPath
        '
        resources.ApplyResources(Me.btnMapPath, "btnMapPath")
        Me.btnMapPath.AutoEllipsis = True
        Me.btnMapPath.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.OpenFile
        Me.btnMapPath.FlatAppearance.BorderSize = 0
        Me.btnMapPath.Name = "btnMapPath"
        Me.ToolTip1.SetToolTip(Me.btnMapPath, resources.GetString("btnMapPath.ToolTip"))
        Me.btnMapPath.UseVisualStyleBackColor = True
        '
        'Label39
        '
        resources.ApplyResources(Me.Label39, "Label39")
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Name = "Label39"
        Me.ToolTip1.SetToolTip(Me.Label39, resources.GetString("Label39.ToolTip"))
        '
        'txtMapDataPath
        '
        resources.ApplyResources(Me.txtMapDataPath, "txtMapDataPath")
        Me.txtMapDataPath.BackColor = System.Drawing.Color.White
        Me.txtMapDataPath.Name = "txtMapDataPath"
        Me.txtMapDataPath.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtMapDataPath, resources.GetString("txtMapDataPath.ToolTip"))
        '
        'btnSetMapPath
        '
        resources.ApplyResources(Me.btnSetMapPath, "btnSetMapPath")
        Me.btnSetMapPath.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetMapPath.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetMapPath.Name = "btnSetMapPath"
        Me.ToolTip1.SetToolTip(Me.btnSetMapPath, resources.GetString("btnSetMapPath.ToolTip"))
        Me.btnSetMapPath.UseVisualStyleBackColor = True
        '
        'btnBack3
        '
        resources.ApplyResources(Me.btnBack3, "btnBack3")
        Me.btnBack3.BackColor = System.Drawing.SystemColors.Control
        Me.btnBack3.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnBack3.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnBack3.FlatAppearance.BorderSize = 0
        Me.btnBack3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnBack3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnBack3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnBack3.Name = "btnBack3"
        Me.ToolTip1.SetToolTip(Me.btnBack3, resources.GetString("btnBack3.ToolTip"))
        Me.btnBack3.UseVisualStyleBackColor = True
        '
        'grpAirPressureUnit
        '
        resources.ApplyResources(Me.grpAirPressureUnit, "grpAirPressureUnit")
        Me.grpAirPressureUnit.Controls.Add(Me.rdbTorr)
        Me.grpAirPressureUnit.Controls.Add(Me.rdbBar)
        Me.grpAirPressureUnit.Controls.Add(Me.rdbPsi)
        Me.grpAirPressureUnit.Controls.Add(Me.rdbKg)
        Me.grpAirPressureUnit.Controls.Add(Me.rdbMPa)
        Me.grpAirPressureUnit.Controls.Add(Me.btnSetAirPressureUnit)
        Me.grpAirPressureUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpAirPressureUnit.Name = "grpAirPressureUnit"
        Me.grpAirPressureUnit.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpAirPressureUnit, resources.GetString("grpAirPressureUnit.ToolTip"))
        '
        'rdbTorr
        '
        resources.ApplyResources(Me.rdbTorr, "rdbTorr")
        Me.rdbTorr.Name = "rdbTorr"
        Me.rdbTorr.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rdbTorr, resources.GetString("rdbTorr.ToolTip"))
        Me.rdbTorr.UseVisualStyleBackColor = True
        '
        'rdbBar
        '
        resources.ApplyResources(Me.rdbBar, "rdbBar")
        Me.rdbBar.Name = "rdbBar"
        Me.rdbBar.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rdbBar, resources.GetString("rdbBar.ToolTip"))
        Me.rdbBar.UseVisualStyleBackColor = True
        '
        'rdbPsi
        '
        resources.ApplyResources(Me.rdbPsi, "rdbPsi")
        Me.rdbPsi.Name = "rdbPsi"
        Me.rdbPsi.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rdbPsi, resources.GetString("rdbPsi.ToolTip"))
        Me.rdbPsi.UseVisualStyleBackColor = True
        '
        'rdbKg
        '
        resources.ApplyResources(Me.rdbKg, "rdbKg")
        Me.rdbKg.Name = "rdbKg"
        Me.rdbKg.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rdbKg, resources.GetString("rdbKg.ToolTip"))
        Me.rdbKg.UseVisualStyleBackColor = True
        '
        'rdbMPa
        '
        resources.ApplyResources(Me.rdbMPa, "rdbMPa")
        Me.rdbMPa.Name = "rdbMPa"
        Me.rdbMPa.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rdbMPa, resources.GetString("rdbMPa.ToolTip"))
        Me.rdbMPa.UseVisualStyleBackColor = True
        '
        'btnSetAirPressureUnit
        '
        resources.ApplyResources(Me.btnSetAirPressureUnit, "btnSetAirPressureUnit")
        Me.btnSetAirPressureUnit.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetAirPressureUnit.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetAirPressureUnit.Name = "btnSetAirPressureUnit"
        Me.ToolTip1.SetToolTip(Me.btnSetAirPressureUnit, resources.GetString("btnSetAirPressureUnit.ToolTip"))
        Me.btnSetAirPressureUnit.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Controls.Add(Me.rdbInch)
        Me.GroupBox2.Controls.Add(Me.rdbmm)
        Me.GroupBox2.Controls.Add(Me.btnSetPositionUnit)
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox2, resources.GetString("GroupBox2.ToolTip"))
        '
        'rdbInch
        '
        resources.ApplyResources(Me.rdbInch, "rdbInch")
        Me.rdbInch.Name = "rdbInch"
        Me.rdbInch.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rdbInch, resources.GetString("rdbInch.ToolTip"))
        Me.rdbInch.UseVisualStyleBackColor = True
        '
        'rdbmm
        '
        resources.ApplyResources(Me.rdbmm, "rdbmm")
        Me.rdbmm.Name = "rdbmm"
        Me.rdbmm.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rdbmm, resources.GetString("rdbmm.ToolTip"))
        Me.rdbmm.UseVisualStyleBackColor = True
        '
        'btnSetPositionUnit
        '
        resources.ApplyResources(Me.btnSetPositionUnit, "btnSetPositionUnit")
        Me.btnSetPositionUnit.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetPositionUnit.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetPositionUnit.Name = "btnSetPositionUnit"
        Me.ToolTip1.SetToolTip(Me.btnSetPositionUnit, resources.GetString("btnSetPositionUnit.ToolTip"))
        Me.btnSetPositionUnit.UseVisualStyleBackColor = True
        '
        'grpCCDImage
        '
        resources.ApplyResources(Me.grpCCDImage, "grpCCDImage")
        Me.grpCCDImage.Controls.Add(Me.Label2)
        Me.grpCCDImage.Controls.Add(Me.cmbCCDImageSaveMode)
        Me.grpCCDImage.Controls.Add(Me.btnCCDImagePath)
        Me.grpCCDImage.Controls.Add(Me.Label3)
        Me.grpCCDImage.Controls.Add(Me.txtCCDImageFolderPath)
        Me.grpCCDImage.Controls.Add(Me.btnSaveImage)
        Me.grpCCDImage.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpCCDImage.Name = "grpCCDImage"
        Me.grpCCDImage.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpCCDImage, resources.GetString("grpCCDImage.ToolTip"))
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Name = "Label2"
        Me.ToolTip1.SetToolTip(Me.Label2, resources.GetString("Label2.ToolTip"))
        '
        'cmbCCDImageSaveMode
        '
        resources.ApplyResources(Me.cmbCCDImageSaveMode, "cmbCCDImageSaveMode")
        Me.cmbCCDImageSaveMode.BackColor = System.Drawing.Color.White
        Me.cmbCCDImageSaveMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCCDImageSaveMode.FormattingEnabled = True
        Me.cmbCCDImageSaveMode.Items.AddRange(New Object() {resources.GetString("cmbCCDImageSaveMode.Items"), resources.GetString("cmbCCDImageSaveMode.Items1"), resources.GetString("cmbCCDImageSaveMode.Items2")})
        Me.cmbCCDImageSaveMode.Name = "cmbCCDImageSaveMode"
        Me.ToolTip1.SetToolTip(Me.cmbCCDImageSaveMode, resources.GetString("cmbCCDImageSaveMode.ToolTip"))
        '
        'btnCCDImagePath
        '
        resources.ApplyResources(Me.btnCCDImagePath, "btnCCDImagePath")
        Me.btnCCDImagePath.AutoEllipsis = True
        Me.btnCCDImagePath.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.OpenFile
        Me.btnCCDImagePath.FlatAppearance.BorderSize = 0
        Me.btnCCDImagePath.Name = "btnCCDImagePath"
        Me.ToolTip1.SetToolTip(Me.btnCCDImagePath, resources.GetString("btnCCDImagePath.ToolTip"))
        Me.btnCCDImagePath.UseVisualStyleBackColor = True
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Name = "Label3"
        Me.ToolTip1.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip"))
        '
        'txtCCDImageFolderPath
        '
        resources.ApplyResources(Me.txtCCDImageFolderPath, "txtCCDImageFolderPath")
        Me.txtCCDImageFolderPath.BackColor = System.Drawing.Color.White
        Me.txtCCDImageFolderPath.Name = "txtCCDImageFolderPath"
        Me.ToolTip1.SetToolTip(Me.txtCCDImageFolderPath, resources.GetString("txtCCDImageFolderPath.ToolTip"))
        '
        'btnSaveImage
        '
        resources.ApplyResources(Me.btnSaveImage, "btnSaveImage")
        Me.btnSaveImage.BackColor = System.Drawing.SystemColors.Control
        Me.btnSaveImage.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSaveImage.Name = "btnSaveImage"
        Me.ToolTip1.SetToolTip(Me.btnSaveImage, resources.GetString("btnSaveImage.ToolTip"))
        Me.btnSaveImage.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Controls.Add(Me.btnLogPath)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.txtLogPath)
        Me.GroupBox3.Controls.Add(Me.txtMachineID)
        Me.GroupBox3.Controls.Add(Me.btnSetMachine)
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox3, resources.GetString("GroupBox3.ToolTip"))
        '
        'btnLogPath
        '
        resources.ApplyResources(Me.btnLogPath, "btnLogPath")
        Me.btnLogPath.AutoEllipsis = True
        Me.btnLogPath.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.OpenFile
        Me.btnLogPath.FlatAppearance.BorderSize = 0
        Me.btnLogPath.Name = "btnLogPath"
        Me.ToolTip1.SetToolTip(Me.btnLogPath, resources.GetString("btnLogPath.ToolTip"))
        Me.btnLogPath.UseVisualStyleBackColor = True
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Name = "Label8"
        Me.ToolTip1.SetToolTip(Me.Label8, resources.GetString("Label8.ToolTip"))
        '
        'Label29
        '
        resources.ApplyResources(Me.Label29, "Label29")
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Name = "Label29"
        Me.ToolTip1.SetToolTip(Me.Label29, resources.GetString("Label29.ToolTip"))
        '
        'txtLogPath
        '
        resources.ApplyResources(Me.txtLogPath, "txtLogPath")
        Me.txtLogPath.BackColor = System.Drawing.Color.White
        Me.txtLogPath.Name = "txtLogPath"
        Me.ToolTip1.SetToolTip(Me.txtLogPath, resources.GetString("txtLogPath.ToolTip"))
        '
        'txtMachineID
        '
        resources.ApplyResources(Me.txtMachineID, "txtMachineID")
        Me.txtMachineID.BackColor = System.Drawing.Color.White
        Me.txtMachineID.Name = "txtMachineID"
        Me.ToolTip1.SetToolTip(Me.txtMachineID, resources.GetString("txtMachineID.ToolTip"))
        '
        'btnSetMachine
        '
        resources.ApplyResources(Me.btnSetMachine, "btnSetMachine")
        Me.btnSetMachine.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetMachine.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetMachine.Name = "btnSetMachine"
        Me.ToolTip1.SetToolTip(Me.btnSetMachine, resources.GetString("btnSetMachine.ToolTip"))
        Me.btnSetMachine.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        resources.ApplyResources(Me.GroupBox5, "GroupBox5")
        Me.GroupBox5.Controls.Add(Me.lblLanguageType)
        Me.GroupBox5.Controls.Add(Me.btnSaveLanguage)
        Me.GroupBox5.Controls.Add(Me.cboLanguageType)
        Me.GroupBox5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox5, resources.GetString("GroupBox5.ToolTip"))
        '
        'lblLanguageType
        '
        resources.ApplyResources(Me.lblLanguageType, "lblLanguageType")
        Me.lblLanguageType.BackColor = System.Drawing.Color.Transparent
        Me.lblLanguageType.Name = "lblLanguageType"
        Me.ToolTip1.SetToolTip(Me.lblLanguageType, resources.GetString("lblLanguageType.ToolTip"))
        '
        'btnSaveLanguage
        '
        resources.ApplyResources(Me.btnSaveLanguage, "btnSaveLanguage")
        Me.btnSaveLanguage.BackColor = System.Drawing.SystemColors.Control
        Me.btnSaveLanguage.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSaveLanguage.Name = "btnSaveLanguage"
        Me.ToolTip1.SetToolTip(Me.btnSaveLanguage, resources.GetString("btnSaveLanguage.ToolTip"))
        Me.btnSaveLanguage.UseVisualStyleBackColor = True
        '
        'cboLanguageType
        '
        resources.ApplyResources(Me.cboLanguageType, "cboLanguageType")
        Me.cboLanguageType.BackColor = System.Drawing.Color.White
        Me.cboLanguageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLanguageType.FormattingEnabled = True
        Me.cboLanguageType.Name = "cboLanguageType"
        Me.ToolTip1.SetToolTip(Me.cboLanguageType, resources.GetString("cboLanguageType.ToolTip"))
        '
        'tabCommonFunction
        '
        resources.ApplyResources(Me.tabCommonFunction, "tabCommonFunction")
        Me.tabCommonFunction.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabCommonFunction.Controls.Add(Me.GroupBox8)
        Me.tabCommonFunction.Controls.Add(Me.GroupBox1)
        Me.tabCommonFunction.Controls.Add(Me.grpConveyor)
        Me.tabCommonFunction.Controls.Add(Me.gpbTolerance)
        Me.tabCommonFunction.Controls.Add(Me.gpTiltSet)
        Me.tabCommonFunction.Controls.Add(Me.grpWeight)
        Me.tabCommonFunction.Controls.Add(Me.grpStableTime)
        Me.tabCommonFunction.Controls.Add(Me.gpbFunction)
        Me.tabCommonFunction.Controls.Add(Me.grpSafeTemperature)
        Me.tabCommonFunction.Controls.Add(Me.gpbAugerValveCTThreshold)
        Me.tabCommonFunction.Controls.Add(Me.gpbDetectGlue)
        Me.tabCommonFunction.Controls.Add(Me.btnBack2)
        Me.tabCommonFunction.Name = "tabCommonFunction"
        Me.ToolTip1.SetToolTip(Me.tabCommonFunction, resources.GetString("tabCommonFunction.ToolTip"))
        '
        'GroupBox8
        '
        resources.ApplyResources(Me.GroupBox8, "GroupBox8")
        Me.GroupBox8.Controls.Add(Me.txtPathMultiple)
        Me.GroupBox8.Controls.Add(Me.Label31)
        Me.GroupBox8.Controls.Add(Me.btnExtendOn)
        Me.GroupBox8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox8, resources.GetString("GroupBox8.ToolTip"))
        '
        'txtPathMultiple
        '
        resources.ApplyResources(Me.txtPathMultiple, "txtPathMultiple")
        Me.txtPathMultiple.BackColor = System.Drawing.Color.White
        Me.txtPathMultiple.Name = "txtPathMultiple"
        Me.ToolTip1.SetToolTip(Me.txtPathMultiple, resources.GetString("txtPathMultiple.ToolTip"))
        '
        'Label31
        '
        resources.ApplyResources(Me.Label31, "Label31")
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Name = "Label31"
        Me.ToolTip1.SetToolTip(Me.Label31, resources.GetString("Label31.ToolTip"))
        '
        'btnExtendOn
        '
        resources.ApplyResources(Me.btnExtendOn, "btnExtendOn")
        Me.btnExtendOn.BackColor = System.Drawing.SystemColors.Control
        Me.btnExtendOn.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnExtendOn.Name = "btnExtendOn"
        Me.ToolTip1.SetToolTip(Me.btnExtendOn, resources.GetString("btnExtendOn.ToolTip"))
        Me.btnExtendOn.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.Label30)
        Me.GroupBox1.Controls.Add(Me.Label36)
        Me.GroupBox1.Controls.Add(Me.Label34)
        Me.GroupBox1.Controls.Add(Me.Label32)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtMaxDispVelocity)
        Me.GroupBox1.Controls.Add(Me.txtMaxCrossDevieVelocity)
        Me.GroupBox1.Controls.Add(Me.txtMaxCrossStepVelocity)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label33)
        Me.GroupBox1.Controls.Add(Me.btnSetVelocity)
        Me.GroupBox1.Controls.Add(Me.Label35)
        Me.GroupBox1.Controls.Add(Me.txtConstVelocityTime)
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'Label30
        '
        resources.ApplyResources(Me.Label30, "Label30")
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Name = "Label30"
        Me.ToolTip1.SetToolTip(Me.Label30, resources.GetString("Label30.ToolTip"))
        '
        'Label36
        '
        resources.ApplyResources(Me.Label36, "Label36")
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Name = "Label36"
        Me.ToolTip1.SetToolTip(Me.Label36, resources.GetString("Label36.ToolTip"))
        '
        'Label34
        '
        resources.ApplyResources(Me.Label34, "Label34")
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Name = "Label34"
        Me.ToolTip1.SetToolTip(Me.Label34, resources.GetString("Label34.ToolTip"))
        '
        'Label32
        '
        resources.ApplyResources(Me.Label32, "Label32")
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Name = "Label32"
        Me.ToolTip1.SetToolTip(Me.Label32, resources.GetString("Label32.ToolTip"))
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Name = "Label15"
        Me.ToolTip1.SetToolTip(Me.Label15, resources.GetString("Label15.ToolTip"))
        '
        'txtMaxDispVelocity
        '
        resources.ApplyResources(Me.txtMaxDispVelocity, "txtMaxDispVelocity")
        Me.txtMaxDispVelocity.BackColor = System.Drawing.Color.White
        Me.txtMaxDispVelocity.Name = "txtMaxDispVelocity"
        Me.ToolTip1.SetToolTip(Me.txtMaxDispVelocity, resources.GetString("txtMaxDispVelocity.ToolTip"))
        '
        'txtMaxCrossDevieVelocity
        '
        resources.ApplyResources(Me.txtMaxCrossDevieVelocity, "txtMaxCrossDevieVelocity")
        Me.txtMaxCrossDevieVelocity.BackColor = System.Drawing.Color.White
        Me.txtMaxCrossDevieVelocity.Name = "txtMaxCrossDevieVelocity"
        Me.ToolTip1.SetToolTip(Me.txtMaxCrossDevieVelocity, resources.GetString("txtMaxCrossDevieVelocity.ToolTip"))
        '
        'txtMaxCrossStepVelocity
        '
        resources.ApplyResources(Me.txtMaxCrossStepVelocity, "txtMaxCrossStepVelocity")
        Me.txtMaxCrossStepVelocity.BackColor = System.Drawing.Color.White
        Me.txtMaxCrossStepVelocity.Name = "txtMaxCrossStepVelocity"
        Me.ToolTip1.SetToolTip(Me.txtMaxCrossStepVelocity, resources.GetString("txtMaxCrossStepVelocity.ToolTip"))
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Name = "Label14"
        Me.ToolTip1.SetToolTip(Me.Label14, resources.GetString("Label14.ToolTip"))
        '
        'Label33
        '
        resources.ApplyResources(Me.Label33, "Label33")
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Name = "Label33"
        Me.ToolTip1.SetToolTip(Me.Label33, resources.GetString("Label33.ToolTip"))
        '
        'btnSetVelocity
        '
        resources.ApplyResources(Me.btnSetVelocity, "btnSetVelocity")
        Me.btnSetVelocity.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetVelocity.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetVelocity.FlatAppearance.BorderSize = 0
        Me.btnSetVelocity.Name = "btnSetVelocity"
        Me.ToolTip1.SetToolTip(Me.btnSetVelocity, resources.GetString("btnSetVelocity.ToolTip"))
        Me.btnSetVelocity.UseVisualStyleBackColor = True
        '
        'Label35
        '
        resources.ApplyResources(Me.Label35, "Label35")
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Name = "Label35"
        Me.ToolTip1.SetToolTip(Me.Label35, resources.GetString("Label35.ToolTip"))
        '
        'txtConstVelocityTime
        '
        resources.ApplyResources(Me.txtConstVelocityTime, "txtConstVelocityTime")
        Me.txtConstVelocityTime.BackColor = System.Drawing.Color.White
        Me.txtConstVelocityTime.Name = "txtConstVelocityTime"
        Me.ToolTip1.SetToolTip(Me.txtConstVelocityTime, resources.GetString("txtConstVelocityTime.ToolTip"))
        '
        'grpConveyor
        '
        resources.ApplyResources(Me.grpConveyor, "grpConveyor")
        Me.grpConveyor.Controls.Add(Me.btnSetCvDirection)
        Me.grpConveyor.Controls.Add(Me.GroupBox7)
        Me.grpConveyor.Controls.Add(Me.GroupBox6)
        Me.grpConveyor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpConveyor.Name = "grpConveyor"
        Me.grpConveyor.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpConveyor, resources.GetString("grpConveyor.ToolTip"))
        '
        'btnSetCvDirection
        '
        resources.ApplyResources(Me.btnSetCvDirection, "btnSetCvDirection")
        Me.btnSetCvDirection.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetCvDirection.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetCvDirection.Name = "btnSetCvDirection"
        Me.ToolTip1.SetToolTip(Me.btnSetCvDirection, resources.GetString("btnSetCvDirection.ToolTip"))
        Me.btnSetCvDirection.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        resources.ApplyResources(Me.GroupBox7, "GroupBox7")
        Me.GroupBox7.Controls.Add(Me.rbtnUnloadReversal)
        Me.GroupBox7.Controls.Add(Me.rbtnUnloadForward)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox7, resources.GetString("GroupBox7.ToolTip"))
        '
        'rbtnUnloadReversal
        '
        resources.ApplyResources(Me.rbtnUnloadReversal, "rbtnUnloadReversal")
        Me.rbtnUnloadReversal.Name = "rbtnUnloadReversal"
        Me.ToolTip1.SetToolTip(Me.rbtnUnloadReversal, resources.GetString("rbtnUnloadReversal.ToolTip"))
        Me.rbtnUnloadReversal.UseVisualStyleBackColor = True
        '
        'rbtnUnloadForward
        '
        resources.ApplyResources(Me.rbtnUnloadForward, "rbtnUnloadForward")
        Me.rbtnUnloadForward.Checked = True
        Me.rbtnUnloadForward.Name = "rbtnUnloadForward"
        Me.rbtnUnloadForward.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rbtnUnloadForward, resources.GetString("rbtnUnloadForward.ToolTip"))
        Me.rbtnUnloadForward.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        resources.ApplyResources(Me.GroupBox6, "GroupBox6")
        Me.GroupBox6.Controls.Add(Me.rbtnLoadReversal)
        Me.GroupBox6.Controls.Add(Me.rbtnLoadForward)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox6, resources.GetString("GroupBox6.ToolTip"))
        '
        'rbtnLoadReversal
        '
        resources.ApplyResources(Me.rbtnLoadReversal, "rbtnLoadReversal")
        Me.rbtnLoadReversal.Name = "rbtnLoadReversal"
        Me.ToolTip1.SetToolTip(Me.rbtnLoadReversal, resources.GetString("rbtnLoadReversal.ToolTip"))
        Me.rbtnLoadReversal.UseVisualStyleBackColor = True
        '
        'rbtnLoadForward
        '
        resources.ApplyResources(Me.rbtnLoadForward, "rbtnLoadForward")
        Me.rbtnLoadForward.Checked = True
        Me.rbtnLoadForward.Name = "rbtnLoadForward"
        Me.rbtnLoadForward.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rbtnLoadForward, resources.GetString("rbtnLoadForward.ToolTip"))
        Me.rbtnLoadForward.UseVisualStyleBackColor = True
        '
        'gpbTolerance
        '
        resources.ApplyResources(Me.gpbTolerance, "gpbTolerance")
        Me.gpbTolerance.Controls.Add(Me.Label37)
        Me.gpbTolerance.Controls.Add(Me.Label38)
        Me.gpbTolerance.Controls.Add(Me.txtPrecisionTolerance)
        Me.gpbTolerance.Controls.Add(Me.lblTemperatureToleranceUnit)
        Me.gpbTolerance.Controls.Add(Me.lblTemperatureTolerance)
        Me.gpbTolerance.Controls.Add(Me.txtTemperatureTolerance)
        Me.gpbTolerance.Controls.Add(Me.lblPressureToleranceUnit)
        Me.gpbTolerance.Controls.Add(Me.lblPressureTolerance)
        Me.gpbTolerance.Controls.Add(Me.txtPressureTolerance)
        Me.gpbTolerance.Controls.Add(Me.lblTriggerToleranceUnit)
        Me.gpbTolerance.Controls.Add(Me.lblMotionToleranceUnit)
        Me.gpbTolerance.Controls.Add(Me.btnTolerance)
        Me.gpbTolerance.Controls.Add(Me.lblTirggerTolerance)
        Me.gpbTolerance.Controls.Add(Me.lblMotionTolerance)
        Me.gpbTolerance.Controls.Add(Me.txtMotionTolerance)
        Me.gpbTolerance.Controls.Add(Me.txtTirggerTolerance)
        Me.gpbTolerance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gpbTolerance.Name = "gpbTolerance"
        Me.gpbTolerance.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gpbTolerance, resources.GetString("gpbTolerance.ToolTip"))
        '
        'Label37
        '
        resources.ApplyResources(Me.Label37, "Label37")
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Name = "Label37"
        Me.ToolTip1.SetToolTip(Me.Label37, resources.GetString("Label37.ToolTip"))
        '
        'Label38
        '
        resources.ApplyResources(Me.Label38, "Label38")
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Name = "Label38"
        Me.ToolTip1.SetToolTip(Me.Label38, resources.GetString("Label38.ToolTip"))
        '
        'txtPrecisionTolerance
        '
        resources.ApplyResources(Me.txtPrecisionTolerance, "txtPrecisionTolerance")
        Me.txtPrecisionTolerance.BackColor = System.Drawing.Color.White
        Me.txtPrecisionTolerance.Name = "txtPrecisionTolerance"
        Me.ToolTip1.SetToolTip(Me.txtPrecisionTolerance, resources.GetString("txtPrecisionTolerance.ToolTip"))
        '
        'lblTemperatureToleranceUnit
        '
        resources.ApplyResources(Me.lblTemperatureToleranceUnit, "lblTemperatureToleranceUnit")
        Me.lblTemperatureToleranceUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblTemperatureToleranceUnit.Name = "lblTemperatureToleranceUnit"
        Me.ToolTip1.SetToolTip(Me.lblTemperatureToleranceUnit, resources.GetString("lblTemperatureToleranceUnit.ToolTip"))
        '
        'lblTemperatureTolerance
        '
        resources.ApplyResources(Me.lblTemperatureTolerance, "lblTemperatureTolerance")
        Me.lblTemperatureTolerance.BackColor = System.Drawing.Color.Transparent
        Me.lblTemperatureTolerance.Name = "lblTemperatureTolerance"
        Me.ToolTip1.SetToolTip(Me.lblTemperatureTolerance, resources.GetString("lblTemperatureTolerance.ToolTip"))
        '
        'txtTemperatureTolerance
        '
        resources.ApplyResources(Me.txtTemperatureTolerance, "txtTemperatureTolerance")
        Me.txtTemperatureTolerance.BackColor = System.Drawing.Color.White
        Me.txtTemperatureTolerance.Name = "txtTemperatureTolerance"
        Me.ToolTip1.SetToolTip(Me.txtTemperatureTolerance, resources.GetString("txtTemperatureTolerance.ToolTip"))
        '
        'lblPressureToleranceUnit
        '
        resources.ApplyResources(Me.lblPressureToleranceUnit, "lblPressureToleranceUnit")
        Me.lblPressureToleranceUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblPressureToleranceUnit.Name = "lblPressureToleranceUnit"
        Me.ToolTip1.SetToolTip(Me.lblPressureToleranceUnit, resources.GetString("lblPressureToleranceUnit.ToolTip"))
        '
        'lblPressureTolerance
        '
        resources.ApplyResources(Me.lblPressureTolerance, "lblPressureTolerance")
        Me.lblPressureTolerance.BackColor = System.Drawing.Color.Transparent
        Me.lblPressureTolerance.Name = "lblPressureTolerance"
        Me.ToolTip1.SetToolTip(Me.lblPressureTolerance, resources.GetString("lblPressureTolerance.ToolTip"))
        '
        'txtPressureTolerance
        '
        resources.ApplyResources(Me.txtPressureTolerance, "txtPressureTolerance")
        Me.txtPressureTolerance.BackColor = System.Drawing.Color.White
        Me.txtPressureTolerance.Name = "txtPressureTolerance"
        Me.ToolTip1.SetToolTip(Me.txtPressureTolerance, resources.GetString("txtPressureTolerance.ToolTip"))
        '
        'lblTriggerToleranceUnit
        '
        resources.ApplyResources(Me.lblTriggerToleranceUnit, "lblTriggerToleranceUnit")
        Me.lblTriggerToleranceUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblTriggerToleranceUnit.Name = "lblTriggerToleranceUnit"
        Me.ToolTip1.SetToolTip(Me.lblTriggerToleranceUnit, resources.GetString("lblTriggerToleranceUnit.ToolTip"))
        '
        'lblMotionToleranceUnit
        '
        resources.ApplyResources(Me.lblMotionToleranceUnit, "lblMotionToleranceUnit")
        Me.lblMotionToleranceUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblMotionToleranceUnit.Name = "lblMotionToleranceUnit"
        Me.ToolTip1.SetToolTip(Me.lblMotionToleranceUnit, resources.GetString("lblMotionToleranceUnit.ToolTip"))
        '
        'btnTolerance
        '
        resources.ApplyResources(Me.btnTolerance, "btnTolerance")
        Me.btnTolerance.BackColor = System.Drawing.SystemColors.Control
        Me.btnTolerance.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnTolerance.FlatAppearance.BorderSize = 0
        Me.btnTolerance.Name = "btnTolerance"
        Me.ToolTip1.SetToolTip(Me.btnTolerance, resources.GetString("btnTolerance.ToolTip"))
        Me.btnTolerance.UseVisualStyleBackColor = True
        '
        'lblTirggerTolerance
        '
        resources.ApplyResources(Me.lblTirggerTolerance, "lblTirggerTolerance")
        Me.lblTirggerTolerance.BackColor = System.Drawing.Color.Transparent
        Me.lblTirggerTolerance.Name = "lblTirggerTolerance"
        Me.ToolTip1.SetToolTip(Me.lblTirggerTolerance, resources.GetString("lblTirggerTolerance.ToolTip"))
        '
        'lblMotionTolerance
        '
        resources.ApplyResources(Me.lblMotionTolerance, "lblMotionTolerance")
        Me.lblMotionTolerance.BackColor = System.Drawing.Color.Transparent
        Me.lblMotionTolerance.Name = "lblMotionTolerance"
        Me.ToolTip1.SetToolTip(Me.lblMotionTolerance, resources.GetString("lblMotionTolerance.ToolTip"))
        '
        'txtMotionTolerance
        '
        resources.ApplyResources(Me.txtMotionTolerance, "txtMotionTolerance")
        Me.txtMotionTolerance.BackColor = System.Drawing.Color.White
        Me.txtMotionTolerance.Name = "txtMotionTolerance"
        Me.ToolTip1.SetToolTip(Me.txtMotionTolerance, resources.GetString("txtMotionTolerance.ToolTip"))
        '
        'txtTirggerTolerance
        '
        resources.ApplyResources(Me.txtTirggerTolerance, "txtTirggerTolerance")
        Me.txtTirggerTolerance.BackColor = System.Drawing.Color.White
        Me.txtTirggerTolerance.Name = "txtTirggerTolerance"
        Me.ToolTip1.SetToolTip(Me.txtTirggerTolerance, resources.GetString("txtTirggerTolerance.ToolTip"))
        '
        'gpTiltSet
        '
        resources.ApplyResources(Me.gpTiltSet, "gpTiltSet")
        Me.gpTiltSet.Controls.Add(Me.rb2)
        Me.gpTiltSet.Controls.Add(Me.rb3)
        Me.gpTiltSet.Controls.Add(Me.rb4)
        Me.gpTiltSet.Controls.Add(Me.rb1)
        Me.gpTiltSet.Controls.Add(Me.btnGoTilt)
        Me.gpTiltSet.Controls.Add(Me.lbTiltStage4)
        Me.gpTiltSet.Controls.Add(Me.lbTiltStage3)
        Me.gpTiltSet.Controls.Add(Me.lbTiltStage2)
        Me.gpTiltSet.Controls.Add(Me.lbTiltStage1)
        Me.gpTiltSet.Controls.Add(Me.cboB4)
        Me.gpTiltSet.Controls.Add(Me.btnSetTilt)
        Me.gpTiltSet.Controls.Add(Me.cboB3)
        Me.gpTiltSet.Controls.Add(Me.cboB2)
        Me.gpTiltSet.Controls.Add(Me.cboB1)
        Me.gpTiltSet.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gpTiltSet.Name = "gpTiltSet"
        Me.gpTiltSet.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gpTiltSet, resources.GetString("gpTiltSet.ToolTip"))
        '
        'rb2
        '
        resources.ApplyResources(Me.rb2, "rb2")
        Me.rb2.Name = "rb2"
        Me.rb2.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rb2, resources.GetString("rb2.ToolTip"))
        Me.rb2.UseVisualStyleBackColor = True
        '
        'rb3
        '
        resources.ApplyResources(Me.rb3, "rb3")
        Me.rb3.Name = "rb3"
        Me.rb3.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rb3, resources.GetString("rb3.ToolTip"))
        Me.rb3.UseVisualStyleBackColor = True
        '
        'rb4
        '
        resources.ApplyResources(Me.rb4, "rb4")
        Me.rb4.Name = "rb4"
        Me.rb4.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rb4, resources.GetString("rb4.ToolTip"))
        Me.rb4.UseVisualStyleBackColor = True
        '
        'rb1
        '
        resources.ApplyResources(Me.rb1, "rb1")
        Me.rb1.Name = "rb1"
        Me.rb1.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rb1, resources.GetString("rb1.ToolTip"))
        Me.rb1.UseVisualStyleBackColor = True
        '
        'btnGoTilt
        '
        resources.ApplyResources(Me.btnGoTilt, "btnGoTilt")
        Me.btnGoTilt.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnGoTilt.FlatAppearance.BorderSize = 0
        Me.btnGoTilt.Name = "btnGoTilt"
        Me.ToolTip1.SetToolTip(Me.btnGoTilt, resources.GetString("btnGoTilt.ToolTip"))
        Me.btnGoTilt.UseVisualStyleBackColor = True
        '
        'lbTiltStage4
        '
        resources.ApplyResources(Me.lbTiltStage4, "lbTiltStage4")
        Me.lbTiltStage4.Name = "lbTiltStage4"
        Me.ToolTip1.SetToolTip(Me.lbTiltStage4, resources.GetString("lbTiltStage4.ToolTip"))
        '
        'lbTiltStage3
        '
        resources.ApplyResources(Me.lbTiltStage3, "lbTiltStage3")
        Me.lbTiltStage3.Name = "lbTiltStage3"
        Me.ToolTip1.SetToolTip(Me.lbTiltStage3, resources.GetString("lbTiltStage3.ToolTip"))
        '
        'lbTiltStage2
        '
        resources.ApplyResources(Me.lbTiltStage2, "lbTiltStage2")
        Me.lbTiltStage2.Name = "lbTiltStage2"
        Me.ToolTip1.SetToolTip(Me.lbTiltStage2, resources.GetString("lbTiltStage2.ToolTip"))
        '
        'lbTiltStage1
        '
        resources.ApplyResources(Me.lbTiltStage1, "lbTiltStage1")
        Me.lbTiltStage1.Name = "lbTiltStage1"
        Me.ToolTip1.SetToolTip(Me.lbTiltStage1, resources.GetString("lbTiltStage1.ToolTip"))
        '
        'cboB4
        '
        resources.ApplyResources(Me.cboB4, "cboB4")
        Me.cboB4.FormattingEnabled = True
        Me.cboB4.Name = "cboB4"
        Me.ToolTip1.SetToolTip(Me.cboB4, resources.GetString("cboB4.ToolTip"))
        '
        'btnSetTilt
        '
        resources.ApplyResources(Me.btnSetTilt, "btnSetTilt")
        Me.btnSetTilt.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetTilt.Name = "btnSetTilt"
        Me.ToolTip1.SetToolTip(Me.btnSetTilt, resources.GetString("btnSetTilt.ToolTip"))
        Me.btnSetTilt.UseVisualStyleBackColor = True
        '
        'cboB3
        '
        resources.ApplyResources(Me.cboB3, "cboB3")
        Me.cboB3.FormattingEnabled = True
        Me.cboB3.Name = "cboB3"
        Me.ToolTip1.SetToolTip(Me.cboB3, resources.GetString("cboB3.ToolTip"))
        '
        'cboB2
        '
        resources.ApplyResources(Me.cboB2, "cboB2")
        Me.cboB2.FormattingEnabled = True
        Me.cboB2.Name = "cboB2"
        Me.ToolTip1.SetToolTip(Me.cboB2, resources.GetString("cboB2.ToolTip"))
        '
        'cboB1
        '
        resources.ApplyResources(Me.cboB1, "cboB1")
        Me.cboB1.FormattingEnabled = True
        Me.cboB1.Name = "cboB1"
        Me.ToolTip1.SetToolTip(Me.cboB1, resources.GetString("cboB1.ToolTip"))
        '
        'grpWeight
        '
        resources.ApplyResources(Me.grpWeight, "grpWeight")
        Me.grpWeight.Controls.Add(Me.labCorrectionmg4)
        Me.grpWeight.Controls.Add(Me.txtCorrectionWeightLower)
        Me.grpWeight.Controls.Add(Me.labCorrectionmg3)
        Me.grpWeight.Controls.Add(Me.txtCorrectionWeightUpper)
        Me.grpWeight.Controls.Add(Me.labWeightLower)
        Me.grpWeight.Controls.Add(Me.labWeightUpper)
        Me.grpWeight.Controls.Add(Me.labCorrectionmg2)
        Me.grpWeight.Controls.Add(Me.txtCorrectionLSL)
        Me.grpWeight.Controls.Add(Me.labCorrectionmg1)
        Me.grpWeight.Controls.Add(Me.txtCorrectionUSL)
        Me.grpWeight.Controls.Add(Me.labCorrectionLSL)
        Me.grpWeight.Controls.Add(Me.btnCorrectionSet)
        Me.grpWeight.Controls.Add(Me.labCorrectionUSL)
        Me.grpWeight.Controls.Add(Me.labCorrection)
        Me.grpWeight.Controls.Add(Me.txtCorrectionSet)
        Me.grpWeight.Controls.Add(Me.cbCorrection)
        Me.grpWeight.Controls.Add(Me.cbNonCorrection)
        Me.grpWeight.Controls.Add(Me.btnWeightSet)
        Me.grpWeight.Controls.Add(Me.Label24)
        Me.grpWeight.Controls.Add(Me.txtWeighingSet)
        Me.grpWeight.Controls.Add(Me.Label23)
        Me.grpWeight.Controls.Add(Me.Label22)
        Me.grpWeight.Controls.Add(Me.cbEnableBWeightMeasure)
        Me.grpWeight.Controls.Add(Me.btnCleanWeightB)
        Me.grpWeight.Controls.Add(Me.txtWeightmeasurementB)
        Me.grpWeight.Controls.Add(Me.cbEnableAWeightMeasure)
        Me.grpWeight.Controls.Add(Me.btnCleanWeightA)
        Me.grpWeight.Controls.Add(Me.txtWeightmeasurementA)
        Me.grpWeight.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpWeight.Name = "grpWeight"
        Me.grpWeight.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpWeight, resources.GetString("grpWeight.ToolTip"))
        '
        'labCorrectionmg4
        '
        resources.ApplyResources(Me.labCorrectionmg4, "labCorrectionmg4")
        Me.labCorrectionmg4.Name = "labCorrectionmg4"
        Me.ToolTip1.SetToolTip(Me.labCorrectionmg4, resources.GetString("labCorrectionmg4.ToolTip"))
        '
        'txtCorrectionWeightLower
        '
        resources.ApplyResources(Me.txtCorrectionWeightLower, "txtCorrectionWeightLower")
        Me.txtCorrectionWeightLower.BackColor = System.Drawing.Color.White
        Me.txtCorrectionWeightLower.Name = "txtCorrectionWeightLower"
        Me.ToolTip1.SetToolTip(Me.txtCorrectionWeightLower, resources.GetString("txtCorrectionWeightLower.ToolTip"))
        '
        'labCorrectionmg3
        '
        resources.ApplyResources(Me.labCorrectionmg3, "labCorrectionmg3")
        Me.labCorrectionmg3.Name = "labCorrectionmg3"
        Me.ToolTip1.SetToolTip(Me.labCorrectionmg3, resources.GetString("labCorrectionmg3.ToolTip"))
        '
        'txtCorrectionWeightUpper
        '
        resources.ApplyResources(Me.txtCorrectionWeightUpper, "txtCorrectionWeightUpper")
        Me.txtCorrectionWeightUpper.BackColor = System.Drawing.Color.White
        Me.txtCorrectionWeightUpper.Name = "txtCorrectionWeightUpper"
        Me.ToolTip1.SetToolTip(Me.txtCorrectionWeightUpper, resources.GetString("txtCorrectionWeightUpper.ToolTip"))
        '
        'labWeightLower
        '
        resources.ApplyResources(Me.labWeightLower, "labWeightLower")
        Me.labWeightLower.BackColor = System.Drawing.Color.Transparent
        Me.labWeightLower.Name = "labWeightLower"
        Me.ToolTip1.SetToolTip(Me.labWeightLower, resources.GetString("labWeightLower.ToolTip"))
        '
        'labWeightUpper
        '
        resources.ApplyResources(Me.labWeightUpper, "labWeightUpper")
        Me.labWeightUpper.BackColor = System.Drawing.Color.Transparent
        Me.labWeightUpper.Name = "labWeightUpper"
        Me.ToolTip1.SetToolTip(Me.labWeightUpper, resources.GetString("labWeightUpper.ToolTip"))
        '
        'labCorrectionmg2
        '
        resources.ApplyResources(Me.labCorrectionmg2, "labCorrectionmg2")
        Me.labCorrectionmg2.Name = "labCorrectionmg2"
        Me.ToolTip1.SetToolTip(Me.labCorrectionmg2, resources.GetString("labCorrectionmg2.ToolTip"))
        '
        'txtCorrectionLSL
        '
        resources.ApplyResources(Me.txtCorrectionLSL, "txtCorrectionLSL")
        Me.txtCorrectionLSL.BackColor = System.Drawing.Color.White
        Me.txtCorrectionLSL.Name = "txtCorrectionLSL"
        Me.ToolTip1.SetToolTip(Me.txtCorrectionLSL, resources.GetString("txtCorrectionLSL.ToolTip"))
        '
        'labCorrectionmg1
        '
        resources.ApplyResources(Me.labCorrectionmg1, "labCorrectionmg1")
        Me.labCorrectionmg1.Name = "labCorrectionmg1"
        Me.ToolTip1.SetToolTip(Me.labCorrectionmg1, resources.GetString("labCorrectionmg1.ToolTip"))
        '
        'txtCorrectionUSL
        '
        resources.ApplyResources(Me.txtCorrectionUSL, "txtCorrectionUSL")
        Me.txtCorrectionUSL.BackColor = System.Drawing.Color.White
        Me.txtCorrectionUSL.Name = "txtCorrectionUSL"
        Me.ToolTip1.SetToolTip(Me.txtCorrectionUSL, resources.GetString("txtCorrectionUSL.ToolTip"))
        '
        'labCorrectionLSL
        '
        resources.ApplyResources(Me.labCorrectionLSL, "labCorrectionLSL")
        Me.labCorrectionLSL.BackColor = System.Drawing.Color.Transparent
        Me.labCorrectionLSL.Name = "labCorrectionLSL"
        Me.ToolTip1.SetToolTip(Me.labCorrectionLSL, resources.GetString("labCorrectionLSL.ToolTip"))
        '
        'btnCorrectionSet
        '
        resources.ApplyResources(Me.btnCorrectionSet, "btnCorrectionSet")
        Me.btnCorrectionSet.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnCorrectionSet.Name = "btnCorrectionSet"
        Me.ToolTip1.SetToolTip(Me.btnCorrectionSet, resources.GetString("btnCorrectionSet.ToolTip"))
        Me.btnCorrectionSet.UseVisualStyleBackColor = True
        '
        'labCorrectionUSL
        '
        resources.ApplyResources(Me.labCorrectionUSL, "labCorrectionUSL")
        Me.labCorrectionUSL.BackColor = System.Drawing.Color.Transparent
        Me.labCorrectionUSL.Name = "labCorrectionUSL"
        Me.ToolTip1.SetToolTip(Me.labCorrectionUSL, resources.GetString("labCorrectionUSL.ToolTip"))
        '
        'labCorrection
        '
        resources.ApplyResources(Me.labCorrection, "labCorrection")
        Me.labCorrection.Name = "labCorrection"
        Me.ToolTip1.SetToolTip(Me.labCorrection, resources.GetString("labCorrection.ToolTip"))
        '
        'txtCorrectionSet
        '
        resources.ApplyResources(Me.txtCorrectionSet, "txtCorrectionSet")
        Me.txtCorrectionSet.BackColor = System.Drawing.Color.White
        Me.txtCorrectionSet.Name = "txtCorrectionSet"
        Me.ToolTip1.SetToolTip(Me.txtCorrectionSet, resources.GetString("txtCorrectionSet.ToolTip"))
        '
        'cbCorrection
        '
        resources.ApplyResources(Me.cbCorrection, "cbCorrection")
        Me.cbCorrection.Name = "cbCorrection"
        Me.ToolTip1.SetToolTip(Me.cbCorrection, resources.GetString("cbCorrection.ToolTip"))
        Me.cbCorrection.UseVisualStyleBackColor = True
        '
        'cbNonCorrection
        '
        resources.ApplyResources(Me.cbNonCorrection, "cbNonCorrection")
        Me.cbNonCorrection.Name = "cbNonCorrection"
        Me.ToolTip1.SetToolTip(Me.cbNonCorrection, resources.GetString("cbNonCorrection.ToolTip"))
        Me.cbNonCorrection.UseVisualStyleBackColor = True
        '
        'btnWeightSet
        '
        resources.ApplyResources(Me.btnWeightSet, "btnWeightSet")
        Me.btnWeightSet.Name = "btnWeightSet"
        Me.ToolTip1.SetToolTip(Me.btnWeightSet, resources.GetString("btnWeightSet.ToolTip"))
        Me.btnWeightSet.UseVisualStyleBackColor = True
        '
        'Label24
        '
        resources.ApplyResources(Me.Label24, "Label24")
        Me.Label24.Name = "Label24"
        Me.ToolTip1.SetToolTip(Me.Label24, resources.GetString("Label24.ToolTip"))
        '
        'txtWeighingSet
        '
        resources.ApplyResources(Me.txtWeighingSet, "txtWeighingSet")
        Me.txtWeighingSet.BackColor = System.Drawing.Color.White
        Me.txtWeighingSet.Name = "txtWeighingSet"
        Me.ToolTip1.SetToolTip(Me.txtWeighingSet, resources.GetString("txtWeighingSet.ToolTip"))
        '
        'Label23
        '
        resources.ApplyResources(Me.Label23, "Label23")
        Me.Label23.Name = "Label23"
        Me.ToolTip1.SetToolTip(Me.Label23, resources.GetString("Label23.ToolTip"))
        '
        'Label22
        '
        resources.ApplyResources(Me.Label22, "Label22")
        Me.Label22.Name = "Label22"
        Me.ToolTip1.SetToolTip(Me.Label22, resources.GetString("Label22.ToolTip"))
        '
        'cbEnableBWeightMeasure
        '
        resources.ApplyResources(Me.cbEnableBWeightMeasure, "cbEnableBWeightMeasure")
        Me.cbEnableBWeightMeasure.Name = "cbEnableBWeightMeasure"
        Me.ToolTip1.SetToolTip(Me.cbEnableBWeightMeasure, resources.GetString("cbEnableBWeightMeasure.ToolTip"))
        Me.cbEnableBWeightMeasure.UseVisualStyleBackColor = True
        '
        'btnCleanWeightB
        '
        resources.ApplyResources(Me.btnCleanWeightB, "btnCleanWeightB")
        Me.btnCleanWeightB.BackColor = System.Drawing.SystemColors.Control
        Me.btnCleanWeightB.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnCleanWeightB.FlatAppearance.BorderSize = 0
        Me.btnCleanWeightB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnCleanWeightB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnCleanWeightB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCleanWeightB.Name = "btnCleanWeightB"
        Me.btnCleanWeightB.Tag = "7"
        Me.ToolTip1.SetToolTip(Me.btnCleanWeightB, resources.GetString("btnCleanWeightB.ToolTip"))
        Me.btnCleanWeightB.UseVisualStyleBackColor = True
        '
        'txtWeightmeasurementB
        '
        resources.ApplyResources(Me.txtWeightmeasurementB, "txtWeightmeasurementB")
        Me.txtWeightmeasurementB.Name = "txtWeightmeasurementB"
        Me.txtWeightmeasurementB.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtWeightmeasurementB, resources.GetString("txtWeightmeasurementB.ToolTip"))
        '
        'cbEnableAWeightMeasure
        '
        resources.ApplyResources(Me.cbEnableAWeightMeasure, "cbEnableAWeightMeasure")
        Me.cbEnableAWeightMeasure.Name = "cbEnableAWeightMeasure"
        Me.ToolTip1.SetToolTip(Me.cbEnableAWeightMeasure, resources.GetString("cbEnableAWeightMeasure.ToolTip"))
        Me.cbEnableAWeightMeasure.UseVisualStyleBackColor = True
        '
        'btnCleanWeightA
        '
        resources.ApplyResources(Me.btnCleanWeightA, "btnCleanWeightA")
        Me.btnCleanWeightA.BackColor = System.Drawing.SystemColors.Control
        Me.btnCleanWeightA.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnCleanWeightA.FlatAppearance.BorderSize = 0
        Me.btnCleanWeightA.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnCleanWeightA.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnCleanWeightA.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCleanWeightA.Name = "btnCleanWeightA"
        Me.btnCleanWeightA.Tag = "7"
        Me.ToolTip1.SetToolTip(Me.btnCleanWeightA, resources.GetString("btnCleanWeightA.ToolTip"))
        Me.btnCleanWeightA.UseVisualStyleBackColor = True
        '
        'txtWeightmeasurementA
        '
        resources.ApplyResources(Me.txtWeightmeasurementA, "txtWeightmeasurementA")
        Me.txtWeightmeasurementA.Name = "txtWeightmeasurementA"
        Me.txtWeightmeasurementA.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtWeightmeasurementA, resources.GetString("txtWeightmeasurementA.ToolTip"))
        '
        'grpStableTime
        '
        resources.ApplyResources(Me.grpStableTime, "grpStableTime")
        Me.grpStableTime.Controls.Add(Me.Label25)
        Me.grpStableTime.Controls.Add(Me.Label26)
        Me.grpStableTime.Controls.Add(Me.nmcPriorHeatTime)
        Me.grpStableTime.Controls.Add(Me.Label12)
        Me.grpStableTime.Controls.Add(Me.nmcSensorTimeOut)
        Me.grpStableTime.Controls.Add(Me.Label13)
        Me.grpStableTime.Controls.Add(Me.Label16)
        Me.grpStableTime.Controls.Add(Me.nmcLaserStableTime)
        Me.grpStableTime.Controls.Add(Me.Label17)
        Me.grpStableTime.Controls.Add(Me.btnSetTime)
        Me.grpStableTime.Controls.Add(Me.nmcCCDStableTime)
        Me.grpStableTime.Controls.Add(Me.Label18)
        Me.grpStableTime.Controls.Add(Me.Label19)
        Me.grpStableTime.Name = "grpStableTime"
        Me.grpStableTime.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpStableTime, resources.GetString("grpStableTime.ToolTip"))
        '
        'Label25
        '
        resources.ApplyResources(Me.Label25, "Label25")
        Me.Label25.Name = "Label25"
        Me.ToolTip1.SetToolTip(Me.Label25, resources.GetString("Label25.ToolTip"))
        '
        'Label26
        '
        resources.ApplyResources(Me.Label26, "Label26")
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Name = "Label26"
        Me.ToolTip1.SetToolTip(Me.Label26, resources.GetString("Label26.ToolTip"))
        '
        'nmcPriorHeatTime
        '
        resources.ApplyResources(Me.nmcPriorHeatTime, "nmcPriorHeatTime")
        Me.nmcPriorHeatTime.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcPriorHeatTime.Name = "nmcPriorHeatTime"
        Me.ToolTip1.SetToolTip(Me.nmcPriorHeatTime, resources.GetString("nmcPriorHeatTime.ToolTip"))
        Me.nmcPriorHeatTime.Value = New Decimal(New Integer() {9999, 0, 0, 0})
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        Me.ToolTip1.SetToolTip(Me.Label12, resources.GetString("Label12.ToolTip"))
        '
        'nmcSensorTimeOut
        '
        resources.ApplyResources(Me.nmcSensorTimeOut, "nmcSensorTimeOut")
        Me.nmcSensorTimeOut.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcSensorTimeOut.Name = "nmcSensorTimeOut"
        Me.ToolTip1.SetToolTip(Me.nmcSensorTimeOut, resources.GetString("nmcSensorTimeOut.ToolTip"))
        Me.nmcSensorTimeOut.Value = New Decimal(New Integer() {9999, 0, 0, 0})
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Name = "Label13"
        Me.ToolTip1.SetToolTip(Me.Label13, resources.GetString("Label13.ToolTip"))
        '
        'Label16
        '
        resources.ApplyResources(Me.Label16, "Label16")
        Me.Label16.Name = "Label16"
        Me.ToolTip1.SetToolTip(Me.Label16, resources.GetString("Label16.ToolTip"))
        '
        'nmcLaserStableTime
        '
        resources.ApplyResources(Me.nmcLaserStableTime, "nmcLaserStableTime")
        Me.nmcLaserStableTime.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcLaserStableTime.Name = "nmcLaserStableTime"
        Me.ToolTip1.SetToolTip(Me.nmcLaserStableTime, resources.GetString("nmcLaserStableTime.ToolTip"))
        Me.nmcLaserStableTime.Value = New Decimal(New Integer() {9999, 0, 0, 0})
        '
        'Label17
        '
        resources.ApplyResources(Me.Label17, "Label17")
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Name = "Label17"
        Me.ToolTip1.SetToolTip(Me.Label17, resources.GetString("Label17.ToolTip"))
        '
        'btnSetTime
        '
        resources.ApplyResources(Me.btnSetTime, "btnSetTime")
        Me.btnSetTime.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetTime.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetTime.Name = "btnSetTime"
        Me.ToolTip1.SetToolTip(Me.btnSetTime, resources.GetString("btnSetTime.ToolTip"))
        Me.btnSetTime.UseVisualStyleBackColor = True
        '
        'nmcCCDStableTime
        '
        resources.ApplyResources(Me.nmcCCDStableTime, "nmcCCDStableTime")
        Me.nmcCCDStableTime.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcCCDStableTime.Name = "nmcCCDStableTime"
        Me.ToolTip1.SetToolTip(Me.nmcCCDStableTime, resources.GetString("nmcCCDStableTime.ToolTip"))
        Me.nmcCCDStableTime.Value = New Decimal(New Integer() {9999, 0, 0, 0})
        '
        'Label18
        '
        resources.ApplyResources(Me.Label18, "Label18")
        Me.Label18.Name = "Label18"
        Me.ToolTip1.SetToolTip(Me.Label18, resources.GetString("Label18.ToolTip"))
        '
        'Label19
        '
        resources.ApplyResources(Me.Label19, "Label19")
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Name = "Label19"
        Me.ToolTip1.SetToolTip(Me.Label19, resources.GetString("Label19.ToolTip"))
        '
        'grpSafeTemperature
        '
        resources.ApplyResources(Me.grpSafeTemperature, "grpSafeTemperature")
        Me.grpSafeTemperature.Controls.Add(Me.cbEnableInitialHotPlate)
        Me.grpSafeTemperature.Controls.Add(Me.Label20)
        Me.grpSafeTemperature.Controls.Add(Me.nmcMinHotplateTemp)
        Me.grpSafeTemperature.Controls.Add(Me.Label21)
        Me.grpSafeTemperature.Controls.Add(Me.Label11)
        Me.grpSafeTemperature.Controls.Add(Me.nmcMaxHotplateTemp)
        Me.grpSafeTemperature.Controls.Add(Me.Label10)
        Me.grpSafeTemperature.Controls.Add(Me.Label6)
        Me.grpSafeTemperature.Controls.Add(Me.nmcMaxValveTemp)
        Me.grpSafeTemperature.Controls.Add(Me.Label5)
        Me.grpSafeTemperature.Controls.Add(Me.btnSetSafeTemp)
        Me.grpSafeTemperature.Controls.Add(Me.nmcSafeTemperature)
        Me.grpSafeTemperature.Controls.Add(Me.Label48)
        Me.grpSafeTemperature.Controls.Add(Me.lblSafeOpenDoor)
        Me.grpSafeTemperature.Name = "grpSafeTemperature"
        Me.grpSafeTemperature.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpSafeTemperature, resources.GetString("grpSafeTemperature.ToolTip"))
        '
        'cbEnableInitialHotPlate
        '
        resources.ApplyResources(Me.cbEnableInitialHotPlate, "cbEnableInitialHotPlate")
        Me.cbEnableInitialHotPlate.Name = "cbEnableInitialHotPlate"
        Me.ToolTip1.SetToolTip(Me.cbEnableInitialHotPlate, resources.GetString("cbEnableInitialHotPlate.ToolTip"))
        Me.cbEnableInitialHotPlate.UseVisualStyleBackColor = True
        '
        'Label20
        '
        resources.ApplyResources(Me.Label20, "Label20")
        Me.Label20.Name = "Label20"
        Me.ToolTip1.SetToolTip(Me.Label20, resources.GetString("Label20.ToolTip"))
        '
        'nmcMinHotplateTemp
        '
        resources.ApplyResources(Me.nmcMinHotplateTemp, "nmcMinHotplateTemp")
        Me.nmcMinHotplateTemp.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcMinHotplateTemp.Name = "nmcMinHotplateTemp"
        Me.ToolTip1.SetToolTip(Me.nmcMinHotplateTemp, resources.GetString("nmcMinHotplateTemp.ToolTip"))
        Me.nmcMinHotplateTemp.Value = New Decimal(New Integer() {999, 0, 0, 0})
        '
        'Label21
        '
        resources.ApplyResources(Me.Label21, "Label21")
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Name = "Label21"
        Me.ToolTip1.SetToolTip(Me.Label21, resources.GetString("Label21.ToolTip"))
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        Me.ToolTip1.SetToolTip(Me.Label11, resources.GetString("Label11.ToolTip"))
        '
        'nmcMaxHotplateTemp
        '
        resources.ApplyResources(Me.nmcMaxHotplateTemp, "nmcMaxHotplateTemp")
        Me.nmcMaxHotplateTemp.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcMaxHotplateTemp.Name = "nmcMaxHotplateTemp"
        Me.ToolTip1.SetToolTip(Me.nmcMaxHotplateTemp, resources.GetString("nmcMaxHotplateTemp.ToolTip"))
        Me.nmcMaxHotplateTemp.Value = New Decimal(New Integer() {999, 0, 0, 0})
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Name = "Label10"
        Me.ToolTip1.SetToolTip(Me.Label10, resources.GetString("Label10.ToolTip"))
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        Me.ToolTip1.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip"))
        '
        'nmcMaxValveTemp
        '
        resources.ApplyResources(Me.nmcMaxValveTemp, "nmcMaxValveTemp")
        Me.nmcMaxValveTemp.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcMaxValveTemp.Name = "nmcMaxValveTemp"
        Me.ToolTip1.SetToolTip(Me.nmcMaxValveTemp, resources.GetString("nmcMaxValveTemp.ToolTip"))
        Me.nmcMaxValveTemp.Value = New Decimal(New Integer() {999, 0, 0, 0})
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Name = "Label5"
        Me.ToolTip1.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip"))
        '
        'btnSetSafeTemp
        '
        resources.ApplyResources(Me.btnSetSafeTemp, "btnSetSafeTemp")
        Me.btnSetSafeTemp.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetSafeTemp.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetSafeTemp.Name = "btnSetSafeTemp"
        Me.ToolTip1.SetToolTip(Me.btnSetSafeTemp, resources.GetString("btnSetSafeTemp.ToolTip"))
        Me.btnSetSafeTemp.UseVisualStyleBackColor = True
        '
        'nmcSafeTemperature
        '
        resources.ApplyResources(Me.nmcSafeTemperature, "nmcSafeTemperature")
        Me.nmcSafeTemperature.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcSafeTemperature.Name = "nmcSafeTemperature"
        Me.ToolTip1.SetToolTip(Me.nmcSafeTemperature, resources.GetString("nmcSafeTemperature.ToolTip"))
        Me.nmcSafeTemperature.Value = New Decimal(New Integer() {999, 0, 0, 0})
        '
        'Label48
        '
        resources.ApplyResources(Me.Label48, "Label48")
        Me.Label48.Name = "Label48"
        Me.ToolTip1.SetToolTip(Me.Label48, resources.GetString("Label48.ToolTip"))
        '
        'lblSafeOpenDoor
        '
        resources.ApplyResources(Me.lblSafeOpenDoor, "lblSafeOpenDoor")
        Me.lblSafeOpenDoor.BackColor = System.Drawing.Color.Transparent
        Me.lblSafeOpenDoor.Name = "lblSafeOpenDoor"
        Me.ToolTip1.SetToolTip(Me.lblSafeOpenDoor, resources.GetString("lblSafeOpenDoor.ToolTip"))
        '
        'gpbDetectGlue
        '
        resources.ApplyResources(Me.gpbDetectGlue, "gpbDetectGlue")
        Me.gpbDetectGlue.Controls.Add(Me.chkIsReset)
        Me.gpbDetectGlue.Controls.Add(Me.lblGlueDetector4)
        Me.gpbDetectGlue.Controls.Add(Me.lblGlueDetector3)
        Me.gpbDetectGlue.Controls.Add(Me.lblGlueDetector2)
        Me.gpbDetectGlue.Controls.Add(Me.lblGlueDetector1)
        Me.gpbDetectGlue.Controls.Add(Me.btnBypassDetectGlueNo4)
        Me.gpbDetectGlue.Controls.Add(Me.btnBypassDetectGlueNo3)
        Me.gpbDetectGlue.Controls.Add(Me.btnBypassDetectGlueNo2)
        Me.gpbDetectGlue.Controls.Add(Me.btnBypassDetectGlueNo1)
        Me.gpbDetectGlue.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gpbDetectGlue.Name = "gpbDetectGlue"
        Me.gpbDetectGlue.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gpbDetectGlue, resources.GetString("gpbDetectGlue.ToolTip"))
        '
        'chkIsReset
        '
        resources.ApplyResources(Me.chkIsReset, "chkIsReset")
        Me.chkIsReset.Name = "chkIsReset"
        Me.ToolTip1.SetToolTip(Me.chkIsReset, resources.GetString("chkIsReset.ToolTip"))
        Me.chkIsReset.UseVisualStyleBackColor = True
        '
        'lblGlueDetector4
        '
        resources.ApplyResources(Me.lblGlueDetector4, "lblGlueDetector4")
        Me.lblGlueDetector4.Name = "lblGlueDetector4"
        Me.ToolTip1.SetToolTip(Me.lblGlueDetector4, resources.GetString("lblGlueDetector4.ToolTip"))
        '
        'lblGlueDetector3
        '
        resources.ApplyResources(Me.lblGlueDetector3, "lblGlueDetector3")
        Me.lblGlueDetector3.Name = "lblGlueDetector3"
        Me.ToolTip1.SetToolTip(Me.lblGlueDetector3, resources.GetString("lblGlueDetector3.ToolTip"))
        '
        'lblGlueDetector2
        '
        resources.ApplyResources(Me.lblGlueDetector2, "lblGlueDetector2")
        Me.lblGlueDetector2.Name = "lblGlueDetector2"
        Me.ToolTip1.SetToolTip(Me.lblGlueDetector2, resources.GetString("lblGlueDetector2.ToolTip"))
        '
        'lblGlueDetector1
        '
        resources.ApplyResources(Me.lblGlueDetector1, "lblGlueDetector1")
        Me.lblGlueDetector1.Name = "lblGlueDetector1"
        Me.ToolTip1.SetToolTip(Me.lblGlueDetector1, resources.GetString("lblGlueDetector1.ToolTip"))
        '
        'btnBypassDetectGlueNo4
        '
        resources.ApplyResources(Me.btnBypassDetectGlueNo4, "btnBypassDetectGlueNo4")
        Me.btnBypassDetectGlueNo4.BackColor = System.Drawing.SystemColors.Control
        Me.btnBypassDetectGlueNo4.FlatAppearance.BorderSize = 0
        Me.btnBypassDetectGlueNo4.Name = "btnBypassDetectGlueNo4"
        Me.ToolTip1.SetToolTip(Me.btnBypassDetectGlueNo4, resources.GetString("btnBypassDetectGlueNo4.ToolTip"))
        Me.btnBypassDetectGlueNo4.UseVisualStyleBackColor = True
        '
        'btnBypassDetectGlueNo3
        '
        resources.ApplyResources(Me.btnBypassDetectGlueNo3, "btnBypassDetectGlueNo3")
        Me.btnBypassDetectGlueNo3.BackColor = System.Drawing.SystemColors.Control
        Me.btnBypassDetectGlueNo3.FlatAppearance.BorderSize = 0
        Me.btnBypassDetectGlueNo3.Name = "btnBypassDetectGlueNo3"
        Me.ToolTip1.SetToolTip(Me.btnBypassDetectGlueNo3, resources.GetString("btnBypassDetectGlueNo3.ToolTip"))
        Me.btnBypassDetectGlueNo3.UseVisualStyleBackColor = True
        '
        'btnBypassDetectGlueNo2
        '
        resources.ApplyResources(Me.btnBypassDetectGlueNo2, "btnBypassDetectGlueNo2")
        Me.btnBypassDetectGlueNo2.BackColor = System.Drawing.SystemColors.Control
        Me.btnBypassDetectGlueNo2.FlatAppearance.BorderSize = 0
        Me.btnBypassDetectGlueNo2.Name = "btnBypassDetectGlueNo2"
        Me.ToolTip1.SetToolTip(Me.btnBypassDetectGlueNo2, resources.GetString("btnBypassDetectGlueNo2.ToolTip"))
        Me.btnBypassDetectGlueNo2.UseVisualStyleBackColor = True
        '
        'btnBypassDetectGlueNo1
        '
        resources.ApplyResources(Me.btnBypassDetectGlueNo1, "btnBypassDetectGlueNo1")
        Me.btnBypassDetectGlueNo1.BackColor = System.Drawing.SystemColors.Control
        Me.btnBypassDetectGlueNo1.FlatAppearance.BorderSize = 0
        Me.btnBypassDetectGlueNo1.Name = "btnBypassDetectGlueNo1"
        Me.ToolTip1.SetToolTip(Me.btnBypassDetectGlueNo1, resources.GetString("btnBypassDetectGlueNo1.ToolTip"))
        Me.btnBypassDetectGlueNo1.UseVisualStyleBackColor = True
        '
        'btnBack2
        '
        resources.ApplyResources(Me.btnBack2, "btnBack2")
        Me.btnBack2.BackColor = System.Drawing.SystemColors.Control
        Me.btnBack2.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnBack2.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnBack2.FlatAppearance.BorderSize = 0
        Me.btnBack2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnBack2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnBack2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnBack2.Name = "btnBack2"
        Me.ToolTip1.SetToolTip(Me.btnBack2, resources.GetString("btnBack2.ToolTip"))
        Me.btnBack2.UseVisualStyleBackColor = True
        '
        'tabCharge
        '
        resources.ApplyResources(Me.tabCharge, "tabCharge")
        Me.tabCharge.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabCharge.Name = "tabCharge"
        Me.ToolTip1.SetToolTip(Me.tabCharge, resources.GetString("tabCharge.ToolTip"))
        '
        'FolderBrowserDialog1
        '
        resources.ApplyResources(Me.FolderBrowserDialog1, "FolderBrowserDialog1")
        '
        'frmSystemSet
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnPreviousPage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSystemSet"
        Me.Tag = "2"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.gpbFunction.ResumeLayout(False)
        Me.gpbAugerValveCTThreshold.ResumeLayout(False)
        Me.gpbAugerValveCTThreshold.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tabMechanismModule.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.gpbMechanismSoftware.ResumeLayout(False)
        Me.gpbValveType.ResumeLayout(False)
        Me.tabBasicFunction.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.grpAirPressureUnit.ResumeLayout(False)
        Me.grpAirPressureUnit.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpCCDImage.ResumeLayout(False)
        Me.grpCCDImage.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.tabCommonFunction.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpConveyor.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.gpbTolerance.ResumeLayout(False)
        Me.gpbTolerance.PerformLayout()
        Me.gpTiltSet.ResumeLayout(False)
        Me.gpTiltSet.PerformLayout()
        Me.grpWeight.ResumeLayout(False)
        Me.grpWeight.PerformLayout()
        Me.grpStableTime.ResumeLayout(False)
        CType(Me.nmcPriorHeatTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcSensorTimeOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcLaserStableTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcCCDStableTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSafeTemperature.ResumeLayout(False)
        CType(Me.nmcMinHotplateTemp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcMaxHotplateTemp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcMaxValveTemp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcSafeTemperature, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpbDetectGlue.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPreviousPage As System.Windows.Forms.Button
    Friend WithEvents gpbFunction As System.Windows.Forms.GroupBox
    Friend WithEvents gpbAugerValveCTThreshold As System.Windows.Forms.GroupBox
    Friend WithEvents lblValve2CTUnit As System.Windows.Forms.Label
    Friend WithEvents lblValve1CTUnit As System.Windows.Forms.Label
    Friend WithEvents btnAugerValveCTThreshold As System.Windows.Forms.Button
    Friend WithEvents lblScrewValveNo2CTThreshold As System.Windows.Forms.Label
    Friend WithEvents lblScrewValveNo1CTThreshold As System.Windows.Forms.Label
    Friend WithEvents txtAugerValveCTThreshold1 As System.Windows.Forms.TextBox
    Friend WithEvents txtAugerValveCTThreshold2 As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabMechanismModule As System.Windows.Forms.TabPage
    Friend WithEvents gpbMechanismSoftware As System.Windows.Forms.GroupBox
    Friend WithEvents lblCCDModuleType As System.Windows.Forms.Label
    Friend WithEvents cboCCDModuleType As System.Windows.Forms.ComboBox
    Friend WithEvents lblMachineType As System.Windows.Forms.Label
    Friend WithEvents lblStageUseValveCount As System.Windows.Forms.Label
    Friend WithEvents cboStageUseValveCount As System.Windows.Forms.ComboBox
    Friend WithEvents cboMachineType As System.Windows.Forms.ComboBox
    Friend WithEvents btnMechanismModule As System.Windows.Forms.Button
    Friend WithEvents gpbValveType As System.Windows.Forms.GroupBox
    Friend WithEvents cboDispenserNo2ValveType As System.Windows.Forms.ComboBox
    Friend WithEvents lblDispenserNo2ValveType As System.Windows.Forms.Label
    Friend WithEvents cboDispenserNo1ValveType As System.Windows.Forms.ComboBox
    Friend WithEvents lblDispenserNo1ValveType As System.Windows.Forms.Label
    Friend WithEvents btnValveType As System.Windows.Forms.Button
    Friend WithEvents tabCommonFunction As System.Windows.Forms.TabPage
    Friend WithEvents gpbDetectGlue As System.Windows.Forms.GroupBox
    Friend WithEvents btnBypassDetectGlueNo2 As System.Windows.Forms.Button
    Friend WithEvents btnBypassDetectGlueNo1 As System.Windows.Forms.Button
    Friend WithEvents cboDispenserNo4ValveType As System.Windows.Forms.ComboBox
    Friend WithEvents lblDispenserNo4ValveType As System.Windows.Forms.Label
    Friend WithEvents cboDispenserNo3ValveType As System.Windows.Forms.ComboBox
    Friend WithEvents lblDispenserNo3ValveType As System.Windows.Forms.Label
    Friend WithEvents tabCharge As System.Windows.Forms.TabPage
    Friend WithEvents btnBypassDetectGlueNo4 As System.Windows.Forms.Button
    Friend WithEvents btnBypassDetectGlueNo3 As System.Windows.Forms.Button
    Friend WithEvents lblGlueDetector4 As System.Windows.Forms.Label
    Friend WithEvents lblGlueDetector3 As System.Windows.Forms.Label
    Friend WithEvents lblGlueDetector2 As System.Windows.Forms.Label
    Friend WithEvents lblValve4CTUnit As System.Windows.Forms.Label
    Friend WithEvents lblValve3CTUnit As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAugerValveCTThreshold3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAugerValveCTThreshold4 As System.Windows.Forms.TextBox
    Friend WithEvents grpSafeTemperature As System.Windows.Forms.GroupBox
    Friend WithEvents lblSafeOpenDoor As System.Windows.Forms.Label
    Friend WithEvents nmcSafeTemperature As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents btnSetSafeTemp As System.Windows.Forms.Button
    Friend WithEvents btnDisableConveyor As System.Windows.Forms.Button
    Friend WithEvents lblConveyor As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnPassLUL As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkIsReset As System.Windows.Forms.CheckBox
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnBack2 As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents nmcMaxHotplateTemp As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents nmcMaxValveTemp As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents grpStableTime As System.Windows.Forms.GroupBox
    Friend WithEvents nmcSensorTimeOut As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents nmcLaserStableTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnSetTime As System.Windows.Forms.Button
    Friend WithEvents nmcCCDStableTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSaveMachineType As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents nmcMinHotplateTemp As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cboDispenserNo4ValveTypeModel As System.Windows.Forms.ComboBox
    Friend WithEvents cboDispenserNo3ValveTypeModel As System.Windows.Forms.ComboBox
    Friend WithEvents cboDispenserNo2ValveTypeModel As System.Windows.Forms.ComboBox
    Friend WithEvents cboDispenserNo1ValveTypeModel As System.Windows.Forms.ComboBox
    Friend WithEvents grpWeight As System.Windows.Forms.GroupBox
    Friend WithEvents btnWeightSet As System.Windows.Forms.Button
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtWeighingSet As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cbEnableBWeightMeasure As System.Windows.Forms.CheckBox
    Friend WithEvents btnCleanWeightB As System.Windows.Forms.Button
    Friend WithEvents txtWeightmeasurementB As System.Windows.Forms.TextBox
    Friend WithEvents cbEnableAWeightMeasure As System.Windows.Forms.CheckBox
    Friend WithEvents btnCleanWeightA As System.Windows.Forms.Button
    Friend WithEvents txtWeightmeasurementA As System.Windows.Forms.TextBox
    Friend WithEvents gpTiltSet As System.Windows.Forms.GroupBox
    Friend WithEvents cboB1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboB4 As System.Windows.Forms.ComboBox
    Friend WithEvents btnSetTilt As System.Windows.Forms.Button
    Friend WithEvents cboB3 As System.Windows.Forms.ComboBox
    Friend WithEvents cboB2 As System.Windows.Forms.ComboBox
    Friend WithEvents lbTiltStage4 As System.Windows.Forms.Label
    Friend WithEvents lbTiltStage3 As System.Windows.Forms.Label
    Friend WithEvents lbTiltStage2 As System.Windows.Forms.Label
    Friend WithEvents lbTiltStage1 As System.Windows.Forms.Label
    Friend WithEvents btnGoTilt As System.Windows.Forms.Button
    Friend WithEvents rb2 As System.Windows.Forms.RadioButton
    Friend WithEvents rb3 As System.Windows.Forms.RadioButton
    Friend WithEvents rb4 As System.Windows.Forms.RadioButton
    Friend WithEvents rb1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents nmcPriorHeatTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents cbEnableInitialHotPlate As System.Windows.Forms.CheckBox
    Friend WithEvents labCorrection As System.Windows.Forms.Label
    Friend WithEvents txtCorrectionSet As System.Windows.Forms.TextBox
    Friend WithEvents cbCorrection As System.Windows.Forms.CheckBox
    Friend WithEvents cbNonCorrection As System.Windows.Forms.CheckBox
    Friend WithEvents btnManualMap As System.Windows.Forms.Button
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents btnCorrectionSet As System.Windows.Forms.Button
    Friend WithEvents labCorrectionUSL As System.Windows.Forms.Label
    Friend WithEvents labCorrectionmg4 As System.Windows.Forms.Label
    Friend WithEvents txtCorrectionWeightLower As System.Windows.Forms.TextBox
    Friend WithEvents labCorrectionmg3 As System.Windows.Forms.Label
    Friend WithEvents txtCorrectionWeightUpper As System.Windows.Forms.TextBox
    Friend WithEvents labWeightLower As System.Windows.Forms.Label
    Friend WithEvents labWeightUpper As System.Windows.Forms.Label
    Friend WithEvents labCorrectionmg2 As System.Windows.Forms.Label
    Friend WithEvents txtCorrectionLSL As System.Windows.Forms.TextBox
    Friend WithEvents labCorrectionmg1 As System.Windows.Forms.Label
    Friend WithEvents txtCorrectionUSL As System.Windows.Forms.TextBox
    Friend WithEvents labCorrectionLSL As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboZHeightDevice As System.Windows.Forms.ComboBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnUnloadReversal As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnUnloadForward As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnLoadReversal As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnLoadForward As System.Windows.Forms.RadioButton
    Friend WithEvents btnSetCvDirection As System.Windows.Forms.Button
    Friend WithEvents gpbTolerance As System.Windows.Forms.GroupBox
    Friend WithEvents lblTriggerToleranceUnit As System.Windows.Forms.Label
    Friend WithEvents lblMotionToleranceUnit As System.Windows.Forms.Label
    Friend WithEvents btnTolerance As System.Windows.Forms.Button
    Friend WithEvents lblTirggerTolerance As System.Windows.Forms.Label
    Friend WithEvents lblMotionTolerance As System.Windows.Forms.Label
    Friend WithEvents txtMotionTolerance As System.Windows.Forms.TextBox
    Friend WithEvents txtTirggerTolerance As System.Windows.Forms.TextBox
    Friend WithEvents grpConveyor As System.Windows.Forms.GroupBox
    Friend WithEvents tabBasicFunction As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents lblLanguageType As System.Windows.Forms.Label
    Friend WithEvents btnSaveLanguage As System.Windows.Forms.Button
    Friend WithEvents cboLanguageType As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnLogPath As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtLogPath As System.Windows.Forms.TextBox
    Friend WithEvents txtMachineID As System.Windows.Forms.TextBox
    Friend WithEvents btnSetMachine As System.Windows.Forms.Button
    Friend WithEvents grpCCDImage As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbCCDImageSaveMode As System.Windows.Forms.ComboBox
    Friend WithEvents btnCCDImagePath As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCCDImageFolderPath As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveImage As System.Windows.Forms.Button
    Friend WithEvents grpAirPressureUnit As System.Windows.Forms.GroupBox
    Friend WithEvents rdbTorr As System.Windows.Forms.RadioButton
    Friend WithEvents rdbBar As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPsi As System.Windows.Forms.RadioButton
    Friend WithEvents rdbKg As System.Windows.Forms.RadioButton
    Friend WithEvents rdbMPa As System.Windows.Forms.RadioButton
    Friend WithEvents btnSetAirPressureUnit As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbInch As System.Windows.Forms.RadioButton
    Friend WithEvents rdbmm As System.Windows.Forms.RadioButton
    Friend WithEvents btnSetPositionUnit As System.Windows.Forms.Button
    Friend WithEvents btnBack3 As System.Windows.Forms.Button
    Friend WithEvents cboMultiDispense As System.Windows.Forms.ComboBox
    Friend WithEvents lblMultiDispense As System.Windows.Forms.Label
    Friend WithEvents cboCCDOnFly As System.Windows.Forms.ComboBox
    Friend WithEvents lblCCDOnFly As System.Windows.Forms.Label
    Friend WithEvents cboConveyorModel As System.Windows.Forms.ComboBox
    Friend WithEvents lblConveyorModel As System.Windows.Forms.Label
    Friend WithEvents lblContinueRun As System.Windows.Forms.Label
    Friend WithEvents btnContinueRun As System.Windows.Forms.Button
    Friend WithEvents lblPressureToleranceUnit As System.Windows.Forms.Label
    Friend WithEvents lblPressureTolerance As System.Windows.Forms.Label
    Friend WithEvents txtPressureTolerance As System.Windows.Forms.TextBox
    Friend WithEvents lblCleanMachine As System.Windows.Forms.Label
    Friend WithEvents btnCleanDevice As System.Windows.Forms.Button
    Friend WithEvents lblTemperatureToleranceUnit As System.Windows.Forms.Label
    Friend WithEvents lblTemperatureTolerance As System.Windows.Forms.Label
    Friend WithEvents txtTemperatureTolerance As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtConstVelocityTime As System.Windows.Forms.TextBox
    Friend WithEvents lblGlueDetector1 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtMaxCrossStepVelocity As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtMaxDispVelocity As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxCrossDevieVelocity As System.Windows.Forms.TextBox
    Friend WithEvents btnSetVelocity As System.Windows.Forms.Button
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPathMultiple As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents btnExtendOn As System.Windows.Forms.Button
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtPrecisionTolerance As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents btnMapPath As System.Windows.Forms.Button
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtMapDataPath As System.Windows.Forms.TextBox
    Friend WithEvents btnSetMapPath As System.Windows.Forms.Button
    Friend WithEvents cboxMapDataModel As System.Windows.Forms.ComboBox
End Class

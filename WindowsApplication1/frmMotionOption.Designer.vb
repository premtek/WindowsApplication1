<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMotionOption
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMotionOption))
        Me.grpIOStatus = New System.Windows.Forms.GroupBox()
        Me.palSD = New System.Windows.Forms.Panel()
        Me.palERC = New System.Windows.Forms.Panel()
        Me.palDIR = New System.Windows.Forms.Panel()
        Me.palEZ = New System.Windows.Forms.Panel()
        Me.palTRIG = New System.Windows.Forms.Panel()
        Me.palNSEL = New System.Windows.Forms.Panel()
        Me.palLTC = New System.Windows.Forms.Panel()
        Me.palALRM = New System.Windows.Forms.Panel()
        Me.palPEL = New System.Windows.Forms.Panel()
        Me.palPSEL = New System.Windows.Forms.Panel()
        Me.palCLR = New System.Windows.Forms.Panel()
        Me.palPCS = New System.Windows.Forms.Panel()
        Me.palORG = New System.Windows.Forms.Panel()
        Me.palSVON = New System.Windows.Forms.Panel()
        Me.palALM = New System.Windows.Forms.Panel()
        Me.palEMG = New System.Windows.Forms.Panel()
        Me.palNEL = New System.Windows.Forms.Panel()
        Me.palINP = New System.Windows.Forms.Panel()
        Me.palRDY = New System.Windows.Forms.Panel()
        Me.lblTRIG = New System.Windows.Forms.Label()
        Me.lblNSEL = New System.Windows.Forms.Label()
        Me.lblPSEL = New System.Windows.Forms.Label()
        Me.lblALRM = New System.Windows.Forms.Label()
        Me.lblSVON = New System.Windows.Forms.Label()
        Me.lblINP = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblSD = New System.Windows.Forms.Label()
        Me.lblLTC = New System.Windows.Forms.Label()
        Me.lblCLR = New System.Windows.Forms.Label()
        Me.lblEZ = New System.Windows.Forms.Label()
        Me.lblERC = New System.Windows.Forms.Label()
        Me.lblPCS = New System.Windows.Forms.Label()
        Me.lblEMG = New System.Windows.Forms.Label()
        Me.lblDIR = New System.Windows.Forms.Label()
        Me.lblORG = New System.Windows.Forms.Label()
        Me.lblNEL = New System.Windows.Forms.Label()
        Me.lblPEL = New System.Windows.Forms.Label()
        Me.lblALM = New System.Windows.Forms.Label()
        Me.lblRDY = New System.Windows.Forms.Label()
        Me.grpAxisParameter = New System.Windows.Forms.GroupBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabBasic = New System.Windows.Forms.TabPage()
        Me.txtAxisName = New System.Windows.Forms.TextBox()
        Me.lblAxisName = New System.Windows.Forms.Label()
        Me.cmbCoordinate = New System.Windows.Forms.ComboBox()
        Me.nmcCardNo = New System.Windows.Forms.NumericUpDown()
        Me.nmcAxisNo = New System.Windows.Forms.NumericUpDown()
        Me.lblCoordinate = New System.Windows.Forms.Label()
        Me.lblIsEncoderExist = New System.Windows.Forms.Label()
        Me.lblPulseOutReverse = New System.Windows.Forms.Label()
        Me.lblPulseInReverse = New System.Windows.Forms.Label()
        Me.cmbIsEncoderExist = New System.Windows.Forms.ComboBox()
        Me.cmbPulseOutReverse = New System.Windows.Forms.ComboBox()
        Me.cmbPulseInReverse = New System.Windows.Forms.ComboBox()
        Me.cmbButtonDirection = New System.Windows.Forms.ComboBox()
        Me.cmbMoveDirection = New System.Windows.Forms.ComboBox()
        Me.nmcScale = New System.Windows.Forms.NumericUpDown()
        Me.cmbMotorType = New System.Windows.Forms.ComboBox()
        Me.cmbCardType = New System.Windows.Forms.ComboBox()
        Me.cmbAxisType = New System.Windows.Forms.ComboBox()
        Me.lblMotorType = New System.Windows.Forms.Label()
        Me.lblCardType = New System.Windows.Forms.Label()
        Me.lblAxisType = New System.Windows.Forms.Label()
        Me.cmbPulseOutMode = New System.Windows.Forms.ComboBox()
        Me.lblPulseOutMode = New System.Windows.Forms.Label()
        Me.cmbPulseInMaxFreq = New System.Windows.Forms.ComboBox()
        Me.lblPulseInMaxFreq = New System.Windows.Forms.Label()
        Me.cmbPulseInMode = New System.Windows.Forms.ComboBox()
        Me.lblButtonDirection = New System.Windows.Forms.Label()
        Me.lblMoveDirection = New System.Windows.Forms.Label()
        Me.lblPulseInMode = New System.Windows.Forms.Label()
        Me.lblScaleUnit = New System.Windows.Forms.Label()
        Me.lblCardNo = New System.Windows.Forms.Label()
        Me.lblAxisNo = New System.Windows.Forms.Label()
        Me.lblScale = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblHomeDecUnit = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblHomeAccUnit = New System.Windows.Forms.Label()
        Me.lblHomeVelHighUnit = New System.Windows.Forms.Label()
        Me.lblHomeVelLowUnit = New System.Windows.Forms.Label()
        Me.lblHomeCrossDistanceUnit = New System.Windows.Forms.Label()
        Me.cmbHomeReset = New System.Windows.Forms.ComboBox()
        Me.lblHomeReset = New System.Windows.Forms.Label()
        Me.cmbHomeDirection = New System.Windows.Forms.ComboBox()
        Me.lblHomeDirection = New System.Windows.Forms.Label()
        Me.nmcHomeDec = New System.Windows.Forms.NumericUpDown()
        Me.nmcHomeAcc = New System.Windows.Forms.NumericUpDown()
        Me.nmcHomeVelHigh = New System.Windows.Forms.NumericUpDown()
        Me.nmcHomeVelLow = New System.Windows.Forms.NumericUpDown()
        Me.lblHomeDec = New System.Windows.Forms.Label()
        Me.lblHomeAcc = New System.Windows.Forms.Label()
        Me.lblHomeVelHigh = New System.Windows.Forms.Label()
        Me.lblHomeVelLow = New System.Windows.Forms.Label()
        Me.lblHomeExSwitchMode = New System.Windows.Forms.Label()
        Me.nmcHomeExSwitchMode = New System.Windows.Forms.NumericUpDown()
        Me.nmcHomeMode = New System.Windows.Forms.NumericUpDown()
        Me.nmcHomeCrossDistance = New System.Windows.Forms.NumericUpDown()
        Me.lblHomeCrossDistance = New System.Windows.Forms.Label()
        Me.lblHomeMode = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.lblMaxVelUnit = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblMaxDecUnit = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblMacAccUnit = New System.Windows.Forms.Label()
        Me.cmbHLMTLogic = New System.Windows.Forms.ComboBox()
        Me.lblHLMTLogic = New System.Windows.Forms.Label()
        Me.cmbHLMTEnable = New System.Windows.Forms.ComboBox()
        Me.lblHLMTEnable = New System.Windows.Forms.Label()
        Me.nmcMaxVel = New System.Windows.Forms.NumericUpDown()
        Me.nmcMaxDec = New System.Windows.Forms.NumericUpDown()
        Me.nmcMaxAcc = New System.Windows.Forms.NumericUpDown()
        Me.lblMaxVel = New System.Windows.Forms.Label()
        Me.lblMacDec = New System.Windows.Forms.Label()
        Me.lblMaxAcc = New System.Windows.Forms.Label()
        Me.nmcSPEL = New System.Windows.Forms.NumericUpDown()
        Me.nmcSNEL = New System.Windows.Forms.NumericUpDown()
        Me.cmbHLMTStopMode = New System.Windows.Forms.ComboBox()
        Me.lblSNELUnit = New System.Windows.Forms.Label()
        Me.lblSPELUnit = New System.Windows.Forms.Label()
        Me.lblSNEL = New System.Windows.Forms.Label()
        Me.lblHLMTStopMode = New System.Windows.Forms.Label()
        Me.lblSPEL = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.cmbEZLogic = New System.Windows.Forms.ComboBox()
        Me.lblEZLogic = New System.Windows.Forms.Label()
        Me.cmbORGLogic = New System.Windows.Forms.ComboBox()
        Me.lblORGLogic = New System.Windows.Forms.Label()
        Me.cmbERCLogic = New System.Windows.Forms.ComboBox()
        Me.cmbLatchLogic = New System.Windows.Forms.ComboBox()
        Me.cmbINPLogic = New System.Windows.Forms.ComboBox()
        Me.cmbALMLogic = New System.Windows.Forms.ComboBox()
        Me.lblERCLogic = New System.Windows.Forms.Label()
        Me.lblLatchLogic = New System.Windows.Forms.Label()
        Me.lblINPLogic = New System.Windows.Forms.Label()
        Me.lblALMLogic = New System.Windows.Forms.Label()
        Me.cmbBacklashEnable = New System.Windows.Forms.ComboBox()
        Me.lblBacklashEnable = New System.Windows.Forms.Label()
        Me.cmbERCEnable = New System.Windows.Forms.ComboBox()
        Me.lblERCEnable = New System.Windows.Forms.Label()
        Me.cmbLatchEnable = New System.Windows.Forms.ComboBox()
        Me.lblLatchEnable = New System.Windows.Forms.Label()
        Me.cmbINPEnable = New System.Windows.Forms.ComboBox()
        Me.lblINPEnable = New System.Windows.Forms.Label()
        Me.cmbALMEnable = New System.Windows.Forms.ComboBox()
        Me.lblALMEnable = New System.Windows.Forms.Label()
        Me.cmbALMStopMode = New System.Windows.Forms.ComboBox()
        Me.lblALMStopMode = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.nmcPPU = New System.Windows.Forms.NumericUpDown()
        Me.lblPPUUnit = New System.Windows.Forms.Label()
        Me.lblINPStable = New System.Windows.Forms.Label()
        Me.lblPPU = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.lblDecelerationUnit = New System.Windows.Forms.Label()
        Me.lblAccelerationUnit = New System.Windows.Forms.Label()
        Me.lblINPStableUnit = New System.Windows.Forms.Label()
        Me.lblVelocityHighUnit = New System.Windows.Forms.Label()
        Me.lblVelocityLowUnit = New System.Windows.Forms.Label()
        Me.nmcDecRatio = New System.Windows.Forms.NumericUpDown()
        Me.nmcAccRatio = New System.Windows.Forms.NumericUpDown()
        Me.nmcINPStable = New System.Windows.Forms.NumericUpDown()
        Me.nmcDec = New System.Windows.Forms.NumericUpDown()
        Me.nmcAcc = New System.Windows.Forms.NumericUpDown()
        Me.nmcVelHigh = New System.Windows.Forms.NumericUpDown()
        Me.lblDecRatio = New System.Windows.Forms.Label()
        Me.lblDec = New System.Windows.Forms.Label()
        Me.lblAccRatio = New System.Windows.Forms.Label()
        Me.lblAcc = New System.Windows.Forms.Label()
        Me.lblVelHigh = New System.Windows.Forms.Label()
        Me.nmcVelLow = New System.Windows.Forms.NumericUpDown()
        Me.lblVelLow = New System.Windows.Forms.Label()
        Me.cmbExternalDriveAxis = New System.Windows.Forms.ComboBox()
        Me.cmbExternalDriveEnable = New System.Windows.Forms.ComboBox()
        Me.lblExternalDriveEnable = New System.Windows.Forms.Label()
        Me.cmbExternalPulseInMode = New System.Windows.Forms.ComboBox()
        Me.lblExternalDriveAxis = New System.Windows.Forms.Label()
        Me.lblExternalPulseInMode = New System.Windows.Forms.Label()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.cmbIN5StopMode = New System.Windows.Forms.ComboBox()
        Me.cmbIN4StopMode = New System.Windows.Forms.ComboBox()
        Me.cmbIN2StopMode = New System.Windows.Forms.ComboBox()
        Me.cmbIN1StopMode = New System.Windows.Forms.ComboBox()
        Me.cmbIN5StopLogic = New System.Windows.Forms.ComboBox()
        Me.cmbIN4StopLogic = New System.Windows.Forms.ComboBox()
        Me.cmbIN2StopLogic = New System.Windows.Forms.ComboBox()
        Me.cmbIN1StopLogic = New System.Windows.Forms.ComboBox()
        Me.cmbIN5StopEnable = New System.Windows.Forms.ComboBox()
        Me.cmbIN4StopEnable = New System.Windows.Forms.ComboBox()
        Me.lblIN5StopMode = New System.Windows.Forms.Label()
        Me.cmbIN2StopEnable = New System.Windows.Forms.ComboBox()
        Me.lblIN4StopMode = New System.Windows.Forms.Label()
        Me.cmbIN1StopEnable = New System.Windows.Forms.ComboBox()
        Me.lblIN5StopLogic = New System.Windows.Forms.Label()
        Me.lblIN2StopMode = New System.Windows.Forms.Label()
        Me.lblIN4StopLogic = New System.Windows.Forms.Label()
        Me.lblIN5StopEnable = New System.Windows.Forms.Label()
        Me.lblIN2StopLogic = New System.Windows.Forms.Label()
        Me.lblIN4StopEnable = New System.Windows.Forms.Label()
        Me.lblIN1StopMode = New System.Windows.Forms.Label()
        Me.lblIN2StopEnable = New System.Windows.Forms.Label()
        Me.lblIN1StopLogic = New System.Windows.Forms.Label()
        Me.lblIN1StopEnable = New System.Windows.Forms.Label()
        Me.tmrOption = New System.Windows.Forms.Timer(Me.components)
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpIOStatus.SuspendLayout()
        Me.grpAxisParameter.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tabBasic.SuspendLayout()
        CType(Me.nmcCardNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcAxisNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcScale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.nmcHomeDec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcHomeAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcHomeVelHigh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcHomeVelLow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcHomeExSwitchMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcHomeMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcHomeCrossDistance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.nmcMaxVel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcMaxDec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcMaxAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcSPEL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcSNEL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        CType(Me.nmcPPU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcDecRatio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcAccRatio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcINPStable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcDec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcVelHigh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcVelLow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpIOStatus
        '
        resources.ApplyResources(Me.grpIOStatus, "grpIOStatus")
        Me.grpIOStatus.Controls.Add(Me.palSD)
        Me.grpIOStatus.Controls.Add(Me.palERC)
        Me.grpIOStatus.Controls.Add(Me.palDIR)
        Me.grpIOStatus.Controls.Add(Me.palEZ)
        Me.grpIOStatus.Controls.Add(Me.palTRIG)
        Me.grpIOStatus.Controls.Add(Me.palNSEL)
        Me.grpIOStatus.Controls.Add(Me.palLTC)
        Me.grpIOStatus.Controls.Add(Me.palALRM)
        Me.grpIOStatus.Controls.Add(Me.palPEL)
        Me.grpIOStatus.Controls.Add(Me.palPSEL)
        Me.grpIOStatus.Controls.Add(Me.palCLR)
        Me.grpIOStatus.Controls.Add(Me.palPCS)
        Me.grpIOStatus.Controls.Add(Me.palORG)
        Me.grpIOStatus.Controls.Add(Me.palSVON)
        Me.grpIOStatus.Controls.Add(Me.palALM)
        Me.grpIOStatus.Controls.Add(Me.palEMG)
        Me.grpIOStatus.Controls.Add(Me.palNEL)
        Me.grpIOStatus.Controls.Add(Me.palINP)
        Me.grpIOStatus.Controls.Add(Me.palRDY)
        Me.grpIOStatus.Controls.Add(Me.lblTRIG)
        Me.grpIOStatus.Controls.Add(Me.lblNSEL)
        Me.grpIOStatus.Controls.Add(Me.lblPSEL)
        Me.grpIOStatus.Controls.Add(Me.lblALRM)
        Me.grpIOStatus.Controls.Add(Me.lblSVON)
        Me.grpIOStatus.Controls.Add(Me.lblINP)
        Me.grpIOStatus.Controls.Add(Me.Label14)
        Me.grpIOStatus.Controls.Add(Me.lblSD)
        Me.grpIOStatus.Controls.Add(Me.lblLTC)
        Me.grpIOStatus.Controls.Add(Me.lblCLR)
        Me.grpIOStatus.Controls.Add(Me.lblEZ)
        Me.grpIOStatus.Controls.Add(Me.lblERC)
        Me.grpIOStatus.Controls.Add(Me.lblPCS)
        Me.grpIOStatus.Controls.Add(Me.lblEMG)
        Me.grpIOStatus.Controls.Add(Me.lblDIR)
        Me.grpIOStatus.Controls.Add(Me.lblORG)
        Me.grpIOStatus.Controls.Add(Me.lblNEL)
        Me.grpIOStatus.Controls.Add(Me.lblPEL)
        Me.grpIOStatus.Controls.Add(Me.lblALM)
        Me.grpIOStatus.Controls.Add(Me.lblRDY)
        Me.grpIOStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpIOStatus.Name = "grpIOStatus"
        Me.grpIOStatus.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpIOStatus, resources.GetString("grpIOStatus.ToolTip"))
        '
        'palSD
        '
        resources.ApplyResources(Me.palSD, "palSD")
        Me.palSD.Name = "palSD"
        Me.ToolTip1.SetToolTip(Me.palSD, resources.GetString("palSD.ToolTip"))
        '
        'palERC
        '
        resources.ApplyResources(Me.palERC, "palERC")
        Me.palERC.Name = "palERC"
        Me.ToolTip1.SetToolTip(Me.palERC, resources.GetString("palERC.ToolTip"))
        '
        'palDIR
        '
        resources.ApplyResources(Me.palDIR, "palDIR")
        Me.palDIR.Name = "palDIR"
        Me.ToolTip1.SetToolTip(Me.palDIR, resources.GetString("palDIR.ToolTip"))
        '
        'palEZ
        '
        resources.ApplyResources(Me.palEZ, "palEZ")
        Me.palEZ.Name = "palEZ"
        Me.ToolTip1.SetToolTip(Me.palEZ, resources.GetString("palEZ.ToolTip"))
        '
        'palTRIG
        '
        resources.ApplyResources(Me.palTRIG, "palTRIG")
        Me.palTRIG.Name = "palTRIG"
        Me.ToolTip1.SetToolTip(Me.palTRIG, resources.GetString("palTRIG.ToolTip"))
        '
        'palNSEL
        '
        resources.ApplyResources(Me.palNSEL, "palNSEL")
        Me.palNSEL.Name = "palNSEL"
        Me.ToolTip1.SetToolTip(Me.palNSEL, resources.GetString("palNSEL.ToolTip"))
        '
        'palLTC
        '
        resources.ApplyResources(Me.palLTC, "palLTC")
        Me.palLTC.Name = "palLTC"
        Me.ToolTip1.SetToolTip(Me.palLTC, resources.GetString("palLTC.ToolTip"))
        '
        'palALRM
        '
        resources.ApplyResources(Me.palALRM, "palALRM")
        Me.palALRM.Name = "palALRM"
        Me.ToolTip1.SetToolTip(Me.palALRM, resources.GetString("palALRM.ToolTip"))
        '
        'palPEL
        '
        resources.ApplyResources(Me.palPEL, "palPEL")
        Me.palPEL.Name = "palPEL"
        Me.ToolTip1.SetToolTip(Me.palPEL, resources.GetString("palPEL.ToolTip"))
        '
        'palPSEL
        '
        resources.ApplyResources(Me.palPSEL, "palPSEL")
        Me.palPSEL.Name = "palPSEL"
        Me.ToolTip1.SetToolTip(Me.palPSEL, resources.GetString("palPSEL.ToolTip"))
        '
        'palCLR
        '
        resources.ApplyResources(Me.palCLR, "palCLR")
        Me.palCLR.Name = "palCLR"
        Me.ToolTip1.SetToolTip(Me.palCLR, resources.GetString("palCLR.ToolTip"))
        '
        'palPCS
        '
        resources.ApplyResources(Me.palPCS, "palPCS")
        Me.palPCS.Name = "palPCS"
        Me.ToolTip1.SetToolTip(Me.palPCS, resources.GetString("palPCS.ToolTip"))
        '
        'palORG
        '
        resources.ApplyResources(Me.palORG, "palORG")
        Me.palORG.Name = "palORG"
        Me.ToolTip1.SetToolTip(Me.palORG, resources.GetString("palORG.ToolTip"))
        '
        'palSVON
        '
        resources.ApplyResources(Me.palSVON, "palSVON")
        Me.palSVON.Name = "palSVON"
        Me.ToolTip1.SetToolTip(Me.palSVON, resources.GetString("palSVON.ToolTip"))
        '
        'palALM
        '
        resources.ApplyResources(Me.palALM, "palALM")
        Me.palALM.Name = "palALM"
        Me.ToolTip1.SetToolTip(Me.palALM, resources.GetString("palALM.ToolTip"))
        '
        'palEMG
        '
        resources.ApplyResources(Me.palEMG, "palEMG")
        Me.palEMG.Name = "palEMG"
        Me.ToolTip1.SetToolTip(Me.palEMG, resources.GetString("palEMG.ToolTip"))
        '
        'palNEL
        '
        resources.ApplyResources(Me.palNEL, "palNEL")
        Me.palNEL.Name = "palNEL"
        Me.ToolTip1.SetToolTip(Me.palNEL, resources.GetString("palNEL.ToolTip"))
        '
        'palINP
        '
        resources.ApplyResources(Me.palINP, "palINP")
        Me.palINP.Name = "palINP"
        Me.ToolTip1.SetToolTip(Me.palINP, resources.GetString("palINP.ToolTip"))
        '
        'palRDY
        '
        resources.ApplyResources(Me.palRDY, "palRDY")
        Me.palRDY.Name = "palRDY"
        Me.ToolTip1.SetToolTip(Me.palRDY, resources.GetString("palRDY.ToolTip"))
        '
        'lblTRIG
        '
        resources.ApplyResources(Me.lblTRIG, "lblTRIG")
        Me.lblTRIG.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblTRIG.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblTRIG.Name = "lblTRIG"
        Me.ToolTip1.SetToolTip(Me.lblTRIG, resources.GetString("lblTRIG.ToolTip"))
        '
        'lblNSEL
        '
        resources.ApplyResources(Me.lblNSEL, "lblNSEL")
        Me.lblNSEL.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblNSEL.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblNSEL.Name = "lblNSEL"
        Me.ToolTip1.SetToolTip(Me.lblNSEL, resources.GetString("lblNSEL.ToolTip"))
        '
        'lblPSEL
        '
        resources.ApplyResources(Me.lblPSEL, "lblPSEL")
        Me.lblPSEL.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblPSEL.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblPSEL.Name = "lblPSEL"
        Me.ToolTip1.SetToolTip(Me.lblPSEL, resources.GetString("lblPSEL.ToolTip"))
        '
        'lblALRM
        '
        resources.ApplyResources(Me.lblALRM, "lblALRM")
        Me.lblALRM.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblALRM.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblALRM.Name = "lblALRM"
        Me.ToolTip1.SetToolTip(Me.lblALRM, resources.GetString("lblALRM.ToolTip"))
        '
        'lblSVON
        '
        resources.ApplyResources(Me.lblSVON, "lblSVON")
        Me.lblSVON.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblSVON.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblSVON.Name = "lblSVON"
        Me.ToolTip1.SetToolTip(Me.lblSVON, resources.GetString("lblSVON.ToolTip"))
        '
        'lblINP
        '
        resources.ApplyResources(Me.lblINP, "lblINP")
        Me.lblINP.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblINP.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblINP.Name = "lblINP"
        Me.ToolTip1.SetToolTip(Me.lblINP, resources.GetString("lblINP.ToolTip"))
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.BackColor = System.Drawing.Color.White
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Name = "Label14"
        Me.ToolTip1.SetToolTip(Me.Label14, resources.GetString("Label14.ToolTip"))
        '
        'lblSD
        '
        resources.ApplyResources(Me.lblSD, "lblSD")
        Me.lblSD.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblSD.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblSD.Name = "lblSD"
        Me.ToolTip1.SetToolTip(Me.lblSD, resources.GetString("lblSD.ToolTip"))
        '
        'lblLTC
        '
        resources.ApplyResources(Me.lblLTC, "lblLTC")
        Me.lblLTC.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblLTC.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLTC.Name = "lblLTC"
        Me.ToolTip1.SetToolTip(Me.lblLTC, resources.GetString("lblLTC.ToolTip"))
        '
        'lblCLR
        '
        resources.ApplyResources(Me.lblCLR, "lblCLR")
        Me.lblCLR.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblCLR.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCLR.Name = "lblCLR"
        Me.ToolTip1.SetToolTip(Me.lblCLR, resources.GetString("lblCLR.ToolTip"))
        '
        'lblEZ
        '
        resources.ApplyResources(Me.lblEZ, "lblEZ")
        Me.lblEZ.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblEZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblEZ.Name = "lblEZ"
        Me.ToolTip1.SetToolTip(Me.lblEZ, resources.GetString("lblEZ.ToolTip"))
        '
        'lblERC
        '
        resources.ApplyResources(Me.lblERC, "lblERC")
        Me.lblERC.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblERC.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblERC.Name = "lblERC"
        Me.ToolTip1.SetToolTip(Me.lblERC, resources.GetString("lblERC.ToolTip"))
        '
        'lblPCS
        '
        resources.ApplyResources(Me.lblPCS, "lblPCS")
        Me.lblPCS.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblPCS.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblPCS.Name = "lblPCS"
        Me.ToolTip1.SetToolTip(Me.lblPCS, resources.GetString("lblPCS.ToolTip"))
        '
        'lblEMG
        '
        resources.ApplyResources(Me.lblEMG, "lblEMG")
        Me.lblEMG.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblEMG.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblEMG.Name = "lblEMG"
        Me.ToolTip1.SetToolTip(Me.lblEMG, resources.GetString("lblEMG.ToolTip"))
        '
        'lblDIR
        '
        resources.ApplyResources(Me.lblDIR, "lblDIR")
        Me.lblDIR.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblDIR.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblDIR.Name = "lblDIR"
        Me.ToolTip1.SetToolTip(Me.lblDIR, resources.GetString("lblDIR.ToolTip"))
        '
        'lblORG
        '
        resources.ApplyResources(Me.lblORG, "lblORG")
        Me.lblORG.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblORG.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblORG.Name = "lblORG"
        Me.ToolTip1.SetToolTip(Me.lblORG, resources.GetString("lblORG.ToolTip"))
        '
        'lblNEL
        '
        resources.ApplyResources(Me.lblNEL, "lblNEL")
        Me.lblNEL.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblNEL.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblNEL.Name = "lblNEL"
        Me.ToolTip1.SetToolTip(Me.lblNEL, resources.GetString("lblNEL.ToolTip"))
        '
        'lblPEL
        '
        resources.ApplyResources(Me.lblPEL, "lblPEL")
        Me.lblPEL.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblPEL.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblPEL.Name = "lblPEL"
        Me.ToolTip1.SetToolTip(Me.lblPEL, resources.GetString("lblPEL.ToolTip"))
        '
        'lblALM
        '
        resources.ApplyResources(Me.lblALM, "lblALM")
        Me.lblALM.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblALM.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblALM.Name = "lblALM"
        Me.ToolTip1.SetToolTip(Me.lblALM, resources.GetString("lblALM.ToolTip"))
        '
        'lblRDY
        '
        resources.ApplyResources(Me.lblRDY, "lblRDY")
        Me.lblRDY.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblRDY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblRDY.Name = "lblRDY"
        Me.ToolTip1.SetToolTip(Me.lblRDY, resources.GetString("lblRDY.ToolTip"))
        '
        'grpAxisParameter
        '
        resources.ApplyResources(Me.grpAxisParameter, "grpAxisParameter")
        Me.grpAxisParameter.Controls.Add(Me.TabControl1)
        Me.grpAxisParameter.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpAxisParameter.Name = "grpAxisParameter"
        Me.grpAxisParameter.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpAxisParameter, resources.GetString("grpAxisParameter.ToolTip"))
        '
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.tabBasic)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.ToolTip1.SetToolTip(Me.TabControl1, resources.GetString("TabControl1.ToolTip"))
        '
        'tabBasic
        '
        resources.ApplyResources(Me.tabBasic, "tabBasic")
        Me.tabBasic.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabBasic.Controls.Add(Me.txtAxisName)
        Me.tabBasic.Controls.Add(Me.lblAxisName)
        Me.tabBasic.Controls.Add(Me.cmbCoordinate)
        Me.tabBasic.Controls.Add(Me.nmcCardNo)
        Me.tabBasic.Controls.Add(Me.nmcAxisNo)
        Me.tabBasic.Controls.Add(Me.lblCoordinate)
        Me.tabBasic.Controls.Add(Me.lblIsEncoderExist)
        Me.tabBasic.Controls.Add(Me.lblPulseOutReverse)
        Me.tabBasic.Controls.Add(Me.lblPulseInReverse)
        Me.tabBasic.Controls.Add(Me.cmbIsEncoderExist)
        Me.tabBasic.Controls.Add(Me.cmbPulseOutReverse)
        Me.tabBasic.Controls.Add(Me.cmbPulseInReverse)
        Me.tabBasic.Controls.Add(Me.cmbButtonDirection)
        Me.tabBasic.Controls.Add(Me.cmbMoveDirection)
        Me.tabBasic.Controls.Add(Me.nmcScale)
        Me.tabBasic.Controls.Add(Me.cmbMotorType)
        Me.tabBasic.Controls.Add(Me.cmbCardType)
        Me.tabBasic.Controls.Add(Me.cmbAxisType)
        Me.tabBasic.Controls.Add(Me.lblMotorType)
        Me.tabBasic.Controls.Add(Me.lblCardType)
        Me.tabBasic.Controls.Add(Me.lblAxisType)
        Me.tabBasic.Controls.Add(Me.cmbPulseOutMode)
        Me.tabBasic.Controls.Add(Me.lblPulseOutMode)
        Me.tabBasic.Controls.Add(Me.cmbPulseInMaxFreq)
        Me.tabBasic.Controls.Add(Me.lblPulseInMaxFreq)
        Me.tabBasic.Controls.Add(Me.cmbPulseInMode)
        Me.tabBasic.Controls.Add(Me.lblButtonDirection)
        Me.tabBasic.Controls.Add(Me.lblMoveDirection)
        Me.tabBasic.Controls.Add(Me.lblPulseInMode)
        Me.tabBasic.Controls.Add(Me.lblScaleUnit)
        Me.tabBasic.Controls.Add(Me.lblCardNo)
        Me.tabBasic.Controls.Add(Me.lblAxisNo)
        Me.tabBasic.Controls.Add(Me.lblScale)
        Me.tabBasic.Name = "tabBasic"
        Me.ToolTip1.SetToolTip(Me.tabBasic, resources.GetString("tabBasic.ToolTip"))
        '
        'txtAxisName
        '
        resources.ApplyResources(Me.txtAxisName, "txtAxisName")
        Me.txtAxisName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAxisName.Name = "txtAxisName"
        Me.ToolTip1.SetToolTip(Me.txtAxisName, resources.GetString("txtAxisName.ToolTip"))
        '
        'lblAxisName
        '
        resources.ApplyResources(Me.lblAxisName, "lblAxisName")
        Me.lblAxisName.Name = "lblAxisName"
        Me.ToolTip1.SetToolTip(Me.lblAxisName, resources.GetString("lblAxisName.ToolTip"))
        '
        'cmbCoordinate
        '
        resources.ApplyResources(Me.cmbCoordinate, "cmbCoordinate")
        Me.cmbCoordinate.BackColor = System.Drawing.Color.White
        Me.cmbCoordinate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCoordinate.FormattingEnabled = True
        Me.cmbCoordinate.Items.AddRange(New Object() {resources.GetString("cmbCoordinate.Items"), resources.GetString("cmbCoordinate.Items1"), resources.GetString("cmbCoordinate.Items2"), resources.GetString("cmbCoordinate.Items3"), resources.GetString("cmbCoordinate.Items4"), resources.GetString("cmbCoordinate.Items5")})
        Me.cmbCoordinate.Name = "cmbCoordinate"
        Me.ToolTip1.SetToolTip(Me.cmbCoordinate, resources.GetString("cmbCoordinate.ToolTip"))
        '
        'nmcCardNo
        '
        resources.ApplyResources(Me.nmcCardNo, "nmcCardNo")
        Me.nmcCardNo.Maximum = New Decimal(New Integer() {7, 0, 0, 0})
        Me.nmcCardNo.Name = "nmcCardNo"
        Me.ToolTip1.SetToolTip(Me.nmcCardNo, resources.GetString("nmcCardNo.ToolTip"))
        '
        'nmcAxisNo
        '
        resources.ApplyResources(Me.nmcAxisNo, "nmcAxisNo")
        Me.nmcAxisNo.Maximum = New Decimal(New Integer() {7, 0, 0, 0})
        Me.nmcAxisNo.Name = "nmcAxisNo"
        Me.ToolTip1.SetToolTip(Me.nmcAxisNo, resources.GetString("nmcAxisNo.ToolTip"))
        '
        'lblCoordinate
        '
        resources.ApplyResources(Me.lblCoordinate, "lblCoordinate")
        Me.lblCoordinate.Name = "lblCoordinate"
        Me.ToolTip1.SetToolTip(Me.lblCoordinate, resources.GetString("lblCoordinate.ToolTip"))
        '
        'lblIsEncoderExist
        '
        resources.ApplyResources(Me.lblIsEncoderExist, "lblIsEncoderExist")
        Me.lblIsEncoderExist.Name = "lblIsEncoderExist"
        Me.ToolTip1.SetToolTip(Me.lblIsEncoderExist, resources.GetString("lblIsEncoderExist.ToolTip"))
        '
        'lblPulseOutReverse
        '
        resources.ApplyResources(Me.lblPulseOutReverse, "lblPulseOutReverse")
        Me.lblPulseOutReverse.Name = "lblPulseOutReverse"
        Me.ToolTip1.SetToolTip(Me.lblPulseOutReverse, resources.GetString("lblPulseOutReverse.ToolTip"))
        '
        'lblPulseInReverse
        '
        resources.ApplyResources(Me.lblPulseInReverse, "lblPulseInReverse")
        Me.lblPulseInReverse.Name = "lblPulseInReverse"
        Me.ToolTip1.SetToolTip(Me.lblPulseInReverse, resources.GetString("lblPulseInReverse.ToolTip"))
        '
        'cmbIsEncoderExist
        '
        resources.ApplyResources(Me.cmbIsEncoderExist, "cmbIsEncoderExist")
        Me.cmbIsEncoderExist.BackColor = System.Drawing.Color.White
        Me.cmbIsEncoderExist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIsEncoderExist.FormattingEnabled = True
        Me.cmbIsEncoderExist.Items.AddRange(New Object() {resources.GetString("cmbIsEncoderExist.Items"), resources.GetString("cmbIsEncoderExist.Items1")})
        Me.cmbIsEncoderExist.Name = "cmbIsEncoderExist"
        Me.ToolTip1.SetToolTip(Me.cmbIsEncoderExist, resources.GetString("cmbIsEncoderExist.ToolTip"))
        '
        'cmbPulseOutReverse
        '
        resources.ApplyResources(Me.cmbPulseOutReverse, "cmbPulseOutReverse")
        Me.cmbPulseOutReverse.BackColor = System.Drawing.Color.White
        Me.cmbPulseOutReverse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPulseOutReverse.FormattingEnabled = True
        Me.cmbPulseOutReverse.Items.AddRange(New Object() {resources.GetString("cmbPulseOutReverse.Items"), resources.GetString("cmbPulseOutReverse.Items1")})
        Me.cmbPulseOutReverse.Name = "cmbPulseOutReverse"
        Me.ToolTip1.SetToolTip(Me.cmbPulseOutReverse, resources.GetString("cmbPulseOutReverse.ToolTip"))
        '
        'cmbPulseInReverse
        '
        resources.ApplyResources(Me.cmbPulseInReverse, "cmbPulseInReverse")
        Me.cmbPulseInReverse.BackColor = System.Drawing.Color.White
        Me.cmbPulseInReverse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPulseInReverse.FormattingEnabled = True
        Me.cmbPulseInReverse.Items.AddRange(New Object() {resources.GetString("cmbPulseInReverse.Items"), resources.GetString("cmbPulseInReverse.Items1")})
        Me.cmbPulseInReverse.Name = "cmbPulseInReverse"
        Me.ToolTip1.SetToolTip(Me.cmbPulseInReverse, resources.GetString("cmbPulseInReverse.ToolTip"))
        '
        'cmbButtonDirection
        '
        resources.ApplyResources(Me.cmbButtonDirection, "cmbButtonDirection")
        Me.cmbButtonDirection.BackColor = System.Drawing.Color.White
        Me.cmbButtonDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbButtonDirection.FormattingEnabled = True
        Me.cmbButtonDirection.Items.AddRange(New Object() {resources.GetString("cmbButtonDirection.Items"), resources.GetString("cmbButtonDirection.Items1")})
        Me.cmbButtonDirection.Name = "cmbButtonDirection"
        Me.ToolTip1.SetToolTip(Me.cmbButtonDirection, resources.GetString("cmbButtonDirection.ToolTip"))
        '
        'cmbMoveDirection
        '
        resources.ApplyResources(Me.cmbMoveDirection, "cmbMoveDirection")
        Me.cmbMoveDirection.BackColor = System.Drawing.Color.White
        Me.cmbMoveDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMoveDirection.FormattingEnabled = True
        Me.cmbMoveDirection.Items.AddRange(New Object() {resources.GetString("cmbMoveDirection.Items"), resources.GetString("cmbMoveDirection.Items1")})
        Me.cmbMoveDirection.Name = "cmbMoveDirection"
        Me.ToolTip1.SetToolTip(Me.cmbMoveDirection, resources.GetString("cmbMoveDirection.ToolTip"))
        '
        'nmcScale
        '
        resources.ApplyResources(Me.nmcScale, "nmcScale")
        Me.nmcScale.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.nmcScale.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcScale.Name = "nmcScale"
        Me.ToolTip1.SetToolTip(Me.nmcScale, resources.GetString("nmcScale.ToolTip"))
        Me.nmcScale.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'cmbMotorType
        '
        resources.ApplyResources(Me.cmbMotorType, "cmbMotorType")
        Me.cmbMotorType.BackColor = System.Drawing.Color.White
        Me.cmbMotorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMotorType.FormattingEnabled = True
        Me.cmbMotorType.Items.AddRange(New Object() {resources.GetString("cmbMotorType.Items"), resources.GetString("cmbMotorType.Items1"), resources.GetString("cmbMotorType.Items2"), resources.GetString("cmbMotorType.Items3")})
        Me.cmbMotorType.Name = "cmbMotorType"
        Me.ToolTip1.SetToolTip(Me.cmbMotorType, resources.GetString("cmbMotorType.ToolTip"))
        '
        'cmbCardType
        '
        resources.ApplyResources(Me.cmbCardType, "cmbCardType")
        Me.cmbCardType.BackColor = System.Drawing.Color.White
        Me.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCardType.FormattingEnabled = True
        Me.cmbCardType.Items.AddRange(New Object() {resources.GetString("cmbCardType.Items"), resources.GetString("cmbCardType.Items1"), resources.GetString("cmbCardType.Items2")})
        Me.cmbCardType.Name = "cmbCardType"
        Me.ToolTip1.SetToolTip(Me.cmbCardType, resources.GetString("cmbCardType.ToolTip"))
        '
        'cmbAxisType
        '
        resources.ApplyResources(Me.cmbAxisType, "cmbAxisType")
        Me.cmbAxisType.BackColor = System.Drawing.Color.White
        Me.cmbAxisType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAxisType.FormattingEnabled = True
        Me.cmbAxisType.Items.AddRange(New Object() {resources.GetString("cmbAxisType.Items"), resources.GetString("cmbAxisType.Items1"), resources.GetString("cmbAxisType.Items2")})
        Me.cmbAxisType.Name = "cmbAxisType"
        Me.ToolTip1.SetToolTip(Me.cmbAxisType, resources.GetString("cmbAxisType.ToolTip"))
        '
        'lblMotorType
        '
        resources.ApplyResources(Me.lblMotorType, "lblMotorType")
        Me.lblMotorType.Name = "lblMotorType"
        Me.ToolTip1.SetToolTip(Me.lblMotorType, resources.GetString("lblMotorType.ToolTip"))
        '
        'lblCardType
        '
        resources.ApplyResources(Me.lblCardType, "lblCardType")
        Me.lblCardType.Name = "lblCardType"
        Me.ToolTip1.SetToolTip(Me.lblCardType, resources.GetString("lblCardType.ToolTip"))
        '
        'lblAxisType
        '
        resources.ApplyResources(Me.lblAxisType, "lblAxisType")
        Me.lblAxisType.Name = "lblAxisType"
        Me.ToolTip1.SetToolTip(Me.lblAxisType, resources.GetString("lblAxisType.ToolTip"))
        '
        'cmbPulseOutMode
        '
        resources.ApplyResources(Me.cmbPulseOutMode, "cmbPulseOutMode")
        Me.cmbPulseOutMode.BackColor = System.Drawing.Color.White
        Me.cmbPulseOutMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPulseOutMode.FormattingEnabled = True
        Me.cmbPulseOutMode.Items.AddRange(New Object() {resources.GetString("cmbPulseOutMode.Items"), resources.GetString("cmbPulseOutMode.Items1"), resources.GetString("cmbPulseOutMode.Items2"), resources.GetString("cmbPulseOutMode.Items3"), resources.GetString("cmbPulseOutMode.Items4"), resources.GetString("cmbPulseOutMode.Items5"), resources.GetString("cmbPulseOutMode.Items6"), resources.GetString("cmbPulseOutMode.Items7")})
        Me.cmbPulseOutMode.Name = "cmbPulseOutMode"
        Me.ToolTip1.SetToolTip(Me.cmbPulseOutMode, resources.GetString("cmbPulseOutMode.ToolTip"))
        '
        'lblPulseOutMode
        '
        resources.ApplyResources(Me.lblPulseOutMode, "lblPulseOutMode")
        Me.lblPulseOutMode.Name = "lblPulseOutMode"
        Me.ToolTip1.SetToolTip(Me.lblPulseOutMode, resources.GetString("lblPulseOutMode.ToolTip"))
        '
        'cmbPulseInMaxFreq
        '
        resources.ApplyResources(Me.cmbPulseInMaxFreq, "cmbPulseInMaxFreq")
        Me.cmbPulseInMaxFreq.BackColor = System.Drawing.Color.White
        Me.cmbPulseInMaxFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPulseInMaxFreq.FormattingEnabled = True
        Me.cmbPulseInMaxFreq.Items.AddRange(New Object() {resources.GetString("cmbPulseInMaxFreq.Items"), resources.GetString("cmbPulseInMaxFreq.Items1"), resources.GetString("cmbPulseInMaxFreq.Items2"), resources.GetString("cmbPulseInMaxFreq.Items3")})
        Me.cmbPulseInMaxFreq.Name = "cmbPulseInMaxFreq"
        Me.ToolTip1.SetToolTip(Me.cmbPulseInMaxFreq, resources.GetString("cmbPulseInMaxFreq.ToolTip"))
        '
        'lblPulseInMaxFreq
        '
        resources.ApplyResources(Me.lblPulseInMaxFreq, "lblPulseInMaxFreq")
        Me.lblPulseInMaxFreq.Name = "lblPulseInMaxFreq"
        Me.ToolTip1.SetToolTip(Me.lblPulseInMaxFreq, resources.GetString("lblPulseInMaxFreq.ToolTip"))
        '
        'cmbPulseInMode
        '
        resources.ApplyResources(Me.cmbPulseInMode, "cmbPulseInMode")
        Me.cmbPulseInMode.BackColor = System.Drawing.Color.White
        Me.cmbPulseInMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPulseInMode.FormattingEnabled = True
        Me.cmbPulseInMode.Items.AddRange(New Object() {resources.GetString("cmbPulseInMode.Items"), resources.GetString("cmbPulseInMode.Items1"), resources.GetString("cmbPulseInMode.Items2"), resources.GetString("cmbPulseInMode.Items3")})
        Me.cmbPulseInMode.Name = "cmbPulseInMode"
        Me.ToolTip1.SetToolTip(Me.cmbPulseInMode, resources.GetString("cmbPulseInMode.ToolTip"))
        '
        'lblButtonDirection
        '
        resources.ApplyResources(Me.lblButtonDirection, "lblButtonDirection")
        Me.lblButtonDirection.Name = "lblButtonDirection"
        Me.ToolTip1.SetToolTip(Me.lblButtonDirection, resources.GetString("lblButtonDirection.ToolTip"))
        '
        'lblMoveDirection
        '
        resources.ApplyResources(Me.lblMoveDirection, "lblMoveDirection")
        Me.lblMoveDirection.Name = "lblMoveDirection"
        Me.ToolTip1.SetToolTip(Me.lblMoveDirection, resources.GetString("lblMoveDirection.ToolTip"))
        '
        'lblPulseInMode
        '
        resources.ApplyResources(Me.lblPulseInMode, "lblPulseInMode")
        Me.lblPulseInMode.Name = "lblPulseInMode"
        Me.ToolTip1.SetToolTip(Me.lblPulseInMode, resources.GetString("lblPulseInMode.ToolTip"))
        '
        'lblScaleUnit
        '
        resources.ApplyResources(Me.lblScaleUnit, "lblScaleUnit")
        Me.lblScaleUnit.Name = "lblScaleUnit"
        Me.ToolTip1.SetToolTip(Me.lblScaleUnit, resources.GetString("lblScaleUnit.ToolTip"))
        '
        'lblCardNo
        '
        resources.ApplyResources(Me.lblCardNo, "lblCardNo")
        Me.lblCardNo.Name = "lblCardNo"
        Me.ToolTip1.SetToolTip(Me.lblCardNo, resources.GetString("lblCardNo.ToolTip"))
        '
        'lblAxisNo
        '
        resources.ApplyResources(Me.lblAxisNo, "lblAxisNo")
        Me.lblAxisNo.Name = "lblAxisNo"
        Me.ToolTip1.SetToolTip(Me.lblAxisNo, resources.GetString("lblAxisNo.ToolTip"))
        '
        'lblScale
        '
        resources.ApplyResources(Me.lblScale, "lblScale")
        Me.lblScale.Name = "lblScale"
        Me.ToolTip1.SetToolTip(Me.lblScale, resources.GetString("lblScale.ToolTip"))
        '
        'TabPage2
        '
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.lblHomeDecUnit)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.lblHomeAccUnit)
        Me.TabPage2.Controls.Add(Me.lblHomeVelHighUnit)
        Me.TabPage2.Controls.Add(Me.lblHomeVelLowUnit)
        Me.TabPage2.Controls.Add(Me.lblHomeCrossDistanceUnit)
        Me.TabPage2.Controls.Add(Me.cmbHomeReset)
        Me.TabPage2.Controls.Add(Me.lblHomeReset)
        Me.TabPage2.Controls.Add(Me.cmbHomeDirection)
        Me.TabPage2.Controls.Add(Me.lblHomeDirection)
        Me.TabPage2.Controls.Add(Me.nmcHomeDec)
        Me.TabPage2.Controls.Add(Me.nmcHomeAcc)
        Me.TabPage2.Controls.Add(Me.nmcHomeVelHigh)
        Me.TabPage2.Controls.Add(Me.nmcHomeVelLow)
        Me.TabPage2.Controls.Add(Me.lblHomeDec)
        Me.TabPage2.Controls.Add(Me.lblHomeAcc)
        Me.TabPage2.Controls.Add(Me.lblHomeVelHigh)
        Me.TabPage2.Controls.Add(Me.lblHomeVelLow)
        Me.TabPage2.Controls.Add(Me.lblHomeExSwitchMode)
        Me.TabPage2.Controls.Add(Me.nmcHomeExSwitchMode)
        Me.TabPage2.Controls.Add(Me.nmcHomeMode)
        Me.TabPage2.Controls.Add(Me.nmcHomeCrossDistance)
        Me.TabPage2.Controls.Add(Me.lblHomeCrossDistance)
        Me.TabPage2.Controls.Add(Me.lblHomeMode)
        Me.TabPage2.Name = "TabPage2"
        Me.ToolTip1.SetToolTip(Me.TabPage2, resources.GetString("TabPage2.ToolTip"))
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        Me.ToolTip1.SetToolTip(Me.Label9, resources.GetString("Label9.ToolTip"))
        '
        'lblHomeDecUnit
        '
        resources.ApplyResources(Me.lblHomeDecUnit, "lblHomeDecUnit")
        Me.lblHomeDecUnit.Name = "lblHomeDecUnit"
        Me.ToolTip1.SetToolTip(Me.lblHomeDecUnit, resources.GetString("lblHomeDecUnit.ToolTip"))
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        Me.ToolTip1.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip"))
        '
        'lblHomeAccUnit
        '
        resources.ApplyResources(Me.lblHomeAccUnit, "lblHomeAccUnit")
        Me.lblHomeAccUnit.Name = "lblHomeAccUnit"
        Me.ToolTip1.SetToolTip(Me.lblHomeAccUnit, resources.GetString("lblHomeAccUnit.ToolTip"))
        '
        'lblHomeVelHighUnit
        '
        resources.ApplyResources(Me.lblHomeVelHighUnit, "lblHomeVelHighUnit")
        Me.lblHomeVelHighUnit.Name = "lblHomeVelHighUnit"
        Me.ToolTip1.SetToolTip(Me.lblHomeVelHighUnit, resources.GetString("lblHomeVelHighUnit.ToolTip"))
        '
        'lblHomeVelLowUnit
        '
        resources.ApplyResources(Me.lblHomeVelLowUnit, "lblHomeVelLowUnit")
        Me.lblHomeVelLowUnit.Name = "lblHomeVelLowUnit"
        Me.ToolTip1.SetToolTip(Me.lblHomeVelLowUnit, resources.GetString("lblHomeVelLowUnit.ToolTip"))
        '
        'lblHomeCrossDistanceUnit
        '
        resources.ApplyResources(Me.lblHomeCrossDistanceUnit, "lblHomeCrossDistanceUnit")
        Me.lblHomeCrossDistanceUnit.Name = "lblHomeCrossDistanceUnit"
        Me.ToolTip1.SetToolTip(Me.lblHomeCrossDistanceUnit, resources.GetString("lblHomeCrossDistanceUnit.ToolTip"))
        '
        'cmbHomeReset
        '
        resources.ApplyResources(Me.cmbHomeReset, "cmbHomeReset")
        Me.cmbHomeReset.BackColor = System.Drawing.Color.White
        Me.cmbHomeReset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHomeReset.FormattingEnabled = True
        Me.cmbHomeReset.Items.AddRange(New Object() {resources.GetString("cmbHomeReset.Items"), resources.GetString("cmbHomeReset.Items1")})
        Me.cmbHomeReset.Name = "cmbHomeReset"
        Me.ToolTip1.SetToolTip(Me.cmbHomeReset, resources.GetString("cmbHomeReset.ToolTip"))
        '
        'lblHomeReset
        '
        resources.ApplyResources(Me.lblHomeReset, "lblHomeReset")
        Me.lblHomeReset.Name = "lblHomeReset"
        Me.ToolTip1.SetToolTip(Me.lblHomeReset, resources.GetString("lblHomeReset.ToolTip"))
        '
        'cmbHomeDirection
        '
        resources.ApplyResources(Me.cmbHomeDirection, "cmbHomeDirection")
        Me.cmbHomeDirection.BackColor = System.Drawing.Color.White
        Me.cmbHomeDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHomeDirection.FormattingEnabled = True
        Me.cmbHomeDirection.Items.AddRange(New Object() {resources.GetString("cmbHomeDirection.Items"), resources.GetString("cmbHomeDirection.Items1")})
        Me.cmbHomeDirection.Name = "cmbHomeDirection"
        Me.ToolTip1.SetToolTip(Me.cmbHomeDirection, resources.GetString("cmbHomeDirection.ToolTip"))
        '
        'lblHomeDirection
        '
        resources.ApplyResources(Me.lblHomeDirection, "lblHomeDirection")
        Me.lblHomeDirection.Name = "lblHomeDirection"
        Me.ToolTip1.SetToolTip(Me.lblHomeDirection, resources.GetString("lblHomeDirection.ToolTip"))
        '
        'nmcHomeDec
        '
        resources.ApplyResources(Me.nmcHomeDec, "nmcHomeDec")
        Me.nmcHomeDec.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcHomeDec.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcHomeDec.Name = "nmcHomeDec"
        Me.ToolTip1.SetToolTip(Me.nmcHomeDec, resources.GetString("nmcHomeDec.ToolTip"))
        Me.nmcHomeDec.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmcHomeAcc
        '
        resources.ApplyResources(Me.nmcHomeAcc, "nmcHomeAcc")
        Me.nmcHomeAcc.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcHomeAcc.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcHomeAcc.Name = "nmcHomeAcc"
        Me.ToolTip1.SetToolTip(Me.nmcHomeAcc, resources.GetString("nmcHomeAcc.ToolTip"))
        Me.nmcHomeAcc.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmcHomeVelHigh
        '
        resources.ApplyResources(Me.nmcHomeVelHigh, "nmcHomeVelHigh")
        Me.nmcHomeVelHigh.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcHomeVelHigh.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcHomeVelHigh.Name = "nmcHomeVelHigh"
        Me.ToolTip1.SetToolTip(Me.nmcHomeVelHigh, resources.GetString("nmcHomeVelHigh.ToolTip"))
        Me.nmcHomeVelHigh.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmcHomeVelLow
        '
        resources.ApplyResources(Me.nmcHomeVelLow, "nmcHomeVelLow")
        Me.nmcHomeVelLow.DecimalPlaces = 1
        Me.nmcHomeVelLow.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcHomeVelLow.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmcHomeVelLow.Name = "nmcHomeVelLow"
        Me.ToolTip1.SetToolTip(Me.nmcHomeVelLow, resources.GetString("nmcHomeVelLow.ToolTip"))
        Me.nmcHomeVelLow.Value = New Decimal(New Integer() {1, 0, 0, 65536})
        '
        'lblHomeDec
        '
        resources.ApplyResources(Me.lblHomeDec, "lblHomeDec")
        Me.lblHomeDec.Name = "lblHomeDec"
        Me.ToolTip1.SetToolTip(Me.lblHomeDec, resources.GetString("lblHomeDec.ToolTip"))
        '
        'lblHomeAcc
        '
        resources.ApplyResources(Me.lblHomeAcc, "lblHomeAcc")
        Me.lblHomeAcc.Name = "lblHomeAcc"
        Me.ToolTip1.SetToolTip(Me.lblHomeAcc, resources.GetString("lblHomeAcc.ToolTip"))
        '
        'lblHomeVelHigh
        '
        resources.ApplyResources(Me.lblHomeVelHigh, "lblHomeVelHigh")
        Me.lblHomeVelHigh.Name = "lblHomeVelHigh"
        Me.ToolTip1.SetToolTip(Me.lblHomeVelHigh, resources.GetString("lblHomeVelHigh.ToolTip"))
        '
        'lblHomeVelLow
        '
        resources.ApplyResources(Me.lblHomeVelLow, "lblHomeVelLow")
        Me.lblHomeVelLow.Name = "lblHomeVelLow"
        Me.ToolTip1.SetToolTip(Me.lblHomeVelLow, resources.GetString("lblHomeVelLow.ToolTip"))
        '
        'lblHomeExSwitchMode
        '
        resources.ApplyResources(Me.lblHomeExSwitchMode, "lblHomeExSwitchMode")
        Me.lblHomeExSwitchMode.Name = "lblHomeExSwitchMode"
        Me.ToolTip1.SetToolTip(Me.lblHomeExSwitchMode, resources.GetString("lblHomeExSwitchMode.ToolTip"))
        '
        'nmcHomeExSwitchMode
        '
        resources.ApplyResources(Me.nmcHomeExSwitchMode, "nmcHomeExSwitchMode")
        Me.nmcHomeExSwitchMode.Maximum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.nmcHomeExSwitchMode.Name = "nmcHomeExSwitchMode"
        Me.ToolTip1.SetToolTip(Me.nmcHomeExSwitchMode, resources.GetString("nmcHomeExSwitchMode.ToolTip"))
        Me.nmcHomeExSwitchMode.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'nmcHomeMode
        '
        resources.ApplyResources(Me.nmcHomeMode, "nmcHomeMode")
        Me.nmcHomeMode.Maximum = New Decimal(New Integer() {16, 0, 0, 0})
        Me.nmcHomeMode.Name = "nmcHomeMode"
        Me.ToolTip1.SetToolTip(Me.nmcHomeMode, resources.GetString("nmcHomeMode.ToolTip"))
        Me.nmcHomeMode.Value = New Decimal(New Integer() {9, 0, 0, 0})
        '
        'nmcHomeCrossDistance
        '
        resources.ApplyResources(Me.nmcHomeCrossDistance, "nmcHomeCrossDistance")
        Me.nmcHomeCrossDistance.DecimalPlaces = 1
        Me.nmcHomeCrossDistance.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmcHomeCrossDistance.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmcHomeCrossDistance.Name = "nmcHomeCrossDistance"
        Me.ToolTip1.SetToolTip(Me.nmcHomeCrossDistance, resources.GetString("nmcHomeCrossDistance.ToolTip"))
        Me.nmcHomeCrossDistance.Value = New Decimal(New Integer() {1, 0, 0, 65536})
        '
        'lblHomeCrossDistance
        '
        resources.ApplyResources(Me.lblHomeCrossDistance, "lblHomeCrossDistance")
        Me.lblHomeCrossDistance.Name = "lblHomeCrossDistance"
        Me.ToolTip1.SetToolTip(Me.lblHomeCrossDistance, resources.GetString("lblHomeCrossDistance.ToolTip"))
        '
        'lblHomeMode
        '
        resources.ApplyResources(Me.lblHomeMode, "lblHomeMode")
        Me.lblHomeMode.Name = "lblHomeMode"
        Me.ToolTip1.SetToolTip(Me.lblHomeMode, resources.GetString("lblHomeMode.ToolTip"))
        '
        'TabPage3
        '
        resources.ApplyResources(Me.TabPage3, "TabPage3")
        Me.TabPage3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TabPage3.Controls.Add(Me.lblMaxVelUnit)
        Me.TabPage3.Controls.Add(Me.Label13)
        Me.TabPage3.Controls.Add(Me.lblMaxDecUnit)
        Me.TabPage3.Controls.Add(Me.Label11)
        Me.TabPage3.Controls.Add(Me.lblMacAccUnit)
        Me.TabPage3.Controls.Add(Me.cmbHLMTLogic)
        Me.TabPage3.Controls.Add(Me.lblHLMTLogic)
        Me.TabPage3.Controls.Add(Me.cmbHLMTEnable)
        Me.TabPage3.Controls.Add(Me.lblHLMTEnable)
        Me.TabPage3.Controls.Add(Me.nmcMaxVel)
        Me.TabPage3.Controls.Add(Me.nmcMaxDec)
        Me.TabPage3.Controls.Add(Me.nmcMaxAcc)
        Me.TabPage3.Controls.Add(Me.lblMaxVel)
        Me.TabPage3.Controls.Add(Me.lblMacDec)
        Me.TabPage3.Controls.Add(Me.lblMaxAcc)
        Me.TabPage3.Controls.Add(Me.nmcSPEL)
        Me.TabPage3.Controls.Add(Me.nmcSNEL)
        Me.TabPage3.Controls.Add(Me.cmbHLMTStopMode)
        Me.TabPage3.Controls.Add(Me.lblSNELUnit)
        Me.TabPage3.Controls.Add(Me.lblSPELUnit)
        Me.TabPage3.Controls.Add(Me.lblSNEL)
        Me.TabPage3.Controls.Add(Me.lblHLMTStopMode)
        Me.TabPage3.Controls.Add(Me.lblSPEL)
        Me.TabPage3.Name = "TabPage3"
        Me.ToolTip1.SetToolTip(Me.TabPage3, resources.GetString("TabPage3.ToolTip"))
        '
        'lblMaxVelUnit
        '
        resources.ApplyResources(Me.lblMaxVelUnit, "lblMaxVelUnit")
        Me.lblMaxVelUnit.Name = "lblMaxVelUnit"
        Me.ToolTip1.SetToolTip(Me.lblMaxVelUnit, resources.GetString("lblMaxVelUnit.ToolTip"))
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        Me.ToolTip1.SetToolTip(Me.Label13, resources.GetString("Label13.ToolTip"))
        '
        'lblMaxDecUnit
        '
        resources.ApplyResources(Me.lblMaxDecUnit, "lblMaxDecUnit")
        Me.lblMaxDecUnit.Name = "lblMaxDecUnit"
        Me.ToolTip1.SetToolTip(Me.lblMaxDecUnit, resources.GetString("lblMaxDecUnit.ToolTip"))
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        Me.ToolTip1.SetToolTip(Me.Label11, resources.GetString("Label11.ToolTip"))
        '
        'lblMacAccUnit
        '
        resources.ApplyResources(Me.lblMacAccUnit, "lblMacAccUnit")
        Me.lblMacAccUnit.Name = "lblMacAccUnit"
        Me.ToolTip1.SetToolTip(Me.lblMacAccUnit, resources.GetString("lblMacAccUnit.ToolTip"))
        '
        'cmbHLMTLogic
        '
        resources.ApplyResources(Me.cmbHLMTLogic, "cmbHLMTLogic")
        Me.cmbHLMTLogic.BackColor = System.Drawing.Color.White
        Me.cmbHLMTLogic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHLMTLogic.FormattingEnabled = True
        Me.cmbHLMTLogic.Items.AddRange(New Object() {resources.GetString("cmbHLMTLogic.Items"), resources.GetString("cmbHLMTLogic.Items1")})
        Me.cmbHLMTLogic.Name = "cmbHLMTLogic"
        Me.ToolTip1.SetToolTip(Me.cmbHLMTLogic, resources.GetString("cmbHLMTLogic.ToolTip"))
        '
        'lblHLMTLogic
        '
        resources.ApplyResources(Me.lblHLMTLogic, "lblHLMTLogic")
        Me.lblHLMTLogic.Name = "lblHLMTLogic"
        Me.ToolTip1.SetToolTip(Me.lblHLMTLogic, resources.GetString("lblHLMTLogic.ToolTip"))
        '
        'cmbHLMTEnable
        '
        resources.ApplyResources(Me.cmbHLMTEnable, "cmbHLMTEnable")
        Me.cmbHLMTEnable.BackColor = System.Drawing.Color.White
        Me.cmbHLMTEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHLMTEnable.FormattingEnabled = True
        Me.cmbHLMTEnable.Items.AddRange(New Object() {resources.GetString("cmbHLMTEnable.Items"), resources.GetString("cmbHLMTEnable.Items1")})
        Me.cmbHLMTEnable.Name = "cmbHLMTEnable"
        Me.ToolTip1.SetToolTip(Me.cmbHLMTEnable, resources.GetString("cmbHLMTEnable.ToolTip"))
        '
        'lblHLMTEnable
        '
        resources.ApplyResources(Me.lblHLMTEnable, "lblHLMTEnable")
        Me.lblHLMTEnable.Name = "lblHLMTEnable"
        Me.ToolTip1.SetToolTip(Me.lblHLMTEnable, resources.GetString("lblHLMTEnable.ToolTip"))
        '
        'nmcMaxVel
        '
        resources.ApplyResources(Me.nmcMaxVel, "nmcMaxVel")
        Me.nmcMaxVel.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcMaxVel.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcMaxVel.Name = "nmcMaxVel"
        Me.ToolTip1.SetToolTip(Me.nmcMaxVel, resources.GetString("nmcMaxVel.ToolTip"))
        Me.nmcMaxVel.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmcMaxDec
        '
        resources.ApplyResources(Me.nmcMaxDec, "nmcMaxDec")
        Me.nmcMaxDec.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.nmcMaxDec.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcMaxDec.Name = "nmcMaxDec"
        Me.ToolTip1.SetToolTip(Me.nmcMaxDec, resources.GetString("nmcMaxDec.ToolTip"))
        Me.nmcMaxDec.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmcMaxAcc
        '
        resources.ApplyResources(Me.nmcMaxAcc, "nmcMaxAcc")
        Me.nmcMaxAcc.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.nmcMaxAcc.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcMaxAcc.Name = "nmcMaxAcc"
        Me.ToolTip1.SetToolTip(Me.nmcMaxAcc, resources.GetString("nmcMaxAcc.ToolTip"))
        Me.nmcMaxAcc.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblMaxVel
        '
        resources.ApplyResources(Me.lblMaxVel, "lblMaxVel")
        Me.lblMaxVel.Name = "lblMaxVel"
        Me.ToolTip1.SetToolTip(Me.lblMaxVel, resources.GetString("lblMaxVel.ToolTip"))
        '
        'lblMacDec
        '
        resources.ApplyResources(Me.lblMacDec, "lblMacDec")
        Me.lblMacDec.Name = "lblMacDec"
        Me.ToolTip1.SetToolTip(Me.lblMacDec, resources.GetString("lblMacDec.ToolTip"))
        '
        'lblMaxAcc
        '
        resources.ApplyResources(Me.lblMaxAcc, "lblMaxAcc")
        Me.lblMaxAcc.Name = "lblMaxAcc"
        Me.ToolTip1.SetToolTip(Me.lblMaxAcc, resources.GetString("lblMaxAcc.ToolTip"))
        '
        'nmcSPEL
        '
        resources.ApplyResources(Me.nmcSPEL, "nmcSPEL")
        Me.nmcSPEL.DecimalPlaces = 1
        Me.nmcSPEL.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcSPEL.Minimum = New Decimal(New Integer() {999, 0, 0, -2147483648})
        Me.nmcSPEL.Name = "nmcSPEL"
        Me.ToolTip1.SetToolTip(Me.nmcSPEL, resources.GetString("nmcSPEL.ToolTip"))
        '
        'nmcSNEL
        '
        resources.ApplyResources(Me.nmcSNEL, "nmcSNEL")
        Me.nmcSNEL.DecimalPlaces = 1
        Me.nmcSNEL.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcSNEL.Minimum = New Decimal(New Integer() {999, 0, 0, -2147483648})
        Me.nmcSNEL.Name = "nmcSNEL"
        Me.ToolTip1.SetToolTip(Me.nmcSNEL, resources.GetString("nmcSNEL.ToolTip"))
        '
        'cmbHLMTStopMode
        '
        resources.ApplyResources(Me.cmbHLMTStopMode, "cmbHLMTStopMode")
        Me.cmbHLMTStopMode.BackColor = System.Drawing.Color.White
        Me.cmbHLMTStopMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHLMTStopMode.FormattingEnabled = True
        Me.cmbHLMTStopMode.Items.AddRange(New Object() {resources.GetString("cmbHLMTStopMode.Items"), resources.GetString("cmbHLMTStopMode.Items1")})
        Me.cmbHLMTStopMode.Name = "cmbHLMTStopMode"
        Me.ToolTip1.SetToolTip(Me.cmbHLMTStopMode, resources.GetString("cmbHLMTStopMode.ToolTip"))
        '
        'lblSNELUnit
        '
        resources.ApplyResources(Me.lblSNELUnit, "lblSNELUnit")
        Me.lblSNELUnit.Name = "lblSNELUnit"
        Me.ToolTip1.SetToolTip(Me.lblSNELUnit, resources.GetString("lblSNELUnit.ToolTip"))
        '
        'lblSPELUnit
        '
        resources.ApplyResources(Me.lblSPELUnit, "lblSPELUnit")
        Me.lblSPELUnit.Name = "lblSPELUnit"
        Me.ToolTip1.SetToolTip(Me.lblSPELUnit, resources.GetString("lblSPELUnit.ToolTip"))
        '
        'lblSNEL
        '
        resources.ApplyResources(Me.lblSNEL, "lblSNEL")
        Me.lblSNEL.Name = "lblSNEL"
        Me.ToolTip1.SetToolTip(Me.lblSNEL, resources.GetString("lblSNEL.ToolTip"))
        '
        'lblHLMTStopMode
        '
        resources.ApplyResources(Me.lblHLMTStopMode, "lblHLMTStopMode")
        Me.lblHLMTStopMode.Name = "lblHLMTStopMode"
        Me.ToolTip1.SetToolTip(Me.lblHLMTStopMode, resources.GetString("lblHLMTStopMode.ToolTip"))
        '
        'lblSPEL
        '
        resources.ApplyResources(Me.lblSPEL, "lblSPEL")
        Me.lblSPEL.Name = "lblSPEL"
        Me.ToolTip1.SetToolTip(Me.lblSPEL, resources.GetString("lblSPEL.ToolTip"))
        '
        'TabPage4
        '
        resources.ApplyResources(Me.TabPage4, "TabPage4")
        Me.TabPage4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TabPage4.Controls.Add(Me.cmbEZLogic)
        Me.TabPage4.Controls.Add(Me.lblEZLogic)
        Me.TabPage4.Controls.Add(Me.cmbORGLogic)
        Me.TabPage4.Controls.Add(Me.lblORGLogic)
        Me.TabPage4.Controls.Add(Me.cmbERCLogic)
        Me.TabPage4.Controls.Add(Me.cmbLatchLogic)
        Me.TabPage4.Controls.Add(Me.cmbINPLogic)
        Me.TabPage4.Controls.Add(Me.cmbALMLogic)
        Me.TabPage4.Controls.Add(Me.lblERCLogic)
        Me.TabPage4.Controls.Add(Me.lblLatchLogic)
        Me.TabPage4.Controls.Add(Me.lblINPLogic)
        Me.TabPage4.Controls.Add(Me.lblALMLogic)
        Me.TabPage4.Controls.Add(Me.cmbBacklashEnable)
        Me.TabPage4.Controls.Add(Me.lblBacklashEnable)
        Me.TabPage4.Controls.Add(Me.cmbERCEnable)
        Me.TabPage4.Controls.Add(Me.lblERCEnable)
        Me.TabPage4.Controls.Add(Me.cmbLatchEnable)
        Me.TabPage4.Controls.Add(Me.lblLatchEnable)
        Me.TabPage4.Controls.Add(Me.cmbINPEnable)
        Me.TabPage4.Controls.Add(Me.lblINPEnable)
        Me.TabPage4.Controls.Add(Me.cmbALMEnable)
        Me.TabPage4.Controls.Add(Me.lblALMEnable)
        Me.TabPage4.Controls.Add(Me.cmbALMStopMode)
        Me.TabPage4.Controls.Add(Me.lblALMStopMode)
        Me.TabPage4.Name = "TabPage4"
        Me.ToolTip1.SetToolTip(Me.TabPage4, resources.GetString("TabPage4.ToolTip"))
        '
        'cmbEZLogic
        '
        resources.ApplyResources(Me.cmbEZLogic, "cmbEZLogic")
        Me.cmbEZLogic.BackColor = System.Drawing.Color.White
        Me.cmbEZLogic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEZLogic.FormattingEnabled = True
        Me.cmbEZLogic.Items.AddRange(New Object() {resources.GetString("cmbEZLogic.Items"), resources.GetString("cmbEZLogic.Items1")})
        Me.cmbEZLogic.Name = "cmbEZLogic"
        Me.ToolTip1.SetToolTip(Me.cmbEZLogic, resources.GetString("cmbEZLogic.ToolTip"))
        '
        'lblEZLogic
        '
        resources.ApplyResources(Me.lblEZLogic, "lblEZLogic")
        Me.lblEZLogic.Name = "lblEZLogic"
        Me.ToolTip1.SetToolTip(Me.lblEZLogic, resources.GetString("lblEZLogic.ToolTip"))
        '
        'cmbORGLogic
        '
        resources.ApplyResources(Me.cmbORGLogic, "cmbORGLogic")
        Me.cmbORGLogic.BackColor = System.Drawing.Color.White
        Me.cmbORGLogic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbORGLogic.FormattingEnabled = True
        Me.cmbORGLogic.Items.AddRange(New Object() {resources.GetString("cmbORGLogic.Items"), resources.GetString("cmbORGLogic.Items1")})
        Me.cmbORGLogic.Name = "cmbORGLogic"
        Me.ToolTip1.SetToolTip(Me.cmbORGLogic, resources.GetString("cmbORGLogic.ToolTip"))
        '
        'lblORGLogic
        '
        resources.ApplyResources(Me.lblORGLogic, "lblORGLogic")
        Me.lblORGLogic.Name = "lblORGLogic"
        Me.ToolTip1.SetToolTip(Me.lblORGLogic, resources.GetString("lblORGLogic.ToolTip"))
        '
        'cmbERCLogic
        '
        resources.ApplyResources(Me.cmbERCLogic, "cmbERCLogic")
        Me.cmbERCLogic.BackColor = System.Drawing.Color.White
        Me.cmbERCLogic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbERCLogic.FormattingEnabled = True
        Me.cmbERCLogic.Items.AddRange(New Object() {resources.GetString("cmbERCLogic.Items"), resources.GetString("cmbERCLogic.Items1")})
        Me.cmbERCLogic.Name = "cmbERCLogic"
        Me.ToolTip1.SetToolTip(Me.cmbERCLogic, resources.GetString("cmbERCLogic.ToolTip"))
        '
        'cmbLatchLogic
        '
        resources.ApplyResources(Me.cmbLatchLogic, "cmbLatchLogic")
        Me.cmbLatchLogic.BackColor = System.Drawing.Color.White
        Me.cmbLatchLogic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLatchLogic.FormattingEnabled = True
        Me.cmbLatchLogic.Items.AddRange(New Object() {resources.GetString("cmbLatchLogic.Items"), resources.GetString("cmbLatchLogic.Items1")})
        Me.cmbLatchLogic.Name = "cmbLatchLogic"
        Me.ToolTip1.SetToolTip(Me.cmbLatchLogic, resources.GetString("cmbLatchLogic.ToolTip"))
        '
        'cmbINPLogic
        '
        resources.ApplyResources(Me.cmbINPLogic, "cmbINPLogic")
        Me.cmbINPLogic.BackColor = System.Drawing.Color.White
        Me.cmbINPLogic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbINPLogic.FormattingEnabled = True
        Me.cmbINPLogic.Items.AddRange(New Object() {resources.GetString("cmbINPLogic.Items"), resources.GetString("cmbINPLogic.Items1")})
        Me.cmbINPLogic.Name = "cmbINPLogic"
        Me.ToolTip1.SetToolTip(Me.cmbINPLogic, resources.GetString("cmbINPLogic.ToolTip"))
        '
        'cmbALMLogic
        '
        resources.ApplyResources(Me.cmbALMLogic, "cmbALMLogic")
        Me.cmbALMLogic.BackColor = System.Drawing.Color.White
        Me.cmbALMLogic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbALMLogic.FormattingEnabled = True
        Me.cmbALMLogic.Items.AddRange(New Object() {resources.GetString("cmbALMLogic.Items"), resources.GetString("cmbALMLogic.Items1")})
        Me.cmbALMLogic.Name = "cmbALMLogic"
        Me.ToolTip1.SetToolTip(Me.cmbALMLogic, resources.GetString("cmbALMLogic.ToolTip"))
        '
        'lblERCLogic
        '
        resources.ApplyResources(Me.lblERCLogic, "lblERCLogic")
        Me.lblERCLogic.Name = "lblERCLogic"
        Me.ToolTip1.SetToolTip(Me.lblERCLogic, resources.GetString("lblERCLogic.ToolTip"))
        '
        'lblLatchLogic
        '
        resources.ApplyResources(Me.lblLatchLogic, "lblLatchLogic")
        Me.lblLatchLogic.Name = "lblLatchLogic"
        Me.ToolTip1.SetToolTip(Me.lblLatchLogic, resources.GetString("lblLatchLogic.ToolTip"))
        '
        'lblINPLogic
        '
        resources.ApplyResources(Me.lblINPLogic, "lblINPLogic")
        Me.lblINPLogic.Name = "lblINPLogic"
        Me.ToolTip1.SetToolTip(Me.lblINPLogic, resources.GetString("lblINPLogic.ToolTip"))
        '
        'lblALMLogic
        '
        resources.ApplyResources(Me.lblALMLogic, "lblALMLogic")
        Me.lblALMLogic.Name = "lblALMLogic"
        Me.ToolTip1.SetToolTip(Me.lblALMLogic, resources.GetString("lblALMLogic.ToolTip"))
        '
        'cmbBacklashEnable
        '
        resources.ApplyResources(Me.cmbBacklashEnable, "cmbBacklashEnable")
        Me.cmbBacklashEnable.BackColor = System.Drawing.Color.White
        Me.cmbBacklashEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBacklashEnable.FormattingEnabled = True
        Me.cmbBacklashEnable.Items.AddRange(New Object() {resources.GetString("cmbBacklashEnable.Items"), resources.GetString("cmbBacklashEnable.Items1")})
        Me.cmbBacklashEnable.Name = "cmbBacklashEnable"
        Me.ToolTip1.SetToolTip(Me.cmbBacklashEnable, resources.GetString("cmbBacklashEnable.ToolTip"))
        '
        'lblBacklashEnable
        '
        resources.ApplyResources(Me.lblBacklashEnable, "lblBacklashEnable")
        Me.lblBacklashEnable.Name = "lblBacklashEnable"
        Me.ToolTip1.SetToolTip(Me.lblBacklashEnable, resources.GetString("lblBacklashEnable.ToolTip"))
        '
        'cmbERCEnable
        '
        resources.ApplyResources(Me.cmbERCEnable, "cmbERCEnable")
        Me.cmbERCEnable.BackColor = System.Drawing.Color.White
        Me.cmbERCEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbERCEnable.FormattingEnabled = True
        Me.cmbERCEnable.Items.AddRange(New Object() {resources.GetString("cmbERCEnable.Items"), resources.GetString("cmbERCEnable.Items1")})
        Me.cmbERCEnable.Name = "cmbERCEnable"
        Me.ToolTip1.SetToolTip(Me.cmbERCEnable, resources.GetString("cmbERCEnable.ToolTip"))
        '
        'lblERCEnable
        '
        resources.ApplyResources(Me.lblERCEnable, "lblERCEnable")
        Me.lblERCEnable.Name = "lblERCEnable"
        Me.ToolTip1.SetToolTip(Me.lblERCEnable, resources.GetString("lblERCEnable.ToolTip"))
        '
        'cmbLatchEnable
        '
        resources.ApplyResources(Me.cmbLatchEnable, "cmbLatchEnable")
        Me.cmbLatchEnable.BackColor = System.Drawing.Color.White
        Me.cmbLatchEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLatchEnable.FormattingEnabled = True
        Me.cmbLatchEnable.Items.AddRange(New Object() {resources.GetString("cmbLatchEnable.Items"), resources.GetString("cmbLatchEnable.Items1")})
        Me.cmbLatchEnable.Name = "cmbLatchEnable"
        Me.ToolTip1.SetToolTip(Me.cmbLatchEnable, resources.GetString("cmbLatchEnable.ToolTip"))
        '
        'lblLatchEnable
        '
        resources.ApplyResources(Me.lblLatchEnable, "lblLatchEnable")
        Me.lblLatchEnable.Name = "lblLatchEnable"
        Me.ToolTip1.SetToolTip(Me.lblLatchEnable, resources.GetString("lblLatchEnable.ToolTip"))
        '
        'cmbINPEnable
        '
        resources.ApplyResources(Me.cmbINPEnable, "cmbINPEnable")
        Me.cmbINPEnable.BackColor = System.Drawing.Color.White
        Me.cmbINPEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbINPEnable.FormattingEnabled = True
        Me.cmbINPEnable.Items.AddRange(New Object() {resources.GetString("cmbINPEnable.Items"), resources.GetString("cmbINPEnable.Items1")})
        Me.cmbINPEnable.Name = "cmbINPEnable"
        Me.ToolTip1.SetToolTip(Me.cmbINPEnable, resources.GetString("cmbINPEnable.ToolTip"))
        '
        'lblINPEnable
        '
        resources.ApplyResources(Me.lblINPEnable, "lblINPEnable")
        Me.lblINPEnable.Name = "lblINPEnable"
        Me.ToolTip1.SetToolTip(Me.lblINPEnable, resources.GetString("lblINPEnable.ToolTip"))
        '
        'cmbALMEnable
        '
        resources.ApplyResources(Me.cmbALMEnable, "cmbALMEnable")
        Me.cmbALMEnable.BackColor = System.Drawing.Color.White
        Me.cmbALMEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbALMEnable.FormattingEnabled = True
        Me.cmbALMEnable.Items.AddRange(New Object() {resources.GetString("cmbALMEnable.Items"), resources.GetString("cmbALMEnable.Items1")})
        Me.cmbALMEnable.Name = "cmbALMEnable"
        Me.ToolTip1.SetToolTip(Me.cmbALMEnable, resources.GetString("cmbALMEnable.ToolTip"))
        '
        'lblALMEnable
        '
        resources.ApplyResources(Me.lblALMEnable, "lblALMEnable")
        Me.lblALMEnable.Name = "lblALMEnable"
        Me.ToolTip1.SetToolTip(Me.lblALMEnable, resources.GetString("lblALMEnable.ToolTip"))
        '
        'cmbALMStopMode
        '
        resources.ApplyResources(Me.cmbALMStopMode, "cmbALMStopMode")
        Me.cmbALMStopMode.BackColor = System.Drawing.Color.White
        Me.cmbALMStopMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbALMStopMode.FormattingEnabled = True
        Me.cmbALMStopMode.Items.AddRange(New Object() {resources.GetString("cmbALMStopMode.Items"), resources.GetString("cmbALMStopMode.Items1")})
        Me.cmbALMStopMode.Name = "cmbALMStopMode"
        Me.ToolTip1.SetToolTip(Me.cmbALMStopMode, resources.GetString("cmbALMStopMode.ToolTip"))
        '
        'lblALMStopMode
        '
        resources.ApplyResources(Me.lblALMStopMode, "lblALMStopMode")
        Me.lblALMStopMode.Name = "lblALMStopMode"
        Me.ToolTip1.SetToolTip(Me.lblALMStopMode, resources.GetString("lblALMStopMode.ToolTip"))
        '
        'TabPage5
        '
        resources.ApplyResources(Me.TabPage5, "TabPage5")
        Me.TabPage5.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TabPage5.Controls.Add(Me.nmcPPU)
        Me.TabPage5.Controls.Add(Me.lblPPUUnit)
        Me.TabPage5.Controls.Add(Me.lblINPStable)
        Me.TabPage5.Controls.Add(Me.lblPPU)
        Me.TabPage5.Controls.Add(Me.Label70)
        Me.TabPage5.Controls.Add(Me.Label68)
        Me.TabPage5.Controls.Add(Me.lblDecelerationUnit)
        Me.TabPage5.Controls.Add(Me.lblAccelerationUnit)
        Me.TabPage5.Controls.Add(Me.lblINPStableUnit)
        Me.TabPage5.Controls.Add(Me.lblVelocityHighUnit)
        Me.TabPage5.Controls.Add(Me.lblVelocityLowUnit)
        Me.TabPage5.Controls.Add(Me.nmcDecRatio)
        Me.TabPage5.Controls.Add(Me.nmcAccRatio)
        Me.TabPage5.Controls.Add(Me.nmcINPStable)
        Me.TabPage5.Controls.Add(Me.nmcDec)
        Me.TabPage5.Controls.Add(Me.nmcAcc)
        Me.TabPage5.Controls.Add(Me.nmcVelHigh)
        Me.TabPage5.Controls.Add(Me.lblDecRatio)
        Me.TabPage5.Controls.Add(Me.lblDec)
        Me.TabPage5.Controls.Add(Me.lblAccRatio)
        Me.TabPage5.Controls.Add(Me.lblAcc)
        Me.TabPage5.Controls.Add(Me.lblVelHigh)
        Me.TabPage5.Controls.Add(Me.nmcVelLow)
        Me.TabPage5.Controls.Add(Me.lblVelLow)
        Me.TabPage5.Controls.Add(Me.cmbExternalDriveAxis)
        Me.TabPage5.Controls.Add(Me.cmbExternalDriveEnable)
        Me.TabPage5.Controls.Add(Me.lblExternalDriveEnable)
        Me.TabPage5.Controls.Add(Me.cmbExternalPulseInMode)
        Me.TabPage5.Controls.Add(Me.lblExternalDriveAxis)
        Me.TabPage5.Controls.Add(Me.lblExternalPulseInMode)
        Me.TabPage5.Name = "TabPage5"
        Me.ToolTip1.SetToolTip(Me.TabPage5, resources.GetString("TabPage5.ToolTip"))
        '
        'nmcPPU
        '
        resources.ApplyResources(Me.nmcPPU, "nmcPPU")
        Me.nmcPPU.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcPPU.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcPPU.Name = "nmcPPU"
        Me.ToolTip1.SetToolTip(Me.nmcPPU, resources.GetString("nmcPPU.ToolTip"))
        Me.nmcPPU.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblPPUUnit
        '
        resources.ApplyResources(Me.lblPPUUnit, "lblPPUUnit")
        Me.lblPPUUnit.Name = "lblPPUUnit"
        Me.ToolTip1.SetToolTip(Me.lblPPUUnit, resources.GetString("lblPPUUnit.ToolTip"))
        '
        'lblINPStable
        '
        resources.ApplyResources(Me.lblINPStable, "lblINPStable")
        Me.lblINPStable.Name = "lblINPStable"
        Me.ToolTip1.SetToolTip(Me.lblINPStable, resources.GetString("lblINPStable.ToolTip"))
        '
        'lblPPU
        '
        resources.ApplyResources(Me.lblPPU, "lblPPU")
        Me.lblPPU.Name = "lblPPU"
        Me.ToolTip1.SetToolTip(Me.lblPPU, resources.GetString("lblPPU.ToolTip"))
        '
        'Label70
        '
        resources.ApplyResources(Me.Label70, "Label70")
        Me.Label70.Name = "Label70"
        Me.ToolTip1.SetToolTip(Me.Label70, resources.GetString("Label70.ToolTip"))
        '
        'Label68
        '
        resources.ApplyResources(Me.Label68, "Label68")
        Me.Label68.Name = "Label68"
        Me.ToolTip1.SetToolTip(Me.Label68, resources.GetString("Label68.ToolTip"))
        '
        'lblDecelerationUnit
        '
        resources.ApplyResources(Me.lblDecelerationUnit, "lblDecelerationUnit")
        Me.lblDecelerationUnit.Name = "lblDecelerationUnit"
        Me.ToolTip1.SetToolTip(Me.lblDecelerationUnit, resources.GetString("lblDecelerationUnit.ToolTip"))
        '
        'lblAccelerationUnit
        '
        resources.ApplyResources(Me.lblAccelerationUnit, "lblAccelerationUnit")
        Me.lblAccelerationUnit.Name = "lblAccelerationUnit"
        Me.ToolTip1.SetToolTip(Me.lblAccelerationUnit, resources.GetString("lblAccelerationUnit.ToolTip"))
        '
        'lblINPStableUnit
        '
        resources.ApplyResources(Me.lblINPStableUnit, "lblINPStableUnit")
        Me.lblINPStableUnit.Name = "lblINPStableUnit"
        Me.ToolTip1.SetToolTip(Me.lblINPStableUnit, resources.GetString("lblINPStableUnit.ToolTip"))
        '
        'lblVelocityHighUnit
        '
        resources.ApplyResources(Me.lblVelocityHighUnit, "lblVelocityHighUnit")
        Me.lblVelocityHighUnit.Name = "lblVelocityHighUnit"
        Me.ToolTip1.SetToolTip(Me.lblVelocityHighUnit, resources.GetString("lblVelocityHighUnit.ToolTip"))
        '
        'lblVelocityLowUnit
        '
        resources.ApplyResources(Me.lblVelocityLowUnit, "lblVelocityLowUnit")
        Me.lblVelocityLowUnit.Name = "lblVelocityLowUnit"
        Me.ToolTip1.SetToolTip(Me.lblVelocityLowUnit, resources.GetString("lblVelocityLowUnit.ToolTip"))
        '
        'nmcDecRatio
        '
        resources.ApplyResources(Me.nmcDecRatio, "nmcDecRatio")
        Me.nmcDecRatio.DecimalPlaces = 2
        Me.nmcDecRatio.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcDecRatio.Name = "nmcDecRatio"
        Me.ToolTip1.SetToolTip(Me.nmcDecRatio, resources.GetString("nmcDecRatio.ToolTip"))
        Me.nmcDecRatio.Value = New Decimal(New Integer() {5, 0, 0, 65536})
        '
        'nmcAccRatio
        '
        resources.ApplyResources(Me.nmcAccRatio, "nmcAccRatio")
        Me.nmcAccRatio.DecimalPlaces = 2
        Me.nmcAccRatio.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcAccRatio.Name = "nmcAccRatio"
        Me.ToolTip1.SetToolTip(Me.nmcAccRatio, resources.GetString("nmcAccRatio.ToolTip"))
        Me.nmcAccRatio.Value = New Decimal(New Integer() {5, 0, 0, 65536})
        '
        'nmcINPStable
        '
        resources.ApplyResources(Me.nmcINPStable, "nmcINPStable")
        Me.nmcINPStable.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmcINPStable.Name = "nmcINPStable"
        Me.ToolTip1.SetToolTip(Me.nmcINPStable, resources.GetString("nmcINPStable.ToolTip"))
        '
        'nmcDec
        '
        resources.ApplyResources(Me.nmcDec, "nmcDec")
        Me.nmcDec.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcDec.Name = "nmcDec"
        Me.ToolTip1.SetToolTip(Me.nmcDec, resources.GetString("nmcDec.ToolTip"))
        '
        'nmcAcc
        '
        resources.ApplyResources(Me.nmcAcc, "nmcAcc")
        Me.nmcAcc.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcAcc.Name = "nmcAcc"
        Me.ToolTip1.SetToolTip(Me.nmcAcc, resources.GetString("nmcAcc.ToolTip"))
        '
        'nmcVelHigh
        '
        resources.ApplyResources(Me.nmcVelHigh, "nmcVelHigh")
        Me.nmcVelHigh.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcVelHigh.Name = "nmcVelHigh"
        Me.ToolTip1.SetToolTip(Me.nmcVelHigh, resources.GetString("nmcVelHigh.ToolTip"))
        '
        'lblDecRatio
        '
        resources.ApplyResources(Me.lblDecRatio, "lblDecRatio")
        Me.lblDecRatio.Name = "lblDecRatio"
        Me.ToolTip1.SetToolTip(Me.lblDecRatio, resources.GetString("lblDecRatio.ToolTip"))
        '
        'lblDec
        '
        resources.ApplyResources(Me.lblDec, "lblDec")
        Me.lblDec.Name = "lblDec"
        Me.ToolTip1.SetToolTip(Me.lblDec, resources.GetString("lblDec.ToolTip"))
        '
        'lblAccRatio
        '
        resources.ApplyResources(Me.lblAccRatio, "lblAccRatio")
        Me.lblAccRatio.Name = "lblAccRatio"
        Me.ToolTip1.SetToolTip(Me.lblAccRatio, resources.GetString("lblAccRatio.ToolTip"))
        '
        'lblAcc
        '
        resources.ApplyResources(Me.lblAcc, "lblAcc")
        Me.lblAcc.Name = "lblAcc"
        Me.ToolTip1.SetToolTip(Me.lblAcc, resources.GetString("lblAcc.ToolTip"))
        '
        'lblVelHigh
        '
        resources.ApplyResources(Me.lblVelHigh, "lblVelHigh")
        Me.lblVelHigh.Name = "lblVelHigh"
        Me.ToolTip1.SetToolTip(Me.lblVelHigh, resources.GetString("lblVelHigh.ToolTip"))
        '
        'nmcVelLow
        '
        resources.ApplyResources(Me.nmcVelLow, "nmcVelLow")
        Me.nmcVelLow.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcVelLow.Name = "nmcVelLow"
        Me.ToolTip1.SetToolTip(Me.nmcVelLow, resources.GetString("nmcVelLow.ToolTip"))
        '
        'lblVelLow
        '
        resources.ApplyResources(Me.lblVelLow, "lblVelLow")
        Me.lblVelLow.Name = "lblVelLow"
        Me.ToolTip1.SetToolTip(Me.lblVelLow, resources.GetString("lblVelLow.ToolTip"))
        '
        'cmbExternalDriveAxis
        '
        resources.ApplyResources(Me.cmbExternalDriveAxis, "cmbExternalDriveAxis")
        Me.cmbExternalDriveAxis.BackColor = System.Drawing.Color.White
        Me.cmbExternalDriveAxis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExternalDriveAxis.FormattingEnabled = True
        Me.cmbExternalDriveAxis.Items.AddRange(New Object() {resources.GetString("cmbExternalDriveAxis.Items"), resources.GetString("cmbExternalDriveAxis.Items1"), resources.GetString("cmbExternalDriveAxis.Items2"), resources.GetString("cmbExternalDriveAxis.Items3")})
        Me.cmbExternalDriveAxis.Name = "cmbExternalDriveAxis"
        Me.ToolTip1.SetToolTip(Me.cmbExternalDriveAxis, resources.GetString("cmbExternalDriveAxis.ToolTip"))
        '
        'cmbExternalDriveEnable
        '
        resources.ApplyResources(Me.cmbExternalDriveEnable, "cmbExternalDriveEnable")
        Me.cmbExternalDriveEnable.BackColor = System.Drawing.Color.White
        Me.cmbExternalDriveEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExternalDriveEnable.FormattingEnabled = True
        Me.cmbExternalDriveEnable.Items.AddRange(New Object() {resources.GetString("cmbExternalDriveEnable.Items"), resources.GetString("cmbExternalDriveEnable.Items1")})
        Me.cmbExternalDriveEnable.Name = "cmbExternalDriveEnable"
        Me.ToolTip1.SetToolTip(Me.cmbExternalDriveEnable, resources.GetString("cmbExternalDriveEnable.ToolTip"))
        '
        'lblExternalDriveEnable
        '
        resources.ApplyResources(Me.lblExternalDriveEnable, "lblExternalDriveEnable")
        Me.lblExternalDriveEnable.Name = "lblExternalDriveEnable"
        Me.ToolTip1.SetToolTip(Me.lblExternalDriveEnable, resources.GetString("lblExternalDriveEnable.ToolTip"))
        '
        'cmbExternalPulseInMode
        '
        resources.ApplyResources(Me.cmbExternalPulseInMode, "cmbExternalPulseInMode")
        Me.cmbExternalPulseInMode.BackColor = System.Drawing.Color.White
        Me.cmbExternalPulseInMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExternalPulseInMode.FormattingEnabled = True
        Me.cmbExternalPulseInMode.Items.AddRange(New Object() {resources.GetString("cmbExternalPulseInMode.Items"), resources.GetString("cmbExternalPulseInMode.Items1"), resources.GetString("cmbExternalPulseInMode.Items2"), resources.GetString("cmbExternalPulseInMode.Items3")})
        Me.cmbExternalPulseInMode.Name = "cmbExternalPulseInMode"
        Me.ToolTip1.SetToolTip(Me.cmbExternalPulseInMode, resources.GetString("cmbExternalPulseInMode.ToolTip"))
        '
        'lblExternalDriveAxis
        '
        resources.ApplyResources(Me.lblExternalDriveAxis, "lblExternalDriveAxis")
        Me.lblExternalDriveAxis.Name = "lblExternalDriveAxis"
        Me.ToolTip1.SetToolTip(Me.lblExternalDriveAxis, resources.GetString("lblExternalDriveAxis.ToolTip"))
        '
        'lblExternalPulseInMode
        '
        resources.ApplyResources(Me.lblExternalPulseInMode, "lblExternalPulseInMode")
        Me.lblExternalPulseInMode.Name = "lblExternalPulseInMode"
        Me.ToolTip1.SetToolTip(Me.lblExternalPulseInMode, resources.GetString("lblExternalPulseInMode.ToolTip"))
        '
        'TabPage6
        '
        resources.ApplyResources(Me.TabPage6, "TabPage6")
        Me.TabPage6.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TabPage6.Controls.Add(Me.cmbIN5StopMode)
        Me.TabPage6.Controls.Add(Me.cmbIN4StopMode)
        Me.TabPage6.Controls.Add(Me.cmbIN2StopMode)
        Me.TabPage6.Controls.Add(Me.cmbIN1StopMode)
        Me.TabPage6.Controls.Add(Me.cmbIN5StopLogic)
        Me.TabPage6.Controls.Add(Me.cmbIN4StopLogic)
        Me.TabPage6.Controls.Add(Me.cmbIN2StopLogic)
        Me.TabPage6.Controls.Add(Me.cmbIN1StopLogic)
        Me.TabPage6.Controls.Add(Me.cmbIN5StopEnable)
        Me.TabPage6.Controls.Add(Me.cmbIN4StopEnable)
        Me.TabPage6.Controls.Add(Me.lblIN5StopMode)
        Me.TabPage6.Controls.Add(Me.cmbIN2StopEnable)
        Me.TabPage6.Controls.Add(Me.lblIN4StopMode)
        Me.TabPage6.Controls.Add(Me.cmbIN1StopEnable)
        Me.TabPage6.Controls.Add(Me.lblIN5StopLogic)
        Me.TabPage6.Controls.Add(Me.lblIN2StopMode)
        Me.TabPage6.Controls.Add(Me.lblIN4StopLogic)
        Me.TabPage6.Controls.Add(Me.lblIN5StopEnable)
        Me.TabPage6.Controls.Add(Me.lblIN2StopLogic)
        Me.TabPage6.Controls.Add(Me.lblIN4StopEnable)
        Me.TabPage6.Controls.Add(Me.lblIN1StopMode)
        Me.TabPage6.Controls.Add(Me.lblIN2StopEnable)
        Me.TabPage6.Controls.Add(Me.lblIN1StopLogic)
        Me.TabPage6.Controls.Add(Me.lblIN1StopEnable)
        Me.TabPage6.Name = "TabPage6"
        Me.ToolTip1.SetToolTip(Me.TabPage6, resources.GetString("TabPage6.ToolTip"))
        '
        'cmbIN5StopMode
        '
        resources.ApplyResources(Me.cmbIN5StopMode, "cmbIN5StopMode")
        Me.cmbIN5StopMode.BackColor = System.Drawing.Color.White
        Me.cmbIN5StopMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIN5StopMode.FormattingEnabled = True
        Me.cmbIN5StopMode.Items.AddRange(New Object() {resources.GetString("cmbIN5StopMode.Items"), resources.GetString("cmbIN5StopMode.Items1")})
        Me.cmbIN5StopMode.Name = "cmbIN5StopMode"
        Me.ToolTip1.SetToolTip(Me.cmbIN5StopMode, resources.GetString("cmbIN5StopMode.ToolTip"))
        '
        'cmbIN4StopMode
        '
        resources.ApplyResources(Me.cmbIN4StopMode, "cmbIN4StopMode")
        Me.cmbIN4StopMode.BackColor = System.Drawing.Color.White
        Me.cmbIN4StopMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIN4StopMode.FormattingEnabled = True
        Me.cmbIN4StopMode.Items.AddRange(New Object() {resources.GetString("cmbIN4StopMode.Items"), resources.GetString("cmbIN4StopMode.Items1")})
        Me.cmbIN4StopMode.Name = "cmbIN4StopMode"
        Me.ToolTip1.SetToolTip(Me.cmbIN4StopMode, resources.GetString("cmbIN4StopMode.ToolTip"))
        '
        'cmbIN2StopMode
        '
        resources.ApplyResources(Me.cmbIN2StopMode, "cmbIN2StopMode")
        Me.cmbIN2StopMode.BackColor = System.Drawing.Color.White
        Me.cmbIN2StopMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIN2StopMode.FormattingEnabled = True
        Me.cmbIN2StopMode.Items.AddRange(New Object() {resources.GetString("cmbIN2StopMode.Items"), resources.GetString("cmbIN2StopMode.Items1")})
        Me.cmbIN2StopMode.Name = "cmbIN2StopMode"
        Me.ToolTip1.SetToolTip(Me.cmbIN2StopMode, resources.GetString("cmbIN2StopMode.ToolTip"))
        '
        'cmbIN1StopMode
        '
        resources.ApplyResources(Me.cmbIN1StopMode, "cmbIN1StopMode")
        Me.cmbIN1StopMode.BackColor = System.Drawing.Color.White
        Me.cmbIN1StopMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIN1StopMode.FormattingEnabled = True
        Me.cmbIN1StopMode.Items.AddRange(New Object() {resources.GetString("cmbIN1StopMode.Items"), resources.GetString("cmbIN1StopMode.Items1")})
        Me.cmbIN1StopMode.Name = "cmbIN1StopMode"
        Me.ToolTip1.SetToolTip(Me.cmbIN1StopMode, resources.GetString("cmbIN1StopMode.ToolTip"))
        '
        'cmbIN5StopLogic
        '
        resources.ApplyResources(Me.cmbIN5StopLogic, "cmbIN5StopLogic")
        Me.cmbIN5StopLogic.BackColor = System.Drawing.Color.White
        Me.cmbIN5StopLogic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIN5StopLogic.FormattingEnabled = True
        Me.cmbIN5StopLogic.Items.AddRange(New Object() {resources.GetString("cmbIN5StopLogic.Items"), resources.GetString("cmbIN5StopLogic.Items1")})
        Me.cmbIN5StopLogic.Name = "cmbIN5StopLogic"
        Me.ToolTip1.SetToolTip(Me.cmbIN5StopLogic, resources.GetString("cmbIN5StopLogic.ToolTip"))
        '
        'cmbIN4StopLogic
        '
        resources.ApplyResources(Me.cmbIN4StopLogic, "cmbIN4StopLogic")
        Me.cmbIN4StopLogic.BackColor = System.Drawing.Color.White
        Me.cmbIN4StopLogic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIN4StopLogic.FormattingEnabled = True
        Me.cmbIN4StopLogic.Items.AddRange(New Object() {resources.GetString("cmbIN4StopLogic.Items"), resources.GetString("cmbIN4StopLogic.Items1")})
        Me.cmbIN4StopLogic.Name = "cmbIN4StopLogic"
        Me.ToolTip1.SetToolTip(Me.cmbIN4StopLogic, resources.GetString("cmbIN4StopLogic.ToolTip"))
        '
        'cmbIN2StopLogic
        '
        resources.ApplyResources(Me.cmbIN2StopLogic, "cmbIN2StopLogic")
        Me.cmbIN2StopLogic.BackColor = System.Drawing.Color.White
        Me.cmbIN2StopLogic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIN2StopLogic.FormattingEnabled = True
        Me.cmbIN2StopLogic.Items.AddRange(New Object() {resources.GetString("cmbIN2StopLogic.Items"), resources.GetString("cmbIN2StopLogic.Items1")})
        Me.cmbIN2StopLogic.Name = "cmbIN2StopLogic"
        Me.ToolTip1.SetToolTip(Me.cmbIN2StopLogic, resources.GetString("cmbIN2StopLogic.ToolTip"))
        '
        'cmbIN1StopLogic
        '
        resources.ApplyResources(Me.cmbIN1StopLogic, "cmbIN1StopLogic")
        Me.cmbIN1StopLogic.BackColor = System.Drawing.Color.White
        Me.cmbIN1StopLogic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIN1StopLogic.FormattingEnabled = True
        Me.cmbIN1StopLogic.Items.AddRange(New Object() {resources.GetString("cmbIN1StopLogic.Items"), resources.GetString("cmbIN1StopLogic.Items1")})
        Me.cmbIN1StopLogic.Name = "cmbIN1StopLogic"
        Me.ToolTip1.SetToolTip(Me.cmbIN1StopLogic, resources.GetString("cmbIN1StopLogic.ToolTip"))
        '
        'cmbIN5StopEnable
        '
        resources.ApplyResources(Me.cmbIN5StopEnable, "cmbIN5StopEnable")
        Me.cmbIN5StopEnable.BackColor = System.Drawing.Color.White
        Me.cmbIN5StopEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIN5StopEnable.FormattingEnabled = True
        Me.cmbIN5StopEnable.Items.AddRange(New Object() {resources.GetString("cmbIN5StopEnable.Items"), resources.GetString("cmbIN5StopEnable.Items1")})
        Me.cmbIN5StopEnable.Name = "cmbIN5StopEnable"
        Me.ToolTip1.SetToolTip(Me.cmbIN5StopEnable, resources.GetString("cmbIN5StopEnable.ToolTip"))
        '
        'cmbIN4StopEnable
        '
        resources.ApplyResources(Me.cmbIN4StopEnable, "cmbIN4StopEnable")
        Me.cmbIN4StopEnable.BackColor = System.Drawing.Color.White
        Me.cmbIN4StopEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIN4StopEnable.FormattingEnabled = True
        Me.cmbIN4StopEnable.Items.AddRange(New Object() {resources.GetString("cmbIN4StopEnable.Items"), resources.GetString("cmbIN4StopEnable.Items1")})
        Me.cmbIN4StopEnable.Name = "cmbIN4StopEnable"
        Me.ToolTip1.SetToolTip(Me.cmbIN4StopEnable, resources.GetString("cmbIN4StopEnable.ToolTip"))
        '
        'lblIN5StopMode
        '
        resources.ApplyResources(Me.lblIN5StopMode, "lblIN5StopMode")
        Me.lblIN5StopMode.Name = "lblIN5StopMode"
        Me.ToolTip1.SetToolTip(Me.lblIN5StopMode, resources.GetString("lblIN5StopMode.ToolTip"))
        '
        'cmbIN2StopEnable
        '
        resources.ApplyResources(Me.cmbIN2StopEnable, "cmbIN2StopEnable")
        Me.cmbIN2StopEnable.BackColor = System.Drawing.Color.White
        Me.cmbIN2StopEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIN2StopEnable.FormattingEnabled = True
        Me.cmbIN2StopEnable.Items.AddRange(New Object() {resources.GetString("cmbIN2StopEnable.Items"), resources.GetString("cmbIN2StopEnable.Items1")})
        Me.cmbIN2StopEnable.Name = "cmbIN2StopEnable"
        Me.ToolTip1.SetToolTip(Me.cmbIN2StopEnable, resources.GetString("cmbIN2StopEnable.ToolTip"))
        '
        'lblIN4StopMode
        '
        resources.ApplyResources(Me.lblIN4StopMode, "lblIN4StopMode")
        Me.lblIN4StopMode.Name = "lblIN4StopMode"
        Me.ToolTip1.SetToolTip(Me.lblIN4StopMode, resources.GetString("lblIN4StopMode.ToolTip"))
        '
        'cmbIN1StopEnable
        '
        resources.ApplyResources(Me.cmbIN1StopEnable, "cmbIN1StopEnable")
        Me.cmbIN1StopEnable.BackColor = System.Drawing.Color.White
        Me.cmbIN1StopEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIN1StopEnable.FormattingEnabled = True
        Me.cmbIN1StopEnable.Items.AddRange(New Object() {resources.GetString("cmbIN1StopEnable.Items"), resources.GetString("cmbIN1StopEnable.Items1")})
        Me.cmbIN1StopEnable.Name = "cmbIN1StopEnable"
        Me.ToolTip1.SetToolTip(Me.cmbIN1StopEnable, resources.GetString("cmbIN1StopEnable.ToolTip"))
        '
        'lblIN5StopLogic
        '
        resources.ApplyResources(Me.lblIN5StopLogic, "lblIN5StopLogic")
        Me.lblIN5StopLogic.Name = "lblIN5StopLogic"
        Me.ToolTip1.SetToolTip(Me.lblIN5StopLogic, resources.GetString("lblIN5StopLogic.ToolTip"))
        '
        'lblIN2StopMode
        '
        resources.ApplyResources(Me.lblIN2StopMode, "lblIN2StopMode")
        Me.lblIN2StopMode.Name = "lblIN2StopMode"
        Me.ToolTip1.SetToolTip(Me.lblIN2StopMode, resources.GetString("lblIN2StopMode.ToolTip"))
        '
        'lblIN4StopLogic
        '
        resources.ApplyResources(Me.lblIN4StopLogic, "lblIN4StopLogic")
        Me.lblIN4StopLogic.Name = "lblIN4StopLogic"
        Me.ToolTip1.SetToolTip(Me.lblIN4StopLogic, resources.GetString("lblIN4StopLogic.ToolTip"))
        '
        'lblIN5StopEnable
        '
        resources.ApplyResources(Me.lblIN5StopEnable, "lblIN5StopEnable")
        Me.lblIN5StopEnable.Name = "lblIN5StopEnable"
        Me.ToolTip1.SetToolTip(Me.lblIN5StopEnable, resources.GetString("lblIN5StopEnable.ToolTip"))
        '
        'lblIN2StopLogic
        '
        resources.ApplyResources(Me.lblIN2StopLogic, "lblIN2StopLogic")
        Me.lblIN2StopLogic.Name = "lblIN2StopLogic"
        Me.ToolTip1.SetToolTip(Me.lblIN2StopLogic, resources.GetString("lblIN2StopLogic.ToolTip"))
        '
        'lblIN4StopEnable
        '
        resources.ApplyResources(Me.lblIN4StopEnable, "lblIN4StopEnable")
        Me.lblIN4StopEnable.Name = "lblIN4StopEnable"
        Me.ToolTip1.SetToolTip(Me.lblIN4StopEnable, resources.GetString("lblIN4StopEnable.ToolTip"))
        '
        'lblIN1StopMode
        '
        resources.ApplyResources(Me.lblIN1StopMode, "lblIN1StopMode")
        Me.lblIN1StopMode.Name = "lblIN1StopMode"
        Me.ToolTip1.SetToolTip(Me.lblIN1StopMode, resources.GetString("lblIN1StopMode.ToolTip"))
        '
        'lblIN2StopEnable
        '
        resources.ApplyResources(Me.lblIN2StopEnable, "lblIN2StopEnable")
        Me.lblIN2StopEnable.Name = "lblIN2StopEnable"
        Me.ToolTip1.SetToolTip(Me.lblIN2StopEnable, resources.GetString("lblIN2StopEnable.ToolTip"))
        '
        'lblIN1StopLogic
        '
        resources.ApplyResources(Me.lblIN1StopLogic, "lblIN1StopLogic")
        Me.lblIN1StopLogic.Name = "lblIN1StopLogic"
        Me.ToolTip1.SetToolTip(Me.lblIN1StopLogic, resources.GetString("lblIN1StopLogic.ToolTip"))
        '
        'lblIN1StopEnable
        '
        resources.ApplyResources(Me.lblIN1StopEnable, "lblIN1StopEnable")
        Me.lblIN1StopEnable.Name = "lblIN1StopEnable"
        Me.ToolTip1.SetToolTip(Me.lblIN1StopEnable, resources.GetString("lblIN1StopEnable.ToolTip"))
        '
        'tmrOption
        '
        '
        'lblDesc
        '
        resources.ApplyResources(Me.lblDesc, "lblDesc")
        Me.lblDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDesc.Name = "lblDesc"
        Me.ToolTip1.SetToolTip(Me.lblDesc, resources.GetString("lblDesc.ToolTip"))
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnCancel.Name = "btnCancel"
        Me.ToolTip1.SetToolTip(Me.btnCancel, resources.GetString("btnCancel.ToolTip"))
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnOK.Name = "btnOK"
        Me.ToolTip1.SetToolTip(Me.btnOK, resources.GetString("btnOK.ToolTip"))
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmMotionOption
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.lblDesc)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.grpAxisParameter)
        Me.Controls.Add(Me.grpIOStatus)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMotionOption"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpIOStatus.ResumeLayout(False)
        Me.grpAxisParameter.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.tabBasic.ResumeLayout(False)
        Me.tabBasic.PerformLayout()
        CType(Me.nmcCardNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcAxisNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcScale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.nmcHomeDec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcHomeAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcHomeVelHigh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcHomeVelLow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcHomeExSwitchMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcHomeMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcHomeCrossDistance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.nmcMaxVel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcMaxDec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcMaxAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcSPEL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcSNEL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        CType(Me.nmcPPU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcDecRatio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcAccRatio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcINPStable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcDec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcVelHigh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcVelLow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpIOStatus As System.Windows.Forms.GroupBox
    Friend WithEvents lblTRIG As System.Windows.Forms.Label
    Friend WithEvents lblNSEL As System.Windows.Forms.Label
    Friend WithEvents lblPSEL As System.Windows.Forms.Label
    Friend WithEvents lblALRM As System.Windows.Forms.Label
    Friend WithEvents lblSVON As System.Windows.Forms.Label
    Friend WithEvents lblINP As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblSD As System.Windows.Forms.Label
    Friend WithEvents lblLTC As System.Windows.Forms.Label
    Friend WithEvents lblCLR As System.Windows.Forms.Label
    Friend WithEvents lblEZ As System.Windows.Forms.Label
    Friend WithEvents lblERC As System.Windows.Forms.Label
    Friend WithEvents lblPCS As System.Windows.Forms.Label
    Friend WithEvents lblEMG As System.Windows.Forms.Label
    Friend WithEvents lblDIR As System.Windows.Forms.Label
    Friend WithEvents lblORG As System.Windows.Forms.Label
    Friend WithEvents lblNEL As System.Windows.Forms.Label
    Friend WithEvents lblPEL As System.Windows.Forms.Label
    Friend WithEvents lblALM As System.Windows.Forms.Label
    Friend WithEvents lblRDY As System.Windows.Forms.Label
    Friend WithEvents grpAxisParameter As System.Windows.Forms.GroupBox
    Friend WithEvents lblAxisNo As System.Windows.Forms.Label
    Friend WithEvents lblCardNo As System.Windows.Forms.Label
    Friend WithEvents tmrOption As System.Windows.Forms.Timer
    Friend WithEvents lblScale As System.Windows.Forms.Label
    Friend WithEvents lblSPEL As System.Windows.Forms.Label
    Friend WithEvents lblSNEL As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblScaleUnit As System.Windows.Forms.Label
    Friend WithEvents lblSPELUnit As System.Windows.Forms.Label
    Friend WithEvents lblSNELUnit As System.Windows.Forms.Label
    Friend WithEvents lblHomeMode As System.Windows.Forms.Label
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabBasic As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents nmcHomeMode As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcHomeCrossDistance As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblHomeCrossDistance As System.Windows.Forms.Label
    Friend WithEvents lblHomeExSwitchMode As System.Windows.Forms.Label
    Friend WithEvents nmcHomeExSwitchMode As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcSPEL As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcSNEL As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbHLMTStopMode As System.Windows.Forms.ComboBox
    Friend WithEvents lblHLMTStopMode As System.Windows.Forms.Label
    Friend WithEvents nmcHomeDec As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcHomeAcc As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcHomeVelHigh As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcHomeVelLow As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblHomeDec As System.Windows.Forms.Label
    Friend WithEvents lblHomeAcc As System.Windows.Forms.Label
    Friend WithEvents lblHomeVelHigh As System.Windows.Forms.Label
    Friend WithEvents lblHomeVelLow As System.Windows.Forms.Label
    Friend WithEvents nmcMaxVel As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcMaxDec As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcMaxAcc As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMaxVel As System.Windows.Forms.Label
    Friend WithEvents lblMacDec As System.Windows.Forms.Label
    Friend WithEvents lblMaxAcc As System.Windows.Forms.Label
    Friend WithEvents cmbPulseInMode As System.Windows.Forms.ComboBox
    Friend WithEvents lblPulseInMode As System.Windows.Forms.Label
    Friend WithEvents cmbPulseOutMode As System.Windows.Forms.ComboBox
    Friend WithEvents lblPulseOutMode As System.Windows.Forms.Label
    Friend WithEvents cmbPulseInMaxFreq As System.Windows.Forms.ComboBox
    Friend WithEvents lblPulseInMaxFreq As System.Windows.Forms.Label
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents cmbALMStopMode As System.Windows.Forms.ComboBox
    Friend WithEvents lblALMStopMode As System.Windows.Forms.Label
    Friend WithEvents cmbAxisType As System.Windows.Forms.ComboBox
    Friend WithEvents lblAxisType As System.Windows.Forms.Label
    Friend WithEvents nmcScale As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblIsEncoderExist As System.Windows.Forms.Label
    Friend WithEvents lblPulseOutReverse As System.Windows.Forms.Label
    Friend WithEvents lblPulseInReverse As System.Windows.Forms.Label
    Friend WithEvents cmbIsEncoderExist As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPulseOutReverse As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPulseInReverse As System.Windows.Forms.ComboBox
    Friend WithEvents cmbButtonDirection As System.Windows.Forms.ComboBox
    Friend WithEvents cmbMoveDirection As System.Windows.Forms.ComboBox
    Friend WithEvents lblButtonDirection As System.Windows.Forms.Label
    Friend WithEvents lblMoveDirection As System.Windows.Forms.Label
    Friend WithEvents cmbHomeReset As System.Windows.Forms.ComboBox
    Friend WithEvents lblHomeReset As System.Windows.Forms.Label
    Friend WithEvents cmbHomeDirection As System.Windows.Forms.ComboBox
    Friend WithEvents lblHomeDirection As System.Windows.Forms.Label
    Friend WithEvents nmcCardNo As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcAxisNo As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbHLMTLogic As System.Windows.Forms.ComboBox
    Friend WithEvents lblHLMTLogic As System.Windows.Forms.Label
    Friend WithEvents cmbHLMTEnable As System.Windows.Forms.ComboBox
    Friend WithEvents lblHLMTEnable As System.Windows.Forms.Label
    Friend WithEvents cmbEZLogic As System.Windows.Forms.ComboBox
    Friend WithEvents lblEZLogic As System.Windows.Forms.Label
    Friend WithEvents cmbORGLogic As System.Windows.Forms.ComboBox
    Friend WithEvents lblORGLogic As System.Windows.Forms.Label
    Friend WithEvents cmbERCLogic As System.Windows.Forms.ComboBox
    Friend WithEvents cmbLatchLogic As System.Windows.Forms.ComboBox
    Friend WithEvents cmbINPLogic As System.Windows.Forms.ComboBox
    Friend WithEvents cmbALMLogic As System.Windows.Forms.ComboBox
    Friend WithEvents lblERCLogic As System.Windows.Forms.Label
    Friend WithEvents lblLatchLogic As System.Windows.Forms.Label
    Friend WithEvents lblINPLogic As System.Windows.Forms.Label
    Friend WithEvents lblALMLogic As System.Windows.Forms.Label
    Friend WithEvents cmbBacklashEnable As System.Windows.Forms.ComboBox
    Friend WithEvents lblBacklashEnable As System.Windows.Forms.Label
    Friend WithEvents cmbERCEnable As System.Windows.Forms.ComboBox
    Friend WithEvents lblERCEnable As System.Windows.Forms.Label
    Friend WithEvents cmbLatchEnable As System.Windows.Forms.ComboBox
    Friend WithEvents lblLatchEnable As System.Windows.Forms.Label
    Friend WithEvents cmbINPEnable As System.Windows.Forms.ComboBox
    Friend WithEvents lblINPEnable As System.Windows.Forms.Label
    Friend WithEvents cmbALMEnable As System.Windows.Forms.ComboBox
    Friend WithEvents lblALMEnable As System.Windows.Forms.Label
    Friend WithEvents cmbMotorType As System.Windows.Forms.ComboBox
    Friend WithEvents lblMotorType As System.Windows.Forms.Label
    Friend WithEvents cmbCoordinate As System.Windows.Forms.ComboBox
    Friend WithEvents lblCoordinate As System.Windows.Forms.Label
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents cmbExternalDriveEnable As System.Windows.Forms.ComboBox
    Friend WithEvents lblExternalDriveEnable As System.Windows.Forms.Label
    Friend WithEvents cmbExternalPulseInMode As System.Windows.Forms.ComboBox
    Friend WithEvents lblExternalPulseInMode As System.Windows.Forms.Label
    Friend WithEvents cmbExternalDriveAxis As System.Windows.Forms.ComboBox
    Friend WithEvents lblExternalDriveAxis As System.Windows.Forms.Label
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents cmbIN5StopMode As System.Windows.Forms.ComboBox
    Friend WithEvents cmbIN4StopMode As System.Windows.Forms.ComboBox
    Friend WithEvents cmbIN2StopMode As System.Windows.Forms.ComboBox
    Friend WithEvents cmbIN1StopMode As System.Windows.Forms.ComboBox
    Friend WithEvents cmbIN5StopLogic As System.Windows.Forms.ComboBox
    Friend WithEvents cmbIN4StopLogic As System.Windows.Forms.ComboBox
    Friend WithEvents cmbIN2StopLogic As System.Windows.Forms.ComboBox
    Friend WithEvents cmbIN1StopLogic As System.Windows.Forms.ComboBox
    Friend WithEvents cmbIN5StopEnable As System.Windows.Forms.ComboBox
    Friend WithEvents cmbIN4StopEnable As System.Windows.Forms.ComboBox
    Friend WithEvents lblIN5StopMode As System.Windows.Forms.Label
    Friend WithEvents cmbIN2StopEnable As System.Windows.Forms.ComboBox
    Friend WithEvents lblIN4StopMode As System.Windows.Forms.Label
    Friend WithEvents cmbIN1StopEnable As System.Windows.Forms.ComboBox
    Friend WithEvents lblIN5StopLogic As System.Windows.Forms.Label
    Friend WithEvents lblIN2StopMode As System.Windows.Forms.Label
    Friend WithEvents lblIN4StopLogic As System.Windows.Forms.Label
    Friend WithEvents lblIN5StopEnable As System.Windows.Forms.Label
    Friend WithEvents lblIN2StopLogic As System.Windows.Forms.Label
    Friend WithEvents lblIN4StopEnable As System.Windows.Forms.Label
    Friend WithEvents lblIN1StopMode As System.Windows.Forms.Label
    Friend WithEvents lblIN2StopEnable As System.Windows.Forms.Label
    Friend WithEvents lblIN1StopLogic As System.Windows.Forms.Label
    Friend WithEvents lblIN1StopEnable As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents lblDecelerationUnit As System.Windows.Forms.Label
    Friend WithEvents lblAccelerationUnit As System.Windows.Forms.Label
    Friend WithEvents lblVelocityHighUnit As System.Windows.Forms.Label
    Friend WithEvents lblVelocityLowUnit As System.Windows.Forms.Label
    Friend WithEvents nmcDecRatio As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcAccRatio As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcDec As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcAcc As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcVelHigh As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblDecRatio As System.Windows.Forms.Label
    Friend WithEvents lblDec As System.Windows.Forms.Label
    Friend WithEvents lblAccRatio As System.Windows.Forms.Label
    Friend WithEvents lblAcc As System.Windows.Forms.Label
    Friend WithEvents lblVelHigh As System.Windows.Forms.Label
    Friend WithEvents nmcVelLow As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblVelLow As System.Windows.Forms.Label
    Friend WithEvents txtAxisName As System.Windows.Forms.TextBox
    Friend WithEvents lblAxisName As System.Windows.Forms.Label
    Friend WithEvents nmcPPU As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblPPUUnit As System.Windows.Forms.Label
    Friend WithEvents lblPPU As System.Windows.Forms.Label
    Friend WithEvents lblINPStable As System.Windows.Forms.Label
    Friend WithEvents lblINPStableUnit As System.Windows.Forms.Label
    Friend WithEvents nmcINPStable As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbCardType As System.Windows.Forms.ComboBox
    Friend WithEvents lblCardType As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblHomeDecUnit As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblHomeAccUnit As System.Windows.Forms.Label
    Friend WithEvents lblHomeVelHighUnit As System.Windows.Forms.Label
    Friend WithEvents lblHomeVelLowUnit As System.Windows.Forms.Label
    Friend WithEvents lblHomeCrossDistanceUnit As System.Windows.Forms.Label
    Friend WithEvents lblMaxVelUnit As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblMaxDecUnit As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblMacAccUnit As System.Windows.Forms.Label
    Friend WithEvents palSD As System.Windows.Forms.Panel
    Friend WithEvents palERC As System.Windows.Forms.Panel
    Friend WithEvents palDIR As System.Windows.Forms.Panel
    Friend WithEvents palEZ As System.Windows.Forms.Panel
    Friend WithEvents palTRIG As System.Windows.Forms.Panel
    Friend WithEvents palNSEL As System.Windows.Forms.Panel
    Friend WithEvents palLTC As System.Windows.Forms.Panel
    Friend WithEvents palALRM As System.Windows.Forms.Panel
    Friend WithEvents palPEL As System.Windows.Forms.Panel
    Friend WithEvents palPSEL As System.Windows.Forms.Panel
    Friend WithEvents palCLR As System.Windows.Forms.Panel
    Friend WithEvents palPCS As System.Windows.Forms.Panel
    Friend WithEvents palORG As System.Windows.Forms.Panel
    Friend WithEvents palSVON As System.Windows.Forms.Panel
    Friend WithEvents palALM As System.Windows.Forms.Panel
    Friend WithEvents palEMG As System.Windows.Forms.Panel
    Friend WithEvents palNEL As System.Windows.Forms.Panel
    Friend WithEvents palINP As System.Windows.Forms.Panel
    Friend WithEvents palRDY As System.Windows.Forms.Panel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

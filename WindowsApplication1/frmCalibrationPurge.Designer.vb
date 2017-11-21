<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalibrationPurge
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalibrationPurge))
        Me.grpHeightSensorPos = New System.Windows.Forms.GroupBox()
        Me.txtCalibrationPurgePosZ = New System.Windows.Forms.TextBox()
        Me.txtCalibrationPurgePosY = New System.Windows.Forms.TextBox()
        Me.txtCalibrationPurgePosX = New System.Windows.Forms.TextBox()
        Me.btnCalibrationPurgeGoPos = New System.Windows.Forms.Button()
        Me.btnCalibrationSetPurgePos = New System.Windows.Forms.Button()
        Me.lblLaserPosZ = New System.Windows.Forms.Label()
        Me.lblLaserPosY = New System.Windows.Forms.Label()
        Me.lblLaserPosX = New System.Windows.Forms.Label()
        Me.lblLaserPosZUnit = New System.Windows.Forms.Label()
        Me.lblLaserPosYUnit = New System.Windows.Forms.Label()
        Me.lblLaserPosXUnit = New System.Windows.Forms.Label()
        Me.grpCCDPos = New System.Windows.Forms.GroupBox()
        Me.txtCalibrationPurgeCCDPosZ = New System.Windows.Forms.TextBox()
        Me.txtCalibrationPurgeCCDPosY = New System.Windows.Forms.TextBox()
        Me.txtCalibrationPurgeCCDPosX = New System.Windows.Forms.TextBox()
        Me.btnCalibrationPurgeGoCCDPos = New System.Windows.Forms.Button()
        Me.btnCalibrationPurgeSetCcdPos = New System.Windows.Forms.Button()
        Me.lblCCDPosZUnit = New System.Windows.Forms.Label()
        Me.lblCCDPosYUnit = New System.Windows.Forms.Label()
        Me.lblCCDPosXUnit = New System.Windows.Forms.Label()
        Me.lblCCDPosZ = New System.Windows.Forms.Label()
        Me.lblCCDPosY = New System.Windows.Forms.Label()
        Me.lblCCDPosX = New System.Windows.Forms.Label()
        Me.gpbPurge = New System.Windows.Forms.GroupBox()
        Me.lblPurgeVacuum = New System.Windows.Forms.Label()
        Me.palVacuumReady = New System.Windows.Forms.Panel()
        Me.btnPurgeVacuumOnOff = New System.Windows.Forms.Button()
        Me.nmcJettingValveNo1CycleTime = New System.Windows.Forms.NumericUpDown()
        Me.nmcDispenserNo1PurgeTime = New System.Windows.Forms.NumericUpDown()
        Me.btnPurge = New System.Windows.Forms.Button()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.lblCycleTime = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblPurgeTime = New System.Windows.Forms.Label()
        Me.grpPurgeAlignPos = New System.Windows.Forms.GroupBox()
        Me.txtCalibrationPurgeAlign3PosZ = New System.Windows.Forms.TextBox()
        Me.txtCalibrationPurgeAlign3PosY = New System.Windows.Forms.TextBox()
        Me.txtCalibrationPurgeAlign3PosX = New System.Windows.Forms.TextBox()
        Me.btnCalibrationPurgeGoAlign3Pos = New System.Windows.Forms.Button()
        Me.btnCalibrationPurgeSetAlign3Pos = New System.Windows.Forms.Button()
        Me.lblPurge3PosZUnit = New System.Windows.Forms.Label()
        Me.lblPurge3PosYUnit = New System.Windows.Forms.Label()
        Me.lblPurge3PosXUnit = New System.Windows.Forms.Label()
        Me.lblPurgeAlign3PosZ = New System.Windows.Forms.Label()
        Me.lblPurgeAlign3PosY = New System.Windows.Forms.Label()
        Me.lblPurgeAlign3PosX = New System.Windows.Forms.Label()
        Me.btnAlign = New System.Windows.Forms.Button()
        Me.txtCalibrationPurgeAlign2PosZ = New System.Windows.Forms.TextBox()
        Me.txtCalibrationPurgeAlign2PosY = New System.Windows.Forms.TextBox()
        Me.txtCalibrationPurgeAlign2PosX = New System.Windows.Forms.TextBox()
        Me.btnCalibrationPurgeGoAlign2Pos = New System.Windows.Forms.Button()
        Me.btnCalibrationPurgeSetAlign2Pos = New System.Windows.Forms.Button()
        Me.lblPurge2PosZUnit = New System.Windows.Forms.Label()
        Me.lblPurge2PosYUnit = New System.Windows.Forms.Label()
        Me.lblPurge2PosXUnit = New System.Windows.Forms.Label()
        Me.lblPurgeAlign2PosZ = New System.Windows.Forms.Label()
        Me.lblPurgeAlign2PosY = New System.Windows.Forms.Label()
        Me.lblPurgeAlign2PosX = New System.Windows.Forms.Label()
        Me.txtCalibrationPurgeAlign1PosZ = New System.Windows.Forms.TextBox()
        Me.txtCalibrationPurgeAlign1PosY = New System.Windows.Forms.TextBox()
        Me.txtCalibrationPurgeAlign1PosX = New System.Windows.Forms.TextBox()
        Me.btnCalibrationPurgeGoAlign1Pos = New System.Windows.Forms.Button()
        Me.btnCalibrationPurgeSetAlign1Pos = New System.Windows.Forms.Button()
        Me.lblPurge1PosZUnit = New System.Windows.Forms.Label()
        Me.lblPurge1PosYUnit = New System.Windows.Forms.Label()
        Me.lblPurge1PosXUnit = New System.Windows.Forms.Label()
        Me.lblPurgeAlign1PosZ = New System.Windows.Forms.Label()
        Me.lblPurgeAlign1PosY = New System.Windows.Forms.Label()
        Me.lblPurgeAlign1PosX = New System.Windows.Forms.Label()
        Me.lblSceneSet = New System.Windows.Forms.Label()
        Me.lblScene = New System.Windows.Forms.Label()
        Me.btnTrainScene = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.nmcExposure = New System.Windows.Forms.NumericUpDown()
        Me.lblExposureTimeUnit = New System.Windows.Forms.Label()
        Me.gpTiltMove = New System.Windows.Forms.GroupBox()
        Me.txtTiltValvePosB = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnGoTilt = New System.Windows.Forms.Button()
        Me.btnCalibrationPurgeCancel = New System.Windows.Forms.Button()
        Me.btnCalibrationPurgeOK = New System.Windows.Forms.Button()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.btnPurgeNext = New System.Windows.Forms.Button()
        Me.btnPurgePrev = New System.Windows.Forms.Button()
        Me.UcLightControl1 = New WindowsApplication1.ucLightControl()
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpHeightSensorPos.SuspendLayout()
        Me.grpCCDPos.SuspendLayout()
        Me.gpbPurge.SuspendLayout()
        CType(Me.nmcJettingValveNo1CycleTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcDispenserNo1PurgeTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPurgeAlignPos.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nmcExposure, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpTiltMove.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpHeightSensorPos
        '
        resources.ApplyResources(Me.grpHeightSensorPos, "grpHeightSensorPos")
        Me.grpHeightSensorPos.Controls.Add(Me.txtCalibrationPurgePosZ)
        Me.grpHeightSensorPos.Controls.Add(Me.txtCalibrationPurgePosY)
        Me.grpHeightSensorPos.Controls.Add(Me.txtCalibrationPurgePosX)
        Me.grpHeightSensorPos.Controls.Add(Me.btnCalibrationPurgeGoPos)
        Me.grpHeightSensorPos.Controls.Add(Me.btnCalibrationSetPurgePos)
        Me.grpHeightSensorPos.Controls.Add(Me.lblLaserPosZ)
        Me.grpHeightSensorPos.Controls.Add(Me.lblLaserPosY)
        Me.grpHeightSensorPos.Controls.Add(Me.lblLaserPosX)
        Me.grpHeightSensorPos.Controls.Add(Me.lblLaserPosZUnit)
        Me.grpHeightSensorPos.Controls.Add(Me.lblLaserPosYUnit)
        Me.grpHeightSensorPos.Controls.Add(Me.lblLaserPosXUnit)
        Me.grpHeightSensorPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpHeightSensorPos.Name = "grpHeightSensorPos"
        Me.grpHeightSensorPos.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpHeightSensorPos, resources.GetString("grpHeightSensorPos.ToolTip"))
        '
        'txtCalibrationPurgePosZ
        '
        resources.ApplyResources(Me.txtCalibrationPurgePosZ, "txtCalibrationPurgePosZ")
        Me.txtCalibrationPurgePosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgePosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgePosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgePosZ.Name = "txtCalibrationPurgePosZ"
        Me.txtCalibrationPurgePosZ.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgePosZ, resources.GetString("txtCalibrationPurgePosZ.ToolTip"))
        '
        'txtCalibrationPurgePosY
        '
        resources.ApplyResources(Me.txtCalibrationPurgePosY, "txtCalibrationPurgePosY")
        Me.txtCalibrationPurgePosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgePosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgePosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgePosY.Name = "txtCalibrationPurgePosY"
        Me.txtCalibrationPurgePosY.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgePosY, resources.GetString("txtCalibrationPurgePosY.ToolTip"))
        '
        'txtCalibrationPurgePosX
        '
        resources.ApplyResources(Me.txtCalibrationPurgePosX, "txtCalibrationPurgePosX")
        Me.txtCalibrationPurgePosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgePosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgePosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgePosX.Name = "txtCalibrationPurgePosX"
        Me.txtCalibrationPurgePosX.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgePosX, resources.GetString("txtCalibrationPurgePosX.ToolTip"))
        '
        'btnCalibrationPurgeGoPos
        '
        resources.ApplyResources(Me.btnCalibrationPurgeGoPos, "btnCalibrationPurgeGoPos")
        Me.btnCalibrationPurgeGoPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnCalibrationPurgeGoPos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationPurgeGoPos.Name = "btnCalibrationPurgeGoPos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationPurgeGoPos, resources.GetString("btnCalibrationPurgeGoPos.ToolTip"))
        Me.btnCalibrationPurgeGoPos.UseVisualStyleBackColor = True
        '
        'btnCalibrationSetPurgePos
        '
        resources.ApplyResources(Me.btnCalibrationSetPurgePos, "btnCalibrationSetPurgePos")
        Me.btnCalibrationSetPurgePos.BackColor = System.Drawing.SystemColors.Control
        Me.btnCalibrationSetPurgePos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnCalibrationSetPurgePos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationSetPurgePos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCalibrationSetPurgePos.Name = "btnCalibrationSetPurgePos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationSetPurgePos, resources.GetString("btnCalibrationSetPurgePos.ToolTip"))
        Me.btnCalibrationSetPurgePos.UseVisualStyleBackColor = True
        '
        'lblLaserPosZ
        '
        resources.ApplyResources(Me.lblLaserPosZ, "lblLaserPosZ")
        Me.lblLaserPosZ.Name = "lblLaserPosZ"
        Me.ToolTip1.SetToolTip(Me.lblLaserPosZ, resources.GetString("lblLaserPosZ.ToolTip"))
        '
        'lblLaserPosY
        '
        resources.ApplyResources(Me.lblLaserPosY, "lblLaserPosY")
        Me.lblLaserPosY.Name = "lblLaserPosY"
        Me.ToolTip1.SetToolTip(Me.lblLaserPosY, resources.GetString("lblLaserPosY.ToolTip"))
        '
        'lblLaserPosX
        '
        resources.ApplyResources(Me.lblLaserPosX, "lblLaserPosX")
        Me.lblLaserPosX.Name = "lblLaserPosX"
        Me.ToolTip1.SetToolTip(Me.lblLaserPosX, resources.GetString("lblLaserPosX.ToolTip"))
        '
        'lblLaserPosZUnit
        '
        resources.ApplyResources(Me.lblLaserPosZUnit, "lblLaserPosZUnit")
        Me.lblLaserPosZUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLaserPosZUnit.Name = "lblLaserPosZUnit"
        Me.ToolTip1.SetToolTip(Me.lblLaserPosZUnit, resources.GetString("lblLaserPosZUnit.ToolTip"))
        '
        'lblLaserPosYUnit
        '
        resources.ApplyResources(Me.lblLaserPosYUnit, "lblLaserPosYUnit")
        Me.lblLaserPosYUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLaserPosYUnit.Name = "lblLaserPosYUnit"
        Me.ToolTip1.SetToolTip(Me.lblLaserPosYUnit, resources.GetString("lblLaserPosYUnit.ToolTip"))
        '
        'lblLaserPosXUnit
        '
        resources.ApplyResources(Me.lblLaserPosXUnit, "lblLaserPosXUnit")
        Me.lblLaserPosXUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLaserPosXUnit.Name = "lblLaserPosXUnit"
        Me.ToolTip1.SetToolTip(Me.lblLaserPosXUnit, resources.GetString("lblLaserPosXUnit.ToolTip"))
        '
        'grpCCDPos
        '
        resources.ApplyResources(Me.grpCCDPos, "grpCCDPos")
        Me.grpCCDPos.Controls.Add(Me.txtCalibrationPurgeCCDPosZ)
        Me.grpCCDPos.Controls.Add(Me.txtCalibrationPurgeCCDPosY)
        Me.grpCCDPos.Controls.Add(Me.txtCalibrationPurgeCCDPosX)
        Me.grpCCDPos.Controls.Add(Me.btnCalibrationPurgeGoCCDPos)
        Me.grpCCDPos.Controls.Add(Me.btnCalibrationPurgeSetCcdPos)
        Me.grpCCDPos.Controls.Add(Me.lblCCDPosZUnit)
        Me.grpCCDPos.Controls.Add(Me.lblCCDPosYUnit)
        Me.grpCCDPos.Controls.Add(Me.lblCCDPosXUnit)
        Me.grpCCDPos.Controls.Add(Me.lblCCDPosZ)
        Me.grpCCDPos.Controls.Add(Me.lblCCDPosY)
        Me.grpCCDPos.Controls.Add(Me.lblCCDPosX)
        Me.grpCCDPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpCCDPos.Name = "grpCCDPos"
        Me.grpCCDPos.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpCCDPos, resources.GetString("grpCCDPos.ToolTip"))
        '
        'txtCalibrationPurgeCCDPosZ
        '
        resources.ApplyResources(Me.txtCalibrationPurgeCCDPosZ, "txtCalibrationPurgeCCDPosZ")
        Me.txtCalibrationPurgeCCDPosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgeCCDPosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgeCCDPosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgeCCDPosZ.Name = "txtCalibrationPurgeCCDPosZ"
        Me.txtCalibrationPurgeCCDPosZ.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgeCCDPosZ, resources.GetString("txtCalibrationPurgeCCDPosZ.ToolTip"))
        '
        'txtCalibrationPurgeCCDPosY
        '
        resources.ApplyResources(Me.txtCalibrationPurgeCCDPosY, "txtCalibrationPurgeCCDPosY")
        Me.txtCalibrationPurgeCCDPosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgeCCDPosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgeCCDPosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgeCCDPosY.Name = "txtCalibrationPurgeCCDPosY"
        Me.txtCalibrationPurgeCCDPosY.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgeCCDPosY, resources.GetString("txtCalibrationPurgeCCDPosY.ToolTip"))
        '
        'txtCalibrationPurgeCCDPosX
        '
        resources.ApplyResources(Me.txtCalibrationPurgeCCDPosX, "txtCalibrationPurgeCCDPosX")
        Me.txtCalibrationPurgeCCDPosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgeCCDPosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgeCCDPosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgeCCDPosX.Name = "txtCalibrationPurgeCCDPosX"
        Me.txtCalibrationPurgeCCDPosX.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgeCCDPosX, resources.GetString("txtCalibrationPurgeCCDPosX.ToolTip"))
        '
        'btnCalibrationPurgeGoCCDPos
        '
        resources.ApplyResources(Me.btnCalibrationPurgeGoCCDPos, "btnCalibrationPurgeGoCCDPos")
        Me.btnCalibrationPurgeGoCCDPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnCalibrationPurgeGoCCDPos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationPurgeGoCCDPos.Name = "btnCalibrationPurgeGoCCDPos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationPurgeGoCCDPos, resources.GetString("btnCalibrationPurgeGoCCDPos.ToolTip"))
        Me.btnCalibrationPurgeGoCCDPos.UseVisualStyleBackColor = True
        '
        'btnCalibrationPurgeSetCcdPos
        '
        resources.ApplyResources(Me.btnCalibrationPurgeSetCcdPos, "btnCalibrationPurgeSetCcdPos")
        Me.btnCalibrationPurgeSetCcdPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnCalibrationPurgeSetCcdPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnCalibrationPurgeSetCcdPos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationPurgeSetCcdPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCalibrationPurgeSetCcdPos.Name = "btnCalibrationPurgeSetCcdPos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationPurgeSetCcdPos, resources.GetString("btnCalibrationPurgeSetCcdPos.ToolTip"))
        Me.btnCalibrationPurgeSetCcdPos.UseVisualStyleBackColor = True
        '
        'lblCCDPosZUnit
        '
        resources.ApplyResources(Me.lblCCDPosZUnit, "lblCCDPosZUnit")
        Me.lblCCDPosZUnit.Name = "lblCCDPosZUnit"
        Me.ToolTip1.SetToolTip(Me.lblCCDPosZUnit, resources.GetString("lblCCDPosZUnit.ToolTip"))
        '
        'lblCCDPosYUnit
        '
        resources.ApplyResources(Me.lblCCDPosYUnit, "lblCCDPosYUnit")
        Me.lblCCDPosYUnit.Name = "lblCCDPosYUnit"
        Me.ToolTip1.SetToolTip(Me.lblCCDPosYUnit, resources.GetString("lblCCDPosYUnit.ToolTip"))
        '
        'lblCCDPosXUnit
        '
        resources.ApplyResources(Me.lblCCDPosXUnit, "lblCCDPosXUnit")
        Me.lblCCDPosXUnit.Name = "lblCCDPosXUnit"
        Me.ToolTip1.SetToolTip(Me.lblCCDPosXUnit, resources.GetString("lblCCDPosXUnit.ToolTip"))
        '
        'lblCCDPosZ
        '
        resources.ApplyResources(Me.lblCCDPosZ, "lblCCDPosZ")
        Me.lblCCDPosZ.Name = "lblCCDPosZ"
        Me.ToolTip1.SetToolTip(Me.lblCCDPosZ, resources.GetString("lblCCDPosZ.ToolTip"))
        '
        'lblCCDPosY
        '
        resources.ApplyResources(Me.lblCCDPosY, "lblCCDPosY")
        Me.lblCCDPosY.Name = "lblCCDPosY"
        Me.ToolTip1.SetToolTip(Me.lblCCDPosY, resources.GetString("lblCCDPosY.ToolTip"))
        '
        'lblCCDPosX
        '
        resources.ApplyResources(Me.lblCCDPosX, "lblCCDPosX")
        Me.lblCCDPosX.Name = "lblCCDPosX"
        Me.ToolTip1.SetToolTip(Me.lblCCDPosX, resources.GetString("lblCCDPosX.ToolTip"))
        '
        'gpbPurge
        '
        resources.ApplyResources(Me.gpbPurge, "gpbPurge")
        Me.gpbPurge.Controls.Add(Me.lblPurgeVacuum)
        Me.gpbPurge.Controls.Add(Me.palVacuumReady)
        Me.gpbPurge.Controls.Add(Me.btnPurgeVacuumOnOff)
        Me.gpbPurge.Controls.Add(Me.nmcJettingValveNo1CycleTime)
        Me.gpbPurge.Controls.Add(Me.nmcDispenserNo1PurgeTime)
        Me.gpbPurge.Controls.Add(Me.btnPurge)
        Me.gpbPurge.Controls.Add(Me.Label92)
        Me.gpbPurge.Controls.Add(Me.lblCycleTime)
        Me.gpbPurge.Controls.Add(Me.Label19)
        Me.gpbPurge.Controls.Add(Me.lblPurgeTime)
        Me.gpbPurge.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gpbPurge.Name = "gpbPurge"
        Me.gpbPurge.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gpbPurge, resources.GetString("gpbPurge.ToolTip"))
        '
        'lblPurgeVacuum
        '
        resources.ApplyResources(Me.lblPurgeVacuum, "lblPurgeVacuum")
        Me.lblPurgeVacuum.BackColor = System.Drawing.Color.Transparent
        Me.lblPurgeVacuum.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblPurgeVacuum.Name = "lblPurgeVacuum"
        Me.ToolTip1.SetToolTip(Me.lblPurgeVacuum, resources.GetString("lblPurgeVacuum.ToolTip"))
        '
        'palVacuumReady
        '
        resources.ApplyResources(Me.palVacuumReady, "palVacuumReady")
        Me.palVacuumReady.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.li_23
        Me.palVacuumReady.Name = "palVacuumReady"
        Me.ToolTip1.SetToolTip(Me.palVacuumReady, resources.GetString("palVacuumReady.ToolTip"))
        '
        'btnPurgeVacuumOnOff
        '
        resources.ApplyResources(Me.btnPurgeVacuumOnOff, "btnPurgeVacuumOnOff")
        Me.btnPurgeVacuumOnOff.Name = "btnPurgeVacuumOnOff"
        Me.ToolTip1.SetToolTip(Me.btnPurgeVacuumOnOff, resources.GetString("btnPurgeVacuumOnOff.ToolTip"))
        Me.btnPurgeVacuumOnOff.UseVisualStyleBackColor = True
        '
        'nmcJettingValveNo1CycleTime
        '
        resources.ApplyResources(Me.nmcJettingValveNo1CycleTime, "nmcJettingValveNo1CycleTime")
        Me.nmcJettingValveNo1CycleTime.DecimalPlaces = 2
        Me.nmcJettingValveNo1CycleTime.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmcJettingValveNo1CycleTime.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcJettingValveNo1CycleTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcJettingValveNo1CycleTime.Name = "nmcJettingValveNo1CycleTime"
        Me.ToolTip1.SetToolTip(Me.nmcJettingValveNo1CycleTime, resources.GetString("nmcJettingValveNo1CycleTime.ToolTip"))
        Me.nmcJettingValveNo1CycleTime.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmcDispenserNo1PurgeTime
        '
        resources.ApplyResources(Me.nmcDispenserNo1PurgeTime, "nmcDispenserNo1PurgeTime")
        Me.nmcDispenserNo1PurgeTime.Maximum = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.nmcDispenserNo1PurgeTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcDispenserNo1PurgeTime.Name = "nmcDispenserNo1PurgeTime"
        Me.ToolTip1.SetToolTip(Me.nmcDispenserNo1PurgeTime, resources.GetString("nmcDispenserNo1PurgeTime.ToolTip"))
        Me.nmcDispenserNo1PurgeTime.Value = New Decimal(New Integer() {100000, 0, 0, 0})
        '
        'btnPurge
        '
        resources.ApplyResources(Me.btnPurge, "btnPurge")
        Me.btnPurge.BackColor = System.Drawing.SystemColors.Control
        Me.btnPurge.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Purge1
        Me.btnPurge.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnPurge.FlatAppearance.BorderSize = 0
        Me.btnPurge.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnPurge.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnPurge.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnPurge.Name = "btnPurge"
        Me.ToolTip1.SetToolTip(Me.btnPurge, resources.GetString("btnPurge.ToolTip"))
        Me.btnPurge.UseVisualStyleBackColor = True
        '
        'Label92
        '
        resources.ApplyResources(Me.Label92, "Label92")
        Me.Label92.BackColor = System.Drawing.Color.Transparent
        Me.Label92.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label92.Name = "Label92"
        Me.ToolTip1.SetToolTip(Me.Label92, resources.GetString("Label92.ToolTip"))
        '
        'lblCycleTime
        '
        resources.ApplyResources(Me.lblCycleTime, "lblCycleTime")
        Me.lblCycleTime.BackColor = System.Drawing.Color.Transparent
        Me.lblCycleTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCycleTime.Name = "lblCycleTime"
        Me.ToolTip1.SetToolTip(Me.lblCycleTime, resources.GetString("lblCycleTime.ToolTip"))
        '
        'Label19
        '
        resources.ApplyResources(Me.Label19, "Label19")
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label19.Name = "Label19"
        Me.ToolTip1.SetToolTip(Me.Label19, resources.GetString("Label19.ToolTip"))
        '
        'lblPurgeTime
        '
        resources.ApplyResources(Me.lblPurgeTime, "lblPurgeTime")
        Me.lblPurgeTime.BackColor = System.Drawing.Color.Transparent
        Me.lblPurgeTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblPurgeTime.Name = "lblPurgeTime"
        Me.ToolTip1.SetToolTip(Me.lblPurgeTime, resources.GetString("lblPurgeTime.ToolTip"))
        '
        'grpPurgeAlignPos
        '
        resources.ApplyResources(Me.grpPurgeAlignPos, "grpPurgeAlignPos")
        Me.grpPurgeAlignPos.Controls.Add(Me.txtCalibrationPurgeAlign3PosZ)
        Me.grpPurgeAlignPos.Controls.Add(Me.txtCalibrationPurgeAlign3PosY)
        Me.grpPurgeAlignPos.Controls.Add(Me.txtCalibrationPurgeAlign3PosX)
        Me.grpPurgeAlignPos.Controls.Add(Me.btnCalibrationPurgeGoAlign3Pos)
        Me.grpPurgeAlignPos.Controls.Add(Me.btnCalibrationPurgeSetAlign3Pos)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurge3PosZUnit)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurge3PosYUnit)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurge3PosXUnit)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurgeAlign3PosZ)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurgeAlign3PosY)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurgeAlign3PosX)
        Me.grpPurgeAlignPos.Controls.Add(Me.btnAlign)
        Me.grpPurgeAlignPos.Controls.Add(Me.txtCalibrationPurgeAlign2PosZ)
        Me.grpPurgeAlignPos.Controls.Add(Me.txtCalibrationPurgeAlign2PosY)
        Me.grpPurgeAlignPos.Controls.Add(Me.txtCalibrationPurgeAlign2PosX)
        Me.grpPurgeAlignPos.Controls.Add(Me.btnCalibrationPurgeGoAlign2Pos)
        Me.grpPurgeAlignPos.Controls.Add(Me.btnCalibrationPurgeSetAlign2Pos)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurge2PosZUnit)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurge2PosYUnit)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurge2PosXUnit)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurgeAlign2PosZ)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurgeAlign2PosY)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurgeAlign2PosX)
        Me.grpPurgeAlignPos.Controls.Add(Me.txtCalibrationPurgeAlign1PosZ)
        Me.grpPurgeAlignPos.Controls.Add(Me.txtCalibrationPurgeAlign1PosY)
        Me.grpPurgeAlignPos.Controls.Add(Me.txtCalibrationPurgeAlign1PosX)
        Me.grpPurgeAlignPos.Controls.Add(Me.btnCalibrationPurgeGoAlign1Pos)
        Me.grpPurgeAlignPos.Controls.Add(Me.btnCalibrationPurgeSetAlign1Pos)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurge1PosZUnit)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurge1PosYUnit)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurge1PosXUnit)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurgeAlign1PosZ)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurgeAlign1PosY)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblPurgeAlign1PosX)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblSceneSet)
        Me.grpPurgeAlignPos.Controls.Add(Me.lblScene)
        Me.grpPurgeAlignPos.Controls.Add(Me.btnTrainScene)
        Me.grpPurgeAlignPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpPurgeAlignPos.Name = "grpPurgeAlignPos"
        Me.grpPurgeAlignPos.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpPurgeAlignPos, resources.GetString("grpPurgeAlignPos.ToolTip"))
        '
        'txtCalibrationPurgeAlign3PosZ
        '
        resources.ApplyResources(Me.txtCalibrationPurgeAlign3PosZ, "txtCalibrationPurgeAlign3PosZ")
        Me.txtCalibrationPurgeAlign3PosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgeAlign3PosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgeAlign3PosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgeAlign3PosZ.Name = "txtCalibrationPurgeAlign3PosZ"
        Me.txtCalibrationPurgeAlign3PosZ.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgeAlign3PosZ, resources.GetString("txtCalibrationPurgeAlign3PosZ.ToolTip"))
        '
        'txtCalibrationPurgeAlign3PosY
        '
        resources.ApplyResources(Me.txtCalibrationPurgeAlign3PosY, "txtCalibrationPurgeAlign3PosY")
        Me.txtCalibrationPurgeAlign3PosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgeAlign3PosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgeAlign3PosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgeAlign3PosY.Name = "txtCalibrationPurgeAlign3PosY"
        Me.txtCalibrationPurgeAlign3PosY.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgeAlign3PosY, resources.GetString("txtCalibrationPurgeAlign3PosY.ToolTip"))
        '
        'txtCalibrationPurgeAlign3PosX
        '
        resources.ApplyResources(Me.txtCalibrationPurgeAlign3PosX, "txtCalibrationPurgeAlign3PosX")
        Me.txtCalibrationPurgeAlign3PosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgeAlign3PosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgeAlign3PosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgeAlign3PosX.Name = "txtCalibrationPurgeAlign3PosX"
        Me.txtCalibrationPurgeAlign3PosX.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgeAlign3PosX, resources.GetString("txtCalibrationPurgeAlign3PosX.ToolTip"))
        '
        'btnCalibrationPurgeGoAlign3Pos
        '
        resources.ApplyResources(Me.btnCalibrationPurgeGoAlign3Pos, "btnCalibrationPurgeGoAlign3Pos")
        Me.btnCalibrationPurgeGoAlign3Pos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnCalibrationPurgeGoAlign3Pos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationPurgeGoAlign3Pos.Name = "btnCalibrationPurgeGoAlign3Pos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationPurgeGoAlign3Pos, resources.GetString("btnCalibrationPurgeGoAlign3Pos.ToolTip"))
        Me.btnCalibrationPurgeGoAlign3Pos.UseVisualStyleBackColor = True
        '
        'btnCalibrationPurgeSetAlign3Pos
        '
        resources.ApplyResources(Me.btnCalibrationPurgeSetAlign3Pos, "btnCalibrationPurgeSetAlign3Pos")
        Me.btnCalibrationPurgeSetAlign3Pos.BackColor = System.Drawing.SystemColors.Control
        Me.btnCalibrationPurgeSetAlign3Pos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnCalibrationPurgeSetAlign3Pos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationPurgeSetAlign3Pos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCalibrationPurgeSetAlign3Pos.Name = "btnCalibrationPurgeSetAlign3Pos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationPurgeSetAlign3Pos, resources.GetString("btnCalibrationPurgeSetAlign3Pos.ToolTip"))
        Me.btnCalibrationPurgeSetAlign3Pos.UseVisualStyleBackColor = True
        '
        'lblPurge3PosZUnit
        '
        resources.ApplyResources(Me.lblPurge3PosZUnit, "lblPurge3PosZUnit")
        Me.lblPurge3PosZUnit.Name = "lblPurge3PosZUnit"
        Me.ToolTip1.SetToolTip(Me.lblPurge3PosZUnit, resources.GetString("lblPurge3PosZUnit.ToolTip"))
        '
        'lblPurge3PosYUnit
        '
        resources.ApplyResources(Me.lblPurge3PosYUnit, "lblPurge3PosYUnit")
        Me.lblPurge3PosYUnit.Name = "lblPurge3PosYUnit"
        Me.ToolTip1.SetToolTip(Me.lblPurge3PosYUnit, resources.GetString("lblPurge3PosYUnit.ToolTip"))
        '
        'lblPurge3PosXUnit
        '
        resources.ApplyResources(Me.lblPurge3PosXUnit, "lblPurge3PosXUnit")
        Me.lblPurge3PosXUnit.Name = "lblPurge3PosXUnit"
        Me.ToolTip1.SetToolTip(Me.lblPurge3PosXUnit, resources.GetString("lblPurge3PosXUnit.ToolTip"))
        '
        'lblPurgeAlign3PosZ
        '
        resources.ApplyResources(Me.lblPurgeAlign3PosZ, "lblPurgeAlign3PosZ")
        Me.lblPurgeAlign3PosZ.Name = "lblPurgeAlign3PosZ"
        Me.ToolTip1.SetToolTip(Me.lblPurgeAlign3PosZ, resources.GetString("lblPurgeAlign3PosZ.ToolTip"))
        '
        'lblPurgeAlign3PosY
        '
        resources.ApplyResources(Me.lblPurgeAlign3PosY, "lblPurgeAlign3PosY")
        Me.lblPurgeAlign3PosY.Name = "lblPurgeAlign3PosY"
        Me.ToolTip1.SetToolTip(Me.lblPurgeAlign3PosY, resources.GetString("lblPurgeAlign3PosY.ToolTip"))
        '
        'lblPurgeAlign3PosX
        '
        resources.ApplyResources(Me.lblPurgeAlign3PosX, "lblPurgeAlign3PosX")
        Me.lblPurgeAlign3PosX.Name = "lblPurgeAlign3PosX"
        Me.ToolTip1.SetToolTip(Me.lblPurgeAlign3PosX, resources.GetString("lblPurgeAlign3PosX.ToolTip"))
        '
        'btnAlign
        '
        resources.ApplyResources(Me.btnAlign, "btnAlign")
        Me.btnAlign.FlatAppearance.BorderSize = 0
        Me.btnAlign.Name = "btnAlign"
        Me.ToolTip1.SetToolTip(Me.btnAlign, resources.GetString("btnAlign.ToolTip"))
        Me.btnAlign.UseVisualStyleBackColor = True
        '
        'txtCalibrationPurgeAlign2PosZ
        '
        resources.ApplyResources(Me.txtCalibrationPurgeAlign2PosZ, "txtCalibrationPurgeAlign2PosZ")
        Me.txtCalibrationPurgeAlign2PosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgeAlign2PosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgeAlign2PosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgeAlign2PosZ.Name = "txtCalibrationPurgeAlign2PosZ"
        Me.txtCalibrationPurgeAlign2PosZ.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgeAlign2PosZ, resources.GetString("txtCalibrationPurgeAlign2PosZ.ToolTip"))
        '
        'txtCalibrationPurgeAlign2PosY
        '
        resources.ApplyResources(Me.txtCalibrationPurgeAlign2PosY, "txtCalibrationPurgeAlign2PosY")
        Me.txtCalibrationPurgeAlign2PosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgeAlign2PosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgeAlign2PosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgeAlign2PosY.Name = "txtCalibrationPurgeAlign2PosY"
        Me.txtCalibrationPurgeAlign2PosY.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgeAlign2PosY, resources.GetString("txtCalibrationPurgeAlign2PosY.ToolTip"))
        '
        'txtCalibrationPurgeAlign2PosX
        '
        resources.ApplyResources(Me.txtCalibrationPurgeAlign2PosX, "txtCalibrationPurgeAlign2PosX")
        Me.txtCalibrationPurgeAlign2PosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgeAlign2PosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgeAlign2PosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgeAlign2PosX.Name = "txtCalibrationPurgeAlign2PosX"
        Me.txtCalibrationPurgeAlign2PosX.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgeAlign2PosX, resources.GetString("txtCalibrationPurgeAlign2PosX.ToolTip"))
        '
        'btnCalibrationPurgeGoAlign2Pos
        '
        resources.ApplyResources(Me.btnCalibrationPurgeGoAlign2Pos, "btnCalibrationPurgeGoAlign2Pos")
        Me.btnCalibrationPurgeGoAlign2Pos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnCalibrationPurgeGoAlign2Pos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationPurgeGoAlign2Pos.Name = "btnCalibrationPurgeGoAlign2Pos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationPurgeGoAlign2Pos, resources.GetString("btnCalibrationPurgeGoAlign2Pos.ToolTip"))
        Me.btnCalibrationPurgeGoAlign2Pos.UseVisualStyleBackColor = True
        '
        'btnCalibrationPurgeSetAlign2Pos
        '
        resources.ApplyResources(Me.btnCalibrationPurgeSetAlign2Pos, "btnCalibrationPurgeSetAlign2Pos")
        Me.btnCalibrationPurgeSetAlign2Pos.BackColor = System.Drawing.SystemColors.Control
        Me.btnCalibrationPurgeSetAlign2Pos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnCalibrationPurgeSetAlign2Pos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationPurgeSetAlign2Pos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCalibrationPurgeSetAlign2Pos.Name = "btnCalibrationPurgeSetAlign2Pos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationPurgeSetAlign2Pos, resources.GetString("btnCalibrationPurgeSetAlign2Pos.ToolTip"))
        Me.btnCalibrationPurgeSetAlign2Pos.UseVisualStyleBackColor = True
        '
        'lblPurge2PosZUnit
        '
        resources.ApplyResources(Me.lblPurge2PosZUnit, "lblPurge2PosZUnit")
        Me.lblPurge2PosZUnit.Name = "lblPurge2PosZUnit"
        Me.ToolTip1.SetToolTip(Me.lblPurge2PosZUnit, resources.GetString("lblPurge2PosZUnit.ToolTip"))
        '
        'lblPurge2PosYUnit
        '
        resources.ApplyResources(Me.lblPurge2PosYUnit, "lblPurge2PosYUnit")
        Me.lblPurge2PosYUnit.Name = "lblPurge2PosYUnit"
        Me.ToolTip1.SetToolTip(Me.lblPurge2PosYUnit, resources.GetString("lblPurge2PosYUnit.ToolTip"))
        '
        'lblPurge2PosXUnit
        '
        resources.ApplyResources(Me.lblPurge2PosXUnit, "lblPurge2PosXUnit")
        Me.lblPurge2PosXUnit.Name = "lblPurge2PosXUnit"
        Me.ToolTip1.SetToolTip(Me.lblPurge2PosXUnit, resources.GetString("lblPurge2PosXUnit.ToolTip"))
        '
        'lblPurgeAlign2PosZ
        '
        resources.ApplyResources(Me.lblPurgeAlign2PosZ, "lblPurgeAlign2PosZ")
        Me.lblPurgeAlign2PosZ.Name = "lblPurgeAlign2PosZ"
        Me.ToolTip1.SetToolTip(Me.lblPurgeAlign2PosZ, resources.GetString("lblPurgeAlign2PosZ.ToolTip"))
        '
        'lblPurgeAlign2PosY
        '
        resources.ApplyResources(Me.lblPurgeAlign2PosY, "lblPurgeAlign2PosY")
        Me.lblPurgeAlign2PosY.Name = "lblPurgeAlign2PosY"
        Me.ToolTip1.SetToolTip(Me.lblPurgeAlign2PosY, resources.GetString("lblPurgeAlign2PosY.ToolTip"))
        '
        'lblPurgeAlign2PosX
        '
        resources.ApplyResources(Me.lblPurgeAlign2PosX, "lblPurgeAlign2PosX")
        Me.lblPurgeAlign2PosX.Name = "lblPurgeAlign2PosX"
        Me.ToolTip1.SetToolTip(Me.lblPurgeAlign2PosX, resources.GetString("lblPurgeAlign2PosX.ToolTip"))
        '
        'txtCalibrationPurgeAlign1PosZ
        '
        resources.ApplyResources(Me.txtCalibrationPurgeAlign1PosZ, "txtCalibrationPurgeAlign1PosZ")
        Me.txtCalibrationPurgeAlign1PosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgeAlign1PosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgeAlign1PosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgeAlign1PosZ.Name = "txtCalibrationPurgeAlign1PosZ"
        Me.txtCalibrationPurgeAlign1PosZ.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgeAlign1PosZ, resources.GetString("txtCalibrationPurgeAlign1PosZ.ToolTip"))
        '
        'txtCalibrationPurgeAlign1PosY
        '
        resources.ApplyResources(Me.txtCalibrationPurgeAlign1PosY, "txtCalibrationPurgeAlign1PosY")
        Me.txtCalibrationPurgeAlign1PosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgeAlign1PosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgeAlign1PosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgeAlign1PosY.Name = "txtCalibrationPurgeAlign1PosY"
        Me.txtCalibrationPurgeAlign1PosY.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgeAlign1PosY, resources.GetString("txtCalibrationPurgeAlign1PosY.ToolTip"))
        '
        'txtCalibrationPurgeAlign1PosX
        '
        resources.ApplyResources(Me.txtCalibrationPurgeAlign1PosX, "txtCalibrationPurgeAlign1PosX")
        Me.txtCalibrationPurgeAlign1PosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationPurgeAlign1PosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationPurgeAlign1PosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationPurgeAlign1PosX.Name = "txtCalibrationPurgeAlign1PosX"
        Me.txtCalibrationPurgeAlign1PosX.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationPurgeAlign1PosX, resources.GetString("txtCalibrationPurgeAlign1PosX.ToolTip"))
        '
        'btnCalibrationPurgeGoAlign1Pos
        '
        resources.ApplyResources(Me.btnCalibrationPurgeGoAlign1Pos, "btnCalibrationPurgeGoAlign1Pos")
        Me.btnCalibrationPurgeGoAlign1Pos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnCalibrationPurgeGoAlign1Pos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationPurgeGoAlign1Pos.Name = "btnCalibrationPurgeGoAlign1Pos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationPurgeGoAlign1Pos, resources.GetString("btnCalibrationPurgeGoAlign1Pos.ToolTip"))
        Me.btnCalibrationPurgeGoAlign1Pos.UseVisualStyleBackColor = True
        '
        'btnCalibrationPurgeSetAlign1Pos
        '
        resources.ApplyResources(Me.btnCalibrationPurgeSetAlign1Pos, "btnCalibrationPurgeSetAlign1Pos")
        Me.btnCalibrationPurgeSetAlign1Pos.BackColor = System.Drawing.SystemColors.Control
        Me.btnCalibrationPurgeSetAlign1Pos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnCalibrationPurgeSetAlign1Pos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationPurgeSetAlign1Pos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCalibrationPurgeSetAlign1Pos.Name = "btnCalibrationPurgeSetAlign1Pos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationPurgeSetAlign1Pos, resources.GetString("btnCalibrationPurgeSetAlign1Pos.ToolTip"))
        Me.btnCalibrationPurgeSetAlign1Pos.UseVisualStyleBackColor = True
        '
        'lblPurge1PosZUnit
        '
        resources.ApplyResources(Me.lblPurge1PosZUnit, "lblPurge1PosZUnit")
        Me.lblPurge1PosZUnit.Name = "lblPurge1PosZUnit"
        Me.ToolTip1.SetToolTip(Me.lblPurge1PosZUnit, resources.GetString("lblPurge1PosZUnit.ToolTip"))
        '
        'lblPurge1PosYUnit
        '
        resources.ApplyResources(Me.lblPurge1PosYUnit, "lblPurge1PosYUnit")
        Me.lblPurge1PosYUnit.Name = "lblPurge1PosYUnit"
        Me.ToolTip1.SetToolTip(Me.lblPurge1PosYUnit, resources.GetString("lblPurge1PosYUnit.ToolTip"))
        '
        'lblPurge1PosXUnit
        '
        resources.ApplyResources(Me.lblPurge1PosXUnit, "lblPurge1PosXUnit")
        Me.lblPurge1PosXUnit.Name = "lblPurge1PosXUnit"
        Me.ToolTip1.SetToolTip(Me.lblPurge1PosXUnit, resources.GetString("lblPurge1PosXUnit.ToolTip"))
        '
        'lblPurgeAlign1PosZ
        '
        resources.ApplyResources(Me.lblPurgeAlign1PosZ, "lblPurgeAlign1PosZ")
        Me.lblPurgeAlign1PosZ.Name = "lblPurgeAlign1PosZ"
        Me.ToolTip1.SetToolTip(Me.lblPurgeAlign1PosZ, resources.GetString("lblPurgeAlign1PosZ.ToolTip"))
        '
        'lblPurgeAlign1PosY
        '
        resources.ApplyResources(Me.lblPurgeAlign1PosY, "lblPurgeAlign1PosY")
        Me.lblPurgeAlign1PosY.Name = "lblPurgeAlign1PosY"
        Me.ToolTip1.SetToolTip(Me.lblPurgeAlign1PosY, resources.GetString("lblPurgeAlign1PosY.ToolTip"))
        '
        'lblPurgeAlign1PosX
        '
        resources.ApplyResources(Me.lblPurgeAlign1PosX, "lblPurgeAlign1PosX")
        Me.lblPurgeAlign1PosX.Name = "lblPurgeAlign1PosX"
        Me.ToolTip1.SetToolTip(Me.lblPurgeAlign1PosX, resources.GetString("lblPurgeAlign1PosX.ToolTip"))
        '
        'lblSceneSet
        '
        resources.ApplyResources(Me.lblSceneSet, "lblSceneSet")
        Me.lblSceneSet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSceneSet.Name = "lblSceneSet"
        Me.ToolTip1.SetToolTip(Me.lblSceneSet, resources.GetString("lblSceneSet.ToolTip"))
        '
        'lblScene
        '
        resources.ApplyResources(Me.lblScene, "lblScene")
        Me.lblScene.Name = "lblScene"
        Me.ToolTip1.SetToolTip(Me.lblScene, resources.GetString("lblScene.ToolTip"))
        '
        'btnTrainScene
        '
        resources.ApplyResources(Me.btnTrainScene, "btnTrainScene")
        Me.btnTrainScene.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.setup1
        Me.btnTrainScene.FlatAppearance.BorderSize = 0
        Me.btnTrainScene.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnTrainScene.Name = "btnTrainScene"
        Me.ToolTip1.SetToolTip(Me.btnTrainScene, resources.GetString("btnTrainScene.ToolTip"))
        Me.btnTrainScene.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 300
        '
        'UcDisplay1
        '
        resources.ApplyResources(Me.UcDisplay1, "UcDisplay1")
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UcDisplay1.Name = "UcDisplay1"
        Me.ToolTip1.SetToolTip(Me.UcDisplay1, resources.GetString("UcDisplay1.ToolTip"))
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.nmcExposure)
        Me.GroupBox1.Controls.Add(Me.lblExposureTimeUnit)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'nmcExposure
        '
        resources.ApplyResources(Me.nmcExposure, "nmcExposure")
        Me.nmcExposure.DecimalPlaces = 1
        Me.nmcExposure.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmcExposure.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.nmcExposure.Name = "nmcExposure"
        Me.ToolTip1.SetToolTip(Me.nmcExposure, resources.GetString("nmcExposure.ToolTip"))
        '
        'lblExposureTimeUnit
        '
        resources.ApplyResources(Me.lblExposureTimeUnit, "lblExposureTimeUnit")
        Me.lblExposureTimeUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblExposureTimeUnit.Name = "lblExposureTimeUnit"
        Me.ToolTip1.SetToolTip(Me.lblExposureTimeUnit, resources.GetString("lblExposureTimeUnit.ToolTip"))
        '
        'gpTiltMove
        '
        resources.ApplyResources(Me.gpTiltMove, "gpTiltMove")
        Me.gpTiltMove.Controls.Add(Me.txtTiltValvePosB)
        Me.gpTiltMove.Controls.Add(Me.Label5)
        Me.gpTiltMove.Controls.Add(Me.Label6)
        Me.gpTiltMove.Controls.Add(Me.btnGoTilt)
        Me.gpTiltMove.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gpTiltMove.Name = "gpTiltMove"
        Me.gpTiltMove.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gpTiltMove, resources.GetString("gpTiltMove.ToolTip"))
        '
        'txtTiltValvePosB
        '
        resources.ApplyResources(Me.txtTiltValvePosB, "txtTiltValvePosB")
        Me.txtTiltValvePosB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTiltValvePosB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtTiltValvePosB.Name = "txtTiltValvePosB"
        Me.ToolTip1.SetToolTip(Me.txtTiltValvePosB, resources.GetString("txtTiltValvePosB.ToolTip"))
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        Me.ToolTip1.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip"))
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label6.Name = "Label6"
        Me.ToolTip1.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip"))
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
        'btnCalibrationPurgeCancel
        '
        resources.ApplyResources(Me.btnCalibrationPurgeCancel, "btnCalibrationPurgeCancel")
        Me.btnCalibrationPurgeCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnCalibrationPurgeCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCalibrationPurgeCancel.Name = "btnCalibrationPurgeCancel"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationPurgeCancel, resources.GetString("btnCalibrationPurgeCancel.ToolTip"))
        Me.btnCalibrationPurgeCancel.UseVisualStyleBackColor = True
        '
        'btnCalibrationPurgeOK
        '
        resources.ApplyResources(Me.btnCalibrationPurgeOK, "btnCalibrationPurgeOK")
        Me.btnCalibrationPurgeOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnCalibrationPurgeOK.Name = "btnCalibrationPurgeOK"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationPurgeOK, resources.GetString("btnCalibrationPurgeOK.ToolTip"))
        Me.btnCalibrationPurgeOK.UseVisualStyleBackColor = True
        '
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        Me.ToolTip1.SetToolTip(Me.UcStatusBar1, resources.GetString("UcStatusBar1.ToolTip"))
        '
        'btnPurgeNext
        '
        resources.ApplyResources(Me.btnPurgeNext, "btnPurgeNext")
        Me.btnPurgeNext.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnPurgeNext.Name = "btnPurgeNext"
        Me.ToolTip1.SetToolTip(Me.btnPurgeNext, resources.GetString("btnPurgeNext.ToolTip"))
        Me.btnPurgeNext.UseVisualStyleBackColor = True
        '
        'btnPurgePrev
        '
        resources.ApplyResources(Me.btnPurgePrev, "btnPurgePrev")
        Me.btnPurgePrev.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnPurgePrev.Name = "btnPurgePrev"
        Me.ToolTip1.SetToolTip(Me.btnPurgePrev, resources.GetString("btnPurgePrev.ToolTip"))
        Me.btnPurgePrev.UseVisualStyleBackColor = True
        '
        'UcLightControl1
        '
        resources.ApplyResources(Me.UcLightControl1, "UcLightControl1")
        Me.UcLightControl1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcLightControl1.Name = "UcLightControl1"
        Me.ToolTip1.SetToolTip(Me.UcLightControl1, resources.GetString("UcLightControl1.ToolTip"))
        '
        'UcJoyStick1
        '
        resources.ApplyResources(Me.UcJoyStick1, "UcJoyStick1")
        Me.UcJoyStick1.AXisA = 0
        Me.UcJoyStick1.AXisB = 0
        Me.UcJoyStick1.AXisC = 0
        Me.UcJoyStick1.AxisX = 0
        Me.UcJoyStick1.AxisY = 0
        Me.UcJoyStick1.AxisZ = 0
        Me.UcJoyStick1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcJoyStick1.ForeColor = System.Drawing.SystemColors.Control
        Me.UcJoyStick1.Name = "UcJoyStick1"
        Me.ToolTip1.SetToolTip(Me.UcJoyStick1, resources.GetString("UcJoyStick1.ToolTip"))
        '
        'frmCalibrationPurge
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnPurgeNext)
        Me.Controls.Add(Me.btnPurgePrev)
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.gpTiltMove)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpPurgeAlignPos)
        Me.Controls.Add(Me.gpbPurge)
        Me.Controls.Add(Me.UcLightControl1)
        Me.Controls.Add(Me.UcDisplay1)
        Me.Controls.Add(Me.btnCalibrationPurgeCancel)
        Me.Controls.Add(Me.btnCalibrationPurgeOK)
        Me.Controls.Add(Me.UcJoyStick1)
        Me.Controls.Add(Me.grpHeightSensorPos)
        Me.Controls.Add(Me.grpCCDPos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCalibrationPurge"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.grpHeightSensorPos.ResumeLayout(False)
        Me.grpHeightSensorPos.PerformLayout()
        Me.grpCCDPos.ResumeLayout(False)
        Me.grpCCDPos.PerformLayout()
        Me.gpbPurge.ResumeLayout(False)
        CType(Me.nmcJettingValveNo1CycleTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcDispenserNo1PurgeTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPurgeAlignPos.ResumeLayout(False)
        Me.grpPurgeAlignPos.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nmcExposure, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpTiltMove.ResumeLayout(False)
        Me.gpTiltMove.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcLightControl1 As ucLightControl
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents btnCalibrationPurgeCancel As System.Windows.Forms.Button
    Friend WithEvents btnCalibrationPurgeOK As System.Windows.Forms.Button
    Friend WithEvents UcJoyStick1 As ucJoyStick
    Friend WithEvents grpHeightSensorPos As System.Windows.Forms.GroupBox
    Friend WithEvents txtCalibrationPurgePosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationPurgePosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationPurgePosX As System.Windows.Forms.TextBox
    Friend WithEvents btnCalibrationPurgeGoPos As System.Windows.Forms.Button
    Friend WithEvents btnCalibrationSetPurgePos As System.Windows.Forms.Button
    Friend WithEvents lblLaserPosZ As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosY As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosX As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosXUnit As System.Windows.Forms.Label
    Friend WithEvents grpCCDPos As System.Windows.Forms.GroupBox
    Friend WithEvents txtCalibrationPurgeCCDPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationPurgeCCDPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationPurgeCCDPosX As System.Windows.Forms.TextBox
    Friend WithEvents btnCalibrationPurgeGoCCDPos As System.Windows.Forms.Button
    Friend WithEvents btnCalibrationPurgeSetCcdPos As System.Windows.Forms.Button
    Friend WithEvents lblCCDPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosZ As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosY As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosX As System.Windows.Forms.Label
    Friend WithEvents gpbPurge As System.Windows.Forms.GroupBox
    Friend WithEvents nmcJettingValveNo1CycleTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcDispenserNo1PurgeTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnPurge As System.Windows.Forms.Button
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents lblCycleTime As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblPurgeTime As System.Windows.Forms.Label
    Friend WithEvents grpPurgeAlignPos As System.Windows.Forms.GroupBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents txtCalibrationPurgeAlign3PosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationPurgeAlign3PosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationPurgeAlign3PosX As System.Windows.Forms.TextBox
    Friend WithEvents btnCalibrationPurgeGoAlign3Pos As System.Windows.Forms.Button
    Friend WithEvents btnCalibrationPurgeSetAlign3Pos As System.Windows.Forms.Button
    Friend WithEvents lblPurge3PosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblPurge3PosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblPurge3PosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblPurgeAlign3PosZ As System.Windows.Forms.Label
    Friend WithEvents lblPurgeAlign3PosY As System.Windows.Forms.Label
    Friend WithEvents lblPurgeAlign3PosX As System.Windows.Forms.Label
    Friend WithEvents btnAlign As System.Windows.Forms.Button
    Friend WithEvents txtCalibrationPurgeAlign2PosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationPurgeAlign2PosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationPurgeAlign2PosX As System.Windows.Forms.TextBox
    Friend WithEvents btnCalibrationPurgeGoAlign2Pos As System.Windows.Forms.Button
    Friend WithEvents btnCalibrationPurgeSetAlign2Pos As System.Windows.Forms.Button
    Friend WithEvents lblPurge2PosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblPurge2PosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblPurge2PosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblPurgeAlign2PosZ As System.Windows.Forms.Label
    Friend WithEvents lblPurgeAlign2PosY As System.Windows.Forms.Label
    Friend WithEvents lblPurgeAlign2PosX As System.Windows.Forms.Label
    Friend WithEvents txtCalibrationPurgeAlign1PosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationPurgeAlign1PosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationPurgeAlign1PosX As System.Windows.Forms.TextBox
    Friend WithEvents btnCalibrationPurgeGoAlign1Pos As System.Windows.Forms.Button
    Friend WithEvents btnCalibrationPurgeSetAlign1Pos As System.Windows.Forms.Button
    Friend WithEvents lblPurge1PosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblPurge1PosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblPurge1PosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblPurgeAlign1PosZ As System.Windows.Forms.Label
    Friend WithEvents lblPurgeAlign1PosY As System.Windows.Forms.Label
    Friend WithEvents lblPurgeAlign1PosX As System.Windows.Forms.Label
    Friend WithEvents lblSceneSet As System.Windows.Forms.Label
    Friend WithEvents lblScene As System.Windows.Forms.Label
    Friend WithEvents btnTrainScene As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents nmcExposure As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblExposureTimeUnit As System.Windows.Forms.Label
    Friend WithEvents gpTiltMove As System.Windows.Forms.GroupBox
    Friend WithEvents txtTiltValvePosB As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnGoTilt As System.Windows.Forms.Button
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents btnPurgeNext As System.Windows.Forms.Button
    Friend WithEvents btnPurgePrev As System.Windows.Forms.Button
    Friend WithEvents btnPurgeVacuumOnOff As System.Windows.Forms.Button
    Friend WithEvents lblPurgeVacuum As System.Windows.Forms.Label
    Friend WithEvents palVacuumReady As System.Windows.Forms.Panel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

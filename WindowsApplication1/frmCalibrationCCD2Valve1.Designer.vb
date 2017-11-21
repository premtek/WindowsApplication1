<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalibrationCCD2Valve1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalibrationCCD2Valve1))
        Me.grpValvePos = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPinZHightPos = New System.Windows.Forms.TextBox()
        Me.btnGetLaser = New System.Windows.Forms.Button()
        Me.lblLaserReaderValue = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtValvePosZ = New System.Windows.Forms.TextBox()
        Me.txtValvePosY = New System.Windows.Forms.TextBox()
        Me.txtValvePosX = New System.Windows.Forms.TextBox()
        Me.btnGoValve1Pos = New System.Windows.Forms.Button()
        Me.btnSetValvePos = New System.Windows.Forms.Button()
        Me.lblAxisZ2 = New System.Windows.Forms.Label()
        Me.lblAxisY2 = New System.Windows.Forms.Label()
        Me.lblAxisX2 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblUnitZ2 = New System.Windows.Forms.Label()
        Me.lblUnitY2 = New System.Windows.Forms.Label()
        Me.lblUnitX2 = New System.Windows.Forms.Label()
        Me.grpCCDPos = New System.Windows.Forms.GroupBox()
        Me.txtCCDPosZ = New System.Windows.Forms.TextBox()
        Me.txtCCDPosY = New System.Windows.Forms.TextBox()
        Me.txtCCDPosX = New System.Windows.Forms.TextBox()
        Me.btnGoCCDPos = New System.Windows.Forms.Button()
        Me.btnSetCcdPos = New System.Windows.Forms.Button()
        Me.lblUnitZ = New System.Windows.Forms.Label()
        Me.lblUnitY = New System.Windows.Forms.Label()
        Me.lblUnitX = New System.Windows.Forms.Label()
        Me.lblAxisZ = New System.Windows.Forms.Label()
        Me.lblAxisY = New System.Windows.Forms.Label()
        Me.lblAxisX = New System.Windows.Forms.Label()
        Me.txtTiltValvePosB = New System.Windows.Forms.TextBox()
        Me.lblLTC = New System.Windows.Forms.Label()
        Me.lblMPa = New System.Windows.Forms.Label()
        Me.btnValve1SyringePressureOnOff = New System.Windows.Forms.Button()
        Me.btnSetSyringePressure = New System.Windows.Forms.Button()
        Me.txtSyringePressure = New System.Windows.Forms.TextBox()
        Me.lblDispenserNo1EPRegulator = New System.Windows.Forms.Label()
        Me.btnValve1On = New System.Windows.Forms.Button()
        Me.btnAutoCalibration = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.grpValveCalibration = New System.Windows.Forms.GroupBox()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnALign = New System.Windows.Forms.Button()
        Me.btnGoTilt = New System.Windows.Forms.Button()
        Me.btnSetTiltPos = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCCDToValveNext = New System.Windows.Forms.Button()
        Me.btnTrainValveScene = New System.Windows.Forms.Button()
        Me.btCalibrationValveParameter = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.nmcExposure = New System.Windows.Forms.NumericUpDown()
        Me.lblExposureTimeUnit = New System.Windows.Forms.Label()
        Me.btnRemoveKey = New System.Windows.Forms.Button()
        Me.lbDegree = New System.Windows.Forms.Label()
        Me.cboB = New System.Windows.Forms.ComboBox()
        Me.gbTiltSet = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblSceneSet = New System.Windows.Forms.Label()
        Me.lblScene = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblOffsetYUnit = New System.Windows.Forms.Label()
        Me.lblOffsetXUnit = New System.Windows.Forms.Label()
        Me.lblOffsetY = New System.Windows.Forms.Label()
        Me.lblOffsetX = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblOffsetXText = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCCDStableTime = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSetSyringe1OnOff = New System.Windows.Forms.Button()
        Me.btnSetAirPressure = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtAirPressure = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtPitch = New System.Windows.Forms.TextBox()
        Me.txtCount = New System.Windows.Forms.TextBox()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.lblPitch = New System.Windows.Forms.Label()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.lbConvertTiltAngle = New System.Windows.Forms.Label()
        Me.btnCalibrationValveDot = New System.Windows.Forms.Button()
        Me.UcLightControl1 = New WindowsApplication1.ucLightControl()
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.grpValvePos.SuspendLayout()
        Me.grpCCDPos.SuspendLayout()
        Me.grpValveCalibration.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nmcExposure, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTiltSet.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpValvePos
        '
        resources.ApplyResources(Me.grpValvePos, "grpValvePos")
        Me.grpValvePos.Controls.Add(Me.Label3)
        Me.grpValvePos.Controls.Add(Me.Label2)
        Me.grpValvePos.Controls.Add(Me.txtPinZHightPos)
        Me.grpValvePos.Controls.Add(Me.btnGetLaser)
        Me.grpValvePos.Controls.Add(Me.lblLaserReaderValue)
        Me.grpValvePos.Controls.Add(Me.Label4)
        Me.grpValvePos.Controls.Add(Me.txtValvePosZ)
        Me.grpValvePos.Controls.Add(Me.txtValvePosY)
        Me.grpValvePos.Controls.Add(Me.txtValvePosX)
        Me.grpValvePos.Controls.Add(Me.btnGoValve1Pos)
        Me.grpValvePos.Controls.Add(Me.btnSetValvePos)
        Me.grpValvePos.Controls.Add(Me.lblAxisZ2)
        Me.grpValvePos.Controls.Add(Me.lblAxisY2)
        Me.grpValvePos.Controls.Add(Me.lblAxisX2)
        Me.grpValvePos.Controls.Add(Me.Label12)
        Me.grpValvePos.Controls.Add(Me.lblUnitZ2)
        Me.grpValvePos.Controls.Add(Me.lblUnitY2)
        Me.grpValvePos.Controls.Add(Me.lblUnitX2)
        Me.grpValvePos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpValvePos.Name = "grpValvePos"
        Me.grpValvePos.TabStop = False
        Me.ToolTip.SetToolTip(Me.grpValvePos, resources.GetString("grpValvePos.ToolTip"))
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        Me.ToolTip.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip"))
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Name = "Label2"
        Me.ToolTip.SetToolTip(Me.Label2, resources.GetString("Label2.ToolTip"))
        '
        'txtPinZHightPos
        '
        resources.ApplyResources(Me.txtPinZHightPos, "txtPinZHightPos")
        Me.txtPinZHightPos.BackColor = System.Drawing.Color.White
        Me.txtPinZHightPos.Name = "txtPinZHightPos"
        Me.ToolTip.SetToolTip(Me.txtPinZHightPos, resources.GetString("txtPinZHightPos.ToolTip"))
        '
        'btnGetLaser
        '
        resources.ApplyResources(Me.btnGetLaser, "btnGetLaser")
        Me.btnGetLaser.Name = "btnGetLaser"
        Me.ToolTip.SetToolTip(Me.btnGetLaser, resources.GetString("btnGetLaser.ToolTip"))
        Me.btnGetLaser.UseVisualStyleBackColor = True
        '
        'lblLaserReaderValue
        '
        resources.ApplyResources(Me.lblLaserReaderValue, "lblLaserReaderValue")
        Me.lblLaserReaderValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLaserReaderValue.Name = "lblLaserReaderValue"
        Me.ToolTip.SetToolTip(Me.lblLaserReaderValue, resources.GetString("lblLaserReaderValue.ToolTip"))
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        Me.ToolTip.SetToolTip(Me.Label4, resources.GetString("Label4.ToolTip"))
        '
        'txtValvePosZ
        '
        resources.ApplyResources(Me.txtValvePosZ, "txtValvePosZ")
        Me.txtValvePosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtValvePosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtValvePosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtValvePosZ.Name = "txtValvePosZ"
        Me.txtValvePosZ.ReadOnly = True
        Me.ToolTip.SetToolTip(Me.txtValvePosZ, resources.GetString("txtValvePosZ.ToolTip"))
        '
        'txtValvePosY
        '
        resources.ApplyResources(Me.txtValvePosY, "txtValvePosY")
        Me.txtValvePosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtValvePosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtValvePosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtValvePosY.Name = "txtValvePosY"
        Me.txtValvePosY.ReadOnly = True
        Me.ToolTip.SetToolTip(Me.txtValvePosY, resources.GetString("txtValvePosY.ToolTip"))
        '
        'txtValvePosX
        '
        resources.ApplyResources(Me.txtValvePosX, "txtValvePosX")
        Me.txtValvePosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtValvePosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtValvePosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtValvePosX.Name = "txtValvePosX"
        Me.txtValvePosX.ReadOnly = True
        Me.ToolTip.SetToolTip(Me.txtValvePosX, resources.GetString("txtValvePosX.ToolTip"))
        '
        'btnGoValve1Pos
        '
        resources.ApplyResources(Me.btnGoValve1Pos, "btnGoValve1Pos")
        Me.btnGoValve1Pos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnGoValve1Pos.FlatAppearance.BorderSize = 0
        Me.btnGoValve1Pos.Name = "btnGoValve1Pos"
        Me.ToolTip.SetToolTip(Me.btnGoValve1Pos, resources.GetString("btnGoValve1Pos.ToolTip"))
        Me.btnGoValve1Pos.UseVisualStyleBackColor = True
        '
        'btnSetValvePos
        '
        resources.ApplyResources(Me.btnSetValvePos, "btnSetValvePos")
        Me.btnSetValvePos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetValvePos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetValvePos.FlatAppearance.BorderSize = 0
        Me.btnSetValvePos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetValvePos.Name = "btnSetValvePos"
        Me.ToolTip.SetToolTip(Me.btnSetValvePos, resources.GetString("btnSetValvePos.ToolTip"))
        Me.btnSetValvePos.UseVisualStyleBackColor = True
        '
        'lblAxisZ2
        '
        resources.ApplyResources(Me.lblAxisZ2, "lblAxisZ2")
        Me.lblAxisZ2.Name = "lblAxisZ2"
        Me.ToolTip.SetToolTip(Me.lblAxisZ2, resources.GetString("lblAxisZ2.ToolTip"))
        '
        'lblAxisY2
        '
        resources.ApplyResources(Me.lblAxisY2, "lblAxisY2")
        Me.lblAxisY2.Name = "lblAxisY2"
        Me.ToolTip.SetToolTip(Me.lblAxisY2, resources.GetString("lblAxisY2.ToolTip"))
        '
        'lblAxisX2
        '
        resources.ApplyResources(Me.lblAxisX2, "lblAxisX2")
        Me.lblAxisX2.Name = "lblAxisX2"
        Me.ToolTip.SetToolTip(Me.lblAxisX2, resources.GetString("lblAxisX2.ToolTip"))
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label12.Name = "Label12"
        Me.ToolTip.SetToolTip(Me.Label12, resources.GetString("Label12.ToolTip"))
        '
        'lblUnitZ2
        '
        resources.ApplyResources(Me.lblUnitZ2, "lblUnitZ2")
        Me.lblUnitZ2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUnitZ2.Name = "lblUnitZ2"
        Me.ToolTip.SetToolTip(Me.lblUnitZ2, resources.GetString("lblUnitZ2.ToolTip"))
        '
        'lblUnitY2
        '
        resources.ApplyResources(Me.lblUnitY2, "lblUnitY2")
        Me.lblUnitY2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUnitY2.Name = "lblUnitY2"
        Me.ToolTip.SetToolTip(Me.lblUnitY2, resources.GetString("lblUnitY2.ToolTip"))
        '
        'lblUnitX2
        '
        resources.ApplyResources(Me.lblUnitX2, "lblUnitX2")
        Me.lblUnitX2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUnitX2.Name = "lblUnitX2"
        Me.ToolTip.SetToolTip(Me.lblUnitX2, resources.GetString("lblUnitX2.ToolTip"))
        '
        'grpCCDPos
        '
        resources.ApplyResources(Me.grpCCDPos, "grpCCDPos")
        Me.grpCCDPos.Controls.Add(Me.txtCCDPosZ)
        Me.grpCCDPos.Controls.Add(Me.txtCCDPosY)
        Me.grpCCDPos.Controls.Add(Me.txtCCDPosX)
        Me.grpCCDPos.Controls.Add(Me.btnGoCCDPos)
        Me.grpCCDPos.Controls.Add(Me.btnSetCcdPos)
        Me.grpCCDPos.Controls.Add(Me.lblUnitZ)
        Me.grpCCDPos.Controls.Add(Me.lblUnitY)
        Me.grpCCDPos.Controls.Add(Me.lblUnitX)
        Me.grpCCDPos.Controls.Add(Me.lblAxisZ)
        Me.grpCCDPos.Controls.Add(Me.lblAxisY)
        Me.grpCCDPos.Controls.Add(Me.lblAxisX)
        Me.grpCCDPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpCCDPos.Name = "grpCCDPos"
        Me.grpCCDPos.TabStop = False
        Me.ToolTip.SetToolTip(Me.grpCCDPos, resources.GetString("grpCCDPos.ToolTip"))
        '
        'txtCCDPosZ
        '
        resources.ApplyResources(Me.txtCCDPosZ, "txtCCDPosZ")
        Me.txtCCDPosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCCDPosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosZ.Name = "txtCCDPosZ"
        Me.txtCCDPosZ.ReadOnly = True
        Me.ToolTip.SetToolTip(Me.txtCCDPosZ, resources.GetString("txtCCDPosZ.ToolTip"))
        '
        'txtCCDPosY
        '
        resources.ApplyResources(Me.txtCCDPosY, "txtCCDPosY")
        Me.txtCCDPosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCCDPosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosY.Name = "txtCCDPosY"
        Me.txtCCDPosY.ReadOnly = True
        Me.ToolTip.SetToolTip(Me.txtCCDPosY, resources.GetString("txtCCDPosY.ToolTip"))
        '
        'txtCCDPosX
        '
        resources.ApplyResources(Me.txtCCDPosX, "txtCCDPosX")
        Me.txtCCDPosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCCDPosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosX.Name = "txtCCDPosX"
        Me.txtCCDPosX.ReadOnly = True
        Me.ToolTip.SetToolTip(Me.txtCCDPosX, resources.GetString("txtCCDPosX.ToolTip"))
        '
        'btnGoCCDPos
        '
        resources.ApplyResources(Me.btnGoCCDPos, "btnGoCCDPos")
        Me.btnGoCCDPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnGoCCDPos.FlatAppearance.BorderSize = 0
        Me.btnGoCCDPos.Name = "btnGoCCDPos"
        Me.ToolTip.SetToolTip(Me.btnGoCCDPos, resources.GetString("btnGoCCDPos.ToolTip"))
        Me.btnGoCCDPos.UseVisualStyleBackColor = True
        '
        'btnSetCcdPos
        '
        resources.ApplyResources(Me.btnSetCcdPos, "btnSetCcdPos")
        Me.btnSetCcdPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetCcdPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetCcdPos.FlatAppearance.BorderSize = 0
        Me.btnSetCcdPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetCcdPos.Name = "btnSetCcdPos"
        Me.ToolTip.SetToolTip(Me.btnSetCcdPos, resources.GetString("btnSetCcdPos.ToolTip"))
        Me.btnSetCcdPos.UseVisualStyleBackColor = True
        '
        'lblUnitZ
        '
        resources.ApplyResources(Me.lblUnitZ, "lblUnitZ")
        Me.lblUnitZ.Name = "lblUnitZ"
        Me.ToolTip.SetToolTip(Me.lblUnitZ, resources.GetString("lblUnitZ.ToolTip"))
        '
        'lblUnitY
        '
        resources.ApplyResources(Me.lblUnitY, "lblUnitY")
        Me.lblUnitY.Name = "lblUnitY"
        Me.ToolTip.SetToolTip(Me.lblUnitY, resources.GetString("lblUnitY.ToolTip"))
        '
        'lblUnitX
        '
        resources.ApplyResources(Me.lblUnitX, "lblUnitX")
        Me.lblUnitX.Name = "lblUnitX"
        Me.ToolTip.SetToolTip(Me.lblUnitX, resources.GetString("lblUnitX.ToolTip"))
        '
        'lblAxisZ
        '
        resources.ApplyResources(Me.lblAxisZ, "lblAxisZ")
        Me.lblAxisZ.Name = "lblAxisZ"
        Me.ToolTip.SetToolTip(Me.lblAxisZ, resources.GetString("lblAxisZ.ToolTip"))
        '
        'lblAxisY
        '
        resources.ApplyResources(Me.lblAxisY, "lblAxisY")
        Me.lblAxisY.Name = "lblAxisY"
        Me.ToolTip.SetToolTip(Me.lblAxisY, resources.GetString("lblAxisY.ToolTip"))
        '
        'lblAxisX
        '
        resources.ApplyResources(Me.lblAxisX, "lblAxisX")
        Me.lblAxisX.Name = "lblAxisX"
        Me.ToolTip.SetToolTip(Me.lblAxisX, resources.GetString("lblAxisX.ToolTip"))
        '
        'txtTiltValvePosB
        '
        resources.ApplyResources(Me.txtTiltValvePosB, "txtTiltValvePosB")
        Me.txtTiltValvePosB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTiltValvePosB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtTiltValvePosB.Name = "txtTiltValvePosB"
        Me.ToolTip.SetToolTip(Me.txtTiltValvePosB, resources.GetString("txtTiltValvePosB.ToolTip"))
        '
        'lblLTC
        '
        resources.ApplyResources(Me.lblLTC, "lblLTC")
        Me.lblLTC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLTC.Name = "lblLTC"
        Me.ToolTip.SetToolTip(Me.lblLTC, resources.GetString("lblLTC.ToolTip"))
        '
        'lblMPa
        '
        resources.ApplyResources(Me.lblMPa, "lblMPa")
        Me.lblMPa.BackColor = System.Drawing.Color.Transparent
        Me.lblMPa.Name = "lblMPa"
        Me.ToolTip.SetToolTip(Me.lblMPa, resources.GetString("lblMPa.ToolTip"))
        '
        'btnValve1SyringePressureOnOff
        '
        resources.ApplyResources(Me.btnValve1SyringePressureOnOff, "btnValve1SyringePressureOnOff")
        Me.btnValve1SyringePressureOnOff.BackColor = System.Drawing.SystemColors.Control
        Me.btnValve1SyringePressureOnOff.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnValve1SyringePressureOnOff.FlatAppearance.BorderSize = 0
        Me.btnValve1SyringePressureOnOff.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnValve1SyringePressureOnOff.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnValve1SyringePressureOnOff.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnValve1SyringePressureOnOff.Name = "btnValve1SyringePressureOnOff"
        Me.btnValve1SyringePressureOnOff.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnValve1SyringePressureOnOff, resources.GetString("btnValve1SyringePressureOnOff.ToolTip"))
        Me.btnValve1SyringePressureOnOff.UseVisualStyleBackColor = True
        '
        'btnSetSyringePressure
        '
        resources.ApplyResources(Me.btnSetSyringePressure, "btnSetSyringePressure")
        Me.btnSetSyringePressure.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetSyringePressure.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnSetSyringePressure.FlatAppearance.BorderSize = 0
        Me.btnSetSyringePressure.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnSetSyringePressure.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnSetSyringePressure.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetSyringePressure.Name = "btnSetSyringePressure"
        Me.btnSetSyringePressure.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnSetSyringePressure, resources.GetString("btnSetSyringePressure.ToolTip"))
        Me.btnSetSyringePressure.UseVisualStyleBackColor = True
        '
        'txtSyringePressure
        '
        resources.ApplyResources(Me.txtSyringePressure, "txtSyringePressure")
        Me.txtSyringePressure.BackColor = System.Drawing.Color.White
        Me.txtSyringePressure.Name = "txtSyringePressure"
        Me.ToolTip.SetToolTip(Me.txtSyringePressure, resources.GetString("txtSyringePressure.ToolTip"))
        '
        'lblDispenserNo1EPRegulator
        '
        resources.ApplyResources(Me.lblDispenserNo1EPRegulator, "lblDispenserNo1EPRegulator")
        Me.lblDispenserNo1EPRegulator.BackColor = System.Drawing.Color.Transparent
        Me.lblDispenserNo1EPRegulator.Name = "lblDispenserNo1EPRegulator"
        Me.ToolTip.SetToolTip(Me.lblDispenserNo1EPRegulator, resources.GetString("lblDispenserNo1EPRegulator.ToolTip"))
        '
        'btnValve1On
        '
        resources.ApplyResources(Me.btnValve1On, "btnValve1On")
        Me.btnValve1On.FlatAppearance.BorderSize = 0
        Me.btnValve1On.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnValve1On.Name = "btnValve1On"
        Me.ToolTip.SetToolTip(Me.btnValve1On, resources.GetString("btnValve1On.ToolTip"))
        Me.btnValve1On.UseVisualStyleBackColor = True
        '
        'btnAutoCalibration
        '
        resources.ApplyResources(Me.btnAutoCalibration, "btnAutoCalibration")
        Me.btnAutoCalibration.FlatAppearance.BorderSize = 0
        Me.btnAutoCalibration.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnAutoCalibration.Name = "btnAutoCalibration"
        Me.ToolTip.SetToolTip(Me.btnAutoCalibration, resources.GetString("btnAutoCalibration.ToolTip"))
        Me.btnAutoCalibration.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 300
        '
        'grpValveCalibration
        '
        resources.ApplyResources(Me.grpValveCalibration, "grpValveCalibration")
        Me.grpValveCalibration.Controls.Add(Me.lblDispenserNo1EPRegulator)
        Me.grpValveCalibration.Controls.Add(Me.txtSyringePressure)
        Me.grpValveCalibration.Controls.Add(Me.lblMPa)
        Me.grpValveCalibration.Controls.Add(Me.btnValve1On)
        Me.grpValveCalibration.Controls.Add(Me.btnValve1SyringePressureOnOff)
        Me.grpValveCalibration.Controls.Add(Me.btnSetSyringePressure)
        Me.grpValveCalibration.Controls.Add(Me.lblLTC)
        Me.grpValveCalibration.Name = "grpValveCalibration"
        Me.grpValveCalibration.TabStop = False
        Me.ToolTip.SetToolTip(Me.grpValveCalibration, resources.GetString("grpValveCalibration.ToolTip"))
        '
        'btnALign
        '
        resources.ApplyResources(Me.btnALign, "btnALign")
        Me.btnALign.FlatAppearance.BorderSize = 0
        Me.btnALign.Name = "btnALign"
        Me.ToolTip.SetToolTip(Me.btnALign, resources.GetString("btnALign.ToolTip"))
        Me.btnALign.UseVisualStyleBackColor = True
        '
        'btnGoTilt
        '
        resources.ApplyResources(Me.btnGoTilt, "btnGoTilt")
        Me.btnGoTilt.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnGoTilt.FlatAppearance.BorderSize = 0
        Me.btnGoTilt.Name = "btnGoTilt"
        Me.ToolTip.SetToolTip(Me.btnGoTilt, resources.GetString("btnGoTilt.ToolTip"))
        Me.btnGoTilt.UseVisualStyleBackColor = True
        '
        'btnSetTiltPos
        '
        resources.ApplyResources(Me.btnSetTiltPos, "btnSetTiltPos")
        Me.btnSetTiltPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetTiltPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetTiltPos.FlatAppearance.BorderSize = 0
        Me.btnSetTiltPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetTiltPos.Name = "btnSetTiltPos"
        Me.ToolTip.SetToolTip(Me.btnSetTiltPos, resources.GetString("btnSetTiltPos.ToolTip"))
        Me.btnSetTiltPos.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Name = "btnCancel"
        Me.ToolTip.SetToolTip(Me.btnCancel, resources.GetString("btnCancel.ToolTip"))
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnOK.Name = "btnOK"
        Me.ToolTip.SetToolTip(Me.btnOK, resources.GetString("btnOK.ToolTip"))
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCCDToValveNext
        '
        resources.ApplyResources(Me.btnCCDToValveNext, "btnCCDToValveNext")
        Me.btnCCDToValveNext.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCCDToValveNext.Name = "btnCCDToValveNext"
        Me.ToolTip.SetToolTip(Me.btnCCDToValveNext, resources.GetString("btnCCDToValveNext.ToolTip"))
        Me.btnCCDToValveNext.UseVisualStyleBackColor = True
        '
        'btnTrainValveScene
        '
        resources.ApplyResources(Me.btnTrainValveScene, "btnTrainValveScene")
        Me.btnTrainValveScene.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.setup1
        Me.btnTrainValveScene.FlatAppearance.BorderSize = 0
        Me.btnTrainValveScene.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnTrainValveScene.Name = "btnTrainValveScene"
        Me.ToolTip.SetToolTip(Me.btnTrainValveScene, resources.GetString("btnTrainValveScene.ToolTip"))
        Me.btnTrainValveScene.UseVisualStyleBackColor = True
        '
        'btCalibrationValveParameter
        '
        resources.ApplyResources(Me.btCalibrationValveParameter, "btCalibrationValveParameter")
        Me.btCalibrationValveParameter.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        Me.btCalibrationValveParameter.FlatAppearance.BorderSize = 0
        Me.btCalibrationValveParameter.Name = "btCalibrationValveParameter"
        Me.ToolTip.SetToolTip(Me.btCalibrationValveParameter, resources.GetString("btCalibrationValveParameter.ToolTip"))
        Me.btCalibrationValveParameter.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.nmcExposure)
        Me.GroupBox1.Controls.Add(Me.lblExposureTimeUnit)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'nmcExposure
        '
        resources.ApplyResources(Me.nmcExposure, "nmcExposure")
        Me.nmcExposure.DecimalPlaces = 1
        Me.nmcExposure.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmcExposure.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.nmcExposure.Name = "nmcExposure"
        Me.ToolTip.SetToolTip(Me.nmcExposure, resources.GetString("nmcExposure.ToolTip"))
        '
        'lblExposureTimeUnit
        '
        resources.ApplyResources(Me.lblExposureTimeUnit, "lblExposureTimeUnit")
        Me.lblExposureTimeUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblExposureTimeUnit.Name = "lblExposureTimeUnit"
        Me.ToolTip.SetToolTip(Me.lblExposureTimeUnit, resources.GetString("lblExposureTimeUnit.ToolTip"))
        '
        'btnRemoveKey
        '
        resources.ApplyResources(Me.btnRemoveKey, "btnRemoveKey")
        Me.btnRemoveKey.BackColor = System.Drawing.SystemColors.Control
        Me.btnRemoveKey.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnRemoveKey.FlatAppearance.BorderSize = 0
        Me.btnRemoveKey.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnRemoveKey.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnRemoveKey.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnRemoveKey.Name = "btnRemoveKey"
        Me.ToolTip.SetToolTip(Me.btnRemoveKey, resources.GetString("btnRemoveKey.ToolTip"))
        Me.btnRemoveKey.UseVisualStyleBackColor = True
        '
        'lbDegree
        '
        resources.ApplyResources(Me.lbDegree, "lbDegree")
        Me.lbDegree.Name = "lbDegree"
        Me.ToolTip.SetToolTip(Me.lbDegree, resources.GetString("lbDegree.ToolTip"))
        '
        'cboB
        '
        resources.ApplyResources(Me.cboB, "cboB")
        Me.cboB.FormattingEnabled = True
        Me.cboB.Name = "cboB"
        Me.ToolTip.SetToolTip(Me.cboB, resources.GetString("cboB.ToolTip"))
        '
        'gbTiltSet
        '
        resources.ApplyResources(Me.gbTiltSet, "gbTiltSet")
        Me.gbTiltSet.Controls.Add(Me.txtTiltValvePosB)
        Me.gbTiltSet.Controls.Add(Me.Label5)
        Me.gbTiltSet.Controls.Add(Me.Label6)
        Me.gbTiltSet.Controls.Add(Me.btnGoTilt)
        Me.gbTiltSet.Controls.Add(Me.btnSetTiltPos)
        Me.gbTiltSet.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gbTiltSet.Name = "gbTiltSet"
        Me.gbTiltSet.TabStop = False
        Me.ToolTip.SetToolTip(Me.gbTiltSet, resources.GetString("gbTiltSet.ToolTip"))
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        Me.ToolTip.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip"))
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label6.Name = "Label6"
        Me.ToolTip.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip"))
        '
        'lblSceneSet
        '
        resources.ApplyResources(Me.lblSceneSet, "lblSceneSet")
        Me.lblSceneSet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSceneSet.Name = "lblSceneSet"
        Me.ToolTip.SetToolTip(Me.lblSceneSet, resources.GetString("lblSceneSet.ToolTip"))
        '
        'lblScene
        '
        resources.ApplyResources(Me.lblScene, "lblScene")
        Me.lblScene.Name = "lblScene"
        Me.ToolTip.SetToolTip(Me.lblScene, resources.GetString("lblScene.ToolTip"))
        '
        'GroupBox3
        '
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Controls.Add(Me.lblOffsetYUnit)
        Me.GroupBox3.Controls.Add(Me.lblOffsetXUnit)
        Me.GroupBox3.Controls.Add(Me.lblOffsetY)
        Me.GroupBox3.Controls.Add(Me.lblOffsetX)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.lblOffsetXText)
        Me.GroupBox3.Controls.Add(Me.btnALign)
        Me.GroupBox3.Controls.Add(Me.lblScene)
        Me.GroupBox3.Controls.Add(Me.btnTrainValveScene)
        Me.GroupBox3.Controls.Add(Me.lblSceneSet)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        Me.ToolTip.SetToolTip(Me.GroupBox3, resources.GetString("GroupBox3.ToolTip"))
        '
        'lblOffsetYUnit
        '
        resources.ApplyResources(Me.lblOffsetYUnit, "lblOffsetYUnit")
        Me.lblOffsetYUnit.Name = "lblOffsetYUnit"
        Me.ToolTip.SetToolTip(Me.lblOffsetYUnit, resources.GetString("lblOffsetYUnit.ToolTip"))
        '
        'lblOffsetXUnit
        '
        resources.ApplyResources(Me.lblOffsetXUnit, "lblOffsetXUnit")
        Me.lblOffsetXUnit.Name = "lblOffsetXUnit"
        Me.ToolTip.SetToolTip(Me.lblOffsetXUnit, resources.GetString("lblOffsetXUnit.ToolTip"))
        '
        'lblOffsetY
        '
        resources.ApplyResources(Me.lblOffsetY, "lblOffsetY")
        Me.lblOffsetY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOffsetY.Name = "lblOffsetY"
        Me.ToolTip.SetToolTip(Me.lblOffsetY, resources.GetString("lblOffsetY.ToolTip"))
        '
        'lblOffsetX
        '
        resources.ApplyResources(Me.lblOffsetX, "lblOffsetX")
        Me.lblOffsetX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOffsetX.Name = "lblOffsetX"
        Me.ToolTip.SetToolTip(Me.lblOffsetX, resources.GetString("lblOffsetX.ToolTip"))
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        Me.ToolTip.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip"))
        '
        'lblOffsetXText
        '
        resources.ApplyResources(Me.lblOffsetXText, "lblOffsetXText")
        Me.lblOffsetXText.Name = "lblOffsetXText"
        Me.ToolTip.SetToolTip(Me.lblOffsetXText, resources.GetString("lblOffsetXText.ToolTip"))
        '
        'GroupBox4
        '
        resources.ApplyResources(Me.GroupBox4, "GroupBox4")
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.txtCCDStableTime)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.btnSetSyringe1OnOff)
        Me.GroupBox4.Controls.Add(Me.btnSetAirPressure)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.txtAirPressure)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.lblCount)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.txtPitch)
        Me.GroupBox4.Controls.Add(Me.txtCount)
        Me.GroupBox4.Controls.Add(Me.lblUnit)
        Me.GroupBox4.Controls.Add(Me.lblPitch)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.TabStop = False
        Me.ToolTip.SetToolTip(Me.GroupBox4, resources.GetString("GroupBox4.ToolTip"))
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Name = "Label11"
        Me.ToolTip.SetToolTip(Me.Label11, resources.GetString("Label11.ToolTip"))
        '
        'txtCCDStableTime
        '
        resources.ApplyResources(Me.txtCCDStableTime, "txtCCDStableTime")
        Me.txtCCDStableTime.BackColor = System.Drawing.Color.White
        Me.txtCCDStableTime.Name = "txtCCDStableTime"
        Me.ToolTip.SetToolTip(Me.txtCCDStableTime, resources.GetString("txtCCDStableTime.ToolTip"))
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Name = "Label1"
        Me.ToolTip.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'btnSetSyringe1OnOff
        '
        resources.ApplyResources(Me.btnSetSyringe1OnOff, "btnSetSyringe1OnOff")
        Me.btnSetSyringe1OnOff.Name = "btnSetSyringe1OnOff"
        Me.ToolTip.SetToolTip(Me.btnSetSyringe1OnOff, resources.GetString("btnSetSyringe1OnOff.ToolTip"))
        Me.btnSetSyringe1OnOff.UseVisualStyleBackColor = True
        '
        'btnSetAirPressure
        '
        resources.ApplyResources(Me.btnSetAirPressure, "btnSetAirPressure")
        Me.btnSetAirPressure.Name = "btnSetAirPressure"
        Me.ToolTip.SetToolTip(Me.btnSetAirPressure, resources.GetString("btnSetAirPressure.ToolTip"))
        Me.btnSetAirPressure.UseVisualStyleBackColor = True
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Name = "Label10"
        Me.ToolTip.SetToolTip(Me.Label10, resources.GetString("Label10.ToolTip"))
        '
        'txtAirPressure
        '
        resources.ApplyResources(Me.txtAirPressure, "txtAirPressure")
        Me.txtAirPressure.BackColor = System.Drawing.Color.White
        Me.txtAirPressure.Name = "txtAirPressure"
        Me.ToolTip.SetToolTip(Me.txtAirPressure, resources.GetString("txtAirPressure.ToolTip"))
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Name = "Label9"
        Me.ToolTip.SetToolTip(Me.Label9, resources.GetString("Label9.ToolTip"))
        '
        'lblCount
        '
        resources.ApplyResources(Me.lblCount, "lblCount")
        Me.lblCount.BackColor = System.Drawing.Color.Transparent
        Me.lblCount.Name = "lblCount"
        Me.ToolTip.SetToolTip(Me.lblCount, resources.GetString("lblCount.ToolTip"))
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Name = "Label8"
        Me.ToolTip.SetToolTip(Me.Label8, resources.GetString("Label8.ToolTip"))
        '
        'txtPitch
        '
        resources.ApplyResources(Me.txtPitch, "txtPitch")
        Me.txtPitch.BackColor = System.Drawing.Color.White
        Me.txtPitch.Name = "txtPitch"
        Me.ToolTip.SetToolTip(Me.txtPitch, resources.GetString("txtPitch.ToolTip"))
        '
        'txtCount
        '
        resources.ApplyResources(Me.txtCount, "txtCount")
        Me.txtCount.BackColor = System.Drawing.Color.White
        Me.txtCount.Name = "txtCount"
        Me.ToolTip.SetToolTip(Me.txtCount, resources.GetString("txtCount.ToolTip"))
        '
        'lblUnit
        '
        resources.ApplyResources(Me.lblUnit, "lblUnit")
        Me.lblUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblUnit.Name = "lblUnit"
        Me.ToolTip.SetToolTip(Me.lblUnit, resources.GetString("lblUnit.ToolTip"))
        '
        'lblPitch
        '
        resources.ApplyResources(Me.lblPitch, "lblPitch")
        Me.lblPitch.BackColor = System.Drawing.Color.Transparent
        Me.lblPitch.Name = "lblPitch"
        Me.ToolTip.SetToolTip(Me.lblPitch, resources.GetString("lblPitch.ToolTip"))
        '
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        Me.ToolTip.SetToolTip(Me.UcStatusBar1, resources.GetString("UcStatusBar1.ToolTip"))
        '
        'UcDisplay1
        '
        resources.ApplyResources(Me.UcDisplay1, "UcDisplay1")
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UcDisplay1.Name = "UcDisplay1"
        Me.ToolTip.SetToolTip(Me.UcDisplay1, resources.GetString("UcDisplay1.ToolTip"))
        '
        'lbConvertTiltAngle
        '
        resources.ApplyResources(Me.lbConvertTiltAngle, "lbConvertTiltAngle")
        Me.lbConvertTiltAngle.Name = "lbConvertTiltAngle"
        Me.ToolTip.SetToolTip(Me.lbConvertTiltAngle, resources.GetString("lbConvertTiltAngle.ToolTip"))
        '
        'btnCalibrationValveDot
        '
        resources.ApplyResources(Me.btnCalibrationValveDot, "btnCalibrationValveDot")
        Me.btnCalibrationValveDot.FlatAppearance.BorderSize = 0
        Me.btnCalibrationValveDot.Name = "btnCalibrationValveDot"
        Me.ToolTip.SetToolTip(Me.btnCalibrationValveDot, resources.GetString("btnCalibrationValveDot.ToolTip"))
        Me.btnCalibrationValveDot.UseVisualStyleBackColor = True
        '
        'UcLightControl1
        '
        resources.ApplyResources(Me.UcLightControl1, "UcLightControl1")
        Me.UcLightControl1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcLightControl1.Name = "UcLightControl1"
        Me.ToolTip.SetToolTip(Me.UcLightControl1, resources.GetString("UcLightControl1.ToolTip"))
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
        Me.ToolTip.SetToolTip(Me.UcJoyStick1, resources.GetString("UcJoyStick1.ToolTip"))
        '
        'frmCalibrationCCD2Valve1
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCalibrationValveDot)
        Me.Controls.Add(Me.lbConvertTiltAngle)
        Me.Controls.Add(Me.btCalibrationValveParameter)
        Me.Controls.Add(Me.btnCCDToValveNext)
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.btnAutoCalibration)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.gbTiltSet)
        Me.Controls.Add(Me.btnRemoveKey)
        Me.Controls.Add(Me.lbDegree)
        Me.Controls.Add(Me.cboB)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.UcLightControl1)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.grpValveCalibration)
        Me.Controls.Add(Me.UcJoyStick1)
        Me.Controls.Add(Me.grpValvePos)
        Me.Controls.Add(Me.grpCCDPos)
        Me.Controls.Add(Me.UcDisplay1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCalibrationCCD2Valve1"
        Me.ToolTip.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.grpValvePos.ResumeLayout(False)
        Me.grpValvePos.PerformLayout
        Me.grpCCDPos.ResumeLayout(false)
        Me.grpCCDPos.PerformLayout
        Me.grpValveCalibration.ResumeLayout(false)
        Me.grpValveCalibration.PerformLayout
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        CType(Me.nmcExposure,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbTiltSet.ResumeLayout(false)
        Me.gbTiltSet.PerformLayout
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.GroupBox4.ResumeLayout(false)
        Me.GroupBox4.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents grpValvePos As System.Windows.Forms.GroupBox
    Friend WithEvents btnSetValvePos As System.Windows.Forms.Button
    Friend WithEvents lblAxisZ2 As System.Windows.Forms.Label
    Friend WithEvents lblAxisY2 As System.Windows.Forms.Label
    Friend WithEvents lblAxisX2 As System.Windows.Forms.Label
    Friend WithEvents lblUnitZ2 As System.Windows.Forms.Label
    Friend WithEvents lblUnitY2 As System.Windows.Forms.Label
    Friend WithEvents lblUnitX2 As System.Windows.Forms.Label
    Friend WithEvents grpCCDPos As System.Windows.Forms.GroupBox
    Friend WithEvents btnSetCcdPos As System.Windows.Forms.Button
    Friend WithEvents lblUnitZ As System.Windows.Forms.Label
    Friend WithEvents lblUnitY As System.Windows.Forms.Label
    Friend WithEvents lblUnitX As System.Windows.Forms.Label
    Friend WithEvents lblAxisZ As System.Windows.Forms.Label
    Friend WithEvents lblAxisY As System.Windows.Forms.Label
    Friend WithEvents lblAxisX As System.Windows.Forms.Label
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents btnGoCCDPos As System.Windows.Forms.Button
    Friend WithEvents btnGoValve1Pos As System.Windows.Forms.Button
    Friend WithEvents btnValve1On As System.Windows.Forms.Button
    Friend WithEvents btnAutoCalibration As System.Windows.Forms.Button
    Friend WithEvents btnValve1SyringePressureOnOff As System.Windows.Forms.Button
    Friend WithEvents btnSetSyringePressure As System.Windows.Forms.Button
    Friend WithEvents txtSyringePressure As System.Windows.Forms.TextBox
    Friend WithEvents lblDispenserNo1EPRegulator As System.Windows.Forms.Label
    Friend WithEvents lblMPa As System.Windows.Forms.Label
    Friend WithEvents lblLTC As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents grpValveCalibration As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents txtCCDPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCCDPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCCDPosX As System.Windows.Forms.TextBox
    Friend WithEvents txtValvePosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtValvePosY As System.Windows.Forms.TextBox
    Friend WithEvents txtValvePosX As System.Windows.Forms.TextBox
    Friend WithEvents UcLightControl1 As ucLightControl
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPinZHightPos As System.Windows.Forms.TextBox
    Friend WithEvents btnGetLaser As System.Windows.Forms.Button
    Friend WithEvents lblLaserReaderValue As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents nmcExposure As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblExposureTimeUnit As System.Windows.Forms.Label
    Friend WithEvents txtTiltValvePosB As System.Windows.Forms.TextBox
    Friend WithEvents btnRemoveKey As System.Windows.Forms.Button
    Friend WithEvents lbDegree As System.Windows.Forms.Label
    Friend WithEvents cboB As System.Windows.Forms.ComboBox
    Friend WithEvents gbTiltSet As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnGoTilt As System.Windows.Forms.Button
    Friend WithEvents btnSetTiltPos As System.Windows.Forms.Button
    Friend WithEvents lblSceneSet As System.Windows.Forms.Label
    Friend WithEvents lblScene As System.Windows.Forms.Label
    Friend WithEvents btnTrainValveScene As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnALign As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblOffsetXText As System.Windows.Forms.Label
    Friend WithEvents lblOffsetYUnit As System.Windows.Forms.Label
    Friend WithEvents lblOffsetXUnit As System.Windows.Forms.Label
    Friend WithEvents lblOffsetY As System.Windows.Forms.Label
    Friend WithEvents lblOffsetX As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtAirPressure As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPitch As System.Windows.Forms.TextBox
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents lblUnit As System.Windows.Forms.Label
    Friend WithEvents lblPitch As System.Windows.Forms.Label
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents btnCCDToValveNext As System.Windows.Forms.Button
    Public WithEvents UcJoyStick1 As ucJoyStick
    Friend WithEvents btnSetAirPressure As System.Windows.Forms.Button
    Friend WithEvents btnSetSyringe1OnOff As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCCDStableTime As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btCalibrationValveParameter As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lbConvertTiltAngle As System.Windows.Forms.Label
    Friend WithEvents btnCalibrationValveDot As System.Windows.Forms.Button
End Class

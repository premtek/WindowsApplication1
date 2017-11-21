<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalibrationZHeight
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalibrationZHeight))
        Me.lblValvePosZUnit = New System.Windows.Forms.Label()
        Me.lblLaserSensorUnit = New System.Windows.Forms.Label()
        Me.lblValvePosZ = New System.Windows.Forms.Label()
        Me.lblLaserSensor = New System.Windows.Forms.Label()
        Me.grpValvePos = New System.Windows.Forms.GroupBox()
        Me.palLTC = New System.Windows.Forms.Panel()
        Me.txtValvePosZ = New System.Windows.Forms.TextBox()
        Me.txtValvePosY = New System.Windows.Forms.TextBox()
        Me.txtValvePosX = New System.Windows.Forms.TextBox()
        Me.btnSetValve1Z = New System.Windows.Forms.Button()
        Me.lblLTC = New System.Windows.Forms.Label()
        Me.btnGo2 = New System.Windows.Forms.Button()
        Me.btnSetValvePosX = New System.Windows.Forms.Button()
        Me.lblValvePosY = New System.Windows.Forms.Label()
        Me.lblValvePosX = New System.Windows.Forms.Label()
        Me.lblValvePosYUnit = New System.Windows.Forms.Label()
        Me.lblValvePosXUnit = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.grpLaserPos = New System.Windows.Forms.GroupBox()
        Me.txtLaserValueZ = New System.Windows.Forms.TextBox()
        Me.txtLaserPosZ = New System.Windows.Forms.TextBox()
        Me.txtLaserPosY = New System.Windows.Forms.TextBox()
        Me.txtLaserPosX = New System.Windows.Forms.TextBox()
        Me.btnLaserSensor = New System.Windows.Forms.Button()
        Me.btnGoLaserPinPos = New System.Windows.Forms.Button()
        Me.btnSetLaserPinPos = New System.Windows.Forms.Button()
        Me.lblLaserPosZ = New System.Windows.Forms.Label()
        Me.lblLaserPosY = New System.Windows.Forms.Label()
        Me.lblLaserPosX = New System.Windows.Forms.Label()
        Me.lblLaserPosZUnit = New System.Windows.Forms.Label()
        Me.lblLaserPosYUnit = New System.Windows.Forms.Label()
        Me.lblLaserPosXUnit = New System.Windows.Forms.Label()
        Me.lblTiltValvePosUUnit = New System.Windows.Forms.Label()
        Me.txtTiltValvePosB = New System.Windows.Forms.TextBox()
        Me.lblTiltValvePosB = New System.Windows.Forms.Label()
        Me.btnAutoZFind = New System.Windows.Forms.Button()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.UcLightControl1 = New WindowsApplication1.ucLightControl()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnGoTiltPos = New System.Windows.Forms.Button()
        Me.btnSetTiltPos = New System.Windows.Forms.Button()
        Me.btnValveToHightNext = New System.Windows.Forms.Button()
        Me.btnValveToHightPrev = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.nmcExposure = New System.Windows.Forms.NumericUpDown()
        Me.lblExposureTimeUnit = New System.Windows.Forms.Label()
        Me.btnRemoveKey = New System.Windows.Forms.Button()
        Me.lbDegree = New System.Windows.Forms.Label()
        Me.cboB = New System.Windows.Forms.ComboBox()
        Me.gbTiltSet = New System.Windows.Forms.GroupBox()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.lbConvertTiltAngle = New System.Windows.Forms.Label()
        Me.grpValvePos.SuspendLayout()
        Me.grpLaserPos.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nmcExposure, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTiltSet.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblValvePosZUnit
        '
        resources.ApplyResources(Me.lblValvePosZUnit, "lblValvePosZUnit")
        Me.lblValvePosZUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblValvePosZUnit.Name = "lblValvePosZUnit"
        Me.ToolTip.SetToolTip(Me.lblValvePosZUnit, resources.GetString("lblValvePosZUnit.ToolTip"))
        '
        'lblLaserSensorUnit
        '
        resources.ApplyResources(Me.lblLaserSensorUnit, "lblLaserSensorUnit")
        Me.lblLaserSensorUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblLaserSensorUnit.Name = "lblLaserSensorUnit"
        Me.ToolTip.SetToolTip(Me.lblLaserSensorUnit, resources.GetString("lblLaserSensorUnit.ToolTip"))
        '
        'lblValvePosZ
        '
        resources.ApplyResources(Me.lblValvePosZ, "lblValvePosZ")
        Me.lblValvePosZ.BackColor = System.Drawing.Color.Transparent
        Me.lblValvePosZ.Name = "lblValvePosZ"
        Me.ToolTip.SetToolTip(Me.lblValvePosZ, resources.GetString("lblValvePosZ.ToolTip"))
        '
        'lblLaserSensor
        '
        resources.ApplyResources(Me.lblLaserSensor, "lblLaserSensor")
        Me.lblLaserSensor.BackColor = System.Drawing.Color.Transparent
        Me.lblLaserSensor.Name = "lblLaserSensor"
        Me.ToolTip.SetToolTip(Me.lblLaserSensor, resources.GetString("lblLaserSensor.ToolTip"))
        '
        'grpValvePos
        '
        resources.ApplyResources(Me.grpValvePos, "grpValvePos")
        Me.grpValvePos.Controls.Add(Me.palLTC)
        Me.grpValvePos.Controls.Add(Me.txtValvePosZ)
        Me.grpValvePos.Controls.Add(Me.txtValvePosY)
        Me.grpValvePos.Controls.Add(Me.txtValvePosX)
        Me.grpValvePos.Controls.Add(Me.btnSetValve1Z)
        Me.grpValvePos.Controls.Add(Me.lblValvePosZUnit)
        Me.grpValvePos.Controls.Add(Me.lblLTC)
        Me.grpValvePos.Controls.Add(Me.lblValvePosZ)
        Me.grpValvePos.Controls.Add(Me.btnGo2)
        Me.grpValvePos.Controls.Add(Me.btnSetValvePosX)
        Me.grpValvePos.Controls.Add(Me.lblValvePosY)
        Me.grpValvePos.Controls.Add(Me.lblValvePosX)
        Me.grpValvePos.Controls.Add(Me.lblValvePosYUnit)
        Me.grpValvePos.Controls.Add(Me.lblValvePosXUnit)
        Me.grpValvePos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpValvePos.Name = "grpValvePos"
        Me.grpValvePos.TabStop = False
        Me.ToolTip.SetToolTip(Me.grpValvePos, resources.GetString("grpValvePos.ToolTip"))
        '
        'palLTC
        '
        resources.ApplyResources(Me.palLTC, "palLTC")
        Me.palLTC.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.li_23
        Me.palLTC.Name = "palLTC"
        Me.ToolTip.SetToolTip(Me.palLTC, resources.GetString("palLTC.ToolTip"))
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
        'btnSetValve1Z
        '
        resources.ApplyResources(Me.btnSetValve1Z, "btnSetValve1Z")
        Me.btnSetValve1Z.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetValve1Z.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetValve1Z.FlatAppearance.BorderSize = 0
        Me.btnSetValve1Z.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetValve1Z.Name = "btnSetValve1Z"
        Me.ToolTip.SetToolTip(Me.btnSetValve1Z, resources.GetString("btnSetValve1Z.ToolTip"))
        Me.btnSetValve1Z.UseVisualStyleBackColor = True
        '
        'lblLTC
        '
        resources.ApplyResources(Me.lblLTC, "lblLTC")
        Me.lblLTC.Name = "lblLTC"
        Me.ToolTip.SetToolTip(Me.lblLTC, resources.GetString("lblLTC.ToolTip"))
        '
        'btnGo2
        '
        resources.ApplyResources(Me.btnGo2, "btnGo2")
        Me.btnGo2.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnGo2.FlatAppearance.BorderSize = 0
        Me.btnGo2.Name = "btnGo2"
        Me.ToolTip.SetToolTip(Me.btnGo2, resources.GetString("btnGo2.ToolTip"))
        Me.btnGo2.UseVisualStyleBackColor = True
        '
        'btnSetValvePosX
        '
        resources.ApplyResources(Me.btnSetValvePosX, "btnSetValvePosX")
        Me.btnSetValvePosX.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetValvePosX.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetValvePosX.FlatAppearance.BorderSize = 0
        Me.btnSetValvePosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetValvePosX.Name = "btnSetValvePosX"
        Me.ToolTip.SetToolTip(Me.btnSetValvePosX, resources.GetString("btnSetValvePosX.ToolTip"))
        Me.btnSetValvePosX.UseVisualStyleBackColor = True
        '
        'lblValvePosY
        '
        resources.ApplyResources(Me.lblValvePosY, "lblValvePosY")
        Me.lblValvePosY.Name = "lblValvePosY"
        Me.ToolTip.SetToolTip(Me.lblValvePosY, resources.GetString("lblValvePosY.ToolTip"))
        '
        'lblValvePosX
        '
        resources.ApplyResources(Me.lblValvePosX, "lblValvePosX")
        Me.lblValvePosX.Name = "lblValvePosX"
        Me.ToolTip.SetToolTip(Me.lblValvePosX, resources.GetString("lblValvePosX.ToolTip"))
        '
        'lblValvePosYUnit
        '
        resources.ApplyResources(Me.lblValvePosYUnit, "lblValvePosYUnit")
        Me.lblValvePosYUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblValvePosYUnit.Name = "lblValvePosYUnit"
        Me.ToolTip.SetToolTip(Me.lblValvePosYUnit, resources.GetString("lblValvePosYUnit.ToolTip"))
        '
        'lblValvePosXUnit
        '
        resources.ApplyResources(Me.lblValvePosXUnit, "lblValvePosXUnit")
        Me.lblValvePosXUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblValvePosXUnit.Name = "lblValvePosXUnit"
        Me.ToolTip.SetToolTip(Me.lblValvePosXUnit, resources.GetString("lblValvePosXUnit.ToolTip"))
        '
        'Timer1
        '
        '
        'grpLaserPos
        '
        resources.ApplyResources(Me.grpLaserPos, "grpLaserPos")
        Me.grpLaserPos.Controls.Add(Me.txtLaserValueZ)
        Me.grpLaserPos.Controls.Add(Me.txtLaserPosZ)
        Me.grpLaserPos.Controls.Add(Me.txtLaserPosY)
        Me.grpLaserPos.Controls.Add(Me.txtLaserPosX)
        Me.grpLaserPos.Controls.Add(Me.btnLaserSensor)
        Me.grpLaserPos.Controls.Add(Me.btnGoLaserPinPos)
        Me.grpLaserPos.Controls.Add(Me.btnSetLaserPinPos)
        Me.grpLaserPos.Controls.Add(Me.lblLaserPosZ)
        Me.grpLaserPos.Controls.Add(Me.lblLaserPosY)
        Me.grpLaserPos.Controls.Add(Me.lblLaserSensorUnit)
        Me.grpLaserPos.Controls.Add(Me.lblLaserPosX)
        Me.grpLaserPos.Controls.Add(Me.lblLaserPosZUnit)
        Me.grpLaserPos.Controls.Add(Me.lblLaserPosYUnit)
        Me.grpLaserPos.Controls.Add(Me.lblLaserSensor)
        Me.grpLaserPos.Controls.Add(Me.lblLaserPosXUnit)
        Me.grpLaserPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpLaserPos.Name = "grpLaserPos"
        Me.grpLaserPos.TabStop = False
        Me.ToolTip.SetToolTip(Me.grpLaserPos, resources.GetString("grpLaserPos.ToolTip"))
        '
        'txtLaserValueZ
        '
        resources.ApplyResources(Me.txtLaserValueZ, "txtLaserValueZ")
        Me.txtLaserValueZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtLaserValueZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLaserValueZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtLaserValueZ.Name = "txtLaserValueZ"
        Me.txtLaserValueZ.ReadOnly = True
        Me.ToolTip.SetToolTip(Me.txtLaserValueZ, resources.GetString("txtLaserValueZ.ToolTip"))
        '
        'txtLaserPosZ
        '
        resources.ApplyResources(Me.txtLaserPosZ, "txtLaserPosZ")
        Me.txtLaserPosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtLaserPosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLaserPosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtLaserPosZ.Name = "txtLaserPosZ"
        Me.txtLaserPosZ.ReadOnly = True
        Me.ToolTip.SetToolTip(Me.txtLaserPosZ, resources.GetString("txtLaserPosZ.ToolTip"))
        '
        'txtLaserPosY
        '
        resources.ApplyResources(Me.txtLaserPosY, "txtLaserPosY")
        Me.txtLaserPosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtLaserPosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLaserPosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtLaserPosY.Name = "txtLaserPosY"
        Me.txtLaserPosY.ReadOnly = True
        Me.ToolTip.SetToolTip(Me.txtLaserPosY, resources.GetString("txtLaserPosY.ToolTip"))
        '
        'txtLaserPosX
        '
        resources.ApplyResources(Me.txtLaserPosX, "txtLaserPosX")
        Me.txtLaserPosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtLaserPosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLaserPosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtLaserPosX.Name = "txtLaserPosX"
        Me.txtLaserPosX.ReadOnly = True
        Me.ToolTip.SetToolTip(Me.txtLaserPosX, resources.GetString("txtLaserPosX.ToolTip"))
        '
        'btnLaserSensor
        '
        resources.ApplyResources(Me.btnLaserSensor, "btnLaserSensor")
        Me.btnLaserSensor.BackColor = System.Drawing.SystemColors.Control
        Me.btnLaserSensor.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnLaserSensor.FlatAppearance.BorderSize = 0
        Me.btnLaserSensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnLaserSensor.Name = "btnLaserSensor"
        Me.ToolTip.SetToolTip(Me.btnLaserSensor, resources.GetString("btnLaserSensor.ToolTip"))
        Me.btnLaserSensor.UseVisualStyleBackColor = True
        '
        'btnGoLaserPinPos
        '
        resources.ApplyResources(Me.btnGoLaserPinPos, "btnGoLaserPinPos")
        Me.btnGoLaserPinPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnGoLaserPinPos.FlatAppearance.BorderSize = 0
        Me.btnGoLaserPinPos.Name = "btnGoLaserPinPos"
        Me.ToolTip.SetToolTip(Me.btnGoLaserPinPos, resources.GetString("btnGoLaserPinPos.ToolTip"))
        Me.btnGoLaserPinPos.UseVisualStyleBackColor = True
        '
        'btnSetLaserPinPos
        '
        resources.ApplyResources(Me.btnSetLaserPinPos, "btnSetLaserPinPos")
        Me.btnSetLaserPinPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetLaserPinPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetLaserPinPos.FlatAppearance.BorderSize = 0
        Me.btnSetLaserPinPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetLaserPinPos.Name = "btnSetLaserPinPos"
        Me.ToolTip.SetToolTip(Me.btnSetLaserPinPos, resources.GetString("btnSetLaserPinPos.ToolTip"))
        Me.btnSetLaserPinPos.UseVisualStyleBackColor = True
        '
        'lblLaserPosZ
        '
        resources.ApplyResources(Me.lblLaserPosZ, "lblLaserPosZ")
        Me.lblLaserPosZ.Name = "lblLaserPosZ"
        Me.ToolTip.SetToolTip(Me.lblLaserPosZ, resources.GetString("lblLaserPosZ.ToolTip"))
        '
        'lblLaserPosY
        '
        resources.ApplyResources(Me.lblLaserPosY, "lblLaserPosY")
        Me.lblLaserPosY.Name = "lblLaserPosY"
        Me.ToolTip.SetToolTip(Me.lblLaserPosY, resources.GetString("lblLaserPosY.ToolTip"))
        '
        'lblLaserPosX
        '
        resources.ApplyResources(Me.lblLaserPosX, "lblLaserPosX")
        Me.lblLaserPosX.Name = "lblLaserPosX"
        Me.ToolTip.SetToolTip(Me.lblLaserPosX, resources.GetString("lblLaserPosX.ToolTip"))
        '
        'lblLaserPosZUnit
        '
        resources.ApplyResources(Me.lblLaserPosZUnit, "lblLaserPosZUnit")
        Me.lblLaserPosZUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLaserPosZUnit.Name = "lblLaserPosZUnit"
        Me.ToolTip.SetToolTip(Me.lblLaserPosZUnit, resources.GetString("lblLaserPosZUnit.ToolTip"))
        '
        'lblLaserPosYUnit
        '
        resources.ApplyResources(Me.lblLaserPosYUnit, "lblLaserPosYUnit")
        Me.lblLaserPosYUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLaserPosYUnit.Name = "lblLaserPosYUnit"
        Me.ToolTip.SetToolTip(Me.lblLaserPosYUnit, resources.GetString("lblLaserPosYUnit.ToolTip"))
        '
        'lblLaserPosXUnit
        '
        resources.ApplyResources(Me.lblLaserPosXUnit, "lblLaserPosXUnit")
        Me.lblLaserPosXUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLaserPosXUnit.Name = "lblLaserPosXUnit"
        Me.ToolTip.SetToolTip(Me.lblLaserPosXUnit, resources.GetString("lblLaserPosXUnit.ToolTip"))
        '
        'lblTiltValvePosUUnit
        '
        resources.ApplyResources(Me.lblTiltValvePosUUnit, "lblTiltValvePosUUnit")
        Me.lblTiltValvePosUUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblTiltValvePosUUnit.Name = "lblTiltValvePosUUnit"
        Me.ToolTip.SetToolTip(Me.lblTiltValvePosUUnit, resources.GetString("lblTiltValvePosUUnit.ToolTip"))
        '
        'txtTiltValvePosB
        '
        resources.ApplyResources(Me.txtTiltValvePosB, "txtTiltValvePosB")
        Me.txtTiltValvePosB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTiltValvePosB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtTiltValvePosB.Name = "txtTiltValvePosB"
        Me.ToolTip.SetToolTip(Me.txtTiltValvePosB, resources.GetString("txtTiltValvePosB.ToolTip"))
        '
        'lblTiltValvePosB
        '
        resources.ApplyResources(Me.lblTiltValvePosB, "lblTiltValvePosB")
        Me.lblTiltValvePosB.Name = "lblTiltValvePosB"
        Me.ToolTip.SetToolTip(Me.lblTiltValvePosB, resources.GetString("lblTiltValvePosB.ToolTip"))
        '
        'btnAutoZFind
        '
        resources.ApplyResources(Me.btnAutoZFind, "btnAutoZFind")
        Me.btnAutoZFind.BackColor = System.Drawing.SystemColors.Control
        Me.btnAutoZFind.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnAutoZFind.FlatAppearance.BorderSize = 0
        Me.btnAutoZFind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnAutoZFind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnAutoZFind.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnAutoZFind.Name = "btnAutoZFind"
        Me.ToolTip.SetToolTip(Me.btnAutoZFind, resources.GetString("btnAutoZFind.ToolTip"))
        Me.btnAutoZFind.UseVisualStyleBackColor = True
        '
        'UcDisplay1
        '
        resources.ApplyResources(Me.UcDisplay1, "UcDisplay1")
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UcDisplay1.Name = "UcDisplay1"
        Me.ToolTip.SetToolTip(Me.UcDisplay1, resources.GetString("UcDisplay1.ToolTip"))
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
        Me.UcJoyStick1.Name = "UcJoyStick1"
        Me.ToolTip.SetToolTip(Me.UcJoyStick1, resources.GetString("UcJoyStick1.ToolTip"))
        '
        'UcLightControl1
        '
        resources.ApplyResources(Me.UcLightControl1, "UcLightControl1")
        Me.UcLightControl1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcLightControl1.Name = "UcLightControl1"
        Me.ToolTip.SetToolTip(Me.UcLightControl1, resources.GetString("UcLightControl1.ToolTip"))
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
        'btnGoTiltPos
        '
        resources.ApplyResources(Me.btnGoTiltPos, "btnGoTiltPos")
        Me.btnGoTiltPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnGoTiltPos.FlatAppearance.BorderSize = 0
        Me.btnGoTiltPos.Name = "btnGoTiltPos"
        Me.ToolTip.SetToolTip(Me.btnGoTiltPos, resources.GetString("btnGoTiltPos.ToolTip"))
        Me.btnGoTiltPos.UseVisualStyleBackColor = True
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
        'btnValveToHightNext
        '
        resources.ApplyResources(Me.btnValveToHightNext, "btnValveToHightNext")
        Me.btnValveToHightNext.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnValveToHightNext.Name = "btnValveToHightNext"
        Me.ToolTip.SetToolTip(Me.btnValveToHightNext, resources.GetString("btnValveToHightNext.ToolTip"))
        Me.btnValveToHightNext.UseVisualStyleBackColor = True
        '
        'btnValveToHightPrev
        '
        resources.ApplyResources(Me.btnValveToHightPrev, "btnValveToHightPrev")
        Me.btnValveToHightPrev.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnValveToHightPrev.Name = "btnValveToHightPrev"
        Me.ToolTip.SetToolTip(Me.btnValveToHightPrev, resources.GetString("btnValveToHightPrev.ToolTip"))
        Me.btnValveToHightPrev.UseVisualStyleBackColor = True
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
        Me.gbTiltSet.Controls.Add(Me.lblTiltValvePosUUnit)
        Me.gbTiltSet.Controls.Add(Me.btnGoTiltPos)
        Me.gbTiltSet.Controls.Add(Me.txtTiltValvePosB)
        Me.gbTiltSet.Controls.Add(Me.btnSetTiltPos)
        Me.gbTiltSet.Controls.Add(Me.lblTiltValvePosB)
        Me.gbTiltSet.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gbTiltSet.Name = "gbTiltSet"
        Me.gbTiltSet.TabStop = False
        Me.ToolTip.SetToolTip(Me.gbTiltSet, resources.GetString("gbTiltSet.ToolTip"))
        '
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        Me.ToolTip.SetToolTip(Me.UcStatusBar1, resources.GetString("UcStatusBar1.ToolTip"))
        '
        'lbConvertTiltAngle
        '
        resources.ApplyResources(Me.lbConvertTiltAngle, "lbConvertTiltAngle")
        Me.lbConvertTiltAngle.Name = "lbConvertTiltAngle"
        Me.ToolTip.SetToolTip(Me.lbConvertTiltAngle, resources.GetString("lbConvertTiltAngle.ToolTip"))
        '
        'frmCalibrationZHeight
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.lbConvertTiltAngle)
        Me.Controls.Add(Me.btnValveToHightNext)
        Me.Controls.Add(Me.btnValveToHightPrev)
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.gbTiltSet)
        Me.Controls.Add(Me.btnRemoveKey)
        Me.Controls.Add(Me.lbDegree)
        Me.Controls.Add(Me.cboB)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.UcLightControl1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.grpLaserPos)
        Me.Controls.Add(Me.btnAutoZFind)
        Me.Controls.Add(Me.grpValvePos)
        Me.Controls.Add(Me.UcDisplay1)
        Me.Controls.Add(Me.UcJoyStick1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCalibrationZHeight"
        Me.ToolTip.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpValvePos.ResumeLayout(False)
        Me.grpValvePos.PerformLayout()
        Me.grpLaserPos.ResumeLayout(False)
        Me.grpLaserPos.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nmcExposure, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTiltSet.ResumeLayout(False)
        Me.gbTiltSet.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents UcJoyStick1 As ucJoyStick
    Friend WithEvents lblValvePosZ As System.Windows.Forms.Label
    Friend WithEvents lblLaserSensor As System.Windows.Forms.Label
    Friend WithEvents lblValvePosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblLaserSensorUnit As System.Windows.Forms.Label
    Friend WithEvents btnLaserSensor As System.Windows.Forms.Button
    Friend WithEvents grpValvePos As System.Windows.Forms.GroupBox
    Friend WithEvents btnGo2 As System.Windows.Forms.Button
    Friend WithEvents btnSetValvePosX As System.Windows.Forms.Button
    Friend WithEvents lblValvePosY As System.Windows.Forms.Label
    Friend WithEvents lblValvePosX As System.Windows.Forms.Label
    Friend WithEvents lblValvePosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblValvePosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblLTC As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents grpLaserPos As System.Windows.Forms.GroupBox
    Friend WithEvents btnGoLaserPinPos As System.Windows.Forms.Button
    Friend WithEvents btnSetLaserPinPos As System.Windows.Forms.Button
    Friend WithEvents lblLaserPosZ As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosY As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosX As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosXUnit As System.Windows.Forms.Label
    Friend WithEvents btnSetValve1Z As System.Windows.Forms.Button
    Friend WithEvents btnAutoZFind As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents txtValvePosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtValvePosY As System.Windows.Forms.TextBox
    Friend WithEvents txtValvePosX As System.Windows.Forms.TextBox
    Friend WithEvents txtLaserValueZ As System.Windows.Forms.TextBox
    Friend WithEvents txtLaserPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtLaserPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtLaserPosX As System.Windows.Forms.TextBox
    Friend WithEvents UcLightControl1 As ucLightControl
    Friend WithEvents palLTC As System.Windows.Forms.Panel
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents nmcExposure As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblExposureTimeUnit As System.Windows.Forms.Label
    Friend WithEvents btnRemoveKey As System.Windows.Forms.Button
    Friend WithEvents lbDegree As System.Windows.Forms.Label
    Friend WithEvents cboB As System.Windows.Forms.ComboBox
    Friend WithEvents lblTiltValvePosUUnit As System.Windows.Forms.Label
    Friend WithEvents txtTiltValvePosB As System.Windows.Forms.TextBox
    Friend WithEvents lblTiltValvePosB As System.Windows.Forms.Label
    Friend WithEvents gbTiltSet As System.Windows.Forms.GroupBox
    Friend WithEvents btnGoTiltPos As System.Windows.Forms.Button
    Friend WithEvents btnSetTiltPos As System.Windows.Forms.Button
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents btnValveToHightNext As System.Windows.Forms.Button
    Friend WithEvents btnValveToHightPrev As System.Windows.Forms.Button
    Friend WithEvents lbConvertTiltAngle As System.Windows.Forms.Label
End Class

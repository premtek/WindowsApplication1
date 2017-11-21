<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalibrationFlowRate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalibrationFlowRate))
        Me.UcLightControl1 = New WindowsApplication1.ucLightControl()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.grpHeightSensorPos = New System.Windows.Forms.GroupBox()
        Me.txtCalibrationFlowRatePosZ = New System.Windows.Forms.TextBox()
        Me.txtCalibrationFlowRatePosY = New System.Windows.Forms.TextBox()
        Me.txtCalibrationFlowRatePosX = New System.Windows.Forms.TextBox()
        Me.btnCalibrationFlowRateGoPos = New System.Windows.Forms.Button()
        Me.btnCalibrationSetFlowRatePos = New System.Windows.Forms.Button()
        Me.lblLaserPosZ = New System.Windows.Forms.Label()
        Me.lblLaserPosY = New System.Windows.Forms.Label()
        Me.lblLaserPosX = New System.Windows.Forms.Label()
        Me.lblLaserPosZUnit = New System.Windows.Forms.Label()
        Me.lblLaserPosYUnit = New System.Windows.Forms.Label()
        Me.lblLaserPosXUnit = New System.Windows.Forms.Label()
        Me.grpCCDPos = New System.Windows.Forms.GroupBox()
        Me.txtCalibrationFlowRateCCDPosZ = New System.Windows.Forms.TextBox()
        Me.txtCalibrationFlowRateCCDPosY = New System.Windows.Forms.TextBox()
        Me.txtCalibrationFlowRateCCDPosX = New System.Windows.Forms.TextBox()
        Me.btnCalibrationFlowRateGoCCDPos = New System.Windows.Forms.Button()
        Me.btnCalibrationFlowRateSetCcdPos = New System.Windows.Forms.Button()
        Me.lblCCDPosZUnit = New System.Windows.Forms.Label()
        Me.lblCCDPosYUnit = New System.Windows.Forms.Label()
        Me.lblCCDPosXUnit = New System.Windows.Forms.Label()
        Me.lblCCDPosZ = New System.Windows.Forms.Label()
        Me.lblCCDPosY = New System.Windows.Forms.Label()
        Me.lblCCDPosX = New System.Windows.Forms.Label()
        Me.btnCalibrationFlowRateCancel = New System.Windows.Forms.Button()
        Me.btnCalibrationFlowRateOK = New System.Windows.Forms.Button()
        Me.grpFlowRateAlignPos = New System.Windows.Forms.GroupBox()
        Me.txtCalibrationWeighingAlign3PosZ = New System.Windows.Forms.TextBox()
        Me.txtCalibrationWeighingAlign3PosY = New System.Windows.Forms.TextBox()
        Me.txtCalibrationWeighingAlign3PosX = New System.Windows.Forms.TextBox()
        Me.btnCalibrationWeighingGoAlign3Pos = New System.Windows.Forms.Button()
        Me.btnCalibrationWeighingSetAlign3Pos = New System.Windows.Forms.Button()
        Me.lblWeighing3PosZUnit = New System.Windows.Forms.Label()
        Me.lblWeighing3PosYUnit = New System.Windows.Forms.Label()
        Me.lblWeighing3PosXUnit = New System.Windows.Forms.Label()
        Me.lblWeighingAlign3PosZ = New System.Windows.Forms.Label()
        Me.lblWeighingAlign3PosY = New System.Windows.Forms.Label()
        Me.lblWeighingAlign3PosX = New System.Windows.Forms.Label()
        Me.txtCalibrationWeighingAlign2PosZ = New System.Windows.Forms.TextBox()
        Me.txtCalibrationWeighingAlign2PosY = New System.Windows.Forms.TextBox()
        Me.txtCalibrationWeighingAlign2PosX = New System.Windows.Forms.TextBox()
        Me.btnCalibrationWeighingGoAlign2Pos = New System.Windows.Forms.Button()
        Me.btnCalibrationWeighingSetAlign2Pos = New System.Windows.Forms.Button()
        Me.lblWeighing2PosZUnit = New System.Windows.Forms.Label()
        Me.lblWeighing2PosYUnit = New System.Windows.Forms.Label()
        Me.lblWeighing2PosXUnit = New System.Windows.Forms.Label()
        Me.lblWeighingAlign2PosZ = New System.Windows.Forms.Label()
        Me.lblWeighingAlign2PosY = New System.Windows.Forms.Label()
        Me.lblWeighingAlign2PosX = New System.Windows.Forms.Label()
        Me.lblSceneSet = New System.Windows.Forms.Label()
        Me.lblScene = New System.Windows.Forms.Label()
        Me.btnTrainScene = New System.Windows.Forms.Button()
        Me.btnALign = New System.Windows.Forms.Button()
        Me.txtCalibrationWeighingAlign1PosZ = New System.Windows.Forms.TextBox()
        Me.txtCalibrationWeighingAlign1PosY = New System.Windows.Forms.TextBox()
        Me.txtCalibrationWeighingAlign1PosX = New System.Windows.Forms.TextBox()
        Me.btnCalibrationWeighingGoAlign1Pos = New System.Windows.Forms.Button()
        Me.btnCalibrationWeighingSetAlign1Pos = New System.Windows.Forms.Button()
        Me.lblWeighing1PosZUnit = New System.Windows.Forms.Label()
        Me.lblWeighing1PosYUnit = New System.Windows.Forms.Label()
        Me.lblWeighing1PosXUnit = New System.Windows.Forms.Label()
        Me.lblWeighingAlign1PosZ = New System.Windows.Forms.Label()
        Me.lblWeighingAlign1PosY = New System.Windows.Forms.Label()
        Me.lblWeighingAlign1PosX = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.nmcExposure = New System.Windows.Forms.NumericUpDown()
        Me.lblExposureTimeUnit = New System.Windows.Forms.Label()
        Me.gpTiltMove = New System.Windows.Forms.GroupBox()
        Me.txtTiltValvePosB = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnGoTilt = New System.Windows.Forms.Button()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.btnFlowRateNext = New System.Windows.Forms.Button()
        Me.btnFlowRatePrev = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpHeightSensorPos.SuspendLayout()
        Me.grpCCDPos.SuspendLayout()
        Me.grpFlowRateAlignPos.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nmcExposure, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpTiltMove.SuspendLayout()
        Me.SuspendLayout()
        '
        'UcLightControl1
        '
        resources.ApplyResources(Me.UcLightControl1, "UcLightControl1")
        Me.UcLightControl1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcLightControl1.Name = "UcLightControl1"
        Me.ToolTip1.SetToolTip(Me.UcLightControl1, resources.GetString("UcLightControl1.ToolTip"))
        '
        'UcDisplay1
        '
        resources.ApplyResources(Me.UcDisplay1, "UcDisplay1")
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UcDisplay1.Name = "UcDisplay1"
        Me.ToolTip1.SetToolTip(Me.UcDisplay1, resources.GetString("UcDisplay1.ToolTip"))
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
        'grpHeightSensorPos
        '
        resources.ApplyResources(Me.grpHeightSensorPos, "grpHeightSensorPos")
        Me.grpHeightSensorPos.Controls.Add(Me.txtCalibrationFlowRatePosZ)
        Me.grpHeightSensorPos.Controls.Add(Me.txtCalibrationFlowRatePosY)
        Me.grpHeightSensorPos.Controls.Add(Me.txtCalibrationFlowRatePosX)
        Me.grpHeightSensorPos.Controls.Add(Me.btnCalibrationFlowRateGoPos)
        Me.grpHeightSensorPos.Controls.Add(Me.btnCalibrationSetFlowRatePos)
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
        'txtCalibrationFlowRatePosZ
        '
        resources.ApplyResources(Me.txtCalibrationFlowRatePosZ, "txtCalibrationFlowRatePosZ")
        Me.txtCalibrationFlowRatePosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationFlowRatePosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationFlowRatePosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationFlowRatePosZ.Name = "txtCalibrationFlowRatePosZ"
        Me.txtCalibrationFlowRatePosZ.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationFlowRatePosZ, resources.GetString("txtCalibrationFlowRatePosZ.ToolTip"))
        '
        'txtCalibrationFlowRatePosY
        '
        resources.ApplyResources(Me.txtCalibrationFlowRatePosY, "txtCalibrationFlowRatePosY")
        Me.txtCalibrationFlowRatePosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationFlowRatePosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationFlowRatePosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationFlowRatePosY.Name = "txtCalibrationFlowRatePosY"
        Me.txtCalibrationFlowRatePosY.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationFlowRatePosY, resources.GetString("txtCalibrationFlowRatePosY.ToolTip"))
        '
        'txtCalibrationFlowRatePosX
        '
        resources.ApplyResources(Me.txtCalibrationFlowRatePosX, "txtCalibrationFlowRatePosX")
        Me.txtCalibrationFlowRatePosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationFlowRatePosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationFlowRatePosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationFlowRatePosX.Name = "txtCalibrationFlowRatePosX"
        Me.txtCalibrationFlowRatePosX.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationFlowRatePosX, resources.GetString("txtCalibrationFlowRatePosX.ToolTip"))
        '
        'btnCalibrationFlowRateGoPos
        '
        resources.ApplyResources(Me.btnCalibrationFlowRateGoPos, "btnCalibrationFlowRateGoPos")
        Me.btnCalibrationFlowRateGoPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnCalibrationFlowRateGoPos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationFlowRateGoPos.Name = "btnCalibrationFlowRateGoPos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationFlowRateGoPos, resources.GetString("btnCalibrationFlowRateGoPos.ToolTip"))
        Me.btnCalibrationFlowRateGoPos.UseVisualStyleBackColor = True
        '
        'btnCalibrationSetFlowRatePos
        '
        resources.ApplyResources(Me.btnCalibrationSetFlowRatePos, "btnCalibrationSetFlowRatePos")
        Me.btnCalibrationSetFlowRatePos.BackColor = System.Drawing.SystemColors.Control
        Me.btnCalibrationSetFlowRatePos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnCalibrationSetFlowRatePos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationSetFlowRatePos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCalibrationSetFlowRatePos.Name = "btnCalibrationSetFlowRatePos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationSetFlowRatePos, resources.GetString("btnCalibrationSetFlowRatePos.ToolTip"))
        Me.btnCalibrationSetFlowRatePos.UseVisualStyleBackColor = True
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
        Me.grpCCDPos.Controls.Add(Me.txtCalibrationFlowRateCCDPosZ)
        Me.grpCCDPos.Controls.Add(Me.txtCalibrationFlowRateCCDPosY)
        Me.grpCCDPos.Controls.Add(Me.txtCalibrationFlowRateCCDPosX)
        Me.grpCCDPos.Controls.Add(Me.btnCalibrationFlowRateGoCCDPos)
        Me.grpCCDPos.Controls.Add(Me.btnCalibrationFlowRateSetCcdPos)
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
        'txtCalibrationFlowRateCCDPosZ
        '
        resources.ApplyResources(Me.txtCalibrationFlowRateCCDPosZ, "txtCalibrationFlowRateCCDPosZ")
        Me.txtCalibrationFlowRateCCDPosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationFlowRateCCDPosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationFlowRateCCDPosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationFlowRateCCDPosZ.Name = "txtCalibrationFlowRateCCDPosZ"
        Me.txtCalibrationFlowRateCCDPosZ.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationFlowRateCCDPosZ, resources.GetString("txtCalibrationFlowRateCCDPosZ.ToolTip"))
        '
        'txtCalibrationFlowRateCCDPosY
        '
        resources.ApplyResources(Me.txtCalibrationFlowRateCCDPosY, "txtCalibrationFlowRateCCDPosY")
        Me.txtCalibrationFlowRateCCDPosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationFlowRateCCDPosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationFlowRateCCDPosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationFlowRateCCDPosY.Name = "txtCalibrationFlowRateCCDPosY"
        Me.txtCalibrationFlowRateCCDPosY.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationFlowRateCCDPosY, resources.GetString("txtCalibrationFlowRateCCDPosY.ToolTip"))
        '
        'txtCalibrationFlowRateCCDPosX
        '
        resources.ApplyResources(Me.txtCalibrationFlowRateCCDPosX, "txtCalibrationFlowRateCCDPosX")
        Me.txtCalibrationFlowRateCCDPosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationFlowRateCCDPosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationFlowRateCCDPosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationFlowRateCCDPosX.Name = "txtCalibrationFlowRateCCDPosX"
        Me.txtCalibrationFlowRateCCDPosX.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationFlowRateCCDPosX, resources.GetString("txtCalibrationFlowRateCCDPosX.ToolTip"))
        '
        'btnCalibrationFlowRateGoCCDPos
        '
        resources.ApplyResources(Me.btnCalibrationFlowRateGoCCDPos, "btnCalibrationFlowRateGoCCDPos")
        Me.btnCalibrationFlowRateGoCCDPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnCalibrationFlowRateGoCCDPos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationFlowRateGoCCDPos.Name = "btnCalibrationFlowRateGoCCDPos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationFlowRateGoCCDPos, resources.GetString("btnCalibrationFlowRateGoCCDPos.ToolTip"))
        Me.btnCalibrationFlowRateGoCCDPos.UseVisualStyleBackColor = True
        '
        'btnCalibrationFlowRateSetCcdPos
        '
        resources.ApplyResources(Me.btnCalibrationFlowRateSetCcdPos, "btnCalibrationFlowRateSetCcdPos")
        Me.btnCalibrationFlowRateSetCcdPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnCalibrationFlowRateSetCcdPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnCalibrationFlowRateSetCcdPos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationFlowRateSetCcdPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCalibrationFlowRateSetCcdPos.Name = "btnCalibrationFlowRateSetCcdPos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationFlowRateSetCcdPos, resources.GetString("btnCalibrationFlowRateSetCcdPos.ToolTip"))
        Me.btnCalibrationFlowRateSetCcdPos.UseVisualStyleBackColor = True
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
        'btnCalibrationFlowRateCancel
        '
        resources.ApplyResources(Me.btnCalibrationFlowRateCancel, "btnCalibrationFlowRateCancel")
        Me.btnCalibrationFlowRateCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnCalibrationFlowRateCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCalibrationFlowRateCancel.Name = "btnCalibrationFlowRateCancel"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationFlowRateCancel, resources.GetString("btnCalibrationFlowRateCancel.ToolTip"))
        Me.btnCalibrationFlowRateCancel.UseVisualStyleBackColor = True
        '
        'btnCalibrationFlowRateOK
        '
        resources.ApplyResources(Me.btnCalibrationFlowRateOK, "btnCalibrationFlowRateOK")
        Me.btnCalibrationFlowRateOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnCalibrationFlowRateOK.Name = "btnCalibrationFlowRateOK"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationFlowRateOK, resources.GetString("btnCalibrationFlowRateOK.ToolTip"))
        Me.btnCalibrationFlowRateOK.UseVisualStyleBackColor = True
        '
        'grpFlowRateAlignPos
        '
        resources.ApplyResources(Me.grpFlowRateAlignPos, "grpFlowRateAlignPos")
        Me.grpFlowRateAlignPos.Controls.Add(Me.txtCalibrationWeighingAlign3PosZ)
        Me.grpFlowRateAlignPos.Controls.Add(Me.txtCalibrationWeighingAlign3PosY)
        Me.grpFlowRateAlignPos.Controls.Add(Me.txtCalibrationWeighingAlign3PosX)
        Me.grpFlowRateAlignPos.Controls.Add(Me.btnCalibrationWeighingGoAlign3Pos)
        Me.grpFlowRateAlignPos.Controls.Add(Me.btnCalibrationWeighingSetAlign3Pos)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighing3PosZUnit)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighing3PosYUnit)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighing3PosXUnit)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighingAlign3PosZ)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighingAlign3PosY)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighingAlign3PosX)
        Me.grpFlowRateAlignPos.Controls.Add(Me.txtCalibrationWeighingAlign2PosZ)
        Me.grpFlowRateAlignPos.Controls.Add(Me.txtCalibrationWeighingAlign2PosY)
        Me.grpFlowRateAlignPos.Controls.Add(Me.txtCalibrationWeighingAlign2PosX)
        Me.grpFlowRateAlignPos.Controls.Add(Me.btnCalibrationWeighingGoAlign2Pos)
        Me.grpFlowRateAlignPos.Controls.Add(Me.btnCalibrationWeighingSetAlign2Pos)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighing2PosZUnit)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighing2PosYUnit)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighing2PosXUnit)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighingAlign2PosZ)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighingAlign2PosY)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighingAlign2PosX)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblSceneSet)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblScene)
        Me.grpFlowRateAlignPos.Controls.Add(Me.btnTrainScene)
        Me.grpFlowRateAlignPos.Controls.Add(Me.btnALign)
        Me.grpFlowRateAlignPos.Controls.Add(Me.txtCalibrationWeighingAlign1PosZ)
        Me.grpFlowRateAlignPos.Controls.Add(Me.txtCalibrationWeighingAlign1PosY)
        Me.grpFlowRateAlignPos.Controls.Add(Me.txtCalibrationWeighingAlign1PosX)
        Me.grpFlowRateAlignPos.Controls.Add(Me.btnCalibrationWeighingGoAlign1Pos)
        Me.grpFlowRateAlignPos.Controls.Add(Me.btnCalibrationWeighingSetAlign1Pos)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighing1PosZUnit)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighing1PosYUnit)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighing1PosXUnit)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighingAlign1PosZ)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighingAlign1PosY)
        Me.grpFlowRateAlignPos.Controls.Add(Me.lblWeighingAlign1PosX)
        Me.grpFlowRateAlignPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpFlowRateAlignPos.Name = "grpFlowRateAlignPos"
        Me.grpFlowRateAlignPos.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpFlowRateAlignPos, resources.GetString("grpFlowRateAlignPos.ToolTip"))
        '
        'txtCalibrationWeighingAlign3PosZ
        '
        resources.ApplyResources(Me.txtCalibrationWeighingAlign3PosZ, "txtCalibrationWeighingAlign3PosZ")
        Me.txtCalibrationWeighingAlign3PosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationWeighingAlign3PosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationWeighingAlign3PosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationWeighingAlign3PosZ.Name = "txtCalibrationWeighingAlign3PosZ"
        Me.txtCalibrationWeighingAlign3PosZ.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationWeighingAlign3PosZ, resources.GetString("txtCalibrationWeighingAlign3PosZ.ToolTip"))
        '
        'txtCalibrationWeighingAlign3PosY
        '
        resources.ApplyResources(Me.txtCalibrationWeighingAlign3PosY, "txtCalibrationWeighingAlign3PosY")
        Me.txtCalibrationWeighingAlign3PosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationWeighingAlign3PosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationWeighingAlign3PosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationWeighingAlign3PosY.Name = "txtCalibrationWeighingAlign3PosY"
        Me.txtCalibrationWeighingAlign3PosY.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationWeighingAlign3PosY, resources.GetString("txtCalibrationWeighingAlign3PosY.ToolTip"))
        '
        'txtCalibrationWeighingAlign3PosX
        '
        resources.ApplyResources(Me.txtCalibrationWeighingAlign3PosX, "txtCalibrationWeighingAlign3PosX")
        Me.txtCalibrationWeighingAlign3PosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationWeighingAlign3PosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationWeighingAlign3PosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationWeighingAlign3PosX.Name = "txtCalibrationWeighingAlign3PosX"
        Me.txtCalibrationWeighingAlign3PosX.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationWeighingAlign3PosX, resources.GetString("txtCalibrationWeighingAlign3PosX.ToolTip"))
        '
        'btnCalibrationWeighingGoAlign3Pos
        '
        resources.ApplyResources(Me.btnCalibrationWeighingGoAlign3Pos, "btnCalibrationWeighingGoAlign3Pos")
        Me.btnCalibrationWeighingGoAlign3Pos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnCalibrationWeighingGoAlign3Pos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationWeighingGoAlign3Pos.Name = "btnCalibrationWeighingGoAlign3Pos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationWeighingGoAlign3Pos, resources.GetString("btnCalibrationWeighingGoAlign3Pos.ToolTip"))
        Me.btnCalibrationWeighingGoAlign3Pos.UseVisualStyleBackColor = True
        '
        'btnCalibrationWeighingSetAlign3Pos
        '
        resources.ApplyResources(Me.btnCalibrationWeighingSetAlign3Pos, "btnCalibrationWeighingSetAlign3Pos")
        Me.btnCalibrationWeighingSetAlign3Pos.BackColor = System.Drawing.SystemColors.Control
        Me.btnCalibrationWeighingSetAlign3Pos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnCalibrationWeighingSetAlign3Pos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationWeighingSetAlign3Pos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCalibrationWeighingSetAlign3Pos.Name = "btnCalibrationWeighingSetAlign3Pos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationWeighingSetAlign3Pos, resources.GetString("btnCalibrationWeighingSetAlign3Pos.ToolTip"))
        Me.btnCalibrationWeighingSetAlign3Pos.UseVisualStyleBackColor = True
        '
        'lblWeighing3PosZUnit
        '
        resources.ApplyResources(Me.lblWeighing3PosZUnit, "lblWeighing3PosZUnit")
        Me.lblWeighing3PosZUnit.Name = "lblWeighing3PosZUnit"
        Me.ToolTip1.SetToolTip(Me.lblWeighing3PosZUnit, resources.GetString("lblWeighing3PosZUnit.ToolTip"))
        '
        'lblWeighing3PosYUnit
        '
        resources.ApplyResources(Me.lblWeighing3PosYUnit, "lblWeighing3PosYUnit")
        Me.lblWeighing3PosYUnit.Name = "lblWeighing3PosYUnit"
        Me.ToolTip1.SetToolTip(Me.lblWeighing3PosYUnit, resources.GetString("lblWeighing3PosYUnit.ToolTip"))
        '
        'lblWeighing3PosXUnit
        '
        resources.ApplyResources(Me.lblWeighing3PosXUnit, "lblWeighing3PosXUnit")
        Me.lblWeighing3PosXUnit.Name = "lblWeighing3PosXUnit"
        Me.ToolTip1.SetToolTip(Me.lblWeighing3PosXUnit, resources.GetString("lblWeighing3PosXUnit.ToolTip"))
        '
        'lblWeighingAlign3PosZ
        '
        resources.ApplyResources(Me.lblWeighingAlign3PosZ, "lblWeighingAlign3PosZ")
        Me.lblWeighingAlign3PosZ.Name = "lblWeighingAlign3PosZ"
        Me.ToolTip1.SetToolTip(Me.lblWeighingAlign3PosZ, resources.GetString("lblWeighingAlign3PosZ.ToolTip"))
        '
        'lblWeighingAlign3PosY
        '
        resources.ApplyResources(Me.lblWeighingAlign3PosY, "lblWeighingAlign3PosY")
        Me.lblWeighingAlign3PosY.Name = "lblWeighingAlign3PosY"
        Me.ToolTip1.SetToolTip(Me.lblWeighingAlign3PosY, resources.GetString("lblWeighingAlign3PosY.ToolTip"))
        '
        'lblWeighingAlign3PosX
        '
        resources.ApplyResources(Me.lblWeighingAlign3PosX, "lblWeighingAlign3PosX")
        Me.lblWeighingAlign3PosX.Name = "lblWeighingAlign3PosX"
        Me.ToolTip1.SetToolTip(Me.lblWeighingAlign3PosX, resources.GetString("lblWeighingAlign3PosX.ToolTip"))
        '
        'txtCalibrationWeighingAlign2PosZ
        '
        resources.ApplyResources(Me.txtCalibrationWeighingAlign2PosZ, "txtCalibrationWeighingAlign2PosZ")
        Me.txtCalibrationWeighingAlign2PosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationWeighingAlign2PosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationWeighingAlign2PosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationWeighingAlign2PosZ.Name = "txtCalibrationWeighingAlign2PosZ"
        Me.txtCalibrationWeighingAlign2PosZ.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationWeighingAlign2PosZ, resources.GetString("txtCalibrationWeighingAlign2PosZ.ToolTip"))
        '
        'txtCalibrationWeighingAlign2PosY
        '
        resources.ApplyResources(Me.txtCalibrationWeighingAlign2PosY, "txtCalibrationWeighingAlign2PosY")
        Me.txtCalibrationWeighingAlign2PosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationWeighingAlign2PosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationWeighingAlign2PosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationWeighingAlign2PosY.Name = "txtCalibrationWeighingAlign2PosY"
        Me.txtCalibrationWeighingAlign2PosY.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationWeighingAlign2PosY, resources.GetString("txtCalibrationWeighingAlign2PosY.ToolTip"))
        '
        'txtCalibrationWeighingAlign2PosX
        '
        resources.ApplyResources(Me.txtCalibrationWeighingAlign2PosX, "txtCalibrationWeighingAlign2PosX")
        Me.txtCalibrationWeighingAlign2PosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationWeighingAlign2PosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationWeighingAlign2PosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationWeighingAlign2PosX.Name = "txtCalibrationWeighingAlign2PosX"
        Me.txtCalibrationWeighingAlign2PosX.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationWeighingAlign2PosX, resources.GetString("txtCalibrationWeighingAlign2PosX.ToolTip"))
        '
        'btnCalibrationWeighingGoAlign2Pos
        '
        resources.ApplyResources(Me.btnCalibrationWeighingGoAlign2Pos, "btnCalibrationWeighingGoAlign2Pos")
        Me.btnCalibrationWeighingGoAlign2Pos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnCalibrationWeighingGoAlign2Pos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationWeighingGoAlign2Pos.Name = "btnCalibrationWeighingGoAlign2Pos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationWeighingGoAlign2Pos, resources.GetString("btnCalibrationWeighingGoAlign2Pos.ToolTip"))
        Me.btnCalibrationWeighingGoAlign2Pos.UseVisualStyleBackColor = True
        '
        'btnCalibrationWeighingSetAlign2Pos
        '
        resources.ApplyResources(Me.btnCalibrationWeighingSetAlign2Pos, "btnCalibrationWeighingSetAlign2Pos")
        Me.btnCalibrationWeighingSetAlign2Pos.BackColor = System.Drawing.SystemColors.Control
        Me.btnCalibrationWeighingSetAlign2Pos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnCalibrationWeighingSetAlign2Pos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationWeighingSetAlign2Pos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCalibrationWeighingSetAlign2Pos.Name = "btnCalibrationWeighingSetAlign2Pos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationWeighingSetAlign2Pos, resources.GetString("btnCalibrationWeighingSetAlign2Pos.ToolTip"))
        Me.btnCalibrationWeighingSetAlign2Pos.UseVisualStyleBackColor = True
        '
        'lblWeighing2PosZUnit
        '
        resources.ApplyResources(Me.lblWeighing2PosZUnit, "lblWeighing2PosZUnit")
        Me.lblWeighing2PosZUnit.Name = "lblWeighing2PosZUnit"
        Me.ToolTip1.SetToolTip(Me.lblWeighing2PosZUnit, resources.GetString("lblWeighing2PosZUnit.ToolTip"))
        '
        'lblWeighing2PosYUnit
        '
        resources.ApplyResources(Me.lblWeighing2PosYUnit, "lblWeighing2PosYUnit")
        Me.lblWeighing2PosYUnit.Name = "lblWeighing2PosYUnit"
        Me.ToolTip1.SetToolTip(Me.lblWeighing2PosYUnit, resources.GetString("lblWeighing2PosYUnit.ToolTip"))
        '
        'lblWeighing2PosXUnit
        '
        resources.ApplyResources(Me.lblWeighing2PosXUnit, "lblWeighing2PosXUnit")
        Me.lblWeighing2PosXUnit.Name = "lblWeighing2PosXUnit"
        Me.ToolTip1.SetToolTip(Me.lblWeighing2PosXUnit, resources.GetString("lblWeighing2PosXUnit.ToolTip"))
        '
        'lblWeighingAlign2PosZ
        '
        resources.ApplyResources(Me.lblWeighingAlign2PosZ, "lblWeighingAlign2PosZ")
        Me.lblWeighingAlign2PosZ.Name = "lblWeighingAlign2PosZ"
        Me.ToolTip1.SetToolTip(Me.lblWeighingAlign2PosZ, resources.GetString("lblWeighingAlign2PosZ.ToolTip"))
        '
        'lblWeighingAlign2PosY
        '
        resources.ApplyResources(Me.lblWeighingAlign2PosY, "lblWeighingAlign2PosY")
        Me.lblWeighingAlign2PosY.Name = "lblWeighingAlign2PosY"
        Me.ToolTip1.SetToolTip(Me.lblWeighingAlign2PosY, resources.GetString("lblWeighingAlign2PosY.ToolTip"))
        '
        'lblWeighingAlign2PosX
        '
        resources.ApplyResources(Me.lblWeighingAlign2PosX, "lblWeighingAlign2PosX")
        Me.lblWeighingAlign2PosX.Name = "lblWeighingAlign2PosX"
        Me.ToolTip1.SetToolTip(Me.lblWeighingAlign2PosX, resources.GetString("lblWeighingAlign2PosX.ToolTip"))
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
        'btnALign
        '
        resources.ApplyResources(Me.btnALign, "btnALign")
        Me.btnALign.FlatAppearance.BorderSize = 0
        Me.btnALign.Name = "btnALign"
        Me.ToolTip1.SetToolTip(Me.btnALign, resources.GetString("btnALign.ToolTip"))
        Me.btnALign.UseVisualStyleBackColor = True
        '
        'txtCalibrationWeighingAlign1PosZ
        '
        resources.ApplyResources(Me.txtCalibrationWeighingAlign1PosZ, "txtCalibrationWeighingAlign1PosZ")
        Me.txtCalibrationWeighingAlign1PosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationWeighingAlign1PosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationWeighingAlign1PosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationWeighingAlign1PosZ.Name = "txtCalibrationWeighingAlign1PosZ"
        Me.txtCalibrationWeighingAlign1PosZ.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationWeighingAlign1PosZ, resources.GetString("txtCalibrationWeighingAlign1PosZ.ToolTip"))
        '
        'txtCalibrationWeighingAlign1PosY
        '
        resources.ApplyResources(Me.txtCalibrationWeighingAlign1PosY, "txtCalibrationWeighingAlign1PosY")
        Me.txtCalibrationWeighingAlign1PosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationWeighingAlign1PosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationWeighingAlign1PosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationWeighingAlign1PosY.Name = "txtCalibrationWeighingAlign1PosY"
        Me.txtCalibrationWeighingAlign1PosY.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationWeighingAlign1PosY, resources.GetString("txtCalibrationWeighingAlign1PosY.ToolTip"))
        '
        'txtCalibrationWeighingAlign1PosX
        '
        resources.ApplyResources(Me.txtCalibrationWeighingAlign1PosX, "txtCalibrationWeighingAlign1PosX")
        Me.txtCalibrationWeighingAlign1PosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCalibrationWeighingAlign1PosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibrationWeighingAlign1PosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCalibrationWeighingAlign1PosX.Name = "txtCalibrationWeighingAlign1PosX"
        Me.txtCalibrationWeighingAlign1PosX.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtCalibrationWeighingAlign1PosX, resources.GetString("txtCalibrationWeighingAlign1PosX.ToolTip"))
        '
        'btnCalibrationWeighingGoAlign1Pos
        '
        resources.ApplyResources(Me.btnCalibrationWeighingGoAlign1Pos, "btnCalibrationWeighingGoAlign1Pos")
        Me.btnCalibrationWeighingGoAlign1Pos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnCalibrationWeighingGoAlign1Pos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationWeighingGoAlign1Pos.Name = "btnCalibrationWeighingGoAlign1Pos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationWeighingGoAlign1Pos, resources.GetString("btnCalibrationWeighingGoAlign1Pos.ToolTip"))
        Me.btnCalibrationWeighingGoAlign1Pos.UseVisualStyleBackColor = True
        '
        'btnCalibrationWeighingSetAlign1Pos
        '
        resources.ApplyResources(Me.btnCalibrationWeighingSetAlign1Pos, "btnCalibrationWeighingSetAlign1Pos")
        Me.btnCalibrationWeighingSetAlign1Pos.BackColor = System.Drawing.SystemColors.Control
        Me.btnCalibrationWeighingSetAlign1Pos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnCalibrationWeighingSetAlign1Pos.FlatAppearance.BorderSize = 0
        Me.btnCalibrationWeighingSetAlign1Pos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCalibrationWeighingSetAlign1Pos.Name = "btnCalibrationWeighingSetAlign1Pos"
        Me.ToolTip1.SetToolTip(Me.btnCalibrationWeighingSetAlign1Pos, resources.GetString("btnCalibrationWeighingSetAlign1Pos.ToolTip"))
        Me.btnCalibrationWeighingSetAlign1Pos.UseVisualStyleBackColor = True
        '
        'lblWeighing1PosZUnit
        '
        resources.ApplyResources(Me.lblWeighing1PosZUnit, "lblWeighing1PosZUnit")
        Me.lblWeighing1PosZUnit.Name = "lblWeighing1PosZUnit"
        Me.ToolTip1.SetToolTip(Me.lblWeighing1PosZUnit, resources.GetString("lblWeighing1PosZUnit.ToolTip"))
        '
        'lblWeighing1PosYUnit
        '
        resources.ApplyResources(Me.lblWeighing1PosYUnit, "lblWeighing1PosYUnit")
        Me.lblWeighing1PosYUnit.Name = "lblWeighing1PosYUnit"
        Me.ToolTip1.SetToolTip(Me.lblWeighing1PosYUnit, resources.GetString("lblWeighing1PosYUnit.ToolTip"))
        '
        'lblWeighing1PosXUnit
        '
        resources.ApplyResources(Me.lblWeighing1PosXUnit, "lblWeighing1PosXUnit")
        Me.lblWeighing1PosXUnit.Name = "lblWeighing1PosXUnit"
        Me.ToolTip1.SetToolTip(Me.lblWeighing1PosXUnit, resources.GetString("lblWeighing1PosXUnit.ToolTip"))
        '
        'lblWeighingAlign1PosZ
        '
        resources.ApplyResources(Me.lblWeighingAlign1PosZ, "lblWeighingAlign1PosZ")
        Me.lblWeighingAlign1PosZ.Name = "lblWeighingAlign1PosZ"
        Me.ToolTip1.SetToolTip(Me.lblWeighingAlign1PosZ, resources.GetString("lblWeighingAlign1PosZ.ToolTip"))
        '
        'lblWeighingAlign1PosY
        '
        resources.ApplyResources(Me.lblWeighingAlign1PosY, "lblWeighingAlign1PosY")
        Me.lblWeighingAlign1PosY.Name = "lblWeighingAlign1PosY"
        Me.ToolTip1.SetToolTip(Me.lblWeighingAlign1PosY, resources.GetString("lblWeighingAlign1PosY.ToolTip"))
        '
        'lblWeighingAlign1PosX
        '
        resources.ApplyResources(Me.lblWeighingAlign1PosX, "lblWeighingAlign1PosX")
        Me.lblWeighingAlign1PosX.Name = "lblWeighingAlign1PosX"
        Me.ToolTip1.SetToolTip(Me.lblWeighingAlign1PosX, resources.GetString("lblWeighingAlign1PosX.ToolTip"))
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
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        Me.ToolTip1.SetToolTip(Me.UcStatusBar1, resources.GetString("UcStatusBar1.ToolTip"))
        '
        'btnFlowRateNext
        '
        resources.ApplyResources(Me.btnFlowRateNext, "btnFlowRateNext")
        Me.btnFlowRateNext.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnFlowRateNext.Name = "btnFlowRateNext"
        Me.ToolTip1.SetToolTip(Me.btnFlowRateNext, resources.GetString("btnFlowRateNext.ToolTip"))
        Me.btnFlowRateNext.UseVisualStyleBackColor = True
        '
        'btnFlowRatePrev
        '
        resources.ApplyResources(Me.btnFlowRatePrev, "btnFlowRatePrev")
        Me.btnFlowRatePrev.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnFlowRatePrev.Name = "btnFlowRatePrev"
        Me.ToolTip1.SetToolTip(Me.btnFlowRatePrev, resources.GetString("btnFlowRatePrev.ToolTip"))
        Me.btnFlowRatePrev.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 300
        '
        'frmCalibrationFlowRate
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnFlowRateNext)
        Me.Controls.Add(Me.btnFlowRatePrev)
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.UcDisplay1)
        Me.Controls.Add(Me.gpTiltMove)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpFlowRateAlignPos)
        Me.Controls.Add(Me.UcLightControl1)
        Me.Controls.Add(Me.btnCalibrationFlowRateCancel)
        Me.Controls.Add(Me.btnCalibrationFlowRateOK)
        Me.Controls.Add(Me.UcJoyStick1)
        Me.Controls.Add(Me.grpHeightSensorPos)
        Me.Controls.Add(Me.grpCCDPos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCalibrationFlowRate"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.grpHeightSensorPos.ResumeLayout(False)
        Me.grpHeightSensorPos.PerformLayout()
        Me.grpCCDPos.ResumeLayout(False)
        Me.grpCCDPos.PerformLayout()
        Me.grpFlowRateAlignPos.ResumeLayout(False)
        Me.grpFlowRateAlignPos.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nmcExposure, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpTiltMove.ResumeLayout(False)
        Me.gpTiltMove.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcLightControl1 As ucLightControl
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents btnCalibrationFlowRateCancel As System.Windows.Forms.Button
    Friend WithEvents btnCalibrationFlowRateOK As System.Windows.Forms.Button
    Friend WithEvents UcJoyStick1 As ucJoyStick
    Friend WithEvents grpHeightSensorPos As System.Windows.Forms.GroupBox
    Friend WithEvents txtCalibrationFlowRatePosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationFlowRatePosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationFlowRatePosX As System.Windows.Forms.TextBox
    Friend WithEvents btnCalibrationFlowRateGoPos As System.Windows.Forms.Button
    Friend WithEvents btnCalibrationSetFlowRatePos As System.Windows.Forms.Button
    Friend WithEvents lblLaserPosZ As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosY As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosX As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosXUnit As System.Windows.Forms.Label
    Friend WithEvents grpCCDPos As System.Windows.Forms.GroupBox
    Friend WithEvents txtCalibrationFlowRateCCDPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationFlowRateCCDPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationFlowRateCCDPosX As System.Windows.Forms.TextBox
    Friend WithEvents btnCalibrationFlowRateGoCCDPos As System.Windows.Forms.Button
    Friend WithEvents btnCalibrationFlowRateSetCcdPos As System.Windows.Forms.Button
    Friend WithEvents lblCCDPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosZ As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosY As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosX As System.Windows.Forms.Label
    Friend WithEvents grpFlowRateAlignPos As System.Windows.Forms.GroupBox
    Friend WithEvents txtCalibrationWeighingAlign3PosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationWeighingAlign3PosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationWeighingAlign3PosX As System.Windows.Forms.TextBox
    Friend WithEvents btnCalibrationWeighingGoAlign3Pos As System.Windows.Forms.Button
    Friend WithEvents btnCalibrationWeighingSetAlign3Pos As System.Windows.Forms.Button
    Friend WithEvents lblWeighing3PosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblWeighing3PosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblWeighing3PosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblWeighingAlign3PosZ As System.Windows.Forms.Label
    Friend WithEvents lblWeighingAlign3PosY As System.Windows.Forms.Label
    Friend WithEvents lblWeighingAlign3PosX As System.Windows.Forms.Label
    Friend WithEvents txtCalibrationWeighingAlign2PosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationWeighingAlign2PosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationWeighingAlign2PosX As System.Windows.Forms.TextBox
    Friend WithEvents btnCalibrationWeighingGoAlign2Pos As System.Windows.Forms.Button
    Friend WithEvents btnCalibrationWeighingSetAlign2Pos As System.Windows.Forms.Button
    Friend WithEvents lblWeighing2PosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblWeighing2PosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblWeighing2PosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblWeighingAlign2PosZ As System.Windows.Forms.Label
    Friend WithEvents lblWeighingAlign2PosY As System.Windows.Forms.Label
    Friend WithEvents lblWeighingAlign2PosX As System.Windows.Forms.Label
    Friend WithEvents lblSceneSet As System.Windows.Forms.Label
    Friend WithEvents lblScene As System.Windows.Forms.Label
    Friend WithEvents btnTrainScene As System.Windows.Forms.Button
    Friend WithEvents btnALign As System.Windows.Forms.Button
    Friend WithEvents txtCalibrationWeighingAlign1PosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationWeighingAlign1PosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrationWeighingAlign1PosX As System.Windows.Forms.TextBox
    Friend WithEvents btnCalibrationWeighingGoAlign1Pos As System.Windows.Forms.Button
    Friend WithEvents btnCalibrationWeighingSetAlign1Pos As System.Windows.Forms.Button
    Friend WithEvents lblWeighing1PosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblWeighing1PosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblWeighing1PosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblWeighingAlign1PosZ As System.Windows.Forms.Label
    Friend WithEvents lblWeighingAlign1PosY As System.Windows.Forms.Label
    Friend WithEvents lblWeighingAlign1PosX As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents nmcExposure As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblExposureTimeUnit As System.Windows.Forms.Label
    Friend WithEvents gpTiltMove As System.Windows.Forms.GroupBox
    Friend WithEvents txtTiltValvePosB As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnGoTilt As System.Windows.Forms.Button
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents btnFlowRateNext As System.Windows.Forms.Button
    Friend WithEvents btnFlowRatePrev As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStageVerificationNew
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStageVerificationNew))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grpArrayPos = New System.Windows.Forms.GroupBox()
        Me.btnGoEndPos = New System.Windows.Forms.Button()
        Me.btnSetEndPos = New System.Windows.Forms.Button()
        Me.btnGoCornerPos = New System.Windows.Forms.Button()
        Me.btnSetCornerPos = New System.Windows.Forms.Button()
        Me.btnGoStartPos = New System.Windows.Forms.Button()
        Me.btnSetStartPos = New System.Windows.Forms.Button()
        Me.lblEndPosZUnit = New System.Windows.Forms.Label()
        Me.nmcEndPosZ = New System.Windows.Forms.NumericUpDown()
        Me.lblEndPosZ = New System.Windows.Forms.Label()
        Me.lblCornerPosZUnit = New System.Windows.Forms.Label()
        Me.nmcCornerPosZ = New System.Windows.Forms.NumericUpDown()
        Me.lblCornerZ = New System.Windows.Forms.Label()
        Me.lblStartPosZUnit = New System.Windows.Forms.Label()
        Me.nmcStartPosZ = New System.Windows.Forms.NumericUpDown()
        Me.lblStartPosZ = New System.Windows.Forms.Label()
        Me.lblCornerY = New System.Windows.Forms.Label()
        Me.nmcCornerPosY = New System.Windows.Forms.NumericUpDown()
        Me.nmcCornerPosX = New System.Windows.Forms.NumericUpDown()
        Me.lblCornerPosYUnit = New System.Windows.Forms.Label()
        Me.lblCornerPosXUnit = New System.Windows.Forms.Label()
        Me.lblCornerX = New System.Windows.Forms.Label()
        Me.nmcEndPosY = New System.Windows.Forms.NumericUpDown()
        Me.nmcEndPosX = New System.Windows.Forms.NumericUpDown()
        Me.nmcStartPosY = New System.Windows.Forms.NumericUpDown()
        Me.nmcStartPosX = New System.Windows.Forms.NumericUpDown()
        Me.lblEndPosX = New System.Windows.Forms.Label()
        Me.lblStartPosX = New System.Windows.Forms.Label()
        Me.lblEndPosYUnit = New System.Windows.Forms.Label()
        Me.lblStartPosYUnit = New System.Windows.Forms.Label()
        Me.lblEndPosXUnit = New System.Windows.Forms.Label()
        Me.lblStartPosXUnit = New System.Windows.Forms.Label()
        Me.lblEndPosY = New System.Windows.Forms.Label()
        Me.lblStartPosY = New System.Windows.Forms.Label()
        Me.txtList = New System.Windows.Forms.TextBox()
        Me.grpGoPos = New System.Windows.Forms.GroupBox()
        Me.lblOffsetYUnit = New System.Windows.Forms.Label()
        Me.lblOffsetXUnit = New System.Windows.Forms.Label()
        Me.btnCalibPos = New System.Windows.Forms.Button()
        Me.lblOffsetY = New System.Windows.Forms.Label()
        Me.lblOffsetX = New System.Windows.Forms.Label()
        Me.lblOdffsetYText = New System.Windows.Forms.Label()
        Me.lblOffsetXText = New System.Windows.Forms.Label()
        Me.cmbYa = New System.Windows.Forms.ComboBox()
        Me.cmbXa = New System.Windows.Forms.ComboBox()
        Me.btnGoCCDAlignPos = New System.Windows.Forms.Button()
        Me.lblYa = New System.Windows.Forms.Label()
        Me.lblXa = New System.Windows.Forms.Label()
        Me.grpAlignmentPos = New System.Windows.Forms.GroupBox()
        Me.lblAction = New System.Windows.Forms.Label()
        Me.btnScene = New System.Windows.Forms.Button()
        Me.btnTrainScene = New System.Windows.Forms.Button()
        Me.lblStageNo = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpSVTRun = New System.Windows.Forms.GroupBox()
        Me.nmcPitchVY = New System.Windows.Forms.NumericUpDown()
        Me.nmcPitchVX = New System.Windows.Forms.NumericUpDown()
        Me.nmcPitchHY = New System.Windows.Forms.NumericUpDown()
        Me.nmcPitchHX = New System.Windows.Forms.NumericUpDown()
        Me.BtnReRun = New System.Windows.Forms.Button()
        Me.BtnVerticalRun = New System.Windows.Forms.Button()
        Me.lblPitchVYUnit = New System.Windows.Forms.Label()
        Me.lblPitchVXUnit = New System.Windows.Forms.Label()
        Me.lblPitchHYUnit = New System.Windows.Forms.Label()
        Me.lblPitchHXUnit = New System.Windows.Forms.Label()
        Me.lblPitchVXText = New System.Windows.Forms.Label()
        Me.lblPitchHXText = New System.Windows.Forms.Label()
        Me.lblPitchVYText = New System.Windows.Forms.Label()
        Me.lblPitchHYText = New System.Windows.Forms.Label()
        Me.lblAngleText = New System.Windows.Forms.Label()
        Me.lblAngle = New System.Windows.Forms.Label()
        Me.BtnHorizontalRun = New System.Windows.Forms.Button()
        Me.nmcArrayYCount = New System.Windows.Forms.NumericUpDown()
        Me.nmcArrayXCount = New System.Windows.Forms.NumericUpDown()
        Me.btnSetPitch = New System.Windows.Forms.Button()
        Me.lblArrayXCount = New System.Windows.Forms.Label()
        Me.lblArrayYCount = New System.Windows.Forms.Label()
        Me.grpArraySetting = New System.Windows.Forms.GroupBox()
        Me.grpPause = New System.Windows.Forms.GroupBox()
        Me.btnPause = New System.Windows.Forms.Button()
        Me.btnVacuum = New System.Windows.Forms.Button()
        Me.grpVacuum = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.NumSteadyTime = New System.Windows.Forms.NumericUpDown()
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.UcLightControl1 = New WindowsApplication1.ucLightControl()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.groupStage4 = New System.Windows.Forms.GroupBox()
        Me.btnStage4Clean = New System.Windows.Forms.Button()
        Me.btnStage4Read = New System.Windows.Forms.Button()
        Me.txtStage4Data = New System.Windows.Forms.TextBox()
        Me.groupStage3 = New System.Windows.Forms.GroupBox()
        Me.btnStage3Clean = New System.Windows.Forms.Button()
        Me.btnStage3Read = New System.Windows.Forms.Button()
        Me.txtStage3Data = New System.Windows.Forms.TextBox()
        Me.groupStage2 = New System.Windows.Forms.GroupBox()
        Me.btnStage2Clean = New System.Windows.Forms.Button()
        Me.btnStage2Read = New System.Windows.Forms.Button()
        Me.txtStage2Data = New System.Windows.Forms.TextBox()
        Me.groupStage1 = New System.Windows.Forms.GroupBox()
        Me.btnStage1Clean = New System.Windows.Forms.Button()
        Me.btnStage1Read = New System.Windows.Forms.Button()
        Me.txtStage1Data = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpArrayPos.SuspendLayout()
        CType(Me.nmcEndPosZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcCornerPosZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcStartPosZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcCornerPosY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcCornerPosX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcEndPosY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcEndPosX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcStartPosY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcStartPosX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGoPos.SuspendLayout()
        Me.grpAlignmentPos.SuspendLayout()
        Me.grpSVTRun.SuspendLayout()
        CType(Me.nmcPitchVY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcPitchVX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcPitchHY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcPitchHX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcArrayYCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcArrayXCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpArraySetting.SuspendLayout()
        Me.grpPause.SuspendLayout()
        Me.grpVacuum.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumSteadyTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupStage4.SuspendLayout()
        Me.groupStage3.SuspendLayout()
        Me.groupStage2.SuspendLayout()
        Me.groupStage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'grpArrayPos
        '
        resources.ApplyResources(Me.grpArrayPos, "grpArrayPos")
        Me.grpArrayPos.Controls.Add(Me.btnGoEndPos)
        Me.grpArrayPos.Controls.Add(Me.btnSetEndPos)
        Me.grpArrayPos.Controls.Add(Me.btnGoCornerPos)
        Me.grpArrayPos.Controls.Add(Me.btnSetCornerPos)
        Me.grpArrayPos.Controls.Add(Me.btnGoStartPos)
        Me.grpArrayPos.Controls.Add(Me.btnSetStartPos)
        Me.grpArrayPos.Controls.Add(Me.lblEndPosZUnit)
        Me.grpArrayPos.Controls.Add(Me.nmcEndPosZ)
        Me.grpArrayPos.Controls.Add(Me.lblEndPosZ)
        Me.grpArrayPos.Controls.Add(Me.lblCornerPosZUnit)
        Me.grpArrayPos.Controls.Add(Me.nmcCornerPosZ)
        Me.grpArrayPos.Controls.Add(Me.lblCornerZ)
        Me.grpArrayPos.Controls.Add(Me.lblStartPosZUnit)
        Me.grpArrayPos.Controls.Add(Me.nmcStartPosZ)
        Me.grpArrayPos.Controls.Add(Me.lblStartPosZ)
        Me.grpArrayPos.Controls.Add(Me.lblCornerY)
        Me.grpArrayPos.Controls.Add(Me.nmcCornerPosY)
        Me.grpArrayPos.Controls.Add(Me.nmcCornerPosX)
        Me.grpArrayPos.Controls.Add(Me.lblCornerPosYUnit)
        Me.grpArrayPos.Controls.Add(Me.lblCornerPosXUnit)
        Me.grpArrayPos.Controls.Add(Me.lblCornerX)
        Me.grpArrayPos.Controls.Add(Me.nmcEndPosY)
        Me.grpArrayPos.Controls.Add(Me.nmcEndPosX)
        Me.grpArrayPos.Controls.Add(Me.nmcStartPosY)
        Me.grpArrayPos.Controls.Add(Me.nmcStartPosX)
        Me.grpArrayPos.Controls.Add(Me.lblEndPosX)
        Me.grpArrayPos.Controls.Add(Me.lblStartPosX)
        Me.grpArrayPos.Controls.Add(Me.lblEndPosYUnit)
        Me.grpArrayPos.Controls.Add(Me.lblStartPosYUnit)
        Me.grpArrayPos.Controls.Add(Me.lblEndPosXUnit)
        Me.grpArrayPos.Controls.Add(Me.lblStartPosXUnit)
        Me.grpArrayPos.Controls.Add(Me.lblEndPosY)
        Me.grpArrayPos.Controls.Add(Me.lblStartPosY)
        Me.grpArrayPos.Name = "grpArrayPos"
        Me.grpArrayPos.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpArrayPos, resources.GetString("grpArrayPos.ToolTip"))
        '
        'btnGoEndPos
        '
        resources.ApplyResources(Me.btnGoEndPos, "btnGoEndPos")
        Me.btnGoEndPos.FlatAppearance.BorderSize = 0
        Me.btnGoEndPos.Name = "btnGoEndPos"
        Me.ToolTip1.SetToolTip(Me.btnGoEndPos, resources.GetString("btnGoEndPos.ToolTip"))
        Me.btnGoEndPos.UseVisualStyleBackColor = True
        '
        'btnSetEndPos
        '
        resources.ApplyResources(Me.btnSetEndPos, "btnSetEndPos")
        Me.btnSetEndPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetEndPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetEndPos.FlatAppearance.BorderSize = 0
        Me.btnSetEndPos.Name = "btnSetEndPos"
        Me.ToolTip1.SetToolTip(Me.btnSetEndPos, resources.GetString("btnSetEndPos.ToolTip"))
        Me.btnSetEndPos.UseVisualStyleBackColor = True
        '
        'btnGoCornerPos
        '
        resources.ApplyResources(Me.btnGoCornerPos, "btnGoCornerPos")
        Me.btnGoCornerPos.FlatAppearance.BorderSize = 0
        Me.btnGoCornerPos.Name = "btnGoCornerPos"
        Me.ToolTip1.SetToolTip(Me.btnGoCornerPos, resources.GetString("btnGoCornerPos.ToolTip"))
        Me.btnGoCornerPos.UseVisualStyleBackColor = True
        '
        'btnSetCornerPos
        '
        resources.ApplyResources(Me.btnSetCornerPos, "btnSetCornerPos")
        Me.btnSetCornerPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetCornerPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetCornerPos.FlatAppearance.BorderSize = 0
        Me.btnSetCornerPos.Name = "btnSetCornerPos"
        Me.ToolTip1.SetToolTip(Me.btnSetCornerPos, resources.GetString("btnSetCornerPos.ToolTip"))
        Me.btnSetCornerPos.UseVisualStyleBackColor = True
        '
        'btnGoStartPos
        '
        resources.ApplyResources(Me.btnGoStartPos, "btnGoStartPos")
        Me.btnGoStartPos.FlatAppearance.BorderSize = 0
        Me.btnGoStartPos.Name = "btnGoStartPos"
        Me.ToolTip1.SetToolTip(Me.btnGoStartPos, resources.GetString("btnGoStartPos.ToolTip"))
        Me.btnGoStartPos.UseVisualStyleBackColor = True
        '
        'btnSetStartPos
        '
        resources.ApplyResources(Me.btnSetStartPos, "btnSetStartPos")
        Me.btnSetStartPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetStartPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetStartPos.FlatAppearance.BorderSize = 0
        Me.btnSetStartPos.Name = "btnSetStartPos"
        Me.ToolTip1.SetToolTip(Me.btnSetStartPos, resources.GetString("btnSetStartPos.ToolTip"))
        Me.btnSetStartPos.UseVisualStyleBackColor = True
        '
        'lblEndPosZUnit
        '
        resources.ApplyResources(Me.lblEndPosZUnit, "lblEndPosZUnit")
        Me.lblEndPosZUnit.Name = "lblEndPosZUnit"
        Me.ToolTip1.SetToolTip(Me.lblEndPosZUnit, resources.GetString("lblEndPosZUnit.ToolTip"))
        '
        'nmcEndPosZ
        '
        resources.ApplyResources(Me.nmcEndPosZ, "nmcEndPosZ")
        Me.nmcEndPosZ.DecimalPlaces = 3
        Me.nmcEndPosZ.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcEndPosZ.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcEndPosZ.Name = "nmcEndPosZ"
        Me.ToolTip1.SetToolTip(Me.nmcEndPosZ, resources.GetString("nmcEndPosZ.ToolTip"))
        '
        'lblEndPosZ
        '
        resources.ApplyResources(Me.lblEndPosZ, "lblEndPosZ")
        Me.lblEndPosZ.Name = "lblEndPosZ"
        Me.ToolTip1.SetToolTip(Me.lblEndPosZ, resources.GetString("lblEndPosZ.ToolTip"))
        '
        'lblCornerPosZUnit
        '
        resources.ApplyResources(Me.lblCornerPosZUnit, "lblCornerPosZUnit")
        Me.lblCornerPosZUnit.Name = "lblCornerPosZUnit"
        Me.ToolTip1.SetToolTip(Me.lblCornerPosZUnit, resources.GetString("lblCornerPosZUnit.ToolTip"))
        '
        'nmcCornerPosZ
        '
        resources.ApplyResources(Me.nmcCornerPosZ, "nmcCornerPosZ")
        Me.nmcCornerPosZ.DecimalPlaces = 3
        Me.nmcCornerPosZ.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcCornerPosZ.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcCornerPosZ.Name = "nmcCornerPosZ"
        Me.ToolTip1.SetToolTip(Me.nmcCornerPosZ, resources.GetString("nmcCornerPosZ.ToolTip"))
        '
        'lblCornerZ
        '
        resources.ApplyResources(Me.lblCornerZ, "lblCornerZ")
        Me.lblCornerZ.Name = "lblCornerZ"
        Me.ToolTip1.SetToolTip(Me.lblCornerZ, resources.GetString("lblCornerZ.ToolTip"))
        '
        'lblStartPosZUnit
        '
        resources.ApplyResources(Me.lblStartPosZUnit, "lblStartPosZUnit")
        Me.lblStartPosZUnit.Name = "lblStartPosZUnit"
        Me.ToolTip1.SetToolTip(Me.lblStartPosZUnit, resources.GetString("lblStartPosZUnit.ToolTip"))
        '
        'nmcStartPosZ
        '
        resources.ApplyResources(Me.nmcStartPosZ, "nmcStartPosZ")
        Me.nmcStartPosZ.DecimalPlaces = 3
        Me.nmcStartPosZ.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcStartPosZ.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcStartPosZ.Name = "nmcStartPosZ"
        Me.ToolTip1.SetToolTip(Me.nmcStartPosZ, resources.GetString("nmcStartPosZ.ToolTip"))
        '
        'lblStartPosZ
        '
        resources.ApplyResources(Me.lblStartPosZ, "lblStartPosZ")
        Me.lblStartPosZ.Name = "lblStartPosZ"
        Me.ToolTip1.SetToolTip(Me.lblStartPosZ, resources.GetString("lblStartPosZ.ToolTip"))
        '
        'lblCornerY
        '
        resources.ApplyResources(Me.lblCornerY, "lblCornerY")
        Me.lblCornerY.Name = "lblCornerY"
        Me.ToolTip1.SetToolTip(Me.lblCornerY, resources.GetString("lblCornerY.ToolTip"))
        '
        'nmcCornerPosY
        '
        resources.ApplyResources(Me.nmcCornerPosY, "nmcCornerPosY")
        Me.nmcCornerPosY.DecimalPlaces = 3
        Me.nmcCornerPosY.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcCornerPosY.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcCornerPosY.Name = "nmcCornerPosY"
        Me.ToolTip1.SetToolTip(Me.nmcCornerPosY, resources.GetString("nmcCornerPosY.ToolTip"))
        '
        'nmcCornerPosX
        '
        resources.ApplyResources(Me.nmcCornerPosX, "nmcCornerPosX")
        Me.nmcCornerPosX.DecimalPlaces = 3
        Me.nmcCornerPosX.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcCornerPosX.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcCornerPosX.Name = "nmcCornerPosX"
        Me.ToolTip1.SetToolTip(Me.nmcCornerPosX, resources.GetString("nmcCornerPosX.ToolTip"))
        '
        'lblCornerPosYUnit
        '
        resources.ApplyResources(Me.lblCornerPosYUnit, "lblCornerPosYUnit")
        Me.lblCornerPosYUnit.Name = "lblCornerPosYUnit"
        Me.ToolTip1.SetToolTip(Me.lblCornerPosYUnit, resources.GetString("lblCornerPosYUnit.ToolTip"))
        '
        'lblCornerPosXUnit
        '
        resources.ApplyResources(Me.lblCornerPosXUnit, "lblCornerPosXUnit")
        Me.lblCornerPosXUnit.Name = "lblCornerPosXUnit"
        Me.ToolTip1.SetToolTip(Me.lblCornerPosXUnit, resources.GetString("lblCornerPosXUnit.ToolTip"))
        '
        'lblCornerX
        '
        resources.ApplyResources(Me.lblCornerX, "lblCornerX")
        Me.lblCornerX.Name = "lblCornerX"
        Me.ToolTip1.SetToolTip(Me.lblCornerX, resources.GetString("lblCornerX.ToolTip"))
        '
        'nmcEndPosY
        '
        resources.ApplyResources(Me.nmcEndPosY, "nmcEndPosY")
        Me.nmcEndPosY.DecimalPlaces = 3
        Me.nmcEndPosY.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcEndPosY.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcEndPosY.Name = "nmcEndPosY"
        Me.ToolTip1.SetToolTip(Me.nmcEndPosY, resources.GetString("nmcEndPosY.ToolTip"))
        '
        'nmcEndPosX
        '
        resources.ApplyResources(Me.nmcEndPosX, "nmcEndPosX")
        Me.nmcEndPosX.DecimalPlaces = 3
        Me.nmcEndPosX.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcEndPosX.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcEndPosX.Name = "nmcEndPosX"
        Me.ToolTip1.SetToolTip(Me.nmcEndPosX, resources.GetString("nmcEndPosX.ToolTip"))
        '
        'nmcStartPosY
        '
        resources.ApplyResources(Me.nmcStartPosY, "nmcStartPosY")
        Me.nmcStartPosY.DecimalPlaces = 3
        Me.nmcStartPosY.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcStartPosY.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcStartPosY.Name = "nmcStartPosY"
        Me.ToolTip1.SetToolTip(Me.nmcStartPosY, resources.GetString("nmcStartPosY.ToolTip"))
        '
        'nmcStartPosX
        '
        resources.ApplyResources(Me.nmcStartPosX, "nmcStartPosX")
        Me.nmcStartPosX.DecimalPlaces = 3
        Me.nmcStartPosX.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcStartPosX.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcStartPosX.Name = "nmcStartPosX"
        Me.ToolTip1.SetToolTip(Me.nmcStartPosX, resources.GetString("nmcStartPosX.ToolTip"))
        '
        'lblEndPosX
        '
        resources.ApplyResources(Me.lblEndPosX, "lblEndPosX")
        Me.lblEndPosX.Name = "lblEndPosX"
        Me.ToolTip1.SetToolTip(Me.lblEndPosX, resources.GetString("lblEndPosX.ToolTip"))
        '
        'lblStartPosX
        '
        resources.ApplyResources(Me.lblStartPosX, "lblStartPosX")
        Me.lblStartPosX.Name = "lblStartPosX"
        Me.ToolTip1.SetToolTip(Me.lblStartPosX, resources.GetString("lblStartPosX.ToolTip"))
        '
        'lblEndPosYUnit
        '
        resources.ApplyResources(Me.lblEndPosYUnit, "lblEndPosYUnit")
        Me.lblEndPosYUnit.Name = "lblEndPosYUnit"
        Me.ToolTip1.SetToolTip(Me.lblEndPosYUnit, resources.GetString("lblEndPosYUnit.ToolTip"))
        '
        'lblStartPosYUnit
        '
        resources.ApplyResources(Me.lblStartPosYUnit, "lblStartPosYUnit")
        Me.lblStartPosYUnit.Name = "lblStartPosYUnit"
        Me.ToolTip1.SetToolTip(Me.lblStartPosYUnit, resources.GetString("lblStartPosYUnit.ToolTip"))
        '
        'lblEndPosXUnit
        '
        resources.ApplyResources(Me.lblEndPosXUnit, "lblEndPosXUnit")
        Me.lblEndPosXUnit.Name = "lblEndPosXUnit"
        Me.ToolTip1.SetToolTip(Me.lblEndPosXUnit, resources.GetString("lblEndPosXUnit.ToolTip"))
        '
        'lblStartPosXUnit
        '
        resources.ApplyResources(Me.lblStartPosXUnit, "lblStartPosXUnit")
        Me.lblStartPosXUnit.Name = "lblStartPosXUnit"
        Me.ToolTip1.SetToolTip(Me.lblStartPosXUnit, resources.GetString("lblStartPosXUnit.ToolTip"))
        '
        'lblEndPosY
        '
        resources.ApplyResources(Me.lblEndPosY, "lblEndPosY")
        Me.lblEndPosY.Name = "lblEndPosY"
        Me.ToolTip1.SetToolTip(Me.lblEndPosY, resources.GetString("lblEndPosY.ToolTip"))
        '
        'lblStartPosY
        '
        resources.ApplyResources(Me.lblStartPosY, "lblStartPosY")
        Me.lblStartPosY.Name = "lblStartPosY"
        Me.ToolTip1.SetToolTip(Me.lblStartPosY, resources.GetString("lblStartPosY.ToolTip"))
        '
        'txtList
        '
        resources.ApplyResources(Me.txtList, "txtList")
        Me.txtList.Name = "txtList"
        Me.ToolTip1.SetToolTip(Me.txtList, resources.GetString("txtList.ToolTip"))
        '
        'grpGoPos
        '
        resources.ApplyResources(Me.grpGoPos, "grpGoPos")
        Me.grpGoPos.Controls.Add(Me.lblOffsetYUnit)
        Me.grpGoPos.Controls.Add(Me.lblOffsetXUnit)
        Me.grpGoPos.Controls.Add(Me.btnCalibPos)
        Me.grpGoPos.Controls.Add(Me.lblOffsetY)
        Me.grpGoPos.Controls.Add(Me.lblOffsetX)
        Me.grpGoPos.Controls.Add(Me.lblOdffsetYText)
        Me.grpGoPos.Controls.Add(Me.lblOffsetXText)
        Me.grpGoPos.Controls.Add(Me.cmbYa)
        Me.grpGoPos.Controls.Add(Me.cmbXa)
        Me.grpGoPos.Controls.Add(Me.btnGoCCDAlignPos)
        Me.grpGoPos.Controls.Add(Me.lblYa)
        Me.grpGoPos.Controls.Add(Me.lblXa)
        Me.grpGoPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpGoPos.Name = "grpGoPos"
        Me.grpGoPos.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpGoPos, resources.GetString("grpGoPos.ToolTip"))
        '
        'lblOffsetYUnit
        '
        resources.ApplyResources(Me.lblOffsetYUnit, "lblOffsetYUnit")
        Me.lblOffsetYUnit.Name = "lblOffsetYUnit"
        Me.ToolTip1.SetToolTip(Me.lblOffsetYUnit, resources.GetString("lblOffsetYUnit.ToolTip"))
        '
        'lblOffsetXUnit
        '
        resources.ApplyResources(Me.lblOffsetXUnit, "lblOffsetXUnit")
        Me.lblOffsetXUnit.Name = "lblOffsetXUnit"
        Me.ToolTip1.SetToolTip(Me.lblOffsetXUnit, resources.GetString("lblOffsetXUnit.ToolTip"))
        '
        'btnCalibPos
        '
        resources.ApplyResources(Me.btnCalibPos, "btnCalibPos")
        Me.btnCalibPos.FlatAppearance.BorderSize = 0
        Me.btnCalibPos.Name = "btnCalibPos"
        Me.ToolTip1.SetToolTip(Me.btnCalibPos, resources.GetString("btnCalibPos.ToolTip"))
        Me.btnCalibPos.UseVisualStyleBackColor = True
        '
        'lblOffsetY
        '
        resources.ApplyResources(Me.lblOffsetY, "lblOffsetY")
        Me.lblOffsetY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOffsetY.Name = "lblOffsetY"
        Me.ToolTip1.SetToolTip(Me.lblOffsetY, resources.GetString("lblOffsetY.ToolTip"))
        '
        'lblOffsetX
        '
        resources.ApplyResources(Me.lblOffsetX, "lblOffsetX")
        Me.lblOffsetX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOffsetX.Name = "lblOffsetX"
        Me.ToolTip1.SetToolTip(Me.lblOffsetX, resources.GetString("lblOffsetX.ToolTip"))
        '
        'lblOdffsetYText
        '
        resources.ApplyResources(Me.lblOdffsetYText, "lblOdffsetYText")
        Me.lblOdffsetYText.Name = "lblOdffsetYText"
        Me.ToolTip1.SetToolTip(Me.lblOdffsetYText, resources.GetString("lblOdffsetYText.ToolTip"))
        '
        'lblOffsetXText
        '
        resources.ApplyResources(Me.lblOffsetXText, "lblOffsetXText")
        Me.lblOffsetXText.Name = "lblOffsetXText"
        Me.ToolTip1.SetToolTip(Me.lblOffsetXText, resources.GetString("lblOffsetXText.ToolTip"))
        '
        'cmbYa
        '
        resources.ApplyResources(Me.cmbYa, "cmbYa")
        Me.cmbYa.FormattingEnabled = True
        Me.cmbYa.Name = "cmbYa"
        Me.ToolTip1.SetToolTip(Me.cmbYa, resources.GetString("cmbYa.ToolTip"))
        '
        'cmbXa
        '
        resources.ApplyResources(Me.cmbXa, "cmbXa")
        Me.cmbXa.FormattingEnabled = True
        Me.cmbXa.Name = "cmbXa"
        Me.ToolTip1.SetToolTip(Me.cmbXa, resources.GetString("cmbXa.ToolTip"))
        '
        'btnGoCCDAlignPos
        '
        resources.ApplyResources(Me.btnGoCCDAlignPos, "btnGoCCDAlignPos")
        Me.btnGoCCDAlignPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.FIDs
        Me.btnGoCCDAlignPos.FlatAppearance.BorderSize = 0
        Me.btnGoCCDAlignPos.Name = "btnGoCCDAlignPos"
        Me.ToolTip1.SetToolTip(Me.btnGoCCDAlignPos, resources.GetString("btnGoCCDAlignPos.ToolTip"))
        Me.btnGoCCDAlignPos.UseVisualStyleBackColor = True
        '
        'lblYa
        '
        resources.ApplyResources(Me.lblYa, "lblYa")
        Me.lblYa.Name = "lblYa"
        Me.ToolTip1.SetToolTip(Me.lblYa, resources.GetString("lblYa.ToolTip"))
        '
        'lblXa
        '
        resources.ApplyResources(Me.lblXa, "lblXa")
        Me.lblXa.Name = "lblXa"
        Me.ToolTip1.SetToolTip(Me.lblXa, resources.GetString("lblXa.ToolTip"))
        '
        'grpAlignmentPos
        '
        resources.ApplyResources(Me.grpAlignmentPos, "grpAlignmentPos")
        Me.grpAlignmentPos.Controls.Add(Me.lblAction)
        Me.grpAlignmentPos.Controls.Add(Me.btnScene)
        Me.grpAlignmentPos.Controls.Add(Me.btnTrainScene)
        Me.grpAlignmentPos.Controls.Add(Me.lblStageNo)
        Me.grpAlignmentPos.Controls.Add(Me.Label3)
        Me.grpAlignmentPos.Controls.Add(Me.Label2)
        Me.grpAlignmentPos.Name = "grpAlignmentPos"
        Me.grpAlignmentPos.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpAlignmentPos, resources.GetString("grpAlignmentPos.ToolTip"))
        '
        'lblAction
        '
        resources.ApplyResources(Me.lblAction, "lblAction")
        Me.lblAction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAction.Name = "lblAction"
        Me.ToolTip1.SetToolTip(Me.lblAction, resources.GetString("lblAction.ToolTip"))
        '
        'btnScene
        '
        resources.ApplyResources(Me.btnScene, "btnScene")
        Me.btnScene.Name = "btnScene"
        Me.ToolTip1.SetToolTip(Me.btnScene, resources.GetString("btnScene.ToolTip"))
        Me.btnScene.UseVisualStyleBackColor = True
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
        'lblStageNo
        '
        resources.ApplyResources(Me.lblStageNo, "lblStageNo")
        Me.lblStageNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStageNo.Name = "lblStageNo"
        Me.ToolTip1.SetToolTip(Me.lblStageNo, resources.GetString("lblStageNo.ToolTip"))
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        Me.ToolTip1.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip"))
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        Me.ToolTip1.SetToolTip(Me.Label2, resources.GetString("Label2.ToolTip"))
        '
        'grpSVTRun
        '
        resources.ApplyResources(Me.grpSVTRun, "grpSVTRun")
        Me.grpSVTRun.Controls.Add(Me.nmcPitchVY)
        Me.grpSVTRun.Controls.Add(Me.nmcPitchVX)
        Me.grpSVTRun.Controls.Add(Me.nmcPitchHY)
        Me.grpSVTRun.Controls.Add(Me.nmcPitchHX)
        Me.grpSVTRun.Controls.Add(Me.BtnReRun)
        Me.grpSVTRun.Controls.Add(Me.BtnVerticalRun)
        Me.grpSVTRun.Controls.Add(Me.lblPitchVYUnit)
        Me.grpSVTRun.Controls.Add(Me.lblPitchVXUnit)
        Me.grpSVTRun.Controls.Add(Me.lblPitchHYUnit)
        Me.grpSVTRun.Controls.Add(Me.lblPitchHXUnit)
        Me.grpSVTRun.Controls.Add(Me.lblPitchVXText)
        Me.grpSVTRun.Controls.Add(Me.lblPitchHXText)
        Me.grpSVTRun.Controls.Add(Me.lblPitchVYText)
        Me.grpSVTRun.Controls.Add(Me.lblPitchHYText)
        Me.grpSVTRun.Controls.Add(Me.lblAngleText)
        Me.grpSVTRun.Controls.Add(Me.lblAngle)
        Me.grpSVTRun.Controls.Add(Me.BtnHorizontalRun)
        Me.grpSVTRun.Name = "grpSVTRun"
        Me.grpSVTRun.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpSVTRun, resources.GetString("grpSVTRun.ToolTip"))
        '
        'nmcPitchVY
        '
        resources.ApplyResources(Me.nmcPitchVY, "nmcPitchVY")
        Me.nmcPitchVY.DecimalPlaces = 3
        Me.nmcPitchVY.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcPitchVY.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcPitchVY.Name = "nmcPitchVY"
        Me.ToolTip1.SetToolTip(Me.nmcPitchVY, resources.GetString("nmcPitchVY.ToolTip"))
        '
        'nmcPitchVX
        '
        resources.ApplyResources(Me.nmcPitchVX, "nmcPitchVX")
        Me.nmcPitchVX.DecimalPlaces = 3
        Me.nmcPitchVX.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcPitchVX.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcPitchVX.Name = "nmcPitchVX"
        Me.ToolTip1.SetToolTip(Me.nmcPitchVX, resources.GetString("nmcPitchVX.ToolTip"))
        '
        'nmcPitchHY
        '
        resources.ApplyResources(Me.nmcPitchHY, "nmcPitchHY")
        Me.nmcPitchHY.DecimalPlaces = 3
        Me.nmcPitchHY.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcPitchHY.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcPitchHY.Name = "nmcPitchHY"
        Me.ToolTip1.SetToolTip(Me.nmcPitchHY, resources.GetString("nmcPitchHY.ToolTip"))
        '
        'nmcPitchHX
        '
        resources.ApplyResources(Me.nmcPitchHX, "nmcPitchHX")
        Me.nmcPitchHX.DecimalPlaces = 3
        Me.nmcPitchHX.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcPitchHX.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcPitchHX.Name = "nmcPitchHX"
        Me.ToolTip1.SetToolTip(Me.nmcPitchHX, resources.GetString("nmcPitchHX.ToolTip"))
        '
        'BtnReRun
        '
        resources.ApplyResources(Me.BtnReRun, "BtnReRun")
        Me.BtnReRun.BackColor = System.Drawing.SystemColors.Control
        Me.BtnReRun.FlatAppearance.BorderSize = 0
        Me.BtnReRun.Name = "BtnReRun"
        Me.ToolTip1.SetToolTip(Me.BtnReRun, resources.GetString("BtnReRun.ToolTip"))
        Me.BtnReRun.UseVisualStyleBackColor = True
        '
        'BtnVerticalRun
        '
        resources.ApplyResources(Me.BtnVerticalRun, "BtnVerticalRun")
        Me.BtnVerticalRun.BackColor = System.Drawing.SystemColors.Control
        Me.BtnVerticalRun.FlatAppearance.BorderSize = 0
        Me.BtnVerticalRun.Name = "BtnVerticalRun"
        Me.ToolTip1.SetToolTip(Me.BtnVerticalRun, resources.GetString("BtnVerticalRun.ToolTip"))
        Me.BtnVerticalRun.UseVisualStyleBackColor = True
        '
        'lblPitchVYUnit
        '
        resources.ApplyResources(Me.lblPitchVYUnit, "lblPitchVYUnit")
        Me.lblPitchVYUnit.Name = "lblPitchVYUnit"
        Me.ToolTip1.SetToolTip(Me.lblPitchVYUnit, resources.GetString("lblPitchVYUnit.ToolTip"))
        '
        'lblPitchVXUnit
        '
        resources.ApplyResources(Me.lblPitchVXUnit, "lblPitchVXUnit")
        Me.lblPitchVXUnit.Name = "lblPitchVXUnit"
        Me.ToolTip1.SetToolTip(Me.lblPitchVXUnit, resources.GetString("lblPitchVXUnit.ToolTip"))
        '
        'lblPitchHYUnit
        '
        resources.ApplyResources(Me.lblPitchHYUnit, "lblPitchHYUnit")
        Me.lblPitchHYUnit.Name = "lblPitchHYUnit"
        Me.ToolTip1.SetToolTip(Me.lblPitchHYUnit, resources.GetString("lblPitchHYUnit.ToolTip"))
        '
        'lblPitchHXUnit
        '
        resources.ApplyResources(Me.lblPitchHXUnit, "lblPitchHXUnit")
        Me.lblPitchHXUnit.Name = "lblPitchHXUnit"
        Me.ToolTip1.SetToolTip(Me.lblPitchHXUnit, resources.GetString("lblPitchHXUnit.ToolTip"))
        '
        'lblPitchVXText
        '
        resources.ApplyResources(Me.lblPitchVXText, "lblPitchVXText")
        Me.lblPitchVXText.Name = "lblPitchVXText"
        Me.ToolTip1.SetToolTip(Me.lblPitchVXText, resources.GetString("lblPitchVXText.ToolTip"))
        '
        'lblPitchHXText
        '
        resources.ApplyResources(Me.lblPitchHXText, "lblPitchHXText")
        Me.lblPitchHXText.Name = "lblPitchHXText"
        Me.ToolTip1.SetToolTip(Me.lblPitchHXText, resources.GetString("lblPitchHXText.ToolTip"))
        '
        'lblPitchVYText
        '
        resources.ApplyResources(Me.lblPitchVYText, "lblPitchVYText")
        Me.lblPitchVYText.Name = "lblPitchVYText"
        Me.ToolTip1.SetToolTip(Me.lblPitchVYText, resources.GetString("lblPitchVYText.ToolTip"))
        '
        'lblPitchHYText
        '
        resources.ApplyResources(Me.lblPitchHYText, "lblPitchHYText")
        Me.lblPitchHYText.Name = "lblPitchHYText"
        Me.ToolTip1.SetToolTip(Me.lblPitchHYText, resources.GetString("lblPitchHYText.ToolTip"))
        '
        'lblAngleText
        '
        resources.ApplyResources(Me.lblAngleText, "lblAngleText")
        Me.lblAngleText.Name = "lblAngleText"
        Me.ToolTip1.SetToolTip(Me.lblAngleText, resources.GetString("lblAngleText.ToolTip"))
        '
        'lblAngle
        '
        resources.ApplyResources(Me.lblAngle, "lblAngle")
        Me.lblAngle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAngle.Name = "lblAngle"
        Me.ToolTip1.SetToolTip(Me.lblAngle, resources.GetString("lblAngle.ToolTip"))
        '
        'BtnHorizontalRun
        '
        resources.ApplyResources(Me.BtnHorizontalRun, "BtnHorizontalRun")
        Me.BtnHorizontalRun.BackColor = System.Drawing.SystemColors.Control
        Me.BtnHorizontalRun.FlatAppearance.BorderSize = 0
        Me.BtnHorizontalRun.Name = "BtnHorizontalRun"
        Me.ToolTip1.SetToolTip(Me.BtnHorizontalRun, resources.GetString("BtnHorizontalRun.ToolTip"))
        Me.BtnHorizontalRun.UseVisualStyleBackColor = True
        '
        'nmcArrayYCount
        '
        resources.ApplyResources(Me.nmcArrayYCount, "nmcArrayYCount")
        Me.nmcArrayYCount.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcArrayYCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcArrayYCount.Name = "nmcArrayYCount"
        Me.ToolTip1.SetToolTip(Me.nmcArrayYCount, resources.GetString("nmcArrayYCount.ToolTip"))
        Me.nmcArrayYCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmcArrayXCount
        '
        resources.ApplyResources(Me.nmcArrayXCount, "nmcArrayXCount")
        Me.nmcArrayXCount.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcArrayXCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcArrayXCount.Name = "nmcArrayXCount"
        Me.ToolTip1.SetToolTip(Me.nmcArrayXCount, resources.GetString("nmcArrayXCount.ToolTip"))
        Me.nmcArrayXCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'btnSetPitch
        '
        resources.ApplyResources(Me.btnSetPitch, "btnSetPitch")
        Me.btnSetPitch.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetPitch.FlatAppearance.BorderSize = 0
        Me.btnSetPitch.Name = "btnSetPitch"
        Me.ToolTip1.SetToolTip(Me.btnSetPitch, resources.GetString("btnSetPitch.ToolTip"))
        Me.btnSetPitch.UseVisualStyleBackColor = True
        '
        'lblArrayXCount
        '
        resources.ApplyResources(Me.lblArrayXCount, "lblArrayXCount")
        Me.lblArrayXCount.Name = "lblArrayXCount"
        Me.ToolTip1.SetToolTip(Me.lblArrayXCount, resources.GetString("lblArrayXCount.ToolTip"))
        '
        'lblArrayYCount
        '
        resources.ApplyResources(Me.lblArrayYCount, "lblArrayYCount")
        Me.lblArrayYCount.Name = "lblArrayYCount"
        Me.ToolTip1.SetToolTip(Me.lblArrayYCount, resources.GetString("lblArrayYCount.ToolTip"))
        '
        'grpArraySetting
        '
        resources.ApplyResources(Me.grpArraySetting, "grpArraySetting")
        Me.grpArraySetting.Controls.Add(Me.lblArrayYCount)
        Me.grpArraySetting.Controls.Add(Me.lblArrayXCount)
        Me.grpArraySetting.Controls.Add(Me.btnSetPitch)
        Me.grpArraySetting.Controls.Add(Me.nmcArrayXCount)
        Me.grpArraySetting.Controls.Add(Me.nmcArrayYCount)
        Me.grpArraySetting.Name = "grpArraySetting"
        Me.grpArraySetting.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpArraySetting, resources.GetString("grpArraySetting.ToolTip"))
        '
        'grpPause
        '
        resources.ApplyResources(Me.grpPause, "grpPause")
        Me.grpPause.Controls.Add(Me.btnPause)
        Me.grpPause.Name = "grpPause"
        Me.grpPause.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpPause, resources.GetString("grpPause.ToolTip"))
        '
        'btnPause
        '
        resources.ApplyResources(Me.btnPause, "btnPause")
        Me.btnPause.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Pause1
        Me.btnPause.FlatAppearance.BorderSize = 0
        Me.btnPause.Name = "btnPause"
        Me.ToolTip1.SetToolTip(Me.btnPause, resources.GetString("btnPause.ToolTip"))
        Me.btnPause.UseVisualStyleBackColor = True
        '
        'btnVacuum
        '
        resources.ApplyResources(Me.btnVacuum, "btnVacuum")
        Me.btnVacuum.BackColor = System.Drawing.SystemColors.Control
        Me.btnVacuum.FlatAppearance.BorderSize = 0
        Me.btnVacuum.Name = "btnVacuum"
        Me.ToolTip1.SetToolTip(Me.btnVacuum, resources.GetString("btnVacuum.ToolTip"))
        Me.btnVacuum.UseVisualStyleBackColor = True
        '
        'grpVacuum
        '
        resources.ApplyResources(Me.grpVacuum, "grpVacuum")
        Me.grpVacuum.Controls.Add(Me.btnVacuum)
        Me.grpVacuum.Name = "grpVacuum"
        Me.grpVacuum.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpVacuum, resources.GetString("grpVacuum.ToolTip"))
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.NumSteadyTime)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        Me.ToolTip1.SetToolTip(Me.Label4, resources.GetString("Label4.ToolTip"))
        '
        'NumSteadyTime
        '
        resources.ApplyResources(Me.NumSteadyTime, "NumSteadyTime")
        Me.NumSteadyTime.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.NumSteadyTime.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.NumSteadyTime.Name = "NumSteadyTime"
        Me.ToolTip1.SetToolTip(Me.NumSteadyTime, resources.GetString("NumSteadyTime.ToolTip"))
        Me.NumSteadyTime.Value = New Decimal(New Integer() {100, 0, 0, 0})
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
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
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
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        Me.ToolTip1.SetToolTip(Me.UcStatusBar1, resources.GetString("UcStatusBar1.ToolTip"))
        '
        'groupStage4
        '
        resources.ApplyResources(Me.groupStage4, "groupStage4")
        Me.groupStage4.Controls.Add(Me.btnStage4Clean)
        Me.groupStage4.Controls.Add(Me.btnStage4Read)
        Me.groupStage4.Controls.Add(Me.txtStage4Data)
        Me.groupStage4.Name = "groupStage4"
        Me.groupStage4.TabStop = False
        Me.ToolTip1.SetToolTip(Me.groupStage4, resources.GetString("groupStage4.ToolTip"))
        '
        'btnStage4Clean
        '
        resources.ApplyResources(Me.btnStage4Clean, "btnStage4Clean")
        Me.btnStage4Clean.Name = "btnStage4Clean"
        Me.ToolTip1.SetToolTip(Me.btnStage4Clean, resources.GetString("btnStage4Clean.ToolTip"))
        Me.btnStage4Clean.UseVisualStyleBackColor = True
        '
        'btnStage4Read
        '
        resources.ApplyResources(Me.btnStage4Read, "btnStage4Read")
        Me.btnStage4Read.Name = "btnStage4Read"
        Me.ToolTip1.SetToolTip(Me.btnStage4Read, resources.GetString("btnStage4Read.ToolTip"))
        Me.btnStage4Read.UseVisualStyleBackColor = True
        '
        'txtStage4Data
        '
        resources.ApplyResources(Me.txtStage4Data, "txtStage4Data")
        Me.txtStage4Data.Name = "txtStage4Data"
        Me.ToolTip1.SetToolTip(Me.txtStage4Data, resources.GetString("txtStage4Data.ToolTip"))
        '
        'groupStage3
        '
        resources.ApplyResources(Me.groupStage3, "groupStage3")
        Me.groupStage3.Controls.Add(Me.btnStage3Clean)
        Me.groupStage3.Controls.Add(Me.btnStage3Read)
        Me.groupStage3.Controls.Add(Me.txtStage3Data)
        Me.groupStage3.Name = "groupStage3"
        Me.groupStage3.TabStop = False
        Me.ToolTip1.SetToolTip(Me.groupStage3, resources.GetString("groupStage3.ToolTip"))
        '
        'btnStage3Clean
        '
        resources.ApplyResources(Me.btnStage3Clean, "btnStage3Clean")
        Me.btnStage3Clean.Name = "btnStage3Clean"
        Me.ToolTip1.SetToolTip(Me.btnStage3Clean, resources.GetString("btnStage3Clean.ToolTip"))
        Me.btnStage3Clean.UseVisualStyleBackColor = True
        '
        'btnStage3Read
        '
        resources.ApplyResources(Me.btnStage3Read, "btnStage3Read")
        Me.btnStage3Read.Name = "btnStage3Read"
        Me.ToolTip1.SetToolTip(Me.btnStage3Read, resources.GetString("btnStage3Read.ToolTip"))
        Me.btnStage3Read.UseVisualStyleBackColor = True
        '
        'txtStage3Data
        '
        resources.ApplyResources(Me.txtStage3Data, "txtStage3Data")
        Me.txtStage3Data.Name = "txtStage3Data"
        Me.ToolTip1.SetToolTip(Me.txtStage3Data, resources.GetString("txtStage3Data.ToolTip"))
        '
        'groupStage2
        '
        resources.ApplyResources(Me.groupStage2, "groupStage2")
        Me.groupStage2.Controls.Add(Me.btnStage2Clean)
        Me.groupStage2.Controls.Add(Me.btnStage2Read)
        Me.groupStage2.Controls.Add(Me.txtStage2Data)
        Me.groupStage2.Name = "groupStage2"
        Me.groupStage2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.groupStage2, resources.GetString("groupStage2.ToolTip"))
        '
        'btnStage2Clean
        '
        resources.ApplyResources(Me.btnStage2Clean, "btnStage2Clean")
        Me.btnStage2Clean.Name = "btnStage2Clean"
        Me.ToolTip1.SetToolTip(Me.btnStage2Clean, resources.GetString("btnStage2Clean.ToolTip"))
        Me.btnStage2Clean.UseVisualStyleBackColor = True
        '
        'btnStage2Read
        '
        resources.ApplyResources(Me.btnStage2Read, "btnStage2Read")
        Me.btnStage2Read.Name = "btnStage2Read"
        Me.ToolTip1.SetToolTip(Me.btnStage2Read, resources.GetString("btnStage2Read.ToolTip"))
        Me.btnStage2Read.UseVisualStyleBackColor = True
        '
        'txtStage2Data
        '
        resources.ApplyResources(Me.txtStage2Data, "txtStage2Data")
        Me.txtStage2Data.Name = "txtStage2Data"
        Me.ToolTip1.SetToolTip(Me.txtStage2Data, resources.GetString("txtStage2Data.ToolTip"))
        '
        'groupStage1
        '
        resources.ApplyResources(Me.groupStage1, "groupStage1")
        Me.groupStage1.Controls.Add(Me.btnStage1Clean)
        Me.groupStage1.Controls.Add(Me.btnStage1Read)
        Me.groupStage1.Controls.Add(Me.txtStage1Data)
        Me.groupStage1.Name = "groupStage1"
        Me.groupStage1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.groupStage1, resources.GetString("groupStage1.ToolTip"))
        '
        'btnStage1Clean
        '
        resources.ApplyResources(Me.btnStage1Clean, "btnStage1Clean")
        Me.btnStage1Clean.Name = "btnStage1Clean"
        Me.ToolTip1.SetToolTip(Me.btnStage1Clean, resources.GetString("btnStage1Clean.ToolTip"))
        Me.btnStage1Clean.UseVisualStyleBackColor = True
        '
        'btnStage1Read
        '
        resources.ApplyResources(Me.btnStage1Read, "btnStage1Read")
        Me.btnStage1Read.Name = "btnStage1Read"
        Me.ToolTip1.SetToolTip(Me.btnStage1Read, resources.GetString("btnStage1Read.ToolTip"))
        Me.btnStage1Read.UseVisualStyleBackColor = True
        '
        'txtStage1Data
        '
        resources.ApplyResources(Me.txtStage1Data, "txtStage1Data")
        Me.txtStage1Data.Name = "txtStage1Data"
        Me.ToolTip1.SetToolTip(Me.txtStage1Data, resources.GetString("txtStage1Data.ToolTip"))
        '
        'frmStageVerificationNew
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.groupStage4)
        Me.Controls.Add(Me.groupStage3)
        Me.Controls.Add(Me.groupStage2)
        Me.Controls.Add(Me.groupStage1)
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpVacuum)
        Me.Controls.Add(Me.grpPause)
        Me.Controls.Add(Me.grpSVTRun)
        Me.Controls.Add(Me.grpAlignmentPos)
        Me.Controls.Add(Me.grpArraySetting)
        Me.Controls.Add(Me.grpGoPos)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.UcJoyStick1)
        Me.Controls.Add(Me.txtList)
        Me.Controls.Add(Me.UcLightControl1)
        Me.Controls.Add(Me.grpArrayPos)
        Me.Controls.Add(Me.UcDisplay1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStageVerificationNew"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.grpArrayPos.ResumeLayout(False)
        CType(Me.nmcEndPosZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcCornerPosZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcStartPosZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcCornerPosY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcCornerPosX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcEndPosY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcEndPosX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcStartPosY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcStartPosX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGoPos.ResumeLayout(False)
        Me.grpGoPos.PerformLayout()
        Me.grpAlignmentPos.ResumeLayout(False)
        Me.grpSVTRun.ResumeLayout(False)
        CType(Me.nmcPitchVY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcPitchVX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcPitchHY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcPitchHX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcArrayYCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcArrayXCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpArraySetting.ResumeLayout(False)
        Me.grpPause.ResumeLayout(False)
        Me.grpVacuum.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.NumSteadyTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupStage4.ResumeLayout(False)
        Me.groupStage4.PerformLayout()
        Me.groupStage3.ResumeLayout(False)
        Me.groupStage3.PerformLayout()
        Me.groupStage2.ResumeLayout(False)
        Me.groupStage2.PerformLayout()
        Me.groupStage1.ResumeLayout(False)
        Me.groupStage1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents grpArrayPos As System.Windows.Forms.GroupBox
    Friend WithEvents nmcEndPosY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcEndPosX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcStartPosY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcStartPosX As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblEndPosX As System.Windows.Forms.Label
    Friend WithEvents lblStartPosX As System.Windows.Forms.Label
    Friend WithEvents lblEndPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblStartPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblEndPosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblStartPosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblEndPosY As System.Windows.Forms.Label
    Friend WithEvents lblStartPosY As System.Windows.Forms.Label
    Friend WithEvents nmcCornerPosY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcCornerPosX As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblCornerPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblCornerPosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblCornerX As System.Windows.Forms.Label
    Friend WithEvents lblCornerY As System.Windows.Forms.Label
    Friend WithEvents UcLightControl1 As ucLightControl
    Friend WithEvents txtList As System.Windows.Forms.TextBox
    Friend WithEvents lblStartPosZUnit As System.Windows.Forms.Label
    Friend WithEvents nmcStartPosZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblStartPosZ As System.Windows.Forms.Label
    Friend WithEvents lblEndPosZUnit As System.Windows.Forms.Label
    Friend WithEvents nmcEndPosZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblEndPosZ As System.Windows.Forms.Label
    Friend WithEvents lblCornerPosZUnit As System.Windows.Forms.Label
    Friend WithEvents nmcCornerPosZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblCornerZ As System.Windows.Forms.Label
    Friend WithEvents UcJoyStick1 As ucJoyStick
    Friend WithEvents btnSetStartPos As System.Windows.Forms.Button
    Friend WithEvents btnGoStartPos As System.Windows.Forms.Button
    Friend WithEvents btnSetCornerPos As System.Windows.Forms.Button
    Friend WithEvents btnGoCornerPos As System.Windows.Forms.Button
    Friend WithEvents btnSetEndPos As System.Windows.Forms.Button
    Friend WithEvents btnGoEndPos As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents grpGoPos As System.Windows.Forms.GroupBox
    Friend WithEvents cmbYa As System.Windows.Forms.ComboBox
    Friend WithEvents cmbXa As System.Windows.Forms.ComboBox
    Friend WithEvents btnGoCCDAlignPos As System.Windows.Forms.Button
    Friend WithEvents lblYa As System.Windows.Forms.Label
    Friend WithEvents lblXa As System.Windows.Forms.Label
    Friend WithEvents grpAlignmentPos As System.Windows.Forms.GroupBox
    Friend WithEvents btnTrainScene As System.Windows.Forms.Button
    Friend WithEvents lblStageNo As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grpSVTRun As System.Windows.Forms.GroupBox
    Friend WithEvents lblPitchVYUnit As System.Windows.Forms.Label
    Friend WithEvents lblPitchVXUnit As System.Windows.Forms.Label
    Friend WithEvents lblPitchHYUnit As System.Windows.Forms.Label
    Friend WithEvents lblPitchHXUnit As System.Windows.Forms.Label
    Friend WithEvents lblPitchVXText As System.Windows.Forms.Label
    Friend WithEvents lblPitchHXText As System.Windows.Forms.Label
    Friend WithEvents lblPitchVYText As System.Windows.Forms.Label
    Friend WithEvents lblPitchHYText As System.Windows.Forms.Label
    Friend WithEvents lblAngleText As System.Windows.Forms.Label
    Friend WithEvents lblAngle As System.Windows.Forms.Label
    Friend WithEvents BtnHorizontalRun As System.Windows.Forms.Button
    Friend WithEvents nmcArrayYCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcArrayXCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnSetPitch As System.Windows.Forms.Button
    Friend WithEvents lblArrayXCount As System.Windows.Forms.Label
    Friend WithEvents lblArrayYCount As System.Windows.Forms.Label
    Friend WithEvents grpArraySetting As System.Windows.Forms.GroupBox
    Friend WithEvents lblOffsetY As System.Windows.Forms.Label
    Friend WithEvents lblOffsetX As System.Windows.Forms.Label
    Friend WithEvents lblOdffsetYText As System.Windows.Forms.Label
    Friend WithEvents lblOffsetXText As System.Windows.Forms.Label
    Friend WithEvents btnCalibPos As System.Windows.Forms.Button
    Friend WithEvents lblOffsetYUnit As System.Windows.Forms.Label
    Friend WithEvents lblOffsetXUnit As System.Windows.Forms.Label
    Friend WithEvents BtnReRun As System.Windows.Forms.Button
    Friend WithEvents BtnVerticalRun As System.Windows.Forms.Button
    Friend WithEvents btnPause As System.Windows.Forms.Button
    Friend WithEvents grpPause As System.Windows.Forms.GroupBox
    Friend WithEvents btnVacuum As System.Windows.Forms.Button
    Friend WithEvents grpVacuum As System.Windows.Forms.GroupBox
    Friend WithEvents btnScene As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents NumSteadyTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblAction As System.Windows.Forms.Label
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents nmcPitchVY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcPitchVX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcPitchHY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcPitchHX As System.Windows.Forms.NumericUpDown
    Friend WithEvents groupStage4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnStage4Clean As System.Windows.Forms.Button
    Friend WithEvents btnStage4Read As System.Windows.Forms.Button
    Friend WithEvents txtStage4Data As System.Windows.Forms.TextBox
    Friend WithEvents groupStage3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnStage3Clean As System.Windows.Forms.Button
    Friend WithEvents btnStage3Read As System.Windows.Forms.Button
    Friend WithEvents txtStage3Data As System.Windows.Forms.TextBox
    Friend WithEvents groupStage2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnStage2Clean As System.Windows.Forms.Button
    Friend WithEvents btnStage2Read As System.Windows.Forms.Button
    Friend WithEvents txtStage2Data As System.Windows.Forms.TextBox
    Friend WithEvents groupStage1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnStage1Clean As System.Windows.Forms.Button
    Friend WithEvents btnStage1Read As System.Windows.Forms.Button
    Friend WithEvents txtStage1Data As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalibrationClearGlue
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalibrationClearGlue))
        Me.grpClearConfig = New System.Windows.Forms.GroupBox()
        Me.btnCylinder = New System.Windows.Forms.Button()
        Me.btnClearGlue = New System.Windows.Forms.Button()
        Me.nmcClearGlueNumLimit = New System.Windows.Forms.NumericUpDown()
        Me.nmcClearGluePressureTime = New System.Windows.Forms.NumericUpDown()
        Me.nmcClearGluePitch = New System.Windows.Forms.NumericUpDown()
        Me.nmcClearGlueDistanceZ = New System.Windows.Forms.NumericUpDown()
        Me.lblUnit5 = New System.Windows.Forms.Label()
        Me.lblTouchTime = New System.Windows.Forms.Label()
        Me.lblUnit4 = New System.Windows.Forms.Label()
        Me.lblZUp = New System.Windows.Forms.Label()
        Me.lblUnit6 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.lblPitch = New System.Windows.Forms.Label()
        Me.grpJetting = New System.Windows.Forms.GroupBox()
        Me.lblHomeAccUnit = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkMotorDir = New System.Windows.Forms.CheckBox()
        Me.nmcClearGlueMoveSpeed = New System.Windows.Forms.NumericUpDown()
        Me.nmcClearGlueMoveOffset = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpClearPos = New System.Windows.Forms.GroupBox()
        Me.nmcValveClearGluePosZ = New System.Windows.Forms.NumericUpDown()
        Me.nmcValveClearGluePosY = New System.Windows.Forms.NumericUpDown()
        Me.nmcValveClearGluePosX = New System.Windows.Forms.NumericUpDown()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.lblUnit3 = New System.Windows.Forms.Label()
        Me.lblAxisZ = New System.Windows.Forms.Label()
        Me.lblUnit2 = New System.Windows.Forms.Label()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.btnGetDispenserNo1ClearGluePos = New System.Windows.Forms.Button()
        Me.lblAxisY = New System.Windows.Forms.Label()
        Me.lblAxisX = New System.Windows.Forms.Label()
        Me.cmbValve = New System.Windows.Forms.ComboBox()
        Me.lblSelctValve = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.btnClearGluePrev = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.grpClearConfig.SuspendLayout()
        CType(Me.nmcClearGlueNumLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcClearGluePressureTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcClearGluePitch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcClearGlueDistanceZ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpJetting.SuspendLayout()
        CType(Me.nmcClearGlueMoveSpeed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcClearGlueMoveOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpClearPos.SuspendLayout()
        CType(Me.nmcValveClearGluePosZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcValveClearGluePosY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcValveClearGluePosX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpClearConfig
        '
        Me.grpClearConfig.Controls.Add(Me.btnCylinder)
        Me.grpClearConfig.Controls.Add(Me.btnClearGlue)
        Me.grpClearConfig.Controls.Add(Me.nmcClearGlueNumLimit)
        Me.grpClearConfig.Controls.Add(Me.nmcClearGluePressureTime)
        Me.grpClearConfig.Controls.Add(Me.nmcClearGluePitch)
        Me.grpClearConfig.Controls.Add(Me.nmcClearGlueDistanceZ)
        Me.grpClearConfig.Controls.Add(Me.lblUnit5)
        Me.grpClearConfig.Controls.Add(Me.lblTouchTime)
        Me.grpClearConfig.Controls.Add(Me.lblUnit4)
        Me.grpClearConfig.Controls.Add(Me.lblZUp)
        Me.grpClearConfig.Controls.Add(Me.lblUnit6)
        Me.grpClearConfig.Controls.Add(Me.lblCount)
        Me.grpClearConfig.Controls.Add(Me.lblPitch)
        resources.ApplyResources(Me.grpClearConfig, "grpClearConfig")
        Me.grpClearConfig.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpClearConfig.Name = "grpClearConfig"
        Me.grpClearConfig.TabStop = False
        '
        'btnCylinder
        '
        resources.ApplyResources(Me.btnCylinder, "btnCylinder")
        Me.btnCylinder.Name = "btnCylinder"
        Me.btnCylinder.UseVisualStyleBackColor = True
        '
        'btnClearGlue
        '
        Me.btnClearGlue.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnClearGlue, "btnClearGlue")
        Me.btnClearGlue.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnClearGlue.FlatAppearance.BorderSize = 0
        Me.btnClearGlue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnClearGlue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnClearGlue.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnClearGlue.Name = "btnClearGlue"
        Me.ToolTip1.SetToolTip(Me.btnClearGlue, resources.GetString("btnClearGlue.ToolTip"))
        Me.btnClearGlue.UseVisualStyleBackColor = True
        '
        'nmcClearGlueNumLimit
        '
        resources.ApplyResources(Me.nmcClearGlueNumLimit, "nmcClearGlueNumLimit")
        Me.nmcClearGlueNumLimit.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcClearGlueNumLimit.Name = "nmcClearGlueNumLimit"
        Me.nmcClearGlueNumLimit.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'nmcClearGluePressureTime
        '
        resources.ApplyResources(Me.nmcClearGluePressureTime, "nmcClearGluePressureTime")
        Me.nmcClearGluePressureTime.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcClearGluePressureTime.Name = "nmcClearGluePressureTime"
        Me.nmcClearGluePressureTime.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'nmcClearGluePitch
        '
        Me.nmcClearGluePitch.DecimalPlaces = 3
        resources.ApplyResources(Me.nmcClearGluePitch, "nmcClearGluePitch")
        Me.nmcClearGluePitch.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcClearGluePitch.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcClearGluePitch.Name = "nmcClearGluePitch"
        Me.nmcClearGluePitch.Value = New Decimal(New Integer() {999999, 0, 0, 196608})
        '
        'nmcClearGlueDistanceZ
        '
        Me.nmcClearGlueDistanceZ.DecimalPlaces = 3
        resources.ApplyResources(Me.nmcClearGlueDistanceZ, "nmcClearGlueDistanceZ")
        Me.nmcClearGlueDistanceZ.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcClearGlueDistanceZ.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcClearGlueDistanceZ.Name = "nmcClearGlueDistanceZ"
        Me.nmcClearGlueDistanceZ.Value = New Decimal(New Integer() {999999, 0, 0, 196608})
        '
        'lblUnit5
        '
        Me.lblUnit5.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblUnit5, "lblUnit5")
        Me.lblUnit5.Name = "lblUnit5"
        '
        'lblTouchTime
        '
        Me.lblTouchTime.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblTouchTime, "lblTouchTime")
        Me.lblTouchTime.Name = "lblTouchTime"
        '
        'lblUnit4
        '
        Me.lblUnit4.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblUnit4, "lblUnit4")
        Me.lblUnit4.Name = "lblUnit4"
        '
        'lblZUp
        '
        Me.lblZUp.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblZUp, "lblZUp")
        Me.lblZUp.Name = "lblZUp"
        '
        'lblUnit6
        '
        Me.lblUnit6.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblUnit6, "lblUnit6")
        Me.lblUnit6.Name = "lblUnit6"
        '
        'lblCount
        '
        Me.lblCount.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblCount, "lblCount")
        Me.lblCount.Name = "lblCount"
        '
        'lblPitch
        '
        Me.lblPitch.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblPitch, "lblPitch")
        Me.lblPitch.Name = "lblPitch"
        '
        'grpJetting
        '
        Me.grpJetting.Controls.Add(Me.lblHomeAccUnit)
        Me.grpJetting.Controls.Add(Me.Label3)
        Me.grpJetting.Controls.Add(Me.chkMotorDir)
        Me.grpJetting.Controls.Add(Me.nmcClearGlueMoveSpeed)
        Me.grpJetting.Controls.Add(Me.nmcClearGlueMoveOffset)
        Me.grpJetting.Controls.Add(Me.Label1)
        Me.grpJetting.Controls.Add(Me.Label2)
        resources.ApplyResources(Me.grpJetting, "grpJetting")
        Me.grpJetting.Name = "grpJetting"
        Me.grpJetting.TabStop = False
        '
        'lblHomeAccUnit
        '
        resources.ApplyResources(Me.lblHomeAccUnit, "lblHomeAccUnit")
        Me.lblHomeAccUnit.Name = "lblHomeAccUnit"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'chkMotorDir
        '
        resources.ApplyResources(Me.chkMotorDir, "chkMotorDir")
        Me.chkMotorDir.Name = "chkMotorDir"
        Me.chkMotorDir.UseVisualStyleBackColor = True
        '
        'nmcClearGlueMoveSpeed
        '
        resources.ApplyResources(Me.nmcClearGlueMoveSpeed, "nmcClearGlueMoveSpeed")
        Me.nmcClearGlueMoveSpeed.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcClearGlueMoveSpeed.Name = "nmcClearGlueMoveSpeed"
        Me.nmcClearGlueMoveSpeed.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'nmcClearGlueMoveOffset
        '
        resources.ApplyResources(Me.nmcClearGlueMoveOffset, "nmcClearGlueMoveOffset")
        Me.nmcClearGlueMoveOffset.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcClearGlueMoveOffset.Name = "nmcClearGlueMoveOffset"
        Me.nmcClearGlueMoveOffset.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'grpClearPos
        '
        Me.grpClearPos.Controls.Add(Me.nmcValveClearGluePosZ)
        Me.grpClearPos.Controls.Add(Me.nmcValveClearGluePosY)
        Me.grpClearPos.Controls.Add(Me.nmcValveClearGluePosX)
        Me.grpClearPos.Controls.Add(Me.btnGo)
        Me.grpClearPos.Controls.Add(Me.lblUnit3)
        Me.grpClearPos.Controls.Add(Me.lblAxisZ)
        Me.grpClearPos.Controls.Add(Me.lblUnit2)
        Me.grpClearPos.Controls.Add(Me.lblUnit)
        Me.grpClearPos.Controls.Add(Me.btnGetDispenserNo1ClearGluePos)
        Me.grpClearPos.Controls.Add(Me.lblAxisY)
        Me.grpClearPos.Controls.Add(Me.lblAxisX)
        resources.ApplyResources(Me.grpClearPos, "grpClearPos")
        Me.grpClearPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpClearPos.Name = "grpClearPos"
        Me.grpClearPos.TabStop = False
        '
        'nmcValveClearGluePosZ
        '
        Me.nmcValveClearGluePosZ.DecimalPlaces = 3
        resources.ApplyResources(Me.nmcValveClearGluePosZ, "nmcValveClearGluePosZ")
        Me.nmcValveClearGluePosZ.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcValveClearGluePosZ.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcValveClearGluePosZ.Name = "nmcValveClearGluePosZ"
        Me.nmcValveClearGluePosZ.Value = New Decimal(New Integer() {999999, 0, 0, 196608})
        '
        'nmcValveClearGluePosY
        '
        Me.nmcValveClearGluePosY.DecimalPlaces = 3
        resources.ApplyResources(Me.nmcValveClearGluePosY, "nmcValveClearGluePosY")
        Me.nmcValveClearGluePosY.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcValveClearGluePosY.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcValveClearGluePosY.Name = "nmcValveClearGluePosY"
        Me.nmcValveClearGluePosY.Value = New Decimal(New Integer() {999999, 0, 0, 196608})
        '
        'nmcValveClearGluePosX
        '
        Me.nmcValveClearGluePosX.DecimalPlaces = 3
        resources.ApplyResources(Me.nmcValveClearGluePosX, "nmcValveClearGluePosX")
        Me.nmcValveClearGluePosX.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcValveClearGluePosX.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcValveClearGluePosX.Name = "nmcValveClearGluePosX"
        Me.nmcValveClearGluePosX.Value = New Decimal(New Integer() {999999, 0, 0, 196608})
        '
        'btnGo
        '
        Me.btnGo.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        resources.ApplyResources(Me.btnGo, "btnGo")
        Me.btnGo.FlatAppearance.BorderSize = 0
        Me.btnGo.Name = "btnGo"
        Me.ToolTip1.SetToolTip(Me.btnGo, resources.GetString("btnGo.ToolTip"))
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'lblUnit3
        '
        Me.lblUnit3.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblUnit3, "lblUnit3")
        Me.lblUnit3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUnit3.Name = "lblUnit3"
        '
        'lblAxisZ
        '
        Me.lblAxisZ.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblAxisZ, "lblAxisZ")
        Me.lblAxisZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblAxisZ.Name = "lblAxisZ"
        '
        'lblUnit2
        '
        Me.lblUnit2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblUnit2, "lblUnit2")
        Me.lblUnit2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUnit2.Name = "lblUnit2"
        '
        'lblUnit
        '
        Me.lblUnit.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblUnit, "lblUnit")
        Me.lblUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUnit.Name = "lblUnit"
        '
        'btnGetDispenserNo1ClearGluePos
        '
        Me.btnGetDispenserNo1ClearGluePos.BackColor = System.Drawing.SystemColors.Control
        Me.btnGetDispenserNo1ClearGluePos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        resources.ApplyResources(Me.btnGetDispenserNo1ClearGluePos, "btnGetDispenserNo1ClearGluePos")
        Me.btnGetDispenserNo1ClearGluePos.FlatAppearance.BorderSize = 0
        Me.btnGetDispenserNo1ClearGluePos.Name = "btnGetDispenserNo1ClearGluePos"
        Me.ToolTip1.SetToolTip(Me.btnGetDispenserNo1ClearGluePos, resources.GetString("btnGetDispenserNo1ClearGluePos.ToolTip"))
        Me.btnGetDispenserNo1ClearGluePos.UseVisualStyleBackColor = True
        '
        'lblAxisY
        '
        Me.lblAxisY.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblAxisY, "lblAxisY")
        Me.lblAxisY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblAxisY.Name = "lblAxisY"
        '
        'lblAxisX
        '
        Me.lblAxisX.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblAxisX, "lblAxisX")
        Me.lblAxisX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblAxisX.Name = "lblAxisX"
        '
        'cmbValve
        '
        Me.cmbValve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cmbValve, "cmbValve")
        Me.cmbValve.FormattingEnabled = True
        Me.cmbValve.Items.AddRange(New Object() {resources.GetString("cmbValve.Items")})
        Me.cmbValve.Name = "cmbValve"
        '
        'lblSelctValve
        '
        resources.ApplyResources(Me.lblSelctValve, "lblSelctValve")
        Me.lblSelctValve.Name = "lblSelctValve"
        '
        'btnOK
        '
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnOK.Name = "btnOK"
        Me.ToolTip1.SetToolTip(Me.btnOK, resources.GetString("btnOK.ToolTip"))
        Me.btnOK.UseVisualStyleBackColor = True
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
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        '
        'btnClearGluePrev
        '
        resources.ApplyResources(Me.btnClearGluePrev, "btnClearGluePrev")
        Me.btnClearGluePrev.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClearGluePrev.Name = "btnClearGluePrev"
        Me.btnClearGluePrev.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 300
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
        resources.ApplyResources(Me.UcJoyStick1, "UcJoyStick1")
        Me.UcJoyStick1.Name = "UcJoyStick1"
        '
        'frmCalibrationClearGlue
        '
        Me.AcceptButton = Me.btnOK
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CancelButton = Me.btnCancel
        Me.ControlBox = False
        Me.Controls.Add(Me.grpJetting)
        Me.Controls.Add(Me.btnClearGluePrev)
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.lblSelctValve)
        Me.Controls.Add(Me.cmbValve)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.UcJoyStick1)
        Me.Controls.Add(Me.grpClearConfig)
        Me.Controls.Add(Me.grpClearPos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCalibrationClearGlue"
        Me.grpClearConfig.ResumeLayout(False)
        CType(Me.nmcClearGlueNumLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcClearGluePressureTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcClearGluePitch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcClearGlueDistanceZ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpJetting.ResumeLayout(False)
        Me.grpJetting.PerformLayout()
        CType(Me.nmcClearGlueMoveSpeed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcClearGlueMoveOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpClearPos.ResumeLayout(False)
        CType(Me.nmcValveClearGluePosZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcValveClearGluePosY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcValveClearGluePosX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcJoyStick1 As ucJoyStick
    Friend WithEvents grpClearConfig As System.Windows.Forms.GroupBox
    Friend WithEvents lblUnit5 As System.Windows.Forms.Label
    Friend WithEvents lblTouchTime As System.Windows.Forms.Label
    Friend WithEvents lblUnit4 As System.Windows.Forms.Label
    Friend WithEvents lblZUp As System.Windows.Forms.Label
    Friend WithEvents lblUnit6 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents lblPitch As System.Windows.Forms.Label
    Friend WithEvents grpClearPos As System.Windows.Forms.GroupBox
    Friend WithEvents lblUnit3 As System.Windows.Forms.Label
    Friend WithEvents lblAxisZ As System.Windows.Forms.Label
    Friend WithEvents lblUnit2 As System.Windows.Forms.Label
    Friend WithEvents lblUnit As System.Windows.Forms.Label
    Friend WithEvents btnGetDispenserNo1ClearGluePos As System.Windows.Forms.Button
    Friend WithEvents lblAxisY As System.Windows.Forms.Label
    Friend WithEvents lblAxisX As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents cmbValve As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelctValve As System.Windows.Forms.Label
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents nmcClearGlueNumLimit As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcClearGluePressureTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcClearGluePitch As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcClearGlueDistanceZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcValveClearGluePosZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcValveClearGluePosY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcValveClearGluePosX As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnClearGlue As System.Windows.Forms.Button
    Friend WithEvents grpJetting As System.Windows.Forms.GroupBox
    Friend WithEvents chkMotorDir As System.Windows.Forms.CheckBox
    Friend WithEvents nmcClearGlueMoveSpeed As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcClearGlueMoveOffset As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents btnClearGluePrev As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblHomeAccUnit As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnCylinder As System.Windows.Forms.Button
End Class

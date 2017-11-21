<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalibrationCCD2Height
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalibrationCCD2Height))
        Me.grpHeightSensorPos = New System.Windows.Forms.GroupBox()
        Me.lblReaderUnit = New System.Windows.Forms.Label()
        Me.btnReZero = New System.Windows.Forms.Button()
        Me.txtLaserPosZ = New System.Windows.Forms.TextBox()
        Me.txtLaserPosY = New System.Windows.Forms.TextBox()
        Me.txtLaserPosX = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAutoZero = New System.Windows.Forms.Button()
        Me.lblLaserReaderValue = New System.Windows.Forms.Label()
        Me.btnGoSensorPos = New System.Windows.Forms.Button()
        Me.btnSetHightSensorPos = New System.Windows.Forms.Button()
        Me.lblLaserPosZ = New System.Windows.Forms.Label()
        Me.lblLaserPosY = New System.Windows.Forms.Label()
        Me.lblLaserPosX = New System.Windows.Forms.Label()
        Me.lblLaserPosZUnit = New System.Windows.Forms.Label()
        Me.lblLaserPosYUnit = New System.Windows.Forms.Label()
        Me.lblLaserPosXUnit = New System.Windows.Forms.Label()
        Me.grpCCDPos = New System.Windows.Forms.GroupBox()
        Me.txtCCDPosZ = New System.Windows.Forms.TextBox()
        Me.txtCCDPosY = New System.Windows.Forms.TextBox()
        Me.txtCCDPosX = New System.Windows.Forms.TextBox()
        Me.btnGoCCDPos = New System.Windows.Forms.Button()
        Me.btnSetCcdPos = New System.Windows.Forms.Button()
        Me.lblCCDPosZUnit = New System.Windows.Forms.Label()
        Me.lblCCDPosYUnit = New System.Windows.Forms.Label()
        Me.lblCCDPosXUnit = New System.Windows.Forms.Label()
        Me.lblCCDPosZ = New System.Windows.Forms.Label()
        Me.lblCCDPosY = New System.Windows.Forms.Label()
        Me.lblCCDPosX = New System.Windows.Forms.Label()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCCDToHightNext = New System.Windows.Forms.Button()
        Me.btnCCDToHightPrev = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.nmcExposure = New System.Windows.Forms.NumericUpDown()
        Me.lblExposureTimeUnit = New System.Windows.Forms.Label()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.UcLightControl1 = New WindowsApplication1.ucLightControl()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.grpHeightSensorPos.SuspendLayout()
        Me.grpCCDPos.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nmcExposure, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpHeightSensorPos
        '
        Me.grpHeightSensorPos.Controls.Add(Me.lblReaderUnit)
        Me.grpHeightSensorPos.Controls.Add(Me.btnReZero)
        Me.grpHeightSensorPos.Controls.Add(Me.txtLaserPosZ)
        Me.grpHeightSensorPos.Controls.Add(Me.txtLaserPosY)
        Me.grpHeightSensorPos.Controls.Add(Me.txtLaserPosX)
        Me.grpHeightSensorPos.Controls.Add(Me.Label1)
        Me.grpHeightSensorPos.Controls.Add(Me.btnAutoZero)
        Me.grpHeightSensorPos.Controls.Add(Me.lblLaserReaderValue)
        Me.grpHeightSensorPos.Controls.Add(Me.btnGoSensorPos)
        Me.grpHeightSensorPos.Controls.Add(Me.btnSetHightSensorPos)
        Me.grpHeightSensorPos.Controls.Add(Me.lblLaserPosZ)
        Me.grpHeightSensorPos.Controls.Add(Me.lblLaserPosY)
        Me.grpHeightSensorPos.Controls.Add(Me.lblLaserPosX)
        Me.grpHeightSensorPos.Controls.Add(Me.lblLaserPosZUnit)
        Me.grpHeightSensorPos.Controls.Add(Me.lblLaserPosYUnit)
        Me.grpHeightSensorPos.Controls.Add(Me.lblLaserPosXUnit)
        resources.ApplyResources(Me.grpHeightSensorPos, "grpHeightSensorPos")
        Me.grpHeightSensorPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpHeightSensorPos.Name = "grpHeightSensorPos"
        Me.grpHeightSensorPos.TabStop = False
        '
        'lblReaderUnit
        '
        resources.ApplyResources(Me.lblReaderUnit, "lblReaderUnit")
        Me.lblReaderUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblReaderUnit.Name = "lblReaderUnit"
        '
        'btnReZero
        '
        resources.ApplyResources(Me.btnReZero, "btnReZero")
        Me.btnReZero.Name = "btnReZero"
        Me.btnReZero.UseVisualStyleBackColor = True
        '
        'txtLaserPosZ
        '
        Me.txtLaserPosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtLaserPosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtLaserPosZ, "txtLaserPosZ")
        Me.txtLaserPosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtLaserPosZ.Name = "txtLaserPosZ"
        Me.txtLaserPosZ.ReadOnly = True
        '
        'txtLaserPosY
        '
        Me.txtLaserPosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtLaserPosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtLaserPosY, "txtLaserPosY")
        Me.txtLaserPosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtLaserPosY.Name = "txtLaserPosY"
        Me.txtLaserPosY.ReadOnly = True
        '
        'txtLaserPosX
        '
        Me.txtLaserPosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtLaserPosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtLaserPosX, "txtLaserPosX")
        Me.txtLaserPosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtLaserPosX.Name = "txtLaserPosX"
        Me.txtLaserPosX.ReadOnly = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'btnAutoZero
        '
        resources.ApplyResources(Me.btnAutoZero, "btnAutoZero")
        Me.btnAutoZero.Name = "btnAutoZero"
        Me.btnAutoZero.UseVisualStyleBackColor = True
        '
        'lblLaserReaderValue
        '
        Me.lblLaserReaderValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.lblLaserReaderValue, "lblLaserReaderValue")
        Me.lblLaserReaderValue.Name = "lblLaserReaderValue"
        '
        'btnGoSensorPos
        '
        Me.btnGoSensorPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        resources.ApplyResources(Me.btnGoSensorPos, "btnGoSensorPos")
        Me.btnGoSensorPos.FlatAppearance.BorderSize = 0
        Me.btnGoSensorPos.Name = "btnGoSensorPos"
        Me.ToolTip.SetToolTip(Me.btnGoSensorPos, resources.GetString("btnGoSensorPos.ToolTip"))
        Me.btnGoSensorPos.UseVisualStyleBackColor = True
        '
        'btnSetHightSensorPos
        '
        Me.btnSetHightSensorPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetHightSensorPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        resources.ApplyResources(Me.btnSetHightSensorPos, "btnSetHightSensorPos")
        Me.btnSetHightSensorPos.FlatAppearance.BorderSize = 0
        Me.btnSetHightSensorPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetHightSensorPos.Name = "btnSetHightSensorPos"
        Me.ToolTip.SetToolTip(Me.btnSetHightSensorPos, resources.GetString("btnSetHightSensorPos.ToolTip"))
        Me.btnSetHightSensorPos.UseVisualStyleBackColor = True
        '
        'lblLaserPosZ
        '
        resources.ApplyResources(Me.lblLaserPosZ, "lblLaserPosZ")
        Me.lblLaserPosZ.Name = "lblLaserPosZ"
        '
        'lblLaserPosY
        '
        resources.ApplyResources(Me.lblLaserPosY, "lblLaserPosY")
        Me.lblLaserPosY.Name = "lblLaserPosY"
        '
        'lblLaserPosX
        '
        resources.ApplyResources(Me.lblLaserPosX, "lblLaserPosX")
        Me.lblLaserPosX.Name = "lblLaserPosX"
        '
        'lblLaserPosZUnit
        '
        resources.ApplyResources(Me.lblLaserPosZUnit, "lblLaserPosZUnit")
        Me.lblLaserPosZUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLaserPosZUnit.Name = "lblLaserPosZUnit"
        '
        'lblLaserPosYUnit
        '
        resources.ApplyResources(Me.lblLaserPosYUnit, "lblLaserPosYUnit")
        Me.lblLaserPosYUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLaserPosYUnit.Name = "lblLaserPosYUnit"
        '
        'lblLaserPosXUnit
        '
        resources.ApplyResources(Me.lblLaserPosXUnit, "lblLaserPosXUnit")
        Me.lblLaserPosXUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLaserPosXUnit.Name = "lblLaserPosXUnit"
        '
        'grpCCDPos
        '
        Me.grpCCDPos.Controls.Add(Me.txtCCDPosZ)
        Me.grpCCDPos.Controls.Add(Me.txtCCDPosY)
        Me.grpCCDPos.Controls.Add(Me.txtCCDPosX)
        Me.grpCCDPos.Controls.Add(Me.btnGoCCDPos)
        Me.grpCCDPos.Controls.Add(Me.btnSetCcdPos)
        Me.grpCCDPos.Controls.Add(Me.lblCCDPosZUnit)
        Me.grpCCDPos.Controls.Add(Me.lblCCDPosYUnit)
        Me.grpCCDPos.Controls.Add(Me.lblCCDPosXUnit)
        Me.grpCCDPos.Controls.Add(Me.lblCCDPosZ)
        Me.grpCCDPos.Controls.Add(Me.lblCCDPosY)
        Me.grpCCDPos.Controls.Add(Me.lblCCDPosX)
        resources.ApplyResources(Me.grpCCDPos, "grpCCDPos")
        Me.grpCCDPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpCCDPos.Name = "grpCCDPos"
        Me.grpCCDPos.TabStop = False
        '
        'txtCCDPosZ
        '
        Me.txtCCDPosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtCCDPosZ, "txtCCDPosZ")
        Me.txtCCDPosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosZ.Name = "txtCCDPosZ"
        Me.txtCCDPosZ.ReadOnly = True
        '
        'txtCCDPosY
        '
        Me.txtCCDPosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtCCDPosY, "txtCCDPosY")
        Me.txtCCDPosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosY.Name = "txtCCDPosY"
        Me.txtCCDPosY.ReadOnly = True
        '
        'txtCCDPosX
        '
        Me.txtCCDPosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtCCDPosX, "txtCCDPosX")
        Me.txtCCDPosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosX.Name = "txtCCDPosX"
        Me.txtCCDPosX.ReadOnly = True
        '
        'btnGoCCDPos
        '
        Me.btnGoCCDPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        resources.ApplyResources(Me.btnGoCCDPos, "btnGoCCDPos")
        Me.btnGoCCDPos.FlatAppearance.BorderSize = 0
        Me.btnGoCCDPos.Name = "btnGoCCDPos"
        Me.ToolTip.SetToolTip(Me.btnGoCCDPos, resources.GetString("btnGoCCDPos.ToolTip"))
        Me.btnGoCCDPos.UseVisualStyleBackColor = True
        '
        'btnSetCcdPos
        '
        Me.btnSetCcdPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetCcdPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        resources.ApplyResources(Me.btnSetCcdPos, "btnSetCcdPos")
        Me.btnSetCcdPos.FlatAppearance.BorderSize = 0
        Me.btnSetCcdPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetCcdPos.Name = "btnSetCcdPos"
        Me.ToolTip.SetToolTip(Me.btnSetCcdPos, resources.GetString("btnSetCcdPos.ToolTip"))
        Me.btnSetCcdPos.UseVisualStyleBackColor = True
        '
        'lblCCDPosZUnit
        '
        resources.ApplyResources(Me.lblCCDPosZUnit, "lblCCDPosZUnit")
        Me.lblCCDPosZUnit.Name = "lblCCDPosZUnit"
        '
        'lblCCDPosYUnit
        '
        resources.ApplyResources(Me.lblCCDPosYUnit, "lblCCDPosYUnit")
        Me.lblCCDPosYUnit.Name = "lblCCDPosYUnit"
        '
        'lblCCDPosXUnit
        '
        resources.ApplyResources(Me.lblCCDPosXUnit, "lblCCDPosXUnit")
        Me.lblCCDPosXUnit.Name = "lblCCDPosXUnit"
        '
        'lblCCDPosZ
        '
        resources.ApplyResources(Me.lblCCDPosZ, "lblCCDPosZ")
        Me.lblCCDPosZ.Name = "lblCCDPosZ"
        '
        'lblCCDPosY
        '
        resources.ApplyResources(Me.lblCCDPosY, "lblCCDPosY")
        Me.lblCCDPosY.Name = "lblCCDPosY"
        '
        'lblCCDPosX
        '
        resources.ApplyResources(Me.lblCCDPosX, "lblCCDPosX")
        Me.lblCCDPosX.Name = "lblCCDPosX"
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
        'btnCCDToHightNext
        '
        resources.ApplyResources(Me.btnCCDToHightNext, "btnCCDToHightNext")
        Me.btnCCDToHightNext.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCCDToHightNext.Name = "btnCCDToHightNext"
        Me.ToolTip.SetToolTip(Me.btnCCDToHightNext, resources.GetString("btnCCDToHightNext.ToolTip"))
        Me.btnCCDToHightNext.UseVisualStyleBackColor = True
        '
        'btnCCDToHightPrev
        '
        resources.ApplyResources(Me.btnCCDToHightPrev, "btnCCDToHightPrev")
        Me.btnCCDToHightPrev.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCCDToHightPrev.Name = "btnCCDToHightPrev"
        Me.ToolTip.SetToolTip(Me.btnCCDToHightPrev, resources.GetString("btnCCDToHightPrev.ToolTip"))
        Me.btnCCDToHightPrev.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.nmcExposure)
        Me.GroupBox1.Controls.Add(Me.lblExposureTimeUnit)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'nmcExposure
        '
        Me.nmcExposure.DecimalPlaces = 1
        resources.ApplyResources(Me.nmcExposure, "nmcExposure")
        Me.nmcExposure.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmcExposure.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.nmcExposure.Name = "nmcExposure"
        '
        'lblExposureTimeUnit
        '
        resources.ApplyResources(Me.lblExposureTimeUnit, "lblExposureTimeUnit")
        Me.lblExposureTimeUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblExposureTimeUnit.Name = "lblExposureTimeUnit"
        '
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        '
        'UcLightControl1
        '
        Me.UcLightControl1.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me.UcLightControl1, "UcLightControl1")
        Me.UcLightControl1.Name = "UcLightControl1"
        '
        'UcDisplay1
        '
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.UcDisplay1, "UcDisplay1")
        Me.UcDisplay1.Name = "UcDisplay1"
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
        Me.UcJoyStick1.ForeColor = System.Drawing.SystemColors.Control
        Me.UcJoyStick1.Name = "UcJoyStick1"
        '
        'Timer1
        '
        Me.Timer1.Interval = 300
        '
        'frmCalibrationCCD2Height
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCCDToHightNext)
        Me.Controls.Add(Me.btnCCDToHightPrev)
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.UcLightControl1)
        Me.Controls.Add(Me.UcDisplay1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.UcJoyStick1)
        Me.Controls.Add(Me.grpHeightSensorPos)
        Me.Controls.Add(Me.grpCCDPos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCalibrationCCD2Height"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.grpHeightSensorPos.ResumeLayout(False)
        Me.grpHeightSensorPos.PerformLayout()
        Me.grpCCDPos.ResumeLayout(False)
        Me.grpCCDPos.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nmcExposure, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcJoyStick1 As ucJoyStick
    Friend WithEvents grpHeightSensorPos As System.Windows.Forms.GroupBox
    Friend WithEvents btnSetHightSensorPos As System.Windows.Forms.Button
    Friend WithEvents lblLaserPosZ As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosY As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosX As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblLaserPosXUnit As System.Windows.Forms.Label
    Friend WithEvents grpCCDPos As System.Windows.Forms.GroupBox
    Friend WithEvents btnSetCcdPos As System.Windows.Forms.Button
    Friend WithEvents lblCCDPosZUnit As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosZ As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosY As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosX As System.Windows.Forms.Label
    Friend WithEvents btnGoCCDPos As System.Windows.Forms.Button
    Friend WithEvents btnGoSensorPos As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents btnAutoZero As System.Windows.Forms.Button
    Friend WithEvents lblLaserReaderValue As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLaserPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtLaserPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtLaserPosX As System.Windows.Forms.TextBox
    Friend WithEvents txtCCDPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCCDPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCCDPosX As System.Windows.Forms.TextBox
    Friend WithEvents UcLightControl1 As ucLightControl
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents nmcExposure As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblExposureTimeUnit As System.Windows.Forms.Label
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents btnCCDToHightNext As System.Windows.Forms.Button
    Friend WithEvents btnCCDToHightPrev As System.Windows.Forms.Button
    Friend WithEvents btnReZero As System.Windows.Forms.Button
    Friend WithEvents lblReaderUnit As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class

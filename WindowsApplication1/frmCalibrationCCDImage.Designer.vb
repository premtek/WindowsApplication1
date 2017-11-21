<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalibrationCCDImage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalibrationCCDImage))
        Me.UcLightControl1 = New WindowsApplication1.ucLightControl()
        Me.btnAWB = New System.Windows.Forms.Button()
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.nmcExposure = New System.Windows.Forms.NumericUpDown()
        Me.lblExposureTimeUnit = New System.Windows.Forms.Label()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.grpCalibration = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCalibration = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.numTileSize = New System.Windows.Forms.NumericUpDown()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageCCD = New System.Windows.Forms.TabPage()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.TabPageCalib = New System.Windows.Forms.TabPage()
        Me.UcDisplay2 = New ProjectAOI.ucDisplay()
        Me.grpCCDScale = New System.Windows.Forms.GroupBox()
        Me.lblSceneSet = New System.Windows.Forms.Label()
        Me.btnTrainScene = New System.Windows.Forms.Button()
        Me.lblScaleYYUnit = New System.Windows.Forms.Label()
        Me.lblScaleYXUnit = New System.Windows.Forms.Label()
        Me.btnGoCCD = New System.Windows.Forms.Button()
        Me.lblScaleXYUnit = New System.Windows.Forms.Label()
        Me.btnSetCcdPosX = New System.Windows.Forms.Button()
        Me.lblScaleXXUnit = New System.Windows.Forms.Label()
        Me.lblCCDPosXUnit = New System.Windows.Forms.Label()
        Me.lblScaleYX = New System.Windows.Forms.Label()
        Me.lblCCDPosYUnit = New System.Windows.Forms.Label()
        Me.txtA21 = New System.Windows.Forms.TextBox()
        Me.lblCCDPosZUnit = New System.Windows.Forms.Label()
        Me.txtA12 = New System.Windows.Forms.TextBox()
        Me.txtCCDPosZ = New System.Windows.Forms.TextBox()
        Me.lblScaleXY = New System.Windows.Forms.Label()
        Me.lblSceneID = New System.Windows.Forms.Label()
        Me.btnScalePR = New System.Windows.Forms.Button()
        Me.lblCCDPosZ = New System.Windows.Forms.Label()
        Me.txtA22 = New System.Windows.Forms.TextBox()
        Me.txtCCDPosY = New System.Windows.Forms.TextBox()
        Me.lblScaleYY = New System.Windows.Forms.Label()
        Me.txtCCDPosX = New System.Windows.Forms.TextBox()
        Me.txtA11 = New System.Windows.Forms.TextBox()
        Me.lblCCDPosY = New System.Windows.Forms.Label()
        Me.lblScaleXX = New System.Windows.Forms.Label()
        Me.lblCCDPosX = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lblSteadyTime = New System.Windows.Forms.Label()
        Me.NumSteadyTime = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nmcExposure, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCalibration.SuspendLayout()
        CType(Me.numTileSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPageCCD.SuspendLayout()
        Me.TabPageCalib.SuspendLayout()
        Me.grpCCDScale.SuspendLayout()
        CType(Me.NumSteadyTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UcLightControl1
        '
        Me.UcLightControl1.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me.UcLightControl1, "UcLightControl1")
        Me.UcLightControl1.Name = "UcLightControl1"
        '
        'btnAWB
        '
        resources.ApplyResources(Me.btnAWB, "btnAWB")
        Me.btnAWB.Name = "btnAWB"
        Me.btnAWB.UseVisualStyleBackColor = True
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
        Me.UcJoyStick1.Cursor = System.Windows.Forms.Cursors.IBeam
        resources.ApplyResources(Me.UcJoyStick1, "UcJoyStick1")
        Me.UcJoyStick1.Name = "UcJoyStick1"
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
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'grpCalibration
        '
        Me.grpCalibration.Controls.Add(Me.Label2)
        Me.grpCalibration.Controls.Add(Me.btnCalibration)
        Me.grpCalibration.Controls.Add(Me.Label1)
        Me.grpCalibration.Controls.Add(Me.numTileSize)
        resources.ApplyResources(Me.grpCalibration, "grpCalibration")
        Me.grpCalibration.Name = "grpCalibration"
        Me.grpCalibration.TabStop = False
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Name = "Label2"
        '
        'btnCalibration
        '
        resources.ApplyResources(Me.btnCalibration, "btnCalibration")
        Me.btnCalibration.Name = "btnCalibration"
        Me.btnCalibration.UseVisualStyleBackColor = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Name = "Label1"
        '
        'numTileSize
        '
        Me.numTileSize.DecimalPlaces = 1
        resources.ApplyResources(Me.numTileSize, "numTileSize")
        Me.numTileSize.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.numTileSize.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.numTileSize.Name = "numTileSize"
        Me.numTileSize.Value = New Decimal(New Integer() {5, 0, 0, 65536})
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageCCD)
        Me.TabControl1.Controls.Add(Me.TabPageCalib)
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        '
        'TabPageCCD
        '
        Me.TabPageCCD.Controls.Add(Me.UcDisplay1)
        resources.ApplyResources(Me.TabPageCCD, "TabPageCCD")
        Me.TabPageCCD.Name = "TabPageCCD"
        Me.TabPageCCD.UseVisualStyleBackColor = True
        '
        'UcDisplay1
        '
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.UcDisplay1, "UcDisplay1")
        Me.UcDisplay1.Name = "UcDisplay1"
        '
        'TabPageCalib
        '
        Me.TabPageCalib.Controls.Add(Me.UcDisplay2)
        resources.ApplyResources(Me.TabPageCalib, "TabPageCalib")
        Me.TabPageCalib.Name = "TabPageCalib"
        Me.TabPageCalib.UseVisualStyleBackColor = True
        '
        'UcDisplay2
        '
        Me.UcDisplay2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.UcDisplay2, "UcDisplay2")
        Me.UcDisplay2.Name = "UcDisplay2"
        '
        'grpCCDScale
        '
        Me.grpCCDScale.Controls.Add(Me.Label4)
        Me.grpCCDScale.Controls.Add(Me.NumSteadyTime)
        Me.grpCCDScale.Controls.Add(Me.lblSteadyTime)
        Me.grpCCDScale.Controls.Add(Me.lblSceneSet)
        Me.grpCCDScale.Controls.Add(Me.btnTrainScene)
        Me.grpCCDScale.Controls.Add(Me.lblScaleYYUnit)
        Me.grpCCDScale.Controls.Add(Me.lblScaleYXUnit)
        Me.grpCCDScale.Controls.Add(Me.btnGoCCD)
        Me.grpCCDScale.Controls.Add(Me.lblScaleXYUnit)
        Me.grpCCDScale.Controls.Add(Me.btnSetCcdPosX)
        Me.grpCCDScale.Controls.Add(Me.lblScaleXXUnit)
        Me.grpCCDScale.Controls.Add(Me.lblCCDPosXUnit)
        Me.grpCCDScale.Controls.Add(Me.lblScaleYX)
        Me.grpCCDScale.Controls.Add(Me.lblCCDPosYUnit)
        Me.grpCCDScale.Controls.Add(Me.txtA21)
        Me.grpCCDScale.Controls.Add(Me.lblCCDPosZUnit)
        Me.grpCCDScale.Controls.Add(Me.txtA12)
        Me.grpCCDScale.Controls.Add(Me.txtCCDPosZ)
        Me.grpCCDScale.Controls.Add(Me.lblScaleXY)
        Me.grpCCDScale.Controls.Add(Me.lblSceneID)
        Me.grpCCDScale.Controls.Add(Me.btnScalePR)
        Me.grpCCDScale.Controls.Add(Me.lblCCDPosZ)
        Me.grpCCDScale.Controls.Add(Me.txtA22)
        Me.grpCCDScale.Controls.Add(Me.txtCCDPosY)
        Me.grpCCDScale.Controls.Add(Me.lblScaleYY)
        Me.grpCCDScale.Controls.Add(Me.txtCCDPosX)
        Me.grpCCDScale.Controls.Add(Me.txtA11)
        Me.grpCCDScale.Controls.Add(Me.lblCCDPosY)
        Me.grpCCDScale.Controls.Add(Me.lblScaleXX)
        Me.grpCCDScale.Controls.Add(Me.lblCCDPosX)
        resources.ApplyResources(Me.grpCCDScale, "grpCCDScale")
        Me.grpCCDScale.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpCCDScale.Name = "grpCCDScale"
        Me.grpCCDScale.TabStop = False
        '
        'lblSceneSet
        '
        Me.lblSceneSet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        resources.ApplyResources(Me.lblSceneSet, "lblSceneSet")
        Me.lblSceneSet.Name = "lblSceneSet"
        '
        'btnTrainScene
        '
        Me.btnTrainScene.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.setup1
        resources.ApplyResources(Me.btnTrainScene, "btnTrainScene")
        Me.btnTrainScene.FlatAppearance.BorderSize = 0
        Me.btnTrainScene.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnTrainScene.Name = "btnTrainScene"
        Me.btnTrainScene.UseVisualStyleBackColor = True
        '
        'lblScaleYYUnit
        '
        resources.ApplyResources(Me.lblScaleYYUnit, "lblScaleYYUnit")
        Me.lblScaleYYUnit.Name = "lblScaleYYUnit"
        '
        'lblScaleYXUnit
        '
        resources.ApplyResources(Me.lblScaleYXUnit, "lblScaleYXUnit")
        Me.lblScaleYXUnit.Name = "lblScaleYXUnit"
        '
        'btnGoCCD
        '
        resources.ApplyResources(Me.btnGoCCD, "btnGoCCD")
        Me.btnGoCCD.FlatAppearance.BorderSize = 0
        Me.btnGoCCD.Name = "btnGoCCD"
        Me.btnGoCCD.UseVisualStyleBackColor = True
        '
        'lblScaleXYUnit
        '
        resources.ApplyResources(Me.lblScaleXYUnit, "lblScaleXYUnit")
        Me.lblScaleXYUnit.Name = "lblScaleXYUnit"
        '
        'btnSetCcdPosX
        '
        Me.btnSetCcdPosX.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetCcdPosX.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        resources.ApplyResources(Me.btnSetCcdPosX, "btnSetCcdPosX")
        Me.btnSetCcdPosX.FlatAppearance.BorderSize = 0
        Me.btnSetCcdPosX.Name = "btnSetCcdPosX"
        Me.btnSetCcdPosX.UseVisualStyleBackColor = True
        '
        'lblScaleXXUnit
        '
        resources.ApplyResources(Me.lblScaleXXUnit, "lblScaleXXUnit")
        Me.lblScaleXXUnit.Name = "lblScaleXXUnit"
        '
        'lblCCDPosXUnit
        '
        Me.lblCCDPosXUnit.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblCCDPosXUnit, "lblCCDPosXUnit")
        Me.lblCCDPosXUnit.Name = "lblCCDPosXUnit"
        '
        'lblScaleYX
        '
        resources.ApplyResources(Me.lblScaleYX, "lblScaleYX")
        Me.lblScaleYX.Name = "lblScaleYX"
        '
        'lblCCDPosYUnit
        '
        Me.lblCCDPosYUnit.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblCCDPosYUnit, "lblCCDPosYUnit")
        Me.lblCCDPosYUnit.Name = "lblCCDPosYUnit"
        '
        'txtA21
        '
        resources.ApplyResources(Me.txtA21, "txtA21")
        Me.txtA21.Name = "txtA21"
        '
        'lblCCDPosZUnit
        '
        Me.lblCCDPosZUnit.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblCCDPosZUnit, "lblCCDPosZUnit")
        Me.lblCCDPosZUnit.Name = "lblCCDPosZUnit"
        '
        'txtA12
        '
        resources.ApplyResources(Me.txtA12, "txtA12")
        Me.txtA12.Name = "txtA12"
        '
        'txtCCDPosZ
        '
        Me.txtCCDPosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtCCDPosZ, "txtCCDPosZ")
        Me.txtCCDPosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosZ.Name = "txtCCDPosZ"
        '
        'lblScaleXY
        '
        resources.ApplyResources(Me.lblScaleXY, "lblScaleXY")
        Me.lblScaleXY.Name = "lblScaleXY"
        '
        'lblSceneID
        '
        Me.lblSceneID.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblSceneID, "lblSceneID")
        Me.lblSceneID.Name = "lblSceneID"
        '
        'btnScalePR
        '
        resources.ApplyResources(Me.btnScalePR, "btnScalePR")
        Me.btnScalePR.FlatAppearance.BorderSize = 0
        Me.btnScalePR.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnScalePR.Name = "btnScalePR"
        Me.btnScalePR.UseVisualStyleBackColor = True
        '
        'lblCCDPosZ
        '
        Me.lblCCDPosZ.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblCCDPosZ, "lblCCDPosZ")
        Me.lblCCDPosZ.Name = "lblCCDPosZ"
        '
        'txtA22
        '
        resources.ApplyResources(Me.txtA22, "txtA22")
        Me.txtA22.Name = "txtA22"
        '
        'txtCCDPosY
        '
        Me.txtCCDPosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtCCDPosY, "txtCCDPosY")
        Me.txtCCDPosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosY.Name = "txtCCDPosY"
        '
        'lblScaleYY
        '
        resources.ApplyResources(Me.lblScaleYY, "lblScaleYY")
        Me.lblScaleYY.Name = "lblScaleYY"
        '
        'txtCCDPosX
        '
        Me.txtCCDPosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtCCDPosX, "txtCCDPosX")
        Me.txtCCDPosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosX.Name = "txtCCDPosX"
        '
        'txtA11
        '
        resources.ApplyResources(Me.txtA11, "txtA11")
        Me.txtA11.Name = "txtA11"
        '
        'lblCCDPosY
        '
        Me.lblCCDPosY.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblCCDPosY, "lblCCDPosY")
        Me.lblCCDPosY.Name = "lblCCDPosY"
        '
        'lblScaleXX
        '
        resources.ApplyResources(Me.lblScaleXX, "lblScaleXX")
        Me.lblScaleXX.Name = "lblScaleXX"
        '
        'lblCCDPosX
        '
        Me.lblCCDPosX.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblCCDPosX, "lblCCDPosX")
        Me.lblCCDPosX.Name = "lblCCDPosX"
        '
        'btnOK
        '
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnOK.Name = "btnOK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'lblSteadyTime
        '
        Me.lblSteadyTime.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblSteadyTime, "lblSteadyTime")
        Me.lblSteadyTime.Name = "lblSteadyTime"
        '
        'NumSteadyTime
        '
        resources.ApplyResources(Me.NumSteadyTime, "NumSteadyTime")
        Me.NumSteadyTime.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumSteadyTime.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.NumSteadyTime.Name = "NumSteadyTime"
        Me.NumSteadyTime.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'frmCalibrationCCDImage
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ControlBox = False
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.grpCCDScale)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.grpCalibration)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.UcJoyStick1)
        Me.Controls.Add(Me.btnAWB)
        Me.Controls.Add(Me.UcLightControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCalibrationCCDImage"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nmcExposure, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCalibration.ResumeLayout(False)
        Me.grpCalibration.PerformLayout()
        CType(Me.numTileSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageCCD.ResumeLayout(False)
        Me.TabPageCalib.ResumeLayout(False)
        Me.grpCCDScale.ResumeLayout(False)
        Me.grpCCDScale.PerformLayout()
        CType(Me.NumSteadyTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcLightControl1 As ucLightControl
    Friend WithEvents btnAWB As System.Windows.Forms.Button
    Friend WithEvents UcJoyStick1 As ucJoyStick
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents nmcExposure As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblExposureTimeUnit As System.Windows.Forms.Label
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents grpCalibration As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnCalibration As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents numTileSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageCCD As System.Windows.Forms.TabPage
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents TabPageCalib As System.Windows.Forms.TabPage
    Friend WithEvents UcDisplay2 As ProjectAOI.ucDisplay
    Friend WithEvents grpCCDScale As System.Windows.Forms.GroupBox
    Friend WithEvents lblScaleYYUnit As System.Windows.Forms.Label
    Friend WithEvents lblScaleYXUnit As System.Windows.Forms.Label
    Friend WithEvents lblScaleXYUnit As System.Windows.Forms.Label
    Friend WithEvents lblScaleXXUnit As System.Windows.Forms.Label
    Friend WithEvents lblScaleYX As System.Windows.Forms.Label
    Friend WithEvents txtA21 As System.Windows.Forms.TextBox
    Friend WithEvents txtA12 As System.Windows.Forms.TextBox
    Friend WithEvents lblScaleXY As System.Windows.Forms.Label
    Friend WithEvents btnScalePR As System.Windows.Forms.Button
    Friend WithEvents txtA22 As System.Windows.Forms.TextBox
    Friend WithEvents lblScaleYY As System.Windows.Forms.Label
    Friend WithEvents txtA11 As System.Windows.Forms.TextBox
    Friend WithEvents lblScaleXX As System.Windows.Forms.Label
    Friend WithEvents btnTrainScene As System.Windows.Forms.Button
    Friend WithEvents btnGoCCD As System.Windows.Forms.Button
    Friend WithEvents btnSetCcdPosX As System.Windows.Forms.Button
    Friend WithEvents lblCCDPosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosZUnit As System.Windows.Forms.Label
    Friend WithEvents txtCCDPosZ As System.Windows.Forms.TextBox
    Friend WithEvents lblSceneID As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosZ As System.Windows.Forms.Label
    Friend WithEvents txtCCDPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCCDPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblCCDPosY As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosX As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblSceneSet As System.Windows.Forms.Label
    Friend WithEvents lblSteadyTime As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents NumSteadyTime As System.Windows.Forms.NumericUpDown
End Class

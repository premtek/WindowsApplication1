<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEngineMode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEngineMode))
        Me.btnManual = New System.Windows.Forms.Button()
        Me.btnIOList = New System.Windows.Forms.Button()
        Me.btnSystemSet = New System.Windows.Forms.Button()
        Me.btnSetUserLevel = New System.Windows.Forms.Button()
        Me.btnIndicator = New System.Windows.Forms.Button()
        Me.btnScale = New System.Windows.Forms.Button()
        Me.btnLight = New System.Windows.Forms.Button()
        Me.btnIOSet = New System.Windows.Forms.Button()
        Me.btnSetCCD = New System.Windows.Forms.Button()
        Me.btnSetLaserReader = New System.Windows.Forms.Button()
        Me.btnSetTriggerController = New System.Windows.Forms.Button()
        Me.btnSetFMCS = New System.Windows.Forms.Button()
        Me.btnValveContler = New System.Windows.Forms.Button()
        Me.btnSetMessage = New System.Windows.Forms.Button()
        Me.btnSetInterlock = New System.Windows.Forms.Button()
        Me.btnConveyorManual = New System.Windows.Forms.Button()
        Me.btnVacuumKit = New System.Windows.Forms.Button()
        Me.btnElectricCylinder = New System.Windows.Forms.Button()
        Me.btnSetEPV = New System.Windows.Forms.Button()
        Me.btnSetTemp = New System.Windows.Forms.Button()
        Me.btnStageSafe = New System.Windows.Forms.Button()
        Me.btnAsaTest = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnBack = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnManual
        '
        resources.ApplyResources(Me.btnManual, "btnManual")
        Me.btnManual.BackColor = System.Drawing.SystemColors.Control
        Me.btnManual.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnManual.FlatAppearance.BorderSize = 0
        Me.btnManual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnManual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnManual.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Tag = "0"
        Me.ToolTip.SetToolTip(Me.btnManual, resources.GetString("btnManual.ToolTip"))
        Me.btnManual.UseVisualStyleBackColor = True
        '
        'btnIOList
        '
        resources.ApplyResources(Me.btnIOList, "btnIOList")
        Me.btnIOList.BackColor = System.Drawing.SystemColors.Control
        Me.btnIOList.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnIOList.FlatAppearance.BorderSize = 0
        Me.btnIOList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnIOList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnIOList.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnIOList.Name = "btnIOList"
        Me.btnIOList.Tag = "3"
        Me.ToolTip.SetToolTip(Me.btnIOList, resources.GetString("btnIOList.ToolTip"))
        Me.btnIOList.UseVisualStyleBackColor = True
        '
        'btnSystemSet
        '
        resources.ApplyResources(Me.btnSystemSet, "btnSystemSet")
        Me.btnSystemSet.BackColor = System.Drawing.SystemColors.Control
        Me.btnSystemSet.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnSystemSet.FlatAppearance.BorderSize = 0
        Me.btnSystemSet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnSystemSet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnSystemSet.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSystemSet.Name = "btnSystemSet"
        Me.btnSystemSet.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnSystemSet, resources.GetString("btnSystemSet.ToolTip"))
        Me.btnSystemSet.UseVisualStyleBackColor = True
        '
        'btnSetUserLevel
        '
        resources.ApplyResources(Me.btnSetUserLevel, "btnSetUserLevel")
        Me.btnSetUserLevel.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetUserLevel.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnSetUserLevel.FlatAppearance.BorderSize = 0
        Me.btnSetUserLevel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnSetUserLevel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnSetUserLevel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetUserLevel.Name = "btnSetUserLevel"
        Me.btnSetUserLevel.Tag = "6"
        Me.ToolTip.SetToolTip(Me.btnSetUserLevel, resources.GetString("btnSetUserLevel.ToolTip"))
        Me.btnSetUserLevel.UseVisualStyleBackColor = True
        '
        'btnIndicator
        '
        resources.ApplyResources(Me.btnIndicator, "btnIndicator")
        Me.btnIndicator.BackColor = System.Drawing.SystemColors.Control
        Me.btnIndicator.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnIndicator.FlatAppearance.BorderSize = 0
        Me.btnIndicator.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnIndicator.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnIndicator.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnIndicator.Name = "btnIndicator"
        Me.btnIndicator.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnIndicator, resources.GetString("btnIndicator.ToolTip"))
        Me.btnIndicator.UseVisualStyleBackColor = True
        '
        'btnScale
        '
        resources.ApplyResources(Me.btnScale, "btnScale")
        Me.btnScale.BackColor = System.Drawing.SystemColors.Control
        Me.btnScale.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnScale.FlatAppearance.BorderSize = 0
        Me.btnScale.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnScale.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnScale.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnScale.Name = "btnScale"
        Me.btnScale.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnScale, resources.GetString("btnScale.ToolTip"))
        Me.btnScale.UseVisualStyleBackColor = True
        '
        'btnLight
        '
        resources.ApplyResources(Me.btnLight, "btnLight")
        Me.btnLight.BackColor = System.Drawing.SystemColors.Control
        Me.btnLight.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnLight.FlatAppearance.BorderSize = 0
        Me.btnLight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnLight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnLight.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnLight.Name = "btnLight"
        Me.btnLight.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnLight, resources.GetString("btnLight.ToolTip"))
        Me.btnLight.UseVisualStyleBackColor = True
        '
        'btnIOSet
        '
        resources.ApplyResources(Me.btnIOSet, "btnIOSet")
        Me.btnIOSet.BackColor = System.Drawing.SystemColors.Control
        Me.btnIOSet.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnIOSet.FlatAppearance.BorderSize = 0
        Me.btnIOSet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnIOSet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnIOSet.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnIOSet.Name = "btnIOSet"
        Me.btnIOSet.Tag = "4"
        Me.ToolTip.SetToolTip(Me.btnIOSet, resources.GetString("btnIOSet.ToolTip"))
        Me.btnIOSet.UseVisualStyleBackColor = True
        '
        'btnSetCCD
        '
        resources.ApplyResources(Me.btnSetCCD, "btnSetCCD")
        Me.btnSetCCD.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetCCD.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnSetCCD.FlatAppearance.BorderSize = 0
        Me.btnSetCCD.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnSetCCD.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnSetCCD.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetCCD.Name = "btnSetCCD"
        Me.btnSetCCD.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnSetCCD, resources.GetString("btnSetCCD.ToolTip"))
        Me.btnSetCCD.UseVisualStyleBackColor = True
        '
        'btnSetLaserReader
        '
        resources.ApplyResources(Me.btnSetLaserReader, "btnSetLaserReader")
        Me.btnSetLaserReader.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetLaserReader.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnSetLaserReader.FlatAppearance.BorderSize = 0
        Me.btnSetLaserReader.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnSetLaserReader.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnSetLaserReader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetLaserReader.Name = "btnSetLaserReader"
        Me.btnSetLaserReader.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnSetLaserReader, resources.GetString("btnSetLaserReader.ToolTip"))
        Me.btnSetLaserReader.UseVisualStyleBackColor = True
        '
        'btnSetTriggerController
        '
        resources.ApplyResources(Me.btnSetTriggerController, "btnSetTriggerController")
        Me.btnSetTriggerController.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetTriggerController.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnSetTriggerController.FlatAppearance.BorderSize = 0
        Me.btnSetTriggerController.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnSetTriggerController.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnSetTriggerController.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetTriggerController.Name = "btnSetTriggerController"
        Me.btnSetTriggerController.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnSetTriggerController, resources.GetString("btnSetTriggerController.ToolTip"))
        Me.btnSetTriggerController.UseVisualStyleBackColor = True
        '
        'btnSetFMCS
        '
        resources.ApplyResources(Me.btnSetFMCS, "btnSetFMCS")
        Me.btnSetFMCS.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetFMCS.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnSetFMCS.FlatAppearance.BorderSize = 0
        Me.btnSetFMCS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnSetFMCS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnSetFMCS.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetFMCS.Name = "btnSetFMCS"
        Me.btnSetFMCS.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnSetFMCS, resources.GetString("btnSetFMCS.ToolTip"))
        Me.btnSetFMCS.UseVisualStyleBackColor = True
        '
        'btnValveContler
        '
        resources.ApplyResources(Me.btnValveContler, "btnValveContler")
        Me.btnValveContler.BackColor = System.Drawing.SystemColors.Control
        Me.btnValveContler.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnValveContler.FlatAppearance.BorderSize = 0
        Me.btnValveContler.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnValveContler.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnValveContler.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnValveContler.Name = "btnValveContler"
        Me.btnValveContler.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnValveContler, resources.GetString("btnValveContler.ToolTip"))
        Me.btnValveContler.UseVisualStyleBackColor = True
        '
        'btnSetMessage
        '
        resources.ApplyResources(Me.btnSetMessage, "btnSetMessage")
        Me.btnSetMessage.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetMessage.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnSetMessage.FlatAppearance.BorderSize = 0
        Me.btnSetMessage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnSetMessage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnSetMessage.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetMessage.Name = "btnSetMessage"
        Me.btnSetMessage.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnSetMessage, resources.GetString("btnSetMessage.ToolTip"))
        Me.btnSetMessage.UseVisualStyleBackColor = True
        '
        'btnSetInterlock
        '
        resources.ApplyResources(Me.btnSetInterlock, "btnSetInterlock")
        Me.btnSetInterlock.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetInterlock.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnSetInterlock.FlatAppearance.BorderSize = 0
        Me.btnSetInterlock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnSetInterlock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnSetInterlock.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetInterlock.Name = "btnSetInterlock"
        Me.btnSetInterlock.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnSetInterlock, resources.GetString("btnSetInterlock.ToolTip"))
        Me.btnSetInterlock.UseVisualStyleBackColor = True
        '
        'btnConveyorManual
        '
        resources.ApplyResources(Me.btnConveyorManual, "btnConveyorManual")
        Me.btnConveyorManual.BackColor = System.Drawing.SystemColors.Control
        Me.btnConveyorManual.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnConveyorManual.FlatAppearance.BorderSize = 0
        Me.btnConveyorManual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnConveyorManual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnConveyorManual.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnConveyorManual.Name = "btnConveyorManual"
        Me.btnConveyorManual.Tag = "0"
        Me.ToolTip.SetToolTip(Me.btnConveyorManual, resources.GetString("btnConveyorManual.ToolTip"))
        Me.btnConveyorManual.UseVisualStyleBackColor = True
        '
        'btnVacuumKit
        '
        resources.ApplyResources(Me.btnVacuumKit, "btnVacuumKit")
        Me.btnVacuumKit.BackColor = System.Drawing.SystemColors.Control
        Me.btnVacuumKit.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnVacuumKit.FlatAppearance.BorderSize = 0
        Me.btnVacuumKit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnVacuumKit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnVacuumKit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnVacuumKit.Name = "btnVacuumKit"
        Me.btnVacuumKit.Tag = "0"
        Me.ToolTip.SetToolTip(Me.btnVacuumKit, resources.GetString("btnVacuumKit.ToolTip"))
        Me.btnVacuumKit.UseVisualStyleBackColor = True
        '
        'btnElectricCylinder
        '
        resources.ApplyResources(Me.btnElectricCylinder, "btnElectricCylinder")
        Me.btnElectricCylinder.BackColor = System.Drawing.SystemColors.Control
        Me.btnElectricCylinder.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnElectricCylinder.FlatAppearance.BorderSize = 0
        Me.btnElectricCylinder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnElectricCylinder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnElectricCylinder.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnElectricCylinder.Name = "btnElectricCylinder"
        Me.btnElectricCylinder.Tag = "0"
        Me.ToolTip.SetToolTip(Me.btnElectricCylinder, resources.GetString("btnElectricCylinder.ToolTip"))
        Me.btnElectricCylinder.UseVisualStyleBackColor = True
        '
        'btnSetEPV
        '
        resources.ApplyResources(Me.btnSetEPV, "btnSetEPV")
        Me.btnSetEPV.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetEPV.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnSetEPV.FlatAppearance.BorderSize = 0
        Me.btnSetEPV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnSetEPV.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnSetEPV.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetEPV.Name = "btnSetEPV"
        Me.btnSetEPV.Tag = "7"
        Me.ToolTip.SetToolTip(Me.btnSetEPV, resources.GetString("btnSetEPV.ToolTip"))
        Me.btnSetEPV.UseVisualStyleBackColor = True
        '
        'btnSetTemp
        '
        resources.ApplyResources(Me.btnSetTemp, "btnSetTemp")
        Me.btnSetTemp.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetTemp.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnSetTemp.FlatAppearance.BorderSize = 0
        Me.btnSetTemp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnSetTemp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnSetTemp.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetTemp.Name = "btnSetTemp"
        Me.btnSetTemp.Tag = "0"
        Me.ToolTip.SetToolTip(Me.btnSetTemp, resources.GetString("btnSetTemp.ToolTip"))
        Me.btnSetTemp.UseVisualStyleBackColor = True
        '
        'btnStageSafe
        '
        resources.ApplyResources(Me.btnStageSafe, "btnStageSafe")
        Me.btnStageSafe.BackColor = System.Drawing.SystemColors.Control
        Me.btnStageSafe.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnStageSafe.FlatAppearance.BorderSize = 0
        Me.btnStageSafe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnStageSafe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnStageSafe.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnStageSafe.Name = "btnStageSafe"
        Me.btnStageSafe.Tag = "0"
        Me.ToolTip.SetToolTip(Me.btnStageSafe, resources.GetString("btnStageSafe.ToolTip"))
        Me.btnStageSafe.UseVisualStyleBackColor = True
        '
        'btnAsaTest
        '
        resources.ApplyResources(Me.btnAsaTest, "btnAsaTest")
        Me.btnAsaTest.BackColor = System.Drawing.SystemColors.Control
        Me.btnAsaTest.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnAsaTest.FlatAppearance.BorderSize = 0
        Me.btnAsaTest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnAsaTest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnAsaTest.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnAsaTest.Name = "btnAsaTest"
        Me.btnAsaTest.Tag = "0"
        Me.ToolTip.SetToolTip(Me.btnAsaTest, resources.GetString("btnAsaTest.ToolTip"))
        Me.btnAsaTest.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.btnSetCCD)
        Me.GroupBox1.Controls.Add(Me.btnScale)
        Me.GroupBox1.Controls.Add(Me.btnLight)
        Me.GroupBox1.Controls.Add(Me.btnSetLaserReader)
        Me.GroupBox1.Controls.Add(Me.btnSetEPV)
        Me.GroupBox1.Controls.Add(Me.btnSetTriggerController)
        Me.GroupBox1.Controls.Add(Me.btnSetFMCS)
        Me.GroupBox1.Controls.Add(Me.btnValveContler)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'GroupBox3
        '
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Controls.Add(Me.btnSetTemp)
        Me.GroupBox3.Controls.Add(Me.btnVacuumKit)
        Me.GroupBox3.Controls.Add(Me.btnElectricCylinder)
        Me.GroupBox3.Controls.Add(Me.btnConveyorManual)
        Me.GroupBox3.Controls.Add(Me.btnAsaTest)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        Me.ToolTip.SetToolTip(Me.GroupBox3, resources.GetString("GroupBox3.ToolTip"))
        '
        'GroupBox4
        '
        resources.ApplyResources(Me.GroupBox4, "GroupBox4")
        Me.GroupBox4.Controls.Add(Me.btnManual)
        Me.GroupBox4.Controls.Add(Me.btnIOSet)
        Me.GroupBox4.Controls.Add(Me.btnIOList)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.TabStop = False
        Me.ToolTip.SetToolTip(Me.GroupBox4, resources.GetString("GroupBox4.ToolTip"))
        '
        'GroupBox5
        '
        resources.ApplyResources(Me.GroupBox5, "GroupBox5")
        Me.GroupBox5.Controls.Add(Me.btnSystemSet)
        Me.GroupBox5.Controls.Add(Me.btnSetUserLevel)
        Me.GroupBox5.Controls.Add(Me.btnIndicator)
        Me.GroupBox5.Controls.Add(Me.btnSetMessage)
        Me.GroupBox5.Controls.Add(Me.btnSetInterlock)
        Me.GroupBox5.Controls.Add(Me.btnStageSafe)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.TabStop = False
        Me.ToolTip.SetToolTip(Me.GroupBox5, resources.GetString("GroupBox5.ToolTip"))
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
        Me.ToolTip.SetToolTip(Me.btnBack, resources.GetString("btnBack.ToolTip"))
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'frmEngineMode
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ControlBox = False
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEngineMode"
        Me.ToolTip.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnManual As System.Windows.Forms.Button
    Friend WithEvents btnIOList As System.Windows.Forms.Button
    Friend WithEvents btnSystemSet As System.Windows.Forms.Button
    Friend WithEvents btnSetUserLevel As System.Windows.Forms.Button
    Friend WithEvents btnIndicator As System.Windows.Forms.Button
    Friend WithEvents btnScale As System.Windows.Forms.Button
    Friend WithEvents btnLight As System.Windows.Forms.Button
    Friend WithEvents btnIOSet As System.Windows.Forms.Button
    Friend WithEvents btnSetCCD As System.Windows.Forms.Button
    Friend WithEvents btnSetLaserReader As System.Windows.Forms.Button
    Friend WithEvents btnSetTriggerController As System.Windows.Forms.Button
    Friend WithEvents btnSetFMCS As System.Windows.Forms.Button
    Friend WithEvents btnValveContler As System.Windows.Forms.Button
    Friend WithEvents btnSetMessage As System.Windows.Forms.Button
    Friend WithEvents btnSetInterlock As System.Windows.Forms.Button
    Friend WithEvents btnConveyorManual As System.Windows.Forms.Button
    Friend WithEvents btnVacuumKit As System.Windows.Forms.Button
    Friend WithEvents btnElectricCylinder As System.Windows.Forms.Button
    Friend WithEvents btnSetEPV As System.Windows.Forms.Button
    Friend WithEvents btnSetTemp As System.Windows.Forms.Button
    Friend WithEvents btnStageSafe As System.Windows.Forms.Button
    Friend WithEvents btnAsaTest As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
End Class

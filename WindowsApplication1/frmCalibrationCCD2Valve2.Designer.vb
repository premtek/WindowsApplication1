<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalibrationCCD2Valve2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalibrationCCD2Valve2))
        Me.btnSetCcdPosX = New System.Windows.Forms.Button()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.btnSetValvePosX = New System.Windows.Forms.Button()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.txtTeachCCD2X = New System.Windows.Forms.TextBox()
        Me.txtTeachCCD2Z = New System.Windows.Forms.TextBox()
        Me.txtTeachDispenser2X = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnGo1 = New System.Windows.Forms.Button()
        Me.txtTeachCCD2Y = New System.Windows.Forms.TextBox()
        Me.txtTeachDispenser2Y = New System.Windows.Forms.TextBox()
        Me.txtTeachDispenser2Z = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnGo2 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnGlueNo1SyringePressure = New System.Windows.Forms.Button()
        Me.btnDispenserNo1EPRegulator = New System.Windows.Forms.Button()
        Me.txtDispenserNo1EPRegulatorAO = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblDispenserNo1EPRegulator = New System.Windows.Forms.Label()
        Me.btnAutoCalibrationSetup = New System.Windows.Forms.Button()
        Me.btnNextPage = New System.Windows.Forms.Button()
        Me.btnValve2On = New System.Windows.Forms.Button()
        Me.btnPrevPage = New System.Windows.Forms.Button()
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.UcCalibrationStatus1 = New WindowsApplication1.ucCalibrationStatus()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSetCcdPosX
        '
        resources.ApplyResources(Me.btnSetCcdPosX, "btnSetCcdPosX")
        Me.btnSetCcdPosX.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetCcdPosX.FlatAppearance.BorderSize = 0
        Me.btnSetCcdPosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetCcdPosX.Name = "btnSetCcdPosX"
        Me.ToolTip1.SetToolTip(Me.btnSetCcdPosX, resources.GetString("btnSetCcdPosX.ToolTip"))
        Me.btnSetCcdPosX.UseVisualStyleBackColor = True
        '
        'Label50
        '
        resources.ApplyResources(Me.Label50, "Label50")
        Me.Label50.Name = "Label50"
        Me.ToolTip1.SetToolTip(Me.Label50, resources.GetString("Label50.ToolTip"))
        '
        'Label51
        '
        resources.ApplyResources(Me.Label51, "Label51")
        Me.Label51.Name = "Label51"
        Me.ToolTip1.SetToolTip(Me.Label51, resources.GetString("Label51.ToolTip"))
        '
        'Label52
        '
        resources.ApplyResources(Me.Label52, "Label52")
        Me.Label52.Name = "Label52"
        Me.ToolTip1.SetToolTip(Me.Label52, resources.GetString("Label52.ToolTip"))
        '
        'btnSetValvePosX
        '
        resources.ApplyResources(Me.btnSetValvePosX, "btnSetValvePosX")
        Me.btnSetValvePosX.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetValvePosX.FlatAppearance.BorderSize = 0
        Me.btnSetValvePosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetValvePosX.Name = "btnSetValvePosX"
        Me.ToolTip1.SetToolTip(Me.btnSetValvePosX, resources.GetString("btnSetValvePosX.ToolTip"))
        Me.btnSetValvePosX.UseVisualStyleBackColor = True
        '
        'Label58
        '
        resources.ApplyResources(Me.Label58, "Label58")
        Me.Label58.Name = "Label58"
        Me.ToolTip1.SetToolTip(Me.Label58, resources.GetString("Label58.ToolTip"))
        '
        'Label59
        '
        resources.ApplyResources(Me.Label59, "Label59")
        Me.Label59.Name = "Label59"
        Me.ToolTip1.SetToolTip(Me.Label59, resources.GetString("Label59.ToolTip"))
        '
        'Label60
        '
        resources.ApplyResources(Me.Label60, "Label60")
        Me.Label60.Name = "Label60"
        Me.ToolTip1.SetToolTip(Me.Label60, resources.GetString("Label60.ToolTip"))
        '
        'Label48
        '
        resources.ApplyResources(Me.Label48, "Label48")
        Me.Label48.Name = "Label48"
        Me.ToolTip1.SetToolTip(Me.Label48, resources.GetString("Label48.ToolTip"))
        '
        'Label49
        '
        resources.ApplyResources(Me.Label49, "Label49")
        Me.Label49.Name = "Label49"
        Me.ToolTip1.SetToolTip(Me.Label49, resources.GetString("Label49.ToolTip"))
        '
        'Label57
        '
        resources.ApplyResources(Me.Label57, "Label57")
        Me.Label57.Name = "Label57"
        Me.ToolTip1.SetToolTip(Me.Label57, resources.GetString("Label57.ToolTip"))
        '
        'Label53
        '
        resources.ApplyResources(Me.Label53, "Label53")
        Me.Label53.Name = "Label53"
        Me.ToolTip1.SetToolTip(Me.Label53, resources.GetString("Label53.ToolTip"))
        '
        'Label54
        '
        resources.ApplyResources(Me.Label54, "Label54")
        Me.Label54.Name = "Label54"
        Me.ToolTip1.SetToolTip(Me.Label54, resources.GetString("Label54.ToolTip"))
        '
        'Label55
        '
        resources.ApplyResources(Me.Label55, "Label55")
        Me.Label55.Name = "Label55"
        Me.ToolTip1.SetToolTip(Me.Label55, resources.GetString("Label55.ToolTip"))
        '
        'txtTeachCCD2X
        '
        resources.ApplyResources(Me.txtTeachCCD2X, "txtTeachCCD2X")
        Me.txtTeachCCD2X.Name = "txtTeachCCD2X"
        Me.ToolTip1.SetToolTip(Me.txtTeachCCD2X, resources.GetString("txtTeachCCD2X.ToolTip"))
        '
        'txtTeachCCD2Z
        '
        resources.ApplyResources(Me.txtTeachCCD2Z, "txtTeachCCD2Z")
        Me.txtTeachCCD2Z.Name = "txtTeachCCD2Z"
        Me.ToolTip1.SetToolTip(Me.txtTeachCCD2Z, resources.GetString("txtTeachCCD2Z.ToolTip"))
        '
        'txtTeachDispenser2X
        '
        resources.ApplyResources(Me.txtTeachDispenser2X, "txtTeachDispenser2X")
        Me.txtTeachDispenser2X.Name = "txtTeachDispenser2X"
        Me.ToolTip1.SetToolTip(Me.txtTeachDispenser2X, resources.GetString("txtTeachDispenser2X.ToolTip"))
        '
        'GroupBox2
        '
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Controls.Add(Me.btnGo1)
        Me.GroupBox2.Controls.Add(Me.btnSetCcdPosX)
        Me.GroupBox2.Controls.Add(Me.Label50)
        Me.GroupBox2.Controls.Add(Me.Label51)
        Me.GroupBox2.Controls.Add(Me.Label52)
        Me.GroupBox2.Controls.Add(Me.Label53)
        Me.GroupBox2.Controls.Add(Me.Label54)
        Me.GroupBox2.Controls.Add(Me.Label55)
        Me.GroupBox2.Controls.Add(Me.txtTeachCCD2X)
        Me.GroupBox2.Controls.Add(Me.txtTeachCCD2Y)
        Me.GroupBox2.Controls.Add(Me.txtTeachCCD2Z)
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox2, resources.GetString("GroupBox2.ToolTip"))
        '
        'btnGo1
        '
        resources.ApplyResources(Me.btnGo1, "btnGo1")
        Me.btnGo1.FlatAppearance.BorderSize = 0
        Me.btnGo1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnGo1.Name = "btnGo1"
        Me.ToolTip1.SetToolTip(Me.btnGo1, resources.GetString("btnGo1.ToolTip"))
        Me.btnGo1.UseVisualStyleBackColor = True
        '
        'txtTeachCCD2Y
        '
        resources.ApplyResources(Me.txtTeachCCD2Y, "txtTeachCCD2Y")
        Me.txtTeachCCD2Y.Name = "txtTeachCCD2Y"
        Me.ToolTip1.SetToolTip(Me.txtTeachCCD2Y, resources.GetString("txtTeachCCD2Y.ToolTip"))
        '
        'txtTeachDispenser2Y
        '
        resources.ApplyResources(Me.txtTeachDispenser2Y, "txtTeachDispenser2Y")
        Me.txtTeachDispenser2Y.Name = "txtTeachDispenser2Y"
        Me.ToolTip1.SetToolTip(Me.txtTeachDispenser2Y, resources.GetString("txtTeachDispenser2Y.ToolTip"))
        '
        'txtTeachDispenser2Z
        '
        resources.ApplyResources(Me.txtTeachDispenser2Z, "txtTeachDispenser2Z")
        Me.txtTeachDispenser2Z.Name = "txtTeachDispenser2Z"
        Me.ToolTip1.SetToolTip(Me.txtTeachDispenser2Z, resources.GetString("txtTeachDispenser2Z.ToolTip"))
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.btnGo2)
        Me.GroupBox1.Controls.Add(Me.btnSetValvePosX)
        Me.GroupBox1.Controls.Add(Me.Label58)
        Me.GroupBox1.Controls.Add(Me.Label59)
        Me.GroupBox1.Controls.Add(Me.Label60)
        Me.GroupBox1.Controls.Add(Me.Label48)
        Me.GroupBox1.Controls.Add(Me.Label49)
        Me.GroupBox1.Controls.Add(Me.Label57)
        Me.GroupBox1.Controls.Add(Me.txtTeachDispenser2X)
        Me.GroupBox1.Controls.Add(Me.txtTeachDispenser2Y)
        Me.GroupBox1.Controls.Add(Me.txtTeachDispenser2Z)
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'btnGo2
        '
        resources.ApplyResources(Me.btnGo2, "btnGo2")
        Me.btnGo2.FlatAppearance.BorderSize = 0
        Me.btnGo2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnGo2.Name = "btnGo2"
        Me.ToolTip1.SetToolTip(Me.btnGo2, resources.GetString("btnGo2.ToolTip"))
        Me.btnGo2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.UcDisplay1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.UcCalibrationStatus1, 0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.ToolTip1.SetToolTip(Me.TableLayoutPanel1, resources.GetString("TableLayoutPanel1.ToolTip"))
        '
        'UcDisplay1
        '
        resources.ApplyResources(Me.UcDisplay1, "UcDisplay1")
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UcDisplay1.Name = "UcDisplay1"
        Me.ToolTip1.SetToolTip(Me.UcDisplay1, resources.GetString("UcDisplay1.ToolTip"))
        '
        'TableLayoutPanel2
        '
        resources.ApplyResources(Me.TableLayoutPanel2, "TableLayoutPanel2")
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox2, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox1, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.UcJoyStick1, 0, 2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.ToolTip1.SetToolTip(Me.TableLayoutPanel2, resources.GetString("TableLayoutPanel2.ToolTip"))
        '
        'Panel1
        '
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Controls.Add(Me.btnGlueNo1SyringePressure)
        Me.Panel1.Controls.Add(Me.btnDispenserNo1EPRegulator)
        Me.Panel1.Controls.Add(Me.txtDispenserNo1EPRegulatorAO)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lblDispenserNo1EPRegulator)
        Me.Panel1.Controls.Add(Me.btnAutoCalibrationSetup)
        Me.Panel1.Controls.Add(Me.btnNextPage)
        Me.Panel1.Controls.Add(Me.btnValve2On)
        Me.Panel1.Controls.Add(Me.btnPrevPage)
        Me.Panel1.Name = "Panel1"
        Me.ToolTip1.SetToolTip(Me.Panel1, resources.GetString("Panel1.ToolTip"))
        '
        'btnGlueNo1SyringePressure
        '
        resources.ApplyResources(Me.btnGlueNo1SyringePressure, "btnGlueNo1SyringePressure")
        Me.btnGlueNo1SyringePressure.BackColor = System.Drawing.SystemColors.Control
        Me.btnGlueNo1SyringePressure.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnGlueNo1SyringePressure.FlatAppearance.BorderSize = 0
        Me.btnGlueNo1SyringePressure.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnGlueNo1SyringePressure.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnGlueNo1SyringePressure.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnGlueNo1SyringePressure.Name = "btnGlueNo1SyringePressure"
        Me.btnGlueNo1SyringePressure.Tag = "7"
        Me.ToolTip1.SetToolTip(Me.btnGlueNo1SyringePressure, resources.GetString("btnGlueNo1SyringePressure.ToolTip"))
        Me.btnGlueNo1SyringePressure.UseVisualStyleBackColor = True
        '
        'btnDispenserNo1EPRegulator
        '
        resources.ApplyResources(Me.btnDispenserNo1EPRegulator, "btnDispenserNo1EPRegulator")
        Me.btnDispenserNo1EPRegulator.BackColor = System.Drawing.SystemColors.Control
        Me.btnDispenserNo1EPRegulator.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnDispenserNo1EPRegulator.FlatAppearance.BorderSize = 0
        Me.btnDispenserNo1EPRegulator.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnDispenserNo1EPRegulator.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnDispenserNo1EPRegulator.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnDispenserNo1EPRegulator.Name = "btnDispenserNo1EPRegulator"
        Me.btnDispenserNo1EPRegulator.Tag = "7"
        Me.ToolTip1.SetToolTip(Me.btnDispenserNo1EPRegulator, resources.GetString("btnDispenserNo1EPRegulator.ToolTip"))
        Me.btnDispenserNo1EPRegulator.UseVisualStyleBackColor = True
        '
        'txtDispenserNo1EPRegulatorAO
        '
        resources.ApplyResources(Me.txtDispenserNo1EPRegulatorAO, "txtDispenserNo1EPRegulatorAO")
        Me.txtDispenserNo1EPRegulatorAO.BackColor = System.Drawing.Color.White
        Me.txtDispenserNo1EPRegulatorAO.Name = "txtDispenserNo1EPRegulatorAO"
        Me.ToolTip1.SetToolTip(Me.txtDispenserNo1EPRegulatorAO, resources.GetString("txtDispenserNo1EPRegulatorAO.ToolTip"))
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'lblDispenserNo1EPRegulator
        '
        resources.ApplyResources(Me.lblDispenserNo1EPRegulator, "lblDispenserNo1EPRegulator")
        Me.lblDispenserNo1EPRegulator.BackColor = System.Drawing.Color.Transparent
        Me.lblDispenserNo1EPRegulator.Name = "lblDispenserNo1EPRegulator"
        Me.ToolTip1.SetToolTip(Me.lblDispenserNo1EPRegulator, resources.GetString("lblDispenserNo1EPRegulator.ToolTip"))
        '
        'btnAutoCalibrationSetup
        '
        resources.ApplyResources(Me.btnAutoCalibrationSetup, "btnAutoCalibrationSetup")
        Me.btnAutoCalibrationSetup.FlatAppearance.BorderSize = 0
        Me.btnAutoCalibrationSetup.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnAutoCalibrationSetup.Name = "btnAutoCalibrationSetup"
        Me.ToolTip1.SetToolTip(Me.btnAutoCalibrationSetup, resources.GetString("btnAutoCalibrationSetup.ToolTip"))
        Me.btnAutoCalibrationSetup.UseVisualStyleBackColor = True
        '
        'btnNextPage
        '
        resources.ApplyResources(Me.btnNextPage, "btnNextPage")
        Me.btnNextPage.FlatAppearance.BorderSize = 0
        Me.btnNextPage.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnNextPage.Name = "btnNextPage"
        Me.ToolTip1.SetToolTip(Me.btnNextPage, resources.GetString("btnNextPage.ToolTip"))
        Me.btnNextPage.UseVisualStyleBackColor = True
        '
        'btnValve2On
        '
        resources.ApplyResources(Me.btnValve2On, "btnValve2On")
        Me.btnValve2On.FlatAppearance.BorderSize = 0
        Me.btnValve2On.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnValve2On.Name = "btnValve2On"
        Me.ToolTip1.SetToolTip(Me.btnValve2On, resources.GetString("btnValve2On.ToolTip"))
        Me.btnValve2On.UseVisualStyleBackColor = True
        '
        'btnPrevPage
        '
        resources.ApplyResources(Me.btnPrevPage, "btnPrevPage")
        Me.btnPrevPage.FlatAppearance.BorderSize = 0
        Me.btnPrevPage.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnPrevPage.Name = "btnPrevPage"
        Me.ToolTip1.SetToolTip(Me.btnPrevPage, resources.GetString("btnPrevPage.ToolTip"))
        Me.btnPrevPage.UseVisualStyleBackColor = True
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
        Me.ToolTip1.SetToolTip(Me.UcJoyStick1, resources.GetString("UcJoyStick1.ToolTip"))
        '
        'UcCalibrationStatus1
        '
        resources.ApplyResources(Me.UcCalibrationStatus1, "UcCalibrationStatus1")
        Me.UcCalibrationStatus1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcCalibrationStatus1.Name = "UcCalibrationStatus1"
        Me.UcCalibrationStatus1.Status = WindowsApplication1.ucCalibrationStatus.enmCalibrationStatus.CCD2Valve1
        Me.ToolTip1.SetToolTip(Me.UcCalibrationStatus1, resources.GetString("UcCalibrationStatus1.ToolTip"))
        '
        'frmCalibrationCCD2Valve2
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCalibrationCCD2Valve2"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSetCcdPosX As System.Windows.Forms.Button
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents btnSetValvePosX As System.Windows.Forms.Button
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents txtTeachCCD2X As System.Windows.Forms.TextBox
    Friend WithEvents txtTeachCCD2Z As System.Windows.Forms.TextBox
    Friend WithEvents txtTeachDispenser2X As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTeachCCD2Y As System.Windows.Forms.TextBox
    Friend WithEvents txtTeachDispenser2Y As System.Windows.Forms.TextBox
    Friend WithEvents txtTeachDispenser2Z As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents UcJoyStick1 As ucJoyStick
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents btnGo1 As System.Windows.Forms.Button
    Friend WithEvents btnGo2 As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnNextPage As System.Windows.Forms.Button
    Friend WithEvents btnValve2On As System.Windows.Forms.Button
    Friend WithEvents btnPrevPage As System.Windows.Forms.Button
    Friend WithEvents btnGlueNo1SyringePressure As System.Windows.Forms.Button
    Friend WithEvents btnDispenserNo1EPRegulator As System.Windows.Forms.Button
    Friend WithEvents txtDispenserNo1EPRegulatorAO As System.Windows.Forms.TextBox
    Friend WithEvents lblDispenserNo1EPRegulator As System.Windows.Forms.Label
    Friend WithEvents btnAutoCalibrationSetup As System.Windows.Forms.Button
    Friend WithEvents UcCalibrationStatus1 As WindowsApplication1.ucCalibrationStatus
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

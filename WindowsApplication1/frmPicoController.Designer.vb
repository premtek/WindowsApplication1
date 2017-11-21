<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPicoController
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPicoController))
        Me.lbCloseVolts = New System.Windows.Forms.Label()
        Me.btnCloseVolts = New System.Windows.Forms.Button()
        Me.btnValveCount = New System.Windows.Forms.Button()
        Me.btnValveStroke = New System.Windows.Forms.Button()
        Me.btnValveCycle = New System.Windows.Forms.Button()
        Me.btnValveClose = New System.Windows.Forms.Button()
        Me.btnValveOpen = New System.Windows.Forms.Button()
        Me.btnValvePulse = New System.Windows.Forms.Button()
        Me.btnSetValveMode = New System.Windows.Forms.Button()
        Me.txtCloseVolts = New System.Windows.Forms.TextBox()
        Me.lbFreq = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCount = New System.Windows.Forms.TextBox()
        Me.txtStroke = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCycle = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtClose = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtOpen = New System.Windows.Forms.TextBox()
        Me.txtPulse = New System.Windows.Forms.TextBox()
        Me.cbxValveMode = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lbCount = New System.Windows.Forms.Label()
        Me.lbStroke = New System.Windows.Forms.Label()
        Me.lbCycle = New System.Windows.Forms.Label()
        Me.lbClose = New System.Windows.Forms.Label()
        Me.lbOpen = New System.Windows.Forms.Label()
        Me.lbPulse = New System.Windows.Forms.Label()
        Me.lbMode = New System.Windows.Forms.Label()
        Me.btnValvePowerOnOff = New System.Windows.Forms.Button()
        Me.btnNozzleHeaters = New System.Windows.Forms.Button()
        Me.lbActual = New System.Windows.Forms.Label()
        Me.txtNozzleHeaters = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lbNozzleHeaters = New System.Windows.Forms.Label()
        Me.btnManyCommandTest = New System.Windows.Forms.Button()
        Me.btnCycleOnOff = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnValveStatus = New System.Windows.Forms.Button()
        Me.lstValveStatus = New System.Windows.Forms.ListBox()
        Me.btnHeaterStatus = New System.Windows.Forms.Button()
        Me.lstHeaterStatus = New System.Windows.Forms.ListBox()
        Me.btnAlarmReset = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtCommandResponse = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cbxHeaterMode = New System.Windows.Forms.ComboBox()
        Me.btnSetHeaterMode = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtOffTime = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cbxCloseProfile = New System.Windows.Forms.ComboBox()
        Me.cbxOpenProfile = New System.Windows.Forms.ComboBox()
        Me.btnSetCloseProfile = New System.Windows.Forms.Button()
        Me.lbTime = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnSetOpenProfile = New System.Windows.Forms.Button()
        Me.btnSetOffTime = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnExit = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbCloseVolts
        '
        resources.ApplyResources(Me.lbCloseVolts, "lbCloseVolts")
        Me.lbCloseVolts.Name = "lbCloseVolts"
        '
        'btnCloseVolts
        '
        resources.ApplyResources(Me.btnCloseVolts, "btnCloseVolts")
        Me.btnCloseVolts.Name = "btnCloseVolts"
        Me.btnCloseVolts.UseVisualStyleBackColor = True
        '
        'btnValveCount
        '
        resources.ApplyResources(Me.btnValveCount, "btnValveCount")
        Me.btnValveCount.Name = "btnValveCount"
        Me.btnValveCount.UseVisualStyleBackColor = True
        '
        'btnValveStroke
        '
        resources.ApplyResources(Me.btnValveStroke, "btnValveStroke")
        Me.btnValveStroke.Name = "btnValveStroke"
        Me.btnValveStroke.UseVisualStyleBackColor = True
        '
        'btnValveCycle
        '
        resources.ApplyResources(Me.btnValveCycle, "btnValveCycle")
        Me.btnValveCycle.Name = "btnValveCycle"
        Me.btnValveCycle.UseVisualStyleBackColor = True
        '
        'btnValveClose
        '
        resources.ApplyResources(Me.btnValveClose, "btnValveClose")
        Me.btnValveClose.Name = "btnValveClose"
        Me.btnValveClose.UseVisualStyleBackColor = True
        '
        'btnValveOpen
        '
        resources.ApplyResources(Me.btnValveOpen, "btnValveOpen")
        Me.btnValveOpen.Name = "btnValveOpen"
        Me.btnValveOpen.UseVisualStyleBackColor = True
        '
        'btnValvePulse
        '
        resources.ApplyResources(Me.btnValvePulse, "btnValvePulse")
        Me.btnValvePulse.Name = "btnValvePulse"
        Me.btnValvePulse.UseVisualStyleBackColor = True
        '
        'btnSetValveMode
        '
        resources.ApplyResources(Me.btnSetValveMode, "btnSetValveMode")
        Me.btnSetValveMode.Name = "btnSetValveMode"
        Me.btnSetValveMode.UseVisualStyleBackColor = True
        '
        'txtCloseVolts
        '
        resources.ApplyResources(Me.txtCloseVolts, "txtCloseVolts")
        Me.txtCloseVolts.Name = "txtCloseVolts"
        '
        'lbFreq
        '
        resources.ApplyResources(Me.lbFreq, "lbFreq")
        Me.lbFreq.Name = "lbFreq"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'txtCount
        '
        resources.ApplyResources(Me.txtCount, "txtCount")
        Me.txtCount.Name = "txtCount"
        '
        'txtStroke
        '
        resources.ApplyResources(Me.txtStroke, "txtStroke")
        Me.txtStroke.Name = "txtStroke"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'txtCycle
        '
        resources.ApplyResources(Me.txtCycle, "txtCycle")
        Me.txtCycle.Name = "txtCycle"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'txtClose
        '
        resources.ApplyResources(Me.txtClose, "txtClose")
        Me.txtClose.Name = "txtClose"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'txtOpen
        '
        resources.ApplyResources(Me.txtOpen, "txtOpen")
        Me.txtOpen.Name = "txtOpen"
        '
        'txtPulse
        '
        resources.ApplyResources(Me.txtPulse, "txtPulse")
        Me.txtPulse.Name = "txtPulse"
        '
        'cbxValveMode
        '
        resources.ApplyResources(Me.cbxValveMode, "cbxValveMode")
        Me.cbxValveMode.FormattingEnabled = True
        Me.cbxValveMode.Items.AddRange(New Object() {resources.GetString("cbxValveMode.Items"), resources.GetString("cbxValveMode.Items1"), resources.GetString("cbxValveMode.Items2"), resources.GetString("cbxValveMode.Items3")})
        Me.cbxValveMode.Name = "cbxValveMode"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'lbCount
        '
        resources.ApplyResources(Me.lbCount, "lbCount")
        Me.lbCount.Name = "lbCount"
        '
        'lbStroke
        '
        resources.ApplyResources(Me.lbStroke, "lbStroke")
        Me.lbStroke.Name = "lbStroke"
        '
        'lbCycle
        '
        resources.ApplyResources(Me.lbCycle, "lbCycle")
        Me.lbCycle.Name = "lbCycle"
        '
        'lbClose
        '
        resources.ApplyResources(Me.lbClose, "lbClose")
        Me.lbClose.Name = "lbClose"
        '
        'lbOpen
        '
        resources.ApplyResources(Me.lbOpen, "lbOpen")
        Me.lbOpen.Name = "lbOpen"
        '
        'lbPulse
        '
        resources.ApplyResources(Me.lbPulse, "lbPulse")
        Me.lbPulse.Name = "lbPulse"
        '
        'lbMode
        '
        resources.ApplyResources(Me.lbMode, "lbMode")
        Me.lbMode.Name = "lbMode"
        '
        'btnValvePowerOnOff
        '
        resources.ApplyResources(Me.btnValvePowerOnOff, "btnValvePowerOnOff")
        Me.btnValvePowerOnOff.Name = "btnValvePowerOnOff"
        Me.btnValvePowerOnOff.UseVisualStyleBackColor = True
        '
        'btnNozzleHeaters
        '
        resources.ApplyResources(Me.btnNozzleHeaters, "btnNozzleHeaters")
        Me.btnNozzleHeaters.Name = "btnNozzleHeaters"
        Me.btnNozzleHeaters.UseVisualStyleBackColor = True
        '
        'lbActual
        '
        resources.ApplyResources(Me.lbActual, "lbActual")
        Me.lbActual.Name = "lbActual"
        '
        'txtNozzleHeaters
        '
        resources.ApplyResources(Me.txtNozzleHeaters, "txtNozzleHeaters")
        Me.txtNozzleHeaters.Name = "txtNozzleHeaters"
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        '
        'lbNozzleHeaters
        '
        resources.ApplyResources(Me.lbNozzleHeaters, "lbNozzleHeaters")
        Me.lbNozzleHeaters.Name = "lbNozzleHeaters"
        '
        'btnManyCommandTest
        '
        resources.ApplyResources(Me.btnManyCommandTest, "btnManyCommandTest")
        Me.btnManyCommandTest.Name = "btnManyCommandTest"
        Me.btnManyCommandTest.UseVisualStyleBackColor = True
        '
        'btnCycleOnOff
        '
        resources.ApplyResources(Me.btnCycleOnOff, "btnCycleOnOff")
        Me.btnCycleOnOff.Name = "btnCycleOnOff"
        Me.btnCycleOnOff.UseVisualStyleBackColor = True
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'btnValveStatus
        '
        resources.ApplyResources(Me.btnValveStatus, "btnValveStatus")
        Me.btnValveStatus.Name = "btnValveStatus"
        Me.btnValveStatus.UseVisualStyleBackColor = True
        '
        'lstValveStatus
        '
        resources.ApplyResources(Me.lstValveStatus, "lstValveStatus")
        Me.lstValveStatus.FormattingEnabled = True
        Me.lstValveStatus.Name = "lstValveStatus"
        '
        'btnHeaterStatus
        '
        resources.ApplyResources(Me.btnHeaterStatus, "btnHeaterStatus")
        Me.btnHeaterStatus.Name = "btnHeaterStatus"
        Me.btnHeaterStatus.UseVisualStyleBackColor = True
        '
        'lstHeaterStatus
        '
        resources.ApplyResources(Me.lstHeaterStatus, "lstHeaterStatus")
        Me.lstHeaterStatus.FormattingEnabled = True
        Me.lstHeaterStatus.Name = "lstHeaterStatus"
        '
        'btnAlarmReset
        '
        resources.ApplyResources(Me.btnAlarmReset, "btnAlarmReset")
        Me.btnAlarmReset.Name = "btnAlarmReset"
        Me.btnAlarmReset.UseVisualStyleBackColor = True
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'txtCommandResponse
        '
        resources.ApplyResources(Me.txtCommandResponse, "txtCommandResponse")
        Me.txtCommandResponse.Name = "txtCommandResponse"
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.btnCycleOnOff, 1, 22)
        Me.TableLayoutPanel1.Controls.Add(Me.lbActual, 1, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.Label9, 0, 22)
        Me.TableLayoutPanel1.Controls.Add(Me.btnAlarmReset, 1, 23)
        Me.TableLayoutPanel1.Controls.Add(Me.btnNozzleHeaters, 3, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.Label13, 0, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 0, 20)
        Me.TableLayoutPanel1.Controls.Add(Me.btnValvePowerOnOff, 1, 20)
        Me.TableLayoutPanel1.Controls.Add(Me.btnCloseVolts, 3, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.txtCloseVolts, 1, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.txtNozzleHeaters, 1, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.lbFreq, 1, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.lbCloseVolts, 0, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.btnValveCount, 3, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.txtCount, 1, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.btnValveStroke, 3, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.btnValveCycle, 3, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.btnValveClose, 3, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.btnValveOpen, 3, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.lbCount, 0, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.lbStroke, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.lbCycle, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.lbClose, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.lbOpen, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.txtStroke, 1, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 2, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.txtCycle, 1, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.txtClose, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.txtOpen, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPulse, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 2, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 2, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 2, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.cbxValveMode, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label12, 0, 13)
        Me.TableLayoutPanel1.Controls.Add(Me.Label14, 0, 17)
        Me.TableLayoutPanel1.Controls.Add(Me.lbNozzleHeaters, 0, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.Label15, 0, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.cbxHeaterMode, 1, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.btnSetHeaterMode, 3, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txtOffTime, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label16, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label17, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label18, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.cbxCloseProfile, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.cbxOpenProfile, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.btnSetCloseProfile, 3, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.btnManyCommandTest, 3, 23)
        Me.TableLayoutPanel1.Controls.Add(Me.lbTime, 2, 23)
        Me.TableLayoutPanel1.Controls.Add(Me.lbMode, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lbPulse, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label11, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnSetValveMode, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnValvePulse, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btnSetOpenProfile, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btnSetOffTime, 3, 5)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.Name = "Label15"
        '
        'cbxHeaterMode
        '
        resources.ApplyResources(Me.cbxHeaterMode, "cbxHeaterMode")
        Me.cbxHeaterMode.FormattingEnabled = True
        Me.cbxHeaterMode.Items.AddRange(New Object() {resources.GetString("cbxHeaterMode.Items"), resources.GetString("cbxHeaterMode.Items1"), resources.GetString("cbxHeaterMode.Items2"), resources.GetString("cbxHeaterMode.Items3"), resources.GetString("cbxHeaterMode.Items4")})
        Me.cbxHeaterMode.Name = "cbxHeaterMode"
        '
        'btnSetHeaterMode
        '
        resources.ApplyResources(Me.btnSetHeaterMode, "btnSetHeaterMode")
        Me.btnSetHeaterMode.Name = "btnSetHeaterMode"
        Me.btnSetHeaterMode.UseVisualStyleBackColor = True
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'txtOffTime
        '
        resources.ApplyResources(Me.txtOffTime, "txtOffTime")
        Me.txtOffTime.Name = "txtOffTime"
        '
        'Label16
        '
        resources.ApplyResources(Me.Label16, "Label16")
        Me.Label16.Name = "Label16"
        '
        'Label17
        '
        resources.ApplyResources(Me.Label17, "Label17")
        Me.Label17.Name = "Label17"
        '
        'Label18
        '
        resources.ApplyResources(Me.Label18, "Label18")
        Me.Label18.Name = "Label18"
        '
        'cbxCloseProfile
        '
        resources.ApplyResources(Me.cbxCloseProfile, "cbxCloseProfile")
        Me.cbxCloseProfile.FormattingEnabled = True
        Me.cbxCloseProfile.Items.AddRange(New Object() {resources.GetString("cbxCloseProfile.Items"), resources.GetString("cbxCloseProfile.Items1"), resources.GetString("cbxCloseProfile.Items2"), resources.GetString("cbxCloseProfile.Items3"), resources.GetString("cbxCloseProfile.Items4"), resources.GetString("cbxCloseProfile.Items5")})
        Me.cbxCloseProfile.Name = "cbxCloseProfile"
        '
        'cbxOpenProfile
        '
        resources.ApplyResources(Me.cbxOpenProfile, "cbxOpenProfile")
        Me.cbxOpenProfile.FormattingEnabled = True
        Me.cbxOpenProfile.Items.AddRange(New Object() {resources.GetString("cbxOpenProfile.Items"), resources.GetString("cbxOpenProfile.Items1"), resources.GetString("cbxOpenProfile.Items2"), resources.GetString("cbxOpenProfile.Items3"), resources.GetString("cbxOpenProfile.Items4"), resources.GetString("cbxOpenProfile.Items5")})
        Me.cbxOpenProfile.Name = "cbxOpenProfile"
        '
        'btnSetCloseProfile
        '
        resources.ApplyResources(Me.btnSetCloseProfile, "btnSetCloseProfile")
        Me.btnSetCloseProfile.Name = "btnSetCloseProfile"
        Me.btnSetCloseProfile.UseVisualStyleBackColor = True
        '
        'lbTime
        '
        resources.ApplyResources(Me.lbTime, "lbTime")
        Me.lbTime.Name = "lbTime"
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        '
        'btnSetOpenProfile
        '
        resources.ApplyResources(Me.btnSetOpenProfile, "btnSetOpenProfile")
        Me.btnSetOpenProfile.Name = "btnSetOpenProfile"
        Me.btnSetOpenProfile.UseVisualStyleBackColor = True
        '
        'btnSetOffTime
        '
        resources.ApplyResources(Me.btnSetOffTime, "btnSetOffTime")
        Me.btnSetOffTime.Name = "btnSetOffTime"
        Me.btnSetOffTime.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        resources.ApplyResources(Me.TableLayoutPanel2, "TableLayoutPanel2")
        Me.TableLayoutPanel2.Controls.Add(Me.lstHeaterStatus, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.btnValveStatus, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.btnHeaterStatus, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.lstValveStatus, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Label10, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtCommandResponse, 0, 1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'btnExit
        '
        Me.btnExit.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        resources.ApplyResources(Me.btnExit, "btnExit")
        Me.btnExit.FlatAppearance.BorderSize = 0
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnExit.Name = "btnExit"
        Me.ToolTip1.SetToolTip(Me.btnExit, resources.GetString("btnExit.ToolTip"))
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmPicoController
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPicoController"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbCloseVolts As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lbCount As System.Windows.Forms.Label
    Friend WithEvents lbStroke As System.Windows.Forms.Label
    Friend WithEvents lbCycle As System.Windows.Forms.Label
    Friend WithEvents lbClose As System.Windows.Forms.Label
    Friend WithEvents lbOpen As System.Windows.Forms.Label
    Friend WithEvents lbPulse As System.Windows.Forms.Label
    Friend WithEvents lbMode As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lbNozzleHeaters As System.Windows.Forms.Label
    Friend WithEvents cbxValveMode As System.Windows.Forms.ComboBox
    Friend WithEvents btnManyCommandTest As System.Windows.Forms.Button
    Friend WithEvents lbFreq As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents txtStroke As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCycle As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtClose As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtOpen As System.Windows.Forms.TextBox
    Friend WithEvents txtPulse As System.Windows.Forms.TextBox
    Friend WithEvents lbActual As System.Windows.Forms.Label
    Friend WithEvents txtNozzleHeaters As System.Windows.Forms.TextBox
    Friend WithEvents btnValvePowerOnOff As System.Windows.Forms.Button
    Friend WithEvents txtCloseVolts As System.Windows.Forms.TextBox
    Friend WithEvents btnCycleOnOff As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnValveStatus As System.Windows.Forms.Button
    Friend WithEvents lstValveStatus As System.Windows.Forms.ListBox
    Friend WithEvents btnHeaterStatus As System.Windows.Forms.Button
    Friend WithEvents lstHeaterStatus As System.Windows.Forms.ListBox
    Friend WithEvents btnAlarmReset As System.Windows.Forms.Button
    Friend WithEvents btnCloseVolts As System.Windows.Forms.Button
    Friend WithEvents btnValveCount As System.Windows.Forms.Button
    Friend WithEvents btnValveStroke As System.Windows.Forms.Button
    Friend WithEvents btnValveCycle As System.Windows.Forms.Button
    Friend WithEvents btnValveClose As System.Windows.Forms.Button
    Friend WithEvents btnValveOpen As System.Windows.Forms.Button
    Friend WithEvents btnValvePulse As System.Windows.Forms.Button
    Friend WithEvents btnSetValveMode As System.Windows.Forms.Button
    Friend WithEvents btnNozzleHeaters As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCommandResponse As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cbxHeaterMode As System.Windows.Forms.ComboBox
    Friend WithEvents btnSetHeaterMode As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtOffTime As System.Windows.Forms.TextBox
    Friend WithEvents btnSetOffTime As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cbxCloseProfile As System.Windows.Forms.ComboBox
    Friend WithEvents cbxOpenProfile As System.Windows.Forms.ComboBox
    Friend WithEvents btnSetCloseProfile As System.Windows.Forms.Button
    Friend WithEvents btnSetOpenProfile As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lbTime As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucValveParameterPico
    Inherits System.Windows.Forms.UserControl

    'UserControl 覆寫 Dispose 以清除元件清單。
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucValveParameterPico))
        Me.grpPicoParameter = New System.Windows.Forms.GroupBox()
        Me.tlpPico = New System.Windows.Forms.TableLayoutPanel()
        Me.lblPicoParameterFrequencyUnit = New System.Windows.Forms.Label()
        Me.lbPicoParameterJetTime = New System.Windows.Forms.Label()
        Me.lblPicoParameterValveOffTimeUnit = New System.Windows.Forms.Label()
        Me.txtPicoParameterFrequency = New System.Windows.Forms.TextBox()
        Me.lbPicoParameterStrokeUnit = New System.Windows.Forms.Label()
        Me.lbPicoParameterCycleTimeUnit = New System.Windows.Forms.Label()
        Me.lbPicoParameterCloseVoltageUnit = New System.Windows.Forms.Label()
        Me.lblPicoParameterJetTimeUnit = New System.Windows.Forms.Label()
        Me.lbPicoParameterCloseTimeUnit = New System.Windows.Forms.Label()
        Me.lblPicoParameterFrequency = New System.Windows.Forms.Label()
        Me.txtPicoParameterValveOffTime = New System.Windows.Forms.TextBox()
        Me.lbPicoParameterPulseTimeUnit = New System.Windows.Forms.Label()
        Me.lbPicoParameterOpenTimeUnit = New System.Windows.Forms.Label()
        Me.lbPicoParameterCloseVoltage = New System.Windows.Forms.Label()
        Me.lbPicoParameterStroke = New System.Windows.Forms.Label()
        Me.txtPicoParameterJetTime = New System.Windows.Forms.TextBox()
        Me.txtPicoParameterStroke = New System.Windows.Forms.TextBox()
        Me.txtPicoParameterCloseTime = New System.Windows.Forms.TextBox()
        Me.lbPicoParameterOpenTime = New System.Windows.Forms.Label()
        Me.lbPicoParameterPulseTime = New System.Windows.Forms.Label()
        Me.lblPicoParameterValveOffTime = New System.Windows.Forms.Label()
        Me.txtPicoParameterCycleTime = New System.Windows.Forms.TextBox()
        Me.txtPicoParameterOpenTime = New System.Windows.Forms.TextBox()
        Me.txtPicoParameterPulseTime = New System.Windows.Forms.TextBox()
        Me.txtPicoParameterCloseVoltage = New System.Windows.Forms.TextBox()
        Me.lbPicoParameterCloseTime = New System.Windows.Forms.Label()
        Me.lbPicoParameterCycleTime = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnSavePicoParameter = New System.Windows.Forms.Button()
        Me.txtJetValveName = New System.Windows.Forms.TextBox()
        Me.lstJetValve = New System.Windows.Forms.ListBox()
        Me.btnValveDBAdd = New System.Windows.Forms.Button()
        Me.btnValveDBDelete = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpPicoParameter.SuspendLayout()
        Me.tlpPico.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpPicoParameter
        '
        Me.grpPicoParameter.Controls.Add(Me.tlpPico)
        resources.ApplyResources(Me.grpPicoParameter, "grpPicoParameter")
        Me.grpPicoParameter.Name = "grpPicoParameter"
        Me.grpPicoParameter.TabStop = False
        '
        'tlpPico
        '
        resources.ApplyResources(Me.tlpPico, "tlpPico")
        Me.tlpPico.Controls.Add(Me.lblPicoParameterFrequencyUnit, 2, 8)
        Me.tlpPico.Controls.Add(Me.lbPicoParameterJetTime, 0, 0)
        Me.tlpPico.Controls.Add(Me.lblPicoParameterValveOffTimeUnit, 2, 6)
        Me.tlpPico.Controls.Add(Me.txtPicoParameterFrequency, 1, 8)
        Me.tlpPico.Controls.Add(Me.lbPicoParameterStrokeUnit, 2, 2)
        Me.tlpPico.Controls.Add(Me.lbPicoParameterCycleTimeUnit, 2, 7)
        Me.tlpPico.Controls.Add(Me.lbPicoParameterCloseVoltageUnit, 2, 1)
        Me.tlpPico.Controls.Add(Me.lblPicoParameterJetTimeUnit, 2, 0)
        Me.tlpPico.Controls.Add(Me.lbPicoParameterCloseTimeUnit, 2, 5)
        Me.tlpPico.Controls.Add(Me.lblPicoParameterFrequency, 0, 8)
        Me.tlpPico.Controls.Add(Me.txtPicoParameterValveOffTime, 1, 6)
        Me.tlpPico.Controls.Add(Me.lbPicoParameterPulseTimeUnit, 2, 4)
        Me.tlpPico.Controls.Add(Me.lbPicoParameterOpenTimeUnit, 2, 3)
        Me.tlpPico.Controls.Add(Me.lbPicoParameterCloseVoltage, 0, 1)
        Me.tlpPico.Controls.Add(Me.lbPicoParameterStroke, 0, 2)
        Me.tlpPico.Controls.Add(Me.txtPicoParameterJetTime, 1, 0)
        Me.tlpPico.Controls.Add(Me.txtPicoParameterStroke, 1, 2)
        Me.tlpPico.Controls.Add(Me.txtPicoParameterCloseTime, 1, 5)
        Me.tlpPico.Controls.Add(Me.lbPicoParameterOpenTime, 0, 3)
        Me.tlpPico.Controls.Add(Me.lbPicoParameterPulseTime, 0, 4)
        Me.tlpPico.Controls.Add(Me.lblPicoParameterValveOffTime, 0, 6)
        Me.tlpPico.Controls.Add(Me.txtPicoParameterCycleTime, 1, 7)
        Me.tlpPico.Controls.Add(Me.txtPicoParameterOpenTime, 1, 3)
        Me.tlpPico.Controls.Add(Me.txtPicoParameterPulseTime, 1, 4)
        Me.tlpPico.Controls.Add(Me.txtPicoParameterCloseVoltage, 1, 1)
        Me.tlpPico.Controls.Add(Me.lbPicoParameterCloseTime, 0, 5)
        Me.tlpPico.Controls.Add(Me.lbPicoParameterCycleTime, 0, 7)
        Me.tlpPico.Name = "tlpPico"
        '
        'lblPicoParameterFrequencyUnit
        '
        resources.ApplyResources(Me.lblPicoParameterFrequencyUnit, "lblPicoParameterFrequencyUnit")
        Me.lblPicoParameterFrequencyUnit.Name = "lblPicoParameterFrequencyUnit"
        '
        'lbPicoParameterJetTime
        '
        resources.ApplyResources(Me.lbPicoParameterJetTime, "lbPicoParameterJetTime")
        Me.lbPicoParameterJetTime.Name = "lbPicoParameterJetTime"
        '
        'lblPicoParameterValveOffTimeUnit
        '
        resources.ApplyResources(Me.lblPicoParameterValveOffTimeUnit, "lblPicoParameterValveOffTimeUnit")
        Me.lblPicoParameterValveOffTimeUnit.Name = "lblPicoParameterValveOffTimeUnit"
        '
        'txtPicoParameterFrequency
        '
        resources.ApplyResources(Me.txtPicoParameterFrequency, "txtPicoParameterFrequency")
        Me.txtPicoParameterFrequency.Name = "txtPicoParameterFrequency"
        '
        'lbPicoParameterStrokeUnit
        '
        resources.ApplyResources(Me.lbPicoParameterStrokeUnit, "lbPicoParameterStrokeUnit")
        Me.lbPicoParameterStrokeUnit.Name = "lbPicoParameterStrokeUnit"
        '
        'lbPicoParameterCycleTimeUnit
        '
        resources.ApplyResources(Me.lbPicoParameterCycleTimeUnit, "lbPicoParameterCycleTimeUnit")
        Me.lbPicoParameterCycleTimeUnit.Name = "lbPicoParameterCycleTimeUnit"
        '
        'lbPicoParameterCloseVoltageUnit
        '
        resources.ApplyResources(Me.lbPicoParameterCloseVoltageUnit, "lbPicoParameterCloseVoltageUnit")
        Me.lbPicoParameterCloseVoltageUnit.Name = "lbPicoParameterCloseVoltageUnit"
        '
        'lblPicoParameterJetTimeUnit
        '
        resources.ApplyResources(Me.lblPicoParameterJetTimeUnit, "lblPicoParameterJetTimeUnit")
        Me.lblPicoParameterJetTimeUnit.Name = "lblPicoParameterJetTimeUnit"
        '
        'lbPicoParameterCloseTimeUnit
        '
        resources.ApplyResources(Me.lbPicoParameterCloseTimeUnit, "lbPicoParameterCloseTimeUnit")
        Me.lbPicoParameterCloseTimeUnit.Name = "lbPicoParameterCloseTimeUnit"
        '
        'lblPicoParameterFrequency
        '
        resources.ApplyResources(Me.lblPicoParameterFrequency, "lblPicoParameterFrequency")
        Me.lblPicoParameterFrequency.Name = "lblPicoParameterFrequency"
        '
        'txtPicoParameterValveOffTime
        '
        resources.ApplyResources(Me.txtPicoParameterValveOffTime, "txtPicoParameterValveOffTime")
        Me.txtPicoParameterValveOffTime.Name = "txtPicoParameterValveOffTime"
        '
        'lbPicoParameterPulseTimeUnit
        '
        resources.ApplyResources(Me.lbPicoParameterPulseTimeUnit, "lbPicoParameterPulseTimeUnit")
        Me.lbPicoParameterPulseTimeUnit.Name = "lbPicoParameterPulseTimeUnit"
        '
        'lbPicoParameterOpenTimeUnit
        '
        resources.ApplyResources(Me.lbPicoParameterOpenTimeUnit, "lbPicoParameterOpenTimeUnit")
        Me.lbPicoParameterOpenTimeUnit.Name = "lbPicoParameterOpenTimeUnit"
        '
        'lbPicoParameterCloseVoltage
        '
        resources.ApplyResources(Me.lbPicoParameterCloseVoltage, "lbPicoParameterCloseVoltage")
        Me.lbPicoParameterCloseVoltage.Name = "lbPicoParameterCloseVoltage"
        '
        'lbPicoParameterStroke
        '
        resources.ApplyResources(Me.lbPicoParameterStroke, "lbPicoParameterStroke")
        Me.lbPicoParameterStroke.Name = "lbPicoParameterStroke"
        '
        'txtPicoParameterJetTime
        '
        resources.ApplyResources(Me.txtPicoParameterJetTime, "txtPicoParameterJetTime")
        Me.txtPicoParameterJetTime.Name = "txtPicoParameterJetTime"
        '
        'txtPicoParameterStroke
        '
        resources.ApplyResources(Me.txtPicoParameterStroke, "txtPicoParameterStroke")
        Me.txtPicoParameterStroke.Name = "txtPicoParameterStroke"
        '
        'txtPicoParameterCloseTime
        '
        resources.ApplyResources(Me.txtPicoParameterCloseTime, "txtPicoParameterCloseTime")
        Me.txtPicoParameterCloseTime.Name = "txtPicoParameterCloseTime"
        '
        'lbPicoParameterOpenTime
        '
        resources.ApplyResources(Me.lbPicoParameterOpenTime, "lbPicoParameterOpenTime")
        Me.lbPicoParameterOpenTime.Name = "lbPicoParameterOpenTime"
        '
        'lbPicoParameterPulseTime
        '
        resources.ApplyResources(Me.lbPicoParameterPulseTime, "lbPicoParameterPulseTime")
        Me.lbPicoParameterPulseTime.Name = "lbPicoParameterPulseTime"
        '
        'lblPicoParameterValveOffTime
        '
        resources.ApplyResources(Me.lblPicoParameterValveOffTime, "lblPicoParameterValveOffTime")
        Me.lblPicoParameterValveOffTime.Name = "lblPicoParameterValveOffTime"
        '
        'txtPicoParameterCycleTime
        '
        resources.ApplyResources(Me.txtPicoParameterCycleTime, "txtPicoParameterCycleTime")
        Me.txtPicoParameterCycleTime.Name = "txtPicoParameterCycleTime"
        '
        'txtPicoParameterOpenTime
        '
        resources.ApplyResources(Me.txtPicoParameterOpenTime, "txtPicoParameterOpenTime")
        Me.txtPicoParameterOpenTime.Name = "txtPicoParameterOpenTime"
        '
        'txtPicoParameterPulseTime
        '
        resources.ApplyResources(Me.txtPicoParameterPulseTime, "txtPicoParameterPulseTime")
        Me.txtPicoParameterPulseTime.Name = "txtPicoParameterPulseTime"
        '
        'txtPicoParameterCloseVoltage
        '
        resources.ApplyResources(Me.txtPicoParameterCloseVoltage, "txtPicoParameterCloseVoltage")
        Me.txtPicoParameterCloseVoltage.Name = "txtPicoParameterCloseVoltage"
        '
        'lbPicoParameterCloseTime
        '
        resources.ApplyResources(Me.lbPicoParameterCloseTime, "lbPicoParameterCloseTime")
        Me.lbPicoParameterCloseTime.Name = "lbPicoParameterCloseTime"
        '
        'lbPicoParameterCycleTime
        '
        resources.ApplyResources(Me.lbPicoParameterCycleTime, "lbPicoParameterCycleTime")
        Me.lbPicoParameterCycleTime.Name = "lbPicoParameterCycleTime"
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
        Me.ToolTip1.SetToolTip(Me.btnBack, resources.GetString("btnBack.ToolTip"))
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnSavePicoParameter
        '
        resources.ApplyResources(Me.btnSavePicoParameter, "btnSavePicoParameter")
        Me.btnSavePicoParameter.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnSavePicoParameter.Name = "btnSavePicoParameter"
        Me.ToolTip1.SetToolTip(Me.btnSavePicoParameter, resources.GetString("btnSavePicoParameter.ToolTip"))
        Me.btnSavePicoParameter.UseVisualStyleBackColor = True
        '
        'txtJetValveName
        '
        resources.ApplyResources(Me.txtJetValveName, "txtJetValveName")
        Me.txtJetValveName.Name = "txtJetValveName"
        '
        'lstJetValve
        '
        resources.ApplyResources(Me.lstJetValve, "lstJetValve")
        Me.lstJetValve.FormattingEnabled = True
        Me.lstJetValve.Name = "lstJetValve"
        '
        'btnValveDBAdd
        '
        resources.ApplyResources(Me.btnValveDBAdd, "btnValveDBAdd")
        Me.btnValveDBAdd.Name = "btnValveDBAdd"
        Me.btnValveDBAdd.UseVisualStyleBackColor = True
        '
        'btnValveDBDelete
        '
        resources.ApplyResources(Me.btnValveDBDelete, "btnValveDBDelete")
        Me.btnValveDBDelete.Name = "btnValveDBDelete"
        Me.btnValveDBDelete.UseVisualStyleBackColor = True
        '
        'ucValveParameterPico
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.txtJetValveName)
        Me.Controls.Add(Me.lstJetValve)
        Me.Controls.Add(Me.btnValveDBAdd)
        Me.Controls.Add(Me.btnValveDBDelete)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnSavePicoParameter)
        Me.Controls.Add(Me.grpPicoParameter)
        Me.Name = "ucValveParameterPico"
        Me.grpPicoParameter.ResumeLayout(False)
        Me.tlpPico.ResumeLayout(False)
        Me.tlpPico.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpPicoParameter As System.Windows.Forms.GroupBox
    Friend WithEvents lbPicoParameterCycleTime As System.Windows.Forms.Label
    Friend WithEvents tlpPico As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbPicoParameterPulseTime As System.Windows.Forms.Label
    Friend WithEvents lbPicoParameterOpenTime As System.Windows.Forms.Label
    Friend WithEvents lbPicoParameterCloseTime As System.Windows.Forms.Label
    Friend WithEvents lbPicoParameterStroke As System.Windows.Forms.Label
    Friend WithEvents lbPicoParameterCloseVoltage As System.Windows.Forms.Label
    Friend WithEvents txtPicoParameterCycleTime As System.Windows.Forms.TextBox
    Friend WithEvents lbPicoParameterCycleTimeUnit As System.Windows.Forms.Label
    Friend WithEvents lbPicoParameterCloseVoltageUnit As System.Windows.Forms.Label
    Friend WithEvents lbPicoParameterStrokeUnit As System.Windows.Forms.Label
    Friend WithEvents lbPicoParameterCloseTimeUnit As System.Windows.Forms.Label
    Friend WithEvents lbPicoParameterOpenTimeUnit As System.Windows.Forms.Label
    Friend WithEvents lbPicoParameterPulseTimeUnit As System.Windows.Forms.Label
    Friend WithEvents txtPicoParameterCloseVoltage As System.Windows.Forms.TextBox
    Friend WithEvents txtPicoParameterStroke As System.Windows.Forms.TextBox
    Friend WithEvents txtPicoParameterCloseTime As System.Windows.Forms.TextBox
    Friend WithEvents txtPicoParameterOpenTime As System.Windows.Forms.TextBox
    Friend WithEvents txtPicoParameterPulseTime As System.Windows.Forms.TextBox
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnSavePicoParameter As System.Windows.Forms.Button
    Friend WithEvents lbPicoParameterJetTime As System.Windows.Forms.Label
    Friend WithEvents txtPicoParameterJetTime As System.Windows.Forms.TextBox
    Friend WithEvents lblPicoParameterJetTimeUnit As System.Windows.Forms.Label
    Friend WithEvents txtPicoParameterValveOffTime As System.Windows.Forms.TextBox
    Friend WithEvents lblPicoParameterValveOffTimeUnit As System.Windows.Forms.Label
    Friend WithEvents lblPicoParameterValveOffTime As System.Windows.Forms.Label
    Friend WithEvents lblPicoParameterFrequencyUnit As System.Windows.Forms.Label
    Friend WithEvents lblPicoParameterFrequency As System.Windows.Forms.Label
    Friend WithEvents txtPicoParameterFrequency As System.Windows.Forms.TextBox
    Friend WithEvents txtJetValveName As System.Windows.Forms.TextBox
    Friend WithEvents lstJetValve As System.Windows.Forms.ListBox
    Friend WithEvents btnValveDBAdd As System.Windows.Forms.Button
    Friend WithEvents btnValveDBDelete As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class

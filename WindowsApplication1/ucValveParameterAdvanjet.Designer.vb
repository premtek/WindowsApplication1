<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucValveParameterAdvanjet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucValveParameterAdvanjet))
        Me.grpAdvanjetParameter = New System.Windows.Forms.GroupBox()
        Me.tlpPico = New System.Windows.Forms.TableLayoutPanel()
        Me.txtAdvanjetParameterJetTime = New System.Windows.Forms.TextBox()
        Me.txtAdvanjetParameterCycleTime = New System.Windows.Forms.TextBox()
        Me.lbAdvanjetParameterRefillTime = New System.Windows.Forms.Label()
        Me.txtAdvanjetParameterRefillTime = New System.Windows.Forms.TextBox()
        Me.lbAdvanjetParameterJetTimeUnit = New System.Windows.Forms.Label()
        Me.lbAdvanjetParameterCycleTime = New System.Windows.Forms.Label()
        Me.lbAdvanjetParameterCycleTimeUnit = New System.Windows.Forms.Label()
        Me.lbAdvanjetParameterJetTime = New System.Windows.Forms.Label()
        Me.lbAdvanjetParameterRefillTimeUnit = New System.Windows.Forms.Label()
        Me.txtJetValveName = New System.Windows.Forms.TextBox()
        Me.lstJetValve = New System.Windows.Forms.ListBox()
        Me.btnValveDBAdd = New System.Windows.Forms.Button()
        Me.btnValveDBDelete = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnSaveAdvanjetParameter = New System.Windows.Forms.Button()
        Me.grpAdvanjetParameter.SuspendLayout()
        Me.tlpPico.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpAdvanjetParameter
        '
        Me.grpAdvanjetParameter.Controls.Add(Me.tlpPico)
        resources.ApplyResources(Me.grpAdvanjetParameter, "grpAdvanjetParameter")
        Me.grpAdvanjetParameter.Name = "grpAdvanjetParameter"
        Me.grpAdvanjetParameter.TabStop = False
        '
        'tlpPico
        '
        resources.ApplyResources(Me.tlpPico, "tlpPico")
        Me.tlpPico.Controls.Add(Me.txtAdvanjetParameterJetTime, 1, 2)
        Me.tlpPico.Controls.Add(Me.txtAdvanjetParameterCycleTime, 1, 1)
        Me.tlpPico.Controls.Add(Me.lbAdvanjetParameterRefillTime, 0, 0)
        Me.tlpPico.Controls.Add(Me.txtAdvanjetParameterRefillTime, 1, 0)
        Me.tlpPico.Controls.Add(Me.lbAdvanjetParameterJetTimeUnit, 2, 2)
        Me.tlpPico.Controls.Add(Me.lbAdvanjetParameterCycleTime, 0, 1)
        Me.tlpPico.Controls.Add(Me.lbAdvanjetParameterCycleTimeUnit, 2, 1)
        Me.tlpPico.Controls.Add(Me.lbAdvanjetParameterJetTime, 0, 2)
        Me.tlpPico.Controls.Add(Me.lbAdvanjetParameterRefillTimeUnit, 2, 0)
        Me.tlpPico.Name = "tlpPico"
        '
        'txtAdvanjetParameterJetTime
        '
        resources.ApplyResources(Me.txtAdvanjetParameterJetTime, "txtAdvanjetParameterJetTime")
        Me.txtAdvanjetParameterJetTime.Name = "txtAdvanjetParameterJetTime"
        '
        'txtAdvanjetParameterCycleTime
        '
        resources.ApplyResources(Me.txtAdvanjetParameterCycleTime, "txtAdvanjetParameterCycleTime")
        Me.txtAdvanjetParameterCycleTime.Name = "txtAdvanjetParameterCycleTime"
        '
        'lbAdvanjetParameterRefillTime
        '
        resources.ApplyResources(Me.lbAdvanjetParameterRefillTime, "lbAdvanjetParameterRefillTime")
        Me.lbAdvanjetParameterRefillTime.Name = "lbAdvanjetParameterRefillTime"
        '
        'txtAdvanjetParameterRefillTime
        '
        resources.ApplyResources(Me.txtAdvanjetParameterRefillTime, "txtAdvanjetParameterRefillTime")
        Me.txtAdvanjetParameterRefillTime.Name = "txtAdvanjetParameterRefillTime"
        '
        'lbAdvanjetParameterJetTimeUnit
        '
        resources.ApplyResources(Me.lbAdvanjetParameterJetTimeUnit, "lbAdvanjetParameterJetTimeUnit")
        Me.lbAdvanjetParameterJetTimeUnit.Name = "lbAdvanjetParameterJetTimeUnit"
        '
        'lbAdvanjetParameterCycleTime
        '
        resources.ApplyResources(Me.lbAdvanjetParameterCycleTime, "lbAdvanjetParameterCycleTime")
        Me.lbAdvanjetParameterCycleTime.Name = "lbAdvanjetParameterCycleTime"
        '
        'lbAdvanjetParameterCycleTimeUnit
        '
        resources.ApplyResources(Me.lbAdvanjetParameterCycleTimeUnit, "lbAdvanjetParameterCycleTimeUnit")
        Me.lbAdvanjetParameterCycleTimeUnit.Name = "lbAdvanjetParameterCycleTimeUnit"
        '
        'lbAdvanjetParameterJetTime
        '
        resources.ApplyResources(Me.lbAdvanjetParameterJetTime, "lbAdvanjetParameterJetTime")
        Me.lbAdvanjetParameterJetTime.Name = "lbAdvanjetParameterJetTime"
        '
        'lbAdvanjetParameterRefillTimeUnit
        '
        resources.ApplyResources(Me.lbAdvanjetParameterRefillTimeUnit, "lbAdvanjetParameterRefillTimeUnit")
        Me.lbAdvanjetParameterRefillTimeUnit.Name = "lbAdvanjetParameterRefillTimeUnit"
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
        'btnSaveAdvanjetParameter
        '
        resources.ApplyResources(Me.btnSaveAdvanjetParameter, "btnSaveAdvanjetParameter")
        Me.btnSaveAdvanjetParameter.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnSaveAdvanjetParameter.Name = "btnSaveAdvanjetParameter"
        Me.ToolTip1.SetToolTip(Me.btnSaveAdvanjetParameter, resources.GetString("btnSaveAdvanjetParameter.ToolTip"))
        Me.btnSaveAdvanjetParameter.UseVisualStyleBackColor = True
        '
        'ucValveParameterAdvanjet
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.txtJetValveName)
        Me.Controls.Add(Me.lstJetValve)
        Me.Controls.Add(Me.btnValveDBAdd)
        Me.Controls.Add(Me.btnValveDBDelete)
        Me.Controls.Add(Me.grpAdvanjetParameter)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnSaveAdvanjetParameter)
        Me.Name = "ucValveParameterAdvanjet"
        Me.grpAdvanjetParameter.ResumeLayout(False)
        Me.tlpPico.ResumeLayout(False)
        Me.tlpPico.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnSaveAdvanjetParameter As System.Windows.Forms.Button
    Friend WithEvents grpAdvanjetParameter As System.Windows.Forms.GroupBox
    Friend WithEvents tlpPico As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtAdvanjetParameterJetTime As System.Windows.Forms.TextBox
    Friend WithEvents txtAdvanjetParameterCycleTime As System.Windows.Forms.TextBox
    Friend WithEvents lbAdvanjetParameterRefillTime As System.Windows.Forms.Label
    Friend WithEvents txtAdvanjetParameterRefillTime As System.Windows.Forms.TextBox
    Friend WithEvents lbAdvanjetParameterJetTimeUnit As System.Windows.Forms.Label
    Friend WithEvents lbAdvanjetParameterCycleTime As System.Windows.Forms.Label
    Friend WithEvents lbAdvanjetParameterCycleTimeUnit As System.Windows.Forms.Label
    Friend WithEvents lbAdvanjetParameterJetTime As System.Windows.Forms.Label
    Friend WithEvents lbAdvanjetParameterRefillTimeUnit As System.Windows.Forms.Label
    Friend WithEvents txtJetValveName As System.Windows.Forms.TextBox
    Friend WithEvents lstJetValve As System.Windows.Forms.ListBox
    Friend WithEvents btnValveDBAdd As System.Windows.Forms.Button
    Friend WithEvents btnValveDBDelete As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class

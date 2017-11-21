<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpStage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOpStage))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnValve1Weight = New System.Windows.Forms.Button()
        Me.btnDispenserAutoValveCalibration1 = New System.Windows.Forms.Button()
        Me.btnChangeGluePos = New System.Windows.Forms.Button()
        Me.btnClearGlue1 = New System.Windows.Forms.Button()
        Me.btnDispenserAutoSearch1 = New System.Windows.Forms.Button()
        Me.btnPurge1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbValve = New System.Windows.Forms.ComboBox()
        Me.btnPm1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 300
        '
        'btnValve1Weight
        '
        Me.btnValve1Weight.BackColor = System.Drawing.SystemColors.Control
        Me.btnValve1Weight.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Weight
        resources.ApplyResources(Me.btnValve1Weight, "btnValve1Weight")
        Me.btnValve1Weight.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnValve1Weight.FlatAppearance.BorderSize = 0
        Me.btnValve1Weight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnValve1Weight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnValve1Weight.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnValve1Weight.Name = "btnValve1Weight"
        Me.ToolTip.SetToolTip(Me.btnValve1Weight, resources.GetString("btnValve1Weight.ToolTip"))
        Me.btnValve1Weight.UseVisualStyleBackColor = True
        '
        'btnDispenserAutoValveCalibration1
        '
        Me.btnDispenserAutoValveCalibration1.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnDispenserAutoValveCalibration1, "btnDispenserAutoValveCalibration1")
        Me.btnDispenserAutoValveCalibration1.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnDispenserAutoValveCalibration1.FlatAppearance.BorderSize = 0
        Me.btnDispenserAutoValveCalibration1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnDispenserAutoValveCalibration1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnDispenserAutoValveCalibration1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnDispenserAutoValveCalibration1.Name = "btnDispenserAutoValveCalibration1"
        Me.ToolTip.SetToolTip(Me.btnDispenserAutoValveCalibration1, resources.GetString("btnDispenserAutoValveCalibration1.ToolTip"))
        Me.btnDispenserAutoValveCalibration1.UseVisualStyleBackColor = True
        '
        'btnChangeGluePos
        '
        Me.btnChangeGluePos.BackColor = System.Drawing.SystemColors.Control
        Me.btnChangeGluePos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Park
        resources.ApplyResources(Me.btnChangeGluePos, "btnChangeGluePos")
        Me.btnChangeGluePos.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnChangeGluePos.FlatAppearance.BorderSize = 0
        Me.btnChangeGluePos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnChangeGluePos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnChangeGluePos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnChangeGluePos.Name = "btnChangeGluePos"
        Me.ToolTip.SetToolTip(Me.btnChangeGluePos, resources.GetString("btnChangeGluePos.ToolTip"))
        Me.btnChangeGluePos.UseVisualStyleBackColor = True
        '
        'btnClearGlue1
        '
        Me.btnClearGlue1.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnClearGlue1, "btnClearGlue1")
        Me.btnClearGlue1.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnClearGlue1.FlatAppearance.BorderSize = 0
        Me.btnClearGlue1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnClearGlue1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnClearGlue1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnClearGlue1.Name = "btnClearGlue1"
        Me.ToolTip.SetToolTip(Me.btnClearGlue1, resources.GetString("btnClearGlue1.ToolTip"))
        Me.btnClearGlue1.UseVisualStyleBackColor = True
        '
        'btnDispenserAutoSearch1
        '
        Me.btnDispenserAutoSearch1.BackColor = System.Drawing.SystemColors.Control
        Me.btnDispenserAutoSearch1.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.ZHeightSearch
        resources.ApplyResources(Me.btnDispenserAutoSearch1, "btnDispenserAutoSearch1")
        Me.btnDispenserAutoSearch1.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnDispenserAutoSearch1.FlatAppearance.BorderSize = 0
        Me.btnDispenserAutoSearch1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnDispenserAutoSearch1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnDispenserAutoSearch1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnDispenserAutoSearch1.Name = "btnDispenserAutoSearch1"
        Me.ToolTip.SetToolTip(Me.btnDispenserAutoSearch1, resources.GetString("btnDispenserAutoSearch1.ToolTip"))
        Me.btnDispenserAutoSearch1.UseVisualStyleBackColor = True
        '
        'btnPurge1
        '
        Me.btnPurge1.BackColor = System.Drawing.SystemColors.Control
        Me.btnPurge1.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Purge1
        resources.ApplyResources(Me.btnPurge1, "btnPurge1")
        Me.btnPurge1.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnPurge1.FlatAppearance.BorderSize = 0
        Me.btnPurge1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnPurge1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnPurge1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnPurge1.Name = "btnPurge1"
        Me.ToolTip.SetToolTip(Me.btnPurge1, resources.GetString("btnPurge1.ToolTip"))
        Me.btnPurge1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'cmbValve
        '
        resources.ApplyResources(Me.cmbValve, "cmbValve")
        Me.cmbValve.FormattingEnabled = True
        Me.cmbValve.Name = "cmbValve"
        '
        'btnPm1
        '
        Me.btnPm1.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnPm1, "btnPm1")
        Me.btnPm1.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnPm1.FlatAppearance.BorderSize = 0
        Me.btnPm1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnPm1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnPm1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnPm1.Name = "btnPm1"
        Me.ToolTip.SetToolTip(Me.btnPm1, resources.GetString("btnPm1.ToolTip"))
        Me.btnPm1.UseVisualStyleBackColor = True
        '
        'frmOpStage
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.btnPm1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbValve)
        Me.Controls.Add(Me.btnValve1Weight)
        Me.Controls.Add(Me.btnDispenserAutoValveCalibration1)
        Me.Controls.Add(Me.btnChangeGluePos)
        Me.Controls.Add(Me.btnClearGlue1)
        Me.Controls.Add(Me.btnDispenserAutoSearch1)
        Me.Controls.Add(Me.btnPurge1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpStage"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnValve1Weight As System.Windows.Forms.Button
    Friend WithEvents btnDispenserAutoValveCalibration1 As System.Windows.Forms.Button
    Friend WithEvents btnChangeGluePos As System.Windows.Forms.Button
    Friend WithEvents btnClearGlue1 As System.Windows.Forms.Button
    Friend WithEvents btnDispenserAutoSearch1 As System.Windows.Forms.Button
    Friend WithEvents btnPurge1 As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbValve As System.Windows.Forms.ComboBox
    Friend WithEvents btnPm1 As System.Windows.Forms.Button
End Class

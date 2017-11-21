<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.IOUseTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TmrSystemState = New System.Windows.Forms.Timer(Me.components)
        Me.btnLogOut = New System.Windows.Forms.RadioButton()
        Me.btnHelp = New System.Windows.Forms.RadioButton()
        Me.btnLogs = New System.Windows.Forms.RadioButton()
        Me.btnEngineMode = New System.Windows.Forms.RadioButton()
        Me.btnCalibration = New System.Windows.Forms.RadioButton()
        Me.btnRecipe = New System.Windows.Forms.RadioButton()
        Me.btnDiagnosis = New System.Windows.Forms.RadioButton()
        Me.btnOpStatus = New System.Windows.Forms.RadioButton()
        Me.btnAirPressure = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'IOUseTimer
        '
        '
        'TmrSystemState
        '
        '
        'btnLogOut
        '
        resources.ApplyResources(Me.btnLogOut, "btnLogOut")
        Me.btnLogOut.BackColor = System.Drawing.Color.Transparent
        Me.btnLogOut.FlatAppearance.BorderSize = 0
        Me.btnLogOut.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnLogOut.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnLogOut.Image = Global.WindowsApplication1.My.Resources.Resources._100Login
        Me.btnLogOut.Name = "btnLogOut"
        Me.btnLogOut.UseVisualStyleBackColor = False
        '
        'btnHelp
        '
        resources.ApplyResources(Me.btnHelp, "btnHelp")
        Me.btnHelp.BackColor = System.Drawing.Color.Transparent
        Me.btnHelp.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnHelp.FlatAppearance.BorderSize = 0
        Me.btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnHelp.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnHelp.Image = Global.WindowsApplication1.My.Resources.Resources._100Help
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Tag = "2"
        Me.btnHelp.UseVisualStyleBackColor = False
        '
        'btnLogs
        '
        resources.ApplyResources(Me.btnLogs, "btnLogs")
        Me.btnLogs.BackColor = System.Drawing.Color.Transparent
        Me.btnLogs.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnLogs.FlatAppearance.BorderSize = 0
        Me.btnLogs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnLogs.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnLogs.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnLogs.Image = Global.WindowsApplication1.My.Resources.Resources._100Log
        Me.btnLogs.Name = "btnLogs"
        Me.btnLogs.Tag = "1"
        Me.btnLogs.UseVisualStyleBackColor = False
        '
        'btnEngineMode
        '
        resources.ApplyResources(Me.btnEngineMode, "btnEngineMode")
        Me.btnEngineMode.BackColor = System.Drawing.Color.Transparent
        Me.btnEngineMode.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnEngineMode.FlatAppearance.BorderSize = 0
        Me.btnEngineMode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnEngineMode.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnEngineMode.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnEngineMode.Image = Global.WindowsApplication1.My.Resources.Resources._100Engineer
        Me.btnEngineMode.Name = "btnEngineMode"
        Me.btnEngineMode.Tag = "1"
        Me.btnEngineMode.UseVisualStyleBackColor = False
        '
        'btnCalibration
        '
        resources.ApplyResources(Me.btnCalibration, "btnCalibration")
        Me.btnCalibration.BackColor = System.Drawing.Color.Transparent
        Me.btnCalibration.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnCalibration.FlatAppearance.BorderSize = 0
        Me.btnCalibration.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnCalibration.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnCalibration.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCalibration.Image = Global.WindowsApplication1.My.Resources.Resources._100Calibration
        Me.btnCalibration.Name = "btnCalibration"
        Me.btnCalibration.Tag = "1"
        Me.btnCalibration.UseVisualStyleBackColor = False
        '
        'btnRecipe
        '
        resources.ApplyResources(Me.btnRecipe, "btnRecipe")
        Me.btnRecipe.BackColor = System.Drawing.Color.Transparent
        Me.btnRecipe.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnRecipe.FlatAppearance.BorderSize = 0
        Me.btnRecipe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnRecipe.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnRecipe.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnRecipe.Image = Global.WindowsApplication1.My.Resources.Resources._100Recipe
        Me.btnRecipe.Name = "btnRecipe"
        Me.btnRecipe.Tag = "1"
        Me.btnRecipe.UseVisualStyleBackColor = False
        '
        'btnDiagnosis
        '
        resources.ApplyResources(Me.btnDiagnosis, "btnDiagnosis")
        Me.btnDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.btnDiagnosis.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnDiagnosis.FlatAppearance.BorderSize = 0
        Me.btnDiagnosis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnDiagnosis.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnDiagnosis.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnDiagnosis.Image = Global.WindowsApplication1.My.Resources.Resources._100Diagnosis
        Me.btnDiagnosis.Name = "btnDiagnosis"
        Me.btnDiagnosis.UseVisualStyleBackColor = False
        '
        'btnOpStatus
        '
        resources.ApplyResources(Me.btnOpStatus, "btnOpStatus")
        Me.btnOpStatus.BackColor = System.Drawing.Color.Transparent
        Me.btnOpStatus.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._170MouseUp
        Me.btnOpStatus.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnOpStatus.FlatAppearance.BorderSize = 0
        Me.btnOpStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnOpStatus.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnOpStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnOpStatus.Image = Global.WindowsApplication1.My.Resources.Resources._100Production
        Me.btnOpStatus.Name = "btnOpStatus"
        Me.btnOpStatus.UseVisualStyleBackColor = False
        '
        'btnAirPressure
        '
        resources.ApplyResources(Me.btnAirPressure, "btnAirPressure")
        Me.btnAirPressure.BackColor = System.Drawing.Color.Transparent
        Me.btnAirPressure.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._170MouseUp
        Me.btnAirPressure.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnAirPressure.FlatAppearance.BorderSize = 0
        Me.btnAirPressure.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnAirPressure.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnAirPressure.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnAirPressure.Name = "btnAirPressure"
        Me.btnAirPressure.UseVisualStyleBackColor = False
        '
        'frmMain
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.wallpaper
        Me.Controls.Add(Me.btnAirPressure)
        Me.Controls.Add(Me.btnLogOut)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.btnLogs)
        Me.Controls.Add(Me.btnEngineMode)
        Me.Controls.Add(Me.btnCalibration)
        Me.Controls.Add(Me.btnRecipe)
        Me.Controls.Add(Me.btnDiagnosis)
        Me.Controls.Add(Me.btnOpStatus)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IOUseTimer As System.Windows.Forms.Timer
    Friend WithEvents btnEngineMode As System.Windows.Forms.RadioButton
    Friend WithEvents TmrSystemState As System.Windows.Forms.Timer
    Friend WithEvents btnRecipe As System.Windows.Forms.RadioButton
    Friend WithEvents btnCalibration As System.Windows.Forms.RadioButton
    Friend WithEvents btnLogs As System.Windows.Forms.RadioButton
    Friend WithEvents btnHelp As System.Windows.Forms.RadioButton
    Friend WithEvents btnOpStatus As System.Windows.Forms.RadioButton
    Friend WithEvents btnDiagnosis As System.Windows.Forms.RadioButton
    Friend WithEvents btnLogOut As System.Windows.Forms.RadioButton
    Friend WithEvents btnAirPressure As System.Windows.Forms.RadioButton

End Class

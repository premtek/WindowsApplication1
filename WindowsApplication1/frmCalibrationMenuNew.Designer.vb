<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalibrationMenuNew
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalibrationMenuNew))
        Me.grpStage1 = New System.Windows.Forms.GroupBox()
        Me.btnStageVerification_New = New System.Windows.Forms.Button()
        Me.btnContactIni = New System.Windows.Forms.Button()
        Me.cmbValveNum = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbStageNum = New System.Windows.Forms.ComboBox()
        Me.btnValveCalibration = New System.Windows.Forms.Button()
        Me.btnValveWeight = New System.Windows.Forms.Button()
        Me.btnStageVerification = New System.Windows.Forms.Button()
        Me.btnCCDImage = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnPidController = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpStage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpStage1
        '
        Me.grpStage1.Controls.Add(Me.btnStageVerification_New)
        Me.grpStage1.Controls.Add(Me.btnContactIni)
        Me.grpStage1.Controls.Add(Me.cmbValveNum)
        Me.grpStage1.Controls.Add(Me.Label1)
        Me.grpStage1.Controls.Add(Me.Label8)
        Me.grpStage1.Controls.Add(Me.cmbStageNum)
        Me.grpStage1.Controls.Add(Me.btnValveCalibration)
        Me.grpStage1.Controls.Add(Me.btnValveWeight)
        Me.grpStage1.Controls.Add(Me.btnStageVerification)
        Me.grpStage1.Controls.Add(Me.btnCCDImage)
        resources.ApplyResources(Me.grpStage1, "grpStage1")
        Me.grpStage1.Name = "grpStage1"
        Me.grpStage1.TabStop = False
        '
        'btnStageVerification_New
        '
        Me.btnStageVerification_New.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnStageVerification_New, "btnStageVerification_New")
        Me.btnStageVerification_New.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnStageVerification_New.FlatAppearance.BorderSize = 0
        Me.btnStageVerification_New.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnStageVerification_New.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnStageVerification_New.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnStageVerification_New.Name = "btnStageVerification_New"
        Me.btnStageVerification_New.Tag = "0"
        Me.btnStageVerification_New.UseVisualStyleBackColor = True
        '
        'btnContactIni
        '
        Me.btnContactIni.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnContactIni, "btnContactIni")
        Me.btnContactIni.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnContactIni.FlatAppearance.BorderSize = 0
        Me.btnContactIni.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnContactIni.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnContactIni.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnContactIni.Name = "btnContactIni"
        Me.btnContactIni.Tag = "0"
        Me.btnContactIni.UseVisualStyleBackColor = True
        '
        'cmbValveNum
        '
        Me.cmbValveNum.FormattingEnabled = True
        resources.ApplyResources(Me.cmbValveNum, "cmbValveNum")
        Me.cmbValveNum.Name = "cmbValveNum"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'cmbStageNum
        '
        Me.cmbStageNum.FormattingEnabled = True
        resources.ApplyResources(Me.cmbStageNum, "cmbStageNum")
        Me.cmbStageNum.Name = "cmbStageNum"
        '
        'btnValveCalibration
        '
        Me.btnValveCalibration.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnValveCalibration, "btnValveCalibration")
        Me.btnValveCalibration.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnValveCalibration.FlatAppearance.BorderSize = 0
        Me.btnValveCalibration.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnValveCalibration.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnValveCalibration.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnValveCalibration.Name = "btnValveCalibration"
        Me.btnValveCalibration.Tag = "0"
        Me.btnValveCalibration.UseVisualStyleBackColor = True
        '
        'btnValveWeight
        '
        Me.btnValveWeight.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnValveWeight, "btnValveWeight")
        Me.btnValveWeight.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnValveWeight.FlatAppearance.BorderSize = 0
        Me.btnValveWeight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnValveWeight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnValveWeight.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnValveWeight.Name = "btnValveWeight"
        Me.btnValveWeight.Tag = "0"
        Me.btnValveWeight.UseVisualStyleBackColor = True
        '
        'btnStageVerification
        '
        Me.btnStageVerification.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnStageVerification, "btnStageVerification")
        Me.btnStageVerification.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnStageVerification.FlatAppearance.BorderSize = 0
        Me.btnStageVerification.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnStageVerification.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnStageVerification.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnStageVerification.Name = "btnStageVerification"
        Me.btnStageVerification.Tag = "0"
        Me.btnStageVerification.UseVisualStyleBackColor = True
        '
        'btnCCDImage
        '
        Me.btnCCDImage.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnCCDImage, "btnCCDImage")
        Me.btnCCDImage.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnCCDImage.FlatAppearance.BorderSize = 0
        Me.btnCCDImage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnCCDImage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnCCDImage.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCCDImage.Name = "btnCCDImage"
        Me.btnCCDImage.Tag = "0"
        Me.btnCCDImage.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.SystemColors.Control
        Me.btnBack.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        resources.ApplyResources(Me.btnBack, "btnBack")
        Me.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnBack.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnBack.Name = "btnBack"
        Me.ToolTip1.SetToolTip(Me.btnBack, resources.GetString("btnBack.ToolTip"))
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnPidController
        '
        Me.btnPidController.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnPidController, "btnPidController")
        Me.btnPidController.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnPidController.FlatAppearance.BorderSize = 0
        Me.btnPidController.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnPidController.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnPidController.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnPidController.Name = "btnPidController"
        Me.btnPidController.Tag = "0"
        Me.ToolTip1.SetToolTip(Me.btnPidController, resources.GetString("btnPidController.ToolTip"))
        Me.btnPidController.UseVisualStyleBackColor = True
        '
        'frmCalibrationMenuNew
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnPidController)
        Me.Controls.Add(Me.grpStage1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCalibrationMenuNew"
        Me.grpStage1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnPidController As System.Windows.Forms.Button
    Friend WithEvents grpStage1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbValveNum As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbStageNum As System.Windows.Forms.ComboBox
    Friend WithEvents btnValveCalibration As System.Windows.Forms.Button
    Friend WithEvents btnValveWeight As System.Windows.Forms.Button
    Friend WithEvents btnStageVerification As System.Windows.Forms.Button
    Friend WithEvents btnCCDImage As System.Windows.Forms.Button
    Friend WithEvents btnContactIni As System.Windows.Forms.Button
    Friend WithEvents btnStageVerification_New As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

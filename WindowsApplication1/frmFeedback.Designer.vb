<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFeedback
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFeedback))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtMemo = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.nmuAcceptLevel = New System.Windows.Forms.NumericUpDown()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.nmuKi = New System.Windows.Forms.NumericUpDown()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.nmuAverageCount = New System.Windows.Forms.NumericUpDown()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.nmuDataCount = New System.Windows.Forms.NumericUpDown()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.nmuTargetVolume = New System.Windows.Forms.NumericUpDown()
        Me.lblTargetValue = New System.Windows.Forms.Label()
        Me.nmuKp = New System.Windows.Forms.NumericUpDown()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.cboControlType = New System.Windows.Forms.ComboBox()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        CType(Me.nmuAcceptLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuKi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuAverageCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuDataCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuTargetVolume, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuKp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Controls.Add(Me.txtMemo)
        Me.GroupBox2.Controls.Add(Me.Label43)
        Me.GroupBox2.Controls.Add(Me.nmuAcceptLevel)
        Me.GroupBox2.Controls.Add(Me.Label42)
        Me.GroupBox2.Controls.Add(Me.nmuKi)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.nmuAverageCount)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.btnReset)
        Me.GroupBox2.Controls.Add(Me.nmuDataCount)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.nmuTargetVolume)
        Me.GroupBox2.Controls.Add(Me.lblTargetValue)
        Me.GroupBox2.Controls.Add(Me.nmuKp)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox2, resources.GetString("GroupBox2.ToolTip"))
        '
        'txtMemo
        '
        resources.ApplyResources(Me.txtMemo, "txtMemo")
        Me.txtMemo.Name = "txtMemo"
        Me.ToolTip1.SetToolTip(Me.txtMemo, resources.GetString("txtMemo.ToolTip"))
        '
        'Label43
        '
        resources.ApplyResources(Me.Label43, "Label43")
        Me.Label43.Name = "Label43"
        Me.ToolTip1.SetToolTip(Me.Label43, resources.GetString("Label43.ToolTip"))
        '
        'nmuAcceptLevel
        '
        resources.ApplyResources(Me.nmuAcceptLevel, "nmuAcceptLevel")
        Me.nmuAcceptLevel.BackColor = System.Drawing.Color.White
        Me.nmuAcceptLevel.Maximum = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.nmuAcceptLevel.Name = "nmuAcceptLevel"
        Me.ToolTip1.SetToolTip(Me.nmuAcceptLevel, resources.GetString("nmuAcceptLevel.ToolTip"))
        Me.nmuAcceptLevel.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label42
        '
        resources.ApplyResources(Me.Label42, "Label42")
        Me.Label42.Name = "Label42"
        Me.ToolTip1.SetToolTip(Me.Label42, resources.GetString("Label42.ToolTip"))
        '
        'nmuKi
        '
        resources.ApplyResources(Me.nmuKi, "nmuKi")
        Me.nmuKi.BackColor = System.Drawing.Color.White
        Me.nmuKi.DecimalPlaces = 4
        Me.nmuKi.Increment = New Decimal(New Integer() {1, 0, 0, 262144})
        Me.nmuKi.Name = "nmuKi"
        Me.ToolTip1.SetToolTip(Me.nmuKi, resources.GetString("nmuKi.ToolTip"))
        Me.nmuKi.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label19
        '
        resources.ApplyResources(Me.Label19, "Label19")
        Me.Label19.Name = "Label19"
        Me.ToolTip1.SetToolTip(Me.Label19, resources.GetString("Label19.ToolTip"))
        '
        'nmuAverageCount
        '
        resources.ApplyResources(Me.nmuAverageCount, "nmuAverageCount")
        Me.nmuAverageCount.BackColor = System.Drawing.Color.White
        Me.nmuAverageCount.Name = "nmuAverageCount"
        Me.ToolTip1.SetToolTip(Me.nmuAverageCount, resources.GetString("nmuAverageCount.ToolTip"))
        Me.nmuAverageCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label18
        '
        resources.ApplyResources(Me.Label18, "Label18")
        Me.Label18.Name = "Label18"
        Me.ToolTip1.SetToolTip(Me.Label18, resources.GetString("Label18.ToolTip"))
        '
        'btnReset
        '
        resources.ApplyResources(Me.btnReset, "btnReset")
        Me.btnReset.BackColor = System.Drawing.SystemColors.Control
        Me.btnReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnReset.Name = "btnReset"
        Me.ToolTip1.SetToolTip(Me.btnReset, resources.GetString("btnReset.ToolTip"))
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'nmuDataCount
        '
        resources.ApplyResources(Me.nmuDataCount, "nmuDataCount")
        Me.nmuDataCount.BackColor = System.Drawing.Color.White
        Me.nmuDataCount.Name = "nmuDataCount"
        Me.ToolTip1.SetToolTip(Me.nmuDataCount, resources.GetString("nmuDataCount.ToolTip"))
        Me.nmuDataCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        Me.ToolTip1.SetToolTip(Me.Label12, resources.GetString("Label12.ToolTip"))
        '
        'nmuTargetVolume
        '
        resources.ApplyResources(Me.nmuTargetVolume, "nmuTargetVolume")
        Me.nmuTargetVolume.BackColor = System.Drawing.Color.White
        Me.nmuTargetVolume.Maximum = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.nmuTargetVolume.Name = "nmuTargetVolume"
        Me.ToolTip1.SetToolTip(Me.nmuTargetVolume, resources.GetString("nmuTargetVolume.ToolTip"))
        '
        'lblTargetValue
        '
        resources.ApplyResources(Me.lblTargetValue, "lblTargetValue")
        Me.lblTargetValue.Name = "lblTargetValue"
        Me.ToolTip1.SetToolTip(Me.lblTargetValue, resources.GetString("lblTargetValue.ToolTip"))
        '
        'nmuKp
        '
        resources.ApplyResources(Me.nmuKp, "nmuKp")
        Me.nmuKp.BackColor = System.Drawing.Color.White
        Me.nmuKp.DecimalPlaces = 4
        Me.nmuKp.Increment = New Decimal(New Integer() {1, 0, 0, 262144})
        Me.nmuKp.Name = "nmuKp"
        Me.ToolTip1.SetToolTip(Me.nmuKp, resources.GetString("nmuKp.ToolTip"))
        Me.nmuKp.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.Name = "Label15"
        Me.ToolTip1.SetToolTip(Me.Label15, resources.GetString("Label15.ToolTip"))
        '
        'Label33
        '
        resources.ApplyResources(Me.Label33, "Label33")
        Me.Label33.Name = "Label33"
        Me.ToolTip1.SetToolTip(Me.Label33, resources.GetString("Label33.ToolTip"))
        '
        'cboControlType
        '
        resources.ApplyResources(Me.cboControlType, "cboControlType")
        Me.cboControlType.BackColor = System.Drawing.Color.White
        Me.cboControlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboControlType.FormattingEnabled = True
        Me.cboControlType.Name = "cboControlType"
        Me.ToolTip1.SetToolTip(Me.cboControlType, resources.GetString("cboControlType.ToolTip"))
        '
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        Me.ToolTip1.SetToolTip(Me.UcStatusBar1, resources.GetString("UcStatusBar1.ToolTip"))
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Name = "btnCancel"
        Me.ToolTip1.SetToolTip(Me.btnCancel, resources.GetString("btnCancel.ToolTip"))
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnOK.Name = "btnOK"
        Me.ToolTip1.SetToolTip(Me.btnOK, resources.GetString("btnOK.ToolTip"))
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmFeedback
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.Label33)
        Me.Controls.Add(Me.cboControlType)
        Me.Controls.Add(Me.GroupBox2)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFeedback"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.nmuAcceptLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuKi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuAverageCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuDataCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuTargetVolume, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuKp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents nmuAcceptLevel As System.Windows.Forms.NumericUpDown
    Public WithEvents nmuKi As System.Windows.Forms.NumericUpDown
    Public WithEvents nmuAverageCount As System.Windows.Forms.NumericUpDown
    Public WithEvents nmuDataCount As System.Windows.Forms.NumericUpDown
    Public WithEvents nmuTargetVolume As System.Windows.Forms.NumericUpDown
    Public WithEvents nmuKp As System.Windows.Forms.NumericUpDown
    Public WithEvents txtMemo As System.Windows.Forms.TextBox
    Public WithEvents lblTargetValue As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents cboControlType As System.Windows.Forms.ComboBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

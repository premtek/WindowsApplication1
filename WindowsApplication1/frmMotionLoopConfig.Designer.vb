<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMotionLoopConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMotionLoopConfig))
        Me.lblDelayTimeUnit = New System.Windows.Forms.Label()
        Me.lblDecelerationUnit = New System.Windows.Forms.Label()
        Me.lblAccelerationUnit = New System.Windows.Forms.Label()
        Me.lblMaxVelocityUnit = New System.Windows.Forms.Label()
        Me.lblStartVelocityUnit = New System.Windows.Forms.Label()
        Me.nmuStableTime = New System.Windows.Forms.NumericUpDown()
        Me.nmuDeleration = New System.Windows.Forms.NumericUpDown()
        Me.nmuAcceleration = New System.Windows.Forms.NumericUpDown()
        Me.nmuVelHigh = New System.Windows.Forms.NumericUpDown()
        Me.nmuVelLow = New System.Windows.Forms.NumericUpDown()
        Me.lblDelayTime = New System.Windows.Forms.Label()
        Me.lblDeceleration = New System.Windows.Forms.Label()
        Me.lblAcceleration = New System.Windows.Forms.Label()
        Me.lblMaxVelocity = New System.Windows.Forms.Label()
        Me.lblStartVelocity = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnHide = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.nmuStableTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuDeleration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuAcceleration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuVelHigh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuVelLow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDelayTimeUnit
        '
        resources.ApplyResources(Me.lblDelayTimeUnit, "lblDelayTimeUnit")
        Me.lblDelayTimeUnit.Name = "lblDelayTimeUnit"
        Me.ToolTip1.SetToolTip(Me.lblDelayTimeUnit, resources.GetString("lblDelayTimeUnit.ToolTip"))
        '
        'lblDecelerationUnit
        '
        resources.ApplyResources(Me.lblDecelerationUnit, "lblDecelerationUnit")
        Me.lblDecelerationUnit.Name = "lblDecelerationUnit"
        Me.ToolTip1.SetToolTip(Me.lblDecelerationUnit, resources.GetString("lblDecelerationUnit.ToolTip"))
        '
        'lblAccelerationUnit
        '
        resources.ApplyResources(Me.lblAccelerationUnit, "lblAccelerationUnit")
        Me.lblAccelerationUnit.Name = "lblAccelerationUnit"
        Me.ToolTip1.SetToolTip(Me.lblAccelerationUnit, resources.GetString("lblAccelerationUnit.ToolTip"))
        '
        'lblMaxVelocityUnit
        '
        resources.ApplyResources(Me.lblMaxVelocityUnit, "lblMaxVelocityUnit")
        Me.lblMaxVelocityUnit.Name = "lblMaxVelocityUnit"
        Me.ToolTip1.SetToolTip(Me.lblMaxVelocityUnit, resources.GetString("lblMaxVelocityUnit.ToolTip"))
        '
        'lblStartVelocityUnit
        '
        resources.ApplyResources(Me.lblStartVelocityUnit, "lblStartVelocityUnit")
        Me.lblStartVelocityUnit.Name = "lblStartVelocityUnit"
        Me.ToolTip1.SetToolTip(Me.lblStartVelocityUnit, resources.GetString("lblStartVelocityUnit.ToolTip"))
        '
        'nmuStableTime
        '
        resources.ApplyResources(Me.nmuStableTime, "nmuStableTime")
        Me.nmuStableTime.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nmuStableTime.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.nmuStableTime.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nmuStableTime.Name = "nmuStableTime"
        Me.ToolTip1.SetToolTip(Me.nmuStableTime, resources.GetString("nmuStableTime.ToolTip"))
        Me.nmuStableTime.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'nmuDeleration
        '
        resources.ApplyResources(Me.nmuDeleration, "nmuDeleration")
        Me.nmuDeleration.Maximum = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.nmuDeleration.Name = "nmuDeleration"
        Me.ToolTip1.SetToolTip(Me.nmuDeleration, resources.GetString("nmuDeleration.ToolTip"))
        Me.nmuDeleration.Value = New Decimal(New Integer() {980, 0, 0, 0})
        '
        'nmuAcceleration
        '
        resources.ApplyResources(Me.nmuAcceleration, "nmuAcceleration")
        Me.nmuAcceleration.Maximum = New Decimal(New Integer() {999999, 0, 0, 0})
        Me.nmuAcceleration.Name = "nmuAcceleration"
        Me.ToolTip1.SetToolTip(Me.nmuAcceleration, resources.GetString("nmuAcceleration.ToolTip"))
        Me.nmuAcceleration.Value = New Decimal(New Integer() {980, 0, 0, 0})
        '
        'nmuVelHigh
        '
        resources.ApplyResources(Me.nmuVelHigh, "nmuVelHigh")
        Me.nmuVelHigh.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmuVelHigh.Name = "nmuVelHigh"
        Me.ToolTip1.SetToolTip(Me.nmuVelHigh, resources.GetString("nmuVelHigh.ToolTip"))
        Me.nmuVelHigh.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'nmuVelLow
        '
        resources.ApplyResources(Me.nmuVelLow, "nmuVelLow")
        Me.nmuVelLow.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.nmuVelLow.Name = "nmuVelLow"
        Me.ToolTip1.SetToolTip(Me.nmuVelLow, resources.GetString("nmuVelLow.ToolTip"))
        '
        'lblDelayTime
        '
        resources.ApplyResources(Me.lblDelayTime, "lblDelayTime")
        Me.lblDelayTime.Name = "lblDelayTime"
        Me.ToolTip1.SetToolTip(Me.lblDelayTime, resources.GetString("lblDelayTime.ToolTip"))
        '
        'lblDeceleration
        '
        resources.ApplyResources(Me.lblDeceleration, "lblDeceleration")
        Me.lblDeceleration.Name = "lblDeceleration"
        Me.ToolTip1.SetToolTip(Me.lblDeceleration, resources.GetString("lblDeceleration.ToolTip"))
        '
        'lblAcceleration
        '
        resources.ApplyResources(Me.lblAcceleration, "lblAcceleration")
        Me.lblAcceleration.Name = "lblAcceleration"
        Me.ToolTip1.SetToolTip(Me.lblAcceleration, resources.GetString("lblAcceleration.ToolTip"))
        '
        'lblMaxVelocity
        '
        resources.ApplyResources(Me.lblMaxVelocity, "lblMaxVelocity")
        Me.lblMaxVelocity.Name = "lblMaxVelocity"
        Me.ToolTip1.SetToolTip(Me.lblMaxVelocity, resources.GetString("lblMaxVelocity.ToolTip"))
        '
        'lblStartVelocity
        '
        resources.ApplyResources(Me.lblStartVelocity, "lblStartVelocity")
        Me.lblStartVelocity.Name = "lblStartVelocity"
        Me.ToolTip1.SetToolTip(Me.lblStartVelocity, resources.GetString("lblStartVelocity.ToolTip"))
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Name = "Label11"
        Me.ToolTip1.SetToolTip(Me.Label11, resources.GetString("Label11.ToolTip"))
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Name = "Label12"
        Me.ToolTip1.SetToolTip(Me.Label12, resources.GetString("Label12.ToolTip"))
        '
        'btnSave
        '
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnSave.Name = "btnSave"
        Me.ToolTip1.SetToolTip(Me.btnSave, resources.GetString("btnSave.ToolTip"))
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnHide
        '
        resources.ApplyResources(Me.btnHide, "btnHide")
        Me.btnHide.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnHide.Name = "btnHide"
        Me.ToolTip1.SetToolTip(Me.btnHide, resources.GetString("btnHide.ToolTip"))
        Me.btnHide.UseVisualStyleBackColor = True
        '
        'frmMotionLoopConfig
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnHide)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblDelayTimeUnit)
        Me.Controls.Add(Me.lblDecelerationUnit)
        Me.Controls.Add(Me.lblAccelerationUnit)
        Me.Controls.Add(Me.lblMaxVelocityUnit)
        Me.Controls.Add(Me.lblStartVelocityUnit)
        Me.Controls.Add(Me.nmuStableTime)
        Me.Controls.Add(Me.nmuDeleration)
        Me.Controls.Add(Me.nmuAcceleration)
        Me.Controls.Add(Me.nmuVelHigh)
        Me.Controls.Add(Me.nmuVelLow)
        Me.Controls.Add(Me.lblDelayTime)
        Me.Controls.Add(Me.lblDeceleration)
        Me.Controls.Add(Me.lblAcceleration)
        Me.Controls.Add(Me.lblMaxVelocity)
        Me.Controls.Add(Me.lblStartVelocity)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMotionLoopConfig"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        CType(Me.nmuStableTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuDeleration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuAcceleration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuVelHigh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuVelLow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblDelayTimeUnit As System.Windows.Forms.Label
    Friend WithEvents lblDecelerationUnit As System.Windows.Forms.Label
    Friend WithEvents lblAccelerationUnit As System.Windows.Forms.Label
    Friend WithEvents lblMaxVelocityUnit As System.Windows.Forms.Label
    Friend WithEvents lblStartVelocityUnit As System.Windows.Forms.Label
    Friend WithEvents nmuStableTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuDeleration As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuAcceleration As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuVelHigh As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuVelLow As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblDelayTime As System.Windows.Forms.Label
    Friend WithEvents lblDeceleration As System.Windows.Forms.Label
    Friend WithEvents lblAcceleration As System.Windows.Forms.Label
    Friend WithEvents lblMaxVelocity As System.Windows.Forms.Label
    Friend WithEvents lblStartVelocity As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnHide As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

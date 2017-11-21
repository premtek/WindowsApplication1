<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucIndicatorConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucIndicatorConfig))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btnIndicator = New System.Windows.Forms.Button()
        Me.nmcCycleTime = New System.Windows.Forms.NumericUpDown()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.nmcPulseTime = New System.Windows.Forms.NumericUpDown()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nmcCycleTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcPulseTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.Label28)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.btnIndicator)
        Me.GroupBox1.Controls.Add(Me.nmcCycleTime)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.nmcPulseTime)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'Label28
        '
        resources.ApplyResources(Me.Label28, "Label28")
        Me.Label28.Name = "Label28"
        '
        'Label27
        '
        resources.ApplyResources(Me.Label27, "Label27")
        Me.Label27.Name = "Label27"
        '
        'btnIndicator
        '
        resources.ApplyResources(Me.btnIndicator, "btnIndicator")
        Me.btnIndicator.Name = "btnIndicator"
        Me.btnIndicator.UseVisualStyleBackColor = True
        '
        'nmcCycleTime
        '
        resources.ApplyResources(Me.nmcCycleTime, "nmcCycleTime")
        Me.nmcCycleTime.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nmcCycleTime.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmcCycleTime.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nmcCycleTime.Name = "nmcCycleTime"
        Me.nmcCycleTime.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'Label25
        '
        resources.ApplyResources(Me.Label25, "Label25")
        Me.Label25.Name = "Label25"
        '
        'nmcPulseTime
        '
        resources.ApplyResources(Me.nmcPulseTime, "nmcPulseTime")
        Me.nmcPulseTime.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nmcPulseTime.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmcPulseTime.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nmcPulseTime.Name = "nmcPulseTime"
        Me.nmcPulseTime.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label26
        '
        resources.ApplyResources(Me.Label26, "Label26")
        Me.Label26.Name = "Label26"
        '
        'ucIndicatorConfig
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ucIndicatorConfig"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nmcCycleTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcPulseTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents btnIndicator As System.Windows.Forms.Button
    Friend WithEvents nmcCycleTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents nmcPulseTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label26 As System.Windows.Forms.Label

End Class

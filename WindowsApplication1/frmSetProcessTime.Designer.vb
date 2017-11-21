<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetProcessTime
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetProcessTime))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblProcessTimeType = New System.Windows.Forms.Label()
        Me.cboProcessTimeType = New System.Windows.Forms.ComboBox()
        Me.dgvPressTime = New System.Windows.Forms.DataGridView()
        Me.Num = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProcessTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnMin = New System.Windows.Forms.RadioButton()
        Me.rbtnMax = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnProcessTimeClose = New System.Windows.Forms.Button()
        Me.btnProcessTimeSave = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.numDieCount = New System.Windows.Forms.NumericUpDown()
        Me.lblDot = New System.Windows.Forms.Label()
        CType(Me.dgvPressTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.numDieCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblProcessTimeType
        '
        resources.ApplyResources(Me.lblProcessTimeType, "lblProcessTimeType")
        Me.lblProcessTimeType.Name = "lblProcessTimeType"
        '
        'cboProcessTimeType
        '
        resources.ApplyResources(Me.cboProcessTimeType, "cboProcessTimeType")
        Me.cboProcessTimeType.FormattingEnabled = True
        Me.cboProcessTimeType.Name = "cboProcessTimeType"
        '
        'dgvPressTime
        '
        Me.dgvPressTime.AllowUserToAddRows = False
        Me.dgvPressTime.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("微軟正黑體", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPressTime.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        resources.ApplyResources(Me.dgvPressTime, "dgvPressTime")
        Me.dgvPressTime.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Num, Me.ProcessTime, Me.Unit})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("微軟正黑體", 9.0!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPressTime.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvPressTime.Name = "dgvPressTime"
        Me.dgvPressTime.RowHeadersVisible = False
        Me.dgvPressTime.RowTemplate.Height = 24
        '
        'Num
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Num.DefaultCellStyle = DataGridViewCellStyle2
        resources.ApplyResources(Me.Num, "Num")
        Me.Num.Name = "Num"
        '
        'ProcessTime
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ProcessTime.DefaultCellStyle = DataGridViewCellStyle3
        resources.ApplyResources(Me.ProcessTime, "ProcessTime")
        Me.ProcessTime.Name = "ProcessTime"
        '
        'Unit
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Unit.DefaultCellStyle = DataGridViewCellStyle4
        resources.ApplyResources(Me.Unit, "Unit")
        Me.Unit.Name = "Unit"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnMin)
        Me.GroupBox1.Controls.Add(Me.rbtnMax)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'rbtnMin
        '
        resources.ApplyResources(Me.rbtnMin, "rbtnMin")
        Me.rbtnMin.Name = "rbtnMin"
        Me.rbtnMin.UseVisualStyleBackColor = True
        '
        'rbtnMax
        '
        resources.ApplyResources(Me.rbtnMax, "rbtnMax")
        Me.rbtnMax.Checked = True
        Me.rbtnMax.Name = "rbtnMax"
        Me.rbtnMax.TabStop = True
        Me.rbtnMax.UseVisualStyleBackColor = True
        '
        'btnProcessTimeClose
        '
        resources.ApplyResources(Me.btnProcessTimeClose, "btnProcessTimeClose")
        Me.btnProcessTimeClose.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnProcessTimeClose.Name = "btnProcessTimeClose"
        Me.ToolTip1.SetToolTip(Me.btnProcessTimeClose, resources.GetString("btnProcessTimeClose.ToolTip"))
        Me.btnProcessTimeClose.UseVisualStyleBackColor = True
        '
        'btnProcessTimeSave
        '
        resources.ApplyResources(Me.btnProcessTimeSave, "btnProcessTimeSave")
        Me.btnProcessTimeSave.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnProcessTimeSave.Name = "btnProcessTimeSave"
        Me.ToolTip1.SetToolTip(Me.btnProcessTimeSave, resources.GetString("btnProcessTimeSave.ToolTip"))
        Me.btnProcessTimeSave.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.numDieCount)
        Me.GroupBox2.Controls.Add(Me.lblDot)
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'numDieCount
        '
        resources.ApplyResources(Me.numDieCount, "numDieCount")
        Me.numDieCount.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.numDieCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numDieCount.Name = "numDieCount"
        Me.numDieCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblDot
        '
        resources.ApplyResources(Me.lblDot, "lblDot")
        Me.lblDot.Name = "lblDot"
        '
        'frmSetProcessTime
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvPressTime)
        Me.Controls.Add(Me.cboProcessTimeType)
        Me.Controls.Add(Me.lblProcessTimeType)
        Me.Controls.Add(Me.btnProcessTimeClose)
        Me.Controls.Add(Me.btnProcessTimeSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetProcessTime"
        CType(Me.dgvPressTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.numDieCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnProcessTimeClose As System.Windows.Forms.Button
    Friend WithEvents btnProcessTimeSave As System.Windows.Forms.Button
    Friend WithEvents lblProcessTimeType As System.Windows.Forms.Label
    Friend WithEvents cboProcessTimeType As System.Windows.Forms.ComboBox
    Friend WithEvents dgvPressTime As System.Windows.Forms.DataGridView
    Friend WithEvents Num As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProcessTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnMin As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnMax As System.Windows.Forms.RadioButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDot As System.Windows.Forms.Label
    Friend WithEvents numDieCount As System.Windows.Forms.NumericUpDown
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucCCDTargerEdit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucCCDTargerEdit))
        Me.grpEditTargetStep = New System.Windows.Forms.GroupBox()
        Me.dgvStep = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.nmcWidth = New System.Windows.Forms.NumericUpDown()
        Me.lblWidthUnit = New System.Windows.Forms.Label()
        Me.lblWidth = New System.Windows.Forms.Label()
        Me.nmcHeight = New System.Windows.Forms.NumericUpDown()
        Me.lblHeightUnit = New System.Windows.Forms.Label()
        Me.lblHeight = New System.Windows.Forms.Label()
        Me.nmcRadius = New System.Windows.Forms.NumericUpDown()
        Me.lblRadiusUnit = New System.Windows.Forms.Label()
        Me.lblRadius = New System.Windows.Forms.Label()
        Me.cmbTargetColor = New System.Windows.Forms.ComboBox()
        Me.cmbTargetType = New System.Windows.Forms.ComboBox()
        Me.lblColor = New System.Windows.Forms.Label()
        Me.lblType = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.grpEditTargetStep.SuspendLayout()
        CType(Me.dgvStep, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nmcWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcRadius, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpEditTargetStep
        '
        Me.grpEditTargetStep.Controls.Add(Me.dgvStep)
        Me.grpEditTargetStep.Controls.Add(Me.btnSave)
        Me.grpEditTargetStep.Controls.Add(Me.btnAdd)
        Me.grpEditTargetStep.Controls.Add(Me.btnDelete)
        Me.grpEditTargetStep.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.grpEditTargetStep.Location = New System.Drawing.Point(3, 3)
        Me.grpEditTargetStep.Name = "grpEditTargetStep"
        Me.grpEditTargetStep.Size = New System.Drawing.Size(556, 604)
        Me.grpEditTargetStep.TabIndex = 1
        Me.grpEditTargetStep.TabStop = False
        Me.grpEditTargetStep.Text = "EditTargetStep"
        '
        'dgvStep
        '
        Me.dgvStep.AllowUserToAddRows = False
        Me.dgvStep.AllowUserToDeleteRows = False
        Me.dgvStep.AllowUserToResizeRows = False
        Me.dgvStep.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvStep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStep.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column5, Me.Column4})
        Me.dgvStep.Location = New System.Drawing.Point(6, 127)
        Me.dgvStep.Name = "dgvStep"
        Me.dgvStep.RowTemplate.Height = 24
        Me.dgvStep.Size = New System.Drawing.Size(543, 471)
        Me.dgvStep.TabIndex = 45
        '
        'Column1
        '
        Me.Column1.HeaderText = "Type"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 79
        '
        'Column2
        '
        Me.Column2.HeaderText = "Color"
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 85
        '
        'Column3
        '
        Me.Column3.HeaderText = "Radius"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 97
        '
        'Column5
        '
        Me.Column5.HeaderText = "Width"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 91
        '
        'Column4
        '
        Me.Column4.HeaderText = "Height"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 97
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.UcDisplay1)
        Me.GroupBox1.Controls.Add(Me.nmcWidth)
        Me.GroupBox1.Controls.Add(Me.lblWidthUnit)
        Me.GroupBox1.Controls.Add(Me.lblWidth)
        Me.GroupBox1.Controls.Add(Me.nmcHeight)
        Me.GroupBox1.Controls.Add(Me.lblHeightUnit)
        Me.GroupBox1.Controls.Add(Me.lblHeight)
        Me.GroupBox1.Controls.Add(Me.nmcRadius)
        Me.GroupBox1.Controls.Add(Me.lblRadiusUnit)
        Me.GroupBox1.Controls.Add(Me.lblRadius)
        Me.GroupBox1.Controls.Add(Me.cmbTargetColor)
        Me.GroupBox1.Controls.Add(Me.cmbTargetType)
        Me.GroupBox1.Controls.Add(Me.lblColor)
        Me.GroupBox1.Controls.Add(Me.lblType)
        Me.GroupBox1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(565, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(448, 604)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'nmcWidth
        '
        Me.nmcWidth.DecimalPlaces = 3
        Me.nmcWidth.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.nmcWidth.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmcWidth.Location = New System.Drawing.Point(203, 174)
        Me.nmcWidth.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.nmcWidth.Name = "nmcWidth"
        Me.nmcWidth.Size = New System.Drawing.Size(110, 33)
        Me.nmcWidth.TabIndex = 513
        '
        'lblWidthUnit
        '
        Me.lblWidthUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblWidthUnit.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblWidthUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblWidthUnit.Location = New System.Drawing.Point(319, 176)
        Me.lblWidthUnit.Name = "lblWidthUnit"
        Me.lblWidthUnit.Size = New System.Drawing.Size(49, 24)
        Me.lblWidthUnit.TabIndex = 512
        Me.lblWidthUnit.Text = "mm"
        Me.lblWidthUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblWidth
        '
        Me.lblWidth.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblWidth.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblWidth.Location = New System.Drawing.Point(18, 172)
        Me.lblWidth.Name = "lblWidth"
        Me.lblWidth.Size = New System.Drawing.Size(140, 33)
        Me.lblWidth.TabIndex = 511
        Me.lblWidth.Text = "Width"
        Me.lblWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'nmcHeight
        '
        Me.nmcHeight.DecimalPlaces = 3
        Me.nmcHeight.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.nmcHeight.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmcHeight.Location = New System.Drawing.Point(203, 217)
        Me.nmcHeight.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.nmcHeight.Name = "nmcHeight"
        Me.nmcHeight.Size = New System.Drawing.Size(110, 33)
        Me.nmcHeight.TabIndex = 510
        '
        'lblHeightUnit
        '
        Me.lblHeightUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblHeightUnit.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblHeightUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblHeightUnit.Location = New System.Drawing.Point(319, 219)
        Me.lblHeightUnit.Name = "lblHeightUnit"
        Me.lblHeightUnit.Size = New System.Drawing.Size(49, 24)
        Me.lblHeightUnit.TabIndex = 509
        Me.lblHeightUnit.Text = "mm"
        Me.lblHeightUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHeight
        '
        Me.lblHeight.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblHeight.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblHeight.Location = New System.Drawing.Point(18, 215)
        Me.lblHeight.Name = "lblHeight"
        Me.lblHeight.Size = New System.Drawing.Size(140, 33)
        Me.lblHeight.TabIndex = 508
        Me.lblHeight.Text = "Height"
        Me.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'nmcRadius
        '
        Me.nmcRadius.DecimalPlaces = 3
        Me.nmcRadius.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.nmcRadius.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmcRadius.Location = New System.Drawing.Point(203, 131)
        Me.nmcRadius.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.nmcRadius.Name = "nmcRadius"
        Me.nmcRadius.Size = New System.Drawing.Size(110, 33)
        Me.nmcRadius.TabIndex = 507
        '
        'lblRadiusUnit
        '
        Me.lblRadiusUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblRadiusUnit.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblRadiusUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRadiusUnit.Location = New System.Drawing.Point(319, 133)
        Me.lblRadiusUnit.Name = "lblRadiusUnit"
        Me.lblRadiusUnit.Size = New System.Drawing.Size(49, 24)
        Me.lblRadiusUnit.TabIndex = 505
        Me.lblRadiusUnit.Text = "mm"
        Me.lblRadiusUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRadius
        '
        Me.lblRadius.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblRadius.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRadius.Location = New System.Drawing.Point(18, 129)
        Me.lblRadius.Name = "lblRadius"
        Me.lblRadius.Size = New System.Drawing.Size(140, 33)
        Me.lblRadius.TabIndex = 91
        Me.lblRadius.Text = "Radius"
        Me.lblRadius.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbTargetColor
        '
        Me.cmbTargetColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTargetColor.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.cmbTargetColor.FormattingEnabled = True
        Me.cmbTargetColor.Items.AddRange(New Object() {"Black", "Blue", "Red", "Yellow"})
        Me.cmbTargetColor.Location = New System.Drawing.Point(173, 83)
        Me.cmbTargetColor.Name = "cmbTargetColor"
        Me.cmbTargetColor.Size = New System.Drawing.Size(195, 32)
        Me.cmbTargetColor.TabIndex = 90
        '
        'cmbTargetType
        '
        Me.cmbTargetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTargetType.Font = New System.Drawing.Font("微軟正黑體", 14.25!)
        Me.cmbTargetType.FormattingEnabled = True
        Me.cmbTargetType.Items.AddRange(New Object() {"Cross", "CrossX", "TickMark", "TickMarkX", "Circle", "Rectangle"})
        Me.cmbTargetType.Location = New System.Drawing.Point(173, 32)
        Me.cmbTargetType.Name = "cmbTargetType"
        Me.cmbTargetType.Size = New System.Drawing.Size(195, 32)
        Me.cmbTargetType.TabIndex = 89
        '
        'lblColor
        '
        Me.lblColor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblColor.Location = New System.Drawing.Point(18, 83)
        Me.lblColor.Name = "lblColor"
        Me.lblColor.Size = New System.Drawing.Size(140, 33)
        Me.lblColor.TabIndex = 23
        Me.lblColor.Text = "Color"
        Me.lblColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblType
        '
        Me.lblType.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblType.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblType.Location = New System.Drawing.Point(18, 32)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(140, 33)
        Me.lblType.TabIndex = 22
        Me.lblType.Text = "Type"
        Me.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSave
        '
        Me.btnSave.BackgroundImage = CType(resources.GetObject("btnSave.BackgroundImage"), System.Drawing.Image)
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSave.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(173, 45)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(70, 70)
        Me.btnSave.TabIndex = 42
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Add1
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnAdd.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAdd.Location = New System.Drawing.Point(21, 45)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(70, 70)
        Me.btnAdd.TabIndex = 44
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Delete
        Me.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnDelete.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnDelete.Location = New System.Drawing.Point(97, 45)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(70, 70)
        Me.btnDelete.TabIndex = 43
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'UcDisplay1
        '
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UcDisplay1.Location = New System.Drawing.Point(19, 271)
        Me.UcDisplay1.Margin = New System.Windows.Forms.Padding(6)
        Me.UcDisplay1.Name = "UcDisplay1"
        Me.UcDisplay1.Size = New System.Drawing.Size(420, 310)
        Me.UcDisplay1.TabIndex = 514
        '
        'ucCCDTargerEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpEditTargetStep)
        Me.Name = "ucCCDTargerEdit"
        Me.Size = New System.Drawing.Size(1030, 620)
        Me.grpEditTargetStep.ResumeLayout(False)
        CType(Me.dgvStep, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.nmcWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcRadius, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpEditTargetStep As System.Windows.Forms.GroupBox
    Friend WithEvents dgvStep As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents nmcWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblWidthUnit As System.Windows.Forms.Label
    Friend WithEvents lblWidth As System.Windows.Forms.Label
    Friend WithEvents nmcHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblHeightUnit As System.Windows.Forms.Label
    Friend WithEvents lblHeight As System.Windows.Forms.Label
    Friend WithEvents nmcRadius As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblRadiusUnit As System.Windows.Forms.Label
    Friend WithEvents lblRadius As System.Windows.Forms.Label
    Friend WithEvents cmbTargetColor As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTargetType As System.Windows.Forms.ComboBox
    Friend WithEvents lblColor As System.Windows.Forms.Label
    Friend WithEvents lblType As System.Windows.Forms.Label

End Class

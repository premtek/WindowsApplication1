<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIOTable
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIOTable))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabDITable = New System.Windows.Forms.TabPage()
        Me.dgvDITable = New System.Windows.Forms.DataGridView()
        Me.DIName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DIAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DIValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DICardType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DIByPass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tabDOTable = New System.Windows.Forms.TabPage()
        Me.dgvDOTable = New System.Windows.Forms.DataGridView()
        Me.DOName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DOAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DOValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DOCardType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DOByPass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tabAITable = New System.Windows.Forms.TabPage()
        Me.dgvAITable = New System.Windows.Forms.DataGridView()
        Me.AIName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AIAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AIValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AICardType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AIByPass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tabAOTable = New System.Windows.Forms.TabPage()
        Me.dgvAOTable = New System.Windows.Forms.DataGridView()
        Me.AOName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AOAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AOValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AOCardType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AOByPass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tabDITable.SuspendLayout()
        CType(Me.dgvDITable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDOTable.SuspendLayout()
        CType(Me.dgvDOTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabAITable.SuspendLayout()
        CType(Me.dgvAITable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabAOTable.SuspendLayout()
        CType(Me.dgvAOTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TableLayoutPanel1.Controls.Add(Me.TabControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnExit, 0, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.ToolTip1.SetToolTip(Me.TableLayoutPanel1, resources.GetString("TableLayoutPanel1.ToolTip"))
        '
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.tabDITable)
        Me.TabControl1.Controls.Add(Me.tabDOTable)
        Me.TabControl1.Controls.Add(Me.tabAITable)
        Me.TabControl1.Controls.Add(Me.tabAOTable)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.ToolTip1.SetToolTip(Me.TabControl1, resources.GetString("TabControl1.ToolTip"))
        '
        'tabDITable
        '
        resources.ApplyResources(Me.tabDITable, "tabDITable")
        Me.tabDITable.Controls.Add(Me.dgvDITable)
        Me.tabDITable.Name = "tabDITable"
        Me.ToolTip1.SetToolTip(Me.tabDITable, resources.GetString("tabDITable.ToolTip"))
        Me.tabDITable.UseVisualStyleBackColor = True
        '
        'dgvDITable
        '
        resources.ApplyResources(Me.dgvDITable, "dgvDITable")
        Me.dgvDITable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDITable.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DIName, Me.DIAddress, Me.DIValue, Me.DICardType, Me.DIByPass})
        Me.dgvDITable.Name = "dgvDITable"
        Me.dgvDITable.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.dgvDITable, resources.GetString("dgvDITable.ToolTip"))
        '
        'DIName
        '
        Me.DIName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.DIName, "DIName")
        Me.DIName.Name = "DIName"
        Me.DIName.ReadOnly = True
        '
        'DIAddress
        '
        Me.DIAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.DIAddress, "DIAddress")
        Me.DIAddress.Name = "DIAddress"
        Me.DIAddress.ReadOnly = True
        '
        'DIValue
        '
        Me.DIValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.DIValue, "DIValue")
        Me.DIValue.Name = "DIValue"
        Me.DIValue.ReadOnly = True
        '
        'DICardType
        '
        Me.DICardType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.DICardType, "DICardType")
        Me.DICardType.Name = "DICardType"
        Me.DICardType.ReadOnly = True
        '
        'DIByPass
        '
        Me.DIByPass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.DIByPass, "DIByPass")
        Me.DIByPass.Name = "DIByPass"
        Me.DIByPass.ReadOnly = True
        '
        'tabDOTable
        '
        resources.ApplyResources(Me.tabDOTable, "tabDOTable")
        Me.tabDOTable.Controls.Add(Me.dgvDOTable)
        Me.tabDOTable.Name = "tabDOTable"
        Me.ToolTip1.SetToolTip(Me.tabDOTable, resources.GetString("tabDOTable.ToolTip"))
        Me.tabDOTable.UseVisualStyleBackColor = True
        '
        'dgvDOTable
        '
        resources.ApplyResources(Me.dgvDOTable, "dgvDOTable")
        Me.dgvDOTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDOTable.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DOName, Me.DOAddress, Me.DOValue, Me.DOCardType, Me.DOByPass})
        Me.dgvDOTable.Name = "dgvDOTable"
        Me.dgvDOTable.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.dgvDOTable, resources.GetString("dgvDOTable.ToolTip"))
        '
        'DOName
        '
        Me.DOName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.DOName, "DOName")
        Me.DOName.Name = "DOName"
        Me.DOName.ReadOnly = True
        '
        'DOAddress
        '
        Me.DOAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.DOAddress, "DOAddress")
        Me.DOAddress.Name = "DOAddress"
        Me.DOAddress.ReadOnly = True
        '
        'DOValue
        '
        Me.DOValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.DOValue, "DOValue")
        Me.DOValue.Name = "DOValue"
        Me.DOValue.ReadOnly = True
        '
        'DOCardType
        '
        Me.DOCardType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.DOCardType, "DOCardType")
        Me.DOCardType.Name = "DOCardType"
        Me.DOCardType.ReadOnly = True
        '
        'DOByPass
        '
        Me.DOByPass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.DOByPass, "DOByPass")
        Me.DOByPass.Name = "DOByPass"
        Me.DOByPass.ReadOnly = True
        '
        'tabAITable
        '
        resources.ApplyResources(Me.tabAITable, "tabAITable")
        Me.tabAITable.Controls.Add(Me.dgvAITable)
        Me.tabAITable.Name = "tabAITable"
        Me.ToolTip1.SetToolTip(Me.tabAITable, resources.GetString("tabAITable.ToolTip"))
        Me.tabAITable.UseVisualStyleBackColor = True
        '
        'dgvAITable
        '
        resources.ApplyResources(Me.dgvAITable, "dgvAITable")
        Me.dgvAITable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAITable.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AIName, Me.AIAddress, Me.AIValue, Me.AICardType, Me.AIByPass})
        Me.dgvAITable.Name = "dgvAITable"
        Me.dgvAITable.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.dgvAITable, resources.GetString("dgvAITable.ToolTip"))
        '
        'AIName
        '
        Me.AIName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.AIName, "AIName")
        Me.AIName.Name = "AIName"
        Me.AIName.ReadOnly = True
        '
        'AIAddress
        '
        Me.AIAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.AIAddress, "AIAddress")
        Me.AIAddress.Name = "AIAddress"
        Me.AIAddress.ReadOnly = True
        '
        'AIValue
        '
        Me.AIValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.AIValue, "AIValue")
        Me.AIValue.Name = "AIValue"
        Me.AIValue.ReadOnly = True
        '
        'AICardType
        '
        Me.AICardType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.AICardType, "AICardType")
        Me.AICardType.Name = "AICardType"
        Me.AICardType.ReadOnly = True
        '
        'AIByPass
        '
        Me.AIByPass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.AIByPass, "AIByPass")
        Me.AIByPass.Name = "AIByPass"
        Me.AIByPass.ReadOnly = True
        '
        'tabAOTable
        '
        resources.ApplyResources(Me.tabAOTable, "tabAOTable")
        Me.tabAOTable.Controls.Add(Me.dgvAOTable)
        Me.tabAOTable.Name = "tabAOTable"
        Me.ToolTip1.SetToolTip(Me.tabAOTable, resources.GetString("tabAOTable.ToolTip"))
        Me.tabAOTable.UseVisualStyleBackColor = True
        '
        'dgvAOTable
        '
        resources.ApplyResources(Me.dgvAOTable, "dgvAOTable")
        Me.dgvAOTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAOTable.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AOName, Me.AOAddress, Me.AOValue, Me.AOCardType, Me.AOByPass})
        Me.dgvAOTable.Name = "dgvAOTable"
        Me.dgvAOTable.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.dgvAOTable, resources.GetString("dgvAOTable.ToolTip"))
        '
        'AOName
        '
        Me.AOName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.AOName, "AOName")
        Me.AOName.Name = "AOName"
        Me.AOName.ReadOnly = True
        '
        'AOAddress
        '
        Me.AOAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.AOAddress, "AOAddress")
        Me.AOAddress.Name = "AOAddress"
        Me.AOAddress.ReadOnly = True
        '
        'AOValue
        '
        Me.AOValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.AOValue, "AOValue")
        Me.AOValue.Name = "AOValue"
        Me.AOValue.ReadOnly = True
        '
        'AOCardType
        '
        Me.AOCardType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.AOCardType, "AOCardType")
        Me.AOCardType.Name = "AOCardType"
        Me.AOCardType.ReadOnly = True
        '
        'AOByPass
        '
        Me.AOByPass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        resources.ApplyResources(Me.AOByPass, "AOByPass")
        Me.AOByPass.Name = "AOByPass"
        Me.AOByPass.ReadOnly = True
        '
        'btnExit
        '
        resources.ApplyResources(Me.btnExit, "btnExit")
        Me.btnExit.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnExit.FlatAppearance.BorderSize = 0
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnExit.Name = "btnExit"
        Me.ToolTip1.SetToolTip(Me.btnExit, resources.GetString("btnExit.ToolTip"))
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmIOTable
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIOTable"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.tabDITable.ResumeLayout(False)
        CType(Me.dgvDITable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDOTable.ResumeLayout(False)
        CType(Me.dgvDOTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabAITable.ResumeLayout(False)
        CType(Me.dgvAITable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabAOTable.ResumeLayout(False)
        CType(Me.dgvAOTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabDITable As System.Windows.Forms.TabPage
    Friend WithEvents dgvDITable As System.Windows.Forms.DataGridView
    Friend WithEvents tabDOTable As System.Windows.Forms.TabPage
    Friend WithEvents dgvDOTable As System.Windows.Forms.DataGridView
    Friend WithEvents tabAITable As System.Windows.Forms.TabPage
    Friend WithEvents dgvAITable As System.Windows.Forms.DataGridView
    Friend WithEvents tabAOTable As System.Windows.Forms.TabPage
    Friend WithEvents dgvAOTable As System.Windows.Forms.DataGridView
    Friend WithEvents DIName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DIAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DIValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DICardType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DIByPass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOCardType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOByPass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AIName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AIAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AIValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AICardType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AIByPass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AOName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AOAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AOValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AOCardType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AOByPass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

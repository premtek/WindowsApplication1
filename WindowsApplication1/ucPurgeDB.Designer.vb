<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucPurgeDB
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucPurgeDB))
        Me.grpAuger = New System.Windows.Forms.GroupBox()
        Me.lblRuns = New System.Windows.Forms.Label()
        Me.lblTimer = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboRunType = New System.Windows.Forms.ComboBox()
        Me.chkJettingPurge = New System.Windows.Forms.CheckBox()
        Me.chkPreDispenePurge = New System.Windows.Forms.CheckBox()
        Me.lblCleanType = New System.Windows.Forms.Label()
        Me.cboCleanType = New System.Windows.Forms.ComboBox()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.lstItem = New System.Windows.Forms.ListBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lblValve4 = New System.Windows.Forms.Label()
        Me.lblValve3 = New System.Windows.Forms.Label()
        Me.lblValve2 = New System.Windows.Forms.Label()
        Me.lblValve1 = New System.Windows.Forms.Label()
        Me.cmbValvePurge4 = New System.Windows.Forms.ComboBox()
        Me.cmbValvePurge3 = New System.Windows.Forms.ComboBox()
        Me.cmbValvePurge2 = New System.Windows.Forms.ComboBox()
        Me.cmbValvePurge1 = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.nmuOnTimer = New System.Windows.Forms.NumericUpDown()
        Me.nmuOnRuns = New System.Windows.Forms.NumericUpDown()
        Me.grpAuger.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nmuOnTimer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuOnRuns, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpAuger
        '
        Me.grpAuger.Controls.Add(Me.nmuOnRuns)
        Me.grpAuger.Controls.Add(Me.nmuOnTimer)
        Me.grpAuger.Controls.Add(Me.lblRuns)
        Me.grpAuger.Controls.Add(Me.lblTimer)
        Me.grpAuger.Controls.Add(Me.Label1)
        Me.grpAuger.Controls.Add(Me.cboRunType)
        Me.grpAuger.Controls.Add(Me.chkJettingPurge)
        Me.grpAuger.Controls.Add(Me.chkPreDispenePurge)
        Me.grpAuger.Controls.Add(Me.lblCleanType)
        Me.grpAuger.Controls.Add(Me.cboCleanType)
        Me.grpAuger.Controls.Add(Me.Label82)
        Me.grpAuger.Controls.Add(Me.Label50)
        Me.grpAuger.Controls.Add(Me.lstItem)
        Me.grpAuger.Controls.Add(Me.txtName)
        Me.grpAuger.Controls.Add(Me.btnUpdate)
        Me.grpAuger.Controls.Add(Me.btnDelete)
        Me.grpAuger.Controls.Add(Me.btnAdd)
        resources.ApplyResources(Me.grpAuger, "grpAuger")
        Me.grpAuger.Name = "grpAuger"
        Me.grpAuger.TabStop = False
        '
        'lblRuns
        '
        resources.ApplyResources(Me.lblRuns, "lblRuns")
        Me.lblRuns.Name = "lblRuns"
        '
        'lblTimer
        '
        resources.ApplyResources(Me.lblTimer, "lblTimer")
        Me.lblTimer.Name = "lblTimer"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'cboRunType
        '
        Me.cboRunType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cboRunType, "cboRunType")
        Me.cboRunType.FormattingEnabled = True
        Me.cboRunType.Name = "cboRunType"
        '
        'chkJettingPurge
        '
        resources.ApplyResources(Me.chkJettingPurge, "chkJettingPurge")
        Me.chkJettingPurge.Name = "chkJettingPurge"
        Me.chkJettingPurge.UseVisualStyleBackColor = True
        '
        'chkPreDispenePurge
        '
        resources.ApplyResources(Me.chkPreDispenePurge, "chkPreDispenePurge")
        Me.chkPreDispenePurge.Name = "chkPreDispenePurge"
        Me.chkPreDispenePurge.UseVisualStyleBackColor = True
        '
        'lblCleanType
        '
        resources.ApplyResources(Me.lblCleanType, "lblCleanType")
        Me.lblCleanType.Name = "lblCleanType"
        '
        'cboCleanType
        '
        Me.cboCleanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cboCleanType, "cboCleanType")
        Me.cboCleanType.FormattingEnabled = True
        Me.cboCleanType.Name = "cboCleanType"
        '
        'Label82
        '
        resources.ApplyResources(Me.Label82, "Label82")
        Me.Label82.Name = "Label82"
        '
        'Label50
        '
        resources.ApplyResources(Me.Label50, "Label50")
        Me.Label50.Name = "Label50"
        '
        'lstItem
        '
        resources.ApplyResources(Me.lstItem, "lstItem")
        Me.lstItem.FormattingEnabled = True
        Me.lstItem.Name = "lstItem"
        '
        'txtName
        '
        resources.ApplyResources(Me.txtName, "txtName")
        Me.txtName.Name = "txtName"
        '
        'btnUpdate
        '
        resources.ApplyResources(Me.btnUpdate, "btnUpdate")
        Me.btnUpdate.Name = "btnUpdate"
        Me.ToolTip1.SetToolTip(Me.btnUpdate, resources.GetString("btnUpdate.ToolTip"))
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Delete
        resources.ApplyResources(Me.btnDelete, "btnDelete")
        Me.btnDelete.Name = "btnDelete"
        Me.ToolTip1.SetToolTip(Me.btnDelete, resources.GetString("btnDelete.ToolTip"))
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Add1
        resources.ApplyResources(Me.btnAdd, "btnAdd")
        Me.btnAdd.Name = "btnAdd"
        Me.ToolTip1.SetToolTip(Me.btnAdd, resources.GetString("btnAdd.ToolTip"))
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'lblValve4
        '
        resources.ApplyResources(Me.lblValve4, "lblValve4")
        Me.lblValve4.Name = "lblValve4"
        '
        'lblValve3
        '
        resources.ApplyResources(Me.lblValve3, "lblValve3")
        Me.lblValve3.Name = "lblValve3"
        '
        'lblValve2
        '
        resources.ApplyResources(Me.lblValve2, "lblValve2")
        Me.lblValve2.Name = "lblValve2"
        '
        'lblValve1
        '
        resources.ApplyResources(Me.lblValve1, "lblValve1")
        Me.lblValve1.Name = "lblValve1"
        '
        'cmbValvePurge4
        '
        Me.cmbValvePurge4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cmbValvePurge4, "cmbValvePurge4")
        Me.cmbValvePurge4.FormattingEnabled = True
        Me.cmbValvePurge4.Name = "cmbValvePurge4"
        '
        'cmbValvePurge3
        '
        Me.cmbValvePurge3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cmbValvePurge3, "cmbValvePurge3")
        Me.cmbValvePurge3.FormattingEnabled = True
        Me.cmbValvePurge3.Name = "cmbValvePurge3"
        '
        'cmbValvePurge2
        '
        Me.cmbValvePurge2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cmbValvePurge2, "cmbValvePurge2")
        Me.cmbValvePurge2.FormattingEnabled = True
        Me.cmbValvePurge2.Name = "cmbValvePurge2"
        '
        'cmbValvePurge1
        '
        Me.cmbValvePurge1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cmbValvePurge1, "cmbValvePurge1")
        Me.cmbValvePurge1.FormattingEnabled = True
        Me.cmbValvePurge1.Name = "cmbValvePurge1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbValvePurge1)
        Me.GroupBox1.Controls.Add(Me.lblValve4)
        Me.GroupBox1.Controls.Add(Me.cmbValvePurge2)
        Me.GroupBox1.Controls.Add(Me.lblValve3)
        Me.GroupBox1.Controls.Add(Me.cmbValvePurge3)
        Me.GroupBox1.Controls.Add(Me.lblValve2)
        Me.GroupBox1.Controls.Add(Me.cmbValvePurge4)
        Me.GroupBox1.Controls.Add(Me.lblValve1)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'nmuOnTimer
        '
        Me.nmuOnTimer.DecimalPlaces = 1
        resources.ApplyResources(Me.nmuOnTimer, "nmuOnTimer")
        Me.nmuOnTimer.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmuOnTimer.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmuOnTimer.Name = "nmuOnTimer"
        Me.nmuOnTimer.Value = New Decimal(New Integer() {2, 0, 0, 65536})
        '
        'nmuOnRuns
        '
        Me.nmuOnRuns.DecimalPlaces = 1
        resources.ApplyResources(Me.nmuOnRuns, "nmuOnRuns")
        Me.nmuOnRuns.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmuOnRuns.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmuOnRuns.Name = "nmuOnRuns"
        Me.nmuOnRuns.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'ucPurgeDB
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpAuger)
        Me.Name = "ucPurgeDB"
        resources.ApplyResources(Me, "$this")
        Me.grpAuger.ResumeLayout(False)
        Me.grpAuger.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nmuOnTimer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuOnRuns, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpAuger As System.Windows.Forms.GroupBox
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents lstItem As System.Windows.Forms.ListBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lblCleanType As System.Windows.Forms.Label
    Friend WithEvents cboCleanType As System.Windows.Forms.ComboBox
    Friend WithEvents lblValve4 As System.Windows.Forms.Label
    Friend WithEvents lblValve3 As System.Windows.Forms.Label
    Friend WithEvents lblValve2 As System.Windows.Forms.Label
    Friend WithEvents lblValve1 As System.Windows.Forms.Label
    Friend WithEvents cmbValvePurge4 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbValvePurge3 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbValvePurge2 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbValvePurge1 As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkPreDispenePurge As System.Windows.Forms.CheckBox
    Friend WithEvents chkJettingPurge As System.Windows.Forms.CheckBox
    Friend WithEvents cboRunType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblRuns As System.Windows.Forms.Label
    Friend WithEvents lblTimer As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents nmuOnRuns As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuOnTimer As System.Windows.Forms.NumericUpDown

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEPVConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEPVConfig))
        Me.lblBaudRate = New System.Windows.Forms.Label()
        Me.lblCOMPort = New System.Windows.Forms.Label()
        Me.lblType = New System.Windows.Forms.Label()
        Me.cmbCOMPort = New System.Windows.Forms.ComboBox()
        Me.cmbCardType = New System.Windows.Forms.ComboBox()
        Me.lblSelectIem = New System.Windows.Forms.Label()
        Me.cmbEPV = New System.Windows.Forms.ComboBox()
        Me.nmuValue = New System.Windows.Forms.NumericUpDown()
        Me.lblValue = New System.Windows.Forms.Label()
        Me.cmbBaudRate = New System.Windows.Forms.ComboBox()
        Me.lblSetValue = New System.Windows.Forms.Label()
        Me.lblGetValue = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblGetUnit = New System.Windows.Forms.Label()
        Me.lblSetUnit = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnGet = New System.Windows.Forms.Button()
        Me.btnSet = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.nmuValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblBaudRate
        '
        resources.ApplyResources(Me.lblBaudRate, "lblBaudRate")
        Me.lblBaudRate.Name = "lblBaudRate"
        Me.ToolTip1.SetToolTip(Me.lblBaudRate, resources.GetString("lblBaudRate.ToolTip"))
        '
        'lblCOMPort
        '
        resources.ApplyResources(Me.lblCOMPort, "lblCOMPort")
        Me.lblCOMPort.Name = "lblCOMPort"
        Me.ToolTip1.SetToolTip(Me.lblCOMPort, resources.GetString("lblCOMPort.ToolTip"))
        '
        'lblType
        '
        resources.ApplyResources(Me.lblType, "lblType")
        Me.lblType.Name = "lblType"
        Me.ToolTip1.SetToolTip(Me.lblType, resources.GetString("lblType.ToolTip"))
        '
        'cmbCOMPort
        '
        resources.ApplyResources(Me.cmbCOMPort, "cmbCOMPort")
        Me.cmbCOMPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCOMPort.FormattingEnabled = True
        Me.cmbCOMPort.Name = "cmbCOMPort"
        Me.ToolTip1.SetToolTip(Me.cmbCOMPort, resources.GetString("cmbCOMPort.ToolTip"))
        '
        'cmbCardType
        '
        resources.ApplyResources(Me.cmbCardType, "cmbCardType")
        Me.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCardType.FormattingEnabled = True
        Me.cmbCardType.Name = "cmbCardType"
        Me.ToolTip1.SetToolTip(Me.cmbCardType, resources.GetString("cmbCardType.ToolTip"))
        '
        'lblSelectIem
        '
        resources.ApplyResources(Me.lblSelectIem, "lblSelectIem")
        Me.lblSelectIem.Name = "lblSelectIem"
        Me.ToolTip1.SetToolTip(Me.lblSelectIem, resources.GetString("lblSelectIem.ToolTip"))
        '
        'cmbEPV
        '
        resources.ApplyResources(Me.cmbEPV, "cmbEPV")
        Me.cmbEPV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEPV.FormattingEnabled = True
        Me.cmbEPV.Name = "cmbEPV"
        Me.ToolTip1.SetToolTip(Me.cmbEPV, resources.GetString("cmbEPV.ToolTip"))
        '
        'nmuValue
        '
        resources.ApplyResources(Me.nmuValue, "nmuValue")
        Me.nmuValue.DecimalPlaces = 2
        Me.nmuValue.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmuValue.Maximum = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.nmuValue.Name = "nmuValue"
        Me.ToolTip1.SetToolTip(Me.nmuValue, resources.GetString("nmuValue.ToolTip"))
        '
        'lblValue
        '
        resources.ApplyResources(Me.lblValue, "lblValue")
        Me.lblValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblValue.Name = "lblValue"
        Me.ToolTip1.SetToolTip(Me.lblValue, resources.GetString("lblValue.ToolTip"))
        '
        'cmbBaudRate
        '
        resources.ApplyResources(Me.cmbBaudRate, "cmbBaudRate")
        Me.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBaudRate.FormattingEnabled = True
        Me.cmbBaudRate.Name = "cmbBaudRate"
        Me.ToolTip1.SetToolTip(Me.cmbBaudRate, resources.GetString("cmbBaudRate.ToolTip"))
        '
        'lblSetValue
        '
        resources.ApplyResources(Me.lblSetValue, "lblSetValue")
        Me.lblSetValue.Name = "lblSetValue"
        Me.ToolTip1.SetToolTip(Me.lblSetValue, resources.GetString("lblSetValue.ToolTip"))
        '
        'lblGetValue
        '
        resources.ApplyResources(Me.lblGetValue, "lblGetValue")
        Me.lblGetValue.Name = "lblGetValue"
        Me.ToolTip1.SetToolTip(Me.lblGetValue, resources.GetString("lblGetValue.ToolTip"))
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.lblGetUnit)
        Me.GroupBox1.Controls.Add(Me.lblSetUnit)
        Me.GroupBox1.Controls.Add(Me.lblSelectIem)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.lblValue)
        Me.GroupBox1.Controls.Add(Me.btnOK)
        Me.GroupBox1.Controls.Add(Me.cmbEPV)
        Me.GroupBox1.Controls.Add(Me.nmuValue)
        Me.GroupBox1.Controls.Add(Me.cmbCardType)
        Me.GroupBox1.Controls.Add(Me.btnGet)
        Me.GroupBox1.Controls.Add(Me.cmbCOMPort)
        Me.GroupBox1.Controls.Add(Me.btnSet)
        Me.GroupBox1.Controls.Add(Me.cmbBaudRate)
        Me.GroupBox1.Controls.Add(Me.lblType)
        Me.GroupBox1.Controls.Add(Me.lblCOMPort)
        Me.GroupBox1.Controls.Add(Me.lblGetValue)
        Me.GroupBox1.Controls.Add(Me.lblBaudRate)
        Me.GroupBox1.Controls.Add(Me.lblSetValue)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'lblGetUnit
        '
        resources.ApplyResources(Me.lblGetUnit, "lblGetUnit")
        Me.lblGetUnit.Name = "lblGetUnit"
        Me.ToolTip1.SetToolTip(Me.lblGetUnit, resources.GetString("lblGetUnit.ToolTip"))
        '
        'lblSetUnit
        '
        resources.ApplyResources(Me.lblSetUnit, "lblSetUnit")
        Me.lblSetUnit.Name = "lblSetUnit"
        Me.ToolTip1.SetToolTip(Me.lblSetUnit, resources.GetString("lblSetUnit.ToolTip"))
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
        'btnGet
        '
        resources.ApplyResources(Me.btnGet, "btnGet")
        Me.btnGet.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        Me.btnGet.Name = "btnGet"
        Me.ToolTip1.SetToolTip(Me.btnGet, resources.GetString("btnGet.ToolTip"))
        Me.btnGet.UseVisualStyleBackColor = True
        '
        'btnSet
        '
        resources.ApplyResources(Me.btnSet, "btnSet")
        Me.btnSet.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSet.Name = "btnSet"
        Me.ToolTip1.SetToolTip(Me.btnSet, resources.GetString("btnSet.ToolTip"))
        Me.btnSet.UseVisualStyleBackColor = True
        '
        'frmEPVConfig
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEPVConfig"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        CType(Me.nmuValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblBaudRate As System.Windows.Forms.Label
    Friend WithEvents lblCOMPort As System.Windows.Forms.Label
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents cmbCOMPort As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCardType As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelectIem As System.Windows.Forms.Label
    Friend WithEvents cmbEPV As System.Windows.Forms.ComboBox
    Friend WithEvents btnSet As System.Windows.Forms.Button
    Friend WithEvents nmuValue As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnGet As System.Windows.Forms.Button
    Friend WithEvents lblValue As System.Windows.Forms.Label
    Friend WithEvents cmbBaudRate As System.Windows.Forms.ComboBox
    Friend WithEvents lblSetValue As System.Windows.Forms.Label
    Friend WithEvents lblGetValue As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblGetUnit As System.Windows.Forms.Label
    Friend WithEvents lblSetUnit As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

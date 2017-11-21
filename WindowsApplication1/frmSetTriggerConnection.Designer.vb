<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetTriggerConnection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetTriggerConnection))
        Me.lblBaudRate = New System.Windows.Forms.Label()
        Me.lblCOMPort = New System.Windows.Forms.Label()
        Me.lblType = New System.Windows.Forms.Label()
        Me.cmbBaudRate = New System.Windows.Forms.ComboBox()
        Me.cmbCOMPort = New System.Windows.Forms.ComboBox()
        Me.cmbTriggerType = New System.Windows.Forms.ComboBox()
        Me.lblSelect = New System.Windows.Forms.Label()
        Me.cmbTrigger = New System.Windows.Forms.ComboBox()
        Me.grpTrigger = New System.Windows.Forms.GroupBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpTrigger.SuspendLayout()
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
        'cmbBaudRate
        '
        resources.ApplyResources(Me.cmbBaudRate, "cmbBaudRate")
        Me.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBaudRate.FormattingEnabled = True
        Me.cmbBaudRate.Name = "cmbBaudRate"
        Me.ToolTip1.SetToolTip(Me.cmbBaudRate, resources.GetString("cmbBaudRate.ToolTip"))
        '
        'cmbCOMPort
        '
        resources.ApplyResources(Me.cmbCOMPort, "cmbCOMPort")
        Me.cmbCOMPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCOMPort.FormattingEnabled = True
        Me.cmbCOMPort.Name = "cmbCOMPort"
        Me.ToolTip1.SetToolTip(Me.cmbCOMPort, resources.GetString("cmbCOMPort.ToolTip"))
        '
        'cmbTriggerType
        '
        resources.ApplyResources(Me.cmbTriggerType, "cmbTriggerType")
        Me.cmbTriggerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTriggerType.FormattingEnabled = True
        Me.cmbTriggerType.Name = "cmbTriggerType"
        Me.ToolTip1.SetToolTip(Me.cmbTriggerType, resources.GetString("cmbTriggerType.ToolTip"))
        '
        'lblSelect
        '
        resources.ApplyResources(Me.lblSelect, "lblSelect")
        Me.lblSelect.Name = "lblSelect"
        Me.ToolTip1.SetToolTip(Me.lblSelect, resources.GetString("lblSelect.ToolTip"))
        '
        'cmbTrigger
        '
        resources.ApplyResources(Me.cmbTrigger, "cmbTrigger")
        Me.cmbTrigger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTrigger.FormattingEnabled = True
        Me.cmbTrigger.Name = "cmbTrigger"
        Me.ToolTip1.SetToolTip(Me.cmbTrigger, resources.GetString("cmbTrigger.ToolTip"))
        '
        'grpTrigger
        '
        resources.ApplyResources(Me.grpTrigger, "grpTrigger")
        Me.grpTrigger.Controls.Add(Me.cmbTrigger)
        Me.grpTrigger.Controls.Add(Me.btnCancel)
        Me.grpTrigger.Controls.Add(Me.lblSelect)
        Me.grpTrigger.Controls.Add(Me.btnOK)
        Me.grpTrigger.Controls.Add(Me.cmbTriggerType)
        Me.grpTrigger.Controls.Add(Me.lblBaudRate)
        Me.grpTrigger.Controls.Add(Me.cmbCOMPort)
        Me.grpTrigger.Controls.Add(Me.lblCOMPort)
        Me.grpTrigger.Controls.Add(Me.cmbBaudRate)
        Me.grpTrigger.Controls.Add(Me.lblType)
        Me.grpTrigger.Name = "grpTrigger"
        Me.grpTrigger.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpTrigger, resources.GetString("grpTrigger.ToolTip"))
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
        'frmSetTriggerConnection
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.grpTrigger)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetTriggerConnection"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpTrigger.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblBaudRate As System.Windows.Forms.Label
    Friend WithEvents lblCOMPort As System.Windows.Forms.Label
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents cmbBaudRate As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCOMPort As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTriggerType As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelect As System.Windows.Forms.Label
    Friend WithEvents cmbTrigger As System.Windows.Forms.ComboBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents grpTrigger As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

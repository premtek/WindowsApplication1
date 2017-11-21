<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetFMCSConnection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetFMCSConnection))
        Me.cmbFMCS = New System.Windows.Forms.ComboBox()
        Me.lblSelectCCD = New System.Windows.Forms.Label()
        Me.cmbMeterType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbCOMPort = New System.Windows.Forms.ComboBox()
        Me.cmbBaudRate = New System.Windows.Forms.ComboBox()
        Me.cmbDataBit = New System.Windows.Forms.ComboBox()
        Me.lblCOMPort = New System.Windows.Forms.Label()
        Me.lblBaudRate = New System.Windows.Forms.Label()
        Me.lblDatabit = New System.Windows.Forms.Label()
        Me.grpFMCS = New System.Windows.Forms.GroupBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpFMCS.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbFMCS
        '
        resources.ApplyResources(Me.cmbFMCS, "cmbFMCS")
        Me.cmbFMCS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFMCS.FormattingEnabled = True
        Me.cmbFMCS.Name = "cmbFMCS"
        Me.ToolTip1.SetToolTip(Me.cmbFMCS, resources.GetString("cmbFMCS.ToolTip"))
        '
        'lblSelectCCD
        '
        resources.ApplyResources(Me.lblSelectCCD, "lblSelectCCD")
        Me.lblSelectCCD.Name = "lblSelectCCD"
        Me.ToolTip1.SetToolTip(Me.lblSelectCCD, resources.GetString("lblSelectCCD.ToolTip"))
        '
        'cmbMeterType
        '
        resources.ApplyResources(Me.cmbMeterType, "cmbMeterType")
        Me.cmbMeterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMeterType.FormattingEnabled = True
        Me.cmbMeterType.Name = "cmbMeterType"
        Me.ToolTip1.SetToolTip(Me.cmbMeterType, resources.GetString("cmbMeterType.ToolTip"))
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'cmbCOMPort
        '
        resources.ApplyResources(Me.cmbCOMPort, "cmbCOMPort")
        Me.cmbCOMPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCOMPort.FormattingEnabled = True
        Me.cmbCOMPort.Name = "cmbCOMPort"
        Me.ToolTip1.SetToolTip(Me.cmbCOMPort, resources.GetString("cmbCOMPort.ToolTip"))
        '
        'cmbBaudRate
        '
        resources.ApplyResources(Me.cmbBaudRate, "cmbBaudRate")
        Me.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBaudRate.FormattingEnabled = True
        Me.cmbBaudRate.Name = "cmbBaudRate"
        Me.ToolTip1.SetToolTip(Me.cmbBaudRate, resources.GetString("cmbBaudRate.ToolTip"))
        '
        'cmbDataBit
        '
        resources.ApplyResources(Me.cmbDataBit, "cmbDataBit")
        Me.cmbDataBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataBit.FormattingEnabled = True
        Me.cmbDataBit.Name = "cmbDataBit"
        Me.ToolTip1.SetToolTip(Me.cmbDataBit, resources.GetString("cmbDataBit.ToolTip"))
        '
        'lblCOMPort
        '
        resources.ApplyResources(Me.lblCOMPort, "lblCOMPort")
        Me.lblCOMPort.Name = "lblCOMPort"
        Me.ToolTip1.SetToolTip(Me.lblCOMPort, resources.GetString("lblCOMPort.ToolTip"))
        '
        'lblBaudRate
        '
        resources.ApplyResources(Me.lblBaudRate, "lblBaudRate")
        Me.lblBaudRate.Name = "lblBaudRate"
        Me.ToolTip1.SetToolTip(Me.lblBaudRate, resources.GetString("lblBaudRate.ToolTip"))
        '
        'lblDatabit
        '
        resources.ApplyResources(Me.lblDatabit, "lblDatabit")
        Me.lblDatabit.Name = "lblDatabit"
        Me.ToolTip1.SetToolTip(Me.lblDatabit, resources.GetString("lblDatabit.ToolTip"))
        '
        'grpFMCS
        '
        resources.ApplyResources(Me.grpFMCS, "grpFMCS")
        Me.grpFMCS.Controls.Add(Me.cmbFMCS)
        Me.grpFMCS.Controls.Add(Me.lblSelectCCD)
        Me.grpFMCS.Controls.Add(Me.cmbMeterType)
        Me.grpFMCS.Controls.Add(Me.lblDatabit)
        Me.grpFMCS.Controls.Add(Me.cmbCOMPort)
        Me.grpFMCS.Controls.Add(Me.lblBaudRate)
        Me.grpFMCS.Controls.Add(Me.cmbBaudRate)
        Me.grpFMCS.Controls.Add(Me.lblCOMPort)
        Me.grpFMCS.Controls.Add(Me.cmbDataBit)
        Me.grpFMCS.Controls.Add(Me.Label1)
        Me.grpFMCS.Name = "grpFMCS"
        Me.grpFMCS.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpFMCS, resources.GetString("grpFMCS.ToolTip"))
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
        'frmSetFMCSConnection
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.grpFMCS)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetFMCSConnection"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpFMCS.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbFMCS As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelectCCD As System.Windows.Forms.Label
    Friend WithEvents cmbMeterType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbCOMPort As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBaudRate As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDataBit As System.Windows.Forms.ComboBox
    Friend WithEvents lblCOMPort As System.Windows.Forms.Label
    Friend WithEvents lblBaudRate As System.Windows.Forms.Label
    Friend WithEvents lblDatabit As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents grpFMCS As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

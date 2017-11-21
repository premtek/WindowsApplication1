<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetLightConnection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetLightConnection))
        Me.cmbCCD = New System.Windows.Forms.ComboBox()
        Me.lblSelectCCD = New System.Windows.Forms.Label()
        Me.cmbLightType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbCOMPort = New System.Windows.Forms.ComboBox()
        Me.cmbBaudRate = New System.Windows.Forms.ComboBox()
        Me.cmbDataBit = New System.Windows.Forms.ComboBox()
        Me.lblCOMPort = New System.Windows.Forms.Label()
        Me.lblBaudRate = New System.Windows.Forms.Label()
        Me.lblDatabit = New System.Windows.Forms.Label()
        Me.LblChMaxValue1 = New System.Windows.Forms.Label()
        Me.LblChMaxValue2 = New System.Windows.Forms.Label()
        Me.LblChMaxValue3 = New System.Windows.Forms.Label()
        Me.LblChMaxValue4 = New System.Windows.Forms.Label()
        Me.grpMaxValue = New System.Windows.Forms.GroupBox()
        Me.nmcCh2MaxNum = New System.Windows.Forms.NumericUpDown()
        Me.nmcCh1MaxNum = New System.Windows.Forms.NumericUpDown()
        Me.nmcCh4MaxNum = New System.Windows.Forms.NumericUpDown()
        Me.nmcCh3MaxNum = New System.Windows.Forms.NumericUpDown()
        Me.grpLightController = New System.Windows.Forms.GroupBox()
        Me.btnLightControl = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpMaxValue.SuspendLayout()
        CType(Me.nmcCh2MaxNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcCh1MaxNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcCh4MaxNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcCh3MaxNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLightController.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbCCD
        '
        resources.ApplyResources(Me.cmbCCD, "cmbCCD")
        Me.cmbCCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCCD.FormattingEnabled = True
        Me.cmbCCD.Name = "cmbCCD"
        Me.ToolTip1.SetToolTip(Me.cmbCCD, resources.GetString("cmbCCD.ToolTip"))
        '
        'lblSelectCCD
        '
        resources.ApplyResources(Me.lblSelectCCD, "lblSelectCCD")
        Me.lblSelectCCD.Name = "lblSelectCCD"
        Me.ToolTip1.SetToolTip(Me.lblSelectCCD, resources.GetString("lblSelectCCD.ToolTip"))
        '
        'cmbLightType
        '
        resources.ApplyResources(Me.cmbLightType, "cmbLightType")
        Me.cmbLightType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLightType.FormattingEnabled = True
        Me.cmbLightType.Name = "cmbLightType"
        Me.ToolTip1.SetToolTip(Me.cmbLightType, resources.GetString("cmbLightType.ToolTip"))
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
        'LblChMaxValue1
        '
        resources.ApplyResources(Me.LblChMaxValue1, "LblChMaxValue1")
        Me.LblChMaxValue1.Name = "LblChMaxValue1"
        Me.ToolTip1.SetToolTip(Me.LblChMaxValue1, resources.GetString("LblChMaxValue1.ToolTip"))
        '
        'LblChMaxValue2
        '
        resources.ApplyResources(Me.LblChMaxValue2, "LblChMaxValue2")
        Me.LblChMaxValue2.Name = "LblChMaxValue2"
        Me.ToolTip1.SetToolTip(Me.LblChMaxValue2, resources.GetString("LblChMaxValue2.ToolTip"))
        '
        'LblChMaxValue3
        '
        resources.ApplyResources(Me.LblChMaxValue3, "LblChMaxValue3")
        Me.LblChMaxValue3.Name = "LblChMaxValue3"
        Me.ToolTip1.SetToolTip(Me.LblChMaxValue3, resources.GetString("LblChMaxValue3.ToolTip"))
        '
        'LblChMaxValue4
        '
        resources.ApplyResources(Me.LblChMaxValue4, "LblChMaxValue4")
        Me.LblChMaxValue4.Name = "LblChMaxValue4"
        Me.ToolTip1.SetToolTip(Me.LblChMaxValue4, resources.GetString("LblChMaxValue4.ToolTip"))
        '
        'grpMaxValue
        '
        resources.ApplyResources(Me.grpMaxValue, "grpMaxValue")
        Me.grpMaxValue.Controls.Add(Me.LblChMaxValue1)
        Me.grpMaxValue.Controls.Add(Me.nmcCh2MaxNum)
        Me.grpMaxValue.Controls.Add(Me.nmcCh1MaxNum)
        Me.grpMaxValue.Controls.Add(Me.LblChMaxValue2)
        Me.grpMaxValue.Controls.Add(Me.nmcCh4MaxNum)
        Me.grpMaxValue.Controls.Add(Me.nmcCh3MaxNum)
        Me.grpMaxValue.Controls.Add(Me.LblChMaxValue3)
        Me.grpMaxValue.Controls.Add(Me.LblChMaxValue4)
        Me.grpMaxValue.Name = "grpMaxValue"
        Me.grpMaxValue.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpMaxValue, resources.GetString("grpMaxValue.ToolTip"))
        '
        'nmcCh2MaxNum
        '
        resources.ApplyResources(Me.nmcCh2MaxNum, "nmcCh2MaxNum")
        Me.nmcCh2MaxNum.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmcCh2MaxNum.Name = "nmcCh2MaxNum"
        Me.ToolTip1.SetToolTip(Me.nmcCh2MaxNum, resources.GetString("nmcCh2MaxNum.ToolTip"))
        '
        'nmcCh1MaxNum
        '
        resources.ApplyResources(Me.nmcCh1MaxNum, "nmcCh1MaxNum")
        Me.nmcCh1MaxNum.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmcCh1MaxNum.Name = "nmcCh1MaxNum"
        Me.ToolTip1.SetToolTip(Me.nmcCh1MaxNum, resources.GetString("nmcCh1MaxNum.ToolTip"))
        '
        'nmcCh4MaxNum
        '
        resources.ApplyResources(Me.nmcCh4MaxNum, "nmcCh4MaxNum")
        Me.nmcCh4MaxNum.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmcCh4MaxNum.Name = "nmcCh4MaxNum"
        Me.ToolTip1.SetToolTip(Me.nmcCh4MaxNum, resources.GetString("nmcCh4MaxNum.ToolTip"))
        '
        'nmcCh3MaxNum
        '
        resources.ApplyResources(Me.nmcCh3MaxNum, "nmcCh3MaxNum")
        Me.nmcCh3MaxNum.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nmcCh3MaxNum.Name = "nmcCh3MaxNum"
        Me.ToolTip1.SetToolTip(Me.nmcCh3MaxNum, resources.GetString("nmcCh3MaxNum.ToolTip"))
        '
        'grpLightController
        '
        resources.ApplyResources(Me.grpLightController, "grpLightController")
        Me.grpLightController.Controls.Add(Me.lblSelectCCD)
        Me.grpLightController.Controls.Add(Me.cmbCCD)
        Me.grpLightController.Controls.Add(Me.btnLightControl)
        Me.grpLightController.Controls.Add(Me.cmbLightType)
        Me.grpLightController.Controls.Add(Me.cmbCOMPort)
        Me.grpLightController.Controls.Add(Me.cmbBaudRate)
        Me.grpLightController.Controls.Add(Me.lblDatabit)
        Me.grpLightController.Controls.Add(Me.cmbDataBit)
        Me.grpLightController.Controls.Add(Me.lblBaudRate)
        Me.grpLightController.Controls.Add(Me.Label1)
        Me.grpLightController.Controls.Add(Me.lblCOMPort)
        Me.grpLightController.Name = "grpLightController"
        Me.grpLightController.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpLightController, resources.GetString("grpLightController.ToolTip"))
        '
        'btnLightControl
        '
        resources.ApplyResources(Me.btnLightControl, "btnLightControl")
        Me.btnLightControl.FlatAppearance.BorderSize = 0
        Me.btnLightControl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnLightControl.Image = Global.WindowsApplication1.My.Resources.Resources.setup1
        Me.btnLightControl.Name = "btnLightControl"
        Me.ToolTip1.SetToolTip(Me.btnLightControl, resources.GetString("btnLightControl.ToolTip"))
        Me.btnLightControl.UseVisualStyleBackColor = True
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
        'frmSetLightConnection
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.grpLightController)
        Me.Controls.Add(Me.grpMaxValue)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetLightConnection"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpMaxValue.ResumeLayout(False)
        CType(Me.nmcCh2MaxNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcCh1MaxNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcCh4MaxNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcCh3MaxNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLightController.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbCCD As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelectCCD As System.Windows.Forms.Label
    Friend WithEvents cmbLightType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbCOMPort As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBaudRate As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDataBit As System.Windows.Forms.ComboBox
    Friend WithEvents lblCOMPort As System.Windows.Forms.Label
    Friend WithEvents lblBaudRate As System.Windows.Forms.Label
    Friend WithEvents lblDatabit As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents LblChMaxValue1 As System.Windows.Forms.Label
    Friend WithEvents LblChMaxValue2 As System.Windows.Forms.Label
    Friend WithEvents LblChMaxValue3 As System.Windows.Forms.Label
    Friend WithEvents LblChMaxValue4 As System.Windows.Forms.Label
    Friend WithEvents btnLightControl As System.Windows.Forms.Button
    Friend WithEvents grpMaxValue As System.Windows.Forms.GroupBox
    Friend WithEvents grpLightController As System.Windows.Forms.GroupBox
    Friend WithEvents nmcCh2MaxNum As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcCh1MaxNum As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcCh4MaxNum As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcCh3MaxNum As System.Windows.Forms.NumericUpDown
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

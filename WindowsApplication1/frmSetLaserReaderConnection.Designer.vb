<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetLaserReaderConnection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetLaserReaderConnection))
        Me.lblSelectLaserReader = New System.Windows.Forms.Label()
        Me.cmbLaserReader = New System.Windows.Forms.ComboBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.cmbLaserReaderType = New System.Windows.Forms.ComboBox()
        Me.lblIPAddress = New System.Windows.Forms.Label()
        Me.lblPort = New System.Windows.Forms.Label()
        Me.txtIPAddress = New System.Windows.Forms.TextBox()
        Me.lblPortName = New System.Windows.Forms.Label()
        Me.lblBaudRate = New System.Windows.Forms.Label()
        Me.tbpItem = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbBaudRate = New System.Windows.Forms.ComboBox()
        Me.btnGet = New System.Windows.Forms.Button()
        Me.cmbCOMPort = New System.Windows.Forms.ComboBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.lblValue = New System.Windows.Forms.Label()
        Me.grpLaser = New System.Windows.Forms.GroupBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.tbpItem.SuspendLayout()
        Me.grpLaser.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSelectLaserReader
        '
        resources.ApplyResources(Me.lblSelectLaserReader, "lblSelectLaserReader")
        Me.lblSelectLaserReader.Name = "lblSelectLaserReader"
        '
        'cmbLaserReader
        '
        resources.ApplyResources(Me.cmbLaserReader, "cmbLaserReader")
        Me.cmbLaserReader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLaserReader.FormattingEnabled = True
        Me.cmbLaserReader.Name = "cmbLaserReader"
        '
        'lblType
        '
        resources.ApplyResources(Me.lblType, "lblType")
        Me.lblType.Name = "lblType"
        '
        'cmbLaserReaderType
        '
        resources.ApplyResources(Me.cmbLaserReaderType, "cmbLaserReaderType")
        Me.cmbLaserReaderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLaserReaderType.FormattingEnabled = True
        Me.cmbLaserReaderType.Name = "cmbLaserReaderType"
        '
        'lblIPAddress
        '
        resources.ApplyResources(Me.lblIPAddress, "lblIPAddress")
        Me.lblIPAddress.Name = "lblIPAddress"
        '
        'lblPort
        '
        resources.ApplyResources(Me.lblPort, "lblPort")
        Me.lblPort.Name = "lblPort"
        '
        'txtIPAddress
        '
        resources.ApplyResources(Me.txtIPAddress, "txtIPAddress")
        Me.txtIPAddress.Name = "txtIPAddress"
        '
        'lblPortName
        '
        resources.ApplyResources(Me.lblPortName, "lblPortName")
        Me.lblPortName.Name = "lblPortName"
        '
        'lblBaudRate
        '
        resources.ApplyResources(Me.lblBaudRate, "lblBaudRate")
        Me.lblBaudRate.Name = "lblBaudRate"
        '
        'tbpItem
        '
        resources.ApplyResources(Me.tbpItem, "tbpItem")
        Me.tbpItem.Controls.Add(Me.cmbBaudRate, 1, 5)
        Me.tbpItem.Controls.Add(Me.btnGet, 0, 6)
        Me.tbpItem.Controls.Add(Me.cmbCOMPort, 1, 4)
        Me.tbpItem.Controls.Add(Me.lblType, 0, 1)
        Me.tbpItem.Controls.Add(Me.lblBaudRate, 0, 5)
        Me.tbpItem.Controls.Add(Me.cmbLaserReader, 1, 0)
        Me.tbpItem.Controls.Add(Me.lblSelectLaserReader, 0, 0)
        Me.tbpItem.Controls.Add(Me.cmbLaserReaderType, 1, 1)
        Me.tbpItem.Controls.Add(Me.lblPortName, 0, 4)
        Me.tbpItem.Controls.Add(Me.lblIPAddress, 0, 2)
        Me.tbpItem.Controls.Add(Me.txtPort, 1, 3)
        Me.tbpItem.Controls.Add(Me.lblPort, 0, 3)
        Me.tbpItem.Controls.Add(Me.txtIPAddress, 1, 2)
        Me.tbpItem.Controls.Add(Me.lblValue, 1, 6)
        Me.tbpItem.Name = "tbpItem"
        '
        'cmbBaudRate
        '
        resources.ApplyResources(Me.cmbBaudRate, "cmbBaudRate")
        Me.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBaudRate.FormattingEnabled = True
        Me.cmbBaudRate.Name = "cmbBaudRate"
        '
        'btnGet
        '
        Me.btnGet.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        resources.ApplyResources(Me.btnGet, "btnGet")
        Me.btnGet.Name = "btnGet"
        Me.ToolTip1.SetToolTip(Me.btnGet, resources.GetString("btnGet.ToolTip"))
        Me.btnGet.UseVisualStyleBackColor = True
        '
        'cmbCOMPort
        '
        resources.ApplyResources(Me.cmbCOMPort, "cmbCOMPort")
        Me.cmbCOMPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCOMPort.FormattingEnabled = True
        Me.cmbCOMPort.Name = "cmbCOMPort"
        '
        'txtPort
        '
        resources.ApplyResources(Me.txtPort, "txtPort")
        Me.txtPort.Name = "txtPort"
        '
        'lblValue
        '
        resources.ApplyResources(Me.lblValue, "lblValue")
        Me.lblValue.Name = "lblValue"
        '
        'grpLaser
        '
        Me.grpLaser.Controls.Add(Me.tbpItem)
        Me.grpLaser.Controls.Add(Me.btnOK)
        Me.grpLaser.Controls.Add(Me.btnCancel)
        resources.ApplyResources(Me.grpLaser, "grpLaser")
        Me.grpLaser.Name = "grpLaser"
        Me.grpLaser.TabStop = False
        '
        'btnOK
        '
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnOK.Name = "btnOK"
        Me.ToolTip1.SetToolTip(Me.btnOK, resources.GetString("btnOK.ToolTip"))
        Me.btnOK.UseVisualStyleBackColor = True
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
        'frmSetLaserReaderConnection
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.grpLaser)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetLaserReaderConnection"
        Me.tbpItem.ResumeLayout(False)
        Me.tbpItem.PerformLayout()
        Me.grpLaser.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblSelectLaserReader As System.Windows.Forms.Label
    Friend WithEvents cmbLaserReader As System.Windows.Forms.ComboBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents cmbLaserReaderType As System.Windows.Forms.ComboBox
    Friend WithEvents lblIPAddress As System.Windows.Forms.Label
    Friend WithEvents lblPort As System.Windows.Forms.Label
    Friend WithEvents txtIPAddress As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblPortName As System.Windows.Forms.Label
    Friend WithEvents lblBaudRate As System.Windows.Forms.Label
    Friend WithEvents tbpItem As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnGet As System.Windows.Forms.Button
    Friend WithEvents lblValue As System.Windows.Forms.Label
    Friend WithEvents cmbBaudRate As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCOMPort As System.Windows.Forms.ComboBox
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents grpLaser As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

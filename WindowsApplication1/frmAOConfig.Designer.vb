<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAOConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAOConfig))
        Me.lblCardType = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.cmbCardType = New System.Windows.Forms.ComboBox()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.grpAOConfig = New System.Windows.Forms.GroupBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.chkByPass = New System.Windows.Forms.CheckBox()
        Me.lblVUnit2 = New System.Windows.Forms.Label()
        Me.lblVUnit = New System.Windows.Forms.Label()
        Me.lblUnit2 = New System.Windows.Forms.Label()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.txtUmin = New System.Windows.Forms.TextBox()
        Me.txtUmax = New System.Windows.Forms.TextBox()
        Me.txtVmin = New System.Windows.Forms.TextBox()
        Me.txtVmax = New System.Windows.Forms.TextBox()
        Me.lblUMin = New System.Windows.Forms.Label()
        Me.lblUMax = New System.Windows.Forms.Label()
        Me.lblVMin = New System.Windows.Forms.Label()
        Me.lblVMax = New System.Windows.Forms.Label()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpAOConfig.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblCardType
        '
        resources.ApplyResources(Me.lblCardType, "lblCardType")
        Me.lblCardType.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCardType.Name = "lblCardType"
        Me.ToolTip1.SetToolTip(Me.lblCardType, resources.GetString("lblCardType.ToolTip"))
        '
        'lblAddress
        '
        resources.ApplyResources(Me.lblAddress, "lblAddress")
        Me.lblAddress.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblAddress.Name = "lblAddress"
        Me.ToolTip1.SetToolTip(Me.lblAddress, resources.GetString("lblAddress.ToolTip"))
        '
        'txtAddress
        '
        resources.ApplyResources(Me.txtAddress, "txtAddress")
        Me.txtAddress.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtAddress.Name = "txtAddress"
        Me.ToolTip1.SetToolTip(Me.txtAddress, resources.GetString("txtAddress.ToolTip"))
        '
        'cmbCardType
        '
        resources.ApplyResources(Me.cmbCardType, "cmbCardType")
        Me.cmbCardType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbCardType.FormattingEnabled = True
        Me.cmbCardType.Name = "cmbCardType"
        Me.ToolTip1.SetToolTip(Me.cmbCardType, resources.GetString("cmbCardType.ToolTip"))
        '
        'lblItem
        '
        resources.ApplyResources(Me.lblItem, "lblItem")
        Me.lblItem.BackColor = System.Drawing.Color.Transparent
        Me.lblItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblItem.Name = "lblItem"
        Me.ToolTip1.SetToolTip(Me.lblItem, resources.GetString("lblItem.ToolTip"))
        '
        'txtName
        '
        resources.ApplyResources(Me.txtName, "txtName")
        Me.txtName.Name = "txtName"
        Me.ToolTip1.SetToolTip(Me.txtName, resources.GetString("txtName.ToolTip"))
        '
        'grpAOConfig
        '
        resources.ApplyResources(Me.grpAOConfig, "grpAOConfig")
        Me.grpAOConfig.Controls.Add(Me.btnCancel)
        Me.grpAOConfig.Controls.Add(Me.btnOK)
        Me.grpAOConfig.Controls.Add(Me.chkByPass)
        Me.grpAOConfig.Controls.Add(Me.lblVUnit2)
        Me.grpAOConfig.Controls.Add(Me.lblVUnit)
        Me.grpAOConfig.Controls.Add(Me.lblUnit2)
        Me.grpAOConfig.Controls.Add(Me.lblUnit)
        Me.grpAOConfig.Controls.Add(Me.txtUmin)
        Me.grpAOConfig.Controls.Add(Me.txtUmax)
        Me.grpAOConfig.Controls.Add(Me.txtVmin)
        Me.grpAOConfig.Controls.Add(Me.txtVmax)
        Me.grpAOConfig.Controls.Add(Me.lblUMin)
        Me.grpAOConfig.Controls.Add(Me.lblUMax)
        Me.grpAOConfig.Controls.Add(Me.lblVMin)
        Me.grpAOConfig.Controls.Add(Me.lblVMax)
        Me.grpAOConfig.Controls.Add(Me.txtName)
        Me.grpAOConfig.Controls.Add(Me.lblItem)
        Me.grpAOConfig.Controls.Add(Me.cmbCardType)
        Me.grpAOConfig.Controls.Add(Me.txtAddress)
        Me.grpAOConfig.Controls.Add(Me.lblAddress)
        Me.grpAOConfig.Controls.Add(Me.lblCardType)
        Me.grpAOConfig.Name = "grpAOConfig"
        Me.grpAOConfig.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpAOConfig, resources.GetString("grpAOConfig.ToolTip"))
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
        'chkByPass
        '
        resources.ApplyResources(Me.chkByPass, "chkByPass")
        Me.chkByPass.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.chkByPass.Name = "chkByPass"
        Me.ToolTip1.SetToolTip(Me.chkByPass, resources.GetString("chkByPass.ToolTip"))
        Me.chkByPass.UseVisualStyleBackColor = True
        '
        'lblVUnit2
        '
        resources.ApplyResources(Me.lblVUnit2, "lblVUnit2")
        Me.lblVUnit2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblVUnit2.Name = "lblVUnit2"
        Me.ToolTip1.SetToolTip(Me.lblVUnit2, resources.GetString("lblVUnit2.ToolTip"))
        '
        'lblVUnit
        '
        resources.ApplyResources(Me.lblVUnit, "lblVUnit")
        Me.lblVUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblVUnit.Name = "lblVUnit"
        Me.ToolTip1.SetToolTip(Me.lblVUnit, resources.GetString("lblVUnit.ToolTip"))
        '
        'lblUnit2
        '
        resources.ApplyResources(Me.lblUnit2, "lblUnit2")
        Me.lblUnit2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUnit2.Name = "lblUnit2"
        Me.ToolTip1.SetToolTip(Me.lblUnit2, resources.GetString("lblUnit2.ToolTip"))
        '
        'lblUnit
        '
        resources.ApplyResources(Me.lblUnit, "lblUnit")
        Me.lblUnit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUnit.Name = "lblUnit"
        Me.ToolTip1.SetToolTip(Me.lblUnit, resources.GetString("lblUnit.ToolTip"))
        '
        'txtUmin
        '
        resources.ApplyResources(Me.txtUmin, "txtUmin")
        Me.txtUmin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtUmin.Name = "txtUmin"
        Me.ToolTip1.SetToolTip(Me.txtUmin, resources.GetString("txtUmin.ToolTip"))
        '
        'txtUmax
        '
        resources.ApplyResources(Me.txtUmax, "txtUmax")
        Me.txtUmax.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtUmax.Name = "txtUmax"
        Me.ToolTip1.SetToolTip(Me.txtUmax, resources.GetString("txtUmax.ToolTip"))
        '
        'txtVmin
        '
        resources.ApplyResources(Me.txtVmin, "txtVmin")
        Me.txtVmin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtVmin.Name = "txtVmin"
        Me.ToolTip1.SetToolTip(Me.txtVmin, resources.GetString("txtVmin.ToolTip"))
        '
        'txtVmax
        '
        resources.ApplyResources(Me.txtVmax, "txtVmax")
        Me.txtVmax.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtVmax.Name = "txtVmax"
        Me.ToolTip1.SetToolTip(Me.txtVmax, resources.GetString("txtVmax.ToolTip"))
        '
        'lblUMin
        '
        resources.ApplyResources(Me.lblUMin, "lblUMin")
        Me.lblUMin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUMin.Name = "lblUMin"
        Me.ToolTip1.SetToolTip(Me.lblUMin, resources.GetString("lblUMin.ToolTip"))
        '
        'lblUMax
        '
        resources.ApplyResources(Me.lblUMax, "lblUMax")
        Me.lblUMax.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUMax.Name = "lblUMax"
        Me.ToolTip1.SetToolTip(Me.lblUMax, resources.GetString("lblUMax.ToolTip"))
        '
        'lblVMin
        '
        resources.ApplyResources(Me.lblVMin, "lblVMin")
        Me.lblVMin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblVMin.Name = "lblVMin"
        Me.ToolTip1.SetToolTip(Me.lblVMin, resources.GetString("lblVMin.ToolTip"))
        '
        'lblVMax
        '
        resources.ApplyResources(Me.lblVMax, "lblVMax")
        Me.lblVMax.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblVMax.Name = "lblVMax"
        Me.ToolTip1.SetToolTip(Me.lblVMax, resources.GetString("lblVMax.ToolTip"))
        '
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        Me.ToolTip1.SetToolTip(Me.UcStatusBar1, resources.GetString("UcStatusBar1.ToolTip"))
        '
        'frmAOConfig
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.grpAOConfig)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAOConfig"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpAOConfig.ResumeLayout(False)
        Me.grpAOConfig.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCardType As System.Windows.Forms.Label
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents cmbCardType As System.Windows.Forms.ComboBox
    Friend WithEvents lblItem As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents grpAOConfig As System.Windows.Forms.GroupBox
    Friend WithEvents lblVUnit2 As System.Windows.Forms.Label
    Friend WithEvents lblVUnit As System.Windows.Forms.Label
    Friend WithEvents lblUnit2 As System.Windows.Forms.Label
    Friend WithEvents lblUnit As System.Windows.Forms.Label
    Friend WithEvents txtUmin As System.Windows.Forms.TextBox
    Friend WithEvents txtUmax As System.Windows.Forms.TextBox
    Friend WithEvents txtVmin As System.Windows.Forms.TextBox
    Friend WithEvents txtVmax As System.Windows.Forms.TextBox
    Friend WithEvents lblUMin As System.Windows.Forms.Label
    Friend WithEvents lblUMax As System.Windows.Forms.Label
    Friend WithEvents lblVMin As System.Windows.Forms.Label
    Friend WithEvents lblVMax As System.Windows.Forms.Label
    Friend WithEvents chkByPass As System.Windows.Forms.CheckBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

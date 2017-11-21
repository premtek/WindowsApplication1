<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDOConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDOConfig))
        Me.lblName = New System.Windows.Forms.Label()
        Me.cmbCardType = New System.Windows.Forms.ComboBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.chkByPass = New System.Windows.Forms.CheckBox()
        Me.chkToggle = New System.Windows.Forms.CheckBox()
        Me.lblCardType = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.grpDOConfig = New System.Windows.Forms.GroupBox()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpDOConfig.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblName
        '
        resources.ApplyResources(Me.lblName, "lblName")
        Me.lblName.BackColor = System.Drawing.Color.White
        Me.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblName.Name = "lblName"
        Me.ToolTip1.SetToolTip(Me.lblName, resources.GetString("lblName.ToolTip"))
        '
        'cmbCardType
        '
        resources.ApplyResources(Me.cmbCardType, "cmbCardType")
        Me.cmbCardType.FormattingEnabled = True
        Me.cmbCardType.Name = "cmbCardType"
        Me.ToolTip1.SetToolTip(Me.cmbCardType, resources.GetString("cmbCardType.ToolTip"))
        '
        'txtAddress
        '
        resources.ApplyResources(Me.txtAddress, "txtAddress")
        Me.txtAddress.Name = "txtAddress"
        Me.ToolTip1.SetToolTip(Me.txtAddress, resources.GetString("txtAddress.ToolTip"))
        '
        'chkByPass
        '
        resources.ApplyResources(Me.chkByPass, "chkByPass")
        Me.chkByPass.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.chkByPass.Name = "chkByPass"
        Me.ToolTip1.SetToolTip(Me.chkByPass, resources.GetString("chkByPass.ToolTip"))
        Me.chkByPass.UseVisualStyleBackColor = True
        '
        'chkToggle
        '
        resources.ApplyResources(Me.chkToggle, "chkToggle")
        Me.chkToggle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.chkToggle.Name = "chkToggle"
        Me.ToolTip1.SetToolTip(Me.chkToggle, resources.GetString("chkToggle.ToolTip"))
        Me.chkToggle.UseVisualStyleBackColor = True
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
        'lblItem
        '
        resources.ApplyResources(Me.lblItem, "lblItem")
        Me.lblItem.BackColor = System.Drawing.Color.Transparent
        Me.lblItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblItem.Name = "lblItem"
        Me.ToolTip1.SetToolTip(Me.lblItem, resources.GetString("lblItem.ToolTip"))
        '
        'grpDOConfig
        '
        resources.ApplyResources(Me.grpDOConfig, "grpDOConfig")
        Me.grpDOConfig.Controls.Add(Me.UcStatusBar1)
        Me.grpDOConfig.Controls.Add(Me.btnCancel)
        Me.grpDOConfig.Controls.Add(Me.btnOK)
        Me.grpDOConfig.Controls.Add(Me.lblName)
        Me.grpDOConfig.Controls.Add(Me.lblItem)
        Me.grpDOConfig.Controls.Add(Me.cmbCardType)
        Me.grpDOConfig.Controls.Add(Me.txtAddress)
        Me.grpDOConfig.Controls.Add(Me.chkByPass)
        Me.grpDOConfig.Controls.Add(Me.lblAddress)
        Me.grpDOConfig.Controls.Add(Me.chkToggle)
        Me.grpDOConfig.Controls.Add(Me.lblCardType)
        Me.grpDOConfig.Name = "grpDOConfig"
        Me.grpDOConfig.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpDOConfig, resources.GetString("grpDOConfig.ToolTip"))
        '
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        Me.ToolTip1.SetToolTip(Me.UcStatusBar1, resources.GetString("UcStatusBar1.ToolTip"))
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
        'frmDOConfig
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.grpDOConfig)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDOConfig"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.TopMost = True
        Me.grpDOConfig.ResumeLayout(False)
        Me.grpDOConfig.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents cmbCardType As System.Windows.Forms.ComboBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents chkByPass As System.Windows.Forms.CheckBox
    Friend WithEvents chkToggle As System.Windows.Forms.CheckBox
    Friend WithEvents lblCardType As System.Windows.Forms.Label
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents lblItem As System.Windows.Forms.Label
    Friend WithEvents grpDOConfig As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

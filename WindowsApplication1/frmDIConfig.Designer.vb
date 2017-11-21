<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDIConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDIConfig))
        Me.cmbCardType = New System.Windows.Forms.ComboBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.chkByPass = New System.Windows.Forms.CheckBox()
        Me.chkToggle = New System.Windows.Forms.CheckBox()
        Me.lblCardType = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grpDICOnfig = New System.Windows.Forms.GroupBox()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lblName = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpDICOnfig.SuspendLayout()
        Me.SuspendLayout()
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
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.Name = "Label4"
        Me.ToolTip1.SetToolTip(Me.Label4, resources.GetString("Label4.ToolTip"))
        '
        'grpDICOnfig
        '
        resources.ApplyResources(Me.grpDICOnfig, "grpDICOnfig")
        Me.grpDICOnfig.Controls.Add(Me.UcStatusBar1)
        Me.grpDICOnfig.Controls.Add(Me.btnCancel)
        Me.grpDICOnfig.Controls.Add(Me.btnOK)
        Me.grpDICOnfig.Controls.Add(Me.lblName)
        Me.grpDICOnfig.Controls.Add(Me.Label4)
        Me.grpDICOnfig.Controls.Add(Me.cmbCardType)
        Me.grpDICOnfig.Controls.Add(Me.txtAddress)
        Me.grpDICOnfig.Controls.Add(Me.chkByPass)
        Me.grpDICOnfig.Controls.Add(Me.lblAddress)
        Me.grpDICOnfig.Controls.Add(Me.chkToggle)
        Me.grpDICOnfig.Controls.Add(Me.lblCardType)
        Me.grpDICOnfig.Name = "grpDICOnfig"
        Me.grpDICOnfig.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpDICOnfig, resources.GetString("grpDICOnfig.ToolTip"))
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
        'lblName
        '
        resources.ApplyResources(Me.lblName, "lblName")
        Me.lblName.BackColor = System.Drawing.Color.White
        Me.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblName.Name = "lblName"
        Me.ToolTip1.SetToolTip(Me.lblName, resources.GetString("lblName.ToolTip"))
        '
        'frmDIConfig
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.grpDICOnfig)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDIConfig"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.TopMost = True
        Me.grpDICOnfig.ResumeLayout(False)
        Me.grpDICOnfig.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbCardType As System.Windows.Forms.ComboBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents chkByPass As System.Windows.Forms.CheckBox
    Friend WithEvents chkToggle As System.Windows.Forms.CheckBox
    Friend WithEvents lblCardType As System.Windows.Forms.Label
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grpDICOnfig As System.Windows.Forms.GroupBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

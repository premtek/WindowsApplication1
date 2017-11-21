<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExchange
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExchange))
        Me.chkPasteExpiredCount = New System.Windows.Forms.CheckBox()
        Me.chkPasteExpireTime = New System.Windows.Forms.CheckBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.txtPasteExpiredCount = New System.Windows.Forms.TextBox()
        Me.txtPasteExpiredTime = New System.Windows.Forms.TextBox()
        Me.Label119 = New System.Windows.Forms.Label()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.SuspendLayout()
        '
        'chkPasteExpiredCount
        '
        resources.ApplyResources(Me.chkPasteExpiredCount, "chkPasteExpiredCount")
        Me.chkPasteExpiredCount.Name = "chkPasteExpiredCount"
        Me.ToolTip.SetToolTip(Me.chkPasteExpiredCount, resources.GetString("chkPasteExpiredCount.ToolTip"))
        Me.chkPasteExpiredCount.UseVisualStyleBackColor = True
        '
        'chkPasteExpireTime
        '
        resources.ApplyResources(Me.chkPasteExpireTime, "chkPasteExpireTime")
        Me.chkPasteExpireTime.Name = "chkPasteExpireTime"
        Me.ToolTip.SetToolTip(Me.chkPasteExpireTime, resources.GetString("chkPasteExpireTime.ToolTip"))
        Me.chkPasteExpireTime.UseVisualStyleBackColor = True
        '
        'Label49
        '
        resources.ApplyResources(Me.Label49, "Label49")
        Me.Label49.Name = "Label49"
        Me.ToolTip.SetToolTip(Me.Label49, resources.GetString("Label49.ToolTip"))
        '
        'Label110
        '
        resources.ApplyResources(Me.Label110, "Label110")
        Me.Label110.Name = "Label110"
        Me.ToolTip.SetToolTip(Me.Label110, resources.GetString("Label110.ToolTip"))
        '
        'txtPasteExpiredCount
        '
        resources.ApplyResources(Me.txtPasteExpiredCount, "txtPasteExpiredCount")
        Me.txtPasteExpiredCount.Name = "txtPasteExpiredCount"
        Me.ToolTip.SetToolTip(Me.txtPasteExpiredCount, resources.GetString("txtPasteExpiredCount.ToolTip"))
        '
        'txtPasteExpiredTime
        '
        resources.ApplyResources(Me.txtPasteExpiredTime, "txtPasteExpiredTime")
        Me.txtPasteExpiredTime.Name = "txtPasteExpiredTime"
        Me.ToolTip.SetToolTip(Me.txtPasteExpiredTime, resources.GetString("txtPasteExpiredTime.ToolTip"))
        '
        'Label119
        '
        resources.ApplyResources(Me.Label119, "Label119")
        Me.Label119.AutoEllipsis = True
        Me.Label119.Name = "Label119"
        Me.ToolTip.SetToolTip(Me.Label119, resources.GetString("Label119.ToolTip"))
        '
        'btnBack
        '
        resources.ApplyResources(Me.btnBack, "btnBack")
        Me.btnBack.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnBack.Name = "btnBack"
        Me.ToolTip.SetToolTip(Me.btnBack, resources.GetString("btnBack.ToolTip"))
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btn_Cancel
        '
        resources.ApplyResources(Me.btn_Cancel, "btn_Cancel")
        Me.btn_Cancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.ToolTip.SetToolTip(Me.btn_Cancel, resources.GetString("btn_Cancel.ToolTip"))
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        Me.ToolTip.SetToolTip(Me.UcStatusBar1, resources.GetString("UcStatusBar1.ToolTip"))
        '
        'frmExchange
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.chkPasteExpiredCount)
        Me.Controls.Add(Me.chkPasteExpireTime)
        Me.Controls.Add(Me.Label49)
        Me.Controls.Add(Me.Label110)
        Me.Controls.Add(Me.txtPasteExpiredCount)
        Me.Controls.Add(Me.txtPasteExpiredTime)
        Me.Controls.Add(Me.Label119)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExchange"
        Me.ToolTip.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkPasteExpiredCount As System.Windows.Forms.CheckBox
    Friend WithEvents chkPasteExpireTime As System.Windows.Forms.CheckBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents txtPasteExpiredCount As System.Windows.Forms.TextBox
    Friend WithEvents txtPasteExpiredTime As System.Windows.Forms.TextBox
    Friend WithEvents Label119 As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
End Class

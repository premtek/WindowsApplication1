<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHelp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHelp))
        Me.lblProgramVersion = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.lblBoardVersion = New System.Windows.Forms.Label()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblProgramVersion
        '
        resources.ApplyResources(Me.lblProgramVersion, "lblProgramVersion")
        Me.lblProgramVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblProgramVersion.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblProgramVersion.Name = "lblProgramVersion"
        Me.ToolTip.SetToolTip(Me.lblProgramVersion, resources.GetString("lblProgramVersion.ToolTip"))
        '
        'Panel1
        '
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        Me.ToolTip.SetToolTip(Me.Panel1, resources.GetString("Panel1.ToolTip"))
        '
        'txtDesc
        '
        resources.ApplyResources(Me.txtDesc, "txtDesc")
        Me.txtDesc.Name = "txtDesc"
        Me.ToolTip.SetToolTip(Me.txtDesc, resources.GetString("txtDesc.ToolTip"))
        '
        'lblBoardVersion
        '
        resources.ApplyResources(Me.lblBoardVersion, "lblBoardVersion")
        Me.lblBoardVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblBoardVersion.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblBoardVersion.Name = "lblBoardVersion"
        Me.ToolTip.SetToolTip(Me.lblBoardVersion, resources.GetString("lblBoardVersion.ToolTip"))
        '
        'btnOK
        '
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnOK.Name = "btnOK"
        Me.ToolTip.SetToolTip(Me.btnOK, resources.GetString("btnOK.ToolTip"))
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmHelp
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblBoardVersion)
        Me.Controls.Add(Me.lblProgramVersion)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHelp"
        Me.ToolTip.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblProgramVersion As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblBoardVersion As System.Windows.Forms.Label
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
End Class

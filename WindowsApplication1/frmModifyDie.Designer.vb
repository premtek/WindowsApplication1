<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmModifyDie
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmModifyDie))
        Me.btnDieModifyOK = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbEditType = New System.Windows.Forms.ComboBox()
        Me.UcWaferMap1 = New WindowsApplication1.ucWaferMap()
        Me.SuspendLayout()
        '
        'btnDieModifyOK
        '
        resources.ApplyResources(Me.btnDieModifyOK, "btnDieModifyOK")
        Me.btnDieModifyOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnDieModifyOK.Name = "btnDieModifyOK"
        Me.ToolTip1.SetToolTip(Me.btnDieModifyOK, resources.GetString("btnDieModifyOK.ToolTip"))
        Me.btnDieModifyOK.UseVisualStyleBackColor = True
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.Button1.Name = "Button1"
        Me.ToolTip1.SetToolTip(Me.Button1, resources.GetString("Button1.ToolTip"))
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.Name = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'cmbEditType
        '
        resources.ApplyResources(Me.cmbEditType, "cmbEditType")
        Me.cmbEditType.FormattingEnabled = True
        Me.cmbEditType.Items.AddRange(New Object() {resources.GetString("cmbEditType.Items"), resources.GetString("cmbEditType.Items1"), resources.GetString("cmbEditType.Items2"), resources.GetString("cmbEditType.Items3")})
        Me.cmbEditType.Name = "cmbEditType"
        '
        'UcWaferMap1
        '
        Me.UcWaferMap1.BackColor = System.Drawing.Color.WhiteSmoke
        resources.ApplyResources(Me.UcWaferMap1, "UcWaferMap1")
        Me.UcWaferMap1.Name = "UcWaferMap1"
        '
        'frmModifyDie
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.cmbEditType)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnDieModifyOK)
        Me.Controls.Add(Me.UcWaferMap1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmModifyDie"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcWaferMap1 As WindowsApplication1.ucWaferMap
    Friend WithEvents btnDieModifyOK As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmbEditType As System.Windows.Forms.ComboBox
End Class

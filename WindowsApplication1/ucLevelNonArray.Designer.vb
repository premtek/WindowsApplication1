<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucLevelNonArray
    Inherits System.Windows.Forms.UserControl

    'UserControl 覆寫 Dispose 以清除元件清單。
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucLevelNonArray))
        Me.grpNonArray = New System.Windows.Forms.GroupBox()
        Me.nmcPosY = New System.Windows.Forms.NumericUpDown()
        Me.nmcPosX = New System.Windows.Forms.NumericUpDown()
        Me.lblPosX = New System.Windows.Forms.Label()
        Me.lblPosYUnit = New System.Windows.Forms.Label()
        Me.lblPosXUnit = New System.Windows.Forms.Label()
        Me.lblPosY = New System.Windows.Forms.Label()
        Me.lstNonArray = New System.Windows.Forms.ListBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnMovePos = New System.Windows.Forms.Button()
        Me.btnSetPos = New System.Windows.Forms.Button()
        Me.grpNonArray.SuspendLayout()
        CType(Me.nmcPosY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcPosX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpNonArray
        '
        resources.ApplyResources(Me.grpNonArray, "grpNonArray")
        Me.grpNonArray.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.grpNonArray.Controls.Add(Me.btnAdd)
        Me.grpNonArray.Controls.Add(Me.btnDelete)
        Me.grpNonArray.Controls.Add(Me.btnMovePos)
        Me.grpNonArray.Controls.Add(Me.btnSetPos)
        Me.grpNonArray.Controls.Add(Me.nmcPosY)
        Me.grpNonArray.Controls.Add(Me.nmcPosX)
        Me.grpNonArray.Controls.Add(Me.lblPosX)
        Me.grpNonArray.Controls.Add(Me.lblPosYUnit)
        Me.grpNonArray.Controls.Add(Me.lblPosXUnit)
        Me.grpNonArray.Controls.Add(Me.lblPosY)
        Me.grpNonArray.Controls.Add(Me.lstNonArray)
        Me.grpNonArray.Name = "grpNonArray"
        Me.grpNonArray.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpNonArray, resources.GetString("grpNonArray.ToolTip"))
        '
        'nmcPosY
        '
        resources.ApplyResources(Me.nmcPosY, "nmcPosY")
        Me.nmcPosY.DecimalPlaces = 3
        Me.nmcPosY.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcPosY.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcPosY.Name = "nmcPosY"
        Me.ToolTip1.SetToolTip(Me.nmcPosY, resources.GetString("nmcPosY.ToolTip"))
        '
        'nmcPosX
        '
        resources.ApplyResources(Me.nmcPosX, "nmcPosX")
        Me.nmcPosX.DecimalPlaces = 3
        Me.nmcPosX.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcPosX.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcPosX.Name = "nmcPosX"
        Me.ToolTip1.SetToolTip(Me.nmcPosX, resources.GetString("nmcPosX.ToolTip"))
        '
        'lblPosX
        '
        resources.ApplyResources(Me.lblPosX, "lblPosX")
        Me.lblPosX.Name = "lblPosX"
        Me.ToolTip1.SetToolTip(Me.lblPosX, resources.GetString("lblPosX.ToolTip"))
        '
        'lblPosYUnit
        '
        resources.ApplyResources(Me.lblPosYUnit, "lblPosYUnit")
        Me.lblPosYUnit.Name = "lblPosYUnit"
        Me.ToolTip1.SetToolTip(Me.lblPosYUnit, resources.GetString("lblPosYUnit.ToolTip"))
        '
        'lblPosXUnit
        '
        resources.ApplyResources(Me.lblPosXUnit, "lblPosXUnit")
        Me.lblPosXUnit.Name = "lblPosXUnit"
        Me.ToolTip1.SetToolTip(Me.lblPosXUnit, resources.GetString("lblPosXUnit.ToolTip"))
        '
        'lblPosY
        '
        resources.ApplyResources(Me.lblPosY, "lblPosY")
        Me.lblPosY.Name = "lblPosY"
        Me.ToolTip1.SetToolTip(Me.lblPosY, resources.GetString("lblPosY.ToolTip"))
        '
        'lstNonArray
        '
        resources.ApplyResources(Me.lstNonArray, "lstNonArray")
        Me.lstNonArray.FormattingEnabled = True
        Me.lstNonArray.Name = "lstNonArray"
        Me.ToolTip1.SetToolTip(Me.lstNonArray, resources.GetString("lstNonArray.ToolTip"))
        '
        'btnAdd
        '
        resources.ApplyResources(Me.btnAdd, "btnAdd")
        Me.btnAdd.BackColor = System.Drawing.SystemColors.Control
        Me.btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.Name = "btnAdd"
        Me.ToolTip1.SetToolTip(Me.btnAdd, resources.GetString("btnAdd.ToolTip"))
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        resources.ApplyResources(Me.btnDelete, "btnDelete")
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.Name = "btnDelete"
        Me.ToolTip1.SetToolTip(Me.btnDelete, resources.GetString("btnDelete.ToolTip"))
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnMovePos
        '
        resources.ApplyResources(Me.btnMovePos, "btnMovePos")
        Me.btnMovePos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnMovePos.FlatAppearance.BorderSize = 0
        Me.btnMovePos.Name = "btnMovePos"
        Me.ToolTip1.SetToolTip(Me.btnMovePos, resources.GetString("btnMovePos.ToolTip"))
        Me.btnMovePos.UseVisualStyleBackColor = True
        '
        'btnSetPos
        '
        resources.ApplyResources(Me.btnSetPos, "btnSetPos")
        Me.btnSetPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetPos.FlatAppearance.BorderSize = 0
        Me.btnSetPos.Name = "btnSetPos"
        Me.ToolTip1.SetToolTip(Me.btnSetPos, resources.GetString("btnSetPos.ToolTip"))
        Me.btnSetPos.UseVisualStyleBackColor = True
        '
        'ucLevelNonArray
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.grpNonArray)
        Me.Name = "ucLevelNonArray"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpNonArray.ResumeLayout(False)
        CType(Me.nmcPosY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcPosX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpNonArray As System.Windows.Forms.GroupBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnMovePos As System.Windows.Forms.Button
    Friend WithEvents btnSetPos As System.Windows.Forms.Button
    Friend WithEvents nmcPosY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcPosX As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblPosX As System.Windows.Forms.Label
    Friend WithEvents lblPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblPosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblPosY As System.Windows.Forms.Label
    Friend WithEvents lstNonArray As System.Windows.Forms.ListBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class

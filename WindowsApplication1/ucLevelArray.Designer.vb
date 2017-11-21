<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucLevelArray
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucLevelArray))
        Me.grpArray = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.nmcStartPosZ = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.nmcArrayYPitch = New System.Windows.Forms.NumericUpDown()
        Me.nmcArrayYCount = New System.Windows.Forms.NumericUpDown()
        Me.nmcArrayXPitch = New System.Windows.Forms.NumericUpDown()
        Me.nmcArrayXCount = New System.Windows.Forms.NumericUpDown()
        Me.nmcEndPosY = New System.Windows.Forms.NumericUpDown()
        Me.nmcEndPosX = New System.Windows.Forms.NumericUpDown()
        Me.nmcStartPosY = New System.Windows.Forms.NumericUpDown()
        Me.nmcStartPosX = New System.Windows.Forms.NumericUpDown()
        Me.btnSetPitch = New System.Windows.Forms.Button()
        Me.lblArrayXCount = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblArrayYCount = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblArrayPosYUnit = New System.Windows.Forms.Label()
        Me.lblArrayXPitch = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblArrayPosXUnit = New System.Windows.Forms.Label()
        Me.lblArrayYPitch = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblArrayPosX = New System.Windows.Forms.Label()
        Me.lblArrayPosY = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnGoEndPos = New System.Windows.Forms.Button()
        Me.btnGoStartPos = New System.Windows.Forms.Button()
        Me.btnSetEndPos = New System.Windows.Forms.Button()
        Me.btnSetStartPos = New System.Windows.Forms.Button()
        Me.grpArray.SuspendLayout()
        CType(Me.nmcStartPosZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcArrayYPitch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcArrayYCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcArrayXPitch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcArrayXCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcEndPosY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcEndPosX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcStartPosY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcStartPosX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpArray
        '
        resources.ApplyResources(Me.grpArray, "grpArray")
        Me.grpArray.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.grpArray.Controls.Add(Me.Label9)
        Me.grpArray.Controls.Add(Me.nmcStartPosZ)
        Me.grpArray.Controls.Add(Me.Label8)
        Me.grpArray.Controls.Add(Me.nmcArrayYPitch)
        Me.grpArray.Controls.Add(Me.nmcArrayYCount)
        Me.grpArray.Controls.Add(Me.nmcArrayXPitch)
        Me.grpArray.Controls.Add(Me.nmcArrayXCount)
        Me.grpArray.Controls.Add(Me.nmcEndPosY)
        Me.grpArray.Controls.Add(Me.nmcEndPosX)
        Me.grpArray.Controls.Add(Me.nmcStartPosY)
        Me.grpArray.Controls.Add(Me.nmcStartPosX)
        Me.grpArray.Controls.Add(Me.btnGoEndPos)
        Me.grpArray.Controls.Add(Me.btnGoStartPos)
        Me.grpArray.Controls.Add(Me.btnSetPitch)
        Me.grpArray.Controls.Add(Me.lblArrayXCount)
        Me.grpArray.Controls.Add(Me.btnSetEndPos)
        Me.grpArray.Controls.Add(Me.Label3)
        Me.grpArray.Controls.Add(Me.btnSetStartPos)
        Me.grpArray.Controls.Add(Me.lblArrayYCount)
        Me.grpArray.Controls.Add(Me.Label6)
        Me.grpArray.Controls.Add(Me.Label1)
        Me.grpArray.Controls.Add(Me.Label5)
        Me.grpArray.Controls.Add(Me.lblArrayPosYUnit)
        Me.grpArray.Controls.Add(Me.lblArrayXPitch)
        Me.grpArray.Controls.Add(Me.Label2)
        Me.grpArray.Controls.Add(Me.lblArrayPosXUnit)
        Me.grpArray.Controls.Add(Me.lblArrayYPitch)
        Me.grpArray.Controls.Add(Me.Label4)
        Me.grpArray.Controls.Add(Me.lblArrayPosX)
        Me.grpArray.Controls.Add(Me.lblArrayPosY)
        Me.grpArray.Name = "grpArray"
        Me.grpArray.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpArray, resources.GetString("grpArray.ToolTip"))
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        Me.ToolTip1.SetToolTip(Me.Label9, resources.GetString("Label9.ToolTip"))
        '
        'nmcStartPosZ
        '
        resources.ApplyResources(Me.nmcStartPosZ, "nmcStartPosZ")
        Me.nmcStartPosZ.DecimalPlaces = 3
        Me.nmcStartPosZ.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcStartPosZ.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcStartPosZ.Name = "nmcStartPosZ"
        Me.ToolTip1.SetToolTip(Me.nmcStartPosZ, resources.GetString("nmcStartPosZ.ToolTip"))
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        Me.ToolTip1.SetToolTip(Me.Label8, resources.GetString("Label8.ToolTip"))
        '
        'nmcArrayYPitch
        '
        resources.ApplyResources(Me.nmcArrayYPitch, "nmcArrayYPitch")
        Me.nmcArrayYPitch.DecimalPlaces = 2
        Me.nmcArrayYPitch.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcArrayYPitch.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcArrayYPitch.Name = "nmcArrayYPitch"
        Me.ToolTip1.SetToolTip(Me.nmcArrayYPitch, resources.GetString("nmcArrayYPitch.ToolTip"))
        Me.nmcArrayYPitch.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmcArrayYCount
        '
        resources.ApplyResources(Me.nmcArrayYCount, "nmcArrayYCount")
        Me.nmcArrayYCount.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcArrayYCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcArrayYCount.Name = "nmcArrayYCount"
        Me.ToolTip1.SetToolTip(Me.nmcArrayYCount, resources.GetString("nmcArrayYCount.ToolTip"))
        Me.nmcArrayYCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmcArrayXPitch
        '
        resources.ApplyResources(Me.nmcArrayXPitch, "nmcArrayXPitch")
        Me.nmcArrayXPitch.DecimalPlaces = 2
        Me.nmcArrayXPitch.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcArrayXPitch.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcArrayXPitch.Name = "nmcArrayXPitch"
        Me.ToolTip1.SetToolTip(Me.nmcArrayXPitch, resources.GetString("nmcArrayXPitch.ToolTip"))
        Me.nmcArrayXPitch.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmcArrayXCount
        '
        resources.ApplyResources(Me.nmcArrayXCount, "nmcArrayXCount")
        Me.nmcArrayXCount.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nmcArrayXCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcArrayXCount.Name = "nmcArrayXCount"
        Me.ToolTip1.SetToolTip(Me.nmcArrayXCount, resources.GetString("nmcArrayXCount.ToolTip"))
        Me.nmcArrayXCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmcEndPosY
        '
        resources.ApplyResources(Me.nmcEndPosY, "nmcEndPosY")
        Me.nmcEndPosY.DecimalPlaces = 3
        Me.nmcEndPosY.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcEndPosY.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcEndPosY.Name = "nmcEndPosY"
        Me.ToolTip1.SetToolTip(Me.nmcEndPosY, resources.GetString("nmcEndPosY.ToolTip"))
        '
        'nmcEndPosX
        '
        resources.ApplyResources(Me.nmcEndPosX, "nmcEndPosX")
        Me.nmcEndPosX.DecimalPlaces = 3
        Me.nmcEndPosX.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcEndPosX.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcEndPosX.Name = "nmcEndPosX"
        Me.ToolTip1.SetToolTip(Me.nmcEndPosX, resources.GetString("nmcEndPosX.ToolTip"))
        '
        'nmcStartPosY
        '
        resources.ApplyResources(Me.nmcStartPosY, "nmcStartPosY")
        Me.nmcStartPosY.DecimalPlaces = 3
        Me.nmcStartPosY.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcStartPosY.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcStartPosY.Name = "nmcStartPosY"
        Me.ToolTip1.SetToolTip(Me.nmcStartPosY, resources.GetString("nmcStartPosY.ToolTip"))
        '
        'nmcStartPosX
        '
        resources.ApplyResources(Me.nmcStartPosX, "nmcStartPosX")
        Me.nmcStartPosX.DecimalPlaces = 3
        Me.nmcStartPosX.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcStartPosX.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcStartPosX.Name = "nmcStartPosX"
        Me.ToolTip1.SetToolTip(Me.nmcStartPosX, resources.GetString("nmcStartPosX.ToolTip"))
        '
        'btnSetPitch
        '
        resources.ApplyResources(Me.btnSetPitch, "btnSetPitch")
        Me.btnSetPitch.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetPitch.FlatAppearance.BorderSize = 0
        Me.btnSetPitch.Name = "btnSetPitch"
        Me.ToolTip1.SetToolTip(Me.btnSetPitch, resources.GetString("btnSetPitch.ToolTip"))
        Me.btnSetPitch.UseVisualStyleBackColor = True
        '
        'lblArrayXCount
        '
        resources.ApplyResources(Me.lblArrayXCount, "lblArrayXCount")
        Me.lblArrayXCount.Name = "lblArrayXCount"
        Me.ToolTip1.SetToolTip(Me.lblArrayXCount, resources.GetString("lblArrayXCount.ToolTip"))
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        Me.ToolTip1.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip"))
        '
        'lblArrayYCount
        '
        resources.ApplyResources(Me.lblArrayYCount, "lblArrayYCount")
        Me.lblArrayYCount.Name = "lblArrayYCount"
        Me.ToolTip1.SetToolTip(Me.lblArrayYCount, resources.GetString("lblArrayYCount.ToolTip"))
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        Me.ToolTip1.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip"))
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        Me.ToolTip1.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip"))
        '
        'lblArrayPosYUnit
        '
        resources.ApplyResources(Me.lblArrayPosYUnit, "lblArrayPosYUnit")
        Me.lblArrayPosYUnit.Name = "lblArrayPosYUnit"
        Me.ToolTip1.SetToolTip(Me.lblArrayPosYUnit, resources.GetString("lblArrayPosYUnit.ToolTip"))
        '
        'lblArrayXPitch
        '
        resources.ApplyResources(Me.lblArrayXPitch, "lblArrayXPitch")
        Me.lblArrayXPitch.Name = "lblArrayXPitch"
        Me.ToolTip1.SetToolTip(Me.lblArrayXPitch, resources.GetString("lblArrayXPitch.ToolTip"))
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        Me.ToolTip1.SetToolTip(Me.Label2, resources.GetString("Label2.ToolTip"))
        '
        'lblArrayPosXUnit
        '
        resources.ApplyResources(Me.lblArrayPosXUnit, "lblArrayPosXUnit")
        Me.lblArrayPosXUnit.Name = "lblArrayPosXUnit"
        Me.ToolTip1.SetToolTip(Me.lblArrayPosXUnit, resources.GetString("lblArrayPosXUnit.ToolTip"))
        '
        'lblArrayYPitch
        '
        resources.ApplyResources(Me.lblArrayYPitch, "lblArrayYPitch")
        Me.lblArrayYPitch.Name = "lblArrayYPitch"
        Me.ToolTip1.SetToolTip(Me.lblArrayYPitch, resources.GetString("lblArrayYPitch.ToolTip"))
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        Me.ToolTip1.SetToolTip(Me.Label4, resources.GetString("Label4.ToolTip"))
        '
        'lblArrayPosX
        '
        resources.ApplyResources(Me.lblArrayPosX, "lblArrayPosX")
        Me.lblArrayPosX.Name = "lblArrayPosX"
        Me.ToolTip1.SetToolTip(Me.lblArrayPosX, resources.GetString("lblArrayPosX.ToolTip"))
        '
        'lblArrayPosY
        '
        resources.ApplyResources(Me.lblArrayPosY, "lblArrayPosY")
        Me.lblArrayPosY.Name = "lblArrayPosY"
        Me.ToolTip1.SetToolTip(Me.lblArrayPosY, resources.GetString("lblArrayPosY.ToolTip"))
        '
        'btnGoEndPos
        '
        resources.ApplyResources(Me.btnGoEndPos, "btnGoEndPos")
        Me.btnGoEndPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnGoEndPos.FlatAppearance.BorderSize = 0
        Me.btnGoEndPos.Name = "btnGoEndPos"
        Me.ToolTip1.SetToolTip(Me.btnGoEndPos, resources.GetString("btnGoEndPos.ToolTip"))
        Me.btnGoEndPos.UseVisualStyleBackColor = True
        '
        'btnGoStartPos
        '
        resources.ApplyResources(Me.btnGoStartPos, "btnGoStartPos")
        Me.btnGoStartPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        Me.btnGoStartPos.FlatAppearance.BorderSize = 0
        Me.btnGoStartPos.Name = "btnGoStartPos"
        Me.ToolTip1.SetToolTip(Me.btnGoStartPos, resources.GetString("btnGoStartPos.ToolTip"))
        Me.btnGoStartPos.UseVisualStyleBackColor = True
        '
        'btnSetEndPos
        '
        resources.ApplyResources(Me.btnSetEndPos, "btnSetEndPos")
        Me.btnSetEndPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetEndPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetEndPos.FlatAppearance.BorderSize = 0
        Me.btnSetEndPos.Name = "btnSetEndPos"
        Me.ToolTip1.SetToolTip(Me.btnSetEndPos, resources.GetString("btnSetEndPos.ToolTip"))
        Me.btnSetEndPos.UseVisualStyleBackColor = True
        '
        'btnSetStartPos
        '
        resources.ApplyResources(Me.btnSetStartPos, "btnSetStartPos")
        Me.btnSetStartPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetStartPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetStartPos.FlatAppearance.BorderSize = 0
        Me.btnSetStartPos.Name = "btnSetStartPos"
        Me.ToolTip1.SetToolTip(Me.btnSetStartPos, resources.GetString("btnSetStartPos.ToolTip"))
        Me.btnSetStartPos.UseVisualStyleBackColor = True
        '
        'ucLevelArray
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.grpArray)
        Me.Name = "ucLevelArray"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpArray.ResumeLayout(False)
        CType(Me.nmcStartPosZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcArrayYPitch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcArrayYCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcArrayXPitch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcArrayXCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcEndPosY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcEndPosX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcStartPosY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcStartPosX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpArray As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents nmcStartPosZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents nmcArrayYPitch As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcArrayYCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcArrayXPitch As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcArrayXCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcEndPosY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcEndPosX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcStartPosY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcStartPosX As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnGoEndPos As System.Windows.Forms.Button
    Friend WithEvents btnGoStartPos As System.Windows.Forms.Button
    Friend WithEvents btnSetPitch As System.Windows.Forms.Button
    Friend WithEvents lblArrayXCount As System.Windows.Forms.Label
    Friend WithEvents btnSetEndPos As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSetStartPos As System.Windows.Forms.Button
    Friend WithEvents lblArrayYCount As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblArrayPosYUnit As System.Windows.Forms.Label
    Friend WithEvents lblArrayXPitch As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblArrayPosXUnit As System.Windows.Forms.Label
    Friend WithEvents lblArrayYPitch As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblArrayPosY As System.Windows.Forms.Label
    Friend WithEvents lblArrayPosX As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class

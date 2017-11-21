<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSafePos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSafePos))
        Me.grpSpanA = New System.Windows.Forms.GroupBox()
        Me.nmcSpanAX = New System.Windows.Forms.NumericUpDown()
        Me.nmcSafeDistanceAY = New System.Windows.Forms.NumericUpDown()
        Me.lblSpanAX = New System.Windows.Forms.Label()
        Me.lblSpanAY = New System.Windows.Forms.Label()
        Me.nmcSpanAY = New System.Windows.Forms.NumericUpDown()
        Me.lblSafeDistanceAY = New System.Windows.Forms.Label()
        Me.lblSafeDistanceAX = New System.Windows.Forms.Label()
        Me.nmcSafeDistanceAX = New System.Windows.Forms.NumericUpDown()
        Me.lblSpanAYUnit = New System.Windows.Forms.Label()
        Me.lblSafeDistanceAXUnit = New System.Windows.Forms.Label()
        Me.lblSpanAXUnit = New System.Windows.Forms.Label()
        Me.lblSafeDistanceAYUnit = New System.Windows.Forms.Label()
        Me.grpSpanB = New System.Windows.Forms.GroupBox()
        Me.nmcSpanBX = New System.Windows.Forms.NumericUpDown()
        Me.nmcSafeDistanceBY = New System.Windows.Forms.NumericUpDown()
        Me.lblSpanBX = New System.Windows.Forms.Label()
        Me.lblSpanBY = New System.Windows.Forms.Label()
        Me.nmcSpanBY = New System.Windows.Forms.NumericUpDown()
        Me.lblSafeDistanceBY = New System.Windows.Forms.Label()
        Me.lblSafeDistanceBX = New System.Windows.Forms.Label()
        Me.nmcSafeDistanceBX = New System.Windows.Forms.NumericUpDown()
        Me.lblSpanBYUnit = New System.Windows.Forms.Label()
        Me.lblSafeDistanceBXUnit = New System.Windows.Forms.Label()
        Me.lblSpanBXUnit = New System.Windows.Forms.Label()
        Me.lblSafeDistanceBYUnit = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.grpSpanA.SuspendLayout()
        CType(Me.nmcSpanAX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcSafeDistanceAY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcSpanAY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcSafeDistanceAX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSpanB.SuspendLayout()
        CType(Me.nmcSpanBX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcSafeDistanceBY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcSpanBY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcSafeDistanceBX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpSpanA
        '
        resources.ApplyResources(Me.grpSpanA, "grpSpanA")
        Me.grpSpanA.Controls.Add(Me.nmcSpanAX)
        Me.grpSpanA.Controls.Add(Me.nmcSafeDistanceAY)
        Me.grpSpanA.Controls.Add(Me.lblSpanAX)
        Me.grpSpanA.Controls.Add(Me.lblSpanAY)
        Me.grpSpanA.Controls.Add(Me.nmcSpanAY)
        Me.grpSpanA.Controls.Add(Me.lblSafeDistanceAY)
        Me.grpSpanA.Controls.Add(Me.lblSafeDistanceAX)
        Me.grpSpanA.Controls.Add(Me.nmcSafeDistanceAX)
        Me.grpSpanA.Controls.Add(Me.lblSpanAYUnit)
        Me.grpSpanA.Controls.Add(Me.lblSafeDistanceAXUnit)
        Me.grpSpanA.Controls.Add(Me.lblSpanAXUnit)
        Me.grpSpanA.Controls.Add(Me.lblSafeDistanceAYUnit)
        Me.grpSpanA.Name = "grpSpanA"
        Me.grpSpanA.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpSpanA, resources.GetString("grpSpanA.ToolTip"))
        '
        'nmcSpanAX
        '
        resources.ApplyResources(Me.nmcSpanAX, "nmcSpanAX")
        Me.nmcSpanAX.DecimalPlaces = 3
        Me.nmcSpanAX.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcSpanAX.Name = "nmcSpanAX"
        Me.ToolTip1.SetToolTip(Me.nmcSpanAX, resources.GetString("nmcSpanAX.ToolTip"))
        '
        'nmcSafeDistanceAY
        '
        resources.ApplyResources(Me.nmcSafeDistanceAY, "nmcSafeDistanceAY")
        Me.nmcSafeDistanceAY.DecimalPlaces = 3
        Me.nmcSafeDistanceAY.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcSafeDistanceAY.Name = "nmcSafeDistanceAY"
        Me.ToolTip1.SetToolTip(Me.nmcSafeDistanceAY, resources.GetString("nmcSafeDistanceAY.ToolTip"))
        '
        'lblSpanAX
        '
        resources.ApplyResources(Me.lblSpanAX, "lblSpanAX")
        Me.lblSpanAX.Name = "lblSpanAX"
        Me.ToolTip1.SetToolTip(Me.lblSpanAX, resources.GetString("lblSpanAX.ToolTip"))
        '
        'lblSpanAY
        '
        resources.ApplyResources(Me.lblSpanAY, "lblSpanAY")
        Me.lblSpanAY.Name = "lblSpanAY"
        Me.ToolTip1.SetToolTip(Me.lblSpanAY, resources.GetString("lblSpanAY.ToolTip"))
        '
        'nmcSpanAY
        '
        resources.ApplyResources(Me.nmcSpanAY, "nmcSpanAY")
        Me.nmcSpanAY.DecimalPlaces = 3
        Me.nmcSpanAY.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcSpanAY.Name = "nmcSpanAY"
        Me.ToolTip1.SetToolTip(Me.nmcSpanAY, resources.GetString("nmcSpanAY.ToolTip"))
        '
        'lblSafeDistanceAY
        '
        resources.ApplyResources(Me.lblSafeDistanceAY, "lblSafeDistanceAY")
        Me.lblSafeDistanceAY.Name = "lblSafeDistanceAY"
        Me.ToolTip1.SetToolTip(Me.lblSafeDistanceAY, resources.GetString("lblSafeDistanceAY.ToolTip"))
        '
        'lblSafeDistanceAX
        '
        resources.ApplyResources(Me.lblSafeDistanceAX, "lblSafeDistanceAX")
        Me.lblSafeDistanceAX.Name = "lblSafeDistanceAX"
        Me.ToolTip1.SetToolTip(Me.lblSafeDistanceAX, resources.GetString("lblSafeDistanceAX.ToolTip"))
        '
        'nmcSafeDistanceAX
        '
        resources.ApplyResources(Me.nmcSafeDistanceAX, "nmcSafeDistanceAX")
        Me.nmcSafeDistanceAX.DecimalPlaces = 3
        Me.nmcSafeDistanceAX.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcSafeDistanceAX.Name = "nmcSafeDistanceAX"
        Me.ToolTip1.SetToolTip(Me.nmcSafeDistanceAX, resources.GetString("nmcSafeDistanceAX.ToolTip"))
        '
        'lblSpanAYUnit
        '
        resources.ApplyResources(Me.lblSpanAYUnit, "lblSpanAYUnit")
        Me.lblSpanAYUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblSpanAYUnit.Name = "lblSpanAYUnit"
        Me.ToolTip1.SetToolTip(Me.lblSpanAYUnit, resources.GetString("lblSpanAYUnit.ToolTip"))
        '
        'lblSafeDistanceAXUnit
        '
        resources.ApplyResources(Me.lblSafeDistanceAXUnit, "lblSafeDistanceAXUnit")
        Me.lblSafeDistanceAXUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblSafeDistanceAXUnit.Name = "lblSafeDistanceAXUnit"
        Me.ToolTip1.SetToolTip(Me.lblSafeDistanceAXUnit, resources.GetString("lblSafeDistanceAXUnit.ToolTip"))
        '
        'lblSpanAXUnit
        '
        resources.ApplyResources(Me.lblSpanAXUnit, "lblSpanAXUnit")
        Me.lblSpanAXUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblSpanAXUnit.Name = "lblSpanAXUnit"
        Me.ToolTip1.SetToolTip(Me.lblSpanAXUnit, resources.GetString("lblSpanAXUnit.ToolTip"))
        '
        'lblSafeDistanceAYUnit
        '
        resources.ApplyResources(Me.lblSafeDistanceAYUnit, "lblSafeDistanceAYUnit")
        Me.lblSafeDistanceAYUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblSafeDistanceAYUnit.Name = "lblSafeDistanceAYUnit"
        Me.ToolTip1.SetToolTip(Me.lblSafeDistanceAYUnit, resources.GetString("lblSafeDistanceAYUnit.ToolTip"))
        '
        'grpSpanB
        '
        resources.ApplyResources(Me.grpSpanB, "grpSpanB")
        Me.grpSpanB.Controls.Add(Me.nmcSpanBX)
        Me.grpSpanB.Controls.Add(Me.nmcSafeDistanceBY)
        Me.grpSpanB.Controls.Add(Me.lblSpanBX)
        Me.grpSpanB.Controls.Add(Me.lblSpanBY)
        Me.grpSpanB.Controls.Add(Me.nmcSpanBY)
        Me.grpSpanB.Controls.Add(Me.lblSafeDistanceBY)
        Me.grpSpanB.Controls.Add(Me.lblSafeDistanceBX)
        Me.grpSpanB.Controls.Add(Me.nmcSafeDistanceBX)
        Me.grpSpanB.Controls.Add(Me.lblSpanBYUnit)
        Me.grpSpanB.Controls.Add(Me.lblSafeDistanceBXUnit)
        Me.grpSpanB.Controls.Add(Me.lblSpanBXUnit)
        Me.grpSpanB.Controls.Add(Me.lblSafeDistanceBYUnit)
        Me.grpSpanB.Name = "grpSpanB"
        Me.grpSpanB.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpSpanB, resources.GetString("grpSpanB.ToolTip"))
        '
        'nmcSpanBX
        '
        resources.ApplyResources(Me.nmcSpanBX, "nmcSpanBX")
        Me.nmcSpanBX.DecimalPlaces = 3
        Me.nmcSpanBX.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcSpanBX.Name = "nmcSpanBX"
        Me.ToolTip1.SetToolTip(Me.nmcSpanBX, resources.GetString("nmcSpanBX.ToolTip"))
        '
        'nmcSafeDistanceBY
        '
        resources.ApplyResources(Me.nmcSafeDistanceBY, "nmcSafeDistanceBY")
        Me.nmcSafeDistanceBY.DecimalPlaces = 3
        Me.nmcSafeDistanceBY.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcSafeDistanceBY.Name = "nmcSafeDistanceBY"
        Me.ToolTip1.SetToolTip(Me.nmcSafeDistanceBY, resources.GetString("nmcSafeDistanceBY.ToolTip"))
        '
        'lblSpanBX
        '
        resources.ApplyResources(Me.lblSpanBX, "lblSpanBX")
        Me.lblSpanBX.Name = "lblSpanBX"
        Me.ToolTip1.SetToolTip(Me.lblSpanBX, resources.GetString("lblSpanBX.ToolTip"))
        '
        'lblSpanBY
        '
        resources.ApplyResources(Me.lblSpanBY, "lblSpanBY")
        Me.lblSpanBY.Name = "lblSpanBY"
        Me.ToolTip1.SetToolTip(Me.lblSpanBY, resources.GetString("lblSpanBY.ToolTip"))
        '
        'nmcSpanBY
        '
        resources.ApplyResources(Me.nmcSpanBY, "nmcSpanBY")
        Me.nmcSpanBY.DecimalPlaces = 3
        Me.nmcSpanBY.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcSpanBY.Name = "nmcSpanBY"
        Me.ToolTip1.SetToolTip(Me.nmcSpanBY, resources.GetString("nmcSpanBY.ToolTip"))
        '
        'lblSafeDistanceBY
        '
        resources.ApplyResources(Me.lblSafeDistanceBY, "lblSafeDistanceBY")
        Me.lblSafeDistanceBY.Name = "lblSafeDistanceBY"
        Me.ToolTip1.SetToolTip(Me.lblSafeDistanceBY, resources.GetString("lblSafeDistanceBY.ToolTip"))
        '
        'lblSafeDistanceBX
        '
        resources.ApplyResources(Me.lblSafeDistanceBX, "lblSafeDistanceBX")
        Me.lblSafeDistanceBX.Name = "lblSafeDistanceBX"
        Me.ToolTip1.SetToolTip(Me.lblSafeDistanceBX, resources.GetString("lblSafeDistanceBX.ToolTip"))
        '
        'nmcSafeDistanceBX
        '
        resources.ApplyResources(Me.nmcSafeDistanceBX, "nmcSafeDistanceBX")
        Me.nmcSafeDistanceBX.DecimalPlaces = 3
        Me.nmcSafeDistanceBX.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcSafeDistanceBX.Name = "nmcSafeDistanceBX"
        Me.ToolTip1.SetToolTip(Me.nmcSafeDistanceBX, resources.GetString("nmcSafeDistanceBX.ToolTip"))
        '
        'lblSpanBYUnit
        '
        resources.ApplyResources(Me.lblSpanBYUnit, "lblSpanBYUnit")
        Me.lblSpanBYUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblSpanBYUnit.Name = "lblSpanBYUnit"
        Me.ToolTip1.SetToolTip(Me.lblSpanBYUnit, resources.GetString("lblSpanBYUnit.ToolTip"))
        '
        'lblSafeDistanceBXUnit
        '
        resources.ApplyResources(Me.lblSafeDistanceBXUnit, "lblSafeDistanceBXUnit")
        Me.lblSafeDistanceBXUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblSafeDistanceBXUnit.Name = "lblSafeDistanceBXUnit"
        Me.ToolTip1.SetToolTip(Me.lblSafeDistanceBXUnit, resources.GetString("lblSafeDistanceBXUnit.ToolTip"))
        '
        'lblSpanBXUnit
        '
        resources.ApplyResources(Me.lblSpanBXUnit, "lblSpanBXUnit")
        Me.lblSpanBXUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblSpanBXUnit.Name = "lblSpanBXUnit"
        Me.ToolTip1.SetToolTip(Me.lblSpanBXUnit, resources.GetString("lblSpanBXUnit.ToolTip"))
        '
        'lblSafeDistanceBYUnit
        '
        resources.ApplyResources(Me.lblSafeDistanceBYUnit, "lblSafeDistanceBYUnit")
        Me.lblSafeDistanceBYUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblSafeDistanceBYUnit.Name = "lblSafeDistanceBYUnit"
        Me.ToolTip1.SetToolTip(Me.lblSafeDistanceBYUnit, resources.GetString("lblSafeDistanceBYUnit.ToolTip"))
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
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
        'frmSafePos
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.grpSpanB)
        Me.Controls.Add(Me.grpSpanA)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSafePos"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpSpanA.ResumeLayout(False)
        CType(Me.nmcSpanAX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcSafeDistanceAY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcSpanAY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcSafeDistanceAX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSpanB.ResumeLayout(False)
        CType(Me.nmcSpanBX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcSafeDistanceBY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcSpanBY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcSafeDistanceBX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblSpanAY As System.Windows.Forms.Label
    Friend WithEvents lblSpanAX As System.Windows.Forms.Label
    Friend WithEvents nmcSpanAY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcSpanAX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcSafeDistanceAY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcSafeDistanceAX As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblSafeDistanceAX As System.Windows.Forms.Label
    Friend WithEvents lblSafeDistanceAY As System.Windows.Forms.Label
    Friend WithEvents lblSafeDistanceAXUnit As System.Windows.Forms.Label
    Friend WithEvents lblSafeDistanceAYUnit As System.Windows.Forms.Label
    Friend WithEvents lblSpanAXUnit As System.Windows.Forms.Label
    Friend WithEvents lblSpanAYUnit As System.Windows.Forms.Label
    Friend WithEvents grpSpanA As System.Windows.Forms.GroupBox
    Friend WithEvents grpSpanB As System.Windows.Forms.GroupBox
    Friend WithEvents nmcSpanBX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcSafeDistanceBY As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblSpanBX As System.Windows.Forms.Label
    Friend WithEvents lblSpanBY As System.Windows.Forms.Label
    Friend WithEvents nmcSpanBY As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblSafeDistanceBY As System.Windows.Forms.Label
    Friend WithEvents lblSafeDistanceBX As System.Windows.Forms.Label
    Friend WithEvents nmcSafeDistanceBX As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblSpanBYUnit As System.Windows.Forms.Label
    Friend WithEvents lblSafeDistanceBXUnit As System.Windows.Forms.Label
    Friend WithEvents lblSpanBXUnit As System.Windows.Forms.Label
    Friend WithEvents lblSafeDistanceBYUnit As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

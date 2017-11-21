<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetBalance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetBalance))
        Me.grpWeightConnection = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblWeight = New System.Windows.Forms.Label()
        Me.btnProductionWeighingUnit = New System.Windows.Forms.Button()
        Me.btnWeightUnitRezero = New System.Windows.Forms.Button()
        Me.lblCardType = New System.Windows.Forms.Label()
        Me.lblComPort = New System.Windows.Forms.Label()
        Me.cmbCardType = New System.Windows.Forms.ComboBox()
        Me.cmbWeightPort = New System.Windows.Forms.ComboBox()
        Me.cmbBalance = New System.Windows.Forms.ComboBox()
        Me.lblSelectBalance = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpWeightConnection.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpWeightConnection
        '
        resources.ApplyResources(Me.grpWeightConnection, "grpWeightConnection")
        Me.grpWeightConnection.Controls.Add(Me.Label1)
        Me.grpWeightConnection.Controls.Add(Me.lblWeight)
        Me.grpWeightConnection.Controls.Add(Me.btnProductionWeighingUnit)
        Me.grpWeightConnection.Controls.Add(Me.btnWeightUnitRezero)
        Me.grpWeightConnection.Controls.Add(Me.lblCardType)
        Me.grpWeightConnection.Controls.Add(Me.lblComPort)
        Me.grpWeightConnection.Controls.Add(Me.cmbCardType)
        Me.grpWeightConnection.Controls.Add(Me.cmbWeightPort)
        Me.grpWeightConnection.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpWeightConnection.Name = "grpWeightConnection"
        Me.grpWeightConnection.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpWeightConnection, resources.GetString("grpWeightConnection.ToolTip"))
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'lblWeight
        '
        resources.ApplyResources(Me.lblWeight, "lblWeight")
        Me.lblWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWeight.Name = "lblWeight"
        Me.ToolTip1.SetToolTip(Me.lblWeight, resources.GetString("lblWeight.ToolTip"))
        '
        'btnProductionWeighingUnit
        '
        resources.ApplyResources(Me.btnProductionWeighingUnit, "btnProductionWeighingUnit")
        Me.btnProductionWeighingUnit.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        Me.btnProductionWeighingUnit.FlatAppearance.BorderSize = 0
        Me.btnProductionWeighingUnit.Name = "btnProductionWeighingUnit"
        Me.ToolTip1.SetToolTip(Me.btnProductionWeighingUnit, resources.GetString("btnProductionWeighingUnit.ToolTip"))
        Me.btnProductionWeighingUnit.UseVisualStyleBackColor = True
        '
        'btnWeightUnitRezero
        '
        resources.ApplyResources(Me.btnWeightUnitRezero, "btnWeightUnitRezero")
        Me.btnWeightUnitRezero.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Rezero
        Me.btnWeightUnitRezero.FlatAppearance.BorderSize = 0
        Me.btnWeightUnitRezero.Name = "btnWeightUnitRezero"
        Me.ToolTip1.SetToolTip(Me.btnWeightUnitRezero, resources.GetString("btnWeightUnitRezero.ToolTip"))
        Me.btnWeightUnitRezero.UseVisualStyleBackColor = True
        '
        'lblCardType
        '
        resources.ApplyResources(Me.lblCardType, "lblCardType")
        Me.lblCardType.Name = "lblCardType"
        Me.ToolTip1.SetToolTip(Me.lblCardType, resources.GetString("lblCardType.ToolTip"))
        '
        'lblComPort
        '
        resources.ApplyResources(Me.lblComPort, "lblComPort")
        Me.lblComPort.Name = "lblComPort"
        Me.ToolTip1.SetToolTip(Me.lblComPort, resources.GetString("lblComPort.ToolTip"))
        '
        'cmbCardType
        '
        resources.ApplyResources(Me.cmbCardType, "cmbCardType")
        Me.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCardType.FormattingEnabled = True
        Me.cmbCardType.Name = "cmbCardType"
        Me.ToolTip1.SetToolTip(Me.cmbCardType, resources.GetString("cmbCardType.ToolTip"))
        '
        'cmbWeightPort
        '
        resources.ApplyResources(Me.cmbWeightPort, "cmbWeightPort")
        Me.cmbWeightPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbWeightPort.FormattingEnabled = True
        Me.cmbWeightPort.Name = "cmbWeightPort"
        Me.ToolTip1.SetToolTip(Me.cmbWeightPort, resources.GetString("cmbWeightPort.ToolTip"))
        '
        'cmbBalance
        '
        resources.ApplyResources(Me.cmbBalance, "cmbBalance")
        Me.cmbBalance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBalance.FormattingEnabled = True
        Me.cmbBalance.Items.AddRange(New Object() {resources.GetString("cmbBalance.Items"), resources.GetString("cmbBalance.Items1")})
        Me.cmbBalance.Name = "cmbBalance"
        Me.ToolTip1.SetToolTip(Me.cmbBalance, resources.GetString("cmbBalance.ToolTip"))
        '
        'lblSelectBalance
        '
        resources.ApplyResources(Me.lblSelectBalance, "lblSelectBalance")
        Me.lblSelectBalance.Name = "lblSelectBalance"
        Me.ToolTip1.SetToolTip(Me.lblSelectBalance, resources.GetString("lblSelectBalance.ToolTip"))
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
        'frmSetBalance
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.cmbBalance)
        Me.Controls.Add(Me.lblSelectBalance)
        Me.Controls.Add(Me.grpWeightConnection)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetBalance"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpWeightConnection.ResumeLayout(False)
        Me.grpWeightConnection.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpWeightConnection As System.Windows.Forms.GroupBox
    Friend WithEvents btnWeightUnitRezero As System.Windows.Forms.Button
    Friend WithEvents lblComPort As System.Windows.Forms.Label
    Friend WithEvents cmbWeightPort As System.Windows.Forms.ComboBox
    Friend WithEvents btnProductionWeighingUnit As System.Windows.Forms.Button
    Friend WithEvents lblWeight As System.Windows.Forms.Label
    Friend WithEvents cmbBalance As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelectBalance As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblCardType As System.Windows.Forms.Label
    Friend WithEvents cmbCardType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

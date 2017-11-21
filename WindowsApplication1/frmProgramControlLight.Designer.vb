<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProgramControlLight
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProgramControlLight))
        Me.lblCh1 = New System.Windows.Forms.Label()
        Me.lblCh2 = New System.Windows.Forms.Label()
        Me.lblCh3 = New System.Windows.Forms.Label()
        Me.lblCh4 = New System.Windows.Forms.Label()
        Me.cmbController = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.grpLightController = New System.Windows.Forms.GroupBox()
        Me.nmuLightValue4 = New System.Windows.Forms.NumericUpDown()
        Me.nmuLightValue3 = New System.Windows.Forms.NumericUpDown()
        Me.nmuLightValue2 = New System.Windows.Forms.NumericUpDown()
        Me.nmuLightValue1 = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblLightCh4 = New System.Windows.Forms.Label()
        Me.lblLightCh3 = New System.Windows.Forms.Label()
        Me.lblLightCh2 = New System.Windows.Forms.Label()
        Me.lblLightCh1 = New System.Windows.Forms.Label()
        Me.btnGetLight4 = New System.Windows.Forms.Button()
        Me.btnGetLight3 = New System.Windows.Forms.Button()
        Me.btnGetLight2 = New System.Windows.Forms.Button()
        Me.btnGetLight = New System.Windows.Forms.Button()
        Me.btnSetlight4 = New System.Windows.Forms.Button()
        Me.btnSetlight3 = New System.Windows.Forms.Button()
        Me.btnSetlight2 = New System.Windows.Forms.Button()
        Me.btnSetlight = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpLightController.SuspendLayout()
        CType(Me.nmuLightValue4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuLightValue3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuLightValue2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmuLightValue1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCh1
        '
        resources.ApplyResources(Me.lblCh1, "lblCh1")
        Me.lblCh1.BackColor = System.Drawing.Color.Transparent
        Me.lblCh1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCh1.Name = "lblCh1"
        Me.ToolTip1.SetToolTip(Me.lblCh1, resources.GetString("lblCh1.ToolTip"))
        '
        'lblCh2
        '
        resources.ApplyResources(Me.lblCh2, "lblCh2")
        Me.lblCh2.BackColor = System.Drawing.Color.Transparent
        Me.lblCh2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCh2.Name = "lblCh2"
        Me.ToolTip1.SetToolTip(Me.lblCh2, resources.GetString("lblCh2.ToolTip"))
        '
        'lblCh3
        '
        resources.ApplyResources(Me.lblCh3, "lblCh3")
        Me.lblCh3.BackColor = System.Drawing.Color.Transparent
        Me.lblCh3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCh3.Name = "lblCh3"
        Me.ToolTip1.SetToolTip(Me.lblCh3, resources.GetString("lblCh3.ToolTip"))
        '
        'lblCh4
        '
        resources.ApplyResources(Me.lblCh4, "lblCh4")
        Me.lblCh4.BackColor = System.Drawing.Color.Transparent
        Me.lblCh4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCh4.Name = "lblCh4"
        Me.ToolTip1.SetToolTip(Me.lblCh4, resources.GetString("lblCh4.ToolTip"))
        '
        'cmbController
        '
        resources.ApplyResources(Me.cmbController, "cmbController")
        Me.cmbController.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbController.FormattingEnabled = True
        Me.cmbController.Name = "cmbController"
        Me.ToolTip1.SetToolTip(Me.cmbController, resources.GetString("cmbController.ToolTip"))
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label6.Name = "Label6"
        Me.ToolTip1.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip"))
        '
        'grpLightController
        '
        resources.ApplyResources(Me.grpLightController, "grpLightController")
        Me.grpLightController.Controls.Add(Me.nmuLightValue4)
        Me.grpLightController.Controls.Add(Me.nmuLightValue3)
        Me.grpLightController.Controls.Add(Me.nmuLightValue2)
        Me.grpLightController.Controls.Add(Me.nmuLightValue1)
        Me.grpLightController.Controls.Add(Me.Label8)
        Me.grpLightController.Controls.Add(Me.Label7)
        Me.grpLightController.Controls.Add(Me.lblLightCh4)
        Me.grpLightController.Controls.Add(Me.lblLightCh3)
        Me.grpLightController.Controls.Add(Me.lblLightCh2)
        Me.grpLightController.Controls.Add(Me.lblLightCh1)
        Me.grpLightController.Controls.Add(Me.Label6)
        Me.grpLightController.Controls.Add(Me.btnGetLight4)
        Me.grpLightController.Controls.Add(Me.btnGetLight3)
        Me.grpLightController.Controls.Add(Me.btnGetLight2)
        Me.grpLightController.Controls.Add(Me.btnGetLight)
        Me.grpLightController.Controls.Add(Me.btnSetlight4)
        Me.grpLightController.Controls.Add(Me.btnSetlight3)
        Me.grpLightController.Controls.Add(Me.btnSetlight2)
        Me.grpLightController.Controls.Add(Me.btnSetlight)
        Me.grpLightController.Controls.Add(Me.cmbController)
        Me.grpLightController.Controls.Add(Me.lblCh4)
        Me.grpLightController.Controls.Add(Me.lblCh3)
        Me.grpLightController.Controls.Add(Me.lblCh2)
        Me.grpLightController.Controls.Add(Me.lblCh1)
        Me.grpLightController.Name = "grpLightController"
        Me.grpLightController.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpLightController, resources.GetString("grpLightController.ToolTip"))
        '
        'nmuLightValue4
        '
        resources.ApplyResources(Me.nmuLightValue4, "nmuLightValue4")
        Me.nmuLightValue4.Name = "nmuLightValue4"
        Me.ToolTip1.SetToolTip(Me.nmuLightValue4, resources.GetString("nmuLightValue4.ToolTip"))
        '
        'nmuLightValue3
        '
        resources.ApplyResources(Me.nmuLightValue3, "nmuLightValue3")
        Me.nmuLightValue3.Name = "nmuLightValue3"
        Me.ToolTip1.SetToolTip(Me.nmuLightValue3, resources.GetString("nmuLightValue3.ToolTip"))
        '
        'nmuLightValue2
        '
        resources.ApplyResources(Me.nmuLightValue2, "nmuLightValue2")
        Me.nmuLightValue2.Name = "nmuLightValue2"
        Me.ToolTip1.SetToolTip(Me.nmuLightValue2, resources.GetString("nmuLightValue2.ToolTip"))
        '
        'nmuLightValue1
        '
        resources.ApplyResources(Me.nmuLightValue1, "nmuLightValue1")
        Me.nmuLightValue1.Name = "nmuLightValue1"
        Me.ToolTip1.SetToolTip(Me.nmuLightValue1, resources.GetString("nmuLightValue1.ToolTip"))
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label8.Name = "Label8"
        Me.ToolTip1.SetToolTip(Me.Label8, resources.GetString("Label8.ToolTip"))
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label7.Name = "Label7"
        Me.ToolTip1.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip"))
        '
        'lblLightCh4
        '
        resources.ApplyResources(Me.lblLightCh4, "lblLightCh4")
        Me.lblLightCh4.BackColor = System.Drawing.Color.Transparent
        Me.lblLightCh4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLightCh4.Name = "lblLightCh4"
        Me.ToolTip1.SetToolTip(Me.lblLightCh4, resources.GetString("lblLightCh4.ToolTip"))
        '
        'lblLightCh3
        '
        resources.ApplyResources(Me.lblLightCh3, "lblLightCh3")
        Me.lblLightCh3.BackColor = System.Drawing.Color.Transparent
        Me.lblLightCh3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLightCh3.Name = "lblLightCh3"
        Me.ToolTip1.SetToolTip(Me.lblLightCh3, resources.GetString("lblLightCh3.ToolTip"))
        '
        'lblLightCh2
        '
        resources.ApplyResources(Me.lblLightCh2, "lblLightCh2")
        Me.lblLightCh2.BackColor = System.Drawing.Color.Transparent
        Me.lblLightCh2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLightCh2.Name = "lblLightCh2"
        Me.ToolTip1.SetToolTip(Me.lblLightCh2, resources.GetString("lblLightCh2.ToolTip"))
        '
        'lblLightCh1
        '
        resources.ApplyResources(Me.lblLightCh1, "lblLightCh1")
        Me.lblLightCh1.BackColor = System.Drawing.Color.Transparent
        Me.lblLightCh1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLightCh1.Name = "lblLightCh1"
        Me.ToolTip1.SetToolTip(Me.lblLightCh1, resources.GetString("lblLightCh1.ToolTip"))
        '
        'btnGetLight4
        '
        resources.ApplyResources(Me.btnGetLight4, "btnGetLight4")
        Me.btnGetLight4.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        Me.btnGetLight4.Name = "btnGetLight4"
        Me.ToolTip1.SetToolTip(Me.btnGetLight4, resources.GetString("btnGetLight4.ToolTip"))
        Me.btnGetLight4.UseVisualStyleBackColor = True
        '
        'btnGetLight3
        '
        resources.ApplyResources(Me.btnGetLight3, "btnGetLight3")
        Me.btnGetLight3.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        Me.btnGetLight3.Name = "btnGetLight3"
        Me.ToolTip1.SetToolTip(Me.btnGetLight3, resources.GetString("btnGetLight3.ToolTip"))
        Me.btnGetLight3.UseVisualStyleBackColor = True
        '
        'btnGetLight2
        '
        resources.ApplyResources(Me.btnGetLight2, "btnGetLight2")
        Me.btnGetLight2.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        Me.btnGetLight2.Name = "btnGetLight2"
        Me.ToolTip1.SetToolTip(Me.btnGetLight2, resources.GetString("btnGetLight2.ToolTip"))
        Me.btnGetLight2.UseVisualStyleBackColor = True
        '
        'btnGetLight
        '
        resources.ApplyResources(Me.btnGetLight, "btnGetLight")
        Me.btnGetLight.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.GetValue
        Me.btnGetLight.Name = "btnGetLight"
        Me.ToolTip1.SetToolTip(Me.btnGetLight, resources.GetString("btnGetLight.ToolTip"))
        Me.btnGetLight.UseVisualStyleBackColor = True
        '
        'btnSetlight4
        '
        resources.ApplyResources(Me.btnSetlight4, "btnSetlight4")
        Me.btnSetlight4.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetlight4.Name = "btnSetlight4"
        Me.ToolTip1.SetToolTip(Me.btnSetlight4, resources.GetString("btnSetlight4.ToolTip"))
        Me.btnSetlight4.UseVisualStyleBackColor = True
        '
        'btnSetlight3
        '
        resources.ApplyResources(Me.btnSetlight3, "btnSetlight3")
        Me.btnSetlight3.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetlight3.Name = "btnSetlight3"
        Me.ToolTip1.SetToolTip(Me.btnSetlight3, resources.GetString("btnSetlight3.ToolTip"))
        Me.btnSetlight3.UseVisualStyleBackColor = True
        '
        'btnSetlight2
        '
        resources.ApplyResources(Me.btnSetlight2, "btnSetlight2")
        Me.btnSetlight2.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetlight2.Name = "btnSetlight2"
        Me.ToolTip1.SetToolTip(Me.btnSetlight2, resources.GetString("btnSetlight2.ToolTip"))
        Me.btnSetlight2.UseVisualStyleBackColor = True
        '
        'btnSetlight
        '
        resources.ApplyResources(Me.btnSetlight, "btnSetlight")
        Me.btnSetlight.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        Me.btnSetlight.Name = "btnSetlight"
        Me.ToolTip1.SetToolTip(Me.btnSetlight, resources.GetString("btnSetlight.ToolTip"))
        Me.btnSetlight.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnOK.Name = "btnOK"
        Me.ToolTip1.SetToolTip(Me.btnOK, resources.GetString("btnOK.ToolTip"))
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmProgramControlLight
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CancelButton = Me.btnOK
        Me.ControlBox = False
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.grpLightController)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProgramControlLight"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpLightController.ResumeLayout(False)
        CType(Me.nmuLightValue4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuLightValue3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuLightValue2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmuLightValue1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSetlight As System.Windows.Forms.Button
    Friend WithEvents btnGetLight As System.Windows.Forms.Button
    Friend WithEvents lblCh1 As System.Windows.Forms.Label
    Friend WithEvents lblCh2 As System.Windows.Forms.Label
    Friend WithEvents lblCh3 As System.Windows.Forms.Label
    Friend WithEvents lblCh4 As System.Windows.Forms.Label
    Friend WithEvents cmbController As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents grpLightController As System.Windows.Forms.GroupBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblLightCh4 As System.Windows.Forms.Label
    Friend WithEvents lblLightCh3 As System.Windows.Forms.Label
    Friend WithEvents lblLightCh2 As System.Windows.Forms.Label
    Friend WithEvents lblLightCh1 As System.Windows.Forms.Label
    Friend WithEvents btnGetLight4 As System.Windows.Forms.Button
    Friend WithEvents btnGetLight3 As System.Windows.Forms.Button
    Friend WithEvents btnGetLight2 As System.Windows.Forms.Button
    Friend WithEvents btnSetlight4 As System.Windows.Forms.Button
    Friend WithEvents btnSetlight3 As System.Windows.Forms.Button
    Friend WithEvents btnSetlight2 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents nmuLightValue1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuLightValue4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuLightValue3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmuLightValue2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

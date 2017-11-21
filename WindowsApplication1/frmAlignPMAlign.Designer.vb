<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAlignPMAlign
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAlignPMAlign))
        Me.CogDisplay1 = New Cognex.VisionPro.Display.CogDisplay()
        Me.grSearchArea = New System.Windows.Forms.GroupBox()
        Me.nmcSearchAreaH = New System.Windows.Forms.NumericUpDown()
        Me.nmcSearchAreaW = New System.Windows.Forms.NumericUpDown()
        Me.lblSearchAreaXUnit = New System.Windows.Forms.Label()
        Me.rdbSearchAreaALL = New System.Windows.Forms.RadioButton()
        Me.lblSearchAreaX = New System.Windows.Forms.Label()
        Me.rdbSearchArea = New System.Windows.Forms.RadioButton()
        Me.lblSearchAreaYUnit = New System.Windows.Forms.Label()
        Me.lblSearchAreaY = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.CogDisplay1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grSearchArea.SuspendLayout()
        CType(Me.nmcSearchAreaH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcSearchAreaW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CogDisplay1
        '
        resources.ApplyResources(Me.CogDisplay1, "CogDisplay1")
        Me.CogDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black
        Me.CogDisplay1.ColorMapLowerRoiLimit = 0.0R
        Me.CogDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None
        Me.CogDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black
        Me.CogDisplay1.ColorMapUpperRoiLimit = 1.0R
        Me.CogDisplay1.DoubleTapZoomCycleLength = 2
        Me.CogDisplay1.DoubleTapZoomSensitivity = 2.5R
        Me.CogDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1
        Me.CogDisplay1.MouseWheelSensitivity = 1.0R
        Me.CogDisplay1.Name = "CogDisplay1"
        Me.CogDisplay1.OcxState = CType(resources.GetObject("CogDisplay1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.ToolTip1.SetToolTip(Me.CogDisplay1, resources.GetString("CogDisplay1.ToolTip"))
        '
        'grSearchArea
        '
        resources.ApplyResources(Me.grSearchArea, "grSearchArea")
        Me.grSearchArea.Controls.Add(Me.nmcSearchAreaH)
        Me.grSearchArea.Controls.Add(Me.nmcSearchAreaW)
        Me.grSearchArea.Controls.Add(Me.lblSearchAreaXUnit)
        Me.grSearchArea.Controls.Add(Me.rdbSearchAreaALL)
        Me.grSearchArea.Controls.Add(Me.lblSearchAreaX)
        Me.grSearchArea.Controls.Add(Me.rdbSearchArea)
        Me.grSearchArea.Controls.Add(Me.lblSearchAreaYUnit)
        Me.grSearchArea.Controls.Add(Me.lblSearchAreaY)
        Me.grSearchArea.Name = "grSearchArea"
        Me.grSearchArea.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grSearchArea, resources.GetString("grSearchArea.ToolTip"))
        '
        'nmcSearchAreaH
        '
        resources.ApplyResources(Me.nmcSearchAreaH, "nmcSearchAreaH")
        Me.nmcSearchAreaH.Cursor = System.Windows.Forms.Cursors.Default
        Me.nmcSearchAreaH.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nmcSearchAreaH.Name = "nmcSearchAreaH"
        Me.ToolTip1.SetToolTip(Me.nmcSearchAreaH, resources.GetString("nmcSearchAreaH.ToolTip"))
        Me.nmcSearchAreaH.Value = New Decimal(New Integer() {800, 0, 0, 0})
        '
        'nmcSearchAreaW
        '
        resources.ApplyResources(Me.nmcSearchAreaW, "nmcSearchAreaW")
        Me.nmcSearchAreaW.Cursor = System.Windows.Forms.Cursors.Default
        Me.nmcSearchAreaW.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nmcSearchAreaW.Name = "nmcSearchAreaW"
        Me.ToolTip1.SetToolTip(Me.nmcSearchAreaW, resources.GetString("nmcSearchAreaW.ToolTip"))
        Me.nmcSearchAreaW.Value = New Decimal(New Integer() {800, 0, 0, 0})
        '
        'lblSearchAreaXUnit
        '
        resources.ApplyResources(Me.lblSearchAreaXUnit, "lblSearchAreaXUnit")
        Me.lblSearchAreaXUnit.Name = "lblSearchAreaXUnit"
        Me.ToolTip1.SetToolTip(Me.lblSearchAreaXUnit, resources.GetString("lblSearchAreaXUnit.ToolTip"))
        '
        'rdbSearchAreaALL
        '
        resources.ApplyResources(Me.rdbSearchAreaALL, "rdbSearchAreaALL")
        Me.rdbSearchAreaALL.Checked = True
        Me.rdbSearchAreaALL.Name = "rdbSearchAreaALL"
        Me.rdbSearchAreaALL.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rdbSearchAreaALL, resources.GetString("rdbSearchAreaALL.ToolTip"))
        Me.rdbSearchAreaALL.UseVisualStyleBackColor = True
        '
        'lblSearchAreaX
        '
        resources.ApplyResources(Me.lblSearchAreaX, "lblSearchAreaX")
        Me.lblSearchAreaX.Name = "lblSearchAreaX"
        Me.ToolTip1.SetToolTip(Me.lblSearchAreaX, resources.GetString("lblSearchAreaX.ToolTip"))
        '
        'rdbSearchArea
        '
        resources.ApplyResources(Me.rdbSearchArea, "rdbSearchArea")
        Me.rdbSearchArea.Name = "rdbSearchArea"
        Me.rdbSearchArea.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rdbSearchArea, resources.GetString("rdbSearchArea.ToolTip"))
        Me.rdbSearchArea.UseVisualStyleBackColor = True
        '
        'lblSearchAreaYUnit
        '
        resources.ApplyResources(Me.lblSearchAreaYUnit, "lblSearchAreaYUnit")
        Me.lblSearchAreaYUnit.Name = "lblSearchAreaYUnit"
        Me.ToolTip1.SetToolTip(Me.lblSearchAreaYUnit, resources.GetString("lblSearchAreaYUnit.ToolTip"))
        '
        'lblSearchAreaY
        '
        resources.ApplyResources(Me.lblSearchAreaY, "lblSearchAreaY")
        Me.lblSearchAreaY.Name = "lblSearchAreaY"
        Me.ToolTip1.SetToolTip(Me.lblSearchAreaY, resources.GetString("lblSearchAreaY.ToolTip"))
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
        'frmAlignPMAlign
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.CogDisplay1)
        Me.Controls.Add(Me.grSearchArea)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.MaximizeBox = False
        Me.Name = "frmAlignPMAlign"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        CType(Me.CogDisplay1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grSearchArea.ResumeLayout(False)
        Me.grSearchArea.PerformLayout()
        CType(Me.nmcSearchAreaH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcSearchAreaW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CogDisplay1 As Cognex.VisionPro.Display.CogDisplay
    Friend WithEvents grSearchArea As System.Windows.Forms.GroupBox
    Friend WithEvents nmcSearchAreaH As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcSearchAreaW As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblSearchAreaXUnit As System.Windows.Forms.Label
    Friend WithEvents rdbSearchAreaALL As System.Windows.Forms.RadioButton
    Friend WithEvents lblSearchAreaX As System.Windows.Forms.Label
    Friend WithEvents rdbSearchArea As System.Windows.Forms.RadioButton
    Friend WithEvents lblSearchAreaYUnit As System.Windows.Forms.Label
    Friend WithEvents lblSearchAreaY As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

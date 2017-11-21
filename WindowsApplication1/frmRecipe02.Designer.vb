<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRecipe02
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRecipe02))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnLevelAdd = New System.Windows.Forms.Button()
        Me.btnLevelDelete = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.cmbLevel = New System.Windows.Forms.ComboBox()
        Me.grpLevel = New System.Windows.Forms.GroupBox()
        Me.nmcStartLocY = New System.Windows.Forms.NumericUpDown()
        Me.nmcStartLocX = New System.Windows.Forms.NumericUpDown()
        Me.lblStartLocX = New System.Windows.Forms.Label()
        Me.lblStartLocY = New System.Windows.Forms.Label()
        Me.UcLevelNonArray1 = New WindowsApplication1.ucLevelNonArray()
        Me.UcLevelArray1 = New WindowsApplication1.ucLevelArray()
        Me.btnNonArray = New System.Windows.Forms.Button()
        Me.btnArray = New System.Windows.Forms.Button()
        Me.grpMAP = New System.Windows.Forms.GroupBox()
        Me.picMap = New System.Windows.Forms.PictureBox()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.UcLightControl1 = New WindowsApplication1.ucLightControl()
        Me.ucJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.btnMultiArryMove = New System.Windows.Forms.Button()
        Me.btnWaferMapFilter = New System.Windows.Forms.Button()
        Me.grpIndex = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbArrayY = New System.Windows.Forms.ComboBox()
        Me.cmbArrayX = New System.Windows.Forms.ComboBox()
        Me.grpLevel.SuspendLayout()
        CType(Me.nmcStartLocY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcStartLocX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMAP.SuspendLayout()
        CType(Me.picMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpIndex.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnLevelAdd
        '
        Me.btnLevelAdd.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnLevelAdd, "btnLevelAdd")
        Me.btnLevelAdd.FlatAppearance.BorderSize = 0
        Me.btnLevelAdd.Name = "btnLevelAdd"
        Me.ToolTip1.SetToolTip(Me.btnLevelAdd, resources.GetString("btnLevelAdd.ToolTip"))
        Me.btnLevelAdd.UseVisualStyleBackColor = True
        '
        'btnLevelDelete
        '
        Me.btnLevelDelete.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnLevelDelete, "btnLevelDelete")
        Me.btnLevelDelete.FlatAppearance.BorderSize = 0
        Me.btnLevelDelete.Name = "btnLevelDelete"
        Me.ToolTip1.SetToolTip(Me.btnLevelDelete, resources.GetString("btnLevelDelete.ToolTip"))
        Me.btnLevelDelete.UseVisualStyleBackColor = True
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
        'cmbLevel
        '
        Me.cmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cmbLevel, "cmbLevel")
        Me.cmbLevel.FormattingEnabled = True
        Me.cmbLevel.Name = "cmbLevel"
        '
        'grpLevel
        '
        Me.grpLevel.Controls.Add(Me.nmcStartLocY)
        Me.grpLevel.Controls.Add(Me.nmcStartLocX)
        Me.grpLevel.Controls.Add(Me.lblStartLocX)
        Me.grpLevel.Controls.Add(Me.lblStartLocY)
        Me.grpLevel.Controls.Add(Me.UcLevelNonArray1)
        Me.grpLevel.Controls.Add(Me.UcLevelArray1)
        Me.grpLevel.Controls.Add(Me.btnNonArray)
        Me.grpLevel.Controls.Add(Me.btnArray)
        Me.grpLevel.Controls.Add(Me.btnLevelAdd)
        Me.grpLevel.Controls.Add(Me.cmbLevel)
        Me.grpLevel.Controls.Add(Me.btnLevelDelete)
        resources.ApplyResources(Me.grpLevel, "grpLevel")
        Me.grpLevel.Name = "grpLevel"
        Me.grpLevel.TabStop = False
        '
        'nmcStartLocY
        '
        resources.ApplyResources(Me.nmcStartLocY, "nmcStartLocY")
        Me.nmcStartLocY.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcStartLocY.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcStartLocY.Name = "nmcStartLocY"
        Me.nmcStartLocY.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmcStartLocX
        '
        resources.ApplyResources(Me.nmcStartLocX, "nmcStartLocX")
        Me.nmcStartLocX.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nmcStartLocX.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcStartLocX.Name = "nmcStartLocX"
        Me.nmcStartLocX.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblStartLocX
        '
        resources.ApplyResources(Me.lblStartLocX, "lblStartLocX")
        Me.lblStartLocX.Name = "lblStartLocX"
        '
        'lblStartLocY
        '
        resources.ApplyResources(Me.lblStartLocY, "lblStartLocY")
        Me.lblStartLocY.Name = "lblStartLocY"
        '
        'UcLevelNonArray1
        '
        Me.UcLevelNonArray1.LevelNo = 0
        resources.ApplyResources(Me.UcLevelNonArray1, "UcLevelNonArray1")
        Me.UcLevelNonArray1.Name = "UcLevelNonArray1"
        '
        'UcLevelArray1
        '
        Me.UcLevelArray1.LevelNo = 0
        resources.ApplyResources(Me.UcLevelArray1, "UcLevelArray1")
        Me.UcLevelArray1.Name = "UcLevelArray1"
        '
        'btnNonArray
        '
        resources.ApplyResources(Me.btnNonArray, "btnNonArray")
        Me.btnNonArray.Name = "btnNonArray"
        Me.btnNonArray.UseVisualStyleBackColor = True
        '
        'btnArray
        '
        resources.ApplyResources(Me.btnArray, "btnArray")
        Me.btnArray.Name = "btnArray"
        Me.btnArray.UseVisualStyleBackColor = True
        '
        'grpMAP
        '
        Me.grpMAP.Controls.Add(Me.picMap)
        resources.ApplyResources(Me.grpMAP, "grpMAP")
        Me.grpMAP.Name = "grpMAP"
        Me.grpMAP.TabStop = False
        '
        'picMap
        '
        Me.picMap.BackColor = System.Drawing.SystemColors.ControlLight
        resources.ApplyResources(Me.picMap, "picMap")
        Me.picMap.Name = "picMap"
        Me.picMap.TabStop = False
        '
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        '
        'UcLightControl1
        '
        Me.UcLightControl1.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me.UcLightControl1, "UcLightControl1")
        Me.UcLightControl1.Name = "UcLightControl1"
        '
        'ucJoyStick1
        '
        Me.ucJoyStick1.AXisA = 0
        Me.ucJoyStick1.AXisB = 0
        Me.ucJoyStick1.AXisC = 0
        Me.ucJoyStick1.AxisX = 0
        Me.ucJoyStick1.AxisY = 0
        Me.ucJoyStick1.AxisZ = 0
        Me.ucJoyStick1.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me.ucJoyStick1, "ucJoyStick1")
        Me.ucJoyStick1.Name = "ucJoyStick1"
        '
        'UcDisplay1
        '
        resources.ApplyResources(Me.UcDisplay1, "UcDisplay1")
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UcDisplay1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.UcDisplay1.Name = "UcDisplay1"
        '
        'btnMultiArryMove
        '
        resources.ApplyResources(Me.btnMultiArryMove, "btnMultiArryMove")
        Me.btnMultiArryMove.Name = "btnMultiArryMove"
        Me.btnMultiArryMove.UseVisualStyleBackColor = True
        '
        'btnWaferMapFilter
        '
        resources.ApplyResources(Me.btnWaferMapFilter, "btnWaferMapFilter")
        Me.btnWaferMapFilter.Name = "btnWaferMapFilter"
        Me.btnWaferMapFilter.UseVisualStyleBackColor = True
        '
        'grpIndex
        '
        Me.grpIndex.Controls.Add(Me.Label2)
        Me.grpIndex.Controls.Add(Me.Label1)
        Me.grpIndex.Controls.Add(Me.cmbArrayY)
        Me.grpIndex.Controls.Add(Me.cmbArrayX)
        resources.ApplyResources(Me.grpIndex, "grpIndex")
        Me.grpIndex.Name = "grpIndex"
        Me.grpIndex.TabStop = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'cmbArrayY
        '
        Me.cmbArrayY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cmbArrayY, "cmbArrayY")
        Me.cmbArrayY.FormattingEnabled = True
        Me.cmbArrayY.Items.AddRange(New Object() {resources.GetString("cmbArrayY.Items")})
        Me.cmbArrayY.Name = "cmbArrayY"
        '
        'cmbArrayX
        '
        Me.cmbArrayX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cmbArrayX, "cmbArrayX")
        Me.cmbArrayX.FormattingEnabled = True
        Me.cmbArrayX.Items.AddRange(New Object() {resources.GetString("cmbArrayX.Items")})
        Me.cmbArrayX.Name = "cmbArrayX"
        '
        'frmRecipe02
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.grpIndex)
        Me.Controls.Add(Me.btnWaferMapFilter)
        Me.Controls.Add(Me.btnMultiArryMove)
        Me.Controls.Add(Me.grpMAP)
        Me.Controls.Add(Me.grpLevel)
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.UcLightControl1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.ucJoyStick1)
        Me.Controls.Add(Me.UcDisplay1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRecipe02"
        Me.grpLevel.ResumeLayout(False)
        CType(Me.nmcStartLocY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcStartLocX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMAP.ResumeLayout(False)
        CType(Me.picMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpIndex.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ucJoyStick1 As ucJoyStick
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents UcLightControl1 As ucLightControl
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents cmbLevel As System.Windows.Forms.ComboBox
    Friend WithEvents btnLevelAdd As System.Windows.Forms.Button
    Friend WithEvents btnLevelDelete As System.Windows.Forms.Button
    Friend WithEvents grpLevel As System.Windows.Forms.GroupBox
    Friend WithEvents btnNonArray As System.Windows.Forms.Button
    Friend WithEvents btnArray As System.Windows.Forms.Button
    Friend WithEvents grpMAP As System.Windows.Forms.GroupBox
    Friend WithEvents picMap As System.Windows.Forms.PictureBox
    Friend WithEvents UcLevelNonArray1 As WindowsApplication1.ucLevelNonArray
    Friend WithEvents UcLevelArray1 As WindowsApplication1.ucLevelArray
    Friend WithEvents nmcStartLocY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcStartLocX As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblStartLocX As System.Windows.Forms.Label
    Friend WithEvents lblStartLocY As System.Windows.Forms.Label
    Friend WithEvents btnMultiArryMove As System.Windows.Forms.Button
    Friend WithEvents btnWaferMapFilter As System.Windows.Forms.Button
    Friend WithEvents grpIndex As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbArrayY As System.Windows.Forms.ComboBox
    Friend WithEvents cmbArrayX As System.Windows.Forms.ComboBox
End Class

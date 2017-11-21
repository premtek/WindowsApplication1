<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMapping
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMapping))
        Me.btnNewMapping = New System.Windows.Forms.Button()
        Me.btnOpenMapping = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dieDataSave = New System.Windows.Forms.Button()
        Me.txtCycle = New System.Windows.Forms.TextBox()
        Me.txtBin = New System.Windows.Forms.TextBox()
        Me.lblCycle = New System.Windows.Forms.Label()
        Me.lblBin = New System.Windows.Forms.Label()
        Me.btnSavePIIMap = New System.Windows.Forms.Button()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblLotID = New System.Windows.Forms.Label()
        Me.lblProduct = New System.Windows.Forms.Label()
        Me.lblDieSizeX = New System.Windows.Forms.Label()
        Me.lblDieSizeY = New System.Windows.Forms.Label()
        Me.lblRecipe = New System.Windows.Forms.Label()
        Me.Group_Data = New System.Windows.Forms.GroupBox()
        Me.CblNotch = New System.Windows.Forms.ComboBox()
        Me.txtMachineType = New System.Windows.Forms.TextBox()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.txtPitch = New System.Windows.Forms.TextBox()
        Me.txtType = New System.Windows.Forms.TextBox()
        Me.txtRecipe = New System.Windows.Forms.TextBox()
        Me.txtDieSizeY = New System.Windows.Forms.TextBox()
        Me.txtDieSizeX = New System.Windows.Forms.TextBox()
        Me.txtProduct = New System.Windows.Forms.TextBox()
        Me.txtLotID = New System.Windows.Forms.TextBox()
        Me.txtVersion = New System.Windows.Forms.TextBox()
        Me.lblNotch = New System.Windows.Forms.Label()
        Me.lblMachineType = New System.Windows.Forms.Label()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.lblPitch = New System.Windows.Forms.Label()
        Me.lblType = New System.Windows.Forms.Label()
        Me.piboxWaferMap = New System.Windows.Forms.PictureBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblBinNotch = New System.Windows.Forms.Label()
        Me.lblFail = New System.Windows.Forms.Label()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.Group_Data.SuspendLayout()
        CType(Me.piboxWaferMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnNewMapping
        '
        resources.ApplyResources(Me.btnNewMapping, "btnNewMapping")
        Me.btnNewMapping.Name = "btnNewMapping"
        Me.ToolTip1.SetToolTip(Me.btnNewMapping, resources.GetString("btnNewMapping.ToolTip"))
        Me.btnNewMapping.UseVisualStyleBackColor = True
        '
        'btnOpenMapping
        '
        resources.ApplyResources(Me.btnOpenMapping, "btnOpenMapping")
        Me.btnOpenMapping.Name = "btnOpenMapping"
        Me.ToolTip1.SetToolTip(Me.btnOpenMapping, resources.GetString("btnOpenMapping.ToolTip"))
        Me.btnOpenMapping.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.dieDataSave)
        Me.GroupBox1.Controls.Add(Me.txtCycle)
        Me.GroupBox1.Controls.Add(Me.txtBin)
        Me.GroupBox1.Controls.Add(Me.lblCycle)
        Me.GroupBox1.Controls.Add(Me.lblBin)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'dieDataSave
        '
        resources.ApplyResources(Me.dieDataSave, "dieDataSave")
        Me.dieDataSave.Name = "dieDataSave"
        Me.ToolTip1.SetToolTip(Me.dieDataSave, resources.GetString("dieDataSave.ToolTip"))
        Me.dieDataSave.UseVisualStyleBackColor = True
        '
        'txtCycle
        '
        resources.ApplyResources(Me.txtCycle, "txtCycle")
        Me.txtCycle.Name = "txtCycle"
        Me.ToolTip1.SetToolTip(Me.txtCycle, resources.GetString("txtCycle.ToolTip"))
        '
        'txtBin
        '
        resources.ApplyResources(Me.txtBin, "txtBin")
        Me.txtBin.Name = "txtBin"
        Me.ToolTip1.SetToolTip(Me.txtBin, resources.GetString("txtBin.ToolTip"))
        '
        'lblCycle
        '
        resources.ApplyResources(Me.lblCycle, "lblCycle")
        Me.lblCycle.Name = "lblCycle"
        Me.ToolTip1.SetToolTip(Me.lblCycle, resources.GetString("lblCycle.ToolTip"))
        '
        'lblBin
        '
        resources.ApplyResources(Me.lblBin, "lblBin")
        Me.lblBin.Name = "lblBin"
        Me.ToolTip1.SetToolTip(Me.lblBin, resources.GetString("lblBin.ToolTip"))
        '
        'btnSavePIIMap
        '
        resources.ApplyResources(Me.btnSavePIIMap, "btnSavePIIMap")
        Me.btnSavePIIMap.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnSavePIIMap.Name = "btnSavePIIMap"
        Me.ToolTip1.SetToolTip(Me.btnSavePIIMap, resources.GetString("btnSavePIIMap.ToolTip"))
        Me.btnSavePIIMap.UseVisualStyleBackColor = True
        '
        'lblVersion
        '
        resources.ApplyResources(Me.lblVersion, "lblVersion")
        Me.lblVersion.Name = "lblVersion"
        Me.ToolTip1.SetToolTip(Me.lblVersion, resources.GetString("lblVersion.ToolTip"))
        '
        'lblLotID
        '
        resources.ApplyResources(Me.lblLotID, "lblLotID")
        Me.lblLotID.Name = "lblLotID"
        Me.ToolTip1.SetToolTip(Me.lblLotID, resources.GetString("lblLotID.ToolTip"))
        '
        'lblProduct
        '
        resources.ApplyResources(Me.lblProduct, "lblProduct")
        Me.lblProduct.Name = "lblProduct"
        Me.ToolTip1.SetToolTip(Me.lblProduct, resources.GetString("lblProduct.ToolTip"))
        '
        'lblDieSizeX
        '
        resources.ApplyResources(Me.lblDieSizeX, "lblDieSizeX")
        Me.lblDieSizeX.Name = "lblDieSizeX"
        Me.ToolTip1.SetToolTip(Me.lblDieSizeX, resources.GetString("lblDieSizeX.ToolTip"))
        '
        'lblDieSizeY
        '
        resources.ApplyResources(Me.lblDieSizeY, "lblDieSizeY")
        Me.lblDieSizeY.Name = "lblDieSizeY"
        Me.ToolTip1.SetToolTip(Me.lblDieSizeY, resources.GetString("lblDieSizeY.ToolTip"))
        '
        'lblRecipe
        '
        resources.ApplyResources(Me.lblRecipe, "lblRecipe")
        Me.lblRecipe.Name = "lblRecipe"
        Me.ToolTip1.SetToolTip(Me.lblRecipe, resources.GetString("lblRecipe.ToolTip"))
        '
        'Group_Data
        '
        resources.ApplyResources(Me.Group_Data, "Group_Data")
        Me.Group_Data.Controls.Add(Me.CblNotch)
        Me.Group_Data.Controls.Add(Me.txtMachineType)
        Me.Group_Data.Controls.Add(Me.txtUser)
        Me.Group_Data.Controls.Add(Me.txtPitch)
        Me.Group_Data.Controls.Add(Me.txtType)
        Me.Group_Data.Controls.Add(Me.txtRecipe)
        Me.Group_Data.Controls.Add(Me.txtDieSizeY)
        Me.Group_Data.Controls.Add(Me.txtDieSizeX)
        Me.Group_Data.Controls.Add(Me.txtProduct)
        Me.Group_Data.Controls.Add(Me.txtLotID)
        Me.Group_Data.Controls.Add(Me.txtVersion)
        Me.Group_Data.Controls.Add(Me.lblNotch)
        Me.Group_Data.Controls.Add(Me.lblMachineType)
        Me.Group_Data.Controls.Add(Me.lblUser)
        Me.Group_Data.Controls.Add(Me.lblPitch)
        Me.Group_Data.Controls.Add(Me.lblType)
        Me.Group_Data.Controls.Add(Me.lblRecipe)
        Me.Group_Data.Controls.Add(Me.lblDieSizeY)
        Me.Group_Data.Controls.Add(Me.lblDieSizeX)
        Me.Group_Data.Controls.Add(Me.lblProduct)
        Me.Group_Data.Controls.Add(Me.lblLotID)
        Me.Group_Data.Controls.Add(Me.lblVersion)
        Me.Group_Data.Name = "Group_Data"
        Me.Group_Data.TabStop = False
        Me.ToolTip1.SetToolTip(Me.Group_Data, resources.GetString("Group_Data.ToolTip"))
        '
        'CblNotch
        '
        resources.ApplyResources(Me.CblNotch, "CblNotch")
        Me.CblNotch.FormattingEnabled = True
        Me.CblNotch.Items.AddRange(New Object() {resources.GetString("CblNotch.Items"), resources.GetString("CblNotch.Items1"), resources.GetString("CblNotch.Items2"), resources.GetString("CblNotch.Items3")})
        Me.CblNotch.Name = "CblNotch"
        Me.ToolTip1.SetToolTip(Me.CblNotch, resources.GetString("CblNotch.ToolTip"))
        '
        'txtMachineType
        '
        resources.ApplyResources(Me.txtMachineType, "txtMachineType")
        Me.txtMachineType.Name = "txtMachineType"
        Me.ToolTip1.SetToolTip(Me.txtMachineType, resources.GetString("txtMachineType.ToolTip"))
        '
        'txtUser
        '
        resources.ApplyResources(Me.txtUser, "txtUser")
        Me.txtUser.Name = "txtUser"
        Me.ToolTip1.SetToolTip(Me.txtUser, resources.GetString("txtUser.ToolTip"))
        '
        'txtPitch
        '
        resources.ApplyResources(Me.txtPitch, "txtPitch")
        Me.txtPitch.Name = "txtPitch"
        Me.ToolTip1.SetToolTip(Me.txtPitch, resources.GetString("txtPitch.ToolTip"))
        '
        'txtType
        '
        resources.ApplyResources(Me.txtType, "txtType")
        Me.txtType.Name = "txtType"
        Me.ToolTip1.SetToolTip(Me.txtType, resources.GetString("txtType.ToolTip"))
        '
        'txtRecipe
        '
        resources.ApplyResources(Me.txtRecipe, "txtRecipe")
        Me.txtRecipe.Name = "txtRecipe"
        Me.ToolTip1.SetToolTip(Me.txtRecipe, resources.GetString("txtRecipe.ToolTip"))
        '
        'txtDieSizeY
        '
        resources.ApplyResources(Me.txtDieSizeY, "txtDieSizeY")
        Me.txtDieSizeY.Name = "txtDieSizeY"
        Me.ToolTip1.SetToolTip(Me.txtDieSizeY, resources.GetString("txtDieSizeY.ToolTip"))
        '
        'txtDieSizeX
        '
        resources.ApplyResources(Me.txtDieSizeX, "txtDieSizeX")
        Me.txtDieSizeX.Name = "txtDieSizeX"
        Me.ToolTip1.SetToolTip(Me.txtDieSizeX, resources.GetString("txtDieSizeX.ToolTip"))
        '
        'txtProduct
        '
        resources.ApplyResources(Me.txtProduct, "txtProduct")
        Me.txtProduct.Name = "txtProduct"
        Me.ToolTip1.SetToolTip(Me.txtProduct, resources.GetString("txtProduct.ToolTip"))
        '
        'txtLotID
        '
        resources.ApplyResources(Me.txtLotID, "txtLotID")
        Me.txtLotID.Name = "txtLotID"
        Me.ToolTip1.SetToolTip(Me.txtLotID, resources.GetString("txtLotID.ToolTip"))
        '
        'txtVersion
        '
        resources.ApplyResources(Me.txtVersion, "txtVersion")
        Me.txtVersion.Name = "txtVersion"
        Me.ToolTip1.SetToolTip(Me.txtVersion, resources.GetString("txtVersion.ToolTip"))
        '
        'lblNotch
        '
        resources.ApplyResources(Me.lblNotch, "lblNotch")
        Me.lblNotch.Name = "lblNotch"
        Me.ToolTip1.SetToolTip(Me.lblNotch, resources.GetString("lblNotch.ToolTip"))
        '
        'lblMachineType
        '
        resources.ApplyResources(Me.lblMachineType, "lblMachineType")
        Me.lblMachineType.Name = "lblMachineType"
        Me.ToolTip1.SetToolTip(Me.lblMachineType, resources.GetString("lblMachineType.ToolTip"))
        '
        'lblUser
        '
        resources.ApplyResources(Me.lblUser, "lblUser")
        Me.lblUser.Name = "lblUser"
        Me.ToolTip1.SetToolTip(Me.lblUser, resources.GetString("lblUser.ToolTip"))
        '
        'lblPitch
        '
        resources.ApplyResources(Me.lblPitch, "lblPitch")
        Me.lblPitch.Name = "lblPitch"
        Me.ToolTip1.SetToolTip(Me.lblPitch, resources.GetString("lblPitch.ToolTip"))
        '
        'lblType
        '
        resources.ApplyResources(Me.lblType, "lblType")
        Me.lblType.Name = "lblType"
        Me.ToolTip1.SetToolTip(Me.lblType, resources.GetString("lblType.ToolTip"))
        '
        'piboxWaferMap
        '
        resources.ApplyResources(Me.piboxWaferMap, "piboxWaferMap")
        Me.piboxWaferMap.Name = "piboxWaferMap"
        Me.piboxWaferMap.TabStop = False
        Me.ToolTip1.SetToolTip(Me.piboxWaferMap, resources.GetString("piboxWaferMap.ToolTip"))
        '
        'SaveFileDialog1
        '
        resources.ApplyResources(Me.SaveFileDialog1, "SaveFileDialog1")
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        resources.ApplyResources(Me.OpenFileDialog1, "OpenFileDialog1")
        '
        'GroupBox2
        '
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Controls.Add(Me.PictureBox2)
        Me.GroupBox2.Controls.Add(Me.PictureBox1)
        Me.GroupBox2.Controls.Add(Me.lblBinNotch)
        Me.GroupBox2.Controls.Add(Me.lblFail)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox2, resources.GetString("GroupBox2.ToolTip"))
        '
        'PictureBox2
        '
        resources.ApplyResources(Me.PictureBox2, "PictureBox2")
        Me.PictureBox2.BackColor = System.Drawing.Color.DarkGray
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox2, resources.GetString("PictureBox2.ToolTip"))
        '
        'PictureBox1
        '
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.BackColor = System.Drawing.Color.Red
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox1, resources.GetString("PictureBox1.ToolTip"))
        '
        'lblBinNotch
        '
        resources.ApplyResources(Me.lblBinNotch, "lblBinNotch")
        Me.lblBinNotch.Name = "lblBinNotch"
        Me.ToolTip1.SetToolTip(Me.lblBinNotch, resources.GetString("lblBinNotch.ToolTip"))
        '
        'lblFail
        '
        resources.ApplyResources(Me.lblFail, "lblFail")
        Me.lblFail.Name = "lblFail"
        Me.ToolTip1.SetToolTip(Me.lblFail, resources.GetString("lblFail.ToolTip"))
        '
        'btn_Cancel
        '
        resources.ApplyResources(Me.btn_Cancel, "btn_Cancel")
        Me.btn_Cancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.ToolTip1.SetToolTip(Me.btn_Cancel, resources.GetString("btn_Cancel.ToolTip"))
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'frmMapping
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.piboxWaferMap)
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.btnSavePIIMap)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Group_Data)
        Me.Controls.Add(Me.btnOpenMapping)
        Me.Controls.Add(Me.btnNewMapping)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMapping"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Group_Data.ResumeLayout(False)
        Me.Group_Data.PerformLayout()
        CType(Me.piboxWaferMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnNewMapping As System.Windows.Forms.Button
    Friend WithEvents btnOpenMapping As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtCycle As System.Windows.Forms.TextBox
    Friend WithEvents txtBin As System.Windows.Forms.TextBox
    Friend WithEvents lblCycle As System.Windows.Forms.Label
    Friend WithEvents lblBin As System.Windows.Forms.Label
    Friend WithEvents btnSavePIIMap As System.Windows.Forms.Button
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblLotID As System.Windows.Forms.Label
    Friend WithEvents lblProduct As System.Windows.Forms.Label
    Friend WithEvents lblDieSizeX As System.Windows.Forms.Label
    Friend WithEvents lblDieSizeY As System.Windows.Forms.Label
    Friend WithEvents lblRecipe As System.Windows.Forms.Label
    Friend WithEvents Group_Data As System.Windows.Forms.GroupBox
    Friend WithEvents lblNotch As System.Windows.Forms.Label
    Friend WithEvents lblMachineType As System.Windows.Forms.Label
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents lblPitch As System.Windows.Forms.Label
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents piboxWaferMap As System.Windows.Forms.PictureBox
    Friend WithEvents dieDataSave As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents txtType As System.Windows.Forms.TextBox
    Friend WithEvents txtRecipe As System.Windows.Forms.TextBox
    Friend WithEvents txtDieSizeY As System.Windows.Forms.TextBox
    Friend WithEvents txtDieSizeX As System.Windows.Forms.TextBox
    Friend WithEvents txtProduct As System.Windows.Forms.TextBox
    Friend WithEvents txtLotID As System.Windows.Forms.TextBox
    Friend WithEvents txtVersion As System.Windows.Forms.TextBox
    Friend WithEvents CblNotch As System.Windows.Forms.ComboBox
    Friend WithEvents txtMachineType As System.Windows.Forms.TextBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents txtPitch As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblBinNotch As System.Windows.Forms.Label
    Friend WithEvents lblFail As System.Windows.Forms.Label
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRecipe03
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRecipe03))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtDispenseBasicZvalve2 = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.btnSetZPos = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtDispenseBasicZ = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAutoZero = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPosZ = New System.Windows.Forms.TextBox()
        Me.lblReaderPosZ = New System.Windows.Forms.Label()
        Me.txtPosY = New System.Windows.Forms.TextBox()
        Me.txtPosX = New System.Windows.Forms.TextBox()
        Me.lblReaderPosY = New System.Windows.Forms.Label()
        Me.lblReaderPosX = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCCDPosZ = New System.Windows.Forms.TextBox()
        Me.txtCCDPosY = New System.Windows.Forms.TextBox()
        Me.txtCCDPosX = New System.Windows.Forms.TextBox()
        Me.btnSetPos = New System.Windows.Forms.Button()
        Me.lblCCDPosZ = New System.Windows.Forms.Label()
        Me.lblCCDPosY = New System.Windows.Forms.Label()
        Me.lblCCDPosX = New System.Windows.Forms.Label()
        Me.btnGoCCDPos = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnSetPos_2 = New System.Windows.Forms.Button()
        Me.btnGoCCDPos_2 = New System.Windows.Forms.Button()
        Me.btnAutoZero_2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnSetPos_NonArray = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSetPos_NonArray_2 = New System.Windows.Forms.Button()
        Me.btnAdd_2 = New System.Windows.Forms.Button()
        Me.btnDelete_2 = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboB = New System.Windows.Forms.ComboBox()
        Me.TabConveyorControl_CCD_Laser = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtDispenseBasicZValve2_2 = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.btnSetZPos_2 = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtDispenseBasicZ_2 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtPosZ_2 = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtPosY_2 = New System.Windows.Forms.TextBox()
        Me.txtPosX_2 = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCCDPosZ_2 = New System.Windows.Forms.TextBox()
        Me.txtCCDPosY_2 = New System.Windows.Forms.TextBox()
        Me.txtCCDPosX_2 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.chkLaserReaderEnable = New System.Windows.Forms.CheckBox()
        Me.TabConveyorControl_CCD_Laser_NonArray = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.NonArrayGroup = New System.Windows.Forms.GroupBox()
        Me.lstNonArray = New System.Windows.Forms.ListBox()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.NonArrayGroup2 = New System.Windows.Forms.GroupBox()
        Me.lstNonArray_2 = New System.Windows.Forms.ListBox()
        Me.ucJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.UcLightControl1 = New WindowsApplication1.ucLightControl()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabConveyorControl_CCD_Laser.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabConveyorControl_CCD_Laser_NonArray.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.NonArrayGroup.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.NonArrayGroup2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.txtDispenseBasicZvalve2)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.btnSetZPos)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtDispenseBasicZ)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnAutoZero)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtPosZ)
        Me.GroupBox1.Controls.Add(Me.lblReaderPosZ)
        Me.GroupBox1.Controls.Add(Me.txtPosY)
        Me.GroupBox1.Controls.Add(Me.txtPosX)
        Me.GroupBox1.Controls.Add(Me.lblReaderPosY)
        Me.GroupBox1.Controls.Add(Me.lblReaderPosX)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label25, "Label25")
        Me.Label25.Name = "Label25"
        '
        'txtDispenseBasicZvalve2
        '
        Me.txtDispenseBasicZvalve2.BackColor = System.Drawing.SystemColors.Control
        Me.txtDispenseBasicZvalve2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtDispenseBasicZvalve2, "txtDispenseBasicZvalve2")
        Me.txtDispenseBasicZvalve2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtDispenseBasicZvalve2.Name = "txtDispenseBasicZvalve2"
        Me.txtDispenseBasicZvalve2.ReadOnly = True
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label24, "Label24")
        Me.Label24.Name = "Label24"
        '
        'btnSetZPos
        '
        resources.ApplyResources(Me.btnSetZPos, "btnSetZPos")
        Me.btnSetZPos.Name = "btnSetZPos"
        Me.btnSetZPos.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'txtDispenseBasicZ
        '
        Me.txtDispenseBasicZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtDispenseBasicZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtDispenseBasicZ, "txtDispenseBasicZ")
        Me.txtDispenseBasicZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtDispenseBasicZ.Name = "txtDispenseBasicZ"
        Me.txtDispenseBasicZ.ReadOnly = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'btnAutoZero
        '
        resources.ApplyResources(Me.btnAutoZero, "btnAutoZero")
        Me.btnAutoZero.Name = "btnAutoZero"
        Me.ToolTip1.SetToolTip(Me.btnAutoZero, resources.GetString("btnAutoZero.ToolTip"))
        Me.btnAutoZero.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'txtPosZ
        '
        Me.txtPosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtPosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtPosZ, "txtPosZ")
        Me.txtPosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtPosZ.Name = "txtPosZ"
        Me.txtPosZ.ReadOnly = True
        '
        'lblReaderPosZ
        '
        Me.lblReaderPosZ.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblReaderPosZ, "lblReaderPosZ")
        Me.lblReaderPosZ.Name = "lblReaderPosZ"
        '
        'txtPosY
        '
        Me.txtPosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtPosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtPosY, "txtPosY")
        Me.txtPosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtPosY.Name = "txtPosY"
        Me.txtPosY.ReadOnly = True
        '
        'txtPosX
        '
        Me.txtPosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtPosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtPosX, "txtPosX")
        Me.txtPosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtPosX.Name = "txtPosX"
        Me.txtPosX.ReadOnly = True
        '
        'lblReaderPosY
        '
        Me.lblReaderPosY.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblReaderPosY, "lblReaderPosY")
        Me.lblReaderPosY.Name = "lblReaderPosY"
        '
        'lblReaderPosX
        '
        Me.lblReaderPosX.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblReaderPosX, "lblReaderPosX")
        Me.lblReaderPosX.Name = "lblReaderPosX"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtCCDPosZ)
        Me.GroupBox2.Controls.Add(Me.txtCCDPosY)
        Me.GroupBox2.Controls.Add(Me.txtCCDPosX)
        Me.GroupBox2.Controls.Add(Me.btnSetPos)
        Me.GroupBox2.Controls.Add(Me.lblCCDPosZ)
        Me.GroupBox2.Controls.Add(Me.lblCCDPosY)
        Me.GroupBox2.Controls.Add(Me.lblCCDPosX)
        Me.GroupBox2.Controls.Add(Me.btnGoCCDPos)
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'txtCCDPosZ
        '
        Me.txtCCDPosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtCCDPosZ, "txtCCDPosZ")
        Me.txtCCDPosZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosZ.Name = "txtCCDPosZ"
        Me.txtCCDPosZ.ReadOnly = True
        '
        'txtCCDPosY
        '
        Me.txtCCDPosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtCCDPosY, "txtCCDPosY")
        Me.txtCCDPosY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosY.Name = "txtCCDPosY"
        Me.txtCCDPosY.ReadOnly = True
        '
        'txtCCDPosX
        '
        Me.txtCCDPosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtCCDPosX, "txtCCDPosX")
        Me.txtCCDPosX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosX.Name = "txtCCDPosX"
        Me.txtCCDPosX.ReadOnly = True
        '
        'btnSetPos
        '
        Me.btnSetPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        resources.ApplyResources(Me.btnSetPos, "btnSetPos")
        Me.btnSetPos.FlatAppearance.BorderSize = 0
        Me.btnSetPos.Name = "btnSetPos"
        Me.ToolTip1.SetToolTip(Me.btnSetPos, resources.GetString("btnSetPos.ToolTip"))
        Me.btnSetPos.UseVisualStyleBackColor = True
        '
        'lblCCDPosZ
        '
        Me.lblCCDPosZ.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblCCDPosZ, "lblCCDPosZ")
        Me.lblCCDPosZ.Name = "lblCCDPosZ"
        '
        'lblCCDPosY
        '
        Me.lblCCDPosY.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblCCDPosY, "lblCCDPosY")
        Me.lblCCDPosY.Name = "lblCCDPosY"
        '
        'lblCCDPosX
        '
        Me.lblCCDPosX.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblCCDPosX, "lblCCDPosX")
        Me.lblCCDPosX.Name = "lblCCDPosX"
        '
        'btnGoCCDPos
        '
        Me.btnGoCCDPos.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        resources.ApplyResources(Me.btnGoCCDPos, "btnGoCCDPos")
        Me.btnGoCCDPos.FlatAppearance.BorderSize = 0
        Me.btnGoCCDPos.Name = "btnGoCCDPos"
        Me.ToolTip1.SetToolTip(Me.btnGoCCDPos, resources.GetString("btnGoCCDPos.ToolTip"))
        Me.btnGoCCDPos.UseVisualStyleBackColor = True
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
        'btnSetPos_2
        '
        Me.btnSetPos_2.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetPos_2.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        resources.ApplyResources(Me.btnSetPos_2, "btnSetPos_2")
        Me.btnSetPos_2.FlatAppearance.BorderSize = 0
        Me.btnSetPos_2.Name = "btnSetPos_2"
        Me.ToolTip1.SetToolTip(Me.btnSetPos_2, resources.GetString("btnSetPos_2.ToolTip"))
        Me.btnSetPos_2.UseVisualStyleBackColor = True
        '
        'btnGoCCDPos_2
        '
        Me.btnGoCCDPos_2.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        resources.ApplyResources(Me.btnGoCCDPos_2, "btnGoCCDPos_2")
        Me.btnGoCCDPos_2.FlatAppearance.BorderSize = 0
        Me.btnGoCCDPos_2.Name = "btnGoCCDPos_2"
        Me.ToolTip1.SetToolTip(Me.btnGoCCDPos_2, resources.GetString("btnGoCCDPos_2.ToolTip"))
        Me.btnGoCCDPos_2.UseVisualStyleBackColor = True
        '
        'btnAutoZero_2
        '
        resources.ApplyResources(Me.btnAutoZero_2, "btnAutoZero_2")
        Me.btnAutoZero_2.Name = "btnAutoZero_2"
        Me.ToolTip1.SetToolTip(Me.btnAutoZero_2, resources.GetString("btnAutoZero_2.ToolTip"))
        Me.btnAutoZero_2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.ToolTip1.SetToolTip(Me.Button1, resources.GetString("Button1.ToolTip"))
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnSetPos_NonArray
        '
        Me.btnSetPos_NonArray.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetPos_NonArray.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        resources.ApplyResources(Me.btnSetPos_NonArray, "btnSetPos_NonArray")
        Me.btnSetPos_NonArray.FlatAppearance.BorderSize = 0
        Me.btnSetPos_NonArray.Name = "btnSetPos_NonArray"
        Me.ToolTip1.SetToolTip(Me.btnSetPos_NonArray, resources.GetString("btnSetPos_NonArray.ToolTip"))
        Me.btnSetPos_NonArray.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnAdd, "btnAdd")
        Me.btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.Name = "btnAdd"
        Me.ToolTip1.SetToolTip(Me.btnAdd, resources.GetString("btnAdd.ToolTip"))
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnDelete, "btnDelete")
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.Name = "btnDelete"
        Me.ToolTip1.SetToolTip(Me.btnDelete, resources.GetString("btnDelete.ToolTip"))
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnSetPos_NonArray_2
        '
        Me.btnSetPos_NonArray_2.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetPos_NonArray_2.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._set
        resources.ApplyResources(Me.btnSetPos_NonArray_2, "btnSetPos_NonArray_2")
        Me.btnSetPos_NonArray_2.FlatAppearance.BorderSize = 0
        Me.btnSetPos_NonArray_2.Name = "btnSetPos_NonArray_2"
        Me.ToolTip1.SetToolTip(Me.btnSetPos_NonArray_2, resources.GetString("btnSetPos_NonArray_2.ToolTip"))
        Me.btnSetPos_NonArray_2.UseVisualStyleBackColor = True
        '
        'btnAdd_2
        '
        Me.btnAdd_2.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnAdd_2, "btnAdd_2")
        Me.btnAdd_2.FlatAppearance.BorderSize = 0
        Me.btnAdd_2.Name = "btnAdd_2"
        Me.ToolTip1.SetToolTip(Me.btnAdd_2, resources.GetString("btnAdd_2.ToolTip"))
        Me.btnAdd_2.UseVisualStyleBackColor = True
        '
        'btnDelete_2
        '
        Me.btnDelete_2.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnDelete_2, "btnDelete_2")
        Me.btnDelete_2.FlatAppearance.BorderSize = 0
        Me.btnDelete_2.Name = "btnDelete_2"
        Me.ToolTip1.SetToolTip(Me.btnDelete_2, resources.GetString("btnDelete_2.ToolTip"))
        Me.btnDelete_2.UseVisualStyleBackColor = True
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'cboB
        '
        resources.ApplyResources(Me.cboB, "cboB")
        Me.cboB.FormattingEnabled = True
        Me.cboB.Name = "cboB"
        '
        'TabConveyorControl_CCD_Laser
        '
        Me.TabConveyorControl_CCD_Laser.Controls.Add(Me.TabPage1)
        Me.TabConveyorControl_CCD_Laser.Controls.Add(Me.TabPage2)
        resources.ApplyResources(Me.TabConveyorControl_CCD_Laser, "TabConveyorControl_CCD_Laser")
        Me.TabConveyorControl_CCD_Laser.Name = "TabConveyorControl_CCD_Laser"
        Me.TabConveyorControl_CCD_Laser.SelectedIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label27)
        Me.GroupBox4.Controls.Add(Me.txtDispenseBasicZValve2_2)
        Me.GroupBox4.Controls.Add(Me.Label26)
        Me.GroupBox4.Controls.Add(Me.btnSetZPos_2)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.txtDispenseBasicZ_2)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.btnAutoZero_2)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Controls.Add(Me.txtPosZ_2)
        Me.GroupBox4.Controls.Add(Me.Label21)
        Me.GroupBox4.Controls.Add(Me.txtPosY_2)
        Me.GroupBox4.Controls.Add(Me.txtPosX_2)
        Me.GroupBox4.Controls.Add(Me.Label22)
        Me.GroupBox4.Controls.Add(Me.Label23)
        resources.ApplyResources(Me.GroupBox4, "GroupBox4")
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.TabStop = False
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label27, "Label27")
        Me.Label27.Name = "Label27"
        '
        'txtDispenseBasicZValve2_2
        '
        Me.txtDispenseBasicZValve2_2.BackColor = System.Drawing.SystemColors.Control
        Me.txtDispenseBasicZValve2_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtDispenseBasicZValve2_2, "txtDispenseBasicZValve2_2")
        Me.txtDispenseBasicZValve2_2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtDispenseBasicZValve2_2.Name = "txtDispenseBasicZValve2_2"
        Me.txtDispenseBasicZValve2_2.ReadOnly = True
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label26, "Label26")
        Me.Label26.Name = "Label26"
        '
        'btnSetZPos_2
        '
        resources.ApplyResources(Me.btnSetZPos_2, "btnSetZPos_2")
        Me.btnSetZPos_2.Name = "btnSetZPos_2"
        Me.btnSetZPos_2.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label16, "Label16")
        Me.Label16.Name = "Label16"
        '
        'txtDispenseBasicZ_2
        '
        Me.txtDispenseBasicZ_2.BackColor = System.Drawing.SystemColors.Control
        Me.txtDispenseBasicZ_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtDispenseBasicZ_2, "txtDispenseBasicZ_2")
        Me.txtDispenseBasicZ_2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtDispenseBasicZ_2.Name = "txtDispenseBasicZ_2"
        Me.txtDispenseBasicZ_2.ReadOnly = True
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label17, "Label17")
        Me.Label17.Name = "Label17"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label18, "Label18")
        Me.Label18.Name = "Label18"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label19, "Label19")
        Me.Label19.Name = "Label19"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label20, "Label20")
        Me.Label20.Name = "Label20"
        '
        'txtPosZ_2
        '
        Me.txtPosZ_2.BackColor = System.Drawing.SystemColors.Control
        Me.txtPosZ_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtPosZ_2, "txtPosZ_2")
        Me.txtPosZ_2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtPosZ_2.Name = "txtPosZ_2"
        Me.txtPosZ_2.ReadOnly = True
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label21, "Label21")
        Me.Label21.Name = "Label21"
        '
        'txtPosY_2
        '
        Me.txtPosY_2.BackColor = System.Drawing.SystemColors.Control
        Me.txtPosY_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtPosY_2, "txtPosY_2")
        Me.txtPosY_2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtPosY_2.Name = "txtPosY_2"
        Me.txtPosY_2.ReadOnly = True
        '
        'txtPosX_2
        '
        Me.txtPosX_2.BackColor = System.Drawing.SystemColors.Control
        Me.txtPosX_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtPosX_2, "txtPosX_2")
        Me.txtPosX_2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtPosX_2.Name = "txtPosX_2"
        Me.txtPosX_2.ReadOnly = True
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label22, "Label22")
        Me.Label22.Name = "Label22"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label23, "Label23")
        Me.Label23.Name = "Label23"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.txtCCDPosZ_2)
        Me.GroupBox3.Controls.Add(Me.txtCCDPosY_2)
        Me.GroupBox3.Controls.Add(Me.txtCCDPosX_2)
        Me.GroupBox3.Controls.Add(Me.btnSetPos_2)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.btnGoCCDPos_2)
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        '
        'txtCCDPosZ_2
        '
        Me.txtCCDPosZ_2.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosZ_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtCCDPosZ_2, "txtCCDPosZ_2")
        Me.txtCCDPosZ_2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosZ_2.Name = "txtCCDPosZ_2"
        Me.txtCCDPosZ_2.ReadOnly = True
        '
        'txtCCDPosY_2
        '
        Me.txtCCDPosY_2.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosY_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtCCDPosY_2, "txtCCDPosY_2")
        Me.txtCCDPosY_2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosY_2.Name = "txtCCDPosY_2"
        Me.txtCCDPosY_2.ReadOnly = True
        '
        'txtCCDPosX_2
        '
        Me.txtCCDPosX_2.BackColor = System.Drawing.SystemColors.Control
        Me.txtCCDPosX_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtCCDPosX_2, "txtCCDPosX_2")
        Me.txtCCDPosX_2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCCDPosX_2.Name = "txtCCDPosX_2"
        Me.txtCCDPosX_2.ReadOnly = True
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.Name = "Label15"
        '
        'chkLaserReaderEnable
        '
        resources.ApplyResources(Me.chkLaserReaderEnable, "chkLaserReaderEnable")
        Me.chkLaserReaderEnable.Name = "chkLaserReaderEnable"
        Me.chkLaserReaderEnable.UseVisualStyleBackColor = True
        '
        'TabConveyorControl_CCD_Laser_NonArray
        '
        Me.TabConveyorControl_CCD_Laser_NonArray.Controls.Add(Me.TabPage3)
        Me.TabConveyorControl_CCD_Laser_NonArray.Controls.Add(Me.TabPage4)
        resources.ApplyResources(Me.TabConveyorControl_CCD_Laser_NonArray, "TabConveyorControl_CCD_Laser_NonArray")
        Me.TabConveyorControl_CCD_Laser_NonArray.Name = "TabConveyorControl_CCD_Laser_NonArray"
        Me.TabConveyorControl_CCD_Laser_NonArray.SelectedIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.NonArrayGroup)
        resources.ApplyResources(Me.TabPage3, "TabPage3")
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'NonArrayGroup
        '
        Me.NonArrayGroup.Controls.Add(Me.btnSetPos_NonArray)
        Me.NonArrayGroup.Controls.Add(Me.btnAdd)
        Me.NonArrayGroup.Controls.Add(Me.btnDelete)
        Me.NonArrayGroup.Controls.Add(Me.lstNonArray)
        resources.ApplyResources(Me.NonArrayGroup, "NonArrayGroup")
        Me.NonArrayGroup.Name = "NonArrayGroup"
        Me.NonArrayGroup.TabStop = False
        '
        'lstNonArray
        '
        resources.ApplyResources(Me.lstNonArray, "lstNonArray")
        Me.lstNonArray.FormattingEnabled = True
        Me.lstNonArray.Items.AddRange(New Object() {resources.GetString("lstNonArray.Items")})
        Me.lstNonArray.Name = "lstNonArray"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.NonArrayGroup2)
        resources.ApplyResources(Me.TabPage4, "TabPage4")
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'NonArrayGroup2
        '
        Me.NonArrayGroup2.Controls.Add(Me.btnSetPos_NonArray_2)
        Me.NonArrayGroup2.Controls.Add(Me.btnAdd_2)
        Me.NonArrayGroup2.Controls.Add(Me.btnDelete_2)
        Me.NonArrayGroup2.Controls.Add(Me.lstNonArray_2)
        resources.ApplyResources(Me.NonArrayGroup2, "NonArrayGroup2")
        Me.NonArrayGroup2.Name = "NonArrayGroup2"
        Me.NonArrayGroup2.TabStop = False
        '
        'lstNonArray_2
        '
        resources.ApplyResources(Me.lstNonArray_2, "lstNonArray_2")
        Me.lstNonArray_2.FormattingEnabled = True
        Me.lstNonArray_2.Items.AddRange(New Object() {resources.GetString("lstNonArray_2.Items")})
        Me.lstNonArray_2.Name = "lstNonArray_2"
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
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UcDisplay1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        resources.ApplyResources(Me.UcDisplay1, "UcDisplay1")
        Me.UcDisplay1.Name = "UcDisplay1"
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
        'frmRecipe03
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.TabConveyorControl_CCD_Laser_NonArray)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.chkLaserReaderEnable)
        Me.Controls.Add(Me.TabConveyorControl_CCD_Laser)
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboB)
        Me.Controls.Add(Me.UcLightControl1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.ucJoyStick1)
        Me.Controls.Add(Me.UcDisplay1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRecipe03"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabConveyorControl_CCD_Laser.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TabConveyorControl_CCD_Laser_NonArray.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.NonArrayGroup.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.NonArrayGroup2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents ucJoyStick1 As ucJoyStick
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPosZ As System.Windows.Forms.TextBox
    Friend WithEvents lblReaderPosZ As System.Windows.Forms.Label
    Friend WithEvents txtPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblReaderPosY As System.Windows.Forms.Label
    Friend WithEvents lblReaderPosX As System.Windows.Forms.Label
    Friend WithEvents btnSetPos As System.Windows.Forms.Button
    Friend WithEvents btnGoCCDPos As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtCCDPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtCCDPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtCCDPosX As System.Windows.Forms.TextBox
    Friend WithEvents lblCCDPosZ As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosY As System.Windows.Forms.Label
    Friend WithEvents lblCCDPosX As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnAutoZero As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtDispenseBasicZ As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UcLightControl1 As ucLightControl
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboB As System.Windows.Forms.ComboBox
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents TabConveyorControl_CCD_Laser As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents chkLaserReaderEnable As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtDispenseBasicZ_2 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnAutoZero_2 As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtPosZ_2 As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtPosY_2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPosX_2 As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtCCDPosZ_2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCCDPosY_2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCCDPosX_2 As System.Windows.Forms.TextBox
    Friend WithEvents btnSetPos_2 As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnGoCCDPos_2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnSetZPos As System.Windows.Forms.Button
    Friend WithEvents btnSetZPos_2 As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtDispenseBasicZvalve2 As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtDispenseBasicZValve2_2 As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents TabConveyorControl_CCD_Laser_NonArray As System.Windows.Forms.TabControl
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents NonArrayGroup As System.Windows.Forms.GroupBox
    Friend WithEvents btnSetPos_NonArray As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents lstNonArray As System.Windows.Forms.ListBox
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents NonArrayGroup2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSetPos_NonArray_2 As System.Windows.Forms.Button
    Friend WithEvents btnAdd_2 As System.Windows.Forms.Button
    Friend WithEvents btnDelete_2 As System.Windows.Forms.Button
    Friend WithEvents lstNonArray_2 As System.Windows.Forms.ListBox
End Class

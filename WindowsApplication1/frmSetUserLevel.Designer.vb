<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetUserLevel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetUserLevel))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.cboUserLevel = New System.Windows.Forms.ComboBox()
        Me.dgwWriteLevel = New System.Windows.Forms.DataGridView()
        Me.lblLevel = New System.Windows.Forms.Label()
        Me.lblPasword = New System.Windows.Forms.Label()
        Me.lblID = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.btnAuth10 = New System.Windows.Forms.Button()
        Me.btnAuth9 = New System.Windows.Forms.Button()
        Me.btnAuth8 = New System.Windows.Forms.Button()
        Me.btnAuth7 = New System.Windows.Forms.Button()
        Me.btnAuth6 = New System.Windows.Forms.Button()
        Me.btnAuth5 = New System.Windows.Forms.Button()
        Me.btnAuth4 = New System.Windows.Forms.Button()
        Me.btnAuth3 = New System.Windows.Forms.Button()
        Me.btnAuth1 = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.btnAuth33 = New System.Windows.Forms.Button()
        Me.btnAuth32 = New System.Windows.Forms.Button()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.btnAuth29 = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.btnAuth30 = New System.Windows.Forms.Button()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.btnAuth31 = New System.Windows.Forms.Button()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.btnAuth24 = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.btnAuth28 = New System.Windows.Forms.Button()
        Me.btnAuth23 = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.btnAuth27 = New System.Windows.Forms.Button()
        Me.btnAuth22 = New System.Windows.Forms.Button()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.btnAuth26 = New System.Windows.Forms.Button()
        Me.btnAuth21 = New System.Windows.Forms.Button()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.btnAuth25 = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.btnAuth20 = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.btnAuth19 = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnAuth18 = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnAuth17 = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btnAuth16 = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnAuth15 = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnAuth14 = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnAuth13 = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btnAuth12 = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnAuth11 = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnAuth2 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnModify = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.dgwWriteLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtDesc
        '
        resources.ApplyResources(Me.txtDesc, "txtDesc")
        Me.txtDesc.BackColor = System.Drawing.SystemColors.Control
        Me.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc.Name = "txtDesc"
        Me.ToolTip1.SetToolTip(Me.txtDesc, resources.GetString("txtDesc.ToolTip"))
        '
        'cboUserLevel
        '
        resources.ApplyResources(Me.cboUserLevel, "cboUserLevel")
        Me.cboUserLevel.BackColor = System.Drawing.Color.White
        Me.cboUserLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUserLevel.FormattingEnabled = True
        Me.cboUserLevel.Name = "cboUserLevel"
        Me.ToolTip1.SetToolTip(Me.cboUserLevel, resources.GetString("cboUserLevel.ToolTip"))
        '
        'dgwWriteLevel
        '
        resources.ApplyResources(Me.dgwWriteLevel, "dgwWriteLevel")
        Me.dgwWriteLevel.AllowUserToAddRows = False
        Me.dgwWriteLevel.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dgwWriteLevel.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgwWriteLevel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgwWriteLevel.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgwWriteLevel.MultiSelect = False
        Me.dgwWriteLevel.Name = "dgwWriteLevel"
        Me.dgwWriteLevel.ReadOnly = True
        Me.dgwWriteLevel.RowTemplate.Height = 24
        Me.dgwWriteLevel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ToolTip1.SetToolTip(Me.dgwWriteLevel, resources.GetString("dgwWriteLevel.ToolTip"))
        '
        'lblLevel
        '
        resources.ApplyResources(Me.lblLevel, "lblLevel")
        Me.lblLevel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLevel.Name = "lblLevel"
        Me.ToolTip1.SetToolTip(Me.lblLevel, resources.GetString("lblLevel.ToolTip"))
        '
        'lblPasword
        '
        resources.ApplyResources(Me.lblPasword, "lblPasword")
        Me.lblPasword.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblPasword.Name = "lblPasword"
        Me.ToolTip1.SetToolTip(Me.lblPasword, resources.GetString("lblPasword.ToolTip"))
        '
        'lblID
        '
        resources.ApplyResources(Me.lblID, "lblID")
        Me.lblID.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblID.Name = "lblID"
        Me.ToolTip1.SetToolTip(Me.lblID, resources.GetString("lblID.ToolTip"))
        '
        'txtPassword
        '
        resources.ApplyResources(Me.txtPassword, "txtPassword")
        Me.txtPassword.BackColor = System.Drawing.Color.White
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Name = "txtPassword"
        Me.ToolTip1.SetToolTip(Me.txtPassword, resources.GetString("txtPassword.ToolTip"))
        '
        'txtID
        '
        resources.ApplyResources(Me.txtID, "txtID")
        Me.txtID.BackColor = System.Drawing.Color.White
        Me.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtID.Name = "txtID"
        Me.ToolTip1.SetToolTip(Me.txtID, resources.GetString("txtID.ToolTip"))
        '
        'btnAuth10
        '
        resources.ApplyResources(Me.btnAuth10, "btnAuth10")
        Me.btnAuth10.Name = "btnAuth10"
        Me.ToolTip1.SetToolTip(Me.btnAuth10, resources.GetString("btnAuth10.ToolTip"))
        Me.btnAuth10.UseVisualStyleBackColor = True
        '
        'btnAuth9
        '
        resources.ApplyResources(Me.btnAuth9, "btnAuth9")
        Me.btnAuth9.Name = "btnAuth9"
        Me.ToolTip1.SetToolTip(Me.btnAuth9, resources.GetString("btnAuth9.ToolTip"))
        Me.btnAuth9.UseVisualStyleBackColor = True
        '
        'btnAuth8
        '
        resources.ApplyResources(Me.btnAuth8, "btnAuth8")
        Me.btnAuth8.Name = "btnAuth8"
        Me.ToolTip1.SetToolTip(Me.btnAuth8, resources.GetString("btnAuth8.ToolTip"))
        Me.btnAuth8.UseVisualStyleBackColor = True
        '
        'btnAuth7
        '
        resources.ApplyResources(Me.btnAuth7, "btnAuth7")
        Me.btnAuth7.Name = "btnAuth7"
        Me.ToolTip1.SetToolTip(Me.btnAuth7, resources.GetString("btnAuth7.ToolTip"))
        Me.btnAuth7.UseVisualStyleBackColor = True
        '
        'btnAuth6
        '
        resources.ApplyResources(Me.btnAuth6, "btnAuth6")
        Me.btnAuth6.Name = "btnAuth6"
        Me.ToolTip1.SetToolTip(Me.btnAuth6, resources.GetString("btnAuth6.ToolTip"))
        Me.btnAuth6.UseVisualStyleBackColor = True
        '
        'btnAuth5
        '
        resources.ApplyResources(Me.btnAuth5, "btnAuth5")
        Me.btnAuth5.Name = "btnAuth5"
        Me.ToolTip1.SetToolTip(Me.btnAuth5, resources.GetString("btnAuth5.ToolTip"))
        Me.btnAuth5.UseVisualStyleBackColor = True
        '
        'btnAuth4
        '
        resources.ApplyResources(Me.btnAuth4, "btnAuth4")
        Me.btnAuth4.Name = "btnAuth4"
        Me.ToolTip1.SetToolTip(Me.btnAuth4, resources.GetString("btnAuth4.ToolTip"))
        Me.btnAuth4.UseVisualStyleBackColor = True
        '
        'btnAuth3
        '
        resources.ApplyResources(Me.btnAuth3, "btnAuth3")
        Me.btnAuth3.Name = "btnAuth3"
        Me.ToolTip1.SetToolTip(Me.btnAuth3, resources.GetString("btnAuth3.ToolTip"))
        Me.btnAuth3.UseVisualStyleBackColor = True
        '
        'btnAuth1
        '
        resources.ApplyResources(Me.btnAuth1, "btnAuth1")
        Me.btnAuth1.Name = "btnAuth1"
        Me.ToolTip1.SetToolTip(Me.btnAuth1, resources.GetString("btnAuth1.ToolTip"))
        Me.btnAuth1.UseVisualStyleBackColor = True
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Name = "Label10"
        Me.ToolTip1.SetToolTip(Me.Label10, resources.GetString("Label10.ToolTip"))
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Name = "Label9"
        Me.ToolTip1.SetToolTip(Me.Label9, resources.GetString("Label9.ToolTip"))
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Name = "Label8"
        Me.ToolTip1.SetToolTip(Me.Label8, resources.GetString("Label8.ToolTip"))
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Name = "Label7"
        Me.ToolTip1.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip"))
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Name = "Label6"
        Me.ToolTip1.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip"))
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Name = "Label5"
        Me.ToolTip1.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip"))
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Name = "Label4"
        Me.ToolTip1.SetToolTip(Me.Label4, resources.GetString("Label4.ToolTip"))
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Name = "Label3"
        Me.ToolTip1.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip"))
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'Panel1
        '
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.btnAuth33)
        Me.Panel1.Controls.Add(Me.btnAuth32)
        Me.Panel1.Controls.Add(Me.Label32)
        Me.Panel1.Controls.Add(Me.btnAuth29)
        Me.Panel1.Controls.Add(Me.Label29)
        Me.Panel1.Controls.Add(Me.btnAuth30)
        Me.Panel1.Controls.Add(Me.Label30)
        Me.Panel1.Controls.Add(Me.btnAuth31)
        Me.Panel1.Controls.Add(Me.Label31)
        Me.Panel1.Controls.Add(Me.btnAuth24)
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Controls.Add(Me.btnAuth28)
        Me.Panel1.Controls.Add(Me.btnAuth23)
        Me.Panel1.Controls.Add(Me.Label28)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.btnAuth27)
        Me.Panel1.Controls.Add(Me.btnAuth22)
        Me.Panel1.Controls.Add(Me.Label27)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.btnAuth26)
        Me.Panel1.Controls.Add(Me.btnAuth21)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.btnAuth25)
        Me.Panel1.Controls.Add(Me.Label25)
        Me.Panel1.Controls.Add(Me.btnAuth20)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.btnAuth19)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.btnAuth18)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.btnAuth17)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.btnAuth16)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.btnAuth15)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.btnAuth14)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.btnAuth13)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.btnAuth10)
        Me.Panel1.Controls.Add(Me.btnAuth9)
        Me.Panel1.Controls.Add(Me.btnAuth8)
        Me.Panel1.Controls.Add(Me.btnAuth7)
        Me.Panel1.Controls.Add(Me.btnAuth6)
        Me.Panel1.Controls.Add(Me.btnAuth5)
        Me.Panel1.Controls.Add(Me.btnAuth4)
        Me.Panel1.Controls.Add(Me.btnAuth3)
        Me.Panel1.Controls.Add(Me.btnAuth1)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Name = "Panel1"
        Me.ToolTip1.SetToolTip(Me.Panel1, resources.GetString("Panel1.ToolTip"))
        '
        'Label33
        '
        resources.ApplyResources(Me.Label33, "Label33")
        Me.Label33.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label33.Name = "Label33"
        Me.ToolTip1.SetToolTip(Me.Label33, resources.GetString("Label33.ToolTip"))
        '
        'btnAuth33
        '
        resources.ApplyResources(Me.btnAuth33, "btnAuth33")
        Me.btnAuth33.Name = "btnAuth33"
        Me.ToolTip1.SetToolTip(Me.btnAuth33, resources.GetString("btnAuth33.ToolTip"))
        Me.btnAuth33.UseVisualStyleBackColor = True
        '
        'btnAuth32
        '
        resources.ApplyResources(Me.btnAuth32, "btnAuth32")
        Me.btnAuth32.Name = "btnAuth32"
        Me.ToolTip1.SetToolTip(Me.btnAuth32, resources.GetString("btnAuth32.ToolTip"))
        Me.btnAuth32.UseVisualStyleBackColor = True
        '
        'Label32
        '
        resources.ApplyResources(Me.Label32, "Label32")
        Me.Label32.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label32.Name = "Label32"
        Me.ToolTip1.SetToolTip(Me.Label32, resources.GetString("Label32.ToolTip"))
        '
        'btnAuth29
        '
        resources.ApplyResources(Me.btnAuth29, "btnAuth29")
        Me.btnAuth29.Name = "btnAuth29"
        Me.ToolTip1.SetToolTip(Me.btnAuth29, resources.GetString("btnAuth29.ToolTip"))
        Me.btnAuth29.UseVisualStyleBackColor = True
        '
        'Label29
        '
        resources.ApplyResources(Me.Label29, "Label29")
        Me.Label29.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label29.Name = "Label29"
        Me.ToolTip1.SetToolTip(Me.Label29, resources.GetString("Label29.ToolTip"))
        '
        'btnAuth30
        '
        resources.ApplyResources(Me.btnAuth30, "btnAuth30")
        Me.btnAuth30.Name = "btnAuth30"
        Me.ToolTip1.SetToolTip(Me.btnAuth30, resources.GetString("btnAuth30.ToolTip"))
        Me.btnAuth30.UseVisualStyleBackColor = True
        '
        'Label30
        '
        resources.ApplyResources(Me.Label30, "Label30")
        Me.Label30.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label30.Name = "Label30"
        Me.ToolTip1.SetToolTip(Me.Label30, resources.GetString("Label30.ToolTip"))
        '
        'btnAuth31
        '
        resources.ApplyResources(Me.btnAuth31, "btnAuth31")
        Me.btnAuth31.Name = "btnAuth31"
        Me.ToolTip1.SetToolTip(Me.btnAuth31, resources.GetString("btnAuth31.ToolTip"))
        Me.btnAuth31.UseVisualStyleBackColor = True
        '
        'Label31
        '
        resources.ApplyResources(Me.Label31, "Label31")
        Me.Label31.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label31.Name = "Label31"
        Me.ToolTip1.SetToolTip(Me.Label31, resources.GetString("Label31.ToolTip"))
        '
        'btnAuth24
        '
        resources.ApplyResources(Me.btnAuth24, "btnAuth24")
        Me.btnAuth24.Name = "btnAuth24"
        Me.ToolTip1.SetToolTip(Me.btnAuth24, resources.GetString("btnAuth24.ToolTip"))
        Me.btnAuth24.UseVisualStyleBackColor = True
        '
        'Label24
        '
        resources.ApplyResources(Me.Label24, "Label24")
        Me.Label24.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label24.Name = "Label24"
        Me.ToolTip1.SetToolTip(Me.Label24, resources.GetString("Label24.ToolTip"))
        '
        'btnAuth28
        '
        resources.ApplyResources(Me.btnAuth28, "btnAuth28")
        Me.btnAuth28.Name = "btnAuth28"
        Me.ToolTip1.SetToolTip(Me.btnAuth28, resources.GetString("btnAuth28.ToolTip"))
        Me.btnAuth28.UseVisualStyleBackColor = True
        '
        'btnAuth23
        '
        resources.ApplyResources(Me.btnAuth23, "btnAuth23")
        Me.btnAuth23.Name = "btnAuth23"
        Me.ToolTip1.SetToolTip(Me.btnAuth23, resources.GetString("btnAuth23.ToolTip"))
        Me.btnAuth23.UseVisualStyleBackColor = True
        '
        'Label28
        '
        resources.ApplyResources(Me.Label28, "Label28")
        Me.Label28.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label28.Name = "Label28"
        Me.ToolTip1.SetToolTip(Me.Label28, resources.GetString("Label28.ToolTip"))
        '
        'Label23
        '
        resources.ApplyResources(Me.Label23, "Label23")
        Me.Label23.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Name = "Label23"
        Me.ToolTip1.SetToolTip(Me.Label23, resources.GetString("Label23.ToolTip"))
        '
        'btnAuth27
        '
        resources.ApplyResources(Me.btnAuth27, "btnAuth27")
        Me.btnAuth27.Name = "btnAuth27"
        Me.ToolTip1.SetToolTip(Me.btnAuth27, resources.GetString("btnAuth27.ToolTip"))
        Me.btnAuth27.UseVisualStyleBackColor = True
        '
        'btnAuth22
        '
        resources.ApplyResources(Me.btnAuth22, "btnAuth22")
        Me.btnAuth22.Name = "btnAuth22"
        Me.ToolTip1.SetToolTip(Me.btnAuth22, resources.GetString("btnAuth22.ToolTip"))
        Me.btnAuth22.UseVisualStyleBackColor = True
        '
        'Label27
        '
        resources.ApplyResources(Me.Label27, "Label27")
        Me.Label27.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label27.Name = "Label27"
        Me.ToolTip1.SetToolTip(Me.Label27, resources.GetString("Label27.ToolTip"))
        '
        'Label22
        '
        resources.ApplyResources(Me.Label22, "Label22")
        Me.Label22.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Name = "Label22"
        Me.ToolTip1.SetToolTip(Me.Label22, resources.GetString("Label22.ToolTip"))
        '
        'btnAuth26
        '
        resources.ApplyResources(Me.btnAuth26, "btnAuth26")
        Me.btnAuth26.Name = "btnAuth26"
        Me.ToolTip1.SetToolTip(Me.btnAuth26, resources.GetString("btnAuth26.ToolTip"))
        Me.btnAuth26.UseVisualStyleBackColor = True
        '
        'btnAuth21
        '
        resources.ApplyResources(Me.btnAuth21, "btnAuth21")
        Me.btnAuth21.Name = "btnAuth21"
        Me.ToolTip1.SetToolTip(Me.btnAuth21, resources.GetString("btnAuth21.ToolTip"))
        Me.btnAuth21.UseVisualStyleBackColor = True
        '
        'Label26
        '
        resources.ApplyResources(Me.Label26, "Label26")
        Me.Label26.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label26.Name = "Label26"
        Me.ToolTip1.SetToolTip(Me.Label26, resources.GetString("Label26.ToolTip"))
        '
        'Label21
        '
        resources.ApplyResources(Me.Label21, "Label21")
        Me.Label21.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label21.Name = "Label21"
        Me.ToolTip1.SetToolTip(Me.Label21, resources.GetString("Label21.ToolTip"))
        '
        'btnAuth25
        '
        resources.ApplyResources(Me.btnAuth25, "btnAuth25")
        Me.btnAuth25.Name = "btnAuth25"
        Me.ToolTip1.SetToolTip(Me.btnAuth25, resources.GetString("btnAuth25.ToolTip"))
        Me.btnAuth25.UseVisualStyleBackColor = True
        '
        'Label25
        '
        resources.ApplyResources(Me.Label25, "Label25")
        Me.Label25.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label25.Name = "Label25"
        Me.ToolTip1.SetToolTip(Me.Label25, resources.GetString("Label25.ToolTip"))
        '
        'btnAuth20
        '
        resources.ApplyResources(Me.btnAuth20, "btnAuth20")
        Me.btnAuth20.Name = "btnAuth20"
        Me.ToolTip1.SetToolTip(Me.btnAuth20, resources.GetString("btnAuth20.ToolTip"))
        Me.btnAuth20.UseVisualStyleBackColor = True
        '
        'Label20
        '
        resources.ApplyResources(Me.Label20, "Label20")
        Me.Label20.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label20.Name = "Label20"
        Me.ToolTip1.SetToolTip(Me.Label20, resources.GetString("Label20.ToolTip"))
        '
        'btnAuth19
        '
        resources.ApplyResources(Me.btnAuth19, "btnAuth19")
        Me.btnAuth19.Name = "btnAuth19"
        Me.ToolTip1.SetToolTip(Me.btnAuth19, resources.GetString("btnAuth19.ToolTip"))
        Me.btnAuth19.UseVisualStyleBackColor = True
        '
        'Label19
        '
        resources.ApplyResources(Me.Label19, "Label19")
        Me.Label19.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Name = "Label19"
        Me.ToolTip1.SetToolTip(Me.Label19, resources.GetString("Label19.ToolTip"))
        '
        'btnAuth18
        '
        resources.ApplyResources(Me.btnAuth18, "btnAuth18")
        Me.btnAuth18.Name = "btnAuth18"
        Me.ToolTip1.SetToolTip(Me.btnAuth18, resources.GetString("btnAuth18.ToolTip"))
        Me.btnAuth18.UseVisualStyleBackColor = True
        '
        'Label18
        '
        resources.ApplyResources(Me.Label18, "Label18")
        Me.Label18.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Name = "Label18"
        Me.ToolTip1.SetToolTip(Me.Label18, resources.GetString("Label18.ToolTip"))
        '
        'btnAuth17
        '
        resources.ApplyResources(Me.btnAuth17, "btnAuth17")
        Me.btnAuth17.Name = "btnAuth17"
        Me.ToolTip1.SetToolTip(Me.btnAuth17, resources.GetString("btnAuth17.ToolTip"))
        Me.btnAuth17.UseVisualStyleBackColor = True
        '
        'Label17
        '
        resources.ApplyResources(Me.Label17, "Label17")
        Me.Label17.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Name = "Label17"
        Me.ToolTip1.SetToolTip(Me.Label17, resources.GetString("Label17.ToolTip"))
        '
        'btnAuth16
        '
        resources.ApplyResources(Me.btnAuth16, "btnAuth16")
        Me.btnAuth16.Name = "btnAuth16"
        Me.ToolTip1.SetToolTip(Me.btnAuth16, resources.GetString("btnAuth16.ToolTip"))
        Me.btnAuth16.UseVisualStyleBackColor = True
        '
        'Label16
        '
        resources.ApplyResources(Me.Label16, "Label16")
        Me.Label16.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Name = "Label16"
        Me.ToolTip1.SetToolTip(Me.Label16, resources.GetString("Label16.ToolTip"))
        '
        'btnAuth15
        '
        resources.ApplyResources(Me.btnAuth15, "btnAuth15")
        Me.btnAuth15.Name = "btnAuth15"
        Me.ToolTip1.SetToolTip(Me.btnAuth15, resources.GetString("btnAuth15.ToolTip"))
        Me.btnAuth15.UseVisualStyleBackColor = True
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Name = "Label15"
        Me.ToolTip1.SetToolTip(Me.Label15, resources.GetString("Label15.ToolTip"))
        '
        'btnAuth14
        '
        resources.ApplyResources(Me.btnAuth14, "btnAuth14")
        Me.btnAuth14.Name = "btnAuth14"
        Me.ToolTip1.SetToolTip(Me.btnAuth14, resources.GetString("btnAuth14.ToolTip"))
        Me.btnAuth14.UseVisualStyleBackColor = True
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Name = "Label14"
        Me.ToolTip1.SetToolTip(Me.Label14, resources.GetString("Label14.ToolTip"))
        '
        'btnAuth13
        '
        resources.ApplyResources(Me.btnAuth13, "btnAuth13")
        Me.btnAuth13.Name = "btnAuth13"
        Me.ToolTip1.SetToolTip(Me.btnAuth13, resources.GetString("btnAuth13.ToolTip"))
        Me.btnAuth13.UseVisualStyleBackColor = True
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Name = "Label13"
        Me.ToolTip1.SetToolTip(Me.Label13, resources.GetString("Label13.ToolTip"))
        '
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.ToolTip1.SetToolTip(Me.TabControl1, resources.GetString("TabControl1.ToolTip"))
        '
        'TabPage1
        '
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TabPage1.Controls.Add(Me.btnAuth12)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.btnAuth11)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.btnAuth2)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Name = "TabPage1"
        Me.ToolTip1.SetToolTip(Me.TabPage1, resources.GetString("TabPage1.ToolTip"))
        '
        'btnAuth12
        '
        resources.ApplyResources(Me.btnAuth12, "btnAuth12")
        Me.btnAuth12.Name = "btnAuth12"
        Me.ToolTip1.SetToolTip(Me.btnAuth12, resources.GetString("btnAuth12.ToolTip"))
        Me.btnAuth12.UseVisualStyleBackColor = True
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label12.Name = "Label12"
        Me.ToolTip1.SetToolTip(Me.Label12, resources.GetString("Label12.ToolTip"))
        '
        'btnAuth11
        '
        resources.ApplyResources(Me.btnAuth11, "btnAuth11")
        Me.btnAuth11.Name = "btnAuth11"
        Me.ToolTip1.SetToolTip(Me.btnAuth11, resources.GetString("btnAuth11.ToolTip"))
        Me.btnAuth11.UseVisualStyleBackColor = True
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label11.Name = "Label11"
        Me.ToolTip1.SetToolTip(Me.Label11, resources.GetString("Label11.ToolTip"))
        '
        'btnAuth2
        '
        resources.ApplyResources(Me.btnAuth2, "btnAuth2")
        Me.btnAuth2.Name = "btnAuth2"
        Me.ToolTip1.SetToolTip(Me.btnAuth2, resources.GetString("btnAuth2.ToolTip"))
        Me.btnAuth2.UseVisualStyleBackColor = True
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label2.Name = "Label2"
        Me.ToolTip1.SetToolTip(Me.Label2, resources.GetString("Label2.ToolTip"))
        '
        'TabPage2
        '
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Controls.Add(Me.Panel1)
        Me.TabPage2.Name = "TabPage2"
        Me.ToolTip1.SetToolTip(Me.TabPage2, resources.GetString("TabPage2.ToolTip"))
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        resources.ApplyResources(Me.Panel2, "Panel2")
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Name = "Panel2"
        Me.ToolTip1.SetToolTip(Me.Panel2, resources.GetString("Panel2.ToolTip"))
        '
        'Panel3
        '
        resources.ApplyResources(Me.Panel3, "Panel3")
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Name = "Panel3"
        Me.ToolTip1.SetToolTip(Me.Panel3, resources.GetString("Panel3.ToolTip"))
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
        'btnModify
        '
        resources.ApplyResources(Me.btnModify, "btnModify")
        Me.btnModify.BackColor = System.Drawing.SystemColors.Control
        Me.btnModify.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Update
        Me.btnModify.FlatAppearance.BorderSize = 0
        Me.btnModify.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnModify.Name = "btnModify"
        Me.ToolTip1.SetToolTip(Me.btnModify, resources.GetString("btnModify.ToolTip"))
        Me.btnModify.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        resources.ApplyResources(Me.btnDelete, "btnDelete")
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Delete
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnDelete.Name = "btnDelete"
        Me.ToolTip1.SetToolTip(Me.btnDelete, resources.GetString("btnDelete.ToolTip"))
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        resources.ApplyResources(Me.btnAdd, "btnAdd")
        Me.btnAdd.BackColor = System.Drawing.SystemColors.Control
        Me.btnAdd.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Add1
        Me.btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnAdd.Name = "btnAdd"
        Me.ToolTip1.SetToolTip(Me.btnAdd, resources.GetString("btnAdd.ToolTip"))
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'frmSetUserLevel
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.cboUserLevel)
        Me.Controls.Add(Me.btnModify)
        Me.Controls.Add(Me.lblID)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.dgwWriteLevel)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lblPasword)
        Me.Controls.Add(Me.lblLevel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetUserLevel"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        CType(Me.dgwWriteLevel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub


    Friend WithEvents cboUserLevel As System.Windows.Forms.ComboBox
    Friend WithEvents btnModify As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents dgwWriteLevel As System.Windows.Forms.DataGridView
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lblLevel As System.Windows.Forms.Label
    Friend WithEvents lblPasword As System.Windows.Forms.Label
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents btnAuth10 As System.Windows.Forms.Button
    Friend WithEvents btnAuth9 As System.Windows.Forms.Button
    Friend WithEvents btnAuth8 As System.Windows.Forms.Button
    Friend WithEvents btnAuth7 As System.Windows.Forms.Button
    Friend WithEvents btnAuth6 As System.Windows.Forms.Button
    Friend WithEvents btnAuth5 As System.Windows.Forms.Button
    Friend WithEvents btnAuth4 As System.Windows.Forms.Button
    Friend WithEvents btnAuth3 As System.Windows.Forms.Button
    Friend WithEvents btnAuth1 As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnAuth12 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnAuth11 As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnAuth2 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnAuth13 As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnAuth24 As System.Windows.Forms.Button
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents btnAuth28 As System.Windows.Forms.Button
    Friend WithEvents btnAuth23 As System.Windows.Forms.Button
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents btnAuth27 As System.Windows.Forms.Button
    Friend WithEvents btnAuth22 As System.Windows.Forms.Button
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents btnAuth26 As System.Windows.Forms.Button
    Friend WithEvents btnAuth21 As System.Windows.Forms.Button
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnAuth25 As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents btnAuth20 As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents btnAuth19 As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnAuth18 As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnAuth17 As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnAuth16 As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnAuth15 As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnAuth14 As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents IdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PasswordDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LevelDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnAuth29 As System.Windows.Forms.Button
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents btnAuth30 As System.Windows.Forms.Button
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents btnAuth31 As System.Windows.Forms.Button
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents btnAuth32 As System.Windows.Forms.Button
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents btnAuth33 As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucDCSW800AQStatus
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucDCSW800AQStatus))
        Me.lblMachineBStatus = New System.Windows.Forms.Label()
        Me.lblMachineAStatus = New System.Windows.Forms.Label()
        Me.picConveyor = New System.Windows.Forms.PictureBox()
        Me.grpUnloadCassetteB = New System.Windows.Forms.GroupBox()
        Me.dgv_UnLCassetteB = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewImageColumn3 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.grpUnloadCassetteA = New System.Windows.Forms.GroupBox()
        Me.dgv_UnLCassetteA = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewImageColumn2 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.grpLoadCassetteB = New System.Windows.Forms.GroupBox()
        Me.dgv_LCassetteB = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpLoadCassetteA = New System.Windows.Forms.GroupBox()
        Me.dgv_LCassetteA = New System.Windows.Forms.DataGridView()
        Me.Column_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column_Pic = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Column_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtGlueID = New System.Windows.Forms.TextBox()
        Me.lblGlueID = New System.Windows.Forms.Label()
        Me.txtLotID = New System.Windows.Forms.TextBox()
        Me.lblLotID = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnMacAOpenMap = New System.Windows.Forms.Button()
        Me.btnMacBOpenMap = New System.Windows.Forms.Button()
        Me.lblMachineAWafer = New System.Windows.Forms.Label()
        Me.lblMachineBWafer = New System.Windows.Forms.Label()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbManualMapData = New System.Windows.Forms.CheckBox()
        Me.tbMapDataB = New System.Windows.Forms.TextBox()
        Me.tbMapDataA = New System.Windows.Forms.TextBox()
        Me.btnCleanBWaferId = New System.Windows.Forms.Button()
        Me.btnCleanAWaferId = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        CType(Me.picConveyor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpUnloadCassetteB.SuspendLayout()
        CType(Me.dgv_UnLCassetteB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpUnloadCassetteA.SuspendLayout()
        CType(Me.dgv_UnLCassetteA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLoadCassetteB.SuspendLayout()
        CType(Me.dgv_LCassetteB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLoadCassetteA.SuspendLayout()
        CType(Me.dgv_LCassetteA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblMachineBStatus
        '
        resources.ApplyResources(Me.lblMachineBStatus, "lblMachineBStatus")
        Me.lblMachineBStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMachineBStatus.Name = "lblMachineBStatus"
        Me.ToolTip1.SetToolTip(Me.lblMachineBStatus, resources.GetString("lblMachineBStatus.ToolTip"))
        '
        'lblMachineAStatus
        '
        resources.ApplyResources(Me.lblMachineAStatus, "lblMachineAStatus")
        Me.lblMachineAStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMachineAStatus.Name = "lblMachineAStatus"
        Me.ToolTip1.SetToolTip(Me.lblMachineAStatus, resources.GetString("lblMachineAStatus.ToolTip"))
        '
        'picConveyor
        '
        resources.ApplyResources(Me.picConveyor, "picConveyor")
        Me.picConveyor.Image = Global.WindowsApplication1.My.Resources.Resources.Conveyor
        Me.picConveyor.Name = "picConveyor"
        Me.picConveyor.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picConveyor, resources.GetString("picConveyor.ToolTip"))
        '
        'grpUnloadCassetteB
        '
        resources.ApplyResources(Me.grpUnloadCassetteB, "grpUnloadCassetteB")
        Me.grpUnloadCassetteB.Controls.Add(Me.dgv_UnLCassetteB)
        Me.grpUnloadCassetteB.Name = "grpUnloadCassetteB"
        Me.grpUnloadCassetteB.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpUnloadCassetteB, resources.GetString("grpUnloadCassetteB.ToolTip"))
        '
        'dgv_UnLCassetteB
        '
        resources.ApplyResources(Me.dgv_UnLCassetteB, "dgv_UnLCassetteB")
        Me.dgv_UnLCassetteB.AllowUserToAddRows = False
        Me.dgv_UnLCassetteB.AllowUserToDeleteRows = False
        Me.dgv_UnLCassetteB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_UnLCassetteB.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn5, Me.DataGridViewImageColumn3})
        Me.dgv_UnLCassetteB.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgv_UnLCassetteB.Name = "dgv_UnLCassetteB"
        Me.dgv_UnLCassetteB.RowHeadersVisible = False
        Me.dgv_UnLCassetteB.RowTemplate.Height = 24
        Me.ToolTip1.SetToolTip(Me.dgv_UnLCassetteB, resources.GetString("dgv_UnLCassetteB.ToolTip"))
        '
        'DataGridViewTextBoxColumn5
        '
        resources.ApplyResources(Me.DataGridViewTextBoxColumn5, "DataGridViewTextBoxColumn5")
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewImageColumn3
        '
        resources.ApplyResources(Me.DataGridViewImageColumn3, "DataGridViewImageColumn3")
        Me.DataGridViewImageColumn3.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn3.Name = "DataGridViewImageColumn3"
        Me.DataGridViewImageColumn3.ReadOnly = True
        Me.DataGridViewImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'grpUnloadCassetteA
        '
        resources.ApplyResources(Me.grpUnloadCassetteA, "grpUnloadCassetteA")
        Me.grpUnloadCassetteA.Controls.Add(Me.dgv_UnLCassetteA)
        Me.grpUnloadCassetteA.Name = "grpUnloadCassetteA"
        Me.grpUnloadCassetteA.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpUnloadCassetteA, resources.GetString("grpUnloadCassetteA.ToolTip"))
        '
        'dgv_UnLCassetteA
        '
        resources.ApplyResources(Me.dgv_UnLCassetteA, "dgv_UnLCassetteA")
        Me.dgv_UnLCassetteA.AllowUserToAddRows = False
        Me.dgv_UnLCassetteA.AllowUserToDeleteRows = False
        Me.dgv_UnLCassetteA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_UnLCassetteA.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewImageColumn2})
        Me.dgv_UnLCassetteA.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgv_UnLCassetteA.Name = "dgv_UnLCassetteA"
        Me.dgv_UnLCassetteA.RowHeadersVisible = False
        Me.dgv_UnLCassetteA.RowTemplate.Height = 24
        Me.ToolTip1.SetToolTip(Me.dgv_UnLCassetteA, resources.GetString("dgv_UnLCassetteA.ToolTip"))
        '
        'DataGridViewTextBoxColumn1
        '
        resources.ApplyResources(Me.DataGridViewTextBoxColumn1, "DataGridViewTextBoxColumn1")
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewImageColumn2
        '
        resources.ApplyResources(Me.DataGridViewImageColumn2, "DataGridViewImageColumn2")
        Me.DataGridViewImageColumn2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn2.Name = "DataGridViewImageColumn2"
        Me.DataGridViewImageColumn2.ReadOnly = True
        Me.DataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'grpLoadCassetteB
        '
        resources.ApplyResources(Me.grpLoadCassetteB, "grpLoadCassetteB")
        Me.grpLoadCassetteB.Controls.Add(Me.dgv_LCassetteB)
        Me.grpLoadCassetteB.Name = "grpLoadCassetteB"
        Me.grpLoadCassetteB.TabStop = False
        Me.grpLoadCassetteB.Tag = ""
        Me.ToolTip1.SetToolTip(Me.grpLoadCassetteB, resources.GetString("grpLoadCassetteB.ToolTip"))
        '
        'dgv_LCassetteB
        '
        resources.ApplyResources(Me.dgv_LCassetteB, "dgv_LCassetteB")
        Me.dgv_LCassetteB.AllowUserToAddRows = False
        Me.dgv_LCassetteB.AllowUserToDeleteRows = False
        Me.dgv_LCassetteB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_LCassetteB.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.DataGridViewImageColumn1, Me.DataGridViewTextBoxColumn4})
        Me.dgv_LCassetteB.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgv_LCassetteB.Name = "dgv_LCassetteB"
        Me.dgv_LCassetteB.RowHeadersVisible = False
        Me.dgv_LCassetteB.RowTemplate.Height = 24
        Me.ToolTip1.SetToolTip(Me.dgv_LCassetteB, resources.GetString("dgv_LCassetteB.ToolTip"))
        '
        'DataGridViewTextBoxColumn3
        '
        resources.ApplyResources(Me.DataGridViewTextBoxColumn3, "DataGridViewTextBoxColumn3")
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewImageColumn1
        '
        resources.ApplyResources(Me.DataGridViewImageColumn1, "DataGridViewImageColumn1")
        Me.DataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ReadOnly = True
        Me.DataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DataGridViewTextBoxColumn4
        '
        resources.ApplyResources(Me.DataGridViewTextBoxColumn4, "DataGridViewTextBoxColumn4")
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'grpLoadCassetteA
        '
        resources.ApplyResources(Me.grpLoadCassetteA, "grpLoadCassetteA")
        Me.grpLoadCassetteA.Controls.Add(Me.dgv_LCassetteA)
        Me.grpLoadCassetteA.Name = "grpLoadCassetteA"
        Me.grpLoadCassetteA.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpLoadCassetteA, resources.GetString("grpLoadCassetteA.ToolTip"))
        '
        'dgv_LCassetteA
        '
        resources.ApplyResources(Me.dgv_LCassetteA, "dgv_LCassetteA")
        Me.dgv_LCassetteA.AllowUserToAddRows = False
        Me.dgv_LCassetteA.AllowUserToDeleteRows = False
        Me.dgv_LCassetteA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_LCassetteA.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column_NO, Me.Column_Pic, Me.Column_Name})
        Me.dgv_LCassetteA.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgv_LCassetteA.Name = "dgv_LCassetteA"
        Me.dgv_LCassetteA.RowHeadersVisible = False
        Me.dgv_LCassetteA.RowTemplate.Height = 24
        Me.ToolTip1.SetToolTip(Me.dgv_LCassetteA, resources.GetString("dgv_LCassetteA.ToolTip"))
        '
        'Column_NO
        '
        resources.ApplyResources(Me.Column_NO, "Column_NO")
        Me.Column_NO.Name = "Column_NO"
        Me.Column_NO.ReadOnly = True
        Me.Column_NO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'Column_Pic
        '
        resources.ApplyResources(Me.Column_Pic, "Column_Pic")
        Me.Column_Pic.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.Column_Pic.Name = "Column_Pic"
        Me.Column_Pic.ReadOnly = True
        Me.Column_Pic.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'Column_Name
        '
        resources.ApplyResources(Me.Column_Name, "Column_Name")
        Me.Column_Name.Name = "Column_Name"
        Me.Column_Name.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'txtGlueID
        '
        resources.ApplyResources(Me.txtGlueID, "txtGlueID")
        Me.txtGlueID.Name = "txtGlueID"
        Me.ToolTip1.SetToolTip(Me.txtGlueID, resources.GetString("txtGlueID.ToolTip"))
        '
        'lblGlueID
        '
        resources.ApplyResources(Me.lblGlueID, "lblGlueID")
        Me.lblGlueID.Name = "lblGlueID"
        Me.ToolTip1.SetToolTip(Me.lblGlueID, resources.GetString("lblGlueID.ToolTip"))
        '
        'txtLotID
        '
        resources.ApplyResources(Me.txtLotID, "txtLotID")
        Me.txtLotID.Name = "txtLotID"
        Me.ToolTip1.SetToolTip(Me.txtLotID, resources.GetString("txtLotID.ToolTip"))
        '
        'lblLotID
        '
        resources.ApplyResources(Me.lblLotID, "lblLotID")
        Me.lblLotID.BackColor = System.Drawing.Color.Transparent
        Me.lblLotID.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLotID.Name = "lblLotID"
        Me.ToolTip1.SetToolTip(Me.lblLotID, resources.GetString("lblLotID.ToolTip"))
        '
        'btnMacAOpenMap
        '
        resources.ApplyResources(Me.btnMacAOpenMap, "btnMacAOpenMap")
        Me.btnMacAOpenMap.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.OpenFile
        Me.btnMacAOpenMap.Name = "btnMacAOpenMap"
        Me.ToolTip1.SetToolTip(Me.btnMacAOpenMap, resources.GetString("btnMacAOpenMap.ToolTip"))
        Me.btnMacAOpenMap.UseVisualStyleBackColor = True
        '
        'btnMacBOpenMap
        '
        resources.ApplyResources(Me.btnMacBOpenMap, "btnMacBOpenMap")
        Me.btnMacBOpenMap.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.OpenFile
        Me.btnMacBOpenMap.Name = "btnMacBOpenMap"
        Me.ToolTip1.SetToolTip(Me.btnMacBOpenMap, resources.GetString("btnMacBOpenMap.ToolTip"))
        Me.btnMacBOpenMap.UseVisualStyleBackColor = True
        '
        'lblMachineAWafer
        '
        resources.ApplyResources(Me.lblMachineAWafer, "lblMachineAWafer")
        Me.lblMachineAWafer.Image = Global.WindowsApplication1.My.Resources.Resources.WaferFrame
        Me.lblMachineAWafer.Name = "lblMachineAWafer"
        Me.ToolTip1.SetToolTip(Me.lblMachineAWafer, resources.GetString("lblMachineAWafer.ToolTip"))
        '
        'lblMachineBWafer
        '
        resources.ApplyResources(Me.lblMachineBWafer, "lblMachineBWafer")
        Me.lblMachineBWafer.Image = Global.WindowsApplication1.My.Resources.Resources.WaferFrame
        Me.lblMachineBWafer.Name = "lblMachineBWafer"
        Me.ToolTip1.SetToolTip(Me.lblMachineBWafer, resources.GetString("lblMachineBWafer.ToolTip"))
        '
        'btnTest
        '
        resources.ApplyResources(Me.btnTest, "btnTest")
        Me.btnTest.Name = "btnTest"
        Me.ToolTip1.SetToolTip(Me.btnTest, resources.GetString("btnTest.ToolTip"))
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cbManualMapData)
        Me.GroupBox1.Controls.Add(Me.tbMapDataB)
        Me.GroupBox1.Controls.Add(Me.tbMapDataA)
        Me.GroupBox1.Controls.Add(Me.btnMacBOpenMap)
        Me.GroupBox1.Controls.Add(Me.btnMacAOpenMap)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        Me.ToolTip1.SetToolTip(Me.Label2, resources.GetString("Label2.ToolTip"))
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'cbManualMapData
        '
        resources.ApplyResources(Me.cbManualMapData, "cbManualMapData")
        Me.cbManualMapData.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.cbManualMapData.Name = "cbManualMapData"
        Me.ToolTip1.SetToolTip(Me.cbManualMapData, resources.GetString("cbManualMapData.ToolTip"))
        Me.cbManualMapData.UseVisualStyleBackColor = False
        '
        'tbMapDataB
        '
        resources.ApplyResources(Me.tbMapDataB, "tbMapDataB")
        Me.tbMapDataB.Name = "tbMapDataB"
        Me.tbMapDataB.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.tbMapDataB, resources.GetString("tbMapDataB.ToolTip"))
        '
        'tbMapDataA
        '
        resources.ApplyResources(Me.tbMapDataA, "tbMapDataA")
        Me.tbMapDataA.Name = "tbMapDataA"
        Me.tbMapDataA.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.tbMapDataA, resources.GetString("tbMapDataA.ToolTip"))
        '
        'btnCleanBWaferId
        '
        resources.ApplyResources(Me.btnCleanBWaferId, "btnCleanBWaferId")
        Me.btnCleanBWaferId.Name = "btnCleanBWaferId"
        Me.ToolTip1.SetToolTip(Me.btnCleanBWaferId, resources.GetString("btnCleanBWaferId.ToolTip"))
        Me.btnCleanBWaferId.UseVisualStyleBackColor = True
        '
        'btnCleanAWaferId
        '
        resources.ApplyResources(Me.btnCleanAWaferId, "btnCleanAWaferId")
        Me.btnCleanAWaferId.Name = "btnCleanAWaferId"
        Me.ToolTip1.SetToolTip(Me.btnCleanAWaferId, resources.GetString("btnCleanAWaferId.ToolTip"))
        Me.btnCleanAWaferId.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Light-Black01.png")
        Me.ImageList1.Images.SetKeyName(1, "Light-Green01.png")
        Me.ImageList1.Images.SetKeyName(2, "Light-Red01.png")
        '
        'ucDCSW800AQStatus
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.btnCleanBWaferId)
        Me.Controls.Add(Me.btnCleanAWaferId)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.lblMachineBWafer)
        Me.Controls.Add(Me.lblMachineAWafer)
        Me.Controls.Add(Me.txtLotID)
        Me.Controls.Add(Me.lblLotID)
        Me.Controls.Add(Me.txtGlueID)
        Me.Controls.Add(Me.lblGlueID)
        Me.Controls.Add(Me.lblMachineBStatus)
        Me.Controls.Add(Me.lblMachineAStatus)
        Me.Controls.Add(Me.picConveyor)
        Me.Controls.Add(Me.grpUnloadCassetteB)
        Me.Controls.Add(Me.grpUnloadCassetteA)
        Me.Controls.Add(Me.grpLoadCassetteB)
        Me.Controls.Add(Me.grpLoadCassetteA)
        Me.Name = "ucDCSW800AQStatus"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        CType(Me.picConveyor,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpUnloadCassetteB.ResumeLayout(false)
        CType(Me.dgv_UnLCassetteB,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpUnloadCassetteA.ResumeLayout(false)
        CType(Me.dgv_UnLCassetteA,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpLoadCassetteB.ResumeLayout(false)
        CType(Me.dgv_LCassetteB,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpLoadCassetteA.ResumeLayout(false)
        CType(Me.dgv_LCassetteA,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents lblMachineBStatus As System.Windows.Forms.Label
    Friend WithEvents lblMachineAStatus As System.Windows.Forms.Label
    Friend WithEvents picConveyor As System.Windows.Forms.PictureBox
    Friend WithEvents grpUnloadCassetteB As System.Windows.Forms.GroupBox
    Friend WithEvents dgv_UnLCassetteB As System.Windows.Forms.DataGridView
    Friend WithEvents grpUnloadCassetteA As System.Windows.Forms.GroupBox
    Friend WithEvents dgv_UnLCassetteA As System.Windows.Forms.DataGridView
    Friend WithEvents grpLoadCassetteB As System.Windows.Forms.GroupBox
    Friend WithEvents dgv_LCassetteB As System.Windows.Forms.DataGridView
    Friend WithEvents grpLoadCassetteA As System.Windows.Forms.GroupBox
    Friend WithEvents dgv_LCassetteA As System.Windows.Forms.DataGridView
    Friend WithEvents txtGlueID As System.Windows.Forms.TextBox
    Friend WithEvents lblGlueID As System.Windows.Forms.Label
    Friend WithEvents txtLotID As System.Windows.Forms.TextBox
    Friend WithEvents lblLotID As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblMachineAWafer As System.Windows.Forms.Label
    Friend WithEvents lblMachineBWafer As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewImageColumn2 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewImageColumn3 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Column_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column_Pic As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Column_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbManualMapData As System.Windows.Forms.CheckBox
    Friend WithEvents btnMacAOpenMap As System.Windows.Forms.Button
    Friend WithEvents btnMacBOpenMap As System.Windows.Forms.Button
    Friend WithEvents tbMapDataB As System.Windows.Forms.TextBox
    Friend WithEvents tbMapDataA As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCleanBWaferId As System.Windows.Forms.Button
    Friend WithEvents btnCleanAWaferId As System.Windows.Forms.Button

End Class

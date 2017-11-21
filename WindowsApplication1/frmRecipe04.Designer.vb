<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRecipe04
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRecipe04))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grpStep = New System.Windows.Forms.GroupBox()
        Me.dgvStep = New System.Windows.Forms.DataGridView()
        Me.OFDLoadRecipe = New System.Windows.Forms.OpenFileDialog()
        Me.lstPatternID = New System.Windows.Forms.ListBox()
        Me.lblFile = New System.Windows.Forms.Label()
        Me.lblPatternEditor = New System.Windows.Forms.Label()
        Me.lblRoundLoopEditor = New System.Windows.Forms.Label()
        Me.lblOthers = New System.Windows.Forms.Label()
        Me.SFDSaveRecipe = New System.Windows.Forms.SaveFileDialog()
        Me.lblStepEditor = New System.Windows.Forms.Label()
        Me.lstRoundNo = New System.Windows.Forms.ListBox()
        Me.treePattern = New System.Windows.Forms.TreeView()
        Me.tabView = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.picPcsSingleGraph = New System.Windows.Forms.PictureBox()
        Me.lblNodeEditor = New System.Windows.Forms.Label()
        Me.btnContiStart = New System.Windows.Forms.Button()
        Me.btnContiEnd = New System.Windows.Forms.Button()
        Me.btnExtendOn = New System.Windows.Forms.Button()
        Me.btnExtendOff = New System.Windows.Forms.Button()
        Me.chkNodeConnect = New System.Windows.Forms.CheckBox()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuFile = New System.Windows.Forms.MenuItem()
        Me.mnuFileCreate = New System.Windows.Forms.MenuItem()
        Me.mnuFileOpen = New System.Windows.Forms.MenuItem()
        Me.mnuFileSave = New System.Windows.Forms.MenuItem()
        Me.mnuFileSaveAs = New System.Windows.Forms.MenuItem()
        Me.mnuFileImport = New System.Windows.Forms.MenuItem()
        Me.mnuFileExport = New System.Windows.Forms.MenuItem()
        Me.MenuItem25 = New System.Windows.Forms.MenuItem()
        Me.mnuExit = New System.Windows.Forms.MenuItem()
        Me.mnuPattern = New System.Windows.Forms.MenuItem()
        Me.mnuPatternAdd = New System.Windows.Forms.MenuItem()
        Me.mnuPatternDelete = New System.Windows.Forms.MenuItem()
        Me.mnuPatternCopy = New System.Windows.Forms.MenuItem()
        Me.mnuPatternPaste = New System.Windows.Forms.MenuItem()
        Me.mnuPatternRename = New System.Windows.Forms.MenuItem()
        Me.mnuRotae = New System.Windows.Forms.MenuItem()
        Me.mnuRotae180 = New System.Windows.Forms.MenuItem()
        Me.mnuMirrorX = New System.Windows.Forms.MenuItem()
        Me.mnuMirrorY = New System.Windows.Forms.MenuItem()
        Me.mnuNode = New System.Windows.Forms.MenuItem()
        Me.mnuNodeAdd = New System.Windows.Forms.MenuItem()
        Me.mnuNodeDelete = New System.Windows.Forms.MenuItem()
        Me.mnuNodeCopy = New System.Windows.Forms.MenuItem()
        Me.mnuNodePaste = New System.Windows.Forms.MenuItem()
        Me.MenuItem21 = New System.Windows.Forms.MenuItem()
        Me.mnuNodeFIDs = New System.Windows.Forms.MenuItem()
        Me.mnuNodeArray = New System.Windows.Forms.MenuItem()
        Me.mnuNodeHeight = New System.Windows.Forms.MenuItem()
        Me.mnuRound = New System.Windows.Forms.MenuItem()
        Me.mnuRoundAdd = New System.Windows.Forms.MenuItem()
        Me.mnuRoundDelete = New System.Windows.Forms.MenuItem()
        Me.mnuRoundCopy = New System.Windows.Forms.MenuItem()
        Me.mnuRoundPaste = New System.Windows.Forms.MenuItem()
        Me.mnuRoundMoveUp = New System.Windows.Forms.MenuItem()
        Me.mnuRoundMoveDown = New System.Windows.Forms.MenuItem()
        Me.mnuProcessTime = New System.Windows.Forms.MenuItem()
        Me.mnuStep = New System.Windows.Forms.MenuItem()
        Me.mnuStepAdd = New System.Windows.Forms.MenuItem()
        Me.mnuStepDelete = New System.Windows.Forms.MenuItem()
        Me.mnuStepCopy = New System.Windows.Forms.MenuItem()
        Me.mnuStepPaste = New System.Windows.Forms.MenuItem()
        Me.mnuStepMoveUp = New System.Windows.Forms.MenuItem()
        Me.mnuStepMoveDown = New System.Windows.Forms.MenuItem()
        Me.MenuItem36 = New System.Windows.Forms.MenuItem()
        Me.mnuStepParameter = New System.Windows.Forms.MenuItem()
        Me.mnuOthers = New System.Windows.Forms.MenuItem()
        Me.mnuBalance = New System.Windows.Forms.MenuItem()
        Me.mnuPark = New System.Windows.Forms.MenuItem()
        Me.mnuLoadA = New System.Windows.Forms.MenuItem()
        Me.mnuLoadB = New System.Windows.Forms.MenuItem()
        Me.mnuUnloadA = New System.Windows.Forms.MenuItem()
        Me.mnuUnloadB = New System.Windows.Forms.MenuItem()
        Me.mnuMAP = New System.Windows.Forms.MenuItem()
        Me.mnuRun = New System.Windows.Forms.MenuItem()
        Me.mnuRunRun = New System.Windows.Forms.MenuItem()
        Me.mnuRunCCDRun = New System.Windows.Forms.MenuItem()
        Me.mnuRunDryRun = New System.Windows.Forms.MenuItem()
        Me.mnuRunPause = New System.Windows.Forms.MenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnAlignUnlock = New System.Windows.Forms.Button()
        Me.btnNodeMap = New System.Windows.Forms.Button()
        Me.btnMachineBBalance = New System.Windows.Forms.Button()
        Me.btnWait = New System.Windows.Forms.Button()
        Me.btnLoadB = New System.Windows.Forms.Button()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnUnload = New System.Windows.Forms.Button()
        Me.btnMove3D = New System.Windows.Forms.Button()
        Me.btnNodeAdd = New System.Windows.Forms.Button()
        Me.btnCircle = New System.Windows.Forms.Button()
        Me.btnUnloadB = New System.Windows.Forms.Button()
        Me.btnArc = New System.Windows.Forms.Button()
        Me.btnNodeDelete = New System.Windows.Forms.Button()
        Me.btnLine3D = New System.Windows.Forms.Button()
        Me.btnDots3D = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnArray = New System.Windows.Forms.Button()
        Me.btnInspect = New System.Windows.Forms.Button()
        Me.btnAlign = New System.Windows.Forms.Button()
        Me.btnCreateRecipe = New System.Windows.Forms.Button()
        Me.btnStepBasicEdit = New System.Windows.Forms.Button()
        Me.btnPatternDelete = New System.Windows.Forms.Button()
        Me.btnStepDelete = New System.Windows.Forms.Button()
        Me.btnRoundAdd = New System.Windows.Forms.Button()
        Me.btnRoundDelete = New System.Windows.Forms.Button()
        Me.btnRoundUp = New System.Windows.Forms.Button()
        Me.btnRoundDown = New System.Windows.Forms.Button()
        Me.btnLock = New System.Windows.Forms.Button()
        Me.btnSaveRecipe = New System.Windows.Forms.Button()
        Me.btnLoadRecipe = New System.Windows.Forms.Button()
        Me.btnHeight = New System.Windows.Forms.Button()
        Me.btnPark = New System.Windows.Forms.Button()
        Me.btnCCDRun = New System.Windows.Forms.Button()
        Me.btnPause = New System.Windows.Forms.Button()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.btnMachineABalance = New System.Windows.Forms.Button()
        Me.btnDryRun = New System.Windows.Forms.Button()
        Me.btnStepUp = New System.Windows.Forms.Button()
        Me.btnPatternAdd = New System.Windows.Forms.Button()
        Me.btnStepDown = New System.Windows.Forms.Button()
        Me.btnAddArray = New System.Windows.Forms.Button()
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.FolderBrowserImport = New System.Windows.Forms.FolderBrowserDialog()
        Me.FolderBrowserExport = New System.Windows.Forms.FolderBrowserDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.grpStep.SuspendLayout()
        CType(Me.dgvStep, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabView.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.picPcsSingleGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpStep
        '
        resources.ApplyResources(Me.grpStep, "grpStep")
        Me.grpStep.Controls.Add(Me.dgvStep)
        Me.grpStep.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpStep.Name = "grpStep"
        Me.grpStep.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpStep, resources.GetString("grpStep.ToolTip"))
        '
        'dgvStep
        '
        resources.ApplyResources(Me.dgvStep, "dgvStep")
        Me.dgvStep.AllowUserToAddRows = False
        Me.dgvStep.AllowUserToDeleteRows = False
        Me.dgvStep.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgvStep.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvStep.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvStep.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvStep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStep.MultiSelect = False
        Me.dgvStep.Name = "dgvStep"
        Me.dgvStep.ReadOnly = True
        Me.dgvStep.RowHeadersVisible = False
        DataGridViewCellStyle3.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText
        Me.dgvStep.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvStep.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("微軟正黑體", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.dgvStep.RowTemplate.Height = 24
        Me.dgvStep.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ToolTip1.SetToolTip(Me.dgvStep, resources.GetString("dgvStep.ToolTip"))
        '
        'OFDLoadRecipe
        '
        Me.OFDLoadRecipe.FileName = "OpenFileDialog1"
        resources.ApplyResources(Me.OFDLoadRecipe, "OFDLoadRecipe")
        '
        'lstPatternID
        '
        resources.ApplyResources(Me.lstPatternID, "lstPatternID")
        Me.lstPatternID.BackColor = System.Drawing.Color.White
        Me.lstPatternID.FormattingEnabled = True
        Me.lstPatternID.Name = "lstPatternID"
        Me.ToolTip1.SetToolTip(Me.lstPatternID, resources.GetString("lstPatternID.ToolTip"))
        '
        'lblFile
        '
        resources.ApplyResources(Me.lblFile, "lblFile")
        Me.lblFile.Name = "lblFile"
        Me.ToolTip1.SetToolTip(Me.lblFile, resources.GetString("lblFile.ToolTip"))
        '
        'lblPatternEditor
        '
        resources.ApplyResources(Me.lblPatternEditor, "lblPatternEditor")
        Me.lblPatternEditor.Name = "lblPatternEditor"
        Me.ToolTip1.SetToolTip(Me.lblPatternEditor, resources.GetString("lblPatternEditor.ToolTip"))
        '
        'lblRoundLoopEditor
        '
        resources.ApplyResources(Me.lblRoundLoopEditor, "lblRoundLoopEditor")
        Me.lblRoundLoopEditor.Name = "lblRoundLoopEditor"
        Me.ToolTip1.SetToolTip(Me.lblRoundLoopEditor, resources.GetString("lblRoundLoopEditor.ToolTip"))
        '
        'lblOthers
        '
        resources.ApplyResources(Me.lblOthers, "lblOthers")
        Me.lblOthers.Name = "lblOthers"
        Me.ToolTip1.SetToolTip(Me.lblOthers, resources.GetString("lblOthers.ToolTip"))
        '
        'SFDSaveRecipe
        '
        resources.ApplyResources(Me.SFDSaveRecipe, "SFDSaveRecipe")
        '
        'lblStepEditor
        '
        resources.ApplyResources(Me.lblStepEditor, "lblStepEditor")
        Me.lblStepEditor.Name = "lblStepEditor"
        Me.ToolTip1.SetToolTip(Me.lblStepEditor, resources.GetString("lblStepEditor.ToolTip"))
        '
        'lstRoundNo
        '
        resources.ApplyResources(Me.lstRoundNo, "lstRoundNo")
        Me.lstRoundNo.BackColor = System.Drawing.Color.White
        Me.lstRoundNo.FormattingEnabled = True
        Me.lstRoundNo.Name = "lstRoundNo"
        Me.ToolTip1.SetToolTip(Me.lstRoundNo, resources.GetString("lstRoundNo.ToolTip"))
        '
        'treePattern
        '
        resources.ApplyResources(Me.treePattern, "treePattern")
        Me.treePattern.Name = "treePattern"
        Me.treePattern.ShowNodeToolTips = True
        Me.ToolTip1.SetToolTip(Me.treePattern, resources.GetString("treePattern.ToolTip"))
        '
        'tabView
        '
        resources.ApplyResources(Me.tabView, "tabView")
        Me.tabView.Controls.Add(Me.TabPage1)
        Me.tabView.Controls.Add(Me.TabPage2)
        Me.tabView.Controls.Add(Me.TabPage3)
        Me.tabView.Name = "tabView"
        Me.tabView.SelectedIndex = 0
        Me.ToolTip1.SetToolTip(Me.tabView, resources.GetString("tabView.ToolTip"))
        '
        'TabPage1
        '
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.UcDisplay1)
        Me.TabPage1.Name = "TabPage1"
        Me.ToolTip1.SetToolTip(Me.TabPage1, resources.GetString("TabPage1.ToolTip"))
        '
        'UcDisplay1
        '
        resources.ApplyResources(Me.UcDisplay1, "UcDisplay1")
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UcDisplay1.Name = "UcDisplay1"
        Me.ToolTip1.SetToolTip(Me.UcDisplay1, resources.GetString("UcDisplay1.ToolTip"))
        '
        'TabPage2
        '
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Controls.Add(Me.treePattern)
        Me.TabPage2.Name = "TabPage2"
        Me.ToolTip1.SetToolTip(Me.TabPage2, resources.GetString("TabPage2.ToolTip"))
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        resources.ApplyResources(Me.TabPage3, "TabPage3")
        Me.TabPage3.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage3.Controls.Add(Me.picPcsSingleGraph)
        Me.TabPage3.Name = "TabPage3"
        Me.ToolTip1.SetToolTip(Me.TabPage3, resources.GetString("TabPage3.ToolTip"))
        '
        'picPcsSingleGraph
        '
        resources.ApplyResources(Me.picPcsSingleGraph, "picPcsSingleGraph")
        Me.picPcsSingleGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPcsSingleGraph.Name = "picPcsSingleGraph"
        Me.picPcsSingleGraph.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picPcsSingleGraph, resources.GetString("picPcsSingleGraph.ToolTip"))
        '
        'lblNodeEditor
        '
        resources.ApplyResources(Me.lblNodeEditor, "lblNodeEditor")
        Me.lblNodeEditor.Name = "lblNodeEditor"
        Me.ToolTip1.SetToolTip(Me.lblNodeEditor, resources.GetString("lblNodeEditor.ToolTip"))
        '
        'btnContiStart
        '
        resources.ApplyResources(Me.btnContiStart, "btnContiStart")
        Me.btnContiStart.FlatAppearance.BorderSize = 0
        Me.btnContiStart.Name = "btnContiStart"
        Me.ToolTip1.SetToolTip(Me.btnContiStart, resources.GetString("btnContiStart.ToolTip"))
        Me.btnContiStart.UseVisualStyleBackColor = True
        '
        'btnContiEnd
        '
        resources.ApplyResources(Me.btnContiEnd, "btnContiEnd")
        Me.btnContiEnd.FlatAppearance.BorderSize = 0
        Me.btnContiEnd.Name = "btnContiEnd"
        Me.ToolTip1.SetToolTip(Me.btnContiEnd, resources.GetString("btnContiEnd.ToolTip"))
        Me.btnContiEnd.UseVisualStyleBackColor = True
        '
        'btnExtendOn
        '
        resources.ApplyResources(Me.btnExtendOn, "btnExtendOn")
        Me.btnExtendOn.FlatAppearance.BorderSize = 0
        Me.btnExtendOn.Name = "btnExtendOn"
        Me.ToolTip1.SetToolTip(Me.btnExtendOn, resources.GetString("btnExtendOn.ToolTip"))
        Me.btnExtendOn.UseVisualStyleBackColor = True
        '
        'btnExtendOff
        '
        resources.ApplyResources(Me.btnExtendOff, "btnExtendOff")
        Me.btnExtendOff.FlatAppearance.BorderSize = 0
        Me.btnExtendOff.Name = "btnExtendOff"
        Me.ToolTip1.SetToolTip(Me.btnExtendOff, resources.GetString("btnExtendOff.ToolTip"))
        Me.btnExtendOff.UseVisualStyleBackColor = True
        '
        'chkNodeConnect
        '
        resources.ApplyResources(Me.chkNodeConnect, "chkNodeConnect")
        Me.chkNodeConnect.Name = "chkNodeConnect"
        Me.ToolTip1.SetToolTip(Me.chkNodeConnect, resources.GetString("chkNodeConnect.ToolTip"))
        Me.chkNodeConnect.UseVisualStyleBackColor = True
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile, Me.mnuPattern, Me.mnuNode, Me.mnuRound, Me.mnuStep, Me.mnuOthers, Me.mnuRun})
        resources.ApplyResources(Me.MainMenu1, "MainMenu1")
        '
        'mnuFile
        '
        resources.ApplyResources(Me.mnuFile, "mnuFile")
        Me.mnuFile.Index = 0
        Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFileCreate, Me.mnuFileOpen, Me.mnuFileSave, Me.mnuFileSaveAs, Me.mnuFileImport, Me.mnuFileExport, Me.MenuItem25, Me.mnuExit})
        '
        'mnuFileCreate
        '
        resources.ApplyResources(Me.mnuFileCreate, "mnuFileCreate")
        Me.mnuFileCreate.Index = 0
        '
        'mnuFileOpen
        '
        resources.ApplyResources(Me.mnuFileOpen, "mnuFileOpen")
        Me.mnuFileOpen.Index = 1
        '
        'mnuFileSave
        '
        resources.ApplyResources(Me.mnuFileSave, "mnuFileSave")
        Me.mnuFileSave.Index = 2
        '
        'mnuFileSaveAs
        '
        resources.ApplyResources(Me.mnuFileSaveAs, "mnuFileSaveAs")
        Me.mnuFileSaveAs.Index = 3
        '
        'mnuFileImport
        '
        resources.ApplyResources(Me.mnuFileImport, "mnuFileImport")
        Me.mnuFileImport.Index = 4
        '
        'mnuFileExport
        '
        resources.ApplyResources(Me.mnuFileExport, "mnuFileExport")
        Me.mnuFileExport.Index = 5
        '
        'MenuItem25
        '
        resources.ApplyResources(Me.MenuItem25, "MenuItem25")
        Me.MenuItem25.Index = 6
        '
        'mnuExit
        '
        resources.ApplyResources(Me.mnuExit, "mnuExit")
        Me.mnuExit.Index = 7
        '
        'mnuPattern
        '
        resources.ApplyResources(Me.mnuPattern, "mnuPattern")
        Me.mnuPattern.Index = 1
        Me.mnuPattern.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuPatternAdd, Me.mnuPatternDelete, Me.mnuPatternCopy, Me.mnuPatternPaste, Me.mnuPatternRename, Me.mnuRotae})
        '
        'mnuPatternAdd
        '
        resources.ApplyResources(Me.mnuPatternAdd, "mnuPatternAdd")
        Me.mnuPatternAdd.Index = 0
        '
        'mnuPatternDelete
        '
        resources.ApplyResources(Me.mnuPatternDelete, "mnuPatternDelete")
        Me.mnuPatternDelete.Index = 1
        '
        'mnuPatternCopy
        '
        resources.ApplyResources(Me.mnuPatternCopy, "mnuPatternCopy")
        Me.mnuPatternCopy.Index = 2
        '
        'mnuPatternPaste
        '
        resources.ApplyResources(Me.mnuPatternPaste, "mnuPatternPaste")
        Me.mnuPatternPaste.Index = 3
        '
        'mnuPatternRename
        '
        resources.ApplyResources(Me.mnuPatternRename, "mnuPatternRename")
        Me.mnuPatternRename.Index = 4
        '
        'mnuRotae
        '
        resources.ApplyResources(Me.mnuRotae, "mnuRotae")
        Me.mnuRotae.Index = 5
        Me.mnuRotae.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuRotae180, Me.mnuMirrorX, Me.mnuMirrorY})
        '
        'mnuRotae180
        '
        resources.ApplyResources(Me.mnuRotae180, "mnuRotae180")
        Me.mnuRotae180.Index = 0
        '
        'mnuMirrorX
        '
        resources.ApplyResources(Me.mnuMirrorX, "mnuMirrorX")
        Me.mnuMirrorX.Index = 1
        '
        'mnuMirrorY
        '
        resources.ApplyResources(Me.mnuMirrorY, "mnuMirrorY")
        Me.mnuMirrorY.Index = 2
        '
        'mnuNode
        '
        resources.ApplyResources(Me.mnuNode, "mnuNode")
        Me.mnuNode.Index = 2
        Me.mnuNode.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuNodeAdd, Me.mnuNodeDelete, Me.mnuNodeCopy, Me.mnuNodePaste, Me.MenuItem21, Me.mnuNodeFIDs, Me.mnuNodeArray, Me.mnuNodeHeight})
        '
        'mnuNodeAdd
        '
        resources.ApplyResources(Me.mnuNodeAdd, "mnuNodeAdd")
        Me.mnuNodeAdd.Index = 0
        '
        'mnuNodeDelete
        '
        resources.ApplyResources(Me.mnuNodeDelete, "mnuNodeDelete")
        Me.mnuNodeDelete.Index = 1
        '
        'mnuNodeCopy
        '
        resources.ApplyResources(Me.mnuNodeCopy, "mnuNodeCopy")
        Me.mnuNodeCopy.Index = 2
        '
        'mnuNodePaste
        '
        resources.ApplyResources(Me.mnuNodePaste, "mnuNodePaste")
        Me.mnuNodePaste.Index = 3
        '
        'MenuItem21
        '
        resources.ApplyResources(Me.MenuItem21, "MenuItem21")
        Me.MenuItem21.Index = 4
        '
        'mnuNodeFIDs
        '
        resources.ApplyResources(Me.mnuNodeFIDs, "mnuNodeFIDs")
        Me.mnuNodeFIDs.Index = 5
        '
        'mnuNodeArray
        '
        resources.ApplyResources(Me.mnuNodeArray, "mnuNodeArray")
        Me.mnuNodeArray.Index = 6
        '
        'mnuNodeHeight
        '
        resources.ApplyResources(Me.mnuNodeHeight, "mnuNodeHeight")
        Me.mnuNodeHeight.Index = 7
        '
        'mnuRound
        '
        resources.ApplyResources(Me.mnuRound, "mnuRound")
        Me.mnuRound.Index = 3
        Me.mnuRound.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuRoundAdd, Me.mnuRoundDelete, Me.mnuRoundCopy, Me.mnuRoundPaste, Me.mnuRoundMoveUp, Me.mnuRoundMoveDown, Me.mnuProcessTime})
        '
        'mnuRoundAdd
        '
        resources.ApplyResources(Me.mnuRoundAdd, "mnuRoundAdd")
        Me.mnuRoundAdd.Index = 0
        '
        'mnuRoundDelete
        '
        resources.ApplyResources(Me.mnuRoundDelete, "mnuRoundDelete")
        Me.mnuRoundDelete.Index = 1
        '
        'mnuRoundCopy
        '
        resources.ApplyResources(Me.mnuRoundCopy, "mnuRoundCopy")
        Me.mnuRoundCopy.Index = 2
        '
        'mnuRoundPaste
        '
        resources.ApplyResources(Me.mnuRoundPaste, "mnuRoundPaste")
        Me.mnuRoundPaste.Index = 3
        '
        'mnuRoundMoveUp
        '
        resources.ApplyResources(Me.mnuRoundMoveUp, "mnuRoundMoveUp")
        Me.mnuRoundMoveUp.Index = 4
        '
        'mnuRoundMoveDown
        '
        resources.ApplyResources(Me.mnuRoundMoveDown, "mnuRoundMoveDown")
        Me.mnuRoundMoveDown.Index = 5
        '
        'mnuProcessTime
        '
        resources.ApplyResources(Me.mnuProcessTime, "mnuProcessTime")
        Me.mnuProcessTime.Index = 6
        '
        'mnuStep
        '
        resources.ApplyResources(Me.mnuStep, "mnuStep")
        Me.mnuStep.Index = 4
        Me.mnuStep.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuStepAdd, Me.mnuStepDelete, Me.mnuStepCopy, Me.mnuStepPaste, Me.mnuStepMoveUp, Me.mnuStepMoveDown, Me.MenuItem36, Me.mnuStepParameter})
        '
        'mnuStepAdd
        '
        resources.ApplyResources(Me.mnuStepAdd, "mnuStepAdd")
        Me.mnuStepAdd.Index = 0
        '
        'mnuStepDelete
        '
        resources.ApplyResources(Me.mnuStepDelete, "mnuStepDelete")
        Me.mnuStepDelete.Index = 1
        '
        'mnuStepCopy
        '
        resources.ApplyResources(Me.mnuStepCopy, "mnuStepCopy")
        Me.mnuStepCopy.Index = 2
        '
        'mnuStepPaste
        '
        resources.ApplyResources(Me.mnuStepPaste, "mnuStepPaste")
        Me.mnuStepPaste.Index = 3
        '
        'mnuStepMoveUp
        '
        resources.ApplyResources(Me.mnuStepMoveUp, "mnuStepMoveUp")
        Me.mnuStepMoveUp.Index = 4
        '
        'mnuStepMoveDown
        '
        resources.ApplyResources(Me.mnuStepMoveDown, "mnuStepMoveDown")
        Me.mnuStepMoveDown.Index = 5
        '
        'MenuItem36
        '
        resources.ApplyResources(Me.MenuItem36, "MenuItem36")
        Me.MenuItem36.Index = 6
        '
        'mnuStepParameter
        '
        resources.ApplyResources(Me.mnuStepParameter, "mnuStepParameter")
        Me.mnuStepParameter.Index = 7
        '
        'mnuOthers
        '
        resources.ApplyResources(Me.mnuOthers, "mnuOthers")
        Me.mnuOthers.Index = 5
        Me.mnuOthers.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuBalance, Me.mnuPark, Me.mnuLoadA, Me.mnuLoadB, Me.mnuUnloadA, Me.mnuUnloadB, Me.mnuMAP})
        '
        'mnuBalance
        '
        resources.ApplyResources(Me.mnuBalance, "mnuBalance")
        Me.mnuBalance.Index = 0
        '
        'mnuPark
        '
        resources.ApplyResources(Me.mnuPark, "mnuPark")
        Me.mnuPark.Index = 1
        '
        'mnuLoadA
        '
        resources.ApplyResources(Me.mnuLoadA, "mnuLoadA")
        Me.mnuLoadA.Index = 2
        '
        'mnuLoadB
        '
        resources.ApplyResources(Me.mnuLoadB, "mnuLoadB")
        Me.mnuLoadB.Index = 3
        '
        'mnuUnloadA
        '
        resources.ApplyResources(Me.mnuUnloadA, "mnuUnloadA")
        Me.mnuUnloadA.Index = 4
        '
        'mnuUnloadB
        '
        resources.ApplyResources(Me.mnuUnloadB, "mnuUnloadB")
        Me.mnuUnloadB.Index = 5
        '
        'mnuMAP
        '
        resources.ApplyResources(Me.mnuMAP, "mnuMAP")
        Me.mnuMAP.Index = 6
        '
        'mnuRun
        '
        resources.ApplyResources(Me.mnuRun, "mnuRun")
        Me.mnuRun.Index = 6
        Me.mnuRun.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuRunRun, Me.mnuRunCCDRun, Me.mnuRunDryRun, Me.mnuRunPause})
        '
        'mnuRunRun
        '
        resources.ApplyResources(Me.mnuRunRun, "mnuRunRun")
        Me.mnuRunRun.Index = 0
        '
        'mnuRunCCDRun
        '
        resources.ApplyResources(Me.mnuRunCCDRun, "mnuRunCCDRun")
        Me.mnuRunCCDRun.Index = 1
        '
        'mnuRunDryRun
        '
        resources.ApplyResources(Me.mnuRunDryRun, "mnuRunDryRun")
        Me.mnuRunDryRun.Index = 2
        '
        'mnuRunPause
        '
        resources.ApplyResources(Me.mnuRunPause, "mnuRunPause")
        Me.mnuRunPause.Index = 3
        '
        'btnAlignUnlock
        '
        resources.ApplyResources(Me.btnAlignUnlock, "btnAlignUnlock")
        Me.btnAlignUnlock.FlatAppearance.BorderSize = 0
        Me.btnAlignUnlock.Name = "btnAlignUnlock"
        Me.ToolTip1.SetToolTip(Me.btnAlignUnlock, resources.GetString("btnAlignUnlock.ToolTip"))
        Me.btnAlignUnlock.UseVisualStyleBackColor = True
        '
        'btnNodeMap
        '
        resources.ApplyResources(Me.btnNodeMap, "btnNodeMap")
        Me.btnNodeMap.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.WaferMap
        Me.btnNodeMap.FlatAppearance.BorderSize = 0
        Me.btnNodeMap.Name = "btnNodeMap"
        Me.ToolTip1.SetToolTip(Me.btnNodeMap, resources.GetString("btnNodeMap.ToolTip"))
        Me.btnNodeMap.UseVisualStyleBackColor = True
        '
        'btnMachineBBalance
        '
        resources.ApplyResources(Me.btnMachineBBalance, "btnMachineBBalance")
        Me.btnMachineBBalance.FlatAppearance.BorderSize = 0
        Me.btnMachineBBalance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnMachineBBalance.Name = "btnMachineBBalance"
        Me.ToolTip1.SetToolTip(Me.btnMachineBBalance, resources.GetString("btnMachineBBalance.ToolTip"))
        Me.btnMachineBBalance.UseVisualStyleBackColor = True
        '
        'btnWait
        '
        resources.ApplyResources(Me.btnWait, "btnWait")
        Me.btnWait.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Wait
        Me.btnWait.FlatAppearance.BorderSize = 0
        Me.btnWait.Name = "btnWait"
        Me.ToolTip1.SetToolTip(Me.btnWait, resources.GetString("btnWait.ToolTip"))
        Me.btnWait.UseVisualStyleBackColor = True
        '
        'btnLoadB
        '
        resources.ApplyResources(Me.btnLoadB, "btnLoadB")
        Me.btnLoadB.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.LoadB
        Me.btnLoadB.FlatAppearance.BorderSize = 0
        Me.btnLoadB.Name = "btnLoadB"
        Me.ToolTip1.SetToolTip(Me.btnLoadB, resources.GetString("btnLoadB.ToolTip"))
        Me.btnLoadB.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        resources.ApplyResources(Me.btnLoad, "btnLoad")
        Me.btnLoad.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.LoadA
        Me.btnLoad.FlatAppearance.BorderSize = 0
        Me.btnLoad.Name = "btnLoad"
        Me.ToolTip1.SetToolTip(Me.btnLoad, resources.GetString("btnLoad.ToolTip"))
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        resources.ApplyResources(Me.btnSelect, "btnSelect")
        Me.btnSelect.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SelectValve
        Me.btnSelect.FlatAppearance.BorderSize = 0
        Me.btnSelect.Name = "btnSelect"
        Me.ToolTip1.SetToolTip(Me.btnSelect, resources.GetString("btnSelect.ToolTip"))
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnUnload
        '
        resources.ApplyResources(Me.btnUnload, "btnUnload")
        Me.btnUnload.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.UnloadA
        Me.btnUnload.FlatAppearance.BorderSize = 0
        Me.btnUnload.Name = "btnUnload"
        Me.ToolTip1.SetToolTip(Me.btnUnload, resources.GetString("btnUnload.ToolTip"))
        Me.btnUnload.UseVisualStyleBackColor = True
        '
        'btnMove3D
        '
        resources.ApplyResources(Me.btnMove3D, "btnMove3D")
        Me.btnMove3D.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Move
        Me.btnMove3D.FlatAppearance.BorderSize = 0
        Me.btnMove3D.Name = "btnMove3D"
        Me.ToolTip1.SetToolTip(Me.btnMove3D, resources.GetString("btnMove3D.ToolTip"))
        Me.btnMove3D.UseVisualStyleBackColor = True
        '
        'btnNodeAdd
        '
        resources.ApplyResources(Me.btnNodeAdd, "btnNodeAdd")
        Me.btnNodeAdd.BackColor = System.Drawing.SystemColors.Control
        Me.btnNodeAdd.FlatAppearance.BorderSize = 0
        Me.btnNodeAdd.Name = "btnNodeAdd"
        Me.ToolTip1.SetToolTip(Me.btnNodeAdd, resources.GetString("btnNodeAdd.ToolTip"))
        Me.btnNodeAdd.UseVisualStyleBackColor = True
        '
        'btnCircle
        '
        resources.ApplyResources(Me.btnCircle, "btnCircle")
        Me.btnCircle.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Circle
        Me.btnCircle.FlatAppearance.BorderSize = 0
        Me.btnCircle.Name = "btnCircle"
        Me.ToolTip1.SetToolTip(Me.btnCircle, resources.GetString("btnCircle.ToolTip"))
        Me.btnCircle.UseVisualStyleBackColor = True
        '
        'btnUnloadB
        '
        resources.ApplyResources(Me.btnUnloadB, "btnUnloadB")
        Me.btnUnloadB.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.UnloadB
        Me.btnUnloadB.FlatAppearance.BorderSize = 0
        Me.btnUnloadB.Name = "btnUnloadB"
        Me.ToolTip1.SetToolTip(Me.btnUnloadB, resources.GetString("btnUnloadB.ToolTip"))
        Me.btnUnloadB.UseVisualStyleBackColor = True
        '
        'btnArc
        '
        resources.ApplyResources(Me.btnArc, "btnArc")
        Me.btnArc.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Arc
        Me.btnArc.FlatAppearance.BorderSize = 0
        Me.btnArc.Name = "btnArc"
        Me.ToolTip1.SetToolTip(Me.btnArc, resources.GetString("btnArc.ToolTip"))
        Me.btnArc.UseVisualStyleBackColor = True
        '
        'btnNodeDelete
        '
        resources.ApplyResources(Me.btnNodeDelete, "btnNodeDelete")
        Me.btnNodeDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnNodeDelete.FlatAppearance.BorderSize = 0
        Me.btnNodeDelete.Name = "btnNodeDelete"
        Me.ToolTip1.SetToolTip(Me.btnNodeDelete, resources.GetString("btnNodeDelete.ToolTip"))
        Me.btnNodeDelete.UseVisualStyleBackColor = True
        '
        'btnLine3D
        '
        resources.ApplyResources(Me.btnLine3D, "btnLine3D")
        Me.btnLine3D.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Line
        Me.btnLine3D.FlatAppearance.BorderSize = 0
        Me.btnLine3D.Name = "btnLine3D"
        Me.ToolTip1.SetToolTip(Me.btnLine3D, resources.GetString("btnLine3D.ToolTip"))
        Me.btnLine3D.UseVisualStyleBackColor = True
        '
        'btnDots3D
        '
        resources.ApplyResources(Me.btnDots3D, "btnDots3D")
        Me.btnDots3D.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Dot
        Me.btnDots3D.FlatAppearance.BorderSize = 0
        Me.btnDots3D.Name = "btnDots3D"
        Me.ToolTip1.SetToolTip(Me.btnDots3D, resources.GetString("btnDots3D.ToolTip"))
        Me.btnDots3D.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        resources.ApplyResources(Me.btnBack, "btnBack")
        Me.btnBack.BackColor = System.Drawing.SystemColors.Control
        Me.btnBack.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnBack.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnBack.Name = "btnBack"
        Me.ToolTip1.SetToolTip(Me.btnBack, resources.GetString("btnBack.ToolTip"))
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnArray
        '
        resources.ApplyResources(Me.btnArray, "btnArray")
        Me.btnArray.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Array
        Me.btnArray.FlatAppearance.BorderSize = 0
        Me.btnArray.Name = "btnArray"
        Me.ToolTip1.SetToolTip(Me.btnArray, resources.GetString("btnArray.ToolTip"))
        Me.btnArray.UseVisualStyleBackColor = True
        '
        'btnInspect
        '
        resources.ApplyResources(Me.btnInspect, "btnInspect")
        Me.btnInspect.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.FIDs
        Me.btnInspect.FlatAppearance.BorderSize = 0
        Me.btnInspect.Name = "btnInspect"
        Me.ToolTip1.SetToolTip(Me.btnInspect, resources.GetString("btnInspect.ToolTip"))
        Me.btnInspect.UseVisualStyleBackColor = True
        '
        'btnAlign
        '
        resources.ApplyResources(Me.btnAlign, "btnAlign")
        Me.btnAlign.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.FIDs
        Me.btnAlign.FlatAppearance.BorderSize = 0
        Me.btnAlign.Name = "btnAlign"
        Me.ToolTip1.SetToolTip(Me.btnAlign, resources.GetString("btnAlign.ToolTip"))
        Me.btnAlign.UseVisualStyleBackColor = True
        '
        'btnCreateRecipe
        '
        resources.ApplyResources(Me.btnCreateRecipe, "btnCreateRecipe")
        Me.btnCreateRecipe.AutoEllipsis = True
        Me.btnCreateRecipe.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CreateFile
        Me.btnCreateRecipe.FlatAppearance.BorderSize = 0
        Me.btnCreateRecipe.Name = "btnCreateRecipe"
        Me.ToolTip1.SetToolTip(Me.btnCreateRecipe, resources.GetString("btnCreateRecipe.ToolTip"))
        Me.btnCreateRecipe.UseVisualStyleBackColor = True
        '
        'btnStepBasicEdit
        '
        resources.ApplyResources(Me.btnStepBasicEdit, "btnStepBasicEdit")
        Me.btnStepBasicEdit.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.setup1
        Me.btnStepBasicEdit.FlatAppearance.BorderSize = 0
        Me.btnStepBasicEdit.Name = "btnStepBasicEdit"
        Me.ToolTip1.SetToolTip(Me.btnStepBasicEdit, resources.GetString("btnStepBasicEdit.ToolTip"))
        Me.btnStepBasicEdit.UseVisualStyleBackColor = True
        '
        'btnPatternDelete
        '
        resources.ApplyResources(Me.btnPatternDelete, "btnPatternDelete")
        Me.btnPatternDelete.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Delete
        Me.btnPatternDelete.FlatAppearance.BorderSize = 0
        Me.btnPatternDelete.Name = "btnPatternDelete"
        Me.ToolTip1.SetToolTip(Me.btnPatternDelete, resources.GetString("btnPatternDelete.ToolTip"))
        Me.btnPatternDelete.UseVisualStyleBackColor = True
        '
        'btnStepDelete
        '
        resources.ApplyResources(Me.btnStepDelete, "btnStepDelete")
        Me.btnStepDelete.FlatAppearance.BorderSize = 0
        Me.btnStepDelete.Name = "btnStepDelete"
        Me.ToolTip1.SetToolTip(Me.btnStepDelete, resources.GetString("btnStepDelete.ToolTip"))
        Me.btnStepDelete.UseVisualStyleBackColor = True
        '
        'btnRoundAdd
        '
        resources.ApplyResources(Me.btnRoundAdd, "btnRoundAdd")
        Me.btnRoundAdd.BackColor = System.Drawing.SystemColors.Control
        Me.btnRoundAdd.FlatAppearance.BorderSize = 0
        Me.btnRoundAdd.Name = "btnRoundAdd"
        Me.ToolTip1.SetToolTip(Me.btnRoundAdd, resources.GetString("btnRoundAdd.ToolTip"))
        Me.btnRoundAdd.UseVisualStyleBackColor = True
        '
        'btnRoundDelete
        '
        resources.ApplyResources(Me.btnRoundDelete, "btnRoundDelete")
        Me.btnRoundDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnRoundDelete.FlatAppearance.BorderSize = 0
        Me.btnRoundDelete.Name = "btnRoundDelete"
        Me.ToolTip1.SetToolTip(Me.btnRoundDelete, resources.GetString("btnRoundDelete.ToolTip"))
        Me.btnRoundDelete.UseVisualStyleBackColor = True
        '
        'btnRoundUp
        '
        resources.ApplyResources(Me.btnRoundUp, "btnRoundUp")
        Me.btnRoundUp.BackColor = System.Drawing.SystemColors.Control
        Me.btnRoundUp.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.MoveUp
        Me.btnRoundUp.FlatAppearance.BorderSize = 0
        Me.btnRoundUp.Name = "btnRoundUp"
        Me.ToolTip1.SetToolTip(Me.btnRoundUp, resources.GetString("btnRoundUp.ToolTip"))
        Me.btnRoundUp.UseVisualStyleBackColor = True
        '
        'btnRoundDown
        '
        resources.ApplyResources(Me.btnRoundDown, "btnRoundDown")
        Me.btnRoundDown.BackColor = System.Drawing.SystemColors.Control
        Me.btnRoundDown.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.MoveDown
        Me.btnRoundDown.FlatAppearance.BorderSize = 0
        Me.btnRoundDown.Name = "btnRoundDown"
        Me.ToolTip1.SetToolTip(Me.btnRoundDown, resources.GetString("btnRoundDown.ToolTip"))
        Me.btnRoundDown.UseVisualStyleBackColor = True
        '
        'btnLock
        '
        resources.ApplyResources(Me.btnLock, "btnLock")
        Me.btnLock.AutoEllipsis = True
        Me.btnLock.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Lock
        Me.btnLock.FlatAppearance.BorderSize = 0
        Me.btnLock.Name = "btnLock"
        Me.ToolTip1.SetToolTip(Me.btnLock, resources.GetString("btnLock.ToolTip"))
        Me.btnLock.UseVisualStyleBackColor = True
        '
        'btnSaveRecipe
        '
        resources.ApplyResources(Me.btnSaveRecipe, "btnSaveRecipe")
        Me.btnSaveRecipe.AutoEllipsis = True
        Me.btnSaveRecipe.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Save1
        Me.btnSaveRecipe.FlatAppearance.BorderSize = 0
        Me.btnSaveRecipe.Name = "btnSaveRecipe"
        Me.ToolTip1.SetToolTip(Me.btnSaveRecipe, resources.GetString("btnSaveRecipe.ToolTip"))
        Me.btnSaveRecipe.UseVisualStyleBackColor = True
        '
        'btnLoadRecipe
        '
        resources.ApplyResources(Me.btnLoadRecipe, "btnLoadRecipe")
        Me.btnLoadRecipe.AutoEllipsis = True
        Me.btnLoadRecipe.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.OpenFile
        Me.btnLoadRecipe.FlatAppearance.BorderSize = 0
        Me.btnLoadRecipe.Name = "btnLoadRecipe"
        Me.ToolTip1.SetToolTip(Me.btnLoadRecipe, resources.GetString("btnLoadRecipe.ToolTip"))
        Me.btnLoadRecipe.UseVisualStyleBackColor = True
        '
        'btnHeight
        '
        resources.ApplyResources(Me.btnHeight, "btnHeight")
        Me.btnHeight.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.LaserReader
        Me.btnHeight.FlatAppearance.BorderSize = 0
        Me.btnHeight.Name = "btnHeight"
        Me.ToolTip1.SetToolTip(Me.btnHeight, resources.GetString("btnHeight.ToolTip"))
        Me.btnHeight.UseVisualStyleBackColor = True
        '
        'btnPark
        '
        resources.ApplyResources(Me.btnPark, "btnPark")
        Me.btnPark.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Park
        Me.btnPark.FlatAppearance.BorderSize = 0
        Me.btnPark.Name = "btnPark"
        Me.ToolTip1.SetToolTip(Me.btnPark, resources.GetString("btnPark.ToolTip"))
        Me.btnPark.UseVisualStyleBackColor = True
        '
        'btnCCDRun
        '
        resources.ApplyResources(Me.btnCCDRun, "btnCCDRun")
        Me.btnCCDRun.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.VideoRun
        Me.btnCCDRun.FlatAppearance.BorderSize = 0
        Me.btnCCDRun.Name = "btnCCDRun"
        Me.ToolTip1.SetToolTip(Me.btnCCDRun, resources.GetString("btnCCDRun.ToolTip"))
        Me.btnCCDRun.UseVisualStyleBackColor = True
        '
        'btnPause
        '
        resources.ApplyResources(Me.btnPause, "btnPause")
        Me.btnPause.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Pause1
        Me.btnPause.FlatAppearance.BorderSize = 0
        Me.btnPause.Name = "btnPause"
        Me.ToolTip1.SetToolTip(Me.btnPause, resources.GetString("btnPause.ToolTip"))
        Me.btnPause.UseVisualStyleBackColor = True
        '
        'btnRun
        '
        resources.ApplyResources(Me.btnRun, "btnRun")
        Me.btnRun.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Run1
        Me.btnRun.FlatAppearance.BorderSize = 0
        Me.btnRun.Name = "btnRun"
        Me.ToolTip1.SetToolTip(Me.btnRun, resources.GetString("btnRun.ToolTip"))
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'btnMachineABalance
        '
        resources.ApplyResources(Me.btnMachineABalance, "btnMachineABalance")
        Me.btnMachineABalance.FlatAppearance.BorderSize = 0
        Me.btnMachineABalance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnMachineABalance.Name = "btnMachineABalance"
        Me.ToolTip1.SetToolTip(Me.btnMachineABalance, resources.GetString("btnMachineABalance.ToolTip"))
        Me.btnMachineABalance.UseVisualStyleBackColor = True
        '
        'btnDryRun
        '
        resources.ApplyResources(Me.btnDryRun, "btnDryRun")
        Me.btnDryRun.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.DryRun
        Me.btnDryRun.FlatAppearance.BorderSize = 0
        Me.btnDryRun.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnDryRun.Name = "btnDryRun"
        Me.ToolTip1.SetToolTip(Me.btnDryRun, resources.GetString("btnDryRun.ToolTip"))
        Me.btnDryRun.UseVisualStyleBackColor = True
        '
        'btnStepUp
        '
        resources.ApplyResources(Me.btnStepUp, "btnStepUp")
        Me.btnStepUp.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.MoveUp
        Me.btnStepUp.FlatAppearance.BorderSize = 0
        Me.btnStepUp.Name = "btnStepUp"
        Me.ToolTip1.SetToolTip(Me.btnStepUp, resources.GetString("btnStepUp.ToolTip"))
        Me.btnStepUp.UseVisualStyleBackColor = True
        '
        'btnPatternAdd
        '
        resources.ApplyResources(Me.btnPatternAdd, "btnPatternAdd")
        Me.btnPatternAdd.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Add1
        Me.btnPatternAdd.FlatAppearance.BorderSize = 0
        Me.btnPatternAdd.Name = "btnPatternAdd"
        Me.ToolTip1.SetToolTip(Me.btnPatternAdd, resources.GetString("btnPatternAdd.ToolTip"))
        Me.btnPatternAdd.UseVisualStyleBackColor = True
        '
        'btnStepDown
        '
        resources.ApplyResources(Me.btnStepDown, "btnStepDown")
        Me.btnStepDown.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.MoveDown
        Me.btnStepDown.FlatAppearance.BorderSize = 0
        Me.btnStepDown.Name = "btnStepDown"
        Me.ToolTip1.SetToolTip(Me.btnStepDown, resources.GetString("btnStepDown.ToolTip"))
        Me.btnStepDown.UseVisualStyleBackColor = True
        '
        'btnAddArray
        '
        resources.ApplyResources(Me.btnAddArray, "btnAddArray")
        Me.btnAddArray.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Array
        Me.btnAddArray.FlatAppearance.BorderSize = 0
        Me.btnAddArray.Name = "btnAddArray"
        Me.ToolTip1.SetToolTip(Me.btnAddArray, resources.GetString("btnAddArray.ToolTip"))
        Me.btnAddArray.UseVisualStyleBackColor = True
        '
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        Me.ToolTip1.SetToolTip(Me.UcStatusBar1, resources.GetString("UcStatusBar1.ToolTip"))
        '
        'FolderBrowserImport
        '
        resources.ApplyResources(Me.FolderBrowserImport, "FolderBrowserImport")
        '
        'FolderBrowserExport
        '
        resources.ApplyResources(Me.FolderBrowserExport, "FolderBrowserExport")
        '
        'Timer1
        '
        '
        'frmRecipe04
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnAlignUnlock)
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.btnAddArray)
        Me.Controls.Add(Me.btnExtendOff)
        Me.Controls.Add(Me.btnNodeMap)
        Me.Controls.Add(Me.btnExtendOn)
        Me.Controls.Add(Me.btnMachineBBalance)
        Me.Controls.Add(Me.btnWait)
        Me.Controls.Add(Me.chkNodeConnect)
        Me.Controls.Add(Me.btnContiEnd)
        Me.Controls.Add(Me.btnLoadB)
        Me.Controls.Add(Me.btnContiStart)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.btnUnload)
        Me.Controls.Add(Me.btnMove3D)
        Me.Controls.Add(Me.btnNodeAdd)
        Me.Controls.Add(Me.btnCircle)
        Me.Controls.Add(Me.btnUnloadB)
        Me.Controls.Add(Me.btnArc)
        Me.Controls.Add(Me.btnNodeDelete)
        Me.Controls.Add(Me.btnLine3D)
        Me.Controls.Add(Me.lblNodeEditor)
        Me.Controls.Add(Me.btnDots3D)
        Me.Controls.Add(Me.tabView)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnArray)
        Me.Controls.Add(Me.lstRoundNo)
        Me.Controls.Add(Me.btnInspect)
        Me.Controls.Add(Me.btnAlign)
        Me.Controls.Add(Me.btnCreateRecipe)
        Me.Controls.Add(Me.btnStepBasicEdit)
        Me.Controls.Add(Me.btnPatternDelete)
        Me.Controls.Add(Me.btnStepDelete)
        Me.Controls.Add(Me.lstPatternID)
        Me.Controls.Add(Me.btnRoundAdd)
        Me.Controls.Add(Me.lblOthers)
        Me.Controls.Add(Me.btnRoundDelete)
        Me.Controls.Add(Me.lblStepEditor)
        Me.Controls.Add(Me.lblRoundLoopEditor)
        Me.Controls.Add(Me.lblPatternEditor)
        Me.Controls.Add(Me.btnRoundUp)
        Me.Controls.Add(Me.btnRoundDown)
        Me.Controls.Add(Me.lblFile)
        Me.Controls.Add(Me.btnLock)
        Me.Controls.Add(Me.btnSaveRecipe)
        Me.Controls.Add(Me.btnLoadRecipe)
        Me.Controls.Add(Me.btnHeight)
        Me.Controls.Add(Me.btnPark)
        Me.Controls.Add(Me.btnCCDRun)
        Me.Controls.Add(Me.btnPause)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.grpStep)
        Me.Controls.Add(Me.btnMachineABalance)
        Me.Controls.Add(Me.btnDryRun)
        Me.Controls.Add(Me.btnStepUp)
        Me.Controls.Add(Me.btnPatternAdd)
        Me.Controls.Add(Me.btnStepDown)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Menu = Me.MainMenu1
        Me.MinimizeBox = False
        Me.Name = "frmRecipe04"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpStep.ResumeLayout(False)
        CType(Me.dgvStep, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabView.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.picPcsSingleGraph, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnStepDelete As System.Windows.Forms.Button
    Friend WithEvents btnStepUp As System.Windows.Forms.Button
    Friend WithEvents btnStepDown As System.Windows.Forms.Button
    Friend WithEvents btnDryRun As System.Windows.Forms.Button
    Friend WithEvents btnStepBasicEdit As System.Windows.Forms.Button
    Friend WithEvents grpStep As System.Windows.Forms.GroupBox
    Friend WithEvents dgvStep As System.Windows.Forms.DataGridView
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents btnMachineABalance As System.Windows.Forms.Button
    Friend WithEvents btnUnload As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents btnPause As System.Windows.Forms.Button
    Friend WithEvents btnCCDRun As System.Windows.Forms.Button
    Friend WithEvents btnHeight As System.Windows.Forms.Button
    Friend WithEvents btnArray As System.Windows.Forms.Button
    Friend WithEvents btnAlign As System.Windows.Forms.Button
    Friend WithEvents btnLoadRecipe As System.Windows.Forms.Button
    Friend WithEvents btnSaveRecipe As System.Windows.Forms.Button
    Friend WithEvents btnLock As System.Windows.Forms.Button
    Friend WithEvents btnCreateRecipe As System.Windows.Forms.Button
    Friend WithEvents btnPark As System.Windows.Forms.Button
    Friend WithEvents OFDLoadRecipe As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnRoundDown As System.Windows.Forms.Button
    Friend WithEvents btnRoundUp As System.Windows.Forms.Button
    Friend WithEvents btnRoundDelete As System.Windows.Forms.Button
    Friend WithEvents btnRoundAdd As System.Windows.Forms.Button
    Friend WithEvents lstPatternID As System.Windows.Forms.ListBox
    Friend WithEvents lblFile As System.Windows.Forms.Label
    Friend WithEvents lblPatternEditor As System.Windows.Forms.Label
    Friend WithEvents lblRoundLoopEditor As System.Windows.Forms.Label
    Friend WithEvents lblOthers As System.Windows.Forms.Label
    Friend WithEvents SFDSaveRecipe As System.Windows.Forms.SaveFileDialog
    Friend WithEvents lblStepEditor As System.Windows.Forms.Label
    Friend WithEvents btnPatternAdd As System.Windows.Forms.Button
    Friend WithEvents btnPatternDelete As System.Windows.Forms.Button
    Friend WithEvents lstRoundNo As System.Windows.Forms.ListBox
    Friend WithEvents treePattern As System.Windows.Forms.TreeView
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents tabView As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnNodeAdd As System.Windows.Forms.Button
    Friend WithEvents btnNodeDelete As System.Windows.Forms.Button
    Friend WithEvents lblNodeEditor As System.Windows.Forms.Label
    Friend WithEvents btnDots3D As System.Windows.Forms.Button
    Friend WithEvents btnLine3D As System.Windows.Forms.Button
    Friend WithEvents btnArc As System.Windows.Forms.Button
    Friend WithEvents btnCircle As System.Windows.Forms.Button
    Friend WithEvents btnMove3D As System.Windows.Forms.Button
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnContiStart As System.Windows.Forms.Button
    Friend WithEvents btnContiEnd As System.Windows.Forms.Button
    Friend WithEvents btnWait As System.Windows.Forms.Button
    Friend WithEvents btnExtendOn As System.Windows.Forms.Button
    Friend WithEvents btnExtendOff As System.Windows.Forms.Button
    Friend WithEvents chkNodeConnect As System.Windows.Forms.CheckBox
    Friend WithEvents btnLoadB As System.Windows.Forms.Button
    Friend WithEvents btnUnloadB As System.Windows.Forms.Button
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents mnuFile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileCreate As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileOpen As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileSave As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileSaveAs As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem25 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuExit As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPattern As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPatternAdd As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPatternDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPatternCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPatternPaste As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPatternRename As System.Windows.Forms.MenuItem
    Friend WithEvents mnuNode As System.Windows.Forms.MenuItem
    Friend WithEvents mnuNodeAdd As System.Windows.Forms.MenuItem
    Friend WithEvents mnuNodeDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mnuNodeCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mnuNodePaste As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem21 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuNodeFIDs As System.Windows.Forms.MenuItem
    Friend WithEvents mnuNodeArray As System.Windows.Forms.MenuItem
    Friend WithEvents mnuNodeHeight As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRound As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRoundAdd As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRoundDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRoundCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRoundPaste As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRoundMoveUp As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRoundMoveDown As System.Windows.Forms.MenuItem
    Friend WithEvents mnuStep As System.Windows.Forms.MenuItem
    Friend WithEvents mnuStepAdd As System.Windows.Forms.MenuItem
    Friend WithEvents mnuStepDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mnuStepCopy As System.Windows.Forms.MenuItem
    Friend WithEvents mnuStepPaste As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem36 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuStepParameter As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOthers As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRun As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRunRun As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRunCCDRun As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRunDryRun As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRunPause As System.Windows.Forms.MenuItem
    Friend WithEvents mnuStepMoveUp As System.Windows.Forms.MenuItem
    Friend WithEvents mnuStepMoveDown As System.Windows.Forms.MenuItem
    Friend WithEvents mnuBalance As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPark As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLoadA As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLoadB As System.Windows.Forms.MenuItem
    Friend WithEvents mnuUnloadA As System.Windows.Forms.MenuItem
    Friend WithEvents mnuUnloadB As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMAP As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents picPcsSingleGraph As System.Windows.Forms.PictureBox
    Friend WithEvents btnMachineBBalance As System.Windows.Forms.Button
    Friend WithEvents btnNodeMap As System.Windows.Forms.Button
    Friend WithEvents mnuProcessTime As System.Windows.Forms.MenuItem
    Friend WithEvents btnAddArray As System.Windows.Forms.Button
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents mnuFileImport As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileExport As System.Windows.Forms.MenuItem
    Friend WithEvents FolderBrowserImport As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents FolderBrowserExport As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnInspect As System.Windows.Forms.Button
    Friend WithEvents btnAlignUnlock As System.Windows.Forms.Button
    Friend WithEvents mnuRotae As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRotae180 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMirrorX As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMirrorY As System.Windows.Forms.MenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNode))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tb_A = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnOpenMapA = New System.Windows.Forms.Button()
        Me.gboxStage2 = New System.Windows.Forms.GroupBox()
        Me.chklbStage2WorkNode = New System.Windows.Forms.CheckedListBox()
        Me.labMapPathA = New System.Windows.Forms.Label()
        Me.gboxStage1 = New System.Windows.Forms.GroupBox()
        Me.chklbStage1WorkNode = New System.Windows.Forms.CheckedListBox()
        Me.gbANode1 = New System.Windows.Forms.GroupBox()
        Me.picANode1 = New System.Windows.Forms.PictureBox()
        Me.tb_B = New System.Windows.Forms.TabPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.btnOpenMapB = New System.Windows.Forms.Button()
        Me.gboxStage4 = New System.Windows.Forms.GroupBox()
        Me.chklbStage4WorkNode = New System.Windows.Forms.CheckedListBox()
        Me.labMapPathB = New System.Windows.Forms.Label()
        Me.gboxStage3 = New System.Windows.Forms.GroupBox()
        Me.chklbStage3WorkNode = New System.Windows.Forms.CheckedListBox()
        Me.gbBNode1 = New System.Windows.Forms.GroupBox()
        Me.picBNode1 = New System.Windows.Forms.PictureBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chklbBin = New System.Windows.Forms.CheckedListBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chklbBin2 = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.rbNotchLeft = New System.Windows.Forms.RadioButton()
        Me.rbNotchBottom = New System.Windows.Forms.RadioButton()
        Me.rbNotchTop = New System.Windows.Forms.RadioButton()
        Me.rbNotchRight = New System.Windows.Forms.RadioButton()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tb_A.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.gboxStage2.SuspendLayout()
        Me.gboxStage1.SuspendLayout()
        Me.gbANode1.SuspendLayout()
        CType(Me.picANode1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tb_B.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.gboxStage4.SuspendLayout()
        Me.gboxStage3.SuspendLayout()
        Me.gbBNode1.SuspendLayout()
        CType(Me.picBNode1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        resources.ApplyResources(Me.SplitContainer1.Panel1, "SplitContainer1.Panel1")
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.ToolTip1.SetToolTip(Me.SplitContainer1.Panel1, resources.GetString("SplitContainer1.Panel1.ToolTip"))
        '
        'SplitContainer1.Panel2
        '
        resources.ApplyResources(Me.SplitContainer1.Panel2, "SplitContainer1.Panel2")
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExit)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox5)
        Me.ToolTip1.SetToolTip(Me.SplitContainer1.Panel2, resources.GetString("SplitContainer1.Panel2.ToolTip"))
        Me.ToolTip1.SetToolTip(Me.SplitContainer1, resources.GetString("SplitContainer1.ToolTip"))
        '
        'Panel1
        '
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Name = "Panel1"
        Me.ToolTip1.SetToolTip(Me.Panel1, resources.GetString("Panel1.ToolTip"))
        '
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.tb_A)
        Me.TabControl1.Controls.Add(Me.tb_B)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.ToolTip1.SetToolTip(Me.TabControl1, resources.GetString("TabControl1.ToolTip"))
        '
        'tb_A
        '
        resources.ApplyResources(Me.tb_A, "tb_A")
        Me.tb_A.Controls.Add(Me.SplitContainer2)
        Me.tb_A.Name = "tb_A"
        Me.ToolTip1.SetToolTip(Me.tb_A, resources.GetString("tb_A.ToolTip"))
        Me.tb_A.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        resources.ApplyResources(Me.SplitContainer2, "SplitContainer2")
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        resources.ApplyResources(Me.SplitContainer2.Panel1, "SplitContainer2.Panel1")
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnOpenMapA)
        Me.SplitContainer2.Panel1.Controls.Add(Me.gboxStage2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.labMapPathA)
        Me.SplitContainer2.Panel1.Controls.Add(Me.gboxStage1)
        Me.ToolTip1.SetToolTip(Me.SplitContainer2.Panel1, resources.GetString("SplitContainer2.Panel1.ToolTip"))
        '
        'SplitContainer2.Panel2
        '
        resources.ApplyResources(Me.SplitContainer2.Panel2, "SplitContainer2.Panel2")
        Me.SplitContainer2.Panel2.Controls.Add(Me.gbANode1)
        Me.ToolTip1.SetToolTip(Me.SplitContainer2.Panel2, resources.GetString("SplitContainer2.Panel2.ToolTip"))
        Me.ToolTip1.SetToolTip(Me.SplitContainer2, resources.GetString("SplitContainer2.ToolTip"))
        '
        'btnOpenMapA
        '
        resources.ApplyResources(Me.btnOpenMapA, "btnOpenMapA")
        Me.btnOpenMapA.Name = "btnOpenMapA"
        Me.ToolTip1.SetToolTip(Me.btnOpenMapA, resources.GetString("btnOpenMapA.ToolTip"))
        Me.btnOpenMapA.UseVisualStyleBackColor = True
        '
        'gboxStage2
        '
        resources.ApplyResources(Me.gboxStage2, "gboxStage2")
        Me.gboxStage2.Controls.Add(Me.chklbStage2WorkNode)
        Me.gboxStage2.Name = "gboxStage2"
        Me.gboxStage2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gboxStage2, resources.GetString("gboxStage2.ToolTip"))
        '
        'chklbStage2WorkNode
        '
        resources.ApplyResources(Me.chklbStage2WorkNode, "chklbStage2WorkNode")
        Me.chklbStage2WorkNode.FormattingEnabled = True
        Me.chklbStage2WorkNode.Name = "chklbStage2WorkNode"
        Me.ToolTip1.SetToolTip(Me.chklbStage2WorkNode, resources.GetString("chklbStage2WorkNode.ToolTip"))
        '
        'labMapPathA
        '
        resources.ApplyResources(Me.labMapPathA, "labMapPathA")
        Me.labMapPathA.Name = "labMapPathA"
        Me.ToolTip1.SetToolTip(Me.labMapPathA, resources.GetString("labMapPathA.ToolTip"))
        '
        'gboxStage1
        '
        resources.ApplyResources(Me.gboxStage1, "gboxStage1")
        Me.gboxStage1.Controls.Add(Me.chklbStage1WorkNode)
        Me.gboxStage1.Name = "gboxStage1"
        Me.gboxStage1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gboxStage1, resources.GetString("gboxStage1.ToolTip"))
        '
        'chklbStage1WorkNode
        '
        resources.ApplyResources(Me.chklbStage1WorkNode, "chklbStage1WorkNode")
        Me.chklbStage1WorkNode.FormattingEnabled = True
        Me.chklbStage1WorkNode.Name = "chklbStage1WorkNode"
        Me.ToolTip1.SetToolTip(Me.chklbStage1WorkNode, resources.GetString("chklbStage1WorkNode.ToolTip"))
        '
        'gbANode1
        '
        resources.ApplyResources(Me.gbANode1, "gbANode1")
        Me.gbANode1.Controls.Add(Me.picANode1)
        Me.gbANode1.Name = "gbANode1"
        Me.gbANode1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gbANode1, resources.GetString("gbANode1.ToolTip"))
        '
        'picANode1
        '
        resources.ApplyResources(Me.picANode1, "picANode1")
        Me.picANode1.BackColor = System.Drawing.Color.Silver
        Me.picANode1.Name = "picANode1"
        Me.picANode1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picANode1, resources.GetString("picANode1.ToolTip"))
        '
        'tb_B
        '
        resources.ApplyResources(Me.tb_B, "tb_B")
        Me.tb_B.Controls.Add(Me.SplitContainer3)
        Me.tb_B.Name = "tb_B"
        Me.ToolTip1.SetToolTip(Me.tb_B, resources.GetString("tb_B.ToolTip"))
        Me.tb_B.UseVisualStyleBackColor = True
        '
        'SplitContainer3
        '
        resources.ApplyResources(Me.SplitContainer3, "SplitContainer3")
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        resources.ApplyResources(Me.SplitContainer3.Panel1, "SplitContainer3.Panel1")
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnOpenMapB)
        Me.SplitContainer3.Panel1.Controls.Add(Me.gboxStage4)
        Me.SplitContainer3.Panel1.Controls.Add(Me.labMapPathB)
        Me.SplitContainer3.Panel1.Controls.Add(Me.gboxStage3)
        Me.ToolTip1.SetToolTip(Me.SplitContainer3.Panel1, resources.GetString("SplitContainer3.Panel1.ToolTip"))
        '
        'SplitContainer3.Panel2
        '
        resources.ApplyResources(Me.SplitContainer3.Panel2, "SplitContainer3.Panel2")
        Me.SplitContainer3.Panel2.Controls.Add(Me.gbBNode1)
        Me.ToolTip1.SetToolTip(Me.SplitContainer3.Panel2, resources.GetString("SplitContainer3.Panel2.ToolTip"))
        Me.ToolTip1.SetToolTip(Me.SplitContainer3, resources.GetString("SplitContainer3.ToolTip"))
        '
        'btnOpenMapB
        '
        resources.ApplyResources(Me.btnOpenMapB, "btnOpenMapB")
        Me.btnOpenMapB.Name = "btnOpenMapB"
        Me.ToolTip1.SetToolTip(Me.btnOpenMapB, resources.GetString("btnOpenMapB.ToolTip"))
        Me.btnOpenMapB.UseVisualStyleBackColor = True
        '
        'gboxStage4
        '
        resources.ApplyResources(Me.gboxStage4, "gboxStage4")
        Me.gboxStage4.Controls.Add(Me.chklbStage4WorkNode)
        Me.gboxStage4.Name = "gboxStage4"
        Me.gboxStage4.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gboxStage4, resources.GetString("gboxStage4.ToolTip"))
        '
        'chklbStage4WorkNode
        '
        resources.ApplyResources(Me.chklbStage4WorkNode, "chklbStage4WorkNode")
        Me.chklbStage4WorkNode.FormattingEnabled = True
        Me.chklbStage4WorkNode.Name = "chklbStage4WorkNode"
        Me.ToolTip1.SetToolTip(Me.chklbStage4WorkNode, resources.GetString("chklbStage4WorkNode.ToolTip"))
        '
        'labMapPathB
        '
        resources.ApplyResources(Me.labMapPathB, "labMapPathB")
        Me.labMapPathB.Name = "labMapPathB"
        Me.ToolTip1.SetToolTip(Me.labMapPathB, resources.GetString("labMapPathB.ToolTip"))
        '
        'gboxStage3
        '
        resources.ApplyResources(Me.gboxStage3, "gboxStage3")
        Me.gboxStage3.Controls.Add(Me.chklbStage3WorkNode)
        Me.gboxStage3.Name = "gboxStage3"
        Me.gboxStage3.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gboxStage3, resources.GetString("gboxStage3.ToolTip"))
        '
        'chklbStage3WorkNode
        '
        resources.ApplyResources(Me.chklbStage3WorkNode, "chklbStage3WorkNode")
        Me.chklbStage3WorkNode.FormattingEnabled = True
        Me.chklbStage3WorkNode.Name = "chklbStage3WorkNode"
        Me.ToolTip1.SetToolTip(Me.chklbStage3WorkNode, resources.GetString("chklbStage3WorkNode.ToolTip"))
        '
        'gbBNode1
        '
        resources.ApplyResources(Me.gbBNode1, "gbBNode1")
        Me.gbBNode1.Controls.Add(Me.picBNode1)
        Me.gbBNode1.Name = "gbBNode1"
        Me.gbBNode1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.gbBNode1, resources.GetString("gbBNode1.ToolTip"))
        '
        'picBNode1
        '
        resources.ApplyResources(Me.picBNode1, "picBNode1")
        Me.picBNode1.BackColor = System.Drawing.Color.Silver
        Me.picBNode1.Name = "picBNode1"
        Me.picBNode1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picBNode1, resources.GetString("picBNode1.ToolTip"))
        '
        'btnExit
        '
        resources.ApplyResources(Me.btnExit, "btnExit")
        Me.btnExit.BackgroundImage = Global.MapData.My.Resources.Resources.CancelExit
        Me.btnExit.Name = "btnExit"
        Me.ToolTip1.SetToolTip(Me.btnExit, resources.GetString("btnExit.ToolTip"))
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.chklbBin)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'chklbBin
        '
        resources.ApplyResources(Me.chklbBin, "chklbBin")
        Me.chklbBin.FormattingEnabled = True
        Me.chklbBin.Items.AddRange(New Object() {resources.GetString("chklbBin.Items"), resources.GetString("chklbBin.Items1"), resources.GetString("chklbBin.Items2"), resources.GetString("chklbBin.Items3"), resources.GetString("chklbBin.Items4"), resources.GetString("chklbBin.Items5"), resources.GetString("chklbBin.Items6"), resources.GetString("chklbBin.Items7"), resources.GetString("chklbBin.Items8"), resources.GetString("chklbBin.Items9")})
        Me.chklbBin.Name = "chklbBin"
        Me.ToolTip1.SetToolTip(Me.chklbBin, resources.GetString("chklbBin.ToolTip"))
        '
        'btnSave
        '
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.BackgroundImage = Global.MapData.My.Resources.Resources.SaveExit
        Me.btnSave.Name = "btnSave"
        Me.ToolTip1.SetToolTip(Me.btnSave, resources.GetString("btnSave.ToolTip"))
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Controls.Add(Me.chklbBin2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox2, resources.GetString("GroupBox2.ToolTip"))
        '
        'chklbBin2
        '
        resources.ApplyResources(Me.chklbBin2, "chklbBin2")
        Me.chklbBin2.FormattingEnabled = True
        Me.chklbBin2.Items.AddRange(New Object() {resources.GetString("chklbBin2.Items"), resources.GetString("chklbBin2.Items1"), resources.GetString("chklbBin2.Items2"), resources.GetString("chklbBin2.Items3"), resources.GetString("chklbBin2.Items4"), resources.GetString("chklbBin2.Items5"), resources.GetString("chklbBin2.Items6"), resources.GetString("chklbBin2.Items7"), resources.GetString("chklbBin2.Items8"), resources.GetString("chklbBin2.Items9"), resources.GetString("chklbBin2.Items10"), resources.GetString("chklbBin2.Items11"), resources.GetString("chklbBin2.Items12"), resources.GetString("chklbBin2.Items13"), resources.GetString("chklbBin2.Items14"), resources.GetString("chklbBin2.Items15"), resources.GetString("chklbBin2.Items16"), resources.GetString("chklbBin2.Items17"), resources.GetString("chklbBin2.Items18"), resources.GetString("chklbBin2.Items19"), resources.GetString("chklbBin2.Items20"), resources.GetString("chklbBin2.Items21"), resources.GetString("chklbBin2.Items22"), resources.GetString("chklbBin2.Items23"), resources.GetString("chklbBin2.Items24"), resources.GetString("chklbBin2.Items25")})
        Me.chklbBin2.Name = "chklbBin2"
        Me.ToolTip1.SetToolTip(Me.chklbBin2, resources.GetString("chklbBin2.ToolTip"))
        '
        'GroupBox5
        '
        resources.ApplyResources(Me.GroupBox5, "GroupBox5")
        Me.GroupBox5.Controls.Add(Me.rbNotchLeft)
        Me.GroupBox5.Controls.Add(Me.rbNotchBottom)
        Me.GroupBox5.Controls.Add(Me.rbNotchTop)
        Me.GroupBox5.Controls.Add(Me.rbNotchRight)
        Me.GroupBox5.Controls.Add(Me.DataGridView2)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox5, resources.GetString("GroupBox5.ToolTip"))
        '
        'rbNotchLeft
        '
        resources.ApplyResources(Me.rbNotchLeft, "rbNotchLeft")
        Me.rbNotchLeft.Name = "rbNotchLeft"
        Me.ToolTip1.SetToolTip(Me.rbNotchLeft, resources.GetString("rbNotchLeft.ToolTip"))
        Me.rbNotchLeft.UseVisualStyleBackColor = True
        '
        'rbNotchBottom
        '
        resources.ApplyResources(Me.rbNotchBottom, "rbNotchBottom")
        Me.rbNotchBottom.Name = "rbNotchBottom"
        Me.ToolTip1.SetToolTip(Me.rbNotchBottom, resources.GetString("rbNotchBottom.ToolTip"))
        Me.rbNotchBottom.UseVisualStyleBackColor = True
        '
        'rbNotchTop
        '
        resources.ApplyResources(Me.rbNotchTop, "rbNotchTop")
        Me.rbNotchTop.Name = "rbNotchTop"
        Me.ToolTip1.SetToolTip(Me.rbNotchTop, resources.GetString("rbNotchTop.ToolTip"))
        Me.rbNotchTop.UseVisualStyleBackColor = True
        '
        'rbNotchRight
        '
        resources.ApplyResources(Me.rbNotchRight, "rbNotchRight")
        Me.rbNotchRight.Name = "rbNotchRight"
        Me.ToolTip1.SetToolTip(Me.rbNotchRight, resources.GetString("rbNotchRight.ToolTip"))
        Me.rbNotchRight.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        resources.ApplyResources(Me.DataGridView2, "DataGridView2")
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowTemplate.Height = 24
        Me.ToolTip1.SetToolTip(Me.DataGridView2, resources.GetString("DataGridView2.ToolTip"))
        '
        'frmNode
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNode"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.tb_A.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.gboxStage2.ResumeLayout(False)
        Me.gboxStage1.ResumeLayout(False)
        Me.gbANode1.ResumeLayout(False)
        CType(Me.picANode1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tb_B.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.gboxStage4.ResumeLayout(False)
        Me.gboxStage3.ResumeLayout(False)
        Me.gbBNode1.ResumeLayout(False)
        CType(Me.picBNode1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tb_A As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tb_B As System.Windows.Forms.TabPage
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents gbBNode1 As System.Windows.Forms.GroupBox
    Friend WithEvents picBNode1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents rbNotchLeft As System.Windows.Forms.RadioButton
    Friend WithEvents rbNotchBottom As System.Windows.Forms.RadioButton
    Friend WithEvents rbNotchTop As System.Windows.Forms.RadioButton
    Friend WithEvents rbNotchRight As System.Windows.Forms.RadioButton
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents chklbBin As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chklbBin2 As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnOpenMapA As System.Windows.Forms.Button
    Friend WithEvents btnOpenMapB As System.Windows.Forms.Button
    Friend WithEvents labMapPathA As System.Windows.Forms.Label
    Friend WithEvents labMapPathB As System.Windows.Forms.Label
    Friend WithEvents chklbStage1WorkNode As System.Windows.Forms.CheckedListBox
    Friend WithEvents chklbStage2WorkNode As System.Windows.Forms.CheckedListBox
    Friend WithEvents chklbStage3WorkNode As System.Windows.Forms.CheckedListBox
    Friend WithEvents chklbStage4WorkNode As System.Windows.Forms.CheckedListBox
    Friend WithEvents gbANode1 As System.Windows.Forms.GroupBox
    Friend WithEvents picANode1 As System.Windows.Forms.PictureBox
    Friend WithEvents gboxStage2 As System.Windows.Forms.GroupBox
    Friend WithEvents gboxStage1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents gboxStage4 As System.Windows.Forms.GroupBox
    Friend WithEvents gboxStage3 As System.Windows.Forms.GroupBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

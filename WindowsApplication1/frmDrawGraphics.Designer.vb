<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDrawGraphics
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
        Me.bntLoad = New System.Windows.Forms.Button()
        Me.OFDLoadRecipe = New System.Windows.Forms.OpenFileDialog()
        Me.picDrawGraphicsRecipeGraph = New System.Windows.Forms.PictureBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.butPIIMap = New System.Windows.Forms.Button()
        Me.CbCycle = New System.Windows.Forms.CheckBox()
        Me.VScrollBar2 = New System.Windows.Forms.VScrollBar()
        Me.panWaferMap = New System.Windows.Forms.Panel()
        Me.txtScrollX = New System.Windows.Forms.TextBox()
        Me.txtScrollY = New System.Windows.Forms.TextBox()
        Me.TimerDrawGraphics = New System.Windows.Forms.Timer(Me.components)
        Me.butBigger = New System.Windows.Forms.Button()
        Me.butSmaller = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.no_work_save = New System.Windows.Forms.Button()
        Me.no_work_load = New System.Windows.Forms.Button()
        Me.Mouse_DIE_locate = New System.Windows.Forms.Label()
        Me.no_work_die = New System.Windows.Forms.ListBox()
        Me.LoadNoWorkDie = New System.Windows.Forms.OpenFileDialog()
        CType(Me.picDrawGraphicsRecipeGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panWaferMap.SuspendLayout()
        Me.SuspendLayout()
        '
        'bntLoad
        '
        Me.bntLoad.Enabled = False
        Me.bntLoad.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.bntLoad.Location = New System.Drawing.Point(12, 12)
        Me.bntLoad.Name = "bntLoad"
        Me.bntLoad.Size = New System.Drawing.Size(80, 35)
        Me.bntLoad.TabIndex = 0
        Me.bntLoad.Text = "Load"
        Me.bntLoad.UseVisualStyleBackColor = True
        Me.bntLoad.Visible = False
        '
        'OFDLoadRecipe
        '
        Me.OFDLoadRecipe.FileName = "OpenFileDialog1"
        '
        'picDrawGraphicsRecipeGraph
        '
        Me.picDrawGraphicsRecipeGraph.ContextMenuStrip = Me.ContextMenuStrip1
        Me.picDrawGraphicsRecipeGraph.Location = New System.Drawing.Point(382, 497)
        Me.picDrawGraphicsRecipeGraph.Name = "picDrawGraphicsRecipeGraph"
        Me.picDrawGraphicsRecipeGraph.Size = New System.Drawing.Size(400, 250)
        Me.picDrawGraphicsRecipeGraph.TabIndex = 1
        Me.picDrawGraphicsRecipeGraph.TabStop = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'butPIIMap
        '
        Me.butPIIMap.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butPIIMap.Location = New System.Drawing.Point(12, 56)
        Me.butPIIMap.Name = "butPIIMap"
        Me.butPIIMap.Size = New System.Drawing.Size(80, 35)
        Me.butPIIMap.TabIndex = 3
        Me.butPIIMap.Text = "PII Map"
        Me.butPIIMap.UseVisualStyleBackColor = True
        '
        'CbCycle
        '
        Me.CbCycle.AutoSize = True
        Me.CbCycle.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.CbCycle.Location = New System.Drawing.Point(12, 103)
        Me.CbCycle.Name = "CbCycle"
        Me.CbCycle.Size = New System.Drawing.Size(78, 28)
        Me.CbCycle.TabIndex = 4
        Me.CbCycle.Text = "Cycle"
        Me.CbCycle.UseVisualStyleBackColor = True
        '
        'VScrollBar2
        '
        Me.VScrollBar2.Location = New System.Drawing.Point(1087, 388)
        Me.VScrollBar2.Name = "VScrollBar2"
        Me.VScrollBar2.Size = New System.Drawing.Size(0, 0)
        Me.VScrollBar2.TabIndex = 6
        '
        'panWaferMap
        '
        Me.panWaferMap.AutoScroll = True
        Me.panWaferMap.Controls.Add(Me.picDrawGraphicsRecipeGraph)
        Me.panWaferMap.Location = New System.Drawing.Point(160, 12)
        Me.panWaferMap.Name = "panWaferMap"
        Me.panWaferMap.Size = New System.Drawing.Size(800, 500)
        Me.panWaferMap.TabIndex = 8
        '
        'txtScrollX
        '
        Me.txtScrollX.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtScrollX.Location = New System.Drawing.Point(975, 14)
        Me.txtScrollX.Name = "txtScrollX"
        Me.txtScrollX.Size = New System.Drawing.Size(100, 33)
        Me.txtScrollX.TabIndex = 9
        Me.txtScrollX.Visible = False
        '
        'txtScrollY
        '
        Me.txtScrollY.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtScrollY.Location = New System.Drawing.Point(975, 50)
        Me.txtScrollY.Name = "txtScrollY"
        Me.txtScrollY.Size = New System.Drawing.Size(100, 33)
        Me.txtScrollY.TabIndex = 10
        Me.txtScrollY.Visible = False
        '
        'TimerDrawGraphics
        '
        Me.TimerDrawGraphics.Interval = 300
        '
        'butBigger
        '
        Me.butBigger.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butBigger.Location = New System.Drawing.Point(12, 184)
        Me.butBigger.Name = "butBigger"
        Me.butBigger.Size = New System.Drawing.Size(80, 35)
        Me.butBigger.TabIndex = 11
        Me.butBigger.Text = "Bigger"
        Me.butBigger.UseVisualStyleBackColor = True
        '
        'butSmaller
        '
        Me.butSmaller.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.butSmaller.Location = New System.Drawing.Point(12, 235)
        Me.butSmaller.Name = "butSmaller"
        Me.butSmaller.Size = New System.Drawing.Size(80, 35)
        Me.butSmaller.TabIndex = 12
        Me.butSmaller.Text = "Smaller"
        Me.butSmaller.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(970, 138)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(163, 24)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "not work die( x,y)"
        '
        'no_work_save
        '
        Me.no_work_save.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.no_work_save.Location = New System.Drawing.Point(1075, 307)
        Me.no_work_save.Name = "no_work_save"
        Me.no_work_save.Size = New System.Drawing.Size(80, 35)
        Me.no_work_save.TabIndex = 15
        Me.no_work_save.Text = "Save"
        Me.no_work_save.UseVisualStyleBackColor = True
        '
        'no_work_load
        '
        Me.no_work_load.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.no_work_load.Location = New System.Drawing.Point(967, 307)
        Me.no_work_load.Name = "no_work_load"
        Me.no_work_load.Size = New System.Drawing.Size(80, 35)
        Me.no_work_load.TabIndex = 17
        Me.no_work_load.Text = "load file"
        Me.no_work_load.UseVisualStyleBackColor = True
        '
        'Mouse_DIE_locate
        '
        Me.Mouse_DIE_locate.AutoSize = True
        Me.Mouse_DIE_locate.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Mouse_DIE_locate.Location = New System.Drawing.Point(973, 103)
        Me.Mouse_DIE_locate.Name = "Mouse_DIE_locate"
        Me.Mouse_DIE_locate.Size = New System.Drawing.Size(54, 24)
        Me.Mouse_DIE_locate.TabIndex = 18
        Me.Mouse_DIE_locate.Text = "die(,)"
        '
        'no_work_die
        '
        Me.no_work_die.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.no_work_die.FormattingEnabled = True
        Me.no_work_die.ItemHeight = 24
        Me.no_work_die.Location = New System.Drawing.Point(966, 201)
        Me.no_work_die.Name = "no_work_die"
        Me.no_work_die.Size = New System.Drawing.Size(193, 100)
        Me.no_work_die.TabIndex = 19
        '
        'LoadNoWorkDie
        '
        Me.LoadNoWorkDie.FileName = "OpenFileDialog1"
        '
        'frmDrawGraphics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(1191, 620)
        Me.ControlBox = False
        Me.Controls.Add(Me.no_work_die)
        Me.Controls.Add(Me.Mouse_DIE_locate)
        Me.Controls.Add(Me.no_work_load)
        Me.Controls.Add(Me.no_work_save)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.butSmaller)
        Me.Controls.Add(Me.butBigger)
        Me.Controls.Add(Me.txtScrollY)
        Me.Controls.Add(Me.txtScrollX)
        Me.Controls.Add(Me.panWaferMap)
        Me.Controls.Add(Me.VScrollBar2)
        Me.Controls.Add(Me.CbCycle)
        Me.Controls.Add(Me.butPIIMap)
        Me.Controls.Add(Me.bntLoad)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDrawGraphics"
        Me.Text = "DrawGraphics"
        CType(Me.picDrawGraphicsRecipeGraph, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panWaferMap.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bntLoad As System.Windows.Forms.Button
    Friend WithEvents OFDLoadRecipe As System.Windows.Forms.OpenFileDialog
    Friend WithEvents picDrawGraphicsRecipeGraph As System.Windows.Forms.PictureBox
    Friend WithEvents butPIIMap As System.Windows.Forms.Button
    Friend WithEvents CbCycle As System.Windows.Forms.CheckBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents VScrollBar2 As System.Windows.Forms.VScrollBar
    Friend WithEvents panWaferMap As System.Windows.Forms.Panel
    Friend WithEvents txtScrollX As System.Windows.Forms.TextBox
    Friend WithEvents txtScrollY As System.Windows.Forms.TextBox
    Friend WithEvents TimerDrawGraphics As System.Windows.Forms.Timer
    Friend WithEvents butBigger As System.Windows.Forms.Button
    Friend WithEvents butSmaller As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents no_work_save As System.Windows.Forms.Button
    Friend WithEvents no_work_load As System.Windows.Forms.Button
    Friend WithEvents Mouse_DIE_locate As System.Windows.Forms.Label
    Friend WithEvents no_work_die As System.Windows.Forms.ListBox
    Friend WithEvents LoadNoWorkDie As System.Windows.Forms.OpenFileDialog
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManualPageF350A
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManualPageF350A))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnConveyor2 = New System.Windows.Forms.RadioButton()
        Me.rbtnConveyor1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.picInSensor = New System.Windows.Forms.PictureBox()
        Me.GroupBox20 = New System.Windows.Forms.GroupBox()
        Me.picVacuum = New System.Windows.Forms.PictureBox()
        Me.btnVacuum = New System.Windows.Forms.Button()
        Me.GroupBox21 = New System.Windows.Forms.GroupBox()
        Me.picCylinderDown = New System.Windows.Forms.PictureBox()
        Me.picCylinderUp = New System.Windows.Forms.PictureBox()
        Me.btnCylinderUp = New System.Windows.Forms.Button()
        Me.btnCylinderDown = New System.Windows.Forms.Button()
        Me.GroupBox23 = New System.Windows.Forms.GroupBox()
        Me.picUnload = New System.Windows.Forms.PictureBox()
        Me.picLoad = New System.Windows.Forms.PictureBox()
        Me.picInitial = New System.Windows.Forms.PictureBox()
        Me.chkboxAuto = New System.Windows.Forms.CheckBox()
        Me.btnMotionStop = New System.Windows.Forms.Button()
        Me.btnInitial = New System.Windows.Forms.Button()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnUnload = New System.Windows.Forms.Button()
        Me.GroupBox24 = New System.Windows.Forms.GroupBox()
        Me.nmcStartLocX = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.rbtnReversal = New System.Windows.Forms.RadioButton()
        Me.rbtnForward = New System.Windows.Forms.RadioButton()
        Me.btnRollerStop = New System.Windows.Forms.Button()
        Me.btnRollerStart = New System.Windows.Forms.Button()
        Me.GroupBox25 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.picStoperDown = New System.Windows.Forms.PictureBox()
        Me.picStoperUp = New System.Windows.Forms.PictureBox()
        Me.btnStoperUp = New System.Windows.Forms.Button()
        Me.picStoperSensor = New System.Windows.Forms.PictureBox()
        Me.btnStoperDown = New System.Windows.Forms.Button()
        Me.timer_UpdateStatus = New System.Windows.Forms.Timer(Me.components)
        Me.imgListItems = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnConveterOpen = New System.Windows.Forms.Button()
        Me.btnConveterClose = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        CType(Me.picInSensor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox20.SuspendLayout()
        CType(Me.picVacuum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox21.SuspendLayout()
        CType(Me.picCylinderDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCylinderUp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox23.SuspendLayout()
        CType(Me.picUnload, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picInitial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox24.SuspendLayout()
        CType(Me.nmcStartLocX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox25.SuspendLayout()
        CType(Me.picStoperDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStoperUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStoperSensor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnConveyor2)
        Me.GroupBox1.Controls.Add(Me.rbtnConveyor1)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'rbtnConveyor2
        '
        resources.ApplyResources(Me.rbtnConveyor2, "rbtnConveyor2")
        Me.rbtnConveyor2.ForeColor = System.Drawing.Color.Black
        Me.rbtnConveyor2.Name = "rbtnConveyor2"
        Me.rbtnConveyor2.UseVisualStyleBackColor = True
        '
        'rbtnConveyor1
        '
        resources.ApplyResources(Me.rbtnConveyor1, "rbtnConveyor1")
        Me.rbtnConveyor1.Checked = True
        Me.rbtnConveyor1.ForeColor = System.Drawing.Color.Black
        Me.rbtnConveyor1.Name = "rbtnConveyor1"
        Me.rbtnConveyor1.TabStop = True
        Me.rbtnConveyor1.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.Label5)
        Me.GroupBox8.Controls.Add(Me.picInSensor)
        resources.ApplyResources(Me.GroupBox8, "GroupBox8")
        Me.GroupBox8.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.TabStop = False
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label5.Name = "Label5"
        '
        'picInSensor
        '
        resources.ApplyResources(Me.picInSensor, "picInSensor")
        Me.picInSensor.Name = "picInSensor"
        Me.picInSensor.TabStop = False
        '
        'GroupBox20
        '
        Me.GroupBox20.Controls.Add(Me.picVacuum)
        Me.GroupBox20.Controls.Add(Me.btnVacuum)
        resources.ApplyResources(Me.GroupBox20, "GroupBox20")
        Me.GroupBox20.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox20.Name = "GroupBox20"
        Me.GroupBox20.TabStop = False
        '
        'picVacuum
        '
        Me.picVacuum.Image = Global.WetcoConveyor.My.Resources.Resources.BlackLight_01
        resources.ApplyResources(Me.picVacuum, "picVacuum")
        Me.picVacuum.Name = "picVacuum"
        Me.picVacuum.TabStop = False
        '
        'btnVacuum
        '
        resources.ApplyResources(Me.btnVacuum, "btnVacuum")
        Me.btnVacuum.ForeColor = System.Drawing.Color.Black
        Me.btnVacuum.Name = "btnVacuum"
        Me.btnVacuum.UseVisualStyleBackColor = True
        '
        'GroupBox21
        '
        Me.GroupBox21.Controls.Add(Me.picCylinderDown)
        Me.GroupBox21.Controls.Add(Me.picCylinderUp)
        Me.GroupBox21.Controls.Add(Me.btnCylinderUp)
        Me.GroupBox21.Controls.Add(Me.btnCylinderDown)
        resources.ApplyResources(Me.GroupBox21, "GroupBox21")
        Me.GroupBox21.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox21.Name = "GroupBox21"
        Me.GroupBox21.TabStop = False
        '
        'picCylinderDown
        '
        resources.ApplyResources(Me.picCylinderDown, "picCylinderDown")
        Me.picCylinderDown.Name = "picCylinderDown"
        Me.picCylinderDown.TabStop = False
        '
        'picCylinderUp
        '
        Me.picCylinderUp.Image = Global.WetcoConveyor.My.Resources.Resources.BlackLight_01
        resources.ApplyResources(Me.picCylinderUp, "picCylinderUp")
        Me.picCylinderUp.Name = "picCylinderUp"
        Me.picCylinderUp.TabStop = False
        '
        'btnCylinderUp
        '
        resources.ApplyResources(Me.btnCylinderUp, "btnCylinderUp")
        Me.btnCylinderUp.ForeColor = System.Drawing.Color.Black
        Me.btnCylinderUp.Name = "btnCylinderUp"
        Me.btnCylinderUp.UseVisualStyleBackColor = True
        '
        'btnCylinderDown
        '
        resources.ApplyResources(Me.btnCylinderDown, "btnCylinderDown")
        Me.btnCylinderDown.ForeColor = System.Drawing.Color.Black
        Me.btnCylinderDown.Name = "btnCylinderDown"
        Me.btnCylinderDown.UseVisualStyleBackColor = True
        '
        'GroupBox23
        '
        Me.GroupBox23.Controls.Add(Me.picUnload)
        Me.GroupBox23.Controls.Add(Me.picLoad)
        Me.GroupBox23.Controls.Add(Me.picInitial)
        Me.GroupBox23.Controls.Add(Me.chkboxAuto)
        Me.GroupBox23.Controls.Add(Me.btnMotionStop)
        Me.GroupBox23.Controls.Add(Me.btnInitial)
        Me.GroupBox23.Controls.Add(Me.btnLoad)
        Me.GroupBox23.Controls.Add(Me.btnUnload)
        resources.ApplyResources(Me.GroupBox23, "GroupBox23")
        Me.GroupBox23.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox23.Name = "GroupBox23"
        Me.GroupBox23.TabStop = False
        '
        'picUnload
        '
        resources.ApplyResources(Me.picUnload, "picUnload")
        Me.picUnload.Name = "picUnload"
        Me.picUnload.TabStop = False
        '
        'picLoad
        '
        resources.ApplyResources(Me.picLoad, "picLoad")
        Me.picLoad.Name = "picLoad"
        Me.picLoad.TabStop = False
        '
        'picInitial
        '
        resources.ApplyResources(Me.picInitial, "picInitial")
        Me.picInitial.Name = "picInitial"
        Me.picInitial.TabStop = False
        '
        'chkboxAuto
        '
        resources.ApplyResources(Me.chkboxAuto, "chkboxAuto")
        Me.chkboxAuto.ForeColor = System.Drawing.Color.Black
        Me.chkboxAuto.Name = "chkboxAuto"
        Me.chkboxAuto.UseVisualStyleBackColor = True
        '
        'btnMotionStop
        '
        resources.ApplyResources(Me.btnMotionStop, "btnMotionStop")
        Me.btnMotionStop.ForeColor = System.Drawing.Color.Black
        Me.btnMotionStop.Name = "btnMotionStop"
        Me.btnMotionStop.UseVisualStyleBackColor = True
        '
        'btnInitial
        '
        resources.ApplyResources(Me.btnInitial, "btnInitial")
        Me.btnInitial.ForeColor = System.Drawing.Color.Black
        Me.btnInitial.Name = "btnInitial"
        Me.btnInitial.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        resources.ApplyResources(Me.btnLoad, "btnLoad")
        Me.btnLoad.ForeColor = System.Drawing.Color.Black
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnUnload
        '
        resources.ApplyResources(Me.btnUnload, "btnUnload")
        Me.btnUnload.ForeColor = System.Drawing.Color.Black
        Me.btnUnload.Name = "btnUnload"
        Me.btnUnload.UseVisualStyleBackColor = True
        '
        'GroupBox24
        '
        Me.GroupBox24.Controls.Add(Me.nmcStartLocX)
        Me.GroupBox24.Controls.Add(Me.Label1)
        Me.GroupBox24.Controls.Add(Me.Label7)
        Me.GroupBox24.Controls.Add(Me.rbtnReversal)
        Me.GroupBox24.Controls.Add(Me.rbtnForward)
        Me.GroupBox24.Controls.Add(Me.btnRollerStop)
        Me.GroupBox24.Controls.Add(Me.btnRollerStart)
        resources.ApplyResources(Me.GroupBox24, "GroupBox24")
        Me.GroupBox24.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox24.Name = "GroupBox24"
        Me.GroupBox24.TabStop = False
        '
        'nmcStartLocX
        '
        Me.nmcStartLocX.DecimalPlaces = 1
        resources.ApplyResources(Me.nmcStartLocX, "nmcStartLocX")
        Me.nmcStartLocX.Maximum = New Decimal(New Integer() {999, 0, 0, 65536})
        Me.nmcStartLocX.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nmcStartLocX.Name = "nmcStartLocX"
        Me.nmcStartLocX.Value = New Decimal(New Integer() {999, 0, 0, 65536})
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Name = "Label1"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label7.Name = "Label7"
        '
        'rbtnReversal
        '
        resources.ApplyResources(Me.rbtnReversal, "rbtnReversal")
        Me.rbtnReversal.ForeColor = System.Drawing.Color.Black
        Me.rbtnReversal.Name = "rbtnReversal"
        Me.rbtnReversal.UseVisualStyleBackColor = True
        '
        'rbtnForward
        '
        resources.ApplyResources(Me.rbtnForward, "rbtnForward")
        Me.rbtnForward.Checked = True
        Me.rbtnForward.ForeColor = System.Drawing.Color.Black
        Me.rbtnForward.Name = "rbtnForward"
        Me.rbtnForward.TabStop = True
        Me.rbtnForward.UseVisualStyleBackColor = True
        '
        'btnRollerStop
        '
        resources.ApplyResources(Me.btnRollerStop, "btnRollerStop")
        Me.btnRollerStop.ForeColor = System.Drawing.Color.Black
        Me.btnRollerStop.Name = "btnRollerStop"
        Me.btnRollerStop.UseVisualStyleBackColor = True
        '
        'btnRollerStart
        '
        resources.ApplyResources(Me.btnRollerStart, "btnRollerStart")
        Me.btnRollerStart.ForeColor = System.Drawing.Color.Black
        Me.btnRollerStart.Name = "btnRollerStart"
        Me.btnRollerStart.UseVisualStyleBackColor = True
        '
        'GroupBox25
        '
        Me.GroupBox25.Controls.Add(Me.Label10)
        Me.GroupBox25.Controls.Add(Me.picStoperDown)
        Me.GroupBox25.Controls.Add(Me.picStoperUp)
        Me.GroupBox25.Controls.Add(Me.btnStoperUp)
        Me.GroupBox25.Controls.Add(Me.picStoperSensor)
        Me.GroupBox25.Controls.Add(Me.btnStoperDown)
        resources.ApplyResources(Me.GroupBox25, "GroupBox25")
        Me.GroupBox25.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox25.Name = "GroupBox25"
        Me.GroupBox25.TabStop = False
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label10.Name = "Label10"
        '
        'picStoperDown
        '
        resources.ApplyResources(Me.picStoperDown, "picStoperDown")
        Me.picStoperDown.Name = "picStoperDown"
        Me.picStoperDown.TabStop = False
        '
        'picStoperUp
        '
        Me.picStoperUp.Image = Global.WetcoConveyor.My.Resources.Resources.BlackLight_01
        resources.ApplyResources(Me.picStoperUp, "picStoperUp")
        Me.picStoperUp.Name = "picStoperUp"
        Me.picStoperUp.TabStop = False
        '
        'btnStoperUp
        '
        resources.ApplyResources(Me.btnStoperUp, "btnStoperUp")
        Me.btnStoperUp.ForeColor = System.Drawing.Color.Black
        Me.btnStoperUp.Name = "btnStoperUp"
        Me.btnStoperUp.UseVisualStyleBackColor = True
        '
        'picStoperSensor
        '
        resources.ApplyResources(Me.picStoperSensor, "picStoperSensor")
        Me.picStoperSensor.Name = "picStoperSensor"
        Me.picStoperSensor.TabStop = False
        '
        'btnStoperDown
        '
        resources.ApplyResources(Me.btnStoperDown, "btnStoperDown")
        Me.btnStoperDown.ForeColor = System.Drawing.Color.Black
        Me.btnStoperDown.Name = "btnStoperDown"
        Me.btnStoperDown.UseVisualStyleBackColor = True
        '
        'timer_UpdateStatus
        '
        Me.timer_UpdateStatus.Interval = 300
        '
        'imgListItems
        '
        Me.imgListItems.ImageStream = CType(resources.GetObject("imgListItems.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgListItems.TransparentColor = System.Drawing.Color.Transparent
        Me.imgListItems.Images.SetKeyName(0, "Light-Green01.png")
        Me.imgListItems.Images.SetKeyName(1, "Light-Black01.png")
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnConveterOpen)
        Me.GroupBox2.Controls.Add(Me.btnConveterClose)
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'btnConveterOpen
        '
        resources.ApplyResources(Me.btnConveterOpen, "btnConveterOpen")
        Me.btnConveterOpen.ForeColor = System.Drawing.Color.Black
        Me.btnConveterOpen.Name = "btnConveterOpen"
        Me.btnConveterOpen.UseVisualStyleBackColor = True
        '
        'btnConveterClose
        '
        resources.ApplyResources(Me.btnConveterClose, "btnConveterClose")
        Me.btnConveterClose.ForeColor = System.Drawing.Color.Black
        Me.btnConveterClose.Name = "btnConveterClose"
        Me.btnConveterClose.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.PictureBox2, "PictureBox2")
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.TabStop = False
        '
        'frmManualPageF350A
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.GroupBox20)
        Me.Controls.Add(Me.GroupBox21)
        Me.Controls.Add(Me.GroupBox23)
        Me.Controls.Add(Me.GroupBox24)
        Me.Controls.Add(Me.GroupBox25)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmManualPageF350A"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        CType(Me.picInSensor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox20.ResumeLayout(False)
        CType(Me.picVacuum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox21.ResumeLayout(False)
        CType(Me.picCylinderDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCylinderUp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox23.ResumeLayout(False)
        Me.GroupBox23.PerformLayout()
        CType(Me.picUnload, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picInitial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox24.ResumeLayout(False)
        Me.GroupBox24.PerformLayout()
        CType(Me.nmcStartLocX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox25.ResumeLayout(False)
        Me.GroupBox25.PerformLayout()
        CType(Me.picStoperDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStoperUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStoperSensor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnConveyor2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnConveyor1 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents picInSensor As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox20 As System.Windows.Forms.GroupBox
    Private WithEvents picVacuum As System.Windows.Forms.PictureBox
    Private WithEvents btnVacuum As System.Windows.Forms.Button
    Friend WithEvents GroupBox21 As System.Windows.Forms.GroupBox
    Private WithEvents picCylinderDown As System.Windows.Forms.PictureBox
    Private WithEvents picCylinderUp As System.Windows.Forms.PictureBox
    Private WithEvents btnCylinderUp As System.Windows.Forms.Button
    Private WithEvents btnCylinderDown As System.Windows.Forms.Button
    Friend WithEvents GroupBox23 As System.Windows.Forms.GroupBox
    Friend WithEvents chkboxAuto As System.Windows.Forms.CheckBox
    Private WithEvents btnMotionStop As System.Windows.Forms.Button
    Private WithEvents btnInitial As System.Windows.Forms.Button
    Private WithEvents btnLoad As System.Windows.Forms.Button
    Private WithEvents btnUnload As System.Windows.Forms.Button
    Friend WithEvents GroupBox24 As System.Windows.Forms.GroupBox
    Friend WithEvents nmcStartLocX As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents rbtnReversal As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnForward As System.Windows.Forms.RadioButton
    Private WithEvents btnRollerStop As System.Windows.Forms.Button
    Private WithEvents btnRollerStart As System.Windows.Forms.Button
    Friend WithEvents GroupBox25 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents picStoperDown As System.Windows.Forms.PictureBox
    Private WithEvents picStoperUp As System.Windows.Forms.PictureBox
    Private WithEvents btnStoperUp As System.Windows.Forms.Button
    Private WithEvents picStoperSensor As System.Windows.Forms.PictureBox
    Private WithEvents btnStoperDown As System.Windows.Forms.Button
    Friend WithEvents timer_UpdateStatus As System.Windows.Forms.Timer
    Friend WithEvents imgListItems As System.Windows.Forms.ImageList
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Private WithEvents btnConveterOpen As System.Windows.Forms.Button
    Private WithEvents btnConveterClose As System.Windows.Forms.Button
    Private WithEvents picUnload As System.Windows.Forms.PictureBox
    Private WithEvents picLoad As System.Windows.Forms.PictureBox
    Private WithEvents picInitial As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class

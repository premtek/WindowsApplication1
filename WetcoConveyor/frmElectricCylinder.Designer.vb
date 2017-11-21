<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmElectricCylinder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmElectricCylinder))
        Me.rbtnA_1000 = New System.Windows.Forms.RadioButton()
        Me.rbtnA_100 = New System.Windows.Forms.RadioButton()
        Me.rbtnA_10 = New System.Windows.Forms.RadioButton()
        Me.rbtnA_1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.nudA_TestSpeed = New System.Windows.Forms.NumericUpDown()
        Me.lblA_Status = New System.Windows.Forms.Label()
        Me.btnA_SetSpeed = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.nudA_Speed = New System.Windows.Forms.NumericUpDown()
        Me.btnA_Save = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tbA_Bottom = New System.Windows.Forms.TextBox()
        Me.tbA_Top = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnA_Home = New System.Windows.Forms.Button()
        Me.btnA_Move = New System.Windows.Forms.Button()
        Me.tbA_Position = New System.Windows.Forms.TextBox()
        Me.btnA_Up = New System.Windows.Forms.Button()
        Me.btnA_Down = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.nudB_TestSpeed = New System.Windows.Forms.NumericUpDown()
        Me.btnB_SetSpeed = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblB_Status = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.nudB_Speed = New System.Windows.Forms.NumericUpDown()
        Me.btnB_Save = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbB_Bottom = New System.Windows.Forms.TextBox()
        Me.tbB_Top = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnB_Home = New System.Windows.Forms.Button()
        Me.btnB_Move = New System.Windows.Forms.Button()
        Me.tbB_Position = New System.Windows.Forms.TextBox()
        Me.btnB_Up = New System.Windows.Forms.Button()
        Me.btnB_Down = New System.Windows.Forms.Button()
        Me.rbtnB_1 = New System.Windows.Forms.RadioButton()
        Me.rbtnB_1000 = New System.Windows.Forms.RadioButton()
        Me.rbtnB_10 = New System.Windows.Forms.RadioButton()
        Me.rbtnB_100 = New System.Windows.Forms.RadioButton()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.timer_GetPosition = New System.Windows.Forms.Timer(Me.components)
        Me.UcStatusBar1 = New ProjectAOI.ucStatusBar()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        CType(Me.nudA_TestSpeed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.nudA_Speed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.nudB_TestSpeed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.nudB_Speed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rbtnA_1000
        '
        resources.ApplyResources(Me.rbtnA_1000, "rbtnA_1000")
        Me.rbtnA_1000.Name = "rbtnA_1000"
        Me.ToolTip1.SetToolTip(Me.rbtnA_1000, resources.GetString("rbtnA_1000.ToolTip"))
        Me.rbtnA_1000.UseVisualStyleBackColor = True
        '
        'rbtnA_100
        '
        resources.ApplyResources(Me.rbtnA_100, "rbtnA_100")
        Me.rbtnA_100.Name = "rbtnA_100"
        Me.ToolTip1.SetToolTip(Me.rbtnA_100, resources.GetString("rbtnA_100.ToolTip"))
        Me.rbtnA_100.UseVisualStyleBackColor = True
        '
        'rbtnA_10
        '
        resources.ApplyResources(Me.rbtnA_10, "rbtnA_10")
        Me.rbtnA_10.Name = "rbtnA_10"
        Me.ToolTip1.SetToolTip(Me.rbtnA_10, resources.GetString("rbtnA_10.ToolTip"))
        Me.rbtnA_10.UseVisualStyleBackColor = True
        '
        'rbtnA_1
        '
        resources.ApplyResources(Me.rbtnA_1, "rbtnA_1")
        Me.rbtnA_1.Checked = True
        Me.rbtnA_1.Name = "rbtnA_1"
        Me.rbtnA_1.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rbtnA_1, resources.GetString("rbtnA_1.ToolTip"))
        Me.rbtnA_1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.nudA_TestSpeed)
        Me.GroupBox1.Controls.Add(Me.lblA_Status)
        Me.GroupBox1.Controls.Add(Me.btnA_SetSpeed)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.btnA_Home)
        Me.GroupBox1.Controls.Add(Me.btnA_Move)
        Me.GroupBox1.Controls.Add(Me.tbA_Position)
        Me.GroupBox1.Controls.Add(Me.btnA_Up)
        Me.GroupBox1.Controls.Add(Me.btnA_Down)
        Me.GroupBox1.Controls.Add(Me.rbtnA_1)
        Me.GroupBox1.Controls.Add(Me.rbtnA_1000)
        Me.GroupBox1.Controls.Add(Me.rbtnA_10)
        Me.GroupBox1.Controls.Add(Me.rbtnA_100)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'nudA_TestSpeed
        '
        resources.ApplyResources(Me.nudA_TestSpeed, "nudA_TestSpeed")
        Me.nudA_TestSpeed.Name = "nudA_TestSpeed"
        Me.ToolTip1.SetToolTip(Me.nudA_TestSpeed, resources.GetString("nudA_TestSpeed.ToolTip"))
        '
        'lblA_Status
        '
        resources.ApplyResources(Me.lblA_Status, "lblA_Status")
        Me.lblA_Status.BackColor = System.Drawing.Color.White
        Me.lblA_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblA_Status.Name = "lblA_Status"
        Me.ToolTip1.SetToolTip(Me.lblA_Status, resources.GetString("lblA_Status.ToolTip"))
        '
        'btnA_SetSpeed
        '
        resources.ApplyResources(Me.btnA_SetSpeed, "btnA_SetSpeed")
        Me.btnA_SetSpeed.ForeColor = System.Drawing.Color.Black
        Me.btnA_SetSpeed.Name = "btnA_SetSpeed"
        Me.ToolTip1.SetToolTip(Me.btnA_SetSpeed, resources.GetString("btnA_SetSpeed.ToolTip"))
        Me.btnA_SetSpeed.UseVisualStyleBackColor = True
        '
        'Label16
        '
        resources.ApplyResources(Me.Label16, "Label16")
        Me.Label16.Name = "Label16"
        Me.ToolTip1.SetToolTip(Me.Label16, resources.GetString("Label16.ToolTip"))
        '
        'GroupBox3
        '
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Controls.Add(Me.nudA_Speed)
        Me.GroupBox3.Controls.Add(Me.btnA_Save)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.tbA_Bottom)
        Me.GroupBox3.Controls.Add(Me.tbA_Top)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox3, resources.GetString("GroupBox3.ToolTip"))
        '
        'nudA_Speed
        '
        resources.ApplyResources(Me.nudA_Speed, "nudA_Speed")
        Me.nudA_Speed.Name = "nudA_Speed"
        Me.ToolTip1.SetToolTip(Me.nudA_Speed, resources.GetString("nudA_Speed.ToolTip"))
        '
        'btnA_Save
        '
        resources.ApplyResources(Me.btnA_Save, "btnA_Save")
        Me.btnA_Save.ForeColor = System.Drawing.Color.Black
        Me.btnA_Save.Image = Global.WetcoConveyor.My.Resources.Resources.Save
        Me.btnA_Save.Name = "btnA_Save"
        Me.ToolTip1.SetToolTip(Me.btnA_Save, resources.GetString("btnA_Save.ToolTip"))
        Me.btnA_Save.UseVisualStyleBackColor = True
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        Me.ToolTip1.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip"))
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        Me.ToolTip1.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip"))
        '
        'tbA_Bottom
        '
        resources.ApplyResources(Me.tbA_Bottom, "tbA_Bottom")
        Me.tbA_Bottom.Name = "tbA_Bottom"
        Me.ToolTip1.SetToolTip(Me.tbA_Bottom, resources.GetString("tbA_Bottom.ToolTip"))
        '
        'tbA_Top
        '
        resources.ApplyResources(Me.tbA_Top, "tbA_Top")
        Me.tbA_Top.Name = "tbA_Top"
        Me.ToolTip1.SetToolTip(Me.tbA_Top, resources.GetString("tbA_Top.ToolTip"))
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        Me.ToolTip1.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip"))
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        Me.ToolTip1.SetToolTip(Me.Label4, resources.GetString("Label4.ToolTip"))
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        Me.ToolTip1.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip"))
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        Me.ToolTip1.SetToolTip(Me.Label2, resources.GetString("Label2.ToolTip"))
        '
        'btnA_Home
        '
        resources.ApplyResources(Me.btnA_Home, "btnA_Home")
        Me.btnA_Home.ForeColor = System.Drawing.Color.Black
        Me.btnA_Home.Image = Global.WetcoConveyor.My.Resources.Resources.i33b_100
        Me.btnA_Home.Name = "btnA_Home"
        Me.ToolTip1.SetToolTip(Me.btnA_Home, resources.GetString("btnA_Home.ToolTip"))
        Me.btnA_Home.UseVisualStyleBackColor = True
        '
        'btnA_Move
        '
        resources.ApplyResources(Me.btnA_Move, "btnA_Move")
        Me.btnA_Move.ForeColor = System.Drawing.Color.Black
        Me.btnA_Move.Image = Global.WetcoConveyor.My.Resources.Resources.i33b_174
        Me.btnA_Move.Name = "btnA_Move"
        Me.ToolTip1.SetToolTip(Me.btnA_Move, resources.GetString("btnA_Move.ToolTip"))
        Me.btnA_Move.UseVisualStyleBackColor = True
        '
        'tbA_Position
        '
        resources.ApplyResources(Me.tbA_Position, "tbA_Position")
        Me.tbA_Position.Name = "tbA_Position"
        Me.ToolTip1.SetToolTip(Me.tbA_Position, resources.GetString("tbA_Position.ToolTip"))
        '
        'btnA_Up
        '
        resources.ApplyResources(Me.btnA_Up, "btnA_Up")
        Me.btnA_Up.ForeColor = System.Drawing.Color.Black
        Me.btnA_Up.Image = Global.WetcoConveyor.My.Resources.Resources.I33_110
        Me.btnA_Up.Name = "btnA_Up"
        Me.ToolTip1.SetToolTip(Me.btnA_Up, resources.GetString("btnA_Up.ToolTip"))
        Me.btnA_Up.UseVisualStyleBackColor = True
        '
        'btnA_Down
        '
        resources.ApplyResources(Me.btnA_Down, "btnA_Down")
        Me.btnA_Down.ForeColor = System.Drawing.Color.Black
        Me.btnA_Down.Image = Global.WetcoConveyor.My.Resources.Resources.I33_156
        Me.btnA_Down.Name = "btnA_Down"
        Me.ToolTip1.SetToolTip(Me.btnA_Down, resources.GetString("btnA_Down.ToolTip"))
        Me.btnA_Down.UseVisualStyleBackColor = True
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        Me.ToolTip1.SetToolTip(Me.Label13, resources.GetString("Label13.ToolTip"))
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.Name = "Label15"
        Me.ToolTip1.SetToolTip(Me.Label15, resources.GetString("Label15.ToolTip"))
        '
        'GroupBox2
        '
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Controls.Add(Me.nudB_TestSpeed)
        Me.GroupBox2.Controls.Add(Me.btnB_SetSpeed)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.lblB_Status)
        Me.GroupBox2.Controls.Add(Me.GroupBox4)
        Me.GroupBox2.Controls.Add(Me.btnB_Home)
        Me.GroupBox2.Controls.Add(Me.btnB_Move)
        Me.GroupBox2.Controls.Add(Me.tbB_Position)
        Me.GroupBox2.Controls.Add(Me.btnB_Up)
        Me.GroupBox2.Controls.Add(Me.btnB_Down)
        Me.GroupBox2.Controls.Add(Me.rbtnB_1)
        Me.GroupBox2.Controls.Add(Me.rbtnB_1000)
        Me.GroupBox2.Controls.Add(Me.rbtnB_10)
        Me.GroupBox2.Controls.Add(Me.rbtnB_100)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox2, resources.GetString("GroupBox2.ToolTip"))
        '
        'nudB_TestSpeed
        '
        resources.ApplyResources(Me.nudB_TestSpeed, "nudB_TestSpeed")
        Me.nudB_TestSpeed.Name = "nudB_TestSpeed"
        Me.ToolTip1.SetToolTip(Me.nudB_TestSpeed, resources.GetString("nudB_TestSpeed.ToolTip"))
        '
        'btnB_SetSpeed
        '
        resources.ApplyResources(Me.btnB_SetSpeed, "btnB_SetSpeed")
        Me.btnB_SetSpeed.ForeColor = System.Drawing.Color.Black
        Me.btnB_SetSpeed.Name = "btnB_SetSpeed"
        Me.ToolTip1.SetToolTip(Me.btnB_SetSpeed, resources.GetString("btnB_SetSpeed.ToolTip"))
        Me.btnB_SetSpeed.UseVisualStyleBackColor = True
        '
        'Label17
        '
        resources.ApplyResources(Me.Label17, "Label17")
        Me.Label17.Name = "Label17"
        Me.ToolTip1.SetToolTip(Me.Label17, resources.GetString("Label17.ToolTip"))
        '
        'Label18
        '
        resources.ApplyResources(Me.Label18, "Label18")
        Me.Label18.Name = "Label18"
        Me.ToolTip1.SetToolTip(Me.Label18, resources.GetString("Label18.ToolTip"))
        '
        'lblB_Status
        '
        resources.ApplyResources(Me.lblB_Status, "lblB_Status")
        Me.lblB_Status.BackColor = System.Drawing.Color.White
        Me.lblB_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblB_Status.Name = "lblB_Status"
        Me.ToolTip1.SetToolTip(Me.lblB_Status, resources.GetString("lblB_Status.ToolTip"))
        '
        'GroupBox4
        '
        resources.ApplyResources(Me.GroupBox4, "GroupBox4")
        Me.GroupBox4.Controls.Add(Me.nudB_Speed)
        Me.GroupBox4.Controls.Add(Me.btnB_Save)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.tbB_Bottom)
        Me.GroupBox4.Controls.Add(Me.tbB_Top)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox4, resources.GetString("GroupBox4.ToolTip"))
        '
        'nudB_Speed
        '
        resources.ApplyResources(Me.nudB_Speed, "nudB_Speed")
        Me.nudB_Speed.Name = "nudB_Speed"
        Me.ToolTip1.SetToolTip(Me.nudB_Speed, resources.GetString("nudB_Speed.ToolTip"))
        '
        'btnB_Save
        '
        resources.ApplyResources(Me.btnB_Save, "btnB_Save")
        Me.btnB_Save.ForeColor = System.Drawing.Color.Black
        Me.btnB_Save.Image = Global.WetcoConveyor.My.Resources.Resources.Save
        Me.btnB_Save.Name = "btnB_Save"
        Me.ToolTip1.SetToolTip(Me.btnB_Save, resources.GetString("btnB_Save.ToolTip"))
        Me.btnB_Save.UseVisualStyleBackColor = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        Me.ToolTip1.SetToolTip(Me.Label8, resources.GetString("Label8.ToolTip"))
        '
        'tbB_Bottom
        '
        resources.ApplyResources(Me.tbB_Bottom, "tbB_Bottom")
        Me.tbB_Bottom.Name = "tbB_Bottom"
        Me.ToolTip1.SetToolTip(Me.tbB_Bottom, resources.GetString("tbB_Bottom.ToolTip"))
        '
        'tbB_Top
        '
        resources.ApplyResources(Me.tbB_Top, "tbB_Top")
        Me.tbB_Top.Name = "tbB_Top"
        Me.ToolTip1.SetToolTip(Me.tbB_Top, resources.GetString("tbB_Top.ToolTip"))
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        Me.ToolTip1.SetToolTip(Me.Label9, resources.GetString("Label9.ToolTip"))
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        Me.ToolTip1.SetToolTip(Me.Label10, resources.GetString("Label10.ToolTip"))
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        Me.ToolTip1.SetToolTip(Me.Label11, resources.GetString("Label11.ToolTip"))
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        Me.ToolTip1.SetToolTip(Me.Label12, resources.GetString("Label12.ToolTip"))
        '
        'btnB_Home
        '
        resources.ApplyResources(Me.btnB_Home, "btnB_Home")
        Me.btnB_Home.ForeColor = System.Drawing.Color.Black
        Me.btnB_Home.Image = Global.WetcoConveyor.My.Resources.Resources.i33b_100
        Me.btnB_Home.Name = "btnB_Home"
        Me.ToolTip1.SetToolTip(Me.btnB_Home, resources.GetString("btnB_Home.ToolTip"))
        Me.btnB_Home.UseVisualStyleBackColor = True
        '
        'btnB_Move
        '
        resources.ApplyResources(Me.btnB_Move, "btnB_Move")
        Me.btnB_Move.ForeColor = System.Drawing.Color.Black
        Me.btnB_Move.Image = Global.WetcoConveyor.My.Resources.Resources.i33b_174
        Me.btnB_Move.Name = "btnB_Move"
        Me.ToolTip1.SetToolTip(Me.btnB_Move, resources.GetString("btnB_Move.ToolTip"))
        Me.btnB_Move.UseVisualStyleBackColor = True
        '
        'tbB_Position
        '
        resources.ApplyResources(Me.tbB_Position, "tbB_Position")
        Me.tbB_Position.Name = "tbB_Position"
        Me.ToolTip1.SetToolTip(Me.tbB_Position, resources.GetString("tbB_Position.ToolTip"))
        '
        'btnB_Up
        '
        resources.ApplyResources(Me.btnB_Up, "btnB_Up")
        Me.btnB_Up.ForeColor = System.Drawing.Color.Black
        Me.btnB_Up.Image = Global.WetcoConveyor.My.Resources.Resources.I33_110
        Me.btnB_Up.Name = "btnB_Up"
        Me.ToolTip1.SetToolTip(Me.btnB_Up, resources.GetString("btnB_Up.ToolTip"))
        Me.btnB_Up.UseVisualStyleBackColor = True
        '
        'btnB_Down
        '
        resources.ApplyResources(Me.btnB_Down, "btnB_Down")
        Me.btnB_Down.ForeColor = System.Drawing.Color.Black
        Me.btnB_Down.Image = Global.WetcoConveyor.My.Resources.Resources.I33_156
        Me.btnB_Down.Name = "btnB_Down"
        Me.ToolTip1.SetToolTip(Me.btnB_Down, resources.GetString("btnB_Down.ToolTip"))
        Me.btnB_Down.UseVisualStyleBackColor = True
        '
        'rbtnB_1
        '
        resources.ApplyResources(Me.rbtnB_1, "rbtnB_1")
        Me.rbtnB_1.Checked = True
        Me.rbtnB_1.Name = "rbtnB_1"
        Me.rbtnB_1.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rbtnB_1, resources.GetString("rbtnB_1.ToolTip"))
        Me.rbtnB_1.UseVisualStyleBackColor = True
        '
        'rbtnB_1000
        '
        resources.ApplyResources(Me.rbtnB_1000, "rbtnB_1000")
        Me.rbtnB_1000.Name = "rbtnB_1000"
        Me.ToolTip1.SetToolTip(Me.rbtnB_1000, resources.GetString("rbtnB_1000.ToolTip"))
        Me.rbtnB_1000.UseVisualStyleBackColor = True
        '
        'rbtnB_10
        '
        resources.ApplyResources(Me.rbtnB_10, "rbtnB_10")
        Me.rbtnB_10.Name = "rbtnB_10"
        Me.ToolTip1.SetToolTip(Me.rbtnB_10, resources.GetString("rbtnB_10.ToolTip"))
        Me.rbtnB_10.UseVisualStyleBackColor = True
        '
        'rbtnB_100
        '
        resources.ApplyResources(Me.rbtnB_100, "rbtnB_100")
        Me.rbtnB_100.Name = "rbtnB_100"
        Me.ToolTip1.SetToolTip(Me.rbtnB_100, resources.GetString("rbtnB_100.ToolTip"))
        Me.rbtnB_100.UseVisualStyleBackColor = True
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        Me.ToolTip1.SetToolTip(Me.Label14, resources.GetString("Label14.ToolTip"))
        '
        'timer_GetPosition
        '
        '
        'UcStatusBar1
        '
        resources.ApplyResources(Me.UcStatusBar1, "UcStatusBar1")
        Me.UcStatusBar1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcStatusBar1.Name = "UcStatusBar1"
        Me.ToolTip1.SetToolTip(Me.UcStatusBar1, resources.GetString("UcStatusBar1.ToolTip"))
        '
        'btnExit
        '
        resources.ApplyResources(Me.btnExit, "btnExit")
        Me.btnExit.BackgroundImage = Global.WetcoConveyor.My.Resources.Resources.CancelExit1
        Me.btnExit.Name = "btnExit"
        Me.ToolTip1.SetToolTip(Me.btnExit, resources.GetString("btnExit.ToolTip"))
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmElectricCylinder
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.UcStatusBar1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmElectricCylinder"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nudA_TestSpeed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.nudA_Speed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.nudB_TestSpeed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.nudB_Speed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents btnA_Down As System.Windows.Forms.Button
    Friend WithEvents rbtnA_1000 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnA_100 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnA_10 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnA_1 As System.Windows.Forms.RadioButton
    Private WithEvents btnA_Up As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents btnA_Move As System.Windows.Forms.Button
    Friend WithEvents tbA_Position As System.Windows.Forms.TextBox
    Private WithEvents btnA_Home As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents btnA_Save As System.Windows.Forms.Button
    Friend WithEvents tbA_Bottom As System.Windows.Forms.TextBox
    Friend WithEvents tbA_Top As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbB_Bottom As System.Windows.Forms.TextBox
    Friend WithEvents tbB_Top As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents btnB_Home As System.Windows.Forms.Button
    Private WithEvents btnB_Move As System.Windows.Forms.Button
    Friend WithEvents tbB_Position As System.Windows.Forms.TextBox
    Private WithEvents btnB_Up As System.Windows.Forms.Button
    Private WithEvents btnB_Down As System.Windows.Forms.Button
    Friend WithEvents rbtnB_1 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnB_1000 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnB_10 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnB_100 As System.Windows.Forms.RadioButton
    Private WithEvents btnB_Save As System.Windows.Forms.Button
    Friend WithEvents timer_GetPosition As System.Windows.Forms.Timer
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents btnA_SetSpeed As System.Windows.Forms.Button
    Friend WithEvents lblA_Status As System.Windows.Forms.Label
    Friend WithEvents lblB_Status As System.Windows.Forms.Label
    Private WithEvents btnB_SetSpeed As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents nudA_TestSpeed As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudB_TestSpeed As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudB_Speed As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudA_Speed As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents UcStatusBar1 As ProjectAOI.ucStatusBar
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

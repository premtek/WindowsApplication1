<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConveyor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConveyor))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageStation1 = New System.Windows.Forms.TabPage()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.nmcS1HeaterValue = New System.Windows.Forms.NumericUpDown()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnGetTemperature1 = New System.Windows.Forms.Button()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnSetTemperature1 = New System.Windows.Forms.Button()
        Me.btnRolling = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ckbStation1TopLiftUpDown = New System.Windows.Forms.CheckBox()
        Me.lblStation1TopLiftDownSensor = New System.Windows.Forms.Label()
        Me.lblStation1TopLiftUpSensor = New System.Windows.Forms.Label()
        Me.ckbStation1Stopper = New System.Windows.Forms.CheckBox()
        Me.lblStation1StopperDownSensor = New System.Windows.Forms.Label()
        Me.lblStation1StopperUpSensor = New System.Windows.Forms.Label()
        Me.lblStation1TrayReadySensor = New System.Windows.Forms.Label()
        Me.TabPageStation2 = New System.Windows.Forms.TabPage()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.nmcS2HeaterValue = New System.Windows.Forms.NumericUpDown()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnMoveFinish = New System.Windows.Forms.Button()
        Me.btnRolling2 = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnSetTemperature2 = New System.Windows.Forms.Button()
        Me.ckbHoldBack = New System.Windows.Forms.CheckBox()
        Me.lblTrayClamperOn = New System.Windows.Forms.Label()
        Me.lblTrayClamperOff = New System.Windows.Forms.Label()
        Me.ckbTrayClamper = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnDown2 = New System.Windows.Forms.Button()
        Me.btnMotorStop2 = New System.Windows.Forms.Button()
        Me.btnUp2 = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ckbStation2Stopper = New System.Windows.Forms.CheckBox()
        Me.lblStation2StopperDownSensor = New System.Windows.Forms.Label()
        Me.lblStation2StopperUpSensor = New System.Windows.Forms.Label()
        Me.lblStation2TrayReadySensor = New System.Windows.Forms.Label()
        Me.TabPageStation3 = New System.Windows.Forms.TabPage()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.nmcS3HeaterValue = New System.Windows.Forms.NumericUpDown()
        Me.btnRolling3 = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnSetTemperature3 = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ckbStation3TopLiftUpDown = New System.Windows.Forms.CheckBox()
        Me.lblStation3TopLiftDownSensor = New System.Windows.Forms.Label()
        Me.lblStation3TopLiftUpSensor = New System.Windows.Forms.Label()
        Me.ckbStation3Stopper = New System.Windows.Forms.CheckBox()
        Me.lblStation3StopperDownSensor = New System.Windows.Forms.Label()
        Me.lblStation3StopperUpSensor = New System.Windows.Forms.Label()
        Me.lblStation3TrayReadySensor = New System.Windows.Forms.Label()
        Me.TabPageLoadULoad = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.ckbUnLoadBoardAvaiable = New System.Windows.Forms.CheckBox()
        Me.lblUnLoadRecieveTray = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.ckbLoadRecieveTray = New System.Windows.Forms.CheckBox()
        Me.lblLoadBoardAvaiable = New System.Windows.Forms.Label()
        Me.TabPagePageBase = New System.Windows.Forms.TabPage()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.cmbRunMode = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtStation3heaterSet = New System.Windows.Forms.TextBox()
        Me.ckbNoUseStation3 = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtStation2heaterSet = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtStation2TopLiftDownPos = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cboStation2TopLiftSpeed = New System.Windows.Forms.ComboBox()
        Me.txtStation2TopLiftUpPos = New System.Windows.Forms.TextBox()
        Me.ckbNoUseStation2 = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtStation1heaterSet = New System.Windows.Forms.TextBox()
        Me.ckbNoUseStation1 = New System.Windows.Forms.CheckBox()
        Me.lblRollSpeed = New System.Windows.Forms.Label()
        Me.cboRollSpeed = New System.Windows.Forms.ComboBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnAutoRun = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnUnload = New System.Windows.Forms.Button()
        Me.btnOKExit = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPageStation1.SuspendLayout()
        CType(Me.nmcS1HeaterValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageStation2.SuspendLayout()
        CType(Me.nmcS2HeaterValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageStation3.SuspendLayout()
        CType(Me.nmcS3HeaterValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageLoadULoad.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPagePageBase.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageStation1)
        Me.TabControl1.Controls.Add(Me.TabPageStation2)
        Me.TabControl1.Controls.Add(Me.TabPageStation3)
        Me.TabControl1.Controls.Add(Me.TabPageLoadULoad)
        Me.TabControl1.Controls.Add(Me.TabPagePageBase)
        Me.TabControl1.Font = New System.Drawing.Font("微軟正黑體", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(12, 13)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1051, 597)
        Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControl1.TabIndex = 3
        '
        'TabPageStation1
        '
        Me.TabPageStation1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TabPageStation1.Controls.Add(Me.Panel6)
        Me.TabPageStation1.Controls.Add(Me.Panel5)
        Me.TabPageStation1.Controls.Add(Me.Panel4)
        Me.TabPageStation1.Controls.Add(Me.Panel3)
        Me.TabPageStation1.Controls.Add(Me.Panel2)
        Me.TabPageStation1.Controls.Add(Me.Panel1)
        Me.TabPageStation1.Controls.Add(Me.nmcS1HeaterValue)
        Me.TabPageStation1.Controls.Add(Me.Label15)
        Me.TabPageStation1.Controls.Add(Me.Label11)
        Me.TabPageStation1.Controls.Add(Me.btnGetTemperature1)
        Me.TabPageStation1.Controls.Add(Me.TextBox6)
        Me.TabPageStation1.Controls.Add(Me.Label8)
        Me.TabPageStation1.Controls.Add(Me.btnSetTemperature1)
        Me.TabPageStation1.Controls.Add(Me.btnRolling)
        Me.TabPageStation1.Controls.Add(Me.Label6)
        Me.TabPageStation1.Controls.Add(Me.ckbStation1TopLiftUpDown)
        Me.TabPageStation1.Controls.Add(Me.lblStation1TopLiftDownSensor)
        Me.TabPageStation1.Controls.Add(Me.lblStation1TopLiftUpSensor)
        Me.TabPageStation1.Controls.Add(Me.ckbStation1Stopper)
        Me.TabPageStation1.Controls.Add(Me.lblStation1StopperDownSensor)
        Me.TabPageStation1.Controls.Add(Me.lblStation1StopperUpSensor)
        Me.TabPageStation1.Controls.Add(Me.lblStation1TrayReadySensor)
        Me.TabPageStation1.Location = New System.Drawing.Point(4, 30)
        Me.TabPageStation1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabPageStation1.Name = "TabPageStation1"
        Me.TabPageStation1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabPageStation1.Size = New System.Drawing.Size(1043, 563)
        Me.TabPageStation1.TabIndex = 0
        Me.TabPageStation1.Text = "Station1"
        '
        'Panel6
        '
        Me.Panel6.BackgroundImage = CType(resources.GetObject("Panel6.BackgroundImage"), System.Drawing.Image)
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel6.Location = New System.Drawing.Point(7, 347)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(47, 53)
        Me.Panel6.TabIndex = 37
        '
        'Panel5
        '
        Me.Panel5.BackgroundImage = CType(resources.GetObject("Panel5.BackgroundImage"), System.Drawing.Image)
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel5.Location = New System.Drawing.Point(7, 280)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(47, 53)
        Me.Panel5.TabIndex = 37
        '
        'Panel4
        '
        Me.Panel4.BackgroundImage = CType(resources.GetObject("Panel4.BackgroundImage"), System.Drawing.Image)
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel4.Location = New System.Drawing.Point(7, 213)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(47, 53)
        Me.Panel4.TabIndex = 37
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel3.Location = New System.Drawing.Point(7, 147)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(47, 53)
        Me.Panel3.TabIndex = 37
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel2.Location = New System.Drawing.Point(7, 80)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(47, 53)
        Me.Panel2.TabIndex = 37
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel1.Location = New System.Drawing.Point(7, 13)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(47, 53)
        Me.Panel1.TabIndex = 37
        '
        'nmcS1HeaterValue
        '
        Me.nmcS1HeaterValue.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.nmcS1HeaterValue.Location = New System.Drawing.Point(315, 424)
        Me.nmcS1HeaterValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.nmcS1HeaterValue.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.nmcS1HeaterValue.Name = "nmcS1HeaterValue"
        Me.nmcS1HeaterValue.Size = New System.Drawing.Size(66, 33)
        Me.nmcS1HeaterValue.TabIndex = 36
        Me.nmcS1HeaterValue.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label15.Location = New System.Drawing.Point(503, 439)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(81, 24)
        Me.Label15.TabIndex = 35
        Me.Label15.Text = "Label15"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label11.Location = New System.Drawing.Point(58, 493)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(182, 24)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = "Temperature Read"
        '
        'btnGetTemperature1
        '
        Me.btnGetTemperature1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnGetTemperature1.Location = New System.Drawing.Point(388, 473)
        Me.btnGetTemperature1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnGetTemperature1.Name = "btnGetTemperature1"
        Me.btnGetTemperature1.Size = New System.Drawing.Size(58, 67)
        Me.btnGetTemperature1.TabIndex = 33
        Me.btnGetTemperature1.Text = "Get"
        Me.btnGetTemperature1.UseVisualStyleBackColor = True
        '
        'TextBox6
        '
        Me.TextBox6.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TextBox6.Location = New System.Drawing.Point(315, 490)
        Me.TextBox6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(66, 33)
        Me.TextBox6.TabIndex = 32
        Me.TextBox6.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label8.Location = New System.Drawing.Point(58, 427)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(162, 24)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "Temperature set"
        '
        'btnSetTemperature1
        '
        Me.btnSetTemperature1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSetTemperature1.Location = New System.Drawing.Point(388, 407)
        Me.btnSetTemperature1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnSetTemperature1.Name = "btnSetTemperature1"
        Me.btnSetTemperature1.Size = New System.Drawing.Size(58, 67)
        Me.btnSetTemperature1.TabIndex = 30
        Me.btnSetTemperature1.Text = "Set"
        Me.btnSetTemperature1.UseVisualStyleBackColor = True
        '
        'btnRolling
        '
        Me.btnRolling.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnRolling.Location = New System.Drawing.Point(315, 27)
        Me.btnRolling.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnRolling.Name = "btnRolling"
        Me.btnRolling.Size = New System.Drawing.Size(175, 67)
        Me.btnRolling.TabIndex = 10
        Me.btnRolling.Text = "Roll    transport"
        Me.btnRolling.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label6.Location = New System.Drawing.Point(58, 361)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(201, 24)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "ChuckVacuumReady"
        '
        'ckbStation1TopLiftUpDown
        '
        Me.ckbStation1TopLiftUpDown.Appearance = System.Windows.Forms.Appearance.Button
        Me.ckbStation1TopLiftUpDown.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ckbStation1TopLiftUpDown.Location = New System.Drawing.Point(315, 240)
        Me.ckbStation1TopLiftUpDown.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ckbStation1TopLiftUpDown.Name = "ckbStation1TopLiftUpDown"
        Me.ckbStation1TopLiftUpDown.Size = New System.Drawing.Size(175, 67)
        Me.ckbStation1TopLiftUpDown.TabIndex = 8
        Me.ckbStation1TopLiftUpDown.Text = "Top Lift Cylinder"
        Me.ckbStation1TopLiftUpDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ckbStation1TopLiftUpDown.UseVisualStyleBackColor = True
        '
        'lblStation1TopLiftDownSensor
        '
        Me.lblStation1TopLiftDownSensor.AutoSize = True
        Me.lblStation1TopLiftDownSensor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStation1TopLiftDownSensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStation1TopLiftDownSensor.Location = New System.Drawing.Point(58, 294)
        Me.lblStation1TopLiftDownSensor.Name = "lblStation1TopLiftDownSensor"
        Me.lblStation1TopLiftDownSensor.Size = New System.Drawing.Size(185, 24)
        Me.lblStation1TopLiftDownSensor.TabIndex = 7
        Me.lblStation1TopLiftDownSensor.Text = "TopLiftDownReady"
        '
        'lblStation1TopLiftUpSensor
        '
        Me.lblStation1TopLiftUpSensor.AutoSize = True
        Me.lblStation1TopLiftUpSensor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStation1TopLiftUpSensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStation1TopLiftUpSensor.Location = New System.Drawing.Point(58, 227)
        Me.lblStation1TopLiftUpSensor.Name = "lblStation1TopLiftUpSensor"
        Me.lblStation1TopLiftUpSensor.Size = New System.Drawing.Size(157, 24)
        Me.lblStation1TopLiftUpSensor.TabIndex = 6
        Me.lblStation1TopLiftUpSensor.Text = "TopLiftUpReady"
        '
        'ckbStation1Stopper
        '
        Me.ckbStation1Stopper.Appearance = System.Windows.Forms.Appearance.Button
        Me.ckbStation1Stopper.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ckbStation1Stopper.Location = New System.Drawing.Point(315, 107)
        Me.ckbStation1Stopper.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ckbStation1Stopper.Name = "ckbStation1Stopper"
        Me.ckbStation1Stopper.Size = New System.Drawing.Size(175, 67)
        Me.ckbStation1Stopper.TabIndex = 5
        Me.ckbStation1Stopper.Text = "Stopper Cylinder"
        Me.ckbStation1Stopper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ckbStation1Stopper.UseVisualStyleBackColor = True
        '
        'lblStation1StopperDownSensor
        '
        Me.lblStation1StopperDownSensor.AutoSize = True
        Me.lblStation1StopperDownSensor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStation1StopperDownSensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStation1StopperDownSensor.Location = New System.Drawing.Point(58, 161)
        Me.lblStation1StopperDownSensor.Name = "lblStation1StopperDownSensor"
        Me.lblStation1StopperDownSensor.Size = New System.Drawing.Size(194, 24)
        Me.lblStation1StopperDownSensor.TabIndex = 4
        Me.lblStation1StopperDownSensor.Text = "StopperDownReady"
        '
        'lblStation1StopperUpSensor
        '
        Me.lblStation1StopperUpSensor.AutoSize = True
        Me.lblStation1StopperUpSensor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStation1StopperUpSensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStation1StopperUpSensor.Location = New System.Drawing.Point(58, 94)
        Me.lblStation1StopperUpSensor.Name = "lblStation1StopperUpSensor"
        Me.lblStation1StopperUpSensor.Size = New System.Drawing.Size(166, 24)
        Me.lblStation1StopperUpSensor.TabIndex = 3
        Me.lblStation1StopperUpSensor.Text = "StopperUpReady"
        '
        'lblStation1TrayReadySensor
        '
        Me.lblStation1TrayReadySensor.AutoSize = True
        Me.lblStation1TrayReadySensor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStation1TrayReadySensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStation1TrayReadySensor.Location = New System.Drawing.Point(58, 27)
        Me.lblStation1TrayReadySensor.Name = "lblStation1TrayReadySensor"
        Me.lblStation1TrayReadySensor.Size = New System.Drawing.Size(107, 24)
        Me.lblStation1TrayReadySensor.TabIndex = 1
        Me.lblStation1TrayReadySensor.Text = "TrayReady"
        '
        'TabPageStation2
        '
        Me.TabPageStation2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TabPageStation2.Controls.Add(Me.Panel7)
        Me.TabPageStation2.Controls.Add(Me.Panel8)
        Me.TabPageStation2.Controls.Add(Me.Panel9)
        Me.TabPageStation2.Controls.Add(Me.Panel10)
        Me.TabPageStation2.Controls.Add(Me.Panel11)
        Me.TabPageStation2.Controls.Add(Me.Panel12)
        Me.TabPageStation2.Controls.Add(Me.nmcS2HeaterValue)
        Me.TabPageStation2.Controls.Add(Me.Label12)
        Me.TabPageStation2.Controls.Add(Me.btnMoveFinish)
        Me.TabPageStation2.Controls.Add(Me.btnRolling2)
        Me.TabPageStation2.Controls.Add(Me.Label9)
        Me.TabPageStation2.Controls.Add(Me.btnSetTemperature2)
        Me.TabPageStation2.Controls.Add(Me.ckbHoldBack)
        Me.TabPageStation2.Controls.Add(Me.lblTrayClamperOn)
        Me.TabPageStation2.Controls.Add(Me.lblTrayClamperOff)
        Me.TabPageStation2.Controls.Add(Me.ckbTrayClamper)
        Me.TabPageStation2.Controls.Add(Me.Label1)
        Me.TabPageStation2.Controls.Add(Me.Label2)
        Me.TabPageStation2.Controls.Add(Me.TextBox2)
        Me.TabPageStation2.Controls.Add(Me.Button7)
        Me.TabPageStation2.Controls.Add(Me.Button6)
        Me.TabPageStation2.Controls.Add(Me.Button5)
        Me.TabPageStation2.Controls.Add(Me.TextBox1)
        Me.TabPageStation2.Controls.Add(Me.btnDown2)
        Me.TabPageStation2.Controls.Add(Me.btnMotorStop2)
        Me.TabPageStation2.Controls.Add(Me.btnUp2)
        Me.TabPageStation2.Controls.Add(Me.Label7)
        Me.TabPageStation2.Controls.Add(Me.ckbStation2Stopper)
        Me.TabPageStation2.Controls.Add(Me.lblStation2StopperDownSensor)
        Me.TabPageStation2.Controls.Add(Me.lblStation2StopperUpSensor)
        Me.TabPageStation2.Controls.Add(Me.lblStation2TrayReadySensor)
        Me.TabPageStation2.Location = New System.Drawing.Point(4, 30)
        Me.TabPageStation2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabPageStation2.Name = "TabPageStation2"
        Me.TabPageStation2.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabPageStation2.Size = New System.Drawing.Size(1043, 563)
        Me.TabPageStation2.TabIndex = 1
        Me.TabPageStation2.Text = "Station2"
        '
        'Panel7
        '
        Me.Panel7.BackgroundImage = CType(resources.GetObject("Panel7.BackgroundImage"), System.Drawing.Image)
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel7.Location = New System.Drawing.Point(7, 347)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(47, 53)
        Me.Panel7.TabIndex = 40
        '
        'Panel8
        '
        Me.Panel8.BackgroundImage = CType(resources.GetObject("Panel8.BackgroundImage"), System.Drawing.Image)
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel8.Location = New System.Drawing.Point(7, 280)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(47, 53)
        Me.Panel8.TabIndex = 41
        '
        'Panel9
        '
        Me.Panel9.BackgroundImage = CType(resources.GetObject("Panel9.BackgroundImage"), System.Drawing.Image)
        Me.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel9.Location = New System.Drawing.Point(7, 213)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(47, 53)
        Me.Panel9.TabIndex = 42
        '
        'Panel10
        '
        Me.Panel10.BackgroundImage = CType(resources.GetObject("Panel10.BackgroundImage"), System.Drawing.Image)
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel10.Location = New System.Drawing.Point(7, 147)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(47, 53)
        Me.Panel10.TabIndex = 43
        '
        'Panel11
        '
        Me.Panel11.BackgroundImage = CType(resources.GetObject("Panel11.BackgroundImage"), System.Drawing.Image)
        Me.Panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel11.Location = New System.Drawing.Point(7, 80)
        Me.Panel11.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(47, 53)
        Me.Panel11.TabIndex = 44
        '
        'Panel12
        '
        Me.Panel12.BackgroundImage = CType(resources.GetObject("Panel12.BackgroundImage"), System.Drawing.Image)
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel12.Location = New System.Drawing.Point(7, 13)
        Me.Panel12.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(47, 53)
        Me.Panel12.TabIndex = 45
        '
        'nmcS2HeaterValue
        '
        Me.nmcS2HeaterValue.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.nmcS2HeaterValue.Location = New System.Drawing.Point(315, 424)
        Me.nmcS2HeaterValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.nmcS2HeaterValue.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.nmcS2HeaterValue.Name = "nmcS2HeaterValue"
        Me.nmcS2HeaterValue.Size = New System.Drawing.Size(66, 33)
        Me.nmcS2HeaterValue.TabIndex = 39
        Me.nmcS2HeaterValue.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label12.Location = New System.Drawing.Point(686, 499)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(81, 24)
        Me.Label12.TabIndex = 38
        Me.Label12.Text = "Label12"
        '
        'btnMoveFinish
        '
        Me.btnMoveFinish.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnMoveFinish.Location = New System.Drawing.Point(778, 476)
        Me.btnMoveFinish.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnMoveFinish.Name = "btnMoveFinish"
        Me.btnMoveFinish.Size = New System.Drawing.Size(126, 70)
        Me.btnMoveFinish.TabIndex = 37
        Me.btnMoveFinish.Text = "Move Finish"
        Me.btnMoveFinish.UseVisualStyleBackColor = True
        '
        'btnRolling2
        '
        Me.btnRolling2.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnRolling2.Location = New System.Drawing.Point(315, 27)
        Me.btnRolling2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnRolling2.Name = "btnRolling2"
        Me.btnRolling2.Size = New System.Drawing.Size(175, 67)
        Me.btnRolling2.TabIndex = 36
        Me.btnRolling2.Text = "Roll    transport"
        Me.btnRolling2.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label9.Location = New System.Drawing.Point(58, 427)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(162, 24)
        Me.Label9.TabIndex = 35
        Me.Label9.Text = "Temperature set"
        '
        'btnSetTemperature2
        '
        Me.btnSetTemperature2.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSetTemperature2.Location = New System.Drawing.Point(388, 407)
        Me.btnSetTemperature2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnSetTemperature2.Name = "btnSetTemperature2"
        Me.btnSetTemperature2.Size = New System.Drawing.Size(58, 67)
        Me.btnSetTemperature2.TabIndex = 34
        Me.btnSetTemperature2.Text = "Set"
        Me.btnSetTemperature2.UseVisualStyleBackColor = True
        '
        'ckbHoldBack
        '
        Me.ckbHoldBack.Appearance = System.Windows.Forms.Appearance.Button
        Me.ckbHoldBack.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ckbHoldBack.Location = New System.Drawing.Point(315, 173)
        Me.ckbHoldBack.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ckbHoldBack.Name = "ckbHoldBack"
        Me.ckbHoldBack.Size = New System.Drawing.Size(175, 67)
        Me.ckbHoldBack.TabIndex = 32
        Me.ckbHoldBack.Text = "Hold Back"
        Me.ckbHoldBack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ckbHoldBack.UseVisualStyleBackColor = True
        '
        'lblTrayClamperOn
        '
        Me.lblTrayClamperOn.AutoSize = True
        Me.lblTrayClamperOn.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblTrayClamperOn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblTrayClamperOn.Location = New System.Drawing.Point(58, 294)
        Me.lblTrayClamperOn.Name = "lblTrayClamperOn"
        Me.lblTrayClamperOn.Size = New System.Drawing.Size(213, 24)
        Me.lblTrayClamperOn.TabIndex = 31
        Me.lblTrayClamperOn.Text = "TrayClamperOnReady"
        '
        'lblTrayClamperOff
        '
        Me.lblTrayClamperOff.AutoSize = True
        Me.lblTrayClamperOff.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblTrayClamperOff.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblTrayClamperOff.Location = New System.Drawing.Point(58, 227)
        Me.lblTrayClamperOff.Name = "lblTrayClamperOff"
        Me.lblTrayClamperOff.Size = New System.Drawing.Size(215, 24)
        Me.lblTrayClamperOff.TabIndex = 30
        Me.lblTrayClamperOff.Text = "TrayClamperOffReady"
        '
        'ckbTrayClamper
        '
        Me.ckbTrayClamper.Appearance = System.Windows.Forms.Appearance.Button
        Me.ckbTrayClamper.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ckbTrayClamper.Location = New System.Drawing.Point(315, 240)
        Me.ckbTrayClamper.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ckbTrayClamper.Name = "ckbTrayClamper"
        Me.ckbTrayClamper.Size = New System.Drawing.Size(175, 67)
        Me.ckbTrayClamper.TabIndex = 29
        Me.ckbTrayClamper.Text = "Tray Clamper"
        Me.ckbTrayClamper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ckbTrayClamper.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(686, 339)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 24)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "通訊機號"
        Me.Label1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(703, 273)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 24)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Label2"
        Me.Label2.Visible = False
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(778, 389)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(81, 33)
        Me.TextBox2.TabIndex = 26
        Me.TextBox2.Text = "1"
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Button7.Location = New System.Drawing.Point(882, 391)
        Me.Button7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(101, 37)
        Me.Button7.TabIndex = 25
        Me.Button7.Text = "Go"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Button6.Location = New System.Drawing.Point(882, 332)
        Me.Button6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(101, 37)
        Me.Button6.TabIndex = 24
        Me.Button6.Text = "Home"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Button5.Location = New System.Drawing.Point(778, 268)
        Me.Button5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(101, 32)
        Me.Button5.TabIndex = 23
        Me.Button5.Text = "Get  Pos"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(778, 331)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(81, 33)
        Me.TextBox1.TabIndex = 21
        Me.TextBox1.Text = "6"
        Me.TextBox1.Visible = False
        '
        'btnDown2
        '
        Me.btnDown2.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnDown2.Location = New System.Drawing.Point(538, 273)
        Me.btnDown2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDown2.Name = "btnDown2"
        Me.btnDown2.Size = New System.Drawing.Size(126, 47)
        Me.btnDown2.TabIndex = 20
        Me.btnDown2.Text = "Down"
        Me.btnDown2.UseVisualStyleBackColor = True
        '
        'btnMotorStop2
        '
        Me.btnMotorStop2.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnMotorStop2.Location = New System.Drawing.Point(538, 339)
        Me.btnMotorStop2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnMotorStop2.Name = "btnMotorStop2"
        Me.btnMotorStop2.Size = New System.Drawing.Size(126, 49)
        Me.btnMotorStop2.TabIndex = 19
        Me.btnMotorStop2.Text = "Motor Stop"
        Me.btnMotorStop2.UseVisualStyleBackColor = True
        '
        'btnUp2
        '
        Me.btnUp2.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnUp2.Location = New System.Drawing.Point(538, 208)
        Me.btnUp2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnUp2.Name = "btnUp2"
        Me.btnUp2.Size = New System.Drawing.Size(126, 45)
        Me.btnUp2.TabIndex = 18
        Me.btnUp2.Text = "Up"
        Me.btnUp2.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label7.Location = New System.Drawing.Point(58, 361)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(201, 24)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "ChuckVacuumReady"
        '
        'ckbStation2Stopper
        '
        Me.ckbStation2Stopper.Appearance = System.Windows.Forms.Appearance.Button
        Me.ckbStation2Stopper.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ckbStation2Stopper.Location = New System.Drawing.Point(315, 107)
        Me.ckbStation2Stopper.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ckbStation2Stopper.Name = "ckbStation2Stopper"
        Me.ckbStation2Stopper.Size = New System.Drawing.Size(175, 67)
        Me.ckbStation2Stopper.TabIndex = 13
        Me.ckbStation2Stopper.Text = "Stopper Cylinder"
        Me.ckbStation2Stopper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ckbStation2Stopper.UseVisualStyleBackColor = True
        '
        'lblStation2StopperDownSensor
        '
        Me.lblStation2StopperDownSensor.AutoSize = True
        Me.lblStation2StopperDownSensor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStation2StopperDownSensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStation2StopperDownSensor.Location = New System.Drawing.Point(58, 161)
        Me.lblStation2StopperDownSensor.Name = "lblStation2StopperDownSensor"
        Me.lblStation2StopperDownSensor.Size = New System.Drawing.Size(194, 24)
        Me.lblStation2StopperDownSensor.TabIndex = 12
        Me.lblStation2StopperDownSensor.Text = "StopperDownReady"
        '
        'lblStation2StopperUpSensor
        '
        Me.lblStation2StopperUpSensor.AutoSize = True
        Me.lblStation2StopperUpSensor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStation2StopperUpSensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStation2StopperUpSensor.Location = New System.Drawing.Point(58, 94)
        Me.lblStation2StopperUpSensor.Name = "lblStation2StopperUpSensor"
        Me.lblStation2StopperUpSensor.Size = New System.Drawing.Size(166, 24)
        Me.lblStation2StopperUpSensor.TabIndex = 11
        Me.lblStation2StopperUpSensor.Text = "StopperUpReady"
        '
        'lblStation2TrayReadySensor
        '
        Me.lblStation2TrayReadySensor.AutoSize = True
        Me.lblStation2TrayReadySensor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStation2TrayReadySensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStation2TrayReadySensor.Location = New System.Drawing.Point(58, 27)
        Me.lblStation2TrayReadySensor.Name = "lblStation2TrayReadySensor"
        Me.lblStation2TrayReadySensor.Size = New System.Drawing.Size(107, 24)
        Me.lblStation2TrayReadySensor.TabIndex = 10
        Me.lblStation2TrayReadySensor.Text = "TrayReady"
        '
        'TabPageStation3
        '
        Me.TabPageStation3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TabPageStation3.Controls.Add(Me.Panel13)
        Me.TabPageStation3.Controls.Add(Me.Panel14)
        Me.TabPageStation3.Controls.Add(Me.Panel15)
        Me.TabPageStation3.Controls.Add(Me.Panel16)
        Me.TabPageStation3.Controls.Add(Me.Panel17)
        Me.TabPageStation3.Controls.Add(Me.Panel18)
        Me.TabPageStation3.Controls.Add(Me.nmcS3HeaterValue)
        Me.TabPageStation3.Controls.Add(Me.btnRolling3)
        Me.TabPageStation3.Controls.Add(Me.Label10)
        Me.TabPageStation3.Controls.Add(Me.btnSetTemperature3)
        Me.TabPageStation3.Controls.Add(Me.Label13)
        Me.TabPageStation3.Controls.Add(Me.ckbStation3TopLiftUpDown)
        Me.TabPageStation3.Controls.Add(Me.lblStation3TopLiftDownSensor)
        Me.TabPageStation3.Controls.Add(Me.lblStation3TopLiftUpSensor)
        Me.TabPageStation3.Controls.Add(Me.ckbStation3Stopper)
        Me.TabPageStation3.Controls.Add(Me.lblStation3StopperDownSensor)
        Me.TabPageStation3.Controls.Add(Me.lblStation3StopperUpSensor)
        Me.TabPageStation3.Controls.Add(Me.lblStation3TrayReadySensor)
        Me.TabPageStation3.Location = New System.Drawing.Point(4, 30)
        Me.TabPageStation3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabPageStation3.Name = "TabPageStation3"
        Me.TabPageStation3.Size = New System.Drawing.Size(1043, 563)
        Me.TabPageStation3.TabIndex = 2
        Me.TabPageStation3.Text = "Station3"
        '
        'Panel13
        '
        Me.Panel13.BackgroundImage = CType(resources.GetObject("Panel13.BackgroundImage"), System.Drawing.Image)
        Me.Panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel13.Location = New System.Drawing.Point(7, 347)
        Me.Panel13.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(47, 53)
        Me.Panel13.TabIndex = 46
        '
        'Panel14
        '
        Me.Panel14.BackgroundImage = CType(resources.GetObject("Panel14.BackgroundImage"), System.Drawing.Image)
        Me.Panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel14.Location = New System.Drawing.Point(7, 280)
        Me.Panel14.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(47, 53)
        Me.Panel14.TabIndex = 47
        '
        'Panel15
        '
        Me.Panel15.BackgroundImage = CType(resources.GetObject("Panel15.BackgroundImage"), System.Drawing.Image)
        Me.Panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel15.Location = New System.Drawing.Point(7, 213)
        Me.Panel15.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(47, 53)
        Me.Panel15.TabIndex = 48
        '
        'Panel16
        '
        Me.Panel16.BackgroundImage = CType(resources.GetObject("Panel16.BackgroundImage"), System.Drawing.Image)
        Me.Panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel16.Location = New System.Drawing.Point(7, 147)
        Me.Panel16.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(47, 53)
        Me.Panel16.TabIndex = 49
        '
        'Panel17
        '
        Me.Panel17.BackgroundImage = CType(resources.GetObject("Panel17.BackgroundImage"), System.Drawing.Image)
        Me.Panel17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel17.Location = New System.Drawing.Point(7, 80)
        Me.Panel17.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(47, 53)
        Me.Panel17.TabIndex = 50
        '
        'Panel18
        '
        Me.Panel18.BackgroundImage = CType(resources.GetObject("Panel18.BackgroundImage"), System.Drawing.Image)
        Me.Panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel18.Location = New System.Drawing.Point(7, 13)
        Me.Panel18.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(47, 53)
        Me.Panel18.TabIndex = 51
        '
        'nmcS3HeaterValue
        '
        Me.nmcS3HeaterValue.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.nmcS3HeaterValue.Location = New System.Drawing.Point(315, 424)
        Me.nmcS3HeaterValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.nmcS3HeaterValue.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.nmcS3HeaterValue.Name = "nmcS3HeaterValue"
        Me.nmcS3HeaterValue.Size = New System.Drawing.Size(66, 33)
        Me.nmcS3HeaterValue.TabIndex = 40
        Me.nmcS3HeaterValue.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'btnRolling3
        '
        Me.btnRolling3.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnRolling3.Location = New System.Drawing.Point(315, 27)
        Me.btnRolling3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnRolling3.Name = "btnRolling3"
        Me.btnRolling3.Size = New System.Drawing.Size(175, 67)
        Me.btnRolling3.TabIndex = 35
        Me.btnRolling3.Text = "Roll    transport"
        Me.btnRolling3.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label10.Location = New System.Drawing.Point(58, 427)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(162, 24)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "Temperature set"
        '
        'btnSetTemperature3
        '
        Me.btnSetTemperature3.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSetTemperature3.Location = New System.Drawing.Point(388, 407)
        Me.btnSetTemperature3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnSetTemperature3.Name = "btnSetTemperature3"
        Me.btnSetTemperature3.Size = New System.Drawing.Size(58, 67)
        Me.btnSetTemperature3.TabIndex = 33
        Me.btnSetTemperature3.Text = "Set"
        Me.btnSetTemperature3.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label13.Location = New System.Drawing.Point(58, 361)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(201, 24)
        Me.Label13.TabIndex = 17
        Me.Label13.Text = "ChuckVacuumReady"
        '
        'ckbStation3TopLiftUpDown
        '
        Me.ckbStation3TopLiftUpDown.Appearance = System.Windows.Forms.Appearance.Button
        Me.ckbStation3TopLiftUpDown.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ckbStation3TopLiftUpDown.Location = New System.Drawing.Point(315, 240)
        Me.ckbStation3TopLiftUpDown.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ckbStation3TopLiftUpDown.Name = "ckbStation3TopLiftUpDown"
        Me.ckbStation3TopLiftUpDown.Size = New System.Drawing.Size(175, 67)
        Me.ckbStation3TopLiftUpDown.TabIndex = 16
        Me.ckbStation3TopLiftUpDown.Text = "Top Lift Cylinder"
        Me.ckbStation3TopLiftUpDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ckbStation3TopLiftUpDown.UseVisualStyleBackColor = True
        '
        'lblStation3TopLiftDownSensor
        '
        Me.lblStation3TopLiftDownSensor.AutoSize = True
        Me.lblStation3TopLiftDownSensor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStation3TopLiftDownSensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStation3TopLiftDownSensor.Location = New System.Drawing.Point(58, 294)
        Me.lblStation3TopLiftDownSensor.Name = "lblStation3TopLiftDownSensor"
        Me.lblStation3TopLiftDownSensor.Size = New System.Drawing.Size(185, 24)
        Me.lblStation3TopLiftDownSensor.TabIndex = 15
        Me.lblStation3TopLiftDownSensor.Text = "TopLiftDownReady"
        '
        'lblStation3TopLiftUpSensor
        '
        Me.lblStation3TopLiftUpSensor.AutoSize = True
        Me.lblStation3TopLiftUpSensor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStation3TopLiftUpSensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStation3TopLiftUpSensor.Location = New System.Drawing.Point(58, 227)
        Me.lblStation3TopLiftUpSensor.Name = "lblStation3TopLiftUpSensor"
        Me.lblStation3TopLiftUpSensor.Size = New System.Drawing.Size(157, 24)
        Me.lblStation3TopLiftUpSensor.TabIndex = 14
        Me.lblStation3TopLiftUpSensor.Text = "TopLiftUpReady"
        '
        'ckbStation3Stopper
        '
        Me.ckbStation3Stopper.Appearance = System.Windows.Forms.Appearance.Button
        Me.ckbStation3Stopper.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ckbStation3Stopper.Location = New System.Drawing.Point(315, 107)
        Me.ckbStation3Stopper.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ckbStation3Stopper.Name = "ckbStation3Stopper"
        Me.ckbStation3Stopper.Size = New System.Drawing.Size(175, 67)
        Me.ckbStation3Stopper.TabIndex = 13
        Me.ckbStation3Stopper.Text = "Stopper Cylinder"
        Me.ckbStation3Stopper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ckbStation3Stopper.UseVisualStyleBackColor = True
        '
        'lblStation3StopperDownSensor
        '
        Me.lblStation3StopperDownSensor.AutoSize = True
        Me.lblStation3StopperDownSensor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStation3StopperDownSensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStation3StopperDownSensor.Location = New System.Drawing.Point(58, 161)
        Me.lblStation3StopperDownSensor.Name = "lblStation3StopperDownSensor"
        Me.lblStation3StopperDownSensor.Size = New System.Drawing.Size(194, 24)
        Me.lblStation3StopperDownSensor.TabIndex = 12
        Me.lblStation3StopperDownSensor.Text = "StopperDownReady"
        '
        'lblStation3StopperUpSensor
        '
        Me.lblStation3StopperUpSensor.AutoSize = True
        Me.lblStation3StopperUpSensor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStation3StopperUpSensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStation3StopperUpSensor.Location = New System.Drawing.Point(58, 94)
        Me.lblStation3StopperUpSensor.Name = "lblStation3StopperUpSensor"
        Me.lblStation3StopperUpSensor.Size = New System.Drawing.Size(166, 24)
        Me.lblStation3StopperUpSensor.TabIndex = 11
        Me.lblStation3StopperUpSensor.Text = "StopperUpReady"
        '
        'lblStation3TrayReadySensor
        '
        Me.lblStation3TrayReadySensor.AutoSize = True
        Me.lblStation3TrayReadySensor.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStation3TrayReadySensor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStation3TrayReadySensor.Location = New System.Drawing.Point(58, 27)
        Me.lblStation3TrayReadySensor.Name = "lblStation3TrayReadySensor"
        Me.lblStation3TrayReadySensor.Size = New System.Drawing.Size(107, 24)
        Me.lblStation3TrayReadySensor.TabIndex = 10
        Me.lblStation3TrayReadySensor.Text = "TrayReady"
        '
        'TabPageLoadULoad
        '
        Me.TabPageLoadULoad.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TabPageLoadULoad.Controls.Add(Me.GroupBox2)
        Me.TabPageLoadULoad.Controls.Add(Me.GroupBox1)
        Me.TabPageLoadULoad.Location = New System.Drawing.Point(4, 30)
        Me.TabPageLoadULoad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabPageLoadULoad.Name = "TabPageLoadULoad"
        Me.TabPageLoadULoad.Size = New System.Drawing.Size(1043, 563)
        Me.TabPageLoadULoad.TabIndex = 5
        Me.TabPageLoadULoad.Text = "Load/ULoad"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Panel20)
        Me.GroupBox2.Controls.Add(Me.ckbUnLoadBoardAvaiable)
        Me.GroupBox2.Controls.Add(Me.lblUnLoadRecieveTray)
        Me.GroupBox2.Font = New System.Drawing.Font("微軟正黑體", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox2.Location = New System.Drawing.Point(56, 256)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(455, 120)
        Me.GroupBox2.TabIndex = 17
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "UnLoad"
        '
        'Panel20
        '
        Me.Panel20.BackgroundImage = CType(resources.GetObject("Panel20.BackgroundImage"), System.Drawing.Image)
        Me.Panel20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel20.Location = New System.Drawing.Point(7, 40)
        Me.Panel20.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(47, 53)
        Me.Panel20.TabIndex = 53
        '
        'ckbUnLoadBoardAvaiable
        '
        Me.ckbUnLoadBoardAvaiable.Appearance = System.Windows.Forms.Appearance.Button
        Me.ckbUnLoadBoardAvaiable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ckbUnLoadBoardAvaiable.FlatAppearance.BorderSize = 0
        Me.ckbUnLoadBoardAvaiable.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ckbUnLoadBoardAvaiable.Location = New System.Drawing.Point(272, 33)
        Me.ckbUnLoadBoardAvaiable.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ckbUnLoadBoardAvaiable.Name = "ckbUnLoadBoardAvaiable"
        Me.ckbUnLoadBoardAvaiable.Size = New System.Drawing.Size(175, 67)
        Me.ckbUnLoadBoardAvaiable.TabIndex = 16
        Me.ckbUnLoadBoardAvaiable.Text = "BoardAvaiable"
        Me.ckbUnLoadBoardAvaiable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ckbUnLoadBoardAvaiable.UseVisualStyleBackColor = True
        '
        'lblUnLoadRecieveTray
        '
        Me.lblUnLoadRecieveTray.AutoSize = True
        Me.lblUnLoadRecieveTray.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblUnLoadRecieveTray.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUnLoadRecieveTray.Location = New System.Drawing.Point(61, 54)
        Me.lblUnLoadRecieveTray.Name = "lblUnLoadRecieveTray"
        Me.lblUnLoadRecieveTray.Size = New System.Drawing.Size(138, 24)
        Me.lblUnLoadRecieveTray.TabIndex = 15
        Me.lblUnLoadRecieveTray.Text = "RecieveReady"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel19)
        Me.GroupBox1.Controls.Add(Me.ckbLoadRecieveTray)
        Me.GroupBox1.Controls.Add(Me.lblLoadBoardAvaiable)
        Me.GroupBox1.Font = New System.Drawing.Font("微軟正黑體", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox1.Location = New System.Drawing.Point(56, 39)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(455, 120)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Load"
        '
        'Panel19
        '
        Me.Panel19.BackgroundImage = CType(resources.GetObject("Panel19.BackgroundImage"), System.Drawing.Image)
        Me.Panel19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel19.Location = New System.Drawing.Point(7, 40)
        Me.Panel19.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(47, 53)
        Me.Panel19.TabIndex = 52
        '
        'ckbLoadRecieveTray
        '
        Me.ckbLoadRecieveTray.Appearance = System.Windows.Forms.Appearance.Button
        Me.ckbLoadRecieveTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ckbLoadRecieveTray.FlatAppearance.BorderSize = 0
        Me.ckbLoadRecieveTray.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ckbLoadRecieveTray.Location = New System.Drawing.Point(272, 33)
        Me.ckbLoadRecieveTray.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ckbLoadRecieveTray.Name = "ckbLoadRecieveTray"
        Me.ckbLoadRecieveTray.Size = New System.Drawing.Size(175, 67)
        Me.ckbLoadRecieveTray.TabIndex = 16
        Me.ckbLoadRecieveTray.Text = "RecieveReady"
        Me.ckbLoadRecieveTray.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ckbLoadRecieveTray.UseVisualStyleBackColor = True
        '
        'lblLoadBoardAvaiable
        '
        Me.lblLoadBoardAvaiable.AutoSize = True
        Me.lblLoadBoardAvaiable.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblLoadBoardAvaiable.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblLoadBoardAvaiable.Location = New System.Drawing.Point(61, 54)
        Me.lblLoadBoardAvaiable.Name = "lblLoadBoardAvaiable"
        Me.lblLoadBoardAvaiable.Size = New System.Drawing.Size(144, 24)
        Me.lblLoadBoardAvaiable.TabIndex = 15
        Me.lblLoadBoardAvaiable.Text = "BoardAvaiable"
        '
        'TabPagePageBase
        '
        Me.TabPagePageBase.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TabPagePageBase.Controls.Add(Me.btnSave)
        Me.TabPagePageBase.Controls.Add(Me.cmbRunMode)
        Me.TabPagePageBase.Controls.Add(Me.Label14)
        Me.TabPagePageBase.Controls.Add(Me.GroupBox5)
        Me.TabPagePageBase.Controls.Add(Me.GroupBox4)
        Me.TabPagePageBase.Controls.Add(Me.GroupBox3)
        Me.TabPagePageBase.Controls.Add(Me.lblRollSpeed)
        Me.TabPagePageBase.Controls.Add(Me.cboRollSpeed)
        Me.TabPagePageBase.Location = New System.Drawing.Point(4, 30)
        Me.TabPagePageBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabPagePageBase.Name = "TabPagePageBase"
        Me.TabPagePageBase.Size = New System.Drawing.Size(1043, 563)
        Me.TabPagePageBase.TabIndex = 3
        Me.TabPagePageBase.Text = "Set"
        '
        'btnSave
        '
        Me.btnSave.BackgroundImage = CType(resources.GetObject("btnSave.BackgroundImage"), System.Drawing.Image)
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSave.Location = New System.Drawing.Point(705, 432)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(175, 81)
        Me.btnSave.TabIndex = 38
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'cmbRunMode
        '
        Me.cmbRunMode.BackColor = System.Drawing.Color.Aquamarine
        Me.cmbRunMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRunMode.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.cmbRunMode.FormattingEnabled = True
        Me.cmbRunMode.Items.AddRange(New Object() {"Manual", "Auto"})
        Me.cmbRunMode.Location = New System.Drawing.Point(173, 444)
        Me.cmbRunMode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbRunMode.Name = "cmbRunMode"
        Me.cmbRunMode.Size = New System.Drawing.Size(122, 28)
        Me.cmbRunMode.TabIndex = 37
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label14.Location = New System.Drawing.Point(24, 448)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(106, 24)
        Me.Label14.TabIndex = 36
        Me.Label14.Text = "Run Mode"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label3)
        Me.GroupBox5.Controls.Add(Me.txtStation3heaterSet)
        Me.GroupBox5.Controls.Add(Me.ckbNoUseStation3)
        Me.GroupBox5.Font = New System.Drawing.Font("微軟正黑體", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox5.Location = New System.Drawing.Point(350, 21)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox5.Size = New System.Drawing.Size(311, 135)
        Me.GroupBox5.TabIndex = 35
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Station3"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(157, 24)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Temperature(C)"
        '
        'txtStation3heaterSet
        '
        Me.txtStation3heaterSet.AutoCompleteCustomSource.AddRange(New String() {"Slow", "Middint", "Heigh"})
        Me.txtStation3heaterSet.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtStation3heaterSet.Location = New System.Drawing.Point(198, 80)
        Me.txtStation3heaterSet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtStation3heaterSet.Name = "txtStation3heaterSet"
        Me.txtStation3heaterSet.Size = New System.Drawing.Size(104, 33)
        Me.txtStation3heaterSet.TabIndex = 30
        '
        'ckbNoUseStation3
        '
        Me.ckbNoUseStation3.AutoSize = True
        Me.ckbNoUseStation3.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ckbNoUseStation3.Location = New System.Drawing.Point(121, 33)
        Me.ckbNoUseStation3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ckbNoUseStation3.Name = "ckbNoUseStation3"
        Me.ckbNoUseStation3.Size = New System.Drawing.Size(173, 28)
        Me.ckbNoUseStation3.TabIndex = 29
        Me.ckbNoUseStation3.Text = "ByPass Station3"
        Me.ckbNoUseStation3.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.txtStation2heaterSet)
        Me.GroupBox4.Controls.Add(Me.Label25)
        Me.GroupBox4.Controls.Add(Me.txtStation2TopLiftDownPos)
        Me.GroupBox4.Controls.Add(Me.Label24)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.cboStation2TopLiftSpeed)
        Me.GroupBox4.Controls.Add(Me.txtStation2TopLiftUpPos)
        Me.GroupBox4.Controls.Add(Me.ckbNoUseStation2)
        Me.GroupBox4.Font = New System.Drawing.Font("微軟正黑體", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox4.Location = New System.Drawing.Point(705, 21)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox4.Size = New System.Drawing.Size(311, 292)
        Me.GroupBox4.TabIndex = 34
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Station2"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 227)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(157, 24)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Temperature(C)"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtStation2heaterSet
        '
        Me.txtStation2heaterSet.AutoCompleteCustomSource.AddRange(New String() {"Slow", "Middint", "Heigh"})
        Me.txtStation2heaterSet.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtStation2heaterSet.Location = New System.Drawing.Point(190, 223)
        Me.txtStation2heaterSet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtStation2heaterSet.Name = "txtStation2heaterSet"
        Me.txtStation2heaterSet.Size = New System.Drawing.Size(104, 33)
        Me.txtStation2heaterSet.TabIndex = 38
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label25.Location = New System.Drawing.Point(14, 185)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(176, 24)
        Me.Label25.TabIndex = 37
        Me.Label25.Text = "Top Lift Down Pos"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtStation2TopLiftDownPos
        '
        Me.txtStation2TopLiftDownPos.AutoCompleteCustomSource.AddRange(New String() {"Slow", "Middint", "Heigh"})
        Me.txtStation2TopLiftDownPos.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtStation2TopLiftDownPos.Location = New System.Drawing.Point(190, 181)
        Me.txtStation2TopLiftDownPos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtStation2TopLiftDownPos.Name = "txtStation2TopLiftDownPos"
        Me.txtStation2TopLiftDownPos.Size = New System.Drawing.Size(104, 33)
        Me.txtStation2TopLiftDownPos.TabIndex = 36
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label24.Location = New System.Drawing.Point(14, 144)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(148, 24)
        Me.Label24.TabIndex = 35
        Me.Label24.Text = "Top Lift Up Pos"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label19.Location = New System.Drawing.Point(14, 100)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(141, 24)
        Me.Label19.TabIndex = 34
        Me.Label19.Text = "Top Lift Speed"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboStation2TopLiftSpeed
        '
        Me.cboStation2TopLiftSpeed.BackColor = System.Drawing.Color.Aquamarine
        Me.cboStation2TopLiftSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStation2TopLiftSpeed.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.cboStation2TopLiftSpeed.FormattingEnabled = True
        Me.cboStation2TopLiftSpeed.Items.AddRange(New Object() {"Slow", "Medium", "Fast"})
        Me.cboStation2TopLiftSpeed.Location = New System.Drawing.Point(190, 96)
        Me.cboStation2TopLiftSpeed.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboStation2TopLiftSpeed.Name = "cboStation2TopLiftSpeed"
        Me.cboStation2TopLiftSpeed.Size = New System.Drawing.Size(104, 32)
        Me.cboStation2TopLiftSpeed.TabIndex = 33
        '
        'txtStation2TopLiftUpPos
        '
        Me.txtStation2TopLiftUpPos.AutoCompleteCustomSource.AddRange(New String() {"Slow", "Middint", "Heigh"})
        Me.txtStation2TopLiftUpPos.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtStation2TopLiftUpPos.Location = New System.Drawing.Point(190, 140)
        Me.txtStation2TopLiftUpPos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtStation2TopLiftUpPos.Name = "txtStation2TopLiftUpPos"
        Me.txtStation2TopLiftUpPos.Size = New System.Drawing.Size(104, 33)
        Me.txtStation2TopLiftUpPos.TabIndex = 32
        '
        'ckbNoUseStation2
        '
        Me.ckbNoUseStation2.AutoSize = True
        Me.ckbNoUseStation2.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ckbNoUseStation2.Location = New System.Drawing.Point(113, 37)
        Me.ckbNoUseStation2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ckbNoUseStation2.Name = "ckbNoUseStation2"
        Me.ckbNoUseStation2.Size = New System.Drawing.Size(173, 28)
        Me.ckbNoUseStation2.TabIndex = 31
        Me.ckbNoUseStation2.Text = "ByPass Station2"
        Me.ckbNoUseStation2.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.txtStation1heaterSet)
        Me.GroupBox3.Controls.Add(Me.ckbNoUseStation1)
        Me.GroupBox3.Font = New System.Drawing.Font("微軟正黑體", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox3.Location = New System.Drawing.Point(14, 17)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(311, 139)
        Me.GroupBox3.TabIndex = 33
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Station1"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(157, 24)
        Me.Label5.TabIndex = 35
        Me.Label5.Text = "Temperature(C)"
        '
        'txtStation1heaterSet
        '
        Me.txtStation1heaterSet.AutoCompleteCustomSource.AddRange(New String() {"Slow", "Middint", "Heigh"})
        Me.txtStation1heaterSet.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtStation1heaterSet.Location = New System.Drawing.Point(213, 84)
        Me.txtStation1heaterSet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtStation1heaterSet.Name = "txtStation1heaterSet"
        Me.txtStation1heaterSet.Size = New System.Drawing.Size(90, 33)
        Me.txtStation1heaterSet.TabIndex = 34
        '
        'ckbNoUseStation1
        '
        Me.ckbNoUseStation1.AutoSize = True
        Me.ckbNoUseStation1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ckbNoUseStation1.Location = New System.Drawing.Point(122, 35)
        Me.ckbNoUseStation1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ckbNoUseStation1.Name = "ckbNoUseStation1"
        Me.ckbNoUseStation1.Size = New System.Drawing.Size(173, 28)
        Me.ckbNoUseStation1.TabIndex = 33
        Me.ckbNoUseStation1.Text = "ByPass Station1"
        Me.ckbNoUseStation1.UseVisualStyleBackColor = True
        '
        'lblRollSpeed
        '
        Me.lblRollSpeed.AutoSize = True
        Me.lblRollSpeed.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblRollSpeed.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblRollSpeed.Location = New System.Drawing.Point(24, 401)
        Me.lblRollSpeed.Name = "lblRollSpeed"
        Me.lblRollSpeed.Size = New System.Drawing.Size(136, 24)
        Me.lblRollSpeed.TabIndex = 21
        Me.lblRollSpeed.Text = "Rolling Speed"
        '
        'cboRollSpeed
        '
        Me.cboRollSpeed.BackColor = System.Drawing.Color.Aquamarine
        Me.cboRollSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRollSpeed.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.cboRollSpeed.FormattingEnabled = True
        Me.cboRollSpeed.Items.AddRange(New Object() {"Slow", "Medium", "Fast"})
        Me.cboRollSpeed.Location = New System.Drawing.Point(173, 391)
        Me.cboRollSpeed.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboRollSpeed.Name = "cboRollSpeed"
        Me.cboRollSpeed.Size = New System.Drawing.Size(122, 28)
        Me.cboRollSpeed.TabIndex = 20
        '
        'Timer1
        '
        '
        'btnAutoRun
        '
        Me.btnAutoRun.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAutoRun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAutoRun.FlatAppearance.BorderSize = 0
        Me.btnAutoRun.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnAutoRun.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnAutoRun.Location = New System.Drawing.Point(1082, 124)
        Me.btnAutoRun.Margin = New System.Windows.Forms.Padding(5)
        Me.btnAutoRun.Name = "btnAutoRun"
        Me.btnAutoRun.Size = New System.Drawing.Size(150, 70)
        Me.btnAutoRun.TabIndex = 4
        Me.btnAutoRun.Text = "AutoRun"
        Me.btnAutoRun.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnStop.FlatAppearance.BorderSize = 0
        Me.btnStop.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnStop.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnStop.Location = New System.Drawing.Point(1082, 204)
        Me.btnStop.Margin = New System.Windows.Forms.Padding(5)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(150, 70)
        Me.btnStop.TabIndex = 5
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReset.FlatAppearance.BorderSize = 0
        Me.btnReset.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnReset.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnReset.Location = New System.Drawing.Point(1082, 44)
        Me.btnReset.Margin = New System.Windows.Forms.Padding(5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(150, 70)
        Me.btnReset.TabIndex = 6
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLoad.FlatAppearance.BorderSize = 0
        Me.btnLoad.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnLoad.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnLoad.Location = New System.Drawing.Point(1082, 284)
        Me.btnLoad.Margin = New System.Windows.Forms.Padding(5)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(150, 70)
        Me.btnLoad.TabIndex = 7
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnUnload
        '
        Me.btnUnload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUnload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUnload.FlatAppearance.BorderSize = 0
        Me.btnUnload.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnUnload.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnUnload.Location = New System.Drawing.Point(1082, 364)
        Me.btnUnload.Margin = New System.Windows.Forms.Padding(5)
        Me.btnUnload.Name = "btnUnload"
        Me.btnUnload.Size = New System.Drawing.Size(150, 70)
        Me.btnUnload.TabIndex = 8
        Me.btnUnload.Text = "UnLoad"
        Me.btnUnload.UseVisualStyleBackColor = True
        '
        'btnOKExit
        '
        Me.btnOKExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOKExit.BackgroundImage = CType(resources.GetObject("btnOKExit.BackgroundImage"), System.Drawing.Image)
        Me.btnOKExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnOKExit.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnOKExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnOKExit.Location = New System.Drawing.Point(1082, 528)
        Me.btnOKExit.Margin = New System.Windows.Forms.Padding(5)
        Me.btnOKExit.Name = "btnOKExit"
        Me.btnOKExit.Size = New System.Drawing.Size(150, 70)
        Me.btnOKExit.TabIndex = 39
        Me.btnOKExit.UseVisualStyleBackColor = True
        '
        'frmConveyor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(1246, 612)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnUnload)
        Me.Controls.Add(Me.btnOKExit)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnAutoRun)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConveyor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Conveyor"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageStation1.ResumeLayout(False)
        Me.TabPageStation1.PerformLayout()
        CType(Me.nmcS1HeaterValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageStation2.ResumeLayout(False)
        Me.TabPageStation2.PerformLayout()
        CType(Me.nmcS2HeaterValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageStation3.ResumeLayout(False)
        Me.TabPageStation3.PerformLayout()
        CType(Me.nmcS3HeaterValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageLoadULoad.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPagePageBase.ResumeLayout(False)
        Me.TabPagePageBase.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageStation1 As System.Windows.Forms.TabPage
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ckbStation1TopLiftUpDown As System.Windows.Forms.CheckBox
    Friend WithEvents lblStation1TopLiftDownSensor As System.Windows.Forms.Label
    Friend WithEvents lblStation1TopLiftUpSensor As System.Windows.Forms.Label
    Friend WithEvents ckbStation1Stopper As System.Windows.Forms.CheckBox
    Friend WithEvents lblStation1StopperDownSensor As System.Windows.Forms.Label
    Friend WithEvents lblStation1StopperUpSensor As System.Windows.Forms.Label
    Friend WithEvents lblStation1TrayReadySensor As System.Windows.Forms.Label
    Friend WithEvents TabPageStation2 As System.Windows.Forms.TabPage
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ckbStation2Stopper As System.Windows.Forms.CheckBox
    Friend WithEvents lblStation2StopperDownSensor As System.Windows.Forms.Label
    Friend WithEvents lblStation2StopperUpSensor As System.Windows.Forms.Label
    Friend WithEvents lblStation2TrayReadySensor As System.Windows.Forms.Label
    Friend WithEvents TabPageStation3 As System.Windows.Forms.TabPage
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ckbStation3TopLiftUpDown As System.Windows.Forms.CheckBox
    Friend WithEvents lblStation3TopLiftDownSensor As System.Windows.Forms.Label
    Friend WithEvents lblStation3TopLiftUpSensor As System.Windows.Forms.Label
    Friend WithEvents ckbStation3Stopper As System.Windows.Forms.CheckBox
    Friend WithEvents lblStation3StopperDownSensor As System.Windows.Forms.Label
    Friend WithEvents lblStation3StopperUpSensor As System.Windows.Forms.Label
    Friend WithEvents lblStation3TrayReadySensor As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnRolling As System.Windows.Forms.Button
    Friend WithEvents btnUp2 As System.Windows.Forms.Button
    Friend WithEvents btnMotorStop2 As System.Windows.Forms.Button
    Friend WithEvents btnDown2 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents btnAutoRun As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents TabPageLoadULoad As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ckbUnLoadBoardAvaiable As System.Windows.Forms.CheckBox
    Friend WithEvents lblUnLoadRecieveTray As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ckbLoadRecieveTray As System.Windows.Forms.CheckBox
    Friend WithEvents lblLoadBoardAvaiable As System.Windows.Forms.Label
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ckbTrayClamper As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblTrayClamperOn As System.Windows.Forms.Label
    Friend WithEvents lblTrayClamperOff As System.Windows.Forms.Label
    Friend WithEvents ckbHoldBack As System.Windows.Forms.CheckBox
    Friend WithEvents btnSetTemperature1 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnSetTemperature2 As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnSetTemperature3 As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnGetTemperature1 As System.Windows.Forms.Button
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnUnload As System.Windows.Forms.Button
    Friend WithEvents TabPagePageBase As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtStation3heaterSet As System.Windows.Forms.TextBox
    Friend WithEvents ckbNoUseStation3 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtStation2heaterSet As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtStation2TopLiftDownPos As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cboStation2TopLiftSpeed As System.Windows.Forms.ComboBox
    Friend WithEvents txtStation2TopLiftUpPos As System.Windows.Forms.TextBox
    Friend WithEvents ckbNoUseStation2 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtStation1heaterSet As System.Windows.Forms.TextBox
    Friend WithEvents ckbNoUseStation1 As System.Windows.Forms.CheckBox
    Friend WithEvents lblRollSpeed As System.Windows.Forms.Label
    Friend WithEvents cboRollSpeed As System.Windows.Forms.ComboBox
    Friend WithEvents btnRolling2 As System.Windows.Forms.Button
    Friend WithEvents btnRolling3 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnMoveFinish As System.Windows.Forms.Button
    Friend WithEvents cmbRunMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents nmcS1HeaterValue As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcS2HeaterValue As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcS3HeaterValue As System.Windows.Forms.NumericUpDown
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnOKExit As System.Windows.Forms.Button

End Class

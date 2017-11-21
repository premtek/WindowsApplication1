<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSysDebug
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblOverallCommand = New System.Windows.Forms.Label()
        Me.lblOverallSysNum = New System.Windows.Forms.Label()
        Me.lblOverallStatus = New System.Windows.Forms.Label()
        Me.lblMachineACommand = New System.Windows.Forms.Label()
        Me.lblMachineASysNum = New System.Windows.Forms.Label()
        Me.lblMachineAStatus = New System.Windows.Forms.Label()
        Me.lblMachineBCommand = New System.Windows.Forms.Label()
        Me.lblMachineBSysNum = New System.Windows.Forms.Label()
        Me.lblMachineBStatus = New System.Windows.Forms.Label()
        Me.lblDispStage1Command = New System.Windows.Forms.Label()
        Me.lblDispStage1SysNum = New System.Windows.Forms.Label()
        Me.lblDispStage1Status = New System.Windows.Forms.Label()
        Me.lblDispStage2Command = New System.Windows.Forms.Label()
        Me.lblDispStage2SysNum = New System.Windows.Forms.Label()
        Me.lblDispStage2Status = New System.Windows.Forms.Label()
        Me.lblDispStage3Command = New System.Windows.Forms.Label()
        Me.lblDispStage3SysNum = New System.Windows.Forms.Label()
        Me.lblDispStage3Status = New System.Windows.Forms.Label()
        Me.lblDispStage4Command = New System.Windows.Forms.Label()
        Me.lblDispStage4SysNum = New System.Windows.Forms.Label()
        Me.lblDispStage4Status = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(175, 39)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "OverAll"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.Location = New System.Drawing.Point(14, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(175, 39)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "MachineA"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 137)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(175, 39)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "MachineB"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 177)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(175, 39)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "DispStage1"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.Location = New System.Drawing.Point(14, 217)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(175, 39)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "DispStage2"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.Location = New System.Drawing.Point(14, 257)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(175, 39)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "DispStage3"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label7.Location = New System.Drawing.Point(14, 297)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(175, 39)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "DispStage4"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label8.Location = New System.Drawing.Point(14, 17)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(175, 39)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "System"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label9.Location = New System.Drawing.Point(196, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(175, 39)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Command"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label10.Location = New System.Drawing.Point(378, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(175, 39)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "SysNum"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label11.Location = New System.Drawing.Point(560, 17)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(175, 39)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Status"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 300
        '
        'lblOverallCommand
        '
        Me.lblOverallCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOverallCommand.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblOverallCommand.Location = New System.Drawing.Point(196, 57)
        Me.lblOverallCommand.Name = "lblOverallCommand"
        Me.lblOverallCommand.Size = New System.Drawing.Size(175, 39)
        Me.lblOverallCommand.TabIndex = 0
        Me.lblOverallCommand.Text = "######"
        Me.lblOverallCommand.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblOverallSysNum
        '
        Me.lblOverallSysNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOverallSysNum.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblOverallSysNum.Location = New System.Drawing.Point(378, 57)
        Me.lblOverallSysNum.Name = "lblOverallSysNum"
        Me.lblOverallSysNum.Size = New System.Drawing.Size(175, 39)
        Me.lblOverallSysNum.TabIndex = 0
        Me.lblOverallSysNum.Text = "######"
        Me.lblOverallSysNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblOverallStatus
        '
        Me.lblOverallStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOverallStatus.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblOverallStatus.Location = New System.Drawing.Point(560, 57)
        Me.lblOverallStatus.Name = "lblOverallStatus"
        Me.lblOverallStatus.Size = New System.Drawing.Size(175, 39)
        Me.lblOverallStatus.TabIndex = 0
        Me.lblOverallStatus.Text = "######"
        Me.lblOverallStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMachineACommand
        '
        Me.lblMachineACommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMachineACommand.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblMachineACommand.Location = New System.Drawing.Point(196, 97)
        Me.lblMachineACommand.Name = "lblMachineACommand"
        Me.lblMachineACommand.Size = New System.Drawing.Size(175, 39)
        Me.lblMachineACommand.TabIndex = 0
        Me.lblMachineACommand.Text = "######"
        Me.lblMachineACommand.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMachineASysNum
        '
        Me.lblMachineASysNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMachineASysNum.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblMachineASysNum.Location = New System.Drawing.Point(378, 97)
        Me.lblMachineASysNum.Name = "lblMachineASysNum"
        Me.lblMachineASysNum.Size = New System.Drawing.Size(175, 39)
        Me.lblMachineASysNum.TabIndex = 0
        Me.lblMachineASysNum.Text = "######"
        Me.lblMachineASysNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMachineAStatus
        '
        Me.lblMachineAStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMachineAStatus.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblMachineAStatus.Location = New System.Drawing.Point(560, 97)
        Me.lblMachineAStatus.Name = "lblMachineAStatus"
        Me.lblMachineAStatus.Size = New System.Drawing.Size(175, 39)
        Me.lblMachineAStatus.TabIndex = 0
        Me.lblMachineAStatus.Text = "######"
        Me.lblMachineAStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMachineBCommand
        '
        Me.lblMachineBCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMachineBCommand.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblMachineBCommand.Location = New System.Drawing.Point(196, 137)
        Me.lblMachineBCommand.Name = "lblMachineBCommand"
        Me.lblMachineBCommand.Size = New System.Drawing.Size(175, 39)
        Me.lblMachineBCommand.TabIndex = 0
        Me.lblMachineBCommand.Text = "######"
        Me.lblMachineBCommand.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMachineBSysNum
        '
        Me.lblMachineBSysNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMachineBSysNum.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblMachineBSysNum.Location = New System.Drawing.Point(378, 137)
        Me.lblMachineBSysNum.Name = "lblMachineBSysNum"
        Me.lblMachineBSysNum.Size = New System.Drawing.Size(175, 39)
        Me.lblMachineBSysNum.TabIndex = 0
        Me.lblMachineBSysNum.Text = "######"
        Me.lblMachineBSysNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMachineBStatus
        '
        Me.lblMachineBStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMachineBStatus.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblMachineBStatus.Location = New System.Drawing.Point(560, 137)
        Me.lblMachineBStatus.Name = "lblMachineBStatus"
        Me.lblMachineBStatus.Size = New System.Drawing.Size(175, 39)
        Me.lblMachineBStatus.TabIndex = 0
        Me.lblMachineBStatus.Text = "######"
        Me.lblMachineBStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDispStage1Command
        '
        Me.lblDispStage1Command.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispStage1Command.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDispStage1Command.Location = New System.Drawing.Point(196, 177)
        Me.lblDispStage1Command.Name = "lblDispStage1Command"
        Me.lblDispStage1Command.Size = New System.Drawing.Size(175, 39)
        Me.lblDispStage1Command.TabIndex = 0
        Me.lblDispStage1Command.Text = "######"
        Me.lblDispStage1Command.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDispStage1SysNum
        '
        Me.lblDispStage1SysNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispStage1SysNum.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDispStage1SysNum.Location = New System.Drawing.Point(378, 177)
        Me.lblDispStage1SysNum.Name = "lblDispStage1SysNum"
        Me.lblDispStage1SysNum.Size = New System.Drawing.Size(175, 39)
        Me.lblDispStage1SysNum.TabIndex = 0
        Me.lblDispStage1SysNum.Text = "######"
        Me.lblDispStage1SysNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDispStage1Status
        '
        Me.lblDispStage1Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispStage1Status.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDispStage1Status.Location = New System.Drawing.Point(560, 177)
        Me.lblDispStage1Status.Name = "lblDispStage1Status"
        Me.lblDispStage1Status.Size = New System.Drawing.Size(175, 39)
        Me.lblDispStage1Status.TabIndex = 0
        Me.lblDispStage1Status.Text = "######"
        Me.lblDispStage1Status.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDispStage2Command
        '
        Me.lblDispStage2Command.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispStage2Command.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDispStage2Command.Location = New System.Drawing.Point(196, 217)
        Me.lblDispStage2Command.Name = "lblDispStage2Command"
        Me.lblDispStage2Command.Size = New System.Drawing.Size(175, 39)
        Me.lblDispStage2Command.TabIndex = 0
        Me.lblDispStage2Command.Text = "######"
        Me.lblDispStage2Command.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDispStage2SysNum
        '
        Me.lblDispStage2SysNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispStage2SysNum.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDispStage2SysNum.Location = New System.Drawing.Point(378, 217)
        Me.lblDispStage2SysNum.Name = "lblDispStage2SysNum"
        Me.lblDispStage2SysNum.Size = New System.Drawing.Size(175, 39)
        Me.lblDispStage2SysNum.TabIndex = 0
        Me.lblDispStage2SysNum.Text = "######"
        Me.lblDispStage2SysNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDispStage2Status
        '
        Me.lblDispStage2Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispStage2Status.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDispStage2Status.Location = New System.Drawing.Point(560, 217)
        Me.lblDispStage2Status.Name = "lblDispStage2Status"
        Me.lblDispStage2Status.Size = New System.Drawing.Size(175, 39)
        Me.lblDispStage2Status.TabIndex = 0
        Me.lblDispStage2Status.Text = "######"
        Me.lblDispStage2Status.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDispStage3Command
        '
        Me.lblDispStage3Command.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispStage3Command.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDispStage3Command.Location = New System.Drawing.Point(196, 257)
        Me.lblDispStage3Command.Name = "lblDispStage3Command"
        Me.lblDispStage3Command.Size = New System.Drawing.Size(175, 39)
        Me.lblDispStage3Command.TabIndex = 0
        Me.lblDispStage3Command.Text = "######"
        Me.lblDispStage3Command.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDispStage3SysNum
        '
        Me.lblDispStage3SysNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispStage3SysNum.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDispStage3SysNum.Location = New System.Drawing.Point(378, 257)
        Me.lblDispStage3SysNum.Name = "lblDispStage3SysNum"
        Me.lblDispStage3SysNum.Size = New System.Drawing.Size(175, 39)
        Me.lblDispStage3SysNum.TabIndex = 0
        Me.lblDispStage3SysNum.Text = "######"
        Me.lblDispStage3SysNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDispStage3Status
        '
        Me.lblDispStage3Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispStage3Status.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDispStage3Status.Location = New System.Drawing.Point(560, 257)
        Me.lblDispStage3Status.Name = "lblDispStage3Status"
        Me.lblDispStage3Status.Size = New System.Drawing.Size(175, 39)
        Me.lblDispStage3Status.TabIndex = 0
        Me.lblDispStage3Status.Text = "######"
        Me.lblDispStage3Status.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDispStage4Command
        '
        Me.lblDispStage4Command.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispStage4Command.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDispStage4Command.Location = New System.Drawing.Point(196, 297)
        Me.lblDispStage4Command.Name = "lblDispStage4Command"
        Me.lblDispStage4Command.Size = New System.Drawing.Size(175, 39)
        Me.lblDispStage4Command.TabIndex = 0
        Me.lblDispStage4Command.Text = "######"
        Me.lblDispStage4Command.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDispStage4SysNum
        '
        Me.lblDispStage4SysNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispStage4SysNum.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDispStage4SysNum.Location = New System.Drawing.Point(378, 297)
        Me.lblDispStage4SysNum.Name = "lblDispStage4SysNum"
        Me.lblDispStage4SysNum.Size = New System.Drawing.Size(175, 39)
        Me.lblDispStage4SysNum.TabIndex = 0
        Me.lblDispStage4SysNum.Text = "######"
        Me.lblDispStage4SysNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDispStage4Status
        '
        Me.lblDispStage4Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispStage4Status.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblDispStage4Status.Location = New System.Drawing.Point(560, 297)
        Me.lblDispStage4Status.Name = "lblDispStage4Status"
        Me.lblDispStage4Status.Size = New System.Drawing.Size(175, 39)
        Me.lblDispStage4Status.TabIndex = 0
        Me.lblDispStage4Status.Text = "######"
        Me.lblDispStage4Status.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmSysDebug
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(747, 345)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblDispStage4Status)
        Me.Controls.Add(Me.lblDispStage4SysNum)
        Me.Controls.Add(Me.lblDispStage3Status)
        Me.Controls.Add(Me.lblDispStage3SysNum)
        Me.Controls.Add(Me.lblDispStage2Status)
        Me.Controls.Add(Me.lblDispStage2SysNum)
        Me.Controls.Add(Me.lblDispStage1Status)
        Me.Controls.Add(Me.lblDispStage1SysNum)
        Me.Controls.Add(Me.lblMachineBStatus)
        Me.Controls.Add(Me.lblDispStage4Command)
        Me.Controls.Add(Me.lblMachineBSysNum)
        Me.Controls.Add(Me.lblDispStage3Command)
        Me.Controls.Add(Me.lblMachineAStatus)
        Me.Controls.Add(Me.lblDispStage2Command)
        Me.Controls.Add(Me.lblMachineASysNum)
        Me.Controls.Add(Me.lblDispStage1Command)
        Me.Controls.Add(Me.lblOverallStatus)
        Me.Controls.Add(Me.lblMachineBCommand)
        Me.Controls.Add(Me.lblOverallSysNum)
        Me.Controls.Add(Me.lblMachineACommand)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblOverallCommand)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSysDebug"
        Me.Text = "frmSysDebug"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblOverallCommand As System.Windows.Forms.Label
    Friend WithEvents lblOverallSysNum As System.Windows.Forms.Label
    Friend WithEvents lblOverallStatus As System.Windows.Forms.Label
    Friend WithEvents lblMachineACommand As System.Windows.Forms.Label
    Friend WithEvents lblMachineASysNum As System.Windows.Forms.Label
    Friend WithEvents lblMachineAStatus As System.Windows.Forms.Label
    Friend WithEvents lblMachineBCommand As System.Windows.Forms.Label
    Friend WithEvents lblMachineBSysNum As System.Windows.Forms.Label
    Friend WithEvents lblMachineBStatus As System.Windows.Forms.Label
    Friend WithEvents lblDispStage1Command As System.Windows.Forms.Label
    Friend WithEvents lblDispStage1SysNum As System.Windows.Forms.Label
    Friend WithEvents lblDispStage1Status As System.Windows.Forms.Label
    Friend WithEvents lblDispStage2Command As System.Windows.Forms.Label
    Friend WithEvents lblDispStage2SysNum As System.Windows.Forms.Label
    Friend WithEvents lblDispStage2Status As System.Windows.Forms.Label
    Friend WithEvents lblDispStage3Command As System.Windows.Forms.Label
    Friend WithEvents lblDispStage3SysNum As System.Windows.Forms.Label
    Friend WithEvents lblDispStage3Status As System.Windows.Forms.Label
    Friend WithEvents lblDispStage4Command As System.Windows.Forms.Label
    Friend WithEvents lblDispStage4SysNum As System.Windows.Forms.Label
    Friend WithEvents lblDispStage4Status As System.Windows.Forms.Label
End Class

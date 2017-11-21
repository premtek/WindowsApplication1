<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMotionOp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMotionOp))
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.lblALM = New System.Windows.Forms.Label()
        Me.lblAxisPosition = New System.Windows.Forms.Label()
        Me.lblPel = New System.Windows.Forms.Label()
        Me.lblNel = New System.Windows.Forms.Label()
        Me.lblOrg = New System.Windows.Forms.Label()
        Me.lblAxisName = New System.Windows.Forms.Label()
        Me.tmrMotorMenu = New System.Windows.Forms.Timer(Me.components)
        Me.chkLoop = New System.Windows.Forms.CheckBox()
        Me.btnConfig1 = New System.Windows.Forms.Button()
        Me.btnConfig2 = New System.Windows.Forms.Button()
        Me.lblUnit1 = New System.Windows.Forms.Label()
        Me.lblUnit2 = New System.Windows.Forms.Label()
        Me.nmcTargetPos1 = New System.Windows.Forms.NumericUpDown()
        Me.nmcTargetPos2 = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnGo2 = New System.Windows.Forms.Button()
        Me.btnGo1 = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnAdvance = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnHome = New System.Windows.Forms.Button()
        Me.btnSpeed = New System.Windows.Forms.Button()
        Me.btnServo = New System.Windows.Forms.Button()
        Me.palPEL = New System.Windows.Forms.Panel()
        Me.palNEL = New System.Windows.Forms.Panel()
        Me.palALM = New System.Windows.Forms.Panel()
        Me.btnPrevAxis = New System.Windows.Forms.Button()
        Me.palORG = New System.Windows.Forms.Panel()
        Me.btnNextAxis = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnLeft = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnRight = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.nmcTargetPos1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcTargetPos2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'CheckBox1
        '
        resources.ApplyResources(Me.CheckBox1, "CheckBox1")
        Me.CheckBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'lblALM
        '
        Me.lblALM.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblALM, "lblALM")
        Me.lblALM.Name = "lblALM"
        '
        'lblAxisPosition
        '
        Me.lblAxisPosition.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblAxisPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.lblAxisPosition, "lblAxisPosition")
        Me.lblAxisPosition.Name = "lblAxisPosition"
        '
        'lblPel
        '
        Me.lblPel.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblPel, "lblPel")
        Me.lblPel.Name = "lblPel"
        '
        'lblNel
        '
        Me.lblNel.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblNel, "lblNel")
        Me.lblNel.Name = "lblNel"
        '
        'lblOrg
        '
        Me.lblOrg.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblOrg, "lblOrg")
        Me.lblOrg.Name = "lblOrg"
        '
        'lblAxisName
        '
        Me.lblAxisName.BackColor = System.Drawing.SystemColors.ControlDark
        Me.lblAxisName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.lblAxisName, "lblAxisName")
        Me.lblAxisName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblAxisName.Name = "lblAxisName"
        '
        'tmrMotorMenu
        '
        '
        'chkLoop
        '
        resources.ApplyResources(Me.chkLoop, "chkLoop")
        Me.chkLoop.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.chkLoop.Name = "chkLoop"
        Me.chkLoop.UseVisualStyleBackColor = True
        '
        'btnConfig1
        '
        resources.ApplyResources(Me.btnConfig1, "btnConfig1")
        Me.btnConfig1.FlatAppearance.BorderSize = 0
        Me.btnConfig1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnConfig1.Name = "btnConfig1"
        Me.btnConfig1.UseVisualStyleBackColor = True
        '
        'btnConfig2
        '
        resources.ApplyResources(Me.btnConfig2, "btnConfig2")
        Me.btnConfig2.FlatAppearance.BorderSize = 0
        Me.btnConfig2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnConfig2.Name = "btnConfig2"
        Me.btnConfig2.UseVisualStyleBackColor = True
        '
        'lblUnit1
        '
        resources.ApplyResources(Me.lblUnit1, "lblUnit1")
        Me.lblUnit1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUnit1.Name = "lblUnit1"
        '
        'lblUnit2
        '
        resources.ApplyResources(Me.lblUnit2, "lblUnit2")
        Me.lblUnit2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUnit2.Name = "lblUnit2"
        '
        'nmcTargetPos1
        '
        Me.nmcTargetPos1.DecimalPlaces = 3
        resources.ApplyResources(Me.nmcTargetPos1, "nmcTargetPos1")
        Me.nmcTargetPos1.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcTargetPos1.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcTargetPos1.Name = "nmcTargetPos1"
        Me.nmcTargetPos1.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'nmcTargetPos2
        '
        Me.nmcTargetPos2.DecimalPlaces = 3
        resources.ApplyResources(Me.nmcTargetPos2, "nmcTargetPos2")
        Me.nmcTargetPos2.Maximum = New Decimal(New Integer() {999999, 0, 0, 196608})
        Me.nmcTargetPos2.Minimum = New Decimal(New Integer() {999999, 0, 0, -2147287040})
        Me.nmcTargetPos2.Name = "nmcTargetPos2"
        Me.nmcTargetPos2.Value = New Decimal(New Integer() {5, 0, 0, -2147483648})
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.nmcTargetPos2)
        Me.GroupBox2.Controls.Add(Me.nmcTargetPos1)
        Me.GroupBox2.Controls.Add(Me.lblUnit2)
        Me.GroupBox2.Controls.Add(Me.lblUnit1)
        Me.GroupBox2.Controls.Add(Me.btnConfig2)
        Me.GroupBox2.Controls.Add(Me.btnConfig1)
        Me.GroupBox2.Controls.Add(Me.chkLoop)
        Me.GroupBox2.Controls.Add(Me.btnGo2)
        Me.GroupBox2.Controls.Add(Me.btnGo1)
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'btnGo2
        '
        Me.btnGo2.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        resources.ApplyResources(Me.btnGo2, "btnGo2")
        Me.btnGo2.FlatAppearance.BorderSize = 0
        Me.btnGo2.Name = "btnGo2"
        Me.ToolTip1.SetToolTip(Me.btnGo2, resources.GetString("btnGo2.ToolTip"))
        Me.btnGo2.UseVisualStyleBackColor = True
        '
        'btnGo1
        '
        Me.btnGo1.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.goPos
        resources.ApplyResources(Me.btnGo1, "btnGo1")
        Me.btnGo1.FlatAppearance.BorderSize = 0
        Me.btnGo1.Name = "btnGo1"
        Me.ToolTip1.SetToolTip(Me.btnGo1, resources.GetString("btnGo1.ToolTip"))
        Me.btnGo1.UseVisualStyleBackColor = True
        '
        'btnAdvance
        '
        Me.btnAdvance.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.setup1
        resources.ApplyResources(Me.btnAdvance, "btnAdvance")
        Me.btnAdvance.FlatAppearance.BorderSize = 0
        Me.btnAdvance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnAdvance.Name = "btnAdvance"
        Me.ToolTip1.SetToolTip(Me.btnAdvance, resources.GetString("btnAdvance.ToolTip"))
        Me.btnAdvance.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        resources.ApplyResources(Me.btnExit, "btnExit")
        Me.btnExit.FlatAppearance.BorderSize = 0
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnExit.Name = "btnExit"
        Me.ToolTip1.SetToolTip(Me.btnExit, resources.GetString("btnExit.ToolTip"))
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnHome
        '
        Me.btnHome.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.home1
        resources.ApplyResources(Me.btnHome, "btnHome")
        Me.btnHome.FlatAppearance.BorderSize = 0
        Me.btnHome.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnHome.Name = "btnHome"
        Me.ToolTip1.SetToolTip(Me.btnHome, resources.GetString("btnHome.ToolTip"))
        Me.btnHome.UseVisualStyleBackColor = True
        '
        'btnSpeed
        '
        Me.btnSpeed.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SpeedLow
        resources.ApplyResources(Me.btnSpeed, "btnSpeed")
        Me.btnSpeed.FlatAppearance.BorderSize = 0
        Me.btnSpeed.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSpeed.Name = "btnSpeed"
        Me.ToolTip1.SetToolTip(Me.btnSpeed, resources.GetString("btnSpeed.ToolTip"))
        Me.btnSpeed.UseVisualStyleBackColor = True
        '
        'btnServo
        '
        Me.btnServo.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.ServoOff
        resources.ApplyResources(Me.btnServo, "btnServo")
        Me.btnServo.FlatAppearance.BorderSize = 0
        Me.btnServo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnServo.Name = "btnServo"
        Me.ToolTip1.SetToolTip(Me.btnServo, resources.GetString("btnServo.ToolTip"))
        Me.btnServo.UseVisualStyleBackColor = True
        '
        'palPEL
        '
        Me.palPEL.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.li_23
        resources.ApplyResources(Me.palPEL, "palPEL")
        Me.palPEL.Name = "palPEL"
        '
        'palNEL
        '
        Me.palNEL.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.li_23
        resources.ApplyResources(Me.palNEL, "palNEL")
        Me.palNEL.Name = "palNEL"
        '
        'palALM
        '
        Me.palALM.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.li_23
        resources.ApplyResources(Me.palALM, "palALM")
        Me.palALM.Name = "palALM"
        '
        'btnPrevAxis
        '
        Me.btnPrevAxis.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Prev
        resources.ApplyResources(Me.btnPrevAxis, "btnPrevAxis")
        Me.btnPrevAxis.FlatAppearance.BorderSize = 0
        Me.btnPrevAxis.Name = "btnPrevAxis"
        Me.btnPrevAxis.UseVisualStyleBackColor = True
        '
        'palORG
        '
        Me.palORG.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.li_23
        resources.ApplyResources(Me.palORG, "palORG")
        Me.palORG.Name = "palORG"
        '
        'btnNextAxis
        '
        Me.btnNextAxis.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._Next
        resources.ApplyResources(Me.btnNextAxis, "btnNextAxis")
        Me.btnNextAxis.FlatAppearance.BorderSize = 0
        Me.btnNextAxis.Name = "btnNextAxis"
        Me.btnNextAxis.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        resources.ApplyResources(Me.btnUp, "btnUp")
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.Name = "btnUp"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'btnLeft
        '
        resources.ApplyResources(Me.btnLeft, "btnLeft")
        Me.btnLeft.FlatAppearance.BorderSize = 0
        Me.btnLeft.Name = "btnLeft"
        Me.btnLeft.UseVisualStyleBackColor = True
        '
        'btnDown
        '
        resources.ApplyResources(Me.btnDown, "btnDown")
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.Name = "btnDown"
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnRight
        '
        resources.ApplyResources(Me.btnRight, "btnRight")
        Me.btnRight.FlatAppearance.BorderSize = 0
        Me.btnRight.Name = "btnRight"
        Me.btnRight.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'frmMotionOp
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.palPEL)
        Me.Controls.Add(Me.btnAdvance)
        Me.Controls.Add(Me.palNEL)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.palALM)
        Me.Controls.Add(Me.btnPrevAxis)
        Me.Controls.Add(Me.palORG)
        Me.Controls.Add(Me.btnHome)
        Me.Controls.Add(Me.btnSpeed)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.btnServo)
        Me.Controls.Add(Me.lblALM)
        Me.Controls.Add(Me.lblAxisName)
        Me.Controls.Add(Me.lblOrg)
        Me.Controls.Add(Me.btnNextAxis)
        Me.Controls.Add(Me.lblNel)
        Me.Controls.Add(Me.lblAxisPosition)
        Me.Controls.Add(Me.lblPel)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.btnLeft)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnRight)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMotionOp"
        CType(Me.nmcTargetPos1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcTargetPos2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSpeed As System.Windows.Forms.Button
    Friend WithEvents btnLeft As System.Windows.Forms.Button
    Friend WithEvents btnRight As System.Windows.Forms.Button
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents lblPel As System.Windows.Forms.Label
    Friend WithEvents lblNel As System.Windows.Forms.Label
    Friend WithEvents lblOrg As System.Windows.Forms.Label
    Friend WithEvents lblAxisName As System.Windows.Forms.Label
    Friend WithEvents btnServo As System.Windows.Forms.Button
    Friend WithEvents btnHome As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents tmrMotorMenu As System.Windows.Forms.Timer
    Friend WithEvents lblAxisPosition As System.Windows.Forms.Label
    Friend WithEvents btnAdvance As System.Windows.Forms.Button
    Friend WithEvents btnPrevAxis As System.Windows.Forms.Button
    Friend WithEvents btnNextAxis As System.Windows.Forms.Button
    Friend WithEvents lblALM As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents palALM As System.Windows.Forms.Panel
    Friend WithEvents palORG As System.Windows.Forms.Panel
    Friend WithEvents palPEL As System.Windows.Forms.Panel
    Friend WithEvents palNEL As System.Windows.Forms.Panel
    Friend WithEvents btnGo1 As System.Windows.Forms.Button
    Friend WithEvents btnGo2 As System.Windows.Forms.Button
    Friend WithEvents chkLoop As System.Windows.Forms.CheckBox
    Friend WithEvents btnConfig1 As System.Windows.Forms.Button
    Friend WithEvents btnConfig2 As System.Windows.Forms.Button
    Friend WithEvents lblUnit1 As System.Windows.Forms.Label
    Friend WithEvents lblUnit2 As System.Windows.Forms.Label
    Friend WithEvents nmcTargetPos1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcTargetPos2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class

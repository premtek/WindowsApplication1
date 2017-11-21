<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucJoyStick
    Inherits System.Windows.Forms.UserControl

    'UserControl 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()>
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucJoyStick))
        Me.grpJoyStick = New System.Windows.Forms.GroupBox()
        Me.txtPosZ = New System.Windows.Forms.TextBox()
        Me.txtPosY = New System.Windows.Forms.TextBox()
        Me.txtPosX = New System.Windows.Forms.TextBox()
        Me.btnGo1 = New System.Windows.Forms.Button()
        Me.btnCoordinate = New System.Windows.Forms.Button()
        Me.btnMode = New System.Windows.Forms.Button()
        Me.btnSpeed = New System.Windows.Forms.Button()
        Me.lblAxis2Unit = New System.Windows.Forms.Label()
        Me.lblAxis1Unit = New System.Windows.Forms.Label()
        Me.lblAxis0Unit = New System.Windows.Forms.Label()
        Me.lblAxis2 = New System.Windows.Forms.Label()
        Me.lblAxis1 = New System.Windows.Forms.Label()
        Me.lblAxis0 = New System.Windows.Forms.Label()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnRight = New System.Windows.Forms.Button()
        Me.btnForward = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnLeft = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpJoyStick.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpJoyStick
        '
        Me.grpJoyStick.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.grpJoyStick.Controls.Add(Me.txtPosZ)
        Me.grpJoyStick.Controls.Add(Me.txtPosY)
        Me.grpJoyStick.Controls.Add(Me.txtPosX)
        Me.grpJoyStick.Controls.Add(Me.btnGo1)
        Me.grpJoyStick.Controls.Add(Me.btnCoordinate)
        Me.grpJoyStick.Controls.Add(Me.btnMode)
        Me.grpJoyStick.Controls.Add(Me.btnSpeed)
        Me.grpJoyStick.Controls.Add(Me.lblAxis2Unit)
        Me.grpJoyStick.Controls.Add(Me.lblAxis1Unit)
        Me.grpJoyStick.Controls.Add(Me.lblAxis0Unit)
        Me.grpJoyStick.Controls.Add(Me.lblAxis2)
        Me.grpJoyStick.Controls.Add(Me.lblAxis1)
        Me.grpJoyStick.Controls.Add(Me.lblAxis0)
        Me.grpJoyStick.Controls.Add(Me.btnUp)
        Me.grpJoyStick.Controls.Add(Me.btnDown)
        Me.grpJoyStick.Controls.Add(Me.btnRight)
        Me.grpJoyStick.Controls.Add(Me.btnForward)
        Me.grpJoyStick.Controls.Add(Me.btnBack)
        Me.grpJoyStick.Controls.Add(Me.btnLeft)
        resources.ApplyResources(Me.grpJoyStick, "grpJoyStick")
        Me.grpJoyStick.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpJoyStick.Name = "grpJoyStick"
        Me.grpJoyStick.TabStop = False
        '
        'txtPosZ
        '
        Me.txtPosZ.BackColor = System.Drawing.SystemColors.Control
        Me.txtPosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtPosZ, "txtPosZ")
        Me.txtPosZ.Name = "txtPosZ"
        '
        'txtPosY
        '
        Me.txtPosY.BackColor = System.Drawing.SystemColors.Control
        Me.txtPosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtPosY, "txtPosY")
        Me.txtPosY.Name = "txtPosY"
        '
        'txtPosX
        '
        Me.txtPosX.BackColor = System.Drawing.SystemColors.Control
        Me.txtPosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtPosX, "txtPosX")
        Me.txtPosX.Name = "txtPosX"
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
        'btnCoordinate
        '
        resources.ApplyResources(Me.btnCoordinate, "btnCoordinate")
        Me.btnCoordinate.FlatAppearance.BorderSize = 0
        Me.btnCoordinate.Name = "btnCoordinate"
        Me.btnCoordinate.UseVisualStyleBackColor = True
        '
        'btnMode
        '
        resources.ApplyResources(Me.btnMode, "btnMode")
        Me.btnMode.FlatAppearance.BorderSize = 0
        Me.btnMode.Name = "btnMode"
        Me.btnMode.UseVisualStyleBackColor = True
        '
        'btnSpeed
        '
        resources.ApplyResources(Me.btnSpeed, "btnSpeed")
        Me.btnSpeed.FlatAppearance.BorderSize = 0
        Me.btnSpeed.Image = Global.WindowsApplication1.My.Resources.Resources.SpeedLow
        Me.btnSpeed.Name = "btnSpeed"
        Me.btnSpeed.UseVisualStyleBackColor = True
        '
        'lblAxis2Unit
        '
        resources.ApplyResources(Me.lblAxis2Unit, "lblAxis2Unit")
        Me.lblAxis2Unit.Name = "lblAxis2Unit"
        '
        'lblAxis1Unit
        '
        resources.ApplyResources(Me.lblAxis1Unit, "lblAxis1Unit")
        Me.lblAxis1Unit.Name = "lblAxis1Unit"
        '
        'lblAxis0Unit
        '
        resources.ApplyResources(Me.lblAxis0Unit, "lblAxis0Unit")
        Me.lblAxis0Unit.Name = "lblAxis0Unit"
        '
        'lblAxis2
        '
        resources.ApplyResources(Me.lblAxis2, "lblAxis2")
        Me.lblAxis2.Name = "lblAxis2"
        '
        'lblAxis1
        '
        resources.ApplyResources(Me.lblAxis1, "lblAxis1")
        Me.lblAxis1.Name = "lblAxis1"
        '
        'lblAxis0
        '
        resources.ApplyResources(Me.lblAxis0, "lblAxis0")
        Me.lblAxis0.Name = "lblAxis0"
        '
        'btnUp
        '
        resources.ApplyResources(Me.btnUp, "btnUp")
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.Image = Global.WindowsApplication1.My.Resources.Resources.I33_110
        Me.btnUp.Name = "btnUp"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'btnDown
        '
        resources.ApplyResources(Me.btnDown, "btnDown")
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.Image = Global.WindowsApplication1.My.Resources.Resources.I33_156
        Me.btnDown.Name = "btnDown"
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnRight
        '
        resources.ApplyResources(Me.btnRight, "btnRight")
        Me.btnRight.FlatAppearance.BorderSize = 0
        Me.btnRight.Image = Global.WindowsApplication1.My.Resources.Resources.I33_135
        Me.btnRight.Name = "btnRight"
        Me.btnRight.UseVisualStyleBackColor = True
        '
        'btnForward
        '
        resources.ApplyResources(Me.btnForward, "btnForward")
        Me.btnForward.FlatAppearance.BorderSize = 0
        Me.btnForward.Image = Global.WindowsApplication1.My.Resources.Resources.I33_156
        Me.btnForward.Name = "btnForward"
        Me.btnForward.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        resources.ApplyResources(Me.btnBack, "btnBack")
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.Image = Global.WindowsApplication1.My.Resources.Resources.I33_110
        Me.btnBack.Name = "btnBack"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnLeft
        '
        resources.ApplyResources(Me.btnLeft, "btnLeft")
        Me.btnLeft.FlatAppearance.BorderSize = 0
        Me.btnLeft.Image = Global.WindowsApplication1.My.Resources.Resources.I33_133
        Me.btnLeft.Name = "btnLeft"
        Me.btnLeft.UseVisualStyleBackColor = True
        '
        'ucJoyStick
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.grpJoyStick)
        Me.Name = "ucJoyStick"
        resources.ApplyResources(Me, "$this")
        Me.grpJoyStick.ResumeLayout(False)
        Me.grpJoyStick.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpJoyStick As System.Windows.Forms.GroupBox
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnRight As System.Windows.Forms.Button
    Friend WithEvents btnForward As System.Windows.Forms.Button
    Friend WithEvents btnLeft As System.Windows.Forms.Button
    Friend WithEvents lblAxis2Unit As System.Windows.Forms.Label
    Friend WithEvents lblAxis1Unit As System.Windows.Forms.Label
    Friend WithEvents lblAxis0Unit As System.Windows.Forms.Label
    Friend WithEvents lblAxis2 As System.Windows.Forms.Label
    Friend WithEvents lblAxis1 As System.Windows.Forms.Label
    Friend WithEvents lblAxis0 As System.Windows.Forms.Label
    Friend WithEvents btnSpeed As System.Windows.Forms.Button
    Friend WithEvents btnMode As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnCoordinate As System.Windows.Forms.Button
    Friend WithEvents btnGo1 As System.Windows.Forms.Button
    Friend WithEvents txtPosZ As System.Windows.Forms.TextBox
    Friend WithEvents txtPosY As System.Windows.Forms.TextBox
    Friend WithEvents txtPosX As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    'Public Sub MyDispose(ByVal dispos As Boolean)

    'End Sub

End Class

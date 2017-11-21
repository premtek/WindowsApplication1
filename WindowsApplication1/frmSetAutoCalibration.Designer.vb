<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetAutoCalibration
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetAutoCalibration))
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSceneName = New System.Windows.Forms.TextBox()
        Me.txtPitch = New System.Windows.Forms.TextBox()
        Me.txtCount = New System.Windows.Forms.TextBox()
        Me.lblScene = New System.Windows.Forms.Label()
        Me.lblPitch = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.grpValvePos = New System.Windows.Forms.GroupBox()
        Me.btnGo2 = New System.Windows.Forms.Button()
        Me.btnSetValvePos = New System.Windows.Forms.Button()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.txtTeachDispenser1X = New System.Windows.Forms.TextBox()
        Me.txtTeachDispenser1Y = New System.Windows.Forms.TextBox()
        Me.txtTeachDispenser1Z = New System.Windows.Forms.TextBox()
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.grpCCDPos = New System.Windows.Forms.GroupBox()
        Me.btnGo1 = New System.Windows.Forms.Button()
        Me.btnSetCcdPos = New System.Windows.Forms.Button()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.txtTeachCCD1X = New System.Windows.Forms.TextBox()
        Me.txtTeachCCD1Y = New System.Windows.Forms.TextBox()
        Me.txtTeachCCD1Z = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpValvePos.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpCCDPos.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Name = "Label5"
        Me.ToolTip1.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip"))
        '
        'txtSceneName
        '
        resources.ApplyResources(Me.txtSceneName, "txtSceneName")
        Me.txtSceneName.BackColor = System.Drawing.Color.White
        Me.txtSceneName.Name = "txtSceneName"
        Me.ToolTip1.SetToolTip(Me.txtSceneName, resources.GetString("txtSceneName.ToolTip"))
        '
        'txtPitch
        '
        resources.ApplyResources(Me.txtPitch, "txtPitch")
        Me.txtPitch.BackColor = System.Drawing.Color.White
        Me.txtPitch.Name = "txtPitch"
        Me.ToolTip1.SetToolTip(Me.txtPitch, resources.GetString("txtPitch.ToolTip"))
        '
        'txtCount
        '
        resources.ApplyResources(Me.txtCount, "txtCount")
        Me.txtCount.BackColor = System.Drawing.Color.White
        Me.txtCount.Name = "txtCount"
        Me.ToolTip1.SetToolTip(Me.txtCount, resources.GetString("txtCount.ToolTip"))
        '
        'lblScene
        '
        resources.ApplyResources(Me.lblScene, "lblScene")
        Me.lblScene.BackColor = System.Drawing.Color.Transparent
        Me.lblScene.Name = "lblScene"
        Me.ToolTip1.SetToolTip(Me.lblScene, resources.GetString("lblScene.ToolTip"))
        '
        'lblPitch
        '
        resources.ApplyResources(Me.lblPitch, "lblPitch")
        Me.lblPitch.BackColor = System.Drawing.Color.Transparent
        Me.lblPitch.Name = "lblPitch"
        Me.ToolTip1.SetToolTip(Me.lblPitch, resources.GetString("lblPitch.ToolTip"))
        '
        'lblCount
        '
        resources.ApplyResources(Me.lblCount, "lblCount")
        Me.lblCount.BackColor = System.Drawing.Color.Transparent
        Me.lblCount.Name = "lblCount"
        Me.ToolTip1.SetToolTip(Me.lblCount, resources.GetString("lblCount.ToolTip"))
        '
        'grpValvePos
        '
        resources.ApplyResources(Me.grpValvePos, "grpValvePos")
        Me.grpValvePos.Controls.Add(Me.btnGo2)
        Me.grpValvePos.Controls.Add(Me.btnSetValvePos)
        Me.grpValvePos.Controls.Add(Me.Label58)
        Me.grpValvePos.Controls.Add(Me.Label59)
        Me.grpValvePos.Controls.Add(Me.Label60)
        Me.grpValvePos.Controls.Add(Me.Label48)
        Me.grpValvePos.Controls.Add(Me.Label49)
        Me.grpValvePos.Controls.Add(Me.Label57)
        Me.grpValvePos.Controls.Add(Me.txtTeachDispenser1X)
        Me.grpValvePos.Controls.Add(Me.txtTeachDispenser1Y)
        Me.grpValvePos.Controls.Add(Me.txtTeachDispenser1Z)
        Me.grpValvePos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpValvePos.Name = "grpValvePos"
        Me.grpValvePos.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpValvePos, resources.GetString("grpValvePos.ToolTip"))
        '
        'btnGo2
        '
        resources.ApplyResources(Me.btnGo2, "btnGo2")
        Me.btnGo2.FlatAppearance.BorderSize = 0
        Me.btnGo2.Name = "btnGo2"
        Me.ToolTip1.SetToolTip(Me.btnGo2, resources.GetString("btnGo2.ToolTip"))
        Me.btnGo2.UseVisualStyleBackColor = True
        '
        'btnSetValvePos
        '
        resources.ApplyResources(Me.btnSetValvePos, "btnSetValvePos")
        Me.btnSetValvePos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetValvePos.FlatAppearance.BorderSize = 0
        Me.btnSetValvePos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetValvePos.Name = "btnSetValvePos"
        Me.ToolTip1.SetToolTip(Me.btnSetValvePos, resources.GetString("btnSetValvePos.ToolTip"))
        Me.btnSetValvePos.UseVisualStyleBackColor = True
        '
        'Label58
        '
        resources.ApplyResources(Me.Label58, "Label58")
        Me.Label58.Name = "Label58"
        Me.ToolTip1.SetToolTip(Me.Label58, resources.GetString("Label58.ToolTip"))
        '
        'Label59
        '
        resources.ApplyResources(Me.Label59, "Label59")
        Me.Label59.Name = "Label59"
        Me.ToolTip1.SetToolTip(Me.Label59, resources.GetString("Label59.ToolTip"))
        '
        'Label60
        '
        resources.ApplyResources(Me.Label60, "Label60")
        Me.Label60.Name = "Label60"
        Me.ToolTip1.SetToolTip(Me.Label60, resources.GetString("Label60.ToolTip"))
        '
        'Label48
        '
        resources.ApplyResources(Me.Label48, "Label48")
        Me.Label48.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label48.Name = "Label48"
        Me.ToolTip1.SetToolTip(Me.Label48, resources.GetString("Label48.ToolTip"))
        '
        'Label49
        '
        resources.ApplyResources(Me.Label49, "Label49")
        Me.Label49.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label49.Name = "Label49"
        Me.ToolTip1.SetToolTip(Me.Label49, resources.GetString("Label49.ToolTip"))
        '
        'Label57
        '
        resources.ApplyResources(Me.Label57, "Label57")
        Me.Label57.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label57.Name = "Label57"
        Me.ToolTip1.SetToolTip(Me.Label57, resources.GetString("Label57.ToolTip"))
        '
        'txtTeachDispenser1X
        '
        resources.ApplyResources(Me.txtTeachDispenser1X, "txtTeachDispenser1X")
        Me.txtTeachDispenser1X.Name = "txtTeachDispenser1X"
        Me.ToolTip1.SetToolTip(Me.txtTeachDispenser1X, resources.GetString("txtTeachDispenser1X.ToolTip"))
        '
        'txtTeachDispenser1Y
        '
        resources.ApplyResources(Me.txtTeachDispenser1Y, "txtTeachDispenser1Y")
        Me.txtTeachDispenser1Y.Name = "txtTeachDispenser1Y"
        Me.ToolTip1.SetToolTip(Me.txtTeachDispenser1Y, resources.GetString("txtTeachDispenser1Y.ToolTip"))
        '
        'txtTeachDispenser1Z
        '
        resources.ApplyResources(Me.txtTeachDispenser1Z, "txtTeachDispenser1Z")
        Me.txtTeachDispenser1Z.Name = "txtTeachDispenser1Z"
        Me.ToolTip1.SetToolTip(Me.txtTeachDispenser1Z, resources.GetString("txtTeachDispenser1Z.ToolTip"))
        '
        'UcJoyStick1
        '
        resources.ApplyResources(Me.UcJoyStick1, "UcJoyStick1")
        Me.UcJoyStick1.AXisA = -1
        Me.UcJoyStick1.AXisB = -1
        Me.UcJoyStick1.AXisC = -1
        Me.UcJoyStick1.AxisX = 0
        Me.UcJoyStick1.AxisY = 0
        Me.UcJoyStick1.AxisZ = 0
        Me.UcJoyStick1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcJoyStick1.ForeColor = System.Drawing.SystemColors.Control
        Me.UcJoyStick1.Name = "UcJoyStick1"
        Me.ToolTip1.SetToolTip(Me.UcJoyStick1, resources.GetString("UcJoyStick1.ToolTip"))
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.grpCCDPos)
        Me.GroupBox1.Controls.Add(Me.grpValvePos)
        Me.GroupBox1.Controls.Add(Me.lblCount)
        Me.GroupBox1.Controls.Add(Me.lblPitch)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblScene)
        Me.GroupBox1.Controls.Add(Me.txtSceneName)
        Me.GroupBox1.Controls.Add(Me.txtCount)
        Me.GroupBox1.Controls.Add(Me.txtPitch)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'grpCCDPos
        '
        resources.ApplyResources(Me.grpCCDPos, "grpCCDPos")
        Me.grpCCDPos.Controls.Add(Me.btnGo1)
        Me.grpCCDPos.Controls.Add(Me.btnSetCcdPos)
        Me.grpCCDPos.Controls.Add(Me.Label50)
        Me.grpCCDPos.Controls.Add(Me.Label51)
        Me.grpCCDPos.Controls.Add(Me.Label52)
        Me.grpCCDPos.Controls.Add(Me.Label53)
        Me.grpCCDPos.Controls.Add(Me.Label54)
        Me.grpCCDPos.Controls.Add(Me.Label55)
        Me.grpCCDPos.Controls.Add(Me.txtTeachCCD1X)
        Me.grpCCDPos.Controls.Add(Me.txtTeachCCD1Y)
        Me.grpCCDPos.Controls.Add(Me.txtTeachCCD1Z)
        Me.grpCCDPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpCCDPos.Name = "grpCCDPos"
        Me.grpCCDPos.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpCCDPos, resources.GetString("grpCCDPos.ToolTip"))
        '
        'btnGo1
        '
        resources.ApplyResources(Me.btnGo1, "btnGo1")
        Me.btnGo1.FlatAppearance.BorderSize = 0
        Me.btnGo1.Name = "btnGo1"
        Me.ToolTip1.SetToolTip(Me.btnGo1, resources.GetString("btnGo1.ToolTip"))
        Me.btnGo1.UseVisualStyleBackColor = True
        '
        'btnSetCcdPos
        '
        resources.ApplyResources(Me.btnSetCcdPos, "btnSetCcdPos")
        Me.btnSetCcdPos.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetCcdPos.FlatAppearance.BorderSize = 0
        Me.btnSetCcdPos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSetCcdPos.Name = "btnSetCcdPos"
        Me.ToolTip1.SetToolTip(Me.btnSetCcdPos, resources.GetString("btnSetCcdPos.ToolTip"))
        Me.btnSetCcdPos.UseVisualStyleBackColor = True
        '
        'Label50
        '
        resources.ApplyResources(Me.Label50, "Label50")
        Me.Label50.Name = "Label50"
        Me.ToolTip1.SetToolTip(Me.Label50, resources.GetString("Label50.ToolTip"))
        '
        'Label51
        '
        resources.ApplyResources(Me.Label51, "Label51")
        Me.Label51.Name = "Label51"
        Me.ToolTip1.SetToolTip(Me.Label51, resources.GetString("Label51.ToolTip"))
        '
        'Label52
        '
        resources.ApplyResources(Me.Label52, "Label52")
        Me.Label52.Name = "Label52"
        Me.ToolTip1.SetToolTip(Me.Label52, resources.GetString("Label52.ToolTip"))
        '
        'Label53
        '
        resources.ApplyResources(Me.Label53, "Label53")
        Me.Label53.Name = "Label53"
        Me.ToolTip1.SetToolTip(Me.Label53, resources.GetString("Label53.ToolTip"))
        '
        'Label54
        '
        resources.ApplyResources(Me.Label54, "Label54")
        Me.Label54.Name = "Label54"
        Me.ToolTip1.SetToolTip(Me.Label54, resources.GetString("Label54.ToolTip"))
        '
        'Label55
        '
        resources.ApplyResources(Me.Label55, "Label55")
        Me.Label55.Name = "Label55"
        Me.ToolTip1.SetToolTip(Me.Label55, resources.GetString("Label55.ToolTip"))
        '
        'txtTeachCCD1X
        '
        resources.ApplyResources(Me.txtTeachCCD1X, "txtTeachCCD1X")
        Me.txtTeachCCD1X.Name = "txtTeachCCD1X"
        Me.ToolTip1.SetToolTip(Me.txtTeachCCD1X, resources.GetString("txtTeachCCD1X.ToolTip"))
        '
        'txtTeachCCD1Y
        '
        resources.ApplyResources(Me.txtTeachCCD1Y, "txtTeachCCD1Y")
        Me.txtTeachCCD1Y.Name = "txtTeachCCD1Y"
        Me.ToolTip1.SetToolTip(Me.txtTeachCCD1Y, resources.GetString("txtTeachCCD1Y.ToolTip"))
        '
        'txtTeachCCD1Z
        '
        resources.ApplyResources(Me.txtTeachCCD1Z, "txtTeachCCD1Z")
        Me.txtTeachCCD1Z.Name = "txtTeachCCD1Z"
        Me.ToolTip1.SetToolTip(Me.txtTeachCCD1Z, resources.GetString("txtTeachCCD1Z.ToolTip"))
        '
        'btnOK
        '
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnOK.Name = "btnOK"
        Me.ToolTip1.SetToolTip(Me.btnOK, resources.GetString("btnOK.ToolTip"))
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmSetAutoCalibration
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.UcJoyStick1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetAutoCalibration"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpValvePos.ResumeLayout(False)
        Me.grpValvePos.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpCCDPos.ResumeLayout(False)
        Me.grpCCDPos.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSceneName As System.Windows.Forms.TextBox
    Friend WithEvents txtPitch As System.Windows.Forms.TextBox
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents lblScene As System.Windows.Forms.Label
    Friend WithEvents lblPitch As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents grpValvePos As System.Windows.Forms.GroupBox
    Friend WithEvents btnGo2 As System.Windows.Forms.Button
    Friend WithEvents btnSetValvePos As System.Windows.Forms.Button
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents txtTeachDispenser1X As System.Windows.Forms.TextBox
    Friend WithEvents txtTeachDispenser1Y As System.Windows.Forms.TextBox
    Friend WithEvents txtTeachDispenser1Z As System.Windows.Forms.TextBox
    Friend WithEvents UcJoyStick1 As ucJoyStick
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpCCDPos As System.Windows.Forms.GroupBox
    Friend WithEvents btnGo1 As System.Windows.Forms.Button
    Friend WithEvents btnSetCcdPos As System.Windows.Forms.Button
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents txtTeachCCD1X As System.Windows.Forms.TextBox
    Friend WithEvents txtTeachCCD1Y As System.Windows.Forms.TextBox
    Friend WithEvents txtTeachCCD1Z As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

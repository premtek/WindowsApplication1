<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetValveAirPressure
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetValveAirPressure))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.combStageNo = New System.Windows.Forms.ComboBox()
        Me.UcValveAirPressure4 = New WindowsApplication1.ucValveAirPressure()
        Me.UcValveAirPressure3 = New WindowsApplication1.ucValveAirPressure()
        Me.UcValveAirPressure2 = New WindowsApplication1.ucValveAirPressure()
        Me.UcValveAirPressure1 = New WindowsApplication1.ucValveAirPressure()
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'combStageNo
        '
        resources.ApplyResources(Me.combStageNo, "combStageNo")
        Me.combStageNo.FormattingEnabled = True
        Me.combStageNo.Name = "combStageNo"
        Me.ToolTip1.SetToolTip(Me.combStageNo, resources.GetString("combStageNo.ToolTip"))
        '
        'UcValveAirPressure4
        '
        resources.ApplyResources(Me.UcValveAirPressure4, "UcValveAirPressure4")
        Me.UcValveAirPressure4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcValveAirPressure4.Name = "UcValveAirPressure4"
        Me.ToolTip1.SetToolTip(Me.UcValveAirPressure4, resources.GetString("UcValveAirPressure4.ToolTip"))
        '
        'UcValveAirPressure3
        '
        resources.ApplyResources(Me.UcValveAirPressure3, "UcValveAirPressure3")
        Me.UcValveAirPressure3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcValveAirPressure3.Name = "UcValveAirPressure3"
        Me.ToolTip1.SetToolTip(Me.UcValveAirPressure3, resources.GetString("UcValveAirPressure3.ToolTip"))
        '
        'UcValveAirPressure2
        '
        resources.ApplyResources(Me.UcValveAirPressure2, "UcValveAirPressure2")
        Me.UcValveAirPressure2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcValveAirPressure2.Name = "UcValveAirPressure2"
        Me.ToolTip1.SetToolTip(Me.UcValveAirPressure2, resources.GetString("UcValveAirPressure2.ToolTip"))
        '
        'UcValveAirPressure1
        '
        resources.ApplyResources(Me.UcValveAirPressure1, "UcValveAirPressure1")
        Me.UcValveAirPressure1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcValveAirPressure1.Name = "UcValveAirPressure1"
        Me.ToolTip1.SetToolTip(Me.UcValveAirPressure1, resources.GetString("UcValveAirPressure1.ToolTip"))
        '
        'UcJoyStick1
        '
        resources.ApplyResources(Me.UcJoyStick1, "UcJoyStick1")
        Me.UcJoyStick1.AXisA = 0
        Me.UcJoyStick1.AXisB = 0
        Me.UcJoyStick1.AXisC = 0
        Me.UcJoyStick1.AxisX = 0
        Me.UcJoyStick1.AxisY = 0
        Me.UcJoyStick1.AxisZ = 0
        Me.UcJoyStick1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcJoyStick1.ForeColor = System.Drawing.SystemColors.Control
        Me.UcJoyStick1.Name = "UcJoyStick1"
        Me.ToolTip1.SetToolTip(Me.UcJoyStick1, resources.GetString("UcJoyStick1.ToolTip"))
        '
        'btnExit
        '
        resources.ApplyResources(Me.btnExit, "btnExit")
        Me.btnExit.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnExit.Name = "btnExit"
        Me.ToolTip1.SetToolTip(Me.btnExit, resources.GetString("btnExit.ToolTip"))
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmSetValveAirPressure
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.UcJoyStick1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.combStageNo)
        Me.Controls.Add(Me.UcValveAirPressure4)
        Me.Controls.Add(Me.UcValveAirPressure3)
        Me.Controls.Add(Me.UcValveAirPressure2)
        Me.Controls.Add(Me.UcValveAirPressure1)
        Me.Name = "frmSetValveAirPressure"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UcValveAirPressure1 As WindowsApplication1.ucValveAirPressure
    Friend WithEvents UcValveAirPressure2 As WindowsApplication1.ucValveAirPressure
    Friend WithEvents UcValveAirPressure3 As WindowsApplication1.ucValveAirPressure
    Friend WithEvents UcValveAirPressure4 As WindowsApplication1.ucValveAirPressure
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents combStageNo As System.Windows.Forms.ComboBox
    Public WithEvents UcJoyStick1 As WindowsApplication1.ucJoyStick
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

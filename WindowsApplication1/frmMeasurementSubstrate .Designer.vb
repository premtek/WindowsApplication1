<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MeasurementSubstrate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MeasurementSubstrate))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAverage = New System.Windows.Forms.TextBox()
        Me.txtMinimun = New System.Windows.Forms.TextBox()
        Me.txtMaximun = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cboHeightDevice = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnMeasure = New System.Windows.Forms.Button()
        Me.txtMeasureTimes = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.UcJoyStick1 = New WindowsApplication1.ucJoyStick()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtAverage)
        Me.GroupBox1.Controls.Add(Me.txtMinimun)
        Me.GroupBox1.Controls.Add(Me.txtMaximun)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        Me.ToolTip1.SetToolTip(Me.Label9, resources.GetString("Label9.ToolTip"))
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        Me.ToolTip1.SetToolTip(Me.Label8, resources.GetString("Label8.ToolTip"))
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        Me.ToolTip1.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip"))
        '
        'txtAverage
        '
        resources.ApplyResources(Me.txtAverage, "txtAverage")
        Me.txtAverage.Name = "txtAverage"
        Me.txtAverage.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtAverage, resources.GetString("txtAverage.ToolTip"))
        '
        'txtMinimun
        '
        resources.ApplyResources(Me.txtMinimun, "txtMinimun")
        Me.txtMinimun.Name = "txtMinimun"
        Me.txtMinimun.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtMinimun, resources.GetString("txtMinimun.ToolTip"))
        '
        'txtMaximun
        '
        resources.ApplyResources(Me.txtMaximun, "txtMaximun")
        Me.txtMaximun.Name = "txtMaximun"
        Me.txtMaximun.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtMaximun, resources.GetString("txtMaximun.ToolTip"))
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
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'GroupBox2
        '
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Controls.Add(Me.cboHeightDevice)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.btnMeasure)
        Me.GroupBox2.Controls.Add(Me.txtMeasureTimes)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox2, resources.GetString("GroupBox2.ToolTip"))
        '
        'cboHeightDevice
        '
        resources.ApplyResources(Me.cboHeightDevice, "cboHeightDevice")
        Me.cboHeightDevice.AutoCompleteCustomSource.AddRange(New String() {resources.GetString("cboHeightDevice.AutoCompleteCustomSource"), resources.GetString("cboHeightDevice.AutoCompleteCustomSource1"), resources.GetString("cboHeightDevice.AutoCompleteCustomSource2")})
        Me.cboHeightDevice.BackColor = System.Drawing.Color.White
        Me.cboHeightDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHeightDevice.FormattingEnabled = True
        Me.cboHeightDevice.Name = "cboHeightDevice"
        Me.ToolTip1.SetToolTip(Me.cboHeightDevice, resources.GetString("cboHeightDevice.ToolTip"))
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        Me.ToolTip1.SetToolTip(Me.Label4, resources.GetString("Label4.ToolTip"))
        '
        'btnMeasure
        '
        resources.ApplyResources(Me.btnMeasure, "btnMeasure")
        Me.btnMeasure.Name = "btnMeasure"
        Me.ToolTip1.SetToolTip(Me.btnMeasure, resources.GetString("btnMeasure.ToolTip"))
        Me.btnMeasure.UseVisualStyleBackColor = True
        '
        'txtMeasureTimes
        '
        resources.ApplyResources(Me.txtMeasureTimes, "txtMeasureTimes")
        Me.txtMeasureTimes.Name = "txtMeasureTimes"
        Me.ToolTip1.SetToolTip(Me.txtMeasureTimes, resources.GetString("txtMeasureTimes.ToolTip"))
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        Me.ToolTip1.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip"))
        '
        'btnBack
        '
        resources.ApplyResources(Me.btnBack, "btnBack")
        Me.btnBack.BackColor = System.Drawing.SystemColors.Control
        Me.btnBack.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnBack.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnBack.Name = "btnBack"
        Me.ToolTip1.SetToolTip(Me.btnBack, resources.GetString("btnBack.ToolTip"))
        Me.btnBack.UseVisualStyleBackColor = True
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
        Me.UcJoyStick1.Name = "UcJoyStick1"
        Me.ToolTip1.SetToolTip(Me.UcJoyStick1, resources.GetString("UcJoyStick1.ToolTip"))
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        Me.ToolTip1.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip"))
        '
        'MeasurementSubstrate
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.UcJoyStick1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label6)
        Me.Name = "MeasurementSubstrate"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnMeasure As System.Windows.Forms.Button
    Friend WithEvents txtMeasureTimes As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAverage As System.Windows.Forms.TextBox
    Friend WithEvents txtMinimun As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximun As System.Windows.Forms.TextBox
    Friend WithEvents cboHeightDevice As System.Windows.Forms.ComboBox
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents UcJoyStick1 As WindowsApplication1.ucJoyStick
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

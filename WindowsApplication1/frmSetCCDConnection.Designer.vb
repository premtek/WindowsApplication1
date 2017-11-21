<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetCCDConnection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetCCDConnection))
        Me.lblRecieveIP = New System.Windows.Forms.Label()
        Me.lblSendPort = New System.Windows.Forms.Label()
        Me.lblSendIP = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbCCDType = New System.Windows.Forms.ComboBox()
        Me.lblSelectCCD = New System.Windows.Forms.Label()
        Me.cmbCCD = New System.Windows.Forms.ComboBox()
        Me.txtSendIP = New System.Windows.Forms.TextBox()
        Me.txtSendPort = New System.Windows.Forms.TextBox()
        Me.txtRecieveIP = New System.Windows.Forms.TextBox()
        Me.txtRecievePort = New System.Windows.Forms.TextBox()
        Me.lblRecievePort = New System.Windows.Forms.Label()
        Me.palOmron = New System.Windows.Forms.Panel()
        Me.palCongex = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbVideoType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CogDisplay1 = New Cognex.VisionPro.Display.CogDisplay()
        Me.btn_Run = New System.Windows.Forms.Button()
        Me.cmbCCD_SN = New System.Windows.Forms.ComboBox()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.lblCCDNo = New System.Windows.Forms.Label()
        Me.cmbTriggerType = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.palOmron.SuspendLayout()
        Me.palCongex.SuspendLayout()
        CType(Me.CogDisplay1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblRecieveIP
        '
        resources.ApplyResources(Me.lblRecieveIP, "lblRecieveIP")
        Me.lblRecieveIP.Name = "lblRecieveIP"
        Me.ToolTip1.SetToolTip(Me.lblRecieveIP, resources.GetString("lblRecieveIP.ToolTip"))
        '
        'lblSendPort
        '
        resources.ApplyResources(Me.lblSendPort, "lblSendPort")
        Me.lblSendPort.Name = "lblSendPort"
        Me.ToolTip1.SetToolTip(Me.lblSendPort, resources.GetString("lblSendPort.ToolTip"))
        '
        'lblSendIP
        '
        resources.ApplyResources(Me.lblSendIP, "lblSendIP")
        Me.lblSendIP.Name = "lblSendIP"
        Me.ToolTip1.SetToolTip(Me.lblSendIP, resources.GetString("lblSendIP.ToolTip"))
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'cmbCCDType
        '
        resources.ApplyResources(Me.cmbCCDType, "cmbCCDType")
        Me.cmbCCDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCCDType.FormattingEnabled = True
        Me.cmbCCDType.Name = "cmbCCDType"
        Me.ToolTip1.SetToolTip(Me.cmbCCDType, resources.GetString("cmbCCDType.ToolTip"))
        '
        'lblSelectCCD
        '
        resources.ApplyResources(Me.lblSelectCCD, "lblSelectCCD")
        Me.lblSelectCCD.Name = "lblSelectCCD"
        Me.ToolTip1.SetToolTip(Me.lblSelectCCD, resources.GetString("lblSelectCCD.ToolTip"))
        '
        'cmbCCD
        '
        resources.ApplyResources(Me.cmbCCD, "cmbCCD")
        Me.cmbCCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCCD.FormattingEnabled = True
        Me.cmbCCD.Name = "cmbCCD"
        Me.ToolTip1.SetToolTip(Me.cmbCCD, resources.GetString("cmbCCD.ToolTip"))
        '
        'txtSendIP
        '
        resources.ApplyResources(Me.txtSendIP, "txtSendIP")
        Me.txtSendIP.Name = "txtSendIP"
        Me.ToolTip1.SetToolTip(Me.txtSendIP, resources.GetString("txtSendIP.ToolTip"))
        '
        'txtSendPort
        '
        resources.ApplyResources(Me.txtSendPort, "txtSendPort")
        Me.txtSendPort.Name = "txtSendPort"
        Me.ToolTip1.SetToolTip(Me.txtSendPort, resources.GetString("txtSendPort.ToolTip"))
        '
        'txtRecieveIP
        '
        resources.ApplyResources(Me.txtRecieveIP, "txtRecieveIP")
        Me.txtRecieveIP.Name = "txtRecieveIP"
        Me.ToolTip1.SetToolTip(Me.txtRecieveIP, resources.GetString("txtRecieveIP.ToolTip"))
        '
        'txtRecievePort
        '
        resources.ApplyResources(Me.txtRecievePort, "txtRecievePort")
        Me.txtRecievePort.Name = "txtRecievePort"
        Me.ToolTip1.SetToolTip(Me.txtRecievePort, resources.GetString("txtRecievePort.ToolTip"))
        '
        'lblRecievePort
        '
        resources.ApplyResources(Me.lblRecievePort, "lblRecievePort")
        Me.lblRecievePort.Name = "lblRecievePort"
        Me.ToolTip1.SetToolTip(Me.lblRecievePort, resources.GetString("lblRecievePort.ToolTip"))
        '
        'palOmron
        '
        resources.ApplyResources(Me.palOmron, "palOmron")
        Me.palOmron.Controls.Add(Me.lblSendIP)
        Me.palOmron.Controls.Add(Me.lblSendPort)
        Me.palOmron.Controls.Add(Me.lblRecieveIP)
        Me.palOmron.Controls.Add(Me.lblRecievePort)
        Me.palOmron.Controls.Add(Me.txtRecievePort)
        Me.palOmron.Controls.Add(Me.txtSendIP)
        Me.palOmron.Controls.Add(Me.txtRecieveIP)
        Me.palOmron.Controls.Add(Me.txtSendPort)
        Me.palOmron.Name = "palOmron"
        Me.ToolTip1.SetToolTip(Me.palOmron, resources.GetString("palOmron.ToolTip"))
        '
        'palCongex
        '
        resources.ApplyResources(Me.palCongex, "palCongex")
        Me.palCongex.Controls.Add(Me.Label8)
        Me.palCongex.Controls.Add(Me.Label7)
        Me.palCongex.Controls.Add(Me.cmbVideoType)
        Me.palCongex.Controls.Add(Me.Label6)
        Me.palCongex.Controls.Add(Me.CogDisplay1)
        Me.palCongex.Controls.Add(Me.btn_Run)
        Me.palCongex.Controls.Add(Me.cmbCCD_SN)
        Me.palCongex.Controls.Add(Me.lblFileName)
        Me.palCongex.Controls.Add(Me.lblCCDNo)
        Me.palCongex.Controls.Add(Me.cmbTriggerType)
        Me.palCongex.Controls.Add(Me.Label5)
        Me.palCongex.Controls.Add(Me.Label4)
        Me.palCongex.Controls.Add(Me.Label3)
        Me.palCongex.Name = "palCongex"
        Me.ToolTip1.SetToolTip(Me.palCongex, resources.GetString("palCongex.ToolTip"))
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Name = "Label8"
        Me.ToolTip1.SetToolTip(Me.Label8, resources.GetString("Label8.ToolTip"))
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        Me.ToolTip1.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip"))
        '
        'cmbVideoType
        '
        resources.ApplyResources(Me.cmbVideoType, "cmbVideoType")
        Me.cmbVideoType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVideoType.FormattingEnabled = True
        Me.cmbVideoType.Name = "cmbVideoType"
        Me.ToolTip1.SetToolTip(Me.cmbVideoType, resources.GetString("cmbVideoType.ToolTip"))
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        Me.ToolTip1.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip"))
        '
        'CogDisplay1
        '
        resources.ApplyResources(Me.CogDisplay1, "CogDisplay1")
        Me.CogDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black
        Me.CogDisplay1.ColorMapLowerRoiLimit = 0.0R
        Me.CogDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None
        Me.CogDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black
        Me.CogDisplay1.ColorMapUpperRoiLimit = 1.0R
        Me.CogDisplay1.DoubleTapZoomCycleLength = 2
        Me.CogDisplay1.DoubleTapZoomSensitivity = 2.5R
        Me.CogDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1
        Me.CogDisplay1.MouseWheelSensitivity = 1.0R
        Me.CogDisplay1.Name = "CogDisplay1"
        Me.CogDisplay1.OcxState = CType(resources.GetObject("CogDisplay1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.ToolTip1.SetToolTip(Me.CogDisplay1, resources.GetString("CogDisplay1.ToolTip"))
        '
        'btn_Run
        '
        resources.ApplyResources(Me.btn_Run, "btn_Run")
        Me.btn_Run.Name = "btn_Run"
        Me.ToolTip1.SetToolTip(Me.btn_Run, resources.GetString("btn_Run.ToolTip"))
        Me.btn_Run.UseVisualStyleBackColor = True
        '
        'cmbCCD_SN
        '
        resources.ApplyResources(Me.cmbCCD_SN, "cmbCCD_SN")
        Me.cmbCCD_SN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCCD_SN.FormattingEnabled = True
        Me.cmbCCD_SN.Name = "cmbCCD_SN"
        Me.ToolTip1.SetToolTip(Me.cmbCCD_SN, resources.GetString("cmbCCD_SN.ToolTip"))
        '
        'lblFileName
        '
        resources.ApplyResources(Me.lblFileName, "lblFileName")
        Me.lblFileName.BackColor = System.Drawing.SystemColors.Control
        Me.lblFileName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFileName.Name = "lblFileName"
        Me.ToolTip1.SetToolTip(Me.lblFileName, resources.GetString("lblFileName.ToolTip"))
        '
        'lblCCDNo
        '
        resources.ApplyResources(Me.lblCCDNo, "lblCCDNo")
        Me.lblCCDNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblCCDNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCCDNo.Name = "lblCCDNo"
        Me.ToolTip1.SetToolTip(Me.lblCCDNo, resources.GetString("lblCCDNo.ToolTip"))
        '
        'cmbTriggerType
        '
        resources.ApplyResources(Me.cmbTriggerType, "cmbTriggerType")
        Me.cmbTriggerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTriggerType.FormattingEnabled = True
        Me.cmbTriggerType.Name = "cmbTriggerType"
        Me.ToolTip1.SetToolTip(Me.cmbTriggerType, resources.GetString("cmbTriggerType.ToolTip"))
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
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Name = "btnCancel"
        Me.ToolTip1.SetToolTip(Me.btnCancel, resources.GetString("btnCancel.ToolTip"))
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.SaveExit
        Me.btnOK.Name = "btnOK"
        Me.ToolTip1.SetToolTip(Me.btnOK, resources.GetString("btnOK.ToolTip"))
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmSetCCDConnection
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.palCongex)
        Me.Controls.Add(Me.palOmron)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbCCDType)
        Me.Controls.Add(Me.lblSelectCCD)
        Me.Controls.Add(Me.cmbCCD)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetCCDConnection"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.palOmron.ResumeLayout(False)
        Me.palOmron.PerformLayout()
        Me.palCongex.ResumeLayout(False)
        CType(Me.CogDisplay1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblRecieveIP As System.Windows.Forms.Label
    Friend WithEvents lblSendPort As System.Windows.Forms.Label
    Friend WithEvents lblSendIP As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbCCDType As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelectCCD As System.Windows.Forms.Label
    Friend WithEvents cmbCCD As System.Windows.Forms.ComboBox
    Friend WithEvents txtSendIP As System.Windows.Forms.TextBox
    Friend WithEvents txtSendPort As System.Windows.Forms.TextBox
    Friend WithEvents txtRecieveIP As System.Windows.Forms.TextBox
    Friend WithEvents txtRecievePort As System.Windows.Forms.TextBox
    Friend WithEvents lblRecievePort As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents palOmron As System.Windows.Forms.Panel
    Friend WithEvents palCongex As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbTriggerType As System.Windows.Forms.ComboBox
    Friend WithEvents lblCCDNo As System.Windows.Forms.Label
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Friend WithEvents cmbCCD_SN As System.Windows.Forms.ComboBox
    Friend WithEvents btn_Run As System.Windows.Forms.Button
    Friend WithEvents CogDisplay1 As Cognex.VisionPro.Display.CogDisplay
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbVideoType As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDiag
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDiag))
        Me.btnDiagRun = New System.Windows.Forms.Button()
        Me.grpbEPValve = New System.Windows.Forms.GroupBox()
        Me.txtbAEPValve1Current = New System.Windows.Forms.TextBox()
        Me.lblAOCurrent = New System.Windows.Forms.Label()
        Me.lblAOReturn = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblAOSet = New System.Windows.Forms.Label()
        Me.txtbAEPValve1Return = New System.Windows.Forms.TextBox()
        Me.palEPValve1 = New System.Windows.Forms.Panel()
        Me.txtbAEPValve1Set = New System.Windows.Forms.TextBox()
        Me.lblEPValve1 = New System.Windows.Forms.Label()
        Me.ckbEPValve = New System.Windows.Forms.CheckBox()
        Me.MessageBox = New System.Windows.Forms.TextBox()
        Me.grpbCylinder1 = New System.Windows.Forms.GroupBox()
        Me.palChuckUp = New System.Windows.Forms.Panel()
        Me.palChuckDown = New System.Windows.Forms.Panel()
        Me.palFixCylinderOff = New System.Windows.Forms.Panel()
        Me.palStopper2Down = New System.Windows.Forms.Panel()
        Me.palFixCylinderOn = New System.Windows.Forms.Panel()
        Me.palStopper2Up = New System.Windows.Forms.Panel()
        Me.lblChuckUp = New System.Windows.Forms.Label()
        Me.lblFixCylinderOff = New System.Windows.Forms.Label()
        Me.lblChuckDown = New System.Windows.Forms.Label()
        Me.lblFixCylinderOn = New System.Windows.Forms.Label()
        Me.lblStopper2Down = New System.Windows.Forms.Label()
        Me.lblStopper2Up = New System.Windows.Forms.Label()
        Me.ckbCylinder1 = New System.Windows.Forms.CheckBox()
        Me.grpbCommunication = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.palLaserReader1 = New System.Windows.Forms.Panel()
        Me.palValveController1 = New System.Windows.Forms.Panel()
        Me.palTriggerBoard1 = New System.Windows.Forms.Panel()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.palBalance = New System.Windows.Forms.Panel()
        Me.grpbLight = New System.Windows.Forms.GroupBox()
        Me.lblLightCurrent = New System.Windows.Forms.Label()
        Me.lblLightReturn = New System.Windows.Forms.Label()
        Me.txtbLight4Return = New System.Windows.Forms.TextBox()
        Me.lblLightSet = New System.Windows.Forms.Label()
        Me.txtbLight2Return = New System.Windows.Forms.TextBox()
        Me.txtbLight3Return = New System.Windows.Forms.TextBox()
        Me.txtbLight1Return = New System.Windows.Forms.TextBox()
        Me.txtbLight4Set = New System.Windows.Forms.TextBox()
        Me.txtbLight2Set = New System.Windows.Forms.TextBox()
        Me.txtbLight3Set = New System.Windows.Forms.TextBox()
        Me.txtbLight1Set = New System.Windows.Forms.TextBox()
        Me.txtbLight4Current = New System.Windows.Forms.TextBox()
        Me.txtbLight2Current = New System.Windows.Forms.TextBox()
        Me.txtbLight3Current = New System.Windows.Forms.TextBox()
        Me.txtbLight1Current = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.palLight4 = New System.Windows.Forms.Panel()
        Me.palLight3 = New System.Windows.Forms.Panel()
        Me.palLight2 = New System.Windows.Forms.Panel()
        Me.palLight1 = New System.Windows.Forms.Panel()
        Me.ckbLight = New System.Windows.Forms.CheckBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnDeselectAll = New System.Windows.Forms.Button()
        Me.ckbVacuum = New System.Windows.Forms.CheckBox()
        Me.ckbCommunication = New System.Windows.Forms.CheckBox()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.ckbDIAlarm = New System.Windows.Forms.CheckBox()
        Me.grpbDI = New System.Windows.Forms.GroupBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.palDetectGlueSensor = New System.Windows.Forms.Panel()
        Me.palMoveInMotorAlarm = New System.Windows.Forms.Panel()
        Me.palMCHeater = New System.Windows.Forms.Panel()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.palMCMotor = New System.Windows.Forms.Panel()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.palPrevAlarm = New System.Windows.Forms.Panel()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.palLValveControllerAlarm = New System.Windows.Forms.Panel()
        Me.palEMS = New System.Windows.Forms.Panel()
        Me.palNextAlarm = New System.Windows.Forms.Panel()
        Me.palDoorClose = New System.Windows.Forms.Panel()
        Me.palCDA = New System.Windows.Forms.Panel()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.grpbVacuum = New System.Windows.Forms.GroupBox()
        Me.palChuckVacuumReady = New System.Windows.Forms.Panel()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.palPurgeVacuum1 = New System.Windows.Forms.Panel()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Panel23 = New System.Windows.Forms.Panel()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpbEPValve.SuspendLayout()
        Me.grpbCylinder1.SuspendLayout()
        Me.grpbCommunication.SuspendLayout()
        Me.grpbLight.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpbDI.SuspendLayout()
        Me.grpbVacuum.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnDiagRun
        '
        resources.ApplyResources(Me.btnDiagRun, "btnDiagRun")
        Me.btnDiagRun.BackColor = System.Drawing.SystemColors.Control
        Me.btnDiagRun.FlatAppearance.BorderSize = 0
        Me.btnDiagRun.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnDiagRun.Name = "btnDiagRun"
        Me.ToolTip1.SetToolTip(Me.btnDiagRun, resources.GetString("btnDiagRun.ToolTip"))
        Me.btnDiagRun.UseVisualStyleBackColor = False
        '
        'grpbEPValve
        '
        resources.ApplyResources(Me.grpbEPValve, "grpbEPValve")
        Me.grpbEPValve.Controls.Add(Me.txtbAEPValve1Current)
        Me.grpbEPValve.Controls.Add(Me.lblAOCurrent)
        Me.grpbEPValve.Controls.Add(Me.lblAOReturn)
        Me.grpbEPValve.Controls.Add(Me.Label9)
        Me.grpbEPValve.Controls.Add(Me.lblAOSet)
        Me.grpbEPValve.Controls.Add(Me.txtbAEPValve1Return)
        Me.grpbEPValve.Controls.Add(Me.palEPValve1)
        Me.grpbEPValve.Controls.Add(Me.txtbAEPValve1Set)
        Me.grpbEPValve.Controls.Add(Me.lblEPValve1)
        Me.grpbEPValve.Name = "grpbEPValve"
        Me.grpbEPValve.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpbEPValve, resources.GetString("grpbEPValve.ToolTip"))
        '
        'txtbAEPValve1Current
        '
        resources.ApplyResources(Me.txtbAEPValve1Current, "txtbAEPValve1Current")
        Me.txtbAEPValve1Current.Name = "txtbAEPValve1Current"
        Me.txtbAEPValve1Current.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtbAEPValve1Current, resources.GetString("txtbAEPValve1Current.ToolTip"))
        '
        'lblAOCurrent
        '
        resources.ApplyResources(Me.lblAOCurrent, "lblAOCurrent")
        Me.lblAOCurrent.Name = "lblAOCurrent"
        Me.ToolTip1.SetToolTip(Me.lblAOCurrent, resources.GetString("lblAOCurrent.ToolTip"))
        '
        'lblAOReturn
        '
        resources.ApplyResources(Me.lblAOReturn, "lblAOReturn")
        Me.lblAOReturn.Name = "lblAOReturn"
        Me.ToolTip1.SetToolTip(Me.lblAOReturn, resources.GetString("lblAOReturn.ToolTip"))
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        Me.ToolTip1.SetToolTip(Me.Label9, resources.GetString("Label9.ToolTip"))
        '
        'lblAOSet
        '
        resources.ApplyResources(Me.lblAOSet, "lblAOSet")
        Me.lblAOSet.Name = "lblAOSet"
        Me.ToolTip1.SetToolTip(Me.lblAOSet, resources.GetString("lblAOSet.ToolTip"))
        '
        'txtbAEPValve1Return
        '
        resources.ApplyResources(Me.txtbAEPValve1Return, "txtbAEPValve1Return")
        Me.txtbAEPValve1Return.Name = "txtbAEPValve1Return"
        Me.txtbAEPValve1Return.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtbAEPValve1Return, resources.GetString("txtbAEPValve1Return.ToolTip"))
        '
        'palEPValve1
        '
        resources.ApplyResources(Me.palEPValve1, "palEPValve1")
        Me.palEPValve1.Name = "palEPValve1"
        Me.ToolTip1.SetToolTip(Me.palEPValve1, resources.GetString("palEPValve1.ToolTip"))
        '
        'txtbAEPValve1Set
        '
        resources.ApplyResources(Me.txtbAEPValve1Set, "txtbAEPValve1Set")
        Me.txtbAEPValve1Set.Name = "txtbAEPValve1Set"
        Me.ToolTip1.SetToolTip(Me.txtbAEPValve1Set, resources.GetString("txtbAEPValve1Set.ToolTip"))
        '
        'lblEPValve1
        '
        resources.ApplyResources(Me.lblEPValve1, "lblEPValve1")
        Me.lblEPValve1.Name = "lblEPValve1"
        Me.ToolTip1.SetToolTip(Me.lblEPValve1, resources.GetString("lblEPValve1.ToolTip"))
        '
        'ckbEPValve
        '
        resources.ApplyResources(Me.ckbEPValve, "ckbEPValve")
        Me.ckbEPValve.Name = "ckbEPValve"
        Me.ToolTip1.SetToolTip(Me.ckbEPValve, resources.GetString("ckbEPValve.ToolTip"))
        Me.ckbEPValve.UseVisualStyleBackColor = True
        '
        'MessageBox
        '
        resources.ApplyResources(Me.MessageBox, "MessageBox")
        Me.MessageBox.Name = "MessageBox"
        Me.ToolTip1.SetToolTip(Me.MessageBox, resources.GetString("MessageBox.ToolTip"))
        '
        'grpbCylinder1
        '
        resources.ApplyResources(Me.grpbCylinder1, "grpbCylinder1")
        Me.grpbCylinder1.Controls.Add(Me.palChuckUp)
        Me.grpbCylinder1.Controls.Add(Me.palChuckDown)
        Me.grpbCylinder1.Controls.Add(Me.palFixCylinderOff)
        Me.grpbCylinder1.Controls.Add(Me.palStopper2Down)
        Me.grpbCylinder1.Controls.Add(Me.palFixCylinderOn)
        Me.grpbCylinder1.Controls.Add(Me.palStopper2Up)
        Me.grpbCylinder1.Controls.Add(Me.lblChuckUp)
        Me.grpbCylinder1.Controls.Add(Me.lblFixCylinderOff)
        Me.grpbCylinder1.Controls.Add(Me.lblChuckDown)
        Me.grpbCylinder1.Controls.Add(Me.lblFixCylinderOn)
        Me.grpbCylinder1.Controls.Add(Me.lblStopper2Down)
        Me.grpbCylinder1.Controls.Add(Me.lblStopper2Up)
        Me.grpbCylinder1.Name = "grpbCylinder1"
        Me.grpbCylinder1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpbCylinder1, resources.GetString("grpbCylinder1.ToolTip"))
        '
        'palChuckUp
        '
        resources.ApplyResources(Me.palChuckUp, "palChuckUp")
        Me.palChuckUp.Name = "palChuckUp"
        Me.ToolTip1.SetToolTip(Me.palChuckUp, resources.GetString("palChuckUp.ToolTip"))
        '
        'palChuckDown
        '
        resources.ApplyResources(Me.palChuckDown, "palChuckDown")
        Me.palChuckDown.Name = "palChuckDown"
        Me.ToolTip1.SetToolTip(Me.palChuckDown, resources.GetString("palChuckDown.ToolTip"))
        '
        'palFixCylinderOff
        '
        resources.ApplyResources(Me.palFixCylinderOff, "palFixCylinderOff")
        Me.palFixCylinderOff.Name = "palFixCylinderOff"
        Me.ToolTip1.SetToolTip(Me.palFixCylinderOff, resources.GetString("palFixCylinderOff.ToolTip"))
        '
        'palStopper2Down
        '
        resources.ApplyResources(Me.palStopper2Down, "palStopper2Down")
        Me.palStopper2Down.Name = "palStopper2Down"
        Me.ToolTip1.SetToolTip(Me.palStopper2Down, resources.GetString("palStopper2Down.ToolTip"))
        '
        'palFixCylinderOn
        '
        resources.ApplyResources(Me.palFixCylinderOn, "palFixCylinderOn")
        Me.palFixCylinderOn.Name = "palFixCylinderOn"
        Me.ToolTip1.SetToolTip(Me.palFixCylinderOn, resources.GetString("palFixCylinderOn.ToolTip"))
        '
        'palStopper2Up
        '
        resources.ApplyResources(Me.palStopper2Up, "palStopper2Up")
        Me.palStopper2Up.Name = "palStopper2Up"
        Me.ToolTip1.SetToolTip(Me.palStopper2Up, resources.GetString("palStopper2Up.ToolTip"))
        '
        'lblChuckUp
        '
        resources.ApplyResources(Me.lblChuckUp, "lblChuckUp")
        Me.lblChuckUp.Name = "lblChuckUp"
        Me.ToolTip1.SetToolTip(Me.lblChuckUp, resources.GetString("lblChuckUp.ToolTip"))
        '
        'lblFixCylinderOff
        '
        resources.ApplyResources(Me.lblFixCylinderOff, "lblFixCylinderOff")
        Me.lblFixCylinderOff.Name = "lblFixCylinderOff"
        Me.ToolTip1.SetToolTip(Me.lblFixCylinderOff, resources.GetString("lblFixCylinderOff.ToolTip"))
        '
        'lblChuckDown
        '
        resources.ApplyResources(Me.lblChuckDown, "lblChuckDown")
        Me.lblChuckDown.Name = "lblChuckDown"
        Me.ToolTip1.SetToolTip(Me.lblChuckDown, resources.GetString("lblChuckDown.ToolTip"))
        '
        'lblFixCylinderOn
        '
        resources.ApplyResources(Me.lblFixCylinderOn, "lblFixCylinderOn")
        Me.lblFixCylinderOn.Name = "lblFixCylinderOn"
        Me.ToolTip1.SetToolTip(Me.lblFixCylinderOn, resources.GetString("lblFixCylinderOn.ToolTip"))
        '
        'lblStopper2Down
        '
        resources.ApplyResources(Me.lblStopper2Down, "lblStopper2Down")
        Me.lblStopper2Down.Name = "lblStopper2Down"
        Me.ToolTip1.SetToolTip(Me.lblStopper2Down, resources.GetString("lblStopper2Down.ToolTip"))
        '
        'lblStopper2Up
        '
        resources.ApplyResources(Me.lblStopper2Up, "lblStopper2Up")
        Me.lblStopper2Up.Name = "lblStopper2Up"
        Me.ToolTip1.SetToolTip(Me.lblStopper2Up, resources.GetString("lblStopper2Up.ToolTip"))
        '
        'ckbCylinder1
        '
        resources.ApplyResources(Me.ckbCylinder1, "ckbCylinder1")
        Me.ckbCylinder1.Name = "ckbCylinder1"
        Me.ToolTip1.SetToolTip(Me.ckbCylinder1, resources.GetString("ckbCylinder1.ToolTip"))
        Me.ckbCylinder1.UseVisualStyleBackColor = True
        '
        'grpbCommunication
        '
        resources.ApplyResources(Me.grpbCommunication, "grpbCommunication")
        Me.grpbCommunication.Controls.Add(Me.Label8)
        Me.grpbCommunication.Controls.Add(Me.Label3)
        Me.grpbCommunication.Controls.Add(Me.Label72)
        Me.grpbCommunication.Controls.Add(Me.palLaserReader1)
        Me.grpbCommunication.Controls.Add(Me.palValveController1)
        Me.grpbCommunication.Controls.Add(Me.palTriggerBoard1)
        Me.grpbCommunication.Controls.Add(Me.Label71)
        Me.grpbCommunication.Controls.Add(Me.palBalance)
        Me.grpbCommunication.Name = "grpbCommunication"
        Me.grpbCommunication.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpbCommunication, resources.GetString("grpbCommunication.ToolTip"))
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        Me.ToolTip1.SetToolTip(Me.Label8, resources.GetString("Label8.ToolTip"))
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        Me.ToolTip1.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip"))
        '
        'Label72
        '
        resources.ApplyResources(Me.Label72, "Label72")
        Me.Label72.Name = "Label72"
        Me.ToolTip1.SetToolTip(Me.Label72, resources.GetString("Label72.ToolTip"))
        '
        'palLaserReader1
        '
        resources.ApplyResources(Me.palLaserReader1, "palLaserReader1")
        Me.palLaserReader1.Name = "palLaserReader1"
        Me.ToolTip1.SetToolTip(Me.palLaserReader1, resources.GetString("palLaserReader1.ToolTip"))
        '
        'palValveController1
        '
        resources.ApplyResources(Me.palValveController1, "palValveController1")
        Me.palValveController1.Name = "palValveController1"
        Me.ToolTip1.SetToolTip(Me.palValveController1, resources.GetString("palValveController1.ToolTip"))
        '
        'palTriggerBoard1
        '
        resources.ApplyResources(Me.palTriggerBoard1, "palTriggerBoard1")
        Me.palTriggerBoard1.Name = "palTriggerBoard1"
        Me.ToolTip1.SetToolTip(Me.palTriggerBoard1, resources.GetString("palTriggerBoard1.ToolTip"))
        '
        'Label71
        '
        resources.ApplyResources(Me.Label71, "Label71")
        Me.Label71.Name = "Label71"
        Me.ToolTip1.SetToolTip(Me.Label71, resources.GetString("Label71.ToolTip"))
        '
        'palBalance
        '
        resources.ApplyResources(Me.palBalance, "palBalance")
        Me.palBalance.Name = "palBalance"
        Me.ToolTip1.SetToolTip(Me.palBalance, resources.GetString("palBalance.ToolTip"))
        '
        'grpbLight
        '
        resources.ApplyResources(Me.grpbLight, "grpbLight")
        Me.grpbLight.Controls.Add(Me.lblLightCurrent)
        Me.grpbLight.Controls.Add(Me.lblLightReturn)
        Me.grpbLight.Controls.Add(Me.txtbLight4Return)
        Me.grpbLight.Controls.Add(Me.lblLightSet)
        Me.grpbLight.Controls.Add(Me.txtbLight2Return)
        Me.grpbLight.Controls.Add(Me.txtbLight3Return)
        Me.grpbLight.Controls.Add(Me.txtbLight1Return)
        Me.grpbLight.Controls.Add(Me.txtbLight4Set)
        Me.grpbLight.Controls.Add(Me.txtbLight2Set)
        Me.grpbLight.Controls.Add(Me.txtbLight3Set)
        Me.grpbLight.Controls.Add(Me.txtbLight1Set)
        Me.grpbLight.Controls.Add(Me.txtbLight4Current)
        Me.grpbLight.Controls.Add(Me.txtbLight2Current)
        Me.grpbLight.Controls.Add(Me.txtbLight3Current)
        Me.grpbLight.Controls.Add(Me.txtbLight1Current)
        Me.grpbLight.Controls.Add(Me.Label31)
        Me.grpbLight.Controls.Add(Me.Label28)
        Me.grpbLight.Controls.Add(Me.Label29)
        Me.grpbLight.Controls.Add(Me.Label7)
        Me.grpbLight.Controls.Add(Me.palLight4)
        Me.grpbLight.Controls.Add(Me.palLight3)
        Me.grpbLight.Controls.Add(Me.palLight2)
        Me.grpbLight.Controls.Add(Me.palLight1)
        Me.grpbLight.Name = "grpbLight"
        Me.grpbLight.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpbLight, resources.GetString("grpbLight.ToolTip"))
        '
        'lblLightCurrent
        '
        resources.ApplyResources(Me.lblLightCurrent, "lblLightCurrent")
        Me.lblLightCurrent.Name = "lblLightCurrent"
        Me.ToolTip1.SetToolTip(Me.lblLightCurrent, resources.GetString("lblLightCurrent.ToolTip"))
        '
        'lblLightReturn
        '
        resources.ApplyResources(Me.lblLightReturn, "lblLightReturn")
        Me.lblLightReturn.Name = "lblLightReturn"
        Me.ToolTip1.SetToolTip(Me.lblLightReturn, resources.GetString("lblLightReturn.ToolTip"))
        '
        'txtbLight4Return
        '
        resources.ApplyResources(Me.txtbLight4Return, "txtbLight4Return")
        Me.txtbLight4Return.Name = "txtbLight4Return"
        Me.txtbLight4Return.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtbLight4Return, resources.GetString("txtbLight4Return.ToolTip"))
        '
        'lblLightSet
        '
        resources.ApplyResources(Me.lblLightSet, "lblLightSet")
        Me.lblLightSet.Name = "lblLightSet"
        Me.ToolTip1.SetToolTip(Me.lblLightSet, resources.GetString("lblLightSet.ToolTip"))
        '
        'txtbLight2Return
        '
        resources.ApplyResources(Me.txtbLight2Return, "txtbLight2Return")
        Me.txtbLight2Return.Name = "txtbLight2Return"
        Me.txtbLight2Return.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtbLight2Return, resources.GetString("txtbLight2Return.ToolTip"))
        '
        'txtbLight3Return
        '
        resources.ApplyResources(Me.txtbLight3Return, "txtbLight3Return")
        Me.txtbLight3Return.Name = "txtbLight3Return"
        Me.txtbLight3Return.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtbLight3Return, resources.GetString("txtbLight3Return.ToolTip"))
        '
        'txtbLight1Return
        '
        resources.ApplyResources(Me.txtbLight1Return, "txtbLight1Return")
        Me.txtbLight1Return.Name = "txtbLight1Return"
        Me.txtbLight1Return.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtbLight1Return, resources.GetString("txtbLight1Return.ToolTip"))
        '
        'txtbLight4Set
        '
        resources.ApplyResources(Me.txtbLight4Set, "txtbLight4Set")
        Me.txtbLight4Set.Name = "txtbLight4Set"
        Me.ToolTip1.SetToolTip(Me.txtbLight4Set, resources.GetString("txtbLight4Set.ToolTip"))
        '
        'txtbLight2Set
        '
        resources.ApplyResources(Me.txtbLight2Set, "txtbLight2Set")
        Me.txtbLight2Set.Name = "txtbLight2Set"
        Me.ToolTip1.SetToolTip(Me.txtbLight2Set, resources.GetString("txtbLight2Set.ToolTip"))
        '
        'txtbLight3Set
        '
        resources.ApplyResources(Me.txtbLight3Set, "txtbLight3Set")
        Me.txtbLight3Set.Name = "txtbLight3Set"
        Me.ToolTip1.SetToolTip(Me.txtbLight3Set, resources.GetString("txtbLight3Set.ToolTip"))
        '
        'txtbLight1Set
        '
        resources.ApplyResources(Me.txtbLight1Set, "txtbLight1Set")
        Me.txtbLight1Set.Name = "txtbLight1Set"
        Me.ToolTip1.SetToolTip(Me.txtbLight1Set, resources.GetString("txtbLight1Set.ToolTip"))
        '
        'txtbLight4Current
        '
        resources.ApplyResources(Me.txtbLight4Current, "txtbLight4Current")
        Me.txtbLight4Current.Name = "txtbLight4Current"
        Me.txtbLight4Current.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtbLight4Current, resources.GetString("txtbLight4Current.ToolTip"))
        '
        'txtbLight2Current
        '
        resources.ApplyResources(Me.txtbLight2Current, "txtbLight2Current")
        Me.txtbLight2Current.Name = "txtbLight2Current"
        Me.txtbLight2Current.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtbLight2Current, resources.GetString("txtbLight2Current.ToolTip"))
        '
        'txtbLight3Current
        '
        resources.ApplyResources(Me.txtbLight3Current, "txtbLight3Current")
        Me.txtbLight3Current.Name = "txtbLight3Current"
        Me.txtbLight3Current.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtbLight3Current, resources.GetString("txtbLight3Current.ToolTip"))
        '
        'txtbLight1Current
        '
        resources.ApplyResources(Me.txtbLight1Current, "txtbLight1Current")
        Me.txtbLight1Current.Name = "txtbLight1Current"
        Me.txtbLight1Current.ReadOnly = True
        Me.ToolTip1.SetToolTip(Me.txtbLight1Current, resources.GetString("txtbLight1Current.ToolTip"))
        '
        'Label31
        '
        resources.ApplyResources(Me.Label31, "Label31")
        Me.Label31.Name = "Label31"
        Me.ToolTip1.SetToolTip(Me.Label31, resources.GetString("Label31.ToolTip"))
        '
        'Label28
        '
        resources.ApplyResources(Me.Label28, "Label28")
        Me.Label28.Name = "Label28"
        Me.ToolTip1.SetToolTip(Me.Label28, resources.GetString("Label28.ToolTip"))
        '
        'Label29
        '
        resources.ApplyResources(Me.Label29, "Label29")
        Me.Label29.Name = "Label29"
        Me.ToolTip1.SetToolTip(Me.Label29, resources.GetString("Label29.ToolTip"))
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        Me.ToolTip1.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip"))
        '
        'palLight4
        '
        resources.ApplyResources(Me.palLight4, "palLight4")
        Me.palLight4.Name = "palLight4"
        Me.ToolTip1.SetToolTip(Me.palLight4, resources.GetString("palLight4.ToolTip"))
        '
        'palLight3
        '
        resources.ApplyResources(Me.palLight3, "palLight3")
        Me.palLight3.Name = "palLight3"
        Me.ToolTip1.SetToolTip(Me.palLight3, resources.GetString("palLight3.ToolTip"))
        '
        'palLight2
        '
        resources.ApplyResources(Me.palLight2, "palLight2")
        Me.palLight2.Name = "palLight2"
        Me.ToolTip1.SetToolTip(Me.palLight2, resources.GetString("palLight2.ToolTip"))
        '
        'palLight1
        '
        resources.ApplyResources(Me.palLight1, "palLight1")
        Me.palLight1.Name = "palLight1"
        Me.ToolTip1.SetToolTip(Me.palLight1, resources.GetString("palLight1.ToolTip"))
        '
        'ckbLight
        '
        resources.ApplyResources(Me.ckbLight, "ckbLight")
        Me.ckbLight.Name = "ckbLight"
        Me.ToolTip1.SetToolTip(Me.ckbLight, resources.GetString("ckbLight.ToolTip"))
        Me.ckbLight.UseVisualStyleBackColor = True
        '
        'Label23
        '
        resources.ApplyResources(Me.Label23, "Label23")
        Me.Label23.Name = "Label23"
        Me.ToolTip1.SetToolTip(Me.Label23, resources.GetString("Label23.ToolTip"))
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        Me.ToolTip1.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip"))
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.btnDeselectAll)
        Me.GroupBox1.Controls.Add(Me.ckbVacuum)
        Me.GroupBox1.Controls.Add(Me.ckbCommunication)
        Me.GroupBox1.Controls.Add(Me.btnSelectAll)
        Me.GroupBox1.Controls.Add(Me.ckbDIAlarm)
        Me.GroupBox1.Controls.Add(Me.ckbEPValve)
        Me.GroupBox1.Controls.Add(Me.ckbCylinder1)
        Me.GroupBox1.Controls.Add(Me.ckbLight)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, resources.GetString("GroupBox1.ToolTip"))
        '
        'btnDeselectAll
        '
        resources.ApplyResources(Me.btnDeselectAll, "btnDeselectAll")
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.ToolTip1.SetToolTip(Me.btnDeselectAll, resources.GetString("btnDeselectAll.ToolTip"))
        Me.btnDeselectAll.UseVisualStyleBackColor = True
        '
        'ckbVacuum
        '
        resources.ApplyResources(Me.ckbVacuum, "ckbVacuum")
        Me.ckbVacuum.Name = "ckbVacuum"
        Me.ToolTip1.SetToolTip(Me.ckbVacuum, resources.GetString("ckbVacuum.ToolTip"))
        Me.ckbVacuum.UseVisualStyleBackColor = True
        '
        'ckbCommunication
        '
        resources.ApplyResources(Me.ckbCommunication, "ckbCommunication")
        Me.ckbCommunication.Checked = True
        Me.ckbCommunication.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckbCommunication.Name = "ckbCommunication"
        Me.ToolTip1.SetToolTip(Me.ckbCommunication, resources.GetString("ckbCommunication.ToolTip"))
        Me.ckbCommunication.UseVisualStyleBackColor = True
        '
        'btnSelectAll
        '
        resources.ApplyResources(Me.btnSelectAll, "btnSelectAll")
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.ToolTip1.SetToolTip(Me.btnSelectAll, resources.GetString("btnSelectAll.ToolTip"))
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'ckbDIAlarm
        '
        resources.ApplyResources(Me.ckbDIAlarm, "ckbDIAlarm")
        Me.ckbDIAlarm.Checked = True
        Me.ckbDIAlarm.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckbDIAlarm.Name = "ckbDIAlarm"
        Me.ToolTip1.SetToolTip(Me.ckbDIAlarm, resources.GetString("ckbDIAlarm.ToolTip"))
        Me.ckbDIAlarm.UseVisualStyleBackColor = True
        '
        'grpbDI
        '
        resources.ApplyResources(Me.grpbDI, "grpbDI")
        Me.grpbDI.Controls.Add(Me.Label61)
        Me.grpbDI.Controls.Add(Me.Label54)
        Me.grpbDI.Controls.Add(Me.Label62)
        Me.grpbDI.Controls.Add(Me.Label55)
        Me.grpbDI.Controls.Add(Me.palDetectGlueSensor)
        Me.grpbDI.Controls.Add(Me.palMoveInMotorAlarm)
        Me.grpbDI.Controls.Add(Me.palMCHeater)
        Me.grpbDI.Controls.Add(Me.Label58)
        Me.grpbDI.Controls.Add(Me.palMCMotor)
        Me.grpbDI.Controls.Add(Me.Label57)
        Me.grpbDI.Controls.Add(Me.Label50)
        Me.grpbDI.Controls.Add(Me.Label56)
        Me.grpbDI.Controls.Add(Me.Label51)
        Me.grpbDI.Controls.Add(Me.palPrevAlarm)
        Me.grpbDI.Controls.Add(Me.Label52)
        Me.grpbDI.Controls.Add(Me.palLValveControllerAlarm)
        Me.grpbDI.Controls.Add(Me.palEMS)
        Me.grpbDI.Controls.Add(Me.palNextAlarm)
        Me.grpbDI.Controls.Add(Me.palDoorClose)
        Me.grpbDI.Controls.Add(Me.palCDA)
        Me.grpbDI.Name = "grpbDI"
        Me.grpbDI.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpbDI, resources.GetString("grpbDI.ToolTip"))
        '
        'Label61
        '
        resources.ApplyResources(Me.Label61, "Label61")
        Me.Label61.Name = "Label61"
        Me.ToolTip1.SetToolTip(Me.Label61, resources.GetString("Label61.ToolTip"))
        '
        'Label54
        '
        resources.ApplyResources(Me.Label54, "Label54")
        Me.Label54.Name = "Label54"
        Me.ToolTip1.SetToolTip(Me.Label54, resources.GetString("Label54.ToolTip"))
        '
        'Label62
        '
        resources.ApplyResources(Me.Label62, "Label62")
        Me.Label62.Name = "Label62"
        Me.ToolTip1.SetToolTip(Me.Label62, resources.GetString("Label62.ToolTip"))
        '
        'Label55
        '
        resources.ApplyResources(Me.Label55, "Label55")
        Me.Label55.Name = "Label55"
        Me.ToolTip1.SetToolTip(Me.Label55, resources.GetString("Label55.ToolTip"))
        '
        'palDetectGlueSensor
        '
        resources.ApplyResources(Me.palDetectGlueSensor, "palDetectGlueSensor")
        Me.palDetectGlueSensor.Name = "palDetectGlueSensor"
        Me.ToolTip1.SetToolTip(Me.palDetectGlueSensor, resources.GetString("palDetectGlueSensor.ToolTip"))
        '
        'palMoveInMotorAlarm
        '
        resources.ApplyResources(Me.palMoveInMotorAlarm, "palMoveInMotorAlarm")
        Me.palMoveInMotorAlarm.Name = "palMoveInMotorAlarm"
        Me.ToolTip1.SetToolTip(Me.palMoveInMotorAlarm, resources.GetString("palMoveInMotorAlarm.ToolTip"))
        '
        'palMCHeater
        '
        resources.ApplyResources(Me.palMCHeater, "palMCHeater")
        Me.palMCHeater.Name = "palMCHeater"
        Me.ToolTip1.SetToolTip(Me.palMCHeater, resources.GetString("palMCHeater.ToolTip"))
        '
        'Label58
        '
        resources.ApplyResources(Me.Label58, "Label58")
        Me.Label58.Name = "Label58"
        Me.ToolTip1.SetToolTip(Me.Label58, resources.GetString("Label58.ToolTip"))
        '
        'palMCMotor
        '
        resources.ApplyResources(Me.palMCMotor, "palMCMotor")
        Me.palMCMotor.Name = "palMCMotor"
        Me.ToolTip1.SetToolTip(Me.palMCMotor, resources.GetString("palMCMotor.ToolTip"))
        '
        'Label57
        '
        resources.ApplyResources(Me.Label57, "Label57")
        Me.Label57.Name = "Label57"
        Me.ToolTip1.SetToolTip(Me.Label57, resources.GetString("Label57.ToolTip"))
        '
        'Label50
        '
        resources.ApplyResources(Me.Label50, "Label50")
        Me.Label50.Name = "Label50"
        Me.ToolTip1.SetToolTip(Me.Label50, resources.GetString("Label50.ToolTip"))
        '
        'Label56
        '
        resources.ApplyResources(Me.Label56, "Label56")
        Me.Label56.Name = "Label56"
        Me.ToolTip1.SetToolTip(Me.Label56, resources.GetString("Label56.ToolTip"))
        '
        'Label51
        '
        resources.ApplyResources(Me.Label51, "Label51")
        Me.Label51.Name = "Label51"
        Me.ToolTip1.SetToolTip(Me.Label51, resources.GetString("Label51.ToolTip"))
        '
        'palPrevAlarm
        '
        resources.ApplyResources(Me.palPrevAlarm, "palPrevAlarm")
        Me.palPrevAlarm.Name = "palPrevAlarm"
        Me.ToolTip1.SetToolTip(Me.palPrevAlarm, resources.GetString("palPrevAlarm.ToolTip"))
        '
        'Label52
        '
        resources.ApplyResources(Me.Label52, "Label52")
        Me.Label52.Name = "Label52"
        Me.ToolTip1.SetToolTip(Me.Label52, resources.GetString("Label52.ToolTip"))
        '
        'palLValveControllerAlarm
        '
        resources.ApplyResources(Me.palLValveControllerAlarm, "palLValveControllerAlarm")
        Me.palLValveControllerAlarm.Name = "palLValveControllerAlarm"
        Me.ToolTip1.SetToolTip(Me.palLValveControllerAlarm, resources.GetString("palLValveControllerAlarm.ToolTip"))
        '
        'palEMS
        '
        resources.ApplyResources(Me.palEMS, "palEMS")
        Me.palEMS.Name = "palEMS"
        Me.ToolTip1.SetToolTip(Me.palEMS, resources.GetString("palEMS.ToolTip"))
        '
        'palNextAlarm
        '
        resources.ApplyResources(Me.palNextAlarm, "palNextAlarm")
        Me.palNextAlarm.Name = "palNextAlarm"
        Me.ToolTip1.SetToolTip(Me.palNextAlarm, resources.GetString("palNextAlarm.ToolTip"))
        '
        'palDoorClose
        '
        resources.ApplyResources(Me.palDoorClose, "palDoorClose")
        Me.palDoorClose.Name = "palDoorClose"
        Me.ToolTip1.SetToolTip(Me.palDoorClose, resources.GetString("palDoorClose.ToolTip"))
        '
        'palCDA
        '
        resources.ApplyResources(Me.palCDA, "palCDA")
        Me.palCDA.Name = "palCDA"
        Me.ToolTip1.SetToolTip(Me.palCDA, resources.GetString("palCDA.ToolTip"))
        '
        'Label73
        '
        resources.ApplyResources(Me.Label73, "Label73")
        Me.Label73.Name = "Label73"
        Me.ToolTip1.SetToolTip(Me.Label73, resources.GetString("Label73.ToolTip"))
        '
        'grpbVacuum
        '
        resources.ApplyResources(Me.grpbVacuum, "grpbVacuum")
        Me.grpbVacuum.Controls.Add(Me.Label73)
        Me.grpbVacuum.Controls.Add(Me.palChuckVacuumReady)
        Me.grpbVacuum.Controls.Add(Me.Label75)
        Me.grpbVacuum.Controls.Add(Me.palPurgeVacuum1)
        Me.grpbVacuum.Name = "grpbVacuum"
        Me.grpbVacuum.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grpbVacuum, resources.GetString("grpbVacuum.ToolTip"))
        '
        'palChuckVacuumReady
        '
        resources.ApplyResources(Me.palChuckVacuumReady, "palChuckVacuumReady")
        Me.palChuckVacuumReady.Name = "palChuckVacuumReady"
        Me.ToolTip1.SetToolTip(Me.palChuckVacuumReady, resources.GetString("palChuckVacuumReady.ToolTip"))
        '
        'Label75
        '
        resources.ApplyResources(Me.Label75, "Label75")
        Me.Label75.Name = "Label75"
        Me.ToolTip1.SetToolTip(Me.Label75, resources.GetString("Label75.ToolTip"))
        '
        'palPurgeVacuum1
        '
        resources.ApplyResources(Me.palPurgeVacuum1, "palPurgeVacuum1")
        Me.palPurgeVacuum1.Name = "palPurgeVacuum1"
        Me.ToolTip1.SetToolTip(Me.palPurgeVacuum1, resources.GetString("palPurgeVacuum1.ToolTip"))
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
        'Panel23
        '
        resources.ApplyResources(Me.Panel23, "Panel23")
        Me.Panel23.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Green
        Me.Panel23.Name = "Panel23"
        Me.ToolTip1.SetToolTip(Me.Panel23, resources.GetString("Panel23.ToolTip"))
        '
        'Panel22
        '
        resources.ApplyResources(Me.Panel22, "Panel22")
        Me.Panel22.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Red
        Me.Panel22.Name = "Panel22"
        Me.ToolTip1.SetToolTip(Me.Panel22, resources.GetString("Panel22.ToolTip"))
        '
        'frmDiag
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.grpbVacuum)
        Me.Controls.Add(Me.grpbDI)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel23)
        Me.Controls.Add(Me.Panel22)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.grpbLight)
        Me.Controls.Add(Me.grpbCommunication)
        Me.Controls.Add(Me.grpbCylinder1)
        Me.Controls.Add(Me.MessageBox)
        Me.Controls.Add(Me.grpbEPValve)
        Me.Controls.Add(Me.btnDiagRun)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDiag"
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.grpbEPValve.ResumeLayout(False)
        Me.grpbEPValve.PerformLayout()
        Me.grpbCylinder1.ResumeLayout(False)
        Me.grpbCylinder1.PerformLayout()
        Me.grpbCommunication.ResumeLayout(False)
        Me.grpbCommunication.PerformLayout()
        Me.grpbLight.ResumeLayout(False)
        Me.grpbLight.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpbDI.ResumeLayout(False)
        Me.grpbDI.PerformLayout()
        Me.grpbVacuum.ResumeLayout(False)
        Me.grpbVacuum.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnDiagRun As System.Windows.Forms.Button
    Friend WithEvents btnDiag As System.Windows.Forms.Button
    Friend WithEvents grpbEPValve As System.Windows.Forms.GroupBox
    Friend WithEvents palEPValve1 As System.Windows.Forms.Panel
    Friend WithEvents txtbAEPValve1Set As System.Windows.Forms.TextBox
    Friend WithEvents lblEPValve1 As System.Windows.Forms.Label
    Friend WithEvents lblAOReturn As System.Windows.Forms.Label
    Friend WithEvents txtbAEPValve1Return As System.Windows.Forms.TextBox
    Friend WithEvents MessageBox As System.Windows.Forms.TextBox
    Friend WithEvents grpbCylinder1 As System.Windows.Forms.GroupBox
    Friend WithEvents palStopper2Up As System.Windows.Forms.Panel
    Friend WithEvents lblStopper2Up As System.Windows.Forms.Label
    Friend WithEvents grpbCommunication As System.Windows.Forms.GroupBox
    Friend WithEvents ckbEPValve As System.Windows.Forms.CheckBox
    Friend WithEvents ckbCylinder1 As System.Windows.Forms.CheckBox
    Friend WithEvents palStopper2Down As System.Windows.Forms.Panel
    Friend WithEvents lblStopper2Down As System.Windows.Forms.Label
    Friend WithEvents grpbLight As System.Windows.Forms.GroupBox
    Friend WithEvents txtbLight1Return As System.Windows.Forms.TextBox
    Friend WithEvents txtbLight1Set As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents palLight1 As System.Windows.Forms.Panel
    Friend WithEvents ckbLight As System.Windows.Forms.CheckBox
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtbLight1Current As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtbLight4Return As System.Windows.Forms.TextBox
    Friend WithEvents txtbLight2Return As System.Windows.Forms.TextBox
    Friend WithEvents txtbLight3Return As System.Windows.Forms.TextBox
    Friend WithEvents txtbLight4Set As System.Windows.Forms.TextBox
    Friend WithEvents txtbLight2Set As System.Windows.Forms.TextBox
    Friend WithEvents txtbLight3Set As System.Windows.Forms.TextBox
    Friend WithEvents txtbLight4Current As System.Windows.Forms.TextBox
    Friend WithEvents txtbLight2Current As System.Windows.Forms.TextBox
    Friend WithEvents txtbLight3Current As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents palLight4 As System.Windows.Forms.Panel
    Friend WithEvents palLight3 As System.Windows.Forms.Panel
    Friend WithEvents palLight2 As System.Windows.Forms.Panel
    Friend WithEvents txtbAEPValve1Current As System.Windows.Forms.TextBox
    Friend WithEvents lblAOCurrent As System.Windows.Forms.Label
    Friend WithEvents lblAOSet As System.Windows.Forms.Label
    Friend WithEvents palChuckUp As System.Windows.Forms.Panel
    Friend WithEvents palChuckDown As System.Windows.Forms.Panel
    Friend WithEvents lblChuckUp As System.Windows.Forms.Label
    Friend WithEvents lblChuckDown As System.Windows.Forms.Label
    Friend WithEvents ckbDIAlarm As System.Windows.Forms.CheckBox
    Friend WithEvents grpbDI As System.Windows.Forms.GroupBox
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents palEMS As System.Windows.Forms.Panel
    Friend WithEvents palDoorClose As System.Windows.Forms.Panel
    Friend WithEvents palCDA As System.Windows.Forms.Panel
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents palDetectGlueSensor As System.Windows.Forms.Panel
    Friend WithEvents palMoveInMotorAlarm As System.Windows.Forms.Panel
    Friend WithEvents palMCHeater As System.Windows.Forms.Panel
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents palMCMotor As System.Windows.Forms.Panel
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents palPrevAlarm As System.Windows.Forms.Panel
    Friend WithEvents palLValveControllerAlarm As System.Windows.Forms.Panel
    Friend WithEvents palNextAlarm As System.Windows.Forms.Panel
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents palBalance As System.Windows.Forms.Panel
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents palTriggerBoard1 As System.Windows.Forms.Panel
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents palChuckVacuumReady As System.Windows.Forms.Panel
    Friend WithEvents grpbVacuum As System.Windows.Forms.GroupBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents palPurgeVacuum1 As System.Windows.Forms.Panel
    Friend WithEvents ckbVacuum As System.Windows.Forms.CheckBox
    Friend WithEvents ckbCommunication As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents palValveController1 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents palLaserReader1 As System.Windows.Forms.Panel
    Friend WithEvents btnDeselectAll As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents palFixCylinderOff As System.Windows.Forms.Panel
    Friend WithEvents palFixCylinderOn As System.Windows.Forms.Panel
    Friend WithEvents lblFixCylinderOff As System.Windows.Forms.Label
    Friend WithEvents lblFixCylinderOn As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblLightCurrent As System.Windows.Forms.Label
    Friend WithEvents lblLightReturn As System.Windows.Forms.Label
    Friend WithEvents lblLightSet As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

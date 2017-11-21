<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOperator
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOperator))
        Me.btnStage4 = New System.Windows.Forms.Button()
        Me.btnStage3 = New System.Windows.Forms.Button()
        Me.btnStage2 = New System.Windows.Forms.Button()
        Me.btnMachineB = New System.Windows.Forms.Button()
        Me.btnStage1 = New System.Windows.Forms.Button()
        Me.grpPanel = New System.Windows.Forms.GroupBox()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnHome = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnPause = New System.Windows.Forms.Button()
        Me.btnMachineA = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.tlpSubTitle = New System.Windows.Forms.TableLayoutPanel()
        Me.cboAlarmMessage = New System.Windows.Forms.ComboBox()
        Me.tlpchildTitle = New System.Windows.Forms.TableLayoutPanel()
        Me.btnChangeStatus = New System.Windows.Forms.Button()
        Me.palGreenLight = New System.Windows.Forms.Panel()
        Me.btnMute = New System.Windows.Forms.Button()
        Me.palYellowLight = New System.Windows.Forms.Panel()
        Me.lblCIMStatus = New System.Windows.Forms.Label()
        Me.palRedLight = New System.Windows.Forms.Panel()
        Me.lblDateTime = New System.Windows.Forms.Label()
        Me.lblSystemState = New System.Windows.Forms.Label()
        Me.OFDLoadRecipe = New System.Windows.Forms.OpenFileDialog()
        Me.tmrUIViewerState = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.tabTemp = New System.Windows.Forms.TabPage()
        Me.palValve1 = New System.Windows.Forms.Panel()
        Me.palValve1TempNozzle = New System.Windows.Forms.Panel()
        Me.lblNozzleTemp1 = New System.Windows.Forms.Label()
        Me.lblNozzleUnit1 = New System.Windows.Forms.Label()
        Me.palValve1TempValveBody = New System.Windows.Forms.Panel()
        Me.lblValveBodyTemp1 = New System.Windows.Forms.Label()
        Me.lblValveBodyUnit1 = New System.Windows.Forms.Label()
        Me.palValve1TempSyringe = New System.Windows.Forms.Panel()
        Me.lblSyringeTemp1 = New System.Windows.Forms.Label()
        Me.lblSyringeUnit1 = New System.Windows.Forms.Label()
        Me.lblRead = New System.Windows.Forms.Label()
        Me.lblSet = New System.Windows.Forms.Label()
        Me.lblMachineB = New System.Windows.Forms.Label()
        Me.lblMachineA = New System.Windows.Forms.Label()
        Me.lblValve4 = New System.Windows.Forms.Label()
        Me.lblValve3 = New System.Windows.Forms.Label()
        Me.lblValve2 = New System.Windows.Forms.Label()
        Me.lblValve1 = New System.Windows.Forms.Label()
        Me.btnChuckBTurnOn = New System.Windows.Forms.Button()
        Me.btnChuckATurnOn = New System.Windows.Forms.Button()
        Me.palRedLineDoubleRight = New System.Windows.Forms.Panel()
        Me.palRedLineDoubleLeft = New System.Windows.Forms.Panel()
        Me.palValve4 = New System.Windows.Forms.Panel()
        Me.palValve4TempNozzle = New System.Windows.Forms.Panel()
        Me.lblNozzleTemp4 = New System.Windows.Forms.Label()
        Me.lblNozzleUnit4 = New System.Windows.Forms.Label()
        Me.palValve4TempValveBody = New System.Windows.Forms.Panel()
        Me.lblValveBodyTemp4 = New System.Windows.Forms.Label()
        Me.lblValveBodyUnit4 = New System.Windows.Forms.Label()
        Me.palValve4TempSyringe = New System.Windows.Forms.Panel()
        Me.lblSyringeTemp4 = New System.Windows.Forms.Label()
        Me.lblSyringeUnit4 = New System.Windows.Forms.Label()
        Me.palValve3 = New System.Windows.Forms.Panel()
        Me.palValve3TempNozzle = New System.Windows.Forms.Panel()
        Me.lblNozzleTemp3 = New System.Windows.Forms.Label()
        Me.lblNozzleUnit3 = New System.Windows.Forms.Label()
        Me.palValve3TempValveBody = New System.Windows.Forms.Panel()
        Me.lblValveBodyTemp3 = New System.Windows.Forms.Label()
        Me.lblValveBodyUnit3 = New System.Windows.Forms.Label()
        Me.palValve3TempSyringe = New System.Windows.Forms.Panel()
        Me.lblSyringeTemp3 = New System.Windows.Forms.Label()
        Me.lblSyringeUnit3 = New System.Windows.Forms.Label()
        Me.palValve2 = New System.Windows.Forms.Panel()
        Me.palValve2TempNozzle = New System.Windows.Forms.Panel()
        Me.lblNozzleTemp2 = New System.Windows.Forms.Label()
        Me.lblNozzleUnit2 = New System.Windows.Forms.Label()
        Me.palValve2TempValveBody = New System.Windows.Forms.Panel()
        Me.lblValveBodyTemp2 = New System.Windows.Forms.Label()
        Me.lblValveBodyUnit2 = New System.Windows.Forms.Label()
        Me.palValve2TempSyringe = New System.Windows.Forms.Panel()
        Me.lblSyringeTemp2 = New System.Windows.Forms.Label()
        Me.lblSyringeUnit2 = New System.Windows.Forms.Label()
        Me.UcChuckStatus2 = New WindowsApplication1.ucChuckStatus()
        Me.UcChuckStatus1 = New WindowsApplication1.ucChuckStatus()
        Me.btnMacAOpenMap = New System.Windows.Forms.Button()
        Me.btnAllMotorLoadRecipe = New System.Windows.Forms.Button()
        Me.tabGlueInfo = New System.Windows.Forms.TabPage()
        Me.btnResetPCS4 = New System.Windows.Forms.Button()
        Me.btnResetPCS3 = New System.Windows.Forms.Button()
        Me.btnResetPCS2 = New System.Windows.Forms.Button()
        Me.btnResetPCS1 = New System.Windows.Forms.Button()
        Me.btnSetGlueStartTime4 = New System.Windows.Forms.Button()
        Me.btnSetGlueStartTime3 = New System.Windows.Forms.Button()
        Me.btnSetGlueStartTime2 = New System.Windows.Forms.Button()
        Me.btnSetGlueStartTime1 = New System.Windows.Forms.Button()
        Me.UcProcessInfo1 = New WindowsApplication1.ucProcessInfo()
        Me.tabAOI = New System.Windows.Forms.TabPage()
        Me.grpAllCCD4 = New System.Windows.Forms.GroupBox()
        Me.UcDisplay4 = New ProjectAOI.ucDisplay()
        Me.grpAllCCD3 = New System.Windows.Forms.GroupBox()
        Me.UcDisplay3 = New ProjectAOI.ucDisplay()
        Me.grpAllCCD2 = New System.Windows.Forms.GroupBox()
        Me.UcDisplay2 = New ProjectAOI.ucDisplay()
        Me.grpAllCCD1 = New System.Windows.Forms.GroupBox()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.tabMAP = New System.Windows.Forms.TabPage()
        Me.cboShowData = New System.Windows.Forms.ComboBox()
        Me.lblshowData = New System.Windows.Forms.Label()
        Me.grpMapA = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel17 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel18 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblNone = New System.Windows.Forms.Label()
        Me.lblNG = New System.Windows.Forms.Label()
        Me.lblOK = New System.Windows.Forms.Label()
        Me.UcWaferMapA = New WindowsApplication1.ucWaferMap()
        Me.grpMapB = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblWhite = New System.Windows.Forms.Label()
        Me.lblRed = New System.Windows.Forms.Label()
        Me.lblGreen = New System.Windows.Forms.Label()
        Me.UcWaferMapB = New WindowsApplication1.ucWaferMap()
        Me.tabRunInfo = New System.Windows.Forms.TabPage()
        Me.grpRerun = New System.Windows.Forms.GroupBox()
        Me.btnExportMap = New System.Windows.Forms.Button()
        Me.UcDCSW800AQStatus1 = New WindowsApplication1.ucDCSW800AQStatus()
        Me.grpMapFile = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cbManualMapData = New System.Windows.Forms.CheckBox()
        Me.tbMapDataA = New System.Windows.Forms.TextBox()
        Me.grpAllMachineRunInfo = New System.Windows.Forms.GroupBox()
        Me.lblAllMachineRecipeName = New System.Windows.Forms.Label()
        Me.lblRecipeName = New System.Windows.Forms.Label()
        Me.tabControl3 = New System.Windows.Forms.TabControl()
        Me.tabWeightInfo = New System.Windows.Forms.TabPage()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lstWeightInfo = New System.Windows.Forms.ListBox()
        Me.grpPanel.SuspendLayout()
        Me.tlpSubTitle.SuspendLayout()
        Me.tlpchildTitle.SuspendLayout()
        Me.tabTemp.SuspendLayout()
        Me.palValve1.SuspendLayout()
        Me.palValve1TempNozzle.SuspendLayout()
        Me.palValve1TempValveBody.SuspendLayout()
        Me.palValve1TempSyringe.SuspendLayout()
        Me.palValve4.SuspendLayout()
        Me.palValve4TempNozzle.SuspendLayout()
        Me.palValve4TempValveBody.SuspendLayout()
        Me.palValve4TempSyringe.SuspendLayout()
        Me.palValve3.SuspendLayout()
        Me.palValve3TempNozzle.SuspendLayout()
        Me.palValve3TempValveBody.SuspendLayout()
        Me.palValve3TempSyringe.SuspendLayout()
        Me.palValve2.SuspendLayout()
        Me.palValve2TempNozzle.SuspendLayout()
        Me.palValve2TempValveBody.SuspendLayout()
        Me.palValve2TempSyringe.SuspendLayout()
        Me.tabGlueInfo.SuspendLayout()
        Me.tabAOI.SuspendLayout()
        Me.grpAllCCD4.SuspendLayout()
        Me.grpAllCCD3.SuspendLayout()
        Me.grpAllCCD2.SuspendLayout()
        Me.grpAllCCD1.SuspendLayout()
        Me.tabMAP.SuspendLayout()
        Me.grpMapA.SuspendLayout()
        Me.TableLayoutPanel17.SuspendLayout()
        Me.TableLayoutPanel18.SuspendLayout()
        Me.grpMapB.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.tabRunInfo.SuspendLayout()
        Me.grpRerun.SuspendLayout()
        Me.grpMapFile.SuspendLayout()
        Me.grpAllMachineRunInfo.SuspendLayout()
        Me.tabControl3.SuspendLayout()
        Me.tabWeightInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnStage4
        '
        Me.btnStage4.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnStage4, "btnStage4")
        Me.btnStage4.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnStage4.FlatAppearance.BorderSize = 0
        Me.btnStage4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnStage4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnStage4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnStage4.Name = "btnStage4"
        Me.btnStage4.UseVisualStyleBackColor = True
        '
        'btnStage3
        '
        Me.btnStage3.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnStage3, "btnStage3")
        Me.btnStage3.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnStage3.FlatAppearance.BorderSize = 0
        Me.btnStage3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnStage3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnStage3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnStage3.Name = "btnStage3"
        Me.btnStage3.UseVisualStyleBackColor = True
        '
        'btnStage2
        '
        Me.btnStage2.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnStage2, "btnStage2")
        Me.btnStage2.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnStage2.FlatAppearance.BorderSize = 0
        Me.btnStage2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnStage2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnStage2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnStage2.Name = "btnStage2"
        Me.btnStage2.UseVisualStyleBackColor = True
        '
        'btnMachineB
        '
        Me.btnMachineB.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnMachineB, "btnMachineB")
        Me.btnMachineB.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnMachineB.FlatAppearance.BorderSize = 0
        Me.btnMachineB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnMachineB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnMachineB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnMachineB.Name = "btnMachineB"
        Me.btnMachineB.UseVisualStyleBackColor = True
        '
        'btnStage1
        '
        Me.btnStage1.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnStage1, "btnStage1")
        Me.btnStage1.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnStage1.FlatAppearance.BorderSize = 0
        Me.btnStage1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnStage1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnStage1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnStage1.Name = "btnStage1"
        Me.btnStage1.UseVisualStyleBackColor = True
        '
        'grpPanel
        '
        Me.grpPanel.Controls.Add(Me.btnStop)
        Me.grpPanel.Controls.Add(Me.btnHome)
        Me.grpPanel.Controls.Add(Me.btnStart)
        Me.grpPanel.Controls.Add(Me.btnPause)
        Me.grpPanel.Controls.Add(Me.btnStage4)
        Me.grpPanel.Controls.Add(Me.btnStage3)
        Me.grpPanel.Controls.Add(Me.btnStage2)
        Me.grpPanel.Controls.Add(Me.btnMachineB)
        Me.grpPanel.Controls.Add(Me.btnMachineA)
        Me.grpPanel.Controls.Add(Me.btnStage1)
        Me.grpPanel.Controls.Add(Me.btnBack)
        resources.ApplyResources(Me.grpPanel, "grpPanel")
        Me.grpPanel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpPanel.Name = "grpPanel"
        Me.grpPanel.TabStop = False
        '
        'btnStop
        '
        Me.btnStop.BackColor = System.Drawing.SystemColors.Control
        Me.btnStop.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources._Stop1
        resources.ApplyResources(Me.btnStop, "btnStop")
        Me.btnStop.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnStop.FlatAppearance.BorderSize = 0
        Me.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnStop.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnStop.Name = "btnStop"
        Me.ToolTip.SetToolTip(Me.btnStop, resources.GetString("btnStop.ToolTip"))
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnHome
        '
        Me.btnHome.BackColor = System.Drawing.SystemColors.Control
        Me.btnHome.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.home1
        resources.ApplyResources(Me.btnHome, "btnHome")
        Me.btnHome.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnHome.FlatAppearance.BorderSize = 0
        Me.btnHome.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnHome.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnHome.Name = "btnHome"
        Me.ToolTip.SetToolTip(Me.btnHome, resources.GetString("btnHome.ToolTip"))
        Me.btnHome.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.BackColor = System.Drawing.SystemColors.Control
        Me.btnStart.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Run1
        resources.ApplyResources(Me.btnStart, "btnStart")
        Me.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnStart.FlatAppearance.BorderSize = 0
        Me.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnStart.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnStart.Name = "btnStart"
        Me.ToolTip.SetToolTip(Me.btnStart, resources.GetString("btnStart.ToolTip"))
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnPause
        '
        Me.btnPause.BackColor = System.Drawing.SystemColors.Control
        Me.btnPause.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Pause1
        resources.ApplyResources(Me.btnPause, "btnPause")
        Me.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnPause.FlatAppearance.BorderSize = 0
        Me.btnPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnPause.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnPause.Name = "btnPause"
        Me.ToolTip.SetToolTip(Me.btnPause, resources.GetString("btnPause.ToolTip"))
        Me.btnPause.UseVisualStyleBackColor = True
        '
        'btnMachineA
        '
        Me.btnMachineA.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnMachineA, "btnMachineA")
        Me.btnMachineA.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnMachineA.FlatAppearance.BorderSize = 0
        Me.btnMachineA.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnMachineA.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnMachineA.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnMachineA.Name = "btnMachineA"
        Me.btnMachineA.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.SystemColors.Control
        Me.btnBack.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.CancelExit
        resources.ApplyResources(Me.btnBack, "btnBack")
        Me.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnBack.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnBack.Name = "btnBack"
        Me.ToolTip.SetToolTip(Me.btnBack, resources.GetString("btnBack.ToolTip"))
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'tlpSubTitle
        '
        resources.ApplyResources(Me.tlpSubTitle, "tlpSubTitle")
        Me.tlpSubTitle.Controls.Add(Me.cboAlarmMessage, 0, 1)
        Me.tlpSubTitle.Controls.Add(Me.tlpchildTitle, 0, 0)
        Me.tlpSubTitle.Name = "tlpSubTitle"
        '
        'cboAlarmMessage
        '
        Me.cboAlarmMessage.BackColor = System.Drawing.SystemColors.ButtonHighlight
        resources.ApplyResources(Me.cboAlarmMessage, "cboAlarmMessage")
        Me.cboAlarmMessage.DropDownHeight = 200
        Me.cboAlarmMessage.ForeColor = System.Drawing.Color.Red
        Me.cboAlarmMessage.FormattingEnabled = True
        Me.cboAlarmMessage.Name = "cboAlarmMessage"
        '
        'tlpchildTitle
        '
        resources.ApplyResources(Me.tlpchildTitle, "tlpchildTitle")
        Me.tlpchildTitle.Controls.Add(Me.btnChangeStatus, 3, 0)
        Me.tlpchildTitle.Controls.Add(Me.palGreenLight, 7, 0)
        Me.tlpchildTitle.Controls.Add(Me.btnMute, 4, 0)
        Me.tlpchildTitle.Controls.Add(Me.palYellowLight, 6, 0)
        Me.tlpchildTitle.Controls.Add(Me.lblCIMStatus, 0, 0)
        Me.tlpchildTitle.Controls.Add(Me.palRedLight, 5, 0)
        Me.tlpchildTitle.Controls.Add(Me.lblDateTime, 1, 0)
        Me.tlpchildTitle.Controls.Add(Me.lblSystemState, 2, 0)
        Me.tlpchildTitle.Name = "tlpchildTitle"
        '
        'btnChangeStatus
        '
        Me.btnChangeStatus.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.btnChangeStatus, "btnChangeStatus")
        Me.btnChangeStatus.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnChangeStatus.FlatAppearance.BorderSize = 0
        Me.btnChangeStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime
        Me.btnChangeStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
        Me.btnChangeStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnChangeStatus.Name = "btnChangeStatus"
        Me.btnChangeStatus.UseVisualStyleBackColor = True
        '
        'palGreenLight
        '
        resources.ApplyResources(Me.palGreenLight, "palGreenLight")
        Me.palGreenLight.Name = "palGreenLight"
        '
        'btnMute
        '
        Me.btnMute.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.BuzzerOn
        resources.ApplyResources(Me.btnMute, "btnMute")
        Me.btnMute.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnMute.FlatAppearance.BorderSize = 0
        Me.btnMute.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnMute.Name = "btnMute"
        Me.ToolTip.SetToolTip(Me.btnMute, resources.GetString("btnMute.ToolTip"))
        Me.btnMute.UseVisualStyleBackColor = True
        '
        'palYellowLight
        '
        resources.ApplyResources(Me.palYellowLight, "palYellowLight")
        Me.palYellowLight.Name = "palYellowLight"
        '
        'lblCIMStatus
        '
        Me.lblCIMStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblCIMStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.lblCIMStatus, "lblCIMStatus")
        Me.lblCIMStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCIMStatus.Name = "lblCIMStatus"
        '
        'palRedLight
        '
        Me.palRedLight.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.li_23
        resources.ApplyResources(Me.palRedLight, "palRedLight")
        Me.palRedLight.Name = "palRedLight"
        '
        'lblDateTime
        '
        Me.lblDateTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.lblDateTime, "lblDateTime")
        Me.lblDateTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblDateTime.Name = "lblDateTime"
        '
        'lblSystemState
        '
        Me.lblSystemState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.lblSystemState, "lblSystemState")
        Me.lblSystemState.ForeColor = System.Drawing.Color.Black
        Me.lblSystemState.Name = "lblSystemState"
        '
        'OFDLoadRecipe
        '
        Me.OFDLoadRecipe.FileName = "OpenFileDialog1"
        '
        'tmrUIViewerState
        '
        Me.tmrUIViewerState.Interval = 300
        '
        'tabTemp
        '
        Me.tabTemp.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabTemp.Controls.Add(Me.palValve1)
        Me.tabTemp.Controls.Add(Me.lblRead)
        Me.tabTemp.Controls.Add(Me.lblSet)
        Me.tabTemp.Controls.Add(Me.lblMachineB)
        Me.tabTemp.Controls.Add(Me.lblMachineA)
        Me.tabTemp.Controls.Add(Me.lblValve4)
        Me.tabTemp.Controls.Add(Me.lblValve3)
        Me.tabTemp.Controls.Add(Me.lblValve2)
        Me.tabTemp.Controls.Add(Me.lblValve1)
        Me.tabTemp.Controls.Add(Me.btnChuckBTurnOn)
        Me.tabTemp.Controls.Add(Me.btnChuckATurnOn)
        Me.tabTemp.Controls.Add(Me.palRedLineDoubleRight)
        Me.tabTemp.Controls.Add(Me.palRedLineDoubleLeft)
        Me.tabTemp.Controls.Add(Me.palValve4)
        Me.tabTemp.Controls.Add(Me.palValve3)
        Me.tabTemp.Controls.Add(Me.palValve2)
        Me.tabTemp.Controls.Add(Me.UcChuckStatus2)
        Me.tabTemp.Controls.Add(Me.UcChuckStatus1)
        resources.ApplyResources(Me.tabTemp, "tabTemp")
        Me.tabTemp.Name = "tabTemp"
        Me.ToolTip.SetToolTip(Me.tabTemp, resources.GetString("tabTemp.ToolTip"))
        '
        'palValve1
        '
        Me.palValve1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.palValve1.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Valve
        resources.ApplyResources(Me.palValve1, "palValve1")
        Me.palValve1.Controls.Add(Me.palValve1TempNozzle)
        Me.palValve1.Controls.Add(Me.palValve1TempValveBody)
        Me.palValve1.Controls.Add(Me.palValve1TempSyringe)
        Me.palValve1.Name = "palValve1"
        '
        'palValve1TempNozzle
        '
        Me.palValve1TempNozzle.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.palValve1TempNozzle, "palValve1TempNozzle")
        Me.palValve1TempNozzle.Controls.Add(Me.lblNozzleTemp1)
        Me.palValve1TempNozzle.Controls.Add(Me.lblNozzleUnit1)
        Me.palValve1TempNozzle.Name = "palValve1TempNozzle"
        '
        'lblNozzleTemp1
        '
        resources.ApplyResources(Me.lblNozzleTemp1, "lblNozzleTemp1")
        Me.lblNozzleTemp1.Name = "lblNozzleTemp1"
        '
        'lblNozzleUnit1
        '
        Me.lblNozzleUnit1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblNozzleUnit1, "lblNozzleUnit1")
        Me.lblNozzleUnit1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblNozzleUnit1.Name = "lblNozzleUnit1"
        '
        'palValve1TempValveBody
        '
        Me.palValve1TempValveBody.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.palValve1TempValveBody, "palValve1TempValveBody")
        Me.palValve1TempValveBody.Controls.Add(Me.lblValveBodyTemp1)
        Me.palValve1TempValveBody.Controls.Add(Me.lblValveBodyUnit1)
        Me.palValve1TempValveBody.Name = "palValve1TempValveBody"
        '
        'lblValveBodyTemp1
        '
        resources.ApplyResources(Me.lblValveBodyTemp1, "lblValveBodyTemp1")
        Me.lblValveBodyTemp1.Name = "lblValveBodyTemp1"
        '
        'lblValveBodyUnit1
        '
        Me.lblValveBodyUnit1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblValveBodyUnit1, "lblValveBodyUnit1")
        Me.lblValveBodyUnit1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblValveBodyUnit1.Name = "lblValveBodyUnit1"
        '
        'palValve1TempSyringe
        '
        Me.palValve1TempSyringe.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.palValve1TempSyringe, "palValve1TempSyringe")
        Me.palValve1TempSyringe.Controls.Add(Me.lblSyringeTemp1)
        Me.palValve1TempSyringe.Controls.Add(Me.lblSyringeUnit1)
        Me.palValve1TempSyringe.Name = "palValve1TempSyringe"
        '
        'lblSyringeTemp1
        '
        resources.ApplyResources(Me.lblSyringeTemp1, "lblSyringeTemp1")
        Me.lblSyringeTemp1.Name = "lblSyringeTemp1"
        '
        'lblSyringeUnit1
        '
        Me.lblSyringeUnit1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblSyringeUnit1, "lblSyringeUnit1")
        Me.lblSyringeUnit1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblSyringeUnit1.Name = "lblSyringeUnit1"
        '
        'lblRead
        '
        resources.ApplyResources(Me.lblRead, "lblRead")
        Me.lblRead.Name = "lblRead"
        '
        'lblSet
        '
        resources.ApplyResources(Me.lblSet, "lblSet")
        Me.lblSet.Name = "lblSet"
        '
        'lblMachineB
        '
        resources.ApplyResources(Me.lblMachineB, "lblMachineB")
        Me.lblMachineB.Name = "lblMachineB"
        '
        'lblMachineA
        '
        resources.ApplyResources(Me.lblMachineA, "lblMachineA")
        Me.lblMachineA.Name = "lblMachineA"
        '
        'lblValve4
        '
        resources.ApplyResources(Me.lblValve4, "lblValve4")
        Me.lblValve4.Name = "lblValve4"
        '
        'lblValve3
        '
        resources.ApplyResources(Me.lblValve3, "lblValve3")
        Me.lblValve3.Name = "lblValve3"
        '
        'lblValve2
        '
        resources.ApplyResources(Me.lblValve2, "lblValve2")
        Me.lblValve2.Name = "lblValve2"
        '
        'lblValve1
        '
        resources.ApplyResources(Me.lblValve1, "lblValve1")
        Me.lblValve1.Name = "lblValve1"
        '
        'btnChuckBTurnOn
        '
        resources.ApplyResources(Me.btnChuckBTurnOn, "btnChuckBTurnOn")
        Me.btnChuckBTurnOn.Name = "btnChuckBTurnOn"
        Me.btnChuckBTurnOn.UseVisualStyleBackColor = True
        '
        'btnChuckATurnOn
        '
        resources.ApplyResources(Me.btnChuckATurnOn, "btnChuckATurnOn")
        Me.btnChuckATurnOn.Name = "btnChuckATurnOn"
        Me.btnChuckATurnOn.UseVisualStyleBackColor = True
        '
        'palRedLineDoubleRight
        '
        Me.palRedLineDoubleRight.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.RedLineDoubleRight
        resources.ApplyResources(Me.palRedLineDoubleRight, "palRedLineDoubleRight")
        Me.palRedLineDoubleRight.Name = "palRedLineDoubleRight"
        '
        'palRedLineDoubleLeft
        '
        Me.palRedLineDoubleLeft.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.RedLineDoubleLeft
        resources.ApplyResources(Me.palRedLineDoubleLeft, "palRedLineDoubleLeft")
        Me.palRedLineDoubleLeft.Name = "palRedLineDoubleLeft"
        '
        'palValve4
        '
        Me.palValve4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.palValve4.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Valve
        resources.ApplyResources(Me.palValve4, "palValve4")
        Me.palValve4.Controls.Add(Me.palValve4TempNozzle)
        Me.palValve4.Controls.Add(Me.palValve4TempValveBody)
        Me.palValve4.Controls.Add(Me.palValve4TempSyringe)
        Me.palValve4.Name = "palValve4"
        '
        'palValve4TempNozzle
        '
        Me.palValve4TempNozzle.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.palValve4TempNozzle, "palValve4TempNozzle")
        Me.palValve4TempNozzle.Controls.Add(Me.lblNozzleTemp4)
        Me.palValve4TempNozzle.Controls.Add(Me.lblNozzleUnit4)
        Me.palValve4TempNozzle.Name = "palValve4TempNozzle"
        '
        'lblNozzleTemp4
        '
        resources.ApplyResources(Me.lblNozzleTemp4, "lblNozzleTemp4")
        Me.lblNozzleTemp4.Name = "lblNozzleTemp4"
        '
        'lblNozzleUnit4
        '
        Me.lblNozzleUnit4.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblNozzleUnit4, "lblNozzleUnit4")
        Me.lblNozzleUnit4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblNozzleUnit4.Name = "lblNozzleUnit4"
        '
        'palValve4TempValveBody
        '
        Me.palValve4TempValveBody.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.palValve4TempValveBody, "palValve4TempValveBody")
        Me.palValve4TempValveBody.Controls.Add(Me.lblValveBodyTemp4)
        Me.palValve4TempValveBody.Controls.Add(Me.lblValveBodyUnit4)
        Me.palValve4TempValveBody.Name = "palValve4TempValveBody"
        '
        'lblValveBodyTemp4
        '
        resources.ApplyResources(Me.lblValveBodyTemp4, "lblValveBodyTemp4")
        Me.lblValveBodyTemp4.Name = "lblValveBodyTemp4"
        '
        'lblValveBodyUnit4
        '
        Me.lblValveBodyUnit4.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblValveBodyUnit4, "lblValveBodyUnit4")
        Me.lblValveBodyUnit4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblValveBodyUnit4.Name = "lblValveBodyUnit4"
        '
        'palValve4TempSyringe
        '
        Me.palValve4TempSyringe.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.palValve4TempSyringe, "palValve4TempSyringe")
        Me.palValve4TempSyringe.Controls.Add(Me.lblSyringeTemp4)
        Me.palValve4TempSyringe.Controls.Add(Me.lblSyringeUnit4)
        Me.palValve4TempSyringe.Name = "palValve4TempSyringe"
        '
        'lblSyringeTemp4
        '
        resources.ApplyResources(Me.lblSyringeTemp4, "lblSyringeTemp4")
        Me.lblSyringeTemp4.Name = "lblSyringeTemp4"
        '
        'lblSyringeUnit4
        '
        Me.lblSyringeUnit4.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblSyringeUnit4, "lblSyringeUnit4")
        Me.lblSyringeUnit4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblSyringeUnit4.Name = "lblSyringeUnit4"
        '
        'palValve3
        '
        Me.palValve3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.palValve3.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Valve
        resources.ApplyResources(Me.palValve3, "palValve3")
        Me.palValve3.Controls.Add(Me.palValve3TempNozzle)
        Me.palValve3.Controls.Add(Me.palValve3TempValveBody)
        Me.palValve3.Controls.Add(Me.palValve3TempSyringe)
        Me.palValve3.Name = "palValve3"
        '
        'palValve3TempNozzle
        '
        Me.palValve3TempNozzle.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.palValve3TempNozzle, "palValve3TempNozzle")
        Me.palValve3TempNozzle.Controls.Add(Me.lblNozzleTemp3)
        Me.palValve3TempNozzle.Controls.Add(Me.lblNozzleUnit3)
        Me.palValve3TempNozzle.Name = "palValve3TempNozzle"
        '
        'lblNozzleTemp3
        '
        resources.ApplyResources(Me.lblNozzleTemp3, "lblNozzleTemp3")
        Me.lblNozzleTemp3.Name = "lblNozzleTemp3"
        '
        'lblNozzleUnit3
        '
        Me.lblNozzleUnit3.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblNozzleUnit3, "lblNozzleUnit3")
        Me.lblNozzleUnit3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblNozzleUnit3.Name = "lblNozzleUnit3"
        '
        'palValve3TempValveBody
        '
        Me.palValve3TempValveBody.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.palValve3TempValveBody, "palValve3TempValveBody")
        Me.palValve3TempValveBody.Controls.Add(Me.lblValveBodyTemp3)
        Me.palValve3TempValveBody.Controls.Add(Me.lblValveBodyUnit3)
        Me.palValve3TempValveBody.Name = "palValve3TempValveBody"
        '
        'lblValveBodyTemp3
        '
        resources.ApplyResources(Me.lblValveBodyTemp3, "lblValveBodyTemp3")
        Me.lblValveBodyTemp3.Name = "lblValveBodyTemp3"
        '
        'lblValveBodyUnit3
        '
        Me.lblValveBodyUnit3.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblValveBodyUnit3, "lblValveBodyUnit3")
        Me.lblValveBodyUnit3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblValveBodyUnit3.Name = "lblValveBodyUnit3"
        '
        'palValve3TempSyringe
        '
        Me.palValve3TempSyringe.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.palValve3TempSyringe, "palValve3TempSyringe")
        Me.palValve3TempSyringe.Controls.Add(Me.lblSyringeTemp3)
        Me.palValve3TempSyringe.Controls.Add(Me.lblSyringeUnit3)
        Me.palValve3TempSyringe.Name = "palValve3TempSyringe"
        '
        'lblSyringeTemp3
        '
        resources.ApplyResources(Me.lblSyringeTemp3, "lblSyringeTemp3")
        Me.lblSyringeTemp3.Name = "lblSyringeTemp3"
        '
        'lblSyringeUnit3
        '
        Me.lblSyringeUnit3.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblSyringeUnit3, "lblSyringeUnit3")
        Me.lblSyringeUnit3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblSyringeUnit3.Name = "lblSyringeUnit3"
        '
        'palValve2
        '
        Me.palValve2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.palValve2.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Valve
        resources.ApplyResources(Me.palValve2, "palValve2")
        Me.palValve2.Controls.Add(Me.palValve2TempNozzle)
        Me.palValve2.Controls.Add(Me.palValve2TempValveBody)
        Me.palValve2.Controls.Add(Me.palValve2TempSyringe)
        Me.palValve2.Name = "palValve2"
        '
        'palValve2TempNozzle
        '
        Me.palValve2TempNozzle.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.palValve2TempNozzle, "palValve2TempNozzle")
        Me.palValve2TempNozzle.Controls.Add(Me.lblNozzleTemp2)
        Me.palValve2TempNozzle.Controls.Add(Me.lblNozzleUnit2)
        Me.palValve2TempNozzle.Name = "palValve2TempNozzle"
        '
        'lblNozzleTemp2
        '
        resources.ApplyResources(Me.lblNozzleTemp2, "lblNozzleTemp2")
        Me.lblNozzleTemp2.Name = "lblNozzleTemp2"
        '
        'lblNozzleUnit2
        '
        Me.lblNozzleUnit2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblNozzleUnit2, "lblNozzleUnit2")
        Me.lblNozzleUnit2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblNozzleUnit2.Name = "lblNozzleUnit2"
        '
        'palValve2TempValveBody
        '
        Me.palValve2TempValveBody.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.palValve2TempValveBody, "palValve2TempValveBody")
        Me.palValve2TempValveBody.Controls.Add(Me.lblValveBodyTemp2)
        Me.palValve2TempValveBody.Controls.Add(Me.lblValveBodyUnit2)
        Me.palValve2TempValveBody.Name = "palValve2TempValveBody"
        '
        'lblValveBodyTemp2
        '
        resources.ApplyResources(Me.lblValveBodyTemp2, "lblValveBodyTemp2")
        Me.lblValveBodyTemp2.Name = "lblValveBodyTemp2"
        '
        'lblValveBodyUnit2
        '
        Me.lblValveBodyUnit2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblValveBodyUnit2, "lblValveBodyUnit2")
        Me.lblValveBodyUnit2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblValveBodyUnit2.Name = "lblValveBodyUnit2"
        '
        'palValve2TempSyringe
        '
        Me.palValve2TempSyringe.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.palValve2TempSyringe, "palValve2TempSyringe")
        Me.palValve2TempSyringe.Controls.Add(Me.lblSyringeTemp2)
        Me.palValve2TempSyringe.Controls.Add(Me.lblSyringeUnit2)
        Me.palValve2TempSyringe.Name = "palValve2TempSyringe"
        '
        'lblSyringeTemp2
        '
        resources.ApplyResources(Me.lblSyringeTemp2, "lblSyringeTemp2")
        Me.lblSyringeTemp2.Name = "lblSyringeTemp2"
        '
        'lblSyringeUnit2
        '
        Me.lblSyringeUnit2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblSyringeUnit2, "lblSyringeUnit2")
        Me.lblSyringeUnit2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblSyringeUnit2.Name = "lblSyringeUnit2"
        '
        'UcChuckStatus2
        '
        Me.UcChuckStatus2.BackColor = System.Drawing.SystemColors.ButtonHighlight
        resources.ApplyResources(Me.UcChuckStatus2, "UcChuckStatus2")
        Me.UcChuckStatus2.Machine = 1
        Me.UcChuckStatus2.Name = "UcChuckStatus2"
        '
        'UcChuckStatus1
        '
        Me.UcChuckStatus1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        resources.ApplyResources(Me.UcChuckStatus1, "UcChuckStatus1")
        Me.UcChuckStatus1.Machine = 0
        Me.UcChuckStatus1.Name = "UcChuckStatus1"
        '
        'btnMacAOpenMap
        '
        Me.btnMacAOpenMap.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.OpenFile
        resources.ApplyResources(Me.btnMacAOpenMap, "btnMacAOpenMap")
        Me.btnMacAOpenMap.Name = "btnMacAOpenMap"
        Me.ToolTip.SetToolTip(Me.btnMacAOpenMap, resources.GetString("btnMacAOpenMap.ToolTip"))
        Me.btnMacAOpenMap.UseVisualStyleBackColor = True
        '
        'btnAllMotorLoadRecipe
        '
        Me.btnAllMotorLoadRecipe.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.OpenFile
        resources.ApplyResources(Me.btnAllMotorLoadRecipe, "btnAllMotorLoadRecipe")
        Me.btnAllMotorLoadRecipe.Name = "btnAllMotorLoadRecipe"
        Me.ToolTip.SetToolTip(Me.btnAllMotorLoadRecipe, resources.GetString("btnAllMotorLoadRecipe.ToolTip"))
        Me.btnAllMotorLoadRecipe.UseVisualStyleBackColor = True
        '
        'tabGlueInfo
        '
        Me.tabGlueInfo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabGlueInfo.Controls.Add(Me.btnResetPCS4)
        Me.tabGlueInfo.Controls.Add(Me.btnResetPCS3)
        Me.tabGlueInfo.Controls.Add(Me.btnResetPCS2)
        Me.tabGlueInfo.Controls.Add(Me.btnResetPCS1)
        Me.tabGlueInfo.Controls.Add(Me.btnSetGlueStartTime4)
        Me.tabGlueInfo.Controls.Add(Me.btnSetGlueStartTime3)
        Me.tabGlueInfo.Controls.Add(Me.btnSetGlueStartTime2)
        Me.tabGlueInfo.Controls.Add(Me.btnSetGlueStartTime1)
        Me.tabGlueInfo.Controls.Add(Me.UcProcessInfo1)
        resources.ApplyResources(Me.tabGlueInfo, "tabGlueInfo")
        Me.tabGlueInfo.Name = "tabGlueInfo"
        '
        'btnResetPCS4
        '
        resources.ApplyResources(Me.btnResetPCS4, "btnResetPCS4")
        Me.btnResetPCS4.Name = "btnResetPCS4"
        Me.btnResetPCS4.UseVisualStyleBackColor = True
        '
        'btnResetPCS3
        '
        resources.ApplyResources(Me.btnResetPCS3, "btnResetPCS3")
        Me.btnResetPCS3.Name = "btnResetPCS3"
        Me.btnResetPCS3.UseVisualStyleBackColor = True
        '
        'btnResetPCS2
        '
        resources.ApplyResources(Me.btnResetPCS2, "btnResetPCS2")
        Me.btnResetPCS2.Name = "btnResetPCS2"
        Me.btnResetPCS2.UseVisualStyleBackColor = True
        '
        'btnResetPCS1
        '
        resources.ApplyResources(Me.btnResetPCS1, "btnResetPCS1")
        Me.btnResetPCS1.Name = "btnResetPCS1"
        Me.btnResetPCS1.UseVisualStyleBackColor = True
        '
        'btnSetGlueStartTime4
        '
        resources.ApplyResources(Me.btnSetGlueStartTime4, "btnSetGlueStartTime4")
        Me.btnSetGlueStartTime4.Name = "btnSetGlueStartTime4"
        Me.btnSetGlueStartTime4.UseVisualStyleBackColor = True
        '
        'btnSetGlueStartTime3
        '
        resources.ApplyResources(Me.btnSetGlueStartTime3, "btnSetGlueStartTime3")
        Me.btnSetGlueStartTime3.Name = "btnSetGlueStartTime3"
        Me.btnSetGlueStartTime3.UseVisualStyleBackColor = True
        '
        'btnSetGlueStartTime2
        '
        resources.ApplyResources(Me.btnSetGlueStartTime2, "btnSetGlueStartTime2")
        Me.btnSetGlueStartTime2.Name = "btnSetGlueStartTime2"
        Me.btnSetGlueStartTime2.UseVisualStyleBackColor = True
        '
        'btnSetGlueStartTime1
        '
        resources.ApplyResources(Me.btnSetGlueStartTime1, "btnSetGlueStartTime1")
        Me.btnSetGlueStartTime1.Name = "btnSetGlueStartTime1"
        Me.btnSetGlueStartTime1.UseVisualStyleBackColor = True
        '
        'UcProcessInfo1
        '
        Me.UcProcessInfo1.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me.UcProcessInfo1, "UcProcessInfo1")
        Me.UcProcessInfo1.Name = "UcProcessInfo1"
        '
        'tabAOI
        '
        Me.tabAOI.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabAOI.Controls.Add(Me.grpAllCCD4)
        Me.tabAOI.Controls.Add(Me.grpAllCCD3)
        Me.tabAOI.Controls.Add(Me.grpAllCCD2)
        Me.tabAOI.Controls.Add(Me.grpAllCCD1)
        resources.ApplyResources(Me.tabAOI, "tabAOI")
        Me.tabAOI.Name = "tabAOI"
        '
        'grpAllCCD4
        '
        Me.grpAllCCD4.Controls.Add(Me.UcDisplay4)
        resources.ApplyResources(Me.grpAllCCD4, "grpAllCCD4")
        Me.grpAllCCD4.Name = "grpAllCCD4"
        Me.grpAllCCD4.TabStop = False
        '
        'UcDisplay4
        '
        Me.UcDisplay4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.UcDisplay4, "UcDisplay4")
        Me.UcDisplay4.Name = "UcDisplay4"
        '
        'grpAllCCD3
        '
        Me.grpAllCCD3.Controls.Add(Me.UcDisplay3)
        resources.ApplyResources(Me.grpAllCCD3, "grpAllCCD3")
        Me.grpAllCCD3.Name = "grpAllCCD3"
        Me.grpAllCCD3.TabStop = False
        '
        'UcDisplay3
        '
        Me.UcDisplay3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.UcDisplay3, "UcDisplay3")
        Me.UcDisplay3.Name = "UcDisplay3"
        '
        'grpAllCCD2
        '
        Me.grpAllCCD2.Controls.Add(Me.UcDisplay2)
        resources.ApplyResources(Me.grpAllCCD2, "grpAllCCD2")
        Me.grpAllCCD2.Name = "grpAllCCD2"
        Me.grpAllCCD2.TabStop = False
        '
        'UcDisplay2
        '
        Me.UcDisplay2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.UcDisplay2, "UcDisplay2")
        Me.UcDisplay2.Name = "UcDisplay2"
        '
        'grpAllCCD1
        '
        Me.grpAllCCD1.Controls.Add(Me.UcDisplay1)
        resources.ApplyResources(Me.grpAllCCD1, "grpAllCCD1")
        Me.grpAllCCD1.Name = "grpAllCCD1"
        Me.grpAllCCD1.TabStop = False
        '
        'UcDisplay1
        '
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.UcDisplay1, "UcDisplay1")
        Me.UcDisplay1.Name = "UcDisplay1"
        '
        'tabMAP
        '
        Me.tabMAP.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabMAP.Controls.Add(Me.cboShowData)
        Me.tabMAP.Controls.Add(Me.lblshowData)
        Me.tabMAP.Controls.Add(Me.grpMapA)
        Me.tabMAP.Controls.Add(Me.grpMapB)
        resources.ApplyResources(Me.tabMAP, "tabMAP")
        Me.tabMAP.Name = "tabMAP"
        '
        'cboShowData
        '
        Me.cboShowData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.cboShowData, "cboShowData")
        Me.cboShowData.FormattingEnabled = True
        Me.cboShowData.Items.AddRange(New Object() {resources.GetString("cboShowData.Items"), resources.GetString("cboShowData.Items1"), resources.GetString("cboShowData.Items2")})
        Me.cboShowData.Name = "cboShowData"
        '
        'lblshowData
        '
        resources.ApplyResources(Me.lblshowData, "lblshowData")
        Me.lblshowData.Name = "lblshowData"
        '
        'grpMapA
        '
        Me.grpMapA.Controls.Add(Me.TableLayoutPanel17)
        resources.ApplyResources(Me.grpMapA, "grpMapA")
        Me.grpMapA.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpMapA.Name = "grpMapA"
        Me.grpMapA.TabStop = False
        '
        'TableLayoutPanel17
        '
        resources.ApplyResources(Me.TableLayoutPanel17, "TableLayoutPanel17")
        Me.TableLayoutPanel17.Controls.Add(Me.TableLayoutPanel18, 0, 1)
        Me.TableLayoutPanel17.Controls.Add(Me.UcWaferMapA, 0, 0)
        Me.TableLayoutPanel17.Name = "TableLayoutPanel17"
        '
        'TableLayoutPanel18
        '
        resources.ApplyResources(Me.TableLayoutPanel18, "TableLayoutPanel18")
        Me.TableLayoutPanel18.Controls.Add(Me.Label2, 3, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.Label1, 4, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.lblNone, 0, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.lblNG, 2, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.lblOK, 1, 0)
        Me.TableLayoutPanel18.Name = "TableLayoutPanel18"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Red
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Name = "Label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Green
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Name = "Label1"
        '
        'lblNone
        '
        Me.lblNone.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lblNone, "lblNone")
        Me.lblNone.ForeColor = System.Drawing.Color.Black
        Me.lblNone.Name = "lblNone"
        '
        'lblNG
        '
        Me.lblNG.BackColor = System.Drawing.Color.Yellow
        resources.ApplyResources(Me.lblNG, "lblNG")
        Me.lblNG.ForeColor = System.Drawing.Color.Black
        Me.lblNG.Name = "lblNG"
        '
        'lblOK
        '
        Me.lblOK.BackColor = System.Drawing.Color.LightGray
        resources.ApplyResources(Me.lblOK, "lblOK")
        Me.lblOK.ForeColor = System.Drawing.Color.Black
        Me.lblOK.Name = "lblOK"
        '
        'UcWaferMapA
        '
        Me.UcWaferMapA.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me.UcWaferMapA, "UcWaferMapA")
        Me.UcWaferMapA.Name = "UcWaferMapA"
        Me.UcWaferMapA.Tag = "show_only"
        '
        'grpMapB
        '
        Me.grpMapB.Controls.Add(Me.TableLayoutPanel2)
        resources.ApplyResources(Me.grpMapB, "grpMapB")
        Me.grpMapB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpMapB.Name = "grpMapB"
        Me.grpMapB.TabStop = False
        '
        'TableLayoutPanel2
        '
        resources.ApplyResources(Me.TableLayoutPanel2, "TableLayoutPanel2")
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.UcWaferMapB, 0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        '
        'TableLayoutPanel3
        '
        resources.ApplyResources(Me.TableLayoutPanel3, "TableLayoutPanel3")
        Me.TableLayoutPanel3.Controls.Add(Me.Label4, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label3, 4, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lblWhite, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lblRed, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lblGreen, 1, 0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Red
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Name = "Label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Green
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Name = "Label3"
        '
        'lblWhite
        '
        Me.lblWhite.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.lblWhite, "lblWhite")
        Me.lblWhite.ForeColor = System.Drawing.Color.Black
        Me.lblWhite.Name = "lblWhite"
        '
        'lblRed
        '
        Me.lblRed.BackColor = System.Drawing.Color.Yellow
        resources.ApplyResources(Me.lblRed, "lblRed")
        Me.lblRed.ForeColor = System.Drawing.Color.Black
        Me.lblRed.Name = "lblRed"
        '
        'lblGreen
        '
        Me.lblGreen.BackColor = System.Drawing.Color.LightGray
        resources.ApplyResources(Me.lblGreen, "lblGreen")
        Me.lblGreen.ForeColor = System.Drawing.Color.Black
        Me.lblGreen.Name = "lblGreen"
        '
        'UcWaferMapB
        '
        Me.UcWaferMapB.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me.UcWaferMapB, "UcWaferMapB")
        Me.UcWaferMapB.Name = "UcWaferMapB"
        Me.UcWaferMapB.Tag = "show_only"
        '
        'tabRunInfo
        '
        Me.tabRunInfo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.tabRunInfo.Controls.Add(Me.grpRerun)
        Me.tabRunInfo.Controls.Add(Me.UcDCSW800AQStatus1)
        Me.tabRunInfo.Controls.Add(Me.grpMapFile)
        Me.tabRunInfo.Controls.Add(Me.grpAllMachineRunInfo)
        resources.ApplyResources(Me.tabRunInfo, "tabRunInfo")
        Me.tabRunInfo.Name = "tabRunInfo"
        '
        'grpRerun
        '
        Me.grpRerun.Controls.Add(Me.btnExportMap)
        resources.ApplyResources(Me.grpRerun, "grpRerun")
        Me.grpRerun.Name = "grpRerun"
        Me.grpRerun.TabStop = False
        '
        'btnExportMap
        '
        resources.ApplyResources(Me.btnExportMap, "btnExportMap")
        Me.btnExportMap.Name = "btnExportMap"
        Me.btnExportMap.UseVisualStyleBackColor = True
        '
        'UcDCSW800AQStatus1
        '
        Me.UcDCSW800AQStatus1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.UcDCSW800AQStatus1, "UcDCSW800AQStatus1")
        Me.UcDCSW800AQStatus1.Name = "UcDCSW800AQStatus1"
        '
        'grpMapFile
        '
        Me.grpMapFile.Controls.Add(Me.Label11)
        Me.grpMapFile.Controls.Add(Me.cbManualMapData)
        Me.grpMapFile.Controls.Add(Me.tbMapDataA)
        Me.grpMapFile.Controls.Add(Me.btnMacAOpenMap)
        resources.ApplyResources(Me.grpMapFile, "grpMapFile")
        Me.grpMapFile.Name = "grpMapFile"
        Me.grpMapFile.TabStop = False
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        '
        'cbManualMapData
        '
        resources.ApplyResources(Me.cbManualMapData, "cbManualMapData")
        Me.cbManualMapData.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.cbManualMapData.Name = "cbManualMapData"
        Me.cbManualMapData.UseVisualStyleBackColor = False
        '
        'tbMapDataA
        '
        resources.ApplyResources(Me.tbMapDataA, "tbMapDataA")
        Me.tbMapDataA.Name = "tbMapDataA"
        Me.tbMapDataA.ReadOnly = True
        '
        'grpAllMachineRunInfo
        '
        Me.grpAllMachineRunInfo.Controls.Add(Me.lblAllMachineRecipeName)
        Me.grpAllMachineRunInfo.Controls.Add(Me.lblRecipeName)
        Me.grpAllMachineRunInfo.Controls.Add(Me.btnAllMotorLoadRecipe)
        resources.ApplyResources(Me.grpAllMachineRunInfo, "grpAllMachineRunInfo")
        Me.grpAllMachineRunInfo.Name = "grpAllMachineRunInfo"
        Me.grpAllMachineRunInfo.TabStop = False
        '
        'lblAllMachineRecipeName
        '
        Me.lblAllMachineRecipeName.BackColor = System.Drawing.Color.Transparent
        Me.lblAllMachineRecipeName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.lblAllMachineRecipeName, "lblAllMachineRecipeName")
        Me.lblAllMachineRecipeName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblAllMachineRecipeName.Name = "lblAllMachineRecipeName"
        '
        'lblRecipeName
        '
        Me.lblRecipeName.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblRecipeName, "lblRecipeName")
        Me.lblRecipeName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblRecipeName.Name = "lblRecipeName"
        '
        'tabControl3
        '
        Me.tabControl3.Controls.Add(Me.tabRunInfo)
        Me.tabControl3.Controls.Add(Me.tabMAP)
        Me.tabControl3.Controls.Add(Me.tabAOI)
        Me.tabControl3.Controls.Add(Me.tabTemp)
        Me.tabControl3.Controls.Add(Me.tabGlueInfo)
        Me.tabControl3.Controls.Add(Me.tabWeightInfo)
        resources.ApplyResources(Me.tabControl3, "tabControl3")
        Me.tabControl3.Name = "tabControl3"
        Me.tabControl3.SelectedIndex = 0
        '
        'tabWeightInfo
        '
        Me.tabWeightInfo.Controls.Add(Me.Label9)
        Me.tabWeightInfo.Controls.Add(Me.lstWeightInfo)
        resources.ApplyResources(Me.tabWeightInfo, "tabWeightInfo")
        Me.tabWeightInfo.Name = "tabWeightInfo"
        Me.tabWeightInfo.UseVisualStyleBackColor = True
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'lstWeightInfo
        '
        Me.lstWeightInfo.FormattingEnabled = True
        resources.ApplyResources(Me.lstWeightInfo, "lstWeightInfo")
        Me.lstWeightInfo.Name = "lstWeightInfo"
        '
        'frmOperator
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ControlBox = False
        Me.Controls.Add(Me.grpPanel)
        Me.Controls.Add(Me.tlpSubTitle)
        Me.Controls.Add(Me.tabControl3)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOperator"
        Me.grpPanel.ResumeLayout(False)
        Me.tlpSubTitle.ResumeLayout(False)
        Me.tlpchildTitle.ResumeLayout(False)
        Me.tabTemp.ResumeLayout(False)
        Me.tabTemp.PerformLayout()
        Me.palValve1.ResumeLayout(False)
        Me.palValve1TempNozzle.ResumeLayout(False)
        Me.palValve1TempValveBody.ResumeLayout(False)
        Me.palValve1TempSyringe.ResumeLayout(False)
        Me.palValve4.ResumeLayout(False)
        Me.palValve4TempNozzle.ResumeLayout(False)
        Me.palValve4TempValveBody.ResumeLayout(False)
        Me.palValve4TempSyringe.ResumeLayout(False)
        Me.palValve3.ResumeLayout(False)
        Me.palValve3TempNozzle.ResumeLayout(False)
        Me.palValve3TempValveBody.ResumeLayout(False)
        Me.palValve3TempSyringe.ResumeLayout(False)
        Me.palValve2.ResumeLayout(False)
        Me.palValve2TempNozzle.ResumeLayout(False)
        Me.palValve2TempValveBody.ResumeLayout(False)
        Me.palValve2TempSyringe.ResumeLayout(False)
        Me.tabGlueInfo.ResumeLayout(False)
        Me.tabAOI.ResumeLayout(False)
        Me.grpAllCCD4.ResumeLayout(False)
        Me.grpAllCCD3.ResumeLayout(False)
        Me.grpAllCCD2.ResumeLayout(False)
        Me.grpAllCCD1.ResumeLayout(False)
        Me.tabMAP.ResumeLayout(False)
        Me.tabMAP.PerformLayout()
        Me.grpMapA.ResumeLayout(False)
        Me.TableLayoutPanel17.ResumeLayout(False)
        Me.TableLayoutPanel18.ResumeLayout(False)
        Me.grpMapB.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.tabRunInfo.ResumeLayout(False)
        Me.grpRerun.ResumeLayout(False)
        Me.grpMapFile.ResumeLayout(False)
        Me.grpMapFile.PerformLayout()
        Me.grpAllMachineRunInfo.ResumeLayout(False)
        Me.tabControl3.ResumeLayout(False)
        Me.tabWeightInfo.ResumeLayout(False)
        Me.tabWeightInfo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnHome As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnPause As System.Windows.Forms.Button
    Friend WithEvents btnStage4 As System.Windows.Forms.Button
    Friend WithEvents btnStage3 As System.Windows.Forms.Button
    Friend WithEvents btnStage2 As System.Windows.Forms.Button
    Friend WithEvents btnMachineB As System.Windows.Forms.Button
    Friend WithEvents btnStage1 As System.Windows.Forms.Button
    Friend WithEvents grpPanel As System.Windows.Forms.GroupBox
    Friend WithEvents btnMachineA As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents tlpSubTitle As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cboAlarmMessage As System.Windows.Forms.ComboBox
    Friend WithEvents tlpchildTitle As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnChangeStatus As System.Windows.Forms.Button
    Friend WithEvents palGreenLight As System.Windows.Forms.Panel
    Friend WithEvents btnMute As System.Windows.Forms.Button
    Friend WithEvents palYellowLight As System.Windows.Forms.Panel
    Friend WithEvents lblCIMStatus As System.Windows.Forms.Label
    Friend WithEvents palRedLight As System.Windows.Forms.Panel
    Friend WithEvents lblDateTime As System.Windows.Forms.Label
    Friend WithEvents lblSystemState As System.Windows.Forms.Label
    Friend WithEvents OFDLoadRecipe As System.Windows.Forms.OpenFileDialog
    Friend WithEvents tmrUIViewerState As System.Windows.Forms.Timer
    Friend WithEvents tabGlueInfo As System.Windows.Forms.TabPage
    Friend WithEvents tabTemp As System.Windows.Forms.TabPage
    Friend WithEvents btnChuckBTurnOn As System.Windows.Forms.Button
    Friend WithEvents btnChuckATurnOn As System.Windows.Forms.Button
    Friend WithEvents tabAOI As System.Windows.Forms.TabPage
    Friend WithEvents grpAllCCD4 As System.Windows.Forms.GroupBox
    Friend WithEvents UcDisplay4 As ProjectAOI.ucDisplay
    Friend WithEvents grpAllCCD3 As System.Windows.Forms.GroupBox
    Friend WithEvents UcDisplay3 As ProjectAOI.ucDisplay
    Friend WithEvents grpAllCCD2 As System.Windows.Forms.GroupBox
    Friend WithEvents UcDisplay2 As ProjectAOI.ucDisplay
    Friend WithEvents grpAllCCD1 As System.Windows.Forms.GroupBox
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents tabMAP As System.Windows.Forms.TabPage
    Friend WithEvents grpMapA As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel17 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel18 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblNone As System.Windows.Forms.Label
    Friend WithEvents lblNG As System.Windows.Forms.Label
    Friend WithEvents lblOK As System.Windows.Forms.Label
    Friend WithEvents UcWaferMapA As WindowsApplication1.ucWaferMap
    Friend WithEvents grpMapB As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblWhite As System.Windows.Forms.Label
    Friend WithEvents lblRed As System.Windows.Forms.Label
    Friend WithEvents lblGreen As System.Windows.Forms.Label
    Friend WithEvents UcWaferMapB As WindowsApplication1.ucWaferMap
    Friend WithEvents tabRunInfo As System.Windows.Forms.TabPage
    Friend WithEvents UcDCSW800AQStatus1 As WindowsApplication1.ucDCSW800AQStatus
    Friend WithEvents grpAllMachineRunInfo As System.Windows.Forms.GroupBox
    Friend WithEvents lblAllMachineRecipeName As System.Windows.Forms.Label
    Friend WithEvents lblRecipeName As System.Windows.Forms.Label
    Friend WithEvents btnAllMotorLoadRecipe As System.Windows.Forms.Button
    Friend WithEvents tabControl3 As System.Windows.Forms.TabControl
    Friend WithEvents palValve1 As System.Windows.Forms.Panel
    Friend WithEvents lblMachineB As System.Windows.Forms.Label
    Friend WithEvents lblMachineA As System.Windows.Forms.Label
    Friend WithEvents lblValve4 As System.Windows.Forms.Label
    Friend WithEvents lblValve3 As System.Windows.Forms.Label
    Friend WithEvents lblValve2 As System.Windows.Forms.Label
    Friend WithEvents lblValve1 As System.Windows.Forms.Label
    Friend WithEvents palValve4 As System.Windows.Forms.Panel
    Friend WithEvents palValve3 As System.Windows.Forms.Panel
    Friend WithEvents palValve2 As System.Windows.Forms.Panel
    Friend WithEvents palRedLineDoubleLeft As System.Windows.Forms.Panel
    Friend WithEvents UcChuckStatus2 As WindowsApplication1.ucChuckStatus
    Friend WithEvents UcChuckStatus1 As WindowsApplication1.ucChuckStatus
    Friend WithEvents lblRead As System.Windows.Forms.Label
    Friend WithEvents lblSet As System.Windows.Forms.Label
    Friend WithEvents palValve1TempSyringe As System.Windows.Forms.Panel
    Friend WithEvents palRedLineDoubleRight As System.Windows.Forms.Panel
    Friend WithEvents palValve4TempNozzle As System.Windows.Forms.Panel
    Friend WithEvents palValve4TempValveBody As System.Windows.Forms.Panel
    Friend WithEvents palValve4TempSyringe As System.Windows.Forms.Panel
    Friend WithEvents palValve3TempNozzle As System.Windows.Forms.Panel
    Friend WithEvents palValve3TempValveBody As System.Windows.Forms.Panel
    Friend WithEvents palValve3TempSyringe As System.Windows.Forms.Panel
    Friend WithEvents palValve2TempNozzle As System.Windows.Forms.Panel
    Friend WithEvents palValve2TempValveBody As System.Windows.Forms.Panel
    Friend WithEvents palValve2TempSyringe As System.Windows.Forms.Panel
    Friend WithEvents palValve1TempNozzle As System.Windows.Forms.Panel
    Friend WithEvents palValve1TempValveBody As System.Windows.Forms.Panel
    Friend WithEvents lblNozzleTemp4 As System.Windows.Forms.Label
    Friend WithEvents lblNozzleUnit4 As System.Windows.Forms.Label
    Friend WithEvents lblValveBodyTemp4 As System.Windows.Forms.Label
    Friend WithEvents lblValveBodyUnit4 As System.Windows.Forms.Label
    Friend WithEvents lblSyringeTemp4 As System.Windows.Forms.Label
    Friend WithEvents lblSyringeUnit4 As System.Windows.Forms.Label
    Friend WithEvents lblNozzleTemp3 As System.Windows.Forms.Label
    Friend WithEvents lblNozzleUnit3 As System.Windows.Forms.Label
    Friend WithEvents lblValveBodyTemp3 As System.Windows.Forms.Label
    Friend WithEvents lblValveBodyUnit3 As System.Windows.Forms.Label
    Friend WithEvents lblSyringeTemp3 As System.Windows.Forms.Label
    Friend WithEvents lblSyringeUnit3 As System.Windows.Forms.Label
    Friend WithEvents lblNozzleTemp2 As System.Windows.Forms.Label
    Friend WithEvents lblNozzleUnit2 As System.Windows.Forms.Label
    Friend WithEvents lblValveBodyTemp2 As System.Windows.Forms.Label
    Friend WithEvents lblValveBodyUnit2 As System.Windows.Forms.Label
    Friend WithEvents lblSyringeTemp2 As System.Windows.Forms.Label
    Friend WithEvents lblSyringeUnit2 As System.Windows.Forms.Label
    Friend WithEvents lblNozzleTemp1 As System.Windows.Forms.Label
    Friend WithEvents lblNozzleUnit1 As System.Windows.Forms.Label
    Friend WithEvents lblValveBodyTemp1 As System.Windows.Forms.Label
    Friend WithEvents lblValveBodyUnit1 As System.Windows.Forms.Label
    Friend WithEvents lblSyringeTemp1 As System.Windows.Forms.Label
    Friend WithEvents lblSyringeUnit1 As System.Windows.Forms.Label
    Friend WithEvents lblshowData As System.Windows.Forms.Label
    Friend WithEvents cboShowData As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tabWeightInfo As System.Windows.Forms.TabPage
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lstWeightInfo As System.Windows.Forms.ListBox
    Friend WithEvents btnResetPCS4 As System.Windows.Forms.Button
    Friend WithEvents btnResetPCS3 As System.Windows.Forms.Button
    Friend WithEvents btnResetPCS2 As System.Windows.Forms.Button
    Friend WithEvents btnResetPCS1 As System.Windows.Forms.Button
    Friend WithEvents btnSetGlueStartTime4 As System.Windows.Forms.Button
    Friend WithEvents btnSetGlueStartTime3 As System.Windows.Forms.Button
    Friend WithEvents btnSetGlueStartTime2 As System.Windows.Forms.Button
    Friend WithEvents btnSetGlueStartTime1 As System.Windows.Forms.Button
    Friend WithEvents UcProcessInfo1 As WindowsApplication1.ucProcessInfo
    Friend WithEvents grpMapFile As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cbManualMapData As System.Windows.Forms.CheckBox
    Friend WithEvents tbMapDataA As System.Windows.Forms.TextBox
    Friend WithEvents btnMacAOpenMap As System.Windows.Forms.Button
    Friend WithEvents btnExportMap As System.Windows.Forms.Button
    Friend WithEvents grpRerun As System.Windows.Forms.GroupBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAlignModule
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAlignModule))
        Me.lstScene = New System.Windows.Forms.ListBox()
        Me.txtScene = New System.Windows.Forms.TextBox()
        Me.TabControlImage = New System.Windows.Forms.TabControl()
        Me.TabPageCCD = New System.Windows.Forms.TabPage()
        Me.UcDisplay1 = New ProjectAOI.ucDisplay()
        Me.TabPageAlign = New System.Windows.Forms.TabPage()
        Me.grpBlob = New System.Windows.Forms.GroupBox()
        Me.btnBlobROI = New System.Windows.Forms.Button()
        Me.btnBlobRun = New System.Windows.Forms.Button()
        Me.nmcMinArea = New System.Windows.Forms.NumericUpDown()
        Me.nmcBlobHighThreshold = New System.Windows.Forms.NumericUpDown()
        Me.nmcBlobLowThreshold = New System.Windows.Forms.NumericUpDown()
        Me.cmbBlobPolarity = New System.Windows.Forms.ComboBox()
        Me.lblBlobMinArea = New System.Windows.Forms.Label()
        Me.lblHighThreshold = New System.Windows.Forms.Label()
        Me.lblLowThreshold = New System.Windows.Forms.Label()
        Me.lblBlobPolarity = New System.Windows.Forms.Label()
        Me.CogDisplay1 = New Cognex.VisionPro.Display.CogDisplay()
        Me.grpPatternArea = New System.Windows.Forms.GroupBox()
        Me.txtTrainMsg = New System.Windows.Forms.TextBox()
        Me.lblTrainMsg = New System.Windows.Forms.Label()
        Me.grpParamter = New System.Windows.Forms.GroupBox()
        Me.btnPMAlignRun = New System.Windows.Forms.Button()
        Me.nmcScale = New System.Windows.Forms.NumericUpDown()
        Me.lblScale = New System.Windows.Forms.Label()
        Me.btnTrain = New System.Windows.Forms.Button()
        Me.lblRotationUnit = New System.Windows.Forms.Label()
        Me.nmcRotation = New System.Windows.Forms.NumericUpDown()
        Me.lblRotation = New System.Windows.Forms.Label()
        Me.lblMatchRate = New System.Windows.Forms.Label()
        Me.lblMatchRateUnit = New System.Windows.Forms.Label()
        Me.nmcMatchRate = New System.Windows.Forms.NumericUpDown()
        Me.nmcPatternAreaH = New System.Windows.Forms.NumericUpDown()
        Me.nmcPatternAreaW = New System.Windows.Forms.NumericUpDown()
        Me.btnAlignROI = New System.Windows.Forms.Button()
        Me.btnImageMask = New System.Windows.Forms.Button()
        Me.btnGrabTrainImage = New System.Windows.Forms.Button()
        Me.lblPatternAreaWUnit = New System.Windows.Forms.Label()
        Me.btnShowTrain = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblPatternAreaHUnit = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grpCaliper = New System.Windows.Forms.GroupBox()
        Me.grpRadiusTolerance = New System.Windows.Forms.GroupBox()
        Me.lblRadiusTolerance = New System.Windows.Forms.Label()
        Me.lblRadiusUnit = New System.Windows.Forms.Label()
        Me.nmcRadiusTolerance = New System.Windows.Forms.NumericUpDown()
        Me.nmcProjectionLength = New System.Windows.Forms.NumericUpDown()
        Me.lblProjectionLength = New System.Windows.Forms.Label()
        Me.btnCaliperRun = New System.Windows.Forms.Button()
        Me.btnShowCalipers = New System.Windows.Forms.Button()
        Me.nmclblFilterHalfSizePixels = New System.Windows.Forms.NumericUpDown()
        Me.lblFilterHalfSizePixels = New System.Windows.Forms.Label()
        Me.nmcContrastThreshold = New System.Windows.Forms.NumericUpDown()
        Me.lblContrastThreshold = New System.Windows.Forms.Label()
        Me.cmbCaplipersPolarity = New System.Windows.Forms.ComboBox()
        Me.lblPolarity = New System.Windows.Forms.Label()
        Me.lblCalipersIgnoreNumber = New System.Windows.Forms.Label()
        Me.nmcCalipersIgnoreNumber = New System.Windows.Forms.NumericUpDown()
        Me.cmbCaplipersSearchDirection = New System.Windows.Forms.ComboBox()
        Me.lblSearchDirection = New System.Windows.Forms.Label()
        Me.nmcSearchLength = New System.Windows.Forms.NumericUpDown()
        Me.lblSearchLength = New System.Windows.Forms.Label()
        Me.lblCalipersNumber = New System.Windows.Forms.Label()
        Me.nmcCalipersNumber = New System.Windows.Forms.NumericUpDown()
        Me.grpThreshold = New System.Windows.Forms.GroupBox()
        Me.nmcOutputDownThreshold = New System.Windows.Forms.NumericUpDown()
        Me.nmcOutputUpThreshold = New System.Windows.Forms.NumericUpDown()
        Me.lblOutput = New System.Windows.Forms.Label()
        Me.lblInput = New System.Windows.Forms.Label()
        Me.nmcInputUpThreshold = New System.Windows.Forms.NumericUpDown()
        Me.btnThresholdMore = New System.Windows.Forms.Button()
        Me.nmcInputDownThreshold = New System.Windows.Forms.NumericUpDown()
        Me.lblLowerlimit = New System.Windows.Forms.Label()
        Me.lblUpperlimit = New System.Windows.Forms.Label()
        Me.grpSelectScene = New System.Windows.Forms.GroupBox()
        Me.btnSceneAdd = New System.Windows.Forms.Button()
        Me.btnSceneDel = New System.Windows.Forms.Button()
        Me.grpCalibScene = New System.Windows.Forms.GroupBox()
        Me.btnLoadScene = New System.Windows.Forms.Button()
        Me.txtCalibScene = New System.Windows.Forms.TextBox()
        Me.grpSetLight = New System.Windows.Forms.GroupBox()
        Me.UcLightControl1 = New WindowsApplication1.ucLightControl()
        Me.grpImageSource = New System.Windows.Forms.GroupBox()
        Me.btnACQ = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.lblFolder = New System.Windows.Forms.Label()
        Me.hscFile = New System.Windows.Forms.HScrollBar()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.lblTrainTool = New System.Windows.Forms.Label()
        Me.grpText = New System.Windows.Forms.GroupBox()
        Me.txtRepeatability = New System.Windows.Forms.TextBox()
        Me.btnRepeatRun = New System.Windows.Forms.Button()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.OFDLoadScene = New System.Windows.Forms.OpenFileDialog()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControlImage.SuspendLayout()
        Me.TabPageCCD.SuspendLayout()
        Me.TabPageAlign.SuspendLayout()
        Me.grpBlob.SuspendLayout()
        CType(Me.nmcMinArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcBlobHighThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcBlobLowThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CogDisplay1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPatternArea.SuspendLayout()
        Me.grpParamter.SuspendLayout()
        CType(Me.nmcScale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcRotation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcMatchRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcPatternAreaH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcPatternAreaW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCaliper.SuspendLayout()
        Me.grpRadiusTolerance.SuspendLayout()
        CType(Me.nmcRadiusTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcProjectionLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmclblFilterHalfSizePixels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcContrastThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcCalipersIgnoreNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcSearchLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcCalipersNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpThreshold.SuspendLayout()
        CType(Me.nmcOutputDownThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcOutputUpThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcInputUpThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmcInputDownThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSelectScene.SuspendLayout()
        Me.grpCalibScene.SuspendLayout()
        Me.grpSetLight.SuspendLayout()
        Me.grpImageSource.SuspendLayout()
        Me.grpText.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstScene
        '
        resources.ApplyResources(Me.lstScene, "lstScene")
        Me.lstScene.FormattingEnabled = True
        Me.lstScene.Items.AddRange(New Object() {resources.GetString("lstScene.Items"), resources.GetString("lstScene.Items1")})
        Me.lstScene.Name = "lstScene"
        '
        'txtScene
        '
        resources.ApplyResources(Me.txtScene, "txtScene")
        Me.txtScene.Name = "txtScene"
        '
        'TabControlImage
        '
        Me.TabControlImage.Controls.Add(Me.TabPageCCD)
        Me.TabControlImage.Controls.Add(Me.TabPageAlign)
        resources.ApplyResources(Me.TabControlImage, "TabControlImage")
        Me.TabControlImage.Name = "TabControlImage"
        Me.TabControlImage.SelectedIndex = 0
        '
        'TabPageCCD
        '
        Me.TabPageCCD.Controls.Add(Me.UcDisplay1)
        resources.ApplyResources(Me.TabPageCCD, "TabPageCCD")
        Me.TabPageCCD.Name = "TabPageCCD"
        Me.TabPageCCD.UseVisualStyleBackColor = True
        '
        'UcDisplay1
        '
        Me.UcDisplay1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.UcDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.UcDisplay1, "UcDisplay1")
        Me.UcDisplay1.Name = "UcDisplay1"
        '
        'TabPageAlign
        '
        Me.TabPageAlign.Controls.Add(Me.grpBlob)
        Me.TabPageAlign.Controls.Add(Me.CogDisplay1)
        Me.TabPageAlign.Controls.Add(Me.grpPatternArea)
        Me.TabPageAlign.Controls.Add(Me.grpCaliper)
        Me.TabPageAlign.Controls.Add(Me.grpThreshold)
        resources.ApplyResources(Me.TabPageAlign, "TabPageAlign")
        Me.TabPageAlign.Name = "TabPageAlign"
        Me.TabPageAlign.UseVisualStyleBackColor = True
        '
        'grpBlob
        '
        Me.grpBlob.Controls.Add(Me.btnBlobROI)
        Me.grpBlob.Controls.Add(Me.btnBlobRun)
        Me.grpBlob.Controls.Add(Me.nmcMinArea)
        Me.grpBlob.Controls.Add(Me.nmcBlobHighThreshold)
        Me.grpBlob.Controls.Add(Me.nmcBlobLowThreshold)
        Me.grpBlob.Controls.Add(Me.cmbBlobPolarity)
        Me.grpBlob.Controls.Add(Me.lblBlobMinArea)
        Me.grpBlob.Controls.Add(Me.lblHighThreshold)
        Me.grpBlob.Controls.Add(Me.lblLowThreshold)
        Me.grpBlob.Controls.Add(Me.lblBlobPolarity)
        resources.ApplyResources(Me.grpBlob, "grpBlob")
        Me.grpBlob.Name = "grpBlob"
        Me.grpBlob.TabStop = False
        '
        'btnBlobROI
        '
        resources.ApplyResources(Me.btnBlobROI, "btnBlobROI")
        Me.btnBlobROI.Name = "btnBlobROI"
        Me.btnBlobROI.UseVisualStyleBackColor = True
        '
        'btnBlobRun
        '
        resources.ApplyResources(Me.btnBlobRun, "btnBlobRun")
        Me.btnBlobRun.Name = "btnBlobRun"
        Me.btnBlobRun.UseVisualStyleBackColor = True
        '
        'nmcMinArea
        '
        resources.ApplyResources(Me.nmcMinArea, "nmcMinArea")
        Me.nmcMinArea.Maximum = New Decimal(New Integer() {200000, 0, 0, 0})
        Me.nmcMinArea.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nmcMinArea.Name = "nmcMinArea"
        Me.nmcMinArea.Value = New Decimal(New Integer() {5000, 0, 0, 0})
        '
        'nmcBlobHighThreshold
        '
        Me.nmcBlobHighThreshold.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.nmcBlobHighThreshold, "nmcBlobHighThreshold")
        Me.nmcBlobHighThreshold.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nmcBlobHighThreshold.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcBlobHighThreshold.Name = "nmcBlobHighThreshold"
        Me.nmcBlobHighThreshold.Value = New Decimal(New Integer() {255, 0, 0, 0})
        '
        'nmcBlobLowThreshold
        '
        Me.nmcBlobLowThreshold.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.nmcBlobLowThreshold, "nmcBlobLowThreshold")
        Me.nmcBlobLowThreshold.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nmcBlobLowThreshold.Name = "nmcBlobLowThreshold"
        '
        'cmbBlobPolarity
        '
        Me.cmbBlobPolarity.FormattingEnabled = True
        Me.cmbBlobPolarity.Items.AddRange(New Object() {resources.GetString("cmbBlobPolarity.Items"), resources.GetString("cmbBlobPolarity.Items1")})
        resources.ApplyResources(Me.cmbBlobPolarity, "cmbBlobPolarity")
        Me.cmbBlobPolarity.Name = "cmbBlobPolarity"
        '
        'lblBlobMinArea
        '
        resources.ApplyResources(Me.lblBlobMinArea, "lblBlobMinArea")
        Me.lblBlobMinArea.Name = "lblBlobMinArea"
        '
        'lblHighThreshold
        '
        resources.ApplyResources(Me.lblHighThreshold, "lblHighThreshold")
        Me.lblHighThreshold.Name = "lblHighThreshold"
        '
        'lblLowThreshold
        '
        resources.ApplyResources(Me.lblLowThreshold, "lblLowThreshold")
        Me.lblLowThreshold.Name = "lblLowThreshold"
        '
        'lblBlobPolarity
        '
        resources.ApplyResources(Me.lblBlobPolarity, "lblBlobPolarity")
        Me.lblBlobPolarity.Name = "lblBlobPolarity"
        '
        'CogDisplay1
        '
        Me.CogDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black
        Me.CogDisplay1.ColorMapLowerRoiLimit = 0.0R
        Me.CogDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None
        Me.CogDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black
        Me.CogDisplay1.ColorMapUpperRoiLimit = 1.0R
        Me.CogDisplay1.DoubleTapZoomCycleLength = 2
        Me.CogDisplay1.DoubleTapZoomSensitivity = 2.5R
        resources.ApplyResources(Me.CogDisplay1, "CogDisplay1")
        Me.CogDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1
        Me.CogDisplay1.MouseWheelSensitivity = 1.0R
        Me.CogDisplay1.Name = "CogDisplay1"
        Me.CogDisplay1.OcxState = CType(resources.GetObject("CogDisplay1.OcxState"), System.Windows.Forms.AxHost.State)
        '
        'grpPatternArea
        '
        Me.grpPatternArea.Controls.Add(Me.txtTrainMsg)
        Me.grpPatternArea.Controls.Add(Me.lblTrainMsg)
        Me.grpPatternArea.Controls.Add(Me.grpParamter)
        Me.grpPatternArea.Controls.Add(Me.nmcPatternAreaH)
        Me.grpPatternArea.Controls.Add(Me.nmcPatternAreaW)
        Me.grpPatternArea.Controls.Add(Me.btnAlignROI)
        Me.grpPatternArea.Controls.Add(Me.btnImageMask)
        Me.grpPatternArea.Controls.Add(Me.btnGrabTrainImage)
        Me.grpPatternArea.Controls.Add(Me.lblPatternAreaWUnit)
        Me.grpPatternArea.Controls.Add(Me.btnShowTrain)
        Me.grpPatternArea.Controls.Add(Me.Label2)
        Me.grpPatternArea.Controls.Add(Me.lblPatternAreaHUnit)
        Me.grpPatternArea.Controls.Add(Me.Label4)
        resources.ApplyResources(Me.grpPatternArea, "grpPatternArea")
        Me.grpPatternArea.Name = "grpPatternArea"
        Me.grpPatternArea.TabStop = False
        '
        'txtTrainMsg
        '
        Me.txtTrainMsg.ForeColor = System.Drawing.SystemColors.WindowText
        resources.ApplyResources(Me.txtTrainMsg, "txtTrainMsg")
        Me.txtTrainMsg.Name = "txtTrainMsg"
        '
        'lblTrainMsg
        '
        resources.ApplyResources(Me.lblTrainMsg, "lblTrainMsg")
        Me.lblTrainMsg.Name = "lblTrainMsg"
        '
        'grpParamter
        '
        Me.grpParamter.Controls.Add(Me.btnPMAlignRun)
        Me.grpParamter.Controls.Add(Me.nmcScale)
        Me.grpParamter.Controls.Add(Me.lblScale)
        Me.grpParamter.Controls.Add(Me.btnTrain)
        Me.grpParamter.Controls.Add(Me.lblRotationUnit)
        Me.grpParamter.Controls.Add(Me.nmcRotation)
        Me.grpParamter.Controls.Add(Me.lblRotation)
        Me.grpParamter.Controls.Add(Me.lblMatchRate)
        Me.grpParamter.Controls.Add(Me.lblMatchRateUnit)
        Me.grpParamter.Controls.Add(Me.nmcMatchRate)
        resources.ApplyResources(Me.grpParamter, "grpParamter")
        Me.grpParamter.Name = "grpParamter"
        Me.grpParamter.TabStop = False
        '
        'btnPMAlignRun
        '
        resources.ApplyResources(Me.btnPMAlignRun, "btnPMAlignRun")
        Me.btnPMAlignRun.Name = "btnPMAlignRun"
        Me.btnPMAlignRun.UseVisualStyleBackColor = True
        '
        'nmcScale
        '
        Me.nmcScale.DecimalPlaces = 2
        resources.ApplyResources(Me.nmcScale, "nmcScale")
        Me.nmcScale.Maximum = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.nmcScale.Name = "nmcScale"
        '
        'lblScale
        '
        resources.ApplyResources(Me.lblScale, "lblScale")
        Me.lblScale.Name = "lblScale"
        '
        'btnTrain
        '
        resources.ApplyResources(Me.btnTrain, "btnTrain")
        Me.btnTrain.Name = "btnTrain"
        Me.btnTrain.UseVisualStyleBackColor = True
        '
        'lblRotationUnit
        '
        resources.ApplyResources(Me.lblRotationUnit, "lblRotationUnit")
        Me.lblRotationUnit.Name = "lblRotationUnit"
        '
        'nmcRotation
        '
        resources.ApplyResources(Me.nmcRotation, "nmcRotation")
        Me.nmcRotation.Maximum = New Decimal(New Integer() {45, 0, 0, 0})
        Me.nmcRotation.Name = "nmcRotation"
        Me.nmcRotation.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'lblRotation
        '
        resources.ApplyResources(Me.lblRotation, "lblRotation")
        Me.lblRotation.Name = "lblRotation"
        '
        'lblMatchRate
        '
        resources.ApplyResources(Me.lblMatchRate, "lblMatchRate")
        Me.lblMatchRate.Name = "lblMatchRate"
        '
        'lblMatchRateUnit
        '
        resources.ApplyResources(Me.lblMatchRateUnit, "lblMatchRateUnit")
        Me.lblMatchRateUnit.Name = "lblMatchRateUnit"
        '
        'nmcMatchRate
        '
        resources.ApplyResources(Me.nmcMatchRate, "nmcMatchRate")
        Me.nmcMatchRate.Name = "nmcMatchRate"
        Me.nmcMatchRate.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'nmcPatternAreaH
        '
        Me.nmcPatternAreaH.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.nmcPatternAreaH, "nmcPatternAreaH")
        Me.nmcPatternAreaH.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nmcPatternAreaH.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nmcPatternAreaH.Name = "nmcPatternAreaH"
        Me.nmcPatternAreaH.Value = New Decimal(New Integer() {600, 0, 0, 0})
        '
        'nmcPatternAreaW
        '
        Me.nmcPatternAreaW.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.nmcPatternAreaW, "nmcPatternAreaW")
        Me.nmcPatternAreaW.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nmcPatternAreaW.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nmcPatternAreaW.Name = "nmcPatternAreaW"
        Me.nmcPatternAreaW.Value = New Decimal(New Integer() {800, 0, 0, 0})
        '
        'btnAlignROI
        '
        resources.ApplyResources(Me.btnAlignROI, "btnAlignROI")
        Me.btnAlignROI.Name = "btnAlignROI"
        Me.btnAlignROI.UseVisualStyleBackColor = True
        '
        'btnImageMask
        '
        resources.ApplyResources(Me.btnImageMask, "btnImageMask")
        Me.btnImageMask.Name = "btnImageMask"
        Me.btnImageMask.UseVisualStyleBackColor = True
        '
        'btnGrabTrainImage
        '
        resources.ApplyResources(Me.btnGrabTrainImage, "btnGrabTrainImage")
        Me.btnGrabTrainImage.Name = "btnGrabTrainImage"
        Me.btnGrabTrainImage.UseVisualStyleBackColor = True
        '
        'lblPatternAreaWUnit
        '
        resources.ApplyResources(Me.lblPatternAreaWUnit, "lblPatternAreaWUnit")
        Me.lblPatternAreaWUnit.Name = "lblPatternAreaWUnit"
        '
        'btnShowTrain
        '
        resources.ApplyResources(Me.btnShowTrain, "btnShowTrain")
        Me.btnShowTrain.Name = "btnShowTrain"
        Me.btnShowTrain.UseVisualStyleBackColor = True
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'lblPatternAreaHUnit
        '
        resources.ApplyResources(Me.lblPatternAreaHUnit, "lblPatternAreaHUnit")
        Me.lblPatternAreaHUnit.Name = "lblPatternAreaHUnit"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'grpCaliper
        '
        Me.grpCaliper.Controls.Add(Me.grpRadiusTolerance)
        Me.grpCaliper.Controls.Add(Me.nmcProjectionLength)
        Me.grpCaliper.Controls.Add(Me.lblProjectionLength)
        Me.grpCaliper.Controls.Add(Me.btnCaliperRun)
        Me.grpCaliper.Controls.Add(Me.btnShowCalipers)
        Me.grpCaliper.Controls.Add(Me.nmclblFilterHalfSizePixels)
        Me.grpCaliper.Controls.Add(Me.lblFilterHalfSizePixels)
        Me.grpCaliper.Controls.Add(Me.nmcContrastThreshold)
        Me.grpCaliper.Controls.Add(Me.lblContrastThreshold)
        Me.grpCaliper.Controls.Add(Me.cmbCaplipersPolarity)
        Me.grpCaliper.Controls.Add(Me.lblPolarity)
        Me.grpCaliper.Controls.Add(Me.lblCalipersIgnoreNumber)
        Me.grpCaliper.Controls.Add(Me.nmcCalipersIgnoreNumber)
        Me.grpCaliper.Controls.Add(Me.cmbCaplipersSearchDirection)
        Me.grpCaliper.Controls.Add(Me.lblSearchDirection)
        Me.grpCaliper.Controls.Add(Me.nmcSearchLength)
        Me.grpCaliper.Controls.Add(Me.lblSearchLength)
        Me.grpCaliper.Controls.Add(Me.lblCalipersNumber)
        Me.grpCaliper.Controls.Add(Me.nmcCalipersNumber)
        resources.ApplyResources(Me.grpCaliper, "grpCaliper")
        Me.grpCaliper.Name = "grpCaliper"
        Me.grpCaliper.TabStop = False
        '
        'grpRadiusTolerance
        '
        Me.grpRadiusTolerance.Controls.Add(Me.lblRadiusTolerance)
        Me.grpRadiusTolerance.Controls.Add(Me.lblRadiusUnit)
        Me.grpRadiusTolerance.Controls.Add(Me.nmcRadiusTolerance)
        resources.ApplyResources(Me.grpRadiusTolerance, "grpRadiusTolerance")
        Me.grpRadiusTolerance.Name = "grpRadiusTolerance"
        Me.grpRadiusTolerance.TabStop = False
        '
        'lblRadiusTolerance
        '
        resources.ApplyResources(Me.lblRadiusTolerance, "lblRadiusTolerance")
        Me.lblRadiusTolerance.Name = "lblRadiusTolerance"
        '
        'lblRadiusUnit
        '
        resources.ApplyResources(Me.lblRadiusUnit, "lblRadiusUnit")
        Me.lblRadiusUnit.Name = "lblRadiusUnit"
        '
        'nmcRadiusTolerance
        '
        resources.ApplyResources(Me.nmcRadiusTolerance, "nmcRadiusTolerance")
        Me.nmcRadiusTolerance.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nmcRadiusTolerance.Name = "nmcRadiusTolerance"
        Me.nmcRadiusTolerance.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'nmcProjectionLength
        '
        resources.ApplyResources(Me.nmcProjectionLength, "nmcProjectionLength")
        Me.nmcProjectionLength.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.nmcProjectionLength.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmcProjectionLength.Name = "nmcProjectionLength"
        Me.nmcProjectionLength.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'lblProjectionLength
        '
        resources.ApplyResources(Me.lblProjectionLength, "lblProjectionLength")
        Me.lblProjectionLength.Name = "lblProjectionLength"
        '
        'btnCaliperRun
        '
        resources.ApplyResources(Me.btnCaliperRun, "btnCaliperRun")
        Me.btnCaliperRun.Name = "btnCaliperRun"
        Me.btnCaliperRun.UseVisualStyleBackColor = True
        '
        'btnShowCalipers
        '
        resources.ApplyResources(Me.btnShowCalipers, "btnShowCalipers")
        Me.btnShowCalipers.Name = "btnShowCalipers"
        Me.btnShowCalipers.UseVisualStyleBackColor = True
        '
        'nmclblFilterHalfSizePixels
        '
        resources.ApplyResources(Me.nmclblFilterHalfSizePixels, "nmclblFilterHalfSizePixels")
        Me.nmclblFilterHalfSizePixels.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.nmclblFilterHalfSizePixels.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmclblFilterHalfSizePixels.Name = "nmclblFilterHalfSizePixels"
        Me.nmclblFilterHalfSizePixels.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'lblFilterHalfSizePixels
        '
        resources.ApplyResources(Me.lblFilterHalfSizePixels, "lblFilterHalfSizePixels")
        Me.lblFilterHalfSizePixels.Name = "lblFilterHalfSizePixels"
        '
        'nmcContrastThreshold
        '
        resources.ApplyResources(Me.nmcContrastThreshold, "nmcContrastThreshold")
        Me.nmcContrastThreshold.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.nmcContrastThreshold.Name = "nmcContrastThreshold"
        Me.nmcContrastThreshold.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'lblContrastThreshold
        '
        resources.ApplyResources(Me.lblContrastThreshold, "lblContrastThreshold")
        Me.lblContrastThreshold.Name = "lblContrastThreshold"
        '
        'cmbCaplipersPolarity
        '
        Me.cmbCaplipersPolarity.FormattingEnabled = True
        Me.cmbCaplipersPolarity.Items.AddRange(New Object() {resources.GetString("cmbCaplipersPolarity.Items"), resources.GetString("cmbCaplipersPolarity.Items1"), resources.GetString("cmbCaplipersPolarity.Items2")})
        resources.ApplyResources(Me.cmbCaplipersPolarity, "cmbCaplipersPolarity")
        Me.cmbCaplipersPolarity.Name = "cmbCaplipersPolarity"
        '
        'lblPolarity
        '
        resources.ApplyResources(Me.lblPolarity, "lblPolarity")
        Me.lblPolarity.Name = "lblPolarity"
        '
        'lblCalipersIgnoreNumber
        '
        resources.ApplyResources(Me.lblCalipersIgnoreNumber, "lblCalipersIgnoreNumber")
        Me.lblCalipersIgnoreNumber.Name = "lblCalipersIgnoreNumber"
        '
        'nmcCalipersIgnoreNumber
        '
        resources.ApplyResources(Me.nmcCalipersIgnoreNumber, "nmcCalipersIgnoreNumber")
        Me.nmcCalipersIgnoreNumber.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nmcCalipersIgnoreNumber.Name = "nmcCalipersIgnoreNumber"
        '
        'cmbCaplipersSearchDirection
        '
        Me.cmbCaplipersSearchDirection.FormattingEnabled = True
        Me.cmbCaplipersSearchDirection.Items.AddRange(New Object() {resources.GetString("cmbCaplipersSearchDirection.Items"), resources.GetString("cmbCaplipersSearchDirection.Items1")})
        resources.ApplyResources(Me.cmbCaplipersSearchDirection, "cmbCaplipersSearchDirection")
        Me.cmbCaplipersSearchDirection.Name = "cmbCaplipersSearchDirection"
        '
        'lblSearchDirection
        '
        resources.ApplyResources(Me.lblSearchDirection, "lblSearchDirection")
        Me.lblSearchDirection.Name = "lblSearchDirection"
        '
        'nmcSearchLength
        '
        resources.ApplyResources(Me.nmcSearchLength, "nmcSearchLength")
        Me.nmcSearchLength.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.nmcSearchLength.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nmcSearchLength.Name = "nmcSearchLength"
        Me.nmcSearchLength.Value = New Decimal(New Integer() {150, 0, 0, 0})
        '
        'lblSearchLength
        '
        resources.ApplyResources(Me.lblSearchLength, "lblSearchLength")
        Me.lblSearchLength.Name = "lblSearchLength"
        '
        'lblCalipersNumber
        '
        resources.ApplyResources(Me.lblCalipersNumber, "lblCalipersNumber")
        Me.lblCalipersNumber.Name = "lblCalipersNumber"
        '
        'nmcCalipersNumber
        '
        resources.ApplyResources(Me.nmcCalipersNumber, "nmcCalipersNumber")
        Me.nmcCalipersNumber.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.nmcCalipersNumber.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.nmcCalipersNumber.Name = "nmcCalipersNumber"
        Me.nmcCalipersNumber.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'grpThreshold
        '
        Me.grpThreshold.Controls.Add(Me.nmcOutputDownThreshold)
        Me.grpThreshold.Controls.Add(Me.nmcOutputUpThreshold)
        Me.grpThreshold.Controls.Add(Me.lblOutput)
        Me.grpThreshold.Controls.Add(Me.lblInput)
        Me.grpThreshold.Controls.Add(Me.nmcInputUpThreshold)
        Me.grpThreshold.Controls.Add(Me.btnThresholdMore)
        Me.grpThreshold.Controls.Add(Me.nmcInputDownThreshold)
        Me.grpThreshold.Controls.Add(Me.lblLowerlimit)
        Me.grpThreshold.Controls.Add(Me.lblUpperlimit)
        resources.ApplyResources(Me.grpThreshold, "grpThreshold")
        Me.grpThreshold.Name = "grpThreshold"
        Me.grpThreshold.TabStop = False
        '
        'nmcOutputDownThreshold
        '
        Me.nmcOutputDownThreshold.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.nmcOutputDownThreshold, "nmcOutputDownThreshold")
        Me.nmcOutputDownThreshold.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nmcOutputDownThreshold.Name = "nmcOutputDownThreshold"
        '
        'nmcOutputUpThreshold
        '
        Me.nmcOutputUpThreshold.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.nmcOutputUpThreshold, "nmcOutputUpThreshold")
        Me.nmcOutputUpThreshold.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nmcOutputUpThreshold.Name = "nmcOutputUpThreshold"
        Me.nmcOutputUpThreshold.Value = New Decimal(New Integer() {255, 0, 0, 0})
        '
        'lblOutput
        '
        resources.ApplyResources(Me.lblOutput, "lblOutput")
        Me.lblOutput.Name = "lblOutput"
        '
        'lblInput
        '
        resources.ApplyResources(Me.lblInput, "lblInput")
        Me.lblInput.Name = "lblInput"
        '
        'nmcInputUpThreshold
        '
        Me.nmcInputUpThreshold.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.nmcInputUpThreshold, "nmcInputUpThreshold")
        Me.nmcInputUpThreshold.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nmcInputUpThreshold.Name = "nmcInputUpThreshold"
        Me.nmcInputUpThreshold.Value = New Decimal(New Integer() {255, 0, 0, 0})
        '
        'btnThresholdMore
        '
        resources.ApplyResources(Me.btnThresholdMore, "btnThresholdMore")
        Me.btnThresholdMore.Name = "btnThresholdMore"
        Me.btnThresholdMore.UseVisualStyleBackColor = True
        '
        'nmcInputDownThreshold
        '
        Me.nmcInputDownThreshold.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.nmcInputDownThreshold, "nmcInputDownThreshold")
        Me.nmcInputDownThreshold.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nmcInputDownThreshold.Name = "nmcInputDownThreshold"
        '
        'lblLowerlimit
        '
        resources.ApplyResources(Me.lblLowerlimit, "lblLowerlimit")
        Me.lblLowerlimit.Name = "lblLowerlimit"
        '
        'lblUpperlimit
        '
        resources.ApplyResources(Me.lblUpperlimit, "lblUpperlimit")
        Me.lblUpperlimit.Name = "lblUpperlimit"
        '
        'grpSelectScene
        '
        Me.grpSelectScene.Controls.Add(Me.lstScene)
        Me.grpSelectScene.Controls.Add(Me.btnSceneAdd)
        Me.grpSelectScene.Controls.Add(Me.btnSceneDel)
        Me.grpSelectScene.Controls.Add(Me.txtScene)
        resources.ApplyResources(Me.grpSelectScene, "grpSelectScene")
        Me.grpSelectScene.Name = "grpSelectScene"
        Me.grpSelectScene.TabStop = False
        '
        'btnSceneAdd
        '
        resources.ApplyResources(Me.btnSceneAdd, "btnSceneAdd")
        Me.btnSceneAdd.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Add1
        Me.btnSceneAdd.Name = "btnSceneAdd"
        Me.ToolTip1.SetToolTip(Me.btnSceneAdd, resources.GetString("btnSceneAdd.ToolTip"))
        Me.btnSceneAdd.UseVisualStyleBackColor = True
        '
        'btnSceneDel
        '
        resources.ApplyResources(Me.btnSceneDel, "btnSceneDel")
        Me.btnSceneDel.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.Delete
        Me.btnSceneDel.Name = "btnSceneDel"
        Me.ToolTip1.SetToolTip(Me.btnSceneDel, resources.GetString("btnSceneDel.ToolTip"))
        Me.btnSceneDel.UseVisualStyleBackColor = True
        '
        'grpCalibScene
        '
        Me.grpCalibScene.Controls.Add(Me.btnLoadScene)
        Me.grpCalibScene.Controls.Add(Me.txtCalibScene)
        resources.ApplyResources(Me.grpCalibScene, "grpCalibScene")
        Me.grpCalibScene.Name = "grpCalibScene"
        Me.grpCalibScene.TabStop = False
        '
        'btnLoadScene
        '
        Me.btnLoadScene.AutoEllipsis = True
        Me.btnLoadScene.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.OpenFile
        resources.ApplyResources(Me.btnLoadScene, "btnLoadScene")
        Me.btnLoadScene.FlatAppearance.BorderSize = 0
        Me.btnLoadScene.Name = "btnLoadScene"
        Me.ToolTip1.SetToolTip(Me.btnLoadScene, resources.GetString("btnLoadScene.ToolTip"))
        Me.btnLoadScene.UseVisualStyleBackColor = True
        '
        'txtCalibScene
        '
        resources.ApplyResources(Me.txtCalibScene, "txtCalibScene")
        Me.txtCalibScene.Name = "txtCalibScene"
        '
        'grpSetLight
        '
        Me.grpSetLight.Controls.Add(Me.UcLightControl1)
        resources.ApplyResources(Me.grpSetLight, "grpSetLight")
        Me.grpSetLight.Name = "grpSetLight"
        Me.grpSetLight.TabStop = False
        '
        'UcLightControl1
        '
        Me.UcLightControl1.BackColor = System.Drawing.SystemColors.ControlLightLight
        resources.ApplyResources(Me.UcLightControl1, "UcLightControl1")
        Me.UcLightControl1.Name = "UcLightControl1"
        '
        'grpImageSource
        '
        Me.grpImageSource.Controls.Add(Me.btnACQ)
        Me.grpImageSource.Controls.Add(Me.btnImport)
        Me.grpImageSource.Controls.Add(Me.lblFolder)
        Me.grpImageSource.Controls.Add(Me.hscFile)
        resources.ApplyResources(Me.grpImageSource, "grpImageSource")
        Me.grpImageSource.Name = "grpImageSource"
        Me.grpImageSource.TabStop = False
        '
        'btnACQ
        '
        resources.ApplyResources(Me.btnACQ, "btnACQ")
        Me.btnACQ.Name = "btnACQ"
        Me.btnACQ.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        resources.ApplyResources(Me.btnImport, "btnImport")
        Me.btnImport.Name = "btnImport"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'lblFolder
        '
        resources.ApplyResources(Me.lblFolder, "lblFolder")
        Me.lblFolder.Name = "lblFolder"
        '
        'hscFile
        '
        resources.ApplyResources(Me.hscFile, "hscFile")
        Me.hscFile.LargeChange = 1
        Me.hscFile.Maximum = 7
        Me.hscFile.Name = "hscFile"
        Me.hscFile.Value = 1
        '
        'btnReset
        '
        resources.ApplyResources(Me.btnReset, "btnReset")
        Me.btnReset.FlatAppearance.BorderSize = 0
        Me.btnReset.Name = "btnReset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'lblTrainTool
        '
        resources.ApplyResources(Me.lblTrainTool, "lblTrainTool")
        Me.lblTrainTool.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblTrainTool.Name = "lblTrainTool"
        '
        'grpText
        '
        Me.grpText.Controls.Add(Me.txtRepeatability)
        Me.grpText.Controls.Add(Me.btnReset)
        Me.grpText.Controls.Add(Me.btnRepeatRun)
        Me.grpText.Controls.Add(Me.btnRun)
        resources.ApplyResources(Me.grpText, "grpText")
        Me.grpText.Name = "grpText"
        Me.grpText.TabStop = False
        '
        'txtRepeatability
        '
        Me.txtRepeatability.AllowDrop = True
        Me.txtRepeatability.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtRepeatability, "txtRepeatability")
        Me.txtRepeatability.Name = "txtRepeatability"
        '
        'btnRepeatRun
        '
        resources.ApplyResources(Me.btnRepeatRun, "btnRepeatRun")
        Me.btnRepeatRun.FlatAppearance.BorderSize = 0
        Me.btnRepeatRun.Name = "btnRepeatRun"
        Me.btnRepeatRun.UseVisualStyleBackColor = True
        '
        'btnRun
        '
        resources.ApplyResources(Me.btnRun, "btnRun")
        Me.btnRun.FlatAppearance.BorderSize = 0
        Me.btnRun.Name = "btnRun"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'OFDLoadScene
        '
        Me.OFDLoadScene.FileName = "OpenFileDialog1"
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
        'frmAlignModule
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ControlBox = False
        Me.Controls.Add(Me.grpCalibScene)
        Me.Controls.Add(Me.grpText)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lblTrainTool)
        Me.Controls.Add(Me.grpImageSource)
        Me.Controls.Add(Me.grpSetLight)
        Me.Controls.Add(Me.grpSelectScene)
        Me.Controls.Add(Me.TabControlImage)
        Me.MaximizeBox = False
        Me.Name = "frmAlignModule"
        Me.TabControlImage.ResumeLayout(False)
        Me.TabPageCCD.ResumeLayout(False)
        Me.TabPageAlign.ResumeLayout(False)
        Me.grpBlob.ResumeLayout(False)
        CType(Me.nmcMinArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcBlobHighThreshold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcBlobLowThreshold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CogDisplay1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPatternArea.ResumeLayout(False)
        Me.grpPatternArea.PerformLayout()
        Me.grpParamter.ResumeLayout(False)
        Me.grpParamter.PerformLayout()
        CType(Me.nmcScale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcRotation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcMatchRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcPatternAreaH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcPatternAreaW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCaliper.ResumeLayout(False)
        Me.grpRadiusTolerance.ResumeLayout(False)
        Me.grpRadiusTolerance.PerformLayout()
        CType(Me.nmcRadiusTolerance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcProjectionLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmclblFilterHalfSizePixels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcContrastThreshold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcCalipersIgnoreNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcSearchLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcCalipersNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpThreshold.ResumeLayout(False)
        Me.grpThreshold.PerformLayout()
        CType(Me.nmcOutputDownThreshold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcOutputUpThreshold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcInputUpThreshold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmcInputDownThreshold, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSelectScene.ResumeLayout(False)
        Me.grpSelectScene.PerformLayout()
        Me.grpCalibScene.ResumeLayout(False)
        Me.grpCalibScene.PerformLayout()
        Me.grpSetLight.ResumeLayout(False)
        Me.grpImageSource.ResumeLayout(False)
        Me.grpText.ResumeLayout(False)
        Me.grpText.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents lstScene As System.Windows.Forms.ListBox
    Friend WithEvents btnSceneAdd As System.Windows.Forms.Button
    Friend WithEvents txtScene As System.Windows.Forms.TextBox
    Friend WithEvents btnSceneDel As System.Windows.Forms.Button
    Friend WithEvents TabControlImage As System.Windows.Forms.TabControl
    Friend WithEvents TabPageCCD As System.Windows.Forms.TabPage
    Friend WithEvents TabPageAlign As System.Windows.Forms.TabPage
    Friend WithEvents UcDisplay1 As ProjectAOI.ucDisplay
    Friend WithEvents grpSelectScene As System.Windows.Forms.GroupBox
    Friend WithEvents grpSetLight As System.Windows.Forms.GroupBox
    Friend WithEvents UcLightControl1 As WindowsApplication1.ucLightControl
    Friend WithEvents grpImageSource As System.Windows.Forms.GroupBox
    Friend WithEvents lblFolder As System.Windows.Forms.Label
    Friend WithEvents hscFile As System.Windows.Forms.HScrollBar
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents btnACQ As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents btnRepeatRun As System.Windows.Forms.Button
    Friend WithEvents lblTrainTool As System.Windows.Forms.Label
    Friend WithEvents grpThreshold As System.Windows.Forms.GroupBox
    Friend WithEvents grpCaliper As System.Windows.Forms.GroupBox
    Friend WithEvents nmcInputDownThreshold As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblLowerlimit As System.Windows.Forms.Label
    Friend WithEvents lblUpperlimit As System.Windows.Forms.Label
    Friend WithEvents grpPatternArea As System.Windows.Forms.GroupBox
    Friend WithEvents grpParamter As System.Windows.Forms.GroupBox
    Friend WithEvents lblMatchRate As System.Windows.Forms.Label
    Friend WithEvents lblMatchRateUnit As System.Windows.Forms.Label
    Friend WithEvents nmcMatchRate As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcRotation As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblRotation As System.Windows.Forms.Label
    Friend WithEvents nmcScale As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblScale As System.Windows.Forms.Label
    Friend WithEvents lblRotationUnit As System.Windows.Forms.Label
    Friend WithEvents grpText As System.Windows.Forms.GroupBox
    Friend WithEvents btnImageMask As System.Windows.Forms.Button
    Friend WithEvents btnTrain As System.Windows.Forms.Button
    Friend WithEvents btnAlignROI As System.Windows.Forms.Button
    Friend WithEvents btnThresholdMore As System.Windows.Forms.Button
    Friend WithEvents btnShowTrain As System.Windows.Forms.Button
    Friend WithEvents btnGrabTrainImage As System.Windows.Forms.Button
    Friend WithEvents nmcInputUpThreshold As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblOutput As System.Windows.Forms.Label
    Friend WithEvents lblInput As System.Windows.Forms.Label
    Friend WithEvents nmcOutputUpThreshold As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcOutputDownThreshold As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblPatternAreaWUnit As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblPatternAreaHUnit As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CogDisplay1 As Cognex.VisionPro.Display.CogDisplay
    Friend WithEvents nmcPatternAreaH As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcPatternAreaW As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnPMAlignRun As System.Windows.Forms.Button
    Friend WithEvents txtTrainMsg As System.Windows.Forms.TextBox
    Friend WithEvents lblTrainMsg As System.Windows.Forms.Label
    Friend WithEvents nmcProjectionLength As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblProjectionLength As System.Windows.Forms.Label
    Friend WithEvents btnCaliperRun As System.Windows.Forms.Button
    Friend WithEvents btnShowCalipers As System.Windows.Forms.Button
    Friend WithEvents nmclblFilterHalfSizePixels As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblFilterHalfSizePixels As System.Windows.Forms.Label
    Friend WithEvents nmcContrastThreshold As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblContrastThreshold As System.Windows.Forms.Label
    Friend WithEvents cmbCaplipersPolarity As System.Windows.Forms.ComboBox
    Friend WithEvents lblPolarity As System.Windows.Forms.Label
    Friend WithEvents lblCalipersIgnoreNumber As System.Windows.Forms.Label
    Friend WithEvents nmcCalipersIgnoreNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbCaplipersSearchDirection As System.Windows.Forms.ComboBox
    Friend WithEvents lblSearchDirection As System.Windows.Forms.Label
    Friend WithEvents nmcSearchLength As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblSearchLength As System.Windows.Forms.Label
    Friend WithEvents lblCalipersNumber As System.Windows.Forms.Label
    Friend WithEvents nmcCalipersNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents grpCalibScene As System.Windows.Forms.GroupBox
    Friend WithEvents btnLoadScene As System.Windows.Forms.Button
    Friend WithEvents txtCalibScene As System.Windows.Forms.TextBox
    Friend WithEvents grpBlob As System.Windows.Forms.GroupBox
    Friend WithEvents nmcMinArea As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcBlobHighThreshold As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmcBlobLowThreshold As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbBlobPolarity As System.Windows.Forms.ComboBox
    Friend WithEvents lblBlobMinArea As System.Windows.Forms.Label
    Friend WithEvents lblHighThreshold As System.Windows.Forms.Label
    Friend WithEvents lblLowThreshold As System.Windows.Forms.Label
    Friend WithEvents lblBlobPolarity As System.Windows.Forms.Label
    Friend WithEvents nmcRadiusTolerance As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblRadiusTolerance As System.Windows.Forms.Label
    Friend WithEvents btnBlobRun As System.Windows.Forms.Button
    Friend WithEvents OFDLoadScene As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblRadiusUnit As System.Windows.Forms.Label
    Friend WithEvents grpRadiusTolerance As System.Windows.Forms.GroupBox
    Friend WithEvents btnBlobROI As System.Windows.Forms.Button
    Friend WithEvents txtRepeatability As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

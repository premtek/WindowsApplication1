Imports System.Globalization
Imports ProjectCore
Imports ProjectIO
Imports ProjectFeedback
Imports ProjectRecipe
Imports ProjectAOI
Imports ProjectLaserInterferometer
Imports ProjectTriggerBoard
Imports ProjectValveController

Module MUILanguage
    Sub CloseForm(ByRef frm As Form)
        If frm Is Nothing Then
            Exit Sub
        End If
        frm.Close()
    End Sub

    Public Sub ChangeUILanguage(ByVal ilang As Integer)

        Select Case ilang
            Case 0
                gMsgHandler.SelectedLanguage = enmLanguageType.eEnglish
                System.Globalization.CultureInfo.CurrentCulture.ClearCachedData()
                System.Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("en")
            Case 1
                gMsgHandler.SelectedLanguage = enmLanguageType.eTraditionalChinese
                System.Globalization.CultureInfo.CurrentCulture.ClearCachedData()
                System.Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("zh-TW")
            Case 2
                gMsgHandler.SelectedLanguage = enmLanguageType.eSimplifiedChinese
                System.Globalization.CultureInfo.CurrentCulture.ClearCachedData()
                System.Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("zh-CN")
        End Select

        gDOCollection.Load(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigDO.ini")
        gDICollection.Load(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigDI.ini")
        gAOCollection.Load(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigAO.ini")
        gAICollection.Load(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigAI.ini")

        'CloseForm(gfrmRecipe01)
        'CloseForm(gfrmRecipe02)
        'CloseForm(gfrmRecipe03)
        CloseForm(gfrmRecipe04)
        'CloseForm(gfrmRecipe06)
        CloseForm(gfrmSetAutoCalibration)
        CloseForm(gfrmCalibrationCCD2Valve1)
        CloseForm(gfrmCalibrationCCD2Valve2)
        CloseForm(gfrmCalibrationZHeight)
        CloseForm(gfrmHelp)

        CloseForm(gfrmCCDHandshake)
        CloseForm(gfrmEngineMode)
        CloseForm(gfrmDiagnosis)
        CloseForm(gfrmDiagnosisWetco)
        CloseForm(gfrmProgramControlLight)
        CloseForm(gfrmLightTowerConfig)
        CloseForm(gfrmLogin)
        CloseForm(gfrmManual)
        'CloseForm(gfrmRecipe06CoverRate)
        'CloseForm(gfrmRecipe06InspectionPR)
        CloseForm(gfrmSetLightConnection)
        CloseForm(gfrmSetCCDConnection)
        CloseForm(gfrmSetLaserReaderConnection)
        CloseForm(gfrmSetTriggerConnection)
        CloseForm(gfrmSetBalance)
        CloseForm(gfrmSetFMCS)
        CloseForm(gfrmRecipeStepParameter)
        CloseForm(gfrmSetUserLevel)
        CloseForm(gfrmSplashScreen)

        CloseForm(gfrmSystemSet)
        CloseForm(gfrmFeedback)
        CloseForm(gfrmWeight)
        CloseForm(gfrmPurge)
        CloseForm(gfrmClearGlue)

        CloseForm(gfrmIOList)
        CloseForm(gfrmIOTable)
        CloseForm(gfrmUIViewer)
       
        'gfrmRecipe01 = New frmRecipe01
        'gfrmRecipe02 = New frmRecipe02
        'gfrmRecipe03 = New frmRecipe03
        gfrmRecipe04 = New frmRecipe04
        'gfrmRecipe06 = New frmRecipe06
        gfrmSetAutoCalibration = New frmSetAutoCalibration
        'gfrmCalibrationCCD2FindHeight = New frmCalibrationCCD2FindHeight
        gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        gfrmCalibrationCCD2Valve2 = New frmCalibrationCCD2Valve2
        gfrmCalibrationZHeight = New frmCalibrationZHeight
        'gfrmOpStatus = New frmOpStatus
        'gfrmLog = New frmLog
        gfrmHelp = New frmHelp
        'gfrmAlarmList = New frmAlarmList

        gfrmCCDHandshake = New frmCCDHandshake
        gfrmEngineMode = New frmEngineMode
        gfrmDiagnosis = New frmDiag
        gfrmDiagnosisWetco = New frmDiagWetco
        gfrmProgramControlLight = New frmProgramControlLight
        gfrmLightTowerConfig = New frmLightTowerConfig
        gfrmLogin = New frmLogin
        gfrmManual = New frmManual
        'gfrmRecipe06CoverRate = New frmRecipe06CoverRate
        'gfrmRecipe06InspectionPR = New frmRecipe06InspectionPR
        gfrmSetLightConnection = New frmSetLightConnection
        gfrmSetCCDConnection = New frmSetCCDConnection
        gfrmSetLaserReaderConnection = New frmSetLaserReaderConnection
        gfrmSetValveController = New frmPicoValveControllerTest
        gfrmSetTriggerConnection = New frmSetTriggerConnection
        gfrmSetBalance = New frmSetBalance
        gfrmSetFMCS = New frmSetFMCSConnection
        gfrmRecipeStepParameter = New frmRecipeStepParameter
        'gfrmSetPurview = New frmSetPurview
        gfrmSetUserLevel = New frmSetUserLevel
        gfrmSplashScreen = New frmSplashScreen
        gfrmSystemSet = New frmSystemSet
        'gfrmUserAuth = New frmUserAuth
        gfrmFeedback = New frmFeedback
        gfrmWeight = New frmWeight
        gfrmPurge = New frmCalibrationPurge
        gfrmClearGlue = New frmCalibrationClearGlue
        gfrmSinglePattern = New frmSinglePattern
        gfrmIOList = New frmIOList
        gfrmIOTable = New frmIOTable
        gfrmUIViewer = New frmOperator
        ' Change gfrmMain UI language
        For Each ctl As Control In gfrmMain.Controls
            ApplyControlText(ctl, resMain)
        Next

        ' Update AI/AO/DI/DO names        

        '              gfrmMain.btnOpStatus.Text = resMain.GetString("btnOpStatus.Text")
        '              gfrmSystemSet.gpbValveType.Text = resSystemSet.GetString("gpbValveType.Text")
        '              gfrmSystemSet.Label8.Text = resSystemSet.GetString("Label8.Text")
        '              gfrmSystemSet.lblMachineType.Text = resSystemSet.GetString("lblMachineType.Text")
        '              gfrmEngineMode.btnIOList.Text = resEngineerMode.GetString("btnIOList.Text")

        'resources.ApplyResources(frmMain.btnOpStatus, "btnOpStatus")
    End Sub

    Private Sub ApplyControlText(ByRef control As Control, ByRef rm As System.ComponentModel.ComponentResourceManager)

        If control Is Nothing Then
            Throw New ArgumentNullException("control")
        End If
        If rm Is Nothing Then
            Throw New ArgumentNullException("rm")
        End If

        If TypeOf control Is Button Or TypeOf control Is RadioButton Then
            control.Text = rm.GetString(control.Name & ".Text")
        End If


        ' If TypeOf control Is MenuStrip Then
        'Dim menuStrip As MenuStrip = CType(control, MenuStrip)
        'For Each menuItem As ToolStripItem In menuStrip.Items
        'ApplyToolStripResource(MenuItem, rm)
        'Next
        'ElseIf TypeOf control Is ToolStrip Then
        'Dim toolStrip As ToolStrip = CType(control, ToolStrip)
        'For Each toolStriptItem As ToolStripItem In toolStrip.Items
        'ApplyToolStripResource(toolStriptItem, rm)
        'Next
        'ElseIf control.Controls.Count > 0 Then
        'For Each ctrl As Control In control.Controls
        'ApplyControlResource(ctrl, rm)
        'Next
        'End If
    End Sub


    ''' <summary>
    ''' 套用資源設定到選單或工具列
    ''' </summary>
    ''' <param name="toolStripItem">The tool strip item.</param>
    ''' <param name="rm">The ComponentResourceManager.</param>
    ''' <remarks></remarks>
    Private Sub ApplyToolStripResource(ByRef toolStripItem As ToolStripItem, ByRef rm As System.ComponentModel.ComponentResourceManager)
        If toolStripItem Is Nothing Then
            Throw New ArgumentNullException("toolStripItem")
        End If
        If rm Is Nothing Then
            Throw New ArgumentNullException("rm")
        End If

        rm.ApplyResources(toolStripItem, toolStripItem.Name)
        If TypeOf toolStripItem Is ToolStripMenuItem Then
            Dim toolStripMenuItem As ToolStripMenuItem = CType(toolStripItem, ToolStripMenuItem)
            For Each Item As ToolStripItem In toolStripMenuItem.DropDownItems
                ApplyToolStripResource(Item, rm)
            Next
        End If
    End Sub
End Module

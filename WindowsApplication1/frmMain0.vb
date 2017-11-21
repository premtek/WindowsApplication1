'為了將其他介面改成物件,避免建立失敗
Imports System.Globalization
Imports ProjectCore
Imports ProjectIO
Imports ProjectFeedback
Imports ProjectRecipe
Imports ProjectAOI
Imports ProjectLaserInterferometer
Imports ProjectTriggerBoard
Imports ProjectValveController
Imports MapData
Imports WetcoConveyor

Public Class frmMain0

    Private Sub frmMain0_Load(sender As Object, e As EventArgs) Handles Me.Load

        'System.Threading.ThreadPool.SetMinThreads(100, 100) 'Eason 20170221

        mGlobalPool.License = New clsLicens
        If Not (mGlobalPool.License.CheckLicens()) Then
            'MsgBox("License failed!!")'
            'End
        End If

        Me.Visible = False

        MachineName = GetMachineTypeFromFileToString()
        gSSystemParameter.LanguageType = CInt(ReadIniString("WorkSize", "LanguageType", Application.StartupPath & "\System\" & MachineName & "\SysParam.ini", 0))

        Select Case gSSystemParameter.LanguageType
            Case 0
                System.Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("en")
            Case 1
                System.Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("zh-TW")
            Case 2
                System.Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("zh-CN")
        End Select


        gSetUserLevel = New CUserLevel(MachineName) 'Soni + 2017.04.21 載入使用者權限資料庫

        gfrmMain = New frmMain
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
        gfrmUIViewer = New frmOperator
        gfrmDrawGraphics = New frmDrawGraphics
        gfrmMapData = New clsMapData

        'gfrmLog = New frmLog
        gfrmHelp = New frmHelp
        'gfrmAlarmList = New frmAlarmList

        gfrmCCDHandshake = New frmCCDHandshake
        gfrmEngineMode = New frmEngineMode
        gfrmDiagnosis = New frmDiag
        gfrmDiagnosisWetco = New frmDiagWetco
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
        gfrmProgramControlLight = New frmProgramControlLight
        'gfrmSetPurview = New frmSetPurview
        gfrmSetUserLevel = New frmSetUserLevel
        gfrmSplashScreen = New frmSplashScreen
        gfrmSystemSet = New frmSystemSet
        'gfrmUserAuth = New frmUserAuth
        gfrmFeedback = New frmFeedback
        ' gfrmWeight = New frmWeight
        gfrmPurge = New frmCalibrationPurge
        gfrmWeightPosition = New frmCalibrationFlowRate
        gfrmClearGlue = New frmCalibrationClearGlue
        'gfrmSinglePattern = New frmSinglePattern


        '20160901
        gfrmSetProcessTime = New frmSetProcessTime(frmRecipe04)

        'gfrmRecipe04Step = New frmRecipe04Step(frmRecipe04)

        gfrmRecipe04Step = New frmRecipe04Step(frmRecipe04, False, gRecipeEdit)   '20160820
        gfrmUIViewerPauseMotor = New frmOpMachine(gfrmUIViewer)
        'gfrmUIViewerSignalOperation = New frmUIViewerSignalOperation(frmUIViewer)

        'Eason 20170209 Ticket:100060 :Memory Log [S]
        gMeEventLog = New MyEventLog("D:\\MonitorLog\\")
        gMeEventLog.IsEnable = True ' 不要關掉這個

        gfrmMain.Show()
    End Sub
End Class
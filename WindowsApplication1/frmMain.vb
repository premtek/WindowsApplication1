﻿Imports Advantech.Motion
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading
Imports ProjectAOI
Imports ProjectRecipe.MCommonRecipe
Imports ProjectMotion
Imports ProjectIO
Imports ProjectRecipe
Imports ProjectCore
Imports ProjectFeedback
Imports ProjectConveyor
Imports ProjectTriggerBoard
Imports ProjectLaserInterferometer
Imports System.Diagnostics
Imports System.ComponentModel
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
'Imports ProjectAOI.AvVideoSDK.AvCtrlapi
'Imports ProjectAOI.AvVideoSDK
Imports Cognex.VisionPro
'Imports Cognex.VisionPro.ImageFile
Imports Cognex.VisionPro.ToolBlock
Imports Cognex.VisionPro.PMAlign
Imports ProjectValveController
Imports WetcoConveyor

Public Class frmMain

    Public myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))

    'Eason 20170209 Ticket:100060 :Memory Log
    Public mMemoryMonitor As ucMemoryMonitor


    ''' <summary>
    ''' 介面關閉, 物件清除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        '[說明]:關閉Thread
        Call gSystemThread.Abort()

        gAOICollection.Close()
        gLightCollection.Close()
        gBalanceCollection.Close() '關閉微量天平通訊

        '=== 為避免參數錯誤存檔, 關閉時不再存檔 ===
        gCMotion.Close() '關閉運動控制卡

        Call CloseIOCard() '關閉IO卡
        gEPVCollection.Close() '電空閥通訊關閉
        ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.Close() '雷射測高通訊關閉

        'Eason 20170209 Ticket:100060 :Memory Log [S]
        mMemoryMonitor.Close()
        mMemoryMonitor.Dispose()
        'Eason 20170209 Ticket:100060 :Memory Log [E]

    End Sub

    ''' <summary>防誤按, 關閉前確認</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing



        gDOCollection.SetState(enmDO.UnlockZAxis, False)    'Z軸剎車鎖住
        '--- Soni + 2014.09.29 機型切換 --- 
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.eDTS300A, enmMachineType.eDTS330A 'Main_FormClosing
                gDOCollection.SetState(enmDO.SystemOn, False)        'System Off 關機時關閉電源
        End Select
        '--- Soni + 2014.09.29 機型切換 ---
        If gSSystemParameter.RunMode = enmRunMode.Run Then '並將IO更新輸出
            Call gDOCollection.RefreshDO()
        End If

        If MessageBox.Show("Do you want exit program?", "My Application", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            IOUseTimer.Enabled = False
            ' Cancel the Closing event from closing the form.
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6000001), "INFO_6000001") '系統關閉.
            DisposeObject()
            Main_FormClosed(sender, Nothing)
            End
        Else
            IOUseTimer.Enabled = True
            gDOCollection.SetState(enmDO.UnlockZAxis, True)
            e.Cancel = True 'Back
        End If


    End Sub

    ''' <summary>發生Alarm時清除暫停</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Sub gEqpMsg_OnAlarmCanPause(sender As Object, e As EventArgs)
        gSYS(eSys.DispStage1).IsCanPause = True
        gSYS(eSys.DispStage2).IsCanPause = True
        gSYS(eSys.DispStage3).IsCanPause = True
        gSYS(eSys.DispStage4).IsCanPause = True
        'continueParam.IsProductMapNotFinish = True 'Soni + 2016.09.14 異常中斷時, 表示MAP未生產完成

    End Sub

    ''' <summary>關鍵項目 </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        gMsgHandler.Load(Application.StartupPath & "\EqpInitData\EqpAlarm.csv")
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6000000), "INFO_6000000") '系統啟動

        Dim intTheadStatus As System.Threading.ThreadState   '[Thead的執行狀態]
        Me.Width = Screen.PrimaryScreen.WorkingArea.Width    '尺寸與主畫面相同
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height  '尺寸與主畫面相同

        gfrmSplashScreen.Show()
        gfrmSplashScreen.Visible = True
        gfrmSplashScreen.Refresh()
       
        '[Note]:先抓取使用哪一種機型-->抓取硬體配置方式-->初始化
        MachineName = GetMachineTypeFromFileToString()
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6000002, MachineName), "INFO_6000002") '記錄機台機型設定
        Me.Text = "Machine Name:" & MachineName
        gSSystemParameter.LoadHardwareParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")

        '[Note]:根據機型抓取最大上限值(機台數量、Stage數量)，不需存讀檔，直接根據機型做判斷即可
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                gSSystemParameter.StageMax = eSys.DispStage2
                gSSystemParameter.MachineMax = eSys.MachineA
                gSSystemParameter.SubDispMax = eSys.SubDisp2
                gSSystemParameter.MonitorDispMax = eSys.MonitorDisp2
                gSSystemParameter.ConveyorMax = eSys.Conveyor1
                gSSystemParameter.StageCount = 2
                enmCCD.Max = enmCCD.CCD2
                
            Case enmMachineType.DCSW_800AQ
                gSSystemParameter.StageMax = eSys.DispStage4
                gSSystemParameter.MachineMax = eSys.MachineB
                gSSystemParameter.SubDispMax = eSys.SubDisp4
                gSSystemParameter.MonitorDispMax = eSys.MonitorDisp4
                gSSystemParameter.ConveyorMax = eSys.Conveyor1
                gSSystemParameter.StageCount = 4
                enmCCD.Max = enmCCD.CCD4

            Case enmMachineType.DCS_350A
                gSSystemParameter.StageMax = eSys.DispStage1
                gSSystemParameter.MachineMax = eSys.MachineA
                gSSystemParameter.SubDispMax = eSys.SubDisp1
                gSSystemParameter.MonitorDispMax = eSys.MonitorDisp1
                gSSystemParameter.ConveyorMax = eSys.Conveyor2
                gSSystemParameter.StageCount = 1
                enmCCD.Max = enmCCD.CCD1

            Case Else '單平台,?閥
                gSSystemParameter.StageMax = eSys.DispStage1
                gSSystemParameter.MachineMax = eSys.MachineA
                gSSystemParameter.SubDispMax = eSys.SubDisp1
                gSSystemParameter.MonitorDispMax = eSys.MonitorDisp1
                gSSystemParameter.ConveyorMax = eSys.Conveyor1
                gSSystemParameter.StageCount = 1
                enmCCD.Max = enmCCD.CCD1
        End Select


        Dim fileName As String
        fileName = System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigInterlock.ini"
        gInterlockCollection.Load(fileName) '硬體保護讀取


        '20170520
        fileName = System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\CalibrationValveParameter.ini"
        gCalibrationValveParameter.LoadPicoTouch(fileName) '校正閥體參數讀取
        gCalibrationValveParameter.LoadAdvanjet(fileName)


        fileName = Application.StartupPath & "\System\" & MachineName & "\Index.ini"
        If Not File.Exists(fileName) Then
            SetIOIndex()
            enmAxis.SaveAxisIndex(fileName)
            enmAI.SaveAIIndex(fileName)
            enmAO.SaveAOIndex(fileName)
            enmDI.SaveDIIndex(fileName)
            enmDO.SaveDOIndex(fileName)
            enmCCD.SaveAOIIndex(fileName)
        Else
            enmAxis.LoadAxisIndex(fileName)
            enmAI.LoadAIIndex(fileName)
            enmAO.LoadAOIndex(fileName)
            enmDI.LoadDIIndex(fileName)
            enmDO.LoadDOIndex(fileName)
            enmCCD.LoadAOIIndex(fileName)
        End If

        'Eason 20170303 Ticket:100100 , XY Offset from CSV File
        For index = enmStage.No1 To gSSystemParameter.StageCount - 1
            gStageOffsetFromSCV(index) = New CStageCalibrationOffsetFromSCV
        Next

        If enmAxis.Max < 0 Then
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002000), "Error_1002000", eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002000), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End
        End If
        ReDim gCMotion.AxisParameter(enmAxis.Max)

        '[說明]:關閉防火牆
        Shell("netsh advfirewall set currentprofile state off")
        gfrmSplashScreen.AddProgress(5)

        '[說明]:程式版本
        gfrmHelp.lblProgramVersion.Text = "Version: " & System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString()
        gSyslog.Save(gMsgHandler.GetMessage(INFO_6000003, System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString()), "INFO_6000003") '軟體執行版本記錄
        gfrmSplashScreen.AddProgress(5)

        '[說明]:登入者等級
        gUserLevel = enmUserLevel.eOperator

        InitDataBase()        '讀取資料庫

        '[說明]:讀檔&存檔 在有新增參數時，讀完檔之後就立刻就檔案的寫入(ini檔)
        gSSystemParameter.ReadSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini") '不含機台型號
        gMsgHandler.SelectedLanguage = gSSystemParameter.LanguageType

        gSSystemParameter.LoadCounter("D:\PIIData\SysCounter.ini") '讀取系統計數器
        gSSystemParameter.StableTime.Load(Application.StartupPath & "\System\" & MachineName & "\SysStable.ini") '讀取穩定時間(by機型)
        gSSystemParameter.LoadSystemPos(Application.StartupPath & "\System\" & MachineName & "\SysPos.ini") '讀取系統位置(by機型)
        gSSystemParameter.LoadAuthority(Application.StartupPath & "\System\" & MachineName & "\SysAuthority.ini") '讀取使用者權限(by機型)
        gSSystemParameter.LoadCCDScale(Application.StartupPath & "\System\" & MachineName & "\SysCCD.ini")
        gSSystemParameter.LoadVelocity(Application.StartupPath & "\system\" & MachineName & "\SysParam.ini") '儲存系統閥參數(by機型)


        gSSystemParameter.SaveCCDSCale(Application.StartupPath & "\System\" & MachineName & "\SysCCD.ini")
        gSSystemParameter.LoadCCDTargetData(Application.StartupPath & "\system\" & MachineName & "\SysCCDTarget.ini") '20171026wenda 

        gfrmSplashScreen.AddProgress(5)

        gSSystemParameter.CoordType = enmCoordinateRelationType.eDTS
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        gSSystemParameter.SaveCounter("D:\PIIData\SysCounter.ini") '儲存系統計數器
        gSSystemParameter.StableTime.Save(Application.StartupPath & "\System\" & MachineName & "\SysStable.ini") '儲存穩定時間(by機型)
        gSSystemParameter.SaveSystemPos(Application.StartupPath & "\System\" & MachineName & "\SysPos.ini") '儲存系統位置(by機型)
        gSSystemParameter.SaveAuthority(Application.StartupPath & "\System\" & MachineName & "\SysAuthority.ini") '儲存使用者權限(by機型)

        gfrmSplashScreen.AddProgress(5)


        If gSysAdapter.SetSysParam = False Then
            MsgBox("System Adapter Error", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
        AddHandler gEqpMsg.OnAlarmCanPause, AddressOf gEqpMsg_OnAlarmCanPause

        gSyslog.Save(gMsgHandler.GetMessage(INFO_6000023, GetCCDTypeString(gAOICollection.GetCCDType(enmCCD.CCD1))), "INFO_6000023") 'CCD Type:{0}

        gAOICollection.Initial() '=== Soni + 2015.04.23 AOI初始化 ===
        AddHandler gAOICollection.OnRunSuccessed, AddressOf gAOICollection_OnRunSuccessed
        AddHandler gEqpStatusHandler.OnIndicatorChanged, AddressOf gEqpStatusHandler_OnIndicatorChanged
        gfrmSplashScreen.AddProgress(5)
        Call gLightCollection.Initialize_Light() 'Soni + 2014.11.11 程控光源初始化
        gfrmSplashScreen.AddProgress(5)
        '--- Soni + 2016.06.18 增加開機時開燈 以校正用參數為基準---
        For mCCDNo As Integer = 0 To enmCCD.Max
            Dim mScene As String = "CALIB" & (mCCDNo + 1).ToString
            Dim mFileName As String = Application.StartupPath & "\System\" & MachineName & "\CALIB" & (mCCDNo + 1).ToString & ".ini"
            gAOICollection.LoadSceneParameter(mScene, mFileName)
            Dim light As enmLight
            If gAOICollection.SceneDictionary.ContainsKey(mScene) Then
                If gAOICollection.SceneDictionary(mScene).LightEnable(enmValveLight.No1) = True Then
                    Dim mLightValue1 As Integer = gAOICollection.SceneDictionary(mScene).LightValue(enmValveLight.No1)
                    light = gSysAdapter.CCDLightMapping(mCCDNo, enmValveLight.No1)
                    Call gLightCollection.SetCCDLight(mCCDNo, light, mLightValue1, True)
                    Call gSysAdapter.SetLightOnOff(light, True)
                End If
                If gAOICollection.SceneDictionary(mScene).LightEnable(enmValveLight.No2) = True Then
                    Dim mLightValue2 As Integer = gAOICollection.SceneDictionary(mScene).LightValue(enmValveLight.No2)
                    light = gSysAdapter.CCDLightMapping(mCCDNo, enmValveLight.No2)
                    Call gLightCollection.SetCCDLight(mCCDNo, light, mLightValue2, True)
                    Call gSysAdapter.SetLightOnOff(light, True)
                End If
                If gAOICollection.SceneDictionary(mScene).LightEnable(enmValveLight.No3) = True Then
                    Dim mLightValue3 As Integer = gAOICollection.SceneDictionary(mScene).LightValue(enmValveLight.No3)
                    light = gSysAdapter.CCDLightMapping(mCCDNo, enmValveLight.No3)
                    Call gLightCollection.SetCCDLight(mCCDNo, light, mLightValue3, True)
                    Call gSysAdapter.SetLightOnOff(light, True)
                End If
                If gAOICollection.SceneDictionary(mScene).LightEnable(enmValveLight.No4) = True Then
                    Dim mLightValue4 As Integer = gAOICollection.SceneDictionary(mScene).LightValue(enmValveLight.No4)
                    light = gSysAdapter.CCDLightMapping(mCCDNo, enmValveLight.No4)
                    Call gLightCollection.SetCCDLight(mCCDNo, light, mLightValue4, True)
                    Call gSysAdapter.SetLightOnOff(light, True)
                End If
            End If

        Next
        '--- Soni + 2016.06.18 增加開機時開燈 以校正用參數為基準---
        Initial_StageMap()
        gfrmSplashScreen.AddProgress(5)

        '--- IO卡初始化 ---
        Call InitialIOCard() 'IO卡初始化
        gfrmSplashScreen.AddProgress(5)
        gDOCollection.SetState(enmDO.StartButtonLight, True) 'Perry要求, 開機時亮燈, 提醒使用者已啟動
        gDOCollection.SetState(enmDO.PauseButtonLight, True) 'Perry要求, 開機時亮燈, 提醒使用者已啟動
        gDOCollection.SetState(enmDO.StartButtonLight2, True) 'Perry要求, 開機時亮燈, 提醒使用者已啟動
        gDOCollection.SetState(enmDO.PauseButtonLight2, True) 'Perry要求, 開機時亮燈, 提醒使用者已啟動

        '--- IO卡初始化 ---

        '[說明]:丟初始狀態
        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None
        gSYS(eSys.OverAll).Act(eAct.AutoRun).RunStatus = enmRunStatus.None
        gSYS(eSys.DispStage1).Act(eAct.ChangeGlue).RunStatus = enmRunStatus.None
        gSYS(eSys.DispStage1).Act(eAct.ClearGlue).RunStatus = enmRunStatus.None
        gSYS(eSys.DispStage1).Act(eAct.Purge).RunStatus = enmRunStatus.None

        '[說明]:程式只要重啟，Purge、Clear Glue就都要重新計算

        Select Case gSSystemParameter.RunMode
            Case enmRunMode.Simulation
                gSyslog.Save("Run Mode: Virtual System(Simulation).")

            Case enmRunMode.Run
                gSyslog.Save("Run Mode: Real System(Production).")
                gfrmSplashScreen.AddProgress(5)
                Call InitialMotionCard() '運動控制卡初始化

        End Select

        'TODO: 置換為Premtek.Base版本失敗, 維持舊版本
        ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.CardFileName = Application.StartupPath & "\System\" & MachineName & "\CardLaserReader.ini"
        ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.Initial() '干涉儀連線

        ''***************************Trigger Board********************************
        Initialize_TriggerBoard()
        For mI As Int16 = enmStage.No1 To gSSystemParameter.StageCount - 1
            gTriggerBoard.TimeoutTimes(mI) = gTriggerBoard.TBConnectionParameter(mI).TriggerBoard30.TimeOutTimes

            gTriggerBoardVersion(mI) = ""

            ''20171010   暫時移除V命令 詢問版本
            'If gTriggerBoard.GetVersion(mI, True) = False Then
            '    gSyslog.Save(gMsgHandler.GetMessage(Error_1016005), "Error_1016005", eMessageLevel.Error)
            '    gSyslog.Save("Error Message: " & gTriggerBoard.ErrMsg(mI), , eMessageLevel.Error)
            '    gEqpMsg.AddHistoryAlarm("Error_1016005", "Main Load", , gMsgHandler.GetMessage(Error_1016005))      '[Trigger Board Firmware Information Fail!]
            'Else
            '    gTriggerBoardVersion(mI) = gTriggerBoard.Version(mI).Value
            '    'gfrmHelp.lblJettingTriggerVersion.Text = "Jetting Version: " & gTriggerBoardVersion(mI)
            '    gSyslog.Save(gMsgHandler.GetMessage(INFO_6000004, gTriggerBoardVersion(mI)), "INFO_6000004")
            'End If
        Next
        '*************************************************************************

        ''***************************Valve Controller********************************
        Call InitialValveController()
        For mI As Int16 = enmStage.No1 To gSSystemParameter.StageCount - 1
            gValvecontrollerCollection.TimeoutTimes(mI) = gValvecontrollerCollection.ConnectionParameter(mI).TimeOutTimes
        Next
        ''***************************************************************************
        gfrmSplashScreen.AddProgress(5)


        gDOCollection.SetState(enmDO.SystemOn, True)   'DO-系統啟動
        gDOCollection.SetState(enmDO.Station2Unlock, True)

        gInterlockCollection.ResetHardwardAlarm()
        gfrmSplashScreen.AddProgress(5)

        '********************************Servo On & IO Set & Home Set ***********************************
        Initial_Motion()
        gfrmSplashScreen.AddProgress(5)
        '***********************************************************************************************

        gEqpStatusHandler.LoadMessageParam(System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\EqpStatusHandler.ini") '燈號設定
        gEqpStatusHandler.ThreadStart()
        gfrmSplashScreen.AddProgress(5)

        '--- Soni + 2014.10.22 微量天平初始化 ---
        gBalanceCollection.LoadBalanceConnectionParameter(Application.StartupPath & "\System\" & MachineName & "\CardBalance.ini") '讀取天平參數
        gBalanceCollection.SaveBalanceConnectionParameter(Application.StartupPath & "\System\" & MachineName & "\CardBalance.ini") '儲存天平參數
        Initialize_Balance()
        gfrmSplashScreen.AddProgress(5)
        '--- Soni + 2014.10.22 微量天平初始化 ---

        gFMCSCollection.LoadFMCSConnectionParameter(Application.StartupPath & "\System\" & MachineName & "\CardFMCS.ini") '讀取FMCS連線參數
        gFMCSCollection.SaveFMCSConnectionParameter(Application.StartupPath & "\System\" & MachineName & "\CardFMCS.ini") '儲存FMCS連線參數
        Call Initial_FMCS() 'Soni + 2014.11.11 FMCS初始化
        gfrmSplashScreen.AddProgress(5)

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.eDTS330A
                LoadConveyorConnectionParameter(Application.StartupPath & "\System\" & MachineName & "\CardConveyor.ini") '讀取Conveyor連線參數
                SaveConveyorConnectionParameter(Application.StartupPath & "\System\" & MachineName & "\CardConveyor.ini") '儲存Conveyor連線參數
                Call Initial_PLC() 'Soni + 2014.11.10 初始化PLC
                gfrmSplashScreen.AddProgress(5)
        End Select

        '=== 四點校正參數 ===
        For stageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
            gSSystemParameter.Pos.CCDLaserCalibration(stageNo) = New CCDLaserCalibration
            gSSystemParameter.Pos.CCDTiltVavleCalbration(stageNo) = New CCCDTiltValveCalibration
            gSSystemParameter.Pos.LaserTiltValveCalbration(stageNo) = New CLaserTiltValveCalibration
            gSSystemParameter.Pos.PurgeCalibration(stageNo) = New CPurgeCalibration
            gSSystemParameter.Pos.WeightCalibration(stageNo) = New CWeightCalibration
            gSSystemParameter.Pos.ChangePotCalibration(stageNo) = New CChangePotCalibration
            gSSystemParameter.Pos.CleanValveCalibration(stageNo) = New CCleanValveCalibration
            gSSystemParameter.StageParts.ValveData(stageNo) = New CValveData
            gSSystemParameter.StageParts.PasteLifeTime(stageNo) = New CPasteLifeTime
            gSSystemParameter.StageParts.Purge(stageNo) = New CInpectionCondition
            gSSystemParameter.StageParts.FlowRate(stageNo) = New CInpectionCondition
            gSSystemParameter.Pos.StageVerification(stageNo) = New CStageVerification '平台精準度驗證
            gSSystemParameter.Pos.CCDLaserCalibration(stageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (stageNo + 1).ToString & ".ini") 'CCD對雷射校正檔
            gSSystemParameter.Pos.CCDTiltVavleCalbration(stageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (stageNo + 1).ToString & ".ini") 'Soni + 2016.09.09
            gSSystemParameter.Pos.LaserTiltValveCalbration(stageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (stageNo + 1).ToString & ".ini") 'Soni + 2016.09.09
            gSSystemParameter.Pos.PurgeCalibration(stageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (stageNo + 1).ToString & ".ini")
            gSSystemParameter.Pos.WeightCalibration(stageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (stageNo + 1).ToString & ".ini")
            gSSystemParameter.Pos.ChangePotCalibration(stageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (stageNo + 1).ToString & ".ini")
            gSSystemParameter.Pos.CleanValveCalibration(stageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (stageNo + 1).ToString & ".ini")
            gSSystemParameter.StageParts.ValveData(stageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (stageNo + 1).ToString & ".ini")
            '[Note]:膠材壽命是獨立存放的 目的是如果切換程式時,壽命計時是延續的
            gSSystemParameter.StageParts.PasteLifeTime(stageNo).Load("D:\PIIData\SysConfigStage" & (stageNo + 1).ToString & ".ini")
            gSSystemParameter.StageParts.Purge(stageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (stageNo + 1).ToString & ".ini", "Purge")
            gSSystemParameter.StageParts.FlowRate(stageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (stageNo + 1).ToString & ".ini", "FlowRate")
            gSSystemParameter.Pos.StageVerification(stageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (stageNo + 1).ToString & ".ini") '平台精準度驗證
        Next

        gCRecipe = New CRecipe(gSSystemParameter.StageCount, gSSystemParameter.StageUseValveCount)
        gRecipeEdit = New CRecipe(gSSystemParameter.StageCount, gSSystemParameter.StageUseValveCount)

        '=== 四點校正參數 ===
        


        '*****************************************************************************************
        '************************************安全區位置*******************************************
        '*****************************************************************************************
        For mStageNo As Integer = 0 To gSSystemParameter.StageCount - 1
            gSSystemParameter.Pos.SafeRegion(mStageNo) = New CSafeRegion
            gSSystemParameter.Pos.SafeRegion(mStageNo).Load(Application.StartupPath & "\System\" & MachineName & "\ConfigSafeRegion" & (mStageNo + 1).ToString & ".ini")
            gSSystemParameter.Pos.SafeRegion(mStageNo).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigSafeRegion" & (mStageNo + 1).ToString & ".ini")
        Next

        '***********************************Fenix+ 2015/12/11*****************************************************
        Try
            '建立流程資料
            For i = enmStage.No1 To gSSystemParameter.StageCount - 1
                Dim WorkPiece As New CRecipePattern("Stage" & (i + 1).ToString())
                'gPattern.Add(WorkPiece)
            Next
            For i = enmMachineStation.MachineA To enmMachineStation.MaxMachine
                gMapData(i) = New MapData.clsMapData
            Next
            'Debug.Print(IsNothing(gPattern).ToString())
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        mGlobalPool.Unit = New clsUnit
        mGlobalPool.cls800AQ_LUL = New cls800AQLul

        'Derek + 2015/04/21 讀取Conveyor參數 
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.eDTS330A
                Dim frmConveyorA As New frmConveyor
                frmConveyorA.ReadConveyorParameter()        '讀取參數
                ConveyorA.ConveryorName = "A"               '訂定Convery名稱
                genmConveyorStatus = enmRunStatus.None      '狀態設定為None

            Case enmMachineType.DCSW_800AQ
                mGlobalPool.CvSMEMA.Clear()
                mGlobalPool.CvSMEMA.Add(New cls800aqSMEMA)
                mGlobalPool.cls800AQ_LUL.IsLoaderPass = gSSystemParameter.PassLUL
                mGlobalPool.cls800AQ_LUL.IsUnloaderPass = gSSystemParameter.PassLUL

            Case enmMachineType.DCS_500AD
                mGlobalPool.CvSMEMA.Clear()
                mGlobalPool.CvSMEMA.Add(New cls500sdSMEMA)
                mGlobalPool.cls800AQ_LUL.IsLoaderPass = gSSystemParameter.PassLUL
                mGlobalPool.cls800AQ_LUL.IsUnloaderPass = gSSystemParameter.PassLUL

            Case enmMachineType.DCS_350A
                Dim loadDirection As IRoller.enmDirection = IIf(gSSystemParameter.LoadDirection = True, IRoller.enmDirection.Forward, IRoller.enmDirection.Reversal)
                Dim unloadDirection As IRoller.enmDirection = IIf(gSSystemParameter.UnloadDirection = True, IRoller.enmDirection.Forward, IRoller.enmDirection.Reversal)
                Unit.A_Roller.SetDirection(loadDirection, unloadDirection)
                Unit.B_Roller.SetDirection(loadDirection, unloadDirection)

                mGlobalPool.CvSMEMA.Clear()
                mGlobalPool.CvSMEMA.Add(New clsCv1SMEMA)
                mGlobalPool.CvSMEMA.Add(New clsCv2SMEMA)
                gConveyor350A.MotionTimeOut = gSSystemParameter.TimeOut4
                gConveyor350A.SmemaTimeOut = gSSystemParameter.TimeOut6

        End Select
        mGlobalPool.Unit.TempController.StartWatcher()
        Dim iniPath As String = System.Windows.Forms.Application.StartupPath & "\system\" & MachineName & "\PidController.ini"
        mGlobalPool.AlarmRange = Convert.ToInt32(ReadIniString("AlarmRange", "Range", iniPath))
        SetElectricCylinder()
        SetVacuum()

        Call gfrmLogin.ShowDialog()
        btnRecipe.Enabled = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.Recipe), gUserLevel)
        btnCalibration.Enabled = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.Calibration), gUserLevel)
        btnEngineMode.Enabled = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.EngineMode), gUserLevel)
        Me.Text = MachineTypeToString(gSSystemParameter.MachineType)
        IOUseTimer.Enabled = True


        'Eason 20170209 Ticket:100060 :Memory Log [S]
        mMemoryMonitor = New ucMemoryMonitor()
        AddHandler mMemoryMonitor.NotifyRuntimeMessageHandle, AddressOf gMeEventLog.Log
        mMemoryMonitor.ScanIntervalTime = 1000
        mMemoryMonitor.SetMemoryThresholdValue = 1024 * 1000 * 10 '(10MB)   'Note : 1024 * 1000 = 1MB
        mMemoryMonitor.SetThreadThresholdValue = 2
        mMemoryMonitor.Open()
        'gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
        gMeEventLog.Log("[EVENT] , Main Load Finish")
        'Eason 20170209 Ticket:100060 :Memory Log [E]

        '20171115
        gfrmSplashScreen.Visible = False

        'gfrmAOIDebug.StartPosition = FormStartPosition.Manual
        'gfrmAOIDebug.Location = New Point(0, 0)
        'gfrmAOIDebug.Show()

        '=== 執行緒運行置底 避免物件未建立即Polling ===
        If gSSystemParameter.RunMode = enmRunMode.Run Then
            'Select Case gSSystemParameter.MachineType 'TODO: Revised from MachineType to MachineCount...etc
            '    Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
            '        mSystem = New Premtek.CSystem0Top(1, 2, gCMotion, gAOICollection, gEqpMsg, gDICollection, gDOCollection)
            '    Case enmMachineType.DCSW_800AQ
            '        mSystem = New Premtek.CSystem0Top(2, 2, gCMotion, gAOICollection, gEqpMsg, gDICollection, gDOCollection)
            '    Case enmMachineType.DCS_350A
            '        mSystem = New Premtek.CSystem0Top(1, 1, gCMotion, gAOICollection, gEqpMsg, gDICollection, gDOCollection)
            '    Case Else '單平台,?閥
            '        mSystem = New Premtek.CSystem0Top(1, 1, gCMotion, gAOICollection, gEqpMsg, gDICollection, gDOCollection)
            'End Select
            '[說明]:啟動Thead
            intTheadStatus = gSystemThread.ThreadState
            If intTheadStatus = Threading.ThreadState.Unstarted Then
                gSystemThread.Name = "CSystemThread"
                gSystemThread.Start()
                gSystemThread.Priority = Threading.ThreadPriority.Highest
                gSystemThread.IsBackground = True 'False
            End If

            '[Note]:啟動監控
            For mStage = eSys.MonitorDisp1 To gSSystemParameter.MonitorDispMax
                gSYS(mStage).Command = eSysCommand.Monitor
            Next
        End If

        '=== 執行緒運行置底 避免物件未建立即Polling ===
        'jimmy 20170725
        If gSSystemParameter.MachineType <> enmMachineType.DCSW_800AQ Then
            btnAirPressure.Visible = False
        Else
            btnAirPressure.Visible = True
        End If
    End Sub

    ''' <summary>介面燈號變更動作</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gEqpStatusHandler_OnIndicatorChanged(ByVal sender As Object, ByVal e As IndicatorEventArgs)
        If gfrmUIViewer Is Nothing Then
            Exit Sub
        End If

        Select Case e.indicator
            Case eIndicator.Red
                gfrmUIViewer.palRedLight.BackgroundImage = IIf(e.value, My.Resources.li_08, My.Resources.li_23)
            Case eIndicator.Yellow
                gfrmUIViewer.palYellowLight.BackgroundImage = IIf(e.value, My.Resources.li_12, My.Resources.li_23)
            Case eIndicator.Green
                gfrmUIViewer.palGreenLight.BackgroundImage = IIf(e.value, My.Resources.li_03, My.Resources.li_23)
        End Select
    End Sub


    ' ''' <summary>Bitmap存檔 使用記憶體串流 Soni + 2016.10.02</summary>
    ' ''' <param name="bitmap"></param>
    ' ''' <param name="fileName"></param>
    ' ''' <remarks></remarks>
    'Async Sub SaveBitmap(ByVal bitmap As Bitmap, ByVal fileName As String)
    '    Await Task.Run(Sub()
    '                       Dim oMS As System.IO.MemoryStream = New System.IO.MemoryStream()
    '                       bitmap.Save(oMS, Imaging.ImageFormat.Bmp)
    '                       Dim oFS As System.IO.FileStream = System.IO.File.Open(fileName, System.IO.FileMode.OpenOrCreate)
    '                       oFS.Write(oMS.ToArray(), 0, oMS.ToArray().Length)
    '                       oFS.Close()
    '                   End Sub)
    'End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Image"></param>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Async Sub SaveBitmap(ByVal Image As ICogImage, ByVal fileName As String)
        Await Task.Run(Sub()
                           Dim bitmap As Bitmap
                           bitmap = Image.ToBitmap()
                           Dim oMS As System.IO.MemoryStream = New System.IO.MemoryStream()
                           bitmap.Save(oMS, Imaging.ImageFormat.Bmp)
                           Dim oFS As System.IO.FileStream = System.IO.File.Open(fileName, System.IO.FileMode.OpenOrCreate)
                           oFS.Write(oMS.ToArray(), 0, oMS.ToArray().Length)
                           oFS.Close()
                       End Sub)
    End Sub

    Dim mCCDOnRunStopWatch As New Stopwatch
    ''' <summary>CCD介面更新動作</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Sub gAOICollection_OnRunSuccessed(sender As Object, e As AOIEventArgs)
        '[Note]順序調整:因需將顯示影像打成UI緒顯示(不占據流程時間)，又必須清除記憶體
        '1.先將必須儲存的影像複製 ,再打出去存檔
        '2.再將顯示UI緒打出去，於UI緒最後清除記憶體
        If Not gfrmUIViewer Is Nothing Then
            If Not gfrmUIViewer.IsDisposed Then
                'SyncLock gCCDAlignResultDict
                mCCDOnRunStopWatch.Restart()

                'D:\PIIData\YYYYMMDD\
                Dim folderName As String = gSSystemParameter.CCDImageFolderPath & "\" & Now.Year.ToString("0000") & Now.Month.ToString("00") & Now.Day.ToString("00") & "\"
                If Not System.IO.Directory.Exists(folderName) Then
                    System.IO.Directory.CreateDirectory(folderName) '建立資料夾
                End If

                '儲存來源影像
                'D:\PIIData\YYYYMMDD\YYYYMMDD_Scene_HHmmss.bmp
                Dim fileName As String = folderName & Now.Year.ToString("0000") & Now.Month.ToString("00") & Now.Day.ToString("00") & Now.Hour.ToString("00") & Now.Minute.ToString("00") & Now.Second.ToString("00") & "_" & e.Ticket & "_" & e.SceneName & ".bmp"
                Dim greyImg As CogImage8Grey
                Dim colorImg As CogImage24PlanarColor
                'Dim bmp As Bitmap
                Select Case gSSystemParameter.CCDImageSaveMode
                    Case eCCDImageProcess.None
                    Case eCCDImageProcess.SaveAll '全部儲存
                        If Not gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image Is Nothing Then
                            If TypeOf gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image Is CogImage8Grey Then 'Mono
                                gSyslog.CCDSave("gAOICollection_OnRunSuccessed 2:" & mCCDOnRunStopWatch.ElapsedMilliseconds & "ms", CSystemLog.eCCDMessageProcess.Add)
                                greyImg = CType(gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image, CogImage8Grey) 'Soni + 2016.09.22 轉型別
                                'bmp = greyImg.ToBitmap() 'Soni + 2016.09.22 轉bmp '移至SaveBitmap處理
                                gSyslog.CCDSave("gAOICollection_OnRunSuccessed 3:" & mCCDOnRunStopWatch.ElapsedMilliseconds & "ms", CSystemLog.eCCDMessageProcess.Add)
                                SaveBitmap(greyImg, fileName) 'Soni + 2016.09.22 bmp儲存
                                gSyslog.CCDSave("gAOICollection_OnRunSuccessed 4:" & mCCDOnRunStopWatch.ElapsedMilliseconds & "ms", CSystemLog.eCCDMessageProcess.Add)
                            ElseIf TypeOf gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image Is CogImage24PlanarColor Then 'Color
                                colorImg = CType(gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image, CogImage24PlanarColor) 'Soni + 2016.09.22 轉型別
                                'bmp = colorImg.ToBitmap 'Soni + 2016.09.22 轉bmp '移至SaveBitmap處理
                                SaveBitmap(colorImg, fileName) 'Soni + 2016.09.22 bmp儲存
                            End If
                        End If
                    Case eCCDImageProcess.SaveNG '只存NG
                        If gCCDAlignResultDict(e.CCDNo)(e.Ticket).Result.Count = 0 Then '比對結果為0,NG
                            If Not gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image Is Nothing Then
                                If TypeOf gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image Is CogImage8Grey Then 'Mono
                                    greyImg = CType(gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image, CogImage8Grey) 'Soni + 2016.09.22 轉型別
                                    'bmp = greyImg.ToBitmap() 'Soni + 2016.09.22 轉bmp
                                    SaveBitmap(greyImg, fileName) 'Soni + 2016.09.22 bmp儲存
                                ElseIf TypeOf gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image Is CogImage24PlanarColor Then 'Color
                                    colorImg = CType(gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image, CogImage24PlanarColor) 'Soni + 2016.09.22 轉型別
                                    'bmp = colorImg.ToBitmap 'Soni + 2016.09.22 轉bmp
                                    SaveBitmap(colorImg, fileName) 'Soni + 2016.09.22 bmp儲存
                                End If
                            End If
                        ElseIf gCCDAlignResultDict(e.CCDNo)(e.Ticket).Result.Count > 1 Then '比對結果太多,NG
                            If Not gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image Is Nothing Then
                                If TypeOf gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image Is CogImage8Grey Then 'Mono
                                    greyImg = CType(gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image, CogImage8Grey) 'Soni + 2016.09.22 轉型別
                                    'bmp = greyImg.ToBitmap() 'Soni + 2016.09.22 轉bmp
                                    SaveBitmap(greyImg, fileName) 'Soni + 2016.09.22 bmp儲存
                                ElseIf TypeOf gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image Is CogImage24PlanarColor Then 'Color
                                    colorImg = CType(gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image, CogImage24PlanarColor) 'Soni + 2016.09.22 轉型別
                                    'bmp = colorImg.ToBitmap 'Soni + 2016.09.22 轉bmp
                                    SaveBitmap(colorImg, fileName) 'Soni + 2016.09.22 bmp儲存
                                End If
                            End If
                        ElseIf gCCDAlignResultDict(e.CCDNo)(e.Ticket).Result(0).SkipMark = True Then 'Skip Mark存在 NG
                            If Not gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image Is Nothing Then
                                If TypeOf gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image Is CogImage8Grey Then 'Mono
                                    greyImg = CType(gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image, CogImage8Grey) 'Soni + 2016.09.22 轉型別
                                    'bmp = greyImg.ToBitmap() 'Soni + 2016.09.22 轉bmp
                                    SaveBitmap(greyImg, fileName) 'Soni + 2016.09.22 bmp儲存
                                ElseIf TypeOf gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image Is CogImage24PlanarColor Then 'Color
                                    colorImg = CType(gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image, CogImage24PlanarColor) 'Soni + 2016.09.22 轉型別
                                    'bmp = colorImg.ToBitmap 'Soni + 2016.09.22 轉bmp
                                    SaveBitmap(colorImg, fileName) 'Soni + 2016.09.22 bmp儲存
                                End If
                            End If
                        End If
                End Select
                'End SyncLock
                gSyslog.CCDSave("gAOICollection_OnRunSuccessed 5:" & mCCDOnRunStopWatch.ElapsedMilliseconds & "ms", CSystemLog.eCCDMessageProcess.Add)

                Select Case e.CCDNo
                    Case 0
                        gAOICollection.ShowAlignResult(e.CCDNo, e.Ticket, gfrmUIViewer.UcDisplay1)
                    Case 1
                        gAOICollection.ShowAlignResult(e.CCDNo, e.Ticket, gfrmUIViewer.UcDisplay2)
                    Case 2
                        gAOICollection.ShowAlignResult(e.CCDNo, e.Ticket, gfrmUIViewer.UcDisplay3)
                    Case 3
                        gAOICollection.ShowAlignResult(e.CCDNo, e.Ticket, gfrmUIViewer.UcDisplay4)
                End Select
                gSyslog.CCDSave("gAOICollection_OnRunSuccessed 1:" & mCCDOnRunStopWatch.ElapsedMilliseconds & "ms", CSystemLog.eCCDMessageProcess.Add)

            End If

        End If

        ''Eason 20170228 Ticket:100035 , Free Memory [S] 不管存不存圖都要清除原本的
        'gCCDAlignResultDict(e.CCDNo)(e.Ticket).Image = Nothing 'Soni + 2016.09.09 顯示後, 清除影像 '[Note]移至ShowAlignResult最後處理
        'gCCDAlignResultDict(e.CCDNo)(e.Ticket).CogPMAlignResults = Nothing 'Soni + 2016.09.09 顯示後, 清除影像結果 '[Note]移至ShowAlignResult最後處理
        'gCCDAlignResultDict(e.CCDNo)(e.Ticket).CogFindCircleResults = Nothing 'Soni + 2016.09.09 顯示後, 清除影像結果 '[Note]移至ShowAlignResult最後處理
        ''Eason 20170228 Ticket:100035 , Free Memory [E]
        gSyslog.CCDSave("gAOICollection_OnRunSuccessed 6:" & mCCDOnRunStopWatch.ElapsedMilliseconds & "ms", CSystemLog.eCCDMessageProcess.Add)
    End Sub

    Private Sub frmOperation_Activated(sender As Object, e As EventArgs) Handles Me.Activated

        'Dim mStatus As AvErrorCode = AvErrorCode.Success

        '[說明]:離開的時候，就關閉Timer，進入就打開
        TmrSystemState.Enabled = True


        '[說明]:使用者資料
        btnLogOut.Text = GetAuthorityName()
    End Sub

    Private Sub frmOperation_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate

        'Dim mStatus As AvErrorCode = AvErrorCode.Success

        '[說明]:離開的時候，就關閉Timer，進入就打開
        TmrSystemState.Enabled = False

    End Sub



#Region "Interlock相關"

    'Public Sub SetbtnAlarmBorderColor(ByVal index As System.Drawing.Color)
    '    Select Case index
    '        Case Color.Green
    '            btnAlarmList.FlatAppearance.BorderColor = Color.Blue
    '        Case Color.Yellow
    '            btnAlarmList.FlatAppearance.BorderColor = Color.Yellow
    '        Case Color.Red
    '            btnAlarmList.FlatAppearance.BorderColor = Color.Red
    '    End Select
    'End Sub

    ' ''' <summary>A機流程異常判定條件</summary>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function IsMachineASoftwareInterlock() As Boolean
    '    If Not gSYS(eSys.MachineA) Is Nothing Then
    '        If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Alarm Then
    '            Return True
    '        End If
    '    End If
    '    For mStageNo As Integer = eSys.DispStage1 To eSys.DispStage2
    '        If Not gSYS(mStageNo) Is Nothing Then
    '            If gSYS(mStageNo).RunStatus = enmRunStatus.Alarm Then
    '                Return True
    '            End If
    '        End If
    '    Next
    '    Return False
    'End Function
    ' ''' <summary>B機流程異常判定條件 </summary>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function IsMachineBSoftwareInterlock() As Boolean
    '    If Not gSYS(eSys.MachineB) Is Nothing Then
    '        If gSYS(eSys.MachineB).RunStatus = enmRunStatus.Alarm Then
    '            Return True
    '        End If
    '    End If
    '    For mStageNo As Integer = eSys.DispStage3 To eSys.DispStage4
    '        If Not gSYS(mStageNo) Is Nothing Then
    '            If gSYS(mStageNo).RunStatus = enmRunStatus.Alarm Then
    '                Return True
    '            End If
    '        End If
    '    Next
    '    Return False
    'End Function

    ''' <summary>
    ''' 螺桿閥過電流保護
    ''' </summary>
    ''' <remarks></remarks>
    Sub ValveProtection()
        '[說明]:若為Valve Type為None，則不檢查Controller Alarm訊號
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) <> enmValveType.None Then
                    If gDICollection.GetState(enmDI.ValveControllerAlarm1, False) = True Then
                        '[說明]:一出現過Dispener Controller Alarm，就關閉出膠
                        Call SetDispensingTrigger(enmStage.No1, eValveWorkMode.Valve1, enmONOFF.eOff)
                        gSYS(eSys.MachineA).Act(eAct.AutoRun).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                        gEqpMsg.AddHistoryAlarm("Alarm_2019002", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2019002))     '[Dispenser No1 Controller Alarm !]
                    End If
                End If

                If gSSystemParameter.StageParts.ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1) <> enmValveType.None Then
                    If gDICollection.GetState(enmDI.ValveControllerAlarm2, False) = True Then
                        '[說明]:一出現過Dispener Controller Alarm，就關閉出膠
                        Call SetDispensingTrigger(enmStage.No2, eValveWorkMode.Valve1, enmONOFF.eOff)
                        gSYS(eSys.MachineA).Act(eAct.AutoRun).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                        gEqpMsg.AddHistoryAlarm("Alarm_2019002", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2019002))     '[Dispenser No1 Controller Alarm !]
                    End If
                End If

                If gSSystemParameter.StageParts.ValveData(enmStage.No3).ValveType(eValveWorkMode.Valve1) <> enmValveType.None Then
                    If gDICollection.GetState(enmDI.ValveControllerAlarm3, False) = True Then
                        '[說明]:一出現過Dispener Controller Alarm，就關閉出膠
                        Call SetDispensingTrigger(enmStage.No3, eValveWorkMode.Valve1, enmONOFF.eOff)
                        gSYS(eSys.MachineA).Act(eAct.AutoRun).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                        gEqpMsg.AddHistoryAlarm("Alarm_2019002", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2019002))     '[Dispenser No1 Controller Alarm !]
                    End If
                End If

                If gSSystemParameter.StageParts.ValveData(enmStage.No4).ValveType(eValveWorkMode.Valve1) <> enmValveType.None Then
                    If gDICollection.GetState(enmDI.ValveControllerAlarm4, False) = True Then
                        '[說明]:一出現過Dispener Controller Alarm，就關閉出膠
                        Call SetDispensingTrigger(enmStage.No4, eValveWorkMode.Valve1, enmONOFF.eOff)
                        gSYS(eSys.MachineA).Act(eAct.AutoRun).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                        gEqpMsg.AddHistoryAlarm("Alarm_2019002", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2019002))     '[Dispenser No1 Controller Alarm !]
                    End If
                End If

                '[說明]:按EMO後，就停止出膠 & Screw Valve轉動
                If gDICollection.GetState(enmDI.EMO, False) = True Then
                    Call SetDispensingTrigger(enmStage.No1, eValveWorkMode.Valve1, enmONOFF.eOff)
                    Call SetDispensingTrigger(enmStage.No2, eValveWorkMode.Valve1, enmONOFF.eOff)
                    Call SetDispensingTrigger(enmStage.No3, eValveWorkMode.Valve1, enmONOFF.eOff)
                    Call SetDispensingTrigger(enmStage.No4, eValveWorkMode.Valve1, enmONOFF.eOff)
                    '20170811 
                    gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None
                End If

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) <> enmValveType.None Then
                    If gDICollection.GetState(enmDI.ValveControllerAlarm1, False) = True Then
                        '[說明]:一出現過Dispener Controller Alarm，就關閉出膠
                        Call SetDispensingTrigger(enmStage.No1, eValveWorkMode.Valve1, enmONOFF.eOff)
                        gSYS(eSys.MachineA).Act(eAct.AutoRun).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                        gEqpMsg.AddHistoryAlarm("Alarm_2019002", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2019002))     '[Dispenser No1 Controller Alarm !]
                    End If
                End If

                If gSSystemParameter.StageParts.ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1) <> enmValveType.None Then
                    If gDICollection.GetState(enmDI.ValveControllerAlarm2, False) = True Then
                        '[說明]:一出現過Dispener Controller Alarm，就關閉出膠
                        Call SetDispensingTrigger(enmStage.No2, eValveWorkMode.Valve1, enmONOFF.eOff)
                        gSYS(eSys.MachineA).Act(eAct.AutoRun).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                        gEqpMsg.AddHistoryAlarm("Alarm_2019002", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2019002))     '[Dispenser No1 Controller Alarm !]
                    End If
                End If

                '[說明]:按EMO後，就停止出膠 & Screw Valve轉動
                If gDICollection.GetState(enmDI.EMO, False) = True Then
                    Call SetDispensingTrigger(enmStage.No1, eValveWorkMode.Valve1, enmONOFF.eOff)
                    Call SetDispensingTrigger(enmStage.No2, eValveWorkMode.Valve1, enmONOFF.eOff)
                    '20170811 
                    gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None
                End If

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) <> enmValveType.None Then
                            If gDICollection.GetState(enmDI.ValveControllerAlarm1, False) = True Then
                                '[說明]:一出現過Dispener Controller Alarm，就關閉出膠
                                Call SetDispensingTrigger(enmStage.No1, eValveWorkMode.Valve1, enmONOFF.eOff)
                                gSYS(eSys.MachineA).Act(eAct.AutoRun).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                                gEqpMsg.AddHistoryAlarm("Alarm_2019002", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2019002))     '[Dispenser No1 Controller Alarm !]
                            End If
                        End If

                        '[說明]:按EMO後，就停止出膠 & Screw Valve轉動
                        If gDICollection.GetState(enmDI.EMO, False) = True Then
                            Call SetDispensingTrigger(enmStage.No1, eValveWorkMode.Valve1, enmONOFF.eOff)
                            '20170811 
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None
                        End If

                    Case eMechanismModule.TwoValveOneStage
                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) <> enmValveType.None Then
                            If gDICollection.GetState(enmDI.ValveControllerAlarm1, False) = True Then
                                '[說明]:一出現過Dispener Controller Alarm，就關閉出膠
                                Call SetDispensingTrigger(enmStage.No1, eValveWorkMode.Valve1, enmONOFF.eOff)
                                gSYS(eSys.MachineA).Act(eAct.AutoRun).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                                gEqpMsg.AddHistoryAlarm("Alarm_2019002", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2019002))     '[Dispenser No1 Controller Alarm !]
                            End If
                        End If

                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve2) <> enmValveType.None Then
                            If gDICollection.GetState(enmDI.ValveControllerAlarm2, False) = True Then
                                '[說明]:一出現過Dispener Controller Alarm，就關閉出膠
                                Call SetDispensingTrigger(enmStage.No1, eValveWorkMode.Valve2, enmONOFF.eOff)
                                gSYS(eSys.MachineA).Act(eAct.AutoRun).RunStatus = enmRunStatus.Alarm  '[Alarm後暫停]
                                gEqpMsg.AddHistoryAlarm("Alarm_2019002", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2019002))     '[Dispenser No1 Controller Alarm !]
                            End If
                        End If

                        '[說明]:按EMO後，就停止出膠 & Screw Valve轉動
                        If gDICollection.GetState(enmDI.EMO, False) = True Then
                            Call SetDispensingTrigger(enmStage.No1, eValveWorkMode.Valve1, enmONOFF.eOff)
                            Call SetDispensingTrigger(enmStage.No1, eValveWorkMode.Valve2, enmONOFF.eOff)
                            '20170811 
                            gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None
                        End If
                End Select
        End Select
    End Sub

    ''' <summary>
    ''' [說明]:3秒後才開始檢查ENO訊號，因為線路配置設定之關係
    ''' Interval=100  30次-->3秒
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEMOStatus() As Boolean
        Static mProgramInitialNoNeedWatchEMO As Boolean = False
        Static mNoNeedWatchEMOCount As Integer
        If mProgramInitialNoNeedWatchEMO = False Then
            mNoNeedWatchEMOCount = mNoNeedWatchEMOCount + 1
            If mNoNeedWatchEMOCount > 30 Then
                mProgramInitialNoNeedWatchEMO = True
            End If
        Else
            Return gDICollection.GetState(enmDI.EMO, False)    'DI-EMO按鈕
        End If
        Return gInterlockCollection.Items(enmHardwardAlarm.EMO).Status
    End Function

    ''' <summary>
    ''' 膠材壽命監控
    ''' </summary>
    ''' <remarks></remarks>
    Sub MonitorPotLife()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If IsPasteLifeCountOver(enmStage.No1, enmValve.No1) Then
                    gEqpMsg.AddHistoryAlarm("Warn_3019007", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019008), eMessageLevel.Warning)
                End If
                If IsPasteLifeTimeOut(enmStage.No1, enmValve.No1) Then
                    gEqpMsg.AddHistoryAlarm("Warn_3019007", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019007), eMessageLevel.Warning)
                End If
                If IsPasteLifeCountOver(enmStage.No2, enmValve.No1) Then
                    gEqpMsg.AddHistoryAlarm("Warn_3019108", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019108), eMessageLevel.Warning)
                End If
                If IsPasteLifeTimeOut(enmStage.No2, enmValve.No1) Then
                    gEqpMsg.AddHistoryAlarm("Warn_3019107", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019107), eMessageLevel.Warning)
                End If
                If IsPasteLifeCountOver(enmStage.No3, enmValve.No1) Then
                    gEqpMsg.AddHistoryAlarm("Warn_3019208", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019208), eMessageLevel.Warning)
                End If
                If IsPasteLifeTimeOut(enmStage.No3, enmValve.No1) Then
                    gEqpMsg.AddHistoryAlarm("Warn_3019207", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019207), eMessageLevel.Warning)
                End If
                If IsPasteLifeCountOver(enmStage.No4, enmValve.No1) Then
                    gEqpMsg.AddHistoryAlarm("Warn_3019308", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019308), eMessageLevel.Warning)
                End If
                If IsPasteLifeTimeOut(enmStage.No4, enmValve.No1) Then
                    gEqpMsg.AddHistoryAlarm("Warn_3019307", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019307), eMessageLevel.Warning)
                End If

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If IsPasteLifeCountOver(enmStage.No1, enmValve.No1) Then
                    gEqpMsg.AddHistoryAlarm("Warn_3019007", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019008), eMessageLevel.Warning)
                End If
                If IsPasteLifeTimeOut(enmStage.No1, enmValve.No1) Then
                    gEqpMsg.AddHistoryAlarm("Warn_3019007", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019007), eMessageLevel.Warning)
                End If
                If IsPasteLifeCountOver(enmStage.No2, enmValve.No1) Then
                    gEqpMsg.AddHistoryAlarm("Warn_3019108", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019108), eMessageLevel.Warning)
                End If
                If IsPasteLifeTimeOut(enmStage.No2, enmValve.No1) Then
                    gEqpMsg.AddHistoryAlarm("Warn_3019107", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019107), eMessageLevel.Warning)
                End If

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        If IsPasteLifeCountOver(enmStage.No1, enmValve.No1) Then
                            gEqpMsg.AddHistoryAlarm("Warn_3019007", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019008), eMessageLevel.Warning)
                        End If

                    Case eMechanismModule.TwoValveOneStage
                        If IsPasteLifeCountOver(enmStage.No1, enmValve.No1) Then
                            gEqpMsg.AddHistoryAlarm("Warn_3019007", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019008), eMessageLevel.Warning)
                        End If
                        If IsPasteLifeCountOver(enmStage.No1, enmValve.No2) Then
                            gEqpMsg.AddHistoryAlarm("Warn_3019007", "MonitorPotLife()", , gMsgHandler.GetMessage(Warn_3019008), eMessageLevel.Warning)
                        End If
                End Select
        End Select
    End Sub
    Private Sub IOUseTimer_Tick(sender As Object, e As EventArgs) Handles IOUseTimer.Tick


        'Eason 20170302 Ticket:100090 , System Update Crash [S] Note: 目前沒有使用了
        '[顯示程式目前使用哪一款生產模式]
        'If gStageMap(gSYS(eSys.Manual).StageNo).Node.Count > 0 Then '節點Pattern有數量
        '    Dim index As New sLevelIndexCollection
        '    index.NodeName = gStageMap(gSYS(eSys.Manual).StageNo).Node.Keys(0)
        '    If gStageMap(gSYS(eSys.Manual).StageNo).Node.ContainsKey(index.NodeName) Then '指定的節點Pattern存在
        '        If IsNothing(gStageMap(gSYS(eSys.Manual).StageNo).Node(index.NodeName).ChipState) = False Then '該節點資料存在
        '            For intYf = gStageMap(gSYS(eSys.Manual).StageNo).Node(index.NodeName).ChipState.GetLowerBound(1) To gStageMap(gSYS(eSys.Manual).StageNo).Node(index.NodeName).ChipState.GetUpperBound(1) '對節點內每一顆
        '                For intXf = gStageMap(gSYS(eSys.Manual).StageNo).Node(index.NodeName).ChipState.GetLowerBound(0) To gStageMap(gSYS(eSys.Manual).StageNo).Node(index.NodeName).ChipState.GetUpperBound(0)
        '                    If gStageMap(gSYS(eSys.Manual).StageNo).Node(index.NodeName).ChipState(index.Xf, index.Yf).NeedUpdate Then
        '                        Call DrawGraphicsMultiDeviceForDieState(gSYS(eSys.Manual), index) '更新顯示
        '                        gStageMap(gSYS(eSys.Manual).StageNo).Node(index.NodeName).ChipState(intXf, intYf).NeedUpdate = False
        '                    End If
        '                Next
        '            Next
        '        End If
        '    End If
        'End If
        'Eason 20170302 Ticket:100090 , System Update Crash [E] Note: 目前沒有使用了
        MonitorPotLife() '膠材壽命監控

        gInterlockCollection.Items(enmHardwardAlarm.EMO).Status = GetEMOStatus()
        gInterlockCollection.Items(enmHardwardAlarm.CDA).Status = gDICollection.GetState(enmDI.CDA)    'DI-CDA異常
        gInterlockCollection.Items(enmHardwardAlarm.CDA2).Status = gDICollection.GetState(enmDI.CDA2)
        gInterlockCollection.Items(enmHardwardAlarm.DoorClose).Status = gDICollection.GetState(enmDI.DoorClose)
        gInterlockCollection.Items(enmHardwardAlarm.DoorClose2).Status = gDICollection.GetState(enmDI.DoorClose2)
        gInterlockCollection.Items(enmHardwardAlarm.EMS).Status = gDICollection.GetState(enmDI.EMS)
        gInterlockCollection.Items(enmHardwardAlarm.EMS2).Status = gDICollection.GetState(enmDI.EMS2)
        gInterlockCollection.Items(enmHardwardAlarm.MC_Heater).Status = gDICollection.GetState(enmDI.MC3)
        gInterlockCollection.Items(enmHardwardAlarm.MC_Heater2).Status = gDICollection.GetState(enmDI.MC_Heater2)
        gInterlockCollection.Items(enmHardwardAlarm.MC_Motor).Status = gDICollection.GetState(enmDI.MC2)
        gInterlockCollection.Items(enmHardwardAlarm.MC_Motor2).Status = gDICollection.GetState(enmDI.MC_Motor2)

        ''[說明]:初始化後Heater紀錄開啟 For 800A   20161010  20161129
        'Select Case gSSystemParameter.enmMachineType
        '    Case enmMachineType.DCSW_800AQ
        '        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.Finish Then
        '            If gfrmUIViewer.Heater = True Then
        '                If gfrmUIViewer.HeaterOn(0) = True Then
        '                    gDOCollection.GetSetState(enmDO.HeaterOn1) = True
        '                End If
        '                If gfrmUIViewer.HeaterOn(1) = True Then
        '                    gDOCollection.GetSetState(enmDO.HeaterOn2) = True
        '                End If
        '                If gfrmUIViewer.HeaterOn(2) = True Then
        '                    gDOCollection.GetSetState(enmDO.HeaterOn3) = True
        '                End If
        '                If gfrmUIViewer.HeaterOn(3) = True Then
        '                    gDOCollection.GetSetState(enmDO.HeaterOn4) = True
        '                End If
        '                If gfrmUIViewer.HeaterOn(4) = True Then
        '                    gDOCollection.GetSetState(enmDO.HeaterOn5) = True
        '                End If
        '                If gfrmUIViewer.HeaterOn(5) = True Then
        '                    gDOCollection.GetSetState(enmDO.HeaterOn6) = True
        '                End If
        '                If gfrmUIViewer.HeaterOn(6) = True Then
        '                    gDOCollection.GetSetState(enmDO.HeaterOn7) = True
        '                End If
        '                If gfrmUIViewer.HeaterOn(7) = True Then
        '                    gDOCollection.GetSetState(enmDO.HeaterOn8) = True
        '                End If
        '                If gfrmUIViewer.HeaterOn(8) = True Then
        '                    gDOCollection.GetSetState(enmDO.HeaterOn9) = True
        '                End If
        '                If gfrmUIViewer.HeaterOn(9) = True Then
        '                    gDOCollection.GetSetState(enmDO.HeaterOn10) = True
        '                End If
        '                If gfrmUIViewer.HeaterOn(10) = True Then
        '                    gDOCollection.GetSetState(enmDO.HeaterOn11) = True
        '                End If
        '                If gfrmUIViewer.HeaterOn(11) = True Then
        '                    gDOCollection.GetSetState(enmDO.HeaterOn12) = True
        '                End If
        '                gfrmUIViewer.Heater = False
        '            End If
        '        End If
        'End Select

        If gInterlockCollection.GetOverallHardwareInterlock() Then '整機異常

            gInterlockCollection.IsAlarm = True           '[故障]硬體InterLock

            If gSSystemParameter.MachineType = enmMachineType.eDTS300A Then '僅300A有PLC
                '--- Soni + 2014.11.11 給PLC的IPC Alarm提示 ---
                If PLCM(enmPLCM.PCAlarmOutput) = False And PLCM(enmPLCM.PLCAlarmInput) = False Then
                    If gPLC.IsOpen Then
                        gPLC.WriteBits("M1610", 1, "1")
                    End If
                End If
                '--- Soni + 2014.11.11 給PLC的IPC Alarm提示 ---
                If PLCM(enmPLCM.PLCAlarmInput) = True Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2000002", "PLC Alarm", , gMsgHandler.GetMessage(Alarm_2000002)) '
                    'MsgBox(gMsgHandler.GetMessage(Alarm_2000002))
                End If
            End If
            If gInterlockCollection.Items(enmHardwardAlarm.EMO).Status = True Then
                gEqpMsg.AddHistoryAlarm("Alarm_2000001", "Main IOUseTimer", , gMsgHandler.GetMessage(Alarm_2000001)) '緊急停止鈕已作動!!      'EMO Alarm!!
                'MsgBox(gMsgHandler.GetMessage(Alarm_2000001))
            End If


            '20161010
            '主控層
            gSYS(eSys.OverAll).RunStatus = enmRunStatus.Alarm
            '機台控制層
            For mMachineNo As Integer = eSys.MachineA To gSSystemParameter.MachineMax
                gSYS(mMachineNo).RunStatus = enmRunStatus.Alarm
            Next
            'Stage控制層
            For mStageNo As Integer = eSys.DispStage1 To gSSystemParameter.StageMax
                gSYS(mStageNo).RunStatus = enmRunStatus.Alarm
            Next
            'SubDisp控制層
            For mStageNo As Integer = eSys.SubDisp1 To gSSystemParameter.SubDispMax
                gSYS(mStageNo).RunStatus = enmRunStatus.Alarm
            Next

            Const Dec As Decimal = 3000
            '所有軸停止 SlowStop能指定減速度的Stop API
            For mSysNo As Integer = eSys.DispStage1 To gSSystemParameter.StageMax 'gSysAdapter.GetSysStageMax Soni / 2017.05.11 統一參數來源
                gCMotion.SlowStop(gSYS(mSysNo).AxisX, Dec)
                gCMotion.SlowStop(gSYS(mSysNo).AxisY, Dec)
                gCMotion.SlowStop(gSYS(mSysNo).AxisZ, Dec)
                gCMotion.SlowStop(gSYS(mSysNo).AxisB, Dec)
                gCMotion.SlowStop(gSYS(mSysNo).AxisC, Dec)
            Next

        Else
            gInterlockCollection.IsAlarm = False
            '--- Soni + 2014.11.11 給PLC的IPC EMO解除 ---
            If gSSystemParameter.MachineType = enmMachineType.eDTS300A Then '僅300A有PLC

                If PLCM(enmPLCM.PCAlarmOutput) = True Then
                    If gPLC.IsOpen Then
                        gPLC.WriteBits("M1610", 1, "0")
                    End If
                End If

            End If
            '--- Soni + 2014.11.11 給PLC的IPC EMO解除 ---
        End If

        ValveProtection() '[說明]:判斷Screw Valve之過電流
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                gEqpInfo.GetW800AQEqpStatus() '取得設備狀態
            Case enmMachineType.DCS_500AD
                gEqpInfo.Get500ADEqpStatus() '取得設備狀態
            Case enmMachineType.DCS_F230A
                gEqpInfo.GetF230AEqpStatus() '取得設備狀態

            Case enmMachineType.DCS_350A
                gEqpInfo.Get350AEqpStatus()

            Case enmMachineType.eDTS300A
                'TODO:350A改230A軟體修改項目
            Case enmMachineType.eDTS_2S2V
                gEqpInfo.Get2S2VEqpStatus() 'Soni + 2016.11.23 取得設備狀態

        End Select

    End Sub

#End Region




#Region "主按鍵"



    ''' <summary>按鍵鎖住</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsButtonLock() As Boolean
        'If Not gfrmCalibrationCCD2FindHeight Is Nothing Then
        '    If gfrmCalibrationCCD2FindHeight.Visible Then
        '        Return True
        '    End If
        'End If

        If Not gfrmCalibrationCCD2Valve1 Is Nothing Then
            If gfrmCalibrationCCD2Valve1.Visible Then
                Return True
            End If
        End If

        If Not gfrmCalibrationCCD2Valve2 Is Nothing Then
            If gfrmCalibrationCCD2Valve2.Visible Then
                Return True
            End If
        End If

        If Not gfrmCalibrationZHeight Is Nothing Then
            If gfrmCalibrationZHeight.Visible Then
                Return True
            End If
        End If

        If Not gfrmEngineMode Is Nothing Then
            If gfrmEngineMode.Visible Then
                Return True
            End If
        End If

        'If Not gfrmRecipe00 Is Nothing Then
        '    If gfrmRecipe00.Visible Then
        '        Return True
        '    End If
        'End If

        'If Not gfrmRecipe01 Is Nothing Then
        '    If gfrmRecipe01.Visible Then
        '        Return True
        '    End If
        'End If

        'If Not gfrmRecipe02 Is Nothing Then
        '    If gfrmRecipe02.Visible Then
        '        Return True
        '    End If
        'End If

        If Not gfrmRecipe04 Is Nothing Then
            If gfrmRecipe04.Visible Then
                Return True
            End If
        End If

        'If Not gfrmRecipe05 Is Nothing Then
        '    If gfrmRecipe05.Visible Then
        '        Return True
        '    End If
        'End If

        'If Not gfrmRecipe06 Is Nothing Then
        '    If gfrmRecipe06.Visible Then
        '        Return True
        '    End If
        'End If

        'If Not gfrmRecipe07 Is Nothing Then
        '    If gfrmRecipe07.Visible Then
        '        Return True
        '    End If
        'End If
        Return False
    End Function

    ''' <summary>
    ''' 介面保護條件 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TmrAutoRun_Tick(sender As Object, e As EventArgs) Handles TmrSystemState.Tick
        'TODO:   改為介面操作保護

        '[說明]:只要進入工程模式內的按鈕，則按鈕都會被鎖住
        'If IsButtonLock() Then

        'Else
        '[說明]:只要進入生產，則按鈕都會被鎖住
        '       若要進入設定頁籤，則必須重新Initial
        Select Case gSYS(eSys.OverAll).ExecuteCommand
            Case eSysCommand.AutoRun
                If gSYS(eSys.OverAll).IsCanPause Then '已暫停
                    btnRecipe.Enabled = True
                    btnCalibration.Enabled = True
                    btnEngineMode.Enabled = True
                Else '不可暫停
                    Select Case gSYS(eSys.OverAll).RunStatus
                        Case enmRunStatus.None
                            btnRecipe.Enabled = True
                            btnCalibration.Enabled = True
                            btnEngineMode.Enabled = True
                        Case enmRunStatus.Running
                            btnRecipe.Enabled = False
                            btnCalibration.Enabled = False
                            btnEngineMode.Enabled = False

                        Case enmRunStatus.Stop
                            btnRecipe.Enabled = True
                            btnCalibration.Enabled = True
                            btnEngineMode.Enabled = True
                        Case enmRunStatus.Alarm
                            btnRecipe.Enabled = True
                            btnCalibration.Enabled = True
                            btnEngineMode.Enabled = True
                        Case enmRunStatus.Finish
                            btnRecipe.Enabled = True
                            btnCalibration.Enabled = True
                            btnEngineMode.Enabled = True
                    End Select
                End If
        End Select
        'End If

    End Sub
    Private Sub btnHelp_MouseDown(sender As Object, e As MouseEventArgs) Handles btnHelp.MouseDown
        btnHelp.BackgroundImage = My.Resources._170MouseDown
    End Sub
    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        If btnHelp.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[frmMain]" & vbTab & "[btnHelp]" & vbTab & "Click")
        btnHelp.Enabled = False
        gfrmHelp = New frmHelp
        gfrmHelp.StartPosition = FormStartPosition.CenterScreen

        gfrmHelp.Show()
        gfrmHelp.BringToFront()
        btnHelp.Enabled = True
    End Sub
    Private Sub btnOpStatus_MouseDown(sender As Object, e As MouseEventArgs) Handles btnOpStatus.MouseDown
        btnOpStatus.BackgroundImage = My.Resources._170MouseDown
    End Sub
    Private Sub btnOpStatus_Click(sender As Object, e As EventArgs) Handles btnOpStatus.Click
        gSyslog.Save("[frmMain]" & vbTab & "[btnOpStatus]" & vbTab & "Click")
        If btnOpStatus.Enabled = False Then
            Exit Sub
        End If
        btnOpStatus.Enabled = False
        gfrmUIViewer = New frmOperator
        With gfrmUIViewer
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .ShowDialog()
            '20161122
            '  .Dispose()
        End With
        btnOpStatus.Enabled = True

    End Sub
    Private Sub btnLogs_MouseDown(sender As Object, e As MouseEventArgs) Handles btnLogs.MouseDown
        btnLogs.BackgroundImage = My.Resources._170MouseDown
    End Sub
    Private Sub btnLogs_Click(sender As Object, e As EventArgs) Handles btnLogs.Click
        If btnLogs.Enabled = False Then
            Exit Sub
        End If
        btnLogs.Enabled = False
        Dim mFrmLog As New frmLog
        With mFrmLog
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .ShowDialog()
            '20161122
            '  .Dispose()
        End With
        'Dim mFilePath As String = Application.StartupPath & "\LogData.exe"
        'If File.Exists(mFilePath) Then 'Soni + 2017.05.02 增加開檔保護
        '    System.Diagnostics.Process.Start(mFilePath)
        'Else
        '    MsgBox("LogData.exe Has Lost!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        'End If
        btnLogs.Enabled = True
    End Sub
    Private Sub btnDiagnosis_MouseDown(sender As Object, e As MouseEventArgs) Handles btnDiagnosis.MouseDown
        btnDiagnosis.BackgroundImage = My.Resources._170MouseDown
    End Sub
    Private Sub btnDiagnosis_Click(sender As Object, e As EventArgs) Handles btnDiagnosis.Click

        'MachineName = MachineTypeToString(gSSystemParameter.MachineID)
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                gSyslog.Save("[frmMain]" & vbTab & "[btnDiagnosisWetco]" & vbTab & "Click")
                btnDiagnosis.Enabled = False
                If gfrmDiagnosisWetco Is Nothing Then
                    gfrmDiagnosisWetco = New frmDiagWetco
                ElseIf gfrmDiagnosisWetco.IsDisposed Then
                    gfrmDiagnosisWetco = New frmDiagWetco
                End If

                With gfrmDiagnosisWetco
                    .StartPosition = FormStartPosition.CenterScreen
                    .Show()
                    .BringToFront()
                End With
            Case enmMachineType.DCS_500AD
                gSyslog.Save("[frmMain]" & vbTab & "[btnDiagnosisWetco]" & vbTab & "Click")
                btnDiagnosis.Enabled = False
                If gfrmDiagnosisWetco Is Nothing Then
                    gfrmDiagnosisWetco = New frmDiagWetco
                ElseIf gfrmDiagnosisWetco.IsDisposed Then
                    gfrmDiagnosisWetco = New frmDiagWetco
                End If

                With gfrmDiagnosisWetco
                    .StartPosition = FormStartPosition.CenterScreen
                    .Show()
                    .BringToFront()
                End With
            Case enmMachineType.DCS_350A
                gSyslog.Save("[frmMain]" & vbTab & "[btnDiagnosis]" & vbTab & "Click")
                btnDiagnosis.Enabled = False
                If gfrmDiagnosis350A Is Nothing Then
                    gfrmDiagnosis350A = New frmDiag350A
                ElseIf gfrmDiagnosis350A.IsDisposed Then
                    gfrmDiagnosis350A = New frmDiag350A
                End If

                With gfrmDiagnosis350A
                    .StartPosition = FormStartPosition.CenterScreen
                    .Show()
                    .BringToFront()
                End With

            Case Else
                gSyslog.Save("[frmMain]" & vbTab & "[btnDiagnosis]" & vbTab & "Click")
                btnDiagnosis.Enabled = False
                If gfrmDiagnosis Is Nothing Then
                    gfrmDiagnosis = New frmDiag
                ElseIf gfrmDiagnosis.IsDisposed Then
                    gfrmDiagnosis = New frmDiag
                End If

                With gfrmDiagnosis
                    .StartPosition = FormStartPosition.CenterScreen
                    .Show()
                    .BringToFront()
                End With
        End Select

        btnDiagnosis.Enabled = True

        If btnDiagnosis.Enabled = False Then
            Exit Sub
        End If
    End Sub

    Function GetString(ByVal value As String) As String
        Select Case value
            Case "SoftwareMaker", "Developer"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Developer"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "开发人员"
                    Case enmLanguageType.eTraditionalChinese
                        Return "開發人員"
                End Select
            Case "Administrator"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Administrator"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "系统管理员"
                    Case enmLanguageType.eTraditionalChinese
                        Return "系統管理員"
                End Select
            Case "Manager"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Manager"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "管理员"
                    Case enmLanguageType.eTraditionalChinese
                        Return "管理員"
                End Select
            Case "Engineer"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Engineer"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "工程师"
                    Case enmLanguageType.eTraditionalChinese
                        Return "工程師"
                End Select
            Case "Operator"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Operator"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "操作员"
                    Case enmLanguageType.eTraditionalChinese
                        Return "作業員"
                End Select
        End Select
        Return ""
    End Function
    ''' <summary>取得使用者權限</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAuthorityName() As String
        Select Case gUserLevel
            Case enmUserLevel.eSoftwareMaker
                Return GetString("Developer")
            Case enmUserLevel.eAdministrator
                Return GetString("Administrator")
            Case enmUserLevel.eManager
                Return GetString("Manager")
            Case enmUserLevel.eEngineer
                Return GetString("Engineer")
            Case enmUserLevel.eOperator
                Return GetString("Operator")
        End Select
        Return GetString("Unknown")
    End Function

    Private Sub btnLogOut_MouseDown(sender As Object, e As MouseEventArgs) Handles btnLogOut.MouseDown
        btnLogOut.BackgroundImage = My.Resources._170MouseDown
    End Sub
    '按下登出[Logout] 按鈕
    Private Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        gSyslog.Save("[frmMain]" & vbTab & "[btnLogOut]" & vbTab & "Click")
        btnLogOut.Enabled = False
        If gfrmLogin Is Nothing Then
            gfrmLogin = New frmLogin
        ElseIf gfrmLogin.IsDisposed Then
            gfrmLogin = New frmLogin
        End If
        gfrmLogin.TopMost = True
        gfrmLogin.Visible = False

        gfrmLogin.StartPosition = FormStartPosition.CenterScreen
        '進入前全介面封鎖

        Dim theResult As Integer = gfrmLogin.ShowDialog()
        '[說明]:使用者資料
        btnLogOut.Text = GetAuthorityName()
        btnRecipe.Enabled = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.Recipe), gUserLevel)
        btnCalibration.Enabled = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.Calibration), gUserLevel)
        btnEngineMode.Enabled = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.EngineMode), gUserLevel)
        btnLogOut.Enabled = True
    End Sub
    Private Sub btnCalibration_MouseDown(sender As Object, e As MouseEventArgs) Handles btnCalibration.MouseDown
        btnCalibration.BackgroundImage = My.Resources._170MouseDown
    End Sub
    Private Sub btnCalibration_Click(sender As Object, e As EventArgs) Handles btnCalibration.Click
        If btnCalibration.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[frmMain]" & vbTab & "[btnCalibration]" & vbTab & "Click")
        btnCalibration.Enabled = False

        ''2016.06.22 因PID, 不能直接進單CCD校正
        'Dim mfrmCalibrationMenu As New frmCalibrationMenu
        'With mfrmCalibrationMenu

        '    .StartPosition = FormStartPosition.CenterScreen

        '    .ShowDialog()
        '    '20161122
        '    .Dispose()
        'End With


        '20170321
        Dim mfrmCalibrationMenuNew As New frmCalibrationMenuNew
        With mfrmCalibrationMenuNew

            .StartPosition = FormStartPosition.CenterScreen

            .ShowDialog()
            '20161122
            .Dispose()
        End With
        btnCalibration.Enabled = True

    End Sub
    Private Sub btnRecipe_MouseDown(sender As Object, e As MouseEventArgs) Handles btnRecipe.MouseDown
        btnRecipe.BackgroundImage = My.Resources._170MouseDown
    End Sub
    Private Sub btnRecipe_Click(sender As Object, e As EventArgs) Handles btnRecipe.Click
        If btnRecipe.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[frmMain]" & vbTab & "[btnRecipe]" & vbTab & "Click")

        btnRecipe.Enabled = False

        If gUserLevel = enmUserLevel.eSoftwareMaker Then '軟體測試版

            Dim gfrmRecipe As Premtek.frmRecipe
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    gfrmRecipe = New Premtek.frmRecipe(2, 2, 1, 1)
                Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                    gfrmRecipe = New Premtek.frmRecipe(1, 2, 1, 1)
                Case enmMachineType.DCS_350A
                    gfrmRecipe = New Premtek.frmRecipe(1, 1, 2, 2)
                Case Else
                    gfrmRecipe = New Premtek.frmRecipe(1, 1, 2, 1)
            End Select
            gfrmRecipe.Motion = gCMotion
            gfrmRecipe.Syslog = gSyslog
            gfrmRecipe.EqpMsg = gEqpMsg
            gfrmRecipe.AOI = gAOICollection
            gfrmRecipe.StartPosition = FormStartPosition.Manual
            gfrmRecipe.Location = New System.Drawing.Point(0, 0)
            gfrmRecipe.ShowDialog()
            btnRecipe.Enabled = True
            Exit Sub
        End If

        If gfrmRecipe04 Is Nothing Then
            gfrmRecipe04 = New frmRecipe04
        ElseIf gfrmRecipe04.IsDisposed Then
            gfrmRecipe04 = New frmRecipe04
        End If
        'gfrmRecipe04 = New frmRecipe04
        'gfrmRecipe01.Hide()
        'gfrmRecipe02.Hide()
        'gfrmRecipe06.Hide()
        gCRecipe.Editable = False '預設唯讀
        gRecipeEdit.Editable = False '預設唯讀
        With gfrmRecipe04
            '20170711
            .sys = gSYS(eSys.DispStage1)
            ' .sys = gSYS(eSys.Manual)
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .ShowDialog()
            '20161122
            '  .Dispose()
        End With
        btnRecipe.Enabled = True
    End Sub

    Private Sub btnEngineMode_MouseDown(sender As Object, e As MouseEventArgs) Handles btnEngineMode.MouseDown
        btnEngineMode.BackgroundImage = My.Resources._170MouseDown
    End Sub
    Private Sub btMainButton_Click(sender As Object, e As EventArgs) Handles btnEngineMode.Click
        If btnEngineMode.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[frmMain]" & vbTab & "[btnEngineMode]" & vbTab & "Click")

        btnEngineMode.Enabled = False
        If gfrmEngineMode Is Nothing Then
            gfrmEngineMode = New frmEngineMode
        ElseIf gfrmEngineMode.IsDisposed Then
            gfrmEngineMode = New frmEngineMode
        End If


        With gfrmEngineMode
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterScreen

            .ShowDialog()
            '20161122
            '     .Dispose()

            gfrmEngineMode = Nothing
        End With
        btnEngineMode.Enabled = True

    End Sub


    Private Sub btnHelp_MouseUp(sender As Object, e As MouseEventArgs) Handles btnHelp.MouseUp
        btnHelp.BackgroundImage = My.Resources._170MouseUp
    End Sub

    Private Sub btnCalibration_MouseUp(sender As Object, e As MouseEventArgs) Handles btnCalibration.MouseUp
        btnCalibration.BackgroundImage = My.Resources._170MouseUp
    End Sub

    Private Sub btnDiagnosis_MouseUp(sender As Object, e As MouseEventArgs) Handles btnDiagnosis.MouseUp
        btnDiagnosis.BackgroundImage = My.Resources._170MouseUp
    End Sub

    Private Sub btnEngineMode_MouseUp(sender As Object, e As MouseEventArgs) Handles btnEngineMode.MouseUp
        btnEngineMode.BackgroundImage = My.Resources._170MouseUp
    End Sub

    Private Sub btnLogOut_MouseUp(sender As Object, e As MouseEventArgs) Handles btnLogOut.MouseUp
        btnLogOut.BackgroundImage = My.Resources._170MouseUp
    End Sub

    Private Sub btnLogs_MouseUp(sender As Object, e As MouseEventArgs) Handles btnLogs.MouseUp
        btnLogs.BackgroundImage = My.Resources._170MouseUp
    End Sub

    Private Sub btnOpStatus_MouseUp(sender As Object, e As MouseEventArgs) Handles btnOpStatus.MouseUp
        btnOpStatus.BackgroundImage = My.Resources._170MouseUp
    End Sub

    Private Sub btnRecipe_MouseUp(sender As Object, e As MouseEventArgs) Handles btnRecipe.MouseUp
        btnRecipe.BackgroundImage = My.Resources._170MouseUp
    End Sub
#End Region



    Private Sub SetElectricCylinder()
        Dim folderName As String = System.Windows.Forms.Application.StartupPath & "\system\" & MachineName
        Dim path As String = folderName & "\Conveyor.ini"
        Unit.A_ECylinderTopPos = Convert.ToDouble(ReadIniString("Electric Cylinder", "A_TopPosition", path))
        Unit.A_ECylinderBottomPos = Convert.ToDouble(ReadIniString("Electric Cylinder", "A_BottomPosition", path))
        Unit.A_ECylinderSpeed = Convert.ToDouble(ReadIniString("Electric Cylinder", "A_Speed", path))

        Unit.B_ECylinderTopPos = Convert.ToDouble(ReadIniString("Electric Cylinder", "B_TopPosition", path))
        Unit.B_ECylinderBottomPos = Convert.ToDouble(ReadIniString("Electric Cylinder", "B_BottomPosition", path))
        Unit.B_ECylinderSpeed = Convert.ToDouble(ReadIniString("Electric Cylinder", "B_Speed", path))
    End Sub

    Private Sub SetVacuum()
        Dim folderName As String = System.Windows.Forms.Application.StartupPath & "\system\" & MachineName
        Dim path As String = folderName & "\Conveyor.ini"
        Dim vacuumA(5) As Boolean
        vacuumA(0) = IIf(ReadIniString("Vacuum", "A1", path) = "1", True, False)
        vacuumA(1) = IIf(ReadIniString("Vacuum", "A2", path) = "1", True, False)
        vacuumA(2) = IIf(ReadIniString("Vacuum", "A3", path) = "1", True, False)
        vacuumA(3) = IIf(ReadIniString("Vacuum", "A4", path) = "1", True, False)
        vacuumA(4) = IIf(ReadIniString("Vacuum", "A5", path) = "1", True, False)
        vacuumA(5) = IIf(ReadIniString("Vacuum", "A6", path) = "1", True, False)
        Unit.A_SetVacuum(vacuumA)

        Dim vacuumB(5) As Boolean
        vacuumB(0) = IIf(ReadIniString("Vacuum", "B1", path) = "1", True, False)
        vacuumB(1) = IIf(ReadIniString("Vacuum", "B2", path) = "1", True, False)
        vacuumB(2) = IIf(ReadIniString("Vacuum", "B3", path) = "1", True, False)
        vacuumB(3) = IIf(ReadIniString("Vacuum", "B4", path) = "1", True, False)
        vacuumB(4) = IIf(ReadIniString("Vacuum", "B5", path) = "1", True, False)
        vacuumB(5) = IIf(ReadIniString("Vacuum", "B6", path) = "1", True, False)
        Unit.B_SetVacuum(vacuumB)
    End Sub

    Private Sub btnSysDebug_Click(sender As Object, e As EventArgs)
        frmSysDebug.Show()
    End Sub

  
  
    Private Sub btnAirPressure_Click(sender As Object, e As EventArgs) Handles btnAirPressure.Click
        If btnAirPressure.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[frmSetValveAirPressure]" & vbTab & "[btnAirPressure]" & vbTab & "Click")

        btnAirPressure.Enabled = False
        If gfrmSetValveAirPressure Is Nothing Then
            gfrmSetValveAirPressure = New frmSetValveAirPressure
        ElseIf gfrmSetValveAirPressure.IsDisposed Then
            gfrmSetValveAirPressure = New frmSetValveAirPressure
        End If


        With gfrmSetValveAirPressure
            .sys = gSYS(eSys.DispStage1)
            ' .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.CenterScreen
            .Location = New Point(0, 0)
            .ShowDialog()

        End With
        btnAirPressure.Enabled = True
    End Sub


    Public Function Initialize_Balance() As Boolean

        gBalanceCollection.Initial(gBalanceCollection.WeighingUnit)
        For i As Integer = 0 To enmBalance.Max
            AddHandler gBalanceCollection.Items(i).OnDataRecieved, AddressOf gPWU_OnDataRecieved
        Next
        For mChNo As Integer = 0 To enmBalance.Max
            gBalanceCollection.Parameter(mChNo).Load(mChNo, Application.StartupPath & "\System\" & MachineName & "\ConfigBalance" & (mChNo + 1).ToString & ".ini")
            gBalanceCollection.Parameter(mChNo).Save(mChNo, Application.StartupPath & "\System\" & MachineName & "\ConfigBalance" & (mChNo + 1).ToString & ".ini")
        Next
        gWeight = New MFunctionWeight

        Return True
    End Function


    Private Sub gPWU_OnDataRecieved(sender As Object, e As ProjectCore.DataEventArgs)

        '重量控制
        If gVolumneControl.Enabled Then
            Select Case gVolumneControl.ControlType
                Case enmControlType.WeightToAir
                    gVolumneControl.AirControl(weightCorrectedValue, gAOCollection.Value(enmAO.DispenserNo1EPRegulator))
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6015008, gAOCollection.Value(enmAO.DispenserNo1EPRegulator)), "INFO_6015008")
                    'Debug.Print("重量控制氣壓:" & gAOCollection.gdblAOHMISET(enmAO.DispenserNo1EPRegulator))
                Case enmControlType.WeightToPoints
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6015009, gVolumneControl.PointsControl(weightCorrectedValue)), "INFO_6015009")
                    'Debug.Print("重量控制點數:" & gVolumneControl.PointsControl(weightCorrectedValue))
            End Select
        End If
        InvokeLabel(gfrmSetBalance.lblWeight, "Weight:" & weightCorrectedValue.ToString("#00.000##"))
    End Sub



End Class
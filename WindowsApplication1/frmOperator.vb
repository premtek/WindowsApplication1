Imports ProjectCore
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectFeedback
Imports ProjectAOI
Imports ProjectMotion
Imports ProjectConveyor
Imports ProjectLaserInterferometer
Imports WetcoConveyor
Imports System.Random
Imports MapData
Imports ProjectTriggerBoard
Imports ProjectValveController
Imports System.IO

Public Class frmOperator

    Public HeaterOn(11) As Boolean         '[說明]:紀錄Hot Plate持續加熱的開關,初始化完要再開啟
    Public gMapInfo As CMapInfo   '20170612 Toby_add


    Function GetString(ByVal value As String) As String
        Select Case value
            Case "Turn Off Heater-A"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Turn Off Heater-A"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "关闭加热器A"
                    Case enmLanguageType.eTraditionalChinese
                        Return "關閉加熱器A"
                End Select
            Case "Turn On Heater-A"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Turn On Heater-A"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "打开加热器A"
                    Case enmLanguageType.eTraditionalChinese
                        Return "打開加熱器A"
                End Select
            Case "Turn Off Heater-B"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Turn Off Heater-B"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "关闭加热器B"
                    Case enmLanguageType.eTraditionalChinese
                        Return "關閉加熱器B"
                End Select
            Case "Turn On Heater-B"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Turn On Heater-B"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "打开加热器B"
                    Case enmLanguageType.eTraditionalChinese
                        Return "打開加熱器B"
                End Select
        End Select
        Return "Undefined."
    End Function
    ''' <summary>
    ''' 取得格式化年月日時分秒
    ''' </summary>
    ''' <remarks></remarks>
    Sub GetFormatedDateNow(ByRef lblDateTime As Label)
        With Date.Now
            'If Not gfrmOpStatus Is Nothing Then
            lblDateTime.Text = .Year.ToString("0000") & "/" & .Month.ToString("00") & "/" & .Day.ToString("00") & " " & .Hour.ToString("00") & ":" & .Minute.ToString("00") & ":" & .Second.ToString("00")
            'End If
        End With
    End Sub

    ''' <summary>繪製層級</summary>
    ''' <remarks></remarks>
    Dim mDrawIdx As sLevelIndexCollection

    ''' <summary>表單已關閉</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmOperator_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        tmrUIViewerState.Enabled = False '關閉才能關Timer,避免按按鍵Timer就失效

        For mSysNo As Integer = eSys.DispStage1 To gSSystemParameter.StageMax
            gAOICollection.SetLiveTriggerMode(gSYS(mSysNo).CCDNo, eTriggerType.SoftwareTrigger)
        Next


        'Eason 20170221 Ticket:100033 , Memory Free Part4 [S]
        UcDisplay1.ManualDispose()
        UcDisplay2.ManualDispose()
        UcDisplay3.ManualDispose()
        UcDisplay4.ManualDispose()
        'Eason 20170221 Ticket:100033 , Memory Free Part4 [E]


        Me.Dispose(True)
        'Eason 20170209 Ticket:100060 :Memory Log




        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)

    End Sub


    Private Sub frmUIViewer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        '機台執行中不能關閉
        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Running Then
            If gSYS(eSys.OverAll).ExecuteCommand <> eSysCommand.Home Then
                e.Cancel = True
            End If

        End If
        If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Running Then
            If gSYS(eSys.MachineA).ExecuteCommand <> eSysCommand.Home Then
                e.Cancel = True
            End If

        End If
        If gSYS(eSys.MachineB).RunStatus = enmRunStatus.Running Then
            If gSYS(eSys.MachineB).ExecuteCommand <> eSysCommand.Home Then
                e.Cancel = True
            End If

        End If

    End Sub


    Private Sub frmOperator_Load(sender As Object, e As EventArgs) Handles Me.Load
        SuspendLayout() '先暫停更新
        Me.Text = "Machine ID: " & gSSystemParameter.MachineID & " Ver.: " & System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString()
        'Dim Str As String
        Dim arr As String() = New String(3) {}
        'Dim itm As ListViewItem
        'Dim itmAll As ListViewItem
        Dim RndNum As Random = New Random

        '20171114
        '[說明]:開始先限定為已按Reset
        gSSystemParameter.EMSResetButton = True

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                UcProcessInfo1.SetMaxValve(enmValve.No4)

                lblMachineA.Visible = True
                lblMachineB.Visible = True
                palValve1.Visible = True
                palValve2.Visible = True
                palValve3.Visible = True
                palValve4.Visible = True
                UcChuckStatus1.Visible = True
                btnChuckATurnOn.Visible = True
                UcChuckStatus2.Visible = True
                btnChuckBTurnOn.Visible = True
                palRedLineDoubleLeft.Visible = True
                palRedLineDoubleRight.Visible = True
                lblSet.Visible = True
                lblRead.Visible = True
                grpAllCCD1.Visible = True
                grpAllCCD2.Visible = True
                grpAllCCD3.Visible = True
                grpAllCCD4.Visible = True
                grpMapA.Visible = True
                grpMapB.Visible = True
                grpMapA.Text = "MAP-A"

                btnStage1.Visible = True
                btnStage2.Visible = True
                btnStage3.Visible = True
                btnStage4.Visible = True
                lblValve1.Visible = True
                lblValve2.Visible = True
                lblValve3.Visible = True
                lblValve4.Visible = True
                btnSetGlueStartTime1.Visible = True
                btnSetGlueStartTime2.Visible = True
                btnSetGlueStartTime3.Visible = True
                btnSetGlueStartTime4.Visible = True
                btnResetPCS1.Visible = True
                btnResetPCS2.Visible = True
                btnResetPCS3.Visible = True
                btnResetPCS4.Visible = True

                HotPlateA_GetSV()
                HotPlateB_GetSV()

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                UcProcessInfo1.SetMaxValve(enmValve.No2)
                lblMachineA.Visible = False
                lblMachineB.Visible = False
                palValve1.Visible = True
                palValve2.Visible = True
                palValve3.Visible = False
                palValve4.Visible = False
                UcChuckStatus1.Visible = True
                btnChuckATurnOn.Visible = True
                UcChuckStatus2.Visible = False
                btnChuckBTurnOn.Visible = False
                palRedLineDoubleLeft.Visible = True
                palRedLineDoubleRight.Visible = False
                lblSet.Visible = True
                lblRead.Visible = True
                grpAllCCD1.Visible = True
                grpAllCCD2.Visible = True
                grpAllCCD3.Visible = False
                grpAllCCD4.Visible = False
                grpMapA.Visible = True
                grpMapB.Visible = False
                grpMapA.Text = "MAP"

                btnStage1.Visible = True
                btnStage2.Visible = True
                btnStage3.Visible = False
                btnStage4.Visible = False
                lblValve1.Visible = True
                lblValve2.Visible = True
                lblValve3.Visible = False
                lblValve4.Visible = False
                btnSetGlueStartTime1.Visible = True
                btnSetGlueStartTime2.Visible = True
                btnSetGlueStartTime3.Visible = False
                btnSetGlueStartTime4.Visible = False
                btnResetPCS1.Visible = True
                btnResetPCS2.Visible = True
                btnResetPCS3.Visible = False
                btnResetPCS4.Visible = False

                cbManualMapData.Checked = Not gAutoMapPath
                If gMapDataPathA <> "" Then
                    Dim Info As New System.IO.FileInfo(gMapDataPathA)
                    tbMapDataA.Text = Info.Name
                End If

                HotPlateA_GetSV()

            Case Else
                '20170215 Jeff
                lblMachineA.Visible = False
                lblMachineB.Visible = False
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        UcProcessInfo1.SetMaxValve(enmValve.No1)
                        palValve1.Visible = True
                        palValve2.Visible = False
                        palValve3.Visible = False
                        palValve4.Visible = False

                        lblValve1.Visible = True
                        lblValve2.Visible = False
                        lblValve3.Visible = False
                        lblValve4.Visible = False

                        grpAllCCD1.Visible = True
                        grpAllCCD2.Visible = False
                        grpAllCCD3.Visible = False
                        grpAllCCD4.Visible = False
                        btnSetGlueStartTime1.Visible = True
                        btnSetGlueStartTime2.Visible = False
                        btnSetGlueStartTime3.Visible = False
                        btnSetGlueStartTime4.Visible = False
                        btnResetPCS1.Visible = True
                        btnResetPCS2.Visible = False
                        btnResetPCS3.Visible = False
                        btnResetPCS4.Visible = False
                    Case eMechanismModule.TwoValveOneStage
                        UcProcessInfo1.SetMaxValve(enmValve.No2)
                        palValve1.Visible = True
                        palValve2.Visible = True
                        palValve3.Visible = False
                        palValve4.Visible = False

                        lblValve1.Visible = True
                        lblValve2.Visible = True
                        lblValve3.Visible = False
                        lblValve4.Visible = False

                        grpAllCCD1.Visible = True
                        grpAllCCD2.Visible = False
                        grpAllCCD3.Visible = False
                        grpAllCCD4.Visible = False
                        btnSetGlueStartTime1.Visible = True
                        btnSetGlueStartTime2.Visible = True
                        btnSetGlueStartTime3.Visible = False
                        btnSetGlueStartTime4.Visible = False
                        btnResetPCS1.Visible = True
                        btnResetPCS2.Visible = True
                        btnResetPCS3.Visible = False
                        btnResetPCS4.Visible = False
                End Select

                UcChuckStatus1.Visible = False
                btnChuckATurnOn.Visible = False
                UcChuckStatus2.Visible = False
                btnChuckBTurnOn.Visible = False
                palRedLineDoubleLeft.Visible = False
                palRedLineDoubleRight.Visible = False
                lblSet.Visible = False
                lblRead.Visible = False

                grpMapA.Visible = True
                grpMapB.Visible = False
                grpMapA.Text = "MAP"
                btnStage1.Visible = True
                btnStage2.Visible = False
                btnStage3.Visible = False
                btnStage4.Visible = False

        End Select

        lblCIMStatus.Text = ""

        btnChangeStatus.Text = "Auto"
        ChangeStatus()

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                UcDCSW800AQStatus1.Visible = True

            Case Else
                UcDCSW800AQStatus1.Visible = False

        End Select
        UIViewerState_Tick(sender, e) '先更新一次介面
        tmrUIViewerState.Start()


        Select Case gAOICollection.GetCCDType(enmCCD.CCD1) 'gSSystemParameter.enmCCDType
            Case enmCCDType.CognexVPRO
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
                        UcDisplay2.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
                        UcDisplay3.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
                        UcDisplay4.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
                        grpAllCCD1.Size = New Size(406, 335)
                        grpAllCCD2.Size = New Size(406, 335)
                        grpAllCCD3.Size = New Size(406, 335)
                        grpAllCCD4.Size = New Size(406, 335)
                        grpAllCCD1.Location = New Point(6, 6)
                        grpAllCCD2.Location = New Point(416, 6)
                        grpAllCCD3.Location = New Point(826, 6)
                        grpAllCCD4.Location = New Point(1236, 6)
                    Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
                        UcDisplay2.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
                        grpAllCCD1.Size = New Size(806, 635)
                        grpAllCCD2.Size = New Size(806, 635)
                        grpAllCCD1.Location = New Point(6, 6)
                        grpAllCCD2.Location = New Point(816, 6)
                    Case Else
                        UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
                        grpAllCCD1.Size = New Size(1124, 878)
                        grpAllCCD1.Location = New Point(6, 6)
                End Select

            Case enmCCDType.OmronFZS2MUDP
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
                        UcDisplay2.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
                        UcDisplay3.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
                        UcDisplay4.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
                    Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
                        UcDisplay2.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
                    Case Else
                        UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
                End Select
            Case Else
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
                        UcDisplay2.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
                        UcDisplay3.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
                        UcDisplay4.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
                    Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
                        UcDisplay2.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
                    Case Else
                        UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
                End Select
        End Select

        ResumeLayout() '繼續配置


        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub


    Function IsCanExport() As Boolean
        If gCRecipe.strFileName = "" Then
            Return False
        End If

        Return True
    End Function


    ''' <summary>是否可開始</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsCanStart() As Boolean
        If gCRecipe.strFileName = "" Then
            Return False
        End If
        If gAOICollection.LoadSceneStatus = CAOICollection.enmStatus.Loading Then
            Return False
        End If
        If gAOICollection.LoadSceneStatus = CAOICollection.enmStatus.NG Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>顯示系統狀態</summary>
    ''' <remarks></remarks>
    Sub ShowSystemStatus()
        GetFormatedDateNow(gfrmUIViewer.lblDateTime)
        gEqpMsg.ShowAlarm(gfrmUIViewer.cboAlarmMessage)  '[說明]:只要有新的Alarm進來就更新

        Select Case gEqpInfo.Status '設備當前狀態
            Case enmEqpStatus.Alarm '異常顯示相關錯誤標題 Ex. Alarm
                lblSystemState.BackColor = Color.Red
                lblSystemState.Text = gEqpInfo.Message
                btnHome.Enabled = True
                btnExportMap.Enabled = IsCanExport()
                btnStart.Enabled = False
                btnPause.Enabled = False
                btnStop.Enabled = True
                btnMachineA.Visible = False
                btnMachineB.Visible = False
                btnStage1.Visible = False
                btnStage2.Visible = False
                btnStage3.Visible = False
                btnStage4.Visible = False
                btnChangeStatus.Enabled = True
                btnChuckATurnOn.Enabled = True
                btnChuckBTurnOn.Enabled = True
                btnAllMotorLoadRecipe.Enabled = True
                Exit Sub
                '^^^^^^^
            Case enmEqpStatus.Warning '注意, 顯示相關注意標題 Ex. Warning
                lblSystemState.BackColor = Color.Yellow
                lblSystemState.Text = gEqpInfo.Message
                btnHome.Enabled = True
                btnExportMap.Enabled = IsCanExport()
                btnStart.Enabled = IsCanStart()
                btnPause.Enabled = True
                btnStop.Enabled = True
                btnMachineA.Visible = False
                btnMachineB.Visible = False
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        btnStage1.Visible = True
                        btnStage2.Visible = True
                        btnStage3.Visible = True
                        btnStage4.Visible = True
                    Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        btnStage1.Visible = True
                        btnStage2.Visible = True
                    Case Else
                        btnStage1.Visible = True
                End Select


                btnChangeStatus.Enabled = False
                btnChuckATurnOn.Enabled = True
                btnChuckBTurnOn.Enabled = True
                btnAllMotorLoadRecipe.Enabled = True
                Exit Sub
                '^^^^^^^
            Case enmEqpStatus.HomeFinish '復歸完成
                lblSystemState.BackColor = Color.LightGreen 'Perry要求復歸完為顯示綠色. 燈號為黃色.

                If lblSystemState.Text <> gEqpInfo.Message Then
                    lblSystemState.Text = gEqpInfo.Message
                    gDOCollection.SetState(enmDO.DoorLock, False) 'A機門鎖 Soni + 2016.09.09 復歸完成解鎖
                    gDOCollection.SetState(enmDO.DoorLock2, False) 'B機門鎖 Soni + 2016.09.09 復歸完成解鎖
                End If

                btnHome.Enabled = True
                btnExportMap.Enabled = IsCanExport()
                btnStart.Enabled = IsCanStart()
                btnPause.Enabled = False
                btnStop.Enabled = False
                btnMachineA.Visible = False
                btnMachineB.Visible = False

                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        btnStage1.Visible = True
                        btnStage2.Visible = True
                        btnStage3.Visible = True
                        btnStage4.Visible = True
                        btnChuckATurnOn.Enabled = True
                        btnChuckBTurnOn.Enabled = True
                    Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        btnStage1.Visible = True
                        btnStage2.Visible = True
                        btnChuckATurnOn.Enabled = True
                        btnChuckBTurnOn.Enabled = False
                    Case Else
                        btnStage1.Visible = True
                        btnChuckATurnOn.Enabled = True
                        btnChuckBTurnOn.Enabled = False
                End Select

                btnChangeStatus.Enabled = True '停下來才能切換運行模式

                btnAllMotorLoadRecipe.Enabled = True
                Exit Sub
                '^^^^^^^
            Case enmEqpStatus.HomeStop '復歸中斷
                lblSystemState.BackColor = Color.Yellow
                lblSystemState.Text = gEqpInfo.Message
                btnHome.Enabled = True
                btnExportMap.Enabled = IsCanExport()
                btnStart.Enabled = False
                btnPause.Enabled = False
                btnStop.Enabled = False
                btnMachineA.Visible = False
                btnMachineB.Visible = False
                btnStage1.Visible = False
                btnStage2.Visible = False
                btnStage3.Visible = False
                btnStage4.Visible = False
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        btnChuckATurnOn.Enabled = True
                        btnChuckBTurnOn.Enabled = True

                    Case Else
                        btnChuckATurnOn.Enabled = True
                        btnChuckBTurnOn.Enabled = False

                End Select
                btnChangeStatus.Enabled = True '停下來才能切換運行模式
                btnAllMotorLoadRecipe.Enabled = False
                Exit Sub
                '^^^^^^^
            Case enmEqpStatus.Homing '復歸中
                lblSystemState.BackColor = Color.Yellow
                lblSystemState.Text = gEqpInfo.Message
                btnHome.Enabled = False
                btnExportMap.Enabled = False
                btnStart.Enabled = False
                btnPause.Enabled = False
                btnStop.Enabled = True
                btnMachineA.Visible = False
                btnMachineB.Visible = False
                btnStage1.Visible = False
                btnStage2.Visible = False
                btnStage3.Visible = False
                btnStage4.Visible = False
                btnChangeStatus.Enabled = False
                btnChuckATurnOn.Enabled = False
                btnChuckBTurnOn.Enabled = False
                btnAllMotorLoadRecipe.Enabled = False
                Exit Sub
                '^^^^^^^
            Case enmEqpStatus.NeedHome '需要復歸
                lblSystemState.BackColor = Color.Yellow
                lblSystemState.Text = gEqpInfo.Message
                btnHome.Enabled = True
                btnExportMap.Enabled = IsCanExport()
                btnStart.Enabled = False
                btnPause.Enabled = False
                btnStop.Enabled = False
                btnMachineA.Visible = False
                btnMachineB.Visible = False
                btnStage1.Visible = False
                btnStage2.Visible = False
                btnStage3.Visible = False
                btnStage4.Visible = False
                btnChangeStatus.Enabled = True
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        btnChuckATurnOn.Enabled = True
                        btnChuckBTurnOn.Enabled = True

                    Case Else
                        btnChuckATurnOn.Enabled = True
                        btnChuckBTurnOn.Enabled = False

                End Select

                btnAllMotorLoadRecipe.Enabled = True
                Exit Sub
                '^^^^^^^
            Case enmEqpStatus.RunFinish '生產完成
                lblSystemState.BackColor = Color.Yellow
                lblSystemState.Text = gEqpInfo.Message
                btnHome.Enabled = True
                btnExportMap.Enabled = IsCanExport()
                btnStart.Enabled = IsCanStart()
                btnPause.Enabled = False
                btnStop.Enabled = True
                btnMachineA.Visible = False
                btnMachineB.Visible = False
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        btnStage1.Visible = True
                        btnStage2.Visible = True
                        btnStage3.Visible = True
                        btnStage4.Visible = True
                    Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        btnStage1.Visible = True
                        btnStage2.Visible = True
                    Case Else
                        btnStage1.Visible = True
                End Select
                btnChangeStatus.Enabled = True '停下來才能切換運行模式
                btnChuckATurnOn.Enabled = False
                btnChuckBTurnOn.Enabled = False
                btnAllMotorLoadRecipe.Enabled = False
                Exit Sub
                '^^^^^^^
            Case enmEqpStatus.Running '生產中
                lblSystemState.BackColor = Color.LightGreen
                lblSystemState.Text = gEqpInfo.Message
                btnHome.Enabled = False
                btnExportMap.Enabled = False
                btnStart.Enabled = False
                btnPause.Enabled = True
                btnStop.Enabled = True
                btnMachineA.Visible = False
                btnMachineB.Visible = False
                btnStage1.Visible = False
                btnStage2.Visible = False
                btnStage3.Visible = False
                btnStage4.Visible = False
                btnChangeStatus.Enabled = False
                btnChuckATurnOn.Enabled = False
                btnChuckBTurnOn.Enabled = False
                btnAllMotorLoadRecipe.Enabled = False
                Exit Sub
                '^^^^^^^
            Case enmEqpStatus.RunPause '生產暫停
                lblSystemState.BackColor = Color.Yellow
                lblSystemState.Text = gEqpInfo.Message
                btnHome.Enabled = True
                btnExportMap.Enabled = IsCanExport()
                btnStart.Enabled = IsCanStart()
                btnPause.Enabled = False
                btnStop.Enabled = True

                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        btnStage1.Visible = True
                        btnStage2.Visible = True
                        btnStage3.Visible = True
                        btnStage4.Visible = True
                        btnMachineA.Visible = True
                        btnMachineB.Visible = True
                    Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        btnStage1.Visible = True
                        btnStage2.Visible = True
                        btnMachineA.Visible = True
                        btnMachineB.Visible = False
                    Case Else
                        btnStage1.Visible = True
                        btnMachineA.Visible = False
                        btnMachineB.Visible = False
                End Select
                btnChangeStatus.Enabled = False
                btnChuckATurnOn.Enabled = False
                btnChuckBTurnOn.Enabled = False
                btnAllMotorLoadRecipe.Enabled = False
                Exit Sub
                '^^^^^^^
            Case enmEqpStatus.RunPauseA 'A機暫停
                lblSystemState.BackColor = Color.Yellow
                lblSystemState.Text = gEqpInfo.Message
                btnHome.Enabled = True
                btnExportMap.Enabled = IsCanExport()
                btnStart.Enabled = IsCanStart()
                btnPause.Enabled = False
                btnStop.Enabled = True

                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        btnStage1.Visible = True
                        btnStage2.Visible = True
                        btnStage3.Visible = False
                        btnStage4.Visible = False
                        btnMachineA.Visible = True
                        btnMachineB.Visible = False
                    Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        btnStage1.Visible = True
                        btnStage2.Visible = True
                        btnMachineA.Visible = True
                        btnMachineB.Visible = False
                    Case Else
                        btnStage1.Visible = True
                        btnMachineA.Visible = True
                        btnMachineB.Visible = False
                End Select

                btnChangeStatus.Enabled = False
                btnChuckATurnOn.Enabled = False
                btnChuckBTurnOn.Enabled = False
                btnAllMotorLoadRecipe.Enabled = False
                Exit Sub
                '^^^^^^^
            Case enmEqpStatus.RunPauseB 'B機暫停
                lblSystemState.BackColor = Color.Yellow
                lblSystemState.Text = gEqpInfo.Message
                btnHome.Enabled = True
                btnExportMap.Enabled = IsCanExport()
                btnStart.Enabled = IsCanStart()
                btnPause.Enabled = False
                btnStop.Enabled = True


                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        btnStage1.Visible = False
                        btnStage2.Visible = False
                        btnStage3.Visible = True
                        btnStage4.Visible = True
                        btnMachineA.Visible = False
                        btnMachineB.Visible = True
                    Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        btnStage1.Visible = True
                        btnStage2.Visible = True
                        btnMachineA.Visible = False
                        btnMachineB.Visible = False
                    Case Else
                        btnStage1.Visible = True
                        btnMachineA.Visible = False
                End Select

                btnChangeStatus.Enabled = False
                btnChuckATurnOn.Enabled = False
                btnChuckBTurnOn.Enabled = False
                btnAllMotorLoadRecipe.Enabled = False
                Exit Sub
                '^^^^^^^

            Case enmEqpStatus.RunStop '生產中斷
                lblSystemState.BackColor = Color.Yellow
                lblSystemState.Text = gEqpInfo.Message
                btnHome.Enabled = True
                btnExportMap.Enabled = IsCanExport()
                btnStart.Enabled = False
                btnPause.Enabled = False
                btnStop.Enabled = False
                btnMachineA.Visible = False
                btnMachineB.Visible = False
                btnStage1.Visible = False
                btnStage2.Visible = False
                btnStage3.Visible = False
                btnStage4.Visible = False
                btnChangeStatus.Enabled = True '停下來才能切換運行模式
                btnChuckATurnOn.Enabled = False
                btnChuckBTurnOn.Enabled = False
                btnAllMotorLoadRecipe.Enabled = False
                Exit Sub
                '^^^^^^^
        End Select


    End Sub

    Function GetDispenseCountStatus(ByVal LifeCount As String, ByVal PasteLifeTime As Long) As String
        If IsNumeric(LifeCount) Then
            If CInt(LifeCount) - PasteLifeTime > 0 Then
                Return (CInt(LifeCount) - PasteLifeTime).ToString
            Else
                Return "Expired."
            End If
        End If
        Return "------"
    End Function

    ''' <summary>顯示膠材壽命</summary>
    ''' <remarks></remarks>
    Sub ShowGlueLife()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                UcProcessInfo1.lblFlowWeightValue1.Text = gCRecipe.GetAvgWeightPerDot(enmStage.No1, eValveWorkMode.Valve1)
                UcProcessInfo1.lblFlowWeightValue2.Text = gCRecipe.GetAvgWeightPerDot(enmStage.No2, eValveWorkMode.Valve1)
                UcProcessInfo1.lblFlowWeightValue3.Text = gCRecipe.GetAvgWeightPerDot(enmStage.No3, eValveWorkMode.Valve1)
                UcProcessInfo1.lblFlowWeightValue4.Text = gCRecipe.GetAvgWeightPerDot(enmStage.No4, eValveWorkMode.Valve1)
                UcProcessInfo1.lblValve1DispenseCount.Text = GetDispenseCountStatus(GetPasteLifeCount(enmStage.No1, eValveWorkMode.Valve1), gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).DotsCount(enmValve.No1))
                UcProcessInfo1.lblValve2DispenseCount.Text = GetDispenseCountStatus(GetPasteLifeCount(enmStage.No2, eValveWorkMode.Valve1), gSSystemParameter.StageParts.PasteLifeTime(enmStage.No2).DotsCount(enmValve.No1))
                UcProcessInfo1.lblValve3DispenseCount.Text = GetDispenseCountStatus(GetPasteLifeCount(enmStage.No3, eValveWorkMode.Valve1), gSSystemParameter.StageParts.PasteLifeTime(enmStage.No3).DotsCount(enmValve.No1))
                UcProcessInfo1.lblValve4DispenseCount.Text = GetDispenseCountStatus(GetPasteLifeCount(enmStage.No4, eValveWorkMode.Valve1), gSSystemParameter.StageParts.PasteLifeTime(enmStage.No4).DotsCount(enmValve.No1))
                UcProcessInfo1.lblPotLife1.Text = GetPasteLifeTime(enmStage.No1, eValveWorkMode.Valve1)
                UcProcessInfo1.lblPotLife2.Text = GetPasteLifeTime(enmStage.No2, eValveWorkMode.Valve1)
                UcProcessInfo1.lblPotLife3.Text = GetPasteLifeTime(enmStage.No3, eValveWorkMode.Valve1)
                UcProcessInfo1.lblPotLife4.Text = GetPasteLifeTime(enmStage.No4, eValveWorkMode.Valve1)

                UcProcessInfo1.lbGlueStartTime1.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).StartLifeTime(enmValve.No1).ToString("HH:mm:ss")
                UcProcessInfo1.lbGlueStartTime2.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No2).StartLifeTime(enmValve.No1).ToString("HH:mm:ss")
                UcProcessInfo1.lbGlueStartTime3.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No3).StartLifeTime(enmValve.No1).ToString("HH:mm:ss")
                UcProcessInfo1.lbGlueStartTime4.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No4).StartLifeTime(enmValve.No1).ToString("HH:mm:ss")
                'UcProcessInfo1.lbDotCount1.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).DotsCount(enmValve.No1)
                'UcProcessInfo1.lbDotCount2.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No2).DotsCount(enmValve.No1)
                'UcProcessInfo1.lbDotCount3.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No3).DotsCount(enmValve.No1)
                'UcProcessInfo1.lbDotCount4.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No4).DotsCount(enmValve.No1)

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                UcProcessInfo1.lblFlowWeightValue1.Text = gCRecipe.GetAvgWeightPerDot(enmStage.No1, eValveWorkMode.Valve1)
                UcProcessInfo1.lblFlowWeightValue2.Text = gCRecipe.GetAvgWeightPerDot(enmStage.No2, eValveWorkMode.Valve1)
                UcProcessInfo1.lblValve1DispenseCount.Text = GetDispenseCountStatus(GetPasteLifeCount(enmStage.No1, eValveWorkMode.Valve1), gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).DotsCount(enmValve.No1))
                UcProcessInfo1.lblValve2DispenseCount.Text = GetDispenseCountStatus(GetPasteLifeCount(enmStage.No2, eValveWorkMode.Valve1), gSSystemParameter.StageParts.PasteLifeTime(enmStage.No2).DotsCount(enmValve.No1))
                UcProcessInfo1.lblPotLife1.Text = GetPasteLifeTime(enmStage.No1, eValveWorkMode.Valve1)
                UcProcessInfo1.lblPotLife2.Text = GetPasteLifeTime(enmStage.No2, eValveWorkMode.Valve1)

                UcProcessInfo1.lbGlueStartTime1.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).StartLifeTime(enmValve.No1).ToString("HH:mm:ss")
                UcProcessInfo1.lbGlueStartTime2.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No2).StartLifeTime(enmValve.No1).ToString("HH:mm:ss")

                'UcProcessInfo1.lbDotCount1.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).DotsCount(enmValve.No1)
                'UcProcessInfo1.lbDotCount2.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No2).DotsCount(enmValve.No1)


            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        UcProcessInfo1.lblFlowWeightValue1.Text = gCRecipe.GetAvgWeightPerDot(enmStage.No1, eValveWorkMode.Valve1)
                        UcProcessInfo1.lblValve1DispenseCount.Text = GetDispenseCountStatus(GetPasteLifeCount(enmStage.No1, eValveWorkMode.Valve1), gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).DotsCount(enmValve.No1))
                        UcProcessInfo1.lblPotLife1.Text = GetPasteLifeTime(enmStage.No1, eValveWorkMode.Valve1)

                        UcProcessInfo1.lbGlueStartTime1.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).StartLifeTime(enmValve.No1).ToString("HH:mm:ss")

                        'UcProcessInfo1.lbDotCount1.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).DotsCount(enmValve.No1)

                    Case eMechanismModule.TwoValveOneStage
                        UcProcessInfo1.lblFlowWeightValue1.Text = gCRecipe.GetAvgWeightPerDot(enmStage.No1, eValveWorkMode.Valve1)
                        UcProcessInfo1.lblFlowWeightValue2.Text = gCRecipe.GetAvgWeightPerDot(enmStage.No1, eValveWorkMode.Valve2)
                        UcProcessInfo1.lblValve1DispenseCount.Text = GetDispenseCountStatus(GetPasteLifeCount(enmStage.No1, eValveWorkMode.Valve1), gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).DotsCount(enmValve.No1))
                        UcProcessInfo1.lblValve2DispenseCount.Text = GetDispenseCountStatus(GetPasteLifeCount(enmStage.No1, eValveWorkMode.Valve2), gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).DotsCount(enmValve.No2))
                        UcProcessInfo1.lblPotLife1.Text = GetPasteLifeTime(enmStage.No1, eValveWorkMode.Valve1)
                        UcProcessInfo1.lblPotLife2.Text = GetPasteLifeTime(enmStage.No1, eValveWorkMode.Valve2)

                        UcProcessInfo1.lbGlueStartTime1.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).StartLifeTime(enmValve.No1).ToString("HH:mm:ss")
                        UcProcessInfo1.lbGlueStartTime2.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).StartLifeTime(enmValve.No2).ToString("HH:mm:ss")

                        'UcProcessInfo1.lbDotCount1.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).DotsCount(enmValve.No1)
                        'UcProcessInfo1.lbDotCount2.Text = gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).DotsCount(enmValve.No2)
                End Select

        End Select
    End Sub

    Private Sub UIViewerState_Tick(sender As Object, e As EventArgs) Handles tmrUIViewerState.Tick


        '[Note]:蜂鳴器顯示更新

        If gEqpStatusHandler.BuzzerStatus = True Then
            btnMute.Image = My.Resources.BuzzerMute
        Else
            btnMute.Image = My.Resources.BuzzerOn
        End If

        '[說明]:Update RecipeName
        lblAllMachineRecipeName.Text = gCRecipe.strName
        ShowGlueLife() '顯示膠材壽命
        'ShowGlueDetector() '顯示膠量檢測
        ShowSystemStatus() '顯示系統狀態

        UcChuckStatus1.UpdatePV()
        UcChuckStatus1.UpdateSV()
        UcChuckStatus2.UpdatePV()
        UcChuckStatus2.UpdateSV()

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                UcDCSW800AQStatus1.LoaderCaseteData()
                UcDCSW800AQStatus1.UnloaderCaseteData()
                UcDCSW800AQStatus1.ShowMachineStatus() '顯示機台狀態
                ShowTempParam(lblSyringeTemp1, enmTemp.SyringeBody1, palValve1TempSyringe)
                ShowTempParam(lblSyringeTemp2, enmTemp.SyringeBody2, palValve2TempSyringe)
                ShowTempParam(lblSyringeTemp3, enmTemp.SyringeBody3, palValve3TempSyringe)
                ShowTempParam(lblSyringeTemp4, enmTemp.SyringeBody4, palValve4TempSyringe)
                'jimmy20170726
                ShowTempParam(lblValveBodyTemp1, enmTemp.ValveBody1, palValve1TempValveBody)
                ShowTempParam(lblValveBodyTemp2, enmTemp.ValveBody2, palValve2TempValveBody)
                ShowTempParam(lblValveBodyTemp3, enmTemp.ValveBody3, palValve3TempValveBody)
                ShowTempParam(lblValveBodyTemp4, enmTemp.ValveBody4, palValve4TempValveBody)
                ShowTempParam(UcProcessInfo1.lblPNozzleTempValue1, enmTemp.Nozzle1, Nothing)
                ShowTempParam(UcProcessInfo1.lblPNozzleTempValue2, enmTemp.Nozzle2, Nothing)
                ShowTempParam(UcProcessInfo1.lblPNozzleTempValue3, enmTemp.Nozzle3, Nothing)
                ShowTempParam(UcProcessInfo1.lblPNozzleTempValue4, enmTemp.Nozzle4, Nothing)
                ShowTempParam(UcProcessInfo1.lblPSyringeTempValue1, enmTemp.SyringeBody1, Nothing)
                ShowTempParam(UcProcessInfo1.lblPSyringeTempValue2, enmTemp.SyringeBody2, Nothing)
                ShowTempParam(UcProcessInfo1.lblPSyringeTempValue3, enmTemp.SyringeBody3, Nothing)
                ShowTempParam(UcProcessInfo1.lblPSyringeTempValue4, enmTemp.SyringeBody4, Nothing)
                UcProcessInfo1.lblAirPressureValue1.Text = gCRecipe.GetSyringetPressure(enmStage.No1, eValveWorkMode.Valve1)
                UcProcessInfo1.lblAirPressureValue2.Text = gCRecipe.GetSyringetPressure(enmStage.No2, eValveWorkMode.Valve1)
                UcProcessInfo1.lblAirPressureValue3.Text = gCRecipe.GetSyringetPressure(enmStage.No3, eValveWorkMode.Valve1)
                UcProcessInfo1.lblAirPressureValue4.Text = gCRecipe.GetSyringetPressure(enmStage.No4, eValveWorkMode.Valve1)
                UcProcessInfo1.lblPNozzleTempValue1.Text = gCRecipe.StageParts(enmStage.No1).NozzleTemperature(eValveWorkMode.Valve1)
                UcProcessInfo1.lblPNozzleTempValue2.Text = gCRecipe.StageParts(enmStage.No2).NozzleTemperature(eValveWorkMode.Valve1)
                UcProcessInfo1.lblPNozzleTempValue3.Text = gCRecipe.StageParts(enmStage.No3).NozzleTemperature(eValveWorkMode.Valve1)
                UcProcessInfo1.lblPNozzleTempValue4.Text = gCRecipe.StageParts(enmStage.No4).NozzleTemperature(eValveWorkMode.Valve1)
                lblNozzleTemp1.Text = gCRecipe.StageParts(enmStage.No1).NozzleTemperature(eValveWorkMode.Valve1)
                lblNozzleTemp2.Text = gCRecipe.StageParts(enmStage.No2).NozzleTemperature(eValveWorkMode.Valve1)
                lblNozzleTemp3.Text = gCRecipe.StageParts(enmStage.No3).NozzleTemperature(eValveWorkMode.Valve1)
                lblNozzleTemp4.Text = gCRecipe.StageParts(enmStage.No4).NozzleTemperature(eValveWorkMode.Valve1)

                If gCRecipe.Node(0).Count > 0 Or gCRecipe.Node(1).Count > 0 Or gCRecipe.Node(2).Count > 0 Or gCRecipe.Node(3).Count > 0 Then '節點Pattern有數量
                    If IsNothing(gMapInfo) Then
                        gMapInfo = New CMapInfo()  '20170612_Toby add
                    End If
                Else
                    UcWaferMapA.ClearMapFile("")
                    UcWaferMapB.ClearMapFile("")
                End If


                'Eason 20170302 Ticket:100090 , System Update Crash
                If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Running Then
                    Select Case gSYS(eSys.MachineA).ExecuteCommand
                        Case eSysCommand.AutoRun
                            If (gStageMap(0).IsPatternMapDataChange() Or gStageMap(1).IsPatternMapDataChange() Or gStageMap(2).IsPatternMapDataChange() Or gStageMap(3).IsPatternMapDataChange()) Then
                                Dim BufferStageMap0 As CStageMap = gStageMap(0).Clone()
                                Dim BufferStageMap1 As CStageMap = gStageMap(1).Clone()
                                Dim BufferStageMap2 As CStageMap = gStageMap(2).Clone()
                                Dim BufferStageMap3 As CStageMap = gStageMap(3).Clone()

                                If gCRecipe.Node(0).Count > 0 Or gCRecipe.Node(1).Count > 0 Then '節點Pattern有數量
                                    If UcWaferMapA.first = True Then
                                        UcWaferMapA.IniMap(gMapInfo.gDrewMapPos_L)
                                    End If
                                    UcWaferMapA.DrawStageMap(gMapInfo.gDrewMapPos_L)
                                    UcWaferMapA.PictureBox1.Refresh()
                                End If
                                If gCRecipe.Node(2).Count > 0 Or gCRecipe.Node(3).Count > 0 Then '節點Pattern有數量
                                    If UcWaferMapB.first = True Then
                                        UcWaferMapB.IniMap(gMapInfo.gDrewMapPos_R)
                                    End If
                                    UcWaferMapB.DrawStageMap(gMapInfo.gDrewMapPos_R)
                                    UcWaferMapB.PictureBox1.Refresh()
                                End If
                            End If
                    End Select
                End If

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If gCRecipe.Node(0).Count > 0 Or gCRecipe.Node(1).Count > 0 Then '節點Pattern有數量
                    If IsNothing(gMapInfo) Then
                        gMapInfo = New CMapInfo()  '20170612_Toby add
                    End If
                Else
                    UcWaferMapA.ClearMapFile("")
                    UcWaferMapB.ClearMapFile("")
                End If

                ShowTempParam(lblSyringeTemp1, enmTemp.SyringeBody1, palValve1TempSyringe)
                ShowTempParam(lblSyringeTemp2, enmTemp.SyringeBody2, palValve2TempSyringe)
                ShowTempParam(UcProcessInfo1.lblPNozzleTempValue1, enmTemp.Nozzle1, Nothing)
                ShowTempParam(UcProcessInfo1.lblPNozzleTempValue2, enmTemp.Nozzle2, Nothing)
                ShowTempParam(UcProcessInfo1.lblPSyringeTempValue1, enmTemp.SyringeBody1, Nothing)
                ShowTempParam(UcProcessInfo1.lblPSyringeTempValue2, enmTemp.SyringeBody2, Nothing)
                UcProcessInfo1.lblAirPressureValue1.Text = gCRecipe.GetSyringetPressure(enmStage.No1, eValveWorkMode.Valve1)
                UcProcessInfo1.lblAirPressureValue2.Text = gCRecipe.GetSyringetPressure(enmStage.No2, eValveWorkMode.Valve1)
                UcProcessInfo1.lblPNozzleTempValue1.Text = gCRecipe.StageParts(enmStage.No1).NozzleTemperature(eValveWorkMode.Valve1)
                UcProcessInfo1.lblPNozzleTempValue2.Text = gCRecipe.StageParts(enmStage.No2).NozzleTemperature(eValveWorkMode.Valve1)
                lblNozzleTemp1.Text = gCRecipe.StageParts(enmStage.No1).NozzleTemperature(eValveWorkMode.Valve1)
                lblNozzleTemp2.Text = gCRecipe.StageParts(enmStage.No2).NozzleTemperature(eValveWorkMode.Valve1)
                'jimmy20170726
                ShowTempParam(lblValveBodyTemp1, enmTemp.ValveBody1, palValve1TempValveBody)
                ShowTempParam(lblValveBodyTemp2, enmTemp.ValveBody2, palValve2TempValveBody)
                ShowTempParam(lblValveBodyTemp3, enmTemp.ValveBody3, palValve3TempValveBody)
                ShowTempParam(lblValveBodyTemp4, enmTemp.ValveBody4, palValve4TempValveBody)
                ShowTempParam(lblNozzleTemp1, enmTemp.Nozzle1, palValve1TempNozzle)
                ShowTempParam(lblNozzleTemp2, enmTemp.Nozzle2, palValve2TempNozzle)
                ShowTempParam(lblNozzleTemp3, enmTemp.Nozzle3, palValve3TempNozzle)
                ShowTempParam(lblNozzleTemp4, enmTemp.Nozzle4, palValve4TempNozzle)


                If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Running Then
                    Select Case gSYS(eSys.MachineA).ExecuteCommand
                        Case eSysCommand.AutoRun
                            If (gStageMap(0).IsPatternMapDataChange() Or gStageMap(1).IsPatternMapDataChange()) Then
                                If UcWaferMapA.first = True Then
                                    UcWaferMapA.IniMap(gMapInfo.gDrewMapPos_L)
                                End If
                                UcWaferMapA.DrawStageMap(gMapInfo.gDrewMapPos_L)
                                UcWaferMapA.PictureBox1.Refresh()
                            End If
                        Case Else
                    End Select
                End If

            Case Else
                If gCRecipe.Node(0).Count > 0 Then '節點Pattern有數量
                    If IsNothing(gMapInfo) Then
                        gMapInfo = New CMapInfo()  '20170612_Toby add
                    End If
                Else
                    UcWaferMapA.ClearMapFile("")
                    UcWaferMapB.ClearMapFile("")
                End If

                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        ShowTempParam(lblSyringeTemp1, enmTemp.SyringeBody1, palValve1TempSyringe)
                        ShowTempParam(UcProcessInfo1.lblPNozzleTempValue1, enmTemp.Nozzle1, Nothing)
                        ShowTempParam(UcProcessInfo1.lblPSyringeTempValue1, enmTemp.SyringeBody1, Nothing)
                        'jimmy20170726
                        ShowTempParam(lblValveBodyTemp1, enmTemp.ValveBody1, palValve1TempValveBody)
                        ShowTempParam(lblNozzleTemp1, enmTemp.ValveBody1, palValve1TempNozzle)

                        UcProcessInfo1.lblAirPressureValue1.Text = gCRecipe.GetSyringetPressure(enmStage.No1, eValveWorkMode.Valve1)
                        UcProcessInfo1.lblPNozzleTempValue1.Text = gCRecipe.StageParts(enmStage.No1).NozzleTemperature(eValveWorkMode.Valve1)
                        lblNozzleTemp1.Text = gCRecipe.StageParts(enmStage.No1).NozzleTemperature(eValveWorkMode.Valve1)


                        'Eason 20170302 Ticket:100090 , System Update Crash [S]
                        If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Running Then
                            Select Case gSYS(eSys.MachineA).ExecuteCommand
                                Case eSysCommand.AutoRun
                                    If (gStageMap(0).IsPatternMapDataChange()) Then
                                        If UcWaferMapA.first = True Then
                                            UcWaferMapA.IniMap(gMapInfo.gDrewMapPos_L)
                                        End If
                                        UcWaferMapA.DrawStageMap(gMapInfo.gDrewMapPos_L)
                                        UcWaferMapA.PictureBox1.Refresh()
                                    End If

                            End Select
                        End If
                        'Eason 20170302 Ticket:100090 , System Update Crash [E]
                    Case eMechanismModule.TwoValveOneStage
                        ShowTempParam(lblSyringeTemp1, enmTemp.SyringeBody1, palValve1TempSyringe)
                        ShowTempParam(lblSyringeTemp2, enmTemp.SyringeBody2, palValve2TempSyringe)
                        ShowTempParam(UcProcessInfo1.lblPNozzleTempValue1, enmTemp.Nozzle1, Nothing)
                        ShowTempParam(UcProcessInfo1.lblPNozzleTempValue2, enmTemp.Nozzle2, Nothing)
                        ShowTempParam(UcProcessInfo1.lblPSyringeTempValue1, enmTemp.SyringeBody1, Nothing)
                        ShowTempParam(UcProcessInfo1.lblPSyringeTempValue2, enmTemp.SyringeBody2, Nothing)
                        'jimmy20170726
                        ShowTempParam(lblValveBodyTemp1, enmTemp.ValveBody1, palValve1TempValveBody)
                        ShowTempParam(lblValveBodyTemp2, enmTemp.ValveBody2, palValve2TempValveBody)
                        ShowTempParam(lblNozzleTemp1, enmTemp.ValveBody1, palValve1TempNozzle)
                        ShowTempParam(lblNozzleTemp2, enmTemp.ValveBody2, palValve2TempNozzle)

                        UcProcessInfo1.lblAirPressureValue1.Text = gCRecipe.GetSyringetPressure(enmStage.No1, eValveWorkMode.Valve1)
                        UcProcessInfo1.lblAirPressureValue2.Text = gCRecipe.GetSyringetPressure(enmStage.No1, eValveWorkMode.Valve2)
                        UcProcessInfo1.lblPNozzleTempValue1.Text = gCRecipe.StageParts(enmStage.No1).NozzleTemperature(eValveWorkMode.Valve1)
                        UcProcessInfo1.lblPNozzleTempValue2.Text = gCRecipe.StageParts(enmStage.No1).NozzleTemperature(eValveWorkMode.Valve2)
                        lblNozzleTemp1.Text = gCRecipe.StageParts(enmStage.No1).NozzleTemperature(eValveWorkMode.Valve1)
                        lblNozzleTemp2.Text = gCRecipe.StageParts(enmStage.No1).NozzleTemperature(eValveWorkMode.Valve2)


                        'Eason 20170302 Ticket:100090 , System Update Crash [S]
                        If gSYS(eSys.MachineA).RunStatus = enmRunStatus.Running Then
                            Select Case gSYS(eSys.MachineA).ExecuteCommand
                                Case eSysCommand.AutoRun
                                    If (gStageMap(0).IsPatternMapDataChange()) Then
                                        If UcWaferMapA.first = True Then
                                            UcWaferMapA.IniMap(gMapInfo.gDrewMapPos_L)
                                        End If
                                        UcWaferMapA.DrawStageMap(gMapInfo.gDrewMapPos_L)
                                        UcWaferMapA.PictureBox1.Refresh()
                                    End If

                            End Select
                        End If
                        'Eason 20170302 Ticket:100090 , System Update Crash [E]
                End Select
        End Select

    End Sub

    ''' <summary>顯示溫度參數</summary>
    ''' <param name="lbl"></param>
    ''' <param name="index"></param>
    ''' <remarks></remarks>
    Sub ShowTempParam(ByRef lbl As Label, ByVal index As enmTemp, ByRef pal As Panel)
        If gCRecipe.TempName = "" Then
            lbl.Text = "------"
            If Not pal Is Nothing Then
                pal.Visible = False
            End If
            Exit Sub
        End If
        If Not gTempDB.ContainsKey(gCRecipe.TempName) Then
            lbl.Text = "------"
            If Not pal Is Nothing Then
                pal.Visible = False
            End If
            Exit Sub
        End If


        If gTempDB(gCRecipe.TempName).TempParam(index).Enabled Then
            lbl.Text = gTempDB(gCRecipe.TempName).TempParam(index).SetValue
            If Not pal Is Nothing Then
                pal.Visible = True
            End If
        Else
            lbl.Text = "------"
            If Not pal Is Nothing Then
                pal.Visible = False
            End If
        End If
    End Sub

    Private Sub btnMute_Click(sender As Object, e As EventArgs) Handles btnMute.Click
        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnMute]" & vbTab & "Click")
        If btnMute.Tag = "" Then
            btnMute.Tag = "1"
            gEqpStatusHandler._bCloseBuzzer = True
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6001074), "INFO_6001074")
        Else
            btnMute.Tag = ""
            gEqpStatusHandler._bCloseBuzzer = False
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6001073), "INFO_6001073")
        End If
    End Sub

#Region "更換模式"

    Public Sub ChangeStatus()
        'Select Case gSSystemParameter.enmMachineType
        '    Case enmMachineType.DCSW_800AQ
        '        Select Case gEqpInfo.RunMode
        '            Case enmMachineRunMode.AutoRun
        '                btnChangeStatus.Text = "Auto"
        '            Case enmMachineRunMode.ManualARun
        '                btnChangeStatus.Text = "Manual-A"
        '            Case enmMachineRunMode.ManualBRun
        '                btnChangeStatus.Text = "Manual-B"
        '            Case enmMachineRunMode.SingleARun
        '                btnChangeStatus.Text = "Single-A"
        '            Case enmMachineRunMode.SingleBRun
        '                btnChangeStatus.Text = "Single-B"
        '            Case enmMachineRunMode.RerunARun
        '                btnChangeStatus.Text = "Rerun-A"
        '            Case enmMachineRunMode.RerunBRun
        '                btnChangeStatus.Text = "Rerun-B"
        '        End Select
        '    Case Else
        '        Select Case gEqpInfo.RunMode
        '            Case enmMachineRunMode.AutoRun
        '                btnChangeStatus.Text = "Auto"
        '            Case enmMachineRunMode.ManualARun
        '                btnChangeStatus.Text = "Manual"
        '            Case enmMachineRunMode.SingleARun
        '                btnChangeStatus.Text = "Single"
        '            Case enmMachineRunMode.RerunARun
        '                btnChangeStatus.Text = "Rerun"
        '            Case Else '未列入狀態
        '                btnChangeStatus.Text = "Error"
        '        End Select
        'End Select

        'Toby add_20160628
        ShowMap()

    End Sub
    Private Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click

        '[說明]:狀態切換列,以後須修改為By 使用者權限切換模式
        'Select Case gSSystemParameter.enmMachineType
        '    Case enmMachineType.DCSW_800AQ
        '        Select Case gEqpInfo.RunMode
        '            Case enmMachineRunMode.AutoRun
        '                gEqpInfo.RunMode = enmMachineRunMode.ManualARun
        '            Case enmMachineRunMode.ManualARun
        '                gEqpInfo.RunMode = enmMachineRunMode.ManualBRun
        '            Case enmMachineRunMode.ManualBRun
        '                gEqpInfo.RunMode = enmMachineRunMode.SingleARun
        '            Case enmMachineRunMode.SingleARun
        '                gEqpInfo.RunMode = enmMachineRunMode.SingleBRun
        '            Case enmMachineRunMode.SingleBRun
        '                gEqpInfo.RunMode = enmMachineRunMode.RerunARun
        '            Case enmMachineRunMode.RerunARun
        '                gEqpInfo.RunMode = enmMachineRunMode.RerunBRun
        '            Case enmMachineRunMode.RerunBRun
        '                gEqpInfo.RunMode = enmMachineRunMode.AutoRun
        '        End Select
        '    Case Else
        '        Select Case gEqpInfo.RunMode
        '            Case enmMachineRunMode.AutoRun
        '                gEqpInfo.RunMode = enmMachineRunMode.ManualARun
        '            Case enmMachineRunMode.ManualARun
        '                gEqpInfo.RunMode = enmMachineRunMode.SingleARun
        '            Case enmMachineRunMode.SingleARun
        '                gEqpInfo.RunMode = enmMachineRunMode.RerunARun
        '            Case enmMachineRunMode.RerunARun
        '                gEqpInfo.RunMode = enmMachineRunMode.AutoRun
        '        End Select

        'End Select

        ChangeStatus()

    End Sub

#End Region




    ''' <summary>回前一頁</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnBack]" & vbTab & "Click")
        UcDisplay1.EndLive()
        UcDisplay2.EndLive()
        UcDisplay3.EndLive()
        UcDisplay4.EndLive()

        Me.Close()
    End Sub

    Public Sub DisabledButton(ByRef MachineType As Integer)

        Select Case MachineType
            Case eSys.MachineA
                btnMachineA.Enabled = False

            Case eSys.MachineB
                btnMachineB.Enabled = False

        End Select
    End Sub

    ''' <summary>整機停止</summary>
    ''' <remarks></remarks>
    Public Sub OverAllEmgStop()
        '所有軸停止
        For i As Integer = 0 To gCMotion.AxisParameter.Count - 1
            gCMotion.EmgStop(i)
        Next
    End Sub

    ''' <summary>復歸動作(整機,A機,B機)</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click

        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnHome]" & vbTab & "Click")

        '20171114
        If gSSystemParameter.EMSResetButton = False Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("Please Puch Resetbutton!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("請按重置按鍵!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("請按重置按鍵!!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            btnHome.Enabled = True
            Exit Sub
        End If

        If btnHome.Enabled = False Then '防連點
            Exit Sub
            '^^^^^^^
        End If
        btnHome.Enabled = False

        '[說明]:詢問是否執行動作
        If MessageBox.Show("Do you want Initial?", "My Application", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            btnHome.Enabled = True
            Exit Sub
            '^^^^^^^
        End If

        '[說明]:判斷是否有無動作
        If gSYS(eSys.MachineA).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineA).RunStatus = enmRunStatus.Running Then
            gSyslog.Save("MachineA" & gMsgHandler.GetMessage(Warn_3000006), "Warn_3000006", eMessageLevel.Warning)
            MsgBox("MachineA" & gMsgHandler.GetMessage(Warn_3000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("MachineA is Running")
            btnHome.Enabled = True
            Exit Sub
            '^^^^^^^
        End If
        If gSYS(eSys.MachineB).ExecuteCommand = eSysCommand.Home And gSYS(eSys.MachineB).RunStatus = enmRunStatus.Running Then
            gSyslog.Save("MachineB" & gMsgHandler.GetMessage(Warn_3000006), "Warn_3000006", eMessageLevel.Warning)
            MsgBox("MachineB" & gMsgHandler.GetMessage(Warn_3000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("MachineB isRunning")
            btnHome.Enabled = True
            Exit Sub
            '^^^^^^^
        End If

        '[說明]:Home 只有Runing的時候才需要進來，可按暫停停止動作。   
        For i As Integer = eSys.DispStage1 To eSys.DispStage4
            If gSYS(i).IsCanPause = False Then '不能暫停時不能復歸
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000008), "Warn_3000008", eMessageLevel.Warning)
                btnHome.Enabled = True
                Exit Sub
                '^^^^^^^
            End If
        Next

        '[說明]:啟動Thead
        Dim intTheadStatus As System.Threading.ThreadState   '[Thead的執行狀態]
        intTheadStatus = gSystemThread.ThreadState
        If intTheadStatus = Threading.ThreadState.Unstarted Then
            gSystemThread.Name = "CSystemThread"
            gSystemThread.Start() '啟動IO緒
        End If

        For i As Integer = 0 To enmAxis.Max '先ServoOff
            If gCMotion.AxisParameter(i).AxisName <> "" Then
                gCMotion.Servo(i, enmONOFF.eOff)
                gCMotion.DOOutput(i, 7, enmCardIOONOFF.eOFF) '輸出ERC
            End If
        Next
        System.Threading.Thread.CurrentThread.Join(50) '斷電時間 電容充放電
        For i As Integer = 0 To enmAxis.Max '再ServoOn以清除Alarm
            If gCMotion.AxisParameter(i).AxisName <> "" Then
                gCMotion.Servo(i, enmONOFF.eON)
                gCMotion.DOOutput(i, 7, enmCardIOONOFF.eON)  '輸出ERC
            End If
        Next
        System.Threading.Thread.CurrentThread.Join(50) '斷電時間 電容充放電
        '[說明]:清除介面上的Alarm顯示
        gEqpMsg.ClearAlarmCmpTable(Me.cboAlarmMessage)

        For stageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
            gSSystemParameter.StageParts.Purge(stageNo).ResetRuns()
            gSSystemParameter.StageParts.FlowRate(stageNo).ResetRuns()
        Next

        '--- 清除連續運動暫存指令 ---
        For i As Integer = 0 To gCMotion.SyncParameter.Count - 1
            gCMotion.GpClearMovePath(gCMotion.SyncParameter(i))
        Next
        '--- 清除連續運動暫存指令 ---


        For i As Integer = 0 To enmAxis.Max '檢查每一軸是否仍異常
            If gCMotion.AxisParameter(i).AxisName <> "" Then
                gCMotion.CheckMotorStatus(i)
                If gCMotion.AxisParameter(i).MotionIOStatus.blnALM = True Then
                    'Sue0710
                    '軸向異常
                    gEqpMsg.AddHistoryAlarm("Alarm_2000028", "btnHome_Click", 0, gCMotion.AxisParameter(i).AxisName & gMsgHandler.GetMessage(Alarm_2000028), eMessageLevel.Warning)
                    btnHome.Enabled = True
                    Exit Sub
                    '^^^^^^^
                End If
            End If
        Next

        '[說明]:是否為InterLock之Alarm
        If gInterlockCollection.IsAlarm = True Then
            If gInterlockCollection.Items(enmHardwardAlarm.CDA).Status = True Then
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000000), "Alarm_2000000", eMessageLevel.Alarm)
            ElseIf gInterlockCollection.Items(enmHardwardAlarm.EMO).Status = True Then
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000001), "Alarm_2000001", eMessageLevel.Alarm)
            ElseIf PLCM(enmPLCM.PLCAlarmInput) = True Then
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000002), "Alarm_2000002", eMessageLevel.Alarm)
            Else
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000003), "Alarm_2000003", eMessageLevel.Alarm)
            End If
            btnHome.Enabled = True
            Exit Sub
            '^^^^^^^
        End If

        For i As Integer = 0 To gSSystemParameter.StageMax
            gTriggerBoard.SetResetAlarm(i, True)
        Next

        gSYS(eSys.OverAll).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage1).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage2).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage3).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage4).ExternalPause = False '暫停清除
        gSYS(eSys.Conveyor1).ExternalPause = False '暫停清除

        '全系統狀態清除
        For mSysNo As Integer = 0 To eSys.Max
            '[Note]:監控系統不能被清除，持續監控
            Select Case mSysNo
                Case eSys.MonitorDisp1, eSys.MonitorDisp2, eSys.MonitorDisp3, eSys.MonitorDisp4

                Case Else
                    gSYS(mSysNo).RunStatus = enmRunStatus.None

            End Select
        Next

        '[說明]:紀錄Hot Plate加熱的開關 For 800A 20161010   20161206
        If gSSystemParameter.EnableInitialHotPlate = True Then
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    HeaterOn(0) = gDOCollection.GetState(enmDO.HeaterOn1)
                    HeaterOn(1) = gDOCollection.GetState(enmDO.HeaterOn2)
                    HeaterOn(2) = gDOCollection.GetState(enmDO.HeaterOn3)
                    HeaterOn(3) = gDOCollection.GetState(enmDO.HeaterOn4)
                    HeaterOn(4) = gDOCollection.GetState(enmDO.HeaterOn5)
                    HeaterOn(5) = gDOCollection.GetState(enmDO.HeaterOn6)
                    HeaterOn(6) = gDOCollection.GetState(enmDO.HeaterOn7)
                    HeaterOn(7) = gDOCollection.GetState(enmDO.HeaterOn8)
                    HeaterOn(8) = gDOCollection.GetState(enmDO.HeaterOn9)
                    HeaterOn(9) = gDOCollection.GetState(enmDO.HeaterOn10)
                    HeaterOn(10) = gDOCollection.GetState(enmDO.HeaterOn11)
                    HeaterOn(11) = gDOCollection.GetState(enmDO.HeaterOn12)
                Case enmMachineType.DCS_500AD
                    HeaterOn(0) = gDOCollection.GetState(enmDO.HeaterOn1)
                    HeaterOn(1) = gDOCollection.GetState(enmDO.HeaterOn2)
                    HeaterOn(2) = gDOCollection.GetState(enmDO.HeaterOn3)
                    HeaterOn(3) = gDOCollection.GetState(enmDO.HeaterOn4)
                    HeaterOn(4) = gDOCollection.GetState(enmDO.HeaterOn5)
                    HeaterOn(5) = gDOCollection.GetState(enmDO.HeaterOn6)
            End Select
        End If

        gWeight.InitialMFunctionWeight_WeightCounter(enmStage.Max) 'Eason 20170406
        gSYS(eSys.OverAll).Command = eSysCommand.Home
        gblnUpdateInitial = True

        '[說明]:Initial時只能點停止
        btnHome.Enabled = False
        btnStart.Enabled = False
        btnPause.Enabled = False
        btnStop.Enabled = True
        btnChangeStatus.Enabled = False '停下來才能切換運行模式

        '=== 輔助按鍵封鎖 ===
        btnMachineA.Visible = False
        btnMachineB.Visible = False
        btnStage1.Visible = False
        btnStage2.Visible = False
        btnStage3.Visible = False
        btnStage4.Visible = False
        '=== 輔助按鍵封鎖 ===

        gDOCollection.SetState(enmDO.DoorLock, True) 'A機門鎖  Soni + 2016.09.09 復歸開始上鎖
        gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖   Soni + 2016.09.09 復歸開始上鎖
    End Sub

    ''' <summary>開始生產動作(整機,A機,B機,各模式)</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click

        Dim mIsExternalPause As Boolean

        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnStart]" & vbTab & "Click")
        If btnStart.Enabled = False Then '防連點
            Exit Sub
            '^^^^^^^
        End If
        btnStart.Enabled = False '鎖定

        '[Note]:若是外部請求暫停的指令，那開始的指令是作接續的動作，而不是在下新的命令
        mIsExternalPause = gSYS(eSys.OverAll).ExternalPause

        '[說明]:回Home完成才能執行 
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.eDTS300A, enmMachineType.eDTS330A, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
                    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
                    btnStart.Enabled = True
                    Exit Sub
                    '^^^^^^^
                End If
                If gDICollection.GetState(enmDI.DoorClose, False) = True Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2081102", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Alarm_2081102), eMessageLevel.Warning)
                    btnStart.Enabled = True
                    Exit Sub
                End If
            Case enmMachineType.DCS_F230A, enmMachineType.DCS_350A
                If gSYS(eSys.MachineA).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
                    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
                    btnStart.Enabled = True
                    Exit Sub
                    '^^^^^^^
                End If
                If gDICollection.GetState(enmDI.DoorClose, False) = True Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2081102", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Alarm_2081102), eMessageLevel.Warning)
                    btnStart.Enabled = True
                    Exit Sub
                End If


            Case enmMachineType.DCSW_800AQ
                If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                    MessageBox.Show(gMsgHandler.GetMessage(Warn_3000005)) '請先復歸!!
                    gEqpMsg.AddHistoryAlarm("Warn_3000005", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Warn_3000005), eMessageLevel.Warning) 'System Home 尚未完成!!    
                    btnStart.Enabled = True
                    Exit Sub
                    '^^^^^^^
                End If
                If gDICollection.GetState(enmDI.DoorClose, False) = True Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2081102", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Alarm_2081102), eMessageLevel.Warning)
                    btnStart.Enabled = True
                    Exit Sub
                End If
                If gDICollection.GetState(enmDI.DoorClose2, False) = True Then
                    gEqpMsg.AddHistoryAlarm("Alarm_2082102", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Alarm_2082102), eMessageLevel.Warning)
                    btnStart.Enabled = True
                    Exit Sub
                End If

        End Select

        '[說明]:判斷有無開啟Recipe
        If gCRecipe.strName = "" Then
            MsgBox(gMsgHandler.GetMessage(Warn_3000011), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gEqpMsg.AddHistoryAlarm("Warn_3000011", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Warn_3000011), eMessageLevel.Warning) '找不到 Recipe 檔案!!
            btnStart.Enabled = True
            btnAllMotorLoadRecipe.Enabled = True
            Exit Sub
            '^^^^^^^
        End If

        If gAOICollection.LoadSceneStatus = CAOICollection.enmStatus.Loading Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000030))
            MsgBox(gMsgHandler.GetMessage(Warn_3000030), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Scene is Loading, Please Wait.")
            btnStart.Enabled = True
            Exit Sub
        End If
        If gAOICollection.LoadSceneStatus = CAOICollection.enmStatus.NG Then
            '場景載入失敗
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000038))
            MsgBox(gMsgHandler.GetMessage(Warn_3000038), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnStart.Enabled = True
            Exit Sub
        End If

        If gSSystemParameter.IsCompareWithMapData <> 0 Then
            '[說明]:判斷使用Map功能時Recipe有無選擇Node
            Dim isNodeReady As Boolean = True
            If (gSSystemParameter.StageMax = eSys.DispStage1) Then
                If (gCRecipe.NodeToMap(enmStage.No1) IsNot Nothing) Then
                    If (gCRecipe.NodeToMap(enmStage.No1).Count = 0) Then
                        isNodeReady = False
                    End If
                End If
            ElseIf (gSSystemParameter.StageMax = eSys.DispStage4) Then
                If (gCRecipe.NodeToMap(enmStage.No1) IsNot Nothing) AndAlso (gCRecipe.NodeToMap(enmStage.No2) IsNot Nothing) AndAlso (gCRecipe.NodeToMap(enmStage.No3) IsNot Nothing) AndAlso (gCRecipe.NodeToMap(enmStage.No4) IsNot Nothing) Then
                    If (gCRecipe.NodeToMap(enmStage.No1).Count = 0) AndAlso (gCRecipe.NodeToMap(enmStage.No2).Count = 0) AndAlso (gCRecipe.NodeToMap(enmStage.No3).Count = 0) AndAlso (gCRecipe.NodeToMap(enmStage.No4).Count = 0) Then
                        isNodeReady = False
                    End If
                End If
            End If

            If isNodeReady = False Then
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000025))
                MsgBox(gMsgHandler.GetMessage(Warn_3000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnStart.Enabled = True
                Exit Sub
            End If
        End If

        '[說明]:清除Alarm
        gEqpMsg.ClearAlarmCmpTable(Me.cboAlarmMessage)

        '[說明]:是否為InterLock之Alarm
        If gInterlockCollection.IsAlarm = True Then
            btnStart.Enabled = True
            Exit Sub
            '^^^^^^^
        End If

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gSSystemParameter.StageParts.ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                    If gDICollection.GetState(enmDI.DetectSyringeSensor1) = False Then
                        gEqpMsg.AddHistoryAlarm("Warn_3019006", "frmOperator btnStart", , gMsgHandler.GetMessage(Warn_3019006), eMessageLevel.Warning)
                        'jimmy 20170725
                        btnStart.Enabled = True
                        Exit Sub
                    End If
                End If
                If gSSystemParameter.StageParts.ValveData(enmStage.No2).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                    If gDICollection.GetState(enmDI.DetectSyringeSensor2) = False Then
                        gEqpMsg.AddHistoryAlarm("Warn_3019106", "frmOperator btnStart", , gMsgHandler.GetMessage(Warn_3019106), eMessageLevel.Warning)
                        'jimmy 20170725
                        btnStart.Enabled = True
                        Exit Sub
                    End If
                End If
                If gSSystemParameter.StageParts.ValveData(enmStage.No3).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                    If gDICollection.GetState(enmDI.DetectSyringeSensor3) = False Then
                        gEqpMsg.AddHistoryAlarm("Warn_3019206", "frmOperator btnStart", , gMsgHandler.GetMessage(Warn_3019206), eMessageLevel.Warning)
                        'jimmy 20170725
                        btnStart.Enabled = True
                        Exit Sub
                    End If
                End If
                If gSSystemParameter.StageParts.ValveData(enmStage.No4).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                    If gDICollection.GetState(enmDI.DetectSyringeSensor3) = False Then
                        gEqpMsg.AddHistoryAlarm("Warn_3019306", "frmOperator btnStart", , gMsgHandler.GetMessage(Warn_3019306), eMessageLevel.Warning)
                        'jimmy 20170725
                        btnStart.Enabled = True
                        Exit Sub
                    End If
                End If

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If gSSystemParameter.StageParts.ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                    If gDICollection.GetState(enmDI.DetectSyringeSensor1) = False Then
                        gEqpMsg.AddHistoryAlarm("Warn_3019006", "frmOperator btnStart", , gMsgHandler.GetMessage(Warn_3019006), eMessageLevel.Warning)
                        'jimmy 20170725
                        btnStart.Enabled = True
                        Exit Sub
                    End If
                End If
                If gSSystemParameter.StageParts.ValveData(enmStage.No2).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                    If gDICollection.GetState(enmDI.DetectSyringeSensor2) = False Then
                        gEqpMsg.AddHistoryAlarm("Warn_3019106", "frmOperator btnStart", , gMsgHandler.GetMessage(Warn_3019106), eMessageLevel.Warning)
                        'jimmy 20170725
                        btnStart.Enabled = True
                        Exit Sub
                    End If
                End If

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                            If gDICollection.GetState(enmDI.DetectSyringeSensor1) = False Then
                                gEqpMsg.AddHistoryAlarm("Warn_3019006", "frmOperator btnStart", , gMsgHandler.GetMessage(Warn_3019006), eMessageLevel.Warning)
                                'jimmy 20170725
                                btnStart.Enabled = True
                                Exit Sub
                            End If
                        End If

                    Case eMechanismModule.TwoValveOneStage
                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve1) = True Then
                            If gDICollection.GetState(enmDI.DetectSyringeSensor1) = False Then
                                gEqpMsg.AddHistoryAlarm("Warn_3019006", "frmOperator btnStart", , gMsgHandler.GetMessage(Warn_3019006), eMessageLevel.Warning)
                                'jimmy 20170725
                                btnStart.Enabled = True
                                Exit Sub
                            End If
                        End If
                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve2) = True Then
                            If gDICollection.GetState(enmDI.DetectSyringeSensor2) = False Then
                                gEqpMsg.AddHistoryAlarm("Warn_3019006", "frmOperator btnStart", , gMsgHandler.GetMessage(Warn_3019106), eMessageLevel.Warning)
                                'jimmy 20170725
                                btnStart.Enabled = True
                                Exit Sub
                            End If
                        End If

                End Select

        End Select

        If gEqpInfo.Status = enmEqpStatus.RunPause Then '暫停後開始, 確認項目
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    If gEqpInfo.IsW800AQPauseCanContinue = False Then
                        'gSyslog.Save(gMsgHandler.GetMessage(Warn_3000039))
                        gEqpMsg.AddHistoryAlarm("Warn_3000039", "frmUIViewer btnStart", , gMsgHandler.GetMessage(Warn_3000039), eMessageLevel.Warning)
                        MsgBox(gMsgHandler.GetMessage(Warn_3000039), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        'jimmy 20170725
                        btnStart.Enabled = True
                        Exit Sub
                        '^^^^^^^
                    End If

                Case Else

            End Select
        End If

        'If continueParam.IsProductMapNotFinish Then '未完成生產需選擇接續模式
        '    Dim gfrmRerunCheck As New frmRerunCheck
        '    gfrmRerunCheck.StartPosition = FormStartPosition.CenterParent
        '    gfrmRerunCheck.ShowDialog()
        '    continueParam.IsProductMapNotFinish = False '設定完成 
        '    Select Case continueParam.Mode
        '        Case enmContinueMode.AutoClean '強迫退料
        '            gSYS(eSys.OverAll).ExternalPause = False '暫停清除
        '            gSYS(eSys.DispStage1).ExternalPause = False '暫停清除
        '            gSYS(eSys.DispStage2).ExternalPause = False '暫停清除
        '            gSYS(eSys.DispStage3).ExternalPause = False '暫停清除
        '            gSYS(eSys.DispStage4).ExternalPause = False '暫停清除
        '            gSYS(eSys.Conveyor1).ExternalPause = False '暫停清除
        '            gDOCollection.SetState(enmDO.StartButtonLight, True)
        '            gDOCollection.SetState(enmDO.PauseButtonLight, False)
        '            gDOCollection.SetState(enmDO.StartButtonLight2, True)
        '            gDOCollection.SetState(enmDO.PauseButtonLight2, False)
        '            gDOCollection.SetState(enmDO.DoorLock, True) 'A機門鎖
        '            gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖
        '            gSYS(eSys.OverAll).Command = eSysCommand.AbnormalUnload '強迫退料

        '            '強迫退料動作
        '            btnStart.Enabled = False
        '            btnHome.Enabled = False
        '            btnPause.Enabled = True
        '            btnStop.Enabled = True
        '            btnChangeStatus.Enabled = False '停下來才能切換運行模式
        '            btnMachineA.Visible = False
        '            btnMachineB.Visible = False
        '            btnStage1.Visible = False
        '            btnStage2.Visible = False
        '            btnStage3.Visible = False
        '            btnStage4.Visible = False
        '            Exit Sub 'Soni + 2016.09.16 與正常流程切開

        '        Case enmContinueMode.ContinueRun '接續生產
        '            '請流程搭配以下參數進行動作
        '            'continueParam.UseValve1 使用閥1
        '            'continueParam.UseValve2 使用閥2
        '            'continueParam.UseValve3 使用閥3
        '            'continueParam.UseValve4 使用閥4

        '        Case enmContinueMode.ManualClean '手動取料, 按下停止
        '            'TODO: 清資料
        '            btnStart.Enabled = True
        '            btnStop.PerformClick()
        '            Exit Sub

        '    End Select
        'Else
        If MessageBox.Show("Do you want AutoRun?", "My Application", MessageBoxButtons.YesNo) = DialogResult.No Then '[說明]:詢問是否執行動作
            btnStart.Enabled = True
            Exit Sub
            '^^^^^^^
        End If

        '[Note]:發生錯誤後接續生產(ex:未載入Mapping data 錯誤,排除後接續生產)
        For mSysNo As Integer = 0 To eSys.Max   'Asa add
            Select Case mSysNo
                Case eSys.MonitorDisp1, eSys.MonitorDisp2, eSys.MonitorDisp3, eSys.MonitorDisp4

                Case Else
                    If (gSYS(mSysNo).RunStatus <> enmRunStatus.None Or gSYS(mSysNo).RunStatus <> enmRunStatus.Finish) Then
                        gSYS(mSysNo).RunStatus = enmRunStatus.Running
                    End If
            End Select
        Next

        '=== Soni + 2016.09.20 不使用VideoRun ===
        'Jimmy 20161006
        For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
            gSSystemParameter.Pos.CCDTiltVavleCalbration(mStageNo).IsVideoRun = False
        Next
        ''=== Soni + 2016.09.20 不使用VideoRun ===


        gSYS(eSys.OverAll).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage1).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage2).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage3).ExternalPause = False '暫停清除
        gSYS(eSys.DispStage4).ExternalPause = False '暫停清除
        gSYS(eSys.Conveyor1).ExternalPause = False '暫停清除

        gDOCollection.SetState(enmDO.StartButtonLight, True)
        gDOCollection.SetState(enmDO.PauseButtonLight, False)
        gDOCollection.SetState(enmDO.StartButtonLight2, True)
        gDOCollection.SetState(enmDO.PauseButtonLight2, False)
        gDOCollection.SetState(enmDO.DoorLock, True) 'A機門鎖
        gDOCollection.SetState(enmDO.DoorLock2, True) 'B機門鎖

        '[Note]:若是外部請求暫停的指令，那開始的指令是作接續的動作，而不是在下新的命令
        If mIsExternalPause = False Then

            gSYS(eSys.OverAll).Command = eSysCommand.AutoRun
            gSSystemParameter.AutoRunStartTime = DateTime.Now '生產開始時間
        End If

        '[說明]:記錄開始結束時間   20161205
        gSyslog.Save("AutoRunStartTime is " & Format(Now.Year, "0000") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Day, "00") & " " & Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00"))

        '開始執行時強制更新map_20171111
        UcWaferMapA.first = True
        UcWaferMapB.first = True

        '[說明]:Start時只能點暫停 
        btnStart.Enabled = False
        btnHome.Enabled = False
        btnPause.Enabled = True
        btnStop.Enabled = True
        btnChangeStatus.Enabled = False '停下來才能切換運行模式
        btnMachineA.Visible = False
        btnMachineB.Visible = False
        btnStage1.Visible = False
        btnStage2.Visible = False
        btnStage3.Visible = False
        btnStage4.Visible = False
    End Sub

    ''' <summary>暫停生產動作(整機,A機,B機,各模式)</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPause_Click(sender As Object, e As EventArgs) Handles btnPause.Click
        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnStop]" & vbTab & "Click")
        If btnPause.Enabled = False Then '防連點
            Exit Sub
            '^^^^^^^
        End If
        btnPause.Enabled = False

        gSYS(eSys.OverAll).ExternalPause = True '整機生產, 整機暫停
        gSYS(eSys.DispStage1).ExternalPause = True 'A機暫停
        gSYS(eSys.DispStage2).ExternalPause = True 'A機暫停
        gSYS(eSys.DispStage3).ExternalPause = True 'B機暫停
        gSYS(eSys.DispStage4).ExternalPause = True 'B機暫停

        For mSysNo As Integer = eSys.DispStage1 To gSSystemParameter.StageMax
            gAOICollection.SetLiveTriggerMode(gSYS(mSysNo).CCDNo, eTriggerType.SoftwareTrigger)
        Next

        '[說明]: 紀錄EndTime 20161205
        gSSystemParameter.AutoRunEndTime = DateTime.Now '生產開始時間
        '[說明]:記錄開始結束時間   
        gSyslog.Save("AutoRunEndTime is " & Format(Now.Year, "0000") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Day, "00") & " " & Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00"))

        btnHome.Enabled = False
        btnStart.Enabled = False
        btnPause.Enabled = False
        btnStop.Enabled = True
        btnChangeStatus.Enabled = False '完全停下來才能切換運行模式
    End Sub

    ''' <summary>
    ''' 停止生產動作(整機,A機,B機,各模式)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        gSyslog.Save("[frmUiViewer]" & vbTab & "[btnStop]" & vbTab & "Click")

        If btnStop.Enabled = False Then '防連點
            Exit Sub
            '^^^^^^^
        End If
        btnStop.Enabled = False

        gSYS(eSys.OverAll).Act(eAct.Home).RunStatus = enmRunStatus.None '必須復歸

        '全系統狀態強制中斷
        For mSysNo As Integer = 0 To eSys.Max
            '[Note]:監控系統不能被清除，持續監控
            Select Case mSysNo
                Case eSys.MonitorDisp1, eSys.MonitorDisp2, eSys.MonitorDisp3, eSys.MonitorDisp4

                Case Else
                    gSYS(mSysNo).RunStatus = enmRunStatus.Stop '強制中斷

            End Select
        Next

        Const Dec As Decimal = 3000

        '所有軸停止
        For mSysNo As Integer = eSys.DispStage1 To gSSystemParameter.StageMax ' gSysAdapter.GetSysStageMax Soni / 2017.05.11 統一配接資料來源
            gCMotion.SlowStop(gSYS(mSysNo).AxisX, Dec)
            gCMotion.SlowStop(gSYS(mSysNo).AxisY, Dec)
            gCMotion.SlowStop(gSYS(mSysNo).AxisZ, Dec)
            gCMotion.SlowStop(gSYS(mSysNo).AxisB, Dec)
            gCMotion.SlowStop(gSYS(mSysNo).AxisC, Dec)
            gAOICollection.SetLiveTriggerMode(gSYS(mSysNo).CCDNo, eTriggerType.SoftwareTrigger)
            gSYS(mSysNo).IsCanPause = True
        Next

        For i = 0 To WetcoConveyor.Unit.TempController.arrPidController.Count - 1 '對每一個溫控器, 不論是否有On
            If WetcoConveyor.Unit.TempController.arrPidController(i).PV > gSSystemParameter.SafeTemperature Then '如果溫度高於安全開門溫度. 不能開門
                btnStop.Enabled = True
                Exit Sub
            End If
        Next

        '[說明]: 紀錄EndTime 20161205
        gSSystemParameter.AutoRunEndTime = DateTime.Now '生產開始時間
        '[說明]:記錄開始結束時間   
        gSyslog.Save("AutoRunEndTime is " & Format(Now.Year, "0000") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Day, "00") & " " & Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00"))

        'TODO: 增加溫度條件保謢
        gDOCollection.SetState(enmDO.DoorLock, False) 'A機門鎖
        gDOCollection.SetState(enmDO.DoorLock2, False) 'B機門鎖
        '[說明]:按Stop後所有按鈕就可以使用了  
        btnHome.Enabled = True
        btnStart.Enabled = False
        btnPause.Enabled = False
        btnStop.Enabled = False
        btnChangeStatus.Enabled = True '停下來才能切換運行模式
    End Sub



    Private Sub SetAHotPlate()

        '[說明]:判斷有無開啟Recipe
        If gCRecipe.strName = "" Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011))
            MsgBox(gMsgHandler.GetMessage(Warn_3000011), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If gCRecipe.TempName = "" Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000032))
            MsgBox(gMsgHandler.GetMessage(Warn_3000032), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If Not gTempDB.ContainsKey(gCRecipe.TempName) Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000032))
            MsgBox(gMsgHandler.GetMessage(Warn_3000032), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '溫控器寫入SV參數
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.A1, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.A2, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.A3, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.A4, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.A5, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.A6, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        WetcoConveyor.mGlobalPool.SV = gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue

        'hot plate設定
        Dim hpA As WetcoConveyor.HotPlate

        hpA.HotPlate1 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateA1).Enabled
        hpA.HotPlate2 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateA2).Enabled
        hpA.HotPlate3 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateA3).Enabled
        hpA.HotPlate4 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateA4).Enabled
        hpA.HotPlate5 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateA5).Enabled
        hpA.HotPlate6 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateA6).Enabled


        WetcoConveyor.Unit.A_SetHotPlate(hpA)

    End Sub

    Private Sub SetBHotPlate()

        '[說明]:判斷有無開啟Recipe
        If gCRecipe.strName = "" Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000011))
            MsgBox(gMsgHandler.GetMessage(Warn_3000011), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gCRecipe.TempName = "" Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000032))
            MsgBox(gMsgHandler.GetMessage(Warn_3000032), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If Not gTempDB.ContainsKey(gCRecipe.TempName) Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000032))
            MsgBox(gMsgHandler.GetMessage(Warn_3000032), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '溫控器寫入SV參數
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.B1, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.B2, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.B3, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.B4, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.B5, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        Unit.TempController.SetSV(clsTemperatureController.enmPidController.B6, gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue)
        WetcoConveyor.mGlobalPool.SV = gTempDB(gCRecipe.TempName).TempParam(enmTemp.WorkStation).SetValue

        'hot plate設定
        Dim hpB As WetcoConveyor.HotPlate

        hpB.HotPlate1 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateB1).Enabled
        hpB.HotPlate2 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateB2).Enabled
        hpB.HotPlate3 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateB3).Enabled
        hpB.HotPlate4 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateB4).Enabled
        hpB.HotPlate5 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateB5).Enabled
        hpB.HotPlate6 = gTempDB(gCRecipe.TempName).TempParam(enmTemp.HotPlateB6).Enabled
        WetcoConveyor.Unit.B_SetHotPlate(hpB)

        '[說明]:記得還有一個IO     TODO:Jeff

    End Sub

    ''' <summary>讀取Recipe</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAllMotorLoadRecipe_Click(sender As Object, e As EventArgs) Handles btnAllMotorLoadRecipe.Click
        gSyslog.Save("[frmRecipe04]" & vbTab & "[btnLoadRecipe]" & vbTab & "Click")
        If btnAllMotorLoadRecipe.Enabled = False Then '防連點
            Exit Sub
            '^^^^^^^
        End If

        Dim mProductName As String

        btnAllMotorLoadRecipe.Enabled = False
        mProductName = "Default"

        '[說明]:檢查資料夾是否存在
        Dim DefaultDirectory As String = Application.StartupPath & "\Recipe\"
        DefaultDirectory = Application.StartupPath & "\Recipe\"

        '[說明]:選取Recipe檔案
        With OFDLoadRecipe
            .InitialDirectory = DefaultDirectory
            .Filter = "文字檔 (*.rcp)|*.rcp"
            .FilterIndex = 2
            .RestoreDirectory = True
            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                btnAllMotorLoadRecipe.Enabled = True
                Exit Sub
            End If
            lblAllMachineRecipeName.Text = .FileName
            gfrmRecipe04.LoadRecipe(.FileName) '檔案讀取
        End With



        '[Note]:Update StageMap資料
        For mStageNo As enmStage = enmStage.No1 To gSSystemParameter.StageCount - 1
            gCRecipe.Initial_StageMap(mStageNo, gSSystemParameter.IsBypassCCD)
        Next
        If CheckLoadRecipeStatus() = False Then
            btnAllMotorLoadRecipe.Enabled = True
            Exit Sub
        End If
        If gCRecipe.CheckPattern = False Then
            MsgBox("Recipe Pattern Fail !!! Please Check Pattern Path & Process Time . If Patten Path Contain Dot ,not Support RetuenTime & NextRunDelayTime Mode", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnAllMotorLoadRecipe.Enabled = True
            Exit Sub
        End If

        gMapInfo = New CMapInfo()  '20170612_Toby add
        UcWaferMapA.first = True
        UcWaferMapB.first = True

        btnAllMotorLoadRecipe.Enabled = True

    End Sub

#Region "Wafer Map 顯示"

    'Toby add 20160616
    Public Sub ShowMap()
        'Dim x(3), y(3) As Integer
        'Dim nodeS(3) As String
        'Dim mIsNG As Boolean

        'Try
        '    mIsNG = False
        '    If gCRecipe.strName <> "" Then
        '        For mStageNo = 0 To 3
        '            Dim mMultiArrayAdapter = New CMultiArrayAdapter(gCRecipe.Node(mStageNo)(nodeS(mStageNo)).Array)
        '            If gCRecipe.NodeToMap(mStageNo) <> "" Then
        '                nodeS(mStageNo) = gCRecipe.NodeToMap(mStageNo)
        '                '[Note]:確認該節點是否存在
        '                If gCRecipe.Node(mStageNo).ContainsKey(nodeS(mStageNo)) = True Then
        '                    x(mStageNo) = mMultiArrayAdapter.GetMemoryCountX()
        '                    y(mStageNo) = mMultiArrayAdapter.GetMemoryCountY()
        '                Else
        '                    mIsNG = True
        '                End If
        '            End If
        '        Next
        '    End If

        '    If mIsNG = False Then
        '        UcWaferMapA.DrawNodeMap(x(0) + x(1), y(0))
        '        UcWaferMapB.DrawNodeMap(x(2) + x(3), y(2))
        '    End If


        '    'Select Case MachineStatus
        '    '    Case enmMachineStatus.AutoRun


        '    '    Case enmMachineStatus.SingleARun, enmMachineStatus.ManualARun, enmMachineStatus.RerunARun, enmMachineStatus.SIngleBRun, enmMachineStatus.ManualBRun, enmMachineStatus.RerunBRun
        '    '        DrawNodeMap(picAllMachineWaferMapA, x(0) + x(1), y(0))
        '    '        DrawNodeMap(picAllMachineWaferMapB, x(2) + x(3), y(2))

        '    'End Select


        'Catch ex As Exception

        'End Try

        ''Toby add 20160616
    End Sub
    'Toby add 20160616__移到控制項
    'Public Function DrawNodeMap(ByRef graphicsPictureBox As PictureBox, ByRef W As Integer, ByRef H As Integer) As Boolean
    '    Dim mBitmap As Bitmap
    '    Dim mDraw As Graphics
    '    Dim mPen As New Pen(Color.Black)
    '    Dim mBrush As New Drawing.SolidBrush(Color.LightGray)
    '    Dim mRedPen As New Drawing.SolidBrush(Color.Red)
    '    Dim WhitePen As New Drawing.SolidBrush(Color.White)
    '    Dim mPointSize As Integer = CInt(graphicsPictureBox.Width / 44.8) '繪點大小
    '    Dim mScaleX As Integer '換算比率
    '    Dim mScaleY As Integer '

    '    mBitmap = graphicsPictureBox.Image


    '    mBitmap = New Bitmap(CInt(graphicsPictureBox.Width), CInt(graphicsPictureBox.Height))

    '    mDraw = Graphics.FromImage(mBitmap)

    '    '[說明]:畫外框(chuck大小)
    '    With mPen
    '        .Width = 3
    '        .DashStyle = Drawing2D.DashStyle.Solid
    '    End With
    '    '填滿
    '    mDraw.FillRectangle(mBrush, 0, 0, CInt(graphicsPictureBox.Width) - 1, CInt(graphicsPictureBox.Height) - 1)
    '    '畫線
    '    mDraw.DrawRectangle(mPen, 0, 0, CInt(graphicsPictureBox.Width) - 1, CInt(graphicsPictureBox.Height) - 1)

    '    '[說明]:畫中心線
    '    With mPen
    '        .Width = graphicsPictureBox.Width / 500
    '        .DashStyle = Drawing2D.DashStyle.Solid
    '        .Color = Color.LightGreen
    '    End With
    '    mDraw.DrawLine(mPen, 0, CInt(graphicsPictureBox.Height / 2), graphicsPictureBox.Width, CInt(graphicsPictureBox.Height / 2))
    '    mDraw.DrawLine(mPen, CInt(graphicsPictureBox.Width / 2), 0, CInt(graphicsPictureBox.Width / 2), graphicsPictureBox.Height)



    '    mScaleY = graphicsPictureBox.Height / H     '實際比例
    '    mScaleX = graphicsPictureBox.Width / W    '實際比例


    '    Select Case graphicsPictureBox.Name '紀錄die size 顯示tooltip用
    '        Case "PictureBox15", "PictureBox9", "picAMachineWaferMap", "PictureBox7"
    '            A_diesize_X = mScaleX
    '            A_diesize_Y = mScaleY
    '        Case "picAllMachineWaferMap", "PictureBox10", "picBMachineWaferMap", "PictureBox8"
    '            B_diesize_X = mScaleX
    '            B_diesize_Y = mScaleY
    '    End Select


    '    '[說明]繪製坐標系
    '    'DrawCoord(mDraw, graphicsPictureBox.Width, graphicsPictureBox.Height)
    '    '-----------------------------------------------------------------------------------------------------
    '    ' Create rectangle.

    '    'Dim D As Die = gfrmMapData.Substrates(0).DieArray(0, 0)
    '    Dim blackPen As New Pen(Color.Black)
    '    'Dim redPen As New Pen(Color.Red)       

    '    For i As Integer = 0 To H - 1
    '        For j As Integer = 0 To W - 1
    '            mDraw.DrawRectangle(blackPen, mScaleX * j, mScaleY * i, mScaleX, mScaleY)
    '        Next
    '    Next

    '    graphicsPictureBox.Image = mBitmap

    '    Return True
    '    'Toby add 20160616
    'End Function

    'Public Function DrawWafer(ByRef graphicsPictureBox As PictureBox) As Boolean
    '    Dim mBitmap As Bitmap
    '    Dim mDraw As Graphics
    '    Dim mPen As New Pen(Color.Black)
    '    Dim mBrush As New Drawing.SolidBrush(Color.LightGray)
    '    Dim mRedPen As New Drawing.SolidBrush(Color.Red)
    '    Dim WhitePen As New Drawing.SolidBrush(Color.White)
    '    Dim mPointSize As Integer = CInt(graphicsPictureBox.Width / 44.8) '繪點大小
    '    Dim mScaleX As Integer '換算比率
    '    Dim mScaleY As Integer '

    '    mBitmap = graphicsPictureBox.Image


    '    mBitmap = New Bitmap(CInt(graphicsPictureBox.Width), CInt(graphicsPictureBox.Height))


    '    mDraw = Graphics.FromImage(mBitmap)

    '    '[說明]:畫外框(chuck大小)
    '    With mPen
    '        .Width = 3
    '        .DashStyle = Drawing2D.DashStyle.Solid
    '    End With
    '    '填滿
    '    mDraw.FillRectangle(mBrush, 0, 0, CInt(graphicsPictureBox.Width) - 1, CInt(graphicsPictureBox.Height) - 1)
    '    '畫線
    '    mDraw.DrawRectangle(mPen, 0, 0, CInt(graphicsPictureBox.Width) - 1, CInt(graphicsPictureBox.Height) - 1)

    '    '[說明]:畫中心線
    '    With mPen
    '        .Width = graphicsPictureBox.Width / 500
    '        .DashStyle = Drawing2D.DashStyle.Solid
    '        .Color = Color.LightGreen
    '    End With
    '    mDraw.DrawLine(mPen, 0, CInt(graphicsPictureBox.Height / 2), graphicsPictureBox.Width, CInt(graphicsPictureBox.Height / 2))
    '    mDraw.DrawLine(mPen, CInt(graphicsPictureBox.Width / 2), 0, CInt(graphicsPictureBox.Width / 2), graphicsPictureBox.Height)



    '    mScaleY = graphicsPictureBox.Height / gfrmMapData.Substrates(0).Rows      '實際比例
    '    mScaleX = graphicsPictureBox.Width / gfrmMapData.Substrates(0).Columns    '實際比例
    '    '[說明]繪製坐標系
    '    'DrawCoord(mDraw, graphicsPictureBox.Width, graphicsPictureBox.Height)
    '    '-----------------------------------------------------------------------------------------------------
    '    ' Create rectangle.

    '    Dim D As Die = gfrmMapData.Substrates(0).DieArray(0, 0)
    '    Dim blackPen As New Pen(Color.Black)
    '    Dim redPen As New Pen(Color.Red)
    '    Dim XDie As Double = gfrmMapData.Substrates(0).Columns - 1
    '    Dim YDie As Double = gfrmMapData.Substrates(0).Rows - 1


    '    For i As Integer = 0 To YDie
    '        For j As Integer = 0 To XDie

    '            If gfrmMapData.Substrates(0).DieArray(j, i).Bin <> "." Then
    '                mDraw.DrawRectangle(blackPen, mScaleX * j, mScaleY * i, mScaleX, mScaleY)
    '            End If
    '        Next
    '    Next

    '    graphicsPictureBox.Image = mBitmap

    '    Return True
    'End Function



#End Region

#Region "Chuck加熱器啟動"
    ''' <summary>ChuckA加熱器啟動</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnChuckATurnOn_Click(sender As Object, e As EventArgs) Handles btnChuckATurnOn.Click
        Dim btn As Button = CType(sender, Button)
        Select Case gDOCollection.GetState(enmDO.HeaterOn1)
            Case False
                btn.Text = GetString("Turn Off Heater-A")
                gDOCollection.SetState(enmDO.HeaterOn1, True)
                gDOCollection.SetState(enmDO.HeaterOn2, True)
                gDOCollection.SetState(enmDO.HeaterOn3, True)
                gDOCollection.SetState(enmDO.HeaterOn4, True)
                gDOCollection.SetState(enmDO.HeaterOn5, True)
                gDOCollection.SetState(enmDO.HeaterOn6, True)
                gDOCollection.SetState(enmDO.Valve1HeaterOn, True)
                gDOCollection.SetState(enmDO.Valve2HeaterOn, True)
                UcChuckStatus1.BackColor = Color.Red
            Case True
                btn.Text = GetString("Turn On Heater-A")
                gDOCollection.SetState(enmDO.HeaterOn1, False)
                gDOCollection.SetState(enmDO.HeaterOn2, False)
                gDOCollection.SetState(enmDO.HeaterOn3, False)
                gDOCollection.SetState(enmDO.HeaterOn4, False)
                gDOCollection.SetState(enmDO.HeaterOn5, False)
                gDOCollection.SetState(enmDO.HeaterOn6, False)
                gDOCollection.SetState(enmDO.Valve1HeaterOn, False)
                gDOCollection.SetState(enmDO.Valve2HeaterOn, False)
                UcChuckStatus1.BackColor = Color.White
        End Select

    End Sub

    ''' <summary>ChuckB加熱器啟動</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnChuckBTurnOn_Click(sender As Object, e As EventArgs) Handles btnChuckBTurnOn.Click
        Dim btn As Button = CType(sender, Button)
        Select Case gDOCollection.GetState(enmDO.HeaterOn7)
            Case False
                btn.Text = GetString("Turn Off Heater-B")
                gDOCollection.SetState(enmDO.HeaterOn7, True)
                gDOCollection.SetState(enmDO.HeaterOn8, True)
                gDOCollection.SetState(enmDO.HeaterOn9, True)
                gDOCollection.SetState(enmDO.HeaterOn10, True)
                gDOCollection.SetState(enmDO.HeaterOn11, True)
                gDOCollection.SetState(enmDO.HeaterOn12, True)
                gDOCollection.SetState(enmDO.Valve3HeaterOn, True)
                gDOCollection.SetState(enmDO.Valve4HeaterOn, True)
                UcChuckStatus2.BackColor = Color.Red
            Case True
                btn.Text = GetString("Turn On Heater-B")
                gDOCollection.SetState(enmDO.HeaterOn7, False)
                gDOCollection.SetState(enmDO.HeaterOn8, False)
                gDOCollection.SetState(enmDO.HeaterOn9, False)
                gDOCollection.SetState(enmDO.HeaterOn10, False)
                gDOCollection.SetState(enmDO.HeaterOn11, False)
                gDOCollection.SetState(enmDO.HeaterOn12, False)
                gDOCollection.SetState(enmDO.Valve3HeaterOn, False)
                gDOCollection.SetState(enmDO.Valve4HeaterOn, False)
                UcChuckStatus2.BackColor = Color.White
        End Select
    End Sub
#End Region

#Region "Chuck狀態顯示(溫度/真空)"
    'Dim chuckA As frmStatusChuck
    'Dim chuckB As frmStatusChuck
    ' ''' <summary>ChuckA狀態顯示</summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Private Sub btnChuckAStatus_Click(sender As Object, e As EventArgs) Handles btnChuckAStatus.Click
    '    chuckA = New frmStatusChuck
    '    chuckA.Machine = 0
    '    Dim btn As Button = CType(sender, Button)
    '    Dim x As Integer
    '    Dim y As Integer
    '    Dim pt As Point = PointToScreen(btn.Location)
    '    x = GetLocationX(btn)
    '    y = GetLocationY(btn) + btn.Height + 30
    '    If x < 0 Then x = 0
    '    If y < 0 Then y = 0
    '    chuckA.StartPosition = FormStartPosition.Manual
    '    chuckA.Location = New Point(x, y)
    '    chuckA.Show()
    'End Sub

    ''' <summary>遞迴取得位置X</summary>
    ''' <param name="btn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLocationX(ByRef btn As Control) As Double
        If btn.Parent Is Nothing Then
            Return btn.Location.X
        Else
            Return btn.Location.X + GetLocationX(btn.Parent)
        End If
    End Function
    ''' <summary>遞迴取得位置Y</summary>
    ''' <param name="btn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLocationY(ByRef btn As Control) As Double
        If btn.Parent Is Nothing Then
            Return btn.Location.Y
        Else
            Return btn.Location.Y + GetLocationY(btn.Parent)
        End If
    End Function


    ' ''' <summary>ChuckB狀態顯示</summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Private Sub btnChuckBStatus_Click(sender As Object, e As EventArgs) Handles btnChuckBStatus.Click
    '    chuckB = New frmStatusChuck
    '    chuckB.Machine = 1
    '    Dim btn As Button = CType(sender, Button)
    '    Dim x As Integer
    '    Dim y As Integer
    '    Dim pt As Point = PointToScreen(btn.Location)
    '    x = GetLocationX(btn)
    '    y = GetLocationY(btn) + btn.Height
    '    If x < 0 Then x = 0
    '    If y < 0 Then y = 0
    '    chuckB.StartPosition = FormStartPosition.Manual
    '    chuckB.Location = New Point(x, y)
    '    chuckB.Show()
    'End Sub

#End Region

#Region "Machine操作選單"
    Private Sub btnMachineA_Click(sender As Object, e As EventArgs) Handles btnMachineA.Click
        '[說明]:新增PauseMotor顯示 20160615 Jeffadd
        'If gfrmUIViewerPauseMotor.Visible = False Then
        gfrmUIViewerPauseMotor = New frmOpMachine(Me)
        gfrmUIViewerPauseMotor.SysMachineNo = eSys.MachineA
        Dim mX As Integer = GetLocationX(btnMachineA) - gfrmUIViewerPauseMotor.Width
        gfrmUIViewerPauseMotor.Location = New Point(mX, grpPanel.Location.Y)
        gfrmUIViewerPauseMotor.Show()
        'Else
        'gfrmUIViewerPauseMotor.BringToFront()
        'End If
    End Sub

    Private Sub btnMachineB_Click(sender As Object, e As EventArgs) Handles btnMachineB.Click
        'If gfrmUIViewerPauseMotor.Visible = False Then
        gfrmUIViewerPauseMotor = New frmOpMachine(Me)
        gfrmUIViewerPauseMotor.SysMachineNo = eSys.MachineB
        Dim mX As Integer = GetLocationX(btnMachineA) - gfrmUIViewerPauseMotor.Width
        gfrmUIViewerPauseMotor.Location = New Point(mX, grpPanel.Location.Y)
        gfrmUIViewerPauseMotor.Show()
        'Else
        'gfrmUIViewerPauseMotor.BringToFront()
        'End If
    End Sub

#End Region

#Region "閥操作"
    '20161129
    Private Sub btnStage1_Click(sender As Object, e As EventArgs) Handles btnStage1.Click
        Dim frmValve As New frmOpStage(eSys.DispStage1, gSYS(eSys.SubDisp1))
        Dim btn As Button = CType(sender, Button)
        Dim mPosX As Integer = GetLocationX(btn) - frmValve.Width
        Dim mPosY As Integer = GetLocationY(btn) - frmValve.Height + btn.Height + 30
        frmValve.StartPosition = FormStartPosition.Manual
        frmValve.Location = New Point(mPosX, mPosY)
        frmValve.Show()
    End Sub

    Private Sub btnStage2_Click(sender As Object, e As EventArgs) Handles btnStage2.Click
        Dim frmValve As New frmOpStage(eSys.DispStage2, gSYS(eSys.SubDisp2))
        Dim btn As Button = CType(sender, Button)
        Dim mPosX As Integer = GetLocationX(btn) - frmValve.Width
        Dim mPosY As Integer = GetLocationY(btn) - frmValve.Height + btn.Height + 30
        frmValve.StartPosition = FormStartPosition.Manual
        frmValve.Location = New Point(mPosX, mPosY)
        frmValve.Show()
    End Sub

    Private Sub btnStage3_Click(sender As Object, e As EventArgs) Handles btnStage3.Click
        Dim frmValve As New frmOpStage(eSys.DispStage3, gSYS(eSys.SubDisp3))
        Dim btn As Button = CType(sender, Button)
        Dim mPosX As Integer = GetLocationX(btn) - frmValve.Width
        Dim mPosY As Integer = GetLocationY(btn) - frmValve.Height + btn.Height + 30
        frmValve.StartPosition = FormStartPosition.Manual
        frmValve.Location = New Point(mPosX, mPosY)
        frmValve.Show()
    End Sub

    Private Sub btnStage4_Click(sender As Object, e As EventArgs) Handles btnStage4.Click
        Dim frmValve As New frmOpStage(eSys.DispStage4, gSYS(eSys.SubDisp4))
        Dim btn As Button = CType(sender, Button)
        Dim mPosX As Integer = GetLocationX(btn) - frmValve.Width
        Dim mPosY As Integer = GetLocationY(btn) - frmValve.Height + btn.Height + 30
        frmValve.StartPosition = FormStartPosition.Manual
        frmValve.Location = New Point(mPosX, mPosY)
        frmValve.Show()
    End Sub
#End Region


    Public Sub cboShowData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboShowData.SelectedIndexChanged
        UcWaferMapA.first = True
        UcWaferMapB.first = True
        UcWaferMapA.ShowBIN = cboShowData.SelectedIndex
        UcWaferMapB.ShowBIN = cboShowData.SelectedIndex
    End Sub


    Private Sub btnSetGlueStartTime1_Click(sender As Object, e As EventArgs) Handles btnSetGlueStartTime1.Click
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).StartLifeTime(enmValve.No1) = Now
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).Save("D:\PIIData\SysConfigStage1.ini")
    End Sub

    Private Sub btnResetPCS1_Click(sender As Object, e As EventArgs) Handles btnResetPCS1.Click
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).DotsCount(enmValve.No1) = 0
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).Save("D:\PIIData\SysConfigStage1.ini")
    End Sub

    Private Sub btnSetGlueStartTime2_Click(sender As Object, e As EventArgs) Handles btnSetGlueStartTime2.Click
        Select Case gSSystemParameter.StageUseValveCount
            Case eMechanismModule.OneValveOneStage
                gSSystemParameter.StageParts.PasteLifeTime(enmStage.No2).StartLifeTime(enmValve.No1) = Now
                gSSystemParameter.StageParts.PasteLifeTime(enmStage.No2).Save("D:\PIIData\SysConfigStage2.ini")
            Case eMechanismModule.TwoValveOneStage
                gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).StartLifeTime(enmValve.No2) = Now
                gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).Save("D:\PIIData\SysConfigStage1.ini")
        End Select
    End Sub

    Private Sub btnResetPCS2_Click(sender As Object, e As EventArgs) Handles btnResetPCS2.Click
        Select Case gSSystemParameter.StageUseValveCount
            Case eMechanismModule.OneValveOneStage
                gSSystemParameter.StageParts.PasteLifeTime(enmStage.No2).DotsCount(enmValve.No1) = 0
                gSSystemParameter.StageParts.PasteLifeTime(enmStage.No2).Save("D:\PIIData\SysConfigStage2.ini")
            Case eMechanismModule.TwoValveOneStage
                gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).DotsCount(enmValve.No2) = 0
                gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).Save("D:\PIIData\SysConfigStage1.ini")
        End Select
    End Sub

    Private Sub btnSetGlueStartTime3_Click(sender As Object, e As EventArgs) Handles btnSetGlueStartTime3.Click
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No3).StartLifeTime(enmValve.No1) = Now
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No3).Save("D:\PIIData\SysConfigStage3.ini")
    End Sub

    Private Sub btnResetPCS3_Click(sender As Object, e As EventArgs) Handles btnResetPCS3.Click
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No3).DotsCount(enmValve.No1) = 0
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No3).Save("D:\PIIData\SysConfigStage3.ini")
    End Sub

    Private Sub btnSetGlueStartTime4_Click(sender As Object, e As EventArgs) Handles btnSetGlueStartTime4.Click
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No4).StartLifeTime(enmValve.No1) = Now
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No4).Save("D:\PIIData\SysConfigStage4.ini")
    End Sub

    Private Sub btnResetPCS4_Click(sender As Object, e As EventArgs) Handles btnResetPCS4.Click
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No4).DotsCount(enmValve.No1) = 0
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No4).Save("D:\PIIData\SysConfigStage4.ini")
    End Sub

    Private Sub btnSetGlueStartTime1_Click_1(sender As Object, e As EventArgs) Handles btnSetGlueStartTime1.Click
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).StartLifeTime(enmValve.No1) = Now
        gSSystemParameter.StageParts.PasteLifeTime(enmStage.No1).Save("D:\PIIData\SysConfigStage1.ini")
    End Sub

    Private Sub tabControl3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabControl3.SelectedIndexChanged

    End Sub

    Private Sub btnMacAOpenMap_Click(sender As Object, e As EventArgs) Handles btnMacAOpenMap.Click
        Dim openDialog As New OpenFileDialog
        If (openDialog.ShowDialog = Windows.Forms.DialogResult.OK) Then
            MFunctionModule.gMapDataPathA = openDialog.FileName
            tbMapDataA.Text = openDialog.SafeFileName
        End If
    End Sub

    Private Sub cbManualMapData_CheckedChanged(sender As Object, e As EventArgs) Handles cbManualMapData.CheckedChanged
        If (cbManualMapData.Checked) Then
            btnMacAOpenMap.Enabled = True
            gAutoMapPath = False
        Else
            tbMapDataA.Text = ""
            gMapDataPathA = ""
            btnMacAOpenMap.Enabled = False
            gAutoMapPath = True
        End If
    End Sub

    Private Sub frmOperator_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If (gSSystemParameter.IsCompareWithMapData <> 0) Then   'Asa add
            grpMapFile.Enabled = True
            cbManualMapData.Enabled = True

            If (gSSystemParameter.IsCompareWithMapData = 2) Then
                cbManualMapData.Checked = False
                cbManualMapData.Enabled = False
            ElseIf (gSSystemParameter.IsCompareWithMapData = 3) Then
                cbManualMapData.Checked = True
                cbManualMapData.Enabled = False
            End If
        Else
            grpMapFile.Enabled = False
        End If
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnExportMap.Click
        Dim name As String = ""
        Dim fileName As String = gSSystemParameter.RerunDataFolderPath
        If (gSSystemParameter.IsCompareWithMapData <> 0) Then
            '取得Map檔名
            Dim path As String() = Split(gMapDataPathA, "\")
            name = path(path.Length - 1)
        End If

        '檔案路徑
        fileName = fileName & "_" & System.DateTime.Now.ToString("yyyyMMdd_HH.mm.ss") & ".txt"

        '輸出ASE map
        If OutputRerunMap(enmMachineStation.MachineA, fileName) Then
            MsgBox("RerunMap: " & fileName & " SUCCES", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        Else
            MsgBox("RerunMap file ERROR", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    Private Sub UcDCSW800AQStatus1_Load(sender As Object, e As EventArgs) Handles UcDCSW800AQStatus1.Load

    End Sub

    Private Sub HotPlateA_GetSV()
        Unit.TempController.GetSV(clsTemperatureController.enmPidController.A1)
        Unit.TempController.GetSV(clsTemperatureController.enmPidController.A2)
        Unit.TempController.GetSV(clsTemperatureController.enmPidController.A3)
        Unit.TempController.GetSV(clsTemperatureController.enmPidController.A4)
        Unit.TempController.GetSV(clsTemperatureController.enmPidController.A5)
        Unit.TempController.GetSV(clsTemperatureController.enmPidController.A6)
    End Sub

    Private Sub HotPlateB_GetSV()
        Unit.TempController.GetSV(clsTemperatureController.enmPidController.B1)
        Unit.TempController.GetSV(clsTemperatureController.enmPidController.B2)
        Unit.TempController.GetSV(clsTemperatureController.enmPidController.B3)
        Unit.TempController.GetSV(clsTemperatureController.enmPidController.B4)
        Unit.TempController.GetSV(clsTemperatureController.enmPidController.B5)
        Unit.TempController.GetSV(clsTemperatureController.enmPidController.B6)
    End Sub
End Class
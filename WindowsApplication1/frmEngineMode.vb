Imports ProjectCore
Imports ProjectIO
Imports ProjectConveyor
Imports ProjectAOI
Imports ProjectValveController

Public Class frmEngineMode

    ''' <summary>按鍵依序排列</summary>
    ''' <param name="btnList"></param>
    ''' <remarks></remarks>
    Sub AppendButton(ByRef btnList As List(Of Button))
        Dim basicPosX As Integer
        Dim basicPosY As Integer
        Dim height As Integer
        If btnList.count > 0 Then
            basicPosX = btnList(0).Location.X
            basicPosY = btnList(0).Location.Y
            height = btnList(0).Height
        End If
        Dim visibleBtnId As Integer = 0
        For i As Integer = 0 To btnList.Count - 1
            If btnList(i).Visible Then
                btnList(i).Location = New Point(basicPosX, basicPosY + visibleBtnId * height)
                visibleBtnId += 1
            End If
        Next

    End Sub

    Private Sub frmEngineMode_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '= GetUserLevel(gSSystemParameter.UserLevelPurview.ClearPos, gUserLevel)

        'gfrmMain.btnRecipe.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.Recipe), gUserLevel)
        'gfrmMain.btnCalibration.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.Calibration), gUserLevel)
        'gfrmMain.btnEngineMode.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.EngineMode), gUserLevel)

        btnManual.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.Manual), gUserLevel)
        btnIOList.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.IOSetup), gUserLevel)
        'btnHandshake.Enabled = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetModuleConveyor), gUserLevel)
        btnSetUserLevel.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetUserLevel), gUserLevel)
        btnSystemSet.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.setHardwareConfig), gUserLevel)
        btnIOSet.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.IOTable), gUserLevel)
        'btnCCDHandshake.Enabled = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetModuleAOI), gUserLevel)
        btnValveContler.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetValveController), gUserLevel)
        btnIndicator.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetModuleTowerLight), gUserLevel)
        btnSetMessage.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetMessageLanguage), gUserLevel)
        btnVacuumKit.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetPartHotPlate), gUserLevel)
        btnSetCCD.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetCCD), gUserLevel)
        btnLight.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetLight), gUserLevel)
        btnSetTriggerController.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetTriggerController), gUserLevel)
        btnScale.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetBalance), gUserLevel)
        btnSetLaserReader.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetLaserReader), gUserLevel)
        btnSetFMCS.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetFMCS), gUserLevel)
        btnSetInterlock.Visible = GetUserLevel(enmUserLevel.eSoftwareMaker, gUserLevel) '限定軟體開發人員權限
        btnConveyorManual.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetConveyor), gUserLevel)

        btnElectricCylinder.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetElectricCylinder), gUserLevel)
        btnSetTemp.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetTemperature), gUserLevel)
        btnSetEPV.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetElectroPneumaticValve), gUserLevel)
        btnStageSafe.Visible = GetUserLevel(gSSystemParameter.UserAuth(enmUserAuthItem.SetStageSafe), gUserLevel)

        btnSetFMCS.Visible = False '先強制封印
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                btnStageSafe.Visible = True
                btnElectricCylinder.Visible = True
                btnVacuumKit.Visible = True
                btnSetTemp.Visible = True

            Case enmMachineType.DCS_350A
                btnStageSafe.Visible = False
                btnElectricCylinder.Visible = False
                btnVacuumKit.Visible = False
                btnSetTemp.Visible = True

            Case Else
                btnStageSafe.Visible = False
                btnElectricCylinder.Visible = False
                btnVacuumKit.Visible = False
                btnSetTemp.Visible = False

        End Select


        Dim btnList As New List(Of Button)
        btnList.Add(btnManual)
        btnList.Add(btnIOList)
        btnList.Add(btnIOSet)
        AppendButton(btnList)

        btnList = New List(Of Button)
        AppendButton(btnList)

        btnList = New List(Of Button)
        btnList.Add(btnSetTemp)
        btnList.Add(btnVacuumKit)
        btnList.Add(btnElectricCylinder)
        btnList.Add(btnConveyorManual)
        AppendButton(btnList)

        btnList = New List(Of Button)
        btnList.Add(btnSystemSet)
        btnList.Add(btnSetInterlock)
        btnList.Add(btnSetUserLevel)
        btnList.Add(btnIndicator)
        btnList.Add(btnSetMessage)
        btnList.Add(btnStageSafe)
        AppendButton(btnList)

        btnList = New List(Of Button)
        btnList.Add(btnSetCCD)
        btnList.Add(btnLight)
        btnList.Add(btnSetTriggerController)
        btnList.Add(btnScale)
        btnList.Add(btnSetLaserReader)
        btnList.Add(btnSetEPV)
        btnList.Add(btnValveContler)
        btnList.Add(btnSetFMCS)
        AppendButton(btnList)

        If gSSystemParameter.MachineType = enmMachineType.DCSW_800AQ Then
            btnValveContler.Visible = True
        Else
            btnValveContler.Visible = False
        End If
    End Sub


    Private Sub btnManual_Click(sender As Object, e As EventArgs) Handles btnManual.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnManual]" & vbTab & "Click")
        If btnManual.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnManual.Enabled = False
        If gfrmManual Is Nothing Then
            gfrmManual = New frmManual
        ElseIf gfrmManual.IsDisposed Then
            gfrmManual = New frmManual
        End If
        'Using mfrmManual As New frmManual
        gfrmManual.StartPosition = FormStartPosition.CenterParent
        gfrmManual.ShowDialog()
        gfrmManual.Dispose()
        'End Using
        'Dim mfrmManual As New frmManual

        btnManual.Enabled = True
    End Sub

    Private Sub btnIOList_Click(sender As Object, e As EventArgs) Handles btnIOList.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnIOList]" & vbTab & "Click")
        If btnIOList.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnIOList.Enabled = False
        If gfrmIOList Is Nothing Then
            gfrmIOList = New frmIOList
        ElseIf gfrmIOList.IsDisposed Then
            gfrmIOList = New frmIOList
        End If
        gfrmIOList.StartPosition = FormStartPosition.CenterParent
        gfrmIOList.WindowState = FormWindowState.Maximized
        gfrmIOList.ShowDialog()
        gfrmIOList.Dispose()
        btnIOList.Enabled = True
    End Sub


    Private Sub btnSystemSet_Click(sender As Object, e As EventArgs) Handles btnSystemSet.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnSystemSet]" & vbTab & "Click")
        If btnSystemSet.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnSystemSet.Enabled = False

        If gfrmSystemSet Is Nothing Then
            gfrmSystemSet = New frmSystemSet
        ElseIf gfrmSystemSet.IsDisposed Then
            gfrmSystemSet = New frmSystemSet
        End If

        gfrmSystemSet.sys1 = gSYS(eSys.DispStage1)
        gfrmSystemSet.sys2 = gSYS(eSys.DispStage2)
        gfrmSystemSet.sys3 = gSYS(eSys.DispStage3)
        gfrmSystemSet.sys4 = gSYS(eSys.DispStage4)
        gfrmSystemSet.StartPosition = FormStartPosition.CenterParent
        gfrmSystemSet.WindowState = FormWindowState.Maximized
        gfrmSystemSet.ShowDialog()
        gfrmSystemSet.Dispose()
        btnSystemSet.Enabled = True
    End Sub

    Private Sub btnIOSet_Click(sender As Object, e As EventArgs) Handles btnIOSet.Click
        '[說明]:紀錄按了哪些按鈕
        'Call WriteButtonLog(gUserLevel, "frmEngineMode", "btnIOSet")
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnIOSet]" & vbTab & "Click")
        If btnIOSet.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnIOSet.Enabled = False
        If gfrmIOTable Is Nothing Then
            gfrmIOTable = New frmIOTable
        ElseIf gfrmIOTable.IsDisposed Then
            gfrmIOTable = New frmIOTable
        End If

        gfrmIOTable.StartPosition = FormStartPosition.CenterParent
        gfrmIOTable.WindowState = FormWindowState.Maximized
        gfrmIOTable.ShowDialog()
        gfrmIOTable.Dispose()
        btnIOSet.Enabled = True
    End Sub
    Dim frmConveyorPLC As New frmPLC
    Dim FrmConveyor As New frmConveyor


   
    Private Sub btnSetUserLevel_Click(sender As Object, e As EventArgs) Handles btnSetUserLevel.Click
        '[說明]:紀錄按了哪些按鈕
        'Call WriteButtonLog(gUserLevel, "frmEngineMode", "btnSetUserLevel")
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnSetUserLevel]" & vbTab & "Click")
        If btnSetUserLevel.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnSetUserLevel.Enabled = False
        If gfrmSetUserLevel Is Nothing Then
            gfrmSetUserLevel = New frmSetUserLevel
        ElseIf gfrmSetUserLevel.IsDisposed Then
            gfrmSetUserLevel = New frmSetUserLevel
        End If

        gfrmSetUserLevel.StartPosition = FormStartPosition.CenterScreen
        gfrmSetUserLevel.ShowDialog()
        gfrmSetUserLevel.Dispose()
        btnSetUserLevel.Enabled = True
    End Sub

    'Private Sub btnPurview_Click(sender As Object, e As EventArgs)
    '    '[說明]:紀錄按了哪些按鈕
    '    'Call WriteButtonLog(gUserLevel, "frmEngineMode", "btnPurview")
    '    gSyslog.Save("[frmEngineMode]" & vbTab & "[btnPurview]" & vbTab & "Click")
    '    If gfrmUserAuth Is Nothing Then
    '        gfrmUserAuth = New frmUserAuth
    '    ElseIf gfrmUserAuth.IsDisposed Then
    '        gfrmUserAuth = New frmUserAuth
    '    End If
    '    gfrmUserAuth.StartPosition = FormStartPosition.CenterParent
    '    gfrmUserAuth.ShowDialog()

    'End Sub

    Private Sub btnIndicator_Click(sender As Object, e As EventArgs) Handles btnIndicator.Click
        '[說明]:紀錄按了哪些按鈕
        'Call WriteButtonLog(gUserLevel, "frmEngineMode", "btnIndicator")
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnIndicator]" & vbTab & "Click")
        If btnIndicator.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnIndicator.Enabled = False
        If gfrmLightTowerConfig Is Nothing Then
            gfrmLightTowerConfig = New frmLightTowerConfig
        ElseIf gfrmLightTowerConfig.IsDisposed Then
            gfrmLightTowerConfig = New frmLightTowerConfig
        End If

        gfrmLightTowerConfig.StartPosition = FormStartPosition.CenterScreen
        gfrmLightTowerConfig.ShowDialog()
        gfrmLightTowerConfig.Dispose()
        btnIndicator.Enabled = True
    End Sub

    Private Sub btnScale_Click(sender As Object, e As EventArgs) Handles btnScale.Click
        '[說明]:紀錄按了哪些按鈕
        'Call WriteButtonLog(gUserLevel, "frmEngineMode", "btnScale")
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnScale]" & vbTab & "Click")
        If btnScale.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnScale.Enabled = False
        If gfrmSetBalance Is Nothing Then
            gfrmSetBalance = New frmSetBalance
        ElseIf gfrmSetBalance.IsDisposed Then
            gfrmSetBalance = New frmSetBalance
        End If

        gfrmSetBalance.StartPosition = FormStartPosition.CenterScreen
        gfrmSetBalance.ShowDialog()
        gfrmSetBalance.Dispose()
        btnScale.Enabled = True
    End Sub

    Private Sub btnLight_Click(sender As Object, e As EventArgs) Handles btnLight.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnLight]" & vbTab & "Click")
        If btnLight.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnLight.Enabled = False
        If gfrmSetLightConnection Is Nothing Then
            gfrmSetLightConnection = New frmSetLightConnection
        ElseIf gfrmSetLightConnection.IsDisposed Then
            gfrmSetLightConnection = New frmSetLightConnection
        End If

        gfrmSetLightConnection.StartPosition = FormStartPosition.CenterScreen
        gfrmSetLightConnection.ShowDialog()
        gfrmSetLightConnection.Dispose()
        btnLight.Enabled = True
    End Sub

    Private Sub btnSetCCD_Click(sender As Object, e As EventArgs) Handles btnSetCCD.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnSetCCD]" & vbTab & "Click")
        If btnSetCCD.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnSetCCD.Enabled = False
        If gfrmSetCCDConnection Is Nothing Then
            gfrmSetCCDConnection = New frmSetCCDConnection
        ElseIf gfrmSetCCDConnection.IsDisposed Then
            gfrmSetCCDConnection = New frmSetCCDConnection
        End If

        gfrmSetCCDConnection.StartPosition = FormStartPosition.CenterScreen
        gfrmSetCCDConnection.ShowDialog()
        gfrmSetCCDConnection.Dispose()
        btnSetCCD.Enabled = True
    End Sub

    Private Sub btnSetLaserReader_Click(sender As Object, e As EventArgs) Handles btnSetLaserReader.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnSetLaserReader]" & vbTab & "Click")
        If btnSetLaserReader.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnSetLaserReader.Enabled = False
        If gfrmSetLaserReaderConnection Is Nothing Then
            gfrmSetLaserReaderConnection = New frmSetLaserReaderConnection
        ElseIf gfrmSetLaserReaderConnection.IsDisposed Then
            gfrmSetLaserReaderConnection = New frmSetLaserReaderConnection
        End If

        gfrmSetLaserReaderConnection.StartPosition = FormStartPosition.CenterScreen
        gfrmSetLaserReaderConnection.ShowDialog()
        gfrmSetLaserReaderConnection.Dispose()
        btnSetLaserReader.Enabled = True
    End Sub

    Private Sub btnSetTriggerController_Click(sender As Object, e As EventArgs) Handles btnSetTriggerController.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnSetTriggerController]" & vbTab & "Click")
        If btnSetTriggerController.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnSetTriggerController.Enabled = False
        If gfrmSetTriggerConnection Is Nothing Then
            gfrmSetTriggerConnection = New frmSetTriggerConnection
        ElseIf gfrmSetTriggerConnection.IsDisposed Then
            gfrmSetTriggerConnection = New frmSetTriggerConnection
        End If

        gfrmSetTriggerConnection.StartPosition = FormStartPosition.CenterScreen
        gfrmSetTriggerConnection.ShowDialog()
        gfrmSetTriggerConnection.Dispose()
        btnSetTriggerController.Enabled = True
    End Sub


    Private Sub btnSetFMCS_Click(sender As Object, e As EventArgs) Handles btnSetFMCS.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnSetFMCS]" & vbTab & "Click")
        If btnSetFMCS.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnSetFMCS.Enabled = False
        If gfrmSetFMCS Is Nothing Then
            gfrmSetFMCS = New frmSetFMCSConnection
        ElseIf gfrmSetFMCS.IsDisposed Then
            gfrmSetFMCS = New frmSetFMCSConnection
        End If

        gfrmSetFMCS.StartPosition = FormStartPosition.CenterScreen
        gfrmSetFMCS.ShowDialog()
        gfrmSetFMCS.Dispose()
        btnSetFMCS.Enabled = True
    End Sub

    Private Sub btnValveContler_Click(sender As Object, e As EventArgs) Handles btnValveContler.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnValveControler]" & vbTab & "Click")
        If btnValveContler.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnValveContler.Enabled = False
        If gfrmValveControler Is Nothing Then
            gfrmValveControler = New frmPicoValveControllerTest
        ElseIf gfrmValveControler.IsDisposed Then
            gfrmValveControler = New frmPicoValveControllerTest
        End If

        gfrmValveControler.StartPosition = FormStartPosition.CenterScreen
        gfrmValveControler.ShowDialog()
        gfrmValveControler.Dispose()
        btnValveContler.Enabled = True
    End Sub

    Private Sub btnSetMessage_Click(sender As Object, e As EventArgs) Handles btnSetMessage.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnSetMessage]" & vbTab & "Click")
        If btnSetMessage.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnSetMessage.Enabled = False
        If gfrmMessageConfig Is Nothing Then
            gfrmMessageConfig = New frmMessageConfig
        ElseIf gfrmMessageConfig.IsDisposed Then
            gfrmMessageConfig = New frmMessageConfig
        End If

        gfrmMessageConfig.StartPosition = FormStartPosition.CenterScreen
        gfrmMessageConfig.WindowState = FormWindowState.Maximized
        gfrmMessageConfig.ShowDialog()
        gfrmMessageConfig.Dispose()
        btnSetMessage.Enabled = True
    End Sub

    Private Sub btnSetInterlock_Click(sender As Object, e As EventArgs) Handles btnSetInterlock.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnSetInterlock]" & vbTab & "Click")
        If btnSetInterlock.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnSetInterlock.Enabled = False
        If gfrmInterlock Is Nothing Then
            gfrmInterlock = New frmInterlockConfig
        ElseIf gfrmInterlock.IsDisposed Then
            gfrmInterlock = New frmInterlockConfig
        End If

        gfrmInterlock.StartPosition = FormStartPosition.CenterScreen
        gfrmInterlock.ShowDialog()
        gfrmInterlock.Dispose()
        btnSetInterlock.Enabled = True
    End Sub

    Private Sub btnVacuumKit_Click(sender As Object, e As EventArgs) Handles btnVacuumKit.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnVacuumKit]" & vbTab & "Click")
        If btnVacuumKit.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnVacuumKit.Enabled = False
        Dim frmKit As WetcoConveyor.frmSetKit = New WetcoConveyor.frmSetKit
        frmKit.StartPosition = FormStartPosition.CenterScreen
        frmKit.ShowDialog()
        frmKit.Dispose()
        btnVacuumKit.Enabled = True
    End Sub

    Private Sub btnConveyorManual_Click(sender As Object, e As EventArgs) Handles btnConveyorManual.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnConveyorManual]" & vbTab & "Click")
        If btnConveyorManual.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnConveyorManual.Enabled = False
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.DCS_500AD
                Dim frmConveyorManual As WetcoConveyor.frmMain = New WetcoConveyor.frmMain
                frmConveyorManual.StartPosition = FormStartPosition.CenterScreen
                frmConveyorManual.ShowDialog()
                frmConveyorManual.Dispose()

            Case enmMachineType.DCS_F230A
                Dim frmConveyorManual As WetcoConveyor.frmMain = New WetcoConveyor.frmMain
                frmConveyorManual.StartPosition = FormStartPosition.CenterScreen
                frmConveyorManual.ShowDialog()
                frmConveyorManual.Dispose()

            Case enmMachineType.DCS_350A
                Dim frmConveyorManual As WetcoConveyor.frmMain = New WetcoConveyor.frmMain
                frmConveyorManual.StartPosition = FormStartPosition.CenterScreen
                frmConveyorManual.ShowDialog()
                frmConveyorManual.Dispose()

            Case enmMachineType.eDTS330A
                If FrmConveyor Is Nothing Then
                    FrmConveyor = New frmConveyor
                ElseIf FrmConveyor.IsDisposed Then
                    FrmConveyor = New frmConveyor
                End If
                FrmConveyor.StartPosition = FormStartPosition.CenterScreen
                FrmConveyor.ShowDialog()
                FrmConveyor.Dispose()

            Case enmMachineType.eDTS300A
                If frmConveyorPLC Is Nothing Then
                    frmConveyorPLC = New frmPLC
                ElseIf frmConveyorPLC.IsDisposed Then
                    frmConveyorPLC = New frmPLC
                End If

                frmConveyorPLC.Visible = True
                frmConveyorPLC.ShowDialog()
                frmConveyorPLC.Dispose()

        End Select
        btnConveyorManual.Enabled = True
    End Sub

    Private Sub btnElectricCylinder_Click(sender As Object, e As EventArgs) Handles btnElectricCylinder.Click
        'TODO: Asa確認該介面是否應在Recipe設定內?
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnElectricCylinder]" & vbTab & "Click")
        If btnElectricCylinder.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnElectricCylinder.Enabled = False
        Dim frmECylinder As WetcoConveyor.frmElectricCylinder = New WetcoConveyor.frmElectricCylinder
        frmECylinder.StartPosition = FormStartPosition.CenterScreen
        frmECylinder.ShowDialog()
        frmECylinder.Dispose()
        btnElectricCylinder.Enabled = True
    End Sub

    Private Sub btnSEtEPV_Click(sender As Object, e As EventArgs) Handles btnSetEPV.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnSetEPV]" & vbTab & "Click")
        If btnSetEPV.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnSetEPV.Enabled = False
        Dim frmEPV As New frmEPVConfig
        frmEPV.StartPosition = FormStartPosition.CenterScreen
        frmEPV.ShowDialog()
        frmEPV.Dispose()
        btnSetEPV.Enabled = True
    End Sub

    Private Sub btnTilt_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnSetTemp_Click(sender As Object, e As EventArgs) Handles btnSetTemp.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnSetTemp]" & vbTab & "Click")
        If btnSetTemp.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnSetTemp.Enabled = False
        Dim frmPidController As WetcoConveyor.frmPidController = New WetcoConveyor.frmPidController
        frmPidController.StartPosition = FormStartPosition.CenterScreen
        frmPidController.ShowDialog()
        frmPidController.Dispose()
        btnSetTemp.Enabled = True
    End Sub

    Private Sub btnStageSafe_Click(sender As Object, e As EventArgs) Handles btnStageSafe.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnStageSafe]" & vbTab & "Click")
        If btnStageSafe.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnStageSafe.Enabled = False
        Dim frmStageSafe As New frmSafePos
        frmStageSafe.StartPosition = FormStartPosition.CenterScreen
        frmStageSafe.ShowDialog()
        frmStageSafe.Dispose()
        btnStageSafe.Enabled = True
    End Sub

    Private Sub btnAsaTest_Click(sender As Object, e As EventArgs) Handles btnAsaTest.Click
        gSyslog.Save("[frmTest]" & vbTab & "[btnAsaTest]" & vbTab & "Click")
        If btnAsaTest.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnAsaTest.Enabled = False
        Dim frmTest As New WetcoConveyor.frmTest
        frmTest.StartPosition = FormStartPosition.CenterScreen
        frmTest.ShowDialog()
        frmTest.Dispose()
        btnAsaTest.Enabled = True
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        'Sue20170627
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnBack]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub frmEngineMode_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub frmEngineMode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'gAOICollection.LoadPathScene() '讀取AOI相關參數
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
       
    End Sub

End Class
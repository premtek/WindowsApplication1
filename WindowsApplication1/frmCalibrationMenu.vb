Imports ProjectCore
Imports ProjectRecipe
Imports ProjectAOI

Public Class frmCalibrationMenu

    ''' <summary>介面更新</summary>
    ''' <remarks></remarks>
    Public Sub RefreshUI()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                grpStage1.Visible = True
                grpStage2.Visible = True
                grpStage3.Visible = True
                grpStage4.Visible = True
                grpValve1.Visible = True
                grpValve2.Visible = True
                grpValve3.Visible = True
                grpValve4.Visible = True
                btnStageVerification1.Visible = True
                btnStageVerification2.Visible = True
                btnStageVerification3.Visible = True
                btnStageVerification4.Visible = True
                btnCCD1Image.Visible = False
                btnCCD2Image.Visible = False
                btnCCD3Image.Visible = False
                btnCCD4Image.Visible = False

                '20170120 [說明]:無此機構
                btnValveCleaner1.Visible = False
                btnValveCleaner2.Visible = False
                btnValveCleaner3.Visible = False
                btnValveCleaner4.Visible = False

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                grpStage1.Visible = True
                grpStage2.Visible = True
                grpStage3.Visible = False
                grpStage4.Visible = False
                grpValve1.Visible = True
                grpValve2.Visible = True
                grpValve3.Visible = False
                grpValve4.Visible = False
                btnStageVerification1.Visible = True
                btnStageVerification2.Visible = True
                btnStageVerification3.Visible = False
                btnStageVerification4.Visible = False
                btnCCD1Image.Visible = True
                btnCCD2Image.Visible = True
                btnCCD3Image.Visible = False
                btnCCD4Image.Visible = False

                '20170120
                btnValveCalibration3.Visible = False
                btnValveCalibration4.Visible = False
                btnValveCleaner3.Visible = False
                btnValveCleaner4.Visible = False

            Case enmMachineType.DCS_350A
                grpStage1.Visible = True
                grpStage2.Visible = False
                grpStage3.Visible = False
                grpStage4.Visible = False
                btnStageVerification1.Visible = True
                btnStageVerification2.Visible = False
                btnStageVerification3.Visible = False
                btnStageVerification4.Visible = False
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        grpValve1.Visible = True
                        grpValve2.Visible = False
                        grpValve3.Visible = False
                        grpValve4.Visible = False
                    Case eMechanismModule.TwoValveOneStage
                        grpValve1.Visible = True
                        grpValve2.Visible = True
                        grpValve3.Visible = False
                        grpValve4.Visible = False
                End Select
                btnCCD1Image.Visible = True
                btnCCD2Image.Visible = False
                btnCCD3Image.Visible = False
                btnCCD4Image.Visible = False
                btnValveCalibration2.Visible = False
                btnValveCalibration3.Visible = False
                btnValveCalibration4.Visible = False
                btnValveCleaner2.Visible = False
                btnValveCleaner3.Visible = False
                btnValveCleaner4.Visible = False
                btnValveWeight2.Visible = False
                btnValveWeight3.Visible = False
                btnValveWeight4.Visible = False
                btnPidController.Visible = True

            Case Else
                grpStage1.Visible = True
                grpStage2.Visible = False
                grpStage3.Visible = False
                grpStage4.Visible = False
                btnStageVerification1.Visible = True
                btnStageVerification2.Visible = False
                btnStageVerification3.Visible = False
                btnStageVerification4.Visible = False
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        grpValve1.Visible = True
                        grpValve2.Visible = False
                        grpValve3.Visible = False
                        grpValve4.Visible = False
                    Case eMechanismModule.TwoValveOneStage
                        grpValve1.Visible = True
                        grpValve2.Visible = True
                        grpValve3.Visible = False
                        grpValve4.Visible = False
                End Select
                btnCCD1Image.Visible = True
                btnCCD2Image.Visible = False
                btnCCD3Image.Visible = False
                btnCCD4Image.Visible = False

                '20170120
                btnValveCalibration2.Visible = False
                btnValveCalibration3.Visible = False
                btnValveCalibration4.Visible = False
                btnValveCleaner2.Visible = False
                btnValveCleaner3.Visible = False
                btnValveCleaner4.Visible = False
                btnValveWeight2.Visible = False
                btnValveWeight3.Visible = False
                btnValveWeight4.Visible = False

                '20161115   還有下面的ValveWeight
                btnPidController.Visible = False
        End Select

        '=== 鎖生產中不能用 ===
        If gSYS(eSys.DispStage1).RunStatus = enmRunStatus.Running Then
            btnCCD1.Enabled = False
            btnCalibrationCCD1Height.Enabled = False
            btnValve1HeightSensor.Enabled = False
            btnValvePurge1.Enabled = False
            btnValveBalance1.Enabled = False
            btnStageVerification1.Enabled = False
            btnCCD1Image.Enabled = False
            btnValveWeight1.Enabled = False
            '20170120
            btnValveCalibration1.Enabled = False
            btnValveCleaner1.Enabled = False
        Else
            btnCCD1.Enabled = True
            btnCalibrationCCD1Height.Enabled = True
            btnValve1HeightSensor.Enabled = True
            btnValvePurge1.Enabled = True
            btnValveBalance1.Enabled = True
            btnStageVerification1.Enabled = True
            btnCCD1Image.Enabled = True
            btnValveWeight1.Enabled = True
            '20170120
            btnValveCalibration1.Enabled = True
            btnValveCleaner1.Enabled = True
        End If

        If gSYS(eSys.DispStage2).RunStatus = enmRunStatus.Running Then
            btnCCD2.Enabled = False
            btnCalibrationCCD2Height.Enabled = False
            btnValve2HeightSensor.Enabled = False
            btnValvePurge2.Enabled = False
            btnValveBalance2.Enabled = False
            btnStageVerification2.Enabled = False
            btnCCD2Image.Enabled = False
            btnValveWeight2.Enabled = False
            '20170120
            btnValveCalibration2.Enabled = False
            btnValveCleaner2.Enabled = False
        Else
            btnCCD2.Enabled = True
            btnCalibrationCCD2Height.Enabled = True
            btnValve2HeightSensor.Enabled = True
            btnValvePurge2.Enabled = True
            btnValveBalance2.Enabled = True
            btnStageVerification2.Enabled = True
            btnCCD2Image.Enabled = True
            btnValveWeight2.Enabled = True
            '20170120
            btnValveCalibration2.Enabled = True
            btnValveCleaner2.Enabled = True
        End If

        If gSYS(eSys.DispStage3).RunStatus = enmRunStatus.Running Then
            btnCCD3.Enabled = False
            btnCalibrationCCD3Height.Enabled = False
            btnValve3HeightSensor.Enabled = False
            btnValvePurge3.Enabled = False
            btnValveBalance3.Enabled = False
            btnStageVerification3.Enabled = False
            btnCCD3Image.Enabled = False
            btnValveWeight3.Enabled = False
            '20170120
            btnValveCalibration3.Enabled = False
            btnValveCleaner3.Enabled = False
        Else
            btnCCD3.Enabled = True
            btnCalibrationCCD3Height.Enabled = True
            btnValve3HeightSensor.Enabled = True
            btnValvePurge3.Enabled = True
            btnValveBalance3.Enabled = True
            btnStageVerification3.Enabled = True
            btnCCD3Image.Enabled = True
            btnValveWeight3.Enabled = True
            '20170120
            btnValveCalibration3.Enabled = True
            btnValveCleaner3.Enabled = True
        End If

        If gSYS(eSys.DispStage4).RunStatus = enmRunStatus.Running Then
            btnCCD4.Enabled = False
            btnCalibrationCCD4Height.Enabled = False
            btnValve4HeightSensor.Enabled = False
            btnValvePurge4.Enabled = False
            btnValveBalance4.Enabled = False
            btnStageVerification4.Enabled = False
            btnCCD4Image.Enabled = False
            btnValveWeight4.Enabled = False
            '20170120
            btnValveCalibration4.Enabled = False
            btnValveCleaner4.Enabled = False
        Else
            btnCCD4.Enabled = True
            btnCalibrationCCD4Height.Enabled = True
            btnValve4HeightSensor.Enabled = True
            btnValvePurge4.Enabled = True
            btnValveBalance4.Enabled = True
            btnStageVerification4.Enabled = True
            btnCCD4Image.Enabled = True
            btnValveWeight4.Enabled = True
            '20170120
            btnValveCalibration4.Enabled = True
            btnValveCleaner4.Enabled = True
        End If
        '=== 鎖生產中不能用 ===
    End Sub
    Private Sub frmCalibrationMenu_Load(sender As Object, e As EventArgs) Handles Me.Load
        RefreshUI()
        '2016/11/22 因場景教導移置校正頁面
        'gAOICollection.LoadPathScene() '讀取AOI相關參數
    End Sub

#Region "CCD與閥校正"
    Private Sub btnCCD1_Click(sender As Object, e As EventArgs) Handles btnCCD1.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnCCD1]" & vbTab & "Click")
        btnCCD1.Enabled = False

        CType(sender, Button).FlatAppearance.BorderColor = Color.Lime
        'If gfrmCalibrationCCD2Valve1 Is Nothing Then
        '    gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        'ElseIf gfrmCalibrationCCD2Valve1.IsDisposed Then
        '    gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        'End If
        Dim mfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        With mfrmCalibrationCCD2Valve1
            .sys = gSYS(eSys.DispStage1)
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            ' .CalibrationEnable = False
            .ShowDialog()
        End With

        btnCCD1.Enabled = True

    End Sub

    Private Sub btnCCD2_Click(sender As Object, e As EventArgs) Handles btnCCD2.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnCCD2]" & vbTab & "Click")
        btnCCD2.Enabled = False
        CType(sender, Button).FlatAppearance.BorderColor = Color.Lime
        'If gfrmCalibrationCCD2Valve1 Is Nothing Then
        '    gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        'ElseIf gfrmCalibrationCCD2Valve1.IsDisposed Then
        '    gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        'End If
        Dim mfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        With mfrmCalibrationCCD2Valve1
            .sys = gSYS(eSys.DispStage2)
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            ' .CalibrationEnable = False
            .ShowDialog()
        End With
        btnCCD2.Enabled = True

    End Sub

    Private Sub btnCCD3_Click(sender As Object, e As EventArgs) Handles btnCCD3.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnCCD3]" & vbTab & "Click")
        btnCCD3.Enabled = False
        CType(sender, Button).FlatAppearance.BorderColor = Color.Lime
        'If gfrmCalibrationCCD2Valve1 Is Nothing Then
        '    gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        'ElseIf gfrmCalibrationCCD2Valve1.IsDisposed Then
        '    gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        'End If
        Dim mfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        With mfrmCalibrationCCD2Valve1
            .sys = gSYS(eSys.DispStage3)
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            '.CalibrationEnable = False
            .ShowDialog()
        End With
        btnCCD3.Enabled = True

    End Sub

    Private Sub btnCCD4_Click(sender As Object, e As EventArgs) Handles btnCCD4.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnCCD4]" & vbTab & "Click")
        btnCCD4.Enabled = False
        CType(sender, Button).FlatAppearance.BorderColor = Color.Lime
        'If gfrmCalibrationCCD2Valve1 Is Nothing Then
        '    gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        'ElseIf gfrmCalibrationCCD2Valve1.IsDisposed Then
        '    gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        'End If
        Dim mfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        With mfrmCalibrationCCD2Valve1
            .sys = gSYS(eSys.DispStage4)
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            ' .CalibrationEnable = False
            .ShowDialog()
        End With
        btnCCD4.Enabled = True

    End Sub
#End Region

    Private Sub btnPidController_Click(sender As Object, e As EventArgs) Handles btnPidController.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnPidController]" & vbTab & "Click")
        btnPidController.Enabled = False
        Dim frmPidCtrl As WetcoConveyor.frmCalibrationPidCtrl = New WetcoConveyor.frmCalibrationPidCtrl
        frmPidCtrl.ShowDialog()
        frmPidCtrl = Nothing
        btnPidController.Enabled = True
    End Sub

#Region "CCD與測高感測器校正"
    Private Sub btnCalibrationCCD1Height_Click(sender As Object, e As EventArgs) Handles btnCalibrationCCD1Height.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnCCDHeightSensor1]" & vbTab & "Click")
        btnCalibrationCCD1Height.Enabled = False
        If gfrmCalibrationCCD2Laser Is Nothing Then
            gfrmCalibrationCCD2Laser = New frmCalibrationCCD2Height
        ElseIf gfrmCalibrationCCD2Laser.IsDisposed Then
            gfrmCalibrationCCD2Laser = New frmCalibrationCCD2Height
        End If
        ' Dim mfrmCalibrationCCD1Laser = New frmCalibrationCCD2Height
        With gfrmCalibrationCCD2Laser
            .sys = gSYS(eSys.DispStage1)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            '.CalibrationEnable = False
            .ShowDialog()
        End With
        btnCalibrationCCD1Height.Enabled = True
    End Sub

    Private Sub btnCalibrationCCD2Height_Click(sender As Object, e As EventArgs) Handles btnCalibrationCCD2Height.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnCalibrationCCD2Height]" & vbTab & "Click")
        btnCalibrationCCD2Height.Enabled = False
        If gfrmCalibrationCCD2Laser Is Nothing Then
            gfrmCalibrationCCD2Laser = New frmCalibrationCCD2Height
        ElseIf gfrmCalibrationCCD2Laser.IsDisposed Then
            gfrmCalibrationCCD2Laser = New frmCalibrationCCD2Height
        End If
        '  Dim mfrmCalibrationCCD2Laser = New frmCalibrationCCD2Height
        With gfrmCalibrationCCD2Laser
            .sys = gSYS(eSys.DispStage2)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            '.CalibrationEnable = False
            .ShowDialog()
        End With
        btnCalibrationCCD2Height.Enabled = True
        'Me.Hide()
    End Sub

    Private Sub btnCalibrationCCD3Height_Click(sender As Object, e As EventArgs) Handles btnCalibrationCCD3Height.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnCalibrationCCD3Height]" & vbTab & "Click")
        btnCalibrationCCD3Height.Enabled = False
        CType(sender, Button).FlatAppearance.BorderColor = Color.Lime
        If gfrmCalibrationCCD3Laser Is Nothing Then
            gfrmCalibrationCCD3Laser = New frmCalibrationCCD2Height
        ElseIf gfrmCalibrationCCD3Laser.IsDisposed Then
            gfrmCalibrationCCD3Laser = New frmCalibrationCCD2Height
        End If
        '  Dim mfrmCalibrationCCD3Laser = New frmCalibrationCCD2Height
        With gfrmCalibrationCCD3Laser
            .sys = gSYS(eSys.DispStage3)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            '.CalibrationEnable = False
            .ShowDialog()
        End With
        btnCalibrationCCD3Height.Enabled = True
        'Me.Hide()
    End Sub

    Private Sub btnCalibrationCCD4Height_Click(sender As Object, e As EventArgs) Handles btnCalibrationCCD4Height.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnCalibrationCCD4Height]" & vbTab & "Click")
        btnCalibrationCCD4Height.Enabled = False
        If gfrmCalibrationCCD4Laser Is Nothing Then
            gfrmCalibrationCCD4Laser = New frmCalibrationCCD2Height
        ElseIf gfrmCalibrationCCD4Laser.IsDisposed Then
            gfrmCalibrationCCD4Laser = New frmCalibrationCCD2Height
        End If
        '    Dim mfrmCalibrationCCD4Laser = New frmCalibrationCCD2Height
        With gfrmCalibrationCCD4Laser
            .sys = gSYS(eSys.DispStage4)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            '.CalibrationEnable = False
            .ShowDialog()
        End With
        btnCalibrationCCD4Height.Enabled = True
        'Me.Hide()
    End Sub
#End Region

#Region "閥與測高感測器校正"

    Private Sub btnValve1HeightSensor_Click(sender As Object, e As EventArgs) Handles btnValve1HeightSensor.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValve1HeightSensor]" & vbTab & "Click")
        btnValve1HeightSensor.Enabled = False
        CType(sender, Button).FlatAppearance.BorderColor = Color.Lime
        If gfrmCalibrationZHeight Is Nothing Then
            gfrmCalibrationZHeight = New frmCalibrationZHeight
        ElseIf gfrmCalibrationZHeight.IsDisposed Then
            gfrmCalibrationZHeight = New frmCalibrationZHeight
        End If
        '   Dim mfrmCalibrationZHeight = New frmCalibrationZHeight
        With gfrmCalibrationZHeight
            .sys = gSYS(eSys.DispStage1)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = False
            .ShowDialog()
        End With
        btnValve1HeightSensor.Enabled = True
        'Me.Hide()
    End Sub

    Private Sub btnValve2HeightSensor_Click(sender As Object, e As EventArgs) Handles btnValve2HeightSensor.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValve2HeightSensor]" & vbTab & "Click")
        btnValve2HeightSensor.Enabled = False
        CType(sender, Button).FlatAppearance.BorderColor = Color.Lime
        If gfrmCalibrationZHeight Is Nothing Then
            gfrmCalibrationZHeight = New frmCalibrationZHeight
        ElseIf gfrmCalibrationZHeight.IsDisposed Then
            gfrmCalibrationZHeight = New frmCalibrationZHeight
        End If
        '   Dim mfrmCalibrationZHeight = New frmCalibrationZHeight
        With gfrmCalibrationZHeight
            .sys = gSYS(eSys.DispStage2)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = False
            .ShowDialog()
        End With
        btnValve2HeightSensor.Enabled = True
        ' Me.Hide()
    End Sub

    Private Sub btnValve3HeightSensor_Click(sender As Object, e As EventArgs) Handles btnValve3HeightSensor.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValve3HeightSensor]" & vbTab & "Click")
        btnValve3HeightSensor.Enabled = False
        CType(sender, Button).FlatAppearance.BorderColor = Color.Lime
        If gfrmCalibrationZHeight Is Nothing Then
            gfrmCalibrationZHeight = New frmCalibrationZHeight
        ElseIf gfrmCalibrationZHeight.IsDisposed Then
            gfrmCalibrationZHeight = New frmCalibrationZHeight
        End If
        '  Dim mfrmCalibrationZHeight = New frmCalibrationZHeight
        With gfrmCalibrationZHeight
            .sys = gSYS(eSys.DispStage3)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = False
            .ShowDialog()
        End With
        btnValve3HeightSensor.Enabled = True
        'Me.Hide()
    End Sub

    Private Sub btnValve4HeightSensor_Click(sender As Object, e As EventArgs) Handles btnValve4HeightSensor.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValve4HeightSensor]" & vbTab & "Click")
        btnValve4HeightSensor.Enabled = False
        CType(sender, Button).FlatAppearance.BorderColor = Color.Lime
        If gfrmCalibrationZHeight Is Nothing Then
            gfrmCalibrationZHeight = New frmCalibrationZHeight
        ElseIf gfrmCalibrationZHeight.IsDisposed Then
            gfrmCalibrationZHeight = New frmCalibrationZHeight
        End If
        '  Dim mfrmCalibrationZHeight = New frmCalibrationZHeight
        With gfrmCalibrationZHeight

            .sys = gSYS(eSys.DispStage4)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = False
            .ShowDialog()
        End With
        btnValve4HeightSensor.Enabled = True
        ' Me.Hide()
    End Sub

#End Region

    Private Sub btnValvePurge1_Click(sender As Object, e As EventArgs) Handles btnValvePurge1.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValvePurge1]" & vbTab & "Click")
        btnValvePurge1.Enabled = False
        If gfrmPurge Is Nothing Then
            gfrmPurge = New frmCalibrationPurge
        ElseIf gfrmPurge.IsDisposed Then
            gfrmPurge = New frmCalibrationPurge
        End If
        '   Dim mfrmPurge = New frmCalibrationPurge
        With gfrmPurge
            .sys = gSYS(eSys.DispStage1)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = False
            .ShowDialog()

        End With
        gfrmPurge = Nothing
        btnValvePurge1.Enabled = True
    End Sub

    Private Sub btnValvePurge2_Click(sender As Object, e As EventArgs) Handles btnValvePurge2.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValvePurge2]" & vbTab & "Click")
        btnValvePurge2.Enabled = False
        If gfrmPurge Is Nothing Then
            gfrmPurge = New frmCalibrationPurge
        ElseIf gfrmPurge.IsDisposed Then
            gfrmPurge = New frmCalibrationPurge
        End If
        '  Dim mfrmPurge = New frmCalibrationPurge
        With gfrmPurge
            .sys = gSYS(eSys.DispStage2)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = False
            .ShowDialog()
        End With
        gfrmPurge = Nothing
        btnValvePurge2.Enabled = True
    End Sub

    Private Sub btnValvePurge3_Click(sender As Object, e As EventArgs) Handles btnValvePurge3.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValvePurge3]" & vbTab & "Click")
        btnValvePurge3.Enabled = False
        If gfrmPurge Is Nothing Then
            gfrmPurge = New frmCalibrationPurge
        ElseIf gfrmPurge.IsDisposed Then
            gfrmPurge = New frmCalibrationPurge
        End If
        '  Dim mfrmPurge = New frmCalibrationPurge
        With gfrmPurge
            .sys = gSYS(eSys.DispStage3)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = False
            .ShowDialog()
        End With
        gfrmPurge = Nothing
        btnValvePurge3.Enabled = True
    End Sub

    Private Sub btnValvePurge4_Click(sender As Object, e As EventArgs) Handles btnValvePurge4.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValvePurge4]" & vbTab & "Click")
        btnValvePurge4.Enabled = False
        If gfrmPurge Is Nothing Then
            gfrmPurge = New frmCalibrationPurge
        ElseIf gfrmPurge.IsDisposed Then
            gfrmPurge = New frmCalibrationPurge
        End If

        '    Dim mfrmPurge = New frmCalibrationPurge
        With gfrmPurge
            .sys = gSYS(eSys.DispStage4)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = False
            .ShowDialog()
        End With
        gfrmPurge = Nothing
        btnValvePurge4.Enabled = True
    End Sub

    Private Sub btnValveBalance1_Click(sender As Object, e As EventArgs) Handles btnValveBalance1.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValveBalance1]" & vbTab & "Click")
        If gfrmWeightPosition Is Nothing Then
            gfrmWeightPosition = New frmCalibrationFlowRate
        ElseIf gfrmWeightPosition.IsDisposed Then
            gfrmWeightPosition = New frmCalibrationFlowRate
        End If

        With gfrmWeightPosition
            .sys = gSYS(eSys.DispStage1)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = False
            .ShowDialog()
        End With

    End Sub

    Private Sub btnValveBalance2_Click(sender As Object, e As EventArgs) Handles btnValveBalance2.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValveBalance2]" & vbTab & "Click")
        If gfrmWeightPosition Is Nothing Then
            gfrmWeightPosition = New frmCalibrationFlowRate
        ElseIf gfrmWeightPosition.IsDisposed Then
            gfrmWeightPosition = New frmCalibrationFlowRate
        End If
        'Dim mfrmWeightPosition = New frmCalibrationFlowRate
        With gfrmWeightPosition
            .sys = gSYS(eSys.DispStage2)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = False
            .ShowDialog()
        End With

    End Sub

    Private Sub btnValveBalance3_Click(sender As Object, e As EventArgs) Handles btnValveBalance3.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValveBalance3]" & vbTab & "Click")
        If gfrmWeightPosition Is Nothing Then
            gfrmWeightPosition = New frmCalibrationFlowRate
        ElseIf gfrmWeightPosition.IsDisposed Then
            gfrmWeightPosition = New frmCalibrationFlowRate
        End If
        '   Dim mfrmWeightPosition = New frmCalibrationFlowRate
        With gfrmWeightPosition
            .sys = gSYS(eSys.DispStage3)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = False
            .ShowDialog()
        End With

    End Sub

    Private Sub btnValveBalance4_Click(sender As Object, e As EventArgs) Handles btnValveBalance4.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValveBalance4]" & vbTab & "Click")
        If gfrmWeightPosition Is Nothing Then
            gfrmWeightPosition = New frmCalibrationFlowRate
        ElseIf gfrmWeightPosition.IsDisposed Then
            gfrmWeightPosition = New frmCalibrationFlowRate
        End If
        ' Dim mfrmWeightPosition = New frmCalibrationFlowRate
        With gfrmWeightPosition
            .sys = gSYS(eSys.DispStage4)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .CalibrationEnable = False
            .ShowDialog()
        End With
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnBack]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub btnStageVerification_Click(sender As Object, e As EventArgs) Handles btnStageVerification1.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnStageVerification1]" & vbTab & "Click")
        If btnStageVerification1.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnStageVerification1.Enabled = False
        'If gfrmMeasureStageVerification Is Nothing Then
        '    gfrmMeasureStageVerification = New frmStageVerification
        'ElseIf gfrmMeasureStageVerification.IsDisposed Then
        '    gfrmMeasureStageVerification = New frmStageVerification
        'End If
        Dim mfrmMeasureStageVerification = New frmStageVerification
        With mfrmMeasureStageVerification
            .sys = gSYS(eSys.DispStage1)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .Show()
            .BringToFront()
        End With
        btnStageVerification1.Enabled = True
    End Sub

    Private Sub btnStageVerification2_Click(sender As Object, e As EventArgs) Handles btnStageVerification2.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnStageVerification2]" & vbTab & "Click")
        If btnStageVerification1.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnStageVerification2.Enabled = False

        'If gfrmMeasureStageVerification Is Nothing Then
        '    gfrmMeasureStageVerification = New frmStageVerification
        'ElseIf gfrmMeasureStageVerification.IsDisposed Then
        '    gfrmMeasureStageVerification = New frmStageVerification
        'End If
        Dim mfrmMeasureStageVerification = New frmStageVerification
        With mfrmMeasureStageVerification
            .sys = gSYS(eSys.DispStage2)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .Show()
            .BringToFront()
        End With
        btnStageVerification2.Enabled = True
    End Sub

    Private Sub btnStageVerification3_Click(sender As Object, e As EventArgs) Handles btnStageVerification3.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnStageVerification3]" & vbTab & "Click")
        If btnStageVerification3.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnStageVerification3.Enabled = False

        'If gfrmMeasureStageVerification Is Nothing Then
        '    gfrmMeasureStageVerification = New frmStageVerification
        'ElseIf gfrmMeasureStageVerification.IsDisposed Then
        '    gfrmMeasureStageVerification = New frmStageVerification
        'End If
        Dim mfrmMeasureStageVerification = New frmStageVerification
        With mfrmMeasureStageVerification
            .sys = gSYS(eSys.DispStage3)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .Show()
            .BringToFront()
        End With
        btnStageVerification3.Enabled = True
    End Sub

    Private Sub btnStageVerification4_Click(sender As Object, e As EventArgs) Handles btnStageVerification4.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnStageVerification4]" & vbTab & "Click")
        If btnStageVerification4.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnStageVerification4.Enabled = False

        'If gfrmMeasureStageVerification Is Nothing Then
        '    gfrmMeasureStageVerification = New frmStageVerification
        'ElseIf gfrmMeasureStageVerification.IsDisposed Then
        '    gfrmMeasureStageVerification = New frmStageVerification
        'End If
        Dim mfrmMeasureStageVerification = New frmStageVerification
        With mfrmMeasureStageVerification
            .sys = gSYS(eSys.DispStage4)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .Show()
            .BringToFront()
        End With
        btnStageVerification4.Enabled = True
    End Sub
    '20160920
    Private Sub btnValveWeight1_Click(sender As Object, e As EventArgs) Handles btnValveWeight1.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnStageVerification4]" & vbTab & "Click")
        '[說明]:判斷有無開啟Recipe
        If gCRecipe.strName = "" Then
            'Recipe讀檔失敗
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002020))
            MsgBox(gMsgHandler.GetMessage(Error_1002020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gfrmWeightValve1 Is Nothing Then
            gfrmWeightValve1 = New frmWeight
        ElseIf gfrmWeightValve1.IsDisposed Then
            gfrmWeightValve1 = New frmWeight
        End If
        'gfrmWeightValve1 = New frmWeight

        '   Dim mfrmWeightValve1 As frmWeight

        '   mfrmWeightValve1 = New frmWeight

        With gfrmWeightValve1
            .StartPosition = FormStartPosition.CenterParent
            '20161208
            .sys = gSYS(eSys.DispStage1)
            .Text = "Valve1 Weight"
            .Show()
            .BringToFront()
        End With
        'gfrmWeightValve1.btnGoTilt.PerformClick()

        gfrmWeightValve1.MoveTiltToSysparam()

    End Sub

    Private Sub btnValveWeight2_Click(sender As Object, e As EventArgs) Handles btnValveWeight2.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValveWeight2]" & vbTab & "Click")
        '[說明]:判斷有無開啟Recipe
        If gCRecipe.strName = "" Then
            'Recipe讀檔失敗
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002020))
            MsgBox(gMsgHandler.GetMessage(Error_1002020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gfrmWeightValve2 Is Nothing Then
            gfrmWeightValve2 = New frmWeight
        ElseIf gfrmWeightValve2.IsDisposed Then
            gfrmWeightValve2 = New frmWeight
        End If
        '  gfrmWeightValve2 = New frmWeight
        With gfrmWeightValve2
            .StartPosition = FormStartPosition.CenterParent
            .sys = gSYS(eSys.DispStage2)
            .Text = "Valve2 Weight"
            .Show()
            .BringToFront()
        End With
    End Sub

    Private Sub btnValveWeight3_Click(sender As Object, e As EventArgs) Handles btnValveWeight3.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValveWeight3]" & vbTab & "Click")
        '[說明]:判斷有無開啟Recipe
        If gCRecipe.strName = "" Then
            'Recipe讀檔失敗
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002020))
            MsgBox(gMsgHandler.GetMessage(Error_1002020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gfrmWeightValve3 Is Nothing Then
            gfrmWeightValve3 = New frmWeight
        ElseIf gfrmWeightValve3.IsDisposed Then
            gfrmWeightValve3 = New frmWeight
        End If
        ' gfrmWeightValve3 = New frmWeight
        With gfrmWeightValve3
            .StartPosition = FormStartPosition.CenterParent
            '20161102
            .sys = gSYS(eSys.DispStage3)
            .Text = "Valve3 Weight"
            .Show()
            .BringToFront()
        End With
    End Sub

    Private Sub btnValveWeight4_Click(sender As Object, e As EventArgs) Handles btnValveWeight4.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnValveWeight4]" & vbTab & "Click")
        '[說明]:判斷有無開啟Recipe
        If gCRecipe.strName = "" Then
            'Recipe讀檔失敗
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002020))
            MsgBox(gMsgHandler.GetMessage(Error_1002020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        If gfrmWeightValve4 Is Nothing Then
            gfrmWeightValve4 = New frmWeight
        ElseIf gfrmWeightValve4.IsDisposed Then
            gfrmWeightValve4 = New frmWeight
        End If
        '  gfrmWeightValve4 = New frmWeight
        With gfrmWeightValve4
            .StartPosition = FormStartPosition.CenterParent
            '20161102
            .sys = gSYS(eSys.DispStage4)
            .Text = "Valve4 Weight"
            .Show()
            .BringToFront()
        End With
    End Sub

    Private Sub btnCCD1Image_Click(sender As Object, e As EventArgs) Handles btnCCD1Image.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnCCD1Image]" & vbTab & "Click")
        If btnCCD1Image.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnCCD1Image.Enabled = False

        'If gfrmCalibrationCCD1Image Is Nothing Then
        '    gfrmCalibrationCCD1Image = New frmCalibrationCCDImage
        'ElseIf gfrmCalibrationCCD1Image.IsDisposed Then
        '    gfrmCalibrationCCD1Image = New frmCalibrationCCDImage
        'End If
        Dim mfrmCalibrationCCD1Image = New frmCalibrationCCDImage 'frmAlignScene '
        With mfrmCalibrationCCD1Image
            .sys = gSYS(eSys.DispStage1)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .ShowDialog()
        End With
        btnCCD1Image.Enabled = True

        'gSyslog.Save("[frmEngineMode]" & vbTab & "[btnCCD1Image]" & vbTab & "Click")
        'If btnCCD1Image.Enabled = False Then
        '    Exit Sub
        '    '^^^^^^^
        'End If
        'btnCCD1Image.Enabled = False

        'If gAOICollection.LoadPathSceneStatus = CAOICollection.enmStatus.Loading Then
        '    MsgBox("Scene is Loading, Please Wait.")
        '    btnCCD1Image.Enabled = True
        '    Exit Sub
        'End If
        'If gAOICollection.LoadPathSceneStatus = CAOICollection.enmStatus.NG Then
        '    MsgBox("Scene Load Failed.")
        '    btnCCD1Image.Enabled = True
        'End If
        'gfrmAlignScene = New frmAlignScene
        'With gfrmAlignScene
        '    .Sys = gSYS(eSys.DispStage1)
        '    .StartPosition = FormStartPosition.Manual
        '    .Location = New Point(0, 0)
        '    .ShowDialog()
        'End With
        'btnCCD1Image.Enabled = True

    End Sub

    Private Sub btnCCD2Image_Click(sender As Object, e As EventArgs) Handles btnCCD2Image.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnCCD2Image]" & vbTab & "Click")
        If btnCCD2Image.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnCCD2Image.Enabled = False
        'If gfrmCalibrationCCD2Image Is Nothing Then
        '    gfrmCalibrationCCD2Image = New frmCalibrationCCDImage
        'ElseIf gfrmCalibrationCCD2Image.IsDisposed Then
        '    gfrmCalibrationCCD2Image = New frmCalibrationCCDImage
        'End If
        Dim mfrmCalibrationCCD2Image = New frmCalibrationCCDImage
        With mfrmCalibrationCCD2Image
            .sys = gSYS(eSys.DispStage2)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .ShowDialog()
        End With
        btnCCD2Image.Enabled = True

        'gSyslog.Save("[frmEngineMode]" & vbTab & "[btnCCD2Image]" & vbTab & "Click")
        'If btnCCD2Image.Enabled = False Then
        '    Exit Sub
        '    ^^^^^^^
        'End If
        'btnCCD2Image.Enabled = False

        'If gAOICollection.LoadPathSceneStatus = CAOICollection.enmStatus.Loading Then
        '    MsgBox("Scene is Loading, Please Wait.")
        '    btnCCD2Image.Enabled = True
        '    Exit Sub
        'End If
        'If gAOICollection.LoadPathSceneStatus = CAOICollection.enmStatus.NG Then
        '    MsgBox("Scene Load Failed.")
        '    btnCCD2Image.Enabled = True
        'End If
        'gfrmAlignScene = New frmAlignScene
        'With gfrmAlignScene
        '    .Sys = gSYS(eSys.DispStage2)
        '    .StartPosition = FormStartPosition.Manual
        '    .Location = New Point(0, 0)
        '    .ShowDialog()
        'End With
        'btnCCD2Image.Enabled = True
    End Sub

    Private Sub btnCCD3Image_Click(sender As Object, e As EventArgs) Handles btnCCD3Image.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnCCD3Image]" & vbTab & "Click")
        If btnCCD3Image.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnCCD3Image.Enabled = False
        'If gfrmCalibrationCCD3Image Is Nothing Then
        '    gfrmCalibrationCCD3Image = New frmCalibrationCCDImage
        'ElseIf gfrmCalibrationCCD3Image.IsDisposed Then
        '    gfrmCalibrationCCD3Image = New frmCalibrationCCDImage
        'End If
        Dim mfrmCalibrationCCD3Image = New frmCalibrationCCDImage
        With mfrmCalibrationCCD3Image
            .sys = gSYS(eSys.DispStage3)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .ShowDialog()
        End With
        btnCCD3Image.Enabled = True

        'gSyslog.Save("[frmEngineMode]" & vbTab & "[btnCCD3Image]" & vbTab & "Click")
        'If btnCCD3Image.Enabled = False Then
        '    Exit Sub
        '    ^^^^^^^
        'End If
        'btnCCD3Image.Enabled = False

        'If gAOICollection.LoadPathSceneStatus = CAOICollection.enmStatus.Loading Then
        '    MsgBox("Scene is Loading, Please Wait.")
        '    btnCCD3Image.Enabled = True
        '    Exit Sub
        'End If
        'If gAOICollection.LoadPathSceneStatus = CAOICollection.enmStatus.NG Then
        '    MsgBox("Scene Load Failed.")
        '    btnCCD3Image.Enabled = True
        'End If
        'gfrmAlignScene = New frmAlignScene
        'With gfrmAlignScene
        '    .Sys = gSYS(eSys.DispStage3)
        '    .StartPosition = FormStartPosition.Manual
        '    .Location = New Point(0, 0)
        '    .ShowDialog()
        'End With
        'btnCCD3Image.Enabled = True
    End Sub

    Private Sub btnCCD4Image_Click(sender As Object, e As EventArgs) Handles btnCCD4Image.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnCCD4Image]" & vbTab & "Click")
        If btnCCD4Image.Enabled = False Then
            Exit Sub
            '^^^^^^^
        End If
        btnCCD4Image.Enabled = False
        'If gfrmCalibrationCCD4Image Is Nothing Then
        '    gfrmCalibrationCCD4Image = New frmCalibrationCCDImage
        'ElseIf gfrmCalibrationCCD4Image.IsDisposed Then
        '    gfrmCalibrationCCD4Image = New frmCalibrationCCDImage
        'End If
        Dim mfrmCalibrationCCD4Image = New frmCalibrationCCDImage
        With mfrmCalibrationCCD4Image
            .sys = gSYS(eSys.DispStage4)
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .ShowDialog()
        End With
        btnCCD4Image.Enabled = True

        'gSyslog.Save("[frmEngineMode]" & vbTab & "[btnCCD4Image]" & vbTab & "Click")
        'If btnCCD4Image.Enabled = False Then
        '    Exit Sub
        '    '^^^^^^^
        'End If
        'btnCCD4Image.Enabled = False

        'If gAOICollection.LoadPathSceneStatus = CAOICollection.enmStatus.Loading Then
        '    MsgBox("Scene is Loading, Please Wait.")
        '    btnCCD4Image.Enabled = True
        '    Exit Sub
        'End If
        'If gAOICollection.LoadPathSceneStatus = CAOICollection.enmStatus.NG Then
        '    MsgBox("Scene Load Failed.")
        '    btnCCD4Image.Enabled = True
        'End If
        'gfrmAlignScene = New frmAlignScene
        'With gfrmAlignScene
        '    .Sys = gSYS(eSys.DispStage4)
        '    .StartPosition = FormStartPosition.Manual
        '    .Location = New Point(0, 0)
        '    .ShowDialog()
        'End With
        'btnCCD4Image.Enabled = True
    End Sub

    Private Sub btnValveCleaner1_Click(sender As Object, e As EventArgs) Handles btnValveCleaner1.Click
        gSyslog.Save("[frmEngineMode]" & vbTab & "[btnCalibrationClearGlue]" & vbTab & "Click")
        If gfrmClearGlue Is Nothing Then
            gfrmClearGlue = New frmCalibrationClearGlue
        ElseIf gfrmClearGlue.IsDisposed Then
            gfrmClearGlue = New frmCalibrationClearGlue
        End If

        With gfrmClearGlue
            .StartPosition = FormStartPosition.CenterScreen
            .CalibrationEnable = False
            gfrmClearGlue.ShowDialog()
        End With
    End Sub

    Private Sub btnValveCleaner2_Click(sender As Object, e As EventArgs) Handles btnValveCleaner2.Click

    End Sub

    Private Sub btnValveCleaner3_Click(sender As Object, e As EventArgs) Handles btnValveCleaner3.Click

    End Sub

    Private Sub btnValveCleaner4_Click(sender As Object, e As EventArgs) Handles btnValveCleaner4.Click

    End Sub

    Private Sub btnValveCalibration1_Click(sender As Object, e As EventArgs) Handles btnValveCalibration1.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnCCD1]" & vbTab & "Click")

        If gfrmCalibrationCCD2Valve1 Is Nothing Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        ElseIf gfrmCalibrationCCD2Valve1.IsDisposed Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        End If
        '      Dim mfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        With gfrmCalibrationCCD2Valve1
            .sys = gSYS(eSys.DispStage1)
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            '.CalibrationEnable = True
            .ShowDialog()
        End With
    End Sub

    Private Sub btnValveCalibration2_Click(sender As Object, e As EventArgs) Handles btnValveCalibration2.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnCCD2]" & vbTab & "Click")
        If gfrmCalibrationCCD2Valve1 Is Nothing Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        ElseIf gfrmCalibrationCCD2Valve1.IsDisposed Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        End If
        '      Dim mfrmCalibrationCCD2Valve2 = New frmCalibrationCCD2Valve1
        With gfrmCalibrationCCD2Valve1
            .sys = gSYS(eSys.DispStage2)
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            ' .CalibrationEnable = True
            .ShowDialog()
        End With
    End Sub

    Private Sub btnValveCalibration3_Click(sender As Object, e As EventArgs) Handles btnValveCalibration3.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnCCD3]" & vbTab & "Click")

        CType(sender, Button).FlatAppearance.BorderColor = Color.Lime
        If gfrmCalibrationCCD2Valve1 Is Nothing Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        ElseIf gfrmCalibrationCCD2Valve1.IsDisposed Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        End If
        '      Dim mfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        With gfrmCalibrationCCD2Valve1
            .sys = gSYS(eSys.DispStage3)
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            ' .CalibrationEnable = True
            .ShowDialog()
        End With
    End Sub

    Private Sub btnValveCalibration4_Click(sender As Object, e As EventArgs) Handles btnValveCalibration4.Click
        gSyslog.Save("[frmCalibrationMenu]" & vbTab & "[btnCCD4]" & vbTab & "Click")

        CType(sender, Button).FlatAppearance.BorderColor = Color.Lime
        If gfrmCalibrationCCD2Valve1 Is Nothing Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        ElseIf gfrmCalibrationCCD2Valve1.IsDisposed Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        End If
        '      Dim mfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        With gfrmCalibrationCCD2Valve1
            .sys = gSYS(eSys.DispStage4)
            .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            ' .CalibrationEnable = True
            .ShowDialog()
        End With
    End Sub
End Class
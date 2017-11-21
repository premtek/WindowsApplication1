Imports ProjectCore
Imports ProjectRecipe
Imports ProjectAOI
Imports ProjectMotion

Public Class frmCalibrationMenuNew

    Private Sub frmCalibrationMenuNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cmbStageNum.Items.Clear()
        cmbValveNum.Items.Clear()

        Dim StageNo As Double = 1
        Dim ValveNo As Double = 2

        '[說明]:寫入cmbValve1對應JetValveDB資料      '20161111
        For i As Integer = 0 To gSSystemParameter.StageCount - 1
            cmbStageNum.Items.Add("Stage " & i + 1)
        Next

        For j As Integer = 0 To gSSystemParameter.StageUseValveCount - 1
            cmbValveNum.Items.Add("Valve " & j + 1)
        Next
        cmbStageNum.SelectedIndex = 0
        cmbValveNum.SelectedIndex = 0

        RefreshUI()


    End Sub
    Public Sub RefreshUI()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                btnValveCalibration.Visible = True
                btnValveWeight.Visible = True
                btnStageVerification.Visible = True
                btnContactIni.Visible = False
                'btnCCDImage.Visible = False
                btnPidController.Visible = True
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                btnValveCalibration.Visible = True
                btnValveWeight.Visible = True
                btnStageVerification.Visible = True
                btnContactIni.Visible = False
                'btnCCDImage.Visible = False
                btnPidController.Visible = False
            Case enmMachineType.DCS_350A
                btnValveCalibration.Visible = True
                btnValveWeight.Visible = True
                btnStageVerification.Visible = True

                '接觸式Calibration 保護
                Select Case gSSystemParameter.MeasureType
                    Case enmMeasureType.Laser
                        btnContactIni.Visible = False
                    Case Else
                        btnContactIni.Visible = True
                End Select

                'btnCCDImage.Visible = False
                btnPidController.Visible = True
            Case Else
                btnValveCalibration.Visible = True
                btnValveWeight.Visible = True
                btnStageVerification.Visible = True
                btnContactIni.Visible = True
                'btnCCDImage.Visible = True
                btnPidController.Visible = False
        End Select

        '=== 鎖生產中不能用 ===
        If gSYS(eSys.OverAll).RunStatus = enmRunStatus.Running Then
            btnValveCalibration.Enabled = False
            btnValveWeight.Enabled = False
            btnStageVerification.Enabled = False
            btnContactIni.Enabled = False
            btnCCDImage.Enabled = False
            btnPidController.Enabled = False
            btnStageVerification_New.Enabled = False
        Else
            btnValveCalibration.Enabled = True
            btnValveWeight.Enabled = True
            btnStageVerification.Enabled = True
            btnContactIni.Enabled = True
            btnCCDImage.Enabled = True
            btnPidController.Enabled = True
            btnStageVerification_New.Enabled = True
        End If
    End Sub
    Private Sub btnValveCalibration_Click(sender As Object, e As EventArgs) Handles btnValveCalibration.Click
        gSyslog.Save("[frmCalibrationMenuNew]" & vbTab & "[btnCCD]" & vbTab & "Click")

        If gfrmCalibrationCCD2Valve1 Is Nothing Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        ElseIf gfrmCalibrationCCD2Valve1.IsDisposed Then
            gfrmCalibrationCCD2Valve1 = New frmCalibrationCCD2Valve1
        End If




        With gfrmCalibrationCCD2Valve1
            '[說明]:選擇Stage and Valve
            Select Case cmbStageNum.SelectedIndex
                Case enmStage.No1
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve2
                    End Select
                    .sys = gSYS(eSys.DispStage1)
                Case enmStage.No2

                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve2
                    End Select
                    .sys = gSYS(eSys.DispStage2)
                Case enmStage.No3

                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve2
                    End Select
                    .sys = gSYS(eSys.DispStage3)
                Case enmStage.No4

                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve2
                    End Select
                    .sys = gSYS(eSys.DispStage4)
            End Select

            ' .WindowState = FormWindowState.Maximized
            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            '.CalibrationEnable = True
            .ShowDialog()
        End With
    End Sub

    Private Sub btnValveWeight_Click(sender As Object, e As EventArgs) Handles btnValveWeight.Click
        gSyslog.Save("[frmCalibrationMenuNew]" & vbTab & "[btnValveWeight]" & vbTab & "Click")
        '[說明]:判斷有無開啟Recipe
        'If gCRecipe.strName = "" Then
        '    'Recipe讀檔失敗
        '    gSyslog.Save(gMsgHandler.GetMessage(Error_1002020))
        '    MsgBox(gMsgHandler.GetMessage(Error_1002020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        '    Exit Sub
        'End If

        If gfrmWeightValve1 Is Nothing Then
            gfrmWeightValve1 = New frmWeight
        ElseIf gfrmWeightValve1.IsDisposed Then
            gfrmWeightValve1 = New frmWeight
        End If

        With gfrmWeightValve1
            .StartPosition = FormStartPosition.CenterParent

            '[說明]:選擇Stage and Valve
            Select Case cmbStageNum.SelectedIndex
                Case enmStage.No1
                    .sys = gSYS(eSys.DispStage1)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve2
                    End Select
                Case enmStage.No2
                    .sys = gSYS(eSys.DispStage2)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve2
                    End Select
                Case enmStage.No3
                    .sys = gSYS(eSys.DispStage3)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve2
                    End Select
                Case enmStage.No4
                    .sys = gSYS(eSys.DispStage4)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve2
                    End Select
            End Select

            .Text = "Valve" & (cmbValveNum.SelectedIndex + 1) & " Weight"
            .ShowDialog() '.Show()
            .BringToFront()
            .Location = New Point(100, 100)
            '.StartPosition = FormStartPosition.CenterScreen
        End With
    End Sub



    Private Sub btnStageVerification_Click(sender As Object, e As EventArgs) Handles btnStageVerification.Click
        gSyslog.Save("[frmCalibrationMenuNew]" & vbTab & "[btnStageVerification]" & vbTab & "Click")
        If btnStageVerification.Enabled = False Then
            Exit Sub
        End If
        btnStageVerification.Enabled = False
        If gfrmMeasureStageVerification Is Nothing Then
            gfrmMeasureStageVerification = New frmStageVerification
        ElseIf gfrmMeasureStageVerification.IsDisposed Then
            gfrmMeasureStageVerification = New frmStageVerification
        End If
        '     Dim mfrmMeasureStageVerification = New frmStageVerification
       
        With gfrmMeasureStageVerification

            '[說明]:選擇Stage and Valve
            Select Case cmbStageNum.SelectedIndex
                Case enmStage.No1
                    .sys = gSYS(eSys.DispStage1)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve2
                    End Select
                Case enmStage.No2
                    .sys = gSYS(eSys.DispStage2)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve2
                    End Select
                Case enmStage.No3
                    .sys = gSYS(eSys.DispStage3)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve2
                    End Select
                Case enmStage.No4
                    .sys = gSYS(eSys.DispStage4)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve2
                    End Select
            End Select

            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .ShowDialog() '.Show()
            .BringToFront()
        End With
        btnStageVerification.Enabled = True
    End Sub

    Private Sub btnCCDImage_Click(sender As Object, e As EventArgs) Handles btnCCDImage.Click
        gSyslog.Save("[frmCalibrationMenuNew]" & vbTab & "[btnCCDImage]" & vbTab & "Click")
        If btnCCDImage.Enabled = False Then
            Exit Sub
        End If
        btnCCDImage.Enabled = False

        If gfrmCalibrationCCDImage Is Nothing Then
            gfrmCalibrationCCDImage = New frmCalibrationCCDImage
        ElseIf gfrmCalibrationCCDImage.IsDisposed Then
            gfrmCalibrationCCDImage = New frmCalibrationCCDImage
        End If


        '      Dim mfrmCalibrationCCD1Image = New frmCalibrationCCDImage 'frmAlignScene '
        With gfrmCalibrationCCDImage

            '[說明]:選擇Stage and Valve
            Select Case cmbStageNum.SelectedIndex
                Case enmStage.No1
                    .sys = gSYS(eSys.DispStage1)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve2
                    End Select
                Case enmStage.No2
                    .sys = gSYS(eSys.DispStage2)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve2
                    End Select
                Case enmStage.No3
                    .sys = gSYS(eSys.DispStage3)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve2
                    End Select
                Case enmStage.No4
                    .sys = gSYS(eSys.DispStage4)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve2
                    End Select
            End Select

            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .ShowDialog()
        End With
        btnCCDImage.Enabled = True
    End Sub


    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        gSyslog.Save("[frmCalibrationMenuNew]" & vbTab & "[btnBack]" & vbTab & "Click")
        Me.Close()
    End Sub


    Private Sub btnPidController_Click(sender As Object, e As EventArgs) Handles btnPidController.Click
        gSyslog.Save("[frmCalibrationMenuNew]" & vbTab & "[btnPidController]" & vbTab & "Click")
        btnPidController.Enabled = False
        Dim frmPidCtrl As WetcoConveyor.frmCalibrationPidCtrl = New WetcoConveyor.frmCalibrationPidCtrl
        frmPidCtrl.ShowDialog()
        frmPidCtrl = Nothing
        btnPidController.Enabled = True
    End Sub

    Private Sub btnContactIni_Click(sender As Object, e As EventArgs) Handles btnContactIni.Click
        gSyslog.Save("[frmCalibrationMenuNew]" & vbTab & "[btnContactIni]" & vbTab & "Click")

    
        If gfrmContactCablication Is Nothing Then
            gfrmContactCablication = New frmContactCablication
        ElseIf gfrmContactCablication.IsDisposed Then
            gfrmContactCablication = New frmContactCablication
        End If

        With gfrmContactCablication

            '[說明]:選擇Stage and Valve
            Select Case cmbStageNum.SelectedIndex
                Case enmStage.No1
                    gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
                    .sys = gSYS(eSys.DispStage1)
                Case enmStage.No2
                    gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve1
                    .sys = gSYS(eSys.DispStage2)
                Case enmStage.No3
                    gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve1
                    .sys = gSYS(eSys.DispStage3)
                Case enmStage.No4
                    gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve1
                    .sys = gSYS(eSys.DispStage4)
            End Select

            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .ShowDialog()
            
        End With
    End Sub

    Private Sub btnStageVerification_New_Click(sender As Object, e As EventArgs) Handles btnStageVerification_New.Click
        gSyslog.Save("[frmCalibrationMenuNew]" & vbTab & "[btnStageVerification]" & vbTab & "Click")
        If btnStageVerification_New.Enabled = False Then
            Exit Sub
        End If
        btnStageVerification_New.Enabled = False
        If gfrmMeasureStageVerification_new Is Nothing Then
            gfrmMeasureStageVerification_new = New frmStageVerificationNew
        ElseIf gfrmMeasureStageVerification_new.IsDisposed Then
            gfrmMeasureStageVerification_new = New frmStageVerificationNew
        End If

        With gfrmMeasureStageVerification_new

            '[說明]:選擇Stage and Valve
            Select Case cmbStageNum.SelectedIndex
                Case enmStage.No1
                    .sys = gSYS(eSys.DispStage1)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve2
                    End Select
                Case enmStage.No2
                    .sys = gSYS(eSys.DispStage2)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve2
                    End Select
                Case enmStage.No3
                    .sys = gSYS(eSys.DispStage3)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve2
                    End Select
                Case enmStage.No4
                    .sys = gSYS(eSys.DispStage4)
                    Select Case cmbValveNum.SelectedIndex
                        Case eValveWorkMode.Valve1
                            gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve1
                        Case eValveWorkMode.Valve2
                            gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve2
                    End Select
            End Select

            .StartPosition = FormStartPosition.Manual
            .Location = New Point(0, 0)
            .ShowDialog() '.Show()
            .BringToFront()
        End With
        btnStageVerification_New.Enabled = True
    End Sub

End Class
Imports ProjectRecipe
Imports ProjectCore
Imports ProjectIO
Imports ProjectMotion
Imports WetcoConveyor
Imports ProjectValveController

Public Class frmRecipeStepParameter
    Public RecipeEdit As ProjectRecipe.CRecipe

    'Soni + 2016.12.10
    ''' <summary>本頁用語系轉換</summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetString(ByVal value As String) As String
        Select Case value
            Case "On"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "開"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select
            Case "Off"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "關"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select
            Case "Save OK."
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "存檔完成"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select
            Case "None"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "無"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select
            Case "OnTimer"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "計時"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select
            Case "OnRuns"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "計次"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select
            Case "OnTimerOrRuns"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return value
                    Case enmLanguageType.eTraditionalChinese
                        Return "計時或計次先到"
                    Case enmLanguageType.eSimplifiedChinese
                        Return value
                End Select

        End Select
        Return value
    End Function


#Region "Load/Activated/Deactivate/Timer"




    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        lblSyringeAPMax1.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Syringe, 0).ToString("0.##")
        lblSyringeAPMax2.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Syringe, 1).ToString("0.##")
        lblSyringeAPMax3.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Syringe, 2).ToString("0.##")
        lblSyringeAPMax4.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Syringe, 3).ToString("0.##")
        lblSyringeAPNow1.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Syringe, 0).ToString("0.##")
        lblSyringeAPNow2.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Syringe, 1).ToString("0.##")
        lblSyringeAPNow3.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Syringe, 2).ToString("0.##")
        lblSyringeAPNow4.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Syringe, 3).ToString("0.##")

        lblValveAPMax1.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Valve, 0).ToString("0.##")
        lblValveAPMax2.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Valve, 1).ToString("0.##")
        lblValveAPMax3.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Valve, 2).ToString("0.##")
        lblValveAPMax4.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Valve, 3).ToString("0.##")
        lblValveAPNow1.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Valve, 0).ToString("0.##")
        lblValveAPNow2.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Valve, 1).ToString("0.##")
        lblValveAPNow3.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Valve, 2).ToString("0.##")
        lblValveAPNow4.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Valve, 3).ToString("0.##")




        'Select Case gSSystemParameter.MachineType
        '    Case enmMachineType.DCSW_800AQ
        '        lblSyringeAPMax1.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Syringe, 0).ToString("0.##")
        '        lblSyringeAPMax2.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Syringe, 1).ToString("0.##")
        '        lblSyringeAPMax3.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Syringe, 2).ToString("0.##")
        '        lblSyringeAPMax4.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Syringe, 3).ToString("0.##")
        '        lblSyringeAPNow1.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Syringe, 0).ToString("0.##")
        '        lblSyringeAPNow2.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Syringe, 1).ToString("0.##")
        '        lblSyringeAPNow3.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Syringe, 2).ToString("0.##")
        '        lblSyringeAPNow4.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Syringe, 3).ToString("0.##")


        '    Case enmMachineType.eDTS_2S2V
        '        lblSyringeAPMax1.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Syringe, 0).ToString("0.##")
        '        lblSyringeAPMax2.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Syringe, 1).ToString("0.##")
        '        lblSyringeAPNow1.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Syringe, 0).ToString("0.##")
        '        lblSyringeAPNow2.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Syringe, 1).ToString("0.##")

        '    Case Else
        '        Select Case gSSystemParameter.StageUseValveCount
        '            Case eMechanismModule.OneValveOneStage
        '                lblSyringeAPMax1.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Syringe, 0).ToString("0.##")
        '                lblSyringeAPNow1.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Syringe, 0).ToString("0.##")

        '            Case eMechanismModule.TwoValveOneStage

        '                lblSyringeAPMax1.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Syringe, 0).ToString("0.##")
        '                lblSyringeAPMax2.Text = gSysAdapter.GetAirPressureMax(eEPVPressureType.Syringe, 1).ToString("0.##")
        '                lblSyringeAPNow1.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Syringe, 0).ToString("0.##")
        '                lblSyringeAPNow2.Text = gSysAdapter.GetAirPressure(eEPVPressureType.Syringe, 1).ToString("0.##")
        '        End Select

        'End Select


    End Sub

    Private Sub frmRecipeStepParameter_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Timer1.Enabled = True
    End Sub

    Private Sub frmRecipeStepParameter_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Timer1.Enabled = False

    End Sub

    Private Sub frmRecipeStepParameter_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub
    '20161206
    Private Sub frmRecipeStepParameter_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If cmbValve1.SelectedIndex = -1 Then
                    e.Cancel = True
                    cmbValve1.BackColor = Color.Yellow
                    '請選擇噴射閥1
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1019005))
                    MsgBox(gMsgHandler.GetMessage(Error_1019005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    cmbValve1.BackColor = Color.White
                End If
                If cmbValve2.SelectedIndex = -1 Then
                    e.Cancel = True
                    cmbValve2.BackColor = Color.Yellow
                    '請選擇噴射閥2
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1019006))
                    MsgBox(gMsgHandler.GetMessage(Error_1019006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    cmbValve2.BackColor = Color.White
                End If
                If cmbValve3.SelectedIndex = -1 Then
                    e.Cancel = True
                    cmbValve3.BackColor = Color.Yellow
                    '請選擇噴射閥3
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1019006))
                    MsgBox(gMsgHandler.GetMessage(Error_1019006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    cmbValve3.BackColor = Color.White
                End If
                If cmbValve4.SelectedIndex = -1 Then
                    e.Cancel = True
                    cmbValve4.BackColor = Color.Yellow
                    '請選擇噴射閥4
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1019007))
                    MsgBox(gMsgHandler.GetMessage(Error_1019007), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    cmbValve4.BackColor = Color.White
                End If

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If cmbValve1.SelectedIndex = -1 Then
                    e.Cancel = True
                    cmbValve1.BackColor = Color.Yellow
                    '請選擇噴射閥1
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1019005))
                    MsgBox(gMsgHandler.GetMessage(Error_1019005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    cmbValve1.BackColor = Color.White
                End If
                If cmbValve2.SelectedIndex = -1 Then
                    e.Cancel = True
                    cmbValve2.BackColor = Color.Yellow
                    '請選擇噴射閥2
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1019006))
                    MsgBox(gMsgHandler.GetMessage(Error_1019006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    cmbValve2.BackColor = Color.White
                End If

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        If cmbValve1.SelectedIndex = -1 Then
                            e.Cancel = True
                            cmbValve1.BackColor = Color.Yellow
                            '請選擇噴射閥1
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1019005))
                            MsgBox(gMsgHandler.GetMessage(Error_1019005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            cmbValve1.BackColor = Color.White
                        End If

                    Case eMechanismModule.TwoValveOneStage
                        If cmbValve1.SelectedIndex = -1 Then
                            e.Cancel = True
                            cmbValve1.BackColor = Color.Yellow
                            '請選擇噴射閥1
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1019005))
                            MsgBox(gMsgHandler.GetMessage(Error_1019005), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            cmbValve1.BackColor = Color.White
                        End If

                        If cmbValve2.SelectedIndex = -1 Then
                            e.Cancel = True
                            cmbValve2.BackColor = Color.Yellow
                            '請選擇噴射閥2
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1019006))
                            MsgBox(gMsgHandler.GetMessage(Error_1019006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            cmbValve2.BackColor = Color.White
                        End If

                        'If cmbValve2.SelectedIndex = -1 Then
                        '    e.Cancel = True
                        '    cmbValve2.BackColor = Color.Yellow
                        '    '請選擇噴射閥2
                        '    gSyslog.Save(gMsgHandler.GetMessage(Error_1019006))
                        '    MsgBox(gMsgHandler.GetMessage(Error_1019006))
                        '    cmbValve2.BackColor = Color.White
                        'End If
                End Select

        End Select
    End Sub

    Private Sub frmRecipeStepParameter_Load(sender As Object, e As EventArgs) Handles Me.Load
        UcPurgeDB1.RecipeEdit = Me.RecipeEdit
        UcArcTypeParameter.RecipeEdit = Me.RecipeEdit

        cmbDispMode.Visible = False
        Label87.Visible = False
        nmuTempWorkStationSet.Maximum = gSSystemParameter.HotplateMaxTemperature
        nmuTempWorkStationSet.Minimum = gSSystemParameter.HotplateMinTemperature
        nmuTempPostStationSet.Maximum = gSSystemParameter.HotplateMaxTemperature
        nmuTempPostStationSet.Minimum = gSSystemParameter.HotplateMinTemperature
        nmuTempPreStationSet.Maximum = gSSystemParameter.HotplateMaxTemperature
        nmuTempPreStationSet.Minimum = gSSystemParameter.HotplateMinTemperature

        nmuTempNozzleSet1.Maximum = gSSystemParameter.ValveMaxTemperature
        nmuTempNozzleSet2.Maximum = gSSystemParameter.ValveMaxTemperature
        nmuTempNozzleSet3.Maximum = gSSystemParameter.ValveMaxTemperature
        nmuTempNozzleSet4.Maximum = gSSystemParameter.ValveMaxTemperature
        'Jeffadd 20160726
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                cbxValve.Items.Clear()
                cbxValve.Items.Add("1")
                cbxValve.Items.Add("2")
                cbxValve.Items.Add("3")
                cbxValve.Items.Add("4")
                rdbSearchDirection2.Visible = False '封鎖左右跑
                rdbSearchDirection3.Visible = False '封鎖左右跑
                lblValve1.Visible = True
                txtGlueOffsetX1.Visible = True
                txtGlueOffsetY1.Visible = True
                'Sue20170704
                txtGlueOffsetZ1.Visible = False
                lblValve2.Visible = True
                txtGlueOffsetX2.Visible = True
                txtGlueOffsetY2.Visible = True
                'Sue20170704
                txtGlueOffsetZ2.Visible = False
                lblValve3.Visible = True
                txtGlueOffsetX3.Visible = True
                txtGlueOffsetY3.Visible = True
                'Sue20170704
                txtGlueOffsetZ3.Visible = False
                lblValve4.Visible = True
                txtGlueOffsetX4.Visible = True
                txtGlueOffsetY4.Visible = True
                'Sue20170704
                txtGlueOffsetZ4.Visible = False
                grpValveAir1.Visible = True
                grpValveAir2.Visible = True
                grpValveAir3.Visible = True
                grpValveAir4.Visible = True
                cmbValveFlowRate1.Visible = True
                cmbValveFlowRate2.Visible = True
                cmbValveFlowRate3.Visible = True
                cmbValveFlowRate4.Visible = True
                cmbBaseOn.Visible = True
                cmbValve1.Visible = True
                cmbValve2.Visible = True
                cmbValve3.Visible = True
                cmbValve4.Visible = True
                lblValveParameter1.Visible = True
                lblValveParameter2.Visible = True
                lblValveParameter3.Visible = True
                lblValveParameter4.Visible = True
                cmbValvePaste1.Visible = True
                cmbValvePaste2.Visible = True
                cmbValvePaste3.Visible = True
                cmbValvePaste4.Visible = True
                lblPasteParameter1.Visible = True
                lblPasteParameter2.Visible = True
                lblPasteParameter3.Visible = True
                lblPasteParameter4.Visible = True
                UcPurgeDB1.SetValve(enmValve.No4)
                grpLoader.Visible = True
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                cbxValve.Items.Clear()
                cbxValve.Items.Add("1")
                cbxValve.Items.Add("2")
                rdbSearchDirection2.Visible = False '封鎖左右跑
                rdbSearchDirection3.Visible = False '封鎖左右跑
                lblValve1.Visible = True
                txtGlueOffsetX1.Visible = True
                txtGlueOffsetY1.Visible = True
                'Sue20170704
                txtGlueOffsetZ1.Visible = False
                lblValve2.Visible = True
                txtGlueOffsetX2.Visible = True
                txtGlueOffsetY2.Visible = True
                'Sue20170704
                txtGlueOffsetZ2.Visible = False
                lblValve3.Visible = False '封鎖後兩閥選項
                txtGlueOffsetX3.Visible = False
                txtGlueOffsetY3.Visible = False
                txtGlueOffsetZ3.Visible = False
                lblValve4.Visible = False
                txtGlueOffsetX4.Visible = False
                txtGlueOffsetY4.Visible = False
                txtGlueOffsetZ4.Visible = False
                grpValveAir1.Visible = True
                grpValveAir2.Visible = True
                grpValveAir3.Visible = False
                grpValveAir4.Visible = False
                cmbValveFlowRate1.Visible = True
                cmbValveFlowRate2.Visible = True
                cmbValveFlowRate3.Visible = False
                cmbValveFlowRate4.Visible = False
                cmbBaseOn.Visible = True
                cmbValve1.Visible = True
                cmbValve2.Visible = True
                cmbValve3.Visible = False
                cmbValve4.Visible = False
                lblValveParameter1.Visible = True
                lblValveParameter2.Visible = True
                lblValveParameter3.Visible = False
                lblValveParameter4.Visible = False
                cmbValvePaste1.Visible = True
                cmbValvePaste2.Visible = True
                cmbValvePaste3.Visible = False
                cmbValvePaste4.Visible = False
                lblPasteParameter1.Visible = True
                lblPasteParameter2.Visible = True
                lblPasteParameter3.Visible = False
                lblPasteParameter4.Visible = False
                UcPurgeDB1.SetValve(enmValve.No2)
                grpLoader.Visible = False

            Case Else
                cbxValve.Items.Clear()

                rdbSearchDirection2.Visible = True
                rdbSearchDirection3.Visible = True
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        cbxValve.Items.Add("1")

                        grpValveAir1.Visible = True
                        grpValveAir2.Visible = False
                        grpValveAir3.Visible = False
                        grpValveAir4.Visible = False
                        lblValve1.Visible = True
                        txtGlueOffsetX1.Visible = True
                        txtGlueOffsetY1.Visible = True
                        'Sue20170704
                        txtGlueOffsetZ1.Visible = False
                        lblValve2.Visible = False
                        txtGlueOffsetX2.Visible = False
                        txtGlueOffsetY2.Visible = False
                        txtGlueOffsetZ2.Visible = False
                        lblValve3.Visible = False
                        txtGlueOffsetX3.Visible = False
                        txtGlueOffsetY3.Visible = False
                        txtGlueOffsetZ3.Visible = False
                        lblValve4.Visible = False
                        txtGlueOffsetX4.Visible = False
                        txtGlueOffsetY4.Visible = False
                        txtGlueOffsetZ4.Visible = False
                        cmbValveFlowRate1.Visible = True
                        cmbValveFlowRate2.Visible = False
                        cmbValveFlowRate3.Visible = False
                        cmbValveFlowRate4.Visible = False
                        cmbBaseOn.Visible = True
                        cmbValve1.Visible = True
                        cmbValve2.Visible = False
                        cmbValve3.Visible = False
                        cmbValve4.Visible = False
                        lblValveParameter1.Visible = True
                        lblValveParameter2.Visible = False
                        lblValveParameter3.Visible = False
                        lblValveParameter4.Visible = False
                        cmbValvePaste1.Visible = True
                        cmbValvePaste2.Visible = False
                        cmbValvePaste3.Visible = False
                        cmbValvePaste4.Visible = False
                        lblPasteParameter1.Visible = True
                        lblPasteParameter2.Visible = False
                        lblPasteParameter3.Visible = False
                        lblPasteParameter4.Visible = False
                        UcPurgeDB1.SetValve(enmValve.No1)

                        '20170327

                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) = eValveModel.Advanjet Then
                            btnSetSyringeAP1.Enabled = True
                            btnSetSyringe1OnOff.Enabled = True
                            btnSetValveAP1.Enabled = True
                            btnSetValve1OnOff.Enabled = True
                        ElseIf gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) = eValveModel.PicoPulse Then
                            btnSetSyringeAP1.Enabled = True
                            btnSetSyringe1OnOff.Enabled = True
                            btnSetValveAP1.Enabled = False
                            btnSetValve1OnOff.Enabled = False
                        End If


                    Case eMechanismModule.TwoValveOneStage
                        cbxValve.Items.Add("1")
                        cbxValve.Items.Add("2")
                        'jimmy 20170630
                        grpValveAir1.Visible = True
                        ' grpValveAir2.Visible = True
                        grpValveAir3.Visible = False
                        grpValveAir4.Visible = False
                        lblValve1.Visible = True
                        txtGlueOffsetX1.Visible = True
                        txtGlueOffsetY1.Visible = True
                        'Sue20170704
                        txtGlueOffsetZ1.Visible = False
                        lblValve2.Visible = True
                        txtGlueOffsetX2.Visible = True
                        txtGlueOffsetY2.Visible = True
                        'Sue20170704
                        txtGlueOffsetZ2.Visible = False
                        lblValve3.Visible = False '封鎖後兩閥選項
                        txtGlueOffsetX3.Visible = False
                        txtGlueOffsetY3.Visible = False
                        txtGlueOffsetZ3.Visible = False
                        lblValve4.Visible = False
                        txtGlueOffsetX4.Visible = False
                        txtGlueOffsetY4.Visible = False
                        txtGlueOffsetZ4.Visible = False
                        cmbValveFlowRate1.Visible = True
                        '  cmbValveFlowRate2.Visible = True
                        cmbValveFlowRate3.Visible = False
                        cmbValveFlowRate4.Visible = False
                        cmbBaseOn.Visible = True
                        cmbValve1.Visible = True
                        ' cmbValve2.Visible = True
                        cmbValve3.Visible = False
                        cmbValve4.Visible = False
                        lblValveParameter1.Visible = True
                        ' lblValveParameter2.Visible = True
                        lblValveParameter3.Visible = False
                        lblValveParameter4.Visible = False
                        cmbValvePaste1.Visible = True
                        '  cmbValvePaste2.Visible = True
                        cmbValvePaste3.Visible = False
                        cmbValvePaste4.Visible = False
                        lblPasteParameter1.Visible = True
                        lblPasteParameter2.Visible = True
                        lblPasteParameter3.Visible = False
                        lblPasteParameter4.Visible = False
                        '   UcPurgeDB1.SetValve(enmValve.No2)

                        cmbValveFlowRate2.Visible = True
                        cmbValve2.Visible = True
                        lblValveParameter2.Visible = True
                        cmbValvePaste2.Visible = True
                        lblPasteParameter2.Visible = True
                        UcPurgeDB1.SetValve(enmValve.No2)
                        grpValveAir2.Visible = True

                        '20170327
                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) = eValveModel.Advanjet Then
                            btnSetSyringeAP1.Enabled = True
                            btnSetSyringe1OnOff.Enabled = True
                            btnSetValveAP1.Enabled = True
                            btnSetValve1OnOff.Enabled = True
                        ElseIf gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) = eValveModel.PicoPulse Then
                            btnSetSyringeAP1.Enabled = True
                            btnSetSyringe1OnOff.Enabled = True
                            btnSetValveAP1.Enabled = False
                            btnSetValve1OnOff.Enabled = False
                        End If
                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve2) = eValveModel.Advanjet Then
                            btnSetSyringeAP2.Enabled = True
                            btnSetSyringe2OnOff.Enabled = True
                            btnSetValveAP2.Enabled = True
                            btnSetValve2OnOff.Enabled = True
                        ElseIf gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve2) = eValveModel.PicoPulse Then
                            btnSetSyringeAP2.Enabled = True
                            btnSetSyringe2OnOff.Enabled = True
                            btnSetValveAP2.Enabled = False
                            btnSetValve2OnOff.Enabled = False
                        End If

                End Select
                grpLoader.Visible = False
        End Select

        If (gSSystemParameter.MachineType = enmMachineType.DCS_350A) Then
            grpConveyorSet.Visible = True
        End If
        If (gSSystemParameter.MachineType = enmMachineType.DCS_350A Or gSSystemParameter.MachineType = enmMachineType.DCS_500AD) Then
            Label10.Visible = True
            cmbLaserRunMode.Visible = True
        End If

        '[Note]20170419 Recipe是否可設飛拍功能
        If gSSystemParameter.CCDOnFlyEnable Then
            grpOnFlySet.Visible = True
        Else
            grpOnFlySet.Visible = False
        End If

        Select Case gDOCollection.GetState(enmDO.SyringePressure1)
            Case True
                btnSetSyringe1OnOff.Text = GetString("On")
            Case False
                btnSetSyringe1OnOff.Text = GetString("Off")
        End Select
        Select Case gDOCollection.GetState(enmDO.SyringePressure2)
            Case True
                btnSetSyringe2OnOff.Text = GetString("On")
            Case False
                btnSetSyringe2OnOff.Text = GetString("Off")
        End Select
        Select Case gDOCollection.GetState(enmDO.SyringePressure3)
            Case True
                btnSetSyringe3OnOff.Text = GetString("On")
            Case False
                btnSetSyringe3OnOff.Text = GetString("Off")
        End Select
        Select Case gDOCollection.GetState(enmDO.SyringePressure4)
            Case True
                btnSetSyringe4OnOff.Text = GetString("On")
            Case False
                btnSetSyringe4OnOff.Text = GetString("Off")
        End Select

        '20170327
        Select Case gDOCollection.GetState(enmDO.ValvePressure1)
            Case True
                btnSetValve1OnOff.Text = GetString("On")
            Case False
                btnSetValve1OnOff.Text = GetString("Off")
        End Select
        Select Case gDOCollection.GetState(enmDO.ValvePressure2)
            Case True
                btnSetValve2OnOff.Text = GetString("On")
            Case False
                btnSetValve2OnOff.Text = GetString("Off")
        End Select
        Select Case gDOCollection.GetState(enmDO.ValvePressure3)
            Case True
                btnSetValve3OnOff.Text = GetString("On")
            Case False
                btnSetValve3OnOff.Text = GetString("Off")
        End Select

        Select Case gDOCollection.GetState(enmDO.ValvePressure4)
            Case True
                btnSetValve4OnOff.Text = GetString("On")
            Case False
                btnSetValve4OnOff.Text = GetString("Off")
        End Select

        '20161102
        cmbValveChooes.Items.Clear()
        cmbValveChooes.Items.Add("PicoPulse")
        cmbValveChooes.Items.Add("Advanjet")
        cmbValveChooes.SelectedIndex = 0   '20170612

        cmbBaseOn.Items.Clear()
        cmbBaseOn.Items.Add(GetString("None"))
        cmbBaseOn.Items.Add(GetString("OnTimer"))
        cmbBaseOn.Items.Add(GetString("OnRuns"))
        cmbBaseOn.Items.Add(GetString("OnTimerOrRuns"))

        cmbDispMode.Items.Clear()
        cmbDispMode.Items.Add("NonHistory")
        cmbDispMode.Items.Add("History")
        cmbProductType.Items.Clear()
        cmbProductType.Items.Add("Wafer")
        cmbProductType.Items.Add("Panel")
        cmbProductType.Items.Add("Strip")

        cmbRunType.Items.Clear()
        cmbRunType.Items.Add("Dots") 'eWeightControlType.Dots
        cmbRunType.Items.Add("Weight") 'eWeightControlType.Weight
        'cmbRunType.Items.Add("Complex") 'eWeightControlType.Complex '[Note]目前無此功能 20170908
        cmbRunType.Items.Add("Velocity") 'eWeightControlType.Velocity()

        ShowTempConfig() '溫度顯示設定

        RecipeLoad()
        PasteDB_Update(RecipeEdit.StageParts(enmStage.No1).PasteName(eValveWorkMode.Valve1))
        JetValveDB_Update(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1))
        AugerValveDB_Update()
        TempDB_Update(RecipeEdit.TempName)
        WeightDB_Update(RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve1))
        UcPurgeDB1.UpdatePurgeUI()
        UpdateSyringe()
        UcDotTypeParameter.SetupDataLink(gDotValueDB) 'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter 
        UcDotTypeParameter.DotValveDB_Update() 'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter

        UcLineTypeParameter.SetupDataLink(gLineValueDB) 'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter
        UcLineTypeParameter.LineValveDB_Update() 'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter

        UcArcTypeParameter.SetupDataLink(gArcValueDB) 'Eason 20170216 Ticket:100080 , Add Arc Type Parameter
        UcArcTypeParameter.ArcValveDB_Update() 'Eason 20170216 Ticket:100080 , Add Arc Type Parameter

        '[說明]:關閉目前不使用的項目
        TabControl2.Controls.Remove(tabAuger)
        TabControl2.Controls.Remove(tabConveyor)




        Timer1.Enabled = True

        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)


    End Sub

#End Region


    Private Sub UpdateSyringe()
        Select Case gDOCollection.GetState(enmDO.SyringePressure1)
            Case True
                btnSetSyringe1OnOff.Text = GetString("On")
            Case False
                btnSetSyringe1OnOff.Text = GetString("Off")
        End Select
        Select Case gDOCollection.GetState(enmDO.SyringePressure2)
            Case True
                btnSetSyringe2OnOff.Text = GetString("On")
            Case False
                btnSetSyringe2OnOff.Text = GetString("Off")
        End Select
        Select Case gDOCollection.GetState(enmDO.SyringePressure3)
            Case True
                btnSetSyringe3OnOff.Text = GetString("On")
            Case False
                btnSetSyringe3OnOff.Text = GetString("Off")
        End Select
        Select Case gDOCollection.GetState(enmDO.SyringePressure4)
            Case True
                btnSetSyringe4OnOff.Text = GetString("On")
            Case False
                btnSetSyringe4OnOff.Text = GetString("Off")
        End Select
    End Sub


#Region "Recipe"

    Public Sub SetRecipeAP(ByRef recipe As CRecipe, ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByRef txtSyringeAPSet As TextBox, ByRef txtValveAPSet As TextBox)
        txtSyringeAPSet.Text = recipe.StageParts(stageNo).SyringePressure(valveNo)
        txtValveAPSet.Text = recipe.StageParts(stageNo).ValvePressure(valveNo)
    End Sub

    Public Sub GetRecipeAP(ByRef recipe As CRecipe, ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByRef txtSyringeAPSet As TextBox, ByRef txtValveAPSet As TextBox)
        recipe.StageParts(stageNo).SyringePressure(valveNo) = Val(txtSyringeAPSet.Text)
        recipe.StageParts(stageNo).ValvePressure(valveNo) = Val(txtValveAPSet.Text)
    End Sub

    Public Sub SetRecipeSearchDir(ByRef recipe As CRecipe, ByRef rdbSearchDirection0 As RadioButton, ByRef rdbSearchDirection1 As RadioButton, ByRef rdbSearchDirection2 As RadioButton, ByRef rdbSearchDirection3 As RadioButton)
        Select Case recipe.SearchType
            Case enmSearchType.Y_Snake
                rdbSearchDirection0.Checked = True
            Case enmSearchType.Y_ZigZag
                rdbSearchDirection1.Checked = True
            Case enmSearchType.X_Snake
                rdbSearchDirection2.Checked = True
            Case enmSearchType.X_ZigZag
                rdbSearchDirection3.Checked = True
        End Select

    End Sub

    Public Sub GetRecipeSearchDir(ByRef recipe As CRecipe, ByRef rdbSearchDirection0 As RadioButton, ByRef rdbSearchDirection1 As RadioButton, ByRef rdbSearchDirection2 As RadioButton, ByRef rdbSearchDirection3 As RadioButton)
        If rdbSearchDirection0.Checked = True Then
            recipe.SearchType = enmSearchType.Y_Snake
        ElseIf rdbSearchDirection1.Checked = True Then
            recipe.SearchType = enmSearchType.Y_ZigZag
        ElseIf rdbSearchDirection2.Checked = True Then
            recipe.SearchType = enmSearchType.X_Snake
        ElseIf rdbSearchDirection3.Checked = True Then
            recipe.SearchType = enmSearchType.X_ZigZag
        End If
    End Sub


    Public Sub SetRecipePartName(ByRef recipe As CRecipe, ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByRef lstValveName As ComboBox, ByRef lstPasteName As ComboBox, ByRef lstFlowRateName As ComboBox)
        With recipe.StageParts(stageNo)
            Try
                lstValveName.Text = .ValveName(valveNo)
            Catch ex As Exception
                lstValveName.SelectedIndex = -1
            End Try

            Try
                lstPasteName.Text = .PasteName(valveNo)
            Catch ex As Exception
                lstPasteName.SelectedIndex = -1
            End Try

            Try
                lstFlowRateName.Text = .FlowRateName(valveNo)
            Catch ex As Exception
                lstFlowRateName.SelectedIndex = -1
            End Try
        End With
    End Sub

    Public Sub GetRecipePartName(ByRef recipe As CRecipe, ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByRef lstValveName As ComboBox, ByRef lstPasteName As ComboBox, ByRef lstFlowRateName As ComboBox, ByRef lstPurgeName As ComboBox)
        With recipe.StageParts(stageNo)
            If lstValveName.SelectedIndex <> -1 Then
                .ValveName(valveNo) = lstValveName.Text
            End If
            If lstPasteName.SelectedIndex <> -1 Then
                .PasteName(valveNo) = lstPasteName.Text
            End If
            If lstFlowRateName.SelectedIndex <> -1 Then
                .FlowRateName(valveNo) = lstFlowRateName.Text
            End If
            If lstPurgeName.SelectedIndex <> -1 Then
                .PurgeName(valveNo) = lstPurgeName.Text
            End If
        End With
    End Sub

    Public Sub SetRecipeManualOFfset(ByRef recipe As CRecipe, ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByRef txtValveOffsetX As TextBox, ByRef txtValveOffsetY As TextBox, ByRef txtValveOffsetZ As TextBox)
        With recipe
            txtValveOffsetX.Text = recipe.StageParts(stageNo).ValveShiftX(valveNo)
            txtValveOffsetY.Text = recipe.StageParts(stageNo).ValveShiftY(valveNo)
            txtValveOffsetZ.Text = recipe.StageParts(stageNo).ValveShiftZ(valveNo)
            '20160920

        End With
    End Sub
    Public Sub GetRecipeManualOFfset(ByRef recipe As CRecipe, ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByRef txtValveOffsetX As TextBox, ByRef txtValveOffsetY As TextBox, ByRef txtValveOffsetZ As TextBox)
        With recipe
            recipe.StageParts(stageNo).ValveShiftX(valveNo) = Val(txtValveOffsetX.Text)
            recipe.StageParts(stageNo).ValveShiftY(valveNo) = Val(txtValveOffsetY.Text)
            recipe.StageParts(stageNo).ValveShiftZ(valveNo) = Val(txtValveOffsetZ.Text)
        End With
    End Sub

    Public Sub RecipeSave()
        Dim mIsValueDBExit As Boolean

        RecipeEdit.ConveyorSpeed = Val(txtConveyorSpeed.Text)
        RecipeEdit.BypassLaserResult = chkBypassLaserResult.Checked
        RecipeEdit.BypassCCDResult = chkByPassCCDResult.Checked
        RecipeEdit.BypassRotationCorrection = chkByPassRotationCorrection.Checked

        If chkOnFly.Checked Then
            RecipeEdit.CCDFixModel = eCCDFixModel.OnFly
        Else
            RecipeEdit.CCDFixModel = eCCDFixModel.NonOnFly
        End If

        'If chkOnFly.Checked Then
        '    '[Note]檢查飛拍觸發的頻率是否會掉張
        '    For mStageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
        '        Dim KeyCollect As Dictionary(Of String, CRecipeNode).KeyCollection = Recipe.Node(mStageNo).Keys
        '        For mNodeNo As Integer = 0 To KeyCollect.Count - 1
        '            Dim mNodeKey As String = KeyCollect(mNodeNo)
        '            For mArrayNo As Integer = 0 To Recipe.Node(mStageNo)(mNodeKey).Array.Count - 1
        '                If Recipe.Node(mStageNo)(mNodeKey).Array(mArrayNo).LevelType = eLevelType.NoneArray Then
        '                    '[Note]飛拍不能支援不定頻觸發
        '                    Select Case gSSystemParameter.LanguageType
        '                        Case enmLanguageType.eEnglish
        '                            MsgBox("CCD On-Fly can't use NoneArray ")
        '                        Case enmLanguageType.eSimplifiedChinese
        '                            MsgBox("CCD 飛拍不能支援非陣列")
        '                        Case enmLanguageType.eTraditionalChinese
        '                            MsgBox("CCD 飞拍不能支援非阵列")
        '                    End Select
        '                    chkOnFly.CheckState = CheckState.Unchecked
        '                    Recipe.CCDFixModel = eCCDFixModel.NonOnFly
        '                    Recipe.LoadStatus = False
        '                    Exit Sub
        '                Else
        '                    '[Note]定頻觸發
        '                    'If Recipe.Node(mStageNo)(mNodeKey).NodeStartingX = 0 And Recipe.Node(mStageNo)(mNodeKey).NodeStartingY = 0 Then
        '                    '    '[Note]單點飛拍?
        '                    'Else
        '                    'End If
        '                    Dim AcqTime As Double = 0.02 'm/s
        '                    Select Case Recipe.SearchType
        '                        Case enmSearchType.X_Snake, enmSearchType.X_ZigZag
        '                            If Recipe.Node(mStageNo)(mNodeKey).Array(mArrayNo).Array.CountX > 1 Then
        '                                If (Math.Abs(Recipe.Node(mStageNo)(mNodeKey).Array(mArrayNo).Array.PitchX) / nmcOnFlySpeed.Value) < AcqTime Then
        '                                    Select Case gSSystemParameter.LanguageType
        '                                        Case enmLanguageType.eEnglish
        '                                            MsgBox("CCD On-Fly frequency does not match,Please set Slower speed  ")
        '                                        Case enmLanguageType.eSimplifiedChinese
        '                                            MsgBox("CCD 飛拍頻率不符，請將飛拍速度設低")
        '                                        Case enmLanguageType.eTraditionalChinese
        '                                            MsgBox("CCD 飞拍频率不符，请将飞拍速度设低")
        '                                    End Select
        '                                    nmcOnFlySpeed.Value = (Math.Abs(Recipe.Node(mStageNo)(mNodeKey).Array(mArrayNo).Array.PitchX) / (AcqTime + 0.005))
        '                                    Recipe.LoadStatus = False
        '                                    Exit Sub
        '                                End If
        '                            Else
        '                                '[Note]飛拍不能支援單點
        '                                Select Case gSSystemParameter.LanguageType
        '                                    Case enmLanguageType.eEnglish
        '                                        MsgBox("CCD On-Fly can't use dot ")
        '                                    Case enmLanguageType.eSimplifiedChinese
        '                                        MsgBox("CCD 飛拍不能支援單點")
        '                                    Case enmLanguageType.eTraditionalChinese
        '                                        MsgBox("CCD 飞拍不能支援奌")
        '                                End Select
        '                                chkOnFly.CheckState = CheckState.Unchecked
        '                                Recipe.LoadStatus = False
        '                                Exit Sub
        '                            End If

        '                        Case enmSearchType.Y_ZigZag, enmSearchType.Y_Snake
        '                            If Recipe.Node(mStageNo)(mNodeKey).Array(mArrayNo).Array.CountY > 1 Then
        '                                If (Math.Abs(Recipe.Node(mStageNo)(mNodeKey).Array(mArrayNo).Array.PitchY) / nmcOnFlySpeed.Value) < AcqTime Then
        '                                    Select Case gSSystemParameter.LanguageType
        '                                        Case enmLanguageType.eEnglish
        '                                            MsgBox("CCD On-Fly frequency does not match,Please set Slower speed  ")
        '                                        Case enmLanguageType.eSimplifiedChinese
        '                                            MsgBox("CCD 飛拍頻率不符，請將飛拍速度設低")
        '                                        Case enmLanguageType.eTraditionalChinese
        '                                            MsgBox("CCD 飞拍频率不符，请将飞拍速度设低")
        '                                    End Select
        '                                    nmcOnFlySpeed.Value = (Math.Abs(Recipe.Node(mStageNo)(mNodeKey).Array(mArrayNo).Array.PitchY) / (AcqTime + 0.005))
        '                                    Recipe.LoadStatus = False
        '                                    Exit Sub
        '                                End If
        '                            Else
        '                                '[Note]飛拍不能支援單點
        '                                Select Case gSSystemParameter.LanguageType
        '                                    Case enmLanguageType.eEnglish
        '                                        MsgBox("CCD On-Fly can't use dot ")
        '                                    Case enmLanguageType.eSimplifiedChinese
        '                                        MsgBox("CCD 飛拍不能支援單點")
        '                                    Case enmLanguageType.eTraditionalChinese
        '                                        MsgBox("CCD 飞拍不能支援奌")
        '                                End Select
        '                                chkOnFly.CheckState = CheckState.Unchecked
        '                                Recipe.LoadStatus = False
        '                                Exit Sub
        '                            End If

        '                    End Select
        '                End If
        '            Next
        '        Next
        '    Next
        'End If


        RecipeEdit.CCDOnFlySpeed = nmcOnFlySpeed.Value
        RecipeEdit.CCDOnFlyDelayTime = nmcOnFlyDelayTime.Value


        '20160920
        RecipeEdit.IsJustOneRun = cbJustOneRun.Checked
        RecipeEdit.IsStopAlignNG = cbIsStopAlignNg.Checked


        RecipeEdit.LeadAngle.Degress = Val(txtLeadAngle.Text)
        RecipeEdit.LeadAngle.Distance = Val(txtLeadAngleDistance.Text)

        'Soni / 2017.09.19
        Select Case cmbRunType.SelectedIndex
            Case 0, -1
                RecipeEdit.RunType = eWeightControlType.Dots
            Case 1
                RecipeEdit.RunType = eWeightControlType.Weight
            Case 2
                RecipeEdit.RunType = eWeightControlType.Velocity
        End Select

        Select Case cmbProductType.SelectedIndex
            Case 0, -1
                RecipeEdit.ProductType = enmProductType.Wafer
            Case 1
                RecipeEdit.ProductType = enmProductType.Panel
            Case 2
                RecipeEdit.ProductType = enmProductType.Strip
        End Select

        Select Case cmbDispMode.SelectedIndex
            Case 0
                RecipeEdit.DispHistory = eDispHistoryModel.DispNonHistory
            Case 1
                RecipeEdit.DispHistory = eDispHistoryModel.DispHistory
            Case Else
                RecipeEdit.DispHistory = eDispHistoryModel.DispNonHistory
        End Select


        Select Case cmbLaserRunMode.SelectedIndex
            Case 0
                RecipeEdit.LaserRunMode = eLaserRunModel.Array
            Case 1
                RecipeEdit.LaserRunMode = eLaserRunModel.NonArray
            Case Else
                RecipeEdit.LaserRunMode = eLaserRunModel.Array
        End Select

        'GetRecipeTemp(Recipe, enmTemp.Nozzle1, chkTempNozzle1, txtTempNozzleSet1)
        'GetRecipeTemp(Recipe, enmTemp.Nozzle2, chkTempNozzle2, txtTempNozzleSet2)
        'GetRecipeTemp(Recipe, enmTemp.PostStation, chkTempPostStation, txtTempPostStationSet)
        'GetRecipeTemp(Recipe, enmTemp.PreStation, chkTempPreStation, txtTempPreStationSet)
        'GetRecipeTemp(Recipe, enmTemp.SyringeBody1, chkTempSyringeBody1, txtTempSyringeBodySet1)
        'GetRecipeTemp(Recipe, enmTemp.SyringeBody2, chkTempSyringeBody2, txtTempSyringeBodySet2)
        'GetRecipeTemp(Recipe, enmTemp.ValveBody1, chkTempValveBody1, txtTempValveBodySet1)
        'GetRecipeTemp(Recipe, enmTemp.ValveBody2, chkTempValveBody2, txtTempValveBodySet2)
        'GetRecipeTemp(Recipe, enmTemp.WorkStation, chkTempWorkStation, txtTempWorkStationSet)
        GetRecipeSearchDir(RecipeEdit, rdbSearchDirection0, rdbSearchDirection1, rdbSearchDirection2, rdbSearchDirection3)
        If cmbTemp1.Items.Count > 0 Then
            RecipeEdit.TempName = cmbTemp1.SelectedItem
        Else
            RecipeEdit.TempName = ""
        End If

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                GetRecipeManualOFfset(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtGlueOffsetX1, txtGlueOffsetY1, txtGlueOffsetZ1)
                GetRecipeManualOFfset(RecipeEdit, enmStage.No2, eValveWorkMode.Valve1, txtGlueOffsetX2, txtGlueOffsetY2, txtGlueOffsetZ2)
                GetRecipeManualOFfset(RecipeEdit, enmStage.No3, eValveWorkMode.Valve1, txtGlueOffsetX3, txtGlueOffsetY3, txtGlueOffsetZ3)
                GetRecipeManualOFfset(RecipeEdit, enmStage.No4, eValveWorkMode.Valve1, txtGlueOffsetX4, txtGlueOffsetY4, txtGlueOffsetZ4)
                GetRecipeAP(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtSyringeAPSet1, txtValveAPSet1)
                GetRecipeAP(RecipeEdit, enmStage.No2, eValveWorkMode.Valve1, txtSyringeAPSet2, txtValveAPSet2)
                GetRecipeAP(RecipeEdit, enmStage.No3, eValveWorkMode.Valve1, txtSyringeAPSet3, txtValveAPSet3)
                GetRecipeAP(RecipeEdit, enmStage.No4, eValveWorkMode.Valve1, txtSyringeAPSet4, txtValveAPSet4)
                GetRecipePartName(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, cmbValve1, cmbValvePaste1, cmbValveFlowRate1, UcPurgeDB1.cmbValvePurge1)
                GetRecipePartName(RecipeEdit, enmStage.No2, eValveWorkMode.Valve1, cmbValve2, cmbValvePaste2, cmbValveFlowRate2, UcPurgeDB1.cmbValvePurge2)
                GetRecipePartName(RecipeEdit, enmStage.No3, eValveWorkMode.Valve1, cmbValve3, cmbValvePaste3, cmbValveFlowRate3, UcPurgeDB1.cmbValvePurge3)
                GetRecipePartName(RecipeEdit, enmStage.No4, eValveWorkMode.Valve1, cmbValve4, cmbValvePaste4, cmbValveFlowRate4, UcPurgeDB1.cmbValvePurge4)

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                GetRecipeManualOFfset(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtGlueOffsetX1, txtGlueOffsetY1, txtGlueOffsetZ1)
                GetRecipeManualOFfset(RecipeEdit, enmStage.No2, eValveWorkMode.Valve1, txtGlueOffsetX2, txtGlueOffsetY2, txtGlueOffsetZ2)
                GetRecipeAP(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtSyringeAPSet1, txtValveAPSet1)
                GetRecipeAP(RecipeEdit, enmStage.No2, eValveWorkMode.Valve1, txtSyringeAPSet2, txtValveAPSet2)
                GetRecipePartName(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, cmbValve1, cmbValvePaste1, cmbValveFlowRate1, UcPurgeDB1.cmbValvePurge1)
                GetRecipePartName(RecipeEdit, enmStage.No2, eValveWorkMode.Valve1, cmbValve2, cmbValvePaste2, cmbValveFlowRate2, UcPurgeDB1.cmbValvePurge2)

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        GetRecipeManualOFfset(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtGlueOffsetX1, txtGlueOffsetY1, txtGlueOffsetZ1)
                        GetRecipeAP(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtSyringeAPSet1, txtValveAPSet1)
                        GetRecipePartName(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, cmbValve1, cmbValvePaste1, cmbValveFlowRate1, UcPurgeDB1.cmbValvePurge1)

                    Case eMechanismModule.TwoValveOneStage
                        GetRecipeManualOFfset(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtGlueOffsetX1, txtGlueOffsetY1, txtGlueOffsetZ1)
                        GetRecipeManualOFfset(RecipeEdit, enmStage.No1, eValveWorkMode.Valve2, txtGlueOffsetX2, txtGlueOffsetY2, txtGlueOffsetZ2)
                        GetRecipeAP(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtSyringeAPSet1, txtValveAPSet1)
                        GetRecipeAP(RecipeEdit, enmStage.No1, eValveWorkMode.Valve2, txtSyringeAPSet2, txtValveAPSet2)
                        GetRecipePartName(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, cmbValve1, cmbValvePaste1, cmbValveFlowRate1, UcPurgeDB1.cmbValvePurge1)
                        GetRecipePartName(RecipeEdit, enmStage.No1, eValveWorkMode.Valve2, cmbValve2, cmbValvePaste2, cmbValveFlowRate2, UcPurgeDB1.cmbValvePurge2)

                End Select

        End Select


        RecipeEdit.GPSetRunMode = eRunMode.BufferMode ' cmbGPSetRunMode.SelectedIndex
        RecipeEdit.SaveRecipe(RecipeEdit.strFileName)
        RecipeEdit.SaveStageParts(RecipeEdit.strFileName)


        '[說明]:檢查JetDB對應參數是否設定正常
        Dim str As String = ""

        If RecipeEdit.TempName = Nothing Then
            '溫控檔不存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000032))
            MsgBox(gMsgHandler.GetMessage(Warn_3000032), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If gTempDB.ContainsKey(RecipeEdit.TempName) = False Then
            '請先選擇溫控檔
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000031))
            MsgBox(gMsgHandler.GetMessage(Warn_3000031), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '20170613
        If Not Unit.A_Roller Is Nothing Then
            If (RecipeEdit.ConveyorSpeed >= 0) Then
                If Unit.A_Roller IsNot Nothing Then
                    If (Unit.A_Roller.SetSpeed(0, RecipeEdit.ConveyorSpeed) <> True) Then
                        gEqpMsg.AddHistoryAlarm("Error_1009014", "frmRecipe btnLoadRecipe", , gMsgHandler.GetMessage(Error_1009014), eMessageLevel.Warning)  'Conveyor Roller 速度設定失敗!
                    End If
                End If
                If Unit.B_Roller IsNot Nothing Then
                    If (Unit.B_Roller.SetSpeed(0, RecipeEdit.ConveyorSpeed) <> True) Then
                        gEqpMsg.AddHistoryAlarm("Error_1009014", "frmRecipe btnLoadRecipe", , gMsgHandler.GetMessage(Error_1009014), eMessageLevel.Warning)  'Conveyor Roller 速度設定失敗!
                    End If
                End If
            End If
        End If


        For mStageNo As enmStage = enmStage.No1 To gSSystemParameter.StageCount - 1
            For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1
                mIsValueDBExit = False
                For mJ As Integer = 0 To gJetValveDB.Count - 1
                    If RecipeEdit.StageParts(mStageNo).ValveName(mValveNo) = gJetValveDB.Keys(mJ) Then
                        Select Case gSSystemParameter.StageParts.ValveData(mStageNo).ValveType(mValveNo)
                            Case enmValveType.Jet
                                '[說明]: Check Valva X 的設定與gSSystemParameter.ValveData Data 及跳出警告視窗
                                If gJetValveDB.ContainsKey(gJetValveDB.Keys(mJ)) = True Then
                                    If gJetValveDB(gJetValveDB.Keys(mJ)).ValveModel <> gSSystemParameter.StageParts.ValveData(mStageNo).JetValve(mValveNo) Or gJetValveDB.ContainsKey(RecipeEdit.StageParts(mStageNo).ValveName(mValveNo)) = False Then
                                        '請確認閥設定值!
                                        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000070))
                                        MsgBox(gMsgHandler.GetMessage(Warn_3000070), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                        Exit Sub
                                    Else
                                        mIsValueDBExit = True
                                    End If
                                End If

                            Case enmValveType.Auger
                                'TODO:後續再補Auger的檢察

                                '請確認閥設定值!
                                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000070))
                                MsgBox(gMsgHandler.GetMessage(Warn_3000070), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Exit Sub

                        End Select
                    End If
                Next
                If mIsValueDBExit = False Then
                    '請確認閥設定值!
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000070))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000070), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Exit Sub
                End If

                If gPurgeDB.ContainsKey(RecipeEdit.StageParts(mStageNo).PurgeName(mValveNo)) = False Then
                    str = "Please Check StageNo" & mStageNo & " ValveNo" & (mValveNo + 1) & " PurgeName"
                    MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Exit Sub
                End If
                If gPasteDataBase.ContainsKey(RecipeEdit.StageParts(mStageNo).PasteName(mValveNo)) = False Then
                    str = "Please Check StageNo" & mStageNo & " ValveNo" & (mValveNo + 1) & " PasteName"
                    MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Exit Sub
                End If
                If gFlowRateDB.ContainsKey(RecipeEdit.StageParts(mStageNo).FlowRateName(mValveNo)) = False Then
                    str = "Please Check StageNo" & mStageNo & " ValveNo" & (mValveNo + 1) & " FlowRateName"
                    MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Exit Sub
                End If

                '[說明]:檢查是Weight or Complex
                If RecipeEdit.RunType = eWeightControlType.Weight Then 'eWeightControlType.Complex Then
                    '[說明]:檢查(微量天平資料平均單點重量) 20170505
                    If RecipeEdit.StageParts(mStageNo).AverageWeightPerDot(mValveNo) < gFlowRateDB(RecipeEdit.StageParts(mStageNo).FlowRateName(mValveNo)).WeighingWeightDotMin Or
                       RecipeEdit.StageParts(mStageNo).AverageWeightPerDot(mValveNo) > gFlowRateDB(RecipeEdit.StageParts(mStageNo).FlowRateName(mValveNo)).WeighingWeightDotMax Then

                        Select Case gSSystemParameter.LanguageType
                            Case enmLanguageType.eEnglish
                                str = "DotWeight < WeighingWeightDotMin or DotWeight > WeighingWeightDotMax!!Please re-weigh the weight or adjust weight the maximum and minimum single"
                            Case enmLanguageType.eSimplifiedChinese
                                str = "单点重量小于最小或单点重量大于最大!!请重作秤重动作或调整单颗最大最小值!!"
                            Case enmLanguageType.eTraditionalChinese
                                str = "單點重量小於最小或單點重量大於最大!!請重作秤重動作或調整單顆最大最小值!!"
                        End Select
                        MsgBox(str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Exit Sub
                    End If
                End If


            Next
        Next

        '20170214
        If cmbMeasureZMode.SelectedIndex < 0 Then
            '請選擇測高模式
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000050))
            MsgBox(gMsgHandler.GetMessage(Warn_3000050), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        '===================================================================================================================================20161221
    End Sub
    Public Sub RecipeLoad()

        RecipeEdit.LoadStageParts(RecipeEdit.strFileName)
        chkByPassCCDResult.Checked = RecipeEdit.BypassCCDResult
        chkBypassLaserResult.Checked = RecipeEdit.BypassLaserResult
        chkByPassRotationCorrection.Checked = RecipeEdit.BypassRotationCorrection
        cbJustOneRun.Checked = RecipeEdit.IsJustOneRun
        cbIsStopAlignNg.Checked = RecipeEdit.IsStopAlignNG
        cmbDispMode.SelectedIndex = CInt(RecipeEdit.DispHistory)
        Select Case RecipeEdit.RunType
            Case eWeightControlType.Dots
                cmbRunType.SelectedIndex = 0
            Case eWeightControlType.Weight
                cmbRunType.SelectedIndex = 1
            Case eWeightControlType.Velocity
                cmbRunType.SelectedIndex = 2
        End Select

        nmcOnFlySpeed.Value = RecipeEdit.CCDOnFlySpeed
        nmcOnFlyDelayTime.Value = RecipeEdit.CCDOnFlyDelayTime
        If RecipeEdit.CCDFixModel = eCCDFixModel.NonOnFly Then
            chkOnFly.CheckState = CheckState.Unchecked
        Else
            chkOnFly.CheckState = CheckState.Checked
        End If

        txtLeadAngle.Text = RecipeEdit.LeadAngle.Degress
        txtLeadAngleDistance.Text = RecipeEdit.LeadAngle.Distance
        Select Case RecipeEdit.ProductType
            Case enmProductType.Default, enmProductType.Wafer
                cmbProductType.SelectedIndex = 0
            Case enmProductType.Panel
                cmbProductType.SelectedIndex = 1
            Case enmProductType.Strip
                cmbProductType.SelectedIndex = 2
            Case Else
                cmbProductType.SelectedIndex = 0
        End Select


        SetRecipeSearchDir(RecipeEdit, rdbSearchDirection0, rdbSearchDirection1, rdbSearchDirection2, rdbSearchDirection3)
        If cmbTemp1.Items.Count > 0 Then
            If Not RecipeEdit.TempName Is Nothing Then
                If cmbTemp1.Items.Contains(RecipeEdit.TempName) Then
                    cmbTemp1.SelectedItem = RecipeEdit.TempName
                End If
            End If
        End If


        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                SetRecipeManualOFfset(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtGlueOffsetX1, txtGlueOffsetY1, txtGlueOffsetZ1)
                SetRecipeManualOFfset(RecipeEdit, enmStage.No2, eValveWorkMode.Valve1, txtGlueOffsetX2, txtGlueOffsetY2, txtGlueOffsetZ2)
                SetRecipeManualOFfset(RecipeEdit, enmStage.No3, eValveWorkMode.Valve1, txtGlueOffsetX3, txtGlueOffsetY3, txtGlueOffsetZ3)
                SetRecipeManualOFfset(RecipeEdit, enmStage.No4, eValveWorkMode.Valve1, txtGlueOffsetX4, txtGlueOffsetY4, txtGlueOffsetZ4)
                SetRecipeAP(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtSyringeAPSet1, txtValveAPSet1)
                SetRecipeAP(RecipeEdit, enmStage.No2, eValveWorkMode.Valve1, txtSyringeAPSet2, txtValveAPSet2)
                SetRecipeAP(RecipeEdit, enmStage.No3, eValveWorkMode.Valve1, txtSyringeAPSet3, txtValveAPSet3)
                SetRecipeAP(RecipeEdit, enmStage.No4, eValveWorkMode.Valve1, txtSyringeAPSet4, txtValveAPSet4)
                SetRecipePartName(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, cmbValve1, cmbValvePaste1, cmbValveFlowRate1)
                SetRecipePartName(RecipeEdit, enmStage.No2, eValveWorkMode.Valve1, cmbValve2, cmbValvePaste2, cmbValveFlowRate2)
                SetRecipePartName(RecipeEdit, enmStage.No3, eValveWorkMode.Valve1, cmbValve3, cmbValvePaste3, cmbValveFlowRate3)
                SetRecipePartName(RecipeEdit, enmStage.No4, eValveWorkMode.Valve1, cmbValve4, cmbValvePaste4, cmbValveFlowRate4)

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                SetRecipeManualOFfset(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtGlueOffsetX1, txtGlueOffsetY1, txtGlueOffsetZ1)
                SetRecipeManualOFfset(RecipeEdit, enmStage.No2, eValveWorkMode.Valve1, txtGlueOffsetX2, txtGlueOffsetY2, txtGlueOffsetZ2)
                SetRecipeAP(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtSyringeAPSet1, txtValveAPSet1)
                SetRecipeAP(RecipeEdit, enmStage.No2, eValveWorkMode.Valve1, txtSyringeAPSet2, txtValveAPSet2)
                SetRecipePartName(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, cmbValve1, cmbValvePaste1, cmbValveFlowRate1)
                SetRecipePartName(RecipeEdit, enmStage.No2, eValveWorkMode.Valve1, cmbValve2, cmbValvePaste2, cmbValveFlowRate2)

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        SetRecipeManualOFfset(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtGlueOffsetX1, txtGlueOffsetY1, txtGlueOffsetZ1)
                        SetRecipeAP(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtSyringeAPSet1, txtValveAPSet1)
                        SetRecipePartName(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, cmbValve1, cmbValvePaste1, cmbValveFlowRate1)

                    Case eMechanismModule.TwoValveOneStage
                        SetRecipeManualOFfset(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtGlueOffsetX1, txtGlueOffsetY1, txtGlueOffsetZ1)
                        SetRecipeManualOFfset(RecipeEdit, enmStage.No1, eValveWorkMode.Valve2, txtGlueOffsetX2, txtGlueOffsetY2, txtGlueOffsetZ2)
                        SetRecipeAP(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, txtSyringeAPSet1, txtValveAPSet1)
                        SetRecipeAP(RecipeEdit, enmStage.No1, eValveWorkMode.Valve2, txtSyringeAPSet2, txtValveAPSet2)
                        SetRecipePartName(RecipeEdit, enmStage.No1, eValveWorkMode.Valve1, cmbValve1, cmbValvePaste1, cmbValveFlowRate1)
                        SetRecipePartName(RecipeEdit, enmStage.No1, eValveWorkMode.Valve2, cmbValve2, cmbValvePaste2, cmbValveFlowRate2)

                End Select

        End Select

        If cmbGPSetRunMode.Items.Count = 3 Then
            Select Case RecipeEdit.GPSetRunMode
                Case eRunMode.BufferMode
                    cmbGPSetRunMode.SelectedIndex = 0
                Case eRunMode.BlendingMode
                    cmbGPSetRunMode.SelectedIndex = 1
                Case eRunMode.FlyMode
                    cmbGPSetRunMode.SelectedIndex = 2
            End Select
        End If

        cmbLaserMode.Items.Clear()
        cmbLaserMode.Items.Add("AverageHigh")
        cmbLaserMode.Items.Add("MaxHigh")
        Select Case RecipeEdit.LaserMode
            Case enmLaserMode.AverageHigh
                cmbLaserMode.SelectedIndex = 0
            Case enmLaserMode.MaxHigh
                cmbLaserMode.SelectedIndex = 1
        End Select


        cmbFollowMode.Items.Clear()
        cmbFollowMode.Items.Add("Restart")
        cmbFollowMode.Items.Add("NextStep")
        cmbFollowMode.Items.Add("NextRound")
        cmbFollowMode.Items.Add("NextPattern")
        Select Case RecipeEdit.FollowMode
            Case enmFollowMode.Restart
                cmbFollowMode.SelectedIndex = 0
            Case enmFollowMode.NextStep
                cmbFollowMode.SelectedIndex = 1
            Case enmFollowMode.NextRound
                cmbFollowMode.SelectedIndex = 2
            Case enmFollowMode.NextPattern
                cmbFollowMode.SelectedIndex = 3
        End Select

        cmbLaserRunMode.Items.Clear()
        cmbLaserRunMode.Items.Add("Array")
        cmbLaserRunMode.Items.Add("NonArray")
        Select Case RecipeEdit.LaserRunMode
            Case eLaserRunModel.Array
                cmbLaserRunMode.SelectedIndex = 0
            Case eLaserRunModel.NonArray
                cmbLaserRunMode.SelectedIndex = 1
        End Select


        '20170214
        '[Note]:利用系統設定檔的設定來擋
        cmbMeasureZMode.Items.Clear()
        Select Case gSSystemParameter.MeasureType
            Case enmMeasureType.Contact
                cmbMeasureZMode.Items.Add("Contact")

            Case enmMeasureType.Laser
                cmbMeasureZMode.Items.Add("Laser(Non-onFly)")
                cmbMeasureZMode.Items.Add("Laser(OnFly)")

            Case enmMeasureType.Both
                cmbMeasureZMode.Items.Add("Contact")
                cmbMeasureZMode.Items.Add("Laser(Non-onFly)")
                cmbMeasureZMode.Items.Add("Laser(OnFly)")

        End Select


        Select Case RecipeEdit.LaserFixMode
            Case eHeightModel.Contact
                If cmbMeasureZMode.Items.Contains("Contact") Then
                    cmbMeasureZMode.SelectedItem = "Contact"
                Else
                    '請選擇測高模式
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000050))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000050), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End If

            Case eHeightModel.Laser_NonOnFly
                If cmbMeasureZMode.Items.Contains("Laser(Non-onFly)") Then
                    cmbMeasureZMode.SelectedItem = "Laser(Non-onFly)"
                Else
                    '請選擇測高模式
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000050))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000050), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End If

            Case eHeightModel.Laser_OnFly
                If cmbMeasureZMode.Items.Contains("Laser(OnFly)") Then
                    cmbMeasureZMode.SelectedItem = "Laser(OnFly)"
                Else
                    '請選擇測高模式
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000050))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000050), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End If

        End Select

    End Sub

#End Region

#Region "PasteDB"


    Public Sub SetPasteDB(ByRef pasteParam As CPasteParameter, ByRef chkPasteExpireTime As CheckBox, ByRef txtPasteExpiredTime As TextBox, ByRef chkPasteExpiredCount As CheckBox, ByRef txtPasteExpiredCount As TextBox, ByRef chkPasteMaxTemp As CheckBox, ByRef txtPasteMaxTemp As TextBox, ByRef chkPasteMinTemp As CheckBox, ByRef txtPasteMinTemp As TextBox, ByRef txtPasteViscocity As TextBox)
        With pasteParam
            chkPasteExpireTime.Checked = .PotLifeEnable
            txtPasteExpiredTime.Text = .PotLife
            chkPasteExpiredCount.Checked = .PotLifeCountEnable
            txtPasteExpiredCount.Text = .PotLifeCount
            chkPasteMaxTemp.Checked = .TempMaxEnable
            txtPasteMaxTemp.Text = .TempMax
            chkPasteMinTemp.Checked = .TempMinEnable
            txtPasteMinTemp.Text = .TempMin
            txtPasteViscocity.Text = .Viscocity

        End With
    End Sub

    '    Function GetJetValveDB(ByRef jetValveParam As CJetValveParameter) As Object
    Function GetPasteDB(ByRef pasteParam As CPasteParameter, ByRef chkPasteExpireTime As CheckBox, ByRef txtPasteExpiredTime As TextBox, ByRef chkPasteExpiredCount As CheckBox, ByRef txtPasteExpiredCount As TextBox, ByRef chkPasteMaxTemp As CheckBox, ByRef txtPasteMaxTemp As TextBox, ByRef chkPasteMinTemp As CheckBox, ByRef txtPasteMinTemp As TextBox, ByRef txtPasteViscocity As TextBox) As Object

        '[說明]:判斷是否為整數
        If (IsNumeric(txtPasteExpiredTime.Text) AndAlso txtPasteExpiredTime.Text > 0) And
           (IsNumeric(txtPasteExpiredCount.Text) AndAlso txtPasteExpiredCount.Text > 0) And
           (IsNumeric(txtPasteMaxTemp.Text) AndAlso txtPasteMaxTemp.Text > 0) And
           (IsNumeric(txtPasteMinTemp.Text) AndAlso txtPasteMinTemp.Text > 0) And
           (IsNumeric(txtPasteViscocity.Text) AndAlso txtPasteViscocity.Text > 0) Then

            With pasteParam
                .PotLifeEnable = chkPasteExpireTime.Checked
                .PotLife = Val(txtPasteExpiredTime.Text)
                .PotLifeCountEnable = chkPasteExpiredCount.Checked
                .PotLifeCount = Val(txtPasteExpiredCount.Text)
                .TempMaxEnable = chkPasteMaxTemp.Checked
                .TempMax = Val(txtPasteMaxTemp.Text)
                .TempMinEnable = chkPasteMinTemp.Checked
                .TempMin = Val(txtPasteMinTemp.Text)
                .Viscocity = Val(txtPasteViscocity.Text)

            End With
            Return True
        Else
            '資料格式錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000059))
            MsgBox(gMsgHandler.GetMessage(Warn_3000059), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("請輸入正整數 !!")
            Return False
        End If
    End Function



    Public Sub PasteDB_Update(ByVal dataBaseName As String)
        lstPaste.Items.Clear()
        cmbValvePaste1.Items.Clear()
        cmbValvePaste2.Items.Clear()
        cmbValvePaste3.Items.Clear()
        cmbValvePaste4.Items.Clear()

        For mI As Integer = 0 To gPasteDataBase.Count - 1
            lstPaste.Items.Add(gPasteDataBase.Keys(mI))
            cmbValvePaste1.Items.Add(gPasteDataBase.Keys(mI))
            cmbValvePaste2.Items.Add(gPasteDataBase.Keys(mI))
            cmbValvePaste3.Items.Add(gPasteDataBase.Keys(mI))
            cmbValvePaste4.Items.Add(gPasteDataBase.Keys(mI))
            '[說明]顯示No1的資料
            If gPasteDataBase.Keys(mI) = dataBaseName Then
                SetPasteDB(gPasteDataBase(dataBaseName), chkPasteExpireTime, txtPasteExpiredTime, chkPasteExpiredCount, txtPasteExpiredCount, chkPasteMaxTemp, txtPasteMaxTemp, chkPasteMinTemp, txtPasteMinTemp, txtPasteViscocity)
                txtPasteName.Text = dataBaseName
                lstPaste.SelectedIndex = mI
            End If
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    If gPasteDataBase.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).PasteName(eValveWorkMode.Valve1) Then
                        cmbValvePaste1.SelectedIndex = mI
                    End If
                    If gPasteDataBase.Keys(mI) = RecipeEdit.StageParts(enmStage.No2).PasteName(eValveWorkMode.Valve1) Then
                        cmbValvePaste2.SelectedIndex = mI
                    End If
                    If gPasteDataBase.Keys(mI) = RecipeEdit.StageParts(enmStage.No3).PasteName(eValveWorkMode.Valve1) Then
                        cmbValvePaste3.SelectedIndex = mI
                    End If
                    If gPasteDataBase.Keys(mI) = RecipeEdit.StageParts(enmStage.No4).PasteName(eValveWorkMode.Valve1) Then
                        cmbValvePaste4.SelectedIndex = mI
                    End If

                Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                    If gPasteDataBase.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).PasteName(eValveWorkMode.Valve1) Then
                        cmbValvePaste1.SelectedIndex = mI
                    End If
                    If gPasteDataBase.Keys(mI) = RecipeEdit.StageParts(enmStage.No2).PasteName(eValveWorkMode.Valve1) Then
                        cmbValvePaste2.SelectedIndex = mI
                    End If

                Case Else
                    Select Case gSSystemParameter.StageUseValveCount
                        Case eMechanismModule.OneValveOneStage
                            If gPasteDataBase.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).PasteName(eValveWorkMode.Valve1) Then
                                cmbValvePaste1.SelectedIndex = mI
                            End If

                        Case eMechanismModule.TwoValveOneStage
                            If gPasteDataBase.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).PasteName(eValveWorkMode.Valve1) Then
                                cmbValvePaste1.SelectedIndex = mI
                            End If
                            If gPasteDataBase.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).PasteName(eValveWorkMode.Valve2) Then
                                cmbValvePaste2.SelectedIndex = mI
                            End If

                    End Select

            End Select
        Next

        If cmbValve1.SelectedIndex = -1 Then
            If cmbValve1.Items.Count > 0 Then
                cmbValve1.SelectedIndex = 0
            End If
        End If
        If cmbValve2.SelectedIndex = -1 Then
            If cmbValve2.Items.Count > 0 Then
                cmbValve2.SelectedIndex = 0
            End If
        End If
        If cmbValve3.SelectedIndex = -1 Then
            If cmbValve3.Items.Count > 0 Then
                cmbValve3.SelectedIndex = 0
            End If
        End If
        If cmbValve4.SelectedIndex = -1 Then
            If cmbValve4.Items.Count > 0 Then
                cmbValve4.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub lstPaste_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstPaste.SelectedIndexChanged
        If lstPaste.SelectedIndex < 0 Then
            lstPaste.BackColor = Color.Yellow
            lstPaste.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstPaste.BackColor = Color.White
            Exit Sub
        End If
        lstPasteSelectedItem = lstPaste.SelectedItem
        txtPasteName.Text = lstPaste.SelectedItem
        SetPasteDB(gPasteDataBase(lstPaste.SelectedItem), chkPasteExpireTime, txtPasteExpiredTime, chkPasteExpiredCount, txtPasteExpiredCount, chkPasteMaxTemp, txtPasteMaxTemp, chkPasteMinTemp, txtPasteMinTemp, txtPasteViscocity)
    End Sub

    Private Sub btnPasteDBUpdate_Click(sender As Object, e As EventArgs) Handles btnPasteDBUpdate.Click
        If lstPaste.SelectedIndex < 0 Then
            lstPaste.BackColor = Color.Yellow
            lstPaste.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstPaste.BackColor = Color.White
            lstPaste.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If

        If GetPasteDB(gPasteDataBase(lstPaste.SelectedItem), chkPasteExpireTime, txtPasteExpiredTime, chkPasteExpiredCount, txtPasteExpiredCount, chkPasteMaxTemp, txtPasteMaxTemp, chkPasteMinTemp, txtPasteMinTemp, txtPasteViscocity) Then
            Dim folderPath As String = Application.StartupPath & "\Database\Paste\" '膠材資料
            Dim fileName As String = folderPath & gPasteDataBase(lstPaste.SelectedItem).Name & ".pst"

            gPasteDataBase(lstPaste.SelectedItem).Save(fileName)
            txtPasteExpiredTime_TextChanged(sender, e)
        End If

    End Sub

    Private Sub btnPasteDBAdd_Click(sender As Object, e As EventArgs) Handles btnPasteDBAdd.Click

        If txtPasteName.Text.Trim = "" Then
            txtPasteName.BackColor = Color.Yellow
            txtPasteName.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            txtPasteName.BackColor = Color.White
            txtPasteName.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If
        Dim newPaste As New CPasteParameter(txtPasteName.Text) 'Soni / 2017.04.26 延後產生
        newPaste.Name = txtPasteName.Text
        If IsillegalFileName(newPaste.Name) = True Then
            '檔案名稱錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000058))
            MsgBox(gMsgHandler.GetMessage(Warn_3000058), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("File Name Error")
            Return
        Else
            If gPasteDataBase.ContainsKey(newPaste.Name) Then
                If MsgBox("Paste Data already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                    lstPaste.SelectedItem = newPaste.Name
                    GetPasteDB(gPasteDataBase(lstPaste.SelectedItem), chkPasteExpireTime, txtPasteExpiredTime, chkPasteExpiredCount, txtPasteExpiredCount, chkPasteMaxTemp, txtPasteMaxTemp, chkPasteMinTemp, txtPasteMinTemp, txtPasteViscocity)
                End If
                Exit Sub
            End If

            If GetPasteDB(newPaste, chkPasteExpireTime, txtPasteExpiredTime, chkPasteExpiredCount, txtPasteExpiredCount, chkPasteMaxTemp, txtPasteMaxTemp, chkPasteMinTemp, txtPasteMinTemp, txtPasteViscocity) Then
                gPasteDataBase.Add(newPaste.Name, newPaste)

                Dim folderPath As String = Application.StartupPath & "\Database\Paste\" '膠材資料
                Dim fileName As String = folderPath & newPaste.Name & ".pst"

                If System.IO.File.Exists(fileName) Then
                    If MsgBox("Paste File already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                        newPaste.Save(fileName)
                    End If
                Else
                    newPaste.Save(fileName)
                End If
                PasteDB_Update(newPaste.Name)
            End If
        End If

    End Sub

    Private Sub btnPasteDBDelete_Click(sender As Object, e As EventArgs) Handles btnPasteDBDelete.Click
        If lstPaste.SelectedIndex < 0 Then
            lstPaste.BackColor = Color.Yellow
            lstPaste.Refresh()
            System.Threading.Thread.Sleep(300)
            lstPaste.BackColor = Color.White
            lstPaste.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If

        If MsgBox("Are You Sure to Delete?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then 'Soni + 2017.04.26 刪除前確認
            Exit Sub
        End If

        Dim folderPath As String = Application.StartupPath & "\Database\Paste\" '膠材資料
        Dim fileName As String = folderPath & lstPaste.SelectedItem & ".pst"

        'jimmy 20170704
        Dim bDBDelectCheck As Boolean = False
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If RecipeEdit.StageParts(enmStage.No1).PasteName(eValveWorkMode.Valve1) = lstPaste.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No2).PasteName(eValveWorkMode.Valve1) = lstPaste.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No3).PasteName(eValveWorkMode.Valve1) = lstPaste.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No4).PasteName(eValveWorkMode.Valve1) = lstPaste.SelectedItem Then
                    bDBDelectCheck = False
                Else
                    bDBDelectCheck = True
                End If
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If RecipeEdit.StageParts(enmStage.No1).PasteName(eValveWorkMode.Valve1) = lstPaste.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No2).PasteName(eValveWorkMode.Valve1) = lstPaste.SelectedItem Then

                    bDBDelectCheck = False
                Else
                    bDBDelectCheck = True
                End If
            Case Else
                Select Case gSSystemParameter.StageUseValveCount

                    Case eMechanismModule.OneValveOneStage
                        If RecipeEdit.StageParts(enmStage.No1).PasteName(eValveWorkMode.Valve1) = lstPaste.SelectedItem Then
                            bDBDelectCheck = False
                        Else
                            bDBDelectCheck = True
                        End If
                    Case eMechanismModule.TwoValveOneStage
                        If RecipeEdit.StageParts(enmStage.No1).PasteName(eValveWorkMode.Valve1) = lstPaste.SelectedItem Or
                            RecipeEdit.StageParts(enmStage.No1).PasteName(eValveWorkMode.Valve2) = lstPaste.SelectedItem Then
                            bDBDelectCheck = False
                        Else
                            bDBDelectCheck = True
                        End If
                End Select

        End Select

        If bDBDelectCheck = True Then
            If System.IO.File.Exists(fileName) Then
                System.IO.File.Delete(fileName)
            End If
            gPasteDataBase.Remove(lstPaste.SelectedItem)
            PasteDB_Update(RecipeEdit.StageParts(enmStage.No1).PasteName(eValveWorkMode.Valve1))

            If Not gPasteDataBase.ContainsKey("Default") Then 'Soni + 2017.04.26 預設檔重建
                gPasteDataBase.Add("Default", New CPasteParameter("Default"))
                gPasteDataBase("Default").Save(folderPath & "Default.pst")
                PasteDB_Update(RecipeEdit.StageParts(enmStage.No1).PasteName(eValveWorkMode.Valve1))
            End If
        Else
            MessageBox.Show("Operation Could Not be Completed")
        End If

    End Sub

    Dim lstPasteSelectedItem As String = ""
    Private Sub txtPasteExpiredTime_TextChanged(sender As Object, e As EventArgs) Handles txtPasteExpiredTime.TextChanged, chkPasteExpireTime.CheckedChanged, txtPasteExpiredCount.TextChanged, chkPasteExpiredCount.CheckedChanged, txtPasteMaxTemp.TextChanged, chkPasteMaxTemp.CheckedChanged, txtPasteMinTemp.TextChanged, chkPasteMinTemp.CheckedChanged, txtPasteViscocity.TextChanged

        If lstPasteSelectedItem = "" Then
            Exit Sub
        End If

        If gPasteDataBase.ContainsKey(lstPasteSelectedItem) = True Then
            Dim ChangeColor As Color = Color.Red
            With gPasteDataBase(lstPasteSelectedItem)
                If .PotLifeEnable = chkPasteExpireTime.Checked Then
                    chkPasteExpireTime.BackColor = Color.White
                Else
                    chkPasteExpireTime.BackColor = ChangeColor
                End If
                If .PotLife = Val(txtPasteExpiredTime.Text) Then
                    txtPasteExpiredTime.BackColor = Color.White
                Else
                    txtPasteExpiredTime.BackColor = ChangeColor
                End If
                If .PotLifeCountEnable = chkPasteExpiredCount.Checked Then
                    chkPasteExpiredCount.BackColor = Color.White
                Else
                    chkPasteExpiredCount.BackColor = ChangeColor
                End If
                If .PotLifeCount = Val(txtPasteExpiredCount.Text) Then
                    txtPasteExpiredCount.BackColor = Color.White
                Else
                    txtPasteExpiredCount.BackColor = ChangeColor
                End If
                If .TempMaxEnable = chkPasteMaxTemp.Checked Then
                    chkPasteMaxTemp.BackColor = Color.White
                Else
                    chkPasteMaxTemp.BackColor = ChangeColor
                End If
                If .TempMax = Val(txtPasteMaxTemp.Text) Then
                    txtPasteMaxTemp.BackColor = Color.White
                Else
                    txtPasteMaxTemp.BackColor = ChangeColor
                End If
                If .TempMinEnable = chkPasteMinTemp.Checked Then
                    chkPasteMinTemp.BackColor = Color.White
                Else
                    chkPasteMinTemp.BackColor = ChangeColor
                End If
                If .TempMin = Val(txtPasteMinTemp.Text) Then
                    txtPasteMinTemp.BackColor = Color.White
                Else
                    txtPasteMinTemp.BackColor = ChangeColor
                End If
                If .Viscocity = Val(txtPasteViscocity.Text) Then
                    txtPasteViscocity.BackColor = Color.White
                Else
                    txtPasteViscocity.BackColor = ChangeColor
                End If

            End With
        End If



    End Sub
#End Region

#Region "JetValveDB"
    Public Sub SetJetValveDB(ByRef jetValveParam As CJetValveParameter)
        '20161102
        Select Case cmbValveChooes.SelectedItem
            Case "PicoPulse"
                With jetValveParam
                    txtPicoCycleTime.Text = CStr(.PicoTouch.CycleTime)
                    txtPicoFrequency.Text = .PicoTouch.Frequence.ToString("0.00")    '20160805
                    Premtek.ControlMisc.SetNumericValue(nmuPicoOpenTime, .PicoTouch.OpenTime)
                    Premtek.ControlMisc.SetNumericValue(nmuPicoValveOnTime, .PicoTouch.ValveOnTime)
                    Premtek.ControlMisc.SetNumericValue(nmuPicoCloseTime, .PicoTouch.CloseTime)
                    Premtek.ControlMisc.SetNumericValue(nmuPicoValveOffTime, .PicoTouch.ValveOffTime)
                    Premtek.ControlMisc.SetNumericValue(nmuPicoStroke, .PicoTouch.Stroke)
                    Premtek.ControlMisc.SetNumericValue(nmuPicoJetTime, .PicoTouch.JetTime)
                    Premtek.ControlMisc.SetNumericValue(nmuPicoCloseVoltage, .PicoTouch.CloseVoltage)
                    txtValveAPSet1.Text = 0
                End With
            Case "Advanjet"
                With jetValveParam
                    Premtek.ControlMisc.SetNumericValue(nmuValveAdvanjetRefillTime, .Advanjet.RefillTime)
                    Premtek.ControlMisc.SetNumericValue(nmuValveAdvanjetCycleTime, .Advanjet.CycleTime)
                    Premtek.ControlMisc.SetNumericValue(nmuValveAdvanJetTime, .Advanjet.JetTime)
                End With
        End Select
    End Sub
    Function GetJetValveDB(ByRef jetValveParam As CJetValveParameter) As Boolean

        '[說明]:判斷是否為整數
        '    (IsNumeric(txtValveAPSet.Text) AndAlso Val(txtValveAPSet.Text) >= 0) And
        '.ValveAirPressure = Val(txtValveAPSet.Text)
        '20161102
        Select Case cmbValveChooes.SelectedItem
            Case "PicoPulse"
                'Eason 20170125 Ticket:100050 , Add JetTime
                If (IsNumeric(txtPicoCycleTime.Text) AndAlso Val(txtPicoCycleTime.Text) >= 0) And
                    (IsNumeric(nmuPicoCloseVoltage.Value) AndAlso Val(nmuPicoCloseVoltage.Value) >= 0) And
                   (IsNumeric(nmuPicoStroke.Value) AndAlso Val(nmuPicoStroke.Value) >= 0) And
                   (IsNumeric(nmuPicoValveOnTime.Value) AndAlso Val(nmuPicoValveOnTime.Value) >= 0) And
                   (IsNumeric(nmuPicoOpenTime.Value) AndAlso Val(nmuPicoOpenTime.Value) >= 0) And
                   (IsNumeric(nmuPicoJetTime.Value) AndAlso Val(nmuPicoJetTime.Value) > 0) And
                   (IsNumeric(nmuPicoCloseTime.Value) AndAlso Val(nmuPicoCloseTime.Value) >= 0) Then

                    With jetValveParam
                        .PicoTouch.OpenTime = Val(nmuPicoOpenTime.Value)
                        .PicoTouch.ValveOnTime = Val(nmuPicoValveOnTime.Value)
                        .PicoTouch.CloseTime = Val(nmuPicoCloseTime.Value)
                        .PicoTouch.ValveOffTime = Val(nmuPicoValveOffTime.Value)
                        .PicoTouch.Stroke = Val(nmuPicoStroke.Value)
                        .PicoTouch.JetTime = Val(nmuPicoJetTime.Value) 'Eason 20170125 Ticket:100050 , Add JetTime
                        .PicoTouch.CloseVoltage = Val(nmuPicoCloseVoltage.Value)
                        jetValveParam.ValveModel = cmbValveChooes.SelectedIndex
                    End With
                    Return True
                Else
                    '資料格式錯誤
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000059))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000059), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MessageBox.Show("請輸入正整數 !!")
                    Return False
                End If
            Case "Advanjet"
                'Eason 20170125 Ticket:100050 , Add JetTime
                If (IsNumeric(nmuValveAdvanjetRefillTime.Value) AndAlso Val(nmuValveAdvanjetRefillTime.Value) >= 0) And
                    (IsNumeric(nmuValveAdvanJetTime.Value) AndAlso Val(nmuValveAdvanJetTime.Value) > 0) And
                    (IsNumeric(nmuValveAdvanjetCycleTime.Value) AndAlso Val(nmuValveAdvanjetCycleTime.Value) >= 0) Then
                    With jetValveParam
                        .Advanjet.RefillTime = Val(nmuValveAdvanjetRefillTime.Value)
                        .Advanjet.CycleTime = Val(nmuValveAdvanjetCycleTime.Value)
                        .Advanjet.JetTime = Val(nmuValveAdvanJetTime.Value) 'Eason 20170125 Ticket:100050 , Add JetTime
                        jetValveParam.ValveModel = cmbValveChooes.SelectedIndex
                    End With

                    Return True
                Else
                    '資料格式錯誤
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000059))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000059), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MessageBox.Show("請輸入正整數 !!")
                    Return False
                End If
        End Select
        Return True
    End Function

    '20161206
    Public Sub JetValveDB_Update(ByVal dataBaseName As String)

        Dim mSelectIndex As Integer = 0

        '[Note]:預設吃ValveNo1
        Select Case gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1)
            Case eValveModel.PicoPulse
                cmbValveChooes.SelectedIndex = 0

            Case eValveModel.Advanjet
                cmbValveChooes.SelectedIndex = 1

        End Select

        lstJetValve.Items.Clear()
        cmbValve1.Items.Clear()
        cmbValve2.Items.Clear()
        cmbValve3.Items.Clear()
        cmbValve4.Items.Clear()

        '[說明]:寫入cmbValve1對應資料
        For mI As Integer = 0 To gJetValveDB.Count - 1
            If gJetValveDB.ContainsKey(gJetValveDB.Keys(mI)) = True Then
                Select Case gSSystemParameter.MachineType
                    Case enmMachineType.DCSW_800AQ
                        If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) Then
                            cmbValve1.Items.Add(gJetValveDB.Keys(mI))
                        End If
                        If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No2).JetValve(eValveWorkMode.Valve1) Then
                            cmbValve2.Items.Add(gJetValveDB.Keys(mI))
                        End If
                        If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No3).JetValve(eValveWorkMode.Valve1) Then
                            cmbValve3.Items.Add(gJetValveDB.Keys(mI))
                        End If
                        If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No4).JetValve(eValveWorkMode.Valve1) Then
                            cmbValve4.Items.Add(gJetValveDB.Keys(mI))
                        End If

                    Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                        If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) Then
                            cmbValve1.Items.Add(gJetValveDB.Keys(mI))
                        End If
                        If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No2).JetValve(eValveWorkMode.Valve1) Then
                            cmbValve2.Items.Add(gJetValveDB.Keys(mI))
                        End If

                    Case Else
                        Select Case gSSystemParameter.StageUseValveCount
                            Case eMechanismModule.OneValveOneStage
                                If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) Then
                                    cmbValve1.Items.Add(gJetValveDB.Keys(mI))
                                End If

                            Case eMechanismModule.TwoValveOneStage
                                If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) Then
                                    cmbValve1.Items.Add(gJetValveDB.Keys(mI))
                                End If
                                If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve2) Then
                                    cmbValve2.Items.Add(gJetValveDB.Keys(mI))
                                End If

                        End Select

                End Select
            End If
        Next

        ''[Note]:顯示上是顯示StageNo1的ValveNo1
        For mI As Integer = 0 To gJetValveDB.Count - 1
            Select Case cmbValveChooes.SelectedItem
                Case "PicoPulse"
                    If gJetValveDB.ContainsKey(gJetValveDB.Keys(mI)) = True Then
                        If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.PicoPulse Then
                            lstJetValve.Items.Add(gJetValveDB.Keys(mI))
                            If dataBaseName = gJetValveDB.Keys(mI) Then
                                SetJetValveDB(gJetValveDB(dataBaseName))
                                txtJetValveName.Text = dataBaseName
                                cmbValve1.SelectedIndex = mSelectIndex
                                lstJetValve.SelectedIndex = mSelectIndex
                            End If
                            mSelectIndex = mSelectIndex + 1
                        End If
                    End If

                Case "Advanjet"
                    If gJetValveDB.ContainsKey(gJetValveDB.Keys(mI)) = True Then
                        If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.Advanjet Then
                            lstJetValve.Items.Add(gJetValveDB.Keys(mI))
                            If dataBaseName = gJetValveDB.Keys(mI) Then
                                SetJetValveDB(gJetValveDB(dataBaseName))
                                txtJetValveName.Text = dataBaseName
                                cmbValve1.SelectedIndex = mSelectIndex
                                lstJetValve.SelectedIndex = mSelectIndex
                            End If
                            mSelectIndex = mSelectIndex + 1
                        End If
                    End If

            End Select
        Next

        Dim mPicSelectIndex As Integer = 0
        Dim mAdvSelectIndex As Integer = 0

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                '[說明]:選擇閥與設定閥不符則不顯示
                For mI As Integer = 0 To gJetValveDB.Count - 1
                    If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) <> "" Then
                        If gJetValveDB.ContainsKey(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)) Then
                            If gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) Then
                                If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) = gJetValveDB.Keys(mI) Then
                                    If gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.Advanjet Then
                                        'cmbValve1.SelectedIndex = mPicSelectIndex
                                        cmbValve1.SelectedIndex = mAdvSelectIndex
                                    ElseIf gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.PicoPulse Then
                                        'cmbValve1.SelectedIndex = mAdvSelectIndex
                                        cmbValve1.SelectedIndex = mPicSelectIndex
                                    End If
                                End If
                            End If
                        End If

                        If gJetValveDB.ContainsKey(RecipeEdit.StageParts(enmStage.No2).ValveName(eValveWorkMode.Valve1)) Then
                            If gJetValveDB(RecipeEdit.StageParts(enmStage.No2).ValveName(eValveWorkMode.Valve1)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No2).JetValve(eValveWorkMode.Valve1) Then
                                If RecipeEdit.StageParts(enmStage.No2).ValveName(eValveWorkMode.Valve1) = gJetValveDB.Keys(mI) Then
                                    If gJetValveDB(RecipeEdit.StageParts(enmStage.No2).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.Advanjet Then
                                        'cmbValve2.SelectedIndex = mPicSelectIndex
                                        cmbValve2.SelectedIndex = mAdvSelectIndex
                                    ElseIf gJetValveDB(RecipeEdit.StageParts(enmStage.No2).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.PicoPulse Then
                                        'cmbValve2.SelectedIndex = mAdvSelectIndex
                                        cmbValve2.SelectedIndex = mPicSelectIndex
                                    End If
                                End If
                            End If
                        End If

                        If gJetValveDB.ContainsKey(RecipeEdit.StageParts(enmStage.No3).ValveName(eValveWorkMode.Valve1)) Then
                            If gJetValveDB(RecipeEdit.StageParts(enmStage.No3).ValveName(eValveWorkMode.Valve1)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No3).JetValve(eValveWorkMode.Valve1) Then
                                If RecipeEdit.StageParts(enmStage.No3).ValveName(eValveWorkMode.Valve1) = gJetValveDB.Keys(mI) Then
                                    If gJetValveDB(RecipeEdit.StageParts(enmStage.No3).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.Advanjet Then
                                        'cmbValve3.SelectedIndex = mPicSelectIndex
                                        cmbValve3.SelectedIndex = mAdvSelectIndex
                                    ElseIf gJetValveDB(RecipeEdit.StageParts(enmStage.No3).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.PicoPulse Then
                                        'cmbValve3.SelectedIndex = mAdvSelectIndex
                                        cmbValve3.SelectedIndex = mPicSelectIndex
                                    End If
                                End If
                            End If
                        End If

                        If gJetValveDB.ContainsKey(RecipeEdit.StageParts(enmStage.No4).ValveName(eValveWorkMode.Valve1)) Then
                            If gJetValveDB(RecipeEdit.StageParts(enmStage.No4).ValveName(eValveWorkMode.Valve1)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No4).JetValve(eValveWorkMode.Valve1) Then
                                If RecipeEdit.StageParts(enmStage.No4).ValveName(eValveWorkMode.Valve1) = gJetValveDB.Keys(mI) Then
                                    If gJetValveDB(RecipeEdit.StageParts(enmStage.No4).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.Advanjet Then
                                        'cmbValve4.SelectedIndex = mPicSelectIndex
                                        cmbValve4.SelectedIndex = mAdvSelectIndex
                                    ElseIf gJetValveDB(RecipeEdit.StageParts(enmStage.No4).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.PicoPulse Then
                                        'cmbValve4.SelectedIndex = mAdvSelectIndex
                                        cmbValve4.SelectedIndex = mPicSelectIndex
                                    End If
                                End If
                            End If
                        End If

                        If gJetValveDB.ContainsKey(gJetValveDB.Keys(mI)) = True Then
                            If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.PicoPulse Then
                                mPicSelectIndex = mPicSelectIndex + 1
                            End If
                            If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.Advanjet Then
                                mAdvSelectIndex = mAdvSelectIndex + 1
                            End If
                        End If
                    End If
                Next

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                '[說明]:選擇閥與設定閥不符則不顯示
                For mI As Integer = 0 To gJetValveDB.Count - 1
                    If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) <> "" Then
                        If gJetValveDB.ContainsKey(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)) Then
                            If gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) Then
                                If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) = gJetValveDB.Keys(mI) Then
                                    If gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.Advanjet Then
                                        ' cmbValve1.SelectedIndex = mPicSelectIndex
                                        cmbValve1.SelectedIndex = mAdvSelectIndex
                                    ElseIf gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.PicoPulse Then
                                        'cmbValve1.SelectedIndex = mAdvSelectIndex
                                        cmbValve1.SelectedIndex = mPicSelectIndex
                                    End If
                                End If
                            End If
                        End If

                        If gJetValveDB.ContainsKey(RecipeEdit.StageParts(enmStage.No2).ValveName(eValveWorkMode.Valve1)) Then
                            If gJetValveDB(RecipeEdit.StageParts(enmStage.No2).ValveName(eValveWorkMode.Valve1)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No2).JetValve(eValveWorkMode.Valve1) Then
                                If RecipeEdit.StageParts(enmStage.No2).ValveName(eValveWorkMode.Valve1) = gJetValveDB.Keys(mI) Then
                                    If gJetValveDB(RecipeEdit.StageParts(enmStage.No2).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.Advanjet Then
                                        ' cmbValve2.SelectedIndex = mPicSelectIndex
                                        cmbValve2.SelectedIndex = mAdvSelectIndex
                                    ElseIf gJetValveDB(RecipeEdit.StageParts(enmStage.No2).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.PicoPulse Then
                                        ' cmbValve2.SelectedIndex = mAdvSelectIndex
                                        cmbValve2.SelectedIndex = mPicSelectIndex
                                    End If
                                End If
                            End If
                        End If

                        If gJetValveDB.ContainsKey(gJetValveDB.Keys(mI)) = True Then
                            If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.PicoPulse Then
                                mPicSelectIndex = mPicSelectIndex + 1
                            End If
                            If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.Advanjet Then
                                mAdvSelectIndex = mAdvSelectIndex + 1
                            End If
                        End If
                    End If
                Next

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        '[說明]:選擇閥與設定閥不符則不顯示
                        For mI As Integer = 0 To gJetValveDB.Count - 1
                            If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) <> "" Then
                                If gJetValveDB.ContainsKey(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)) Then
                                    If gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) Then
                                        If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) = gJetValveDB.Keys(mI) Then
                                            If gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.Advanjet Then
                                                ' cmbValve1.SelectedIndex = mPicSelectIndex
                                                cmbValve1.SelectedIndex = mAdvSelectIndex
                                            ElseIf gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.PicoPulse Then
                                                'cmbValve1.SelectedIndex = mAdvSelectIndex
                                                cmbValve1.SelectedIndex = mPicSelectIndex
                                            End If
                                        End If
                                    End If
                                End If

                                If gJetValveDB.ContainsKey(gJetValveDB.Keys(mI)) = True Then
                                    If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.PicoPulse Then
                                        mPicSelectIndex = mPicSelectIndex + 1
                                    End If
                                    If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.Advanjet Then
                                        mAdvSelectIndex = mAdvSelectIndex + 1
                                    End If
                                End If
                            End If
                        Next

                    Case eMechanismModule.TwoValveOneStage
                        '[說明]:選擇閥與設定閥不符則不顯示
                        For mI As Integer = 0 To gJetValveDB.Count - 1
                            If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) <> "" Then
                                If gJetValveDB.ContainsKey(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)) Then
                                    If gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) Then
                                        If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) = gJetValveDB.Keys(mI) Then
                                            If gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.Advanjet Then
                                                ' cmbValve1.SelectedIndex = mPicSelectIndex
                                                cmbValve1.SelectedIndex = mAdvSelectIndex
                                            ElseIf gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)).ValveModel = eValveModel.PicoPulse Then
                                                'cmbValve1.SelectedIndex = mAdvSelectIndex
                                                cmbValve1.SelectedIndex = mPicSelectIndex
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve2) <> "" Then
                                If gJetValveDB.ContainsKey(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve2)) Then
                                    If gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve2)).ValveModel = gSSystemParameter.StageParts.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve2) Then
                                        If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve2) = gJetValveDB.Keys(mI) Then
                                            If gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve2)).ValveModel = eValveModel.Advanjet Then
                                                ' cmbValve2.SelectedIndex = mPicSelectIndex
                                                cmbValve2.SelectedIndex = mAdvSelectIndex
                                            ElseIf gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve2)).ValveModel = eValveModel.PicoPulse Then
                                                ' cmbValve2.SelectedIndex = mAdvSelectIndex
                                                cmbValve2.SelectedIndex = mPicSelectIndex
                                            End If
                                        End If
                                    End If
                                End If

                                If gJetValveDB.ContainsKey(gJetValveDB.Keys(mI)) = True Then
                                    If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.PicoPulse Then
                                        mPicSelectIndex = mPicSelectIndex + 1
                                    End If
                                    If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.Advanjet Then
                                        mAdvSelectIndex = mAdvSelectIndex + 1
                                    End If
                                End If
                            End If
                        Next

                End Select
        End Select

        If cmbValve1.SelectedIndex = -1 Then
            If cmbValve1.Items.Count > 0 Then
                cmbValve1.SelectedIndex = -1
            End If
        End If
        If cmbValve2.SelectedIndex = -1 Then
            If cmbValve2.Items.Count > 0 Then
                cmbValve2.SelectedIndex = -1
            End If
        End If
        If cmbValve3.SelectedIndex = -1 Then
            If cmbValve3.Items.Count > 0 Then
                cmbValve3.SelectedIndex = -1
            End If
        End If
        If cmbValve4.SelectedIndex = -1 Then
            If cmbValve4.Items.Count > 0 Then
                cmbValve4.SelectedIndex = -1
            End If
        End If
    End Sub


    Private Sub txtJetValveFrequency_TextChanged(sender As Object, e As EventArgs)
        Dim ChangeColor As Color = Color.Red
        If JetValveSelectedItem = "" Then
            Exit Sub
        End If
        '[Note]:沒有此DateBase
        If gJetValveDB.ContainsKey(JetValveSelectedItem) = False Then
            Exit Sub
        End If

        With gJetValveDB(JetValveSelectedItem)
            If .PicoTouch.OpenTime = Val(nmuPicoOpenTime.Value) Then
                nmuPicoOpenTime.BackColor = Color.White
            Else
                nmuPicoOpenTime.BackColor = ChangeColor
            End If
            If .PicoTouch.ValveOnTime = Val(nmuPicoValveOnTime.Value) Then
                nmuPicoValveOnTime.BackColor = Color.White
            Else
                nmuPicoValveOnTime.BackColor = ChangeColor
            End If
            If .PicoTouch.CloseTime = Val(nmuPicoCloseTime.Value) Then
                nmuPicoCloseTime.BackColor = Color.White
            Else
                nmuPicoCloseTime.BackColor = ChangeColor
            End If
            If .PicoTouch.ValveOffTime = Val(nmuPicoValveOffTime.Value) Then
                nmuPicoValveOffTime.BackColor = Color.White
            Else
                nmuPicoValveOffTime.BackColor = ChangeColor
            End If

            '********************************************************************
            txtPicoCycleTime.Text = .PicoTouch.CycleTime
            txtPicoFrequency.Text = .PicoTouch.Frequence
            '********************************************************************


            If .PicoTouch.Stroke = Val(nmuPicoStroke.Value) Then
                nmuPicoStroke.BackColor = Color.White
            Else
                nmuPicoStroke.BackColor = ChangeColor
            End If

            'Eason 20170125 Ticket:100050 , Add JetTime
            If .PicoTouch.JetTime = Val(nmuPicoJetTime.Value) Then
                nmuPicoJetTime.BackColor = Color.White
            Else
                nmuPicoJetTime.BackColor = ChangeColor
            End If

        End With
    End Sub

    ''' <summary>選擇的噴射閥名稱</summary>
    ''' <remarks></remarks>
    Dim JetValveSelectedItem As String = ""
    Private Sub lstJetValve_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstJetValve.SelectedIndexChanged
        If lstJetValve.SelectedIndex < 0 Then
            lstJetValve.BackColor = Color.Yellow
            lstJetValve.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstJetValve.BackColor = Color.White
            Exit Sub
        End If
        JetValveSelectedItem = lstJetValve.SelectedItem
        txtJetValveName.Text = lstJetValve.SelectedItem
        SetJetValveDB(gJetValveDB(lstJetValve.SelectedItem))
    End Sub

    Private Sub btnValveDBUpdate_Click(sender As Object, e As EventArgs) Handles btnValveDBUpdate.Click
        If lstJetValve.SelectedIndex < 0 Then
            lstJetValve.BackColor = Color.Yellow
            lstJetValve.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstJetValve.BackColor = Color.White
            Exit Sub
        End If

        '[說明]:計算實際Counter數
        Dim ValveCounter As Integer = 0
        For i As Integer = 0 To gJetValveDB.Count - 1
            Select Case cmbValveChooes.SelectedItem
                Case "PicoPulse"
                Case "Advanjet"
            End Select
        Next

        If gJetValveDB.ContainsKey(lstJetValve.SelectedItem) = True Then
            If GetJetValveDB(gJetValveDB(lstJetValve.SelectedItem)) Then
                '20161102
                Dim folderPath As String = "" '= Application.StartupPath & "\Database\JetValve\" '噴射閥資料
                Select Case cmbValveChooes.SelectedItem
                    Case "PicoPulse"
                        folderPath = Application.StartupPath & "\Database\JetValve\PicoTouch\"
                    Case "Advanjet"
                        folderPath = Application.StartupPath & "\Database\JetValve\Advanjet\"
                End Select

                Dim fileName As String = folderPath & gJetValveDB(lstJetValve.SelectedItem).Name & ".pst"
                gJetValveDB(lstJetValve.SelectedItem).Save(fileName)

                txtJetValveFrequency_TextChanged(sender, e)
            End If
        End If

    End Sub

    Private Sub btnValveDBAdd_Click(sender As Object, e As EventArgs) Handles btnValveDBAdd.Click

        If txtJetValveName.Text.Trim = "" Then
            txtJetValveName.BackColor = Color.Yellow
            txtJetValveName.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            txtJetValveName.BackColor = Color.White
            txtJetValveName.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If
        Dim newJetValve As New CJetValveParameter(txtJetValveName.Text)
        newJetValve.Name = txtJetValveName.Text
        If IsillegalFileName(newJetValve.Name) = True Then
            '檔案名稱錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000058))
            MsgBox(gMsgHandler.GetMessage(Warn_3000058), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("File Name Error")
            Return
        Else
            If gJetValveDB.ContainsKey(newJetValve.Name) Then
                If MsgBox("Jet Valve Data already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                    lstJetValve.SelectedItem = newJetValve.Name
                    GetJetValveDB(gJetValveDB(lstJetValve.SelectedItem))
                End If
                Exit Sub
            End If

            If GetJetValveDB(newJetValve) Then
                gJetValveDB.Add(newJetValve.Name, newJetValve)

                '20161102
                Dim folderPath As String = "" ' = Application.StartupPath & "\Database\JetValve\" '噴射閥資料
                Select Case cmbValveChooes.SelectedItem
                    Case "PicoPulse"
                        folderPath = Application.StartupPath & "\Database\JetValve\PicoTouch\"
                    Case "Advanjet"
                        folderPath = Application.StartupPath & "\Database\JetValve\Advanjet\"
                End Select

                Dim fileName As String = folderPath & newJetValve.Name & ".pst"

                If System.IO.File.Exists(fileName) Then
                    If MsgBox("Jet Valve File already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                        newJetValve.Save(fileName)
                    End If
                Else
                    newJetValve.Save(fileName)
                End If
                JetValveDB_Update(newJetValve.Name)
            End If
        End If

    End Sub

    Private Sub btnValveDBDelete_Click(sender As Object, e As EventArgs) Handles btnValveDBDelete.Click
        If lstJetValve.SelectedIndex < 0 Then
            lstJetValve.BackColor = Color.Yellow
            lstJetValve.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstJetValve.BackColor = Color.White
            lstJetValve.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If

        If MsgBox("Are You Sure to Delete?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then 'Soni + 2017.04.26 刪除前確認
            Exit Sub
        End If

        '20161102
        Dim folderPath As String = "" '= Application.StartupPath & "\Database\JetValve\" '噴射閥資料
        Select Case cmbValveChooes.SelectedItem
            Case "PicoPulse"
                folderPath = Application.StartupPath & "\Database\JetValve\PicoTouch\"
            Case "Advanjet"
                folderPath = Application.StartupPath & "\Database\JetValve\Advanjet\"
        End Select

        Dim fileName As String = folderPath & lstJetValve.SelectedItem & ".pst"

        'jimmy 20170704
        Dim bDBDelectCheck As Boolean = False
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) = lstJetValve.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No2).ValveName(eValveWorkMode.Valve1) = lstJetValve.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No3).ValveName(eValveWorkMode.Valve1) = lstJetValve.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No4).ValveName(eValveWorkMode.Valve1) = lstJetValve.SelectedItem Then
                    bDBDelectCheck = False
                Else
                    bDBDelectCheck = True
                End If
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) = lstJetValve.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No2).ValveName(eValveWorkMode.Valve1) = lstJetValve.SelectedItem Then
                    bDBDelectCheck = False
                Else
                    bDBDelectCheck = True
                End If
            Case Else
                Select Case gSSystemParameter.StageUseValveCount

                    Case eMechanismModule.OneValveOneStage
                        If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) = lstJetValve.SelectedItem Then
                            bDBDelectCheck = False
                        Else
                            bDBDelectCheck = True
                        End If
                    Case eMechanismModule.TwoValveOneStage
                        If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) = lstJetValve.SelectedItem Or
                            RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve2) = lstJetValve.SelectedItem Then
                            bDBDelectCheck = False
                        Else
                            bDBDelectCheck = True
                        End If
                End Select

        End Select

        If bDBDelectCheck = True Then
            If System.IO.File.Exists(fileName) Then
                System.IO.File.Delete(fileName)
            End If
            If gJetValveDB.ContainsKey(lstJetValve.SelectedItem) = True Then
                gJetValveDB.Remove(lstJetValve.SelectedItem)
                '[Note]:顯示上是顯示StageNo1的ValveNo1
                JetValveDB_Update(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1))
            End If

            If Not gJetValveDB.ContainsKey("Default") Then 'Soni + 2017.04.26 預設檔重建
                gJetValveDB.Add("Default", New CJetValveParameter("Default"))
                gJetValveDB("Default").Save(folderPath & "Default.pst")
                JetValveDB_Update(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1))
            End If
        Else
            MessageBox.Show("Operation Could Not be Completed")
        End If



    End Sub

#End Region

#Region "AugerValveDB"
    ''' <summary>選擇的螺桿閥名稱</summary>
    ''' <remarks></remarks>
    Dim AugerValveSelectedItem As String = ""

    Public Sub SetAugerValveDB(ByRef augerValveParam As CAugerValveParameter, ByRef txtAugerRotationSpeed As TextBox, ByRef txtOpenTime As TextBox, ByRef rdoManual As RadioButton, ByRef rdoOneShot As RadioButton)
        With augerValveParam
            txtOpenTime.Text = .OneShotOpenTime
            txtAugerRotationSpeed.Text = .RotationSpeed
            Select Case .ValveControlType
                Case eValveControlType.OneShot
                    rdoManual.Checked = False
                    rdoOneShot.Checked = True
                Case eValveControlType.Manual
                    rdoManual.Checked = True
                    rdoOneShot.Checked = False
            End Select

        End With
    End Sub
    Public Sub GetAugerValveDB(ByRef augerValveParam As CAugerValveParameter, ByRef txtAugerRotationSpeed As TextBox, ByRef txtOpenTime As TextBox, ByRef rdoManual As RadioButton, ByRef rdoOneShot As RadioButton)
        With augerValveParam
            .OneShotOpenTime = Val(txtOpenTime.Text)
            If rdoManual.Checked = True Then
                .ValveControlType = eValveControlType.Manual
            ElseIf rdoOneShot.Checked = True Then
                .ValveControlType = eValveControlType.OneShot
            End If
            .RotationSpeed = Val(txtAugerRotationSpeed.Text)

        End With
    End Sub

    Public Sub AugerValveDB_Update()

        lstAugerValve.Items.Clear()
        For mI As Integer = 0 To gAugerValveDB.Count - 1
            lstAugerValve.Items.Add(gAugerValveDB.Keys(mI))
        Next

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) = enmValveType.Auger Then
                    cmbValve1.Items.Clear()
                    For mI As Integer = 0 To gAugerValveDB.Count - 1
                        cmbValve1.Items.Add(gAugerValveDB.Keys(mI))
                    Next
                End If
                If gSSystemParameter.StageParts.ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1) = enmValveType.Auger Then
                    cmbValve1.Items.Clear()
                    For mI As Integer = 0 To gAugerValveDB.Count - 1
                        cmbValve2.Items.Add(gAugerValveDB.Keys(mI))
                    Next
                End If
                If gSSystemParameter.StageParts.ValveData(enmStage.No3).ValveType(eValveWorkMode.Valve1) = enmValveType.Auger Then
                    cmbValve1.Items.Clear()
                    For mI As Integer = 0 To gAugerValveDB.Count - 1
                        cmbValve3.Items.Add(gAugerValveDB.Keys(mI))
                    Next
                End If
                If gSSystemParameter.StageParts.ValveData(enmStage.No4).ValveType(eValveWorkMode.Valve1) = enmValveType.Auger Then
                    cmbValve1.Items.Clear()
                    For mI As Integer = 0 To gAugerValveDB.Count - 1
                        cmbValve4.Items.Add(gAugerValveDB.Keys(mI))
                    Next
                End If

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) = enmValveType.Auger Then
                    cmbValve1.Items.Clear()
                    For mI As Integer = 0 To gAugerValveDB.Count - 1
                        cmbValve1.Items.Add(gAugerValveDB.Keys(mI))
                    Next
                End If
                If gSSystemParameter.StageParts.ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1) = enmValveType.Auger Then
                    cmbValve1.Items.Clear()
                    For mI As Integer = 0 To gAugerValveDB.Count - 1
                        cmbValve2.Items.Add(gAugerValveDB.Keys(mI))
                    Next
                End If

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) = enmValveType.Auger Then
                            cmbValve1.Items.Clear()
                            For mI As Integer = 0 To gAugerValveDB.Count - 1
                                cmbValve1.Items.Add(gAugerValveDB.Keys(mI))
                            Next
                        End If

                    Case eMechanismModule.TwoValveOneStage
                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) = enmValveType.Auger Then
                            cmbValve1.Items.Clear()
                            For mI As Integer = 0 To gAugerValveDB.Count - 1
                                cmbValve1.Items.Add(gAugerValveDB.Keys(mI))
                            Next
                        End If
                        If gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve2) = enmValveType.Auger Then
                            cmbValve1.Items.Clear()
                            For mI As Integer = 0 To gAugerValveDB.Count - 1
                                cmbValve2.Items.Add(gAugerValveDB.Keys(mI))
                            Next
                        End If

                End Select
        End Select

    End Sub

    Private Sub lstAugerValve_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAugerValve.SelectedIndexChanged
        If lstAugerValve.SelectedIndex < 0 Then
            lstAugerValve.BackColor = Color.Yellow
            lstAugerValve.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstAugerValve.BackColor = Color.White
            lstAugerValve.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If
        AugerValveSelectedItem = lstAugerValve.SelectedItem
        SetAugerValveDB(gAugerValveDB(lstAugerValve.SelectedItem), txtAugerRotationSpeed, txtAugerValveOpenTime, rdoManual, rdoOneShot)
    End Sub

    Private Sub btnAugerControllerUpdate_Click(sender As Object, e As EventArgs) Handles btnAugerControllerUpdate.Click
        If lstAugerValve.SelectedIndex < 0 Then
            lstAugerValve.BackColor = Color.Yellow
            lstAugerValve.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstAugerValve.BackColor = Color.White
            lstAugerValve.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If
        GetAugerValveDB(gAugerValveDB(lstAugerValve.SelectedItem), txtAugerRotationSpeed, txtAugerValveOpenTime, rdoManual, rdoOneShot)
        Dim folderPath As String = Application.StartupPath & "\Database\AugerValve\" '噴射閥資料
        Dim fileName As String = folderPath & gAugerValveDB(lstAugerValve.SelectedItem).Name & ".pst"

        gAugerValveDB(lstAugerValve.SelectedItem).Save(fileName)

        ' AugerValveDB_Update()      Jeffdel  20160413
        txtAugerRotationSpeed_TextChanged(sender, e)
    End Sub

    Private Sub btnAugerControllerAdd_Click(sender As Object, e As EventArgs) Handles btnAugerControllerAdd.Click

        If txtAugerValveName.Text.Trim = "" Then
            txtAugerValveName.BackColor = Color.Yellow
            txtAugerValveName.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            txtAugerValveName.BackColor = Color.White
            txtAugerValveName.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If
        Dim newAugerValve As New CAugerValveParameter(txtAugerValveName.Text) 'Soni / 2017.04.26
        newAugerValve.Name = txtAugerValveName.Text
        If IsillegalFileName(newAugerValve.Name) = True Then
            '檔案名稱錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000058))
            MsgBox(gMsgHandler.GetMessage(Warn_3000058), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("File Name Error")
            Return
        Else
            If gAugerValveDB.ContainsKey(newAugerValve.Name) Then
                If MsgBox("Auger Valve Data already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                    lstAugerValve.SelectedItem = newAugerValve.Name
                    GetAugerValveDB(gAugerValveDB(lstAugerValve.SelectedItem), txtAugerRotationSpeed, txtAugerValveOpenTime, rdoManual, rdoOneShot)
                End If
                Exit Sub
            End If
            GetAugerValveDB(newAugerValve, txtAugerRotationSpeed, txtAugerValveOpenTime, rdoManual, rdoOneShot)
            gAugerValveDB.Add(newAugerValve.Name, newAugerValve)

            Dim folderPath As String = Application.StartupPath & "\Database\AugerValve\" '噴射閥資料
            Dim fileName As String = folderPath & newAugerValve.Name & ".pst"

            If System.IO.File.Exists(fileName) Then
                If MsgBox("Auger Valve File already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                    newAugerValve.Save(fileName)
                End If
            Else
                newAugerValve.Save(fileName)
            End If
        End If


        '20160805
        AugerValveDB_Update()
    End Sub

    Private Sub btnAugerControllerDelete_Click(sender As Object, e As EventArgs) Handles btnAugerControllerDelete.Click
        If lstAugerValve.SelectedIndex < 0 Then
            lstAugerValve.BackColor = Color.Yellow
            lstAugerValve.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstAugerValve.BackColor = Color.White
            lstAugerValve.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If

        If MsgBox("Are You Sure to Delete?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then 'Soni + 2017.04.26 刪除前確認
            Exit Sub
        End If

        Dim folderPath As String = Application.StartupPath & "\Database\AugerValve\" '噴射閥資料
        Dim fileName As String = folderPath & lstAugerValve.SelectedItem & ".pst"
        If System.IO.File.Exists(fileName) Then
            System.IO.File.Delete(fileName)
        End If
        If gAugerValveDB.ContainsKey(lstAugerValve.SelectedItem) = True Then
            gAugerValveDB.Remove(lstAugerValve.SelectedItem)
            AugerValveDB_Update()           '20160805
        End If
    End Sub
    Private Sub txtAugerRotationSpeed_TextChanged(sender As Object, e As EventArgs) Handles txtAugerRotationSpeed.TextChanged, txtAugerValveOpenTime.TextChanged, rdoManual.CheckedChanged, rdoOneShot.CheckedChanged
        Dim ChangeColor As Color = Color.Red
        If AugerValveSelectedItem = "" Then
            Exit Sub
        End If
        If gAugerValveDB.ContainsKey(AugerValveSelectedItem) = False Then
            Exit Sub
        End If

        With gAugerValveDB(AugerValveSelectedItem)

            If .OneShotOpenTime = Val(txtAugerValveOpenTime.Text) Then
                txtAugerValveOpenTime.BackColor = Color.White
            Else
                txtAugerValveOpenTime.BackColor = ChangeColor
            End If
            If .RotationSpeed = Val(txtAugerRotationSpeed.Text) Then
                txtAugerRotationSpeed.BackColor = Color.White
            Else
                txtAugerRotationSpeed.BackColor = ChangeColor
            End If
            'If .Comment = txtAugerValveComment.Text Then
            '    txtAugerValveComment.BackColor = Color.White
            'Else
            '    txtAugerValveComment.BackColor = ChangeColor
            'End If
            Select Case .ValveControlType
                Case eValveControlType.Manual
                    If rdoManual.Checked Then
                        rdoManual.BackColor = Color.White
                        rdoOneShot.BackColor = Color.White
                    Else
                        rdoManual.BackColor = Color.Red
                        rdoOneShot.BackColor = Color.Red
                    End If
                Case eValveControlType.OneShot
                    If rdoOneShot.Checked Then
                        rdoManual.BackColor = Color.White
                        rdoOneShot.BackColor = Color.White
                    Else
                        rdoManual.BackColor = Color.Red
                        rdoOneShot.BackColor = Color.Red
                    End If
            End Select

            If rdoManual.Checked Then
                txtAugerValveOpenTime.Enabled = False
            Else
                txtAugerValveOpenTime.Enabled = True
            End If

        End With

    End Sub
#End Region


    Private Sub txtJetValveCycleTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles _
    txtGlueOffsetZ4.KeyPress, txtGlueOffsetZ3.KeyPress, txtGlueOffsetZ2.KeyPress, txtGlueOffsetZ1.KeyPress, txtGlueOffsetY4.KeyPress, txtGlueOffsetY3.KeyPress, txtGlueOffsetY2.KeyPress, txtGlueOffsetY1.KeyPress, txtGlueOffsetX4.KeyPress, txtGlueOffsetX3.KeyPress, txtGlueOffsetX2.KeyPress, txtGlueOffsetX1.KeyPress, _
    txtLeadAngle.KeyPress
        Premtek.Base.CKeyPress.CheckDecimal(sender, e)
    End Sub
    Private Sub txtSyringeAPSet1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSyringeAPSet1.KeyPress, txtSyringeAPSet2.KeyPress, txtSyringeAPSet3.KeyPress, txtSyringeAPSet4.KeyPress, _
        txtValveAPSet1.KeyPress, txtValveAPSet2.KeyPress, txtValveAPSet3.KeyPress, txtValveAPSet4.KeyPress, _
 _
 _
 _
 _
        txtPasteViscocity.KeyPress, txtPasteMinTemp.KeyPress, txtPasteMaxTemp.KeyPress, txtPasteExpiredTime.KeyPress, _
 _
        txtLeadAngleDistance.KeyPress, txtConveyorSpeed.KeyPress

        Premtek.Base.CKeyPress.CheckUDecimal(sender, e)
    End Sub

    Private Sub txtPasteExpiredCount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPasteExpiredCount.KeyPress
        Premtek.Base.CKeyPress.CheckUInteger(sender, e)
    End Sub

#Region "氣壓設定"
    Private Sub btnSetSyringeAP1_Click(sender As Object, e As EventArgs) Handles btnSetSyringeAP1.Click
        btnSetSyringeAP1.BackColor = Color.Yellow
        gEPVCollection.SetValue(eEPVPressureType.Syringe, 0, Val(txtSyringeAPSet1.Text), True)
        btnSetSyringeAP1.BackColor = SystemColors.Control
    End Sub

    Private Sub btnSetSyringeAP2_Click(sender As Object, e As EventArgs) Handles btnSetSyringeAP2.Click
        btnSetSyringeAP2.BackColor = Color.Yellow
        gEPVCollection.SetValue(eEPVPressureType.Syringe, 1, Val(txtSyringeAPSet2.Text), True)
        btnSetSyringeAP2.BackColor = SystemColors.Control
    End Sub

    Private Sub btnSetSyringeAP3_Click(sender As Object, e As EventArgs) Handles btnSetSyringeAP3.Click
        btnSetSyringeAP3.BackColor = Color.Yellow
        gEPVCollection.SetValue(eEPVPressureType.Syringe, 2, Val(txtSyringeAPSet3.Text), True)
        btnSetSyringeAP3.BackColor = SystemColors.Control
    End Sub

    Private Sub btnSetSyringeAP4_Click(sender As Object, e As EventArgs) Handles btnSetSyringeAP4.Click
        btnSetSyringeAP4.BackColor = Color.Yellow
        gEPVCollection.SetValue(eEPVPressureType.Syringe, 3, Val(txtSyringeAPSet4.Text), True)
        btnSetSyringeAP4.BackColor = SystemColors.Control
    End Sub

    '20170327
    Private Sub btnSetValveAP1_Click(sender As Object, e As EventArgs) Handles btnSetValveAP1.Click
        btnSetValveAP1.BackColor = Color.Yellow
        gEPVCollection.SetValue(eEPVPressureType.Valve, 0, Val(txtValveAPSet1.Text), True)
        btnSetValveAP1.BackColor = SystemColors.Control
    End Sub

    Private Sub btnSetValveAP2_Click(sender As Object, e As EventArgs) Handles btnSetValveAP2.Click
        btnSetValveAP2.BackColor = Color.Yellow
        gEPVCollection.SetValue(eEPVPressureType.Valve, 1, Val(txtValveAPSet2.Text), True)
        btnSetValveAP2.BackColor = SystemColors.Control
    End Sub
    Private Sub btnSetValveAP3_Click(sender As Object, e As EventArgs) Handles btnSetValveAP3.Click
        btnSetValveAP3.BackColor = Color.Yellow
        gEPVCollection.SetValue(eEPVPressureType.Valve, 2, Val(txtValveAPSet2.Text), True)
        btnSetValveAP3.BackColor = SystemColors.Control
    End Sub
    Private Sub btnSetValveAP4_Click(sender As Object, e As EventArgs) Handles btnSetValveAP4.Click
        btnSetValveAP4.BackColor = Color.Yellow
        gEPVCollection.SetValue(eEPVPressureType.Valve, 3, Val(txtValveAPSet2.Text), True)
        btnSetValveAP4.BackColor = SystemColors.Control
    End Sub

    Private Sub btnSetSyringe1OnOff_Click(sender As Object, e As EventArgs) Handles btnSetSyringe1OnOff.Click
        gDOCollection.SetState(enmDO.SyringePressure1, Not gDOCollection.GetState(enmDO.SyringePressure1))
        Select Case gDOCollection.GetState(enmDO.SyringePressure1)
            Case True
                btnSetSyringe1OnOff.Text = GetString("On")
            Case False
                btnSetSyringe1OnOff.Text = GetString("Off")
        End Select
    End Sub

    Private Sub btnSetSyringe2OnOff_Click(sender As Object, e As EventArgs) Handles btnSetSyringe2OnOff.Click
        gDOCollection.SetState(enmDO.SyringePressure2, Not gDOCollection.GetState(enmDO.SyringePressure2))
        Select Case gDOCollection.GetState(enmDO.SyringePressure2)
            Case True
                btnSetSyringe2OnOff.Text = GetString("On")
            Case False
                btnSetSyringe2OnOff.Text = GetString("Off")
        End Select
    End Sub

    Private Sub btnSetSyringe3OnOff_Click(sender As Object, e As EventArgs) Handles btnSetSyringe3OnOff.Click
        gDOCollection.SetState(enmDO.SyringePressure3, Not gDOCollection.GetState(enmDO.SyringePressure3))
        Select Case gDOCollection.GetState(enmDO.SyringePressure3)
            Case True
                btnSetSyringe3OnOff.Text = GetString("On")
            Case False
                btnSetSyringe3OnOff.Text = GetString("Off")
        End Select
    End Sub

    Private Sub btnSetSyringe4OnOff_Click(sender As Object, e As EventArgs) Handles btnSetSyringe4OnOff.Click
        gDOCollection.SetState(enmDO.SyringePressure4, Not gDOCollection.GetState(enmDO.SyringePressure4))
        Select Case gDOCollection.GetState(enmDO.SyringePressure4)
            Case True
                btnSetSyringe4OnOff.Text = GetString("On")
            Case False
                btnSetSyringe4OnOff.Text = GetString("Off")
        End Select
    End Sub
#End Region

    Private Sub cmbLaserMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLaserMode.SelectedIndexChanged
        If cmbLaserMode.SelectedIndex < 0 Then
            Exit Sub
        End If
        Select Case cmbLaserMode.SelectedIndex
            Case 0
                RecipeEdit.LaserMode = enmLaserMode.AverageHigh
            Case 1
                RecipeEdit.LaserMode = enmLaserMode.MaxHigh
        End Select
    End Sub


#Region "溫度DB"

    ''' <summary>溫度顯示設定</summary>
    ''' <remarks></remarks>
    Sub ShowTempConfig()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                grpValveTemp1.Visible = True
                grpValveTemp2.Visible = True
                grpValveTemp3.Visible = True
                grpValveTemp4.Visible = True
                lblFlowRateParameter1.Visible = True
                lblFlowRateParameter2.Visible = True
                lblFlowRateParameter3.Visible = True
                lblFlowRateParameter4.Visible = True
                cmbValveFlowRate1.Visible = True
                cmbValveFlowRate2.Visible = True
                cmbValveFlowRate3.Visible = True
                cmbValveFlowRate4.Visible = True
                grpMachineAHotPlate.Visible = True
                grpMachineBHotPlate.Visible = True
                grpConveyorTemp.Visible = True
                grpLoader.Visible = True
                grpTime.Visible = True

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                grpValveTemp1.Visible = False
                grpValveTemp2.Visible = False
                grpValveTemp3.Visible = False
                grpValveTemp4.Visible = False
                lblFlowRateParameter1.Visible = True
                lblFlowRateParameter2.Visible = True
                lblFlowRateParameter3.Visible = False
                lblFlowRateParameter4.Visible = False
                cmbValveFlowRate1.Visible = True
                cmbValveFlowRate2.Visible = True
                cmbValveFlowRate3.Visible = False
                cmbValveFlowRate4.Visible = False
                grpMachineAHotPlate.Visible = True
                grpMachineBHotPlate.Visible = False
                grpConveyorTemp.Visible = True
                grpLoader.Visible = False
                grpTime.Visible = True

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        grpValveTemp1.Visible = True
                        grpValveTemp2.Visible = False
                        grpValveTemp3.Visible = False
                        grpValveTemp4.Visible = False
                        lblFlowRateParameter1.Visible = True
                        lblFlowRateParameter2.Visible = False
                        lblFlowRateParameter3.Visible = False
                        lblFlowRateParameter4.Visible = False
                        cmbValveFlowRate1.Visible = True
                        cmbValveFlowRate2.Visible = False
                        cmbValveFlowRate3.Visible = False
                        cmbValveFlowRate4.Visible = False
                        grpMachineAHotPlate.Visible = False
                        grpMachineBHotPlate.Visible = False
                        'Sue0510
                        grpConveyorTemp.Visible = True
                        grpLoader.Visible = False
                        grpTime.Visible = False

                    Case eMechanismModule.TwoValveOneStage
                        grpValveTemp1.Visible = True
                        grpValveTemp2.Visible = True
                        grpValveTemp3.Visible = False
                        grpValveTemp4.Visible = False
                        lblFlowRateParameter1.Visible = True
                        ' lblFlowRateParameter2.Visible = True
                        lblFlowRateParameter3.Visible = False
                        lblFlowRateParameter4.Visible = False
                        cmbValveFlowRate1.Visible = True
                        '  cmbValveFlowRate2.Visible = True
                        cmbValveFlowRate3.Visible = False
                        cmbValveFlowRate4.Visible = False
                        grpMachineAHotPlate.Visible = False
                        grpMachineBHotPlate.Visible = False
                        'Sue0510
                        grpConveyorTemp.Visible = True
                        grpLoader.Visible = False
                        grpTime.Visible = False

                        cmbValveFlowRate2.Visible = True
                        lblFlowRateParameter2.Visible = True


                End Select
        End Select
    End Sub

    Private Sub btnTempDBAdd_Click(sender As Object, e As EventArgs) Handles btnTempDBAdd.Click

        If txtTempDB.Text.Trim = "" Then
            txtTempDB.BackColor = Color.Yellow
            txtTempDB.Refresh() ''Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            txtTempDB.BackColor = Color.White
            txtTempDB.Refresh() ''Soni / 2017.05.10
            Exit Sub
        End If

        If IsillegalFileName(txtTempDB.Text) = True Then
            '檔案名稱錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000058))
            MsgBox(gMsgHandler.GetMessage(Warn_3000058), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("File Name Error")
            Return
        Else
            Dim mNewTempConfig As New CTempParameter(txtTempDB.Text.Trim) 'Soni / 2017.04.26 下拉
            mNewTempConfig.Name = txtTempDB.Text
            If gTempDB.ContainsKey(mNewTempConfig.Name) Then
                If MsgBox("Temperature Data already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                    lstTempDB.SelectedItem = mNewTempConfig.Name
                    GetTempDB(gTempDB(lstTempDB.SelectedItem))
                End If
                Exit Sub
            End If

            If GetTempDB(mNewTempConfig) Then
                gTempDB.Add(mNewTempConfig.Name, mNewTempConfig)
                Dim folderPath As String = Application.StartupPath & "\Database\Temperature\" '溫控資料
                Dim fileName As String = folderPath & mNewTempConfig.Name & ".tdb"

                If System.IO.File.Exists(fileName) Then
                    If MsgBox("Temperature File already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                        mNewTempConfig.save(fileName)
                    End If
                Else
                    mNewTempConfig.Save(fileName)
                End If
                TempDB_Update(mNewTempConfig.Name)
            End If
        End If

    End Sub




    Sub SetNumericUpDown(ByRef nmc As NumericUpDown, ByVal value As Decimal)
        If value > nmc.Maximum Then
            nmc.BackColor = Color.Red
            nmc.Value = nmc.Maximum
            Exit Sub
        End If
        If value < nmc.Minimum Then
            nmc.BackColor = Color.Red
            nmc.Value = nmc.Minimum
            Exit Sub
        End If
        nmc.Value = value
        nmc.BackColor = Color.White
    End Sub

    ''' <summary>介面到DB</summary>
    ''' <param name="jetValveParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTempDB(ByRef jetValveParam As CTempParameter) As Object

        '[說明]:判斷是否為整數
        '    (IsNumeric(txtValveAPSet.Text) AndAlso Val(txtValveAPSet.Text) >= 0) And
        '.ValveAirPressure = Val(txtValveAPSet.Text)

        With jetValveParam
            .TempParam(enmTemp.HotPlateA1).Enabled = cboxHotPlate1.CheckState
            .TempParam(enmTemp.HotPlateA2).Enabled = cboxHotPlate2.CheckState
            .TempParam(enmTemp.HotPlateA3).Enabled = cboxHotPlate3.CheckState
            .TempParam(enmTemp.HotPlateA4).Enabled = cboxHotPlate4.CheckState
            .TempParam(enmTemp.HotPlateA5).Enabled = cboxHotPlate5.CheckState
            .TempParam(enmTemp.HotPlateA6).Enabled = cboxHotPlate6.CheckState
            .TempParam(enmTemp.HotPlateB1).Enabled = cboxHotPlateB1.CheckState
            .TempParam(enmTemp.HotPlateB2).Enabled = cboxHotPlateB2.CheckState
            .TempParam(enmTemp.HotPlateB3).Enabled = cboxHotPlateB3.CheckState
            .TempParam(enmTemp.HotPlateB4).Enabled = cboxHotPlateB4.CheckState
            .TempParam(enmTemp.HotPlateB5).Enabled = cboxHotPlateB5.CheckState
            .TempParam(enmTemp.HotPlateB6).Enabled = cboxHotPlateB6.CheckState

            .TempParam(enmTemp.HotPlateA1).SetValue = Val(nmuTempWorkStationSet.Value)
            .TempParam(enmTemp.HotPlateA2).SetValue = Val(nmuTempWorkStationSet.Value)
            .TempParam(enmTemp.HotPlateA3).SetValue = Val(nmuTempWorkStationSet.Value)
            .TempParam(enmTemp.HotPlateA4).SetValue = Val(nmuTempWorkStationSet.Value)
            .TempParam(enmTemp.HotPlateA5).SetValue = Val(nmuTempWorkStationSet.Value)
            .TempParam(enmTemp.HotPlateA6).SetValue = Val(nmuTempWorkStationSet.Value)
            .TempParam(enmTemp.HotPlateB1).SetValue = Val(nmuTempWorkStationSet.Value)
            .TempParam(enmTemp.HotPlateB2).SetValue = Val(nmuTempWorkStationSet.Value)
            .TempParam(enmTemp.HotPlateB3).SetValue = Val(nmuTempWorkStationSet.Value)
            .TempParam(enmTemp.HotPlateB4).SetValue = Val(nmuTempWorkStationSet.Value)
            .TempParam(enmTemp.HotPlateB5).SetValue = Val(nmuTempWorkStationSet.Value)
            .TempParam(enmTemp.HotPlateB6).SetValue = Val(nmuTempWorkStationSet.Value)

            .TempParam(enmTemp.HotPlateA1).PVOS = nmcPVOS1.Value
            .TempParam(enmTemp.HotPlateA2).PVOS = nmcPVOS2.Value
            .TempParam(enmTemp.HotPlateA3).PVOS = nmcPVOS3.Value
            .TempParam(enmTemp.HotPlateA4).PVOS = nmcPVOS4.Value
            .TempParam(enmTemp.HotPlateA5).PVOS = nmcPVOS5.Value
            .TempParam(enmTemp.HotPlateA6).PVOS = nmcPVOS6.Value
            .TempParam(enmTemp.HotPlateB1).PVOS = nmcPVOSB1.Value
            .TempParam(enmTemp.HotPlateB2).PVOS = nmcPVOSB2.Value
            .TempParam(enmTemp.HotPlateB3).PVOS = nmcPVOSB3.Value
            .TempParam(enmTemp.HotPlateB4).PVOS = nmcPVOSB4.Value
            .TempParam(enmTemp.HotPlateB5).PVOS = nmcPVOSB5.Value
            .TempParam(enmTemp.HotPlateB6).PVOS = nmcPVOSB6.Value

            .TempParam(enmTemp.PreStation).SetValue = Val(nmuTempPreStationSet.Value)
            .TempParam(enmTemp.WorkStation).SetValue = Val(nmuTempWorkStationSet.Value)
            .TempParam(enmTemp.PostStation).SetValue = Val(nmuTempPostStationSet.Value)

            '20160920
            .TempParam(enmTemp.Loader).SetValue = Val(nmuTempLoader.Value)
            .TempParam(enmTemp.Unloader).SetValue = Val(nmuTempUnLoader.Value)

            '20161205
            .TempParam(enmTemp.Loader).Enabled = chkTempLoader.CheckState
            .TempParam(enmTemp.Unloader).Enabled = chkTempUnLoader.CheckState

            .TempParam(enmTemp.Nozzle1).Enabled = chkTempNozzle1.CheckState
            .TempParam(enmTemp.Nozzle2).Enabled = chkTempNozzle2.CheckState
            .TempParam(enmTemp.Nozzle3).Enabled = chkTempNozzle3.CheckState
            .TempParam(enmTemp.Nozzle4).Enabled = chkTempNozzle4.CheckState
            .TempParam(enmTemp.Nozzle1).SetValue = Val(nmuTempNozzleSet1.Value)
            .TempParam(enmTemp.Nozzle2).SetValue = Val(nmuTempNozzleSet2.Value)
            .TempParam(enmTemp.Nozzle3).SetValue = Val(nmuTempNozzleSet3.Value)
            .TempParam(enmTemp.Nozzle4).SetValue = Val(nmuTempNozzleSet4.Value)

            .TempParam(enmTemp.ValveBody1).Enabled = chkTempValveBody1.CheckState
            .TempParam(enmTemp.ValveBody2).Enabled = chkTempValveBody2.CheckState
            .TempParam(enmTemp.ValveBody3).Enabled = chkTempValveBody3.CheckState
            .TempParam(enmTemp.ValveBody4).Enabled = chkTempValveBody4.CheckState
            .TempParam(enmTemp.ValveBody1).SetValue = Val(nmuTempValveBodySet1.Value)
            .TempParam(enmTemp.ValveBody2).SetValue = Val(nmuTempValveBodySet2.Value)
            .TempParam(enmTemp.ValveBody3).SetValue = Val(nmuTempValveBodySet3.Value)
            .TempParam(enmTemp.ValveBody4).SetValue = Val(nmuTempValveBodySet4.Value)

            .TempParam(enmTemp.SyringeBody1).Enabled = chkTempSyringeBody1.CheckState
            .TempParam(enmTemp.SyringeBody2).Enabled = chkTempSyringeBody2.CheckState
            .TempParam(enmTemp.SyringeBody3).Enabled = chkTempSyringeBody3.CheckState
            .TempParam(enmTemp.SyringeBody4).Enabled = chkTempSyringeBody4.CheckState
            .TempParam(enmTemp.SyringeBody1).SetValue = Val(nmuTempSyringeBodySet1.Value)
            .TempParam(enmTemp.SyringeBody2).SetValue = Val(nmuTempSyringeBodySet2.Value)
            .TempParam(enmTemp.SyringeBody3).SetValue = Val(nmuTempSyringeBodySet3.Value)
            .TempParam(enmTemp.SyringeBody4).SetValue = Val(nmuTempSyringeBodySet4.Value)

            '[說明]:分機型,不連動顯示,不過要提醒 20161010   待測試
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    If .TempParam(enmTemp.HotPlateA1).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateA2).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateA3).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateA4).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateA5).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateA6).Enabled = CheckState.Checked AndAlso _
                       .TempParam(enmTemp.HotPlateB1).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateB2).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateB3).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateB4).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateB5).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateB6).Enabled = CheckState.Checked Then
                        If chkTempWorkStation.CheckState = CheckState.Unchecked Then
                            '工作區確認狀態錯誤
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080020))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2080020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            'MessageBox.Show("Work-Station CheckState is False!!!")

                        End If
                    ElseIf .TempParam(enmTemp.HotPlateA1).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateA2).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateA3).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateA4).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateA5).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateA6).Enabled = CheckState.Unchecked AndAlso _
                        .TempParam(enmTemp.HotPlateB1).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateB2).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateB3).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateB4).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateB5).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateB6).Enabled = CheckState.Unchecked Then
                        If chkTempWorkStation.CheckState = CheckState.Checked Then
                            '工作區確認狀態正確
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080021))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2080021), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            'MessageBox.Show("Work-Station CheckState is True!!!")
                        End If
                    End If
                Case enmMachineType.DCS_500AD
                    If .TempParam(enmTemp.HotPlateA1).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateA2).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateA3).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateA4).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateA5).Enabled = CheckState.Checked AndAlso .TempParam(enmTemp.HotPlateA6).Enabled = CheckState.Checked Then
                        If chkTempWorkStation.CheckState = CheckState.Unchecked Then
                            '工作區確認狀態錯誤
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080020))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2080020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            'MessageBox.Show("Work-Station CheckState is False!!!")

                        End If
                    ElseIf .TempParam(enmTemp.HotPlateA1).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateA2).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateA3).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateA4).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateA5).Enabled = CheckState.Unchecked AndAlso .TempParam(enmTemp.HotPlateA6).Enabled = CheckState.Unchecked Then
                        If chkTempWorkStation.CheckState = CheckState.Checked Then
                            '工作區確認狀態正確
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080021))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2080021), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            'MessageBox.Show("Work-Station CheckState is True!!!")
                        End If
                    End If
                Case enmMachineType.eDTS_2S2V
                    If .TempParam(enmTemp.HotPlateA1).Enabled = True AndAlso .TempParam(enmTemp.HotPlateA2).Enabled = True AndAlso .TempParam(enmTemp.HotPlateA3).Enabled = True AndAlso .TempParam(enmTemp.HotPlateA4).Enabled = True AndAlso .TempParam(enmTemp.HotPlateA5).Enabled = True AndAlso .TempParam(enmTemp.HotPlateA6).Enabled = True Then
                        If chkTempWorkStation.CheckState = CheckState.Checked Then
                            '工作區確認狀態錯誤
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080020))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2080020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            'MessageBox.Show("Work-Station CheckState is False!!!")
                        End If
                    ElseIf .TempParam(enmTemp.HotPlateA1).Enabled = False AndAlso .TempParam(enmTemp.HotPlateA2).Enabled = False AndAlso .TempParam(enmTemp.HotPlateA3).Enabled = False AndAlso .TempParam(enmTemp.HotPlateA4).Enabled = False AndAlso .TempParam(enmTemp.HotPlateA5).Enabled = False AndAlso .TempParam(enmTemp.HotPlateA6).Enabled = False Then
                        If chkTempWorkStation.CheckState = CheckState.Unchecked Then
                            '工作區確認狀態正確
                            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080021))
                            MsgBox(gMsgHandler.GetMessage(Alarm_2080021), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            'MessageBox.Show("Work-Station CheckState is True!!!")
                        End If
                    End If
            End Select

            .TempParam(enmTemp.PreStation).Enabled = chkTempPreStation.CheckState
            .TempParam(enmTemp.WorkStation).Enabled = chkTempWorkStation.CheckState
            .TempParam(enmTemp.PostStation).Enabled = chkTempPostStation.CheckState

            '20161206
            .PriorHeatTime = Val(nmuPriorHeatTime.Value)
        End With
        Return True

    End Function
    ''' <summary>
    ''' DB到介面
    ''' </summary>
    ''' <param name="jetValveParam"></param>
    ''' <remarks></remarks>
    Public Sub SetTempDB(ByRef jetValveParam As CTempParameter)
        With jetValveParam
            cboxHotPlate1.CheckState = .TempParam(enmTemp.HotPlateA1).Enabled
            cboxHotPlate2.CheckState = .TempParam(enmTemp.HotPlateA2).Enabled
            cboxHotPlate3.CheckState = .TempParam(enmTemp.HotPlateA3).Enabled
            cboxHotPlate4.CheckState = .TempParam(enmTemp.HotPlateA4).Enabled
            cboxHotPlate5.CheckState = .TempParam(enmTemp.HotPlateA5).Enabled
            cboxHotPlate6.CheckState = .TempParam(enmTemp.HotPlateA6).Enabled
            cboxHotPlateB1.CheckState = .TempParam(enmTemp.HotPlateB1).Enabled
            cboxHotPlateB2.CheckState = .TempParam(enmTemp.HotPlateB2).Enabled
            cboxHotPlateB3.CheckState = .TempParam(enmTemp.HotPlateB3).Enabled
            cboxHotPlateB4.CheckState = .TempParam(enmTemp.HotPlateB4).Enabled
            cboxHotPlateB5.CheckState = .TempParam(enmTemp.HotPlateB5).Enabled
            cboxHotPlateB6.CheckState = .TempParam(enmTemp.HotPlateB6).Enabled

            SetNumericUpDown(nmcPVOS1, .TempParam(enmTemp.HotPlateA1).PVOS)
            SetNumericUpDown(nmcPVOS2, .TempParam(enmTemp.HotPlateA2).PVOS)
            SetNumericUpDown(nmcPVOS3, .TempParam(enmTemp.HotPlateA3).PVOS)
            SetNumericUpDown(nmcPVOS4, .TempParam(enmTemp.HotPlateA4).PVOS)
            SetNumericUpDown(nmcPVOS5, .TempParam(enmTemp.HotPlateA5).PVOS)
            SetNumericUpDown(nmcPVOS6, .TempParam(enmTemp.HotPlateA6).PVOS)
            SetNumericUpDown(nmcPVOSB1, .TempParam(enmTemp.HotPlateB1).PVOS)
            SetNumericUpDown(nmcPVOSB2, .TempParam(enmTemp.HotPlateB2).PVOS)
            SetNumericUpDown(nmcPVOSB3, .TempParam(enmTemp.HotPlateB3).PVOS)
            SetNumericUpDown(nmcPVOSB4, .TempParam(enmTemp.HotPlateB4).PVOS)
            SetNumericUpDown(nmcPVOSB5, .TempParam(enmTemp.HotPlateB5).PVOS)
            SetNumericUpDown(nmcPVOSB6, .TempParam(enmTemp.HotPlateB6).PVOS)

            chkTempPreStation.CheckState = .TempParam(enmTemp.PreStation).Enabled
            chkTempWorkStation.CheckState = .TempParam(enmTemp.WorkStation).Enabled
            chkTempPostStation.CheckState = .TempParam(enmTemp.PostStation).Enabled

            Premtek.ControlMisc.SetNumericValue(nmuTempPreStationSet, .TempParam(enmTemp.PreStation).SetValue)
            Premtek.ControlMisc.SetNumericValue(nmuTempWorkStationSet, .TempParam(enmTemp.WorkStation).SetValue)
            Premtek.ControlMisc.SetNumericValue(nmuTempPostStationSet, .TempParam(enmTemp.PostStation).SetValue)

            '20160920
            Premtek.ControlMisc.SetNumericValue(nmuTempLoader, .TempParam(enmTemp.Loader).SetValue)
            Premtek.ControlMisc.SetNumericValue(nmuTempUnLoader, .TempParam(enmTemp.Unloader).SetValue)


            '20161205
            chkTempLoader.CheckState = .TempParam(enmTemp.Loader).Enabled
            chkTempUnLoader.CheckState = .TempParam(enmTemp.Unloader).Enabled

            chkTempNozzle1.CheckState = .TempParam(enmTemp.Nozzle1).Enabled
            chkTempNozzle2.CheckState = .TempParam(enmTemp.Nozzle2).Enabled
            chkTempNozzle3.CheckState = .TempParam(enmTemp.Nozzle3).Enabled
            chkTempNozzle4.CheckState = .TempParam(enmTemp.Nozzle4).Enabled

            Premtek.ControlMisc.SetNumericValue(nmuTempNozzleSet1, .TempParam(enmTemp.Nozzle1).SetValue)
            Premtek.ControlMisc.SetNumericValue(nmuTempNozzleSet2, .TempParam(enmTemp.Nozzle2).SetValue)
            Premtek.ControlMisc.SetNumericValue(nmuTempNozzleSet3, .TempParam(enmTemp.Nozzle3).SetValue)
            Premtek.ControlMisc.SetNumericValue(nmuTempNozzleSet4, .TempParam(enmTemp.Nozzle4).SetValue)


            chkTempValveBody1.CheckState = .TempParam(enmTemp.ValveBody1).Enabled
            chkTempValveBody2.CheckState = .TempParam(enmTemp.ValveBody2).Enabled
            chkTempValveBody3.CheckState = .TempParam(enmTemp.ValveBody3).Enabled
            chkTempValveBody4.CheckState = .TempParam(enmTemp.ValveBody4).Enabled

            Premtek.ControlMisc.SetNumericValue(nmuTempValveBodySet1, .TempParam(enmTemp.ValveBody1).SetValue)
            Premtek.ControlMisc.SetNumericValue(nmuTempValveBodySet2, .TempParam(enmTemp.ValveBody2).SetValue)
            Premtek.ControlMisc.SetNumericValue(nmuTempValveBodySet3, .TempParam(enmTemp.ValveBody3).SetValue)
            Premtek.ControlMisc.SetNumericValue(nmuTempValveBodySet4, .TempParam(enmTemp.ValveBody4).SetValue)


            chkTempSyringeBody1.CheckState = .TempParam(enmTemp.SyringeBody1).Enabled
            chkTempSyringeBody2.CheckState = .TempParam(enmTemp.SyringeBody2).Enabled
            chkTempSyringeBody3.CheckState = .TempParam(enmTemp.SyringeBody3).Enabled
            chkTempSyringeBody4.CheckState = .TempParam(enmTemp.SyringeBody4).Enabled

            Premtek.ControlMisc.SetNumericValue(nmuTempSyringeBodySet1, .TempParam(enmTemp.SyringeBody1).SetValue)
            Premtek.ControlMisc.SetNumericValue(nmuTempSyringeBodySet2, .TempParam(enmTemp.SyringeBody2).SetValue)
            Premtek.ControlMisc.SetNumericValue(nmuTempSyringeBodySet3, .TempParam(enmTemp.SyringeBody3).SetValue)
            Premtek.ControlMisc.SetNumericValue(nmuTempSyringeBodySet4, .TempParam(enmTemp.SyringeBody4).SetValue)

            '20161206
            Premtek.ControlMisc.SetNumericValue(nmuPriorHeatTime, .PriorHeatTime)

        End With
    End Sub
    Private Sub btnTempDBUpdate_Click(sender As Object, e As EventArgs) Handles btnTempDBUpdate.Click
        If lstTempDB.SelectedIndex < 0 Then
            lstTempDB.BackColor = Color.Yellow
            lstTempDB.Refresh() ''Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstTempDB.BackColor = Color.White
            lstTempDB.Refresh() ''Soni / 2017.05.10
            Exit Sub
        End If
        If gTempDB.ContainsKey(lstTempDB.SelectedItem) = True Then
            If GetTempDB(gTempDB(lstTempDB.SelectedItem)) Then
                Dim folderPath As String = Application.StartupPath & "\Database\Temperature\" '溫控資料
                Dim fileName As String = folderPath & gTempDB(lstTempDB.SelectedItem).Name & ".tdb"
                gTempDB(lstTempDB.SelectedItem).Save(fileName)
            End If
        End If
    End Sub

    Private Sub btnTempDBDel_Click(sender As Object, e As EventArgs) Handles btnTempDBDel.Click
        If lstTempDB.SelectedIndex < 0 Then
            lstTempDB.BackColor = Color.Yellow
            lstTempDB.Refresh() ''Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstTempDB.BackColor = Color.White
            lstTempDB.Refresh() ''Soni / 2017.05.10
            Exit Sub
        End If

        If MsgBox("Are You Sure to Delete?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then 'Soni + 2017.04.26 刪除前確認
            Exit Sub
        End If

        Dim folderPath As String = Application.StartupPath & "\Database\Temperature\" '噴射閥資料
        Dim fileName As String = folderPath & lstTempDB.SelectedItem & ".tdb"


        'jimmy 20170704
        If lstTempDB.SelectedItem = RecipeEdit.TempName Then

            MessageBox.Show("Operation Could Not be Completed")
        Else
            If System.IO.File.Exists(fileName) Then
                System.IO.File.Delete(fileName)
            End If
            If gTempDB.ContainsKey(lstTempDB.SelectedItem) = True Then
                gTempDB.Remove(lstTempDB.SelectedItem)
                TempDB_Update(RecipeEdit.TempName)
            End If

            If Not gTempDB.ContainsKey("Default") Then '預設檔重建
                gTempDB.Add("Default", New CTempParameter("Default"))
                gTempDB("Default").save(folderPath & "Default.tdb")
                TempDB_Update(RecipeEdit.TempName)
            End If
        End If



    End Sub

    ''' <summary>溫控選擇名稱</summary>
    ''' <remarks></remarks>
    Dim TempSelectedItem As String = ""
    Private Sub lstTempDB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTempDB.SelectedIndexChanged
        If lstTempDB.SelectedIndex < 0 Then
            lstTempDB.BackColor = Color.Yellow
            lstTempDB.Refresh() ''Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstTempDB.BackColor = Color.White
            Exit Sub
        End If
        TempSelectedItem = lstTempDB.SelectedItem
        txtTempDB.Text = lstTempDB.SelectedItem
        SetTempDB(gTempDB(lstTempDB.SelectedItem))
    End Sub

    Public Sub TempDB_Update(ByVal dataBaseName As String)
        lstTempDB.Items.Clear()
        cmbTemp1.Items.Clear()
        For i As Integer = 0 To gTempDB.Count - 1
            lstTempDB.Items.Add(gTempDB.Keys(i))
            cmbTemp1.Items.Add(gTempDB.Keys(i))
            If dataBaseName = gTempDB.Keys(i) Then
                SetTempDB(gTempDB(dataBaseName))
                txtTempDB.Text = dataBaseName
                cmbTemp1.SelectedIndex = i
                lstTempDB.SelectedIndex = i
            End If
        Next
    End Sub

    Private Sub btnSetPVOSA1_Click(sender As Object, e As EventArgs) Handles btnSetPVOSA1.Click
        Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A1, nmcPVOS1.Value)
    End Sub
    Private Sub btnSetPVOSA2_Click(sender As Object, e As EventArgs) Handles btnSetPVOSA2.Click
        Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A2, nmcPVOS2.Value)
    End Sub

    Private Sub btnSetPVOSA3_Click(sender As Object, e As EventArgs) Handles btnSetPVOSA3.Click
        Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A3, nmcPVOS3.Value)
    End Sub

    Private Sub btnSetPVOSA4_Click(sender As Object, e As EventArgs) Handles btnSetPVOSA4.Click
        Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A4, nmcPVOS4.Value)
    End Sub

    Private Sub btnSetPVOSA5_Click(sender As Object, e As EventArgs) Handles btnSetPVOSA5.Click
        Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A5, nmcPVOS5.Value)
    End Sub

    Private Sub btnSetPVOSA6_Click(sender As Object, e As EventArgs) Handles btnSetPVOSA6.Click
        Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A6, nmcPVOS6.Value)
    End Sub

    Private Sub btnSetPVOSB1_Click(sender As Object, e As EventArgs) Handles btnSetPVOSB1.Click
        Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B1, nmcPVOSB1.Value)
    End Sub

    Private Sub btnSetPVOSB2_Click(sender As Object, e As EventArgs) Handles btnSetPVOSB2.Click
        Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B2, nmcPVOSB2.Value)
    End Sub

    Private Sub btnSetPVOSB3_Click(sender As Object, e As EventArgs) Handles btnSetPVOSB3.Click
        Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B3, nmcPVOSB3.Value)
    End Sub

    Private Sub btnSetPVOSB4_Click(sender As Object, e As EventArgs) Handles btnSetPVOSB4.Click
        Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B4, nmcPVOSB4.Value)
    End Sub

    Private Sub btnSetPVOSB5_Click(sender As Object, e As EventArgs) Handles btnSetPVOSB5.Click
        Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B5, nmcPVOSB5.Value)
    End Sub

    Private Sub btnSetPVOSB6_Click(sender As Object, e As EventArgs) Handles btnSetPVOSB6.Click
        Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B6, nmcPVOSB6.Value)
    End Sub

#End Region

#Region "秤重DB"

    ''' <summary>更新選單</summary>
    ''' <param name="dataBaseName"></param>
    ''' <remarks></remarks>
    Public Sub WeightDB_Update(ByVal dataBaseName As String)

        lstWeighDB.Items.Clear()
        cmbValveFlowRate1.Items.Clear()
        cmbValveFlowRate2.Items.Clear()
        cmbValveFlowRate3.Items.Clear()
        cmbValveFlowRate4.Items.Clear()

        For mI As Integer = 0 To gFlowRateDB.Count - 1
            lstWeighDB.Items.Add(gFlowRateDB.Keys(mI))
            cmbValveFlowRate1.Items.Add(gFlowRateDB.Keys(mI))
            cmbValveFlowRate2.Items.Add(gFlowRateDB.Keys(mI))
            cmbValveFlowRate3.Items.Add(gFlowRateDB.Keys(mI))
            cmbValveFlowRate4.Items.Add(gFlowRateDB.Keys(mI))
            '[說明]顯示No1的資料
            If gFlowRateDB.Keys(mI) = dataBaseName Then
                SetWeightDB(gFlowRateDB(dataBaseName))
                txtWeightDB.Text = dataBaseName
                lstWeighDB.SelectedIndex = mI
            End If
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    If gFlowRateDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve1) Then
                        cmbValveFlowRate1.SelectedIndex = mI
                    End If
                    If gFlowRateDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No2).FlowRateName(eValveWorkMode.Valve1) Then
                        cmbValveFlowRate2.SelectedIndex = mI
                    End If
                    If gFlowRateDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No3).FlowRateName(eValveWorkMode.Valve1) Then
                        cmbValveFlowRate3.SelectedIndex = mI
                    End If
                    If gFlowRateDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No4).FlowRateName(eValveWorkMode.Valve1) Then
                        cmbValveFlowRate4.SelectedIndex = mI
                    End If

                Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                    If gFlowRateDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve1) Then
                        cmbValveFlowRate1.SelectedIndex = mI
                    End If
                    If gFlowRateDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No2).FlowRateName(eValveWorkMode.Valve1) Then
                        cmbValveFlowRate2.SelectedIndex = mI
                    End If

                Case Else
                    Select Case gSSystemParameter.StageUseValveCount
                        Case eMechanismModule.OneValveOneStage
                            If gFlowRateDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve1) Then
                                cmbValveFlowRate1.SelectedIndex = mI
                            End If

                        Case eMechanismModule.TwoValveOneStage
                            If gFlowRateDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve1) Then
                                cmbValveFlowRate1.SelectedIndex = mI
                            End If
                            If gFlowRateDB.Keys(mI) = RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve2) Then
                                cmbValveFlowRate2.SelectedIndex = mI
                            End If

                    End Select

            End Select
        Next

        'If cmbValveFlowRate1.SelectedIndex = -1 Then
        '    If cmbValveFlowRate1.Items.Count > 0 Then
        '        cmbValveFlowRate1.SelectedIndex = 0
        '    End If
        'End If
        'If cmbValveFlowRate2.SelectedIndex = -1 Then
        '    If cmbValveFlowRate2.Items.Count > 0 Then
        '        cmbValveFlowRate2.SelectedIndex = 0
        '    End If
        'End If
        'If cmbValveFlowRate3.SelectedIndex = -1 Then
        '    If cmbValveFlowRate3.Items.Count > 0 Then
        '        cmbValveFlowRate3.SelectedIndex = 0
        '    End If
        'End If
        'If cmbValveFlowRate4.SelectedIndex = -1 Then
        '    If cmbValveFlowRate4.Items.Count > 0 Then
        '        cmbValveFlowRate4.SelectedIndex = 0
        '    End If
        'End If

    End Sub

    Function SetWeightDB(ByRef item As CFlowRateParameter) As Boolean
        'Jeffadd 20160726
        With item
            If CInt(.WeighingTimes) <= 0 Then
                Premtek.ControlMisc.SetNumericValue(nmcWeightTimes, 1)
                .WeighingTimes = 1
            Else
                Premtek.ControlMisc.SetNumericValue(nmcWeightTimes, .WeighingTimes)
            End If

            Premtek.ControlMisc.SetNumericValue(nmcWeighingPointNumber, .WeighingPointNumber)
            'EnableDoAverageWeight
            If .WeighingEnableDoAverageWeight = True Then
                cbEnableAverageWeight.Checked = True
            Else
                cbEnableAverageWeight.Checked = False
                '[說明]:確認False 關閉以下選項
                'nmcWeighingWeight.Enabled = False
            End If
            'EnableDoAverageDot
            If .WeighingEnableDoAverageDot = True Then
                cbEnableAverageDot.Checked = True
            Else
                cbEnableAverageDot.Checked = False
                '[說明]:確認False 關閉以下選項
                nmcWeightDotMin.Enabled = False
                nmcWeightDotMax.Enabled = False
            End If
            If .WeighingEnableDoAverageWeight = True Or .WeighingEnableDoAverageDot = True Then
                '先mark 一定要設定重量_ Toby
                'nmcWeighingWaitCalibrationTime.Enabled = True
                'nmcWeighingTolerance.Enabled = True
            Else
                '先mark 一定要設定重量_ Toby
                'nmcWeighingWaitCalibrationTime.Enabled = False
                'nmcWeighingTolerance.Enabled = False
            End If
            'nmcWeighingWeight.Value = CDec(.WeighingWeight)
            Premtek.ControlMisc.SetNumericValue(nmcWeighingWaitCalibrationTime, .WeighingWaitCalibrationTime)
            Premtek.ControlMisc.SetNumericValue(nmcWeighingTolerance, .WeighingTolerance)
            Premtek.ControlMisc.SetNumericValue(nmcWeightDotMin, .WeighingWeightDotMin)
            Premtek.ControlMisc.SetNumericValue(nmcWeightDotMax, .WeighingWeightDotMax)

            If .WeighingEnableProductionRunFail = True Then
                cbEnableProductionRunFail.Checked = True
            Else
                cbEnableProductionRunFail.Checked = False
            End If
            Premtek.ControlMisc.SetNumericValue(nmcWeighingPulseTime, .WeighingPulseTimes)
            Premtek.ControlMisc.SetNumericValue(nmcWeighingCycleTime, .WeighingCycleTime)
            Premtek.ControlMisc.SetNumericValue(nmcWeighingBlanceSteadyTime, .WeighingBlanceSteadyTimes)
            Premtek.ControlMisc.SetNumericValue(nmcWeighingTableSteadyTime, .WeighingTableTimes)
            Premtek.ControlMisc.SetNumericValue(nmcWeighingPressure, .WeighingPressure)
            Premtek.ControlMisc.SetNumericValue(nmcWeightBaseTimes, .OnTimer)
            Premtek.ControlMisc.SetNumericValue(nmcWeightBaseRuns, .OnRuns)

            cmbBaseOn.SelectedIndex = .BaseOn

            Premtek.ControlMisc.SetNumericValue(nmcWeighingGetBalance, .WeighingGetBalance)
            If .WeighingWeight < (.WeighingPointNumber * .WeighingWeightDotMin) / 4 Then
                'MsgBox("設定目標重量異常")
                .WeighingWeight = (.WeighingPointNumber * .WeighingWeightDotMin) / 4
            End If

            Premtek.ControlMisc.SetNumericValue(nmcWeighingWeight, .WeighingWeight)
        End With
        Return True
    End Function


    Function GetWeightDB(ByRef item As CFlowRateParameter) As Boolean
        'Jeffadd 20160726
        With item
            '20161122
            'If cbEnableWeightFunc.Checked = True Then
            '    .WeighingEnableWeightFunc = True
            '    '20161114
            '    .BaseOn = CDbl(cmbBaseOn.SelectedIndex)
            'Else
            '    .WeighingEnableWeightFunc = False
            '    '20161114
            '    .BaseOn = eInspectionType.Noen
            'End If

            .BaseOn = CDbl(cmbBaseOn.SelectedIndex)


            If CInt(nmcWeightTimes.Value) <= 0 Then
                .WeighingTimes = 1
                Premtek.ControlMisc.SetNumericValue(nmcWeightTimes, 1)
            Else
                .WeighingTimes = CInt(nmcWeightTimes.Value)
            End If

            .WeighingPointNumber = CDbl(nmcWeighingPointNumber.Value)

            'EnableAverageWeight
            If cbEnableAverageWeight.Checked = True Then
                .WeighingEnableDoAverageWeight = True
            Else
                .WeighingEnableDoAverageWeight = False
            End If
            'EnableAverageDot
            If cbEnableAverageDot.Checked = True Then
                .WeighingEnableDoAverageDot = True
            Else
                .WeighingEnableDoAverageDot = False
            End If

            '.WeighingWeight = CDbl(nmcWeighingWeight.Value)

            .WeighingWaitCalibrationTime = CDbl(nmcWeighingWaitCalibrationTime.Value)
            .WeighingTolerance = CDbl(nmcWeighingTolerance.Value)
            .WeighingWeightDotMin = CDbl(nmcWeightDotMin.Value)
            .WeighingWeightDotMax = CDbl(nmcWeightDotMax.Value)

            If nmcWeighingWeight.Value < (.WeighingPointNumber * .WeighingWeightDotMin) / 4 Then
                MsgBox("設定目標重量異常", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Premtek.ControlMisc.SetNumericValue(nmcWeighingWeight, (.WeighingPointNumber * .WeighingWeightDotMin) / 4)
            End If
            .WeighingWeight = CDbl(nmcWeighingWeight.Value)


            If cbEnableProductionRunFail.Checked = True Then
                .WeighingEnableProductionRunFail = True
            Else
                .WeighingEnableProductionRunFail = False
            End If
            .WeighingPulseTimes = CDbl(nmcWeighingPulseTime.Value)
            .WeighingCycleTime = CDbl(nmcWeighingCycleTime.Value)
            .WeighingBlanceSteadyTimes = CDbl(nmcWeighingBlanceSteadyTime.Value)
            .WeighingTableTimes = CDbl(nmcWeighingTableSteadyTime.Value)

            .WeighingPressure = CDbl(nmcWeighingPressure.Value)


            .WeighingGetBalance = CDbl(nmcWeighingGetBalance.Value)
            .OnTimer = CDbl(nmcWeightBaseTimes.Value)
            .OnRuns = CDbl(nmcWeightBaseRuns.Value)


        End With
        Return True
    End Function

    Private Sub btnWeightDBAdd_Click(sender As Object, e As EventArgs) Handles btnWeightDBAdd.Click
        'Jeffadd 20160726
        Dim mWeightDB As New CFlowRateParameter("Default")
        If txtWeightDB.Text.Trim = "" Then
            txtWeightDB.BackColor = Color.Yellow
            txtWeightDB.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            txtWeightDB.BackColor = Color.White
            txtWeightDB.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If

        mWeightDB.Name = txtWeightDB.Text
        If IsillegalFileName(mWeightDB.Name) = True Then
            '檔案名稱錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000058))
            MsgBox(gMsgHandler.GetMessage(Warn_3000058), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("File Name Error")
            Return
        Else
            If gFlowRateDB.ContainsKey(mWeightDB.Name) Then
                If MsgBox("Weight Data already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                    lstWeighDB.SelectedItem = mWeightDB.Name
                    GetWeightDB(gFlowRateDB(lstWeighDB.SelectedItem))
                End If
                Exit Sub
            End If

            If GetWeightDB(mWeightDB) Then
                gFlowRateDB.Add(mWeightDB.Name, mWeightDB)
                Dim folderPath As String = Application.StartupPath & "\Database\Weight\"
                Dim fileName As String = folderPath & mWeightDB.Name & ".wdb"
                If System.IO.File.Exists(fileName) Then
                    If MsgBox("Weight File already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbYes Then
                        mWeightDB.Save(fileName)
                    End If
                Else
                    mWeightDB.Save(fileName)
                End If

                WeightDB_Update(mWeightDB.Name)
            End If
        End If
    End Sub

    Private Sub btnWeightDBUpdate_Click(sender As Object, e As EventArgs) Handles btnWeightDBUpdate.Click
        If lstWeighDB.SelectedIndex < 0 Then
            lstWeighDB.BackColor = Color.Yellow
            lstWeighDB.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstWeighDB.BackColor = Color.White
            lstWeighDB.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If

        If GetWeightDB(gFlowRateDB(lstWeighDB.SelectedItem)) Then
            Dim folderPath As String = Application.StartupPath & "\Database\Weight\" '溫控資料
            Dim fileName As String = folderPath & gFlowRateDB(lstWeighDB.SelectedItem).Name & ".wdb"
            gFlowRateDB(lstWeighDB.SelectedItem).Save(fileName)

        End If
        ' WeightDB_Update()
    End Sub

    Private Sub btnWeightDBDel_Click(sender As Object, e As EventArgs) Handles btnWeightDBDel.Click
        If lstWeighDB.SelectedIndex < 0 Then
            lstWeighDB.BackColor = Color.Yellow
            lstWeighDB.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstWeighDB.BackColor = Color.White
            lstWeighDB.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If

        If MsgBox("Are You Sure to Delete?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then 'Soni + 2017.04.26 刪除前確認
            Exit Sub
        End If

        Dim folderPath As String = Application.StartupPath & "\Database\Weight\" '噴射閥資料
        Dim fileName As String = folderPath & lstWeighDB.SelectedItem & ".wdb"

        'jimmy 20170704
        Dim bDBDelectCheck As Boolean = False
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve1) = lstWeighDB.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No2).FlowRateName(eValveWorkMode.Valve1) = lstWeighDB.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No3).FlowRateName(eValveWorkMode.Valve1) = lstWeighDB.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No4).FlowRateName(eValveWorkMode.Valve1) = lstWeighDB.SelectedItem Then
                    bDBDelectCheck = False
                Else
                    bDBDelectCheck = True
                End If
            Case enmMachineType.eDTS_2S2V
                If RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve1) = lstWeighDB.SelectedItem Or
                   RecipeEdit.StageParts(enmStage.No2).FlowRateName(eValveWorkMode.Valve1) = lstWeighDB.SelectedItem Then
                    bDBDelectCheck = False
                Else
                    bDBDelectCheck = True
                End If
            Case Else
                Select Case gSSystemParameter.StageUseValveCount

                    Case eMechanismModule.OneValveOneStage
                        If RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve1) = lstWeighDB.SelectedItem Then
                            bDBDelectCheck = False
                        Else
                            bDBDelectCheck = True
                        End If
                    Case eMechanismModule.TwoValveOneStage
                        If RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve1) = lstWeighDB.SelectedItem Or
                            RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve2) = lstWeighDB.SelectedItem Then
                            bDBDelectCheck = False
                        Else
                            bDBDelectCheck = True
                        End If
                End Select

        End Select

        If bDBDelectCheck = True Then
            If System.IO.File.Exists(fileName) Then
                System.IO.File.Delete(fileName)
            End If
            gFlowRateDB.Remove(lstWeighDB.SelectedItem)
            WeightDB_Update(RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve1))

            If Not gFlowRateDB.ContainsKey("Default") Then 'Soni + 2017.04.26 預設檔重建
                gFlowRateDB.Add("Default", New CFlowRateParameter("Default"))
                gFlowRateDB("Default").Save(folderPath & "Default.wdb")
                WeightDB_Update(RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve1))
            End If
        Else
            MessageBox.Show("Operation Could Not be Completed")
        End If


    End Sub
    Dim weightSelectedItem As String
    Private Sub lstWeighDB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstWeighDB.SelectedIndexChanged
        If lstWeighDB.SelectedIndex < 0 Then
            lstWeighDB.BackColor = Color.Yellow
            lstWeighDB.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstWeighDB.BackColor = Color.White
            lstWeighDB.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If
        weightSelectedItem = lstWeighDB.SelectedItem
        txtWeightDB.Text = lstWeighDB.SelectedItem
        SetWeightDB(gFlowRateDB(lstWeighDB.SelectedItem))
    End Sub


    'Private Sub cbEnableWeightFunc_CheckedChanged(sender As Object, e As EventArgs) Handles cbEnableWeightFunc.CheckedChanged
    '    'rdbOnTimer.Enabled = chkEnableDotWeightMaintenance.Checked

    '    'Jeffadd 20160726
    '    nmcWeightTimes.Enabled = cbEnableWeightFunc.Checked
    '    nmcWeighingPointNumber.Enabled = cbEnableWeightFunc.Checked
    '    cbEnableAverageWeight.Enabled = cbEnableWeightFunc.Checked
    '    cbEnableAverageDot.Enabled = cbEnableWeightFunc.Checked
    '    nmcWeighingWeight.Enabled = cbEnableWeightFunc.Checked
    '    nmcWeighingWaitCalibrationTime.Enabled = cbEnableWeightFunc.Checked
    '    nmcWeighingTolerance.Enabled = cbEnableWeightFunc.Checked
    '    nmcWeightDotMin.Enabled = cbEnableWeightFunc.Checked
    '    nmcWeightDotMax.Enabled = cbEnableWeightFunc.Checked
    '    cbEnableProductionRunFail.Enabled = cbEnableWeightFunc.Checked
    '    nmcWeighingPulseTime.Enabled = cbEnableWeightFunc.Checked
    '    nmcWeighingCycleTime.Enabled = cbEnableWeightFunc.Checked
    '    nmcWeighingBlanceSteadyTime.Enabled = cbEnableWeightFunc.Checked
    '    nmcWeighingTableSteadyTime.Enabled = cbEnableWeightFunc.Checked
    '    nmcWeighingPressure.Enabled = cbEnableWeightFunc.Checked

    '    cmbBaseOn.Enabled = cbEnableWeightFunc.Checked
    '    nmcWeightBaseTimes.Enabled = cbEnableWeightFunc.Checked
    '    nmcWeightBaseRuns.Enabled = cbEnableWeightFunc.Checked
    'End Sub
#End Region

    Private Sub cbEnableAverageWeight_CheckedChanged(sender As Object, e As EventArgs) Handles cbEnableAverageWeight.CheckedChanged
        'nmcWeighingWeight.Enabled = cbEnableAverageWeight.Checked

        'If cbEnableAverageDot.Checked = True Or cbEnableAverageWeight.Checked = True Then
        '    '先mark 一定要設定重量_ Toby
        '    ' nmcWeighingWaitCalibrationTime.Enabled = True
        '    'nmcWeighingTolerance.Enabled = True
        'Else
        '    '先mark 一定要設定重量_ Toby
        '    'nmcWeighingWaitCalibrationTime.Enabled = False
        '    'nmcWeighingTolerance.Enabled = False
        'End If
    End Sub

    Private Sub cbEnableAverageDot_CheckedChanged(sender As Object, e As EventArgs) Handles cbEnableAverageDot.CheckedChanged
        nmcWeightDotMin.Enabled = cbEnableAverageDot.Checked
        nmcWeightDotMax.Enabled = cbEnableAverageDot.Checked

        If cbEnableAverageDot.Checked = True Or cbEnableAverageWeight.Checked = True Then
            '先mark 一定要設定重量_ Toby
            'nmcWeighingWaitCalibrationTime.Enabled = True
            'nmcWeighingTolerance.Enabled = True
        Else
            '先mark 一定要設定重量_ Toby
            'nmcWeighingWaitCalibrationTime.Enabled = False
            'nmcWeighingTolerance.Enabled = False
        End If
    End Sub

    Private Sub cmbValveFlowRate1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbValveFlowRate1.SelectedIndexChanged
        '[Note]:第一組一定是StageNo1 Valve1
        RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve1) = cmbValveFlowRate1.SelectedItem
    End Sub

    Private Sub cmbValveFlowRate2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbValveFlowRate2.SelectedIndexChanged
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                RecipeEdit.StageParts(enmStage.No2).FlowRateName(eValveWorkMode.Valve1) = cmbValveFlowRate2.SelectedItem

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                RecipeEdit.StageParts(enmStage.No2).FlowRateName(eValveWorkMode.Valve1) = cmbValveFlowRate2.SelectedItem

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        '[Note]:不可能發生，若有肯定有問題

                    Case eMechanismModule.TwoValveOneStage
                        RecipeEdit.StageParts(enmStage.No1).FlowRateName(eValveWorkMode.Valve2) = cmbValveFlowRate2.SelectedItem

                End Select

        End Select
    End Sub

    Private Sub cmbValveFlowRate3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbValveFlowRate3.SelectedIndexChanged

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                RecipeEdit.StageParts(enmStage.No3).FlowRateName(eValveWorkMode.Valve1) = cmbValveFlowRate3.SelectedItem

            Case enmMachineType.eDTS_2S2V
                '[Note]:不可能發生，若有肯定有問題(除非也改成雙閥)

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        '[Note]:不可能發生，若有肯定有問題

                    Case eMechanismModule.TwoValveOneStage
                        '[Note]:不可能發生，若有肯定有問題

                End Select

        End Select

    End Sub

    Private Sub cmbValveFlowRate4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbValveFlowRate4.SelectedIndexChanged
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                RecipeEdit.StageParts(enmStage.No4).FlowRateName(eValveWorkMode.Valve1) = cmbValveFlowRate4.SelectedItem

            Case enmMachineType.eDTS_2S2V
                '[Note]:不可能發生，若有肯定有問題(除非也改成雙閥)

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        '[Note]:不可能發生，若有肯定有問題

                    Case eMechanismModule.TwoValveOneStage
                        '[Note]:不可能發生，若有肯定有問題

                End Select

        End Select
    End Sub

    Private Sub cmbBaseOn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBaseOn.SelectedIndexChanged

        Select Case cmbBaseOn.SelectedIndex
            Case eInspectionType.Noen
                nmcWeightBaseTimes.Enabled = False
                nmcWeightBaseRuns.Enabled = False
            Case eInspectionType.OnTimer
                nmcWeightBaseTimes.Enabled = True
                nmcWeightBaseRuns.Enabled = False
            Case eInspectionType.OnRuns
                nmcWeightBaseTimes.Enabled = False
                nmcWeightBaseRuns.Enabled = True
            Case eInspectionType.OnTimerOrRuns
                nmcWeightBaseTimes.Enabled = True
                nmcWeightBaseRuns.Enabled = True
        End Select
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSaveTemp.Click, btnSaveWeight.Click, btnSaveJet.Click, btnSavePaste.Click, btnSavePurge.Click, btnSaveRecipe.Click, btnSaveAir.Click
        'Sue20170627
        gSyslog.Save("[frmRecipeStepParameter]" & vbTab & "[btnSave]" & vbTab & "Click")
        If btnSaveTemp.Enabled = False Then
            Exit Sub
        End If
        btnSaveTemp.Enabled = False
        RecipeSave()
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        btnSaveTemp.Enabled = True
    End Sub

    'Eason 20170216 Ticket:100080 , Add Arc Type Parameter
    'Eason + 20170109 Ticket:100010 , Add Dot Line Recipe Parameter
    'Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBackTemp.Click, btnBackWeight.Click, btnBackJet.Click, btnBackPaste.Click, btnBackPurge.Click, btnBackRecipe.Click, btnBackAir.Click
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBackTemp.Click, btnBackWeight.Click, btnBackJet.Click, btnBackPaste.Click, btnBackPurge.Click, btnBackRecipe.Click, btnBackAir.Click, btnBackLine.Click, btnBackDot.Click, btnBackArc.Click
        'Sue20170627
        gSyslog.Save("[frmRecipeStepParameter]" & vbTab & "[btnBack]" & vbTab & "Click")
        Me.Close()
    End Sub

#Region "閥,加熱平台溫度上限設定保護"
    Private Sub txtTempNozzleSet1_TextChanged(sender As Object, e As EventArgs)

        If gSSystemParameter.ValveMaxTemperature > 0 Then '有設定最大值
            Dim txt As TextBox = CType(sender, TextBox)
            If Val(txt.Text) > gSSystemParameter.ValveMaxTemperature Then '輸入值超標
                txt.Text = gSSystemParameter.ValveMaxTemperature '卡上限
            End If
        End If
    End Sub

    Private Sub txtTempPreStationSet_TextChanged(sender As Object, e As EventArgs)
        If gSSystemParameter.HotplateMaxTemperature > 0 And gSSystemParameter.HotplateMinTemperature > 0 Then '有設定最大值
            Dim txt As TextBox = CType(sender, TextBox)

            If Val(txt.Text) > gSSystemParameter.HotplateMaxTemperature Then '輸入值超高標
                txt.Text = gSSystemParameter.HotplateMaxTemperature '卡上限
            End If

            '20160920
            If Val(txt.Text) < gSSystemParameter.HotplateMinTemperature Then '輸入值超低標
                txt.Text = gSSystemParameter.HotplateMinTemperature '卡下限
            End If
        End If
    End Sub
#End Region

    Private Sub cmbValveChooes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbValveChooes.SelectedIndexChanged
        If cmbValveChooes.SelectedIndex = -1 Then
            Exit Sub
        End If

        '[說明]:切換參數設定介面
        Select Case cmbValveChooes.SelectedItem
            Case "PicoPulse"
                gpbPICOTouch.Visible = True
                gpbAdvanjet.Visible = False
            Case "Advanjet"
                gpbAdvanjet.Visible = True
                gpbPICOTouch.Visible = False
        End Select

        '[說明]:切換JetValve設定介面
        lstJetValve.Items.Clear()
        Dim mSelectIndex As Integer = 0

        '[Note]:顯示的部分，預設都是先塞在StageNo1的ValveNo1
        For mI As Integer = 0 To gJetValveDB.Count - 1
            '[說明]:依照ValveChooes 塞入lstJetValve資料
            Select Case cmbValveChooes.SelectedItem
                Case "PicoPulse"
                    If gJetValveDB.ContainsKey(gJetValveDB.Keys(mI)) = True Then
                        If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.PicoPulse Then
                            lstJetValve.Items.Add(gJetValveDB.Keys(mI))
                            ''[說明]:顯示第一支閥的lstJetValve資料,如果不是此lst則顯示空
                            If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) = gJetValveDB.Keys(mI) Then
                                SetJetValveDB(gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)))
                                txtJetValveName.Text = RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)
                                lstJetValve.SelectedIndex = mSelectIndex
                            End If
                            mSelectIndex = mSelectIndex + 1
                        End If
                    End If

                Case "Advanjet"
                    If gJetValveDB.ContainsKey(gJetValveDB.Keys(mI)) = True Then
                        If gJetValveDB(gJetValveDB.Keys(mI)).ValveModel = eValveModel.Advanjet Then
                            lstJetValve.Items.Add(gJetValveDB.Keys(mI))
                            ''[說明]:顯示第一支閥的lstJetValve資料,如果不是此lst則顯示空
                            If RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1) = gJetValveDB.Keys(mI) Then
                                SetJetValveDB(gJetValveDB(RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)))
                                txtJetValveName.Text = RecipeEdit.StageParts(enmStage.No1).ValveName(eValveWorkMode.Valve1)
                                lstJetValve.SelectedIndex = mSelectIndex
                            End If
                            mSelectIndex = mSelectIndex + 1
                        End If
                    End If

            End Select
        Next
    End Sub

    Private Sub txtValveAdvanjetRefillTime_KeyPress(sender As Object, e As KeyPressEventArgs)
        '20161010
        If Char.IsDigit(e.KeyChar) Or e.KeyChar = Chr(8) Then
            If IsNumeric(sender.Text) Or sender.Text = "" Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        Else
            If IsNumeric(sender.Text & e.KeyChar) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub btnSetPicoStroke_Click(sender As Object, e As EventArgs) Handles btnSetPicoStroke.Click
        Dim sTemp As String = ""
        If cbxValve.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("尚未選擇")
        Else
            gValvecontrollerCollection.SetStrokeValve(cbxValve.SelectedItem, CInt(nmuPicoStroke.Value), sTemp, False)
        End If
    End Sub

    Private Sub btnSetPicoOpenTime_Click(sender As Object, e As EventArgs) Handles btnSetPicoOpenTime.Click
        Dim sTemp As String = ""
        If cbxValve.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("尚未選擇")
        Else
            gValvecontrollerCollection.SetOpenTime(cbxValve.SelectedItem, CInt(nmuPicoOpenTime.Value), sTemp, False)
        End If
    End Sub

    Private Sub btnSetPicoValveOnTime_Click(sender As Object, e As EventArgs) Handles btnSetPicoValveOnTime.Click
        Dim sTemp As String = ""
        If cbxValve.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("尚未選擇")
        Else
            gValvecontrollerCollection.SetValveOnTime(cbxValve.SelectedItem, CInt(nmuPicoValveOnTime.Value), sTemp, False)
        End If
    End Sub

    Private Sub btnSetPicoCloseTime_Click(sender As Object, e As EventArgs) Handles btnSetPicoCloseTime.Click
        Dim sTemp As String = ""
        If cbxValve.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("尚未選擇")
        Else
            gValvecontrollerCollection.SetCloseTime(cbxValve.SelectedItem, CInt(nmuPicoCloseTime.Value), sTemp, False)
        End If
    End Sub

    Private Sub btnSetPicoValveOffTime_Click(sender As Object, e As EventArgs) Handles btnSetPicoValveOffTime.Click
        Dim sTemp As String = ""
        If cbxValve.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("尚未選擇")
        Else
            gValvecontrollerCollection.SetValveOffTime(cbxValve.SelectedItem, CInt(nmuPicoValveOffTime.Value), sTemp, False)
        End If
    End Sub

    Private Sub btnSetPicoCycleTime_Click(sender As Object, e As EventArgs) Handles btnSetPicoCycleTime.Click
        Dim sTemp As String = ""
        If cbxValve.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("尚未選擇")
        Else
            gValvecontrollerCollection.SetValveCycleTime(cbxValve.SelectedItem, CInt(txtPicoCycleTime.Text), sTemp, False)
        End If
    End Sub

    Private Sub cmbMeasureZMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMeasureZMode.SelectedIndexChanged

        If cmbMeasureZMode.SelectedIndex < 0 Then
            Exit Sub
        End If
        Select Case cmbMeasureZMode.SelectedItem.ToString
            Case "Contact"
                RecipeEdit.LaserFixMode = eHeightModel.Contact
            Case "Laser(Non-onFly)"
                RecipeEdit.LaserFixMode = eHeightModel.Laser_NonOnFly
            Case "Laser(OnFly)"
                RecipeEdit.LaserFixMode = eHeightModel.Laser_OnFly

        End Select

    End Sub

    Private Sub btnSetValve1OnOff_Click(sender As Object, e As EventArgs) Handles btnSetValve1OnOff.Click
        gDOCollection.SetState(enmDO.ValvePressure1, Not gDOCollection.GetState(enmDO.ValvePressure1))
        Select Case gDOCollection.GetState(enmDO.ValvePressure1)
            Case True
                btnSetValve1OnOff.Text = GetString("On")
            Case False
                btnSetValve1OnOff.Text = GetString("Off")
        End Select
    End Sub

    Private Sub btnSetValve2OnOff_Click(sender As Object, e As EventArgs) Handles btnSetValve2OnOff.Click
        gDOCollection.SetState(enmDO.ValvePressure2, Not gDOCollection.GetState(enmDO.ValvePressure2))
        Select Case gDOCollection.GetState(enmDO.ValvePressure2)
            Case True
                btnSetValve2OnOff.Text = GetString("On")
            Case False
                btnSetValve2OnOff.Text = GetString("Off")
        End Select
    End Sub

    Private Sub btnSetValve3OnOff_Click(sender As Object, e As EventArgs) Handles btnSetValve3OnOff.Click
        gDOCollection.SetState(enmDO.ValvePressure3, Not gDOCollection.GetState(enmDO.ValvePressure3))
        Select Case gDOCollection.GetState(enmDO.ValvePressure3)
            Case True
                btnSetValve3OnOff.Text = GetString("On")
            Case False
                btnSetValve3OnOff.Text = GetString("Off")
        End Select
    End Sub

    Private Sub btnSetValve4OnOff_Click(sender As Object, e As EventArgs) Handles btnSetValve4OnOff.Click
        gDOCollection.SetState(enmDO.ValvePressure4, Not gDOCollection.GetState(enmDO.ValvePressure4))
        Select Case gDOCollection.GetState(enmDO.ValvePressure4)
            Case True
                btnSetValve4OnOff.Text = GetString("On")
            Case False
                btnSetValve4OnOff.Text = GetString("Off")
        End Select
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub


    '20170505
    Private Sub cmbRunType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRunType.SelectedIndexChanged
        Dim Str As String = ""

        '[說明]:檢查是Weight or Complex
        'If cmbRunType.SelectedIndex = eWeightControlType.Weight Or cmbRunType.SelectedIndex = Recipe.RunType = eWeightControlType.Complex Then
        If cmbRunType.SelectedIndex = eWeightControlType.Weight Then 'Or cmbRunType.SelectedIndex = eWeightControlType.Complex Then
            For mStageNo As enmStage = enmStage.No1 To gSSystemParameter.StageCount - 1
                For mValveNo As eValveWorkMode = eValveWorkMode.Valve1 To gSSystemParameter.StageUseValveCount - 1

                    '[說明]:檢查(微量天平資料平均單點重量) 
                    If RecipeEdit.StageParts(mStageNo).AverageWeightPerDot(mValveNo) < gFlowRateDB(RecipeEdit.StageParts(mStageNo).FlowRateName(mValveNo)).WeighingWeightDotMin Or
                       RecipeEdit.StageParts(mStageNo).AverageWeightPerDot(mValveNo) > gFlowRateDB(RecipeEdit.StageParts(mStageNo).FlowRateName(mValveNo)).WeighingWeightDotMax Then

                        Select Case gSSystemParameter.LanguageType
                            Case enmLanguageType.eEnglish
                                Str = "DotWeight < WeighingWeightDotMin or DotWeight > WeighingWeightDotMax!!Please re-weigh the weight or adjust weight the maximum and minimum single"
                            Case enmLanguageType.eSimplifiedChinese
                                Str = "单点重量小于最小或单点重量大于最大!!请重作秤重动作或调整单颗最大最小值!!"
                            Case enmLanguageType.eTraditionalChinese
                                Str = "單點重量小於最小或單點重量大於最大!!請重作秤重動作或調整單顆最大最小值!!"
                        End Select
                        MsgBox(Str, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Exit Sub
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub tabAir_Click(sender As Object, e As EventArgs) Handles tabAir.Click

    End Sub

    Private Sub cmbValve1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbValve1.SelectedIndexChanged, cmbValve2.SelectedIndexChanged
        Dim mValve1 As Decimal = GetTriggerCycleTime(cmbValve1.SelectedItem)
        Dim mValve2 As Decimal = GetTriggerCycleTime(cmbValve2.SelectedItem)
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
            Case enmMachineType.eDTS_2S2V

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                    Case eMechanismModule.TwoValveOneStage
                        If gSSystemParameter.MultiDispenseEnable = True Then '雙閥同動時
                            If cmbValve1.SelectedIndex <> -1 And cmbValve2.SelectedIndex <> -1 Then '如果兩閥都有選才比較
                                If mValve1 <> mValve2 Then '兩閥Cycle Time不同時
                                    MsgBox("Cycle Time Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                    CType(sender, ComboBox).SelectedIndex = -1
                                End If
                            End If
                        End If

                End Select
        End Select
    End Sub

   
  
End Class

Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports ProjectAOI
Imports ProjectRecipe.MCommonRecipe
Imports ProjectRecipe
Imports ProjectMotion
Imports ProjectIO
Imports ProjectCore
Imports ProjectFeedback
Imports ProjectTriggerBoard '觸發控制
Imports WetcoConveyor
Imports System.IO

Public Class frmSystemSet
    Public mCount As Integer = 0
    Public sys1 As sSysParam
    Public sys2 As sSysParam
    Public sys3 As sSysParam
    Public sys4 As sSysParam
    'Public myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSystemSet))
    Private mAutoSearchMode As Boolean  '[True:表示為Standard測高 False:表示為換針的測高]

    ''' <summary>Resource內的資料消失, 另開Function處理</summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetString(ByVal value As String) As String
        Select Case value
            Case "Disabled"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Disabled"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "禁用"
                    Case enmLanguageType.eTraditionalChinese
                        Return "關閉"
                End Select
            Case "Enabled"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Enabled"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "启用"
                    Case enmLanguageType.eTraditionalChinese
                        Return "開啓"
                End Select
            Case "Jet"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Jet"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "喷射阀"
                    Case enmLanguageType.eTraditionalChinese
                        Return "噴射閥"
                End Select
            Case "Auger"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Auger"
                    Case enmLanguageType.eSimplifiedChinese
                    Case enmLanguageType.eTraditionalChinese
                        Return "螺桿閥"
                End Select
            Case "OnFly"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "OnFly"
                    Case enmLanguageType.eSimplifiedChinese
                    Case enmLanguageType.eTraditionalChinese
                        Return "飛拍"
                End Select
            Case "NonOnFly"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "NonOnFly"
                    Case enmLanguageType.eSimplifiedChinese
                    Case enmLanguageType.eTraditionalChinese
                        Return "定點拍照"
                End Select
            Case "None"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "None"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "无"
                    Case enmLanguageType.eTraditionalChinese
                        Return "無"
                End Select
        End Select
        Return ""
    End Function

    Dim mtabCharge As TabPage

    Private Sub SystemSet_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        mtabCharge = tabCharge
        TabControl1.Controls.Remove(tabCharge) '先移除

        With cboDispenserNo1ValveType.Items
            .Clear()
            .Add(enmValveType.Jet & " " & GetString("Jet"))
            .Add(enmValveType.Auger & " " & GetString("Auger"))
            .Add(enmValveType.None & " " & GetString("None"))

        End With

        With cboDispenserNo2ValveType.Items
            .Clear()
            .Add(enmValveType.Jet & " " & GetString("Jet"))
            .Add(enmValveType.Auger & " " & GetString("Auger"))
            .Add(enmValveType.None & " " & GetString("None"))
        End With
        With cboDispenserNo3ValveType.Items
            .Clear()
            .Add(enmValveType.Jet & " " & GetString("Jet"))
            .Add(enmValveType.Auger & " " & GetString("Auger"))
            .Add(enmValveType.None & " " & GetString("None"))
        End With

        With cboDispenserNo4ValveType.Items
            .Clear()
            .Add(enmValveType.Jet & " " & GetString("Jet"))
            .Add(enmValveType.Auger & " " & GetString("Auger"))
            .Add(enmValveType.None & " " & GetString("None"))
        End With

        With cboStageUseValveCount.Items
            .Clear()
            
            'jimmy 20170725 
            .Add(eMechanismModule.None & " 0Valve/1Stage ")
            .Add(eMechanismModule.OneValveOneStage & " 1Valve/1Stage ")
            If gSSystemParameter.MachineType <> enmMachineType.DCSW_800AQ Then
                .Add(eMechanismModule.TwoValveOneStage & " 2Valve/1Stage ")
            End If

        End With

        With cboMachineType.Items
            .Clear()
            .Add("0 Reserved")
            .Add("1 Reserved")
            .Add(enmMachineType.eDTS300A & " DTS 300A")
            .Add(enmMachineType.eDTS_Desktop & " DTS Desktop")
            .Add(enmMachineType.eDTS330A & " DTS 330A")
            .Add(enmMachineType.eDTS_2S2V & " DTS 2S2V")
            .Add(enmMachineType.DCSW_800AQ & " DCS W800AQ")
            .Add(enmMachineType.eDTS330ACR1 & " DTS 330ACR1")
            .Add(enmMachineType.DCS_F230A & " DCS F230A")
            .Add(enmMachineType.DCS_350A & " DCS 350A")
            .Add(enmMachineType.DCS_500AD & " DCS 500AD")
        End With

        With cboCCDModuleType.Items
            .Clear()
            .Add(enmCCDModule.eFix & " Fix ")
            .Add(enmCCDModule.eFree & " Free ")
        End With

        With cboMultiDispense.Items
            .Clear()
            .Add("Disable")
            .Add("Enable")
        End With

        With cboCCDOnFly.Items
            .Clear()
            .Add("Disable")
            .Add("Enable")
        End With

        'With cboCCDFixModelType.Items
        '    .Clear()
        '    .Add(eCCDFixModel.NonOnFly & " Non OnFly ")
        '    .Add(eCCDFixModel.OnFly & " OnFly ")
        'End With

        With cboLanguageType.Items
            .Clear()
            .Add(enmLanguageType.eEnglish & " English ")
            .Add(enmLanguageType.eTraditionalChinese & " 繁體中文")
            .Add(enmLanguageType.eSimplifiedChinese & " 簡体中文")
        End With

        With cboZHeightDevice.Items
            .Clear()
            .Add(enmMeasureType.Laser & " Laser")
            .Add(enmMeasureType.Contact & " Contact")
            .Add(enmMeasureType.Both & " Both(Contact+Laser)")
        End With

        With cmbCCDImageSaveMode.Items
            .Clear()
            .Add(eCCDImageProcess.None & " None")
            .Add(eCCDImageProcess.SaveNG & " Save NG")
            .Add(eCCDImageProcess.SaveAll & " Save All")
        End With

        With cboConveyorModel.Items
            .Clear()
            .Add(eConveyorModel.eConveyorNo1 & " ConveyorNo1")
            .Add(eConveyorModel.eConveyorNo2 & " ConveyorNo2")
            .Add(eConveyorModel.eConveyorNo1No2 & " ConveyorNo1No2")
        End With

        '[Note]:歸零計數是所有的閥都一樣
        chkIsReset.Checked = gSSystemParameter.StageParts.Purge(enmStage.No1).IsReset(eValveWorkMode.Valve1)

        'Sue 20170523
        ShowEnableDisable(btnContinueRun, gSSystemParameter.IsContinueLastRun)
        'chkIsContinueRun.Checked = gSSystemParameter.IsContinueLastRun
       
        Select Case gSSystemParameter.AirPressureUnit
            Case enmAirPressureUnit.Bar
                rdbBar.Checked = True
            Case enmAirPressureUnit.Kgcm2
                rdbKg.Checked = True
            Case enmAirPressureUnit.MPa
                rdbMPa.Checked = True
            Case enmAirPressureUnit.psi
                rdbPsi.Checked = True
            Case enmAirPressureUnit.Torr
                rdbTorr.Checked = True
        End Select

        Dim toolTipText As String = ""

        toolTipText &= "1kg/cm2 =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.Kgcm2, enmAirPressureUnit.MPa) & "MPa" & vbCrLf
        toolTipText &= "1Bar =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.Bar, enmAirPressureUnit.MPa) & "MPa" & vbCrLf
        toolTipText &= "1psi =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.psi, enmAirPressureUnit.MPa) & "MPa" & vbCrLf
        toolTipText &= "1Torr =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.Torr, enmAirPressureUnit.MPa) & "MPa" & vbCrLf
        ToolTip1.SetToolTip(rdbMPa, toolTipText)

        toolTipText = ""
        toolTipText &= "1MPa =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.MPa, enmAirPressureUnit.Kgcm2) & "kg/cm2" & vbCrLf
        toolTipText &= "1Bar =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.Bar, enmAirPressureUnit.Kgcm2) & "kg/cm2" & vbCrLf
        toolTipText &= "1psi =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.psi, enmAirPressureUnit.Kgcm2) & "kg/cm2" & vbCrLf
        toolTipText &= "1Torr =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.Torr, enmAirPressureUnit.Kgcm2) & "kg/cm2" & vbCrLf
        ToolTip1.SetToolTip(rdbKg, toolTipText)

        toolTipText = ""
        toolTipText &= "1MPa =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.MPa, enmAirPressureUnit.Bar) & "Bar" & vbCrLf
        toolTipText &= "1kg/cm2 =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.Kgcm2, enmAirPressureUnit.Bar) & "Bar" & vbCrLf
        toolTipText &= "1psi =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.psi, enmAirPressureUnit.Bar) & "Bar" & vbCrLf
        toolTipText &= "1Torr =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.Torr, enmAirPressureUnit.Bar) & "Bar" & vbCrLf
        ToolTip1.SetToolTip(rdbBar, toolTipText)

        toolTipText = ""
        toolTipText &= "1MPa =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.MPa, enmAirPressureUnit.psi) & "psi" & vbCrLf
        toolTipText &= "1kg/cm2 =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.Kgcm2, enmAirPressureUnit.psi) & "psi" & vbCrLf
        toolTipText &= "1Bar =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.Bar, enmAirPressureUnit.psi) & "psi" & vbCrLf
        toolTipText &= "1Torr =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.Torr, enmAirPressureUnit.psi) & "psi" & vbCrLf
        ToolTip1.SetToolTip(rdbPsi, toolTipText)

        toolTipText = ""
        toolTipText &= "1MPa =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.MPa, enmAirPressureUnit.Torr) & "Torr" & vbCrLf
        toolTipText &= "1kg/cm2 =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.Kgcm2, enmAirPressureUnit.Torr) & "Torr" & vbCrLf
        toolTipText &= "1Bar =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.Bar, enmAirPressureUnit.Torr) & "Torr" & vbCrLf
        toolTipText &= "1psi =" & AirPressureTransform.ToAirPressureUnit(1, enmAirPressureUnit.psi, enmAirPressureUnit.Torr) & "Torr" & vbCrLf
        ToolTip1.SetToolTip(rdbTorr, toolTipText)

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                cbEnableAWeightMeasure.Checked = gSSystemParameter.EnableWeightMeasureA
                txtWeightmeasurementA.Text = gSSystemParameter.WeightMeasureA
                cbEnableBWeightMeasure.Checked = gSSystemParameter.EnableWeightMeasureB
                txtWeightmeasurementB.Text = gSSystemParameter.WeightMeasureB
                cbEnableInitialHotPlate.Checked = gSSystemParameter.EnableInitialHotPlate
                txtWeighingSet.Text = gSSystemParameter.WeightSet
                cbNonCorrection.Visible = False
                cbCorrection.Visible = False
                txtCorrectionSet.Visible = False
                labCorrection.Visible = False
                labCorrectionmg1.Visible = False
                labCorrectionmg2.Visible = False
                labCorrectionmg3.Visible = False
                labCorrectionmg4.Visible = False
                labCorrectionUSL.Visible = False
                labCorrectionLSL.Visible = False
                labWeightUpper.Visible = False
                labWeightLower.Visible = False
                txtCorrectionUSL.Visible = False
                txtCorrectionLSL.Visible = False
                txtCorrectionWeightUpper.Visible = False
                txtCorrectionWeightLower.Visible = False

            Case Else
                cbEnableAWeightMeasure.Checked = gSSystemParameter.EnableWeightMeasureA
                txtWeightmeasurementA.Text = gSSystemParameter.WeightMeasureA
                cbEnableBWeightMeasure.Visible = False
                txtWeightmeasurementB.Visible = False
                cbEnableInitialHotPlate.Checked = gSSystemParameter.EnableInitialHotPlate
                txtWeighingSet.Text = gSSystemParameter.WeightSet
                cbNonCorrection.Checked = gSSystemParameter.NonCorrection
                cbCorrection.Checked = gSSystemParameter.Correction
                txtCorrectionSet.Text = gSSystemParameter.CorrectionNum
                labCorrection.Visible = True
                labCorrectionmg1.Visible = True
                labCorrectionmg2.Visible = True
                labCorrectionmg3.Visible = True
                labCorrectionmg4.Visible = True
                labCorrectionUSL.Visible = True
                labCorrectionLSL.Visible = True
                labWeightUpper.Visible = True
                labWeightLower.Visible = True
                txtCorrectionUSL.Visible = True
                txtCorrectionLSL.Visible = True
                txtCorrectionWeightUpper.Visible = True
                txtCorrectionWeightLower.Visible = True
                txtCorrectionLSL.Text = gSSystemParameter.CorrectionLSL
                txtCorrectionUSL.Text = gSSystemParameter.CorrectionUSL
                txtCorrectionWeightUpper.Text = gSSystemParameter.CorrectionWeightUpper
                txtCorrectionWeightLower.Text = gSSystemParameter.CorrectionWeightLower
                btnCleanWeightB.Visible = False
                Label23.Visible = False
                grpConveyor.Visible = True
        End Select


        txtPathMultiple.Text = gSSystemParameter.PathMultiple
        'gSSystemParameter.PathMultiple = txtPathMultiple.Text

        '20170613
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCS_F230A
                grpConveyor.Visible = False
        End Select


        UpdateComboxB()
        '[說明]:根據使用者權限，限定使用之頁籤
        Select Case gUserLevel
            Case enmUserLevel.eSoftwareMaker
                cboMachineType.Enabled = True
                cboStageUseValveCount.Enabled = True
                cboCCDModuleType.Enabled = True
                cboZHeightDevice.Enabled = True
                cboMultiDispense.Enabled = True
                cboCCDOnFly.Enabled = True
                cboConveyorModel.Enabled = True
                btnSaveMachineType.Enabled = True
                btnMechanismModule.Enabled = True
                If Not TabControl1.Contains(mtabCharge) Then
                    TabControl1.Controls.Add(mtabCharge)
                End If
            Case enmUserLevel.eAdministrator
                If Not TabControl1.Contains(mtabCharge) Then
                    TabControl1.Controls.Add(mtabCharge)
                End If
            Case enmUserLevel.eManager
                If TabControl1.Contains(mtabCharge) Then
                    TabControl1.Controls.Remove(mtabCharge)
                End If
            Case enmUserLevel.eEngineer
                If TabControl1.Contains(mtabCharge) Then
                    TabControl1.Controls.Remove(mtabCharge)
                End If
            Case enmUserLevel.eOperator
                If TabControl1.Contains(mtabCharge) Then
                    TabControl1.Controls.Remove(mtabCharge)
                End If

        End Select

        '讀取參數
        With gSSystemParameter
            txtLogPath.Text = .LogFolderPath
            txtMachineID.Text = .MachineID
            SelectComboBox(cboConveyorModel, CInt(.ConveyorModel))
            SelectComboBox(cboCCDModuleType, CInt(.CCDModuleType))
            SelectComboBox(cboLanguageType, CInt(.LanguageType))
            SelectComboBox(cboMachineType, CInt(.MachineType))
            SelectComboBox(cboStageUseValveCount, CInt(.StageUseValveCount))
            SelectComboBox(cmbCCDImageSaveMode, CInt(.CCDImageSaveMode)) 'CCD存檔模式
            txtCCDImageFolderPath.Text = .CCDImageFolderPath
            txtMotionTolerance.Text = .MotionTolerance.ToString()
            txtPressureTolerance.Text = .PressureTolerance.ToString()
            txtTirggerTolerance.Text = .TriggerTolerance.ToString()
            txtPrecisionTolerance.Text = .PrecisionTolerance.ToString()
            txtTemperatureTolerance.Text = .TemperatureTolerance.ToString()
            SelectComboBox(cboZHeightDevice, CInt(.MeasureType))
            nmcSafeTemperature.Value = .SafeTemperature
            nmcMaxHotplateTemp.Value = .HotplateMaxTemperature
            nmcMinHotplateTemp.Value = .HotplateMinTemperature
            nmcMaxValveTemp.Value = .ValveMaxTemperature
            nmcCCDStableTime.Value = .StableTime.CCDStableTime
            nmcLaserStableTime.Value = .StableTime.LaserStableTime
            nmcSensorTimeOut.Value = .StableTime.CheckSensorTimeout
            nmcPriorHeatTime.Value = .StableTime.PriorHeatTime
            ShowEnableDisable(btnDisableConveyor, Not .IsBypassConveyor)
            ShowEnableDisable(btnPassLUL, Not .PassLUL)
            ShowEnableDisable(btnManualMap, .IsManualMap)
            'Sue 20170523
            ShowEnableDisable(btnContinueRun, .IsContinueLastRun)
            '20170418 飛拍硬體by系統參數設定
            cboCCDOnFly.SelectedIndex = Math.Abs(CInt(.CCDOnFlyEnable))
            cboMultiDispense.SelectedIndex = Math.Abs(CInt(.MultiDispenseEnable))
            'jimmy 20170630
            ShowEnableDisable(btnCleanDevice, .IsCleanDevice)
            cboxMapDataModel.SelectedIndex = gSSystemParameter.IsCompareWithMapData 'Asa add

            'Soni + 2017.07.04 System Set中unload方向修改(Forward→Reversal)後，離開再進入發現還是顯示Forward，但實際上已變成Reversal
            If gSSystemParameter.LoadDirection = True Then
                rbtnLoadForward.Checked = True
                rbtnLoadReversal.Checked = False
            Else
                rbtnLoadForward.Checked = False
                rbtnLoadReversal.Checked = True
            End If
            If gSSystemParameter.UnloadDirection = True Then
                rbtnUnloadForward.Checked = True
                rbtnUnloadReversal.Checked = False
            Else
                rbtnUnloadForward.Checked = False
                rbtnUnloadReversal.Checked = True
            End If
            txtMaxDispVelocity.Text = .MaxDispVelocity
        End With
        
        '=== 讀取速度設定 ===
        With gSSystemParameter
            txtConstVelocityTime.Text = .CrossVerticalTime
            txtMaxCrossStepVelocity.Text = .MaxCrossStepVelocity
            txtMaxCrossDevieVelocity.Text = .MaxCrossDeviceVelocity
            txtMaxDispVelocity.Text = .MaxDispVelocity
        End With
        '=== 讀取速度設定 ===

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                With gSSystemParameter.StageParts
                    SelectComboBox(cboDispenserNo1ValveType, CInt(.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1)))
                    SelectComboBox(cboDispenserNo2ValveType, CInt(.ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1)))
                    SelectComboBox(cboDispenserNo3ValveType, CInt(.ValveData(enmStage.No3).ValveType(eValveWorkMode.Valve1)))
                    SelectComboBox(cboDispenserNo4ValveType, CInt(.ValveData(enmStage.No4).ValveType(eValveWorkMode.Valve1)))
                    SelectComboBox(cboDispenserNo1ValveTypeModel, CInt(.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1)))
                    SelectComboBox(cboDispenserNo2ValveTypeModel, CInt(.ValveData(enmStage.No2).JetValve(eValveWorkMode.Valve1)))
                    SelectComboBox(cboDispenserNo3ValveTypeModel, CInt(.ValveData(enmStage.No3).JetValve(eValveWorkMode.Valve1)))
                    SelectComboBox(cboDispenserNo4ValveTypeModel, CInt(.ValveData(enmStage.No4).JetValve(eValveWorkMode.Valve1)))
                    txtAugerValveCTThreshold1.Text = .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1)
                    txtAugerValveCTThreshold2.Text = .ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1)
                    txtAugerValveCTThreshold3.Text = .ValveData(enmStage.No3).CTThreshold(eValveWorkMode.Valve1)
                    txtAugerValveCTThreshold4.Text = .ValveData(enmStage.No4).CTThreshold(eValveWorkMode.Valve1)
                    ShowEnableDisable(btnBypassDetectGlueNo1, .ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve1))
                    ShowEnableDisable(btnBypassDetectGlueNo2, .ValveData(enmStage.No2).EnableDetectPaste(eValveWorkMode.Valve1))
                    ShowEnableDisable(btnBypassDetectGlueNo3, .ValveData(enmStage.No3).EnableDetectPaste(eValveWorkMode.Valve1))
                    ShowEnableDisable(btnBypassDetectGlueNo4, .ValveData(enmStage.No4).EnableDetectPaste(eValveWorkMode.Valve1))
                End With

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                With gSSystemParameter.StageParts
                    SelectComboBox(cboDispenserNo1ValveType, CInt(.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1)))
                    SelectComboBox(cboDispenserNo2ValveType, CInt(.ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1)))
                    SelectComboBox(cboDispenserNo1ValveTypeModel, CInt(.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1)))
                    SelectComboBox(cboDispenserNo2ValveTypeModel, CInt(.ValveData(enmStage.No2).JetValve(eValveWorkMode.Valve1)))
                    txtAugerValveCTThreshold1.Text = .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1)
                    txtAugerValveCTThreshold2.Text = .ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1)
                    ShowEnableDisable(btnBypassDetectGlueNo1, .ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve1))
                    ShowEnableDisable(btnBypassDetectGlueNo2, .ValveData(enmStage.No2).EnableDetectPaste(eValveWorkMode.Valve1))
                End With

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        With gSSystemParameter.StageParts
                            SelectComboBox(cboDispenserNo1ValveType, CInt(.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1)))
                            SelectComboBox(cboDispenserNo1ValveTypeModel, CInt(.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1)))
                            txtAugerValveCTThreshold1.Text = .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1)
                            ShowEnableDisable(btnBypassDetectGlueNo1, .ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve1))
                        End With

                    Case eMechanismModule.TwoValveOneStage
                        With gSSystemParameter.StageParts
                            SelectComboBox(cboDispenserNo1ValveType, CInt(.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1)))
                            SelectComboBox(cboDispenserNo2ValveType, CInt(.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve2)))
                            SelectComboBox(cboDispenserNo1ValveTypeModel, CInt(.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1)))
                            SelectComboBox(cboDispenserNo2ValveTypeModel, CInt(.ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve2)))
                            txtAugerValveCTThreshold1.Text = .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1)
                            txtAugerValveCTThreshold2.Text = .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve2)
                            ShowEnableDisable(btnBypassDetectGlueNo1, .ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve1))
                            ShowEnableDisable(btnBypassDetectGlueNo2, .ValveData(enmStage.No1).EnableDetectPaste(eValveWorkMode.Valve2))
                        End With

                End Select

        End Select
    End Sub


    '*************************************************************************************************************
#Region "mobary+ 2013/12/15"

    Private Sub btnValveType_Click(sender As Object, e As EventArgs) Handles btnValveType.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnValveType]" & vbTab & "Click")
        If btnValveType.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        btnValveType.Enabled = False
        Dim mToolTip As New ToolTip

        '[說明]:紀錄按之前後的狀態
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                With gSSystemParameter.StageParts
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002074, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002074")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002076, "(StageNo2 ValveNo1) " & .ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002076")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002078, "(StageNo3 ValveNo1) " & .ValveData(enmStage.No3).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002078")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002080, "(StageNo4 ValveNo1) " & .ValveData(enmStage.No4).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002080")
                End With
                If (cboDispenserNo1ValveType.SelectedIndex = -1 Or (cboDispenserNo1ValveType.SelectedIndex = 0 And cboDispenserNo1ValveTypeModel.SelectedIndex = -1)) Or
                    (cboDispenserNo2ValveType.SelectedIndex = -1 Or (cboDispenserNo2ValveType.SelectedIndex = 0 And cboDispenserNo2ValveTypeModel.SelectedIndex = -1)) Or
                    (cboDispenserNo3ValveType.SelectedIndex = -1 Or (cboDispenserNo3ValveType.SelectedIndex = 0 And cboDispenserNo3ValveTypeModel.SelectedIndex = -1)) Or
                    (cboDispenserNo4ValveType.SelectedIndex = -1 Or (cboDispenserNo4ValveType.SelectedIndex = 0 And cboDispenserNo4ValveTypeModel.SelectedIndex = -1)) Then

                    '請確認閥設定值!
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000070))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000070), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MessageBox.Show("Plase Check Valve set !!")
                    Exit Sub
                End If
                With gSSystemParameter.StageParts
                    .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) = cboDispenserNo1ValveType.SelectedIndex
                    .ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1) = cboDispenserNo2ValveType.SelectedIndex
                    .ValveData(enmStage.No3).ValveType(eValveWorkMode.Valve1) = cboDispenserNo3ValveType.SelectedIndex
                    .ValveData(enmStage.No4).ValveType(eValveWorkMode.Valve1) = cboDispenserNo4ValveType.SelectedIndex
                    If .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) = enmValveType.Jet Then
                        .ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) = cboDispenserNo1ValveTypeModel.SelectedIndex
                    End If
                    If .ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1) = enmValveType.Jet Then
                        .ValveData(enmStage.No2).JetValve(eValveWorkMode.Valve1) = cboDispenserNo2ValveTypeModel.SelectedIndex
                    End If
                    If .ValveData(enmStage.No3).ValveType(eValveWorkMode.Valve1) = enmValveType.Jet Then
                        .ValveData(enmStage.No3).JetValve(eValveWorkMode.Valve1) = cboDispenserNo3ValveTypeModel.SelectedIndex
                    End If
                    If .ValveData(enmStage.No4).ValveType(eValveWorkMode.Valve1) = enmValveType.Jet Then
                        .ValveData(enmStage.No4).JetValve(eValveWorkMode.Valve1) = cboDispenserNo4ValveTypeModel.SelectedIndex
                    End If
                End With

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                With gSSystemParameter.StageParts
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002074, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002074")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002076, "(StageNo2 ValveNo1) " & .ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002076")
                End With
                If (cboDispenserNo1ValveType.SelectedIndex = -1 Or (cboDispenserNo1ValveType.SelectedIndex = 0 And cboDispenserNo1ValveTypeModel.SelectedIndex = -1)) Or
                   (cboDispenserNo2ValveType.SelectedIndex = -1 Or (cboDispenserNo2ValveType.SelectedIndex = 0 And cboDispenserNo2ValveTypeModel.SelectedIndex = -1)) Then
                    '請確認閥設定值!
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000070))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000070), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MessageBox.Show("Plase Check Valve set !!")
                    Exit Sub
                End If
                With gSSystemParameter.StageParts
                    .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) = cboDispenserNo1ValveType.SelectedIndex
                    .ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1) = cboDispenserNo2ValveType.SelectedIndex
                    If .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) = enmValveType.Jet Then
                        .ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) = cboDispenserNo1ValveTypeModel.SelectedIndex
                    End If
                    If .ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1) = enmValveType.Jet Then
                        .ValveData(enmStage.No2).JetValve(eValveWorkMode.Valve1) = cboDispenserNo2ValveTypeModel.SelectedIndex
                    End If
                End With

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        With gSSystemParameter.StageParts
                            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002074, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002074")
                        End With
                        If (cboDispenserNo1ValveType.SelectedIndex = -1 Or (cboDispenserNo1ValveType.SelectedIndex = 0 And cboDispenserNo1ValveTypeModel.SelectedIndex = -1)) Then
                            '請確認閥設定值!
                            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000070))
                            MsgBox(gMsgHandler.GetMessage(Warn_3000070), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            'MessageBox.Show("Plase Check Valve set !!")
                            Exit Sub
                        End If
                        With gSSystemParameter.StageParts
                            .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) = cboDispenserNo1ValveType.SelectedIndex
                            If .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) = enmValveType.Jet Then
                                .ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) = cboDispenserNo1ValveTypeModel.SelectedIndex
                            End If
                        End With

                    Case eMechanismModule.TwoValveOneStage
                        With gSSystemParameter.StageParts
                            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002074, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002074")
                            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002076, "(StageNo1 ValveNo2) " & .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve2).ToString()), "INFO_6002076")
                        End With
                        If (cboDispenserNo1ValveType.SelectedIndex = -1 Or (cboDispenserNo1ValveType.SelectedIndex = 0 And cboDispenserNo1ValveTypeModel.SelectedIndex = -1)) Or
                            (cboDispenserNo2ValveType.SelectedIndex = -1 Or (cboDispenserNo2ValveType.SelectedIndex = 0 And cboDispenserNo2ValveTypeModel.SelectedIndex = -1)) Then
                            '請確認閥設定值!
                            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000070))
                            MsgBox(gMsgHandler.GetMessage(Warn_3000070), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            'MessageBox.Show("Plase Check Valve set !!")
                            Exit Sub
                        End If
                        With gSSystemParameter.StageParts
                            .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) = cboDispenserNo1ValveType.SelectedIndex
                            .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve2) = cboDispenserNo2ValveType.SelectedIndex
                            If .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1) = enmValveType.Jet Then
                                .ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve1) = cboDispenserNo1ValveTypeModel.SelectedIndex
                            End If
                            If .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve2) = enmValveType.Jet Then
                                .ValveData(enmStage.No1).JetValve(eValveWorkMode.Valve2) = cboDispenserNo2ValveTypeModel.SelectedIndex
                            End If
                        End With

                End Select
        End Select

        '[說明]:紀錄按之前後的狀態
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                With gSSystemParameter.StageParts
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002075, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002075")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002077, "(StageNo2 ValveNo1) " & .ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002077")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002079, "(StageNo3 ValveNo1) " & .ValveData(enmStage.No3).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002079")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002081, "(StageNo4 ValveNo1) " & .ValveData(enmStage.No4).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002081")
                End With

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                With gSSystemParameter.StageParts
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002075, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002075")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002077, "(StageNo2 ValveNo1) " & .ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002077")
                End With

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        With gSSystemParameter.StageParts
                            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002075, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002075")
                        End With

                    Case eMechanismModule.TwoValveOneStage
                        With gSSystemParameter.StageParts
                            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002075, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1).ToString()), "INFO_6002075")
                            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002077, "(StageNo1 ValveNo2) " & .ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve2).ToString()), "INFO_6002077")
                        End With

                End Select
        End Select

        If cboConveyorModel.SelectedIndex <> -1 Then
            gSSystemParameter.ConveyorModel = cboConveyorModel.SelectedIndex
        Else
            gSSystemParameter.ConveyorModel = eConveyorModel.eConveyorNo1
        End If


        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        For stageNo As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
            gSSystemParameter.StageParts.ValveData(stageNo).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (stageNo + 1).ToString & ".ini")
        Next


        cboDispenserNo1ValveType.BackColor = Color.White
        cboDispenserNo2ValveType.BackColor = Color.White
        btnValveType.Enabled = True
    End Sub

    Private Sub cboDispenserNo1ValveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDispenserNo1ValveType.SelectedIndexChanged
        If cboDispenserNo1ValveType.SelectedIndex = -1 Then
            Exit Sub
        End If

        '[Note]:不管怎麼樣第一個肯定都是StageNo1 ValveNo1
        If cboDispenserNo1ValveType.SelectedIndex <> CInt(gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve1)) Then
            cboDispenserNo1ValveType.BackColor = Color.Red
        Else
            cboDispenserNo1ValveType.BackColor = Color.White
        End If

        Select Case cboDispenserNo1ValveType.SelectedIndex
            Case enmValveType.Jet
                cboDispenserNo1ValveTypeModel.Items.Clear()
                cboDispenserNo1ValveTypeModel.Items.Add("PicoPulse")
                cboDispenserNo1ValveTypeModel.Items.Add("Advanjet")
            Case enmValveType.Auger
                cboDispenserNo1ValveTypeModel.Items.Clear()
            Case enmValveType.None
                cboDispenserNo1ValveTypeModel.Items.Clear()
        End Select

    End Sub

    Private Sub cboDispenserNo2ValveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDispenserNo2ValveType.SelectedIndexChanged
        If cboDispenserNo2ValveType.SelectedIndex = -1 Then
            Exit Sub
        End If

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If cboDispenserNo2ValveType.SelectedIndex <> CInt(gSSystemParameter.StageParts.ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1)) Then
                    cboDispenserNo2ValveType.BackColor = Color.Red
                Else
                    cboDispenserNo2ValveType.BackColor = Color.White
                End If
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If cboDispenserNo2ValveType.SelectedIndex <> CInt(gSSystemParameter.StageParts.ValveData(enmStage.No2).ValveType(eValveWorkMode.Valve1)) Then
                    cboDispenserNo2ValveType.BackColor = Color.Red
                Else
                    cboDispenserNo2ValveType.BackColor = Color.White
                End If
            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        '[Note]:不可能發生
                    Case eMechanismModule.TwoValveOneStage
                        If cboDispenserNo2ValveType.SelectedIndex <> CInt(gSSystemParameter.StageParts.ValveData(enmStage.No1).ValveType(eValveWorkMode.Valve2)) Then
                            cboDispenserNo2ValveType.BackColor = Color.Red
                        Else
                            cboDispenserNo2ValveType.BackColor = Color.White
                        End If
                End Select
        End Select

        Select Case cboDispenserNo2ValveType.SelectedIndex
            Case enmValveType.Jet
                cboDispenserNo2ValveTypeModel.Items.Clear()
                cboDispenserNo2ValveTypeModel.Items.Add("PicoPulse")
                cboDispenserNo2ValveTypeModel.Items.Add("Advanjet")

            Case enmValveType.Auger
                cboDispenserNo2ValveTypeModel.Items.Clear()

            Case enmValveType.None
                cboDispenserNo2ValveTypeModel.Items.Clear()

        End Select

    End Sub

    Private Sub cboDispenserNo3ValveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDispenserNo3ValveType.SelectedIndexChanged
        If cboDispenserNo3ValveType.SelectedIndex = -1 Then
            Exit Sub
        End If
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If cboDispenserNo3ValveType.SelectedIndex <> CInt(gSSystemParameter.StageParts.ValveData(enmStage.No3).ValveType(eValveWorkMode.Valve1)) Then
                    cboDispenserNo3ValveType.BackColor = Color.Red
                Else
                    cboDispenserNo3ValveType.BackColor = Color.White
                End If
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                '[Note]:不可能發生

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        '[Note]:不可能發生

                    Case eMechanismModule.TwoValveOneStage
                        '[Note]:不可能發生

                End Select
        End Select

        Select Case cboDispenserNo3ValveType.SelectedIndex
            Case enmValveType.Jet
                cboDispenserNo3ValveTypeModel.Items.Clear()
                cboDispenserNo3ValveTypeModel.Items.Add("PicoPulse")
                cboDispenserNo3ValveTypeModel.Items.Add("Advanjet")
            Case enmValveType.Auger
                cboDispenserNo3ValveTypeModel.Items.Clear()
            Case enmValveType.None
                cboDispenserNo3ValveTypeModel.Items.Clear()
        End Select
    End Sub

    Private Sub cboDispenserNo4ValveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDispenserNo4ValveType.SelectedIndexChanged
        If cboDispenserNo4ValveType.SelectedIndex = -1 Then
            Exit Sub
        End If

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If cboDispenserNo4ValveType.SelectedIndex <> CInt(gSSystemParameter.StageParts.ValveData(enmStage.No4).ValveType(eValveWorkMode.Valve1)) Then
                    cboDispenserNo4ValveType.BackColor = Color.Red
                Else
                    cboDispenserNo4ValveType.BackColor = Color.White
                End If
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                '[Note]:不可能發生

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        '[Note]:不可能發生

                    Case eMechanismModule.TwoValveOneStage
                        '[Note]:不可能發生

                End Select
        End Select

        Select Case cboDispenserNo4ValveType.SelectedIndex
            Case enmValveType.Jet
                cboDispenserNo4ValveTypeModel.Items.Clear()
                cboDispenserNo4ValveTypeModel.Items.Add("PicoPulse")
                cboDispenserNo4ValveTypeModel.Items.Add("Advanjet")
            Case enmValveType.Auger
                cboDispenserNo4ValveTypeModel.Items.Clear()
            Case enmValveType.None
                cboDispenserNo4ValveTypeModel.Items.Clear()
        End Select
    End Sub

#End Region



    Private Sub btnMechanismModule_Click(sender As Object, e As EventArgs) Handles btnMechanismModule.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnMechanismModule]" & vbTab & "Click")
        If btnMechanismModule.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        btnMechanismModule.Enabled = False
        Dim mToolTip As New ToolTip

        '[說明]:紀錄按之前後的狀態
        With gSSystemParameter
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002042), "INFO_6002042")
            gSyslog.Save("StageUseValveCount: " & "(" & CInt(.StageUseValveCount) & ")" & .StageUseValveCount.ToString())
            gSyslog.Save("CCD Module: " & "(" & CInt(.CCDModuleType) & ")" & .CCDModuleType.ToString())
            gSyslog.Save("Measure Z Type: " & "(" & CInt(.MeasureType) & ")" & .MeasureType.ToString())
            gSyslog.Save("CCDOnFlyEnable: " & "(" & CBool(.CCDOnFlyEnable) & ")" & .CCDOnFlyEnable.ToString())
            gSyslog.Save("MultiDispenseEnable: " & "(" & CBool(.MultiDispenseEnable) & ")" & .MultiDispenseEnable.ToString())
        End With

        With gSSystemParameter
            .StageUseValveCount = cboStageUseValveCount.SelectedIndex
            .CCDModuleType = cboCCDModuleType.SelectedIndex
            .MeasureType = cboZHeightDevice.SelectedIndex
            '20170418 飛拍硬體by系統參數設定
            .CCDOnFlyEnable = CInt(cboCCDOnFly.SelectedIndex)
            .MultiDispenseEnable = CInt(cboMultiDispense.SelectedIndex)
        End With

        '[說明]:紀錄按之前後的狀態
        With gSSystemParameter
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002043), "INFO_6002043")
            gSyslog.Save("StageUseValveCount: " & "(" & CInt(.StageUseValveCount) & ")" & .StageUseValveCount.ToString())
            gSyslog.Save("CCD Module: " & "(" & CInt(.CCDModuleType) & ")" & .CCDModuleType.ToString())
            gSyslog.Save("Measure Z Type: " & "(" & CInt(.MeasureType) & ")" & .MeasureType.ToString())
            gSyslog.Save("CCDOnFlyEnable: " & "(" & CBool(.CCDOnFlyEnable) & ")" & .CCDOnFlyEnable.ToString())
            gSyslog.Save("MultiDispenseEnable: " & "(" & CBool(.MultiDispenseEnable) & ")" & .MultiDispenseEnable.ToString())
        End With

        'Sue 20170523
        If cboConveyorModel.SelectedIndex <> -1 Then
            gSSystemParameter.ConveyorModel = cboConveyorModel.SelectedIndex
        Else
            gSSystemParameter.ConveyorModel = eConveyorModel.eConveyorNo1
        End If

        gSSystemParameter.SaveHardwareParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        cboStageUseValveCount.BackColor = Color.White
        cboCCDModuleType.BackColor = Color.White
        cboZHeightDevice.BackColor = Color.White

        mToolTip.AutomaticDelay = 100
        mToolTip.SetToolTip(cboStageUseValveCount, "Preset value is : " & gSSystemParameter.StageUseValveCount.ToString())
        mToolTip.SetToolTip(cboCCDModuleType, "Preset value is : " & gSSystemParameter.CCDModuleType.ToString())
        mToolTip.SetToolTip(cboZHeightDevice, "Preset value is : " & gSSystemParameter.MeasureType.ToString())
        mToolTip = Nothing
        btnMechanismModule.Enabled = True

    End Sub


    Private Sub btnAugerValveCTThreshold_Click(sender As Object, e As EventArgs) Handles btnAugerValveCTThreshold.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnAugerValveCTThreshold]" & vbTab & "Click")
        If btnAugerValveCTThreshold.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If

        Dim mToolTip As New ToolTip

        btnAugerValveCTThreshold.Enabled = False

        '[說明]:紀錄按之前後的狀態
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                With gSSystemParameter.StageParts
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo2 ValveNo1) " & .ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo3 ValveNo1) " & .ValveData(enmStage.No3).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo4 ValveNo1) " & .ValveData(enmStage.No4).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                    .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1) = CDbl(txtAugerValveCTThreshold1.Text)
                    .ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1) = CDbl(txtAugerValveCTThreshold2.Text)
                    .ValveData(enmStage.No3).CTThreshold(eValveWorkMode.Valve1) = CDbl(txtAugerValveCTThreshold3.Text)
                    .ValveData(enmStage.No4).CTThreshold(eValveWorkMode.Valve1) = CDbl(txtAugerValveCTThreshold4.Text)
                End With

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                With gSSystemParameter.StageParts
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo2 ValveNo1) " & .ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                    .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1) = CDbl(txtAugerValveCTThreshold1.Text)
                    .ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1) = CDbl(txtAugerValveCTThreshold2.Text)
                End With


            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        With gSSystemParameter.StageParts
                            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                            .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1) = CDbl(txtAugerValveCTThreshold1.Text)
                        End With


                    Case eMechanismModule.TwoValveOneStage
                        With gSSystemParameter.StageParts
                            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo1 ValveNo2) " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve2).ToString()), "INFO_6002048")
                            .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1) = CDbl(txtAugerValveCTThreshold1.Text)
                            .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve2) = CDbl(txtAugerValveCTThreshold2.Text)
                        End With

                End Select
        End Select

        '[說明]:紀錄按之前後的狀態
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                With gSSystemParameter.StageParts
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo2 ValveNo1) " & .ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo3 ValveNo1) " & .ValveData(enmStage.No3).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo4 ValveNo1) " & .ValveData(enmStage.No4).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                End With


            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                With gSSystemParameter.StageParts
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo2 ValveNo1) " & .ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                End With


            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        With gSSystemParameter.StageParts
                            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                        End With


                    Case eMechanismModule.TwoValveOneStage
                        With gSSystemParameter.StageParts
                            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo1 ValveNo1) " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString()), "INFO_6002048")
                            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002048, "(StageNo1 ValveNo2) " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve2).ToString()), "INFO_6002048")
                        End With

                End Select
        End Select


        For mStageNo As enmStage = enmStage.No1 To gSSystemParameter.StageCount - 1
            gSSystemParameter.StageParts.ValveData(mStageNo).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (mStageNo + 1).ToString & ".ini")
        Next


        'gSSystemParameter.StageParts.ValveData(enmStage.No2).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (enmStage.No2 + 1).ToString & ".ini")
        'gSSystemParameter.StageParts.ValveData(enmStage.No3).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (enmStage.No3 + 1).ToString & ".ini")
        'gSSystemParameter.StageParts.ValveData(enmStage.No4).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (enmStage.No4 + 1).ToString & ".ini")

        mToolTip.AutomaticDelay = 100
        txtAugerValveCTThreshold1.BackColor = Color.White
        txtAugerValveCTThreshold2.BackColor = Color.White


        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                With gSSystemParameter.StageParts
                    mToolTip.SetToolTip(txtAugerValveCTThreshold1, "Preset value is : " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString())
                    mToolTip.SetToolTip(txtAugerValveCTThreshold2, "Preset value is : " & .ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1).ToString())
                    mToolTip.SetToolTip(txtAugerValveCTThreshold3, "Preset value is : " & .ValveData(enmStage.No3).CTThreshold(eValveWorkMode.Valve1).ToString())
                    mToolTip.SetToolTip(txtAugerValveCTThreshold4, "Preset value is : " & .ValveData(enmStage.No4).CTThreshold(eValveWorkMode.Valve1).ToString())
                End With


            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                With gSSystemParameter.StageParts
                    mToolTip.SetToolTip(txtAugerValveCTThreshold1, "Preset value is : " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString())
                    mToolTip.SetToolTip(txtAugerValveCTThreshold2, "Preset value is : " & .ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1).ToString())
                End With


            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        With gSSystemParameter.StageParts
                            mToolTip.SetToolTip(txtAugerValveCTThreshold1, "Preset value is : " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString())
                            mToolTip.SetToolTip(txtAugerValveCTThreshold2, "Preset value is : " & .ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1).ToString())
                        End With


                    Case eMechanismModule.TwoValveOneStage
                        With gSSystemParameter.StageParts
                            mToolTip.SetToolTip(txtAugerValveCTThreshold1, "Preset value is : " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString())
                            mToolTip.SetToolTip(txtAugerValveCTThreshold2, "Preset value is : " & .ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve2).ToString())
                        End With

                End Select
        End Select
        mToolTip = Nothing
        btnAugerValveCTThreshold.Enabled = True
    End Sub

    Private Sub txtAugerValveCTThreshold2_TextChanged(sender As Object, e As EventArgs) Handles txtAugerValveCTThreshold2.TextChanged
        Dim mToolTip As New ToolTip
        mToolTip.AutomaticDelay = 100
        If IsNothing(gSSystemParameter.StageParts.ValveData(enmStage.No2)) = True Then
            mToolTip = Nothing
            Exit Sub
        End If
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If txtAugerValveCTThreshold2.Text <> gSSystemParameter.StageParts.ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1).ToString() Then
                    txtAugerValveCTThreshold2.BackColor = Color.Red
                Else
                    txtAugerValveCTThreshold2.BackColor = Color.White
                End If
                mToolTip.SetToolTip(txtAugerValveCTThreshold2, "Preset value is : " & gSSystemParameter.StageParts.ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1).ToString())
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                If txtAugerValveCTThreshold2.Text <> gSSystemParameter.StageParts.ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1).ToString() Then
                    txtAugerValveCTThreshold2.BackColor = Color.Red
                Else
                    txtAugerValveCTThreshold2.BackColor = Color.White
                End If
                mToolTip.SetToolTip(txtAugerValveCTThreshold2, "Preset value is : " & gSSystemParameter.StageParts.ValveData(enmStage.No2).CTThreshold(eValveWorkMode.Valve1).ToString())
            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        '[Note]:不可能發生
                    Case eMechanismModule.TwoValveOneStage
                        If txtAugerValveCTThreshold2.Text <> gSSystemParameter.StageParts.ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve2).ToString() Then
                            txtAugerValveCTThreshold2.BackColor = Color.Red
                        Else
                            txtAugerValveCTThreshold2.BackColor = Color.White
                        End If
                        mToolTip.SetToolTip(txtAugerValveCTThreshold2, "Preset value is : " & gSSystemParameter.StageParts.ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve2).ToString())
                End Select
        End Select
        mToolTip = Nothing
    End Sub

    Private Sub txtAugerValveCTThreshold1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAugerValveCTThreshold1.KeyPress, txtAugerValveCTThreshold3.KeyPress
        Premtek.Base.CKeyPress.CheckUInteger(sender, e)
    End Sub

    Private Sub txtAugerValveCTThreshold2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAugerValveCTThreshold2.KeyPress, txtAugerValveCTThreshold4.KeyPress
        If (ChrW(Asc(".")) <= e.KeyChar And e.KeyChar <= ChrW(Asc("9"))) Or e.KeyChar = ChrW(8) Then '只準0~9 & backspace
            Exit Sub
        Else
            e.KeyChar = vbNullChar
        End If
    End Sub

    Private Sub btnPreviousPage_Click(sender As Object, e As EventArgs) Handles btnPreviousPage.Click
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnPreviousPage]" & vbTab & "Click")
        '[說明]:因為事件有機會會漏掉所以強制觸發會比較保險
        Me.OnDeactivate(e)
    End Sub


    Private Sub txtSystemCycleRun_KeyPress(sender As Object, e As KeyPressEventArgs)
        If (ChrW(Asc(".")) <= e.KeyChar And e.KeyChar <= ChrW(Asc("9"))) Or e.KeyChar = ChrW(8) Then '只準0~9 & backspace
            Exit Sub
        Else
            e.KeyChar = vbNullChar
        End If
    End Sub

    'Private Sub txtSystemCycleRun_TextChanged(sender As Object, e As EventArgs)
    '    Dim mToolTip As New ToolTip

    '    If txtSystemCycleRun.Text <> gSSystemParameter.intSystemRunCycle.ToString() Then
    '        txtSystemCycleRun.BackColor = Color.Red
    '    Else
    '        txtSystemCycleRun.BackColor = Color.White
    '    End If

    '    mToolTip.AutomaticDelay = 100
    '    mToolTip.SetToolTip(txtSystemCycleRun, "Preset value is : " & gSSystemParameter.intSystemRunCycle.ToString())
    '    mToolTip = Nothing
    'End Sub



    'Private Sub btnAutoTuningSystemCycleRun_Click(sender As Object, e As EventArgs)
    '    gSyslog.Save("[frmSystemSet]" & vbTab & "[btnAutoTuningSystemCycleRun]" & vbTab & "Click")
    '    '[說明]:紀錄按了哪些按鈕
    '    'Call WriteButtonLog(gUserLevel, "frmSystemSet", "btnAutoTuningSystemCycleRun")
    '    '[說明]:紀錄按之前後的狀態
    '    'Call WriteParameterLog(False, CInt(gSSystemParameter.blnAutoTuningSystemCycleRun))
    '    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002050, CInt(gSSystemParameter.blnAutoTuningSystemCycleRun)), "INFO_6002050")
    '    gSSystemParameter.blnAutoTuningSystemCycleRun = Not gSSystemParameter.blnAutoTuningSystemCycleRun
    '    'ShowEnableDisable(btnAutoTuningSystemCycleRun, gSSystemParameter.blnAutoTuningSystemCycleRun)
    '    gSyslog.Save(gMsgHandler.GetMessage(INFO_6002051, CInt(gSSystemParameter.blnAutoTuningSystemCycleRun)), "INFO_6002051")
    '    '[說明]:紀錄按之前後的狀態
    '    gSSystemParameter.SaveStableTime(Application.StartupPath & "\system\" & MachineName & "\SysStable.ini") '儲存穩定時間(by機型)
    'End Sub

    'Private Sub btnAutoTuningUPH_Click(sender As Object, e As EventArgs)
    '    gSyslog.Save("[frmSystemSet]" & vbTab & "[btnAutoTuningUPH]" & vbTab & "Click")
    '    btnAutoTuningUPH.Enabled = False
    '    Dim mToolTip As New ToolTip

    '    If btnAutoTuningUPH.Enabled = True Then
    '        btnAutoTuningUPH.Enabled = False
    '        gSyslog.Save("Old Auto Cycle Time: " & gSSystemParameter.intSystemRunCycle & "ms")
    '        With gSSystemParameter
    '            .intSystemRunCycle = CInt(txtSystemCycleRun.Text)
    '        End With

    '        '[說明]:紀錄按之前後的狀態
    '        gSyslog.Save("New Auto Cycle Time: " & gSSystemParameter.intSystemRunCycle & "ms")
    '        gSSystemParameter.SaveStableTime(Application.StartupPath & "\system\" & MachineName & "\SysStable.ini") '儲存穩定時間(by機型)
    '        btnAutoTuningUPH.Enabled = True
    '    End If

    '    mToolTip.AutomaticDelay = 100
    '    txtSystemCycleRun.BackColor = Color.White

    '    mToolTip.SetToolTip(txtSystemCycleRun, "Preset value is : " & gSSystemParameter.intSystemRunCycle.ToString())

    '    mToolTip = Nothing
    '    btnAutoTuningUPH.Enabled = True
    'End Sub

    Private Sub cboMachineType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMachineType.SelectedIndexChanged, ComboBox1.SelectedIndexChanged
        Dim mToolTip As New ToolTip

        If cboMachineType.SelectedIndex <> -1 Then
            If cboMachineType.SelectedIndex <> CInt(gSSystemParameter.MachineType) Then
                cboMachineType.BackColor = Color.Red
            Else
                cboMachineType.BackColor = Color.White
            End If

            mToolTip.AutomaticDelay = 100
            mToolTip.SetToolTip(cboMachineType, "Preset value is : " & gSSystemParameter.MachineType.ToString())
            mToolTip = Nothing
        End If

    End Sub

    Private Sub cboCCDModuleType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCCDModuleType.SelectedIndexChanged
        Dim mToolTip As New ToolTip

        If cboCCDModuleType.SelectedIndex <> -1 Then
            If cboCCDModuleType.SelectedIndex <> CInt(gSSystemParameter.CCDModuleType) Then
                cboCCDModuleType.BackColor = Color.Red
            Else
                cboCCDModuleType.BackColor = Color.White
            End If

            mToolTip.AutomaticDelay = 100
            mToolTip.SetToolTip(cboCCDModuleType, "Preset value is : " & gSSystemParameter.CCDModuleType.ToString())
            mToolTip = Nothing
        End If
    End Sub

    Private Sub cboLanguageType_SelectedIndexChanged(sender As Object, e As EventArgs)
        If cboLanguageType.SelectedIndex = -1 Then
            Exit Sub
        End If

        If cboLanguageType.SelectedIndex <> CInt(gSSystemParameter.LanguageType) Then
            cboLanguageType.BackColor = Color.Red
        Else
            cboLanguageType.BackColor = Color.White
        End If

    End Sub

    Private Sub btnSetSafeTemp_Click(sender As Object, e As EventArgs) Handles btnSetSafeTemp.Click
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnSetSafeTemp]" & vbTab & "Click")
        btnSetSafeTemp.Enabled = False
        gSSystemParameter.SafeTemperature = nmcSafeTemperature.Value
        gSSystemParameter.ValveMaxTemperature = nmcMaxValveTemp.Value
        gSSystemParameter.HotplateMaxTemperature = nmcMaxHotplateTemp.Value
        '20160920
        gSSystemParameter.HotplateMinTemperature = nmcMinHotplateTemp.Value

        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        btnSetSafeTemp.Enabled = True
    End Sub

    Private Sub btnDisableConveyor_Click(sender As Object, e As EventArgs) Handles btnDisableConveyor.Click
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnDisableConveyor]" & vbTab & "Click")
        '[說明]:紀錄按了哪些按鈕
        '[說明]:紀錄按之前後的狀態
        btnDisableConveyor.Enabled = False
        gSSystemParameter.IsBypassConveyor = Not gSSystemParameter.IsBypassConveyor
        ShowEnableDisable(btnDisableConveyor, Not gSSystemParameter.IsBypassConveyor)
        If gSSystemParameter.IsBypassConveyor = True Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6001000), "INFO_6001000")
        Else
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6001001), "INFO_6001001")
        End If

        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        btnDisableConveyor.Enabled = True
    End Sub


#Region "膠量檢查開關"
    ''' <summary>
    ''' 顯示功能開關
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Sub ShowEnableDisable(ByRef sender As Button, ByVal value As Boolean)
        If value = False Then
            sender.Text = GetString("Disabled") '"Disabled"
            sender.BackColor = Color.Yellow
        Else
            sender.Text = GetString("Enabled") '"Enabled"
            sender.BackColor = SystemColors.Control
            sender.UseVisualStyleBackColor = True
        End If
    End Sub
    ''' <summary>功能開關與存檔</summary>
    ''' <param name="sender"></param>
    ''' <param name="valveNo"></param>
    ''' <remarks></remarks>
    Sub ReverseDetectGlue(ByRef sender As Button, ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode)
        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.StageParts.ValveData(stageNo).EnableDetectPaste(valveNo) = Not gSSystemParameter.StageParts.ValveData(stageNo).EnableDetectPaste(valveNo)
        ShowEnableDisable(sender, gSSystemParameter.StageParts.ValveData(stageNo).EnableDetectPaste(valveNo))
        If gSSystemParameter.StageParts.ValveData(stageNo).EnableDetectPaste(valveNo) Then
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6001005), "INFO_6001005")
        Else
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6001004), "INFO_6001004")
        End If
        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.StageParts.ValveData(stageNo).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (stageNo + 1).ToString & ".ini")
    End Sub
    Private Sub btnBypassDetectGlueNo1_Click(sender As Object, e As EventArgs) Handles btnBypassDetectGlueNo1.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnBypassDetectGlueNo1]" & vbTab & "Click")
        '[說明]:紀錄按之前後的狀態
        '[Note]:基本上第一組肯定是StageNo1 ValveNo1
        ReverseDetectGlue(btnBypassDetectGlueNo1, enmStage.No1, eValveWorkMode.Valve1)
    End Sub

    Private Sub btnBypassDetectGlueNo2_Click(sender As Object, e As EventArgs) Handles btnBypassDetectGlueNo2.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnBypassDetectGlueNo2]" & vbTab & "Click")
        '[說明]:紀錄按之前後的狀態
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                ReverseDetectGlue(btnBypassDetectGlueNo2, enmStage.No2, eValveWorkMode.Valve1)

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                ReverseDetectGlue(btnBypassDetectGlueNo2, enmStage.No2, eValveWorkMode.Valve1)

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        '[Note]:基本上不可能發生

                    Case eMechanismModule.TwoValveOneStage
                        ReverseDetectGlue(btnBypassDetectGlueNo2, enmStage.No1, eValveWorkMode.Valve2)

                End Select
        End Select

    End Sub

    Private Sub btnBypassDetectGlueNo3_Click(sender As Object, e As EventArgs) Handles btnBypassDetectGlueNo3.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnBypassDetectGlueNo3]" & vbTab & "Click")
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                ReverseDetectGlue(btnBypassDetectGlueNo3, enmStage.No3, eValveWorkMode.Valve1)

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                '[Note]:基本上不可能發生

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        '[Note]:基本上不可能發生

                    Case eMechanismModule.TwoValveOneStage
                        '[Note]:基本上不可能發生

                End Select
        End Select
    End Sub

    Private Sub btnBypassDetectGlueNo4_Click(sender As Object, e As EventArgs) Handles btnBypassDetectGlueNo4.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnBypassDetectGlueNo4]" & vbTab & "Click")
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                ReverseDetectGlue(btnBypassDetectGlueNo4, enmStage.No4, eValveWorkMode.Valve1)

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                '[Note]:基本上不可能發生

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        '[Note]:基本上不可能發生

                    Case eMechanismModule.TwoValveOneStage
                        '[Note]:基本上不可能發生

                End Select
        End Select
    End Sub
#End Region

    ''' <summary>氣壓單位設定</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSetAirPressureUnit_Click(sender As Object, e As EventArgs)
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnSetAirPressureUnit]" & vbTab & "Click")
        btnSetAirPressureUnit.Enabled = False
        If rdbBar.Checked = True Then
            gSSystemParameter.AirPressureUnit = enmAirPressureUnit.Bar
        ElseIf rdbKg.Checked = True Then
            gSSystemParameter.AirPressureUnit = enmAirPressureUnit.Kgcm2
        ElseIf rdbMPa.Checked = True Then
            gSSystemParameter.AirPressureUnit = enmAirPressureUnit.MPa
        ElseIf rdbPsi.Checked = True Then
            gSSystemParameter.AirPressureUnit = enmAirPressureUnit.psi
        ElseIf rdbTorr.Checked = True Then
            gSSystemParameter.AirPressureUnit = enmAirPressureUnit.Torr
        End If
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        btnSetAirPressureUnit.Enabled = True
    End Sub

    Private Sub btnSetPositionUnit_Click(sender As Object, e As EventArgs)
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnSetPositionUnit]" & vbTab & "Click")
        btnSetPositionUnit.Enabled = False
        If rdbmm.Checked = True Then
            gSSystemParameter.PositionUnit = enmPositionUnit.mm
        ElseIf rdbInch.Checked = True Then
            gSSystemParameter.PositionUnit = enmPositionUnit.Inch
        End If
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        btnSetPositionUnit.Enabled = True
    End Sub

    Private Sub btnLogPath_Click(sender As Object, e As EventArgs) Handles btnLogPath.Click
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnLogPath]" & vbTab & "Click")
        btnLogPath.Enabled = False
        Dim dialog As New FolderBrowserDialog
        Dim allDriver() As DriveInfo = DriveInfo.GetDrives()
        Dim d As DriveInfo
        If dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtLogPath.Text = dialog.SelectedPath
        End If

        For Each d In allDriver
            If txtLogPath.Text.StartsWith(d.Name) Then
                'CD-ROM為5
                If d.DriveType = DriveType.CDRom Then
                    txtLogPath.Text = ""
                    MsgBox("請選擇其他存放位置")
                End If

            End If
        Next

        btnLogPath.Enabled = True
    End Sub

    Private Sub btnSetMachine_Click(sender As Object, e As EventArgs) Handles btnSetMachine.Click
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnSetMachine]" & vbTab & "Click")
        btnSetMachine.Enabled = False
        If IsIllegalFolderName(txtLogPath.Text) Or txtLogPath.Text = "" Then 'Soni + 2016.09.14 存檔時不良檔名保謢
            '存檔失敗 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(gMsgHandler.GetMessage(Warn_3000035), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Log Folder Path is Illegal. File Not Saved!", vbOKOnly)
            btnSetMachine.Enabled = True
            Exit Sub
        Else
            gSSystemParameter.LogFolderPath = txtLogPath.Text
        End If

        gSSystemParameter.MachineID = txtMachineID.Text
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        btnSetMachine.Enabled = True
    End Sub

    Private Sub btnPassLUL_Click(sender As Object, e As EventArgs) Handles btnPassLUL.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnMappingData]" & vbTab & "Click")
        btnPassLUL.Enabled = False
        gSSystemParameter.PassLUL = Not gSSystemParameter.PassLUL
        mGlobalPool.cls800AQ_LUL.IsLoaderPass = gSSystemParameter.PassLUL
        mGlobalPool.cls800AQ_LUL.IsUnloaderPass = gSSystemParameter.PassLUL
        ShowEnableDisable(btnPassLUL, Not gSSystemParameter.PassLUL)

        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        btnPassLUL.Enabled = True
    End Sub

    Private Sub chkIsReset_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsReset.CheckedChanged
        gSyslog.Save("[frmSystemSet]" & vbTab & "[chkIsReset]" & vbTab & "Checked")
        chkIsReset.Enabled = False

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                gSSystemParameter.StageParts.Purge(enmStage.No1).IsReset(eValveWorkMode.Valve1) = chkIsReset.Checked
                gSSystemParameter.StageParts.Purge(enmStage.No2).IsReset(eValveWorkMode.Valve1) = chkIsReset.Checked
                gSSystemParameter.StageParts.Purge(enmStage.No3).IsReset(eValveWorkMode.Valve1) = chkIsReset.Checked
                gSSystemParameter.StageParts.Purge(enmStage.No4).IsReset(eValveWorkMode.Valve1) = chkIsReset.Checked

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                gSSystemParameter.StageParts.Purge(enmStage.No1).IsReset(eValveWorkMode.Valve1) = chkIsReset.Checked
                gSSystemParameter.StageParts.Purge(enmStage.No2).IsReset(eValveWorkMode.Valve1) = chkIsReset.Checked

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        gSSystemParameter.StageParts.Purge(enmStage.No1).IsReset(eValveWorkMode.Valve1) = chkIsReset.Checked

                    Case eMechanismModule.TwoValveOneStage
                        gSSystemParameter.StageParts.Purge(enmStage.No1).IsReset(eValveWorkMode.Valve1) = chkIsReset.Checked
                        gSSystemParameter.StageParts.Purge(enmStage.No1).IsReset(eValveWorkMode.Valve2) = chkIsReset.Checked

                End Select
        End Select

        For mStageNo As enmStage = enmStage.No1 To gSSystemParameter.StageCount - 1
            gSSystemParameter.StageParts.Purge(mStageNo).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigStage" & (mStageNo + 1).ToString & ".ini", "Purge")
        Next
        chkIsReset.Enabled = True
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click, btnBack2.Click, btnBack3.Click
        Me.Close()
    End Sub

    Private Sub btnSetTime_Click(sender As Object, e As EventArgs) Handles btnSetTime.Click
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnSetSafeTemp]" & vbTab & "Click")
        btnSetSafeTemp.Enabled = False
        gSSystemParameter.StableTime.CCDStableTime = nmcCCDStableTime.Value
        gSSystemParameter.StableTime.LaserStableTime = nmcLaserStableTime.Value
        gSSystemParameter.StableTime.CheckSensorTimeout = nmcSensorTimeOut.Value
        '20161206
        gSSystemParameter.StableTime.PriorHeatTime = nmcPriorHeatTime.Value

        gSSystemParameter.StableTime.Save(Application.StartupPath & "\System\" & MachineName & "\SysStable.ini") '儲存穩定時間(by機型)
        btnSetSafeTemp.Enabled = True
    End Sub

    Private Sub btnCCDImagePath_Click(sender As Object, e As EventArgs) Handles btnCCDImagePath.Click
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnCCDImagePath]" & vbTab & "Click")
        btnCCDImagePath.Enabled = False
        Dim dialog As New FolderBrowserDialog
        Dim allDriver() As DriveInfo = DriveInfo.GetDrives()
        Dim d As DriveInfo
        If dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtCCDImageFolderPath.Text = dialog.SelectedPath
        End If

        For Each d In allDriver
            If txtCCDImageFolderPath.Text.StartsWith(d.Name) Then
                'CD-ROM為5
                If d.DriveType = DriveType.CDRom Then
                    txtCCDImageFolderPath.Text = ""
                    MsgBox("請選擇其他存放位置")
                End If

            End If
        Next
      
        btnCCDImagePath.Enabled = True
    End Sub

    Private Sub btnSaveImage_Click(sender As Object, e As EventArgs) Handles btnSaveImage.Click
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnSaveImage]" & vbTab & "Click")
        btnSetMachine.Enabled = False
        If IsIllegalFolderName(txtCCDImageFolderPath.Text) Or txtCCDImageFolderPath.Text = "" Then 'Soni + 2016.09.14 存檔時不良檔名保謢
            '存檔失敗 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(gMsgHandler.GetMessage(Warn_3000035), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("CCD Image Folder Path is Illegal. File Not Saved!", vbOKOnly)
            btnSetMachine.Enabled = True
            Exit Sub
        Else
            gSSystemParameter.CCDImageFolderPath = txtCCDImageFolderPath.Text
        End If

        Select Case cmbCCDImageSaveMode.SelectedIndex
            Case -1, 0
                gSSystemParameter.CCDImageSaveMode = eCCDImageProcess.None
            Case 1
                gSSystemParameter.CCDImageSaveMode = eCCDImageProcess.SaveNG
            Case 2
                gSSystemParameter.CCDImageSaveMode = eCCDImageProcess.SaveAll
        End Select

        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        btnSetMachine.Enabled = True
    End Sub

    Private Sub btnSaveLanguage_Click(sender As Object, e As EventArgs) Handles btnSaveLanguage.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnSaveLanguage]" & vbTab & "Click")
        If btnSaveLanguage.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        btnSaveLanguage.Enabled = False
        Dim mToolTip As New ToolTip

        '[說明]:紀錄按之前後的狀態
        With gSSystemParameter
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002042), "INFO_6002042")
            gSyslog.Save("Language:      " & "(" & CInt(.LanguageType) & ")" & .LanguageType.ToString())
        End With

        ' 改變語系'
        If gSSystemParameter.LanguageType <> cboLanguageType.SelectedIndex Then
            gSSystemParameter.LanguageType = cboLanguageType.SelectedIndex
            ChangeUILanguage(gSSystemParameter.LanguageType)
        End If

        With gSSystemParameter
            .LanguageType = cboLanguageType.SelectedIndex
        End With

        '[說明]:紀錄按之前後的狀態
        With gSSystemParameter
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002043), "INFO_6002043")
            gSyslog.Save("Language:      " & "(" & CInt(.LanguageType) & ")" & .LanguageType.ToString())
        End With


        '20161010
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")


        SaveMachineType()
        cboLanguageType.BackColor = Color.White

        mToolTip.SetToolTip(cboLanguageType, "Preset value is : " & gSSystemParameter.LanguageType.ToString())
        mToolTip = Nothing

        btnSaveLanguage.Enabled = True
    End Sub

    Private Sub btnSaveMachineType_Click(sender As Object, e As EventArgs) Handles btnSaveMachineType.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnSaveMachineType]" & vbTab & "Click")
        If btnSaveMachineType.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If
        btnSaveMachineType.Enabled = False

        Dim mToolTip As New ToolTip

        '[說明]:紀錄按之前後的狀態
        With gSSystemParameter
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002042), "INFO_6002042")
            gSyslog.Save("MachineType:   " & "(" & CInt(.MachineType) & ")" & .MachineType.ToString())
        End With

        With gSSystemParameter
            .MachineType = cboMachineType.SelectedIndex
        End With

        '[說明]:紀錄按之前後的狀態
        With gSSystemParameter
            gSyslog.Save(gMsgHandler.GetMessage(INFO_6002043), "INFO_6002043")
            gSyslog.Save("MachineType:   " & "(" & CInt(.MachineType) & ")" & .MachineType.ToString())
        End With

        SaveMachineType()
        mToolTip.AutomaticDelay = 100
        cboMachineType.BackColor = Color.White
        mToolTip.SetToolTip(cboMachineType, "Preset value is : " & gSSystemParameter.MachineType.ToString())
        mToolTip = Nothing
        '儲存完成!請重啟程式
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000051))
        MsgBox(gMsgHandler.GetMessage(Warn_3000051), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        'MsgBox("Change Machine Type need to Restart System.Mechine Type has Changed, System is Closing...", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End
        btnSaveMachineType.Enabled = True
    End Sub


    '20161116
    '============================================================================================================================
    Private Sub btnCleanWeight_Click(sender As Object, e As EventArgs) Handles btnCleanWeightA.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[CleanWeightA]" & vbTab & "Click")

        txtWeightmeasurementA.Text = "0"
        gSSystemParameter.WeightMeasureA = 0

        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
    End Sub

    Private Sub cbEnableWeightMeasure_CheckStateChanged(sender As Object, e As EventArgs) Handles cbEnableAWeightMeasure.CheckStateChanged
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[EnableWeightMeasureA]" & vbTab & "Click")

        gSSystemParameter.EnableWeightMeasureA = cbEnableAWeightMeasure.Checked

        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
    End Sub




    Private Sub btnCleanWeightB_Click(sender As Object, e As EventArgs) Handles btnCleanWeightB.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[CleanWeightB]" & vbTab & "Click")

        txtWeightmeasurementB.Text = "0"
        gSSystemParameter.WeightMeasureB = 0

        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
    End Sub


    Private Sub cbEnableBWeightMeasure_CheckStateChanged(sender As Object, e As EventArgs) Handles cbEnableBWeightMeasure.CheckStateChanged
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[EnableWeightMeasureB]" & vbTab & "Click")

        gSSystemParameter.EnableWeightMeasureB = cbEnableBWeightMeasure.Checked

        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
    End Sub
    '============================================================================================================================
    '20161116
    Private Sub btnWeightSet_Click(sender As Object, e As EventArgs) Handles btnWeightSet.Click



        If txtWeighingSet.Text > 0 Then
            gSSystemParameter.WeightSet = txtWeighingSet.Text
            gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        Else
            'Dim str As String
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'str = "Weighing number is < 0!!!"

            'MessageBox.Show(str)
        End If
    End Sub


    Public Sub UpdateComboxB()
        cboB1.Items.Clear()
        cboB2.Items.Clear()
        cboB3.Items.Clear()
        cboB4.Items.Clear()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                cboB1.Visible = True
                cboB2.Visible = True
                cboB3.Visible = True
                cboB4.Visible = True
                lbTiltStage1.Visible = True
                lbTiltStage2.Visible = True
                lbTiltStage3.Visible = True
                lbTiltStage4.Visible = True
                rb1.Visible = True
                rb2.Visible = True
                rb3.Visible = True
                rb4.Visible = True
                '[Note]:使用該組Stage的第一組閥
                sys1.SelectValve = eValveWorkMode.Valve1
                For i As Integer = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(sys1.StageNo).DicCCDTiltValveCalib(sys1.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
                    cboB1.Items.Add(Format(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys1.StageNo).DicCCDTiltValveCalib(sys1.SelectValve).Keys(i), "#0.000").ToString)
                Next
                For i = 0 To cboB1.Items.Count - 1
                    If CDec(cboB1.Items(i).ToString) = gSSystemParameter.Stage1TiltAngle Then
                        cboB1.SelectedIndex = i
                    End If
                Next
                '[Note]:使用該組Stage的第一組閥
                sys2.SelectValve = eValveWorkMode.Valve1
                For i As Integer = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(sys2.StageNo).DicCCDTiltValveCalib(sys2.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
                    cboB2.Items.Add(Format(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys2.StageNo).DicCCDTiltValveCalib(sys2.SelectValve).Keys(i), "#0.000").ToString)
                Next
                For i = 0 To cboB2.Items.Count - 1
                    If CDec(cboB2.Items(i).ToString) = gSSystemParameter.Stage2TiltAngle.ToString Then
                        cboB2.SelectedIndex = i
                    End If
                Next
                '[Note]:使用該組Stage的第一組閥
                sys3.SelectValve = eValveWorkMode.Valve1
                For i As Integer = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(sys3.StageNo).DicCCDTiltValveCalib(sys3.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
                    cboB3.Items.Add(Format(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys3.StageNo).DicCCDTiltValveCalib(sys3.SelectValve).Keys(i), "#0.000").ToString)
                Next
                For i = 0 To cboB3.Items.Count - 1
                    If CDec(cboB3.Items(i).ToString) = gSSystemParameter.Stage3TiltAngle.ToString Then
                        cboB3.SelectedIndex = i
                    End If
                Next
                '[Note]:使用該組Stage的第一組閥
                sys4.SelectValve = eValveWorkMode.Valve1
                For i As Integer = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(sys4.StageNo).DicCCDTiltValveCalib(sys4.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
                    cboB4.Items.Add(Format(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys4.StageNo).DicCCDTiltValveCalib(sys4.SelectValve).Keys(i), "#0.000").ToString)
                Next
                For i = 0 To cboB4.Items.Count - 1
                    If CDec(cboB4.Items(i).ToString) = gSSystemParameter.Stage4TiltAngle.ToString Then
                        cboB4.SelectedIndex = i
                    End If
                Next

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                cboB1.Visible = True
                cboB2.Visible = True
                cboB3.Visible = False
                cboB4.Visible = False
                lbTiltStage1.Visible = True
                lbTiltStage2.Visible = True
                lbTiltStage3.Visible = False
                lbTiltStage4.Visible = False
                rb1.Visible = True
                rb2.Visible = True
                rb3.Visible = False
                rb4.Visible = False
                '[Note]:使用該組Stage的第一組閥
                sys1.SelectValve = eValveWorkMode.Valve1
                For i As Integer = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(sys1.StageNo).DicCCDTiltValveCalib(sys1.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
                    cboB1.Items.Add(Format(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys1.StageNo).DicCCDTiltValveCalib(sys1.SelectValve).Keys(i), "#0.000").ToString)
                Next
                For i = 0 To cboB1.Items.Count - 1
                    If CDec(cboB1.Items(i).ToString) = gSSystemParameter.Stage1TiltAngle Then
                        cboB1.SelectedIndex = i
                    End If
                Next
                '[Note]:使用該組Stage的第一組閥
                sys2.SelectValve = eValveWorkMode.Valve1
                For i As Integer = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(sys2.StageNo).DicCCDTiltValveCalib(sys2.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
                    cboB2.Items.Add(Format(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys2.StageNo).DicCCDTiltValveCalib(sys2.SelectValve).Keys(i), "#0.000").ToString)
                Next
                For i = 0 To cboB2.Items.Count - 1
                    If CDec(cboB2.Items(i).ToString) = gSSystemParameter.Stage2TiltAngle.ToString Then
                        cboB2.SelectedIndex = i
                    End If
                Next

            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        cboB1.Visible = True
                        cboB2.Visible = False
                        cboB3.Visible = False
                        cboB4.Visible = False
                        lbTiltStage1.Visible = True
                        lbTiltStage2.Visible = False
                        lbTiltStage3.Visible = False
                        lbTiltStage4.Visible = False
                        rb1.Visible = True
                        rb2.Visible = False
                        rb3.Visible = False
                        rb4.Visible = False
                        '[Note]:使用該組Stage的第一組閥
                        sys1.SelectValve = eValveWorkMode.Valve1
                        For i As Integer = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(sys1.StageNo).DicCCDTiltValveCalib(sys1.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
                            cboB1.Items.Add(Format(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys1.StageNo).DicCCDTiltValveCalib(sys1.SelectValve).Keys(i), "#0.000").ToString)
                        Next
                        For i = 0 To cboB1.Items.Count - 1
                            If CDec(cboB1.Items(i).ToString) = gSSystemParameter.Stage1TiltAngle.ToString Then
                                cboB1.SelectedIndex = i
                            End If
                        Next

                    Case eMechanismModule.TwoValveOneStage
                        cboB1.Visible = True
                        cboB2.Visible = True
                        cboB3.Visible = False
                        cboB4.Visible = False
                        lbTiltStage1.Visible = True
                        lbTiltStage2.Visible = True
                        lbTiltStage3.Visible = False
                        lbTiltStage4.Visible = False
                        rb1.Visible = True
                        rb2.Visible = True
                        rb3.Visible = False
                        rb4.Visible = False
                        '[Note]:使用該組Stage的第一組閥
                        sys1.SelectValve = eValveWorkMode.Valve1
                        For i As Integer = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(sys1.StageNo).DicCCDTiltValveCalib(sys1.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
                            cboB1.Items.Add(Format(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys1.StageNo).DicCCDTiltValveCalib(sys1.SelectValve).Keys(i), "#0.000").ToString)
                        Next
                        For i = 0 To cboB1.Items.Count - 1
                            If CDec(cboB1.Items(i).ToString) = gSSystemParameter.Stage1TiltAngle Then
                                cboB1.SelectedIndex = i
                            End If
                        Next
                        '[Note]:使用該組Stage的第一組閥
                        sys1.SelectValve = eValveWorkMode.Valve2
                        For i As Integer = 0 To gSSystemParameter.Pos.CCDTiltVavleCalbration(sys1.StageNo).DicCCDTiltValveCalib(sys1.SelectValve).Keys.Count - 1   '對每個角度Key, 加入清單選項
                            cboB2.Items.Add(Format(gSSystemParameter.Pos.CCDTiltVavleCalbration(sys1.StageNo).DicCCDTiltValveCalib(sys1.SelectValve).Keys(i), "#0.000").ToString)
                        Next
                        For i = 0 To cboB2.Items.Count - 1
                            If CDec(cboB2.Items(i).ToString) = gSSystemParameter.Stage2TiltAngle.ToString Then
                                cboB2.SelectedIndex = i
                            End If
                        Next

                End Select

        End Select
    End Sub

    Private Sub btnSetTilt_Click(sender As Object, e As EventArgs) Handles btnSetTilt.Click
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If cboB1.SelectedIndex <> -1 And cboB2.SelectedIndex <> -1 And cboB3.SelectedIndex <> -1 And cboB4.SelectedIndex <> -1 Then
                    gSSystemParameter.Stage1TiltAngle = CDec(cboB1.SelectedItem)
                    gSSystemParameter.Stage2TiltAngle = CDec(cboB2.SelectedItem)
                    gSSystemParameter.Stage3TiltAngle = CDec(cboB3.SelectedItem)
                    gSSystemParameter.Stage4TiltAngle = CDec(cboB4.SelectedItem)
                    gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
                Else
                    '請選擇項目
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000063))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000063), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MessageBox.Show("Select Error")
                End If

            Case enmMachineType.DCS_F230A
                If cboB1.SelectedIndex <> -1 Then
                    gSSystemParameter.Stage1TiltAngle = CDec(cboB1.SelectedItem)
                    gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
                Else
                    '請選擇項目
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000063))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000063), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MessageBox.Show("Select Error")
                End If

            Case enmMachineType.DCS_350A
                If cboB1.SelectedIndex <> -1 Then
                    gSSystemParameter.Stage1TiltAngle = CDec(cboB1.SelectedItem)
                    gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
                Else
                    '請選擇項目
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000063))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000063), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MessageBox.Show("Select Error")
                End If

        End Select
    End Sub

    Private Sub btnGoTilt_Click(sender As Object, e As EventArgs) Handles btnGoTilt.Click
        btnGoTilt.Enabled = False

        Dim AxisNo(1) As Integer
        Dim TargetPos(1) As Decimal

        If rb1.Checked Then
            AxisNo(0) = gSYS(eSys.DispStage1).AxisZ
            AxisNo(1) = gSYS(eSys.DispStage1).AxisB
            TargetPos(0) = 0
            TargetPos(1) = CDec(Val(cboB1.SelectedItem))
            If gSYS(eSys.DispStage1).AxisB <> -1 Then
                ButtonSafeMoveTiltPos(sender, AxisNo, TargetPos, gSYS(eSys.DispStage1))
            Else
                '沒有Tilt軸
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080022))
                MsgBox(gMsgHandler.GetMessage(Alarm_2080022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MessageBox.Show("None Tilt Axis")
            End If
        ElseIf rb2.Checked Then
            AxisNo(0) = gSYS(eSys.DispStage2).AxisZ
            AxisNo(1) = gSYS(eSys.DispStage2).AxisB
            TargetPos(0) = 0
            TargetPos(1) = CDec(Val(cboB2.SelectedItem))
            If gSYS(eSys.DispStage1).AxisB <> -1 Then
                ButtonSafeMoveTiltPos(sender, AxisNo, TargetPos, gSYS(eSys.DispStage2))
            Else
                '沒有Tilt軸
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080022))
                MsgBox(gMsgHandler.GetMessage(Alarm_2080022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MessageBox.Show("None Tilt Axis")
            End If
        ElseIf rb3.Checked Then
            AxisNo(0) = gSYS(eSys.DispStage3).AxisZ
            AxisNo(1) = gSYS(eSys.DispStage3).AxisB
            TargetPos(0) = 0
            TargetPos(1) = CDec(Val(cboB3.SelectedItem))
            If gSYS(eSys.DispStage1).AxisB <> -1 Then
                ButtonSafeMoveTiltPos(sender, AxisNo, TargetPos, gSYS(eSys.DispStage3))
            Else
                '沒有Tilt軸
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080022))
                MsgBox(gMsgHandler.GetMessage(Alarm_2080022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MessageBox.Show("None Tilt Axis")
            End If
        ElseIf rb4.Checked Then
            AxisNo(0) = gSYS(eSys.DispStage4).AxisZ
            AxisNo(1) = gSYS(eSys.DispStage4).AxisB
            TargetPos(0) = 0
            TargetPos(1) = CDec(Val(cboB4.SelectedItem))
            If gSYS(eSys.DispStage1).AxisB <> -1 Then
                ButtonSafeMoveTiltPos(sender, AxisNo, TargetPos, gSYS(eSys.DispStage4))
            Else
                '沒有Tilt軸
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080022))
                MsgBox(gMsgHandler.GetMessage(Alarm_2080022), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'MessageBox.Show("None Tilt Axis")
            End If
        End If

        btnGoTilt.Enabled = True
    End Sub

    Private Sub nmcPriorHeatTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles nmcPriorHeatTime.KeyPress, nmcSensorTimeOut.KeyPress, nmcLaserStableTime.KeyPress, nmcCCDStableTime.KeyPress
        If Char.IsDigit(e.KeyChar) Or e.KeyChar = Chr(8) Then
            '20160901
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

    Private Sub cbEnableInitialHotPlate_CheckedChanged(sender As Object, e As EventArgs) Handles cbEnableInitialHotPlate.CheckedChanged
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[EnableInitialHotPlate]" & vbTab & "Click")
        gSSystemParameter.EnableInitialHotPlate = cbEnableInitialHotPlate.Checked
        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
    End Sub

    Private Sub cbNonCorrection_CheckedChanged(sender As Object, e As EventArgs) Handles cbNonCorrection.CheckedChanged
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[NonCorrection]" & vbTab & "Click")

        gSSystemParameter.NonCorrection = cbNonCorrection.Checked

        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
    End Sub

    Private Sub cbCorrection_CheckedChanged(sender As Object, e As EventArgs) Handles cbCorrection.CheckedChanged
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[Correction]" & vbTab & "Click")
        gSSystemParameter.Correction = cbCorrection.Checked
        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
    End Sub

    Private Sub btnManualMap_Click(sender As Object, e As EventArgs) Handles btnManualMap.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnManualMap]" & vbTab & "Click")
        btnManualMap.Enabled = False
        gSSystemParameter.IsManualMap = Not gSSystemParameter.IsManualMap
        ShowEnableDisable(btnManualMap, gSSystemParameter.IsManualMap)
        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        btnManualMap.Enabled = True
    End Sub
    'Sue 20170523
    Private Sub btnContinueRun_Click(sender As Object, e As EventArgs) Handles btnContinueRun.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnContinueRun]" & vbTab & "Click")
        btnContinueRun.Enabled = False
        gSSystemParameter.IsContinueLastRun = Not gSSystemParameter.IsContinueLastRun
        ShowEnableDisable(btnContinueRun, gSSystemParameter.IsContinueLastRun)
        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        btnContinueRun.Enabled = True
    End Sub
    Private Sub btnCorrectionSet_Click(sender As Object, e As EventArgs) Handles btnCorrectionSet.Click
        If txtCorrectionUSL.Text >= 0 And txtCorrectionLSL.Text >= 0 And txtCorrectionWeightUpper.Text >= 0 And txtCorrectionWeightLower.Text >= 0 And txtCorrectionSet.Text > 0 Then
            gSSystemParameter.CorrectionLSL = txtCorrectionLSL.Text
            gSSystemParameter.CorrectionUSL = txtCorrectionUSL.Text
            gSSystemParameter.CorrectionWeightUpper = txtCorrectionWeightUpper.Text
            gSSystemParameter.CorrectionWeightLower = txtCorrectionWeightLower.Text
            gSSystemParameter.CorrectionNum = txtCorrectionSet.Text
            gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        Else
            'Dim str As String
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'str = "USL ,LSL  number ,WeightUpper,WeightLower is <= 0!!!"

            'MessageBox.Show(str)
        End If
    End Sub

    'Private Sub chkIsContinueRun_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsContinueRun.CheckedChanged
    '    gSyslog.Save("[frmSystemSet]" & vbTab & "[chkIsContinueRun]" & vbTab & "Checked")
    '    chkIsContinueRun.Enabled = False
    '    gSSystemParameter.IsContinueLastRun = chkIsContinueRun.Checked
    '    gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
    '    chkIsContinueRun.Enabled = True
    'End Sub

    Private Sub txtAugerValveCTThreshold1_TextChanged(sender As Object, e As EventArgs) Handles txtAugerValveCTThreshold1.TextChanged
        Dim mToolTip As New ToolTip
        mToolTip.AutomaticDelay = 100
        If IsNothing(gSSystemParameter.StageParts.ValveData(enmStage.No1)) = True Then
            mToolTip = Nothing
            Exit Sub
        End If
        '[Note]:基本上肯定是StageNo1 ValveNo1
        If txtAugerValveCTThreshold1.Text <> gSSystemParameter.StageParts.ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString() Then
            txtAugerValveCTThreshold1.BackColor = Color.Red
        Else
            txtAugerValveCTThreshold1.BackColor = Color.White
        End If
        mToolTip.SetToolTip(txtAugerValveCTThreshold1, "Preset value is : " & gSSystemParameter.StageParts.ValveData(enmStage.No1).CTThreshold(eValveWorkMode.Valve1).ToString())
        mToolTip = Nothing
    End Sub

    Private Sub txtAugerValveCTThreshold3_TextChanged(sender As Object, e As EventArgs) Handles txtAugerValveCTThreshold3.TextChanged
        Dim mToolTip As New ToolTip
        mToolTip.AutomaticDelay = 100
        If IsNothing(gSSystemParameter.StageParts.ValveData(enmStage.No3)) = True Then
            mToolTip = Nothing
            Exit Sub
        End If
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If txtAugerValveCTThreshold3.Text <> gSSystemParameter.StageParts.ValveData(enmStage.No3).CTThreshold(eValveWorkMode.Valve1).ToString() Then
                    txtAugerValveCTThreshold3.BackColor = Color.Red
                Else
                    txtAugerValveCTThreshold3.BackColor = Color.White
                End If
                mToolTip.SetToolTip(txtAugerValveCTThreshold3, "Preset value is : " & gSSystemParameter.StageParts.ValveData(enmStage.No3).CTThreshold(eValveWorkMode.Valve1).ToString())
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                '[Note]:不可能發生
            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        '[Note]:不可能發生
                    Case eMechanismModule.TwoValveOneStage
                        '[Note]:不可能發生
                End Select
        End Select
        mToolTip = Nothing
    End Sub

    Private Sub txtAugerValveCTThreshold4_TextChanged(sender As Object, e As EventArgs) Handles txtAugerValveCTThreshold4.TextChanged
        Dim mToolTip As New ToolTip
        mToolTip.AutomaticDelay = 100
        If IsNothing(gSSystemParameter.StageParts.ValveData(enmStage.No4)) = True Then
            mToolTip = Nothing
            Exit Sub
        End If
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                If txtAugerValveCTThreshold4.Text <> gSSystemParameter.StageParts.ValveData(enmStage.No4).CTThreshold(eValveWorkMode.Valve1).ToString() Then
                    txtAugerValveCTThreshold4.BackColor = Color.Red
                Else
                    txtAugerValveCTThreshold4.BackColor = Color.White
                End If
                mToolTip.SetToolTip(txtAugerValveCTThreshold4, "Preset value is : " & gSSystemParameter.StageParts.ValveData(enmStage.No4).CTThreshold(eValveWorkMode.Valve1).ToString())
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                '[Note]:不可能發生
            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        '[Note]:不可能發生
                    Case eMechanismModule.TwoValveOneStage
                        '[Note]:不可能發生
                End Select
        End Select
        mToolTip = Nothing
    End Sub

    Private Sub btnSetCvDirection_Click(sender As Object, e As EventArgs) Handles btnSetCvDirection.Click
        Dim loadDirection As WetcoConveyor.IRoller.enmDirection
        Dim unloadDirection As WetcoConveyor.IRoller.enmDirection

        If (rbtnLoadForward.Checked) Then
            loadDirection = WetcoConveyor.IRoller.enmDirection.Forward
            gSSystemParameter.LoadDirection = True
        Else
            loadDirection = WetcoConveyor.IRoller.enmDirection.Reversal
            gSSystemParameter.LoadDirection = False
        End If

        If (rbtnUnloadForward.Checked) Then
            unloadDirection = WetcoConveyor.IRoller.enmDirection.Forward
            gSSystemParameter.UnloadDirection = True
        Else
            unloadDirection = WetcoConveyor.IRoller.enmDirection.Reversal
            gSSystemParameter.UnloadDirection = False
        End If

        'If cboConveyorModel.SelectedIndex <> -1 Then
        '    gSSystemParameter.ConveyorModel = cboConveyorModel.SelectedIndex
        'Else
        '    gSSystemParameter.ConveyorModel = eConveyorModel.eConveyorNo1
        'End If
        If Unit IsNot Nothing Then
            If Unit.A_Roller IsNot Nothing Then
                WetcoConveyor.mGlobalPool.Unit.A_Roller.SetDirection(loadDirection, unloadDirection)
            End If
            If Unit.B_Roller IsNot Nothing Then
                WetcoConveyor.mGlobalPool.Unit.B_Roller.SetDirection(loadDirection, unloadDirection)
            End If
        End If

        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
    End Sub

    Private Sub btnTolerance_Click(sender As Object, e As EventArgs) Handles btnTolerance.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnTolerance]" & vbTab & "Click")
        If btnTolerance.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If

        btnTolerance.Enabled = False
        Dim mToolTip As New ToolTip

        '[說明]:紀錄按之前後的狀態
        With gSSystemParameter
            gSyslog.Save("Tolerance: " & .MotionTolerance & " , " & .TriggerTolerance & " , " & .PressureTolerance & " , " & .TemperatureTolerance)
        End With

        With gSSystemParameter
            .PrecisionTolerance = CDbl(txtPrecisionTolerance.Text)
            .TriggerTolerance = CDbl(txtTirggerTolerance.Text)
            .MotionTolerance = CDbl(txtMotionTolerance.Text)
            .PressureTolerance = CDbl(txtPressureTolerance.Text)
            .TemperatureTolerance = CDbl(txtTemperatureTolerance.Text)
        End With

        '[說明]:紀錄按之前後的狀態
        With gSSystemParameter
            gSyslog.Save("Tolerance: " & .MotionTolerance & " , " & .TriggerTolerance & " , " & .PressureTolerance & " , " & .TemperatureTolerance)
        End With
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\system\" & MachineName & "\SysParam.ini") '儲存系統閥參數(by機型)

        mToolTip.AutomaticDelay = 100
        txtMotionTolerance.BackColor = Color.White
        txtTirggerTolerance.BackColor = Color.White
        txtPressureTolerance.BackColor = Color.White
        txtTemperatureTolerance.BackColor = Color.White
        mToolTip.SetToolTip(txtMotionTolerance, "Preset value is : " & gSSystemParameter.MotionTolerance.ToString())
        mToolTip.SetToolTip(txtTirggerTolerance, "Preset value is : " & gSSystemParameter.TriggerTolerance.ToString())
        mToolTip.SetToolTip(txtPrecisionTolerance, "Preset value is : " & gSSystemParameter.PrecisionTolerance.ToString())
        mToolTip.SetToolTip(txtPressureTolerance, "Preset value is : " & gSSystemParameter.PressureTolerance.ToString())
        mToolTip.SetToolTip(txtTemperatureTolerance, "Preset value is : " & gSSystemParameter.TemperatureTolerance.ToString())
        mToolTip = Nothing
        btnTolerance.Enabled = True
    End Sub

    Private Sub cboStageUseValveCount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStageUseValveCount.SelectedIndexChanged
        If cboStageUseValveCount.SelectedIndex = -1 Then
            Exit Sub
        End If

        If cboStageUseValveCount.SelectedIndex <> CInt(gSSystemParameter.StageUseValveCount) Then
            cboStageUseValveCount.BackColor = Color.Red
        Else
            cboStageUseValveCount.BackColor = Color.White
        End If

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                cboDispenserNo1ValveType.Visible = True
                cboDispenserNo2ValveType.Visible = True
                cboDispenserNo3ValveType.Visible = True
                cboDispenserNo4ValveType.Visible = True

                '20161111
                cboDispenserNo1ValveTypeModel.Visible = True
                cboDispenserNo2ValveTypeModel.Visible = True
                cboDispenserNo3ValveTypeModel.Visible = True
                cboDispenserNo4ValveTypeModel.Visible = True

                lblDispenserNo1ValveType.Visible = True
                lblDispenserNo2ValveType.Visible = True
                lblDispenserNo3ValveType.Visible = True
                lblDispenserNo4ValveType.Visible = True

                'cmbValveController1.Visible = True
                'cmbValveController2.Visible = True
                'cmbValveController3.Visible = True
                'cmbValveController4.Visible = True

                'lblValveController1.Visible = True
                'lblValveController2.Visible = True
                'lblValveController3.Visible = True
                'lblValveController4.Visible = True

                btnBypassDetectGlueNo1.Visible = True
                btnBypassDetectGlueNo2.Visible = True
                btnBypassDetectGlueNo3.Visible = True
                btnBypassDetectGlueNo4.Visible = True

                lblGlueDetector1.Visible = True
                lblGlueDetector2.Visible = True
                lblGlueDetector3.Visible = True
                lblGlueDetector4.Visible = True

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                cboDispenserNo1ValveType.Visible = True
                cboDispenserNo2ValveType.Visible = True
                cboDispenserNo3ValveType.Visible = False
                cboDispenserNo4ValveType.Visible = False

                '20161111
                cboDispenserNo1ValveTypeModel.Visible = True
                cboDispenserNo2ValveTypeModel.Visible = True
                cboDispenserNo3ValveTypeModel.Visible = False
                cboDispenserNo4ValveTypeModel.Visible = False

                lblDispenserNo1ValveType.Visible = True
                lblDispenserNo2ValveType.Visible = True
                lblDispenserNo3ValveType.Visible = False
                lblDispenserNo4ValveType.Visible = False

                'cmbValveController1.Visible = True
                'cmbValveController2.Visible = True
                'cmbValveController3.Visible = False
                'cmbValveController4.Visible = False

                'lblValveController1.Visible = True
                'lblValveController2.Visible = True
                'lblValveController3.Visible = False
                'lblValveController4.Visible = False

                btnBypassDetectGlueNo1.Visible = True
                btnBypassDetectGlueNo2.Visible = True
                btnBypassDetectGlueNo3.Visible = False
                btnBypassDetectGlueNo4.Visible = False

                lblGlueDetector1.Visible = True
                lblGlueDetector2.Visible = True
                lblGlueDetector3.Visible = False
                lblGlueDetector4.Visible = False

            Case Else
                Select Case cboStageUseValveCount.SelectedIndex
                    Case CInt(eMechanismModule.OneValveOneStage)
                        cboDispenserNo1ValveType.Visible = True
                        cboDispenserNo2ValveType.Visible = False
                        cboDispenserNo3ValveType.Visible = False
                        cboDispenserNo4ValveType.Visible = False

                        '20161111
                        cboDispenserNo1ValveTypeModel.Visible = True
                        cboDispenserNo2ValveTypeModel.Visible = False
                        cboDispenserNo3ValveTypeModel.Visible = False
                        cboDispenserNo4ValveTypeModel.Visible = False

                        lblDispenserNo1ValveType.Visible = True
                        lblDispenserNo2ValveType.Visible = False
                        lblDispenserNo3ValveType.Visible = False
                        lblDispenserNo4ValveType.Visible = False

                        'cmbValveController1.Visible = True
                        'cmbValveController2.Visible = False
                        'cmbValveController3.Visible = False
                        'cmbValveController4.Visible = False

                        'lblValveController1.Visible = True
                        'lblValveController2.Visible = False
                        'lblValveController3.Visible = False
                        'lblValveController4.Visible = False

                        btnBypassDetectGlueNo1.Visible = True
                        btnBypassDetectGlueNo2.Visible = False
                        btnBypassDetectGlueNo3.Visible = False
                        btnBypassDetectGlueNo4.Visible = False

                        lblGlueDetector1.Visible = True
                        lblGlueDetector2.Visible = False
                        lblGlueDetector3.Visible = False
                        lblGlueDetector4.Visible = False
                    Case CInt(eMechanismModule.TwoValveOneStage)
                        cboDispenserNo1ValveType.Visible = True
                        cboDispenserNo2ValveType.Visible = True
                        cboDispenserNo3ValveType.Visible = False
                        cboDispenserNo4ValveType.Visible = False

                        lblDispenserNo1ValveType.Visible = True
                        lblDispenserNo2ValveType.Visible = True
                        lblDispenserNo3ValveType.Visible = False
                        lblDispenserNo4ValveType.Visible = False

                        '20170321
                        cboDispenserNo1ValveTypeModel.Visible = True
                        cboDispenserNo2ValveTypeModel.Visible = True
                        cboDispenserNo3ValveTypeModel.Visible = False
                        cboDispenserNo4ValveTypeModel.Visible = False

                        'cmbValveController1.Visible = True
                        'cmbValveController2.Visible = True
                        'cmbValveController3.Visible = False
                        'cmbValveController4.Visible = False

                        'lblValveController1.Visible = True
                        'lblValveController2.Visible = True
                        'lblValveController3.Visible = False
                        'lblValveController4.Visible = False

                        btnBypassDetectGlueNo1.Visible = True
                        btnBypassDetectGlueNo2.Visible = True
                        btnBypassDetectGlueNo3.Visible = False
                        btnBypassDetectGlueNo4.Visible = False

                        lblGlueDetector1.Visible = True
                        lblGlueDetector2.Visible = True
                        lblGlueDetector3.Visible = False
                        lblGlueDetector4.Visible = False
                End Select
        End Select
    End Sub
    'jimmy 2017630
    Private Sub btnCleanDevice_Click(sender As Object, e As EventArgs) Handles btnCleanDevice.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnCleanDevice]" & vbTab & "Click")
        btnCleanDevice.Enabled = False
        gSSystemParameter.IsCleanDevice = Not gSSystemParameter.IsCleanDevice
        ShowEnableDisable(btnCleanDevice, gSSystemParameter.IsCleanDevice)
        '[說明]:紀錄按之前後的狀態
        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        btnCleanDevice.Enabled = True
    End Sub

    'jimmy 20170726
    Public Sub SetToolTip()
        Dim toolTip1 As New ToolTip()
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eEnglish
                'page 1
                toolTip1.SetToolTip(cboMachineType, "")
                toolTip1.SetToolTip(cboStageUseValveCount, "")
                toolTip1.SetToolTip(cboCCDModuleType, "")
                toolTip1.SetToolTip(cboZHeightDevice, "")
                toolTip1.SetToolTip(cboMultiDispense, "")
                toolTip1.SetToolTip(cboCCDOnFly, "")
                toolTip1.SetToolTip(cboConveyorModel, "")
                toolTip1.SetToolTip(btnMechanismModule, "")
                toolTip1.SetToolTip(cboDispenserNo1ValveType, "")
                toolTip1.SetToolTip(cboDispenserNo2ValveType, "")
                toolTip1.SetToolTip(cboDispenserNo3ValveType, "")
                toolTip1.SetToolTip(cboDispenserNo4ValveType, "")
                toolTip1.SetToolTip(cboDispenserNo1ValveTypeModel, "")
                toolTip1.SetToolTip(cboDispenserNo2ValveTypeModel, "")
                toolTip1.SetToolTip(cboDispenserNo3ValveTypeModel, "")
                toolTip1.SetToolTip(cboDispenserNo4ValveTypeModel, "")
                toolTip1.SetToolTip(btnValveType, "")
                'page 2
                toolTip1.SetToolTip(cboLanguageType, "")
                toolTip1.SetToolTip(btnSaveLanguage, "")
                toolTip1.SetToolTip(txtMachineID, "")
                toolTip1.SetToolTip(txtLogPath, "")
                toolTip1.SetToolTip(btnLogPath, "")
                toolTip1.SetToolTip(btnSetMachine, "")
                toolTip1.SetToolTip(cmbCCDImageSaveMode, "")
                toolTip1.SetToolTip(txtCCDImageFolderPath, "")
                toolTip1.SetToolTip(btnCCDImagePath, "")
                toolTip1.SetToolTip(btnSaveImage, "")
                'page 3
                toolTip1.SetToolTip(chkIsReset, "")
                toolTip1.SetToolTip(btnBypassDetectGlueNo1, "")
                toolTip1.SetToolTip(btnBypassDetectGlueNo2, "")
                toolTip1.SetToolTip(btnBypassDetectGlueNo3, "")
                toolTip1.SetToolTip(btnBypassDetectGlueNo4, "")
                toolTip1.SetToolTip(btnDisableConveyor, "")
                toolTip1.SetToolTip(btnPassLUL, "")
                toolTip1.SetToolTip(btnManualMap, "")
                toolTip1.SetToolTip(btnContinueRun, "")
                toolTip1.SetToolTip(btnCleanDevice, "")
                toolTip1.SetToolTip(nmcSensorTimeOut, "")
                toolTip1.SetToolTip(nmcLaserStableTime, "")
                toolTip1.SetToolTip(nmcCCDStableTime, "")
                toolTip1.SetToolTip(nmcPriorHeatTime, "")
                toolTip1.SetToolTip(btnSetTime, "")
                toolTip1.SetToolTip(nmcSafeTemperature, "")
                toolTip1.SetToolTip(nmcMaxValveTemp, "")
                toolTip1.SetToolTip(nmcMaxHotplateTemp, "")
                toolTip1.SetToolTip(nmcMinHotplateTemp, "")
                toolTip1.SetToolTip(cbEnableInitialHotPlate, "")
                toolTip1.SetToolTip(btnSetSafeTemp, "")
                toolTip1.SetToolTip(rb1, "")
                toolTip1.SetToolTip(rb2, "")
                toolTip1.SetToolTip(rb3, "")
                toolTip1.SetToolTip(rb4, "")
                toolTip1.SetToolTip(cboB1, "")
                toolTip1.SetToolTip(cboB2, "")
                toolTip1.SetToolTip(cboB3, "")
                toolTip1.SetToolTip(cboB4, "")
                toolTip1.SetToolTip(btnGoTilt, "")
                toolTip1.SetToolTip(btnSetTilt, "")
                toolTip1.SetToolTip(cbEnableAWeightMeasure, "")
                toolTip1.SetToolTip(txtWeightmeasurementA, "")
                toolTip1.SetToolTip(btnCleanWeightA, "")
                toolTip1.SetToolTip(cbEnableBWeightMeasure, "")
                toolTip1.SetToolTip(txtWeightmeasurementB, "")
                toolTip1.SetToolTip(btnCleanWeightB, "")
                toolTip1.SetToolTip(txtWeighingSet, "")
                toolTip1.SetToolTip(btnWeightSet, "")
                toolTip1.SetToolTip(cbNonCorrection, "")
                toolTip1.SetToolTip(txtCorrectionSet, "")
                toolTip1.SetToolTip(cbCorrection, "")
                toolTip1.SetToolTip(txtCorrectionUSL, "")
                toolTip1.SetToolTip(txtCorrectionLSL, "")
                toolTip1.SetToolTip(txtCorrectionWeightUpper, "")
                toolTip1.SetToolTip(txtCorrectionWeightLower, "")
                toolTip1.SetToolTip(btnCorrectionSet, "")
                toolTip1.SetToolTip(txtAugerValveCTThreshold1, "")
                toolTip1.SetToolTip(txtAugerValveCTThreshold2, "")
                toolTip1.SetToolTip(txtAugerValveCTThreshold3, "")
                toolTip1.SetToolTip(txtAugerValveCTThreshold4, "")
                toolTip1.SetToolTip(btnAugerValveCTThreshold, "")
                toolTip1.SetToolTip(rbtnLoadForward, "")
                toolTip1.SetToolTip(rbtnLoadReversal, "")
                toolTip1.SetToolTip(rbtnUnloadForward, "")
                toolTip1.SetToolTip(rbtnUnloadReversal, "")
                toolTip1.SetToolTip(btnSetCvDirection, "")
                toolTip1.SetToolTip(txtMotionTolerance, "")
                toolTip1.SetToolTip(txtTirggerTolerance, "")
                toolTip1.SetToolTip(txtPrecisionTolerance, "")
                toolTip1.SetToolTip(txtPressureTolerance, "")
                toolTip1.SetToolTip(txtTemperatureTolerance, "")
                toolTip1.SetToolTip(btnTolerance, "")
                'page 4

            Case enmLanguageType.eSimplifiedChinese
                'page 1
                toolTip1.SetToolTip(cboMachineType, "")
                toolTip1.SetToolTip(cboStageUseValveCount, "")
                toolTip1.SetToolTip(cboCCDModuleType, "")
                toolTip1.SetToolTip(cboZHeightDevice, "")
                toolTip1.SetToolTip(cboMultiDispense, "")
                toolTip1.SetToolTip(cboCCDOnFly, "")
                toolTip1.SetToolTip(cboConveyorModel, "")
                toolTip1.SetToolTip(btnMechanismModule, "")
                toolTip1.SetToolTip(cboDispenserNo1ValveType, "")
                toolTip1.SetToolTip(cboDispenserNo2ValveType, "")
                toolTip1.SetToolTip(cboDispenserNo3ValveType, "")
                toolTip1.SetToolTip(cboDispenserNo4ValveType, "")
                toolTip1.SetToolTip(cboDispenserNo1ValveTypeModel, "")
                toolTip1.SetToolTip(cboDispenserNo2ValveTypeModel, "")
                toolTip1.SetToolTip(cboDispenserNo3ValveTypeModel, "")
                toolTip1.SetToolTip(cboDispenserNo4ValveTypeModel, "")
                toolTip1.SetToolTip(btnValveType, "")

                'page 2
                toolTip1.SetToolTip(cboLanguageType, "")
                toolTip1.SetToolTip(btnSaveLanguage, "")
                toolTip1.SetToolTip(txtMachineID, "")
                toolTip1.SetToolTip(txtLogPath, "")
                toolTip1.SetToolTip(btnLogPath, "")
                toolTip1.SetToolTip(btnSetMachine, "")
                toolTip1.SetToolTip(cmbCCDImageSaveMode, "")
                toolTip1.SetToolTip(txtCCDImageFolderPath, "")
                toolTip1.SetToolTip(btnCCDImagePath, "")
                toolTip1.SetToolTip(btnSaveImage, "")
                'page 3
                toolTip1.SetToolTip(chkIsReset, "")
                toolTip1.SetToolTip(btnBypassDetectGlueNo1, "")
                toolTip1.SetToolTip(btnBypassDetectGlueNo2, "")
                toolTip1.SetToolTip(btnBypassDetectGlueNo3, "")
                toolTip1.SetToolTip(btnBypassDetectGlueNo4, "")
                toolTip1.SetToolTip(btnDisableConveyor, "")
                toolTip1.SetToolTip(btnPassLUL, "")
                toolTip1.SetToolTip(btnManualMap, "")
                toolTip1.SetToolTip(btnContinueRun, "")
                toolTip1.SetToolTip(btnCleanDevice, "")
                toolTip1.SetToolTip(nmcSensorTimeOut, "")
                toolTip1.SetToolTip(nmcLaserStableTime, "")
                toolTip1.SetToolTip(nmcCCDStableTime, "")
                toolTip1.SetToolTip(nmcPriorHeatTime, "")
                toolTip1.SetToolTip(btnSetTime, "")
                toolTip1.SetToolTip(nmcSafeTemperature, "")
                toolTip1.SetToolTip(nmcMaxValveTemp, "")
                toolTip1.SetToolTip(nmcMaxHotplateTemp, "")
                toolTip1.SetToolTip(nmcMinHotplateTemp, "")
                toolTip1.SetToolTip(cbEnableInitialHotPlate, "")
                toolTip1.SetToolTip(btnSetSafeTemp, "")
                toolTip1.SetToolTip(rb1, "")
                toolTip1.SetToolTip(rb2, "")
                toolTip1.SetToolTip(rb3, "")
                toolTip1.SetToolTip(rb4, "")
                toolTip1.SetToolTip(cboB1, "")
                toolTip1.SetToolTip(cboB2, "")
                toolTip1.SetToolTip(cboB3, "")
                toolTip1.SetToolTip(cboB4, "")
                toolTip1.SetToolTip(btnGoTilt, "")
                toolTip1.SetToolTip(btnSetTilt, "")
                toolTip1.SetToolTip(cbEnableAWeightMeasure, "")
                toolTip1.SetToolTip(txtWeightmeasurementA, "")
                toolTip1.SetToolTip(btnCleanWeightA, "")
                toolTip1.SetToolTip(cbEnableBWeightMeasure, "")
                toolTip1.SetToolTip(txtWeightmeasurementB, "")
                toolTip1.SetToolTip(btnCleanWeightB, "")
                toolTip1.SetToolTip(txtWeighingSet, "")
                toolTip1.SetToolTip(btnWeightSet, "")
                toolTip1.SetToolTip(cbNonCorrection, "")
                toolTip1.SetToolTip(txtCorrectionSet, "")
                toolTip1.SetToolTip(cbCorrection, "")
                toolTip1.SetToolTip(txtCorrectionUSL, "")
                toolTip1.SetToolTip(txtCorrectionLSL, "")
                toolTip1.SetToolTip(txtCorrectionWeightUpper, "")
                toolTip1.SetToolTip(txtCorrectionWeightLower, "")
                toolTip1.SetToolTip(btnCorrectionSet, "")
                toolTip1.SetToolTip(txtAugerValveCTThreshold1, "")
                toolTip1.SetToolTip(txtAugerValveCTThreshold2, "")
                toolTip1.SetToolTip(txtAugerValveCTThreshold3, "")
                toolTip1.SetToolTip(txtAugerValveCTThreshold4, "")
                toolTip1.SetToolTip(btnAugerValveCTThreshold, "")
                toolTip1.SetToolTip(rbtnLoadForward, "")
                toolTip1.SetToolTip(rbtnLoadReversal, "")
                toolTip1.SetToolTip(rbtnUnloadForward, "")
                toolTip1.SetToolTip(rbtnUnloadReversal, "")
                toolTip1.SetToolTip(btnSetCvDirection, "")
                toolTip1.SetToolTip(txtMotionTolerance, "")
                toolTip1.SetToolTip(txtTirggerTolerance, "")
                toolTip1.SetToolTip(txtPrecisionTolerance, "")
                toolTip1.SetToolTip(txtPressureTolerance, "")
                toolTip1.SetToolTip(txtTemperatureTolerance, "")
                toolTip1.SetToolTip(btnTolerance, "")
            Case enmLanguageType.eTraditionalChinese
                'page 1
                toolTip1.SetToolTip(cboMachineType, "")
                toolTip1.SetToolTip(cboStageUseValveCount, "")
                toolTip1.SetToolTip(cboCCDModuleType, "")
                toolTip1.SetToolTip(cboZHeightDevice, "")
                toolTip1.SetToolTip(cboMultiDispense, "")
                toolTip1.SetToolTip(cboCCDOnFly, "")
                toolTip1.SetToolTip(cboConveyorModel, "")
                toolTip1.SetToolTip(btnMechanismModule, "")
                toolTip1.SetToolTip(cboDispenserNo1ValveType, "")
                toolTip1.SetToolTip(cboDispenserNo2ValveType, "")
                toolTip1.SetToolTip(cboDispenserNo3ValveType, "")
                toolTip1.SetToolTip(cboDispenserNo4ValveType, "")
                toolTip1.SetToolTip(cboDispenserNo1ValveTypeModel, "")
                toolTip1.SetToolTip(cboDispenserNo2ValveTypeModel, "")
                toolTip1.SetToolTip(cboDispenserNo3ValveTypeModel, "")
                toolTip1.SetToolTip(cboDispenserNo4ValveTypeModel, "")
                toolTip1.SetToolTip(btnValveType, "")
                'page 2
                toolTip1.SetToolTip(cboLanguageType, "")
                toolTip1.SetToolTip(btnSaveLanguage, "")
                toolTip1.SetToolTip(txtMachineID, "")
                toolTip1.SetToolTip(txtLogPath, "")
                toolTip1.SetToolTip(btnLogPath, "")
                toolTip1.SetToolTip(btnSetMachine, "")
                toolTip1.SetToolTip(cmbCCDImageSaveMode, "")
                toolTip1.SetToolTip(txtCCDImageFolderPath, "")
                toolTip1.SetToolTip(btnCCDImagePath, "")
                toolTip1.SetToolTip(btnSaveImage, "")
                'page 3
                toolTip1.SetToolTip(chkIsReset, "")
                toolTip1.SetToolTip(btnBypassDetectGlueNo1, "")
                toolTip1.SetToolTip(btnBypassDetectGlueNo2, "")
                toolTip1.SetToolTip(btnBypassDetectGlueNo3, "")
                toolTip1.SetToolTip(btnBypassDetectGlueNo4, "")
                toolTip1.SetToolTip(btnDisableConveyor, "")
                toolTip1.SetToolTip(btnPassLUL, "")
                toolTip1.SetToolTip(btnManualMap, "")
                toolTip1.SetToolTip(btnContinueRun, "")
                toolTip1.SetToolTip(btnCleanDevice, "")
                toolTip1.SetToolTip(nmcSensorTimeOut, "")
                toolTip1.SetToolTip(nmcLaserStableTime, "")
                toolTip1.SetToolTip(nmcCCDStableTime, "")
                toolTip1.SetToolTip(nmcPriorHeatTime, "")
                toolTip1.SetToolTip(btnSetTime, "")
                toolTip1.SetToolTip(nmcSafeTemperature, "")
                toolTip1.SetToolTip(nmcMaxValveTemp, "")
                toolTip1.SetToolTip(nmcMaxHotplateTemp, "")
                toolTip1.SetToolTip(nmcMinHotplateTemp, "")
                toolTip1.SetToolTip(cbEnableInitialHotPlate, "")
                toolTip1.SetToolTip(btnSetSafeTemp, "")
                toolTip1.SetToolTip(rb1, "")
                toolTip1.SetToolTip(rb2, "")
                toolTip1.SetToolTip(rb3, "")
                toolTip1.SetToolTip(rb4, "")
                toolTip1.SetToolTip(cboB1, "")
                toolTip1.SetToolTip(cboB2, "")
                toolTip1.SetToolTip(cboB3, "")
                toolTip1.SetToolTip(cboB4, "")
                toolTip1.SetToolTip(btnGoTilt, "")
                toolTip1.SetToolTip(btnSetTilt, "")
                toolTip1.SetToolTip(cbEnableAWeightMeasure, "")
                toolTip1.SetToolTip(txtWeightmeasurementA, "")
                toolTip1.SetToolTip(btnCleanWeightA, "")
                toolTip1.SetToolTip(cbEnableBWeightMeasure, "")
                toolTip1.SetToolTip(txtWeightmeasurementB, "")
                toolTip1.SetToolTip(btnCleanWeightB, "")
                toolTip1.SetToolTip(txtWeighingSet, "")
                toolTip1.SetToolTip(btnWeightSet, "")
                toolTip1.SetToolTip(cbNonCorrection, "")
                toolTip1.SetToolTip(txtCorrectionSet, "")
                toolTip1.SetToolTip(cbCorrection, "")
                toolTip1.SetToolTip(txtCorrectionUSL, "")
                toolTip1.SetToolTip(txtCorrectionLSL, "")
                toolTip1.SetToolTip(txtCorrectionWeightUpper, "")
                toolTip1.SetToolTip(txtCorrectionWeightLower, "")
                toolTip1.SetToolTip(btnCorrectionSet, "")
                toolTip1.SetToolTip(txtAugerValveCTThreshold1, "")
                toolTip1.SetToolTip(txtAugerValveCTThreshold2, "")
                toolTip1.SetToolTip(txtAugerValveCTThreshold3, "")
                toolTip1.SetToolTip(txtAugerValveCTThreshold4, "")
                toolTip1.SetToolTip(btnAugerValveCTThreshold, "")
                toolTip1.SetToolTip(rbtnLoadForward, "")
                toolTip1.SetToolTip(rbtnLoadReversal, "")
                toolTip1.SetToolTip(rbtnUnloadForward, "")
                toolTip1.SetToolTip(rbtnUnloadReversal, "")
                toolTip1.SetToolTip(btnSetCvDirection, "")
                toolTip1.SetToolTip(txtMotionTolerance, "")
                toolTip1.SetToolTip(txtTirggerTolerance, "")
                toolTip1.SetToolTip(txtPrecisionTolerance, "")
                toolTip1.SetToolTip(txtPressureTolerance, "")
                toolTip1.SetToolTip(txtTemperatureTolerance, "")
                toolTip1.SetToolTip(btnTolerance, "")

        End Select

    End Sub


    Private Sub btnSetVelocity_Click(sender As Object, e As EventArgs) Handles btnSetVelocity.Click
        '[說明]:紀錄按了哪些按鈕
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnSetVelocity]" & vbTab & "Click")
        If btnSetVelocity.Enabled = False Then '防連按
            Exit Sub
            '^^^^^^^
        End If

        btnSetVelocity.Enabled = False

        '[說明]:紀錄按之前後的狀態
        With gSSystemParameter
            gSyslog.Save("Velocity: " & .MaxCrossDeviceVelocity & " , " & .MaxCrossStepVelocity & "," & .MaxDispVelocity)
        End With

        With gSSystemParameter
            .CrossVerticalTime = Val(txtConstVelocityTime.Text)
            .MaxCrossStepVelocity = Val(txtMaxCrossStepVelocity.Text)
            .MaxCrossDeviceVelocity = Val(txtMaxCrossDevieVelocity.Text)
            .MaxDispVelocity = Val(txtMaxDispVelocity.Text)
        End With

        '[說明]:紀錄按之前後的狀態
        With gSSystemParameter
            gSyslog.Save("Velocity: " & .MaxCrossDeviceVelocity & " , " & .MaxCrossStepVelocity & "," & .MaxDispVelocity)
        End With
        gSSystemParameter.SaveVelocity(Application.StartupPath & "\system\" & MachineName & "\SysParam.ini") '儲存系統閥參數(by機型)
        txtConstVelocityTime.BackColor = Color.White
        txtMaxCrossStepVelocity.BackColor = Color.White
        MsgBox("Save OK.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        btnSetVelocity.Enabled = True
    End Sub

 

    Private Sub btnExtendOn_Click(sender As Object, e As EventArgs) Handles btnExtendOn.Click
        If txtPathMultiple.Text >= 0 And txtCorrectionLSL.Text <= 5 Then
            gSSystemParameter.PathMultiple = txtPathMultiple.Text
            gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")
        Else
            MsgBox("PathMultiple num is < 0 or > 5.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    Private Sub btnMapPath_Click(sender As Object, e As EventArgs) Handles btnMapPath.Click
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnMapPath]" & vbTab & "Click")
        Dim dialog As New FolderBrowserDialog
        Dim allDriver() As DriveInfo = DriveInfo.GetDrives()
        Dim d As DriveInfo
        If dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtMapDataPath.Text = dialog.SelectedPath
        End If

        For Each d In allDriver
            If txtMapDataPath.Text.StartsWith(d.Name) Then
                If d.DriveType = DriveType.CDRom Then
                    txtMapDataPath.Text = ""
                    MsgBox("請選擇其他存放位置")
                End If
            End If
        Next
       
    End Sub

    Private Sub btnSetMapPath_Click(sender As Object, e As EventArgs) Handles btnSetMapPath.Click
        gSyslog.Save("[frmSystemSet]" & vbTab & "[btnSetMapPath]" & vbTab & "Click")
        If IsIllegalFolderName(txtMapDataPath.Text) Or txtMapDataPath.Text = "" Then '存檔時不良檔名保謢
            '存檔失敗 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(gMsgHandler.GetMessage(Warn_3000035), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        Else
            gSSystemParameter.RerunDataFolderPath = txtMapDataPath.Text.Replace("\", "\\")

            If Not System.IO.Directory.Exists(gSSystemParameter.RerunDataFolderPath) Then
                System.IO.Directory.CreateDirectory(gSSystemParameter.RerunDataFolderPath)
            End If
        End If
    End Sub

    Private Sub cboxMapDataModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboxMapDataModel.SelectedIndexChanged
        gSyslog.Save("[frmSystemSet]" & vbTab & "[cboxMapDataModel]" & vbTab & "SelectedIndexChanged") '[說明]:紀錄按了哪些按鈕

        gSSystemParameter.IsCompareWithMapData = cboxMapDataModel.SelectedIndex

        gSSystemParameter.SaveSystemParameter(Application.StartupPath & "\System\" & MachineName & "\SysParam.ini")   '[說明]:紀錄按之前後的狀態
    End Sub
End Class
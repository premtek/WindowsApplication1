Imports ProjectIO
Imports ProjectCore
Imports ProjectMotion

Public Class frmMotionOption
    Public myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMotionOption))

    ''' <summary>內部索引</summary>
    ''' <remarks></remarks>
    Dim mAxisNo As Integer = -1
    Public Property AxisNo As Integer
        Get
            Return mAxisNo
        End Get
        Set(value As Integer)

            mAxisNo = value
            RefreshUI()

        End Set
    End Property

   

    Sub RefreshUI()
        grpAxisParameter.Text = myResource.GetString("grpAxisParameter.Text") & "(" & gCMotion.AxisParameter(mAxisNo).AxisName & ")"

        txtAxisName.Text = gCMotion.AxisParameter(mAxisNo).AxisName
        SelectNumericUpDown(nmcCardNo, gCMotion.AxisParameter(mAxisNo).CardParameter.ItemNo)
        SelectNumericUpDown(nmcAxisNo, gCMotion.AxisParameter(mAxisNo).CardParameter.AxisNo)

        SelectNumericUpDown(nmcSNEL, gCMotion.AxisParameter(mAxisNo).Limit.NegativeLimit)
        SelectNumericUpDown(nmcSPEL, gCMotion.AxisParameter(mAxisNo).Limit.PosivtiveLimit)

        SelectNumericUpDown(nmcScale, gCMotion.AxisParameter(mAxisNo).Parameter.Scale)
        SelectNumericUpDown(nmcPPU, gCMotion.AxisParameter(mAxisNo).Parameter.PPU)

        SelectNumericUpDown(nmcHomeMode, gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeMode)
        SelectNumericUpDown(nmcHomeCrossDistance, gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeCrossDistance)
        SelectNumericUpDown(nmcHomeExSwitchMode, gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeExSwitchMode)

        SelectNumericUpDown(nmcHomeAcc, gCMotion.AxisParameter(mAxisNo).Velocity.HomeAcc)
        SelectNumericUpDown(nmcHomeDec, gCMotion.AxisParameter(mAxisNo).Velocity.HomeDec)
        SelectNumericUpDown(nmcHomeVelHigh, gCMotion.AxisParameter(mAxisNo).Velocity.HomeVelHigh)
        SelectNumericUpDown(nmcHomeVelLow, gCMotion.AxisParameter(mAxisNo).Velocity.HomeVelLow)
        SelectNumericUpDown(nmcMaxAcc, gCMotion.AxisParameter(mAxisNo).Velocity.MaxAcc)
        SelectNumericUpDown(nmcMaxDec, gCMotion.AxisParameter(mAxisNo).Velocity.MaxDec)
        SelectNumericUpDown(nmcMaxVel, gCMotion.AxisParameter(mAxisNo).Velocity.MaxVel)
        SelectNumericUpDown(nmcVelHigh, gCMotion.AxisParameter(mAxisNo).Velocity.VelHigh)
        SelectNumericUpDown(nmcVelLow, gCMotion.AxisParameter(mAxisNo).Velocity.VelLow)
        SelectNumericUpDown(nmcAcc, gCMotion.AxisParameter(mAxisNo).Velocity.Acc)
        SelectNumericUpDown(nmcDec, gCMotion.AxisParameter(mAxisNo).Velocity.Dec)
        SelectNumericUpDown(nmcAccRatio, gCMotion.AxisParameter(mAxisNo).Velocity.AccRatio)
        SelectNumericUpDown(nmcDecRatio, gCMotion.AxisParameter(mAxisNo).Velocity.DecRatio)

        SelectNumericUpDown(nmcINPStable, gCMotion.AxisParameter(mAxisNo).InpositionStableTime)

        SelectComboBox(cmbCardType, gCMotion.AxisParameter(mAxisNo).CardParameter.CardType)
        SelectComboBox(cmbALMEnable, gCMotion.AxisParameter(mAxisNo).Parameter.AlarmEnable)
        SelectComboBox(cmbALMLogic, gCMotion.AxisParameter(mAxisNo).Parameter.AlarmLogic)
        SelectComboBox(cmbALMStopMode, gCMotion.AxisParameter(mAxisNo).Parameter.AlarmStopMode)
        SelectComboBox(cmbAxisType, gCMotion.AxisParameter(mAxisNo).CardParameter.AxisType)
        SelectComboBox(cmbMotorType, gCMotion.AxisParameter(mAxisNo).Parameter.MotorType)
        SelectComboBox(cmbBacklashEnable, gCMotion.AxisParameter(mAxisNo).Parameter.BacklashEnable)

        SelectComboBox(cmbERCEnable, gCMotion.AxisParameter(mAxisNo).Parameter.ErcEnable)
        SelectComboBox(cmbERCLogic, gCMotion.AxisParameter(mAxisNo).Parameter.ErcLogic)
        SelectComboBox(cmbEZLogic, gCMotion.AxisParameter(mAxisNo).Parameter.EZLogic)
        SelectComboBox(cmbHLMTEnable, gCMotion.AxisParameter(mAxisNo).Limit.HLimitEnable)
        SelectComboBox(cmbHLMTLogic, gCMotion.AxisParameter(mAxisNo).Limit.HLimitLogic)
        SelectComboBox(cmbHLMTStopMode, gCMotion.AxisParameter(mAxisNo).Limit.HLimitStopMode)
        SelectComboBox(cmbHomeDirection, gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeDirection)
        SelectComboBox(cmbHomeReset, gCMotion.AxisParameter(mAxisNo).Parameter.HomeReset)
        SelectComboBox(cmbINPEnable, gCMotion.AxisParameter(mAxisNo).Parameter.INPEnable)
        SelectComboBox(cmbINPLogic, gCMotion.AxisParameter(mAxisNo).Parameter.INPLogic)
        If gCMotion.AxisParameter(mAxisNo).Parameter.IsEncoderExist Then
            SelectComboBox(cmbIsEncoderExist, 1)
        Else
            SelectComboBox(cmbIsEncoderExist, 0)
        End If

        SelectComboBox(cmbLatchEnable, gCMotion.AxisParameter(mAxisNo).Parameter.LatchEnable)
        SelectComboBox(cmbLatchLogic, gCMotion.AxisParameter(mAxisNo).Parameter.LatchLogic)
        SelectComboBox(cmbMoveDirection, gCMotion.AxisParameter(mAxisNo).Parameter.Direction)
        SelectComboBox(cmbORGLogic, gCMotion.AxisParameter(mAxisNo).Parameter.OrgLogic)
        SelectComboBox(cmbPulseInMaxFreq, gCMotion.AxisParameter(mAxisNo).Parameter.PulseInMaxFreq)
        SelectComboBox(cmbPulseInMode, gCMotion.AxisParameter(mAxisNo).Parameter.PulseInMode)
        SelectComboBox(cmbPulseInReverse, gCMotion.AxisParameter(mAxisNo).Parameter.PulseInDirection)
        Select Case gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutMode
            Case enmPulseOutMode.AB_Phase
                SelectComboBox(cmbPulseOutMode, 6)
            Case enmPulseOutMode.BA_Phase
                SelectComboBox(cmbPulseOutMode, 7)
            Case enmPulseOutMode.CW_CCV_CWCCW_NegativeLogic
                SelectComboBox(cmbPulseOutMode, 5)
            Case enmPulseOutMode.CW_CCV_DIR_NegativeLogic
                SelectComboBox(cmbPulseOutMode, 9)
            Case enmPulseOutMode.CW_CCV_OUT_NegativeLogic
                SelectComboBox(cmbPulseOutMode, 8)
            Case enmPulseOutMode.CW_CCW
                SelectComboBox(cmbPulseOutMode, 4)
            Case enmPulseOutMode.OUT_DIR
                SelectComboBox(cmbPulseOutMode, 0)
            Case enmPulseOutMode.OUT_DIR_DIR_NegativeLogic
                SelectComboBox(cmbPulseOutMode, 2)
            Case enmPulseOutMode.OUT_DIR_OUT_NegativeLogic
                SelectComboBox(cmbPulseOutMode, 1)
            Case enmPulseOutMode.OUT_DIR_OUTDIR_NegativeLogic
                SelectComboBox(cmbPulseOutMode, 3)
        End Select

        SelectComboBox(cmbPulseOutReverse, gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutReverse)
        SelectComboBox(cmbButtonDirection, gCMotion.AxisParameter(mAxisNo).Parameter.ButtonDirection)
        SelectComboBox(cmbCoordinate, gCMotion.AxisParameter(mAxisNo).Coordinate)
        SelectComboBox(cmbExternalDriveEnable, gCMotion.AxisParameter(mAxisNo).Parameter.ExternalDriveEnable)
        SelectComboBox(cmbExternalDriveAxis, gCMotion.AxisParameter(mAxisNo).Parameter.ExternalDriveAxis)
        SelectComboBox(cmbExternalPulseInMode, gCMotion.AxisParameter(mAxisNo).Parameter.ExternalDrivePulseInMode)
        SelectComboBox(cmbIN1StopEnable, gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable1)
        SelectComboBox(cmbIN2StopEnable, gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable2)
        SelectComboBox(cmbIN4StopEnable, gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable4)
        SelectComboBox(cmbIN5StopEnable, gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable5)
        SelectComboBox(cmbIN1StopLogic, gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic1)
        SelectComboBox(cmbIN2StopLogic, gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic2)
        SelectComboBox(cmbIN4StopLogic, gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic4)
        SelectComboBox(cmbIN5StopLogic, gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic5)
        SelectComboBox(cmbIN1StopMode, gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode1)
        SelectComboBox(cmbIN2StopMode, gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode2)
        SelectComboBox(cmbIN4StopMode, gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode4)
        SelectComboBox(cmbIN5StopMode, gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode5)

    End Sub

    Sub SelectComboBox(ByRef comboBox As ComboBox, ByVal selectedIndex As Integer)
        If selectedIndex >= comboBox.Items.Count Then '設定值失效, 請重新選擇
            comboBox.BackColor = Color.Red
            Exit Sub
        End If
        If selectedIndex < 0 Then '未選擇設定值
            comboBox.SelectedIndex = -1
            comboBox.BackColor = Color.Red
            Exit Sub
        End If
        comboBox.SelectedIndex = selectedIndex
        comboBox.BackColor = Color.White
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmMotionOption]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Hide()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmMotionOption]" & vbTab & "[btnOK]" & vbTab & "Click")
        gSyslog.Save("Old Axis Parameter (" & gCMotion.AxisParameter(mAxisNo).AxisName & ") Start")
        gSyslog.Save("AxisName: " & gCMotion.AxisParameter(mAxisNo).AxisName)
        gSyslog.Save("Ticket: " & gCMotion.AxisParameter(mAxisNo).CardParameter.ItemNo)
        gSyslog.Save("AxisNo: " & gCMotion.AxisParameter(mAxisNo).CardParameter.AxisNo)
        gSyslog.Save("AxisType: " & gCMotion.AxisParameter(mAxisNo).CardParameter.AxisType)
        gSyslog.Save("CardType: " & gCMotion.AxisParameter(mAxisNo).CardParameter.CardType)
        gSyslog.Save("GroupNo: " & gCMotion.AxisParameter(mAxisNo).CardParameter.GroupNo)
        gSyslog.Save("Coordinate: " & gCMotion.AxisParameter(mAxisNo).Coordinate)

        gSyslog.Save("HomeCrossDistance: " & gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeCrossDistance)
        gSyslog.Save("HomeDirection: " & gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeDirection)
        gSyslog.Save("HomeExSwitchMode: " & gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeExSwitchMode)
        gSyslog.Save("HomeMode: " & gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeMode)
        gSyslog.Save("HomeOffset: " & gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeOffset)
        gSyslog.Save("INP Stable: " & gCMotion.AxisParameter(mAxisNo).InpositionStableTime)

        gSyslog.Save("HLMT Enable: " & gCMotion.AxisParameter(mAxisNo).Limit.HLimitEnable)
        gSyslog.Save("HLMT Logic: " & gCMotion.AxisParameter(mAxisNo).Limit.HLimitLogic)
        gSyslog.Save("HLMT Stop Mode: " & gCMotion.AxisParameter(mAxisNo).Limit.HLimitStopMode)
        gSyslog.Save("SNEL: " & gCMotion.AxisParameter(mAxisNo).Limit.NegativeLimit)
        gSyslog.Save("SPEL: " & gCMotion.AxisParameter(mAxisNo).Limit.PosivtiveLimit)

        gSyslog.Save("AlarmLogic: " & gCMotion.AxisParameter(mAxisNo).Parameter.AlarmLogic)
        gSyslog.Save("AlarmStopMode: " & gCMotion.AxisParameter(mAxisNo).Parameter.AlarmStopMode)
        gSyslog.Save("AlarmEnable: " & gCMotion.AxisParameter(mAxisNo).Parameter.AlarmEnable)
        gSyslog.Save("BacklashEnable: " & gCMotion.AxisParameter(mAxisNo).Parameter.BacklashEnable)
        gSyslog.Save("ButtonDirection: " & gCMotion.AxisParameter(mAxisNo).Parameter.ButtonDirection)
        gSyslog.Save("Direction: " & gCMotion.AxisParameter(mAxisNo).Parameter.Direction)
        gSyslog.Save("ErcEnable: " & gCMotion.AxisParameter(mAxisNo).Parameter.ErcEnable)
        gSyslog.Save("ErcLogic: " & gCMotion.AxisParameter(mAxisNo).Parameter.ErcLogic)
        gSyslog.Save("ExternalDriveAxis: " & gCMotion.AxisParameter(mAxisNo).Parameter.ExternalDriveAxis)
        gSyslog.Save("ExternalDriveEnable: " & gCMotion.AxisParameter(mAxisNo).Parameter.ExternalDriveEnable)
        gSyslog.Save("ExternalDrivePulseInMode: " & gCMotion.AxisParameter(mAxisNo).Parameter.ExternalDrivePulseInMode)
        gSyslog.Save("EZLogic: " & gCMotion.AxisParameter(mAxisNo).Parameter.EZLogic)
        gSyslog.Save("HomeReset: " & gCMotion.AxisParameter(mAxisNo).Parameter.HomeReset)
        gSyslog.Save("INPEnable: " & gCMotion.AxisParameter(mAxisNo).Parameter.INPEnable)
        gSyslog.Save("INPLogic: " & gCMotion.AxisParameter(mAxisNo).Parameter.INPLogic)
        gSyslog.Save("IsEncoderExist: " & gCMotion.AxisParameter(mAxisNo).Parameter.IsEncoderExist)
        gSyslog.Save("LatchEnable: " & gCMotion.AxisParameter(mAxisNo).Parameter.LatchEnable)
        gSyslog.Save("LatchLogic: " & gCMotion.AxisParameter(mAxisNo).Parameter.LatchLogic)
        gSyslog.Save("MotorType: " & gCMotion.AxisParameter(mAxisNo).Parameter.MotorType)
        gSyslog.Save("OrgLogic: " & gCMotion.AxisParameter(mAxisNo).Parameter.OrgLogic)
        gSyslog.Save("PPU: " & gCMotion.AxisParameter(mAxisNo).Parameter.PPU)
        gSyslog.Save("PulseInDirection: " & gCMotion.AxisParameter(mAxisNo).Parameter.PulseInDirection)
        gSyslog.Save("PulseInMaxFreq: " & gCMotion.AxisParameter(mAxisNo).Parameter.PulseInMaxFreq)
        gSyslog.Save("PulseInMode: " & gCMotion.AxisParameter(mAxisNo).Parameter.PulseInMode)
        gSyslog.Save("PulseOutMode: " & gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutMode)
        gSyslog.Save("PulseOutReverse: " & gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutReverse)
        gSyslog.Save("Scale: " & gCMotion.AxisParameter(mAxisNo).Parameter.Scale)
        gSyslog.Save("TriggerStopEnable1: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable1)
        gSyslog.Save("TriggerStopEnable2: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable2)
        gSyslog.Save("TriggerStopEnable4: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable4)
        gSyslog.Save("TriggerStopEnable5: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable5)
        gSyslog.Save("TriggerStopLogic1: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic1)
        gSyslog.Save("TriggerStopLogic2: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic2)
        gSyslog.Save("TriggerStopLogic4: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic4)
        gSyslog.Save("TriggerStopLogic5: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic5)
        gSyslog.Save("TriggerStopMode1: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode1)
        gSyslog.Save("TriggerStopMode2: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode2)
        gSyslog.Save("TriggerStopMode4: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode4)
        gSyslog.Save("TriggerStopMode5: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode5)

        gSyslog.Save("Old Axis Parameter (" & gCMotion.AxisParameter(mAxisNo).AxisName & ") End")

        gCMotion.AxisParameter(mAxisNo).AxisName = txtAxisName.Text
        gCMotion.AxisParameter(mAxisNo).CardParameter.ItemNo = nmcCardNo.Value
        gCMotion.AxisParameter(mAxisNo).CardParameter.AxisNo = nmcAxisNo.Value
        gCMotion.AxisParameter(mAxisNo).CardParameter.AxisType = cmbAxisType.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).CardParameter.CardType = cmbCardType.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Coordinate = cmbCoordinate.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeCrossDistance = nmcHomeCrossDistance.Value
        gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeDirection = cmbHomeDirection.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeExSwitchMode = nmcHomeExSwitchMode.Value
        gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeMode = nmcHomeMode.Value

        gCMotion.AxisParameter(mAxisNo).InpositionStableTime = nmcINPStable.Value
        gCMotion.AxisParameter(mAxisNo).Limit.HLimitEnable = cmbHLMTEnable.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Limit.HLimitLogic = cmbHLMTLogic.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Limit.HLimitStopMode = cmbHLMTStopMode.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Limit.NegativeLimit = nmcSNEL.Value
        gCMotion.AxisParameter(mAxisNo).Limit.PosivtiveLimit = nmcSPEL.Value
        gCMotion.AxisParameter(mAxisNo).Parameter.AlarmLogic = cmbALMLogic.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.AlarmStopMode = cmbALMStopMode.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.AlarmEnable = cmbALMEnable.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.BacklashEnable = cmbBacklashEnable.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.ButtonDirection = cmbButtonDirection.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.Direction = cmbMoveDirection.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.ErcEnable = cmbERCEnable.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.ErcLogic = cmbERCLogic.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.ExternalDriveAxis = cmbExternalDriveAxis.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.ExternalDriveEnable = cmbExternalDriveEnable.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.ExternalDrivePulseInMode = cmbExternalPulseInMode.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.EZLogic = cmbEZLogic.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.HomeReset = cmbHomeReset.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.INPEnable = cmbINPEnable.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.INPLogic = cmbINPLogic.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Velocity.HomeAcc = nmcHomeAcc.Value
        gCMotion.AxisParameter(mAxisNo).Velocity.HomeDec = nmcHomeDec.Value
        gCMotion.AxisParameter(mAxisNo).Velocity.HomeVelHigh = nmcHomeVelHigh.Value
        gCMotion.AxisParameter(mAxisNo).Velocity.HomeVelLow = nmcHomeVelLow.Value
        gCMotion.AxisParameter(mAxisNo).Velocity.Acc = nmcAcc.Value
        gCMotion.AxisParameter(mAxisNo).Velocity.AccRatio = nmcAccRatio.Value
        gCMotion.AxisParameter(mAxisNo).Velocity.Dec = nmcDec.Value
        gCMotion.AxisParameter(mAxisNo).Velocity.DecRatio = nmcDecRatio.Value
        gCMotion.AxisParameter(mAxisNo).Velocity.MaxAcc = nmcMaxAcc.Value
        gCMotion.AxisParameter(mAxisNo).Velocity.MaxDec = nmcMaxDec.Value
        gCMotion.AxisParameter(mAxisNo).Velocity.MaxVel = nmcMaxVel.Value
        gCMotion.AxisParameter(mAxisNo).Velocity.VelHigh = nmcVelHigh.Value
        gCMotion.AxisParameter(mAxisNo).Velocity.VelLow = nmcVelLow.Value

        Select Case cmbIsEncoderExist.SelectedIndex
            Case 0
                gCMotion.AxisParameter(mAxisNo).Parameter.IsEncoderExist = False
            Case 1
                gCMotion.AxisParameter(mAxisNo).Parameter.IsEncoderExist = True
        End Select


        gCMotion.AxisParameter(mAxisNo).Parameter.LatchEnable = cmbLatchEnable.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.LatchLogic = cmbLatchLogic.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.MotorType = cmbMotorType.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.OrgLogic = cmbORGLogic.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.PPU = nmcPPU.Value
        gCMotion.AxisParameter(mAxisNo).Parameter.PulseInDirection = cmbPulseInReverse.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.PulseInMaxFreq = cmbPulseInMaxFreq.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.PulseInMode = cmbPulseInMode.SelectedIndex
        Select Case cmbPulseOutMode.SelectedIndex
            Case 0
                gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutMode = enmPulseOutMode.OUT_DIR
            Case 1
                gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutMode = enmPulseOutMode.OUT_DIR_OUT_NegativeLogic
            Case 2
                gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutMode = enmPulseOutMode.OUT_DIR_DIR_NegativeLogic
            Case 3
                gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutMode = enmPulseOutMode.OUT_DIR_OUTDIR_NegativeLogic
            Case 4
                gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutMode = enmPulseOutMode.CW_CCW
            Case 5
                gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutMode = enmPulseOutMode.CW_CCV_CWCCW_NegativeLogic
            Case 6
                gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutMode = enmPulseOutMode.AB_Phase
            Case 7
                gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutMode = enmPulseOutMode.BA_Phase
            Case 8
                gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutMode = enmPulseOutMode.CW_CCV_OUT_NegativeLogic
            Case 9
                gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutMode = enmPulseOutMode.CW_CCV_DIR_NegativeLogic
        End Select

        gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutReverse = cmbPulseOutReverse.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.Scale = nmcScale.Value
        gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable1 = cmbIN1StopEnable.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable2 = cmbIN2StopEnable.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable4 = cmbIN4StopEnable.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable5 = cmbIN5StopEnable.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic1 = cmbIN1StopLogic.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic2 = cmbIN2StopLogic.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic4 = cmbIN4StopLogic.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic5 = cmbIN5StopLogic.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode1 = cmbIN1StopMode.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode2 = cmbIN2StopMode.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode4 = cmbIN4StopMode.SelectedIndex
        gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode5 = cmbIN5StopMode.SelectedIndex

        gCMotion.AxisParameter(mAxisNo).Save(Application.StartupPath & "\System\" & MachineName & "\ConfigAxis" & mAxisNo & ".ini")

        gSyslog.Save("New Axis Parameter (" & gCMotion.AxisParameter(mAxisNo).AxisName & ") Start")
        gSyslog.Save("AxisName: " & gCMotion.AxisParameter(mAxisNo).AxisName)
        gSyslog.Save("Ticket: " & gCMotion.AxisParameter(mAxisNo).CardParameter.ItemNo)
        gSyslog.Save("AxisNo: " & gCMotion.AxisParameter(mAxisNo).CardParameter.AxisNo)
        gSyslog.Save("AxisType: " & gCMotion.AxisParameter(mAxisNo).CardParameter.AxisType)
        gSyslog.Save("CardType: " & gCMotion.AxisParameter(mAxisNo).CardParameter.CardType)
        gSyslog.Save("GroupNo: " & gCMotion.AxisParameter(mAxisNo).CardParameter.GroupNo)
        gSyslog.Save("Coordinate: " & gCMotion.AxisParameter(mAxisNo).Coordinate)

        gSyslog.Save("HomeCrossDistance: " & gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeCrossDistance)
        gSyslog.Save("HomeDirection: " & gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeDirection)
        gSyslog.Save("HomeExSwitchMode: " & gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeExSwitchMode)
        gSyslog.Save("HomeMode: " & gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeMode)
        gSyslog.Save("HomeOffset: " & gCMotion.AxisParameter(mAxisNo).HomeParameter.HomeOffset)
        gSyslog.Save("INP Stable: " & gCMotion.AxisParameter(mAxisNo).InpositionStableTime)

        gSyslog.Save("HLMT Enable: " & gCMotion.AxisParameter(mAxisNo).Limit.HLimitEnable)
        gSyslog.Save("HLMT Logic: " & gCMotion.AxisParameter(mAxisNo).Limit.HLimitLogic)
        gSyslog.Save("HLMT Stop Mode: " & gCMotion.AxisParameter(mAxisNo).Limit.HLimitStopMode)
        gSyslog.Save("SNEL: " & gCMotion.AxisParameter(mAxisNo).Limit.NegativeLimit)
        gSyslog.Save("SPEL: " & gCMotion.AxisParameter(mAxisNo).Limit.PosivtiveLimit)

        gSyslog.Save("AlarmLogic: " & gCMotion.AxisParameter(mAxisNo).Parameter.AlarmLogic)
        gSyslog.Save("AlarmStopMode: " & gCMotion.AxisParameter(mAxisNo).Parameter.AlarmStopMode)
        gSyslog.Save("AlarmEnable: " & gCMotion.AxisParameter(mAxisNo).Parameter.AlarmEnable)
        gSyslog.Save("BacklashEnable: " & gCMotion.AxisParameter(mAxisNo).Parameter.BacklashEnable)
        gSyslog.Save("ButtonDirection: " & gCMotion.AxisParameter(mAxisNo).Parameter.ButtonDirection)
        gSyslog.Save("Direction: " & gCMotion.AxisParameter(mAxisNo).Parameter.Direction)
        gSyslog.Save("ErcEnable: " & gCMotion.AxisParameter(mAxisNo).Parameter.ErcEnable)
        gSyslog.Save("ErcLogic: " & gCMotion.AxisParameter(mAxisNo).Parameter.ErcLogic)
        gSyslog.Save("ExternalDriveAxis: " & gCMotion.AxisParameter(mAxisNo).Parameter.ExternalDriveAxis)
        gSyslog.Save("ExternalDriveEnable: " & gCMotion.AxisParameter(mAxisNo).Parameter.ExternalDriveEnable)
        gSyslog.Save("ExternalDrivePulseInMode: " & gCMotion.AxisParameter(mAxisNo).Parameter.ExternalDrivePulseInMode)
        gSyslog.Save("EZLogic: " & gCMotion.AxisParameter(mAxisNo).Parameter.EZLogic)
        gSyslog.Save("HomeReset: " & gCMotion.AxisParameter(mAxisNo).Parameter.HomeReset)
        gSyslog.Save("INPEnable: " & gCMotion.AxisParameter(mAxisNo).Parameter.INPEnable)
        gSyslog.Save("INPLogic: " & gCMotion.AxisParameter(mAxisNo).Parameter.INPLogic)
        gSyslog.Save("IsEncoderExist: " & gCMotion.AxisParameter(mAxisNo).Parameter.IsEncoderExist)
        gSyslog.Save("LatchEnable: " & gCMotion.AxisParameter(mAxisNo).Parameter.LatchEnable)
        gSyslog.Save("LatchLogic: " & gCMotion.AxisParameter(mAxisNo).Parameter.LatchLogic)
        gSyslog.Save("MotorType: " & gCMotion.AxisParameter(mAxisNo).Parameter.MotorType)
        gSyslog.Save("OrgLogic: " & gCMotion.AxisParameter(mAxisNo).Parameter.OrgLogic)
        gSyslog.Save("PPU: " & gCMotion.AxisParameter(mAxisNo).Parameter.PPU)
        gSyslog.Save("PulseInDirection: " & gCMotion.AxisParameter(mAxisNo).Parameter.PulseInDirection)
        gSyslog.Save("PulseInMaxFreq: " & gCMotion.AxisParameter(mAxisNo).Parameter.PulseInMaxFreq)
        gSyslog.Save("PulseInMode: " & gCMotion.AxisParameter(mAxisNo).Parameter.PulseInMode)
        gSyslog.Save("PulseOutMode: " & gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutMode)
        gSyslog.Save("PulseOutReverse: " & gCMotion.AxisParameter(mAxisNo).Parameter.PulseOutReverse)
        gSyslog.Save("Scale: " & gCMotion.AxisParameter(mAxisNo).Parameter.Scale)
        gSyslog.Save("TriggerStopEnable1: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable1)
        gSyslog.Save("TriggerStopEnable2: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable2)
        gSyslog.Save("TriggerStopEnable4: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable4)
        gSyslog.Save("TriggerStopEnable5: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopEnable5)
        gSyslog.Save("TriggerStopLogic1: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic1)
        gSyslog.Save("TriggerStopLogic2: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic2)
        gSyslog.Save("TriggerStopLogic4: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic4)
        gSyslog.Save("TriggerStopLogic5: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopLogic5)
        gSyslog.Save("TriggerStopMode1: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode1)
        gSyslog.Save("TriggerStopMode2: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode2)
        gSyslog.Save("TriggerStopMode4: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode4)
        gSyslog.Save("TriggerStopMode5: " & gCMotion.AxisParameter(mAxisNo).Parameter.TriggerStopMode5)
        gSyslog.Save("New Axis Parameter (" & gCMotion.AxisParameter(mAxisNo).AxisName & ") End")

        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        'Me.Hide()
    End Sub

    Private Sub tmrOption_Tick(sender As Object, e As EventArgs) Handles tmrOption.Tick
        gCMotion.CheckMotorStatus(AxisNo)
        With gCMotion.AxisParameter(AxisNo).MotionIOStatus

            SetSensorBackgroundImage(palRDY, .blnRDY, False)
            SetSensorBackgroundImage(palALM, .blnALM, False)
            SetSensorBackgroundImage(palALRM, .blnALRM, False)
            SetSensorBackgroundImage(palCLR, .blnCLR, False)
            SetSensorBackgroundImage(palDIR, .blnDIR, False)
            SetSensorBackgroundImage(palEMG, .blnEMG, False)
            SetSensorBackgroundImage(palERC, .blnERC, False)
            SetSensorBackgroundImage(palEZ, .blnEZ, False)
            SetSensorBackgroundImage(palINP, .blnINP, False)
            SetSensorBackgroundImage(palLTC, .blnLTC, False)
            SetSensorBackgroundImage(palNEL, .blnNEL, False)
            SetSensorBackgroundImage(palNSEL, .blnSNEL, False)
            SetSensorBackgroundImage(palORG, .blnORG, False)
            SetSensorBackgroundImage(palPCS, .blnPCS, False)
            SetSensorBackgroundImage(palPEL, .blnPEL, False)
            SetSensorBackgroundImage(palSD, .blnSD, False)
            SetSensorBackgroundImage(palNSEL, .blnSNEL, False)
            SetSensorBackgroundImage(palPSEL, .blnSPEL, False)
            SetSensorBackgroundImage(palSVON, .blnSVON, False)
            SetSensorBackgroundImage(palTRIG, .blnTRIG, False)

        End With
    End Sub

    Private Sub frmMotionOption_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        tmrOption.Start()
    End Sub

    Private Sub frmMotionOption_Load(sender As Object, e As EventArgs) Handles Me.Load
        cmbCardType.Items.Clear()
        cmbCardType.Items.Add("None")
        cmbCardType.Items.Add("PCI_1245")
        cmbCardType.Items.Add("PCI_1285")
        cmbCardType.Items.Add("ModBus")
       
        cmbAxisType.Items.Clear()
        cmbAxisType.Items.Add(myResource.GetString("cmbAxisType.Items2")) '"Default")
        cmbAxisType.Items.Add("HM60163C(MODBUS)")
        cmbAxisType.Items.Add("RK2(MODBUS)")
        cmbMotorType.Items.Clear()
        cmbMotorType.Items.Add(myResource.GetString("cmbMotorType.Items2")) ' "ServoMotor")
        cmbMotorType.Items.Add(myResource.GetString("cmbMotorType.Items3")) '"SteppingMotor")
        cmbMotorType.Items.Add(myResource.GetString("cmbMotorType.Items")) '"ElectricCylinder")

        cmbPulseInMode.Items.Clear()
        cmbPulseInMode.Items.Add("1XAB")
        cmbPulseInMode.Items.Add("2XAB")
        cmbPulseInMode.Items.Add("4XAB")
        cmbPulseInMode.Items.Add("CCW/CW")
        cmbPulseInMaxFreq.Items.Clear()
        cmbPulseInMaxFreq.Items.Add("500KHz")
        cmbPulseInMaxFreq.Items.Add("1MHz")
        cmbPulseInMaxFreq.Items.Add("2MHz")
        cmbPulseInMaxFreq.Items.Add("4MHz")
        cmbPulseOutMode.Items.Clear()
        cmbPulseOutMode.Items.Add("P/D") '1
        cmbPulseOutMode.Items.Add("P/D,Negative Pulse") '2
        cmbPulseOutMode.Items.Add("P/D,Negative Direction") '4
        cmbPulseOutMode.Items.Add("P/D,Negative Pulse&Direction") '8
        cmbPulseOutMode.Items.Add("CW/CCW") '16
        cmbPulseOutMode.Items.Add("CW/CCW, Negaitve CW&CCW") '32
        cmbPulseOutMode.Items.Add("A/B Phase") '64
        cmbPulseOutMode.Items.Add("B/A Phase") '128
        cmbPulseOutMode.Items.Add("CW/CCW, Negative CW") '256
        cmbPulseOutMode.Items.Add("CW/CCW, Negative CCW") '512
        cmbMoveDirection.Items.Clear()
        cmbMoveDirection.Items.Add(myResource.GetString("cmbMoveDirection.Items")) '"NotReverse")
        cmbMoveDirection.Items.Add(myResource.GetString("cmbMoveDirection.Items1")) '"Reverse")
        cmbButtonDirection.Items.Clear()
        cmbButtonDirection.Items.Add(myResource.GetString("cmbButtonDirection.Items")) '"NotReverse")
        cmbButtonDirection.Items.Add(myResource.GetString("cmbButtonDirection.Items1")) '"Reverse")
        cmbPulseInReverse.Items.Clear()
        cmbPulseInReverse.Items.Add(myResource.GetString("cmbPulseInReverse.Items")) '"NotReverse")
        cmbPulseInReverse.Items.Add(myResource.GetString("cmbPulseInReverse.Items1")) '"Reverse")
        cmbPulseOutReverse.Items.Clear()
        cmbPulseOutReverse.Items.Add(myResource.GetString("cmbPulseOutReverse.Items")) '"NotReverse")
        cmbPulseOutReverse.Items.Add(myResource.GetString("cmbPulseOutReverse.Items1")) '"Reverse")
        cmbIsEncoderExist.Items.Clear()
        cmbIsEncoderExist.Items.Add(myResource.GetString("cmbIsEncoderExist.Items")) '"No")
        cmbIsEncoderExist.Items.Add(myResource.GetString("cmbIsEncoderExist.Items1")) '"Yes")
        cmbCoordinate.Items.Clear()
        cmbCoordinate.Items.Add(myResource.GetString("cmbCoordinate.Items3")) '"X(Linear)")
        cmbCoordinate.Items.Add(myResource.GetString("cmbCoordinate.Items4")) '"Y(Linear)")
        cmbCoordinate.Items.Add(myResource.GetString("cmbCoordinate.Items5")) '"Z(Linear)")
        cmbCoordinate.Items.Add(myResource.GetString("cmbCoordinate.Items")) '"A(Rotation)")
        cmbCoordinate.Items.Add(myResource.GetString("cmbCoordinate.Items1")) '"B(Rotation)")
        cmbCoordinate.Items.Add(myResource.GetString("cmbCoordinate.Items2")) '"C(Rotation)")
        cmbHomeDirection.Items.Clear()
        cmbHomeDirection.Items.Add(myResource.GetString("cmbHomeDirection.Items")) '"NotReverse")
        cmbHomeDirection.Items.Add(myResource.GetString("cmbHomeDirection.Items1")) '"Reverse")
        cmbHomeReset.Items.Clear()
        cmbHomeReset.Items.Add(myResource.GetString("cmbHomeReset.Items")) '"No")
        cmbHomeReset.Items.Add(myResource.GetString("cmbHomeReset.Items1")) '"Yes")
        cmbHLMTEnable.Items.Clear()
        cmbHLMTEnable.Items.Add(myResource.GetString("cmbHLMTEnable.Items")) '"Disabled")
        cmbHLMTEnable.Items.Add(myResource.GetString("cmbHLMTEnable.Items1")) '"Enabled")
        cmbHLMTLogic.Items.Clear()
        cmbHLMTLogic.Items.Add(myResource.GetString("cmbHLMTLogic.Items1")) '"Low Activated")
        cmbHLMTLogic.Items.Add(myResource.GetString("cmbHLMTLogic.Items")) '"High Activated")
        cmbHLMTStopMode.Items.Clear()
        cmbHLMTStopMode.Items.Add(myResource.GetString("cmbHLMTStopMode.Items")) '"EmgStop")
        cmbHLMTStopMode.Items.Add(myResource.GetString("cmbHLMTStopMode.Items1")) '"SlowStop")

        cmbALMEnable.Items.Clear()
        cmbALMEnable.Items.Add(myResource.GetString("cmbALMEnable.Items")) '"Disabled")
        cmbALMEnable.Items.Add(myResource.GetString("cmbALMEnable.Items1")) '"Enabled")
        cmbALMLogic.Items.Clear()
        cmbALMLogic.Items.Add(myResource.GetString("cmbALMLogic.Items1")) '"Low Activated")
        cmbALMLogic.Items.Add(myResource.GetString("cmbALMLogic.Items")) '"High Activated")
        cmbALMStopMode.Items.Clear()
        cmbALMStopMode.Items.Add(myResource.GetString("cmbALMStopMode.Items")) '"EmgStop")
        cmbALMStopMode.Items.Add(myResource.GetString("cmbALMStopMode.Items1")) '"SlowStop")

        cmbHLMTEnable.Items.Clear()
        cmbHLMTEnable.Items.Add(myResource.GetString("cmbHLMTEnable.Items")) '"Disabled")
        cmbHLMTEnable.Items.Add(myResource.GetString("cmbHLMTEnable.Items1")) '"Enabled")
        cmbBacklashEnable.Items.Clear()
        cmbBacklashEnable.Items.Add(myResource.GetString("cmbBacklashEnable.Items")) '"Disabled")
        cmbBacklashEnable.Items.Add(myResource.GetString("cmbBacklashEnable.Items1")) '"Enabled")
        cmbORGLogic.Items.Clear()
        cmbORGLogic.Items.Add(myResource.GetString("cmbORGLogic.Items1")) '"Low Activated")
        cmbORGLogic.Items.Add(myResource.GetString("cmbORGLogic.Items")) '"High Activated")
        cmbEZLogic.Items.Clear()
        cmbEZLogic.Items.Add(myResource.GetString("cmbEZLogic.Items1")) '"Low Activated")
        cmbEZLogic.Items.Add(myResource.GetString("cmbEZLogic.Items")) '"High Activated")

        cmbINPEnable.Items.Clear()
        cmbINPEnable.Items.Add(myResource.GetString("cmbINPEnable.Items")) '"Disabled")
        cmbINPEnable.Items.Add(myResource.GetString("cmbINPEnable.Items1")) '"Enabled")
        cmbINPLogic.Items.Clear()
        cmbINPLogic.Items.Add(myResource.GetString("cmbINPLogic.Items1")) '"Low Activated")
        cmbINPLogic.Items.Add(myResource.GetString("cmbINPLogic.Items")) '"High Activated")
        cmbLatchEnable.Items.Clear()
        cmbLatchEnable.Items.Add(myResource.GetString("cmbLatchEnable.Items")) '"Disabled")
        cmbLatchEnable.Items.Add(myResource.GetString("cmbLatchEnable.Items1")) '"Enabled")
        cmbLatchLogic.Items.Clear()
        cmbLatchLogic.Items.Add(myResource.GetString("cmbLatchLogic.Items1")) '"Low Activated")
        cmbLatchLogic.Items.Add(myResource.GetString("cmbLatchLogic.Items")) '"High Activated")
        cmbERCEnable.Items.Clear()
        cmbERCEnable.Items.Add(myResource.GetString("cmbERCEnable.Items")) '"Disabled")
        cmbERCEnable.Items.Add(myResource.GetString("cmbERCEnable.Items1")) '"Enabled")
        cmbERCLogic.Items.Clear()
        cmbERCLogic.Items.Add(myResource.GetString("cmbERCLogic.Items1")) '"Low Activated")
        cmbERCLogic.Items.Add(myResource.GetString("cmbERCLogic.Items")) '"High Activated")
        cmbExternalDriveEnable.Items.Clear()
        cmbExternalDriveEnable.Items.Add(myResource.GetString("cmbExternalDriveEnable.Items")) '"Disabled")
        cmbExternalDriveEnable.Items.Add(myResource.GetString("cmbExternalDriveEnable.Items1")) '"Enabled")

        cmbExternalDriveAxis.Items.Clear()
        cmbExternalDriveAxis.Items.Add(myResource.GetString("cmbExternalDriveAxis.Items")) '"Axis0")
        cmbExternalDriveAxis.Items.Add(myResource.GetString("cmbExternalDriveAxis.Items1")) '"Axis1")
        cmbExternalDriveAxis.Items.Add(myResource.GetString("cmbExternalDriveAxis.Items2")) '"Axis2")
        cmbExternalDriveAxis.Items.Add(myResource.GetString("cmbExternalDriveAxis.Items3")) '"Axis3")
        cmbExternalPulseInMode.Items.Clear()
        cmbExternalPulseInMode.Items.Add("1XAB")
        cmbExternalPulseInMode.Items.Add("2XAB")
        cmbExternalPulseInMode.Items.Add("4XAB")
        cmbExternalPulseInMode.Items.Add("CCW/CW")

        cmbIN1StopEnable.Items.Clear()
        cmbIN1StopEnable.Items.Add(myResource.GetString("cmbIN1StopEnable.Items")) '"Disabled")
        cmbIN1StopEnable.Items.Add(myResource.GetString("cmbIN1StopEnable.Items1")) '"Enabled")
        cmbIN2StopEnable.Items.Clear()
        cmbIN2StopEnable.Items.Add(myResource.GetString("cmbIN2StopEnable.Items")) '"Disabled")
        cmbIN2StopEnable.Items.Add(myResource.GetString("cmbIN2StopEnable.Items1")) '"Enabled")
        cmbIN4StopEnable.Items.Clear()
        cmbIN4StopEnable.Items.Add(myResource.GetString("cmbIN4StopEnable.Items")) '"Disabled")
        cmbIN4StopEnable.Items.Add(myResource.GetString("cmbIN4StopEnable.Items1")) '"Enabled")
        cmbIN5StopEnable.Items.Clear()
        cmbIN5StopEnable.Items.Add(myResource.GetString("cmbIN5StopEnable.Items")) '"Disabled")
        cmbIN5StopEnable.Items.Add(myResource.GetString("cmbIN5StopEnable.Items1")) '"Enabled")

        cmbIN1StopLogic.Items.Clear()
        cmbIN1StopLogic.Items.Add(myResource.GetString("cmbIN1StopLogic.Items1")) '"Low Activated")
        cmbIN1StopLogic.Items.Add(myResource.GetString("cmbIN1StopLogic.Items")) '"High Activated")
        cmbIN2StopLogic.Items.Clear()
        cmbIN2StopLogic.Items.Add(myResource.GetString("cmbIN2StopLogic.Items1")) '"Low Activated")
        cmbIN2StopLogic.Items.Add(myResource.GetString("cmbIN2StopLogic.Items")) '"High Activated")
        cmbIN4StopLogic.Items.Clear()
        cmbIN4StopLogic.Items.Add(myResource.GetString("cmbIN4StopLogic.Items1")) '"Low Activated")
        cmbIN4StopLogic.Items.Add(myResource.GetString("cmbIN4StopLogic.Items")) '"High Activated")
        cmbIN5StopLogic.Items.Clear()
        cmbIN5StopLogic.Items.Add(myResource.GetString("cmbIN5StopLogic.Items1")) '"Low Activated")
        cmbIN5StopLogic.Items.Add(myResource.GetString("cmbIN5StopLogic.Items")) '"High Activated")

        cmbIN1StopMode.Items.Clear()
        cmbIN1StopMode.Items.Add(myResource.GetString("cmbIN1StopMode.Items")) '"EmgStop")
        cmbIN1StopMode.Items.Add(myResource.GetString("cmbIN1StopMode.Items1")) '"SlowStop")
        cmbIN2StopMode.Items.Clear()
        cmbIN2StopMode.Items.Add(myResource.GetString("cmbIN2StopMode.Items")) '"EmgStop")
        cmbIN2StopMode.Items.Add(myResource.GetString("cmbIN2StopMode.Items1")) '"SlowStop")
        cmbIN4StopMode.Items.Clear()
        cmbIN4StopMode.Items.Add(myResource.GetString("cmbIN4StopMode.Items")) '"EmgStop")
        cmbIN4StopMode.Items.Add(myResource.GetString("cmbIN4StopMode.Items1")) '"SlowStop")
        cmbIN5StopMode.Items.Clear()
        cmbIN5StopMode.Items.Add(myResource.GetString("cmbIN5StopMode.Items")) '"EmgStop")
        cmbIN5StopMode.Items.Add(myResource.GetString("cmbIN5StopMode.Items1")) '"SlowStop")

        If gUserLevel = enmUserLevel.eSoftwareMaker Then '限定最高權限可用
            cmbCardType.Enabled = True
            cmbAxisType.Enabled = True
            cmbMotorType.Enabled = True
            cmbPulseInMode.Enabled = True
            cmbPulseInMaxFreq.Enabled = True
            cmbPulseOutMode.Enabled = True
            cmbMoveDirection.Enabled = True
            cmbButtonDirection.Enabled = True
            cmbPulseInReverse.Enabled = True
            cmbPulseOutReverse.Enabled = True
            cmbIsEncoderExist.Enabled = True
            cmbCoordinate.Enabled = True
        Else
            cmbCardType.Enabled = False
            cmbAxisType.Enabled = False
            cmbMotorType.Enabled = False
            cmbPulseInMode.Enabled = False
            cmbPulseInMaxFreq.Enabled = False
            cmbPulseOutMode.Enabled = False
            cmbMoveDirection.Enabled = False
            cmbButtonDirection.Enabled = False
            cmbPulseInReverse.Enabled = False
            cmbPulseOutReverse.Enabled = False
            cmbIsEncoderExist.Enabled = False
            cmbCoordinate.Enabled = False
        End If
    End Sub

#Region "外部同步移動"
    Public SyncForm As Form

    
    Private Sub frmMotionOption_Move(sender As Object, e As EventArgs) Handles Me.Move
        If Not Me.SyncForm Is Nothing Then
            Me.SyncForm.Location = New Point(Me.Location.X - Me.SyncForm.Width, Me.Location.Y)
        End If
    End Sub
#End Region
    
#Region "功能說明"

   
    Private Sub lblAxisName_MouseMove(sender As Object, e As MouseEventArgs) Handles lblAxisName.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "目前軸的顯示名稱."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "Current Axis Name."
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "目前轴的显示名称"
        End Select

    End Sub

    Private Sub lblCardType_MouseMove(sender As Object, e As MouseEventArgs) Handles lblCardType.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "卡片型號,決定介面設定值開放與選項."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "Card Type, for Configuration & Option."
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "卡片型号,决定介面设定值开放与选项."
        End Select

    End Sub
    Private Sub lblCardNo_MouseMove(sender As Object, e As MouseEventArgs) Handles lblCardNo.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "指向底層的卡號/Item索引(限客服工程師可修改)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "Refer to Card No/Item No(Only Customer Service Engineer)."
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "指向底层的卡号/Item索引(限客服工程师可修改)"
        End Select

    End Sub

    Private Sub lblAxisNo_MouseMove(sender As Object, e As MouseEventArgs) Handles lblAxisNo.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "指向底層卡片上的軸號."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "Axis No on Card."
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "指向底层卡片上的轴号."
        End Select

    End Sub

    Private Sub lblScale_MouseMove(sender As Object, e As MouseEventArgs) Handles lblScale.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "介面設定值與對軸卡輸出比例關係. 標準為1000pulse/mm"
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "Pulse Per mm. Standard Value: 1000pulse/mm"
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "介面设定值与对轴卡输出比例关系. 标准为1000pulse/mm"
        End Select

    End Sub

    Private Sub lblAxisType_MouseMove(sender As Object, e As MouseEventArgs) Handles lblAxisType.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "決定對本軸的操作模式、MODBUS使用COM埠通訊控制."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "决定对本轴的操作模式、MODBUS使用COM埠通讯控制."
        End Select

    End Sub

    Private Sub lblMotorType_MouseMove(sender As Object, e As MouseEventArgs) Handles lblMotorType.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "決定對本軸配接的馬達類型. 通常主要部件是伺服馬達、週邊部件是步進馬達或電動缸"
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "决定对本轴配接的马达类型. 通常主要部件是伺服马达、周边部件是步进马达或电动缸."
        End Select

    End Sub

    Private Sub lblPulseInMode_MouseMove(sender As Object, e As MouseEventArgs) Handles lblPulseInMode.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "脈衝輸入模式. 通常是4XAB."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "脉冲输入模式. 通常是4XAB."
        End Select
    End Sub

    Private Sub lblPulseInMaxFreq_MouseMove(sender As Object, e As MouseEventArgs) Handles lblPulseInMaxFreq.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "脈衝輸入頻寬限制. 通常是1M."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "脉冲输入频宽限制. 通常是1M."
        End Select
    End Sub

    Private Sub lblPulseOutMode_MouseMove(sender As Object, e As MouseEventArgs) Handles lblPulseOutMode.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "脈衝輸出模式. 通常是CW/CCW. 若正負方向只會往單邊移動、表示本模式設定錯誤."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "脉冲输出模式. 通常是CW/CCW. 若正负方向只会往单边移动、表示本模式设定错误."
        End Select
    End Sub

    Private Sub lblMoveDirection_MouseMove(sender As Object, e As MouseEventArgs) Handles lblMoveDirection.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "運動命令反向. 不建議反向,僅針對無脈衝輸出反向功能的軸卡提供功能模仿, 注意反向時正負極限作動將失效."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "运动命令反向. 不建议反向,仅针对无脉冲输出反向功能的轴卡提供功能模仿, 注意反向时正负极限作动将失效."
        End Select
    End Sub

    Private Sub lblButtonDirection_MouseMove(sender As Object, e As MouseEventArgs) Handles lblButtonDirection.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "單軸操作介面方向反向. 用於調整介面操作與機台實際動作方向一致."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "单轴操作介面方向反向. 用于调整介面操作与机台实际动作方向一致."
        End Select
    End Sub

    Private Sub lblPulseInReverse_MouseMove(sender As Object, e As MouseEventArgs) Handles lblPulseInReverse.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "脈衝輸入反向, 用於回授與軸動作方向相反時調整用, 若驅動器支援該功能,優先推薦以驅動器調整."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "脉冲输入反向, 用于回授与轴动作方向相反时调整用, 若驱动器支援该功能,优先推荐以驱动器调整."
        End Select
    End Sub

    Private Sub lblPulseOutReverse_MouseMove(sender As Object, e As MouseEventArgs) Handles lblPulseOutReverse.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "脈衝輸出反向, 用於命令與實際需求動作方向相反時調整用, 若驅動器支援該功能,優先推薦以驅動器調整."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "脉冲输出反向, 用于命令与实际需求动作方向相反时调整用, 若驱动器支援该功能,优先推荐以驱动器调整."
        End Select
    End Sub

    Private Sub lblIsEncoderExist_MouseMove(sender As Object, e As MouseEventArgs) Handles lblIsEncoderExist.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "編碼器是否存在? 若編碼器存在,則讀取位置為編碼器實際值,若編碼器不存在,則讀取位置為命令輸出值. 注意部分步進馬達具有編碼器回授."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "编码器是否存在? 若编码器存在,则读取位置为编码器实际值,若编码器不存在,则读取位置为命令输出值. 注意部分步进马达具有编码器回授."
        End Select
    End Sub

    Private Sub lblCoordinate_MouseMove(sender As Object, e As MouseEventArgs) Handles lblCoordinate.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "相應座標系設定. 決定單軸操作介面上方向鍵的呈現方向."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "相应座标系设定. 决定单轴操作介面上方向键的呈现方向."
        End Select
    End Sub

    Private Sub lblHomeMode_MouseMove(sender As Object, e As MouseEventArgs) Handles lblHomeMode.MouseMove
        lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "復歸模式." & vbCrLf
        Select Case gCMotion.AxisParameter(mAxisNo).CardParameter.CardType
            Case enmMotionCardType.PCI_1285, enmMotionCardType.PCI_1245
                lblDec.Text += "MODE1_ABS" & vbCrLf & "MODE2_Lmt" & vbCrLf & "MODE3_Ref" & vbCrLf & "MODE4_Abs_Ref" & vbCrLf & "MODE5_Abs_NegRef" & vbCrLf & "MODE6_Lmt_Ref" & vbCrLf & "MODE7_AbsSearch" & vbCrLf & "MODE8_LmtSearch" & vbCrLf & "MODE9_AbsSearch_Ref" & vbCrLf & "MODE10_Abs_Search_NegRef" & vbCrLf & "MODE11_LmtSearch_Ref" & vbCrLf & "MODE12_AbsSearchReFind" & vbCrLf & "MODE13_LmtSearchRefind" & vbCrLf & "MODE14_AbsSearchReFind_Ref" & vbCrLf & "MODE15_AbsSearchRefomd_NegRef" & vbCrLf & "MODE16_LmtSearchReFind_Ref" & vbCrLf
            Case Else
                lblDec.Text += "請參閱相關手冊."
        End Select
    End Sub

    Private Sub lblHomeCrossDistance_MouseMove(sender As Object, e As MouseEventArgs) Handles lblHomeCrossDistance.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "在ORG有效範圍內復歸時, 以該位移量向外移動至離開ORG範圍."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "在ORG有效范围内复归时, 以该位移量向外移动至离开ORG范围."
        End Select
    End Sub

    Private Sub lblHomeReset_MouseMove(sender As Object, e As MouseEventArgs) Handles lblHomeReset.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "原點復歸完成後,是否將位置清零. 該設定對電動缸無效."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "原点复归完成后,是否将位置清零. 该设定对电动缸无效."
        End Select
    End Sub

    Private Sub lblHomeDirection_MouseMove(sender As Object, e As MouseEventArgs) Handles lblHomeDirection.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "復歸方向是否反向."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "复归方向是否反向."
        End Select
    End Sub

    Private Sub lblHomeDec_MouseMove(sender As Object, e As MouseEventArgs) Handles lblHomeDec.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "復歸用減速度."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "复归用减速度."
        End Select
    End Sub

    Private Sub lblHomeAcc_MouseMove(sender As Object, e As MouseEventArgs) Handles lblHomeAcc.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "復歸用加速度."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "复归用加速度."
        End Select
    End Sub

    Private Sub lblHomeVelHigh_MouseMove(sender As Object, e As MouseEventArgs) Handles lblHomeVelHigh.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "第一段高速復歸, 用於快速找到原點."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "第一段高速复归, 用于快速找到原点."
        End Select
    End Sub

    Private Sub lblHomeVelLow_MouseMove(sender As Object, e As MouseEventArgs) Handles lblHomeVelLow.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "第二段慢速復歸, 用於尋找原點並減速停止."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "第二段慢速复归, 用于寻找原点并减速停止."
        End Select
    End Sub

    Private Sub lblHomeExSwitchMode_MouseMove(sender As Object, e As MouseEventArgs) Handles lblHomeExSwitchMode.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "特殊復歸方式的停止條件."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "特殊复归方式的停止条件."
        End Select

    End Sub

    Private Sub lblSNEL_MouseMove(sender As Object, e As MouseEventArgs) Handles lblSNEL.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "軟體負極限. 低於該值的絕對移動命令無法下達"
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "软体负极限. 低于该值的绝对移动命令无法下达."
        End Select

    End Sub

    Private Sub lblSPEL_MouseMove(sender As Object, e As MouseEventArgs) Handles lblSPEL.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "軟體正極限. 高於該值的絕對移動命令無法下達."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "软体正极限. 高于该值的绝对移动命令无法下达."
        End Select
    End Sub

    Private Sub lblHLMTEnable_MouseMove(sender As Object, e As MouseEventArgs) Handles lblHLMTEnable.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "硬體極限(Hardware Limit)致能, 如該軸裝有硬體極限開關,則啟用該功能以確保過極限停止運動."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "硬体极限(Hardware Limit)致能, 如该轴装有硬体极限开关,则启用该功能以确保过极限停止运动."
        End Select
    End Sub

    Private Sub lblHLMTLogic_MouseMove(sender As Object, e As MouseEventArgs) Handles lblHLMTLogic.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "硬體極限(Hardware Limit)邏輯, 如該軸裝有硬體極限開關,則須確認正常範圍內訊號為LOW."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "硬体极限(Hardware Limit)逻辑, 如该轴装有硬体极限开关,则须确认正常范围内讯号为LOW."
        End Select
    End Sub

    Private Sub lblHLMTStopMode_MouseMove(sender As Object, e As MouseEventArgs) Handles lblHLMTStopMode.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "硬體極限(Hardware Limit)停止模式, 該軸接觸硬體極限開關時,應立即停止(EmgStop)或減速停止(SlowStop)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "硬体极限(Hardware Limit)停止模式, 该轴接触硬体极限开关时,应立即停止(EmgStop)或减速停止(SlowStop)."
        End Select

    End Sub

    Private Sub lblALMEnable_MouseMove(sender As Object, e As MouseEventArgs) Handles lblALMEnable.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "異警停止(Alarm)致能, 若啟用該功能,則異常發生時停止運動."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "异警停止(Alarm)致能, 若启用该功能,则异常发生时停止运动."
        End Select

    End Sub

    Private Sub lblALMLogic_MouseMove(sender As Object, e As MouseEventArgs) Handles lblALMLogic.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "異警停止(Alarm)邏輯準位, 如該軸接有異警停止訊號,則須確認正常範圍內訊號為LOW."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "异警停止(Alarm)逻辑准位, 如该轴接有异警停止讯号,则须确认正常范围内讯号为LOW."
        End Select
    End Sub

    Private Sub lblALMStopMode_MouseMove(sender As Object, e As MouseEventArgs) Handles lblALMStopMode.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "異警停止(Alarm)模式, 該軸異警停止做動時,應立即停止(EmgStop)或減速停止(SlowStop). 通常是硬體採用馬達斷電保護."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "异警停止(Alarm)模式, 该轴异警停止作动时,应立即停止(EmgStop)或减速停止(SlowStop). 通常是硬体采用马达断电保护."
        End Select
    End Sub

    Private Sub lblBacklashEnable_MouseMove(sender As Object, e As MouseEventArgs) Handles lblBacklashEnable.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "背隙補償(Backlash)致能."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "背隙补偿(Backlash)致能."
        End Select

    End Sub

    Private Sub lblORGLogic_MouseMove(sender As Object, e As MouseEventArgs) Handles lblORGLogic.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "原點(Origin)邏輯準位,須確認正常範圍內訊號為LOW."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "原点(Origin)逻辑准位,须确认正常范围内讯号为LOW."
        End Select

    End Sub

    Private Sub lblEZLogic_MouseMove(sender As Object, e As MouseEventArgs) Handles lblEZLogic.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "Z相(EZ)邏輯準位,須確認正常範圍內訊號為LOW、伺服馬達一圈應有一個Pulse為HIGH."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "Z相(EZ)逻辑准位,须确认正常范围内讯号为LOW、伺服马达一圈应有一个Pulse为HIGH."
        End Select

    End Sub

    Private Sub lblINPEnable_MouseMove(sender As Object, e As MouseEventArgs) Handles lblINPEnable.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "到位訊號(Inposition)致能, 通常伺服馬達致能, 步進馬達除能."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "到位讯号(Inposition)致能, 通常伺服马达致能, 步进马达除能."
        End Select

    End Sub

    Private Sub lblINPLogic_MouseMove(sender As Object, e As MouseEventArgs) Handles lblINPLogic.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "到位訊號(Inposition)邏輯準位, 須確保馬達停止時為HIGH,運動中為LOW."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "到位讯号(Inposition)逻辑准位, 须确保马达停止时为HIGH,运动中为LOW."
        End Select

    End Sub

    Private Sub lblLatchEnable_MouseMove(sender As Object, e As MouseEventArgs) Handles lblLatchEnable.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "栓鎖訊號(Latch)致能, 通常用於Z軸測高等功能."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "栓锁讯号(Latch)致能, 通常用于Z轴测高等功能."
        End Select

    End Sub

    Private Sub lblLatchLogic_MouseMove(sender As Object, e As MouseEventArgs) Handles lblLatchLogic.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "栓鎖訊號(Latch)邏輯準位, 須確保未觸發時為LOW,觸發為HIGH."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "栓锁信号(Latch)逻辑准位, 须确保未触发时为LOW,触发为HIGH."
        End Select

    End Sub

    Private Sub lblERCEnable_MouseMove(sender As Object, e As MouseEventArgs) Handles lblERCEnable.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "錯誤清除訊號(Error Clear)致能."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "错误清除讯号(Error Clear)致能."
        End Select
    End Sub

    Private Sub lblERCLogic_MouseMove(sender As Object, e As MouseEventArgs) Handles lblERCLogic.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "錯誤清除訊號(Error Clear)邏輯準位, 須確保未設定時為LOW,設定為HIGH."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "错误清除讯号(Error Clear)逻辑准位, 须确保未设定时为LOW,设定为HIGH."
        End Select
    End Sub

    Private Sub lblExternalDriveEnable_MouseMove(sender As Object, e As MouseEventArgs) Handles lblExternalDriveEnable.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "外部驅動(External Drive)致能, 通常用於手搖輪,搖桿之類的脈衝輸入訊號."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "外部驱动(External Drive)致能, 通常用于手摇轮,摇杆之类的脉冲输入讯号."
        End Select
    End Sub

    Private Sub lblExternalDriveAxis_MouseMove(sender As Object, e As MouseEventArgs) Handles lblExternalDriveAxis.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "外部驅動(External Drive)來源軸."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "外部驱动(External Drive)来源轴."
        End Select
    End Sub

    Private Sub lblExternalPulseInMode_MouseMove(sender As Object, e As MouseEventArgs) Handles lblExternalPulseInMode.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "外部驅動(External Drive)模式. 請參閱相關手冊."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "外部驱动(External Drive)模式. 请参阅相关手册."
        End Select
    End Sub

    Private Sub lblVelLow_MouseMove(sender As Object, e As MouseEventArgs) Handles lblVelLow.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "初始移動速度. 一般為0, "
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "初始移动速度. 一般为0, "
        End Select

    End Sub

    Private Sub lblVelHigh_MouseMove(sender As Object, e As MouseEventArgs) Handles lblVelHigh.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "最大移動速度. "
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "最大移动速度. "
        End Select

    End Sub

    Private Sub lblAcc_MouseMove(sender As Object, e As MouseEventArgs) Handles lblAcc.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "加速度."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "加速度."
        End Select

    End Sub

    Private Sub lblDec_MouseMove(sender As Object, e As MouseEventArgs) Handles lblDec.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "減速度."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "减速度. "
        End Select
    End Sub

    Private Sub lblAccRatio_MouseMove(sender As Object, e As MouseEventArgs) Handles lblAccRatio.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "加速度比率."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "加速度比率."
        End Select

    End Sub

    Private Sub lblDecRatio_MouseMove(sender As Object, e As MouseEventArgs) Handles lblDecRatio.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "減速度比率."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "减速度比率."
        End Select
    End Sub

    Private Sub lblPPU_MouseMove(sender As Object, e As MouseEventArgs) Handles lblPPU.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "單位脈衝數量. 建議設定為1."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "单位脉冲数量. 建议设定为1."
        End Select

    End Sub

    Private Sub lblINPStable_MouseMove(sender As Object, e As MouseEventArgs) Handles lblINPStable.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "到位穩定時間. 建議設定至少為10, 實際狀況依機構穩定性而定."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "到位稳定时间. 建议设定至少为10, 实际状况依机构稳定性而定."
        End Select

    End Sub

    Private Sub lblIN1StopEnable_MouseMove(sender As Object, e As MouseEventArgs) Handles lblIN1StopEnable.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN1觸發停止功能. 不建議使用."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN1触发停止功能. 不建议使用."
        End Select

    End Sub

    Private Sub lblIN2StopEnable_MouseMove(sender As Object, e As MouseEventArgs) Handles lblIN2StopEnable.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN2觸發停止功能. 不建議使用."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN2触发停止功能. 不建议使用."
        End Select
    End Sub

    Private Sub lblIN4StopEnable_MouseMove(sender As Object, e As MouseEventArgs) Handles lblIN4StopEnable.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN4觸發停止功能. 不建議使用."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN4触发停止功能. 不建议使用."
        End Select
    End Sub

    Private Sub lblIN5StopEnable_MouseMove(sender As Object, e As MouseEventArgs) Handles lblIN5StopEnable.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN5觸發停止功能. 不建議使用."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN5触发停止功能. 不建议使用."
        End Select
    End Sub


    Private Sub lblIN1StopLogic_MouseMove(sender As Object, e As MouseEventArgs) Handles lblIN1StopLogic.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN1觸發停止邏輯準位. "
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN1触发停止逻辑准位."
        End Select

    End Sub

    Private Sub lblIN2StopLogic_MouseMove(sender As Object, e As MouseEventArgs) Handles lblIN2StopLogic.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN2觸發停止邏輯準位. "
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN2触发停止逻辑准位."
        End Select

    End Sub

    Private Sub lblIN4StopLogic_MouseMove(sender As Object, e As MouseEventArgs) Handles lblIN4StopLogic.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN4觸發停止邏輯準位. "
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN4触发停止逻辑准位."
        End Select

    End Sub

    Private Sub lblIN5StopLogic_MouseMove(sender As Object, e As MouseEventArgs) Handles lblIN5StopLogic.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN5觸發停止邏輯準位. "
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN5触发停止逻辑准位."
        End Select

    End Sub

    Private Sub lblIN1StopMode_MouseMove(sender As Object, e As MouseEventArgs) Handles lblIN1StopMode.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN1觸發停止模式."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN1触发停止模式."
        End Select

    End Sub

    Private Sub lblIN2StopMode_MouseMove(sender As Object, e As MouseEventArgs) Handles lblIN2StopMode.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN2觸發停止模式."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN2触发停止模式."
        End Select
    End Sub

    Private Sub lblIN4StopMode_MouseMove(sender As Object, e As MouseEventArgs) Handles lblIN4StopMode.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN4觸發停止模式."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN4触发停止模式."
        End Select
    End Sub

    Private Sub lblIN5StopMode_MouseMove(sender As Object, e As MouseEventArgs) Handles lblIN5StopMode.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN5觸發停止模式."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "IN5触发停止模式."
        End Select
    End Sub

    Private Sub lblRDY_MouseMove(sender As Object, e As MouseEventArgs) Handles lblRDY.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "伺服就緒訊號(RDY)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "伺服就绪讯号(RDY)."
        End Select
    End Sub

    Private Sub lblINP_MouseMove(sender As Object, e As MouseEventArgs) Handles lblINP.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "到位訊號(INP)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "到位讯号(INP)."
        End Select

    End Sub

    Private Sub lblALM_MouseMove(sender As Object, e As MouseEventArgs) Handles lblALM.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "伺服報警(ALM)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "伺服报警(ALM)."
        End Select

    End Sub

    Private Sub lblSVON_MouseMove(sender As Object, e As MouseEventArgs) Handles lblSVON.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "伺服激磁(SVON)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "伺服激磁(SVON)."
        End Select


    End Sub

    Private Sub lblPEL_MouseMove(sender As Object, e As MouseEventArgs) Handles lblPEL.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "硬體正極限(PEL,Limit+)輸入."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "硬体正极限(PEL,Limit)输入."
        End Select

    End Sub

    Private Sub lblALRM_MouseMove(sender As Object, e As MouseEventArgs) Handles lblALRM.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "報警復位輸出狀態(ALRM)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "报警复位输出状态(ALARM)."
        End Select

    End Sub

    Private Sub lblNEL_MouseMove(sender As Object, e As MouseEventArgs) Handles lblNEL.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "硬體負極限(NEL,Limit-)輸入."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "硬体负极限(NEL,Limit-)输入."
        End Select

    End Sub

    Private Sub lblEMG_MouseMove(sender As Object, e As MouseEventArgs) Handles lblEMG.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "緊急訊號(EMG)輸入."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "紧急讯号(EMG)输入."
        End Select

    End Sub

    Private Sub lblORG_MouseMove(sender As Object, e As MouseEventArgs) Handles lblORG.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "原點位置(ORG)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "原点位置(ORG)."
        End Select

    End Sub

    Private Sub lblPCS_MouseMove(sender As Object, e As MouseEventArgs) Handles lblPCS.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "PCS訊號(PCS)輸入."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "PCS讯号(PCS)输入."
        End Select

    End Sub

    Private Sub lblDIR_MouseMove(sender As Object, e As MouseEventArgs) Handles lblDIR.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "運動方向(DIR)訊號輸出."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "运动方向(DIR)讯号输出."
        End Select
    End Sub

    Private Sub lblERC_MouseMove(sender As Object, e As MouseEventArgs) Handles lblERC.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "清除伺服誤差計數器(ERC)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "清除伺服误差计数器(ERC)."
        End Select
    End Sub

    Private Sub lblTRIG_MouseMove(sender As Object, e As MouseEventArgs) Handles lblTRIG.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "比較信號(TRIG)"
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "比较信号(TRIG)."
        End Select

    End Sub

    Private Sub lblEZ_MouseMove(sender As Object, e As MouseEventArgs) Handles lblEZ.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "編碼器Z相訊號(EZ)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "Encoder EZ Signal(EZ)."
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "编码器Z相讯号(EZ)."
        End Select
    End Sub

    Private Sub lblCLR_MouseMove(sender As Object, e As MouseEventArgs) Handles lblCLR.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "外部輸入至清除位置計數器(CLR)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "Input External Signal to Clear Position Counter(CLR)."
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "外部输入至清除位置计数器(CLR)."
        End Select

    End Sub

    Private Sub lblPSEL_MouseMove(sender As Object, e As MouseEventArgs) Handles lblPSEL.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "軟體限位正極限(PSEL,SLMT+)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "Positive Software Edge Limit(NSEL,SLMT-)."
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "软体限位正极限(PSEL,SLMT-)."
        End Select
    End Sub

    Private Sub lblLTC_MouseMove(sender As Object, e As MouseEventArgs) Handles lblLTC.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "位置鎖存(LTC)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "Position Latch Signal(LTC)."
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "位址锁存(LTC)."
        End Select
    End Sub

    Private Sub lblNSEL_MouseMove(sender As Object, e As MouseEventArgs) Handles lblNSEL.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "軟體限位負極限(NSEL,SLMT-)."
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "Negative Software Edge Limit(NSEL,SLMT-)."
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "软体限位负极限(NSEL,SLMT-)."
        End Select
    End Sub

    Private Sub lblSD_MouseMove(sender As Object, e As MouseEventArgs) Handles lblSD.MouseMove
        Select Case gSSystemParameter.LanguageType
            Case enmLanguageType.eTraditionalChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "減速訊號輸入(SD)"
            Case enmLanguageType.eEnglish
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "Slow Down Signal (SD)"
            Case enmLanguageType.eSimplifiedChinese
                lblDesc.Text = myResource.GetString("lblDesc.Text") & vbCrLf & "减速讯号输入(SD)"
        End Select
    End Sub

    Private Sub frmMotionOption_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        RefreshUI()
    End Sub

#End Region

End Class
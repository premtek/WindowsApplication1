Imports ProjectRecipe
Imports ProjectCore
Imports ProjectIO
Imports ProjectMotion
Imports Premtek

Public Class frmSetValveAirPressure
    Public sys As sSysParam
    Private Sub frmSetValveAirPressure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim mValveAirPressureData1 As ucValveAirPressure.sValveAirPressure = New ucValveAirPressure.sValveAirPressure
        Dim mValveAirPressureData2 As ucValveAirPressure.sValveAirPressure = New ucValveAirPressure.sValveAirPressure
        Dim mValveAirPressureData3 As ucValveAirPressure.sValveAirPressure = New ucValveAirPressure.sValveAirPressure
        Dim mValveAirPressureData4 As ucValveAirPressure.sValveAirPressure = New ucValveAirPressure.sValveAirPressure

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                mValveAirPressureData1.StageNo = enmStage.No1
                mValveAirPressureData1.ValveWorkMode = eValveWorkMode.Valve1
                mValveAirPressureData1.SyringeEPV = enmEPV.No1
                mValveAirPressureData1.ValveAPEPV = enmEPV.No1
                mValveAirPressureData1.ValveModel = eValveModel.PicoPulse
                mValveAirPressureData1.SyringePressure = enmDO.SyringePressure1
                mValveAirPressureData1.ValvePressure = enmDO.ValvePressure1
                UcValveAirPressure1.SetUcValveData(mValveAirPressureData1)

                mValveAirPressureData2.StageNo = enmStage.No2
                mValveAirPressureData2.ValveWorkMode = eValveWorkMode.Valve1
                mValveAirPressureData2.SyringeEPV = enmEPV.No2
                mValveAirPressureData2.ValveAPEPV = enmEPV.No2
                mValveAirPressureData2.ValveModel = eValveModel.PicoPulse
                mValveAirPressureData2.SyringePressure = enmDO.SyringePressure2
                mValveAirPressureData2.ValvePressure = enmDO.ValvePressure2
                UcValveAirPressure2.SetUcValveData(mValveAirPressureData2)

                mValveAirPressureData3.StageNo = enmStage.No3
                mValveAirPressureData3.ValveWorkMode = eValveWorkMode.Valve1
                mValveAirPressureData3.SyringeEPV = enmEPV.No3
                mValveAirPressureData3.ValveAPEPV = enmEPV.No3
                mValveAirPressureData3.ValveModel = eValveModel.PicoPulse
                mValveAirPressureData3.SyringePressure = enmDO.SyringePressure3
                mValveAirPressureData3.ValvePressure = enmDO.ValvePressure3
                UcValveAirPressure3.SetUcValveData(mValveAirPressureData3)

                mValveAirPressureData4.StageNo = enmStage.No4
                mValveAirPressureData4.ValveWorkMode = eValveWorkMode.Valve1
                mValveAirPressureData4.SyringeEPV = enmEPV.No2
                mValveAirPressureData4.ValveAPEPV = enmEPV.No2
                mValveAirPressureData4.ValveModel = eValveModel.PicoPulse
                mValveAirPressureData4.SyringePressure = enmDO.SyringePressure4
                mValveAirPressureData4.ValvePressure = enmDO.ValvePressure2
                UcValveAirPressure4.SetUcValveData(mValveAirPressureData4)

                combStageNo.Items.Clear()
                combStageNo.Items.Add(enmStage.No1)
                combStageNo.Items.Add(enmStage.No2)
                combStageNo.Items.Add(enmStage.No3)
                combStageNo.Items.Add(enmStage.No4)
                combStageNo.SelectedIndex = 0
                'With UcJoyStick1
                '    .AxisX = sys.AxisX
                '    .AxisY = sys.AxisY
                '    .AxisZ = sys.AxisZ
                '    .AXisA = sys.AxisA
                '    .AXisB = sys.AxisB
                '    .AXisC = sys.AxisC
                'End With
                'UcJoyStick1.SetSpeedType(SpeedType.Slow)
                'UcJoyStick1.RefreshPosition()

            Case enmMachineType.DCS_F230A

                'jimmy20170714
            Case enmMachineType.DCS_350A
                mValveAirPressureData1.StageNo = enmStage.No1
                mValveAirPressureData1.ValveWorkMode = eValveWorkMode.Valve1
                mValveAirPressureData1.SyringeEPV = enmEPV.No1
                mValveAirPressureData1.ValveAPEPV = enmEPV.No1
                mValveAirPressureData1.ValveModel = eValveModel.Advanjet
                mValveAirPressureData1.SyringePressure = enmDO.SyringePressure1
                mValveAirPressureData1.ValvePressure = enmDO.ValvePressure1
                UcValveAirPressure1.SetUcValveData(mValveAirPressureData1)

                If gSSystemParameter.StageUseValveCount = eMechanismModule.TwoValveOneStage Then
                    mValveAirPressureData2.StageNo = enmStage.No1
                    mValveAirPressureData2.ValveWorkMode = eValveWorkMode.Valve2
                    mValveAirPressureData2.SyringeEPV = enmEPV.No2
                    mValveAirPressureData2.ValveAPEPV = enmEPV.No2
                    mValveAirPressureData2.ValveModel = eValveModel.Advanjet
                    mValveAirPressureData2.SyringePressure = enmDO.SyringePressure2
                    mValveAirPressureData2.ValvePressure = enmDO.ValvePressure2
                    UcValveAirPressure2.SetUcValveData(mValveAirPressureData2)

                Else
                    UcValveAirPressure2.Visible = False
                End If
                

                UcValveAirPressure3.Visible = False
                UcValveAirPressure4.Visible = False
                combStageNo.Items.Clear()
                combStageNo.Items.Add(enmStage.No1)

                combStageNo.SelectedIndex = 0
            Case Else
        End Select
    End Sub



    Private Sub frmSetValveAirPressure_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        UcJoyStick1.ManualDispose()
        Me.Dispose(True)
    End Sub

    Private Sub combStageNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles combStageNo.SelectedIndexChanged
        With UcJoyStick1
            Select Case combStageNo.SelectedIndex
                Case 0
                    sys = gSYS(eSys.DispStage1)
                Case 1
                    sys = gSYS(eSys.DispStage2)
                Case 2
                    sys = gSYS(eSys.DispStage3)
                Case 3
                    sys = gSYS(eSys.DispStage4)

            End Select
            With UcJoyStick1
                .AxisX = sys.AxisX
                .AxisY = sys.AxisY
                .AxisZ = sys.AxisZ
                .AXisA = sys.AxisA
                .AXisB = sys.AxisB
                .AXisC = sys.AxisC
            End With
            UcJoyStick1.SetSpeedType(SpeedType.Slow)
            UcJoyStick1.RefreshPosition()

            If (gSSystemParameter.StageCount > 1) Then
                If (gSSystemParameter.MachineSafeData.Count > 0) Then
                    UcJoyStick1.InverseAxisX.SafeDistance = gSSystemParameter.MachineSafeData(sys.MachineNo).SafeDistanceX
                    UcJoyStick1.InverseAxisX.Spread = gSSystemParameter.MachineSafeData(sys.MachineNo).SpreadX

                    If (sys.StageNo = enmStage.No1) Then
                        UcJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage2).AxisX    '對立軸
                        UcJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Posivtive
                    ElseIf (sys.StageNo = enmStage.No2) Then
                        UcJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage1).AxisX    '對立軸
                        UcJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Negative
                    ElseIf (sys.StageNo = enmStage.No3) Then
                        UcJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage4).AxisX    '對立軸
                        UcJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Posivtive
                    ElseIf (sys.StageNo = enmStage.No4) Then
                        UcJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage3).AxisX    '對立軸
                        UcJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Negative
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Sue20170627
        gSyslog.Save("[frmSetValveAirPressure]" & vbTab & "[btnExit]" & vbTab & "Click")
        Me.Close()
    End Sub
End Class
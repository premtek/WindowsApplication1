Imports ProjectCore
Imports ProjectIO
Imports ProjectMotion
Imports ProjectRecipe
Imports Premtek

Public Class CSystemAdapter
#Region "清膠夾爪"
    ''' <summary>清膠夾爪開閉</summary>
    ''' <param name="valveNo"></param>
    ''' <param name="onOff"></param>
    ''' <remarks></remarks>
    Sub ClearGlueClampOnOff(ByVal valveNo As Integer, ByVal onOff As enmONOFF)
        Select Case valveNo
            Case enmValve.No1, enmValve.No2
                Select Case onOff
                    Case enmONOFF.eON
                        gDOCollection.SetState(enmDO.ClearGlueClampOn, True)
                        gDOCollection.SetState(enmDO.ClearGlueClampOff, False)
                    Case enmONOFF.eOff
                        '[說明]:Clamp OFF
                        gDOCollection.SetState(enmDO.ClearGlueClampOn, False)
                        gDOCollection.SetState(enmDO.ClearGlueClampOff, True)
                End Select
            Case enmValve.No3, enmValve.No4

        End Select

    End Sub

    Function IsClearnGlueClampOnOffReady(ByVal valveNo As Integer, ByVal onOff As enmONOFF) As Boolean
        Select Case valveNo
            Case enmValve.No1, enmValve.No2
                Select Case onOff
                    Case enmONOFF.eON
                        If gDICollection.GetState(enmDI.ClearGlueClampOffSensor, False) = False And gDICollection.GetState(enmDI.ClearGlueClampOnSensor, True) = True Then
                            Return True
                        End If
                    Case enmONOFF.eOff
                        If gDICollection.GetState(enmDI.ClearGlueClampOffSensor, True) = True And gDICollection.GetState(enmDI.ClearGlueClampOnSensor, False) = False Then
                            Return True
                        End If
                End Select
            Case enmValve.No3, enmValve.No4
                Select Case onOff
                    Case enmONOFF.eON
                    Case enmONOFF.eOff
                End Select
        End Select
        Return False
    End Function


#End Region
  
#Region "程控光源"
    ''' <summary>CCD光源 Mapping</summary>
    ''' <param name="ccd"></param>
    ''' <param name="light"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CCDLightMapping(ByVal ccd As Integer, ByVal light As enmValveLight) As enmLight

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                Select Case ccd
                    Case enmCCD.CCD1
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No1
                            Case enmValveLight.No2
                                Return enmLight.No2
                            Case enmValveLight.No3
                                Return -1
                            Case enmValveLight.No4
                                Return -1
                        End Select
                    Case enmCCD.CCD2
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No3
                            Case enmValveLight.No2
                                Return enmLight.No4
                            Case enmValveLight.No3
                                Return -1
                            Case enmValveLight.No4
                                Return -1
                        End Select
                    Case enmCCD.CCD3
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No5
                            Case enmValveLight.No2
                                Return enmLight.No6
                            Case enmValveLight.No3
                                Return -1
                            Case enmValveLight.No4
                                Return -1
                        End Select
                    Case enmCCD.CCD4
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No7
                            Case enmValveLight.No2
                                Return enmLight.No8
                            Case enmValveLight.No3
                                Return -1
                            Case enmValveLight.No4
                                Return -1
                        End Select
                End Select

            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                Select Case ccd
                    Case enmCCD.CCD1
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No1
                            Case enmValveLight.No2
                                Return enmLight.No2
                            Case enmValveLight.No3
                                Return -1
                            Case enmValveLight.No4
                                Return -1
                        End Select
                    Case enmCCD.CCD2
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No3
                            Case enmValveLight.No2
                                Return enmLight.No4
                            Case enmValveLight.No3
                                Return -1
                            Case enmValveLight.No4
                                Return -1
                        End Select
                    Case Else
                        Return -1
                End Select

            Case Else
                Select Case ccd
                    Case enmCCD.CCD1
                        Select Case light
                            Case enmValveLight.No1
                                Return enmLight.No1
                            Case enmValveLight.No2
                                Return enmLight.No2
                            Case enmValveLight.No3
                                Return enmLight.No3
                            Case enmValveLight.No4
                                Return enmLight.No4
                        End Select
                    Case Else '不合法
                        Return -1
                End Select

        End Select
        Return -1
    End Function

    ''' <summary>[設定配接光源開關]</summary>
    ''' <param name="light"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub SetLightOnOff(ByVal light As enmLight, ByVal value As Boolean)
        Select Case light
            Case enmLight.No1
                gDOCollection.SetState(enmDO.CCDLight, value)
            Case enmLight.No2
                gDOCollection.SetState(enmDO.CCDLight2, value)
            Case enmLight.No3
                gDOCollection.SetState(enmDO.CCDLight3, value)
            Case enmLight.No4
                gDOCollection.SetState(enmDO.CCDLight4, value)
            Case enmLight.No5
                gDOCollection.SetState(enmDO.CCDLight5, value)
            Case enmLight.No6
                gDOCollection.SetState(enmDO.CCDLight6, value)
            Case enmLight.No7
                gDOCollection.SetState(enmDO.CCDLight7, value)
            Case enmLight.No8
                gDOCollection.SetState(enmDO.CCDLight8, value)
        End Select

    End Sub

#End Region
   
 
#Region "膠管氣壓"
    ''' <summary>[暫存氣壓值(Stage、Valve、Type)]</summary>
    ''' <remarks></remarks>
    Dim mAP_SVT(enmStage.Max, eValveWorkMode.MaxValve, eEPVPressureType.Max) As Decimal
    ''' <summary>[暫存氣壓值(Type、Index)]</summary>
    ''' <remarks></remarks>
    Dim mAP_TI(eEPVPressureType.Max, 100) As Decimal

    ''' <summary>設定膠管氣壓開關</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="onoff"></param>
    ''' <remarks></remarks>
    Sub SetSyringePressure(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByVal onoff As enmONOFF)
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                Select Case stageNo
                    Case enmStage.No1
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.SyringePressure1, IIf(onoff = enmONOFF.eON, True, False))
                        End Select

                    Case enmStage.No2
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.SyringePressure2, IIf(onoff = enmONOFF.eON, True, False))
                        End Select

                    Case enmStage.No3
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.SyringePressure3, IIf(onoff = enmONOFF.eON, True, False))
                        End Select

                    Case enmStage.No4
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.SyringePressure4, IIf(onoff = enmONOFF.eON, True, False))
                        End Select

                End Select

            Case Else
                '[Note]:只有一組Stage
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.SyringePressure1, IIf(onoff = enmONOFF.eON, True, False))
                        End Select

                    Case eMechanismModule.TwoValveOneStage
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.SyringePressure1, IIf(onoff = enmONOFF.eON, True, False))

                            Case eValveWorkMode.Valve2
                                gDOCollection.SetState(enmDO.SyringePressure2, IIf(onoff = enmONOFF.eON, True, False))
                        End Select

                End Select


        End Select

    End Sub


    ''' <summary>設定閥體氣壓開關</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="onoff"></param>
    ''' <remarks></remarks>
    Sub SetValvePressure(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByVal onoff As enmONOFF)
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                Select Case stageNo
                    Case enmStage.No1
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.ValvePressure1, IIf(onoff = enmONOFF.eON, True, False))
                        End Select

                    Case enmStage.No2
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.ValvePressure2, IIf(onoff = enmONOFF.eON, True, False))
                        End Select

                    Case enmStage.No3
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.ValvePressure3, IIf(onoff = enmONOFF.eON, True, False))
                        End Select

                    Case enmStage.No4
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.ValvePressure4, IIf(onoff = enmONOFF.eON, True, False))
                        End Select

                End Select

            Case Else
                '[Note]:只有一組Stage
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.ValvePressure1, IIf(onoff = enmONOFF.eON, True, False))
                        End Select

                    Case eMechanismModule.TwoValveOneStage
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.ValvePressure1, IIf(onoff = enmONOFF.eON, True, False))

                            Case eValveWorkMode.Valve2
                                gDOCollection.SetState(enmDO.ValvePressure2, IIf(onoff = enmONOFF.eON, True, False))
                        End Select

                End Select


        End Select

    End Sub

    ''' <summary>膠管氣壓開關對應</summary>
    ''' <param name="valveNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSyringeAirPressureState(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode) As Boolean
        Select Case gSSystemParameter.StageUseValveCount
            Case eMechanismModule.OneValveOneStage
                Select Case stageNo
                    Case enmStage.No1
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDOCollection.GetState(enmDO.SyringePressure1)

                            Case eValveWorkMode.Valve2
                                Return False

                        End Select

                    Case enmStage.No2
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDOCollection.GetState(enmDO.SyringePressure2)

                            Case eValveWorkMode.Valve2
                                Return False

                        End Select

                    Case enmStage.No3
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDOCollection.GetState(enmDO.SyringePressure3)

                            Case eValveWorkMode.Valve2
                                Return False

                        End Select

                    Case enmStage.No4
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDOCollection.GetState(enmDO.SyringePressure4)

                            Case eValveWorkMode.Valve2
                                Return False

                        End Select
                End Select

            Case eMechanismModule.TwoValveOneStage
                Select Case stageNo
                    Case enmStage.No1
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDOCollection.GetState(enmDO.SyringePressure1)

                            Case eValveWorkMode.Valve2
                                Return gDOCollection.GetState(enmDO.SyringePressure2)

                        End Select

                    Case enmStage.No2
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDOCollection.GetState(enmDO.SyringePressure3)

                            Case eValveWorkMode.Valve2
                                Return gDOCollection.GetState(enmDO.SyringePressure4)

                        End Select

                    Case enmStage.No3
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDOCollection.GetState(enmDO.SyringePressure3)

                            Case eValveWorkMode.Valve2
                                Return gDOCollection.GetState(enmDO.SyringePressure3)

                        End Select

                    Case enmStage.No4
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDOCollection.GetState(enmDO.SyringePressure4)

                            Case eValveWorkMode.Valve2
                                Return gDOCollection.GetState(enmDO.SyringePressure4)

                        End Select
                End Select
        End Select
        Return False
    End Function
    ''' <summary>[取得膠管氣壓最大值]</summary>
    ''' <param name="type"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAirPressureMax(ByVal type As eEPVPressureType, ByVal index As Integer) As Decimal
        Const FailedValue As Double = 0 '失效時傳回值
        Dim value As Decimal
        If gEPVCollection.GetMaxMpa(type, index, value) Then
            Return value
        Else
            Return FailedValue
        End If
    End Function


    ''' <summary>取得既有氣壓值</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAirPressureOld(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByVal type As eEPVPressureType) As Decimal
        Const FailedValue As Double = 0 '失效時傳回值
        If IsNumeric(gEPVCollection.Result(stageNo, valveNo, type).Value) Then
            Return gEPVCollection.Result(stageNo, valveNo, type).Value
        Else
            Return FailedValue
        End If
    End Function
    ''' <summary>取得既有氣壓值</summary>
    ''' <param name="type"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAirPressureOld(ByVal type As eEPVPressureType, ByVal index As Integer) As Decimal
        Const FailedValue As Double = 0 '失效時傳回值
        If IsNumeric(gEPVCollection.Result(type, index).Value) Then
            Return gEPVCollection.Result(type, index).Value
        Else
            Return FailedValue
        End If
    End Function

    ''' <summary>取得膠管氣壓值</summary>
    ''' <param name="stageNo"></param>
    ''' <param name="valveNo"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAirPressure(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByVal type As eEPVPressureType) As Decimal

        Task.Run(Sub()
                     Dim value As Decimal = 0
                     gEPVCollection.GetValue(stageNo, valveNo, type, value, True)
                     mAP_SVT(stageNo, valveNo, type) = value
                     Exit Sub
                 End Sub)
        Return mAP_SVT(stageNo, valveNo, type)
    End Function
    ''' <summary>取得膠管氣壓值</summary>
    ''' <param name="type"></param>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAirPressure(ByVal type As eEPVPressureType, ByVal index As Integer) As Decimal
        Task.Run(Sub()
                     Dim value As Decimal = 0
                     gEPVCollection.GetValue(type, index, value, True)
                     mAP_TI(type, index) = value
                     Exit Sub
                 End Sub)
        Return mAP_TI(type, index)
    End Function

#End Region

#Region "Purge杯"
    ''' <summary>Purge杯真空設定</summary>
    ''' <param name="valveNo"></param>
    ''' <param name="isOn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetPurgeVacuum(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode, ByVal isOn As enmONOFF) As Boolean

        Dim mIsOn As Boolean = False
        If isOn = enmONOFF.eON Then mIsOn = True

        Select Case gSSystemParameter.StageUseValveCount
            Case eMechanismModule.OneValveOneStage
                Select Case stageNo
                    Case enmStage.No1
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.Purge, mIsOn)

                            Case eValveWorkMode.Valve2
                                Return False

                        End Select

                    Case enmStage.No2
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.PurgeVacuum2, mIsOn)

                            Case eValveWorkMode.Valve2
                                Return False

                        End Select
                    Case enmStage.No3
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.PurgeVacuum3, mIsOn)

                            Case eValveWorkMode.Valve2
                                Return False

                        End Select

                    Case enmStage.No4
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.PurgeVacuum4, mIsOn)

                            Case eValveWorkMode.Valve2
                                Return False

                        End Select
                End Select

            Case eMechanismModule.TwoValveOneStage
                Select Case stageNo
                    Case enmStage.No1
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.Purge, mIsOn)

                            Case eValveWorkMode.Valve2
                                gDOCollection.SetState(enmDO.Purge, mIsOn)

                        End Select

                    Case enmStage.No2
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.PurgeVacuum2, mIsOn)

                            Case eValveWorkMode.Valve2
                                gDOCollection.SetState(enmDO.PurgeVacuum2, mIsOn)

                        End Select

                    Case enmStage.No3
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.PurgeVacuum3, mIsOn)

                            Case eValveWorkMode.Valve2
                                gDOCollection.SetState(enmDO.PurgeVacuum3, mIsOn)

                        End Select

                    Case enmStage.No4
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                gDOCollection.SetState(enmDO.PurgeVacuum4, mIsOn)

                            Case eValveWorkMode.Valve2
                                gDOCollection.SetState(enmDO.PurgeVacuum4, mIsOn)

                        End Select

                End Select

        End Select
        Return True
    End Function

    ''' <summary>Purge杯真空建立穩定</summary>
    ''' <param name="valveNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsPurgeVacuumReady(ByVal stageNo As enmStage, ByVal valveNo As eValveWorkMode) As Boolean

        Select Case gSSystemParameter.StageUseValveCount
            Case eMechanismModule.OneValveOneStage
                Select Case stageNo
                    Case enmStage.No1
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDICollection.GetState(enmDI.PurgeVacuumReady, True)

                            Case eValveWorkMode.Valve2
                                Return False

                        End Select

                    Case enmStage.No2
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDICollection.GetState(enmDI.PurgeVacuumReady2, True)

                            Case eValveWorkMode.Valve2
                                Return False

                        End Select

                    Case enmStage.No3
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDICollection.GetState(enmDI.PurgeVacuumReady3, True)

                            Case eValveWorkMode.Valve2
                                Return False

                        End Select

                    Case enmStage.No4
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDICollection.GetState(enmDI.PurgeVacuumReady4, True)

                            Case eValveWorkMode.Valve2
                                Return False

                        End Select

                End Select
            Case eMechanismModule.TwoValveOneStage
                Select Case stageNo
                    Case enmStage.No1
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDICollection.GetState(enmDI.PurgeVacuumReady, True)

                            Case eValveWorkMode.Valve2
                                Return gDICollection.GetState(enmDI.PurgeVacuumReady, True)

                        End Select

                    Case enmStage.No2
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDICollection.GetState(enmDI.PurgeVacuumReady2, True)

                            Case eValveWorkMode.Valve2
                                Return gDICollection.GetState(enmDI.PurgeVacuumReady2, True)

                        End Select

                    Case enmStage.No3
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDICollection.GetState(enmDI.PurgeVacuumReady3, True)

                            Case eValveWorkMode.Valve2
                                Return gDICollection.GetState(enmDI.PurgeVacuumReady3, True)

                        End Select

                    Case enmStage.No4
                        Select Case valveNo
                            Case eValveWorkMode.Valve1
                                Return gDICollection.GetState(enmDI.PurgeVacuumReady4, True)

                            Case eValveWorkMode.Valve2
                                Return gDICollection.GetState(enmDI.PurgeVacuumReady4, True)

                        End Select

                End Select
        End Select
        Return gDICollection.GetState(enmDI.PurgeVacuumReady, True)

    End Function
#End Region


#Region "系統配接"

    ''' <summary>F350A系統配接設定</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSysParam350A() As Boolean
        gSYS(eSys.MachineA).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.Conveyor1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.Conveyor2).ConveyorNo = enmConveyor.No2
        gSYS(eSys.Conveyor1).AxisConverter = enmAxis.Converter
        gSYS(eSys.Conveyor2).AxisConverter = enmAxis.Converter

        gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.DispStage1).ValveNo(0) = enmValve.No1
        gSYS(eSys.DispStage1).ValveNo(1) = enmValve.No2
        gSYS(eSys.DispStage1).CCDNo = enmCCD.CCD1
        gSYS(eSys.DispStage1).StageNo = enmStage.No1
        gSYS(eSys.DispStage1).BalanceNo = enmBalance.No1
        gSYS(eSys.DispStage1).PinNo = enmStage.No1
        gSYS(eSys.DispStage1).FMCSNo = 0
        gSYS(eSys.DispStage1).ClearNo = enmStage.No1
        gSYS(eSys.DispStage1).LaserNo = enmLaserReader.No1
        gSYS(eSys.DispStage1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.DispStage1).AxisX = enmAxis.XAxis
        gSYS(eSys.DispStage1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.DispStage1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.DispStage1).AxisA = enmAxis.AAxis
        gSYS(eSys.DispStage1).AxisB = enmAxis.BAxis
        gSYS(eSys.DispStage1).AxisC = enmAxis.CAxis
        gSYS(eSys.DispStage1).EPVNo = enmEPV.No1
        gSYS(eSys.DispStage1).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.DispStage1).EsysNum = eSys.DispStage1

        gSYS(eSys.SubDisp1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.SubDisp1).ValveNo(0) = enmValve.No1
        gSYS(eSys.SubDisp1).ValveNo(1) = enmValve.No2
        gSYS(eSys.SubDisp1).CCDNo = enmCCD.CCD1
        gSYS(eSys.SubDisp1).StageNo = enmStage.No1
        gSYS(eSys.SubDisp1).BalanceNo = enmBalance.No1
        gSYS(eSys.SubDisp1).PinNo = enmStage.No1
        gSYS(eSys.SubDisp1).FMCSNo = 0
        gSYS(eSys.SubDisp1).ClearNo = enmStage.No1
        gSYS(eSys.SubDisp1).LaserNo = enmLaserReader.No1
        gSYS(eSys.SubDisp1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.SubDisp1).AxisX = enmAxis.XAxis
        gSYS(eSys.SubDisp1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.SubDisp1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.SubDisp1).AxisA = enmAxis.AAxis
        gSYS(eSys.SubDisp1).AxisB = enmAxis.BAxis
        gSYS(eSys.SubDisp1).AxisC = enmAxis.CAxis
        gSYS(eSys.SubDisp1).EPVNo = enmEPV.No1
        gSYS(eSys.SubDisp1).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.SubDisp1).EsysNum = eSys.DispStage1
        gSYS(eSys.SubDisp1).ValveControllerNo1 = enmValveController.No1
        gSYS(eSys.SubDisp1).ValveControllerNo2 = enmValveController.No1

        gSYS(eSys.MonitorDisp1).StageNo = enmStage.No1
        gSYS(eSys.MonitorDisp1).ValveControllerNo1 = enmValveController.No1
        gSYS(eSys.MonitorDisp1).ValveControllerNo2 = enmValveController.No1
        Return True
    End Function
    ''' <summary>F230A系統配接設定</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSysParamF230A() As Boolean
        gSYS(eSys.MachineA).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.Conveyor1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.Conveyor1).AxisConverter = enmAxis.Converter

        gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.DispStage1).ValveNo(0) = enmValve.No1
        gSYS(eSys.DispStage1).CCDNo = enmCCD.CCD1
        gSYS(eSys.DispStage1).StageNo = enmStage.No1
        gSYS(eSys.DispStage1).BalanceNo = enmBalance.No1
        gSYS(eSys.DispStage1).PinNo = enmStage.No1
        gSYS(eSys.DispStage1).FMCSNo = 0
        gSYS(eSys.DispStage1).ClearNo = enmStage.No1
        gSYS(eSys.DispStage1).LaserNo = enmLaserReader.No1
        gSYS(eSys.DispStage1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.DispStage1).AxisX = enmAxis.XAxis
        gSYS(eSys.DispStage1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.DispStage1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.DispStage1).AxisA = enmAxis.AAxis
        gSYS(eSys.DispStage1).AxisB = enmAxis.BAxis
        gSYS(eSys.DispStage1).AxisC = enmAxis.CAxis
        gSYS(eSys.DispStage1).EPVNo = enmEPV.No1
        gSYS(eSys.DispStage1).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.DispStage1).EsysNum = eSys.DispStage1

        gSYS(eSys.SubDisp1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.SubDisp1).ValveNo(0) = enmValve.No1
        gSYS(eSys.SubDisp1).CCDNo = enmCCD.CCD1
        gSYS(eSys.SubDisp1).StageNo = enmStage.No1
        gSYS(eSys.SubDisp1).BalanceNo = enmBalance.No1
        gSYS(eSys.SubDisp1).PinNo = enmStage.No1
        gSYS(eSys.SubDisp1).FMCSNo = 0
        gSYS(eSys.SubDisp1).ClearNo = enmStage.No1
        gSYS(eSys.SubDisp1).LaserNo = enmLaserReader.No1
        gSYS(eSys.SubDisp1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.SubDisp1).AxisX = enmAxis.XAxis
        gSYS(eSys.SubDisp1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.SubDisp1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.SubDisp1).AxisA = enmAxis.AAxis
        gSYS(eSys.SubDisp1).AxisB = enmAxis.BAxis
        gSYS(eSys.SubDisp1).AxisC = enmAxis.CAxis
        gSYS(eSys.SubDisp1).EPVNo = enmEPV.No1
        gSYS(eSys.SubDisp1).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.SubDisp1).EsysNum = eSys.DispStage1
        gSYS(eSys.SubDisp1).ValveControllerNo1 = enmValveController.No1
        gSYS(eSys.SubDisp1).ValveControllerNo2 = enmValveController.No1

        gSYS(eSys.MonitorDisp1).StageNo = enmStage.No1
        gSYS(eSys.MonitorDisp1).ValveControllerNo1 = enmValveController.No1
        gSYS(eSys.MonitorDisp1).ValveControllerNo2 = enmValveController.No1

        Return True
    End Function
    ''' <summary>800AQ系統配接設定</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSystemParamW800QA() As Boolean
        gSYS(eSys.MachineA).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.MachineB).MachineNo = enmMachineStation.MachineB
        gSYS(eSys.Conveyor1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.Conveyor1).AxisConverter = enmAxis.Converter

        gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.DispStage1).ValveNo(0) = enmValve.No1
        gSYS(eSys.DispStage1).CCDNo = enmCCD.CCD1
        gSYS(eSys.DispStage1).StageNo = enmStage.No1
        gSYS(eSys.DispStage1).BalanceNo = enmBalance.No1
        gSYS(eSys.DispStage1).PinNo = enmStage.No1
        gSYS(eSys.DispStage1).FMCSNo = 0
        gSYS(eSys.DispStage1).ClearNo = enmStage.No1
        gSYS(eSys.DispStage1).LaserNo = enmLaserReader.No1
        gSYS(eSys.DispStage1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.DispStage1).AxisX = enmAxis.XAxis
        gSYS(eSys.DispStage1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.DispStage1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.DispStage1).AxisA = enmAxis.AAxis
        gSYS(eSys.DispStage1).AxisB = enmAxis.BAxis
        gSYS(eSys.DispStage1).AxisC = enmAxis.CAxis
        gSYS(eSys.DispStage1).EPVNo = enmEPV.No1
        gSYS(eSys.DispStage1).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.DispStage1).EsysNum = eSys.DispStage1
        gSYS(eSys.DispStage1).ValveControllerNo1 = enmValveController.No1

        gSYS(eSys.DispStage2).ValveNo(0) = enmValve.No2
        gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.DispStage2).CCDNo = enmCCD.CCD2
        gSYS(eSys.DispStage2).StageNo = enmStage.No2
        gSYS(eSys.DispStage2).BalanceNo = enmBalance.No1
        gSYS(eSys.DispStage2).PinNo = enmStage.No2
        gSYS(eSys.DispStage2).FMCSNo = 1
        gSYS(eSys.DispStage2).ClearNo = enmStage.No2
        gSYS(eSys.DispStage2).LaserNo = enmLaserReader.No2
        gSYS(eSys.DispStage2).ConveyorNo = enmConveyor.No1
        gSYS(eSys.DispStage2).AxisX = enmAxis.UAxis
        gSYS(eSys.DispStage2).AxisY = enmAxis.VAxis
        gSYS(eSys.DispStage2).AxisZ = enmAxis.WAxis
        gSYS(eSys.DispStage2).AxisA = enmAxis.DAxis
        gSYS(eSys.DispStage2).AxisB = enmAxis.EAxis
        gSYS(eSys.DispStage2).AxisC = enmAxis.FAxis
        gSYS(eSys.DispStage2).EPVNo = enmEPV.No2
        gSYS(eSys.DispStage2).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.DispStage2).EsysNum = eSys.DispStage2
        gSYS(eSys.DispStage2).ValveControllerNo1 = enmValveController.No2

        gSYS(eSys.DispStage3).ValveNo(0) = enmValve.No3
        gSYS(eSys.DispStage3).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.DispStage3).CCDNo = enmCCD.CCD3
        gSYS(eSys.DispStage3).StageNo = enmStage.No3
        gSYS(eSys.DispStage3).BalanceNo = enmBalance.No2
        gSYS(eSys.DispStage3).PinNo = enmStage.No3
        gSYS(eSys.DispStage3).FMCSNo = 2
        gSYS(eSys.DispStage3).ClearNo = enmStage.No3
        gSYS(eSys.DispStage3).LaserNo = enmLaserReader.No3
        gSYS(eSys.DispStage3).ConveyorNo = enmConveyor.No1
        gSYS(eSys.DispStage3).AxisX = enmAxis.RAxis
        gSYS(eSys.DispStage3).AxisY = enmAxis.SAxis
        gSYS(eSys.DispStage3).AxisZ = enmAxis.TAxis
        gSYS(eSys.DispStage3).AxisA = enmAxis.GAxis
        gSYS(eSys.DispStage3).AxisB = enmAxis.HAxis
        gSYS(eSys.DispStage3).AxisC = enmAxis.IAxis
        gSYS(eSys.DispStage3).EPVNo = enmEPV.No3
        gSYS(eSys.DispStage3).MachineNo = enmMachineStation.MachineB
        gSYS(eSys.DispStage3).EsysNum = eSys.DispStage3
        gSYS(eSys.DispStage3).ValveControllerNo1 = enmValveController.No3

        gSYS(eSys.DispStage4).ValveNo(0) = enmValve.No4
        gSYS(eSys.DispStage4).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.DispStage4).CCDNo = enmCCD.CCD4
        gSYS(eSys.DispStage4).StageNo = enmStage.No4
        gSYS(eSys.DispStage4).BalanceNo = enmBalance.No2
        gSYS(eSys.DispStage4).PinNo = enmStage.No4
        gSYS(eSys.DispStage4).FMCSNo = 3
        gSYS(eSys.DispStage4).ClearNo = enmStage.No3
        gSYS(eSys.DispStage4).LaserNo = enmLaserReader.No4
        gSYS(eSys.DispStage4).ConveyorNo = enmConveyor.No1
        gSYS(eSys.DispStage4).AxisX = enmAxis.OAxis
        gSYS(eSys.DispStage4).AxisY = enmAxis.PAxis
        gSYS(eSys.DispStage4).AxisZ = enmAxis.QAxis
        gSYS(eSys.DispStage4).AxisA = enmAxis.JAxis
        gSYS(eSys.DispStage4).AxisB = enmAxis.KAxis
        gSYS(eSys.DispStage4).AxisC = enmAxis.LAxis
        gSYS(eSys.DispStage4).EPVNo = enmEPV.No4
        gSYS(eSys.DispStage4).MachineNo = enmMachineStation.MachineB
        gSYS(eSys.DispStage4).EsysNum = eSys.DispStage4
        gSYS(eSys.DispStage4).ValveControllerNo1 = enmValveController.No4

        gSYS(eSys.SubDisp1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.SubDisp1).ValveNo(0) = enmValve.No1
        gSYS(eSys.SubDisp1).CCDNo = enmCCD.CCD1
        gSYS(eSys.SubDisp1).StageNo = enmStage.No1
        gSYS(eSys.SubDisp1).BalanceNo = enmBalance.No1
        gSYS(eSys.SubDisp1).PinNo = enmStage.No1
        gSYS(eSys.SubDisp1).FMCSNo = 0
        gSYS(eSys.SubDisp1).ClearNo = enmStage.No1
        gSYS(eSys.SubDisp1).LaserNo = enmLaserReader.No1
        gSYS(eSys.SubDisp1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.SubDisp1).AxisX = enmAxis.XAxis
        gSYS(eSys.SubDisp1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.SubDisp1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.SubDisp1).AxisA = enmAxis.AAxis
        gSYS(eSys.SubDisp1).AxisB = enmAxis.BAxis
        gSYS(eSys.SubDisp1).AxisC = enmAxis.CAxis
        gSYS(eSys.SubDisp1).EPVNo = enmEPV.No1
        gSYS(eSys.SubDisp1).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.SubDisp1).EsysNum = eSys.DispStage1
        gSYS(eSys.SubDisp1).ValveControllerNo1 = enmValveController.No1

        gSYS(eSys.SubDisp2).ValveNo(0) = enmValve.No2
        gSYS(eSys.SubDisp2).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.SubDisp2).CCDNo = enmCCD.CCD2
        gSYS(eSys.SubDisp2).StageNo = enmStage.No2
        gSYS(eSys.SubDisp2).BalanceNo = enmBalance.No1
        gSYS(eSys.SubDisp2).PinNo = enmStage.No2
        gSYS(eSys.SubDisp2).FMCSNo = 1
        gSYS(eSys.SubDisp2).ClearNo = enmStage.No2
        gSYS(eSys.SubDisp2).LaserNo = enmLaserReader.No2
        gSYS(eSys.SubDisp2).ConveyorNo = enmConveyor.No1
        gSYS(eSys.SubDisp2).AxisX = enmAxis.UAxis
        gSYS(eSys.SubDisp2).AxisY = enmAxis.VAxis
        gSYS(eSys.SubDisp2).AxisZ = enmAxis.WAxis
        gSYS(eSys.SubDisp2).AxisA = enmAxis.DAxis
        gSYS(eSys.SubDisp2).AxisB = enmAxis.EAxis
        gSYS(eSys.SubDisp2).AxisC = enmAxis.FAxis
        gSYS(eSys.SubDisp2).EPVNo = enmEPV.No2
        gSYS(eSys.SubDisp2).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.SubDisp2).EsysNum = eSys.DispStage2
        gSYS(eSys.SubDisp2).ValveControllerNo1 = enmValveController.No2

        gSYS(eSys.SubDisp3).ValveNo(0) = enmValve.No3
        gSYS(eSys.SubDisp3).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.SubDisp3).CCDNo = enmCCD.CCD3
        gSYS(eSys.SubDisp3).StageNo = enmStage.No3
        gSYS(eSys.SubDisp3).BalanceNo = enmBalance.No2
        gSYS(eSys.SubDisp3).PinNo = enmStage.No3
        gSYS(eSys.SubDisp3).FMCSNo = 2
        gSYS(eSys.SubDisp3).ClearNo = enmStage.No3
        gSYS(eSys.SubDisp3).LaserNo = enmLaserReader.No3
        gSYS(eSys.SubDisp3).ConveyorNo = enmConveyor.No1
        gSYS(eSys.SubDisp3).AxisX = enmAxis.RAxis
        gSYS(eSys.SubDisp3).AxisY = enmAxis.SAxis
        gSYS(eSys.SubDisp3).AxisZ = enmAxis.TAxis
        gSYS(eSys.SubDisp3).AxisA = enmAxis.GAxis
        gSYS(eSys.SubDisp3).AxisB = enmAxis.HAxis
        gSYS(eSys.SubDisp3).AxisC = enmAxis.IAxis
        gSYS(eSys.SubDisp3).EPVNo = enmEPV.No3
        gSYS(eSys.SubDisp3).MachineNo = enmMachineStation.MachineB
        gSYS(eSys.SubDisp3).EsysNum = eSys.DispStage3
        gSYS(eSys.SubDisp3).ValveControllerNo1 = enmValveController.No3

        gSYS(eSys.SubDisp4).ValveNo(0) = enmValve.No4
        gSYS(eSys.SubDisp4).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.SubDisp4).CCDNo = enmCCD.CCD4
        gSYS(eSys.SubDisp4).StageNo = enmStage.No4
        gSYS(eSys.SubDisp4).BalanceNo = enmBalance.No2
        gSYS(eSys.SubDisp4).PinNo = enmStage.No4
        gSYS(eSys.SubDisp4).FMCSNo = 3
        gSYS(eSys.SubDisp4).ClearNo = enmStage.No3
        gSYS(eSys.SubDisp4).LaserNo = enmLaserReader.No4
        gSYS(eSys.SubDisp4).ConveyorNo = enmConveyor.No1
        gSYS(eSys.SubDisp4).AxisX = enmAxis.OAxis
        gSYS(eSys.SubDisp4).AxisY = enmAxis.PAxis
        gSYS(eSys.SubDisp4).AxisZ = enmAxis.QAxis
        gSYS(eSys.SubDisp4).AxisA = enmAxis.JAxis
        gSYS(eSys.SubDisp4).AxisB = enmAxis.KAxis
        gSYS(eSys.SubDisp4).AxisC = enmAxis.LAxis
        gSYS(eSys.SubDisp4).EPVNo = enmEPV.No4
        gSYS(eSys.SubDisp4).MachineNo = enmMachineStation.MachineB
        gSYS(eSys.SubDisp4).EsysNum = eSys.DispStage4
        gSYS(eSys.SubDisp4).ValveControllerNo1 = enmValveController.No4

        gSYS(eSys.MonitorDisp1).StageNo = enmStage.No1
        gSYS(eSys.MonitorDisp2).StageNo = enmStage.No2
        gSYS(eSys.MonitorDisp3).StageNo = enmStage.No3
        gSYS(eSys.MonitorDisp4).StageNo = enmStage.No4
        gSYS(eSys.MonitorDisp1).ValveControllerNo1 = enmValveController.No1
        gSYS(eSys.MonitorDisp2).ValveControllerNo1 = enmValveController.No2
        gSYS(eSys.MonitorDisp3).ValveControllerNo1 = enmValveController.No3
        gSYS(eSys.MonitorDisp4).ValveControllerNo1 = enmValveController.No4
        Return True

    End Function

    ''' <summary>800AQ系統配接設定</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSystemParam500AD() As Boolean
        gSYS(eSys.MachineA).MachineNo = enmMachineStation.MachineA

        gSYS(eSys.Conveyor1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.Conveyor1).AxisConverter = enmAxis.Converter

        gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.DispStage1).ValveNo(0) = enmValve.No1
        gSYS(eSys.DispStage1).CCDNo = enmCCD.CCD1
        gSYS(eSys.DispStage1).StageNo = enmStage.No1
        gSYS(eSys.DispStage1).BalanceNo = enmBalance.No1
        gSYS(eSys.DispStage1).PinNo = enmStage.No1
        gSYS(eSys.DispStage1).FMCSNo = 0
        gSYS(eSys.DispStage1).ClearNo = enmStage.No1
        gSYS(eSys.DispStage1).LaserNo = enmLaserReader.No1
        gSYS(eSys.DispStage1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.DispStage1).AxisX = enmAxis.XAxis
        gSYS(eSys.DispStage1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.DispStage1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.DispStage1).AxisA = enmAxis.AAxis
        gSYS(eSys.DispStage1).AxisB = enmAxis.BAxis
        gSYS(eSys.DispStage1).AxisC = enmAxis.CAxis
        gSYS(eSys.DispStage1).EPVNo = enmEPV.No1
        gSYS(eSys.DispStage1).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.DispStage1).EsysNum = eSys.DispStage1
        gSYS(eSys.DispStage1).ValveControllerNo1 = enmValveController.No1

        gSYS(eSys.DispStage2).ValveNo(0) = enmValve.No2
        gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.DispStage2).CCDNo = enmCCD.CCD2
        gSYS(eSys.DispStage2).StageNo = enmStage.No2
        gSYS(eSys.DispStage2).BalanceNo = enmBalance.No1
        gSYS(eSys.DispStage2).PinNo = enmStage.No2
        gSYS(eSys.DispStage2).FMCSNo = 1
        gSYS(eSys.DispStage2).ClearNo = enmStage.No2
        gSYS(eSys.DispStage2).LaserNo = enmLaserReader.No2
        gSYS(eSys.DispStage2).ConveyorNo = enmConveyor.No1
        gSYS(eSys.DispStage2).AxisX = enmAxis.UAxis
        gSYS(eSys.DispStage2).AxisY = enmAxis.VAxis
        gSYS(eSys.DispStage2).AxisZ = enmAxis.WAxis
        gSYS(eSys.DispStage2).AxisA = enmAxis.DAxis
        gSYS(eSys.DispStage2).AxisB = enmAxis.EAxis
        gSYS(eSys.DispStage2).AxisC = enmAxis.FAxis
        gSYS(eSys.DispStage2).EPVNo = enmEPV.No2
        gSYS(eSys.DispStage2).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.DispStage2).EsysNum = eSys.DispStage2
        gSYS(eSys.DispStage2).ValveControllerNo1 = enmValveController.No2


        gSYS(eSys.SubDisp1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.SubDisp1).ValveNo(0) = enmValve.No1
        gSYS(eSys.SubDisp1).CCDNo = enmCCD.CCD1
        gSYS(eSys.SubDisp1).StageNo = enmStage.No1
        gSYS(eSys.SubDisp1).BalanceNo = enmBalance.No1
        gSYS(eSys.SubDisp1).PinNo = enmStage.No1
        gSYS(eSys.SubDisp1).FMCSNo = 0
        gSYS(eSys.SubDisp1).ClearNo = enmStage.No1
        gSYS(eSys.SubDisp1).LaserNo = enmLaserReader.No1
        gSYS(eSys.SubDisp1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.SubDisp1).AxisX = enmAxis.XAxis
        gSYS(eSys.SubDisp1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.SubDisp1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.SubDisp1).AxisA = enmAxis.AAxis
        gSYS(eSys.SubDisp1).AxisB = enmAxis.BAxis
        gSYS(eSys.SubDisp1).AxisC = enmAxis.CAxis
        gSYS(eSys.SubDisp1).EPVNo = enmEPV.No1
        gSYS(eSys.SubDisp1).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.SubDisp1).EsysNum = eSys.DispStage1
        gSYS(eSys.SubDisp1).ValveControllerNo1 = enmValveController.No1

        gSYS(eSys.SubDisp2).ValveNo(0) = enmValve.No2
        gSYS(eSys.SubDisp2).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.SubDisp2).CCDNo = enmCCD.CCD2
        gSYS(eSys.SubDisp2).StageNo = enmStage.No2
        gSYS(eSys.SubDisp2).BalanceNo = enmBalance.No1
        gSYS(eSys.SubDisp2).PinNo = enmStage.No2
        gSYS(eSys.SubDisp2).FMCSNo = 1
        gSYS(eSys.SubDisp2).ClearNo = enmStage.No2
        gSYS(eSys.SubDisp2).LaserNo = enmLaserReader.No2
        gSYS(eSys.SubDisp2).ConveyorNo = enmConveyor.No1
        gSYS(eSys.SubDisp2).AxisX = enmAxis.UAxis
        gSYS(eSys.SubDisp2).AxisY = enmAxis.VAxis
        gSYS(eSys.SubDisp2).AxisZ = enmAxis.WAxis
        gSYS(eSys.SubDisp2).AxisA = enmAxis.DAxis
        gSYS(eSys.SubDisp2).AxisB = enmAxis.EAxis
        gSYS(eSys.SubDisp2).AxisC = enmAxis.FAxis
        gSYS(eSys.SubDisp2).EPVNo = enmEPV.No2
        gSYS(eSys.SubDisp2).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.SubDisp2).EsysNum = eSys.DispStage2
        gSYS(eSys.SubDisp2).ValveControllerNo1 = enmValveController.No2


        gSYS(eSys.MonitorDisp1).StageNo = enmStage.No1
        gSYS(eSys.MonitorDisp2).StageNo = enmStage.No2
        gSYS(eSys.MonitorDisp3).StageNo = enmStage.No3
        gSYS(eSys.MonitorDisp4).StageNo = enmStage.No4
        gSYS(eSys.MonitorDisp1).ValveControllerNo1 = enmValveController.No1
        gSYS(eSys.MonitorDisp2).ValveControllerNo1 = enmValveController.No2
        gSYS(eSys.MonitorDisp3).ValveControllerNo1 = enmValveController.No3
        gSYS(eSys.MonitorDisp4).ValveControllerNo1 = enmValveController.No4
        Return True

    End Function

    ''' <summary>雙平台雙閥系統配接設定</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSystemParam2S2V() As Boolean
        gSYS(eSys.MachineA).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.Conveyor1).ConveyorNo = eConveyor.ConveyorNo1
        gSYS(eSys.Conveyor1).AxisConverter = enmAxis.Converter

        gSYS(eSys.DispStage1).ValveNo(0) = enmValve.No1
        gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.DispStage1).CCDNo = enmCCD.CCD1
        gSYS(eSys.DispStage1).StageNo = enmStage.No1
        gSYS(eSys.DispStage1).BalanceNo = enmBalance.No1
        gSYS(eSys.DispStage1).PinNo = 0
        gSYS(eSys.DispStage1).FMCSNo = enmValve.No1
        gSYS(eSys.DispStage1).ClearNo = 0
        gSYS(eSys.DispStage1).LaserNo = enmLaserReader.No1
        gSYS(eSys.DispStage1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.DispStage1).AxisX = enmAxis.XAxis
        gSYS(eSys.DispStage1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.DispStage1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.DispStage1).AxisA = enmAxis.AAxis
        gSYS(eSys.DispStage1).AxisB = enmAxis.BAxis
        gSYS(eSys.DispStage1).AxisC = enmAxis.CAxis
        gSYS(eSys.DispStage1).EPVNo = enmEPV.No1
        gSYS(eSys.DispStage1).Timer.Restart()
        gSYS(eSys.DispStage1).MachineNo = 0
        gSYS(eSys.DispStage1).EsysNum = eSys.DispStage1
        gSYS(eSys.DispStage1).ValveControllerNo1 = enmValveController.No1

        gSYS(eSys.DispStage2).ValveNo(0) = enmValve.No2
        gSYS(eSys.DispStage2).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.DispStage2).CCDNo = enmCCD.CCD2
        gSYS(eSys.DispStage2).StageNo = enmStage.No2
        gSYS(eSys.DispStage2).BalanceNo = enmBalance.No1
        gSYS(eSys.DispStage2).PinNo = 0
        gSYS(eSys.DispStage2).FMCSNo = enmValve.No2
        gSYS(eSys.DispStage2).ClearNo = 0
        gSYS(eSys.DispStage2).LaserNo = enmLaserReader.No2
        gSYS(eSys.DispStage2).ConveyorNo = enmConveyor.No1
        gSYS(eSys.DispStage2).AxisX = enmAxis.UAxis
        gSYS(eSys.DispStage2).AxisY = enmAxis.VAxis
        gSYS(eSys.DispStage2).AxisZ = enmAxis.WAxis
        gSYS(eSys.DispStage2).AxisA = enmAxis.DAxis
        gSYS(eSys.DispStage2).AxisB = enmAxis.EAxis
        gSYS(eSys.DispStage2).AxisC = enmAxis.FAxis
        gSYS(eSys.DispStage2).EPVNo = enmEPV.No2
        gSYS(eSys.DispStage2).Timer.Restart()
        gSYS(eSys.DispStage2).MachineNo = 0
        gSYS(eSys.DispStage2).EsysNum = eSys.DispStage2
        gSYS(eSys.DispStage2).ValveControllerNo1 = enmValveController.No2

        gSYS(eSys.SubDisp1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.SubDisp1).ValveNo(0) = enmValve.No1
        gSYS(eSys.SubDisp1).CCDNo = enmCCD.CCD1
        gSYS(eSys.SubDisp1).StageNo = enmStage.No1
        gSYS(eSys.SubDisp1).BalanceNo = enmBalance.No1
        gSYS(eSys.SubDisp1).PinNo = enmStage.No1
        gSYS(eSys.SubDisp1).FMCSNo = 0
        gSYS(eSys.SubDisp1).ClearNo = enmStage.No1
        gSYS(eSys.SubDisp1).LaserNo = enmLaserReader.No1
        gSYS(eSys.SubDisp1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.SubDisp1).AxisX = enmAxis.XAxis
        gSYS(eSys.SubDisp1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.SubDisp1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.SubDisp1).AxisA = enmAxis.AAxis
        gSYS(eSys.SubDisp1).AxisB = enmAxis.BAxis
        gSYS(eSys.SubDisp1).AxisC = enmAxis.CAxis
        gSYS(eSys.SubDisp1).EPVNo = enmEPV.No1
        gSYS(eSys.SubDisp1).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.SubDisp1).EsysNum = eSys.DispStage1
        gSYS(eSys.SubDisp1).ValveControllerNo1 = enmValveController.No1
        gSYS(eSys.SubDisp1).ValveControllerNo2 = enmValveController.No1

        gSYS(eSys.SubDisp2).ValveNo(0) = enmValve.No2
        gSYS(eSys.SubDisp2).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.SubDisp2).CCDNo = enmCCD.CCD2
        gSYS(eSys.SubDisp2).StageNo = enmStage.No2
        gSYS(eSys.SubDisp2).BalanceNo = enmBalance.No1
        gSYS(eSys.SubDisp2).PinNo = enmStage.No2
        gSYS(eSys.SubDisp2).FMCSNo = 1
        gSYS(eSys.SubDisp2).ClearNo = enmStage.No2
        gSYS(eSys.SubDisp2).LaserNo = enmLaserReader.No2
        gSYS(eSys.SubDisp2).ConveyorNo = enmConveyor.No1
        gSYS(eSys.SubDisp2).AxisX = enmAxis.UAxis
        gSYS(eSys.SubDisp2).AxisY = enmAxis.VAxis
        gSYS(eSys.SubDisp2).AxisZ = enmAxis.WAxis
        gSYS(eSys.SubDisp2).AxisA = enmAxis.DAxis
        gSYS(eSys.SubDisp2).AxisB = enmAxis.EAxis
        gSYS(eSys.SubDisp2).AxisC = enmAxis.FAxis
        gSYS(eSys.SubDisp2).EPVNo = enmEPV.No2
        gSYS(eSys.SubDisp2).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.SubDisp2).EsysNum = eSys.DispStage2
        gSYS(eSys.SubDisp2).ValveControllerNo1 = enmValveController.No2
        gSYS(eSys.SubDisp2).ValveControllerNo2 = enmValveController.No2

        gSYS(eSys.MonitorDisp1).StageNo = enmStage.No1
        gSYS(eSys.MonitorDisp2).StageNo = enmStage.No2
        Return True
    End Function
    ''' <summary>300A系統配接設定</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSystemParam300A() As Boolean
        gSYS(eSys.MachineA).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.Conveyor1).ConveyorNo = eConveyor.ConveyorNo1
        gSYS(eSys.Conveyor1).AxisConverter = enmAxis.Converter

        gSYS(eSys.DispStage1).ValveNo(0) = enmValve.No1
        gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.DispStage1).CCDNo = enmCCD.CCD1
        gSYS(eSys.DispStage1).StageNo = enmStage.No1
        gSYS(eSys.DispStage1).BalanceNo = enmBalance.No1
        gSYS(eSys.DispStage1).PinNo = 0
        gSYS(eSys.DispStage1).FMCSNo = 0
        gSYS(eSys.DispStage1).ClearNo = 0
        gSYS(eSys.DispStage1).LaserNo = 0
        gSYS(eSys.DispStage1).ConveyorNo = eConveyor.ConveyorNo1
        gSYS(eSys.DispStage1).AxisX = enmAxis.XAxis
        gSYS(eSys.DispStage1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.DispStage1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.DispStage1).AxisB = enmAxis.BAxis
        gSYS(eSys.DispStage1).AxisC = enmAxis.CAxis
        gSYS(eSys.DispStage1).EPVNo = enmEPV.No1
        gSYS(eSys.DispStage1).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.DispStage1).EsysNum = eSys.DispStage1
        gSYS(eSys.DispStage1).ValveControllerNo1 = enmValveController.No1

        gSYS(eSys.SubDisp1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.SubDisp1).ValveNo(0) = enmValve.No1
        gSYS(eSys.SubDisp1).CCDNo = enmCCD.CCD1
        gSYS(eSys.SubDisp1).StageNo = enmStage.No1
        gSYS(eSys.SubDisp1).BalanceNo = enmBalance.No1
        gSYS(eSys.SubDisp1).PinNo = enmStage.No1
        gSYS(eSys.SubDisp1).FMCSNo = 0
        gSYS(eSys.SubDisp1).ClearNo = enmStage.No1
        gSYS(eSys.SubDisp1).LaserNo = enmLaserReader.No1
        gSYS(eSys.SubDisp1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.SubDisp1).AxisX = enmAxis.XAxis
        gSYS(eSys.SubDisp1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.SubDisp1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.SubDisp1).AxisA = enmAxis.AAxis
        gSYS(eSys.SubDisp1).AxisB = enmAxis.BAxis
        gSYS(eSys.SubDisp1).AxisC = enmAxis.CAxis
        gSYS(eSys.SubDisp1).EPVNo = enmEPV.No1
        gSYS(eSys.SubDisp1).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.SubDisp1).EsysNum = eSys.DispStage1
        gSYS(eSys.SubDisp1).ValveControllerNo1 = enmValveController.No1
        gSYS(eSys.SubDisp1).ValveControllerNo2 = enmValveController.No1

        gSYS(eSys.MonitorDisp1).StageNo = enmStage.No1
        Return True
    End Function
    ''' <summary>330A系統配接設定</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSystemParam330A() As Boolean
        gSYS(eSys.MachineA).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.Conveyor1).ConveyorNo = eConveyor.ConveyorNo1
        gSYS(eSys.Conveyor1).AxisConverter = enmAxis.Converter

        gSYS(eSys.DispStage1).ValveNo(0) = enmValve.No1
        gSYS(eSys.DispStage1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.DispStage1).CCDNo = enmCCD.CCD1
        gSYS(eSys.DispStage1).StageNo = enmStage.No1
        gSYS(eSys.DispStage1).BalanceNo = enmBalance.No1
        gSYS(eSys.DispStage1).PinNo = 0
        gSYS(eSys.DispStage1).FMCSNo = 0
        gSYS(eSys.DispStage1).ClearNo = 0
        gSYS(eSys.DispStage1).LaserNo = 0
        gSYS(eSys.DispStage1).ConveyorNo = eConveyor.ConveyorNo1
        gSYS(eSys.DispStage1).AxisX = enmAxis.XAxis
        gSYS(eSys.DispStage1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.DispStage1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.DispStage1).AxisB = enmAxis.BAxis
        gSYS(eSys.DispStage1).AxisC = enmAxis.CAxis
        gSYS(eSys.DispStage1).EPVNo = enmEPV.No1
        gSYS(eSys.DispStage1).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.DispStage1).EsysNum = eSys.DispStage1
        gSYS(eSys.DispStage1).ValveControllerNo1 = enmValveController.No1

        gSYS(eSys.SubDisp1).SelectValve = eValveWorkMode.Valve1
        gSYS(eSys.SubDisp1).ValveNo(0) = enmValve.No1
        gSYS(eSys.SubDisp1).CCDNo = enmCCD.CCD1
        gSYS(eSys.SubDisp1).StageNo = enmStage.No1
        gSYS(eSys.SubDisp1).BalanceNo = enmBalance.No1
        gSYS(eSys.SubDisp1).PinNo = enmStage.No1
        gSYS(eSys.SubDisp1).FMCSNo = 0
        gSYS(eSys.SubDisp1).ClearNo = enmStage.No1
        gSYS(eSys.SubDisp1).LaserNo = enmLaserReader.No1
        gSYS(eSys.SubDisp1).ConveyorNo = enmConveyor.No1
        gSYS(eSys.SubDisp1).AxisX = enmAxis.XAxis
        gSYS(eSys.SubDisp1).AxisY = enmAxis.Y1Axis
        gSYS(eSys.SubDisp1).AxisZ = enmAxis.ZAxis
        gSYS(eSys.SubDisp1).AxisA = enmAxis.AAxis
        gSYS(eSys.SubDisp1).AxisB = enmAxis.BAxis
        gSYS(eSys.SubDisp1).AxisC = enmAxis.CAxis
        gSYS(eSys.SubDisp1).EPVNo = enmEPV.No1
        gSYS(eSys.SubDisp1).MachineNo = enmMachineStation.MachineA
        gSYS(eSys.SubDisp1).EsysNum = eSys.DispStage1
        gSYS(eSys.SubDisp1).ValveControllerNo1 = enmValveController.No1
        gSYS(eSys.SubDisp1).ValveControllerNo2 = enmValveController.No1

        gSYS(eSys.MonitorDisp1).StageNo = enmStage.No1
        Return True
    End Function

    ''' <summary>SysParam配接設定</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSysParam() As Boolean
        For i As Integer = 0 To eSys.Max
            gSYS(i) = New sSysParam
        Next
        Select Case gSSystemParameter.MachineType '配接系統設定
            Case enmMachineType.DCS_F230A
                Return SetSysParamF230A()

            Case enmMachineType.DCS_350A
                Return SetSysParam350A()

            Case enmMachineType.DCS_500AD
                Return SetSystemParam500AD()

            Case enmMachineType.DCSW_800AQ
                Return SetSystemParamW800QA()

            Case enmMachineType.eDTS_2S2V
                Return SetSystemParam2S2V()

            Case enmMachineType.eDTS300A
                Return SetSystemParam300A()
            Case enmMachineType.eDTS330A
                Return SetSystemParam330A()
        End Select
        Return False
    End Function

#End Region

End Class

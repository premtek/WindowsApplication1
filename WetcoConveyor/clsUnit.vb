Imports ProjectIO
Imports ProjectCore
Imports ProjectMotion

Public Class clsUnit

    ''' <summary>動作列舉 相當於Command</summary>
    ''' <remarks></remarks>
    Enum enmDirection
        Home
        Up
        Down
    End Enum

    ''' <summary>汽缸索引配接列舉,相當於enmValveLight</summary>
    ''' <remarks></remarks>
    Enum enmCylinder
        Cylinder1
        Cylinder2
        Cylinder3
        Cylinder4
        Cylinder5
        Cylinder6
    End Enum

    ''' <summary>動作完成狀態列舉, 相當於Act(eAct.Home)</summary>
    ''' <remarks></remarks>
    Enum enmLocation
        Null
        Top
        Bottom
        Home
    End Enum

    ''' <summary>
    ''' 設定A機1~6 Hot Plate 於Load/Unload動作時是否運作
    ''' </summary>
    Private A_HotPlate As New HotPlate
    ''' <summary>
    ''' 設定B機1~6 Hot Plate 於Load/Unload動作時是否運作
    ''' </summary>
    Private B_HotPlate As New HotPlate

    ''' <summary>
    ''' 溫控器控制元件
    ''' </summary>
    Public TempController As New clsTemperatureController
    ''' <summary>
    ''' 可接受的最高溫度
    ''' </summary>
    Private MaxTargetTemp As Integer
    ''' <summary>
    ''' 可接受的最低溫度
    ''' </summary>
    Private MinTargetTemp As Integer
    ''' <summary>
    ''' 可接受溫度的調整參數
    ''' </summary>
    Private TargetRange As Integer

    Dim _workingTemp As UInteger
    ''' <summary>
    ''' 點膠溫度
    ''' </summary>
    Public Property WorkingTemp As UInteger
        Get
            Return _workingTemp
        End Get
        Set(value As UInteger)
            _workingTemp = value
            MaxTargetTemp = value + TargetRange
            MinTargetTemp = value - TargetRange
        End Set
    End Property

    ''' <summary>軸索引配接, 相當於gSYS(eSys.Conveyor1).AxisZ</summary>
    ''' <remarks></remarks>
    Dim A_Chuck As Integer = enmAxis.MachineAChuck1
    ''' <summary>軸索引配接, 相當於gSYS(eSys.Conveyor1).AxisZ2</summary>
    ''' <remarks></remarks>
    Dim B_Chuck As Integer = enmAxis.MachineBChuck1
    ''' <summary>電動缸上抬高度.相當於gSsystemParameter.Pos.xxxx</summary>
    ''' <remarks></remarks>
    Public A_ECylinderTopPos As Double = 20
    ''' <summary>電動缸下降高度.相當於gSsystemParameter.Pos.xxxx</summary>
    ''' <remarks></remarks>
    Public A_ECylinderBottomPos As Double = 10
    ''' <summary>A機電動缸速度,相當於gCMotion.AxisParameter(enmAXis.MachineAChuck1).Velocity.MaxVel</summary>
    ''' <remarks></remarks>
    Public A_ECylinderSpeed As Integer = 1
    ''' <summary>電動缸上抬高度</summary>
    ''' <remarks></remarks>
    Public B_ECylinderTopPos As Double = 20
    ''' <summary>電動缸下降高度</summary>
    ''' <remarks></remarks>
    Public B_ECylinderBottomPos As Double = 10
    ''' <summary>B機電動缸速度</summary>
    ''' <remarks></remarks>
    Public B_ECylinderSpeed As Integer = 1

    Dim ConveyorIniPath As String = System.Windows.Forms.Application.StartupPath & "\system\" & MachineName & "\Conveyor.ini"
    Dim PidControllerIniPath As String = System.Windows.Forms.Application.StartupPath & "\system\" & MachineName & "\PidController.ini"
    ''' <summary>
    ''' 真空建立/真空破除 所需時間
    ''' </summary>
    Dim VacuumDelayTime As Integer = 300

    Sub New()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.DCS_500AD    'Asa
                A_Roller = New A_Roller
                B_Roller = New B_Roller
            Case enmMachineType.DCS_350A
                A_Roller = New clsF350A_Roller(1)
                B_Roller = New clsF350A_Roller(2)
        End Select

        Try
            TempController.A1_PidController.Comport = ReadIniString("Comport", "A1", PidControllerIniPath)
            TempController.A2_PidController.Comport = ReadIniString("Comport", "A2", PidControllerIniPath)
            TempController.A3_PidController.Comport = ReadIniString("Comport", "A3", PidControllerIniPath)
            TempController.A4_PidController.Comport = ReadIniString("Comport", "A4", PidControllerIniPath)
            TempController.A5_PidController.Comport = ReadIniString("Comport", "A5", PidControllerIniPath)
            TempController.A6_PidController.Comport = ReadIniString("Comport", "A6", PidControllerIniPath)
            TempController.B1_PidController.Comport = ReadIniString("Comport", "B1", PidControllerIniPath)
            TempController.B2_PidController.Comport = ReadIniString("Comport", "B2", PidControllerIniPath)
            TempController.B3_PidController.Comport = ReadIniString("Comport", "B3", PidControllerIniPath)
            TempController.B4_PidController.Comport = ReadIniString("Comport", "B4", PidControllerIniPath)
            TempController.B5_PidController.Comport = ReadIniString("Comport", "B5", PidControllerIniPath)
            TempController.B6_PidController.Comport = ReadIniString("Comport", "B6", PidControllerIniPath)
            TargetRange = Convert.ToInt32(ReadIniString("TargetRange", "Range", PidControllerIniPath))

            A_ECylinderTopPos = Convert.ToInt32(ReadIniString("Electric Cylinder", "A_TopPosition", ConveyorIniPath))
            A_ECylinderBottomPos = Convert.ToInt32(ReadIniString("Electric Cylinder", "A_BottomPosition", ConveyorIniPath))
            A_ECylinderSpeed = Convert.ToInt32(ReadIniString("Electric Cylinder", "A_Speed", ConveyorIniPath))
            B_ECylinderTopPos = Convert.ToInt32(ReadIniString("Electric Cylinder", "B_TopPosition", ConveyorIniPath))
            B_ECylinderBottomPos = Convert.ToInt32(ReadIniString("Electric Cylinder", "B_BottomPosition", ConveyorIniPath))
            B_ECylinderSpeed = Convert.ToInt32(ReadIniString("Electric Cylinder", "B_Speed", ConveyorIniPath))
            VacuumDelayTime = Convert.ToInt32(ReadIniString("Vacuum", "DelayTime", ConveyorIniPath))
        Catch ex As Exception
        End Try

        If Not (TempController.OpenAll()) Then
            MsgBox(TempController.ErrorMessage, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If

    End Sub

#Region "主機A元件"
    ''' <summary>
    ''' [A機] Roller控制
    ''' </summary>
    Public A_Roller As IRoller 'New A_Roller

    ''' <summary>
    ''' [A機] 1~6 Vacuum 於Load/Unload動作時是否運作
    ''' </summary>
    Public A_Vacuum() As Boolean = {False, False, False, False, False, False}

    ''' <summary>
    ''' [A機] 設定Vacuum 1~6是否啟用
    ''' </summary>
    Public Sub A_SetVacuum(ByVal vacuum() As Boolean)
        For i = 0 To 5
            A_Vacuum(i) = vacuum(i)
        Next
    End Sub

    ''' <summary>
    ''' [A機] 真空控制
    ''' </summary>
    ''' <param name="enable"></param>
    ''' <returns>完成/未完成且逾時</returns>
    ''' <remarks></remarks>
    Public Function A_VacuumControl(ByVal enable As Boolean) As Boolean
        If enable Then
            '吹氣OFF->吸真空ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, False)  '吹氣OFF
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak2, False) '吹氣OFF
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak3, False)  '吹氣OFF
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak4, False)  '吹氣OFF
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak5, False)  '吹氣OFF
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak6, False)  '吹氣OFF

            gDOCollection.SetState(enmDO.Station2ChuckVacuum, A_Vacuum(0))   '吸真空ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuum2, A_Vacuum(1))   '吸真空ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuum3, A_Vacuum(2))  '吸真空ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuum4, A_Vacuum(3)) '吸真空ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuum5, A_Vacuum(4))  '吸真空ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuum6, A_Vacuum(5))   '吸真空ON

            Threading.Thread.CurrentThread.Join(VacuumDelayTime)

            If (A_IsVacuum(True)) Then '真空建立判斷
                Return True '真空
            Else
                Return False
            End If
        Else
            '吸真空OFF->吹氣ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuum, False)  '吸真空OFF
            gDOCollection.SetState(enmDO.Station2ChuckVacuum2, False)  '吸真空OFF
            gDOCollection.SetState(enmDO.Station2ChuckVacuum3, False) '吸真空OFF
            gDOCollection.SetState(enmDO.Station2ChuckVacuum4, False)  '吸真空OFF
            gDOCollection.SetState(enmDO.Station2ChuckVacuum5, False) '吸真空OFF
            gDOCollection.SetState(enmDO.Station2ChuckVacuum6, False) '吸真空OFF

            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, True)   '吹氣ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak2, True)  '吹氣ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak3, True) '吹氣ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak4, True)   '吹氣ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak5, True)  '吹氣ON
            gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak6, True) '吹氣ON

            Threading.Thread.CurrentThread.Join(VacuumDelayTime)

            If (gDICollection.GetState(enmDI.Station2ChuckVacuumReady) = False AndAlso _
                gDICollection.GetState(enmDI.Station2ChuckVacuumReady2) = False AndAlso _
                gDICollection.GetState(enmDI.Station2ChuckVacuumReady3) = False AndAlso _
                gDICollection.GetState(enmDI.Station2ChuckVacuumReady4) = False AndAlso _
                gDICollection.GetState(enmDI.Station2ChuckVacuumReady5) = False AndAlso _
                gDICollection.GetState(enmDI.Station2ChuckVacuumReady6) = False) Then '已破真空

                gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak1, False)   '吹氣OFF
                gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak2, False)  '吹氣OFF
                gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak3, False)   '吹氣OFF
                gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak4, False)   '吹氣OFF
                gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak5, False)   '吹氣OFF
                gDOCollection.SetState(enmDO.Station2ChuckVacuumBreak6, False)  '吹氣OFF

                Return True '破真空
            Else
                Return False '真空
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' [A機] 是否為真空狀態
    ''' </summary>
    Public ReadOnly Property A_IsVacuum(Optional ByVal defaults As Boolean = True) As Boolean
        Get
            If (gDICollection.GetState(enmDI.Station2ChuckVacuumReady, defaults) Or A_Vacuum(0) = False) Then
                If (gDICollection.GetState(enmDI.Station2ChuckVacuumReady2, defaults) Or A_Vacuum(1) = False) Then
                    If (gDICollection.GetState(enmDI.Station2ChuckVacuumReady3, defaults) Or A_Vacuum(2) = False) Then
                        If (gDICollection.GetState(enmDI.Station2ChuckVacuumReady4, defaults) Or A_Vacuum(3) = False) Then
                            If (gDICollection.GetState(enmDI.Station2ChuckVacuumReady5, defaults) Or A_Vacuum(4) = False) Then
                                If (gDICollection.GetState(enmDI.Station2ChuckVacuumReady6, defaults) Or A_Vacuum(5) = False) Then
                                    '防止全部Vacuum都未開的情況下回復true
                                    For Each bool As Boolean In A_Vacuum
                                        If (bool) Then
                                            Return True
                                        End If
                                    Next
                                    Return False
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            Return False


            'If ((gDICollection.GetState(enmDI.Station2ChuckVacuumReady, A_Vacuum(0)) = A_Vacuum(0) AndAlso _
            '    gDICollection.GetState(enmDI.Station2ChuckVacuumReady2, A_Vacuum(1)) = A_Vacuum(1) AndAlso _
            '    gDICollection.GetState(enmDI.Station2ChuckVacuumReady3, A_Vacuum(2)) = A_Vacuum(2) AndAlso _
            '    gDICollection.GetState(enmDI.Station2ChuckVacuumReady4, A_Vacuum(3)) = A_Vacuum(3) AndAlso _
            '    gDICollection.GetState(enmDI.Station2ChuckVacuumReady5, A_Vacuum(4)) = A_Vacuum(4) AndAlso _
            '    gDICollection.GetState(enmDI.Station2ChuckVacuumReady6, A_Vacuum(5)) = A_Vacuum(5))) Then

            '    '防止全部Vacuum都未開的情況下回復true
            '    For Each bool As Boolean In A_Vacuum
            '        If (bool) Then
            '            Return True
            '        End If
            '    Next

            '    Return False
            'Else
            '    Return False
            'End If

        End Get
    End Property

    ''' <summary>
    ''' [A機] Stoper控制
    ''' </summary>
    Public Function A_Stoper(ByVal direction As enmDirection) As Boolean
        If (direction = enmDirection.Up) Then
            gDOCollection.SetState(enmDO.Station2StopperUp, True) 'Stoper上升
            gDOCollection.SetState(enmDO.Station2StopperDown, False) 'Stoper上升
            If (gDICollection.GetState(enmDI.Station2StopperUpReady, True)) Then 'Stoper上升完成
                Return True
            End If
        ElseIf (direction = enmDirection.Down) Then
            gDOCollection.SetState(enmDO.Station2StopperDown, True) 'Stoper上升
            gDOCollection.SetState(enmDO.Station2StopperUp, False) 'Stoper下降
            If (gDICollection.GetState(enmDI.Station2StopperDownReady, True)) Then 'Stoper下降完成
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' [A機] Stoper Sensor狀態
    ''' </summary>
    Public ReadOnly Property A_StoperSensor(ByVal defaultValue As Boolean) As Boolean
        Get
            If (gDICollection.GetState(enmDI.Station2TrayReady, defaultValue)) Then
                Return True
            End If
            Return False
        End Get
    End Property

    ''' <summary>
    ''' [A機] 前Stoper控制
    ''' </summary>
    Public Function A_FrontStoper(ByVal direction As enmDirection) As Boolean
        If (direction = enmDirection.Up) Then
            gDOCollection.SetState(enmDO.Station1StopperDown, False) 'Stoper上升
            gDOCollection.SetState(enmDO.Station1StopperUpDown, True) 'Stoper上升
            If (gDICollection.GetState(enmDI.Station1StopperUpReady, True)) Then 'Stoper上升完成
                Return True
            End If
        ElseIf (direction = enmDirection.Down) Then
            gDOCollection.SetState(enmDO.Station1StopperUpDown, False) 'Stoper下降
            gDOCollection.SetState(enmDO.Station1StopperDown, True) 'Stoper上升
            If (gDICollection.GetState(enmDI.Station1StopperDownReady, True)) Then 'Stoper下降完成
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' [A機] 入料Sensor狀態
    ''' </summary>
    Public ReadOnly Property A_EntranceSensor(Optional defaults As Boolean = False) As Boolean
        Get
            If (gDICollection.GetState(enmDI.Station2TrayInSensor, defaults)) Then
                Return True
            End If
            Return False
        End Get
    End Property

    ''' <summary>
    ''' [A機] 出料Sensor狀態
    ''' </summary>
    Public ReadOnly Property A_ExitSensor(Optional defaults As Boolean = False) As Boolean
        Get
            If (gDICollection.GetState(enmDI.Station2TrayOutSensor, defaults)) Then
                Return True
            End If
            Return False
        End Get
    End Property

    Dim A_RSensorFlag As Boolean
    ''' <summary>
    ''' [A機] 檢查是否"通過"入料Sensor
    ''' </summary>
    Public Function A_CheckRecieveSensor() As Boolean
        If (gDICollection.GetState(enmDI.Station2TrayInSensor) AndAlso A_RSensorFlag = False) Then  '入料Sensor IO
            A_RSensorFlag = True
        End If

        If A_RSensorFlag Then
            If gDICollection.GetState(enmDI.Station2TrayInSensor) = False Then
                A_RSensorFlag = False
                Return True
            End If
        End If
        Return False
    End Function

    Dim A_DSensorFlag As Boolean
    ''' <summary>
    ''' [A機] 檢查是否"通過"出料Sensor
    ''' </summary>
    Public Function A_CheckDespatchSensor() As Boolean
        If (gDICollection.GetState(enmDI.Station2TrayOutSensor) AndAlso A_DSensorFlag = False) Then  '入料Sensor IO
            A_DSensorFlag = True
        End If

        If A_DSensorFlag Then
            If gDICollection.GetState(enmDI.Station2TrayOutSensor) = False Then
                A_DSensorFlag = False
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' [A機] Hot plate設定
    ''' </summary>
    Public Sub A_SetHotPlate(ByVal plate As HotPlate)
        Me.A_HotPlate = plate
    End Sub

    ''' <summary>
    ''' [A機] 溫度檢查
    ''' </summary>
    Public Function A_CheckTargetTemp() As Boolean
        If (A_HotPlate.HotPlate1) Then
            If TempController.A1_PidController.PV > MaxTargetTemp AndAlso TempController.A1_PidController.PV < MinTargetTemp Then
                Return False
            End If
        End If

        If (A_HotPlate.HotPlate2) Then
            If TempController.A2_PidController.PV > MaxTargetTemp AndAlso TempController.A2_PidController.PV < MinTargetTemp Then
                Return False
            End If
        End If

        If (A_HotPlate.HotPlate3) Then
            If TempController.A3_PidController.PV > MaxTargetTemp AndAlso TempController.A3_PidController.PV < MinTargetTemp Then
                Return False
            End If
        End If

        If (A_HotPlate.HotPlate4) Then
            If TempController.A4_PidController.PV > MaxTargetTemp AndAlso TempController.A4_PidController.PV < MinTargetTemp Then
                Return False
            End If
        End If

        If (A_HotPlate.HotPlate5) Then
            If TempController.A5_PidController.PV > MaxTargetTemp AndAlso TempController.A5_PidController.PV < MinTargetTemp Then
                Return False
            End If
        End If

        If (A_HotPlate.HotPlate6) Then
            If TempController.A6_PidController.PV > MaxTargetTemp AndAlso TempController.A6_PidController.PV < MinTargetTemp Then
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' [A機] 加熱區溫度Interlock異常
    ''' </summary>
    Public ReadOnly Property A_OverHeat As Boolean
        Get
            If gDICollection.GetState(enmDI.OverTemperature) Or _
               gDICollection.GetState(enmDI.OverTemperature2) Or _
               gDICollection.GetState(enmDI.OverTemperature3) Or _
               gDICollection.GetState(enmDI.OverTemperature4) Or _
               gDICollection.GetState(enmDI.OverTemperature5) Or _
               gDICollection.GetState(enmDI.OverTemperature6) Then
                Return True
            End If
            Return False
        End Get
    End Property

    ''' <summary>
    ''' [A機] 加熱器異常
    ''' </summary>
    Public ReadOnly Property A_HeaterAlarm As Boolean
        Get
            If gDICollection.GetState(enmDI.HeaterAlarm1) Or _
               gDICollection.GetState(enmDI.HeaterAlarm2) Or _
               gDICollection.GetState(enmDI.HeaterAlarm3) Or _
               gDICollection.GetState(enmDI.HeaterAlarm4) Or _
               gDICollection.GetState(enmDI.HeaterAlarm5) Or _
               gDICollection.GetState(enmDI.HeaterAlarm6) Then
                Return True
            End If
            Return False
        End Get
    End Property

    'Public Function A_Cylinder(ByVal direction As enmDirection) As Boolean
    '    If (direction = enmDirection.Up) Then
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown1) = False '#1下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown2) = False '#2下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown3) = False '#3下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown4) = False '#4下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown5) = False '#5下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown6) = False '#6下降

    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp1) = A_Plate.HotPlate1 '#1上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp2) = A_Plate.HotPlate2 '#2上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp3) = A_Plate.HotPlate3 '#3上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp4) = A_Plate.HotPlate4 '#4上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp5) = A_Plate.HotPlate5 '#5上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp6) = A_Plate.HotPlate6 '#6上升

    '        If (gDICollection.GetState(enmDI.Station2Heater1CylinderUpReady) = A_Plate.HotPlate1 AndAlso _
    '            gDICollection.GetState(enmDI.Station2Heater2CylinderUpReady) = A_Plate.HotPlate2 AndAlso _
    '            gDICollection.GetState(enmDI.Station2Heater3CylinderUpReady) = A_Plate.HotPlate3 AndAlso _
    '            gDICollection.GetState(enmDI.Station2Heater4CylinderUpReady) = A_Plate.HotPlate4 AndAlso _
    '            gDICollection.GetState(enmDI.Station2Heater5CylinderUpReady) = A_Plate.HotPlate5 AndAlso _
    '            gDICollection.GetState(enmDI.Station2Heater6CylinderUpReady) = A_Plate.HotPlate6) Then  '頂升上升完成
    '            Return True
    '        End If
    '    ElseIf (direction = enmDirection.Down) Then
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp1) = False '#1上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp2) = False '#2上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp3) = False '#3上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp4) = False '#4上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp5) = False '#5上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp6) = False '#6上升

    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown1) = True '#1下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown2) = True '#2下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown3) = True '#3下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown4) = True '#4下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown5) = True '#5下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown6) = True '#6下降

    '        If (gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady) AndAlso _
    '            gDICollection.GetState(enmDI.Station2Heater2CylinderDownReady) AndAlso _
    '            gDICollection.GetState(enmDI.Station2Heater3CylinderDownReady) AndAlso _
    '            gDICollection.GetState(enmDI.Station2Heater4CylinderDownReady) AndAlso _
    '            gDICollection.GetState(enmDI.Station2Heater5CylinderDownReady) AndAlso _
    '            gDICollection.GetState(enmDI.Station2Heater6CylinderDownReady)) Then
    '            Return True '頂升下降完成
    '        End If
    '    End If
    '    Return False
    'End Function

    ''' <summary>
    ''' [A機] 汽缸控制
    ''' </summary>
    Public Function A_Cylinder(ByVal cylinder As enmCylinder, ByVal direction As enmDirection) As Boolean
        If direction = enmDirection.Up Then
            Select Case cylinder
                Case enmCylinder.Cylinder1
                    gDOCollection.SetState(enmDO.HeaterCylinderUp1, True)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown1, False)
                    If (gDICollection.GetState(enmDI.Station2Heater1CylinderUpReady)) Then  '頂升上升完成
                        Return True
                    End If
                Case enmCylinder.Cylinder2
                    gDOCollection.SetState(enmDO.HeaterCylinderUp2, True)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown2, False)
                    If (gDICollection.GetState(enmDI.Station2Heater2CylinderUpReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder3
                    gDOCollection.SetState(enmDO.HeaterCylinderUp3, True)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown3, False)
                    If (gDICollection.GetState(enmDI.Station2Heater3CylinderUpReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder4
                    gDOCollection.SetState(enmDO.HeaterCylinderUp4, True)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown4, False)
                    If (gDICollection.GetState(enmDI.Station2Heater4CylinderUpReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder5
                    gDOCollection.SetState(enmDO.HeaterCylinderUp5, True)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown5, False)
                    If (gDICollection.GetState(enmDI.Station2Heater5CylinderUpReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder6
                    gDOCollection.SetState(enmDO.HeaterCylinderUp6, True)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown6, False)
                    If (gDICollection.GetState(enmDI.Station2Heater6CylinderUpReady)) Then
                        Return True
                    End If
            End Select
        ElseIf direction = enmDirection.Down Then
            Select Case cylinder
                Case enmCylinder.Cylinder1
                    gDOCollection.SetState(enmDO.HeaterCylinderUp1, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown1, True)
                    If (gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady)) Then  '頂升下降完成
                        Return True
                    End If
                Case enmCylinder.Cylinder2
                    gDOCollection.SetState(enmDO.HeaterCylinderUp2, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown2, True)
                    If (gDICollection.GetState(enmDI.Station2Heater2CylinderDownReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder3
                    gDOCollection.SetState(enmDO.HeaterCylinderUp3, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown3, True)
                    If (gDICollection.GetState(enmDI.Station2Heater3CylinderDownReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder4
                    gDOCollection.SetState(enmDO.HeaterCylinderUp4, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown4, True)
                    If (gDICollection.GetState(enmDI.Station2Heater4CylinderDownReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder5
                    gDOCollection.SetState(enmDO.HeaterCylinderUp5, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown5, True)
                    If (gDICollection.GetState(enmDI.Station2Heater5CylinderDownReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder6
                    gDOCollection.SetState(enmDO.HeaterCylinderUp6, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown6, True)
                    If (gDICollection.GetState(enmDI.Station2Heater6CylinderDownReady)) Then
                        Return True
                    End If
            End Select
        End If

        Return False
    End Function

    ''' <summary>
    ''' [A機] 電動缸設定
    ''' </summary>
    Public Function A_SetElectricCylinder() As Boolean
        If gCMotion.SetVelLow(A_Chuck, gCMotion.AxisParameter(A_Chuck).Velocity.VelLow) <> CommandStatus.Sucessed Then
            Return False
        End If

        If gCMotion.SetVelHigh(A_Chuck, gCMotion.AxisParameter(A_Chuck).Velocity.VelHigh) <> CommandStatus.Sucessed Then
            Return False
        End If

        gCMotion.AxisParameter(A_Chuck).Velocity.AccRatio = 0.01
        gCMotion.AxisParameter(A_Chuck).Velocity.DecRatio = 0.01

        If gCMotion.SetAcc(A_Chuck, gCMotion.AxisParameter(A_Chuck).Velocity.Acc * gCMotion.AxisParameter(A_Chuck).Velocity.AccRatio) <> CommandStatus.Sucessed Then
            Return False
        End If

        If gCMotion.SetDec(A_Chuck, gCMotion.AxisParameter(A_Chuck).Velocity.Dec * gCMotion.AxisParameter(A_Chuck).Velocity.DecRatio) <> CommandStatus.Sucessed Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' [A機] 電動缸控制
    ''' </summary>
    ''' <param name="direction">上升/下降</param>
    Public Function A_ElectricCylinder(ByVal direction As enmDirection, Optional ByVal byPass As Boolean = False) As Boolean
        If direction = enmDirection.Up Then
            gCMotion.AbsMove(A_Chuck, A_ECylinderTopPos)
            If (A_ECylinderLocation = enmLocation.Top) Then
                Return True
            End If

        ElseIf direction = enmDirection.Down Then
            gCMotion.AbsMove(A_Chuck, A_ECylinderBottomPos)
            If (A_ECylinderLocation = enmLocation.Bottom) Then
                Return True
            End If

        ElseIf direction = enmDirection.Home Then
            gCMotion.DOOutput(enmAxis.MachineAChuck1, 7, enmCardIOONOFF.eOFF)
            gCMotion.DOOutput(enmAxis.MachineAChuck1, 7, enmCardIOONOFF.eON)
            If gCMotion.SetVelHigh(enmAxis.MachineAChuck1, mGlobalPool.Unit.A_ECylinderSpeed) <> CommandStatus.Sucessed Then '2016.07.03追加 
                Return False
            End If
            If gCMotion.Home(A_Chuck) <> CommandStatus.Sucessed Then
                Return False
            End If
            If (A_ECylinderLocation = enmLocation.Home) Then
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' [A機] 電動缸位置
    ''' </summary>
    Public ReadOnly Property A_ECylinderLocation As enmLocation
        Get
            If gCMotion.HomeFinish(A_Chuck) = CommandStatus.Sucessed Then
                Return enmLocation.Home
            ElseIf Math.Abs(Convert.ToDouble(gCMotion.GetPositionValue(A_Chuck)) - A_ECylinderBottomPos) < 0.025 AndAlso gCMotion.MotionDone(A_Chuck, False) = CommandStatus.Sucessed Then
                Return enmLocation.Bottom
            ElseIf Math.Abs(Convert.ToDouble(gCMotion.GetPositionValue(A_Chuck)) - A_ECylinderTopPos) < 0.025 AndAlso gCMotion.MotionDone(A_Chuck, False) = CommandStatus.Sucessed Then
                Return enmLocation.Top
            Else
                Return enmLocation.Null
            End If
        End Get
    End Property

    ''' <summary>
    ''' 升降器
    ''' </summary>
    Public Function A_Lifter(direction As IConveyorUnit.enmDirection) As Boolean
        If (direction = IConveyorUnit.enmDirection.Up) Then
            gDOCollection.SetState(enmDO.HeaterCylinderDown1, False)
            gDOCollection.SetState(enmDO.HeaterCylinderUp1, True)
            If (gDICollection.GetState(enmDI.Station2Heater1CylinderUpReady, True)) Then '上升完成
                Return True
            End If
        ElseIf (direction = IConveyorUnit.enmDirection.Down) Then
            gDOCollection.SetState(enmDO.HeaterCylinderUp1, False)
            gDOCollection.SetState(enmDO.HeaterCylinderDown1, True)
            If (gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady, True)) Then '下降完成
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 升降器位置
    ''' </summary>
    Public ReadOnly Property A_LifterLocation As IConveyorUnit.enmDirection
        Get
            If (gDICollection.GetState(enmDI.Station2Heater1CylinderUpReady)) Then
                Return IConveyorUnit.enmDirection.Up
            ElseIf (gDICollection.GetState(enmDI.Station2Heater1CylinderDownReady)) Then
                Return IConveyorUnit.enmDirection.Down
            Else
                Return IConveyorUnit.enmDirection.Null
            End If
        End Get
    End Property

#End Region

#Region "主機B元件"
    ''' <summary>
    ''' [B機] Roller控制
    ''' </summary>
    Public B_Roller As IRoller 'New B_Roller

    ''' <summary>
    ''' [B機] 1~6 Vacuum 於Load/Unload動作時是否運作
    ''' </summary>
    Public B_Vacuum() As Boolean = {False, False, False, False, False, False}

    ''' <summary>
    ''' [B機] 設定Vacuum 1~6是否啟用
    ''' </summary>
    Public Sub B_SetVacuum(ByVal vacuum() As Boolean)
        For i = 0 To 5
            B_Vacuum(i) = vacuum(i)
        Next
    End Sub

    ''' <summary>
    ''' [B機] 真空控制
    ''' </summary>
    ''' <param name="enable"></param>
    ''' <returns>完成/未完成且逾時</returns>
    ''' <remarks></remarks>
    Public Function B_VacuumControl(ByVal enable As Boolean) As Boolean
        If enable Then
            '吹氣OFF->吸真空ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak1, False)  '吹氣OFF
            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak2, False)  '吹氣OFF
            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak3, False)  '吹氣OFF
            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak4, False)  '吹氣OFF
            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak5, False)  '吹氣OFF
            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak6, False)  '吹氣OFF

            gDOCollection.SetState(enmDO.Station3ChuckVacuum, B_Vacuum(0))    '吸真空ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuum2, B_Vacuum(1))   '吸真空ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuum3, B_Vacuum(2))   '吸真空ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuum4, B_Vacuum(3))   '吸真空ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuum5, B_Vacuum(4))   '吸真空ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuum6, B_Vacuum(5))   '吸真空ON

            Threading.Thread.CurrentThread.Join(VacuumDelayTime)

            If (B_IsVacuum(True)) Then '真空建立判斷
                Return True '真空
            Else
                Return False
            End If
        Else
            '吸真空OFF->吹氣ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuum, False)   '吸真空OFF
            gDOCollection.SetState(enmDO.Station3ChuckVacuum2, False)  '吸真空OFF
            gDOCollection.SetState(enmDO.Station3ChuckVacuum3, False)  '吸真空OFF
            gDOCollection.SetState(enmDO.Station3ChuckVacuum4, False)  '吸真空OFF
            gDOCollection.SetState(enmDO.Station3ChuckVacuum5, False)  '吸真空OFF
            gDOCollection.SetState(enmDO.Station3ChuckVacuum6, False)  '吸真空OFF

            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak1, True)  '吹氣ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak2, True)  '吹氣ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak3, True)  '吹氣ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak4, True)  '吹氣ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak5, True)  '吹氣ON
            gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak6, True)  '吹氣ON

            Threading.Thread.CurrentThread.Join(VacuumDelayTime)

            If (gDICollection.GetState(enmDI.Station3ChuckVacuumReady) = False AndAlso _
                gDICollection.GetState(enmDI.Station3ChuckVacuumReady2) = False AndAlso _
                gDICollection.GetState(enmDI.Station3ChuckVacuumReady3) = False AndAlso _
                gDICollection.GetState(enmDI.Station3ChuckVacuumReady4) = False AndAlso _
                gDICollection.GetState(enmDI.Station3ChuckVacuumReady5) = False AndAlso _
                gDICollection.GetState(enmDI.Station3ChuckVacuumReady6) = False) Then '真空建立判斷

                gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak1, False)   '吹氣OFF
                gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak2, False)   '吹氣OFF
                gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak3, False)   '吹氣OFF
                gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak4, False)   '吹氣OFF
                gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak5, False)   '吹氣OFF
                gDOCollection.SetState(enmDO.Station3ChuckVacuumBreak6, False)   '吹氣OFF

                Return True '破真空
            Else
                Return False '真空
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' [B機] 是否為真空狀態
    ''' </summary>
    Public ReadOnly Property B_IsVacuum(Optional ByVal defaults As Boolean = True) As Boolean
        Get
            If (gDICollection.GetState(enmDI.Station3ChuckVacuumReady, defaults) Or B_Vacuum(0) = False) Then
                If (gDICollection.GetState(enmDI.Station3ChuckVacuumReady2, defaults) Or B_Vacuum(1) = False) Then
                    If (gDICollection.GetState(enmDI.Station3ChuckVacuumReady3, defaults) Or B_Vacuum(2) = False) Then
                        If (gDICollection.GetState(enmDI.Station3ChuckVacuumReady4, defaults) Or B_Vacuum(3) = False) Then
                            If (gDICollection.GetState(enmDI.Station3ChuckVacuumReady5, defaults) Or B_Vacuum(4) = False) Then
                                If (gDICollection.GetState(enmDI.Station3ChuckVacuumReady6, defaults) Or B_Vacuum(5) = False) Then
                                    '防止全部Vacuum都未開的情況下回復true
                                    For Each bool As Boolean In B_Vacuum
                                        If (bool) Then
                                            Return True
                                        End If
                                    Next
                                    Return False
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            Return False

            'If (gDICollection.GetState(enmDI.Station3ChuckVacuumReady, B_Vacuum(0)) = B_Vacuum(0) AndAlso _
            '    gDICollection.GetState(enmDI.Station3ChuckVacuumReady2, B_Vacuum(1)) = B_Vacuum(1) AndAlso _
            '    gDICollection.GetState(enmDI.Station3ChuckVacuumReady3, B_Vacuum(2)) = B_Vacuum(2) AndAlso _
            '    gDICollection.GetState(enmDI.Station3ChuckVacuumReady4, B_Vacuum(3)) = B_Vacuum(3) AndAlso _
            '    gDICollection.GetState(enmDI.Station3ChuckVacuumReady5, B_Vacuum(4)) = B_Vacuum(4) AndAlso _
            '    gDICollection.GetState(enmDI.Station3ChuckVacuumReady6, B_Vacuum(5)) = B_Vacuum(5)) Then

            '    '防止全部Vacuum都未開的情況下回復true
            '    For Each bool As Boolean In B_Vacuum
            '        If (bool) Then
            '            Return True
            '        End If
            '    Next

            '    Return False
            'Else
            '    Return False
            'End If
        End Get
    End Property

    ''' <summary>
    ''' [B機] Stoper控制
    ''' </summary>
    Public Function B_Stoper(ByVal direction As enmDirection) As Boolean
        If (direction = enmDirection.Up) Then
            gDOCollection.SetState(enmDO.Station3StopperDown, False) 'Stoper上升
            gDOCollection.SetState(enmDO.Station3StopperUp, True)  'Stoper上升
            If (gDICollection.GetState(enmDI.Station3StopperUpReady, True)) Then  'Stoper上升完成
                Return True
            End If
        ElseIf (direction = enmDirection.Down) Then
            gDOCollection.SetState(enmDO.Station3StopperUp, False)
            gDOCollection.SetState(enmDO.Station3StopperDown, True)
            If (gDICollection.GetState(enmDI.Station3StopperDownReady, True)) Then 'Stoper下降完成
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' [B機] Stoper Sensor狀態
    ''' </summary>
    Public ReadOnly Property B_StoperSensor As Boolean
        Get
            If (gDICollection.GetState(enmDI.Station3TrayReady)) Then
                Return True
            End If
            Return False
        End Get
    End Property

    ''' <summary>
    ''' [B機] 入料Sensor狀態
    ''' </summary>
    Public ReadOnly Property B_EntranceSensor(Optional defaults As Boolean = False) As Boolean
        Get
            If gDICollection.GetState(enmDI.Station3TrayInSensor, defaults) Then
                Return True
            End If
            Return False
        End Get
    End Property

    ''' <summary>
    ''' [B機] 出料Sensor狀態
    ''' </summary>
    Public ReadOnly Property B_ExitSensor(Optional defaults As Boolean = False) As Boolean
        Get
            If (gDICollection.GetState(enmDI.Station3TrayOutSensor, defaults)) Then
                Return True
            End If
            Return False
        End Get
    End Property

    Dim B_RSensorFlag As Boolean
    ''' <summary>
    ''' [B機] 檢查是否"通過"入料Sensor
    ''' </summary>
    Public Function B_CheckRecieveSensor() As Boolean
        If (gDICollection.GetState(enmDI.Station3TrayInSensor) AndAlso B_RSensorFlag = False) Then  '入料Sensor IO
            B_RSensorFlag = True
        End If

        If B_RSensorFlag Then
            If gDICollection.GetState(enmDI.Station3TrayInSensor) = False Then
                B_RSensorFlag = False
                Return True
            End If
        End If
        Return False
    End Function

    Dim B_DSensorFlag As Boolean
    ''' <summary>
    ''' [B機] 檢查是否"通過"出料Sensor
    ''' </summary>
    Public Function B_CheckDespatchSensor() As Boolean
        If (gDICollection.GetState(enmDI.Station3TrayOutSensor) AndAlso B_DSensorFlag = False) Then  '入料Sensor IO
            B_DSensorFlag = True
        End If

        If B_DSensorFlag Then
            If gDICollection.GetState(enmDI.Station3TrayOutSensor) = False Then
                B_DSensorFlag = False
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' [B機] Hot plate設定
    ''' </summary>
    Public Sub B_SetHotPlate(ByVal plate As HotPlate)
        Me.B_HotPlate = plate
    End Sub

    ''' <summary>
    ''' [B機] 溫度檢查
    ''' </summary>
    Public Function B_CheckTargetTemp() As Boolean
        If (B_HotPlate.HotPlate1) Then
            If TempController.B1_PidController.PV > MaxTargetTemp AndAlso TempController.B1_PidController.PV < MinTargetTemp Then
                Return False
            End If
        End If

        If (B_HotPlate.HotPlate2) Then
            If TempController.B2_PidController.PV > MaxTargetTemp AndAlso TempController.B2_PidController.PV < MinTargetTemp Then
                Return False
            End If
        End If

        If (B_HotPlate.HotPlate3) Then
            If TempController.B3_PidController.PV > MaxTargetTemp AndAlso TempController.B3_PidController.PV < MinTargetTemp Then
                Return False
            End If
        End If

        If (B_HotPlate.HotPlate4) Then
            If TempController.B4_PidController.PV > MaxTargetTemp AndAlso TempController.B4_PidController.PV < MinTargetTemp Then
                Return False
            End If
        End If

        If (B_HotPlate.HotPlate5) Then
            If TempController.B5_PidController.PV > MaxTargetTemp AndAlso TempController.B5_PidController.PV < MinTargetTemp Then
                Return False
            End If
        End If

        If (B_HotPlate.HotPlate6) Then
            If TempController.B6_PidController.PV > MaxTargetTemp AndAlso TempController.B6_PidController.PV < MinTargetTemp Then
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' [B機] 加熱區溫度Interlock異常
    ''' </summary>
    Public ReadOnly Property B_OverHeat() As Boolean
        Get
            If gDICollection.GetState(enmDI.OverTemperature7) Or _
               gDICollection.GetState(enmDI.OverTemperature8) Or _
               gDICollection.GetState(enmDI.OverTemperature9) Or _
               gDICollection.GetState(enmDI.OverTemperature10) Or _
               gDICollection.GetState(enmDI.OverTemperature11) Or _
               gDICollection.GetState(enmDI.OverTemperature12) Then
                Return True
            End If
            Return False
        End Get
    End Property

    ''' <summary>
    ''' [B機] 加熱器異常
    ''' </summary>
    Public ReadOnly Property B_HeaterAlarm As Boolean
        Get
            If gDICollection.GetState(enmDI.HeaterAlarm7) Or _
               gDICollection.GetState(enmDI.HeaterAlarm8) Or _
               gDICollection.GetState(enmDI.HeaterAlarm9) Or _
               gDICollection.GetState(enmDI.HeaterAlarm10) Or _
               gDICollection.GetState(enmDI.HeaterAlarm11) Or _
               gDICollection.GetState(enmDI.HeaterAlarm12) Then
                Return True
            End If
            Return False
        End Get
    End Property

    'Public Function B_Cylinder(ByVal direction As enmDirection) As Boolean
    '    If (direction = enmDirection.Up) Then
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown7) = False '#1下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown8) = False '#2下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown9) = False '#3下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown10) = False '#4下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown11) = False '#5下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown12) = False '#6下降

    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp7) = B_Plate.HotPlate1 '#1上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp8) = B_Plate.HotPlate2 '#2上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp9) = B_Plate.HotPlate3 '#3上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp10) = B_Plate.HotPlate4 '#4上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp11) = B_Plate.HotPlate5 '#5上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp12) = B_Plate.HotPlate6 '#6上升

    '        If (gDICollection.GetState(enmDI.Station3Heater1CylinderUpReady) = B_Plate.HotPlate1 AndAlso _
    '            gDICollection.GetState(enmDI.Station3Heater2CylinderUpReady) = B_Plate.HotPlate2 AndAlso _
    '            gDICollection.GetState(enmDI.Station3Heater3CylinderUpReady) = B_Plate.HotPlate3 AndAlso _
    '            gDICollection.GetState(enmDI.Station3Heater4CylinderUpReady) = B_Plate.HotPlate4 AndAlso _
    '            gDICollection.GetState(enmDI.Station3Heater5CylinderUpReady) = B_Plate.HotPlate5 AndAlso _
    '            gDICollection.GetState(enmDI.Station3Heater6CylinderUpReady) = B_Plate.HotPlate6) Then  '頂升上升完成
    '            Return True
    '        End If
    '    ElseIf (direction = enmDirection.Down) Then
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp7) = False '#1上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp8) = False '#2上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp9) = False '#3上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp10) = False '#4上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp11) = False '#5上升
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderUp12) = False '#6上升

    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown7) = True '#1下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown8) = True '#2下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown9) = True '#3下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown10) = True '#4下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown11) = True '#5下降
    '        gDOCollection.GetSetState(enmDO.HeaterCylinderDown12) = True '#6下降

    '        If (gDICollection.GetState(enmDI.Station3Heater1CylinderDownReady) AndAlso _
    '            gDICollection.GetState(enmDI.Station3Heater2CylinderDownReady) AndAlso _
    '            gDICollection.GetState(enmDI.Station3Heater3CylinderDownReady) AndAlso _
    '            gDICollection.GetState(enmDI.Station3Heater4CylinderDownReady) AndAlso _
    '            gDICollection.GetState(enmDI.Station3Heater5CylinderDownReady) AndAlso _
    '            gDICollection.GetState(enmDI.Station3Heater6CylinderDownReady)) Then
    '            Return True '頂升下降完成
    '        End If
    '    End If
    '    Return False
    'End Function

    ''' <summary>
    ''' [B機] 汽缸控制
    ''' </summary>
    Public Function B_Cylinder(ByVal cylinder As enmCylinder, ByVal direction As enmDirection) As Boolean
        If direction = enmDirection.Up Then
            Select Case cylinder
                Case enmCylinder.Cylinder1
                    gDOCollection.SetState(enmDO.HeaterCylinderUp7, True)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown7, False)
                    If (gDICollection.GetState(enmDI.Station3Heater1CylinderUpReady)) Then  '頂升上升完成
                        Return True
                    End If
                Case enmCylinder.Cylinder2
                    gDOCollection.SetState(enmDO.HeaterCylinderUp8, True)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown8, False)
                    If (gDICollection.GetState(enmDI.Station3Heater2CylinderUpReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder3
                    gDOCollection.SetState(enmDO.HeaterCylinderUp9, True)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown9, False)
                    If (gDICollection.GetState(enmDI.Station3Heater3CylinderUpReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder4
                    gDOCollection.SetState(enmDO.HeaterCylinderUp10, True)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown10, False)
                    If (gDICollection.GetState(enmDI.Station3Heater4CylinderUpReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder5
                    gDOCollection.SetState(enmDO.HeaterCylinderUp11, True)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown11, False)
                    If (gDICollection.GetState(enmDI.Station3Heater5CylinderUpReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder6
                    gDOCollection.SetState(enmDO.HeaterCylinderUp12, True)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown12, False)
                    If (gDICollection.GetState(enmDI.Station3Heater6CylinderUpReady)) Then
                        Return True
                    End If
            End Select
        ElseIf direction = enmDirection.Down Then
            Select Case cylinder
                Case enmCylinder.Cylinder1
                    gDOCollection.SetState(enmDO.HeaterCylinderUp7, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown7, True)
                    If (gDICollection.GetState(enmDI.Station3Heater1CylinderDownReady)) Then  '頂升下降完成
                        Return True
                    End If
                Case enmCylinder.Cylinder2
                    gDOCollection.SetState(enmDO.HeaterCylinderUp8, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown8, True)
                    If (gDICollection.GetState(enmDI.Station3Heater2CylinderDownReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder3
                    gDOCollection.SetState(enmDO.HeaterCylinderUp9, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown9, True)
                    If (gDICollection.GetState(enmDI.Station3Heater3CylinderDownReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder4
                    gDOCollection.SetState(enmDO.HeaterCylinderUp10, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown10, True)
                    If (gDICollection.GetState(enmDI.Station3Heater4CylinderDownReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder5
                    gDOCollection.SetState(enmDO.HeaterCylinderUp11, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown11, True)
                    If (gDICollection.GetState(enmDI.Station3Heater5CylinderDownReady)) Then
                        Return True
                    End If
                Case enmCylinder.Cylinder6
                    gDOCollection.SetState(enmDO.HeaterCylinderUp12, False)
                    gDOCollection.SetState(enmDO.HeaterCylinderDown12, True)
                    If (gDICollection.GetState(enmDI.Station3Heater6CylinderDownReady)) Then
                        Return True
                    End If
            End Select
        End If

        Return False
    End Function

    ''' <summary>
    ''' [B機] 電動缸設定
    ''' </summary>
    Public Function B_SetElectricCylinder() As Boolean
        '[Note]:速度命令
        If gCMotion.SetVelLow(B_Chuck, gCMotion.AxisParameter(B_Chuck).Velocity.VelLow) <> CommandStatus.Sucessed Then
            Return False
        End If

        If gCMotion.SetVelHigh(B_Chuck, gCMotion.AxisParameter(B_Chuck).Velocity.VelHigh) <> CommandStatus.Sucessed Then
            Return False
        End If

        gCMotion.AxisParameter(B_Chuck).Velocity.AccRatio = 0.01
        gCMotion.AxisParameter(B_Chuck).Velocity.DecRatio = 0.01

        If gCMotion.SetAcc(B_Chuck, gCMotion.AxisParameter(B_Chuck).Velocity.Acc * gCMotion.AxisParameter(B_Chuck).Velocity.AccRatio) <> CommandStatus.Sucessed Then
            Return False
        End If

        If gCMotion.SetDec(B_Chuck, gCMotion.AxisParameter(B_Chuck).Velocity.Dec * gCMotion.AxisParameter(B_Chuck).Velocity.DecRatio) <> CommandStatus.Sucessed Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' [B機] 電動缸控制
    ''' </summary>
    ''' <param name="direction">上升/下降</param>
    Public Function B_ElectricCylinder(ByVal direction As enmDirection, Optional ByVal byPass As Boolean = False) As Boolean    '
        If (byPass) Then
            Return True
        End If

        If direction = enmDirection.Up Then
            gCMotion.AbsMove(B_Chuck, B_ECylinderTopPos)
            If (B_ECylinderLocation = enmLocation.Top) Then
                Return True
            End If

        ElseIf direction = enmDirection.Down Then
            gCMotion.AbsMove(B_Chuck, B_ECylinderBottomPos)
            If (B_ECylinderLocation = enmLocation.Bottom) Then
                Return True
            End If

        ElseIf direction = enmDirection.Home Then
            gCMotion.DOOutput(enmAxis.MachineBChuck1, 7, enmCardIOONOFF.eOFF)
            gCMotion.DOOutput(enmAxis.MachineBChuck1, 7, enmCardIOONOFF.eON)
            If gCMotion.SetVelHigh(enmAxis.MachineBChuck1, mGlobalPool.Unit.B_ECylinderSpeed) <> CommandStatus.Sucessed Then
                Return False
            End If
            If gCMotion.Home(B_Chuck) <> CommandStatus.Sucessed Then
                Return False
            End If
            If (B_ECylinderLocation = enmLocation.Home) Then
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' [B機] 電動缸位置
    ''' </summary>
    Public ReadOnly Property B_ECylinderLocation As enmLocation
        Get
            If gCMotion.HomeFinish(B_Chuck) = CommandStatus.Sucessed Then
                Return enmLocation.Home
            ElseIf Math.Abs(Convert.ToDouble(gCMotion.GetPositionValue(B_Chuck)) - B_ECylinderBottomPos) < 0.025 AndAlso gCMotion.MotionDone(B_Chuck, False) = CommandStatus.Sucessed Then
                Return enmLocation.Bottom
            ElseIf Math.Abs(Convert.ToDouble(gCMotion.GetPositionValue(B_Chuck)) - B_ECylinderTopPos) < 0.025 AndAlso gCMotion.MotionDone(B_Chuck, False) = CommandStatus.Sucessed Then
                Return enmLocation.Top
            Else
                Return enmLocation.Null
            End If
        End Get
    End Property

    ''' <summary>
    ''' 升降器
    ''' </summary>
    Public Function B_Lifter(direction As IConveyorUnit.enmDirection) As Boolean
        If (direction = IConveyorUnit.enmDirection.Up) Then
            gDOCollection.SetState(enmDO.HeaterCylinderDown7, False)
            gDOCollection.SetState(enmDO.HeaterCylinderUp7, True)
            If (gDICollection.GetState(enmDI.Station3Heater1CylinderUpReady, True)) Then  'Stoper上升完成
                Return True
            End If
        ElseIf (direction = IConveyorUnit.enmDirection.Down) Then
            gDOCollection.SetState(enmDO.HeaterCylinderUp7, False)
            gDOCollection.SetState(enmDO.HeaterCylinderDown7, True)
            If (gDICollection.GetState(enmDI.Station3Heater1CylinderDownReady, True)) Then 'Stoper下降完成
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 升降器位置
    ''' </summary>
    Public ReadOnly Property B_LifterLocation As IConveyorUnit.enmDirection
        Get
            If (gDICollection.GetState(enmDI.Station3Heater1CylinderUpReady)) Then
                Return IConveyorUnit.enmDirection.Up
            ElseIf (gDICollection.GetState(enmDI.Station3Heater1CylinderDownReady)) Then
                Return IConveyorUnit.enmDirection.Down
            Else
                Return IConveyorUnit.enmDirection.Null
            End If
        End Get
    End Property
#End Region

End Class

Public Class A_Roller : Implements IRoller

    Private _status As IRoller.enmStatus
    Public ReadOnly Property Status As IRoller.enmStatus Implements IRoller.Status
        Get
            Return _status
        End Get
    End Property

    Public Sub [Stop]() Implements IRoller.Stop
        gDOCollection.SetState(enmDO.MoveInMotorCW, False) 'A機Roller
        gDOCollection.SetState(enmDO.MoveInMotorSlow, False) 'A機關閉低速IO
        _status = IRoller.enmStatus.Stop
    End Sub

    Public Sub Load(Optional speed As IRoller.enmSpeed = IRoller.enmSpeed.Normal) Implements IRoller.Load
        gDOCollection.SetState(enmDO.MoveInMotorCW, True) 'A機Roller
        If (speed = IRoller.enmSpeed.Fast Or speed = IRoller.enmSpeed.Normal) Then
            gDOCollection.SetState(enmDO.MoveInMotorSlow, False) 'A機關閉低速IO
        ElseIf (speed = IRoller.enmSpeed.Slow) Then
            gDOCollection.SetState(enmDO.MoveInMotorSlow, True) 'A機開啟低速IO
        End If
        _status = IRoller.enmStatus.Load
    End Sub

    Public Sub Unload(Optional speed As IRoller.enmSpeed = IRoller.enmSpeed.Normal) Implements IRoller.Unload
        Call Load(speed)
        _status = IRoller.enmStatus.Unload
    End Sub

    Public ReadOnly Property Alarm As Boolean Implements IRoller.Alarm
        Get
            Return gDICollection.GetState(enmDI.MoveInMotorAlarm)
        End Get
    End Property

#Region "未使用"
    Private Sub SetDirection(load As IRoller.enmDirection, unload As IRoller.enmDirection) Implements IRoller.SetDirection

    End Sub

    Private ReadOnly Property LoadDirection As IRoller.enmDirection Implements IRoller.LoadDirection
        Get
            Return IRoller.enmDirection.Forward
        End Get
    End Property

    Private ReadOnly Property UnloadDirection As IRoller.enmDirection Implements IRoller.UnloadDirection
        Get
            Return IRoller.enmDirection.Forward
        End Get
    End Property

    Public Function Initial() As Boolean Implements IRoller.Initial
        Return True
    End Function

    Public Sub Run(direction As IRoller.enmDirection) Implements IRoller.Run

    End Sub

    Private Sub Load() Implements IRoller.Load

    End Sub

    Private Sub Unload() Implements IRoller.Unload

    End Sub

    Public Function SetSpeed(low As Decimal, high As Decimal) As Boolean Implements IRoller.SetSpeed
        Return True
    End Function
#End Region
End Class

Public Class B_Roller : Implements IRoller

    Private _status As IRoller.enmStatus
    Public ReadOnly Property Status As IRoller.enmStatus Implements IRoller.Status
        Get
            Return _status
        End Get
    End Property

    Public Sub Load(Optional speed As IRoller.enmSpeed = IRoller.enmSpeed.Normal) Implements IRoller.Load
        gDOCollection.SetState(enmDO.MoveInMotorCW2, True) 'B機Roller
        If (speed = IRoller.enmSpeed.Fast Or speed = IRoller.enmSpeed.Normal) Then
            gDOCollection.SetState(enmDO.MoveInMotorSlow2, False) 'B機關閉低速IO
        ElseIf (speed = IRoller.enmSpeed.Slow) Then
            gDOCollection.SetState(enmDO.MoveInMotorSlow2, True) 'B機開啟低速IO
        End If
        _status = IRoller.enmStatus.Load
    End Sub

    Public Sub Unload(Optional speed As IRoller.enmSpeed = IRoller.enmSpeed.Normal) Implements IRoller.Unload
        Call Load(speed)
        _status = IRoller.enmStatus.Unload
    End Sub

    Public ReadOnly Property Alarm As Boolean Implements IRoller.Alarm
        Get
            Return gDICollection.GetState(enmDI.MoveInMotorAlarm2)
        End Get
    End Property

    Public Sub [Stop]() Implements IRoller.Stop
        gDOCollection.SetState(enmDO.MoveInMotorCW2, False) 'B機Roller
        gDOCollection.SetState(enmDO.MoveInMotorSlow2, False) 'B機關閉低速IO
        _status = IRoller.enmStatus.Stop
    End Sub

#Region "未使用"
    Private Sub SetDirection(load As IRoller.enmDirection, unload As IRoller.enmDirection) Implements IRoller.SetDirection

    End Sub

    Private ReadOnly Property LoadDirection As IRoller.enmDirection Implements IRoller.LoadDirection
        Get
            Return IRoller.enmDirection.Forward
        End Get
    End Property

    Private ReadOnly Property UnloadDirection As IRoller.enmDirection Implements IRoller.UnloadDirection
        Get
            Return IRoller.enmDirection.Forward
        End Get
    End Property

    Public Function Initial() As Boolean Implements IRoller.Initial
        Return True
    End Function

    Private Sub Run(direction As IRoller.enmDirection) Implements IRoller.Run

    End Sub

    Private Sub Load() Implements IRoller.Load

    End Sub

    Private Sub Unload() Implements IRoller.Unload

    End Sub

    Public Function SetSpeed(low As Decimal, high As Decimal) As Boolean Implements IRoller.SetSpeed
        Return True
    End Function
#End Region
End Class


﻿Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectCore
Imports System.Threading
Imports Advantech.Motion
Imports ProjectIO
Imports WetcoConveyor
Imports MapData

''' <summary>對ProjectMotion銜接模組</summary>
''' <remarks></remarks>
Module MFunctionMotion

    ''' <summary>[馬達點位移動暫存器List]</summary>
    ''' <remarks></remarks>
    Public gMotionPathList(enmStage.Max) As List(Of sMotionPathRegister)

    ''' <summary>
    ''' [A機B機MapData]
    ''' </summary>
    ''' <remarks></remarks>
    Public gMapData(enmMachineStation.MaxMachine) As clsMapData
    ''' <summary>
    ''' PCI_1245 Board板開卡
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitialMotionCard()
        For i As Integer = enmStage.No1 To gSSystemParameter.StageCount - 1
            gMotionPathList(i) = New List(Of sMotionPathRegister)
        Next
        Call gCMotion.Cards.Load(Application.StartupPath & "\System\" & MachineName & "\CardMotion.ini") '讀取軸卡配接參數
        Call gCMotion.Initial(gCMotion.Cards.MotionCardConnectParameter)  '依軸卡配接參數初始化

        Call ReadMotionParameter() '讀取各軸參數

        Select Case gSSystemParameter.MachineType
            Case enmMachineType.eDTS300A, enmMachineType.eDTS330A ' enmMachineType.eGN2, enmMachineType.eGN3, 
                gCMotion.SyncParameter.Clear()
                gCMotion.SyncParameter.Add(New Premtek.Base.CSyncParameter)
                gCMotion.SyncParameter(0).SyncAxisNo.Clear()
                gCMotion.SyncParameter(0).SyncAxisNo.Add(gCMotion.AxisParameter(enmAxis.XAxis).CardParameter.AxisNo)
                gCMotion.SyncParameter(0).SyncAxisNo.Add(gCMotion.AxisParameter(enmAxis.Y1Axis).CardParameter.AxisNo)
                gCMotion.SyncParameter(0).SyncAxisNo.Add(gCMotion.AxisParameter(enmAxis.ZAxis).CardParameter.AxisNo) '追加同動軸
                gCMotion.SyncParameter(0).SPEL(0) = gCMotion.AxisParameter(enmAxis.XAxis).Limit.PosivtiveLimit
                gCMotion.SyncParameter(0).SPEL(1) = gCMotion.AxisParameter(enmAxis.Y1Axis).Limit.PosivtiveLimit
                gCMotion.SyncParameter(0).SPEL(2) = gCMotion.AxisParameter(enmAxis.ZAxis).Limit.PosivtiveLimit
                gCMotion.SyncParameter(0).SNEL(0) = gCMotion.AxisParameter(enmAxis.XAxis).Limit.NegativeLimit
                gCMotion.SyncParameter(0).SNEL(1) = gCMotion.AxisParameter(enmAxis.Y1Axis).Limit.NegativeLimit
                gCMotion.SyncParameter(0).SNEL(2) = gCMotion.AxisParameter(enmAxis.ZAxis).Limit.NegativeLimit
                gCMotion.SyncParameter(0).Scale(0) = gCMotion.AxisParameter(enmAxis.XAxis).Parameter.Scale
                gCMotion.SyncParameter(0).Scale(1) = gCMotion.AxisParameter(enmAxis.Y1Axis).Parameter.Scale
                gCMotion.SyncParameter(0).Scale(2) = gCMotion.AxisParameter(enmAxis.ZAxis).Parameter.Scale
                gCMotion.SyncParameter(0).Velocity.Acc = gCMotion.AxisParameter(enmAxis.XAxis).Velocity.Acc
                gCMotion.SyncParameter(0).Velocity.Dec = gCMotion.AxisParameter(enmAxis.XAxis).Velocity.Dec
                gCMotion.SyncParameter(0).Velocity.AccRatio = gCMotion.AxisParameter(enmAxis.XAxis).Velocity.AccRatio
                gCMotion.SyncParameter(0).Velocity.DecRatio = gCMotion.AxisParameter(enmAxis.XAxis).Velocity.DecRatio
                gCMotion.SyncParameter(0).Velocity.VelHigh = gCMotion.AxisParameter(enmAxis.XAxis).Velocity.VelHigh
                Call gCMotion.GpAddAxis(gCMotion.SyncParameter(0))  '第一群組打包

            Case enmMachineType.eDTS330A
                gCMotion.SyncParameter.Clear()
                gCMotion.SyncParameter.Add(New Premtek.Base.CSyncParameter)
                gCMotion.SyncParameter(0).SyncAxisNo.Clear()
                gCMotion.SyncParameter(0).SyncAxisNo.Add(gCMotion.AxisParameter(enmAxis.XAxis).CardParameter.AxisNo)
                gCMotion.SyncParameter(0).SyncAxisNo.Add(gCMotion.AxisParameter(enmAxis.Y1Axis).CardParameter.AxisNo)
                gCMotion.SyncParameter(0).SyncAxisNo.Add(gCMotion.AxisParameter(enmAxis.ZAxis).CardParameter.AxisNo) '追加同動軸
                gCMotion.SyncParameter(0).SPEL(0) = gCMotion.AxisParameter(enmAxis.XAxis).Limit.PosivtiveLimit
                gCMotion.SyncParameter(0).SPEL(1) = gCMotion.AxisParameter(enmAxis.Y1Axis).Limit.PosivtiveLimit
                gCMotion.SyncParameter(0).SPEL(2) = gCMotion.AxisParameter(enmAxis.ZAxis).Limit.PosivtiveLimit
                gCMotion.SyncParameter(0).SNEL(0) = gCMotion.AxisParameter(enmAxis.XAxis).Limit.NegativeLimit
                gCMotion.SyncParameter(0).SNEL(1) = gCMotion.AxisParameter(enmAxis.Y1Axis).Limit.NegativeLimit
                gCMotion.SyncParameter(0).SNEL(2) = gCMotion.AxisParameter(enmAxis.ZAxis).Limit.NegativeLimit
                gCMotion.SyncParameter(0).Scale(0) = gCMotion.AxisParameter(enmAxis.XAxis).Parameter.Scale
                gCMotion.SyncParameter(0).Scale(1) = gCMotion.AxisParameter(enmAxis.Y1Axis).Parameter.Scale
                gCMotion.SyncParameter(0).Scale(2) = gCMotion.AxisParameter(enmAxis.ZAxis).Parameter.Scale
                gCMotion.SyncParameter(0).Velocity.Acc = gCMotion.AxisParameter(enmAxis.XAxis).Velocity.Acc
                gCMotion.SyncParameter(0).Velocity.Dec = gCMotion.AxisParameter(enmAxis.XAxis).Velocity.Dec
                gCMotion.SyncParameter(0).Velocity.AccRatio = gCMotion.AxisParameter(enmAxis.XAxis).Velocity.AccRatio
                gCMotion.SyncParameter(0).Velocity.DecRatio = gCMotion.AxisParameter(enmAxis.XAxis).Velocity.DecRatio
                gCMotion.SyncParameter(0).Velocity.VelHigh = gCMotion.AxisParameter(enmAxis.XAxis).Velocity.VelHigh
                Call gCMotion.GpAddAxis(gCMotion.SyncParameter(0))  '第一群組打包


            Case enmMachineType.eDTS_2S2V '二台二閥
                gCMotion.SyncParameter.Clear()
                Dim mItemNo As Integer '= -1
                Dim mSyncNo As Integer
                For mStageNo As Integer = eSys.DispStage1 To eSys.DispStage2
                    gCMotion.SyncParameter.Add(New Premtek.Base.CSyncParameter)
                    Dim syncAxisIdx As Integer = 0
                    gCMotion.SyncParameter(mSyncNo).SyncAxisNo.Clear()
                    If gSYS(mStageNo).AxisX = -1 Then
                        gSyslog.Save("Stage" & mStageNo & "AxisX No. Disabled")
                    Else
                        gCMotion.SyncParameter(mSyncNo).SyncAxisNo.Add(gCMotion.AxisParameter(gSYS(mStageNo).AxisX).CardParameter.AxisNo)
                        gCMotion.SyncParameter(mSyncNo).SPEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Limit.PosivtiveLimit
                        gCMotion.SyncParameter(mSyncNo).SNEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Limit.NegativeLimit
                        gCMotion.SyncParameter(mSyncNo).Scale(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Parameter.Scale
                        mItemNo = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).CardParameter.ItemNo '以存在軸的Ticket為設定 (所有軸應該都在同一張卡上)
                        syncAxisIdx += 1
                    End If
                    If gSYS(mStageNo).AxisY = -1 Then
                        gSyslog.Save("Stage" & mStageNo & "AxisY No. Disabled")
                    Else
                        gCMotion.SyncParameter(mSyncNo).SyncAxisNo.Add(gCMotion.AxisParameter(gSYS(mStageNo).AxisY).CardParameter.AxisNo)
                        gCMotion.SyncParameter(mSyncNo).SPEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Limit.PosivtiveLimit
                        gCMotion.SyncParameter(mSyncNo).SNEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Limit.NegativeLimit
                        gCMotion.SyncParameter(mSyncNo).Scale(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Parameter.Scale
                        mItemNo = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).CardParameter.ItemNo '以存在軸的Ticket為設定 (所有軸應該都在同一張卡上)
                        syncAxisIdx += 1
                    End If

                    If gSYS(mStageNo).AxisZ = -1 Then
                        gSyslog.Save("Stage" & mStageNo & "AxisZ No. Disabled")
                    Else
                        gCMotion.SyncParameter(mSyncNo).SyncAxisNo.Add(gCMotion.AxisParameter(gSYS(mStageNo).AxisZ).CardParameter.AxisNo) '追加同動軸
                        gCMotion.SyncParameter(mSyncNo).SPEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisZ).Limit.PosivtiveLimit
                        gCMotion.SyncParameter(mSyncNo).SNEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisZ).Limit.NegativeLimit
                        gCMotion.SyncParameter(mSyncNo).Scale(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisZ).Parameter.Scale
                        mItemNo = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).CardParameter.ItemNo '以存在軸的Ticket為設定 (所有軸應該都在同一張卡上)
                        syncAxisIdx += 1
                    End If

                    If gSYS(mStageNo).AxisB = -1 Then
                        gSyslog.Save("Stage" & mStageNo & "AxisB No. Disabled")
                    Else
                        gCMotion.SyncParameter(mSyncNo).SyncAxisNo.Add(gCMotion.AxisParameter(gSYS(mStageNo).AxisB).CardParameter.AxisNo) '追加同動軸
                        gCMotion.SyncParameter(mSyncNo).SPEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisB).Limit.PosivtiveLimit
                        gCMotion.SyncParameter(mSyncNo).SNEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisB).Limit.NegativeLimit
                        gCMotion.SyncParameter(mSyncNo).Scale(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisB).Parameter.Scale
                        mItemNo = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).CardParameter.ItemNo '以存在軸的Ticket為設定 (所有軸應該都在同一張卡上)
                        syncAxisIdx += 1
                    End If

                    If gSYS(mStageNo).AxisC = -1 Then
                        gSyslog.Save("Stage" & mStageNo & "AxisC No. Disabled")
                    Else
                        gCMotion.SyncParameter(mSyncNo).SyncAxisNo.Add(gCMotion.AxisParameter(gSYS(mStageNo).AxisC).CardParameter.AxisNo) '追加同動軸
                        gCMotion.SyncParameter(mSyncNo).SPEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisC).Limit.PosivtiveLimit
                        gCMotion.SyncParameter(mSyncNo).SNEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisC).Limit.NegativeLimit
                        gCMotion.SyncParameter(mSyncNo).Scale(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisC).Parameter.Scale
                        mItemNo = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).CardParameter.ItemNo '以存在軸的Ticket為設定 (所有軸應該都在同一張卡上)
                        syncAxisIdx += 1
                    End If
                    gCMotion.SyncParameter(mSyncNo).CardParameter.ItemNo = mItemNo
                    gCMotion.SyncParameter(mSyncNo).CardParameter.GroupNo = 0

                    '[Note]:群組移動加減速度(基本試看X軸、Y軸)
                    '       後續在定義群組移動加減速要吃誰
                    If gSYS(mStageNo).AxisX <> -1 Then
                        gCMotion.SyncParameter(mSyncNo).Velocity.Acc = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Velocity.Acc
                        gCMotion.SyncParameter(mSyncNo).Velocity.Dec = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Velocity.Dec
                        gCMotion.SyncParameter(mSyncNo).Velocity.AccRatio = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Velocity.AccRatio
                        gCMotion.SyncParameter(mSyncNo).Velocity.DecRatio = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Velocity.DecRatio
                        gCMotion.SyncParameter(mSyncNo).Velocity.VelHigh = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Velocity.VelHigh
                    Else
                        If gSYS(mStageNo).AxisY <> -1 Then
                            gCMotion.SyncParameter(mSyncNo).Velocity.Acc = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Velocity.Acc
                            gCMotion.SyncParameter(mSyncNo).Velocity.Dec = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Velocity.Dec
                            gCMotion.SyncParameter(mSyncNo).Velocity.AccRatio = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Velocity.AccRatio
                            gCMotion.SyncParameter(mSyncNo).Velocity.DecRatio = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Velocity.DecRatio
                            gCMotion.SyncParameter(mSyncNo).Velocity.VelHigh = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Velocity.VelHigh
                        Else
                            '[Note]:都沒有，就是預設1G的加減速度
                            gCMotion.SyncParameter(mSyncNo).Velocity.Acc = 9800
                            gCMotion.SyncParameter(mSyncNo).Velocity.Dec = 9800
                            gCMotion.SyncParameter(mSyncNo).Velocity.AccRatio = 1
                            gCMotion.SyncParameter(mSyncNo).Velocity.DecRatio = 1
                            gCMotion.SyncParameter(mSyncNo).Velocity.VelHigh = 1000
                        End If
                    End If
                    Call gCMotion.GpAddAxis(gCMotion.SyncParameter(mSyncNo))  '打包加入群組
                    mSyncNo += 1
                Next
            Case enmMachineType.DCSW_800AQ '四台四閥
                gCMotion.SyncParameter.Clear()
                SetSyncGroup(eSys.DispStage1, enmStage.No1, 0)
                SetSyncGroup(eSys.DispStage2, enmStage.No2, 1)
                SetSyncGroup(eSys.DispStage3, enmStage.No3, 0)
                SetSyncGroup(eSys.DispStage4, enmStage.No4, 1)
            Case enmMachineType.DCS_500AD '四台四閥
                gCMotion.SyncParameter.Clear()
                SetSyncGroup(eSys.DispStage1, enmStage.No1, 0)
                SetSyncGroup(eSys.DispStage2, enmStage.No2, 1)
            Case enmMachineType.DCS_F230A
                gCMotion.SyncParameter.Clear()
                SetSyncGroup(eSys.DispStage1, enmStage.No1, 0)
                SetSyncGroup(eSys.DispStage2, enmStage.No2, 1)

            Case enmMachineType.DCS_350A
                gCMotion.SyncParameter.Clear()
                SetSyncGroup(eSys.DispStage1, enmStage.No1, 0)

        End Select

    End Sub
    ''' <summary>
    ''' 同動群組設定
    ''' </summary>
    ''' <param name="mStageNo"></param>
    ''' <param name="mSyncNo"></param>
    ''' <param name="groupNo"></param>
    ''' <remarks></remarks>
    Sub SetSyncGroup(ByVal mStageNo As Integer, ByVal mSyncNo As Integer, ByVal groupNo As Integer)
        Dim mItemNo As Integer '= -1
        gCMotion.SyncParameter.Add(New Premtek.Base.CSyncParameter)
        Dim syncAxisIdx As Integer = 0
        gCMotion.SyncParameter(mSyncNo).SyncAxisNo.Clear()
        If gSYS(mStageNo).AxisX = -1 Then
            gSyslog.Save("Stage" & mStageNo & "AxisX No. Disabled")
        Else
            gCMotion.SyncParameter(mSyncNo).SyncAxisNo.Add(gCMotion.AxisParameter(gSYS(mStageNo).AxisX).CardParameter.AxisNo)
            gCMotion.SyncParameter(mSyncNo).SPEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Limit.PosivtiveLimit
            gCMotion.SyncParameter(mSyncNo).SNEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Limit.NegativeLimit
            gCMotion.SyncParameter(mSyncNo).Scale(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Parameter.Scale
            mItemNo = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).CardParameter.ItemNo '以存在軸的Ticket為設定 (所有軸應該都在同一張卡上)
            syncAxisIdx += 1
        End If
        If gSYS(mStageNo).AxisY = -1 Then
            gSyslog.Save("Stage" & mStageNo & "AxisY No. Disabled")
        Else
            gCMotion.SyncParameter(mSyncNo).SyncAxisNo.Add(gCMotion.AxisParameter(gSYS(mStageNo).AxisY).CardParameter.AxisNo)
            gCMotion.SyncParameter(mSyncNo).SPEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Limit.PosivtiveLimit
            gCMotion.SyncParameter(mSyncNo).SNEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Limit.NegativeLimit
            gCMotion.SyncParameter(mSyncNo).Scale(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Parameter.Scale
            mItemNo = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).CardParameter.ItemNo '以存在軸的Ticket為設定 (所有軸應該都在同一張卡上)
            syncAxisIdx += 1
        End If

        If gSYS(mStageNo).AxisZ = -1 Then
            gSyslog.Save("Stage" & mStageNo & "AxisZ No. Disabled")
        Else
            gCMotion.SyncParameter(mSyncNo).SyncAxisNo.Add(gCMotion.AxisParameter(gSYS(mStageNo).AxisZ).CardParameter.AxisNo) '追加同動軸
            gCMotion.SyncParameter(mSyncNo).SPEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisZ).Limit.PosivtiveLimit
            gCMotion.SyncParameter(mSyncNo).SNEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisZ).Limit.NegativeLimit
            gCMotion.SyncParameter(mSyncNo).Scale(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisZ).Parameter.Scale
            mItemNo = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).CardParameter.ItemNo '以存在軸的Ticket為設定 (所有軸應該都在同一張卡上)
            syncAxisIdx += 1
        End If

        If gSYS(mStageNo).AxisB = -1 Then
            gSyslog.Save("Stage" & mStageNo & "AxisB No. Disabled")
        Else
            gCMotion.SyncParameter(mSyncNo).SyncAxisNo.Add(gCMotion.AxisParameter(gSYS(mStageNo).AxisB).CardParameter.AxisNo) '追加同動軸
            gCMotion.SyncParameter(mSyncNo).SPEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisB).Limit.PosivtiveLimit
            gCMotion.SyncParameter(mSyncNo).SNEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisB).Limit.NegativeLimit
            gCMotion.SyncParameter(mSyncNo).Scale(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisB).Parameter.Scale
            mItemNo = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).CardParameter.ItemNo '以存在軸的Ticket為設定 (所有軸應該都在同一張卡上)
            syncAxisIdx += 1
        End If

        If gSYS(mStageNo).AxisC = -1 Then
            gSyslog.Save("Stage" & mStageNo & "AxisC No. Disabled")
        Else
            gCMotion.SyncParameter(mSyncNo).SyncAxisNo.Add(gCMotion.AxisParameter(gSYS(mStageNo).AxisC).CardParameter.AxisNo) '追加同動軸
            gCMotion.SyncParameter(mSyncNo).SPEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisC).Limit.PosivtiveLimit
            gCMotion.SyncParameter(mSyncNo).SNEL(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisC).Limit.NegativeLimit
            gCMotion.SyncParameter(mSyncNo).Scale(syncAxisIdx) = gCMotion.AxisParameter(gSYS(mStageNo).AxisC).Parameter.Scale
            mItemNo = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).CardParameter.ItemNo '以存在軸的Ticket為設定 (所有軸應該都在同一張卡上)
            syncAxisIdx += 1
        End If
        If mItemNo = -1 Then '無符合卡號
            Exit Sub
        End If
        gCMotion.SyncParameter(mSyncNo).CardParameter.ItemNo = mItemNo 'mSyncNo '修正 0-3組不代表與0-2張軸卡對應
        gCMotion.SyncParameter(mSyncNo).CardParameter.GroupNo = groupNo

        '[Note]:群組移動加減速度(基本試看X軸、Y軸)
        '       後續在定義群組移動加減速要吃誰
        If gSYS(mStageNo).AxisX <> -1 Then
            gCMotion.SyncParameter(mSyncNo).Velocity.Acc = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Velocity.Acc
            gCMotion.SyncParameter(mSyncNo).Velocity.Dec = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Velocity.Dec
            gCMotion.SyncParameter(mSyncNo).Velocity.AccRatio = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Velocity.AccRatio
            gCMotion.SyncParameter(mSyncNo).Velocity.DecRatio = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Velocity.DecRatio
            gCMotion.SyncParameter(mSyncNo).Velocity.VelHigh = gCMotion.AxisParameter(gSYS(mStageNo).AxisX).Velocity.VelHigh
        Else
            If gSYS(mStageNo).AxisY <> -1 Then
                gCMotion.SyncParameter(mSyncNo).Velocity.Acc = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Velocity.Acc
                gCMotion.SyncParameter(mSyncNo).Velocity.Dec = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Velocity.Dec
                gCMotion.SyncParameter(mSyncNo).Velocity.AccRatio = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Velocity.AccRatio
                gCMotion.SyncParameter(mSyncNo).Velocity.DecRatio = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Velocity.DecRatio
                gCMotion.SyncParameter(mSyncNo).Velocity.VelHigh = gCMotion.AxisParameter(gSYS(mStageNo).AxisY).Velocity.VelHigh
            Else
                '[Note]:都沒有，就是預設1G的加減速度
                gCMotion.SyncParameter(mSyncNo).Velocity.Acc = 9800
                gCMotion.SyncParameter(mSyncNo).Velocity.Dec = 9800
                gCMotion.SyncParameter(mSyncNo).Velocity.AccRatio = 1
                gCMotion.SyncParameter(mSyncNo).Velocity.DecRatio = 1
                gCMotion.SyncParameter(mSyncNo).Velocity.VelHigh = 1000
            End If
        End If

        If gCMotion.GpAddAxis(gCMotion.SyncParameter(mSyncNo)) = CommandStatus.Sucessed Then '打包加入群組
            Debug.Print("同動群組" & mSyncNo & ":" & "所在軸卡" & mItemNo & " 所屬群號:" & groupNo)
        Else
            Debug.Print("同動群組" & mSyncNo & ": 設定失敗.")
        End If

    End Sub

    Public Sub Initial_Motion()
        gCMotion.gEqpMsg = gEqpMsg
        '********************************Servo On & IO Set & Home Set ***********************************
        If gSSystemParameter.RunMode = enmRunMode.Simulation Then
            Exit Sub
        End If
        '不分機型, 設定軸全軸打開, 確認設定檔最大軸號正確即可
        For i As Integer = 0 To enmAxis.Max
            Call gCMotion.Servo(i, enmONOFF.eON)
            'Call gCMotion.SetPPU(i, gCMotion.AxisParameter(i).Parameter.PPU)
            Call gCMotion.IOSet(i)
            Call gCMotion.SetHomeExSwitchMode(i, gCMotion.AxisParameter(i).HomeParameter.HomeExSwitchMode)
            Call gCMotion.SetHomeCrossDistance(i, gCMotion.AxisParameter(i).HomeParameter.HomeCrossDistance)
            Call gCMotion.SetHomeOffset(i, gCMotion.AxisParameter(i).HomeParameter.HomeOffset)
            Call gCMotion.SetMaxAcc(i, gCMotion.AxisParameter(i).Velocity.MaxAcc)
            Call gCMotion.SetMaxDec(i, gCMotion.AxisParameter(i).Velocity.MaxDec)
            Call gCMotion.SetMaxVel(i, gCMotion.AxisParameter(i).Velocity.MaxVel)
            Call gCMotion.SetAcc(i, 100) '強迫複寫,避免未設定造成參數錯誤
            Call gCMotion.SetDec(i, 100) '強迫複寫,避免未設定造成參數錯誤
        Next
        'Exit Sub
        ''^^^^^^^

        'Select Case gSSystemParameter.enmMachineType
        '    Case enmMachineType.eDTS300A 'frmMain_Load

        '        'Call gCMotion.SetPPU(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).Parameter.PPU)
        '        'Call gCMotion.SetPPU(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).Parameter.PPU)
        '        'Call gCMotion.SetPPU(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).Parameter.PPU)
        '        'Call gCMotion.SetPPU(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.ZAxis).Parameter.PPU)
        '        'gSyslog.Save("Motion Set Pulse Per Unit.")

        '        Call gCMotion.IOSet(enmAxis.XAxis)
        '        Call gCMotion.IOSet(enmAxis.Y1Axis)
        '        Call gCMotion.IOSet(enmAxis.ZAxis)
        '        Call gCMotion.IOSet(enmAxis.Y2Axis)
        '        gSyslog.Save("Motion Set IO Config.")

        '        Call gCMotion.SetHomeExSwitchMode(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).HomeParameter.HomeExSwitchMode)
        '        Call gCMotion.SetHomeExSwitchMode(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).HomeParameter.HomeExSwitchMode)
        '        Call gCMotion.SetHomeExSwitchMode(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).HomeParameter.HomeExSwitchMode)
        '        Call gCMotion.SetHomeExSwitchMode(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.Y2Axis).HomeParameter.HomeExSwitchMode)

        '        Call gCMotion.SetHomeCrossDistance(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).HomeParameter.dblHomeCrossDistance)
        '        Call gCMotion.SetHomeCrossDistance(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).HomeParameter.dblHomeCrossDistance)
        '        Call gCMotion.SetHomeCrossDistance(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).HomeParameter.dblHomeCrossDistance)
        '        Call gCMotion.SetHomeCrossDistance(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.Y2Axis).HomeParameter.dblHomeCrossDistance)
        '        gSyslog.Save("Motion Set Home Config.")
        '        Call gCMotion.SetHomeOffset(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).HomeParameter.HomeOffset)
        '        Call gCMotion.SetHomeOffset(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).HomeParameter.HomeOffset)
        '        Call gCMotion.SetHomeOffset(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).HomeParameter.HomeOffset)
        '        Call gCMotion.SetHomeOffset(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.Y2Axis).HomeParameter.HomeOffset)

        '        Call gCMotion.SetMaxAcc(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).Velocity.MaxAcc)
        '        Call gCMotion.SetMaxAcc(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).Velocity.MaxAcc)
        '        Call gCMotion.SetMaxAcc(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).Velocity.MaxAcc)
        '        Call gCMotion.SetMaxAcc(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.Y2Axis).Velocity.MaxAcc)
        '        gSyslog.Save("Motion Set MaxAcc")

        '        Call gCMotion.SetMaxDec(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).Velocity.MaxDec)
        '        Call gCMotion.SetMaxDec(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).Velocity.MaxDec)
        '        Call gCMotion.SetMaxDec(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).Velocity.MaxDec)
        '        Call gCMotion.SetMaxDec(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.Y2Axis).Velocity.MaxDec)
        '        gSyslog.Save("Motion Set MaxDec")

        '        Call gCMotion.SetMaxVel(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).Velocity.MaxVel)
        '        Call gCMotion.SetMaxVel(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).Velocity.MaxVel)
        '        Call gCMotion.SetMaxVel(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).Velocity.MaxVel)
        '        Call gCMotion.SetMaxVel(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.Y2Axis).Velocity.MaxVel)
        '        gSyslog.Save("Motion Set MaxVel")
        '        Call gCMotion.Servo(enmAxis.XAxis, enmONOFF.eON)
        '        Call gCMotion.Servo(enmAxis.Y1Axis, enmONOFF.eON)
        '        Call gCMotion.Servo(enmAxis.ZAxis, enmONOFF.eON)
        '        Call gCMotion.Servo(enmAxis.Y2Axis, enmONOFF.eON)
        '        gSyslog.Save("Motion Set Servo On")
        '        Dim result As ErrorCode
        '        ''--- Soni + 2014.09.29 機型切換 ---
        '        Try
        '            Do
        '                result = gCMotion.SetGantry(enmAxis.Y1Axis, enmAxis.Y2Axis)
        '                If result <> CInt(ErrorCode.SUCCESS) Then
        '                    Thread.CurrentThread.Join(300)
        '                End If
        '            Loop Until result = ErrorCode.SUCCESS
        '            gSyslog.Save("Motion Set Y1 Y2 Gantry OK.")
        '        Catch ex As Exception
        '            gSyslog.Save("Motion Set Y1 Y2 Gantry NG. " & ex.Message)
        '        End Try


        '        ''--- Soni + 2014.09.29 機型切換 ---
        '    Case enmMachineType.eDTS330A
        '        Call gCMotion.Servo(enmAxis.XAxis, enmONOFF.eON)
        '        Call gCMotion.Servo(enmAxis.Y1Axis, enmONOFF.eON)
        '        Call gCMotion.Servo(enmAxis.ZAxis, enmONOFF.eON)
        '        Call gCMotion.Servo(enmAxis.Y2Axis, enmONOFF.eON)
        '        gSyslog.Save("Motion Set Servo On")

        '        'Call gCMotion.SetPPU(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).Parameter.PPU)
        '        'Call gCMotion.SetPPU(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).Parameter.PPU)
        '        'Call gCMotion.SetPPU(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).Parameter.PPU)
        '        'Call gCMotion.SetPPU(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.Y2Axis).Parameter.PPU)
        '        'gSyslog.Save("Motion Set Pulse Per Unit.")

        '        Call gCMotion.IOSet(enmAxis.XAxis)
        '        Call gCMotion.IOSet(enmAxis.Y1Axis)
        '        Call gCMotion.IOSet(enmAxis.ZAxis)
        '        Call gCMotion.IOSet(enmAxis.Y2Axis)
        '        gSyslog.Save("Motion Set IO Config.")

        '        Call gCMotion.SetHomeExSwitchMode(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).HomeParameter.HomeExSwitchMode)
        '        Call gCMotion.SetHomeExSwitchMode(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).HomeParameter.HomeExSwitchMode)
        '        Call gCMotion.SetHomeExSwitchMode(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).HomeParameter.HomeExSwitchMode)
        '        Call gCMotion.SetHomeExSwitchMode(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).HomeParameter.HomeExSwitchMode)

        '        Call gCMotion.SetHomeCrossDistance(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).HomeParameter.dblHomeCrossDistance)
        '        Call gCMotion.SetHomeCrossDistance(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).HomeParameter.dblHomeCrossDistance)
        '        Call gCMotion.SetHomeCrossDistance(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).HomeParameter.dblHomeCrossDistance)
        '        Call gCMotion.SetHomeCrossDistance(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.Y2Axis).HomeParameter.dblHomeCrossDistance)
        '        gSyslog.Save("Motion Set Home Config.")
        '        Call gCMotion.SetHomeOffset(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).HomeParameter.HomeOffset)
        '        Call gCMotion.SetHomeOffset(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).HomeParameter.HomeOffset)
        '        Call gCMotion.SetHomeOffset(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).HomeParameter.HomeOffset)
        '        Call gCMotion.SetHomeOffset(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.Y2Axis).HomeParameter.HomeOffset)

        '        Call gCMotion.SetMaxAcc(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).Velocity.MaxAcc)
        '        Call gCMotion.SetMaxAcc(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).Velocity.MaxAcc)
        '        Call gCMotion.SetMaxAcc(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).Velocity.MaxAcc)
        '        Call gCMotion.SetMaxAcc(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.Y2Axis).Velocity.MaxAcc)
        '        gSyslog.Save("Motion Set MaxAcc")

        '        Call gCMotion.SetMaxDec(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).Velocity.MaxDec)
        '        Call gCMotion.SetMaxDec(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).Velocity.MaxDec)
        '        Call gCMotion.SetMaxDec(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).Velocity.MaxDec)
        '        Call gCMotion.SetMaxDec(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.Y2Axis).Velocity.MaxDec)
        '        gSyslog.Save("Motion Set MaxDec")

        '        Call gCMotion.SetMaxVel(enmAxis.XAxis, gCMotion.AxisParameter(enmAxis.XAxis).Velocity.MaxVel)
        '        Call gCMotion.SetMaxVel(enmAxis.Y1Axis, gCMotion.AxisParameter(enmAxis.Y1Axis).Velocity.MaxVel)
        '        Call gCMotion.SetMaxVel(enmAxis.ZAxis, gCMotion.AxisParameter(enmAxis.ZAxis).Velocity.MaxVel)
        '        Call gCMotion.SetMaxVel(enmAxis.Y2Axis, gCMotion.AxisParameter(enmAxis.Y2Axis).Velocity.MaxVel)
        '        gSyslog.Save("Motion Set MaxVel")

        '        Dim result As ErrorCode
        '        ''--- Soni + 2014.09.29 機型切換 ---
        '        Try

        '            Do
        '                result = gCMotion.SetGantry(enmAxis.Y1Axis, enmAxis.Y2Axis)
        '            Loop Until result = ErrorCode.SUCCESS
        '            gSyslog.Save("Motion Set Y1 Y2 Gantry OK.")
        '        Catch ex As Exception
        '            gSyslog.Save("Motion Set Y1 Y2 Gantry NG. " & ex.Message, , eMessageLevel.Error)
        '        End Try

        '        ''--- Soni + 2014.09.29 機型切換 ---
        '    Case enmMachineType.eDTS_2S2V, enmMachineType.DCSW_800AQ
        '        For i As Integer = 0 To enmAxis.Max
        '            Call gCMotion.Servo(i, enmONOFF.eON)
        '            'Call gCMotion.SetPPU(i, gCMotion.AxisParameter(i).Parameter.PPU)
        '            Call gCMotion.IOSet(i)
        '            Call gCMotion.SetHomeExSwitchMode(i, gCMotion.AxisParameter(i).HomeParameter.HomeExSwitchMode)
        '            Call gCMotion.SetHomeCrossDistance(i, gCMotion.AxisParameter(i).HomeParameter.dblHomeCrossDistance)
        '            Call gCMotion.SetHomeOffset(i, gCMotion.AxisParameter(i).HomeParameter.HomeOffset)
        '            Call gCMotion.SetMaxAcc(i, gCMotion.AxisParameter(i).Velocity.MaxAcc)
        '            Call gCMotion.SetMaxDec(i, gCMotion.AxisParameter(i).Velocity.MaxDec)
        '            Call gCMotion.SetMaxVel(i, gCMotion.AxisParameter(i).Velocity.MaxVel)
        '            Call gCMotion.SetAcc(i, 100) '強迫複寫,避免未設定造成參數錯誤
        '            Call gCMotion.SetDec(i, 100) '強迫複寫,避免未設定造成參數錯誤
        '        Next


        'End Select


    End Sub

    Dim mStopWatch As New Stopwatch
    ''' <summary>按鍵用 安全移動 Z軸先到0再水平移動 最後再動Z</summary>
    ''' <param name="sender"></param>
    ''' <remarks>AxisNo的順序必須是X,Y,Z,B,C且X,Y,Z必備</remarks>
    Public Function ButtonSafeMovePos(ByRef sender As Button, ByVal AxisNo() As Integer, ByVal TargetPos() As Decimal, ByVal sys As sSysParam) As Boolean 'Soni / 2016.08.20 增加動作例外傳回False, 供外部使用
        Dim isINP(4) As Boolean
        'Const TIME_OUT_IN_MS As Integer = 5000
        If AxisNo.Count > 5 Then
            MsgBox("ButtonSafeMovePos Not Supports.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        If AxisNo.Count < 5 Then
            MsgBox("ButtonSafeMovePos Not Supports.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        If Not gSYS(eSys.OverAll) Is Nothing Then
            If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                MsgBox("Need Home!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If
        End If
        If Not sender Is Nothing Then
            CType(sender, Button).BackColor = Color.Yellow '按鍵顏色
        End If

        Dim StartPos(AxisNo.Count - 1) As Decimal

        For i As Integer = 0 To AxisNo.Count - 1
            StartPos(i) = gCMotion.GetPositionValue(AxisNo(i))
        Next

       
        Debug.Print(".AxisParameter.Count:" & gCMotion.AxisParameter.Count)
        For i As Integer = 0 To AxisNo.Count - 1
            If AxisNo(i) < gCMotion.AxisParameter.Count Then
                If AxisNo(i) >= 0 Then
                    gCMotion.SetVelHigh(AxisNo(i), gSSystemParameter.ManualVelHigh) 'gCMotion.AxisParameter(AxisNo(i)).Velocity.VelHigh)
                    gCMotion.SetVelLow(AxisNo(i), gCMotion.AxisParameter(AxisNo(i)).Velocity.VelLow)
                    gCMotion.SetAcc(AxisNo(i), gCMotion.AxisParameter(AxisNo(i)).Velocity.Acc * gCMotion.AxisParameter(AxisNo(i)).Velocity.AccRatio)
                    gCMotion.SetDec(AxisNo(i), gCMotion.AxisParameter(AxisNo(i)).Velocity.Dec * gCMotion.AxisParameter(AxisNo(i)).Velocity.DecRatio)
                End If

            End If
        Next

        If gCMotion.AbsMove(AxisNo(2), 0) <> CommandStatus.Sucessed Then 'Z上飛高
            If Not sender Is Nothing Then
                CType(sender, Button).BackColor = Color.Red
            End If
            MsgBox("Z軸移動到0,失敗!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        gSyslog.Save(gCMotion.AxisParameter(AxisNo(2)).AxisName & " Move to 0")
        System.Threading.Thread.CurrentThread.Join(100) '移動命令下達後不能立刻看到位
        mStopWatch.Restart()
        Do
            Application.DoEvents()
            If gCMotion.MotionDone(AxisNo(2)) = CommandStatus.Sucessed Then '到位 離開等待迴圈
                Exit Do
            End If
            If AxisNo(2) > -1 Then
                If gCMotion.IsMoveTimeOut(AxisNo(2)) Then '逾時 中斷離開
                    If Not sender Is Nothing Then
                        CType(sender, Button).BackColor = Color.Red
                    End If
                    Select Case sys.StageNo
                        Case 0
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1032004), , eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                        Case 1
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1042004), , eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1042004), MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                        Case 2
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1062004), , eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                        Case 3
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1069004), , eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                    End Select
                    Return False
                End If
            End If
        Loop
        gSyslog.Save(gCMotion.AxisParameter(AxisNo(2)).AxisName & " Move OK.")
        '有Tilt的話先轉Tilt
        If AxisNo(3) > -1 Then
            If gCMotion.AbsMove(AxisNo(3), TargetPos(3)) <> CommandStatus.Sucessed Then
                MsgBox(gCMotion.AxisParameter(AxisNo(3)).AxisName & " AbsMove Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If
        End If
        mStopWatch.Restart()
        System.Threading.Thread.CurrentThread.Join(100) '移動命令下達後不能立刻看到位
        If gCMotion.MotionDone(AxisNo(3)) <> CommandStatus.Sucessed Then
            isINP(3) = False 'Axis is Not Inposition
            If gCMotion.IsMoveTimeOut(AxisNo(3)) = True Then '到位逾時
                If Not sender Is Nothing Then
                    CType(sender, Button).BackColor = Color.Red
                End If
                gSyslog.Save(gCMotion.AxisParameter(AxisNo(3)).AxisName & " Move Time Out!", , eMessageLevel.Warning)
                MsgBox(gCMotion.AxisParameter(AxisNo(3)).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                Return False
            End If
        End If

        ' For i As Integer = 3 To AxisNo.Count - 1
        For i As Integer = 4 To AxisNo.Count - 1
            If gCMotion.AbsMove(AxisNo(i), TargetPos(i)) <> CommandStatus.Sucessed Then
                MsgBox(gCMotion.AxisParameter(AxisNo(i)).AxisName & " AbsMove Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If
        Next
        mStopWatch.Restart()
        System.Threading.Thread.CurrentThread.Join(100) '移動命令下達後不能立刻看到位

        Do
            Application.DoEvents()
            isINP(3) = True 'Assume INP is Ready
            isINP(4) = True
            'If AxisNo(3) > -1 Then 'B軸存在
            '    If gCMotion.MotionDone(AxisNo(3)) <> CommandStatus.Sucessed Then
            '        isINP(3) = False 'Axis is Not Inposition
            '        If gCMotion.IsMoveTimeOut(AxisNo(3)) = True Then '到位逾時
            '            If Not sender Is Nothing Then
            '                CType(sender, Button).BackColor = Color.Red
            '            End If
            '            gSyslog.Save(gCMotion.AxisParameter(AxisNo(3)).AxisName & " Move Time Out!", , eMessageLevel.Warning)
            '            MsgBox(gCMotion.AxisParameter(AxisNo(3)).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
            '            Return False
            '        End If
            '    End If
            'End If
            If AxisNo(4) > -1 Then 'C軸存在
                If gCMotion.MotionDone(AxisNo(4)) <> CommandStatus.Sucessed Then
                    isINP(4) = False 'Axis is Not Inposition
                    If gCMotion.IsMoveTimeOut(AxisNo(4)) Then '到位逾時
                        If Not sender Is Nothing Then
                            CType(sender, Button).BackColor = Color.Red
                        End If
                        gSyslog.Save(gCMotion.AxisParameter(AxisNo(4)).AxisName & " Move Time Out!", , eMessageLevel.Warning)
                        MsgBox(gCMotion.AxisParameter(AxisNo(4)).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                    End If
                End If

            End If
            If isINP(3) And isINP(4) Then '到位 離開等待迴圈
                Exit Do
            End If

        Loop

        If gCMotion.AbsMove(AxisNo(0), TargetPos(0)) <> CommandStatus.Sucessed Then
            If Not sender Is Nothing Then
                CType(sender, Button).BackColor = Color.Red
            End If
            MsgBox("X軸移動到" & TargetPos(0) & "失敗!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        If gCMotion.AbsMove(AxisNo(1), TargetPos(1)) <> CommandStatus.Sucessed Then '水平移動
            If Not sender Is Nothing Then
                CType(sender, Button).BackColor = Color.Red
            End If
            MsgBox("Y軸移動到" & TargetPos(1) & "失敗!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        gSyslog.Save(gCMotion.AxisParameter(AxisNo(0)).AxisName & " Move To (" & TargetPos(0) & ")")
        gSyslog.Save(gCMotion.AxisParameter(AxisNo(1)).AxisName & " Move To (" & TargetPos(1) & ")")
        mStopWatch.Restart()
        System.Threading.Thread.CurrentThread.Join(100) '移動命令下達後不能立刻看到位
        Do
            Application.DoEvents()
            isINP(0) = True 'Assume INP is Ready
            isINP(1) = True
            If AxisNo(0) > -1 Then 'B軸存在
                If gCMotion.MotionDone(AxisNo(0)) <> CommandStatus.Sucessed Then
                    isINP(0) = False 'Axis is Not Inposition
                    If gCMotion.IsMoveTimeOut(AxisNo(0)) = True Then '到位逾時
                        If Not sender Is Nothing Then
                            CType(sender, Button).BackColor = Color.Red
                        End If
                        gSyslog.Save(gCMotion.AxisParameter(AxisNo(0)).AxisName & " Move Time Out!", , eMessageLevel.Warning)
                        MsgBox(gCMotion.AxisParameter(AxisNo(0)).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                        Return False
                    End If
                End If
            End If
            If AxisNo(1) > -1 Then 'C軸存在
                If gCMotion.MotionDone(AxisNo(1)) <> CommandStatus.Sucessed Then
                    isINP(1) = False 'Axis is Not Inposition
                    If gCMotion.IsMoveTimeOut(AxisNo(1)) Then '到位逾時
                        If Not sender Is Nothing Then
                            CType(sender, Button).BackColor = Color.Red
                        End If
                        gSyslog.Save(gCMotion.AxisParameter(AxisNo(1)).AxisName & " Move Time Out!", , eMessageLevel.Warning)
                        MsgBox(gCMotion.AxisParameter(AxisNo(1)).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                    End If
                End If

            End If
            If isINP(0) And isINP(1) Then '到位 離開等待迴圈
                Exit Do
            End If
        Loop
        gSyslog.Save(gCMotion.AxisParameter(enmAxis.XAxis).AxisName & " Move OK.")
        gSyslog.Save(gCMotion.AxisParameter(enmAxis.Y1Axis).AxisName & " Move OK.")
        'XY到位
        If gCMotion.AbsMove(AxisNo(2), TargetPos(2)) <> CommandStatus.Sucessed Then 'Z軸下降 then
            If Not sender Is Nothing Then
                CType(sender, Button).BackColor = Color.Red
            End If
            MsgBox("Z軸移動到" & TargetPos(2) & "失敗!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        gSyslog.Save(gCMotion.AxisParameter(AxisNo(2)).AxisName & " Move To (" & TargetPos(2) & ")")
        System.Threading.Thread.CurrentThread.Join(100) '移動命令下達後不能立刻看到位
        mStopWatch.Restart()
        Do
            Application.DoEvents()
            isINP(2) = True
            If AxisNo(2) > -1 Then 'C軸存在
                If gCMotion.MotionDone(AxisNo(2)) <> CommandStatus.Sucessed Then
                    isINP(2) = False 'Axis is Not Inposition
                    If gCMotion.IsMoveTimeOut(AxisNo(2)) Then '到位逾時
                        If Not sender Is Nothing Then
                            CType(sender, Button).BackColor = Color.Red
                        End If
                        gSyslog.Save(gCMotion.AxisParameter(AxisNo(2)).AxisName & " Move Time Out!", , eMessageLevel.Warning)
                        MsgBox(gCMotion.AxisParameter(AxisNo(2)).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                    End If
                End If

            End If
            If isINP(2) Then '到位 離開等待迴圈
                Exit Do
            End If
        Loop
        gSyslog.Save(gCMotion.AxisParameter(AxisNo(2)).AxisName & " Move OK.")
        '動作完成
        If Not sender Is Nothing Then
            CType(sender, Button).BackColor = SystemColors.Control
            CType(sender, Button).UseVisualStyleBackColor = True
        End If
        Return True
    End Function

    ''' <summary>按鍵用 安全移動 Tilt</summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Public Function ButtonSafeMoveTiltPos(ByRef sender As Button, ByVal AxisNo() As Integer, ByVal TargetPos() As Decimal, ByVal sys As sSysParam) As Boolean
        Dim isINP(2) As Boolean
        'Const TIME_OUT_IN_MS As Integer = 5000
        If AxisNo.Count > 2 Then
            MsgBox("ButtonSafeMovePos Not Supports.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        If AxisNo.Count < 2 Then
            MsgBox("ButtonSafeMovePos Not Supports.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        If Not gSYS(eSys.OverAll) Is Nothing Then
            If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
                MsgBox("Need Home!!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If
        End If
        If Not sender Is Nothing Then
            CType(sender, Button).BackColor = Color.Yellow '按鍵顏色
        End If


        Debug.Print(".AxisParameter.Count:" & gCMotion.AxisParameter.Count)
        For i As Integer = 0 To AxisNo.Count - 1
            If AxisNo(i) < gCMotion.AxisParameter.Count Then
                If AxisNo(i) >= 0 Then
                    gCMotion.SetVelHigh(AxisNo(i), gSSystemParameter.ManualVelHigh) 'gCMotion.AxisParameter(AxisNo(i)).Velocity.VelHigh)
                    gCMotion.SetVelLow(AxisNo(i), gCMotion.AxisParameter(AxisNo(i)).Velocity.VelLow)
                    gCMotion.SetAcc(AxisNo(i), gCMotion.AxisParameter(AxisNo(i)).Velocity.Acc * gCMotion.AxisParameter(AxisNo(i)).Velocity.AccRatio)
                    gCMotion.SetDec(AxisNo(i), gCMotion.AxisParameter(AxisNo(i)).Velocity.Dec * gCMotion.AxisParameter(AxisNo(i)).Velocity.DecRatio)
                End If

            End If
        Next

        If gCMotion.AbsMove(AxisNo(0), 0) <> CommandStatus.Sucessed Then 'Z上飛高
            CType(sender, Button).BackColor = Color.Red
            MsgBox("Z軸移動到0,失敗!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End If
        gSyslog.Save(gCMotion.AxisParameter(AxisNo(0)).AxisName & " Move to 0")
        System.Threading.Thread.CurrentThread.Join(100) '移動命令下達後不能立刻看到位
        mStopWatch.Restart()
        Do
            Application.DoEvents()
            If gCMotion.MotionDone(AxisNo(0)) = CommandStatus.Sucessed Then '到位 離開等待迴圈
                Exit Do
            End If
            If AxisNo(0) > -1 Then
                If gCMotion.IsMoveTimeOut(AxisNo(0)) Then '逾時 中斷離開
                    If Not sender Is Nothing Then
                        CType(sender, Button).BackColor = Color.Red
                    End If
                    Select Case sys.StageNo
                        Case 0
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1032004), , eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                        Case 1
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1042004), , eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1042004), MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                        Case 2
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1062004), , eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                        Case 3
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1069004), , eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                    End Select
                    Return False
                End If
            End If
        Loop
        gSyslog.Save(gCMotion.AxisParameter(AxisNo(0)).AxisName & " Move OK.")


        If AxisNo(1) > -1 Then
            If gCMotion.AbsMove(AxisNo(1), TargetPos(1)) <> CommandStatus.Sucessed Then
                MsgBox(gCMotion.AxisParameter(AxisNo(1)).AxisName & " AbsMove Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If
        End If
        mStopWatch.Restart()
        System.Threading.Thread.CurrentThread.Join(100) '移動命令下達後不能立刻看到位
        If gCMotion.MotionDone(AxisNo(1)) <> CommandStatus.Sucessed Then
            isINP(1) = False 'Axis is Not Inposition
            If gCMotion.IsMoveTimeOut(AxisNo(1)) = True Then '到位逾時
                If Not sender Is Nothing Then
                    CType(sender, Button).BackColor = Color.Red
                End If
                gSyslog.Save(gCMotion.AxisParameter(AxisNo(1)).AxisName & " Move Time Out!", , eMessageLevel.Warning)
                MsgBox(gCMotion.AxisParameter(AxisNo(1)).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                Return False
            End If
        End If
        ' gSyslog.Save(gCMotion.AxisParameter(AxisNo(1)).AxisName & " Move OK.")
        '動作完成
        If Not sender Is Nothing Then
            CType(sender, Button).BackColor = SystemColors.Control
            CType(sender, Button).UseVisualStyleBackColor = True
        End If
        Return True
    End Function

    ''' <summary>Sensor背景影像套用</summary>
    ''' <param name="refPanel"></param>
    ''' <param name="status"></param>
    ''' <param name="isPass"></param>
    ''' <remarks></remarks>
    Public Sub SetSensorBackgroundImage(ByRef refPanel As System.Windows.Forms.Panel, ByVal status As Boolean, ByVal isPass As Boolean)
        If isPass Then
            refPanel.BackgroundImage = My.Resources.li_28 '無燈
            Exit Sub
        End If
        If status Then
            refPanel.BackgroundImage = My.Resources.li_08 '紅燈
        Else
            refPanel.BackgroundImage = My.Resources.li_23 '不亮
        End If
    End Sub



    ''' <summary>判斷位置是否超出軟體極限</summary>
    ''' <remarks></remarks>
    Public Function EstimateSafePos(ByVal Pos As Premtek.sPos, ByVal AxisNo() As Integer) As Boolean
        If Pos.PosX < gCMotion.AxisParameter(AxisNo(0)).Limit.NegativeLimit Or Pos.PosX > gCMotion.AxisParameter(AxisNo(0)).Limit.PosivtiveLimit Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("X Axis  Command is Out of limit!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("X轴位置超出极限!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("X軸位置超出極限!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            Return False
        End If
        If Pos.PosY < gCMotion.AxisParameter(AxisNo(1)).Limit.NegativeLimit Or Pos.PosY > gCMotion.AxisParameter(AxisNo(1)).Limit.PosivtiveLimit Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("Y Axis  Command is Out of limit!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("Y轴位置超出极限!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("Y軸位置超出極限!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            Return False
        End If
        If Pos.PosZ < gCMotion.AxisParameter(AxisNo(2)).Limit.NegativeLimit Or Pos.PosZ > gCMotion.AxisParameter(AxisNo(2)).Limit.PosivtiveLimit Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("Z Axis  Command is Out of limit!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("Z轴位置超出极限!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("Z軸位置超出極限!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            Return False
        End If
        Return True
    End Function

    ''' <summary>修改速度為優化速度</summary>
    ''' <param name="axisNo">軸號</param>
    ''' <param name="targetPos">目標位置</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReviseVelocity(ByVal axisNo As Integer, ByVal targetPos As Decimal, ByVal MaxVelLimit As Decimal) As CommandStatus
        With gCMotion.AxisParameter(axisNo).Velocity
            Dim mNowPos As Decimal
            mNowPos = gCMotion.GetPositionValue(axisNo)

            Dim mDistance As Decimal = Math.Abs(targetPos - mNowPos)
            Dim mVelocity As Decimal = gCMotion.AxisParameter(axisNo).Velocity.MaxVel
            Premtek.CDispensingMath.GetCrossVelocity(MaxVelLimit, .Acc * .AccRatio, .Dec * .DecRatio, mDistance, gSSystemParameter.CrossVerticalTime, mVelocity)
            If mVelocity <> 0 Then
                Return gCMotion.SetVelHigh(axisNo, mVelocity)
            Else
                Return CommandStatus.Sucessed
            End If

        End With
    End Function

    ''' <summary>取得平台X軸極限
    ''' </summary>
    ''' <param name="stageNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPELPosX(ByVal stageNo As enmStage) As Decimal
        Select Case gSSystemParameter.MachineMax
            Case eSys.MachineA
                Select Case gSSystemParameter.StageCount
                    Case 1
                        Return gCMotion.AxisParameter(gSYS(eSys.DispStage1).AxisX).Limit.PosivtiveLimit
                    Case 2
                        Select Case stageNo
                            Case enmStage.No1
                                Dim curPosX As Decimal = gCMotion.GetPositionValue(gSYS(eSys.DispStage2).AxisX)
                                Dim cmdPosX As Decimal
                                gCMotion.GetTargetPos(gSYS(eSys.DispStage2).AxisX, cmdPosX)
                                If cmdPosX < curPosX Then
                                    Return gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX + cmdPosX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX
                                Else
                                    Return gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX + curPosX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX
                                End If

                            Case enmStage.No2
                                Return gCMotion.AxisParameter(gSYS(eSys.DispStage2).AxisX).Limit.PosivtiveLimit
                        End Select

                End Select

            Case eSys.MachineB
                Select Case stageNo
                    Case enmStage.No1
                        Dim curPosX As Decimal = gCMotion.GetPositionValue(gSYS(eSys.DispStage2).AxisX)
                        Dim cmdPosX As Decimal
                        gCMotion.GetTargetPos(gSYS(eSys.DispStage2).AxisX, cmdPosX)
                        If cmdPosX < curPosX Then
                            Return gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX + cmdPosX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX
                        Else
                            Return gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX + curPosX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX
                        End If
                    Case enmStage.No2
                        Return gCMotion.AxisParameter(gSYS(eSys.DispStage2).AxisX).Limit.PosivtiveLimit
                    Case enmStage.No3
                        Dim curPosX As Decimal = gCMotion.GetPositionValue(gSYS(eSys.DispStage4).AxisX)
                        Dim cmdPosX As Decimal
                        gCMotion.GetTargetPos(gSYS(eSys.DispStage4).AxisX, cmdPosX)
                        If cmdPosX < curPosX Then
                            Return gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SpreadX + cmdPosX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SafeDistanceX
                        Else
                            Return gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SpreadX + curPosX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SafeDistanceX
                        End If
                    Case enmStage.No4
                        Return gCMotion.AxisParameter(gSYS(eSys.DispStage4).AxisX).Limit.PosivtiveLimit
                End Select
        End Select
        
        Return 0
    End Function

    Public Function GetNELPosX(ByVal stageNo As enmStage) As Decimal
        Select Case gSSystemParameter.MachineMax
            Case eSys.MachineA
                Select Case gSSystemParameter.StageCount
                    Case 1
                        Return gCMotion.AxisParameter(gSYS(eSys.DispStage1).AxisX).Limit.NegativeLimit
                    Case 2
                        Select Case stageNo
                            Case enmStage.No1
                                Return gCMotion.AxisParameter(gSYS(eSys.DispStage1).AxisX).Limit.NegativeLimit
                            Case enmStage.No2
                                Dim curPosX As Decimal = gCMotion.GetPositionValue(gSYS(eSys.DispStage1).AxisX)
                                Dim cmdPosX As Decimal
                                gCMotion.GetTargetPos(gSYS(eSys.DispStage1).AxisX, cmdPosX)
                                If cmdPosX > curPosX Then
                                    Return -(gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX + cmdPosX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX)
                                Else
                                    Return -(gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX + curPosX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX)
                                End If
                        End Select
                End Select

            Case eSys.MachineB
                Select Case stageNo
                    Case enmStage.No1
                         Return gCMotion.AxisParameter(gSYS(eSys.DispStage1).AxisX).Limit.NegativeLimit
                    Case enmStage.No2
                        Dim curPosX As Decimal = gCMotion.GetPositionValue(gSYS(eSys.DispStage1).AxisX)
                        Dim cmdPosX As Decimal
                        gCMotion.GetTargetPos(gSYS(eSys.DispStage1).AxisX, cmdPosX)
                        If cmdPosX > curPosX Then
                            Return -(gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX + cmdPosX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX)
                        Else
                            Return -(gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX + curPosX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX)
                        End If
                    Case enmStage.No3
                        Return gCMotion.AxisParameter(gSYS(eSys.DispStage3).AxisX).Limit.NegativeLimit
                    Case enmStage.No4
                        Dim curPosX As Decimal = gCMotion.GetPositionValue(gSYS(eSys.DispStage3).AxisX)
                        Dim cmdPosX As Decimal
                        gCMotion.GetTargetPos(gSYS(eSys.DispStage3).AxisX, cmdPosX)
                        If cmdPosX > curPosX Then
                            Return -(gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SpreadX + cmdPosX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SafeDistanceX)
                        Else
                            Return -(gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SpreadX + curPosX - gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SafeDistanceX)
                        End If
                End Select
        End Select

        Return 0
    End Function

    ' ''' <summary>[比對位置是否落在安全範圍內]</summary>
    ' ''' <param name="stageNo"></param>
    ' ''' <param name="dispProtect">[另一側的資料]</param>
    ' ''' <param name="estimatePos">[目標位置(估測)]</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function IsSafePos(ByVal stageNo As enmStage, ByRef dispProtect As sProtectData, ByVal estimatePos As Premtek.sPos) As Boolean

    '    '[Note]:比對是否超出保護框架
    '    Select Case stageNo
    '        Case enmStage.No1
    '            If mStageState(enmStage.No2) = False Then
    '                Return True
    '            End If
    '            '[說明]:更新另一側的目前座標
    '            dispProtect.NowPos.PosX = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage2).AxisX))
    '            dispProtect.NowPos.PosY = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage2).AxisY))
    '            '[Note]:先檢查X方向，Y方向視情況再決定要不要加
    '            '       跨距-L+R(Now)>S
    '            If gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX - estimatePos.PosX + dispProtect.NowPos.PosX >= gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX Then
    '                '[Note]:跨距-L+R(Target)>S
    '                If gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX - estimatePos.PosX + dispProtect.TargetPos.PosX >= gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX Then
    '                    Return True
    '                Else
    '                    Debug.Print("OOOOOOO" & gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX - estimatePos.PosX + dispProtect.TargetPos.PosX)
    '                    Debug.Print("XXXXXXX" & gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX)
    '                End If
    '            End If

    '        Case enmStage.No2
    '            If mStageState(enmStage.No1) = False Then
    '                Return True
    '            End If
    '            dispProtect.NowPos.PosX = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage1).AxisX))
    '            dispProtect.NowPos.PosY = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage1).AxisY))
    '            '[Note]:先檢查X方向，Y方向視情況再決定要不要加
    '            '       跨距-L(Now)+R>S
    '            If gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX + estimatePos.PosX - dispProtect.NowPos.PosX >= gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX Then
    '                '[Note]:跨距-L(Target)+R>S
    '                If gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SpreadX + estimatePos.PosX - dispProtect.TargetPos.PosX >= gSSystemParameter.MachineSafeData(enmMachineStation.MachineA).SafeDistanceX Then
    '                    Return True
    '                End If
    '            End If

    '        Case enmStage.No3
    '            If mStageState(enmStage.No4) = False Then
    '                Return True
    '            End If
    '            dispProtect.NowPos.PosX = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage4).AxisX))
    '            dispProtect.NowPos.PosY = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage4).AxisY))
    '            '[Note]:先檢查X方向，Y方向視情況再決定要不要加
    '            '       跨距-L+R(Now)>S
    '            If gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SpreadX - estimatePos.PosX + dispProtect.NowPos.PosX >= gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SafeDistanceX Then
    '                '[Note]:跨距-L+R(Target)>S
    '                If gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SpreadX - estimatePos.PosX + dispProtect.TargetPos.PosX >= gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SafeDistanceX Then
    '                    Return True
    '                End If
    '            End If

    '        Case enmStage.No4
    '            If mStageState(enmStage.No3) = False Then
    '                Return True
    '            End If
    '            dispProtect.NowPos.PosX = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage3).AxisX))
    '            dispProtect.NowPos.PosY = CDec(gCMotion.GetPositionValue(gSYS(eSys.DispStage3).AxisY))
    '            '[Note]:先檢查X方向，Y方向視情況再決定要不要加
    '            '       跨距-L+R(Now)>S
    '            If gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SpreadX + estimatePos.PosX - dispProtect.NowPos.PosX >= gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SafeDistanceX Then
    '                '[Note]:跨距-L+R(Target)>S
    '                If gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SpreadX + estimatePos.PosX - dispProtect.TargetPos.PosX >= gSSystemParameter.MachineSafeData(enmMachineStation.MachineB).SafeDistanceX Then
    '                    Return True
    '                End If
    '            End If

    '    End Select

    '    Return False

    'End Function

End Module

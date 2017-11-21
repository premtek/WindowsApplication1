Imports ProjectCore
Imports ProjectMotion
Imports ProjectIO
Imports ProjectLaserInterferometer
Public Class frmContactCablication
    Public sys As sSysParam
    Dim mStopWatch As New Stopwatch

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim dCurrentZ, dLaserZeroZ As Decimal
        Dim iRetryCount As Integer = 30
        Dim iSearchStep As Integer = 10
        Dim TimeOutStopwatch As New Stopwatch
        Dim HeightValue As String = ""
        Dim dSearchZ As Double

        If MsgBox("stage會移到試點平台，請清空試點平台並確認上面無膠材", MsgBoxStyle.OkCancel + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbOK Then
            '移動到試點平台
            '校正

            '按鍵保護
            If Button3.Enabled = False Then
                Exit Sub
            End If

            'Z軸速度設定
            gCMotion.SetVelLow(sys.AxisZ, 0)
            gCMotion.SetVelHigh(sys.AxisZ, 50)
            gCMotion.SetAcc(sys.AxisZ, 1000)
            gCMotion.SetDec(sys.AxisZ, 1000)

            '按鍵保護
            Button3.Enabled = False
            UcJoyStick1.Enabled = False
            btnBack.Enabled = False

            '先移到Z軸0安全高度
            SafeLaserZMove(0)

            'X&Y 移動至試點平台
            With gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo)
                gSyslog.Save(gMsgHandler.GetMessage(INFO_6019015, .CCDX(sys.SelectValve, 0), .CCDY(sys.SelectValve, 0), .CCDZ(sys.SelectValve, 0)), "INFO_6019015")
                Dim AxisNo(4) As Integer
                Dim TargetPos(4) As Decimal
                AxisNo(0) = sys.AxisX
                AxisNo(1) = sys.AxisY
                AxisNo(2) = sys.AxisZ
                AxisNo(3) = sys.AxisB
                AxisNo(4) = sys.AxisC

                TargetPos(0) = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDX(sys.SelectValve, 0) - gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetX(sys.SelectValve)
                TargetPos(1) = gSSystemParameter.Pos.CCDTiltVavleCalbration(sys.StageNo).CCDY(sys.SelectValve, 0) - gSSystemParameter.Pos.CCDLaserCalibration(sys.StageNo).CCDLaserOffsetY(sys.SelectValve)
                TargetPos(2) = 0
                TargetPos(3) = 0
                TargetPos(4) = 0
                ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
            End With



            '先推汽缸，將汽缸全推出的數值設為0
            gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, True)
            Call gDOCollection.RefreshDO()
            System.Threading.Thread.CurrentThread.Join(500)
            '歸零 Reader
            gDOCollection.SetState(enmDO.ContactHeightReZero, True)
            Call gDOCollection.RefreshDO()
            System.Threading.Thread.CurrentThread.Join(500)
            '歸零DO 回復
            gDOCollection.SetState(enmDO.ContactHeightReZero, False)
            Call gDOCollection.RefreshDO()

            If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                Button3.BackColor = Color.Red
                Select Case sys.StageNo
                    Case enmStage.No1
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1014004), "Error_1014004", eMessageLevel.Error)
                        MsgBox(gMsgHandler.GetMessage(Error_1014004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case enmStage.No2
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1014104), "Error_1014104", eMessageLevel.Error)
                        MsgBox(gMsgHandler.GetMessage(Error_1014104), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case enmStage.No3
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1014204), "Error_1014204", eMessageLevel.Error)
                        MsgBox(gMsgHandler.GetMessage(Error_1014204), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case enmStage.No4
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1014304), "Error_1014304", eMessageLevel.Error)
                        MsgBox(gMsgHandler.GetMessage(Error_1014304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
                Button3.BackColor = System.Drawing.Color.Red
                Button3.Enabled = True
                '按鍵保護
                UcJoyStick1.Enabled = True
                btnBack.Enabled = True

                Exit Sub
            End If

            dCurrentZ = Val(gCMotion.GetPositionValue(sys.AxisZ))
            UcJoyStick1.SetSpeedType(SpeedType.Fast)

            If Val(HeightValue) < -10 Or Val(HeightValue) > 10 Then
                dSearchZ = dCurrentZ
                While (dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.PosivtiveLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)
                    SafeLaserZMove(dSearchZ)
                    dSearchZ = Val(gCMotion.GetPositionValue(UcJoyStick1.AxisZ))

                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                        Button3.BackColor = Color.Red
                        Select Case sys.StageNo
                            Case enmStage.No1
                                gSyslog.Save(gMsgHandler.GetMessage(Error_1014004), "Error_1014004", eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1014004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case enmStage.No2
                                gSyslog.Save(gMsgHandler.GetMessage(Error_1014104), "Error_1014104", eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1014104), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case enmStage.No3
                                gSyslog.Save(gMsgHandler.GetMessage(Error_1014204), "Error_1014204", eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1014204), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case enmStage.No4
                                gSyslog.Save(gMsgHandler.GetMessage(Error_1014304), "Error_1014304", eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1014304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        End Select
                        Button3.BackColor = System.Drawing.Color.Red
                        Button3.Enabled = True
                        '按鍵保護
                        UcJoyStick1.Enabled = True
                        btnBack.Enabled = True

                        Exit Sub
                    End If

                    dSearchZ += iSearchStep
                    iRetryCount -= 1
                End While
                dSearchZ = dCurrentZ
                While (dSearchZ > gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit And (Val(HeightValue) < -10 Or Val(HeightValue) > 10) And iRetryCount > 0)

                    SafeLaserZMove(dSearchZ)
                    dSearchZ = Val(gCMotion.GetPositionValue(UcJoyStick1.AxisZ))
                    If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                        Button3.BackColor = Color.Red
                        Select Case sys.StageNo
                            Case enmStage.No1
                                gSyslog.Save(gMsgHandler.GetMessage(Error_1014004), "Error_1014004", eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1014004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case enmStage.No2
                                gSyslog.Save(gMsgHandler.GetMessage(Error_1014104), "Error_1014104", eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1014104), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case enmStage.No3
                                gSyslog.Save(gMsgHandler.GetMessage(Error_1014204), "Error_1014204", eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1014204), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            Case enmStage.No4
                                gSyslog.Save(gMsgHandler.GetMessage(Error_1014304), "Error_1014304", eMessageLevel.Error)
                                MsgBox(gMsgHandler.GetMessage(Error_1014304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        End Select
                        Button3.BackColor = System.Drawing.Color.Red
                        Button3.Enabled = True
                        '按鍵保護
                        UcJoyStick1.Enabled = True
                        btnBack.Enabled = True

                        Exit Sub
                    End If
                    dSearchZ -= iSearchStep
                    iRetryCount -= 1
                End While
            End If

            UcJoyStick1.SetSpeedType(SpeedType.Slow)
            If iRetryCount <= 0 Then
                '測高儀自動測高失敗
                Select Case sys.StageNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014003))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2014003), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014103))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2014103), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014203))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2014203), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Alarm_2014303))
                        MsgBox(gMsgHandler.GetMessage(Alarm_2014303), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
                Button3.BackColor = System.Drawing.Color.Red
                Button3.Enabled = True
                '按鍵保護
                UcJoyStick1.Enabled = True
                btnBack.Enabled = True
                Exit Sub
            End If
            If dSearchZ < gCMotion.AxisParameter(sys.AxisZ).Limit.NegativeLimit Then
                'Z 軸碰到負極限
                Select Case sys.StageNo
                    Case 0
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1032008))
                        MsgBox(gMsgHandler.GetMessage(Error_1032008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 1
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1044008))
                        MsgBox(gMsgHandler.GetMessage(Error_1044008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 2
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1062008))
                        MsgBox(gMsgHandler.GetMessage(Error_1062008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case 3
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1069008))
                        MsgBox(gMsgHandler.GetMessage(Error_1069008), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
                Button3.BackColor = System.Drawing.Color.Red
                Button3.Enabled = True
                '按鍵保護
                UcJoyStick1.Enabled = True
                btnBack.Enabled = True
                Exit Sub
            End If


            For i As Integer = 0 To 10
                If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.LaserNo, HeightValue, True) = False Then
                    '測高儀讀值失敗
                    Select Case sys.StageNo
                        Case enmStage.No1
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014004), "Error_1014004", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No2
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014104), "Error_1014104", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014104), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No3
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014204), "Error_1014204", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014204), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No4
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1014304), "Error_1014304", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1014304), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    Button3.BackColor = System.Drawing.Color.Red
                    Button3.Enabled = True
                    '按鍵保護
                    UcJoyStick1.Enabled = True
                    btnBack.Enabled = True


                    '汽缸動作(收回)
                    gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
                    Call gDOCollection.RefreshDO()
                    '汽缸動作_end

                    Exit Sub
                End If
                dCurrentZ = Val(gCMotion.GetPositionValue(UcJoyStick1.AxisZ))
                dLaserZeroZ = dCurrentZ + (Val(HeightValue) - 6)
                SafeLaserZMove(dLaserZeroZ)
                System.Threading.Thread.CurrentThread.Join(500)
            Next


            '歸零 Reader
            gDOCollection.SetState(enmDO.ContactHeightReZero, True)
            Call gDOCollection.RefreshDO()
            System.Threading.Thread.CurrentThread.Join(500)
            '歸零DO 回復
            gDOCollection.SetState(enmDO.ContactHeightReZero, False)
            Call gDOCollection.RefreshDO()
            System.Threading.Thread.CurrentThread.Join(500)
            '汽缸動作(收回)
            gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
            Call gDOCollection.RefreshDO()
            '汽缸動作_end

            '回到安全高度
            SafeLaserZMove(0)
            Button3.Enabled = True
            '按鍵保護
            UcJoyStick1.Enabled = True
            btnBack.Enabled = True

        Else
            MsgBox("請清空試點平台", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        End If

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        '離開前汽缸收回
        gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
        Call gDOCollection.RefreshDO()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, Not gDOCollection.GetState(enmDO.ContactHeightSolenoidValve))
        'Select Case gDOCollection.GetSetState(enmDO.ContactHeightSolenoidValve)
        '    Case True
        '        Button1.Text = "接觸式汽缸On"
        '    Case False
        '        Button1.Text = "接觸式汽缸Off"
        'End Select
        Call gDOCollection.RefreshDO()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        '歸零 Reader
        gDOCollection.SetState(enmDO.ContactHeightReZero, True)
        Call gDOCollection.RefreshDO()
        System.Threading.Thread.CurrentThread.Join(500)

        '歸零DO 回復
        gDOCollection.SetState(enmDO.ContactHeightReZero, False)
        Call gDOCollection.RefreshDO()


    End Sub

    Private Sub frmContactCablication_Load(sender As Object, e As EventArgs) Handles Me.Load
        '20170514 jog防護
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
            UcJoyStick1.Enabled = False
        Else
            UcJoyStick1.Enabled = True
        End If

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

        'Select Case gDOCollection.GetSetState(enmDO.ContactHeightSolenoidValve)
        '    Case True
        '        Button1.Text = "接觸式汽缸On"
        '    Case False
        '        Button1.Text = "接觸式汽缸Off"
        'End Select

    End Sub
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
    Private Sub SafeLaserZMove(ByVal TargetPos As Decimal)

        If gCMotion.AbsMove(sys.AxisZ, TargetPos) <> CommandStatus.Sucessed Then
            'Z 軸移動失敗
            Select Case sys.StageNo
                Case 0
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1032000) & TargetPos)
                    MsgBox(gMsgHandler.GetMessage(Error_1032000) & TargetPos, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 1
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1044000) & TargetPos)
                    MsgBox(gMsgHandler.GetMessage(Error_1044000) & TargetPos, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 2
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1062000) & TargetPos)
                    MsgBox(gMsgHandler.GetMessage(Error_1062000) & TargetPos, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case 3
                    gSyslog.Save(gMsgHandler.GetMessage(Error_1069000) & TargetPos)
                    MsgBox(gMsgHandler.GetMessage(Error_1069000) & TargetPos, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            Exit Sub
        End If

        'gCMotion.AbsMove(sys.AxisZ, TargetPos) '移至targetPos
        System.Threading.Thread.CurrentThread.Join(200) '移動命令下達後不能立刻看到位


        mStopWatch.Restart()
        Do
            Application.DoEvents() 'Toby Modify_20170513(拿掉doEvents)
            If gCMotion.MotionDone(sys.AxisZ) = CommandStatus.Sucessed Then '到位 離開等待迴圈
                Exit Do
            End If
            If sys.AxisZ > -1 Then
                If gCMotion.IsMoveTimeOut(sys.AxisZ) Then '逾時 中斷離開
                    Select Case sys.StageNo
                        Case enmStage.No1
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1032004), "Error_1032004", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No2
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1044004), "Error_1044004", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No3
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1062004), "Error_1062004", eMessageLevel.Error)
                            MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        Case enmStage.No4
                            gSyslog.Save(gMsgHandler.GetMessage(Error_1069004), "Error_1069004", eMessageLevel.Error) '
                            MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    End Select
                    MsgBox(gCMotion.AxisParameter(sys.AxisZ).AxisName & " Move Time Out!", MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground, "Premtek")
                    Exit Do
                    Exit Sub
                End If
            End If

            If mStopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut4 Then
                Select Case sys.StageNo
                    Case enmStage.No1
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1032004), "Error_1032004", eMessageLevel.Error)
                        MsgBox(gMsgHandler.GetMessage(Error_1032004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case enmStage.No2
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1044004), "Error_1044004", eMessageLevel.Error)
                        MsgBox(gMsgHandler.GetMessage(Error_1044004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case enmStage.No3
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1062004), "Error_1062004", eMessageLevel.Error)
                        MsgBox(gMsgHandler.GetMessage(Error_1062004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Case enmStage.No4
                        gSyslog.Save(gMsgHandler.GetMessage(Error_1069004), "Error_1069004", eMessageLevel.Error)
                        MsgBox(gMsgHandler.GetMessage(Error_1069004), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                End Select
                MsgBox(gCMotion.AxisParameter(sys.AxisZ).AxisName & " Move Time Out!", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Exit Do
                Exit Sub
            End If

        Loop

    End Sub
End Class
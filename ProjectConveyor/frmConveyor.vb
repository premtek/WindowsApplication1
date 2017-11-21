Imports ProjectIO
Imports ProjectCore
Imports ProjectMotion

Public Class frmConveyor

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        

        'Station1 燈號顯示
        If gDICollection.GetState(enmDI.Station1TrayReady, True) = True Then
            lblStation1TrayReadySensor.BackColor = Color.Lime
        Else
            lblStation1TrayReadySensor.BackColor = SystemColors.Control
        End If

        If gDICollection.GetState(enmDI.Station1StopperUpReady, True) = True Then
            lblStation1StopperUpSensor.BackColor = Color.Lime
        Else
            lblStation1StopperUpSensor.BackColor = SystemColors.Control
        End If
        If gDICollection.GetState(enmDI.Station1StopperDownReady, True) = True Then
            lblStation1StopperDownSensor.BackColor = Color.Lime
        Else
            lblStation1StopperDownSensor.BackColor = SystemColors.Control
        End If


        If gDICollection.GetState(enmDI.Station1TopLiftUpReady, True) = True Then
            lblStation1TopLiftUpSensor.BackColor = Color.Lime
        Else
            lblStation1TopLiftUpSensor.BackColor = SystemColors.Control
        End If
        If gDICollection.GetState(enmDI.Station1TopLiftDownReady, True) = True Then
            lblStation1TopLiftDownSensor.BackColor = Color.Lime
        Else
            lblStation1TopLiftDownSensor.BackColor = SystemColors.Control
        End If


        'Station2 燈號顯示
        If gDICollection.GetState(enmDI.Station2TrayReady, True) = True Then
            lblStation2TrayReadySensor.BackColor = Color.Lime
        Else
            lblStation2TrayReadySensor.BackColor = SystemColors.Control
        End If

        If gDICollection.GetState(enmDI.Station2StopperUpReady, True) = True Then
            lblStation2StopperUpSensor.BackColor = Color.Lime
        Else
            lblStation2StopperUpSensor.BackColor = SystemColors.Control
        End If
        If gDICollection.GetState(enmDI.Station2StopperDownReady, True) = True Then
            lblStation2StopperDownSensor.BackColor = Color.Lime
        Else
            lblStation2StopperDownSensor.BackColor = SystemColors.Control
        End If
        If gDICollection.GetState(enmDI.TrayClamperOnReady, True) = True Then
            lblTrayClamperOn.BackColor = Color.Lime
        Else
            lblTrayClamperOn.BackColor = SystemColors.Control
        End If

        If gDICollection.GetState(enmDI.TrayClamperOffReady, True) = True Then
            lblTrayClamperOff.BackColor = Color.Lime
        Else
            lblTrayClamperOff.BackColor = SystemColors.Control
        End If



        'Station3 燈號顯示
        If gDICollection.GetState(enmDI.Station3TrayReady, True) = True Then
            lblStation3TrayReadySensor.BackColor = Color.Lime
        Else
            lblStation3TrayReadySensor.BackColor = SystemColors.Control
        End If

        If gDICollection.GetState(enmDI.Station3StopperUpReady, True) = True Then
            lblStation3StopperUpSensor.BackColor = Color.Lime
        Else
            lblStation3StopperUpSensor.BackColor = SystemColors.Control
        End If
        If gDICollection.GetState(enmDI.Station3StopperDownReady, True) = True Then
            lblStation3StopperDownSensor.BackColor = Color.Lime
        Else
            lblStation3StopperDownSensor.BackColor = SystemColors.Control
        End If

        If gDICollection.GetState(enmDI.Station3TopLiftUpReady, True) = True Then
            lblStation3TopLiftUpSensor.BackColor = Color.Lime
        Else
            lblStation3TopLiftUpSensor.BackColor = SystemColors.Control
        End If
        If gDICollection.GetState(enmDI.Station3TopLiftDownReady, True) = True Then
            lblStation3TopLiftDownSensor.BackColor = Color.Lime
        Else
            lblStation3TopLiftDownSensor.BackColor = SystemColors.Control
        End If


        'Load 訊號
        If gDICollection.GetState(enmDI.BoardAvailable, True) = True Then
            lblLoadBoardAvaiable.BackColor = Color.Lime
        Else
            lblLoadBoardAvaiable.BackColor = SystemColors.Control
        End If

        'UnLoad 訊號
        If gDICollection.GetState(enmDI.MachineReadyToRecieve, True) = True Then
            lblUnLoadRecieveTray.BackColor = Color.Lime
        Else
            lblUnLoadRecieveTray.BackColor = SystemColors.Control
        End If


        'Label2.Text = gCMotion.HomeFinish(6).ToString
        'gCMotion.GetCmdStatus()
    End Sub

    Private Sub ckbStation1Stopper_CheckedChanged(sender As Object, e As EventArgs) Handles ckbStation1Stopper.CheckedChanged
        If ckbStation1Stopper.Checked = False Then
            gDOCollection.SetState(enmDO.Station1StopperUpDown, False)
            ckbStation1Stopper.BackColor = SystemColors.Control

        Else
            gDOCollection.SetState(enmDO.Station1StopperUpDown, True)
            ckbStation1Stopper.BackColor = Color.Yellow
        End If

    End Sub

    Private Sub ckbStation2Stopper_CheckedChanged(sender As Object, e As EventArgs) Handles ckbStation2Stopper.CheckedChanged
        If ckbStation2Stopper.Checked = False Then
            gDOCollection.SetState(enmDO.Station2StopperUp, False)
            ckbStation2Stopper.BackColor = SystemColors.Control
        Else
            gDOCollection.SetState(enmDO.Station2StopperUp, True)
            ckbStation2Stopper.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub ckbStation3Stopper_CheckedChanged(sender As Object, e As EventArgs) Handles ckbStation3Stopper.CheckedChanged
        If ckbStation3Stopper.Checked = False Then
            gDOCollection.SetState(enmDO.Station3StopperUp, False)
            ckbStation3Stopper.BackColor = SystemColors.Control
        Else
            gDOCollection.SetState(enmDO.Station3StopperUp, True)
            ckbStation3Stopper.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub ckbStation1TopLiftUpDown_CheckedChanged(sender As Object, e As EventArgs) Handles ckbStation1TopLiftUpDown.CheckedChanged
        If ckbStation1TopLiftUpDown.Checked = False Then
            gDOCollection.SetState(enmDO.Station1TopLiftUpDown, False)
            ckbStation1TopLiftUpDown.BackColor = SystemColors.Control
        Else
            gDOCollection.SetState(enmDO.Station1TopLiftUpDown, True)
            ckbStation1TopLiftUpDown.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub ckbStation3TopLiftUpDown_CheckedChanged(sender As Object, e As EventArgs) Handles ckbStation3TopLiftUpDown.CheckedChanged
        If ckbStation3TopLiftUpDown.Checked = False Then
            gDOCollection.SetState(enmDO.Station3TopLiftUpDown, False)
            ckbStation3TopLiftUpDown.BackColor = SystemColors.Control
        Else
            gDOCollection.SetState(enmDO.Station3TopLiftUpDown, True)
            ckbStation3TopLiftUpDown.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub ckbStation2TrayClamper_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        SaveConveyorConnectionParameter(Application.StartupPath & "\system\" & MachineName & "\SystemParamter.ini") '儲存Conveyor連線參數

        ' 存檔參數
        SaveConveyorParameter()
    End Sub

    ''' <summary>儲存Conveyor參數</summary>
    Public Sub SaveConveyorParameter()

        Dim filename As String
        filename = Application.StartupPath & "\System\" & MachineName & "\ConveyorSet.ini"
        Dim strSection As String = "Control Parameter"

        '-----------------以下將System Set資料寫入------------------------------------
        With gCRegister
            .boolNoUseStation1 = CBool(ckbNoUseStation1.Checked)
            .boolNoUseStation2 = CBool(ckbNoUseStation2.Checked)
            .boolNoUseStation3 = CBool(ckbNoUseStation3.Checked)
            .intRollSpeed = CInt(cboRollSpeed.SelectedIndex)
            .intStation2TopLiftSpeed = CInt(cboStation2TopLiftSpeed.SelectedIndex)
            .dblStation2TopUpPos = CDbl(txtStation2TopLiftUpPos.Text)
            .dblStation2TopDownPos = CDbl(txtStation2TopLiftDownPos.Text)
            .dblStation1HeaterSet = CDbl(txtStation1heaterSet.Text)
            .dblStation2HeaterSet = CDbl(txtStation2heaterSet.Text)
            .dblStation3HeaterSet = CDbl(txtStation3heaterSet.Text)

            If cmbRunMode.SelectedIndex >= 0 Then
                ConveyorA.RunMode = cmbRunMode.SelectedIndex
            End If


            'Write Parameter
            SaveIniString(strSection, "NoUseStation1", .boolNoUseStation1, filename)
            SaveIniString(strSection, "NoUseStation2", .boolNoUseStation2, filename)
            SaveIniString(strSection, "NoUseStation3", .boolNoUseStation3, filename)
            SaveIniString(strSection, "RollSpeed", .intRollSpeed, filename)
            SaveIniString(strSection, "Station2TopLiftSpeed", .intStation2TopLiftSpeed, filename)
            SaveIniString(strSection, "Station2TopLiftUpPos", .dblStation2TopUpPos, filename)
            SaveIniString(strSection, "Station2TopLiftDownPos", .dblStation2TopDownPos, filename)
            SaveIniString(strSection, "Station1HeaterSet", .dblStation1HeaterSet, filename)
            SaveIniString(strSection, "Station2HeaterSet", .dblStation2HeaterSet, filename)
            SaveIniString(strSection, "Station3HeaterSet", .dblStation3HeaterSet, filename)

            SaveIniString(strSection, "ConveyorA.RunMode", ConveyorA.RunMode, filename)
            '程式內部用-----------------------------------------------------------------

            '關閉/開啟
            ConveyorA.DisableStation(1, .boolNoUseStation1)
            ConveyorA.DisableStation(2, .boolNoUseStation2)
            ConveyorA.DisableStation(3, .boolNoUseStation3)

            Select Case .intRollSpeed '滾輪速度設定
                Case 0
                    ConveyorA.SetRollSpeedType(SpeedType.Slow)
                Case 1
                    ConveyorA.SetRollSpeedType(SpeedType.Medium)
                Case 2
                    ConveyorA.SetRollSpeedType(SpeedType.Fast)
            End Select

            Select Case .intStation2TopLiftSpeed  'Chuck 2 馬達速度設定
                Case 0
                    ConveyorA.SetMotorSpeedType(SpeedType.Slow)
                Case 1
                    ConveyorA.SetMotorSpeedType(SpeedType.Medium)
                Case 2
                    ConveyorA.SetMotorSpeedType(SpeedType.Fast)
            End Select

            'Chuck2 馬達上升/下降點位設定
            ConveyorA.SetMotorUpPos(2, .dblStation2TopUpPos)
            ConveyorA.SetMotorDownPos(2, .dblStation2TopDownPos)

            '設定加熱溫度
            ConveyorA.SetTargetTemperature(1, .dblStation1HeaterSet)
            ConveyorA.SetTargetTemperature(2, .dblStation2HeaterSet)
            ConveyorA.SetTargetTemperature(3, .dblStation3HeaterSet)
        End With
    End Sub

    ''' <summary>讀出Conveyor參數</summary>
    ''' <remarks></remarks>
    Public Sub ReadConveyorParameter()

        Dim strFileName As String = Application.StartupPath & "\System\" & MachineName & "\ConveyorSet.ini"
        Dim strSection As String = "Control Parameter"
        Dim strBufferSize As String = "1024"
        Dim strDefual As String = "0"
        Dim strReadData As String = New String(Chr(0), strBufferSize)

        With gCRegister

            '資料讀取-----------------------------------------------------------------

            'GetPrivateProfileString("Section", "Key", "預設值", 傳回字串, 傳回長度, "ini檔名")
            'GetPrivateProfileString(strSection, "NoUseStation1", strDefual, strReadData, strBufferSize, strFileName)
            '.boolNoUseStation1 = CBool(strReadData)

            .boolNoUseStation1 = Val(ReadIniString(strSection, "NoUseStation1", strFileName, strDefual))
            .boolNoUseStation2 = Val(ReadIniString(strSection, "NoUseStation2", strFileName, strDefual))
            .boolNoUseStation3 = Val(ReadIniString(strSection, "NoUseStation3", strFileName, strDefual))
            .intRollSpeed = Val(ReadIniString(strSection, "RollSpeed", strFileName, strDefual))

            ckbNoUseStation1.Checked = .boolNoUseStation1 'CBool(strReadData)
            ckbNoUseStation2.Checked = .boolNoUseStation2 'CBool(strReadData)
            ckbNoUseStation3.Checked = .boolNoUseStation3 'CBool(strReadData)

            cboRollSpeed.SelectedIndex = .intRollSpeed

            'GetPrivateProfileString(strSection, "NoUseStation2", strDefual, strReadData, strBufferSize, strFileName)
            'ckbNoUseStation2.Checked = CBool(strReadData)
            '.boolNoUseStation2 = CBool(strReadData)


            'GetPrivateProfileString(strSection, "NoUseStation3", strDefual, strReadData, strBufferSize, strFileName)
            'ckbNoUseStation3.Checked = CBool(strReadData)
            '.boolNoUseStation3 = CBool(strReadData)

            'Dim strReadNumber As String = New String(Chr(0), strBufferSize)
            'GetPrivateProfileString(strSection, "RollSpeed", strDefual, strReadNumber, strBufferSize, strFileName)
            'cboRollSpeed.SelectedIndex = CInt(strReadNumber)
            '.intRollSpeed = CInt(strReadNumber)
            .intStation2TopLiftSpeed = Val(ReadIniString(strSection, "Station2TopLiftSpeed", strFileName, strDefual))
            cboStation2TopLiftSpeed.SelectedIndex = .intStation2TopLiftSpeed

            'GetPrivateProfileString(strSection, "Station2TopLiftSpeed", strDefual, strReadNumber, strBufferSize, strFileName)
            'cboStation2TopLiftSpeed.SelectedIndex = CInt(strReadNumber)  'Chuck2 馬達速度設定
            '.intStation2TopLiftSpeed = CInt(strReadNumber)

            .dblStation2TopUpPos = Val(ReadIniString(strSection, "Station2TopLiftUpPos", strFileName, strDefual))
            txtStation2TopLiftUpPos.Text = .dblStation2TopUpPos
            'GetPrivateProfileString(strSection, "Station2TopLiftUpPos", strDefual, strReadNumber, strBufferSize, strFileName)
            'txtStation2TopLiftUpPos.Text = strReadNumber.ToString()
            '.dblStation2TopUpPos = strReadNumber.ToString()

            .dblStation2TopDownPos = Val(ReadIniString(strSection, "Station2TopLiftDownPos", strFileName, strDefual))
            txtStation2TopLiftDownPos.Text = .dblStation2TopDownPos
            'GetPrivateProfileString(strSection, "Station2TopLiftDownPos", strDefual, strReadNumber, strBufferSize, strFileName)
            'txtStation2TopLiftDownPos.Text = strReadNumber.ToString()
            '.dblStation2TopDownPos = strReadNumber.ToString()

            .dblStation1HeaterSet = Val(ReadIniString(strSection, "Station1HeaterSet", strFileName, strDefual))
            txtStation1heaterSet.Text = .dblStation1HeaterSet
            'GetPrivateProfileString(strSection, "Station1HeaterSet", strDefual, strReadNumber, strBufferSize, strFileName)
            'txtStation1heaterSet.Text = strReadNumber.ToString()
            '.dblStation1HeaterSet = CDbl(strReadNumber)

            .dblStation2HeaterSet = Val(ReadIniString(strSection, "Station2HeaterSet", strFileName, strDefual))
            txtStation2heaterSet.Text = .dblStation2HeaterSet
            'GetPrivateProfileString(strSection, "Station2HeaterSet", strDefual, strReadNumber, strBufferSize, strFileName)
            'txtStation2heaterSet.Text = strReadNumber.ToString()
            '.dblStation2HeaterSet = CDbl(strReadNumber)
            .dblStation3HeaterSet = Val(ReadIniString(strSection, "Station3HeaterSet", strFileName, strDefual))
            txtStation3heaterSet.Text = .dblStation3HeaterSet
            'GetPrivateProfileString(strSection, "Station3HeaterSet", strDefual, strReadNumber, strBufferSize, strFileName)
            'txtStation3heaterSet.Text = strReadNumber.ToString()
            '.dblStation3HeaterSet = CDbl(strReadNumber)
            ConveyorA.RunMode = Val(ReadIniString(strSection, "ConveyorA.RunMode", strFileName, strDefual))
            'GetPrivateProfileString(strSection, "ConveyorA.RunMode", strDefual, strReadNumber, strBufferSize, strFileName)
            'ConveyorA.RunMode = CInt(strReadNumber)
            If ConveyorA.RunMode >= 0 Then
                cmbRunMode.SelectedIndex = ConveyorA.RunMode
            End If

            '程式內部用-----------------------------------------------------------------

            ConveyorA.DisableStation(1, .boolNoUseStation1)
            ConveyorA.DisableStation(2, .boolNoUseStation2)
            ConveyorA.DisableStation(3, .boolNoUseStation3)

            Select Case .intRollSpeed             '轉輪速度
                Case 0
                    ConveyorA.SetRollSpeedType(SpeedType.Slow)
                Case 1
                    ConveyorA.SetRollSpeedType(SpeedType.Medium)
                Case 2
                    ConveyorA.SetRollSpeedType(SpeedType.Fast)
            End Select

            Select Case .intStation2TopLiftSpeed  'Chuck 2 馬達速度設定
                Case 0
                    ConveyorA.SetMotorSpeedType(SpeedType.Slow)
                Case 1
                    ConveyorA.SetMotorSpeedType(SpeedType.Medium)
                Case 2
                    ConveyorA.SetMotorSpeedType(SpeedType.Fast)
            End Select

            ConveyorA.SetMotorUpPos(2, .dblStation2TopUpPos)     'Chuck2 馬達上升點位設定
            ConveyorA.SetMotorDownPos(2, .dblStation2TopDownPos) 'Chuck2 馬達下降點位設定

            '設定加熱溫度
            ConveyorA.SetTargetTemperature(1, .dblStation1HeaterSet)
            ConveyorA.SetTargetTemperature(2, .dblStation2HeaterSet)
            ConveyorA.SetTargetTemperature(3, .dblStation3HeaterSet)

        End With
    End Sub


    Private Sub frmConveyor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Timer1.Enabled = False
    End Sub


    Private Sub frmConveyor_Load(sender As Object, e As EventArgs) Handles Me.Load
        Timer1.Enabled = True

        'With cboRollSpeed.Items
        '    .Clear()
        '    .Add(" Slow ")
        '    .Add(" Medium ")
        '    .Add(" Fast ")
        'End With

        ReadConveyorParameter() '讀出Conveyor參數

    End Sub

    Private Sub btnRolling_Click(sender As Object, e As EventArgs) Handles btnRolling.Click, btnRolling2.Click, btnRolling3.Click
        'Dim btn As Button = CType(sender, Button)


        If sender.BackColor = SystemColors.Control Then
            sender.BackColor = Color.Yellow    '更改顏色-Yellow
            If gCMotion.VelMove(enmAxis.Conveyor1, eDirection.Negative) = False Then
                gEqpMsg.AddHistoryAlarm("Error_1037000", "frmConveyor btnRolling", , gMsgHandler.GetMessage(Error_1037000), eMessageLevel.Error)
            End If
            gCMotion.WaitCmdStatus(enmAxis.Conveyor1) 'Soni / 2017.05.10

            If gCMotion.VelMove(enmAxis.Conveyor2, eDirection.Positive) = False Then
                gEqpMsg.AddHistoryAlarm("Error_1038000", "frmConveyor btnRolling", , gMsgHandler.GetMessage(Error_1038000), eMessageLevel.Error)
            End If
            gCMotion.WaitCmdStatus(enmAxis.Conveyor2) 'Soni / 2017.05.10

            sender.Text = "Roll    Stop"              '更改顯示
        Else
            sender.BackColor = SystemColors.Control   '更改顏色-Control
            gCMotion.EmgStop(enmAxis.Conveyor1)
            gCMotion.WaitCmdStatus(enmAxis.Conveyor1) 'Soni / 2017.05.10
            gCMotion.EmgStop(enmAxis.Conveyor2)
            gCMotion.WaitCmdStatus(enmAxis.Conveyor2) 'Soni / 2017.05.10
            sender.Text = "Roll    Start"             '更改顯示
        End If


        'If btnRolling.BackColor = SystemColors.Control Then
        '    btnRolling.BackColor = Color.Yellow
        '    If gCMotion.JogPlus(enmAxis.Conveyor1, enmDirection.Negative) = False Then
        '        gEqpMsg.AddHistoryAlarm("A0104", "frmConveyor btnRolling")
        '    End If
        '    Do
        '        Application.DoEvents()
        '    Loop Until gCMotion.GetCmdStatus(enmAxis.Conveyor1) = CommandStatus.Sucessed


        '    If gCMotion.JogPlus(enmAxis.Conveyor2, enmDirection.Positive) = False Then
        '        gEqpMsg.AddHistoryAlarm("A0104", "frmConveyor btnRolling")
        '    End If
        '    Do
        '        Application.DoEvents()
        '    Loop Until gCMotion.GetCmdStatus(enmAxis.Conveyor2) = CommandStatus.Sucessed
        '    btnRolling.Text = "Roll    Stop"
        'Else
        '    btnRolling.BackColor = SystemColors.Control
        '    gCMotion.EmgStop(enmAxis.Conveyor1)
        '    Do
        '        Application.DoEvents()
        '    Loop Until gCMotion.GetCmdStatus(enmAxis.Conveyor1) = CommandStatus.Sucessed
        '    gCMotion.EmgStop(enmAxis.Conveyor2)
        '    Do
        '        Application.DoEvents()
        '    Loop Until gCMotion.GetCmdStatus(enmAxis.Conveyor2) = CommandStatus.Sucessed
        '    btnRolling.Text = "Roll    Start"
        'End If


    End Sub


    Private Sub btnUp2_Click(sender As Object, e As EventArgs) Handles btnUp2.Click
        Dim index As Integer = CInt(TextBox1.Text)


        If gCMotion.VelMove(index, eDirection.Positive) = False Then
            gEqpMsg.AddHistoryAlarm("Error_1037000", "frmConveyor btnRolling", , gMsgHandler.GetMessage(Error_1037000), eMessageLevel.Error)
        End If
    End Sub

    Private Sub btnMotorStop2_Click(sender As Object, e As EventArgs) Handles btnMotorStop2.Click
        Dim index As Integer = CInt(TextBox1.Text)
        gCMotion.EmgStop(index)
    End Sub

    Private Sub btnDown2_Click(sender As Object, e As EventArgs) Handles btnDown2.Click
        Dim index As Integer = CInt(TextBox1.Text)

        If gCMotion.VelMove(index, eDirection.Negative) = False Then
            gEqpMsg.AddHistoryAlarm("Error_1037000", "frmConveyor btnRolling", , gMsgHandler.GetMessage(Error_1037000), eMessageLevel.Error)
        End If
    End Sub

    Private Sub btnAutoRun_Click(sender As Object, e As EventArgs) Handles btnAutoRun.Click

        genmConveyorStatus = enmRunStatus.Running
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim index As Integer = CInt(TextBox1.Text)

        Label1.Text = gCMotion.GetPositionValue(index)
    End Sub

    Private Sub ckbLoadRecieveTray_CheckedChanged(sender As Object, e As EventArgs) Handles ckbLoadRecieveTray.CheckedChanged
        If ckbLoadRecieveTray.Checked = False Then
            gDOCollection.SetState(enmDO.MachineReadyToRecieve, True)
            ckbLoadRecieveTray.BackColor = SystemColors.Control
        Else
            gDOCollection.SetState(enmDO.MachineReadyToRecieve, False)
            ckbLoadRecieveTray.BackColor = Color.Yellow
        End If

    End Sub

    Private Sub ckbUnLoadBoardAvaiable_CheckedChanged(sender As Object, e As EventArgs) Handles ckbUnLoadBoardAvaiable.CheckedChanged
        If ckbUnLoadBoardAvaiable.Checked = False Then
            gDOCollection.SetState(enmDO.BoardAvailable, True)
            ckbUnLoadBoardAvaiable.BackColor = SystemColors.Control
        Else
            gDOCollection.SetState(enmDO.BoardAvailable, False)
            ckbUnLoadBoardAvaiable.BackColor = Color.Yellow
        End If
    End Sub

    Private Async Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'Dim index As Integer = CInt(TextBox1.Text)
        Dim index As Integer = enmAxis.Station2
        gCMotion.Home(index)
        Await Task.Run(Sub()
                           Do
                               System.Threading.Thread.Sleep(1)
                               If gCMotion.HomeFinish(index) = CommandStatus.Sucessed Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          Button6.BackColor = SystemColors.Control
                                                      End Sub)
                                   End If

                               Else
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          Button6.BackColor = Color.Red
                                                      End Sub)
                                   End If

                               End If
                           Loop Until gCMotion.GetCmdStatus(index) = CommandStatus.Sucessed
                       End Sub)
        
        If gCMotion.HomeFinish(index) = CommandStatus.Sucessed Then
            Button6.BackColor = SystemColors.Control
        Else
            Button6.BackColor = Color.Red
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim index As Integer = CInt(TextBox1.Text)
        Dim dblPos As Double = CDbl(TextBox2.Text)
        gCMotion.AbsMove(index, dblPos)
    End Sub

    Private Sub ckbTrayClamper_CheckedChanged(sender As Object, e As EventArgs) Handles ckbTrayClamper.CheckedChanged
        If ckbTrayClamper.Checked = False Then
            gDOCollection.SetState(enmDO.TrayClamper, False)
            ckbTrayClamper.BackColor = SystemColors.Control
        Else
            gDOCollection.SetState(enmDO.TrayClamper, True)
            ckbTrayClamper.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ckbHoldBack_CheckedChanged(sender As Object, e As EventArgs) Handles ckbHoldBack.CheckedChanged
        If ckbHoldBack.Checked = False Then
            gDOCollection.SetState(enmDO.HoldBack, False)
            ckbHoldBack.BackColor = SystemColors.Control
        Else
            gDOCollection.SetState(enmDO.HoldBack, True)
            ckbHoldBack.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles btnSetTemperature1.Click
        Dim index As Integer = enmAxis.Heater1
        Dim valve As Integer = CInt(nmcS1HeaterValue.Value)
        gCMotion.SetHeaterSV(index, valve)

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles btnSetTemperature3.Click
        Dim index As Integer = enmAxis.Heater3
        Dim valve As Integer = CInt(nmcS3HeaterValue.Value)
        gCMotion.SetHeaterSV(index, valve)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles btnSetTemperature2.Click
        Dim index As Integer = enmAxis.Heater2
        Dim valve As Integer = CInt(nmcS2HeaterValue.Value)
        gCMotion.SetHeaterSV(index, valve)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles btnGetTemperature1.Click
        Dim index As Integer = enmAxis.Heater1
        TextBox6.Text = gCMotion.ReadHeaterPV(index)
    End Sub

    'Private Sub Button12_Click(sender As Object, e As EventArgs) Handles btnStop.Click
    '    genmConveyorStatus = enmRunStatus.Stop
    'End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ConveyorA.Reset()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        ConveyorA.boolTrayRecive = True
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles btnUnload.Click
        ConveyorA.boolUnloadTray = True
    End Sub

    Private Sub TabControl1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles TabControl1.DrawItem
        Dim tabctl As TabControl = DirectCast(sender, TabControl)
        Dim g As Graphics = e.Graphics
        Dim font As Font = tabctl.Font
        Dim brush As New SolidBrush(Color.Black)
        Dim tabTextArea As RectangleF = RectangleF.op_Implicit(tabctl.GetTabRect(e.Index))
        If tabctl.SelectedIndex = e.Index Then
            font = New Font(font, FontStyle.Bold)
            brush = New SolidBrush(Color.LightGray)
        End If
        g.DrawString(tabctl.TabPages(e.Index).Text, font, brush, tabTextArea)
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button17_Click_1(sender As Object, e As EventArgs)

    End Sub




    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles btnMoveFinish.Click
        Dim index As Integer = CInt(TextBox1.Text)
        Label12.Text = "No"

        'Do
        '    Application.DoEvents()
        'Loop Until gCMotion.GetCmdStatus(index) = CommandStatus.Sucessed
        
        gCMotion.WaitCmdStatus(index) 'Soni / 2017.05.10

        Delay(100)

        'Do
        '    Application.DoEvents()
        'Loop Until gCMotion.GetCmdStatus(index) = CommandStatus.Sucessed
         gCMotion.WaitCmdStatus(index) 'Soni / 2017.05.10


        Label12.Text = "Finish"
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnOKExit.Click
        'Sue20170627
        gSyslog.Save("[frmConveyor]" & vbTab & "[btnExit]" & vbTab & "Click")
        Me.Close()
    End Sub
End Class

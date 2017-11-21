Imports ProjectMotion
Imports ProjectCore
Imports System.Windows.Forms

Public Class frmElectricCylinder
    Dim Path As String = System.Windows.Forms.Application.StartupPath & "\system\DCSW-800AQ\Conveyor.ini"
    Dim WriteSysIni As WriteIni.clsWriteIni = New WriteIni.clsWriteIni(Path)

    Dim A_Chuck As Integer = enmAxis.MachineAChuck1
    Dim B_Chuck As Integer = enmAxis.MachineBChuck1

    Dim A_AbsPosition As Double
    Dim B_AbsPosition As Double

    Dim A_MoveDec As Double
    Dim B_MoveDec As Double

    Dim A_HomeFlag As Boolean
    Dim B_HomeFlag As Boolean

    Private Sub frmElectricCylinder_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        timer_GetPosition.Enabled = False
    End Sub

    'Dim A_PosivtiveLimit As Double = gCMotion.AxisParameter(A_Chuck).Limit.PosivtiveLimit
    'Dim A_NegativeLimit As Double = gCMotion.AxisParameter(A_Chuck).Limit.NegativeLimit
    'Dim B_PosivtiveLimit As Double = gCMotion.AxisParameter(B_Chuck).Limit.PosivtiveLimit
    'Dim B_NegativeLimit As Double = gCMotion.AxisParameter(B_Chuck).Limit.NegativeLimit

    Private Sub frmElectricCylinder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        timer_GetPosition.Start()

        tbA_Top.Text = mGlobalPool.Unit.A_ECylinderTopPos.ToString()
        tbA_Bottom.Text = mGlobalPool.Unit.A_ECylinderBottomPos.ToString()
        nudA_TestSpeed.Text = mGlobalPool.Unit.A_ECylinderSpeed
        nudA_Speed.Text = mGlobalPool.Unit.A_ECylinderSpeed

        tbB_Top.Text = mGlobalPool.Unit.B_ECylinderTopPos.ToString()
        tbB_Bottom.Text = mGlobalPool.Unit.B_ECylinderBottomPos.ToString()
        nudB_TestSpeed.Text = mGlobalPool.Unit.B_ECylinderSpeed
        nudB_Speed.Text = mGlobalPool.Unit.B_ECylinderSpeed
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                GroupBox2.Visible = True
            Case Else
                GroupBox2.Visible = False
        End Select
    End Sub

    Private Sub tb_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs) Handles tbA_Position.KeyPress, tbB_Position.KeyPress, tbB_Top.KeyPress, tbB_Bottom.KeyPress, tbA_Top.KeyPress, tbA_Bottom.KeyPress
        If Char.IsDigit(e.KeyChar) Or e.KeyChar = Chr(8) Then '0~9 & backspace
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

#Region "Machine A"
    Private Sub rbtnA_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnA_1.CheckedChanged, rbtnA_1000.CheckedChanged, rbtnA_100.CheckedChanged, rbtnA_10.CheckedChanged
        If (rbtnA_1.Checked) Then
            A_MoveDec = 1
        ElseIf (rbtnA_10.Checked) Then
            A_MoveDec = 10
        ElseIf (rbtnA_100.Checked) Then
            A_MoveDec = 100
        ElseIf (rbtnA_1000.Checked) Then
            A_MoveDec = 1000
        End If
    End Sub

    Private Sub btnA_Home_Click(sender As Object, e As EventArgs) Handles btnA_Home.Click
        'Sue20170627
        gSyslog.Save("[frmElectricCylinder]" & vbTab & "[btnA_Home]" & vbTab & "Click")
        Unit.A_ElectricCylinder(clsUnit.enmDirection.Home)
        A_HomeFlag = True
    End Sub

    Private Sub btnAMove_Click(sender As Object, e As EventArgs) Handles btnA_Move.Click
        'Sue20170627
        gSyslog.Save("[frmElectricCylinder]" & vbTab & "[btnAMove]" & vbTab & "Click")
        A_AbsPosition = Convert.ToDouble(tbA_Position.Text)
        gCMotion.AbsMove(A_Chuck, A_AbsPosition)
    End Sub

    Private Sub btnA_Up_Click(sender As Object, e As EventArgs) Handles btnA_Up.Click
        'Sue20170627
        gSyslog.Save("[frmElectricCylinder]" & vbTab & "[btnA_Up]" & vbTab & "Click")
        'A_AbsPosition = A_AbsPosition + A_MoveDec
        A_AbsPosition = gCMotion.GetPositionValue(A_Chuck) + A_MoveDec
        gCMotion.AbsMove(A_Chuck, A_AbsPosition)
    End Sub

    Private Sub btnA_Down_Click(sender As Object, e As EventArgs) Handles btnA_Down.Click
        'Sue20170627
        gSyslog.Save("[frmElectricCylinder]" & vbTab & "[btnA_Down]" & vbTab & "Click")
        'A_AbsPosition = A_AbsPosition - A_MoveDec
        A_AbsPosition = gCMotion.GetPositionValue(A_Chuck) - A_MoveDec
        gCMotion.AbsMove(A_Chuck, A_AbsPosition)
    End Sub

    Private Sub btnA_Save_Click(sender As Object, e As EventArgs) Handles btnA_Save.Click
        'Sue20170627
        gSyslog.Save("[frmElectricCylinder]" & vbTab & "[btnA_Save]" & vbTab & "Click")
        Try
            mGlobalPool.Unit.A_ECylinderTopPos = Convert.ToInt32(tbA_Top.Text)
            mGlobalPool.Unit.A_ECylinderBottomPos = Convert.ToInt32(tbA_Bottom.Text)
            mGlobalPool.Unit.A_ECylinderSpeed = Convert.ToInt32(nudA_Speed.Text)

            WriteSysIni.IniWriteValue("Electric Cylinder", "A_TopPosition", tbA_Top.Text)
            WriteSysIni.IniWriteValue("Electric Cylinder", "A_BottomPosition", tbA_Bottom.Text)
            WriteSysIni.IniWriteValue("Electric Cylinder", "A_Speed", nudA_TestSpeed.Text)
        Catch ex As Exception
            MsgBox("Typing error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try

    End Sub

    Private Sub btnA_SetSpeed_Click(sender As Object, e As EventArgs) Handles btnA_SetSpeed.Click
        'Sue20170627
        gSyslog.Save("[frmElectricCylinder]" & vbTab & "[btnA_SetSpeed]" & vbTab & "Click")
        gCMotion.SetVelHigh(A_Chuck, Convert.ToDouble(nudA_TestSpeed.Text))
    End Sub

    Private Sub nudA_TestSpeed_ValueChanged(sender As Object, e As EventArgs) Handles nudA_TestSpeed.ValueChanged
        gCMotion.SetVelHigh(A_Chuck, Convert.ToDouble(nudA_TestSpeed.Text))
    End Sub
#End Region

#Region "Machine B"
    Private Sub rbtnB_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnB_1.CheckedChanged, rbtnB_1000.CheckedChanged, rbtnB_100.CheckedChanged, rbtnB_10.CheckedChanged
        If (rbtnB_1.Checked) Then
            B_MoveDec = 1
        ElseIf (rbtnB_10.Checked) Then
            B_MoveDec = 10
        ElseIf (rbtnB_100.Checked) Then
            B_MoveDec = 100
        ElseIf (rbtnB_1000.Checked) Then
            B_MoveDec = 1000
        End If
    End Sub

    Private Sub btnB_Home_Click(sender As Object, e As EventArgs) Handles btnB_Home.Click
        'Sue20170627
        gSyslog.Save("[frmElectricCylinder]" & vbTab & "[btnB_Home]" & vbTab & "Click")
        Unit.B_ElectricCylinder(clsUnit.enmDirection.Home)
        B_HomeFlag = True
    End Sub

    Private Sub btnB_Move_Click(sender As Object, e As EventArgs) Handles btnB_Move.Click
        'Sue20170627
        gSyslog.Save("[frmElectricCylinder]" & vbTab & "[btnB_Move]" & vbTab & "Click")
        B_AbsPosition = Convert.ToDouble(tbB_Position.Text)
        gCMotion.AbsMove(B_Chuck, B_AbsPosition)
    End Sub

    Private Sub btnB_Up_Click(sender As Object, e As EventArgs) Handles btnB_Up.Click
        'Sue20170627
        gSyslog.Save("[frmElectricCylinder]" & vbTab & "[btnB_Up]" & vbTab & "Click")
        'B_AbsPosition = B_AbsPosition + B_MoveDec
        B_AbsPosition = gCMotion.GetPositionValue(B_Chuck) + B_MoveDec
        gCMotion.AbsMove(B_Chuck, B_AbsPosition)
    End Sub

    Private Sub btnB_Down_Click(sender As Object, e As EventArgs) Handles btnB_Down.Click
        'Sue20170627
        gSyslog.Save("[frmElectricCylinder]" & vbTab & "[btnB_Down]" & vbTab & "Click")
        'B_AbsPosition = B_AbsPosition - B_MoveDec
        B_AbsPosition = gCMotion.GetPositionValue(B_Chuck) - B_MoveDec
        gCMotion.AbsMove(B_Chuck, B_AbsPosition)
    End Sub

    Private Sub btnB_Save_Click(sender As Object, e As EventArgs) Handles btnB_Save.Click
        'Sue20170627
        gSyslog.Save("[frmElectricCylinder]" & vbTab & "[btnB_Save]" & vbTab & "Click")
        Try
            mGlobalPool.Unit.B_ECylinderTopPos = Convert.ToInt32(tbB_Top.Text)
            mGlobalPool.Unit.B_ECylinderBottomPos = Convert.ToInt32(tbB_Bottom.Text)
            mGlobalPool.Unit.B_ECylinderSpeed = Convert.ToInt32(nudB_Speed.Text)

            WriteSysIni.IniWriteValue("Electric Cylinder", "B_TopPosition", tbB_Top.Text)
            WriteSysIni.IniWriteValue("Electric Cylinder", "B_BottomPosition", tbB_Bottom.Text)
            WriteSysIni.IniWriteValue("Electric Cylinder", "B_Speed", nudB_TestSpeed.Text)
        Catch ex As Exception
            MsgBox("Typing error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub

    Private Sub btnB_SetSpeed_Click(sender As Object, e As EventArgs) Handles btnB_SetSpeed.Click
        'Sue20170627
        gSyslog.Save("[frmElectricCylinder]" & vbTab & "[btnB_SetSpeed]" & vbTab & "Click")
        gCMotion.SetVelHigh(B_Chuck, Convert.ToDouble(nudB_TestSpeed.Text))
    End Sub

    Private Sub nudB_TestSpeed_ValueChanged(sender As Object, e As EventArgs) Handles nudB_TestSpeed.ValueChanged
        gCMotion.SetVelHigh(B_Chuck, Convert.ToDouble(nudB_TestSpeed.Text))
    End Sub
#End Region

    Private Sub timer_GetPosition_Tick(sender As Object, e As EventArgs) Handles timer_GetPosition.Tick
        Dim A_status As String = ""
        If A_Chuck <> -1 Then
            If (A_HomeFlag) Then
                If gCMotion.CheckMotorStatus(A_Chuck) = Premtek.Base.CommandStatus.Sucessed Then '更新gCMotion.AxisParameter(A_Chuck).MotionIOStatus狀態
                    If gCMotion.AxisParameter(A_Chuck).MotionIOStatus.blnALM = True Then
                        A_status = "Alarm"
                        lblA_Status.BackColor = Drawing.Color.Red
                    Else
                        If gCMotion.MotionDone(A_Chuck) = CommandStatus.Sucessed Or gCMotion.HomeFinish(A_Chuck) = CommandStatus.Sucessed Then
                            A_status = "Finish"
                            lblA_Status.BackColor = Drawing.Color.Green

                            btnA_Move.Enabled = True
                            btnA_Home.Enabled = True
                            btnA_Up.Enabled = True
                            btnA_Down.Enabled = True
                            tbA_Position.Enabled = True
                            nudA_TestSpeed.Enabled = True
                            btnA_SetSpeed.Enabled = True
                        Else
                            A_status = "Running"
                            lblA_Status.BackColor = Drawing.Color.Blue

                            btnA_Move.Enabled = False
                            btnA_Home.Enabled = False
                            btnA_Up.Enabled = False
                            btnA_Down.Enabled = False
                            tbA_Position.Enabled = False
                            nudA_TestSpeed.Enabled = False
                            btnA_SetSpeed.Enabled = False
                        End If
                    End If
                End If
            End If
        Else
            A_status = "A_Chuck index is null"
        End If
        lblA_Status.Text = A_status & ": " & gCMotion.GetPositionValue(A_Chuck) & " (mm)"

        Dim B_status As String = ""
        If B_Chuck <> -1 Then
            If (B_HomeFlag) Then
                If gCMotion.CheckMotorStatus(B_Chuck) = Premtek.Base.CommandStatus.Sucessed Then '更新gCMotion.AxisParameter(A_Chuck).MotionIOStatus狀態
                    If gCMotion.AxisParameter(B_Chuck).MotionIOStatus.blnALM = True Then
                        B_status = "Alarm"
                        lblB_Status.BackColor = Drawing.Color.Red
                    Else
                        If gCMotion.MotionDone(B_Chuck) = CommandStatus.Sucessed Or gCMotion.HomeFinish(B_Chuck) = CommandStatus.Sucessed Then
                            B_status = "Finish"
                            lblB_Status.BackColor = Drawing.Color.Green

                            btnB_Move.Enabled = True
                            btnB_Home.Enabled = True
                            btnB_Up.Enabled = True
                            btnB_Down.Enabled = True
                            tbB_Position.Enabled = True
                            nudB_TestSpeed.Enabled = True
                            btnB_SetSpeed.Enabled = True
                        Else
                            B_status = "Running"
                            lblB_Status.BackColor = Drawing.Color.Blue

                            btnB_Move.Enabled = False
                            btnB_Home.Enabled = False
                            btnB_Up.Enabled = False
                            btnB_Down.Enabled = False
                            tbB_Position.Enabled = False
                            nudB_TestSpeed.Enabled = False
                            btnB_SetSpeed.Enabled = False
                        End If
                    End If
                End If
            End If
        Else
            B_status = "B_Chuck index is null"
        End If
        lblB_Status.Text = B_status & ": " & gCMotion.GetPositionValue(B_Chuck) & " (mm)"

        'Application.DoEvents()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Sue20170627
        gSyslog.Save("[frmElectricCylinder]" & vbTab & "[btnExit]" & vbTab & "Click")
        Me.Close()
    End Sub
End Class

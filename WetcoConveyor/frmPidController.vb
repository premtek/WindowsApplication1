Imports ProjectIO
Imports ProjectCore

Public Class frmPidController

    Private Sub frmPidController_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        gboxA1.Enabled = Unit.TempController.A1_PidController.IsOpen
        gboxA2.Enabled = Unit.TempController.A2_PidController.IsOpen
        gboxA3.Enabled = Unit.TempController.A3_PidController.IsOpen
        gboxA4.Enabled = Unit.TempController.A4_PidController.IsOpen
        gboxA5.Enabled = Unit.TempController.A5_PidController.IsOpen
        gboxA6.Enabled = Unit.TempController.A6_PidController.IsOpen

        gboxB1.Enabled = Unit.TempController.B1_PidController.IsOpen
        gboxB2.Enabled = Unit.TempController.B2_PidController.IsOpen
        gboxB3.Enabled = Unit.TempController.B3_PidController.IsOpen
        gboxB4.Enabled = Unit.TempController.B4_PidController.IsOpen
        gboxB5.Enabled = Unit.TempController.B5_PidController.IsOpen
        gboxB6.Enabled = Unit.TempController.B6_PidController.IsOpen

        gboxMachineA.Text = "Conveyor 1"
        gboxMachineB.Text = "Conveyor 2"
        '[說明]:關閉目前不使用的項目
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                gboxMachineA.Text = "Machine A"
                gboxMachineB.Text = "Machine B"
                gboxMachineB.Visible = True
            Case enmMachineType.DCS_500AD
                gboxMachineA.Text = "Machine A"
                gboxMachineB.Visible = False
            Case enmMachineType.DCS_350A
                gboxMachineB.Visible = False
                gboxMachineA.Size = New Drawing.Size(290, 205)
                gboxA1.Location = New Drawing.Point(19, 37)
                gboxMachineB.Size = New Drawing.Size(290, 205)
                gboxMachineB.Location = New Drawing.Point(306, 13)
                gboxB1.Location = New Drawing.Point(19, 37)
                Me.Size = New Drawing.Size(624, 366)

            Case Else
                gboxMachineB.Visible = False
                gboxA1.Location = New Drawing.Point(19, 37)
                gboxMachineA.Size = New Drawing.Size(290, 205)
                Me.Size = New Drawing.Size(342, 375)

        End Select
    End Sub

    Private Sub tbValueA6_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs) Handles tbValueB6.KeyPress, tbValueB5.KeyPress, tbValueB4.KeyPress, tbValueB3.KeyPress, tbValueB2.KeyPress, tbValueB1.KeyPress, tbValueA6.KeyPress, tbValueA5.KeyPress, tbValueA4.KeyPress, tbValueA3.KeyPress, tbValueA2.KeyPress, tbValueA1.KeyPress
        If (ChrW(Asc(".")) <= e.KeyChar And e.KeyChar <= ChrW(Asc("9"))) Or e.KeyChar = ChrW(8) Then '0~9 & backspace
            Exit Sub
        Else
            e.KeyChar = vbNullChar
        End If
    End Sub

    Private Sub btnSetA1_Click(sender As Object, e As EventArgs) Handles btnSetA1.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnSetA1]" & vbTab & "Click")
        If (IsNumeric(tbValueA1.Text)) Then
            Unit.TempController.SetSV(clsTemperatureController.enmPidController.A1, Convert.ToDouble(tbValueA1.Text))
        End If
    End Sub

    Private Sub btnSetA2_Click(sender As Object, e As EventArgs) Handles btnSetA2.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnSetA2]" & vbTab & "Click")
        If (IsNumeric(tbValueA2.Text)) Then
            Unit.TempController.SetSV(clsTemperatureController.enmPidController.A2, Convert.ToDouble(tbValueA2.Text))
        End If
    End Sub

    Private Sub btnSetA3_Click(sender As Object, e As EventArgs) Handles btnSetA3.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnSetA3]" & vbTab & "Click")
        If (IsNumeric(tbValueA3.Text)) Then
            Unit.TempController.SetSV(clsTemperatureController.enmPidController.A3, Convert.ToDouble(tbValueA3.Text))
        End If
    End Sub

    Private Sub btnSetA4_Click(sender As Object, e As EventArgs) Handles btnSetA4.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnSetA4]" & vbTab & "Click")
        If (IsNumeric(tbValueA4.Text)) Then
            Unit.TempController.SetSV(clsTemperatureController.enmPidController.A4, Convert.ToDouble(tbValueA4.Text))
        End If
    End Sub

    Private Sub btnSetA5_Click(sender As Object, e As EventArgs) Handles btnSetA5.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnSetA5]" & vbTab & "Click")
        If (IsNumeric(tbValueA5.Text)) Then
            Unit.TempController.SetSV(clsTemperatureController.enmPidController.A5, Convert.ToDouble(tbValueA5.Text))
        End If
    End Sub

    Private Sub btnSetA6_Click(sender As Object, e As EventArgs) Handles btnSetA6.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnSetA6]" & vbTab & "Click")
        If (IsNumeric(tbValueA6.Text)) Then
            Unit.TempController.SetSV(clsTemperatureController.enmPidController.A6, Convert.ToDouble(tbValueA6.Text))
        End If
    End Sub

    Private Sub btnSetB1_Click(sender As Object, e As EventArgs) Handles btnSetB1.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnSetB1]" & vbTab & "Click")
        If (IsNumeric(tbValueB1.Text)) Then
            Unit.TempController.SetSV(clsTemperatureController.enmPidController.B1, Convert.ToDouble(tbValueB1.Text))
        End If
    End Sub

    Private Sub btnSetB2_Click(sender As Object, e As EventArgs) Handles btnSetB2.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnSetB2]" & vbTab & "Click")
        If (IsNumeric(tbValueB2.Text)) Then
            Unit.TempController.SetSV(clsTemperatureController.enmPidController.B2, Convert.ToDouble(tbValueB2.Text))
        End If
    End Sub

    Private Sub btnSetB3_Click(sender As Object, e As EventArgs) Handles btnSetB3.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnSetB3]" & vbTab & "Click")
        If (IsNumeric(tbValueB3.Text)) Then
            Unit.TempController.SetSV(clsTemperatureController.enmPidController.B3, Convert.ToDouble(tbValueB3.Text))
        End If
    End Sub

    Private Sub btnSetB4_Click(sender As Object, e As EventArgs) Handles btnSetB4.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnSetB4]" & vbTab & "Click")
        If (IsNumeric(tbValueB4.Text)) Then
            Unit.TempController.SetSV(clsTemperatureController.enmPidController.B4, Convert.ToDouble(tbValueB4.Text))
        End If
    End Sub

    Private Sub btnSetB5_Click(sender As Object, e As EventArgs) Handles btnSetB5.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnSetB5]" & vbTab & "Click")
        If (IsNumeric(tbValueB5.Text)) Then
            Unit.TempController.SetSV(clsTemperatureController.enmPidController.B5, Convert.ToDouble(tbValueB5.Text))
        End If
    End Sub

    Private Sub btnSetB6_Click(sender As Object, e As EventArgs) Handles btnSetB6.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnSetB6]" & vbTab & "Click")
        If (IsNumeric(tbValueB6.Text)) Then
            Unit.TempController.SetSV(clsTemperatureController.enmPidController.B6, Convert.ToDouble(tbValueB6.Text))
        End If
    End Sub

    Private Sub btnOpenA_Click(sender As Object, e As EventArgs) Handles btnOpenA.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenA]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn1, True)
        gDOCollection.SetState(enmDO.HeaterOn2, True)
        gDOCollection.SetState(enmDO.HeaterOn3, True)
        gDOCollection.SetState(enmDO.HeaterOn4, True)
        gDOCollection.SetState(enmDO.HeaterOn5, True)
        gDOCollection.SetState(enmDO.HeaterOn6, True)
    End Sub

    Private Sub btnCloseA_Click(sender As Object, e As EventArgs) Handles btnCloseA.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseA]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn1, False)
        gDOCollection.SetState(enmDO.HeaterOn2, False)
        gDOCollection.SetState(enmDO.HeaterOn3, False)
        gDOCollection.SetState(enmDO.HeaterOn4, False)
        gDOCollection.SetState(enmDO.HeaterOn5, False)
        gDOCollection.SetState(enmDO.HeaterOn6, False)
    End Sub

    Private Sub btnOpenB_Click(sender As Object, e As EventArgs) Handles btnOpenB.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenB]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn7, True)
        gDOCollection.SetState(enmDO.HeaterOn8, True)
        gDOCollection.SetState(enmDO.HeaterOn9, True)
        gDOCollection.SetState(enmDO.HeaterOn10, True)
        gDOCollection.SetState(enmDO.HeaterOn11, True)
        gDOCollection.SetState(enmDO.HeaterOn12, True)
    End Sub

    Private Sub btnCloseB_Click(sender As Object, e As EventArgs) Handles btnCloseB.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseB]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn7, False)
        gDOCollection.SetState(enmDO.HeaterOn8, False)
        gDOCollection.SetState(enmDO.HeaterOn9, False)
        gDOCollection.SetState(enmDO.HeaterOn10, False)
        gDOCollection.SetState(enmDO.HeaterOn11, False)
        gDOCollection.SetState(enmDO.HeaterOn12, False)
    End Sub

    Private Sub btnOpenA1_Click(sender As Object, e As EventArgs) Handles btnOpenA1.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenA1]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn1, True)
    End Sub

    Private Sub btnOpenA2_Click(sender As Object, e As EventArgs) Handles btnOpenA2.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenA2]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn2, True)
    End Sub

    Private Sub btnOpenA3_Click(sender As Object, e As EventArgs) Handles btnOpenA3.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenA3]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn3, True)
    End Sub

    Private Sub btnOpenA4_Click(sender As Object, e As EventArgs) Handles btnOpenA4.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenA4]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn4, True)
    End Sub

    Private Sub btnOpenA5_Click(sender As Object, e As EventArgs) Handles btnOpenA5.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenA5]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn5, True)
    End Sub

    Private Sub btnOpenA6_Click(sender As Object, e As EventArgs) Handles btnOpenA6.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenA6]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn6, True)
    End Sub

    Private Sub btnCloseA1_Click(sender As Object, e As EventArgs) Handles btnCloseA1.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseA1]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn1, False)
    End Sub

    Private Sub btnCloseA2_Click(sender As Object, e As EventArgs) Handles btnCloseA2.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseA2]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn2, False)
    End Sub

    Private Sub btnCloseA3_Click(sender As Object, e As EventArgs) Handles btnCloseA3.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseA3]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn3, False)
    End Sub

    Private Sub btnCloseA4_Click(sender As Object, e As EventArgs) Handles btnCloseA4.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseA4]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn4, False)
    End Sub

    Private Sub btnCloseA5_Click(sender As Object, e As EventArgs) Handles btnCloseA5.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseA5]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn5, False)
    End Sub

    Private Sub btnCloseA6_Click(sender As Object, e As EventArgs) Handles btnCloseA6.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseA6]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn6, False)
    End Sub

    Private Sub btnOpenB1_Click(sender As Object, e As EventArgs) Handles btnOpenB1.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenB1]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn7, True)
    End Sub

    Private Sub btnOpenB2_Click(sender As Object, e As EventArgs) Handles btnOpenB2.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenB2]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn8, True)
    End Sub

    Private Sub btnOpenB3_Click(sender As Object, e As EventArgs) Handles btnOpenB3.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenB3]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn9, True)
    End Sub

    Private Sub btnOpenB4_Click(sender As Object, e As EventArgs) Handles btnOpenB4.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenB4]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn10, True)
    End Sub

    Private Sub btnOpenB5_Click(sender As Object, e As EventArgs) Handles btnOpenB5.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenB5]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn11, True)
    End Sub

    Private Sub btnOpenB6_Click(sender As Object, e As EventArgs) Handles btnOpenB6.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnOpenB6]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn12, True)
    End Sub

    Private Sub btnCloseB1_Click(sender As Object, e As EventArgs) Handles btnCloseB1.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseB1]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn7, False)
    End Sub

    Private Sub btnCloseB2_Click(sender As Object, e As EventArgs) Handles btnCloseB2.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseB2]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn8, False)
    End Sub

    Private Sub btnCloseB3_Click(sender As Object, e As EventArgs) Handles btnCloseB3.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseB3]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn9, False)
    End Sub

    Private Sub btnCloseB4_Click(sender As Object, e As EventArgs) Handles btnCloseB4.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseB4]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn10, False)
    End Sub

    Private Sub btnCloseB5_Click(sender As Object, e As EventArgs) Handles btnCloseB5.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseB5]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn11, False)
    End Sub

    Private Sub btnCloseB6_Click(sender As Object, e As EventArgs) Handles btnCloseB6.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnCloseB6]" & vbTab & "Click")
        gDOCollection.SetState(enmDO.HeaterOn12, False)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Sue20170627
        gSyslog.Save("[frmPidController]" & vbTab & "[btnExi]" & vbTab & "Click")
        Me.Close()
    End Sub
End Class
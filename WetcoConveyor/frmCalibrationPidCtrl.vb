Imports ProjectCore

Public Class frmCalibrationPidCtrl
    Dim ErrorMessage As String

    Private Sub frmCalibrationPidCtrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (Unit.TempController.A1_PidController.IsOpen) Then
            gboxA1.Enabled = True
            tbTeachValueA1.Text = (Unit.TempController.A1_PidController.PVOS / 10).ToString()
        End If

        If (Unit.TempController.A2_PidController.IsOpen) Then
            gboxA2.Enabled = True
            tbTeachValueA2.Text = (Unit.TempController.A2_PidController.PVOS / 10).ToString()
        End If

        If (Unit.TempController.A3_PidController.IsOpen) Then
            gboxA3.Enabled = True
            tbTeachValueA3.Text = (Unit.TempController.A3_PidController.PVOS / 10).ToString()
        End If

        If (Unit.TempController.A4_PidController.IsOpen) Then
            gboxA4.Enabled = True
            tbTeachValueA4.Text = (Unit.TempController.A4_PidController.PVOS / 10).ToString()
        End If

        If (Unit.TempController.A5_PidController.IsOpen) Then
            gboxA5.Enabled = True
            tbTeachValueA5.Text = (Unit.TempController.A5_PidController.PVOS / 10).ToString()
        End If

        If (Unit.TempController.A6_PidController.IsOpen) Then
            gboxA6.Enabled = True
            tbTeachValueA6.Text = (Unit.TempController.A6_PidController.PVOS / 10).ToString()
        End If

        If (Unit.TempController.B1_PidController.IsOpen) Then
            gboxB1.Enabled = True
            tbTeachValueB1.Text = (Unit.TempController.B1_PidController.PVOS / 10).ToString()
        End If

        If (Unit.TempController.B2_PidController.IsOpen) Then
            gboxB2.Enabled = True
            tbTeachValueB2.Text = (Unit.TempController.B2_PidController.PVOS / 10).ToString()
        End If

        If (Unit.TempController.B3_PidController.IsOpen) Then
            gboxB3.Enabled = True
            tbTeachValueB3.Text = (Unit.TempController.B3_PidController.PVOS / 10).ToString()
        End If

        If (Unit.TempController.B4_PidController.IsOpen) Then
            gboxB4.Enabled = True
            tbTeachValueB4.Text = (Unit.TempController.B4_PidController.PVOS / 10).ToString()
        End If

        If (Unit.TempController.B5_PidController.IsOpen) Then
            gboxB5.Enabled = True
            tbTeachValueB5.Text = (Unit.TempController.B5_PidController.PVOS / 10).ToString()
        End If

        If (Unit.TempController.B6_PidController.IsOpen) Then
            gboxB6.Enabled = True
            tbTeachValueB6.Text = (Unit.TempController.B6_PidController.PVOS / 10).ToString()
        End If

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

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnExit]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub tbTeachValue_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs) Handles tbTeachValueA1.KeyPress, tbTeachValueB6.KeyPress, tbTeachValueB5.KeyPress, tbTeachValueB4.KeyPress, tbTeachValueB3.KeyPress, tbTeachValueB2.KeyPress, tbTeachValueB1.KeyPress, tbTeachValueA6.KeyPress, tbTeachValueA5.KeyPress, tbTeachValueA4.KeyPress, tbTeachValueA3.KeyPress, tbTeachValueA2.KeyPress
        If (ChrW(Asc(".")) <= e.KeyChar And e.KeyChar <= ChrW(Asc("9"))) Or e.KeyChar = ChrW(8) Or e.KeyChar = ChrW(Asc("-")) Then '0~9 & backspace
            Exit Sub
        Else
            e.KeyChar = vbNullChar
        End If
    End Sub

#Region "Machine A"
#Region "溫度復歸"
    Private Sub btnInitialTempA1_Click(sender As Object, e As EventArgs) Handles btnInitialTempA1.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnInitialTempA1]" & vbTab & "Click")
        If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A1, 0)) Then
            Unit.TempController.A1_PidController.PVOS = 0
            btnTeachTempA1.Enabled = True
        Else
            ErrorMessage = Unit.TempController.ErrorMessage
        End If
    End Sub

    Private Sub btnInitialTempA2_Click(sender As Object, e As EventArgs) Handles btnInitialTempA2.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnInitialTempA2]" & vbTab & "Click")
        If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A2, 0)) Then
            Unit.TempController.A2_PidController.PVOS = 0
            btnTeachTempA2.Enabled = True
        Else
            ErrorMessage = Unit.TempController.ErrorMessage
        End If
    End Sub

    Private Sub btnInitialTempA3_Click(sender As Object, e As EventArgs) Handles btnInitialTempA3.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnInitialTempA3]" & vbTab & "Click")
        If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A3, 0)) Then
            Unit.TempController.A3_PidController.PVOS = 0
            btnTeachTempA3.Enabled = True
        Else
            ErrorMessage = Unit.TempController.ErrorMessage
        End If
    End Sub

    Private Sub btnInitialTempA4_Click(sender As Object, e As EventArgs) Handles btnInitialTempA4.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnInitialTempA4]" & vbTab & "Click")
        If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A4, 0)) Then
            Unit.TempController.A4_PidController.PVOS = 0
            btnTeachTempA4.Enabled = True
        Else
            ErrorMessage = Unit.TempController.ErrorMessage
        End If
    End Sub

    Private Sub btnInitialTempA5_Click(sender As Object, e As EventArgs) Handles btnInitialTempA5.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnInitialTempA5]" & vbTab & "Click")
        If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A5, 0)) Then
            Unit.TempController.A5_PidController.PVOS = 0
            btnTeachTempA5.Enabled = True
        Else
            ErrorMessage = Unit.TempController.ErrorMessage
        End If
    End Sub

    Private Sub btnInitialTempA6_Click(sender As Object, e As EventArgs) Handles btnInitialTempA6.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnInitialTempA6]" & vbTab & "Click")
        If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A6, 0)) Then
            Unit.TempController.A6_PidController.PVOS = 0
            btnTeachTempA6.Enabled = True
        Else
            ErrorMessage = Unit.TempController.ErrorMessage
        End If
    End Sub
#End Region

#Region "溫度校正"
    Private Sub btnTeachTempA1_Click(sender As Object, e As EventArgs) Handles btnTeachTempA1.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnTeachTempA1]" & vbTab & "Click")
        btnTeachTempA1.Enabled = False
        If (IsNumeric(tbTeachValueA1.Text)) Then
            Dim OS As Double = Convert.ToDouble(tbTeachValueA1.Text)
            Dim value As Short = Math.Round(OS, 1, MidpointRounding.AwayFromZero)
            If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A1, value)) Then
                Unit.TempController.A1_PidController.PVOS = value
            End If
        End If
    End Sub

    Private Sub btnTeachTempA2_Click(sender As Object, e As EventArgs) Handles btnTeachTempA2.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnTeachTempA2]" & vbTab & "Click")
        btnTeachTempA2.Enabled = False
        If (IsNumeric(tbTeachValueA2.Text)) Then
            Dim OS As Double = Convert.ToDouble(tbTeachValueA2.Text)
            Dim value As Short = Math.Round(OS, 1, MidpointRounding.AwayFromZero)
            If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A2, value)) Then
                Unit.TempController.A2_PidController.PVOS = value
            End If
        End If
    End Sub

    Private Sub btnTeachTempA3_Click(sender As Object, e As EventArgs) Handles btnTeachTempA3.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnTeachTempA3]" & vbTab & "Click")
        btnTeachTempA3.Enabled = False
        If (IsNumeric(tbTeachValueA3.Text)) Then
            Dim OS As Double = Convert.ToDouble(tbTeachValueA3.Text)
            Dim value As Short = Math.Round(OS, 1, MidpointRounding.AwayFromZero)
            If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A3, value)) Then
                Unit.TempController.A3_PidController.PVOS = value
            End If
        End If
    End Sub

    Private Sub btnTeachTempA4_Click(sender As Object, e As EventArgs) Handles btnTeachTempA4.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnTeachTempA4]" & vbTab & "Click")
        btnTeachTempA4.Enabled = False
        If (IsNumeric(tbTeachValueA4.Text)) Then
            Dim OS As Double = Convert.ToDouble(tbTeachValueA4.Text)
            Dim value As Short = Math.Round(OS, 1, MidpointRounding.AwayFromZero)
            If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A4, value)) Then
                Unit.TempController.A4_PidController.PVOS = value
            End If
        End If
    End Sub

    Private Sub btnTeachTempA5_Click(sender As Object, e As EventArgs) Handles btnTeachTempA5.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnTeachTempA5]" & vbTab & "Click")
        btnTeachTempA5.Enabled = False
        If (IsNumeric(tbTeachValueA5.Text)) Then
            Dim OS As Double = Convert.ToDouble(tbTeachValueA5.Text)
            Dim value As Short = Math.Round(OS, 1, MidpointRounding.AwayFromZero)
            If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A5, value)) Then
                Unit.TempController.A5_PidController.PVOS = value
            End If
        End If
    End Sub

    Private Sub btnTeachTempA6_Click(sender As Object, e As EventArgs) Handles btnTeachTempA6.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnTeachTempA6]" & vbTab & "Click")
        btnTeachTempA6.Enabled = False
        If (IsNumeric(tbTeachValueA6.Text)) Then
            Dim OS As Double = Convert.ToDouble(tbTeachValueA6.Text)
            Dim value As Short = Math.Round(OS, 1, MidpointRounding.AwayFromZero)
            If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.A6, value)) Then
                Unit.TempController.A6_PidController.PVOS = value
            End If
        End If
    End Sub
#End Region
#End Region

#Region "Machine B"
#Region "溫度復歸"
    Private Sub btnInitialTempB1_Click(sender As Object, e As EventArgs) Handles btnInitialTempB1.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnInitialTempB1]" & vbTab & "Click")
        If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B1, 0)) Then
            Unit.TempController.B1_PidController.PVOS = 0
            btnTeachTempB1.Enabled = True
        Else
            ErrorMessage = Unit.TempController.ErrorMessage
        End If
    End Sub

    Private Sub btnInitialTempB2_Click(sender As Object, e As EventArgs) Handles btnInitialTempB2.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnInitialTempB2]" & vbTab & "Click")
        If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B2, 0)) Then
            Unit.TempController.B2_PidController.PVOS = 0
            btnTeachTempB2.Enabled = True
        Else
            ErrorMessage = Unit.TempController.ErrorMessage
        End If
    End Sub

    Private Sub btnInitialTempB3_Click(sender As Object, e As EventArgs) Handles btnInitialTempB3.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnInitialTempB3]" & vbTab & "Click")
        If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B3, 0)) Then
            Unit.TempController.B3_PidController.PVOS = 0
            btnTeachTempB3.Enabled = True
        Else
            ErrorMessage = Unit.TempController.ErrorMessage
        End If
    End Sub

    Private Sub btnInitialTempB4_Click(sender As Object, e As EventArgs) Handles btnInitialTempB4.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnInitialTempB4]" & vbTab & "Click")
        If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B4, 0)) Then
            Unit.TempController.B4_PidController.PVOS = 0
            btnTeachTempB4.Enabled = True
        Else
            ErrorMessage = Unit.TempController.ErrorMessage
        End If
    End Sub

    Private Sub btnInitialTempB5_Click(sender As Object, e As EventArgs) Handles btnInitialTempB5.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnInitialTempB5]" & vbTab & "Click")
        If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B5, 0)) Then
            Unit.TempController.B5_PidController.PVOS = 0
            btnTeachTempB5.Enabled = True
        Else
            ErrorMessage = Unit.TempController.ErrorMessage
        End If
    End Sub

    Private Sub btnInitialTempB6_Click(sender As Object, e As EventArgs) Handles btnInitialTempB6.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnInitialTempB6]" & vbTab & "Click")
        If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B6, 0)) Then
            Unit.TempController.B6_PidController.PVOS = 0
            btnTeachTempB6.Enabled = True
        Else
            ErrorMessage = Unit.TempController.ErrorMessage
        End If
    End Sub
#End Region

#Region "溫度校正"
    Private Sub btnTeachTempB1_Click(sender As Object, e As EventArgs) Handles btnTeachTempB1.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnTeachTempB1]" & vbTab & "Click")
        btnTeachTempB1.Enabled = False
        If (IsNumeric(tbTeachValueB1.Text)) Then
            Dim OS As Double = Convert.ToDouble(tbTeachValueB1.Text)
            Dim value As Short = Math.Round(OS, 1, MidpointRounding.AwayFromZero)
            If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B1, value)) Then
                Unit.TempController.B1_PidController.PVOS = value
            End If
        End If
    End Sub

    Private Sub btnTeachTempB2_Click(sender As Object, e As EventArgs) Handles btnTeachTempB2.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnTeachTempB2]" & vbTab & "Click")
        btnTeachTempB2.Enabled = False
        If (IsNumeric(tbTeachValueB2.Text)) Then
            Dim OS As Double = Convert.ToDouble(tbTeachValueB2.Text)
            Dim value As Short = Math.Round(OS, 1, MidpointRounding.AwayFromZero)
            If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B2, value)) Then
                Unit.TempController.B2_PidController.PVOS = value
            End If
        End If
    End Sub

    Private Sub btnTeachTempB3_Click(sender As Object, e As EventArgs) Handles btnTeachTempB3.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnTeachTempB3]" & vbTab & "Click")
        btnTeachTempB3.Enabled = False
        If (IsNumeric(tbTeachValueB3.Text)) Then
            Dim OS As Double = Convert.ToDouble(tbTeachValueB3.Text)
            Dim value As Short = Math.Round(OS, 1, MidpointRounding.AwayFromZero)
            If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B3, value)) Then
                Unit.TempController.B3_PidController.PVOS = value
            End If
        End If
    End Sub

    Private Sub btnTeachTempB4_Click(sender As Object, e As EventArgs) Handles btnTeachTempB4.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnTeachTempB4]" & vbTab & "Click")
        btnTeachTempB4.Enabled = False
        If (IsNumeric(tbTeachValueB4.Text)) Then
            Dim OS As Double = Convert.ToDouble(tbTeachValueB4.Text)
            Dim value As Short = Math.Round(OS, 1, MidpointRounding.AwayFromZero)
            If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B4, value)) Then
                Unit.TempController.B4_PidController.PVOS = value
            End If
        End If
    End Sub

    Private Sub btnTeachTempB5_Click(sender As Object, e As EventArgs) Handles btnTeachTempB5.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnTeachTempB5]" & vbTab & "Click")
        btnTeachTempB5.Enabled = False
        If (IsNumeric(tbTeachValueB5.Text)) Then
            Dim OS As Double = Convert.ToDouble(tbTeachValueB5.Text)
            Dim value As Short = Math.Round(OS, 1, MidpointRounding.AwayFromZero)
            If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B5, value)) Then
                Unit.TempController.B5_PidController.PVOS = value
            End If
        End If
    End Sub

    Private Sub btnTeachTempB6_Click(sender As Object, e As EventArgs) Handles btnTeachTempB6.Click
        'Sue20170627
        gSyslog.Save("[frmCalibrationPidCtrl]" & vbTab & "[btnTeachTempB6]" & vbTab & "Click")
        btnTeachTempB6.Enabled = False
        If (IsNumeric(tbTeachValueB6.Text)) Then
            Dim OS As Double = Convert.ToDouble(tbTeachValueB6.Text)
            Dim value As Short = Math.Round(OS, 1, MidpointRounding.AwayFromZero)
            If (Unit.TempController.SetPVOS(clsTemperatureController.enmPidController.B6, value)) Then
                Unit.TempController.B6_PidController.PVOS = value
            End If
        End If
    End Sub
#End Region
#End Region

End Class
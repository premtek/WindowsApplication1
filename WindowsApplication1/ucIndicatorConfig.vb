Imports ProjectCore
Imports ProjectIO

Public Class ucIndicatorConfig
    Public Subject As sBehavior

    Public Sub RefreshUI()
        GroupBox1.Text = Subject.Name
        Select Case Subject.DOutput
            Case eDOBehavior.On
                btnIndicator.Text = "On"
                nmcPulseTime.Enabled = False
                nmcCycleTime.Enabled = False
            Case eDOBehavior.Off
                btnIndicator.Text = "Off"
                nmcPulseTime.Enabled = False
                nmcCycleTime.Enabled = False
            Case eDOBehavior.Flash
                btnIndicator.Text = "Flash"
                nmcPulseTime.Enabled = True
                nmcCycleTime.Enabled = True
        End Select
        If Subject.FlashPulseTime <> 0 Then
            nmcPulseTime.Value = Subject.FlashPulseTime
        End If
        If Subject.FlashCycleTime <> 0 Then
            nmcCycleTime.Value = Subject.FlashCycleTime
        End If

    End Sub

    Private Sub btnIndicator_Click(sender As Object, e As EventArgs) Handles btnIndicator.Click
        gSyslog.Save("[ucIndicatorConfig]" & vbTab & "[btnIndicator]" & vbTab & "Click")
        Select Case Subject.DOutput
            Case eDOBehavior.On
                Subject.DOutput = eDOBehavior.Off
            Case eDOBehavior.Off
                Subject.DOutput = eDOBehavior.Flash

                If Subject.FlashPulseTime < nmcPulseTime.Minimum Then
                    Subject.FlashPulseTime = nmcPulseTime.Value
                End If
                If Subject.FlashPulseTime > nmcPulseTime.Maximum Then
                    Subject.FlashPulseTime = nmcPulseTime.Value
                End If
                nmcPulseTime.Value = Subject.FlashPulseTime

                If Subject.FlashCycleTime < nmcCycleTime.Minimum Then
                    Subject.FlashCycleTime = nmcCycleTime.Value
                End If
                If Subject.FlashCycleTime > nmcCycleTime.Maximum Then
                    Subject.FlashCycleTime = nmcCycleTime.Value
                End If
                nmcCycleTime.Value = Subject.FlashCycleTime
            Case eDOBehavior.Flash
                Subject.DOutput = eDOBehavior.On
        End Select

        RefreshUI()

    End Sub

    Private Sub nmcPulseTime_ValueChanged(sender As Object, e As EventArgs) Handles nmcPulseTime.ValueChanged
        Subject.FlashPulseTime = nmcPulseTime.Value
    End Sub

    Private Sub nmcCycleTime_ValueChanged(sender As Object, e As EventArgs) Handles nmcCycleTime.ValueChanged
        Subject.FlashCycleTime = nmcCycleTime.Value
    End Sub
End Class

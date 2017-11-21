Imports ProjectRecipe
Imports ProjectCore
Imports ProjectFeedback

Public Class frmFeedback

    Private Sub btnReset_Click(sender As Object, e As EventArgs)

        With gVolumneControl
            Select Case gVolumneControl.ControlType
                Case enmControlType.FlowToAir, enmControlType.VolumeToAir, enmControlType.WeightToAir
                    .Reset(gCRecipe.StageParts(enmStage.No1).SyringePressure(eValveWorkMode.Valve1))

                Case enmControlType.VolumeToPoints, enmControlType.FlowToPoints, enmControlType.WeightToPoints
                    gVolumneControl.Reset(1)

            End Select
            txtMemo.Text = "u(k)" & .output_0.ToString("00.00") & vbCrLf & _
                            "AcceptLevel(%): " & .AcceptLevel & vbCrLf & _
                            "AvgCount:" & .AverageCount & vbCrLf & _
                            "LSL:" & .AvgValueLSL & vbCrLf & _
                            "USL" & .AvgValueUSL & vbCrLf & _
                            "ControlType:" & .ControlType.ToString() & vbCrLf & _
                            "Enabled:" & .Enabled & vbCrLf & _
                            "Chuck:" & .FeekBackPerChuck & vbCrLf & _
                            "Ki:" & .Ki & vbCrLf & _
                            "Kp:" & .Kp & vbCrLf & _
                            "Target:" & .TargetPatternValue & vbCrLf

        End With


    End Sub

    Private Sub frmFeedback_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        With cboControlType.Items
            .Clear()
            .Add("OpenLoop")
            .Add("Flow-Air")
            .Add("Flow-Points")
            .Add("Volume-Air")
            .Add("Volume-Points")
            .Add("Weight-Air")
            .Add("Weight-Points")
        End With
    End Sub

    Private Sub frmFeedback_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        'Sue20170627
        gSyslog.Save("[frmExchange]" & vbTab & "[btnOK]" & vbTab & "Click")
        If gCRecipe.Editable Then
            nmuKp.BackColor = Color.White
            nmuKi.BackColor = Color.White
            nmuTargetVolume.BackColor = Color.White
            nmuDataCount.BackColor = Color.White
            nmuAverageCount.BackColor = Color.White


            gVolumneControl.Kp = nmuKp.Value
            gVolumneControl.Ki = nmuKi.Value
            gVolumneControl.TargetPatternValue = nmuTargetVolume.Value
            gVolumneControl.AcceptLevel = nmuAcceptLevel.Value
            gVolumneControl.AvgValueUSL = gVolumneControl.TargetPatternValue * (1 + gVolumneControl.AcceptLevel * 0.01)
            gVolumneControl.AvgValueLSL = gVolumneControl.TargetPatternValue * (1 - gVolumneControl.AcceptLevel * 0.01)
            gVolumneControl.FeekBackPerChuck = nmuDataCount.Value
            gVolumneControl.AverageCount = nmuAverageCount.Value
            Select Case cboControlType.SelectedIndex
                Case 0 'OpenLoop
                    gVolumneControl.Enabled = False
                    lblTargetValue.Text = "Target Value"
                    nmuTargetVolume.Enabled = False
                    nmuDataCount.Enabled = False
                    nmuAverageCount.Enabled = False
                Case 1 'Flow-Air
                    gVolumneControl.ControlType = enmControlType.FlowToAir
                    gVolumneControl.Enabled = True
                    lblTargetValue.Text = "Target Flow(ul/min)"
                    nmuTargetVolume.Enabled = True
                    nmuDataCount.Enabled = True
                    nmuAverageCount.Enabled = True
                Case 2 'Flow-Points
                    gVolumneControl.ControlType = enmControlType.FlowToPoints
                    gVolumneControl.Enabled = True
                    lblTargetValue.Text = "Target Flow(ul/min)"
                    nmuTargetVolume.Enabled = True
                    nmuDataCount.Enabled = True
                    nmuAverageCount.Enabled = True
                Case 3 'Volume-Air
                    gVolumneControl.ControlType = enmControlType.VolumeToAir
                    gVolumneControl.Enabled = True
                    lblTargetValue.Text = "Target Volume(ul)"
                    nmuTargetVolume.Enabled = True
                    nmuDataCount.Enabled = True
                    nmuAverageCount.Enabled = True
                Case 4 'Volume-Points
                    gVolumneControl.ControlType = enmControlType.VolumeToPoints
                    gVolumneControl.Enabled = True
                    lblTargetValue.Text = "Target Volume(ul)"
                    nmuTargetVolume.Enabled = True
                    nmuDataCount.Enabled = True
                    nmuAverageCount.Enabled = True
                Case 5 'Weight-Air
                    gVolumneControl.ControlType = enmControlType.WeightToAir
                    gVolumneControl.Enabled = True
                    lblTargetValue.Text = "Target Weight(mg)"
                    nmuTargetVolume.Enabled = True
                    nmuDataCount.Enabled = True
                    nmuAverageCount.Enabled = True
                Case 6 'Weight-Points
                    gVolumneControl.ControlType = enmControlType.WeightToPoints
                    gVolumneControl.Enabled = True
                    lblTargetValue.Text = "Target Weight(mg)"
                    nmuTargetVolume.Enabled = True
                    nmuDataCount.Enabled = True
                    nmuAverageCount.Enabled = True
            End Select

            gVolumneControl.Save(gCRecipe.strFileName)

        End If
        'Sue0829
        '待補AlarmCode
        MsgBox("設定完成", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        'Me.Hide()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'Sue20170627
        gSyslog.Save("[frmExchange]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Hide()
    End Sub
End Class
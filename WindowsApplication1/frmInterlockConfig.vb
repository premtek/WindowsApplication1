Imports ProjectCore
Imports ProjectIO

Public Class frmInterlockConfig

    Private Sub frmHardwareInterlockConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
            Case Else
                lblItem7.Visible = False
                lblItem8.Visible = False
                lblItem9.Visible = False
                lblItem10.Visible = False
                lblItem11.Visible = False
                UcHeavyLight7.Visible = False
                UcHeavyLight8.Visible = False
                UcHeavyLight9.Visible = False
                UcHeavyLight10.Visible = False
                UcHeavyLight11.Visible = False
        End Select
        UcHeavyLight1.UpdateStatus(gInterlockCollection.Items(enmHardwardAlarm.EMO).Level)
        UcHeavyLight2.UpdateStatus(gInterlockCollection.Items(enmHardwardAlarm.EMS).Level)
        UcHeavyLight3.UpdateStatus(gInterlockCollection.Items(enmHardwardAlarm.CDA).Level)
        UcHeavyLight4.UpdateStatus(gInterlockCollection.Items(enmHardwardAlarm.DoorClose).Level)
        UcHeavyLight5.UpdateStatus(gInterlockCollection.Items(enmHardwardAlarm.MC_Motor).Level)
        UcHeavyLight6.UpdateStatus(gInterlockCollection.Items(enmHardwardAlarm.MC_Heater).Level)
        UcHeavyLight7.UpdateStatus(gInterlockCollection.Items(enmHardwardAlarm.EMS2).Level)
        UcHeavyLight8.UpdateStatus(gInterlockCollection.Items(enmHardwardAlarm.CDA2).Level)
        UcHeavyLight9.UpdateStatus(gInterlockCollection.Items(enmHardwardAlarm.DoorClose2).Level)
        UcHeavyLight10.UpdateStatus(gInterlockCollection.Items(enmHardwardAlarm.MC_Motor2).Level)
        UcHeavyLight11.UpdateStatus(gInterlockCollection.Items(enmHardwardAlarm.MC_Heater2).Level)

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmInterlockConfig]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        gSyslog.Save("[frmInterlockConfig]" & vbTab & "[btnOK]" & vbTab & "Click")
        gInterlockCollection.Items(enmHardwardAlarm.EMO).Level = UcHeavyLight1.Status
        gInterlockCollection.Items(enmHardwardAlarm.EMS).Level = UcHeavyLight2.Status
        gInterlockCollection.Items(enmHardwardAlarm.CDA).Level = UcHeavyLight3.Status
        gInterlockCollection.Items(enmHardwardAlarm.DoorClose).Level = UcHeavyLight4.Status
        gInterlockCollection.Items(enmHardwardAlarm.MC_Motor).Level = UcHeavyLight5.Status
        gInterlockCollection.Items(enmHardwardAlarm.MC_Heater).Level = UcHeavyLight6.Status
        gInterlockCollection.Items(enmHardwardAlarm.EMS2).Level = UcHeavyLight7.Status
        gInterlockCollection.Items(enmHardwardAlarm.CDA2).Level = UcHeavyLight8.Status
        gInterlockCollection.Items(enmHardwardAlarm.DoorClose2).Level = UcHeavyLight9.Status
        gInterlockCollection.Items(enmHardwardAlarm.MC_Motor2).Level = UcHeavyLight10.Status
        gInterlockCollection.Items(enmHardwardAlarm.MC_Heater2).Level = UcHeavyLight11.Status

        Dim fileName As String = System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigInterlock.ini"
        gInterlockCollection.Save(fileName)
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        'Me.Close()
    End Sub
End Class
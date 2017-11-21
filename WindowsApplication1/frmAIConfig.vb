Imports ProjectIO
Imports ProjectCore

Public Class frmAIConfig
    Public AIIndex As Integer

    Private Sub frmAIConfig_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If AIIndex < 0 Then
            Me.Close()
            Exit Sub
        End If
        If AIIndex > gAICollection.AIParameter.Count - 1 Then
            Me.Close()
            Exit Sub
        End If
        With cmbCardType.Items
            .Clear()
            .AddRange([Enum].GetNames(GetType(enmAICardType)))
        End With

        With gAICollection.AIParameter(AIIndex)
            txtName.Text = .Name
            cmbCardType.SelectedIndex = .CardType
            txtAddress.Text = .Address
            txtUmax.Text = .MaxValue
            txtUmin.Text = .MinValue
            txtVmax.Text = .MaxVoltage
            txtVmin.Text = .MinVoltage
            chkByPass.Checked = .ByPass
            If .UserUnit <> "" Then
                lblUnit.Text = .UserUnit
                lblUnit2.Text = .UserUnit
            End If
            
        End With
        Select Case gUserLevel
            Case enmUserLevel.eSoftwareMaker
                txtName.Enabled = True
                cmbCardType.Enabled = True
                txtAddress.Enabled = True
            Case enmUserLevel.eAdministrator
                txtName.Enabled = False
                cmbCardType.Enabled = False
                txtAddress.Enabled = True
            Case enmUserLevel.eManager
                txtName.Enabled = False
                cmbCardType.Enabled = False
                txtAddress.Enabled = False
            Case enmUserLevel.eEngineer
                txtName.Enabled = False
                cmbCardType.Enabled = False
                txtAddress.Enabled = False
            Case enmUserLevel.eOperator
                txtName.Enabled = False
                cmbCardType.Enabled = False
                txtAddress.Enabled = False
        End Select
    End Sub


    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmAIConfig]" & vbTab & "[btnSave]" & vbTab & "Click")
        If AIIndex < 0 Then
            '儲存失敗
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(gMsgHandler.GetMessage(Warn_3000035), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Me.Close()
            Exit Sub
        End If
        If AIIndex > gAICollection.AIParameter.Count - 1 Then
            '儲存失敗
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(gMsgHandler.GetMessage(Warn_3000035), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Me.Close()
            Exit Sub
        End If

        With gAICollection.AIParameter(AIIndex)
            .Name = txtName.Text
            .Address = txtAddress.Text
            .MaxValue = txtUmax.Text
            .MinValue = txtUmin.Text
            .MaxVoltage = txtVmax.Text
            .MinVoltage = txtVmin.Text
            .ByPass = chkByPass.Checked
        End With

        Dim strFileName As String

        strFileName = System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigAI.ini"
        gAICollection.SaveAI(strFileName)
        '存檔成功
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmAIConfig]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub txtVmax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVmax.KeyPress, txtVmin.KeyPress, txtUmax.KeyPress, txtUmin.KeyPress
        Premtek.Base.CKeyPress.CheckDecimal(sender, e)
    End Sub

End Class
Imports ProjectIO
Imports ProjectCore

Public Class frmAOConfig
    Public AOIndex As Integer

    Private Sub frmAOConfig_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If AOIndex < 0 Then
            Me.Close()
            Exit Sub
        End If
        If AOIndex > gAOCollection.AOParameter.Count - 1 Then
            Me.Close()
            Exit Sub
        End If
        With cmbCardType.Items
            .Clear()
            .AddRange([Enum].GetNames(GetType(enmAOCardType)))
        End With

        With gAOCollection.AOParameter(AOIndex)
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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmAOConfig]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmAOConfig]" & vbTab & "[btnSave]" & vbTab & "Click")
        If AOIndex < 0 Then
            '存檔失敗 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(gMsgHandler.GetMessage(Warn_3000035), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Me.Close()
            Exit Sub
        End If
        If AOIndex > gAOCollection.AOParameter.Count - 1 Then
            '存檔失敗 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(gMsgHandler.GetMessage(Warn_3000035), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Me.Close()
            Exit Sub
        End If

        With gAOCollection.AOParameter(AOIndex)
            .Name = txtName.Text
            .Address = txtAddress.Text
            .MaxValue = txtUmax.Text
            .MinValue = txtUmin.Text
            .MaxVoltage = txtVmax.Text
            .MinVoltage = txtVmin.Text
            .ByPass = chkByPass.Checked
        End With

        Dim strFileName As String

        strFileName = System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigAO.ini"
        gAOCollection.Save(strFileName)
        '存檔完成
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        'Me.Close()
    End Sub

    Private Sub txtVmax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVmax.KeyPress, txtVmin.KeyPress, txtUmax.KeyPress, txtUmin.KeyPress
        Premtek.Base.CKeyPress.CheckDecimal(sender, e)
    End Sub
End Class
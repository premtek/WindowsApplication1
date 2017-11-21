Imports ProjectIO
Imports ProjectCore

Public Class frmDOConfig
    Public DOIndex As Integer

    Private Sub frmDOConfig_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If DOIndex < 0 Then
            Me.Hide()
            Exit Sub
        End If
        If DOIndex > gDOCollection.DOParameter.Count - 1 Then
            Me.Hide()
            Exit Sub
        End If

        With cmbCardType.Items
            .Clear()
            .AddRange([Enum].GetNames(GetType(enmDOCardType)))
        End With

        With gDOCollection.DOParameter(DOIndex)
            lblName.Text = .Name
            cmbCardType.SelectedIndex = .CardType
            txtAddress.Text = .Address
            chkByPass.Checked = .ByPass
            chkToggle.Checked = .Toggle
        End With

        Select Case gUserLevel
            Case enmUserLevel.eSoftwareMaker
                chkByPass.Visible = True
                chkToggle.Visible = True
            Case enmUserLevel.eAdministrator
                chkByPass.Visible = True
                chkToggle.Visible = True
            Case enmUserLevel.eManager
                chkByPass.Visible = True
                chkToggle.Visible = False
            Case enmUserLevel.eEngineer
                chkByPass.Visible = True
                chkToggle.Visible = False
            Case enmUserLevel.eOperator
                chkByPass.Visible = False
                chkToggle.Visible = False
        End Select
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmDOConfig]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmDOConfig]" & vbTab & "[btnSave]" & vbTab & "Click")
        If DOIndex < 0 Then
            '存檔失敗 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(gMsgHandler.GetMessage(Warn_3000035),MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If DOIndex > gDOCollection.DOParameter.Count - 1 Then
            '存檔失敗 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(gMsgHandler.GetMessage(Warn_3000035),MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        Dim strFileName As String
        strFileName = System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigDO.ini" '路徑改向至System下
        With gDOCollection.DOParameter(DOIndex)
            .Address = txtAddress.Text
            .ByPass = chkByPass.Checked
            .Toggle = chkToggle.Checked
        End With

        gDOCollection.Save(strFileName)
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    End Sub

End Class
Imports ProjectIO
Imports ProjectCore

Public Class frmDIConfig
    Public DIIndex As Integer
    Public myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDIConfig))
    Private Sub frmDIConfig_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If DIIndex < 0 Then
            Me.Close()
            Exit Sub
        End If
        If DIIndex >= gDICollection.DIParameter.Count Then
            Me.Close()
            Exit Sub
        End If
        With cmbCardType.Items
            .Clear()
            .AddRange([Enum].GetNames(GetType(enmDICardType)))
        End With

        With gDICollection.DIParameter(DIIndex)
            lblName.Text = .Name
            cmbCardType.SelectedIndex = .CardType
            txtAddress.Text = .Address
            chkByPass.Checked = .ByPass
            chkToggle.Checked = .Toggle
        End With
        Select Case gUserLevel
            Case enmUserLevel.eSoftwareMaker
                lblName.Enabled = True
                cmbCardType.Enabled = True
                txtAddress.Enabled = True
                chkByPass.Visible = True
                chkToggle.Visible = True
            Case enmUserLevel.eAdministrator
                lblName.Enabled = False
                cmbCardType.Enabled = False
                txtAddress.Enabled = True
                chkByPass.Visible = True
                chkToggle.Visible = True
            Case enmUserLevel.eManager
                lblName.Enabled = False
                cmbCardType.Enabled = False
                txtAddress.Enabled = False
                chkByPass.Visible = False
                chkToggle.Visible = False
            Case enmUserLevel.eEngineer
                lblName.Enabled = False
                cmbCardType.Enabled = False
                txtAddress.Enabled = False
                chkByPass.Visible = False
                chkToggle.Visible = False
            Case enmUserLevel.eOperator
                lblName.Enabled = False
                cmbCardType.Enabled = False
                txtAddress.Enabled = False
                chkByPass.Visible = False
                chkToggle.Visible = False
        End Select
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmDIConfig]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmDIConfig]" & vbTab & "[btnSave]" & vbTab & "Click")
        If DIIndex < 0 Then
            '存檔失敗 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(gMsgHandler.GetMessage(Warn_3000035),MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If DIIndex >= gDICollection.DIParameter.Count - 1 Then
            '存檔失敗 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(gMsgHandler.GetMessage(Warn_3000035),MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Dim strFileName As String
        strFileName = System.Windows.Forms.Application.StartupPath & "\System\" & MachineName & "\ConfigDI.ini" '路徑改向至System下
        With gDICollection.DIParameter(DIIndex)
            .Name = lblName.Text
            .Address = txtAddress.Text
            .ByPass = chkByPass.Checked
            .Toggle = chkToggle.Checked
        End With

        gDICollection.Save(strFileName)
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    End Sub

    Private Sub frmDIConfig_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblCardType.Text = myResource.GetString("Label2.Text")
        lblAddress.Text = myResource.GetString("Label3.Text")
        chkByPass.Text = myResource.GetString("chkByPass.Text")
        chkToggle.Text = myResource.GetString("chkToggle.Text")
        'btnCancel.Text = myResource.GetString("btnCancel.Text")
        'btnSave.Text = myResource.GetString("btnSave.Text")
    End Sub
End Class
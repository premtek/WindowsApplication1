Imports ProjectCore
Imports ProjectTriggerBoard

Public Class frmHelp

    Private Sub frmHelp_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        lblProgramVersion.Text += Application.ProductVersion
        For i As Integer = 0 To gTriggerBoardVersion.Count - 1
            lblBoardVersion.Text += vbCrLf & gTriggerBoardVersion(i)
        Next
    End Sub

    Private Sub frmHelp_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmHelp]" & vbTab & "[btnOK]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub frmHelp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim fileName As String = Application.StartupPath & "\System\" & MachineName & "\logo.jpg"
        If System.IO.File.Exists(fileName) Then
            Panel1.BackgroundImage = Image.FromFile(fileName)
        End If
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ, enmMachineType.DCS_500AD
                txtDesc.Text = ""

            Case enmMachineType.DCS_F230A
                txtDesc.Text = "Copyright (C) 2017. All Rights Reserved."

            Case Else
                txtDesc.Text = "Copyright (C) 2017 Premtek International Inc. All Rights Reserved."
        End Select

    End Sub
End Class
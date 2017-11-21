Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports ProjectAOI
Imports ProjectIO
Imports ProjectCore

Public Class frmCCDHandshake

    Private Sub btnCcdImageTrigger_Click(sender As Object, e As EventArgs) Handles btnCcdImageTrigger.Click
        gSyslog.Save("[frmCCDHandshake]" & vbTab & "[btnCcdImageTrigger]" & vbTab & "Click")
        gDOCollection.ToggleOutput(enmDO.CcdImageTrigger)
    End Sub

    Private Sub btnCcdBit0_Click(sender As Object, e As EventArgs) Handles btnCcdBit0.Click
        gSyslog.Save("[frmCCDHandshake]" & vbTab & "[btnCcdBit0]" & vbTab & "Click")
        gDOCollection.ToggleOutput(enmDO.CcdBit0)
    End Sub

    Private Sub btnCcdBit1_Click(sender As Object, e As EventArgs) Handles btnCcdBit1.Click
        gSyslog.Save("[frmCCDHandshake]" & vbTab & "[btnCcdBit1]" & vbTab & "Click")
        gDOCollection.ToggleOutput(enmDO.CcdBit1)
    End Sub


    Private Sub tmrCcdHandshake_Tick(sender As Object, e As EventArgs) Handles tmrCcdHandshake.Tick
        '*****************************************mobary+ 2014/03/12************************************
        '[說明]:Update DO
        If gDOCollection.GetState(enmDO.CcdImageTrigger) = True Then
            btnCcdImageTrigger.BackColor = Color.Yellow
        Else
            btnCcdImageTrigger.BackColor = SystemColors.Control
        End If

        If gDOCollection.GetState(enmDO.CcdBit0) = True Then
            btnCcdBit0.BackColor = Color.Yellow
        Else
            btnCcdBit0.BackColor = SystemColors.Control
        End If

        If gDOCollection.GetState(enmDO.CcdBit1) = True Then
            btnCcdBit1.BackColor = Color.Yellow
        Else
            btnCcdBit1.BackColor = SystemColors.Control
        End If

        '[說明]:Update DI
        If gDICollection.GetState(enmDI.CcdGate, False) = True Then
            btnCcdGate.BackColor = Color.Yellow
        Else
            btnCcdGate.BackColor = SystemColors.Control
        End If

        If gDICollection.GetState(enmDI.CcdBusy, False) = True Then
            btnCcdBusy.BackColor = Color.Yellow
        Else
            btnCcdBusy.BackColor = SystemColors.Control
        End If

        If gDICollection.GetState(enmDI.CcdReady, False) = True Then
            btnCcdReady.BackColor = Color.Yellow
        Else
            btnCcdReady.BackColor = SystemColors.Control
        End If

        If gDICollection.GetState(enmDI.CcdOutputResult, False) = True Then
            btnCcdOutputResult.BackColor = Color.Yellow
        Else
            btnCcdOutputResult.BackColor = SystemColors.Control
        End If

        '***********************************************************************************************
        txtCCDReceiveData.Text = gAOICollection.ReceiveString(enmCCD.CCD1)

        Select Case gAOICollection.GetStatus(enmCCD.CCD1)
            Case SockStatusInfo.sckConnected
                txtCCDConnectState.Text = "Connected"
            Case SockStatusInfo.sckDisconnect
                txtCCDConnectState.Text = "Disconnected"
            Case SockStatusInfo.sckError
                txtCCDConnectState.Text = "Error"
        End Select

    End Sub

    Private Sub btnCCDSendData_Click(sender As Object, e As EventArgs) Handles btnCCDSendData.Click
        gSyslog.Save("[frmCCDHandshake]" & vbTab & "[btnCCDSendData]" & vbTab & "Click")
        Dim strSendCommand As String

        strSendCommand = txtCCDSendData.Text


        Select Case gAOICollection.GetCCDType(enmCCD.CCD1) 'gSSystemParameter.enmCCDType
            Case enmCCDType.OmronFZS2MUDP
                If gAOICollection.GetStatus(enmCCD.CCD1) = SockStatusInfo.sckConnected = True Then
                    Call gAOICollection.CCDSendCommand(enmCCD.CCD1, strSendCommand)
                End If

            Case enmCCDType.OmronFZS2MTCP

            Case enmCCDType.KeyenceCV200CTCP

        End Select

    End Sub

    Private Sub btnConnectStatus_Click(sender As Object, e As EventArgs) Handles btnConnectStatus.Click
        gSyslog.Save("[frmCCDHandshake]" & vbTab & "[btnConnectStatus]" & vbTab & "Click")
        Select Case gAOICollection.GetCCDType(enmCCD.CCD1) 'gSSystemParameter.enmCCDType
            Case enmCCDType.OmronFZS2MUDP

            Case enmCCDType.OmronFZS2MTCP


            Case enmCCDType.KeyenceCV200CTCP
                'txtCCDReceiveData.Text = gAOICollection.ReceiveString(0)
                'If CType(gAOICollection.Items(0), CAOIKeyenceCV200CTCP).gKeyenceCcdTcp.IsConnected = False Then
                '    CType(gAOICollection.Items(0), CAOIKeyenceCV200CTCP).gKeyenceCcdTcp.Connect(gSSystemParameter.CCDSendAddress.strIP, gSSystemParameter.CCDSendAddress.intPort)
                'Else
                '    CType(gAOICollection.Items(0), CAOIKeyenceCV200CTCP).gKeyenceCcdTcp.Disconnect()
                'End If
        End Select
    End Sub

    Private Sub frmCCDHandshake_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        tmrCcdHandshake.Enabled = True
    End Sub

    Private Sub frmCCDHandshake_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        tmrCcdHandshake.Enabled = False
    End Sub

    Private Sub frmCCDHandshake_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub frmCCDHandshake_Load(sender As Object, e As EventArgs) Handles Me.Load
        Select Case gAOICollection.GetCCDType(enmCCD.CCD1) 'gSSystemParameter.enmCCDType
            Case enmCCDType.OmronFZS2MUDP
                btnConnectStatus.Visible = False

            Case enmCCDType.OmronFZS2MTCP
                btnConnectStatus.Visible = True

            Case enmCCDType.KeyenceCV200CTCP
                btnConnectStatus.Visible = True
        End Select
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub
End Class
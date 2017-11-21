Imports ProjectCore
Imports ProjectFeedback

''' <summary>程控光源工程設定介面</summary>
''' <remarks></remarks>
Public Class frmSetFMCSConnection
    Dim EditParameter(3) As sFMCSConnectionParameter

    Private Sub frmSetFMCSConnection_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub frmProgramLight_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i As Integer = 0 To gFMCSCollection.ConnectionParameter.Count - 1
            EditParameter(i).CardType = gFMCSCollection.ConnectionParameter(i).CardType
            EditParameter(i).PortName = gFMCSCollection.ConnectionParameter(i).PortName
            EditParameter(i).BaudRate = gFMCSCollection.ConnectionParameter(i).BaudRate
        Next

        cmbMeterType.Items.Clear()
        cmbMeterType.Items.Add(enmFMCSType.None & " None.")
        cmbMeterType.Items.Add(enmFMCSType.FMCS & " FMCS.")
        cmbMeterType.Items.Add(enmFMCSType.FMCS_P & " FMCS_P.")
        cmbMeterType.Items.Add(enmFMCSType.FMCS_PI & " FMCS_PI.")
        cmbMeterType.Items.Add(enmFMCSType.FMCS_PII & " FMCS_PII.")
        cmbCOMPort.Items.Clear()
        cmbCOMPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames())

        With cmbBaudRate.Items
            .Clear()
            .Add(CInt(enmBaudRate.e9600))
            .Add(CInt(enmBaudRate.e14400))
            .Add(CInt(enmBaudRate.e19200))
            .Add(CInt(enmBaudRate.e38400))
            .Add(CInt(enmBaudRate.e57600))
            .Add(CInt(enmBaudRate.e115200))
        End With

        cmbFMCS.Items.Clear()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                cmbFMCS.Items.Add("FMCS1")
                cmbFMCS.Items.Add("FMCS2")
                cmbFMCS.Items.Add("FMCS3")
                cmbFMCS.Items.Add("FMCS4")
                cmbFMCS.SelectedIndex = 0
                cmbFMCS.Visible = True
            Case enmMachineType.eDTS_2S2V
                cmbFMCS.Items.Add("FMCS1")
                cmbFMCS.Items.Add("FMCS2")
                cmbFMCS.SelectedIndex = 0
                cmbFMCS.Visible = True
            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        cmbFMCS.Items.Add("FMCS1")
                        cmbFMCS.SelectedIndex = 0
                        cmbFMCS.Visible = False
                    Case eMechanismModule.TwoValveOneStage
                        cmbFMCS.Items.Add("FMCS1")
                        cmbFMCS.Items.Add("FMCS2")
                        cmbFMCS.SelectedIndex = 0
                        cmbFMCS.Visible = True
                End Select
        End Select
      
        cmbDataBit.Visible = False '先隱藏
        lblDatabit.Visible = False
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub cmbCCD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFMCS.SelectedIndexChanged
        If cmbFMCS.SelectedIndex < 0 Then
            Exit Sub
        End If

        With gFMCSCollection.ConnectionParameter(cmbFMCS.SelectedIndex)

            If .PortName = "" Then '未設定
                '請先選擇COM Port
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000055))
                MsgBox(gMsgHandler.GetMessage(Warn_3000055), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            ElseIf cmbCOMPort.Items.Contains(.PortName) Then '配接程控光源COM Port
                cmbCOMPort.SelectedItem = .PortName
                'Else
                '    MsgBox(.PortName & " Not Exists.")
            End If

            If .CardType < cmbMeterType.Items.Count Then '配接程控光源類型.
                cmbMeterType.SelectedIndex = .CardType
            End If
            'If cmbBaudRate.Items.Contains(.BaudRate) Then '選擇Baud Rate
            Select Case .BaudRate
                Case enmBaudRate.e9600
                    cmbBaudRate.SelectedIndex = 0
                Case enmBaudRate.e14400
                    cmbBaudRate.SelectedIndex = 1
                Case enmBaudRate.e19200
                    cmbBaudRate.SelectedIndex = 2
                Case enmBaudRate.e38400
                    cmbBaudRate.SelectedIndex = 3
                Case enmBaudRate.e57600
                    cmbBaudRate.SelectedIndex = 4
                Case enmBaudRate.e115200
                    cmbBaudRate.SelectedIndex = 5
            End Select

            'End If

        End With

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmSetFMCSConnection]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Hide()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmSetFMCSConnection]" & vbTab & "[btnOK]" & vbTab & "Click")

        Try
            For i As Integer = 0 To gFMCSCollection.ConnectionParameter.Count - 1
                gFMCSCollection.ConnectionParameter(i).CardType = EditParameter(i).CardType
                gFMCSCollection.ConnectionParameter(i).PortName = EditParameter(i).PortName
                gFMCSCollection.ConnectionParameter(i).BaudRate = EditParameter(i).BaudRate
            Next

            Call gFMCSCollection.SaveFMCSConnectionParameter(Application.StartupPath & "\System\" & MachineName & "\CardFMCS.ini") '儲存程控光源設定值
            '儲存完成!請重啟程式
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000051))
            MsgBox(gMsgHandler.GetMessage(Warn_3000051), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("FMCS Save OK. Please Restart Software.")
            'Me.Hide()
        Catch ex As Exception
            '存檔失敗 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(cmbFMCS.SelectedItem & gMsgHandler.GetMessage(Warn_3000035), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gSyslog.Save("Exception Message: " & ex.Message)
        End Try

    End Sub

    Private Sub cmbLightType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMeterType.SelectedIndexChanged
        If cmbFMCS.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Select Case cmbMeterType.SelectedIndex
            Case -1
            Case 0 'None
                cmbCOMPort.Visible = False
                lblCOMPort.Visible = False
                cmbBaudRate.Visible = False
                lblBaudRate.Visible = False
                cmbDataBit.Visible = False
                lblDatabit.Visible = False
            Case 1 'KeyenceCV200CTCP
                cmbCOMPort.Visible = False
                lblCOMPort.Visible = False
                cmbBaudRate.Visible = False
                lblBaudRate.Visible = False
                cmbDataBit.Visible = False
                lblDatabit.Visible = False
            Case 2 'GTCTD12V30W
                cmbCOMPort.Visible = True
                lblCOMPort.Visible = True
                cmbBaudRate.Visible = True
                lblBaudRate.Visible = True
                cmbDataBit.Visible = False
                lblDatabit.Visible = False
        End Select

        EditParameter(cmbFMCS.SelectedIndex).CardType = cmbMeterType.SelectedIndex '將操作變更暫存至編輯區
    End Sub

    Private Sub cmbCOMPort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCOMPort.SelectedIndexChanged
        If cmbFMCS.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        EditParameter(cmbFMCS.SelectedIndex).PortName = cmbCOMPort.SelectedItem '將操作變更暫存至編輯區
    End Sub

    Private Sub cmbBaudRate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBaudRate.SelectedIndexChanged
        If cmbFMCS.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        EditParameter(cmbFMCS.SelectedIndex).BaudRate = cmbBaudRate.SelectedItem '將操作變更暫存至編輯區
    End Sub

End Class
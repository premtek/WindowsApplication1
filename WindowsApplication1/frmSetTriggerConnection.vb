Imports ProjectCore
Imports System.Windows.Forms
Imports ProjectTriggerBoard

''' <summary>觸發板工程設定介面</summary>
''' <remarks></remarks>
Public Class frmSetTriggerConnection

    ''' <summary>[連線設定參數]</summary>
    ''' <remarks></remarks>
    Dim mEditParameter(enmValve.Max) As sTriggerBoardConnectionParameter

    Private Sub frmSetTriggerConnection_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub frmProgramLight_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim mI As Integer

        For mI = 0 To gTriggerBoard.TBConnectionParameter.Count - 1
            mEditParameter(mI).CardType = gTriggerBoard.TBConnectionParameter(mI).CardType
            Select Case gTriggerBoard.TBConnectionParameter(mI).CardType
                Case eTriggerBoardType.Trigger30
                    mEditParameter(mI).TriggerBoard30.PortName = gTriggerBoard.TBConnectionParameter(mI).TriggerBoard30.PortName
                    mEditParameter(mI).TriggerBoard30.BaudRate = gTriggerBoard.TBConnectionParameter(mI).TriggerBoard30.BaudRate
            End Select
        Next

        cmbTriggerType.Items.Clear()
        cmbTriggerType.Items.Add(eTriggerBoardType.None & " None.")
        cmbTriggerType.Items.Add(eTriggerBoardType.Trigger30 & " Trigger 3.0")
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

        cmbTrigger.Items.Clear()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                cmbTrigger.Items.Add("TriggerNo1")
                cmbTrigger.Items.Add("TriggerNo2")
                cmbTrigger.Items.Add("TriggerNo3")
                cmbTrigger.Items.Add("TriggerNo4")
                cmbTrigger.SelectedIndex = 0
                cmbTrigger.Visible = True
                lblSelect.Visible = True
            Case enmMachineType.eDTS_2S2V
                cmbTrigger.Items.Add("TriggerNo1")
                cmbTrigger.Items.Add("TriggerNo2")
                cmbTrigger.SelectedIndex = 0
                cmbTrigger.Visible = True
                lblSelect.Visible = True
            Case Else
                Select Case gSSystemParameter.StageUseValveCount
                    Case eMechanismModule.OneValveOneStage
                        cmbTrigger.Items.Add("TriggerNo1")
                        cmbTrigger.SelectedIndex = 0
                        cmbTrigger.Visible = False
                        lblSelect.Visible = False
                    Case eMechanismModule.TwoValveOneStage
                        cmbTrigger.Items.Add("TriggerNo1")
                        cmbTrigger.Items.Add("TriggerNo2")
                        cmbTrigger.SelectedIndex = 0
                        cmbTrigger.Visible = True
                        lblSelect.Visible = True
                End Select

        End Select
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub cmbTrigger_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTrigger.SelectedIndexChanged
        If cmbTrigger.SelectedIndex < 0 Then
            Exit Sub
        End If

        With gTriggerBoard.TBConnectionParameter(cmbTrigger.SelectedIndex)
            '[說明]:配接Trigger board類型
            cmbTriggerType.SelectedIndex = .CardType

            Select Case .CardType
                Case eTriggerBoardType.Trigger30
                    '[說明]:配接Port Name
                    If cmbCOMPort.Items.Contains("COM" & .TriggerBoard30.PortName) Then
                        cmbCOMPort.SelectedItem = "COM" & .TriggerBoard30.PortName
                    End If

                    '[說明]:配接Baud Rate
                    Select Case .TriggerBoard30.BaudRate
                        Case CInt(enmBaudRate.e9600).ToString()
                            cmbBaudRate.SelectedIndex = 0
                        Case CInt(enmBaudRate.e14400).ToString()
                            cmbBaudRate.SelectedIndex = 1
                        Case CInt(enmBaudRate.e19200).ToString()
                            cmbBaudRate.SelectedIndex = 2
                        Case CInt(enmBaudRate.e38400).ToString()
                            cmbBaudRate.SelectedIndex = 3
                        Case CInt(enmBaudRate.e57600).ToString()
                            cmbBaudRate.SelectedIndex = 4
                        Case CInt(enmBaudRate.e115200).ToString()
                            cmbBaudRate.SelectedIndex = 5
                    End Select
            End Select

        End With

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmSetLightConnection]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Hide()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmSetLightConnection]" & vbTab & "[btnOK]" & vbTab & "Click")

        Try
            Dim mI As Integer
            For mI = 0 To gTriggerBoard.TBConnectionParameter.Count - 1
                gTriggerBoard.TBConnectionParameter(mI).CardType = mEditParameter(mI).CardType
                Select Case gTriggerBoard.TBConnectionParameter(mI).CardType
                    Case eTriggerBoardType.Trigger30
                        gTriggerBoard.TBConnectionParameter(mI).TriggerBoard30.PortName = mEditParameter(mI).TriggerBoard30.PortName
                        gTriggerBoard.TBConnectionParameter(mI).TriggerBoard30.BaudRate = mEditParameter(mI).TriggerBoard30.BaudRate

                End Select
            Next

            gTriggerBoard.Save(Application.StartupPath & "\System\" & MachineName & "\CardTriggerBoard.ini") 'Soni 2017.06.30 CardTrigger->CardTriggerBoard
            '儲存完成!請重啟程式
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000051))
            MsgBox(gMsgHandler.GetMessage(Warn_3000051), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        Catch ex As Exception
            '存檔失敗 
            gSyslog.Save("Trigger Board" & gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(cmbTrigger.SelectedItem & gMsgHandler.GetMessage(Warn_3000035), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gSyslog.Save("Exception Message: " & ex.Message)
        End Try
    End Sub

    Private Sub cmbTriggerType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTriggerType.SelectedIndexChanged
        If cmbTrigger.SelectedIndex < 0 Then
            '請先選擇觸發板
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000057))
            MsgBox(gMsgHandler.GetMessage(Warn_3000057), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Select Case cmbTriggerType.SelectedIndex
            Case -1
            Case 0 'None
                cmbCOMPort.Visible = False
                lblCOMPort.Visible = False
                cmbBaudRate.Visible = False
                lblBaudRate.Visible = False

            Case 1 'Trigger 3.0
                cmbCOMPort.Visible = True
                lblCOMPort.Visible = True
                cmbBaudRate.Visible = True
                lblBaudRate.Visible = True

        End Select
        '[說明]:將操作變更暫存至編輯區
        mEditParameter(cmbTrigger.SelectedIndex).CardType = cmbTriggerType.SelectedIndex
    End Sub

    Private Sub cmbCOMPort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCOMPort.SelectedIndexChanged
        If cmbTrigger.SelectedIndex < 0 Then
            '請先選擇觸發板
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000057))
            MsgBox(gMsgHandler.GetMessage(Warn_3000057), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        '[說明]:將操作變更暫存至編輯區
        Select Case mEditParameter(cmbTrigger.SelectedIndex).CardType
            Case eTriggerBoardType.Trigger30
                mEditParameter(cmbTrigger.SelectedIndex).TriggerBoard30.PortName = cmbCOMPort.SelectedItem.ToString.Substring(3)

        End Select
    End Sub

    Private Sub cmbBaudRate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBaudRate.SelectedIndexChanged
        If cmbTrigger.SelectedIndex < 0 Then
            '請先選擇觸發板
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000057))
            MsgBox(gMsgHandler.GetMessage(Warn_3000057), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        '[說明]:將操作變更暫存至編輯區
        Select Case mEditParameter(cmbTrigger.SelectedIndex).CardType
            Case eTriggerBoardType.Trigger30
                mEditParameter(cmbTrigger.SelectedIndex).TriggerBoard30.BaudRate = cmbBaudRate.SelectedItem.ToString()

        End Select
    End Sub

End Class
Imports ProjectCore
Imports ProjectMotion
Imports ProjectFeedback

Public Class frmSetBalance
    Public myResource As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetBalance))

    Private Sub frmSetBalance_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub frmBalance_Load(sender As Object, e As EventArgs) Handles Me.Load
      
        Me.Text = "Balance Config. "
        With cmbCardType.Items
            .Clear()
            .Add(sBalanceConnectionParameter.CardTypeToString(enmBalanceType.None))
            .Add(sBalanceConnectionParameter.CardTypeToString(enmBalanceType.AD4212A100))
            .Add(sBalanceConnectionParameter.CardTypeToString(enmBalanceType.WZA215_LC))
        End With

        With cmbBalance.Items
            .Clear()
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    .Add(myResource.GetString("cmbBalance.Items"))
                    .Add(myResource.GetString("cmbBalance.Items1"))
                    Me.Text += " 2-Balance"
                Case Else
                    .Add(myResource.GetString("cmbBalance.Items"))
                    Me.Text += " 1-Balance"
            End Select
        End With

        With cmbWeightPort.Items
            .Clear()
            .AddRange(System.IO.Ports.SerialPort.GetPortNames())
        End With
       

        Select Case cmbBalance.Items.Count
            Case 0
            Case 1
                cmbBalance.SelectedIndex = 0 '預選
                cmbBalance.Visible = False '單選的隱藏
                lblSelectBalance.Visible = False
                Me.Text += "(Selected)"
            Case Is > 0
                cmbBalance.SelectedIndex = 0 '預選
                cmbBalance.Visible = True
                lblSelectBalance.Visible = True
        End Select
        If cmbBalance.SelectedIndex >= 0 Then
            Dim mPortName As String = gBalanceCollection.WeighingUnit(cmbBalance.SelectedIndex).PortName
            If cmbWeightPort.Items.Contains(mPortName) Then
                cmbWeightPort.SelectedText = mPortName
            End If
        End If
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

#Region "介面操作"
    Private Sub btnProductionWeighingUnit_Click(sender As Object, e As EventArgs) Handles btnProductionWeighingUnit.Click
        gSyslog.Save("[frmBalance]" & vbTab & "[btnProductionWeighingUnit]" & vbTab & "Click")
        If btnProductionWeighingUnit.Enabled = False Then
            Exit Sub
        End If
        btnProductionWeighingUnit.Enabled = False
        If cmbBalance.SelectedIndex < 0 Then
            '請先選擇天秤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000026))
            MsgBox(gMsgHandler.GetMessage(Warn_3000026), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnProductionWeighingUnit.Enabled = True
            Exit Sub
        End If
        Dim value As Double
        If gBalanceCollection.RequestCurrentValue(cmbBalance.SelectedIndex, value, True) = False Then
            MsgBox(gMsgHandler.GetMessage(Error_1015002), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnProductionWeighingUnit.Enabled = True
            Exit Sub
        End If
        lblWeight.Text = myResource.GetString("btnProductionWeighingUnit.Text") & value
        btnProductionWeighingUnit.Enabled = True
    End Sub

    Private Sub btnWeightUnitRezero_Click(sender As Object, e As EventArgs) Handles btnWeightUnitRezero.Click
        gSyslog.Save("[frmBalance]" & vbTab & "[btnWeightUnitRezero]" & vbTab & "Click")
        If btnWeightUnitRezero.Enabled = False Then
            Exit Sub
        End If
        btnWeightUnitRezero.Enabled = False
        If cmbBalance.SelectedIndex < 0 Then
            '請先選擇天秤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000026))
            MsgBox(gMsgHandler.GetMessage(Warn_3000026), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        gBalanceCollection.Rezero(cmbBalance.SelectedIndex)
        btnWeightUnitRezero.Enabled = True
    End Sub

#End Region
    
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmBalance]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmBalance]" & vbTab & "[btnOK]" & vbTab & "Click")
        If btnOK.Enabled = False Then
            Exit Sub
        End If
        btnOK.Enabled = False
        If cmbWeightPort.SelectedIndex < 0 Then
            '20161010
            btnOK.Enabled = True
            Exit Sub
        End If
        gBalanceCollection.WeighingUnit(cmbBalance.SelectedIndex).PortName = cmbWeightPort.SelectedItem
        gBalanceCollection.WeighingUnit(cmbBalance.SelectedIndex).CardType = cmbCardType.SelectedIndex
        '[說明]:紀錄按之前後的狀態
        gBalanceCollection.SaveBalanceConnectionParameter(Application.StartupPath & "\System\" & MachineName & "\CardBalance.ini") '儲存天平參數
        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        btnOK.Enabled = True
        '20161010
        'Me.Close()
    End Sub

  
    Private Sub cmbBalance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBalance.SelectedIndexChanged
        If cmbBalance.SelectedIndex < 0 Then
            Exit Sub
        End If

        cmbCardType.SelectedIndex = gBalanceCollection.WeighingUnit(cmbBalance.SelectedIndex).CardType

        If cmbWeightPort.Items.Contains(gBalanceCollection.WeighingUnit(cmbBalance.SelectedIndex).PortName) Then
            cmbWeightPort.SelectedItem = gBalanceCollection.WeighingUnit(cmbBalance.SelectedIndex).PortName
        End If

    End Sub
End Class
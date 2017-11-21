Imports ProjectCore
Imports ProjectAOI
''' <summary>程控光源工程設定介面</summary>
''' <remarks></remarks>
Public Class frmSetLightConnection
    Dim EditParameter(3) As sLightConnectionParameter

    Private Sub frmSetLightConnection_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub frmProgramLight_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        For mLightNo As Integer = 0 To gLightCollection.Cards.Count - 1
            EditParameter(mLightNo).CardType = gLightCollection.Cards(mLightNo).CardType
            EditParameter(mLightNo).PortName = gLightCollection.Cards(mLightNo).PortName
            EditParameter(mLightNo).BaudRate = gLightCollection.Cards(mLightNo).BaudRate
            EditParameter(mLightNo).DataBit = gLightCollection.Cards(mLightNo).DataBit
            EditParameter(mLightNo).ChannelMaxValue1 = gLightCollection.Cards(mLightNo).ChannelMaxValue1
            EditParameter(mLightNo).ChannelMaxValue2 = gLightCollection.Cards(mLightNo).ChannelMaxValue2
            EditParameter(mLightNo).ChannelMaxValue3 = gLightCollection.Cards(mLightNo).ChannelMaxValue3
            EditParameter(mLightNo).ChannelMaxValue4 = gLightCollection.Cards(mLightNo).ChannelMaxValue4
            EditParameter(mLightNo).Unit = gLightCollection.Cards(mLightNo).Unit
            EditParameter(mLightNo).ChannelScale1 = gLightCollection.Cards(mLightNo).ChannelScale1
            EditParameter(mLightNo).ChannelScale2 = gLightCollection.Cards(mLightNo).ChannelScale2
            EditParameter(mLightNo).ChannelScale3 = gLightCollection.Cards(mLightNo).ChannelScale3
            EditParameter(mLightNo).ChannelScale4 = gLightCollection.Cards(mLightNo).ChannelScale4
        Next

        cmbLightType.Items.Clear()
        cmbLightType.Items.Add(enmProgramLightType.None & " None.")
        cmbLightType.Items.Add(enmProgramLightType.KeyenceCV200CTCP & " KeyenceCV200CTCP.")
        cmbLightType.Items.Add(enmProgramLightType.GLCTD12V30W & " GTCTD12V30W.")
        cmbLightType.Items.Add(enmProgramLightType.CTK_P_RS & " CTK_P_RS")
        cmbLightType.Items.Add(enmProgramLightType.OPT_DP1024_4 & " OPT_DP1024_4")
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

        cmbCCD.Items.Clear()

        If gSSystemParameter.LanguageType = enmLanguageType.eTraditionalChinese Then
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    cmbCCD.Items.Add("控制器1")
                    cmbCCD.Items.Add("控制器2")
                    cmbCCD.Items.Add("控制器3")
                    cmbCCD.Items.Add("控制器4")
                    cmbCCD.SelectedIndex = 0
                    cmbCCD.Visible = True
                    lblSelectCCD.Visible = True
                Case enmMachineType.eDTS_2S2V
                    cmbCCD.Items.Add("控制器1")
                    cmbCCD.Items.Add("控制器2")
                    cmbCCD.SelectedIndex = 0
                    cmbCCD.Visible = True
                    lblSelectCCD.Visible = True
                Case Else
                    cmbCCD.Items.Add("控制器1")
                    cmbCCD.SelectedIndex = 0
                    cmbCCD.Visible = False
                    lblSelectCCD.Visible = False
            End Select

        Else

            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    cmbCCD.Items.Add("Controller1")
                    cmbCCD.Items.Add("Controller2")
                    cmbCCD.Items.Add("Controller3")
                    cmbCCD.Items.Add("Controller4")
                    cmbCCD.SelectedIndex = 0
                    cmbCCD.Visible = True
                    lblSelectCCD.Visible = True
                Case enmMachineType.eDTS_2S2V
                    cmbCCD.Items.Add("Controller1")
                    cmbCCD.Items.Add("Controller2")
                    cmbCCD.SelectedIndex = 0
                    cmbCCD.Visible = True
                    lblSelectCCD.Visible = True
                Case Else
                    cmbCCD.Items.Add("Controller1")
                    cmbCCD.SelectedIndex = 0
                    cmbCCD.Visible = False
                    lblSelectCCD.Visible = False
            End Select

        End If



 
        cmbDataBit.Visible = False '先隱藏
        lblDatabit.Visible = False

        '20160625
        If gUserLevel = enmUserLevel.eAdministrator Or gUserLevel = enmUserLevel.eSoftwareMaker Then '軟控與客服才可設置光源通道最大值
            grpMaxValue.Visible = True
            LblChMaxValue1.Visible = True
            LblChMaxValue2.Visible = True
            LblChMaxValue3.Visible = True
            LblChMaxValue4.Visible = True
            nmcCh1MaxNum.Visible = True
            nmcCh2MaxNum.Visible = True
            nmcCh3MaxNum.Visible = True
            nmcCh4MaxNum.Visible = True
        Else
            grpMaxValue.Visible = False
            LblChMaxValue1.Visible = False
            LblChMaxValue2.Visible = False
            LblChMaxValue3.Visible = False
            LblChMaxValue4.Visible = False
            nmcCh1MaxNum.Visible = False
            nmcCh2MaxNum.Visible = False
            nmcCh3MaxNum.Visible = False
            nmcCh4MaxNum.Visible = False
        End If
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub cmbCCD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCCD.SelectedIndexChanged
        If cmbCCD.SelectedIndex < 0 Then
            Exit Sub
        End If

        With gLightCollection.Cards(cmbCCD.SelectedIndex)

            If .PortName = "" Then '未設定
                '請先選擇COM Port
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000055))
                MsgBox(gMsgHandler.GetMessage(Warn_3000055), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            ElseIf cmbCOMPort.Items.Contains(.PortName) Then '配接程控光源COM Port
                cmbCOMPort.SelectedItem = .PortName
                'Else
                '    MsgBox(.PortName & " Not Exists.")
            End If

            If .CardType < cmbLightType.Items.Count Then '配接程控光源類型.
                cmbLightType.SelectedIndex = .CardType
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
            nmcCh1MaxNum.Value = .ChannelMaxValue1
            nmcCh2MaxNum.Value = .ChannelMaxValue2
            nmcCh3MaxNum.Value = .ChannelMaxValue3
            nmcCh4MaxNum.Value = .ChannelMaxValue4

        End With

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmSetLightConnection]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Hide()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmSetLightConnection]" & vbTab & "[btnOK]" & vbTab & "Click")
        If btnOK.Enabled = False Then
            Exit Sub
        End If
        btnOK.Enabled = False
        Try

            For mCardNo As Integer = 0 To gLightCollection.Cards.Count - 1
                gLightCollection.Cards.Parameters(mCardNo).CardType = EditParameter(mCardNo).CardType
                gLightCollection.Cards.Parameters(mCardNo).PortName = EditParameter(mCardNo).PortName
                gLightCollection.Cards.Parameters(mCardNo).BaudRate = EditParameter(mCardNo).BaudRate
                gLightCollection.Cards.Parameters(mCardNo).DataBit = EditParameter(mCardNo).DataBit
                gLightCollection.Cards.Parameters(mCardNo).ChannelMaxValue1 = EditParameter(mCardNo).ChannelMaxValue1
                gLightCollection.Cards.Parameters(mCardNo).ChannelMaxValue2 = EditParameter(mCardNo).ChannelMaxValue2
                gLightCollection.Cards.Parameters(mCardNo).ChannelMaxValue3 = EditParameter(mCardNo).ChannelMaxValue3
                gLightCollection.Cards.Parameters(mCardNo).ChannelMaxValue4 = EditParameter(mCardNo).ChannelMaxValue4
                If EditParameter(mCardNo).Unit > 0 Then '[Note]Ernest要求電流正規化 0-255
                    gLightCollection.Cards.Parameters(mCardNo).ChannelScale1 = Math.Round(EditParameter(mCardNo).ChannelMaxValue1 / EditParameter(mCardNo).Unit, 3)
                    gLightCollection.Cards.Parameters(mCardNo).ChannelScale2 = Math.Round(EditParameter(mCardNo).ChannelMaxValue2 / EditParameter(mCardNo).Unit, 3)
                    gLightCollection.Cards.Parameters(mCardNo).ChannelScale3 = Math.Round(EditParameter(mCardNo).ChannelMaxValue3 / EditParameter(mCardNo).Unit, 3)
                    gLightCollection.Cards.Parameters(mCardNo).ChannelScale4 = Math.Round(EditParameter(mCardNo).ChannelMaxValue4 / EditParameter(mCardNo).Unit, 3)
                End If
            Next

            Call gLightCollection.Cards.SaveLightControlParameter(Application.StartupPath & "\System\" & MachineName & "\CardLightControl.ini") '儲存程控光源設定值
            '儲存完成!請重啟程式
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000051))
            MsgBox(gMsgHandler.GetMessage(Warn_3000051), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnOK.Enabled = True
            '20161010
            'Me.Hide()
        Catch ex As Exception

            '存檔失敗 
            gSyslog.Save("Program Light" & gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(cmbCCD.SelectedItem & gMsgHandler.GetMessage(Warn_3000035), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gSyslog.Save("Exception Message: " & ex.Message)
            btnOK.Enabled = True
        End Try

    End Sub

    Private Sub cmbLightType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLightType.SelectedIndexChanged
        If cmbCCD.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Select Case cmbLightType.SelectedIndex
            Case -1
            Case enmProgramLightType.None '0 'None
                cmbCOMPort.Visible = False
                lblCOMPort.Visible = False
                cmbBaudRate.Visible = False
                lblBaudRate.Visible = False
                cmbDataBit.Visible = False
                lblDatabit.Visible = False
            Case enmProgramLightType.KeyenceCV200CTCP '1 'KeyenceCV200CTCP
                cmbCOMPort.Visible = False
                lblCOMPort.Visible = False
                cmbBaudRate.Visible = False
                lblBaudRate.Visible = False
                cmbDataBit.Visible = False
                lblDatabit.Visible = False
            Case enmProgramLightType.GLCTD12V30W '2 'GTCTD12V30W
                cmbCOMPort.Visible = True
                lblCOMPort.Visible = True
                cmbBaudRate.Visible = True
                lblBaudRate.Visible = True
                cmbDataBit.Visible = False
                lblDatabit.Visible = False
            Case enmProgramLightType.CTK_P_RS '3 'CTK-P-R
                cmbCOMPort.Visible = True
                lblCOMPort.Visible = True
                cmbBaudRate.Visible = True
                lblBaudRate.Visible = True
                cmbDataBit.Visible = False
                lblDatabit.Visible = False
            Case enmProgramLightType.OPT_DP1024_4 '4 'OPT_DP1024
                cmbCOMPort.Visible = True
                lblCOMPort.Visible = True
                cmbBaudRate.Visible = True
                lblBaudRate.Visible = True
                cmbDataBit.Visible = False
                lblDatabit.Visible = False
        End Select

        EditParameter(cmbCCD.SelectedIndex).CardType = cmbLightType.SelectedIndex '將操作變更暫存至編輯區
    End Sub

    Private Sub cmbCOMPort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCOMPort.SelectedIndexChanged
        If cmbCCD.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        EditParameter(cmbCCD.SelectedIndex).PortName = cmbCOMPort.SelectedItem '將操作變更暫存至編輯區
    End Sub

    Private Sub cmbBaudRate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBaudRate.SelectedIndexChanged
        If cmbCCD.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        EditParameter(cmbCCD.SelectedIndex).BaudRate = cmbBaudRate.SelectedItem '將操作變更暫存至編輯區
    End Sub

    Private Sub cmbDataBit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDataBit.SelectedIndexChanged
        If cmbCCD.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        EditParameter(cmbCCD.SelectedIndex).DataBit = cmbDataBit.SelectedItem '將操作變更暫存至編輯區
    End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs)
    '    Dim frmPCL As New frmProgramControlLight
    '    frmPCL.ShowDialog()
    'End Sub
    Private Sub btnLightControl_Click(sender As Object, e As EventArgs) Handles btnLightControl.Click
        Dim frmPCL As New frmProgramControlLight
        frmPCL.ShowDialog()
    End Sub
    'Private Sub txtCh1MaxNum_TextChanged(sender As Object, e As EventArgs)
    '    If cmbCCD.SelectedIndex < 0 Then
    '        '請先選擇CCD
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
    '        MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    '        Exit Sub
    '    End If
    '    If Not IsNumeric(txtCh1MaxNum.Text) Then 'wenda
    '        txtCh1MaxNum.Text = 0
    '    End If
    '    EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue1 = CInt(txtCh1MaxNum.Text)  '將操作變更暫存至編輯區
    '    EditParameter(cmbCCD.SelectedIndex).ChannelScale1 = EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue1 / EditParameter(cmbCCD.SelectedIndex).Unit
    'End Sub

    'Private Sub txtCh2MaxNum_TextChanged(sender As Object, e As EventArgs)
    '    If cmbCCD.SelectedIndex < 0 Then
    '        '請先選擇CCD
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
    '        MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    '        Exit Sub
    '    End If
    '    If Not IsNumeric(txtCh2MaxNum.Text) Then
    '        txtCh2MaxNum.Text = 0
    '    End If

    '    EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue2 = CInt(txtCh2MaxNum.Text)  '將操作變更暫存至編輯區
    '    EditParameter(cmbCCD.SelectedIndex).ChannelScale2 = EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue2 / EditParameter(cmbCCD.SelectedIndex).Unit
    'End Sub

    'Private Sub txtCh3MaxNum_TextChanged(sender As Object, e As EventArgs)
    '    If cmbCCD.SelectedIndex < 0 Then
    '        '請先選擇CCD
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
    '        MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    '        Exit Sub
    '    End If
    '    If Not IsNumeric(txtCh3MaxNum.Text) Then
    '        txtCh3MaxNum.Text = 0
    '    End If
    '    EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue3 = CInt(txtCh3MaxNum.Text)  '將操作變更暫存至編輯區
    '    EditParameter(cmbCCD.SelectedIndex).ChannelScale3 = EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue3 / EditParameter(cmbCCD.SelectedIndex).Unit
    'End Sub

    'Private Sub txtCh4MaxNum_TextChanged(sender As Object, e As EventArgs)
    '    If cmbCCD.SelectedIndex < 0 Then
    '        '請先選擇CCD
    '        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
    '        MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
    '        Exit Sub
    '    End If
    '    If Not IsNumeric(txtCh4MaxNum.Text) Then
    '        txtCh4MaxNum.Text = 0
    '    End If
    '    EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue4 = CInt(txtCh4MaxNum.Text)  '將操作變更暫存至編輯區
    '    EditParameter(cmbCCD.SelectedIndex).ChannelScale4 = EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue4 / EditParameter(cmbCCD.SelectedIndex).Unit
    'End Sub



    Private Sub nmcCh1MaxNum_ValueChanged(sender As Object, e As EventArgs) Handles nmcCh1MaxNum.ValueChanged
        If cmbCCD.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue1 = nmcCh1MaxNum.Value  '將操作變更暫存至編輯區
        If EditParameter(cmbCCD.SelectedIndex).Unit > 0 Then
            EditParameter(cmbCCD.SelectedIndex).ChannelScale1 = EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue1 / EditParameter(cmbCCD.SelectedIndex).Unit
        End If

    End Sub

    Private Sub nmcCh2MaxNum_ValueChanged(sender As Object, e As EventArgs) Handles nmcCh2MaxNum.ValueChanged
        If cmbCCD.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue2 = nmcCh2MaxNum.Value  '將操作變更暫存至編輯區
        If EditParameter(cmbCCD.SelectedIndex).Unit > 0 Then
            EditParameter(cmbCCD.SelectedIndex).ChannelScale2 = EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue2 / EditParameter(cmbCCD.SelectedIndex).Unit
        End If
    End Sub

    Private Sub nmcCh3MaxNum_ValueChanged(sender As Object, e As EventArgs) Handles nmcCh3MaxNum.ValueChanged
        If cmbCCD.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue3 = nmcCh3MaxNum.Value  '將操作變更暫存至編輯區
        If EditParameter(cmbCCD.SelectedIndex).Unit > 0 Then
            EditParameter(cmbCCD.SelectedIndex).ChannelScale3 = EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue3 / EditParameter(cmbCCD.SelectedIndex).Unit
        End If

    End Sub

    Private Sub nmcCh4MaxNum_ValueChanged(sender As Object, e As EventArgs) Handles nmcCh4MaxNum.ValueChanged
        If cmbCCD.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue4 = nmcCh4MaxNum.Value  '將操作變更暫存至編輯區
        If EditParameter(cmbCCD.SelectedIndex).Unit > 0 Then
            EditParameter(cmbCCD.SelectedIndex).ChannelScale4 = EditParameter(cmbCCD.SelectedIndex).ChannelMaxValue4 / EditParameter(cmbCCD.SelectedIndex).Unit
        End If
    End Sub
End Class
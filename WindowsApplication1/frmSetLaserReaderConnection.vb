Imports ProjectCore
Imports Premtek.Base

Public Class frmSetLaserReaderConnection

    ''' <summary>連線參數(編輯用)</summary>
    ''' <remarks></remarks>
    Dim EditCards(enmLaserReader.Max) As ProjectLaserInterferometer.sLaserReaderConnectionParameter

    Private Sub frmSetLaserReaderConnection_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If cmbLaserReader.Items.Count > 0 Then
            If cmbLaserReader.SelectedIndex < 0 Then cmbLaserReader.SelectedIndex = 0
        End If
    End Sub

    Private Sub frmSetLaserReaderConnection_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub frmLaserReader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '資料讀取至編輯區
        ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.ArrayCopyTo(EditCards)
        cmbLaserReaderType.Items.Clear()
        cmbLaserReaderType.Items.Add(enmLaserInterferometerType.None & " None")
        cmbLaserReaderType.Items.Add(enmLaserInterferometerType.KeyenceILS065Voltage & " IL-S065")
        cmbLaserReaderType.Items.Add(enmLaserInterferometerType.KeyenceLJV7060TCP & " LJ-V7060 TCP")
        cmbLaserReaderType.Items.Add(enmLaserInterferometerType.KeyenceIL065DLRS1A & "IL-S065 DL-RS1A")
        cmbLaserReader.Items.Clear()

        With cmbLaserReader.Items
            .Clear()
            Select Case gSSystemParameter.MachineType
                Case enmMachineType.DCSW_800AQ
                    .Add("LaserReader1")
                    .Add("LaserReader2")
                    .Add("LaserReader3")
                    .Add("LaserReader4")
                    Me.Text += " 4-LaserReader"
                    cmbLaserReader.Visible = True
                    lblSelectLaserReader.Visible = True
                Case enmMachineType.eDTS_2S2V
                    .Add("LaserReader1")
                    .Add("LaserReader2")
                    Me.Text += " 2-LaserReader"
                    cmbLaserReader.Visible = True
                    lblSelectLaserReader.Visible = True
                Case Else
                    .Add("LaserReader1")
                    Me.Text += " 1-LaserReader"
                    cmbLaserReader.Visible = False
                    lblSelectLaserReader.Visible = False
            End Select
        End With
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
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub cmbLaserReader_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLaserReader.SelectedIndexChanged
        If cmbLaserReader.SelectedIndex < 0 Then
            '請先選擇 Laser Reader
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000056))
            MsgBox(gMsgHandler.GetMessage(Warn_3000056), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        lblValue.Text = ""
        With EditCards(cmbLaserReader.SelectedIndex)
            cmbLaserReaderType.SelectedIndex = CInt(.CardType)
            txtIPAddress.Text = .LJV7060TCP.IP
            txtPort.Text = .LJV7060TCP.Port

            If cmbCOMPort.Items.Contains(.DLRS1A.PortName) Then
                cmbCOMPort.SelectedItem = .DLRS1A.PortName
            End If

            '[說明]:配接Baud Rate
            Select Case CInt(.DLRS1A.BaudRate)
                Case enmBaudRate.e9600 '.ToString()
                    cmbBaudRate.SelectedIndex = 0
                Case enmBaudRate.e14400 '.ToString()
                    cmbBaudRate.SelectedIndex = 1
                Case enmBaudRate.e19200 '.ToString()
                    cmbBaudRate.SelectedIndex = 2
                Case enmBaudRate.e38400 '.ToString()
                    cmbBaudRate.SelectedIndex = 3
                Case enmBaudRate.e57600 '.ToString()
                    cmbBaudRate.SelectedIndex = 4
                Case enmBaudRate.e115200 '.ToString()
                    cmbBaudRate.SelectedIndex = 5
            End Select

        End With
    End Sub

    Private Sub cmbLaserReaderType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLaserReaderType.SelectedIndexChanged
        If cmbLaserReader.SelectedIndex < 0 Then
            '請先選擇 Laser Reader
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000056))
            MsgBox(gMsgHandler.GetMessage(Warn_3000056), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Const ROW_HEIGHT As Integer = 36

        Select Case cmbLaserReaderType.SelectedIndex
            Case -1
            Case 0
                tbpItem.RowStyles(0).Height = ROW_HEIGHT
                tbpItem.RowStyles(1).Height = ROW_HEIGHT
                tbpItem.RowStyles(tbpItem.RowStyles.Count - 1).Height = ROW_HEIGHT '最末列
                tbpItem.RowStyles(2).Height = 0
                tbpItem.RowStyles(3).Height = 0
                tbpItem.RowStyles(4).Height = 0
                tbpItem.RowStyles(5).Height = 0
                tbpItem.Height = ROW_HEIGHT * 3
            Case 1
                tbpItem.RowStyles(0).Height = ROW_HEIGHT
                tbpItem.RowStyles(1).Height = ROW_HEIGHT
                tbpItem.RowStyles(tbpItem.RowStyles.Count - 1).Height = ROW_HEIGHT '最末列
                tbpItem.RowStyles(2).Height = 0
                tbpItem.RowStyles(3).Height = 0
                tbpItem.RowStyles(4).Height = 0
                tbpItem.RowStyles(5).Height = 0
                tbpItem.Height = ROW_HEIGHT * 3 + 40
            Case 2
                tbpItem.RowStyles(0).Height = ROW_HEIGHT
                tbpItem.RowStyles(1).Height = ROW_HEIGHT
                tbpItem.RowStyles(tbpItem.RowStyles.Count - 1).Height = ROW_HEIGHT '最末列
                tbpItem.RowStyles(2).Height = ROW_HEIGHT
                tbpItem.RowStyles(3).Height = ROW_HEIGHT
                tbpItem.RowStyles(4).Height = 0
                tbpItem.RowStyles(5).Height = 0
                tbpItem.Height = ROW_HEIGHT * 5 + 40
            Case 3
                tbpItem.RowStyles(0).Height = ROW_HEIGHT
                tbpItem.RowStyles(1).Height = ROW_HEIGHT
                tbpItem.RowStyles(tbpItem.RowStyles.Count - 1).Height = ROW_HEIGHT '最末列
                tbpItem.RowStyles(2).Height = 0
                tbpItem.RowStyles(3).Height = 0
                tbpItem.RowStyles(4).Height = ROW_HEIGHT
                tbpItem.RowStyles(5).Height = ROW_HEIGHT
                tbpItem.Height = ROW_HEIGHT * 5 + 40
        End Select
        EditCards(cmbLaserReader.SelectedIndex).CardType = cmbLaserReaderType.SelectedIndex '將操作變更暫存至編輯區
    End Sub

    Private Sub txtIPAddress_TextChanged(sender As Object, e As EventArgs) Handles txtIPAddress.TextChanged
        If cmbLaserReader.SelectedIndex < 0 Then
            '請先選擇 Laser Reader
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000056))
            MsgBox(gMsgHandler.GetMessage(Warn_3000056), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        EditCards(cmbLaserReader.SelectedIndex).LJV7060TCP.IP = txtIPAddress.Text '將操作變更暫存至編輯區
    End Sub

    Private Sub txtPort_TextChanged(sender As Object, e As EventArgs) Handles txtPort.TextChanged
        If cmbLaserReader.SelectedIndex < 0 Then
            '請先選擇 Laser Reader
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000056))
            MsgBox(gMsgHandler.GetMessage(Warn_3000056), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        EditCards(cmbLaserReader.SelectedIndex).LJV7060TCP.Port = txtPort.Text '將操作變更暫存至編輯區
    End Sub

    Private Sub txtPortName_TextChanged(sender As Object, e As EventArgs)
        If cmbLaserReader.SelectedIndex < 0 Then
            '請先選擇 Laser Reader
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000056))
            MsgBox(gMsgHandler.GetMessage(Warn_3000056), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        EditCards(cmbLaserReader.SelectedIndex).DLRS1A.PortName = cmbCOMPort.SelectedItem '將操作變更暫存至編輯區
    End Sub

    Private Sub txtBaudRate_TextChanged(sender As Object, e As EventArgs)
        If cmbLaserReader.SelectedIndex < 0 Then
            '請先選擇 Laser Reader
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000056))
            MsgBox(gMsgHandler.GetMessage(Warn_3000056), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        EditCards(cmbLaserReader.SelectedIndex).DLRS1A.BaudRate = cmbBaudRate.SelectedItem '將操作變更暫存至編輯區
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmSetLightConnection]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmSetLightConnection]" & vbTab & "[btnSave]" & vbTab & "Click")

        Try
            '編輯區資料全部套用
            ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.ArrayCopyFrom(EditCards)
            ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.Save(ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.CardFileName) '儲存雷射主機設定值

            '20161010
            Dim mDLRS1A As New ProjectLaserInterferometer.DLRS1AConnect
            Dim LJV7060TCP As New LJV7060TCPConnect

            If cmbCOMPort.SelectedIndex <> Nothing Then
                mDLRS1A.PortName = cmbCOMPort.SelectedIndex
            End If

            '[說明]:配接Baud Rate
            Select Case cmbBaudRate.SelectedIndex
                Case 0
                    mDLRS1A.BaudRate = enmBaudRate.e9600
                Case 1
                    mDLRS1A.BaudRate = enmBaudRate.e14400
                Case 2
                    mDLRS1A.BaudRate = enmBaudRate.e19200
                Case 3
                    mDLRS1A.BaudRate = enmBaudRate.e38400
                Case 4
                    mDLRS1A.BaudRate = enmBaudRate.e57600
                Case 5
                    mDLRS1A.BaudRate = enmBaudRate.e115200
            End Select


            If txtIPAddress.Text <> Nothing Then
                LJV7060TCP.IP = txtIPAddress.Text
            End If
            If txtPort.Text <> Nothing Then
                LJV7060TCP.Port = Val(txtPort.Text)
            End If

            EditCards(cmbLaserReader.SelectedIndex).DLRS1A = mDLRS1A
            Dim CardFileName As String = Application.StartupPath & "\System\" & MachineName & "\CardLaserReader.ini"
            ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.Cards.Save(CardFileName)

            '儲存完成!請重啟程式
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000051))
            MsgBox(gMsgHandler.GetMessage(Warn_3000051), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            '20161010
            'Me.Close()
        Catch ex As Exception
            '存檔失敗 
            gSyslog.Save(cmbLaserReader.SelectedItem & gMsgHandler.GetMessage(Warn_3000035))
            MsgBox(cmbLaserReader.SelectedItem & gMsgHandler.GetMessage(Warn_3000035), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gSyslog.Save("Exception Message: " & ex.Message)
        End Try


    End Sub
    Private Sub frmSetLaserReaderConnection_Validated(sender As Object, e As EventArgs) Handles Me.Validated
        Debug.Print("Validated")
    End Sub

    Private Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click
        If btnGet.Enabled = False Then
            Exit Sub
        End If
        btnGet.Enabled = False
        If cmbLaserReader.SelectedIndex < 0 Then
            '請先選擇 Laser Reader
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000056))
            MsgBox(gMsgHandler.GetMessage(Warn_3000056), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnGet.Enabled = True
            Exit Sub
        End If
        'gLaserReaderCollection.ResetState(cmbLaserReader.SelectedIndex)
        Dim value As String = ""

        Select Case gSSystemParameter.MeasureType 'TOOD: ProjectLaserInterferometer -> Premtek.Base 目前測試讀值失敗
            Case enmMeasureType.Contact  '接觸式測高 
                ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", cmbLaserReader.SelectedIndex, value, True)
            Case enmMeasureType.Laser
                ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", cmbLaserReader.SelectedIndex, value, True)
        End Select


        'gLaserReaderCollection.GetValue("Laser", cmbLaserReader.SelectedIndex, value, True)
        'Sue20170703 加單位顯示
        lblValue.Text = value + "mm"
        btnGet.Enabled = True
    End Sub
End Class
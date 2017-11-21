Imports ProjectCore
Imports Cognex.VisionPro
Imports ProjectAOI

''' <summary>CCD連線設定介面</summary>
''' <remarks></remarks>
Public Class frmSetCCDConnection

    ''' <summary>連線設定(編輯用)</summary>
    ''' <remarks></remarks>
    Dim EditParameter(enmCCD.ConstMax) As sCCDConnectionParameter

    Dim DeviceNum As Integer

    Dim iniflag As Boolean = True

    Private Sub cmbCCD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCCD.SelectedIndexChanged
        If cmbCCD.SelectedIndex < 0 Then
            Exit Sub
        End If
        'Dim str As String
        With EditParameter(cmbCCD.SelectedIndex)
            If cmbCCDType.Items.Count > CInt(.CCDType) Then
                cmbCCDType.SelectedIndex = CInt(.CCDType)
            End If
            txtSendIP.Text = .Omron.SendIPAddress
            txtSendPort.Text = .Omron.SendPort
            txtRecieveIP.Text = .Omron.RecieveIPAddress
            txtRecievePort.Text = .Omron.RecievePort

            lblCCDNo.Text = cmbCCD.SelectedIndex
            lblFileName.Text = cmbCCD.Text
            cmbTriggerType.Items.Clear()
            cmbTriggerType.Items.Add("0 None")
            cmbTriggerType.Items.Add("1 SoftwareTrigger")
            cmbTriggerType.Items.Add("2 HardwareTrigger")


            cmbTriggerType.Text = cmbTriggerType.Items(.Cognex.TriggerType)
            .Cognex.CCDNo = lblCCDNo.Text
            .Cognex.ShortFileName = lblFileName.Text
            cmbCCD_SN.Text = .Cognex.SerialNumber
            cmbVideoType.Text = (TransformVideoNumToStr(.Cognex.VideoFormatType))
            'cmbAWB.SelectedIndex = .Cognex.AutoWhiteBalance
        End With

    End Sub

    Private Sub frmSetCCDConnection_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Eason 20170209 Ticket:100060 :Memory Log
        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub frmSetCCDConnection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '資料讀取至編輯區
        gAOICollection.ArrayCopyTo(EditParameter)

        cmbCCDType.Items.Clear()
        cmbCCDType.Items.Add(enmCCDType.None & " None")
        cmbCCDType.Items.Add(enmCCDType.OmronFZS2MUDP & " Omron FZS2M UDP")
        cmbCCDType.Items.Add(enmCCDType.OmronFZS2MTCP & " Omron FZS2M TCP")
        cmbCCDType.Items.Add(enmCCDType.KeyenceCV200CTCP & " Keyence CV200C TCP")
        cmbCCDType.Items.Add(enmCCDType.CognexVPRO & " Cognex VPRO")
        'cmbCCDType.Items.Add(enmCCDType.Halcon & " Halcon V12")

        cmbCCD.Items.Clear()
        Select Case gSSystemParameter.MachineType
            Case enmMachineType.DCSW_800AQ
                cmbCCD.Items.Add("CCD1")
                cmbCCD.Items.Add("CCD2")
                cmbCCD.Items.Add("CCD3")
                cmbCCD.Items.Add("CCD4")
                cmbCCD.SelectedIndex = 0
                cmbCCD.Visible = True
                lblSelectCCD.Visible = True
            Case enmMachineType.eDTS_2S2V, enmMachineType.DCS_500AD
                cmbCCD.Items.Add("CCD1")
                cmbCCD.Items.Add("CCD2")
                cmbCCD.SelectedIndex = 0
                cmbCCD.Visible = True
                lblSelectCCD.Visible = True
            Case Else
                cmbCCD.Items.Add("CCD1")
                cmbCCD.SelectedIndex = 0
                cmbCCD.Visible = False
                lblSelectCCD.Visible = False
        End Select

        Cognex_InitializeAcquisition() 'Cognex設定
        'Eason 20170209 Ticket:100060 :Memory Log
        mAcqFifoTool.GarbageCollectionEnabled = True

        gMeEventLog.Log("[EVENT] ," & Reflection.MethodBase.GetCurrentMethod().Name)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[frmSetCCDConnection]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Hide()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        gSyslog.Save("[frmSetCCDConnection]" & vbTab & "[btnOK]" & vbTab & "Click")

        'If EditParameter(cmbCCD.SelectedIndex).CCDType = enmCCDType.CognexVPRO Then
        '    Dim fileName = Application.StartupPath & "\System\" & MachineName & "\" & lblFileName.Text & ".vpp"
        '    Debug.WriteLine("fileName: " & fileName)
        '    CogSerializer.SaveObjectToFile(mAcqFifoTool, fileName)
        'End If

        Try
            gAOICollection.ArrayCopyFrom(EditParameter)
            gAOICollection.SaveCCDConnectionParameter(Application.StartupPath & "\System\" & MachineName & "\CardCCD.ini") '儲存程控光源設定值
            '儲存完成,請重啟程式
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000051))
            MsgBox(gMsgHandler.GetMessage(Warn_3000051), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            '20161010
            'Me.Hide()
        Catch ex As Exception
            '存檔失敗 
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000035))
            gSyslog.Save("Exception Message: " & ex.Message)
            MsgBox(gMsgHandler.GetMessage(Warn_3000035),MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try
    End Sub

    Private Sub cmbCCDType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCCDType.SelectedIndexChanged
        If cmbCCD.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        Select Case cmbCCDType.SelectedIndex
            Case -1
            Case enmCCDType.None '0 'None
                palOmron.Visible = False
                palCongex.Visible = False
                'palHalcon.Visible = False
                'txtSendIP.Visible = False
                'txtSendPort.Visible = False
                'txtRecieveIP.Visible = False
                'txtRecievePort.Visible = False
            Case enmCCDType.OmronFZS2MUDP '1 'FZS2M UDP
                palOmron.Location = New Point(10, 130)
                palOmron.Visible = True
                palCongex.Visible = False
                'palHalcon.Visible = False
                'txtSendIP.Visible = True
                'txtSendPort.Visible = True
                'txtRecieveIP.Visible = True
                'txtRecievePort.Visible = True
            Case enmCCDType.OmronFZS2MTCP '2 'FZS2M TCP
                palOmron.Location = New Point(10, 130)
                palOmron.Visible = True
                palCongex.Visible = False
                'palHalcon.Visible = False
                'txtSendIP.Visible = True
                'txtSendPort.Visible = True
                'txtRecieveIP.Visible = True
                'txtRecievePort.Visible = True
            Case enmCCDType.KeyenceCV200CTCP '3 'Keyence CV200C TCP
                palOmron.Location = New Point(10, 130)
                palOmron.Visible = True
                palCongex.Visible = False
                'palHalcon.Visible = False
                'txtSendIP.Visible = True
                'txtSendPort.Visible = True
                'txtRecieveIP.Visible = True
                'txtRecievePort.Visible = True
            Case enmCCDType.CognexVPRO '4 'Cognex VPRO
                palOmron.Visible = False
                palOmron.Location = New Point(10, 130)
                palCongex.Visible = True
                btn_Run.Visible = False
                CogDisplay1.Visible = False
                'palHalcon.Visible = False
                'txtSendIP.Visible = False
                'txtSendPort.Visible = False
                'txtRecieveIP.Visible = False
                'txtRecievePort.Visible = False
            Case enmCCDType.Halcon '5 'Halcon
                palOmron.Visible = False
                palCongex.Visible = False
                'palHalcon.Location = New Point(10, 130)
                'palHalcon.Visible = True
        End Select
        EditParameter(cmbCCD.SelectedIndex).CCDType = cmbCCDType.SelectedIndex '將操作變更暫存至編輯區
    End Sub

    Private Sub txtSendIP_TextChanged(sender As Object, e As EventArgs) Handles txtSendIP.TextChanged
        If cmbCCD.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            Exit Sub
        End If
        EditParameter(cmbCCD.SelectedIndex).Omron.SendIPAddress = txtSendIP.Text '將操作變更暫存至編輯區
    End Sub

    Private Sub txtSendPort_TextChanged(sender As Object, e As EventArgs) Handles txtSendPort.TextChanged
        If cmbCCD.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        EditParameter(cmbCCD.SelectedIndex).Omron.SendPort = txtSendPort.Text '將操作變更暫存至編輯區
    End Sub

    Private Sub txtRecieveIP_TextChanged(sender As Object, e As EventArgs) Handles txtRecieveIP.TextChanged
        If cmbCCD.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        EditParameter(cmbCCD.SelectedIndex).Omron.RecieveIPAddress = txtRecieveIP.Text '將操作變更暫存至編輯區
    End Sub


    Private Sub txtRecievePort_TextChanged(sender As Object, e As EventArgs) Handles txtRecievePort.TextChanged
        If cmbCCD.SelectedIndex < 0 Then
            '請先選擇CCD
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000052))
            MsgBox(gMsgHandler.GetMessage(Warn_3000052), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        EditParameter(cmbCCD.SelectedIndex).Omron.RecievePort = txtRecievePort.Text '將操作變更暫存至編輯區

    End Sub




    Private Sub cmbTriggerType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTriggerType.SelectedIndexChanged
        EditParameter(cmbCCD.SelectedIndex).Cognex.TriggerType = cmbTriggerType.SelectedIndex
        ToolTip1.SetToolTip(cmbTriggerType, cmbTriggerType.SelectedItem)
    End Sub


    Private mFrameGrabber As Cognex.VisionPro.ICogFrameGrabber = Nothing
    Private mFrameGrabbers As New CogFrameGrabbers
    Private Sub Cognex_InitializeAcquisition()
        Dim DeviceString As String
        cmbCCD_SN.Items.Clear()
        cmbCCD_SN.Text = ""
        cmbVideoType.Items.Clear()
        cmbVideoType.Text = ""

        If mFrameGrabbers.Count < 1 Then
            MsgBox("No frame grabbers found", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        For i As Integer = 0 To mFrameGrabbers.Count - 1
            DeviceString = mFrameGrabbers(i).SerialNumber
            'Debug.WriteLine("Name: " & mFrameGrabbers(i).Name)
            'Debug.WriteLine("SerialNumber: " & mFrameGrabbers(i).SerialNumber)
            cmbCCD_SN.Items.Add(DeviceString)
        Next
        For i As Integer = 0 To mFrameGrabbers.Count - 1
            If EditParameter(cmbCCD.SelectedIndex).Cognex.SerialNumber = mFrameGrabbers(i).SerialNumber Then
                mFrameGrabber = mFrameGrabbers(i)
                DeviceNum = i
                cmbCCD_SN.Text = mFrameGrabber.SerialNumber 'mFrameGrabber.Name
                'lblCCDSN.Text = mFrameGrabber.SerialNumber
                'cmbVideoType.Text = DeviceString
                For j As Integer = 0 To mFrameGrabber.AvailableVideoFormats.Count - 1
                    If EditParameter(cmbCCD.SelectedIndex).Cognex.VideoFormatType = TransformCognexVideoStrToNum(mFrameGrabber.AvailableVideoFormats(j)) Then
                        cmbVideoType.Text = TransformVideoNumToStr(EditParameter(cmbCCD.SelectedIndex).Cognex.VideoFormatType)
                    End If
                Next
                Exit For
            End If
        Next

    End Sub


    Private numAcqs As Integer = 0
    'Private mAcqFifo As Cognex.VisionPro.ICogAcqFifo = Nothing
    Private mAcqFifoTool As New CogAcqFifoTool
    Private Sub cmbVideoType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVideoType.SelectedIndexChanged
        If cmbVideoType.SelectedIndex < 0 Then
            '請先選擇CCD裝置
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000053))
            MsgBox(gMsgHandler.GetMessage(Warn_3000053), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        Dim videoFormat As String '= cmbVideoType.SelectedItem.ToString()
        cmbVideoType.Text = cmbVideoType.SelectedItem.ToString
        videoFormat = TransformVideoNumToCognexStr(cmbVideoType.SelectedIndex)

        Try
            'mAcqFifo = mFrameGrabber.CreateAcqFifo(videoFormat, CogAcqFifoPixelFormatConstants.Format8Grey, 0, True)
            'mAcqFifoTool.Operator.FrameGrabber.CreateAcqFifo(videoFormat, CogAcqFifoPixelFormatConstants.Format8Grey, 0, True)
            mAcqFifoTool.Operator = mFrameGrabber.CreateAcqFifo(videoFormat, CogAcqFifoPixelFormatConstants.Format8Grey, 0, True)
        Catch ex As Exception
            gSyslog.Save("Exception Message: " & ex.Message)
            MsgBox("ccd HardwareNotInit", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try

        EditParameter(cmbCCD.SelectedIndex).Cognex.VideoFormatType = cmbVideoType.SelectedIndex
        ToolTip1.SetToolTip(cmbVideoType, cmbVideoType.SelectedItem)
        'If cmbVideoType.Text = "Bayer" Then
        '    cmbAWB.Enabled = True
        'Else
        '    cmbAWB.SelectedIndex = 0
        '    cmbAWB.Enabled = False
        'End If
        btn_Run.Enabled = True
    End Sub



    Private Sub btn_Run_Click(sender As Object, e As EventArgs) Handles btn_Run.Click
        Dim trignum As Integer
        CogDisplay1.Image = mAcqFifoTool.Operator.Acquire(trignum)
        numAcqs += 1
        Debug.WriteLine("numAcqs:" & numAcqs)
        If numAcqs > 4 Then
            GC.Collect()
            numAcqs = 0
        End If
    End Sub



    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        btn_Run.Visible = True
        CogDisplay1.Visible = True
    End Sub


    'Private Sub cmbAWB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAWB.SelectedIndexChanged
    '    EditParameter(cmbCCD.SelectedIndex).Cognex.AutoWhiteBalance = cmbAWB.SelectedIndex
    'End Sub

    Private Sub cmbCCD_SN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCCD_SN.SelectedIndexChanged
        If cmbCCD_SN.SelectedIndex < 0 Then
            '請先選擇CCD SN
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000054))
            MsgBox(gMsgHandler.GetMessage(Warn_3000054), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If


        If iniflag Then
            mFrameGrabber = mFrameGrabbers(DeviceNum)
            iniflag = False
        Else
            mFrameGrabber = mFrameGrabbers(cmbCCD_SN.SelectedIndex) 'issue
        End If

        cmbCCD_SN.Text = mFrameGrabber.SerialNumber
        EditParameter(cmbCCD.SelectedIndex).Cognex.SerialNumber = mFrameGrabber.SerialNumber

        cmbVideoType.Items.Clear()

        Try
            If mFrameGrabber.AvailableVideoFormats.Count > 1 Then
                Dim j As Integer
                Dim VideoFormat As Integer
                For j = mFrameGrabber.AvailableVideoFormats.Count - 1 To 0 Step -1
                    VideoFormat = TransformCognexVideoStrToNum(mFrameGrabber.AvailableVideoFormats(j))
                    If VideoFormat = enmVideoFormatType.Mono Then
                        cmbVideoType.Items.Add("Mono") '0
                    End If
                    If VideoFormat = enmVideoFormatType.Bayer Then
                        cmbVideoType.Items.Add("Bayer") '1
                    End If
                    'Debug.WriteLine("AvailableVideoFormats:  " & mFrameGrabber.AvailableVideoFormats(j))
                    ToolTip1.SetToolTip(cmbCCD_SN, cmbCCD_SN.SelectedItem)
                Next
            Else
                MsgBox("frame grabbers error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        Catch ex As Exception

            gSyslog.Save("Exception Message: " & ex.Message)
            MsgBox("ccd HardwareNotInit", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try

        btn_Run.Enabled = False

    End Sub
End Class
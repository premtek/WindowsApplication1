
Imports ProjectCore


Public Class frmTest
    Dim AutoRun As Integer = 1000
    Dim bAutoStart As Boolean
    Dim Device As cls800AQLul.enmDevice = cls800AQLul.enmDevice.Loader

    ReadOnly Property IsLULBusy As Boolean
        Get
            If Device = cls800AQLul.enmDevice.Loader Then
                Return cls800AQ_LUL.IsLoaderBusy
            Else
                Return cls800AQ_LUL.IsUnloaderBusy
            End If
        End Get
    End Property

    Private Sub frmTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        timerCheckIO.Start()
    End Sub

    Private Sub timerCheckIO_Tick(sender As Object, e As EventArgs) Handles timerCheckIO.Tick

        LoaderUpdate()
        UnloaderUpdate()

        If (bAutoStart) Then
            Select Case AutoRun
                Case 1000
                    gSYS(eSys.Conveyor1).Command = eSysCommand.LoadB
                    AutoRun = 1100
                Case 1100
                    If (gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.LoadB AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish) Then
                        AutoRun = 2000
                    End If

                Case 2000
                    gSYS(eSys.Conveyor1).Command = eSysCommand.LoadA
                    AutoRun = 2100

                Case 2100
                    If (gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.LoadA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish) Then
                        AutoRun = 3000
                    End If

                Case 3000
                    gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadB
                    AutoRun = 3100

                Case 3100
                    If (gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.UnloadB AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish) Then
                        AutoRun = 4000
                    End If

                Case 4000
                    gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadA
                    AutoRun = 4100

                Case 4100
                    If (gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.UnloadA AndAlso gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish) Then
                        AutoRun = 1000
                    End If
            End Select
        End If

    End Sub

    Private Sub LoaderUpdate()
        ' CvSMEMA(0) IO 
        picLD1.Image = imgListItems.Images(IIf(cls800AQ_LUL.Loader.IsAlarm, 0, 1))
        picLD2.Image = imgListItems.Images(IIf(CvSMEMA(0).IsLoaderReady, 0, 1))
        picLD3.Image = imgListItems.Images(IIf(CvSMEMA(0).IsReadyToRecieve, 0, 1))
        picLD4.Image = imgListItems.Images(IIf(cls800AQ_LUL.Loader.IsCaseteBarCodeReady, 0, 1))
        picLD5.Image = imgListItems.Images(IIf(cls800AQ_LUL.Loader.IsCaseteBarCodeReceiveFinish, 0, 1))
        picLD6.Image = imgListItems.Images(IIf(cls800AQ_LUL.Loader.IsMappingFinish, 0, 1))
        picLD7.Image = imgListItems.Images(IIf(cls800AQ_LUL.Loader.IsMappingReceiveFinish, 0, 1))
        picLD8.Image = imgListItems.Images(IIf(cls800AQ_LUL.Loader.IsRecipeChange, 0, 1))
        picLD9.Image = imgListItems.Images(IIf(cls800AQ_LUL.Loader.IsRecipeChangeFinish, 0, 1))
        picLD10.Image = imgListItems.Images(IIf(cls800AQ_LUL.Loader.IsCaseteAbort, 0, 1))
        picLD11.Image = imgListItems.Images(IIf(cls800AQ_LUL.Loader.IsCaseteAbortFinish, 0, 1))
        picLD12.Image = imgListItems.Images(IIf(cls800AQ_LUL.Loader.IsAMachineAlarm, 0, 1))

        'Alarm Code
        tbLDAlarmCode1.Text = mGlobalPool.cls800AQ_LUL.LoaderData.AlarmCodes(0)
        tbLDAlarmCode2.Text = mGlobalPool.cls800AQ_LUL.LoaderData.AlarmCodes(1)
        tbLDAlarmCode3.Text = mGlobalPool.cls800AQ_LUL.LoaderData.AlarmCodes(2)
        tbLDAlarmCode4.Text = mGlobalPool.cls800AQ_LUL.LoaderData.AlarmCodes(3)
        tbLDAlarmCode5.Text = mGlobalPool.cls800AQ_LUL.LoaderData.AlarmCodes(4)

        'Slot Status
        Dim strStatusA As String = ""
        For i = 0 To mGlobalPool.cls800AQ_LUL.LoaderData.SlotStatusA.Length - 1
            strStatusA = strStatusA & mGlobalPool.cls800AQ_LUL.LoaderData.SlotStatusA(i) & " , "
        Next
        tbLDSlotStatus1.Text = strStatusA

        Dim strStatusB As String = ""
        For i = 0 To mGlobalPool.cls800AQ_LUL.LoaderData.SlotStatusB.Length - 1
            strStatusB = strStatusB & mGlobalPool.cls800AQ_LUL.LoaderData.SlotStatusB(i) & " , "
        Next
        tbLDSlotStatus2.Text = strStatusB

        'Hot plate Temperatures
        tbLDTemp1.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Temperatures(0)
        tbLDTemp2.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Temperatures(1)
        tbLDTemp3.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Temperatures(2)
        tbLDTemp4.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Temperatures(3)
        tbLDTemp5.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Temperatures(4)
        tbLDTemp6.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Temperatures(5)
        tbLDTemp7.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Temperatures(6)
        tbLDTemp8.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Temperatures(7)
        tbLDTemp9.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Temperatures(8)
        tbLDTemp10.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Temperatures(9)
        tbLDTemp11.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Temperatures(10)
        tbLDTemp12.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Temperatures(11)

        'Other Data
        tbLDStatus.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Status
        tbLDProductNumA.Text = mGlobalPool.cls800AQ_LUL.A_ProductNum
        tbLDProductNumB.Text = mGlobalPool.cls800AQ_LUL.B_ProductNum
        tbLDProductType.Text = mGlobalPool.cls800AQ_LUL.LoaderData.ProductType
        tbLDProductCount.Text = mGlobalPool.cls800AQ_LUL.LoaderData.ProductCountA
        tbLDCaseteBarCode.Text = mGlobalPool.cls800AQ_LUL.LoaderData.CaseteBarCode
        tbLDPass.Text = mGlobalPool.cls800AQ_LUL.LoaderData.Pass
        tbLDTargetTemp.Text = mGlobalPool.cls800AQ_LUL.LoaderData.TargetTemp
    End Sub

    Private Sub UnloaderUpdate()
        ' CvSMEMA(0) IO 
        picUL1.Image = imgListItems.Images(IIf(cls800AQ_LUL.Unloader.IsAlarm, 0, 1))
        picUL2.Image = imgListItems.Images(IIf(CvSMEMA(0).IsUnloaderReady, 0, 1))
        picUL3.Image = imgListItems.Images(IIf(CvSMEMA(0).IsReadyToSend, 0, 1))
        picUL4.Image = imgListItems.Images(IIf(cls800AQ_LUL.Unloader.IsMappingFinish, 0, 1))
        picUL5.Image = imgListItems.Images(IIf(cls800AQ_LUL.Unloader.IsMappingReceiveFinish, 0, 1))
        picUL6.Image = imgListItems.Images(IIf(cls800AQ_LUL.Unloader.IsRecipeChange, 0, 1))
        picUL7.Image = imgListItems.Images(IIf(cls800AQ_LUL.Unloader.IsRecipeChangeFinish, 0, 1))
        picUL8.Image = imgListItems.Images(IIf(cls800AQ_LUL.Unloader.IsCaseteAbort, 0, 1))
        picUL9.Image = imgListItems.Images(IIf(cls800AQ_LUL.Unloader.IsCaseteAbortFinish, 0, 1))
        picUL10.Image = imgListItems.Images(IIf(cls800AQ_LUL.Unloader.IsBMachineAlarm, 0, 1))

        'Alarm Code
        tbULAlarmCode1.Text = cls800AQ_LUL.UnloaderData.AlarmCodes(0)
        tbULAlarmCode2.Text = cls800AQ_LUL.UnloaderData.AlarmCodes(1)
        tbULAlarmCode3.Text = cls800AQ_LUL.UnloaderData.AlarmCodes(2)
        tbULAlarmCode4.Text = cls800AQ_LUL.UnloaderData.AlarmCodes(3)
        tbULAlarmCode5.Text = cls800AQ_LUL.UnloaderData.AlarmCodes(4)

        'Slot Status
        Dim strStatusA As String = ""
        For i = 0 To mGlobalPool.cls800AQ_LUL.UnloaderData.SlotStatusA.Length - 1
            strStatusA = strStatusA & mGlobalPool.cls800AQ_LUL.UnloaderData.SlotStatusA(i) & " , "
        Next
        tbULSlotStatus1.Text = strStatusA

        Dim strStatusB As String = ""
        For i = 0 To mGlobalPool.cls800AQ_LUL.UnloaderData.SlotStatusB.Length - 1
            strStatusB = strStatusB & mGlobalPool.cls800AQ_LUL.UnloaderData.SlotStatusB(i) & " , "
        Next
        tbULSlotStatus2.Text = strStatusB

        'Hot plate Temperatures
        tbULTemp1.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Temperatures(0)
        tbULTemp2.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Temperatures(1)
        tbULTemp3.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Temperatures(2)
        tbULTemp4.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Temperatures(3)
        tbULTemp5.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Temperatures(4)
        tbULTemp6.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Temperatures(5)
        tbULTemp7.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Temperatures(6)
        tbULTemp8.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Temperatures(7)
        tbULTemp9.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Temperatures(8)
        tbULTemp10.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Temperatures(9)
        tbULTemp11.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Temperatures(10)
        tbULTemp12.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Temperatures(11)

        'Other Data
        tbULStatus.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Status
        tbULProductNum.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.ProductNum
        tbULProductType.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.ProductType
        tbULProductCount.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.ProductCountA
        tbULCaseteBarCode.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.CaseteBarCode
        tbULPass.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.Pass
        tbULTargetTemp.Text = mGlobalPool.cls800AQ_LUL.UnloaderData.TargetTemp
    End Sub

#Region "Machine A"

    Private Sub btnAInitial_Click(sender As Object, e As EventArgs) Handles btnAInitial.Click
        'mGlobalPool.Conveyor.Motion(clsDTSConveyor.enmMotion.A_Initial)
        gSYS(eSys.Conveyor1).Command = eSysCommand.HomeA
    End Sub

    Private Sub btnALoad_Click(sender As Object, e As EventArgs) Handles btnALoad.Click
        'mGlobalPool.Conveyor.Motion(clsDTSConveyor.enmMotion.A_Load, chkbAuto.Checked)
        gSSystemParameter.PassLUL = Not chkbAuto.Checked
        gSYS(eSys.Conveyor1).Command = eSysCommand.LoadA
    End Sub

    Private Sub btnAUnload_Click(sender As Object, e As EventArgs) Handles btnAUnload.Click
        'mGlobalPool.Conveyor.Motion(clsDTSConveyor.enmMotion.A_Unload, chkbAuto.Checked)
        gSSystemParameter.PassLUL = Not chkbAuto.Checked
        gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadA
    End Sub

#End Region

#Region "Machine B"

    Private Sub btnBInitial_Click(sender As Object, e As EventArgs) Handles btnBInitial.Click
        'mGlobalPool.Conveyor.Motion(clsDTSConveyor.enmMotion.B_Initial)
        gSYS(eSys.Conveyor1).Command = eSysCommand.HomeB
    End Sub

    Private Sub btnBLoad_Click(sender As Object, e As EventArgs) Handles btnBLoad.Click
        'mGlobalPool.Conveyor.Motion(clsDTSConveyor.enmMotion.B_Load, chkbAuto.Checked)
        gSSystemParameter.PassLUL = Not chkbAuto.Checked
        gSYS(eSys.Conveyor1).Command = eSysCommand.LoadB
    End Sub

    Private Sub btnBUnload_Click(sender As Object, e As EventArgs) Handles btnBUnload.Click
        'mGlobalPool.Conveyor.Motion(clsDTSConveyor.enmMotion.B_Unload, chkbAuto.Checked)
        gSSystemParameter.PassLUL = Not chkbAuto.Checked
        gSYS(eSys.Conveyor1).Command = eSysCommand.UnloadB
    End Sub

#End Region

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        gSYS(eSys.Conveyor1).Command = eSysCommand.None
        gSYS(eSys.Conveyor1).ExecuteCommand = eSysCommand.None
        gSYS(eSys.Conveyor1).RunStatus = eSysCommand.None
    End Sub

    Private Sub rbLoader_CheckedChanged(sender As Object, e As EventArgs) Handles rbLoader.CheckedChanged
        If (rbLoader.Checked) Then
            Device = cls800AQLul.enmDevice.Loader

            btnSetPassModel.Enabled = True
            btnSetProductType.Enabled = True
            btnSetTargetTemp.Enabled = True
            btnGetCastetData.Enabled = True
            btnGetCstBarCode.Enabled = True
            btnGetProductNum.Enabled = True
            btnSetProductNum.Enabled = False
            btnCassetteAbort.Enabled = True
            btnGetAlarmCode.Enabled = True
        End If
    End Sub

    Private Sub rbUnloader_CheckedChanged(sender As Object, e As EventArgs) Handles rbUnloader.CheckedChanged
        If (rbUnloader.Checked) Then
            Device = cls800AQLul.enmDevice.Unloader

            btnSetPassModel.Enabled = False
            btnSetProductType.Enabled = True
            btnSetTargetTemp.Enabled = True
            btnGetCastetData.Enabled = True
            btnGetCstBarCode.Enabled = False
            btnGetProductNum.Enabled = False
            btnSetProductNum.Enabled = True
            btnCassetteAbort.Enabled = True
            btnGetAlarmCode.Enabled = True
        End If
    End Sub



    Private Sub btnGetPassModel_Click(sender As Object, e As EventArgs)
        mGlobalPool.cls800AQ_LUL.GetMachineStatus(Device)
        mGlobalPool.cls800AQ_LUL.SetPassModel(True)
        mGlobalPool.cls800AQ_LUL.SetProductType(0, Device)
        mGlobalPool.cls800AQ_LUL.GetTempreature(Device)
        mGlobalPool.cls800AQ_LUL.SetTargetTemp(100, Device)
        mGlobalPool.cls800AQ_LUL.GetCastetData(Device)
        mGlobalPool.cls800AQ_LUL.GetCaseteBarCode()
        'mGlobalPool.Conveyor.SetCaseteBarCode("ABCDEFGHIJKLMNOPQRST")
        mGlobalPool.cls800AQ_LUL.GetProductNum(Device)
        mGlobalPool.cls800AQ_LUL.SetProductNum(100, Device)
        mGlobalPool.cls800AQ_LUL.CassetteAbort(Device)
        mGlobalPool.cls800AQ_LUL.GetAlarmCode(Device)
    End Sub


    Private Sub btnSetPassModel_Click(sender As Object, e As EventArgs) Handles btnSetPassModel.Click
        If (IsLULBusy = False) Then
            If (cls800AQ_LUL.SetPassModel(chkbPass.Checked) = False) Then
                MsgBox("Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        Else
            MsgBox(Device.ToString() & " Busy", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    Private Sub btnSetProductType_Click(sender As Object, e As EventArgs) Handles btnSetProductType.Click
        If (IsLULBusy = False) Then
            If (cls800AQ_LUL.SetProductType(Convert.ToInt32(tbSetProductType.Text), Device) = False) Then
                MsgBox("Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        Else
            MsgBox(Device.ToString() & " Busy", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    Private Sub btnSetTargetTemp_Click(sender As Object, e As EventArgs) Handles btnSetTargetTemp.Click
        If (IsLULBusy = False) Then
            If (cls800AQ_LUL.SetTargetTemp(Convert.ToInt32(tbSetTargetTemp.Text) * 10, Device) = False) Then
                MsgBox("Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        Else
            MsgBox(Device.ToString() & " Busy", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    Private Sub btnGetCastetData_Click(sender As Object, e As EventArgs) Handles btnGetCastetData.Click
        If (IsLULBusy = False) Then
            If (cls800AQ_LUL.GetCastetData(Device) = False) Then
                MsgBox("Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        Else
            MsgBox(Device.ToString() & " Busy", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    Private Sub btnGetCstBarCode_Click(sender As Object, e As EventArgs) Handles btnGetCstBarCode.Click
        If (IsLULBusy = False) Then
            If (cls800AQ_LUL.GetCaseteBarCode() = False) Then
                MsgBox("Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        Else
            MsgBox(Device.ToString() & " Busy", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    Private Sub btnGetProductNum_Click(sender As Object, e As EventArgs) Handles btnGetProductNum.Click
        If (IsLULBusy = False) Then
            If (cls800AQ_LUL.GetProductNum(Device) = False) Then
                MsgBox("Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        Else
            MsgBox(Device.ToString() & " Busy", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    Private Sub btnSetProductNum_Click(sender As Object, e As EventArgs) Handles btnSetProductNum.Click
        If (IsLULBusy = False) Then
            If (cls800AQ_LUL.SetProductNum(Convert.ToInt32(tbSetProductNum.Text), Device) = False) Then
                MsgBox("Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        Else
            MsgBox(Device.ToString() & " Busy", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    Private Sub btnCassetteAbort_Click(sender As Object, e As EventArgs) Handles btnCassetteAbort.Click
        If (IsLULBusy = False) Then
            If (cls800AQ_LUL.CassetteAbort(Device) = False) Then
                MsgBox("Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        Else
            MsgBox(Device.ToString() & " Busy", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    Private Sub btnGetAlarmCode_Click(sender As Object, e As EventArgs) Handles btnGetAlarmCode.Click
        If (IsLULBusy = False) Then
            If (cls800AQ_LUL.GetAlarmCode(Device) = False) Then
                MsgBox("Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        Else
            MsgBox(Device.ToString() & " Busy", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

    Private Sub btnSetLastNum_Click(sender As Object, e As EventArgs) Handles btnSetLastNum.Click
        If (IsLULBusy = False) Then
            If (cls800AQ_LUL.SetLastProductNum(333, 555) = False) Then
                MsgBox("Error", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        Else
            MsgBox(Device.ToString() & " Busy", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
    End Sub

#Region "Loader IO Control"

    Private Sub btnLdON1_Click(sender As Object, e As EventArgs) Handles btnLdON1.Click
        CvSMEMA(0).IsReadyToRecieve = True
    End Sub

    Private Sub btnLdOFF1_Click(sender As Object, e As EventArgs) Handles btnLdOFF1.Click
        CvSMEMA(0).IsReadyToRecieve = False
    End Sub

    Private Sub btnLdON2_Click(sender As Object, e As EventArgs) Handles btnLdON2.Click
        cls800AQ_LUL.Loader.IsCaseteBarCodeReceiveFinish = True
    End Sub

    Private Sub btnLdOFF2_Click(sender As Object, e As EventArgs) Handles btnLdOFF2.Click
        cls800AQ_LUL.Loader.IsCaseteBarCodeReceiveFinish = False
    End Sub

    Private Sub btnLdON3_Click(sender As Object, e As EventArgs) Handles btnLdON3.Click
        cls800AQ_LUL.Loader.IsMappingReceiveFinish = True
    End Sub

    Private Sub btnLdOFF3_Click(sender As Object, e As EventArgs) Handles btnLdOFF3.Click
        cls800AQ_LUL.Loader.IsMappingReceiveFinish = False
    End Sub

    Private Sub btnLdON4_Click(sender As Object, e As EventArgs) Handles btnLdON4.Click
        cls800AQ_LUL.Loader.IsRecipeChange = True
    End Sub

    Private Sub btnLdOFF4_Click(sender As Object, e As EventArgs) Handles btnLdOFF4.Click
        cls800AQ_LUL.Loader.IsRecipeChange = False
    End Sub

    Private Sub btnLdON5_Click(sender As Object, e As EventArgs) Handles btnLdON5.Click
        cls800AQ_LUL.Loader.IsCaseteAbort = True
    End Sub

    Private Sub btnLdOFF5_Click(sender As Object, e As EventArgs) Handles btnLdOFF5.Click
        cls800AQ_LUL.Loader.IsCaseteAbort = False
    End Sub

    Private Sub btnLdON6_Click(sender As Object, e As EventArgs) Handles btnLdON6.Click
        cls800AQ_LUL.Loader.IsAMachineAlarm = True
    End Sub

    Private Sub btnLdOFF6_Click(sender As Object, e As EventArgs) Handles btnLdOFF6.Click
        cls800AQ_LUL.Loader.IsAMachineAlarm = False
    End Sub

#End Region

#Region "Unloader IO Control"

    Private Sub btnUlON1_Click(sender As Object, e As EventArgs) Handles btnUlON1.Click
        mGlobalPool.CvSMEMA(0).IsReadyToSend = True
    End Sub

    Private Sub btnUlOFF1_Click(sender As Object, e As EventArgs) Handles btnUlOFF1.Click
        mGlobalPool.CvSMEMA(0).IsReadyToSend = False
    End Sub

    Private Sub btnUlON2_Click(sender As Object, e As EventArgs) Handles btnUlON2.Click
        cls800AQ_LUL.Unloader.IsMappingReceiveFinish = True
    End Sub

    Private Sub btnUlOFF2_Click(sender As Object, e As EventArgs) Handles btnUlOFF2.Click
        cls800AQ_LUL.Unloader.IsMappingReceiveFinish = False
    End Sub

    Private Sub btnUlON3_Click(sender As Object, e As EventArgs) Handles btnUlON3.Click
        cls800AQ_LUL.Unloader.IsRecipeChange = True
    End Sub

    Private Sub btnUlOFF3_Click(sender As Object, e As EventArgs) Handles btnUlOFF3.Click
        cls800AQ_LUL.Unloader.IsRecipeChange = False
    End Sub

    Private Sub btnUlON4_Click(sender As Object, e As EventArgs) Handles btnUlON4.Click
        cls800AQ_LUL.Unloader.IsCaseteAbort = True
    End Sub

    Private Sub btnUlOFF4_Click(sender As Object, e As EventArgs) Handles btnUlOFF4.Click
        cls800AQ_LUL.Unloader.IsCaseteAbort = False
    End Sub

    Private Sub btnUlON5_Click(sender As Object, e As EventArgs) Handles btnUlON5.Click
        cls800AQ_LUL.Unloader.IsBMachineAlarm = True
    End Sub

    Private Sub btnUlOFF5_Click(sender As Object, e As EventArgs) Handles btnUlOFF5.Click
        cls800AQ_LUL.Unloader.IsBMachineAlarm = False
    End Sub

#End Region

    Private Sub Button1_Click(sender As Object, e As EventArgs)

        gSYS(eSys.Conveyor1).Command = eSysCommand.LoadA

        gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.None

        gSYS(eSys.Conveyor1).RunStatus = enmRunStatus.Finish

    End Sub

    Private Sub frmTest_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        timerCheckIO.Stop()
    End Sub

    Private Sub btnAuto_Click(sender As Object, e As EventArgs) Handles btnAuto.Click
        gSSystemParameter.PassLUL = False
        If (bAutoStart) Then
            bAutoStart = False
            btnAuto.Text = "Start"
        Else
            bAutoStart = True
            btnAuto.Text = "Stop"
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If (bAutoStart = False) Then
            AutoRun = 1000
        Else
            '請先停止 Auto Run
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2080023))
            MsgBox(gMsgHandler.GetMessage(Alarm_2080023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("請先停止 Auto Run")
        End If
    End Sub

    
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
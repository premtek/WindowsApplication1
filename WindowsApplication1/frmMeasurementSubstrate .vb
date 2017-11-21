Imports ProjectCore
Imports ProjectLaserInterferometer
Imports ProjectMotion.MCommonMotion
Imports ProjectIO

Public Class MeasurementSubstrate
    Public Value() As Decimal
    Public sys As sSysParam


    Private Sub txtMeasureTimes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMeasureTimes.KeyPress
        Premtek.Base.CKeyPress.CheckUInteger(sender, e)
    End Sub

    Public Sub btnMeasure_Click(sender As Object, e As EventArgs) Handles btnMeasure.Click

        If btnMeasure.Enabled = False Then '防連按
            Exit Sub
        End If

        '20170602按鍵保護
        btnMeasure.Enabled = False
        UcJoyStick1.Enabled = False
        GroupBox2.Enabled = False
        btnBack.Enabled = False


        If txtMeasureTimes.Text = "" Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MessageBox.Show("Please check Times must > 0")
            btnMeasure.Enabled = True 'Soni 2017.06.26
            UcJoyStick1.Enabled = True 'Soni 2017.06.26
            GroupBox2.Enabled = True 'Soni 2017.06.26
            btnBack.Enabled = True 'Soni 2017.06.26
            Exit Sub
        ElseIf txtMeasureTimes.Text = 0 Then
            '輸入資料錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000016))
            MsgBox(gMsgHandler.GetMessage(Warn_3000016), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnMeasure.Enabled = True 'Soni 2017.06.26
            UcJoyStick1.Enabled = True 'Soni 2017.06.26
            GroupBox2.Enabled = True 'Soni 2017.06.26
            btnBack.Enabled = True 'Soni 2017.06.26
            Exit Sub
        Else
            ReDim Value(CInt(txtMeasureTimes.Text) - 1)
            Dim HeightValue As String = ""

            Select Case cboHeightDevice.SelectedItem.ToString
                Case "Contact"
                    gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, True)
                    Call gDOCollection.RefreshDO()
                    System.Threading.Thread.CurrentThread.Join(1000) '到位穩定時間

                    For i = 0 To Value.Length - 1
                        If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Contact", sys.StageNo, HeightValue, True) = False Then
                            '測高儀讀值失敗
                            Select Case sys.StageNo
                                Case enmStage.No1
                                    gSyslog.Save(gMsgHandler.GetMessage(Error_1014004), "Error_1014004", eMessageLevel.Error)
                                    MsgBox(gMsgHandler.GetMessage(Error_1014004), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmStage.No2
                                    gSyslog.Save(gMsgHandler.GetMessage(Error_1014104), "Error_1014104", eMessageLevel.Error)
                                    MsgBox(gMsgHandler.GetMessage(Error_1014104), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmStage.No3
                                    gSyslog.Save(gMsgHandler.GetMessage(Error_1014204), "Error_1014204", eMessageLevel.Error)
                                    MsgBox(gMsgHandler.GetMessage(Error_1014204), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmStage.No4
                                    gSyslog.Save(gMsgHandler.GetMessage(Error_1014304), "Error_1014304", eMessageLevel.Error)
                                    MsgBox(gMsgHandler.GetMessage(Error_1014304), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            End Select
                            'MsgBox("Laser Reader Failed!", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                            '20170602按鍵保護
                            btnMeasure.Enabled = True
                            UcJoyStick1.Enabled = True
                            GroupBox2.Enabled = True
                            btnBack.Enabled = True

                            Exit Sub
                        Else
                            Value(i) = CDec(HeightValue)
                        End If
                    Next

                    gDOCollection.SetState(enmDO.ContactHeightSolenoidValve, False)
                    Call gDOCollection.RefreshDO()
                    Measure(Value)
                    '20170602按鍵保護
                    btnMeasure.Enabled = True
                    UcJoyStick1.Enabled = True
                    GroupBox2.Enabled = True
                    btnBack.Enabled = True

                Case "Laser"

                    For i = 0 To Value.Length - 1
                        If ProjectLaserInterferometer.MCommonLaserReader.gLaserReaderCollection.GetValue("Laser", sys.StageNo, HeightValue, True) = False Then
                            '測高儀讀值失敗
                            Select Case sys.StageNo
                                Case enmStage.No1
                                    gSyslog.Save(gMsgHandler.GetMessage(Error_1014004), "Error_1014004", eMessageLevel.Error)
                                    MsgBox(gMsgHandler.GetMessage(Error_1014004), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmStage.No2
                                    gSyslog.Save(gMsgHandler.GetMessage(Error_1014104), "Error_1014104", eMessageLevel.Error)
                                    MsgBox(gMsgHandler.GetMessage(Error_1014104), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmStage.No3
                                    gSyslog.Save(gMsgHandler.GetMessage(Error_1014204), "Error_1014204", eMessageLevel.Error)
                                    MsgBox(gMsgHandler.GetMessage(Error_1014204), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                Case enmStage.No4
                                    gSyslog.Save(gMsgHandler.GetMessage(Error_1014304), "Error_1014304", eMessageLevel.Error)
                                    MsgBox(gMsgHandler.GetMessage(Error_1014304), MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            End Select
                            ' MsgBox("Read Date Failed!", MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                            '20170602按鍵保護
                            btnMeasure.Enabled = True
                            UcJoyStick1.Enabled = True
                            GroupBox2.Enabled = True
                            btnBack.Enabled = True
                            Exit Sub
                        Else
                            Value(i) = CDec(HeightValue)
                        End If
                    Next
                    Measure(Value)
                    '20170602按鍵保護
                    btnMeasure.Enabled = True
                    UcJoyStick1.Enabled = True
                    GroupBox2.Enabled = True
                    btnBack.Enabled = True
                Case Else
                    '請選擇測高模式
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000050))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000050), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MessageBox.Show("Plase Select Device Mode")
                    '20170602按鍵保護
                    btnMeasure.Enabled = True
                    UcJoyStick1.Enabled = True
                    GroupBox2.Enabled = True
                    btnBack.Enabled = True
            End Select
        End If


    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        'Sue20170627
        gSyslog.Save("[frmMeasurementSubstrate ]" & vbTab & "[btnBack]" & vbTab & "Click")
        UcJoyStick1.ManualDispose()
        Me.Close()
    End Sub

    Public Sub Measure(Value As Decimal())

        Dim Max As Decimal = Decimal.MinValue
        Dim Min As Decimal = Decimal.MaxValue
        Dim Average As Decimal
        Dim MaxS, MinS, AverageS As String

        Dim Sum As Decimal = 0
        Dim i As Integer

        For i = 0 To Value.Length - 1

            If Value(i) > Max Then
                Max = Value(i)
            End If
            If Value(i) < Min Then
                Min = Value(i)
            End If
            Sum = Sum + Value(i)


        Next

        Average = Sum / Value.Length

        MaxS = Max.ToString("0.0000")
        MinS = Min.ToString("0.0000")
        AverageS = Average.ToString("0.0000")

        txtMaximun.Text = MaxS
        txtMinimun.Text = MinS
        txtAverage.Text = AverageS

    End Sub

   Public Sub New()

        ' 此為設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
    End Sub


    Private Sub MeasurementSubstrate_Load(sender As Object, e As EventArgs) Handles Me.Load

        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按         
            UcJoyStick1.Enabled = False
        Else
            UcJoyStick1.Enabled = True
        End If

        'sys = SysNO
        UcJoyStick1.AxisX = sys.AxisX
        UcJoyStick1.AxisY = sys.AxisY
        UcJoyStick1.AxisZ = sys.AxisZ
        UcJoyStick1.AXisA = sys.AxisA
        UcJoyStick1.AXisB = sys.AxisB
        UcJoyStick1.AXisC = sys.AxisC
        UcJoyStick1.SetSpeedType(SpeedType.Slow)
        UcJoyStick1.RefreshPosition()

        If (gSSystemParameter.StageCount > 1) Then
            If (gSSystemParameter.MachineSafeData.Count > 0) Then
                UcJoyStick1.InverseAxisX.SafeDistance = gSSystemParameter.MachineSafeData(sys.MachineNo).SafeDistanceX
                UcJoyStick1.InverseAxisX.Spread = gSSystemParameter.MachineSafeData(sys.MachineNo).SpreadX

                If (sys.StageNo = enmStage.No1) Then
                    UcJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage2).AxisX    '對立軸
                    UcJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Posivtive
                ElseIf (sys.StageNo = enmStage.No2) Then
                    UcJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage1).AxisX    '對立軸
                    UcJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Negative
                ElseIf (sys.StageNo = enmStage.No3) Then
                    UcJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage4).AxisX    '對立軸
                    UcJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Posivtive
                ElseIf (sys.StageNo = enmStage.No4) Then
                    UcJoyStick1.InverseAxisX.Axis = gSYS(eSys.DispStage3).AxisX    '對立軸
                    UcJoyStick1.InverseAxisX.Direction = InverseAxis.enmDirection.Negative
                End If
            End If
        End If

        Select Case gSSystemParameter.MeasureType
            Case enmMeasureType.Contact
                cboHeightDevice.Items.Add("Contact")
            Case enmMeasureType.Laser
                cboHeightDevice.Items.Add("Laser")
            Case enmMeasureType.Both
                cboHeightDevice.Items.Add("Contact")
                cboHeightDevice.Items.Add("Laser")
        End Select
    End Sub
End Class
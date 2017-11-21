Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectAOI

Public Class frmCalibAlignTool
    Public Sys As sSysParam
    '''<summary>預選場景</summary>
    ''' <remarks></remarks>
    Public SceneName As String

    Dim ClassName As String = "frmCalibAlignTool"

    Private Sub frmCalibAlignTool_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        CogToolBlockEditV21.Dispose()
    End Sub


    Private Sub frmCalibAlignTool_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If SceneName = "" Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        Else
            lblSceneSet.Text = SceneName
        End If


        '限制光源通道最大輸出
        lightBar1.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No1)
        lightBar2.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No2)
        lightBar3.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No3)
        lightBar4.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No4)
        nmcLight1.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No1)
        nmcLight2.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No2)
        nmcLight3.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No3)
        nmcLight4.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No4)


        Dim fileName As String
        'SceneName = lstScene.SelectedItem
        fileName = Application.StartupPath & "\System\" & MachineName & "\" & lblSceneSet.Text & ".ini" 'System.IO.Path.GetDirectoryName(gCRecipe.strFileName) & "\" & SceneName & ".ini" '光源設定檔路徑
        gAOICollection.LoadSceneParameter(SceneName, fileName) '讀取光源,曝光值等設定

        SetnmcLightwithLightBar(nmcLight1, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No1), lightBar1)
        SetnmcLightwithLightBar(nmcLight2, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No2), lightBar2)
        SetnmcLightwithLightBar(nmcLight3, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No3), lightBar3)
        SetnmcLightwithLightBar(nmcLight4, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No4), lightBar4)

        chkLight1.Checked = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No1)
        chkLight2.Checked = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No2)
        chkLight3.Checked = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No3)
        chkLight4.Checked = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No4)

        btnSetLight1.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No1)
        btnSetLight2.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No2)
        btnSetLight3.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No3)
        btnSetLight4.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No4)

        'nmcExposure.Value = gAOICollection.SceneDictionary(SceneName).CCDExposureTime

        InvokeCogToolBLock(CogToolBlockEditV21, gAOICollection.GetToolBlock(Sys.CCDNo, lblSceneSet.Text))
        CogToolBlockEditV21.SuspendLayout()
        CogToolBlockEditV21.Subject = gAOICollection.GetToolBlock(Sys.CCDNo, lblSceneSet.Text)
        CogToolBlockEditV21.ResumeLayout()



    End Sub

    ''' <summary>設定數值,卡拉霸上下限</summary>
    ''' <param name="nmcLight"></param>
    ''' <param name="value"></param>
    ''' <param name="trackBar"></param>
    ''' <remarks></remarks>
    Sub SetnmcLightwithLightBar(ByRef nmcLight As NumericUpDown, ByVal value As Decimal, ByRef trackBar As TrackBar)
        If value > trackBar.Maximum Then
            nmcLight.Value = trackBar.Maximum
            Exit Sub
        End If
        If value < trackBar.Minimum Then
            nmcLight.Value = trackBar.Minimum
            Exit Sub
        End If
        nmcLight.Value = value
    End Sub

    ''' <summary>委派 控制項操作物件更新</summary>
    ''' <param name="ctrl">目標控制項</param>
    ''' <param name="str">物件</param>
    ''' <remarks></remarks>
    Private Delegate Sub ImInvokeCogToolBLock(ByVal ctrl As Cognex.VisionPro.ToolBlock.CogToolBlockEditV2, ByVal [str] As Cognex.VisionPro.ToolBlock.CogToolBlock)
    Public Sub InvokeCogToolBLock(ByVal ctrl As Cognex.VisionPro.ToolBlock.CogToolBlockEditV2, ByVal [str] As Cognex.VisionPro.ToolBlock.CogToolBlock)
        If ctrl.InvokeRequired Then
            Dim t As New ImInvokeCogToolBLock(AddressOf InvokeCogToolBLock)
            ctrl.Invoke(t, New Object() {ctrl, str})
        Else
            If str Is Nothing Then
                ctrl.Subject = Nothing
            Else
                ctrl.SuspendLayout()
                ctrl.Subject = str
                ctrl.ResumeLayout()
            End If
        End If

    End Sub


    Private Async Sub btnACQ_Click(sender As Object, e As EventArgs) Handles btnACQ.Click
        If btnACQ.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnACQ]" & vbTab & "Click")
        btnACQ.Enabled = False

        '20170602按鍵保護
        btnRun.Enabled = False
        btnRepeatRun.Enabled = False
        btnReset.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False

        If Not gAOICollection.IsCCDExist(Sys.CCDNo) Then
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnACQ.Enabled = True

            '20170602按鍵保護
            btnRun.Enabled = True
            btnRepeatRun.Enabled = True
            btnReset.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True


            Exit Sub
        End If

        Await Task.Run(Sub()

                           Try
                               If lblSceneSet.Text Is Nothing Then
                                   '請選擇場景
                                   gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
                                   MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnACQ.Enabled = True

                                                          '20170602按鍵保護
                                                          btnRun.Enabled = True
                                                          btnRepeatRun.Enabled = True
                                                          btnReset.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True

                                                      End Sub)
                                   End If

                                   Exit Sub
                               End If
                               gAOICollection.SetCCDScene(Sys.CCDNo, lblSceneSet.Text)
                               ' gAOICollection.SetExposure(Sys.CCDNo, nmcExposure.Value) '設定曝光值

                               gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
                               System.Threading.Thread.CurrentThread.Join(100)
                               gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照開  
                               System.Threading.Thread.CurrentThread.Join(100)
                               gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保

                               Dim stopWatch As New Stopwatch
                               stopWatch.Restart()
                               Do
                                   System.Threading.Thread.Sleep(1)
                                   If stopWatch.ElapsedMilliseconds > 1000 Then
                                       'CCD 取像TimeOut
                                       Select Case Sys.StageNo
                                           Case 0
                                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012003))
                                               MsgBox(gMsgHandler.GetMessage(Alarm_2012003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 1
                                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012303))
                                               MsgBox(gMsgHandler.GetMessage(Alarm_2012303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 2
                                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012603))
                                               MsgBox(gMsgHandler.GetMessage(Alarm_2012603), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 3
                                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012903))
                                               MsgBox(gMsgHandler.GetMessage(Alarm_2012903), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       End Select
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              btnACQ.Enabled = True

                                                              '20170602按鍵保護
                                                              btnRun.Enabled = True
                                                              btnRepeatRun.Enabled = True
                                                              btnReset.Enabled = True
                                                              btnOK.Enabled = True
                                                              btnCancel.Enabled = True

                                                          End Sub)
                                       End If
                                       Exit Sub
                                   End If
                                   '[Note] EMO時跳出
                                   If gDICollection.GetState(enmDI.EMO) = True Then
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              btnACQ.Enabled = True

                                                              '20170602按鍵保護
                                                              btnRun.Enabled = True
                                                              btnRepeatRun.Enabled = True
                                                              btnReset.Enabled = True
                                                              btnOK.Enabled = True
                                                              btnCancel.Enabled = True

                                                          End Sub)
                                       End If
                                       Exit Sub
                                   Else
                                       If Sys.MachineNo = enmMachineStation.MachineA Then
                                           If gDICollection.GetState(enmDI.EMS) = True Then
                                               '20170929 Toby_ Add 判斷
                                               If (Not IsNothing(Me)) Then
                                                   Me.BeginInvoke(Sub()
                                                                      btnACQ.Enabled = True

                                                                      '20170602按鍵保護
                                                                      btnRun.Enabled = True
                                                                      btnRepeatRun.Enabled = True
                                                                      btnReset.Enabled = True
                                                                      btnOK.Enabled = True
                                                                      btnCancel.Enabled = True

                                                                  End Sub)
                                               End If
                                               Exit Sub
                                           End If
                                       ElseIf Sys.MachineNo = enmMachineStation.MachineB Then
                                           If gDICollection.GetState(enmDI.EMS2) = True Then
                                               '20170929 Toby_ Add 判斷
                                               If (Not IsNothing(Me)) Then
                                                   Me.BeginInvoke(Sub()
                                                                      btnACQ.Enabled = True

                                                                      '20170602按鍵保護
                                                                      btnRun.Enabled = True
                                                                      btnRepeatRun.Enabled = True
                                                                      btnReset.Enabled = True
                                                                      btnOK.Enabled = True
                                                                      btnCancel.Enabled = True

                                                                  End Sub)
                                               End If
                                               Exit Sub
                                           End If
                                       End If
                                   End If
                               Loop Until gAOICollection.IsCCDCBusy(Sys.CCDNo) = False

                               'InvokeUcDisplay(UcDisplay1, gAOICollection, Sys, lstScene.SelectedItem) '更新控制項,必要條件 frmMain必須是實體

                               'TabControl1.SelectTab(TabAlignmentPatternRecognition) '切換到教導介面

                               If CogToolBlockEditV21.Subject Is Nothing Then
                                   '工具不存在
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000023))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnACQ.Enabled = True

                                                          '20170602按鍵保護
                                                          btnRun.Enabled = True
                                                          btnRepeatRun.Enabled = True
                                                          btnReset.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True

                                                      End Sub)
                                   End If
                                   Exit Sub
                               End If
                               If CogToolBlockEditV21.Subject.Inputs.Count = 0 Then
                                   '工具輸入不存在
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000024))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2000024), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnACQ.Enabled = True

                                                          '20170602按鍵保護
                                                          btnRun.Enabled = True
                                                          btnRepeatRun.Enabled = True
                                                          btnReset.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True

                                                      End Sub)
                                   End If
                                   Exit Sub
                               End If
                               If Not CogToolBlockEditV21.Subject.Inputs.Contains("InputImage") Then
                                   '工具輸入影像不存在
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000025))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnACQ.Enabled = True

                                                          '20170602按鍵保護
                                                          btnRun.Enabled = True
                                                          btnRepeatRun.Enabled = True
                                                          btnReset.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True

                                                      End Sub)
                                   End If
                                   Exit Sub
                               End If
                               CogToolBlockEditV21.Subject.Inputs("InputImage").Value = gAOICollection.CalibBoardCalibration(Sys.CCDNo, gAOICollection.GetAcqOutputImage(Sys.CCDNo), False, 0) '20170317Wenda gAOICollection.GetAcqOutputImage(Sys.CCDNo)

                               If lblSceneSet.Text Is Nothing Then '沒選擇不套用
                                   Exit Sub
                               End If

                               ' gAOICollection.SetSceneInputImage(Sys.CCDNo, SceneName, gAOICollection.GetAcqOutputImage(Sys.CCDNo))
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnACQ.Enabled = True

                                                      '20170602按鍵保護
                                                      btnRun.Enabled = True
                                                      btnRepeatRun.Enabled = True
                                                      btnReset.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True

                                                  End Sub)
                               End If

                           Catch ex As Exception
                               MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnACQ.Enabled = True

                                                      '20170602按鍵保護
                                                      btnRun.Enabled = True
                                                      btnRepeatRun.Enabled = True
                                                      btnReset.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True

                                                  End Sub)
                               End If
                           End Try
                       End Sub)

    End Sub



    Sub SetLightOnOff(ByVal light As enmLight, ByVal value As Boolean)
        Select Case light
            Case enmLight.No1
                gDOCollection.SetState(enmDO.CCDLight, value)
            Case enmLight.No2
                gDOCollection.SetState(enmDO.CCDLight2, value)
            Case enmLight.No3
                gDOCollection.SetState(enmDO.CCDLight3, value)
            Case enmLight.No4
                gDOCollection.SetState(enmDO.CCDLight4, value)
            Case enmLight.No5
                gDOCollection.SetState(enmDO.CCDLight5, value)
            Case enmLight.No6
                gDOCollection.SetState(enmDO.CCDLight6, value)
            Case enmLight.No7
                gDOCollection.SetState(enmDO.CCDLight7, value)
            Case enmLight.No8
                gDOCollection.SetState(enmDO.CCDLight8, value)
        End Select

    End Sub


    Private Sub btnSetLight1_Click(sender As Object, e As EventArgs) Handles btnSetLight1.Click
        If btnSetLight1.Enabled = False Then
            Exit Sub
        End If
        btnSetLight1.Enabled = False
        Dim light As enmLight = gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No1)
        gLightCollection.SetCCDLight(Sys.CCDNo, light, nmcLight1.Value, True)
        SetLightOnOff(light, True)
        btnSetLight1.Enabled = True
    End Sub

    Private Sub btnSetLight2_Click(sender As Object, e As EventArgs) Handles btnSetLight2.Click
        If btnSetLight2.Enabled = False Then
            Exit Sub
        End If
        btnSetLight2.Enabled = False
        Dim light As enmLight = gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No2)
        gLightCollection.SetCCDLight(Sys.CCDNo, light, nmcLight2.Value, True)
        SetLightOnOff(light, True)
        btnSetLight2.Enabled = True
    End Sub

    Private Sub btnSetLight3_Click(sender As Object, e As EventArgs) Handles btnSetLight3.Click
        If btnSetLight3.Enabled = False Then
            Exit Sub
        End If
        btnSetLight3.Enabled = False
        Dim light As enmLight = gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No3)
        gLightCollection.SetCCDLight(Sys.CCDNo, light, nmcLight3.Value, True)
        SetLightOnOff(light, True)
        btnSetLight3.Enabled = True
    End Sub

    Private Sub btnSetLight4_Click(sender As Object, e As EventArgs) Handles btnSetLight4.Click
        If btnSetLight4.Enabled = False Then
            Exit Sub
        End If
        btnSetLight4.Enabled = False
        Dim light As enmLight = gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No4)
        gLightCollection.SetCCDLight(Sys.CCDNo, light, nmcLight4.Value, True)
        SetLightOnOff(light, True)
        btnSetLight4.Enabled = True
    End Sub

    Private Sub chkLight1_CheckedChanged(sender As Object, e As EventArgs) Handles chkLight1.CheckedChanged
        If SceneName = Nothing Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No1) = chkLight1.Checked
        btnSetLight1.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No1)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No1)
        SetLightOnOff(light, chkLight1.Checked)
    End Sub

    Private Sub chkLight2_CheckedChanged(sender As Object, e As EventArgs) Handles chkLight2.CheckedChanged
        If SceneName = Nothing Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No2) = chkLight2.Checked
        btnSetLight2.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No2)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No2)
        SetLightOnOff(light, chkLight2.Checked)
    End Sub

    Private Sub chkLight3_CheckedChanged(sender As Object, e As EventArgs) Handles chkLight3.CheckedChanged
        If SceneName = Nothing Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No3) = chkLight3.Checked
        btnSetLight3.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No3)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No3)
        SetLightOnOff(light, chkLight3.Checked)
    End Sub

    Private Sub chkLight4_CheckedChanged(sender As Object, e As EventArgs) Handles chkLight4.CheckedChanged
        If SceneName = Nothing Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No4) = chkLight4.Checked
        btnSetLight4.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No4)
        Dim light As enmLight = gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No4)
        SetLightOnOff(light, chkLight4.Checked)
    End Sub

    Private Sub lightBar1_ValueChanged(sender As Object, e As EventArgs) Handles lightBar1.ValueChanged
        nmcLight1.Value = lightBar1.Value
    End Sub

    Private Sub lightBar2_ValueChanged(sender As Object, e As EventArgs) Handles lightBar2.ValueChanged
        nmcLight2.Value = lightBar2.Value
    End Sub

    Private Sub lightBar3_ValueChanged(sender As Object, e As EventArgs) Handles lightBar3.ValueChanged
        nmcLight3.Value = lightBar3.Value
    End Sub

    Private Sub lightBar4_ValueChanged(sender As Object, e As EventArgs) Handles lightBar4.ValueChanged
        nmcLight4.Value = lightBar4.Value
    End Sub

    Private Sub nmcLight1_ValueChanged(sender As Object, e As EventArgs) Handles nmcLight1.ValueChanged
        lightBar1.Value = nmcLight1.Value
    End Sub

    Private Sub nmcLight2_ValueChanged(sender As Object, e As EventArgs) Handles nmcLight2.ValueChanged
        lightBar2.Value = nmcLight2.Value
    End Sub

    Private Sub nmcLight3_ValueChanged(sender As Object, e As EventArgs) Handles nmcLight3.ValueChanged
        lightBar3.Value = nmcLight3.Value
    End Sub

    Private Sub nmcLight4_ValueChanged(sender As Object, e As EventArgs) Handles nmcLight4.ValueChanged
        lightBar4.Value = nmcLight4.Value
    End Sub

    Function GetValue(ByVal propertyName As String, ByVal defaultValue As Object) As Object
        For i As Integer = 0 To CogToolBlockEditV21.Subject.Outputs.Count - 1
            If propertyName = CogToolBlockEditV21.Subject.Outputs(i).Name Then
                Return CogToolBlockEditV21.Subject.Outputs(i).Value
            End If
        Next
        Return defaultValue
    End Function


    Private Sub btnRepeatRun_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If btnReset.Enabled = False Then
            Exit Sub
        End If
        btnReset.Enabled = False
        'mStatisticX.Clear()
        'mStatisticY.Clear()
        'mStatisticT.Clear()
        lblRepeatability.Text = ""
        btnReset.Enabled = True
    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click, Button2.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Hide()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click, Button1.Click
        Try
            If btnOK.Enabled = False Then
                Exit Sub
            End If
            gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnOK]" & vbTab & "Click")
            btnOK.Enabled = False
            Dim fileName As String
            Dim iniFileName As String
            fileName = Application.StartupPath & "\System\" & MachineName & "\" & lblSceneSet.Text & ".ini" '光源設定檔路徑
            Dim SceneName As String = lblSceneSet.Text
            Dim mScene As New CSceneParameter
            With mScene
                .LightValue(0) = nmcLight1.Value
                .LightValue(1) = nmcLight2.Value
                .LightValue(2) = nmcLight3.Value
                .LightValue(3) = nmcLight4.Value

                .LightEnable(0) = btnSetLight1.Enabled
                .LightEnable(1) = btnSetLight2.Enabled
                .LightEnable(2) = btnSetLight3.Enabled
                .LightEnable(3) = btnSetLight4.Enabled

                '.CCDExposureTime = nmcExposure.Value
            End With
            If SceneName Is Nothing Then
                '請選擇場景
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
                MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnOK.Enabled = True
                Exit Sub
            End If
            If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
                gAOICollection.SceneDictionary(SceneName) = mScene
            Else
                gAOICollection.SceneDictionary.Add(SceneName, mScene)
            End If
            gAOICollection.SaveSceneParameter(SceneName, fileName) '儲存光源,曝光值等設定

            fileName = Application.StartupPath & "\System\" & MachineName & "\" & lblSceneSet.Text & ".vpp"
            Cognex.VisionPro.CogSerializer.SaveObjectToFile(CogToolBlockEditV21.Subject, fileName) '控制項存到選定的Scene.
            iniFileName = Application.StartupPath & "\System\" & MachineName & "\" & lblSceneSet.Text & ".ini"
            gAOICollection.SaveSceneOutputParam(Sys.CCDNo, SceneName, iniFileName)

            If gAOICollection.LoadVision(Sys.CCDNo, fileName) = True Then '使用標準方式讀出 含掛載事件
                '存檔成功 
                gSyslog.Save(SceneName & gMsgHandler.GetMessage(Warn_3000036))
                MsgBox(SceneName & gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Else
                '存檔失敗 
                gSyslog.Save(SceneName & gMsgHandler.GetMessage(Warn_3000035))
                MsgBox(SceneName & gMsgHandler.GetMessage(Warn_3000035), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
            btnOK.Enabled = True
            'Me.Hide()
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message & " " & ex.StackTrace, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnOK.Enabled = True
        End Try
    End Sub

    ''' <summary>統計X</summary>
    ''' <remarks></remarks>
    Dim mStatisticX As New List(Of Double)
    ''' <summary>統計Y</summary>
    ''' <remarks></remarks>
    Dim mStatisticY As New List(Of Double)
    ''' <summary>統計Theta</summary>
    ''' <remarks></remarks>
    Dim mStatisticT As New List(Of Double)
    Private Async Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click 'Soni / 2017.05.16 去除DoEvents
        If btnRun.Enabled = False Then '防連按
            Exit Sub
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnRun]" & vbTab & "Click")
        btnRun.Enabled = False

        '20170602按鍵保護
        btnRepeatRun.Enabled = False
        btnACQ.Enabled = False
        btnReset.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False

        If lblSceneSet.Text Is Nothing Then '未選場景
            lblSceneSet.BackColor = Color.Yellow
            lblSceneSet.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.CurrentThread.Join(100)
            lblSceneSet.BackColor = Color.White
            btnRun.Enabled = True

            '20170602按鍵保護
            btnRepeatRun.Enabled = True
            btnACQ.Enabled = True
            btnReset.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True

            Exit Sub
            '^^^^^^^
        End If
        If CogToolBlockEditV21.Subject Is Nothing Then '計算工具不存在
            '工具不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000023))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnRun.Enabled = True

            '20170602按鍵保護
            btnRepeatRun.Enabled = True
            btnACQ.Enabled = True
            btnReset.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True

            Exit Sub
            '^^^^^^^
        End If
        If gAOICollection.IsCCDExist(Sys.CCDNo) = False Then '取像工具不存在
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnRun.Enabled = True

            '20170602按鍵保護
            btnRepeatRun.Enabled = True
            btnACQ.Enabled = True
            btnReset.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True

            Exit Sub
            '^^^^^^^
        End If
        'gAOICollection.SetExposure(Sys.CCDNo, nmcExposure.Value) '設定曝光值

        Await Task.Run(Sub()

                           gAOICollection.SetCCDScene(Sys.CCDNo, lblSceneSet.Text) '選擇場景
                           gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
                           System.Threading.Thread.CurrentThread.Join(10)
                           gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照開
                           System.Threading.Thread.CurrentThread.Join(10)
                           gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
                           Dim stopWatch As New Stopwatch
                           stopWatch.Restart()
                           Do
                               System.Threading.Thread.Sleep(1)
                               If stopWatch.ElapsedMilliseconds > 1000 Then
                                   'CCD 取像TimeOut
                                   Select Case Sys.StageNo
                                       Case 0
                                           gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012003))
                                           MsgBox(gMsgHandler.GetMessage(Alarm_2012003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 1
                                           gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012303))
                                           MsgBox(gMsgHandler.GetMessage(Alarm_2012303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 2
                                           gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012603))
                                           MsgBox(gMsgHandler.GetMessage(Alarm_2012603), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       Case 3
                                           gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012903))
                                           MsgBox(gMsgHandler.GetMessage(Alarm_2012903), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   End Select
                                   'MsgBox("Acquisition is Timeout!")
                                   btnRun.Enabled = True

                                   '20170602按鍵保護
                                   btnRepeatRun.Enabled = True
                                   btnACQ.Enabled = True
                                   btnReset.Enabled = True
                                   btnOK.Enabled = True
                                   btnCancel.Enabled = True

                                   Exit Sub
                               End If
                               '[Note] EMO時跳出
                               If gDICollection.GetState(enmDI.EMO) = True Then
                                   btnRun.Enabled = True

                                   '20170602按鍵保護
                                   btnRepeatRun.Enabled = True
                                   btnACQ.Enabled = True
                                   btnReset.Enabled = True
                                   btnOK.Enabled = True
                                   btnCancel.Enabled = True

                                   Exit Sub
                               Else
                                   If Sys.MachineNo = enmMachineStation.MachineA Then
                                       If gDICollection.GetState(enmDI.EMS) = True Then
                                           btnRun.Enabled = True

                                           '20170602按鍵保護
                                           btnRepeatRun.Enabled = True
                                           btnACQ.Enabled = True
                                           btnReset.Enabled = True
                                           btnOK.Enabled = True
                                           btnCancel.Enabled = True

                                           Exit Sub
                                       End If
                                   ElseIf Sys.MachineNo = enmMachineStation.MachineB Then
                                       If gDICollection.GetState(enmDI.EMS2) = True Then
                                           btnRun.Enabled = True

                                           '20170602按鍵保護
                                           btnRepeatRun.Enabled = True
                                           btnACQ.Enabled = True
                                           btnReset.Enabled = True
                                           btnOK.Enabled = True
                                           btnCancel.Enabled = True

                                           Exit Sub
                                       End If
                                   End If
                               End If
                           Loop Until gAOICollection.IsCCDCBusy(Sys.CCDNo) = False '等待CCD取像完成
                           Debug.Print("IsCCDCBusy:" & stopWatch.ElapsedMilliseconds)

                           'InvokeUcDisplay(UcDisplay1, gAOICollection, Sys, lstScene.SelectedItem, 0, 0, enmDisplayShape.Alignment) '更新控制項

                           If CogToolBlockEditV21.Subject.Inputs.Count = 0 Then '計算工具輸入不存在
                               '工具輸入不存在
                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000024))
                               MsgBox(gMsgHandler.GetMessage(Alarm_2000024), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               btnRun.Enabled = True

                               '20170602按鍵保護
                               btnRepeatRun.Enabled = True
                               btnACQ.Enabled = True
                               btnReset.Enabled = True
                               btnOK.Enabled = True
                               btnCancel.Enabled = True

                               Exit Sub
                               '^^^^^^^
                           End If

                           If Not CogToolBlockEditV21.Subject.Inputs.Contains("InputImage") Then '輸入影像接口不存在
                               '工具輸入影像不存在
                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000025))
                               MsgBox(gMsgHandler.GetMessage(Alarm_2000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               btnRun.Enabled = True

                               '20170602按鍵保護
                               btnRepeatRun.Enabled = True
                               btnACQ.Enabled = True
                               btnReset.Enabled = True
                               btnOK.Enabled = True
                               btnCancel.Enabled = True

                               Exit Sub
                               '^^^^^^^
                           End If

                           CogToolBlockEditV21.Subject.Inputs("InputImage").Value = gAOICollection.CalibBoardCalibration(Sys.CCDNo, gAOICollection.GetAcqOutputImage(Sys.CCDNo), False, 0) '20170317Wenda gAOICollection.GetAcqOutputImage(Sys.CCDNo) '取像工具的結果套入
                           Debug.Print("CogToolBlockEditV21.Subject.Inputs('InputImage').Value = acqObj.OutputImage:" & stopWatch.ElapsedMilliseconds)

                           CogToolBlockEditV21.Subject.Run() '計算工具 試算
                           Debug.Print("CogToolBlockEditV21.Subject.Run()" & stopWatch.ElapsedMilliseconds)

                           'TabControl1.SelectTab(tabCCD)
                           If CogToolBlockEditV21.Subject.Tools.Count = 0 Then '如果計算工具內沒有工具
                               '工具不存在
                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000023))
                               MsgBox(gMsgHandler.GetMessage(Alarm_2000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               btnRun.Enabled = True

                               '20170602按鍵保護
                               btnRepeatRun.Enabled = True
                               btnACQ.Enabled = True
                               btnReset.Enabled = True
                               btnOK.Enabled = True
                               btnCancel.Enabled = True

                               Exit Sub
                           End If

                           Dim ResultCount = CogToolBlockEditV21.Subject.Outputs("Results_Count").Value

                           If ResultCount > 0 Then
                               Dim posX As Decimal = 0
                               Dim posY As Decimal = 0
                               Dim posT As Decimal = 0
                               For mResult As Integer = 0 To ResultCount - 1
                                   posX = CogToolBlockEditV21.Subject.Outputs("Results_Item_0_TranslationX").Value
                                   posY = CogToolBlockEditV21.Subject.Outputs("Results_Item_0_TranslationY").Value
                                   posT = CogToolBlockEditV21.Subject.Outputs("Results_Item_0_Rotation").Value
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          lblRepeatability.Text = "Run Count:" & ResultCount & vbCrLf
                                                          lblRepeatability.Text += "Item" & vbTab & " " & "X(pixel)" & vbTab & " " & "Y(pixel)" & vbTab & " " & "Theta(Deg)" & vbCrLf
                                                          lblRepeatability.Text += "Now:" & vbTab & " " & posX.ToString("000.00") & vbTab & " " & posY.ToString("000.00") & vbTab & " " & posT.ToString("0.00") & vbCrLf
                                                      End Sub)
                                   End If

                                   mStatisticX.Add(posX)
                                   mStatisticY.Add(posY)
                                   mStatisticT.Add(posT)
                                   If mStatisticX.Count > 1 Then
                                       Dim avgX As Double = mStatisticX.Average()
                                       Dim avgY As Double = mStatisticY.Average()
                                       Dim avgT As Double = mStatisticT.Average()
                                       Dim sqrX As Double = 0
                                       Dim sqrY As Double = 0
                                       Dim sqrT As Double = 0
                                       For mResultNo As Integer = 0 To mStatisticX.Count - 1
                                           sqrX += Math.Pow(mStatisticX(mResultNo) - avgX, 2)
                                           sqrY += Math.Pow(mStatisticY(mResultNo) - avgY, 2)
                                           sqrT += Math.Pow(mStatisticT(mResultNo) - avgT, 2)
                                       Next
                                       Dim stdX As Double = Math.Sqrt(sqrX / (mStatisticX.Count - 1))
                                       Dim stdY As Double = Math.Sqrt(sqrY / (mStatisticY.Count - 1))
                                       Dim stdT As Double = Math.Sqrt(sqrT / (mStatisticT.Count - 1))
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              lblRepeatability.Text += "Avg: " & vbTab & " " & avgX.ToString("000.00") & vbTab & " " & avgY.ToString("000.00") & vbTab & " " & avgT.ToString("0.00") & vbCrLf
                                                              lblRepeatability.Text += "Max: " & vbTab & " " & mStatisticX.Max.ToString("000.00") & vbTab & " " & mStatisticY.Max.ToString("000.00") & vbTab & " " & mStatisticT.Max.ToString("0.00") & vbCrLf
                                                              lblRepeatability.Text += "Min: " & vbTab & " " & mStatisticX.Min.ToString("000.00") & vbTab & " " & mStatisticY.Min.ToString("000.00") & vbTab & " " & mStatisticT.Min.ToString("0.00") & vbCrLf
                                                              lblRepeatability.Text += "Std: " & vbTab & " " & stdX.ToString("000.00") & vbTab & " " & stdY.ToString("000.00") & vbTab & " " & stdT.ToString("0.00") & vbCrLf
                                                          End Sub)
                                       End If

                                   End If
                               Next
                           Else
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      lblRepeatability.Text += "No Result" & vbCrLf

                                                      '20170602按鍵保護
                                                      btnRun.Enabled = True
                                                      btnRepeatRun.Enabled = True
                                                      btnACQ.Enabled = True
                                                      btnReset.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True
                                                  End Sub)
                               End If

                           End If
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  btnRun.Enabled = True
                                                  '20170602按鍵保護
                                                  btnRepeatRun.Enabled = True
                                                  btnACQ.Enabled = True
                                                  btnReset.Enabled = True
                                                  btnOK.Enabled = True
                                                  btnCancel.Enabled = True
                                              End Sub)
                           End If

                       End Sub)

    End Sub

    Private Async Sub btnRepeatRun_Click_1(sender As Object, e As EventArgs) Handles btnRepeatRun.Click 'Soni / 2017.05.16 去除DoEvents
        If btnRepeatRun.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnRepeatRun]" & vbTab & "Click")
        btnRepeatRun.Enabled = False

        '20170602按鍵保護
        btnRun.Enabled = False
        btnACQ.Enabled = False
        btnReset.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False

        lblRepeatability.Text = ""

        Await Task.Run(Sub()


                           mStatisticX.Clear()
                           mStatisticY.Clear()
                           mStatisticT.Clear()

                           For i As Integer = 0 To 99

                               If lblSceneSet.Text Is Nothing Then '未選場景
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          lblSceneSet.BackColor = Color.Yellow
                                                          lblSceneSet.Refresh() 'Soni / 2017.05.10
                                                          System.Threading.Thread.CurrentThread.Join(100)
                                                          lblSceneSet.BackColor = Color.White
                                                          btnRepeatRun.Enabled = True

                                                          '20170602按鍵保護
                                                          btnRun.Enabled = True
                                                          btnACQ.Enabled = True
                                                          btnReset.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True

                                                      End Sub)
                                   End If
                                   Exit For
                               End If
                               If CogToolBlockEditV21.Subject Is Nothing Then '計算工具不存在
                                   '工具不存在
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000023))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   btnRepeatRun.Enabled = True

                                   '20170602按鍵保護
                                   btnRun.Enabled = True
                                   btnACQ.Enabled = True
                                   btnReset.Enabled = True
                                   btnOK.Enabled = True
                                   btnCancel.Enabled = True

                                   Exit For
                               End If
                               If gAOICollection.IsCCDExist(Sys.CCDNo) = False Then '取像工具不存在
                                   '取像工具不存在! 請先設定!
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnRepeatRun.Enabled = True

                                                          '20170602按鍵保護
                                                          btnRun.Enabled = True
                                                          btnACQ.Enabled = True
                                                          btnReset.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True

                                                      End Sub)
                                   End If
                                   Exit For
                               End If
                               'gAOICollection.SetExposure(Sys.CCDNo, nmcExposure.Value) '設定曝光值

                               gAOICollection.SetCCDScene(Sys.CCDNo, lblSceneSet.Text) '選擇場景
                               gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
                               System.Threading.Thread.CurrentThread.Join(10)
                               gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照開
                               System.Threading.Thread.CurrentThread.Join(10)
                               gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
                               Dim stopWatch As New Stopwatch
                               stopWatch.Restart()
                               Do
                                   System.Threading.Thread.Sleep(1)
                                   If stopWatch.ElapsedMilliseconds > 1000 Then
                                       'CCD 取像TimeOut
                                       Select Case Sys.StageNo
                                           Case 0
                                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012003))
                                               MsgBox(gMsgHandler.GetMessage(Alarm_2012003), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 1
                                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012303))
                                               MsgBox(gMsgHandler.GetMessage(Alarm_2012303), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 2
                                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012603))
                                               MsgBox(gMsgHandler.GetMessage(Alarm_2012603), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                           Case 3
                                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2012903))
                                               MsgBox(gMsgHandler.GetMessage(Alarm_2012903), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                       End Select
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              btnRepeatRun.Enabled = True

                                                              '20170602按鍵保護
                                                              btnRun.Enabled = True
                                                              btnACQ.Enabled = True
                                                              btnReset.Enabled = True
                                                              btnOK.Enabled = True
                                                              btnCancel.Enabled = True

                                                          End Sub)
                                       End If

                                       Exit For
                                   End If
                                   '[Note] EMO時跳出
                                   If gDICollection.GetState(enmDI.EMO) = True Then
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              btnRepeatRun.Enabled = True

                                                              '20170602按鍵保護
                                                              btnRun.Enabled = True
                                                              btnACQ.Enabled = True
                                                              btnReset.Enabled = True
                                                              btnOK.Enabled = True
                                                              btnCancel.Enabled = True

                                                          End Sub)
                                       End If
                                       Exit Sub
                                   Else
                                       If Sys.MachineNo = enmMachineStation.MachineA Then
                                           If gDICollection.GetState(enmDI.EMS) = True Then
                                               '20170929 Toby_ Add 判斷
                                               If (Not IsNothing(Me)) Then
                                                   Me.BeginInvoke(Sub()
                                                                      btnRepeatRun.Enabled = True

                                                                      '20170602按鍵保護
                                                                      btnRun.Enabled = True
                                                                      btnACQ.Enabled = True
                                                                      btnReset.Enabled = True
                                                                      btnOK.Enabled = True
                                                                      btnCancel.Enabled = True

                                                                  End Sub)
                                               End If
                                               Exit Sub
                                           End If
                                       ElseIf Sys.MachineNo = enmMachineStation.MachineB Then
                                           If gDICollection.GetState(enmDI.EMS2) = True Then
                                               '20170929 Toby_ Add 判斷
                                               If (Not IsNothing(Me)) Then
                                                   Me.BeginInvoke(Sub()
                                                                      btnRepeatRun.Enabled = True

                                                                      '20170602按鍵保護
                                                                      btnRun.Enabled = True
                                                                      btnACQ.Enabled = True
                                                                      btnReset.Enabled = True
                                                                      btnOK.Enabled = True
                                                                      btnCancel.Enabled = True

                                                                  End Sub)
                                               End If
                                               Exit Sub
                                           End If
                                       End If
                                   End If
                               Loop Until gAOICollection.IsCCDCBusy(Sys.CCDNo) = False '等待CCD取像完成
                               Debug.Print("IsCCDCBusy:" & stopWatch.ElapsedMilliseconds)

                               'InvokeUcDisplay(UcDisplay1, gAOICollection, Sys, lstScene.SelectedItem, 0, 0, enmDisplayShape.Alignment) '更新控制項

                               If CogToolBlockEditV21.Subject.Inputs.Count = 0 Then '計算工具輸入不存在
                                   '工具輸入不存在
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000024))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2000024), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnRepeatRun.Enabled = True

                                                          '20170602按鍵保護
                                                          btnRun.Enabled = True
                                                          btnACQ.Enabled = True
                                                          btnReset.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True

                                                      End Sub)
                                   End If
                                   Exit For
                               End If

                               If Not CogToolBlockEditV21.Subject.Inputs.Contains("InputImage") Then '輸入影像接口不存在
                                   '工具輸入影像不存在
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000025))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnRepeatRun.Enabled = True

                                                          '20170602按鍵保護
                                                          btnRun.Enabled = True
                                                          btnACQ.Enabled = True
                                                          btnReset.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True

                                                      End Sub)
                                   End If
                                   Exit For
                               End If

                               CogToolBlockEditV21.Subject.Inputs("InputImage").Value = gAOICollection.CalibBoardCalibration(Sys.CCDNo, gAOICollection.GetAcqOutputImage(Sys.CCDNo), False, 0) '20170317Wenda gAOICollection.GetAcqOutputImage(Sys.CCDNo) '取像工具的結果套入
                               Debug.Print("CogToolBlockEditV21.Subject.Inputs('InputImage').Value = CalibBoardCalibration.OutputImage:" & stopWatch.ElapsedMilliseconds)

                               CogToolBlockEditV21.Subject.Run() '計算工具 試算
                               Debug.Print("CogToolBlockEditV21.Subject.Run()" & stopWatch.ElapsedMilliseconds)

                               'TabControl1.SelectTab(tabCCD)
                               If CogToolBlockEditV21.Subject.Tools.Count = 0 Then '如果計算工具內沒有工具
                                   '工具不存在
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000023))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnRepeatRun.Enabled = True

                                                          '20170602按鍵保護
                                                          btnRun.Enabled = True
                                                          btnACQ.Enabled = True
                                                          btnReset.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True

                                                      End Sub)
                                   End If
                                   Exit For
                               End If

                               Dim ResultCount = CogToolBlockEditV21.Subject.Outputs("Results_Count").Value

                               If ResultCount > 0 Then
                                   Dim posX As Decimal = 0
                                   Dim posY As Decimal = 0
                                   Dim posT As Decimal = 0
                                   For mResult As Integer = 0 To ResultCount - 1
                                       posX = CogToolBlockEditV21.Subject.Outputs("Results_Item_0_TranslationX").Value
                                       posY = CogToolBlockEditV21.Subject.Outputs("Results_Item_0_TranslationY").Value
                                       posT = CogToolBlockEditV21.Subject.Outputs("Results_Item_0_Rotation").Value
                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              lblRepeatability.Text = "Run Count:" & ResultCount & vbCrLf
                                                              lblRepeatability.Text += "Item" & vbTab & " " & "X(pixel)" & vbTab & " " & "Y(pixel)" & vbTab & " " & "Theta(Deg)" & vbCrLf
                                                              lblRepeatability.Text += "Now:" & vbTab & " " & posX.ToString("000.00") & vbTab & " " & posY.ToString("000.00") & vbTab & " " & posT.ToString("0.00") & vbCrLf
                                                          End Sub)
                                       End If

                                       mStatisticX.Add(posX)
                                       mStatisticY.Add(posY)
                                       mStatisticT.Add(posT)
                                       If mStatisticX.Count > 1 Then
                                           Dim avgX As Double = mStatisticX.Average()
                                           Dim avgY As Double = mStatisticY.Average()
                                           Dim avgT As Double = mStatisticT.Average()
                                           Dim sqrX As Double = 0
                                           Dim sqrY As Double = 0
                                           Dim sqrT As Double = 0
                                           For mResultNo As Integer = 0 To mStatisticX.Count - 1
                                               sqrX += Math.Pow(mStatisticX(mResultNo) - avgX, 2)
                                               sqrY += Math.Pow(mStatisticY(mResultNo) - avgY, 2)
                                               sqrT += Math.Pow(mStatisticT(mResultNo) - avgT, 2)
                                           Next
                                           Dim stdX As Double = Math.Sqrt(sqrX / (mStatisticX.Count - 1))
                                           Dim stdY As Double = Math.Sqrt(sqrY / (mStatisticY.Count - 1))
                                           Dim stdT As Double = Math.Sqrt(sqrT / (mStatisticT.Count - 1))
                                           '20170929 Toby_ Add 判斷
                                           If (Not IsNothing(Me)) Then
                                               Me.BeginInvoke(Sub()
                                                                  lblRepeatability.Text += "Avg: " & vbTab & " " & avgX.ToString("000.00") & vbTab & " " & avgY.ToString("000.00") & vbTab & " " & avgT.ToString("0.00") & vbCrLf
                                                                  lblRepeatability.Text += "Max: " & vbTab & " " & mStatisticX.Max.ToString("000.00") & vbTab & " " & mStatisticY.Max.ToString("000.00") & vbTab & " " & mStatisticT.Max.ToString("0.00") & vbCrLf
                                                                  lblRepeatability.Text += "Min: " & vbTab & " " & mStatisticX.Min.ToString("000.00") & vbTab & " " & mStatisticY.Min.ToString("000.00") & vbTab & " " & mStatisticT.Min.ToString("0.00") & vbCrLf
                                                                  lblRepeatability.Text += "Std: " & vbTab & " " & stdX.ToString("000.00") & vbTab & " " & stdY.ToString("000.00") & vbTab & " " & stdT.ToString("0.00") & vbCrLf
                                                              End Sub)
                                           End If

                                       End If
                                   Next
                               Else
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          lblRepeatability.Text += "No Result" & vbCrLf
                                                          btnRepeatRun.Enabled = True

                                                          '20170602按鍵保護
                                                          btnRun.Enabled = True
                                                          btnACQ.Enabled = True
                                                          btnReset.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True

                                                      End Sub)
                                   End If

                                   Exit For
                               End If
                           Next
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  btnRepeatRun.Enabled = True

                                                  '20170602按鍵保護
                                                  btnRun.Enabled = True
                                                  btnACQ.Enabled = True
                                                  btnReset.Enabled = True
                                                  btnOK.Enabled = True
                                                  btnCancel.Enabled = True

                                              End Sub)
                           End If


                       End Sub)
    End Sub


End Class
Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports Cognex.VisionPro
Imports ProjectAOI

Public Class frmAlignPR01
    Public Sys As sSysParam
    Public Recipe As ProjectRecipe.CRecipe

    Dim ClassName As String = "frmAlignPR01"
    ''' <summary>CCD動態顯示頁籤</summary>
    ''' <remarks></remarks>
    Const tabCCD As Integer = 0
    ''' <summary>定位特徵膠導頁籤</summary>
    ''' <remarks></remarks>
    Const TabAlignmentPatternRecognition As Integer = 1

    '''<summary>預選場景</summary>
    ''' <remarks></remarks>
    Public SceneName As String
    ''' <summary>上一組場景</summary>
    ''' <remarks></remarks>
    Private PreSceneName As String
    ''' <summary>上一組場景</summary>
    ''' <remarks></remarks>
    Public RecipeSceneName As String

    Private Flag_Pause As Boolean = False

    Private Sub frmAlignPR01_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        UcDisplay1.EndLive()
        CogToolBlockEditV21.Subject = New Cognex.VisionPro.ToolBlock.CogToolBlock()
        CogToolBlockEditV21.Subject.Dispose()
        CogToolBlockEditV21.Subject = Nothing
        CogToolBlockEditV21.Dispose()
        'CogToolBlockEditV21.Subject.Dispose() 'Eason 20170228 會把被連結的也殺掉
        'CogToolBlockEditV21.Dispose()

        UcDisplay1.ManualDispose()
        Me.Dispose(True)
        'Eason 20170221 Ticket:100033 , Memory Free Part4 [E]

    End Sub
    ''' <summary>選擇相應的顯示方式,與工具清單.</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmAlignPR01_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CogToolBlockEditV21.Subject.GarbageCollectionEnabled = True
        CogToolBlockEditV21.Subject.GarbageCollectionFrequency = 5

        SuspendLayout() '測試是否改善顯示
        Select Case gAOICollection.GetCCDType(Sys.CCDNo) '
            Case enmCCDType.CognexVPRO
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
            Case enmCCDType.OmronFZS2MUDP
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
            Case Else
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
        End Select
        '
        '限制光源通道最大輸出
        lightBar1.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No1)
        lightBar2.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No2)
        lightBar3.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No3)
        lightBar4.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No4)
        nmcLight1.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No1)
        nmcLight2.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No2)
        nmcLight3.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No3)
        nmcLight4.Maximum = gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No4)

        lstScene.Items.Clear()

        'lstScene.Items.AddRange(gAOICollection.GetAlignmentToolNameList(Sys.CCDNo)) '顯示定位工具清單
        lstScene.Items.AddRange(gAOICollection.GetSceneList)
        If Not SceneName Is Nothing Then
            If lstScene.Items.Contains(SceneName) Then
                lstScene.SelectedItem = SceneName
                RecipeSceneName = SceneName '保存場景名稱
            End If
        End If

        'mobary+2016.11.04
        '[Note]:光源切換
        '[Note]:光源切換沒有成功，改了設定，但是實際上卻沒有做切換
        '       FromLoad時，I/O切換一定要強制做一次切換，若前一次進入開啟I/O，下一次進入若設定為關閉時CheckedChanged不會觸發，故不會關閉I/O
        If Not SceneName Is Nothing Then
            If SceneName <> "0" And gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
                SetLightOnOff(gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No1), gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No1))
                SetLightOnOff(gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No2), gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No2))
                SetLightOnOff(gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No3), gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No3))
                SetLightOnOff(gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No4), gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No4))
            End If
        Else '無場景時預設光源
            SetLightOnOff(gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No1), True)
            SetLightOnOff(gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No2), True)
            SetLightOnOff(gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No3), False)
            SetLightOnOff(gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No4), False)
            chkLight1.Checked = True
            chkLight2.Checked = True
            chkLight3.Checked = False
            chkLight4.Checked = False
            btnSetLight1.Enabled = True
            btnSetLight2.Enabled = True
            btnSetLight3.Enabled = False
            btnSetLight4.Enabled = False
            gLightCollection.SetCCDLight(Sys.CCDNo, gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No1), nmcLight1.Value, True)
            gLightCollection.SetCCDLight(Sys.CCDNo, gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No2), nmcLight2.Value, True)
        End If

        ''[Note]固定機台曝光時間(飛拍考量),依光源不同而有所不同 2017/03/07
        'Select Case gSSystemParameter.MachineType
        '    Case enmMachineType.DCSW_800AQ, enmMachineType.eDTS_2S2V
        '        nmcExposure.Value = 10
        '    Case enmMachineType.DCS_F230A
        '        nmcExposure.Value = 15
        '    Case enmMachineType.DCS_350A
        '        nmcExposure.Value = 0.3
        '    Case Else
        'End Select
        'gAOICollection.SetExposure(Sys.CCDNo, nmcExposure.Value) '設定曝光值

        'If lstScene.Items.Count > 0 Then
        '    lstScene.SelectedIndex = 0
        '    CogDisplay1.Image = CType(gAOICollection.Items(0), CAOICognexVPRO).cogTB_AlignList.Item(lstScene.SelectedItem).Subject.Inputs("InputImage").Value
        'End If


        'CogToolDisplay1.Tool = CType(gAOICollection.Items(0), CAOICognexVPRO).cogTB_AlignList(lstScene.SelectedItem).Subject
        'CogToolDisplay1.Tool = CType(gAOICollection.Items(0), CAOICognexVPRO).cogTB_AlignList(lstScene.SelectedItem).Subject.Tools
        If UcDisplay1.StartLive(Sys.CCDNo) = False Then 'Soni / 2016.09.07 增加異常時顯示與紀錄
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
        End If
        ResumeLayout()
    End Sub


    Private Async Sub btnACQ_Click(sender As Object, e As EventArgs) Handles btnACQ.Click 'Soni / 2017.05.16 去除DoEvents
        If btnACQ.Enabled = False Then
            Exit Sub
        End If

        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnACQ]" & vbTab & "Click")
        btnACQ.Enabled = False

        '20170602按鍵保護
        btnImport.Enabled = False
        btnRun.Enabled = False
        btnRepeatRun.Enabled = False
        btnReset.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False

        If Not gAOICollection.IsCCDExist(Sys.CCDNo) Then
            '物件不存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000034))
            MsgBox(gMsgHandler.GetMessage(Warn_3000034), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnACQ.Enabled = True

            '20170602按鍵保護
            btnImport.Enabled = True
            btnRun.Enabled = True
            btnRepeatRun.Enabled = True
            btnReset.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True

            Exit Sub
        End If
        If lstScene.SelectedItem Is Nothing Then
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
               '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   btnACQ.Enabled = True

                                   '20170602按鍵保護
                                   btnImport.Enabled = True
                                   btnRun.Enabled = True
                                   btnRepeatRun.Enabled = True
                                   btnReset.Enabled = True
                                   btnOK.Enabled = True
                                   btnCancel.Enabled = True

                               End Sub)
            End If
            Exit Sub
        End If

        Dim mSceneID As String = lstScene.SelectedItem

        TabControl1.SelectTab(TabAlignmentPatternRecognition) '切換到教導介面

        Await Task.Run(Sub()
                           Try

                               gAOICollection.SetCCDScene(Sys.CCDNo, mSceneID)
                               'gAOICollection.SetExposure(Sys.CCDNo, nmcExposure.Value) '設定曝光值

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
                                                              btnACQ.Enabled = True

                                                              '20170602按鍵保護
                                                              btnImport.Enabled = True
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
                                                              btnImport.Enabled = True
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
                                                                      btnImport.Enabled = True
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
                                                                      btnImport.Enabled = True
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

                               InvokeUcDisplay(UcDisplay1, gAOICollection, Sys, mSceneID, 0, 0, enmDisplayShape.Alignment) '更新控制項,必要條件 frmMain必須是實體


                               If CogToolBlockEditV21.Subject Is Nothing Then
                                   '工具不存在
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000023))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnACQ.Enabled = True

                                                          '20170602按鍵保護
                                                          btnImport.Enabled = True
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
                                                          btnImport.Enabled = True
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
                                                          btnImport.Enabled = True
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

                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnACQ.Enabled = True

                                                      '20170602按鍵保護
                                                      btnImport.Enabled = True
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
                                                      btnImport.Enabled = True
                                                      btnRun.Enabled = True
                                                      btnRepeatRun.Enabled = True
                                                      btnReset.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True

                                                  End Sub)
                               End If
                           End Try
                       End Sub)
        'CogDisplay1.Image = CType(gAOICollection.Items(0), CAOICognexVPRO).cogTB_ACQ0.OutputImage
    End Sub



    ''' <summary>瀏覽檔名清單</summary>
    ''' <remarks></remarks>
    Dim mBrowserFileNameList As New List(Of String)

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        If btnImport.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnImport]" & vbTab & "Click")
        btnImport.Enabled = False

        '20170602按鍵保護
        btnACQ.Enabled = False
        btnRun.Enabled = False
        btnRepeatRun.Enabled = False
        btnReset.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False


        Dim dialog As New FolderBrowserDialog
        'dialog.Filter = "*.bmp|*.bmp"
        If dialog.ShowDialog() <> Windows.Forms.DialogResult.OK Then '未確認 路徑維持原設定不變更
            Exit Sub
        End If
        If System.IO.Directory.Exists(dialog.SelectedPath) = False Then '選擇路徑不存在
            '檔案不存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000066))
            MsgBox(gMsgHandler.GetMessage(Warn_3000066), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnImport.Enabled = True

            '20170602按鍵保護
            btnACQ.Enabled = True
            btnRun.Enabled = True
            btnRepeatRun.Enabled = True
            btnReset.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True

            Exit Sub
        End If

        mBrowserFileNameList.Clear()
        For Each file In System.IO.Directory.GetFiles(dialog.SelectedPath) '對於選取的資料夾
            If file.EndsWith(".bmp") Then '只取bmp檔
                mBrowserFileNameList.Add(file)
            End If
        Next
        hscFile.Maximum = mBrowserFileNameList.Count - 1
        hscFile.Minimum = 0
        hscFile.Value = 0
        hscFile.Visible = True
        lblFolder.Visible = True
        btnImport.Enabled = True

        '20170602按鍵保護
        btnACQ.Enabled = True
        btnRun.Enabled = True
        btnRepeatRun.Enabled = True
        btnReset.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True

    End Sub

    Private Sub hscFile_Scroll(sender As Object, e As ScrollEventArgs) Handles hscFile.Scroll
        Dim fileName As String = mBrowserFileNameList(hscFile.Value)
        lblFolder.Text = "Path:" & fileName
        If Not System.IO.File.Exists(fileName) Then
            Exit Sub
        End If
        Dim bmp As New Bitmap(fileName) '讀取指定檔案

        Select Case bmp.PixelFormat
            Case Imaging.PixelFormat.Format8bppIndexed
            Case Imaging.PixelFormat.Format16bppGrayScale
                Dim img As New Cognex.VisionPro.CogImage8Grey(bmp) '影像檔轉檔 bmp To CogImage
                If CogToolBlockEditV21.Subject Is Nothing Then
                    'sue0428
                    '請選擇場景
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Exit Sub
                End If
                If Not CogToolBlockEditV21.Subject.Inputs.Contains("InputImage") Then
                    '工具輸入影像不存在
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000025))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Exit Sub
                End If

                TabControl1.SelectTab(TabAlignmentPatternRecognition) '切換到Alignment
                CogToolBlockEditV21.Subject.Inputs("InputImage").Value = img 'cogImage影像丟入CogToolBlock
            Case Imaging.PixelFormat.Format24bppRgb
                Dim img As New Cognex.VisionPro.CogImage24PlanarColor(bmp)
                If CogToolBlockEditV21.Subject Is Nothing Then
                    'sue0428
                    '請選擇場景
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Exit Sub
                End If
                If Not CogToolBlockEditV21.Subject.Inputs.Contains("InputImage") Then
                    '工具輸入影像不存在
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000025))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Exit Sub
                End If

                TabControl1.SelectTab(TabAlignmentPatternRecognition) '切換到Alignment
                CogToolBlockEditV21.Subject.Inputs("InputImage").Value = img 'cogImage影像丟入CogToolBlock
        End Select

        'CogToolBlockEditV21.Subject.Run()
    End Sub

    Private Sub btnSceneAdd_Click(sender As Object, e As EventArgs) Handles btnSceneAdd.Click

        If btnSceneAdd.Enabled = False Then
            Exit Sub
        End If
        btnSceneAdd.Enabled = False
        If txtScene.Text = "" Then
            'sue0428
            '請輸入場景ID
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000021))
            MsgBox(gMsgHandler.GetMessage(Warn_3000021), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSceneAdd.Enabled = True
            Exit Sub
        End If

        If gAOICollection.IsSceneExist(Sys.CCDNo, txtScene.Text) Then
            '場景ID已經存在!
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000067))
            MsgBox(gMsgHandler.GetMessage(Warn_3000067), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSceneAdd.Enabled = True
            Exit Sub
        End If

        gAOICollection.CreateScene(Sys.CCDNo, txtScene.Text) '建立場景物件
        lstScene.Items.Add(txtScene.Text)

        'mobary+ 2016.10.30
        '[Note]:要使用上一組場景光源設定的前提條件是有先選用其他場景然後才做新增的動作
        If Not IsNothing(lstScene.SelectedItem) Then
            PreSceneName = lstScene.SelectedItem
        Else
            PreSceneName = Nothing
        End If

        '[Note]:將Index指向目前新增的場景
        For mI As Integer = 0 To lstScene.Items.Count - 1
            If lstScene.Items(mI).ToString = txtScene.Text Then
                lstScene.SelectedIndex = mI
            End If
        Next

        If Not IsNothing(PreSceneName) = True Then
            If gAOICollection.SceneDictionary.ContainsKey(PreSceneName) Then
                '====根據上一組場景做光源與曝光設定
                SetnmcLightwithLightBar(nmcLight1, gAOICollection.SceneDictionary(PreSceneName).LightValue(enmValveLight.No1), lightBar1)
                SetnmcLightwithLightBar(nmcLight2, gAOICollection.SceneDictionary(PreSceneName).LightValue(enmValveLight.No2), lightBar2)
                SetnmcLightwithLightBar(nmcLight3, gAOICollection.SceneDictionary(PreSceneName).LightValue(enmValveLight.No3), lightBar3)
                SetnmcLightwithLightBar(nmcLight4, gAOICollection.SceneDictionary(PreSceneName).LightValue(enmValveLight.No4), lightBar4)
                chkLight1.Checked = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No1)
                chkLight2.Checked = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No2)
                chkLight3.Checked = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No3)
                chkLight4.Checked = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No4)
                btnSetLight1.Enabled = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No1)
                btnSetLight2.Enabled = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No2)
                btnSetLight3.Enabled = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No3)
                btnSetLight4.Enabled = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No4)
                'nmcExposure.Value = gAOICollection.SceneDictionary(PreSceneName).CCDExposureTime
            Else
                SetnmcLightwithLightBar(nmcLight1, 100, lightBar1)
                SetnmcLightwithLightBar(nmcLight2, 0, lightBar2)
                SetnmcLightwithLightBar(nmcLight3, 0, lightBar3)
                SetnmcLightwithLightBar(nmcLight4, 0, lightBar4)
                chkLight1.Checked = True
                chkLight2.Checked = True
                chkLight3.Checked = False
                chkLight4.Checked = False
                btnSetLight1.Enabled = True
                btnSetLight2.Enabled = True
                btnSetLight3.Enabled = False
                btnSetLight4.Enabled = False
                'nmcExposure.Value = 5
            End If
        Else
            SetnmcLightwithLightBar(nmcLight1, 100, lightBar1)
            SetnmcLightwithLightBar(nmcLight2, 0, lightBar2)
            SetnmcLightwithLightBar(nmcLight3, 0, lightBar3)
            SetnmcLightwithLightBar(nmcLight4, 0, lightBar4)
            chkLight1.Checked = True
            chkLight2.Checked = True
            chkLight3.Checked = False
            chkLight4.Checked = False
            btnSetLight1.Enabled = True
            btnSetLight2.Enabled = True
            btnSetLight3.Enabled = False
            btnSetLight4.Enabled = False
            'nmcExposure.Value = 5
        End If

        'If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
        '    PreSceneName = SceneName '上一組場景名稱
        'End If
        'If gAOICollection.SceneDictionary.ContainsKey(PreSceneName) Then
        '    '====根據上一組場景做光源與曝光設定
        '    SetnmcLightwithLightBar(nmcLight1, gAOICollection.SceneDictionary(PreSceneName).LightValue(enmValveLight.No1), lightBar1)
        '    SetnmcLightwithLightBar(nmcLight2, gAOICollection.SceneDictionary(PreSceneName).LightValue(enmValveLight.No2), lightBar2)
        '    SetnmcLightwithLightBar(nmcLight3, gAOICollection.SceneDictionary(PreSceneName).LightValue(enmValveLight.No3), lightBar3)
        '    SetnmcLightwithLightBar(nmcLight4, gAOICollection.SceneDictionary(PreSceneName).LightValue(enmValveLight.No4), lightBar4)
        '    chkLight1.Checked = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No1)
        '    chkLight2.Checked = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No2)
        '    chkLight3.Checked = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No3)
        '    chkLight4.Checked = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No4)
        '    btnSetLight1.Enabled = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No1)
        '    btnSetLight2.Enabled = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No2)
        '    btnSetLight3.Enabled = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No3)
        '    btnSetLight4.Enabled = gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No4)
        '    nmcExposure.Value = gAOICollection.SceneDictionary(PreSceneName).CCDExposureTime
        'Else
        '    SetnmcLightwithLightBar(nmcLight1, 0, lightBar1)
        '    SetnmcLightwithLightBar(nmcLight2, 0, lightBar2)
        '    SetnmcLightwithLightBar(nmcLight3, 0, lightBar3)
        '    SetnmcLightwithLightBar(nmcLight4, 0, lightBar4)
        '    chkLight1.Checked = True
        '    chkLight2.Checked = True
        '    chkLight3.Checked = False
        '    chkLight4.Checked = False
        '    btnSetLight1.Enabled = True
        '    btnSetLight2.Enabled = True
        '    btnSetLight3.Enabled = False
        '    btnSetLight4.Enabled = False
        '    nmcExposure.Value = 5
        'End If
        'gAOICollection.SetExposure(Sys.CCDNo, nmcExposure.Value)
        Dim light1 As enmLight = gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No1)
        gLightCollection.SetCCDLight(Sys.CCDNo, light1, nmcLight1.Value, True)
        SetLightOnOff(light1, chkLight1.CheckState)
        Dim light2 As enmLight = gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No2)
        gLightCollection.SetCCDLight(Sys.CCDNo, light2, nmcLight2.Value, True)
        SetLightOnOff(light2, chkLight2.CheckState)
        Dim light3 As enmLight = gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No3)
        gLightCollection.SetCCDLight(Sys.CCDNo, light3, nmcLight3.Value, True)
        SetLightOnOff(light3, chkLight3.CheckState)
        Dim light4 As enmLight = gSysAdapter.CCDLightMapping(Sys.CCDNo, enmValveLight.No4)
        gLightCollection.SetCCDLight(Sys.CCDNo, light4, nmcLight4.Value, True)
        SetLightOnOff(light4, chkLight4.CheckState)


        '存場景檔案
        Dim RecipeDirectoryName As String = Application.StartupPath & "\Scene\" & MachineName
        Dim fileName = RecipeDirectoryName & "\" & SceneName & ".ini" 'System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & txtScene.Text & ".ini" '光源設定檔路徑
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
            'sue0428
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

        gAOICollection.SaveSceneParameter(txtScene.Text, fileName)
        gAOICollection.LoadSceneParameter(txtScene.Text, fileName)
        Dim file = RecipeDirectoryName & "\" & txtScene.Text & ".vpp" 'System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & txtScene.Text & ".vpp" '場景vpp設定檔路徑
        Cognex.VisionPro.CogSerializer.SaveObjectToFile(CogToolBlockEditV21.Subject, file, GetType(System.Runtime.Serialization.Formatters.Binary.BinaryFormatter), CogSerializationOptionsConstants.Results)
        'gAOICollection.LoadVision(Sys.CCDNo, file)
        gAOICollection.LoadSceneList(Recipe.strFileName) '更新存在場景字串
        '===
        txtScene.Text = Nothing
        btnSceneAdd.Enabled = True
    End Sub

    Private Sub btnSceneDel_Click(sender As Object, e As EventArgs) Handles btnSceneDel.Click
        If btnSceneDel.Enabled = False Then
            Exit Sub
        End If
        btnSceneDel.Enabled = False
        Dim mSceneName As String = lstScene.SelectedItem
        If Not gAOICollection.IsSceneExist(Sys.CCDNo, mSceneName) Then
            'sue0602
            '場景不存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000020))
            MsgBox(gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSceneDel.Enabled = True
        End If
        If MsgBox("Do you want to delete " & mSceneName, MsgBoxStyle.OkCancel + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        gAOICollection.RemoveScene(Sys.CCDNo, lstScene.SelectedItem) '刪除場景物件
        gAOICollection.SceneDictionary.Remove(lstScene.SelectedItem) '刪除光源物件
        lstScene.Items.Remove(lstScene.SelectedItem)

        If mSceneName Is Nothing Then
            'sue0428
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSceneDel.Enabled = True
            Exit Sub
        End If
        Dim fileName As String
        Dim RecipeDirectoryName As String = Application.StartupPath & "\Scene\" & MachineName
        fileName = RecipeDirectoryName & "\" & mSceneName & ".ini" ''System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & mSceneName & ".ini" '光源設定檔路徑
        If System.IO.File.Exists(fileName) Then
            System.IO.File.Delete(fileName)
        End If
        fileName = RecipeDirectoryName & "\" & mSceneName & ".vpp" 'System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & mSceneName & ".vpp" '影像設定檔路徑
        If System.IO.File.Exists(fileName) Then
            System.IO.File.Delete(fileName)
        End If
        gAOICollection.LoadSceneList(Recipe.strFileName) '更新存在場景字串
        'Sue AlarmCode 20170602
        MsgBox(mSceneName & " Delete OK.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        btnSceneDel.Enabled = True
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

    Private Sub lstScene_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstScene.SelectedIndexChanged
        If lstScene.SelectedItem Is Nothing Then
            Exit Sub
        End If
        Dim fileName As String
        Dim RecipeDirectoryName As String = Application.StartupPath & "\Scene\" & MachineName
        SceneName = lstScene.SelectedItem
        If SceneName.Length > 5 Then

            If SceneName.Substring(0, 5) = "CALIB" Then
                fileName = Application.StartupPath & "\System\" & MachineName & "\" & SceneName & ".ini"
            Else
                fileName = RecipeDirectoryName & "\" & SceneName & ".ini" 'System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & SceneName & ".ini" '光源設定檔路徑
            End If
        Else
            fileName = RecipeDirectoryName & "\" & SceneName & ".ini" 'System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & SceneName & ".ini" '光源設定檔路徑
        End If

        gAOICollection.LoadSceneParameter(SceneName, fileName) '讀取光源,曝光值等設定
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            chkLight1.Checked = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No1)
            chkLight2.Checked = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No2)
            chkLight3.Checked = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No3)
            chkLight4.Checked = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No4)

            btnSetLight1.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No1)
            btnSetLight2.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No2)
            btnSetLight3.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No3)
            btnSetLight4.Enabled = gAOICollection.SceneDictionary(SceneName).LightEnable(enmValveLight.No4)
            If gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No1) > gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No1) Then
                SetnmcLightwithLightBar(nmcLight1, gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No1), lightBar1)
            Else
                SetnmcLightwithLightBar(nmcLight1, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No1), lightBar1)
            End If
            If gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No2) > gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No2) Then
                SetnmcLightwithLightBar(nmcLight2, gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No2), lightBar2)
            Else
                SetnmcLightwithLightBar(nmcLight2, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No2), lightBar2)
            End If
            If gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No3) > gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No3) Then
                SetnmcLightwithLightBar(nmcLight3, gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No3), lightBar3)
            Else
                SetnmcLightwithLightBar(nmcLight3, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No3), lightBar3)
            End If
            If gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No4) > gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No4) Then
                SetnmcLightwithLightBar(nmcLight4, gLightCollection.GetCCDLightMaxValue(Sys.CCDNo, enmValveLight.No4), lightBar4)
            Else
                SetnmcLightwithLightBar(nmcLight4, gAOICollection.SceneDictionary(SceneName).LightValue(enmValveLight.No4), lightBar4)
            End If

            'nmcExposure.Value = gAOICollection.SceneDictionary(SceneName).CCDExposureTime
        End If

        If gAOICollection.IsSceneExist(Sys.CCDNo, SceneName) = False Then '場景不存在
            Dim file As String = RecipeDirectoryName & "\" & SceneName & ".vpp" 'System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & SceneName & ".vpp"
            If gAOICollection.LoadVision(Sys.CCDNo, file) = False Then
                'sue0428
                '場景不存在
                gSyslog.Save(SceneName & gMsgHandler.GetMessage(Warn_3000020))
                MsgBox(SceneName & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Exit Sub
            End If
        End If

        InvokeCogToolBLock(CogToolBlockEditV21, gAOICollection.GetToolBlock(Sys.CCDNo, lstScene.SelectedItem))
        'SuspendLayout()
        ''CogToolBlockEditV21.SuspendLayout()
        'CogToolBlockEditV21.Subject = gAOICollection.GetToolBlock(Sys.CCDNo, lstScene.SelectedItem)
        ''CogToolBlockEditV21.ResumeLayout()
        'ResumeLayout()
    End Sub


    Dim mTrainRegion As Cognex.VisionPro.ICogRegion
    Private Sub btnTrain_Click(sender As Object, e As EventArgs)
        If lstScene.SelectedIndex < 0 Then
            'sue0428
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Select Align Scene, Please!")
            Exit Sub
        End If

        Dim cogToolBlock = gAOICollection.GetToolBlock(Sys.CCDNo, lstScene.SelectedItem)
        If cogToolBlock Is Nothing Then
            'sue0428
            '定位工具不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000007))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000007), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Align Tool Not Exists!")
            Exit Sub
        End If
        'Dim mTool As Cognex.VisionPro.PMAlign.CogPMAlignTool = cogToolBlock.Tools(0)
        Dim mTool As Cognex.VisionPro.PMAlign.CogPMAlignTool = Nothing
        For i As Integer = 0 To cogToolBlock.Tools.Count - 1
            If cogToolBlock.Tools(i).GetType = GetType(Cognex.VisionPro.PMAlign.CogPMAlignTool) Then
                mTool = cogToolBlock.Tools(i)
            End If
        Next
        If mTool Is Nothing Then
            'Sue AlarmCode 20170602
            '定位工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000007))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000007), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Please use PMAlignTool")
            Exit Sub
        End If

        mTool.Pattern.TrainRegion = mTrainRegion
        mTool.Pattern.TrainAlgorithm = Cognex.VisionPro.PMAlign.CogPMAlignTrainAlgorithmConstants.PatMaxAndPatQuick
        mTool.Pattern.Train()
        'CogDisplay1.StaticGraphics.AddList(mTool.Pattern.CreateGraphicsCoarse(Cognex.VisionPro.CogColorConstants.Cyan), "test")
        'CogDisplay1.StaticGraphics.AddList(mTool.Pattern.CreateGraphicsFine(Cognex.VisionPro.CogColorConstants.Green), "test")
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
    Private Async Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click
        If btnRun.Enabled = False Then '防連按
            Exit Sub
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnRun]" & vbTab & "Click")
        btnRun.Enabled = False

        '20170602按鍵保護
        btnImport.Enabled = False
        btnACQ.Enabled = False
        btnRepeatRun.Enabled = False
        btnReset.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False

        If lstScene.SelectedItem Is Nothing Then '未選場景
            lstScene.BackColor = Color.Yellow
            lstScene.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.CurrentThread.Join(100)
            lstScene.BackColor = Color.White
            btnRun.Enabled = True

            '20170602按鍵保護
            btnImport.Enabled = True
            btnACQ.Enabled = True
            btnRepeatRun.Enabled = True
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
            'MsgBox("Train Tool First, Please.")
            btnRun.Enabled = True

            '20170602按鍵保護
            btnImport.Enabled = True
            btnACQ.Enabled = True
            btnRepeatRun.Enabled = True
            btnReset.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True

            Exit Sub
            '^^^^^^^
        End If
        If gAOICollection.IsCCDExist(Sys.CCDNo) = False Then '取像工具不存在
            'sue0428
            '取像工具不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Acquisition Object Not Exists.")
            btnRun.Enabled = True

            '20170602按鍵保護
            btnImport.Enabled = True
            btnACQ.Enabled = True
            btnRepeatRun.Enabled = True
            btnReset.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True

            Exit Sub
            '^^^^^^^
        End If
        'gAOICollection.SetExposure(Sys.CCDNo, nmcExposure.Value) '設定曝光值
        Dim mSceneID As String = lstScene.SelectedItem
        Await Task.Run(Sub()
                           gAOICollection.SetCCDScene(Sys.CCDNo, mSceneID) '選擇場景
                           gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
                           System.Threading.Thread.CurrentThread.Join(10)
                           gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照開
                           System.Threading.Thread.CurrentThread.Join(10)
                           gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
                           Dim stopWatch As New Stopwatch
                           stopWatch.Restart()
                           Do
                               System.Threading.Thread.Sleep(1)
                               If stopWatch.ElapsedMilliseconds > 10000 Then
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
                                                          btnRun.Enabled = True

                                                          '20170602按鍵保護
                                                          btnImport.Enabled = True
                                                          btnACQ.Enabled = True
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
                                                          btnRun.Enabled = True

                                                          '20170602按鍵保護
                                                          btnImport.Enabled = True
                                                          btnACQ.Enabled = True
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
                                                                  btnRun.Enabled = True

                                                                  '20170602按鍵保護
                                                                  btnImport.Enabled = True
                                                                  btnACQ.Enabled = True
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
                                                                  btnRun.Enabled = True

                                                                  '20170602按鍵保護
                                                                  btnImport.Enabled = True
                                                                  btnACQ.Enabled = True
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
                           Loop Until gAOICollection.IsCCDCBusy(Sys.CCDNo) = False '等待CCD取像完成
                           Debug.Print("IsCCDCBusy:" & stopWatch.ElapsedMilliseconds)

                           InvokeUcDisplay(UcDisplay1, gAOICollection, Sys, mSceneID, 0, 0, enmDisplayShape.Alignment) '更新控制項


                           If CogToolBlockEditV21.Subject.Inputs.Count = 0 Then '計算工具輸入不存在
                               '工具輸入不存在
                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000024))
                               MsgBox(gMsgHandler.GetMessage(Alarm_2000024), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               'MsgBox("Tool Subject Input Not Exists.")
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnRun.Enabled = True

                                                      '20170602按鍵保護
                                                      btnImport.Enabled = True
                                                      btnACQ.Enabled = True
                                                      btnRepeatRun.Enabled = True
                                                      btnReset.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True

                                                  End Sub)
                               End If
                               Exit Sub
                               '^^^^^^^
                           End If

                           If Not CogToolBlockEditV21.Subject.Inputs.Contains("InputImage") Then '輸入影像接口不存在
                               '工具輸入影像不存在
                               gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000025))
                               MsgBox(gMsgHandler.GetMessage(Alarm_2000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnRun.Enabled = True

                                                      '20170602按鍵保護
                                                      btnImport.Enabled = True
                                                      btnACQ.Enabled = True
                                                      btnRepeatRun.Enabled = True
                                                      btnReset.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True

                                                  End Sub)
                               End If
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
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnRun.Enabled = True

                                                      '20170602按鍵保護
                                                      btnImport.Enabled = True
                                                      btnACQ.Enabled = True
                                                      btnRepeatRun.Enabled = True
                                                      btnReset.Enabled = True
                                                      btnOK.Enabled = True
                                                      btnCancel.Enabled = True

                                                  End Sub)
                               End If
                               Exit Sub
                           End If

                           Dim ResultCount = CogToolBlockEditV21.Subject.Outputs("Results_Count").Value

                           If ResultCount > 0 Then
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      Dim posX As Decimal = 0
                                                      Dim posY As Decimal = 0
                                                      Dim posT As Decimal = 0
                                                      For mResult As Integer = 0 To ResultCount - 1
                                                          posX = CogToolBlockEditV21.Subject.Outputs("Results_Item_0_TranslationX").Value
                                                          posY = CogToolBlockEditV21.Subject.Outputs("Results_Item_0_TranslationY").Value
                                                          posT = CogToolBlockEditV21.Subject.Outputs("Results_Item_0_Rotation").Value
                                                          lblRepeatability.Text = "Run Count:" & ResultCount & vbCrLf
                                                          lblRepeatability.Text += "Item" & vbTab & " " & "X(pixel)" & vbTab & " " & "Y(pixel)" & vbTab & " " & "Theta(Deg)" & vbCrLf
                                                          lblRepeatability.Text += "Now:" & vbTab & " " & posX.ToString("000.00") & vbTab & " " & posY.ToString("000.00") & vbTab & " " & posT.ToString("0.00") & vbCrLf

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

                                                              lblRepeatability.Text += "Avg: " & vbTab & " " & avgX.ToString("000.00") & vbTab & " " & avgY.ToString("000.00") & vbTab & " " & avgT.ToString("0.00") & vbCrLf
                                                              lblRepeatability.Text += "Max: " & vbTab & " " & mStatisticX.Max.ToString("000.00") & vbTab & " " & mStatisticY.Max.ToString("000.00") & vbTab & " " & mStatisticT.Max.ToString("0.00") & vbCrLf
                                                              lblRepeatability.Text += "Min: " & vbTab & " " & mStatisticX.Min.ToString("000.00") & vbTab & " " & mStatisticY.Min.ToString("000.00") & vbTab & " " & mStatisticT.Min.ToString("0.00") & vbCrLf
                                                              lblRepeatability.Text += "Std: " & vbTab & " " & stdX.ToString("000.00") & vbTab & " " & stdY.ToString("000.00") & vbTab & " " & stdT.ToString("0.00") & vbCrLf

                                                          End If
                                                      Next
                                                  End Sub)
                               End If

                           Else
                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      lblRepeatability.Text += "No Result" & vbCrLf

                                                      '20170602按鍵保護
                                                      btnRun.Enabled = True
                                                      btnImport.Enabled = True
                                                      btnACQ.Enabled = True
                                                      btnRepeatRun.Enabled = True
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
                                                  btnImport.Enabled = True
                                                  btnACQ.Enabled = True
                                                  btnRepeatRun.Enabled = True
                                                  btnReset.Enabled = True
                                                  btnOK.Enabled = True
                                                  btnCancel.Enabled = True

                                              End Sub)
                           End If
                       End Sub)
    End Sub

    Function GetValue(ByVal propertyName As String, ByVal defaultValue As Object) As Object
        For i As Integer = 0 To CogToolBlockEditV21.Subject.Outputs.Count - 1
            If propertyName = CogToolBlockEditV21.Subject.Outputs(i).Name Then
                Return CogToolBlockEditV21.Subject.Outputs(i).Value
            End If
        Next
        Return defaultValue
    End Function

    ''' <summary>委派 控制項操作物件更新</summary>
    ''' <param name="ctrl">目標控制項</param>
    ''' <param name="str">物件</param>
    ''' <remarks></remarks>
    Private Delegate Sub ImInvokeCogToolBLock(ByVal ctrl As Cognex.VisionPro.ToolBlock.CogToolBlockEditV2, ByVal [str] As Cognex.VisionPro.ToolBlock.CogToolBlock)
    ''' <summary>在擁有控制項基礎視窗控制代碼的執行緒上執行指定的委派</summary>
    ''' <param name="ctrl"></param>
    ''' <param name="str"></param>
    ''' <remarks></remarks>
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



#Region "光源操作"

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
#End Region

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'Me.Hide()
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close() 'Eason 20170223
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If btnOK.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnOK]" & vbTab & "Click")
        btnOK.Enabled = False
        Try
            Dim fileName As String
            Dim iniFileName As String
            'If lstScene.SelectedItem = "CALIB1" OrElse lstScene.SelectedItem = "CALIB2" OrElse lstScene.SelectedItem = "CALIB3" OrElse lstScene.SelectedItem = "CALIB4" Then '系統用場景路徑指向
            '    fileName = Application.StartupPath & "\System\" & MachineName & "\" & lstScene.SelectedItem & ".ini"
            'Else
            '    fileName = System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & lstScene.SelectedItem & ".ini" '光源設定檔路徑
            'End If

            Dim RecipeDirectoryName As String = Application.StartupPath & "\Scene\" & MachineName
            Dim SceneName As String = lstScene.SelectedItem
            If SceneName Is Nothing Then
                'sue0428
                '請選擇場景
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
                MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                btnOK.Enabled = True
                Exit Sub
            End If
            If SceneName.Length > 5 Then
                If SceneName.Substring(0, 5) = "CALIB" Then
                    fileName = Application.StartupPath & "\System\" & MachineName & "\" & SceneName & ".ini"
                Else
                    fileName = RecipeDirectoryName & "\" & SceneName & ".ini" 'System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & SceneName & ".ini" '光源設定檔路徑
                End If
            Else
                fileName = RecipeDirectoryName & "\" & SceneName & ".ini" 'System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & SceneName & ".ini" '光源設定檔路徑
            End If

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

            If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
                gAOICollection.SceneDictionary(SceneName) = mScene
            Else
                gAOICollection.SceneDictionary.Add(SceneName, mScene)
            End If
            gAOICollection.SaveSceneParameter(SceneName, fileName) '儲存光源,曝光值等設定

            'If lstScene.SelectedItem = "CALIB1" OrElse lstScene.SelectedItem = "CALIB2" OrElse lstScene.SelectedItem = "CALIB3" OrElse lstScene.SelectedItem = "CALIB4" Then '系統用場景路徑指向
            '    fileName = Application.StartupPath & "\System\" & MachineName & "\" & lstScene.SelectedItem & ".vpp"
            'Else
            '    fileName = System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & lstScene.SelectedItem & ".vpp"
            'End If
            If SceneName.Length > 5 Then
                If SceneName.Substring(0, 5) = "CALIB" Then
                    fileName = Application.StartupPath & "\System\" & MachineName & "\" & SceneName & ".vpp"
                Else
                    fileName = RecipeDirectoryName & "\" & SceneName & ".vpp" 'System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & SceneName & ".vpp"
                End If
            Else
                fileName = RecipeDirectoryName & "\" & SceneName & ".vpp" 'System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & SceneName & ".vpp"
            End If

            'Cognex.VisionPro.CogSerializer.SaveObjectToFile(CogToolBlockEditV21.Subject, fileName) '控制項存到選定的Scene.
            Cognex.VisionPro.CogSerializer.SaveObjectToFile(CogToolBlockEditV21.Subject, fileName, GetType(System.Runtime.Serialization.Formatters.Binary.BinaryFormatter), CogSerializationOptionsConstants.Results)

            If SceneName.Length > 5 Then
                If SceneName.Substring(0, 5) = "CALIB" Then
                    iniFileName = Application.StartupPath & "\System\" & MachineName & "\" & SceneName & ".ini"
                Else
                    iniFileName = RecipeDirectoryName & "\" & SceneName & ".ini" 'System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & SceneName & ".ini" '光源設定檔路徑
                End If
            Else
                iniFileName = RecipeDirectoryName & "\" & SceneName & ".ini" 'System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & SceneName & ".ini" '光源設定檔路徑
            End If
            'If lstScene.SelectedItem = "CALIB1" OrElse lstScene.SelectedItem = "CALIB2" OrElse lstScene.SelectedItem = "CALIB3" OrElse lstScene.SelectedItem = "CALIB4" Then '系統用場景路徑指向
            '    iniFileName = Application.StartupPath & "\System\" & MachineName & "\" & lstScene.SelectedItem & ".ini"
            'Else
            '    iniFileName = System.IO.Path.GetDirectoryName(Recipe.strFileName) & "\" & lstScene.SelectedItem & ".ini"
            'End If

            gAOICollection.SaveSceneOutputParam(Sys.CCDNo, SceneName, iniFileName)

            For mCCDNo As Integer = enmCCD.CCD1 To enmCCD.Max
                If gAOICollection.LoadVision(mCCDNo, fileName) = True Then '使用標準方式讀出 含掛載事件
                    gSyslog.Save("CCDNo:" & mCCDNo & " Save Image File(.vpp) as :" & fileName, , eMessageLevel.Information)
                    gSyslog.Save("CCDNo:" & mCCDNo & " Save Image File(.ini) as :" & iniFileName, , eMessageLevel.Information)
                Else
                    'Sue20170627
                    '存檔失敗 
                    'gSyslog.Save("CCDNo:" & mCCDNo & " " & SceneName & gMsgHandler.GetMessage(Warn_3000035))
                    MsgBox("CCDNo:" & mCCDNo & " " & SceneName & gMsgHandler.GetMessage(Warn_3000035), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'MsgBox("CCDNo:" & mCCDNo & " " & SceneName & " Save Failed.")
                    btnOK.Enabled = True
                    Exit Sub
                End If
            Next
            RecipeSceneName = lstScene.SelectedItem
            'sue0428
            '儲存成功
            gSyslog.Save(SceneName & gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(SceneName & gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnOK.Enabled = True
            'Me.Hide()
            'Eason 20170223 
            'Me.Close()
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message & " " & ex.StackTrace, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnOK.Enabled = True
        End Try
    End Sub


    Private Sub chkLight1_CheckedChanged(sender As Object, e As EventArgs) Handles chkLight1.CheckedChanged
        If SceneName = Nothing Then
            'sue0428
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            'sue0428
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
            'sue0428
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            'sue0428
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
            'sue0428
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            'sue0428
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
            'sue0428
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            'sue0428
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

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If btnReset.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnReset]" & vbTab & "Click")
        btnReset.Enabled = False
        mStatisticX.Clear()
        mStatisticY.Clear()
        mStatisticT.Clear()
        lblRepeatability.Text = ""
        Flag_Pause = True
        btnReset.Enabled = True
    End Sub

    Private Sub nmcExposure_ValueChanged(sender As Object, e As EventArgs) Handles nmcExposure.ValueChanged
        'gAOICollection.SetExposure(Sys.CCDNo, nmcExposure.Value) '設定曝光值

        gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照開  
        System.Threading.Thread.CurrentThread.Join(10)
        gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
    End Sub

    Private Async Sub btnRepeatRun_Click(sender As Object, e As EventArgs) Handles btnRepeatRun.Click
        'TODO: 需增加關Form的保護.
        If btnRepeatRun.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnRepeatRun]" & vbTab & "Click")
        btnRepeatRun.Enabled = False

        '20170602按鍵保護
        btnImport.Enabled = False
        btnACQ.Enabled = False
        btnRun.Enabled = False
        btnReset.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        


        mStatisticX.Clear()
        mStatisticY.Clear()
        mStatisticT.Clear()
        lblRepeatability.Text = ""


        If lstScene.SelectedItem Is Nothing Then '未選場景
            lstScene.BackColor = Color.Yellow
            lstScene.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.CurrentThread.Join(100)
            lstScene.BackColor = Color.White
            btnRepeatRun.Enabled = True

            '20170602按鍵保護
            btnImport.Enabled = True
            btnACQ.Enabled = True
            btnRun.Enabled = True
            btnReset.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True

            Exit Sub
        End If
        If CogToolBlockEditV21.Subject Is Nothing Then '計算工具不存在
            '工具不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000023))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnRepeatRun.Enabled = True

            '20170602按鍵保護
            btnImport.Enabled = True
            btnACQ.Enabled = True
            btnRun.Enabled = True
            btnReset.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True

            Exit Sub
        End If
        If gAOICollection.IsCCDExist(Sys.CCDNo) = False Then '取像工具不存在
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnRepeatRun.Enabled = True

            '20170602按鍵保護
            btnImport.Enabled = True
            btnACQ.Enabled = True
            btnRun.Enabled = True
            btnReset.Enabled = True
            btnOK.Enabled = True
            btnCancel.Enabled = True

            Exit Sub
        End If
        Dim mSceneID As String = lstScene.SelectedItem
        Await Task.Run(Sub()


                           For i As Integer = 0 To 99
                               'btnRun_Click(sender, e)
                               If Flag_Pause = True Then '按下暫停
                                   Flag_Pause = False
                                   Exit Sub
                               End If
                               'gAOICollection.SetExposure(Sys.CCDNo, nmcExposure.Value) '設定曝光值
                               gAOICollection.SetCCDScene(Sys.CCDNo, mSceneID) '選擇場景
                               gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
                               System.Threading.Thread.CurrentThread.Join(10)
                               gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eON, True, False) '觸發拍照開
                               System.Threading.Thread.CurrentThread.Join(10)
                               gAOICollection.SetCCDTrigger(Sys.CCDNo, enmONOFF.eOff, True, False) '觸發拍照關確保
                               Dim stopWatch As New Stopwatch
                               stopWatch.Restart()
                               Do
                                   System.Threading.Thread.Sleep(1)
                                   If stopWatch.ElapsedMilliseconds > gSSystemParameter.TimeOut1 Then
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
                                                              btnImport.Enabled = True
                                                              btnACQ.Enabled = True
                                                              btnRun.Enabled = True
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
                                                              btnImport.Enabled = True
                                                              btnACQ.Enabled = True
                                                              btnRun.Enabled = True
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
                                                                      btnImport.Enabled = True
                                                                      btnACQ.Enabled = True
                                                                      btnRun.Enabled = True
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
                                                                      btnImport.Enabled = True
                                                                      btnACQ.Enabled = True
                                                                      btnRun.Enabled = True
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

                               InvokeUcDisplay(UcDisplay1, gAOICollection, Sys, mSceneID, 0, 0, enmDisplayShape.Alignment) '更新控制項


                               If CogToolBlockEditV21.Subject.Inputs.Count = 0 Then '計算工具輸入不存在
                                   '工具輸入不存在
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000024))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2000024), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnRepeatRun.Enabled = True

                                                          '20170602按鍵保護
                                                          btnImport.Enabled = True
                                                          btnACQ.Enabled = True
                                                          btnRun.Enabled = True
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
                                                          btnImport.Enabled = True
                                                          btnACQ.Enabled = True
                                                          btnRun.Enabled = True
                                                          btnReset.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True

                                                      End Sub)
                                   End If
                                   Exit For
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
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          btnRepeatRun.Enabled = True

                                                          '20170602按鍵保護
                                                          btnImport.Enabled = True
                                                          btnACQ.Enabled = True
                                                          btnRun.Enabled = True
                                                          btnReset.Enabled = True
                                                          btnOK.Enabled = True
                                                          btnCancel.Enabled = True

                                                      End Sub)
                                   End If
                                   Exit For
                               End If

                               Dim ResultCount = CogToolBlockEditV21.Subject.Outputs("Results_Count").Value

                               If ResultCount > 0 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          Dim posX As Decimal = 0
                                                          Dim posY As Decimal = 0
                                                          Dim posT As Decimal = 0
                                                          For mResult As Integer = 0 To ResultCount - 1
                                                              posX = CogToolBlockEditV21.Subject.Outputs("Results_Item_0_TranslationX").Value
                                                              posY = CogToolBlockEditV21.Subject.Outputs("Results_Item_0_TranslationY").Value
                                                              posT = CogToolBlockEditV21.Subject.Outputs("Results_Item_0_Rotation").Value
                                                              lblRepeatability.Text = "Run Count:" & ResultCount & vbCrLf
                                                              lblRepeatability.Text += "Item" & vbTab & " " & "X(pixel)" & vbTab & " " & "Y(pixel)" & vbTab & " " & "Theta(Deg)" & vbCrLf
                                                              lblRepeatability.Text += "Now:" & vbTab & " " & posX.ToString("000.00") & vbTab & " " & posY.ToString("000.00") & vbTab & " " & posT.ToString("0.00") & vbCrLf

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

                                                                  lblRepeatability.Text += "Avg: " & vbTab & " " & avgX.ToString("000.00") & vbTab & " " & avgY.ToString("000.00") & vbTab & " " & avgT.ToString("0.00") & vbCrLf
                                                                  lblRepeatability.Text += "Max: " & vbTab & " " & mStatisticX.Max.ToString("000.00") & vbTab & " " & mStatisticY.Max.ToString("000.00") & vbTab & " " & mStatisticT.Max.ToString("0.00") & vbCrLf
                                                                  lblRepeatability.Text += "Min: " & vbTab & " " & mStatisticX.Min.ToString("000.00") & vbTab & " " & mStatisticY.Min.ToString("000.00") & vbTab & " " & mStatisticT.Min.ToString("0.00") & vbCrLf
                                                                  lblRepeatability.Text += "Std: " & vbTab & " " & stdX.ToString("000.00") & vbTab & " " & stdY.ToString("000.00") & vbTab & " " & stdT.ToString("0.00") & vbCrLf
                                                              End If


                                                          Next
                                                      End Sub)
                                   End If

                               Else
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          lblRepeatability.Text += "No Result" & vbCrLf
                                                          btnRepeatRun.Enabled = True

                                                          '20170602按鍵保護
                                                          btnImport.Enabled = True
                                                          btnACQ.Enabled = True
                                                          btnRun.Enabled = True
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
                                                  btnImport.Enabled = True
                                                  btnACQ.Enabled = True
                                                  btnRun.Enabled = True
                                                  btnReset.Enabled = True
                                                  btnOK.Enabled = True
                                                  btnCancel.Enabled = True

                                              End Sub)
                           End If

                       End Sub)
    End Sub
End Class
﻿Imports System

Imports ProjectCore
Imports ProjectMotion
Imports ProjectRecipe
Imports ProjectIO
Imports ProjectAOI

Imports Cognex.VisionPro
Imports Cognex.VisionPro.ToolGroup
Imports Cognex.VisionPro.ToolBlock
Imports Cognex.VisionPro.PMAlign
Imports Cognex.VisionPro.PixelMap
Imports Cognex.VisionPro.Blob
Imports Cognex.VisionPro.CalibFix
Imports Cognex.VisionPro.Caliper


Public Class frmAlignModule
    Dim ClassName As String = "frmAlignModule"

    Public Sys As sSysParam
    ''' <summary>CCD動態顯示頁籤</summary>
    ''' <remarks></remarks>
    Const TabCCD As Integer = 0
    ''' <summary>定位特徵膠導頁籤</summary>
    ''' <remarks></remarks>
    Const TabAlignment As Integer = 1
    '''<summary>預選場景</summary>
    ''' <remarks></remarks>
    Public SceneName As String
    ''' <summary>上一組場景</summary>
    ''' <remarks></remarks>
    Private PreSceneName As String
    ''' <summary>上一組場景</summary>
    ''' <remarks></remarks>
    Public RecipeSceneName As String

    Public IsRecipeScene As Boolean

    Private Flag_Pause As Boolean = False

    Private flag_ImageSourceACQ As Boolean = True

    '[Note]模組化參數
    Private CogToolBlock As Object = Nothing
    Private myToolBlock As CogToolBlock
    Private myPixelMapTool As CogPixelMapTool
    Private myPMTool As CogPMAlignTool
    Private myFixtureTool As CogFixtureTool
    Private myBlobTool As CogBlobTool
    Private myFindCircleTool As CogFindCircleTool
    Private myFindLineTool_1 As CogFindLineTool
    Private myFindLineTool_2 As CogFindLineTool
    '介面中選到的場景
    Private mySceneName As String
    '[Note]紀錄是否載完頁面
    Private Flag_frmLoadDone As Boolean = False

    Public Structure enmAlignToolBlock
        ''' <summary>
        ''' 灰階工具
        ''' </summary>
        ''' <remarks></remarks>
        Public myPixelMapTool As CogPixelMapTool
        ''' <summary>
        ''' 定位工具
        ''' </summary>
        ''' <remarks></remarks>
        Public myPMTool As CogPMAlignTool
        ''' <summary>
        ''' 座標工具
        ''' </summary>
        ''' <remarks></remarks>
        Public myFixtureTool As CogFixtureTool
        ''' <summary>
        ''' 找圓工具
        ''' </summary>
        ''' <remarks></remarks>
        Public myFindCircleTool As CogFindCircleTool
    End Structure

    Dim AlignToolBlock As enmAlignToolBlock

    Private Sub frmAlignModule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Flag_frmLoadDone = False

        Select Case gAOICollection.GetCCDType(enmCCD.CCD1) 'gSSystemParameter.enmCCDType
            Case enmCCDType.CognexVPRO
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Cognex)
            Case enmCCDType.OmronFZS2MUDP
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Omronx64)
            Case Else
                UcDisplay1.ShowDisplay(ProjectAOI.ucDisplay.DisplayType.Picture)
        End Select

        '[Note]光源控制
        Dim mSceneName As String
        UcLightControl1.CCDNo = Sys.CCDNo
        If SceneName = "" Or SceneName Is Nothing Then
            '[Note]場景不存在，載入預設場景
            mSceneName = "CALIB" & (Sys.CCDNo + 1).ToString '預設CALIB校正場景光源
        Else
            mSceneName = SceneName
        End If
        If gAOICollection.SceneDictionary.ContainsKey(mSceneName) Then
            '[Note]:這是用來方便操作用，故只要塞一個存在的場景即可。
            UcLightControl1.SceneName = mSceneName
            UcLightControl1.ShowUI()
        Else
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000020))
            MsgBox(gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If


        If IsRecipeScene Then
            '[Note]Recipe場景
            '[Note]按健保護
            grpSetLight.Enabled = False
            grpImageSource.Enabled = False
            grpText.Enabled = False
            TabControlImage.Enabled = False
            '[Note]顯示定位場景清單
            lstScene.Items.Clear()
            lstScene.Items.AddRange(gAOICollection.GetSceneList)
            If Not SceneName Is Nothing Then
                If lstScene.Items.Contains(SceneName) Then
                    lstScene.SelectedItem = SceneName
                    RecipeSceneName = SceneName '保存場景名稱
                End If
            End If
            grpCalibScene.Visible = False
            grpSelectScene.Visible = True

        Else
            '[Note]工程場景
            grpCalibScene.Visible = True
            grpSelectScene.Visible = False
            txtCalibScene.Text = SceneName
            LoadSelectScene(SceneName)

        End If

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

        Flag_frmLoadDone = True
    End Sub

    Private Sub frmAlignModule_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        CogDisplay1.Dispose()
        UcDisplay1.EndLive()
        UcDisplay1.ManualDispose()
        Me.Dispose(True)

    End Sub



    Public Function GetString(ByVal value As String) As String
        Select Case value
            Case "Please Check Image Source"
                Select Case gSSystemParameter.LanguageType
                    Case enmLanguageType.eEnglish
                        Return "Please Check Image Source"
                    Case enmLanguageType.eSimplifiedChinese
                        Return "请检査影像來源"
                    Case enmLanguageType.eTraditionalChinese
                        Return "請檢察影像來源"
                End Select

            Case Else
                Return "No String"
        End Select
        Return "No String"
    End Function

    ''' <summary>
    ''' 場景工具清除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SceneToolClear()
        myToolBlock = Nothing
        myPixelMapTool = Nothing
        myPMTool = Nothing
        myFixtureTool = Nothing
        myFindCircleTool = Nothing
        myBlobTool = Nothing
        myFindLineTool_1 = Nothing
        myFindLineTool_2 = Nothing
    End Sub

    ''' <summary>
    ''' 工具關閉顯示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SceneGrpUIClear()
        grpThreshold.Visible = False
        grpPatternArea.Visible = False
        grpBlob.Visible = False
        grpCaliper.Visible = False
        'grpRadiusTolerance.Visible = False
    End Sub


    ''' <summary>
    ''' 按鈕保護
    ''' </summary>
    ''' <param name="disable"></param>
    ''' <remarks></remarks>
    Public Sub Control_Btn(ByVal disable As Boolean)
        'Me.BeginInvoke(Sub()
        '[Note]1.場景
        btnSceneAdd.Enabled = disable
        btnSceneDel.Enabled = disable
        btnLoadScene.Enabled = disable
        '[Note]2.光源


        '[Note]3.影像來源
        btnACQ.Enabled = disable
        btnImport.Enabled = disable

        '[Note]5.測試
        btnRun.Enabled = disable
        btnRepeatRun.Enabled = disable

        '[Note]存讀檔
        btnCancel.Enabled = disable
        btnOK.Enabled = disable
        'End Sub)

    End Sub

    ''' <summary>
    ''' 場景教導保護
    ''' </summary>
    ''' <param name="disable"></param>
    ''' <remarks></remarks>
    Public Sub TabAlign_Control(ByVal disable As Boolean)
        'Me.BeginInvoke(Sub()
        '[Note]1.灰階
        grpThreshold.Enabled = disable

        '[Note]2.PMAlign

        '[Note]3.卡尺
        grpCaliper.Enabled = disable

    End Sub

    Private Sub CogDisplay1_GraphicsClear()
        If CogDisplay1 IsNot Nothing Then
            If CogDisplay1.InteractiveGraphics.Count > 0 Then
                CogDisplay1.InteractiveGraphics.Clear()
            End If
            CogDisplay1.StaticGraphics.Clear()
        End If
    End Sub




#Region "模組介面功能"
    Private Sub btnLoadScene_Click(sender As Object, e As EventArgs) Handles btnLoadScene.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        If IsNothing(SceneName) Then
            Exit Sub
        End If

        btnLoadScene.Enabled = False
        Control_Btn(False)

        '[Note]可選擇不同的定位方式
        Dim AlignType As eAlignType
        Dim ScenePath As String
        Dim mfrmAlignType As frmAlignType
        mfrmAlignType = New frmAlignType
        With mfrmAlignType
            .StartPosition = FormStartPosition.CenterScreen
            .Location = New Point(0, 0)
            .ShowDialog()
            ScenePath = .mScenePath
            AlignType = .mAlignType
        End With

        '存場景檔案 存在機台系統檔
        Dim RecipeDirectoryName As String = Application.StartupPath & "\System\" & MachineName
        Dim fileName = RecipeDirectoryName & "\" & SceneName & ".ini" 'System.IO.Path.GetDirectoryName(gCRecipe.strFileName) & "\" & txtScene.Text & ".ini" '光源設定檔路徑
        Dim mScene As New CSceneParameter
        With mScene
            .AlignType = AlignType
            .LightValue(0) = UcLightControl1.GetLight1Value
            .LightValue(1) = UcLightControl1.GetLight2Value
            .LightValue(2) = UcLightControl1.GetLight3Value
            .LightValue(3) = UcLightControl1.GetLight4Value

            .LightEnable(0) = UcLightControl1.GetLight1_OnOff
            .LightEnable(1) = UcLightControl1.GetLight2_OnOff
            .LightEnable(2) = UcLightControl1.GetLight3_OnOff
            .LightEnable(3) = UcLightControl1.GetLight4_OnOff
            '.CCDExposureTime = nmcExposure.Value
        End With
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            gAOICollection.SceneDictionary(SceneName) = mScene
        Else
            gAOICollection.SceneDictionary.Add(SceneName, mScene)
        End If
        gAOICollection.SaveSceneParameter(SceneName, fileName)
        gAOICollection.LoadSceneParameter(SceneName, fileName)

        SceneGrpUIClear()
        gAOICollection.RemoveScene(Sys.CCDNo, SceneName)
        gAOICollection.CreateScene(Sys.CCDNo, SceneName, AlignType, ScenePath) '建立場景物件

        LoadSelectScene(SceneName)

        btnLoadScene.Enabled = True
        Control_Btn(True)
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "ClickEnd")
    End Sub


    Private Function IsSceneExist(ByVal mySceneNane As String) As Boolean
        Dim SceneList As String()
        SceneList = gAOICollection.GetSceneList() '[Note] 取得所有場景名稱

        Dim mSceneName, tmpString As String
        mSceneName = mySceneNane.ToUpper
        For i As Integer = 0 To SceneList.Length - 1
            tmpString = SceneList(i).ToUpper '[Note]全部轉大寫比對
            If mSceneName = tmpString Then
                Return True
            End If
        Next
        Return False
    End Function


    ''' <summary>
    ''' 場景新增
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSceneAdd_Click(sender As Object, e As EventArgs) Handles btnSceneAdd.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnSceneAdd.Enabled = False
        Control_Btn(False)

        If txtScene.Text = "" Then
            'sue0428
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000023))
            MsgBox(gMsgHandler.GetMessage(Warn_3000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSceneAdd.Enabled = True
            Control_Btn(True)
            Exit Sub
        End If

        If IsSceneExist(txtScene.Text) Then  'gAOICollection.IsSceneExist(Sys.CCDNo, txtScene.Text) Then
            '場景ID已經存在!
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000067))
            MsgBox(gMsgHandler.GetMessage(Warn_3000067), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSceneAdd.Enabled = True
            Control_Btn(True)
            Exit Sub
        End If

        TabControlImage.SelectTab(TabCCD) '切到即時影像
        TabControlImage.Enabled = False

        Dim AlignType As eAlignType
        Dim ScenePath As String
        Dim mfrmAlignType As frmAlignType
        mfrmAlignType = New frmAlignType
        With mfrmAlignType
            .StartPosition = FormStartPosition.CenterScreen
            .Location = New Point(0, 0)
            .ShowDialog()
            ScenePath = .mScenePath
            AlignType = .mAlignType
        End With

        SceneGrpUIClear()
        gAOICollection.CreateScene(Sys.CCDNo, txtScene.Text, AlignType, ScenePath) '建立場景物件
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
                UcLightControl1.SetLight1Value(gAOICollection.SceneDictionary(PreSceneName).LightValue(enmValveLight.No1))
                UcLightControl1.SetLight2Value(gAOICollection.SceneDictionary(PreSceneName).LightValue(enmValveLight.No2))
                UcLightControl1.SetLight3Value(gAOICollection.SceneDictionary(PreSceneName).LightValue(enmValveLight.No3))
                UcLightControl1.SetLight4Value(gAOICollection.SceneDictionary(PreSceneName).LightValue(enmValveLight.No4))
                UcLightControl1.SetLight1_OnOff(gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No1))
                UcLightControl1.SetLight2_OnOff(gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No2))
                UcLightControl1.SetLight3_OnOff(gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No3))
                UcLightControl1.SetLight4_OnOff(gAOICollection.SceneDictionary(PreSceneName).LightEnable(enmValveLight.No4))
                'nmcExposure.Value = gAOICollection.SceneDictionary(PreSceneName).CCDExposureTime
            Else
                UcLightControl1.SetLight1Value(100)
                UcLightControl1.SetLight2Value(0)
                UcLightControl1.SetLight3Value(0)
                UcLightControl1.SetLight4Value(0)
                UcLightControl1.SetLight1_OnOff(True)
                UcLightControl1.SetLight2_OnOff(False)
                UcLightControl1.SetLight3_OnOff(False)
                UcLightControl1.SetLight4_OnOff(False)
                'nmcExposure.Value = 5
            End If
        Else
            UcLightControl1.SetLight1Value(100)
            UcLightControl1.SetLight2Value(0)
            UcLightControl1.SetLight3Value(0)
            UcLightControl1.SetLight4Value(0)
            UcLightControl1.SetLight1_OnOff(True)
            UcLightControl1.SetLight2_OnOff(False)
            UcLightControl1.SetLight3_OnOff(False)
            UcLightControl1.SetLight4_OnOff(False)
            'nmcExposure.Value = 5
        End If


        '存場景檔案
        Dim RecipeDirectoryName As String = Application.StartupPath & "\Scene\" & MachineName
        Dim fileName = RecipeDirectoryName & "\" & SceneName & ".ini" 'System.IO.Path.GetDirectoryName(gCRecipe.strFileName) & "\" & txtScene.Text & ".ini" '光源設定檔路徑
        Dim mScene As New CSceneParameter
        With mScene
            .AlignType = AlignType
            .LightValue(0) = UcLightControl1.GetLight1Value
            .LightValue(1) = UcLightControl1.GetLight2Value
            .LightValue(2) = UcLightControl1.GetLight3Value
            .LightValue(3) = UcLightControl1.GetLight4Value

            .LightEnable(0) = UcLightControl1.GetLight1_OnOff
            .LightEnable(1) = UcLightControl1.GetLight2_OnOff
            .LightEnable(2) = UcLightControl1.GetLight3_OnOff
            .LightEnable(3) = UcLightControl1.GetLight4_OnOff
            '.CCDExposureTime = nmcExposure.Value
        End With
        If SceneName Is Nothing Then
            'sue0428
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Select Scene, First! Save Failed!")
            btnOK.Enabled = True
            Control_Btn(True)
            Exit Sub
        End If
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            gAOICollection.SceneDictionary(SceneName) = mScene
        Else
            gAOICollection.SceneDictionary.Add(SceneName, mScene)
        End If

        gAOICollection.SaveSceneParameter(txtScene.Text, fileName)
        gAOICollection.LoadSceneParameter(txtScene.Text, fileName)
        Dim file = RecipeDirectoryName & "\" & txtScene.Text & ".vpp" 'System.IO.Path.GetDirectoryName(gCRecipe.strFileName) & "\" & txtScene.Text & ".vpp" '場景vpp設定檔路徑
        Cognex.VisionPro.CogSerializer.SaveObjectToFile(myToolBlock, file, GetType(System.Runtime.Serialization.Formatters.Binary.BinaryFormatter), CogSerializationOptionsConstants.Results)

        gAOICollection.LoadSceneList("") '更新存在場景字串
        txtScene.Text = Nothing

        Select Case gAOICollection.SceneDictionary(SceneName).AlignType
            Case eAlignType.LoadFile
                '[Note]不開放介面操作，只能看定位結果
                SceneGrpUIClear()
        End Select



        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnSceneAdd]" & vbTab & "ClickEnd")
        btnSceneAdd.Enabled = True
        Control_Btn(True)
    End Sub

    ''' <summary>
    ''' 場景刪除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSceneDel_Click(sender As Object, e As EventArgs) Handles btnSceneDel.Click
        If btnSceneDel.Enabled = False Then
            Exit Sub
        End If
        btnSceneDel.Enabled = False
        Control_Btn(False)
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnSceneDel]" & vbTab & "Click")
        Dim mSceneName As String = lstScene.SelectedItem
        If Not gAOICollection.IsSceneExist(Sys.CCDNo, mSceneName) Then
            'sue0428
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSceneDel.Enabled = True
            Control_Btn(True)
        End If
        If MsgBox("Do you want to delete " & mSceneName, MsgBoxStyle.OkCancel + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = MsgBoxResult.Cancel Then
            Control_Btn(True)
            Exit Sub
        End If

        gAOICollection.RemoveScene(Sys.CCDNo, lstScene.SelectedItem) '刪除場景物件
        gAOICollection.SceneDictionary.Remove(lstScene.SelectedItem) '刪除光源物件
        lstScene.Items.Remove(lstScene.SelectedItem)

        If mSceneName Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnSceneDel.Enabled = True
            Control_Btn(True)
            Exit Sub
        End If
        Dim fileName As String
        Dim RecipeDirectoryName As String = Application.StartupPath & "\Scene\" & MachineName
        fileName = RecipeDirectoryName & "\" & mSceneName & ".ini"  '光源設定檔路徑
        If System.IO.File.Exists(fileName) Then
            System.IO.File.Delete(fileName)
        End If
        fileName = RecipeDirectoryName & "\" & mSceneName & ".vpp"  '影像設定檔路徑
        If System.IO.File.Exists(fileName) Then
            System.IO.File.Delete(fileName)
        End If
        gAOICollection.LoadSceneList("") '更新存在場景字串
        MsgBox(mSceneName & " Delete OK.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        TabControlImage.SelectTab(TabCCD) '切到即時影像
        TabControlImage.Enabled = False

        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnSceneDel]" & vbTab & "ClickEnd")
        btnSceneDel.Enabled = True
        Control_Btn(True)
    End Sub

    Private Function UpDateThreshold() As Boolean
        Try
            If myPixelMapTool IsNot Nothing Then
                'Dim inputUpThreshold, outputUpThreshold As Decimal
                'Dim inputDownThreshold, outputDownThreshold As Decimal
                If myPixelMapTool.RunParams.NumReferencePoints >= 2 Then
                    'myPixelMapTool.RunParams.GetReferencePointOutputAbsolute(0, inputDownThreshold, outputDownThreshold)
                    'myPixelMapTool.RunParams.GetReferencePoint(1, inputUpThreshold, outputUpThreshold)

                    'Debug.Print("myPixelMapTool.RunParams.GetReferencePointInputAbsolute(0)" & myPixelMapTool.RunParams.GetReferencePointInputAbsolute(0))
                    'Debug.Print("myPixelMapTool.RunParams.GetReferencePointInputAbsolute(1)" & myPixelMapTool.RunParams.GetReferencePointInputAbsolute(1))
                    'Debug.Print("myPixelMapTool.RunParams.GetReferencePointOutputAbsolute(0)" & myPixelMapTool.RunParams.GetReferencePointOutputAbsolute(0))
                    'Debug.Print("myPixelMapTool.RunParams.GetReferencePointOutputAbsolute(1)" & myPixelMapTool.RunParams.GetReferencePointOutputAbsolute(1))

                    nmcInputUpThreshold.Value = GetSafeThreshold(myPixelMapTool.RunParams.GetReferencePointInputAbsolute(1))
                    nmcOutputUpThreshold.Value = GetSafeThreshold(myPixelMapTool.RunParams.GetReferencePointOutputAbsolute(1))
                    nmcInputDownThreshold.Value = GetSafeThreshold(myPixelMapTool.RunParams.GetReferencePointInputAbsolute(0))
                    nmcOutputDownThreshold.Value = GetSafeThreshold(myPixelMapTool.RunParams.GetReferencePointOutputAbsolute(0))
                    myPixelMapTool.Run()

                End If
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function

    Private Function UpDatePMAlign() As Boolean
        Try
            If myPMTool IsNot Nothing Then
                '[Note]相似度
                nmcMatchRate.Value = (myPMTool.RunParams.AcceptThreshold * 100) '(myToolBlock.Inputs("AcceptScore").Value * 100) '
                '[Note]角度
                If myPMTool.RunParams.ZoneAngle.Configuration = CogPMAlignZoneConstants.Nominal Then
                    nmcRotation.Value = 0
                Else
                    nmcRotation.Value = myPMTool.RunParams.ZoneAngle.High * 180 / Math.PI
                End If
                '[Note]縮放
                If myPMTool.RunParams.ZoneScale.Configuration = CogPMAlignZoneConstants.Nominal Then
                    nmcScale.Value = 0
                Else
                    nmcScale.Value = myPMTool.RunParams.ZoneScale.High - 1
                End If
                '[Note] Pattern Region
                If myPMTool.Pattern.TrainRegion IsNot Nothing Then
                    'nmcPatternAreaW.Value = myPMTool.Pattern.TrainImage.
                    Dim typ = myPMTool.Pattern.TrainRegion.GetType()
                    Dim mtmpRect = myPMTool.Pattern.TrainRegion
                    Select Case typ.FullName
                        Case "Cognex.VisionPro.CogRectangle"
                            Dim tmp As CogRectangle = CType(mtmpRect, CogRectangle)
                            nmcPatternAreaW.Value = tmp.Width '教導Pattern大小
                            nmcPatternAreaH.Value = tmp.Height '教導Pattern大小
                    End Select
                End If
                If myPMTool.Pattern.Trained Then
                    txtTrainMsg.Text = "Trained"
                    txtTrainMsg.ForeColor = Color.Black
                Else
                    txtTrainMsg.Text = "Untrained"
                    txtTrainMsg.ForeColor = Color.Red
                    grpCaliper.Enabled = False
                End If

            End If
            Return True
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function


    Private Function UpDateFindCircle(ByVal myFindCircleTool As CogFindCircleTool) As Boolean
        Try
            If myFindCircleTool IsNot Nothing Then
                nmcCalipersNumber.Value = myFindCircleTool.RunParams.NumCalipers
                nmcCalipersIgnoreNumber.Value = myFindCircleTool.RunParams.NumToIgnore
                nmcSearchLength.Value = Math.Ceiling(myFindCircleTool.RunParams.CaliperSearchLength) '取整數
                nmcProjectionLength.Value = Math.Ceiling(myFindCircleTool.RunParams.CaliperProjectionLength) '取整數
                cmbCaplipersSearchDirection.SelectedIndex = myFindCircleTool.RunParams.CaliperSearchDirection
                cmbCaplipersPolarity.SelectedIndex = (myFindCircleTool.RunParams.CaliperRunParams.Edge0Polarity - 1)
                nmcContrastThreshold.Value = myFindCircleTool.RunParams.CaliperRunParams.ContrastThreshold
                nmclblFilterHalfSizePixels.Value = myFindCircleTool.RunParams.CaliperRunParams.FilterHalfSizeInPixels
            End If
            Return True
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try

    End Function

    Private Function UpDateFindCorner(ByVal myFindLineTool As CogFindLineTool) As Boolean
        Try
            If myFindLineTool IsNot Nothing Then
                nmcCalipersNumber.Value = myFindLineTool.RunParams.NumCalipers
                nmcCalipersIgnoreNumber.Value = myFindLineTool.RunParams.NumToIgnore
                nmcSearchLength.Value = Math.Ceiling(myFindLineTool.RunParams.CaliperSearchLength) '取整數
                nmcProjectionLength.Value = Math.Ceiling(myFindLineTool.RunParams.CaliperProjectionLength) '取整數
                Dim Deg = myFindLineTool.RunParams.CaliperSearchDirection
                If Deg > 0 Then
                    cmbCaplipersSearchDirection.SelectedIndex = 0
                Else
                    cmbCaplipersSearchDirection.SelectedIndex = 1
                End If
                cmbCaplipersPolarity.SelectedIndex = (myFindLineTool.RunParams.CaliperRunParams.Edge0Polarity - 1)
                nmcContrastThreshold.Value = myFindLineTool.RunParams.CaliperRunParams.ContrastThreshold
                nmclblFilterHalfSizePixels.Value = myFindLineTool.RunParams.CaliperRunParams.FilterHalfSizeInPixels
            End If
            Return True
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try

    End Function

    Private Function UpDateBlob() As Boolean
        Try
            If myBlobTool IsNot Nothing Then
                cmbBlobPolarity.SelectedIndex = myBlobTool.RunParams.SegmentationParams.Polarity
                nmcBlobHighThreshold.Value = myBlobTool.RunParams.SegmentationParams.SoftFixedThresholdHigh
                nmcBlobLowThreshold.Value = myBlobTool.RunParams.SegmentationParams.SoftFixedThresholdLow
                nmcMinArea.Value = myBlobTool.RunParams.ConnectivityMinPixels
            End If
            Return True
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function


    Private Function LoadSelectScene(ByVal mScene As String) As Boolean

        TabControlImage.SelectTab(TabCCD) '切到即時影像
        TabControlImage.Enabled = False

        Dim fileName As String
        'Dim RecipeDirectoryName As String = Application.StartupPath & "\Scene\" & MachineName
        'If mScene.Length > 5 Then
        '    If mScene.Substring(0, 5) = "CALIB" Then
        '        fileName = Application.StartupPath & "\System\" & MachineName & "\" & mScene & ".ini"
        '    Else
        '        fileName = RecipeDirectoryName & "\" & mScene & ".ini" 'System.IO.Path.GetDirectoryName(gCRecipe.strFileName) & "\" & SceneName & ".ini" '光源設定檔路徑
        '    End If
        'Else
        '    fileName = RecipeDirectoryName & "\" & mScene & ".ini" 'System.IO.Path.GetDirectoryName(gCRecipe.strFileName) & "\" & SceneName & ".ini" '光源設定檔路徑
        'End If
        Dim ScenefilePath As String = Application.StartupPath & "\Scene\" & MachineName
        Dim CalibfilePath As String = Application.StartupPath & "\System\" & MachineName
        If IsRecipeScene Then
            '[Note]場景
            fileName = ScenefilePath & "\" & SceneName & ".ini"
        Else
            '[Note]校正
            fileName = CalibfilePath & "\" & SceneName & ".ini"

        End If

        gAOICollection.LoadSceneParameter(mScene, fileName) '讀取光源等設定
        If gAOICollection.SceneDictionary.ContainsKey(mScene) Then
            'UcLightControl1.CCDNo = Sys.CCDNo
            UcLightControl1.SceneName = mScene
            UcLightControl1.ShowUI()
        End If

        If gAOICollection.IsSceneExist(Sys.CCDNo, mScene) = False Then '場景不存在
            Dim file As String
            If IsRecipeScene Then
                file = ScenefilePath & "\" & mScene & ".vpp" 'System.IO.Path.GetDirectoryName(gCRecipe.strFileName) & "\" & SceneName & ".vpp"
            Else
                file = CalibfilePath & "\" & mScene & ".vpp"
            End If

            If gAOICollection.LoadVision(Sys.CCDNo, file) = False Then
                gSyslog.Save(mScene & gMsgHandler.GetMessage(Warn_3000020))
                MsgBox(mScene & gMsgHandler.GetMessage(Warn_3000020), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Return False
            End If
        End If

        '[Note]場景工具清除
        SceneToolClear()
        SceneGrpUIClear()

        myToolBlock = gAOICollection.GetToolBlock(Sys.CCDNo, mScene)
        If myToolBlock Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000007))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000007), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
            'Else
            '    '[Note]增加輸入Input for 使用者設定分數
            '    If myToolBlock.Inputs.Contains("AcceptScore") Then
            '    Else
            '        Dim AcceptScore As Cognex.VisionPro.ToolBlock.CogToolBlockTerminal
            '        AcceptScore = New CogToolBlockTerminal("AcceptScore", 0.8)
            '        myToolBlock.Inputs.Add(AcceptScore)
            '    End If
        End If


        For i As Integer = 0 To myToolBlock.Tools.Count - 1
            If myToolBlock.Tools(i).GetType = GetType(CogPixelMapTool) And myPixelMapTool Is Nothing Then
                '[Note]二值化工具
                myPixelMapTool = New CogPixelMapTool
                myPixelMapTool = myToolBlock.Tools(i)
                'AlignToolBlock.myPixelMapTool = myPixelMapTool
                grpThreshold.Visible = True
                Continue For
            End If
            If myToolBlock.Tools(i).GetType = GetType(CogPMAlignTool) And myPMTool Is Nothing Then
                '[Note]定位工具
                myPMTool = New CogPMAlignTool
                myPMTool = myToolBlock.Tools(i)
                grpPatternArea.Visible = True
                Continue For
            End If
            If myToolBlock.Tools(i).GetType = GetType(CogFixtureTool) And myFixtureTool Is Nothing Then
                '[Note]座標工具
                myFixtureTool = New CogFixtureTool
                myFixtureTool = myToolBlock.Tools(i)
                Continue For
            End If
            If myToolBlock.Tools(i).GetType = GetType(CogBlobTool) And myBlobTool Is Nothing Then
                '[Note]面積工具
                myBlobTool = New CogBlobTool
                myBlobTool = myToolBlock.Tools(i)
                grpBlob.Visible = True
                Continue For
            End If
            If myToolBlock.Tools(i).GetType = GetType(CogFindCircleTool) And myFindCircleTool Is Nothing Then
                '[Note]找圓工具
                myFindCircleTool = New CogFindCircleTool
                myFindCircleTool = myToolBlock.Tools(i)
                grpCaliper.Visible = True
                grpCaliper.Enabled = False
                'grpRadiusTolerance.Visible = True
                'grpRadiusTolerance.Enabled = False
                Continue For
            End If
            If myToolBlock.Tools(i).GetType = GetType(CogFindLineTool) And myFindLineTool_1 Is Nothing Then
                '[Note]找線工具一
                myFindLineTool_1 = New CogFindLineTool
                myFindLineTool_1 = myToolBlock.Tools(i)
                grpCaliper.Visible = True
                grpCaliper.Enabled = False
                Continue For
            End If
            If myToolBlock.Tools(i).GetType = GetType(CogFindLineTool) And myFindLineTool_2 Is Nothing Then
                '[Note]找線工具二
                myFindLineTool_2 = New CogFindLineTool
                myFindLineTool_2 = myToolBlock.Tools(i)
                'grpCaliper.Visible = True
                'grpCaliper.Enabled = False
                Continue For
            End If

        Next


        '[Note]灰階重分佈工具
        UpDateThreshold()

        '[Note]PMAlign工具
        UpDatePMAlign()

        '[Note]Blob工具
        UpDateBlob()

        '[Note]卡尺工具工具
        Select Case gAOICollection.SceneDictionary(mScene).AlignType
            Case eAlignType.Circle, eAlignType.Blob
                UpDateFindCircle(myFindCircleTool)

            Case eAlignType.Corner, eAlignType.Lane
                UpDateFindCorner(myFindLineTool_1)

            Case eAlignType.LoadFile
                '[Note]不開放介面操作，只能看定位結果
                SceneGrpUIClear()
        End Select


        '[Note]開啟按鍵保護
        grpSetLight.Enabled = True
        grpImageSource.Enabled = True
        grpText.Enabled = True
        'TabControlImage.Enabled = True

        Return True
    End Function


    ''' <summary>
    ''' 更換場景
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lstScene_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstScene.SelectedIndexChanged

        If lstScene.SelectedItem Is Nothing Then
            Exit Sub
        End If
        SceneName = lstScene.SelectedItem
        LoadSelectScene(SceneName)


    End Sub


    ''' <summary>
    ''' 取像
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub btnACQ_Click(sender As Object, e As EventArgs) Handles btnACQ.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnACQ.Enabled = False
        If Not gAOICollection.IsCCDExist(Sys.CCDNo) Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000034))
            MsgBox(gMsgHandler.GetMessage(Warn_3000034), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnACQ.Enabled = True
            Exit Sub
        End If

        'Dim mScene As String
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Me)) Then
            If Me.IsRecipeScene Then
                'If lstScene.SelectedItem Is Nothing Then
                If SceneName Is Nothing Then
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                    '20170929 Toby_ Add 判斷
                    If (Not IsNothing(Me)) Then
                        Me.BeginInvoke(Sub()
                                           btnACQ.Enabled = True

                                       End Sub)
                    End If

                    Exit Sub

                End If
                'mScene = lstScene.SelectedItem
            Else
                'mScene = txtScene.Text
            End If
        End If


        flag_ImageSourceACQ = True

        hscFile.Visible = False
        lblFolder.Visible = False
        Await Task.Run(Sub()
                           Try

                               gAOICollection.SetCCDScene(Sys.CCDNo, SceneName)
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

                                                                  End Sub)
                                               End If
                                               Exit Sub
                                           End If
                                       End If
                                   End If
                               Loop Until gAOICollection.IsCCDCBusy(Sys.CCDNo) = False

                               'InvokeUcDisplay(UcDisplay1, gAOICollection, Sys, mScene, 0, 0, enmDisplayShape.Alignment) '更新控制項,必要條件 frmMain必須是實體
                               'CogToolBlockEditV21.Subject.Inputs("InputImage").Value = gAOICollection.CalibBoardCalibration(Sys.CCDNo, gAOICollection.GetAcqOutputImage(Sys.CCDNo), False, 0) '20170317Wenda gAOICollection.GetAcqOutputImage(Sys.CCDNo)


                           Catch ex As Exception
                               MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                               '20170929 Toby_ Add 判斷
                               If (Not IsNothing(Me)) Then
                                   Me.BeginInvoke(Sub()
                                                      btnACQ.Enabled = True

                                                  End Sub)
                               End If
                           End Try
                       End Sub)

        'gAOICollection.AcquirePicture(Sys, Sys.CCDNo, mScene)

        If myToolBlock.Inputs.Contains("InputImage") Then
            '[Note]取像
            myToolBlock.Inputs("InputImage").Value = gAOICollection.CalibBoardCalibration(Sys.CCDNo, gAOICollection.GetAcqOutputImage(Sys.CCDNo), False, 0)
            CogDisplay1.InteractiveGraphics.Clear()
            CogDisplay1.StaticGraphics.Clear()
            CogDisplay1.Image = myToolBlock.Inputs("InputImage").Value
            CogDisplay1.Fit(True)
            myToolBlock.Run()

        Else
            MsgBox("無取像結果", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
        TabControlImage.Enabled = True
        TabControlImage.SelectTab(TabAlignment) '切換到教導介面

        btnACQ.Enabled = True
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnACQ]" & vbTab & "ClickEnd")
    End Sub



    Private Function LoadImage(ByVal ImagePath As String) As Boolean
        If Not System.IO.File.Exists(ImagePath) Then
            Return False
        End If


        Dim ImageName As String = My.Computer.FileSystem.GetName(ImagePath)
        lblFolder.Visible = True
        lblFolder.Text = ImageName
        Dim bmp As New Bitmap(ImagePath) '讀取指定檔案

        Select Case bmp.PixelFormat
            Case Imaging.PixelFormat.Format8bppIndexed
                Debug.Print("Format8bppIndexed")
                Dim img As New Cognex.VisionPro.CogImage8Grey(bmp) '影像檔轉檔 bmp To CogImage
                UpDateImage(img)

            Case Imaging.PixelFormat.Format16bppGrayScale
                Debug.Print("Format16bppGrayScale")
                Dim img As New Cognex.VisionPro.CogImage8Grey(bmp) '影像檔轉檔 bmp To CogImage
                UpDateImage(img)


            Case Imaging.PixelFormat.Format24bppRgb
                Debug.Print("Format24bppRgb")
                Dim img As New Cognex.VisionPro.CogImage24PlanarColor(bmp)
                If myToolBlock Is Nothing Then
                    'sue0428
                    '請選擇場景
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Return False
                End If
                If Not myToolBlock.Inputs.Contains("InputImage") Then
                    '工具輸入影像不存在
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000025))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Return False
                End If

                CogDisplay1_GraphicsClear()
                TabControlImage.SelectTab(TabAlignment) '切換到教導介面
                myToolBlock.Inputs("InputImage").Value = img 'cogImage影像丟入CogToolBlock

                CogDisplay1.Image = myToolBlock.Inputs("InputImage").Value
                CogDisplay1.Fit(True)

        End Select
        Return True
    End Function


    ''' <summary>瀏覽檔名清單</summary>
    ''' <remarks></remarks>
    Dim mBrowserFileNameList As New List(Of String)
    ''' <summary>
    ''' 輸入圖片文件夾
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        '[Note]20170818 製程Eddie與Ernset決議不要選擇資料夾，只載入一張圖片，顯示檔名與副檔名即可

        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
        btnImport.Enabled = False
        Control_Btn(False)


        'Dim dialog As New FolderBrowserDialog
        ''dialog.Filter = "*.bmp|*.bmp"
        'If dialog.ShowDialog() <> Windows.Forms.DialogResult.OK Then '未確認 路徑維持原設定不變更
        '    Control_Btn(True)
        '    Exit Sub
        'End If
        'If System.IO.Directory.Exists(dialog.SelectedPath) = False Then '選擇路徑不存在
        '    '檔案不存在
        '    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000066))
        '    MsgBox(gMsgHandler.GetMessage(Warn_3000066))
        '    btnImport.Enabled = True
        '    Control_Btn(True)

        '    Exit Sub
        'End If


        Dim DefaultDirectory As String = "C:\"

        '[說明]:選取檔案
        With OFDLoadScene
            .InitialDirectory = DefaultDirectory
            .Filter = "圖檔 (*.bmp)|*.bmp"
            .FilterIndex = 2
            .RestoreDirectory = True
            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                Control_Btn(True)
                Exit Sub
            End If
            LoadImage(.FileName)
        End With



        flag_ImageSourceACQ = False
        btnImport.Enabled = True
        Control_Btn(True)
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "ClickEnd")

#If 0 Then '[Note]原本設計可以載入整個圖片的文件夾，點擊hscFile可更換圖片
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
        btnImport.Enabled = False
        Control_Btn(False)

        Dim dialog As New FolderBrowserDialog
        'dialog.Filter = "*.bmp|*.bmp"
        If dialog.ShowDialog() <> Windows.Forms.DialogResult.OK Then '未確認 路徑維持原設定不變更
            Control_Btn(True)
            Exit Sub
        End If
        If System.IO.Directory.Exists(dialog.SelectedPath) = False Then '選擇路徑不存在
            '檔案不存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000066))
            MsgBox(gMsgHandler.GetMessage(Warn_3000066))
            btnImport.Enabled = True
            Control_Btn(True)

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
        flag_ImageSourceACQ = False

        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "ClickEnd")
        btnImport.Enabled = True
        Control_Btn(True)

#End If
    End Sub

    Private Sub UpDateImage(ByVal Image As Cognex.VisionPro.CogImage8Grey)
        If myToolBlock Is Nothing Then
            'sue0428
            '請選擇場景
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
            MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not myToolBlock.Inputs.Contains("InputImage") Then
            '工具輸入影像不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000025))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        TabControlImage.Enabled = True
        TabControlImage.SelectTab(TabAlignment) '切換到教導介面
        myToolBlock.Inputs("InputImage").Value = Image 'cogImage影像丟入CogToolBlock
        myToolBlock.Run()

        CogDisplay1.InteractiveGraphics.Clear()
        CogDisplay1.StaticGraphics.Clear()
        CogDisplay1.Image = myToolBlock.Inputs("InputImage").Value
        CogDisplay1.Fit(True)
    End Sub



    Private Sub hscFile_Scroll(sender As Object, e As ScrollEventArgs) Handles hscFile.Scroll
        Dim fileName As String = mBrowserFileNameList(hscFile.Value)
        Dim fInfo As New System.IO.FileInfo(fileName)
        Dim ImageName As String = fInfo.Name.Replace(".bmp", "") '.Replace(".Vpp", "")
        lblFolder.Text = "Image:" & ImageName
        If Not System.IO.File.Exists(fileName) Then
            Exit Sub
        End If
        Dim bmp As New Bitmap(fileName) '讀取指定檔案

        Select Case bmp.PixelFormat
            Case Imaging.PixelFormat.Format8bppIndexed
                Debug.Print("Format8bppIndexed")
                Dim img As New Cognex.VisionPro.CogImage8Grey(bmp) '影像檔轉檔 bmp To CogImage
                UpDateImage(img)

            Case Imaging.PixelFormat.Format16bppGrayScale
                Debug.Print("Format16bppGrayScale")
                Dim img As New Cognex.VisionPro.CogImage8Grey(bmp) '影像檔轉檔 bmp To CogImage
                UpDateImage(img)


            Case Imaging.PixelFormat.Format24bppRgb
                Debug.Print("Format24bppRgb")
                Dim img As New Cognex.VisionPro.CogImage24PlanarColor(bmp)
                If myToolBlock Is Nothing Then
                    'sue0428
                    '請選擇場景
                    gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
                    MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Exit Sub
                End If
                If Not myToolBlock.Inputs.Contains("InputImage") Then
                    '工具輸入影像不存在
                    gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000025))
                    MsgBox(gMsgHandler.GetMessage(Alarm_2000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    Exit Sub
                End If

                CogDisplay1_GraphicsClear()
                TabControlImage.SelectTab(TabAlignment) '切換到教導介面
                myToolBlock.Inputs("InputImage").Value = img 'cogImage影像丟入CogToolBlock

                CogDisplay1.Image = myToolBlock.Inputs("InputImage").Value
                CogDisplay1.Fit(True)
        End Select
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
    ''' <summary>
    ''' 單拍看結果
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        '[Note]按鍵保護
        'btnRun.Enabled = False
        Control_Btn(False)
        UcDisplay1.CogDisplay1.StaticGraphics.Clear()
        CogDisplay1_GraphicsClear()

        If SceneName Is Nothing Then '未選場景
            lstScene.BackColor = Color.Yellow
            lstScene.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.CurrentThread.Join(100)
            lstScene.BackColor = Color.White
            '[Note]按鍵保護
            Control_Btn(True)
            Exit Sub
        End If
        If myToolBlock Is Nothing Then '計算工具不存在
            '工具不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000023))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            '[Note]按鍵保護
            Control_Btn(True)
            Exit Sub
        End If
        If myPMTool IsNot Nothing Then
            '沒訓練特徵
            If Not myPMTool.Pattern.Trained Then
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000009))
                MsgBox(gMsgHandler.GetMessage(Alarm_2000009), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                '[Note]按鍵保護
                Control_Btn(True)
                Exit Sub
            End If
        End If
        If gAOICollection.IsCCDExist(Sys.CCDNo) = False Then '取像工具不存在
            '取像工具不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            '[Note]按鍵保護
            Control_Btn(True)
            Exit Sub
        End If
        'gAOICollection.SetExposure(Sys.CCDNo, nmcExposure.Value) '設定曝光值
        'Dim mSceneID As String = lstScene.SelectedItem
        'TabControlImage.SelectTab(TabCCD)
        'Await Task.Run(Sub()
        gAOICollection.SetCCDScene(Sys.CCDNo, SceneName) '選擇場景
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
                                       '[Note]按鍵保護
                                       Control_Btn(True)
                                   End Sub)
                End If
                Exit Sub


            End If
            '[Note] EMO時跳出
            If gDICollection.GetState(enmDI.EMO) = True Then

                '20170929 Toby_ Add 判斷
                If (Not IsNothing(Me)) Then
                    Me.BeginInvoke(Sub()
                                       '[Note]按鍵保護
                                       Control_Btn(True)

                                   End Sub)

                End If
                Exit Sub

            Else
                If Sys.MachineNo = enmMachineStation.MachineA Then
                    If gDICollection.GetState(enmDI.EMS) = True Then

                        '20170929 Toby_ Add 判斷
                        If (Not IsNothing(Me)) Then
                            Me.BeginInvoke(Sub()
                                               '[Note]按鍵保護
                                               Control_Btn(True)
                                           End Sub)
                        End If
                        Exit Sub

                    End If
                ElseIf Sys.MachineNo = enmMachineStation.MachineB Then
                    If gDICollection.GetState(enmDI.EMS2) = True Then

                        '20170929 Toby_ Add 判斷
                        If (Not IsNothing(Me)) Then
                            Me.BeginInvoke(Sub()
                                               '[Note]按鍵保護
                                               Control_Btn(True)
                                           End Sub)
                        End If
                        Exit Sub

                    End If
                End If
            End If
        Loop Until gAOICollection.IsCCDCBusy(Sys.CCDNo) = False '等待CCD取像完成
        Debug.Print("IsCCDCBusy:" & stopWatch.ElapsedMilliseconds)



        '[Note]'計算工具輸入不存在
        If myToolBlock.Inputs.Count = 0 Then
            '工具輸入不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000024))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000024), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   '[Note]按鍵保護
                                   Control_Btn(True)
                               End Sub)
            End If
            Exit Sub


        End If

        '[Note]'輸入影像接口不存在
        If Not myToolBlock.Inputs.Contains("InputImage") Then
            '工具輸入影像不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000025))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   '[Note]按鍵保護
                                   Control_Btn(True)
                               End Sub)
            End If
            Exit Sub


        End If

        'If flag_ImageSourceACQ Then
        myToolBlock.Inputs("InputImage").Value = gAOICollection.CalibBoardCalibration(Sys.CCDNo, gAOICollection.GetAcqOutputImage(Sys.CCDNo), False, 0) '20170317Wenda gAOICollection.GetAcqOutputImage(Sys.CCDNo) '取像工具的結果套入
        CogDisplay1.Image = myToolBlock.Inputs("InputImage").Value
        CogDisplay1.Fit(True)
        'Debug.Print("myToolBlock.Inputs('InputImage').Value = acqObj.OutputImage:" & stopWatch.ElapsedMilliseconds)
        'End If

        'TabControl1.SelectTab(tabCCD)
        If myToolBlock.Tools.Count = 0 Then '如果計算工具內沒有工具
            '工具不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000023))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

            '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   '[Note]按鍵保護
                                   Control_Btn(True)
                               End Sub)
            End If
            Exit Sub


        End If



        'Dim AcceptScore As Decimal
        'If myToolBlock.Inputs.Contains("AcceptScore") Then
        '    AcceptScore = myToolBlock.Inputs("AcceptScore").Value
        'Else
        '    AcceptScore = 0.3
        'End If


        myToolBlock.Run() '計算工具 試算
        Debug.Print("myToolBlock.Run()" & stopWatch.ElapsedMilliseconds)
        Dim ResultCount = myToolBlock.Outputs("Results_Count").Value

        'If myToolBlock.Outputs("Results_Item_0_Score").Value > AcceptScore Then
        '    '[Note]找出特徵分數高於AcceptScore

        'Else
        '    '[Note]找出特徵分數低於可接受分數(AcceptScore)
        '    ResultCount = 0

        'End If

        If ResultCount = 1 Then
   '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
            Me.BeginInvoke(Sub()
                               Dim posX As Decimal = 0
                               Dim posY As Decimal = 0
                               Dim posT As Decimal = 0
                               Dim Score As Decimal = 0
                               For mResult As Integer = 0 To ResultCount - 1
                                   posX = myToolBlock.Outputs("Results_Item_0_TranslationX").Value
                                   posY = myToolBlock.Outputs("Results_Item_0_TranslationY").Value
                                   posT = myToolBlock.Outputs("Results_Item_0_Rotation").Value
                                   Score = myToolBlock.Outputs("Results_Item_0_Score").Value
                                   DrawCenter(posX, posY, 10)
                                   DrawAlignTypeRegion(myToolBlock, gAOICollection.SceneDictionary(SceneName).AlignType)
                                   InvokeUcDisplay(UcDisplay1, gAOICollection, Sys, SceneName, 0, 0, enmDisplayShape.Alignment) '更新控制項
                                   txtRepeatability.Text = "Count:" & ResultCount & " " & "Score:" & (Score * 100).ToString("00") & vbCrLf
                                   txtRepeatability.Text += "Item" & " " & "X(pixel)" & " " & "Y(pixel)" & " " & "Theta(Deg)" & vbCrLf
                                   txtRepeatability.Text += "Now:" & " " & posX.ToString("000.00") & " " & posY.ToString("000.00") & " " & posT.ToString("0.00") & vbCrLf
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

                                       txtRepeatability.Text += "Avg: " & " " & avgX.ToString("000.00") & " " & avgY.ToString("000.00") & " " & avgT.ToString("0.00") & vbCrLf
                                       txtRepeatability.Text += "Max: " & " " & mStatisticX.Max.ToString("000.00") & " " & mStatisticY.Max.ToString("000.00") & " " & mStatisticT.Max.ToString("0.00") & vbCrLf
                                       txtRepeatability.Text += "Min: " & " " & mStatisticX.Min.ToString("000.00") & " " & mStatisticY.Min.ToString("000.00") & " " & mStatisticT.Min.ToString("0.00") & vbCrLf
                                       txtRepeatability.Text += "Std: " & " " & stdX.ToString("000.00") & " " & stdY.ToString("000.00") & " " & stdT.ToString("0.00") & vbCrLf
                                   End If
                               Next
                           End Sub)
            End If

        Else
            InvokeUcDisplay(UcDisplay1, gAOICollection, Sys, SceneName, 0, 0, enmDisplayShape.None) '更新控制項
           '20170929 Toby_ Add 判斷
            If (Not IsNothing(Me)) Then
                Me.BeginInvoke(Sub()
                                   txtRepeatability.Text = "Count:" & ResultCount & " " & "Score:" & (CInt(myToolBlock.Outputs("Results_Item_0_Score").Value * 100)).ToString & vbCrLf
                                   '[Note]按鍵保護
                                   Control_Btn(True)
                               End Sub)
            End If

        End If

        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Me)) Then
            Me.BeginInvoke(Sub()
                               '[Note]按鍵保護
                               Control_Btn(True)
                           End Sub)
        End If
        'End Sub)
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnRun]" & vbTab & "ClickEnd")
        Control_Btn(True)
        'btnRun.Enabled = True
    End Sub
    ''' <summary>
    ''' 重複拍照看結果
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub btnRepeatRun_Click(sender As Object, e As EventArgs) Handles btnRepeatRun.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        '[Note]按鍵保護
        'btnRun.Enabled = False
        Control_Btn(False)
        UcDisplay1.CogDisplay1.StaticGraphics.Clear()
        CogDisplay1_GraphicsClear()


        mStatisticX.Clear()
        mStatisticY.Clear()
        mStatisticT.Clear()
        txtRepeatability.Text = ""

        If SceneName Is Nothing Then '未選場景
            lstScene.BackColor = Color.Yellow
            lstScene.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.CurrentThread.Join(100)
            Control_Btn(True)

            Exit Sub
        End If
        If myToolBlock Is Nothing Then '計算工具不存在
            '工具不存在
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000023))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Control_Btn(True)

            Exit Sub
        End If
        If myPMTool IsNot Nothing Then
            '沒訓練特徵
            If Not myPMTool.Pattern.Trained Then
                gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000009))
                MsgBox(gMsgHandler.GetMessage(Alarm_2000009), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                '[Note]按鍵保護
                Control_Btn(True)
                Exit Sub
            End If
        End If

        If gAOICollection.IsCCDExist(Sys.CCDNo) = False Then '取像工具不存在
            '取像工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000006))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000006), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Control_Btn(True)

            Exit Sub
        End If

        Await Task.Run(Sub()
                           For i As Integer = 0 To 99
                               'btnRun_Click(sender, e)
                               If Flag_Pause = True Then '按下暫停
                                   Flag_Pause = False
                                   Exit Sub
                               End If
                               'gAOICollection.SetExposure(Sys.CCDNo, nmcExposure.Value) '設定曝光值
                               gAOICollection.SetCCDScene(Sys.CCDNo, SceneName) '選擇場景
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
                                                              Control_Btn(True)

                                                          End Sub)
                                       End If
                                       Exit For
                                   End If
                                   '[Note] EMO時跳出
                                   If gDICollection.GetState(enmDI.EMO) = True Then

                                       '20170929 Toby_ Add 判斷
                                       If (Not IsNothing(Me)) Then
                                           Me.BeginInvoke(Sub()
                                                              Control_Btn(True)

                                                          End Sub)
                                       End If
                                       Exit Sub


                                   Else
                                       If Sys.MachineNo = enmMachineStation.MachineA Then
                                           If gDICollection.GetState(enmDI.EMS) = True Then

                                               '20170929 Toby_ Add 判斷
                                               If (Not IsNothing(Me)) Then
                                                   Me.BeginInvoke(Sub()
                                                                      Control_Btn(True)

                                                                  End Sub)
                                               End If
                                               Exit Sub
                                           End If
                                       ElseIf Sys.MachineNo = enmMachineStation.MachineB Then
                                           If gDICollection.GetState(enmDI.EMS2) = True Then

                                               '20170929 Toby_ Add 判斷
                                               If (Not IsNothing(Me)) Then
                                                   Me.BeginInvoke(Sub()
                                                                      Control_Btn(True)

                                                                  End Sub)
                                               End If
                                               Exit Sub
                                           End If
                                       End If
                                   End If
                               Loop Until gAOICollection.IsCCDCBusy(Sys.CCDNo) = False '等待CCD取像完成
                               Debug.Print("IsCCDCBusy:" & stopWatch.ElapsedMilliseconds)


                               If myToolBlock.Inputs.Count = 0 Then '計算工具輸入不存在
                                   '工具輸入不存在
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000024))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2000024), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          Control_Btn(True)

                                                      End Sub)
                                   End If
                                   Exit For
                               End If

                               If Not myToolBlock.Inputs.Contains("InputImage") Then '輸入影像接口不存在
                                   '工具輸入影像不存在
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000025))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2000025), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          Control_Btn(True)
                                                      End Sub)
                                   End If

                                   Exit For
                               End If

                               myToolBlock.Inputs("InputImage").Value = gAOICollection.CalibBoardCalibration(Sys.CCDNo, gAOICollection.GetAcqOutputImage(Sys.CCDNo), False, 0) '20170317Wenda gAOICollection.GetAcqOutputImage(Sys.CCDNo) '取像工具的結果套入
                               Debug.Print("myToolBlock.Inputs('InputImage').Value = acqObj.OutputImage:" & stopWatch.ElapsedMilliseconds)

                               'TabControl1.SelectTab(tabCCD)
                               If myToolBlock.Tools.Count = 0 Then '如果計算工具內沒有工具
                                   '工具不存在
                                   gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000023))
                                   MsgBox(gMsgHandler.GetMessage(Alarm_2000023), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          Control_Btn(True)
                                                      End Sub)
                                   End If

                                   Exit For
                               End If

                               myToolBlock.Run() '計算工具 試算
                               Debug.Print("myToolBlock.Run()" & stopWatch.ElapsedMilliseconds)


                               Dim ResultCount = myToolBlock.Outputs("Results_Count").Value
                               'Dim AcceptScore As Decimal
                               'If myToolBlock.Inputs.Contains("AcceptScore") Then
                               '    AcceptScore = myToolBlock.Inputs("AcceptScore").Value
                               'Else
                               '    AcceptScore = 0.3
                               'End If

                               'If myToolBlock.Outputs("Results_Item_0_Score").Value > AcceptScore Then
                               '    '[Note]找出特徵分數高於AcceptScore

                               'Else
                               '    '[Note]找出特徵分數低於可接受分數(AcceptScore)
                               '    ResultCount = 0

                               'End If

                               If ResultCount > 0 Then
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          Dim posX As Decimal = 0
                                                          Dim posY As Decimal = 0
                                                          Dim posT As Decimal = 0
                                                          Dim Score As Decimal = 0
                                                          For mResult As Integer = 0 To ResultCount - 1
                                                              posX = myToolBlock.Outputs("Results_Item_0_TranslationX").Value
                                                              posY = myToolBlock.Outputs("Results_Item_0_TranslationY").Value
                                                              posT = myToolBlock.Outputs("Results_Item_0_Rotation").Value
                                                              Score = myToolBlock.Outputs("Results_Item_0_Score").Value
                                                              DrawCenter(posX, posY, 10)
                                                              DrawAlignTypeRegion(myToolBlock, gAOICollection.SceneDictionary(SceneName).AlignType)
                                                              InvokeUcDisplay(UcDisplay1, gAOICollection, Sys, SceneName, 0, 0, enmDisplayShape.Alignment) '更新控制項
                                                              txtRepeatability.Text = "Count:" & ResultCount & " " & "Score:" & (Score * 100).ToString("00") & vbCrLf
                                                              txtRepeatability.Text += "Item" & " " & "X(pixel)" & " " & "Y(pixel)" & " " & "Theta(Deg)" & vbCrLf
                                                              txtRepeatability.Text += "Now:" & " " & posX.ToString("000.00") & " " & posY.ToString("000.00") & " " & posT.ToString("0.00") & vbCrLf
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

                                                                  txtRepeatability.Text += "Avg: " & " " & avgX.ToString("000.00") & " " & avgY.ToString("000.00") & " " & avgT.ToString("0.00") & vbCrLf
                                                                  txtRepeatability.Text += "Max: " & " " & mStatisticX.Max.ToString("000.00") & " " & mStatisticY.Max.ToString("000.00") & " " & mStatisticT.Max.ToString("0.00") & vbCrLf
                                                                  txtRepeatability.Text += "Min: " & " " & mStatisticX.Min.ToString("000.00") & " " & mStatisticY.Min.ToString("000.00") & " " & mStatisticT.Min.ToString("0.00") & vbCrLf
                                                                  txtRepeatability.Text += "Std: " & " " & stdX.ToString("000.00") & " " & stdY.ToString("000.00") & " " & stdT.ToString("0.00") & vbCrLf
                                                              End If


                                                          Next
                                                      End Sub)
                                   End If

                               Else
                                   '20170929 Toby_ Add 判斷
                                   If (Not IsNothing(Me)) Then
                                       Me.BeginInvoke(Sub()
                                                          txtRepeatability.Text = "Count:" & ResultCount & " " & "Score:" & (CInt(myToolBlock.Outputs("Results_Item_0_Score").Value * 100)).ToString & vbCrLf
                                                          Control_Btn(True)
                                                      End Sub)
                                   End If

                                   Exit For
                               End If

                           Next
                           '20170929 Toby_ Add 判斷
                           If (Not IsNothing(Me)) Then
                               Me.BeginInvoke(Sub()
                                                  Control_Btn(True)

                                              End Sub)
                           End If

                       End Sub)
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnRepeatRun]" & vbTab & "ClickEnd")
        Control_Btn(True)
    End Sub
    ''' <summary>
    ''' 刪除資料訊息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnReset.Enabled = False
        UcDisplay1.CogDisplay1.StaticGraphics.Clear()
        mStatisticX.Clear()
        mStatisticY.Clear()
        mStatisticT.Clear()
        txtRepeatability.Text = ""
        Flag_Pause = True
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnReset]" & vbTab & "ClickEnd")
        btnReset.Enabled = True
    End Sub



    'Private Sub SaveToolBlock()
    '    For i As Integer = 0 To myToolBlock.Tools.Count - 1
    '        If myToolBlock.Tools(i).GetType = GetType(CogPixelMapTool) Then
    '            myToolBlock.Tools(i) = myPixelMapTool
    '        End If
    '        If myToolBlock.Tools(i).GetType = GetType(CogPMAlignTool) Then
    '            myToolBlock.Tools(i) = myPMTool
    '        End If
    '        If myToolBlock.Tools(i).GetType = GetType(CogFixtureTool) Then
    '            myToolBlock.Tools(i) = myFixtureTool
    '        End If
    '        If myToolBlock.Tools(i).GetType = GetType(CogFindCircleTool) Then
    '            myToolBlock.Tools(i) = myFindCircleTool
    '        End If
    '    Next
    'End Sub


    ''' <summary>
    ''' 存檔
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        'btnOK.Enabled = False
        Control_Btn(False)
        Try
            Dim fileName As String
            Dim iniFileName As String
            'Dim RecipeDirectoryName As String = Application.StartupPath & "\Scene\" & MachineName
            'Dim SceneName As String = lstScene.SelectedItem
            If SceneName Is Nothing Or Not gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000015))
                MsgBox(gMsgHandler.GetMessage(Warn_3000015), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                'btnOK.Enabled = True
                Control_Btn(True)
                Exit Sub
            End If


            Dim ScenefilePath As String = Application.StartupPath & "\Scene\" & MachineName
            Dim CalibfilePath As String = Application.StartupPath & "\System\" & MachineName
            Dim mAlignType As eAlignType
            If IsRecipeScene Then
                '[Note]場景
                fileName = ScenefilePath & "\" & SceneName & ".ini"
                mAlignType = gAOICollection.SceneDictionary(SceneName).AlignType
            Else
                '[Note]校正
                fileName = CalibfilePath & "\" & SceneName & ".ini"
                mAlignType = gAOICollection.SceneDictionary(SceneName).AlignType
            End If


            Dim mScene As New CSceneParameter
            With mScene
                .AlignType = mAlignType
                .LightValue(0) = UcLightControl1.GetLight1Value 'nmcLight1.Value
                .LightValue(1) = UcLightControl1.GetLight2Value 'nmcLight2.Value
                .LightValue(2) = UcLightControl1.GetLight3Value ''nmcLight3.Value
                .LightValue(3) = UcLightControl1.GetLight4Value 'nmcLight4.Value

                .LightEnable(0) = UcLightControl1.GetLight1_OnOff 'btnSetLight1.Enabled
                .LightEnable(1) = UcLightControl1.GetLight2_OnOff 'btnSetLight2.Enabled
                .LightEnable(2) = UcLightControl1.GetLight3_OnOff 'btnSetLight3.Enabled
                .LightEnable(3) = UcLightControl1.GetLight4_OnOff 'btnSetLight4.Enabled
                '.CCDExposureTime = nmcExposure.Value
            End With

            If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
                gAOICollection.SceneDictionary(SceneName) = mScene
            Else
                gAOICollection.SceneDictionary.Add(SceneName, mScene)
            End If
            gAOICollection.SaveSceneParameter(SceneName, fileName) '儲存光源值等設定

            If IsRecipeScene Then
                '[Note]場景
                fileName = ScenefilePath & "\" & SceneName & ".vpp"
            Else
                '[Note]校正
                fileName = CalibfilePath & "\" & SceneName & ".vpp"
            End If


            'Cognex.VisionPro.CogSerializer.SaveObjectToFile(CogToolBlockEditV21.Subject, fileName) '控制項存到選定的Scene.
            '[Note]存檔待確認

            'SaveToolBlock()
            myToolBlock.Run()
            Cognex.VisionPro.CogSerializer.SaveObjectToFile(myToolBlock, fileName, GetType(System.Runtime.Serialization.Formatters.Binary.BinaryFormatter), CogSerializationOptionsConstants.All)

            If IsRecipeScene Then
                '[Note]場景
                iniFileName = ScenefilePath & "\" & SceneName & ".ini"
            Else
                '[Note]校正
                iniFileName = CalibfilePath & "\" & SceneName & ".ini"
            End If

            gAOICollection.SaveSceneOutputParam(Sys.CCDNo, SceneName, iniFileName)

            For mCCDNo As Integer = enmCCD.CCD1 To enmCCD.Max
                If gAOICollection.LoadVision(mCCDNo, fileName) = True Then '使用標準方式讀出 含掛載事件
                    gSyslog.Save("CCDNo:" & mCCDNo & " Save Image File(.vpp) as :" & fileName, , eMessageLevel.Information)
                    gSyslog.Save("CCDNo:" & mCCDNo & " Save Image File(.ini) as :" & iniFileName, , eMessageLevel.Information)
                Else
                    MsgBox("CCDNo: " & mCCDNo & " " & SceneName & " Save Failed.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                    'btnOK.Enabled = True
                    Control_Btn(True)
                    Exit Sub
                End If
            Next
            RecipeSceneName = SceneName 'lstScene.SelectedItem

            '儲存成功
            gSyslog.Save(SceneName & gMsgHandler.GetMessage(Warn_3000036))
            MsgBox(SceneName & " " & gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnOK]" & vbTab & "ClickEnd")
            'btnOK.Enabled = True
            Control_Btn(True)
            'Me.Close()
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message & " " & ex.StackTrace, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnOK ex]" & vbTab & "ClickEnd")
            'btnOK.Enabled = True
            Control_Btn(True)
        End Try
    End Sub
    ''' <summary>
    ''' 不存檔
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim btn As Button = sender
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")
        Me.Close()
    End Sub

#End Region


#Region "灰階重分佈"

    Private Sub Control_nmcThreshold(ByVal State As Boolean)
        nmcInputUpThreshold.Enabled = State
        nmcInputDownThreshold.Enabled = State
        nmcOutputUpThreshold.Enabled = State
        nmcOutputDownThreshold.Enabled = State
    End Sub

    Private Function GetSafeThreshold(ByVal Value As Integer) As Integer
        Dim Max As Integer = 255 '[Note]灰階
        If Value > Max Then
            Return Max
        End If
        Return Value
    End Function

    Private Sub nmcInputUpThreshold_ValueChanged(sender As Object, e As EventArgs) Handles nmcInputUpThreshold.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If

        If nmcInputUpThreshold.Enabled = False Then
            Exit Sub
        End If
        '[Note]控制項保護
        Control_nmcThreshold(False)

        '[Note]上限小於下限時，下限退
        If nmcInputUpThreshold.Value < nmcInputDownThreshold.Value Then
            nmcInputDownThreshold.Value = nmcInputUpThreshold.Value
        End If

        'Dim tmpInputUpThreshold, tmpInputDownThreshold As Decimal
        'Dim tmpOutputUpThreshold, tmpOutputDownThreshold As Decimal
        'tmpInputUpThreshold = nmcInputUpThreshold.Value
        'tmpInputDownThreshold = nmcInputDownThreshold.Value
        'tmpOutputUpThreshold = nmcOutputUpThreshold.Value
        'tmpOutputDownThreshold = nmcOutputDownThreshold.Value
        ''[Note]下限必須低於上限
        'If tmpInputUpThreshold <= tmpInputDownThreshold Then
        '    tmpInputDownThreshold = tmpInputUpThreshold
        'End If
        'If tmpOutputUpThreshold <= tmpOutputDownThreshold Then
        '    tmpOutputDownThreshold = tmpOutputUpThreshold
        'End If
        '[Note]下限必須低於上限
        'If tmpInputDownThreshold >= tmpInputUpThreshold Then
        '    tmpInputDownThreshold = tmpInputUpThreshold
        'End If
        'If tmpOutputDownThreshold >= tmpOutputUpThreshold Then
        '    tmpOutputDownThreshold = tmpOutputUpThreshold
        'End If

        If Not myPixelMapTool Is Nothing Then
            CogDisplay1_GraphicsClear()
            If myPixelMapTool.InputImage IsNot Nothing Then
                myPixelMapTool.RunParams.SetReferencePointInputAbsolute(0, CInt(nmcInputDownThreshold.Value))
                myPixelMapTool.RunParams.SetReferencePointInputAbsolute(1, CInt(nmcInputUpThreshold.Value))
                'myPixelMapTool.RunParams.SetReferencePointOutputAbsolute(0, tmpOutputDownThreshold)
                'myPixelMapTool.RunParams.SetReferencePointOutputAbsolute(1, tmpOutputUpThreshold)
                myPixelMapTool.Run()
                CogDisplay1.Image = myPixelMapTool.OutputImage
                CogDisplay1.Fit(True)
            Else
                gSyslog.Save(GetString("Please Check Image Source"))
                MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        End If
        '[Note]控制項保護
        Control_nmcThreshold(True)

    End Sub

    Private Sub nmcInputDownThreshold_ValueChanged(sender As Object, e As EventArgs) Handles nmcInputDownThreshold.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If

        If nmcInputDownThreshold.Enabled = False Then
            Exit Sub
        End If
        nmcInputDownThreshold.Enabled = False

        '[Note]下限只能退，不能更改上限
        If nmcInputDownThreshold.Value >= nmcInputUpThreshold.Value Then
            nmcInputDownThreshold.Value = nmcInputUpThreshold.Value
            Exit Sub
        End If
        'If nmcInputDownThreshold.Value > nmcInputDownThreshold.Maximum Then
        '    nmcInputDownThreshold.Value = nmcInputDownThreshold.Maximum
        'End If

        If Not myPixelMapTool Is Nothing Then
            CogDisplay1_GraphicsClear()
            If myPixelMapTool.InputImage IsNot Nothing Then
                myPixelMapTool.RunParams.SetReferencePointInputAbsolute(0, CInt(nmcInputDownThreshold.Value))
                myPixelMapTool.RunParams.SetReferencePointInputAbsolute(1, CInt(nmcInputUpThreshold.Value))
                myPixelMapTool.Run()
                CogDisplay1.Image = myPixelMapTool.OutputImage
                CogDisplay1.Fit(True)
            Else
                gSyslog.Save(GetString("Please Check Image Source"))
                MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        End If

        nmcInputDownThreshold.Enabled = True
    End Sub


    Private Sub nmcOutputUpThreshold_ValueChanged(sender As Object, e As EventArgs) Handles nmcOutputUpThreshold.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If

        If nmcOutputUpThreshold.Enabled = False Then
            Exit Sub
        End If
        'nmcOutputUpThreshold.Enabled = False
        '[Note]控制項保護
        Control_nmcThreshold(False)
        '[Note]上限小於下限時，下限退
        If nmcOutputUpThreshold.Value < nmcOutputDownThreshold.Value Then
            nmcOutputDownThreshold.Value = nmcOutputUpThreshold.Value
        End If
        '[Note]上限必須高於下限
        'If nmcOutputUpThreshold.Value <= nmcOutputDownThreshold.Value Then
        '    nmcOutputUpThreshold.Value = nmcOutputDownThreshold.Value
        'End If
        If Not myPixelMapTool Is Nothing Then
            CogDisplay1_GraphicsClear()
            If myPixelMapTool.InputImage IsNot Nothing Then
                myPixelMapTool.RunParams.SetReferencePointOutputAbsolute(0, CInt(nmcOutputDownThreshold.Value))
                myPixelMapTool.RunParams.SetReferencePointOutputAbsolute(1, CInt(nmcOutputUpThreshold.Value))
                myPixelMapTool.Run()
                CogDisplay1.Image = myPixelMapTool.OutputImage
                CogDisplay1.Fit(True)
            Else
                gSyslog.Save(GetString("Please Check Image Source"))
                MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        End If
        Control_nmcThreshold(True)
        'nmcOutputUpThreshold.Enabled = True
    End Sub

    Private Sub nmcOutputDownThreshold_ValueChanged(sender As Object, e As EventArgs) Handles nmcOutputDownThreshold.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If

        If nmcOutputDownThreshold.Enabled = False Then
            Exit Sub
        End If
        Control_nmcThreshold(False)

        '[Note]下限只能退，不能更改上限
        If nmcOutputDownThreshold.Value >= nmcOutputUpThreshold.Value Then
            nmcOutputDownThreshold.Value = nmcOutputUpThreshold.Value
            Exit Sub
        End If
        'nmcOutputDownThreshold.Enabled = False
        ''[Note]下限必須低於上限
        'If nmcOutputDownThreshold.Value >= nmcOutputUpThreshold.Value Then
        '    nmcOutputDownThreshold.Value = nmcOutputUpThreshold.Value
        'End If

        If Not myPixelMapTool Is Nothing Then
            CogDisplay1_GraphicsClear()
            If myPixelMapTool.InputImage IsNot Nothing Then
                myPixelMapTool.RunParams.SetReferencePointOutputAbsolute(0, CInt(nmcOutputDownThreshold.Value))
                myPixelMapTool.RunParams.SetReferencePointOutputAbsolute(1, CInt(nmcOutputUpThreshold.Value))
                myPixelMapTool.Run()
                CogDisplay1.Image = myPixelMapTool.OutputImage
                CogDisplay1.Fit(True)
            Else
                gSyslog.Save(GetString("Please Check Image Source"))
                MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        End If

        'nmcOutputDownThreshold.Enabled = True
        Control_nmcThreshold(True)
    End Sub

    Private Sub btnThresholdMore_Click(sender As Object, e As EventArgs) Handles btnThresholdMore.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnThresholdMore.Enabled = False
        CogDisplay1_GraphicsClear()
        If Not myPixelMapTool Is Nothing Then
            If myPixelMapTool.InputImage IsNot Nothing Then
                'myPixelMapTool.RunParams.SetReferencePointInput(0, 0)
                'myPixelMapTool.RunParams.SetReferencePointInputAbsolute(0, nmcDownThreshold.Value)
                'myPixelMapTool.RunParams.SetReferencePointInputAbsolute(1, nmcUpThreshold.Value)
                myPixelMapTool.Run()
                Dim mfrmAlignThreshold As frmAlignThreshold
                mfrmAlignThreshold = New frmAlignThreshold
                With mfrmAlignThreshold
                    .myPixelMapTool = myPixelMapTool
                    .StartPosition = FormStartPosition.CenterScreen
                    .Location = New Point(0, 0)
                    .mInputUpThreshold = nmcInputUpThreshold.Value
                    .mInputDownThreshold = nmcInputDownThreshold.Value
                    .mOutputUpThreshold = nmcOutputUpThreshold.Value
                    .mOutputDownThreshold = nmcOutputDownThreshold.Value
                    .ShowDialog()
                    nmcInputUpThreshold.Value = .mInputUpThreshold
                    nmcInputDownThreshold.Value = .mInputDownThreshold
                    nmcOutputUpThreshold.Value = .mOutputUpThreshold
                    nmcOutputDownThreshold.Value = .mOutputDownThreshold
                    myPixelMapTool = .myPixelMapTool
                End With
                myPixelMapTool.Run()
                CogDisplay1.Image = myPixelMapTool.OutputImage
                CogDisplay1.Fit(True)
            Else
                gSyslog.Save(GetString("Please Check Image Source"))
                MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If

        End If

        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnThresholdMore]" & vbTab & "ClickEnd")
        btnThresholdMore.Enabled = True
    End Sub

#End Region

#Region "定位工具"
    Private Sub btnGrabTrainImage_Click(sender As Object, e As EventArgs) Handles btnGrabTrainImage.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnGrabTrainImage.Enabled = False

        If myPixelMapTool IsNot Nothing Then
            myPixelMapTool.Run()
            myPMTool.InputImage = myPixelMapTool.OutputImage
        Else
            If myToolBlock.Inputs.Contains("InputImage") Then
                myPMTool.InputImage = myToolBlock.Inputs("InputImage").Value
            Else
                gSyslog.Save(GetString("Please Check Image Source"))
                MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If

        End If

        CogDisplay1_GraphicsClear()
        If Not myPMTool.InputImage Is Nothing Then
            myPMTool.Pattern.TrainImage = myPMTool.InputImage
            CogDisplay1.Image = myPMTool.Pattern.TrainImage
            CogDisplay1.Fit(True)
            If myPMTool.Pattern.Trained Then
                txtTrainMsg.Text = "Trained"
                txtTrainMsg.ForeColor = Color.Black
            Else
                txtTrainMsg.Text = "Untrained"
                txtTrainMsg.ForeColor = Color.Red
                grpCaliper.Enabled = False
            End If
        Else
            gSyslog.Save(GetString("Please Check Image Source"))
            MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If


        btnGrabTrainImage.Enabled = True
    End Sub

    Private Sub btnImageMask_Click(sender As Object, e As EventArgs) Handles btnImageMask.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnImageMask.Enabled = False

        Dim mfrmAlignImageMask As frmAlignImageMask
        mfrmAlignImageMask = New frmAlignImageMask
        With mfrmAlignImageMask
            If Not myPMTool.Pattern.TrainImage Is Nothing Then
                .mInputImage = myPMTool.Pattern.TrainImage
                .mImageMask = myPMTool.Pattern.TrainImageMask
            End If
            .StartPosition = FormStartPosition.CenterScreen
            .Location = New Point(0, 0)
            .ShowDialog()
            myPMTool.Pattern.TrainImageMask = .mImageMask
            myPMTool.Pattern.TrainImage = .mInputImage
        End With
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnImageMask]" & vbTab & "ClickEnd")
        btnImageMask.Enabled = True
    End Sub

    Dim mRect As New CogRectangle
    Dim dragRect As New CogRectangle


    Private Function ShowPMAlignTrainRegion(ByRef myPMTool As CogPMAlignTool)
        If myPMTool.InputImage IsNot Nothing Then 'myPMTool.Pattern.TrainImage
            ' If myPMTool.Pattern.Trained Then
            '[Note]有訓練圖像
            Dim myRectWidth, myRectHeight As Double
            Dim myRectCenterX, myRectCenterY As Double
            Dim Region As ICogRegion
            Dim typ = myPMTool.Pattern.TrainRegion.GetType()
            Region = myPMTool.Pattern.TrainRegion
            Select Case typ.FullName
                Case "Cognex.VisionPro.CogRectangle"
                    Dim tmp As CogRectangle = CType(Region, CogRectangle)
                    myRectWidth = tmp.Width '教導Pattern大小
                    myRectHeight = tmp.Height '教導Pattern大小
                    myRectCenterX = tmp.CenterX
                    myRectCenterY = tmp.CenterY
                Case "Cognex.VisionPro.CogRectangleAffine"
                    Dim tmp As CogRectangleAffine = CType(Region, CogRectangleAffine)
                    myRectWidth = tmp.SideXLength '教導Pattern大小
                    myRectHeight = tmp.SideYLength '教導Pattern大小
                    myRectCenterX = tmp.CenterX
                    myRectCenterY = tmp.CenterY
                Case "Cognex.VisionPro.CogCircle"
                    Dim tmp As CogCircle = CType(Region, CogCircle)
                    myRectWidth = tmp.Radius * 2 '教導Pattern大小
                    myRectHeight = tmp.Radius * 2 '教導Pattern大小
                    myRectCenterX = tmp.CenterX
                    myRectCenterY = tmp.CenterY
                Case "Cognex.VisionPro.CogEllipse"
                    Dim tmp As CogEllipse = CType(Region, CogEllipse)
                    myRectWidth = tmp.RadiusX * 2 '教導Pattern大小
                    myRectHeight = tmp.RadiusY * 2 '教導Pattern大小
                    myRectCenterX = tmp.CenterX
                    myRectCenterY = tmp.CenterY
                Case "Cognex.VisionPro.CogPolygon"
                    Dim tmp As CogPolygon = CType(Region, CogPolygon)
                    'tmp.
            End Select
            mRect.SetCenterWidthHeight(myRectCenterX, myRectCenterY, myRectWidth, myRectHeight)
            CogDisplay1.StaticGraphics.Add(mRect, "Rect")
            Return True
        Else
            Return False
        End If
    End Function


    Private Sub btnShowTrain_Click(sender As Object, e As EventArgs) Handles btnShowTrain.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnShowTrain.Enabled = False
        'Dim tmpRecord As Cognex.VisionPro.ICogRecord
        'myPMTool.Pattern.TrainImage = myPMTool.InputImage
        'tmpRecord = myPMTool.CreateCurrentRecord()
        'tmpRecord = tmpRecord.SubRecords.Item("TrainImage")
        'mRect.DragLineStyle = CogGraphicLineStyleConstants.Solid
        'mRect.DragLineWidthInScreenPixels = 1
        'mRect.SetCenterWidthHeight(100, 100, 100, 100)
        'mRect.Color = CogColorConstants.Red
        'mRect.X = 100
        'mRect.Y = 100
        'mRect.Width = 100
        'mRect.Height = 100
        'Dim mCircle As New CogCircle
        'Dim mRect As New CogRectangle
        'CogRecordDisplay1.DrawingEnabled = False
        'CogRecordDisplay1.AttentionGraphic
        'CogRecordDisplay1.InteractiveGraphics.Add(mRect, "Rect", False)
        'mCircle.CenterX = CogRecordDisplay1.Image.Width / 2
        'mCircle.CenterY = CogRecordDisplay1.Image.Height / 2
        'mCircle.Radius = CogRecordDisplay1.Image.Height / 4
        'mRect.SetCenterWidthHeight(mCircle.CenterX, mCircle.CenterY, CogRecordDisplay1.Image.Width / 2, CogRecordDisplay1.Image.Height / 2)
        'CogRecordDisplay1.DrawingEnabled = True
        'CogRecordDisplay1.InteractiveGraphics.Add(mRect, "#", False)
        CogDisplay1_GraphicsClear()

        If myPMTool.Pattern.TrainImage IsNot Nothing Then
            ' If myPMTool.Pattern.Trained Then
            '[Note]有訓練圖像
            Dim myRectWidth, myRectHeight As Double
            Dim myRectCenterX, myRectCenterY As Double
            Dim Region As ICogRegion
            If myPMTool.Pattern.Trained Then '[Note]有教導
                Dim typ = myPMTool.Pattern.TrainRegion.GetType()
                Region = myPMTool.Pattern.TrainRegion
                Select Case typ.FullName '[Note]防止舊的教導場景使用不同的教導框
                    Case "Cognex.VisionPro.CogRectangle"
                        Dim tmp As CogRectangle = CType(Region, CogRectangle)
                        myRectWidth = tmp.Width '教導Pattern大小
                        myRectHeight = tmp.Height '教導Pattern大小
                        myRectCenterX = tmp.CenterX
                        myRectCenterY = tmp.CenterY
                    Case "Cognex.VisionPro.CogRectangleAffine"
                        Dim tmp As CogRectangleAffine = CType(Region, CogRectangleAffine)
                        myRectWidth = tmp.SideXLength '教導Pattern大小
                        myRectHeight = tmp.SideYLength '教導Pattern大小
                        myRectCenterX = tmp.CenterX
                        myRectCenterY = tmp.CenterY
                    Case "Cognex.VisionPro.CogCircle"
                        Dim tmp As CogCircle = CType(Region, CogCircle)
                        myRectWidth = tmp.Radius * 2 '教導Pattern大小
                        myRectHeight = tmp.Radius * 2 '教導Pattern大小
                        myRectCenterX = tmp.CenterX
                        myRectCenterY = tmp.CenterY
                    Case "Cognex.VisionPro.CogEllipse"
                        Dim tmp As CogEllipse = CType(Region, CogEllipse)
                        myRectWidth = tmp.RadiusX * 2 '教導Pattern大小
                        myRectHeight = tmp.RadiusY * 2 '教導Pattern大小
                        myRectCenterX = tmp.CenterX
                        myRectCenterY = tmp.CenterY
                    Case "Cognex.VisionPro.CogPolygon"
                        Dim tmp As CogPolygon = CType(Region, CogPolygon)
                End Select
            Else
                '[Note]預設使用畫面中心 1/4的範圍做教導
                myRectCenterX = myPMTool.InputImage.Width / 2
                myRectCenterY = myPMTool.InputImage.Height / 2
                myRectWidth = myPMTool.InputImage.Width / 2
                myRectHeight = myPMTool.InputImage.Height / 2
            End If


            mRect.Interactive = True
            mRect.GraphicDOFEnable = CogRectangleDOFConstants.All
            mRect.Color = CogColorConstants.Blue

            mRect.SetCenterWidthHeight(myRectCenterX, myRectCenterY, myRectWidth, myRectHeight)
            CogDisplay1.InteractiveGraphics.Add(mRect, "Rect", True)
            '[Note]顯示特徵
            If myPMTool.Pattern.Trained Then
                Try
                    Dim grainColl As CogGraphicCollection
                    grainColl = myPMTool.Pattern.CreateGraphicsFine(CogColorConstants.Yellow)
                    For i = 0 To grainColl.Count - 1
                        CogDisplay1.StaticGraphics.Add(grainColl.Item(i), "")
                    Next
                Catch ex As Exception
                    Debug.Print("CreateGraphicsFine GG")
                End Try
            Else
                myPMTool.Pattern.TrainRegion = mRect
            End If
            'End If
            CogDisplay1.Image = myPMTool.Pattern.TrainImage
            CogDisplay1.Fit(True)

            'AddHandler CogDisplay1.KeyDown, AddressOf cogDisplay1_KeyDown
            'AddHandler CogDisplay1.KeyUp, AddressOf cogDisplay1_KeyUp
            AddHandler mRect.Dragging, AddressOf mRect_Dragging
            AddHandler mRect.DraggingStopped, AddressOf mRect_DraggingStopped
        Else
            gSyslog.Save(GetString("Please Check Image Source"))
            MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnShowTrain]" & vbTab & "ClickEnd")
        btnShowTrain.Enabled = True
    End Sub


    Dim mTrainRegion As Cognex.VisionPro.ICogRegion
    Private Sub btnTrain_Click(sender As Object, e As EventArgs) Handles btnTrain.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnTrain.Enabled = False

        If myPMTool Is Nothing Then
            '定位工具不存在! 請先設定!
            gSyslog.Save(gMsgHandler.GetMessage(Alarm_2000007))
            MsgBox(gMsgHandler.GetMessage(Alarm_2000007), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            btnTrain.Enabled = True
            Exit Sub
        End If
        Try
            '[Note]timeout
            myPMTool.RunParams.Timeout = 30 '10
            myPMTool.RunParams.TimeoutEnabled = True
            myPMTool.Pattern.Origin.TranslationX = myPMTool.InputImage.Width / 2
            myPMTool.Pattern.Origin.TranslationY = myPMTool.InputImage.Height / 2
            '[Note]相似度
            'myToolBlock.Inputs("AcceptScore").Value = (nmcMatchRate.Value) * 0.01
            myPMTool.RunParams.AcceptThreshold = (nmcMatchRate.Value) * 0.01
            'myPMTool.RunParams.AcceptThreshold = 0.3
            '[Note]角度
            If nmcRotation.Value = 0 Then
                myPMTool.RunParams.ZoneAngle.Configuration = CogPMAlignZoneConstants.Nominal
                myPMTool.RunParams.ZoneAngle.Nominal = 0
            Else
                myPMTool.RunParams.ZoneAngle.Configuration = CogPMAlignZoneConstants.LowHigh
                myPMTool.RunParams.ZoneAngle.High = nmcRotation.Value * Math.PI / 180
                myPMTool.RunParams.ZoneAngle.Low = -(nmcRotation.Value * Math.PI / 180)
            End If
            '[Note]縮放
            If nmcScale.Value = 0 Then
                myPMTool.RunParams.ZoneScale.Configuration = CogPMAlignZoneConstants.Nominal
                myPMTool.RunParams.ZoneScale.Nominal = 1
            Else
                myPMTool.RunParams.ZoneScale.Configuration = CogPMAlignZoneConstants.LowHigh
                myPMTool.RunParams.ZoneScale.Low = 1 - nmcScale.Value
                myPMTool.RunParams.ZoneScale.High = 1 + nmcScale.Value
            End If

            If mRect.CenterX <> myPMTool.InputImage.Width / 2 Then '[Note]介面上沒有教導視窗
                btnShowTrain.BackColor = Color.Yellow
                btnShowTrain.Refresh()
                System.Threading.Thread.Sleep(1000)
                btnShowTrain.BackColor = System.Drawing.Color.Transparent
                btnTrain.Enabled = True
                Exit Sub
            End If

            myPMTool.Pattern.TrainRegion = mRect

            myPMTool.Pattern.Train()
            myPMTool.Run()
            CogDisplay1_GraphicsClear()
            ShowPMAlignTrainRegion(myPMTool)
            'Dim tmpRecord As Cognex.VisionPro.ICogRecord
            Dim grainColl As CogGraphicCollection
            If myPMTool.InputImage IsNot Nothing Then
                CogDisplay1.Image = myPMTool.Pattern.TrainImage
                grainColl = myPMTool.Pattern.CreateGraphicsFine(CogColorConstants.Yellow)
                For i = 0 To grainColl.Count - 1
                    CogDisplay1.StaticGraphics.Add(grainColl.Item(i), "")
                Next
                CogDisplay1.Fit(True)
            Else
                gSyslog.Save(GetString("Please Check Image Source"))
                MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If

            If myPMTool.Pattern.Trained Then
                txtTrainMsg.Text = "Trained"
                txtTrainMsg.ForeColor = Color.Black
            Else
                txtTrainMsg.Text = "Untrained"
                txtTrainMsg.ForeColor = Color.Red
                grpCaliper.Enabled = False
            End If

        Catch ex As Exception
            MsgBox("Message:" & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
        End Try

        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnTrain]" & vbTab & "ClickEnd")
        btnTrain.Enabled = True
    End Sub

    Private Function FindPMAlignResult(ByVal myPMTool As CogPMAlignTool) As Boolean
        Try
            If myPMTool IsNot Nothing Then
                If myPMTool.InputImage IsNot Nothing Then
                    'myPMTool.Run()
                    If myPMTool.Results.Count > 0 Then
                        DrawCenter(myPMTool.Results.Item(0).GetPose.TranslationX, myPMTool.Results.Item(0).GetPose.TranslationY, 10)
                        CogDisplay1.StaticGraphics.Add(myPMTool.Results(0).CreateResultGraphics(Cognex.VisionPro.PMAlign.CogPMAlignResultGraphicConstants.MatchRegion), "#")
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            MsgBox("Message:" & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function



    Private Sub btnPMAlignRun_Click(sender As Object, e As EventArgs) Handles btnPMAlignRun.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnPMAlignRun.Enabled = False

        If myPMTool IsNot Nothing Then
            If myPMTool.InputImage IsNot Nothing Then
                If myPMTool.Pattern.Trained Then
                    myPMTool.Run()
                    myToolBlock.Run()
                    CogDisplay1_GraphicsClear()
                    CogDisplay1.Image = myPMTool.InputImage
                    CogDisplay1.Fit(True)
                    If myPMTool.Results IsNot Nothing Then
                        If myPMTool.Results.Count > 0 Then
                            FindPMAlignResult(myPMTool)
                            myToolBlock.Run()
                            grpCaliper.Enabled = True
                            'grpRadiusTolerance.Enabled = True
                        Else
                            grpCaliper.Enabled = False
                            'grpRadiusTolerance.Enabled = False
                        End If
                    End If
                Else
                    btnTrain.BackColor = Color.Yellow
                    btnTrain.Refresh()
                    System.Threading.Thread.Sleep(1000)
                    btnTrain.BackColor = System.Drawing.Color.Transparent
                End If
            Else
                gSyslog.Save(GetString("Please Check Image Source"))
                MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
        End If

        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnPMAlignRun]" & vbTab & "ClickEnd")
        btnPMAlignRun.Enabled = True
    End Sub


    Private shiftIsDown As Boolean
    Private Sub cogDisplay1_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        shiftIsDown = True 'e.Shift
        'Debug.Print("KeyDown shiftIsDown:" & shiftIsDown)

    End Sub

    Private Sub cogDisplay1_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        shiftIsDown = False 'e.Shift
        'Debug.Print("KeyUp shiftIsDown:" & shiftIsDown)

    End Sub
    Private Sub mRect_Dragging(ByVal sender As System.Object, ByVal e As CogDraggingEventArgs)
        'Debug.Print("mRect_Dragging:" & shiftIsDown)
        dragRect = CType(e.DragGraphic, CogRectangle)
        If Not myPMTool.Pattern.TrainImage Is Nothing Then
            Debug.Print("myPMTool.Pattern.TrainImage")
            'If shiftIsDown Then
            If dragRect.Width > myPMTool.Pattern.TrainImage.Width Then
                dragRect.Width = myPMTool.Pattern.TrainImage.Width
            End If
            If dragRect.Height > myPMTool.Pattern.TrainImage.Height Then
                dragRect.Height = myPMTool.Pattern.TrainImage.Height
            End If
            dragRect.Width = Math.Ceiling(dragRect.Width)   '取整數
            dragRect.Height = Math.Ceiling(dragRect.Height) '取整數

            dragRect.X = (myPMTool.Pattern.TrainImage.Width / 2) - (dragRect.Width / 2)  '800
            dragRect.Y = (myPMTool.Pattern.TrainImage.Height / 2) - (dragRect.Height / 2) '600
            'End If

        End If

    End Sub

    Private Sub mRect_DraggingStopped(ByVal sender As System.Object, ByVal e As CogDraggingEventArgs)
        ' Debug.Print("mRect_DraggingStopped:" & shiftIsDown)
        mRect_Dragging(sender, e)

        '[Note]更新介面數值
        dragRect = CType(e.DragGraphic, CogRectangle)
        nmcPatternAreaW.Value = dragRect.Width
        nmcPatternAreaH.Value = dragRect.Height

        If myPMTool.Pattern.Trained Then
            txtTrainMsg.Text = "Trained"
            txtTrainMsg.ForeColor = Color.Black
        Else
            CogDisplay1.StaticGraphics.Clear()
            txtTrainMsg.Text = "Untrained"
            txtTrainMsg.ForeColor = Color.Red
            grpCaliper.Enabled = False
        End If

    End Sub


    Private Sub nmcPatternAreaW_ValueChanged(sender As Object, e As EventArgs) Handles nmcPatternAreaW.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If

        If Not myPMTool Is Nothing Then
            If Not myPMTool.Pattern.TrainImage Is Nothing Then
                If nmcPatternAreaW.Value > myPMTool.Pattern.TrainImage.Width Then
                    nmcPatternAreaW.Value = myPMTool.Pattern.TrainImage.Width
                End If
                mRect.Width = nmcPatternAreaW.Value
                mRect.X = (myPMTool.Pattern.TrainImage.Width / 2) - (mRect.Width / 2)
            End If
        End If
    End Sub

    Private Sub nmcPatternAreaH_ValueChanged(sender As Object, e As EventArgs) Handles nmcPatternAreaH.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If

        If Not myPMTool Is Nothing Then
            If Not myPMTool.Pattern.TrainImage Is Nothing Then
                If nmcPatternAreaH.Value > myPMTool.Pattern.TrainImage.Height Then
                    nmcPatternAreaH.Value = myPMTool.Pattern.TrainImage.Height
                End If
                mRect.Height = nmcPatternAreaH.Value
                mRect.Y = (myPMTool.Pattern.TrainImage.Height / 2) - (mRect.Height / 2)
            End If
        End If
    End Sub



    Private Sub btnAlignMore_Click(sender As Object, e As EventArgs) Handles btnAlignROI.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnAlignROI.Enabled = False
        If Not myPMTool Is Nothing Then
            If myPMTool.InputImage IsNot Nothing Then
                Dim mfrmAlignPMAlign As frmAlignPMAlign
                mfrmAlignPMAlign = New frmAlignPMAlign
                With mfrmAlignPMAlign
                    .myPMAlignTool = myPMTool
                    .myROIType = eROIType.PMAlign
                    .StartPosition = FormStartPosition.CenterScreen
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    myPMTool = .myPMAlignTool
                End With
            Else
                gSyslog.Save(GetString("Please Check Image Source"))
                MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
            myPMTool.Run()
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnAlignMore]" & vbTab & "ClickEnd")
        btnAlignROI.Enabled = True
    End Sub

#End Region


#Region "面積工具"

    Private Function FindBlobShowResult(ByVal myBlobTool As Cognex.VisionPro.Blob.CogBlobTool) As Boolean
        Try
            If myBlobTool IsNot Nothing Then
                If myBlobTool.InputImage IsNot Nothing Then
                    myBlobTool.Run()

                    CogDisplay1.InteractiveGraphics.Clear()
                    CogDisplay1.StaticGraphics.Clear()
                    ''myFindCircleTool.GetCircle.CopyBase(CogCopyShapeConstants.All)
                    If myBlobTool.Results.GetBlobs.Count > 0 Then

                        '[Note]重心
                        If myBlobTool.Results.GetBlobs.Count > 0 Then
                            DrawCenter(myBlobTool.Results.GetBlobs.Item(0).CenterOfMassX, myBlobTool.Results.GetBlobs.Item(0).CenterOfMassY, 10)
                        End If

                        For Each blob As CogBlobResult In myBlobTool.Results.GetBlobs(True)
                            CogDisplay1.StaticGraphics.Add(blob.CreateResultGraphics(CogBlobResultGraphicConstants.Boundary), "")
                        Next

                    Else
                        '[Note]沒訓練結果
                        Return False
                    End If

                    CogDisplay1.Fit(True)
                Else
                    '[Note]沒輸入影像
                    Return False
                End If
            Else
                '[Note]工具不存在
                Return False
            End If
            Return True

        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function


    Private Function SetBlobPolarity(ByRef myBlobTool As Cognex.VisionPro.Blob.CogBlobTool, ByVal Polarity As Integer)
        If myBlobTool IsNot Nothing Then

            myBlobTool.RunParams.SegmentationParams.Mode = CogBlobSegmentationModeConstants.SoftFixedThreshold

            Select Case Polarity
                Case 0
                    myBlobTool.RunParams.SegmentationParams.Polarity = CogBlobSegmentationPolarityConstants.DarkBlobs
                Case 1
                    myBlobTool.RunParams.SegmentationParams.Polarity = CogBlobSegmentationPolarityConstants.LightBlobs

            End Select
            FindBlobShowResult(myBlobTool)
        Else
            Return False
        End If

        Return True
    End Function





    Private Sub cmbBlobPolarity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBlobPolarity.SelectedIndexChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If

        If cmbBlobPolarity.SelectedIndex < 0 Then
            Exit Sub
        End If
        If IsNothing(SceneName) Then
            Exit Sub
        End If
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            'Select Case gAOICollection.SceneDictionary(SceneName).AlignType

            SetBlobPolarity(myBlobTool, cmbBlobPolarity.SelectedIndex)



            'End Select
        End If

    End Sub


    Private Function SetBlobLowThreshold(ByRef myBlobTool As Cognex.VisionPro.Blob.CogBlobTool, ByVal LowThreshold As Integer)
        If myBlobTool IsNot Nothing Then
            myBlobTool.RunParams.SegmentationParams.SoftFixedThresholdLow = LowThreshold
            If LowThreshold >= nmcBlobHighThreshold.Value Then
                nmcBlobHighThreshold.Value = LowThreshold + 1
            End If

            FindBlobShowResult(myBlobTool)
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub nmcBlobLowThreshold_ValueChanged(sender As Object, e As EventArgs) Handles nmcBlobLowThreshold.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            'Select Case gAOICollection.SceneDictionary(SceneName).AlignType
            SetBlobLowThreshold(myBlobTool, nmcBlobLowThreshold.Value)
            'End Select
        End If
    End Sub


    Private Function SetBlobHighThreshold(ByRef myBlobTool As Cognex.VisionPro.Blob.CogBlobTool, ByVal HighThreshold As Integer)
        If myBlobTool IsNot Nothing Then
            myBlobTool.RunParams.SegmentationParams.SoftFixedThresholdHigh = HighThreshold
            If HighThreshold <= nmcBlobLowThreshold.Value Then
                nmcBlobLowThreshold.Value = HighThreshold - 1
            End If

            FindBlobShowResult(myBlobTool)
            Return True
        Else
            Return False
        End If
    End Function


    Private Sub nmcBlobHighThreshold_ValueChanged(sender As Object, e As EventArgs) Handles nmcBlobHighThreshold.ValueChanged
        If IsNothing(SceneName) Then
            Exit Sub
        End If
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            'Select Case gAOICollection.SceneDictionary(SceneName).AlignType
            SetBlobHighThreshold(myBlobTool, nmcBlobHighThreshold.Value)
            'End Select
        End If
    End Sub

    Private Function SetBlobMinArea(ByRef myBlobTool As Cognex.VisionPro.Blob.CogBlobTool, ByVal MinArea As Integer)
        If myBlobTool IsNot Nothing Then
            myBlobTool.RunParams.ConnectivityMinPixels = MinArea
            FindBlobShowResult(myBlobTool)
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub nmcMinArea_ValueChanged(sender As Object, e As EventArgs) Handles nmcMinArea.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If
        If IsNothing(SceneName) Then
            Exit Sub
        End If
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            'Select Case gAOICollection.SceneDictionary(SceneName).AlignType
            SetBlobMinArea(myBlobTool, nmcMinArea.Value)
            'End Select
        End If
    End Sub


    Private Sub btnBlobRun_Click(sender As Object, e As EventArgs) Handles btnBlobRun.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnBlobRun.Enabled = False

        FindBlobShowResult(myBlobTool)
        myToolBlock.Run()
        If myBlobTool.Results.GetBlobs.Count > 0 Then
            grpCaliper.Enabled = True
            'grpRadiusTolerance.Enabled = True
        Else
            grpCaliper.Enabled = False
            'grpRadiusTolerance.Enabled = False
        End If


        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnBlobRun]" & vbTab & "ClickEnd")
        btnBlobRun.Enabled = True
    End Sub


    Private Sub btnBlobROI_Click(sender As Object, e As EventArgs) Handles btnBlobROI.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnBlobROI.Enabled = False
        If Not myBlobTool Is Nothing Then
            If myBlobTool.InputImage IsNot Nothing Then
                Dim mfrmBlobROI As frmAlignPMAlign
                mfrmBlobROI = New frmAlignPMAlign
                With mfrmBlobROI
                    .myBlobTool = myBlobTool
                    .myROIType = eROIType.Blob
                    .StartPosition = FormStartPosition.CenterScreen
                    .Location = New Point(0, 0)
                    .ShowDialog()
                    myBlobTool = .myBlobTool
                End With
            Else
                gSyslog.Save(GetString("Please Check Image Source"))
                MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If
            myBlobTool.Run()
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnBlobROI]" & vbTab & "ClickEnd")
        btnBlobROI.Enabled = True
    End Sub



#End Region

#Region "找圓工具"

    Dim mCircularArc As New CogCircularArc
    Dim dragCircular As New CogCircularArc
    Dim mCaliper As New CogCaliperEdge

    'CogGraphicCollection myRegions;
    Dim myCircularRegions As New CogGraphicCollection
    Dim dragCircularRegions As New CogGraphicCollection
    'Dim mGraphic As ICogGraphic
    Dim dragGraphic As New CogCaliper

    Dim mPointMarker As New CogPointMarker
    Dim dragPointMarker As New CogPointMarker
    Dim mRectangleAffine As New CogRectangleAffine
    Dim dragRectangleAffine As New CogRectangleAffine
    Private Sub mCircularArc_Dragging(ByVal sender As System.Object, ByVal e As CogDraggingEventArgs)
        'Debug.Print("mCircular_Dragging:" & shiftIsDown)
        dragCircular = CType(e.DragGraphic, CogCircularArc)
        If Not myFindCircleTool.InputImage Is Nothing Then
            'If shiftIsDown Then
            'If dragCircular.Radius > myFindCircleTool.InputImage Then
            '    dragRect.Width = myPMTool.Pattern.TrainImage.Width
            'End If
            'If dragRect.Height > myPMTool.Pattern.TrainImage.Height Then
            '    dragRect.Height = myPMTool.Pattern.TrainImage.Height
            'End If
            dragCircular.Radius = Math.Ceiling(dragCircular.Radius)   '取整數
            'Debug.Print("dragGraphic.LineWidthInScreenPixels:" & dragGraphic.)
            'Debug.Print("LineWidth:" & myCircularRegions.Item(0).LineWidthInScreenPixels)
            'End If
        End If

    End Sub

    Private Sub mCircularArc_DraggingStopped(ByVal sender As System.Object, ByVal e As CogDraggingEventArgs)
        'Debug.Print("mCircular_DraggingStopped:" & shiftIsDown)
        mCircularArc_Dragging(sender, e)
        '[Note]更新介面數值
        dragCircular = CType(e.DragGraphic, CogCircularArc)

    End Sub

    'Dim myFCircle As New CogFindCircle
    Private Function ShowCircularArc(ByRef myFindCircleTool As CogFindCircleTool)
        If myFindCircleTool IsNot Nothing Then
            If myFindCircleTool.InputImage IsNot Nothing Then
                'Dim myCircularCenterX, myCircularCenterY As Double
                'Dim myCircularRadius As Double
                'mCircularArc = myFindCircleTool.RunParams.ExpectedCircularArc
                'myCircularRadius = myFindCircleTool.RunParams.ExpectedCircularArc.Radius
                ''myCircularCenterX = myFindCircleTool.RunParams.ExpectedCircularArc.CenterX
                'myCircularCenterY = myFindCircleTool.RunParams.ExpectedCircularArc.CenterY

                'myFindCircleTool.RunParams.ExpectedCircularArc.SelectedSpaceName = "@/Fixture"
                'If myPMTool.Results.Count > 0 Then
                'myCircularCenterX = myFindCircleTool.RunParams.ExpectedCircularArc.CenterX
                'myCircularCenterY = myFindCircleTool.RunParams.ExpectedCircularArc.CenterY
                'Else
                'myCircularCenterX = myFindCircleTool.InputImage.Width / 2
                'myCircularCenterY = myFindCircleTool.InputImage.Height / 2
                'End If
                'mCircular.SelectedSpaceName = "@/Fixture"
                'mCircular.Interactive = True
                'mCircular.GraphicDOFEnable = CogRectangleDOFConstants.All
                'mCircular.Color = CogColorConstants.Blue

                'mCircular.SetCenterRadiusAngleStartAngleSpan(myCircularCenterX, myCircularCenterY, myCircularRadius, 0, 360)
                'mCircular = myFindCircleTool.RunParams.ExpectedCircularArc
                Dim myRec As ICogRecord
                myRec = myFindCircleTool.CreateCurrentRecord()
                mCircularArc = CType(myRec.SubRecords("InputImage").SubRecords("ExpectedShapeSegment").Content, CogCircularArc)
                myCircularRegions = CType(myRec.SubRecords("InputImage").SubRecords("CaliperRegions").Content, CogGraphicCollection)

                CogDisplay1.InteractiveGraphics.Add(mCircularArc, "CircularArc", True)
                AddHandler mCircularArc.Dragging, AddressOf mCircularArc_Dragging
                AddHandler mCircularArc.DraggingStopped, AddressOf mCircularArc_DraggingStopped
                'Dim mGraphic As ICogGraphic
                'For Each mGraphic In myCircularRegions
                '    Dim tmpGraphic = CType(mGraphic, ICogGraphicInteractive)
                '    CogDisplay1.InteractiveGraphics.Add(mGraphic, "Graphic", True)
                '    If mGraphic.GetType() = GetType(CogRectangleAffine) Then
                '        mRectangleAffine = CType(mGraphic, CogRectangleAffine)
                '        Debug.Print("mRectangleAffine SideXLength:" & mRectangleAffine.SideXLength)
                '        Debug.Print("mRectangleAffine SideYLength:" & mRectangleAffine.SideYLength)
                '    End If
                'Next

                If myCircularRegions IsNot Nothing Then
                    Dim mGraphic As ICogGraphic
                    For i As Integer = 0 To myCircularRegions.Count - 1
                        mGraphic = myCircularRegions.Item(i)
                        If i = 0 Then
                            If mGraphic.GetType = GetType(CogRectangleAffine) Then
                                mRectangleAffine = CType(mGraphic, CogRectangleAffine)
                                CogDisplay1.InteractiveGraphics.Add(mRectangleAffine, "RectangleAffine", True)
                            End If
                        ElseIf mGraphic.GetType = GetType(CogPointMarker) Then
                            mPointMarker = CType(mGraphic, CogPointMarker)
                            CogDisplay1.InteractiveGraphics.Add(mPointMarker, "PointMarker", True)
                        Else
                            CogDisplay1.InteractiveGraphics.Add(mGraphic, "Graphic", True)
                        End If

                        'AddHandler CogDisplay1.InteractiveGraphics(CogDisplay1.InteractiveGraphics.Count - 1).Dragging, AddressOf mRectangleAffine_Dragging
                        'AddHandler CogDisplay1.InteractiveGraphics(CogDisplay1.InteractiveGraphics.Count - 1).DraggingStopped, AddressOf mRectangleAffine_DraggingStopped
                    Next
                    AddHandler mPointMarker.Dragging, AddressOf mRectangleAffine_Dragging
                    AddHandler mPointMarker.DraggingStopped, AddressOf mRectangleAffine_DraggingStopped

                End If

                CogDisplay1.Image = myFindCircleTool.InputImage
                CogDisplay1.Fit(True)

            End If

        Else
            Return False
        End If

        Return True
    End Function

    Private Function SetFindCircleSearchLength(ByRef myFindCircleTool As CogFindCircleTool, ByVal CaliperSearchLength As Integer)
        If myFindCircleTool IsNot Nothing Then
            myFindCircleTool.RunParams.CaliperSearchLength = CaliperSearchLength
            ShowCircularArc(myFindCircleTool)
        Else
            Return False
        End If

        Return True
    End Function

    Private Function SetFindCircleCalipersNumber(ByRef myFindCircleTool As CogFindCircleTool, ByVal NumCalipers As Integer)
        Try
            If myFindCircleTool IsNot Nothing Then
                CogDisplay1.StaticGraphics.Clear()
                CogDisplay1.InteractiveGraphics.Clear()
                myFindCircleTool.RunParams.NumCalipers = NumCalipers

                myFindCircleTool.Run()
                ShowCircularArc(myFindCircleTool)

            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try

        Return True
    End Function


    Private Function SetFindCircleProjectionLength(ByRef myFindCircleTool As CogFindCircleTool, ByVal ProjectionLength As Integer)
        If myFindCircleTool IsNot Nothing Then
            myFindCircleTool.RunParams.CaliperProjectionLength = ProjectionLength
            ShowCircularArc(myFindCircleTool)
        Else
            Return False
        End If

        Return True
    End Function


    Private Function SetFindCircleCalipersIgnoreNumber(ByRef myFindCircleTool As CogFindCircleTool, ByVal IgnoreNumber As Integer)
        Try
            If myFindCircleTool IsNot Nothing Then
                If IgnoreNumber = 0 Then
                    myFindCircleTool.RunParams.DecrementNumToIgnore = False
                Else
                    myFindCircleTool.RunParams.DecrementNumToIgnore = True
                End If
                myFindCircleTool.RunParams.NumToIgnore = IgnoreNumber
                ShowCircularArc(myFindCircleTool)
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try

        Return True
    End Function

    Private Function SetFindCaplipersPolarity(ByRef myFindCircleTool As CogFindCircleTool, ByVal Polarity As Integer)
        If myFindCircleTool IsNot Nothing Then
            myFindCircleTool.RunParams.CaliperRunParams.EdgeMode = CogCaliperEdgeModeConstants.SingleEdge
            Select Case Polarity
                Case 0
                    myFindCircleTool.RunParams.CaliperRunParams.Edge0Polarity = CogCaliperPolarityConstants.DarkToLight
                Case 1
                    myFindCircleTool.RunParams.CaliperRunParams.Edge0Polarity = CogCaliperPolarityConstants.LightToDark
                Case 2
                    myFindCircleTool.RunParams.CaliperRunParams.Edge0Polarity = CogCaliperPolarityConstants.DontCare
            End Select
            FindCircleShowResult(myFindCircleTool)
        Else
            Return False
        End If

        Return True
    End Function

    Private Function SetFindCaplipersSearchDirection(ByRef myFindCircleTool As CogFindCircleTool, ByVal SearchDirection As Integer)
        If myFindCircleTool IsNot Nothing Then
            Select Case SearchDirection
                Case 0
                    myFindCircleTool.RunParams.CaliperSearchDirection = CogFindCircleSearchDirectionConstants.Inward
                Case 1
                    myFindCircleTool.RunParams.CaliperSearchDirection = CogFindCircleSearchDirectionConstants.Outward
            End Select
            myFindCircleTool.Run()
            ShowCircularArc(myFindCircleTool)
        Else
            Return False
        End If

        Return True
    End Function

    Private Function SetFindCalipersContrastThreshold(ByRef myFindCircleTool As Cognex.VisionPro.Caliper.CogFindCircleTool, ByVal ContrastThreshold As Integer)
        If myFindCircleTool IsNot Nothing Then
            myFindCircleTool.RunParams.CaliperRunParams.ContrastThreshold = ContrastThreshold
            FindCircleShowResult(myFindCircleTool)
        Else
            Return False
        End If

        Return True
    End Function


    Private Function SetFindCalipersFilterPixels(ByRef myFindCircleTool As Cognex.VisionPro.Caliper.CogFindCircleTool, ByVal FilterPixels As Integer)
        If myFindCircleTool IsNot Nothing Then
            myFindCircleTool.RunParams.CaliperRunParams.FilterHalfSizeInPixels = FilterPixels
            FindCircleShowResult(myFindCircleTool)
        Else
            Return False
        End If

        Return True
    End Function



    Dim RunStatus As enmResultConstants
    Private Function FindCircleShowResult(ByVal myFindCircleTool As Cognex.VisionPro.Caliper.CogFindCircleTool, Optional ByVal RunStatus As enmResultConstants = enmResultConstants.Accept) As Boolean
        Try
            If myFindCircleTool IsNot Nothing Then
                If myFindCircleTool.InputImage IsNot Nothing Then
                    myFindCircleTool.Run()
                    'myFindCircleTool.GetCircle.CopyBase(CogCopyShapeConstants.All)
                    If myFindCircleTool.Results IsNot Nothing Then
                        If myFindCircleTool.Results.NumPointsUsed > 3 Then
                            Dim mGetCircle As ICogGraphic
                            mGetCircle = myFindCircleTool.Results.GetCircle.CopyBase(CogCopyShapeConstants.All)
                            If RunStatus = enmResultConstants.Accept Then
                                mGetCircle.Color = Cognex.VisionPro.CogColorConstants.Green
                            Else
                                mGetCircle.Color = Cognex.VisionPro.CogColorConstants.Red
                            End If
                            CogDisplay1.StaticGraphics.Add(mGetCircle, "")

                            '[Note]圓心
                            DrawCenter(myFindCircleTool.Results.GetCircle.CenterX, myFindCircleTool.Results.GetCircle.CenterY, 10)

                            '[Note]找圓卡尺結果
                            Dim HLine, VLIne As New Cognex.VisionPro.CogLineSegment
                            Dim hs, he, vs, ve As Double
                            HLine.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                            VLIne.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                            HLine.LineWidthInScreenPixels = 1 '3
                            VLIne.LineWidthInScreenPixels = 1 '3

                            For i As Integer = 0 To myFindCircleTool.Results.Count - 1
                                If myFindCircleTool.Results.Item(i).Found Then '[Note]有卡尺資料
                                    If myFindCircleTool.Results.Item(i).Used Then
                                        HLine.Color = Cognex.VisionPro.CogColorConstants.Green
                                        VLIne.Color = Cognex.VisionPro.CogColorConstants.Green
                                    Else
                                        HLine.Color = Cognex.VisionPro.CogColorConstants.Red
                                        VLIne.Color = Cognex.VisionPro.CogColorConstants.Red
                                    End If

                                    hs = myFindCircleTool.Results.Item(i).X - 5
                                    he = myFindCircleTool.Results.Item(i).X + 5
                                    vs = myFindCircleTool.Results.Item(i).Y - 5
                                    ve = myFindCircleTool.Results.Item(i).Y + 5
                                    HLine.SetStartEnd(hs, myFindCircleTool.Results.Item(i).Y, he, myFindCircleTool.Results.Item(i).Y)
                                    VLIne.SetStartEnd(myFindCircleTool.Results.Item(i).X, vs, myFindCircleTool.Results.Item(i).X, ve)
                                    'HLine.SetFromStartXYEndXY(hs, myFindCircleTool.Results.Item(i).Y, he, myFindCircleTool.Results.Item(i).Y)
                                    'VLIne.SetFromStartXYEndXY(myFindCircleTool.Results.Item(i).X, vs, myFindCircleTool.Results.Item(i).X, ve)
                                    CogDisplay1.StaticGraphics.Add(HLine, "##")
                                    CogDisplay1.StaticGraphics.Add(VLIne, "##")
                                End If
                            Next
                        Else
                            '[Note]沒訓練結果
                            Return False
                        End If
                        CogDisplay1.Fit(True)
                    End If
                Else
                    '[Note]沒輸入影像
                    Return False
                End If
            Else
                '[Note]工具不存在
                Return False
            End If
            Return True

        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function


#End Region



#Region "找角工具"
    Dim mLineSegment_1 As New CogLineSegment
    Dim dragLineSegment_1 As New CogLineSegment
    Dim mLineSegment_2 As New CogLineSegment
    Dim dragLineSegment_2 As New CogLineSegment
    Private Sub mLineSegment_1_Dragging(ByVal sender As System.Object, ByVal e As CogDraggingEventArgs)
        Debug.Print("mLineSegment_1_Dragging:" & shiftIsDown)
        dragLineSegment_1 = CType(mLineSegment_1, CogLineSegment)
        'dragLineSegment_1.StartX = Math.Ceiling(mLineSegment_1.StartX)   '取整數
        'dragLineSegment_1.StartY = Math.Ceiling(mLineSegment_1.StartY)   '取整數
        'dragLineSegment_1.EndX = Math.Ceiling(mLineSegment_1.EndX)   '取整數
        'dragLineSegment_1.EndY = Math.Ceiling(mLineSegment_1.EndY)   '取整數
        'End If
        'dragLineSegment_2 = CType(mLineSegment_2, CogLineSegment)
        'mLineSegment_2.StartX = (dragLineSegment_1.StartX)
        'mLineSegment_2.StartY = (dragLineSegment_1.StartY)
    End Sub

    Private Sub mLineSegment_1_DraggingStopped(ByVal sender As System.Object, ByVal e As CogDraggingEventArgs)
        Debug.Print("mLineSegment_1_DraggingStopped:" & shiftIsDown)
        mLineSegment_1_Dragging(sender, e)
        '[Note]更新介面數值
        dragRectangleAffine = CType(mRectangleAffine, CogRectangleAffine)
        nmcSearchLength.Value = Math.Ceiling(dragRectangleAffine.SideXLength)
        nmcProjectionLength.Value = Math.Ceiling(dragRectangleAffine.SideYLength)


        mLineSegment_2_DraggingStopped(sender, e)
    End Sub

    Private Sub mLineSegment_2_Dragging(ByVal sender As System.Object, ByVal e As CogDraggingEventArgs)

        'Debug.Print("mLineSegment_2_Dragging:" & shiftIsDown)
        'dragLineSegment_1 = CType(mLineSegment_1, CogLineSegment)
        'dragLineSegment_2 = CType(mLineSegment_2, CogLineSegment)
        'dragLineSegment_2.StartX = (dragLineSegment_1.StartX)
        'dragLineSegment_2.StartY = (dragLineSegment_1.StartY)

    End Sub

    Private Sub mLineSegment_2_DraggingStopped(ByVal sender As System.Object, ByVal e As CogDraggingEventArgs)
        Debug.Print("mLineSegment_2_DraggingStopped:" & shiftIsDown)
        mLineSegment_2_Dragging(sender, e)
        '[Note]更新介面數值
        'dragRectangleAffine = CType(mRectangleAffine, CogRectangleAffine)
        'nmcSearchLength.Value = Math.Ceiling(dragRectangleAffine.SideXLength)
        'nmcProjectionLength.Value = Math.Ceiling(dragRectangleAffine.SideYLength)

    End Sub

    Private Function ShowCornerLine(ByRef myFindLineTool_1 As CogFindLineTool, ByRef myFindLineTool_2 As CogFindLineTool)
        If (myFindLineTool_1 IsNot Nothing) And (myFindLineTool_2 IsNot Nothing) Then
            If (myFindLineTool_1.InputImage IsNot Nothing) And (myFindLineTool_2.InputImage IsNot Nothing) Then
                '[Note]線段一
                Dim myRec As ICogRecord
                myRec = myFindLineTool_1.CreateCurrentRecord()
                mLineSegment_1 = CType(myRec.SubRecords("InputImage").SubRecords("ExpectedShapeSegment").Content, CogLineSegment)
                myCircularRegions = CType(myRec.SubRecords("InputImage").SubRecords("CaliperRegions").Content, CogGraphicCollection)
                CogDisplay1.InteractiveGraphics.Add(mLineSegment_1, "LineSegment", True)
                AddHandler mLineSegment_1.Dragging, AddressOf mLineSegment_1_Dragging
                AddHandler mLineSegment_1.DraggingStopped, AddressOf mLineSegment_1_DraggingStopped
                If myCircularRegions IsNot Nothing Then
                    Dim mGraphic As ICogGraphic
                    For i As Integer = 0 To myCircularRegions.Count - 1
                        mGraphic = myCircularRegions.Item(i)
                        If i = 0 Then
                            If mGraphic.GetType = GetType(CogRectangleAffine) Then
                                mRectangleAffine = CType(mGraphic, CogRectangleAffine)
                                CogDisplay1.InteractiveGraphics.Add(mRectangleAffine, "RectangleAffine", True)
                            End If
                        ElseIf mGraphic.GetType = GetType(CogPointMarker) Then
                            mPointMarker = CType(mGraphic, CogPointMarker)
                            CogDisplay1.InteractiveGraphics.Add(mPointMarker, "PointMarker", True)
                        Else
                            CogDisplay1.InteractiveGraphics.Add(mGraphic, "Graphic", True)
                        End If

                        'AddHandler CogDisplay1.InteractiveGraphics(CogDisplay1.InteractiveGraphics.Count - 1).Dragging, AddressOf mRectangleAffine_Dragging
                        'AddHandler CogDisplay1.InteractiveGraphics(CogDisplay1.InteractiveGraphics.Count - 1).DraggingStopped, AddressOf mRectangleAffine_DraggingStopped
                    Next
                    AddHandler mPointMarker.Dragging, AddressOf mRectangleAffine_Dragging
                    AddHandler mPointMarker.DraggingStopped, AddressOf mRectangleAffine_DraggingStopped
                End If
                '[Note]線段二
                Dim myRec_2 As ICogRecord
                myRec_2 = myFindLineTool_2.CreateCurrentRecord()
                mLineSegment_2 = CType(myRec_2.SubRecords("InputImage").SubRecords("ExpectedShapeSegment").Content, CogLineSegment)
                myCircularRegions = CType(myRec_2.SubRecords("InputImage").SubRecords("CaliperRegions").Content, CogGraphicCollection)
                CogDisplay1.InteractiveGraphics.Add(mLineSegment_2, "LineSegment2", True)
                AddHandler mLineSegment_2.Dragging, AddressOf mLineSegment_2_Dragging
                AddHandler mLineSegment_2.DraggingStopped, AddressOf mLineSegment_2_DraggingStopped

                If myCircularRegions IsNot Nothing Then
                    Dim mGraphic As ICogGraphic
                    For i As Integer = 0 To myCircularRegions.Count - 1
                        mGraphic = myCircularRegions.Item(i)
                        If mGraphic.GetType = GetType(CogPointMarker) Then
                            '不顯示PointMarker 避免介面被更改
                            '    mPointMarker = CType(mGraphic, CogPointMarker)
                            '    CogDisplay1.InteractiveGraphics.Add(mPointMarker, "PointMarker", True)
                        Else
                            CogDisplay1.InteractiveGraphics.Add(mGraphic, "Graphic", True)
                        End If
                    Next


                End If

                CogDisplay1.Image = myFindLineTool_1.InputImage
                CogDisplay1.Fit(True)
            End If
        Else
            Return False
        End If

        Return True
    End Function

    Private Function SetFindCornerCalipersNumber(ByRef myFindLine1 As CogFindLineTool, ByRef myFindLine2 As CogFindLineTool, ByVal NumCalipers As Integer)
        If (myFindLine1 IsNot Nothing) And (myFindLine2 IsNot Nothing) Then
            CogDisplay1.StaticGraphics.Clear()
            CogDisplay1.InteractiveGraphics.Clear()
            myFindLine1.RunParams.NumCalipers = NumCalipers
            myFindLine2.RunParams.NumCalipers = NumCalipers


            myFindLine1.Run()
            myFindLine2.Run()
            ShowCornerLine(myFindLine1, myFindLine2)

        Else
            Return False
        End If

        Return True
    End Function


    Private Function SetFindCornerCalipersIgnoreNumber(ByRef myFindLine1 As CogFindLineTool, ByRef myFindLine2 As CogFindLineTool, ByVal IgnoreNumber As Integer)
        If (myFindLine1 IsNot Nothing) And (myFindLine2 IsNot Nothing) Then
            If IgnoreNumber = 0 Then
                myFindLine1.RunParams.DecrementNumToIgnore = False
                myFindLine2.RunParams.DecrementNumToIgnore = False
            Else
                myFindLine1.RunParams.DecrementNumToIgnore = True
                myFindLine2.RunParams.DecrementNumToIgnore = True
            End If
            myFindLine1.RunParams.NumToIgnore = IgnoreNumber
            myFindLine2.RunParams.NumToIgnore = IgnoreNumber

            ShowCornerLine(myFindLine1, myFindLine2)
        Else
            Return False
        End If

        Return True
    End Function

    Private Function SetFindCornerSearchLength(ByRef myFindLine1 As CogFindLineTool, ByRef myFindLine2 As CogFindLineTool, ByVal CaliperSearchLength As Integer)
        If (myFindLine1 IsNot Nothing) And (myFindLine2 IsNot Nothing) Then
            CogDisplay1.StaticGraphics.Clear()
            CogDisplay1.InteractiveGraphics.Clear()
            myFindLine1.RunParams.CaliperSearchLength = CaliperSearchLength
            myFindLine2.RunParams.CaliperSearchLength = CaliperSearchLength
            ShowCornerLine(myFindLine1, myFindLine2)
        Else
            Return False
        End If

        Return True
    End Function

    Private Function SetFindCornerProjectionLength(ByRef myFindLine1 As CogFindLineTool, ByRef myFindLine2 As CogFindLineTool, ByVal ProjectionLength As Integer)
        If (myFindLine1 IsNot Nothing) And (myFindLine2 IsNot Nothing) Then
            myFindLine1.RunParams.CaliperProjectionLength = ProjectionLength
            myFindLine2.RunParams.CaliperProjectionLength = ProjectionLength
            ShowCornerLine(myFindLine1, myFindLine2)
        Else
            Return False
        End If

        Return True
    End Function

    Private Function SetFindCornerPolarity(ByRef myFindLine1 As CogFindLineTool, ByRef myFindLine2 As CogFindLineTool, ByVal Polarity As Integer)
        If (myFindLine1 IsNot Nothing) And (myFindLine2 IsNot Nothing) Then
            myFindLine1.RunParams.CaliperRunParams.EdgeMode = CogCaliperEdgeModeConstants.SingleEdge
            myFindLine2.RunParams.CaliperRunParams.EdgeMode = CogCaliperEdgeModeConstants.SingleEdge
            Select Case Polarity
                Case 0
                    myFindLine1.RunParams.CaliperRunParams.Edge0Polarity = CogCaliperPolarityConstants.DarkToLight
                    myFindLine2.RunParams.CaliperRunParams.Edge0Polarity = CogCaliperPolarityConstants.DarkToLight
                Case 1
                    myFindLine1.RunParams.CaliperRunParams.Edge0Polarity = CogCaliperPolarityConstants.LightToDark
                    myFindLine2.RunParams.CaliperRunParams.Edge0Polarity = CogCaliperPolarityConstants.LightToDark
                Case 2
                    myFindLine1.RunParams.CaliperRunParams.Edge0Polarity = CogCaliperPolarityConstants.DontCare
                    myFindLine2.RunParams.CaliperRunParams.Edge0Polarity = CogCaliperPolarityConstants.DontCare
            End Select
            FindCornerShowResult(myFindLine1, myFindLine2)

            Return False
        End If

        Return True
    End Function

    Private Function SetFindCornerSearchDirection(ByRef myFindLine1 As CogFindLineTool, ByRef myFindLine2 As CogFindLineTool, ByVal SearchDirection As Integer)
        If (myFindLine1 IsNot Nothing) And (myFindLine2 IsNot Nothing) Then
            'rad = angle * Math.PI / 180 
            Dim mRad As Double
            mRad = 90 * Math.PI / 180
            Select Case SearchDirection
                Case 0
                    myFindLine1.RunParams.CaliperSearchDirection = mRad
                    myFindLine2.RunParams.CaliperSearchDirection = mRad
                Case 1
                    myFindLine1.RunParams.CaliperSearchDirection = -mRad
                    myFindLine2.RunParams.CaliperSearchDirection = -mRad
            End Select
            myFindLine1.Run()
            myFindLine2.Run()
            ShowCornerLine(myFindLine1, myFindLine2)
        Else
            Return False
        End If

        Return True
    End Function

    Private Function SetFindCornerContrastThreshold(ByRef myFindLine1 As CogFindLineTool, ByRef myFindLine2 As CogFindLineTool, ByVal ContrastThreshold As Integer)
        If (myFindLine1 IsNot Nothing) And (myFindLine2 IsNot Nothing) Then
            myFindLine1.RunParams.CaliperRunParams.ContrastThreshold = ContrastThreshold
            myFindLine2.RunParams.CaliperRunParams.ContrastThreshold = ContrastThreshold
            FindCornerShowResult(myFindLine1, myFindLine2)
        Else
            Return False
        End If

        Return True
    End Function

    Private Function SetFindCornerFilterPixels(ByRef myFindLine1 As CogFindLineTool, ByRef myFindLine2 As CogFindLineTool, ByVal FilterPixels As Integer)
        If (myFindLine1 IsNot Nothing) And (myFindLine2 IsNot Nothing) Then
            myFindLine1.RunParams.CaliperRunParams.FilterHalfSizeInPixels = FilterPixels
            myFindLine2.RunParams.CaliperRunParams.FilterHalfSizeInPixels = FilterPixels
            FindCornerShowResult(myFindLine1, myFindLine2)
        Else
            Return False
        End If

        Return True
    End Function

    Private Function FindCornerShowResult(ByVal myFindLine1 As CogFindLineTool, ByVal myFindLine2 As CogFindLineTool) As Boolean
        Try
            If (myFindLine1 IsNot Nothing) And (myFindLine2 IsNot Nothing) Then
                If (myFindLine1.InputImage IsNot Nothing) And (myFindLine2.InputImage IsNot Nothing) Then
                    myFindLine1.Run()
                    myFindLine2.Run()
                    CogDisplay1_GraphicsClear()
                    'myFindCircleTool.GetCircle.CopyBase(CogCopyShapeConstants.All)
                    If (myFindLine1.Results IsNot Nothing) AndAlso (myFindLine1.Results.NumPointsUsed > 3) Then
                        Dim mGetLine_1 As ICogGraphic
                        mGetLine_1 = myFindLine1.Results.GetLineSegment.CopyBase(CogCopyShapeConstants.All)
                        mGetLine_1.Color = Cognex.VisionPro.CogColorConstants.Green
                        CogDisplay1.StaticGraphics.Add(mGetLine_1, "")

                        '[Note]找圓卡尺結果
                        Dim HLine, VLIne As New Cognex.VisionPro.CogLineSegment
                        Dim hs, he, vs, ve As Double
                        HLine.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                        VLIne.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                        HLine.LineWidthInScreenPixels = 1 '3
                        VLIne.LineWidthInScreenPixels = 1 '3
                        For i As Integer = 0 To myFindLine1.Results.Count - 1
                            If myFindLine1.Results.Item(i).Found Then '[Note]有卡尺資料
                                If myFindLine1.Results.Item(i).Used Then
                                    HLine.Color = Cognex.VisionPro.CogColorConstants.Green
                                    VLIne.Color = Cognex.VisionPro.CogColorConstants.Green
                                Else
                                    HLine.Color = Cognex.VisionPro.CogColorConstants.Red
                                    VLIne.Color = Cognex.VisionPro.CogColorConstants.Red
                                End If
                                hs = myFindLine1.Results.Item(i).X - 5
                                he = myFindLine1.Results.Item(i).X + 5
                                vs = myFindLine1.Results.Item(i).Y - 5
                                ve = myFindLine1.Results.Item(i).Y + 5
                                HLine.SetStartEnd(hs, myFindLine1.Results.Item(i).Y, he, myFindLine1.Results.Item(i).Y)
                                VLIne.SetStartEnd(myFindLine1.Results.Item(i).X, vs, myFindLine1.Results.Item(i).X, ve)
                                'HLine.SetFromStartXYEndXY(hs, myFindCircleTool.Results.Item(i).Y, he, myFindCircleTool.Results.Item(i).Y)
                                'VLIne.SetFromStartXYEndXY(myFindCircleTool.Results.Item(i).X, vs, myFindCircleTool.Results.Item(i).X, ve)
                                CogDisplay1.StaticGraphics.Add(HLine, "##")
                                CogDisplay1.StaticGraphics.Add(VLIne, "##")
                            End If
                        Next
                    Else
                        '[Note]沒訓練結果
                        Return False
                    End If



                    If myFindLine2.Results.NumPointsUsed > 3 Then
                        Dim mGetLine_2 As ICogGraphic
                        mGetLine_2 = myFindLine2.Results.GetLineSegment.CopyBase(CogCopyShapeConstants.All)
                        mGetLine_2.Color = Cognex.VisionPro.CogColorConstants.Green
                        CogDisplay1.StaticGraphics.Add(mGetLine_2, "")

                        '[Note]找圓卡尺結果
                        Dim HLine, VLIne As New Cognex.VisionPro.CogLineSegment
                        Dim hs, he, vs, ve As Double
                        HLine.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                        VLIne.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
                        HLine.LineWidthInScreenPixels = 1 '3
                        VLIne.LineWidthInScreenPixels = 1 '3
                        For i As Integer = 0 To myFindLine2.Results.Count - 1
                            If myFindLine2.Results.Item(i).Found Then '[Note]有卡尺資料
                                If myFindLine2.Results.Item(i).Used Then
                                    HLine.Color = Cognex.VisionPro.CogColorConstants.Green
                                    VLIne.Color = Cognex.VisionPro.CogColorConstants.Green
                                Else
                                    HLine.Color = Cognex.VisionPro.CogColorConstants.Red
                                    VLIne.Color = Cognex.VisionPro.CogColorConstants.Red
                                End If
                                hs = myFindLine2.Results.Item(i).X - 5
                                he = myFindLine2.Results.Item(i).X + 5
                                vs = myFindLine2.Results.Item(i).Y - 5
                                ve = myFindLine2.Results.Item(i).Y + 5
                                HLine.SetStartEnd(hs, myFindLine2.Results.Item(i).Y, he, myFindLine2.Results.Item(i).Y)
                                VLIne.SetStartEnd(myFindLine2.Results.Item(i).X, vs, myFindLine2.Results.Item(i).X, ve)
                                'HLine.SetFromStartXYEndXY(hs, myFindCircleTool.Results.Item(i).Y, he, myFindCircleTool.Results.Item(i).Y)
                                'VLIne.SetFromStartXYEndXY(myFindCircleTool.Results.Item(i).X, vs, myFindCircleTool.Results.Item(i).X, ve)
                                CogDisplay1.StaticGraphics.Add(HLine, "##")
                                CogDisplay1.StaticGraphics.Add(VLIne, "##")
                            End If
                        Next
                    Else
                        '[Note]沒訓練結果
                        Return False
                    End If


                    CogDisplay1.Fit(True)
                Else
                    '[Note]沒輸入影像
                    Return False
                End If
            Else
                '[Note]工具不存在
                Return False
            End If
            Return True

        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function





#End Region



#Region "卡尺設定介面"
    Private Sub mRectangleAffine_Dragging(ByVal sender As System.Object, ByVal e As CogDraggingEventArgs)
        'Debug.Print("mRectangleAffine_Dragging:" & shiftIsDown)
        dragRectangleAffine = CType(mRectangleAffine, CogRectangleAffine)
        'If Not myFindCircleTool.InputImage Is Nothing Then
        'If shiftIsDown Then
        dragRectangleAffine.SideXLength = Math.Ceiling(dragRectangleAffine.SideXLength) '取整數
        dragRectangleAffine.SideYLength = Math.Ceiling(dragRectangleAffine.SideYLength)
        'Debug.Print("SideXLength:" & dragRectangleAffine.SideXLength)
        'Debug.Print("SideYLength:" & dragRectangleAffine.SideYLength)
        'End If
        'End If

    End Sub

    Private Sub mRectangleAffine_DraggingStopped(ByVal sender As System.Object, ByVal e As CogDraggingEventArgs)
        'Debug.Print("mRectangleAffine_DraggingStopped:" & shiftIsDown)
        mRectangleAffine_Dragging(sender, e)
        '[Note]更新介面數值
        dragRectangleAffine = CType(mRectangleAffine, CogRectangleAffine)
        nmcSearchLength.Value = Math.Ceiling(dragRectangleAffine.SideXLength)
        nmcProjectionLength.Value = Math.Ceiling(dragRectangleAffine.SideYLength)

    End Sub


    Private Sub btnShowCalipers_Click(sender As Object, e As EventArgs) Handles btnShowCalipers.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        'If IsNothing(lstScene.SelectedItem) Then
        '    Exit Sub
        'End If
        'Dim SceneName = lstScene.SelectedItem
        If IsNothing(SceneName) Then
            Exit Sub
        End If

        btnShowCalipers.Enabled = False
        CogDisplay1_GraphicsClear()
        myToolBlock.Run()
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            Select Case gAOICollection.SceneDictionary(SceneName).AlignType
                Case eAlignType.Circle, eAlignType.Blob
                    If myPixelMapTool IsNot Nothing Then
                        myPixelMapTool.Run()
                        myFindCircleTool.InputImage = myPixelMapTool.OutputImage
                    Else
                        If myToolBlock.Inputs.Contains("InputImage") Then
                            myFindCircleTool.InputImage = myToolBlock.Inputs("InputImage").Value
                        Else
                            gSyslog.Save(GetString("Please Check Image Source"))
                            MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        End If
                    End If
                    'myFindCircleTool.InputImage = myPixelMapTool.OutputImage
                    myFindCircleTool.CurrentRecordEnable = CogFindCircleCurrentRecordConstants.All
                    ShowCircularArc(myFindCircleTool)
                Case eAlignType.Corner, eAlignType.Lane
                    If myPixelMapTool IsNot Nothing Then
                        myPixelMapTool.Run()
                        myFindLineTool_1.InputImage = myPixelMapTool.OutputImage
                        myFindLineTool_2.InputImage = myPixelMapTool.OutputImage
                    Else
                        If myToolBlock.Inputs.Contains("InputImage") Then
                            myFindLineTool_1.InputImage = myToolBlock.Inputs("InputImage").Value
                            myFindLineTool_2.InputImage = myToolBlock.Inputs("InputImage").Value
                        Else
                            gSyslog.Save(GetString("Please Check Image Source"))
                            MsgBox(GetString("Please Check Image Source"), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                        End If
                    End If
                    myFindLineTool_1.CurrentRecordEnable = CogFindCircleCurrentRecordConstants.All
                    myFindLineTool_2.CurrentRecordEnable = CogFindCircleCurrentRecordConstants.All
                    ShowCornerLine(myFindLineTool_1, myFindLineTool_2)
                Case Else

            End Select
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnShowCalipers]" & vbTab & "ClickEnd")
        btnShowCalipers.Enabled = True
    End Sub



    Private Sub nmcCalipersNumber_ValueChanged(sender As Object, e As EventArgs) Handles nmcCalipersNumber.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If
        If IsNothing(SceneName) Then
            Exit Sub
        End If
        'Dim SceneName = lstScene.SelectedItem
        CogDisplay1_GraphicsClear()
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            Select Case gAOICollection.SceneDictionary(SceneName).AlignType
                Case eAlignType.Circle, eAlignType.Blob
                    If nmcCalipersNumber.Value - nmcCalipersIgnoreNumber.Value < 3 Then
                        'nmcCalipersIgnoreNumber.Value = nmcCalipersNumber.Value - 3
                        nmcCalipersNumber.Value = nmcCalipersIgnoreNumber.Value + 3
                        Exit Sub
                    End If
                    SetFindCircleCalipersNumber(myFindCircleTool, nmcCalipersNumber.Value)
                Case eAlignType.Corner, eAlignType.Lane
                    If nmcCalipersNumber.Value - nmcCalipersIgnoreNumber.Value < 2 Then
                        'nmcCalipersIgnoreNumber.Value = nmcCalipersNumber.Value - 2
                        nmcCalipersNumber.Value = nmcCalipersIgnoreNumber.Value + 2
                        Exit Sub
                    End If
                    SetFindCornerCalipersNumber(myFindLineTool_1, myFindLineTool_2, nmcCalipersNumber.Value)
                Case Else

            End Select
        End If
    End Sub



    Private Sub nmcSearchLength_ValueChanged(sender As Object, e As EventArgs) Handles nmcSearchLength.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If
        'If IsNothing(lstScene.SelectedItem) Then
        '    Exit Sub
        'End If
        'Dim SceneName = lstScene.SelectedItem
        If IsNothing(SceneName) Then
            Exit Sub
        End If
        CogDisplay1_GraphicsClear()
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            Select Case gAOICollection.SceneDictionary(SceneName).AlignType
                Case eAlignType.Circle, eAlignType.Blob
                    SetFindCircleSearchLength(myFindCircleTool, nmcSearchLength.Value)
                Case eAlignType.Corner, eAlignType.Lane
                    SetFindCornerSearchLength(myFindLineTool_1, myFindLineTool_2, nmcSearchLength.Value)
                Case Else

            End Select
        End If
    End Sub


    Private Sub nmcProjectionLength_ValueChanged(sender As Object, e As EventArgs) Handles nmcProjectionLength.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If
        'If IsNothing(lstScene.SelectedItem) Then
        '    Exit Sub
        'End If
        'Dim SceneName = lstScene.SelectedItem
        If IsNothing(SceneName) Then
            Exit Sub
        End If
        CogDisplay1_GraphicsClear()
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            Select Case gAOICollection.SceneDictionary(SceneName).AlignType
                Case eAlignType.Circle, eAlignType.Blob
                    SetFindCircleProjectionLength(myFindCircleTool, nmcProjectionLength.Value)
                Case eAlignType.Corner, eAlignType.Lane
                    SetFindCornerProjectionLength(myFindLineTool_1, myFindLineTool_2, nmcProjectionLength.Value)
                Case Else

            End Select
        End If
    End Sub


    Private Sub nmcCalipersIgnoreNumber_ValueChanged(sender As Object, e As EventArgs) Handles nmcCalipersIgnoreNumber.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If
        'If IsNothing(lstScene.SelectedItem) Then
        '    Exit Sub
        'End If
        'Dim SceneName = lstScene.SelectedItem
        If IsNothing(SceneName) Then
            Exit Sub
        End If
        CogDisplay1_GraphicsClear()
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            Select Case gAOICollection.SceneDictionary(SceneName).AlignType
                Case eAlignType.Circle, eAlignType.Blob
                    If nmcCalipersNumber.Value - nmcCalipersIgnoreNumber.Value < 3 Then
                        nmcCalipersNumber.Value = nmcCalipersIgnoreNumber.Value + 3
                        Exit Sub
                    End If
                    SetFindCircleCalipersIgnoreNumber(myFindCircleTool, nmcCalipersIgnoreNumber.Value)
                Case eAlignType.Corner, eAlignType.Lane
                    If nmcCalipersNumber.Value - nmcCalipersIgnoreNumber.Value < 2 Then
                        nmcCalipersNumber.Value = nmcCalipersIgnoreNumber.Value + 2
                        Exit Sub
                    End If
                    SetFindCornerCalipersIgnoreNumber(myFindLineTool_1, myFindLineTool_2, nmcCalipersIgnoreNumber.Value)
                Case Else

            End Select
        End If
    End Sub




    Private Sub cmbCaplipersPolarity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCaplipersPolarity.SelectedIndexChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If
        If cmbCaplipersPolarity.SelectedIndex < 0 Then
            Exit Sub
        End If
        'If IsNothing(lstScene.SelectedItem) Then
        '    Exit Sub
        'End If
        'Dim SceneName = lstScene.SelectedItem
        If IsNothing(SceneName) Then
            Exit Sub
        End If
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            Select Case gAOICollection.SceneDictionary(SceneName).AlignType
                Case eAlignType.Circle, eAlignType.Blob
                    SetFindCaplipersPolarity(myFindCircleTool, cmbCaplipersPolarity.SelectedIndex)
                Case eAlignType.Corner, eAlignType.Lane
                    SetFindCornerPolarity(myFindLineTool_1, myFindLineTool_2, cmbCaplipersPolarity.SelectedIndex)
                Case Else

            End Select
        End If

    End Sub




    Private Sub cmbCaplipersSearchDirection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCaplipersSearchDirection.SelectedIndexChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If
        If cmbCaplipersSearchDirection.SelectedIndex < 0 Then
            Exit Sub
        End If
        'If IsNothing(lstScene.SelectedItem) Then
        '    Exit Sub
        'End If
        'Dim SceneName = lstScene.SelectedItem
        If IsNothing(SceneName) Then
            Exit Sub
        End If
        CogDisplay1_GraphicsClear()
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            Select Case gAOICollection.SceneDictionary(SceneName).AlignType
                Case eAlignType.Circle, eAlignType.Blob
                    SetFindCaplipersSearchDirection(myFindCircleTool, cmbCaplipersSearchDirection.SelectedIndex)
                Case eAlignType.Corner, eAlignType.Lane
                    SetFindCornerSearchDirection(myFindLineTool_1, myFindLineTool_2, cmbCaplipersSearchDirection.SelectedIndex)
                Case Else

            End Select
        End If

    End Sub


    Private Sub nmcContrastThreshold_ValueChanged(sender As Object, e As EventArgs) Handles nmcContrastThreshold.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If
        'If IsNothing(lstScene.SelectedItem) Then
        '    Exit Sub
        'End If
        'Dim SceneName = lstScene.SelectedItem
        If IsNothing(SceneName) Then
            Exit Sub
        End If
        CogDisplay1_GraphicsClear()
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            Select Case gAOICollection.SceneDictionary(SceneName).AlignType
                Case eAlignType.Circle, eAlignType.Blob
                    SetFindCalipersContrastThreshold(myFindCircleTool, nmcContrastThreshold.Value)
                Case eAlignType.Corner, eAlignType.Lane
                    SetFindCornerContrastThreshold(myFindLineTool_1, myFindLineTool_2, nmcContrastThreshold.Value)
                Case Else

            End Select
        End If

    End Sub


    Private Sub nmclblFilterHalfSizePixels_ValueChanged(sender As Object, e As EventArgs) Handles nmclblFilterHalfSizePixels.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If
        'If IsNothing(lstScene.SelectedItem) Then
        '    Exit Sub
        'End If
        'Dim SceneName = lstScene.SelectedItem
        If IsNothing(SceneName) Then
            Exit Sub
        End If
        CogDisplay1_GraphicsClear()
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            Select Case gAOICollection.SceneDictionary(SceneName).AlignType
                Case eAlignType.Circle, eAlignType.Blob
                    SetFindCalipersFilterPixels(myFindCircleTool, nmclblFilterHalfSizePixels.Value)
                Case eAlignType.Corner, eAlignType.Lane
                    SetFindCornerFilterPixels(myFindLineTool_1, myFindLineTool_2, nmclblFilterHalfSizePixels.Value)
                Case Else

            End Select
        End If
    End Sub

    Private Sub DrawCenter(ByVal CenterX As Double, ByVal CenterY As Double, ByVal LineLength As Integer)
        'Dim CenterXLine, CenterYLine As New Cognex.VisionPro.CogLineSegment
        'CenterXLine.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
        'CenterYLine.LineStyle = Cognex.VisionPro.CogGraphicLineStyleConstants.Solid
        'CenterXLine.LineWidthInScreenPixels = 3
        'CenterYLine.LineWidthInScreenPixels = 3
        'CenterXLine.Color = Cognex.VisionPro.CogColorConstants.Green
        'CenterYLine.Color = Cognex.VisionPro.CogColorConstants.Green
        'CenterXLine.SetStartEnd(CenterX - LineLength, CenterY, CenterX + LineLength, CenterY)
        'CenterYLine.SetStartEnd(CenterX, CenterY - LineLength, CenterX, CenterY + LineLength)
        'CogDisplay1.StaticGraphics.Add(CenterXLine, "##")
        'CogDisplay1.StaticGraphics.Add(CenterYLine, "##")
        Dim marker As New CogPointMarker()
        marker.X = CenterX
        marker.Y = CenterY
        marker.Color = CogColorConstants.Green
        CogDisplay1.StaticGraphics.Add(marker, "#")
    End Sub

    Private Sub DrawAlignTypeRegion(ByVal mToolBlock As CogToolBlock, ByVal mAlignType As eAlignType)

        Select Case mAlignType
            Case eAlignType.Circle, eAlignType.Blob
                FindCircleShowResult(myFindCircleTool)
            Case eAlignType.LoadFile

            Case Else
                'FindPMAlignResult(myPMTool)
                CogDisplay1.StaticGraphics.Add(myPMTool.Results(0).CreateResultGraphics(Cognex.VisionPro.PMAlign.CogPMAlignResultGraphicConstants.MatchRegion), "#")
        End Select


    End Sub


    Private Sub btnCaliperRun_Click(sender As Object, e As EventArgs) Handles btnCaliperRun.Click
        Dim btn As Button = sender
        If btn.Enabled = False Then '[Note]防連按
            Exit Sub
        End If
        gSyslog.Save("[" & Me.Name.ToString() & "]" & vbTab & btn.Name.ToString() & vbTab & "Click")

        btnCaliperRun.Enabled = False
        CogDisplay1_GraphicsClear()
        myToolBlock.Run()

        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            Select Case gAOICollection.SceneDictionary(SceneName).AlignType
                Case eAlignType.Circle, eAlignType.Blob
                    FindCircleShowResult(myFindCircleTool)
                Case eAlignType.Corner, eAlignType.Lane
                    FindCornerShowResult(myFindLineTool_1, myFindLineTool_2)
                    myToolBlock.Run()
                    If myToolBlock.Outputs.Contains("Results_Item_0_TranslationX") Then
                        If myToolBlock.Outputs.Contains("Results_Item_0_TranslationY") Then
                            DrawCenter(myToolBlock.Outputs("Results_Item_0_TranslationX").Value, myToolBlock.Outputs("Results_Item_0_TranslationY").Value, 10)
                        End If
                    End If
                Case Else

            End Select
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCaliperRun]" & vbTab & "ClickEnd")
        btnCaliperRun.Enabled = True
    End Sub

    Private Function ShowRunStatus() As Boolean
        Try
            If myToolBlock IsNot Nothing Then
                If myToolBlock.Outputs.Contains("RunStatus_Result") Then
                    Dim RunStatus As enmResultConstants
                    RunStatus = myToolBlock.Outputs("RunStatus_Result").Value
                    FindCircleShowResult(myFindCircleTool, RunStatus)
                End If
            End If
            Return True
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function



    Private Function SetCircleRadiusTolerance(ByVal RadiusTolerance As Integer) As Boolean
        Try
            If myToolBlock.Inputs.Contains("RadiusTolerance") Then
                myToolBlock.Inputs("RadiusTolerance").Value = RadiusTolerance
                myToolBlock.Run()

                ShowRunStatus()
            End If
            Return True
        Catch ex As Exception
            MsgBox("Exception Message: " & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try
    End Function

    Private Sub nmcRadiusTolerance_ValueChanged(sender As Object, e As EventArgs) Handles nmcRadiusTolerance.ValueChanged
        '[Note]頁面還沒載完不處理
        If Flag_frmLoadDone = False Then
            Exit Sub
        End If
        If IsNothing(SceneName) Then
            Exit Sub
        End If
        If gAOICollection.SceneDictionary.ContainsKey(SceneName) Then
            Select Case gAOICollection.SceneDictionary(SceneName).AlignType
                Case eAlignType.Circle, eAlignType.Blob
                    SetCircleRadiusTolerance(nmcRadiusTolerance.Value)
                Case Else

            End Select
        End If
    End Sub


#End Region



End Class